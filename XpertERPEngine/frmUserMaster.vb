'Developed By -BibhuPrasad Parida
'Database - TSPLERP
'Table - tspl_user_master
'Start Date -
'End Date -
'' work done agaist ticket no KDI/07/06/18-000347 
'Add Mobile No
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports common
Public Class FrmUserMaster
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim PanelCNF As Boolean = False
    Dim ChkSuperUser As Boolean = False
    Dim ChkAutoDepOnPurchaseCycle As Boolean = False
    Dim isCheckCustomerType As Boolean = False
    Public PasswordRules As Boolean = Nothing
    Public CheckPassword As Boolean = Nothing
    Public arrZone As ArrayList
    Public arrCustomerCategory As ArrayList
    Public arrRoute As ArrayList
    Dim UserWiseRouteMapping As Boolean = False
#Region "Constructor"
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
#End Region
#Region "Variables"
    Dim userCode, companyCode As String
    Dim sql As String
    Dim ds As DataSet
    Dim dr As SqlDataReader
    'Const colICustCode As String = "COLICustCode"
    'Const colICustName As String = "COLICustName"
    'Const colIStatus As String = "COLIStatus"

    Const colLineNo As String = "LineNo"
    Const colSelect As String = "Select"
    Const colCustCode As String = "Customer Code"
    Const colCustName As String = "CustName"

    Const colLineNoUser As String = "Line No"
    Const colSelectUser As String = "Select"
    Const colUserCode As String = "User Code"
    Const colUserName As String = "User Name"
    Dim dt As DataTable
    Dim qry As String
    Private isInsideLoadData As Boolean = False
    Private isFromLoad As Boolean = False

#End Region
#Region "Page Load"
    Private Sub SetUserMgmtNew()
        '' Anubhooti 30-July-2014 BM00000003130
        'MyBase.SetUserMgmt(clsUserMgtCode.userMaster)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'common.clsCommon.MyMessageBoxShow("Permission Denied")
            'Me.Close()
            'Exit Sub
        End If
        btnSave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 03/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnSave.Visible = True Then
            menuImport.Enabled = True
            menuExport.Enabled = True
        Else
            menuImport.Enabled = False
            menuExport.Enabled = False
        End If
        '--------------------------------------------------
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmUserMaster_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub

    Private Sub FrmUserMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            btnSave.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Private Sub FrmUserMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        fndUserCode.TabIndex = 1
        Employee_TextChanged()
        usercodeTextChanged()
        btnDelete.Enabled = False
        fndLabel1.Enabled = False
        fndLabel3.Enabled = False
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New Trasnaction")
        loadUserLevels()
        LoadAppUserType()
        fndUserCode.Focus()
        RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Collapsed
        'RadPageViewPage2.Enabled = False
        ' RadPageViewPage2.Visible = False
        isCheckCustomerType = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowLoginType, clsFixedParameterCode.AllowLoginType, Nothing)) = 1, True, False)

        ChkSuperUser = IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AddTypeForUserMaster, clsFixedParameterCode.AddTypeForUserMaster, Nothing)) = "1", True, False)
        UserWiseRouteMapping = IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.UserWiseRouteMapping, clsFixedParameterCode.UserWiseRouteMapping, Nothing)) = "1", True, False)
        If UserWiseRouteMapping = True Then
            GBRoute.Visible = True
        Else
            GBRoute.Visible = False
        End If
        ' glsecurityaccountupdate()
        ChkAutoDepOnPurchaseCycle = IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AutoDepartmentMendatroryFieldOnPurcahseCycle, clsFixedParameterCode.AutoDepartmentMendatroryFieldOnPurcahseCycle, Nothing)) = "1", True, False)
        fndDepartment.MendatroryField = ChkAutoDepOnPurchaseCycle
        If clsCommon.CompairString(objCommonVar.CurrentIndustryType, "D") <> CompairStringResult.Equal Then
            ChkViewMilkReceiptAndSample.Visible = False
        End If
        RadPanel1.Visible = PanelCNF
        PanelCNF = IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowLoginTypeCNFdistributerRetailer, clsFixedParameterCode.AllowLoginTypeCNFdistributerRetailer, Nothing)) = "1", True, False)
        chkLicenceReserved.Visible = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SameuserCanNotloginmultipletimes, clsFixedParameterCode.SameuserCanNotloginmultipletimes, Nothing)) = 1)
        If PanelCNF = True Then
            LoadType()
            fndCustCode.MyReadOnly = False
            fndDisRetailerCode.MyReadOnly = False
            lblDisRetailer.Text = "Distributer"
            CmbLoginType.SelectedValue = "Select"
            RadPanel1.Visible = True
        ElseIf isCheckCustomerType = True Then
            PickDetailsFromPOSGroupMaster()
            fndCustCode.MyReadOnly = False
            fndDisRetailerCode.MyReadOnly = False
            lblDisRetailer.Text = "POS Details"
            CmbLoginType.SelectedValue = "Select"
            RadPanel1.Visible = True
        End If
        LoadAppUserSaleType()
        PasswordRules = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.PasswordRules, clsFixedParameterCode.PasswordRules, Nothing)) = "1", True, False))
        If PasswordRules = True Then
            lblLength.Visible = True
        End If
        If ChkSuperUser = True AndAlso PanelCNF = True Then
            'RadPageViewPage2.Enabled = True
            'RadPageView1.Pages("RadPageViewPage2").Item.Visibility = MyBase.customFieldTabProperty
            RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Visible
            LoadBlankCustomerGrid()
            gvCustomer.Rows.AddNew()
            If CmbLoginType.Text = "SuperUser" Or CmbLoginType.Text = "Super User" Then
                RadPageViewPage2.Enabled = True
            Else
                RadPageViewPage2.Enabled = False
            End If
            rmImportCustomerMapping.Visibility = ElementVisibility.Visible
            RadMenuItem1.Visibility = ElementVisibility.Visible

        Else
            rmImportCustomerMapping.Visibility = ElementVisibility.Collapsed
            RadMenuItem1.Visibility = ElementVisibility.Collapsed
        End If
        LoadBlankUserGrid()
        gvUser.Rows.AddNew()
        txt_Mob_No.Text = ""
        dtInActive.Enabled = False
        dtInActive.Value = connectSql.serverDate()
        RadPageView1.SelectedPage = RadPageViewPage1
        If clsCommon.myLen(Me.Tag) > 0 Then
            fndUserCode.Value = clsCommon.myCstr(Me.Tag)
            usercodeTextChanged()
        End If
    End Sub
#End Region
#Region "KeyPress Events"
    Private Sub UserCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub Employee_keypress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub Label1_keypress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub
    Private Sub Label2_keypress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub
    Private Sub Label3_keypress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub
    Private Sub Label4_keypress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub
#End Region
#Region "TextChanged Event"
    Sub Employee_TextChanged()
        If fndEmployeeCode.Value <> "" Then

            Dim str As String = "select Emp_Code as [Employee Code] from TSPL_EMPLOYEE_MASTER  where Emp_Code='" + fndEmployeeCode.Value + "'"
            Dim strvalue As String = clsDBFuncationality.getSingleValue(str)
            'While dr.Read()
            '    strvalue = dr(0).ToString()
            'End While
            'dr.Close()
            If strvalue <> fndEmployeeCode.Value Then
                ' fndEmployeeCode.Value.Focus()
            Else
                txtEmployeeName.Text = fndEmployeeCode.Tag
                txtPassword.Focus()
            End If

        Else
            txtEmployeeName.Text = ""

        End If

    End Sub
    Private Sub Employee_Text_Changed(ByVal sender As System.Object, ByVal e As System.EventArgs)


    End Sub
    Sub usercodeTextChanged()

        If fndUserCode.Value <> "" Then

            Dim str As String = "select User_Code from TSPL_USER_MASTER where User_Code='" + fndUserCode.Value + "'"
            Dim strvalue As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(str))
            'While dr.Read()
            '    strvalue = dr(0).ToString()
            'End While
            'dr.Close()
            If clsCommon.CompairString(strvalue.ToUpper(), fndUserCode.Value, True) = CompairStringResult.Equal Then
                funfill()
                chkInActive.Enabled = True
            Else
                txtPassword.Text = ""
                txtUserName.Text = ""
                fndEmployeeCode.Value = ""
                txtEmployeeName.Text = ""
                ddlUserType.Text = "----Select----"
                fndLabel1.Value = ""
                fndLabel2.Value = ""
                fndLabel3.Value = ""
                fndLabel4.Value = ""
                fndLabel1.Enabled = False
                chkLicenceReserved.Checked = False
                cmbLevel.SelectedValue = 1
                txtDefaultLocation.Value = ""
                lblLocationName.Text = ""
                fndLabel3.Enabled = False
                'fndLabel4.Enabled = False
                ''fndZone.Value = ""
                ''lblzone.Text = ""
                mulZone.arrValueMember = Nothing
                mulCustomerCategory.arrValueMember = Nothing
                txtRoute.arrValueMember = Nothing
                btnSave.Text = "Save"
                btnDelete.Enabled = False
                If isCheckCustomerType = True Then
                    lblDisRetailer.Text = "POS Details"
                    CmbLoginType.Text = "Select"
                    fndCustCode.Value = ""
                    lblCustCode.Text = ""
                    fndDisRetailerCode.Value = ""
                    lblRetailerCode.Text = ""
                End If
            End If
        Else

        End If

    End Sub
