
Imports common
Imports System.Data.SqlClient
Public Class frmScreenControlMappingMultiple
    Inherits FrmMainTranScreen
    Public formId As String = String.Empty
    Dim isCellValueChangedOpen As Boolean = True
    Private Sub frmScreenControlMappingMultiple_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If clsCommon.myLen(formId) > 0 Then
                isCellValueChangedOpen = True
                clsCreateAllTables.getControlsOnForm(formId, gv2)
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim trans As SqlTransaction = Nothing
        Try
            If gv2 Is Nothing OrElse gv2.Rows.Count <= 0 Then
                Throw New Exception(" No Record Found to Save")
            Else
                trans = clsDBFuncationality.GetTransactin()
                Dim qry As String = "delete from TSPL_SCREEN_CONTROL_MASTER where programCode='" & formId & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                For i As Integer = 0 To gv2.Rows.Count - 1
                    If clsCommon.myLen(gv2.Rows(i).Cells("Description").Value) > 0 Then
                        Dim cnt As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_SCREEN_CONTROL_MASTER where controlName<>'" & gv2.Rows(i).Cells("ControlName").Value & "' and ProgramCode<>'" & gv2.Rows(i).Cells("ScreenCode").Value & "' and Description='" & gv2.Rows(i).Cells("Description").Value & "'", trans))
                        If cnt > 0 Then
                            Throw New Exception(" Description  :" & gv2.Rows(i).Cells("Description").Value & "  Found Duplicate [Also been specified for other Control at same screen]")
                        End If
                    End If
                    qry = "insert into TSPL_SCREEN_CONTROL_MASTER(ProgramCode,ControlName,ControlType,Description,tableName,fieldName) values('" & gv2.Rows(i).Cells("ScreenCode").Value & "','" & gv2.Rows(i).Cells("ControlName").Value & "','" & gv2.Rows(i).Cells("ControlType").Value & "','" & gv2.Rows(i).Cells("Description").Value & "','" & gv2.Rows(i).Cells("TableName").Value & "','" & gv2.Rows(i).Cells("FieldName").Value & "')"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                Next
                trans.Commit()
                clsCommon.MyMessageBoxShow(Me, "Saved Successfully", Me.Text)
            End If


        Catch ex As Exception
            Try
                trans.Rollback()
            Catch ex1 As Exception
            End Try
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If gv2 IsNot Nothing AndAlso gv2.Rows.Count > 0 Then
                If clsCommon.MyMessageBoxShow(Me, "Do You Want to Delete ?", Me.Text, Windows.Forms.MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                    Dim qry As String = "delete from TSPL_SCREEN_CONTROL_MASTER where programCode='" & formId & "'"
                    clsDBFuncationality.ExecuteNonQuery(qry)
                    clsCommon.MyMessageBoxShow(Me, "deleted Successfully", Me.Text)
                    clsCreateAllTables.getControlsOnForm(formId, gv2)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv2_CellValueChanged(sender As Object, e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv2.CellValueChanged
        If Not isCellValueChangedOpen Then
            isCellValueChangedOpen = True
            If e.Column Is gv2.Columns("TableName") Then
                Dim qry As String = "SELECT  UPPER (TABLE_NAME ) as TABLE_NAME FROM INFORMATION_SCHEMA.TABLES  "
                gv2.Rows(e.RowIndex).Cells("TableName").Value = clsCommon.ShowSelectForm("TableList", qry, "TABLE_NAME", "TABLE_TYPE='BASE TABLE'", gv2.Rows(e.RowIndex).Cells("TableName").Value, "", False)
            ElseIf e.Column Is gv2.Columns("FieldName") Then
                If clsCommon.myLen(gv2.Rows(e.RowIndex).Cells("TableName").Value) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Please Select Table First", Me.Text)
                    Exit Sub
                End If
                Dim qry As String = "select  upper(sys.columns.Name) as FieldName from sys.columns inner join sys.tables on sys.tables.object_id=sys.columns.object_id   "
                gv2.Rows(e.RowIndex).Cells("FieldName").Value = clsCommon.ShowSelectForm("FiledList", qry, "FieldName", "sys.tables.name='" & gv2.Rows(e.RowIndex).Cells("TableName").Value & "'", gv2.Rows(e.RowIndex).Cells("FieldName").Value, "", False)
            End If

        End If
        isCellValueChangedOpen = False
    End Sub
End Class