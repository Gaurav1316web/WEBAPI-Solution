Imports common
Imports System.Data.SqlClient

Public Class frmCustomerDeduction
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim SettNoOFCustomerForImportExport As Integer
    Private isLoadData As Boolean = False
#End Region

    Private Sub FrmAccountMainGroup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SettNoOFCustomerForImportExport = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.NoOFCustomerForImportExport, clsFixedParameterCode.NoOFCustomerForImportExport, Nothing))
        SetUserMgmtNew()
        funReset()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Transaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D for Delete Transaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N for New Transaction")
    End Sub

    Private Sub fndaccgp__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndaccgp__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCode._MYValidating
        Dim str As String = "select count(*) from TSPL_CUSTOMER_DEDUCTION_CUSTOMER where Deduction_Code ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            txtCode.Value = clsCustomerDeductionHead.getFinder("", txtCode.Value, isButtonClicked)
            If txtCode.Value <> "" Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                funReset()
            End If
        End If
    End Sub

    Private Sub FrmAccountMainGroup_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            PostData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.AccountSubGroup)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        btnPost.Visible = MyBase.isPostFlag
        If btnsave.Visible = True Then
            rmExport.Enabled = True
            rmImport.Enabled = True
        Else
            rmExport.Enabled = False
            rmImport.Enabled = False
        End If
    End Sub

    Sub funReset()
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        txtDescription.Text = ""
        txtAmount.Value = 0
        txtCustomer.arrValueMember = Nothing
        btnsave.Text = "Save"
        txtDate.Value = clsCommon.GETSERVERDATE()
        btnsave.Enabled = True
        btndelete.Enabled = False
        btnPost.Enabled = False
        UsLock1.Status = ERPTransactionStatus.Pending
        chkInactive.Enabled = False
        chkInactive.Checked = False
        isLoadData = False
    End Sub

    Private Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Function AllowToSave() As Boolean
        If clsCommon.myLen(txtDescription.Text) <= 0 Then
            Throw New Exception("Please enter Description")
        ElseIf txtAmount.Value <= 0 Then
            Throw New Exception("Amount should be greater then zero")
        ElseIf txtCustomer.arrValueMember Is Nothing OrElse txtCustomer.arrValueMember.Count <= 0 Then
            Throw New Exception("Please select at lease one customer")
        End If
        Return True
    End Function

    Public Sub SaveData()
        Try
            If AllowToSave() Then
                Dim obj As New clsCustomerDeductionHead()
                obj.Deduction_Code = txtCode.Value
                obj.Deduction_Valid_Till = txtDate.Value
                obj.Deduction_Name = txtDescription.Text
                obj.Deduction_Amount = txtAmount.Value
                obj.arr = txtCustomer.arrValueMember
                If (clsCustomerDeductionHead.SaveData(obj, isNewEntry)) Then
                    clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    LoadData(obj.Deduction_Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As New clsCustomerDeductionHead()
        obj = clsCustomerDeductionHead.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Deduction_Code) > 0) Then
            funReset()
            isLoadData = True
            isNewEntry = False
            btnsave.Text = "Update"
            txtCode.Value = obj.Deduction_Code
            txtDate.Value = obj.Deduction_Valid_Till
            txtDescription.Text = obj.Deduction_Name
            txtAmount.Value = obj.Deduction_Amount
            txtCustomer.arrValueMember = obj.arr
            UsLock1.Status = obj.Posted
            chkInactive.Checked = obj.Inactive_Status
            If obj.Posted = ERPTransactionStatus.Approved Then
                btnsave.Enabled = False
                btndelete.Enabled = False
                btnPost.Enabled = False
                chkInactive.Enabled = Not obj.Inactive_Status
                If chkInactive.Enabled Then
                    chkInactive.Enabled = MyBase.isPostFlag
                End If
            Else
                btnsave.Enabled = True
                btndelete.Enabled = True
                btnPost.Enabled = True
            End If
            txtCode.MyReadOnly = True
            isLoadData = False
        End If
    End Sub

    Sub funClose()
        Me.Close()
        GC.Collect()
    End Sub

    Private Sub rmExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rmExport.Click
        Dim str As String
        str = "select '' as [Valid Till(dd/MMM/yyyy)],'' as [Description],0 AS [Amount] "
        For ii As Integer = 1 To SettNoOFCustomerForImportExport
            str += ",'' as [Customer " + clsCommon.myCstr(ii) + "]"
        Next
        ListImpExpColumnsMandatory = New List(Of String)({"Amount"})
        transportSql.ExporttoExcel(str, "", "", Me, ListImpExpColumnsMandatory)
    End Sub

    Private Sub rmImport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rmImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Try
            Dim currentdate As Date = Date.Today
            Dim linno As Integer = 0
            Dim Strs As List(Of String) = New List(Of String)
            Strs.Add("Valid Till(dd/MMM/yyyy)")
            Strs.Add("Description")
            Strs.Add("Amount")
            For ii As Integer = 1 To SettNoOFCustomerForImportExport
                Strs.Add("Customer " + clsCommon.myCstr(ii))
            Next
            If transportSql.importExcel(gv, Strs.ToArray()) Then
                Dim trans As SqlTransaction = Nothing
                Try
                    trans = clsDBFuncationality.GetTransactin()
                    clsCommon.ProgressBarShow()
                    For Each grow As GridViewRowInfo In gv.Rows
                        Dim obj As New clsCustomerDeductionHead()
                        linno += 1
                        obj.Deduction_Name = clsCommon.myCstr(grow.Cells("Description").Value)
                        If clsCommon.myLen(obj.Deduction_Name) > 0 Then
                            If obj.Deduction_Name.Length > 100 Then
                                Throw New Exception("Description length can not be more than 100 at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                            obj.Deduction_Amount = clsCommon.myCdbl(grow.Cells("Amount").Value)
                            If obj.Deduction_Amount < 0 Then
                                Throw New Exception("Deduction amount cannot be less than or equal to zero. " + clsCommon.myCstr(linno) + ".")
                            End If
                            obj.Deduction_Valid_Till = clsCommon.myCDate(grow.Cells("Valid Till(dd/MMM/yyyy)").Value)
                            obj.arr = New ArrayList()
                            For ii As Integer = 1 To SettNoOFCustomerForImportExport
                                Dim strCustCode As String = clsCommon.myCstr(grow.Cells("Customer " + clsCommon.myCstr(ii)).Value)
                                If clsCommon.myLen(strCustCode) > 0 Then
                                    strCustCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cust_Code from TSPL_CUSTOMER_MASTER  where Cust_Code='" + strCustCode + "'", trans))
                                    If clsCommon.myLen(strCustCode) <= 0 Then
                                        Throw New Exception("Invalid customer [" + clsCommon.myCstr(grow.Cells("Customer " + clsCommon.myCstr(ii)).Value) + "]")
                                    End If
                                    obj.arr.Add(strCustCode)
                                End If
                            Next
                            If obj.arr IsNot Nothing AndAlso obj.arr.Count > 0 Then
                                clsCustomerDeductionHead.SaveData(obj, True, trans)
                            End If
                        End If
                    Next
                    trans.Commit()
                    clsCommon.ProgressBarHide()
                    clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarHide()
                    Throw New Exception("Error at Line No" + clsCommon.myCstr(linno) + Environment.NewLine + ex.Message)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        Finally
            Me.Controls.Remove(gv)
        End Try
        

    End Sub

    Private Sub btnnew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnnew.Click
        funReset()
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        funClose()
    End Sub

    Private Sub btndelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub DeleteData()
        Try
            If (deleteConfirm()) Then
                If (clsCustomerDeductionHead.DeleteData(txtCode.Value)) Then
                    clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    funReset()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtMainGroup__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        'StrQry = "SELECT Comp_Code as Code,Comp_Name as Name,DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0"

        Dim StrQry As String = "select Cust_Code as Code,Customer_Name as Name from TSPL_CUSTOMER_MASTER "
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("cust@deducon", StrQry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("Document not found")
            End If
            If (postConfirm()) Then
                If (clsCustomerDeductionHead.PostData(txtCode.Value)) Then
                    clsCommon.MyMessageBoxShow("Successfully Posted")
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub chkInactive_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkInactive.ToggleStateChanged ''ERO/29/03/19-000526 by balwinder on 01/04/2019
        Try
            If Not isLoadData Then
                If chkInactive.Checked Then
                    If clsCommon.myLen(txtCode.Value) > 0 Then
                        If clsCommon.MyMessageBoxShow("Current deduction code [" + txtCode.Value + "] will be inactive" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, Telerik.WinControls.RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                            If (clsCustomerDeductionHead.InactiveData(txtCode.Value)) Then
                                clsCommon.MyMessageBoxShow("Successfully Inactivated")
                            End If
                        End If
                    End If
                End If
                LoadData(txtCode.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
End Class
