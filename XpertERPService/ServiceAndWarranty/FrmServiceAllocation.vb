' ----------------- Created By Anubhooti On 02-Sep-2015 Against BM00000006776-------------------- '
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports common
Imports System.IO
Imports XpertERPEngine

Public Class FrmServiceAllocation
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Dim isInsideLoadData As Boolean = False
    Public errorControl As clsErrorControl = New clsErrorControl()

#Region "Functions"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmServiceAllocation)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub
    Function AllowToSave() As Boolean
        Try
            btnsave.Focus()

            If clsCommon.myLen(TxtSerDoc.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please fill service document no.")
                TxtSerDoc.Focus()
                errorControl.SetError(TxtSerDoc, "service document no. must not be blank ")
                Return False
            Else
                errorControl.SetError(TxtSerDoc, "")
            End If

            If clsCommon.myLen(TxtEmpCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please fill engineer id")
                TxtEmpCode.Focus()
                errorControl.SetError(TxtEmpCode, "Engineer id must not be blank ")
                Return False
            Else
                errorControl.SetError(TxtEmpCode, "")
            End If

            'Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function
    Sub FunReset()
        isNewEntry = True
        txtcode.MyReadOnly = False
        txtcode.Value = Nothing
        txtcode.Focus()
        dtpDate.Value = clsCommon.GETSERVERDATE()
        TxtSerDoc.Value = ""
        LblSerDoc.Text = ""
        TxtEmpCode.Value = ""
        LblEmpName.Text = ""
        LblCustGrp.Text = ""
        LblDealer.Text = ""
        LblIssueN.Text = ""
        LblItemP.Text = ""
        LblVehicle.Text = ""

        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = False
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As ClsServiceAllocation = ClsServiceAllocation.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            FunReset()
            isNewEntry = False
            txtcode.Value = obj.Service_Allocation_Code
            TxtSerDoc.Value = obj.Service_Enquiry_Code
            TxtEmpCode.Value = obj.Engineer_Code
            dtpDate.Value = obj.Service_Allocation_Date

            If clsCommon.myLen(clsCommon.myCstr(obj.Service_Enquiry_Code)) > 0 Then
                LblSerDoc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Remarks FROM TSPL_SW_SERVICE_ENQUIRY WHERE Service_Enquiry_Code='" & clsCommon.myCstr(obj.Service_Enquiry_Code) & "'"))
                LoadEnquiryDetails(obj.Service_Enquiry_Code)
            Else
                LblSerDoc.Text = ""
            End If

            If clsCommon.myLen(clsCommon.myCstr(obj.Engineer_Code)) > 0 Then
                LblEmpName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Emp_Name FROM tspl_employee_master WHERE EMP_CODE='" & clsCommon.myCstr(obj.Engineer_Code) & "'"))
            Else
                LblEmpName.Text = ""
            End If
            txtcode.MyReadOnly = True
            btnsave.Text = "Update"
            btndelete.Enabled = True
        End If
    End Sub
    Sub SaveData()
        Try
            btnsave.Focus()
            If AllowToSave() Then
                Dim arr As New List(Of ClsServiceAllocation)
                Dim obj As New ClsServiceAllocation()
                obj.Service_Allocation_Code = txtcode.Value
                obj.Service_Enquiry_Code = TxtSerDoc.Value
                obj.Engineer_Code = TxtEmpCode.Value
                obj.Service_Allocation_Date = dtpDate.Value
                Dim qry As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT COUNT(Service_Allocation_Code) FROM TSPL_SW_SERVICE_ALLOCATION WHERE Service_Allocation_Code='" + obj.Service_Allocation_Code + "'"))
                If (qry = 0) Then
                    isNewEntry = True
                Else
                    isNewEntry = False
                End If
                arr.Add(obj)
                If (ClsServiceAllocation.SaveData(arr)) Then
                    clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                    LoadData(obj.Service_Allocation_Code, NavigatorType.Current)
                    btnsave.Text = "Update"
                    btndelete.Enabled = True
                Else
                    btnsave.Text = "Save"
                    btndelete.Enabled = False
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadEnquiryDetails(ByVal EnquiryCode As String)

        Dim Qry As String = String.Empty

        If clsCommon.myLen(EnquiryCode) > 0 Then
            Qry = "Select *  From TSPL_SW_SERVICE_ENQUIRY WHERE Service_Enquiry_Code='" & EnquiryCode & "'"
            Dim DT As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If (DT IsNot Nothing AndAlso DT.Rows.Count > 0) Then
                LblCustGrp.Text = clsCommon.myCstr(DT.Rows(0)("Cust_Group_Code"))
                LblDealer.Text = clsCommon.myCstr(DT.Rows(0)("Dealer_Code"))
                LblVehicle.Text = clsCommon.myCstr(DT.Rows(0)("Vehicle_Code"))
                LblItemP.Text = clsCommon.myCstr(DT.Rows(0)("Item_Part_No"))
                LblIssueN.Text = clsCommon.myCstr(DT.Rows(0)("Issued_Notice"))
            End If
        Else
            LblCustGrp.Text = ""
            LblDealer.Text = ""
            LblVehicle.Text = ""
            LblItemP.Text = ""
            LblIssueN.Text = ""
        End If
    End Sub
    Private Sub DeleteData()
        Try
            If clsCommon.myLen(txtcode.Value) <= 0 Then
                Throw New Exception("Code not found to delete")
            End If
            If clsCommon.MyMessageBoxShow("Are you sure? do you want to delete this Code ('" + txtcode.Value + "')", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then

                Dim qry As String = "DELETE FROM TSPL_SW_SERVICE_ALLOCATION WHERE Service_Allocation_Code='" + txtcode.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qry)
                clsCommon.MyMessageBoxShow("Deleted Successfully", Me.Text)
                FunReset()
            End If
        Catch ex As Exception
            If (clsCommon.CompairString(clsCommon.myCstr(ex.Message), "Code not found to delete") <> CompairStringResult.Equal) Then
                clsCommon.MyMessageBoxShow("Current Code is in use")
            Else
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End If
        End Try
    End Sub
#End Region
#Region "Events"
    Private Sub TxtEmpCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtEmpCode._MYValidating
        Try
            TxtEmpCode.Value = clsEmployeeMaster.getFinder("", TxtEmpCode.Value, isButtonClicked)
            If clsCommon.myLen(TxtEmpCode.Value) > 0 Then
                LblEmpName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Emp_Name FROM tspl_employee_master WHERE EMP_CODE='" & TxtEmpCode.Value & "'"))
            Else
                LblEmpName.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtSerDoc__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtSerDoc._MYValidating
        Try
            TxtSerDoc.Value = ClsServiceEnquiry.GetFinder(" Allocated = 0 ", TxtSerDoc.Value, isButtonClicked)
            If clsCommon.myLen(TxtSerDoc.Value) > 0 Then
                LblSerDoc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Remarks FROM TSPL_SW_SERVICE_ENQUIRY WHERE Service_Enquiry_Code='" & TxtSerDoc.Value & "'"))
            Else
                LblSerDoc.Text = ""
            End If
            LoadEnquiryDetails(TxtSerDoc.Value)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtcode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtcode._MYNavigator
        Try
            LoadData(txtcode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtcode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtcode._MYValidating
        Dim str As String = "select count(*) from TSPL_SW_SERVICE_ALLOCATION where Service_Allocation_Code ='" + txtcode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtcode.MyReadOnly = False
        Else
            txtcode.MyReadOnly = True
        End If

        If txtcode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = ""
            txtcode.Value = ClsServiceAllocation.GetFinder("", txtcode.Value, isButtonClicked)
            If clsCommon.myLen(txtcode.Value) > 0 Then
                Dim objOT As ClsServiceAllocation
                objOT = ClsServiceAllocation.GetData(txtcode.Value, NavigatorType.Current)
                If Not objOT Is Nothing Then
                    LoadData(txtcode.Value, NavigatorType.Current)
                End If
            Else
                FunReset()
            End If
        End If
    End Sub

    Private Sub FrmServiceAllocation_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            FunReset()
        End If
    End Sub

    Private Sub FrmServiceAllocation_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetUserMgmtNew()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
        FunReset()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        FunReset()
    End Sub
#End Region
End Class