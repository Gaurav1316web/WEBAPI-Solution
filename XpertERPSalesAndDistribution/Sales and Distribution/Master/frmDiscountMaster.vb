'--30/08/2012--Updation By-[Pankaj Kumar]--Added New Column (Sampling) for Import and Export-----Fwd by--[Ranjana Mam]
'' 19/10/2012,10:09 am Updated by abhishek For Check If DisCode Exist in ShipmentDetails Table Then Don't Delete.
'--preeti gupta-ticket no.[BM00000003133]
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class FrmDiscountMaster
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String


    Const colSelect As String = "SELECT"
    Const colCompCode As String = "COMPCODE"
    Const colCompName As String = "COMPNAME"
    Const colDataBaseName As String = "DATABASE"


#End Region
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Save()
    End Sub

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

    Public Sub Save()
        Try
            Dim strDiscount As String
            Dim strVsndType As String
            Dim strOther, strSampling As String
            Dim strchk As String
            If AllowToSave() Then
                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmDiscountMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
                Dim obj As New clsDiscountMaster()
                obj.Code = txtCode.Value
                obj.Description = txtDescription.Text
                obj.Account_Code = txtGLAccount.Value
                obj.Account_Description = txtGlAccDesc.Text
                obj.DiscountCategory = txtdiscountcategory.Value

                If rbtnDiscount.IsChecked = True Then
                    strDiscount = "Y"
                Else
                    strDiscount = "N"
                End If
                obj.Discount = strDiscount

                If rbtnVSNDType.IsChecked = True Then
                    strVsndType = "Y"
                Else
                    strVsndType = "N"
                End If
                obj.Vsnd = strVsndType
                If rbtnOther.IsChecked = True Then
                    strOther = "Y"
                Else
                    strOther = "N"
                End If
                obj.Other = strOther

                If rbtnSampling.IsChecked = True Then
                    strSampling = "Y"
                Else
                    strSampling = "N"
                End If
                obj.Sampling = strSampling

                If chksku.Checked = True Then
                    strchk = "Y"
                Else
                    strchk = "N"
                End If

                obj.skuwise = strchk
                obj.IsOpening = chkIsOpening.Checked
                If (obj.SaveData(obj, isNewEntry, GetReplecateCompaniesDataBase())) Then
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    LoadData(obj.Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

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

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        funReset()
        txtCode.MyReadOnly = True
        btnSave.Enabled = True
        btnDelete.Enabled = True
        isNewEntry = False
        Dim obj As New clsDiscountMaster()
        obj = clsDiscountMaster.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
            txtCode.Value = obj.Code
            txtDescription.Text = obj.Description
            txtGLAccount.Value = obj.Account_Code
            txtGlAccDesc.Text = obj.Account_Description
            txtdiscountcategory.Value = obj.DiscountCategory
            If obj.Discount = "Y" Then
                rbtnDiscount.IsChecked = True
            Else
                rbtnDiscount.IsChecked = False
            End If

            If obj.Vsnd = "Y" Then
                rbtnVSNDType.IsChecked = True
            Else
                rbtnVSNDType.IsChecked = False
            End If

            If obj.Other = "Y" Then
                rbtnOther.IsChecked = True
            Else
                rbtnOther.IsChecked = False
            End If

            If obj.Sampling = "Y" Then
                rbtnSampling.IsChecked = True
            Else
                rbtnSampling.IsChecked = False
            End If

            If obj.skuwise = "Y" Then
                chksku.Checked = True
            Else
                chksku.Checked = False
            End If

            chkIsOpening.Checked = obj.IsOpening

            Dim qry1 As String = "select description from TSPL_Discount_Category_Master where code='" + txtdiscountcategory.Value + "'"
            Dim desc As String = clsDBFuncationality.getSingleValue(qry1)
            If desc <> "" Then
                txtCategoryDesc.Text = desc
            Else
                txtCategoryDesc.Text = ""
            End If

        End If

    End Sub

    Function AllowToSave() As Boolean
        If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) AndAlso clsCommon.myLen(txtCode.Value) <= 0 Then
            myMessages.blankValue("Code")
            txtCode.Focus()
            Return False
        ElseIf clsCommon.myLen(txtGLAccount.Value) <= 0 Then
            myMessages.blankValue("Account")
            txtGLAccount.Focus()
            Return False
            'ElseIf clsCommon.myLen(txtdiscountcategory.Value) <= 0 Then
            '    myMessages.blankValue("Discount Category Code")
            '    txtdiscountcategory.Focus()
            '    Return False
        End If
        Return True
    End Function

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("You Cannot Delete Record")
            Exit Sub
        End If
        Dim discCode As String
        discCode = clsDBFuncationality.getSingleValue("select Discount_Code  from TSPL_SHIPMENT_DETAILS  where Discount_Code ='" & txtCode.Value & "'")
        If clsCommon.myLen(discCode) > 0 Then
            common.clsCommon.MyMessageBoxShow("This record can't be deleted.It is used in another process")
            Exit Sub
        End If
        funDelete()
    End Sub

    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsDiscountMaster.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Public Sub SetLength()
        txtCode.MyMaxLength = 12
        txtDescription.MaxLength = 50
    End Sub
    Private Sub FrmDiscountMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetLength()
        SetUserMgmtNew()
        isNewEntry = True
        txtCategoryDesc.Enabled = False
        rbtnDiscount.IsChecked = True
        SetDataBaseGrid()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ' ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmDiscountMaster)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        funReset()
    End Sub

    Sub funReset()
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        txtDescription.Text = ""
        txtGLAccount.Value = Nothing
        txtGlAccDesc.Text = ""
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = True
        rbtnDiscount.IsChecked = False
        txtdiscountcategory.Value = ""
        rbtnVSNDType.IsChecked = False
        rbtnOther.IsChecked = False
        txtCategoryDesc.Text = ""
        rbtnDiscount.IsChecked = True
        chksku.Checked = False
        SetDataBaseGrid()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        funClose()

    End Sub

    Sub funClose()
        Me.Close()
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Dim str As String = "select count(*) from TSPL_Discount_Master where code ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
        Else
            txtCode.MyReadOnly = True
        End If
        Dim Discount As String
        Dim Vsnd As String
        Dim Other As String
        Dim Sampling As String
        Dim skuwise As String
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = "select Code,Description from TSPL_Discount_Master"
            txtCode.Value = clsCommon.ShowSelectForm("DiscontMasterFND", qry, "Code", "", txtCode.Value, "Code", isButtonClicked)
            qry = "SELECT Description,Account_Code,Account_Description,Discount,VSND_Type, Other, Sampling , discount_category_Code ,skuwise,IsOpening  FROM TSPL_DISCOUNT_MASTER   where Code='" + txtCode.Value + "'"
            Dim dt As New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry)
            For Each row As DataRow In dt.Rows
                isNewEntry = False
                txtDescription.Text = row("Description").ToString()
                txtGLAccount.Value = row("Account_Code").ToString()
                txtGlAccDesc.Text = row("Account_Description").ToString()
                Discount = row("Discount").ToString()
                Vsnd = row("VSND_Type").ToString()
                Other = row("Other").ToString()
                Sampling = row("Sampling").ToString()
                skuwise = row("skuwise").ToString()
                If Discount = "Y" Then
                    rbtnDiscount.IsChecked = True
                Else
                    rbtnDiscount.IsChecked = False
                End If

                If Vsnd = "Y" Then
                    rbtnVSNDType.IsChecked = True
                Else
                    rbtnVSNDType.IsChecked = False
                End If

                If Other = "Y" Then
                    rbtnOther.IsChecked = True
                Else
                    rbtnOther.IsChecked = False
                End If
                If Sampling = "Y" Then
                    rbtnSampling.IsChecked = True
                Else
                    rbtnSampling.IsChecked = False
                End If

                If skuwise = "Y" Then
                    chksku.IsChecked = True
                Else
                    chksku.IsChecked = False
                End If
                chkIsOpening.Checked = clsCommon.myCBool(row("IsOpening"))
                txtdiscountcategory.Value = row("discount_category_Code").ToString()
                Dim qry1 As String = "select description from TSPL_Discount_Category_Master where code='" + txtdiscountcategory.Value + "'"
                Dim desc As String = clsDBFuncationality.getSingleValue(qry1)
                If desc <> "" Then
                    txtCategoryDesc.Text = desc
                End If
            Next
        End If

    End Sub

    Private Sub txtCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub menuImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuImport.Click
        funImport()
    End Sub

    Sub funImport()

        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Discount Code", "Description", "Account Code", "Account Description", "Discount", "Vsnd Type", "Discount Category Code", "Other", "SKU Wise", "Sampling") Then
            Dim trans As SqlTransaction = Nothing

            Try
                connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim counter As Integer = 0  '''' Validates the Multiplicity Of 'Y' in (Discount, VSND Type, Other)-----Pankaj Kumar
                    Dim strCode As String = clsCommon.myCstr(grow.Cells(0).Value)
                    Dim strDescription As String = clsCommon.myCstr(grow.Cells(1).Value)
                    Dim Account As String = clsCommon.myCstr(grow.Cells(2).Value)
                    Dim AccountDesc As String = clsCommon.myCstr(grow.Cells(3).Value)
                    ''''-------------------Code By----Pankaj Kumar----on------14/03/2012--------------------
                    Dim Discount As String = clsCommon.myCstr(grow.Cells(4).Value)
                    If Discount = "Y" Then
                        counter = counter + 1
                    End If
                    If Discount <> "Y" And Discount <> "N" Then
                        Throw New Exception("Please Insert Discount as Only 'Y' or 'N' with Discount '" + strDescription + "'")
                        Return
                    End If

                    Dim Vsnd As String = clsCommon.myCstr(grow.Cells(5).Value)
                    If Vsnd = "Y" Then
                        counter = counter + 1
                    ElseIf Vsnd <> "Y" And Vsnd <> "N" Then
                        Throw New Exception("Please Insert VSND Type as Only 'Y' or 'N' with Discount '" + strDescription + "'")
                    End If
                    '---------------------------------Code Ends Here-----------------------------
                    Dim discountCategoryCode As String = clsCommon.myCstr(grow.Cells(6).Value)
                    Dim finaldiscountCode As String = ""

                    If (String.IsNullOrEmpty(discountCategoryCode)) Or clsCommon.myLen(discountCategoryCode) > 12 Then
                        Throw New Exception(" Discount Categroy Code can not be blank or greather 12 length")
                    End If

                    If discountCategoryCode <> "" AndAlso clsCommon.myLen(discountCategoryCode) < 12 Then
                        Dim qry1 As String = "select Code from TSPL_Discount_Category_Master where Code='" + discountCategoryCode + "' "
                        Dim DiscountCode As String = clsDBFuncationality.getSingleValue(qry1, trans)
                        If DiscountCode <> "" Then
                            finaldiscountCode = DiscountCode
                        Else
                            'finaldiscountCode = discountCategoryCode
                            'qry = "INSERT Into TSPL_Discount_Category_Master(code,Description,created_by,created_date,modified_by,modified_date,comp_code) values('" + discountCategoryCode + "','Blank Description','" + objCommonVar.CurrentUserCode + "','" + connectSql.serverDate() + "','" + objCommonVar.CurrentUserCode + "','" + connectSql.serverDate + "','" + objCommonVar.CurrentCompanyCode + "')"
                            'connectSql.RunSql(qry)
                            Throw New Exception("Discount Categroy Code does not exist")
                        End If

                    End If

                    ''''-------------------Code By----Pankaj Kumar----on------14/03/2012--------------------
                    Dim Other As String = clsCommon.myCstr(grow.Cells(7).Value)
                    If Other = "Y" Then
                        counter = counter + 1
                    ElseIf Other <> "Y" And Other <> "N" Then
                        Throw New Exception("Please Insert Other as Only 'Y' or 'N' with Discount '" + strDescription + "'")
                    End If

                    If counter > 1 Then
                        Throw New Exception("Please Select Only one field as 'Y' among (Discount, VSND Type, Other) Under '" + strDescription + "'")
                    End If


                    Dim sku As String = clsCommon.myCstr(grow.Cells(8).Value)
                    If sku = "Y" Then
                        'counter = counter + 1
                    ElseIf sku <> "Y" And sku <> "N" And sku <> "" Then
                        Throw New Exception("Please Insert Other as Only 'Y' or 'N' ")
                    End If

                    'If counter > 1 Then
                    '    Throw New Exception("Please Select Only one field as 'Y' among (Discount, VSND Type, Other) Under '" + strDescription + "'")
                    'End If
                    Dim Sampling As String = clsCommon.myCstr(grow.Cells(9).Value)
                    If Sampling <> "Y" And Sampling <> "N" And Sampling <> "" Then
                        Throw New Exception("Please Insert Sampling as Only 'Y' or 'N' or Leave Blank with Discount '" + strDescription + "'")
                    End If
                    '--------------------------------------Code Ends Here----------------------------------
                    Dim qry As String = "select Description from tspl_gl_accounts where Account_Code='" + Account + "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dt.Rows.Count <= 0 Then
                        Throw New Exception("Account does not exist")
                    Else
                        For Each row As DataRow In dt.Rows
                            If (AccountDesc <> row("Description").ToString()) Then
                                Throw New Exception("Account description not exist for this account")
                            End If
                        Next
                    End If

                    If (String.IsNullOrEmpty(strCode)) Or clsCommon.myLen(strCode) > 50 Then
                        Throw New Exception("Code can not be blank or greather 50 length")
                    End If

                    If (String.IsNullOrEmpty(strDescription)) Or clsCommon.myLen(strDescription) > 50 Then
                        Throw New Exception("Description can not be blank or greather than 50 length")
                    End If
                    If (String.IsNullOrEmpty(Account)) Or clsCommon.myLen(Account) > 50 Then
                        Throw New Exception("Account can not be blank or greather than 50 length")
                    End If

                    If (String.IsNullOrEmpty(AccountDesc)) Or clsCommon.myLen(AccountDesc) > 100 Then
                        Throw New Exception("Account Description can not be blank or greather than 100 length")
                    End If

                    Dim sql1 As String = "select count(*) from TSPL_Discount_Master where Code='" + strCode + "'"
                    Dim i As Integer = CInt(connectSql.RunScalar(trans, sql1))
                    If (i = 0) Then
                        qry = "INSERT Into TSPL_Discount_Master values('" + strCode + "','" + strDescription + "','" + objCommonVar.CurrentUserCode + "','" + connectSql.serverDate(trans) + "','" + objCommonVar.CurrentUserCode + "','" + connectSql.serverDate(trans) + "','" + objCommonVar.CurrentCompanyCode + "','" + Account + "','" + AccountDesc + "','" + Discount + "','" + Vsnd + "','" + finaldiscountCode + "', '" + Other + "','" + sku + "', '" + Sampling + "')"
                        connectSql.RunSqlTransaction(trans, qry)
                        'connectSql.RunSpTransaction(trans, "sp_DesignationMaster_insert", New SqlParameter("@Category Code", strCtgryCode), New SqlParameter("@Category Name", strCtgryName), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode))
                    Else
                        qry = "UPDATE TSPL_Discount_Master set Code='" + strCode + "', Description='" + strDescription + "',Account_Code='" + Account + "',Account_Description='" + AccountDesc + "' ,Modified_By='" + objCommonVar.CurrentUserCode + "', Modified_Date='" + connectSql.serverDate(trans) + "', Comp_Code='" + objCommonVar.CurrentCompanyCode + "', Discount ='" + Discount + "',VSND_Type ='" + Vsnd + "',Discount_category_Code='" + finaldiscountCode + "', Other='" + Other + "',skuwise='" + sku + "', Sampling='" + Sampling + "' WHERE Code='" + strCode + "'"
                        connectSql.RunSqlTransaction(trans, qry)
                        'connectSql.RunSpTransaction(trans, "sp_DesignationMaster_update", New SqlParameter("@CAtegory Code", strCtgryCode), New SqlParameter("@Category Name", strCtgryName), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode))
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

    Private Sub RadMenuItem6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem6.Click
        funClose()
    End Sub

    Private Sub menuExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuExport.Click
        funExport()
    End Sub

    Sub funExport()
        Dim str As String
        str = "select Code as [Discount Code] ,Description,Account_Code as [Account Code],Account_Description as [Account Description], Discount, VSND_Type as [Vsnd Type] ,Discount_category_Code as [Discount Category Code], Other , skuwise as [SKU Wise], Sampling from TSPL_Discount_Master"
        ListImpExpColumnsMandatory = New List(Of String)({"Discount Category Code", "Discount Code", "Description", "Account Code", "Account Description"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"Discount Code"})
        transportSql.ExporttoExcel(str, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
    End Sub

    Private Sub FrmDiscountMaster_KeyDown_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Private Sub txtGLAccount_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtGLAccount.Load

    End Sub

    Private Sub txtGLAccount__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtGLAccount._MYValidating
        Dim qry As String = "select Account_Code as [Account],Description from tspl_gl_accounts"
        txtGLAccount.Value = clsCommon.ShowSelectForm("AditioCHRGFND", qry, "Account", "", txtGLAccount.Value, "Account", isButtonClicked)
        ''BHA/16/05/18-000025 by priti on 24/07/2018 remove ControlAccount ='Y'  becuase no condition required of control account.
        ''BHA/16/05/18-000025 by balwinder on 09/07/2018 Change ControlAccount <>'Y' to ControlAccount ='Y' becuase account shoule be of control account.
        qry = "select Description from tspl_gl_accounts where Account_Code ='" + txtGLAccount.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            txtGlAccDesc.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
        Else
            txtGlAccDesc.Text = ""
        End If

    End Sub

    Private Sub txtdiscountcategory__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtdiscountcategory._MYValidating
        Dim qry As String = "select Code as [DiscountCategroyCode],Description from TSPL_Discount_Category_Master"
        txtdiscountcategory.Value = clsCommon.ShowSelectForm("DiscountCatFND", qry, "DiscountCategroyCode", "", txtdiscountcategory.Value, "DiscountCategroyCode", isButtonClicked)
        ''lblVendorName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Name from TSPL_VENDOR_MASTER where Vendor_Code='" + txtVendorNo.Value + "'"))
        qry = "select Description from TSPL_Discount_Category_Master where Code ='" + txtdiscountcategory.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            txtCategoryDesc.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
        Else
            txtCategoryDesc.Text = ""
        End If
    End Sub

End Class
