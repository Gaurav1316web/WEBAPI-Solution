Imports common
Imports System.Data
Imports XpertERPEngine
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Public Class frmEmployeeOTEntry
    Inherits FrmMainTranScreen

#Region "Variables"
    Private isNewEntry As Boolean = True
    Private isInsideLoadData As Boolean = False
    Private isCellValueChangedOpen As Boolean = False
    Const colOTDate As String = "colOTDate"
    Const colEmpCode As String = "colEmpCode"
    Const colPayPeriod As String = "colPayPeriod"
    Const ColEmpName As String = "ColEmpName"
    Const ColOTType As String = "ColOTType"
    Const ColOTBasic As String = "ColKM"
    Const ColOTDA As String = "ColOTDA"
    Const ColAmount As String = "ColAmount"
    Const ColDiesel As String = "ColDiesel"
    Const ColStation As String = "ColStation"
    Const ColStation2 As String = "ColStation2"
    Const ColStation3 As String = "ColStation3"
    Const ColStation4 As String = "ColStation4"
    Const ColOTHours As String = "ColOTHours"
    Const ColGPSKM As String = "ColGPSKM"
    Dim TotalAmount As Decimal = 0
    Dim TotalDiesel As Decimal = 0
    Dim TotalQuantity As Decimal = 0
    Dim TotalBMCQuantity As Decimal = 0
    Dim Total_Toll_Tax As Decimal = 0
    Dim Total_Ice_Charge As Decimal = 0
    Dim Total_BMC_TOTAL As Decimal = 0
    Dim Total_fat_snf_shortage As Decimal = 0
    Dim Total_Amount As Decimal = 0
    Public EnableOnPrivateChkbox As Boolean = False
    Public tripValue As String = ""
    Dim totalDays As Integer

