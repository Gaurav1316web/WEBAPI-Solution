Imports common
Imports System.Data.SqlClient
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class frmIncomeTaxTDSCalculation
    Inherits FrmMainTranScreen
#Region "Variables"

    Const colEMPSNo As String = "colEMPSNo"
    Const colEMPEmpCode As String = "colEMPEmpCode"
    Const colEMPEmpName As String = "colEMPEmpName"
    Const colEMPGrossAmt As String = "colEMPGrossAmt"
    Const colEMPAllowanceAmt As String = "colEMPAllowanceAmt"
    Const colEMPSectionAmt As String = "colEMPSectionAmt"
    Const colEMPTaxableAmt As String = "colEMPTaxableAmt"
    Const colEMPTaxGroup As String = "colEMPTaxGroup"
    Const colEMPTotalTDSAmt As String = "colEMPTotalTDSAmt"

    Const colSNo As String = "colSNo"
    Const colEmpCode As String = "colEmpCode"
    Const colEmpName As String = "colEmpName"
    Const colType As String = "colType"
    Const colTypeCode As String = "colTypeCode"
    Const colGrossAmt As String = "colGrossAmt"
    Const colLimitAmt As String = "colLimitAmt"
    Const colApplicableAmt As String = "colApplicableAmt"

    Const colTaxSNo As String = "colTaxSNo"
    Const colTaxEmpCode As String = "colTaxEmpCode"
    Const colTaxEmpName As String = "colTaxEmpName"
    Const colTaxSlabCode As String = "colTaxSlabCode"
    Const colTaxSlabTRCode As String = "colTaxSlabTRCode"
    Const colTaxSlabTaxableAmt As String = "colTaxSlabTaxableAmt"

    Const colTax1 As String = "COLTAX1"
    Const colTaxBaseAmt1 As String = "COLTAXBASEAMT1"
    Const colTaxRate1 As String = "COLTAXRATE1"
    Const colTaxAmt1 As String = "COLTAXAMT1"
    Const colIsTaxable1 As String = "ISTAXABLE1"
    Const colIsSurTax1 As String = "ISSURTAX1"
    Const colSurTaxCode1 As String = "SURTAXCODE1"
    Const colIsExcisable1 As String = "ISEXCISABLE1"
    Const colIsTaxOnBaseAmt1 As String = "ISTAXONBASEAMT1"

    Const colTax2 As String = "COLTAX2"
    Const colTaxBaseAmt2 As String = "COLTAXBASEAMT2"
    Const colTaxRate2 As String = "COLTAXRATE2"
    Const colTaxAmt2 As String = "COLTAXAMT2"
    Const colIsTaxable2 As String = "ISTAXABLE2"
    Const colIsSurTax2 As String = "ISSURTAX2"
    Const colSurTaxCode2 As String = "SURTAXCODE2"
    Const colIsExcisable2 As String = "ISEXCISABLE2"
    Const colIsTaxOnBaseAmt2 As String = "ISTAXONBASEAMT2"

    Const colTax3 As String = "COLTAX3"
    Const colTaxBaseAmt3 As String = "COLTAXBASEAMT3"
    Const colTaxRate3 As String = "COLTAXRATE3"
    Const colTaxAmt3 As String = "COLTAXAMT3"
    Const colIsTaxable3 As String = "ISTAXABLE3"
    Const colIsSurTax3 As String = "ISSURTAX3"
    Const colSurTaxCode3 As String = "SURTAXCODE3"
    Const colIsExcisable3 As String = "ISEXCISABLE3"
    Const colIsTaxOnBaseAmt3 As String = "ISTAXONBASEAMT3"

    Const colTax4 As String = "COLTAX4"
    Const colTaxBaseAmt4 As String = "COLTAXBASEAMT4"
    Const colTaxRate4 As String = "COLTAXRATE4"
    Const colTaxAmt4 As String = "COLTAXAMT4"
    Const colIsTaxable4 As String = "ISTAXABLE4"
    Const colIsSurTax4 As String = "ISSURTAX4"
    Const colSurTaxCode4 As String = "SURTAXCODE4"
    Const colIsExcisable4 As String = "ISEXCISABLE4"
    Const colIsTaxOnBaseAmt4 As String = "ISTAXONBASEAMT4"

    Const colTax5 As String = "COLTAX5"
    Const colTaxBaseAmt5 As String = "COLTAXBASEAMT5"
    Const colTaxRate5 As String = "COLTAXRATE5"
    Const colTaxAmt5 As String = "COLTAXAMT5"
    Const colIsTaxable5 As String = "ISTAXABLE5"
    Const colIsSurTax5 As String = "ISSURTAX5"
    Const colSurTaxCode5 As String = "SURTAXCODE5"
    Const colIsExcisable5 As String = "ISEXCISABLE5"
    Const colIsTaxOnBaseAmt5 As String = "ISTAXONBASEAMT5"

    Const colTax6 As String = "COLTAX6"
    Const colTaxBaseAmt6 As String = "COLTAXBASEAMT6"
    Const colTaxRate6 As String = "COLTAXRATE6"
    Const colTaxAmt6 As String = "COLTAXAMT6"
    Const colIsTaxable6 As String = "ISTAXABLE6"
    Const colIsSurTax6 As String = "ISSURTAX6"
    Const colSurTaxCode6 As String = "SURTAXCODE6"
    Const colIsExcisable6 As String = "ISEXCISABLE6"
    Const colIsTaxOnBaseAmt6 As String = "ISTAXONBASEAMT6"

    Const colTax7 As String = "COLTAX7"
    Const colTaxBaseAmt7 As String = "COLTAXBASEAMT7"
    Const colTaxRate7 As String = "COLTAXRATE7"
    Const colTaxAmt7 As String = "COLTAXAMT7"
    Const colIsTaxable7 As String = "ISTAXABLE7"
    Const colIsSurTax7 As String = "ISSURTAX7"
    Const colSurTaxCode7 As String = "SURTAXCODE7"
    Const colIsExcisable7 As String = "ISEXCISABLE7"
    Const colIsTaxOnBaseAmt7 As String = "ISTAXONBASEAMT7"

    Const colTax8 As String = "COLTAX8"
    Const colTaxBaseAmt8 As String = "COLTAXBASEAMT8"
    Const colTaxRate8 As String = "COLTAXRATE8"
    Const colTaxAmt8 As String = "COLTAXAMT8"
    Const colIsTaxable8 As String = "ISTAXABLE8"
    Const colIsSurTax8 As String = "ISSURTAX8"
    Const colSurTaxCode8 As String = "SURTAXCODE8"
    Const colIsExcisable8 As String = "ISEXCISABLE8"
    Const colIsTaxOnBaseAmt8 As String = "ISTAXONBASEAMT8"

    Const colTax9 As String = "COLTAX9"
    Const colTaxBaseAmt9 As String = "COLTAXBASEAMT9"
    Const colTaxRate9 As String = "COLTAXRATE9"
    Const colTaxAmt9 As String = "COLTAXAMT9"
    Const colIsTaxable9 As String = "ISTAXABLE9"
    Const colIsSurTax9 As String = "ISSURTAX9"
    Const colSurTaxCode9 As String = "SURTAXCODE9"
    Const colIsExcisable9 As String = "ISEXCISABLE9"
    Const colIsTaxOnBaseAmt9 As String = "ISTAXONBASEAMT9"

    Const colTax10 As String = "COLTAX10"
    Const colTaxBaseAmt10 As String = "COLTAXBASEAMT10"
    Const colTaxRate10 As String = "COLTAXRATE10"
    Const colTaxAmt10 As String = "COLTAXAMT10"
    Const colIsTaxable10 As String = "ISTAXABLE10"
    Const colIsSurTax10 As String = "ISSURTAX10"
    Const colSurTaxCode10 As String = "SURTAXCODE10"
    Const colIsExcisable10 As String = "ISEXCISABLE10"
    Const colIsTaxOnBaseAmt10 As String = "ISTAXONBASEAMT10"
    Const ColTaxTDSAmt As String = "ColTaxTDSAmt"

    Private isInsideLoadData As Boolean = False
    Dim isNewEntry As Boolean = True
    Dim isCellValueChangedOpen As Boolean = False
    Dim dt As DataTable
    Dim qry As String
    Dim CurrentDate As DateTime = clsCommon.GETSERVERDATE()
    Dim ButtonToolTip As ToolTip = New ToolTip()
