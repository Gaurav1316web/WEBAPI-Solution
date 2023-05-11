Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Collections
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Imports System.Text.RegularExpressions
Imports common
Imports System.Globalization
Imports XpertERPEngine

Public Class frmConsumerMaster
    Inherits FrmMainTranScreen

#Region "Variable"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False
    Private isCellValueChangedOpen As Boolean = False
    Private isNewEntry As Boolean = True
#End Region

#Region "Variable"
    Dim strCmd As String
    Dim myDt As DataTable
    Dim myDr As SqlDataReader
    Dim myDs As DataSet
    Dim myDataTable As DataTable
    Dim i As Integer
#End Region

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmConsumerDetailsForm)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub loadTitle()
        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        dt.Rows.Add("Mr.", "Mr.")
        dt.Rows.Add("Mrs.", "Mrs.")
        dt.Rows.Add("Ms.", "Ms.")
        ddlTitle.DataSource = dt
        ddlTitle.ValueMember = "Code"
        ddlTitle.DisplayMember = "Name"
    End Sub

    Sub loadMaritalStatus()
        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        dt.Rows.Add("Single", "Single")
        dt.Rows.Add("Married", "Married")
        ddlMaritalStatus.DataSource = dt
        ddlMaritalStatus.ValueMember = "Code"
        ddlMaritalStatus.DisplayMember = "Name"
    End Sub

    Sub loadGender()
        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        dt.Rows.Add("M", "Male")
        dt.Rows.Add("F", "Female")
        ddlGender.DataSource = dt
        ddlGender.ValueMember = "Code"
        ddlGender.DisplayMember = "Name"
    End Sub

    Sub loadProductUsed()
        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        dt.Rows.Add("Amul", "Amul")
        dt.Rows.Add("Mother Dairy", "Mother Dairy")
        dt.Rows.Add("Dairy Best", "Dairy Best")
        dt.Rows.Add("Other", "Other")
        ddlProductUsed.DataSource = dt
        ddlProductUsed.ValueMember = "Code"
        ddlProductUsed.DisplayMember = "Name"
    End Sub

    Sub loadHowKnow()
        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        dt.Rows.Add("Advertisment", "Advertisment")
        dt.Rows.Add("TV News", "TV News")
        dt.Rows.Add("Friend/Relative", "Friend/Relative")
        dt.Rows.Add("Other", "Other")
        ddlHowKnow.DataSource = dt
        ddlHowKnow.ValueMember = "Code"
        ddlHowKnow.DisplayMember = "Name"
    End Sub

    Private Sub frmConsumerMaster_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            FunReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            btnSave.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            btnDelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub

    Private Sub frmConsumerMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        loadTitle()
        loadMaritalStatus()
        loadGender()
        loadProductUsed()
        loadHowKnow()

        FunReset()

        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for save record.")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D for delete record.")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C close the window.")
    End Sub

    Public Sub FunReset()
        fndConsumerCode.Value = ""
        Me.txtFirstName.Text = ""
        Me.txtMiddleName.Text = ""
        Me.txtLastName.Text = ""

        Me.dtpDOB.Text = clsCommon.GETSERVERDATE()
        Me.txtFatherName.Text = ""
        Me.ddlGender.SelectedValue = ""
        Me.txtEducation.Text = ""

        Me.txtAdd1.Text = ""
        Me.txtAdd2.Text = ""
        Me.txtAdd3.Text = ""
        Me.fndCountry.Value = ""
        Me.txtCountry.Text = ""
        Me.fndState.Value = ""
        Me.txtState.Text = ""
        Me.fndCity.Value = ""
        Me.txtCity.Text = ""
        Me.txtPinNo.Text = ""

        chkSameAdd.Checked = False

        Me.txtPAdd1.Text = ""
        Me.txtPAdd2.Text = ""
        Me.txtPAdd3.Text = ""
        Me.fndPCountry.Value = ""
        Me.txtPCountry.Text = ""
        Me.fndPState.Value = ""
        Me.txtPState.Text = ""
        Me.fndPCity.Value = ""
        Me.txtPCity.Text = ""
        Me.txtPPinNo.Text = ""

        Me.txtPAdd1.Enabled = True
        Me.txtPAdd2.Enabled = True
        Me.txtPAdd3.Enabled = True
        Me.fndPCountry.Enabled = True
        Me.fndPState.Enabled = True
        Me.fndPCity.Enabled = True
        Me.txtPCountry.Enabled = True
        Me.txtPState.Enabled = True
        Me.txtPCity.Enabled = True
        Me.txtPPinNo.Enabled = True
        Me.txtMobileNo.Text = "(+__)__________"
        Me.txtLandLineNo.Text = "(+__)__________"
        Me.txtEmail.Text = ""
        Me.txtAlternateEmail.Text = ""
        Me.txtSpecifyOtherProduct.Text = ""
        Me.txtSpecifyHowKnow.Text = ""
        MyLabel18.Visible = False
        MyLabel19.Visible = False
        txtSpecifyOtherProduct.Visible = False
        txtSpecifyHowKnow.Visible = False

        isNewEntry = True
        btnSave.Text = "Save"
        btnDelete.Enabled = False
        fndConsumerCode.MyReadOnly = False

    End Sub

    Sub LoadDataConsumerDetails(ByVal strConsumerCode As String, ByVal NavType As NavigatorType)
        Try
            fndConsumerCode.MyReadOnly = True
            btnSave.Enabled = True
            btnSave.Text = "Update"
            btnDelete.Enabled = True
            isNewEntry = False
            Dim obj As New clsConsumerMaster()
            obj = clsConsumerMaster.GetData(strConsumerCode, NavType)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Consumer_Code) > 0) Then
                fndConsumerCode.Value = obj.Consumer_Code
                ddlTitle.SelectedValue = obj.Title
                txtFirstName.Text = obj.First_Name
                txtMiddleName.Text = obj.Middle_Name
                txtLastName.Text = obj.Last_Name
                'If obj.DOB IsNot Nothing AndAlso clsCommon.myLen(obj.DOB) > 0 AndAlso IsDate(obj.DOB) Then
                '    dtpDOB.Text = obj.DOB
                'End If
                dtpDOB.Text = obj.DOB
                txtFatherName.Text = obj.Father_Name
                ddlMaritalStatus.SelectedValue = obj.Marital_Status
                ddlMaritalStatus.SelectedValue = obj.Gender
                txtEducation.Text = obj.Education
                txtAdd1.Text = obj.C_Add1
                txtAdd2.Text = obj.C_Add2
                txtAdd3.Text = obj.C_Add3
                fndCountry.Value = obj.C_Country
                txtCountry.Text = clsCountryMaster.GetName(obj.C_Country, Nothing)
                fndState.Value = obj.C_State
                txtState.Text = clsStateMaster.GetName(obj.C_State)
                fndCity.Value = obj.C_City
                txtCity.Text = clsCityMaster.GetName(obj.C_City)
                txtPinNo.Text = obj.C_Pin_No

                If clsCommon.CompairString(obj.Same_Address, "1") = CompairStringResult.Equal Then
                    chkSameAdd.Checked = True
                Else
                    chkSameAdd.Checked = False
                End If

                txtPAdd1.Text = obj.P_Add1
                txtPAdd2.Text = obj.P_Add2
                txtPAdd3.Text = obj.P_Add3
                fndPCountry.Value = obj.P_Country
                txtPCountry.Text = clsCountryMaster.GetName(obj.P_Country, Nothing)
                fndPState.Value = obj.P_State
                txtPState.Text = clsStateMaster.GetName(obj.P_State)
                fndPCity.Value = obj.P_City
                txtPCity.Text = clsCityMaster.GetName(obj.P_City)
                txtPPinNo.Text = obj.P_Pin_No
                txtMobileNo.Text = obj.Mobile_No
                txtLandLineNo.Text = obj.Land_Line_No
                txtEmail.Text = obj.Email
                txtAlternateEmail.Text = obj.Alternate_Email
                ddlProductUsed.SelectedValue = obj.Product_Used
                If ddlProductUsed.SelectedValue = "Other" Then
                    MyLabel18.Visible = True
                    txtSpecifyOtherProduct.Visible = True
                End If
                txtSpecifyOtherProduct.Text = obj.Specify_Product_Used
                ddlHowKnow.SelectedValue = obj.How_To_Know
                If (ddlHowKnow.SelectedValue.ToString() = "Other") Then
                    MyLabel19.Visible = True
                    txtSpecifyHowKnow.Visible = True
                End If
                txtSpecifyHowKnow.Text = obj.Specify_How_To_Know
            Else
                FunReset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Private Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(txtFirstName.Text) <= 0 Then
                txtFirstName.Focus()
                txtFirstName.Select()
                Throw New Exception("Please enter first name")

            End If
            If clsCommon.myLen(txtCountry.Text) <= 0 Then
                fndCountry.Focus()
                fndCountry.Select()
                Throw New Exception("Please enter country name")
            End If
            If clsCommon.myLen(txtState.Text) <= 0 Then
                fndState.Focus()
                fndState.Select()
                Throw New Exception("Please enter state name")
            End If
            If clsCommon.myLen(txtCity.Text) <= 0 Then
                fndCity.Focus()
                fndCity.Select()
                Throw New Exception("Please enter city name")
            End If
            If clsCommon.myLen(txtMobileNo.Text) <= 0 Then
                txtMobileNo.Focus()
                txtMobileNo.Select()
                Throw New Exception("Please enter mobile no")
            End If
            'Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
        Return True
    End Function

    Sub SaveConsumerDetails()
        Try
            Dim obj As New clsConsumerMaster()
            obj.Consumer_Code = fndConsumerCode.Value
            obj.Title = ddlTitle.SelectedValue.ToString()
            obj.First_Name = txtFirstName.Text
            obj.Middle_Name = txtMiddleName.Text
            obj.Last_Name = txtLastName.Text
            'If dtpDOB.Text IsNot Nothing AndAlso clsCommon.myLen(dtpDOB.Text) > 0 AndAlso IsDate(dtpDOB.Text) Then
            '    obj.DOB = clsCommon.myCDate(dtpDOB.Text)
            'End If
            'obj.DOB = clsCommon.myCDate(dtpDOB.Text, "dd/MMM/yyyy hh:mm tt")
            obj.DOB = clsCommon.myCDate(dtpDOB.Text)
            obj.Father_Name = txtFatherName.Text
            obj.Marital_Status = ddlMaritalStatus.SelectedValue.ToString()
            obj.Gender = ddlGender.SelectedValue
            obj.Education = txtEducation.Text
            obj.C_Add1 = txtAdd1.Text
            obj.C_Add2 = txtAdd2.Text
            obj.C_Add3 = txtAdd3.Text
            obj.C_Country = fndCountry.Value
            obj.C_State = fndState.Value
            obj.C_City = fndCity.Value
            obj.C_Pin_No = txtPinNo.Text
            If chkSameAdd.Checked = True Then
                obj.Same_Address = 1
            Else
                obj.Same_Address = 0
            End If
            obj.P_Add1 = txtPAdd1.Text
            obj.P_Add2 = txtPAdd2.Text
            obj.P_Add3 = txtPAdd3.Text
            obj.P_Country = fndPCountry.Value
            obj.P_State = fndPState.Value
            obj.P_City = fndPCity.Value
            obj.P_Pin_No = txtPPinNo.Text
            obj.Mobile_No = txtMobileNo.Text
            obj.Land_Line_No = txtLandLineNo.Text
            obj.Email = txtEmail.Text
            obj.Alternate_Email = txtAlternateEmail.Text
            obj.Product_Used = ddlProductUsed.SelectedValue.ToString()
            obj.Specify_Product_Used = txtSpecifyOtherProduct.Text
            obj.How_To_Know = ddlHowKnow.SelectedValue.ToString()
            obj.Specify_How_To_Know = txtSpecifyHowKnow.Text
            obj.Details_Date = clsCommon.myCDate(clsCommon.GETSERVERDATE())

            If (clsConsumerMaster.SaveData(obj, isNewEntry)) Then
                common.clsCommon.MyMessageBoxShow("Data Saved Successfully.")
                LoadDataConsumerDetails(obj.Consumer_Code, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub DeleteConsumerDetails()
        Try
            If clsCommon.myLen(fndConsumerCode.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Consumer code is not found to delete.")
                Exit Sub
            End If
            If (myMessages.deleteConfirm()) Then
                If (clsConsumerMaster.DeleteData(fndConsumerCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    FunReset()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Public Sub ImportData()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Consumer_Code", "Title", "First_Name", "Middle_Name", "Last_Name", "DOB", "Father_Name", "Marital_Status", "Gender", "Education", "C_Add1", "C_Add2", "C_Add3", "C_Country", "C_State", "C_City", "C_Pin_No", "P_Add1", "P_Add2", "P_Add3", "P_Country", "P_State", "P_City", "P_Pin_No", "Mobile_No", "Land_Line_No", "Email", "Alternate_Email", "Product_Used", "Specify_Product_Used", "How_To_Know", "Specify_How_To_Know", "Details_Date") Then
            Try
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clsConsumerMaster()

                    Dim strCode As String = clsCommon.myCstr(grow.Cells(0).Value)
                    If (String.IsNullOrEmpty(strCode)) OrElse strCode.Length > 30 Then
                        Throw New Exception("Consumer Code cannot be blank or incorrect.")
                    End If
                    obj.Consumer_Code = strCode

                    Dim strTitle As String = clsCommon.myCstr(grow.Cells(1).Value)
                    If (String.IsNullOrEmpty(strCode)) OrElse strTitle.Length > 5 Then
                        Throw New Exception("Title cannot be blank or incorrect.")
                    End If
                    obj.Title = strTitle

                    Dim strFirstName As String = clsCommon.myCstr(grow.Cells(2).Value)
                    If (String.IsNullOrEmpty(strFirstName)) OrElse strFirstName.Length > 30 Then
                        Throw New Exception("First Name cannot be blank or incorrect.")
                    End If
                    obj.First_Name = strFirstName

                    Dim strMiddleName As String = clsCommon.myCstr(grow.Cells(3).Value) ''should check length from backend
                    If strMiddleName.Length > 30 Then
                        Throw New Exception("First Name cannot be incorrect.")
                    End If
                    obj.Middle_Name = strMiddleName

                    Dim strLastName As String = clsCommon.myCstr(grow.Cells(4).Value)
                    If strLastName.Length > 30 Then
                        Throw New Exception("First Name cannot be incorrect.")
                    End If
                    obj.Last_Name = strLastName

                    Dim strDOB As String = clsCommon.myCDate(grow.Cells(5).Value)
                    If IsDate(strDOB) Then
                        obj.DOB = strDOB
                    ElseIf strDOB IsNot Nothing OrElse strDOB.Length > 20 Then
                        Throw New Exception("DOB is not in correct format.")
                    End If

                    Dim strFatherName As String = clsCommon.myCstr(grow.Cells(6).Value)
                    If strFatherName.Length > 30 Then
                        Throw New Exception("Father Name cannot be incorrect.")
                    End If
                    obj.Father_Name = strFatherName

                    Dim strMaritalStatus As String = clsCommon.myCstr(grow.Cells(7).Value)
                    If clsCommon.CompairString(clsCommon.myCstr(strMaritalStatus), "Single") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(strMaritalStatus), "Unmarried") = CompairStringResult.Equal Then
                        obj.Marital_Status = strMaritalStatus
                    Else
                        Throw New Exception("Marital status will be Single or Unmarried")
                    End If

                    Dim strGender As String = clsCommon.myCstr(grow.Cells(8).Value)
                    If clsCommon.CompairString(clsCommon.myCstr(strGender), "M") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(strGender), "F") = CompairStringResult.Equal Then
                        obj.Gender = strGender
                    Else
                        Throw New Exception("Gender will be M or F")
                    End If

                    Dim strEducation As String = clsCommon.myCstr(grow.Cells(9).Value)
                    If strEducation.Length > 30 Then
                        Throw New Exception("Education cannot be incorrect.")
                    End If
                    obj.Education = strEducation

                    Dim strC_Add1 As String = clsCommon.myCstr(grow.Cells(10).Value)
                    If strC_Add1.Length > 100 Then
                        Throw New Exception("Address cannot be incorrect.")
                    End If
                    obj.C_Add1 = strC_Add1

                    Dim strC_Add2 As String = clsCommon.myCstr(grow.Cells(11).Value)
                    If strC_Add2.Length > 100 Then
                        Throw New Exception("Address cannot be incorrect.")
                    End If
                    obj.C_Add2 = strC_Add2

                    Dim strC_Add3 As String = clsCommon.myCstr(grow.Cells(12).Value)
                    If strC_Add3.Length > 100 Then
                        Throw New Exception("Address cannot be incorrect.")
                    End If
                    obj.C_Add3 = strC_Add3

                    Dim strC_Country As String = clsCommon.myCstr(grow.Cells(13).Value)
                    If strC_Country.Length > 30 Then
                        Throw New Exception("Country cannot be incorrect.")
                    End If
                    obj.C_Country = strC_Country

                    Dim strC_State As String = clsCommon.myCstr(grow.Cells(14).Value)
                    If strC_State.Length > 30 Then
                        Throw New Exception("State cannot be incorrect.")
                    End If
                    obj.C_State = strC_State

                    Dim strC_City As String = clsCommon.myCstr(grow.Cells(15).Value)
                    If strC_City.Length > 30 Then
                        Throw New Exception("City cannot be incorrect.")
                    End If
                    obj.C_City = strC_City

                    Dim strC_Pin_No As String = clsCommon.myCstr(grow.Cells(16).Value)
                    If strC_Pin_No.Length > 6 Then
                        Throw New Exception("Pin code cannot greate that 6 digit.")
                    End If
                    obj.C_Pin_No = strC_Pin_No

                    Dim strP_Add1 As String = clsCommon.myCstr(grow.Cells(17).Value)
                    If strP_Add1.Length > 100 Then
                        Throw New Exception("Address cannot be incorrect.")
                    End If
                    obj.P_Add1 = strP_Add1

                    Dim strP_Add2 As String = clsCommon.myCstr(grow.Cells(18).Value)
                    If strP_Add2.Length > 100 Then
                        Throw New Exception("Address cannot be incorrect.")
                    End If
                    obj.P_Add2 = strP_Add2

                    Dim strP_Add3 As String = clsCommon.myCstr(grow.Cells(19).Value)
                    If strP_Add3.Length > 100 Then
                        Throw New Exception("Address cannot be incorrect.")
                    End If
                    obj.P_Add3 = strP_Add3

                    Dim strP_Country As String = clsCommon.myCstr(grow.Cells(20).Value)
                    If strP_Country.Length > 30 Then
                        Throw New Exception("Country cannot be incorrect.")
                    End If
                    obj.P_Country = strP_Country

                    Dim strP_State As String = clsCommon.myCstr(grow.Cells(21).Value)
                    If strP_State.Length > 30 Then
                        Throw New Exception("State cannot be incorrect.")
                    End If
                    obj.P_State = strP_State

                    Dim strP_City As String = clsCommon.myCstr(grow.Cells(22).Value)
                    If strP_City.Length > 30 Then
                        Throw New Exception("City cannot be incorrect.")
                    End If
                    obj.P_City = strP_City

                    Dim strP_Pin_No As String = clsCommon.myCstr(grow.Cells(23).Value)
                    If strP_Pin_No.Length > 6 Then
                        Throw New Exception("Pin code cannot greate that 6 digit.")
                    End If
                    obj.P_Pin_No = strP_Pin_No

                    Dim strMobile_No As String = clsCommon.myCstr(grow.Cells(24).Value)
                    If strMobile_No.Length > 15 Or (String.IsNullOrEmpty(strMobile_No)) Then
                        Throw New Exception("Mobile No cannot be blank or incorrect.")
                    End If
                    obj.Mobile_No = strMobile_No

                    Dim strLand_Line_No As String = clsCommon.myCstr(grow.Cells(25).Value)
                    If strLand_Line_No.Length > 20 Then
                        Throw New Exception("Landline No cannot be incorrect.")
                    End If
                    obj.Land_Line_No = strLand_Line_No

                    Dim strEmail As String = clsCommon.myCstr(grow.Cells(26).Value)
                    If strEmail.Length > 50 Then
                        Throw New Exception("Email cannot be incorrect.")
                    End If
                    obj.Email = strEmail

                    Dim strAlternate_Email As String = clsCommon.myCstr(grow.Cells(27).Value)
                    If strAlternate_Email.Length > 50 Then
                        Throw New Exception("Alternate Email cannot be incorrect.")
                    End If
                    obj.Alternate_Email = strAlternate_Email

                    Dim strProduct_Used As String = clsCommon.myCstr(grow.Cells(28).Value)
                    If strProduct_Used.Length > 20 Then
                        Throw New Exception("Product Used No cannot be incorrect.")
                    End If
                    obj.Product_Used = strProduct_Used

                    Dim strSpecify_Product_Used As String = clsCommon.myCstr(grow.Cells(29).Value)
                    If strSpecify_Product_Used.Length > 20 Then
                        Throw New Exception("Specify product used cannot be incorrect.")
                    End If
                    obj.Specify_Product_Used = strSpecify_Product_Used

                    Dim strHow_To_Know As String = clsCommon.myCstr(grow.Cells(30).Value)
                    If strHow_To_Know.Length > 20 Then
                        Throw New Exception("How to know us cannot be incorrect.")
                    End If
                    obj.How_To_Know = strHow_To_Know

                    Dim strSpecify_How_To_Know As String = clsCommon.myCstr(grow.Cells(31).Value)
                    If strSpecify_How_To_Know.Length > 20 Then
                        Throw New Exception("Specify how to know us cannot be incorrect.")
                    End If
                    obj.Specify_How_To_Know = strSpecify_How_To_Know

                    Dim strDetails_Date As String = clsCommon.myCDate(grow.Cells(32).Value)
                    If IsDate(strDetails_Date) Then
                        obj.Details_Date = strDetails_Date
                    ElseIf strDetails_Date IsNot Nothing OrElse strSpecify_How_To_Know.Length > 20 Then
                        Throw New Exception("Document date is not in correct format.")
                    End If

                    Dim qry As String = "SELECT Count(*) FROM TSPL_CONSUMER_MASTER  where Consumer_Code= '" & obj.Consumer_Code & "'"
                    Dim check As Integer = clsDBFuncationality.getSingleValue(qry, Nothing)
                    If check = 0 Then
                        clsConsumerMaster.SaveData(obj, True)
                    Else
                        clsConsumerMaster.SaveData(obj, False)
                    End If
                Next
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

    Public Sub ExportData()
        Try
            strCmd = "Select Consumer_Code,Title,First_Name,Middle_Name,Last_Name,convert (varchar,DOB,103 ) as DOB,Father_Name,Marital_Status,Gender,Education,C_Add1,C_Add2,C_Add3,C_Country,C_State,C_City,C_Pin_No,P_Add1,P_Add2,P_Add3,P_Country,P_State,P_City,P_Pin_No,Mobile_No,Land_Line_No,Email,Alternate_Email,Product_Used,Specify_Product_Used,How_To_Know,Specify_How_To_Know,convert (varchar,Details_Date,103 ) as Details_Date from TSPL_CONSUMER_MASTER "
            transportSql.ExporttoExcel(strCmd, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndConsumerCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles fndConsumerCode._MYNavigator
        Try
            LoadDataConsumerDetails(fndConsumerCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndConsumerCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndConsumerCode._MYValidating
        Dim qry As String = " SELECT Consumer_Code,Title,First_Name,Middle_Name,Last_Name,DOB,Father_Name,Marital_Status,Gender,Education,C_Add1,C_Add2,C_Add3,C_Country,C_State,C_City,C_Pin_No,Same_Address,P_Add1,P_Add2,P_Add3,P_Country,P_State,P_City,P_Pin_No,Mobile_No,Land_Line_No,Email,Alternate_Email,Product_Used,Specify_Product_Used,How_To_Know,Specify_How_To_Know FROM TSPL_CONSUMER_MASTER  "
        LoadDataConsumerDetails(clsCommon.myCstr(clsCommon.ShowSelectForm("CONSUMDETAILS", qry, "Consumer_Code", "", fndConsumerCode.Value, "Consumer_Code", isButtonClicked)), NavigatorType.Current)
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        FunReset()
    End Sub

    Private Sub fndCountry__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCountry._MYValidating
        Try
            fndCountry.Value = clsCountryMaster.getFinder("", fndCountry.Value, isButtonClicked)
            If clsCommon.myLen(fndCountry.Value) > 0 Then
                txtCountry.Text = clsCountryMaster.GetName(fndCountry.Value, Nothing)
                If (chkSameAdd.Checked = True) Then
                    fndPCountry.Value = fndCountry.Value
                    txtPCountry.Text = txtCountry.Text
                Else
                    fndPCountry.Value = ""
                    txtPCountry.Text = ""
                End If
            Else
                txtCountry.Text = ""
                fndstate.Value = ""
                txtState.Text = ""
                txtCity.Text = ""
                fndCity.Value = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndState__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndState._MYValidating
        Try
            fndState.Value = clsStateMaster.getFinder("country_code='" + fndCountry.Value + "' ", fndState.Value, isButtonClicked)
            If clsCommon.myLen(fndState.Value) > 0 Then
                txtState.Text = clsStateMaster.GetName(fndState.Value)
                If (chkSameAdd.Checked = True) Then
                    fndPState.Value = fndState.Value
                    txtPState.Text = txtState.Text
                Else
                    fndPState.Value = ""
                    txtPState.Text = ""
                End If
            Else
                txtState.Text = ""
                txtCity.Text = ""
                fndCity.Value = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndCity__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCity._MYValidating
        Try
            fndCity.Value = clsCityMaster.getFinder("STATE_CODE='" + fndState.Value + "'", fndCity.Value, isButtonClicked) 'country_code='" + fndCountry.Value + "' and 
            If clsCommon.myLen(fndCity.Value) > 0 Then
                txtCity.Text = clsCityMaster.GetName(fndCity.Value)
                If (chkSameAdd.Checked = True) Then
                    fndPCity.Value = fndCity.Value
                    txtPCity.Text = txtCity.Text
                Else
                    fndPCity.Value = ""
                    txtPCity.Text = ""
                End If
            Else
                txtCity.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub chkSameAdd_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkSameAdd.ToggleStateChanged
        If (chkSameAdd.Checked = True) Then
            Me.fndPCountry.Value = Me.fndCountry.Value
            Me.fndPState.Value = Me.fndState.Value
            Me.fndPCity.Value = Me.fndCity.Value

            Me.txtPAdd1.Text = Me.txtAdd1.Text
            Me.txtPAdd2.Text = Me.txtAdd2.Text
            Me.txtPAdd3.Text = Me.txtAdd3.Text
            Me.txtPCountry.Text = Me.txtCountry.Text
            Me.txtPState.Text = Me.txtState.Text
            Me.txtPCity.Text = Me.txtCity.Text
            Me.txtPPinNo.Text = Me.txtPinNo.Text

            Me.txtPAdd1.Enabled = False
            Me.txtPAdd2.Enabled = False
            Me.txtPAdd3.Enabled = False
            Me.fndPCountry.Enabled = False
            Me.fndPState.Enabled = False
            Me.fndPCity.Enabled = False
            Me.txtPPinNo.Enabled = False
        Else
            Me.txtPAdd1.Text = ""
            Me.txtPAdd2.Text = ""
            Me.txtPAdd3.Text = ""
            Me.fndPCountry.Value = ""
            Me.txtPCountry.Text = ""
            Me.fndPState.Value = ""
            Me.txtPState.Text = ""
            Me.fndPCity.Value = ""
            Me.txtPCity.Text = ""
            Me.txtPPinNo.Text = ""

            Me.fndPCountry.Value = ""
            Me.fndPState.Value = ""
            Me.fndPCity.Value = ""

            Me.txtPAdd1.Enabled = True
            Me.txtPAdd2.Enabled = True
            Me.txtPAdd3.Enabled = True
            Me.fndPCountry.Enabled = True
            Me.fndPState.Enabled = True
            Me.fndPCity.Enabled = True
            Me.txtPPinNo.Enabled = True
        End If
    End Sub

    Private Sub fndPCountry__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndPCountry._MYValidating
        Try
            fndPCountry.Value = clsCountryMaster.getFinder("", fndPCountry.Value, isButtonClicked)
            If clsCommon.myLen(fndPCountry.Value) > 0 Then
                txtPCountry.Text = clsCountryMaster.GetName(fndPCountry.Value, Nothing)
            Else
                txtPCountry.Text = ""
                fndPState.Value = ""
                txtPState.Text = ""
                txtPCity.Text = ""
                fndPCity.Value = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndPState__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndPState._MYValidating
        Try
            fndPState.Value = clsStateMaster.getFinder("country_code='" + fndPCountry.Value + "'", fndPState.Value, isButtonClicked)
            If clsCommon.myLen(fndPState.Value) > 0 Then
                txtPState.Text = clsStateMaster.GetName(fndPState.Value)
            Else
                txtPState.Text = ""
                txtPCity.Text = ""
                fndPCity.Value = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndPCity__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndPCity._MYValidating
        Try
            fndPCity.Value = clsCityMaster.getFinder("STATE_CODE='" + fndPState.Value + "'", fndPCity.Value, isButtonClicked) 'country_code='" + fndPCountry.Value + "' and 
            If clsCommon.myLen(fndPCity.Value) > 0 Then
                txtPCity.Text = clsCityMaster.GetName(fndPCity.Value)
            Else
                txtPCity.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If AllowToSave() Then SaveConsumerDetails()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteConsumerDetails()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        ImportData()
    End Sub

    Private Sub RadMenuItem3_Click(sender As Object, e As EventArgs) Handles RadMenuItem3.Click
        ExportData()
    End Sub

    Private Sub RadMenuItem4_Click(sender As Object, e As EventArgs) Handles RadMenuItem4.Click
        Me.Close()
    End Sub

    Private Sub ddlProductUsed_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles ddlProductUsed.SelectedIndexChanged
        If (ddlProductUsed.SelectedValue.ToString() = "Other") Then
            MyLabel18.Visible = True
            txtSpecifyOtherProduct.Visible = True
        Else
            MyLabel18.Visible = False
            txtSpecifyOtherProduct.Visible = False
            txtSpecifyOtherProduct.Text = ""
        End If
    End Sub

    Private Sub ddlHowKnow_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles ddlHowKnow.SelectedIndexChanged
        If (ddlHowKnow.SelectedValue.ToString() = "Other") Then
            MyLabel19.Visible = True
            txtSpecifyHowKnow.Visible = True
        Else
            MyLabel19.Visible = False
            txtSpecifyHowKnow.Visible = False
            txtSpecifyHowKnow.Text = ""
        End If
    End Sub

    Private Sub txtAdd1_TextChanged(sender As Object, e As EventArgs) Handles txtAdd1.TextChanged
        If (chkSameAdd.Checked = True) Then
            txtPAdd1.Text = txtAdd1.Text
        Else
            txtPAdd1.Text = ""
        End If
    End Sub

    Private Sub txtAdd2_TextChanged(sender As Object, e As EventArgs) Handles txtAdd2.TextChanged
        If (chkSameAdd.Checked = True) Then
            txtPAdd2.Text = txtAdd2.Text
        Else
            txtPAdd2.Text = ""
        End If
    End Sub

    Private Sub txtAdd3_TextChanged(sender As Object, e As EventArgs) Handles txtAdd3.TextChanged
        If (chkSameAdd.Checked = True) Then
            txtPAdd3.Text = txtAdd3.Text
        Else
            txtPAdd3.Text = ""
        End If
    End Sub

    Private Sub txtPinNo_TextChanged(sender As Object, e As EventArgs) Handles txtPinNo.TextChanged
        If (chkSameAdd.Checked = True) Then
            txtPPinNo.Text = txtPinNo.Text
        Else
            txtPPinNo.Text = ""
        End If
    End Sub
End Class