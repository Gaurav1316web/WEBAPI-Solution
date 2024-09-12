Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports Telerik.WinControls

'================Created By Preeti Gupta-==============
''changes against[BM00000008156]
Public Class RptBankTransferDetail
#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
    Dim Print As Boolean = True
    Dim btnReferesh As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.RptBankTransferDetail)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag
    End Sub



    Sub PrintData()
        Try
            If clsCommon.myLen(txtpayPeriod.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please Select Pay Period.", Me.Text)
                Return
            End If
            Dim LocAddress As String = ""

            Dim LocationFirstTime As Integer = 0
            If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count = 1 Then
                LocationFirstTime += 1
                LocAddress = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Add1 + ' ' + Add2 + ' ' + Add3 + ' ' + add4 As [Address] FROM TSPL_LOCATION_MASTER WHERE Location_Code in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ")"))
            Else
                LocAddress = objCommonVar.CurrentCompanyName
            End If
            Dim CompCode As String = clsDBFuncationality.getSingleValue("select TSPL_COMPANY_MASTER.Comp_Code from tspl_company_master where Comp_Code='" + objCommonVar.CurrentCompanyCode + "' ")

            Dim CompName As String = clsDBFuncationality.getSingleValue("select TSPL_COMPANY_MASTER.Comp_Name from tspl_company_master where Comp_Code='" + objCommonVar.CurrentCompanyCode + "' ")
            Dim CompanyAdress As String = clsDBFuncationality.getSingleValue(" select  TSPL_COMPANY_MASTER.Add1+Case When ISNULL(TSPL_COMPANY_MASTER.Add2,'')='' Then '' else ', '+TSPL_COMPANY_MASTER.Add2+ Case When ISNULL(TSPL_COMPANY_MASTER.Add3,'')='' Then '' Else ', '+TSPL_COMPANY_MASTER.Add3+ Case When ISNULL(TSPL_COMPANY_MASTER.City_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_COMPANY_MASTER.City_Code) End End End as Comp_Address from tspl_company_master where Comp_Code='" + objCommonVar.CurrentCompanyCode + "' ")

            Dim Qry As String = ""
            Qry = ""
            Qry += " select max(TSPL_GENERATE_SALARY.DEVISION_CODE) as DEVISION_CODE ,max(TSPL_DEVISION_MASTER.DEVISION_NAME) as DEVISION_NAME, max('" & CompCode & "') as Comp_Code,max('" & LocAddress & "') as Comp_Name,max('" + CompanyAdress + "') as Comp_Address, max(TSPL_EMPLOYEE_MASTER.Bank_Branch) as IFSC_Code, max(TSPL_GENERATE_SALARY.PAY_PERIOD_CODE) as pay_period ,TSPL_GENERATE_SALARY.LOCATION_CODE Loc_Code,max(TSPL_LOCATION_MASTER.Location_Desc) as Loc_desc,sum( case when TSPL_PAYHEAD_MASTER.isearning=1 then  TSPL_GENERATE_SALARY_PAYHEADS.ACTUAL_AMOUNT else -TSPL_GENERATE_SALARY_PAYHEADS.ACTUAL_AMOUNT end  )as Net_Payment,max(TSPL_EMPLOYEE_MASTER.EMP_CODE) as EMP_CODE,max(tSPL_EMPLOYEE_MASTER.Emp_Name) as Emp_Name ,max(TSPL_EMPLOYEE_MASTER.BANK_ACC_NO )as Bank_Account,max(TSPL_EMPLOYEE_MASTER.Bank_Branch) as IFSC_Code,max(TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then '' else ', '+TSPL_LOCATION_MASTER.Add2+  Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_STATE_MASTER.State_Name ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_STATE_MASTER.State_Name) End End End) as Loc_Add ,max(TSPL_EMPLOYEE_MASTER.Bank_Name)as Bank_Name,max(TSPL_BANK_MASTER.DESCRIPTION)COMPANY_BANK  from TSPL_GENERATE_SALARY_PAYHEADS"
            Qry += " left outer join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE =TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE "
            Qry += " left join TSPL_PAYHEAD_MASTER on TSPL_PAYHEAD_MASTER.Pay_HEAD_Code=TSPL_GENERATE_SALARY_PAYHEADS.Pay_HEAD_Code"
            Qry += " left outer join TSPL_PAYPERIOD_MASTER on TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE =TSPL_GENERATE_SALARY.PAY_PERIOD_CODE "
            Qry += " left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE "
            Qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code =TSPL_GENERATE_SALARY.LOCATION_CODE "
            Qry += " left join TSPL_STATE_MASTER on TSPL_STATE_MASTER.State_Code=TSPL_LOCATION_MASTER.State"
            Qry += " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER  .Comp_Code =TSPL_LOCATION_MASTER.Comp_code "
            Qry += "  left join TSPL_DESIGNATION_MASTER on TSPL_DESIGNATION_MASTER .Designation_id =TSPL_EMPLOYEE_MASTER.Designation"
            ' Qry += " left join TSPL_BANK_MASTER on TSPL_BANK_MASTER .BANK_CODE =TSPL_EMPLOYEE_MASTER.BANK_CODE "
            Qry += " left outer join TSPL_DEVISION_MASTER   on TSPL_DEVISION_MASTER .DEVISION_CODE  =TSPL_GENERATE_SALARY.DEVISION_CODE"
            Qry += " left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_EMPLOYEE_MASTER.BANK_CODE"
            Qry += " WHERE   TSPL_GENERATE_SALARY.PAY_PERIOD_CODE = '" + txtpayPeriod.Value + "'"

            If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
                Qry += " and TSPL_LOCATION_MASTER.LOCATION_CODE  in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ") "
            End If
            If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
                Qry += " and TSPL_GENERATE_SALARY.Devision_Code  in (" + clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) + ") "
            End If

            If txtBankMult.arrValueMember IsNot Nothing AndAlso txtBankMult.arrValueMember.Count > 0 Then
                Qry += " and TSPL_EMPLOYEE_MASTER.Bank_Code  in (" + clsCommon.GetMulcallString(txtBankMult.arrValueMember) + ") "
            End If

            Qry += " group by TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE,TSPL_GENERATE_SALARY.Location_Code"


            Dim dtFinal As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dtFinal IsNot Nothing And dtFinal.Rows.Count > 0 Then
                gv1.DataSource = Nothing
                gv1.Rows.Clear()
                gv1.Columns.Clear()
                gv1.DataSource = dtFinal
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                FormatGrid()
                If btnReferesh = False Then
                    If dtFinal.Rows.Count <= 0 Then
                        common.clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
                    Else
                        Dim frmcrystal As New frmCrystalReportViewer()
                        frmcrystal.funreport(CrystalReportFolder.HRPayroll, dtFinal, "rptPayrollBankTransferDetail", "Bank Transfer Detail")
                    End If
                End If

                RadPageView1.SelectedPage = RadPageViewPage2
                'EnableDisableControl(False)
            Else
                'tmpValLoad = False
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If
            ReStoreGridLayout()

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

