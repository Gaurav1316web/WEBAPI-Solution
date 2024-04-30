'========================================================
'----Created By--Pankaj Kumar
'----Date-11/02/2013-Monday
'----Table Used--TSPL_SD_USERCUSTOMER_RATE
'========================================================
Imports common
Imports System.Data.SqlClient

Public Class Frm_User_Customer_Rate_Settings
    Inherits FrmMainTranScreen
    Dim Qry As String = ""
    Dim dt As DataTable
    Private isInsideLoadData As Boolean = False
    Const colLineNo As String = "LineNo"
    Const colUserCode As String = "UserCOde"
    Const colUserName As String = "UserName"
    Const colCustomerCode As String = "CustomerCode"
    Const colCustomerName As String = "CustomerName"
    Const colIsEditable As String = "IsEditable"

    Private Sub Frm_User_Customer_Rate_Settings_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled AndAlso MyBase.isDeleteFlag Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            Me.Close()
        End If
    End Sub

    Private Sub Frm_User_Customer_Rate_Settings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Reset()
        SetUserMgmtNew()
        LoadData()
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.AssetSegment)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
        End If
        btnSave.Visible = MyBase.isModifyFlag
    End Sub

    Sub LoadBlankGrid()
        dgvUserCustomer.Rows.Clear()
        dgvUserCustomer.Columns.Clear()

        Dim LineNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        LineNo.FormatString = ""
        LineNo.HeaderText = "Line No"
        LineNo.Name = colLineNo
        LineNo.Width = 71
        LineNo.ReadOnly = True
        LineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        dgvUserCustomer.MasterTemplate.Columns.Add(LineNo)

        Dim UserCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        UserCode.FormatString = ""
        UserCode.HeaderText = "User Code"
        UserCode.Name = colUserCode
        UserCode.Width = 151
        UserCode.ReadOnly = False
        UserCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        dgvUserCustomer.MasterTemplate.Columns.Add(UserCode)

        Dim UserName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        UserName.FormatString = ""
        UserName.HeaderText = "User Name"
        UserName.Name = colUserName
        UserName.Width = 251
        UserName.ReadOnly = True
        UserName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        dgvUserCustomer.MasterTemplate.Columns.Add(UserName)

        Dim CustomerCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        CustomerCode.FormatString = ""
        CustomerCode.HeaderText = "Customer Code"
        CustomerCode.Name = colCustomerCode
        CustomerCode.Width = 151
        CustomerCode.ReadOnly = False
        CustomerCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        dgvUserCustomer.MasterTemplate.Columns.Add(CustomerCode)

        Dim CustomerName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        CustomerName.FormatString = ""
        CustomerName.HeaderText = "Customer Name"
        CustomerName.Name = colCustomerName
        CustomerName.Width = 251
        CustomerName.ReadOnly = True
        CustomerName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        dgvUserCustomer.MasterTemplate.Columns.Add(CustomerName)

        Dim IsEditable As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        IsEditable.FormatString = ""
        IsEditable.HeaderText = "Is Editable"
        IsEditable.Name = colIsEditable
        IsEditable.Width = 71
        IsEditable.ReadOnly = False
        IsEditable.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        dgvUserCustomer.MasterTemplate.Columns.Add(IsEditable)

        dgvUserCustomer.AllowDeleteRow = True
        dgvUserCustomer.Rows.AddNew()
        dgvUserCustomer.ShowGroupPanel = False
        dgvUserCustomer.AllowColumnReorder = False
        dgvUserCustomer.AllowRowReorder = False
        dgvUserCustomer.EnableSorting = False
        dgvUserCustomer.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        dgvUserCustomer.MasterTemplate.ShowRowHeaderColumn = False
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub
    Private Sub SaveData()
        Try
            If AllowToSave() Then
                If chkRateEditable.Checked = True Then
                    ClsUserCustomerSettings.RateEditable = "1"
                Else
                    ClsUserCustomerSettings.RateEditable = "0"
                End If
                Dim Arr As New List(Of ClsUserCustomerSettings)
                For Each grow As GridViewRowInfo In dgvUserCustomer.Rows
                    Dim objTr As New ClsUserCustomerSettings()
                    If clsCommon.myLen(grow.Cells(colUserCode).Value) > 0 Then
                        objTr.User_Code = clsCommon.myCstr(grow.Cells(colUserCode).Value)
                        objTr.CustomerCode = clsCommon.myCstr(grow.Cells(colCustomerCode).Value)
                        If grow.Cells(colIsEditable).Value = True Then
                            objTr.Is_Editable = "1"
                        End If
                        Arr.Add(objTr)
                    End If
                Next

                If (ClsUserCustomerSettings.SaveData(chkRateEditable.Checked, Arr)) Then
                    RadMessageBox.Show("Data Saved Successfully")
                    LoadData()
                End If
            End If
        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function AllowToSave() As Boolean
        Dim ii As Integer = 0
        Dim jj As Integer = 0
        If dgvUserCustomer.Rows.Count > 0 Then
            For ii = 0 To dgvUserCustomer.Rows.Count - 1
                If clsCommon.myLen(dgvUserCustomer.Rows(ii).Cells(colUserCode).Value) > 0 Then
                    If clsCommon.myLen(dgvUserCustomer.Rows(ii).Cells(colCustomerCode).Value) <= 0 Then
                        RadMessageBox.Show("Enter Customer Code On Line No '" + clsCommon.myCstr(ii + 1) + "'")
                        Return False
                    End If
                End If
                For jj = ii + 1 To dgvUserCustomer.Rows.Count - 1
                    If clsCommon.CompairString(clsCommon.myCstr(dgvUserCustomer.Rows(ii).Cells(colUserCode).Value), clsCommon.myCstr(dgvUserCustomer.Rows(jj).Cells(colUserCode).Value)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(dgvUserCustomer.Rows(ii).Cells(colCustomerCode).Value), clsCommon.myCstr(dgvUserCustomer.Rows(jj).Cells(colCustomerCode).Value)) = CompairStringResult.Equal Then
                        RadMessageBox.Show("User And Customer Are Identical On Line No '" + clsCommon.myCstr(ii + 1) + "' AND '" + clsCommon.myCstr(jj + 1) + "' ")
                        Return False
                    End If
                Next
            Next
        End If
        Return True
    End Function

    Private Sub LoadData()
        Try
            chkRateEditable.Checked = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SalesRateEditable, clsFixedParameterCode.SalesRateEditable, Nothing)) = 1, True, False)

            LoadBlankGrid()
            Dim Arr As New List(Of ClsUserCustomerSettings)
            Arr = ClsUserCustomerSettings.GetData()
            If Arr.Count > 0 Then
                isInsideLoadData = True
                Dim LineNo As Integer = 0
                For Each objTr As ClsUserCustomerSettings In Arr
                    LineNo += 1
                    dgvUserCustomer.CurrentRow.Cells(colLineNo).Value = clsCommon.myCstr(LineNo)
                    dgvUserCustomer.CurrentRow.Cells(colUserCode).Value = objTr.User_Code
                    dgvUserCustomer.CurrentRow.Cells(colUserName).Value = GetUserName(objTr.User_Code)
                    dgvUserCustomer.CurrentRow.Cells(colCustomerCode).Value = objTr.CustomerCode
                    dgvUserCustomer.CurrentRow.Cells(colCustomerName).Value = GetCustomerName(objTr.CustomerCode)
                    If objTr.Is_Editable = "1" Then
                        dgvUserCustomer.CurrentRow.Cells(colIsEditable).Value = True
                    End If
                    dgvUserCustomer.Rows.AddNew()
                Next
            End If
        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Private Function GetUserName(ByVal UserCode As String)
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select User_Name from TSPL_USER_MASTER Where User_Code='" + UserCode + "'"))
    End Function

    Private Function GetCustomerName(ByVal UserCode As String)
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select  Customer_Name from TSPL_CUSTOMER_MASTER Where Cust_Code='" + UserCode + "'"))
    End Function


    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub dgvUserCustomer_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles dgvUserCustomer.CellValueChanged
        Try
            If isInsideLoadData = False Then
                If e.Column Is dgvUserCustomer.Columns(colUserCode) Then
                    OpenUser(False)
                ElseIf e.Column Is dgvUserCustomer.Columns(colCustomerCode) Then
                    If clsCommon.myLen(dgvUserCustomer.CurrentRow.Cells(colUserCode).Value) <= 0 Then
                        If clsCommon.myLen(dgvUserCustomer.CurrentRow.Cells(colCustomerCode).Value) > 0 Then
                            RadMessageBox.Show("Please select user code first.")
                            dgvUserCustomer.CurrentRow.Cells(colCustomerCode).Value = ""
                            Exit Sub
                        End If
                    Else
                        OpenCustomer(False)
                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub OpenUser(ByVal isButtonClick As Boolean)
        Qry = "Select User_Code as Code, User_Name as Name from TSPL_USER_MASTER"
        dgvUserCustomer.CurrentRow.Cells(colUserCode).Value = clsCommon.ShowSelectForm("UCSUserFinder", Qry, "Code", "", clsCommon.myCstr(dgvUserCustomer.CurrentRow.Cells(colUserCode).Value), "Code", isButtonClick)
        dgvUserCustomer.CurrentRow.Cells(colUserName).Value = GetUserName(clsCommon.myCstr(dgvUserCustomer.CurrentRow.Cells(colUserCode).Value))
    End Sub

    Sub OpenCustomer(ByVal isButtonClick As Boolean)
        Qry = "Select Cust_Code as Code,  Customer_Name as Name from TSPL_CUSTOMER_MASTER"
        dgvUserCustomer.CurrentRow.Cells(colCustomerCode).Value = clsCommon.ShowSelectForm("UCSCustomerFinder", Qry, "Code", "", clsCommon.myCstr(dgvUserCustomer.CurrentRow.Cells(colCustomerCode).Value), "Code", isButtonClick)
        dgvUserCustomer.CurrentRow.Cells(colCustomerName).Value = GetCustomerName(clsCommon.myCstr(dgvUserCustomer.CurrentRow.Cells(colCustomerCode).Value))
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        
        DeleteData()
    End Sub
    Private Sub DeleteData()
        Try
            ClsUserCustomerSettings.DeleteData(Nothing)
            clsCommon.MyMessageBoxShow("Data deleted Successfully")
            LoadData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub dgvUserCustomer_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles dgvUserCustomer.CurrentColumnChanged
        If dgvUserCustomer.RowCount > 0 Then
            Dim intCurrRow As Integer = dgvUserCustomer.CurrentRow.Index
            dgvUserCustomer.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = dgvUserCustomer.Rows.Count - 1 Then
                dgvUserCustomer.Rows.AddNew()
                dgvUserCustomer.CurrentRow = dgvUserCustomer.Rows(intCurrRow)
            End If
        End If
    End Sub
End Class
