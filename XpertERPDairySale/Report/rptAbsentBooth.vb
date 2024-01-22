Imports common
Imports System.Data.SqlClient

Public Class rptAbsentBooth
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim ButtonTooltip As New ToolTip()
    Dim DtIncentive As DataTable
    Dim Formcode As String
    Dim Is_Load As Boolean = False
    Dim AllowDateChanged As Boolean = False

    Dim IsNewEntry As Boolean = False
    Dim isInsideLoadData As Boolean = False

    Const colCustomerCode As String = "colCustomerCode"
    Const colCustomerName As String = "colCustomerName"
    Const colIncentiveAmount As String = "colIncentiveAmount"
    Const colDeductionCode As String = "colDeductionCode"
    Const colDeductionTRCode As String = "colDeductionTRCode"
    Const colDeductionAmount As String = "colDeductionAmount"
    Const colAmount As String = "colAmount"
    Const colAvgQty As String = "colAvgQty"
    Const colSecuity As String = "colSecuity"
    Const colDues As String = "colDues"
    Const colAdditionalSecurityDepositAmt As String = "colAdditionalSecurityDepositAmt"
    Const colSecurityToBeTaken As String = "colSecurityToBeTaken"
    Const colNetMarginPayable As String = "colNetMarginPayable"

    Const colCIncDateWise As String = "colCIncDateWise"
    Const colCIncCustomerCode As String = "colCIncCustomerCode"
    Const colCIncCustomerName As String = "colCIncCustomerName"
    Const colCIncIncentiveCode As String = "colCIncIncentiveCode"
    Const colCIncIncentiveTRCode As String = "colCIncIncentiveTRCode"
    Const colCIncRangeQty As String = "colCIncRangeQty"
    Const colCIncRangeAvgQty As String = "colRangeAvgQty"
    Const colCIncRangeUOM As String = "colCIncRangeUOM"
    Const colCIncIncentiveQty As String = "colCIncIncentiveQty"
    Const colCIncIncentiveUOM As String = "colCIncIncentiveUOM"
    Const colCIncIncentiveRate As String = "colCIncIncentiveRate"
    Const colCIncIncentiveAmount As String = "colCIncIncentiveAmount"

    Const colCSDateWise As String = "colCSDateWise"
    Const colCSCustomerCode As String = "colCSCustomerCode"
    Const colCSCustomerName As String = "colCSCustomerName"
    Const colCSStructureCode As String = "colCSStructureCode"
    Const colCSStockQty As String = "colCSStockQty"
    Const colCSStockUOM As String = "colCSStockUOM"
    Const colCSRangeQty As String = "colCSRangeQty"
    Const colCSRangeUOM As String = "colCSRangeUOM"
    Const colCSIncentiveQty As String = "colCSIncentiveQty"
    Const colCSIncentiveUOM As String = "colCSIncentiveUOM"
    Const colCSIncentiveCode As String = "colCSIncentiveCode"

    Const colICDateWise As String = "colICDateWise"
    Const colICCustomerCode As String = "colICCustomerCode"
    Const colICCustomerName As String = "colICCustomerName"
    Const colICItemCode As String = "colICItemCode"
    Const colICItemName As String = "colICItemName"
    Const colICStructureCode As String = "colICStructureCode"
    Const colICStockQty As String = "colICStockQty"
    Const colICStockUOM As String = "colICStockUOM"
    Const colICRangeQty As String = "colICRangeQty"
    Const colICRangeUOM As String = "colICRangeUOM"
    Const colICIncentiveQty As String = "colICIncentiveQty"
    Const colICIncentiveUOM As String = "colICIncentiveUOM"

    Const colInvCustCode As String = "colInvCustCode"
    Const colInvCustName As String = "colInvCustName"
    Const colInvItemCode As String = "colInvItemCode"
    Const colInvItemName As String = "colInvItemName"
    Const colInvInvoiceCode As String = "colInvInvoiceCode"
    Const colInvReturnCode As String = "colInvReturnCode"
    Const colInvDocDate As String = "colInvDocDate"
    Const colInvStructureCode As String = "colInvStructureCode"
    Const colInvQty As String = "colInvQty"
    Const colInvUOM As String = "colInvUOM"
    ''ERO/31/10/18-000414 by balwinder on 12/12/2018
    Dim isCellValueChangedOpen As Boolean = False
    Dim SettDayWiseCustomerIncentiveCalculation As Boolean = False
    Dim SettCustomerIncetiveAutoSecuity As Boolean = False

