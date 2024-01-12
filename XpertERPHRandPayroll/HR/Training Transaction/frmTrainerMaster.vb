Imports Microsoft.VisualBasic
Imports System
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
Imports XpertERPEngine
'created by --> Rohit
'createddate --> 22/08/2014

Public Class frmTrainerMaster
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
    Dim str As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()

    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub

#Region "Page Load"
    Private Sub frmTrainerMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        funreset()
        SetLength()
        pageCus.SelectedPage = RadPageViewPage1
        SetUserMgmtNew()

        ToolTipvendor.SetToolTip(btnnew, "New")
       
        fndcity_text_Changed()
        fndCity_leave()
        
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclear, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New Trasnaction")

    End Sub

    Public Sub SetLength()
        fndvendorNo.MyMaxLength = 12
        txtName.MaxLength = 50
        txtAdd1.MaxLength = 50
        txtAdd2.MaxLength = 50
        txtAdd3.MaxLength = 50
        txtPhone1.MaxLength = 20
        txtPhone2.MaxLength = 20
        txtfax.MaxLength = 20
        txtEmail.MaxLength = 50
        txtLastName.MaxLength = 50
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.TRAINER_MASTER)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow(Me, "Permission Denied", Me.Text)
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub
#End Region

#Region "Function"

    'It will fill the  controls if value exist in database according to fndCity
    Public Sub funfilfndcity()
        Try

            Dim strquery As String = "select City_Code ,city_Name from TSPL_City_MASTER where City_code = '" + fndCity.Value + "'"
            Dim dr As DataTable
            ' Dim strvalue As String
            dr = clsDBFuncationality.GetDataTable(strquery)
            If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                txtCity.Text = dr.Rows(0)("city_Name").ToString()
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

   
    Public Sub LoadData(ByVal strDoc As String, ByVal navType As NavigatorType)
        Try
            ' funreset()
            btnsave.Enabled = True
            BtnPost.Enabled = True
            btndelete.Enabled = True
            btnsave.Text = "Save"
            Dim obj As clsTrainerMaster = clsTrainerMaster.GetData(strDoc, navType)
            If obj IsNot Nothing Then
                fndvendorNo.Value = clsCommon.myCstr(obj.Code)
                txtName.Text = clsCommon.myCstr(obj.Name)
                CmbType.Text = clsCommon.myCstr(obj.Type)
                fndInstitutecode.Value = clsCommon.myCstr(obj.Institute_Code)
                TxtFirstName.Text = clsCommon.myCstr(obj.First_name)
                txtLastName.Text = clsCommon.myCstr(obj.Last_Name)
                FndEployeeCode.Value = clsCommon.myCstr(obj.Emp_Code)
                txtEmail.Text = clsCommon.myCstr(obj.Email_Id)
                CmbPaymentType.SelectedValue = clsCommon.myCstr(obj.Payment_type)
                txtAdd1.Text = clsCommon.myCstr(obj.Address1)
                txtAdd2.Text = clsCommon.myCstr(obj.Address2)
                txtAdd3.Text = clsCommon.myCstr(obj.Address3)
                fndCity.Value = clsCommon.myCstr(obj.City)
                txtstatecode.Value = clsCommon.myCstr(obj.State)
                txtcountrycode.Value = clsCommon.myCstr(obj.Country)

                TxtPinCode.Text = clsCommon.myCstr(obj.Pin_Code)
                txtPhone1.Text = clsCommon.myCstr(obj.PhoneNo1)
                txtPhone2.Text = clsCommon.myCstr(obj.PhoneNo2)
                CmbGender.Text = clsCommon.myCstr(obj.Gendor)
                dtpDoB.Value = clsCommon.myCDate(obj.DOB)
                TxtRemark.Text = clsCommon.myCstr(obj.Remark)
                chkIsApplicable.Checked = clsCommon.myCBool(IIf(obj.Is_Applicable = "1", True, False))
                txtfax.Text = clsCommon.myCstr(obj.Fax)
                If clsCommon.CompairString(CmbType.Text, "External") = CompairStringResult.Equal Then
                    txtempext.Visible = True
                    LblEmp.Text = "External Employee"
                    FndEployeeCode.Visible = False
                    LblEmployeeName.Visible = False
                Else
                    txtempext.Visible = False
                    LblEmp.Text = "Employee Code"
                    FndEployeeCode.Visible = True
                    LblEmployeeName.Visible = True
                End If
                If obj.POSTED = ERPTransactionStatus.Approved Then
                    btnsave.Enabled = False
                    BtnPost.Enabled = False
                    btndelete.Enabled = False
                End If
                UsLock1.Status = obj.POSTED

                LblEmployeeName.Text = clsCommon.myCstr(obj.Employee_Name)
                LblInstituteName.Text = clsCommon.myCstr(obj.Institute_Name)
                txtCity.Text = clsCommon.myCstr(obj.City_Name)
                txtState.Text = clsCommon.myCstr(obj.State_name)
                txtCountry.Text = clsCommon.myCstr(obj.Country_Name)

                LoadCity()
                LoadCourse()
                LoadQualification()



                gvQualification.CheckedValue = clsTrainerMaster.ArrQualification_Arr
                gvTrainingCities.CheckedValue = clsTrainerMaster.ArrCities_Arr
                gvTrainingCourse.CheckedValue = clsTrainerMaster.ArrCourse_Arr


                fndvendorNo.Value = obj.Code

                btnsave.Text = "Update"
                'btndelete.Enabled = True
            End If
            
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    'this function will reset all the fields for new entry
    Public Sub funreset()
        txtcountrycode.Value = ""
        txtstatecode.Value = ""
        Me.fndvendorNo.Value = ""
        fndInstitutecode.Value = Nothing
        LblInstituteName.Text = ""
        FndEployeeCode.Value = Nothing
        LblEmployeeName.Text = ""
        dtpDoB.Value = clsCommon.GETSERVERDATE()
        Me.txtName.Text = ""
        Me.fndInstitutecode.Value = ""
        chkIsApplicable.Checked = False
        TxtFirstName.Text = ""
        TxtPinCode.Text = ""
        TxtRemark.Text = ""
        Me.txtAdd1.Text = ""
        Me.txtAdd2.Text = ""
        Me.txtAdd3.Text = ""
        Me.fndCity.Value = ""
        Me.txtCity.Text = ""
        Me.txtState.Text = ""
        Me.txtCountry.Text = ""
        Me.txtPhone1.Text = ""
        Me.txtPhone2.Text = ""
        Me.txtfax.Text = ""
        Me.txtEmail.Text = ""

        Me.txtLastName.Text = ""

        '' Anubhooti 4-Aug-2014 BM00000003319
        LoadPaymentType()
        LoadCity()
        LoadCourse()
        LoadQualification()

        If clsCommon.CompairString(CmbType.Text, "External") = CompairStringResult.Equal Then
            txtempext.Visible = True
            LblEmp.Text = "External Employee"
            FndEployeeCode.Visible = False
            LblEmployeeName.Visible = False
        Else
            txtempext.Visible = False
            LblEmp.Text = "Employee Code"
            FndEployeeCode.Visible = True
            LblEmployeeName.Visible = True
        End If
        btnsave.Text = "Save"
        btndelete.Enabled = False
        btnsave.Enabled = True
        BtnPost.Enabled = False

        UsLock1.Status = ERPTransactionStatus.Pending
        txtName.Focus()

        fndvendorNo.MyReadOnly = False
        UcAttachment1.Form_ID = Me.Form_ID
        UcAttachment1.BlankAllControls()
        ''For Custom Fields

        ''End of For Custom Fields

    End Sub

    Public Sub funExport()
        Try


            Dim strCmd As String = "select count(*) from tspl_vendor_master where form_type='VSP'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(strCmd)

            Dim whrCls As String = ""

            If check > 0 Then
                'strCmd = " SELECT [Vendor_Code] as [Vendor No.],[Vendor_Name] as [Vendor Name] ,[Vendor_Group_Code] as [Group Code],[Terms_Code] as [Terms Code],[Ven_Type_Code] as [Vendor Type]" & _
                '    " ,[Bank_Code] as [Bank Code],[Vendor_Account] as [Account Set],[Tax_Group] as [Tax Group] FROM [TSPL_Vendor_MASTER]"
                'strCmd = "select Vendor_Code as [Vendor No],Vendor_Name as[Vendor Name],Vendor_Group_Code as [Group Code],Vendor_Group_Code_Desc as [Vendor Group Description],Terms_Code as [Terms Code],Terms_Code_Desc as [Terms Description],Ven_Type_Code as [Vendor Type],Ven_Type_Desc as [Vendor Type Description],Bank_Code as [Bank Code],Bank_Code_Desc as [Bank Code Description],Vendor_Account as [Account Set],Vendor_Account_Desc as [Account Set Description],Tax_Group as [Tax Group],Tax_Group_Desc as [Tax Group Description] from TSPL_VENDOR_MASTER "
                strCmd = "select Vendor_Code as [VSP No],Vendor_Name as[VSP Name],Add1 as [Address1],Add2 as [Address2]" & _
                          ",Add3 as [Address3],Vendor_Group_Code as [Group Code]" & _
                          ",Vendor_Group_Code_Desc as [Vendor Group Description],City_Code as [City Code]" & _
                          ",City_Code_Desc as [City Code Description],State as [State],Country as [Country],Phone1 as [Phone Num1]" & _
                          ",Phone2 as [Phone Num2],Fax as [Fax],Email as [Email Id],WebSite as [Website]" & _
                          ",Terms_Code as [Terms Code],Terms_Code_Desc as [Terms Description],Vendor_Account as [Vendor Account]" & _
                           ",Vendor_Account_Desc as [Vendor Account Description],Payment_Code as [Payment Code]" & _
                           ",Payment_Code_Desc as [Paymnet Code Description],Bank_Code as [Bank Code],Bank_Code_Desc as [Bank Code Description]" & _
                           ",Ven_Type_Code as [Vendor Type],Ven_Type_Desc as [Vendor Type Description]" & _
                           ",Tax_Group as [Tax Group],Tax_Group_Desc as [Tax Group Description]" & _
                           ",TAX1 as [Tax1],TAX1_Rate as [Tax1 Rate],TAX2 as [Tax2],TAX2_Rate as [Tax2 Rate],TAX3 as [Tax3]" & _
                           ",TAX3_Rate as [Tax3 Rate],TAX4 as [Tax4],TAX4_Rate as [Tax4 Rate],TAX5 as [Tax5]" & _
                           ",TAX5_Rate as [Tax5 Rate],TAX6 as [Tax6],TAX6_Rate as [Tax6 Rate],TAX7 as [Tax7]" & _
                           ",TAX7_Rate as [Tax7 Rate], TAX8 as [Tax8],TAX8_Rate as [Tax8 Rate],TAX9 as [Tax9]" & _
                           ",TAX9_Rate as [Tax9 Rate],TAX10 as [Tax10],TAX10_Rate as [Tax10 Rate]" & _
                           ",Transporter as [Transporter]" & _
                           ",Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By]" & _
                           ",Modify_Date as [Modify Date],comp_code as [Company Code]" & _
                           ",Collectorate as [Collectorate],PAN as [PAN],State_Code,Country_Code,service_charges,Service_Charge_Type,commision_pers,payment_commision_pers,incentive,incentive_days,vsp_payment,VSP_Payee_Name,Joint_Name, Branch_Name,Account_No,Bank_Name,IFSC_Code ,Account_Type,form_type from TSPL_VENDOR_MASTER "

                whrCls = " and form_type='VSP'"
            Else
                whrCls = ""
                strCmd = "select '' as [VSP No],'' as[VSP Name],'' as [Address1],'' as [Address2]" & _
                          ",'' as [Address3],'' as [Closing Date],'' as [Group Code]" & _
                          ",'' as [Vendor Group Description],'' as [City Code]" & _
                          ",'' as [City Code Description],'' as [State],'' as [Country],'' as [Phone Num1]" & _
                          ",'' as [Phone Num2],'' as [Fax],'' as [Email Id],'' as [Website]" & _
                          ",'' as [Terms Code],'' as [Terms Description],'' as [Vendor Account]" & _
                           ",'' as [Vendor Account Description],'' as [Payment Code]" & _
                           ",'' as [Paymnet Code Description],'' as [Bank Code],'' as [Bank Code Description]" & _
                           ",'' as [Vendor Type],'' as [Vendor Type Description]" & _
                           ",'' as [Tax Group],'' as [Tax Group Description]" & _
                           ",'' as [Tax1],'' as [Tax1 Rate],'' as [Tax2],'' as [Tax2 Rate],'' as [Tax3]" & _
                           ",'' as [Tax3 Rate],'' as [Tax4],'' as [Tax4 Rate],'' as [Tax5]" & _
                           ",'' as [Tax5 Rate],'' as [Tax6],'' as [Tax6 Rate],'' as [Tax7]" & _
                           ",'' as [Tax7 Rate], '' as [Tax8],'' as [Tax8 Rate],'' as [Tax9]" & _
                           ",'' as [Tax9 Rate],'' as [Tax10],'' as [Tax10 Rate]" & _
                           ",'' as [Transporter]" & _
                           ",'' as [Created By],'' as [Created Date],'' as [Modify By]" & _
                           ",'' as [Modify Date],'' as [Company Code]" & _
                           ",'' as [Collectorate],'' as [PAN],'' as State_Code,'' as Country_Code,'' as service_charges,'' as Service_Charge_Type,'' as commision_pers,'' as payment_commision_pers,'' as incentive,'' as incentive_days,'' as vsp_payment,'' as VSP_Payee_Name,'' as Joint_Name,'' AS Branch_Name,'' As Account_No,'' As Bank_Name,'' As IFSC_Code ,'' As Account_Type,'VSP' as form_type"
            End If
            transportSql.ExporttoExcel(strCmd, whrCls, Me)


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Customer")
        End Try

    End Sub

    Public Sub funImport()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim Count As String = ""
        If transportSql.importExcel(gv, "VSP No", "VSP Name", "Address1", "Address2", "Address3", "Group Code", "Vendor Group Description", "City Code", "City Code Description", "State", "Country", "Phone Num1", "Phone Num2", "Fax", "Email Id", "Website", "Terms Code", "Terms Description", "Vendor Account", "Vendor Account Description", "Payment Code", "Paymnet Code Description", "Bank Code", "Bank Code Description", "Vendor Type", "Vendor Type Description", "Tax Group", "Tax Group Description", "Tax1", "Tax1 Rate", "Tax2", "Tax2 Rate", "Tax3", "Tax3 Rate", "Tax4", "Tax4 Rate", "Tax5", "Tax5 Rate", "Tax6", "Tax6 Rate", "Tax7", "Tax7 Rate", "Tax8", "Tax8 Rate", "Tax9", "Tax9 Rate", "Tax10", "Tax10 Rate", "Transporter", "Created By", "Created Date", "Modify By", "Modify Date", "Company Code", "Collectorate", "PAN", "State_Code", "Country_Code", "service_charges", "Service_Charge_Type", "commision_pers", "payment_commision_pers", "incentive", "incentive_days", "vsp_payment", "VSP_Payee_Name", "Joint_Name", "form_type", "Branch_Name", "Account_No", "Bank_Name", "IFSC_Code", "Account_Type") Then
            Dim trans As SqlTransaction = Nothing
            Try
                connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                Dim counter As Integer = 1

                For Each grow As GridViewRowInfo In gv.Rows
                    Count = clsCommon.myCstr(grow.Index + 2)
                    Dim strvendorNo As String = clsCommon.myCstr(grow.Cells("VSP No").Value)
                    If strvendorNo.Length > 12 Then
                        Throw New Exception("Check the length of VSP No.,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    If String.IsNullOrEmpty(strvendorNo) Then
                        Throw New Exception("VSP No. can not be blank,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    Dim strvendorname1 As String = clsCommon.myCstr(grow.Cells("VSP Name").Value)
                    Dim strvendorname As String = strvendorname1.Replace("'", "''")
                    If strvendorname.Length > 100 Then
                        Throw New Exception("Length of VSP Name can not be greater than 100.,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If String.IsNullOrEmpty(strvendorname) Then
                        Throw New Exception("VSP Name can not be blank,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    Dim add1 As String = clsCommon.myCstr(grow.Cells("Address1").Value)
                    Dim add2 As String = clsCommon.myCstr(grow.Cells("Address2").Value)
                    Dim add3 As String = clsCommon.myCstr(grow.Cells("Address3").Value)
                    Dim closing_date As String = System.DateTime.Now.Date

                    'If clsCommon.myCstr(grow.Cells("Closing Date").Value) = Nothing Then
                    '    closing_date = System.DateTime.Now.Date
                    'Else
                    '    closing_date = clsCommon.myCstr(grow.Cells("Closing Date").Value)
                    'End If



                    Dim strgroupCode As String = clsCommon.myCstr(grow.Cells("Group Code").Value)
                    If String.IsNullOrEmpty(strgroupCode) Then
                        Throw New Exception(" Group Code can not be blank,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    Dim i As Integer
                    Dim qry As String = "select Count(*)from TSPL_VENDOR_GROUP  where Ven_Group_Code ='" + strgroupCode + "'"
                    i = connectSql.RunScalar(trans, qry)
                    If i = 0 Then
                        Throw New Exception("Vendor Group Code does not exist : " + strgroupCode + ",See At Line No. " + clsCommon.myCstr(counter) + "")
                    Else
                    End If
                    If strgroupCode.Length > 12 Then
                        Throw New Exception("Check the length of Group Code,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    Dim strgroupDes As String = grow.Cells("Vendor Group Description").Value.ToString()
                    If strgroupDes.Length > 50 Then
                        Throw New Exception("Check the length of Group Code Description,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    Dim citycode As String = clsCommon.myCstr(grow.Cells("City Code").Value)
                    Dim citycodedesc As String = clsCommon.myCstr(grow.Cells("City Code Description").Value)
                    Dim state As String = clsCommon.myCstr(grow.Cells("State").Value)
                    Dim country As String = clsCommon.myCstr(grow.Cells("Country").Value)

                    Dim statecode As String = clsCommon.myCstr(grow.Cells("state_code").Value)
                    Dim countrycode As String = clsCommon.myCstr(grow.Cells("country_code").Value)
                    Dim check As Integer = 0

                    If clsCommon.myLen(countrycode) <= 0 Then
                        Throw New Exception("Please Fill Country,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.myLen(statecode) <= 0 Then
                        Throw New Exception("Please Fill State,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.myLen(citycode) <= 0 Then
                        Throw New Exception("Please Fill City,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    If clsCommon.myLen(countrycode) > 0 Then
                        qry = "select count(*) from tspl_country_master where country_code='" + countrycode + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)

                        If check <= 0 Then
                            Throw New Exception("Country Code Does Not Exist,Please Make Country Master,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    End If

                    If clsCommon.myLen(statecode) > 0 AndAlso clsCommon.myLen(countrycode) > 0 Then
                        qry = "select count(*) from tspl_state_master where country_code='" + countrycode + "' and state_code='" + statecode + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)

                        If check <= 0 Then
                            Throw New Exception("State Code Does Not Exist,Please Make Its Master First,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    End If

                    If clsCommon.myLen(citycode) > 0 AndAlso clsCommon.myLen(statecode) > 0 Then
                        qry = "select count(*) from tspl_city_master where city_code='" + citycode + "' and state_code='" + statecode + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)

                        If check <= 0 Then
                            Throw New Exception("City Code Does Not Exist,Please Make Its Master First,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    End If

                    Dim srvccharge As Decimal = clsCommon.myCdbl(grow.Cells("service_charges").Value)
                    Dim srvctype As String = clsCommon.myCstr(grow.Cells("Service_Charge_Type").Value)
                    Dim commsn As Decimal = clsCommon.myCdbl(grow.Cells("commision_pers").Value)
                    Dim paymnt_commsn As Decimal = clsCommon.myCdbl(grow.Cells("payment_commision_pers").Value)
                    Dim incentv As String = clsCommon.myCstr(grow.Cells("incentive").Value).Replace("'", "`")
                    Dim noofdays As Decimal = clsCommon.myCdbl(grow.Cells("incentive_days").Value)
                    Dim vsppaymnt As String = clsCommon.myCstr(grow.Cells("vsp_payment").Value).Replace("'", "`")
                    Dim payeename As String = clsCommon.myCstr(grow.Cells("vsp_payee_name").Value).Replace("'", "`")
                    Dim jointname As String = clsCommon.myCstr(grow.Cells("Joint_Name").Value).Replace("'", "`")

                    If clsCommon.CompairString(vsppaymnt, "Different") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(vsppaymnt, "Self") <> CompairStringResult.Equal Then
                        Throw New Exception("Fill Self/Different in vsp payment at line no. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.CompairString(vsppaymnt, "Different") = CompairStringResult.Equal AndAlso (clsCommon.myLen(payeename) <= 0 Or clsCommon.myLen(jointname) <= 0) Then
                        Throw New Exception("Please Fill Payee Name and Joint Name At Line No. " + clsCommon.myCstr(counter) + "")
                    ElseIf clsCommon.CompairString(vsppaymnt, "Different") <> CompairStringResult.Equal Then
                        payeename = ""
                        jointname = ""
                    End If

                    If clsCommon.myLen(srvctype) <= 0 Then
                        Throw New Exception("Please Fill Service Type(%(Percentage),Rate/Kg,Rate/Ltr) At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    If clsCommon.CompairString(srvctype, "%(Percentage)") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(srvctype, "Rate/Kg") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(srvctype, "Rate/Ltr") <> CompairStringResult.Equal Then
                        Throw New Exception("Filled Service Type Should Be Any One From %(Percentage),Rate/Kg,Rate/Ltr At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    Dim phonenum1 As String = clsCommon.myCstr(grow.Cells("Phone Num1").Value)
                    Dim phonenum2 As String = clsCommon.myCstr(grow.Cells("Phone Num2").Value)
                    Dim fax As String = clsCommon.myCstr(grow.Cells("Fax").Value)
                    Dim emailid As String = clsCommon.myCstr(grow.Cells("Email Id").Value)
                    Dim website As String = clsCommon.myCstr(grow.Cells("Website").Value)
                    Dim contct_person_name As String = "" 'clsCommon.myCstr(grow.Cells("Contact Person Name").Value)
                    Dim contct_perfson_phone As String = "" 'clsCommon.myCstr(grow.Cells("Contect Person Phone").Value)
                    Dim contct_person_fax As String = "" 'clsCommon.myCstr(grow.Cells("Contact Person Fax").Value)
                    Dim contct_person_website As String = "" 'clsCommon.myCstr(grow.Cells("Contact Person Website").Value)
                    Dim contct_person_email As String = "" 'clsCommon.myCstr(grow.Cells("Contact Person Email").Value)
                    Dim strtermcode As String = clsCommon.myCstr(grow.Cells("Terms Code").Value)
                    If String.IsNullOrEmpty(strtermcode) Then
                        Throw New Exception(" Terms Code can not be blank,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    Dim i1 As Integer
                    Dim qry1 As String = "select count(*) from tspl_terms_master where terms_code='" + strtermcode + "'"
                    i1 = connectSql.RunScalar(trans, qry1)
                    If i1 = 0 Then
                        Throw New Exception("Terms Code Does not exist : " + strtermcode + ",See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    If strtermcode.Length > 12 Then
                        Throw New Exception("Check the length of Terms Code,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    Dim strtermdes As String = clsCommon.myCstr(grow.Cells("Terms Description").Value)
                    If strtermdes.Length > 50 Then
                        Throw New Exception("Check the length of Term Description,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    Dim vendoracct As String = clsCommon.myCstr(grow.Cells("Vendor Account").Value)
                    If String.IsNullOrEmpty(vendoracct) Then
                        Throw New Exception(" Vendor Account can not be blank,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    Dim i3 As String

                    Dim qry3 As String = "select COUNT(*) from TSPL_VENDOR_ACCOUNT_SET where Acct_Set_Code ='" + vendoracct + "'"
                    i3 = connectSql.RunScalar(trans, qry3)
                    If i3 = 0 Then
                        Throw New Exception("Vendor Account Does Not Exist : " + vendoracct + ",See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If vendoracct.Length > 12 Then
                        Throw New Exception("Check the length of Vendor Account Set Code,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    Dim vendoracctdesc As String = clsCommon.myCstr(grow.Cells("Vendor Account Description").Value)

                    Dim paymenttype As String = clsCommon.myCstr(grow.Cells("Payment Code").Value)
                    Dim i4 As String
                    If Not String.IsNullOrEmpty(paymenttype) Then
                        Dim qry5 As String = "select COUNT(*) from TSPL_PAYMENT_CODE  where Payment_Code ='" + paymenttype + "'"
                        i4 = connectSql.RunScalar(trans, qry5)
                        If i4 = 0 Then
                            Throw New Exception("Payment Code Does Not Exist : " + paymenttype + ",See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If paymenttype.Length > 12 Then
                            Throw New Exception("Check the length of Payment Code,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    End If
                    Dim paymenttypedesc As String = clsCommon.myCstr(grow.Cells("Paymnet Code Description").Value)
                    Dim strbank As String = clsCommon.myCstr(grow.Cells("Bank Code").Value)

                    If String.IsNullOrEmpty(strbank) Then
                        Throw New Exception("Bank Code can not be blank,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    Dim i5 As String

                    Dim qry7 As String = "select COUNT(*) from TSPL_BANK_MASTER  where Bank_Code ='" + strbank + "'"
                    i5 = connectSql.RunScalar(trans, qry7)
                    If i5 = 0 Then
                        Throw New Exception("Bank Code Does Not Exist : " + strbank + ",See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If strbank.Length > 12 Then
                        Throw New Exception("Check the length of Bank Code,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    Dim strbankdes As String = clsCommon.myCstr(grow.Cells("Bank Code Description").Value)
                    If strbankdes.Length > 50 Then
                        Throw New Exception("Check the length of Bank Description,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    Dim strvendortype As String = clsCommon.myCstr(grow.Cells("Vendor Type").Value)
                    Dim strvendortypedes As String = grow.Cells("Vendor Type Description").Value.ToString()
                    If strvendortype.Length > 12 Then
                        Throw New Exception("Check the length of Vendor Type,See At Line No. " + clsCommon.myCstr(counter) + " ")
                    End If
                    If strvendortypedes.Length > 50 Then
                        Throw New Exception("Check the length of Vendor Type Description,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    Dim strTax As String = clsCommon.myCstr(grow.Cells("Tax Group").Value)
                    If String.IsNullOrEmpty(strTax) Then
                        Throw New Exception(" Tax Group can not be blank,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    Dim i6 As String
                    Dim qry9 As String = "select COUNT(*) from  TSPL_TAX_GROUP_MASTER   where tax_group_Code ='" + strTax + "'"
                    i6 = connectSql.RunScalar(trans, qry9)
                    If i6 = 0 Then
                        Throw New Exception("Tax Group Code Does Not Exist : " + strTax + ",See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If strTax.Length > 12 Then
                        Throw New Exception("Check the length of Tax Group Code,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    Dim strtaxdes As String = grow.Cells("Tax Group Description").Value.ToString()
                    If strtaxdes.Length > 50 Then
                        Throw New Exception("Check the length of Tax Description,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    Dim interbranch As String = "" 'grow.Cells("Inter Branch").Value.ToString()
                    If interbranch.Length > 1 Then
                        Throw New Exception("Check the length of Inter Branch,See At Line No. " + clsCommon.myCstr(counter) + "")
                    ElseIf String.IsNullOrEmpty(interbranch) Then
                        interbranch = "N"
                    End If

                    Dim strTagAsFranchise As String = "" ' grow.Cells("Tagged as Franchise").Value.ToString()
                    If strTagAsFranchise.Length > 1 Then
                        Throw New Exception("Check the length of Tagged as Franchise,See At Line No. " + clsCommon.myCstr(counter) + "")
                    ElseIf String.IsNullOrEmpty(strTagAsFranchise) Then
                        strTagAsFranchise = "N"
                    End If

                    '' Anubhooti 4-Aug-2014 BM00000003319
                    Dim strBrachName As String = clsCommon.myCstr(grow.Cells("Branch_Name").Value)
                    If clsCommon.myLen(strBrachName) > 150 Then
                        Throw New Exception("Branch Name should be max 150 character.")
                    End If

                    Dim strAccNo As String = clsCommon.myCstr(grow.Cells("Account_No").Value)
                    If clsCommon.myLen(strAccNo) > 50 Then
                        Throw New Exception("Account No. should be max 50 character.")
                    End If

                    Dim strBName As String = clsCommon.myCstr(grow.Cells("Bank_Name").Value)
                    If clsCommon.myLen(strBName) > 50 Then
                        Throw New Exception("Bank Name should be max 50 character.")
                    End If

                    Dim strIFSCCode As String = clsCommon.myCstr(grow.Cells("IFSC_Code").Value)
                    If clsCommon.myLen(strIFSCCode) > 50 Then
                        Throw New Exception("IFSC Code should be max 50 character.")
                    End If

                    Dim strAccType As String = clsCommon.myCstr(grow.Cells("Account_Type").Value)
                    If (String.IsNullOrEmpty(strAccType)) Or clsCommon.myLen(strAccType) > 10 Then
                        Throw New Exception("Length of Account Type should be max. 10 character .")
                    End If

                    If clsCommon.myLen(strAccType) > 0 Then
                        If clsCommon.CompairString(strAccType, "Cur") = CompairStringResult.Equal Or clsCommon.CompairString(strAccType, "Cre") = CompairStringResult.Equal Or clsCommon.CompairString(strAccType, "Oth") = CompairStringResult.Equal Or clsCommon.CompairString(strAccType, "Sav") = CompairStringResult.Equal Or clsCommon.CompairString(strAccType, "Cas") = CompairStringResult.Equal Or clsCommon.CompairString(strAccType, "Loa") = CompairStringResult.Equal Then
                        Else
                            Throw New Exception("Account Type For should be amoung 'Cur','Cas','Sav','Cre','Loa','Oth'.")
                        End If
                    End If

                    Dim sql1 As String = "select count(*) from TSPL_VENDOR_MASTER  where  vendor_code='" + strvendorNo + "'"
                    Dim i2 As Integer = CInt(connectSql.RunScalar(trans, sql1))
                    If (i2 = 0) Then
                        Dim strcmd As String = ""
                        strcmd = "Insert into TSPL_VENDOR_MASTER (Vendor_Code,Vendor_Name,Add1,Add2,Add3,Closing_Date ,Vendor_Group_Code,Vendor_Group_Code_Desc,City_Code ,City_Code_Desc,State,Country,Phone1,Phone2,Fax,Email,WebSite,Terms_Code,Terms_Code_Desc ,Vendor_Account,Vendor_Account_Desc,Payment_Code,Payment_Code_Desc,Bank_Code ,Bank_Code_Desc,Tax_Group ,Tax_Group_Desc,TAX1,TAX1_Rate,TAX2,TAX2_Rate,TAX3,TAX3_Rate,TAX4,TAX4_Rate,TAX5,TAX5_Rate,TAX6,TAX6_Rate,TAX7,TAX7_Rate,TAX8,TAX8_Rate,TAX9,TAX9_Rate,TAX10,TAX10_Rate,Transporter,created_by,created_date,modify_by,modify_date,comp_code,PAN,Inter_branch,franchise_yn,form_type,state_code,country_code,service_charges,commision_pers,incentive,incentive_days,vsp_payment,vsp_payee_name,Joint_Name,Service_Charge_Type,payment_commision_pers,Branch_Name,Account_No,Bank_Name,IFSC_Code,Account_Type) values ('" + strvendorNo + "','" + strvendorname + "','" + add1 + "','" + add2 + "','" + add3 + "','" + closing_date + "','" + strgroupCode + "','" + strgroupDes + "','" + citycode + "','" + citycodedesc + "','" + state + "','" + country + "','" + phonenum1 + "','" + phonenum2 + "','" + fax + "','" + emailid + "','" + website + "','" + strtermcode + "','" + strtermdes + "','" + vendoracct + "','" + vendoracctdesc + "','" + paymenttype + "','" + paymenttypedesc + "','" + strbank + "','" + strbankdes + "','" + strTax + "','" + strtaxdes + "','" + clsCommon.myCstr(grow.Cells("Tax1").Value.ToString()) + "'," + clsCommon.myCstr(grow.Cells("Tax1 Rate").Value.ToString()) + ",'" + clsCommon.myCstr(grow.Cells("Tax2").Value.ToString()) + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax2 Rate").Value)) + "','" + clsCommon.myCstr(grow.Cells("Tax3").Value.ToString()) + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax3 Rate").Value)) + "','" + clsCommon.myCstr(grow.Cells("Tax4").Value.ToString()) + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax4 Rate").Value)) + "','" + clsCommon.myCstr(grow.Cells("Tax5").Value.ToString()) + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax5 Rate").Value)) + "','" + grow.Cells("Tax6").Value.ToString() + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax6 Rate").Value)) + "','" + clsCommon.myCstr(grow.Cells("Tax7").Value.ToString()) + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax7 Rate").Value)) + "','" + clsCommon.myCstr(grow.Cells("Tax8").Value.ToString()) + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax8 Rate").Value)) + "','" + clsCommon.myCstr(grow.Cells("Tax9").Value.ToString()) + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax9 Rate").Value)) + "','" + clsCommon.myCstr(grow.Cells("Tax10").Value.ToString()) + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax10 Rate").Value)) + "','" + clsCommon.myCstr(grow.Cells("Transporter").Value.ToString()) + "','" + userCode + "','" + connectSql.serverDate(trans) + "','" + userCode + "','" + connectSql.serverDate(trans) + "','" + companyCode + "','" + clsCommon.myCstr(grow.Cells("PAN").Value.ToString()) + "','" + interbranch + "','" + strTagAsFranchise + "','VSP','" + statecode + "','" + countrycode + "','" + clsCommon.myCstr(srvccharge) + "','" + clsCommon.myCstr(commsn) + "','" + clsCommon.myCstr(incentv) + "','" + clsCommon.myCstr(noofdays) + "','" + clsCommon.myCstr(vsppaymnt) + "','" + clsCommon.myCstr(payeename) + "','" + jointname + "','" + srvctype + "','" + clsCommon.myCstr(paymnt_commsn) + "','" + strBrachName + "','" + strAccNo + "','" + strBName + "','" + strIFSCCode + "','" + strAccType + "')"
                        connectSql.RunSqlTransaction(trans, strcmd)
                    Else
                        Dim strcmd As String = "Update  TSPL_VENDOR_MASTER set  Vendor_Name='" + strvendorname + "',add1='" + add1 + "',add2='" + add2 + "',add3='" + add3 + "',Closing_Date='" + closing_date + "',Vendor_Group_Code='" + strgroupCode + "',Vendor_Group_Code_Desc='" + strgroupDes + "',City_Code='" + citycode + "',City_Code_Desc='" + citycodedesc + "',State='" + state + "',Country='" + country + "',Phone1='" + phonenum1 + "',Phone2='" + phonenum2 + "',Fax='" + fax + "',Email='" + emailid + "',WebSite='" + website + "',Terms_Code='" + strtermcode + "',Terms_Code_Desc='" + strtermdes + "' ,Vendor_Account='" + vendoracct + "',Vendor_Account_Desc='" + vendoracctdesc + "',Payment_Code='" + paymenttype + "',Payment_Code_Desc='" + paymenttypedesc + "',Bank_Code='" + strbank + "', Bank_Code_Desc='" + strbankdes + "',Ven_Type_Code='" + strvendortype + "',Ven_Type_Desc='" + strvendortypedes + "' ,Tax_Group='" + strTax + "',Tax_Group_Desc='" + strtaxdes + "' ,TAX1='" + grow.Cells("Tax1").Value.ToString() + "',TAX1_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax1 Rate").Value)) + "',TAX2='" + grow.Cells("Tax2").Value.ToString() + "',TAX2_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax2 Rate").Value)) + "',TAX3='" + grow.Cells("Tax3").Value.ToString() + "',TAX3_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax3 Rate").Value)) + "',TAX4='" + grow.Cells("Tax4").Value.ToString() + "',TAX4_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax4 Rate").Value)) + "',TAX5='" + grow.Cells("Tax5").Value.ToString() + "',TAX5_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax5 Rate").Value)) + "',TAX6='" + grow.Cells("Tax6").Value.ToString() + "',TAX6_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax6 Rate").Value)) + "',TAX7='" + grow.Cells("Tax7").Value.ToString() + "',TAX7_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax7 Rate").Value)) + "',TAX8='" + grow.Cells("Tax8").Value.ToString() + "',TAX8_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax8 Rate").Value)) + "',TAX9='" + grow.Cells("Tax9").Value.ToString() + "',TAX9_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax9 Rate").Value)) + "',TAX10='" + grow.Cells("Tax10").Value.ToString() + "',TAX10_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax10 Rate").Value)) + "',Transporter='" + grow.Cells("Transporter").Value.ToString() + "',modify_by='" + userCode + "',modify_date='" + connectSql.serverDate(trans) + "',comp_code='" + companyCode + "',PAN='" + grow.Cells("PAN").Value.ToString() + "',Inter_Branch='" + interbranch + "', franchise_yn='" + strTagAsFranchise + "',form_type='VSP',state_code='" + statecode + "',country_code='" + countrycode + "',service_charges='" + clsCommon.myCstr(srvccharge) + "',commision_pers='" + clsCommon.myCstr(commsn) + "',incentive='" + clsCommon.myCstr(incentv) + "',incentive_days='" + clsCommon.myCstr(noofdays) + "',vsp_payment='" + clsCommon.myCstr(vsppaymnt) + "',vsp_payee_name='" + clsCommon.myCstr(payeename) + "',Joint_Name='" + jointname + "',Service_Charge_Type='" + srvctype + "',payment_commision_pers='" + clsCommon.myCstr(paymnt_commsn) + "',Branch_Name='" + strBrachName + "',Account_No='" + strAccNo + "',Bank_Name='" + strBName + "',IFSC_Code='" + strIFSCCode + "',Account_Type='" + strAccType + "' where vendor_code='" + strvendorNo + "' and form_type='VSP'"
                        connectSql.RunSqlTransaction(trans, strcmd)
                    End If
                    counter += 1
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow("Line: " + Count + " - " + ex.Message)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub
#End Region

