Imports common
Public Class frmSelectLocation
    Public strRmks As String = ""
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        btnOKPressed()
    End Sub

    Sub btnOKPressed()
        Try
            If rdoSingle.IsChecked = False AndAlso rdoMultiple.IsChecked = False Then
                Throw New Exception("Please Select Location")
            End If

            If rdoSingle.IsChecked AndAlso clsCommon.myLen(txtLocation.Value) <= 0 Then
                Throw New Exception("Please Select Location")
            End If

            If rdoSingle.IsChecked Then
                Dim qry As String = "update TSPL_USER_MASTER SET DEFAULT_LOCATION='" + txtLocation.Value + "' WHERE USER_CODE='" + objCommonVar.CurrentUserCode + "' "
                clsDBFuncationality.ExecuteNonQuery(qry)

                qry = "select COUNT(1)count from TSPL_GL_SEGMENT_PERMISSION WHERE User_Code='" + objCommonVar.CurrentUserCode + "' " & _
                      "and Segment_Code=(select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "')"
                Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
                If count = 0 Then
                    qry = "select Seg_No from TSPL_GL_SEGMENT_CODE where Segment_name ='LOCATION' "
                    Dim segNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

                    qry = "select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "' "
                    Dim SegmentCode As String = clsDBFuncationality.getSingleValue(qry)
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "User_Code", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "GL_Segment", segNo)
                    clsCommon.AddColumnsForChange(coll, "Segment_Code", SegmentCode)
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "Default_Segment", "N")
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GL_SEGMENT_PERMISSION", OMInsertOrUpdate.Insert, "")
                End If
            End If
            Me.Close()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        btnCancePressed()
    End Sub

    Sub btnCancePressed()
        strRmks = ""
        Me.Close()
    End Sub

    Private Sub frmSelectLocation_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnOKPressed()
        ElseIf e.KeyCode = Keys.Escape Then
            btnCancePressed()
        End If
    End Sub

    Private Sub rdoSingle_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdoSingle.ToggleStateChanged
        txtLocation.Enabled = True
    End Sub

    Private Sub rdoMultiple_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdoMultiple.ToggleStateChanged
        txtLocation.Enabled = False
        txtLocation.Value = ""
        lblLocation.Text = ""
    End Sub

  
    Private Sub txtLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtLocation._MYValidating
        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = " Location_Type='Physical'  "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtLocation.Value = clsCommon.ShowSelectForm("ShipmentMasteidfndr", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
        lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))

    End Sub
End Class
