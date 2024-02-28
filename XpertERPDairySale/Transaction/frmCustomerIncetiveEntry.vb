Imports common
Imports System.Data.SqlClient

Public Class frmCustomerIncetiveEntry
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

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
        btnExportExcel.Visible = MyBase.isExport
    End Sub

    Private Sub FrmMilkVSPPayment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SettDayWiseCustomerIncentiveCalculation = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DayWiseCustomerIncentiveCalculation, clsFixedParameterCode.DayWiseCustomerIncentiveCalculation, Nothing)) = 1)
        If SettDayWiseCustomerIncentiveCalculation Then
            RadPageView1.Pages("RadPageViewPage1").Item.Text = "Date," + RadPageView1.Pages("RadPageViewPage1").Item.Text
            RadPageView1.Pages("RadPageViewPage2").Item.Text = "Date," + RadPageView1.Pages("RadPageViewPage2").Item.Text
            RadPageView1.Pages("RadPageViewPage3").Item.Text = "Date," + RadPageView1.Pages("RadPageViewPage3").Item.Text
        End If
        SettCustomerIncetiveAutoSecuity = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CustomerIncetiveAutoSecuity, clsFixedParameterCode.CustomerIncetiveAutoSecuity, Nothing)) = 1)
        pnlSecuity.Visible = SettCustomerIncetiveAutoSecuity

        Is_Load = True
        ButtonTooltip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction")
        ButtonTooltip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonTooltip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
        ButtonTooltip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonTooltip.SetToolTip(btnnew, "Press Alt+N Adding New Trasnaction")

        SetUserMgmtNew()
        Is_Load = False
        AllowDateChanged = True
        RadPageView1.SelectedPage = RadPageViewPage4
        AddNew()
        RadSplitExp.Visible = SettDayWiseCustomerIncentiveCalculation
        btnExportExcel.Visible = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnableExportExcelOnIncentiveEntry, clsFixedParameterCode.EnableExportExcelOnIncentiveEntry, Nothing)) = 1, True, False)
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
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then

                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SIRC"
                frm.strCode = "SIReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnReverse.Visible = True
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
                'MessageBox.Show("You are not authorized to perform this action.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
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

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
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
        txtMonth.Enabled = isEnable
        txtCustomer.Enabled = isEnable
        chkApplyDateRange.Enabled = isEnable
        Panel4.Enabled = isEnable
        pnlSecuity.Enabled = isEnable
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
        Dim qry As String = "select TSPL_SD_SALE_INVOICE_DETAIL.Document_Code,convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Document_Date,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Structure_Code,TSPL_SD_SALE_INVOICE_DETAIL.Qty,TSPL_SD_SALE_INVOICE_DETAIL.Unit_code,(TSPL_SD_SALE_INVOICE_DETAIL.Qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as StockQty,StockUOMDetail.UOM_Code as StcokUOM,1 as RI" + Environment.NewLine + _
            "from TSPL_SD_SALE_INVOICE_DETAIL" + Environment.NewLine + _
            "left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE " + Environment.NewLine + _
            "left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.item_code" + Environment.NewLine + _
            "left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code" + Environment.NewLine + _
            "left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SD_SALE_INVOICE_DETAIL.Unit_code" + Environment.NewLine + _
            "left outer join TSPL_ITEM_UOM_DETAIL as StockUOMDetail on StockUOMDetail.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and StockUOMDetail.Stocking_Unit='Y'" + Environment.NewLine + _
            "where TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS' and isnull(TSPL_SD_SALE_INVOICE_HEAD.IsSampling,0)=0 and isnull(TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item,'N')='N' and isnull(TSPL_SD_SALE_INVOICE_DETAIL.FOC_Item,0)=0 and TSPL_SD_SALE_INVOICE_HEAD.Document_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFrom), "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_SD_SALE_INVOICE_HEAD.Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtTo), "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location='" + strLocation + "' and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(arrCustomer) + ")" + Environment.NewLine + _
            "and exists (select 1 from TSPL_ITEM_UOM_DETAIL as innTSPL_ITEM_UOM_DETAIL left outer join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.Unit_Code=innTSPL_ITEM_UOM_DETAIL.UOM_Code where TSPL_UNIT_MASTER.Ltr_Type='Y' and innTSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code) " + Environment.NewLine + _
            "and not exists(select 1 from TSPL_CUSTOMER_INCENTIVE_INVOICE_WISE where TSPL_CUSTOMER_INCENTIVE_INVOICE_WISE.Doc_Code not in ('" + DocCode + "') and TSPL_CUSTOMER_INCENTIVE_INVOICE_WISE.Invoice_Code=TSPL_SD_SALE_INVOICE_HEAD.Document_Code)" + Environment.NewLine + _
            "union all" + Environment.NewLine + _
            "select TSPL_SD_SALE_RETURN_DETAIL.Document_Code,TSPL_SD_SALE_RETURN_HEAD.Document_Date,TSPL_SD_SALE_RETURN_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_SD_SALE_RETURN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Structure_Code,TSPL_SD_SALE_RETURN_DETAIL.Qty,TSPL_SD_SALE_RETURN_DETAIL.Unit_code,(TSPL_SD_SALE_RETURN_DETAIL.Qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as StockQty,StockUOMDetail.UOM_Code as StcokUOM,-1 as RI" + Environment.NewLine + _
            "from TSPL_SD_SALE_RETURN_DETAIL" + Environment.NewLine + _
            "left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Document_Code=TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE " + Environment.NewLine + _
            "left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.item_code" + Environment.NewLine + _
            "left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_RETURN_HEAD.Customer_Code" + Environment.NewLine + _
            "left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SD_SALE_RETURN_DETAIL.Unit_code" + Environment.NewLine + _
            "left outer join TSPL_ITEM_UOM_DETAIL as StockUOMDetail on StockUOMDetail.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code and StockUOMDetail.Stocking_Unit='Y'" + Environment.NewLine + _
            "where TSPL_SD_SALE_RETURN_HEAD.Screen_Type='DS' and isnull(TSPL_SD_SALE_RETURN_DETAIL.Scheme_Item,'N')='N' and isnull(TSPL_SD_SALE_RETURN_DETAIL.FOC_Item,0)=0 and TSPL_SD_SALE_RETURN_HEAD.Document_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFrom), "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_SD_SALE_RETURN_HEAD.Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtTo), "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location='" + strLocation + "' and TSPL_SD_SALE_RETURN_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(arrCustomer) + ")" + Environment.NewLine + _
            "and exists (select 1 from TSPL_ITEM_UOM_DETAIL as innTSPL_ITEM_UOM_DETAIL left outer join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.Unit_Code=innTSPL_ITEM_UOM_DETAIL.UOM_Code where TSPL_UNIT_MASTER.Ltr_Type='Y' and innTSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code) " + Environment.NewLine + _
            "and not exists(select 1 from TSPL_CUSTOMER_INCENTIVE_INVOICE_WISE where TSPL_CUSTOMER_INCENTIVE_INVOICE_WISE.Doc_Code not in ('" + DocCode + "') and TSPL_CUSTOMER_INCENTIVE_INVOICE_WISE.Return_Code=TSPL_SD_SALE_RETURN_HEAD.Document_Code)"
        Return qry
    End Function

    Sub FillData()
        If SettCustomerIncetiveAutoSecuity Then
            If txtADSecuity.Value <= 0 Then
                txtADSecuity.Value = 0
                If clsCommon.MyMessageBoxShow(Me, "AD Secutity is Zero." + Environment.NewLine + "Do you want to continue..") = DialogResult.No Then
                    txtADSecuity.Focus()
                    Exit Sub
                End If
            End If
            If txtSecuityPart.Value <= 0 Then
                txtSecuityPart.Value = 0
                If clsCommon.MyMessageBoxShow(Me, "Secutity Part is Zero." + Environment.NewLine + "Do you want to continue..") = DialogResult.No Then
                    Exit Sub
                End If
            End If
        End If

        ''ERO/13/12/18-000442 by balwinder on 14/12/2018
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

            Dim dtFrom As DateTime = New Date(txtMonth.Value.Year, txtMonth.Value.Month, 1)
            Dim dtTo As DateTime = New Date(txtMonth.Value.Year, txtMonth.Value.Month, 1).AddMonths(1).AddDays(-1)
            If chkApplyDateRange.Checked Then
                If clsCommon.GetDateWithStartTime(txtFromDate.Value) < dtFrom OrElse clsCommon.GetDateWithStartTime(txtFromDate.Value) > dtTo Then
                    txtFromDate.Focus()
                    Throw New Exception("From Date Range should be [" + clsCommon.GetPrintDate(dtFrom, "dd/MM/yyyy") + " - " + clsCommon.GetPrintDate(dtTo, "dd/MM/yyyy") + "]")
                End If
                If clsCommon.GetDateWithStartTime(txtToDate.Value) < dtFrom OrElse clsCommon.GetDateWithStartTime(txtToDate.Value) > dtTo Then
                    txtToDate.Focus()
                    Throw New Exception("To Date Range should be [" + clsCommon.GetPrintDate(dtFrom, "dd/MM/yyyy") + " - " + clsCommon.GetPrintDate(dtTo, "dd/MM/yyyy") + "]")
                End If
                If clsCommon.GetDateWithStartTime(txtFromDate.Value) > clsCommon.GetDateWithStartTime(txtToDate.Value) Then
                    txtFromDate.Focus()
                    Throw New Exception("From Date Should be less then To Date")
                End If
                dtFrom = txtFromDate.Value
                dtTo = txtToDate.Value
            End If
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
            qry += " from (" + GetBaseQty(txtCode.Value, txtLocation.Value, dtFrom, dtTo, txtCustomer.arrValueMember) + ")xx group by Customer_Code,Structure_Code,Item_Code"
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
                    qry = GetBaseQty(txtCode.Value, txtLocation.Value, dtFrom, dtTo, ArrValidCustomer) + " order by TSPL_SD_SALE_INVOICE_HEAD.Customer_Code"
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
                Dim qry As String = "select top 1 TSPL_CUSTOMER_DEDUCTION_HEAD.Deduction_Code,TSPL_CUSTOMER_DEDUCTION_CUSTOMER.TR_Code,TSPL_CUSTOMER_DEDUCTION_HEAD.Deduction_Amount from TSPL_CUSTOMER_DEDUCTION_HEAD" + Environment.NewLine + _
                        "left outer join TSPL_CUSTOMER_DEDUCTION_CUSTOMER on TSPL_CUSTOMER_DEDUCTION_CUSTOMER.Deduction_Code=TSPL_CUSTOMER_DEDUCTION_HEAD.Deduction_Code" + Environment.NewLine + _
                        "where TSPL_CUSTOMER_DEDUCTION_HEAD.Posted=1 and TSPL_CUSTOMER_DEDUCTION_HEAD.Inactive_Status = 0 and TSPL_CUSTOMER_DEDUCTION_CUSTOMER.Cust_Code in ('" + strCustCode + "')" + Environment.NewLine + _
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
        txtLocation.Value = ""
        lblLocation.Text = ""
        txtMonth.Value = txtDate.Value
        txtFromDate.Value = New Date(txtMonth.Value.Year, txtMonth.Value.Month, 1)
        txtToDate.Value = New Date(txtMonth.Value.Year, txtMonth.Value.Month, 1).AddMonths(1).AddDays(-1)
        chkApplyDateRange.Checked = False
        TxtTotalAmount.Value = 0
        TxtTotDeductionAmount.Value = 0
        TxtTotalAmount.Value = 0
        txtCustomer.arrValueMember = Nothing
        UsLock1.Status = ERPTransactionStatus.Pending

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

    Sub SaveData()
        Try
            If AllowToSave() Then
                Dim obj As clsCustomerIncentiveEntryHead = New clsCustomerIncentiveEntryHead
                obj.Doc_Code = txtCode.Value
                obj.Doc_Date = txtDate.Value
                obj.Location_Code = txtLocation.Value
                obj.Filter_Month = txtMonth.Value
                If chkApplyDateRange.Checked Then
                    obj.Filter_Month_From = txtFromDate.Value
                    obj.Filter_Month_To = txtToDate.Value
                End If
                obj.Additional_Security = txtADSecuity.Value
                obj.Security_Part = txtSecuityPart.Value
                obj.arr = New List(Of clsCustomerIncentiveEntryDetail)
                For ii As Integer = 0 To gvCustomer.Rows.Count - 1
                    Dim objtr As New clsCustomerIncentiveEntryDetail()
                    objtr.Cust_Code = clsCommon.myCstr(gvCustomer.Rows(ii).Cells(colCustomerCode).Value)
                    objtr.Incentive_Amount = clsCommon.myCdbl(gvCustomer.Rows(ii).Cells(colIncentiveAmount).Value)
                    objtr.Deduction_Code = clsCommon.myCstr(gvCustomer.Rows(ii).Cells(colDeductionCode).Value)
                    objtr.Deduction_TR_Code = clsCommon.myCstr(gvCustomer.Rows(ii).Cells(colDeductionTRCode).Value)
                    objtr.Deduction_Amount = clsCommon.myCdbl(gvCustomer.Rows(ii).Cells(colDeductionAmount).Value)
                    objtr.Amount = clsCommon.myCdbl(gvCustomer.Rows(ii).Cells(colAmount).Value)

                    objtr.Avg_Qty = clsCommon.myCdbl(gvCustomer.Rows(ii).Cells(colAvgQty).Value)
                    objtr.Secuity = clsCommon.myCdbl(gvCustomer.Rows(ii).Cells(colSecuity).Value)
                    objtr.Dues = clsCommon.myCdbl(gvCustomer.Rows(ii).Cells(colDues).Value)
                    objtr.Additional_Security_Deposit_Amt = clsCommon.myCdbl(gvCustomer.Rows(ii).Cells(colAdditionalSecurityDepositAmt).Value)
                    objtr.Security_To_Be_Taken = clsCommon.myCdbl(gvCustomer.Rows(ii).Cells(colSecurityToBeTaken).Value)
                    objtr.Net_Margin_Payable = clsCommon.myCdbl(gvCustomer.Rows(ii).Cells(colNetMarginPayable).Value)

                    obj.arr.Add(objtr)
                Next

                obj.arrCustIncentive = New List(Of clsCustomerIncentiveEntryCustomerIncentiveWise)
                For ii As Integer = 0 To gvCustomerIncentive.Rows.Count - 1
                    Dim objtr As New clsCustomerIncentiveEntryCustomerIncentiveWise()
                    If SettDayWiseCustomerIncentiveCalculation Then
                        objtr.Date_Wise = clsCommon.myCDate(gvCustomerIncentive.Rows(ii).Cells(colCIncDateWise).Value)
                    End If
                    objtr.Cust_Code = clsCommon.myCstr(gvCustomerIncentive.Rows(ii).Cells(colCIncCustomerCode).Value)
                    objtr.Range_Qty = clsCommon.myCdbl(gvCustomerIncentive.Rows(ii).Cells(colCIncRangeQty).Value)
                    objtr.Range_Avg_Qty = clsCommon.myCdbl(gvCustomerIncentive.Rows(ii).Cells(colCIncRangeAvgQty).Value)
                    objtr.Range_UOM = clsCommon.myCstr(gvCustomerIncentive.Rows(ii).Cells(colCIncRangeUOM).Value)
                    objtr.Incentive_Qty = clsCommon.myCdbl(gvCustomerIncentive.Rows(ii).Cells(colCIncIncentiveQty).Value)
                    objtr.Incentive_UOM = clsCommon.myCstr(gvCustomerIncentive.Rows(ii).Cells(colCIncIncentiveUOM).Value)
                    objtr.Incentive_TR_Code = clsCommon.myCstr(gvCustomerIncentive.Rows(ii).Cells(colCIncIncentiveTRCode).Value)
                    objtr.Incentive_Code = clsCommon.myCstr(gvCustomerIncentive.Rows(ii).Cells(colCIncIncentiveCode).Value)
                    objtr.Incentive_Rate = clsCommon.myCdbl(gvCustomerIncentive.Rows(ii).Cells(colCIncIncentiveRate).Value)
                    objtr.Incentive_Amount = clsCommon.myCdbl(gvCustomerIncentive.Rows(ii).Cells(colCIncIncentiveAmount).Value)
                    obj.arrCustIncentive.Add(objtr)
                Next

                obj.arrCustStru = New List(Of clsCustomerIncentiveEntryStrucreCustomerWise)
                For ii As Integer = 0 To gvCustomerStructure.Rows.Count - 1
                    Dim objtr As New clsCustomerIncentiveEntryStrucreCustomerWise()
                    If SettDayWiseCustomerIncentiveCalculation Then
                        objtr.Date_Wise = clsCommon.myCDate(gvCustomerStructure.Rows(ii).Cells(colCSDateWise).Value)
                    End If
                    objtr.Cust_Code = clsCommon.myCstr(gvCustomerStructure.Rows(ii).Cells(colCSCustomerCode).Value)
                    objtr.Structure_Code = clsCommon.myCstr(gvCustomerStructure.Rows(ii).Cells(colCSStructureCode).Value)
                    objtr.Stock_Qty = clsCommon.myCdbl(gvCustomerStructure.Rows(ii).Cells(colCSStockQty).Value)
                    objtr.Stock_UOM = clsCommon.myCstr(gvCustomerStructure.Rows(ii).Cells(colCSStockUOM).Value)
                    objtr.Range_Qty = clsCommon.myCdbl(gvCustomerStructure.Rows(ii).Cells(colCSRangeQty).Value)
                    objtr.Range_UOM = clsCommon.myCstr(gvCustomerStructure.Rows(ii).Cells(colCSRangeUOM).Value)
                    objtr.Incentive_Qty = clsCommon.myCdbl(gvCustomerStructure.Rows(ii).Cells(colCSIncentiveQty).Value)
                    objtr.Incentive_UOM = clsCommon.myCstr(gvCustomerStructure.Rows(ii).Cells(colCSIncentiveUOM).Value)
                    objtr.Incentive_Code = clsCommon.myCstr(gvCustomerStructure.Rows(ii).Cells(colCSIncentiveCode).Value)
                    obj.arrCustStru.Add(objtr)
                Next

                obj.arrCustItem = New List(Of clsCustomerIncentiveEntryItemCustomerWise)
                For ii As Integer = 0 To gvCustomerItem.Rows.Count - 1
                    Dim objtr As New clsCustomerIncentiveEntryItemCustomerWise()
                    If SettDayWiseCustomerIncentiveCalculation Then
                        objtr.Date_Wise = clsCommon.myCDate(gvCustomerItem.Rows(ii).Cells(colICDateWise).Value)
                    End If
                    objtr.Cust_Code = clsCommon.myCstr(gvCustomerItem.Rows(ii).Cells(colICCustomerCode).Value)
                    objtr.Item_Code = clsCommon.myCstr(gvCustomerItem.Rows(ii).Cells(colICItemCode).Value)
                    objtr.Structure_Code = clsCommon.myCstr(gvCustomerItem.Rows(ii).Cells(colICStructureCode).Value)
                    objtr.Stock_Qty = clsCommon.myCdbl(gvCustomerItem.Rows(ii).Cells(colICStockQty).Value)
                    objtr.Stock_UOM = clsCommon.myCstr(gvCustomerItem.Rows(ii).Cells(colICStockUOM).Value)
                    objtr.Range_Qty = clsCommon.myCdbl(gvCustomerItem.Rows(ii).Cells(colICRangeQty).Value)
                    objtr.Range_UOM = clsCommon.myCstr(gvCustomerItem.Rows(ii).Cells(colICRangeUOM).Value)
                    objtr.Incentive_Qty = clsCommon.myCdbl(gvCustomerItem.Rows(ii).Cells(colICIncentiveQty).Value)
                    objtr.Incentive_UOM = clsCommon.myCstr(gvCustomerItem.Rows(ii).Cells(colICIncentiveUOM).Value)
                    obj.arrCustItem.Add(objtr)
                Next

                obj.arrInvoice = New List(Of clsCustomerIncentiveEntryInvoiceWise)
                For ii As Integer = 0 To gvInvoice.Rows.Count - 1
                    Dim objtr As New clsCustomerIncentiveEntryInvoiceWise()
                    objtr.Cust_Code = clsCommon.myCstr(gvInvoice.Rows(ii).Cells(colInvCustCode).Value)
                    objtr.Item_Code = clsCommon.myCstr(gvInvoice.Rows(ii).Cells(colInvItemCode).Value)
                    objtr.Invoice_Code = clsCommon.myCstr(gvInvoice.Rows(ii).Cells(colInvInvoiceCode).Value)
                    objtr.Return_Code = clsCommon.myCstr(gvInvoice.Rows(ii).Cells(colInvReturnCode).Value)
                    If SettDayWiseCustomerIncentiveCalculation Then
                        objtr.Doc_date = clsCommon.myCDate(gvInvoice.Rows(ii).Cells(colInvDocDate).Value)
                    End If

                    objtr.Structure_Code = clsCommon.myCstr(gvInvoice.Rows(ii).Cells(colInvStructureCode).Value)
                    objtr.Qty = clsCommon.myCdbl(gvInvoice.Rows(ii).Cells(colInvQty).Value)
                    objtr.UOM = clsCommon.myCstr(gvInvoice.Rows(ii).Cells(colInvUOM).Value)
                    obj.arrInvoice.Add(objtr)
                Next
                obj.SaveData(obj, IsNewEntry)
                clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
                LoadData(obj.Doc_Code, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            AddNew()
            Dim obj As New clsCustomerIncentiveEntryHead()
            obj = clsCustomerIncentiveEntryHead.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Doc_Code) > 0) Then
                EnableDisableFilter(False)
                If obj.Posted = ERPTransactionStatus.Posted Then
                    btnsave.Enabled = False
                    btnPost.Enabled = False
                    btndelete.Enabled = False
                End If
                IsNewEntry = False
                txtCode.Value = obj.Doc_Code
                txtDate.Value = obj.Doc_Date
                If obj.Filter_Month_From IsNot Nothing Then
                    chkApplyDateRange.Checked = True
                    txtFromDate.Value = obj.Filter_Month_From
                    txtToDate.Value = obj.Filter_Month_To
                End If

                txtADSecuity.Value = obj.Additional_Security
                txtSecuityPart.Value = obj.Security_Part

                txtLocation.Value = obj.Location_Code
                lblLocation.Text = obj.Location_Name
                UsLock1.Status = obj.Posted
                txtMonth.Value = obj.Filter_Month
                txtCustomer.arrValueMember = obj.arrCustomer
                If obj.arr IsNot Nothing AndAlso obj.arr.Count > 0 Then
                    For Each objTr As clsCustomerIncentiveEntryDetail In obj.arr
                        gvCustomer.Rows.AddNew()
                        gvCustomer.Rows(gvCustomer.Rows.Count - 1).Cells(colCustomerCode).Value = objTr.Cust_Code
                        gvCustomer.Rows(gvCustomer.Rows.Count - 1).Cells(colCustomerName).Value = objTr.Cust_Name
                        gvCustomer.Rows(gvCustomer.Rows.Count - 1).Cells(colIncentiveAmount).Value = objTr.Incentive_Amount
                        gvCustomer.Rows(gvCustomer.Rows.Count - 1).Cells(colDeductionCode).Value = objTr.Deduction_Code
                        gvCustomer.Rows(gvCustomer.Rows.Count - 1).Cells(colDeductionTRCode).Value = objTr.Deduction_TR_Code
                        gvCustomer.Rows(gvCustomer.Rows.Count - 1).Cells(colDeductionAmount).Value = objTr.Deduction_Amount
                        gvCustomer.Rows(gvCustomer.Rows.Count - 1).Cells(colAmount).Value = objTr.Amount

                        gvCustomer.Rows(gvCustomer.Rows.Count - 1).Cells(colAvgQty).Value = objTr.Avg_Qty
                        gvCustomer.Rows(gvCustomer.Rows.Count - 1).Cells(colSecuity).Value = objTr.Secuity
                        gvCustomer.Rows(gvCustomer.Rows.Count - 1).Cells(colDues).Value = objTr.Dues
                        gvCustomer.Rows(gvCustomer.Rows.Count - 1).Cells(colAdditionalSecurityDepositAmt).Value = objTr.Additional_Security_Deposit_Amt
                        gvCustomer.Rows(gvCustomer.Rows.Count - 1).Cells(colSecurityToBeTaken).Value = objTr.Security_To_Be_Taken
                        gvCustomer.Rows(gvCustomer.Rows.Count - 1).Cells(colNetMarginPayable).Value = objTr.Net_Margin_Payable
                    Next
                End If

                If obj.arrCustIncentive IsNot Nothing AndAlso obj.arrCustIncentive.Count > 0 Then
                    For Each objTr As clsCustomerIncentiveEntryCustomerIncentiveWise In obj.arrCustIncentive
                        gvCustomerIncentive.Rows.AddNew()
                        If objTr.Date_Wise IsNot Nothing Then
                            gvCustomerIncentive.Rows(gvCustomerIncentive.Rows.Count - 1).Cells(colCIncDateWise).Value = objTr.Date_Wise
                        End If
                        gvCustomerIncentive.Rows(gvCustomerIncentive.Rows.Count - 1).Cells(colCIncCustomerCode).Value = objTr.Cust_Code
                        gvCustomerIncentive.Rows(gvCustomerIncentive.Rows.Count - 1).Cells(colCIncCustomerName).Value = objTr.Cust_Name
                        gvCustomerIncentive.Rows(gvCustomerIncentive.Rows.Count - 1).Cells(colCIncRangeQty).Value = objTr.Range_Qty
                        gvCustomerIncentive.Rows(gvCustomerIncentive.Rows.Count - 1).Cells(colCIncRangeAvgQty).Value = objTr.Range_Avg_Qty
                        gvCustomerIncentive.Rows(gvCustomerIncentive.Rows.Count - 1).Cells(colCIncRangeUOM).Value = objTr.Range_UOM
                        gvCustomerIncentive.Rows(gvCustomerIncentive.Rows.Count - 1).Cells(colCIncIncentiveQty).Value = objTr.Incentive_Qty
                        gvCustomerIncentive.Rows(gvCustomerIncentive.Rows.Count - 1).Cells(colCIncIncentiveUOM).Value = objTr.Incentive_UOM
                        gvCustomerIncentive.Rows(gvCustomerIncentive.Rows.Count - 1).Cells(colCIncIncentiveTRCode).Value = objTr.Incentive_TR_Code
                        gvCustomerIncentive.Rows(gvCustomerIncentive.Rows.Count - 1).Cells(colCIncIncentiveCode).Value = objTr.Incentive_Code
                        gvCustomerIncentive.Rows(gvCustomerIncentive.Rows.Count - 1).Cells(colCIncIncentiveRate).Value = objTr.Incentive_Rate
                        gvCustomerIncentive.Rows(gvCustomerIncentive.Rows.Count - 1).Cells(colCIncIncentiveAmount).Value = objTr.Incentive_Amount
                    Next
                End If

                If obj.arrCustStru IsNot Nothing AndAlso obj.arrCustStru.Count > 0 Then
                    For Each objTr As clsCustomerIncentiveEntryStrucreCustomerWise In obj.arrCustStru
                        gvCustomerStructure.Rows.AddNew()
                        If objTr.Date_Wise IsNot Nothing Then
                            gvCustomerStructure.Rows(gvCustomerStructure.Rows.Count - 1).Cells(colCSDateWise).Value = objTr.Date_Wise
                        End If
                        gvCustomerStructure.Rows(gvCustomerStructure.Rows.Count - 1).Cells(colCSCustomerCode).Value = objTr.Cust_Code
                        gvCustomerStructure.Rows(gvCustomerStructure.Rows.Count - 1).Cells(colCSCustomerName).Value = objTr.Cust_Name
                        gvCustomerStructure.Rows(gvCustomerStructure.Rows.Count - 1).Cells(colCSStructureCode).Value = objTr.Structure_Code
                        gvCustomerStructure.Rows(gvCustomerStructure.Rows.Count - 1).Cells(colCSStockQty).Value = objTr.Stock_Qty
                        gvCustomerStructure.Rows(gvCustomerStructure.Rows.Count - 1).Cells(colCSStockUOM).Value = objTr.Stock_UOM
                        gvCustomerStructure.Rows(gvCustomerStructure.Rows.Count - 1).Cells(colCSRangeQty).Value = objTr.Range_Qty
                        gvCustomerStructure.Rows(gvCustomerStructure.Rows.Count - 1).Cells(colCSRangeUOM).Value = objTr.Range_UOM
                        gvCustomerStructure.Rows(gvCustomerStructure.Rows.Count - 1).Cells(colCSIncentiveQty).Value = objTr.Incentive_Qty
                        gvCustomerStructure.Rows(gvCustomerStructure.Rows.Count - 1).Cells(colCSIncentiveUOM).Value = objTr.Incentive_UOM
                        gvCustomerStructure.Rows(gvCustomerStructure.Rows.Count - 1).Cells(colCSIncentiveCode).Value = objTr.Incentive_Code
                    Next
                End If

                If obj.arr IsNot Nothing AndAlso obj.arr.Count > 0 Then
                    For Each objTr As clsCustomerIncentiveEntryItemCustomerWise In obj.arrCustItem
                        gvCustomerItem.Rows.AddNew()
                        If objTr.Date_Wise IsNot Nothing Then
                            gvCustomerItem.Rows(gvCustomerItem.Rows.Count - 1).Cells(colICDateWise).Value = objTr.Date_Wise
                        End If
                        gvCustomerItem.Rows(gvCustomerItem.Rows.Count - 1).Cells(colICCustomerCode).Value = objTr.Cust_Code
                        gvCustomerItem.Rows(gvCustomerItem.Rows.Count - 1).Cells(colICCustomerName).Value = objTr.Cust_Name
                        gvCustomerItem.Rows(gvCustomerItem.Rows.Count - 1).Cells(colICItemCode).Value = objTr.Item_Code
                        gvCustomerItem.Rows(gvCustomerItem.Rows.Count - 1).Cells(colICItemName).Value = objTr.Item_Name
                        gvCustomerItem.Rows(gvCustomerItem.Rows.Count - 1).Cells(colICStructureCode).Value = objTr.Structure_Code
                        gvCustomerItem.Rows(gvCustomerItem.Rows.Count - 1).Cells(colICStockQty).Value = objTr.Stock_Qty
                        gvCustomerItem.Rows(gvCustomerItem.Rows.Count - 1).Cells(colICStockUOM).Value = objTr.Stock_UOM
                        gvCustomerItem.Rows(gvCustomerItem.Rows.Count - 1).Cells(colICRangeQty).Value = objTr.Range_Qty
                        gvCustomerItem.Rows(gvCustomerItem.Rows.Count - 1).Cells(colICRangeUOM).Value = objTr.Range_UOM
                        gvCustomerItem.Rows(gvCustomerItem.Rows.Count - 1).Cells(colICIncentiveQty).Value = objTr.Incentive_Qty
                        gvCustomerItem.Rows(gvCustomerItem.Rows.Count - 1).Cells(colICIncentiveUOM).Value = objTr.Incentive_UOM
                    Next
                End If
                If obj.arrInvoice IsNot Nothing AndAlso obj.arrInvoice.Count > 0 Then
                    For Each objTr As clsCustomerIncentiveEntryInvoiceWise In obj.arrInvoice
                        gvInvoice.Rows.AddNew()
                        gvInvoice.Rows(gvInvoice.Rows.Count - 1).Cells(colInvCustCode).Value = objTr.Cust_Code
                        gvInvoice.Rows(gvInvoice.Rows.Count - 1).Cells(colInvCustName).Value = objTr.Cust_Name
                        gvInvoice.Rows(gvInvoice.Rows.Count - 1).Cells(colInvItemCode).Value = objTr.Item_Code
                        gvInvoice.Rows(gvInvoice.Rows.Count - 1).Cells(colInvItemName).Value = objTr.Item_Name
                        gvInvoice.Rows(gvInvoice.Rows.Count - 1).Cells(colInvInvoiceCode).Value = objTr.Invoice_Code
                        gvInvoice.Rows(gvInvoice.Rows.Count - 1).Cells(colInvReturnCode).Value = objTr.Return_Code
                        If objTr.Doc_Code IsNot Nothing Then
                            gvInvoice.Rows(gvInvoice.Rows.Count - 1).Cells(colInvDocDate).Value = objTr.Doc_Date
                        End If
                        gvInvoice.Rows(gvInvoice.Rows.Count - 1).Cells(colInvStructureCode).Value = objTr.Structure_Code
                        gvInvoice.Rows(gvInvoice.Rows.Count - 1).Cells(colInvQty).Value = objTr.Qty
                        gvInvoice.Rows(gvInvoice.Rows.Count - 1).Cells(colInvUOM).Value = objTr.UOM
                    Next
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
                If clsCustomerIncentiveEntryHead.DeleteData(txtCode.Value) Then
                    clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
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
                If (clsCustomerIncentiveEntryHead.PostData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtCode._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD where Doc_Code='" + txtCode.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        Dim qry As String = "select TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD.Doc_Code,TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD.Doc_Date,TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD.Location_Code,TSPL_LOCATION_MASTER.Location_Desc, TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD.Filter_Month,case when TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD.Posted=1 then 'Approved' else 'Pending' end as	status " + Environment.NewLine + _
        " from TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD" + Environment.NewLine + _
        " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD.Location_Code "
        Dim whrClas As String = ""
        LoadData(clsCommon.ShowSelectForm("Code@inceCu", qry, "Doc_Code", whrClas, txtCode.Value, "", isButtonClicked), NavigatorType.Current)
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Document Not Found For Print", Me.Text)
                Return
            End If
            Dim qry As String = ""
            Dim dtFrom As DateTime = New Date(txtMonth.Value.Year, txtMonth.Value.Month, 1)
            Dim dtTo As DateTime = New Date(txtMonth.Value.Year, txtMonth.Value.Month, 1).AddMonths(1).AddDays(-1)
            Dim dtFromPendingLeger As DateTime = Nothing
            Dim dtToPendingLeger As DateTime = Nothing
            Dim dtNextMoth As DateTime = New Date(txtMonth.Value.Year, txtMonth.Value.Month, 1).AddMonths(1)
            Dim dtNexMothFrom As DateTime = dtNextMoth
            Dim whrforPending As String = ""
            If chkApplyDateRange.Checked = True Then
                dtFromPendingLeger = txtFromDate.Value
                dtToPendingLeger = txtToDate.Value
                dtFrom = txtFromDate.Value
                dtTo = txtToDate.Value
            End If
            Dim Query As String = "select * from TSPL_Fiscal_Year_Master where '" + clsCommon.GetPrintDate(txtMonth.Value, "dd/MMM/yyyy") + "' between start_Date and End_Date  "
            Dim dtFinYear As DataTable = clsDBFuncationality.GetDataTable(Query)
            If dtFinYear IsNot Nothing AndAlso dtFinYear.Rows.Count > 0 Then
                dtFromPendingLeger = clsCommon.myCDate(dtFinYear.Rows(0)("start_Date"))
                dtToPendingLeger = clsCommon.myCDate(dtFinYear.Rows(0)("End_Date"))
            End If

            qry = "  select TSPL_CUSTOMER_INCENTIVE_CUSTOMER_INCENTIVE_WISE.Cust_Code,'' as Structure_Code ,TSPL_CUSTOMER_INCENTIVE_CUSTOMER_INCENTIVE_WISE.Incentive_Qty  as Discount_Total_Ltr,TSPL_CUSTOMER_INCENTIVE_CUSTOMER_INCENTIVE_WISE.Incentive_UOM  as Discount_Total_Ltr_UOM,TSPL_CUSTOMER_INCENTIVE_CUSTOMER_INCENTIVE_WISE.Range_Qty  as Discount_Total_Crts, TSPL_CUSTOMER_INCENTIVE_CUSTOMER_INCENTIVE_WISE.Range_UOM as Discount_Total_Crts_Uom ,TSPL_CUSTOMER_INCENTIVE_CUSTOMER_INCENTIVE_WISE. Range_AVg_Qty  as Discount_Avg_Qty,TSPL_CUSTOMER_INCENTIVE_CUSTOMER_INCENTIVE_WISE.Incentive_Rate  as " & _
                  "  Discount_TDSlab,   TSPL_CUSTOMER_INCENTIVE_CUSTOMER_INCENTIVE_WISE.Incentive_Amount  as Discount_TDAmount   from TSPL_CUSTOMER_INCENTIVE_CUSTOMER_INCENTIVE_WISE " & _
                  "  where  Doc_Code = '" + txtCode.Value + "' "
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt2.Rows.Count > 0 Then
                Dim type As String = "Aged Trial Balance By Document date"
                Dim IsFifoBased As String = "N"
                Dim ArryLst As New ArrayList
                ArryLst.Add("IN")
                ArryLst.Add("DB")
                ArryLst.Add("CR")
                ArryLst.Add("RC")

                ArryLst.Add("UC")
                ArryLst.Add("SR")

                ArryLst.Add("AD")
                ArryLst.Add("RF")

                ArryLst.Add("AV")
                ArryLst.Add("OA")
                ArryLst.Add("VGCL")
                Dim ArryLstLocation As New ArrayList
                ArryLstLocation.Add(txtLocation.Value)
                Dim CheckCustomer As String
                CheckCustomer = " AND TSPL_CUSTOMER_MASTER.Status='N'"
                Dim rptHeading As String
                rptHeading = "Aged Trial Balance Report"
                Dim isonduedate As String = String.Empty
                isonduedate = "DueDate"

                If clsCommon.myLen(clsCommon.myCstr(dtFromPendingLeger)) > 0 AndAlso clsCommon.myLen(clsCommon.myCstr(dtToPendingLeger)) > 0 Then
                    'dtFrom = dtFromPendingLeger
                    'dtTo = dtToPendingLeger
                    dtNexMothFrom = dtFromPendingLeger
                    dtNextMoth = dtToPendingLeger
                End If
                Dim strageingDate As Date = New Date(txtMonth.Value.Year, txtMonth.Value.Month, 1).AddMonths(1).AddDays(-1)
                'whrforPending = " and convert (date,Final.[Document Date],103) >= convert (date, '" + strageingDate + "',103)  and convert (date,Final.[Document Date],103) <= convert (date,'" + dtToPendingLeger + "',103)"
                whrforPending = "  and convert (date,Final.[Document Date],103) <= convert (date,'" + strageingDate + "',103)"
                'Dim strFromDate As String = "01/" + clsCommon.myCstr(txtDate.Value.Month) + "/" + clsCommon.myCstr(txtDate.Value.Year)
                'Dim strToDAteValue As String = clsCommon.myCDate(DateSerial(txtDate.Value.Year, txtDate.Value.Month + 1, 0))
                'Dim strLastDateOfMonth As String = clsCommon.myCDate(DateSerial(txtDate.Value.Year, txtDate.Value.Month + 1, 0))

                ''Dim strInnerQry As String = GetOutStandingQry(clsCommon.myCDate(clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtToPendingLeger), "dd/MMM/yyyy hh:mm:ss tt")), clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtToPendingLeger), "dd/MMM/yyyy hh:mm:ss tt"), False, ArryLst, isonduedate, "ConvRate", IIf((txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0), txtCustomer.arrValueMember, Nothing), ArryLstLocation, Nothing, False, Nothing, IIf(False, "", "AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N'"))

                Dim strInnerQry As String = GetOutStandingQry(clsCommon.myCDate(clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(strageingDate), "dd/MMM/yyyy hh:mm:ss tt")), clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(strageingDate), "dd/MMM/yyyy hh:mm:ss tt"), False, ArryLst, isonduedate, "ConvRate", IIf((txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0), txtCustomer.arrValueMember, Nothing), ArryLstLocation, Nothing, False, Nothing, IIf(False, "", "AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N'"))

                Dim dtPendingLeger_qry As String = "  select '" + clsCommon.GetPrintDate(dtNexMothFrom, "dd/MMM/yyyy") + "' as 'dtNexMothFrom', '" + clsCommon.GetPrintDate(strageingDate, "dd/MMM/yyyy") + "' as 'dtNextMonth' , Final.[Document Id],convert (varchar, Final.[Document Date],103) as [Document Date] ,Final.[Customer Id] as [Cust_Code] , Final.[Customer Name], Final.[Due Amount], Case when Document_Type not in ('CR','OA','AV') then  Final.[Document Amount] else 0 end [Document Amount], " & _
                                                   " Case when Document_Type in ('CR','OA','AV') then   (isnull(Final.[Document Amount],0) - (isnull(Final.[Due Amount],0) * (-1)) ) else (isnull(Final.[Document Amount],0) - (isnull(Final.[Due Amount],0)) ) end  + Case when Document_Type in ('CR','OA','AV') then isnull(Final.[Document Amount],0) else 0 end  as [Receipt Amount] from (  " & _
                                                   " " + strInnerQry + "   " & _
                                                   "   ) Final where Final.[Due Amount] <> 0   " + whrforPending + "  order by Final.[Customer Id], convert (datetime ,Final.[Document Date],103) "  ' where Final.[Due Amount] > 0
                Dim dtPendingLedger As DataTable = clsDBFuncationality.GetDataTable(dtPendingLeger_qry)

                ' CTE ===================================================================================================
                qry = "  WITH PenidngAmountTotal (Cust_Code,TotalDueAmount)  AS  ( select xxxx.Cust_Code ,sum (xxxx.[Due Amount])  as TotalDueAmount from ( " & _
                      "  select '" + clsCommon.GetPrintDate(dtNexMothFrom, "dd/MMM/yyyy") + "' as 'dtNexMothFrom', '" + clsCommon.GetPrintDate(strageingDate, "dd/MMM/yyyy") + "' as 'dtNextMonth' , Final.[Document Id],convert (varchar, Final.[Document Date],103) as [Document Date] ,Final.[Customer Id] as [Cust_Code] , Final.[Customer Name], Final.[Due Amount], Case when Document_Type not in ('CR','OA','AV') then  Final.[Document Amount] else 0 end [Document Amount], " & _
                      "  Case when Document_Type in ('CR','OA','AV') then   (isnull(Final.[Document Amount],0) - (isnull(Final.[Due Amount],0) * (-1)) ) else (isnull(Final.[Document Amount],0) - (isnull(Final.[Due Amount],0)) ) end  + Case when Document_Type in ('CR','OA','AV') then isnull(Final.[Document Amount],0) else 0 end  as [Receipt Amount] from (  " & _
                      "  " + strInnerQry + "   " & _
                      "   ) Final where Final.[Due Amount] <> 0   " + whrforPending + "  " & _
                      "  ) XXXX group by Cust_Code  )  "
                If SettDayWiseCustomerIncentiveCalculation = True Then '  clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "TSDDCF") = CompairStringResult.Equal
                    qry += "  select TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD.Doc_Date,'" + clsCommon.GetPrintDate(dtFrom, "dd/MMM/yyyy") + "' as 'dtFrom', '" + clsCommon.GetPrintDate(dtTo, "dd/MMM/yyyy") + "' as 'dtTo',TSPL_COMPANY_MASTER.Comp_Name, TSPL_CUSTOMER_INCENTIVE_DETAIL.Doc_Code,TSPL_CUSTOMER_MASTER.Zone_Code,TSPL_Zone_Master.Description as Zone_Name,TSPL_CUSTOMER_INCENTIVE_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.OldName,TSPL_CUSTOMER_MASTER.Customer_Name,TBL_Incentive.Incentive_Qty,(TBL_Incentive.Incentive_Qty/ (Case when TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD.Filter_Month_From is Null and Filter_Month_To is Null then   DAY(EOMONTH(Filter_Month)) else DATEDIFF(day,convert(date,Filter_Month_From,103),convert(date,Filter_Month_To,103))+1  end )) as MothAvg, " & _
                           "  Cast (Round ((TBL_Incentive.Incentive_Qty/ (Case when TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD.Filter_Month_From is Null and Filter_Month_To is Null then   DAY(EOMONTH(Filter_Month)) else DATEDIFF(day,convert(date,Filter_Month_From,103),convert(date,Filter_Month_To,103))+1  end )),0) as Integer) as MothAvgWithRoundOff, TSPL_CUSTOMER_INCENTIVE_DETAIL.Incentive_Amount as VendorMargin,TSPL_CUSTOMER_INCENTIVE_DETAIL.Deduction_Amount, " & _
                           "  isnull(PenidngAmountTotal.TotalDueAmount,0) as TotalDueAmount , TSPL_CUSTOMER_INCENTIVE_DETAIL.Amount from TSPL_CUSTOMER_INCENTIVE_DETAIL " & _
                           "  Left Outer Join TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD on TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD.Doc_Code = TSPL_CUSTOMER_INCENTIVE_DETAIL.Doc_Code " & _
                           "  Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_CUSTOMER_INCENTIVE_DETAIL.Cust_Code " & _
                           "  Left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code " & _
                           "  Left Outer Join ( " & _
                           "  Select max(TSPL_CUSTOMER_INCENTIVE_ITEM_CUSTOMER_WISE.Doc_Code) as Doc_Code, TSPL_CUSTOMER_INCENTIVE_ITEM_CUSTOMER_WISE.Cust_Code,Sum (TSPL_CUSTOMER_INCENTIVE_ITEM_CUSTOMER_WISE.Incentive_Qty ) as Incentive_Qty from TSPL_CUSTOMER_INCENTIVE_ITEM_CUSTOMER_WISE " & _
                           "  where TSPL_CUSTOMER_INCENTIVE_ITEM_CUSTOMER_WISE.Doc_Code = '" + txtCode.Value + "' group by TSPL_CUSTOMER_INCENTIVE_ITEM_CUSTOMER_WISE.Cust_Code " & _
                           "  )TBL_Incentive on TBL_Incentive.Cust_Code = TSPL_CUSTOMER_INCENTIVE_DETAIL.Cust_Code and TBL_Incentive.Doc_Code = TSPL_CUSTOMER_INCENTIVE_DETAIL.Doc_Code " & _
                           "  Left outer join PenidngAmountTotal on PenidngAmountTotal.Cust_Code = TSPL_CUSTOMER_INCENTIVE_DETAIL.Cust_Code " & _
                           "  Left Outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = TSPL_CUSTOMER_MASTER.Comp_Code " & _
                           "  where TSPL_CUSTOMER_INCENTIVE_DETAIL.Doc_Code = '" + txtCode.Value + "' "
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funsubreportWithdt(CrystalReportFolder.SalesReport, dt, Nothing, "rptCustomerIncentiveEntryDateWise", "Abstract of Vendor Margin", clsCommon.myCDate(dt.Rows(0)("Doc_Date")))
                    'frmCRV.funsubreportWithdt(CrystalReportFolder.SalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptCustomerIncentiveEntry", "Turnover Discount", clsCommon.myCDate(dt.Rows(0)("Doc_Date")), "rptCompanyAddress.rpt", "rptPendingDetailsAsPerSaleLeger.rpt", dtPendingLedger, "rptIncentiveCustomerStructure.rpt", dt2)
                Else


                    qry += " select '" + clsCommon.GetPrintDate(dtFrom, "dd/MMM/yyyy") + "' as 'dtFrom', '" + clsCommon.GetPrintDate(dtTo, "dd/MMM/yyyy") + "' as 'dtTo',  tspl_company_master.Comp_Name as Company_Name ,tspl_company_master.GSTReg_No as Company_GSTReg_No, tspl_company_master.CINNO as Company_CINNO,tspl_company_master.PAN_No as Company_PAN_No,tspl_company_master.Tin_NO as Company_Tin_NO , FILTER_MONTH as Document_Date, " & _
                              " Format( FILTER_MONTH, 'MMMM/yyyy') as Entry_Date, TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD.Location_Code, " & _
                              " TSPL_Location_Master.Location_Desc as Location_Name , TSPL_Location_Master.Add1 as Location_Add1 ,TSPL_Location_Master.Add2 as Location_Add2,  " & _
                              " TSPL_Location_Master.Add3 as Location_Add3, TSPL_Location_Master.Add4 as Location_add4, TSPL_Location_Master.City_Code as Location_City_Code,  " & _
                              " TSPL_City_Master_For_Location.City_Name as Location_City_Name,TSPL_Location_Master.State as Location_State_Code ,TSPL_STATE_MASTER_For_Location.State_Name as Location_State_Name ,TSPL_Location_Master.Pin_Code as Location_Pin_Code ,TSPL_Location_Master.GSTNO as Location_GSTNO, " & _
                              " TSPL_CUSTOMER_INCENTIVE_ITEM_CUSTOMER_WISE.Cust_Code  , TSPL_Customer_Master.Customer_Name, " & _
                              " TSPL_Customer_Master.Add1 as Customer_Add1, TSPL_Customer_Master.Add2 as Customer_Add2, TSPL_Customer_Master.Add3 as Customer_Add3, TSPL_Customer_Master.City_Code as Customer_City_Code, TSPL_City_Master_For_Customer.City_Name as Customer_City_Name,TSPL_Customer_Master.State as Customer_State_Code, " & _
                              " TSPL_STATE_MASTER_For_Customer.STATE_NAME as Customer_State_Name , TSPL_Customer_Master.Phone1 as Customer_Phone1, TSPL_Customer_Master.Phone2 as Customer_Phone2 , TSPL_Customer_Master.PIN_Code as Customer_Pin_Code , TSPL_Customer_Master.GSTNO  as Customer_GSTINO , " & _
                              " TSPL_CUSTOMER_INCENTIVE_ITEM_CUSTOMER_WISE.Item_Code,TSPL_ITEM_MASTER.Item_Desc ,TSPL_CUSTOMER_INCENTIVE_ITEM_CUSTOMER_WISE.Stock_Qty ,  " & _
                              " TSPL_CUSTOMER_INCENTIVE_ITEM_CUSTOMER_WISE.Stock_UOM ,TSPL_CUSTOMER_INCENTIVE_ITEM_CUSTOMER_WISE.Range_Qty ,  " & _
                              " TSPL_CUSTOMER_INCENTIVE_ITEM_CUSTOMER_WISE.Range_UOM,TSPL_CUSTOMER_INCENTIVE_ITEM_CUSTOMER_WISE.Incentive_Qty , TSPL_CUSTOMER_INCENTIVE_ITEM_CUSTOMER_WISE.Incentive_UOM, " & _
                              " 'MILK' as Structure_Code, " & _
                              "  " & _
                              " TSPL_CUSTOMER_INCENTIVE_DETAIL.Incentive_Amount as Discount_TDAmount, TSPL_CUSTOMER_INCENTIVE_DETAIL.Incentive_Amount as Discount_Cumulative_TD,TSPL_CUSTOMER_INCENTIVE_DETAIL.Deduction_Amount as Discount_Standing_Recovery,isnull (TSPL_CUSTOMER_INCENTIVE_DETAIL.Amount,0) - isnull (TotalDueAmount,0) as Discount_TD_Payable_Amount, PenidngAmountTotal.TotalDueAmount " & _
                              " from TSPL_CUSTOMER_INCENTIVE_ITEM_CUSTOMER_WISE  " & _
                              " left outer join TSPL_CUSTOMER_INCENTIVE_DETAIL on TSPL_CUSTOMER_INCENTIVE_DETAIL.Doc_Code = TSPL_CUSTOMER_INCENTIVE_ITEM_CUSTOMER_WISE.Doc_Code  and  " & _
                              " TSPL_CUSTOMER_INCENTIVE_DETAIL.Cust_Code = TSPL_CUSTOMER_INCENTIVE_ITEM_CUSTOMER_WISE.Cust_Code " & _
                              " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_CUSTOMER_INCENTIVE_ITEM_CUSTOMER_WISE.Item_Code " & _
                              " left outer join TSPL_Customer_Master on TSPL_Customer_Master.Cust_Code = TSPL_CUSTOMER_INCENTIVE_ITEM_CUSTOMER_WISE.Cust_Code " & _
                              " left outer join TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD on TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD.Doc_Code = TSPL_CUSTOMER_INCENTIVE_DETAIL.Doc_Code " & _
                              " left outer join TSPL_Location_Master on TSPL_Location_Master.Location_Code  = TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD.Location_Code " & _
                              " left outer join TSPL_City_Master as TSPL_City_Master_For_Location on TSPL_City_Master_For_Location.City_Code = TSPL_Location_Master.City_Code " & _
                              " left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_For_Location on TSPL_STATE_MASTER_For_Location.STATE_CODE = TSPL_Location_Master.State " & _
                              " left outer join TSPL_City_Master as TSPL_City_Master_For_Customer on TSPL_City_Master_For_Customer.City_Code = TSPL_Customer_Master.City_Code " & _
                              " left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_For_Customer on TSPL_STATE_MASTER_For_Customer.STATE_CODE = TSPL_Customer_Master.State " & _
                              " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_code = TSPL_CUSTOMER_MASTER.Comp_Code " & _
                              " left outer join PenidngAmountTotal on PenidngAmountTotal.Cust_Code = TSPL_CUSTOMER_INCENTIVE_ITEM_CUSTOMER_WISE.Cust_Code " & _
                              " where TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD .Doc_Code in ('" + txtCode.Value + "')  OPTION (MAXRECURSION 0) "
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    '==========================================================================================================
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funsubreportWithdt(CrystalReportFolder.SalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptCustomerIncentiveEntry", "Turnover Discount", clsCommon.myCDate(dt.Rows(0)("Document_Date")), "rptCompanyAddress.rpt", "rptPendingDetailsAsPerSaleLeger.rpt", dtPendingLedger, "rptIncentiveCustomerStructure.rpt", dt2)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Shared Function GetOutStandingQry(ByVal AgeOfDate As DateTime, ByVal CutOfDate As DateTime, ByVal IsInactiveCustomer As Boolean, ByVal DocTypeList As ArrayList, ByVal IsOnDueDate As String, Optional ByVal strCurrency As String = "", Optional ByVal CustomerList As ArrayList = Nothing, Optional ByVal LocationList As ArrayList = Nothing, Optional ByVal CustomerGroupList As ArrayList = Nothing, Optional ByVal IsParentCustomer As Boolean = False, Optional ByVal ParentCustomerList As ArrayList = Nothing, Optional ByVal Is_Security As String = "", Optional ByVal ISShowCustomerInvoiceorDocNo As Boolean = False) As String
        Try

            Dim Qry As String = "SELECT qUERY.* FROM ( Select MAX(xxx.Comp_Code) AS Comp_Code, [Customer Id], MAX([Parent Code]) AS [Parent Code],MAX(Parent_Master.Customer_Name) as ParentName, MAX([Customer Name]) AS [Customer Name], MAX(TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code) AS Cust_Group_Code, MAX(TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc) AS Cust_Group_Desc, [Document Id], MAX([Desc]) as [Desc]," & _
             " SUM([Due Amount]*" & strCurrency & ")  AS [Due Amount],  max([Document Amount]) as [Document Amount]," & _
"" & IIf(strCurrency = "1", "MAX(xxx.CURRENCY_CODE)", "'INR'") & "  as Currency,max(xxx.CURRENCY_CODE) As CURRENCY_CODE,MAX(xxx.ConvRate) As ConvRate, MAX([Due Date]) AS [Due Date], MAX(type) AS type, MAX([Document Date]) AS [Document Date], MAX(Ageing_Days) AS Ageing_Days, MAX(Document_Type) AS Document_Type, MAX(Location) AS Location  from ("

            Qry += " SELECT  TSPL_Customer_Invoice_Head.Document_No  as ARINvoiceNo, TSPL_Customer_Invoice_Head.Comp_Code,TSPL_CUSTOMER_MASTER.Cust_Code AS [Customer Id], TSPL_CUSTOMER_MASTER.Parent_Customer_No AS [Parent Code]," & _
         " TSPL_CUSTOMER_MASTER.Customer_Name AS [Customer Name]," & _
         " " + IIf(ISShowCustomerInvoiceorDocNo = True, " TSPL_Customer_Invoice_Head.Document_No ", "(case when ISNULL( Against_Sale_No,'')<>'' then Against_Sale_No when ISNULL(Against_Sale_Return_No,'')<>'' then TSPL_Customer_Invoice_Head.Document_No  when ISNULL( AgainstScrap,'')<>'' then AgainstScrap else TSPL_Customer_Invoice_Head.Document_No end)") + "  as [Document Id], " & _
         " TSPL_Customer_Invoice_Head.Description as [Desc], (case when TSPL_Customer_Invoice_Head.Document_Type IN ('I','D') then TSPL_Customer_Invoice_Head.Document_Total Else (TSPL_Customer_Invoice_Head.Document_Total-  ISNULL((Select SUM(TSPL_RECEIPT_DETAIL.Applied_Amount) from TSPL_RECEIPT_HEADER INNER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_DETAIL.Receipt_No=TSPL_RECEIPT_HEADER.Receipt_No AND ISNULL(TSPL_RECEIPT_HEADER.Posted,'')<>'N' AND TSPL_RECEIPT_HEADER.Receipt_Type in ('A','R') AND TSPL_RECEIPT_DETAIL.Document_No=TSPL_Customer_Invoice_Head.Document_No and CONVERT(DATE,TSPL_RECEIPT_HEADER.Receipt_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) LEFT OUTER JOIN TSPL_BANK_REVERSE ON TSPL_BANK_REVERSE.Document_No = TSPL_RECEIPT_HEADER.Receipt_No AND ISNULL(TSPL_BANK_REVERSE.Source_Type,'') ='AR' WHERE ISNULL(TSPL_BANK_REVERSE.REVERSE_CODE,'' )='' ),0) " & _
         "  - ISNULL((Select SUM(TSPL_RECEIPT_HEADER.Receipt_Amount) from TSPL_RECEIPT_HEADER  LEFT OUTER JOIN TSPL_BANK_REVERSE ON TSPL_BANK_REVERSE.Document_No = TSPL_RECEIPT_HEADER.Receipt_No AND ISNULL(TSPL_BANK_REVERSE.Source_Type,'') ='AR' WHERE ISNULL(TSPL_BANK_REVERSE.REVERSE_CODE,'' )='' and  ISNULL(TSPL_RECEIPT_HEADER.Posted,'')<>'N' AND TSPL_RECEIPT_HEADER.Receipt_Type in ('A') AND TSPL_RECEIPT_HEADER.Applied_RECEIPT=TSPL_Customer_Invoice_Head.Document_No and CONVERT(DATE,TSPL_RECEIPT_HEADER.Receipt_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103)),0) " & _
         ") *-1 end " & _
         " - case when TSPL_Customer_Invoice_Head.Document_Type IN ('I','D') then ISNULL((Select SUM(TSPL_RECEIPT_DETAIL.Applied_Amount) from TSPL_RECEIPT_HEADER INNER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_DETAIL.Receipt_No=TSPL_RECEIPT_HEADER.Receipt_No AND ISNULL(TSPL_RECEIPT_HEADER.Posted,'')<>'N' AND TSPL_RECEIPT_HEADER.Receipt_Type in ('A','R') AND TSPL_RECEIPT_DETAIL.Document_No=TSPL_Customer_Invoice_Head.Document_No and CONVERT(DATE,TSPL_RECEIPT_HEADER.Receipt_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) LEFT OUTER JOIN TSPL_BANK_REVERSE ON TSPL_BANK_REVERSE.Document_No = TSPL_RECEIPT_HEADER.Receipt_No AND ISNULL(TSPL_BANK_REVERSE.Source_Type,'') ='AR' WHERE ISNULL(TSPL_BANK_REVERSE.REVERSE_CODE,'' )='' ),0) else 0 end -isnull((select sum(isnull(TSPL_SD_SALE_RETURN_HEAD.Total_Amt,0)) from TSPL_SD_SALE_RETURN_HEAD left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No  LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  and CONVERT(DATE,TSPL_SD_SALE_RETURN_HEAD.Document_Date  ,103)<=CONVERT(DATE,'" & CutOfDate & "',103)  and isnull(TSPL_SD_SALE_RETURN_HEAD.Status,'0') =1 ),0) -isnull((select sum(isnull(TSPL_SALE_RETURN_MASTER_BULKSALE.Total_Amt,0)) from TSPL_SALE_RETURN_MASTER_BULKSALE  left outer join TSPL_INVOICE_MASTER_BULKSALE  on TSPL_INVOICE_MASTER_BULKSALE.Document_No =TSPL_SALE_RETURN_MASTER_BULKSALE.InvoiceNo   LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.Against_Sale_No=TSPL_INVOICE_MASTER_BULKSALE.Document_No  where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  and CONVERT(DATE,TSPL_SALE_RETURN_MASTER_BULKSALE.Document_Date  ,103)<=CONVERT(DATE,'" & CutOfDate & "',103)  and isnull(TSPL_SALE_RETURN_MASTER_BULKSALE.Posted ,'0') =1 ),0) -isnull((select sum(isnull(TSPL_SCRAPSALE_HEAD_RETURN.Doc_Amt ,0)) from TSPL_SCRAPSALE_HEAD_RETURN left outer join TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD.invoice_No =TSPL_SCRAPSALE_HEAD_RETURN.Invoice_No  LEFT OUTER JOIN TSPL_Customer_Invoice_Head as innINVHead ON innINVHead.AgainstScrap =TSPL_SCRAPINVOICE_HEAD.Invoice_No where innINVHead.Document_No = TSPL_Customer_Invoice_Head.Document_No  and CONVERT(DATE,TSPL_SCRAPSALE_HEAD_RETURN.Return_ship_Date  ,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and isnull(TSPL_SCRAPSALE_HEAD_RETURN.Status,'0') =1),0)) as [Due Amount],TSPL_Customer_Invoice_Head.Document_Total as [Document Amount]   ,TSPL_Customer_Invoice_Head.CURRENCY_CODE, case when isnull(TSPL_REVALUATION_HEAD .Currency_Rate,0) <>0 then isnull(TSPL_REVALUATION_HEAD .Currency_Rate,0) else TSPL_Customer_Invoice_Head.ConvRate end as ConvRate  ,"

            Qry += "  TSPL_Customer_Invoice_Head.due_date as [Due Date],'' AS type, CONVERT(DATE,TSPL_Customer_Invoice_Head.Document_Date,103) as [Document Date], "

            If clsCommon.CompairString(IsOnDueDate, "DueDate") = CompairStringResult.Equal Then
                Qry += " DATEDIFF(day,convert(date,TSPL_Customer_Invoice_Head.Due_Date,103),convert(date,'" & AgeOfDate & "',103)) AS Ageing_Days , "
            ElseIf clsCommon.CompairString(IsOnDueDate, "DocumentDate") = CompairStringResult.Equal Then
                'Qry += " DATEDIFF(dd,convert(date,Invoice_Entry_Date,103),'" & StrAgeOfDate & "') as datedifference, "
                ' Qry += " DATEDIFF(day,convert(date,TSPL_Customer_Invoice_Head." + IIf(IsOnDueDate = True, "Due_Date", "Document_Date") + ",103),convert(date,'" & AgeOfDate & "',103)) AS Ageing_Days , "
                Qry += " DATEDIFF(day,convert(date,TSPL_Customer_Invoice_Head.Document_Date,103),convert(date,'" & AgeOfDate & "',103)) AS Ageing_Days , "
            End If

            Qry += " case when TSPL_Customer_Invoice_Head.Document_Type ='I' then 'IN'  when TSPL_Customer_Invoice_Head.Document_Type ='D' then 'DB'  when TSPL_Customer_Invoice_Head.Document_Type ='C' then 'CR' end  as [Document_Type] ," & _
           " TSPL_Customer_Invoice_Head.Loc_Code as Location FROM  TSPL_Customer_Invoice_Head INNER JOIN   TSPL_CUSTOMER_MASTER ON TSPL_Customer_Invoice_Head.Customer_Code  = TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_Customer_Invoice_Head.Against_Sale_No " & _
           " left outer join (  Select  TSPL_REVALUATION_DETAIL.AR_Invoice_No,TSPL_REVALUATION_HEAD.Currency_Rate,TSPL_REVALUATION_HEAD.Document_Date,TSPL_REVALUATION_HEAD.Document_No  " & _
           " from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD  on TSPL_REVALUATION_DETAIL.Document_No =TSPL_REVALUATION_HEAD.Document_No and TSPL_REVALUATION_HEAD.Document_No in  (select top 1 h.Document_No  from TSPL_REVALUATION_HEAD as h  left outer join TSPL_REVALUATION_DETAIL d on d.Document_No=h.Document_No" & _
           " where d.AR_Invoice_No=TSPL_REVALUATION_DETAIL.AR_Invoice_No and CONVERT(DATE,h.Document_Date,103)<=CONVERT(DATE,'" & AgeOfDate & "',103) and isnull(d.AR_Invoice_No ,'')<>'' order by h.Document_Date desc) where CONVERT(DATE,TSPL_REVALUATION_HEAD .Document_Date,103)<=CONVERT(DATE,'" & AgeOfDate & "',103) and isnull(AR_Invoice_No ,'')<>'')TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD .AR_Invoice_No =TSPL_Customer_Invoice_Head .Document_No where TSPL_Customer_Invoice_Head.Status='1' AND TSPL_CUSTOMER_MASTER.Status='" + IIf(IsInactiveCustomer = True, "Y", "N") + "'" & _
           " UNION ALL SELECT ''  as ARINvoiceNo,  TSPL_SALE_RETURN_INTER_HEAD.Comp_Code, TSPL_SALE_RETURN_INTER_HEAD.Cust_Code AS [Customer Id] ,Parent_Customer_No AS [Parent Code] ,Cust_Name AS [Customer Name] ,Document_No as [Document Id]  ,Description as [Desc] ,(Total_Order_Amt)*-1 as [Due Amount] ,Total_Order_Amt as [Document Amount] ,'INR' AS CURRENCY_CODE, 1 AS ConvRate, CONVERT(DATE,Document_Date,103) as [Due Date] ,'' AS [type], CONVERT(DATE,Document_Date,103) as [Document Date]  , DATEDIFF(day,convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103),convert(date,'" & AgeOfDate & "',103)) AS Ageing_Days,'SR' as [Document_Type], (select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code=Location) as Location  from TSPL_SALE_RETURN_INTER_HEAD INNER JOIN   TSPL_CUSTOMER_MASTER ON TSPL_SALE_RETURN_INTER_HEAD.Cust_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where  TSPL_SALE_RETURN_INTER_HEAD.Is_Post=1 AND TSPL_CUSTOMER_MASTER.Status='" + IIf(IsInactiveCustomer = True, "Y", "N") + "'" & _
           " UNION ALL select TSPL_Customer_Invoice_Head.Document_No  as ARINvoiceNo, TSPL_VCGL_Head.Comp_Code, TSPL_VCGL_Head.VC_Code as ACode,TSPL_CUSTOMER_MASTER.Parent_Customer_No,TSPL_VCGL_Head.VC_Name as AName,TSPL_VCGL_Head.Document_No as DocNo,'',CASE WHEN Amount_Type='Cr' THEN TSPL_VCGL_Head.Amount ELSE TSPL_VCGL_Head.Amount*-1 END  as [Due Amount],TSPL_VCGL_Head.Amount as [Document Amount], 'INR' AS CURRENCY_CODE, 1 AS ConvRate ,convert(DATE,TSPL_VCGL_Head.Document_Date,103) as DueDate,'',convert(date,TSPL_VCGL_Head.Document_Date,103),DATEDIFF(day,convert(date,TSPL_VCGL_Head.Document_Date,103),convert(date,'" & AgeOfDate & "',103)) AS Ageing_Days,'VGCL',TSPL_VCGL_Head.Location_Segment from  TSPL_VCGL_Head  left outer JOIN   TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Head.VC_Code  = TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON ISNULL(TSPL_Customer_Invoice_Head.Against_VCGL,'') = TSPL_VCGL_Head.Document_No  where TSPL_VCGL_Head.Document_Type='C' and TSPL_VCGL_Head.Status=1 AND TSPL_CUSTOMER_MASTER.Status='" + IIf(IsInactiveCustomer = True, "Y", "N") + "'  AND ISNULL(TSPL_Customer_Invoice_Head.Against_VCGL,'') =''" & _
           " UNION ALL select TSPL_Customer_Invoice_Head.Document_No  as ARINvoiceNo, TSPL_VCGL_Head.Comp_Code, TSPL_VCGL_Detail.VCGL_Code as ACode,TSPL_CUSTOMER_MASTER.Parent_Customer_No,TSPL_VCGL_Detail.VCGL_Name as AName,TSPL_VCGL_Head.Document_No as DocNo,'',CASE WHEN TSPL_VCGL_Detail.Cr_Amount>0 THEN TSPL_VCGL_Detail.Cr_Amount*-1 ELSE TSPL_VCGL_Detail.Dr_Amount END as [Due Amount], TSPL_VCGL_Head.Amount as [Document Amount], 'INR' AS CURRENCY_CODE, 1 AS ConvRate ,convert(date,TSPL_VCGL_Head.Document_Date,103) as DueDate,'',convert(date,TSPL_VCGL_Head.Document_Date,103),DATEDIFF(day,convert(date,TSPL_VCGL_Head.Document_Date,103),convert(date,'" & AgeOfDate & "',103)) AS Ageing_Days,'VGCL',TSPL_VCGL_Head.Location_Segment from  TSPL_VCGL_Detail left outer join TSPL_VCGL_Head on TSPL_VCGL_Head .Document_No=TSPL_VCGL_Detail.Document_No left outer JOIN   TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Detail.VCGL_Code  = TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON ISNULL(TSPL_Customer_Invoice_Head.Against_VCGL,'') = TSPL_VCGL_Detail.Document_No  where  TSPL_VCGL_Head.Status=1 and TSPL_VCGL_Detail.Row_Type='Customer' AND TSPL_CUSTOMER_MASTER.Status='" + IIf(IsInactiveCustomer = True, "Y", "N") + "' AND ISNULL(TSPL_Customer_Invoice_Head.Against_VCGL ,'') =''" & _
           " UNION ALL select ''  as ARINvoiceNo, TSPL_RECEIPT_HEADER.Comp_Code ,TSPL_RECEIPT_HEADER.Cust_Code,TSPL_CUSTOMER_MASTER.Parent_Customer_No ,TSPL_RECEIPT_HEADER.Customer_Name ,TSPL_RECEIPT_HEADER.Receipt_No ,TSPL_RECEIPT_HEADER.Entry_Desc , (Case When TSPL_RECEIPT_HEADER.Receipt_Type='F' Then TSPL_RECEIPT_HEADER.Balance_Amt Else (TSPL_RECEIPT_HEADER.Receipt_Amount- (SELECT case when isnull(SUM(Z.Applied_Amount),0)=0 then isnull(SUM(Z.Receipt_Amount ),0) else isnull(SUM(Z.Applied_Amount),0) end FROM  (Select CASE WHEN TSPL_RECEIPT_DETAIL.Receipt_Type ='C' THEN COALESCE(TSPL_RECEIPT_DETAIL.Applied_Amount,0) * -1 ELSE " & _
           " COALESCE(TSPL_RECEIPT_DETAIL.Applied_Amount,0) END  AS Applied_Amount,TSPL_RECEIPT_HEADER.Receipt_Amount from TSPL_RECEIPT_HEADER AS APP_REC  INNER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_DETAIL.Receipt_No=APP_REC.Receipt_No  " & _
           " AND ISNULL(APP_REC.Posted,'')<>'N'  AND APP_REC.Receipt_Type in ('A') AND APP_REC.Applied_Receipt=TSPL_RECEIPT_HEADER.Receipt_No and " & _
           " CONVERT(DATE,APP_REC.Receipt_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and APP_REC.Receipt_No not in (  Select Document_No from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Source_Type ='AR' and TSPL_BANK_REVERSE.Document_No = APP_REC.Receipt_No and CONVERT(DATE,TSPL_BANK_REVERSE.Reversal_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and isnull(TSPL_BANK_REVERSE.Post ,'')='P' ))Z))*-1 End) as [Due Amount],TSPL_RECEIPT_HEADER.Receipt_Amount as [Document Amount], TSPL_RECEIPT_HEADER.CURRENCY_CODE  AS CURRENCY_CODE, TSPL_RECEIPT_HEADER.ConvRate AS ConvRate ,convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103) ,'' AS type ,convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103) , DATEDIFF(day,convert(date,TSPL_RECEIPT_HEADER.Receipt_Date ,103),convert(date,'" & AgeOfDate & "',103)), case when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'AV'  when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'OA'  when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'UC'  when TSPL_RECEIPT_HEADER.Receipt_Type='F' then 'RF' when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'RC' end , RIGHT(TSPL_RECEIPT_HEADER.Dr_Account,3) as Location  from TSPL_RECEIPT_HEADER inner join TSPL_CUSTOMER_MASTER  ON TSPL_RECEIPT_HEADER.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code where TSPL_RECEIPT_HEADER.Receipt_Type NOT IN ('M', 'A','R') and TSPL_RECEIPT_HEADER.Posted='Y' AND TSPL_CUSTOMER_MASTER.Status='" + IIf(IsInactiveCustomer = True, "Y", "N") + "'" + Is_Security + "" & _
           "  and TSPL_RECEIPT_HEADER.Receipt_No not in (  Select Document_No from TSPL_BANK_REVERSE where  TSPL_BANK_REVERSE.Source_Type ='AR' and TSPL_BANK_REVERSE.Document_No = TSPL_RECEIPT_HEADER.Receipt_No and CONVERT(DATE,TSPL_BANK_REVERSE.Reversal_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and isnull(TSPL_BANK_REVERSE.Post ,'')='P') " & _
            "  and TSPL_RECEIPT_HEADER.Receipt_No not in (  Select Document_No from TSPL_BANK_REVERSE where  TSPL_BANK_REVERSE.Source_Type ='AR' and TSPL_BANK_REVERSE.Document_No = TSPL_RECEIPT_HEADER.Receipt_No and CONVERT(DATE,TSPL_BANK_REVERSE.Reversal_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and isnull(TSPL_BANK_REVERSE.Post ,'')='P') " & _
                       " AND TSPL_RECEIPT_HEADER.Receipt_No NOT IN ( " & Environment.NewLine & _
                       " sELECT Applied_Receipt  FROM TSPL_RECEIPT_HEADER WHERE Receipt_Type ='F' AND ISNULL(Applied_Receipt ,'')<>'' and CONVERT(DATE,TSPL_RECEIPT_HEADER.Receipt_Date ,103)<=CONVERT(DATE,'" & CutOfDate & "',103)  and TSPL_RECEIPT_HEADER.Receipt_No not in (  Select Document_No from TSPL_BANK_REVERSE where  TSPL_BANK_REVERSE.Source_Type ='AR' and TSPL_BANK_REVERSE.Document_No = TSPL_RECEIPT_HEADER.Receipt_No and CONVERT(DATE,TSPL_BANK_REVERSE.Reversal_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and isnull(TSPL_BANK_REVERSE.Post ,'')='P')" & Environment.NewLine & _
                       " union all" & Environment.NewLine & _
                       " sELECT Receipt_No  FROM TSPL_RECEIPT_HEADER WHERE Receipt_Type ='F' AND ISNULL(Applied_Receipt ,'')<>'' and  CONVERT(DATE,TSPL_RECEIPT_HEADER.Receipt_Date ,103)<=CONVERT(DATE,'" & CutOfDate & "',103)  and TSPL_RECEIPT_HEADER.Receipt_No not in (  Select Document_No from TSPL_BANK_REVERSE where  TSPL_BANK_REVERSE.Source_Type ='AR' and TSPL_BANK_REVERSE.Document_No = TSPL_RECEIPT_HEADER.Receipt_No and CONVERT(DATE,TSPL_BANK_REVERSE.Reversal_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and isnull(TSPL_BANK_REVERSE.Post ,'')='P')" & Environment.NewLine & _
           " union all " & Environment.NewLine & _
           " Select distinct TSPL_RECEIPT_DETAIL.Document_No  from TSPL_RECEIPT_DETAIL left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No =TSPL_RECEIPT_DETAIL .Receipt_No where TSPL_RECEIPT_DETAIL.Document_No in (Select ISNULL(Receipt_No ,'') from TSPL_RECEIPT_HEADER where Receipt_Type ='F' and isnull(Applied_Receipt ,'')='') and  CONVERT(DATE,TSPL_RECEIPT_HEADER.Receipt_Date ,103)<=CONVERT(DATE,'" & CutOfDate & "',103)  and TSPL_RECEIPT_HEADER.Receipt_No not in (  Select Document_No from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Source_Type ='AR' and TSPL_BANK_REVERSE.Document_No = TSPL_RECEIPT_HEADER.Receipt_No and CONVERT(DATE,TSPL_BANK_REVERSE.Reversal_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) and isnull(TSPL_BANK_REVERSE.Post ,'')='P' ))"




            ''richa agarwal use TSPL_Receipt_Adjustment_Header.ARInvoiceNo in place of Adjustment_no in below line to show ar invoice no BM00000007349
            ' Qry += " UNION ALL Select TSPL_Customer_Invoice_Head.Document_No  as ARINvoiceNo, TSPL_SD_SALE_INVOICE_HEAD.Comp_Code, Customer_No as [Customer Id], TSPL_CUSTOMER_MASTER.Parent_Customer_No AS [Parent Code], TSPL_CUSTOMER_MASTER.Customer_Name AS [Customer Name], TSPL_Receipt_Adjustment_Header.ARInvoiceNo as [Document Id], TSPL_Receipt_Adjustment_Header.Description as [Desc], case when TSPL_Customer_Invoice_Head.Document_Type='C' then 0 else TSPL_Receipt_Adjustment_Header.Adjustment_Amount end *-1 as [Due Amount],'INR' AS CURRENCY_CODE, 1 AS ConvRate, CONVERT(DATE,Adjustment_Date,103) as [Due Date], '' AS type, CONVERT(DATE,Adjustment_Date,103) as [Document Date], DATEDIFF(day,convert(date, Adjustment_Date,103),convert(date,'" & AgeOfDate & "',103)) AS Ageing_Days, 'RC' as [Document_Type], (select Loc_Segment_Code from TSPL_LOCATION_MASTER WHERE Location_Code = TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location) as Location from TSPL_Receipt_Adjustment_Header LEFT OUTER JOIN TSPL_CUSTOMER_MASTER on TSPL_Receipt_Adjustment_Header.Customer_No= TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD on TSPL_Receipt_Adjustment_Header.Doc_No= TSPL_SD_SALE_INVOICE_HEAD.Document_Code  LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON ISNULL(TSPL_Customer_Invoice_Head.Document_No ,'') = TSPL_Receipt_Adjustment_Header.ARInvoiceNo  WHERE TSPL_Receipt_Adjustment_Header.Is_Post ='Y' AND TSPL_CUSTOMER_MASTER.Status='" + IIf(IsInactiveCustomer = True, "Y", "N") + "'" & _
            Qry += " UNION ALL Select TSPL_Customer_Invoice_Head.Document_No  as ARINvoiceNo, TSPL_SD_SALE_INVOICE_HEAD.Comp_Code, Customer_No as [Customer Id], TSPL_CUSTOMER_MASTER.Parent_Customer_No AS [Parent Code], TSPL_CUSTOMER_MASTER.Customer_Name AS [Customer Name], TSPL_Receipt_Adjustment_Header.ARInvoiceNo as [Document Id], TSPL_Receipt_Adjustment_Header.Description as [Desc], case when TSPL_Customer_Invoice_Head.Document_Type='C' then TSPL_Receipt_Adjustment_Header.Adjustment_Amount else TSPL_Receipt_Adjustment_Header.Adjustment_Amount * -1 end  as [Due Amount]  ,TSPL_Receipt_Adjustment_Header.Adjustment_Amount as [Document Amount],'INR' AS CURRENCY_CODE, 1 AS ConvRate, CONVERT(DATE,Adjustment_Date,103) as [Due Date], '' AS type, CONVERT(DATE,Adjustment_Date,103) as [Document Date], DATEDIFF(day,convert(date, Adjustment_Date,103),convert(date,'" & AgeOfDate & "',103)) AS Ageing_Days, 'RC' as [Document_Type], (select Loc_Segment_Code from TSPL_LOCATION_MASTER WHERE Location_Code = TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location) as Location from TSPL_Receipt_Adjustment_Header LEFT OUTER JOIN TSPL_CUSTOMER_MASTER on TSPL_Receipt_Adjustment_Header.Customer_No= TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD on TSPL_Receipt_Adjustment_Header.Doc_No= TSPL_SD_SALE_INVOICE_HEAD.Document_Code  LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON ISNULL(TSPL_Customer_Invoice_Head.Document_No ,'') = TSPL_Receipt_Adjustment_Header.ARInvoiceNo  WHERE TSPL_Receipt_Adjustment_Header.Is_Post ='Y' AND TSPL_CUSTOMER_MASTER.Status='" + IIf(IsInactiveCustomer = True, "Y", "N") + "'" & _
                              " UNION ALL SELECT ''  as ARINvoiceNo, TSPL_SALE_RETURN_INTER_HEAD.Comp_Code, TSPL_SALE_RETURN_INTER_HEAD.Cust_Code AS [Customer Id] ,Parent_Customer_No AS [Parent Code] ,Cust_Name AS [Customer Name],Document_No as [Document Id] ,Description as [Desc] , Empty_Value*-1 AS [Due Amount] , Empty_Value as [Document Amount],'INR' AS CURRENCY_CODE, 1 AS ConvRate, CONVERT(DATE,Document_Date,103) as [Due Date] ,'' AS [type], CONVERT(DATE,Document_Date,103) as [Document Date], DATEDIFF(day,convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103),convert(date,'" & AgeOfDate & "',103)) AS Ageing_Days, 'SR' as [Document_Type], (select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code=Location) as Location from TSPL_SALE_RETURN_INTER_HEAD INNER JOIN   TSPL_CUSTOMER_MASTER ON TSPL_SALE_RETURN_INTER_HEAD.Cust_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where  TSPL_SALE_RETURN_INTER_HEAD.Is_Post=1 AND TSPL_CUSTOMER_MASTER.Status='" + IIf(IsInactiveCustomer = True, "Y", "N") + "'" & _
                              " UNION ALL SELECT '' as ARINvoiceNo,  TSPL_ADJUSTMENT_HEADER.Comp_Code,TSPL_ADJUSTMENT_HEADER.Customer_CODE,'',TSPL_ADJUSTMENT_HEADER.Customer_NAME,TSPL_ADJUSTMENT_HEADER.Adjustment_No,'',case when TSPL_ADJUSTMENT_HEADER.Trans_Type<>'In' then  (SELECT SUM(ISNULL(Item_Cost,0)+ISNULL(Breakage_Cost,0))*-1 FROM dbo.TSPL_ADJUSTMENT_DETAIL WHERE TSPL_ADJUSTMENT_DETAIL.Adjustment_No=TSPL_ADJUSTMENT_HEADER.Adjustment_No)*-1 else (SELECT SUM(ISNULL(Item_Cost,0)+ISNULL(Breakage_Cost,0))*-1 FROM dbo.TSPL_ADJUSTMENT_DETAIL WHERE TSPL_ADJUSTMENT_DETAIL.Adjustment_No=TSPL_ADJUSTMENT_HEADER.Adjustment_No) end  AS [Due Amount] ,(SELECT SUM(ISNULL(Item_Cost,0)+ISNULL(Breakage_Cost,0))*-1 FROM dbo.TSPL_ADJUSTMENT_DETAIL WHERE TSPL_ADJUSTMENT_DETAIL.Adjustment_No=TSPL_ADJUSTMENT_HEADER.Adjustment_No) as [Document Amount] ,'INR' AS CURRENCY_CODE, 1 AS ConvRate,convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) as DueDate,'',convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103),DATEDIFF(day,convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103),convert(date,'" & AgeOfDate & "',103)) AS Ageing_Days,'AD',(select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code=TSPL_ADJUSTMENT_HEADER.Loc_Code) FROM dbo.TSPL_ADJUSTMENT_HEADER LEFT OUTER JOIN TSPL_CUSTOMER_MASTER on TSPL_ADJUSTMENT_HEADER.Customer_CODE=TSPL_CUSTOMER_MASTER.Cust_Code WHERE TSPL_ADJUSTMENT_HEADER.Customer_CODE <> '' AND ISNULL(Reference_Document,'')='' AND TSPL_CUSTOMER_MASTER.Status='" + IIf(IsInactiveCustomer = True, "Y", "N") + "'" & _
                          " ) XXX LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON XXX.[Customer Id]=TSPL_CUSTOMER_MASTER.Cust_Code" & _
                          " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER as Parent_Master ON Parent_Master.Cust_Code=XXX.[Parent Code]" & _
                          " LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code = TSPL_CUSTOMER_MASTER.Cust_Group_Code" & _
                          " where  XXX.Document_Type in (" + clsCommon.GetMulcallString(DocTypeList) + "  ) " & _
                          " and convert(date,XXX.[Document Date] ,103) <= convert(date,'" & CutOfDate & "',103)"
            If IsParentCustomer Then
                If ParentCustomerList IsNot Nothing AndAlso ParentCustomerList.Count > 0 Then
                    If CustomerList IsNot Nothing AndAlso CustomerList.Count > 0 Then
                        Qry += " AND ((XXX.[Parent Code] IN  (" + clsCommon.GetMulcallString(ParentCustomerList) + ") and XXX.[Customer Id] in (" + clsCommon.GetMulcallString(CustomerList) + ")) or XXX.[Customer Id] IN  (" + clsCommon.GetMulcallString(ParentCustomerList) + "))"
                    Else
                        Qry += " AND (XXX.[Parent Code] IN  (" + clsCommon.GetMulcallString(ParentCustomerList) + ") or XXX.[Customer Id] IN  (" + clsCommon.GetMulcallString(ParentCustomerList) + "))"
                    End If

                Else
                    If CustomerList IsNot Nothing AndAlso CustomerList.Count > 0 Then
                        Qry += " and XXX.[Customer Id] in (" + clsCommon.GetMulcallString(CustomerList) + ") "
                    End If
                End If
            Else
                Dim AllowtoSHOWParentChildCustomer As Boolean = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowtoSHOWParentChildCustomer, clsFixedParameterCode.AllowtoSHOWParentChildCustomer, Nothing)) = 1, True, False))
                If AllowtoSHOWParentChildCustomer = True Then
                    If CustomerList IsNot Nothing AndAlso CustomerList.Count > 0 Then
                        Qry += " and (XXX.[Customer Id] in (" + clsCommon.GetMulcallString(CustomerList) + ") or XXX.[Parent Code]  in (" + clsCommon.GetMulcallString(CustomerList) + ") ) "
                    End If
                Else
                    If CustomerList IsNot Nothing AndAlso CustomerList.Count > 0 Then
                        Qry += " and XXX.[Customer Id] in (" + clsCommon.GetMulcallString(CustomerList) + ") "
                    End If
                End If

            End If

            'If CustomerList IsNot Nothing AndAlso CustomerList.Count > 0 Then
            '    Qry += " and XXX.[Customer Id] in (" + clsCommon.GetMulcallString(CustomerList) + ") "
            'End If
            If LocationList IsNot Nothing AndAlso LocationList.Count > 0 Then
                Qry += " and XXX.Location in (" + clsCommon.GetMulcallString(LocationList) + ") "
            End If
            If CustomerGroupList IsNot Nothing AndAlso CustomerGroupList.Count > 0 Then
                Qry += " and TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + clsCommon.GetMulcallString(CustomerGroupList) + ") "
            End If
            ''richa 12 Dec, 2016
            'Qry += " AND XXX.[Document Id]  NOT IN (sELECT TSPL_RECEIPT_DETAIL.Document_No FROM TSPL_RECEIPT_DETAIL LEFT OUTER JOIN TSPL_RECEIPT_HEADER rh  ON TSPL_RECEIPT_DETAIL.Receipt_No  =rh.Receipt_No left outer join TSPL_BANK_REVERSE on isnull(TSPL_BANK_REVERSE.Document_No,'') = rh.Receipt_No  WHERE TSPL_RECEIPT_DETAIL.Document_No=" + Environment.NewLine & _
            '" XXX.[Document Id] AND TSPL_RECEIPT_DETAIL.Applied_Amount =case when XXX.[Due Amount]  <0 then XXX.[Due Amount]* -1 else XXX.[Due Amount] end" + Environment.NewLine & _
            '" AND XXX.Document_Type IN ('CR','DR') AND rh.Posted='Y' AND ISNULL(TSPL_RECEIPT_DETAIL.Document_No,'')<>'' AND Convert(Date, rh.Receipt_Date , 103) <=convert(date,'" & CutOfDate & "',103) and isnull(TSPL_BANK_REVERSE.Document_No,'')<>rh.Receipt_No )"
            Qry += " AND XXX.ARINvoiceNo  NOT IN (sELECT TSPL_RECEIPT_DETAIL.Document_No FROM TSPL_RECEIPT_DETAIL LEFT OUTER JOIN TSPL_RECEIPT_HEADER rh  ON TSPL_RECEIPT_DETAIL.Receipt_No  =rh.Receipt_No left outer join TSPL_BANK_REVERSE on isnull(TSPL_BANK_REVERSE.Document_No,'') = rh.Receipt_No  WHERE TSPL_RECEIPT_DETAIL.Document_No=" + Environment.NewLine & _
           " XXX.ARINvoiceNo AND TSPL_RECEIPT_DETAIL.Applied_Amount =case when XXX.[Due Amount]  <0 then XXX.[Due Amount]* -1 else XXX.[Due Amount] end" + Environment.NewLine & _
           " AND XXX.Document_Type IN ('CR','DR') AND rh.Posted='Y' AND ISNULL(TSPL_RECEIPT_DETAIL.Document_No,'')<>'' AND Convert(Date, rh.Receipt_Date , 103) <=convert(date,'" & CutOfDate & "',103) and isnull(TSPL_BANK_REVERSE.Document_No,'')<>rh.Receipt_No )"


            ''--------
            Qry += " Group By XXX.[Customer Id], XXX.[Document Id]" & Environment.NewLine & _
            " ) Query  LEFT OUTER JOIN TSPL_COMPANY_MASTER  on TSPL_COMPANY_MASTER.Comp_Code = Query .Comp_Code  Where 1=1  AND " & Environment.NewLine & _
            " Query.[Document Id] not in (Select  UnApplied_No  from TSPL_RECEIPT_HEADER ForUnapliedEntry_bankReverse_Exclude " & Environment.NewLine & _
            " where ForUnapliedEntry_bankReverse_Exclude.UnApplied_No =Query .[Document Id]  and isnull(UnApplied_No,'')<>''  and" & Environment.NewLine & _
            " ForUnapliedEntry_bankReverse_Exclude.Receipt_No  in (  Select Document_No from TSPL_BANK_REVERSE where  TSPL_BANK_REVERSE.Source_Type ='AR' and " & Environment.NewLine & _
            " TSPL_BANK_REVERSE.Document_No = ForUnapliedEntry_bankReverse_Exclude.Receipt_No and CONVERT(DATE,TSPL_BANK_REVERSE.Reversal_Date,103)<=CONVERT(DATE,'" & CutOfDate & "',103) " & Environment.NewLine & _
            " and isnull(TSPL_BANK_REVERSE.Post ,'')='P')   )  AND Query.[Document Id] not in ( Select distinct a.Document_No from TSPL_REVALUATION_DETAIL " & Environment.NewLine & _
            " inner join (Select RefDocNo,Document_No   from TSPL_Customer_Invoice_Head where RefDocType ='REVALUATION ENTRY') a on a.RefDocNo =TSPL_REVALUATION_DETAIL.Document_No " & Environment.NewLine & _
            " inner join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Document_No =TSPL_REVALUATION_DETAIL.AR_Invoice_No  " & Environment.NewLine & _
            " where  isnull(TSPL_REVALUATION_DETAIL.AR_Invoice_No ,'')<>'' union all Select Document_No  from TSPL_Customer_Invoice_Head where Document_Type ='C' and " & Environment.NewLine & _
            " (ISNULL (Against_Sale_Return_No,'') IN (Select Document_Code from TSPL_SD_SALE_rETURN_HEAD WHERE Document_Code " & Environment.NewLine & _
            " IN (Select Against_Sale_Return_No  from TSPL_Customer_Invoice_Head where Document_Type ='C' and ISNULL (Against_Sale_Return_No,'')<>'') AND ISNULL(Against_Invoice_No,'')<>'') ) " & Environment.NewLine & _
            " union all Select Document_No  from TSPL_Customer_Invoice_Head where Document_Type ='C' and ISNULL (Against_Sale_Return_No,'')<>'' AND ISNULL(Trans_Type,'') ='BSR'  union all Select Document_No  from TSPL_Customer_Invoice_Head where Document_Type ='C' and (ISNULL (Against_MCC_Material_Sale_Return,'') IN (Select Document_Code from TSPL_SD_SALE_rETURN_HEAD WHERE Document_Code IN (Select Against_MCC_Material_Sale_Return  from TSPL_Customer_Invoice_Head where Document_Type ='C' and ISNULL (Against_MCC_Material_Sale_Return,'')<>'') AND ISNULL(Against_Invoice_No,'')<>'') )  union all Select Document_No  from TSPL_Customer_Invoice_Head where Document_Type ='C' and (ISNULL (AgainstScrapReturn,'') IN (Select Document_No from TSPL_SCRAPSALE_HEAD_RETURN WHERE Document_No in (Select AgainstScrapReturn  from TSPL_Customer_Invoice_Head where Document_Type ='C' and ISNULL (AgainstScrapReturn,'')<>'') ) )) " & Environment.NewLine
            ''" order by Query  .[Customer Id],Query .[Document Date]"


            Return Qry
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvCustomer.CellValueChanged, gvCustomer.CellValueChanged
        Try
            If Not isCellValueChangedOpen Then
                isCellValueChangedOpen = True
                If e.Column Is gvCustomer.Columns(colDeductionAmount) Then
                    CalculateRow(gvCustomer.CurrentRow.Index)
                End If
                UpdateAllTotals()
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub CalculateRow(ByVal RowIndex As Integer)
        gvCustomer.Rows(RowIndex).Cells(colAmount).Value = Math.Round(clsCommon.myCdbl(gvCustomer.Rows(RowIndex).Cells(colIncentiveAmount).Value) - clsCommon.myCdbl(gvCustomer.Rows(RowIndex).Cells(colDeductionAmount).Value), 2, MidpointRounding.ToEven)
    End Sub

    Private Sub btnReverse_Click(sender As Object, e As EventArgs) Handles btnReverse.Click
        Try
            If clsCommon.myLen(txtCode.Value) > 0 Then
                If clsCommon.MyMessageBoxShow(Me, "Reverese and unpost current document " + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                    clsCustomerIncentiveEntryHead.ReverseAndUnpostData(txtCode.Value)
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub chkApplyDateRange_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkApplyDateRange.ToggleStateChanged
        Panel4.Visible = False
        If chkApplyDateRange.Checked Then
            Panel4.Visible = True
            setFromAndToDate()
        End If
    End Sub

    Private Sub txtMonth_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtMonth.Validating
        setFromAndToDate()
    End Sub

    Sub setFromAndToDate()
        txtFromDate.Value = New Date(txtMonth.Value.Year, txtMonth.Value.Month, 1)
        txtToDate.Value = New Date(txtMonth.Value.Year, txtMonth.Value.Month, 1).AddMonths(1).AddDays(-1)
    End Sub

    Private Sub rmSummary_Click(sender As Object, e As EventArgs) Handles rmSummary.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Document Not Found", Me.Text)
                Return
            End If
            Dim qry As String = ""
            Dim dtFrom As DateTime = New Date(txtMonth.Value.Year, txtMonth.Value.Month, 1)
            Dim dtTo As DateTime = New Date(txtMonth.Value.Year, txtMonth.Value.Month, 1).AddMonths(1).AddDays(-1)
            'Dim dtFromPendingLeger As DateTime = Nothing
            'Dim dtToPendingLeger As DateTime = Nothing
            'Dim dtNextMoth As DateTime = New Date(txtMonth.Value.Year, txtMonth.Value.Month, 1).AddMonths(1)
            'Dim dtNexMothFrom As DateTime = dtNextMoth
            'Dim whrforPending As String = ""
            If chkApplyDateRange.Checked = True Then
                'dtFromPendingLeger = txtFromDate.Value
                'dtToPendingLeger = txtToDate.Value
                dtFrom = txtFromDate.Value
                dtTo = txtToDate.Value
            End If
            'Dim Query As String = "select * from TSPL_Fiscal_Year_Master where '" + clsCommon.GetPrintDate(txtMonth.Value, "dd/MMM/yyyy") + "' between start_Date and End_Date  "
            'Dim dtFinYear As DataTable = clsDBFuncationality.GetDataTable(Query)
            'If dtFinYear IsNot Nothing AndAlso dtFinYear.Rows.Count > 0 Then
            '    dtFromPendingLeger = clsCommon.myCDate(dtFinYear.Rows(0)("start_Date"))
            '    dtToPendingLeger = clsCommon.myCDate(dtFinYear.Rows(0)("End_Date"))
            'End If

            'qry = "  select TSPL_CUSTOMER_INCENTIVE_CUSTOMER_INCENTIVE_WISE.Cust_Code,'' as Structure_Code ,TSPL_CUSTOMER_INCENTIVE_CUSTOMER_INCENTIVE_WISE.Incentive_Qty  as Discount_Total_Ltr,TSPL_CUSTOMER_INCENTIVE_CUSTOMER_INCENTIVE_WISE.Incentive_UOM  as Discount_Total_Ltr_UOM,TSPL_CUSTOMER_INCENTIVE_CUSTOMER_INCENTIVE_WISE.Range_Qty  as Discount_Total_Crts, TSPL_CUSTOMER_INCENTIVE_CUSTOMER_INCENTIVE_WISE.Range_UOM as Discount_Total_Crts_Uom ,TSPL_CUSTOMER_INCENTIVE_CUSTOMER_INCENTIVE_WISE. Range_AVg_Qty  as Discount_Avg_Qty,TSPL_CUSTOMER_INCENTIVE_CUSTOMER_INCENTIVE_WISE.Incentive_Rate  as " & _
            '      "  Discount_TDSlab,   TSPL_CUSTOMER_INCENTIVE_CUSTOMER_INCENTIVE_WISE.Incentive_Amount  as Discount_TDAmount   from TSPL_CUSTOMER_INCENTIVE_CUSTOMER_INCENTIVE_WISE " & _
            '      "  where  Doc_Code = '" + txtCode.Value + "' "
            'Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry)
            'If dt2.Rows.Count > 0 Then
            '    Dim type As String = "Aged Trial Balance By Document date"
            '    Dim IsFifoBased As String = "N"
            '    Dim ArryLst As New ArrayList
            '    ArryLst.Add("IN")
            '    ArryLst.Add("DB")
            '    ArryLst.Add("CR")
            '    ArryLst.Add("RC")

            '    ArryLst.Add("UC")
            '    ArryLst.Add("SR")

            '    ArryLst.Add("AD")
            '    ArryLst.Add("RF")

            '    ArryLst.Add("AV")
            '    ArryLst.Add("OA")
            '    ArryLst.Add("VGCL")
            '    Dim ArryLstLocation As New ArrayList
            '    ArryLstLocation.Add(txtLocation.Value)
            '    Dim CheckCustomer As String
            '    CheckCustomer = " AND TSPL_CUSTOMER_MASTER.Status='N'"
            '    Dim rptHeading As String
            '    rptHeading = "Aged Trial Balance Report"
            '    Dim isonduedate As String = String.Empty
            '    isonduedate = "DueDate"

            '    If clsCommon.myLen(clsCommon.myCstr(dtFromPendingLeger)) > 0 AndAlso clsCommon.myLen(clsCommon.myCstr(dtToPendingLeger)) > 0 Then
            '        dtNexMothFrom = dtFromPendingLeger
            '        dtNextMoth = dtToPendingLeger
            '    End If
            '    whrforPending = " and convert (date,Final.[Document Date],103) >= convert (date, '" + dtFromPendingLeger + "',103)  and convert (date,Final.[Document Date],103) <= convert (date,'" + dtToPendingLeger + "',103)"
            '    Dim strInnerQry As String = GetOutStandingQry(clsCommon.myCDate(clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtToPendingLeger), "dd/MMM/yyyy hh:mm:ss tt")), clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtToPendingLeger), "dd/MMM/yyyy hh:mm:ss tt"), False, ArryLst, isonduedate, "ConvRate", IIf((txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0), txtCustomer.arrValueMember, Nothing), ArryLstLocation, Nothing, False, Nothing, IIf(False, "", "AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N'"))

            '    Dim dtPendingLeger_qry As String = "  select '" + clsCommon.GetPrintDate(dtNexMothFrom, "dd/MMM/yyyy") + "' as 'dtNexMothFrom', '" + clsCommon.GetPrintDate(dtNextMoth, "dd/MMM/yyyy") + "' as 'dtNextMonth' , Final.[Document Id],convert (varchar, Final.[Document Date],103) as [Document Date] ,Final.[Customer Id] as [Cust_Code] , Final.[Customer Name], Final.[Due Amount], Case when Document_Type not in ('CR','OA','AV') then  Final.[Document Amount] else 0 end [Document Amount], " & _
            '                                       " Case when Document_Type in ('CR','OA','AV') then   (isnull(Final.[Document Amount],0) - (isnull(Final.[Due Amount],0) * (-1)) ) else (isnull(Final.[Document Amount],0) - (isnull(Final.[Due Amount],0)) ) end  + Case when Document_Type in ('CR','OA','AV') then isnull(Final.[Document Amount],0) else 0 end  as [Receipt Amount] from (  " & _
            '                                       " " + strInnerQry + "   " & _
            '                                       "   ) Final where Final.[Due Amount] <> 0   " + whrforPending + "  order by Final.[Customer Id], convert (datetime ,Final.[Document Date],103) "  ' where Final.[Due Amount] > 0
            '    Dim dtPendingLedger As DataTable = clsDBFuncationality.GetDataTable(dtPendingLeger_qry)

            '    ' CTE ===================================================================================================
            '    qry = "  WITH PenidngAmountTotal (Cust_Code,TotalDueAmount)  AS  ( select xxxx.Cust_Code ,sum (xxxx.[Due Amount])  as TotalDueAmount from ( " & _
            '          "  select '" + clsCommon.GetPrintDate(dtNexMothFrom, "dd/MMM/yyyy") + "' as 'dtNexMothFrom', '" + clsCommon.GetPrintDate(dtNextMoth, "dd/MMM/yyyy") + "' as 'dtNextMonth' , Final.[Document Id],convert (varchar, Final.[Document Date],103) as [Document Date] ,Final.[Customer Id] as [Cust_Code] , Final.[Customer Name], Final.[Due Amount], Case when Document_Type not in ('CR','OA','AV') then  Final.[Document Amount] else 0 end [Document Amount], " & _
            '          "  Case when Document_Type in ('CR','OA','AV') then   (isnull(Final.[Document Amount],0) - (isnull(Final.[Due Amount],0) * (-1)) ) else (isnull(Final.[Document Amount],0) - (isnull(Final.[Due Amount],0)) ) end  + Case when Document_Type in ('CR','OA','AV') then isnull(Final.[Document Amount],0) else 0 end  as [Receipt Amount] from (  " & _
            '          "  " + strInnerQry + "   " & _
            '          "   ) Final where Final.[Due Amount] <> 0   " + whrforPending + "  " & _
            '          "  ) XXXX group by Cust_Code  )  "
            '    'If SettDayWiseCustomerIncentiveCalculation = True Then '  
            '    qry += "  select TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD.Doc_Date,'" + clsCommon.GetPrintDate(dtFrom, "dd/MMM/yyyy") + "' as 'dtFrom', '" + clsCommon.GetPrintDate(dtTo, "dd/MMM/yyyy") + "' as 'dtTo',TSPL_COMPANY_MASTER.Comp_Name, TSPL_CUSTOMER_INCENTIVE_DETAIL.Doc_Code,TSPL_CUSTOMER_MASTER.Zone_Code,TSPL_Zone_Master.Description as Zone_Name,TSPL_CUSTOMER_INCENTIVE_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.OldName,TSPL_CUSTOMER_MASTER.Customer_Name,TBL_Incentive.Incentive_Qty,(TBL_Incentive.Incentive_Qty/ (Case when TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD.Filter_Month_From is Null and Filter_Month_To is Null then   DAY(EOMONTH(Filter_Month)) else DATEDIFF(day,convert(date,Filter_Month_From,103),convert(date,Filter_Month_To,103))+1  end )) as MothAvg, " & _
            '           "  Cast (Round ((TBL_Incentive.Incentive_Qty/ (Case when TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD.Filter_Month_From is Null and Filter_Month_To is Null then   DAY(EOMONTH(Filter_Month)) else DATEDIFF(day,convert(date,Filter_Month_From,103),convert(date,Filter_Month_To,103))+1  end )),0) as Integer) as MothAvgWithRoundOff, TSPL_CUSTOMER_INCENTIVE_DETAIL.Incentive_Amount as VendorMargin,TSPL_CUSTOMER_INCENTIVE_DETAIL.Deduction_Amount, " & _
            '           "  isnull(PenidngAmountTotal.TotalDueAmount,0) as TotalDueAmount , TSPL_CUSTOMER_INCENTIVE_DETAIL.Amount, isnull(TSPL_CUSTOMER_INCENTIVE_DETAIL.Incentive_Amount,0) - isnull(TSPL_CUSTOMER_INCENTIVE_DETAIL.Deduction_Amount,0) - isnull(TotalDueAmount,0)   as NetAmount from TSPL_CUSTOMER_INCENTIVE_DETAIL " & _
            '           "  Left Outer Join TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD on TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD.Doc_Code = TSPL_CUSTOMER_INCENTIVE_DETAIL.Doc_Code " & _
            '           "  Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_CUSTOMER_INCENTIVE_DETAIL.Cust_Code " & _
            '           "  Left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code " & _
            '           "  Left Outer Join ( " & _
            '           "  Select max(TSPL_CUSTOMER_INCENTIVE_ITEM_CUSTOMER_WISE.Doc_Code) as Doc_Code, TSPL_CUSTOMER_INCENTIVE_ITEM_CUSTOMER_WISE.Cust_Code,Sum (TSPL_CUSTOMER_INCENTIVE_ITEM_CUSTOMER_WISE.Incentive_Qty ) as Incentive_Qty from TSPL_CUSTOMER_INCENTIVE_ITEM_CUSTOMER_WISE " & _
            '           "  where TSPL_CUSTOMER_INCENTIVE_ITEM_CUSTOMER_WISE.Doc_Code = '" + txtCode.Value + "' group by TSPL_CUSTOMER_INCENTIVE_ITEM_CUSTOMER_WISE.Cust_Code " & _
            '           "  )TBL_Incentive on TBL_Incentive.Cust_Code = TSPL_CUSTOMER_INCENTIVE_DETAIL.Cust_Code and TBL_Incentive.Doc_Code = TSPL_CUSTOMER_INCENTIVE_DETAIL.Doc_Code " & _
            '           "  Left outer join PenidngAmountTotal on PenidngAmountTotal.Cust_Code = TSPL_CUSTOMER_INCENTIVE_DETAIL.Cust_Code " & _
            '           "  Left Outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = TSPL_CUSTOMER_MASTER.Comp_Code " & _
            '           "  where TSPL_CUSTOMER_INCENTIVE_DETAIL.Doc_Code = '" + txtCode.Value + "' "


            qry = "select TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD.Doc_Date,'" + clsCommon.GetPrintDate(dtFrom, "dd/MMM/yyyy") + "' as 'dtFrom', '" + clsCommon.GetPrintDate(dtTo, "dd/MMM/yyyy") + "' as 'dtTo',TSPL_COMPANY_MASTER.Comp_Name,TSPL_CUSTOMER_INCENTIVE_DETAIL.Doc_Code,TSPL_CUSTOMER_MASTER.Zone_Code,TSPL_Zone_Master.Description as Zone_Name,TSPL_CUSTOMER_INCENTIVE_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.OldName,TSPL_CUSTOMER_MASTER.Customer_Name,TBL_Incentive.Incentive_Qty,TSPL_CUSTOMER_INCENTIVE_DETAIL.Avg_Qty as MothAvg,   TSPL_CUSTOMER_INCENTIVE_DETAIL.Avg_Qty  as MothAvgWithRoundOff, TSPL_CUSTOMER_INCENTIVE_DETAIL.Incentive_Amount as VendorMargin,TSPL_CUSTOMER_INCENTIVE_DETAIL.Deduction_Amount,TSPL_CUSTOMER_INCENTIVE_DETAIL.Dues as TotalDueAmount,TSPL_CUSTOMER_INCENTIVE_DETAIL.Amount,TSPL_CUSTOMER_INCENTIVE_DETAIL.Security_To_Be_Taken, TSPL_CUSTOMER_INCENTIVE_DETAIL.Net_Margin_Payable as NetAmount " + Environment.NewLine +
                "from TSPL_CUSTOMER_INCENTIVE_DETAIL   " + Environment.NewLine +
                "Left Outer Join TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD on TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD.Doc_Code = TSPL_CUSTOMER_INCENTIVE_DETAIL.Doc_Code   " + Environment.NewLine +
                "Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_CUSTOMER_INCENTIVE_DETAIL.Cust_Code   " + Environment.NewLine +
                "Left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code   " + Environment.NewLine +
                "Left Outer Join (   Select max(TSPL_CUSTOMER_INCENTIVE_ITEM_CUSTOMER_WISE.Doc_Code) as Doc_Code, TSPL_CUSTOMER_INCENTIVE_ITEM_CUSTOMER_WISE.Cust_Code,Sum (TSPL_CUSTOMER_INCENTIVE_ITEM_CUSTOMER_WISE.Incentive_Qty ) as Incentive_Qty from TSPL_CUSTOMER_INCENTIVE_ITEM_CUSTOMER_WISE   where TSPL_CUSTOMER_INCENTIVE_ITEM_CUSTOMER_WISE.Doc_Code = '" + txtCode.Value + "' group by TSPL_CUSTOMER_INCENTIVE_ITEM_CUSTOMER_WISE.Cust_Code   )TBL_Incentive on TBL_Incentive.Cust_Code = TSPL_CUSTOMER_INCENTIVE_DETAIL.Cust_Code and TBL_Incentive.Doc_Code = TSPL_CUSTOMER_INCENTIVE_DETAIL.Doc_Code   " + Environment.NewLine +
                "Left Outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = TSPL_CUSTOMER_MASTER.Comp_Code   " + Environment.NewLine +
                "where TSPL_CUSTOMER_INCENTIVE_DETAIL.Doc_Code = '" + txtCode.Value + "' "


                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                    Dim obj As clsDosPrint = New clsDosPrint()
                    obj.ReportName = objCommonVar.CurrentCompanyName
                    obj.ReportName1 = "Abstract of Vendor Margin"
                    obj.ShowPageNo = True
                    obj.arrFilter = New List(Of clsDosPrintHeaderFilter)()
                    obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("Date Range ", clsCommon.myCstr(clsCommon.GetPrintDate(dtFrom, "dd/MM/yyyy")) + " to " + clsCommon.myCstr(clsCommon.GetPrintDate(dtTo, "dd/MM/yyyy"))))
                    'obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("Shift ", clsCommon.myCstr(cboABSReportShift.SelectedValue)))
                    'obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("Type", clsCommon.myCstr(cboABSReportType.SelectedValue)))
                    obj.arrColumn = New List(Of clsDosPrintColumn)()
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Zone_Name", "Znrt", True, DosPrintAlignment.Left, 10, False, DecimalPlaces.NA))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Cust_Code", "Vndr Id", True, DosPrintAlignment.Left, 10, False, DecimalPlaces.NA))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("OldName", "Loc", True, DosPrintAlignment.Left, 10, False, DecimalPlaces.NA))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Customer_Name", " Vndr   Name", True, DosPrintAlignment.Left, 10, False, DecimalPlaces.NA))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Incentive_Qty", "Tot Qty", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("MothAvgWithRoundOff", "Mth Avg", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("VendorMargin", " Vndr   Margin", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Deduction_Amount", "SD Amt", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("TotalDueAmount", "Dues", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Security_To_Be_Taken", "Security Taken", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("NetAmount", " Net   Margin", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))
                    obj.Print(obj, dt, PageSetup.Potrate)
                Else
                clsCommon.MyMessageBoxShow(Me, "Data Not Found.", Me.Text)
            End If
            ' End If
            'End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        
    End Sub

    Private Sub rmDetails_Click(sender As Object, e As EventArgs) Handles rmDetails.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Document Not Found", Me.Text)
                Return
            End If
            Dim dtFrom As DateTime = New Date(txtMonth.Value.Year, txtMonth.Value.Month, 1)
            Dim dtTo As DateTime = New Date(txtMonth.Value.Year, txtMonth.Value.Month, 1).AddMonths(1).AddDays(-1)
            If chkApplyDateRange.Checked Then
                dtFrom = txtFromDate.Value
                dtTo = txtToDate.Value
            End If

            Dim qry As String = "  Select  DateFinal.Cust_Code+'('+DateFinal.Customer_Name+')'+'  Location:'+DateFinal.Location_Code+'('+DateFinal.Location_Desc+')' as  GroupColumn, DateFinal.thedate,DateFinal.[Day],DateFinal.Cust_Code, DateFinal.Customer_Name,DateFinal.Location_Code,DateFinal.Location_Desc ,KKK.Document_Date   " & _
                                " ,isnull (KKK.Cash,0) as Cash   , isnull(KKK.CD,0) as CD, isnull(KKK.CR,0) as CR, isnull(KKK.SO,0) as SO, ( isnull (KKK.Cash,0) + isnull(KKK.CD,0) + isnull(KKK.CR,0) + isnull(KKK.SO,0) ) as Total  ,isnull(KKK.Cash2,0) as Cash2,isnull (KKK.CD2,0) as CD2,isnull(KKK.CR2,0) as CR2, isnull (KKK.SO2,0) as SO2 , (isnull(KKK.Cash2,0) + isnull (KKK.CD2,0) + isnull(KKK.CR2,0) +  isnull (KKK.SO2,0) ) as Total2   " & _
                                "  from (  " & _
                                "  Select TBL_SOURCE.Cust_Code,TBL_SOURCE.Customer_Name,TBL_SOURCE.Location_Code,TBL_SOURCE.Location_Desc ,TBL_DATE.thedate, TBL_DATE.[Day] from (Select TSPL_CUSTOMER_INCENTIVE_DETAIL.Cust_Code, '1' as  [Code], TSPL_Customer_Master.Customer_Name,TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD.Location_Code,TSPL_Location_Master.Location_Desc From TSPL_CUSTOMER_INCENTIVE_DETAIL Left Outer Join TSPL_Customer_Master on TSPL_Customer_Master.Cust_Code =TSPL_CUSTOMER_INCENTIVE_DETAIL.Cust_Code Left Outer Join TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD on TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD.Doc_Code = TSPL_CUSTOMER_INCENTIVE_DETAIL.Doc_Code Left Outer Join TSPL_Location_Master on TSPL_Location_Master.Location_Code = TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD.Location_Code  where TSPL_CUSTOMER_INCENTIVE_DETAIL.Doc_Code = '" + txtCode.Value + "' ) TBL_SOURCE   " & _
                                "  Left Outer Join   " & _
                                "  (select  convert (varchar,thedate,103) as thedate, DATEPART (day,thedate) as [Day], '1' as  Code  from ExplodeDates (convert (date,'01/01/2020',103) , Convert (date,'31/01/2020',103))) TBL_DATE on TBL_DATE.Code = TBL_SOURCE.Code ) DateFinal   " & _
                                "  Left Outer Join    " & _
                                "  (   " & _
                                "  Select HHHHHFinal.* from    " & _
                                "  (    " & _
                                "  Select Document_Date,Customer_Code,Customer_Name, isnull ([Cash],0) as [Cash],isnull ([CD],0) as [CD] ,isnull([CR],0) as [CR],isnull([SO],0) as [SO], isnull([Cash2],0) as [Cash2] ,isnull ([CD2],0) as [CD2] ,isnull ([CR2],0) as [CR2] ,isnull ([SO2],0) as [SO2] from (    " & _
                                "  Select XXXXFinal. *, Convert (decimal(18,2), (XXXXFinal.Incentive_Qty * XXXXFinal.INCENTIVE_AMOUNT) ) as Amount  from (   " & _
                                "  Select distinct XXXFinal. *  from (   " & _
                                "  Select  XXFinal.* , isnull (TBL_INCENTIVE.INCENTIVE,0) as INCENTIVE_AMOUNT,XXFinal.Booking_Type+'2' as Booking_Type_For_Amount from (   " & _
                                "  Select XFinal.Document_Date ,XFinal.Booking_Type , XFinal.customer_Code , max(XFinal.customer_Name) as customer_Name, max(XFinal.Structure_Code) as Structure_Code ,sum(XFinal.StockQty) as StockQty ,sum (XFinal.Incentive_Qty) as Incentive_Qty    " & _
                                "  from (   " & _
                                "  Select Final.*, Convert (decimal(18,2), (isnull (SourceUOM.Conversion_Factor,0)/nullif (TargetUOM.Conversion_Factor,0) ) * convert (decimal(18,2),isnull(Final.StockQty,0)))  as Incentive_Qty   " & _
                                "  from (   " & _
                                "  select Booking_Type,Customer_Code,max(Customer_Name) as Customer_Name,Item_Code,max(Item_Desc) as Item_Desc,Structure_Code as Structure_Code,sum(StockQty*RI) as StockQty,max(StcokUOM) as StcokUOM,Convert (varchar,Document_Date,103) as Document_Date from (select TSPL_BOOKING_MATSER.Booking_Type, TSPL_SD_SALE_INVOICE_DETAIL.Document_Code,convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Document_Date,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Structure_Code,TSPL_SD_SALE_INVOICE_DETAIL.Qty,TSPL_SD_SALE_INVOICE_DETAIL.Unit_code,(TSPL_SD_SALE_INVOICE_DETAIL.Qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as StockQty,StockUOMDetail.UOM_Code as StcokUOM,1 as RI   " & _
                                "  from TSPL_SD_SALE_INVOICE_DETAIL   " & _
                                "  left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE    " & _
                                "  Left Outer Join TSPL_BOOKING_DETAIL on TSPL_SD_SALE_INVOICE_Detail.Delivery_Code = TSPL_BOOKING_DETAIL.Delivery_No    " & _
                                "  and TSPL_SD_SALE_INVOICE_Detail.Item_Code = TSPL_BOOKING_DETAIL.Item_Code    " & _
                                "  Left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No   " & _
                                "  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.item_code   " & _
                                "  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code   " & _
                                "  left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SD_SALE_INVOICE_DETAIL.Unit_code    " & _
                                "  left outer join TSPL_ITEM_UOM_DETAIL as StockUOMDetail on StockUOMDetail.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and StockUOMDetail.Stocking_Unit='Y'    " & _
                                "  where TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS' and isnull(TSPL_SD_SALE_INVOICE_HEAD.IsSampling,0)=0 and isnull(TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item,'N')='N' and isnull(TSPL_SD_SALE_INVOICE_DETAIL.FOC_Item,0)=0 and TSPL_SD_SALE_INVOICE_HEAD.Document_Date>= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFrom), "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_SD_SALE_INVOICE_HEAD.Document_Date<= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtTo), "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location='" + txtLocation.Value + "' and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")    " & _
                                "  and exists (select 1 from TSPL_ITEM_UOM_DETAIL as innTSPL_ITEM_UOM_DETAIL left outer join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.Unit_Code=innTSPL_ITEM_UOM_DETAIL.UOM_Code where TSPL_UNIT_MASTER.Ltr_Type='Y' and innTSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code)   " & _
                                "  and not exists(select 1 from TSPL_CUSTOMER_INCENTIVE_INVOICE_WISE where TSPL_CUSTOMER_INCENTIVE_INVOICE_WISE.Doc_Code not in ('" + txtCode.Value + "') and TSPL_CUSTOMER_INCENTIVE_INVOICE_WISE.Invoice_Code=TSPL_SD_SALE_INVOICE_HEAD.Document_Code)    " & _
                                "  union all    " & _
                                " select TSPL_BOOKING_MATSER.Booking_Type,TSPL_SD_SALE_RETURN_DETAIL.Document_Code,TSPL_SD_SALE_RETURN_HEAD.Document_Date,TSPL_SD_SALE_RETURN_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_SD_SALE_RETURN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Structure_Code,TSPL_SD_SALE_RETURN_DETAIL.Qty,TSPL_SD_SALE_RETURN_DETAIL.Unit_code,(TSPL_SD_SALE_RETURN_DETAIL.Qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as StockQty,StockUOMDetail.UOM_Code as StcokUOM,-1 as RI   " & _
                                " from TSPL_SD_SALE_RETURN_DETAIL   " & _
                                " left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Document_Code=TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE    " & _
                                " Left Outer Join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.Document_Code = TSPL_SD_SALE_RETURN_DETAIL.Invoice_Code and TSPL_SD_SALE_INVOICE_DETAIL.Item_code = TSPL_SD_SALE_RETURN_DETAIL.Item_Code    " & _
                                " Left Outer Join TSPL_BOOKING_DETAIL on TSPL_SD_SALE_INVOICE_Detail.Delivery_Code = TSPL_BOOKING_DETAIL.Delivery_No    " & _
                                " and TSPL_SD_SALE_INVOICE_Detail.Item_Code = TSPL_BOOKING_DETAIL.Item_Code   " & _
                                " Left Outer Join TSPL_BOOKING_MATSER on TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No    " & _
                                " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.item_code    " & _
                                " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_RETURN_HEAD.Customer_Code    " & _
                                " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SD_SALE_RETURN_DETAIL.Unit_code    " & _
                                " left outer join TSPL_ITEM_UOM_DETAIL as StockUOMDetail on StockUOMDetail.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code and StockUOMDetail.Stocking_Unit='Y'    " & _
                                " where TSPL_SD_SALE_RETURN_HEAD.Screen_Type='DS' and isnull(TSPL_SD_SALE_RETURN_DETAIL.Scheme_Item,'N')='N' and isnull(TSPL_SD_SALE_RETURN_DETAIL.FOC_Item,0)=0 and TSPL_SD_SALE_RETURN_HEAD.Document_Date>= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFrom), "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_SD_SALE_RETURN_HEAD.Document_Date<= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtTo), "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location='" + txtLocation.Value + "' and TSPL_SD_SALE_RETURN_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")   " & _
                                " and exists (select 1 from TSPL_ITEM_UOM_DETAIL as innTSPL_ITEM_UOM_DETAIL left outer join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.Unit_Code=innTSPL_ITEM_UOM_DETAIL.UOM_Code where TSPL_UNIT_MASTER.Ltr_Type='Y' and innTSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code)   " & _
                                " and not exists(select 1 from TSPL_CUSTOMER_INCENTIVE_INVOICE_WISE where TSPL_CUSTOMER_INCENTIVE_INVOICE_WISE.Doc_Code not in ('" + txtCode.Value + "') and TSPL_CUSTOMER_INCENTIVE_INVOICE_WISE.Return_Code=TSPL_SD_SALE_RETURN_HEAD.Document_Code))xx group by Booking_Type,Customer_Code,Structure_Code,Item_Code,Document_Date having sum(StockQty*RI)>0    " & _
                                " ) Final    " & _
                                " left outer join tspl_item_uom_detail as SourceUOM  on SourceUOM.Item_Code = Final.Item_Code  AND SourceUOM.UOM_Code = Final.StcokUOM   " & _
                                " left Outer Join tspl_item_uom_detail as TargetUOM on TargetUOM.Item_Code = Final.Item_Code and TargetUOM.UOM_Code = 'LTR'   " & _
                                " ) XFinal  group by XFinal.Document_Date , XFinal.Booking_Type , XFinal.Customer_Code    " & _
                                " )XXFinal    " & _
                                " Inner Join  " & _
                                "  (    " & _
                                " select   TSPL_SALES_INCENTIVE_HEADER.FROM_DATE,TSPL_SALES_INCENTIVE_HEADER.TO_DATE, TSPL_SALES_INCENTIVE_SLAB.TR_CODE, TSPL_SALES_INCENTIVE_SLAB.INCENTIVE_CODE,TSPL_SALES_INCENTIVE_SLAB.FROM_RANGE,TSPL_SALES_INCENTIVE_SLAB.TO_RANGE,TSPL_SALES_INCENTIVE_HEADER.RANGE_UOM,TSPL_SALES_INCENTIVE_SLAB.INCENTIVE,TSPL_SALES_INCENTIVE_HEADER.INCENTIVE_UOM,TSPL_SALES_INCENTIVE_ITEM_STRUCTURE_MAPPING.Structure_Code ,TSPL_SALES_INCENTIVE_CUSTOMER_MAPPING.CUSTOMER_CODE   " & _
                                " from TSPL_SALES_INCENTIVE_HEADER    " & _
                                " left outer join TSPL_SALES_INCENTIVE_SLAB on TSPL_SALES_INCENTIVE_SLAB.INCENTIVE_CODE=TSPL_SALES_INCENTIVE_HEADER.INCENTIVE_CODE   " & _
                                " left outer join TSPL_SALES_INCENTIVE_CUSTOMER_MAPPING on TSPL_SALES_INCENTIVE_CUSTOMER_MAPPING.INCENTIVE_CODE=TSPL_SALES_INCENTIVE_HEADER.INCENTIVE_CODE   " & _
                                " left outer join TSPL_SALES_INCENTIVE_ITEM_STRUCTURE_MAPPING on TSPL_SALES_INCENTIVE_ITEM_STRUCTURE_MAPPING.INCENTIVE_CODE=TSPL_SALES_INCENTIVE_HEADER.INCENTIVE_CODE    " & _
                                " where TSPL_SALES_INCENTIVE_HEADER.Status = 1 And TSPL_SALES_INCENTIVE_HEADER.In_Active = 0   " & _
                                " and '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFrom), "dd/MMM/yyyy hh:mm:ss tt") + "'  between TSPL_SALES_INCENTIVE_HEADER.FROM_DATE and TSPL_SALES_INCENTIVE_HEADER.TO_DATE   " & _
                                " ) TBL_INCENTIVE on TBL_INCENTIVE.Structure_Code = XXFinal.Structure_Code    " & _
                                " and XXFinal.StockQty between TBL_INCENTIVE.FROM_RANGE  and TO_RANGE    " & _
                                " ) XXXFinal where XXXFinal.INCENTIVE_AMOUNT > 0   " & _
                                " ) XXXXFinal   " & _
                                " ) HHHHH    " & _
                                " pivot  (  max(HHHHH.Incentive_Qty) for HHHHH.Booking_Type in ([Cash],[CD],[CR],[SO]) )as pivots   " & _
                                " pivot  (  max(Amount) for Booking_Type_For_Amount in ([Cash2],[CD2],[CR2],[SO2]) )as pivots2   " & _
                                " ) HHHHHFinal ) KKK on  KKK.Document_Date = DateFinal.theDate and DateFinal.Cust_Code = KKK.Customer_Code order by Cust_code, [Day] asc  "


            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                Dim obj As clsDosPrint = New clsDosPrint()
                obj.ReportName = objCommonVar.CurrentCompanyName
                obj.ReportName1 = "VENDOR WISE MARGIN DETAILS ON MILK SALE : (" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFrom), "dd/MMM/yyyy") + " - " + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtTo), "dd/MMM/yyyy") + ")"
                obj.ShowPageNo = True

                obj.arrGroup = New List(Of clsDosPrintGroup)()
                'obj.arrGroup.Add(clsDosPrintGroup.GetObject("Cust_Code", "VENDORID", ""))
                obj.arrGroup.Add(clsDosPrintGroup.GetObject("GroupColumn", "VENDOR", ""))

                obj.arrFilter = New List(Of clsDosPrintHeaderFilter)()
                'obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("Date Range ", clsCommon.GetPrintDate(dtFrom, "dd/MM/yyyy")))
                'obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("Shift ", clsCommon.myCstr(cboABSReportShift.SelectedValue)))
                'obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("Type", clsCommon.myCstr(cboABSReportType.SelectedValue)))
                obj.arrColumn = New List(Of clsDosPrintColumn)()
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Day", "Day", True, DosPrintAlignment.Left, 8, False, DecimalPlaces.NA))
                'obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Cust_Code", "Cust", True, DosPrintAlignment.Left, 8, False, DecimalPlaces.NA))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("CR", "  CR  <--", False, DosPrintAlignment.Right, 9, True, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("CD", "CD", False, DosPrintAlignment.Right, 9, True, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("SO", "  SO  Qty", False, DosPrintAlignment.Right, 9, True, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Cash", "Cash", False, DosPrintAlignment.Right, 9, True, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Total", "  Total -->", False, DosPrintAlignment.Right, 11, True, DecimalPlaces.Two))

                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("CR2", "  CR  <--", False, DosPrintAlignment.Right, 9, True, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("CD2", "CD", False, DosPrintAlignment.Right, 9, True, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("SO2", "  SO  Amount", False, DosPrintAlignment.Right, 9, True, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Cash2", "Cash", False, DosPrintAlignment.Right, 9, True, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Total2", "      Total-->", False, DosPrintAlignment.Right, 15, True, DecimalPlaces.Two))

                obj.Print(obj, dt, PageSetup.Potrate)



            Else
                clsCommon.MyMessageBoxShow(Me, "Data Not Found.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
       

    End Sub

    Sub CalculateSecurity()
        If SettCustomerIncetiveAutoSecuity Then
            Dim d1 As DateTime = txtMonth.Value
            Dim d2 As DateTime = txtMonth.Value
            If chkApplyDateRange.Checked Then
                d1 = txtFromDate.Value
                d2 = txtToDate.Value
            Else
                d1 = New Date(txtMonth.Value.Year, txtMonth.Value.Month, 1)
                d2 = d1.AddMonths(1).AddDays(-1)
            End If
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
                gvCustomer.Rows(ii).Cells(colAdditionalSecurityDepositAmt).Value = Math.Round((clsCommon.myCdbl(gvCustomer.Rows(ii).Cells(colAvgQty).Value) * txtADSecuity.Value), 2, MidpointRounding.ToEven)
                If clsCommon.myCdbl(gvCustomer.Rows(ii).Cells(colDues).Value) > clsCommon.myCdbl(gvCustomer.Rows(ii).Cells(colAmount).Value) Then
                    If txtSecuityPart.Value > 0 Then
                        gvCustomer.Rows(ii).Cells(colSecurityToBeTaken).Value = clsCommon.myCdbl(gvCustomer.Rows(ii).Cells(colAdditionalSecurityDepositAmt).Value) / txtSecuityPart.Value
                    End If
                End If
                gvCustomer.Rows(ii).Cells(colNetMarginPayable).Value = clsCommon.myCdbl(gvCustomer.Rows(ii).Cells(colAmount).Value) - clsCommon.myCdbl(gvCustomer.Rows(ii).Cells(colDues).Value) - clsCommon.myCdbl(gvCustomer.Rows(ii).Cells(colSecurityToBeTaken).Value)
                If clsCommon.myCdbl(gvCustomer.Rows(ii).Cells(colNetMarginPayable).Value) < 0 Then
                    gvCustomer.Rows(ii).Cells(colNetMarginPayable).Value = 0
                End If
            Next
        End If
    End Sub

    Private Sub btnExportExcel_Click(sender As Object, e As EventArgs) Handles btnExportExcel.Click
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(" Document Code : " + txtCode.Value)
            arrHeader.Add(" Date : " + clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy") + "")
            arrHeader.Add(" Location : " + txtLocation.Value)
            arrHeader.Add(" Month : " + txtMonth.Text)
            If clsCommon.CompairString(Me.RadPageView1.SelectedPage.Name, "RadPageViewPage4") = CompairStringResult.Equal AndAlso gvCustomer.Rows.Count > 0 Then
                clsCommon.MyExportToExcelGrid("Customer Incentive Entry(Detail)", gvCustomer, arrHeader, Me.Text)
            ElseIf clsCommon.CompairString(Me.RadPageView1.SelectedPage.Name, "RadPageViewPage3") = CompairStringResult.Equal AndAlso gvCustomerIncentive.Rows.Count > 0 Then
                clsCommon.MyExportToExcelGrid("Customer Incentive Entry(Customer And Incentive Wise)", gvCustomerIncentive, arrHeader, Me.Text)
            ElseIf clsCommon.CompairString(Me.RadPageView1.SelectedPage.Name, "RadPageViewPage2") = CompairStringResult.Equal AndAlso gvCustomerStructure.Rows.Count > 0 Then
                clsCommon.MyExportToExcelGrid("Customer Incentive Entry(Customer And Structure Wise)", gvCustomerStructure, arrHeader, Me.Text)
            ElseIf clsCommon.CompairString(Me.RadPageView1.SelectedPage.Name, "RadPageViewPage1") = CompairStringResult.Equal AndAlso gvCustomerItem.Rows.Count > 0 Then
                clsCommon.MyExportToExcelGrid("Customer Incentive Entry(Customer And Item Detail)", gvCustomerItem, arrHeader, Me.Text)
            ElseIf clsCommon.CompairString(Me.RadPageView1.SelectedPage.Name, "RadPageViewPage5") = CompairStringResult.Equal AndAlso gvInvoice.Rows.Count > 0 Then
                clsCommon.MyExportToExcelGrid("Customer Incentive Entry(Invoice Detail)", gvInvoice, arrHeader, Me.Text)
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error, Me.Text)
        End Try
    End Sub
    Private Sub UpdateAllTotals()
        Dim dblTotalIncentiveAmt As Double = 0
        Dim dblTotalDeductionAmt As Double = 0
        Dim dblTotalAmount As Double = 0
        Dim dblTotalDocAmt As Decimal = 0
        For i As Int16 = 0 To gvCustomer.Rows.Count - 1
            dblTotalIncentiveAmt = dblTotalIncentiveAmt + clsCommon.myCdbl(gvCustomer.Rows(i).Cells(colIncentiveAmount).Value)
            dblTotalDeductionAmt = dblTotalDeductionAmt + clsCommon.myCdbl(gvCustomer.Rows(i).Cells(colDeductionAmount).Value)
            dblTotalAmount = dblTotalAmount + clsCommon.myCdbl(gvCustomer.Rows(i).Cells(colAmount).Value)
        Next
        TxtTotDeductionAmount.Value = clsCommon.myCdbl(dblTotalDeductionAmt)
        TxtTotIncentiveAmt.Value = clsCommon.myCdbl(dblTotalIncentiveAmt)
        TxtTotalAmount.Value = clsCommon.myCdbl(dblTotalAmount)

    End Sub
End Class


