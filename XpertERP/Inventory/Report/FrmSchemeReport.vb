'-05/10/2012--Updation By--Pankaj Kumar--Added Two new columns[StartDate, Enddate] And AND Added Two radio Buttons[All, Active]----Fwd By--Ranjana Mam
Imports common
Public Class FrmSchemeReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.SchemeReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P Print the Report")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+R Reset the Window")
        btnPrint.Visible = MyBase.isPrintFlag
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub FrmSchemeReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        reset()
        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        SetUserMgmtNew()

    End Sub
    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "SCH-RPT"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
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
    '        If strTemp(1) = "0" Then 'Grant modify access

    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access

    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception
    '        myMessages.myExceptions(er)
    '    End Try
    'End Function


    Sub LoadItem()
        Dim qry As String = " select Item_Code as [Item Code],Item_Desc as [Description] from TSPL_ITEM_MASTER where Item_Type in('f','p')"
        cgvmainitem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cgvmainitem.ValueMember = "Item Code"
        cgvmainitem.DisplayMember = "Description"

        cgvschemeitem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cgvschemeitem.ValueMember = "Item Code"
        cgvschemeitem.DisplayMember = "Description"
    End Sub

    Sub Loadcategory()
        Dim qry As String = " select  CUST_CATEGORY_CODE as [Category Code],CUST_CATEGORY_DESC as [Description] from TSPL_CUSTOMER_CATEGORY_MASTER"
        cgvCustCategory.DataSource = clsDBFuncationality.GetDataTable(qry)
        cgvCustCategory.ValueMember = "Category Code"
        cgvCustCategory.DisplayMember = "Description"
    End Sub

    Public Sub reset()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        LoadItem()
        Loadcategory()
        chkMainall.IsChecked = True
        chkschemeall.IsChecked = True
        chkcustcateall.IsChecked = True
        chkActive.IsChecked = True
    End Sub
    Private Sub chkMainall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkMainall.ToggleStateChanged
        cgvmainitem.Enabled = Not chkMainall.IsChecked
    End Sub

    Private Sub chkschemeall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkschemeall.ToggleStateChanged
        cgvschemeitem.Enabled = Not chkschemeall.IsChecked
    End Sub

    Private Sub chkcustcateall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkcustcateall.ToggleStateChanged
        cgvCustCategory.Enabled = Not chkcustcateall.IsChecked
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        PrintData()
    End Sub
    Sub PrintData()
        Try
            If chkmainselect.IsChecked AndAlso cgvmainitem.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select at least one Main Item")
                Return
            End If
            If chkcateselect.IsChecked AndAlso cgvCustCategory.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select at least one Customer Category")
                Return
            End If
            If chkschemeselect.IsChecked AndAlso cgvschemeitem.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select at least one Scheme ")
                Return
            End If
            Dim IsActive As String = ""
            If chkActive.IsChecked Then
                IsActive = " AND ISNULL(TSPL_SCHEME_MASTER.End_Date, '') = '' "
            End If
            Dim qry As String
            qry = "   SELECT   '" + txtFromDate.Value + "' as fDate ,'" + txtToDate.Value + "' as stdate , TSPL_SCHEME_MASTER.Main_Item_Code,TSPL_SCHEME_MASTER.Main_Item_UOM, TSPL_SCHEME_MASTER.Main_Item_Qty,TSPL_SCHEME_MASTER.Cust_Cate,TSPL_SCHEME_MASTER.MRP,TSPL_SCHEME_MASTER.Cust_Cate_desc, TSPL_SCHEME_DETAILS.Scheme_Item_Code, " & _
                      " TSPL_SCHEME_DETAILS.Qty, TSPL_SCHEME_DETAILS.UOM, ISNULL(CONVERT(VARCHAR,TSPL_SCHEME_MASTER.Start_Date, 103), '') as SDate, ISNULL(CONVERT(VARCHAR,TSPL_SCHEME_MASTER.End_Date, 103), '') as EDate,  TSPL_COMPANY_MASTER.Comp_Name, " & _
           " TSPL_COMPANY_MASTER.Logo_Img, TSPL_COMPANY_MASTER.Logo_Img2, TSPL_COMPANY_MASTER.Add1, TSPL_COMPANY_MASTER.Add2 " & _
           "  FROM   TSPL_SCHEME_MASTER LEFT OUTER JOIN " & _
           "    TSPL_SCHEME_DETAILS ON TSPL_SCHEME_MASTER.Scheme_Code = TSPL_SCHEME_DETAILS.Scheme_Code LEFT OUTER JOIN " & _
           "       TSPL_COMPANY_MASTER ON TSPL_SCHEME_MASTER.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code  where  2=2 " + IsActive + " " & _
           "  and  convert(date,TSPL_SCHEME_MASTER.Start_Date,103) >= convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_SCHEME_MASTER.Start_Date,103) <= convert(date,'" + txtToDate.Value + "',103) "


            If chkmainselect.IsChecked Then
                qry += " and TSPL_SCHEME_MASTER.Main_Item_Code in  (" + clsCommon.GetMulcallString(cgvmainitem.CheckedValue) + ")  "
            End If
            If chkschemeselect.IsChecked Then
                qry += " and TSPL_SCHEME_DETAILS.Scheme_Item_Code in  (" + clsCommon.GetMulcallString(cgvschemeitem.CheckedValue) + ")  "
            End If
            If chkcateselect.IsChecked Then
                qry += " and TSPL_SCHEME_MASTER.Cust_Cate in  (" + clsCommon.GetMulcallString(cgvCustCategory.CheckedValue) + ")  "
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data found to Print")
            Else
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, "crptSchemeReport", "Scheme Report")
                frmCRV = Nothing
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub FrmSchemeReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Control And e.KeyCode = Keys.P Then
            PrintData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            reset()
        End If
    End Sub
End Class
