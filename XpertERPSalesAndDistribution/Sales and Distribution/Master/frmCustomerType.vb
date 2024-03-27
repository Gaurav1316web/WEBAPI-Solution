'Developed By -BibhuPrasad Parida
'Database - TSPLERP
'Table - tspl_customer_category_master
'Start Date -
'End Date -
'-20/12/2012:11:00AM--Updation By--Pankaj Kumar--Applied Validations
'-03/04/2013:5:41PM--Updation By--Pankaj Kumar--Transaction Problem while importing-------------------Ashok
'--preeti gupta-ticket no-[BM00000003128]
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports common
Imports XpertERPEngine

Public Class frmCustomerType
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()


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
    Dim userCode, companyCode As String
    Const colSelect As String = "SELECT"
    Const colCompCode As String = "COMPCODE"
    Const colCompName As String = "COMPNAME"
    Const colDataBaseName As String = "DATABASE"

#End Region
#Region "Page Load"
    Private Sub Customer_Type_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        gvDB.AllowDeleteRow = False
        SetDataBaseGrid()
        SetUserMgmtNew()
        TextChangedsub()
        btnDelete.Enabled = False
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New Trasnaction")
        ValidateLength()
    End Sub
    Private Sub ValidateLength()
        txtCustomerDesc.MaxLength = 50
        fndCustomerId.MyMaxLength = 12
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.CustomerType)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnSave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 02/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnSave.Visible = True Then
            MenuImport.Enabled = True
            MenuExport.Enabled = True
        Else
            MenuImport.Enabled = False
            MenuExport.Enabled = False
        End If
        '--------------------------------------------------
        ' btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
#End Region
#Region "KeyPress Event"
    Private Sub KeyPress1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub
#End Region
#Region "TextChanged"
    Sub TextChangedsub()
        'dr = connectSql.RunSqlReturnDR("select cust_type_code from tspl_customer_type_master where cust_type_code='" + fndCustomerId.Value + "'")
        Dim s As String
        'While dr.Read()
        '    s = dr(0).ToString()
        'End While
        'dr.Close()
        s = clsDBFuncationality.getSingleValue("select cust_type_code from tspl_customer_type_master where cust_type_code='" + fndCustomerId.Value + "'")
        If s <> fndCustomerId.Value Then
            txtCustomerDesc.Text = ""
            btnSave.Text = "Save"
            btnDelete.Enabled = False
        Else
            funFill()
        End If
        If fndCustomerId.Value = "" Then
        End If
    End Sub
