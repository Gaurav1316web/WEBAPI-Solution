'--29/07/2013--form Add By- Pradeep Sharma ---------
'' Anubhooti(3-July-2014) Added Export Permission Against BM00000003016 ''''''''
'' Anubhooti(11-July-2014) Added Export (Clubed)Button BM00000003137 ''''''''

Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports Telerik.WinControls.UI.Data
Imports XpertERPEngine

Public Class frmEmployeeRegister
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Const ReportID As String = "EmployeeRegister"

#Region "Variable"
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
    Dim DT As DataTable
    Dim DT_Details As DataTable

#End Region
    Sub LoadData()
        Try
            PageSetupReport_ID = ReportID
            TemplateGridview = gv1
            DT = clsEmployeeMaster.GetEmployeeRegister(fndLocation.Value, CboStatus.SelectedValue, txtEmp.arrValueMember, TxtDesignation.Value, txtDepartment.Value)

            If DT Is Nothing OrElse DT.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            Else
                SetupMasterForAutoGenerateHierarchy()
                EnableDisable(False)
                RadPageView1.SelectedPage = RadPageViewPage2
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub frmEmployeeRegister_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        CboStatus.DataSource = GetEmpStatusDataTable()
        CboStatus.DisplayMember = "Name"
        CboStatus.ValueMember = "Code"
        CboStatus.SelectedIndex = 0
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmEmployeeRegister)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        ' Preeti Gupta Added Export Permission  ''''''''
        'btnExpoExl.Visible = MyBase.isExport
        'btnExpoPDF.Visible = MyBase.isExport
        btnExport.Visible = MyBase.isExport

    End Sub

    Private Function GetEmpStatusDataTable() As DataTable
        Dim DT As DataTable = New DataTable
        DT.Columns.Add("Code", GetType(String))
        DT.Columns.Add("Name", GetType(String))
        Dim DR As DataRow = DT.NewRow()

        DR = DT.NewRow()
        DR("Code") = "All"
        DR("Name") = "All"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Code") = "Active"
        DR("Name") = "Active"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Code") = "Inactive"
        DR("Name") = "Inactive"
        DT.Rows.Add(DR)
        DT.AcceptChanges()
        Return DT
    End Function

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        funClose()
    End Sub

    Sub funClose()
        Me.Close()
    End Sub


    Private Sub frmEmployeeRegister_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.C Then
            funClose()
        End If
    End Sub

    Private Sub RadMenuItemSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemSave.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If

            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next

                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub RadMenuItemDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemDelete.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub btnExpoExl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim arr As New List(Of String)()
        arr.Add(objCommonVar.CurrentCompanyName)
        arr.Add("Employee Register ")
        arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        'clsCommon.MyExportToExcel("Employee Register ", gv1, arr, "Employee Register ")
        clsCommon.MyExportToExcelGrid("Employee Register", gv1, arr, "Employee Register", False)

    End Sub

    Private Sub btnExpoPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim arr As New List(Of String)()
        arr.Add(objCommonVar.CurrentCompanyName)
        arr.Add("Employee Register")
        arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        clsCommon.MyExportToPDF("Employee Register", gv1, arr, "Employee Register", False)

    End Sub

#Region "grid operations"

    Private Sub SetupMasterForAutoGenerateHierarchy()
        Using Me.gv1.DeferRefresh()
            Me.gv1.DataSource = DT
            gv1.BestFitColumns()
            gv1.Columns("Sh/Ex Date").IsVisible = False
            gv1.Columns("Cash Sh/Ex").IsVisible = False
            gv1.Columns("Empty Ex").IsVisible = False
            gv1.Columns("Company Code").IsVisible = False
            gv1.Columns("GL Account").IsVisible = False
            ReStoreGridLayout()
            ' Me.gv1.AutoSize = GridViewAutoSizeColumnsMode.Fill
        End Using
    End Sub
