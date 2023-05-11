'Developed By -BibhuPrasad Parida
'Database - TSPLERP
'Table - TSPL_EXCISABLE_PROFILE
'Start Date -27-06-2011 3.30PM
'End Date -
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports common

Public Class FrmExcisableLocationDetails

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
    Dim dt As DataTable

    Dim objstr As String = "Tecxpert Software Pvt Ltd."
    ' Dim dt1 As Date = Date.Today
    ' Dim dt As Date = connectSql.serverDate()
    ' Dim realDate As Date = Date.ParseExact(dt, "mm/dd/yyyy", culture)
#End Region


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If fndLocation.Value = "" Then
            myMessages.blankValue("Location")
        ElseIf txtEccNumber.Text = "" Then
            myMessages.blankValue("ECC Number")
        ElseIf btnSave.Text = "&Save" Then
            funInsert()
        Else
            funUpdate()
        End If

    

    End Sub
    Private Sub funInsert()
        Dim strexcisable As String
        If chkExcisable.Checked = True Then
            strexcisable = "Y"
        Else
            strexcisable = "N"
        End If
        Try

            connectSql.RunSp("tspl_TSPL_EXCISABLE_PROFILE_insert", New SqlParameter("@LocationCode", fndLocation.Value), New SqlParameter("@LocationDesc", txtLocationDesc.Text), New SqlParameter("@GlAccount", fndGLAccount.Value), New SqlParameter("@Excisable", strexcisable), New SqlParameter("@LocationType", ddlLocationType.SelectedItem.Text), New SqlParameter("@PurchaseTaxGroup", fndPurchaseTaxGrp.Value), New SqlParameter("@SalesTaxGroup", fndSaleTaxGrp.Value), New SqlParameter("@EccNumber", txtEccNumber.Text), New SqlParameter("@RegistrationNumber", txtRegistration.Text), New SqlParameter("@Commissionerate", txtCommissionerate.Text), New SqlParameter("@CERCode", txtRangeCode.Text), New SqlParameter("@CERName", txtRangeName.Text), New SqlParameter("@CERAddress", txtRangeAddress.Text), New SqlParameter("@CEDCode", txtDivisionCode.Text), New SqlParameter("@CEDName", txtDivisionName.Text), New SqlParameter("@CEDAddress", txtDivisionAddress.Text), New SqlParameter("@CreatedBy", userCode), New SqlParameter("CreatedDate", connectSql.serverDate()), New SqlParameter("@ModifiedBy", userCode), New SqlParameter("@ModifiedDate", connectSql.serverDate()), New SqlParameter("@CompanyCode", companyCode))
            myMessages.insert()
            btnSave.Text = "&Update"
            btnDelete.Enabled = True

        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Private Sub funUpdate()
        Dim strexcisable As String
        If chkExcisable.Checked = True Then
            strexcisable = "Y"
        Else
            strexcisable = "N"
        End If
        Try

            connectSql.RunSp("tspl_TSPL_EXCISABLE_PROFILE_update", New SqlParameter("@LocationCode", fndLocation.Value), New SqlParameter("@LocationDesc", txtLocationDesc.Text), New SqlParameter("@GlAccount", fndGLAccount.Value), New SqlParameter("@Excisable", strexcisable), New SqlParameter("@LocationType", ddlLocationType.SelectedItem.Text), New SqlParameter("@PurchaseTaxGroup", fndPurchaseTaxGrp.Value), New SqlParameter("@SalesTaxGroup", fndSaleTaxGrp.Value), New SqlParameter("@EccNumber", txtEccNumber.Text), New SqlParameter("@RegistrationNumber", txtRegistration.Text), New SqlParameter("@Commissionerate", txtCommissionerate.Text), New SqlParameter("@CERCode", txtRangeCode.Text), New SqlParameter("@CERName", txtRangeName.Text), New SqlParameter("@CERAddress", txtRangeAddress.Text), New SqlParameter("@CEDCode", txtDivisionCode.Text), New SqlParameter("@CEDName", txtDivisionName.Text), New SqlParameter("@CEDAddress", txtDivisionAddress.Text), New SqlParameter("@CreatedBy", userCode), New SqlParameter("CreatedDate", connectSql.serverDate()), New SqlParameter("@ModifiedBy", userCode), New SqlParameter("@ModifiedDate", connectSql.serverDate()), New SqlParameter("@CompanyCode", companyCode))
            myMessages.insert()



        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub FrmExcisableLocationDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'AddHandler fndLocation.txtValue.TextChanged, AddressOf fndLocation_TextChanged

        'AddHandler fndLocation.txtValue.KeyPress, AddressOf fndLocation_keyPress
        'AddHandler fndGlAccount.txtValue.KeyPress, AddressOf fndGlAccount_keyPress
        'AddHandler fndPurchaseTaxGroup.txtValue.KeyPress, AddressOf fndPurchaseTaxGroup_keyPress
        'AddHandler fndSalesTaxGroup.txtValue.KeyPress, AddressOf fndSalesTaxGroup_keyPress
        btnDelete.Enabled = False
    End Sub

    Private Sub fndLocation_keyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        'fndLocation.txtValue.CharacterCasing = CharacterCasing.Upper
        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub
    Private Sub fndGlAccount_keyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        'fndGlAccount.txtValue.CharacterCasing = CharacterCasing.Upper
        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub
    Private Sub fndPurchaseTaxGroup_keyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        'fndPurchaseTaxGroup.txtValue.CharacterCasing = CharacterCasing.Upper
        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub
    Private Sub fndSalesTaxGroup_keyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        'fndSalesTaxGroup.txtValue.CharacterCasing = CharacterCasing.Upper
        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub
    Private Sub fndLocation_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim lccode As String
        lccode = clsDBFuncationality.getSingleValue("select Location_Code from TSPL_EXCISABLE_PROFILE where Location_Code='" + fndLocation.Value + "'")
        Dim s As String
        If clsCommon.myLen(lccode) > 0 Then
            s = lccode
        End If

        'If s <> fndLocation.txtValue.Text Then
        '    txtLocationDesc.Text = ""
        '    'fndPurchaseTaxGroup.txtValue.Text = ""
        '    'fndSalesTaxGroup.txtValue.Text = ""
        '    'fndGlAccount.txtValue.Text = ""
        '    txtEccNumber.Text = ""
        '    txtRegistration.Text = ""
        '    txtCommissionerate.Text = ""
        '    txtRangeCode.Text = ""
        '    txtRangeName.Text = ""
        '    txtRangeAddress.Text = ""
        '    txtDivisionCode.Text = ""
        '    txtDivisionName.Text = ""
        '    txtDivisionAddress.Text = ""
        '    ddlLocationType.Text = "Depot"
        '    btnDelete.Enabled = False
        '    btnSave.Text = "&Save"
        '    chkExcisable.Checked = False

        'Else
        '    funFill()
        'End If

    End Sub

    Private Sub fndGlAccount_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndGlAccount.ConnectionString = connectSql.SqlCon()
        'fndGlAccount.Query = "select Account_Code as [Account],Description from TSPL_GL_ACCOUNTS"
        'fndGlAccount.ValueToSelect = "Account"
        'fndGlAccount.ValueToSelect1 = "Description"
        'fndGlAccount.Caption = "Account Details"

    End Sub

    Private Sub fndPurchaseTaxGroup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndPurchaseTaxGroup.ConnectionString = connectSql.SqlCon()
        ''  fndPurchaseTaxGroup.Query = "  SELECT TAX_Group_Code as 'Tax Group',Tax_Group_Desc as Description From TSPL_TAX_GROUP_MASTER"
        'fndPurchaseTaxGroup.Query = clsERPFuncationality.UserAvailableTaxGroup
        'fndPurchaseTaxGroup.ValueToSelect = "Tax Group"
        'fndPurchaseTaxGroup.ValueToSelect1 = "Description"
        'fndPurchaseTaxGroup.Caption = "Tax Group Details"
    End Sub

    Private Sub fndSalesTaxGroup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndSalesTaxGroup.ConnectionString = connectSql.SqlCon()
        ''  fndSalesTaxGroup.Query = "  SELECT TAX_Group_Code as 'Tax Group',Tax_Group_Desc as Description From TSPL_TAX_GROUP_MASTER"
        'fndSalesTaxGroup.Query = clsERPFuncationality.UserAvailableTaxGroup
        'fndSalesTaxGroup.ValueToSelect = "Tax Group"
        'fndSalesTaxGroup.ValueToSelect1 = "Description"
        'fndSalesTaxGroup.Caption = "Tax Group Details"
    End Sub
    Private Sub funFill()
        dt = clsDBFuncationality.GetDataTable("select Location_Desc ,GL_Account ,Excisable ,Location_Type ,Purchase_Tax_Group ,Sales_Tax_Group ,Ecc_Number ,Registration_Number ,Commissionerate ,CER_Code ,CER_Name,CER_Address ,CED_Code ,CED_Name ,CED_Address  from TSPL_EXCISABLE_PROFILE where Location_Code='" + fndLocation.Value + "'")
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                txtLocationDesc.Text = dr(0).ToString()
                'fndGlAccount.txtValue.Text = dr(1).ToString()
                If dr(2).ToString() = "Y" Then
                    chkExcisable.Checked = True
                Else
                    chkExcisable.Checked = False
                End If
                ddlLocationType.Text = dr(3).ToString()
                'fndPurchaseTaxGroup.txtValue.Text = dr(4).ToString()
                'fndSalesTaxGroup.txtValue.Text = dr(5).ToString()
                txtEccNumber.Text = dr(6).ToString()
                txtRegistration.Text = dr(7).ToString()
                txtCommissionerate.Text = dr(8).ToString()
                txtRangeCode.Text = dr(9).ToString()
                txtRangeName.Text = dr(10).ToString()
                txtRangeAddress.Text = dr(11).ToString()
                txtDivisionCode.Text = dr(12).ToString()
                txtDivisionName.Text = dr(13).ToString()
                txtDivisionAddress.Text = dr(14).ToString()
            Next
        End If
        btnSave.Text = "&Update"
    End Sub


    Private Sub txtEccNumber_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtEccNumber.KeyPress
        If IsNumeric(e.KeyChar) = True Then
            e.Handled = False
        ElseIf Microsoft.VisualBasic.Asc(e.KeyChar) = 8 Then
        Else
            e.Handled = True

        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If myMessages.deleteConfirm() Then
            funDelete()
        End If

    End Sub
    Private Sub funDelete()
        Try

            connectSql.RunSp("tspl_TSPL_EXCISABLE_PROFILE_Delete", New SqlParameter("@LocationCode", fndLocation.Value))
            myMessages.delete()
            btnSave.Text = "&Save"
            btnDelete.Enabled = False

        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub fndLocation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndLocation.ConnectionString = connectSql.SqlCon()
        'fndLocation.Query = "select Location_Code as 'Location',Location_Desc as 'Description' from TSPL_EXCISABLE_PROFILE"
        'fndLocation.ValueToSelect = "Location"
        'fndLocation.ValueToSelect1 = "Description"
        'fndLocation.Caption = "Location Details"
    End Sub

    Private Sub fndPurchaseTaxGrp__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndPurchaseTaxGrp._MYValidating
        Dim qry As String = "select DISTINCT  M.TAX_Group_Code as 'Code',(CASE WHEN M.Tax_Group_Type='S' THEN 'Sales' ELSE 'Purchase' END) as 'Transaction Type',M.Tax_Group_Desc as Description from TSPL_TAX_GROUP_MASTER M JOIN TSPL_TAX_GROUP_DETAILS D ON M.Tax_Group_Code = D.Tax_Group_Code join TSPL_TAX_MASTER TM ON D.Tax_Code = TM.Tax_Code"
        fndPurchaseTaxGrp.Value = clsCommon.ShowSelectForm("fndPurchaseTaxGrp", qry, "Code", "", fndPurchaseTaxGrp.Value, "Code", isButtonClicked)
    End Sub

    Private Sub fndSaleTaxGrp__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndSaleTaxGrp._MYValidating
        Dim qry As String = "select DISTINCT  M.TAX_Group_Code as 'Code',(CASE WHEN M.Tax_Group_Type='S' THEN 'Sales' ELSE 'Purchase' END) as 'Transaction Type',M.Tax_Group_Desc as Description from TSPL_TAX_GROUP_MASTER M JOIN TSPL_TAX_GROUP_DETAILS D ON M.Tax_Group_Code = D.Tax_Group_Code join TSPL_TAX_MASTER TM ON D.Tax_Code = TM.Tax_Code"
        fndSaleTaxGrp.Value = clsCommon.ShowSelectForm("fndSaleTaxGrp", qry, "Code", "", fndSaleTaxGrp.Value, "Code", isButtonClicked)
    End Sub

    Private Sub fndGLAccount__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndGLAccount._MYValidating
        Dim qry As String = "select Account_Code as [Account],Description from TSPL_GL_ACCOUNTS "
        fndGLAccount.Value = clsCommon.ShowSelectForm("fndGLAccount", qry, "Account", "", fndGLAccount.Value, "Account", isButtonClicked)
    End Sub

    Private Sub fndLocation__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndLocation._MYNavigator
        Dim qry As String = "select Location_Code  from TSPL_EXCISABLE_PROFILE Where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_EXCISABLE_PROFILE.Location_Code=(select MIN(Location_Code) from TSPL_EXCISABLE_PROFILE)"
            Case NavigatorType.Last
                qry += " and TSPL_EXCISABLE_PROFILE.Location_Code=(select MAX(Location_Code) from TSPL_EXCISABLE_PROFILE)"
            Case NavigatorType.Next
                qry += " and TSPL_EXCISABLE_PROFILE.Location_Code=(select Min(Location_Code) from TSPL_EXCISABLE_PROFILE where Location_Code > '" + fndLocation.Value + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_EXCISABLE_PROFILE.Location_Code=(select Max(Location_Code) from TSPL_EXCISABLE_PROFILE where Location_Code < '" + fndLocation.Value + "')"
            Case NavigatorType.Current
                qry += " and TSPL_EXCISABLE_PROFILE.Location_Code='" + fndLocation.Value + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            fndLocation.Value = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            funFill()
        End If
    End Sub

    Private Sub fndLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndLocation._MYValidating
        Dim qry As String = "select Location_Code as 'Location',Location_Desc as 'Description' from TSPL_EXCISABLE_PROFILE"
        fndLocation.Value = clsCommon.ShowSelectForm("fndLocation", qry, "Location", "", fndLocation.Value, "Location", isButtonClicked)
        funFill()
    End Sub
End Class