#End Region
#Region "Methods"
    Private Sub funfill()

        Dim str As String = "select USER_NAME ,password ,Emp_Code,Emp_Name,User_Type,Level1_Code,Level2_Code,Level3_Code,Level4_Code, ApprovalLevel,E_Mail, Default_Location, Vendor_Code, Login_Type,Cust_Code,Distributor_Retailer_Code,Segment_code,View_Milk_Receipt_Sample,Department_Head,Licence_Reserved,Mob_No,InActive,isnull(InActive_Date,'') as InActive_Date,User_APP_Type,User_APP_Sale_Type,tspl_user_master.MP_Code,tspl_user_master.HR_Admin from tspl_user_master where  User_Code ='" + fndUserCode.Value + "'"
        Dim dr As DataTable
        dr = clsDBFuncationality.GetDataTable(str)
        For Each row As DataRow In dr.Rows
            txtUserName.Text = row(0).ToString()
            txtPassword.Text = clsCommon.DecryptString(clsCommon.myCstr(row(1)))
            ChkViewMilkReceiptAndSample.Checked = (clsCommon.myCdbl(row("View_Milk_Receipt_Sample")) = 1)
            ChkDepartmentHead.Checked = (clsCommon.myCdbl(row("Department_Head")) = 1)
            chkHRAdmin.Checked = (clsCommon.myCdbl(row("HR_Admin")) = 1)
            fndEmployeeCode.Value = row(2).ToString()
            EmployeeFinder.Value = row(2).ToString()
            If clsCommon.myLen(EmployeeFinder.Value) > 0 Then
                lblEmployeeFinder.Text = clsDBFuncationality.getSingleValue("select Emp_Name from TSPL_EMPLOYEE_MASTER where EMP_CODE = '" + EmployeeFinder.Value + "'")
            End If
            txtEmployeeName.Text = row(3).ToString()
            ddlUserType.Text = row(4).ToString()
            fndVendor.Value = row("Vendor_Code").ToString()
            cmbLevel.SelectedValue = row("ApprovalLevel")
            chkLicenceReserved.Checked = (clsCommon.myCdbl(row("Licence_Reserved")) = 1)

            CboAppUserType.SelectedValue = clsCommon.myCstr(row("User_APP_Type"))
            CmbAppUserSaleType.SelectedValue = clsCommon.myCstr(row("User_APP_Sale_Type"))
            If row(4).ToString() <> "" Then
                fndLabel1.Enabled = True
                fndLabel1.Value = row(4).ToString()
            End If
            If row(5).ToString() <> "" Then
                RadGroupBox1.Enabled = True
                fndLabel2.Value = row(5).ToString()
            End If
            If row(6).ToString() <> "" Then
                fndLabel3.Enabled = True
                fndLabel3.Value = row(6).ToString()

            End If
            If row("Level4_Code").ToString() <> "" Then
                fndLabel4.Enabled = True
                fndLabel4.Value = row("Level4_Code").ToString()
                lblReportingUserName.Text = clsDBFuncationality.getSingleValue("select USER_NAME  from TSPL_USER_MASTER where User_Code = '" + row("Level4_Code").ToString() + "'")

            End If
            txtEmailId.Text = row("E_Mail").ToString()
            txt_Mob_No.Text = clsCommon.myCstr(row("Mob_No").ToString())
            fndDepartment.Value = row("Segment_code").ToString()
            'fndZone.Value = row("Zone_Code").ToString()
            'If clsCommon.myLen(fndZone.Value) > 0 Then
            '    lblzone.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Description from TSPL_ZONE_MASTER where  Zone_Code='" + fndZone.Value + "'"))
            'Else
            '    lblzone.Text = ""
            'End If
            ' Ticket No : UDL/21/05/18-000169 By Prabhakar 
            If clsCommon.myLen(fndDepartment.Value) > 0 Then
                lblDepartmentName.Text = clsDBFuncationality.getSingleValue("select top 1 Description from TSPL_GL_SEGMENT_CODE where Seg_No=3 and Segment_code='" + fndDepartment.Value + "'")
            Else
                lblDepartmentName.Text = ""
            End If
            chkInActive.Checked = IIf(row("InActive") = "Y", True, False)
            Dim date1 As String = clsCommon.myCstr(row("InActive_Date"))
            If date1 <> "" Then

                Me.dtInActive.Value = row("InActive_Date").ToString()
            Else
                dtInActive.Value = dtInActive.MinDate

            End If


            'Add By : Prabhakar'
            If PanelCNF = True Then
                fndCustCode.Value = ""
                lblCustCode.Text = ""
                fndDisRetailerCode.Value = ""
                lblRetailerCode.Text = ""
                CmbLoginType.Text = row("Login_Type").ToString()
                If (CmbLoginType.Text = "CNF" Or CmbLoginType.Text = "Parlor") Then

                    fndCustCode.Value = row("Cust_Code").ToString()
                    lblCustCode.Text = clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" + fndCustCode.Value + "'")
                End If

                If CmbLoginType.Text = "Distributer" Then

                    fndDisRetailerCode.Value = row("Distributor_Retailer_Code").ToString()
                    lblRetailerCode.Text = clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_SECONDARY_CUSTOMER_MASTER where Cust_Code='" + fndDisRetailerCode.Value + "'")
                End If

                If CmbLoginType.Text = "Retailer" Then
                    fndDisRetailerCode.Value = row("Distributor_Retailer_Code").ToString()
                    lblRetailerCode.Text = clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_SECONDARY_CUSTOMER_MASTER where Cust_Code='" + fndDisRetailerCode.Value + "'")
                End If

            End If

            If isCheckCustomerType = True Then
                CmbLoginType.Text = row("Login_Type").ToString()
                If (CmbLoginType.Text = "CNF" Or CmbLoginType.Text = "Parlor") Then

                    fndCustCode.Value = row("Cust_Code").ToString()
                    lblCustCode.Text = clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" + fndCustCode.Value + "'")
                    fndDisRetailerCode.Enabled = False
                    fndCustCode.Enabled = True
                    fndDisRetailerCode.Value = ""
                    lblRetailerCode.Text = ""
                End If

                If CmbLoginType.Text = "Distributer" Then

                    fndDisRetailerCode.Value = row("Distributor_Retailer_Code").ToString()
                    lblRetailerCode.Text = clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_SECONDARY_CUSTOMER_MASTER where Cust_Code='" + fndDisRetailerCode.Value + "'")
                    fndCustCode.Enabled = False
                    fndDisRetailerCode.Enabled = True
                    fndCustCode.Value = ""
                    lblCustCode.Text = ""
                End If
                If CmbLoginType.Text = "Retailer" Then
                    fndDisRetailerCode.Value = row("Distributor_Retailer_Code").ToString()
                    lblRetailerCode.Text = clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_SECONDARY_CUSTOMER_MASTER where Cust_Code='" + fndDisRetailerCode.Value + "'")
                    fndCustCode.Enabled = False
                    fndDisRetailerCode.Enabled = True
                    fndCustCode.Value = ""
                    lblCustCode.Text = ""
                End If

            End If
            txtDefaultLocation.Value = clsCommon.myCstr(row("Default_Location"))
            lblLocationName.Text = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_Location_Master where Location_Code='" + txtDefaultLocation.Value + "'")
            lblVendorName.Text = clsDBFuncationality.getSingleValue("select Vendor_Name from TSPL_VENDOR_MASTER where Vendor_Code='" + fndVendor.Value + "'")

            txtMP.Value = clsCommon.myCstr(row("MP_Code"))
            lblMP.Text = clsMpMaster.GetName(txtMP.Value, Nothing)

            btnSave.Text = "Update"
            btnDelete.Enabled = True
        Next

        If ChkSuperUser = True AndAlso PanelCNF = True Then

            If CmbLoginType.Text = "SuperUser" Or CmbLoginType.Text = "Super User" Then
                RadPageViewPage2.Enabled = True
                FillCustomerGrid(fndUserCode.Value)
            Else
                RadPageViewPage2.Enabled = False
            End If



        End If
        FillUserGrid(fndUserCode.Value)

        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        '===========================
        Dim arrZone1 As ArrayList = Nothing
        Dim qry As String = "select TSPL_USER_CUSTOMER_ZONE.Zone_Code from TSPL_USER_CUSTOMER_ZONE where TSPL_USER_CUSTOMER_ZONE.User_Code='" + fndUserCode.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arrZone1 = New ArrayList()
            For Each drZone As DataRow In dt.Rows
                arrZone1.Add(clsCommon.myCstr(drZone("Zone_Code")))
            Next
        End If
        mulZone.arrValueMember = arrZone1

        '---------------------------------

        Dim arrCustCategory1 As ArrayList = Nothing
        qry = "select TSPL_USER_CUSTOMER_CATEGORY.Customer_Category as  Code from TSPL_USER_CUSTOMER_CATEGORY where TSPL_USER_CUSTOMER_CATEGORY.User_Code='" + fndUserCode.Value + "'"
        Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0 Then
            arrCustCategory1 = New ArrayList()
            For Each drCustCategory As DataRow In dt2.Rows
                arrCustCategory1.Add(clsCommon.myCstr(drCustCategory("Code")))
            Next
        End If
        mulCustomerCategory.arrValueMember = arrCustCategory1

        Dim arrRoute1 As ArrayList = Nothing
        qry = "select TSPL_User_Route_Mapping.Route_No from TSPL_User_Route_Mapping where TSPL_User_Route_Mapping.User_Code='" + fndUserCode.Value + "'"
        Dim dt3 As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt3 IsNot Nothing AndAlso dt3.Rows.Count > 0 Then
            arrRoute1 = New ArrayList()
            For Each drRoute As DataRow In dt3.Rows
                arrRoute1.Add(clsCommon.myCstr(drRoute("Route_No")))
            Next
        End If
        txtRoute.arrValueMember = arrRoute1

        '==========================
    End Sub

    Private Sub funInsert()
        Try '

            If IsCorrectUSer(Nothing) Then
                If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False)) Then
                    Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_USER_MASTER where User_Code='" & fndUserCode.Value & "'")
                    If ChkNewEntry = 0 Then
                        fndUserCode.Value = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"), clsDocType.UserMaster, "", "")
                        If clsCommon.myLen(fndUserCode.Value) <= 0 Then
                            Throw New Exception("Error in Code Generation")
                        End If
                    End If
                End If
                Dim str As String = ddlUserType.Text
                If str = "----Select----" Then
                    'connectSql.RunSp("sp_tspl_user_master_insert", New SqlParameter("@Usercode", fndUserCode.Value), New SqlParameter("@UserName", txtUserName.Text), New SqlParameter("@EmployeeCode", fndEmployeeCode.Value), New SqlParameter("@EmployeeName", txtEmployeeName.Text), New SqlParameter("@Password", txtPassword.Text), New SqlParameter("@UserType", ddlUserType.Text), New SqlParameter("@Level1", fndLabel1.Value), New SqlParameter("@Level2", fndLabel2.Value), New SqlParameter("@Level3", fndLabel3.Value), New SqlParameter("@Level4", fndLabel4.Value), New SqlParameter("@Createdby", userCode), New SqlParameter("@Createddate", connectSql.serverDate()), New SqlParameter("@Modifiedby", userCode), New SqlParameter("@Modifieddate", connectSql.serverDate()), New SqlParameter("@CompCode", companyCode))
                    If (EmployeeFinder.Value IsNot Nothing AndAlso clsCommon.myLen(EmployeeFinder.Value) > 0) Or (clsCommon.myLen(fndEmployeeCode.Value) <= 0) Then
                        fndEmployeeCode.Value = EmployeeFinder.Value
                    End If
                    clsDBFuncationality.UpdateInAllDatabase("sp_tspl_user_master_insert", New SqlParameter("@Usercode", fndUserCode.Value), New SqlParameter("@UserName", txtUserName.Text), New SqlParameter("@EmployeeCode", fndEmployeeCode.Value), New SqlParameter("@EmployeeName", txtEmployeeName.Text), New SqlParameter("@Password", clsCommon.EncryptString(txtPassword.Text)), New SqlParameter("@UserType", ddlUserType.Text), New SqlParameter("@Level1", fndLabel1.Value), New SqlParameter("@Level2", fndLabel2.Value), New SqlParameter("@Level3", fndLabel3.Value), New SqlParameter("@Level4", fndLabel4.Value), New SqlParameter("@Createdby", userCode), New SqlParameter("@Createddate", connectSql.serverDate()), New SqlParameter("@Modifiedby", userCode), New SqlParameter("@Modifieddate", connectSql.serverDate()), New SqlParameter("@CompCode", companyCode), New SqlParameter("@ApprovalLevel", cmbLevel.SelectedValue))
                    'myMessages.insert()
                    btnSave.Text = "Update"
                    btnDelete.Enabled = True
                End If
                If str = "Level1" Then
                    Try
                        clsDBFuncationality.UpdateInAllDatabase("sp_tspl_user_master_insert", New SqlParameter("@Usercode", fndUserCode.Value), New SqlParameter("@UserName", txtUserName.Text), New SqlParameter("@EmployeeCode", fndEmployeeCode.Value), New SqlParameter("@EmployeeName", txtEmployeeName.Text), New SqlParameter("@Password", clsCommon.EncryptString(txtPassword.Text)), New SqlParameter("@UserType", ddlUserType.Text), New SqlParameter("@Level1", fndLabel1.Value), New SqlParameter("@Level2", fndLabel2.Value), New SqlParameter("@Level3", fndLabel3.Value), New SqlParameter("@Level4", fndLabel4.Value), New SqlParameter("@Createdby", userCode), New SqlParameter("@Createddate", connectSql.serverDate()), New SqlParameter("@Modifiedby", userCode), New SqlParameter("@Modifieddate", connectSql.serverDate()), New SqlParameter("@CompCode", companyCode), New SqlParameter("@ApprovalLevel", cmbLevel.SelectedValue))
                        myMessages.insert()
                        btnSave.Text = "Update"
                        btnDelete.Enabled = True
                    Catch ex As Exception
                        myMessages.myExceptions(ex)
                    End Try
                End If

                If str = "Level2" Then
                    If fndLabel1.Value = "" Then
                        common.clsCommon.MyMessageBoxShow("Level1 cannot be blank.")
                    Else
                        Try
                            clsDBFuncationality.UpdateInAllDatabase("sp_tspl_user_master_insert", New SqlParameter("@Usercode", fndUserCode.Value), New SqlParameter("@UserName", txtUserName.Text), New SqlParameter("@EmployeeCode", fndEmployeeCode.Value), New SqlParameter("@EmployeeName", txtEmployeeName.Text), New SqlParameter("@Password", clsCommon.EncryptString(txtPassword.Text)), New SqlParameter("@UserType", ddlUserType.Text), New SqlParameter("@Level1", fndLabel1.Value), New SqlParameter("@Level2", fndLabel2.Value), New SqlParameter("@Level3", fndLabel3.Value), New SqlParameter("@Level4", fndLabel4.Value), New SqlParameter("@Createdby", userCode), New SqlParameter("@Createddate", connectSql.serverDate()), New SqlParameter("@Modifiedby", userCode), New SqlParameter("@Modifieddate", connectSql.serverDate()), New SqlParameter("@CompCode", companyCode), New SqlParameter("@ApprovalLevel", cmbLevel.SelectedValue))
                            myMessages.insert()
                            btnSave.Text = "Update"
                            btnDelete.Enabled = True
                        Catch ex As Exception
                            myMessages.myExceptions(ex)
                        End Try
                    End If

                ElseIf str = "Level3" Then
                    If fndLabel1.Value = "" Then
                        common.clsCommon.MyMessageBoxShow("Level1 cannot be blank.")
                    ElseIf fndLabel2.Value = "" Then
                        common.clsCommon.MyMessageBoxShow("Level2 cannot be blank.")
                    Else
                        Try
                            clsDBFuncationality.UpdateInAllDatabase("sp_tspl_user_master_insert", New SqlParameter("@Usercode", fndUserCode.Value), New SqlParameter("@UserName", txtUserName.Text), New SqlParameter("@EmployeeCode", fndEmployeeCode.Value), New SqlParameter("@EmployeeName", txtEmployeeName.Text), New SqlParameter("@Password", clsCommon.EncryptString(txtPassword.Text)), New SqlParameter("@UserType", ddlUserType.Text), New SqlParameter("@Level1", fndLabel1.Value), New SqlParameter("@Level2", fndLabel2.Value), New SqlParameter("@Level3", fndLabel3.Value), New SqlParameter("@Level4", fndLabel4.Value), New SqlParameter("@Createdby", userCode), New SqlParameter("@Createddate", connectSql.serverDate()), New SqlParameter("@Modifiedby", userCode), New SqlParameter("@Modifieddate", connectSql.serverDate()), New SqlParameter("@CompCode", companyCode), New SqlParameter("@ApprovalLevel", cmbLevel.SelectedValue))
                            myMessages.insert()
                            btnSave.Text = "Update"
                            btnDelete.Enabled = True
                        Catch ex As Exception
                            myMessages.myExceptions(ex)
                        End Try
                    End If
                ElseIf str = "Level4" Then
                    If fndLabel1.Value = "" Then
                        common.clsCommon.MyMessageBoxShow("Level1 cannot be blank.")
                    ElseIf fndLabel2.Value = "" Then
                        common.clsCommon.MyMessageBoxShow("Level2 cannot be blank.")
                    ElseIf fndLabel3.Value = "" Then
                        common.clsCommon.MyMessageBoxShow("Level3 cannot be blank.")
                    Else
                        Try
                            clsDBFuncationality.UpdateInAllDatabase("sp_tspl_user_master_insert", New SqlParameter("@Usercode", fndUserCode.Value), New SqlParameter("@UserName", txtUserName.Text), New SqlParameter("@EmployeeCode", fndEmployeeCode.Value), New SqlParameter("@EmployeeName", txtEmployeeName.Text), New SqlParameter("@Password", clsCommon.EncryptString(txtPassword.Text)), New SqlParameter("@UserType", ddlUserType.Text), New SqlParameter("@Level1", fndLabel1.Value), New SqlParameter("@Level2", fndLabel2.Value), New SqlParameter("@Level3", fndLabel3.Value), New SqlParameter("@Level4", fndLabel4.Value), New SqlParameter("@Createdby", userCode), New SqlParameter("@Createddate", connectSql.serverDate()), New SqlParameter("@Modifiedby", userCode), New SqlParameter("@Modifieddate", connectSql.serverDate()), New SqlParameter("@CompCode", companyCode), New SqlParameter("@ApprovalLevel", cmbLevel.SelectedValue))
                            myMessages.insert()
                            btnSave.Text = "Update"
                            btnDelete.Enabled = True
                            btnDelete.Enabled = True
                        Catch ex As Exception
                            myMessages.myExceptions(ex)
                        End Try
                    End If
                ElseIf str = "Level5" Then


                    If fndLabel1.Value = "" Then
                        common.clsCommon.MyMessageBoxShow("Level1 cannot be blank.")
                    ElseIf fndLabel2.Value = "" Then
                        common.clsCommon.MyMessageBoxShow("Level2 cannot be blank.")
                    ElseIf fndLabel3.Value = "" Then
                        common.clsCommon.MyMessageBoxShow("Level3 cannot be blank.")
                    ElseIf fndLabel4.Value = "" Then
                        common.clsCommon.MyMessageBoxShow("Level4 cannot be blank.")
                    Else
                        Try
                            clsDBFuncationality.UpdateInAllDatabase("sp_tspl_user_master_insert", New SqlParameter("@Usercode", fndUserCode.Value), New SqlParameter("@UserName", txtUserName.Text), New SqlParameter("@EmployeeCode", fndEmployeeCode.Value), New SqlParameter("@EmployeeName", txtEmployeeName.Text), New SqlParameter("@Password", clsCommon.EncryptString(txtPassword.Text)), New SqlParameter("@UserType", ddlUserType.Text), New SqlParameter("@Level1", fndLabel1.Value), New SqlParameter("@Level2", fndLabel2.Value), New SqlParameter("@Level3", fndLabel3.Value), New SqlParameter("@Level4", fndLabel4.Value), New SqlParameter("@Createdby", userCode), New SqlParameter("@Createddate", connectSql.serverDate()), New SqlParameter("@Modifiedby", userCode), New SqlParameter("@Modifieddate", connectSql.serverDate()), New SqlParameter("@CompCode", companyCode), New SqlParameter("@ApprovalLevel", cmbLevel.SelectedValue))
                            myMessages.insert()
                            btnSave.Text = "Update"
                            btnDelete.Enabled = True
                            btnDelete.Enabled = True
                        Catch ex As Exception
                            myMessages.myExceptions(ex)
                        End Try
                    End If
                End If


                clsDBFuncationality.ExecuteNonQuery(" update TSPL_USER_MASTER set Mob_No = '" + txt_Mob_No.Text + "',E_Mail = '" + txtEmailId.Text + "',Licence_Reserved=" + IIf(chkLicenceReserved.Checked, "1", "0") + " where User_Code ='" + fndUserCode.Value + "'")

                If clsCommon.myLen(fndDepartment.Value) > 0 Then
                    clsDBFuncationality.ExecuteNonQuery(" update TSPL_USER_MASTER set Segment_code = '" + fndDepartment.Value + "' where User_Code ='" + fndUserCode.Value + "'")
                End If
                'If clsCommon.myLen(fndZone.Value) > 0 Then
                '    clsDBFuncationality.ExecuteNonQuery(" update TSPL_USER_MASTER set Zone_code = '" + fndZone.Value + "' where User_Code ='" + fndUserCode.Value + "'")
                'End If
                If (clsCommon.myLen(txtDefaultLocation.Value) > 0) Then
                    clsDBFuncationality.ExecuteNonQuery(" update TSPL_USER_MASTER set Default_Location = '" + txtDefaultLocation.Value + "' where User_Code ='" + fndUserCode.Value + "'")
                    ''RICHA AGARWAL 12/05/2015
                    Dim SegCodeQrY As String = "SELECT Loc_Segment_Code FROM TSPL_LOCATION_MASTER WHERE Location_Code ='" & txtDefaultLocation.Value & "' "
                    Dim Gl_Segment As String = clsDBFuncationality.getSingleValue(SegCodeQrY)
                    connectSql.RunSpTransaction(Nothing, "SP_TSPL_GL_SEGMENT_PERMISSION_INSERT", New SqlParameter("@usercode", fndUserCode.Value), New SqlParameter("@glsegment", "7"), New SqlParameter("@segmentcode", Gl_Segment), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifydate", connectSql.serverDate()), New SqlParameter("@modifyby", userCode), New SqlParameter("@compcode", companyCode), New SqlParameter("@Default_Segment", "Y"))
                    ''---------------------------
                End If
                If clsCommon.myLen(fndVendor.Value) > 0 Then
                    clsDBFuncationality.ExecuteNonQuery(" update TSPL_USER_MASTER set Vendor_Code = '" + fndVendor.Value + "' where User_Code ='" + fndUserCode.Value + "'")
                End If
                'Prabhakar Ticket : BM00000009802' 
                If PanelCNF = True AndAlso (CmbLoginType.Text = "CNF" Or CmbLoginType.Text = "Parlor" Or CmbLoginType.Text = "Distributer" Or CmbLoginType.Text = "Retailer" Or CmbLoginType.Text = "Driver" Or CmbLoginType.Text = "Super User") Then
                    If (CmbLoginType.Text = "CNF" Or CmbLoginType.Text = "Parlor") Then
                        clsDBFuncationality.ExecuteNonQuery(" update TSPL_USER_MASTER set Login_Type = '" + CmbLoginType.SelectedValue + "',Cust_Code='" + fndCustCode.Value + "' where User_Code ='" + fndUserCode.Value + "'")
                    End If
                    If CmbLoginType.Text = "Distributer" Or CmbLoginType.Text = "Retailer" Then
                        clsDBFuncationality.ExecuteNonQuery(" update TSPL_USER_MASTER set Login_Type = '" + CmbLoginType.SelectedValue + "',Distributor_Retailer_Code='" + fndDisRetailerCode.Value + "' where User_Code ='" + fndUserCode.Value + "'")
                    End If
                    If CmbLoginType.Text = "Super User" Or CmbLoginType.Text = "Driver" Then
                        clsDBFuncationality.ExecuteNonQuery(" update TSPL_USER_MASTER set Login_Type = '" + CmbLoginType.SelectedValue + "' where User_Code ='" + fndUserCode.Value + "'")
                    End If
                    ' clsDBFuncationality.ExecuteNonQuery(" update TSPL_USER_MASTER set Login_Type = '" + CmbLoginType.SelectedValue + "',Cust_Code='" + fndCustCode.Value + "',Distributor_Retailer_Code='" + fndDisRetailerCode.Value + "' where User_Code ='" + fndUserCode.Value + "'")
                End If

                If isCheckCustomerType = True Then
                    If MatchLevel() = 1 Then
                        clsDBFuncationality.ExecuteNonQuery(" update TSPL_USER_MASTER set Login_Type = '" + CmbLoginType.SelectedValue + "',Cust_Code='" + fndCustCode.Value + "' where User_Code ='" + fndUserCode.Value + "'")
                    End If
                    If MatchLevel() > 1 Then
                        clsDBFuncationality.ExecuteNonQuery(" update TSPL_USER_MASTER set Login_Type = '" + CmbLoginType.SelectedValue + "',Distributor_Retailer_Code='" + fndDisRetailerCode.Value + "' where User_Code ='" + fndUserCode.Value + "'")
                    End If
                End If
            End If
            updateExtraColumns()

            If ChkSuperUser = True AndAlso PanelCNF = True Then
                SaveCustomerMapping()
            End If
            SaveUserMapping()



            '===============================================
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_USER_CUSTOMER_ZONE  where User_Code ='" + fndUserCode.Value + "' ")
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_USER_CUSTOMER_CATEGORY  where User_Code ='" + fndUserCode.Value + "' ")
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_User_Route_Mapping  where User_Code ='" + fndUserCode.Value + "' ")
            arrZone = mulZone.arrValueMember
            If (arrZone IsNot Nothing AndAlso arrZone.Count > 0) Then
                For Each strZoneCode As String In arrZone
                    Dim collZone As New Hashtable()
                    Dim strTRCode As String = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"), clsDocType.Detail, clsDocTransactionType.Detail, "")
                    clsCommon.AddColumnsForChange(collZone, "TR_Code", strTRCode)
                    clsCommon.AddColumnsForChange(collZone, "User_Code", fndUserCode.Value)
                    clsCommon.AddColumnsForChange(collZone, "Zone_Code", strZoneCode)
                    clsCommonFunctionality.UpdateDataTable(collZone, "TSPL_USER_CUSTOMER_ZONE", OMInsertOrUpdate.Insert, "")
                Next
            End If

            arrCustomerCategory = mulCustomerCategory.arrValueMember
            If (arrCustomerCategory IsNot Nothing AndAlso arrCustomerCategory.Count > 0) Then
                For Each strCustomerCategory As String In arrCustomerCategory
                    Dim collCustCategory As New Hashtable()
                    Dim strTRCode As String = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"), clsDocType.Detail, clsDocTransactionType.Detail, "")
                    clsCommon.AddColumnsForChange(collCustCategory, "TR_Code", strTRCode)
                    clsCommon.AddColumnsForChange(collCustCategory, "User_Code", fndUserCode.Value)
                    clsCommon.AddColumnsForChange(collCustCategory, "Customer_Category", strCustomerCategory)
                    clsCommonFunctionality.UpdateDataTable(collCustCategory, "TSPL_USER_CUSTOMER_CATEGORY", OMInsertOrUpdate.Insert, "")
                Next
            End If

            arrRoute = txtRoute.arrValueMember
            If (arrRoute IsNot Nothing AndAlso arrRoute.Count > 0) Then
                For Each strRouteCode As String In arrRoute
                    Dim collRoute As New Hashtable()
                    Dim strTRCode As String = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"), clsDocType.Detail, clsDocTransactionType.Detail, "")
                    clsCommon.AddColumnsForChange(collRoute, "TR_Code", strTRCode)
                    clsCommon.AddColumnsForChange(collRoute, "User_Code", fndUserCode.Value)
                    clsCommon.AddColumnsForChange(collRoute, "Route_No", strRouteCode)
                    clsCommonFunctionality.UpdateDataTable(collRoute, "TSPL_User_Route_Mapping", OMInsertOrUpdate.Insert, "")
                Next
            End If


            '===============================================

            myMessages.insert()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub


    Sub updateExtraColumns()
        Dim coll As New Hashtable()
        clsCommon.AddColumnsForChange(coll, "EmployeeCode", EmployeeFinder.Value, True)
        clsCommon.AddColumnsForChange(coll, "Department_Head", IIf(ChkDepartmentHead.Checked, 1, 0))
        clsCommon.AddColumnsForChange(coll, "HR_Admin", IIf(chkHRAdmin.Checked, 1, 0))
        clsCommon.AddColumnsForChange(coll, "View_Milk_Receipt_Sample", IIf(ChkViewMilkReceiptAndSample.Checked, 1, 0))
        clsCommon.AddColumnsForChange(coll, "InActive", IIf(chkInActive.Checked, "Y", "N"))
        clsCommon.AddColumnsForChange(coll, "InActive_Date", IIf(chkInActive.Checked, clsCommon.GetPrintDate(dtInActive.Value, "dd/MM/yyyy"), Nothing), True)
        clsCommon.AddColumnsForChange(coll, "User_APP_Type", CboAppUserType.SelectedValue, True)
        clsCommon.AddColumnsForChange(coll, "User_APP_Sale_Type", CmbAppUserSaleType.SelectedValue, True)
        clsCommon.AddColumnsForChange(coll, "MP_Code", txtMP.Value, True)
        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_USER_MASTER", OMInsertOrUpdate.Update, "User_Code='" + fndUserCode.Value + "'")
    End Sub

    'Private Sub funInsert()
    '    Try '
    '        If IsCorrectUSer(Nothing) Then
    '            Dim str As String = ddlUserType.Text
    '            If str = "----Select----" Then
    '                'connectSql.RunSp("sp_tspl_user_master_insert", New SqlParameter("@Usercode", fndUserCode.Value), New SqlParameter("@UserName", txtUserName.Text), New SqlParameter("@EmployeeCode", fndEmployeeCode.Value), New SqlParameter("@EmployeeName", txtEmployeeName.Text), New SqlParameter("@Password", txtPassword.Text), New SqlParameter("@UserType", ddlUserType.Text), New SqlParameter("@Level1", fndLabel1.Value), New SqlParameter("@Level2", fndLabel2.Value), New SqlParameter("@Level3", fndLabel3.Value), New SqlParameter("@Level4", fndLabel4.Value), New SqlParameter("@Createdby", userCode), New SqlParameter("@Createddate", connectSql.serverDate()), New SqlParameter("@Modifiedby", userCode), New SqlParameter("@Modifieddate", connectSql.serverDate()), New SqlParameter("@CompCode", companyCode))
    '                clsDBFuncationality.UpdateInAllDatabase("sp_tspl_user_master_insert", New SqlParameter("@Usercode", fndUserCode.Value), New SqlParameter("@UserName", txtUserName.Text), New SqlParameter("@EmployeeCode", fndEmployeeCode.Value), New SqlParameter("@EmployeeName", txtEmployeeName.Text), New SqlParameter("@Password", txtPassword.Text), New SqlParameter("@UserType", ddlUserType.Text), New SqlParameter("@Level1", fndLabel1.Value), New SqlParameter("@Level2", fndLabel2.Value), New SqlParameter("@Level3", fndLabel3.Value), New SqlParameter("@Level4", fndLabel4.Value), New SqlParameter("@Createdby", userCode), New SqlParameter("@Createddate", connectSql.serverDate()), New SqlParameter("@Modifiedby", userCode), New SqlParameter("@Modifieddate", connectSql.serverDate()), New SqlParameter("@CompCode", companyCode), New SqlParameter("@ApprovalLevel", cmbLevel.SelectedValue))
    '                myMessages.insert()
    '                btnSave.Text = "Update"
    '                btnDelete.Enabled = True
    '            End If
    '            If str = "Level1" Then
    '                Try
    '                    clsDBFuncationality.UpdateInAllDatabase("sp_tspl_user_master_insert", New SqlParameter("@Usercode", fndUserCode.Value), New SqlParameter("@UserName", txtUserName.Text), New SqlParameter("@EmployeeCode", fndEmployeeCode.Value), New SqlParameter("@EmployeeName", txtEmployeeName.Text), New SqlParameter("@Password", txtPassword.Text), New SqlParameter("@UserType", ddlUserType.Text), New SqlParameter("@Level1", fndLabel1.Value), New SqlParameter("@Level2", fndLabel2.Value), New SqlParameter("@Level3", fndLabel3.Value), New SqlParameter("@Level4", fndLabel4.Value), New SqlParameter("@Createdby", userCode), New SqlParameter("@Createddate", connectSql.serverDate()), New SqlParameter("@Modifiedby", userCode), New SqlParameter("@Modifieddate", connectSql.serverDate()), New SqlParameter("@CompCode", companyCode), New SqlParameter("@ApprovalLevel", cmbLevel.SelectedValue))
    '                    myMessages.insert()
    '                    btnSave.Text = "Update"
    '                    btnDelete.Enabled = True
    '                Catch ex As Exception
    '                    myMessages.myExceptions(ex)
    '                End Try
    '            End If

    '            If str = "Level2" Then
    '                If fndLabel1.Value = "" Then
    '                    common.clsCommon.MyMessageBoxShow("Level1 cannot be blank.")
    '                Else
    '                    Try
    '                        clsDBFuncationality.UpdateInAllDatabase("sp_tspl_user_master_insert", New SqlParameter("@Usercode", fndUserCode.Value), New SqlParameter("@UserName", txtUserName.Text), New SqlParameter("@EmployeeCode", fndEmployeeCode.Value), New SqlParameter("@EmployeeName", txtEmployeeName.Text), New SqlParameter("@Password", txtPassword.Text), New SqlParameter("@UserType", ddlUserType.Text), New SqlParameter("@Level1", fndLabel1.Value), New SqlParameter("@Level2", fndLabel2.Value), New SqlParameter("@Level3", fndLabel3.Value), New SqlParameter("@Level4", fndLabel4.Value), New SqlParameter("@Createdby", userCode), New SqlParameter("@Createddate", connectSql.serverDate()), New SqlParameter("@Modifiedby", userCode), New SqlParameter("@Modifieddate", connectSql.serverDate()), New SqlParameter("@CompCode", companyCode), New SqlParameter("@ApprovalLevel", cmbLevel.SelectedValue))
    '                        myMessages.insert()
    '                        btnSave.Text = "Update"
    '                        btnDelete.Enabled = True
    '                    Catch ex As Exception
    '                        myMessages.myExceptions(ex)
    '                    End Try
    '                End If

    '            ElseIf str = "Level3" Then
    '                If fndLabel1.Value = "" Then
    '                    common.clsCommon.MyMessageBoxShow("Level1 cannot be blank.")
    '                ElseIf fndLabel2.Value = "" Then
    '                    common.clsCommon.MyMessageBoxShow("Level2 cannot be blank.")
    '                Else
    '                    Try
    '                        clsDBFuncationality.UpdateInAllDatabase("sp_tspl_user_master_insert", New SqlParameter("@Usercode", fndUserCode.Value), New SqlParameter("@UserName", txtUserName.Text), New SqlParameter("@EmployeeCode", fndEmployeeCode.Value), New SqlParameter("@EmployeeName", txtEmployeeName.Text), New SqlParameter("@Password", txtPassword.Text), New SqlParameter("@UserType", ddlUserType.Text), New SqlParameter("@Level1", fndLabel1.Value), New SqlParameter("@Level2", fndLabel2.Value), New SqlParameter("@Level3", fndLabel3.Value), New SqlParameter("@Level4", fndLabel4.Value), New SqlParameter("@Createdby", userCode), New SqlParameter("@Createddate", connectSql.serverDate()), New SqlParameter("@Modifiedby", userCode), New SqlParameter("@Modifieddate", connectSql.serverDate()), New SqlParameter("@CompCode", companyCode), New SqlParameter("@ApprovalLevel", cmbLevel.SelectedValue))
    '                        myMessages.insert()
    '                        btnSave.Text = "Update"
    '                        btnDelete.Enabled = True
    '                    Catch ex As Exception
    '                        myMessages.myExceptions(ex)
    '                    End Try
    '                End If
    '            ElseIf str = "Level4" Then
    '                If fndLabel1.Value = "" Then
    '                    common.clsCommon.MyMessageBoxShow("Level1 cannot be blank.")
    '                ElseIf fndLabel2.Value = "" Then
    '                    common.clsCommon.MyMessageBoxShow("Level2 cannot be blank.")
    '                ElseIf fndLabel3.Value = "" Then
    '                    common.clsCommon.MyMessageBoxShow("Level3 cannot be blank.")
    '                Else
    '                    Try
    '                        clsDBFuncationality.UpdateInAllDatabase("sp_tspl_user_master_insert", New SqlParameter("@Usercode", fndUserCode.Value), New SqlParameter("@UserName", txtUserName.Text), New SqlParameter("@EmployeeCode", fndEmployeeCode.Value), New SqlParameter("@EmployeeName", txtEmployeeName.Text), New SqlParameter("@Password", txtPassword.Text), New SqlParameter("@UserType", ddlUserType.Text), New SqlParameter("@Level1", fndLabel1.Value), New SqlParameter("@Level2", fndLabel2.Value), New SqlParameter("@Level3", fndLabel3.Value), New SqlParameter("@Level4", fndLabel4.Value), New SqlParameter("@Createdby", userCode), New SqlParameter("@Createddate", connectSql.serverDate()), New SqlParameter("@Modifiedby", userCode), New SqlParameter("@Modifieddate", connectSql.serverDate()), New SqlParameter("@CompCode", companyCode), New SqlParameter("@ApprovalLevel", cmbLevel.SelectedValue))
    '                        myMessages.insert()
    '                        btnSave.Text = "Update"
    '                        btnDelete.Enabled = True
    '                        btnDelete.Enabled = True
    '                    Catch ex As Exception
    '                        myMessages.myExceptions(ex)
    '                    End Try
    '                End If
    '            ElseIf str = "Level5" Then


    '                If fndLabel1.Value = "" Then
    '                    common.clsCommon.MyMessageBoxShow("Level1 cannot be blank.")
    '                ElseIf fndLabel2.Value = "" Then
    '                    common.clsCommon.MyMessageBoxShow("Level2 cannot be blank.")
    '                ElseIf fndLabel3.Value = "" Then
    '                    common.clsCommon.MyMessageBoxShow("Level3 cannot be blank.")
    '                ElseIf fndLabel4.Value = "" Then
    '                    common.clsCommon.MyMessageBoxShow("Level4 cannot be blank.")
    '                Else
    '                    Try
    '                        clsDBFuncationality.UpdateInAllDatabase("sp_tspl_user_master_insert", New SqlParameter("@Usercode", fndUserCode.Value), New SqlParameter("@UserName", txtUserName.Text), New SqlParameter("@EmployeeCode", fndEmployeeCode.Value), New SqlParameter("@EmployeeName", txtEmployeeName.Text), New SqlParameter("@Password", txtPassword.Text), New SqlParameter("@UserType", ddlUserType.Text), New SqlParameter("@Level1", fndLabel1.Value), New SqlParameter("@Level2", fndLabel2.Value), New SqlParameter("@Level3", fndLabel3.Value), New SqlParameter("@Level4", fndLabel4.Value), New SqlParameter("@Createdby", userCode), New SqlParameter("@Createddate", connectSql.serverDate()), New SqlParameter("@Modifiedby", userCode), New SqlParameter("@Modifieddate", connectSql.serverDate()), New SqlParameter("@CompCode", companyCode), New SqlParameter("@ApprovalLevel", cmbLevel.SelectedValue))
    '                        myMessages.insert()
    '                        btnSave.Text = "Update"
    '                        btnDelete.Enabled = True
    '                        btnDelete.Enabled = True
    '                    Catch ex As Exception
    '                        myMessages.myExceptions(ex)
    '                    End Try
    '                End If
    '            End If

    '            If clsCommon.myLen(txtEmailId.Text) > 0 Then
    '                clsDBFuncationality.ExecuteNonQuery(" update TSPL_USER_MASTER set E_Mail = '" + txtEmailId.Text + "' where User_Code ='" + fndUserCode.Value + "'")
    '            End If
    '            If (clsCommon.myLen(txtDefaultLocation.Value) > 0) Then
    '                clsDBFuncationality.ExecuteNonQuery(" update TSPL_USER_MASTER set Default_Location = '" + txtDefaultLocation.Value + "' where User_Code ='" + fndUserCode.Value + "'")
    '                ''RICHA AGARWAL 12/05/2015
    '                Dim SegCodeQrY As String = "SELECT Loc_Segment_Code FROM TSPL_LOCATION_MASTER WHERE Location_Code ='" & txtDefaultLocation.Value & "' "
    '                Dim Gl_Segment As String = clsDBFuncationality.getSingleValue(SegCodeQrY)
    '                connectSql.RunSpTransaction(Nothing, "SP_TSPL_GL_SEGMENT_PERMISSION_INSERT", New SqlParameter("@usercode", fndUserCode.Value), New SqlParameter("@glsegment", "7"), New SqlParameter("@segmentcode", Gl_Segment), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifydate", connectSql.serverDate()), New SqlParameter("@modifyby", userCode), New SqlParameter("@compcode", companyCode), New SqlParameter("@Default_Segment", "Y"))
    '                ''---------------------------
    '            End If
    '            If clsCommon.myLen(fndVendor.Value) > 0 Then
    '                clsDBFuncationality.ExecuteNonQuery(" update TSPL_USER_MASTER set Vendor_Code = '" + fndVendor.Value + "' where User_Code ='" + fndUserCode.Value + "'")
    '            End If
    '            'Add By : Prabhakar'
    '            If PanelCNF = True Then
    '                clsDBFuncationality.ExecuteNonQuery(" update TSPL_USER_MASTER set Login_Type = '" + CmbLoginType.SelectedValue + "' ,Cust_Code='" + fndCustCode.Value + "', Distributor_Retailer_Code='" + fndDisRetailerCode.Value + "'  where User_Code ='" + fndUserCode.Value + "'")
    '            End If


    '        End If
    '        Dim coll As New Hashtable()
    '        clsCommon.AddColumnsForChange(coll, "EmployeeCode", EmployeeFinder.Value)
    '        Dim result As Boolean = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_USER_MASTER", OMInsertOrUpdate.Insert, "")
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
    '    End Try
    'End Sub
    Sub glsecurityaccountupdate()
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("Select TSPL_USER_MASTER.User_Code,TSPL_USER_MASTER.Default_Location,TSPL_LOCATION_MASTER.Loc_Segment_Code  from TSPL_USER_MASTER Left outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location=TSPL_LOCATION_MASTER.Location_Code  where TSPL_USER_MASTER.Default_Location is not null")
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                Dim str As String = "UPDATE TSPL_GL_SEGMENT_PERMISSION SET Default_Segment ='N' WHERE User_Code ='" & clsCommon.myCstr(dr("User_Code")) & "'"
                clsDBFuncationality.ExecuteNonQuery(str)
                'If clsCommon.CompairString(clsCommon.myCstr(dr("User_Code")), "DHANSINGH") = CompairStringResult.Equal Then
                '    clsCommon.MyMessageBoxShow("hi")
                'End If
                If clsDBFuncationality.getSingleValue("Select count(*) from TSPL_GL_SEGMENT_PERMISSION where User_Code ='" & clsCommon.myCstr(dr("User_Code")) & "' and Segment_Code ='" & clsCommon.myCstr(dr("Loc_Segment_Code")) & "'") <= 0 Then
                    connectSql.RunSpTransaction(Nothing, "SP_TSPL_GL_SEGMENT_PERMISSION_INSERT", New SqlParameter("@usercode", "" & clsCommon.myCstr(dr("User_Code")) & ""), New SqlParameter("@glsegment", "7"), New SqlParameter("@segmentcode", "" & clsCommon.myCstr(dr("Loc_Segment_Code")) & ""), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifydate", connectSql.serverDate()), New SqlParameter("@modifyby", userCode), New SqlParameter("@compcode", companyCode), New SqlParameter("@Default_Segment", "Y"))
                Else
                    clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_GL_SEGMENT_PERMISSION SET Default_Segment ='Y' WHERE User_Code ='" & clsCommon.myCstr(dr("User_Code")) & "' and Segment_Code ='" & clsCommon.myCstr(dr("Loc_Segment_Code")) & "' ")
                End If
            Next
        End If
    End Sub
    Private Sub funUpdate()
        Try
            If IsCorrectUSer(Nothing) Then
                If (EmployeeFinder.Value IsNot Nothing AndAlso clsCommon.myLen(EmployeeFinder.Value) > 0) Or (clsCommon.myLen(fndEmployeeCode.Value) <= 0) Then
                    fndEmployeeCode.Value = EmployeeFinder.Value
                End If
                Try
                    ' Ticket No : BHA/10/10/18-000614 By prabhakar - For Save History Data
                    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, fndUserCode.Value, "TSPL_USER_MASTER", "User_Code", Nothing)
                Catch ex As Exception

                End Try


                clsDBFuncationality.UpdateInAllDatabase("sp_tspl_user_master_update", New SqlParameter("@Usercode", fndUserCode.Value), New SqlParameter("@UserName", txtUserName.Text), New SqlParameter("@EmployeeCode", fndEmployeeCode.Value), New SqlParameter("@EmployeeName", txtEmployeeName.Text), New SqlParameter("@Password", clsCommon.EncryptString(txtPassword.Text)), New SqlParameter("@UserType", ddlUserType.Text), New SqlParameter("@Level1", fndLabel1.Value), New SqlParameter("@Level2", fndLabel2.Value), New SqlParameter("@Level3", fndLabel3.Value), New SqlParameter("@Level4", fndLabel4.Value), New SqlParameter("@Createdby", userCode), New SqlParameter("@Createddate", connectSql.serverDate()), New SqlParameter("@Modifiedby", userCode), New SqlParameter("@Modifieddate", connectSql.serverDate()), New SqlParameter("@CompCode", companyCode), New SqlParameter("@ApprovalLevel", cmbLevel.SelectedValue))
                clsDBFuncationality.ExecuteNonQuery(" update TSPL_USER_MASTER set Mob_No = '" + txt_Mob_No.Text + "',E_Mail = '" + txtEmailId.Text + "',ApprovalLevel='" & cmbLevel.SelectedValue & "',Licence_Reserved=" + IIf(chkLicenceReserved.Checked, "1", "0") + "  where User_Code ='" + fndUserCode.Value + "'")
                If clsCommon.myLen(fndDepartment.Value) > 0 Then
                    clsDBFuncationality.ExecuteNonQuery(" update TSPL_USER_MASTER set Segment_code = '" + fndDepartment.Value + "' where User_Code ='" + fndUserCode.Value + "'")
                End If

                'If clsCommon.myLen(fndZone.Value) > 0 Then
                '    clsDBFuncationality.ExecuteNonQuery(" update TSPL_USER_MASTER set Zone_code = '" + fndZone.Value + "' where User_Code ='" + fndUserCode.Value + "'")
                'End If
                If (clsCommon.myLen(txtDefaultLocation.Value) > 0) Then
                    clsDBFuncationality.ExecuteNonQuery(" update TSPL_USER_MASTER set Default_Location = '" + txtDefaultLocation.Value + "' where User_Code ='" + fndUserCode.Value + "'")
                    ''RICHA AGARWAL 12/05/2015
                    Dim str As String = "UPDATE TSPL_GL_SEGMENT_PERMISSION SET Default_Segment ='N' WHERE User_Code ='" & fndUserCode.Value & "'"
                    clsDBFuncationality.ExecuteNonQuery(str)
                    Dim SegCodeQrY As String = "SELECT Loc_Segment_Code FROM TSPL_LOCATION_MASTER WHERE Location_Code ='" & txtDefaultLocation.Value & "' "
                    Dim Gl_Segment As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(SegCodeQrY))
                    If clsDBFuncationality.getSingleValue("Select count(*) from TSPL_GL_SEGMENT_PERMISSION where User_Code ='" & fndUserCode.Value & "' and Segment_Code ='" & Gl_Segment & "'") <= 0 Then
                        connectSql.RunSpTransaction(Nothing, "SP_TSPL_GL_SEGMENT_PERMISSION_INSERT", New SqlParameter("@usercode", fndUserCode.Value), New SqlParameter("@glsegment", "7"), New SqlParameter("@segmentcode", Gl_Segment), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifydate", connectSql.serverDate()), New SqlParameter("@modifyby", userCode), New SqlParameter("@compcode", companyCode), New SqlParameter("@Default_Segment", "Y"))
                    Else
                        clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_GL_SEGMENT_PERMISSION SET Default_Segment ='Y' WHERE User_Code ='" & fndUserCode.Value & "' and Segment_Code ='" & Gl_Segment & "' ")
                    End If

                    ''---------------------------
                End If
                If clsCommon.myLen(fndVendor.Value) > 0 Then
                    clsDBFuncationality.ExecuteNonQuery(" update TSPL_USER_MASTER set Vendor_Code = '" + fndVendor.Value + "' where User_Code ='" + fndUserCode.Value + "'")
                End If
                updateExtraColumns()

                'Prabhakar Ticket : BM00000009802' 
                If PanelCNF = True AndAlso (CmbLoginType.Text = "CNF" Or CmbLoginType.Text = "Parlor" Or CmbLoginType.Text = "Distributer" Or CmbLoginType.Text = "Retailer") Then
                    If (CmbLoginType.Text = "CNF" Or CmbLoginType.Text = "Parlor") Then
                        clsDBFuncationality.ExecuteNonQuery(" update TSPL_USER_MASTER set Login_Type = '" + CmbLoginType.SelectedValue + "',Cust_Code='" + fndCustCode.Value + "' where User_Code ='" + fndUserCode.Value + "'")
                    End If
                    If CmbLoginType.Text = "Distributer" Or CmbLoginType.Text = "Retailer" Then
                        clsDBFuncationality.ExecuteNonQuery(" update TSPL_USER_MASTER set Login_Type = '" + CmbLoginType.SelectedValue + "',Distributor_Retailer_Code='" + fndDisRetailerCode.Value + "' where User_Code ='" + fndUserCode.Value + "'")
                    End If
                End If
                If isCheckCustomerType = True Then
                    If MatchLevel() = 1 Then
                        clsDBFuncationality.ExecuteNonQuery(" update TSPL_USER_MASTER set Login_Type = '" + CmbLoginType.SelectedValue + "',Cust_Code='" + fndCustCode.Value + "' where User_Code ='" + fndUserCode.Value + "'")
                    End If
                    If MatchLevel() > 1 Then
                        clsDBFuncationality.ExecuteNonQuery(" update TSPL_USER_MASTER set Login_Type = '" + CmbLoginType.SelectedValue + "',Distributor_Retailer_Code='" + fndDisRetailerCode.Value + "' where User_Code ='" + fndUserCode.Value + "'")
                    End If
                End If

                If CmbLoginType.Text = "Super User" Or CmbLoginType.Text = "Driver" Then
                    clsDBFuncationality.ExecuteNonQuery(" update TSPL_USER_MASTER set Login_Type = '" + CmbLoginType.SelectedValue + "' where User_Code ='" + fndUserCode.Value + "'")
                End If

                If ChkSuperUser = True AndAlso PanelCNF = True Then
                    clsDBFuncationality.ExecuteNonQuery(" delete from TSPL_USER_CUSTOMER_MAPPING where User_Code = '" + fndUserCode.Value + "' ")
                    SaveCustomerMapping()
                End If

                SaveUserMapping()
                ''Add By : Prabhakar'
                'If PanelCNF = True Then
                '    clsDBFuncationality.ExecuteNonQuery(" update TSPL_USER_MASTER set Login_Type = '" + CmbLoginType.SelectedValue + "' ,Cust_Code='" + fndCustCode.Value + "', Distributor_Retailer_Code='" + fndDisRetailerCode.Value + "'  where User_Code ='" + fndUserCode.Value + "'")
                'End If
                '===============================================
                clsDBFuncationality.ExecuteNonQuery("delete from TSPL_USER_CUSTOMER_ZONE  where User_Code ='" + fndUserCode.Value + "' ")
                clsDBFuncationality.ExecuteNonQuery("delete from TSPL_USER_CUSTOMER_CATEGORY  where User_Code ='" + fndUserCode.Value + "' ")
                clsDBFuncationality.ExecuteNonQuery("delete from TSPL_User_Route_Mapping  where User_Code ='" + fndUserCode.Value + "' ")
                arrZone = mulZone.arrValueMember
                If (arrZone IsNot Nothing AndAlso arrZone.Count > 0) Then
                    For Each strZoneCode As String In arrZone
                        Dim collZone As New Hashtable()
                        Dim strTRCode As String = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"), clsDocType.Detail, clsDocTransactionType.Detail, "")
                        clsCommon.AddColumnsForChange(collZone, "TR_Code", strTRCode)
                        clsCommon.AddColumnsForChange(collZone, "User_Code", fndUserCode.Value)
                        clsCommon.AddColumnsForChange(collZone, "Zone_Code", strZoneCode)
                        clsCommonFunctionality.UpdateDataTable(collZone, "TSPL_USER_CUSTOMER_ZONE", OMInsertOrUpdate.Insert, "")
                    Next
                End If

                arrCustomerCategory = mulCustomerCategory.arrValueMember
                If (arrCustomerCategory IsNot Nothing AndAlso arrCustomerCategory.Count > 0) Then
                    For Each strCustomerCategory As String In arrCustomerCategory
                        Dim collCustCategory As New Hashtable()
                        Dim strTRCode As String = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"), clsDocType.Detail, clsDocTransactionType.Detail, "")
                        clsCommon.AddColumnsForChange(collCustCategory, "TR_Code", strTRCode)
                        clsCommon.AddColumnsForChange(collCustCategory, "User_Code", fndUserCode.Value)
                        clsCommon.AddColumnsForChange(collCustCategory, "Customer_Category", strCustomerCategory)
                        clsCommonFunctionality.UpdateDataTable(collCustCategory, "TSPL_USER_CUSTOMER_CATEGORY", OMInsertOrUpdate.Insert, "")
                    Next
                End If

                arrRoute = txtRoute.arrValueMember
                If (arrRoute IsNot Nothing AndAlso arrRoute.Count > 0) Then
                    For Each strRouteCode As String In arrRoute
                        Dim collRoute As New Hashtable()
                        Dim strTRCode As String = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"), clsDocType.Detail, clsDocTransactionType.Detail, "")
                        clsCommon.AddColumnsForChange(collRoute, "TR_Code", strTRCode)
                        clsCommon.AddColumnsForChange(collRoute, "User_Code", fndUserCode.Value)
                        clsCommon.AddColumnsForChange(collRoute, "Route_No", strRouteCode)
                        clsCommonFunctionality.UpdateDataTable(collRoute, "TSPL_User_Route_Mapping", OMInsertOrUpdate.Insert, "")
                    Next
                End If

                '===============================================
                myMessages.update()
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Function IsCorrectUSer(ByVal trans As SqlTransaction) As Boolean
        If Not objCommonVar.NoOfJournalEnteryLicence = -1 Then
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(1) from TSPL_USER_MASTER", trans)) > objCommonVar.NoOfJournalEnteryLicence Then
                Throw New Exception("Please ask your administrator to purchase licence" + Environment.NewLine + objCommonVar.LicenceMessageContactPersion)
            End If
        End If
        Return True
    End Function

    Private Sub funDelete()
        clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_USER_CUSTOMER_CATEGORY where User_Code ='" & clsCommon.myCstr(fndUserCode.Value) & "'")
        clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_USER_CUSTOMER_ZONE where User_Code ='" & clsCommon.myCstr(fndUserCode.Value) & "'")
        clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_USER_MAPPING_DETAIL where User_Code ='" & clsCommon.myCstr(fndUserCode.Value) & "'")
        clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_USER_Customer_MAPPING where User_Code ='" & clsCommon.myCstr(fndUserCode.Value) & "'")
        clsDBFuncationality.UpdateInAllDatabase("sp_TSPL_USER_MASTER_delete", New SqlParameter("@UserCode", fndUserCode.Value))
        myMessages.delete()
        btnSave.Text = "Save"
        btnDelete.Enabled = False
    End Sub
    Private Sub funReset()
        fndUserCode.MyReadOnly = False
        fndUserCode.Value = ""
        fndEmployeeCode.Value = ""
        txtEmployeeName.Text = ""
        txtPassword.Text = ""
        txtUserName.Text = ""
        ddlUserType.Text = "----Select----"
        fndLabel1.Value = ""
        fndLabel2.Value = ""
        fndLabel3.Value = ""
        fndLabel4.Value = ""
        fndVendor.Value = ""
        lblVendorName.Text = ""
        fndLabel1.Enabled = False
        fndLabel3.Enabled = False
        chkLicenceReserved.Checked = False
        txtEmailId.Text = ""
        txt_Mob_No.Text = ""
        txtDefaultLocation.Value = ""
        lblLocationName.Text = ""
        cmbLevel.SelectedValue = 0
        btnSave.Text = "Save"
        btnDelete.Enabled = False
        EmployeeFinder.Value = ""
        lblEmployeeFinder.Text = ""
        fndDepartment.Value = ""
        lblDepartmentName.Text = ""
        mulCustomerCategory.arrValueMember = Nothing
        mulZone.arrValueMember = Nothing
        txtRoute.arrValueMember = Nothing
        'fndZone.Value = ""
        'lblzone.Text = ""
        ChkViewMilkReceiptAndSample.Checked = False
        ChkDepartmentHead.Checked = False
        chkHRAdmin.Checked = False
        chkInActive.Checked = False
        chkInActive.Enabled = False
        dtInActive.Value = connectSql.serverDate()
        CboAppUserType.SelectedValue = ""
        CmbAppUserSaleType.SelectedValue = ""
        'Add By : Prabhakar Ticket : BM00000009802'
        If PanelCNF = True Then
            lblDisRetailer.Text = "Distributer"
            CmbLoginType.Text = "Select"
            fndCustCode.Value = ""
            lblCustCode.Text = ""
            fndDisRetailerCode.Value = ""
            lblRetailerCode.Text = ""

        End If
        If ChkSuperUser = True AndAlso PanelCNF = True Then
            'RadPageView1.Pages("RadPageViewPage2").Item.Visibility = MyBase.customFieldTabProperty
            gvCustomer.DataSource = Nothing
            LoadBlankCustomerGrid()
            gvCustomer.Rows.AddNew()

            If CmbLoginType.Text = "Super User" Then
                RadPageViewPage2.Enabled = True
            Else
                RadPageViewPage2.Enabled = False
            End If

        End If
        If isCheckCustomerType = True Then
            lblDisRetailer.Text = "POS Details"
            CmbLoginType.Text = "Select"
            fndCustCode.Value = ""
            lblCustCode.Text = ""
            fndDisRetailerCode.Value = ""
            lblRetailerCode.Text = ""

        End If
        gvUser.DataSource = Nothing
        LoadBlankUserGrid()
        gvUser.Rows.AddNew()
        fndUserCode.Focus()
    End Sub
    'priti added on 01-06-2011 --- To implement the access control
    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        'If funCheckLoginStatus() = False Then Exit Function
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "USER-M"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
    '        strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '        strTemp = Split(strRights, ",")
    '        If strTemp(0) = "0" Then
    '            MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    '            funSetUserAccess = False
    '            blnRead = False
    '            Me.Close()
    '            Exit Function
    '        Else
    '            blnRead = True
    '        End If
    '        If strTemp(1) = "0" Then 'Grant modify access
    '            btnSave.Enabled = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            btnDelete.Enabled = False
    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception

    '    End Try
    'End Function
    'Code ends here
#End Region
#Region "SelectedIndexChanged"

    Private Sub RadDropDownList1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles ddlUserType.SelectedIndexChanged
        If ddlUserType.Text = "Level1" Then
            fndLabel1.Enabled = False
            '  RadGroupBox1.Enabled = False
            fndLabel3.Enabled = False
            'fndLabel4.Enabled = False

        ElseIf ddlUserType.Text = "Level2" Then
            fndLabel1.Enabled = False
            'RadGroupBox1.Enabled = False
            fndLabel3.Enabled = False
            'fndLabel4.Enabled = False

            fndLabel1.Enabled = True
            ' globalFunc.mandatoryText(fndLabel1.Value)
        ElseIf ddlUserType.Text = "Level3" Then

            fndLabel1.Enabled = False
            ' RadGroupBox1.Enabled = False
            fndLabel3.Enabled = False
            'fndLabel4.Enabled = False

            fndLabel1.Enabled = True
            RadGroupBox1.Enabled = True
            '   globalFunc.mandatoryText(fndLabel1.Value, fndLabel2.Value)
        ElseIf ddlUserType.Text = "Level4" Then

            fndLabel1.Enabled = False
            'RadGroupBox1.Enabled = False
            fndLabel3.Enabled = False
            'fndLabel4.Enabled = False

            fndLabel1.Enabled = True
            RadGroupBox1.Enabled = True
            fndLabel3.Enabled = True
            ' globalFunc.mandatoryText(fndLabel1.Value, fndLabel2.Value, fndLabel3.txtValue)
        ElseIf ddlUserType.Text = "Level5" Then

            fndLabel1.Enabled = False
            '  RadGroupBox1.Enabled = False
            fndLabel3.Enabled = False
            'fndLabel4.Enabled = False

            fndLabel1.Enabled = True
            'RadGroupBox1.Enabled = True
            fndLabel3.Enabled = True
            fndLabel4.Enabled = True
            'globalFunc.mandatoryText(fndLabel1.Value, fndLabel2.Value, fndLabel3.txtValue, fndLabel4.Value)
        Else
        End If
    End Sub
#End Region
#Region "Button Click Event"
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) Then
                If clsCommon.myLen(fndUserCode.Value) <= 0 Then
                    fndUserCode.Focus()
                    Throw New Exception("User Code cannot be left blank.")
                End If
                If clsCommon.myLen(fndUserCode.Value) > 12 Then
                    fndUserCode.Focus()
                    Throw New Exception("User Code cannot be greater than 12 characters.")
                End If
            End If

            If clsCommon.myLen(txtPassword.Text) <= 0 Then
                txtPassword.Focus()
                Throw New Exception("Password cannot be left blank.")
            End If

            If clsCommon.myLen(txtDefaultLocation.Value) <= 0 Then
                txtDefaultLocation.Focus()
                Throw New Exception("Default Location cannot be left blank.")
            End If
            If PanelCNF = True Then
                'If CmbLoginType.Text = "Select" Then
                '    CmbLoginType.Focus()
                '    Throw New Exception("Please select Type.")
                'End If
                If CmbLoginType.Text = "CNF" Then
                    If clsCommon.myLen(fndCustCode.Value) <= 0 Then
                        fndCustCode.Focus()
                        Throw New Exception("Customer Code cannot be left blank.")
                    End If
                End If

                If CmbLoginType.Text = "Parlor" Then
                    If clsCommon.myLen(fndCustCode.Value) <= 0 Then
                        fndCustCode.Focus()
                        Throw New Exception("Customer Code cannot be left blank.")
                    End If
                End If

                If CmbLoginType.Text = "Distributer" Then
                    If clsCommon.myLen(fndDisRetailerCode.Value) <= 0 Then
                        fndDisRetailerCode.Focus()
                        Throw New Exception("Distributer Code cannot be left blank.")
                    End If

                End If
                If CmbLoginType.Text = "Retailer" Then
                    If clsCommon.myLen(fndDisRetailerCode.Value) <= 0 Then
                        fndDisRetailerCode.Focus()
                        Throw New Exception("Retailer Code cannot be left blank.")
                    End If

                End If
            ElseIf isCheckCustomerType Then
                If MatchLevel() = 1 Then
                    If clsCommon.myLen(fndCustCode.Value) <= 0 Then
                        fndCustCode.Focus()
                        Throw New Exception("Customer Code cannot be left blank.")
                    End If
                End If

                If MatchLevel() > 1 Then
                    If clsCommon.myLen(fndDisRetailerCode.Value) <= 0 Then
                        fndDisRetailerCode.Focus()
                        Throw New Exception("POS detail Code cannot be left blank.")
                    End If

                End If
            End If

            If ChkSuperUser = True AndAlso PanelCNF = True Then
                Dim Icode As String = ""
                Dim oldicode As String = ""

                For ii As Integer = 0 To gvCustomer.Rows.Count - 1
                    Icode = clsCommon.myCstr(gvCustomer.Rows(ii).Cells(colCustCode).Value)

                    If clsCommon.myLen(Icode) > 0 Then

                        For jj As Integer = ii + 1 To gvCustomer.Rows.Count - 1
                            oldicode = clsCommon.myCstr(gvCustomer.Rows(jj).Cells(colCustCode).Value)

                            If clsCommon.CompairString(Icode, oldicode) = CompairStringResult.Equal Then
                                gvCustomer.CurrentRow = gvCustomer.Rows(jj + 1)
                                Throw New Exception("Duplicate Customer at row no. " + clsCommon.myCstr(jj + 1) + "")
                            End If
                        Next
                    End If
                Next

            End If

            Dim Ucode As String = ""
            Dim oldUcode As String = ""

            For ii As Integer = 0 To gvUser.Rows.Count - 1
                Ucode = clsCommon.myCstr(gvUser.Rows(ii).Cells(colUserCode).Value)

                If clsCommon.myLen(Ucode) > 0 Then

                    For jj As Integer = ii + 1 To gvUser.Rows.Count - 1
                        oldUcode = clsCommon.myCstr(gvUser.Rows(jj).Cells(colUserCode).Value)

                        If clsCommon.CompairString(Ucode, oldUcode) = CompairStringResult.Equal Then
                            gvUser.CurrentRow = gvUser.Rows(jj + 1)
                            Throw New Exception("Duplicate User at row no. " + clsCommon.myCstr(jj + 1) + "")
                        End If
                    Next
                End If
            Next

            If ChkAutoDepOnPurchaseCycle = True Then
                If clsCommon.myLen(fndDepartment.Value) <= 0 Then
                    fndDepartment.Focus()
                    Throw New Exception("Department Code cannot be left blank.")
                End If
            End If

            If PasswordRules = True Then
                ValidatePassword(txtPassword.Text, 8, 2, 2, 2, 2)
                If CheckPassword = False Then
                    common.clsCommon.MyMessageBoxShow("Password is invalid. Format not match")
                    Exit Sub
                End If
            End If
            If chkLicenceReserved.Checked Then
                Dim NoofLiceence As Decimal = clsCommon.myCdbl(clsCommon.DecryptString(clsFixedParameter.GetData(clsFixedParameterType.LicenceNoOfExeConnection, clsFixedParameterCode.LicenceNoOfExeConnection, Nothing), objCommonVar.CurrentCompanyCode + "C"))
                If NoofLiceence > 0 Then
                    Dim qry As String = clsLoginInfo.funGetActiveUserQuery(False)
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

                    'Dim dt As Data"'")Table = clsDBFuncationality.GetDataTable("select 1 from TSPL_USER_MASTER where Licence_Reserved=1 and User_Code <> '" + fndUserCode.Value + 
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        If (dt.Rows.Count - NoofLiceence) > -1 Then
                            Throw New Exception("You Cannot Reserve more Users")
                        End If
                    End If
                End If
            End If

            If clsCommon.CompairString(clsCommon.myCstr(CboAppUserType.SelectedValue), "R") = CompairStringResult.Equal Then
                If clsCommon.myLen(EmployeeFinder.Value) <= 0 Then
                    EmployeeFinder.Focus()
                    Throw New Exception("Please select Employee ")
                End If
            ElseIf clsCommon.CompairString(clsCommon.myCstr(CboAppUserType.SelectedValue), "V") = CompairStringResult.Equal Then
                If clsCommon.myLen(fndVendor.Value) <= 0 Then
                    fndVendor.Focus()
                    Throw New Exception("Please select Vendor/VSP ")
                End If
            ElseIf clsCommon.CompairString(clsCommon.myCstr(CboAppUserType.SelectedValue), "F") = CompairStringResult.Equal Then
                If clsCommon.myLen(txtMP.Value) <= 0 Then
                    txtMP.Focus()
                    Throw New Exception("Please select Milk Producer/Farmer ")
                End If
            End If

            SaveData()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Function ValidatePassword(ByVal pwd As String, Optional ByVal minLength As Integer = 8, Optional ByVal numUpper As Integer = 2, Optional ByVal numLower As Integer = 2, Optional ByVal numNumbers As Integer = 2, Optional ByVal numSpecial As Integer = 2) As Boolean
        ' Replace [A-Z] with \p{Lu}, to allow for Unicode uppercase letters.
        Dim upper As New System.Text.RegularExpressions.Regex("[A-Z]")
        Dim lower As New System.Text.RegularExpressions.Regex("[a-z]")
        Dim number As New System.Text.RegularExpressions.Regex("[0-9]")
        ' Special is "none of the above".
        Dim special As New System.Text.RegularExpressions.Regex("[^a-zA-Z0-9]")
        CheckPassword = True
        ' Check the length.
        If Len(pwd) < minLength Then
            CheckPassword = False
            Return False
        End If
        ' Check for minimum number of occurrences.
        If upper.Matches(pwd).Count <= 0 Then
            CheckPassword = False
            Return False
        End If

        If lower.Matches(pwd).Count <= 0 Then
            CheckPassword = False
            Return False
        End If

        If number.Matches(pwd).Count <= 0 Then
            CheckPassword = False
            Return False
        End If
        If special.Matches(pwd).Count <= 0 Then
            CheckPassword = False
            Return False
        End If

        ' Passed all checks.
        Return True
    End Function
    Sub SaveData()
        If MyBase.isModifyonPasswordFlag Then
            If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.userMaster, clsCommon.myCstr(companyCode)) Then
            Else
                Return
            End If
        End If
        If btnSave.Text = "Save" Then
            'If fndUserCode.Value = "" Then
            '    ' common.clsCommon.MyMessageBoxShow("User Code cannot be blank.")
            '    myMessages.blankValue("User Code")
            '    fndUserCode.Focus()

            '    'ElseIf fndEmployeeCode.Value = "" Then
            '    '    myMessages.blankValue("Employee Code")
            'Else
            '    funInsert()
            'End If
            funInsert()
        Else
            funUpdate()
        End If
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(fndUserCode.Value) <= 0 Then
            Exit Sub
        End If

        If myMessages.deleteConfirm() Then
            funDelete()
        Else

        End If

    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        funReset()
    End Sub