#End Region

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        'Dim arr As New List(Of String)()
        'arr.Add(objCommonVar.CurrentCompanyName)
        'arr.Add("Employee Register ")
        'arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        ''clsCommon.MyExportToExcel("Employee Register ", gv1, arr, "Employee Register ")
        'If gv1.Rows.Count <= 0 Then
        '    gv1.Focus()
        '    clsCommon.MyMessageBoxShow("Data not found.")
        'Else
        '    clsCommon.MyExportToExcelGrid("Employee Register", gv1, arr, "Employee Register", False)
        'End If
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub btnPDF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        'Dim arr As New List(Of String)()
        'arr.Add(objCommonVar.CurrentCompanyName)
        'arr.Add("Employee Register")
        'arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        'clsCommon.MyExportToPDF("Employee Register", gv1, arr, "Employee Register", False)
        ExportGrid(EnumExportTo.PDF)
    End Sub
    Sub LoadReportPFWithdrawn()
        Dim Qry As String = "select distinct TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE , Emp_Name ,FATHERS_NAME,PF_NO,ESI_NO ,convert(varchar,Joining_date,103)as Joining_date ,convert(varchar,RELIEVING_DATE,103)as RELIEVING_DATE ,Comp_Name ,TSPL_COMPANY_MASTER.Add1 ,TSPL_COMPANY_MASTER.Add2,City_Code      from TSPL_GENERATE_SALARY_PAYHEADS inner join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY_PAYHEADS .SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE INNER JOIN TSPL_PAYPERIOD_MASTER  ON TSPL_GENERATE_SALARY.PAY_PERIOD_CODE=TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE  INNER JOIN TSPL_PAYHEAD_MASTER  ON TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE=TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE    INNER JOIN TSPL_EMPLOYEE_MASTER  ON TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_EMPLOYEE_MASTER.EMP_CODE  left join TSPL_COMPANY_MASTER on TSPL_EMPLOYEE_MASTER.Comp_Code =  TSPL_COMPANY_MASTER.Comp_Code left join TSPL_DEPARTMENT_MASTER on TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE = TSPL_EMPLOYEE_MASTER.DEPARTMENT_CODE"
        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(Qry)
        Dim frmcrystal As New frmCrystalReportViewer()
        frmcrystal.funreport(CrystalReportFolder.HRPayroll, dtgv, "crptPFWithdrawnFormStatement", "PF Withdrawn Form Statement")
    End Sub
    Private Sub rmPFWithdrawnForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmPFWithdrawnForm.Click
        LoadReportPFWithdrawn()

    End Sub
    Sub LoadReportPFEligibilityRegister()
        Dim Qry As String = "select distinct Emp_Name ,FATHERS_NAME,DEPARTMENT_NAME ,convert(varchar,Joining_date,103)as Joining_date ,convert(varchar,Birth_date,103)as Birth_date ,convert(varchar,RELIEVING_DATE,103)as RELIEVING_DATE ,PF_NO,Comp_Name ,TSPL_COMPANY_MASTER.Add1 ,TSPL_COMPANY_MASTER.Add2,City_Code      from TSPL_GENERATE_SALARY_PAYHEADS inner join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY_PAYHEADS .SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE INNER JOIN TSPL_PAYPERIOD_MASTER  ON TSPL_GENERATE_SALARY.PAY_PERIOD_CODE=TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE  INNER JOIN TSPL_PAYHEAD_MASTER  ON TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE=TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE    INNER JOIN TSPL_EMPLOYEE_MASTER  ON TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_EMPLOYEE_MASTER.EMP_CODE  left join TSPL_COMPANY_MASTER on TSPL_EMPLOYEE_MASTER.Comp_Code =  TSPL_COMPANY_MASTER.Comp_Code left join TSPL_DEPARTMENT_MASTER on TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE = TSPL_EMPLOYEE_MASTER.DEPARTMENT_CODE   "
        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(Qry)
        Dim frmcrystal As New frmCrystalReportViewer()
        frmcrystal.funreport(CrystalReportFolder.HRPayroll, dtgv, "crptPFEligibilityRegister", "Eligibility Register For PF")
    End Sub
    Private Sub rmPFEligibiltyRegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmPFEligibiltyRegister.Click
        LoadReportPFEligibilityRegister()
    End Sub

    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            'arrHeader.Add("Date :" + clsCommon.GETSERVERDATE() + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmEmployeeRegister & "'"))

            'If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
            '    arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocationMult.arrDispalyMember))
            'End If
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
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
                'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)
            Else
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Employee Register", gv1, arrHeader, "Employee Register", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            EnableDisable(True)
            gv1.DataSource = Nothing
            RadPageView1.SelectedPage = RadPageViewPage1
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub EnableDisable(ByVal val As Boolean)
        fndLocation.Enabled = val
        txtEmp.Enabled = val
        CboStatus.Enabled = val
        TxtDesignation.Enabled = val
        txtDepartment.Enabled = val
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadData()
    End Sub

    Private Sub txtEmp__My_Click(sender As Object, e As EventArgs) Handles txtEmp._My_Click
        Try
            Dim qry As String
            Dim StrWhere As String = ""
            If clsCommon.CompairString(CboStatus.SelectedValue, "All") = CompairStringResult.Equal OrElse clsCommon.CompairString(CboStatus.SelectedValue, "") = CompairStringResult.Equal Then
                StrWhere = " 1=1 "
            ElseIf clsCommon.CompairString(CboStatus.SelectedValue, "Active") = CompairStringResult.Equal Then
                StrWhere = " TSPL_EMPLOYEE_MASTER.Emp_Status ='Active' "
            ElseIf clsCommon.CompairString(CboStatus.SelectedValue, "InActive") = CompairStringResult.Equal Then
                StrWhere = " TSPL_EMPLOYEE_MASTER.Emp_Status ='InActive' "
            End If

            qry = "Select TSPL_EMPLOYEE_MASTER.EMP_CODE As [Employee Code] ,TSPL_EMPLOYEE_MASTER.Emp_Name As [Employee Name] from TSPL_EMPLOYEE_MASTER where " + StrWhere + ""

            txtEmp.arrValueMember = clsCommon.ShowMultipleSelectForm("ER@WH1", qry, "Employee Code", "Employee Name", txtEmp.arrValueMember, txtEmp.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLocation._MYValidating
        fndLocation.Value = clsLocation.getFinder("Location_Type='Physical'", Me.fndLocation.Value, isButtonClicked)

        If clsCommon.myLen(fndLocation.Value) > 0 Then
            lblLocationName.Text = clsLocation.GetName(fndLocation.Value, Nothing)
        Else
            lblLocationName.Text = ""
        End If

    End Sub

    Private Sub CboStatus_SelectedIndexChanged(sender As Object, e As PositionChangedEventArgs) Handles CboStatus.SelectedIndexChanged
        Try
            txtEmp.arrValueMember = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtDesignation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtDesignation._MYValidating
        Try
            Dim qry As String = " select designation_id As Code,designation_desc  as [Description]from TSPL_Designation_MASTER "
            TxtDesignation.Value = clsCommon.ShowSelectForm("frmERDes", qry, "Code", "", TxtDesignation.Value, "", isButtonClicked)

            If clsCommon.myLen(TxtDesignation.Value) > 0 Then
                lblDesignation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select designation_desc from TSPL_Designation_MASTER where designation_id='" + TxtDesignation.Value + "'"))
            Else
                lblDesignation.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub txtDepartment__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDepartment._MYValidating
        Try
            Dim qry As String = "select DEPARTMENT_CODE AS Code, DEPARTMENT_NAME AS Name, DESCRIPTION AS Description from TSPL_DEPARTMENT_MASTER"
            txtDepartment.Value = clsCommon.ShowSelectForm("frmERDep", qry, "Code", "", txtDepartment.Value, "DEPARTMENT_CODE", isButtonClicked)

            If clsCommon.myLen(txtDepartment.Value) > 0 Then
                lblDepartment.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select DEPARTMENT_NAME from TSPL_DEPARTMENT_MASTER where DEPARTMENT_CODE='" + txtDepartment.Value + "'"))
            Else
                lblDepartment.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
End Class
