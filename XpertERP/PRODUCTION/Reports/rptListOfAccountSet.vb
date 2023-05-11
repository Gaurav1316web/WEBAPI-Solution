Imports common
'''' <summary>
'''' ''''''BM00000000506'''''''''''''''''''''''''''''''''''''
'''' </summary>
'''' <remarks></remarks>
Public Class RptListOfAccountSet
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Functions"
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.LACCt)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnprint.Visible = MyBase.isPrintFlag
    End Sub
    Sub LoadToolType()
        Dim strquery As String = "select ACCOUNT_SET_CODE as Code,  DESCRIPTION as 'Description' from TSPL_MF_ACCOUNTSETS  where 2=2 "
        cbgAccount.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgAccount.ValueMember = "Code"
        cbgAccount.DisplayMember = "Description"
    End Sub
    Sub Print()

        Dim qry As String = " select ACCOUNT_SET_CODE as Code ,TSPL_MF_ACCOUNTSETS.DESCRIPTION as Description,WIP_CATEGORY as [WIP Category],a.Description as [WIPCategory description]    ,GL_WIP as [WIP],b.Description as [WIP Description],GL_SETUP_LABOR as [SetupLabour],c.Description as [SetupLabour Desc] ,GL_RUN_LABOR as [RunLabour] ,d.Description as [RunLabour Desc] ,GL_SUBCONTRACT as [Subcontract],e.Description as [Subcontract Desc] ,GL_OVERHEAD as [Overhead],f.Description as [Overhead Desc] ,GL_MATERIAL_VARIANCE as [Material Variance],g.Description as [Material Variance Desc] ,GL_PRODUCTION_VARIANCE as [Production],h.Description as [Production Desc] from TSPL_MF_ACCOUNTSETS"
        qry += " left outer join TSPL_GL_ACCOUNTS a on TSPL_MF_ACCOUNTSETS.WIP_CATEGORY =a.Account_Code "
        qry += " left outer join TSPL_GL_ACCOUNTS b on TSPL_MF_ACCOUNTSETS.GL_WIP =b.Account_Code"
        qry += " left outer join TSPL_GL_ACCOUNTS c on TSPL_MF_ACCOUNTSETS.GL_SETUP_LABOR =c.Account_Code"
        qry += " left outer join TSPL_GL_ACCOUNTS d on TSPL_MF_ACCOUNTSETS.GL_RUN_LABOR =d.Account_Code "
        qry += " left outer join TSPL_GL_ACCOUNTS e on TSPL_MF_ACCOUNTSETS.GL_SUBCONTRACT =e.Account_Code "
        qry += " left outer join TSPL_GL_ACCOUNTS f on TSPL_MF_ACCOUNTSETS.GL_OVERHEAD =f.Account_Code"
        qry += " left outer join TSPL_GL_ACCOUNTS g on TSPL_MF_ACCOUNTSETS.GL_MATERIAL_VARIANCE =g.Account_Code "
        qry += " left outer join TSPL_GL_ACCOUNTS h on TSPL_MF_ACCOUNTSETS.GL_PRODUCTION_VARIANCE =h.Account_Code "

        qry += " where 2=2"
       

        If fndWIp.Value <> "" Then
            qry += " and WIP_CATEGORY='" + fndWIp.Value + "'"
        End If
        If chkAccountSetSelect.IsChecked AndAlso cbgAccount.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast one AccountSet")
            Return
        ElseIf chkAccountSetSelect.IsChecked AndAlso cbgAccount.CheckedValue.Count > 0 Then
            qry += " and  ACCOUNT_SET_CODE in (" + clsCommon.GetMulcallString(cbgAccount.CheckedValue) + ")"
        End If
        qry += " and convert(date,TSPL_MF_ACCOUNTSETS.created_date,103) >= '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and  "
        qry += "convert(date,TSPL_MF_ACCOUNTSETS.created_date,103) <= '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'   "

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        gv.DataSource = Nothing
        gv.Columns.Clear()
        gv.Rows.Clear()
        gv.GroupDescriptors.Clear()
        gv.MasterTemplate.SummaryRowsBottom.Clear()
        gv.EnableFiltering = True

        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
            Exit Sub
        Else
            gv.DataSource = dt
            For ii As Integer = 0 To gv.Columns.Count - 1
                gv.Columns(ii).ReadOnly = True
                gv.Columns(ii).Width = 100
                gv.Columns(gv.Columns.Count - 1).Width = 500
            Next
            gv.EnableGrouping = False
        End If

        gv.MasterTemplate.AllowAddNewRow = False
        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub
    Sub Reset()
        LoadToolType()
        fndWIp.Value = ""
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
       
        gv.DataSource = Nothing
    End Sub
  
#End Region
#Region "Events"
    Private Sub fndWIp__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndWIp._MYValidating
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts "
        fndWIp.Value = clsCommon.ShowSelectForm("REd6", qry, "AccountCode", "", fndWIp.Value, "", isButtonClicked)
    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        Print()
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


    Private Sub chkAccountSetAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkAccountSetAll.ToggleStateChanged
        cbgAccount.Enabled = chkAccountSetSelect.IsChecked
    End Sub

    Private Sub RptListOfAccountSet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Reset()
        ButtonToolTip.SetToolTip(btnprint, "Press Alt+S for Print ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Reset ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        SetUserMgmtNew()
    End Sub

    Private Sub RptListOfAccountSet_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            Print()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub
#End Region
End Class
