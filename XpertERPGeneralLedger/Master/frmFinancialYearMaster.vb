Imports common
Imports System.Data.SqlClient

Public Class frmFinancialYearMaster
    Inherits FrmMainTranScreen
#Region "Variables"
    Public TransactionNo As String
    Private isNewEntry As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
#End Region

    Private Sub frmProjectMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        If clsCommon.myLen(TransactionNo) > 0 Then
            LoadData(TransactionNo, NavigatorType.Current)
        Else
            AddNew()
        End If
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FiscalYear)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow(Me, "Permission Denied", Me.Text)
            Me.Close()
            Exit Sub
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_Fiscal_Year_Master where Fiscal_Code='" + txtCode.Value + "' "
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
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

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        AddNew()
        Dim obj As clsFiscalYear = clsFiscalYear.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            isNewEntry = False
            txtCode.Value = obj.Fiscal_Code
            txtName.Text = obj.Fiscal_Name
            txtStartDate.Value = obj.Start_Date
            txtEndDate.Value = obj.End_Date
            '' Anubhooti 09-Sep-2014 BM00000003735
            If obj.Is_Current_Year = 1 Then
                ChkCurrFin.Checked = True
            Else
                ChkCurrFin.Checked = False
            End If
        End If
    End Sub

    Private Sub AddNew()
        txtCode.Value = ""
        txtName.Text = ""
        txtStartDate.Value = clsCommon.GETSERVERDATE()
        txtEndDate.Value = txtStartDate.Value

        isNewEntry = True
        btnSave.Enabled = True
        btnDelete.Enabled = True
        txtCode.MyReadOnly = False
        '' Anubhooti 09-Sep-2014
        ChkCurrFin.Checked = False
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Sub SaveData()
        Try
            If AllowToSave() Then

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.FiscalYear, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
                Dim obj As New clsFiscalYear()
                obj.Fiscal_Code = txtCode.Value
                obj.Fiscal_Name = txtName.Text
                obj.Start_Date = txtStartDate.Value
                obj.End_Date = txtEndDate.Value
                '' Anubhooti 09-Sep-2014 BM00000003735
                If ChkCurrFin.Checked = True Then
                    obj.Is_Current_Year = 1
                Else
                    obj.Is_Current_Year = 0
                End If
                If (obj.SaveData(obj, isNewEntry)) Then
                    clsCommon.MyMessageBoxShow(Me, "Data saved Successfully", Me.Text)
                    LoadData(obj.Fiscal_Code, NavigatorType.Current)
                    objCommonVar.RefreshCommonVar()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function AllowToSave() As Boolean
        If clsCommon.myLen(clsCommon.myCstr(txtCode.Value)) <= 0 Then
            txtCode.Focus()
            Throw New Exception("Project Code cannot be blank")
        End If
        If clsCommon.myLen(clsCommon.myCstr(txtName.Text)) <= 0 Then
            txtName.Focus()
            Throw New Exception("Please Enter Name")
        End If
        Dim qry As String = "select Fiscal_Code,Fiscal_Name from TSPL_Fiscal_Year_Master where Fiscal_Code not in ('" + txtCode.Value + "')  and (('" + clsCommon.GetPrintDate(txtStartDate.Value, "dd/MMM/yyyy") + "'>=Start_Date and '" + clsCommon.GetPrintDate(txtStartDate.Value, "dd/MMM/yyyy") + "'<=End_Date) or ('" + clsCommon.GetPrintDate(txtEndDate.Value, "dd/MMM/yyyy") + "'>=Start_Date and '" + clsCommon.GetPrintDate(txtEndDate.Value, "dd/MMM/yyyy") + "'<=End_Date))"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            txtStartDate.Focus()
            Dim str As String = "Date range exists in the Follwoing Fiscal year"
            For Each dr As DataRow In dt.Rows
                str += clsCommon.myCstr(dr("Fiscal_Code")) + " (" + clsCommon.myCstr(dr("Fiscal_Name")) + ") "
            Next
            Throw New Exception("Please Enter Name")
        End If
        '' Anubhooti 09-Sep-2104 BM00000003735
        Dim qry1 As String = "select count(*) As Row from TSPL_Fiscal_Year_Master where Is_Current_Year=1 AND Fiscal_Code NOT IN ('" & clsCommon.myCstr(txtCode.Value) & "')"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry1)
        If check > 0 AndAlso ChkCurrFin.Checked = True Then
            ChkCurrFin.Focus()
            Throw New Exception("Please check ! not more than one financial year can be make as current year")
        End If
        Return True
    End Function

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Dim str As String = "select count(*) from TSPL_Fiscal_Year_Master where Fiscal_Code ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 Then
            txtCode.MyReadOnly = False
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = "select Fiscal_Code as Code,Fiscal_Name as Name from TSPL_Fiscal_Year_Master"
            Dim whrClas As String = " Comp_Code='" + objCommonVar.CurrentCompanyCode + "' "

            txtCode.Value = clsCommon.ShowSelectForm("TSPL_Fiscal_Year_Master", qry, "Code", whrClas, txtCode.Value, "", isButtonClicked)
            LoadData(txtCode.Value, NavigatorType.Current)
        End If


    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click

        Me.Close()

    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        DeleteData()

    End Sub

    Private Sub DeleteData()
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("Project Code not found to delete")

            End If
            If clsCommon.MyMessageBoxShow(Me, "Delete the Current Transaction." + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then

                Dim qry As String = "delete from TSPL_Fiscal_Year_Master where Fiscal_Code='" + txtCode.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qry)
                clsCommon.MyMessageBoxShow(Me, "Successfully Deleted", Me.Text)
                AddNew()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, "Current Code is in use")
        End Try
    End Sub

    Private Sub frmProjectMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.A Then
            AddNew()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub

    Private Sub txtStartDate_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtStartDate.Validating
        txtEndDate.Value = txtStartDate.Value.AddYears(1).AddDays(-1)
    End Sub
End Class
