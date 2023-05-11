'Developed By -BibhuPrasad Parida
'Database - TSPLERP
'Table - tspl_customer_group_master,TSPL_CUSTOMER_SALESMAN_DETAIL
'Start Date -
'End Date -

'19/10/2011'''''[Pankaj Kumar Chaudhary]''''added new Field(Shelf LIfe) with the Functionalities(Fill, Insert, Update, Delete)
'--21/12/2012--Updation By --Pankaj Kumar--- Applied Validations
'19/02/2013 by vipin disabling the salemancode,saleperson name,percentage fields
'-03/04/2013:5:41PM--Updation By--Pankaj Kumar--Transaction Problem while importing-------------------Ashok
'--preeti gupta-ticket no-[BM00000003128]
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports System.Windows.Forms
Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System
Imports System.Globalization
Imports System.Drawing
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Themes
Imports Telerik.WinControls.UI.Export
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Text.RegularExpressions
Imports common


Public Class frmCustomerGroup
    Inherits FrmMainTranScreen

    Const colSelect As String = "SELECT"
    Const colCompCode As String = "COMPCODE"
    Const colCompName As String = "COMPNAME"
    Const colDataBaseName As String = "DATABASE"

    Dim userCode, companyCode As String

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing

#Region "Constructor"
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
#End Region
#Region "Variables"
    Dim sql As String
    Dim ds As DataSet
    Dim dr As SqlDataReader
    Dim tableName As String = "TSPL_TAX_MASTER"
    Dim tableCode As String = "Tax_Code"
    Dim codePrefix As String = "TAX"
    Dim dt As Date = Date.Today

    Dim i As New Decimal
    Dim j As New Decimal
#End Region
    Dim objfu
#Region "Page Load"
    Private Sub Customer_Group_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TxtShfLife.Text = 0
        SetUserMgmtNew()
        SetDataBaseGrid()
        RadPageView1.SelectedPage = RadPageViewPage1
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New Trasnaction")
        fndCustomerGroupCode.Focus()
        btnDelete.Enabled = False
        ValidateLength()
        gvDB.AllowDeleteRow = False
        ''For Custom Fields
        RadPageView1.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.Report_ID = MyBase.Form_ID
            UcCustomFields1.LoadCustomControls()
        End If
        ''End of For Custom Fields
    End Sub
    Private Sub ValidateLength()
        txtCustomerGroupDesc.MaxLength = 50
    End Sub
    
    Private Sub SetUserMgmtNew()
        ''richa 25/07/2014 Against Ticket No.BM00000003129
        'MyBase.SetUserMgmt(clsUserMgtCode.CustomerGroup)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 02/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnSave.Visible = True Then
            MenuImport1.Enabled = True
            MenuExport.Enabled = True
        Else
            MenuImport1.Enabled = False
            MenuExport.Enabled = False
        End If
        '--------------------------------------------------
        'btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