#End Region

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub FrmMilkVSPPayment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SettDayWiseCustomerIncentiveCalculation = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DayWiseCustomerIncentiveCalculation, clsFixedParameterCode.DayWiseCustomerIncentiveCalculation, Nothing)) = 1)
        If SettDayWiseCustomerIncentiveCalculation Then
            RadPageView1.Pages("RadPageViewPage1").Item.Text = "Date," + RadPageView1.Pages("RadPageViewPage1").Item.Text
            RadPageView1.Pages("RadPageViewPage2").Item.Text = "Date," + RadPageView1.Pages("RadPageViewPage2").Item.Text
            RadPageView1.Pages("RadPageViewPage3").Item.Text = "Date," + RadPageView1.Pages("RadPageViewPage3").Item.Text
        End If
        SettCustomerIncetiveAutoSecuity = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CustomerIncetiveAutoSecuity, clsFixedParameterCode.CustomerIncetiveAutoSecuity, Nothing)) = 1)
        HideExtraTab(ElementVisibility.Collapsed)
        AllowDateChanged = True
        RadPageView1.SelectedPage = RadPageViewPage6
        AddNew()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub FrmMilkVSPPayment_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.X Then
            HideExtraTab(ElementVisibility.Visible)
        End If
    End Sub

    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
        Dim qry As String = "select Location_Code as Code,Location_Desc as Name,Loc_Short_Name as [Short Name] from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = "  Location_Type='Physical' and CSA_Type='N' and Is_Section='N' and Is_Sub_Location='N'  "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtLocation.Value = clsCommon.ShowSelectForm("Loc@CusInc", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
        lblLocation.Text = clsLocation.GetName(txtLocation.Value, Nothing)
        gvCustomerStructure.DataSource = Nothing
    End Sub

    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        Try
            If clsCommon.myLen(txtLocation.Value) <= 0 Then
                txtLocation.Focus()
                Throw New Exception("Please select Location")
            End If
            ''ERO/14/11/19-001097 by balwinder on 18/11/2019
            Dim qry As String = " select TSPL_CUSTOMER_MASTER.Cust_Code as Code,TSPL_CUSTOMER_MASTER.Customer_Name as Name,TSPL_CUSTOMER_MASTER.add1 as Address,TSPL_CUSTOMER_MASTER.Zone_Code as [Zone Code],TSPL_ZONE_MASTER.Description as Zone " + Environment.NewLine +
            " from TSPL_CUSTOMER_MASTER" + Environment.NewLine +
            " left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code=TSPL_CUSTOMER_MASTER.Zone_Code"
            txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm(False, "cusInEn@c", qry, "Code", "", txtCustomer.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton2_Click(sender As Object, e As EventArgs) Handles RadButton2.Click
        EnableDisableFilter(True)
        LoadBlankGrid()
        LoadBlankGridCInc()
        LoadBlankGridCS()
        LoadBlankGridIC()
        LoadBlankGridInvoice()
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        FillData()
    End Sub

    Sub EnableDisableFilter(ByVal isEnable As Boolean)
        txtLocation.Enabled = isEnable
        'txtMonth.Enabled = isEnable
        txtCustomer.Enabled = isEnable
        'chkApplyDateRange.Enabled = isEnable
        'Panel4.Enabled = isEnable
        'pnlSecuity.Enabled = isEnable
        txtToDate.Enabled = isEnable
    End Sub

    Sub LoadBlankGridInvoice()
        gvInvoice.Rows.Clear()
        gvInvoice.Columns.Clear()

        Dim repoDecimal As GridViewDecimalColumn = New GridViewDecimalColumn()
        Dim repoString As GridViewTextBoxColumn = New GridViewTextBoxColumn()

        Dim repoDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()



        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Invoice No"
        repoString.WrapText = True
        repoString.Name = colInvInvoiceCode
        repoString.Width = 130
        repoString.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Return No"
        repoString.WrapText = True
        repoString.Name = colInvReturnCode
        repoString.Width = 100
        repoString.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(repoString)

        repoDate = New GridViewDateTimeColumn()
        repoDate.Format = DateTimePickerFormat.Custom
        repoDate.CustomFormat = "dd-MM-yyyy"
        repoDate.HeaderText = "Date"
        repoDate.FormatString = "{0:d}"
        repoDate.Name = colInvDocDate
        repoDate.WrapText = True
        repoDate.ReadOnly = True
        repoDate.IsVisible = SettDayWiseCustomerIncentiveCalculation
        repoDate.Width = 80
        gvInvoice.MasterTemplate.Columns.Add(repoDate)


        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Customer Code"
        repoString.Name = colInvCustCode
        repoString.WrapText = True
        'repoString.HeaderImage = Global.XpertERPDairySale.My.Resources.Resources.search4
        repoString.TextImageRelation = TextImageRelation.TextBeforeImage
        repoString.Width = 100
        repoString.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Customer Name"
        repoString.Name = colInvCustName
        repoString.WrapText = True
        repoString.Width = 300
        repoString.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Item Code"
        repoString.Name = colInvItemCode
        repoString.WrapText = True
        'repoString.HeaderImage = Global.XpertERPDairySale.My.Resources.Resources.search4
        repoString.TextImageRelation = TextImageRelation.TextBeforeImage
        repoString.Width = 100
        repoString.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Item Name"
        repoString.Name = colInvItemName
        repoString.WrapText = True
        repoString.Width = 200
        repoString.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(repoString)



        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Structure Code"
        repoString.WrapText = True
        repoString.Name = colInvStructureCode
        repoString.Width = 100
        repoString.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(repoString)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Qty"
        repoDecimal.Name = colInvQty
        repoDecimal.WrapText = True
        repoDecimal.Minimum = 0
        repoDecimal.Width = 100
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDecimal.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(repoDecimal)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "UOM"
        repoString.WrapText = True
        repoString.Name = colInvUOM
        repoString.Width = 100
        repoString.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(repoString)

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
        gvInvoice.AllowRowResize = False
    End Sub

    Sub LoadBlankGrid()
        gvCustomer.Rows.Clear()
        gvCustomer.Columns.Clear()

        Dim repoString As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Customer Code"
        repoString.WrapText = True
        repoString.Name = colCustomerCode
        'repoString.HeaderImage = Global.XpertERPDairySale.My.Resources.Resources.search4
        repoString.TextImageRelation = TextImageRelation.TextBeforeImage
        repoString.Width = 100
        repoString.ReadOnly = True
        gvCustomer.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Customer Name"
        repoString.WrapText = True
        repoString.Name = colCustomerName
        repoString.Width = 300
        repoString.ReadOnly = True
        gvCustomer.MasterTemplate.Columns.Add(repoString)

        Dim repoDecimal As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Incentive Amount"
        repoDecimal.Name = colIncentiveAmount
        repoDecimal.WrapText = True
        repoDecimal.Minimum = 0
        repoDecimal.Width = 100
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDecimal.ReadOnly = True
        gvCustomer.MasterTemplate.Columns.Add(repoDecimal)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Deduction Code"
        repoString.WrapText = True
        repoString.Name = colDeductionCode
        repoString.IsVisible = False
        repoString.ReadOnly = True
        gvCustomer.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Deduction TR Code"
        repoString.WrapText = True
        repoString.Name = colDeductionTRCode
        repoString.IsVisible = False
        repoString.ReadOnly = True
        gvCustomer.MasterTemplate.Columns.Add(repoString)


        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Deduction Amount"
        repoDecimal.Name = colDeductionAmount
        repoDecimal.WrapText = True
        repoDecimal.Minimum = 0
        repoDecimal.Width = 100
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDecimal.ReadOnly = False
        gvCustomer.MasterTemplate.Columns.Add(repoDecimal)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Amount"
        repoDecimal.Name = colAmount
        repoDecimal.WrapText = True
        repoDecimal.Minimum = 0
        repoDecimal.Width = 100
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDecimal.ReadOnly = True
        gvCustomer.MasterTemplate.Columns.Add(repoDecimal)


        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Avg Qty"
        repoDecimal.Name = colAvgQty
        repoDecimal.WrapText = True
        repoDecimal.Minimum = 0
        repoDecimal.Width = 100
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDecimal.ReadOnly = True
        repoDecimal.IsVisible = SettCustomerIncetiveAutoSecuity
        gvCustomer.MasterTemplate.Columns.Add(repoDecimal)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Secuity"
        repoDecimal.Name = colSecuity
        repoDecimal.WrapText = True
        repoDecimal.Minimum = 0
        repoDecimal.Width = 100
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDecimal.ReadOnly = True
        repoDecimal.IsVisible = SettCustomerIncetiveAutoSecuity
        gvCustomer.MasterTemplate.Columns.Add(repoDecimal)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Dues"
        repoDecimal.Name = colDues
        repoDecimal.WrapText = True
        repoDecimal.Minimum = 0
        repoDecimal.Width = 100
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDecimal.ReadOnly = True
        repoDecimal.IsVisible = SettCustomerIncetiveAutoSecuity
        gvCustomer.MasterTemplate.Columns.Add(repoDecimal)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "AD Secuity Deposit Amt"
        repoDecimal.Name = colAdditionalSecurityDepositAmt
        repoDecimal.WrapText = True
        repoDecimal.Minimum = 0
        repoDecimal.Width = 100
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDecimal.ReadOnly = True
        repoDecimal.IsVisible = SettCustomerIncetiveAutoSecuity
        gvCustomer.MasterTemplate.Columns.Add(repoDecimal)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Security Taken"
        repoDecimal.Name = colSecurityToBeTaken
        repoDecimal.WrapText = True
        repoDecimal.Minimum = 0
        repoDecimal.Width = 100
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDecimal.ReadOnly = True
        repoDecimal.IsVisible = SettCustomerIncetiveAutoSecuity
        gvCustomer.MasterTemplate.Columns.Add(repoDecimal)


        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Net Margin Payable"
        repoDecimal.Name = colNetMarginPayable
        repoDecimal.WrapText = True
        repoDecimal.Minimum = 0
        repoDecimal.Width = 100
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDecimal.ReadOnly = True
        repoDecimal.IsVisible = SettCustomerIncetiveAutoSecuity
        gvCustomer.MasterTemplate.Columns.Add(repoDecimal)

        gvCustomer.AllowAddNewRow = False
        gvCustomer.ShowGroupPanel = False
        gvCustomer.AllowColumnReorder = True
        gvCustomer.AllowRowReorder = False
        gvCustomer.EnableSorting = False
        gvCustomer.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvCustomer.MasterTemplate.ShowRowHeaderColumn = False
        gvCustomer.TableElement.TableHeaderHeight = 40
        gvCustomer.EnableFiltering = True
        gvCustomer.AllowRowResize = False
        gvCustomer.AllowDeleteRow = False
    End Sub

    Sub LoadBlankGridCInc()
        gvCustomerIncentive.Rows.Clear()
        gvCustomerIncentive.Columns.Clear()

        Dim repoDecimal As GridViewDecimalColumn = New GridViewDecimalColumn()
        Dim repoString As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Dim repoDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()

        repoDate = New GridViewDateTimeColumn()
        repoDate.Format = DateTimePickerFormat.Custom
        repoDate.CustomFormat = "dd-MM-yyyy"
        repoDate.HeaderText = "Date Wise"
        repoDate.FormatString = "{0:d}"
        repoDate.Name = colCIncDateWise
        repoDate.WrapText = True
        repoDate.ReadOnly = True
        repoDate.IsVisible = SettDayWiseCustomerIncentiveCalculation
        repoDate.Width = 80
        gvCustomerIncentive.MasterTemplate.Columns.Add(repoDate)



        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Customer Code"
        repoString.Name = colCIncCustomerCode
        repoString.WrapText = True
        repoString.TextImageRelation = TextImageRelation.TextBeforeImage
        repoString.Width = 100
        repoString.ReadOnly = True
        gvCustomerIncentive.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Customer Name"
        repoString.Name = colCIncCustomerName
        repoString.WrapText = True
        repoString.Width = 300
        repoString.ReadOnly = True
        gvCustomerIncentive.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Incentive Code"
        repoString.WrapText = True
        repoString.Name = colCIncIncentiveCode
        repoString.IsVisible = True
        repoString.ReadOnly = True
        repoString.Width = 130
        gvCustomerIncentive.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Incentive TR Code"
        repoString.WrapText = True
        repoString.Name = colCIncIncentiveTRCode
        repoString.IsVisible = False
        repoString.ReadOnly = True
        gvCustomerIncentive.MasterTemplate.Columns.Add(repoString)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Range Qty"
        repoDecimal.Name = colCIncRangeQty
        repoDecimal.WrapText = True
        repoDecimal.Minimum = 0
        repoDecimal.Width = 100
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDecimal.ReadOnly = True
        gvCustomerIncentive.MasterTemplate.Columns.Add(repoDecimal)


        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Avg Qty"
        repoDecimal.Name = colCIncRangeAvgQty
        repoDecimal.WrapText = True
        repoDecimal.Minimum = 0
        repoDecimal.Width = 100
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDecimal.ReadOnly = True
        gvCustomerIncentive.MasterTemplate.Columns.Add(repoDecimal)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Range UOM"
        repoString.WrapText = True
        repoString.Name = colCIncRangeUOM
        repoString.Width = 100
        repoString.ReadOnly = True
        gvCustomerIncentive.MasterTemplate.Columns.Add(repoString)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Incentive Qty"
        repoDecimal.Name = colCIncIncentiveQty
        repoDecimal.WrapText = True
        repoDecimal.Minimum = 0
        repoDecimal.Width = 100
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDecimal.ReadOnly = True
        gvCustomerIncentive.MasterTemplate.Columns.Add(repoDecimal)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Incentive UOM"
        repoString.WrapText = True
        repoString.Name = colCIncIncentiveUOM
        repoString.Width = 100
        repoString.ReadOnly = True
        gvCustomerIncentive.MasterTemplate.Columns.Add(repoString)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Incentive Rate"
        repoDecimal.Name = colCIncIncentiveRate
        repoDecimal.WrapText = True
        repoDecimal.Minimum = 0
        repoDecimal.Width = 100
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDecimal.ReadOnly = True
        gvCustomerIncentive.MasterTemplate.Columns.Add(repoDecimal)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Incentive Amount"
        repoDecimal.Name = colCIncIncentiveAmount
        repoDecimal.WrapText = True
        repoDecimal.Minimum = 0
        repoDecimal.Width = 100
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDecimal.ReadOnly = True
        gvCustomerIncentive.MasterTemplate.Columns.Add(repoDecimal)

        gvCustomerIncentive.AllowAddNewRow = False
        gvCustomerIncentive.ShowGroupPanel = False
        gvCustomerIncentive.AllowColumnReorder = True
        gvCustomerIncentive.AllowRowReorder = False
        gvCustomerIncentive.EnableSorting = False
        gvCustomerIncentive.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvCustomerIncentive.MasterTemplate.ShowRowHeaderColumn = False
        gvCustomerIncentive.TableElement.TableHeaderHeight = 40
        gvCustomerIncentive.EnableFiltering = True
        gvCustomerIncentive.AllowDeleteRow = False
        gvCustomerIncentive.AllowRowResize = False
    End Sub

    Sub LoadBlankGridCS()
        gvCustomerStructure.Rows.Clear()
        gvCustomerStructure.Columns.Clear()

        Dim repoDecimal As GridViewDecimalColumn = New GridViewDecimalColumn()
        Dim repoString As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Dim repoDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()


        repoDate = New GridViewDateTimeColumn()
        repoDate.Format = DateTimePickerFormat.Custom
        repoDate.CustomFormat = "dd-MM-yyyy"
        repoDate.HeaderText = "Date Wise"
        repoDate.FormatString = "{0:d}"
        repoDate.Name = colCSDateWise
        repoDate.WrapText = True
        repoDate.ReadOnly = True
        repoDate.IsVisible = SettDayWiseCustomerIncentiveCalculation
        repoDate.Width = 80
        gvCustomerStructure.MasterTemplate.Columns.Add(repoDate)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Customer Code"
        repoString.Name = colCSCustomerCode
        repoString.WrapText = True
        'repoString.HeaderImage = Global.XpertERPDairySale.My.Resources.Resources.search4
        repoString.TextImageRelation = TextImageRelation.TextBeforeImage
        repoString.Width = 100
        repoString.ReadOnly = True
        gvCustomerStructure.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Customer Name"
        repoString.Name = colCSCustomerName
        repoString.WrapText = True
        repoString.Width = 300
        repoString.ReadOnly = True
        gvCustomerStructure.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Structure Code"
        repoString.WrapText = True
        repoString.Name = colCSStructureCode
        repoString.Width = 100
        repoString.ReadOnly = True
        gvCustomerStructure.MasterTemplate.Columns.Add(repoString)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Stock Qty"
        repoDecimal.Name = colCSStockQty
        repoDecimal.WrapText = True
        repoDecimal.Minimum = 0
        repoDecimal.Width = 100
        'repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDecimal.ReadOnly = True
        gvCustomerStructure.MasterTemplate.Columns.Add(repoDecimal)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Stock UOM"
        repoString.WrapText = True
        repoString.Name = colCSStockUOM
        repoString.Width = 100
        repoString.ReadOnly = True
        gvCustomerStructure.MasterTemplate.Columns.Add(repoString)


        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Range Qty"
        repoDecimal.Name = colCSRangeQty
        repoDecimal.WrapText = True
        repoDecimal.Minimum = 0
        repoDecimal.Width = 100
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDecimal.ReadOnly = True
        gvCustomerStructure.MasterTemplate.Columns.Add(repoDecimal)


        'repoDecimal = New GridViewDecimalColumn()
        'repoDecimal.FormatString = ""
        'repoDecimal.HeaderText = "Avg Qty"
        'repoDecimal.Name = colCSRangeAvgQty
        'repoDecimal.WrapText = True
        'repoDecimal.Minimum = 0
        'repoDecimal.Width = 100
        'repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'repoDecimal.ReadOnly = True
        'gvCustomerStructure.MasterTemplate.Columns.Add(repoDecimal)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Range UOM"
        repoString.WrapText = True
        repoString.Name = colCSRangeUOM
        repoString.Width = 100
        repoString.ReadOnly = True
        gvCustomerStructure.MasterTemplate.Columns.Add(repoString)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Incentive Qty"
        repoDecimal.Name = colCSIncentiveQty
        repoDecimal.WrapText = True
        repoDecimal.Minimum = 0
        repoDecimal.Width = 100
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDecimal.ReadOnly = True
        gvCustomerStructure.MasterTemplate.Columns.Add(repoDecimal)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Incentive UOM"
        repoString.WrapText = True
        repoString.Name = colCSIncentiveUOM
        repoString.Width = 100
        repoString.ReadOnly = True
        gvCustomerStructure.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Incentive Code"
        repoString.WrapText = True
        repoString.Name = colCSIncentiveCode
        repoString.IsVisible = False
        repoString.ReadOnly = True
        gvCustomerStructure.MasterTemplate.Columns.Add(repoString)

        gvCustomerStructure.AllowAddNewRow = False
        gvCustomerStructure.ShowGroupPanel = False
        gvCustomerStructure.AllowColumnReorder = True
        gvCustomerStructure.AllowRowReorder = False
        gvCustomerStructure.EnableSorting = False
        gvCustomerStructure.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvCustomerStructure.MasterTemplate.ShowRowHeaderColumn = False
        gvCustomerStructure.TableElement.TableHeaderHeight = 40
        gvCustomerStructure.EnableFiltering = True
        gvCustomerStructure.AllowDeleteRow = False
        gvCustomerStructure.AllowRowResize = False
    End Sub

    Sub LoadBlankGridIC()
        gvCustomerItem.Rows.Clear()
        gvCustomerItem.Columns.Clear()

        Dim repoDecimal As GridViewDecimalColumn = New GridViewDecimalColumn()
        Dim repoString As GridViewTextBoxColumn = New GridViewTextBoxColumn()

        Dim repoDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()


        repoDate = New GridViewDateTimeColumn()
        repoDate.Format = DateTimePickerFormat.Custom
        repoDate.CustomFormat = "dd-MM-yyyy"
        repoDate.HeaderText = "Date Wise"
        repoDate.FormatString = "{0:d}"
        repoDate.Name = colICDateWise
        repoDate.WrapText = True
        repoDate.ReadOnly = True
        repoDate.IsVisible = SettDayWiseCustomerIncentiveCalculation
        repoDate.Width = 80
        gvCustomerItem.MasterTemplate.Columns.Add(repoDate)



        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Customer Code"
        repoString.Name = colICCustomerCode
        repoString.WrapText = True
        repoString.TextImageRelation = TextImageRelation.TextBeforeImage
        repoString.Width = 100
        repoString.ReadOnly = True
        gvCustomerItem.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Customer Name"
        repoString.Name = colICCustomerName
        repoString.WrapText = True
        repoString.Width = 300
        repoString.ReadOnly = True
        gvCustomerItem.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Item Code"
        repoString.Name = colICItemCode
        repoString.WrapText = True
        repoString.HeaderImage = Global.XpertERPDairySale.My.Resources.Resources.search4
        repoString.TextImageRelation = TextImageRelation.TextBeforeImage
        repoString.Width = 100
        repoString.ReadOnly = True
        gvCustomerItem.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Item Name"
        repoString.Name = colICItemName
        repoString.WrapText = True
        repoString.Width = 200
        repoString.ReadOnly = True
        gvCustomerItem.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Structure Code"
        repoString.WrapText = True
        repoString.Name = colICStructureCode
        repoString.Width = 100
        repoString.ReadOnly = True
        gvCustomerItem.MasterTemplate.Columns.Add(repoString)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Stock Qty"
        repoDecimal.Name = colICStockQty
        repoDecimal.WrapText = True
        repoDecimal.Minimum = 0
        repoDecimal.Width = 100
        'repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDecimal.ReadOnly = True
        gvCustomerItem.MasterTemplate.Columns.Add(repoDecimal)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Stock UOM"
        repoString.WrapText = True
        repoString.Name = colICStockUOM
        repoString.Width = 100
        repoString.ReadOnly = True
        gvCustomerItem.MasterTemplate.Columns.Add(repoString)


        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Range Qty"
        repoDecimal.Name = colICRangeQty
        repoDecimal.WrapText = True
        repoDecimal.Minimum = 0
        repoDecimal.Width = 100
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDecimal.ReadOnly = True
        gvCustomerItem.MasterTemplate.Columns.Add(repoDecimal)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Range UOM"
        repoString.WrapText = True
        repoString.Name = colICRangeUOM
        repoString.Width = 100
        repoString.ReadOnly = True
        gvCustomerItem.MasterTemplate.Columns.Add(repoString)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Incentive Qty"
        repoDecimal.Name = colICIncentiveQty
        repoDecimal.WrapText = True
        repoDecimal.Minimum = 0
        repoDecimal.Width = 100
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDecimal.ReadOnly = True
        gvCustomerItem.MasterTemplate.Columns.Add(repoDecimal)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Incentive UOM"
        repoString.WrapText = True
        repoString.Name = colICIncentiveUOM
        repoString.Width = 100
        repoString.ReadOnly = True
        gvCustomerItem.MasterTemplate.Columns.Add(repoString)

        gvCustomerItem.AllowAddNewRow = False
        gvCustomerItem.ShowGroupPanel = False
        gvCustomerItem.AllowColumnReorder = True
        gvCustomerItem.AllowRowReorder = False
        gvCustomerItem.EnableSorting = False
        gvCustomerItem.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvCustomerItem.MasterTemplate.ShowRowHeaderColumn = False
        gvCustomerItem.TableElement.TableHeaderHeight = 40
        gvCustomerItem.EnableFiltering = True
        gvCustomerItem.AllowDeleteRow = False
        gvCustomerItem.AllowRowResize = False
    End Sub

    Function GetBaseQty(ByVal DocCode As String, ByVal strLocation As String, ByVal dtFrom As DateTime, ByVal dtTo As DateTime, ByVal arrCustomer As ArrayList) As String
        Dim strCustCategoryMappInUserMaster As String = String.Empty
        Dim chkCustCategoryMappInUserMaster As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select count ( distinct CUSTOMER_CATEGORY) as CUSTOMER_CATEGORY from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (select Customer_Category from TSPL_USER_CUSTOMER_CATEGORY where USER_Code = '" + objCommonVar.CurrentUserCode + "')"))
        If chkCustCategoryMappInUserMaster = True Then
            strCustCategoryMappInUserMaster += " and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (  select  distinct CUSTOMER_CATEGORY from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (select Customer_Category from TSPL_USER_CUSTOMER_CATEGORY where USER_Code = '" + objCommonVar.CurrentUserCode + "')) "
        End If

        Dim qry As String = "select TSPL_SD_SALE_INVOICE_DETAIL.Document_Code,convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Document_Date,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Structure_Code,TSPL_SD_SALE_INVOICE_DETAIL.Qty,TSPL_SD_SALE_INVOICE_DETAIL.Unit_code,(TSPL_SD_SALE_INVOICE_DETAIL.Qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as StockQty,StockUOMDetail.UOM_Code as StcokUOM,1 as RI" + Environment.NewLine +
            "from TSPL_SD_SALE_INVOICE_DETAIL" + Environment.NewLine +
            "left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE " + Environment.NewLine +
            "left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.item_code" + Environment.NewLine +
            "left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code" + Environment.NewLine +
            "left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SD_SALE_INVOICE_DETAIL.Unit_code" + Environment.NewLine +
            "left outer join TSPL_ITEM_UOM_DETAIL as StockUOMDetail on StockUOMDetail.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and StockUOMDetail.Stocking_Unit='Y'" + Environment.NewLine +
            "where TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS' and isnull(TSPL_SD_SALE_INVOICE_HEAD.IsSampling,0)=0 and isnull(TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item,'N')='N' and isnull(TSPL_SD_SALE_INVOICE_DETAIL.FOC_Item,0)=0 and TSPL_SD_SALE_INVOICE_HEAD.Document_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFrom), "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_SD_SALE_INVOICE_HEAD.Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtTo), "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location='" + strLocation + "' and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(arrCustomer) + ")  " + strCustCategoryMappInUserMaster + " " + Environment.NewLine +
            "and exists (select 1 from TSPL_ITEM_UOM_DETAIL as innTSPL_ITEM_UOM_DETAIL left outer join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.Unit_Code=innTSPL_ITEM_UOM_DETAIL.UOM_Code where TSPL_UNIT_MASTER.Ltr_Type='Y' and innTSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code) " + Environment.NewLine +
            "and not exists(select 1 from TSPL_CUSTOMER_INCENTIVE_INVOICE_WISE where TSPL_CUSTOMER_INCENTIVE_INVOICE_WISE.Doc_Code not in ('" + DocCode + "') and TSPL_CUSTOMER_INCENTIVE_INVOICE_WISE.Invoice_Code=TSPL_SD_SALE_INVOICE_HEAD.Document_Code)" + Environment.NewLine +
            "union all" + Environment.NewLine +
            "select TSPL_SD_SALE_RETURN_DETAIL.Document_Code,TSPL_SD_SALE_RETURN_HEAD.Document_Date,TSPL_SD_SALE_RETURN_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_SD_SALE_RETURN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Structure_Code,TSPL_SD_SALE_RETURN_DETAIL.Qty,TSPL_SD_SALE_RETURN_DETAIL.Unit_code,(TSPL_SD_SALE_RETURN_DETAIL.Qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as StockQty,StockUOMDetail.UOM_Code as StcokUOM,-1 as RI" + Environment.NewLine +
            "from TSPL_SD_SALE_RETURN_DETAIL" + Environment.NewLine +
            "left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Document_Code=TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE " + Environment.NewLine +
            "left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.item_code" + Environment.NewLine +
            "left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_RETURN_HEAD.Customer_Code" + Environment.NewLine +
            "left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SD_SALE_RETURN_DETAIL.Unit_code" + Environment.NewLine +
            "left outer join TSPL_ITEM_UOM_DETAIL as StockUOMDetail on StockUOMDetail.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code and StockUOMDetail.Stocking_Unit='Y'" + Environment.NewLine +
            "where TSPL_SD_SALE_RETURN_HEAD.Screen_Type='DS' and isnull(TSPL_SD_SALE_RETURN_DETAIL.Scheme_Item,'N')='N' and isnull(TSPL_SD_SALE_RETURN_DETAIL.FOC_Item,0)=0 and TSPL_SD_SALE_RETURN_HEAD.Document_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFrom), "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_SD_SALE_RETURN_HEAD.Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtTo), "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location='" + strLocation + "' and TSPL_SD_SALE_RETURN_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(arrCustomer) + ")  " + strCustCategoryMappInUserMaster + " " + Environment.NewLine +
            "and exists (select 1 from TSPL_ITEM_UOM_DETAIL as innTSPL_ITEM_UOM_DETAIL left outer join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.Unit_Code=innTSPL_ITEM_UOM_DETAIL.UOM_Code where TSPL_UNIT_MASTER.Ltr_Type='Y' and innTSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code) " + Environment.NewLine +
            "and not exists(select 1 from TSPL_CUSTOMER_INCENTIVE_INVOICE_WISE where TSPL_CUSTOMER_INCENTIVE_INVOICE_WISE.Doc_Code not in ('" + DocCode + "') and TSPL_CUSTOMER_INCENTIVE_INVOICE_WISE.Return_Code=TSPL_SD_SALE_RETURN_HEAD.Document_Code)"
        Return qry
    End Function

    Sub FillData()
        clsCommon.ProgressBarPercentShow()
        Try
            If clsCommon.myLen(txtLocation.Value) <= 0 Then
                txtLocation.Focus()
                Throw New Exception("Please select Location")
            End If
            If txtCustomer.arrValueMember Is Nothing OrElse txtCustomer.arrValueMember.Count <= 0 Then
                txtCustomer.Focus()
                Throw New Exception("Please select as least one Customer")
            End If

            Dim dtFrom As DateTime = New Date(txtToDate.Value.Year, txtToDate.Value.Month, 1)
            Dim dtTo As DateTime = txtToDate.Value

            Dim intNoOfDaysInMonth As Integer = (dtTo.Day - dtFrom.Day + 1)
            LoadBlankGrid()
            LoadBlankGridCInc()
            LoadBlankGridCS()
            LoadBlankGridIC()
            LoadBlankGridInvoice()
            Dim ArrValidCustomerItem As New Dictionary(Of String, List(Of String))
            Dim qry As String = "select Customer_Code,max(Customer_Name) as Customer_Name,Item_Code,max(Item_Desc) as Item_Desc,Structure_Code,sum(StockQty*RI) as StockQty,max(StcokUOM) as StcokUOM"
            If SettDayWiseCustomerIncentiveCalculation Then
                qry += ",Document_Date"
                intNoOfDaysInMonth = 1
            End If
            qry += " from (" + GetBaseQty("", txtLocation.Value, dtFrom, dtTo, txtCustomer.arrValueMember) + ")xx group by Customer_Code,Structure_Code,Item_Code"
            If SettDayWiseCustomerIncentiveCalculation Then
                qry += ",Document_Date"
            End If
            qry += " having sum(StockQty*RI)>0  order by Customer_Code,Structure_Code "
            If SettDayWiseCustomerIncentiveCalculation Then
                qry += ",Document_Date"
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim dtInvoiceDate As Date? = Nothing
                Dim strCustCode As String = ""
                Dim strStructreCode As String = ""
                Dim dtSlab As DataTable = Nothing
                Dim TotRangeQty As Decimal = 0
                Dim TotStockQty As Decimal = 0
                Dim TotIncentiveQty As Decimal = 0
                Dim strStockUOM As String = ""
                For ii As Integer = 0 To dt.Rows.Count - 1
                    clsCommon.ProgressBarPercentUpdate(ii * 100 / dt.Rows.Count, "Calculate Incentive " + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                    Dim flag As Boolean = True
                    If SettDayWiseCustomerIncentiveCalculation Then
                        flag = False
                        If dtInvoiceDate IsNot Nothing Then
                            flag = (clsCommon.myCDate(dt.Rows(ii)("Document_Date")) = dtInvoiceDate)
                        End If
                    End If

                    If Not (clsCommon.CompairString(strCustCode, clsCommon.myCstr(dt.Rows(ii)("Customer_Code"))) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strStructreCode, clsCommon.myCstr(dt.Rows(ii)("Structure_Code"))) = CompairStringResult.Equal AndAlso flag) Then
                        If ii > 0 Then
                            AddCSSummaryRow(dt.Rows(ii - 1), dtSlab, TotRangeQty, TotIncentiveQty, intNoOfDaysInMonth, TotStockQty, strStockUOM)
                            TotRangeQty = 0
                            TotIncentiveQty = 0
                            TotStockQty = 0
                            strStockUOM = ""
                            dtInvoiceDate = Nothing
                        End If

                        strCustCode = clsCommon.myCstr(dt.Rows(ii)("Customer_Code"))
                        strStructreCode = clsCommon.myCstr(dt.Rows(ii)("Structure_Code"))
                        If SettDayWiseCustomerIncentiveCalculation Then
                            dtInvoiceDate = clsCommon.myCDate(dt.Rows(ii)("Document_Date"))
                        End If
                        qry = "(select TSPL_SALES_INCENTIVE_SLAB.TR_CODE, TSPL_SALES_INCENTIVE_SLAB.INCENTIVE_CODE,TSPL_SALES_INCENTIVE_SLAB.FROM_RANGE,TSPL_SALES_INCENTIVE_SLAB.TO_RANGE,TSPL_SALES_INCENTIVE_HEADER.RANGE_UOM,TSPL_SALES_INCENTIVE_SLAB.INCENTIVE,TSPL_SALES_INCENTIVE_HEADER.INCENTIVE_UOM,TSPL_SALES_INCENTIVE_ITEM_STRUCTURE_MAPPING.Structure_Code,TSPL_SALES_INCENTIVE_CUSTOMER_MAPPING.CUSTOMER_CODE" + Environment.NewLine +
                        "from TSPL_SALES_INCENTIVE_HEADER" + Environment.NewLine +
                        "left outer join TSPL_SALES_INCENTIVE_SLAB on TSPL_SALES_INCENTIVE_SLAB.INCENTIVE_CODE=TSPL_SALES_INCENTIVE_HEADER.INCENTIVE_CODE" + Environment.NewLine +
                        "left outer join TSPL_SALES_INCENTIVE_CUSTOMER_MAPPING on TSPL_SALES_INCENTIVE_CUSTOMER_MAPPING.INCENTIVE_CODE=TSPL_SALES_INCENTIVE_HEADER.INCENTIVE_CODE" + Environment.NewLine +
                        "left outer join TSPL_SALES_INCENTIVE_ITEM_STRUCTURE_MAPPING on TSPL_SALES_INCENTIVE_ITEM_STRUCTURE_MAPPING.INCENTIVE_CODE=TSPL_SALES_INCENTIVE_HEADER.INCENTIVE_CODE" + Environment.NewLine +
                        "where TSPL_SALES_INCENTIVE_HEADER.Status=1  and TSPL_SALES_INCENTIVE_HEADER.In_Active=0 and TSPL_SALES_INCENTIVE_CUSTOMER_MAPPING.Customer_Code in ('" + strCustCode + "') and TSPL_SALES_INCENTIVE_ITEM_STRUCTURE_MAPPING.Structure_Code in ('" + strStructreCode + "')" + Environment.NewLine +
                        "and '" + clsCommon.GetPrintDate(dtFrom, "dd/MMM/yyyy hh:mm:ss tt") + "' between TSPL_SALES_INCENTIVE_HEADER.FROM_DATE and TSPL_SALES_INCENTIVE_HEADER.TO_DATE)"
                        dtSlab = clsDBFuncationality.GetDataTable(qry)
                    End If
                    If dtSlab IsNot Nothing AndAlso dtSlab.Rows.Count > 0 Then
                        Dim index As Integer = -1
                        For kk As Integer = 0 To dtSlab.Rows.Count - 1
                            If clsCommon.CompairString(strStructreCode, dtSlab.Rows(kk)("Structure_Code")) = CompairStringResult.Equal Then
                                index = kk
                                Exit For
                            End If
                        Next
                        If index > -1 Then
                            TotStockQty += clsCommon.myCdbl(dt.Rows(ii)("StockQty"))
                            strStockUOM = clsCommon.myCstr(dt.Rows(ii)("StcokUOM"))
                            gvCustomerItem.Rows.AddNew()
                            If SettDayWiseCustomerIncentiveCalculation Then
                                gvCustomerItem.Rows(gvCustomerItem.RowCount - 1).Cells(colICDateWise).Value = clsCommon.myCDate(dt.Rows(ii)("Document_Date"))
                            End If
                            gvCustomerItem.Rows(gvCustomerItem.RowCount - 1).Cells(colICCustomerCode).Value = clsCommon.myCstr(dt.Rows(ii)("Customer_Code"))
                            gvCustomerItem.Rows(gvCustomerItem.RowCount - 1).Cells(colICCustomerName).Value = clsCommon.myCstr(dt.Rows(ii)("Customer_Name"))
                            gvCustomerItem.Rows(gvCustomerItem.RowCount - 1).Cells(colICItemCode).Value = clsCommon.myCstr(dt.Rows(ii)("Item_Code"))
                            gvCustomerItem.Rows(gvCustomerItem.RowCount - 1).Cells(colICItemName).Value = clsCommon.myCstr(dt.Rows(ii)("Item_Desc"))
                            gvCustomerItem.Rows(gvCustomerItem.RowCount - 1).Cells(colICStructureCode).Value = strStructreCode
                            gvCustomerItem.Rows(gvCustomerItem.RowCount - 1).Cells(colICStockQty).Value = clsCommon.myCdbl(dt.Rows(ii)("StockQty"))
                            gvCustomerItem.Rows(gvCustomerItem.RowCount - 1).Cells(colICStockUOM).Value = clsCommon.myCstr(dt.Rows(ii)("StcokUOM"))
                            Dim ConvFac As Decimal = clsItemMaster.GetConvertionFactor(clsCommon.myCstr(dt.Rows(ii)("Item_Code")), clsCommon.myCstr(dtSlab.Rows(index)("RANGE_UOM")), Nothing)
                            If ConvFac <> 0 Then
                                gvCustomerItem.Rows(gvCustomerItem.RowCount - 1).Cells(colICRangeQty).Value = Math.Round(clsCommon.myCdbl(dt.Rows(ii)("StockQty")) / ConvFac, 2, MidpointRounding.ToEven)
                                gvCustomerItem.Rows(gvCustomerItem.RowCount - 1).Cells(colICRangeUOM).Value = clsCommon.myCstr(dtSlab.Rows(index)("RANGE_UOM"))
                                TotRangeQty += clsCommon.myCdbl(gvCustomerItem.Rows(gvCustomerItem.RowCount - 1).Cells(colICRangeQty).Value)
                            End If
                            ConvFac = clsItemMaster.GetConvertionFactor(clsCommon.myCstr(dt.Rows(ii)("Item_Code")), clsCommon.myCstr(dtSlab.Rows(index)("INCENTIVE_UOM")), Nothing)
                            If ConvFac <> 0 Then
                                gvCustomerItem.Rows(gvCustomerItem.RowCount - 1).Cells(colICIncentiveQty).Value = Math.Round(clsCommon.myCdbl(dt.Rows(ii)("StockQty")) / ConvFac, 2, MidpointRounding.ToEven)
                                gvCustomerItem.Rows(gvCustomerItem.RowCount - 1).Cells(colICIncentiveUOM).Value = clsCommon.myCstr(dtSlab.Rows(index)("INCENTIVE_UOM"))
                                TotIncentiveQty += clsCommon.myCdbl(gvCustomerItem.Rows(gvCustomerItem.RowCount - 1).Cells(colICIncentiveQty).Value)
                            End If
                        End If
                    End If
                    If ii = dt.Rows.Count - 1 Then
                        AddCSSummaryRow(dt.Rows(ii), dtSlab, TotRangeQty, TotIncentiveQty, intNoOfDaysInMonth, TotStockQty, strStockUOM)
                    End If
                Next

                Dim ArrValidCustomer As New ArrayList
                For ii As Integer = gvCustomerStructure.Rows.Count - 1 To 0 Step -1
                    If clsCommon.myLen(gvCustomerStructure.Rows(ii).Cells(colCSIncentiveCode).Value) > 0 Then
                        If Not ArrValidCustomerItem.ContainsKey(clsCommon.myCstr(gvCustomerStructure.Rows(ii).Cells(colCSCustomerCode).Value)) Then
                            ArrValidCustomerItem.Add(clsCommon.myCstr(gvCustomerStructure.Rows(ii).Cells(colCSCustomerCode).Value), New List(Of String))
                            ArrValidCustomer.Add(clsCommon.myCstr(gvCustomerStructure.Rows(ii).Cells(colCSCustomerCode).Value))
                        End If
                        If Not ArrValidCustomerItem.Item(clsCommon.myCstr(gvCustomerStructure.Rows(ii).Cells(colCSCustomerCode).Value)).Contains(clsCommon.myCstr(gvCustomerStructure.Rows(ii).Cells(colCSStructureCode).Value)) Then
                            ArrValidCustomerItem.Item(clsCommon.myCstr(gvCustomerStructure.Rows(ii).Cells(colCSCustomerCode).Value)).Add(clsCommon.myCstr(gvCustomerStructure.Rows(ii).Cells(colCSStructureCode).Value))
                        End If
                    Else
                        gvCustomerStructure.Rows.RemoveAt(ii)
                    End If
                Next
                If ArrValidCustomerItem IsNot Nothing AndAlso ArrValidCustomerItem.Count > 0 Then
                    Dim Count As Integer = gvCustomerItem.Rows.Count
                    For ii As Integer = gvCustomerItem.Rows.Count - 1 To 0 Step -1
                        clsCommon.ProgressBarPercentUpdate((Count - ii) * 100 / Count, "Removing Customer having no Incentive " + clsCommon.myCstr(ii) + "/" + clsCommon.myCstr(Count))
                        If ArrValidCustomerItem.ContainsKey(clsCommon.myCstr(gvCustomerItem.Rows(ii).Cells(colICCustomerCode).Value)) Then
                            If Not ArrValidCustomerItem.Item(clsCommon.myCstr(gvCustomerItem.Rows(ii).Cells(colICCustomerCode).Value)).Contains(clsCommon.myCstr(gvCustomerItem.Rows(ii).Cells(colICStructureCode).Value)) Then
                                gvCustomerItem.Rows.RemoveAt(ii)
                            End If
                        Else
                            gvCustomerItem.Rows.RemoveAt(ii)
                        End If
                    Next
                    qry = GetBaseQty("", txtLocation.Value, dtFrom, dtTo, ArrValidCustomer) + " order by TSPL_SD_SALE_INVOICE_HEAD.Customer_Code"
                    dt = clsDBFuncationality.GetDataTable(qry)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        For ii As Integer = 0 To dt.Rows.Count - 1
                            If ArrValidCustomerItem.ContainsKey(clsCommon.myCstr(dt.Rows(ii)("Customer_Code"))) Then
                                If ArrValidCustomerItem.Item(clsCommon.myCstr(dt.Rows(ii)("Customer_Code"))).Contains(clsCommon.myCstr(dt.Rows(ii)("Structure_Code"))) Then
                                    gvInvoice.Rows.AddNew()
                                    clsCommon.ProgressBarPercentUpdate(ii * 100 / dt.Rows.Count, "Loading Invoices " + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                                    gvInvoice.Rows(gvInvoice.RowCount - 1).Cells(colInvCustCode).Value = clsCommon.myCstr(dt.Rows(ii)("Customer_Code"))
                                    gvInvoice.Rows(gvInvoice.RowCount - 1).Cells(colInvCustName).Value = clsCommon.myCstr(dt.Rows(ii)("Customer_Name"))
                                    gvInvoice.Rows(gvInvoice.RowCount - 1).Cells(colInvItemCode).Value = clsCommon.myCstr(dt.Rows(ii)("Item_Code"))
                                    gvInvoice.Rows(gvInvoice.RowCount - 1).Cells(colInvItemName).Value = clsCommon.myCstr(dt.Rows(ii)("Item_Desc"))
                                    If clsCommon.myCstr(dt.Rows(ii)("RI")) > 0 Then
                                        gvInvoice.Rows(gvInvoice.RowCount - 1).Cells(colInvInvoiceCode).Value = clsCommon.myCstr(dt.Rows(ii)("Document_Code"))
                                    Else
                                        gvInvoice.Rows(gvInvoice.RowCount - 1).Cells(colInvReturnCode).Value = clsCommon.myCstr(dt.Rows(ii)("Document_Code"))
                                    End If
                                    gvInvoice.Rows(gvInvoice.RowCount - 1).Cells(colInvDocDate).Value = clsCommon.myCstr(dt.Rows(ii)("Document_Date"))
                                    gvInvoice.Rows(gvInvoice.RowCount - 1).Cells(colInvStructureCode).Value = clsCommon.myCstr(dt.Rows(ii)("Structure_Code"))
                                    gvInvoice.Rows(gvInvoice.RowCount - 1).Cells(colInvQty).Value = clsCommon.myCdbl(dt.Rows(ii)("Qty"))
                                    gvInvoice.Rows(gvInvoice.RowCount - 1).Cells(colInvUOM).Value = clsCommon.myCstr(dt.Rows(ii)("Unit_code"))
                                End If
                            End If
                        Next
                    End If
                    AddCIncSummaryRow(dtFrom, intNoOfDaysInMonth)
                Else
                    LoadBlankGrid()
                    LoadBlankGridCInc()
                    LoadBlankGridCS()
                    LoadBlankGridIC()
                    LoadBlankGridInvoice()
                End If
            Else
                Throw New Exception("No data found")
            End If
            RadPageView1.SelectedPage = RadPageViewPage4
            CalculateSecurity()

            clsCommon.ProgressBarPercentHide()
            EnableDisableFilter(False)
        Catch ex As Exception
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub AddCSSummaryRow(ByVal dr As DataRow, ByVal dtSlab As DataTable, ByVal TotRangeQty As Decimal, ByVal TotIncentiveQty As Decimal, ByVal NoOfDaysInMonth As Integer, ByVal TotStockQty As Decimal, ByVal StockUOM As String)
        If dtSlab Is Nothing OrElse dtSlab.Rows.Count <= 0 Then
            Exit Sub
        End If
        TotRangeQty = Math.Round(TotRangeQty, 2, MidpointRounding.ToEven)
        TotIncentiveQty = Math.Round(TotIncentiveQty, 2, MidpointRounding.ToEven)
        'Dim AvgRange As Decimal = Math.Round(TotRangeQty / NoOfDaysInMonth, 2, MidpointRounding.ToEven)
        gvCustomerStructure.Rows.AddNew()

        If SettDayWiseCustomerIncentiveCalculation Then
            gvCustomerStructure.Rows(gvCustomerStructure.RowCount - 1).Cells(colCSDateWise).Value = clsCommon.myCstr(dr("Document_Date"))
        End If
        gvCustomerStructure.Rows(gvCustomerStructure.RowCount - 1).Cells(colCSCustomerCode).Value = clsCommon.myCstr(dr("Customer_Code"))
        gvCustomerStructure.Rows(gvCustomerStructure.RowCount - 1).Cells(colCSCustomerName).Value = clsCommon.myCstr(dr("Customer_Name"))
        gvCustomerStructure.Rows(gvCustomerStructure.RowCount - 1).Cells(colCSStructureCode).Value = clsCommon.myCstr(dr("Structure_Code"))
        gvCustomerStructure.Rows(gvCustomerStructure.RowCount - 1).Cells(colCSStockQty).Value = TotStockQty
        gvCustomerStructure.Rows(gvCustomerStructure.RowCount - 1).Cells(colCSStockUOM).Value = StockUOM
        gvCustomerStructure.Rows(gvCustomerStructure.RowCount - 1).Cells(colCSRangeQty).Value = TotRangeQty
        gvCustomerStructure.Rows(gvCustomerStructure.RowCount - 1).Cells(colCSRangeUOM).Value = clsCommon.myCstr(dtSlab.Rows(0)("RANGE_UOM"))
        gvCustomerStructure.Rows(gvCustomerStructure.RowCount - 1).Cells(colCSIncentiveQty).Value = TotIncentiveQty
        gvCustomerStructure.Rows(gvCustomerStructure.RowCount - 1).Cells(colCSIncentiveUOM).Value = clsCommon.myCstr(dtSlab.Rows(0)("INCENTIVE_UOM"))
        If dtSlab IsNot Nothing AndAlso dtSlab.Rows.Count > 0 Then
            For ii As Integer = 0 To dtSlab.Rows.Count - 1
                If clsCommon.CompairString(clsCommon.myCstr(dtSlab.Rows(ii)("Structure_Code")), clsCommon.myCstr(dr("Structure_Code"))) = CompairStringResult.Equal Then
                    'If AvgRange >= clsCommon.myCdbl(dtSlab.Rows(ii)("FROM_RANGE")) AndAlso AvgRange <= clsCommon.myCdbl(dtSlab.Rows(ii)("TO_RANGE")) Then
                    gvCustomerStructure.Rows(gvCustomerStructure.RowCount - 1).Cells(colCSIncentiveCode).Value = clsCommon.myCstr(dtSlab.Rows(ii)("INCENTIVE_CODE"))
                    Exit For
                    'End If
                End If
            Next
        End If
    End Sub

    Sub AddCIncSummaryRow(ByVal dtFrom As DateTime, ByVal NoOfDaysInMonth As Integer)
        Dim arrCustomerInc As New Dictionary(Of String, Integer)
        For ii As Integer = 0 To gvCustomerStructure.RowCount - 1
            Dim strCustIncentiveCode As String = clsCommon.myCstr(gvCustomerStructure.Rows(ii).Cells(colCSCustomerCode).Value) + "#" + clsCommon.myCstr(gvCustomerStructure.Rows(ii).Cells(colCSIncentiveCode).Value)
            If SettDayWiseCustomerIncentiveCalculation Then
                strCustIncentiveCode += clsCommon.GetPrintDate(gvCustomerStructure.Rows(ii).Cells(colCSDateWise).Value, "yyyy-MM-dd")
            End If

            If Not arrCustomerInc.ContainsKey(strCustIncentiveCode) Then
                gvCustomerIncentive.Rows.AddNew()
                If SettDayWiseCustomerIncentiveCalculation Then
                    gvCustomerIncentive.Rows(gvCustomerIncentive.Rows.Count - 1).Cells(colCIncDateWise).Value = clsCommon.myCDate(gvCustomerStructure.Rows(ii).Cells(colCSDateWise).Value)
                End If
                gvCustomerIncentive.Rows(gvCustomerIncentive.Rows.Count - 1).Cells(colCIncCustomerCode).Value = clsCommon.myCstr(gvCustomerStructure.Rows(ii).Cells(colCSCustomerCode).Value)
                gvCustomerIncentive.Rows(gvCustomerIncentive.Rows.Count - 1).Cells(colCIncCustomerName).Value = clsCommon.myCstr(gvCustomerStructure.Rows(ii).Cells(colCSCustomerName).Value)
                gvCustomerIncentive.Rows(gvCustomerIncentive.Rows.Count - 1).Cells(colCIncIncentiveCode).Value = clsCommon.myCstr(gvCustomerStructure.Rows(ii).Cells(colCSIncentiveCode).Value)
                gvCustomerIncentive.Rows(gvCustomerIncentive.Rows.Count - 1).Cells(colCIncRangeUOM).Value = clsCommon.myCstr(gvCustomerStructure.Rows(ii).Cells(colCSRangeUOM).Value)
                gvCustomerIncentive.Rows(gvCustomerIncentive.Rows.Count - 1).Cells(colCIncIncentiveUOM).Value = clsCommon.myCstr(gvCustomerStructure.Rows(ii).Cells(colCSIncentiveUOM).Value)
                arrCustomerInc.Add(strCustIncentiveCode, gvCustomerIncentive.Rows.Count - 1)
            End If
            gvCustomerIncentive.Rows(arrCustomerInc.Item(strCustIncentiveCode)).Cells(colCIncRangeQty).Value += clsCommon.myCdbl(gvCustomerStructure.Rows(ii).Cells(colCSRangeQty).Value)
            gvCustomerIncentive.Rows(arrCustomerInc.Item(strCustIncentiveCode)).Cells(colCIncIncentiveQty).Value += clsCommon.myCdbl(gvCustomerStructure.Rows(ii).Cells(colCSIncentiveQty).Value)
        Next

        For ii As Integer = 0 To gvCustomerIncentive.RowCount - 1
            'Dim AvgRange As Decimal = Math.Round(clsCommon.myCdbl(gvCustomerIncentive.Rows(ii).Cells(colCIncRangeQty).Value) / NoOfDaysInMonth, 2, MidpointRounding.ToEven)
            Dim AvgRange As Decimal = Math.Round(clsCommon.myCdbl(gvCustomerIncentive.Rows(ii).Cells(colCIncRangeQty).Value) / NoOfDaysInMonth)
            Dim Arr As List(Of clsSaleIncentiveDetails) = clsSaleIncentiveDetails.GetData(clsCommon.myCstr(gvCustomerIncentive.Rows(ii).Cells(colCIncIncentiveCode).Value), Nothing)
            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                For Each objtr As clsSaleIncentiveDetails In Arr
                    If AvgRange >= objtr.FROM_RANGE AndAlso AvgRange <= objtr.TO_RANGE Then ''ERO/28/03/19-000523,ERO/09/04/19-000552 by balwihder on 28/03/2019
                        gvCustomerIncentive.Rows(ii).Cells(colCIncRangeAvgQty).Value = AvgRange
                        gvCustomerIncentive.Rows(ii).Cells(colCIncIncentiveTRCode).Value = objtr.TR_CODE
                        gvCustomerIncentive.Rows(ii).Cells(colCIncIncentiveCode).Value = objtr.INCENTIVE_CODE
                        gvCustomerIncentive.Rows(ii).Cells(colCIncIncentiveRate).Value = Math.Round(objtr.INCENTIVE, 2, MidpointRounding.ToEven)
                        gvCustomerIncentive.Rows(ii).Cells(colCIncIncentiveAmount).Value = Math.Round(clsCommon.myCdbl(gvCustomerIncentive.Rows(ii).Cells(colCIncIncentiveQty).Value) * clsCommon.myCdbl(objtr.INCENTIVE), 2, MidpointRounding.ToEven)
                        Exit For
                    End If
                Next
            End If
        Next
        AddSummaryRow(dtFrom)
    End Sub

    Sub AddSummaryRow(ByVal dtFrom As DateTime)
        Dim arrCustomer As New Dictionary(Of String, Integer)
        For ii As Integer = 0 To gvCustomerIncentive.RowCount - 1
            Dim strCustCode As String = clsCommon.myCstr(gvCustomerIncentive.Rows(ii).Cells(colCIncCustomerCode).Value)
            If Not arrCustomer.ContainsKey(strCustCode) Then
                gvCustomer.Rows.AddNew()
                gvCustomer.Rows(gvCustomer.Rows.Count - 1).Cells(colCustomerCode).Value = clsCommon.myCstr(gvCustomerIncentive.Rows(ii).Cells(colCIncCustomerCode).Value)
                gvCustomer.Rows(gvCustomer.Rows.Count - 1).Cells(colCustomerName).Value = clsCommon.myCstr(gvCustomerIncentive.Rows(ii).Cells(colCIncCustomerName).Value)
                Dim qry As String = "select top 1 TSPL_CUSTOMER_DEDUCTION_HEAD.Deduction_Code,TSPL_CUSTOMER_DEDUCTION_CUSTOMER.TR_Code,TSPL_CUSTOMER_DEDUCTION_HEAD.Deduction_Amount from TSPL_CUSTOMER_DEDUCTION_HEAD" + Environment.NewLine +
                        "left outer join TSPL_CUSTOMER_DEDUCTION_CUSTOMER on TSPL_CUSTOMER_DEDUCTION_CUSTOMER.Deduction_Code=TSPL_CUSTOMER_DEDUCTION_HEAD.Deduction_Code" + Environment.NewLine +
                        "where TSPL_CUSTOMER_DEDUCTION_HEAD.Posted=1 and TSPL_CUSTOMER_DEDUCTION_HEAD.Inactive_Status = 0 and TSPL_CUSTOMER_DEDUCTION_CUSTOMER.Cust_Code in ('" + strCustCode + "')" + Environment.NewLine +
                        "and TSPL_CUSTOMER_DEDUCTION_HEAD.Deduction_Valid_Till>='" + clsCommon.GetPrintDate(dtFrom, "dd/MMM/yyyy hh:mm:ss tt") + "'  order by Deduction_Valid_Till"
                Dim dtDeduction As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dtDeduction IsNot Nothing AndAlso dtDeduction.Rows.Count > 0 Then
                    gvCustomer.Rows(gvCustomer.RowCount - 1).Cells(colDeductionCode).Value = clsCommon.myCstr(dtDeduction.Rows(0)("Deduction_Code"))
                    gvCustomer.Rows(gvCustomer.RowCount - 1).Cells(colDeductionTRCode).Value = clsCommon.myCstr(dtDeduction.Rows(0)("TR_Code"))
                    gvCustomer.Rows(gvCustomer.RowCount - 1).Cells(colDeductionAmount).Value = Math.Round(clsCommon.myCdbl(dtDeduction.Rows(0)("Deduction_Amount")), 2, MidpointRounding.ToEven)
                End If


                arrCustomer.Add(clsCommon.myCstr(gvCustomerIncentive.Rows(ii).Cells(colCIncCustomerCode).Value), gvCustomer.Rows.Count - 1)
            End If

            ''richa agarwal 31 Oct,2020 round off 0 below columns
            gvCustomer.Rows(arrCustomer.Item(strCustCode)).Cells(colIncentiveAmount).Value += Math.Round(clsCommon.myCdbl(gvCustomerIncentive.Rows(ii).Cells(colCIncIncentiveAmount).Value), 0, MidpointRounding.ToEven)
            gvCustomer.Rows(arrCustomer.Item(strCustCode)).Cells(colAmount).Value = Math.Round(clsCommon.myCdbl(gvCustomer.Rows(arrCustomer.Item(strCustCode)).Cells(colIncentiveAmount).Value) - clsCommon.myCdbl(gvCustomer.Rows(gvCustomer.RowCount - 1).Cells(colDeductionAmount).Value), 0, MidpointRounding.ToEven)
        Next
    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs)
        AddNew()
    End Sub

    Sub AddNew()
        LoadBlankGrid()
        LoadBlankGridCInc()
        LoadBlankGridCS()
        LoadBlankGridIC()
        LoadBlankGridInvoice()
        EnableDisableFilter(True)
    End Sub

    Function AllowToSave() As Boolean
        For ii As Integer = 0 To gvCustomer.Rows.Count - 1
            CalculateRow(ii)
            If clsCommon.myCdbl(gvCustomer.Rows(ii).Cells(colAmount).Value) <= 0 Then
                gvCustomer.CurrentRow = gvCustomer.Rows(ii)
                RadPageView1.SelectedPage = RadPageViewPage4
                Throw New Exception("Amount Can't be -ve for customer " + clsCommon.myCstr(gvCustomer.Rows(ii).Cells(colCustomerCode).Value))
            End If
        Next

        For ii As Integer = 0 To gvCustomerItem.Rows.Count - 1
            If clsCommon.myLen(gvCustomerItem.Rows(ii).Cells(colICRangeUOM).Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                gvCustomerItem.CurrentRow = gvCustomerItem.Rows(ii)
                Throw New Exception("Range Unit not found for customer " + clsCommon.myCstr(gvCustomerItem.Rows(ii).Cells(colICCustomerCode).Value))
            End If
            If clsCommon.myLen(gvCustomerItem.Rows(ii).Cells(colICIncentiveUOM)) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                gvCustomerItem.CurrentRow = gvCustomerItem.Rows(ii)
                Throw New Exception("Incentive Unit not found for customer " + clsCommon.myCstr(gvCustomerItem.Rows(ii).Cells(colICCustomerCode).Value))
            End If
        Next
        Return True
    End Function

    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvCustomer.CellValueChanged, gvCustomer.CellValueChanged
        Try
            If Not isCellValueChangedOpen Then
                isCellValueChangedOpen = True
                If e.Column Is gvCustomer.Columns(colDeductionAmount) Then
                    CalculateRow(gvCustomer.CurrentRow.Index)
                End If
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub CalculateRow(ByVal RowIndex As Integer)
        gvCustomer.Rows(RowIndex).Cells(colAmount).Value = Math.Round(clsCommon.myCdbl(gvCustomer.Rows(RowIndex).Cells(colIncentiveAmount).Value) - clsCommon.myCdbl(gvCustomer.Rows(RowIndex).Cells(colDeductionAmount).Value), 2, MidpointRounding.ToEven)
    End Sub

    Sub CalculateSecurity()
        If SettCustomerIncetiveAutoSecuity Then
            Dim d1 As DateTime = New Date(txtToDate.Value.Year, txtToDate.Value.Month, 1)
            Dim d2 As DateTime = txtToDate.Value

            Dim TTF As New TimeSpan
            TTF = d2.Subtract(d1)
            Dim NoOfDays As Integer = Math.Abs(TTF.TotalDays)
            'Dim arrLoc As New ArrayList
            'arrLoc.Add(txtLocation.Value)
            Dim dtSecurity As DataTable = clsDB.GetQueryCustomerSecurity(True, d2, d2, Nothing, txtCustomer.arrValueMember, Nothing, True, False, "All", Nothing, Nothing, False)
            For ii As Integer = 0 To gvCustomer.Rows.Count - 1
                Dim dblAvgQty As Decimal = 0
                For jj As Integer = 0 To gvCustomerIncentive.Rows.Count - 1
                    If clsCommon.CompairString(clsCommon.myCstr(gvCustomer.Rows(ii).Cells(colCustomerCode).Value), clsCommon.myCstr(gvCustomerIncentive.Rows(jj).Cells(colCIncCustomerCode).Value)) = CompairStringResult.Equal Then
                        dblAvgQty += clsCommon.myCdbl(gvCustomerIncentive.Rows(jj).Cells(colCIncRangeAvgQty).Value)
                    End If
                Next
                gvCustomer.Rows(ii).Cells(colAvgQty).Value = dblAvgQty
                If NoOfDays > 0 Then
                    gvCustomer.Rows(ii).Cells(colAvgQty).Value = Math.Round(dblAvgQty / NoOfDays, 0)
                End If
                If dtSecurity IsNot Nothing AndAlso dtSecurity.Rows.Count > 0 Then
                    For Each dr As DataRow In dtSecurity.Rows
                        If clsCommon.CompairString(clsCommon.myCstr(dr("Customer_Code")), clsCommon.myCstr(gvCustomer.Rows(ii).Cells(colCustomerCode).Value)) = CompairStringResult.Equal Then
                            If clsCommon.myCdbl(dr("Closing")) < 0 Then
                                gvCustomer.Rows(ii).Cells(colSecuity).Value = Math.Abs(clsCommon.myCdbl(dr("Closing")))
                            End If
                            Exit For
                        End If
                    Next
                End If
                Dim CustomerOutstanding As DataTable = clsDBFuncationality.GetDataTable(clsCustomerMaster.getCustomerOutstandingAmtWithOPeningAndClosing("'" + clsCommon.myCstr(gvCustomer.Rows(ii).Cells(colCustomerCode).Value) + "'", clsCommon.GetPrintDate(d2, "dd/MMM/yyyy"), clsCommon.GetPrintDate(clsCommon.myCDate(d2), "dd/MMM/yyyy"), "ConvRate"))
                If CustomerOutstanding IsNot Nothing AndAlso CustomerOutstanding.Rows.Count > 0 Then
                    gvCustomer.Rows(ii).Cells(colDues).Value = clsCommon.myCdbl(CustomerOutstanding.Rows(0)("BalAmt"))
                End If
            Next
        End If
    End Sub

    Sub HideExtraTab(ByVal isHide As ElementVisibility)
        RadPageView1.Pages("RadPageViewPage1").Item.Visibility = isHide
        RadPageView1.Pages("RadPageViewPage2").Item.Visibility = isHide
        RadPageView1.Pages("RadPageViewPage3").Item.Visibility = isHide
        RadPageView1.Pages("RadPageViewPage4").Item.Visibility = isHide
        RadPageView1.Pages("RadPageViewPage5").Item.Visibility = isHide
    End Sub
End Class