#End Region
    Private Sub TextChanged1(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub

#Region "Methods"
    'fill Customer Type Details
    Private Sub funFill()
        If fndCustomerId.Value <> "" Then
            'dr = connectSql.RunSqlReturnDR("select * from tspl_customer_type_master where Cust_Type_Code='" + fndCustomerId.Value + "'")
            sql = "select Cust_Type_Desc from tspl_customer_type_master where Cust_Type_Code='" + fndCustomerId.Value + "'"

            'Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(sql)
            'If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            '    txtCustomerDesc.Text = dr(1).ToString()
            'End If
            txtCustomerDesc.Text = clsDBFuncationality.getSingleValue(sql)
            btnSave.Enabled = True
            btnSave.Text = "Update"
            btnDelete.Enabled = True
            txtCustomerDesc.Enabled = True
        Else
            btnSave.Enabled = True
            btnSave.Text = "Save"
            txtCustomerDesc.Enabled = True
        End If
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If

    End Sub
    'insert Customer Type Details
    Private Sub funInsert()
        Try
            If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False)) Then
                Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from tspl_customer_type_master where Cust_Type_Code='" & fndCustomerId.Value & "'")
                If ChkNewEntry = 0 Then
                    fndCustomerId.Value = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"), clsDocType.CustomerType, "", "")
                    If clsCommon.myLen(fndCustomerId.Value) <= 0 Then
                        Throw New Exception("Error in Code Generation")
                    End If
                End If
            End If
            clsDBFuncationality.SaveAStorePorcedure("sp_tspl_Customer_Type_Master_insert", New SqlParameter("@CustTypeCode", fndCustomerId.Value), New SqlParameter("@CustTypeDesc", txtCustomerDesc.Text), New SqlParameter("@CreatedBy", userCode), New SqlParameter("@CreatedDate", connectSql.serverDate()), New SqlParameter("@ModifiedBy", userCode), New SqlParameter("@ModifiedDate", connectSql.serverDate()), New SqlParameter("@CompCode", companyCode))
            myMessages.insert()

            btnSave.Text = "Update"
            btnDelete.Enabled = True
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'update Customer Type Details
    Private Sub funUpdate()
        If fndCustomerId.Value = "" Then
            myMessages.blankValue(Me, "Customer Type", Me.Text)
        Else
            Try
                clsDBFuncationality.SaveAStorePorcedure("sp_tspl_Customer_Type_Master_update", New SqlParameter("@CustTypeCode", fndCustomerId.Value), New SqlParameter("@CustTypeDesc", txtCustomerDesc.Text), New SqlParameter("@CreatedBy", userCode), New SqlParameter("@CreatedDate", connectSql.serverDate()), New SqlParameter("@ModifiedBy", userCode), New SqlParameter("@ModifiedDate", connectSql.serverDate()), New SqlParameter("@CompCode", companyCode))
                myMessages.update()
                btnSave.Text = "Update"
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
    End Sub
    Private Sub funReset()
        fndCustomerId.Value = ""
        txtCustomerDesc.Text = ""
        btnSave.Text = "Save"
        btnDelete.Enabled = False
        fndCustomerId.Focus()
        fndCustomerId.MyReadOnly = False
        SetDataBaseGrid()
    End Sub

    'delete Customer Type Details
    Private Sub funDelete()
        Try
            clsDBFuncationality.SaveAStorePorcedure("sp_tspl_Customer_Type_Master_delete", New SqlParameter("@CustTypeCode", fndCustomerId.Value))
            myMessages.delete()
            btnSave.Text = "Save"
            btnDelete.Enabled = False
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    'priti added on 01-06-2011 --- To implement the access control
    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        'If funCheckLoginStatus() = False Then Exit Function
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "CUST-TYPE"
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
#Region "ButtonClick Event"
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub
    Sub SaveData()
        If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) AndAlso fndCustomerId.Value = "" Then
            myMessages.blankValue(Me, "Customer Type", Me.Text)
            fndCustomerId.Focus()
        Else
            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.CustomerType, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return
                End If
            End If

            If btnSave.Text = "Save" Then
                funInsert()
            Else
                funUpdate()
            End If
        End If

    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        funReset()
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If fndCustomerId.Value <> "" Then
            If myMessages.deleteConfirm() Then
                funDelete()
            Else

            End If
        End If
    End Sub
    Private Sub MenuClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuClose.Click
        Me.Close()
    End Sub
#End Region
#Region "Finder Load"
    Private Sub fndCustomerId_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndCustomerId.ConnectionString = connectSql.SqlCon()
        'fndCustomerId.Query = "select Cust_Type_Code as [Customer Type] ,Cust_Type_Desc as Description from tspl_customer_type_master order by Cust_Type_Code"
        'fndCustomerId.ValueToSelect = "Customer Type"
        'fndCustomerId.ValueToSelect1 = "Description"
        'fndCustomerId.Caption = "Customer Details"
    End Sub
#End Region
#Region "Finder Leave Event"
    Private Sub fndCustomerId_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If fndCustomerId.txtValue.Text <> "" Then
        '    txtCustomerDesc.Enabled = True
        '    txtCustomerDesc.Focus()
        '    btnSave.Enabled = True
        'Else
        '    btnClose.Focus()
        'End If
    End Sub
