Imports System.Data.SqlClient
Imports common
Imports Telerik.WinControls.UI
Imports XpertERPEngine

Public Class frmEmployeeOTCalculation
    Inherits FrmMainTranScreen

#Region "Variables"
    Private isNewEntry As Boolean = True
    Private isInsideLoadData As Boolean = False
    Private isCellValueChangedOpen As Boolean = False
    Const colOTDate As String = "colOTDate"
    Const colPKID As String = "colPKID"
    Const colEmpCode As String = "colEmpCode"
    Const ColEmpName As String = "ColEmpName"
    Const colEmpCode2 As String = "colEmpCode"
    Const ColEmpName2 As String = "ColEmpName"
    Const ColOTType As String = "ColOTType"
    Const ColOTBasic As String = "ColKM"
    Const ColOTIncrementBasic As String = "ColIncBasic"
    Const ColOTIncrementDA As String = "ColIncDA"
    Const ColOTDA As String = "ColOTDA"
    Const ColAmount As String = "ColAmount"
    Const ColAmount2 As String = "ColAmount"
    Const ColDiesel As String = "ColDiesel"
    Const ColStation As String = "ColStation"
    Const ColStation2 As String = "ColStation2"
    Const ColStation3 As String = "ColStation3"
    Const ColStation4 As String = "ColStation4"
    Const ColOTHours As String = "ColOTHours"
    Const ColOTHours2 As String = "ColOTHours"
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
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub

    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtDocNo.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function
    Private Sub frmEmployeeOTCalculation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        RadPageView1.SelectedPage = RadPageViewPage3
        LoadBlankGrid()
        LoadBlankGrid2()
        AddNew()
        'RadGroupBox5.Enabled = False
        ReStoreGridLayout()
        tablecreate()
    End Sub

    Sub tablecreate()
        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)
        coll.Add("Document_Code", "varchar(30) NOT NULL PRIMARY KEY")
        coll.Add("Document_Date", "datetime NOT NULL")
        coll.Add("Status", "integer null")
        coll.Add("Remarks", "varchar(300) NULL")
        coll.Add("From_Date", "datetime NOT NULL")
        coll.Add("To_Date", "datetime NOT NULL")
        'coll.Add("PAY_PERIOD_CODE", "VARCHAR(30) NOT NULL REFERENCES TSPL_PAYPERIOD_MASTER(PAY_PERIOD_CODE) ")
        coll.Add("Created_By", "varchar(12)  NOT NULL")
        coll.Add("Created_Date", "datetime  NOT NULL")
        coll.Add("Modify_By", "varchar(12)  NOT NULL")
        coll.Add("Modify_Date", "datetime NOT NULL")
        coll.Add("Posted_By", "varchar(12) NULL")
        coll.Add("Posted_Date", "datetime null")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_EMPLOYEE_OT_CALCULATION_HEAD", coll, Nothing, True, False, "", "Document_Code", "Document_Date", True)

        coll = New Dictionary(Of String, String)()
        coll.Add("PK_ID", "integer NOT NULL identity NOT FOR REPLICATION PRIMARY KEY")
        coll.Add("REF_PK_ID", "Integer NOT NULL References TSPL_EMPLOYEE_OT_ENTRY_DETAIL(PK_ID)")
        coll.Add("Document_Code", "varchar(30) NOT NULL References TSPL_EMPLOYEE_OT_CALCULATION_HEAD(Document_Code)")
        coll.Add("Increment_Basic", "decimal (18,2) NULL")
        coll.Add("Increment_DA", "decimal (18,2) NULL")
        coll.Add("Amount", "decimal (18,2) NULL")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_EMPLOYEE_OT_CALCULATION_DETAILS", coll, Nothing, True, False, "TSPL_EMPLOYEE_OT_CALCULATION_HEAD", "Document_Code", "", True)

        coll = New Dictionary(Of String, String)()
        coll.Add("Document_Code", "varchar(30) NOT NULL References TSPL_EMPLOYEE_OT_CALCULATION_HEAD(Document_Code)")
        coll.Add("EMP_CODE", "VARCHAR(12) NOT NULL REFERENCES TSPL_EMPLOYEE_MASTER(EMP_CODE)")
        coll.Add("OT_HOURS", "decimal (18,2) NULL")
        coll.Add("Amount", "decimal (18,2) NULL")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_EMPLOYEE_OT_CALCULATION_SUMMARY", coll, Nothing, True, False, "TSPL_EMPLOYEE_OT_CALCULATION_HEAD", "Document_Code", "", True)

    End Sub
    Sub BlankAllControls()
        txtDocNo.Value = ""
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtRemarks.Text = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        'gv1.DataSource = Nothing
        isNewEntry = True
        gv1.Rows.Clear()

        gv1.SummaryRowsBottom.Clear()
        UsLock1.Status = ERPTransactionStatus.Pending
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

    Private Sub btnAddnew_Click(sender As Object, e As EventArgs) Handles btnAddnew.Click
        AddNew()
    End Sub
    Sub AddNew()
        BlankAllControls()
        LoadBlankGrid()
        LoadBlankGrid2()
        ReStoreGridLayout()

        btnSave.Text = "Save"
    End Sub

    Sub LoadBlankGrid2()
        Dim qry As String = String.Empty
        gv2.Rows.Clear()
        gv2.Columns.Clear()

        Dim repoPKID As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPKID.FormatString = ""
        repoPKID.HeaderText = "PK ID"
        repoPKID.Name = colPKID
        repoPKID.Width = 150
        repoPKID.IsVisible = True
        repoPKID.ReadOnly = True
        gv2.MasterTemplate.Columns.Add(repoPKID)

        Dim repoEmpCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoEmpCode.FormatString = ""
        repoEmpCode.HeaderText = "Employee Code"
        repoEmpCode.Name = colEmpCode2
        repoEmpCode.Width = 150
        repoEmpCode.IsVisible = True
        repoEmpCode.ReadOnly = True
        gv2.MasterTemplate.Columns.Add(repoEmpCode)

        Dim repoEmpName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoEmpName.FormatString = ""
        repoEmpName.HeaderText = "Employee Name"
        repoEmpName.Name = ColEmpName2
        repoEmpName.Width = 150
        repoEmpName.IsVisible = True
        repoEmpName.ReadOnly = True
        gv2.MasterTemplate.Columns.Add(repoEmpName)

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
        repoOTDate.ReadOnly = True
        repoOTDate.Width = 150
        gv2.MasterTemplate.Columns.Add(repoOTDate)

        Dim repoOTType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoOTType.FormatString = ""
        repoOTType.HeaderText = "OT Type"
        repoOTType.Name = ColOTType
        'repoOTType.DataSource = GetItemType()
        'repoOTType.ValueMember = "Code"
        'repoOTType.DisplayMember = "Code"
        'PaymentMode.DisplayMember = "RTGS"
        repoOTType.Width = 100
        repoOTType.IsVisible = True
        repoOTType.ReadOnly = True
        gv2.MasterTemplate.Columns.Add(repoOTType)

        Dim repoOTHours As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoOTHours.FormatString = ""
        repoOTHours.HeaderText = "OT Hours"
        repoOTHours.Name = ColOTHours2
        repoOTHours.Width = 150
        repoOTHours.IsVisible = True
        repoOTHours.ReadOnly = True
        gv2.MasterTemplate.Columns.Add(repoOTHours)

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
        gv2.MasterTemplate.Columns.Add(repoOTBasic)

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
        gv2.MasterTemplate.Columns.Add(repoOTDA)

        Dim repoOTIncrementBasic As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoOTIncrementBasic = New GridViewDecimalColumn()
        repoOTIncrementBasic.FormatString = ""
        repoOTIncrementBasic.HeaderText = "Increment Basic"
        repoOTIncrementBasic.WrapText = True
        repoOTIncrementBasic.Name = ColOTIncrementBasic
        repoOTIncrementBasic.Width = 150
        repoOTIncrementBasic.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoOTIncrementBasic.VisibleInColumnChooser = False
        repoOTIncrementBasic.ReadOnly = False
        gv2.MasterTemplate.Columns.Add(repoOTIncrementBasic)

        Dim repoOTIncrementDA As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoOTIncrementDA = New GridViewDecimalColumn()
        repoOTIncrementDA.FormatString = ""
        repoOTIncrementDA.HeaderText = "Increment DA"
        repoOTIncrementDA.WrapText = True
        repoOTIncrementDA.Name = ColOTIncrementDA
        repoOTIncrementDA.Width = 150
        repoOTIncrementDA.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoOTIncrementDA.VisibleInColumnChooser = False
        repoOTIncrementDA.ReadOnly = False
        gv2.MasterTemplate.Columns.Add(repoOTIncrementDA)

        Dim repoOTAmount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoOTAmount = New GridViewDecimalColumn()
        repoOTAmount.FormatString = ""
        repoOTAmount.HeaderText = "Amount"
        repoOTAmount.WrapText = True
        repoOTAmount.Name = ColAmount2
        repoOTAmount.Width = 150
        repoOTAmount.ReadOnly = True
        repoOTAmount.FormatString = "{0:N2}"
        repoOTAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoOTAmount.VisibleInColumnChooser = False
        gv2.MasterTemplate.Columns.Add(repoOTAmount)

        gv2.AllowDeleteRow = True
        gv2.AllowAddNewRow = False
        gv2.ShowGroupPanel = False
        gv2.AllowColumnReorder = False
        gv2.AllowRowReorder = False
        gv2.EnableSorting = False

        gv2.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv2.MasterTemplate.ShowRowHeaderColumn = False
        gv2.TableElement.TableHeaderHeight = 40
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

        Dim repoOTHours As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoOTHours.FormatString = ""
        repoOTHours.HeaderText = "OT Hours"
        repoOTHours.Name = ColOTHours
        repoOTHours.Width = 150
        repoOTHours.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoOTHours)

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
        'gv1.Rows.AddNew()
    End Sub

    Sub loadGridData()
        Try
            Dim Slot1 As String = ""
            Dim Slot2 As String = ""
            Slot1 = clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy")
            Slot2 = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")
            Dim qry As String = ""
            qry = " Select PAY_PERIOD_CODE from TSPL_PAYPERIOD_MASTER where CONVERT(DATE, TSPL_PAYPERIOD_MASTER.DATE_FROM, 103) >= CONVERT(DATE, '" + Slot1 + "', 103) and
                                  CONVERT(DATE, TSPL_PAYPERIOD_MASTER.DATE_TO, 103) <= CONVERT(DATE, '" + Slot2 + "', 103)"
            Dim dt12 As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim codes As New List(Of String)

            For Each row As DataRow In dt12.Rows
                codes.Add("'" & row("PAY_PERIOD_CODE").ToString() & "'")
            Next
            Dim finalString As String = "(" & String.Join(",", codes.ToArray()) & ")"

            Dim qry1 As String = ""
            qry1 = " Select TSPL_EMPLOYEE_OT_ENTRY_HEAD.Document_Code from TSPL_EMPLOYEE_OT_ENTRY_HEAD WHERE TSPL_EMPLOYEE_OT_ENTRY_HEAD.PAY_PERIOD_CODE In " + finalString + " "
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry1)

            Dim codes1 As New List(Of String)

            For Each row As DataRow In dt1.Rows
                codes1.Add("'" & row("Document_Code").ToString() & "'")
            Next
            Dim finalString1 As String = "(" & String.Join(",", codes1.ToArray()) & ")"

            Dim qry2 As String = ""
            qry2 = " Select TSPL_EMPLOYEE_OT_ENTRY_DETAIL.PK_ID,TSPL_EMPLOYEE_OT_ENTRY_DETAIL.EMP_CODE,TSPL_EMPLOYEE_OT_ENTRY_DETAIL.OT_DATE,TSPL_EMPLOYEE_OT_ENTRY_DETAIL.OT_TYPE,TSPL_EMPLOYEE_OT_ENTRY_DETAIL.OT_HOURS,TSPL_EMPLOYEE_OT_ENTRY_DETAIL.OT_BASIC,
                        TSPL_EMPLOYEE_OT_ENTRY_DETAIL.OT_DA,TSPL_EMPLOYEE_OT_ENTRY_DETAIL.Amount from TSPL_EMPLOYEE_OT_ENTRY_DETAIL where TSPL_EMPLOYEE_OT_ENTRY_DETAIL.Document_Code In " + finalString1 + " order by EMP_CODE  "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry2)

            Dim qry3 As String = ""
            qry3 = " Select TSPL_EMPLOYEE_OT_ENTRY_DETAIL.EMP_CODE,sum(TSPL_EMPLOYEE_OT_ENTRY_DETAIL.OT_HOURS)OT_HOURS,sum(TSPL_EMPLOYEE_OT_ENTRY_DETAIL.Amount)Amount from TSPL_EMPLOYEE_OT_ENTRY_DETAIL 
                        where TSPL_EMPLOYEE_OT_ENTRY_DETAIL.Document_Code In " + finalString1 + "   group by EMP_CODE "
            Dim dt3 As DataTable = clsDBFuncationality.GetDataTable(qry3)

            Dim ii As Integer = 0
            If dt.Rows.Count > 0 Then
                For ii = 0 To dt.Rows.Count - 1
                    gv2.Rows.AddNew()
                    gv2.CurrentRow.Cells(colPKID).Value = clsCommon.myCstr(dt.Rows(ii)("PK_ID"))
                    gv2.CurrentRow.Cells(colEmpCode2).Value = clsCommon.myCstr(dt.Rows(ii)("EMP_CODE"))
                    gv2.CurrentRow.Cells(ColEmpName2).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select TSPL_EMPLOYEE_MASTER.Emp_Name from TSPL_EMPLOYEE_MASTER where TSPL_EMPLOYEE_MASTER.EMP_CODE = '" + gv2.CurrentRow.Cells(colEmpCode2).Value + "'"))
                    gv2.CurrentRow.Cells(colOTDate).Value = clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(ii)("OT_DATE"), "dd/MMM/yyyy"))
                    gv2.CurrentRow.Cells(ColOTType).Value = clsCommon.myCstr(dt.Rows(0)("OT_TYPE"))
                    gv2.CurrentRow.Cells(ColOTHours2).Value = clsCommon.myCdbl(dt.Rows(ii)("OT_HOURS"))
                    gv2.CurrentRow.Cells(ColOTBasic).Value = clsCommon.myCdbl(dt.Rows(ii)("OT_BASIC"))
                    gv2.CurrentRow.Cells(ColOTIncrementBasic).Value = clsCommon.myCdbl(dt.Rows(ii)("OT_BASIC"))
                    gv2.CurrentRow.Cells(ColOTDA).Value = clsCommon.myCdbl(dt.Rows(ii)("OT_DA"))
                    gv2.CurrentRow.Cells(ColOTIncrementDA).Value = clsCommon.myCdbl(dt.Rows(ii)("OT_DA"))
                    gv2.CurrentRow.Cells(ColAmount).Value = clsCommon.myCdbl(clsCommon.myCdbl(dt.Rows(ii)("Amount")))
                Next
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text, MessageBoxButtons.OK)
        End Try
    End Sub

    Sub loadGridDataSummary()
        Try
            Dim strlst As List(Of String) = New List(Of String)
            'Dim ii As Integer = 0
            For Each grow As GridViewRowInfo In gv2.Rows
                Dim Hours As Decimal = 0
                Dim Amount As Decimal = 0
                Dim Emp_Code As String = Nothing
                Dim Emp_Name As String = Nothing


                Dim empcode As String = grow.Cells(colEmpCode).Value
                Dim count As Integer = 0
                If count = 0 Then
                    Emp_Code = clsCommon.myCdbl(grow.Cells(colEmpCode2).Value)
                    Emp_Name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select TSPL_EMPLOYEE_MASTER.Emp_Name from TSPL_EMPLOYEE_MASTER where TSPL_EMPLOYEE_MASTER.EMP_CODE = '" + Emp_Code + "'"))
                    Hours += clsCommon.myCdbl(grow.Cells(ColOTHours2).Value)
                    Amount += clsCommon.myCdbl(grow.Cells(ColAmount2).Value)
                End If

                If strlst.Contains(empcode) Then
                    Hours += clsCommon.myCdbl(grow.Cells(ColOTHours2).Value)
                    Amount += clsCommon.myCdbl(grow.Cells(ColAmount2).Value)
                Else
                    gv1.Rows.AddNew()
                    strlst.Add(empcode)
                End If

                If count = 0 Then
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colEmpCode).Value = Emp_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColEmpName).Value = empcode
                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColOTHours).Value = Hours
                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColAmount).Value = Amount
                End If


                'Dim empcode As String = gv2.row(introw).cells(colEmpCode).value
                'For Each grow As GridViewRowInfo In gv2.Rows
                '    Hours += clsCommon.myCdbl(grow.Cells(ColOTHours2).Value)
                '    Amount += clsCommon.myCdbl(grow.Cells(ColAmount2).Value)
                'Next
            Next


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text, MessageBoxButtons.OK)
        End Try
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        loadGridData()
        'loadGridDataSummary()
    End Sub

    Private Sub FillSummaryGrid()

        'Dim dict As New Dictionary(Of String, Tuple(Of String, Double, Double))
        Dim dict As New Dictionary(Of String, clsempsummary)
        ' Key = EmpCode
        ' Value = (EmpName, TotalHours, TotalAmount)

        For Each row As GridViewRowInfo In gv2.Rows
            ' If row.IsNewRow Then Continue For
            If Not TypeOf row Is GridViewDataRowInfo Then Continue For

            Dim empCode As String = clsCommon.myCstr(row.Cells(colEmpCode2).Value)
            Dim empName As String = clsCommon.myCstr(row.Cells(ColEmpName2).Value)
            Dim hours As Double = clsCommon.myCdbl(row.Cells(ColOTHours2).Value)
            Dim amount As Double = clsCommon.myCdbl(row.Cells(ColAmount2).Value)
            If dict.ContainsKey(empCode) Then
                dict(empCode).TotalHours += hours
                dict(empCode).TotalAmount += amount
            Else
                dict.Add(empCode, New clsempsummary With {
                .EmpName = empName,
                .TotalHours = hours,
                .TotalAmount = amount
            })
            End If
        Next

        ' Clear Grid1
        gv1.Rows.Clear()

        For Each kvp In dict
            gv1.Rows.Add(
                kvp.Key,
                kvp.Value.EmpName,
                kvp.Value.TotalHours,
                kvp.Value.TotalAmount
            )
        Next


    End Sub
    Private Sub gv2_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv2.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    If e.Column.Name = ColOTIncrementBasic OrElse e.Column.Name = ColOTIncrementDA Then
                        isCellValueChangedOpen = True
                        UpdateCurrentRow(gv2.CurrentRow.Index, Nothing)
                        FillSummaryGrid()
                        'UpdateAllTotals(gv2.CurrentRow.Index, Nothing)
                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text, MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer, ByVal trans As SqlTransaction)
        Try

            Dim OTTypeValue As Double = 0
            If gv2.CurrentRow.Cells(ColOTType).Value = "Single" Then
                OTTypeValue = 1
            Else
                OTTypeValue = 2
            End If
            Dim OTBasic As Double = clsCommon.myCdbl(gv2.Rows(IntRowNo).Cells(ColOTIncrementBasic).Value)
            Dim PKID As Integer = (gv2.Rows(IntRowNo).Cells(colPKID).Value)
            Dim OTDA As Double = clsCommon.myCdbl(gv2.Rows(IntRowNo).Cells(ColOTIncrementDA).Value)

            Dim dblBasicAmt As Double = 0
            'Dim qry As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select TSPL_EMPLOYEE_OT_ENTRY_DETAIL.Document_Code from TSPL_EMPLOYEE_OT_ENTRY_DETAIL where TSPL_EMPLOYEE_OT_ENTRY_DETAIL.PK_ID = '" + clsCommon.myCstr(PKID) + "' "))

            Dim QryPayperiod As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  Select TSPL_EMPLOYEE_OT_ENTRY_HEAD.PAY_PERIOD_CODE from TSPL_EMPLOYEE_OT_ENTRY_DETAIL 
				   left outer join TSPL_EMPLOYEE_OT_ENTRY_HEAD on TSPL_EMPLOYEE_OT_ENTRY_HEAD.Document_Code=TSPL_EMPLOYEE_OT_ENTRY_DETAIL.Document_Code
				   where TSPL_EMPLOYEE_OT_ENTRY_DETAIL.PK_ID ='" + clsCommon.myCstr(PKID) + "' "))

            Dim Qrydays As String = " Select DATE_FROM,DATE_TO from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE = '" + QryPayperiod + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qrydays)

            'If dt.Rows.Count > 0 Then
            Dim fromdate As Date = clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("DATE_FROM"), "dd/MMM/yyyy"))
            Dim Todate As Date = clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("DATE_TO"), "dd/MMM/yyyy"))
            ' 🧮 Total Days (Inclusive)
            Dim totalDays As Integer = DateDiff(DateInterval.Day, fromdate, Todate) + 1

            'End If
            'totalDays = 31

            dblBasicAmt = ((OTBasic + OTDA) / totalDays) * OTTypeValue

            gv2.Rows(IntRowNo).Cells(ColAmount2).Value = clsCommon.myCdbl(dblBasicAmt)

            isCellValueChangedOpen = False
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text, MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub UpdateAllTotals(ByVal IntRowNo As Integer, ByVal trans As SqlTransaction)
        Try
            Dim Hours As Decimal = 0
            Dim Amount As Decimal = 0
            Dim strlst As List(Of String) = New List(Of String)

            Dim empcode As String = gv2.Rows(IntRowNo).Cells(colEmpCode).Value
            Dim count As Integer = 0
            If count = 0 Then
                'Hours += clsCommon.myCdbl(gv2.Rows(IntRowNo).Cells(ColOTHours2).Value)
                Amount += clsCommon.myCdbl(gv2.Rows(IntRowNo).Cells(ColAmount2).Value)
            End If
            If strlst.Contains(empcode) Then
                'Hours += clsCommon.myCdbl(gv2.Rows(IntRowNo).Cells(ColOTHours2).Value)
                Amount += clsCommon.myCdbl(gv2.Rows(IntRowNo).Cells(ColAmount2).Value)
            Else
                gv1.Rows.AddNew()
                strlst.Add(empcode)
            End If

            'gv1.Rows(gv1.Rows.Count - 1).Cells(colEmpCode).Value = empcode
            'gv1.Rows(gv1.Rows.Count - 1).Cells(ColEmpName).Value = empcode
            gv1.Rows(gv1.Rows.Count - 1).Cells(ColOTHours).Value = Hours
            gv1.Rows(gv1.Rows.Count - 1).Cells(ColAmount).Value = Amount
            'Dim empcode As String = gv2.row(introw).cells(colEmpCode).value
            'For Each grow As GridViewRowInfo In gv2.Rows
            '    Hours += clsCommon.myCdbl(grow.Cells(ColOTHours2).Value)
            '    Amount += clsCommon.myCdbl(grow.Cells(ColAmount2).Value)
            'Next
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text, MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveData(False)
    End Sub

    Function AllowToSave() As Boolean
        Try
            Xtra.TransactionValidity(txtdate.Value)

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
    End Function
    Sub SaveData(ByVal isPost As Boolean)
        Try
            If (AllowToSave()) Then
                Dim obj As New ClsEmpOTCalculation()
                obj.Document_Code = txtDocNo.Value
                obj.Document_Date = txtdate.Value
                obj.Remarks = txtRemarks.Text
                obj.From_Date = txtFromDate.Value
                obj.To_Date = txtToDate.Value

                obj.Arr_Calc = New List(Of ClsEmpOTEntryDetailCalculation)
                obj.Arr = New List(Of ClsEmpOTEntryDetailCalculationData)

                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New ClsEmpOTEntryDetailCalculationData()
                    objTr.Emp_Code = clsCommon.myCstr(grow.Cells(colEmpCode).Value)
                    objTr.OT_Hours = clsCommon.myCdbl(grow.Cells(ColOTHours).Value)
                    objTr.Amount = clsCommon.myCdbl(grow.Cells(ColAmount).Value)

                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colEmpCode).Value)) > 0 Then
                        obj.Arr.Add(objTr)
                    End If
                Next

                For Each grow As GridViewRowInfo In gv2.Rows
                    Dim objTrC As New ClsEmpOTEntryDetailCalculation()
                    objTrC.PK_ID = clsCommon.myCstr(grow.Cells(colPKID).Value)
                    objTrC.OT_Incremented_Basic = clsCommon.myCdbl(grow.Cells(ColOTIncrementBasic).Value)
                    objTrC.OT_Incremented_DA = clsCommon.myCdbl(grow.Cells(ColOTIncrementDA).Value)
                    objTrC.Amount = clsCommon.myCdbl(grow.Cells(ColAmount2).Value)

                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colEmpCode).Value)) > 0 Then
                        obj.Arr_Calc.Add(objTrC)
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
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text, MessageBoxButtons.OK)
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
            LoadBlankGrid2()
            gv1.Rows.Clear()
            gv2.Rows.Clear()
            isNewEntry = False

            Dim obj As New ClsEmpOTCalculation()
            obj = ClsEmpOTCalculation.GetData(strDocumentNo, NavType, True, Nothing)

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0) Then
                isNewEntry = False
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                End If

                USLock1.Status = obj.Status
                txtDocNo.Value = obj.Document_Code
                txtdate.Value = obj.Document_Date
                txtRemarks.Text = obj.Remarks
                txtFromDate.Value = obj.From_Date
                txtToDate.Value = obj.To_Date

                If obj.Arr IsNot Nothing Then
                    Dim i As Integer = 0

                    For Each objrow As ClsEmpOTEntryDetailCalculationData In obj.Arr

                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colEmpCode).Value = objrow.Emp_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColEmpName).Value = clsDBFuncationality.getSingleValue(" Select Emp_Name from TSPL_EMPLOYEE_MASTER where TSPL_EMPLOYEE_MASTER.EMP_CODE ='" + objrow.Emp_Code + "'")
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(ColOTType).Value = objrow.OT_Type
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colOTDate).Value = objrow.OT_Date
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColAmount).Value = objrow.Amount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColOTHours).Value = objrow.OT_Hours
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(ColOTBasic).Value = objrow.OT_Basic
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(ColOTDA).Value = objrow.OT_DA
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(ColOTHours).Value = objrow.
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(ColOTHours).Value = objrow.OT_Hours
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(ColOTHours).Value = objrow.OT_Hours
                    Next
                End If

                If obj.Arr_Calc IsNot Nothing Then
                    Dim i As Integer = 0

                    For Each objrow As ClsEmpOTEntryDetailCalculation In obj.Arr_Calc

                        gv2.Rows.AddNew()
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colPKID).Value = objrow.PK_ID
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colEmpCode).Value = objrow.Emp_Code
                        gv2.Rows(gv2.Rows.Count - 1).Cells(ColEmpName).Value = clsDBFuncationality.getSingleValue(" Select Emp_Name from TSPL_EMPLOYEE_MASTER where TSPL_EMPLOYEE_MASTER.EMP_CODE ='" + objrow.Emp_Code + "'")
                        gv2.Rows(gv2.Rows.Count - 1).Cells(ColOTType).Value = objrow.OT_Type
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colOTDate).Value = objrow.OT_Date
                        gv2.Rows(gv2.Rows.Count - 1).Cells(ColAmount2).Value = objrow.Amount
                        gv2.Rows(gv2.Rows.Count - 1).Cells(ColOTHours2).Value = objrow.OT_Hours
                        gv2.Rows(gv2.Rows.Count - 1).Cells(ColOTBasic).Value = objrow.OT_Basic
                        gv2.Rows(gv2.Rows.Count - 1).Cells(ColOTDA).Value = objrow.OT_DA
                        gv2.Rows(gv2.Rows.Count - 1).Cells(ColOTIncrementBasic).Value = objrow.OT_Incremented_Basic
                        gv2.Rows(gv2.Rows.Count - 1).Cells(ColOTIncrementDA).Value = objrow.OT_Incremented_DA
                    Next
                End If
            End If
            ReStoreGridLayout()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text, MessageBoxButtons.OK)
        Finally
            isInsideLoadData = False
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
            If (ClsEmpOTCalculation.DeleteData(txtDocNo.Value)) Then
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
                If (ClsEmpOTCalculation.PostData(txtDocNo.Value)) Then
                    msg = "Successfully Posted"
                End If
                common.clsCommon.MyMessageBoxShow(Me, msg, Me.Text)
                LoadData(txtDocNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnreverse_Click(sender As Object, e As EventArgs) Handles btnreverse.Click
        Try
            If common.clsCommon.MyMessageBoxShow("Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If ClsEmpOTCalculation.ReverseAndUnpost(txtDocNo.Value) Then
                    common.clsCommon.MyMessageBoxShow("Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Try

            Dim qry As String = "select TSPL_EMPLOYEE_OT_CALCULATION_HEAD.Document_Code ,convert(varchar,TSPL_EMPLOYEE_OT_CALCULATION_HEAD.Document_date,103) as Document_date,TSPL_EMPLOYEE_OT_CALCULATION_HEAD.Remarks,case when TSPL_EMPLOYEE_OT_CALCULATION_HEAD.status =1  then 'Approved' else 'Pending' end as Status   
                             from TSPL_EMPLOYEE_OT_CALCULATION_HEAD "
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

End Class