#Region "Event"
    'This will check the existence of value in databse on text change property of finder,if it exist then it will call funfill() otherwise it will blank the fields
    Sub fndcity_text_Changed()
        Try
            Dim str As String = "select City_Code,city_Name from TSPL_City_MASTER where City_code = '" + fndCity.Value + "'"
            Dim dr As DataTable
            Dim strvalue As String = ""
            dr = clsDBFuncationality.GetDataTable(str)
            If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then

                strvalue = dr.Rows(0)("City_Code").ToString()
            End If
            If strvalue <> "" Then
                funfilfndcity()
            Else
                txtCity.Text = ""

            End If
        Catch ex As Exception

            myMessages.myExceptions(ex)
        End Try
    End Sub
    Public Sub fndcity_text_Changed(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    'This will check the existence of value in databse on text change property of finder,if it exist then it will call funfill() otherwise it will blank the fields
    'This will check the existence of value in databse on leave property of finder,if it exist then it will fill, otherwise it will blank the fields
    Sub fndCity_leave()
        If fndCity.Value = "" Then
        Else
            Try
                Dim strquery As String = "select City_Code ,city_Name from TSPL_City_MASTER where City_code = '" + fndCity.Value + "'"
                Dim dr As DataTable
                Dim strvalue As String = ""
                dr = clsDBFuncationality.GetDataTable(strquery)
                If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                    strvalue = dr.Rows(0)("City_Code").ToString()
                End If
                If strvalue <> "" Then
                Else : strquery = ""
                    txtCity.Text = ""
                    common.clsCommon.MyMessageBoxShow(Me, "This City does not exist in Master Table", Me.Text)
                    fndCity.Value = ""
                End If
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
    End Sub

    'This will check the existence of value in databse on leave property of finder,if it exist then it will fill, otherwise it will blank the fields
    'Email Validation
    Private Sub txtEmail_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEmail.Leave
        If txtEmail.Text = "" Then
        Else
            Dim check As Match = Regex.Match(txtEmail.Text, "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")
            If check.Success Then
                Errorcontrol.ResetError(txtEmail)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "Please Enter the proper format of e-mail address", Me.Text)
                txtEmail.Text = ""
                txtEmail.Focus()
                txtEmail.Select()
                Errorcontrol.SetError(txtEmail, "Please Enter the proper format of e-mail address")
            End If
        End If
    End Sub

    'Numerics Validation---------------------------------------------
    Private Sub txtPhone1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPhone1.KeyPress
        If ((e.KeyChar >= Chr(48) And e.KeyChar <= Chr(57)) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(48) Or e.KeyChar = Chr(45)) Then
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtPhone2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPhone2.KeyPress
        If ((e.KeyChar >= Chr(48) And e.KeyChar <= Chr(57)) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(48) Or e.KeyChar = Chr(45)) Then
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtfax_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtfax.KeyPress
        If ((e.KeyChar >= Chr(48) And e.KeyChar <= Chr(57)) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(48) Or e.KeyChar = Chr(45)) Then
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtContactFax_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If ((e.KeyChar >= Chr(48) And e.KeyChar <= Chr(57)) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(48) Or e.KeyChar = Chr(45)) Then
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtContPhone_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If ((e.KeyChar >= Chr(48) And e.KeyChar <= Chr(57)) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(48) Or e.KeyChar = Chr(45)) Then
        Else
            e.Handled = True
        End If
    End Sub