#End Region
#Region "Finder Load"

    Private Sub fndLabel1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndLabel1.ConnectionString = connectSql.SqlCon()
        'fndLabel1.Query = "select User_Code  as [User Code],USER_NAME  as [User Name]from TSPL_USER_MASTER  where User_Type in ('Level1') order by User_Code"
        'fndLabel1.ValueToSelect = "User Code"
        'fndLabel1.Caption = "User Details"
        'fndLabel1.ValueToSelect1 = "User Name"
    End Sub
    Private Sub fndLabel2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndLabel2.ConnectionString = connectSql.SqlCon()
        'fndLabel2.Query = "select User_Code  as [User Code],USER_NAME  as [User Name]from TSPL_USER_MASTER  where User_Type in ('Level2') order by User_Code"
        'fndLabel2.ValueToSelect = "User Code"
        'fndLabel2.Caption = "User Details"
        'fndLabel2.ValueToSelect1 = "User Name"
    End Sub

    Private Sub fndLabel3_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndLabel3.ConnectionString = connectSql.SqlCon()
        'fndLabel3.Query = "select User_Code  as [User Code],USER_NAME  as [User Name]from TSPL_USER_MASTER  where User_Type in ('Level3') order by User_Code"
        'fndLabel3.ValueToSelect = "User Code"
        'fndLabel3.Caption = "User Details3"
        'fndLabel3.ValueToSelect1 = "User Name"
    End Sub

    Private Sub fndLabel4_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndLabel4.ConnectionString = connectSql.SqlCon()
        'fndLabel4.Query = "select User_Code  as [User Code],USER_NAME  as [User Name]from TSPL_USER_MASTER  where User_Type in ('Level4') order by User_Code"
        'fndLabel4.ValueToSelect = "User Code"
        'fndLabel4.Caption = "User Details"
        'fndLabel4.ValueToSelect1 = "User Name"
    End Sub

    Private Sub fndUserCode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndUserCode.ConnectionString = connectSql.SqlCon()
        'fndUserCode.Query = "select User_Code  as [User Code],USER_NAME  as [User Name]from TSPL_USER_MASTER  order by User_Code"
        'fndUserCode.ValueToSelect = "User Code"
        'fndUserCode.Caption = "User Details"
        'fndUserCode.ValueToSelect1 = "User Name"
    End Sub
    Private Sub fndEmployeeCode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndEmployeeCode.ConnectionString = connectSql.SqlCon()
        'fndEmployeeCode.Query = "select Emp_Code as [Employee Code],Emp_Name as [Employee Name] from TSPL_EMPLOYEE_MASTER order by Emp_Code"
        'fndEmployeeCode.ValueToSelect = "Employee Code"
        'fndEmployeeCode.ValueToSelect1 = "Employee Name"
        'fndEmployeeCode.Caption = "Employee Details"
    End Sub
