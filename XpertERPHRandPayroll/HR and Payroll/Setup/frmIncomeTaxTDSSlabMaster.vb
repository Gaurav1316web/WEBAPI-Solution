Imports common
Imports System.Data.SqlClient
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class frmIncomeTaxTDSSlabMaster
    Inherits FrmMainTranScreen
#Region "Variables"
    Const colSNo As String = "colSNo"
    Const colFromRange As String = "colFromRange"
    Const colToRange As String = "colToRange"
    Const colTaxableAmt As String = "colTaxableAmt"
    Const ColTAX1 As String = "ColTAX1"
    Const ColTax1Rate As String = "ColTax1Rate"
    Const ColTAX2 As String = "ColTAX2"
    Const ColTax2Rate As String = "ColTax2Rate"
    Const ColTAX3 As String = "ColTAX3"
    Const ColTax3Rate As String = "ColTax3Rate"
    Const ColTAX4 As String = "ColTAX4"
    Const ColTax4Rate As String = "ColTax4Rate"
    Const ColTAX5 As String = "ColTAX5"
    Const ColTax5Rate As String = "ColTax5Rate"
    Const ColTAX6 As String = "ColTAX6"
    Const ColTax6Rate As String = "ColTax6Rate"
    Const ColTAX7 As String = "ColTAX7"
    Const ColTax7Rate As String = "ColTax7Rate"
    Const ColTAX8 As String = "ColTAX8"
    Const ColTax8Rate As String = "ColTax8Rate"
    Const ColTAX9 As String = "ColTAX9"
    Const ColTax9Rate As String = "ColTax9Rate"
    Const ColTAX10 As String = "ColTAX10"
    Const ColTax10Rate As String = "ColTax10Rate"

    Private isInsideLoadData As Boolean = False
    Dim isNewEntry As Boolean = True
    Dim isCellValueChangedOpen As Boolean = False
    Dim dt As DataTable
    Dim qry As String
    Dim CurrentDate As DateTime = clsCommon.GETSERVERDATE()
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim SettNoOFSlabForImportExport As Integer
    ''BHA/31/12/18-000771 by balwinder on 10/01/2019
