'--11/10/2013--form Add By- Pradeep Sharma ---------
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine

Public Class frmFormulaSelection
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()

#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
    Public OldFormula As String
    Public ListOperand As New List(Of String)
    Public ListOperandSe As New List(Of String)
#End Region

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If validate_Formula() Then
            Me.Close()
        End If
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        funReset()
    End Sub

    Sub funReset()
        ListOperand.Clear()
        For Each it As String In ListOperandSe
            ListOperand.Add(it)
        Next
        txtnum.Value = Nothing
        txtFormula.Text = ""
        CboOperand.DataSource = Nothing
        CboOperand.DataSource = ListOperand
        CboOperand.SelectedIndex = -1
        CboOperand.Enabled = True
        btnItemAdd.Enabled = True
        btnAddNum.Enabled = True
        btnMin.Enabled = False
        btndiv.Enabled = False
        btnMul.Enabled = False
        btnPul.Enabled = False
        btnPer.Enabled = False
        btnOpBr.Enabled = True
        btnClBr.Enabled = False
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        funClose()
    End Sub

    Sub funClose()
        txtFormula.Text = OldFormula
        Me.Close()
    End Sub

    Private Sub frmFormulaSelection_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtFormula.Text = OldFormula
        For Each it As String In ListOperand
            If txtFormula.Text.Contains(it) Then
            Else
                ListOperandSe.Add(it)
            End If

        Next
        txtnum.Value = Nothing


        CboOperand.DataSource = ListOperandSe
        CboOperand.SelectedIndex = -1
        btnItemAdd.Enabled = True
        btnAddNum.Enabled = True
        btnMin.Enabled = False
        btndiv.Enabled = False
        btnMul.Enabled = False
        btnPul.Enabled = False
        btnPer.Enabled = False
        btnOpBr.Enabled = True
        btnClBr.Enabled = False
    End Sub

    Private Sub btnPul_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPul.Click
        txtFormula.Text = txtFormula.Text + " + "
        btnItemAdd.Enabled = True
        btnAddNum.Enabled = True
        btnMin.Enabled = False
        btnMul.Enabled = False
        btndiv.Enabled = False
        btnPul.Enabled = False
        btnPer.Enabled = False
        btnOpBr.Enabled = True
        btnClBr.Enabled = False

    End Sub

    Private Sub btnMin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMin.Click
        txtFormula.Text = txtFormula.Text + " - "
        btnItemAdd.Enabled = True
        btnAddNum.Enabled = True
        btnMin.Enabled = False
        btndiv.Enabled = False
        btnMul.Enabled = False
        btnPul.Enabled = False
        btnPer.Enabled = False
        btnOpBr.Enabled = True
        btnClBr.Enabled = False

    End Sub

    Private Sub btnMul_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMul.Click
        txtFormula.Text = txtFormula.Text + " * "
        btnItemAdd.Enabled = True
        btnAddNum.Enabled = True
        btnMin.Enabled = False
        btndiv.Enabled = False
        btnMul.Enabled = False
        btnPul.Enabled = False
        btnPer.Enabled = False
        btnOpBr.Enabled = True
        btnClBr.Enabled = False

    End Sub

    Private Sub btndiv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndiv.Click
        txtFormula.Text = txtFormula.Text + " / "
        btnItemAdd.Enabled = True
        btnAddNum.Enabled = True
        btnMin.Enabled = False
        btnMul.Enabled = False
        btndiv.Enabled = False
        btnPul.Enabled = False
        btnPer.Enabled = False
        btnOpBr.Enabled = True
        btnClBr.Enabled = False

    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnItemAdd.Click
        If ListOperand.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "No item remain to add.", Me.Text)
            btnItemAdd.Enabled = False
            Exit Sub
        End If
        If CboOperand.SelectedValue IsNot Nothing Or clsCommon.myLen(CboOperand.SelectedValue) > 0 Then
            txtFormula.Text = txtFormula.Text + "[" + CboOperand.SelectedValue + "]"
            btnItemAdd.Enabled = False
            btnAddNum.Enabled = False
            btnMin.Enabled = True
            btndiv.Enabled = True
            btnMul.Enabled = True
            btnPul.Enabled = True
            btnOpBr.Enabled = False
            btnClBr.Enabled = True

            ListOperand.Remove(clsCommon.myCstr(CboOperand.SelectedValue))
            CboOperand.DataSource = Nothing
            CboOperand.DataSource = ListOperand
            CboOperand.SelectedIndex = -1
        End If
        'If ListOperand.Count <= 0 Then
        '    clsCommon.MyMessageBoxShow("No item remain to add. Formula Completed.")
        '    btnItemAdd.Enabled = False
        '    btnAddNum.Enabled = True
        '    btnMin.Enabled = False
        '    btndiv.Enabled = False
        '    btnMul.Enabled = False
        '    btnPul.Enabled = False
        '    btnPer.Enabled = False

        'End If
    End Sub

    Private Sub btnPer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPer.Click
        txtFormula.Text = txtFormula.Text + " % "
        btnItemAdd.Enabled = True
        btnAddNum.Enabled = True
        btnMin.Enabled = False
        btnMul.Enabled = False
        btndiv.Enabled = False
        btnPul.Enabled = False
        btnPer.Enabled = False
        btnOpBr.Enabled = True
        btnClBr.Enabled = False

    End Sub

    Private Sub btnClBr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClBr.Click
        If txtFormula.Text.Contains("(") Then
            Dim BrCounter As Int16 = 0
            For Each ch As Char In txtFormula.Text
                If ch = "(" Then
                    BrCounter += 1
                End If
                If ch = ")" Then
                    BrCounter -= 1
                End If
            Next
            If BrCounter > 0 Then
                txtFormula.Text = txtFormula.Text + " ) "
                btnItemAdd.Enabled = False
                btnAddNum.Enabled = False
                btnMin.Enabled = True
                btnMul.Enabled = True
                btndiv.Enabled = True
                btnPul.Enabled = True
                btnPer.Enabled = True
            Else
                clsCommon.MyMessageBoxShow(Me, "NO Opening Brackets Found foe Closing. ", Me.Text)
            End If
        End If
    End Sub

    Private Sub btnAddNum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNum.Click
        txtFormula.Text = txtFormula.Text + " " + clsCommon.myCstr(txtnum.Value) + " "
        btnItemAdd.Enabled = False
        btnAddNum.Enabled = False
        btnMin.Enabled = True
        btndiv.Enabled = True
        btnMul.Enabled = True
        btnPul.Enabled = True
        btnPer.Enabled = True
        btnOpBr.Enabled = False
        btnClBr.Enabled = True
    End Sub

    Private Sub btnValidate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnValidate.Click
        If validate_Formula() Then
            clsCommon.MyMessageBoxShow(Me, " Syntax of Formula is correct. ", Me.Text)
        End If
    End Sub

    Function validate_Formula() As Boolean
        Dim str As String = txtFormula.Text
        Dim BrCounter As Int16 = 0
        If str.Contains(")") Or str.Contains("(") Then
            For Each ch As Char In str
                If ch = "(" Then
                    BrCounter += 1
                End If

                If ch = ")" Then
                    BrCounter -= 1
                End If
            Next
        End If
        If BrCounter <> 0 Then
            clsCommon.MyMessageBoxShow(Me, " Foemula is not currect Please Check For open or close brackets. ", Me.Text)
            Return False
        End If
        If str.EndsWith("+ ") Or str.EndsWith("- ") Or str.EndsWith("/ ") Or str.EndsWith("* ") Or str.EndsWith("( ") Then
            clsCommon.MyMessageBoxShow(Me, "Last part of Formula is not Currect. It contain incurrect syntext with : '" + str.Substring(str.Length - 3, 2) + "'. ")
            Return False
        End If
        Return True
    End Function

    Private Sub btnOpBr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpBr.Click
        txtFormula.Text = txtFormula.Text + " ( "
        btnItemAdd.Enabled = True
        btnAddNum.Enabled = True
        btnMin.Enabled = False
        btnMul.Enabled = False
        btndiv.Enabled = False
        btnPul.Enabled = False
        btnPer.Enabled = False
    End Sub
End Class