#End Region
#Region "Finder Leave"
    Private Sub fndLabel1_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fndLabel1.Value <> "" Then
            Dim s As String = clsDBFuncationality.getSingleValue(" select User_Code from TSPL_USER_MASTER  where User_Code ='" + fndLabel1.Value + "'")

            'While dr.Read()
            '    s = dr(0).ToString()
            'End While
            If s <> fndLabel1.Value Then
                common.clsCommon.MyMessageBoxShow("Level1 doesn't exist.")
                fndLabel1.Value = ""
                fndLabel1.Focus()
            Else

            End If
        End If

    End Sub

    Private Sub fndLabel2_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fndLabel2.Value <> "" Then
            Dim s As String = clsDBFuncationality.getSingleValue(" select User_Code from TSPL_USER_MASTER  where User_Code ='" + fndLabel2.Value + "'")

            'While dr.Read()
            '    s = dr(0).ToString()
            'End While
            If s <> fndLabel2.Value Then
                common.clsCommon.MyMessageBoxShow("Level2 doesn't exist.")
                fndLabel2.Value = ""
                fndLabel2.Focus()
            Else

            End If
        End If
    End Sub

    Private Sub fndLabel3_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fndLabel3.Value <> "" Then
            Dim s As String = clsDBFuncationality.getSingleValue(" select User_Code from TSPL_USER_MASTER  where User_Code ='" + fndLabel3.Value + "'")

            'While dr.Read()
            '    s = dr(0).ToString()
            'End While
            If s <> fndLabel3.Value Then
                common.clsCommon.MyMessageBoxShow("Level3 doesn't exist.")
                fndLabel3.Value = ""
                fndLabel3.Focus()
            Else
            End If
        End If
    End Sub

    Private Sub fndLabel4_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fndLabel4.Value <> "" Then
            Dim s As String = clsDBFuncationality.getSingleValue(" select User_Code from TSPL_USER_MASTER  where User_Code ='" + fndLabel4.Value + "'")

            'While dr.Read()
            '    s = dr(0).ToString()
            'End While
            If s <> fndLabel4.Value Then
                common.clsCommon.MyMessageBoxShow("Level4 doesn't exist.")
                fndLabel4.Value = ""
                fndLabel4.Focus()
            Else

            End If
        End If
    End Sub
    Private Sub fndEmployeeCode_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fndEmployeeCode.Value <> "" Then
            Dim s As String = clsDBFuncationality.getSingleValue("select Emp_Code from TSPL_EMPLOYEE_MASTER where Emp_Code='" + fndEmployeeCode.Value + "'")

            'While dr.Read()
            '    s = dr(0).ToString()
            'End While
            If s <> fndEmployeeCode.Value Then
                common.clsCommon.MyMessageBoxShow("Employee Code doesn't exist")
                fndEmployeeCode.Value = ""
                txtEmployeeName.Text = ""
                fndEmployeeCode.Focus()
            Else
                txtEmployeeName.Text = fndEmployeeCode.Tag
            End If
        Else
            txtPassword.Focus()
        End If
    End Sub

