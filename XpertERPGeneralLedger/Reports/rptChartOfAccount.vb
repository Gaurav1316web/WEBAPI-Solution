Imports System.IO
Imports common
'====================================Created By Preeti Gupta Against Ticket No[BM00000008523]
Public Class RptChartOfAccount
    Inherits FrmMainTranScreen
    Dim dt As DataTable
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim tmpValLoad As Boolean = True
    Dim arrLoc As String = Nothing

   
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptChartOfAccount)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExp.Visible = MyBase.isExport
        btnGo.Visible = MyBase.isPrintFlag

    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv.Columns.Count - 1 Step ii + 1
                        gv.Columns(ii).IsVisible = False
                        gv.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Public Sub Load_Report()
        If txtFromDate.Value > txtToDate.Value Then
            common.clsCommon.MyMessageBoxShow("From date can not be greater then to Date")
            txtFromDate.Focus()
            Exit Sub
        End If
        Dim squery As String = " select TSPL_ACCOUNT_GROUPS.Account_Main_Group_Code as [Account Main Group Code],"
        squery += " TSPL_ACCOUNT_MAIN_GROUPS.Account_Main_Group_Desc as [Account Main Group Description], TSPL_ACCOUNT_MAIN_GROUPS.Group_Type as [Account Main Group Type],TSPL_ACCOUNT_SUB_GROUPS.Account_Group_Code As [Account Group Code],"
        squery += " TSPL_ACCOUNT_GROUPS.Account_Group_Desc as [Account Group Desc],TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Sub_Group_Code As [Account Sub Group Code],"
        squery += " TSPL_ACCOUNT_SUB_GROUPS.Account_Sub_Group_Desc AS [Account Sub Group Description],TSPL_GL_ACCOUNTS.GL_Main_Code as [GL Main Account Code],"
        squery += "  TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account_Desc AS [GL Main Account Description],TSPL_GL_ACCOUNTS.Account_Code as [Chart of Account Code] ,"
        squery += "   TSPL_GL_ACCOUNTS.Description as [Chart Of Account Description],(case when TSPL_GL_ACCOUNTS.status='Y' then 'Active' else 'In Active' end)  as [Status]  from TSPL_GL_ACCOUNTS "
        squery += " left outer join TSPL_ACCOUNT_MAIN_GL_ACCOUNT on TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account=TSPL_GL_ACCOUNTS.GL_Main_Code "
        squery += " left outer join TSPL_ACCOUNT_SUB_GROUPS on TSPL_ACCOUNT_SUB_GROUPS.Account_Sub_Group_Code=TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Sub_Group_Code"
        squery += " left outer join TSPL_ACCOUNT_GROUPS on TSPL_ACCOUNT_GROUPS.Account_Group_Code= TSPL_ACCOUNT_SUB_GROUPS.Account_Group_Code"
        squery += " left outer join  TSPL_ACCOUNT_MAIN_GROUPS on TSPL_ACCOUNT_MAIN_GROUPS.Account_Main_Group_Code=TSPL_ACCOUNT_GROUPS.Account_Main_Group_Code  where 2=2"

        If MultAccountMainGroup.arrValueMember IsNot Nothing AndAlso MultAccountMainGroup.arrValueMember.Count > 0 Then
            squery += " and TSPL_ACCOUNT_GROUPS.Account_Main_Group_Code in (" & clsCommon.GetMulcallString(MultAccountMainGroup.arrValueMember) & " )"
        End If
        If MultAccountGroup.arrValueMember IsNot Nothing AndAlso MultAccountGroup.arrValueMember.Count > 0 Then
            squery += " and TSPL_ACCOUNT_SUB_GROUPS.Account_Group_Code in (" & clsCommon.GetMulcallString(MultAccountGroup.arrValueMember) & " )"
        End If
        If MultAccountSubGroup.arrValueMember IsNot Nothing AndAlso MultAccountSubGroup.arrValueMember.Count > 0 Then
            squery += " and TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Sub_Group_Code in (" & clsCommon.GetMulcallString(MultAccountSubGroup.arrValueMember) & " )"
        End If
        If MultGLMainAccount.arrValueMember IsNot Nothing AndAlso MultGLMainAccount.arrValueMember.Count > 0 Then
            squery += " and TSPL_GL_ACCOUNTS.GL_Main_Code in (" & clsCommon.GetMulcallString(MultGLMainAccount.arrValueMember) & " )"
        End If
        If MultChartOfAccount.arrValueMember IsNot Nothing AndAlso MultChartOfAccount.arrValueMember.Count > 0 Then
            squery += " and TSPL_GL_ACCOUNTS.Account_Code in (" & clsCommon.GetMulcallString(MultChartOfAccount.arrValueMember) & " )"
        End If
        If ChkActive.IsChecked Then
            squery += "and TSPL_GL_ACCOUNTS.status='Y'"
        ElseIf ChkInActive.IsChecked Then
            squery += "and TSPL_GL_ACCOUNTS.status='N'"
        End If

       
        Dim dtgvreport As New DataTable
        dtgvreport = clsDBFuncationality.GetDataTable(squery)
        If dtgvreport IsNot Nothing And dtgvreport.Rows.Count > 0 Then
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dtgvreport
            gv.BestFitColumns()
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            RadPageView1.SelectedPage = RadPageViewPage2
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If

        ReStoreGridLayout()
    End Sub
    Sub reset()
        RadPageView1.SelectedPage = RadPageViewPage1
        ChkAll.IsChecked = True
        MultAccountMainGroup.arrValueMember = Nothing
        MultAccountGroup.arrValueMember = Nothing
        MultAccountSubGroup.arrValueMember = Nothing
        MultGLMainAccount.arrValueMember = Nothing
        MultChartOfAccount.arrValueMember = Nothing
        gv.DataSource = Nothing
    End Sub
    Private Sub MultAccountMainGroup__My_Click(sender As Object, e As EventArgs) Handles MultAccountMainGroup._My_Click
        Dim qry As String = "select Account_Main_Group_Code as Code ,Account_Main_Group_Desc as Name from TSPL_ACCOUNT_MAIN_GROUPS "
        MultAccountMainGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("MultAccountMainGroup", qry, "Code", "Name", MultAccountMainGroup.arrValueMember, MultAccountMainGroup.arrDispalyMember)
    End Sub

    Private Sub MultAccountGroup__My_Click(sender As Object, e As EventArgs) Handles MultAccountGroup._My_Click
        Dim qry As String = "select Account_Group_Code as Code ,Account_Group_Desc as Name from TSPL_ACCOUNT_GROUPS "
        MultAccountGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("MultAccountGroup", qry, "Code", "Name", MultAccountGroup.arrValueMember, MultAccountGroup.arrDispalyMember)
    End Sub

    Private Sub MultAccountSubGroup__My_Click(sender As Object, e As EventArgs) Handles MultAccountSubGroup._My_Click
        Dim qry As String = "select Account_Sub_Group_Code as Code ,Account_Sub_Group_Desc as Name from TSPL_ACCOUNT_SUB_GROUPS"
        MultAccountSubGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("MultAccountSubGroup", qry, "Code", "Name", MultAccountSubGroup.arrValueMember, MultAccountSubGroup.arrDispalyMember)
    End Sub

    Private Sub MultChartOfAccount__My_Click(sender As Object, e As EventArgs) Handles MultChartOfAccount._My_Click
        Dim qry As String = "select Account_Code as Code ,Description as Name  from TSPL_GL_ACCOUNTS"
        MultChartOfAccount.arrValueMember = clsCommon.ShowMultipleSelectForm("MultChartOfAccount", qry, "Code", "Name", MultChartOfAccount.arrValueMember, MultChartOfAccount.arrDispalyMember)
    End Sub

    Private Sub MultGLMainAccount__My_Click(sender As Object, e As EventArgs) Handles MultGLMainAccount._My_Click
        Dim qry As String = "select Main_GL_Account as Code,Main_GL_Account_Desc as Name from TSPL_ACCOUNT_MAIN_GL_ACCOUNT"
        MultGLMainAccount.arrValueMember = clsCommon.ShowMultipleSelectForm("MultGLMainAccount", qry, "Code", "Name", MultGLMainAccount.arrValueMember, MultGLMainAccount.arrDispalyMember)
    End Sub

    Private Sub RptChartOfAccount_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+N Go ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+R Referesh")
        RadPageView1.SelectedPage = RadPageViewPage1
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        reset()
    End Sub

    Private Sub RptChartOfAccount_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Load_Report()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()
        End If
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub
   
    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        reset()
    End Sub

    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptChartOfAccount & "'"))

                Dim strTemp As String = ""
                'arrHeader.Add("Salary Summary (" & txtFromPP.Value & " )")
                If Not MultAccountMainGroup.arrValueMember Is Nothing Then
                    arrHeader.Add("Account Main Group : " & clsCommon.GetMulcallStringWithComma(MultAccountMainGroup.arrValueMember))
                Else
                    arrHeader.Add("Account Main Group : All")
                End If
                If Not MultAccountGroup.arrValueMember Is Nothing Then
                    arrHeader.Add("Account Group : " & clsCommon.GetMulcallStringWithComma(MultAccountGroup.arrValueMember))
                Else
                    arrHeader.Add("Account Group : All")
                End If

                If Not MultAccountGroup.arrValueMember Is Nothing Then
                    arrHeader.Add("Account Sub Group : " & clsCommon.GetMulcallStringWithComma(MultAccountSubGroup.arrValueMember))
                Else
                    arrHeader.Add("Account Sub Group : All")
                End If

                If Not MultGLMainAccount.arrValueMember Is Nothing Then
                    arrHeader.Add("GL Main Account: " & clsCommon.GetMulcallStringWithComma(MultGLMainAccount.arrValueMember))
                Else
                    arrHeader.Add("GL Main Account : All")
                End If

                If Not MultChartOfAccount.arrValueMember Is Nothing Then
                    arrHeader.Add("Chart of Account: " & clsCommon.GetMulcallStringWithComma(MultChartOfAccount.arrValueMember))
                Else
                    arrHeader.Add("Chart of Account : All")
                End If

                If exporter = EnumExportTo.Excel Then
                    'Dim sfd As SaveFileDialog = New SaveFileDialog()
                    'Dim filePath As String
                    'sfd.FileName = Me.Text
                    'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                    'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    '    filePath = sfd.FileName
                    'Else
                    '    Exit Sub
                    'End If
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
                    'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                Else
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("Chart Of Account", gv, arrHeader, "Chart Of Account", PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.close()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        Load_Report()
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Export(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Export(EnumExportTo.PDF)
    End Sub
End Class