#End Region
#Region "KeyPress Event"

    Private Sub txtPercentage_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If txtPercentage.Text <> "" Then
            If (Microsoft.VisualBasic.Asc(e.KeyChar) >= 48 And Microsoft.VisualBasic.Asc(e.KeyChar) <= 57) Then
                e.Handled = False
            ElseIf Microsoft.VisualBasic.Asc(e.KeyChar) = 8 Then
                e.Handled = False
            Else
                e.Handled = True
            End If
        Else
        End If

    End Sub

    Private Sub CustomerGroupCode_keypress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub
    Private Sub AccountSet_keypres(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub
    Private Sub TermsCode_keypress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub
    Private Sub TaxGroup_keypress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub
    Private Sub Salespersoncode_keypress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtPercentage_KeyPress_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPercentage.KeyPress

        If (Microsoft.VisualBasic.Asc(e.KeyChar) >= 48 And Microsoft.VisualBasic.Asc(e.KeyChar) <= 57) Then
            e.Handled = False
        ElseIf Microsoft.VisualBasic.Asc(e.KeyChar) = 8 Then
            e.Handled = False
        Else
            e.Handled = True
        End If

    End Sub
#End Region
#Region "TextChanged Event"
    Private Sub Salespersoncode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtSalespersonname.Text = fndSalespersoncode.Tag
    End Sub
    Private Sub CustomerGroupCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        LoadData()
    End Sub
    Sub LoadData()
        Dim s As String
        s = clsDBFuncationality.getSingleValue("select cust_group_code from tspl_Customer_Group_Master where cust_group_code='" + fndCustomerGroupCode.Value + "'")

        'While dr.Read()
        '    s = dr(0).ToString()

        'End While
        'dr.Close()
        If s <> fndCustomerGroupCode.Value Then
            btnSave.Text = "Save"
            btnDelete.Enabled = False
        Else
            funFill()
        End If

    End Sub
    Private Sub AccountSet_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' txtAccountSetDesc.Text = fndAccountSet.txtValue.Tag
        txtAccountSetDesc.Text = connectSql.RunScalar("select Cust_Acct_Desc  from TSPL_CUSTOMER_ACCOUNT_SET where Cust_Account ='" + fndAccountSet.Value + "'")
    End Sub

    Private Sub TermsCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' txtTermsCode.Text = fndTermsCode.txtValue.Tag
        txtTermsCode.Text = connectSql.RunScalar("select terms_desc from TSPL_TERMS_MASTER where Terms_Code ='" + fndTermsCode.Value + "'")
    End Sub
    Private Sub TaxGroup_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '  txtTaxGroup.Text = fndTaxGroup.txtValue.Tag
        txtTaxGroup.Text = connectSql.RunScalar("select Tax_Group_Desc   from tspl_tax_group_master where Tax_Group_Code  ='" + fndTaxGroup.Value + "'")
    End Sub
#End Region
#Region "Methods"
    'insert into  master table  and details table
    Private Sub funInsert()
        Dim IsCustomerGrpDetailMandatory As Boolean = False
        IsCustomerGrpDetailMandatory = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsCustomerGroupFieldsMandatory, clsFixedParameterCode.IsCustomerGroupFieldsMandatory, Nothing)) = 1, True, False)

        Dim trans As SqlTransaction = Nothing
        If fndCustomerGroupCode.Value = "" Then
            myMessages.blankValue("Customer Group Code")
            fndCustomerGroupCode.Focus()
        ElseIf txtCustomerGroupDesc.Text = "" Then
            myMessages.blankValue("Customer Group Desc")
            txtCustomerGroupDesc.Focus()
        ElseIf IsCustomerGrpDetailMandatory Then
            If fndAccountSet.Value = "" Then
                myMessages.blankValue("Account Set")
                fndAccountSet.Focus()
            ElseIf fndTermsCode.Value = "" Then
                myMessages.blankValue("Term Code")
                fndTermsCode.Focus()
            ElseIf fndTaxGroup.Value = "" Then
                myMessages.blankValue("Tax Group")
                fndTaxGroup.Focus()
            End If
        Else
            Try
                Dim percentage As Decimal
                If txtPercentage.Text = "" Then
                    percentage = 0.0
                Else
                    percentage = txtPercentage.Text
                End If

                trans = clsDBFuncationality.GetTransactin()
                Dim isSaved As Boolean = clsDBFuncationality.UpdateInSelectedDatabase(trans, GetReplecateCompaniesDataBase(), "sp_tspl_customer_group_master_insert", New SqlParameter("@CustGroupCode", fndCustomerGroupCode.Value), New SqlParameter("@CustGroupDesc", txtCustomerGroupDesc.Text), New SqlParameter("@TaxGroup", fndTaxGroup.Value), New SqlParameter("@CustAccount", fndAccountSet.Value), New SqlParameter("@TermsCode", fndTermsCode.Value), New SqlParameter("@CreatedBy", userCode), New SqlParameter("@CreatedDate", connectSql.serverDate(trans)), New SqlParameter("@ModifiedBy", userCode), New SqlParameter("@ModifiedDate", connectSql.serverDate(trans)), New SqlParameter("@CompCode", companyCode), New SqlParameter("@ShelfLife", TxtShfLife.Text))
                If fndSalespersoncode.Value <> "" Then
                    isSaved = isSaved AndAlso clsDBFuncationality.UpdateInSelectedDatabase(trans, GetReplecateCompaniesDataBase(), "sp_TSPL_CUSTOMER_SALESMAN_DETAIL_insert", New SqlParameter("@CustCode", fndCustomerGroupCode.Value), New SqlParameter("@Salespersoncode", fndSalespersoncode.Value), New SqlParameter("@SalespersonName", txtSalespersonname.Text), New SqlParameter("@Percentage", percentage), New SqlParameter("@CreatedBy", userCode), New SqlParameter("@CreatedDate", connectSql.serverDate(trans)), New SqlParameter("@ModifiedBy", userCode), New SqlParameter("@ModifiedDate", connectSql.serverDate(trans)), New SqlParameter("@CompCode", companyCode))
                End If
                ''For Custom Fields
                'obj.Form_ID = MyBase.Form_ID
                arrCustomFields = New List(Of clsCustomFieldValues)
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.GetData(arrCustomFields)
                End If
                '' custom fields
                isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(MyBase.Form_ID, fndCustomerGroupCode.Value.Trim(), arrCustomFields, trans)
                ''End of For Custom Fields

                If isSaved Then
                    trans.Commit()
                    myMessages.insert()
                    btnSave.Text = "Update"
                    btnDelete.Enabled = True
                Else
                    trans.Rollback()
                End If
            Catch ex As Exception
                myMessages.myExceptions(ex)
                trans.Rollback()
            End Try
        End If
    End Sub

    'fill Custormer  Mster and  Custormer Details
    Private Sub funFill()
        Try

            If fndCustomerGroupCode.Value <> "" Then
                ds = connectSql.RunSQLReturnDS("select cust_group_desc,tax_group,cust_account,Terms_Code, Shelf_Life from tspl_Customer_Group_Master where cust_group_code='" + fndCustomerGroupCode.Value + "'")
                Dim dr As DataRow = ds.Tables(0).Rows(0)
                txtCustomerGroupDesc.Text = dr(0).ToString().Trim()
                fndTaxGroup.Value = dr(1).ToString().Trim()
                fndAccountSet.Value = dr(2).ToString().Trim()
                fndTermsCode.Value = dr(3).ToString().Trim()
                TxtShfLife.Text = dr(4).ToString()
                txtTaxGroup.Enabled = True

                ''For Custom Fields
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.LoadData(fndCustomerGroupCode.Value.Trim())
                End If
                ''End of For Custom Fields
            End If
            Dim dt As DataTable
            dt = clsDBFuncationality.GetDataTable(" select SalesPerson_Code,SalesPerson_Name,floor(Percentage) as Percentage from TSPL_CUSTOMER_SALESMAN_DETAIL where Cust_Code='" + fndCustomerGroupCode.Value + "'")
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

                For Each row As DataRow In dt.Rows
                    fndSalespersoncode.Value = row(0).ToString()
                    txtSalespersonname.Text = row(1).ToString()
                    txtPercentage.Text = row(2).ToString()
                Next
            End If
            If fndCustomerGroupCode.Value = "" Then
                btnSave.Text = "Save"
            End If
            btnSave.Enabled = True
            btnSave.Text = "Update"
            btnDelete.Enabled = True

            'If userCode <> "ADMIN" Then
            '    If funSetUserAccess() = False Then Exit Sub
            'End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'update  Custromer Master and  Customer Details
    Private Sub funUpdate()
        Dim trans As SqlTransaction = Nothing

        If fndCustomerGroupCode.Value = "" Then
            myMessages.blankValue("Customer Group Code")
            fndCustomerGroupCode.Focus()
        ElseIf fndAccountSet.Value = "" Then
            myMessages.blankValue("Account Set")
            fndAccountSet.Focus()
        ElseIf fndTermsCode.Value = "" Then
            myMessages.blankValue("Terms Code")
            fndTermsCode.Focus()
        ElseIf fndTaxGroup.Value = "" Then
            myMessages.blankValue("Tax Group")
            fndTaxGroup.Focus()
        Else
            Try
                Dim percentage As Decimal
                If txtPercentage.Text = "" Then
                    percentage = 0.0
                Else
                    percentage = txtPercentage.Text
                End If
                Dim ArrDBName As List(Of String) = GetReplecateCompaniesDataBase()
                trans = clsDBFuncationality.GetTransactin()
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(fndCustomerGroupCode.Value), "TSPL_CUSTOMER_GROUP_MASTER", "Cust_Group_Code", trans)
                Dim isSaved As Boolean = clsDBFuncationality.UpdateInSelectedDatabase(trans, ArrDBName, "sp_tspl_customer_group_master_update", New SqlParameter("@CustGroupCode", fndCustomerGroupCode.Value), New SqlParameter("@CustGroupDesc", txtCustomerGroupDesc.Text), New SqlParameter("@TaxGroup", fndTaxGroup.Value), New SqlParameter("@CustAccount", fndAccountSet.Value), New SqlParameter("@TermsCode", fndTermsCode.Value), New SqlParameter("@CreatedBy", userCode), New SqlParameter("@CreatedDate", connectSql.serverDate(trans)), New SqlParameter("@ModifiedBy", userCode), New SqlParameter("@ModifiedDate", connectSql.serverDate(trans)), New SqlParameter("@CompCode", companyCode), New SqlParameter("@ShelfLife", TxtShfLife.Text))
                If fndSalespersoncode.Value <> "" Then
                    isSaved = isSaved AndAlso clsDBFuncationality.UpdateInSelectedDatabase(trans, ArrDBName, "sp_TSPL_CUSTOMER_SALESMAN_DETAIL_Delete", New SqlParameter("@CustCode", fndCustomerGroupCode.Value))

                    isSaved = isSaved AndAlso clsDBFuncationality.UpdateInSelectedDatabase(trans, ArrDBName, "sp_TSPL_CUSTOMER_SALESMAN_DETAIL_insert", New SqlParameter("@CustCode", fndCustomerGroupCode.Value), New SqlParameter("@Salespersoncode", fndSalespersoncode.Value), New SqlParameter("@SalespersonName", txtSalespersonname.Text), New SqlParameter("@Percentage", percentage), New SqlParameter("@CreatedBy", userCode), New SqlParameter("@CreatedDate", connectSql.serverDate(trans)), New SqlParameter("@ModifiedBy", userCode), New SqlParameter("@ModifiedDate", connectSql.serverDate(trans)), New SqlParameter("@CompCode", companyCode))
                End If

                ''For Custom Fields
                'obj.Form_ID = MyBase.Form_ID
                arrCustomFields = New List(Of clsCustomFieldValues)
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.GetData(arrCustomFields)
                End If
                '' custom fields
                isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(MyBase.Form_ID, fndCustomerGroupCode.Value.Trim(), arrCustomFields, trans)
                ''End of For Custom Fields

                If isSaved Then
                    trans.Commit()
                    myMessages.update()
                    btnSave.Enabled = True
                    btnSave.Text = "Update"
                Else
                    trans.Rollback()
                End If
            Catch ex As Exception
                myMessages.myExceptions(ex)
                trans.Rollback()
            End Try
        End If
    End Sub
    ' reset Customer Master and Customer Details
    Private Sub funReset()
        fndCustomerGroupCode.Value = ""
        txtCustomerGroupDesc.Text = ""
        fndAccountSet.Value = ""
        txtAccountSetDesc.Text = ""
        fndTermsCode.Value = ""
        txtTermsCode.Text = ""
        fndTaxGroup.Value = ""
        txtTaxGroup.Text = ""
        btnSave.Enabled = True
        btnSave.Text = "Save"
        btnDelete.Enabled = False
        fndSalespersoncode.Value = ""
        txtSalespersonname.Text = ""
        txtPercentage.Text = ""
        fndCustomerGroupCode.Focus()
        TxtShfLife.Text = 0
        fndCustomerGroupCode.MyReadOnly = False
        SetDataBaseGrid()
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
        End If
        ''End of For Custom Fields
    End Sub
    ' delete Customer Master and Customer Details
    Private Sub funDelete()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = clsDBFuncationality.UpdateInSelectedDatabase(trans, GetReplecateCompaniesDataBase(), "sp_tspl_customer_group_master_delete", New SqlParameter("@CustGroupCode", fndCustomerGroupCode.Value))
            isSaved = isSaved AndAlso clsDBFuncationality.UpdateInSelectedDatabase(trans, GetReplecateCompaniesDataBase(), "sp_TSPL_CUSTOMER_SALESMAN_DETAIL_Delete", New SqlParameter("@CustCode", fndCustomerGroupCode.Value))
            If isSaved Then
                trans.Commit()
                myMessages.delete()
                btnSave.Enabled = True
                btnSave.Text = "Save"
                btnDelete.Enabled = False
            Else
                trans.Rollback()
            End If
        Catch ex As Exception
            trans.Rollback()
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'priti added on 01-06-2011 --- To implement the access control
    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        'If funCheckLoginStatus() = False Then Exit Function
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "CUST-GRP-M"
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
#Region "Finder Load"
    Private Sub fndCustomerGroupCode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndCustomerGroupCode.ConnectionString = connectSql.SqlCon()
        'fndCustomerGroupCode.Query = "select Cust_Group_Code as [Customer Group Code],Cust_Group_Desc as [Description]  from tspl_customer_group_master  order by Cust_Group_Code"
        'fndCustomerGroupCode.ValueToSelect = "Customer Group Code"
        'fndCustomerGroupCode.ValueToSelect1 = "Description"
        'fndCustomerGroupCode.Caption = "Customer Details"
    End Sub
    Private Sub fndTermsCode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndTermsCode.ConnectionString = connectSql.SqlCon()
        'fndTermsCode.Query = "select terms_code as [Terms Code], terms_desc as [Terms Description] from TSPL_TERMS_MASTER  order by terms_code"
        'fndTermsCode.ValueToSelect = "Terms Code"
        'fndTermsCode.ValueToSelect1 = "Terms Description"
        'fndTermsCode.Caption = "Terms Code Details"
    End Sub

    Private Sub fndAccountSet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndAccountSet.ConnectionString = connectSql.SqlCon()
        'fndAccountSet.Query = "select cust_account as [AccountSet Code],cust_acct_desc[Account Description] from TSPL_CUSTOMER_ACCOUNT_SET  order by cust_account"
        'fndAccountSet.ValueToSelect = "AccountSet Code"
        'fndAccountSet.ValueToSelect1 = "Account Description"
        'fndAccountSet.Caption = "AccountSet Details"
    End Sub

    Private Sub fndTaxGroup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndTaxGroup.ConnectionString = connectSql.SqlCon()
        'fndTaxGroup.Query = " select Tax_Group_Code as [TaxGroup Code],Tax_Group_Desc as [Tax Group Description],Tax_Group_Type as [Tax Group Type] from tspl_tax_group_master  order by Tax_Group_Code"
        'fndTaxGroup.ValueToSelect = "TaxGroup Code"
        'fndTaxGroup.ValueToSelect1 = "Tax Group Description"
        'fndTaxGroup.Caption = "TaxGroup Details"
    End Sub

    Private Sub fndTermsCode_Load_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndTermsCode.ConnectionString = connectSql.SqlCon()
        'fndTermsCode.Query = "select terms_code as [Terms Code], terms_desc as [Terms Description] from TSPL_TERMS_MASTER  order by  terms_code"
        'fndTermsCode.ValueToSelect = "Terms Code"
        'fndTermsCode.ValueToSelect1 = "Terms Description"
        'fndTermsCode.Caption = "Terms Code Details"
    End Sub
    Private Sub fndSalespersoncode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndSalespersoncode.ConnectionString = connectSql.SqlCon()
        'fndSalespersoncode.Query = "select Emp_Code as [Salesperson Code],Emp_Name as [Salesperson Name] from tspl_employee_master order by Emp_Code"
        'fndSalespersoncode.ValueToSelect = "Salesperson Code"
        'fndSalespersoncode.ValueToSelect1 = "Salesperson Name"
        'fndSalespersoncode.Caption = "Salesperson Details"
    End Sub