#End Region

#Region "Finder"


    Sub LoadCity()
        Dim qry As String = " select City_Code as City,City_Name as name from tspl_city_master where Comp_Code='" + objCommonVar.CurrentCompanyCode + "' "
        gvTrainingCities.DataSource = clsDBFuncationality.GetDataTable(qry)
        gvTrainingCities.ValueMember = "City"
        gvTrainingCities.DisplayMember = "name"
    End Sub

    Sub LoadPaymentType()
        Dim Dt As New DataTable
        Dt.Columns.Add("Code", GetType(String))
        Dt.Columns.Add("Desc", GetType(String))
        Dt.Rows.Add("L", "Cash")
        Dt.Rows.Add("C", "Cheque")
        Dt.Rows.Add("D", "Draft")
        Dt.Rows.Add("N", "Net Transfer")
        CmbPaymentType.DataSource = Dt
        CmbPaymentType.ValueMember = "Code"
        CmbPaymentType.DisplayMember = "Desc"

    End Sub

    Sub LoadCourse()
        Dim qry As String = "select Code,Name from Tspl_Training_Master " 'where Comp_Code='" + objCommonVar.CurrentCompanyCode + "' "
        gvTrainingCourse.DataSource = clsDBFuncationality.GetDataTable(qry)
        gvTrainingCourse.ValueMember = "Code"
        gvTrainingCourse.DisplayMember = "Name"
    End Sub

    Sub LoadQualification()
        Dim qry As String = "select Qualification_Code as Code,Qualification_Name As Name from TSPL_HR_QUALIFICATION_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "' "
        gvQualification.DataSource = clsDBFuncationality.GetDataTable(qry)
        gvQualification.ValueMember = "Code"
        gvQualification.DisplayMember = "Name"
    End Sub

    Private Sub fndvendorNo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndvendorNo.Query = "select Vendor_Code as [Vendor Code],Vendor_Name as [Vendor Name],Vendor_Group_Code as [Vendor Group],(select case when Status='N' then 'Active' else 'In.Active' end ) as [Status] from TSPL_VENDOR_MASTER  "
        'fndvendorNo.ConnectionString = connectSql.SqlCon()
        'fndvendorNo.Caption = "Vendor"
        'fndvendorNo.ValueToSelect = "Vendor Code"
        'fndvendorNo.ValueToSelect1 = "Vendor Name"
    End Sub

    Private Sub fndgroupcode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

        'fndgroupcode.Query = " SELECT ven_Group_Code as [Vendor Group Code],Group_Desc as [Description],Tax_Group_Code as [Tax Group],Acct_Set_Code as [Account Set],Terms_Code as [Terms Code] FROM [TSPL_VENDOR_GROUP] "
        'fndgroupcode.ConnectionString = connectSql.SqlCon()
        'fndgroupcode.Caption = "Vendor Group"
        'fndgroupcode.ValueToSelect = "Vendor Group Code"
        'fndgroupcode.ValueToSelect1 = "Description"
    End Sub

    Private Sub fndCity_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndCity.Query = "SELECT [City_Code] as [City Code],[City_Name] as [City Name]FROM [TSPL_CITY_MASTER]"
        'fndCity.ConnectionString = connectSql.SqlCon()
        'fndCity.Caption = "City"
        'fndCity.ValueToSelect = "City Code"
        'fndCity.ValueToSelect1 = "City Name"
    End Sub
    'Private Sub fndTrmsCode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fndTrmsCode.Query = "SELECT [Terms_Code] as [Terms Code],[Terms_Desc] as [Description],[No_Days] as [No. of Days] FROM [TSPL_TERMS_MASTER]"
    '    fndTrmsCode.ConnectionString = connectSql.SqlCon()
    '    fndTrmsCode.Caption = "Payments Terms"
    '    fndTrmsCode.ValueToSelect = "Terms Code"
    '    fndTrmsCode.ValueToSelect1 = "Description"
    'End Sub

    'Private Sub fndAccntSet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fndAccntSet.Query = "SELECT  [Acct_Set_Code] as [Account Set Code],[Acct_Set_Desc] as [Description] FROM [TSPL_VENDOR_ACCOUNT_SET]"
    '    fndAccntSet.ConnectionString = connectSql.SqlCon()
    '    fndAccntSet.Caption = "Vendor Account Set"
    '    fndAccntSet.ValueToSelect = "Account Set Code"
    '    fndAccntSet.ValueToSelect1 = "Description"
    'End Sub

    'Private Sub fndPayCode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fndPayCode1.Query = "  SELECT [Payment_Code] as [Payment Code],[payment_Desc] as [Description] FROM [TSPL_PAYMENT_CODE]"
    '    fndPayCode1.ConnectionString = connectSql.SqlCon()
    '    fndPayCode1.Caption = "Payment Code"
    '    fndPayCode1.ValueToSelect = "Payment Code"
    '    fndPayCode1.ValueToSelect1 = "Description"
    'End Sub


    'Private Sub fndvendortype_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    fndvendortype1.Query = "SELECT  [ven_Type_Code] as [Vendor Type Code],[ven_Type_Desc] as [Description]FROM [TSPL_VENDOR_TYPE_MASTER]"
    '    fndvendortype1.ConnectionString = connectSql.SqlCon()
    '    fndvendortype1.Caption = "Vendor Type"
    '    fndvendortype1.ValueToSelect = "Vendor Type Code"
    '    fndvendortype1.ValueToSelect1 = "Description"
    'End Sub

    'Private Sub fndbankcode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fndbankcode.ConnectionString = connectSql.SqlCon()
    '    'fndbankcode.Query = " select bank_code As [Bank Code],description  as [Description]from TSPL_Bank_MASTER "
    '    fndbankcode.Query = clsERPFuncationality.glbankquery
    '    fndbankcode.ValueToSelect = "Bank Code"
    '    fndbankcode.Caption = "Bank Master"
    '    fndbankcode.ValueToSelect1 = "Description"
    'End Sub

    'Private Sub fndTxGrp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    'fndTxGrp.Query = "SELECT [Tax_Group_Code] AS [Tax Group Code],[Tax_Group_Desc] as [Description]," & _
    '    '   " (select case when [Tax_Group_Type]='S' then 'Sale' else 'Purchase' end) as [Type] FROM [TSPL_TAX_GROUP_MASTER] where Tax_Group_Type='S'"
    '    fndTxGrp.Query = clsERPFuncationality.UserAvailableTaxGroup + " and  M.Tax_Group_Type='P'"
    '    fndTxGrp.ConnectionString = connectSql.SqlCon()
    '    fndTxGrp.Caption = "Tax Group"
    '    fndTxGrp.ValueToSelect = "Code"
    '    fndTxGrp.ValueToSelect1 = "Description"
    'End Sub

    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "VENDOR-M"
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
    '            btnsave.Enabled = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            btndelete.Enabled = False
    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception
    '        myMessages.myExceptions(er)
    '    End Try
    'End Function





