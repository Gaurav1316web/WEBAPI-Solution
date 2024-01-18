Imports common
Imports System.Data
Imports System.Data.SqlClient

Public Class frmRackBinMaster
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String

#End Region

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Save()
    End Sub

    Public Sub Save()
        Try
            If AllowToSave() Then

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.DistrictMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If

                If clsCommon.CompairString(rbtnRock.IsChecked, True) = CompairStringResult.Equal Then
                    Dim obj As New clsRackMaster()
                    obj.Rack_Code = txtCode.Value
                    obj.Description = txtName.Text
                    obj.Location = txtLocation.Value
                    If (clsRackMaster.SaveData(obj, isNewEntry)) Then
                        common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                        LoadData(obj.Rack_Code, NavigatorType.Current)
                        btnSave.Text = "Update"
                        btnDelete.Enabled = True
                    Else
                        btnSave.Text = "Save"
                        btnDelete.Enabled = False
                    End If
                Else
                    Dim objBin As New clsBinMaster()
                    objBin.Bin_Code = txtCode.Value
                    objBin.Rack_Code = txtRockCode.Value
                    objBin.Description = txtName.Text
                    objBin.Location = txtLocation.Value
                    If (clsBinMaster.SaveData(objBin, isNewEntry)) Then
                        common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                        LoadData(objBin.Bin_Code, NavigatorType.Current)
                        btnSave.Text = "Update"
                        btnDelete.Enabled = True
                    Else
                        btnSave.Text = "Save"
                        btnDelete.Enabled = False
                    End If
                End If

             

            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        txtCode.MyReadOnly = True
        btnSave.Enabled = True
        btnDelete.Enabled = True
        isNewEntry = False
        Dim obj As New clsRackMaster()
        Dim objBin As New clsBinMaster()
        If clsCommon.CompairString(rbtnRock.IsChecked, True) = CompairStringResult.Equal Then
            obj = clsRackMaster.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Rack_Code) > 0) Then
                'funReset()
                isNewEntry = False
                btnSave.Text = "Update"
                btnDelete.Enabled = True
                txtCode.Value = obj.Rack_Code
                txtName.Text = obj.Description
                txtLocation.Value = obj.Location
                lblBillToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
            End If
        Else
            objBin = clsBinMaster.GetData(strCode, NavTyep)
            If (objBin IsNot Nothing AndAlso clsCommon.myLen(objBin.Bin_Code) > 0) Then
                'funReset()
                isNewEntry = False
                btnSave.Text = "Update"
                btnDelete.Enabled = True
                txtCode.Value = objBin.Bin_Code
                txtName.Text = objBin.Description
                txtLocation.Value = objBin.Location
                lblBillToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
                txtRockCode.Value = objBin.Rack_Code
                lblRockDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_Rack_MASTER where Code='" + txtRockCode.Value + "'"))
            End If
        End If

      
    End Sub

    Function AllowToSave() As Boolean
       If clsCommon.myLen(txtName.Text) <= 0 Then
            myMessages.blankValue("Description")
            txtName.Focus()
            Return False
        ElseIf clsCommon.myLen(txtLocation.Value) <= 0 Then
            myMessages.blankValue("Select location")
            txtLocation.Focus()
            Return False
        End If
        If clsCommon.CompairString(rbtnBin.IsChecked, True) = CompairStringResult.Equal Then
            If clsCommon.myLen(txtRockCode.Value) <= 0 Then
                myMessages.blankValue("Select Rack")
                txtRockCode.Focus()
                Return False
            End If
        End If
        Return True
    End Function


    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "You Cannot Delete Record", Me.Text)
            Exit Sub
        End If
        ' Code Ends 
        funDelete()
    End Sub

    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If clsCommon.CompairString(rbtnRock.IsChecked, True) = CompairStringResult.Equal Then
                    If (clsRackMaster.DeleteData(txtCode.Value)) Then
                        common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                        funReset()
                    End If
                Else
                    If (clsBinMaster.DeleteData(txtCode.Value)) Then
                        common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                        funReset()
                    End If
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub frmStateMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isNewEntry = True

        funReset()
        'rbtnRock.Text = "Rack"
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ' ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")


    End Sub

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        If btnSave.Visible = True Then
            MenuItemImport.Enabled = True
            MenuItemExport.Enabled = True
        Else
            MenuItemImport.Enabled = False
            MenuItemExport.Enabled = False
        End If
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        funReset()
    End Sub

    Sub funReset()
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        txtName.Text = ""
        btnSave.Text = "Save"
        btnSave.Enabled = True
        txtRockCode.Value = ""
        txtCode.Value = ""
        lblBillToLocation.Text = ""
        lblRock_BinCode.Text = ""
        lblRockDesc.Text = ""
        txtLocation.Value = ""
        txtName.Text = ""
        btnDelete.Enabled = False
        lblRock_BinCode.Text = "Code"
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        funClose()
    End Sub

    Sub funClose()
        Me.Close()
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating

        Dim str As String = ""
        If clsCommon.CompairString(rbtnRock.IsChecked, True) = CompairStringResult.Equal Then
            str = "select count(*) from TSPL_Rack_MASTER where Code ='" + txtCode.Value + "' "
        Else
            str = "select count(*) from TSPL_Bin_MASTER where Bin_Code ='" + txtCode.Value + "' "
        End If

        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            If clsCommon.CompairString(rbtnRock.IsChecked, True) = CompairStringResult.Equal Then
                txtCode.Value = clsRackMaster.getFinder("", txtCode.Value, isButtonClicked)
            Else
                txtCode.Value = clsBinMaster.getFinder("", txtCode.Value, isButtonClicked)
            End If

            If txtCode.Value <> "" Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                funReset()
            End If
        End If


    End Sub

    Sub funFill()

    End Sub

    Private Sub txtCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub frmStateMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Private Sub MenuRackItemImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Description", "Location") Then
            Try
                clsCommon.ProgressBarShow()
                Dim ii As Integer = 0
                Try
                    For Each grow As GridViewRowInfo In gv.Rows
                        ii = ii + 1
                        Dim obj As New clsRackMaster()

                        obj.Description = clsCommon.myCstr(grow.Cells("Description").Value)
                        If clsCommon.myLen(obj.Description) <= 0 Then
                            Throw New Exception("Description can not be blank or incorrect.")
                        End If
                        obj.Location = clsCommon.myCstr(grow.Cells("Location").Value)
                        If clsCommon.myLen(obj.Location) <= 0 Then
                            Throw New Exception("Location can not be blank or incorrect.")
                        End If
                        clsRackMaster.SaveData(obj, isNewEntry, True)

                    Next
                Catch ex As Exception
                    Throw New Exception("At Row No" + clsCommon.myCstr(ii) + ex.Message)
                End Try
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub


    Private Sub MenuItemExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemExport.Click
        Dim str As String
        str = "select  Description,Location from TSPL_Rack_MASTER"
        transportSql.ExporttoExcel(str, Me)

    End Sub

    Private Sub rbtnBin_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnBin.ToggleStateChanged

        labelRock.Visible = True
        txtRockCode.Visible = True
        lblRockDesc.Visible = True
        funReset()
        lblRock_BinCode.Text = "Bin Code"
    End Sub

    Private Sub rbtnRock_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnRock.ToggleStateChanged

        labelRock.Visible = False
        txtRockCode.Visible = False
        lblRockDesc.Visible = False
        funReset()
        lblRock_BinCode.Text = "Rack Code"
    End Sub

    Private Sub txtBillToLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtLocation._MYValidating

        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = " Location_Type='Physical'  "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If

        txtLocation.Value = clsCommon.ShowSelectForm("BillToLocFNDD", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
        lblBillToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
        txtRockCode.Value = ""
        lblRockDesc.Text = ""
    End Sub
    Private Sub txtRack__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtRockCode._MYValidating
        Dim qry As String = "select Code as Code,Description as Name,Location from TSPL_Rack_MASTER "
        Dim WhrCls As String = ""
        If clsCommon.myLen(txtLocation.Value) > 0 Then
            WhrCls += "  Location in (" + txtLocation.Value + ")"
        End If
        txtRockCode.Value = clsCommon.ShowSelectForm("RackCode", qry, "Code", WhrCls, txtRockCode.Value, "Code", isButtonClicked)
        lblRockDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_Rack_MASTER where Code='" + txtRockCode.Value + "'"))

    End Sub

    Private Sub RadBinItemImport_Click(sender As Object, e As EventArgs) Handles RadMenuItem4.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Description", "Rack Code", "Location") Then
            Try
                clsCommon.ProgressBarShow()
                Dim ii As Integer = 0
                Try
                    For Each grow As GridViewRowInfo In gv.Rows
                        ii = ii + 1
                        Dim obj As New clsBinMaster()

                        obj.Description = clsCommon.myCstr(grow.Cells("Description").Value)
                        If clsCommon.myLen(obj.Description) <= 0 Then
                            Throw New Exception("Description can not be blank or incorrect.")
                        End If
                        obj.Rack_Code = clsCommon.myCstr(grow.Cells("Rack Code").Value)
                        If clsCommon.myLen(obj.Rack_Code) <= 0 Then
                            Throw New Exception("Rack Code can not be blank or incorrect.")
                        End If
                        Dim RackCode As String = clsDBFuncationality.getSingleValue("select Code from tspl_rack_master where Code='" & obj.Rack_Code & "'")
                        If clsCommon.myLen(RackCode) <= 0 Then
                            Throw New Exception("Rack Code can not be Defined.")
                        End If
                        obj.Location = clsCommon.myCstr(grow.Cells("Location").Value)
                        If clsCommon.myLen(obj.Location) <= 0 Then
                            Throw New Exception("Location can not be blank or incorrect.")
                        End If
                        clsBinMaster.SaveData(obj, isNewEntry, True)
                    Next
                Catch ex As Exception
                    Throw New Exception("At Row No" + clsCommon.myCstr(ii) + ex.Message)
                End Try
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub RadBinItemExport_Click(sender As Object, e As EventArgs) Handles RadMenuItem5.Click
        Dim str As String
        str = "select  Description,Rack_Code as [Rack Code],Location from TSPL_Bin_MASTER"
        transportSql.ExporttoExcel(str, Me)
    End Sub
End Class
