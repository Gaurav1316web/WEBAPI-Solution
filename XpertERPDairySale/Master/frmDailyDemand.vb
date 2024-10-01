Imports common
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Public Class frmDailyDemand
    Inherits FrmMainTranScreen
    Dim isNewEntry As Boolean = False

    Private Sub frmDailyDemand_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UnionName()
        AddNew()
        txtDocumentDate.Value = clsCommon.GETSERVERDATE()

        'UsLock1.Status = ERPTransactionStatus.Pending
        ' UnionName()
        'If Not objCommonVar.RCDFCFP Then
        '    UnionName()
        'End If

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try

            'If (AllowToSave()) Then
            Dim obj As New clsDailyDemand()
            obj.Code = txtCode.Value
            obj.Start_Date = txtStartDate.Value
            obj.End_Date = txtEndDate.Value
            ' obj.Qty = clsCommon.myCdbl(txtQuantity1.Text)
            obj.Qty = clsCommon.myCdbl(txtqty.Text)
            'obj.Qty = clsCommon.myCdbl(txtQtys.Text)
            obj.Union_Name = txtUnionName1.Value
            obj.Document_Date = txtDocumentDate.Value

            If clsCommon.myLen(obj.Code) <= 0 Then
                isNewEntry = True
            End If
            If (obj.SaveData(obj, isNewEntry)) Then
                clsCommon.MyMessageBoxShow(Me, "Data save successfully.", Me.Text)
                LoadData(obj.Code, NavigatorType.Current)
            End If
            'End If
            isNewEntry = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try

    End Sub

    'Private Sub txtUnion__My_Click(sender As Object, e As EventArgs)
    '    UnionName()
    'End Sub
    Public Sub UnionName()
        If objCommonVar.RCDFCFP Then
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                Exit Sub
            End If
            Dim qry As String = ""
            qry = "SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE Union_Report=1 ORDER BY [TSPL_APP_LOCATION].Location_Name"

            'txtUnion.Value = clsCommon.ShowMultipleSelectForm("JanAStatus", qry, "DataBase_Name", "Location_Name", txtUnion.Value, tx.arrDispalyMember)
            txtUnionName1.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + txtUnionName1.Value + "' ")

        Else
            'Dim DatabaseName As New ArrayList()
            'DatabaseName.Add(objCommonVar.CurrComp_Code1)
            txtUnionName1.Value = clsCommon.myCstr(objCommonVar.CurrComp_Code1)
        End If
    End Sub
    'Public Sub UnionName()
    '    Try
    '        If objCommonVar.RCDFCFP Then
    '            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
    '            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
    '                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
    '                Exit Sub
    '            End If
    '            Dim qry As String = ""
    '            qry = "SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE Union_Report=1 ORDER BY [TSPL_APP_LOCATION].Location_Name"

    '            txtUnion.arrValueMember = clsCommon.ShowMultipleSelectForm("JanAStatus", qry, "DataBase_Name", "Location_Name", txtUnion.arrValueMember, txtUnion.arrDispalyMember)

    '        Else
    '            Dim DatabaseName As New ArrayList()
    '            DatabaseName.Add(objCommonVar.CurrComp_Code1)
    '            txtUnion.arrValueMember = DatabaseName
    '        End If

    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '    End Try
    'End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try

            txtCode.MyReadOnly = True
            Dim obj As New clsDailyDemand()
            obj = clsDailyDemand.GetData(strCode, NavType, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
                isNewEntry = False
                txtCode.Value = obj.Code
                'txtEndDate.Value = obj.End_Date
                txtStartDate.Value = obj.Start_Date
                txtDocumentDate.Value = obj.Document_Date
                ' txtUnion.arrValueMember = clsCommon.GetMulcallString(obj.Union_Name)
                txtqty.Text = obj.Qty
                txtUnionName1.Value = obj.Union_Name
                If clsCommon.myLen(obj.End_Date) > 0 Then
                    txtEndDate.Value = obj.End_Date
                Else
                    txtEndDate.Value = Nothing
                End If

                Dim sno As Integer = 1

                If clsCommon.myCdbl(ERPTransactionStatus.Approved) = clsCommon.myCdbl(obj.Status) Then
                    UsLock1.Status = obj.Status
                    btnSave.Enabled = False
                    btnDelete.Enabled = False
                    btnpost.Enabled = False
                ElseIf ERPTransactionStatus.Pending = obj.Status Then
                    UsLock1.Status = obj.Status
                    btnSave.Enabled = True
                    btnSave.Text = "Update"
                    btnDelete.Enabled = True
                    btnpost.Enabled = True
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating

        Dim qst As String = "select count(*) from Tspl_Daily_Demand_Master where Code='" + txtCode.Value + "'"
        Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
        If count = 0 Then
            txtCode.MyReadOnly = False
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            Dim whrClas As String = ""
            Dim qry As String = "select Code,Start_Date As 'Start Date',End_Date As 'End Date',Qty,Union_Name as 'Union Name',Document_Date as 'Document Date' from Tspl_Daily_Demand_Master"
            txtCode.Value = clsCommon.ShowSelectForm("DRT", qry, "Code", "", txtCode.Value, "Tspl_Daily_Demand_Master.Code ", isButtonClicked)
            LoadData(txtCode.Value, NavigatorType.Current)
        End If
    End Sub

    Private Sub txtUnion__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)
        UnionName()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Public Sub AddNew()
        UsLock1.Status = ERPTransactionStatus.Pending
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtStartDate.Value = clsCommon.GETSERVERDATE()
        txtEndDate.Value = txtStartDate.Value
        txtCode.Focus()
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = True
        isNewEntry = True
        txtqty.Text = ""
        txtUnionName1.Text = ""
        txtUnionName = Nothing
        'txtQuantity1 = Nothing
    End Sub
    Sub DeleteData()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If (clsDailyDemand.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        AddNew()
    End Sub

    Private Sub btnpost_Click(sender As Object, e As EventArgs) Handles btnpost.Click
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
                clsDailyDemand.PostData(clsCommon.myCstr(txtCode.Value))
                clsCommon.MyMessageBoxShow(Me, "Data posted successfully", Me.Text)
                LoadData(clsCommon.myCstr(txtCode.Value), NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtCode._MYNavigator
        Try
            Dim qry As String = "select count(*) from Tspl_Daily_Demand_Master where Code='" + txtCode.Value + "'"
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

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class