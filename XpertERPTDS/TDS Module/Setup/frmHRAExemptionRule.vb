' ------------------------- Created By Preeti Gupta  --------------------'
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports System.IO
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Enumerations
Public Class FrmHRAExemptionRule
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim userCode, companyCode As String
#Region "Variable"
    Private isNewEntry As Boolean = False
    Const colHRA_CODE As String = "HRA CODE"
    Const colLineNo As String = "LineNo"
    Const colPayHeadCode As String = "PayHeadCode"
    Const colPayHeadName As String = "PayHeadName"
    Const colPayHeadType As String = "PayHeadType"
    Const colPayHeadCategory As String = "PayHeadCategory"
    Const colCalculationBasis As String = "CalculationBasis"
    Const colFormula As String = "Formula"

    Dim Obj As New ClsHRAExemptionRule()
    Dim ObjList As List(Of ClsHRAExemptionRule)
#End Region

#Region "Functions"
    Public Function SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmHRAExemptionRule)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow(Me, "Permission Denied", Me.Text)
            Me.Close()
            Return False
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        RadMenuItem3.Enabled = MyBase.isModifyFlag
        Return True
    End Function
    Function AllowToSave() As Boolean
        Dim Attachment As Integer
        Dim Rows As Integer = 0
        Attachment = clsDBFuncationality.getSingleValue("Select Count(*) From TSPL_ATTACHMENTS WHERE FormId ='" & MyBase.Form_ID & "' AND TransactionId ='" & txtCode.Value & "'")

        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Code can not be left blank", Me.Text)
            txtcode.Focus()
            Return False
        ElseIf clsCommon.myLen(txtSalStructure.Value) <= 0 Then
            myMessages.blankValue("Salary Structure Code")
            txtSalStructure.Focus()
            Return False
        ElseIf clsCommon.myLen(txtFormula.Text) <= 0 Then
            myMessages.blankValue("Formula")
            txtFormula.Focus()
            Return False
        End If
        Return True
    End Function
    Sub funReset()
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        TxtParticulars.Text = ""
        txtFormula.Text = ""
        txtSalStructure.Value = ""
        Me.gv.Rows.Clear()
        Me.gv.Rows.AddNew()

        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = False
    End Sub
    Sub LoadLocation()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr("Code") = "M"
        dr("Name") = "Metro"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "N"
        dr("Name") = "Non-Metro"
        dt.Rows.Add(dr)

        cmbLocation.DataSource = dt
        cmbLocation.ValueMember = "Code"
        cmbLocation.DisplayMember = "Name"
    End Sub
    Sub LoadGridColumns()
        gv.DataSource = Nothing
        gv.Rows.Clear()
        gv.Columns.Clear()
        gv.Enabled = True

        Dim lineNo As GridViewTextBoxColumn
        gv.ReadOnly = False
        lineNo = New GridViewTextBoxColumn()
        lineNo.FormatString = ""
        lineNo.HeaderText = "Line No"
        lineNo.Name = colLineNo
        lineNo.Width = 50
        lineNo.ReadOnly = True
        lineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.Columns.Add(lineNo)

        Dim PayHeadCode As GridViewTextBoxColumn
        PayHeadCode = New GridViewTextBoxColumn()
        PayHeadCode.FormatString = ""
        PayHeadCode.HeaderText = "Pay Head Code"
        PayHeadCode.Name = colPayHeadCode
        PayHeadCode.Width = 80
        PayHeadCode.ReadOnly = True
        PayHeadCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.Columns.Add(PayHeadCode)

        Dim PayHeadName As GridViewTextBoxColumn
        PayHeadName = New GridViewTextBoxColumn()
        PayHeadName.FormatString = ""
        PayHeadName.HeaderText = "Pay Head Name"
        PayHeadName.Name = colPayHeadName
        PayHeadName.Width = 100
        PayHeadName.ReadOnly = True
        PayHeadName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv.Columns.Add(PayHeadName)

        Dim PayHeadType As GridViewTextBoxColumn
        PayHeadType = New GridViewTextBoxColumn()
        PayHeadType.FormatString = ""
        PayHeadType.HeaderText = "Pay Head Type"
        PayHeadType.Name = colPayHeadType
        PayHeadType.Width = 80
        PayHeadType.ReadOnly = True
        PayHeadType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv.Columns.Add(PayHeadType)

        Dim PayHeadCategory As GridViewTextBoxColumn
        PayHeadCategory = New GridViewTextBoxColumn()
        PayHeadCategory.FormatString = ""
        PayHeadCategory.HeaderText = "Pay Head Category"
        PayHeadCategory.Name = colPayHeadCategory
        PayHeadCategory.Width = 100
        PayHeadCategory.ReadOnly = True
        PayHeadCategory.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv.Columns.Add(PayHeadCategory)

        Dim CalculationBasis As GridViewTextBoxColumn
        CalculationBasis = New GridViewTextBoxColumn()
        CalculationBasis.FormatString = ""
        CalculationBasis.HeaderText = "Calculation Basis"
        CalculationBasis.Name = colCalculationBasis
        CalculationBasis.Width = 100
        CalculationBasis.ReadOnly = True
        CalculationBasis.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv.Columns.Add(CalculationBasis)

        Dim Formula As GridViewTextBoxColumn
        Formula = New GridViewTextBoxColumn()
        Formula.FormatString = ""
        Formula.HeaderText = "Formula"
        Formula.Name = colFormula
        Formula.Width = 100
        Formula.ReadOnly = True
        Formula.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv.Columns.Add(Formula)

    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType, ByVal SalStruCode As String)
        'txtCode.MyReadOnly = True
        btnsave.Enabled = True
        'btndelete.Enabled = True

        Obj = ClsHRAExemptionRule.GetData(strCode, NavTyep)
        If (Obj IsNot Nothing AndAlso clsCommon.myLen(Obj.HRA_Code) > 0) Then
            funReset()
            isNewEntry = False
            btndelete.Enabled = True
            btnsave.Text = "Update"
            btndelete.Enabled = True
            Dim ii As Int16 = 0
            LoadGridColumns()
            txtCode.Value = Obj.HRA_Code
            TxtParticulars.Text = Obj.Particulars
            txtSalStructure.Value = Obj.SALARY_STRUCTURE_CODE
            txtFormula.Text = Obj.Formula
            cmbLocation.SelectedValue = Obj.Location_City

            Dim arr As New List(Of ClsHRAExemptionRule)
            arr = ClsHRAExemptionRule.GetPayHeadData(Obj.SALARY_STRUCTURE_CODE)

            For Each obj As ClsHRAExemptionRule In arr
                gv.Rows.AddNew()
                gv.Rows(gv.Rows.Count - 1).Cells(colLineNo).Value = obj.LINE_NO
                gv.Rows(gv.Rows.Count - 1).Cells(colPayHeadCode).Value = obj.PAY_HEAD_CODE
                gv.Rows(gv.Rows.Count - 1).Cells(colPayHeadName).Value = obj.PAY_HEAD_NAME
                gv.Rows(gv.Rows.Count - 1).Cells(colPayHeadType).Value = obj.HEAD_TYPE
                gv.Rows(gv.Rows.Count - 1).Cells(colPayHeadCategory).Value = obj.SUB_HEAD_TYPE
                gv.Rows(gv.Rows.Count - 1).Cells(colCalculationBasis).Value = obj.CALC_BASIS
                gv.Rows(gv.Rows.Count - 1).Cells(colFormula).Value = obj.PAYHEAD_FORMULA
            Next
        End If
    End Sub
    Private Sub SaveData()
        Try
            If AllowToSave() Then
                Dim obj As New ClsHRAExemptionRule()
                obj.HRA_Code = txtcode.Value
                obj.Particulars = TxtParticulars.Text
                obj.Location_City = cmbLocation.SelectedValue
                obj.Formula = txtFormula.Text
                obj.SALARY_STRUCTURE_CODE = txtSalStructure.Value
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                If (obj.SaveData(obj, isNewEntry, trans)) Then
                    trans.Commit()
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(obj.HRA_Code, NavigatorType.Current, obj.SALARY_STRUCTURE_CODE)
                    btnsave.Text = "Update"
                    btndelete.Enabled = True
                Else
                    btnsave.Text = "Save"
                    btndelete.Enabled = False
                    trans.Rollback()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Code not found to delete", Me.Text)
            Exit Sub
        End If
        funDelete()
    End Sub
    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (ClsHRAExemptionRule.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
#End Region

    Private Sub FrmHRAExemptionRule_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub FrmHRAExemptionRule_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

    End Sub

    Private Sub txtSalStructure__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtSalStructure._MYValidating

    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click

    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click

    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click

    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click

    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click

    End Sub

    Private Sub gv_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv.CellDoubleClick

    End Sub
End Class