#End Region
#Region "Import/Export"
    Private Sub menuExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuExport.Click
        '=======Update By preeti Gupta Against Ticket No[BM00000008831]
        'sql = "select User_Code,User_Name,Password,Emp_Code,Emp_Name,User_Type,Level1_Code,Level2_Code,Level3_Code,Level4_Code from TSPL_USER_MASTER "
        sql = "select User_Code,User_Name,Default_Location as [Default Location],Department_Head as [Department Head],InActive,InActive_Date from TSPL_USER_MASTER "
        ListImpExpColumnsMandatory = New List(Of String)({"User_Code"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"User_Code"})
        transportSql.ExporttoExcel(sql, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
    End Sub
    Private Sub menuImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "User_Code", "User_Name", "Default Location", "Department Head", "InActive", "InActive_Date") Then  ' "Zone Code"
            Dim trans As SqlTransaction = Nothing

            Try
                trans = clsDBFuncationality.GetTransactin()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim strUserCode As String
                    Dim strName As String
                    Dim strEmpCode As String = ""
                    Dim strEmpName As String = ""
                    Dim strUserType As String = ""
                    Dim strLoginType As String = ""
                    Dim strLevel1 As String = ""
                    Dim strLevel2 As String = ""
                    Dim strLevel3 As String = ""
                    Dim strLevel4 As String = ""
                    ' Dim strZoneCode As String = ""

                    If clsCommon.myLen(grow.Cells("User_Code").Value) <= 0 Then
                        common.clsCommon.MyMessageBoxShow("User Code cannot be blank.")
                        trans.Rollback()
                        Exit Sub
                    ElseIf grow.Cells("User_Code").Value.ToString().Length > 12 Then
                        common.clsCommon.MyMessageBoxShow("User Code cannot be greater than 12 length.")
                        trans.Rollback()
                        Exit Sub
                    Else
                        strUserCode = grow.Cells("User_Code").Value.ToString().ToUpper()
                    End If
                    If grow.Cells("User_Name").Value.ToString().Length > 50 Then
                        common.clsCommon.MyMessageBoxShow("User Name cannot be greater than 50 length.")
                        trans.Rollback()
                        Exit Sub
                    Else
                        strName = grow.Cells("User_Name").Value.ToString()
                    End If

                    Dim strDefaultLocation As String = clsCommon.myCstr(grow.Cells("Default Location").Value)
                    If clsCommon.myLen(strDefaultLocation) > 0 Then
                        Dim Default_Location As String = clsDBFuncationality.getSingleValue("select Location_Code  from TSPL_LOCATION_MASTER  where Location_Code ='" + grow.Cells("Default Location").Value.ToString() + "'", trans)
                        If clsCommon.CompairString(Default_Location, strDefaultLocation) = CompairStringResult.Equal Then
                            strDefaultLocation = grow.Cells("Default Location").Value.ToString()
                        Else
                            Throw New Exception("Default Location '" + strDefaultLocation + "' Does Not Exist In Location Master")
                        End If
                    End If
                    'strZoneCode = clsCommon.myCstr(grow.Cells("Zone Code").Value)
                    'If clsCommon.myLen(strZoneCode) > 0 Then
                    '    Dim isValidZoneCode As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("Select Count(*) from TSPL_ZONE_MASTER where Zone_Code = '" + strZoneCode + "'", trans))
                    '    If isValidZoneCode = False Then
                    '        Throw New Exception("" + strZoneCode + " Invalid Zone Code.Does Not Exist In Zone Master")
                    '    End If
                    'End If

                    Dim sql1 As String = "select COUNT(*) from TSPL_USER_MASTER  where User_Code='" + strUserCode + "'"
                    Dim i As Integer = CInt(connectSql.RunScalar(trans, sql1))
                    If (i = 0) Then
                        connectSql.RunSpTransaction(trans, "sp_tspl_user_master_insert", New SqlParameter("@Usercode", strUserCode), New SqlParameter("@UserName", strName), New SqlParameter("@EmployeeCode", strEmpCode), New SqlParameter("@EmployeeName", strEmpName), New SqlParameter("@Password", clsCommon.EncryptString(strUserCode)), New SqlParameter("@UserType", strUserType), New SqlParameter("@Level1", strLevel1), New SqlParameter("@Level2", strLevel2), New SqlParameter("@Level3", strLevel3), New SqlParameter("@Level4", strLevel4), New SqlParameter("@Createdby", userCode), New SqlParameter("@Createddate", connectSql.serverDate(trans)), New SqlParameter("@Modifiedby", userCode), New SqlParameter("@Modifieddate", connectSql.serverDate(trans)), New SqlParameter("@CompCode", companyCode), New SqlParameter("@ApprovalLevel", cmbLevel.SelectedValue))
                    Else
                        Dim strPassword As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select password from TSPL_USER_MASTER  where User_Code='" + strUserCode + "'", trans))
                        connectSql.RunSpTransaction(trans, "sp_tspl_user_master_update", New SqlParameter("@Usercode", strUserCode), New SqlParameter("@UserName", strName), New SqlParameter("@EmployeeCode", strEmpCode), New SqlParameter("@EmployeeName", strEmpName), New SqlParameter("@Password", strPassword), New SqlParameter("@UserType", strUserType), New SqlParameter("@Level1", strLevel1), New SqlParameter("@Level2", strLevel2), New SqlParameter("@Level3", strLevel3), New SqlParameter("@Level4", strLevel4), New SqlParameter("@Createdby", userCode), New SqlParameter("@Createddate", connectSql.serverDate(trans)), New SqlParameter("@Modifiedby", userCode), New SqlParameter("@Modifieddate", connectSql.serverDate(trans)), New SqlParameter("@CompCode", companyCode), New SqlParameter("@ApprovalLevel", cmbLevel.SelectedValue))
                    End If

                    Dim coll As New Hashtable()
                    If (clsCommon.myLen(strDefaultLocation) > 0) Then ' Condition Add by Prabhakar 25/11/2016
                        clsCommon.AddColumnsForChange(coll, "Default_Location", strDefaultLocation)
                        clsCommonFunctionality.UpdateDataTable(coll, "tspl_user_master", OMInsertOrUpdate.Update, "User_Code='" + strUserCode + "'", trans)
                    End If

                    'sanjay
                    Dim colll As New Hashtable()
                    clsCommon.AddColumnsForChange(colll, "Department_Head", grow.Cells("Department Head").Value.ToString())
                    If clsCommon.CompairString(grow.Cells("InActive").Value.ToString(), "Y") = CompairStringResult.Equal Then
                        clsCommon.AddColumnsForChange(colll, "InActive", grow.Cells("InActive").Value.ToString())
                        clsCommon.AddColumnsForChange(colll, "InActive_Date", clsCommon.GetPrintDate(grow.Cells("InActive_Date").Value.ToString(), "dd/MM/yyyy"))
                    End If

                    'If clsCommon.myLen(strZoneCode) > 0 Then
                    '    clsCommon.AddColumnsForChange(colll, "Zone_Code", strZoneCode, True)
                    'End If
                    clsCommonFunctionality.UpdateDataTable(colll, "tspl_user_master", OMInsertOrUpdate.Update, "User_Code='" + strUserCode + "'", trans)
                    'sanjay

                Next
                IsCorrectUSer(trans)
                trans.Commit()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                trans.Rollback()
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub
#End Region


    Private Sub loadUserLevels()
        cmbLevel.DataSource = clsUserMgtCode.GetUserLevels()
        cmbLevel.ValueMember = "Code"
        cmbLevel.DisplayMember = "Description"
    End Sub

    Private Sub LoadAppUserType()
        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        dt.Rows.Add("", "Select")
        dt.Rows.Add("A", "Admin")
        dt.Rows.Add("M", "MCC")
        dt.Rows.Add("R", "RP")
        dt.Rows.Add("V", "VSP")
        dt.Rows.Add("F", "Milk Producer")


        CboAppUserType.DataSource = dt
        CboAppUserType.ValueMember = "Code"
        CboAppUserType.DisplayMember = "Name"
    End Sub

    Private Sub fndLabel4__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndLabel4._MYValidating
        If isButtonClicked Then
            'Dim qry As String = "select User_Code  as [User Code],USER_NAME  as [User Name]from TSPL_USER_MASTER   "
            'fndLabel4.Value = clsCommon.ShowSelectForm("userLevel4", qry, "User Code", " User_Code!='" & clsCommon.myCstr(Me.fndUserCode.Value) & "'", fndLabel4.Value, "User_Code", isButtonClicked)
            fndLabel4.Value = clsUserMaster.getFinder(" User_Code!='" & clsCommon.myCstr(Me.fndUserCode.Value) & "'", fndLabel4.Value, isButtonClicked)
            Me.lblReportingUserName.Text = fndLabel4.Value
        End If
    End Sub

    Private Sub EmployeeFinder__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles EmployeeFinder._MYValidating
        EmployeeFinder.Value = clsEmployeeMaster.getFinder("", EmployeeFinder.Value, isButtonClicked)
        lblEmployeeFinder.Text = clsEmployeeMaster.GetName(EmployeeFinder.Value, Nothing)

    End Sub

    Private Sub TxtFinder1__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean)

    End Sub

    Private Sub fndLabel2__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndLabel2._MYValidating
        If isButtonClicked Then
            Dim qry As String = "select User_Code  as [User Code],USER_NAME  as [User Name]from TSPL_USER_MASTER      "
            'fndLabel2.Value = clsCommon.ShowSelectForm("userLevel2", qry, "User Code", "User_Type in ('Level2')", fndLabel2.Value, "User_Code", isButtonClicked)
            fndLabel2.Value = clsUserMaster.getFinder("User_Type in ('Level2')", fndLabel2.Value, isButtonClicked)
            usercodeTextChanged()
        End If
    End Sub

    Private Sub fndLabel1__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndLabel1._MYValidating
        If isButtonClicked Then
            'Dim qry As String = "select User_Code  as [User Code],USER_NAME  as [User Name]from TSPL_USER_MASTER    "
            'fndLabel1.Value = clsCommon.ShowSelectForm("userLevel1", qry, "User Code", "User_Type in ('Level1')", fndLabel1.Value, "User_Code", isButtonClicked)
            fndLabel1.Value = clsUserMaster.getFinder("User_Type in ('Level1')", fndLabel1.Value, isButtonClicked)
            usercodeTextChanged()
        End If
    End Sub

    Private Sub fndLabel3__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndLabel3._MYValidating
        If isButtonClicked Then
            Dim qry As String = "select User_Code  as [User Code],USER_NAME  as [User Name]from TSPL_USER_MASTER     "
            'fndLabel3.Value = clsCommon.ShowSelectForm("userLevel3", qry, "User Code", "User_Type in ('Level3') ", fndLabel3.Value, "User_Code", isButtonClicked)
            fndLabel3.Value = clsUserMaster.getFinder("User_Type in ('Level3')", fndLabel3.Value, isButtonClicked)
            usercodeTextChanged()
        End If
    End Sub

    Private Sub fndEmployeeCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndEmployeeCode._MYValidating
        If isButtonClicked Then
            'Dim qry As String = "select Emp_Code as [Employee Code],Emp_Name as [Employee Name] from TSPL_EMPLOYEE_MASTER "
            'fndEmployeeCode.Value = clsCommon.ShowSelectForm("userEmployee_Code", qry, "Employee Code", "", fndEmployeeCode.Value, "Emp_Code", isButtonClicked)
            fndEmployeeCode.Value = clsEmployeeMaster.getFinder("", fndEmployeeCode.Value, isButtonClicked)
            Employee_TextChanged()
            txtEmployeeName.Text = clsDBFuncationality.getSingleValue("select Emp_Name from TSPL_EMPLOYEE_MASTER where Emp_code='" + fndEmployeeCode.Value + "'")

        End If
    End Sub

    Private Sub fndUserCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavigatorType As common.NavigatorType) Handles fndUserCode._MYNavigator

        Dim qst As String = "select User_Code  as [User Code],USER_NAME  as [User Name]from TSPL_USER_MASTER   where  2=2 "
        Select Case NavigatorType
            Case NavigatorType.Current
            Case NavigatorType.Next
                qst += "and User_Code in (select min(User_Code) from TSPL_USER_MASTER where User_Code>'" + fndUserCode.Value + "'   ) "
            Case NavigatorType.First
                qst += "and User_Code in (select MIN(User_Code) from TSPL_USER_MASTER  )"
            Case NavigatorType.Last
                qst += "and User_Code in (select Max(User_Code) from TSPL_USER_MASTER  )"
            Case NavigatorType.Previous
                qst += "and User_Code in (select max(User_Code) from TSPL_USER_MASTER where User_Code<'" + fndUserCode.Value + "'   )"
        End Select
        ' fun_gridfill()
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            fndUserCode.Value = clsCommon.myCstr(dt.Rows(0)("User Code"))
            txtUserName.Text = clsCommon.myCstr(dt.Rows(0)("User Name"))
            If ChkSuperUser = True AndAlso PanelCNF = True Then
                If CmbLoginType.Text = "SuperUser" Or CmbLoginType.Text = "Super User" Then
                    RadPageViewPage2.Enabled = True
                    FillCustomerGrid(fndUserCode.Value)
                Else
                    RadPageViewPage2.Enabled = False
                    LoadBlankCustomerGrid()
                End If
            End If
            FillUserGrid(fndUserCode.Value)
        End If
        'TextChanged()
        If fndUserCode.Value IsNot Nothing Then
            btnDelete.Enabled = True
        Else
            btnDelete.Enabled = False
        End If
        usercodeTextChanged()
    End Sub

    Private Sub fndUserCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndUserCode._MYValidating

        If isButtonClicked Then
            'Dim qry As String = "select User_Code  as [User Code],USER_NAME  as [User Name]from TSPL_USER_MASTER   "
            'fndUserCode.Value = clsCommon.ShowSelectForm("UserMaster_Code", qry, "User Code", "", fndUserCode.Value, "User_Code", isButtonClicked)
            fndUserCode.Value = clsUserMaster.getFinder("", fndUserCode.Value, isButtonClicked)
            txtUserName.Text = clsDBFuncationality.getSingleValue("Select user_name from tspl_user_master where user_code='" + fndUserCode.Value + "'")
            If fndUserCode.Value IsNot Nothing Then
                btnDelete.Enabled = True
            Else
                btnDelete.Enabled = False
            End If
            usercodeTextChanged()
        ElseIf fndUserCode.MyReadOnly OrElse fndUserCode.Value IsNot Nothing Then

            Dim qry As String = "Select * from TSPL_USER_MASTER where User_Code ='" + fndUserCode.Value + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt.Rows.Count <= 0 Then
                'txtUserName.Text = ""
                'txtPassword.Text = ""
                txtEmployeeName.Text = ""
                fndEmployeeCode.Value = Nothing
                btnSave.Text = "Save"
            Else
                usercodeTextChanged()
                fndUserCode.MyReadOnly = True
            End If
            'If ChkSuperUser = True AndAlso PanelCNF = True Then
            '    ' isInsideLoadData = True
            '    FillCustomerGrid(fndUserCode.Value)
            'End If
            If ChkSuperUser = True AndAlso PanelCNF = True Then
                If CmbLoginType.Text = "SuperUser" Or CmbLoginType.Text = "Super User" Then
                    RadPageViewPage2.Enabled = True
                    FillCustomerGrid(fndUserCode.Value)
                Else
                    RadPageViewPage2.Enabled = False
                    LoadBlankCustomerGrid()
                End If
            End If
        End If
    End Sub

    Public Shared Function GetSubbordinateUsers(ByVal strUserCode As String) As ArrayList
        Dim arrUser As New ArrayList
        Try
            Dim qry As String
            If clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal Then
                qry = "Select User_Code from TSPL_User_MASTER Where 1=1"
            Else
                qry = "Select User_Code from TSPL_User_MASTER Where Level4_Code='" + strUserCode + "' OR User_Code='" + strUserCode + "'"
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            For Each dr As DataRow In dt.Rows
                arrUser.Add(clsCommon.myCstr(dr("User_Code")))
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return arrUser
    End Function

    'Public Shared Function GetSubbordinateUsersQry(ByVal strUserCode As String) As String
    '    Dim qry As String = ""
    '    Try
    '        If clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal Then
    '            qry = "Select User_Code, User_Name from TSPL_User_MASTER Where 1=1 "
    '        Else
    '            qry = "Select User_Code, User_Name from TSPL_User_MASTER Where Level4_Code='" + strUserCode + "' OR User_Code='" + strUserCode + "'"
    '        End If
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    '    Return qry
    'End Function

    Private Sub txtDefaultLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDefaultLocation._MYValidating
        Try
            ' If isButtonClicked Then
            txtDefaultLocation.Value = clsLocation.getFinder("", txtDefaultLocation.Value, isButtonClicked)
            lblLocationName.Text = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_Location_Master where Location_Code='" + txtDefaultLocation.Value + "'")
            'End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndVendor__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndVendor._MYValidating
        Try
            Dim qry As String = "select TSPL_VENDOR_MASTER.Vendor_Code as Code,Vendor_Name as Name,TSPL_VENDOR_MASTER.add1 +case when len(TSPL_VENDOR_MASTER.add2)>0 then ', '+TSPL_VENDOR_MASTER.add2 else '' end +case when LEN(isnull(TSPL_VENDOR_MASTER.Add3,''))>0 then ', '+isnull(TSPL_VENDOR_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_VENDOR_MASTER.City_Code_Desc)>0 then ', '+TSPL_VENDOR_MASTER.City_Code_Desc else ' ' end + case when len(TSPL_VENDOR_MASTER.State )>0 then TSPL_VENDOR_MASTER.State else '' end  as Address,TSPL_VENDOR_MASTER.Terms_Code as [Term Code] ,TSPL_VENDOR_MASTER.Terms_Code_Desc as [Term Description] ,TSPL_VENDOR_MASTER.Tax_Group as [Tax Group],TSPL_VENDOR_MASTER.Tax_Group_Desc as [Tax Group Description],TSPL_VENDOR_MASTER.form_type as [Vendor Type],TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [Uploader No], TSPL_VLC_MASTER_HEAD.MCC as [MCC],TSPL_MCC_MASTER.MCC_NAME as [MCC Name]
