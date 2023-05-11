Imports common
Imports System.Data.SqlClient

Public Class frmIncetiveEntry
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim ButtonTooltip As New ToolTip()
    Dim DtIncentive As DataTable
    Dim Formcode As String
    Dim Is_Load As Boolean = False
    Dim AllowDateChanged As Boolean = False
   
    Dim IsNewEntry As Boolean = False
    Dim isInsideLoadData As Boolean = False
    Const colInvSNo As String = "colInvSNo"
    Const colInvVSPCode As String = "colInvVSPCode"
    Const colInvVSPName As String = "colInvVSPName"
    Const colInvInvoiceNo As String = "colInvInvoiceNo"
    Const colInvAmount As String = "colInvAmount"

    Const colVSPCode As String = "colVSPCode"
    Const colVSPName As String = "colVSPName"
    Const colPaymentCycleAmount1 As String = "colPaymentCycleAmount1"
    Const colPaymentCycleAmount2 As String = "colPaymentCycleAmount2"
    Const colPaymentCycleAmount3 As String = "colPaymentCycleAmount3"
    Const colPaymentCycleAmount4 As String = "colPaymentCycleAmount4"
    Const colPaymentCycleAmount5 As String = "colPaymentCycleAmount5"
    Const colPaymentCycleAmount6 As String = "colPaymentCycleAmount6"
    Const colPaymentCycleAmount7 As String = "colPaymentCycleAmount7"
    Const colPaymentCycleAmount8 As String = "colPaymentCycleAmount8"
    Const colPaymentCycleAmount9 As String = "colPaymentCycleAmount9"
    Const colPaymentCycleAmount10 As String = "colPaymentCycleAmount10"
    Const colAmount As String = "colAmount"