#End Region

    Private Sub frmSaleIncentiveMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            RadPageView1.SelectedPage = RadPageViewPage1
            SetUserMgmtNew()
            AddNew()
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
            ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D To Delete ")
            ButtonToolTip.SetToolTip(btnClose, "Press Alt+C To Close the Window")
            ButtonToolTip.SetToolTip(btnReset, "Press Alt+N For New")
            ButtonToolTip.SetToolTip(btnPost, "Press Alt+P to Post the Transaction")
            ValidateLength()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPost.Visible = MyBase.isPostFlag
    End Sub

    Private Sub ValidateLength()
        txtDesc.MaxLength = 200
    End Sub

    Private Sub AddNew()
        BlankAllControl()
        txtCode.MyReadOnly = False
        btnSave.Text = "Save"
        isCellValueChangedOpen = False
        isNewEntry = True
        btnSave.Enabled = True
        btnDelete.Enabled = False
        btnPost.Enabled = False
        isCellValueChangedOpen = False
    End Sub

    Sub BlankAllControl()
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtCode.Value = ""
        txtDesc.Text = ""
        lblPending.Status = ERPTransactionStatus.Pending
        txtFiscalYear.Value = ""
        lblFiscalYear.Text = ""
        txtEmployee.arrValueMember = Nothing
        LoadBlankGridEmp()
        LoadBlankGrid()
        LoadBlankGridTax()
    End Sub

    Sub LoadBlankGridEmp()
        gvEmp.Rows.Clear()
        gvEmp.Columns.Clear()

        Dim repoDecimal As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDecimal.FormatString = "{0:n0}"
        repoDecimal.HeaderText = "SNo"
        repoDecimal.Name = colEMPSNo
        repoDecimal.Width = 50
        repoDecimal.ReadOnly = True
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvEmp.MasterTemplate.Columns.Add(repoDecimal)

        Dim repoTextBox As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTextBox.HeaderText = "Employee Code"
        repoTextBox.Name = colEMPEmpCode
        repoTextBox.Width = 100
        repoTextBox.ReadOnly = True
        gvEmp.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.HeaderText = "Employee"
        repoTextBox.Name = colEMPEmpName
        repoTextBox.Width = 150
        repoTextBox.ReadOnly = True
        gvEmp.MasterTemplate.Columns.Add(repoTextBox)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = "{0:n2}"
        repoDecimal.HeaderText = "Gross Salary"
        repoDecimal.Name = colEMPGrossAmt
        repoDecimal.Width = 100
        repoDecimal.ReadOnly = True
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvEmp.MasterTemplate.Columns.Add(repoDecimal)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = "{0:n2}"
        repoDecimal.HeaderText = "Allowance Amount"
        repoDecimal.Name = colEMPAllowanceAmt
        repoDecimal.Width = 100
        repoDecimal.ReadOnly = True
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvEmp.MasterTemplate.Columns.Add(repoDecimal)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = "{0:n2}"
        repoDecimal.HeaderText = "Section Amount"
        repoDecimal.Name = colEMPSectionAmt
        repoDecimal.Width = 100
        repoDecimal.ReadOnly = True
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvEmp.MasterTemplate.Columns.Add(repoDecimal)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = "{0:n2}"
        repoDecimal.HeaderText = "Taxable Amount"
        repoDecimal.Name = colEMPTaxableAmt
        repoDecimal.Width = 100
        repoDecimal.ReadOnly = True
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvEmp.MasterTemplate.Columns.Add(repoDecimal)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.HeaderText = "Tax Group"
        repoTextBox.Name = colEMPTaxGroup
        repoTextBox.Width = 150
        repoTextBox.ReadOnly = True
        gvEmp.MasterTemplate.Columns.Add(repoTextBox)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = "{0:n2}"
        repoDecimal.HeaderText = "TDS Amount"
        repoDecimal.Name = colEMPTotalTDSAmt
        repoDecimal.Width = 100
        repoDecimal.ReadOnly = True
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvEmp.MasterTemplate.Columns.Add(repoDecimal)

        gvEmp.AllowAddNewRow = False
        gvEmp.AllowDeleteRow = False ''BHA/28/01/19-000794 by balwinder on 28/01/2019 
        gvEmp.AllowRowReorder = False
        gvEmp.ShowGroupPanel = False
        gvEmp.EnableFiltering = False
        gvEmp.EnableSorting = False
        gvEmp.EnableGrouping = False
        gvEmp.AllowColumnChooser = True
        gvEmp.AllowColumnReorder = True
        gvEmp.Rows.AddNew()
    End Sub

    Sub LoadBlankGrid()
        gv.Rows.Clear()
        gv.Columns.Clear()

        Dim repoDecimal As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDecimal.FormatString = "{0:n0}"
        repoDecimal.HeaderText = "SNo"
        repoDecimal.Name = colSNo
        repoDecimal.Width = 50
        repoDecimal.ReadOnly = True
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv.MasterTemplate.Columns.Add(repoDecimal)

        Dim repoTextBox As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTextBox.HeaderText = "Employee Code"
        repoTextBox.Name = colEmpCode
        repoTextBox.Width = 100
        repoTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.HeaderText = "Employee"
        repoTextBox.Name = colEmpName
        repoTextBox.Width = 150
        repoTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.HeaderText = "Type"
        repoTextBox.Name = colType
        repoTextBox.Width = 100
        repoTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.HeaderText = "Type Code"
        repoTextBox.Name = colTypeCode
        repoTextBox.Width = 100
        repoTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoTextBox)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = "{0:n2}"
        repoDecimal.HeaderText = "Gross Amount"
        repoDecimal.Name = colGrossAmt
        repoDecimal.Width = 100
        repoDecimal.ReadOnly = True
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoDecimal)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = "{0:n2}"
        repoDecimal.HeaderText = "Max Limit Amount"
        repoDecimal.Name = colLimitAmt
        repoDecimal.Width = 100
        repoDecimal.ReadOnly = True
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoDecimal)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = "{0:n2}"
        repoDecimal.HeaderText = "Applicable Amount"
        repoDecimal.Name = colApplicableAmt
        repoDecimal.Width = 100
        repoDecimal.ReadOnly = True
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoDecimal)

        gv.AllowAddNewRow = False
        gv.AllowDeleteRow = False
        gv.AllowRowReorder = False
        gv.ShowGroupPanel = False
        gv.EnableFiltering = False
        gv.EnableSorting = False
        gv.EnableGrouping = False
        gv.AllowColumnChooser = True
        gv.AllowColumnReorder = True
        gv.Rows.AddNew()
    End Sub

    Sub LoadBlankGridTax()
        gvTax.Rows.Clear()
        gvTax.Columns.Clear()

        Dim repoDecimal As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDecimal.FormatString = "{0:n0}"
        repoDecimal.HeaderText = "SNo"
        repoDecimal.Name = colTaxSNo
        repoDecimal.Width = 50
        repoDecimal.ReadOnly = True
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvTax.MasterTemplate.Columns.Add(repoDecimal)

        Dim repoTextBox As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTextBox.HeaderText = "Employee Code"
        repoTextBox.Name = colTaxEmpCode
        repoTextBox.Width = 100
        repoTextBox.ReadOnly = True
        gvTax.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.HeaderText = "Employee"
        repoTextBox.Name = colTaxEmpName
        repoTextBox.Width = 150
        repoTextBox.ReadOnly = True
        gvTax.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.HeaderText = "Slab Code"
        repoTextBox.Name = colTaxSlabCode
        repoTextBox.Width = 100
        repoTextBox.ReadOnly = True
        gvTax.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.HeaderText = "Slab TR Code"
        repoTextBox.Name = colTaxSlabTRCode
        repoTextBox.Width = 100
        repoTextBox.ReadOnly = True
        gvTax.MasterTemplate.Columns.Add(repoTextBox)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = "{0:n2}"
        repoDecimal.HeaderText = "Slab Taxable Amount"
        repoDecimal.Name = colTaxSlabTaxableAmt
        repoDecimal.Width = 100
        repoDecimal.Minimum = 0
        repoDecimal.ReadOnly = True
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTax.MasterTemplate.Columns.Add(repoDecimal)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Tax 1"
        repoTextBox.Name = colTax1
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = False
        gvTax.MasterTemplate.Columns.Add(repoTextBox) '26

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Tax Base Amount 1"
        repoDecimal.Name = colTaxBaseAmt1
        repoDecimal.ReadOnly = True
        repoDecimal.IsVisible = False
        gvTax.MasterTemplate.Columns.Add(repoDecimal) '27

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Tax Rate 1"
        repoDecimal.Name = colTaxRate1
        repoDecimal.IsVisible = False
        repoDecimal.ReadOnly = True
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTax.MasterTemplate.Columns.Add(repoDecimal) '28

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Tax Amt 1"
        repoDecimal.Name = colTaxAmt1
        repoDecimal.IsVisible = False
        repoDecimal.ReadOnly = True
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTax.MasterTemplate.Columns.Add(repoDecimal) '29

        Dim repoCheckBox = New GridViewCheckBoxColumn()
        repoCheckBox.HeaderText = "Is Surtax 1"
        repoCheckBox.Name = colIsSurTax1
        repoCheckBox.ReadOnly = True
        repoCheckBox.IsVisible = False
        repoCheckBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvTax.MasterTemplate.Columns.Add(repoCheckBox) '30

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Surtax 1"
        repoTextBox.Name = colSurTaxCode1
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = False
        gvTax.MasterTemplate.Columns.Add(repoTextBox) '31

        repoCheckBox = New GridViewCheckBoxColumn()
        repoCheckBox.HeaderText = "Is Taxable 1"
        repoCheckBox.Name = colIsTaxable1
        repoCheckBox.ReadOnly = True
        repoCheckBox.IsVisible = False
        repoCheckBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvTax.MasterTemplate.Columns.Add(repoCheckBox)

        repoCheckBox = New GridViewCheckBoxColumn()
        repoCheckBox.HeaderText = "Is Tax On Base Amt 1"
        repoCheckBox.Name = colIsTaxOnBaseAmt1
        repoCheckBox.ReadOnly = True
        repoCheckBox.IsVisible = False
        repoCheckBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvTax.MasterTemplate.Columns.Add(repoCheckBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Tax 2"
        repoTextBox.Name = colTax2
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = False
        gvTax.MasterTemplate.Columns.Add(repoTextBox)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Tax Base Amount 2"
        repoDecimal.Name = colTaxBaseAmt2
        repoDecimal.ReadOnly = True
        repoDecimal.IsVisible = False
        gvTax.MasterTemplate.Columns.Add(repoDecimal) '35

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Tax Rate 2"
        repoDecimal.Name = colTaxRate2
        repoDecimal.IsVisible = False
        repoDecimal.ReadOnly = True
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTax.MasterTemplate.Columns.Add(repoDecimal) '36

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Tax Amt 2"
        repoDecimal.Name = colTaxAmt2
        repoDecimal.IsVisible = False
        repoDecimal.ReadOnly = True
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTax.MasterTemplate.Columns.Add(repoDecimal) '37

        repoCheckBox = New GridViewCheckBoxColumn()
        repoCheckBox.HeaderText = "Is Surtax 2"
        repoCheckBox.Name = colIsSurTax2
        repoCheckBox.ReadOnly = True
        repoCheckBox.IsVisible = False
        repoCheckBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvTax.MasterTemplate.Columns.Add(repoCheckBox) '38

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Surtax 2"
        repoTextBox.Name = colSurTaxCode2
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = False
        gvTax.MasterTemplate.Columns.Add(repoTextBox) '39

        repoCheckBox = New GridViewCheckBoxColumn()
        repoCheckBox.HeaderText = "Is Taxable 2"
        repoCheckBox.Name = colIsTaxable2
        repoCheckBox.ReadOnly = True
        repoCheckBox.IsVisible = False
        repoCheckBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvTax.MasterTemplate.Columns.Add(repoCheckBox) '40

        repoCheckBox = New GridViewCheckBoxColumn()
        repoCheckBox.HeaderText = "Is Tax On Base Amt 2"
        repoCheckBox.Name = colIsTaxOnBaseAmt2
        repoCheckBox.ReadOnly = True
        repoCheckBox.IsVisible = False
        repoCheckBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvTax.MasterTemplate.Columns.Add(repoCheckBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Tax 3"
        repoTextBox.Name = colTax3
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = False
        gvTax.MasterTemplate.Columns.Add(repoTextBox) '42

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Tax Base Amount 3"
        repoDecimal.Name = colTaxBaseAmt3
        repoDecimal.ReadOnly = True
        repoDecimal.IsVisible = False
        gvTax.MasterTemplate.Columns.Add(repoDecimal) '43

        Dim repoTaxRate3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate3 = New GridViewDecimalColumn()
        repoTaxRate3.FormatString = ""
        repoTaxRate3.HeaderText = "Tax Rate 3"
        repoTaxRate3.Name = colTaxRate3
        repoTaxRate3.IsVisible = False
        repoTaxRate3.ReadOnly = True
        repoTaxRate3.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTax.MasterTemplate.Columns.Add(repoTaxRate3) '44

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Tax Amt 3"
        repoDecimal.Name = colTaxAmt3
        repoDecimal.IsVisible = False
        repoDecimal.ReadOnly = True
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTax.MasterTemplate.Columns.Add(repoDecimal) '45

        repoCheckBox = New GridViewCheckBoxColumn()
        repoCheckBox.HeaderText = "Is Surtax 3"
        repoCheckBox.Name = colIsSurTax3
        repoCheckBox.ReadOnly = True
        repoCheckBox.IsVisible = False
        repoCheckBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvTax.MasterTemplate.Columns.Add(repoCheckBox) '46

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Surtax 3"
        repoTextBox.Name = colSurTaxCode3
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = False
        gvTax.MasterTemplate.Columns.Add(repoTextBox) '47

        repoCheckBox = New GridViewCheckBoxColumn()
        repoCheckBox.HeaderText = "Is Taxable 3"
        repoCheckBox.Name = colIsTaxable3
        repoCheckBox.ReadOnly = True
        repoCheckBox.IsVisible = False
        repoCheckBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvTax.MasterTemplate.Columns.Add(repoCheckBox) '48

        repoCheckBox = New GridViewCheckBoxColumn()
        repoCheckBox.HeaderText = "Is Tax On Base Amt 3"
        repoCheckBox.Name = colIsTaxOnBaseAmt3
        repoCheckBox.ReadOnly = True
        repoCheckBox.IsVisible = False
        repoCheckBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvTax.MasterTemplate.Columns.Add(repoCheckBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Tax 4"
        repoTextBox.Name = colTax4
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = False
        gvTax.MasterTemplate.Columns.Add(repoTextBox)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Tax Base Amount 4"
        repoDecimal.Name = colTaxBaseAmt4
        repoDecimal.ReadOnly = True
        repoDecimal.IsVisible = False
        gvTax.MasterTemplate.Columns.Add(repoDecimal)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Tax Rate 4"
        repoDecimal.Name = colTaxRate4
        repoDecimal.IsVisible = False
        repoDecimal.ReadOnly = True
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTax.MasterTemplate.Columns.Add(repoDecimal)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Tax Amt 4"
        repoDecimal.Name = colTaxAmt4
        repoDecimal.IsVisible = False
        repoDecimal.ReadOnly = True
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTax.MasterTemplate.Columns.Add(repoDecimal) '53

        repoCheckBox = New GridViewCheckBoxColumn()
        repoCheckBox.HeaderText = "Is Surtax 4"
        repoCheckBox.Name = colIsSurTax4
        repoCheckBox.ReadOnly = True
        repoCheckBox.IsVisible = False
        repoCheckBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvTax.MasterTemplate.Columns.Add(repoCheckBox) '54

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Surtax 4"
        repoTextBox.Name = colSurTaxCode4
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = False
        gvTax.MasterTemplate.Columns.Add(repoTextBox) '55

        repoCheckBox = New GridViewCheckBoxColumn()
        repoCheckBox.HeaderText = "Is Taxable 4"
        repoCheckBox.Name = colIsTaxable4
        repoCheckBox.ReadOnly = True
        repoCheckBox.IsVisible = False
        repoCheckBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvTax.MasterTemplate.Columns.Add(repoCheckBox) '56

        repoCheckBox = New GridViewCheckBoxColumn()
        repoCheckBox.HeaderText = "Is Tax On Base Amt 4"
        repoCheckBox.Name = colIsTaxOnBaseAmt4
        repoCheckBox.ReadOnly = True
        repoCheckBox.IsVisible = False
        repoCheckBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvTax.MasterTemplate.Columns.Add(repoCheckBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Tax 5"
        repoTextBox.Name = colTax5
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = False
        gvTax.MasterTemplate.Columns.Add(repoTextBox) '58

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Tax Base Amount 5"
        repoDecimal.Name = colTaxBaseAmt5
        repoDecimal.ReadOnly = True
        repoDecimal.IsVisible = False
        gvTax.MasterTemplate.Columns.Add(repoDecimal) '59

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Tax Rate 5"
        repoDecimal.Name = colTaxRate5
        repoDecimal.IsVisible = False
        repoDecimal.ReadOnly = True
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTax.MasterTemplate.Columns.Add(repoDecimal) '60

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Tax Amt 5"
        repoDecimal.Name = colTaxAmt5
        repoDecimal.IsVisible = False
        repoDecimal.ReadOnly = True
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTax.MasterTemplate.Columns.Add(repoDecimal) '61

        repoCheckBox = New GridViewCheckBoxColumn()
        repoCheckBox.HeaderText = "Is Surtax 5"
        repoCheckBox.Name = colIsSurTax5
        repoCheckBox.ReadOnly = True
        repoCheckBox.IsVisible = False
        repoCheckBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvTax.MasterTemplate.Columns.Add(repoCheckBox) '62

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Surtax 5"
        repoTextBox.Name = colSurTaxCode5
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = False
        gvTax.MasterTemplate.Columns.Add(repoTextBox) '63

        repoCheckBox = New GridViewCheckBoxColumn()
        repoCheckBox.HeaderText = "Is Taxable 5"
        repoCheckBox.Name = colIsTaxable5
        repoCheckBox.ReadOnly = True
        repoCheckBox.IsVisible = False
        repoCheckBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvTax.MasterTemplate.Columns.Add(repoCheckBox) '64

        repoCheckBox = New GridViewCheckBoxColumn()
        repoCheckBox.HeaderText = "Is Tax On Base Amt 5"
        repoCheckBox.Name = colIsTaxOnBaseAmt5
        repoCheckBox.ReadOnly = True
        repoCheckBox.IsVisible = False
        repoCheckBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvTax.MasterTemplate.Columns.Add(repoCheckBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Tax 6"
        repoTextBox.Name = colTax6
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = False
        gvTax.MasterTemplate.Columns.Add(repoTextBox) '66

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Tax Base Amount 6"
        repoDecimal.Name = colTaxBaseAmt6
        repoDecimal.ReadOnly = True
        repoDecimal.IsVisible = False
        gvTax.MasterTemplate.Columns.Add(repoDecimal) '67

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Tax Rate 6"
        repoDecimal.Name = colTaxRate6
        repoDecimal.IsVisible = False
        repoDecimal.ReadOnly = True
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTax.MasterTemplate.Columns.Add(repoDecimal)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Tax Amt 6"
        repoDecimal.Name = colTaxAmt6
        repoDecimal.IsVisible = False
        repoDecimal.ReadOnly = True
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTax.MasterTemplate.Columns.Add(repoDecimal)

        repoCheckBox = New GridViewCheckBoxColumn()
        repoCheckBox.HeaderText = "Is Surtax 6"
        repoCheckBox.Name = colIsSurTax6
        repoCheckBox.ReadOnly = True
        repoCheckBox.IsVisible = False
        repoCheckBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvTax.MasterTemplate.Columns.Add(repoCheckBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Surtax 6"
        repoTextBox.Name = colSurTaxCode6
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = False
        gvTax.MasterTemplate.Columns.Add(repoTextBox)

        repoCheckBox = New GridViewCheckBoxColumn()
        repoCheckBox.HeaderText = "Is Taxable 6"
        repoCheckBox.Name = colIsTaxable6
        repoCheckBox.ReadOnly = True
        repoCheckBox.IsVisible = False
        repoCheckBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvTax.MasterTemplate.Columns.Add(repoCheckBox) '72

        repoCheckBox = New GridViewCheckBoxColumn()
        repoCheckBox.HeaderText = "Is Tax On Base Amt 6"
        repoCheckBox.Name = colIsTaxOnBaseAmt6
        repoCheckBox.ReadOnly = True
        repoCheckBox.IsVisible = False
        repoCheckBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvTax.MasterTemplate.Columns.Add(repoCheckBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Tax 7"
        repoTextBox.Name = colTax7
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = False
        gvTax.MasterTemplate.Columns.Add(repoTextBox) '74

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Tax Base Amount 7"
        repoDecimal.Name = colTaxBaseAmt7
        repoDecimal.ReadOnly = True
        repoDecimal.IsVisible = False
        gvTax.MasterTemplate.Columns.Add(repoDecimal)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Tax Rate 7"
        repoDecimal.Name = colTaxRate7
        repoDecimal.IsVisible = False
        repoDecimal.ReadOnly = True
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTax.MasterTemplate.Columns.Add(repoDecimal)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Tax Amt 7"
        repoDecimal.Name = colTaxAmt7
        repoDecimal.IsVisible = False
        repoDecimal.ReadOnly = True
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTax.MasterTemplate.Columns.Add(repoDecimal)

        repoCheckBox = New GridViewCheckBoxColumn()
        repoCheckBox.HeaderText = "Is Surtax 7"
        repoCheckBox.Name = colIsSurTax7
        repoCheckBox.ReadOnly = True
        repoCheckBox.IsVisible = False
        repoCheckBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvTax.MasterTemplate.Columns.Add(repoCheckBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Surtax 7"
        repoTextBox.Name = colSurTaxCode7
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = False
        gvTax.MasterTemplate.Columns.Add(repoTextBox)

        repoCheckBox = New GridViewCheckBoxColumn()
        repoCheckBox.HeaderText = "Is Taxable 7"
        repoCheckBox.Name = colIsTaxable7
        repoCheckBox.ReadOnly = True
        repoCheckBox.IsVisible = False
        repoCheckBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvTax.MasterTemplate.Columns.Add(repoCheckBox) '80

        repoCheckBox = New GridViewCheckBoxColumn()
        repoCheckBox.HeaderText = "Is Tax On Base Amt 7"
        repoCheckBox.Name = colIsTaxOnBaseAmt7
        repoCheckBox.ReadOnly = True
        repoCheckBox.IsVisible = False
        repoCheckBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvTax.MasterTemplate.Columns.Add(repoCheckBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Tax 8"
        repoTextBox.Name = colTax8
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = False
        gvTax.MasterTemplate.Columns.Add(repoTextBox) '82

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Tax Base Amount 8"
        repoDecimal.Name = colTaxBaseAmt8
        repoDecimal.ReadOnly = True
        repoDecimal.IsVisible = False
        gvTax.MasterTemplate.Columns.Add(repoDecimal)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Tax Rate 8"
        repoDecimal.Name = colTaxRate8
        repoDecimal.IsVisible = False
        repoDecimal.ReadOnly = True
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTax.MasterTemplate.Columns.Add(repoDecimal)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Tax Amt 8"
        repoDecimal.Name = colTaxAmt8
        repoDecimal.IsVisible = False
        repoDecimal.ReadOnly = True
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTax.MasterTemplate.Columns.Add(repoDecimal)

        repoCheckBox = New GridViewCheckBoxColumn()
        repoCheckBox.HeaderText = "Is Surtax 8"
        repoCheckBox.Name = colIsSurTax8
        repoCheckBox.ReadOnly = True
        repoCheckBox.IsVisible = False
        repoCheckBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvTax.MasterTemplate.Columns.Add(repoCheckBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Surtax 8"
        repoTextBox.Name = colSurTaxCode8
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = False
        gvTax.MasterTemplate.Columns.Add(repoTextBox)

        repoCheckBox = New GridViewCheckBoxColumn()
        repoCheckBox.HeaderText = "Is Taxable 8"
        repoCheckBox.Name = colIsTaxable8
        repoCheckBox.ReadOnly = True
        repoCheckBox.IsVisible = False
        repoCheckBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvTax.MasterTemplate.Columns.Add(repoCheckBox) '88

        repoCheckBox = New GridViewCheckBoxColumn()
        repoCheckBox.HeaderText = "Is Tax On Base Amt 8"
        repoCheckBox.Name = colIsTaxOnBaseAmt8
        repoCheckBox.ReadOnly = True
        repoCheckBox.IsVisible = False
        repoCheckBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvTax.MasterTemplate.Columns.Add(repoCheckBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Tax 9"
        repoTextBox.Name = colTax9
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = False
        gvTax.MasterTemplate.Columns.Add(repoTextBox) '90

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Tax Base Amount 9"
        repoDecimal.Name = colTaxBaseAmt9
        repoDecimal.ReadOnly = True
        repoDecimal.IsVisible = False
        gvTax.MasterTemplate.Columns.Add(repoDecimal)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Tax Rate 9"
        repoDecimal.Name = colTaxRate9
        repoDecimal.IsVisible = False
        repoDecimal.ReadOnly = True
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTax.MasterTemplate.Columns.Add(repoDecimal)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Tax Amt 9"
        repoDecimal.Name = colTaxAmt9
        repoDecimal.IsVisible = False
        repoDecimal.ReadOnly = True
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTax.MasterTemplate.Columns.Add(repoDecimal)

        repoCheckBox = New GridViewCheckBoxColumn()
        repoCheckBox.HeaderText = "Is Surtax 9"
        repoCheckBox.Name = colIsSurTax9
        repoCheckBox.ReadOnly = True
        repoCheckBox.IsVisible = False
        repoCheckBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvTax.MasterTemplate.Columns.Add(repoCheckBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Surtax 9"
        repoTextBox.Name = colSurTaxCode9
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = False
        gvTax.MasterTemplate.Columns.Add(repoTextBox)

        repoCheckBox = New GridViewCheckBoxColumn()
        repoCheckBox.HeaderText = "Is Taxable 9"
        repoCheckBox.Name = colIsTaxable9
        repoCheckBox.ReadOnly = True
        repoCheckBox.IsVisible = False
        repoCheckBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvTax.MasterTemplate.Columns.Add(repoCheckBox) '96

        repoCheckBox = New GridViewCheckBoxColumn()
        repoCheckBox.HeaderText = "Is Tax On Base Amt 9"
        repoCheckBox.Name = colIsTaxOnBaseAmt9
        repoCheckBox.ReadOnly = True
        repoCheckBox.IsVisible = False
        repoCheckBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvTax.MasterTemplate.Columns.Add(repoCheckBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Tax 10"
        repoTextBox.Name = colTax10
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = False
        gvTax.MasterTemplate.Columns.Add(repoTextBox) '98

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Tax Base Amount 10"
        repoDecimal.Name = colTaxBaseAmt10
        repoDecimal.ReadOnly = True
        repoDecimal.IsVisible = False
        gvTax.MasterTemplate.Columns.Add(repoDecimal)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Tax Rate 10"
        repoDecimal.Name = colTaxRate10
        repoDecimal.IsVisible = False
        repoDecimal.ReadOnly = True
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTax.MasterTemplate.Columns.Add(repoDecimal)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Tax Amt 10"
        repoDecimal.Name = colTaxAmt10
        repoDecimal.IsVisible = False
        repoDecimal.ReadOnly = True
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTax.MasterTemplate.Columns.Add(repoDecimal)

        repoCheckBox = New GridViewCheckBoxColumn()
        repoCheckBox.HeaderText = "Is Surtax 10"
        repoCheckBox.Name = colIsSurTax10
        repoCheckBox.ReadOnly = True
        repoCheckBox.IsVisible = False
        repoCheckBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvTax.MasterTemplate.Columns.Add(repoCheckBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Surtax 10"
        repoTextBox.Name = colSurTaxCode10
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = False
        gvTax.MasterTemplate.Columns.Add(repoTextBox)

        repoCheckBox = New GridViewCheckBoxColumn()
        repoCheckBox.HeaderText = "Is Taxable 10"
        repoCheckBox.Name = colIsTaxable10
        repoCheckBox.ReadOnly = True
        repoCheckBox.IsVisible = False
        repoCheckBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvTax.MasterTemplate.Columns.Add(repoCheckBox) '104

        repoCheckBox = New GridViewCheckBoxColumn()
        repoCheckBox.HeaderText = "Is Tax On Base Amt 10"
        repoCheckBox.Name = colIsTaxOnBaseAmt10
        repoCheckBox.ReadOnly = True
        repoCheckBox.IsVisible = False
        repoCheckBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvTax.MasterTemplate.Columns.Add(repoCheckBox)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = "{0:n2}"
        repoDecimal.HeaderText = "TDS Amount"
        repoDecimal.Name = ColTaxTDSAmt
        repoDecimal.Width = 80
        repoDecimal.ReadOnly = True
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTax.MasterTemplate.Columns.Add(repoDecimal)

        gvTax.AllowAddNewRow = False
        gvTax.AllowDeleteRow = False
        gvTax.AllowRowReorder = False
        gvTax.ShowGroupPanel = False
        gvTax.EnableFiltering = False
        gvTax.EnableSorting = False
        gvTax.EnableGrouping = False
        gvTax.AllowColumnChooser = True
        gvTax.AllowColumnReorder = True
        gvTax.Rows.AddNew()
    End Sub

    Sub SaveData(ByVal isPost As Boolean)
        Try
            If (AllowToSave()) Then
                Dim obj As New clsIncomeTaxTDSCalculationHead()
                obj.Code = txtCode.Value
                obj.Doc_Date = txtDate.Value
                obj.Description = txtDesc.Text
                obj.Fiscal_Code = txtFiscalYear.Value
                obj.ArrEmp = New List(Of clsIncomeTaxTDSCalculationEmp)
                obj.ArrDetail = New List(Of clsIncomeTaxTDSCalculationDetail)
                obj.ArrTax = New List(Of clsIncomeTaxTDSCalculationTax)

                For Each grow As GridViewRowInfo In gvEmp.Rows
                    If clsCommon.myLen(grow.Cells(colEMPEmpCode).Value) > 0 Then
                        Dim objTr As New clsIncomeTaxTDSCalculationEmp()
                        objTr.SNo = clsCommon.myCdbl(grow.Cells(colEMPSNo).Value)
                        objTr.Emp_Code = clsCommon.myCstr(grow.Cells(colEMPEmpCode).Value)
                        objTr.Gross_Amt = clsCommon.myCdbl(grow.Cells(colEMPGrossAmt).Value)
                        objTr.Allowance_Amt = clsCommon.myCdbl(grow.Cells(colEMPAllowanceAmt).Value)
                        objTr.Section_Amt = clsCommon.myCdbl(grow.Cells(colEMPSectionAmt).Value)
                        objTr.Taxable_Amt = clsCommon.myCdbl(grow.Cells(colEMPTaxableAmt).Value)
                        objTr.Tax_Group = clsCommon.myCstr(grow.Cells(colEMPTaxGroup).Value)
                        objTr.Total_TDS_Amt = clsCommon.myCdbl(grow.Cells(colEMPTotalTDSAmt).Value)
                        obj.ArrEmp.Add(objTr)
                    End If
                Next
                For Each grow As GridViewRowInfo In gv.Rows
                    If clsCommon.myLen(grow.Cells(colEmpCode).Value) > 0 Then
                        Dim objTr As New clsIncomeTaxTDSCalculationDetail()
                        objTr.SNo = clsCommon.myCdbl(grow.Cells(colSNo).Value)
                        objTr.Emp_Code = clsCommon.myCstr(grow.Cells(colEmpCode).Value)
                        objTr.Type = clsCommon.myCstr(grow.Cells(colType).Value)
                        objTr.Type_Code = clsCommon.myCstr(grow.Cells(colTypeCode).Value)
                        objTr.Gross_Amt = clsCommon.myCdbl(grow.Cells(colGrossAmt).Value)
                        objTr.Limit_Amt = clsCommon.myCdbl(grow.Cells(colLimitAmt).Value)
                        objTr.Applicable_Amt = clsCommon.myCdbl(grow.Cells(colApplicableAmt).Value)
                        obj.ArrDetail.Add(objTr)
                    End If
                Next

                For Each grow As GridViewRowInfo In gvTax.Rows
                    If clsCommon.myLen(grow.Cells(colTaxEmpCode).Value) > 0 Then
                        Dim objTr As New clsIncomeTaxTDSCalculationTax()
                        objTr.SNo = clsCommon.myCdbl(grow.Cells(colTaxSNo).Value)
                        objTr.Emp_Code = clsCommon.myCstr(grow.Cells(colTaxEmpCode).Value)
                        objTr.Slab_Code = clsCommon.myCstr(grow.Cells(colTaxSlabCode).Value)
                        objTr.Slab_Code_TR = clsCommon.myCstr(grow.Cells(colTaxSlabTRCode).Value)
                        objTr.Slab_Taxable_Amt = clsCommon.myCdbl(grow.Cells(colTaxSlabTaxableAmt).Value)
                        objTr.TAX1_Base_Amount = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt1).Value)
                        objTr.TAX1 = clsCommon.myCstr(grow.Cells(colTax1).Value)
                        objTr.TAX1_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate1).Value)
                        objTr.TAX1_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt1).Value)
                        objTr.TAX2_Base_Amount = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt2).Value)
                        objTr.TAX2 = clsCommon.myCstr(grow.Cells(colTax2).Value)
                        objTr.TAX2_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate2).Value)
                        objTr.TAX2_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt2).Value)
                        objTr.TAX3_Base_Amount = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt3).Value)
                        objTr.TAX3 = clsCommon.myCstr(grow.Cells(colTax3).Value)
                        objTr.TAX3_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate3).Value)
                        objTr.TAX3_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt3).Value)
                        objTr.TAX4_Base_Amount = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt4).Value)
                        objTr.TAX4 = clsCommon.myCstr(grow.Cells(colTax4).Value)
                        objTr.TAX4_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate4).Value)
                        objTr.TAX4_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt4).Value)
                        objTr.TAX5_Base_Amount = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt5).Value)
                        objTr.TAX5 = clsCommon.myCstr(grow.Cells(colTax5).Value)
                        objTr.TAX5_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate5).Value)
                        objTr.TAX5_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt5).Value)
                        objTr.TAX6_Base_Amount = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt6).Value)
                        objTr.TAX6 = clsCommon.myCstr(grow.Cells(colTax6).Value)
                        objTr.TAX6_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate6).Value)
                        objTr.TAX6_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt6).Value)
                        objTr.TAX7_Base_Amount = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt7).Value)
                        objTr.TAX7 = clsCommon.myCstr(grow.Cells(colTax7).Value)
                        objTr.TAX7_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate7).Value)
                        objTr.TAX7_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt7).Value)
                        objTr.TAX8_Base_Amount = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt8).Value)
                        objTr.TAX8 = clsCommon.myCstr(grow.Cells(colTax8).Value)
                        objTr.TAX8_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate8).Value)
                        objTr.TAX8_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt8).Value)
                        objTr.TAX9_Base_Amount = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt9).Value)
                        objTr.TAX9 = clsCommon.myCstr(grow.Cells(colTax9).Value)
                        objTr.TAX9_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate9).Value)
                        objTr.TAX9_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt9).Value)
                        objTr.TAX10_Base_Amount = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt10).Value)
                        objTr.TAX10 = clsCommon.myCstr(grow.Cells(colTax10).Value)
                        objTr.TAX10_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate10).Value)
                        objTr.TAX10_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt10).Value)
                        objTr.TDS_Amt = clsCommon.myCdbl(grow.Cells(ColTaxTDSAmt).Value)
                        obj.ArrTax.Add(objTr)
                    End If
                Next
                If obj.SaveData(obj, isNewEntry) Then
                    If isPost = False Then
                        clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                        LoadData(obj.Code, NavigatorType.Current)
                        Exit Sub
                    End If
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Function AllowToSave() As Boolean
        Dim linno As Integer = 0

        If clsCommon.myLen(txtDesc.Text) <= 0 Then
            txtDesc.Focus()
            Throw New Exception("Please enter Incentive Description")
        End If
        If clsCommon.myLen(txtFiscalYear.Value) <= 0 Then
            txtFiscalYear.Focus()
            Throw New Exception("Please select Fiscal Year")
        End If

        If txtEmployee.arrValueMember Is Nothing OrElse txtEmployee.arrValueMember.Count <= 0 Then
            txtEmployee.Focus()
            Throw New Exception("Please select at least one employee")
        End If

        If gvEmp.Rows.Count > 0 Then
            For ii As Integer = 0 To gvEmp.RowCount - 1
                CalculateCurrentRow(ii)
                SetTaxDetail(ii)
            Next
        End If
        Return True
    End Function

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
        GC.Collect()
    End Sub

    Sub LoadData(ByVal strIncentiveCode As String, ByVal NavType As NavigatorType)
        Try
            AddNew()
            isInsideLoadData = True
            Dim obj As New clsIncomeTaxTDSCalculationHead
            obj = clsIncomeTaxTDSCalculationHead.GetData(strIncentiveCode, NavType)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
                isNewEntry = False
                btnSave.Text = "Update"
                txtCode.Value = obj.Code
                txtDesc.Text = obj.Description
                txtFiscalYear.Value = obj.Fiscal_Code
                lblFiscalYear.Text = clsDBFuncationality.getSingleValue("select Fiscal_Name from TSPL_Fiscal_Year_Master where fiscal_code='" + obj.Fiscal_Code + "'")
                Dim arrEmpCode As New ArrayList
                For Each objTR As clsIncomeTaxTDSCalculationEmp In obj.ArrEmp
                    gvEmp.CurrentRow.Cells(colEMPSNo).Value = objTR.SNo
                    gvEmp.CurrentRow.Cells(colEMPEmpCode).Value = objTR.Emp_Code
                    gvEmp.CurrentRow.Cells(colEMPEmpName).Value = objTR.Emp_Name
                    gvEmp.CurrentRow.Cells(colEMPGrossAmt).Value = objTR.Gross_Amt
                    gvEmp.CurrentRow.Cells(colEMPAllowanceAmt).Value = objTR.Allowance_Amt
                    gvEmp.CurrentRow.Cells(colEMPSectionAmt).Value = objTR.Section_Amt
                    gvEmp.CurrentRow.Cells(colEMPTaxableAmt).Value = objTR.Taxable_Amt
                    gvEmp.CurrentRow.Cells(colEMPTaxGroup).Value = objTR.Tax_Group
                    gvEmp.CurrentRow.Cells(colEMPTotalTDSAmt).Value = objTR.Total_TDS_Amt
                    gvEmp.Rows.AddNew()
                    If Not arrEmpCode.Contains(objTR.Emp_Code) Then
                        arrEmpCode.Add(objTR.Emp_Code)
                    End If
                Next
                txtEmployee.arrValueMember = arrEmpCode
                For Each objTR As clsIncomeTaxTDSCalculationDetail In obj.ArrDetail
                    gv.CurrentRow.Cells(colSNo).Value = objTR.SNo
                    gv.CurrentRow.Cells(colEmpCode).Value = objTR.Emp_Code
                    gv.CurrentRow.Cells(colEmpName).Value = objTR.Emp_Name
                    gv.CurrentRow.Cells(colType).Value = objTR.Type
                    gv.CurrentRow.Cells(colTypeCode).Value = objTR.Type_Code
                    gv.CurrentRow.Cells(colGrossAmt).Value = objTR.Gross_Amt
                    gv.CurrentRow.Cells(colLimitAmt).Value = objTR.Limit_Amt
                    gv.CurrentRow.Cells(colApplicableAmt).Value = objTR.Applicable_Amt
                    gv.Rows.AddNew()
                Next
                For Each objTR As clsIncomeTaxTDSCalculationTax In obj.ArrTax
                    gvTax.CurrentRow.Cells(colTaxSNo).Value = objTR.SNo
                    gvTax.CurrentRow.Cells(colTaxEmpCode).Value = objTR.Emp_Code
                    gvTax.CurrentRow.Cells(colTaxEmpName).Value = objTR.Emp_Name
                    gvTax.CurrentRow.Cells(colTaxSlabCode).Value = objTR.Slab_Code
                    gvTax.CurrentRow.Cells(colTaxSlabTRCode).Value = objTR.Slab_Code_TR
                    gvTax.CurrentRow.Cells(colTaxSlabTaxableAmt).Value = objTR.Slab_Taxable_Amt

                    gvTax.CurrentRow.Cells(colTaxBaseAmt1).Value = objTR.TAX1_Base_Amount
                    gvTax.CurrentRow.Cells(colTax1).Value = objTR.TAX1
                    gvTax.CurrentRow.Cells(colTaxRate1).Value = objTR.TAX1_Rate
                    gvTax.CurrentRow.Cells(colTaxAmt1).Value = objTR.TAX1_Amt

                    gvTax.CurrentRow.Cells(colTaxBaseAmt2).Value = objTR.TAX2_Base_Amount
                    gvTax.CurrentRow.Cells(colTax2).Value = objTR.TAX2
                    gvTax.CurrentRow.Cells(colTaxRate2).Value = objTR.TAX2_Rate
                    gvTax.CurrentRow.Cells(colTaxAmt2).Value = objTR.TAX2_Amt

                    gvTax.CurrentRow.Cells(colTaxBaseAmt3).Value = objTR.TAX3_Base_Amount
                    gvTax.CurrentRow.Cells(colTax3).Value = objTR.TAX3
                    gvTax.CurrentRow.Cells(colTaxRate3).Value = objTR.TAX3_Rate
                    gvTax.CurrentRow.Cells(colTaxAmt3).Value = objTR.TAX3_Amt

                    gvTax.CurrentRow.Cells(colTaxBaseAmt4).Value = objTR.TAX4_Base_Amount
                    gvTax.CurrentRow.Cells(colTax4).Value = objTR.TAX4
                    gvTax.CurrentRow.Cells(colTaxRate4).Value = objTR.TAX4_Rate
                    gvTax.CurrentRow.Cells(colTaxAmt4).Value = objTR.TAX4_Amt

                    gvTax.CurrentRow.Cells(colTaxBaseAmt5).Value = objTR.TAX5_Base_Amount
                    gvTax.CurrentRow.Cells(colTax5).Value = objTR.TAX5
                    gvTax.CurrentRow.Cells(colTaxRate5).Value = objTR.TAX5_Rate
                    gvTax.CurrentRow.Cells(colTaxAmt5).Value = objTR.TAX5_Amt

                    gvTax.CurrentRow.Cells(colTaxBaseAmt6).Value = objTR.TAX6_Base_Amount
                    gvTax.CurrentRow.Cells(colTax6).Value = objTR.TAX6
                    gvTax.CurrentRow.Cells(colTaxRate6).Value = objTR.TAX6_Rate
                    gvTax.CurrentRow.Cells(colTaxAmt6).Value = objTR.TAX6_Amt

                    gvTax.CurrentRow.Cells(colTaxBaseAmt7).Value = objTR.TAX7_Base_Amount
                    gvTax.CurrentRow.Cells(colTax7).Value = objTR.TAX7
                    gvTax.CurrentRow.Cells(colTaxRate7).Value = objTR.TAX7_Rate
                    gvTax.CurrentRow.Cells(colTaxAmt7).Value = objTR.TAX7_Amt

                    gvTax.CurrentRow.Cells(colTaxBaseAmt8).Value = objTR.TAX8_Base_Amount
                    gvTax.CurrentRow.Cells(colTax8).Value = objTR.TAX8
                    gvTax.CurrentRow.Cells(colTaxRate8).Value = objTR.TAX8_Rate
                    gvTax.CurrentRow.Cells(colTaxAmt8).Value = objTR.TAX8_Amt

                    gvTax.CurrentRow.Cells(colTaxBaseAmt9).Value = objTR.TAX9_Base_Amount
                    gvTax.CurrentRow.Cells(colTax9).Value = objTR.TAX9
                    gvTax.CurrentRow.Cells(colTaxRate9).Value = objTR.TAX9_Rate
                    gvTax.CurrentRow.Cells(colTaxAmt9).Value = objTR.TAX9_Amt

                    gvTax.CurrentRow.Cells(colTaxBaseAmt10).Value = objTR.TAX10_Base_Amount
                    gvTax.CurrentRow.Cells(colTax10).Value = objTR.TAX10
                    gvTax.CurrentRow.Cells(colTaxRate10).Value = objTR.TAX10_Rate
                    gvTax.CurrentRow.Cells(colTaxAmt10).Value = objTR.TAX10_Amt

                    gvTax.CurrentRow.Cells(ColTaxTDSAmt).Value = objTR.TDS_Amt
                    gvTax.Rows.AddNew()
                Next
                lblPending.Status = obj.Status
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnDelete.Enabled = False
                    btnPost.Enabled = False
                Else
                    btnSave.Enabled = True
                    btnDelete.Enabled = True
                    btnPost.Enabled = True
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub DeleteData(ByVal strIcentiveCode As String)
        Try
            If clsCommon.myLen(strIcentiveCode) = 0 Then
                Throw New Exception("No Code found to delete.")
            End If
            If (myMessages.deleteConfirm) Then
                If clsIncomeTaxTDSCalculationHead.fundelete(strIcentiveCode) Then
                    clsCommon.MyMessageBoxShow(Me, "Data deleted successfully.", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        AddNew()
    End Sub

    Private Sub rmiClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub frmSaleIncentiveMaster_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData(False)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled Then
            DeleteData(txtCode.Value)
        End If
    End Sub

    Private Sub txtIncentive__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        txtCode.Value = clsIncomeTaxTDSCalculationHead.getFinder("", txtCode.Value, isButtonClicked)
        LoadData(txtCode.Value, NavigatorType.Current)
    End Sub

    Private Sub txtIncentive__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtCode._MYNavigator
        Try
            qry = "select count(*) from TSPL_HR_TDS_INCOME_TAX_SLAB where CODE='" + txtCode.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If count = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv_UserDeletedRow(sender As Object, e As GridViewRowEventArgs)
        For ii As Integer = 1 To gvEmp.Rows.Count
            gvEmp.Rows(ii - 1).Cells(colTaxSNo).Value = ii
        Next
    End Sub

    Private Sub gv_UserDeletingRow(sender As Object, e As GridViewRowCancelEventArgs)
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            SaveData(False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteData(txtCode.Value)
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        postData()
    End Sub

    Sub postData()
        Try
            If (myMessages.postConfirm()) Then
                SaveData(True)
                clsIncomeTaxTDSCalculationHead.postData(txtCode.Value)
                clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                LoadData(txtCode.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub CalculateCurrentRow(ByVal ii As Integer)
        'gvEmp.Rows(ii).Cells(colTaxSlabTaxableAmt).Value = clsCommon.myCdbl(gvEmp.Rows(ii).Cells(colTaxToRange).Value) - clsCommon.myCdbl(gvEmp.Rows(ii).Cells(colTaxFromRange).Value)
    End Sub

    Sub OpenTaxRateFinded(ByVal TaxNo As Integer)
        Dim qry As String = "select Tax_Rate from TSPL_TAX_RATES"
        Dim whr As String = "Tax_Type='H'  and Tax_Code='" + clsCommon.myCstr(gvEmp.CurrentRow.Cells("ColTAX" + clsCommon.myCstr(TaxNo)).Value) + "'"
        Dim str As String = clsCommon.ShowSelectForm("ITSL@TaxR", qry, "Tax_Rate", whr, clsCommon.myCstr(gvEmp.CurrentRow.Cells("ColTAX" + clsCommon.myCstr(TaxNo) + "Rate").Value), "Tax_Rate", False)
        If clsCommon.myLen(str) > 0 Then
            gvEmp.CurrentRow.Cells("ColTAX" + clsCommon.myCstr(TaxNo) + "Rate").Value = clsCommon.myCdbl(str)
        Else
            gvEmp.CurrentRow.Cells("ColTAX" + clsCommon.myCstr(TaxNo) + "Rate").Value = Nothing
        End If
    End Sub

    Private Sub gv_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs)
        'If gvEmp.RowCount > 0 Then
        '    Dim intCurrRow As Integer = gvEmp.CurrentRow.Index
        '    gvEmp.CurrentRow.Cells(colTaxSNo).Value = clsCommon.myCdbl(intCurrRow + 1)
        '    If intCurrRow = gvEmp.Rows.Count - 1 Then
        '        gvEmp.Rows.AddNew()
        '        gvEmp.CurrentRow = gvEmp.Rows(intCurrRow)
        '        SetTaxDetail(gvEmp.CurrentRow.Index)
        '    End If
        'End If
    End Sub

    Private Sub gv_CellFormatting(sender As Object, e As CellFormattingEventArgs)
        'If IsLoaded = True Then
        '    If e.Column Is gvEmp.Columns(colTaxFromRange) OrElse e.Column Is gvEmp.Columns(colTaxToRange) Then
        '        If clsCommon.CompairString(gvEmp.CurrentRow.Index, 0) = CompairStringResult.Equal Then
        '            gvEmp.CurrentRow.Cells(colTaxFromRange).ReadOnly = False
        '        Else
        '            gvEmp.CurrentRow.Cells(colTaxFromRange).ReadOnly = True
        '        End If
        '        If String.IsNullOrEmpty(clsCommon.myCstr(gvEmp.CurrentRow.Cells(colTaxFromRange).Value)) = True Then
        '            gvEmp.CurrentRow.Cells(colTaxToRange).ReadOnly = True
        '        Else
        '            gvEmp.CurrentRow.Cells(colTaxToRange).ReadOnly = False
        '        End If
        '    ElseIf e.Column Is gvEmp.Columns(ColTaxTax1Rate) Then
        '        gvEmp.CurrentRow.Cells(ColTaxTax1Rate).ReadOnly = Not (clsCommon.myLen(gvEmp.CurrentRow.Cells(ColTaxTAX1).Value) > 0)
        '    ElseIf e.Column Is gvEmp.Columns(ColTaxTax2Rate) Then
        '        gvEmp.CurrentRow.Cells(ColTaxTax2Rate).ReadOnly = Not (clsCommon.myLen(gvEmp.CurrentRow.Cells(ColTaxTAX2).Value) > 0)
        '    ElseIf e.Column Is gvEmp.Columns(ColTaxTax3Rate) Then
        '        gvEmp.CurrentRow.Cells(ColTaxTax3Rate).ReadOnly = Not (clsCommon.myLen(gvEmp.CurrentRow.Cells(ColTaxTAX3).Value) > 0)
        '    ElseIf e.Column Is gvEmp.Columns(ColTaxTax4Rate) Then
        '        gvEmp.CurrentRow.Cells(ColTaxTax4Rate).ReadOnly = Not (clsCommon.myLen(gvEmp.CurrentRow.Cells(ColTaxTAX4).Value) > 0)
        '    ElseIf e.Column Is gvEmp.Columns(ColTaxTax5Rate) Then
        '        gvEmp.CurrentRow.Cells(ColTaxTax5Rate).ReadOnly = Not (clsCommon.myLen(gvEmp.CurrentRow.Cells(ColTaxTAX5).Value) > 0)
        '    ElseIf e.Column Is gvEmp.Columns(ColTaxTax6Rate) Then
        '        gvEmp.CurrentRow.Cells(ColTaxTax6Rate).ReadOnly = Not (clsCommon.myLen(gvEmp.CurrentRow.Cells(ColTaxTAX6).Value) > 0)
        '    ElseIf e.Column Is gvEmp.Columns(ColTaxTax7Rate) Then
        '        gvEmp.CurrentRow.Cells(ColTaxTax7Rate).ReadOnly = Not (clsCommon.myLen(gvEmp.CurrentRow.Cells(ColTaxTAX7).Value) > 0)
        '    ElseIf e.Column Is gvEmp.Columns(ColTaxTax8Rate) Then
        '        gvEmp.CurrentRow.Cells(ColTaxTax8Rate).ReadOnly = Not (clsCommon.myLen(gvEmp.CurrentRow.Cells(ColTaxTAX8).Value) > 0)
        '    ElseIf e.Column Is gvEmp.Columns(ColTaxTax9Rate) Then
        '        gvEmp.CurrentRow.Cells(ColTaxTax9Rate).ReadOnly = Not (clsCommon.myLen(gvEmp.CurrentRow.Cells(ColTaxTAX9).Value) > 0)
        '    ElseIf e.Column Is gvEmp.Columns(ColTaxTax10Rate) Then
        '        gvEmp.CurrentRow.Cells(ColTaxTax10Rate).ReadOnly = Not (clsCommon.myLen(gvEmp.CurrentRow.Cells(ColTaxTAX10).Value) > 0)
        '    End If
        'End If
    End Sub

    Private Sub gv_CurrentRowChanged(sender As Object, e As CurrentRowChangedEventArgs)
        'If gvEmp.RowCount > 0 Then
        '    Dim intCurrRow As Integer = gvEmp.CurrentRow.Index
        '    gvEmp.CurrentRow.Cells(colTaxSNo).Value = clsCommon.myCdbl(intCurrRow + 1)
        '    If intCurrRow = gvEmp.Rows.Count - 1 Then
        '        gvEmp.CurrentRow = gvEmp.Rows(intCurrRow)
        '    End If
        'End If
    End Sub

    Private Sub txtGLAccount__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtFiscalYear._MYValidating
        Dim Qry As String = "select Fiscal_Code, Fiscal_Name,Start_Date,End_Date from TSPL_Fiscal_Year_Master"
        txtFiscalYear.Value = clsCommon.ShowSelectForm("ITSL@Fiscal", Qry, "fiscal_code", "", txtFiscalYear.Value, "fiscal_code", isButtonClicked)
        lblFiscalYear.Text = clsDBFuncationality.getSingleValue("select  Fiscal_Name from TSPL_Fiscal_Year_Master where Fiscal_Code ='" + txtFiscalYear.Value + "' ")
    End Sub

    Sub SetTaxDetail(ByVal RowIndex As Integer)
        'Dim qry As String = "select tax_code,Tax_Code_Desc   from TSPL_TAX_GROUP_DETAILS where Tax_Group_Code='" + txtTaxGroup.Value + "' and Tax_Group_Type='H'  order by Trans_Code"
        'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
        '    For ii As Integer = 0 To dt.Rows.Count - 1
        '        gvEmp.Rows(RowIndex).Cells("ColTAX" + clsCommon.myCstr(ii + 1)).Value = clsCommon.myCstr(dt.Rows(ii)("tax_code"))
        '    Next
        'End If
    End Sub

    Private Sub txtVSP__My_Click(sender As Object, e As EventArgs) Handles txtEmployee._My_Click
        Try
            Dim qry As String = "select EMP_CODE,Emp_Name from TSPL_EMPLOYEE_MASTER"
            txtEmployee.arrValueMember = clsCommon.ShowMultipleSelectForm(False, "EmpF@TDSC", qry, "EMP_CODE", "", txtEmployee.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            LoadBlankGrid()
            LoadBlankGridEmp()
            LoadBlankGridTax()
            If clsCommon.myLen(txtFiscalYear.Value) <= 0 Then
                txtFiscalYear.Focus()
                Throw New Exception("Please select Fiscal Year")
            End If
            If txtEmployee.arrValueMember Is Nothing OrElse txtEmployee.arrValueMember.Count <= 0 Then
                txtEmployee.Focus()
                Throw New Exception("Please select Employees")
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Start_Date,End_Date from TSPL_Fiscal_Year_Master where Fiscal_Code='" + txtFiscalYear.Value + "'")
            Dim dtFiscalYearFromDate As DateTime = clsCommon.GetDateWithStartTime(dt.Rows(0)("Start_Date"))
            Dim dtFiscalYearToDate As DateTime = clsCommon.GetDateWithEndTime(dt.Rows(0)("End_Date"))

            Dim BaseQry As String = "select xxx.EMP_CODE,TSPL_EMPLOYEE_MASTER.Emp_Name,xxx.Type,xxx.TypeCode,xxx.GrossAmount,xxx.MaxLimit,xxx.ApplicableAmt,xxx.RI,xxx.SNo  from (" + Environment.NewLine + _
            "select EMP_CODE,'Gross Salary' as Type,'' as TypeCode,sum(RATE_AMOUNT) as GrossAmount,0 as MaxLimit,sum(RATE_AMOUNT) as ApplicableAmt ,1 as RI,1 as SNo from ( " + Environment.NewLine + _
            "select TSPL_EMPLOYEE_SALARY.EMP_CODE, TSPL_EMPLOYEE_SALARY_PAYHEADS.EMP_SAL_CODE,TSPL_EMPLOYEE_SALARY_PAYHEADS.LINE_NO,TSPL_EMPLOYEE_SALARY_PAYHEADS.PAY_HEAD_CODE,TSPL_EMPLOYEE_SALARY_PAYHEADS.RATE_AMOUNT*12 as  RATE_AMOUNT " + Environment.NewLine + _
            "from TSPL_EMPLOYEE_SALARY_PAYHEADS  " + Environment.NewLine + _
            "left outer join TSPL_EMPLOYEE_SALARY on TSPL_EMPLOYEE_SALARY.EMP_SAL_CODE=TSPL_EMPLOYEE_SALARY_PAYHEADS.EMP_SAL_CODE" + Environment.NewLine + _
            "where TSPL_EMPLOYEE_SALARY.POSTED=1 and TSPL_EMPLOYEE_SALARY.EMP_CODE in (" + clsCommon.GetMulcallString(txtEmployee.arrValueMember) + ") and TSPL_EMPLOYEE_SALARY.EMP_SAL_CODE =(select top 1 innerTable.EMP_SAL_CODE from TSPL_EMPLOYEE_SALARY as innerTable where innerTable.EMP_CODE=TSPL_EMPLOYEE_SALARY.EMP_CODE and innerTable.APPLICABLE_FROM<='" + clsCommon.GetPrintDate(dtFiscalYearToDate, "dd/MMM/yyyy hh:mm:ss tt") + "' order by APPLICABLE_FROM desc)" + Environment.NewLine + _
            ")xx group by EMP_CODE" + Environment.NewLine + _
            "union all" + Environment.NewLine + _
            "select EMP_CODE,'Allowance' as Type,PAY_HEAD_CODE as TypeCode ,sum(RATE_AMOUNT) as GrossAmount,max(MAX_LIMIT) as MaxLimit,case when sum(RATE_AMOUNT)>max(MAX_LIMIT) then max(MAX_LIMIT) else sum(RATE_AMOUNT) end as ApplicableAmt,-1 as RI,2 as SNo from ( " + Environment.NewLine + _
            "select TSPL_EMPLOYEE_SALARY.EMP_CODE, TSPL_EMPLOYEE_SALARY_PAYHEADS.EMP_SAL_CODE,TSPL_EMPLOYEE_SALARY_PAYHEADS.LINE_NO,TSPL_EMPLOYEE_SALARY_PAYHEADS.PAY_HEAD_CODE,TSPL_EMPLOYEE_SALARY_PAYHEADS.RATE_AMOUNT*12 as  RATE_AMOUNT " + Environment.NewLine + _
            ",TSPL_SECTION_ALLOWANCE_MASTER.Type,TSPL_SECTION_ALLOWANCE_MASTER.MAX_LIMIT" + Environment.NewLine + _
            "from TSPL_EMPLOYEE_SALARY_PAYHEADS  " + Environment.NewLine + _
            "left outer join TSPL_EMPLOYEE_SALARY on TSPL_EMPLOYEE_SALARY.EMP_SAL_CODE=TSPL_EMPLOYEE_SALARY_PAYHEADS.EMP_SAL_CODE" + Environment.NewLine + _
            "inner join TSPL_SECTION_ALLOWANCE_MASTER on TSPL_SECTION_ALLOWANCE_MASTER.CODE=TSPL_EMPLOYEE_SALARY_PAYHEADS.PAY_HEAD_CODE" + Environment.NewLine + _
            "where TSPL_EMPLOYEE_SALARY.POSTED=1 and TSPL_EMPLOYEE_SALARY.EMP_CODE in (" + clsCommon.GetMulcallString(txtEmployee.arrValueMember) + ") and TSPL_EMPLOYEE_SALARY.EMP_SAL_CODE =(select top 1 innerTable.EMP_SAL_CODE from TSPL_EMPLOYEE_SALARY as innerTable where innerTable.EMP_CODE=TSPL_EMPLOYEE_SALARY.EMP_CODE and innerTable.APPLICABLE_FROM<='" + clsCommon.GetPrintDate(dtFiscalYearToDate, "dd/MMM/yyyy hh:mm:ss tt") + "' order by APPLICABLE_FROM desc)" + Environment.NewLine + _
            ")xx group by EMP_CODE,PAY_HEAD_CODE" + Environment.NewLine + _
            "union all" + Environment.NewLine + _
            "select EMP_CODE,'Section' as Type,Section_Code as TypeCode,sum(AMOUNT) as GrossAmount,max(MAX_LIMIT)  as MaxLimit,case when sum(AMOUNT)>max(MAX_LIMIT) then max(MAX_LIMIT) else sum(AMOUNT) end as ApplicableAmt,-1 as RI,3 as SNo   from (" + Environment.NewLine + _
            "select TSPL_EMPLOYEE_SAVINGS_MAPPING_MASTER.EMP_CODE,TSPL_EMPLOYEE_SAVINGS_MAPPING_DETAIL.Section_Code,TSPL_EMPLOYEE_SAVINGS_MAPPING_DETAIL.AMOUNT,TSPL_SECTION_ALLOWANCE_MASTER.MAX_LIMIT" + Environment.NewLine + _
            "from TSPL_EMPLOYEE_SAVINGS_MAPPING_DETAIL" + Environment.NewLine + _
            "left outer join TSPL_EMPLOYEE_SAVINGS_MAPPING_MASTER on TSPL_EMPLOYEE_SAVINGS_MAPPING_MASTER.DOCUMENT_CODE=TSPL_EMPLOYEE_SAVINGS_MAPPING_DETAIL.DOCUMENT_CODE" + Environment.NewLine + _
            "left outer join TSPL_SECTION_ALLOWANCE_MASTER on TSPL_SECTION_ALLOWANCE_MASTER.CODE=TSPL_EMPLOYEE_SAVINGS_MAPPING_DETAIL.Section_Code" + Environment.NewLine + _
            "where TSPL_SECTION_ALLOWANCE_MASTER.Type='S' and TSPL_EMPLOYEE_SAVINGS_MAPPING_MASTER.Fiscal_Code='18-19' and TSPL_EMPLOYEE_SAVINGS_MAPPING_MASTER.EMP_CODE in (" + clsCommon.GetMulcallString(txtEmployee.arrValueMember) + ")" + Environment.NewLine + _
            ")xx group by EMP_CODE,Section_Code" + Environment.NewLine + _
            ")xxx " + Environment.NewLine + _
            "left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=xxx.EMP_CODE"

            Dim qry As String = "select EMP_CODE,max(Emp_Name) as Emp_Name,sum(ApplicableAmt * case when type='Gross Salary' then 1 else 0 end) as GrossSalary,sum(ApplicableAmt * case when type='Allowance' then 1 else 0 end) as AllowancesAmt,sum(ApplicableAmt * case when type='Section' then 1 else 0 end) as SectionAmt,sum(ApplicableAmt *RI) as TaxableAmt from (" + BaseQry + ")xxxx group by EMP_CODE order by EMP_CODE "
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data found")
            End If
            For ii As Integer = 0 To dt.Rows.Count - 1
                gvEmp.CurrentRow.Cells(colEMPSNo).Value = ii + 1
                gvEmp.CurrentRow.Cells(colEMPEmpCode).Value = clsCommon.myCstr(dt.Rows(ii)("EMP_CODE"))
                gvEmp.CurrentRow.Cells(colEMPEmpName).Value = clsCommon.myCstr(dt.Rows(ii)("Emp_Name"))
                gvEmp.CurrentRow.Cells(colEMPGrossAmt).Value = clsCommon.myCdbl(dt.Rows(ii)("GrossSalary"))
                gvEmp.CurrentRow.Cells(colEMPAllowanceAmt).Value = clsCommon.myCdbl(dt.Rows(ii)("AllowancesAmt"))
                gvEmp.CurrentRow.Cells(colEMPSectionAmt).Value = clsCommon.myCdbl(dt.Rows(ii)("SectionAmt"))
                gvEmp.CurrentRow.Cells(colEMPTaxableAmt).Value = clsCommon.myCdbl(dt.Rows(ii)("TaxableAmt"))
                Dim TotalTDSAmt As Decimal = 0
                gvEmp.CurrentRow.Cells(colEMPTaxGroup).Value = AddSlabGrid(clsCommon.myCstr(dt.Rows(ii)("EMP_CODE")), clsCommon.myCstr(dt.Rows(ii)("Emp_Name")), dtFiscalYearFromDate, clsCommon.myCdbl(dt.Rows(ii)("TaxableAmt")), TotalTDSAmt)
                gvEmp.CurrentRow.Cells(colEMPTotalTDSAmt).Value = TotalTDSAmt
                gvEmp.Rows.AddNew()
            Next

            qry = BaseQry + " order by EMP_CODE,SNo "
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data found")
            End If
            For ii As Integer = 0 To dt.Rows.Count - 1
                gv.CurrentRow.Cells(colSNo).Value = ii + 1
                gv.CurrentRow.Cells(colEmpCode).Value = clsCommon.myCstr(dt.Rows(ii)("EMP_CODE"))
                gv.CurrentRow.Cells(colEmpName).Value = clsCommon.myCstr(dt.Rows(ii)("Emp_Name"))
                gv.CurrentRow.Cells(colType).Value = clsCommon.myCstr(dt.Rows(ii)("Type"))
                gv.CurrentRow.Cells(colTypeCode).Value = clsCommon.myCstr(dt.Rows(ii)("TypeCode"))
                gv.CurrentRow.Cells(colGrossAmt).Value = clsCommon.myCdbl(dt.Rows(ii)("GrossAmount"))
                gv.CurrentRow.Cells(colLimitAmt).Value = clsCommon.myCdbl(dt.Rows(ii)("MaxLimit"))
                gv.CurrentRow.Cells(colApplicableAmt).Value = clsCommon.myCdbl(dt.Rows(ii)("ApplicableAmt"))
                gv.Rows.AddNew()
            Next
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            LoadBlankGrid()
            LoadBlankGridEmp()
            LoadBlankGridTax()
        End Try
    End Sub

    Function AddSlabGrid(ByVal strEmpCode As String, ByVal strEmpName As String, ByVal dtFiscalYearFromDate As DateTime, ByVal dclTaxableAmt As Decimal, ByRef TotalTDSAmt As Decimal) As String
        TotalTDSAmt = 0
        Dim qry As String = "select sex,DATEDIFF(year,convert(date, Birth_date,103), '" + clsCommon.GetPrintDate(dtFiscalYearFromDate, "dd/MMM/yyyy") + "') as Age  from TSPL_EMPLOYEE_MASTER where EMP_CODE='" + strEmpCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            Throw New Exception("No Data found for Employee [" + strEmpCode + "]")
        End If
        If clsCommon.myLen(dt.Rows(0)("sex")) <= 0 Then
            Throw New Exception("Please define sex of Employee [" + strEmpCode + "]")
        End If
        If clsCommon.myCdbl(dt.Rows(0)("Age")) < 18 Then
            Throw New Exception("Please check Date of Birth of Employee [" + strEmpCode + "].Age should be Greater then 18")
        End If
        qry = "select TSPL_HR_TDS_INCOME_TAX_SLAB.Tax_Group ,TSPL_HR_TDS_INCOME_TAX_SLAB_DETAIL.*" + Environment.NewLine + _
        "from TSPL_HR_TDS_INCOME_TAX_SLAB" + Environment.NewLine + _
        "left outer join TSPL_HR_TDS_INCOME_TAX_SLAB_DETAIL on TSPL_HR_TDS_INCOME_TAX_SLAB_DETAIL.Code=TSPL_HR_TDS_INCOME_TAX_SLAB.Code where 2=2 " + Environment.NewLine
        If clsCommon.myCdbl(dt.Rows(0)("Age")) > 80 Then
            qry += " and TSPL_HR_TDS_INCOME_TAX_SLAB.Type='SRCA'"
        ElseIf clsCommon.myCdbl(dt.Rows(0)("Age")) > 60 Then
            qry += " and TSPL_HR_TDS_INCOME_TAX_SLAB.Type='SRC'"
        ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("sex")), "Male") = CompairStringResult.Equal Then
            qry += " and TSPL_HR_TDS_INCOME_TAX_SLAB.Type='M'"
        ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("sex")), "Female") = CompairStringResult.Equal Then
            qry += " and TSPL_HR_TDS_INCOME_TAX_SLAB.Type='F'"
        Else
            qry += " and TSPL_HR_TDS_INCOME_TAX_SLAB.Type='O'"
        End If
        qry += " and TSPL_HR_TDS_INCOME_TAX_SLAB.Status=1 and TSPL_HR_TDS_INCOME_TAX_SLAB.Fiscal_Code='" + txtFiscalYear.Value + "'" + Environment.NewLine + _
        "order by TSPL_HR_TDS_INCOME_TAX_SLAB_DETAIL.SNo"
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            Throw New Exception("No Income Tax TDS Slab found for Employee [" + strEmpCode + "]")
        End If
        qry = "select TSPL_TAX_GROUP_DETAILS.Tax_Code,TSPL_TAX_GROUP_DETAILS.Tax_Code_Desc,Taxable,Surtax,Surtax_Tax_Code,Tax_On_Base_Amount from TSPL_TAX_GROUP_DETAILS where Tax_Group_Code='" + dt.Rows(0)("Tax_Group") + "' and Tax_Group_Type='H' order by Trans_Code"
        Dim dtTax As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dtTax Is Nothing OrElse dtTax.Rows.Count <= 0 Then
            Throw New Exception("Tax Detail not found for HR and Payroll tax group [" + clsCommon.myCstr(dt.Rows(0)("Tax_Group")) + "]")
        End If
        Dim dclBalance As Decimal = dclTaxableAmt
        For ii As Integer = 0 To dt.Rows.Count - 1
            gvTax.CurrentRow.Cells(colTaxSNo).Value = ii + 1
            gvTax.CurrentRow.Cells(colTaxEmpCode).Value = strEmpCode
            gvTax.CurrentRow.Cells(colTaxEmpName).Value = strEmpName
            gvTax.CurrentRow.Cells(colTaxSlabCode).Value = clsCommon.myCstr(dt.Rows(ii)("Code"))
            gvTax.CurrentRow.Cells(colTaxSlabTRCode).Value = clsCommon.myCstr(dt.Rows(ii)("TR_Code"))
            If clsCommon.myCdbl(clsCommon.myCdbl(dt.Rows(ii)("Taxable_Amt"))) < dclBalance Then
                gvTax.CurrentRow.Cells(colTaxSlabTaxableAmt).Value = clsCommon.myCdbl(dt.Rows(ii)("Taxable_Amt"))
                dclBalance -= clsCommon.myCdbl(dt.Rows(ii)("Taxable_Amt"))
            Else
                gvTax.CurrentRow.Cells(colTaxSlabTaxableAmt).Value = dclBalance
                dclBalance = 0
            End If
            SetitemWiseTaxSetting(dtTax)
            For jj As Integer = 1 To 10
                Dim Strii As String = clsCommon.myCstr(jj)
                gvTax.CurrentRow.Cells("COLTAXRATE" + Strii).Value = clsCommon.myCdbl(dt.Rows(ii)("TAX" + Strii + "_Rate"))

                Dim arrTaxableAuth As New List(Of String)
                Dim strTaxCode As String = clsCommon.myCstr(gvTax.CurrentRow.Cells(clsCommon.myCstr("COLTAX" + Strii)).Value)
                If clsCommon.myLen(strTaxCode) > 0 Then
                    Dim dblCurrentTaxableAmount As Decimal = clsCommon.myCdbl(gvTax.CurrentRow.Cells(colTaxSlabTaxableAmt).Value)
                    Dim dblTaxRate As Double = clsCommon.myCdbl(gvTax.CurrentRow.Cells(clsCommon.myCstr("COLTAXRATE" + Strii)).Value)
                    Dim IsSurTax As Boolean = clsCommon.myCBool(gvTax.CurrentRow.Cells(clsCommon.myCstr("ISSURTAX" + Strii)).Value)
                    Dim IsTaxOnBaseAmt As Boolean = clsCommon.myCBool(gvTax.CurrentRow.Cells(clsCommon.myCstr("ISTAXONBASEAMT" + Strii)).Value)
                    Dim strSurTaxCode As String = clsCommon.myCstr(gvTax.CurrentRow.Cells(clsCommon.myCstr("SURTAXCODE" + Strii)).Value)
                    Dim IsTaxable As Boolean = clsCommon.myCBool(gvTax.CurrentRow.Cells(clsCommon.myCstr("ISTAXABLE" + Strii)).Value)
                    Dim dblBaseAmt As Double = 0
                    Dim dblTaxAmt As Double = 0
                    If IsSurTax Then
                        Dim dblSurTaxAmt As Double = GetCurrentRowSurTaxAmt(gvTax.CurrentRow.Index, jj, strSurTaxCode)
                        dblBaseAmt = dblSurTaxAmt
                    Else
                        Dim dblOtherTaxAmt As Double = 0
                        If Not IsTaxOnBaseAmt Then
                            dblOtherTaxAmt = GetCurrentRowOtherTaxAmt(gvTax.CurrentRow.Index, Strii, arrTaxableAuth)
                        End If
                        dblBaseAmt = (dblCurrentTaxableAmount + dblOtherTaxAmt)
                    End If
                    gvTax.CurrentRow.Cells(clsCommon.myCstr("COLTAXBASEAMT" + Strii)).Value = Math.Round(dblBaseAmt, 2)
                    dblTaxAmt = (dblBaseAmt * dblTaxRate) / 100
                    gvTax.CurrentRow.Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value = Math.Round(dblTaxAmt, IIf(objCommonVar.IsRoundOffTaxToZeroDecimal, 0, 2))
                    If IsTaxable AndAlso Not arrTaxableAuth.Contains(strTaxCode.ToUpper()) Then
                        arrTaxableAuth.Add(strTaxCode.ToUpper())
                    End If
                End If
            Next
            gvTax.CurrentRow.Cells(ColTaxTDSAmt).Value = GetCurrentRowTotalTaxAmt(gvTax.CurrentRow.Index)
            TotalTDSAmt += clsCommon.myCdbl(gvTax.CurrentRow.Cells(ColTaxTDSAmt).Value)
            gvTax.Rows.AddNew()
        Next
        Return clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
    End Function

    Private Function GetCurrentRowTotalTaxAmt(ByVal IntRowNo As Integer) As Double
        Dim dblTotTax As Double = 0
        For ii As Integer = 1 To 10
            Dim strii As String = clsCommon.myCstr(ii)
            If IntRowNo < 0 Then
                dblTotTax = dblTotTax + clsCommon.myCdbl(gvTax.CurrentRow.Cells(clsCommon.myCstr("COLTAXAMT" + strii)).Value)
            Else
                dblTotTax = dblTotTax + clsCommon.myCdbl(gvTax.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXAMT" + strii)).Value)
            End If
        Next
        Return dblTotTax
    End Function

    Private Function GetCurrentRowSurTaxAmt(ByVal IntRowNo As Integer, ByVal intEndCol As Integer, ByVal strSurTaxCode As String) As Double
        For ii As Integer = 1 To intEndCol
            Dim strii As String = clsCommon.myCstr(ii)
            If IntRowNo < 0 Then
                If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gvTax.CurrentRow.Cells(clsCommon.myCstr("colTax" + strii)).Value)) = CompairStringResult.Equal Then
                    Return clsCommon.myCdbl(gvTax.CurrentRow.Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
                End If
            Else
                If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gvTax.Rows(IntRowNo).Cells(clsCommon.myCstr("colTax" + strii)).Value)) = CompairStringResult.Equal Then
                    Return clsCommon.myCdbl(gvTax.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
                End If
            End If
        Next
        Return 0
    End Function

    Private Function GetCurrentRowOtherTaxAmt(ByVal IntRowNo As Integer, ByVal intEndCol As Integer, ByVal arrTaxableAuth As List(Of String)) As Double
        Dim dblRet As Double = 0
        For Each strTaxAuth As String In arrTaxableAuth
            For ii As Integer = 1 To intEndCol
                Dim strii As String = clsCommon.myCstr(ii)
                If IntRowNo < 0 Then
                    If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gvTax.CurrentRow.Cells(clsCommon.myCstr("colTax" + strii)).Value)) = CompairStringResult.Equal Then
                        dblRet = dblRet + clsCommon.myCdbl(gvTax.CurrentRow.Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
                    End If
                Else
                    If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gvTax.Rows(IntRowNo).Cells(clsCommon.myCstr("colTax" + strii)).Value)) = CompairStringResult.Equal Then
                        dblRet = dblRet + clsCommon.myCdbl(gvTax.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
                    End If
                End If
            Next
        Next
        Return dblRet
    End Function

    Sub SetitemWiseTaxSetting(ByVal dt As DataTable)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            Dim ii As Integer = 1
            For Each dr As DataRow In dt.Rows
                Dim strII As String = clsCommon.myCstr(ii)
                gvTax.CurrentRow.Cells(clsCommon.myCstr("colTax" + strII)).Value = clsCommon.myCstr(dr("Tax_Code"))
                gvTax.CurrentRow.Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                gvTax.CurrentRow.Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                gvTax.CurrentRow.Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                gvTax.CurrentRow.Cells(clsCommon.myCstr("ISTAXONBASEAMT" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_On_Base_Amount")), "Y") = CompairStringResult.Equal)
                ii = ii + 1
            Next
        End If
    End Sub
    
End Class