#End Region
#Region "ButtonClick Event"
    'close the form
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        funReset()
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(fndCustomerGroupCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("You Cannot Delete Record")
            Exit Sub
        End If
        If myMessages.deleteConfirm() Then
            funDelete()
        End If
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub
    Sub SaveData()
        If MyBase.isModifyonPasswordFlag Then
            If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.CustomerGroup, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
            Else
                Return
            End If
        End If
        If btnSave.Text = "Save" Then
            funInsert()
        Else
            funUpdate()
        End If
    End Sub
#End Region
#Region "Finder Leave Event"

    Private Sub fndAccountSet_Leave_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fndAccountSet.Value <> "" Then
            Dim s As String
            s = clsDBFuncationality.getSingleValue("select cust_account from tspl_Customer_account_set where cust_account='" + fndAccountSet.Value + "'")

            'While dr.Read()
            '    s = dr(0).ToString()
            'End While
            If s <> fndAccountSet.Value Then
                common.clsCommon.MyMessageBoxShow("Account Set doesn't exist")
                fndAccountSet.Value = ""
                txtAccountSetDesc.Text = ""
                fndAccountSet.Focus()
            Else

                txtAccountSetDesc.Text = fndAccountSet.Tag
            End If
        Else
            fndTermsCode.Focus()
        End If
    End Sub

    Private Sub fndTermsCode_Leave_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fndTermsCode.Value <> "" Then
            Dim s As String
            s = clsDBFuncationality.getSingleValue("select Terms_Code from tspl_Terms_Master where Terms_Code='" + fndTermsCode.Value + "'")

            'While dr.Read()
            '    s = dr(0).ToString()
            'End While
            If s <> fndTermsCode.Value Then
                common.clsCommon.MyMessageBoxShow("Terms Code doesn't exist")
                fndTermsCode.Value = ""
                txtTermsCode.Text = ""
                fndTermsCode.Focus()
            Else
                txtTermsCode.Text = fndTermsCode.Tag
            End If
        Else
            fndTaxGroup.Focus()
        End If
    End Sub

    Private Sub fndTaxGroup_Leave_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fndTaxGroup.Value <> "" Then
            Dim s As String
            s = clsDBFuncationality.getSingleValue("select Tax_Group_Code from tspl_Tax_Group_Master where Tax_Group_Code='" + fndTaxGroup.Value + "'")

            'While dr.Read()
            '    s = dr(0).ToString()
            'End While
            If s <> fndTaxGroup.Value Then
                common.clsCommon.MyMessageBoxShow("Tax Group doesn't exist")
                fndTaxGroup.Value = ""
                txtTaxGroup.Text = ""
                fndTaxGroup.Focus()
            Else
                txtTaxGroup.Text = fndTaxGroup.Tag
            End If
        Else
            fndSalespersoncode.Focus()
        End If
    End Sub


    Private Sub fndSalespersoncode_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fndSalespersoncode.Value <> "" Then
            Dim s As String
            s = clsDBFuncationality.getSingleValue("select Emp_Code from TSPL_EMPLOYEE_MASTER where Emp_Code='" + fndSalespersoncode.Value + "'")

            'While dr.Read()
            '    s = dr(0).ToString()
            'End While
            If s <> fndSalespersoncode.Value Then
                common.clsCommon.MyMessageBoxShow("Salesperson Code doesn't exist")
                fndSalespersoncode.Value = ""
                txtSalespersonname.Text = ""
                fndSalespersoncode.Focus()
            Else
                txtSalespersonname.Text = fndSalespersoncode.Tag
            End If
        Else
        End If
    End Sub

    Private Sub fndTermsCode_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fndTermsCode.Value <> "" Then
            Dim s As String
            s = clsDBFuncationality.getSingleValue("select terms_code from TSPL_TERMS_MASTER where terms_code='" + fndTermsCode.Value + "'")

            'While dr.Read()
            '    s = dr(0).ToString()
            'End While
            If s <> fndTermsCode.Value Then
                common.clsCommon.MyMessageBoxShow("Terms Code doesn't exist")
                fndTermsCode.Value = ""
                txtTermsCode.Text = ""
                fndTermsCode.Focus()
            Else
            End If
        Else
            ''fndSalespersoncode.txtValue.Focus()
        End If
    End Sub

    Private Sub txtPercentage_Leave_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPercentage.Leave
        If txtPercentage.Text <> "" Then
            Dim m As Integer = Integer.Parse(txtPercentage.Text)
            If m > 100 Then
                common.clsCommon.MyMessageBoxShow("Percentage cannot be greater than 100.")
                txtPercentage.Text = 100
                ' txtPercentage.Text = ""
                'txtPercentage.Focus()
            Else

            End If
        End If
    End Sub

#End Region
#Region "Import/Export"
    Private Sub MenuImport1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuImport1.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Cust_Group_Code", "Cust_Group_Desc", "Tax_Group", "Cust_Account", "Terms_Code", "SalesPerson_Code", "SalesPerson_Name", "Percentage", "Shelf_Life") Then
            clsCommon.ProgressBarShow()
            Dim trans As SqlTransaction = Nothing
            Try
                trans = clsDBFuncationality.GetTransactin()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim strCustGroup As String
                    Dim strCustDesc As String
                    Dim strAccSet As String
                    Dim strTaxGroup As String
                    Dim strTermsCode As String
                    Dim strSalespersonCode As String
                    Dim dblShelfLife As String
                    If clsCommon.myLen(grow.Cells(0).Value) <= 0 Then
                        Throw New Exception("Customer Group can't be Blank")
                    ElseIf clsCommon.myLen(grow.Cells(0).Value) > 12 Then
                        Throw New Exception("Customer Group cannot be greater than 12 length")
                    Else
                        strCustGroup = clsCommon.myCstr(grow.Cells(0).Value).ToUpper()
                    End If
                    If clsCommon.myLen(grow.Cells(1).Value) > 50 Then
                        Throw New Exception("Description cannot be greater than 50")
                    Else
                        strCustDesc = clsCommon.myCstr(grow.Cells(1).Value)
                    End If
                    If clsCommon.myLen(grow.Cells(3).Value) > 6 Then
                        Throw New Exception("AccountSet cannot be greater than 6 length.")
                    Else
                        strAccSet = clsCommon.myCstr(grow.Cells(3).Value).ToUpper()
                    End If
                    If clsCommon.myLen(grow.Cells(2).Value) > 12 Then
                        Throw New Exception("Tax Group cannot be greater than 12")
                    Else
                        strTaxGroup = clsCommon.myCstr(grow.Cells(2).Value)
                    End If
                    If clsCommon.myLen(grow.Cells(4).Value) > 50 Then
                        Throw New Exception("Terms Code cannot be greater than 50 length.")
                    Else
                        strTermsCode = clsCommon.myCstr(grow.Cells(4).Value).ToUpper()
                    End If

                    If clsCommon.myLen(grow.Cells(5).Value) > 12 Then
                        Throw New Exception("Salesperson Code cannot be greater than 12 length.")
                    Else
                        strSalespersonCode = clsCommon.myCstr(grow.Cells(5).Value).ToUpper()
                    End If

                    Dim strSalespersonName As String
                    If clsCommon.myLen(grow.Cells(6).Value) > 50 Then
                        Throw New Exception("Salesperson Name cannot be greater than 50 length.")
                    Else
                        strSalespersonName = clsCommon.myCstr(grow.Cells(6).Value)
                    End If


                    Dim percentage As Decimal = clsCommon.myCdbl(grow.Cells(7).Value)
                    If percentage > 100 Then
                        Throw New Exception("Percentage can't be greater than 100.")
                    End If

                    dblShelfLife = clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Shelf_Life").Value))
                    Dim Datee As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")

                    Dim sql1 As String = "select COUNT(*) from TSPL_CUSTOMER_GROUP_MASTER  where Cust_Group_Code='" + strCustGroup + "'"
                    Dim i As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sql1, trans))
                    If (i = 0) Then
                        sql = "insert into TSPL_CUSTOMER_GROUP_MASTER values('" + strCustGroup + "','" + strCustDesc + "','" + strTaxGroup + "','" + strAccSet + "','" + strTermsCode + "','" + userCode + "','" + Datee + "','" + userCode + "','" + Datee + "','" + companyCode + "','" + dblShelfLife + "')"
                        clsDBFuncationality.ExecuteNonQuery(sql, trans)
                    Else
                        clsDBFuncationality.SaveAStorePorcedure(trans, "sp_tspl_customer_group_master_update", New SqlParameter("@CustGroupCode", strCustGroup), New SqlParameter("@CustGroupDesc", strCustDesc), New SqlParameter("@TaxGroup", strTaxGroup), New SqlParameter("@CustAccount", strAccSet), New SqlParameter("@TermsCode", strTermsCode), New SqlParameter("@CreatedBy", userCode), New SqlParameter("@CreatedDate", Datee), New SqlParameter("@ModifiedBy", userCode), New SqlParameter("@ModifiedDate", Datee), New SqlParameter("@CompCode", companyCode), New SqlParameter("@ShelfLife", dblShelfLife))
                    End If
                    Dim sql2 As String = "select COUNT(*) from TSPL_CUSTOMER_SALESMAN_DETAIL  where Cust_Code='" + strCustGroup + "'"
                    Dim j As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sql2, trans))
                    If (j = 0) Then
                        clsDBFuncationality.SaveAStorePorcedure(trans, "[sp_TSPL_CUSTOMER_SALESMAN_DETAIL_insert]", New SqlParameter("@CustCode", strCustGroup), New SqlParameter("@Salespersoncode", strSalespersonCode), New SqlParameter("@SalespersonName", strSalespersonName), New SqlParameter("@Percentage", percentage), New SqlParameter("@CreatedBy", userCode), New SqlParameter("@CreatedDate", Datee), New SqlParameter("@ModifiedBy", userCode), New SqlParameter("@ModifiedDate", Datee), New SqlParameter("@CompCode", companyCode))
                    Else
                        clsDBFuncationality.SaveAStorePorcedure(trans, "[sp_TSPL_CUSTOMER_SALESMAN_DETAIL_update]", New SqlParameter("@CustCode", strCustGroup), New SqlParameter("@Salespersoncode", strSalespersonCode), New SqlParameter("@SalespersonName", strSalespersonName), New SqlParameter("@Percentage", percentage), New SqlParameter("@CreatedBy", userCode), New SqlParameter("@CreatedDate", Datee), New SqlParameter("@ModifiedBy", userCode), New SqlParameter("@ModifiedDate", Datee), New SqlParameter("@CompCode", companyCode))
                    End If
                    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCustGroup), "TSPL_CUSTOMER_GROUP_MASTER", "Cust_Group_Code", trans)
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)

            Finally
                clsCommon.ProgressBarHide()
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub MenuExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuExport.Click
        sql = "select m.Cust_Group_Code,m.Cust_Group_Desc,m.Tax_Group,m.Cust_Account,m.Terms_Code,d.SalesPerson_Code ,d.SalesPerson_Name ,d.Percentage ,m.Shelf_Life from [TSPL_CUSTOMER_GROUP_MASTER] m left outer join TSPL_CUSTOMER_SALESMAN_DETAIL d on m.Cust_Group_Code =d.Cust_Code "
        transportSql.ExporttoExcel(sql, Me)
    End Sub