#End Region

#Region "Button Click"
    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        Me.Close()
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Sub SaveData()
        'Dim trans As SqlTransaction
        Try
            If (AllowToSave()) Then
                'trans = clsDBFuncationality.GetTransactin()
                Dim objHead As clsTrainerMaster
                '' asign screen vaules in objHead
                objHead = New clsTrainerMaster
                objHead.Code = clsCommon.myCstr(fndvendorNo.Value)
                objHead.Name = clsCommon.myCstr(txtName.Text)
                objHead.Type = clsCommon.myCstr(IIf(CmbType.Text = "Internal", "I", "E"))
                objHead.Institute_Code = clsCommon.myCstr(fndInstitutecode.Value)
                objHead.First_name = clsCommon.myCstr(TxtFirstName.Text)
                objHead.Last_Name = clsCommon.myCstr(txtLastName.Text)
                objHead.Emp_Code = clsCommon.myCstr(FndEployeeCode.Value)
                objHead.Email_Id = clsCommon.myCstr(txtEmail.Text)
                objHead.Payment_type = clsCommon.myCstr(CmbPaymentType.SelectedValue)
                objHead.Address1 = clsCommon.myCstr(txtAdd1.Text)
                objHead.Address2 = clsCommon.myCstr(txtAdd2.Text)
                objHead.Address3 = clsCommon.myCstr(txtAdd3.Text)
                objHead.City = clsCommon.myCstr(fndCity.Value)
                objHead.State = clsCommon.myCstr(txtstatecode.Value)
                objHead.Country = clsCommon.myCstr(txtcountrycode.Value)
                objHead.Pin_Code = clsCommon.myCstr(TxtPinCode.Text)
                objHead.PhoneNo1 = clsCommon.myCstr(txtPhone1.Text)
                objHead.PhoneNo2 = clsCommon.myCstr(txtPhone2.Text)
                objHead.Gendor = clsCommon.myCstr(IIf(CmbGender.Text = "Male", "M", IIf(CmbGender.Text = "FeMale", "F", "U")))
                objHead.DOB = clsCommon.myCDate(dtpDoB.Value)
                objHead.Remark = clsCommon.myCstr(TxtRemark.Text)
                objHead.Is_Applicable = clsCommon.myCstr(IIf(chkIsApplicable.Checked, "1", "0"))
                objHead.Fax = clsCommon.myCstr(txtfax.Text)

                Dim ArrQualification As New List(Of clsQualification)
                Dim ArrCourse As New List(Of clsCourse)
                Dim ArrCities As New List(Of clsTrainingGivenCities)



                For Each grow As String In gvQualification.CheckedValue
                    Dim obj As clsQualification
                    obj = New clsQualification

                    obj.DOC_CODE = clsCommon.myCstr(fndvendorNo.Value)
                    obj.Qualification_code = clsCommon.myCstr(grow)
                    ArrQualification.Add(obj)
                Next

                For Each grow As String In gvTrainingCourse.CheckedValue
                    Dim obj As clsCourse
                    obj = New clsCourse

                    obj.DOC_CODE = clsCommon.myCstr(fndvendorNo.Value)
                    obj.Course_code = clsCommon.myCstr(grow)
                    ArrCourse.Add(obj)
                Next

                For Each grow As String In gvTrainingCities.CheckedValue
                    Dim obj As clsTrainingGivenCities
                    obj = New clsTrainingGivenCities

                    obj.DOC_CODE = clsCommon.myCstr(fndvendorNo.Value)
                    obj.City_code = clsCommon.myCstr(grow)
                    ArrCities.Add(obj)
                Next
                If clsTrainerMaster.SaveData(objHead, ArrQualification, ArrCourse, ArrCities) Then
                    clsCommon.MyMessageBoxShow(Me, "Data saved Successfully", Me.Text)
                    LoadData(objHead.Code, NavigatorType.Current)
                End If
            End If


        Catch ex As Exception
            'trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Function AllowToSave() As Boolean
        Try
            If btnsave.Text = "Update" Then
                Dim strchk As String = "select Posted from TSPL_Trainer_master where DOC_COde='" + fndvendorNo.Value + "'"
                Dim chkpost As String = clsDBFuncationality.getSingleValue(strchk)
                If chkpost = "1" Then
                    clsCommon.MyMessageBoxShow(Me, "Transection already posted", Me.Text)
                    Return False
                End If
            End If

            If clsCommon.myLen(fndvendorNo.Value) <= 0 Then
                myMessages.blankValue("Please Fill Code")
                pageCus.SelectedPage = RadPageViewPage1
                fndvendorNo.Focus()
                fndvendorNo.Select()
                Errorcontrol.SetError(fndvendorNo, "Please Fill Code")
                Return False
            Else
                Errorcontrol.ResetError(fndvendorNo)
            End If

            If txtName.Text = "" Then
                myMessages.blankValue("Please Fill Name")
                pageCus.SelectedPage = RadPageViewPage1
                txtName.Focus()
                txtName.Select()
                Errorcontrol.SetError(txtName, "Please Fill Name")
                Return False
            Else
                Errorcontrol.ResetError(txtName)
            End If




            If fndInstitutecode.Value = "" Then
                myMessages.blankValue("Please Select Institute Code")
                pageCus.SelectedPage = RadPageViewPage1
                fndInstitutecode.Focus()
                fndInstitutecode.Select()
                Errorcontrol.SetError(fndInstitutecode, "Please Select Institute Code")
                Return False
            Else
                Errorcontrol.ResetError(fndInstitutecode)
            End If

            If clsCommon.myLen(txtAdd1.Text) <= 0 AndAlso clsCommon.myLen(txtAdd2.Text) <= 0 AndAlso clsCommon.myLen(txtAdd3.Text) <= 0 Then
                myMessages.blankValue("Please Fill Address")
                pageCus.SelectedPage = RadPageViewPage1
                txtAdd1.Focus()
                txtAdd1.Select()
                Errorcontrol.SetError(txtAdd1, "Please Fill Address")
                Errorcontrol.SetError(txtAdd2, "Please Fill Address")
                Errorcontrol.SetError(txtAdd3, "Please Fill Address")
                Return False
            Else
                Errorcontrol.ResetError(txtAdd1)
                Errorcontrol.ResetError(txtAdd2)
                Errorcontrol.ResetError(txtAdd3)
            End If

            If clsCommon.myLen(txtcountrycode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select Country", Me.Text)
                pageCus.SelectedPage = RadPageViewPage1
                txtcountrycode.Select()
                txtcountrycode.Focus()
                Errorcontrol.SetError(txtcountrycode, "Please Select Country")
                Return False
            Else
                Errorcontrol.ResetError(txtcountrycode)
            End If

            If clsCommon.myLen(txtstatecode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select State", Me.Text)
                pageCus.SelectedPage = RadPageViewPage1
                txtstatecode.Select()
                txtstatecode.Focus()
                Errorcontrol.SetError(txtstatecode, "Please Select State")
                Return False
            Else
                Errorcontrol.ResetError(txtstatecode)
            End If

            If clsCommon.myLen(txtCity.Text) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select City", Me.Text)
                pageCus.SelectedPage = RadPageViewPage1
                txtCity.Select()
                txtCity.Focus()
                Errorcontrol.SetError(txtcountrycode, "Please Select City")
                Return False
            Else
                Errorcontrol.ResetError(txtCity)
            End If


            If clsCommon.myLen(CmbType.Text) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select Type", Me.Text)
                pageCus.SelectedPage = RadPageViewPage1
                CmbType.Select()
                CmbType.Focus()
                Errorcontrol.SetError(CmbType, "Please Select Type")
                Return False
            Else
                Errorcontrol.ResetError(CmbType)
            End If

            If clsCommon.myLen(TxtFirstName.Text) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Enter First Name", Me.Text)
                pageCus.SelectedPage = RadPageViewPage1
                TxtFirstName.Select()
                TxtFirstName.Focus()
                Errorcontrol.SetError(TxtFirstName, "Please Enter First Name")
                Return False
            Else
                Errorcontrol.ResetError(TxtFirstName)
            End If


            If clsCommon.myLen(txtLastName.Text) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Enter Last Name", Me.Text)
                pageCus.SelectedPage = RadPageViewPage1
                txtLastName.Select()
                txtLastName.Focus()
                Errorcontrol.SetError(txtLastName, "Please Enter Last Name")
                Return False
            Else
                Errorcontrol.ResetError(txtLastName)
            End If


            If clsCommon.myLen(CmbGender.Text) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select Gender", Me.Text)
                pageCus.SelectedPage = RadPageViewPage1
                CmbGender.Select()
                CmbGender.Focus()
                Errorcontrol.SetError(CmbGender, "Please Select Gender")
                Return False
            Else
                Errorcontrol.ResetError(CmbGender)
            End If


            Return True
        Catch ex As Exception
            myMessages.myExceptions(ex)
            Return True
        End Try
    End Function
    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If fndvendorNo.Value = "" Then
            myMessages.blankValue("Trainer Code")
        ElseIf myMessages.deleteConfirm() Then
            Dim obj As New clsTrainerMaster
            If obj.DeleteData(fndvendorNo.Value) Then
                clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully...", "Delete Data", Me.Text)
                funreset()
            End If
        End If
    End Sub



    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        funreset()
    End Sub

    Private Sub MenuExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuExport.Click
        funExport()
    End Sub

    Private Sub MenuImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuImport.Click
        funImport()
    End Sub
    Private Sub RadMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem4.Click
        Me.Close()
    End Sub
#End Region


    Private Sub frmTrainerMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            'PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funreset()
        ElseIf e.Alt And e.KeyCode = Keys.U Then
            Dim squery As String = "update Tspl_Trainer_Master set is_APPLICAbLE='" & IIf(chkIsApplicable.Checked, "1", "0") & "' WHERE DOC_CODE='" & fndvendorNo.Value & "'"
            clsDBFuncationality.ExecuteNonQuery(squery)
            clsCommon.MyMessageBoxShow(Me, "IsApplicable Updated.", Me.Text)
        End If
    End Sub

    Private Sub fndvendorNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndvendorNo._MYNavigator
        LoadData(fndvendorNo.Value, NavType)
    End Sub

    Private Sub fndvendorNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndvendorNo._MYValidating
        Dim str As String = "select count(*) from Tspl_Trainer_Master where DOC_Code ='" + fndvendorNo.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            fndvendorNo.MyReadOnly = False
        Else
            fndvendorNo.MyReadOnly = True
        End If
        If fndvendorNo.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = "select DOC_Code,DOC_Name,First_Name,Last_Name from Tspl_Trainer_Master"
            Dim dr As DataRow = clsCommon.ShowSelectFormForRow("TRAINERFND", qry)
            If dr IsNot Nothing Then
                fndvendorNo.Value = clsCommon.myCstr(dr("DOC_Code"))
                LoadData(fndvendorNo.Value, NavigatorType.Current)
            Else
                fndvendorNo.Value = ""
                funreset()
            End If
            'If fndvendorNo.Value IsNot Nothing Then
            '    btndelete.Enabled = True
            'Else
            '    btndelete.Enabled = False
            'End If
        End If
    End Sub

    Private Sub fndCity__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndCity._MYValidating
        If clsCommon.myLen(txtstatecode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Select State Code First", Me.Text)
            txtstatecode.Focus()
            txtstatecode.Select()
            Return
        End If
        '    If isButtonClicked Then
        'Dim qry As String = "SELECT [City_Code] as [CityCode],[City_Name] as [City Name]FROM [TSPL_CITY_MASTER] "
        'fndCity.Value = clsCommon.ShowSelectForm("CityFmfnd", qry, "CityCode", "", fndCity.Value, "", isButtonClicked)
        fndCity.Value = clsCityMaster.getFinder(" state_code='" + txtstatecode.Value + "'", fndCity.Value, isButtonClicked)
        txtCity.Text = clsDBFuncationality.getSingleValue("Select City_Name from TSPL_CITY_MASTER where City_Code='" + fndCity.Value + "' and state_code='" + txtstatecode.Value + "'")
        ''fndcity_text_Changed()
        ''fndCity_leave()
        '  End If
    End Sub


    Private Sub txtcountrycode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtcountrycode._MYValidating
        Try
            Dim qry As String = "select country_code as Code,country_name as Country from tspl_country_master"
            txtcountrycode.Value = clsCommon.ShowSelectForm("CNTFND", qry, "Code", "", txtcountrycode.Value, "Code", isButtonClicked)

            If clsCommon.myLen(txtcountrycode.Value) > 0 Then
                txtCountry.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select country_name from tspl_country_master where country_code='" + txtcountrycode.Value + "'"))
                txtState.Text = ""
                txtCity.Text = ""
                fndCity.Value = ""
                txtstatecode.Value = ""
            Else
                txtState.Text = ""
                txtCity.Text = ""
                fndCity.Value = ""
                txtCountry.Text = ""
                txtstatecode.Value = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtstatecode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtstatecode._MYValidating
        If clsCommon.myLen(txtcountrycode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Select Country First", Me.Text)
            txtcountrycode.Focus()
            txtcountrycode.Select()
            Return
        End If

        Try
            Dim qry As String = "select State_code as Code,state_name as State from tspl_state_master"
            txtstatecode.Value = clsCommon.ShowSelectForm("STAFND", qry, "Code", " country_code='" + txtcountrycode.Value + "'", txtcountrycode.Value, "Code", isButtonClicked)

            If clsCommon.myLen(txtstatecode.Value) > 0 Then
                txtState.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select state_name from tspl_state_master where state_code='" + txtstatecode.Value + "'"))
                txtCity.Text = ""
                fndCity.Value = ""
            Else
                txtState.Text = ""
                txtCity.Text = ""
                fndCity.Value = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndInstitutecode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndInstitutecode._MYValidating
        Try
            Dim qry As String = "select Code,Name from Tspl_Institute_Master where Posted=1"
            Dim dr As DataRow = clsCommon.ShowSelectFormForRow("INSTFND", qry)

            If dr IsNot Nothing Then
                fndInstitutecode.Value = clsCommon.myCstr(dr("code"))
                LblInstituteName.Text = clsCommon.myCstr(dr("Name"))
            Else
                fndInstitutecode.Value = ""
                LblInstituteName.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FndEployeeCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles FndEployeeCode._MYValidating
        Try
            Dim obj As New clsItemMaster
            FndEployeeCode.Value = clsEmployeeMaster.getFinder("", FndEployeeCode.Value, isButtonClicked)
            If clsCommon.myLen(FndEployeeCode.Value) > 0 Then
                LblEmployeeName.Text = clsDBFuncationality.getSingleValue("select Emp_Name from TSPL_EMPLOYEE_MASTER where EMP_CODE='" & FndEployeeCode.Value & "'")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

    Private Sub BtnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnPost.Click
        Try
            Dim obj As New clsTrainerMaster
            If (myMessages.postConfirm()) Then
                If (clsTrainerMaster.PostData(fndvendorNo.Value, True)) Then
                    clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                    LoadData(fndvendorNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

    Private Sub CmbType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles CmbType.SelectedIndexChanged
        If clsCommon.CompairString(CmbType.Text, "External") = CompairStringResult.Equal Then
            txtempext.Visible = True
            LblEmp.Text = "External Employee"
            FndEployeeCode.Visible = False
            LblEmployeeName.Visible = False
        Else
            txtempext.Visible = False
            LblEmp.Text = "Employee Code"
            FndEployeeCode.Visible = True
            LblEmployeeName.Visible = True
        End If
    End Sub
End Class