#End Region

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub frmEmployeeOTEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        RadPageView1.SelectedPage = RadPageViewPage1
        LoadBlankGrid()
        AddNew()
        ReStoreGridLayout()
        tablecreate()

    End Sub

    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtDocNo.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function
    Sub tablecreate()
        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)
        coll.Add("Document_Code", "varchar(30) NOT NULL PRIMARY KEY")
        coll.Add("Document_Date", "datetime NOT NULL")
        coll.Add("Status", "integer null")
        coll.Add("PAY_PERIOD_CODE", "VARCHAR(30) NOT NULL REFERENCES TSPL_PAYPERIOD_MASTER(PAY_PERIOD_CODE) ")
        coll.Add("Created_By", "varchar(12)  NOT NULL")
        coll.Add("Created_Date", "datetime  NOT NULL")
        coll.Add("Modify_By", "varchar(12)  NOT NULL")
        coll.Add("Modify_Date", "datetime NOT NULL")
        coll.Add("Posted_By", "varchar(12) NULL")
        coll.Add("Posted_Date", "datetime null")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_EMPLOYEE_OT_ENTRY_HEAD", coll, Nothing, True, False, "", "Document_Code", "Document_Date", True)

        coll = New Dictionary(Of String, String)()
        coll.Add("PK_ID", "integer NOT NULL identity NOT FOR REPLICATION PRIMARY KEY")
        coll.Add("Document_Code", "varchar(30) NOT NULL References TSPL_EMPLOYEE_OT_ENTRY_HEAD(Document_Code)")
        coll.Add("EMP_CODE", "VARCHAR(12) NOT NULL REFERENCES TSPL_EMPLOYEE_MASTER(EMP_CODE)")
        coll.Add("OT_DATE", "datetime NOT NULL")
        coll.Add("PAY_PERIOD_CODE", "VARCHAR(30) NOT NULL REFERENCES TSPL_PAYPERIOD_MASTER(PAY_PERIOD_CODE) ")
        coll.Add("OT_TYPE", "varchar(40)  NOT NULL")
        coll.Add("OT_HOURS", "decimal (18,2) NULL")
        coll.Add("OT_BASIC", "decimal (18,2) NULL")
        coll.Add("OT_DA", "decimal (18,2) NULL") ' add gps km column
        coll.Add("Amount", "decimal (18,2) NULL")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_EMPLOYEE_OT_ENTRY_DETAIL", coll, Nothing, True, False, "TSPL_EMPLOYEE_OT_ENTRY_HEAD", "Document_Code", "", True)

    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(MyBase.Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
    Sub AddNew()
        BlankAllControls()
        LoadBlankGrid()
        ReStoreGridLayout()

        Dim Qry As String = ""
        If rbtnSingle.IsChecked Then
            gv1.Rows(0).Cells(ColOTType).Value = "Single"
        Else
            gv1.Rows(0).Cells(ColOTType).Value = "Double"
        End If
        btnSave.Text = "Save"
        'Qry = " Select Payment_Code from TSPL_PAYMENT_CODE  WHERE IsDefault=1 "
        'Dim DT As DataTable = clsDBFuncationality.GetDataTable(Qry)
        'Dim paymentcode As String = Nothing
        'If DT.Rows.Count > 0 Then
        '    paymentcode = clsCommon.myCstr(DT.Rows(0).Item("Payment_Code"))
        'End If
        'MasterTemplate.Rows(0).Cells("gvPaymentMode").Value = paymentcode
    End Sub

    Sub BlankAllControls()
        txtDocNo.Value = ""
        txtPayPeriod.Value = ""
        lblPayPeriodDesc.Text = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        'gv1.DataSource = Nothing
        isNewEntry = True
        gv1.Rows.Clear()

        gv1.SummaryRowsBottom.Clear()
        UsLock1.Status = ERPTransactionStatus.Pending
    End Sub
    Sub LoadBlankGrid()
        Dim qry As String = String.Empty
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoEmpCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoEmpCode.FormatString = ""
        repoEmpCode.HeaderText = "Employee Code"
        repoEmpCode.Name = colEmpCode
        repoEmpCode.Width = 150
        repoEmpCode.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoEmpCode)

        Dim repoEmpName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoEmpName.FormatString = ""
        repoEmpName.HeaderText = "Employee Name"
        repoEmpName.Name = ColEmpName
        repoEmpName.Width = 150
        repoEmpName.IsVisible = True
        repoEmpName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoEmpName)

        Dim repoOTDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoOTDate.Format = DateTimePickerFormat.Custom
        'repoDate.CustomFormat = "dd-MM-yyyy"
        repoOTDate.CustomFormat = "dd/MMM/YYYY"
        repoOTDate.HeaderText = "OT Date"
        repoOTDate.WrapText = True
        repoOTDate.FormatString = "{0:d}"
        repoOTDate.Name = colOTDate
        'repoDate.ReadOnly = True
        repoOTDate.IsVisible = True
        repoOTDate.Width = 150
        gv1.MasterTemplate.Columns.Add(repoOTDate)

        Dim repoPayCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPayCode.FormatString = ""
        repoPayCode.HeaderText = "Pay Period"
        repoPayCode.Name = colPayPeriod
        repoPayCode.Width = 150
        repoPayCode.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoPayCode)

        Dim repoOTType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoOTType.FormatString = ""
        repoOTType.HeaderText = "OT Type"
        repoOTType.Name = ColOTType
        repoOTType.DataSource = GetItemType()
        repoOTType.ValueMember = "Code"
        repoOTType.DisplayMember = "Code"
        'PaymentMode.DisplayMember = "RTGS"
        repoOTType.Width = 100
        repoOTType.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoOTType)

        Dim repoOTHours As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoOTHours.FormatString = ""
        repoOTHours.HeaderText = "OT Hours"
        repoOTHours.Name = ColOTHours
        repoOTHours.Width = 150
        repoOTHours.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoOTHours)

        Dim repoOTBasic As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoOTBasic = New GridViewDecimalColumn()
        repoOTBasic.FormatString = ""
        repoOTBasic.HeaderText = "Basic"
        repoOTBasic.WrapText = True
        repoOTBasic.Name = ColOTBasic
        repoOTBasic.Width = 150
        repoOTBasic.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoOTBasic.VisibleInColumnChooser = False
        repoOTBasic.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoOTBasic)

        Dim repoOTDA As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoOTDA = New GridViewDecimalColumn()
        repoOTDA.FormatString = ""
        repoOTDA.HeaderText = "DA"
        repoOTDA.WrapText = True
        repoOTDA.Name = ColOTDA
        repoOTDA.Width = 150
        repoOTDA.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoOTDA.VisibleInColumnChooser = False
        repoOTDA.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoOTDA)

        Dim repoOTAmount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoOTAmount = New GridViewDecimalColumn()
        repoOTAmount.FormatString = ""
        repoOTAmount.HeaderText = "Amount"
        repoOTAmount.WrapText = True
        repoOTAmount.Name = ColAmount
        repoOTAmount.Width = 150
        repoOTAmount.ReadOnly = True
        repoOTAmount.FormatString = "{0:N2}"
        repoOTAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoOTAmount.VisibleInColumnChooser = False

        'repoAmount.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoOTAmount)


        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False

        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.Rows.AddNew()
    End Sub

    Private Function GetItemType() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Double"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Single"
        dt.Rows.Add(dr)

        Return dt
    End Function
    Private Sub txtPayPeriod__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtPayPeriod._MYValidating
        Try
            Dim qry As String = " select TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE as [Code] ,TSPL_PAYPERIOD_MASTER.PAY_PERIOD_NAME as [Pay Period Name] ,TSPL_PAYPERIOD_MASTER.DATE_FROM as [Date From] ,TSPL_PAYPERIOD_MASTER.DATE_TO as [Date To] ,TSPL_PAYPERIOD_MASTER.DESCRIPTION as [Description] ,TSPL_PAYPERIOD_MASTER.POSTED as [Posted] ,TSPL_PAYPERIOD_MASTER.FREEZED as [Freezed] ,TSPL_PAYPERIOD_MASTER.Posting_Date as [Posting Date] ,TSPL_PAYPERIOD_MASTER.Created_By as [Created By] ,TSPL_PAYPERIOD_MASTER.Created_Date as [Created Date] ,TSPL_PAYPERIOD_MASTER.Modified_By as [Modified By] ,TSPL_PAYPERIOD_MASTER.Modified_Date as [Modified Date]  From TSPL_PAYPERIOD_MASTER "
            txtPayPeriod.Value = clsCommon.ShowSelectForm("vbaMccm", qry, "Code", "", txtPayPeriod.Value, "Code", isButtonClicked)
            lblPayPeriodDesc.Text = clsPayPeriodMaster.GetName(txtPayPeriod.Value, Nothing)
            'lblPayPeriodDesc.Name = "CODE"


            Dim qry1 As String = " select TSPL_PAYPERIOD_MASTER.DATE_FROM  From TSPL_PAYPERIOD_MASTER where TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE = '" + txtPayPeriod.Value + "'  "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry1)

            Dim fromdate As Date = clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("DATE_FROM"), "dd/MMM/yyyy"))
            totalDays = Date.DaysInMonth(fromdate.Year, fromdate.Month)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text, MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    If e.Column.Name = colEmpCode Then
                        Dim qry As String = " Select Distinct EMP_CODE as Cust_Code  from TSPL_GENERATE_SALARY_PAYHEADS
	                                        LEFT OUTER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE
	                                         "
                        Dim whrCls As String = " PAY_PERIOD_CODE = '" + txtPayPeriod.Value + "' "
                        gv1.CurrentRow.Cells(0).Value = clsCommon.ShowSelectForm("dfsas", qry, "Cust_Code", "", gv1.CurrentRow.Cells(0).Value)
                        gv1.CurrentRow.Cells(1).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Emp_Name from TSPL_EMPLOYEE_MASTER where EMP_CODE='" + clsCommon.myCstr(gv1.CurrentRow.Cells(0).Value) + "'"))
                    End If

                    If e.Column.Name = colPayPeriod Then
                        Dim qry As String = " Select PAY_PERIOD_CODE from TSPL_PAYPERIOD_MASTER "
                        gv1.CurrentRow.Cells(3).Value = clsCommon.ShowSelectForm("dfsas", qry, "Cust_Code", "", gv1.CurrentRow.Cells(0).Value)
                    End If

                    isCellValueChangedOpen = True
                    UpdateCurrentRow(gv1.CurrentRow.Index, Nothing)
                    'UpdateAllTotals(Nothing)
                End If

            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer, ByVal trans As SqlTransaction)
        Try
            Dim Singles As String = ""
            If rbtnSingle.IsChecked Then
                gv1.CurrentRow.Cells(ColOTType).Value = "Single"
                Singles = "Single"
            Else
                gv1.CurrentRow.Cells(ColOTType).Value = "Double"
                Singles = "Double"
            End If

            Dim Emp_Code As String = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colEmpCode).Value)
            Dim Payperiod As String = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colPayPeriod).Value)
            Dim BasicDA As String = " Select  max(case when PAY_HEAD_CODE='BASIC' then ACTUAL_AMOUNT end) as Basic_Amt,max(Case when PAY_HEAD_CODE='DA' THEN (ACTUAL_AMOUNT) end )as DA_Amt  from TSPL_GENERATE_SALARY_PAYHEADS
	                                 LEFT OUTER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE   
                                    WHERE PAY_PERIOD_CODE = '" + Payperiod + "' and EMP_CODE = '" + Emp_Code + "' and PAY_HEAD_CODE in ('BASIC','DA') group by EMP_CODE  "
            Dim dtbasic As DataTable = clsDBFuncationality.GetDataTable(BasicDA)
            If dtbasic.Rows.Count > 0 Then
                For ii = 0 To dtbasic.Rows.Count - 1
                    gv1.CurrentRow.Cells(ColOTBasic).Value = clsCommon.myCstr(dtbasic.Rows(0)("Basic_Amt"))
                    gv1.CurrentRow.Cells(ColOTDA).Value = clsCommon.myCstr(dtbasic.Rows(0)("DA_Amt"))
                Next
            End If

            Dim OTBasic As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(ColOTBasic).Value)
            Dim OTDA As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(ColOTDA).Value)
            'Dim TYpe As String = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(ColOTType).Value)
            Dim TYpe As String = Singles

            Dim OTTypeValue As Double = 0

            If TYpe = "Double" Then
                OTTypeValue = 2
            Else
                OTTypeValue = 1
            End If
            Dim dblBasicAmt As Double = 0

            Dim Qrydays As String = " Select DATE_FROM,DATE_TO from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE = '" + Payperiod + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qrydays)

            'If dt.Rows.Count > 0 Then
            Dim fromdate As Date = clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("DATE_FROM"), "dd/MMM/yyyy"))
            Dim Todate As Date = clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("DATE_TO"), "dd/MMM/yyyy"))
            ' 🧮 Total Days (Inclusive)
            Dim totalDays As Integer = DateDiff(DateInterval.Day, fromdate, Todate) + 1

            dblBasicAmt = ((OTBasic + OTDA) / totalDays) * OTTypeValue

            gv1.Rows(IntRowNo).Cells(ColAmount).Value = clsCommon.myCdbl(dblBasicAmt)
            isCellValueChangedOpen = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        Try
            If gv1.RowCount > 0 Then
                Dim intCurrRow As Integer = gv1.CurrentRow.Index
                If intCurrRow = gv1.Rows.Count - 1 Then
                    gv1.Rows.AddNew()
                    gv1.CurrentRow = gv1.Rows(intCurrRow)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_CellValidated(sender As Object, e As CellValidatedEventArgs) Handles gv1.CellValidated
        Try
            SetGridFocus()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFocus()
        If gv1.CurrentCell IsNot Nothing AndAlso gv1.Rows.Count > 0 Then
            Dim setnxtRow As Boolean = False
            If gv1.CurrentCell.ColumnInfo.Name = ColOTHours Then
                gv1.CurrentColumn = gv1.Columns(ColAmount)
                'ElseIf gv1.CurrentCell.ColumnInfo.Name = ColAmount Then
                setnxtRow = True
                gv1.CurrentColumn = gv1.Columns(colEmpCode)
            End If

            If setnxtRow Then
                Dim nextRowIndex As Integer = gv1.CurrentRow.Index + 1
                If nextRowIndex < gv1.Rows.Count Then
                    gv1.CurrentRow = gv1.Rows(nextRowIndex)
                    gv1.CurrentColumn = gv1.Columns(colEmpCode)
                End If
            End If
        End If

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveData(False)
    End Sub

    Function AllowToSave() As Boolean
        Try
            Xtra.TransactionValidity(txtDate.Value)

            For Each grow As GridViewRowInfo In gv1.Rows
                Dim HoursOT As Decimal = 0
                HoursOT = clsCommon.myCdbl(grow.Cells(ColOTHours).Value)
                If HoursOT Mod 4 <> 0 Then
                    clsCommon.MyMessageBoxShow(Me, " OT Hours must be in multiples of 4 ", Me.Text)
                    'MessageBox.Show("OT Hours must be in multiples of 4.", "Validation Error")
                    Return False
                End If
            Next

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
    End Function
    Sub SaveData(ByVal isPost As Boolean)
        Try
            If (AllowToSave()) Then
                Dim obj As New ClsEmpOTEntry()
                obj.Document_Code = txtDocNo.Value
                obj.Document_Date = txtDate.Value
                obj.Pay_Period_Code = txtPayPeriod.Value

                obj.Arr = New List(Of ClsEmpOTEntryDetail)
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New ClsEmpOTEntryDetail()
                    objTr.Emp_Code = clsCommon.myCstr(grow.Cells(colEmpCode).Value)
                    objTr.Emp_Name = clsCommon.myCstr(grow.Cells(ColEmpName).Value)
                    objTr.OT_Date = clsCommon.myCDate(grow.Cells(colOTDate).Value)
                    objTr.Pay_Period_Code = clsCommon.myCstr(grow.Cells(colPayPeriod).Value)
                    objTr.OT_Type = clsCommon.myCstr(grow.Cells(ColOTType).Value)
                    objTr.OT_Hours = clsCommon.myCdbl(grow.Cells(ColOTHours).Value)
                    objTr.OT_Basic = clsCommon.myCdbl(grow.Cells(ColOTBasic).Value)
                    objTr.OT_DA = clsCommon.myCdbl(grow.Cells(ColOTDA).Value)
                    objTr.Amount = clsCommon.myCdbl(grow.Cells(ColAmount).Value)

                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colEmpCode).Value)) > 0 Then
                        obj.Arr.Add(objTr)
                    End If
                Next

                If (obj.SaveData(obj, isNewEntry)) Then
                    If Not isPost Then
                        common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    End If
                    LoadData(obj.Document_Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strDocumentNo As String, NavType As common.NavigatorType)
        Try
            btnSave.Enabled = True
            btnPost.Enabled = True
            btnDelete.Enabled = True
            btnSave.Text = "Update"
            txtDocNo.MyReadOnly = True
            isInsideLoadData = True
            BlankAllControls()
            LoadBlankGrid()
            gv1.Rows.Clear()
            isNewEntry = False

            Dim obj As New ClsEmpOTEntry()
            obj = ClsEmpOTEntry.GetData(strDocumentNo, NavType, True, Nothing)

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0) Then
                isNewEntry = False
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                End If

                UsLock1.Status = obj.Status
                txtDocNo.Value = obj.Document_Code
                txtDate.Value = obj.Document_Date
                txtPayPeriod.Value = obj.Pay_Period_Code
                'Dim qry1 As String = clsDBFuncationality.getSingleValue(" Select PAY_PERIOD_NAME from TSPL_PAYPERIOD_MASTER ='" + txtPayPeriod.Value + "'")
                lblPayPeriodDesc.Text = clsDBFuncationality.getSingleValue(" Select PAY_PERIOD_NAME from TSPL_PAYPERIOD_MASTER where TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE ='" + txtPayPeriod.Value + "'")

                If obj.Arr IsNot Nothing Then
                    Dim i As Integer = 0

                    For Each objrow As ClsEmpOTEntryDetail In obj.Arr

                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colEmpCode).Value = objrow.Emp_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColEmpName).Value = clsDBFuncationality.getSingleValue(" Select Emp_Name from TSPL_EMPLOYEE_MASTER where TSPL_EMPLOYEE_MASTER.EMP_CODE ='" + objrow.Emp_Code + "'")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColOTType).Value = objrow.OT_Type
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colOTDate).Value = objrow.OT_Date
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPayPeriod).Value = objrow.Pay_Period_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColAmount).Value = objrow.Amount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColOTHours).Value = objrow.OT_Hours
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColOTBasic).Value = objrow.OT_Basic
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColOTDA).Value = objrow.OT_DA
                    Next
                End If
            End If
            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If clsCancelLog.CheckForReasonOnDelete() Then
                    '' REASON FOR DELETE 
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Delete"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If
                End If
            End If
            If (ClsEmpOTEntry.DeleteData(txtDocNo.Value)) Then
                saveCancelLog(Reason, "Delete", Nothing)
                common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                AddNew()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            Dim msg As String = ""
            Dim qry As String = ""
            Dim dt As DataTable = Nothing
            If (myMessages.postConfirm()) Then
                If Not AllowToSave() Then
                    Exit Sub
                End If
                'SaveData(True)
                If (ClsEmpOTEntry.PostData(txtDocNo.Value)) Then
                    msg = "Successfully Posted"
                End If
                common.clsCommon.MyMessageBoxShow(Me, msg, Me.Text)
                LoadData(txtDocNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Try

            Dim qry As String = "select TSPL_EMPLOYEE_OT_ENTRY_HEAD.Document_Code ,convert(varchar,TSPL_EMPLOYEE_OT_ENTRY_HEAD.Document_date,103) as Document_date,TSPL_EMPLOYEE_OT_ENTRY_HEAD.PAY_PERIOD_CODE,case when TSPL_EMPLOYEE_OT_ENTRY_HEAD.status =1  then 'Approved' else 'Pending' end as Status   
                             from TSPL_EMPLOYEE_OT_ENTRY_HEAD "
            txtDocNo.Value = clsCommon.ShowSelectForm("fmGroup_Code", qry, "Document_Code", "", txtDocNo.Value, "", isButtonClicked)
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                LoadData(txtDocNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocNo._MYNavigator
        Try

            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    'Sub AddNew()
    '    BlankAllControls()
    '    LoadBlankGrid()
    '    ReStoreGridLayout()
    '    btnSave.Text = "Save"
    'End Sub
End Class