Imports System.Data.SqlClient
Imports common
Public Class frmOfficeOrder
    Inherits FrmMainTranScreen

    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        Dim qst As String = "select count(*) from TSPL_OFFICE_ORDER where Document_No='" + txtCode.Value + "'"
        Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
        If count = 0 Then
            txtCode.MyReadOnly = False
        Else
            txtCode.MyReadOnly = True
        End If
        Dim whrClas As String = ""
        Dim qry As String = "select Document_No as Code from TSPL_OFFICE_ORDER"
        LoadData(clsCommon.ShowSelectForm("", qry, "Code", "", txtCode.Value, "TSPL_OFFICE_ORDER.Document_No  ", isButtonClicked), NavigatorType.Current)
    End Sub

    'Public Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
    '    Try
    '        Addnew()
    '        txtCode.MyReadOnly = True

    '        Dim obj As ClsOfficeOrderTemplate = ClsOfficeOrderTemplate.GetData(strCode, NavType, Nothing)

    '        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
    '            'isNewEntry = False
    '            txtCode.Value = obj.Code
    '            RichTextBox1.Rtf = obj.Subject
    '            txtDescription.Text = obj.Description
    '        Else
    '            Addnew()
    '        End If

    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '    End Try
    'End Sub

    Public Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try
            Addnew()
            txtCode.MyReadOnly = True

            Dim obj As ClsOfficeOrder =
            ClsOfficeOrder.GetData(strCode, NavType, Nothing)

            If obj Is Nothing OrElse clsCommon.myLen(obj.Code) = 0 Then
                Addnew()
                Exit Sub
            End If

            txtCode.Value = obj.Code
            txtDate.Value = obj.DocumentDate
            txtTemplate.Value = obj.Template
            If String.IsNullOrEmpty(obj.Template) Then
                RichTextBox1.Clear()
            ElseIf obj.Template.Trim().StartsWith("{\rtf") Then
                RichTextBox1.Rtf = obj.Subject  ' valid RTF
            Else
                RichTextBox1.Rtf = obj.Subject ' plain text
            End If

            txtDescription.Text = obj.Description
            'txtTemplate.Value = obj.Template
            If clsCommon.myLen(txtTemplate.Value) < 0 Then
                TemplateLable.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_OFFICE_ORDER_TEMPLATE where Document_No = '" & txtTemplate.Value & "'"))
                'RichTextBox1.Rtf = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Subject from TSPL_OFFICE_ORDER_TEMPLATE where Document_No = '" & txtTemplate.Value & "'"))
            End If
            RadGroupBox2.Text = obj.Print
            UsLock1.Text = obj.Status
            'txtCreatedBy.Text = obj.CreatedBy
            'txtModifiedBy.Text = obj.ModifiedBy
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub



    Private Sub txtCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtCode._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_OFFICE_ORDER where Document_No='" + txtCode.Value + "'"

            Dim count As Integer = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue(qry))
            If count = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If
            LoadData(clsCommon.myCstr(txtCode.Value), NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        Addnew()
    End Sub

    Public Sub Addnew()
        rbtnA4.IsChecked = True
        txtTemplate.Value = ""
        TemplateLable.Text = ""
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        RichTextBox1.Clear()
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtDescription.Text = ""
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = True
    End Sub

    Private Sub txtTemplate__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtTemplate._MYValidating
        Dim qry As String = "select Document_No as Code,Description as Name from TSPL_OFFICE_ORDER_TEMPLATE "
        txtTemplate.Value = clsCommon.ShowSelectForm("BILLTOxxCPO", qry, "Code", "", txtTemplate.Value, "Code", isButtonClicked)
        If clsCommon.myLen(txtTemplate.Value) > 0 Then
            TemplateLable.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_OFFICE_ORDER_TEMPLATE where Document_No = '" & txtTemplate.Value & "'"))
            RichTextBox1.Rtf = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Subject from TSPL_OFFICE_ORDER_TEMPLATE where Document_No = '" & txtTemplate.Value & "'"))
        End If
    End Sub

    Private Sub frmOfficeOrder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtDate.Value = clsCommon.GETSERVERDATE()
        rbtnA4.IsChecked = True
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Private Sub SaveData()
        Try
            'If clsCommon.myLen(txtCode.Value) <= 0 Then
            '    clsCommon.MyMessageBoxShow(Me, "Please enter document code First", Me.Text)
            '    Exit Sub

            'End If

            Dim obj As New ClsOfficeOrder()

            obj.Code = txtCode.Value
            obj.DocumentDate = txtDate.Value
            obj.TemplateFinder = txtTemplate.Value
            obj.Description = txtDescription.Text
            'obj.Print = RadGroupBox2.Text
            obj.Template = txtTemplate.Value
            obj.Subject = RichTextBox1.Rtf
            obj.Status = UsLock1.Text

            Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try
                ClsOfficeOrder.SaveData(obj, tran)
                tran.Commit()
                clsCommon.MyMessageBoxShow(Me, "Data saved successfully.", Me.Text)
                LoadData(obj.Code, NavigatorType.Current)
            Catch ex As Exception
                tran.Rollback()
                Throw
            End Try

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "No data found print.", Me.Text)
            Exit Sub
        Else
            LoadData()
        End If
    End Sub
    Private Sub LoadData()
        Try
            Dim qry As String = "select Subject from TSPL_OFFICE_ORDER_TEMPLATE where Document_No= '" & txtTemplate.Value & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                If rbtnA4.IsChecked Then
                    frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.PurchaseOrder, dt, "crptOfficeOrder", "")
                Else
                    frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.PurchaseOrder, dt, "crptOfficeOrderLegal", "")
                End If

            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to display", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        Try
            If (myMessages.deleteConfirm()) Then
                If (ClsOfficeOrderTemplate.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully", Me.Text)
                    Addnew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        If clsCommon.myLen(txtCode.Value) > 0 Then
            PostData(txtCode.Value)
        Else
        End If
    End Sub

    Sub PostData(ByVal strCode As String)
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("No document found to post")
            End If

            If clsCommon.MyMessageBoxShow(Me, "Post the Current Document [" + txtCode.Value + "]" + Environment.NewLine + "Are You Sure.", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                ClsOfficeOrder.PostData(clsCommon.myCstr(txtCode.Value))
                clsCommon.MyMessageBoxShow(Me, "Data posted successfully", Me.Text)
                LoadData(clsCommon.myCstr(txtCode.Value), NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class