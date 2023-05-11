Imports common

Public Class FrmCashDiscountReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()



    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.mbtnCashDiscountReport)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        '      btnSave.Visible = MyBase.isModifyFlag
        '       btnAuth.Visible = MyBase.isPostFlag
        '        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmCashDiscountReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            funPrint()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            reset()
        End If
    End Sub


    Private Sub FrmCashDiscountReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        SetUserMgmtNew()
        dtpstart.Value = clsCommon.GETSERVERDATE()
        dtpend.Value = clsCommon.GETSERVERDATE()
        RadioBtnSummary.IsChecked = True
        chkSelectAll.IsChecked = True
        LoadRouteType()
        'If Not clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(btnprint, "Press Alt+P for Print ")





    End Sub

    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "CSH-DIS-RPT"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete & "," & enuUserRights.enuAuthorised
    '        strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '        strTemp = Split(strRights, ",")
    '        If strTemp(0) = "0" Then
    '            MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    '            funSetUserAccess = False
    '            blnRead = False
    '            Me.Close()
    '            Exit Function
    '        Else
    '            blnRead = True
    '        End If
    '        funSetUserAccess = True
    '    Catch er As Exception

    '    End Try
    'End Function
    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        funPrint()
    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        reset()
    End Sub

    Public Sub reset()
        dtpstart.Value = clsCommon.GETSERVERDATE()
        dtpend.Value = clsCommon.GETSERVERDATE()
        chkSelectAll.IsChecked = True
        RadioBtnSummary.IsChecked = True
    End Sub

    Public Sub funPrint()

        If (dtpstart.Value > dtpend.Value) Then
            common.clsCommon.MyMessageBoxShow("'Start Date' Cann't be more than 'End date'")
        Else
            Try
                If chkSelect.IsChecked = True AndAlso cbgRoute.CheckedValue.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select at least one route or select ALL")
                    Return
                End If
                Dim qry As String
                If RadioBtnDetail.IsChecked Then
                    qry = " Select TSPL_COMPANY_MASTER.Comp_Name as [Company Name], TSPL_COMPANY_MASTER.Add1 as [Address1], " & _
    " TSPL_COMPANY_MASTER.Add2 as [Address2], TSPL_COMPANY_MASTER.Add3 as [Address3], TSPL_COMPANY_MASTER.Logo_Img as [Logo1], " & _
    " TSPL_COMPANY_MASTER.Logo_Img2 as [Logo2], TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date AS [Invoice Date], TSPL_SALE_INVOICE_HEAD.Vehicle_No AS [Vehicle No], " & _
    " TSPL_SALE_INVOICE_HEAD.Route_No AS [Route No], TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No AS [Invoice No], TSPL_SALE_INVOICE_DETAIL.Item_Code AS [Item Code], " & _
    " TSPL_SALE_INVOICE_DETAIL.Invoice_Qty AS [Quantity], TSPL_SALE_INVOICE_DETAIL.Scheme_Code_Cash AS [Scheme Code], " & _
    " TSPL_SALE_INVOICE_DETAIL.Disc_Amt AS [Amount], TSPL_SALE_INVOICE_HEAD.Comp_Code as [CompCode], TSPL_SALE_INVOICE_HEAD.Remarks as [Remarks], " & _
    " CONVERT(date, '" & dtpstart.Value & "', 103) AS [startdate], CONVERT(date, '" & dtpend.Value & "', 103) AS [enddate], '" + clsCommon.GETSERVERDATE + "' as [On Dated] " & _
    " FROM TSPL_SALE_INVOICE_DETAIL INNER JOIN TSPL_SALE_INVOICE_HEAD ON TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No = TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No " & _
    " Left OUTER JOIN TSPL_COMPANY_MASTER on TSPL_SALE_INVOICE_HEAD.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code " & _
    " where convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & dtpstart.Value & "',103) " & _
    " AND convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & dtpend.Value & "',103)"

                    If chkSelect.IsChecked = True AndAlso cbgRoute.CheckedValue.Count > 0 Then
                        qry += " AND TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ")"
                    End If
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        common.clsCommon.MyMessageBoxShow("No Record Found")
                    Else
                        'dt = clsDBFuncationality.GetDataTable(qry)
                        frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dt, "crptCashDiscountDetail", "Cash Discount Detail")
                    End If

                ElseIf RadioBtnSummary.IsChecked = True Then
                    qry = " SELECT [StartDate], [EndDate], '" + clsCommon.GETSERVERDATE + "' as [On Dated], TSPL_COMPANY_MASTER.Comp_Name as [Company Name], TSPL_COMPANY_MASTER.Add1 as [Address1], " & _
    " TSPL_COMPANY_MASTER.Add2 as [Address2], TSPL_COMPANY_MASTER.Add3 as [Address3], TSPL_COMPANY_MASTER.Logo_Img as [Logo1], " & _
    " TSPL_COMPANY_MASTER.Logo_Img2 as [Logo2], [Invoice Date], Quantity, Amount, [Scheme Code]  FROM " & _
    " (Select TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date AS [Invoice Date], MAX(TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No) AS [Invoice No], " & _
    " MAX(TSPL_SALE_INVOICE_DETAIL.Item_Code) AS [Item Code], SUM(TSPL_SALE_INVOICE_DETAIL.Invoice_Qty) AS [Quantity], " & _
    " MAX(TSPL_SALE_INVOICE_DETAIL.Scheme_Code_Cash) AS [Scheme Code], SUM(TSPL_SALE_INVOICE_DETAIL.Disc_Amt) AS [Amount], " & _
    " MAX(TSPL_SALE_INVOICE_HEAD.Comp_Code) as [CompCode], CONVERT(date, '" & dtpstart.Value & "', 103) AS [startdate], " & _
    " CONVERT(date, '" & dtpend.Value & "', 103) AS [enddate] " & _
    " FROM TSPL_SALE_INVOICE_DETAIL INNER JOIN TSPL_SALE_INVOICE_HEAD ON TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No = TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No " & _
    " where convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & dtpstart.Value & "',103) " & _
    " AND convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & dtpend.Value & "',103) " & _
    " GROUP BY TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date)as XXX " & _
    " Left OUTER JOIN TSPL_COMPANY_MASTER on XXX.CompCode=TSPL_COMPANY_MASTER.Comp_Code "

                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        common.clsCommon.MyMessageBoxShow("No Record Found")
                    Else
                        dt = clsDBFuncationality.GetDataTable(qry)

                        frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dt, "crptCashDiscountSummary", "Cash Discount Summary")
                    End If
                End If
            Catch ex As Exception
                common.clsCommon.MyMessageBoxShow(ex.Message)
            End Try
        End If
    End Sub

    Sub LoadRouteType()
        ''Dim qry As String = "select distinct Item_Code as [Item Code],Scheme_Applicable as [Scheme Applicable] from TSPL_SALE_INVOICE_DETAIL"
        Dim qry As String = "select  TSPL_ROUTE_MASTER.Route_No as [Route No], TSPL_ROUTE_MASTER.Route_Desc as [Route Description] from TSPL_ROUTE_MASTER"
        cbgRoute.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgRoute.ValueMember = "Route No"
        cbgRoute.DisplayMember = "Route Description"
    End Sub


    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    
    Private Sub chkSelectAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkSelectAll.ToggleStateChanged
        cbgRoute.Enabled = False
    End Sub

    Private Sub chkSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkSelect.ToggleStateChanged
        cbgRoute.Enabled = True
    End Sub
End Class
