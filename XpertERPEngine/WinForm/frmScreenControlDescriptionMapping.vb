Imports common
Imports System.Windows.Forms
Imports System.Data.SqlClient

Public Class frmScreenControlDescriptionMapping
    Inherits FrmMainTranScreen
    Public ctrl As Control = Nothing
    Public formId As String = String.Empty
    Public isNewEntry As Boolean = False
    Sub loadControlInfo()
        reset()
        clsCommon.MyMessageBoxShow("Hi")
        Dim qry As String = "select count(*) from TSPL_SCREEN_CONTROL_MASTER where programCode='" & formId & "' and ControlName='" & ctrl.Name & "'"
        Dim cnt As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
        If cnt > 0 Then
            qry = "select TSPL_SCREEN_CONTROL_MASTER.*,TSPL_PROGRAM_MASTER.Program_Name  from TSPL_SCREEN_CONTROL_MASTER left outer join TSPL_PROGRAM_MASTER on TSPL_SCREEN_CONTROL_MASTER.programcode=TSPL_PROGRAM_MASTER.Program_Code where  TSPL_SCREEN_CONTROL_MASTER.programCode='" & formId & "' and TSPL_SCREEN_CONTROL_MASTER.ControlName='" & ctrl.Name & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                txtScreenCode.Text = clsCommon.myCstr(dt.Rows(0)("ProgramCode"))
                txtScreenDesc.Text = clsCommon.myCstr(dt.Rows(0)("Program_Name"))
                txtControlName.Text = clsCommon.myCstr(ctrl.Name)
                txtControlType.Text = clsCommon.myCstr(ctrl.GetType().Name)
                txtdesc.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
                fndReferenceTable.Value = clsCommon.myCstr(dt.Rows(0)("TableName"))
                fndFieldName.Value = clsCommon.myCstr(dt.Rows(0)("FieldName"))
                isNewEntry = False
            Else
                txtScreenCode.Text = clsCommon.myCstr(formId)
                txtScreenDesc.Text = clsCommon.myCstr(ProgramCodeNew.GetProgramName(formId))
                txtControlName.Text = clsCommon.myCstr(ctrl.Name)
                txtControlType.Text = clsCommon.myCstr(ctrl.GetType().Name)
                txtdesc.Text = ""
                fndReferenceTable.Value = ""
                fndFieldName.Value = ""
                isNewEntry = True
            End If
        Else
            txtScreenCode.Text = clsCommon.myCstr(formId)
            txtScreenDesc.Text = clsCommon.myCstr(ProgramCodeNew.GetProgramName(formId))
            txtControlName.Text = clsCommon.myCstr(ctrl.Name)
            txtControlType.Text = clsCommon.myCstr(ctrl.GetType().Name)
            txtdesc.Text = ""
            fndReferenceTable.Value = ""
            fndFieldName.Value = ""
            isNewEntry = True
        End If
    End Sub
    Sub reset()
        txtScreenCode.Text = ""
        txtScreenDesc.Text = ""
        txtControlName.Text = ""
        txtControlType.Text = ""
        txtdesc.Text = ""
        fndReferenceTable.Value = ""
        fndFieldName.Value = ""
    End Sub
    Private Sub frmScreenControlDescriptionMapping_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If ctrl IsNot Nothing AndAlso clsCommon.myLen(formId) > 0 AndAlso clsCommon.myLen(ctrl.Name) > 0 Then
                loadControlInfo()
            Else
                Throw New Exception("Not a Valid control to set description")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Me.Close()
            GC.Collect()
            GC.WaitForPendingFinalizers()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If clsCommon.myLen(txtScreenCode.Text) > 0 AndAlso clsCommon.myLen(txtControlName.Text) > 0 Then
                If clsCommon.MyMessageBoxShow("Do You Want To Delete ?", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                    Dim qry As String = "delete from TSPL_SCREEN_CONTROL_MASTER where ProgramCode='" & txtScreenCode.Text & "' and controlName='" & txtControlName.Text & "'"
                    clsDBFuncationality.ExecuteNonQuery(qry)
                    clsCommon.MyMessageBoxShow(Me, "Deleted Successfully", Me.Text)
                End If
            Else
                Throw New Exception("Screen Code and/or control Name found Blank, Unable to delete")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If clsCommon.myLen(txtScreenCode.Text) <= 0 Then
                Throw New Exception("Screen Code Found Blank")
            End If
            If clsCommon.myLen(txtControlName.Text) <= 0 Then
                Throw New Exception("Control Name Found Blank")
            End If
            If clsCommon.myLen(txtdesc.Text) <= 0 Then
                Throw New Exception("Description Found Blank")
            End If
            Dim qry As String = ""
            If txtdesc.Text.Contains(" ") Then
                Throw New Exception("Description must not have any blank space")
            End If

            If txtdesc.Text.Contains(".") OrElse txtdesc.Text.Contains(",") OrElse txtdesc.Text.Contains(":") OrElse txtdesc.Text.Contains(";") OrElse txtdesc.Text.Contains("'") OrElse txtdesc.Text.Contains("""") OrElse txtdesc.Text.Contains("]") OrElse txtdesc.Text.Contains("[") OrElse txtdesc.Text.Contains("]") OrElse txtdesc.Text.Contains("{") OrElse txtdesc.Text.Contains("}") OrElse txtdesc.Text.Contains("(") OrElse txtdesc.Text.Contains(")") OrElse txtdesc.Text.Contains("-") OrElse txtdesc.Text.Contains("+") OrElse txtdesc.Text.Contains("=") OrElse txtdesc.Text.Contains(">") OrElse txtdesc.Text.Contains("<") OrElse txtdesc.Text.Contains("/") OrElse txtdesc.Text.Contains("\") OrElse txtdesc.Text.Contains("|") OrElse txtdesc.Text.Contains("?") OrElse txtdesc.Text.Contains("*") OrElse txtdesc.Text.Contains("&") OrElse txtdesc.Text.Contains("^") OrElse txtdesc.Text.Contains("%") OrElse txtdesc.Text.Contains("$") OrElse txtdesc.Text.Contains("#") OrElse txtdesc.Text.Contains("@") OrElse txtdesc.Text.Contains("!") OrElse txtdesc.Text.Contains("~") OrElse txtdesc.Text.Contains("`") Then
                Throw New Exception("Description must not have following Symbols , . : ; ' "" ) ( { } [ ] | \ / ? > < * & ^ % $ # @ ! ~ ` = + -")
            End If

            Dim cnt As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_SCREEN_CONTROL_MASTER where controlName<>'" & txtControlName.Text & "' and ProgramCode<>'" & txtScreenCode.Text & "' and Description='" & txtdesc.Text & "'"))
            If cnt > 0 Then
                Throw New Exception(" Description  :" & txtdesc.Text & "  Found Duplicate [Also been specified for other Control at same screen]")
            End If
            If isNewEntry Then
                qry = "insert into TSPL_SCREEN_CONTROL_MASTER(ProgramCode,ControlName,ControlType,Description,tableName,fieldName) values('" & txtScreenCode.Text & "','" & txtControlName.Text & "','" & txtControlType.Text & "','" & txtdesc.Text & "','" & fndReferenceTable.Value & "','" & fndFieldName.Value & "')"

            Else
                qry = "update TSPL_SCREEN_CONTROL_MASTER set ProgramCode'" & txtScreenCode.Text & "',ControlName='" & txtControlName.Text & "',ControlType='" & txtControlType.Text & "',Description='" & txtdesc.Text & "',TableName='" & fndReferenceTable.Value & "',FieldName='" & fndFieldName.Value & "' where programCode='" & txtScreenCode.Text & "' and ControlName='" & txtControlName.Text & "' "
            End If

            clsDBFuncationality.ExecuteNonQuery(qry)
            clsCommon.MyMessageBoxShow(Me, "Saved Successfully", Me.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndReferenceTable__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndReferenceTable._MYValidating
        Dim qry As String = "SELECT  UPPER (TABLE_NAME ) as TABLE_NAME FROM INFORMATION_SCHEMA.TABLES  "
        fndReferenceTable.Value = clsCommon.ShowSelectForm("TableList", qry, "TABLE_NAME", "TABLE_TYPE='BASE TABLE'", fndReferenceTable.Value, "", isButtonClicked)
    End Sub

    Private Sub fndFieldName__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndFieldName._MYValidating
        If clsCommon.myLen(fndReferenceTable.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Select Table First", Me.Text)
            Exit Sub
        End If
        Dim qry As String = "select  upper(sys.columns.Name) as FieldName from sys.columns inner join sys.tables on sys.tables.object_id=sys.columns.object_id   "
        fndFieldName.Value = clsCommon.ShowSelectForm("FiledList", qry, "FieldName", "sys.tables.name='" & fndReferenceTable.Value & "'", fndFieldName.Value, "", isButtonClicked)
    End Sub
End Class