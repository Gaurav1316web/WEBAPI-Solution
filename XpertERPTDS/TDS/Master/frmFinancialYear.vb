'Created By---> Mayank
'Created Date--->16/june/2011
'Modified By--> Mayank
'Last Modify Date-->20/june/2011
'Tables Used-->TSPL_TDS_FINANCIAL_YEAR
'--preeti gupta..ticket no.[BM00000003134]
Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.WinControls.Enumerations
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports System.Threading
Imports common
Imports System.Data
Imports XpertERPEngine


Public Class frmFinancialYear
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FinancialYear)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Me.Close()
        End If
        rbtnSave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 02/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If rbtnSave.Visible = True Then
            RadMenuItemImport.Enabled = True
            RadMenuItemExport.Enabled = True
        Else
            RadMenuItemImport.Enabled = False
            RadMenuItemExport.Enabled = False
        End If
        '--------------------------------------------------
        'btnPost.Visible = MyBase.isPostFlag
        rbtnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub frmFinancialYear_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso rbtnSave.Enabled Then
            SaveData()
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            '    PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso rbtnDelete.Enabled Then
            Deletedata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub
    Private Sub frmFinancialYear_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ' globalFunc.mandatoryText()
        'globalFunc.mandatoryDropdown(dtpFromDate.Text)
        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-GB")
        dtpFromDate.Value = connectSql.myDate()
        'AddHandler fndFinancialYearold.txtValue.TextChanged, AddressOf fndFinancialYear_Textchanged
        fndFinancialYear.MyReadOnly = True
        rbtnDelete.Enabled = False
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub
    'It Is Used To Save And Update All Records
    Private Sub rbtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnSave.Click
        SaveData()

    End Sub
    Sub SaveData()

        If dtpFromDate.Text = "" Then
            myMessages.blankValue(Me, "From Date", Me.Text)
            txtToDate.Focus()
        ElseIf rbtnSave.Text = "Save" Then

            Dim strQuery As String = "select From_date from TSPL_TDS_FINANCIAL_YEAR where From_Date='" + dtpFromDate.Value + "'"

            Dim strvalue As String = clsDBFuncationality.getSingleValue(strQuery)
                      If strvalue <> "" Then
                common.clsCommon.MyMessageBoxShow(Me, "Record Already Exist", Me.Text)
                'funReset()
                Exit Sub
            Else
                funInsert()
            End If
        Else
            funUpdate()
        End If
    End Sub
    'This is Insert Function Used To Insert Values In TSPL_TDS_FINANCIAL_YEAR
    Private Sub funInsert()
        Try
            Dim from_date As String = Format(dtpFromDate.Value, "dd/MM/yyyy")
            Dim dueDate1 As String = Format(dtpDueDates1.Value, "dd/MM/yyyy")
            Dim dueDate2 As String = Format(dtpDueDates2.Value, "dd/MM/yyyy")
            Dim dueDate3 As String = Format(dtpDueDates3.Value, "dd/MM/yyyy")
            Dim dueDate4 As String = Format(dtpDueDates4.Value, "dd/MM/yyyy")
            Dim dueDate5 As String = Format(dtpDueDates5.Value, "dd/MM/yyyy")
            Dim dueDate6 As String = Format(dtpDueDates6.Value, "dd/MM/yyyy")
            Dim dueDate7 As String = Format(dtpDueDates7.Value, "dd/MM/yyyy")
            Dim dueDate8 As String = Format(dtpDueDates8.Value, "dd/MM/yyyy")
            Dim dueDate9 As String = Format(dtpDueDates9.Value, "dd/MM/yyyy")
            Dim dueDate10 As String = Format(dtpDueDates10.Value, "dd/MM/yyyy")
            Dim dueDate11 As String = Format(dtpDueDates11.Value, "dd/MM/yyyy")
            Dim dueDate12 As String = Format(dtpDueDates12.Value, "dd/MM/yyyy")

            connectSql.RunSp("SP_TSPL_TDS_FINANCIAL_YEAR_INSERT", New SqlParameter("@From_Date", from_date), New SqlParameter("@To_Date", txtToDate.Text), New SqlParameter("@Year_Name", txtYearName.Text), New SqlParameter("@End_Date1", txtEnddate1.Text), New SqlParameter("@End_Date2", txtEnddate2.Text), New SqlParameter("@End_Date3", txtEnddate3.Text), New SqlParameter("@End_Date4", txtEnddate4.Text), New SqlParameter("@End_Date5", txtEnddate5.Text), New SqlParameter("@End_Date6", txtEnddate6.Text), New SqlParameter("@End_Date7", txtEnddate7.Text), New SqlParameter("@End_Date8", txtEnddate8.Text), New SqlParameter("@End_Date9", txtEnddate9.Text), New SqlParameter("@End_Date10", txtEnddate10.Text), New SqlParameter("@End_Date11", txtEnddate11.Text), New SqlParameter("@End_Date12", txtEnddate12.Text), New SqlParameter("@Due_Date1", dueDate1), New SqlParameter("@Due_Date2", dueDate2), New SqlParameter("@Due_Date3", dueDate3), New SqlParameter("@Due_Date4", dueDate4), New SqlParameter("@Due_Date5", dueDate5), New SqlParameter("@Due_Date6", dueDate6), New SqlParameter("@Due_Date7", dueDate7), New SqlParameter("@Due_Date8", dueDate8), New SqlParameter("@Due_Date9", dueDate9), New SqlParameter("@Due_Date10", dueDate10), New SqlParameter("@Due_Date11", dueDate11), New SqlParameter("@Due_Date12", dueDate12), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate()), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate()), New SqlParameter("@Comp_Code", companyCode))
            myMessages.insert()
            fndFinancialYear.Value = from_date
            rbtnSave.Text = "Update"
            rbtnDelete.Enabled = True
            'If userCode <> "ADMIN" Then
            '    If funSetUserAccess() = False Then Exit Sub
            'End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'This is Update Function Used To Update Records In TSPL_TDS_FINANCIAL_YEAR
    Private Sub funUpdate()
        Try
            Dim from_date As String = Format(dtpFromDate.Value, "dd/MM/yyyy")
            Dim dueDate1 As String = Format(dtpDueDates1.Value, "dd/MM/yyyy")
            Dim dueDate2 As String = Format(dtpDueDates2.Value, "dd/MM/yyyy")
            Dim dueDate3 As String = Format(dtpDueDates3.Value, "dd/MM/yyyy")
            Dim dueDate4 As String = Format(dtpDueDates4.Value, "dd/MM/yyyy")
            Dim dueDate5 As String = Format(dtpDueDates5.Value, "dd/MM/yyyy")
            Dim dueDate6 As String = Format(dtpDueDates6.Value, "dd/MM/yyyy")
            Dim dueDate7 As String = Format(dtpDueDates7.Value, "dd/MM/yyyy")
            Dim dueDate8 As String = Format(dtpDueDates8.Value, "dd/MM/yyyy")
            Dim dueDate9 As String = Format(dtpDueDates9.Value, "dd/MM/yyyy")
            Dim dueDate10 As String = Format(dtpDueDates10.Value, "dd/MM/yyyy")
            Dim dueDate11 As String = Format(dtpDueDates11.Value, "dd/MM/yyyy")
            Dim dueDate12 As String = Format(dtpDueDates12.Value, "dd/MM/yyyy")

            connectSql.RunSp("SP_TSPL_TDS_FINANCIAL_YEAR_UPDATE", New SqlParameter("@From_Date", from_date), New SqlParameter("@To_Date", txtToDate.Text), New SqlParameter("@Year_Name", txtYearName.Text), New SqlParameter("@End_Date1", txtEnddate1.Text), New SqlParameter("@End_Date2", txtEnddate2.Text), New SqlParameter("@End_Date3", txtEnddate3.Text), New SqlParameter("@End_Date4", txtEnddate4.Text), New SqlParameter("@End_Date5", txtEnddate5.Text), New SqlParameter("@End_Date6", txtEnddate6.Text), New SqlParameter("@End_Date7", txtEnddate7.Text), New SqlParameter("@End_Date8", txtEnddate8.Text), New SqlParameter("@End_Date9", txtEnddate9.Text), New SqlParameter("@End_Date10", txtEnddate10.Text), New SqlParameter("@End_Date11", txtEnddate11.Text), New SqlParameter("@End_Date12", txtEnddate12.Text), New SqlParameter("@Due_Date1", dueDate1), New SqlParameter("@Due_Date2", dueDate2), New SqlParameter("@Due_Date3", dueDate3), New SqlParameter("@Due_Date4", dueDate4), New SqlParameter("@Due_Date5", dueDate5), New SqlParameter("@Due_Date6", dueDate6), New SqlParameter("@Due_Date7", dueDate7), New SqlParameter("@Due_Date8", dueDate8), New SqlParameter("@Due_Date9", dueDate9), New SqlParameter("@Due_Date10", dueDate10), New SqlParameter("@Due_Date11", dueDate11), New SqlParameter("@Due_Date12", dueDate12), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate()), New SqlParameter("@Comp_Code", companyCode))
            myMessages.update()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'This event is used to fill all fields according to The value in Date Time Picker (dtpFromDate)
    Private Sub dtpFromDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFromDate.ValueChanged
        txtToDate.Text = dtpFromDate.Value.AddDays(-1).AddYears(1).ToString("dd/MM/yyyy")
        txtYearName.Text = "FY " + dtpFromDate.Value.ToString("yyyy") + " - " + CInt(txtToDate.Text.ToString().Substring(7, 3)).ToString()
        Dim arr As New ArrayList
        Dim arr1 As New ArrayList
        Dim arr2 As New ArrayList
        For i As Integer = 1 To 12
            arr.Insert(i - 1, dtpFromDate.Value.AddDays(-1).AddMonths(i).ToString("dd/MM/yyyy"))
            arr1.Insert(i - 1, dtpFromDate.Value.AddDays(-1).AddMonths(i))
            arr2.Insert(i - 1, dtpFromDate.Value.AddDays(6).AddMonths(i))
        Next i
        txtEnddate1.Text = arr.Item(0)
        txtEnddate2.Text = arr.Item(1)
        txtEnddate3.Text = arr.Item(2)
        txtEnddate4.Text = arr.Item(3)
        txtEnddate5.Text = arr.Item(4)
        txtEnddate6.Text = arr.Item(5)
        txtEnddate7.Text = arr.Item(6)
        txtEnddate8.Text = arr.Item(7)
        txtEnddate9.Text = arr.Item(8)
        txtEnddate10.Text = arr.Item(9)
        txtEnddate11.Text = arr.Item(10)
        txtEnddate12.Text = arr.Item(11)
        txtEnddate1.Tag = arr1.Item(0)
        txtEnddate2.Tag = arr1.Item(1)
        txtEnddate3.Tag = arr1.Item(2)
        txtEnddate4.Tag = arr1.Item(3)
        txtEnddate5.Tag = arr1.Item(4)
        txtEnddate6.Tag = arr1.Item(5)
        txtEnddate7.Tag = arr1.Item(6)
        txtEnddate8.Tag = arr1.Item(7)
        txtEnddate9.Tag = arr1.Item(8)
        txtEnddate10.Tag = arr1.Item(9)
        txtEnddate11.Tag = arr1.Item(10)
        txtEnddate12.Tag = arr1.Item(11)
        dtpDueDates1.MinDate = CDate(txtEnddate1.Tag)
        dtpDueDates2.MinDate = CDate(txtEnddate2.Tag)
        dtpDueDates3.MinDate = CDate(txtEnddate3.Tag)
        dtpDueDates4.MinDate = CDate(txtEnddate4.Tag)
        dtpDueDates5.MinDate = CDate(txtEnddate5.Tag)
        dtpDueDates6.MinDate = CDate(txtEnddate6.Tag)
        dtpDueDates7.MinDate = CDate(txtEnddate7.Tag)
        dtpDueDates8.MinDate = CDate(txtEnddate8.Tag)
        dtpDueDates9.MinDate = CDate(txtEnddate9.Tag)
        dtpDueDates10.MinDate = CDate(txtEnddate10.Tag)
        dtpDueDates11.MinDate = CDate(txtEnddate11.Tag)
        dtpDueDates12.MinDate = CDate(txtEnddate12.Tag)
        dtpDueDates1.Value = arr2.Item(0)
        dtpDueDates2.Value = arr2.Item(1)
        dtpDueDates3.Value = arr2.Item(2)
        dtpDueDates4.Value = arr2.Item(3)
        dtpDueDates5.Value = arr2.Item(4)
        dtpDueDates6.Value = arr2.Item(5)
        dtpDueDates7.Value = arr2.Item(6)
        dtpDueDates8.Value = arr2.Item(7)
        dtpDueDates9.Value = arr2.Item(8)
        dtpDueDates10.Value = arr2.Item(9)
        dtpDueDates11.Value = arr2.Item(10)
        dtpDueDates12.Value = arr2.Item(11)
        rbtnSave.Text = "Save"
        rbtnDelete.Enabled = False
    End Sub
    'It Is Used To Delete The Record From TSPL_TDS_FINANCIAL_YEAR
    Private Sub rbtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnDelete.Click
        Deletedata()

    End Sub
    Sub Deletedata()
        If myMessages.deleteConfirm() Then
            funDelete()
            rbtnSave.Text = "Save"
            rbtnDelete.Enabled = False
        End If
    End Sub
    'This is Delete Function Used To Delete Records From TSPL_TDS_FINANCIAL_YEAR
    Private Sub funDelete()
        Try
            Dim fromdate As String = Format(dtpFromDate.Value, "dd/MM/yyyy")
            connectSql.RunSp("SP_TSPL_TDS_FINANCIAL_YEAR_DELETE", New SqlParameter("@From_Date", fromdate))
            myMessages.delete()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'It is Used To Clear All Fields Of Current Windows Form
    Private Sub rdbtnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnRefresh.Click
        funReset()
    End Sub
    'This is Reset Function Used To Clear All Fields Of Current Windows Form
    Private Sub funReset()
        Try
            dtpFromDate.Value = Date.Today()
            rbtnSave.Text = "Save"
            rbtnDelete.Enabled = False
            fndFinancialYear.Value = ""
            dtpFromDate.Enabled = True
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'It Is Used To Close The Current Windows Form
    Private Sub rbtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnClose.Click
        Me.Close()
    End Sub
    'It Is Used To Fill The From Date in fndFinancialYear 
    'Private Sub fndFinancialYear_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fndFinancialYearold.Load
    '    fndFinancialYearold.ConnectionString = connectSql.SqlCon()
    '    fndFinancialYearold.Query = "select From_Date as [From Date],To_Date as [To Date],Year_Name as [Year Name] from TSPL_TDS_FINANCIAL_YEAR"
    '    fndFinancialYearold.ValueToSelect = "From Date"
    '    fndFinancialYearold.Caption = "Financial Year"
    '    fndFinancialYearold.ValueToSelect1 = "Year Name"
    '    fndFinancialYearold.txtValue.MaxLength = 12
    'End Sub

    '' Added by Abhishek kumar as on 2/06/2012
    Private Sub fndFinancialYear__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndFinancialYear._MYValidating
        If fndFinancialYear.MyReadOnly Or isButtonClicked Then
            Dim qry As String = "select From_Date as [FromDate],To_Date as [ToDate],Year_Name as [YearName] from TSPL_TDS_FINANCIAL_YEAR "
            fndFinancialYear.Value = clsCommon.ShowSelectForm("FinancialYearfnd", qry, "FromDate", "", fndFinancialYear.Value, "FromDate", isButtonClicked)
            fndFinancialYear.MyMaxLength = 12
            LoadFinancialYear()
        End If
    End Sub
    ' '' Added by Abhishek kumar as on 2/06/2012
    Private Sub fndFinancialYear__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndFinancialYear._MYNavigator
        Dim qry As String = "select From_Date as [FromDate],To_Date as [ToDate],Year_Name as [YearName] from TSPL_TDS_FINANCIAL_YEAR where 2=2"
        Select Case NavType
            Case NavigatorType.Current
                qry += " and TSPL_TDS_FINANCIAL_YEAR  .From_Date in ('" + fndFinancialYear.Value + "')"
            Case NavigatorType.Next
                qry += " and TSPL_TDS_FINANCIAL_YEAR  .From_Date in (select min(From_Date) from TSPL_TDS_FINANCIAL_YEAR where From_Date >'" + fndFinancialYear.Value + "')"
            Case NavigatorType.First
                qry += " and TSPL_TDS_FINANCIAL_YEAR  .From_Date in (select MIN(From_Date ) from TSPL_TDS_FINANCIAL_YEAR)"

            Case NavigatorType.Last
                qry += " and TSPL_TDS_FINANCIAL_YEAR  .From_Date in (select Max(From_Date ) from TSPL_TDS_FINANCIAL_YEAR)"
            Case NavigatorType.Previous
                qry += " and TSPL_TDS_FINANCIAL_YEAR  .From_Date in (select Max(From_Date ) from TSPL_TDS_FINANCIAL_YEAR where From_Date  <'" + fndFinancialYear.Value + "')"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            fndFinancialYear.Value = clsCommon.myCstr(dt.Rows(0)("FromDate"))

        End If
        Reset()
        LoadFinancialYear()

    End Sub
    'It Is Used To Fill Or Clear All Fields of Current Windows Form Bassed On From_Date(fndFinancialYear) From TSPL_TDS_FINANCIAL_YEAR
    'Public Sub fndFinancialYear_Textchanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    LoadFinancialYear()
    'End Sub
    Public Sub LoadFinancialYear()
        Dim Fromdate As String = "select From_Date from TSPL_TDS_FINANCIAL_YEAR where From_Date ='" + fndFinancialYear.Value + "'"
        Dim strvalue As String
        strvalue = clsDBFuncationality.getSingleValue(Fromdate)
       
        If strvalue <> "" Then
            funfill()
        Else
            dtpFromDate.Value = Date.Today()
            rbtnSave.Text = "Save"
            rbtnDelete.Enabled = False
            dtpFromDate.Enabled = True
        End If
    End Sub
    'This is Funfill Function Used To Fill All Fields of Current Windows Form.
    Private Sub funfill()
        Try
            Dim query As String = "select From_Date,To_Date,Year_Name,End_Date1,End_Date2,End_Date3,End_Date4,End_Date5,End_Date6,End_Date7,End_Date8,End_Date9,End_Date10,End_Date11,End_Date12,Due_Date1,Due_Date2,Due_Date3,Due_Date4,Due_Date5,Due_Date6,Due_Date7,Due_Date8,Due_Date9,Due_Date10,Due_Date11,Due_Date12 from TSPL_TDS_FINANCIAL_YEAR where From_Date='" + fndFinancialYear.Value + "'"


            Dim dt As DataTable
            dt = clsDBFuncationality.GetDataTable(query)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    dtpFromDate.Value = CDate(dt.Rows(i)("From_Date"))
                    txtToDate.Text = dt.Rows(i)("To_Date")
                    txtYearName.Text = dt.Rows(i)("Year_Name")
                    txtEnddate1.Text = dt.Rows(i)("End_Date1")
                    txtEnddate2.Text = dt.Rows(i)("End_Date2")
                    txtEnddate3.Text = dt.Rows(i)("End_Date3")
                    txtEnddate4.Text = dt.Rows(i)("End_Date4")
                    txtEnddate5.Text = dt.Rows(i)("End_Date5")
                    txtEnddate6.Text = dt.Rows(i)("End_Date6")
                    txtEnddate7.Text = dt.Rows(i)("End_Date7")
                    txtEnddate8.Text = dt.Rows(i)("End_Date8")
                    txtEnddate9.Text = dt.Rows(i)("End_Date9")
                    txtEnddate10.Text = dt.Rows(i)("End_Date10")
                    txtEnddate11.Text = dt.Rows(i)("End_Date11")
                    txtEnddate12.Text = dt.Rows(i)("End_Date12")

                    dtpDueDates1.Value = dt.Rows(i)("Due_Date1")
                    dtpDueDates2.Value = dt.Rows(i)("Due_Date2")
                    dtpDueDates3.Value = dt.Rows(i)("Due_Date3")
                    dtpDueDates4.Value = dt.Rows(i)("Due_Date4")
                    dtpDueDates5.Value = dt.Rows(i)("Due_Date5")
                    dtpDueDates6.Value = dt.Rows(i)("Due_Date6")
                    dtpDueDates7.Value = dt.Rows(i)("Due_Date7")
                    dtpDueDates8.Value = dt.Rows(i)("Due_Date8")
                    dtpDueDates9.Value = dt.Rows(i)("Due_Date9")
                    dtpDueDates10.Value = dt.Rows(i)("Due_Date10")
                    dtpDueDates11.Value = dt.Rows(i)("Due_Date11")
                    dtpDueDates12.Value = dt.Rows(i)("Due_Date12")
                    rbtnSave.Text = "Update"
                    rbtnDelete.Enabled = True
                    dtpFromDate.Enabled = False
                    dtpFromDate.BackColor = Color.White
                Next
            End If





            ''Dim dr As SqlDataReader = connectSql.RunSqlReturnDR(query)
            ''If dr.Read() Then
            ''    dtpFromDate.Value = CDate(dr(0).ToString())
            ''    txtToDate.Text = dr(1).ToString()
            ''    txtYearName.Text = dr(2).ToString()
            ''    txtEnddate1.Text = dr(3).ToString()
            ''    txtEnddate2.Text = dr(4).ToString()
            ''    txtEnddate3.Text = dr(5).ToString()
            ''    txtEnddate4.Text = dr(6).ToString()
            ''    txtEnddate5.Text = dr(7).ToString()
            ''    txtEnddate6.Text = dr(8).ToString()
            ''    txtEnddate7.Text = dr(9).ToString()
            ''    txtEnddate8.Text = dr(10).ToString()
            ''    txtEnddate9.Text = dr(11).ToString()
            ''    txtEnddate10.Text = dr(12).ToString()
            ''    txtEnddate11.Text = dr(13).ToString()
            ''    txtEnddate12.Text = dr(14).ToString()

            ''    dtpDueDates1.Value = dr(15).ToString()
            ''    dtpDueDates2.Value = dr(16).ToString()
            ''    dtpDueDates3.Value = dr(17).ToString()
            ''    dtpDueDates4.Value = dr(18).ToString()
            ''    dtpDueDates5.Value = dr(19).ToString()
            ''    dtpDueDates6.Value = dr(20).ToString()
            ''    dtpDueDates7.Value = dr(21).ToString()
            ''    dtpDueDates8.Value = dr(22).ToString()
            ''    dtpDueDates9.Value = dr(23).ToString()
            ''    dtpDueDates10.Value = dr(24).ToString()
            ''    dtpDueDates11.Value = dr(25).ToString()
            ''    dtpDueDates12.Value = dr(26).ToString()
            ''    rbtnSave.Text = "Update"
            ''    rbtnDelete.Enabled = True
            ''    dtpFromDate.Enabled = False
            ''    dtpFromDate.BackColor = Color.White
            ''    'If userCode <> "ADMIN" Then
            ''    '    If funSetUserAccess() = False Then Exit Sub
            ''    'End If
            ''End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'It Is Used To Export The Records From TSPL_TDS_FINANCIAL_YEAR
    Private Sub RadMenuItemExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemExport.Click
        '' Anubhooti 24-July-2014 (BM00000003135)
        Dim Sql As String = "Select From_Date as [From Date],To_Date as [To Date],Year_Name as [Year Name],End_Date1 as [End Date1],End_Date2 as [End Date2],End_Date3 as [End Date3],End_Date4 as [End Date4],End_Date5 as [End Date5],End_Date6 as [End Date6],End_Date7 as [End Date7],End_Date8 as [End Date8],End_Date9 as [End Date9],End_Date10 as [End Date10],End_Date11 as [End Date11],End_Date12 as [End Date12],Due_Date1 as [Due Date1],Due_Date2 as [Due Date2],Due_Date3 as [Due Date3],Due_Date4 as [Due Date4],Due_Date5 as [Due Date5],Due_Date6 as [Due Date6],Due_Date7 as [Due Date7],Due_Date8 as [Due Date8],Due_Date9 as [Due Date9],Due_Date10 as [Due Date10],Due_Date11 as [Due Date11],Due_Date12 as [Due Date12] from TSPL_TDS_FINANCIAL_YEAR"
        transportSql.ExporttoExcel(Sql, Me)
    End Sub
    'It Is Used To Import The Records From TSPL_TDS_FINANCIAL_YEAR
    Private Sub RadMenuItemImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "From Date", "To Date", "Year Name", "End Date1", "End Date2", "End Date3", "End Date4", "End Date5", "End Date6", "End Date7", "End Date8", "End Date9", "End Date10", "End Date11", "End Date12", "Due Date1", "Due Date2", "Due Date3", "Due Date4", "Due Date5", "Due Date6", "Due Date7", "Due Date8", "Due Date9", "Due Date10", "Due Date11", "Due Date12") Then
            Dim trans As SqlTransaction = Nothing
            Try
                connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows

                    Dim StrFromDate As String
                    Dim strFrom_date As String = grow.Cells(0).Value.ToString()
                    If String.IsNullOrEmpty(strFrom_date) Or clsCommon.myLen(strFrom_date) > 10 Then
                        Throw New Exception("From Date can not be left Blank or size can not be greater than 10")
                    Else
                        StrFromDate = CDate(strFrom_date)
                    End If

                    Dim strToDate As String = clsCommon.myCstr(grow.Cells(1).Value)
                    If String.IsNullOrEmpty(strToDate) Or clsCommon.myLen(strToDate) > 10 Then
                        Throw New Exception("To Date Can not be left Blank or greater than 10")
                    End If

                    Dim strYearName As String = clsCommon.myCstr(grow.Cells(2).Value)
                    If String.IsNullOrEmpty(strYearName) Or clsCommon.myLen(strYearName) > 20 Then
                        Throw New Exception("Year Name can not be left blank or greater than 20")
                    End If

                    Dim strEndDate1 As String = clsCommon.myCstr(grow.Cells(3).Value)
                    If String.IsNullOrEmpty(strEndDate1) Or clsCommon.myLen(strEndDate1) > 10 Then
                        Throw New Exception("End Date1 Can not be left blank or greater than 10")
                    End If

                    Dim strEndDate2 As String = clsCommon.myCstr(grow.Cells(4).Value)
                    If String.IsNullOrEmpty(strEndDate2) Or clsCommon.myLen(strEndDate2) > 10 Then
                        Throw New Exception("End Date2 Can not be left blank or greater than 10")
                    End If

                    Dim strEndDate3 As String = clsCommon.myCstr(grow.Cells(5).Value)
                    If String.IsNullOrEmpty(strEndDate3) Or clsCommon.myLen(strEndDate3) > 10 Then
                        Throw New Exception("End Date3 Can not be left blank or greater than 10")
                    End If

                    Dim strEndDate4 As String = clsCommon.myCstr(grow.Cells(6).Value)
                    If String.IsNullOrEmpty(strEndDate4) Or clsCommon.myLen(strEndDate4) > 10 Then
                        Throw New Exception("End Date4 Can not be left blank or greater than 10")
                    End If

                    Dim strEndDate5 As String = clsCommon.myCstr(grow.Cells(7).Value)
                    If String.IsNullOrEmpty(strEndDate5) Or clsCommon.myLen(strEndDate5) > 10 Then
                        Throw New Exception("End Date5 Can not be left blank or greater than 10")
                    End If

                    Dim strEndDate6 As String = clsCommon.myCstr(grow.Cells(8).Value)
                    If String.IsNullOrEmpty(strEndDate6) Or clsCommon.myLen(strEndDate6) > 10 Then
                        Throw New Exception("End Date6 Can not be left blank or greater than 10")
                    End If

                    Dim strEndDate7 As String = clsCommon.myCstr(grow.Cells(9).Value)
                    If String.IsNullOrEmpty(strEndDate7) Or clsCommon.myLen(strEndDate7) > 10 Then
                        Throw New Exception("End Date7 Can not be left blank or greater than 10")
                    End If

                    Dim strEndDate8 As String = clsCommon.myCstr(grow.Cells(10).Value)
                    If String.IsNullOrEmpty(strEndDate8) Or clsCommon.myLen(strEndDate8) > 10 Then
                        Throw New Exception("End Date8 Can not be left blank or greater than 10")
                    End If

                    Dim strEndDate9 As String = clsCommon.myCstr(grow.Cells(11).Value)
                    If String.IsNullOrEmpty(strEndDate9) Or clsCommon.myLen(strEndDate9) > 10 Then
                        Throw New Exception("End Date9 Can not be left blankn or greater than 10")
                    End If

                    Dim strEndDate10 As String = clsCommon.myCstr(grow.Cells(12).Value)
                    If String.IsNullOrEmpty(strEndDate10) Or clsCommon.myLen(strEndDate10) > 10 Then
                        Throw New Exception("End Date10 Can not be left blank or greater than 10")
                    End If

                    Dim strEndDate11 As String = clsCommon.myCstr(grow.Cells(13).Value)
                    If String.IsNullOrEmpty(strEndDate11) Or clsCommon.myLen(strEndDate11) > 10 Then
                        Throw New Exception("End Date11 Can not be left blank or greater than 10")
                    End If

                    Dim strEndDate12 As String = clsCommon.myCstr(grow.Cells(14).Value)
                    If String.IsNullOrEmpty(strEndDate12) Or clsCommon.myLen(strEndDate12) > 10 Then
                        Throw New Exception("End Date12 Can not be left blank or greater than 10")
                    End If

                    Dim strDue_date1 As String
                    Dim strDueDate1 As String = clsCommon.myCstr(grow.Cells(15).Value)
                    If String.IsNullOrEmpty(strDueDate1) Or clsCommon.myLen(strDueDate1) > 10 Then
                        Throw New Exception("Due Date1 Can not be left blank or greater than 10")
                        'ElseIf strDueDate1 = "" Then
                        '    strDue_date1 = ""
                    Else
                        strDue_date1 = CDate(strDueDate1)
                    End If


                    Dim strDue_date2 As String
                    Dim strDueDate2 As String = clsCommon.myCstr(grow.Cells(16).Value)
                    If String.IsNullOrEmpty(strDueDate2) Or clsCommon.myLen(strDueDate2) > 10 Then
                        Throw New Exception("Due Date2 Can not be left blank or greater than 10")
                    Else
                        strDue_date2 = CDate(strDueDate2)
                    End If

                    Dim strDue_date3 As String
                    Dim strDueDate3 As String = clsCommon.myCstr(grow.Cells(17).Value)
                    If String.IsNullOrEmpty(strDueDate3) Or clsCommon.myLen(strDueDate3) > 10 Then
                        Throw New Exception("Due Date3 Can not be left blank or greater than 10")
                    Else
                        strDue_date3 = CDate(strDueDate3)
                    End If

                    Dim strDue_date4 As String
                    Dim strDueDate4 As String = clsCommon.myCstr(grow.Cells(18).Value)
                    If String.IsNullOrEmpty(strDueDate4) Or clsCommon.myLen(strDueDate4) > 10 Then
                        Throw New Exception("Due Date4 Can not be left blank or greater than 10")
                        'ElseIf strDueDate4 = "" Then
                        '    strDue_date4 = ""
                    Else
                        strDue_date4 = CDate(strDueDate4)
                    End If


                    Dim strDue_date5 As String
                    Dim strDueDate5 As String = clsCommon.myCstr(grow.Cells(19).Value)
                    If String.IsNullOrEmpty(strDueDate5) Or clsCommon.myLen(strDueDate5) > 10 Then
                        Throw New Exception("Due Date5 Can not be left blank or greater than 10")
                        'ElseIf strDueDate5 = "" Then
                        '    strDue_date5 = ""
                    Else
                        strDue_date5 = CDate(strDueDate5)
                    End If

                    Dim strDue_date6 As String
                    Dim strDueDate6 As String = clsCommon.myCstr(grow.Cells(20).Value)
                    If String.IsNullOrEmpty(strDueDate6) Or clsCommon.myLen(strDueDate6) > 10 Then
                        Throw New Exception("Due Date6 Can not be left blank or greater than 10")
                        'ElseIf strDueDate6 = "" Then
                        '    strDue_date6 = ""
                    Else
                        strDue_date6 = CDate(strDueDate6)
                    End If


                    Dim strDue_date7 As String
                    Dim strDueDate7 As String = clsCommon.myCstr(grow.Cells(21).Value)
                    If String.IsNullOrEmpty(strDueDate7) Or clsCommon.myLen(strDueDate7) > 10 Then
                        Throw New Exception("Due Date7 Can not be left blank or greater than 10")
                        'ElseIf strDueDate7 = "" Then
                        '    strDue_date7 = ""
                    Else
                        strDue_date7 = CDate(strDueDate7)
                    End If

                    Dim strDue_date8 As String
                    Dim strDueDate8 As String = clsCommon.myCstr(grow.Cells(22).Value)
                    If String.IsNullOrEmpty(strDueDate8) Or clsCommon.myLen(strDueDate8) > 10 Then
                        Throw New Exception("Due Date8 Can not be left blank or greater than 10")
                        'ElseIf strDueDate8 = "" Then
                        '    strDue_date8 = ""
                    Else
                        strDue_date8 = CDate(strDueDate8)
                    End If

                    Dim strDue_date9 As String
                    Dim strDueDate9 As String = clsCommon.myCstr(grow.Cells(23).Value)
                    If String.IsNullOrEmpty(strDueDate9) Or clsCommon.myLen(strDueDate9) > 10 Then
                        Throw New Exception("Due Date9 Can not be left blank or greater than 10")
                        'ElseIf strDueDate9 = "" Then
                        '    strDue_date9 = ""
                    Else
                        strDue_date9 = CDate(strDueDate9)
                    End If

                    Dim strDue_date10 As String
                    Dim strDueDate10 As String = clsCommon.myCstr(grow.Cells(24).Value)
                    If String.IsNullOrEmpty(strDueDate10) Or clsCommon.myLen(strDueDate10) > 10 Then
                        Throw New Exception("Due Date10 Can not be left blank or greater than 10")
                        'ElseIf strDueDate10 = "" Then
                        '    strDue_date10 = ""
                    Else
                        strDue_date10 = CDate(strDueDate10)
                    End If

                    Dim strDue_date11 As String
                    Dim strDueDate11 As String = clsCommon.myCstr(grow.Cells(25).Value)
                    If String.IsNullOrEmpty(strDueDate11) Or clsCommon.myLen(strDueDate11) > 10 Then
                        Throw New Exception("Due Date11 Can not be left blank or greater than 10")
                        'ElseIf strDueDate11 = "" Then
                        '    strDue_date11 = ""
                    Else
                        strDue_date11 = CDate(strDueDate11)
                    End If

                    Dim strDue_date12 As String
                    Dim strDueDate12 As String = clsCommon.myCstr(grow.Cells(26).Value)
                    If String.IsNullOrEmpty(strDueDate12) Or clsCommon.myLen(strDueDate12) > 10 Then
                        Throw New Exception("Due Date12 Can not be left blank or greater than 10")
                        'ElseIf strDueDate12 = "" Then
                        '    strDue_date12 = ""
                    Else
                        strDue_date12 = CDate(strDueDate12)
                    End If

                    Dim count As String = "select count(*) from TSPL_TDS_FINANCIAL_YEAR where From_Date='" + StrFromDate + "'"
                    Dim i As Integer = CInt(connectSql.RunScalar(trans, count))
                    If (i = 0) Then
                        connectSql.RunSpTransaction(trans, "SP_TSPL_TDS_FINANCIAL_YEAR_INSERT", New SqlParameter("@From_Date", StrFromDate), New SqlParameter("@To_Date", strToDate), New SqlParameter("@Year_Name", strYearName), New SqlParameter("@End_Date1", strEndDate1), New SqlParameter("@End_Date2", strEndDate2), New SqlParameter("@End_Date3", strEndDate3), New SqlParameter("@End_Date4", strEndDate4), New SqlParameter("@End_Date5", strEndDate5), New SqlParameter("@End_Date6", strEndDate6), New SqlParameter("@End_Date7", strEndDate7), New SqlParameter("@End_Date8", strEndDate8), New SqlParameter("@End_Date9", strEndDate9), New SqlParameter("@End_Date10", strEndDate10), New SqlParameter("@End_Date11", strEndDate11), New SqlParameter("@End_Date12", strEndDate12), New SqlParameter("@Due_Date1", strDue_date1), New SqlParameter("@Due_Date2", strDue_date2), New SqlParameter("@Due_Date3", strDue_date3), New SqlParameter("@Due_Date4", strDue_date4), New SqlParameter("@Due_Date5", strDue_date5), New SqlParameter("@Due_Date6", strDue_date6), New SqlParameter("@Due_Date7", strDue_date7), New SqlParameter("@Due_Date8", strDue_date8), New SqlParameter("@Due_Date9", strDue_date9), New SqlParameter("@Due_Date10", strDue_date10), New SqlParameter("@Due_Date11", strDue_date11), New SqlParameter("@Due_Date12", strDue_date12), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", companyCode))
                    Else
                        connectSql.RunSpTransaction(trans, "SP_TSPL_TDS_FINANCIAL_YEAR_UPDATE", New SqlParameter("@From_Date", StrFromDate), New SqlParameter("@To_Date", strToDate), New SqlParameter("@Year_Name", strYearName), New SqlParameter("@End_Date1", strEndDate1), New SqlParameter("@End_Date2", strEndDate2), New SqlParameter("@End_Date3", strEndDate3), New SqlParameter("@End_Date4", strEndDate4), New SqlParameter("@End_Date5", strEndDate5), New SqlParameter("@End_Date6", strEndDate6), New SqlParameter("@End_Date7", strEndDate7), New SqlParameter("@End_Date8", strEndDate8), New SqlParameter("@End_Date9", strEndDate9), New SqlParameter("@End_Date10", strEndDate10), New SqlParameter("@End_Date11", strEndDate11), New SqlParameter("@End_Date12", strEndDate12), New SqlParameter("@Due_Date1", strDue_date1), New SqlParameter("@Due_Date2", strDue_date2), New SqlParameter("@Due_Date3", strDue_date3), New SqlParameter("@Due_Date4", strDue_date4), New SqlParameter("@Due_Date5", strDue_date5), New SqlParameter("@Due_Date6", strDue_date6), New SqlParameter("@Due_Date7", strDue_date7), New SqlParameter("@Due_Date8", strDue_date8), New SqlParameter("@Due_Date9", strDue_date9), New SqlParameter("@Due_Date10", strDue_date10), New SqlParameter("@Due_Date11", strDue_date11), New SqlParameter("@Due_Date12", strDue_date12), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", companyCode))
                    End If
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()

                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub
    'It Is Used To Close The Current Windows Form
    Private Sub RadMenuItemClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemClose.Click
        Me.Close()
    End Sub
    'It Is Used To Give The Authority To User,To Access This Form.(It Is Bassed On Mapping)
    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "FIN_YEAR"
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
    '            rbtnSave.Enabled = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            rbtnDelete.Enabled = False
    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception

    '    End Try
    'End Function

   
 


End Class

