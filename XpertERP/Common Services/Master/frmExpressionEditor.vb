Imports common
Imports System.Reflection
Imports System.Data.SqlClient


Public Class frmExpressionEditor
    Inherits FrmMainTranScreen

    Public ProgCode As String = String.Empty
    Public expression As String = String.Empty

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        expression = txtExpression.Text
        Me.Close()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub btnAddToExpressionTo_Click(sender As Object, e As EventArgs) Handles btnAddToExpressionTo.Click
        If clsCommon.myLen(txtOperatorsLiterals.Text) > 0 Then
            txtExpression.AppendText(txtOperatorsLiterals.Text)
            txtOperatorsLiterals.Text = ""
        End If
    End Sub

    Private Sub btnAddToExpression1_Click(sender As Object, e As EventArgs) Handles btnAddToExpression1.Click
        If clsCommon.myLen(fndFields.Value) > 0 Then
            txtExpression.AppendText("#" & fndFields.Value & "#")
        End If
        fndFields.Value = ""
    End Sub

    Private Sub fndFields__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndFields._MYValidating
        Dim qry As String = ""
        Dim whrcls As String = ""
        If clsCommon.myCdbl(Me.Tag) = 1 Then
            qry = "select isnull(TSPL_SCREEN_Grid_CONTROL_MASTER.ColumnDescription,'') as FieldName, case when left(ColumnName,2)='CF' then 'Custom Field' else 'Existing Field' end as [Type of Field]   from TSPL_SCREEN_Grid_CONTROL_MASTER  "
            whrcls = "ProgramCode='" & ProgCode & "'"
        Else
            qry = "select * from (select isnull(TSPL_CUSTOM_FIELD_HEAD.FieldName,'') as FieldName   from TSPL_CUSTOM_FIELD_MAPPING  left outer join TSPL_CUSTOM_FIELD_HEAD on TSPL_CUSTOM_FIELD_HEAD.Code=TSPL_CUSTOM_FIELD_MAPPING.Custom_Field_Code where  Program_code='" & ProgCode & "' and isnull(TSPL_CUSTOM_FIELD_HEAD.FieldName,'')<>'' and isnull(TSPL_CUSTOM_FIELD_HEAD.type,0)=1 union all select description from TSPL_SCREEN_CONTROL_MASTER where ProgramCode='" & ProgCode & "' and isnull(Description,'')<>'' and ControlType='MyNumbox')xx"
        End If

        fndFields.Value = clsCommon.myCstr(clsCommon.ShowSelectForm("rptFieldFinder", qry, "FieldName", whrcls, fndFields.Value, "", isButtonClicked))
    End Sub

    Private Sub btnClearExpression_Click(sender As Object, e As EventArgs) Handles btnClearExpression.Click
        txtExpression.Text = ""
    End Sub

    Private Sub frmExpressionEditor_Load(sender As Object, e As EventArgs) Handles Me.Load
        fndFields.Value = ""
        txtOperatorsLiterals.Text = ""
        txtExpression.Text = clsCommon.myCstr(expression)
    End Sub

    Private Sub fndFields_Load(sender As Object, e As EventArgs) Handles fndFields.Load

    End Sub
End Class