#End Region

    Private Sub frmSaleIncentiveMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            SetUserMgmtNew()
            Reset()
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
            ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D To Delete ")
            ButtonToolTip.SetToolTip(btnClose, "Press Alt+C To Close the Window")
            ButtonToolTip.SetToolTip(btnReset, "Press Alt+N For New")
            ButtonToolTip.SetToolTip(btnPost, "Press Alt+P to Post the Transaction")
            ValidateLength()
            LoadType()

            SettNoOFSlabForImportExport = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.NoOFSlabForImportExport, clsFixedParameterCode.NoOFSlabForImportExport, Nothing))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub LoadType()
        cboType.DataSource = Nothing
        Dim dt As New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Name")

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select..."
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "M"
        dr("Name") = "Male"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "F"
        dr("Name") = "Female"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "O"
        dr("Name") = "Other"
        dt.Rows.Add(dr)


        dr = dt.NewRow()
        dr("Code") = "SRC"
        dr("Name") = "Senior Citizen[60-80]"
        dt.Rows.Add(dr)


        dr = dt.NewRow()
        dr("Code") = "SRCA"
        dr("Name") = "Senior Citizen[Above 80]"
        dt.Rows.Add(dr)
         

        cboType.DataSource = dt
        cboType.ValueMember = "Code"
        cboType.DisplayMember = "Name"
    End Sub

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        If btnSave.Visible = True Then
            rmiImport.Enabled = True
            rmiExport.Enabled = True
        Else
            rmiImport.Enabled = False
            rmiExport.Enabled = False
        End If
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPost.Visible = MyBase.isPostFlag
    End Sub

    Private Sub ValidateLength()
        txtDesc.MaxLength = 200
    End Sub

    Private Sub Reset()
        BlankAllControl()
        txtCode.MyReadOnly = False
        btnSave.Text = "Save"
        lblPending.Status = ERPTransactionStatus.Pending
        isCellValueChangedOpen = False
        isNewEntry = True
        btnSave.Enabled = True
        btnDelete.Enabled = False
        btnPost.Enabled = False
        chkInactive.Enabled = False
        chkInactive.Checked = False
        isCellValueChangedOpen = False
    End Sub

    Sub BlankAllControl()
        txtCode.Value = ""
        cboType.SelectedValue = ""
        txtDesc.Text = ""
        lblPending.Status = ERPTransactionStatus.Pending
        txtFiscalYear.Value = ""
        lblFiscalYear.Text = ""
        txtTaxGroup.Value = ""
        lblTaxGroup.Text = ""
        LoadBlankIncentiveGrid()
    End Sub

    Sub LoadBlankIncentiveGrid()
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

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = "{0:n2}"
        repoDecimal.HeaderText = "From Range"
        repoDecimal.Name = colFromRange
        repoDecimal.Width = 100
        repoDecimal.Minimum = 0
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoDecimal)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = "{0:n2}"
        repoDecimal.HeaderText = "To Range"
        repoDecimal.Name = colToRange
        repoDecimal.Width = 100
        repoDecimal.Minimum = 0
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoDecimal)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = "{0:n2}"
        repoDecimal.HeaderText = "Taxable Amount"
        repoDecimal.Name = colTaxableAmt
        repoDecimal.Width = 100
        repoDecimal.ReadOnly = True
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoDecimal)

        Dim repoTextBox As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTextBox.HeaderText = "Tax 1"
        repoTextBox.Name = ColTAX1
        repoTextBox.Width = 80
        repoTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoTextBox)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = "{0:n2}"
        repoDecimal.HeaderText = "Tax Rate 1"
        repoDecimal.Name = ColTax1Rate
        repoDecimal.Width = 80
        repoDecimal.ReadOnly = False
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoDecimal)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.HeaderText = "Tax 2"
        repoTextBox.Name = ColTAX2
        repoTextBox.Width = 80
        repoTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoTextBox)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = "{0:n2}"
        repoDecimal.HeaderText = "Tax Rate 2"
        repoDecimal.Name = ColTax2Rate
        repoDecimal.Width = 80
        repoDecimal.ReadOnly = False
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoDecimal)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.HeaderText = "Tax3"
        repoTextBox.Name = ColTAX3
        repoTextBox.Width = 80
        repoTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoTextBox)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = "{0:n2}"
        repoDecimal.HeaderText = "Tax Rate 3"
        repoDecimal.Name = ColTax3Rate
        repoDecimal.Width = 80
        repoDecimal.ReadOnly = False
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoDecimal)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.HeaderText = "Tax 4"
        repoTextBox.Name = ColTAX4
        repoTextBox.Width = 80
        repoTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoTextBox)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = "{0:n2}"
        repoDecimal.HeaderText = "Tax Rate 4"
        repoDecimal.Name = ColTax4Rate
        repoDecimal.Width = 80
        repoDecimal.ReadOnly = False
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoDecimal)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.HeaderText = "Tax 5"
        repoTextBox.Name = ColTAX5
        repoTextBox.Width = 80
        repoTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoTextBox)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = "{0:n2}"
        repoDecimal.HeaderText = "Tax Rate 5"
        repoDecimal.Name = ColTax5Rate
        repoDecimal.Width = 80
        repoDecimal.ReadOnly = False
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoDecimal)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.HeaderText = "Tax 6"
        repoTextBox.Name = ColTAX6
        repoTextBox.Width = 80
        repoTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoTextBox)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = "{0:n2}"
        repoDecimal.HeaderText = "Tax Rate 6"
        repoDecimal.Name = ColTax6Rate
        repoDecimal.Width = 80
        repoDecimal.ReadOnly = False
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoDecimal)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.HeaderText = "Tax 7"
        repoTextBox.Name = ColTAX7
        repoTextBox.Width = 80
        repoTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoTextBox)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = "{0:n2}"
        repoDecimal.HeaderText = "Tax Rate 7"
        repoDecimal.Name = ColTax7Rate
        repoDecimal.Width = 80
        repoDecimal.ReadOnly = False
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoDecimal)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.HeaderText = "Tax 8"
        repoTextBox.Name = ColTAX8
        repoTextBox.Width = 80
        repoTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoTextBox)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = "{0:n2}"
        repoDecimal.HeaderText = "Tax Rate 8"
        repoDecimal.Name = ColTax8Rate
        repoDecimal.Width = 80
        repoDecimal.ReadOnly = False
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoDecimal)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.HeaderText = "Tax 9"
        repoTextBox.Name = ColTAX9
        repoTextBox.Width = 80
        repoTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoTextBox)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = "{0:n2}"
        repoDecimal.HeaderText = "Tax Rate 9"
        repoDecimal.Name = ColTax9Rate
        repoDecimal.Width = 80
        repoDecimal.ReadOnly = False
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoDecimal)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.HeaderText = "Tax 10"
        repoTextBox.Name = ColTAX10
        repoTextBox.Width = 80
        repoTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoTextBox)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = "{0:n2}"
        repoDecimal.HeaderText = "Tax Rate 10"
        repoDecimal.Name = ColTax10Rate
        repoDecimal.Width = 80
        repoDecimal.ReadOnly = False
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoDecimal)
         


        gv.AllowAddNewRow = False
        gv.AllowDeleteRow = True
        gv.AllowRowReorder = False
        gv.ShowGroupPanel = False
        gv.EnableFiltering = False
        gv.EnableSorting = False
        gv.EnableGrouping = False
        gv.AllowColumnChooser = True
        gv.AllowColumnReorder = True
        gv.Rows.AddNew()
    End Sub

    Sub SaveData(ByVal isPost As Boolean)
        Try
            If (AllowToSave()) Then
                Dim obj As New clsIncomeTaxTDSSlabHead()
                obj.Code = txtCode.Value
                obj.Description = txtDesc.Text
                obj.Fiscal_Code = txtFiscalYear.Value
                obj.Tax_Group = txtTaxGroup.Value
                obj.Type = clsCommon.myCstr(cboType.SelectedValue)
                obj.Arr = New List(Of clsIncomeTaxTDSSlabDetail)
                Dim Count As Integer = 1
                For Each grow As GridViewRowInfo In gv.Rows
                    If clsCommon.myLen(grow.Cells(colFromRange).Value) > 0 OrElse clsCommon.myLen(grow.Cells(colToRange).Value) > 0 Then
                        Dim objTr As New clsIncomeTaxTDSSlabDetail()
                        objTr.SNo = Count
                        objTr.From_Range = clsCommon.myCdbl(grow.Cells(colFromRange).Value)
                        objTr.To_Range = clsCommon.myCdbl(grow.Cells(colToRange).Value)
                        objTr.Taxable_Amt = clsCommon.myCdbl(grow.Cells(colTaxableAmt).Value)
                        objTr.TAX1 = clsCommon.myCstr(grow.Cells(ColTAX1).Value)
                        objTr.TAX1_Rate = clsCommon.myCdbl(grow.Cells(ColTax1Rate).Value)
                        objTr.TAX2 = clsCommon.myCstr(grow.Cells(ColTAX2).Value)
                        objTr.TAX2_Rate = clsCommon.myCdbl(grow.Cells(ColTax2Rate).Value)
                        objTr.TAX3 = clsCommon.myCstr(grow.Cells(ColTAX3).Value)
                        objTr.TAX3_Rate = clsCommon.myCdbl(grow.Cells(ColTax3Rate).Value)
                        objTr.TAX4 = clsCommon.myCstr(grow.Cells(ColTAX4).Value)
                        objTr.TAX4_Rate = clsCommon.myCdbl(grow.Cells(ColTax4Rate).Value)
                        objTr.TAX5 = clsCommon.myCstr(grow.Cells(ColTAX5).Value)
                        objTr.TAX5_Rate = clsCommon.myCdbl(grow.Cells(ColTax5Rate).Value)
                        objTr.TAX6 = clsCommon.myCstr(grow.Cells(ColTAX6).Value)
                        objTr.TAX6_Rate = clsCommon.myCdbl(grow.Cells(ColTax6Rate).Value)
                        objTr.TAX7 = clsCommon.myCstr(grow.Cells(ColTAX7).Value)
                        objTr.TAX7_Rate = clsCommon.myCdbl(grow.Cells(ColTax7Rate).Value)
                        objTr.TAX8 = clsCommon.myCstr(grow.Cells(ColTAX8).Value)
                        objTr.TAX8_Rate = clsCommon.myCdbl(grow.Cells(ColTax8Rate).Value)
                        objTr.TAX9 = clsCommon.myCstr(grow.Cells(ColTAX9).Value)
                        objTr.TAX9_Rate = clsCommon.myCdbl(grow.Cells(ColTax9Rate).Value)
                        objTr.TAX10 = clsCommon.myCstr(grow.Cells(ColTAX10).Value)
                        objTr.TAX10_Rate = clsCommon.myCdbl(grow.Cells(ColTax10Rate).Value)
                        Count += 1
                        obj.Arr.Add(objTr)
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
        If clsCommon.myLen(cboType.SelectedValue) <= 0 Then
            cboType.Focus()
            Throw New Exception("Please select Type")
        End If
        If clsCommon.myLen(txtDesc.Text) <= 0 Then
            txtDesc.Focus()
            Throw New Exception("Please enter Incentive Description")
        End If
        If clsCommon.myLen(txtFiscalYear.Value) <= 0 Then
            txtFiscalYear.Focus()
            Throw New Exception("Please select Fiscal Year")
        End If
      
        If clsCommon.myLen(txtTaxGroup.Value) <= 0 Then
            txtTaxGroup.Focus()
            Throw New Exception("Please select Tax Group")
        End If
         
        If gv.Rows.Count > 0 Then
            For ii As Integer = 0 To gv.RowCount - 1
                CalculateCurrentRow(ii)
                SetTaxDetail(ii)
                If clsCommon.myCdbl(gv.Rows(ii).Cells(colToRange).Value) > 0 AndAlso clsCommon.myCdbl(gv.Rows(ii).Cells(colFromRange).Value) > 0 Then
                    If clsCommon.myCdbl(gv.Rows(ii).Cells(colFromRange).Value) > clsCommon.myCdbl(gv.Rows(ii).Cells(colToRange).Value) Then
                        Throw New Exception("From Range should be less then To Range . At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)))
                    End If
                    If clsCommon.myLen(gv.Rows(ii).Cells(colTaxableAmt).Value) <= 0 Then
                        Throw New Exception("Taxable amount should be greater than 0. At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)))
                    End If
                End If
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
            Reset()
            isInsideLoadData = True
            Dim obj As New clsIncomeTaxTDSSlabHead
            obj = clsIncomeTaxTDSSlabHead.GetData(strIncentiveCode, NavType)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
                isNewEntry = False
                btnSave.Text = "Update"
                txtCode.Value = obj.Code
                txtDesc.Text = obj.Description
                txtFiscalYear.Value = obj.Fiscal_Code
                lblFiscalYear.Text = clsDBFuncationality.getSingleValue("select Fiscal_Name from TSPL_Fiscal_Year_Master where fiscal_code='" + obj.Fiscal_Code + "'")
                txtTaxGroup.Value = obj.Tax_Group
                lblTaxGroup.Text = clsDBFuncationality.getSingleValue("Select Tax_Group_Desc from TSPL_TAX_GROUP_MASTER Where Tax_Group_Code='" + obj.Tax_Group + "' and Tax_Group_Type='H'  ")
                cboType.SelectedValue = obj.Type
                For Each objTR As clsIncomeTaxTDSSlabDetail In obj.Arr
                    gv.CurrentRow.Cells(colSNo).Value = objTR.SNo
                    gv.CurrentRow.Cells(colFromRange).Value = objTR.From_Range
                    gv.CurrentRow.Cells(colToRange).Value = objTR.To_Range
                    gv.CurrentRow.Cells(colTaxableAmt).Value = objTR.Taxable_Amt
                    gv.CurrentRow.Cells(ColTAX1).Value = objTR.TAX1
                    gv.CurrentRow.Cells(ColTax1Rate).Value = objTR.TAX1_Rate
                    gv.CurrentRow.Cells(ColTAX2).Value = objTR.TAX2
                    gv.CurrentRow.Cells(ColTax2Rate).Value = objTR.TAX2_Rate
                    gv.CurrentRow.Cells(ColTAX3).Value = objTR.TAX3
                    gv.CurrentRow.Cells(ColTax3Rate).Value = objTR.TAX3_Rate
                    gv.CurrentRow.Cells(ColTAX4).Value = objTR.TAX4
                    gv.CurrentRow.Cells(ColTax4Rate).Value = objTR.TAX4_Rate
                    gv.CurrentRow.Cells(ColTAX5).Value = objTR.TAX5
                    gv.CurrentRow.Cells(ColTax5Rate).Value = objTR.TAX5_Rate
                    gv.CurrentRow.Cells(ColTAX6).Value = objTR.TAX6
                    gv.CurrentRow.Cells(ColTax6Rate).Value = objTR.TAX6_Rate
                    gv.CurrentRow.Cells(ColTAX7).Value = objTR.TAX7
                    gv.CurrentRow.Cells(ColTax7Rate).Value = objTR.TAX7_Rate
                    gv.CurrentRow.Cells(ColTAX8).Value = objTR.TAX8
                    gv.CurrentRow.Cells(ColTax8Rate).Value = objTR.TAX8_Rate
                    gv.CurrentRow.Cells(ColTAX9).Value = objTR.TAX9
                    gv.CurrentRow.Cells(ColTax9Rate).Value = objTR.TAX9_Rate
                    gv.CurrentRow.Cells(ColTAX10).Value = objTR.TAX10
                    gv.CurrentRow.Cells(ColTax10Rate).Value = objTR.TAX10_Rate
                    gv.Rows.AddNew()
                Next
                lblPending.Status = obj.Status
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnDelete.Enabled = False
                    btnPost.Enabled = False
                    chkInactive.Enabled = True
                Else
                    btnSave.Enabled = True
                    btnDelete.Enabled = True
                    btnPost.Enabled = True
                    chkInactive.Enabled = False
                End If
                chkInactive.Checked = obj.In_Active
                If obj.In_Active Then
                    chkInactive.Enabled = False
                End If
                If Not MyBase.isModifyFlag Then
                    chkInactive.Enabled = False
                End If
            Else
                Reset()
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
                If clsIncomeTaxTDSSlabHead.fundelete(strIcentiveCode) Then
                    clsCommon.MyMessageBoxShow(Me, "Data deleted successfully.", Me.Text)
                    Reset()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub rmiExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiExport.Click
        Dim str As String
        str = "select '' as [Description],'' as [Fiscal Code],'' as [Type(M/F/O/SRC/SRCA)],'' AS [Tax Group],'' as [From Range] "
        For ii As Integer = 1 To SettNoOFSlabForImportExport
            str += ",'' as [To Range " + clsCommon.myCstr(ii) + "]"
            str += ",'' as [Slab " + clsCommon.myCstr(ii) + " Tax Rate 1],'' as [Slab " + clsCommon.myCstr(ii) + " Tax Rate 2],'' as [Slab " + clsCommon.myCstr(ii) + " Tax Rate 3],'' as [Slab " + clsCommon.myCstr(ii) + " Tax Rate 4],'' as [Slab " + clsCommon.myCstr(ii) + " Tax Rate 5],'' as [Slab " + clsCommon.myCstr(ii) + " Tax Rate 6],'' as [Slab " + clsCommon.myCstr(ii) + " Tax Rate 7],'' as [Slab " + clsCommon.myCstr(ii) + " Tax Rate 8],'' as [Slab " + clsCommon.myCstr(ii) + " Tax Rate 9],'' as [Slab " + clsCommon.myCstr(ii) + " Tax Rate 10]"
        Next
        transportSql.ExporttoExcelWithoutFilter(str, "", "", Me)
    End Sub

    Private Sub rmiImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Try
            Dim currentdate As Date = Date.Today
            Dim linno As Integer = 0

            Dim Strs As List(Of String) = New List(Of String)
            Strs.Add("Description")
            Strs.Add("Fiscal Code")
            Strs.Add("Type(M/F/O/SRC/SRCA)")
            Strs.Add("Tax Group")
            Strs.Add("From Range")
            For ii As Integer = 1 To SettNoOFSlabForImportExport
                Strs.Add("To Range " + clsCommon.myCstr(ii))
                Strs.Add("Slab " + clsCommon.myCstr(ii) + " Tax Rate 1")
                Strs.Add("Slab " + clsCommon.myCstr(ii) + " Tax Rate 2")
                Strs.Add("Slab " + clsCommon.myCstr(ii) + " Tax Rate 3")
                Strs.Add("Slab " + clsCommon.myCstr(ii) + " Tax Rate 4")
                Strs.Add("Slab " + clsCommon.myCstr(ii) + " Tax Rate 5")
                Strs.Add("Slab " + clsCommon.myCstr(ii) + " Tax Rate 6")
                Strs.Add("Slab " + clsCommon.myCstr(ii) + " Tax Rate 7")
                Strs.Add("Slab " + clsCommon.myCstr(ii) + " Tax Rate 8")
                Strs.Add("Slab " + clsCommon.myCstr(ii) + " Tax Rate 9")
                Strs.Add("Slab " + clsCommon.myCstr(ii) + " Tax Rate 10")
            Next
            Dim qry As String
             If transportSql.importExcel(gv, Strs.ToArray()) Then
                Dim trans As SqlTransaction = Nothing
                Try
                    trans = clsDBFuncationality.GetTransactin()
                    clsCommon.ProgressBarShow()
                    For Each grow As GridViewRowInfo In gv.Rows
                        Dim obj As New clsIncomeTaxTDSSlabHead()
                        linno += 1
                        obj.Fiscal_Code = clsCommon.myCstr(grow.Cells("Fiscal Code").Value)
                        obj.Tax_Group = clsCommon.myCstr(grow.Cells("Tax Group").Value)

                        If clsCommon.myLen(obj.Fiscal_Code) > 0 AndAlso clsCommon.myLen(obj.Tax_Group) > 0 Then
                            obj.Description = clsCommon.myCstr(grow.Cells("Description").Value)
                            If clsCommon.myLen(obj.Description) > 200 Then
                                Throw New Exception("Please enter Description ")
                            ElseIf clsCommon.myLen(obj.Description) > 200 Then
                                Throw New Exception("Description length can not be more than 200")
                            End If
                            If clsCommon.myLen(obj.Fiscal_Code) > 0 Then
                                qry = "select Fiscal_Code from TSPL_Fiscal_Year_Master where Fiscal_Code='" + obj.Fiscal_Code + "'"
                                obj.Fiscal_Code = clsDBFuncationality.getSingleValue(qry, trans)
                                If clsCommon.myLen(obj.Fiscal_Code) <= 0 Then
                                    Throw New Exception("Invalid Fiscal Code [" + clsCommon.myCstr(grow.Cells("Fiscal Code").Value) + "]")
                                End If
                            End If
                            If clsCommon.myLen(obj.Tax_Group) > 0 Then
                                qry = "select Tax_Group_Code from TSPL_TAX_GROUP_MASTER where Tax_Group_Code='" + obj.Tax_Group + "' and Tax_Group_Type='H'"
                                obj.Tax_Group = clsDBFuncationality.getSingleValue(qry, trans)
                                If clsCommon.myLen(obj.Tax_Group) <= 0 Then
                                    Throw New Exception("Invalid Tax Group Code [" + clsCommon.myCstr(grow.Cells("Fiscal Code").Value) + "]")
                                End If
                            End If

                            obj.Type = clsCommon.myCstr(grow.Cells("Type(M/F/O/SRC/SRCA)").Value)
                            If Not (clsCommon.CompairString(obj.Type, "M") = CompairStringResult.Equal _
                                    OrElse clsCommon.CompairString(obj.Type, "F") = CompairStringResult.Equal _
                                    OrElse clsCommon.CompairString(obj.Type, "O") = CompairStringResult.Equal _
                                    OrElse clsCommon.CompairString(obj.Type, "SRC") = CompairStringResult.Equal _
                                    OrElse clsCommon.CompairString(obj.Type, "SRCA") = CompairStringResult.Equal _
                                    ) Then
                                Throw New Exception("Invalid Type Code [" + clsCommon.myCstr(grow.Cells("Type(M/F/O/SRC/SRCA)").Value) + "]" + Environment.NewLine + "It Should be M/F/O/SRC/SRCA")
                            End If
                            Dim strFromRange As String = clsCommon.myCstr(grow.Cells("From Range").Value)
                            qry = "select tax_code,Tax_Code_Desc   from TSPL_TAX_GROUP_DETAILS where Tax_Group_Code='" + obj.Tax_Group + "' and Tax_Group_Type='H'  order by Trans_Code"
                            Dim dtTaxRate As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                            If dtTaxRate Is Nothing OrElse dtTaxRate.Rows.Count <= 0 Then
                                Throw New Exception("No Tax Group details found for tax group code " + obj.Tax_Group)
                            End If

                            '=======================Slab===========================================
                            obj.Arr = New List(Of clsIncomeTaxTDSSlabDetail)
                            Dim sno As Integer = 1
                            Dim strMakeFromRangeByToRange As String = Nothing
                            For i As Integer = 1 To SettNoOFSlabForImportExport
                                Dim objTR As New clsIncomeTaxTDSSlabDetail()
                                If i = 1 Then
                                    strFromRange = clsCommon.myCstr(strFromRange)
                                Else
                                    strFromRange = clsCommon.myCstr((clsCommon.myCdbl(strMakeFromRangeByToRange) + 0.01))
                                End If
                                Dim strToRange As String = clsCommon.myCstr(grow.Cells("To Range " + clsCommon.myCstr(i)).Value)
                                objTR.SNo = sno
                                If clsCommon.myLen(strFromRange) > 0 Then
                                    If IsNumeric(strFromRange) = False Then
                                        Throw New Exception("" + clsCommon.myCstr(grow.Cells("" + clsCommon.myCstr(i)).Value) + " should be Numeric at line no. " + clsCommon.myCstr(linno) + ".")
                                    End If
                                End If
                                If clsCommon.myLen(strToRange) > 0 Then
                                    If IsNumeric(strToRange) = False Then
                                        Throw New Exception("" + clsCommon.myCstr(grow.Cells("" + clsCommon.myCstr(i)).Value) + " should be Numeric at line no. " + clsCommon.myCstr(linno) + ".")
                                    End If
                                End If
                                If clsCommon.myCdbl(strFromRange) > 0 AndAlso clsCommon.myCdbl(strToRange) > 0 Then
                                    If clsCommon.myCdbl(strFromRange) > clsCommon.myCdbl(strToRange) AndAlso i > 1 Then
                                        Throw New Exception("[To Range] should be accending order. [To Range " + clsCommon.myCstr(i - 1) + "] can not be greater then [To Range " + clsCommon.myCstr(i) + "]  at line no. " + clsCommon.myCstr(linno) + ".")
                                    End If
                                End If
                                If strFromRange <> "" Then
                                    objTR.From_Range = strFromRange
                                End If
                                If strToRange <> "" Then
                                    objTR.To_Range = strToRange
                                End If
                                If strFromRange <> "" AndAlso strToRange <> "" Then
                                    objTR.Taxable_Amt = objTR.To_Range - objTR.From_Range
                                    If objTR.Taxable_Amt < 0 Then
                                        Throw New Exception("Taxable Amount coming -ve")
                                    End If

                                    If dtTaxRate.Rows.Count > 0 Then
                                        objTR.TAX1 = clsCommon.myCstr(dtTaxRate.Rows(0)("tax_code"))
                                        objTR.TAX1_Rate = clsCommon.myCdbl(grow.Cells("Slab " + clsCommon.myCstr(i) + " Tax Rate 1").Value)
                                        qry = "select Tax_Rate from TSPL_TAX_RATES where Tax_Type='H'  and Tax_Code='" + objTR.TAX1 + "'"
                                        Dim dttemp As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                        If dttemp Is Nothing OrElse dttemp.Rows.Count <= 0 Then
                                            Throw New Exception("Tax Rate 1 invalid Rate [" + clsCommon.myCstr(objTR.TAX1_Rate) + "]")
                                        End If
                                    End If
                                    If dtTaxRate.Rows.Count > 1 Then
                                        objTR.TAX2 = clsCommon.myCstr(dtTaxRate.Rows(1)("tax_code"))
                                        objTR.TAX2_Rate = clsCommon.myCdbl(grow.Cells("Slab " + clsCommon.myCstr(i) + " Tax Rate 2").Value)
                                        qry = "select Tax_Rate from TSPL_TAX_RATES where Tax_Type='H'  and Tax_Code='" + objTR.TAX2 + "'"
                                        Dim dttemp As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                        If dttemp Is Nothing OrElse dttemp.Rows.Count <= 0 Then
                                            Throw New Exception("Tax Rate 2 invalid Rate [" + clsCommon.myCstr(objTR.TAX1_Rate) + "]")
                                        End If
                                    End If
                                    If dtTaxRate.Rows.Count > 2 Then
                                        objTR.TAX3 = clsCommon.myCstr(dtTaxRate.Rows(2)("tax_code"))
                                        objTR.TAX3_Rate = clsCommon.myCdbl(grow.Cells("Slab " + clsCommon.myCstr(i) + " Tax Rate 3").Value)
                                        qry = "select Tax_Rate from TSPL_TAX_RATES where Tax_Type='H'  and Tax_Code='" + objTR.TAX3 + "'"
                                        Dim dttemp As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                        If dttemp Is Nothing OrElse dttemp.Rows.Count <= 0 Then
                                            Throw New Exception("Tax Rate 3 invalid Rate [" + clsCommon.myCstr(objTR.TAX1_Rate) + "]")
                                        End If
                                    End If
                                    If dtTaxRate.Rows.Count > 3 Then
                                        objTR.TAX4 = clsCommon.myCstr(dtTaxRate.Rows(3)("tax_code"))
                                        objTR.TAX4_Rate = clsCommon.myCdbl(grow.Cells("Slab " + clsCommon.myCstr(i) + " Tax Rate 4").Value)
                                        qry = "select Tax_Rate from TSPL_TAX_RATES where Tax_Type='H'  and Tax_Code='" + objTR.TAX4 + "'"
                                        Dim dttemp As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                        If dttemp Is Nothing OrElse dttemp.Rows.Count <= 0 Then
                                            Throw New Exception("Tax Rate 4 invalid Rate [" + clsCommon.myCstr(objTR.TAX1_Rate) + "]")
                                        End If
                                    End If
                                    If dtTaxRate.Rows.Count > 4 Then
                                        objTR.TAX5 = clsCommon.myCstr(dtTaxRate.Rows(4)("tax_code"))
                                        objTR.TAX5_Rate = clsCommon.myCdbl(grow.Cells("Slab " + clsCommon.myCstr(i) + " Tax Rate 5").Value)
                                        qry = "select Tax_Rate from TSPL_TAX_RATES where Tax_Type='H'  and Tax_Code='" + objTR.TAX5 + "'"
                                        Dim dttemp As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                        If dttemp Is Nothing OrElse dttemp.Rows.Count <= 0 Then
                                            Throw New Exception("Tax Rate 5 invalid Rate [" + clsCommon.myCstr(objTR.TAX1_Rate) + "]")
                                        End If
                                    End If
                                    If dtTaxRate.Rows.Count > 5 Then
                                        objTR.TAX6 = clsCommon.myCstr(dtTaxRate.Rows(5)("tax_code"))
                                        objTR.TAX6_Rate = clsCommon.myCdbl(grow.Cells("Slab " + clsCommon.myCstr(i) + " Tax Rate 6").Value)
                                        qry = "select Tax_Rate from TSPL_TAX_RATES where Tax_Type='H'  and Tax_Code='" + objTR.TAX6 + "'"
                                        Dim dttemp As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                        If dttemp Is Nothing OrElse dttemp.Rows.Count <= 0 Then
                                            Throw New Exception("Tax Rate 6 invalid Rate [" + clsCommon.myCstr(objTR.TAX1_Rate) + "]")
                                        End If
                                    End If
                                    If dtTaxRate.Rows.Count > 6 Then
                                        objTR.TAX7 = clsCommon.myCstr(dtTaxRate.Rows(6)("tax_code"))
                                        objTR.TAX7_Rate = clsCommon.myCdbl(grow.Cells("Slab " + clsCommon.myCstr(i) + " Tax Rate 7").Value)
                                        qry = "select Tax_Rate from TSPL_TAX_RATES where Tax_Type='H'  and Tax_Code='" + objTR.TAX7 + "'"
                                        Dim dttemp As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                        If dttemp Is Nothing OrElse dttemp.Rows.Count <= 0 Then
                                            Throw New Exception("Tax Rate 7 invalid Rate [" + clsCommon.myCstr(objTR.TAX1_Rate) + "]")
                                        End If
                                    End If
                                    If dtTaxRate.Rows.Count > 7 Then
                                        objTR.TAX8 = clsCommon.myCstr(dtTaxRate.Rows(7)("tax_code"))
                                        objTR.TAX8_Rate = clsCommon.myCdbl(grow.Cells("Slab " + clsCommon.myCstr(i) + " Tax Rate 8").Value)
                                        qry = "select Tax_Rate from TSPL_TAX_RATES where Tax_Type='H'  and Tax_Code='" + objTR.TAX8 + "'"
                                        Dim dttemp As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                        If dttemp Is Nothing OrElse dttemp.Rows.Count <= 0 Then
                                            Throw New Exception("Tax Rate 8 invalid Rate [" + clsCommon.myCstr(objTR.TAX1_Rate) + "]")
                                        End If
                                    End If
                                    If dtTaxRate.Rows.Count > 8 Then
                                        objTR.TAX9 = clsCommon.myCstr(dtTaxRate.Rows(8)("tax_code"))
                                        objTR.TAX9_Rate = clsCommon.myCdbl(grow.Cells("Slab " + clsCommon.myCstr(i) + " Tax Rate 9").Value)
                                        qry = "select Tax_Rate from TSPL_TAX_RATES where Tax_Type='H'  and Tax_Code='" + objTR.TAX9 + "'"
                                        Dim dttemp As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                        If dttemp Is Nothing OrElse dttemp.Rows.Count <= 0 Then
                                            Throw New Exception("Tax Rate 9 invalid Rate [" + clsCommon.myCstr(objTR.TAX1_Rate) + "]")
                                        End If
                                    End If
                                    If dtTaxRate.Rows.Count > 9 Then
                                        objTR.TAX10 = clsCommon.myCstr(dtTaxRate.Rows(9)("tax_code"))
                                        objTR.TAX10_Rate = clsCommon.myCdbl(grow.Cells("Slab " + clsCommon.myCstr(i) + " Tax Rate 10").Value)
                                        qry = "select Tax_Rate from TSPL_TAX_RATES where Tax_Type='H'  and Tax_Code='" + objTR.TAX10 + "'"
                                        Dim dttemp As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                        If dttemp Is Nothing OrElse dttemp.Rows.Count <= 0 Then
                                            Throw New Exception("Tax Rate 10 invalid Rate [" + clsCommon.myCstr(objTR.TAX1_Rate) + "]")
                                        End If
                                    End If
                                    obj.Arr.Add(objTR)
                                    sno = sno + 1
                                    strMakeFromRangeByToRange = strToRange
                                End If
                            Next
                            If obj.Arr.Count <= 0 Then
                                Throw New Exception("Atleast One Slab Enter.")
                            End If
                            If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                                obj.SaveData(obj, True, trans)
                            End If
                        End If
                    Next
                    trans.Commit()
                    clsCommon.ProgressBarHide()
                    clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarHide()
                    Throw New Exception("Error at Line No" + clsCommon.myCstr(linno) + Environment.NewLine + ex.Message)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            Me.Controls.Remove(gv)
        End Try
    End Sub

    Private Sub rmiClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiClose.Click
        Me.Close()
    End Sub

    Private Sub frmSaleIncentiveMaster_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData(False)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled Then
            DeleteData(txtCode.Value)
        End If
    End Sub

    Private Sub txtIncentive__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        txtCode.Value = clsIncomeTaxTDSSlabHead.getFinder("", txtCode.Value, isButtonClicked)
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
        For ii As Integer = 1 To gv.Rows.Count
            gv.Rows(ii - 1).Cells(colSNo).Value = ii
        Next
    End Sub

    Private Sub gv_UserDeletingRow(sender As Object, e As GridViewRowCancelEventArgs)
        If common.clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
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
                clsIncomeTaxTDSSlabHead.postData(txtCode.Value)
                clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                LoadData(txtCode.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv.Columns(colFromRange) Then
                        If clsCommon.myCdbl(clsCommon.myCstr(gv.Rows(gv.CurrentRow.Index).Cells(colToRange).Value)) <= 0 Then
                            gv.Rows(gv.CurrentRow.Index).Cells(colToRange).Value = 9999999999.0
                        End If
                        SetTaxDetail(gv.CurrentRow.Index)
                        CalculateCurrentRow(gv.CurrentRow.Index)
                    ElseIf e.Column Is gv.Columns(colToRange) Then
                        If gv.CurrentRow.Index = gv.Rows.Count - 1 Then
                            gv.Rows.AddNew()
                            gv.CurrentRow.Cells(colSNo).Value = clsCommon.myCdbl(gv.CurrentRow.Index + 1)
                            SetTaxDetail(gv.CurrentRow.Index)
                            gv.CurrentRow = gv.Rows(gv.CurrentRow.Index - 1)
                        End If
                        gv.Rows(gv.CurrentRow.Index + 1).Cells(colFromRange).Value = clsCommon.myCdbl(gv.Rows(gv.CurrentRow.Index).Cells(colToRange).Value + 0.01)
                        If clsCommon.myCdbl(clsCommon.myCstr(gv.Rows(gv.CurrentRow.Index + 1).Cells(colToRange).Value)) <= 0 Then
                            gv.Rows(gv.CurrentRow.Index + 1).Cells(colToRange).Value = 9999999999.0
                            CalculateCurrentRow(gv.CurrentRow.Index + 1)
                        End If
                        SetTaxDetail(gv.CurrentRow.Index)
                        CalculateCurrentRow(gv.CurrentRow.Index)
                    ElseIf e.Column Is gv.Columns(ColTax1Rate) Then
                        OpenTaxRateFinded(1)
                    ElseIf e.Column Is gv.Columns(ColTax2Rate) Then
                        OpenTaxRateFinded(2)
                    ElseIf e.Column Is gv.Columns(ColTax3Rate) Then
                        OpenTaxRateFinded(3)
                    ElseIf e.Column Is gv.Columns(ColTax4Rate) Then
                        OpenTaxRateFinded(4)
                    ElseIf e.Column Is gv.Columns(ColTax5Rate) Then
                        OpenTaxRateFinded(5)
                    ElseIf e.Column Is gv.Columns(ColTax6Rate) Then
                        OpenTaxRateFinded(6)
                    ElseIf e.Column Is gv.Columns(ColTax7Rate) Then
                        OpenTaxRateFinded(7)
                    ElseIf e.Column Is gv.Columns(ColTax8Rate) Then
                        OpenTaxRateFinded(8)
                    ElseIf e.Column Is gv.Columns(ColTax9Rate) Then
                        OpenTaxRateFinded(9)
                    ElseIf e.Column Is gv.Columns(ColTax10Rate) Then
                        OpenTaxRateFinded(10)
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            isCellValueChangedOpen = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub CalculateCurrentRow(ByVal ii As Integer)
        gv.Rows(ii).Cells(colTaxableAmt).Value = clsCommon.myCdbl(gv.Rows(ii).Cells(colToRange).Value) - clsCommon.myCdbl(gv.Rows(ii).Cells(colFromRange).Value)
    End Sub

    Sub OpenTaxRateFinded(ByVal TaxNo As Integer)
        Dim qry As String = "select Tax_Rate from TSPL_TAX_RATES"
        Dim whr As String = "Tax_Type='H'  and Tax_Code='" + clsCommon.myCstr(gv.CurrentRow.Cells("ColTAX" + clsCommon.myCstr(TaxNo)).Value) + "'"
        Dim str As String = clsCommon.ShowSelectForm("ITSL@TaxR", qry, "Tax_Rate", whr, clsCommon.myCstr(gv.CurrentRow.Cells("ColTAX" + clsCommon.myCstr(TaxNo) + "Rate").Value), "Tax_Rate", False)
        If clsCommon.myLen(str) > 0 Then
            gv.CurrentRow.Cells("ColTAX" + clsCommon.myCstr(TaxNo) + "Rate").Value = clsCommon.myCdbl(str)
        Else
            gv.CurrentRow.Cells("ColTAX" + clsCommon.myCstr(TaxNo) + "Rate").Value = Nothing
        End If
    End Sub

    Private Sub gv_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gv.CurrentColumnChanged
        If gv.RowCount > 0 Then
            Dim intCurrRow As Integer = gv.CurrentRow.Index
            gv.CurrentRow.Cells(colSNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv.Rows.Count - 1 Then
                gv.Rows.AddNew()
                gv.CurrentRow = gv.Rows(intCurrRow)
                SetTaxDetail(gv.CurrentRow.Index)
            End If
        End If
    End Sub

    Private Sub gv_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gv.CellFormatting
        If IsLoaded = True Then
            If e.Column Is gv.Columns(colFromRange) OrElse e.Column Is gv.Columns(colToRange) Then
                If clsCommon.CompairString(gv.CurrentRow.Index, 0) = CompairStringResult.Equal Then
                    gv.CurrentRow.Cells(colFromRange).ReadOnly = False
                Else
                    gv.CurrentRow.Cells(colFromRange).ReadOnly = True
                End If
                If String.IsNullOrEmpty(clsCommon.myCstr(gv.CurrentRow.Cells(colFromRange).Value)) = True Then
                    gv.CurrentRow.Cells(colToRange).ReadOnly = True
                Else
                    gv.CurrentRow.Cells(colToRange).ReadOnly = False
                End If
            ElseIf e.Column Is gv.Columns(ColTax1Rate) Then
                gv.CurrentRow.Cells(ColTax1Rate).ReadOnly = Not (clsCommon.myLen(gv.CurrentRow.Cells(ColTAX1).Value) > 0)
            ElseIf e.Column Is gv.Columns(ColTax2Rate) Then
                gv.CurrentRow.Cells(ColTax2Rate).ReadOnly = Not (clsCommon.myLen(gv.CurrentRow.Cells(ColTAX2).Value) > 0)
            ElseIf e.Column Is gv.Columns(ColTax3Rate) Then
                gv.CurrentRow.Cells(ColTax3Rate).ReadOnly = Not (clsCommon.myLen(gv.CurrentRow.Cells(ColTAX3).Value) > 0)
            ElseIf e.Column Is gv.Columns(ColTax4Rate) Then
                gv.CurrentRow.Cells(ColTax4Rate).ReadOnly = Not (clsCommon.myLen(gv.CurrentRow.Cells(ColTAX4).Value) > 0)
            ElseIf e.Column Is gv.Columns(ColTax5Rate) Then
                gv.CurrentRow.Cells(ColTax5Rate).ReadOnly = Not (clsCommon.myLen(gv.CurrentRow.Cells(ColTAX5).Value) > 0)
            ElseIf e.Column Is gv.Columns(ColTax6Rate) Then
                gv.CurrentRow.Cells(ColTax6Rate).ReadOnly = Not (clsCommon.myLen(gv.CurrentRow.Cells(ColTAX6).Value) > 0)
            ElseIf e.Column Is gv.Columns(ColTax7Rate) Then
                gv.CurrentRow.Cells(ColTax7Rate).ReadOnly = Not (clsCommon.myLen(gv.CurrentRow.Cells(ColTAX7).Value) > 0)
            ElseIf e.Column Is gv.Columns(ColTax8Rate) Then
                gv.CurrentRow.Cells(ColTax8Rate).ReadOnly = Not (clsCommon.myLen(gv.CurrentRow.Cells(ColTAX8).Value) > 0)
            ElseIf e.Column Is gv.Columns(ColTax9Rate) Then
                gv.CurrentRow.Cells(ColTax9Rate).ReadOnly = Not (clsCommon.myLen(gv.CurrentRow.Cells(ColTAX9).Value) > 0)
            ElseIf e.Column Is gv.Columns(ColTax10Rate) Then
                gv.CurrentRow.Cells(ColTax10Rate).ReadOnly = Not (clsCommon.myLen(gv.CurrentRow.Cells(ColTAX10).Value) > 0)
            End If
        End If
    End Sub

    Private Sub gv_CurrentRowChanged(sender As Object, e As CurrentRowChangedEventArgs) Handles gv.CurrentRowChanged
        If gv.RowCount > 0 Then
            Dim intCurrRow As Integer = gv.CurrentRow.Index
            gv.CurrentRow.Cells(colSNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv.Rows.Count - 1 Then
                gv.CurrentRow = gv.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub txtGLAccount__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtFiscalYear._MYValidating
        Dim Qry As String = "select Fiscal_Code, Fiscal_Name,Start_Date,End_Date from TSPL_Fiscal_Year_Master"
        txtFiscalYear.Value = clsCommon.ShowSelectForm("ITSL@Fiscal", Qry, "fiscal_code", "", txtFiscalYear.Value, "fiscal_code", isButtonClicked)
        lblFiscalYear.Text = clsDBFuncationality.getSingleValue("select  Fiscal_Name from TSPL_Fiscal_Year_Master where Fiscal_Code ='" + txtFiscalYear.Value + "' ")
    End Sub

    Private Sub chkInactive_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkInactive.ToggleStateChanged
        Try
            If Not isInsideLoadData Then
                If chkInactive.Checked Then
                    If clsCommon.myLen(txtCode.Value) > 0 Then
                        If clsCommon.MyMessageBoxShow(Me, "Current Incentive will be getting in active" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, Telerik.WinControls.RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                            clsIncomeTaxTDSSlabHead.InActiveData(txtCode.Value)
                            clsCommon.MyMessageBoxShow(Me, "Successfully Incentive the Incentive", Me.Text)
                            LoadData(txtCode.Value, NavigatorType.Current)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtTaxGroup__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtTaxGroup._MYValidating
        Dim Qry As String = "select Tax_Group_Code,Tax_Group_Desc  from TSPL_TAX_GROUP_MASTER"
        txtTaxGroup.Value = clsCommon.ShowSelectForm("ITSL@TaxG", Qry, "Tax_Group_Code", "Tax_Group_Type='H'", txtTaxGroup.Value, "Tax_Group_Code", isButtonClicked)
        lblTaxGroup.Text = clsDBFuncationality.getSingleValue("select Tax_Group_Desc from TSPL_TAX_GROUP_MASTER where Tax_Group_Code='" + txtTaxGroup.Value + "' ")
        For ii As Integer = 0 To gv.Rows.Count - 1
            SetTaxDetail(ii)
        Next
    End Sub

    Sub SetTaxDetail(ByVal RowIndex As Integer)
        Dim qry As String = "select tax_code,Tax_Code_Desc   from TSPL_TAX_GROUP_DETAILS where Tax_Group_Code='" + txtTaxGroup.Value + "' and Tax_Group_Type='H'  order by Trans_Code"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For ii As Integer = 0 To dt.Rows.Count - 1
                gv.Rows(RowIndex).Cells("ColTAX" + clsCommon.myCstr(ii + 1)).Value = clsCommon.myCstr(dt.Rows(ii)("tax_code"))
            Next
        End If
    End Sub

End Class