#End Region

    Private Function GetReplecateCompaniesDataBase() As List(Of String)
        Dim arrDBName As New List(Of String)
        arrDBName.Add(objCommonVar.CurrDatabase)
        For ii As Integer = 0 To gvDB.Rows.Count - 1
            If (clsCommon.myCBool(gvDB.Rows(ii).Cells(colSelect).Value)) Then
                arrDBName.Add(clsCommon.myCstr(gvDB.Rows(ii).Cells(colDataBaseName).Value))
            End If
        Next
        Return arrDBName
    End Function


    Sub SetDataBaseGrid()
        gvDB.Rows.Clear()
        gvDB.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.FormatString = ""
        repoSelect.HeaderText = "Select"
        repoSelect.Name = colSelect
        repoSelect.Width = 50
        repoSelect.ReadOnly = False
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvDB.MasterTemplate.Columns.Add(repoSelect)

        Dim repoCompCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCompCode.FormatString = ""
        repoCompCode.HeaderText = "Company Code"
        repoCompCode.Name = colCompCode
        repoCompCode.Width = 150
        repoCompCode.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoCompCode)

        Dim repoCompName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCompName.FormatString = ""
        repoCompName.HeaderText = "Company Name"
        repoCompName.Name = colCompName
        repoCompName.Width = 150
        repoCompName.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoCompName)

        Dim repoDB As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDB.FormatString = ""
        repoDB.HeaderText = "Database Name"
        repoDB.Name = colDataBaseName
        repoDB.IsVisible = False
        repoDB.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoDB)

        Dim qry As String = "SELECT Comp_Code,Comp_Name,DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0 and Comp_Code not in ('" + objCommonVar.CurrentCompanyCode + "')"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                gvDB.Rows.AddNew()
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colSelect).Value = False
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colCompCode).Value = clsCommon.myCstr(dr("Comp_Code"))
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colCompName).Value = clsCommon.myCstr(dr("Comp_Name"))
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colDataBaseName).Value = clsCommon.myCstr(dr("DataBase_Name"))
            Next
        End If
    End Sub

    Private Sub frmCustomerGroup_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            'PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()

        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()

        End If
    End Sub

    Private Sub fndCustomerGroupCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean)
       
    End Sub

    Private Sub fndAccountSet__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndAccountSet._MYValidating
        Dim qry As String = "select cust_account as [AccountSetCode],cust_acct_desc[Account Description] from TSPL_CUSTOMER_ACCOUNT_SET   "
        fndAccountSet.Value = clsCommon.ShowSelectForm("CUSTACCETFND", qry, "AccountSetCode", "", fndAccountSet.Value, "cust_account", isButtonClicked)
        txtAccountSetDesc.Text = clsDBFuncationality.getSingleValue("select cust_acct_desc from TSPL_CUSTOMER_ACCOUNT_SET where cust_account='" + fndAccountSet.Value + "'")
    End Sub

    Private Sub fndTermsCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndTermsCode._MYValidating
        Dim qry As String = "select terms_code as [TermsCode], terms_desc as [Terms Description] from TSPL_TERMS_MASTER   "
        fndTermsCode.Value = clsCommon.ShowSelectForm("TREMCODEFNDN", qry, "TermsCode", "", fndTermsCode.Value, "terms_code", isButtonClicked)
        txtTermsCode.Text = clsDBFuncationality.getSingleValue("select terms_desc from TSPL_TERMS_MASTER where Terms_Code ='" + fndTermsCode.Value + "'")
    End Sub

    Private Sub fndTaxGroup__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndTaxGroup._MYValidating
        Dim qry As String = " select Tax_Group_Code as [TaxGroupCode],Tax_Group_Desc as [Tax Group Description],Tax_Group_Type as [Tax Group Type] from tspl_tax_group_master   "
        fndTaxGroup.Value = clsCommon.ShowSelectForm("TAXGROUPECODFND", qry, "TaxGroupCode", "", fndTaxGroup.Value, "Tax_Group_Code", isButtonClicked)
        txtTaxGroup.Text = clsDBFuncationality.getSingleValue("select Tax_Group_Desc   from tspl_tax_group_master where Tax_Group_Code  ='" + fndTaxGroup.Value + "'")
    End Sub

    
    Private Sub fndSalespersoncode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndSalespersoncode._MYValidating
        Dim qry As String = "select Emp_Code as [SalespersonCode],Emp_Name as [Salesperson Name] from tspl_employee_master  "
        fndSalespersoncode.Value = clsCommon.ShowSelectForm("EMPCODEFND", qry, "SalespersonCode", "", fndSalespersoncode.Value, "Emp_Code", isButtonClicked)
        txtSalespersonname.Text = clsDBFuncationality.getSingleValue("select Emp_Name  from tspl_employee_master where Emp_Code ='" + fndSalespersoncode.Value + "'  ")
    End Sub

    Private Sub fndCustomerGroupCode__MYValidating_1(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndCustomerGroupCode._MYValidating
        Dim str As String = "select count(*) from tspl_customer_group_master   where  Cust_Group_Code ='" + fndCustomerGroupCode.Value + "' "

        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))

        If no = 0 Then
            fndCustomerGroupCode.MyReadOnly = False
        Else
            fndCustomerGroupCode.MyReadOnly = True
        End If

        If fndCustomerGroupCode.MyReadOnly OrElse isButtonClicked Then
            'Dim QRY As String = "select Cust_Group_Code as [CustomerGroupCode],Cust_Group_Desc as [Description]  from tspl_customer_group_master   "
            'fndCustomerGroupCode.Value = clsCommon.ShowSelectForm("CUSTGROUPFNDD", QRY, "CustomerGroupCode", "", fndCustomerGroupCode.Value, "Cust_Group_Code", isButtonClicked)
            fndCustomerGroupCode.Value = clsCustomerGroupMaster.getFinder("", fndCustomerGroupCode.Value, isButtonClicked)
            txtCustomerGroupDesc.Text = clsDBFuncationality.getSingleValue("select Cust_Group_Desc  from tspl_customer_group_master where Cust_Group_Code='" + fndCustomerGroupCode.Value + "'")
            LoadData()
            Loadme()
        End If
    End Sub
    Sub Loadme()
        txtAccountSetDesc.Text = clsDBFuncationality.getSingleValue("select cust_acct_desc from TSPL_CUSTOMER_ACCOUNT_SET where cust_account='" + fndAccountSet.Value + "'")
        txtTermsCode.Text = clsDBFuncationality.getSingleValue("select terms_desc from TSPL_TERMS_MASTER where Terms_Code ='" + fndTermsCode.Value + "'")
        txtTaxGroup.Text = clsDBFuncationality.getSingleValue("select Tax_Group_Desc   from tspl_tax_group_master where Tax_Group_Code  ='" + fndTaxGroup.Value + "'")
        txtSalespersonname.Text = clsDBFuncationality.getSingleValue("select Emp_Name  from tspl_employee_master where Emp_Code ='" + fndSalespersoncode.Value + "'  ")
    End Sub
    Private Sub fndCustomerGroupCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndCustomerGroupCode._MYNavigator
        Dim qst As String = "select Cust_Group_Code as [Customer Group Code],Cust_Group_Desc as [Description]  from tspl_customer_group_master   where 2=2   "
        Select Case NavType
            Case NavigatorType.Current
                qst += " and tspl_customer_group_master .Cust_Group_Code in ('" + fndCustomerGroupCode.Value + "')"
            Case NavigatorType.Next
                qst += " and tspl_customer_group_master .Cust_Group_Code in (select min(Cust_Group_Code ) from tspl_customer_group_master where tspl_customer_group_master .Cust_Group_Code  >'" + fndCustomerGroupCode.Value + "')"
            Case NavigatorType.First
                qst += " and tspl_customer_group_master .Cust_Group_Code in (select MIN(Cust_Group_Code ) from tspl_customer_group_master)"

            Case NavigatorType.Last
                qst += " and tspl_customer_group_master .Cust_Group_Code in (select Max(Cust_Group_Code ) from tspl_customer_group_master)"
            Case NavigatorType.Previous
                qst += " and tspl_customer_group_master .Cust_Group_Code in (select Max(Cust_Group_Code ) from tspl_customer_group_master where tspl_customer_group_master .Cust_Group_Code  <'" + fndCustomerGroupCode.Value + "')"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            fndCustomerGroupCode.Value = clsCommon.myCstr(dt.Rows(0)("Customer Group Code"))
            txtCustomerGroupDesc.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
        End If
        LoadData()
        Loadme()

    End Sub

    Private Sub fndAccountSet_Load_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fndAccountSet.Load

    End Sub
End Class
