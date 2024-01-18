'-12/12/12-11:35AM--Updation By--Pankaj Kumar--While retreiving data , Applied check of Segment No also, because in case of multiple Code desc was not right.---fwd By--Varun
'=========BM00000002965,Updated By Rohit on June 24,2014,12:30 PM (Remark : ASM/ZM,Salesman Details was not showing when used load details of any Employee,Now It will show ASM/ZM Details and Respective State and Region for That ASM/ZM) =========================

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
Imports System.Globalization
Imports System.Threading
Imports common
'Createb by  --> Vipin
'Created Date --> 12/05/2011
'Modified by --> Vipin
'Modified Date -->03/06/2011
'Tables used --> tspl_employee_master,tspl_gl_segment_code,tspl_fixed_parameter
Public Class frmEmployeeMaster
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private isInsideLoadData As Boolean = False
    Dim userCode, companyCode As String
    Dim ChkEmployeeTypeParavet As Boolean = False


#Region "Variables"
    Dim sql As String
    Dim ds As DataSet
    Dim dr As SqlDataReader
    Dim tableName As String = "TSPL_TAX_MASTER"
    Dim tableCode As String = "Tax_Code"
    Dim codePrefix As String = "TAX"
    Const ColStateCode As String = "State Code"
    Const ColStatename As String = "State Name"
    Const ColRegionCode As String = "Region Code"
    Const ColRegionname As String = "Region Name"
    Const Colcity As String = "City"
    Const Colcityname As String = "City Name"
    Dim IsValueChanged As Boolean = False