#End Region
#Region "Import/Export"
    Private Sub MenuExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuExport.Click
        sql = "select Cust_Type_Code,Cust_Type_Desc from TSPL_CUSTOMER_TYPE_MASTER "
        ListImpExpColumnsMandatory = New List(Of String)({"Cust_Type_Code"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"Cust_Type_Code"})
        transportSql.ExporttoExcel(sql, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
    End Sub

    Private Sub MenuImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Cust_Type_Code", "Cust_Type_Desc") Then
            Dim trans As SqlTransaction = Nothing
            Try
                'connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim strCustomerType As String
                    Dim strCustomerTypeDesc As String
                    If String.IsNullOrEmpty(grow.Cells(0).Value.ToString()) Then
                        'common.clsCommon.MyMessageBoxShow("Customer Type has some incorrect values")
                        Throw New Exception("Customer Type can not be blank")

                    ElseIf clsCommon.myLen(grow.Cells(0).Value) > 12 Then
                        Throw New Exception("Customer Type cannot be greater than 12 length.")

                    Else
                        strCustomerType = grow.Cells(0).Value.ToString().ToUpper()
                    End If
                    If grow.Cells(1).Value.ToString().Length > 50 Then
                        Throw New Exception("Customer Type Description cannot be greater than 50 length")

                    Else
                        strCustomerTypeDesc = clsCommon.myCstr(grow.Cells(1).Value)
                    End If

                    Dim sql1 As String = "select COUNT(*) from TSPL_CUSTOMER_TYPE_MASTER  where Cust_Type_Code='" + strCustomerType + "'"
                    Dim i As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sql1, trans))
                    If (i = 0) Then
                        ' connectSql.RunSpTransaction(trans, "@sp_tspl_Customer_Type_Master_insert", New SqlParameter("@CustTypeCode", str), New SqlParameter("@CustTypeDesc", str1), New SqlParameter("@CreatedBy", userCode), New SqlParameter("@CreatedDate", connectSql.serverDate()), New SqlParameter("@ModifiedBy", userCode), New SqlParameter("@ModifiedDate", connectSql.serverDate()), New SqlParameter("@CompCode", companyCode))
                        clsDBFuncationality.SaveAStorePorcedure(trans, "sp_tspl_Customer_Type_Master_insert", New SqlParameter("@CustTypeCode", strCustomerType), New SqlParameter("@CustTypeDesc", strCustomerTypeDesc), New SqlParameter("@CreatedBy", userCode), New SqlParameter("@CreatedDate", clsCommon.GETSERVERDATE(trans)), New SqlParameter("@ModifiedBy", userCode), New SqlParameter("@ModifiedDate", clsCommon.GETSERVERDATE(trans)), New SqlParameter("@CompCode", companyCode))
                    Else
                        clsDBFuncationality.SaveAStorePorcedure(trans, "sp_tspl_Customer_Type_Master_update", New SqlParameter("@CustTypeCode", strCustomerType), New SqlParameter("@CustTypeDesc", strCustomerTypeDesc), New SqlParameter("@CreatedBy", userCode), New SqlParameter("@CreatedDate", clsCommon.GETSERVERDATE(trans)), New SqlParameter("@ModifiedBy", userCode), New SqlParameter("@ModifiedDate", clsCommon.GETSERVERDATE(trans)), New SqlParameter("@CompCode", companyCode))
                    End If
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)

            End Try

        End If
        Me.Controls.Remove(gv)
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


    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click

    End Sub

    Private Sub frmCustomerType_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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


    Private Sub TxtNavigator1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fndCustomerId.Load

    End Sub

    Private Sub fndCustomerId__MYValidating_1(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndCustomerId._MYValidating
        Dim str As String = "select count(*) from tspl_customer_type_master   where  Cust_Type_Code ='" + fndCustomerId.Value + "' "

        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))

        If no = 0 Then
            fndCustomerId.MyReadOnly = False
        Else
            fndCustomerId.MyReadOnly = True
        End If
        If fndCustomerId.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = "select Cust_Type_Code as [CustomerType] ,Cust_Type_Desc as Description from tspl_customer_type_master "
            fndCustomerId.Value = clsCommon.ShowSelectForm("CUSTTYPCUSTFND", qry, "CustomerType", "", fndCustomerId.Value, "", isButtonClicked)
            txtCustomerDesc.Text = clsDBFuncationality.getSingleValue("select Cust_Type_Desc from tspl_customer_type_master where Cust_Type_Code= '" + fndCustomerId.Value + "'")
            TextChangedsub()
        End If
                            End Sub

    Private Sub fndCustomerId__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndCustomerId._MYNavigator
        Dim qst As String = "select Cust_Type_Code as [Customer Type] ,Cust_Type_Desc as Description from tspl_customer_type_master where 2=2 "
        Select Case NavType
            Case NavigatorType.Current
                qst += " and tspl_customer_type_master .Cust_Type_Code in ('" + fndCustomerId.Value + "')"
            Case NavigatorType.Next
                qst += " and tspl_customer_type_master .Cust_Type_Code in (select min(Cust_Type_Code ) from tspl_customer_type_master where Cust_Type_Code  >'" + fndCustomerId.Value + "')"
            Case NavigatorType.First
                qst += " and tspl_customer_type_master .Cust_Type_Code in (select MIN(Cust_Type_Code ) from tspl_customer_type_master)"

            Case NavigatorType.Last
                qst += " and tspl_customer_type_master .Cust_Type_Code in (select Max(Cust_Type_Code ) from tspl_customer_type_master)"
            Case NavigatorType.Previous
                qst += " and tspl_customer_type_master .Cust_Type_Code in (select Max(Cust_Type_Code ) from tspl_customer_type_master where Cust_Type_Code  <'" + fndCustomerId.Value + "')"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            fndCustomerId.Value = clsCommon.myCstr(dt.Rows(0)("Customer Type"))
            txtCustomerDesc.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
        End If
        TextChangedsub()
    End Sub
End Class