from TSPL_VENDOR_MASTER 
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_MASTER.Vendor_Code
left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC"

            fndVendor.Value = clsCommon.ShowSelectForm("VendorFndr", qry, "Code", "", fndVendor.Value, "Code", isButtonClicked)
            If clsCommon.myLen(fndVendor.Value) > 0 Then
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("Select Vendor_Name from TSPL_VENDOR_MASTER where vendor_code='" + fndVendor.Value + "' ")
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    lblVendorName.Text = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub LoadAppUserSaleType()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Milk"
        dr("Name") = "Milk"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Product"
        dr("Name") = "Product"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Both"
        dr("Name") = "Both"
        dt.Rows.Add(dr)

        CmbAppUserSaleType.DataSource = dt
        CmbAppUserSaleType.ValueMember = "Code"
        CmbAppUserSaleType.DisplayMember = "Name"
    End Sub

    Private Sub LoadType()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "CNF"
        dr("Name") = "CNF"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Distributer"
        dr("Name") = "Distributer"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Retailer"
        dr("Name") = "Retailer"
        dt.Rows.Add(dr)
        If ChkSuperUser = True Then
            dr = dt.NewRow()
            dr("Code") = "SuperUser"
            dr("Name") = "Super User"
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Code") = "Driver"
            dr("Name") = "Driver"
            dt.Rows.Add(dr)
        End If

        dr = dt.NewRow()
        dr("Code") = "Parlor"
        dr("Name") = "Parlor"
        dt.Rows.Add(dr)

        CmbLoginType.DataSource = dt
        CmbLoginType.ValueMember = "Code"
        CmbLoginType.DisplayMember = "Name"
    End Sub

    Private Sub CmbLoginType_SelectedIndexChanged(sender As Object, e As Data.PositionChangedEventArgs) Handles CmbLoginType.SelectedIndexChanged
        Try
            If clsCommon.myLen(CmbLoginType.Text) > 0 Then
                fndCustCode.MyReadOnly = False
                fndDisRetailerCode.MyReadOnly = False
            End If
            If CmbLoginType.Text = "Select" Then
                fndCustCode.MyReadOnly = False
                fndDisRetailerCode.MyReadOnly = False
            End If
            If PanelCNF = True Then
                If CmbLoginType.Text = "CNF" Then
                    fndCustCode.MyReadOnly = True
                    fndDisRetailerCode.MyReadOnly = False
                End If
                If CmbLoginType.Text = "Distributer" Then
                    fndCustCode.MyReadOnly = False
                    fndDisRetailerCode.MyReadOnly = True
                    lblDisRetailer.Text = "Distributer"
                End If

                If CmbLoginType.Text = "Retail" Then
                    fndCustCode.MyReadOnly = False
                    fndDisRetailerCode.MyReadOnly = True
                    lblDisRetailer.Text = "Retail"
                End If

                If ChkSuperUser = True Then
                    fndCustCode.MyReadOnly = True
                    fndDisRetailerCode.MyReadOnly = True
                    lblDisRetailer.Text = "Distributer"
                End If

                If ChkSuperUser = True AndAlso PanelCNF = True Then
                    If CmbLoginType.Text = "SuperUser" Or CmbLoginType.Text = "Super User" Then
                        RadPageViewPage2.Enabled = True
                        FillCustomerGrid(fndUserCode.Value)
                    Else
                        RadPageViewPage2.Enabled = False
                        LoadBlankCustomerGrid()
                    End If
                End If
            ElseIf isCheckCustomerType = True Then
                MatchLevel()
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try


    End Sub

    Private Sub fndDisRetailerCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndDisRetailerCode._MYValidating
        If PanelCNF = True And (CmbLoginType.Text = "Distributer" Or CmbLoginType.Text = "Retailer") Then
            Try
                Dim qry As String = " select Cust_Code as Code, Customer_Name as Name from TSPL_SECONDARY_CUSTOMER_MASTER "
                fndDisRetailerCode.Value = clsCommon.ShowSelectForm("Fndr", qry, "Code", "", fndDisRetailerCode.Value, "Code", isButtonClicked)
                If clsCommon.myLen(fndDisRetailerCode.Value) > 0 Then
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable("Select Customer_Name from TSPL_SECONDARY_CUSTOMER_MASTER where Cust_Code='" + fndDisRetailerCode.Value + "' ")
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        lblRetailerCode.Text = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
                    End If
                End If
            Catch ex As Exception
                clsCommon.MyMessageBoxShow(ex.Message)
            End Try
        ElseIf isCheckCustomerType = True And (CmbLoginType.Text = "Distributer" Or CmbLoginType.Text = "Retailer") Then
            Try
                Dim qry As String = " select Cust_Code as Code, Customer_Name as Name from TSPL_SECONDARY_CUSTOMER_MASTER "
                fndDisRetailerCode.Value = clsCommon.ShowSelectForm("Fndr1", qry, "Code", " POS_type='" + CmbLoginType.SelectedValue + "' ", fndDisRetailerCode.Value, "Code", isButtonClicked)
                If clsCommon.myLen(fndDisRetailerCode.Value) > 0 Then
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable("Select Customer_Name from TSPL_SECONDARY_CUSTOMER_MASTER where Cust_Code='" + fndDisRetailerCode.Value + "' ")
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        lblRetailerCode.Text = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
                    End If
                End If
            Catch ex As Exception
                clsCommon.MyMessageBoxShow(ex.Message)
            End Try
        End If
    End Sub

    Private Sub fndCustCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCustCode._MYValidating
        If (PanelCNF = True And (CmbLoginType.Text = "CNF" Or CmbLoginType.Text = "Parlor")) OrElse (isCheckCustomerType AndAlso MatchLevel() = 1) Then
            Try
                Dim qry As String = "select Cust_Code as Code, Customer_Name as Name from TSPL_CUSTOMER_MASTER "
                fndCustCode.Value = clsCommon.ShowSelectForm("CustomerFndr", qry, "Code", "STATUS='N'", fndCustCode.Value, "Code", isButtonClicked)
                If clsCommon.myLen(fndCustCode.Value) > 0 Then
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(" Select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" + fndCustCode.Value + "' ")
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                        lblCustCode.Text = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
                    End If
                End If
            Catch ex As Exception
                clsCommon.MyMessageBoxShow(ex.Message)
            End Try
        End If
    End Sub

    Private Sub CmbLoginType_SelectedValueChanged(sender As Object, e As EventArgs) Handles CmbLoginType.SelectedValueChanged
        Try
            If PanelCNF = True Then
                PanalCNFReset()
                If clsCommon.myLen(CmbLoginType.Text) < 0 Then
                    fndCustCode.MyReadOnly = False
                    fndDisRetailerCode.MyReadOnly = False
                End If
                If CmbLoginType.Text = "Select" Then
                    fndCustCode.MyReadOnly = True
                    fndDisRetailerCode.MyReadOnly = True
                End If
                If CmbLoginType.Text = "CNF" Then
                    fndCustCode.MyReadOnly = False
                    fndDisRetailerCode.MyReadOnly = True
                End If
                If CmbLoginType.Text = "Distributer" Then
                    fndCustCode.MyReadOnly = True
                    fndDisRetailerCode.MyReadOnly = False
                    lblDisRetailer.Text = "Distributer"
                End If

                If CmbLoginType.Text = "Retail" Then
                    fndCustCode.MyReadOnly = True
                    fndDisRetailerCode.MyReadOnly = False
                    lblDisRetailer.Text = "Retail"

                End If
                If ChkSuperUser = True AndAlso PanelCNF = True Then

                    If CmbLoginType.Text = "SuperUser" Or CmbLoginType.Text = "Super User" Then
                        RadPageViewPage2.Enabled = True
                        FillCustomerGrid(fndUserCode.Value)
                    Else
                        RadPageViewPage2.Enabled = False
                        LoadBlankCustomerGrid()
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub PanalCNFReset()
        Try
            If PanelCNF = True Then
                fndCustCode.Value = ""
                fndDisRetailerCode.Value = ""
                lblCustCode.Text = ""
                lblRetailerCode.Text = ""


            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Sub LoadBlankCustomerGrid()
        gvCustomer.DataSource = Nothing
        gvCustomer.Rows.Clear()
        gvCustomer.Columns.Clear()

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 100
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvCustomer.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.FormatString = ""
        repoSelect.HeaderText = "Select"
        repoSelect.Name = colSelect
        repoSelect.Width = 100
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvCustomer.MasterTemplate.Columns.Add(repoSelect)

        Dim repoCCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCCode.FormatString = ""
        repoCCode.HeaderText = "Customer Code"
        repoCCode.Name = colCustCode
        repoCCode.HeaderImage = XpertERPEngine.My.Resources.Resources.search4
        repoCCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoCCode.Width = 150
        gvCustomer.MasterTemplate.Columns.Add(repoCCode)

        Dim repoCName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCName.FormatString = ""
        repoCName.HeaderText = "Customer Name"
        repoCName.Name = colCustName
        repoCName.Width = 300
        repoCName.ReadOnly = True
        gvCustomer.MasterTemplate.Columns.Add(repoCName)

        gvCustomer.AllowDeleteRow = True
        gvCustomer.AllowAddNewRow = False
        gvCustomer.ShowGroupPanel = False
        gvCustomer.AllowColumnReorder = False
        gvCustomer.AllowRowReorder = False
        gvCustomer.EnableSorting = False
        gvCustomer.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvCustomer.MasterTemplate.ShowRowHeaderColumn = False
        gvCustomer.TableElement.TableHeaderHeight = 40
    End Sub

    Private Sub FillCustomerGrid(ByVal strCriteria As String)
        Try
            gvCustomer.DataSource = Nothing
            gvCustomer.Rows.Clear()
            gvCustomer.Columns.Clear()
            If clsCommon.myLen(fndUserCode.Value) > 0 Then
                qry = "Select ROW_NUMBER() Over (order by TSPL_CUSTOMER_MASTER.Cust_Code) as [LineNo], Cast(1 as bit) as [Select], TSPL_CUSTOMER_MASTER.Cust_Code as [Customer Code], Customer_Name as [Customer Name],Cast((case when TSPL_CUSTOMER_MASTER.Status='N' THEN 0 ELSE 1 END) as bit) AS [InActive] from TSPL_CUSTOMER_MASTER left outer join  TSPL_USER_CUSTOMER_MAPPING on  TSPL_USER_CUSTOMER_MAPPING.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code WHERE TSPL_USER_CUSTOMER_MAPPING.User_Code='" + strCriteria + "'"
                LoadCustomerData(clsDBFuncationality.GetDataTable(qry))

            End If
            'If gvCustomer.Rows.Count > 0 Then
            gvCustomer.Columns("LineNo").Width = 100
            gvCustomer.Columns("Select").Width = 100
            gvCustomer.Columns("Customer Code").Width = 150
            gvCustomer.Columns("Customer Name").Width = 300
            gvCustomer.Columns("LineNo").ReadOnly = True
            gvCustomer.Columns("Customer Name").ReadOnly = True
            gvCustomer.Columns("InActive").ReadOnly = True
            gvCustomer.Columns("InActive").Width = 100
            gvCustomer.Rows.AddNew()
            ' End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub LoadCustomerData(ByVal dt As DataTable)
        Try
            gvCustomer.DataSource = dt

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub gvCustomer_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvCustomer.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If e.Column Is gvCustomer.Columns(colCustCode) Then
                    qry = "Select Cust_Code as Code, Customer_Name as Name from TSPL_CUSTOMER_MASTER"
                    gvCustomer.CurrentRow.Cells(colCustCode).Value = clsCommon.ShowSelectForm("CustFinder@SM", qry, "Code", "STATUS='N'", gvCustomer.CurrentRow.Cells(colCustCode).Value, "Code", False)
                    'Dim custCode As String = Nothing
                    'Dim custName As String = Nothing
                    'custCode = gvCustomer.CurrentRow.Cells(colCustCode).Value
                    'If clsCommon.myLen(custCode) > 0 Then
                    '    custName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select  Customer_Name  from TSPL_CUSTOMER_MASTER where Cust_Code = '" + custCode + "'"))
                    'End If

                    'gvCustomer.CurrentRow.Cells(colCustName).Value = "hhhh"
                    gvCustomer.CurrentRow.Cells(3).Value = clsCommon.myCstr(clsCustomerMaster.GetName(gvCustomer.CurrentRow.Cells(colCustCode).Value, Nothing))
                    'gvCustomer.CurrentRow.Cells(colCustName).Value = clsDBFuncationality.getSingleValue("Select  Customer_Name  from TSPL_CUSTOMER_MASTER where Cust_Code = '" + custCode + "'")
                    If clsCommon.myLen(gvCustomer.CurrentRow.Cells(colCustCode).Value) > 0 Then
                        gvCustomer.CurrentRow.Cells(colSelect).Value = True
                    Else
                        gvCustomer.CurrentRow.Cells(colSelect).Value = False
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gvCustomer_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gvCustomer.CurrentColumnChanged
        If gvCustomer.RowCount > 0 Then
            Dim intCurrRow As Integer = gvCustomer.CurrentRow.Index
            gvCustomer.CurrentRow.Cells(colLineNo).Value = clsCommon.myCstr(clsCommon.myCdbl(intCurrRow + 1))
            If intCurrRow = gvCustomer.Rows.Count - 1 Then
                gvCustomer.Rows.AddNew()
                gvCustomer.CurrentRow = gvCustomer.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub SaveCustomerMapping()
        Try
            If CmbLoginType.Text = "SuperUser" Or CmbLoginType.Text = "Super User" Then
                clsDBFuncationality.ExecuteNonQuery(" delete from TSPL_USER_CUSTOMER_MAPPING where User_Code = '" + fndUserCode.Value + "' ")
                For Each grow As GridViewRowInfo In gvCustomer.Rows
                    If IsDBNull(grow.Cells(colSelect).Value) = False Then
                        If grow.Cells(colSelect).Value = True Then
                            Dim UserCode As String = Nothing
                            Dim CustomerCode As String = Nothing
                            Dim iSelect As Boolean = False

                            UserCode = clsCommon.myCstr(fndUserCode.Value)
                            CustomerCode = clsCommon.myCstr(grow.Cells(colCustCode).Value)
                            iSelect = clsCommon.myCBool(grow.Cells(colSelect).Value)
                            If iSelect Then
                                clsDBFuncationality.ExecuteNonQuery(" insert into TSPL_USER_CUSTOMER_MAPPING (User_Code,Cust_Code) values ( '" + UserCode + "', '" + CustomerCode + "' ) ")
                            End If

                        End If
                    End If

                Next
            End If


        Catch ex As Exception

            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub fndDepartment__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndDepartment._MYValidating
        Try
            Dim qry As String = "select Segment_code as Code,Description as Name from TSPL_GL_SEGMENT_CODE "
            fndDepartment.Value = clsCommon.ShowSelectForm("DeptFndr", qry, "Code", "Seg_No=3", fndDepartment.Value, "Code", isButtonClicked)
            If clsCommon.myLen(fndDepartment.Value) > 0 Then
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("Select Top 1 Description from TSPL_GL_SEGMENT_CODE where Seg_No=3 and Segment_code='" + fndDepartment.Value + "' ")
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    lblDepartmentName.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub PickDetailsFromPOSGroupMaster()
        Try
            Dim qry As String = "Select GROUP_CODE,DESCRIPTION from TSPL_POS_GROUP_MASTER order by LEVEL"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            CmbLoginType.DataSource = dt
            CmbLoginType.ValueMember = "GROUP_CODE"
            CmbLoginType.DisplayMember = "DESCRIPTION"
            CmbLoginType.Text = "Select"
        Catch ex As Exception
        End Try
    End Sub

    Private Function MatchLevel() As Decimal
        Dim level As Decimal
        Try
            Dim qry As String = "Select max(LEVEL) as level from TSPL_POS_GROUP_MASTER Where GROUP_CODE='" + CmbLoginType.SelectedValue + "'"
            level = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If level = 1 Then
                fndCustCode.Enabled = True
                fndDisRetailerCode.Enabled = False
                fndDisRetailerCode.Value = ""
                lblRetailerCode.Text = ""
            Else
                fndCustCode.Enabled = False
                fndDisRetailerCode.Enabled = True
                fndCustCode.Value = ""
                lblCustCode.Text = ""
            End If
            'Return level
        Catch ex As Exception
        End Try
        Return level
    End Function

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        If clsCommon.myLen(fndUserCode.Value) > 0 Then
            Dim frm As New frmFreeImage
            frm.Text = "Biometric Login for " + fndUserCode.Value
            frm.strCode = fndUserCode.Value
            frm.ShowDialog()
        End If
    End Sub



    'Public Shared Function SaveHistory(ByVal User_Code As String, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
    '    Try
    '        If isNewEntry = False Then
    '            If clsCommon.MyMessageBoxShow("Do you want to save Amendment history?", "", MessageBoxButtons.YesNo) = DialogResult.Yes Then
    '                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, User_Code, "TSPL_USER_MASTER", "User_Code", trans)
    '            End If
    '        End If
    '        Return True
    '    Catch ex As Exception
    '        Return False
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Function


    Private Sub btnGetHistory_Click(sender As Object, e As EventArgs) Handles btnGetHistory.Click
        Try
            If clsCommon.myLen(fndUserCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select User")
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowHistoryData(fndUserCode.Value, "USER_Code", "TSPL_USER_MASTER")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        If ChkSuperUser = True AndAlso PanelCNF = True Then
            sql = "   Select TSPL_USER_CUSTOMER_MAPPING.User_code as [User Code] ,TSPL_USER_MASTER.User_Name as [User Name] , TSPL_USER_MASTER.Login_Type as [Login Type] ,TSPL_USER_CUSTOMER_MAPPING.Cust_Code [Cust Code] from TSPL_USER_CUSTOMER_MAPPING left outer join TSPL_USER_MASTER on TSPL_USER_CUSTOMER_MAPPING.User_Code = TSPL_USER_MASTER.User_Code "
            transportSql.ExporttoExcel(sql, Me)
        End If
    End Sub
    ' rmImportCustomerMapping, RadMenuItem1
    Private Sub rmImportCustomerMapping_Click(sender As Object, e As EventArgs) Handles rmImportCustomerMapping.Click
        If ChkSuperUser = True AndAlso PanelCNF = True Then
            Dim gv As New RadGridView()
            Me.Controls.Add(gv)
            If transportSql.importExcel(gv, "User Code", "User Name", "Login Type", "Cust Code") Then
                Dim trans As SqlTransaction = Nothing

                Try
                    trans = clsDBFuncationality.GetTransactin()
                    Dim LineNo As Integer = 1
                    For Each grow As GridViewRowInfo In gv.Rows
                        Dim strUserCode As String = ""
                        Dim strLoginType As String = ""
                        Dim strCustomerCode As String = ""

                        Dim qry As String = ""
                        Dim chkValidUserCode As Boolean = False
                        Dim chkValidLogintype As Boolean = False
                        Dim chkValidCustomerCode As Boolean = False
                        strUserCode = clsCommon.myCstr(grow.Cells("User Code").Value)
                        strCustomerCode = clsCommon.myCstr(grow.Cells("Cust Code").Value)
                        strLoginType = clsCommon.myCstr(grow.Cells("Login Type").Value)

                        chkValidUserCode = clsCommon.myCBool(clsDBFuncationality.getSingleValue(" select count(*) from TSPL_USER_MASTER where USER_CODE = '" + strUserCode + "' ", trans))
                        chkValidLogintype = clsCommon.myCBool(clsDBFuncationality.getSingleValue(" select count(*) from TSPL_USER_MASTER where USER_CODE = '" + strUserCode + "'  and Login_Type = 'SuperUser'", trans))
                        chkValidCustomerCode = clsCommon.myCBool(clsDBFuncationality.getSingleValue("  select Count (*) from TSPL_CUSTOMER_MASTER where Cust_Code = '" + strCustomerCode + "'", trans))
                        If chkValidUserCode = False Then
                            common.clsCommon.MyMessageBoxShow("Invalid User Code (" + strUserCode + ") at Line no " + clsCommon.myCstr(LineNo) + ".")
                            'trans.Rollback()
                            Exit Sub
                        End If
                        If clsCommon.CompairString(strLoginType, "SuperUser") <> CompairStringResult.Equal Then
                            common.clsCommon.MyMessageBoxShow("Login type should be SuperUser For User Code (" + strUserCode + ") at Line no " + clsCommon.myCstr(LineNo) + ".")
                            'trans.Rollback()
                            Exit Sub
                        End If
                        If chkValidCustomerCode = False Then
                            common.clsCommon.MyMessageBoxShow("Invalid Cust Code (" + strCustomerCode + ") for User Code (" + strUserCode + ") at Line no " + clsCommon.myCstr(LineNo) + ".")
                            'trans.Rollback()
                            Exit Sub
                        End If
                        qry = "Select count (*) from TSPL_USER_CUSTOMER_MAPPING where User_Code = '" + strUserCode + "' and Cust_Code = '" + strCustomerCode + "'"
                        Dim isExist As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue(qry, trans))
                        If isExist = False Then
                            clsDBFuncationality.ExecuteNonQuery(" Update TSPL_USER_MASTER set Login_Type = 'SuperUser' where User_Code = '" + strUserCode + "'", trans)
                            clsDBFuncationality.ExecuteNonQuery(" insert into TSPL_USER_CUSTOMER_MAPPING (User_Code,Cust_Code) values ( '" + strUserCode + "', '" + strCustomerCode + "' ) ", trans)
                        End If
                        LineNo = LineNo + 1
                    Next

                    trans.Commit()
                    common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    myMessages.myExceptions(ex)
                    trans.Rollback()
                End Try

            End If
            Me.Controls.Remove(gv)
        End If
    End Sub
    Sub LoadBlankUserGrid()
        gvUser.DataSource = Nothing
        gvUser.Rows.Clear()
        gvUser.Columns.Clear()

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNoUser
        repoLineNo.Width = 100
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvUser.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.FormatString = ""
        repoSelect.HeaderText = "Select"
        repoSelect.Name = colSelectUser
        repoSelect.Width = 100
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvUser.MasterTemplate.Columns.Add(repoSelect)

        Dim repoCCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCCode.FormatString = ""
        repoCCode.HeaderText = "User Code"
        repoCCode.Name = colUserCode
        repoCCode.HeaderImage = XpertERPEngine.My.Resources.Resources.search4
        repoCCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoCCode.Width = 150
        gvUser.MasterTemplate.Columns.Add(repoCCode)

        Dim repoCName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCName.FormatString = ""
        repoCName.HeaderText = "User Name"
        repoCName.Name = colUserName
        repoCName.Width = 300
        repoCName.ReadOnly = True
        gvUser.MasterTemplate.Columns.Add(repoCName)

        gvUser.AllowDeleteRow = True
        gvUser.AllowAddNewRow = False
        gvUser.ShowGroupPanel = False
        gvUser.AllowColumnReorder = False
        gvUser.AllowRowReorder = False
        gvUser.EnableSorting = False
        gvUser.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvUser.MasterTemplate.ShowRowHeaderColumn = False
        gvUser.TableElement.TableHeaderHeight = 40
    End Sub
    Private Sub FillUserGrid(ByVal strCriteria As String)
        Try
            gvUser.DataSource = Nothing
            gvUser.Rows.Clear()
            gvUser.Columns.Clear()

            If clsCommon.myLen(fndUserCode.Value) > 0 Then
                qry = "Select ROW_NUMBER() Over (order by TSPL_User_master.User_Code) as [Line No], Cast(1 as bit) as [Select], TSPL_USER_MAPPING_DETAIL.Mapped_UserCode as [User Code], TSPL_User_master.User_Name  as [User Name] from TSPL_User_master left outer join  TSPL_USER_MAPPING_DETAIL on  TSPL_USER_MAPPING_DETAIL.Mapped_UserCode = TSPL_User_master.User_Code WHERE TSPL_USER_MAPPING_DETAIL.User_Code='" + strCriteria + "'"
                gvUser.DataSource = clsDBFuncationality.GetDataTable(qry)

            End If
            gvUser.Columns("Line No").Width = 100
            gvUser.Columns("Select").Width = 100
            gvUser.Columns("User Code").Width = 150
            gvUser.Columns("User Name").Width = 300
            gvUser.Columns("Line No").ReadOnly = True
            gvUser.Columns("User Name").ReadOnly = True

            gvUser.Rows.AddNew()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub


    Private Sub gvUser_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvUser.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If e.Column Is gvUser.Columns(colUserCode) Then
                    OpenUSerList(False)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Sub OpenUSerList(ByVal isButtonClick As Boolean)
        Dim qry As String = "Select User_Code as Code, User_Name as Name from TSPL_USER_MASTER"
        gvUser.CurrentRow.Cells(colUserCode).Value = clsCommon.ShowSelectForm("UserFinder@SM", qry, "Code", "", clsCommon.myCstr(gvUser.CurrentRow.Cells(colUserCode).Value), "Code", isButtonClick)

        gvUser.CurrentRow.Cells(colUserName).Value = clsCommon.myCstr(clsUserMaster.GetName(gvUser.CurrentRow.Cells(colUserCode).Value, Nothing))
        If clsCommon.myLen(gvUser.CurrentRow.Cells(colUserCode).Value) > 0 Then
            gvUser.CurrentRow.Cells(colSelectUser).Value = True
        Else
            gvUser.CurrentRow.Cells(colSelectUser).Value = False
        End If
    End Sub

    Private Sub gvUser_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gvUser.CurrentColumnChanged
        If gvUser.RowCount > 0 Then
            Dim intCurrRow As Integer = gvUser.CurrentRow.Index
            gvUser.CurrentRow.Cells(colLineNoUser).Value = clsCommon.myCstr(clsCommon.myCdbl(intCurrRow + 1))
            If intCurrRow = gvUser.Rows.Count - 1 Then
                gvUser.Rows.AddNew()
                gvUser.CurrentRow = gvUser.Rows(intCurrRow)
            End If
        End If
    End Sub

    'Private Sub gvUser_CurrentRowChanged(sender As Object, e As CurrentRowChangedEventArgs) Handles gvUser.CurrentRowChanged
    '    'If gvUser.RowCount > 0 Then
    '    '    Dim intCurrRow As Integer = gvUser.CurrentRow.Index
    '    '    gvUser.CurrentRow.Cells(colLineNoUser).Value = clsCommon.myCstr(clsCommon.myCdbl(intCurrRow + 1))
    '    '    If intCurrRow = gvUser.Rows.Count - 1 Then
    '    '        gvUser.Rows.AddNew()
    '    '        gvUser.CurrentRow = gvUser.Rows(intCurrRow)
    '    '    End If
    '    'End If
    'End Sub

    'Private Sub fndZone__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)
    '    Try
    '        Dim qry As String = "select Zone_Code as Code , Description as Name from TSPL_ZONE_MASTER "
    '        fndZone.Value = clsCommon.ShowSelectForm("Fnd@ZoneCode@UserMaster", qry, "Code", "", fndZone.Value, "Code", isButtonClicked)
    '        If clsCommon.myLen(fndZone.Value) > 0 Then
    '            Dim dt As DataTable = clsDBFuncationality.GetDataTable("Select Top 1 Description from TSPL_ZONE_MASTER where Zone_Code ='" + fndZone.Value + "' ")
    '            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '                lblzone.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
    '            End If
    '        End If
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub

    Private Sub mulZone__My_Click(sender As Object, e As EventArgs) Handles mulZone._My_Click
        Dim StrQry As String = "select Zone_Code as Code , Description as Name from TSPL_ZONE_MASTER"
        mulZone.arrValueMember = clsCommon.ShowMultipleSelectForm("mulZone@UserMaster", StrQry, "Code", "Name", mulZone.arrValueMember, mulZone.arrDispalyMember)
    End Sub

    Private Sub mulCustomerCategory__My_Click(sender As Object, e As EventArgs) Handles mulCustomerCategory._My_Click
        Dim StrQry As String = "Select 'Vendor' as Code  Union All Select 'Institution CR' as Code  Union All Select 'Institution SO' as Code  Union All Select 'Distributor' as Code  Union All Select 'Others' as Code "
        mulCustomerCategory.arrValueMember = clsCommon.ShowMultipleSelectForm("mulCustCategory@UserMaster", StrQry, "Code", "Code", mulCustomerCategory.arrValueMember, mulCustomerCategory.arrDispalyMember)
    End Sub

    Private Sub rmExportBlankSheetZone_Click(sender As Object, e As EventArgs) Handles rmExportBlankSheetZone.Click
        Dim str As String
        str = "select '' as [User Code]"
        For ii As Integer = 1 To 10
            str += ",'' as [Zone " + clsCommon.myCstr(ii) + "]"
        Next

        transportSql.ExporttoExcelWithoutFilter(str, "", "", Me)
    End Sub

    Private Sub rmExportBlankSheetCustomerCategory_Click(sender As Object, e As EventArgs) Handles rmExportBlankSheetCustomerCategory.Click
        Dim str As String
        str = "select '' as [User Code]"
        For ii As Integer = 1 To 5
            str += ",'' as [Customer Category " + clsCommon.myCstr(ii) + "]"
        Next

        transportSql.ExporttoExcelWithoutFilter(str, "", "", Me)
    End Sub

    Private Sub rmImportZone_Click(sender As Object, e As EventArgs) Handles rmImportZone.Click
        'Dim gv As New RadGridView()
        'Me.Controls.Add(gv)
        'Try
        '    Dim currentdate As Date = Date.Today
        '    Dim linno As Integer = 0
        '    Dim Strs As List(Of String) = New List(Of String)
        '    Strs.Add("User Code")

        '    For ii As Integer = 1 To 10
        '        Strs.Add("Zone " + clsCommon.myCstr(ii))
        '    Next
        '    If transportSql.importExcel(gv, Strs.ToArray()) Then
        '        Dim trans As SqlTransaction = Nothing
        '        Try
        '            trans = clsDBFuncationality.GetTransactin()
        '            clsCommon.ProgressBarShow()
        '            For Each grow As GridViewRowInfo In gv.Rows
        '                ' Dim obj As New clsCustomerDeductionHead()
        '                Dim strUserCcode As String = ""
        '                linno += 1
        '                strUserCcode = clsCommon.myCstr(grow.Cells("User Code").Value)
        '                If clsCommon.myLen(strUserCcode) > 0 Then
        '                    Dim isvalidUserId As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select Count (*) from tspl_User_Master where user_code = '" + strUserCcode + "'", trans))

        '                    If isvalidUserId = False Then
        '                        Throw New Exception("Invalid User Code. " + clsCommon.myCstr(linno) + ".")
        '                    End If
        '                    For ii As Integer = 1 To 10
        '                        Dim strZoneCode As String = clsCommon.myCstr(grow.Cells("Zone " + clsCommon.myCstr(ii)).Value)
        '                        If clsCommon.myLen(strZoneCode) > 0 Then
        '                            strZoneCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select zone_Code from TSPL_Zone_master  where zone_Code='" + strZoneCode + "'", trans))
        '                            If clsCommon.myLen(strZoneCode) <= 0 Then
        '                                Throw New Exception("Invalid Zone Code [" + clsCommon.myCstr(grow.Cells("Zone " + clsCommon.myCstr(ii)).Value) + "]")
        '                            Else
        '                                Dim isupdate As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select count (*) from TSPL_USER_CUSTOMER_ZONE where User_Code = '" + strUserCcode + "' and Zone_Code = '" + strZoneCode + "'  "))
        '                                If isupdate = False Then
        '                                    Dim collZone As New Hashtable()
        '                                    Dim strTRCode As String = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"), clsDocType.Detail, clsDocTransactionType.Detail, "")
        '                                    clsCommon.AddColumnsForChange(collZone, "TR_Code", strTRCode)
        '                                    clsCommon.AddColumnsForChange(collZone, "User_Code", strUserCcode)
        '                                    clsCommon.AddColumnsForChange(collZone, "Zone_Code", strZoneCode)
        '                                    clsCommonFunctionality.UpdateDataTable(collZone, "TSPL_USER_CUSTOMER_ZONE", OMInsertOrUpdate.Insert, "")
        '                                End If
        '                            End If
        '                        End If

        '                    Next

        '                End If
        '            Next
        '            trans.Commit()
        '            clsCommon.ProgressBarHide()
        '            clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
        '        Catch ex As Exception
        '            trans.Rollback()
        '            clsCommon.ProgressBarHide()
        '            Throw New Exception("Error at Line No" + clsCommon.myCstr(linno) + Environment.NewLine + ex.Message)
        '        End Try
        '    End If
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        'Finally
        '    Me.Controls.Remove(gv)
        'End Try
    End Sub

    Private Sub rmImportCustomerCategory_Click(sender As Object, e As EventArgs) Handles rmImportCustomerCategory.Click
        'Dim gv As New RadGridView()
        'Me.Controls.Add(gv)
        'Try
        '    Dim currentdate As Date = Date.Today
        '    Dim linno As Integer = 0
        '    Dim Strs As List(Of String) = New List(Of String)
        '    Strs.Add("User Code")

        '    For ii As Integer = 1 To 5
        '        Strs.Add("Customer Category " + clsCommon.myCstr(ii))
        '    Next
        '    If transportSql.importExcel(gv, Strs.ToArray()) Then
        '        Dim trans As SqlTransaction = Nothing
        '        Try
        '            trans = clsDBFuncationality.GetTransactin()
        '            clsCommon.ProgressBarShow()
        '            For Each grow As GridViewRowInfo In gv.Rows
        '                ' Dim obj As New clsCustomerDeductionHead()
        '                Dim strUserCcode As String = ""
        '                linno += 1
        '                strUserCcode = clsCommon.myCstr(grow.Cells("User Code").Value)
        '                If clsCommon.myLen(strUserCcode) > 0 Then
        '                    Dim isvalidUserId As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select Count (*) from tspl_User_Master where user_code = '" + strUserCcode + "'", trans))

        '                    If isvalidUserId = False Then
        '                        Throw New Exception("Invalid User Code. " + clsCommon.myCstr(linno) + ".")
        '                    End If
        '                    For ii As Integer = 1 To 5
        '                        Dim strZoneCode As String = clsCommon.myCstr(grow.Cells("Customer Category " + clsCommon.myCstr(ii)).Value)
        '                        If clsCommon.myLen(strZoneCode) > 0 Then
        '                            'strZoneCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select zone_Code from TSPL_Zone_master  where zone_Code='" + strZoneCode + "'", trans))
        '                            If clsCommon.CompairString(strZoneCode, "Vendor") = CompairStringResult.Equal OrElse clsCommon.CompairString(strZoneCode, "Institution CR") = CompairStringResult.Equal OrElse clsCommon.CompairString(strZoneCode, "Institution SO") = CompairStringResult.Equal OrElse clsCommon.CompairString(strZoneCode, "Distributor") = CompairStringResult.Equal OrElse clsCommon.CompairString(strZoneCode, "Others") = CompairStringResult.Equal Then
        '                                Throw New Exception("Invalid Customer Category [" + clsCommon.myCstr(grow.Cells("Customer Category " + clsCommon.myCstr(ii)).Value) + "]. Customer Category Should be 'Vendor'OR 'Institution CR' OR 'Institution SO' OR 'Distributor' OR 'Others'")
        '                            Else
        '                                Dim isupdate As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select count (*) from TSPL_USER_CUSTOMER_CATEGORY where User_Code = '" + strUserCcode + "' and Customer_Category = '" + strZoneCode + "'  "))
        '                                If isupdate = False Then
        '                                    Dim collZone As New Hashtable()
        '                                    Dim strTRCode As String = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"), clsDocType.Detail, clsDocTransactionType.Detail, "")
        '                                    clsCommon.AddColumnsForChange(collZone, "TR_Code", strTRCode)
        '                                    clsCommon.AddColumnsForChange(collZone, "User_Code", strUserCcode)
        '                                    clsCommon.AddColumnsForChange(collZone, "Customer_Category", strZoneCode)
        '                                    clsCommonFunctionality.UpdateDataTable(collZone, "TSPL_USER_CUSTOMER_CATEGORY", OMInsertOrUpdate.Insert, "")
        '                                End If
        '                            End If
        '                        End If

        '                    Next

        '                End If
        '            Next
        '            trans.Commit()
        '            clsCommon.ProgressBarHide()
        '            clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
        '        Catch ex As Exception
        '            trans.Rollback()
        '            clsCommon.ProgressBarHide()
        '            Throw New Exception("Error at Line No" + clsCommon.myCstr(linno) + Environment.NewLine + ex.Message)
        '        End Try
        '    End If
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        'Finally
        '    Me.Controls.Remove(gv)
        'End Try
    End Sub

    Private Sub ChkInActive_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkInActive.ToggleStateChanged
        If chkInActive.Checked = True Then
            dtInActive.Enabled = True
        ElseIf chkInActive.Checked = False Then
            dtInActive.Enabled = False
            dtInActive.Value = connectSql.serverDate()
        End If
    End Sub

    Private Sub txtRoute__My_Click(sender As Object, e As EventArgs) Handles txtRoute._My_Click
        Dim StrQry As String = "Select TSPL_ROUTE_MASTER.Route_No as Code,Route_Desc as Name from TSPL_ROUTE_MASTER"
        txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("mulRoute@UserMaster", StrQry, "Code", "Name", txtRoute.arrValueMember, txtRoute.arrDispalyMember)
    End Sub

    Private Sub txtMP__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtMP._MYValidating
        txtMP.Value = clsMpMaster.getFinder("", txtMP.Value, isButtonClicked)
        lblMP.Text = clsMpMaster.GetName(txtMP.Value, Nothing)
    End Sub

    Private Sub SaveUserMapping()
        Try
            clsDBFuncationality.ExecuteNonQuery(" delete from TSPL_USER_MAPPING_DETAIL where User_Code = '" + fndUserCode.Value + "' ")
            For Each grow As GridViewRowInfo In gvUser.Rows
                If IsDBNull(grow.Cells(colSelectUser).Value) = False Then
                    If grow.Cells(colSelectUser).Value = True Then
                        Dim UserCode As String = Nothing
                        Dim MappedUserCode As String = Nothing
                        Dim iSelect As Boolean = False

                        UserCode = clsCommon.myCstr(fndUserCode.Value)
                        MappedUserCode = clsCommon.myCstr(grow.Cells(colUserCode).Value)
                        iSelect = clsCommon.myCBool(grow.Cells(colSelectUser).Value)
                        If iSelect Then
                            clsDBFuncationality.ExecuteNonQuery(" insert into TSPL_USER_MAPPING_detail (User_Code,Mapped_UserCode) values ( '" + UserCode + "', '" + MappedUserCode + "' ) ")
                        End If

                    End If
                End If

            Next
        Catch ex As Exception

            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try

    End Sub


End Class