#End Region
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
    'Main Form Load
    Private Sub SetUserMgmtNew()
        '' Anubhooti 30-July-2014 BM00000003130
        'MyBase.SetUserMgmt(clsUserMgtCode.EmployeeMaster)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If
        btnsave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 03/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnsave.Visible = True Then
            empim.Enabled = True
            empex.Enabled = True
        Else
            empim.Enabled = False
            empex.Enabled = False
        End If
        '--------------------------------------------------
        'btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub frmEmployeeMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            '    PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()

        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funreset()
        End If
    End Sub
    Private Sub FrmEmployeeMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ChkEmployeeTypeParavet = IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AddParavetEmployeeType, clsFixedParameterCode.AddParavetEmployeeType, Nothing)) = "1", True, False)
        dgstate.Visible = False
        'globalFunc.mandatoryText(fndempcode.txtValue, fnddesignation.txtValue)
        globalFunc.mandatoryDropdown(ddlemptype, ddlempstatus)
        ToolTipemp.SetToolTip(btnnew, "Reset")

        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New Trasnaction")
        ' fndempcode.txtValue.MaxLength = 12
        '  fnddesignation.txtValue.MaxLength = 12
        textchangedsub()
        fnddesileave()
        fndempleave()

        '  AddHandler fndempcode.ValueChanged, AddressOf text_changed
        'AddHandler fndempcode.txtValue.KeyPress, AddressOf key_press
        '  AddHandler fnddesignation.txtValue.KeyPress, AddressOf fnddesig_keypress
        '  AddHandler fnddesignation.txtValue.Leave, AddressOf fnddesi_leave
        ' AddHandler fndempcode.txtValue.Leave, AddressOf fndemp_leave
        '      fndempcode.txtValue.CharacterCasing = CharacterCasing.Upper
        '   fnddesignation.txtValue.CharacterCasing = CharacterCasing.Upper
        btndelete.Enabled = False
        btnsave.Enabled = True
        funddltypefill()
        funddlstatusfill()
        dtpdob.Value = Date.Today
        dtpexdate.Value = Date.Today
        dtpjoin.Value = Date.Today
        dtpreleaving.Value = Date.Today
        ddlempstatus.Text = "Active"
        LoadBlankGrid()
        'dgstate.Visible = False
        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-GB")
        lblempty.Visible = True


    End Sub
    'this code is to load blank grid added by meenesh
    Sub LoadBlankGrid()

        dgstate.AddNewRowPosition = SystemRowPosition.Bottom
        dgstate.Rows.Clear()
        dgstate.Columns.Clear()

        Dim State_Code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        State_Code.FormatString = ""
        State_Code.HeaderText = "State Code"
        State_Code.Name = ColStateCode
        State_Code.Width = 150
        State_Code.ReadOnly = False
        State_Code.TextImageRelation = TextImageRelation.TextBeforeImage
        State_Code.HeaderImage = Global.XpertERPCommanServices.My.Resources.Resources.search4
        State_Code.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        dgstate.MasterTemplate.Columns.Add(State_Code)

        Dim State_desc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        State_desc.FormatString = ""
        State_desc.HeaderText = "State Name"
        State_desc.Name = ColStatename
        State_desc.Width = 300
        State_desc.ReadOnly = True
        State_desc.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        dgstate.MasterTemplate.Columns.Add(State_desc)




        Dim Region_Code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Region_Code.FormatString = ""
        Region_Code.HeaderText = "Region Code"
        Region_Code.Name = ColRegionCode
        Region_Code.Width = 300
        Region_Code.ReadOnly = True
        Region_Code.IsVisible = False

        Region_Code.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        dgstate.MasterTemplate.Columns.Add(Region_Code)



        Dim Region_desc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Region_desc.FormatString = ""
        Region_desc.HeaderText = "Region Name"
        Region_desc.Name = ColRegionname
        Region_desc.Width = 300
        Region_desc.ReadOnly = True
        Region_desc.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        dgstate.MasterTemplate.Columns.Add(Region_desc)





        Dim City As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        City.FormatString = ""
        City.HeaderText = "City"
        City.Name = Colcity
        City.Width = 150
        City.ReadOnly = True
        City.IsVisible = False
        City.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        dgstate.MasterTemplate.Columns.Add(City)


        Dim Cityname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Cityname.FormatString = ""
        Cityname.HeaderText = "City Name"
        Cityname.Name = Colcityname
        Cityname.Width = 150
        Cityname.ReadOnly = True
        Cityname.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        dgstate.MasterTemplate.Columns.Add(Cityname)


        dgstate.AllowDeleteRow = True
        dgstate.AllowAddNewRow = False
        dgstate.ShowGroupPanel = False
        dgstate.AllowColumnReorder = False
        dgstate.AllowRowReorder = False
        dgstate.EnableSorting = False
        dgstate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        dgstate.MasterTemplate.ShowRowHeaderColumn = False
        dgstate.Rows.AddNew()

    End Sub
    'Keypress validation on finder and converting lower case to upper case
    Public Sub key_press(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If (e.KeyChar = Chr(39)) Then
            e.Handled = True

        End If
    End Sub
    'Keypress validation on finder and converting lower case to upper case
    Public Sub fnddesig_keypress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        'If (e.KeyChar = Chr(39)) Then
        '    e.Handled = True
        'End If
    End Sub
    'It will check the designation in database ,if it exist then it will allow to add
    Sub fnddesileave()
        Try
            Dim strquery As String = "select designation_id from TSPL_Designation_MASTER where designation_id='" + fnddesignation.Value + "'"
            Dim strvalue As String = clsDBFuncationality.getSingleValue(strquery)
            'While dr.Read()
            '    strvalue = dr(0).ToString()
            'End While
            If strvalue <> "" Or fnddesignation.Value = "" Then
            Else : strvalue = ""
                common.clsCommon.MyMessageBoxShow(Me, "This Designation does not exist in Master Table", Me.Text)
                fnddesignation.Value = ""
            End If
        Catch ex As Exception

            myMessages.myExceptions(ex)
        End Try
    End Sub
    Public Sub fnddesi_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    'it will check the data existance in table on leave of fndempcode 
    Sub fndempleave()
        Try
            Dim strquery As String = "select segment_code,description from tspl_gl_segment_code where segment_code='" + fndempcode.Value + "'"
            Dim strvalue As String = clsDBFuncationality.getSingleValue(strquery)
            'While dr.Read()
            '    strvalue = dr(0).ToString()
            'End While
            If strvalue <> "" Or fndempcode.Value = "" Then
            Else : strvalue = ""
                common.clsCommon.MyMessageBoxShow(Me, "This Employee code does not exist in Master Table", Me.Text)
                fndempcode.Value = ""
                txtname.Text = ""
            End If
        Catch ex As Exception

            myMessages.myExceptions(ex)
        End Try
    End Sub
    Public Sub fndemp_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    'It will check the value in databse ,if it exist then it will fill the field otherwise it will blank the fields
    Sub textchangedsub()
        Try
            Dim strquery As String = "select * from TSPL_Employee_MASTER where emp_code='" + fndempcode.Value + "'"
            Dim strvalue As String = clsDBFuncationality.getSingleValue(strquery)
            'While dr.Read()
            '    strvalue = dr(0).ToString()
            'End While
            If strvalue <> "" Then
                funfill()
            Else
                Dim str1 As String = "select segment_code,description from tspl_gl_segment_code where segment_code='" + fndempcode.Value + "' AND Seg_No=4"
                Dim dr1 As DataTable
                Dim chk As String = ""
                Dim des As String = ""
                dr1 = clsDBFuncationality.GetDataTable(str1)
                For Each row As DataRow In dr1.Rows
                    chk = row(0).ToString()
                    des = row(1).ToString()
                Next
                If chk <> "" Then
                    txtname.Text = des
                    fnddesignation.Value = ""
                    txtEmailId.Text = ""
                    txtadd1.Text = ""
                    txtadd2.Text = ""
                    txtpin.Text = ""
                    txtphone.Text = ""
                    dtpdob.Value = Date.Today
                    txtshex.Text = ""
                    txtcardno.Text = ""
                    dtpjoin.Value = Date.Today
                    ddlemptype.SelectedValue = ""
                    'dgstate.Visible = False
                    dtpexdate.Value = Date.Today
                    ddlempstatus.Text = "Active"
                    dtpreleaving.Value = Date.Today
                    txtpayroll.Text = ""
                    txtempty.Text = ""
                    btnsave.Text = "Save"
                    btndelete.Enabled = False

                Else
                    'txtname.Text = ""
                    fnddesignation.Value = ""
                    txtadd1.Text = ""
                    txtadd2.Text = ""
                    txtpin.Text = ""
                    txtphone.Text = ""
                    dtpdob.Value = Date.Today
                    txtshex.Text = ""
                    txtcardno.Text = ""
                    dtpjoin.Value = Date.Today
                    ddlemptype.SelectedValue = ""
                    'dgstate.Visible = False
                    dtpexdate.Value = Date.Today
                    ddlempstatus.Text = "Active"
                    dtpreleaving.Value = Date.Today
                    txtpayroll.Text = ""
                    txtempty.Text = ""
                    btnsave.Text = "Save"
                    btndelete.Enabled = False
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Public Sub text_changed(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    'To fill Drop Down List with table tspl_fixed_parameter
    Public Sub funddltypefill()
        Try
            Dim strquery As String = ""
            If ChkEmployeeTypeParavet = True Then
                strquery = " select 'Paravet' as 'Code' , 'Paravet' as 'Type' union  "
                strquery = strquery + " select a.* from (select code,type from tspl_fixed_parameter where description='Employee Type' union all select '' as code,'Select' as type)a order by code"
            Else
                strquery = " select a.* from (select code,type from tspl_fixed_parameter where description='Employee Type' union all select '' as code,'Select' as type)a order by code"
            End If

            transportSql.FillComboBox(strquery, ddlemptype, "type", "type")
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'To fill the drop down list during runtime with table tspl_fixed_parameter
    Public Sub funddlstatusfill()
        Try
            Dim strquery As String = "select type from tspl_fixed_parameter where description='Employee status'"
            transportSql.FillComboBox(strquery, ddlempstatus, "type", "type")
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'It will fill all controls in screen if find any existing data in table 
    Public Sub funfill()
        Dim emp_code As String = fndempcode.Value
        Try
            Dim str As String = "select emp_Name,designation,add1,add2,pin_code,phone,birth_date,cash,card_no,joining_date,emp_type,exdate,emp_status," _
            & " rel_date,payroll_code,empty_ex, GL_Account,email_id from TSPL_Employee_MASTER "
            ' Left Join (select a.* from (select code,type from " _
            ' & " tspl_fixed_parameter where description='Employee Type' union all select '' as code,'Select' as type)a)a on (a.type=TSPL_Employee_MASTER.emp_type or a.code=TSPL_Employee_MASTER.emp_type) " _
            str += " where Emp_Code='" + fndempcode.Value + "'"
            Dim dr As DataTable
            dr = clsDBFuncationality.GetDataTable(str)
            For Each row As DataRow In dr.Rows
                isInsideLoadData = True
                txtEmailId.Text = row("email_id").ToString()
                txtname.Text = row(0).ToString()
                fnddesignation.Value = row(1).ToString()
                txtadd1.Text = row(2).ToString()
                txtadd2.Text = row(3).ToString()
                txtpin.Text = row(4).ToString()
                txtphone.Text = row(5).ToString()
                'dtpdob.Value = row(6).ToString()
                'dtpdob.Value = Convert.ToDateTime(row(6).ToString())
                dtpdob.Value = CDate(row(6).ToString())
                txtshex.Text = row(7).ToString()
                txtcardno.Text = row(8).ToString()
                'dtpjoin.Value = row(9).ToString()
                'dtpjoin.Value = Convert.ToDateTime(row(9).ToString())
                If row(9) IsNot Nothing AndAlso clsCommon.myLen(row(9)) > 0 AndAlso IsDate(row(9)) Then
                    dtpjoin.Value = CDate(row(9).ToString())
                End If

                ddlemptype.SelectedValue = row(10).ToString()
                'If ddlemptype.Text = "ASM/ZM" Then
                '    dgstate.Visible = True
                'Else
                '    dgstate.Visible = False
                'End If
              
                'dtpexdate.Value = row(11).ToString()
                'dtpexdate.Value = Convert.ToDateTime(row(11).ToString())
                If row(11) IsNot Nothing AndAlso clsCommon.myLen(row(11)) > 0 AndAlso IsDate(row(11)) Then
                    dtpexdate.Value = CDate(row(11).ToString())
                End If



                ddlempstatus.Text = row(12).ToString()

                If row(13) IsNot Nothing AndAlso clsCommon.myLen(row(13)) > 0 AndAlso IsDate(row(13)) Then
                    dtpreleaving.Value = CDate(row(13).ToString())
                End If


                txtpayroll.Text = row(14).ToString()
                txtempty.Text = row(15).ToString()
                TxtGLAccount.Value = clsCommon.myCstr(row(16))
            Next
            dgstate.Rows.Clear()
            dgstate.Rows.AddNew()
            If clsCommon.myLen(ddlemptype.SelectedValue) > 0 Then
                'dgstate.Visible = True
                isInsideLoadData = True
                Dim Counter As Integer = 0

                Dim arr As List(Of clsEmpASMZMDetails) = clsEmpASMZMDetails.GetData(emp_code)
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    For Each obj As clsEmpASMZMDetails In arr

                        dgstate.Rows(Counter).Cells(ColStateCode).Value = obj.statecode
                        dgstate.Rows(Counter).Cells(ColStatename).Value = obj.statename
                        dgstate.Rows(Counter).Cells(ColRegionCode).Value = obj.regioncode
                        dgstate.Rows(Counter).Cells(ColRegionname).Value = obj.regionname
                        dgstate.Rows(Counter).Cells(Colcity).Value = obj.citycode
                        dgstate.Rows(Counter).Cells(Colcityname).Value = obj.cityname
                        dgstate.Rows.AddNew()
                        Counter += 1
                    Next

                End If
                isInsideLoadData = False

            End If
            btndelete.Enabled = True
            btnsave.Enabled = True
            btnsave.Text = "Update"
            'If userCode <> "ADMIN" Then
            '    If funSetUserAccess() = False Then Exit Sub
            'End If

            isInsideLoadData = False
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'Funtion for insertion of data
    Public Sub funinsert()
        Try
            Dim dob As String = Format(dtpdob.Value, "dd/MM/yyyy")
            Dim joindate As String = Format(dtpjoin.Value, "dd/MM/yyyy")
            Dim exdate As String = Format(dtpexdate.Value, "dd/MM/yyyy")
            Dim reldate As String = Format(dtpreleaving.Value, "dd/MM/yyyy")
            Dim strstatus As String
            Dim emp_code As String = fndempcode.Value
            If ddlempstatus.Text = "Inactive" Then
                strstatus = "Y"
            Else
                strstatus = "N"
            End If
            Dim strexcise As String = "F"
            Dim strlogical As String = "Logical"

            Dim add3 As String = ""
            Dim add4 As String = ""
            Dim city As String = ""
            Dim state As String = ""
            Dim country As String = ""
            Dim email As String = ""
            Dim statusdate As String = ""

            Dim Loc_Segment_Code As String = ""
            Dim Type As String = ""
            Dim Purchase_Tax_Group As String = ""
            Dim Sales_Tax_Group As String = ""
            Dim Ecc_Number As String = ""
            Dim Registration_Number As String = ""
            Dim Commissionerate As String = ""
            Dim Range_Code As String = ""
            Dim Range_Name As String = ""
            Dim Range_Address As String = ""
            Dim Division_Code As String = ""
            Dim Division_Name As String = ""
            Dim Division_Address As String = ""
            Dim TinNo As String = ""
            Dim TanNo As String = ""
            Dim TcanNo As String = ""
            Dim ServiceTaxRegNo As String = ""

            Dim pur As String = ""
            Dim sal As String = ""
            Dim duty As String = "N"

            connectSql.RunSp("sp_EmployeeMaster_insert", New SqlParameter("@empcode", fndempcode.Value), New SqlParameter("@empname", txtname.Text.ToString()), New SqlParameter("@designation", fnddesignation.Value.ToString()), New SqlParameter("@add1", txtadd1.Text.ToString()), New SqlParameter("@add2", txtadd2.Text.ToString()), New SqlParameter("@pin", txtpin.Text.ToString()), New SqlParameter("@phone", txtphone.Text.ToString()), New SqlParameter("@dob", dob), New SqlParameter("@cash", clsCommon.myCstr(clsCommon.myCdbl(txtshex.Text))), New SqlParameter("@cardno", txtcardno.Text.ToString()), New SqlParameter("@joindate", joindate), New SqlParameter("@emptype", ddlemptype.SelectedValue.ToString()), New SqlParameter("@exdate", exdate), New SqlParameter("@empstatus", ddlempstatus.Text.ToString()), New SqlParameter("@rel_date", reldate), New SqlParameter("@payroll", txtpayroll.Text.ToString()), New SqlParameter("@emptyex", clsCommon.myCstr(clsCommon.myCdbl(txtempty.Text))), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode), New SqlParameter("@GLAccount", clsCommon.myCstr(TxtGLAccount.Value)), New SqlParameter("@EmailId", txtEmailId.Text))

            If ddlemptype.Text = "Salesman" Then

                connectSql.RunSp("sp_TSPL_LOCATION_MASTER_insert", New SqlParameter("@LocCode", fndempcode.Value), New SqlParameter("@LocDesc", txtname.Text.ToString()), New SqlParameter("@Add1", txtadd1.Text.ToString()), New SqlParameter("@Add2", txtadd2.Text.ToString()), New SqlParameter("@Add3", add3), New SqlParameter("@Add4", add4), New SqlParameter("@citycode", city), New SqlParameter("@state", state), New SqlParameter("@PinCode", txtpin.Text.ToString()), New SqlParameter("@country", country), New SqlParameter("@Telephone", txtphone.Text.ToString()), New SqlParameter("@email", email), New SqlParameter("@Location", strlogical), New SqlParameter("@Status", strstatus), New SqlParameter("@statusdate", statusdate), New SqlParameter("@Excisable", strexcise), New SqlParameter("@Loc_Segment_Code", Loc_Segment_Code), New SqlParameter("@Type", Type), New SqlParameter("@Purchase_Tax_Group", Purchase_Tax_Group), New SqlParameter("@Sales_Tax_Group", Sales_Tax_Group), New SqlParameter("@Ecc_Number", Ecc_Number), New SqlParameter("@Registration_Number", Registration_Number), New SqlParameter("@Commissionerate", Commissionerate), New SqlParameter("@Range_Code", Range_Code), New SqlParameter("@Range_Name", Range_Name), New SqlParameter("@Range_Address", Range_Address), New SqlParameter("@Division_Code", Division_Code), New SqlParameter("@Division_Name", Division_Name), New SqlParameter("@Division_Address", Division_Address), New SqlParameter("@TinNo", TinNo), New SqlParameter("@TanNo", TanNo), New SqlParameter("@TcanNo", TcanNo), New SqlParameter("@ServiceTaxRegNo", ServiceTaxRegNo), New SqlParameter("@DutyPaid", duty), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode), New SqlParameter("@purchaseTaxGroupIS", pur), New SqlParameter("@SalesTaxGroupIS", sal), New SqlParameter("@Stock_Transfer_Filled_Ac", ""), New SqlParameter("@Stock_Transfer_Empty_Ac", ""), New SqlParameter("@CST_NO", ""), New SqlParameter("@PHONE1", ""), New SqlParameter("@PHONE2", ""))

            End If

            If clsCommon.myLen(fndempcode.Value) > 0 Then
                Dim transs As SqlTransaction = clsDBFuncationality.GetTransactin()
                Try
                    Dim qry As String = "delete from TSPL_emptype_ASMZM_details where emp_code='" + fndempcode.Value + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, transs)
                    transs.Commit()
                Catch exx As Exception
                    transs.Rollback()
                End Try
            End If
            If clsCommon.CompairString(ddlemptype.SelectedValue, "ASM") = CompairStringResult.Equal Or clsCommon.CompairString(ddlemptype.SelectedValue, "Service Dealer") = CompairStringResult.Equal Then
                SavingData()

            End If

            
            myMessages.insert()
            btnsave.Text = "Update"
            btndelete.Enabled = True
            'If userCode <> "ADMIN" Then
            '    If funSetUserAccess() += False Then Exit Sub
            'End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'Funtion for updation  of data
    Public Sub funupdate()
        Try
            Dim dob As String = Format(dtpdob.Value, "dd/MM/yyyy")
            Dim joindate As String = Format(dtpjoin.Value, "dd/MM/yyyy")
            Dim exdate As String = Format(dtpexdate.Value, "dd/MM/yyyy")
            Dim reldate As String = Format(dtpreleaving.Value, "dd/MM/yyyy")
            Dim currentdate As Date = Date.Today

            Dim strstatus As String

            If ddlempstatus.Text = "Inactive" Then
                strstatus = "Y"
            Else
                strstatus = "N"
            End If
            Dim strexcise As String = "F"
            Dim strlogical As String = "Logical"

            Dim add3 As String = ""
            Dim add4 As String = ""
            Dim city As String = ""
            Dim state As String = ""
            Dim country As String = ""
            Dim email As String = ""
            Dim statusdate As String = ""

            Dim Loc_Segment_Code As String = ""
            Dim Type As String = ""
            Dim Purchase_Tax_Group As String = ""
            Dim Sales_Tax_Group As String = ""
            Dim Ecc_Number As String = ""
            Dim Registration_Number As String = ""
            Dim Commissionerate As String = ""
            Dim Range_Code As String = ""
            Dim Range_Name As String = ""
            Dim Range_Address As String = ""
            Dim Division_Code As String = ""
            Dim Division_Name As String = ""
            Dim Division_Address As String = ""
            Dim TinNo As String = ""
            Dim TanNo As String = ""
            Dim TcanNo As String = ""
            Dim ServiceTaxRegNo As String = ""

            Dim pur As String = ""
            Dim sal As String = ""
            Dim duty As String = "N"
            connectSql.RunSp("sp_EmployeeMaster_update", New SqlParameter("@empcode", fndempcode.Value), New SqlParameter("@empname", txtname.Text.ToString()), New SqlParameter("@designation", fnddesignation.Value.ToString()), New SqlParameter("@add1", txtadd1.Text.ToString()), New SqlParameter("@add2", txtadd2.Text.ToString()), New SqlParameter("@pin", txtpin.Text.ToString()), New SqlParameter("@phone", txtphone.Text.ToString()), New SqlParameter("@dob", dob), New SqlParameter("@cash", clsCommon.myCstr(clsCommon.myCdbl(txtshex.Text))), New SqlParameter("@cardno", txtcardno.Text.ToString()), New SqlParameter("@joindate", joindate), New SqlParameter("@emptype", ddlemptype.SelectedValue.ToString()), New SqlParameter("@exdate", exdate), New SqlParameter("@empstatus", ddlempstatus.Text.ToString()), New SqlParameter("@rel_date", reldate), New SqlParameter("@payroll", txtpayroll.Text.ToString()), New SqlParameter("@emptyex", clsCommon.myCstr(clsCommon.myCdbl(txtempty.Text))), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode), New SqlParameter("@GLAccount", clsCommon.myCstr(TxtGLAccount.Value)), New SqlParameter("@EmailId", txtEmailId.Text))
            If ddlemptype.Text = "Salesman" Then

                Dim strquery As String = "select * from TSPL_LOCATION_MASTER where location_code='" + fndempcode.Value + "'"
                Dim dr As DataTable
                Dim strvalue As String = ""

                dr = clsDBFuncationality.GetDataTable(strquery)

                If dr IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                    strvalue = dr.Rows(0)(0).ToString()
                End If

                If strvalue <> "" Then
                    Loc_Segment_Code = clsCommon.myCstr(connectSql.RunScalar("Select Loc_Segment_Code from TSPL_LOCATION_MASTER Where Location_Code='" + strvalue + "'"))
                    connectSql.RunSp("sp_TSPL_LOCATION_MASTER_update", New SqlParameter("@LocCode", fndempcode.Value), New SqlParameter("@LocDesc", txtname.Text.ToString()), New SqlParameter("@Add1", txtadd1.Text.ToString()), New SqlParameter("@Add2", txtadd2.Text.ToString()), New SqlParameter("@Add3", add3), New SqlParameter("@Add4", add4), New SqlParameter("@citycode", city), New SqlParameter("@state", state), New SqlParameter("@PinCode", txtpin.Text.ToString()), New SqlParameter("@country", country), New SqlParameter("@Telephone", txtphone.Text.ToString()), New SqlParameter("@email", email), New SqlParameter("@Location", strlogical), New SqlParameter("@Status", strstatus), New SqlParameter("@statusdate", statusdate), New SqlParameter("@Excisable", strexcise), New SqlParameter("@Loc_Segment_Code", Loc_Segment_Code), New SqlParameter("@Type", Type), New SqlParameter("@Purchase_Tax_Group", Purchase_Tax_Group), New SqlParameter("@Sales_Tax_Group", Sales_Tax_Group), New SqlParameter("@Ecc_Number", Ecc_Number), New SqlParameter("@Registration_Number", Registration_Number), New SqlParameter("@Commissionerate", Commissionerate), New SqlParameter("@Range_Code", Range_Code), New SqlParameter("@Range_Name", Range_Name), New SqlParameter("@Range_Address", Range_Address), New SqlParameter("@Division_Code", Division_Code), New SqlParameter("@Division_Name", Division_Name), New SqlParameter("@Division_Address", Division_Address), New SqlParameter("@TinNo", TinNo), New SqlParameter("@TanNo", TanNo), New SqlParameter("@TcanNo", TcanNo), New SqlParameter("@ServiceTaxRegNo", ServiceTaxRegNo), New SqlParameter("@DutyPaid", duty), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode), New SqlParameter("@purchaseTaxGroupIS", pur), New SqlParameter("@SalesTaxGroupIS", sal), New SqlParameter("@Stock_Transfer_Filled_Ac", ""), New SqlParameter("@Stock_Transfer_Empty_Ac", ""), New SqlParameter("@CST_NO", ""), New SqlParameter("@PHONE1", ""), New SqlParameter("@PHONE2", ""))
                Else
                    connectSql.RunSp("sp_TSPL_LOCATION_MASTER_insert", New SqlParameter("@LocCode", fndempcode.Value), New SqlParameter("@LocDesc", txtname.Text.ToString()), New SqlParameter("@Add1", txtadd1.Text.ToString()), New SqlParameter("@Add2", txtadd2.Text.ToString()), New SqlParameter("@Add3", add3), New SqlParameter("@Add4", add4), New SqlParameter("@citycode", city), New SqlParameter("@state", state), New SqlParameter("@PinCode", txtpin.Text.ToString()), New SqlParameter("@country", country), New SqlParameter("@Telephone", txtphone.Text.ToString()), New SqlParameter("@email", email), New SqlParameter("@Location", strlogical), New SqlParameter("@Status", strstatus), New SqlParameter("@statusdate", statusdate), New SqlParameter("@Excisable", strexcise), New SqlParameter("@Loc_Segment_Code", Loc_Segment_Code), New SqlParameter("@Type", Type), New SqlParameter("@Purchase_Tax_Group", Purchase_Tax_Group), New SqlParameter("@Sales_Tax_Group", Sales_Tax_Group), New SqlParameter("@Ecc_Number", Ecc_Number), New SqlParameter("@Registration_Number", Registration_Number), New SqlParameter("@Commissionerate", Commissionerate), New SqlParameter("@Range_Code", Range_Code), New SqlParameter("@Range_Name", Range_Name), New SqlParameter("@Range_Address", Range_Address), New SqlParameter("@Division_Code", Division_Code), New SqlParameter("@Division_Name", Division_Name), New SqlParameter("@Division_Address", Division_Address), New SqlParameter("@TinNo", TinNo), New SqlParameter("@TanNo", TanNo), New SqlParameter("@TcanNo", TcanNo), New SqlParameter("@ServiceTaxRegNo", ServiceTaxRegNo), New SqlParameter("@DutyPaid", duty), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode), New SqlParameter("@purchaseTaxGroupIS", pur), New SqlParameter("@SalesTaxGroupIS", sal), New SqlParameter("@Stock_Transfer_Filled_Ac", ""), New SqlParameter("@Stock_Transfer_Empty_Ac", ""), New SqlParameter("@CST_NO", ""), New SqlParameter("@PHONE1", ""), New SqlParameter("@PHONE2", ""))
                End If
            Else
                connectSql.RunSp("sp_TSPL_LOCATION_MASTER_delete", New SqlParameter("@LocCode", fndempcode.Value))
            End If

            If clsCommon.myLen(fndempcode.Value) > 0 Then
                Dim transs As SqlTransaction = clsDBFuncationality.GetTransactin()
                Try
                    Dim qry As String = "delete from TSPL_emptype_ASMZM_details where emp_code='" + fndempcode.Value + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, transs)
                    transs.Commit()
                Catch exx As Exception
                    transs.Rollback()
                End Try
            End If

            If clsCommon.CompairString(ddlemptype.SelectedValue, "ASM") = CompairStringResult.Equal Or clsCommon.CompairString(ddlemptype.SelectedValue, "Service Dealer") = CompairStringResult.Equal Then
                SavingData()
            End If



            myMessages.update()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Sub SavingData()

        Try
            Dim arr As New List(Of clsEmpASMZMDetails)
            Dim obj As New clsEmpASMZMDetails()
            If AllowToSave() = True Then
                arr = New List(Of clsEmpASMZMDetails)

                For ii As Integer = 0 To dgstate.Rows.Count - 1

                    obj = New clsEmpASMZMDetails()

                    obj.empcode = clsCommon.myCstr(fndempcode.Value)
                    obj.empltype = clsCommon.myCstr(ddlemptype.SelectedValue)
                    obj.statecode = clsCommon.myCstr(dgstate.Rows(ii).Cells(ColStateCode).Value)
                    obj.statename = clsCommon.myCstr(dgstate.Rows(ii).Cells(ColStatename).Value)
                    obj.regioncode = clsCommon.myCstr(dgstate.Rows(ii).Cells(ColRegionCode).Value)
                    obj.regionname = clsCommon.myCstr(dgstate.Rows(ii).Cells(ColRegionname).Value)
                    obj.citycode = clsCommon.myCstr(dgstate.Rows(ii).Cells(Colcity).Value)
                    obj.cityname = clsCommon.myCstr(dgstate.Rows(ii).Cells(Colcityname).Value)

                    If clsCommon.myLen(obj.statecode) > 0 Then
                        arr.Add(obj)
                    End If
                Next


                If clsEmpASMZMDetails.SaveData(obj.empcode, arr) = True Then

                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Function AllowToSave() As Boolean
        For ii As Integer = 0 To dgstate.Rows.Count - 1
            For jj As Integer = ii + 1 To dgstate.Rows.Count - 1

                If clsCommon.myCstr(dgstate.Rows(0).Cells("State Code").Value) Is Nothing OrElse clsCommon.myLen(dgstate.Rows(0).Cells(ColStateCode).Value) <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Fill the state details", Me.Text)
                    Return False
                End If
            Next
        Next
        '-------comment by Monika 16/05/2014 no need of checking the whole grid only check first row



        For ii As Integer = 0 To dgstate.Rows.Count - 1
            For jj As Integer = ii + 1 To dgstate.Rows.Count - 1
                If ((clsCommon.CompairString(dgstate.Rows(ii).Cells(ColRegionCode).Value, dgstate.Rows(jj).Cells(ColRegionCode).Value) = CompairStringResult.Equal) AndAlso (clsCommon.CompairString(dgstate.Rows(ii).Cells(ColStateCode).Value, dgstate.Rows(jj).Cells(ColStateCode).Value) = CompairStringResult.Equal) AndAlso (clsCommon.CompairString(dgstate.Rows(ii).Cells(Colcity).Value, dgstate.Rows(jj).Cells(Colcity).Value) = CompairStringResult.Equal)) AndAlso Not clsCommon.CompairString(dgstate.Rows(ii).Cells(ColRegionCode).Value, "") = CompairStringResult.Equal Then
                    Throw New Exception("The state,region,city value Already Exist")
                    Return False
                End If
            Next
        Next
        Return True
    End Function
    'Function for deletion of data

    Public Sub fundelete()


        Try
            Dim qry = "select count(*) from tspl_transfer_head where Salesmancode='" + fndempcode.Value + "'"
            Dim count As Integer = clsDBFuncationality.getSingleValue(qry)
            '----------for empty transaction----
            Dim qry1 As String = "select count(*) from TSPL_ADJUSTMENT_HEADER where EMP_CODE='" + fndempcode.Value + "'"
            Dim count1 As Integer = clsDBFuncationality.getSingleValue(qry1)
            If (count = 0 AndAlso count1 = 0) Then
                connectSql.RunSp("sp_EmployeeMaster_delete", New SqlParameter("@emp_code", fndempcode.Value.ToString()))
                If ddlemptype.Text = "Salesman" Then

                    connectSql.RunSp("sp_TSPL_LOCATION_MASTER_delete", New SqlParameter("@LocCode", fndempcode.Value))
                ElseIf ddlemptype.Text = "ASM" AndAlso ddlemptype.Text = "ZM" Then
                    dgstate.Visible = True

                    Dim qst As String = "Delete  from TSPL_emptype_ASMZM_details WHERE emp_code='" + fndempcode.Value + "'"
                    clsDBFuncationality.ExecuteNonQuery(qst)
                    dgstate.Visible = True
                End If
                myMessages.delete()
                btnsave.Text = "Save"
                btndelete.Enabled = False
            Else
                common.clsCommon.MyMessageBoxShow("This Record Cannot be deleted." + Environment.NewLine + "It is used by another process")
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
       
    'It will reset all the controls in screens
    Public Sub funreset()
        fndempcode.MyReadOnly = False
        txtEmailId.Text = ""
        fndempcode.Value = ""
        txtname.Text = ""
        fnddesignation.Value = ""
        txtadd1.Text = ""
        txtadd2.Text = ""
        txtpin.Text = ""
        txtphone.Text = ""
        dtpdob.Value = Date.Today
        txtshex.Text = ""
        txtcardno.Text = ""
        dtpjoin.Value = Date.Today
        dtpexdate.Value = Date.Today
        ddlemptype.Text = ""
        ddlempstatus.Text = "Active"
        dtpreleaving.Text = Date.Today
        txtpayroll.Text = ""
        txtempty.Text = ""
        btnsave.Text = "Save"
        btndelete.Enabled = False
        TxtGLAccount.Value = ""
        LoadBlankGrid()
        dgstate.Visible = False
        IsValueChanged = False
    End Sub
    'closing of current window form
    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        funreset()
    End Sub


    'Delete funtion call on delete button
    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If fndempcode.Value = "" Then
            myMessages.blankValue("Employee Code")
        ElseIf myMessages.deleteConfirm() Then
            fundelete()


        End If
    End Sub
    'Validation on save button click and calling funinsert,funupdate
    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub
    Sub SaveData()
        Try
            If fndempcode.Value = "" Then
                myMessages.blankValue("Employee Code")
                fndempcode.Focus()
            ElseIf txtname.Text = "" Then
                myMessages.blankValue("Employee Name")
                txtname.Focus()
            ElseIf fnddesignation.Value = "" Then
                myMessages.blankValue("Designation")
                fnddesignation.Focus()
            ElseIf ddlemptype.Text = "" Then
                myMessages.blankValue("Employee Type")
                ddlemptype.Focus()
            ElseIf ddlempstatus.Text = "" Then
                myMessages.blankValue("Employee Status")
                ddlempstatus.Focus()

            ElseIf txtEmailId.Text IsNot Nothing AndAlso clsCommon.myLen(txtEmailId.Text) > 100 Then
                clsCommon.MyMessageBoxShow(Me, "E-Mail Id should be of 100 characters max.", Me.Text)
                txtEmailId.Focus()
                txtEmailId.Select()
                Return
            Else

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.EmployeeMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If

                If btnsave.Text = "Save" Then
                    funinsert()
                ElseIf btnsave.Text = "Update" Then
                    funupdate()
                End If
            End If



        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Private Sub fndempcode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndempcode.ConnectionString = connectSql.SqlCon()
        'fndempcode.Query = " select segment_code As [Segment Code],description  as [Description]from TSPL_GL_Segment_code where seg_no='4'"
        'fndempcode.ValueToSelect = "Segment Code"
        'fndempcode.Caption = "Segment Code"
        'fndempcode.ValueToSelect1 = "Description"
    End Sub

    Private Sub fnddesignation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fnddesignation.ConnectionString = connectSql.SqlCon()
        'fnddesignation.Query = " select designation_id As [Designation Id],designation_desc  as [Description]from TSPL_Designation_MASTER "
        'fnddesignation.ValueToSelect = "Designation Id"
        'fnddesignation.Caption = "Designation Master"
        'fnddesignation.ValueToSelect1 = "Description"
    End Sub
    ''For Export functionality 
    'Private Sub empex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles empex.Click

    '    sql = "select emp_code AS [Employee Code],emp_name As [Employee Name],designation AS [Designation],add1 as [Address1],add2 as [Address2],pin_code as [Pin Code],phone as [Phone],birth_date as [Birth Date],cash as [Cash Sh/Ex],card_no As [Card Number],joining_date As [Joining Date],emp_type as [Employee Type],exdate as [Ex Date],emp_status as [Employee Status],rel_date AS [Releaving Date],payroll_code AS [Payroll Code],empty_ex as [Empty Sh/Ex], GL_Account as [GL Account],EMail_ID as [Email ID] from tspl_employee_Master"
    '    transportSql.ExporttoExcel(sql, Me)
    'End Sub
    'For Import functionality 
    'Private Sub empim_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles empim.Click
    '    Dim gv As New RadGridView()
    '    Me.Controls.Add(gv)
    '    Dim currentdate As Date = Date.Today
    '    If transportSql.importExcel(gv, "Employee Code", "Employee Name", "Designation", "Address1", "Address2", "Pin Code", "Phone", "Birth Date", "Cash Sh/Ex", "Card Number", "Joining Date", "Employee Type", "Ex Date", "Employee Status", "Releaving Date", "Payroll Code", "Empty Sh/Ex", "GL Account") Then
    '        Dim trans As SqlTransaction
    '        Dim bdate As String
    '        Dim jdate As String
    '        Dim exdate As String
    '        Dim add1 As String
    '        Dim add2 As String
    '        Dim add3 As String = ""
    '        Dim add4 As String = ""
    '        Dim city As String = ""
    '        Dim state As String = ""
    '        Dim country As String = ""
    '        Dim email As String = ""
    '        Dim statusdate As String = ""
    '        Dim strlogical As String = "Logical"
    '        Dim strexcise As String = "F"
    '        Dim Purchase_Tax_Group As String = ""
    '        Dim Sales_Tax_Group As String = ""
    '        Dim Ecc_Number As String = ""
    '        Dim Registration_Number As String = ""
    '        Dim Loc_Segment_Code As String = ""
    '        Dim Type As String = ""
    '        Dim Commissionerate As String = ""
    '        Dim Range_Code As String = ""
    '        Dim Range_Name As String = ""
    '        Dim Range_Address As String = ""
    '        Dim Division_Code As String = ""
    '        Dim Division_Name As String = ""
    '        Dim Division_Address As String = ""
    '        Dim TinNo As String = ""
    '        Dim TanNo As String = ""
    '        Dim TcanNo As String = ""
    '        Dim ServiceTaxRegNo As String = ""

    '        Dim pur As String = ""
    '        Dim sal As String = ""
    '        Dim duty As String = "N"

    '        Dim releavingdate As String
    '        Try
    '            'connectSql.OpenConnection()
    '            trans = clsDBFuncationality.GetTransactin()
    '            clsCommon.ProgressBarShow()

    '            For Each grow As GridViewRowInfo In gv.Rows
    '                Dim strcode As String = clsCommon.myCstr(grow.Cells(0).Value)
    '                Dim strname As String = clsCommon.myCstr(grow.Cells(1).Value)
    '                Dim strdesig As String = clsCommon.myCstr(grow.Cells(2).Value)
    '                If strdesig.Length > 12 Then
    '                    Throw New Exception("Check the length of designation")

    '                End If
    '                Dim stradd1 As String = clsCommon.myCstr(grow.Cells(3).Value)
    '                If stradd1.Length > 50 Then
    '                    Throw New Exception("Check the length of address1")

    '                End If
    '                Dim stradd2 As String = clsCommon.myCstr(grow.Cells(4).Value)
    '                If stradd2.Length > 50 Then
    '                    Throw New Exception("Check the length of address2")

    '                End If
    '                Dim strpin As String = clsCommon.myCstr(grow.Cells(5).Value)
    '                If strpin.Length > 10 Then
    '                    Throw New Exception("Check the length of pincode")

    '                End If
    '                Dim strphone As String = clsCommon.myCstr(grow.Cells(6).Value)
    '                If strphone.Length > 20 Then
    '                    Throw New Exception("Check the length of phone")

    '                End If
    '                'Dim strbdate As String = CDate(grow.Cells(7).Value).ToString("MM/dd/yyyy")
    '                'Dim bdate As String = grow.Cells(7).Value.ToString()
    '                Dim bdate1 As String = clsCommon.myCstr(grow.Cells(7).Value)
    '                If String.IsNullOrEmpty(bdate1) Then
    '                    Throw New Exception("Birth Date has some incorrect values")

    '                Else
    '                    bdate = CDate(bdate1)
    '                End If
    '                'Dim bdate As String = globalFunc.getDate(bdate1)
    '                Dim strcash As String = clsCommon.myCstr(grow.Cells(8).Value)
    '                If strcash.Length > 50 Then
    '                    Throw New Exception("Check the length of Cash ")
    '                ElseIf Not IsNumeric(strcash) Then
    '                    Throw New Exception("Please Insert Numeric Value In 'Cash Sh/Ex' Against Employee '" + strcode + "'")
    '                End If

    '                Dim strcard As String = clsCommon.myCstr(grow.Cells(9).Value)
    '                If strcard.Length > 50 Then
    '                    Throw New Exception("Check the length of Card ")

    '                End If
    '                'Dim strjdate As String = CDate(grow.Cells(10).Value).ToString("MM/dd/yyyy")
    '                'Dim jdate As String = grow.Cells(10).Value.ToString()
    '                Dim jdate1 As String = clsCommon.myCstr(grow.Cells(10).Value)
    '                If String.IsNullOrEmpty(jdate1) Then
    '                    Throw New Exception("Joining date Has Some incorrect values")

    '                Else
    '                    jdate = CDate(jdate1)
    '                End If
    '                'Dim jdate As String = globalFunc.getDate(jdate1)
    '                Dim stretype As String = clsCommon.myCstr(grow.Cells(11).Value)

    '                If stretype.Length > 20 Then
    '                    Throw New Exception("Check the length of Employee Type ")

    '                End If

    '                'Dim strexdate As String = CDate(grow.Cells(12).Value).ToString("MM/dd/yyyy")
    '                'Dim exdate As String = grow.Cells(12).Value.ToString()
    '                Dim exdate1 As String = clsCommon.myCstr(grow.Cells(12).Value)
    '                'Dim exdate As String = globalFunc.getDate(exdate1)
    '                If String.IsNullOrEmpty(exdate1) Then
    '                    Throw New Exception("Ex date has some incorrect value")

    '                Else
    '                    exdate = CDate(exdate1)
    '                End If
    '                Dim strstatus As String = clsCommon.myCstr(grow.Cells(13).Value)
    '                If strstatus.Length > 50 Then
    '                    Throw New Exception("Check the length of Employee Status ")

    '                End If
    '                Dim releavingdate1 As String = clsCommon.myCstr(grow.Cells(14).Value)
    '                If String.IsNullOrEmpty(releavingdate1) Then
    '                    Throw New Exception("Releaving date has some incorrect value")

    '                Else
    '                    releavingdate = CDate(releavingdate1)
    '                End If

    '                Dim strpay As String = clsCommon.myCstr(grow.Cells(15).Value)
    '                If strpay.Length > 50 Then
    '                    Throw New Exception("Check the length of Payroll ")

    '                End If
    '                Dim strempty As String = clsCommon.myCstr(grow.Cells(16).Value)
    '                If strempty.Length > 50 Then
    '                    Throw New Exception("Check the length of Employee Empty Ex ")
    '                ElseIf Not IsNumeric(strempty) Then
    '                    Throw New Exception("Please Insert Numeric Value In Empty Sh/Ex Against Employee '" + strcode + "'")
    '                End If
    '                If (String.IsNullOrEmpty(strcode)) Or strcode.Length > 12 Then
    '                    Throw New Exception("Employee Code can not be blank or incorrect")

    '                End If
    '                If String.IsNullOrEmpty(strname) Or strname.Length > 50 Then
    '                    Throw New Exception(" Employee Name can not be blank or incorrect")

    '                End If

    '                Dim strGLAccount As String = ""
    '                If clsCommon.myLen(grow.Cells("GL Account").Value) > 0 Then
    '                    strGLAccount = connectSql.RunScalar(trans, "SELECT Account_Code FROM TSPL_GL_ACCOUNTS WHERE Account_Code='" + clsCommon.myCstr(grow.Cells("GL Account").Value) + "'")
    '                    If clsCommon.myLen(strGLAccount) <= 0 Then
    '                        Throw New Exception(" The GL Account Against Employee '" + strcode + "' Does Not Exist, Please Insert a Valid Account Or Leave blank ")
    '                    End If
    '                End If
    '                Dim strEmail As String = clsCommon.myCstr(grow.Cells(16).Value)
    '                If strEmail.Length > 100 Then
    '                    Throw New Exception("Check the length of Email ID ")

    '                End If


    '                Dim sql1 As String = "select count(*) from tspl_Employee_master where emp_code='" + strcode + "'"
    '                Dim i As Integer = CInt(connectSql.RunScalar(trans, sql1))
    '                If (i = 0) Then

    '                    connectSql.RunSpTransaction(trans, "sp_EmployeeMaster_insert", New SqlParameter("@empcode", strcode), New SqlParameter("@empname", strname), New SqlParameter("@designation", strdesig), New SqlParameter("@add1", stradd1), New SqlParameter("@add2", stradd2), New SqlParameter("@pin", strpin), New SqlParameter("@phone", strphone), New SqlParameter("@dob", bdate), New SqlParameter("@cash", strcash), New SqlParameter("@cardno", strcard), New SqlParameter("@joindate", jdate), New SqlParameter("@emptype", stretype), New SqlParameter("@exdate", exdate), New SqlParameter("@empstatus", strstatus), New SqlParameter("@rel_date", releavingdate), New SqlParameter("@payroll", strpay), New SqlParameter("@emptyex", strempty), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode), New SqlParameter("@GLAccount", strGLAccount), New SqlParameter("@EmailId", strEmail))
    '                    If stretype = "Salesman" Then
    '                        connectSql.RunSpTransaction(trans, "sp_TSPL_LOCATION_MASTER_insert", New SqlParameter("@LocCode", strcode), New SqlParameter("@LocDesc", strname), New SqlParameter("@Add1", stradd1), New SqlParameter("@Add2", stradd2), New SqlParameter("@Add3", add3), New SqlParameter("@Add4", add4), New SqlParameter("@citycode", city), New SqlParameter("@state", state), New SqlParameter("@PinCode", strpin), New SqlParameter("@country", country), New SqlParameter("@Telephone", strphone), New SqlParameter("@email", email), New SqlParameter("@Location", strlogical), New SqlParameter("@Status", strstatus), New SqlParameter("@statusdate", statusdate), New SqlParameter("@Excisable", strexcise), New SqlParameter("@Loc_Segment_Code", Loc_Segment_Code), New SqlParameter("@Type", Type), New SqlParameter("@Purchase_Tax_Group", Purchase_Tax_Group), New SqlParameter("@Sales_Tax_Group", Sales_Tax_Group), New SqlParameter("@Ecc_Number", Ecc_Number), New SqlParameter("@Registration_Number", Registration_Number), New SqlParameter("@Commissionerate", Commissionerate), New SqlParameter("@Range_Code", Range_Code), New SqlParameter("@Range_Name", Range_Name), New SqlParameter("@Range_Address", Range_Address), New SqlParameter("@Division_Code", Division_Code), New SqlParameter("@Division_Name", Division_Name), New SqlParameter("@Division_Address", Division_Address), New SqlParameter("@TinNo", TinNo), New SqlParameter("@TanNo", TanNo), New SqlParameter("@TcanNo", TcanNo), New SqlParameter("@ServiceTaxRegNo", ServiceTaxRegNo), New SqlParameter("@DutyPaid", duty), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode), New SqlParameter("@purchaseTaxGroupIS", pur), New SqlParameter("@SalesTaxGroupIS", sal), New SqlParameter("@Stock_Transfer_Filled_Ac", ""), New SqlParameter("@Stock_Transfer_Empty_Ac", ""))
    '                    End If
    '                Else
    '                    connectSql.RunSpTransaction(trans, "sp_EmployeeMaster_update", New SqlParameter("@empcode", strcode), New SqlParameter("@empname", strname), New SqlParameter("@designation", strdesig), New SqlParameter("@add1", stradd1), New SqlParameter("@add2", stradd2), New SqlParameter("@pin", strpin), New SqlParameter("@phone", strphone), New SqlParameter("@dob", bdate), New SqlParameter("@cash", strcash), New SqlParameter("@cardno", strcard), New SqlParameter("@joindate", jdate), New SqlParameter("@emptype", stretype), New SqlParameter("@exdate", exdate), New SqlParameter("@empstatus", strstatus), New SqlParameter("@rel_date", releavingdate), New SqlParameter("@payroll", strpay), New SqlParameter("@emptyex", strempty), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode), New SqlParameter("@GLAccount", strGLAccount), New SqlParameter("@EmailId", strEmail))
    '                    If stretype = "Salesman" Then
    '                        Loc_Segment_Code = clsCommon.myCstr(connectSql.RunScalar(trans, "Select Loc_Segment_Code from TSPL_LOCATION_MASTER Where Location_Code='" + strcode + "'"))
    '                        connectSql.RunSpTransaction(trans, "sp_TSPL_LOCATION_MASTER_update", New SqlParameter("@LocCode", strcode), New SqlParameter("@LocDesc", strname), New SqlParameter("@Add1", stradd1), New SqlParameter("@Add2", stradd2), New SqlParameter("@Add3", add3), New SqlParameter("@Add4", add4), New SqlParameter("@citycode", city), New SqlParameter("@state", state), New SqlParameter("@PinCode", strpin), New SqlParameter("@country", country), New SqlParameter("@Telephone", strphone), New SqlParameter("@email", email), New SqlParameter("@Location", strlogical), New SqlParameter("@Status", strstatus), New SqlParameter("@statusdate", statusdate), New SqlParameter("@Excisable", strexcise), New SqlParameter("@Loc_Segment_Code", Loc_Segment_Code), New SqlParameter("@Type", Type), New SqlParameter("@Purchase_Tax_Group", Purchase_Tax_Group), New SqlParameter("@Sales_Tax_Group", Sales_Tax_Group), New SqlParameter("@Ecc_Number", Ecc_Number), New SqlParameter("@Registration_Number", Registration_Number), New SqlParameter("@Commissionerate", Commissionerate), New SqlParameter("@Range_Code", Range_Code), New SqlParameter("@Range_Name", Range_Name), New SqlParameter("@Range_Address", Range_Address), New SqlParameter("@Division_Code", Division_Code), New SqlParameter("@Division_Name", Division_Name), New SqlParameter("@Division_Address", Division_Address), New SqlParameter("@TinNo", TinNo), New SqlParameter("@TanNo", TanNo), New SqlParameter("@TcanNo", TcanNo), New SqlParameter("@ServiceTaxRegNo", ServiceTaxRegNo), New SqlParameter("@DutyPaid", duty), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode), New SqlParameter("@purchaseTaxGroupIS", pur), New SqlParameter("@SalesTaxGroupIS", sal), New SqlParameter("@Stock_Transfer_Filled_Ac", ""), New SqlParameter("@Stock_Transfer_Empty_Ac", ""))
    '                    End If
    '                End If
    '            Next
    '            trans.Commit()
    '            clsCommon.ProgressBarHide()
    '            common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
    '        Catch ex As Exception
    '            trans.Rollback()
    '            clsCommon.ProgressBarHide()
    '            myMessages.myExceptions(ex)

    '        End Try

    '    End If
    '    Me.Controls.Remove(gv)
    'End Sub
    'For closing current screen by menu strip Close
    Private Sub RadMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem4.Click
        Me.Close()
    End Sub


    Private Sub menuEmployeeMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuEmployeeMaster.Click
        'Dim frm As New frmEmployeeMasterRpt()
        'frm.Show()
    End Sub

    'For pin number number validation
    Private Sub txtpin_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtpin.KeyPress
        If ((e.KeyChar >= Chr(48) And e.KeyChar <= Chr(57)) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(48) Or e.KeyChar = Chr(45)) Then
        Else
            e.Handled = True
        End If
    End Sub
    'For phone number number validation
    Private Sub txtphone_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtphone.KeyPress
        If ((e.KeyChar >= Chr(48) And e.KeyChar <= Chr(57)) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(48) Or e.KeyChar = Chr(45)) Then
        Else
            e.Handled = True
        End If
    End Sub



    Private Sub fndempcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndempcode._MYValidating
        Dim str As String = "select count(*) from TSPL_GL_Segment_code where segment_code ='" + fndempcode.Value + "' AND Seg_No=4 "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 Then
            fndempcode.MyReadOnly = False
        Else
            fndempcode.MyReadOnly = True
        End If

        If fndempcode.MyReadOnly OrElse isButtonClicked Then
            'Dim qry As String = " select segment_code As Code,description  as [Description] from TSPL_GL_Segment_code  "
            'fndempcode.Value = clsCommon.ShowSelectForm("fmempcode", qry, "Code", "seg_no='4'", fndempcode.Value, "", isButtonClicked)
            fndempcode.Value = clsGLSegmentCode.getFinder("seg_no='4'", fndempcode.Value, isButtonClicked)
            txtname.Text = clsDBFuncationality.getSingleValue("select description from TSPL_GL_Segment_code where segment_code = '" + fndempcode.Value + "' AND Seg_No=4")

            ' fndUser_NameLeave()
            If fndempcode.Value IsNot Nothing Then
                btndelete.Enabled = True
            Else
                btndelete.Enabled = False
            End If
            btnsave.Enabled = True
            funddltypefill()

            fnddesileave()
            funddlstatusfill()
            ''dtpdob.Value = Date.Today
            ''dtpexdate.Value = Date.Today
            ''dtpjoin.Value = Date.Today
            ''dtpreleaving.Value = Date.Today
            ''ddlempstatus.Text = "Active"
            textchangedsub()
        End If
    End Sub

    Private Sub fndempcode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavigatorType As common.NavigatorType) Handles fndempcode._MYNavigator
        funddltypefill()
        fnddesileave()
        funddlstatusfill()
        'dtpdob.Value = Date.Today
        'dtpexdate.Value = Date.Today
        'dtpjoin.Value = Date.Today
        'dtpreleaving.Value = Date.Today
        ddlempstatus.Text = "Active"
        Dim qst As String = "select segment_code As [Segment Code],description  as [Description] from TSPL_GL_Segment_code where  2=2 "
        Select Case NavigatorType
            Case NavigatorType.Current
                '  qst += "and assign_to='" + txtassign.Value + "' "
                ' qst += "and job_code in ('" + txtcode1.Value + "')"
            Case NavigatorType.Next
                qst += "and segment_code in (select min(segment_code) from TSPL_GL_Segment_code where segment_code>'" + fndempcode.Value + "' and seg_no='4' ) "
            Case NavigatorType.First
                qst += "and segment_code in (select MIN(segment_code) from TSPL_GL_Segment_code where seg_no='4'  )"
            Case NavigatorType.Last
                qst += "and segment_code in (select Max(segment_code) from TSPL_GL_Segment_code where seg_no='4'  )"
            Case NavigatorType.Previous
                qst += "and segment_code in (select max(segment_code) from TSPL_GL_Segment_code where segment_code<'" + fndempcode.Value + "' and seg_no='4'  )"
        End Select
        ' fun_gridfill()
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            fndempcode.Value = clsCommon.myCstr(dt.Rows(0)("Segment Code"))
            txtname.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
        End If
        textchangedsub()
        If fndempcode.Value IsNot Nothing Then
            btndelete.Enabled = True
        Else
            btndelete.Enabled = False

        End If
        
    End Sub

    Private Sub fnddesignation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fnddesignation._MYValidating

        If isButtonClicked Then
            'Dim qry As String = " select designation_id As [Designation Id],designation_desc  as [Description]from TSPL_Designation_MASTER "
            '            fnddesignation.Value = clsCommon.ShowSelectForm("fmdesignation", qry, "Designation Id", "", fnddesignation.Value, "", isButtonClicked)
            '   txtname.Text = clsDBFuncationality.getSingleValue("select description from TSPL_GL_Segment_code where segment_code = '" + fnddesignation.Value + "'")
            fnddesignation.Value = clsDesignationMaster.getFinder("", fnddesignation.Value, isButtonClicked)

            fndempleave()
            funddltypefill()
            funddlstatusfill()
            dtpdob.Value = Date.Today
            dtpexdate.Value = Date.Today
            dtpjoin.Value = Date.Today
            dtpreleaving.Value = Date.Today
            ddlempstatus.Text = "Active"
            dgstate.Visible = False
        End If
    End Sub


    Private Sub TxtGLAccount__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles TxtGLAccount._MYValidating
        'Dim qry As String = " SELECT Account_Code AS [Code], Description FROM TSPL_GL_ACCOUNTS "
        'TxtGLAccount.Value = clsCommon.ShowSelectForm("GL Accounts", qry, "Code", "", TxtGLAccount.Value, "", isButtonClicked)
        TxtGLAccount.Value = clsGLAccount.getFinder("", TxtGLAccount.Value, isButtonClicked)
    End Sub


    Private Sub txtshex_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtshex.KeyPress

    End Sub

    Private Sub ddlemptype_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles ddlemptype.SelectedIndexChanged
        If clsCommon.CompairString(ddlemptype.Text, "ASM") = CompairStringResult.Equal Or clsCommon.CompairString(ddlemptype.Text, "ZM") = CompairStringResult.Equal Or clsCommon.CompairString(ddlemptype.Text, "Service Executive") = CompairStringResult.Equal Then
            dgstate.Visible = True
        Else
            dgstate.Visible = False
        End If
    End Sub


    Sub Openstatefinder(ByVal isbuttonclicked As Boolean)
        Dim strICode As String = clsCommon.myCstr(dgstate.CurrentRow.Cells(ColStateCode).Value)
        Dim qry As String = ""
        'If clsCommon.myLen(strICode) > 0 Then
        qry = " select TSPL_REGION_MASTER.REGION_CODE as [region code] ,TSPL_REGION_MASTER.REGION_NAME as [region name],TSPL_CITY_MASTER.City_Code as [city code] ,TSPL_CITY_MASTER.City_Name  as [city name],"
        qry += " TSPL_STATE_MASTER.STATE_CODE as [code],TSPL_STATE_MASTER.STATE_NAME  as [state name] from TSPL_city_master "
        qry += " inner join TSPL_REGION_MASTER on TSPL_REGION_MASTER.REGION_CODE =TSPL_CITY_MASTER .region_code "
        qry += " inner join tspl_state_master on TSPL_STATE_MASTER.STATE_CODE =TSPL_CITY_MASTER .STATE_CODE"
        Dim whrCls As String = " "

        'dgstate.CurrentRow.Cells(ColStateCode).Value = clsCommon.ShowSelectForm("stateRegionfinder", qry, "code", whrCls, clsCommon.myCstr(dgstate.CurrentRow.Cells(ColStateCode).Value), "code", True)
        'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        'If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
        '    dgstate.CurrentRow.Cells(ColStateCode).Value = clsCommon.myCstr(dt.Rows(0)("code"))
        '    dgstate.CurrentRow.Cells(ColStatename).Value = clsCommon.myCstr(dt.Rows(0)("state name"))
        '    dgstate.CurrentRow.Cells(ColRegionCode).Value = clsCommon.myCstr(dt.Rows(0)("region code"))
        '    dgstate.CurrentRow.Cells(ColRegionname).Value = clsCommon.myCstr(dt.Rows(0)("region name"))
        '    dgstate.CurrentRow.Cells(Colcity).Value = clsCommon.myCstr(dt.Rows(0)("city code"))
        '    dgstate.CurrentRow.Cells(Colcityname).Value = clsCommon.myCstr(dt.Rows(0)("city name"))
        '    dgstate.Rows.AddNew()
        'End If

        '--------------------Done By Monika 16/05/2014--------------All Comments Done By Me-----------------------------
        Dim dr As DataRow = clsCommon.ShowSelectFormForRow("RGNFND", qry)

        If dr IsNot Nothing Then
            dgstate.CurrentRow.Cells(ColStateCode).Value = clsCommon.myCstr(dr("code"))
            dgstate.CurrentRow.Cells(ColStatename).Value = clsCommon.myCstr(dr("state name"))
            dgstate.CurrentRow.Cells(ColRegionCode).Value = clsCommon.myCstr(dr("region code"))
            dgstate.CurrentRow.Cells(ColRegionname).Value = clsCommon.myCstr(dr("region name"))
            dgstate.CurrentRow.Cells(Colcity).Value = clsCommon.myCstr(dr("city code"))
            dgstate.CurrentRow.Cells(Colcityname).Value = clsCommon.myCstr(dr("city name"))
            'dgstate.Rows.AddNew()
        End If
        '-----------------------------------End--------------------------------------------

        'End If


    End Sub

    Private Sub dgstate_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles dgstate.CellValueChanged
        Try
            Dim counter = 0
            If Not isInsideLoadData Then
                If Not IsValueChanged Then
                    IsValueChanged = True


                    If e.Column Is dgstate.Columns(ColStateCode) Then
                        Openstatefinder(True)
                        IsValueChanged = False
                    End If

                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub btnEmpdetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmpdetails.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Employee Code", "Employee Name", "Designation", "Address1", "Address2", "Pin Code", "Phone", "Birth Date", "Cash Sh/Ex", "Card Number", "Joining Date", "Employee Type", "Ex Date", "Employee Status", "Releaving Date", "Payroll Code", "Empty Sh/Ex", "GL Account") Then
            Dim trans As SqlTransaction = Nothing
            Dim bdate As String
            Dim jdate As String
            Dim exdate As String
            'Dim add1 As String
            'Dim add2 As String
            Dim add3 As String = ""
            Dim add4 As String = ""
            Dim city As String = ""
            Dim state As String = ""
            Dim country As String = ""
            Dim email As String = ""
            Dim statusdate As String = ""
            Dim strlogical As String = "Logical"
            Dim strexcise As String = "F"
            Dim Purchase_Tax_Group As String = ""
            Dim Sales_Tax_Group As String = ""
            Dim Ecc_Number As String = ""
            Dim Registration_Number As String = ""
            Dim Loc_Segment_Code As String = ""
            Dim Type As String = ""
            Dim Commissionerate As String = ""
            Dim Range_Code As String = ""
            Dim Range_Name As String = ""
            Dim Range_Address As String = ""
            Dim Division_Code As String = ""
            Dim Division_Name As String = ""
            Dim Division_Address As String = ""
            Dim TinNo As String = ""
            Dim TanNo As String = ""
            Dim TcanNo As String = ""
            Dim ServiceTaxRegNo As String = ""

            Dim pur As String = ""
            Dim sal As String = ""
            Dim duty As String = "N"

            Dim releavingdate As String
            Try
                'connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()

                For Each grow As GridViewRowInfo In gv.Rows
                    Dim strcode As String = clsCommon.myCstr(grow.Cells(0).Value)
                    Dim strname As String = clsCommon.myCstr(grow.Cells(1).Value)
                    Dim strdesig As String = clsCommon.myCstr(grow.Cells(2).Value)
                    If strdesig.Length > 12 Then
                        Throw New Exception("Check the length of designation")

                    End If
                    Dim stradd1 As String = clsCommon.myCstr(grow.Cells(3).Value)
                    If stradd1.Length > 50 Then
                        Throw New Exception("Check the length of address1")

                    End If
                    Dim stradd2 As String = clsCommon.myCstr(grow.Cells(4).Value)
                    If stradd2.Length > 50 Then
                        Throw New Exception("Check the length of address2")

                    End If
                    Dim strpin As String = clsCommon.myCstr(grow.Cells(5).Value)
                    If strpin.Length > 10 Then
                        Throw New Exception("Check the length of pincode")

                    End If
                    Dim strphone As String = clsCommon.myCstr(grow.Cells(6).Value)
                    If strphone.Length > 20 Then
                        Throw New Exception("Check the length of phone")

                    End If
                    'Dim strbdate As String = CDate(grow.Cells(7).Value).ToString("MM/dd/yyyy")
                    'Dim bdate As String = grow.Cells(7).Value.ToString()
                    Dim bdate1 As String = clsCommon.myCstr(grow.Cells(7).Value)
                    If String.IsNullOrEmpty(bdate1) Then
                        Throw New Exception("Birth Date has some incorrect values")

                    Else
                        bdate = CDate(bdate1)
                    End If
                    'Dim bdate As String = globalFunc.getDate(bdate1)
                    Dim strcash As String = clsCommon.myCstr(grow.Cells(8).Value)
                    If strcash.Length > 50 Then
                        Throw New Exception("Check the length of Cash ")
                    ElseIf Not IsNumeric(strcash) Then
                        Throw New Exception("Please Insert Numeric Value In 'Cash Sh/Ex' Against Employee '" + strcode + "'")
                    End If

                    Dim strcard As String = clsCommon.myCstr(grow.Cells(9).Value)
                    If strcard.Length > 50 Then
                        Throw New Exception("Check the length of Card ")

                    End If
                    'Dim strjdate As String = CDate(grow.Cells(10).Value).ToString("MM/dd/yyyy")
                    'Dim jdate As String = grow.Cells(10).Value.ToString()
                    Dim jdate1 As String = clsCommon.myCstr(grow.Cells(10).Value)
                    If String.IsNullOrEmpty(jdate1) Then
                        Throw New Exception("Joining date Has Some incorrect values")

                    Else
                        jdate = CDate(jdate1)
                    End If
                    'Dim jdate As String = globalFunc.getDate(jdate1)
                    Dim stretype As String = clsCommon.myCstr(grow.Cells(11).Value)

                    If stretype.Length > 20 Then
                        Throw New Exception("Check the length of Employee Type ")

                    End If

                    'Dim strexdate As String = CDate(grow.Cells(12).Value).ToString("MM/dd/yyyy")
                    'Dim exdate As String = grow.Cells(12).Value.ToString()
                    Dim exdate1 As String = clsCommon.myCstr(grow.Cells(12).Value)
                    'Dim exdate As String = globalFunc.getDate(exdate1)
                    If String.IsNullOrEmpty(exdate1) Then
                        Throw New Exception("Ex date has some incorrect value")

                    Else
                        exdate = CDate(exdate1)
                    End If
                    Dim strstatus As String = clsCommon.myCstr(grow.Cells(13).Value)
                    If strstatus.Length > 50 Then
                        Throw New Exception("Check the length of Employee Status ")

                    End If
                    Dim releavingdate1 As String = clsCommon.myCstr(grow.Cells(14).Value)
                    If String.IsNullOrEmpty(releavingdate1) Then
                        Throw New Exception("Releaving date has some incorrect value")

                    Else
                        releavingdate = CDate(releavingdate1)
                    End If

                    Dim strpay As String = clsCommon.myCstr(grow.Cells(15).Value)
                    If strpay.Length > 50 Then
                        Throw New Exception("Check the length of Payroll ")

                    End If
                    Dim strempty As String = clsCommon.myCstr(grow.Cells(16).Value)
                    If strempty.Length > 50 Then
                        Throw New Exception("Check the length of Employee Empty Ex ")
                    ElseIf Not IsNumeric(strempty) Then
                        Throw New Exception("Please Insert Numeric Value In Empty Sh/Ex Against Employee '" + strcode + "'")
                    End If
                    If (String.IsNullOrEmpty(strcode)) Or strcode.Length > 12 Then
                        Throw New Exception("Employee Code can not be blank or incorrect")

                    End If
                    If String.IsNullOrEmpty(strname) Or strname.Length > 50 Then
                        Throw New Exception(" Employee Name can not be blank or incorrect")

                    End If

                    Dim strGLAccount As String = ""
                    If clsCommon.myLen(grow.Cells("GL Account").Value) > 0 Then
                        strGLAccount = connectSql.RunScalar(trans, "SELECT Account_Code FROM TSPL_GL_ACCOUNTS WHERE Account_Code='" + clsCommon.myCstr(grow.Cells("GL Account").Value) + "'")
                        If clsCommon.myLen(strGLAccount) <= 0 Then
                            Throw New Exception(" The GL Account Against Employee '" + strcode + "' Does Not Exist, Please Insert a Valid Account Or Leave blank ")
                        End If
                    End If
                    Dim strEmail As String = clsCommon.myCstr(grow.Cells(16).Value)
                    If strEmail.Length > 100 Then
                        Throw New Exception("Check the length of Email ID ")

                    End If


                    Dim sql1 As String = "select count(*) from tspl_Employee_master where emp_code='" + strcode + "'"
                    Dim i As Integer = CInt(connectSql.RunScalar(trans, sql1))
                    If (i = 0) Then

                        connectSql.RunSpTransaction(trans, "sp_EmployeeMaster_insert", New SqlParameter("@empcode", strcode), New SqlParameter("@empname", strname), New SqlParameter("@designation", strdesig), New SqlParameter("@add1", stradd1), New SqlParameter("@add2", stradd2), New SqlParameter("@pin", strpin), New SqlParameter("@phone", strphone), New SqlParameter("@dob", bdate), New SqlParameter("@cash", strcash), New SqlParameter("@cardno", strcard), New SqlParameter("@joindate", jdate), New SqlParameter("@emptype", stretype), New SqlParameter("@exdate", exdate), New SqlParameter("@empstatus", strstatus), New SqlParameter("@rel_date", releavingdate), New SqlParameter("@payroll", strpay), New SqlParameter("@emptyex", strempty), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode), New SqlParameter("@GLAccount", strGLAccount), New SqlParameter("@EmailId", strEmail))
                        If stretype = "Salesman" Then
                            connectSql.RunSpTransaction(trans, "sp_TSPL_LOCATION_MASTER_insert", New SqlParameter("@LocCode", strcode), New SqlParameter("@LocDesc", strname), New SqlParameter("@Add1", stradd1), New SqlParameter("@Add2", stradd2), New SqlParameter("@Add3", add3), New SqlParameter("@Add4", add4), New SqlParameter("@citycode", city), New SqlParameter("@state", state), New SqlParameter("@PinCode", strpin), New SqlParameter("@country", country), New SqlParameter("@Telephone", strphone), New SqlParameter("@email", email), New SqlParameter("@Location", strlogical), New SqlParameter("@Status", strstatus), New SqlParameter("@statusdate", statusdate), New SqlParameter("@Excisable", strexcise), New SqlParameter("@Loc_Segment_Code", Loc_Segment_Code), New SqlParameter("@Type", Type), New SqlParameter("@Purchase_Tax_Group", Purchase_Tax_Group), New SqlParameter("@Sales_Tax_Group", Sales_Tax_Group), New SqlParameter("@Ecc_Number", Ecc_Number), New SqlParameter("@Registration_Number", Registration_Number), New SqlParameter("@Commissionerate", Commissionerate), New SqlParameter("@Range_Code", Range_Code), New SqlParameter("@Range_Name", Range_Name), New SqlParameter("@Range_Address", Range_Address), New SqlParameter("@Division_Code", Division_Code), New SqlParameter("@Division_Name", Division_Name), New SqlParameter("@Division_Address", Division_Address), New SqlParameter("@TinNo", TinNo), New SqlParameter("@TanNo", TanNo), New SqlParameter("@TcanNo", TcanNo), New SqlParameter("@ServiceTaxRegNo", ServiceTaxRegNo), New SqlParameter("@DutyPaid", duty), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode), New SqlParameter("@purchaseTaxGroupIS", pur), New SqlParameter("@SalesTaxGroupIS", sal), New SqlParameter("@Stock_Transfer_Filled_Ac", ""), New SqlParameter("@Stock_Transfer_Empty_Ac", ""))
                        End If
                    Else
                        connectSql.RunSpTransaction(trans, "sp_EmployeeMaster_update", New SqlParameter("@empcode", strcode), New SqlParameter("@empname", strname), New SqlParameter("@designation", strdesig), New SqlParameter("@add1", stradd1), New SqlParameter("@add2", stradd2), New SqlParameter("@pin", strpin), New SqlParameter("@phone", strphone), New SqlParameter("@dob", bdate), New SqlParameter("@cash", strcash), New SqlParameter("@cardno", strcard), New SqlParameter("@joindate", jdate), New SqlParameter("@emptype", stretype), New SqlParameter("@exdate", exdate), New SqlParameter("@empstatus", strstatus), New SqlParameter("@rel_date", releavingdate), New SqlParameter("@payroll", strpay), New SqlParameter("@emptyex", strempty), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode), New SqlParameter("@GLAccount", strGLAccount), New SqlParameter("@EmailId", strEmail))
                        If stretype = "Salesman" Then
                            Loc_Segment_Code = clsCommon.myCstr(connectSql.RunScalar(trans, "Select Loc_Segment_Code from TSPL_LOCATION_MASTER Where Location_Code='" + strcode + "'"))
                            connectSql.RunSpTransaction(trans, "sp_TSPL_LOCATION_MASTER_update", New SqlParameter("@LocCode", strcode), New SqlParameter("@LocDesc", strname), New SqlParameter("@Add1", stradd1), New SqlParameter("@Add2", stradd2), New SqlParameter("@Add3", add3), New SqlParameter("@Add4", add4), New SqlParameter("@citycode", city), New SqlParameter("@state", state), New SqlParameter("@PinCode", strpin), New SqlParameter("@country", country), New SqlParameter("@Telephone", strphone), New SqlParameter("@email", email), New SqlParameter("@Location", strlogical), New SqlParameter("@Status", strstatus), New SqlParameter("@statusdate", statusdate), New SqlParameter("@Excisable", strexcise), New SqlParameter("@Loc_Segment_Code", Loc_Segment_Code), New SqlParameter("@Type", Type), New SqlParameter("@Purchase_Tax_Group", Purchase_Tax_Group), New SqlParameter("@Sales_Tax_Group", Sales_Tax_Group), New SqlParameter("@Ecc_Number", Ecc_Number), New SqlParameter("@Registration_Number", Registration_Number), New SqlParameter("@Commissionerate", Commissionerate), New SqlParameter("@Range_Code", Range_Code), New SqlParameter("@Range_Name", Range_Name), New SqlParameter("@Range_Address", Range_Address), New SqlParameter("@Division_Code", Division_Code), New SqlParameter("@Division_Name", Division_Name), New SqlParameter("@Division_Address", Division_Address), New SqlParameter("@TinNo", TinNo), New SqlParameter("@TanNo", TanNo), New SqlParameter("@TcanNo", TcanNo), New SqlParameter("@ServiceTaxRegNo", ServiceTaxRegNo), New SqlParameter("@DutyPaid", duty), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode), New SqlParameter("@purchaseTaxGroupIS", pur), New SqlParameter("@SalesTaxGroupIS", sal), New SqlParameter("@Stock_Transfer_Filled_Ac", ""), New SqlParameter("@Stock_Transfer_Empty_Ac", ""), New SqlParameter("@CST_NO", ""), New SqlParameter("@PHONE1", ""), New SqlParameter("@PHONE2", ""))
                        End If
                    End If
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)

            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub btnAsmtype_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAsmtype.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Emp Code", "Emp type", "city code", "city name", "state code", "state name", "region code", "region name") Then

            Dim empcode As String = ""
            Dim emptype As String = ""
            Dim regioncode As String = ""
            Dim regionname As String = ""
            Dim statecode As String = ""
            Dim statename As String = ""
            Dim citycode As String = ""
            Dim cityname As String = ""
            Dim obj As clsEmpASMZMDetails
            Dim arr As New List(Of clsEmpASMZMDetails)

            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try

                Dim LineNo As String = "0"
                clsCommon.ProgressBarShow()

                For Each grow As GridViewRowInfo In gv.Rows

                    LineNo = clsCommon.myCstr(grow.Index + 2)
                    obj = New clsEmpASMZMDetails()

                    Dim strempcode As String = clsCommon.myCstr(grow.Cells("Emp Code").Value)
                    obj.empcode = strempcode

                    If clsCommon.myLen(strempcode) <= 0 Then
                        Throw New Exception("Please Fill Employee Code")
                    End If

                    Dim stremptype As String = clsCommon.myCstr(grow.Cells("Emp type").Value)
                    If clsCommon.CompairString(stremptype, "ASM") = CompairStringResult.Equal Then

                        obj.empltype = stremptype
                    Else
                        Throw New Exception("Line " + LineNo + " : This  '" + stremptype + "'  need to be set as ASM")
                    End If

                    Dim emp_check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_Employee_MASTER where Emp_Code='" + strempcode + "' ", trans))
                    If emp_check <= 0 Then
                        Throw New Exception("Line " + LineNo + " : This  '" + strempcode + "'  emp  code does not exist")
                    End If



                    Dim query As String = "update TSPL_Employee_MASTER set EMP_type ='ASM' where emp_Code ='" + strempcode + "'"
                    clsDBFuncationality.ExecuteNonQuery(query, trans)


                    Dim strstatecode As String = clsCommon.myCstr(grow.Cells("state code").Value)

                    If strstatecode.Length > 30 Then
                        Throw New Exception("Check state code length")
                    End If
                    obj.statecode = strstatecode


                    Dim check_state As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_STATE_MASTER where  state_code='" + strstatecode + "'", trans))
                    If check_state <= 0 Then
                        Throw New Exception("Line " + LineNo + " : This  '" + strstatecode + "'  state code does not exist")
                    End If

                    Dim strstatename As String = clsCommon.myCstr(grow.Cells("state name").Value)
                    If clsCommon.myLen(strstatename) > 100 Then
                        Throw New Exception("State Name Length Should Be Max. 100 Characters")
                    End If
                    obj.statename = strstatename

                    Dim strregioncode As String = clsCommon.myCstr(grow.Cells("region code").Value)

                    If clsCommon.myLen(strregioncode) > 30 Then
                        Throw New Exception("Region Code Length Should Be Max. 30 Characters")
                    End If

                    Dim check_region As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_REGION_MASTER  where  REGION_CODE='" + strregioncode + "'", trans))
                    If check_region <= 0 Then
                        Throw New Exception("Line " + LineNo + " : This  '" + strregioncode + "'region code does not exist")
                    End If
                    Dim strregionname As String = clsCommon.myCstr(grow.Cells("region name").Value)
                    If clsCommon.myLen(strregionname) > 100 Then
                        Throw New Exception("Region Name Length Should Be Max. 100 Characters")
                    End If
                    obj.regionname = strregionname
                    obj.regioncode = strregioncode

                    Dim strcitycode As String = clsCommon.myCstr(grow.Cells("city code").Value)
                    If clsCommon.myLen(strcitycode) > 30 Then
                        Throw New Exception("City Code Length Should Be Max. 30 Characters")
                    End If
                    Dim check_city As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_CITY_MASTER  where  City_Code='" + strcitycode + "'", trans))
                    If check_city <= 0 Then
                        Throw New Exception("Line " + LineNo + " : This  '" + strcitycode + "' city code does not exist")
                    End If
                    obj.citycode = strcitycode
                    Dim strcityname As String = clsCommon.myCstr(grow.Cells("city name").Value)
                    obj.cityname = strcityname

                    If clsCommon.myLen(strcityname) > 100 Then
                        Throw New Exception("City Name Length Should Be Max. 100 Characters")
                    End If

                    arr.Add(obj)


                    Dim qry As String = "delete from TSPL_emptype_ASMZM_details where emp_code='" + obj.empcode + "' and emp_type='" + obj.empltype + "' and state_code='" + obj.statecode + "' and region_code='" + obj.regioncode + "' and city_code='" + obj.citycode + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    Dim coll As New Hashtable()
                    Dim isSaved As Boolean = True
                    clsCommon.AddColumnsForChange(coll, "state_code", clsCommon.myCstr(obj.statecode))
                    clsCommon.AddColumnsForChange(coll, "state_name", clsCommon.myCstr(obj.statename))
                    clsCommon.AddColumnsForChange(coll, "region_code", clsCommon.myCstr(obj.regioncode))
                    clsCommon.AddColumnsForChange(coll, "region_name", clsCommon.myCstr(obj.regionname))
                    clsCommon.AddColumnsForChange(coll, "city_code", clsCommon.myCstr(obj.citycode))
                    clsCommon.AddColumnsForChange(coll, "city_name", clsCommon.myCstr(obj.cityname))
                    clsCommon.AddColumnsForChange(coll, "emp_code", obj.empcode)
                    clsCommon.AddColumnsForChange(coll, "emp_type", obj.empltype)

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_emptype_ASMZM_details", OMInsertOrUpdate.Insert, "", trans)
                Next

                'obj.SaveData(obj.empcode, arr, Nothing)
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                trans.Commit()
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                trans.Rollback()
                myMessages.myExceptions(ex)
            Finally
                clsCommon.ProgressBarHide()
                'trans.Rollback()
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub btnExpEmpdetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExpEmpdetails.Click
        sql = "select emp_code AS [Employee Code],emp_name As [Employee Name],designation AS [Designation],add1 as [Address1],add2 as [Address2],pin_code as [Pin Code],phone as [Phone],birth_date as [Birth Date],cash as [Cash Sh/Ex],card_no As [Card Number],joining_date As [Joining Date],emp_type as [Employee Type],exdate as [Ex Date],emp_status as [Employee Status],rel_date AS [Releaving Date],payroll_code AS [Payroll Code],empty_ex as [Empty Sh/Ex], GL_Account as [GL Account],EMail_ID as [Email ID] from tspl_employee_Master"
        ListImpExpColumnsMandatory = New List(Of String)({"Birth Date", "Joining Date", "Ex Date", "Releaving Date", "Employee Code", "Employee Name"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"Employee Code"})
        transportSql.ExporttoExcel(sql, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
    End Sub

    Private Sub btnExpASMdetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExpASMdetails.Click
        sql = "select TSPL_emptype_ASMZM_details.emp_code as [Emp Code],TSPL_emptype_ASMZM_details.emp_type as [Emp type],TSPL_emptype_ASMZM_details.city_code as [city code],tspl_city_master.city_name as [city name],TSPL_emptype_ASMZM_details.state_code as [state code] ,tspl_state_master.state_name  as [state name],TSPL_emptype_ASMZM_details.region_code  as [region code] ,tspl_region_master.region_name as [region name] from TSPL_emptype_ASMZM_details left outer join tspl_city_master on tspl_city_master.city_code=TSPL_emptype_ASMZM_details.city_code left outer join tspl_state_master on tspl_state_master.state_code=TSPL_emptype_ASMZM_details.state_code left outer join tspl_region_master on tspl_region_master.region_code=TSPL_emptype_ASMZM_details.region_code "
        ListImpExpColumnsMandatory = New List(Of String)({"Emp Code"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"Emp Code"})
        transportSql.ExporttoExcel(sql, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID + "ASMdetails")

    End Sub

    
    Private Sub dgstate_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles dgstate.CurrentColumnChanged
        If dgstate.RowCount > 0 Then
            Dim intCurrRow As Integer = dgstate.CurrentRow.Index
            If intCurrRow = dgstate.Rows.Count - 1 Then
                dgstate.Rows.AddNew()
                dgstate.CurrentRow = dgstate.Rows(intCurrRow)
            End If
        End If
    End Sub
End Class