#End Region
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
            clsCommon.MyMessageBoxShow(Me, err.Message, Me.Text)
        End Try
    End Sub
    Sub FormatGrid()
        gv1.TableElement.TableHeaderHeight = 25
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = False

        Next
        gv1.Columns("DEVISION_CODE").IsVisible = True
        gv1.Columns("DEVISION_CODE").Width = 100
        gv1.Columns("DEVISION_CODE").HeaderText = "Division Code"

        gv1.Columns("DEVISION_NAME").IsVisible = True
        gv1.Columns("DEVISION_NAME").Width = 100
        gv1.Columns("DEVISION_NAME").HeaderText = "Division Name"

        gv1.Columns("IFSC_Code").IsVisible = True
        gv1.Columns("IFSC_Code").Width = 100
        gv1.Columns("IFSC_Code").HeaderText = "IFSC Code"

        gv1.Columns("Loc_Code").IsVisible = True
        gv1.Columns("Loc_Code").Width = 100
        gv1.Columns("Loc_Code").HeaderText = "Location Code"

        gv1.Columns("Loc_desc").IsVisible = True
        gv1.Columns("Loc_desc").Width = 100
        gv1.Columns("Loc_desc").HeaderText = "Location Name"

        gv1.Columns("Comp_Code").IsVisible = False
        gv1.Columns("Comp_Code").Width = 100
        gv1.Columns("Comp_Code").HeaderText = "Comp Code"

        gv1.Columns("Comp_Name").IsVisible = False
        gv1.Columns("Comp_Name").Width = 100
        gv1.Columns("Comp_Name").HeaderText = "Comp Name"

        gv1.Columns("Comp_Address").IsVisible = False
        gv1.Columns("Comp_Address").Width = 100
        gv1.Columns("Comp_Address").HeaderText = "Comp Address"

        gv1.Columns("pay_period").IsVisible = False
        gv1.Columns("pay_period").Width = 100
        gv1.Columns("pay_period").HeaderText = "Pay Period"

        gv1.Columns("Net_Payment").IsVisible = True
        gv1.Columns("Net_Payment").Width = 100
        gv1.Columns("Net_Payment").HeaderText = "Net Payment"

        gv1.Columns("EMP_CODE").IsVisible = True
        gv1.Columns("EMP_CODE").Width = 100
        gv1.Columns("EMP_CODE").HeaderText = "Emp Code"

        gv1.Columns("Emp_Name").IsVisible = True
        gv1.Columns("Emp_Name").Width = 100
        gv1.Columns("Emp_Name").HeaderText = "Emp Name"

        gv1.Columns("Bank_Account").IsVisible = True
        gv1.Columns("Bank_Account").Width = 100
        gv1.Columns("Bank_Account").HeaderText = "Bank Account "

        gv1.Columns("Loc_Add").IsVisible = False
        gv1.Columns("Loc_Add").Width = 100
        gv1.Columns("Loc_Add").HeaderText = "Location Address"

        gv1.Columns("Bank_Name").IsVisible = True
        gv1.Columns("Bank_Name").Width = 100
        gv1.Columns("Bank_Name").HeaderText = "Bank Name"

        gv1.Columns("COMPANY_BANK").IsVisible = False
        gv1.Columns("COMPANY_BANK").Width = 100
        gv1.Columns("COMPANY_BANK").HeaderText = "Compnay Bank "

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        gv1.GroupDescriptors.Add(New GridGroupByExpression("COMPANY_BANK as Item format ""{0}: {1}"" Group By COMPANY_BANK"))
        'gv1.GroupDescriptors.Add(New GridGroupByExpression("Bank_Name as Item format ""{0}: {1}"" Group By Bank_Name"))
        gv1.GroupDescriptors.Add(New GridGroupByExpression("Loc_Code as Item format ""{0}: {1}"" Group By Loc_Code"))
        gv1.GroupDescriptors.Add(New GridGroupByExpression("DEVISION_CODE as Item format ""{0}: {1}"" Group By DEVISION_CODE"))

        gv1.ShowGroupPanel = False
        gv1.MasterTemplate.AutoExpandGroups = True

        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom




    End Sub
    Sub print1(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            'arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Pay Period: " + lblFrompp.Text)
            If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
                Dim strLocationName As String = clsCommon.GetMulcallStringWithComma(txtLocationMult.arrValueMember)
                arrHeader.Add((" Location : " + strLocationName + " "))
            Else
                arrHeader.Add((" Location : All"))
            End If
            If txtBankMult.arrValueMember IsNot Nothing AndAlso txtBankMult.arrValueMember.Count > 0 Then
                arrHeader.Add(" Bank : " + clsCommon.GetMulcallStringWithComma(txtBankMult.arrValueMember))
            Else
                arrHeader.Add((" Bank: All"))
            End If
            If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
                arrHeader.Add("Division : " + clsCommon.GetMulcallStringWithComma(txtDivisionMult.arrValueMember))
            Else
                arrHeader.Add(("Division: All"))
            End If

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Bank Transfer Detail", gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Bank Transfer Detail", gv1, arrHeader, Me.Text, True)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub txtpayPeriod__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtpayPeriod._MYValidating
        Try
            Dim qry As String = "SELECT PAY_PERIOD_CODE AS 'Code',(DATEDIFF(DAY,date_from,date_to)+1) as 'Total days', " _
           & " PAY_PERIOD_NAME as 'Pay Period Name' FROM TSPL_PAYPERIOD_MASTER  "
            txtpayPeriod.Value = clsCommon.ShowSelectForm("TSPL_PAYPERIOD_MASTER", qry, "Code", "POSTED=1 AND FREEZED=0", txtpayPeriod.Value, "", isButtonClicked)
            If clsCommon.myLen(txtpayPeriod.Value) > 0 Then
                lblFrompp.Text = clsPayPeriodMaster.GetName(txtpayPeriod.Value, Nothing)
            Else
                lblFrompp.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        btnReferesh = False
        PrintData()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub RptBankTransferDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            SetUserMgmtNew()
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+P for Print ")
            ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RptBankTransferDetail_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isModifyFlag Then
                PrintData()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
                'DeleteData()
            ElseIf e.Alt And e.KeyCode = Keys.C Then
                Close()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub txtLocationMult__My_Click(sender As Object, e As EventArgs) Handles txtLocationMult._My_Click
        Try
            Dim qry As String = " select Location_Code as Code,Location_Desc as [Name] from TSPL_LOCATION_MASTER where LOCATION_CODE IN (select DISTINCT LOCATION_CODE from TSPL_GENERATE_SALARY where PAY_PERIOD_CODE='" & txtpayPeriod.Value & "') "
            txtLocationMult.arrValueMember = clsCommon.ShowMultipleSelectForm("LocMulSel", qry, "Code", "Name", txtLocationMult.arrValueMember, txtLocationMult.arrDispalyMember)
            Dim frmpending As New FrmPendingRequisitionQty()
            frmpending.SetDiplayMember(txtLocationMult, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtBankMult__My_Click(sender As Object, e As EventArgs) Handles txtBankMult._My_Click
        Try
            Dim qry As String = "select  TSPL_BANK_MASTER.BANK_CODE as [Code],TSPL_BANK_MASTER.DESCRIPTION as [Name]  from TSPL_BANK_MASTER "
            txtBankMult.arrValueMember = clsCommon.ShowMultipleSelectForm("BankMulSel", qry, "Code", "Name", txtBankMult.arrValueMember, txtBankMult.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDivisionMult__My_Click(sender As Object, e As EventArgs) Handles txtDivisionMult._My_Click
        Try
            Dim qry As String = " select DEVISION_CODE as Code,DEVISION_NAME as Name from TSPL_DEVISION_MASTER"
            txtDivisionMult.arrValueMember = clsCommon.ShowMultipleSelectForm("DivMulSel", qry, "Code", "Name", txtDivisionMult.arrValueMember, txtDivisionMult.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            btnReferesh = True
            PageSetupReport_ID = MyBase.Form_ID
            TemplateGridview = gv1
            PrintData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptBankTransferDetail & "'"))
                arrHeader.Add("Pay Period: " + lblFrompp.Text)
                If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
                    Dim strLocationName As String = clsCommon.GetMulcallStringWithComma(txtLocationMult.arrValueMember)
                    arrHeader.Add(("Location : " + strLocationName + " "))
                Else
                    arrHeader.Add(("Location : All"))
                End If
                If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
                    arrHeader.Add("Division : " + clsCommon.GetMulcallStringWithComma(txtDivisionMult.arrDispalyMember))
                End If
                If txtBankMult.arrValueMember IsNot Nothing AndAlso txtBankMult.arrValueMember.Count > 0 Then
                    arrHeader.Add("Bank : " + clsCommon.GetMulcallStringWithComma(txtBankMult.arrDispalyMember))
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
                    'transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                    'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                    transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                    clsCommon.MyExportToExcelGrid("Bank Transfer Detail", gv1, arrHeader, Me.Text)
                Else
                    transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("Bank Transfer Detail", gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            ExportGrid(EnumExportTo.Excel)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            ExportGrid(EnumExportTo.PDF)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        Try
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
                    common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", Me.Text)
                End If
                obj.GridLayout.Close()
                obj.GridLayout.Dispose()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        Try
            clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
            common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