#End Region

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmMilkVSPPayment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Is_Load = True
        ButtonTooltip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction")
        ButtonTooltip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonTooltip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
        ButtonTooltip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonTooltip.SetToolTip(btnnew, "Press Alt+N Adding New Trasnaction")

        SetUserMgmtNew()
        txtMonth.Value = clsCommon.GETSERVERDATE()
        Is_Load = False
        AllowDateChanged = True
        RadPageView1.SelectedPage = RadPageViewPage4
        AddNew()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub FrmMilkVSPPayment_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            DeleteData()
        End If
    End Sub

    Private Sub DtpDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMonth.ValueChanged
        Try
            AllowDateChanged = False
            txtFromDate.MinDate = "01-Jan-0001"
            txtFromDate.MaxDate = Date.DaysInMonth(txtMonth.Value.Year, txtMonth.Value.Month) & "-" & txtMonth.Value.Month & "-" & txtMonth.Value.Year
            txtFromDate.MinDate = "01-" & txtMonth.Value.Month & "-" & txtMonth.Value.Year
            txtToDate.Value = Date.DaysInMonth(txtMonth.Value.Year, txtMonth.Value.Month) & "-" & txtMonth.Value.Month & "-" & txtMonth.Value.Year
            AllowDateChanged = True
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DtpFromDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFromDate.ValueChanged
        SetToDate()
    End Sub

    Private Sub txtNoOfPaymentCycle_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtNoOfPaymentCycle.Validating
        Try
            If txtNoOfPaymentCycle.Value < 1 Or txtNoOfPaymentCycle.Value > 10 Then
                Throw New Exception("No of Payment Cycle should be 1-10")
            End If
            SetToDate()
            SetIncentiveColumns()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetToDate()
        Try
            If AllowDateChanged Then
                If Is_Load = False Then
                    If clsCommon.myLen(txtMCC.Value) <= 0 Then
                        txtMCC.Focus()
                        Throw New Exception("Please select Mcc First.")
                    End If
                End If

                Dim sQuery As String = "select Pc_Type as Type,PC_VALUE as Value, case when Pc_Type='Day' then PC_VALUE when PC_Type='Month' then PC_Value * " & Date.DaysInMonth(txtMonth.Value.Year, txtMonth.Value.Month) & " end " _
              & " as Pc_Value from tspl_Mcc_master inner join TSPL_PAYMENT_CYCLE_MASTER  on tspl_Mcc_master.payment_cycle=TSPL_PAYMENT_CYCLE_MASTER.PC_CODE where Mcc_code='" & txtMCC.Value & "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(sQuery)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    Throw New Exception("Please set payment cycle in Mcc master")
                End If
                lblPaymentType.Text = clsCommon.myCstr(dt.Rows(0)("Type"))
                lblPaymentType.Tag = clsCommon.myCdbl(dt.Rows(0)("Value"))
                If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Type")), "Week") = CompairStringResult.Equal Then
                    AllowDateChanged = False
                    txtMonth.Enabled = False
                    txtFromDate.MinDate = New Date(2000, 1, 1)
                    txtFromDate.MaxDate = New Date(3000, 1, 1).AddDays(-1)
                    Dim today As Date = txtFromDate.Value
                    Dim dayDiff As Integer = today.DayOfWeek - IIf(lblPaymentType.Tag = 1, DayOfWeek.Sunday, IIf(lblPaymentType.Tag = 2, DayOfWeek.Monday, IIf(lblPaymentType.Tag = 3, DayOfWeek.Tuesday, IIf(lblPaymentType.Tag = 4, DayOfWeek.Wednesday, IIf(lblPaymentType.Tag = 5, DayOfWeek.Thursday, IIf(lblPaymentType.Tag = 6, DayOfWeek.Friday, DayOfWeek.Saturday))))))
                    txtFromDate.Value = today.AddDays(-dayDiff)
                    txtToDate.Value = txtFromDate.Value.AddDays(6 * txtNoOfPaymentCycle.Value)
                    AllowDateChanged = True
                Else
                    txtMonth.Enabled = True
                    Dim PaymentCycleValue As Integer = dt.Rows(0)("Pc_Value")
                    If txtFromDate.Value.Day Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                        AllowDateChanged = False
                        clsCommon.MyMessageBoxShow("Invalid date.Date should be multiple of " & clsCommon.myCstr(PaymentCycleValue) & " + 1 ")
                        txtFromDate.Value = txtFromDate.MinDate
                        txtFromDate.Text = txtFromDate.MinDate
                        AllowDateChanged = True
                    End If
                    txtToDate.Value = txtFromDate.Value.AddDays((PaymentCycleValue * txtNoOfPaymentCycle.Value) - 1)
                    If txtFromDate.Value.Month <> txtToDate.Value.Month Then
                        txtToDate.Value = New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                    End If
                    Dim dtNxtPay As DateTime = txtToDate.Value.AddDays(Math.Ceiling(PaymentCycleValue / 2.0))
                    If txtFromDate.Value.Month <> dtNxtPay.Month Then
                        txtToDate.Value = New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                    End If
                End If
                gv1.DataSource = Nothing
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtMCC__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtMCC._MYValidating
        Dim qry As String = ""
        Dim arrLoc As String = ""
        Dim obj As New clsMCCCodes()
        obj = clsMCCCodes.GetData(True)
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
            arrLoc = obj.arrLocCodes
        End If

        qry = "select * from ( select Mcc_Code as [Code],MCC_Name as [Name] from tspl_mcc_master inner join tspl_location_master on location_Code= mcc_Code " _
        & " and (loc_segment_Code in (" & arrLoc & ") or mcc_Code in (" & arrLoc & ")))xx "

        txtMCC.Value = clsCommon.ShowSelectForm("VSPPMCCInEn", qry, "Code", "", txtMCC.Value, "", isButtonClicked)
        lblMCC.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MCC_Name from tspl_mcc_master where mcc_Code='" + txtMCC.Value + "'"))
        SetToDate()
        gv1.DataSource = Nothing
    End Sub

    Private Sub txtVSP__My_Click(sender As Object, e As EventArgs) Handles txtVSP._My_Click
        Try
            If clsCommon.myLen(txtMCC.Value) <= 0 Then
                txtMCC.Focus()
                Throw New Exception("Please select MCC")
            End If

            Dim qry As String = "select xx.VSP_CODE as Code,TSPL_VENDOR_MASTER.Vendor_Name as Name from (" + Environment.NewLine +
            " select VSP_CODE from TSPL_MILK_PURCHASE_INVOICE_HEAD where  MCC_CODE= '" + txtMCC.Value + "' and  TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' " + Environment.NewLine + _
            " and TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' " + Environment.NewLine + _
            " group by VSP_CODE " + Environment.NewLine + _
            " )xx " + Environment.NewLine +
            " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=xx.VSP_CODE " + Environment.NewLine +
            " order by xx.VSP_CODE"
            txtVSP.arrValueMember = clsCommon.ShowMultipleSelectForm(False, "PPfPVie", qry, "Code", "", txtVSP.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtFromDate_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtFromDate.Validating
        SetToDate()
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub RadButton2_Click(sender As Object, e As EventArgs) Handles RadButton2.Click
        EnableDisableFilter(True)
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        FillData()

    End Sub

    Sub SetIncentiveColumns()
        For ii As Integer = 1 To 10
            If ii <= txtNoOfPaymentCycle.Value Then
                gv1.Columns("colPaymentCycleAmount" + clsCommon.myCstr(ii)).IsVisible = True
            Else
                gv1.Columns("colPaymentCycleAmount" + clsCommon.myCstr(ii)).IsVisible = False
            End If
        Next
    End Sub

    Sub EnableDisableFilter(ByVal isEnable As Boolean)
        txtMCC.Enabled = isEnable
        txtMonth.Enabled = isEnable
        txtFromDate.Enabled = isEnable
        txtNoOfPaymentCycle.Enabled = isEnable
        txtToDate.Enabled = isEnable
        txtVSP.Enabled = isEnable
    End Sub

    Sub LoadBlankGridInvoice()
        gvInvoice.Rows.Clear()
        gvInvoice.Columns.Clear()

        Dim repoDecimal As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "SNo"
        repoDecimal.Name = colInvSNo
        repoDecimal.WrapText = True
        repoDecimal.Minimum = 0
        repoDecimal.Width = 100
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDecimal.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(repoDecimal)

        Dim repoString As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "VSP Code"
        repoString.Name = colInvVSPCode
        repoString.WrapText = True
        repoString.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoString.TextImageRelation = TextImageRelation.TextBeforeImage
        repoString.Width = 150
        repoString.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "VSP Name"
        repoString.Name = colInvVSPName
        repoString.WrapText = True
        repoString.Width = 150
        repoString.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Invoice No"
        repoString.WrapText = True
        repoString.Name = colInvInvoiceNo
        repoString.Width = 150
        repoString.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(repoString)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Incentive Amount"
        repoDecimal.Name = colInvAmount
        repoDecimal.WrapText = True
        repoDecimal.Minimum = 0
        repoDecimal.Width = 100
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDecimal.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(repoDecimal)

        gvInvoice.AllowAddNewRow = False
        gvInvoice.ShowGroupPanel = False
        gvInvoice.AllowColumnReorder = True
        gvInvoice.AllowRowReorder = False
        gvInvoice.EnableSorting = False
        gvInvoice.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvInvoice.MasterTemplate.ShowRowHeaderColumn = False
        gvInvoice.TableElement.TableHeaderHeight = 40
        gvInvoice.EnableFiltering = True
        gvInvoice.AllowDeleteRow = False

    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoString As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "VSP Code"
        repoString.WrapText = True
        repoString.Name = colVSPCode
        repoString.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoString.TextImageRelation = TextImageRelation.TextBeforeImage
        repoString.Width = 150
        repoString.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "VSP Name"
        repoString.WrapText = True
        repoString.Name = colVSPName
        repoString.Width = 150
        repoString.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoString)

        Dim repoDecimal As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "1 Payment Cycle Incetive Amount"
        repoDecimal.Name = colPaymentCycleAmount1
        repoDecimal.WrapText = True
        repoDecimal.Minimum = 0
        repoDecimal.Width = 100
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDecimal.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDecimal)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "2 Payment Cycle Incetive Amount"
        repoDecimal.Name = colPaymentCycleAmount2
        repoDecimal.WrapText = True
        repoDecimal.Minimum = 0
        repoDecimal.Width = 100
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDecimal.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDecimal)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "3 Payment Cycle Incetive Amount"
        repoDecimal.WrapText = True
        repoDecimal.Name = colPaymentCycleAmount3
        repoDecimal.Minimum = 0
        repoDecimal.Width = 100
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDecimal.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDecimal)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "4 Payment Cycle Incetive Amount"
        repoDecimal.WrapText = True
        repoDecimal.Name = colPaymentCycleAmount4
        repoDecimal.Minimum = 0
        repoDecimal.Width = 100
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDecimal.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDecimal)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "5 Payment Cycle Incetive Amount"
        repoDecimal.WrapText = True
        repoDecimal.Name = colPaymentCycleAmount5
        repoDecimal.Minimum = 0
        repoDecimal.Width = 100
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDecimal.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDecimal)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "6 Payment Cycle Incetive Amount"
        repoDecimal.WrapText = True
        repoDecimal.Name = colPaymentCycleAmount6
        repoDecimal.Minimum = 0
        repoDecimal.Width = 100
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDecimal.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDecimal)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "7 Payment Cycle Incetive Amount"
        repoDecimal.WrapText = True
        repoDecimal.Name = colPaymentCycleAmount7
        repoDecimal.Minimum = 0
        repoDecimal.Width = 100
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight

        repoDecimal.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDecimal)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "8 Payment Cycle Incetive Amount"
        repoDecimal.Name = colPaymentCycleAmount8
        repoDecimal.WrapText = True
        repoDecimal.Minimum = 0
        repoDecimal.Width = 100
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDecimal.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDecimal)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "9 Payment Cycle Incetive Amount"
        repoDecimal.Name = colPaymentCycleAmount9
        repoDecimal.WrapText = True
        repoDecimal.Minimum = 0
        repoDecimal.Width = 100
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDecimal.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDecimal)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "10 Payment Cycle Incetive Amount"
        repoDecimal.Name = colPaymentCycleAmount10
        repoDecimal.WrapText = True
        repoDecimal.Minimum = 0
        repoDecimal.Width = 100
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDecimal.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDecimal)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Incentive Amount"
        repoDecimal.Name = colAmount
        repoDecimal.WrapText = True
        repoDecimal.Minimum = 0
        repoDecimal.Width = 100
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDecimal.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDecimal)

        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.EnableFiltering = True
        gv1.AllowDeleteRow = False
    End Sub

    Sub FillData()
        Try
            If clsCommon.myLen(txtMCC.Value) <= 0 Then
                txtMCC.Focus()
                Throw New Exception("Please select MCC")
            End If
            If txtVSP.arrValueMember Is Nothing OrElse txtVSP.arrValueMember.Count <= 0 Then
                txtVSP.Focus()
                Throw New Exception("Please select VSP")
            End If

            LoadBlankGrid()
            LoadBlankGridInvoice()

            Dim qry As String = "select TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_CODE " + Environment.NewLine + _
            " from TSPL_MILK_PURCHASE_INVOICE_HEAD " + Environment.NewLine + _
            " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE" + Environment.NewLine + _
            " where TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  and TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'" + Environment.NewLine + _
            " and TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_CODE='" + txtMCC.Value + "' and TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE in (" + clsCommon.GetMulcallString(txtVSP.arrValueMember) + ") " + Environment.NewLine + _
            " and not exists (select 1 from TSPL_INCENTIVE_ENTRY_INVOICE_DETAIL where TSPL_INCENTIVE_ENTRY_INVOICE_DETAIL.Milk_Invoice_No= TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE and TSPL_INCENTIVE_ENTRY_INVOICE_DETAIL.Doc_Code not in ('" + txtCode.Value + "'))" + Environment.NewLine + _
            "order by TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim strOldVSP As String = ""
                Dim dtOldDate As Date = txtFromDate.Value.AddDays(-1)
                Dim intPaymentCycle As Integer = 0
                For ii As Integer = 0 To dt.Rows.Count - 1
                    Dim strVSP As String = clsCommon.myCstr(dt.Rows(ii)("VSP_CODE"))
                    Dim dtDate As Date = clsCommon.myCDate(dt.Rows(ii)("DOC_DATE"))
                    ''Calculate incentive Amt
                    Dim FromDate As Date
                    If clsCommon.CompairString(lblPaymentType.Text, "Week") = CompairStringResult.Equal Then
                        FromDate = dtDate.AddDays(-6)
                    Else
                        Dim FirstDays As Integer = Math.Round(dtDate.Day / clsCommon.myCdbl(lblPaymentType.Tag), 0)
                        FirstDays = clsCommon.myCdbl(lblPaymentType.Tag) * (FirstDays - 1) + 1
                        FromDate = New Date(dtDate.Year, dtDate.Month, FirstDays)
                    End If
                    Dim DclIncentiveAmt As Decimal = 0
                    Dim incentive As ArrayList = clsMilkPurchaseInvoiceMCC.LoadDataQuery_For_Incentive(clsCommon.myCstr(dt.Rows(ii)("DOC_CODE")), clsCommon.myCstr(dt.Rows(ii)("VSP_CODE")), clsCommon.myCstr(dt.Rows(ii)("MCC_CODE")), FromDate, dtDate, False, Nothing, (dtDate.Day - FromDate.Day) + 1)
                    If incentive.Count > 0 Then
                        If incentive(1) > 0 Then
                            DclIncentiveAmt = Math.Round(clsCommon.myCdbl(incentive(1)), 2, MidpointRounding.ToEven)
                        End If
                    End If
                    If DclIncentiveAmt <= 0 Then
                        Continue For
                    End If
                    ''End of sCalculate incentive Amt
                    If Not clsCommon.CompairString(strOldVSP, strVSP) = CompairStringResult.Equal Then
                        dtOldDate = txtFromDate.Value.AddDays(-1)
                        intPaymentCycle = 1

                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.RowCount - 1).Cells(colVSPCode).Value = strVSP
                        gv1.Rows(gv1.RowCount - 1).Cells(colVSPName).Value = clsCommon.myCstr(dt.Rows(ii)("Vendor_Name"))

                    End If
                    If dtOldDate.Year = dtDate.Year AndAlso dtOldDate.Month = dtDate.Month AndAlso dtOldDate.Day = dtDate.Day Then
                        Throw New Exception("VSP - " + strVSP + " found above one invoice in same Payment Cycle.")
                    End If
                    If intPaymentCycle > 10 Then
                        Throw New Exception("No of payment cycle can't be more than 10")
                    End If

                    gvInvoice.Rows.AddNew()
                    gvInvoice.Rows(gvInvoice.RowCount - 1).Cells(colInvSNo).Value = gvInvoice.RowCount
                    gvInvoice.Rows(gvInvoice.RowCount - 1).Cells(colInvVSPCode).Value = strVSP
                    gvInvoice.Rows(gvInvoice.RowCount - 1).Cells(colInvVSPName).Value = clsCommon.myCstr(dt.Rows(ii)("Vendor_Name"))
                    gvInvoice.Rows(gvInvoice.RowCount - 1).Cells(colInvInvoiceNo).Value = clsCommon.myCstr(dt.Rows(ii)("DOC_CODE"))
                    'gvInvoice.Rows(gvInvoice.RowCount - 1).Cells(colInvAmount).Value = 100 + gvInvoice.RowCount ''To be calculate
                    gvInvoice.Rows(gvInvoice.RowCount - 1).Cells(colInvAmount).Value = DclIncentiveAmt
                    gv1.Rows(gv1.RowCount - 1).Cells("colPaymentCycleAmount" + clsCommon.myCstr(intPaymentCycle)).Value = clsCommon.myCdbl(gvInvoice.Rows(gvInvoice.RowCount - 1).Cells(colInvAmount).Value)
                    gv1.Rows(gv1.RowCount - 1).Cells(colAmount).Value += clsCommon.myCdbl(gvInvoice.Rows(gvInvoice.RowCount - 1).Cells(colInvAmount).Value)
                    strOldVSP = strVSP
                    dtOldDate = dtDate
                    intPaymentCycle += 1
                Next
                SetIncentiveColumns()
                RadPageView1.SelectedPage = RadPageViewPage4
            Else
                Throw New Exception("No data found")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        AddNew()
    End Sub

    Sub AddNew()
        btnsave.Enabled = True
        btnPost.Enabled = True
        btndelete.Enabled = True

        IsNewEntry = True
        txtCode.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtMCC.Value = ""
        lblMCC.Text = ""
        txtMonth.Value = txtDate.Value
        txtNoOfPaymentCycle.Value = 0
        txtVSP.arrValueMember = Nothing
        UsLock1.Status = ERPTransactionStatus.Pending
        lblPaymentType.Tag = Nothing
        LoadBlankGrid()
        LoadBlankGridInvoice()
    End Sub

    Sub SaveData()
        Try
            Dim obj As clsIncentiveEntryHead = New clsIncentiveEntryHead
            obj.Doc_Code = txtCode.Value
            obj.Doc_Date = txtDate.Value
            obj.MCC_Code = txtMCC.Value
            obj.Filter_Month = txtMonth.Value
            obj.Filter_From_Date = txtFromDate.Value
            obj.Filter_To_Date = txtToDate.Value
            obj.Filter_No_Of_Payment_Cycle = txtNoOfPaymentCycle.Value
            obj.Filter_Payment_Type = lblPaymentType.Text
            obj.Filter_Payment_Value = lblPaymentType.Tag
            obj.arr = New List(Of clsIncentiveEntryDetail)
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim objtr As New clsIncentiveEntryDetail()
                objtr.Vsp_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colVSPCode).Value)
                objtr.Payment_Cycle_Amount_1 = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPaymentCycleAmount1).Value)
                objtr.Payment_Cycle_Amount_2 = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPaymentCycleAmount2).Value)
                objtr.Payment_Cycle_Amount_3 = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPaymentCycleAmount3).Value)
                objtr.Payment_Cycle_Amount_4 = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPaymentCycleAmount4).Value)
                objtr.Payment_Cycle_Amount_5 = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPaymentCycleAmount5).Value)
                objtr.Payment_Cycle_Amount_6 = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPaymentCycleAmount6).Value)
                objtr.Payment_Cycle_Amount_7 = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPaymentCycleAmount7).Value)
                objtr.Payment_Cycle_Amount_8 = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPaymentCycleAmount8).Value)
                objtr.Payment_Cycle_Amount_9 = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPaymentCycleAmount9).Value)
                objtr.Payment_Cycle_Amount_10 = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPaymentCycleAmount10).Value)
                objtr.Amount = clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmount).Value)
                obj.arr.Add(objtr)
            Next
            obj.arrInvoice = New List(Of clsIncentiveEntryInvoiceDetail)
            For ii As Integer = 0 To gvInvoice.Rows.Count - 1
                Dim objtr As New clsIncentiveEntryInvoiceDetail()
                objtr.VSP_CODE = clsCommon.myCstr(gvInvoice.Rows(ii).Cells(colInvVSPCode).Value)
                objtr.Milk_Invoice_No = clsCommon.myCstr(gvInvoice.Rows(ii).Cells(colInvInvoiceNo).Value)
                objtr.Amount = clsCommon.myCdbl(gvInvoice.Rows(ii).Cells(colInvAmount).Value)
                obj.arrInvoice.Add(objtr)
            Next

            obj.SaveData(obj, IsNewEntry)
            clsCommon.MyMessageBoxShow("Data saved successfully", Me.Text)
            LoadData(obj.Doc_Code, NavigatorType.Current)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            AddNew()
            Dim obj As New clsIncentiveEntryHead()
            obj = clsIncentiveEntryHead.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Doc_Code) > 0) Then
                If obj.Is_Post = ERPTransactionStatus.Posted Then
                    btnsave.Enabled = False
                    btnPost.Enabled = False
                    btndelete.Enabled = False
                End If
                IsNewEntry = False
                txtCode.Value = obj.Doc_Code
                txtDate.Value = obj.Doc_Date
                txtMCC.Value = obj.MCC_Code
                lblMCC.Text = obj.MCC_Name
                UsLock1.Status = obj.Is_Post
                txtMonth.Value = obj.Filter_Month
                txtFromDate.Value = obj.Filter_From_Date
                txtToDate.Value = obj.Filter_To_Date
                txtNoOfPaymentCycle.Value = obj.Filter_No_Of_Payment_Cycle
                SetIncentiveColumns()
                lblPaymentType.Text = obj.Filter_Payment_Type
                lblPaymentType.Tag = obj.Filter_Payment_Value
                txtVSP.arrValueMember = obj.arrVSP

                If obj.arr IsNot Nothing AndAlso obj.arr.Count > 0 Then
                    For Each objTr As clsIncentiveEntryDetail In obj.arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colVSPCode).Value = objTr.Vsp_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colVSPName).Value = objTr.Vsp_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPaymentCycleAmount1).Value = objTr.Payment_Cycle_Amount_1
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPaymentCycleAmount2).Value = objTr.Payment_Cycle_Amount_2
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPaymentCycleAmount3).Value = objTr.Payment_Cycle_Amount_3
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPaymentCycleAmount4).Value = objTr.Payment_Cycle_Amount_4
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPaymentCycleAmount5).Value = objTr.Payment_Cycle_Amount_5
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPaymentCycleAmount6).Value = objTr.Payment_Cycle_Amount_6
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPaymentCycleAmount7).Value = objTr.Payment_Cycle_Amount_7
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPaymentCycleAmount8).Value = objTr.Payment_Cycle_Amount_8
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPaymentCycleAmount9).Value = objTr.Payment_Cycle_Amount_9
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPaymentCycleAmount10).Value = objTr.Payment_Cycle_Amount_10
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmount).Value = objTr.Amount
                    Next
                End If
                If obj.arrInvoice IsNot Nothing AndAlso obj.arrInvoice.Count > 0 Then
                    For Each objTr As clsIncentiveEntryInvoiceDetail In obj.arrInvoice
                        gvInvoice.Rows.AddNew()
                        gvInvoice.Rows(gvInvoice.Rows.Count - 1).Cells(colInvSNo).Value = objTr.SNo
                        gvInvoice.Rows(gvInvoice.Rows.Count - 1).Cells(colInvVSPCode).Value = objTr.VSP_CODE
                        gvInvoice.Rows(gvInvoice.Rows.Count - 1).Cells(colInvVSPName).Value = objTr.Vsp_Name
                        gvInvoice.Rows(gvInvoice.Rows.Count - 1).Cells(colInvInvoiceNo).Value = objTr.Milk_Invoice_No
                        gvInvoice.Rows(gvInvoice.Rows.Count - 1).Cells(colInvAmount).Value = objTr.Amount
                    Next
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("No Document found to delete")
            End If

            If (myMessages.deleteConfirm()) Then
                If clsIncentiveEntryHead.DeleteData(txtCode.Value) Then
                    clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    AddNew()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            If (clsCommon.myLen(txtCode.Value) > 0 AndAlso myMessages.postConfirm()) Then
                If (clsIncentiveEntryHead.PostData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Successfully Posted")
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub


    Private Sub txtCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtCode._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_INCENTIVE_ENTRY_HEAD where Doc_Code='" + txtCode.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        Dim qry As String = "select TSPL_INCENTIVE_ENTRY_HEAD.Doc_Code,TSPL_INCENTIVE_ENTRY_HEAD.Doc_Date,TSPL_INCENTIVE_ENTRY_HEAD.MCC_Code,TSPL_MCC_MASTER.MCC_NAME,TSPL_INCENTIVE_ENTRY_HEAD.Filter_From_Date,TSPL_INCENTIVE_ENTRY_HEAD.Filter_To_Date,case when TSPL_INCENTIVE_ENTRY_HEAD.Is_Post=1 then 'Approved' else 'Pending' end as	status " + Environment.NewLine + _
        "from TSPL_INCENTIVE_ENTRY_HEAD" + Environment.NewLine + _
        "left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_INCENTIVE_ENTRY_HEAD.MCC_Code"

        Dim whrClas As String = ""
        LoadData(clsCommon.ShowSelectForm("incenentmf", qry, "Doc_Code", whrClas, txtCode.Value, "", isButtonClicked), NavigatorType.Current)
    End Sub
End Class
