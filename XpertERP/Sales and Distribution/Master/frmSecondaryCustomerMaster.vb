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

Public Class FrmSecondaryCustomerMaster
    Inherits FrmMainTranScreen

#Region "Variable"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False
    Const ColLocation As String = "ColLocation"
    Const ColOpeningDate As String = "ColOpeningDate"
    Const ColOpeningQty As String = "ColOpeningQty"
    Const CatcolCode As String = "CatcolCode"
    Const CatcolCodeDesc As String = "CatcolCodeDesc"
    Const CatcolValue As String = "CatcolValue"
    Const CatcolValueDesc As String = "CatcolValueDesc"
    Const colLocationName As String = "LOCATIONNAME"
    Const colLineNo As String = "ColLineNo"
    Const colSelect As String = "SELECT"
    Const colCompCode As String = "COMPCODE"
    Const colCompName As String = "COMPNAME"
    Const colDataBaseName As String = "DATABASE"
    Const colitemno As String = "itemCode"
    Const coldesc As String = "itemName"
    Const coluom As String = "UOM"

    Const ColItemValidUpto As String = "VAIDUPTO"
    Const ColItemStartDate As String = "Start Date"

    Private isCellValueChangedOpen As Boolean = False
    Private isNewEntry As Boolean = True
    Dim userCode, companyCode As String
    Dim CustId As String
    Dim Custname As String

    Public DrillDown_transType As String = Nothing
    Public DrillDown_FormName As String = Nothing
    Public isDisplayFranchiseDetails As Boolean = False
    Public isCheckCustomerType As Boolean = False
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

        'MyBase.SetUserMgmt(clsUserMgtCode.SecondaryCustomerMaster)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        funNew()
    End Sub

    Public Sub funNew()


        LoadBlankGrid()

        Me.fndZone.Value = ""
        Me.txtAliesName.Text = ""

        Me.fndCustomer.Value = ""
        Me.fndCusCategory.Value = ""
        Me.fndChannel.Value = ""
        Me.fndCusgrp.Value = ""
        Me.fndCusType.Value = ""
        Me.fndRoute.Value = ""
        Me.fndTrmsCode.Value = ""
        Me.fndAccntSet.Value = ""
        Me.fndTxGrp.Value = ""
        Me.txtTxGrp.Text = ""
        Me.fndSalePerson.Value = ""

        Me.fndPayCode.Value = ""
        Me.fndCity.Value = ""
        Me.txtSalesPerson.Text = ""
        Me.txtCustomerName.Text = ""
        Me.dtpAggMade.Value = Nothing
        Me.dtpAggClose.Value = Nothing
        Me.txtCusgrp.Text = ""
        Me.txtAdd1.Text = ""
        Me.txtAdd2.Text = ""
        Me.txtAdd3.Text = ""
        Me.txtPinNo.Text = ""
        Me.dtClosing.Value = connectSql.serverDate()
        Me.txtRoute.Text = ""
        chkHold.Checked = False
        chkInActive.Checked = False
        chkInActive.Enabled = False
        chkcredit.Checked = False
        Me.txtPriceCode.Value = ""
        Me.fndstate.Value = ""
        Me.fndCountry.Value = ""
        Me.TxtCountryName.Text = ""
        Me.txtPhone1.Text = "(+__)__________"
        Me.txtPhone2.Text = "(+__)__________"
        Me.txtfax.Text = ""
        Me.txtEmail.Text = ""
        Me.txtTinNo.Text = ""
        Me.drpformtype.SelectedIndex = 0
        Me.txtWeb.Text = ""
        Me.txtContactName.Text = ""
        Me.txtContPhone.Text = "(+__)__________"
        Me.txtContactFax.Text = ""
        Me.txtContactWeb.Text = ""
        Me.txtContactEmail.Text = ""
        Me.grdTax.DataSource = Nothing
        Me.grdTax.Rows.Clear()
        Me.txtStaxNo.Text = ""
        Me.txtLstNo.Text = ""
        Me.txtChannel.Text = ""
        Me.txtRemarks1.Text = ""
        Me.txtRemarks2.Text = ""
        Me.txtAddInfo1.Text = ""
        Me.txtAddInfo2.Text = ""
        Me.txtAddInfo3.Text = ""

        Me.txtroutegroup.Text = ""
        Me.txtCredit.Text = "0.00"
        Me.txtcst.Text = ""
        Me.txtecc.Text = ""
        Me.txtrange.Text = ""
        Me.txtcollect.Text = ""
        Me.txtpan.Text = ""
        Me.txtdivision.Text = ""

        Me.txtPriceCodeNon.Value = ""
        Me.fndroutegroup.Value = ""

        Me.txtTempCreditLimit.Text = "0.00"
        txttempCreditLimitFrom.Text = clsCommon.GETSERVERDATE()
        txttempCreditLimitTo.Text = clsCommon.GETSERVERDATE()
        ChkCheckCreditLimit.Checked = False
        btnSave.Text = "Save"
        btnDelete.Enabled = False
        LoadCustomerType()
        fndCustomer.MyReadOnly = False
        cboCustomerClass.Text = "Select"
        txtStateName.Text = ""
        txtCity.Text = ""
        ChkOther.Checked = False
        CmbTransaction.SelectedValue = ""
        CmbCustomerType.SelectedValue = ""
        SetDataBaseGrid()


        txtCredit.Enabled = True
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.SetDefaultValues()
        End If
        UcAttachment1.BlankAllControls()
        FillCustName()
        btnSave.Text = "Save"
        btnDelete.Enabled = False
        isNewEntry = True
        txtParentCstmrNo.Text = ""
        CmbCustomerType.Text = ""
        txtParentCstNo.Value = ""

    End Sub

    Sub LoadCustomerType()
        Dim dt As DataTable = New DataTable()
        Dim strcusttype As String = "select Cust_Type_Desc from TSPL_CUSTOMER_TYPE_MASTER"
        dt = clsDBFuncationality.GetDataTable(strcusttype)
        cboCustomerClass.DataSource = dt
        cboCustomerClass.ValueMember = "Cust_Type_Desc"
        cboCustomerClass.DisplayMember = "Cust_Type_Desc"
    End Sub

    Private Function GetItemType(Optional ByVal istype As Boolean = False) As DataTable
        Dim dt As New DataTable()
        If istype = True Then
            dt.Columns.Add("Code", GetType(String))

            Dim dr As DataRow = dt.NewRow()
            dr("Code") = "New"
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Code") = "Refurnished"
            dt.Rows.Add(dr)
        Else
            dt.Columns.Add("Code", GetType(String))

            Dim dr As DataRow = dt.NewRow()
            dr("Code") = "Yes"
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Code") = "No"
            dt.Rows.Add(dr)
        End If

        Return dt
    End Function

    Public Sub FillCustName()
        Try
            Dim col As New AutoCompleteStringCollection
            Dim strquery As String = "Select Customer_Name  From TSPL_SECONDARY_CUSTOMER_MASTER "
            Dim ds As DataTable
            'Dim strvalue As String
            ds = clsDBFuncationality.GetDataTable(strquery)
            Dim comp As Integer
            For comp = 0 To ds.Rows.Count - 1
                col.Add(ds.Rows(comp).Item(0))

            Next
            txtCustomerName.AutoCompleteSource = AutoCompleteSource.CustomSource
            txtCustomerName.AutoCompleteCustomSource = col
            txtCustomerName.AutoCompleteMode = AutoCompleteMode.Suggest

        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
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
        repoDB.Width = 100
        repoDB.IsVisible = True
        repoDB.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoDB)

        Dim qry As String = "SELECT Comp_Code,Comp_Name,DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0"
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


    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'SaveData()
        Try

            'If clsCommon.myLen(fndCustomer.Value) <= 0 Then
            '    myMessages.blankValue("Customer No")
            '    fndCustomer.Focus()
            '    Return
            'End If


            If clsCommon.myLen(txtCustomerName.Text) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please fill customer name", "Customer", MessageBoxButtons.OK)
                pageCus.SelectedPage = RadPageViewPage1
                txtCustomerName.Focus()
                Return
            End If
            If clsCommon.myLen(fndCusgrp.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Select Customer Group", "Customer", MessageBoxButtons.OK)
                pageCus.SelectedPage = RadPageViewPage1
                fndCusgrp.Focus()
                Return
            End If
            If clsCommon.myLen(fndCusCategory.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Select Customer Catageory ", "Customer", MessageBoxButtons.OK)
                pageCus.SelectedPage = RadPageViewPage3
                fndCusCategory.Focus()
                Return
            End If

            If clsCommon.myLen(fndAccntSet.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Select Customer Account Set", "Customer", MessageBoxButtons.OK)
                pageCus.SelectedPage = RadPageViewPage4
                fndAccntSet.Focus()
                Return
            End If
            Dim arrChecked As List(Of String) = New List(Of String)
            For jj As Integer = 0 To gvDB.Rows.Count - 1
                If (clsCommon.myCBool(gvDB.Rows(jj).Cells(colSelect).Value)) Then
                    arrChecked.Add(clsCommon.myCstr(gvDB.Rows(jj).Cells(colDataBaseName).Value))
                End If
            Next

            If arrChecked Is Nothing OrElse arrChecked.Count = 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Company Under Additional Info Tab")
                pageCus.SelectedPage = RadPageViewPage5
                Return
            End If
            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.SecondaryCustomerMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return
                End If
            End If


            SaveSecondaryCustomerMaster()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub SaveData()
        '' Anubhooti 02-Sep-2014
        Dim AllowAutoCCode As String
        AllowAutoCCode = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AutoGeneratedCustomerCode, clsFixedParameterCode.AutoGeneratedCustomerCode, Nothing))
        If clsCommon.CompairString(AllowAutoCCode, "0") = CompairStringResult.Equal Then
            If fndCustomer.Value <> "" Then
                If clsCommon.myLen(txtCustomerName.Text) > 0 Then

                    If fndCusgrp.Value <> "" Then
                        If fndCusCategory.Value <> "" Then
                            If fndAccntSet.Value <> "" Then
                                'Anand on 22/09/2014
                                'If clsCommon.myLen(clsCommon.myCstr(CmbTransaction.SelectedValue)) > 0 Then
                                If AllowToSave() Then
                                    funInsert()
                                End If
                                'Else
                                '    common.clsCommon.MyMessageBoxShow("Please Select Transaction Type", "Customer", MessageBoxButtons.OK)
                                '    CmbTransaction.Focus()
                                'End If
                            Else
                                common.clsCommon.MyMessageBoxShow("Select Customer Account Set", "Customer", MessageBoxButtons.OK)
                                pageCus.SelectedPage = RadPageViewPage4
                                fndAccntSet.Focus()
                            End If
                        Else

                            common.clsCommon.MyMessageBoxShow("Select Customer Catageory ", "Customer", MessageBoxButtons.OK)
                            pageCus.SelectedPage = RadPageViewPage3
                            fndCusCategory.Focus()
                        End If
                    Else
                        common.clsCommon.MyMessageBoxShow("Select Customer Group", "Customer", MessageBoxButtons.OK)
                        pageCus.SelectedPage = RadPageViewPage1
                        fndCusgrp.Focus()
                    End If
                Else
                    common.clsCommon.MyMessageBoxShow("Please fill customer name", "Customer", MessageBoxButtons.OK)
                    pageCus.SelectedPage = RadPageViewPage1
                    txtCustomerName.Focus()
                End If
            ElseIf clsCommon.myLen(fndCustomer.Value) <= 0 Then
                myMessages.blankValue("Customer No")
                fndCustomer.Focus()
                Return
            Else
                Return
            End If
        Else
            If clsCommon.myLen(txtCustomerName.Text) > 0 Then
                If fndCusgrp.Value <> "" Then

                    If fndAccntSet.Value <> "" Then
                        'Anand on 22/09/2014
                        'If clsCommon.myLen(clsCommon.myCstr(CmbTransaction.SelectedValue)) > 0 Then
                        If AllowToSave() Then
                            funInsert()
                        End If
                        'Else
                        '    common.clsCommon.MyMessageBoxShow("Please Select Transaction Type", "Customer", MessageBoxButtons.OK)
                        '    CmbTransaction.Focus()
                        'End If
                    Else
                        common.clsCommon.MyMessageBoxShow("Select Customer Account Set", "Customer", MessageBoxButtons.OK)
                        pageCus.SelectedPage = RadPageViewPage4
                        fndAccntSet.Focus()
                    End If

                Else
                    common.clsCommon.MyMessageBoxShow("Select Customer Group", "Customer", MessageBoxButtons.OK)
                    pageCus.SelectedPage = RadPageViewPage1
                    fndCusgrp.Focus()
                End If
            Else
                common.clsCommon.MyMessageBoxShow("Please fill customer name", "Customer", MessageBoxButtons.OK)
                pageCus.SelectedPage = RadPageViewPage1
                txtCustomerName.Focus()
            End If
            'ElseIf clsCommon.myLen(fndCustomer.Value) <= 0 Then
            '    myMessages.blankValue("Customer No")
            '    fndCustomer.Focus()
            '    Return
            'Else
            '    Return
        End If

    End Sub

    Function AllowToSave() As Boolean
        Try
            If CmbTransaction.SelectedValue = "T" AndAlso clsCommon.myLen(txtTinNo.Text) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Fill the Tin No")
                pageCus.SelectedPage = RadPageViewPage4
                txtTinNo.Focus()
                Return False

            End If
            If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.CustomerNameUniqueOnCM & "' and Type ='" & clsFixedParameterType.CustomerNameUniqueOnCM & "'")), "0") = CompairStringResult.Equal Then
                Dim dt1 As DataTable
                dt1 = clsDBFuncationality.GetDataTable("Select * From TSPL_SECONDARY_CUSTOMER_MASTER Where (((ISNULL( ECC,'')='" & txtecc.Text.Trim() & "' and LEN(ISNULL( ECC,'')) > 0))  or ((ISNULL(Email,'')='" & txtEmail.Text.Trim() & "' ANd ISNULL(Email,'')<>'' )) or ((ISNULL(Tin_No,'')='" & txtTinNo.Text.Trim() & "' AND ISNULL(Tin_No,'')<>'' )) or ((ISNULL(Contact_Person_Email,'')='" & txtContactEmail.Text.Trim() & "' ANd ISNULL(Contact_Person_Email,'')<>'' )) ) and (TSPL_SECONDARY_CUSTOMER_MASTER.Cust_Code not in ('" & fndCustomer.Value.Trim() & "'))")
                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    If common.clsCommon.MyMessageBoxShow("Customer exists with same customer description.Do you still want to continue ", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                    Else
                        Exit Function
                    End If
                End If
            End If


            If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.CustomerNameUniqueOnCM & "' and Type ='" & clsFixedParameterType.CustomerNameUniqueOnCM & "'")), "1") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select count(*) from TSPL_SECONDARY_CUSTOMER_MASTER where Customer_Name='" & clsCommon.myCstr(txtCustomerName.Text) & "' and Cust_Code<>'" & clsCommon.myCstr(fndCustomer.Value) & "'")) > 0 Then
                    common.clsCommon.MyMessageBoxShow("Same Customer Name is exist with another customer so please change customer name because Customer Name is unique.")
                    Return False
                End If
            End If

            Dim arrChecked As List(Of String) = New List(Of String)
            For jj As Integer = 0 To gvDB.Rows.Count - 1
                If (clsCommon.myCBool(gvDB.Rows(jj).Cells(colSelect).Value)) Then
                    arrChecked.Add(clsCommon.myCstr(gvDB.Rows(jj).Cells(colDataBaseName).Value))
                End If
            Next

            If arrChecked Is Nothing OrElse arrChecked.Count = 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Company Under Additional Info Tab")
                pageCus.SelectedPage = RadPageViewPage5
                Return False
            End If










            '' validation for multicurrency
            If clsCommon.myLen(clsCommon.myCstr(fndCustCurrency.Value)) > 0 Then
                If clsCommon.myLen(Me.fndAccntSet.Value) <= 0 Then
                    clsCommon.MyMessageBoxShow("Select Account Set. Under Process Tab")
                    Me.fndAccntSet.Focus()
                    Return False
                End If


                Dim qry As String
                qry = "select CURRENCY_CODE from TSPL_CUSTOMER_ACCOUNT_SET where Cust_Account='" & clsCommon.myCstr(Me.fndAccntSet.Value) & "' "
                Dim accCurrCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                If clsCommon.CompairString(accCurrCode, clsCommon.myCstr(Me.fndCustCurrency.Value)) <> CompairStringResult.Equal Then
                    clsCommon.MyMessageBoxShow("Account Set Currency and Customer Currency must be same in case of Multicurrency.")
                    Return False
                End If
                '' match tax Group currency with vendor currency
                qry = " select TSPL_TAX_GROUP_DETAILS.Tax_Code,coalesce(TSPL_TAX_MASTER.CURRENCY_CODE,'') as CURRENCY_CODE from TSPL_TAX_GROUP_DETAILS " & _
                      " inner join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_DETAILS.Tax_Group_Code=TSPL_TAX_GROUP_MASTER.Tax_Group_Code " & _
                      " inner join TSPL_TAX_MASTER on TSPL_TAX_GROUP_DETAILS.Tax_Code=TSPL_TAX_MASTER.Tax_Code where TSPL_TAX_GROUP_MASTER.Tax_Group_Code='" & clsCommon.myCstr(Me.fndTxGrp.Value) & "' " & _
                      " and coalesce(TSPL_TAX_MASTER.CURRENCY_CODE,'')<>'" & clsCommon.myCstr(Me.fndCustCurrency.Value) & "'"
                Dim dt As DataTable
                dt = clsDBFuncationality.GetDataTable(qry)
                Dim taxCode As String = ""
                For Each dr As DataRow In dt.Rows
                    If dt.Rows.IndexOf(dr) = 0 Then
                        taxCode = dr.Item("Tax_Code")
                    Else
                        taxCode = taxCode & "," & dr.Item("Tax_Code")
                    End If
                Next
                If clsCommon.myLen(taxCode) > 0 Then
                    clsCommon.MyMessageBoxShow("Tax Code '" & taxCode & "' in Tax Group " & clsCommon.myCstr(Me.fndTxGrp.Value) & " are created for currency other than " & clsCommon.myCstr(Me.fndCustCurrency.Value) & " .")
                    Exit Function
                End If
                'End If
            End If




            If clsCommon.myCdbl(txtTempCreditLimit.Text) > 0 Then
                If txttempCreditLimitTo.Value.Date < txttempCreditLimitFrom.Value.Date Then
                    clsCommon.MyMessageBoxShow("Temp Credit Limit To can't be less than from Temp Credit Limit From", Me.Text)
                    txttempCreditLimitTo.Focus()
                    txttempCreditLimitTo.Select()
                    Return False
                End If
            End If
            ''------------------------------------------

            '=============BM00000003721==============By Monika
            If clsCommon.myLen(txtpan.Text) > 0 Then
                If clsCommon.myLen(txtpan.Text) < 10 Then
                    clsCommon.MyMessageBoxShow("PAN number should have max. 10 characters.", Me.Text)
                    txtpan.Focus()
                    txtpan.Select()
                    Return False
                End If
                If ChkOther.Checked = False Then
                    Dim msg As String = clsERPFuncationality.CheckPanStructure(txtpan.Text.Trim(), txtCustomerName.Text)
                    If clsCommon.myLen(msg) > 10 Then
                        clsCommon.MyMessageBoxShow(msg, Me.Text)
                        txtpan.Focus()
                        txtpan.Select()
                        Return False
                    End If
                End If
            End If


            Dim strFinancialDat As Date = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Type='ERPStartDate'"))
            For intCount As Integer = 0 To gvCrate.Rows.Count - 1
                Dim strLocation As String = clsCommon.myCstr(gvCrate.Rows(intCount).Cells(ColLocation).Value)
                If clsCommon.myLen(strLocation) > 0 Then
                    Dim strOpeningDate As Date = clsCommon.myCDate(gvCrate.Rows(intCount).Cells(ColOpeningDate).Value)
                    If clsCommon.myCDate(strOpeningDate) > clsCommon.myCDate(strFinancialDat) Then
                        Throw New Exception("Date cannot be greater than Financial Date")
                    End If
                End If
                If clsCommon.myLen(clsCommon.myCstr(gvCrate.Rows(intCount).Cells(ColLocation).Value)) <= 0 And (clsCommon.myLen(clsCommon.myCstr(gvCrate.Rows(intCount).Cells(ColOpeningDate).Value)) > 0 Or clsCommon.myCdbl(gvCrate.Rows(intCount).Cells(ColOpeningQty).Value) > 0) Then
                    Throw New Exception("Please Fill Location in Crate Accounting at Line no " & intCount + 1 & "")
                End If
                If clsCommon.myLen(clsCommon.myCstr(gvCrate.Rows(intCount).Cells(ColOpeningDate).Value)) <= 0 And (clsCommon.myLen(clsCommon.myCstr(gvCrate.Rows(intCount).Cells(ColLocation).Value)) > 0 Or clsCommon.myCdbl(gvCrate.Rows(intCount).Cells(ColOpeningQty).Value) > 0) Then
                    Throw New Exception("Please Fill Opening Date in Crate Accounting at Line no " & intCount + 1 & "")
                End If
                'If clsCommon.myCdbl(gvCrate.Rows(intCount).Cells(ColOpeningQty).Value) <= 0 And (clsCommon.myLen(clsCommon.myCstr(gvCrate.Rows(intCount).Cells(ColLocation).Value)) > 0 Or clsCommon.myLen(clsCommon.myCstr(gvCrate.Rows(intCount).Cells(ColOpeningDate).Value)) > 0) Then
                '    Throw New Exception("Please Fill Opening Qty in Crate Accounting at Line no " & intCount + 1 & "")
                'End If  '' Opening Qty can be negative 
                If (clsCommon.myLen(clsCommon.myCstr(gvCrate.Rows(intCount).Cells(ColOpeningQty).Value)) <= 0 OrElse clsCommon.myCdbl(gvCrate.Rows(intCount).Cells(ColOpeningQty).Value) = 0) And (clsCommon.myLen(clsCommon.myCstr(gvCrate.Rows(intCount).Cells(ColLocation).Value)) > 0 Or clsCommon.myLen(clsCommon.myCstr(gvCrate.Rows(intCount).Cells(ColOpeningDate).Value)) > 0) Then
                    Throw New Exception("Please Fill Opening Qty in Crate Accounting at Line no " & intCount + 1 & "")
                End If
            Next
            '===================================================

            UcCustomFields1.AllowToSave()
            Return True

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try

    End Function

    Public Sub funInsert()
        Try
            Dim AllowCustCode As String = ""
            Dim AutoCustCode As String = ""
            Dim CustName As String = ""

            If btnSave.Text = "Save" Then
                isNewEntry = True

                AllowCustCode = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AutoGeneratedCustomerCode, clsFixedParameterCode.AutoGeneratedCustomerCode, Nothing))
                If clsCommon.CompairString(AllowCustCode, "1") = CompairStringResult.Equal AndAlso clsCommon.myLen(fndCustomer.Value.Trim()) <= 0 Then
                    If clsCommon.myLen(txtCustomerName.Text) > 0 Then
                        CustName = txtCustomerName.Text.Substring(0, 1)
                        AutoCustCode = GetVendorNextCode("TSPL_SECONDARY_CUSTOMER_MASTER", "Customer_Name", CustName, Nothing)
                        'AutoCustCode = clsERPFuncationality.GetVendorNextCode("TSPL_SECONDARY_CUSTOMER_MASTER", "Customer_Name", CustName, Nothing)
                        fndCustomer.Value = AutoCustCode
                    Else
                        CustName = ""
                        AutoCustCode = GetVendorNextCode("TSPL_SECONDARY_CUSTOMER_MASTER", "Customer_Name", CustName, Nothing)
                        'AutoCustCode = clsERPFuncationality.GetVendorNextCode("TSPL_SECONDARY_CUSTOMER_MASTER", "Customer_Name", CustName, Nothing)
                        fndCustomer.Value = AutoCustCode
                    End If
                Else
                    fndCustomer.Value = fndCustomer.Value
                End If
            Else
                isNewEntry = False
                fndCustomer.Value = fndCustomer.Value
            End If
            Dim obj As New clsCustomerMaster()
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '' Anubhooti 02-Sep-2014 AutoCode based on settings



            obj.Cust_Code = clsCommon.myCstr(fndCustomer.Value)
            obj.Customer_Name = clsCommon.myCstr(txtCustomerName.Text)
            obj.Alies_Name = clsCommon.myCstr(txtAliesName.Text)
            obj.Zone_Code = clsCommon.myCstr(fndZone.Value)
            obj.Agg_Made_Date = dtpAggMade.Value
            obj.Agg_Close_Date = dtpAggClose.Value
            obj.Add1 = clsCommon.myCstr(txtAdd1.Text)
            obj.Add2 = clsCommon.myCstr(txtAdd2.Text)
            obj.Add3 = clsCommon.myCstr(txtAdd3.Text)
            obj.Cust_Category_Code = clsCommon.myCstr(fndCusCategory.Value)
            obj.Cust_Group_Code = clsCommon.myCstr(fndCusgrp.Value)
            obj.Cust_Type_Code = clsCommon.myCstr(fndCusType.Value)
            obj.Route_No = clsCommon.myCstr(fndRoute.Value)
            obj.City_Code = clsCommon.myCstr(fndCity.Value)
            obj.State = clsCommon.myCstr(fndstate.Value)

            If clsCommon.myCdbl(txtTempCreditLimit.Text) > 0 Then
                obj.TempCreditLimit = clsCommon.myCdbl(txtTempCreditLimit.Text)
                obj.TempCreditLimitFrom = clsCommon.myCDate(txttempCreditLimitFrom.Text)
                obj.TempCreditLimitTo = clsCommon.myCDate(txttempCreditLimitTo.Text)
            End If
            ''--------------------------------------------
            ''richa 12/09/2014
            If ChkCheckCreditLimit.Checked = True Then
                obj.CheckCreditLimit = 1
            Else
                obj.CheckCreditLimit = 0
            End If
            ''============================
            'Dim Other_For_Pan As Integer = 0
            If ChkOther.Checked = True Then
                obj.Other_For_PAN = 1
            Else
                obj.Other_For_PAN = 0
            End If
            ''richa 
            '' Remove these fields 09-June-2015 (Amit Sir)
            'obj.Crate_Opening = clsCommon.myCdbl(TxtCrateOpeningQty.Value)
            'If TxtCrateOpeningDate.Checked = True Then
            '    obj.Crate_Opening_Date = TxtCrateOpeningDate.Value
            'Else
            '    obj.Crate_Opening_Date = Nothing
            'End If
            ''----------------
            '' Anubhooti 26-Aug-2014 BM00000003619
            'obj.Country = clsCommon.myCstr(txtCountry.Text)
            obj.Country = clsCommon.myCstr(fndCountry.Value)

            obj.Phone1 = clsCommon.myCstr(txtPhone1.Text)
            obj.Phone2 = clsCommon.myCstr(txtPhone2.Text)
            obj.Fax = clsCommon.myCstr(txtfax.Text)
            obj.Email = clsCommon.myCstr(txtEmail.Text)
            obj.WebSite = clsCommon.myCstr(txtWeb.Text)
            obj.Contact_Person_Name = clsCommon.myCstr(txtContactName.Text)
            obj.Contact_Person_Phone = clsCommon.myCstr(txtContPhone.Text)
            obj.Contact_Person_Fax = clsCommon.myCstr(txtContactFax.Text)
            obj.Contact_Person_Email = clsCommon.myCstr(txtContactEmail.Text)
            obj.Contact_Person_Website = clsCommon.myCstr(txtContactWeb.Text)
            obj.Terms_Code = clsCommon.myCstr(fndTrmsCode.Value)
            obj.Cust_Account = clsCommon.myCstr(fndAccntSet.Value)
            obj.Tax_Group = clsCommon.myCstr(fndTxGrp.Value)
            If (grdTax.Rows.Count > 0) Then
                obj.TAX1 = clsCommon.myCstr(grdTax.Rows(0).Cells(0).Value)
                obj.TAX1_Rate = clsCommon.myCdbl(grdTax.Rows(0).Cells(1).Value)
            End If
            If (grdTax.Rows.Count > 1) Then
                obj.TAX2 = clsCommon.myCstr(grdTax.Rows(1).Cells(0).Value)
                obj.TAX2_Rate = clsCommon.myCdbl(grdTax.Rows(1).Cells(1).Value)
            End If
            If (grdTax.Rows.Count > 2) Then
                obj.TAX3 = clsCommon.myCstr(grdTax.Rows(2).Cells(0).Value)
                obj.TAX3_Rate = clsCommon.myCdbl(grdTax.Rows(2).Cells(1).Value)
            End If
            If (grdTax.Rows.Count > 3) Then
                obj.TAX4 = clsCommon.myCstr(grdTax.Rows(3).Cells(0).Value)
                obj.TAX4_Rate = clsCommon.myCdbl(grdTax.Rows(3).Cells(1).Value)
            End If
            If (grdTax.Rows.Count > 4) Then
                obj.TAX5 = clsCommon.myCstr(grdTax.Rows(4).Cells(0).Value)
                obj.TAX5_Rate = clsCommon.myCdbl(grdTax.Rows(4).Cells(1).Value)
            End If
            If (grdTax.Rows.Count > 5) Then
                obj.TAX6 = clsCommon.myCstr(grdTax.Rows(5).Cells(0).Value)
                obj.TAX6_Rate = clsCommon.myCdbl(grdTax.Rows(5).Cells(1).Value)
            End If
            If (grdTax.Rows.Count > 6) Then
                obj.TAX7 = clsCommon.myCstr(grdTax.Rows(6).Cells(0).Value)
                obj.TAX7_Rate = clsCommon.myCdbl(grdTax.Rows(6).Cells(1).Value)
            End If
            If (grdTax.Rows.Count > 7) Then
                obj.TAX8 = clsCommon.myCstr(grdTax.Rows(7).Cells(0).Value)
                obj.TAX8_Rate = clsCommon.myCdbl(grdTax.Rows(7).Cells(1).Value)
            End If
            If (grdTax.Rows.Count > 8) Then
                obj.TAX9 = clsCommon.myCstr(grdTax.Rows(8).Cells(0).Value)
                obj.TAX9_Rate = clsCommon.myCdbl(grdTax.Rows(8).Cells(1).Value)
            End If
            If (grdTax.Rows.Count > 9) Then
                obj.TAX10 = clsCommon.myCstr(grdTax.Rows(9).Cells(0).Value)
                obj.TAX10_Rate = clsCommon.myCdbl(grdTax.Rows(9).Cells(1).Value)
            End If
            obj.Payment_Code = clsCommon.myCstr(fndPayCode.Value)
            obj.Service_Tax_No = clsCommon.myCstr(txtStaxNo.Text)
            obj.Tin_No = clsCommon.myCstr(txtTinNo.Text)
            obj.Lst_No = clsCommon.myCstr(txtLstNo.Text)
            obj.Form_Type = clsCommon.myCstr(drpformtype.Text)
            obj.Channel_Code = clsCommon.myCstr(fndChannel.Value)

            If chkInActive.Checked = True Then
                obj.Status = "Y"    '******* for:In-Active ********
                obj.Closing_Date = clsCommon.GetPrintDate(dtClosing.Value, "dd/MM/yyyy")
            ElseIf chkInActive.Checked = False Then
                obj.Status = "N"                    '******* for:Active ******** 
                obj.Closing_Date = Nothing
            End If
            If chkHold.Checked = True Then
                obj.OnHold = "Y"                      '******* for:Hold ******** 
            ElseIf chkHold.Checked = False Then
                obj.OnHold = "N"                      '******* for:Remove Hold ********
            End If
            '=========shivani
            obj.PIN_NO = clsCommon.myCstr(txtPinNo.Text)
            '============
            obj.Remarks1 = clsCommon.myCstr(txtRemarks1.Text)
            obj.Remarks2 = clsCommon.myCstr(txtRemarks2.Text)
            obj.Additional1 = clsCommon.myCstr(txtAddInfo1.Text)
            obj.Additional2 = clsCommon.myCstr(txtAddInfo2.Text)
            obj.Additional3 = clsCommon.myCstr(txtAddInfo3.Text)
            obj.Salesman_Code = clsCommon.myCstr(fndSalePerson.Value)
            obj.OutLet_Commossion = clsCommon.myCdbl(0) '--default 0
            obj.Balance_ToDate = 0                      '--Default 0
            obj.Credit_Limit = clsCommon.myCdbl(txtCredit.Text)
            obj.Route_Group = clsCommon.myCstr(fndroutegroup.Value)
            obj.CST = clsCommon.myCstr(txtcst.Text)
            obj.ECC = clsCommon.myCstr(txtecc.Text)
            obj.Range = clsCommon.myCstr(txtrange.Text)
            obj.Collectorate = clsCommon.myCstr(txtcollect.Text)
            obj.PAN = clsCommon.myCstr(txtpan.Text)
            obj.Division = clsCommon.myCstr(txtdivision.Text)

            obj.Customer_Class = clsCommon.myCstr(fndCusType.Value)
            If chkcredit.Checked = True Then
                obj.Credit_Customer = "Y"
            ElseIf chkcredit.Checked = False Then
                obj.Credit_Customer = "N"
            End If
            obj.LastInvoice_No = Nothing
            obj.LastInvoice_Date = Nothing
            obj.Price_Code = clsCommon.myCstr(txtPriceCode.Value)
            obj.Price_CodeNon = clsCommon.myCstr(txtPriceCodeNon.Value)
            obj.Price_Group_Code = clsCommon.myCstr(txtpgfnd.Value)






            obj.Transaction_Type = CmbTransaction.SelectedValue
            obj.Comp_Code = objCommonVar.CurrentCompanyCode
            '--------------------------------------------------------------------------Pass dataBase Name in Array

            Dim arrDBName As New List(Of String)
            For ii As Integer = 0 To gvDB.Rows.Count - 1
                If (clsCommon.myCBool(gvDB.Rows(ii).Cells(colSelect).Value)) Then
                    arrDBName.Add(clsCommon.myCstr(gvDB.Rows(ii).Cells(colDataBaseName).Value))
                End If
            Next
            '---------------------------------------------------------------------------------------

            '-----------------------------------------------
            obj.Arr_CrateAccount = New List(Of clsCustomerCrateAccounting)
            For Each gr As GridViewRowInfo In gvCrate.Rows
                Dim objTr As New clsCustomerCrateAccounting()
                objTr.Line_No = clsCommon.myCdbl(gr.Cells(colLineNo).Value)
                objTr.Location_Code = clsCommon.myCstr(gr.Cells(ColLocation).Value)
                If clsCommon.myLen(gr.Cells(ColOpeningDate).Value) > 0 Then
                    objTr.Crate_Opening_Date = clsCommon.GetPrintDate(clsCommon.myCDate(gr.Cells(ColOpeningDate).Value), "dd/MMM/yyyy")
                End If
                objTr.Crate_Opening = clsCommon.myCdbl(gr.Cells(ColOpeningQty).Value)
                If (clsCommon.myLen(gr.Cells(ColLocation).Value) > 0) Then
                    obj.Arr_CrateAccount.Add(objTr)
                End If
            Next

            '---------------------------------------------------------------------



            ''For Custom Fields
            obj.Form_ID = MyBase.Form_ID
            obj.arrCustomFields = New List(Of clsCustomFieldValues)
            If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                UcCustomFields1.GetData(obj.arrCustomFields)
            End If
            ''End of For Custom Fields
            '' FOR Multicurrency
            If Me.fndCustCurrency.Visible = True Then
                obj.CURRENCY_CODE = clsCommon.myCstr(Me.fndCustCurrency.Value)
            Else
                obj.CURRENCY_CODE = Nothing
            End If

            '' end of multicurrency

            '' Anubhooti 01-Sep-2014 BM00000003425  ******************* Check Outstanding Amount Of customer *************
            Dim QryToGetOutAmt As String = ""
            Dim OutStandAmt As Double = 0

            QryToGetOutAmt = " Select [Customer Id], MAX(TSPL_SECONDARY_CUSTOMER_MASTER.Customer_Name) as Customer_Name, SUM([Due Amount]) AS [Due Amount] from (  " & _
 " SELECT  TSPL_SECONDARY_CUSTOMER_MASTER.Cust_Code AS [Customer Id], TSPL_SD_SALE_INVOICE_HEAD.Document_Code as [Document Id], case when TSPL_Customer_Invoice_Head.Document_Type ='I' then TSPL_Customer_Invoice_Head.Document_Total  when TSPL_Customer_Invoice_Head.Document_Type ='D' then TSPL_Customer_Invoice_Head.Document_Total  when TSPL_Customer_Invoice_Head.Document_Type ='C' then TSPL_Customer_Invoice_Head.Document_Total*-1  end as [Due Amount], CONVERT(DATE,TSPL_Customer_Invoice_Head.Document_Date,103) as [Document Date], case when TSPL_Customer_Invoice_Head.Document_Type ='I' then 'IN'  when TSPL_Customer_Invoice_Head.Document_Type ='D' then 'DB'  when TSPL_Customer_Invoice_Head.Document_Type ='C' then 'CR' end  as [Document_Type] FROM  TSPL_Customer_Invoice_Head INNER JOIN   TSPL_SECONDARY_CUSTOMER_MASTER ON TSPL_Customer_Invoice_Head.Customer_Code  = TSPL_SECONDARY_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_Customer_Invoice_Head.Against_Sale_No where TSPL_Customer_Invoice_Head.Status='1'  AND TSPL_SECONDARY_CUSTOMER_MASTER.Status='N'  " & _
  "          UNION ALL " & _
" SELECT TSPL_SALE_RETURN_INTER_HEAD.Cust_Code AS [Customer Id] ,Document_No as [Document Id], (Total_Order_Amt)*-1 as [Due Amount] , CONVERT(DATE,Document_Date,103) as [Document Date]  , 'SR' as [Document_Type]  from TSPL_SALE_RETURN_INTER_HEAD INNER JOIN   TSPL_SECONDARY_CUSTOMER_MASTER ON TSPL_SALE_RETURN_INTER_HEAD.Cust_Code  = TSPL_SECONDARY_CUSTOMER_MASTER.Cust_Code where  TSPL_SALE_RETURN_INTER_HEAD.Is_Post=1  AND TSPL_SECONDARY_CUSTOMER_MASTER.Status='N'  " & _
           " union All " & _
" select TSPL_VCGL_Head.VC_Code as ACode, TSPL_VCGL_Head.Document_No as DocNo, CASE WHEN Amount_Type='Cr' THEN TSPL_VCGL_Head.Amount ELSE TSPL_VCGL_Head.Amount*-1 END, convert(date,TSPL_VCGL_Head.Document_Date,103), 'VGCL' from  TSPL_VCGL_Head  left outer JOIN   TSPL_SECONDARY_CUSTOMER_MASTER ON TSPL_VCGL_Head.VC_Code  = TSPL_SECONDARY_CUSTOMER_MASTER.Cust_Code where TSPL_VCGL_Head.Document_Type='C' and TSPL_VCGL_Head.Status=1  AND TSPL_SECONDARY_CUSTOMER_MASTER.Status='N'  " & _
           " UNION ALL " & _
 " select TSPL_VCGL_Detail.VCGL_Code as ACode, TSPL_VCGL_Head.Document_No as DocNo, CASE WHEN TSPL_VCGL_Detail.Cr_Amount>0 THEN TSPL_VCGL_Detail.Cr_Amount*-1 ELSE TSPL_VCGL_Detail.Dr_Amount END, convert(date,TSPL_VCGL_Head.Document_Date,103), 'VGCL' from  TSPL_VCGL_Detail left outer join TSPL_VCGL_Head on TSPL_VCGL_Head .Document_No=TSPL_VCGL_Detail.Document_No left outer JOIN   TSPL_SECONDARY_CUSTOMER_MASTER ON TSPL_VCGL_Detail.VCGL_Code  = TSPL_SECONDARY_CUSTOMER_MASTER.Cust_Code where  TSPL_VCGL_Head.Status=1 and TSPL_VCGL_Detail.Row_Type='Customer'  AND TSPL_SECONDARY_CUSTOMER_MASTER.Status='N'  " & _
            " union All " & _
 " select TSPL_RECEIPT_HEADER.Cust_Code, TSPL_RECEIPT_HEADER.Receipt_No , Case When TSPL_RECEIPT_HEADER.Receipt_Type='F' Then TSPL_RECEIPT_HEADER.Balance_Amt Else TSPL_RECEIPT_HEADER.Balance_Amt*-1 End,convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103), case when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'AV'  when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'OA'  when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'UC'  when TSPL_RECEIPT_HEADER.Receipt_Type='F' then 'RF' when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'RC' end from TSPL_RECEIPT_HEADER inner join TSPL_SECONDARY_CUSTOMER_MASTER  ON TSPL_RECEIPT_HEADER.Cust_Code = TSPL_SECONDARY_CUSTOMER_MASTER.Cust_Code where TSPL_RECEIPT_HEADER.Receipt_Type NOT IN ('M') and TSPL_RECEIPT_HEADER.Posted='Y' AND TSPL_RECEIPT_HEADER.IsChkReverse='N' AND TSPL_RECEIPT_HEADER.Balance_Amt>0  AND TSPL_SECONDARY_CUSTOMER_MASTER.Status='N'  " & _
   "         UNION ALL " & _
 " Select Customer_No as [Customer Id], Adjustment_No as [Document Id], Adjustment_Amount*-1 as [Due Amount], CONVERT(DATE,Adjustment_Date,103) as [Document Date], 'RC' as [Document_Type] from TSPL_Receipt_Adjustment_Header LEFT OUTER JOIN TSPL_SECONDARY_CUSTOMER_MASTER on TSPL_Receipt_Adjustment_Header.Customer_No= TSPL_SECONDARY_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_SALE_INVOICE_HEAD on TSPL_Receipt_Adjustment_Header.Doc_No= TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No WHERE TSPL_Receipt_Adjustment_Header.Is_Post ='Y'  AND TSPL_SECONDARY_CUSTOMER_MASTER.Status='N'  " & _
           " Union All  " & _
 " SELECT TSPL_SALE_RETURN_INTER_HEAD.Cust_Code AS [Customer Id], Document_No as [Document Id], Empty_Value*-1 AS [Due Amount], CONVERT(DATE,Document_Date,103) as [Document Date], 'SR' as [Document_Type] from TSPL_SALE_RETURN_INTER_HEAD INNER JOIN   TSPL_SECONDARY_CUSTOMER_MASTER ON TSPL_SALE_RETURN_INTER_HEAD.Cust_Code  = TSPL_SECONDARY_CUSTOMER_MASTER.Cust_Code where  TSPL_SALE_RETURN_INTER_HEAD.Is_Post=1  AND TSPL_SECONDARY_CUSTOMER_MASTER.Status='N'  " & _
            " UNION ALL " & _
 " SELECT  TSPL_ADJUSTMENT_HEADER.Customer_CODE, TSPL_ADJUSTMENT_HEADER.Adjustment_No, case when TSPL_ADJUSTMENT_HEADER.Trans_Type<>'In' then  (SELECT SUM(ISNULL(Item_Cost,0)+ISNULL(Breakage_Cost,0))*-1 FROM dbo.TSPL_ADJUSTMENT_DETAIL WHERE TSPL_ADJUSTMENT_DETAIL.Adjustment_No=TSPL_ADJUSTMENT_HEADER.Adjustment_No)*-1 else (SELECT SUM(ISNULL(Item_Cost,0)+ISNULL(Breakage_Cost,0))*-1 FROM dbo.TSPL_ADJUSTMENT_DETAIL WHERE TSPL_ADJUSTMENT_DETAIL.Adjustment_No=TSPL_ADJUSTMENT_HEADER.Adjustment_No) end, convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103), 'AD' FROM dbo.TSPL_ADJUSTMENT_HEADER LEFT OUTER JOIN TSPL_SECONDARY_CUSTOMER_MASTER on TSPL_ADJUSTMENT_HEADER.Customer_CODE=TSPL_SECONDARY_CUSTOMER_MASTER.Cust_Code WHERE TSPL_ADJUSTMENT_HEADER.Customer_CODE <> '' AND ISNULL(Reference_Document,'')=''  and TSPL_ADJUSTMENT_HEADER.Posted='Y'  AND TSPL_SECONDARY_CUSTOMER_MASTER.Status='N'  " & _
 " ) XXX LEFT OUTER JOIN TSPL_SECONDARY_CUSTOMER_MASTER ON XXX.[Customer Id]=TSPL_SECONDARY_CUSTOMER_MASTER.Cust_Code where  XXX.Document_Type in ('IN','DB','CR','RC','AV','OA','UC','SR','VGCL','AD','RF','RC'  )  and convert(date,XXX.[Document Date] ,103) <= convert(date,'03/09/2014',103) AND [Due Amount] <> 0 And [Customer Id]='" & fndCustomer.Value & "' Group By XXX.[Customer Id]  ORDER BY [Customer Id] "


            Dim dt As DataTable
            dt = clsDBFuncationality.GetDataTable(QryToGetOutAmt)
            If dt.Rows IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                OutStandAmt = clsCommon.myCdbl(dt.Rows(0)("Due Amount").ToString())
            End If
            If OutStandAmt > 0 AndAlso chkInActive.Checked = True Then
                Throw New Exception("You can not make this customer Inactive because it has outstanding amount")
            End If
            '' ******************* Check Outstanding Amount Of customer *************

            'Prabhakar Dim issaved As Boolean = obj.SaveData(obj, obj.ArrVisi, isNewEntry, arrDBName)
            UcAttachment1.SaveData(obj.Cust_Code)

            If btnSave.Text = "Save" Then
                common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                btnSave.Text = "Update"
                btnDelete.Enabled = True
            Else
                common.clsCommon.MyMessageBoxShow("Data Updated Successfully")
            End If
            LoadData() '-Fills data

            '===================if customer master open from another form then delete button and new button should be disable====================
            If DrillDown_FormName IsNot Nothing AndAlso clsCommon.CompairString(DrillDown_FormName, "ENQ-MST") = CompairStringResult.Equal Then
                Dim qry As String = "insert into cust_info (cust_code,cust_name) values ('" + fndCustomer.Value + "','" + txtCustomerName.Text + "')"
                clsDBFuncationality.ExecuteNonQuery(qry)

                Me.Close()
            End If
            '==============================================================================
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    End Sub

    Sub LoadData()
        Dim strId As String
        strCmd = "select Cust_Code from  TSPL_SECONDARY_CUSTOMER_MASTER where Cust_Code='" + fndCustomer.Value + "'"
        strId = clsDBFuncationality.getSingleValue(strCmd)


        If clsCommon.myCstr(strId).ToUpper() = fndCustomer.Value Then
            funFill()
            chkInActive.Enabled = True
        Else
            NewReset()
        End If
    End Sub

    Public Sub funFill()
        Try

            If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Description from tspl_fixed_parameter where type='" & clsFixedParameterType.ShowVisiDetail & "' and code ='" & clsFixedParameterCode.ShowVisiDetail & "' ")), "1") = CompairStringResult.Equal Then
                'LoadVisiDetail()
            End If

            strCmd = "SELECT [Customer_Name],[Add1],[Add2],[Add3],[Closing_Date],[Cust_Category_Code],[Cust_Group_Code],[Cust_Type_Code]" & _
                             "  ,[Route_No],[Route_Desc],[Price_Code],[City_Code],[State],[Country],[Phone1],[Phone2],[Fax],[Email],[WebSite],[Contact_Person_Name],[Contact_Person_Phone],[Contact_Person_Fax]" & _
                             " ,[Contact_Person_Website],[Contact_Person_Email],[Terms_Code],[Cust_Account],[Tax_Group],[TAX1],[TAX1_Rate],[TAX2],[TAX2_Rate],[TAX3],[TAX3_Rate],[TAX4],[TAX4_Rate],[TAX5],[TAX5_Rate],[TAX6],[TAX6_Rate] " & _
                             " ,[TAX7],[TAX7_Rate],[TAX8],[TAX8_Rate],[TAX9],[TAX9_Rate],[TAX10],[TAX10_Rate],[Payment_Code],[Service_Tax_No] " & _
                             " ,[Tin_No],[Lst_No],[Form_Type],[Channel_Code],[Status],[OnHold],[Remarks1],[Remarks2],[Additional1],[Additional2],[Additional3],[Salesman_Code],[Visi_Id] " & _
                             " ,[Credit_Limit],[Channel_Desc],[Visi_Desc],[Salesman_Desc],[Route_Group],[CST],[ECC],[Range],[Collectorate],[PAN],[Division], [Parent_Customer_No],Customer_Class,credit_customer,Price_CodeNon,Inter_branch,transaction_type,Agg_Made_Date,Agg_Close_Date,CURRENCY_CODE,parent_customer_yn,Service_Dealer_Code,TDM_Code,Distributor_Code,IsDistributor,Price_Group_Code,CSA_Type,Category_Struct_Code,TempCreditLimit,TempCreditLimitFrom,TempCreditLimitTo,CheckCreditLimit,Alies_Name,Zone_Code,[PIN_NO],Crate_Opening ,Crate_Opening_Date,Franchise_Code,Other_For_PAN FROM TSPL_SECONDARY_CUSTOMER_MASTER where Cust_Code = '" + fndCustomer.Value + "'"

            For ii As Integer = 0 To gvDB.Rows.Count - 1
                If clsCommon.CompairString(clsCommon.myCstr(gvDB.Rows(ii).Cells(colCompCode).Value), objCommonVar.CurrentCompanyCode) = CompairStringResult.Equal Then
                    gvDB.Rows(ii).Cells(colSelect).Value = True
                Else
                    gvDB.Rows(ii).Cells(colSelect).Value = False
                End If
            Next

            ''For Custom Fields
            If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                UcCustomFields1.BlankAllControls()
            End If
            ''End of For Custom Fields

            myDs = connectSql.RunSQLReturnDS(strCmd)
            Dim myDr As DataRow
            For Each myDr In myDs.Tables(0).Rows
                Me.txtCustomerName.Text = myDr(0)
                Me.txtAdd1.Text = myDr(1)
                Me.txtAdd2.Text = myDr(2)
                Me.txtAdd3.Text = myDr(3)

                Dim date1 As String = clsCommon.myCstr(myDr(4))
                If date1 <> "" Then

                    Me.dtClosing.Value = myDr(4).ToString()
                Else
                    dtClosing.Value = dtClosing.MinDate

                End If
                Me.fndCusCategory.Value = clsCommon.myCstr(myDr(5))
                Me.fndCusgrp.Value = clsCommon.myCstr(myDr(6))
                Me.txtCusgrp.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Cust_Group_Desc  from TSPL_CUSTOMER_GROUP_MASTER where Cust_Group_Code='" + fndCusgrp.Value + "'"))
                Me.fndCusType.Value = clsCommon.myCstr(myDr(7))
                Me.fndRoute.Value = clsCommon.myCstr(myDr(8))

                '' MULTICURRENCY
                'If Me.fndBaseCurrency.Enabled = True Then
                '    Me.fndBaseCurrency.Value = clsCommon.myCstr(myDr("CURRENCY_CODE"))
                'Else
                '    Me.fndBaseCurrency.Value = Nothing
                'End If
                Me.fndCustCurrency.Value = clsCommon.myCstr(myDr("CURRENCY_CODE"))
                ChkOther.Checked = IIf(clsCommon.myCstr(myDr("Other_For_PAN")) = "1", True, False)
                '' END MULTICURRENCY
                Me.txtRoute.Text = clsCommon.myCstr(myDr(9))
                Me.txtPriceCode.Value = clsCommon.myCstr(myDr(10))
                Me.fndCity.Value = clsCommon.myCstr(myDr(11))
                txtCity.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select City_Name from TSPL_CITY_MASTER where City_Code='" + fndCity.Value + "'"))
                fndstate.Value = clsCommon.myCstr(myDr(12))
                txtStateName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  STATE_NAME  from TSPL_State_MASTER where STATE_CODE ='" + fndstate.Value + "'"))

                'Dim qry As String = "select state_name from tspl_tds_state_master where state_code='" & fndstate.Value & "'"
                'txtStateName.Text = clsCommon.myCstr(connectSql.RunScalar(qry))
                '' Anubhooti 26-Aug-2014 BM00000003619
                Me.fndCountry.Value = clsCommon.myCstr(myDr(13))
                Dim Countryqry As String = "select COUNTRY_NAME from TSPL_COUNTRY_MASTER where COUNTRY_CODE='" & fndCountry.Value & "'"
                TxtCountryName.Text = clsCommon.myCstr(connectSql.RunScalar(Countryqry))


                ' Me.txtCountry.Text = clsCommon.myCstr(myDr(13))

                ''richa
                '' Remove these two fields 09-June-2015 (Amit Sir)
                'TxtCrateOpeningQty.Value = clsCommon.myCdbl(myDr("Crate_Opening"))
                'If clsCommon.myLen(myDr("Crate_Opening_Date")) > 0 Then
                '    TxtCrateOpeningDate.Enabled = True
                '    TxtCrateOpeningDate.Checked = True
                '    TxtCrateOpeningDate.Value = clsCommon.myCDate(myDr("Crate_Opening_Date"))
                'Else
                '    TxtCrateOpeningDate.Enabled = False
                '    TxtCrateOpeningDate.Checked = False
                'End If

                Me.txtPhone1.Text = clsCommon.myCstr(myDr(14))
                Me.txtPhone2.Text = clsCommon.myCstr(myDr(15))
                Me.txtfax.Text = clsCommon.myCstr(myDr(16))
                Me.txtEmail.Text = clsCommon.myCstr(myDr(17))
                Me.txtWeb.Text = clsCommon.myCstr(myDr(18))
                Me.txtContactName.Text = clsCommon.myCstr(myDr(19))
                Me.txtContPhone.Text = clsCommon.myCstr(myDr(20))
                Me.txtContactFax.Text = clsCommon.myCstr(myDr(21))
                Me.txtContactWeb.Text = clsCommon.myCstr(myDr(22))
                Me.txtContactEmail.Text = clsCommon.myCstr(myDr(23))
                Me.fndTrmsCode.Value = clsCommon.myCstr(myDr(24))
                Me.fndAccntSet.Value = clsCommon.myCstr(myDr(25))
                Me.fndTxGrp.Value = clsCommon.myCstr(myDr(26))
                Me.fndPayCode.Value = clsCommon.myCstr(myDr(47))
                Me.txtStaxNo.Text = clsCommon.myCstr(myDr(48))
                Me.txtTinNo.Text = myDr(49).ToString
                Me.txtLstNo.Text = myDr(50).ToString
                Me.drpformtype.Text = myDr(51).ToString
                Me.fndChannel.Value = myDr(52).ToString
                txtTxGrp.Text = clsDBFuncationality.getSingleValue("SELECT [Tax_Group_Desc] FROM [TSPL_TAX_GROUP_MASTER] where Tax_Group_Code='" + fndTxGrp.Value + "'")

                Dim strStatus As String = clsCommon.myCstr(myDr("Status"))
                If clsCommon.CompairString(strStatus, "N") = CompairStringResult.Equal OrElse Not clsCommon.CompairString(strStatus, "Y") = CompairStringResult.Equal Then
                    chkInActive.Checked = False
                ElseIf strStatus = "Y" Then
                    chkInActive.Checked = True
                End If

                Dim strHold As String = myDr(54).ToString
                If strHold = "N" Then
                    chkHold.Checked = False
                ElseIf strHold = "Y" Then
                    chkHold.Checked = True
                End If

                Me.txtRemarks1.Text = myDr(55).ToString
                Me.txtRemarks2.Text = myDr(56).ToString
                Me.txtAddInfo1.Text = myDr(57).ToString
                Me.txtAddInfo2.Text = myDr(58).ToString
                Me.txtAddInfo3.Text = myDr(59).ToString
                Me.fndSalePerson.Value = myDr(60).ToString


                Me.txtCredit.Text = myDr(62).ToString
                Me.txtChannel.Text = myDr(63).ToString()

                Me.txtSalesPerson.Text = myDr(65).ToString()
                Me.fndroutegroup.Value = myDr(66).ToString()
                Me.txtcst.Text = myDr(67).ToString()
                Me.txtecc.Text = myDr(68).ToString()
                Me.txtrange.Text = myDr(69).ToString()
                Me.txtcollect.Text = myDr(70).ToString()
                Me.txtpan.Text = myDr(71).ToString()
                Me.txtdivision.Text = myDr(72).ToString()

                Me.txtPinNo.Text = myDr("PIN_NO").ToString
                ''richa agarwal
                'If clsCommon.myLen(txtParentCstNo.Value) > 0 Then
                '    txtCredit.Enabled = False
                'Else
                '    txtCredit.Enabled = True
                'End If
                ''-===================
                txtroutegroup.Text = clsDBFuncationality.getSingleValue("select  Description from  TSPL_ROUTE_GROUP_MASTER where Group_Id='" + fndroutegroup.Value + "'  ")


                If clsCommon.myLen(myDr("Agg_Made_Date").ToString) > 0 Then
                    dtpAggMade.Value = clsCommon.GetPrintDate(myDr("Agg_Made_Date"), "dd/MMM/yyyy")
                End If
                If clsCommon.myLen(myDr("Agg_Close_Date").ToString) > 0 Then
                    dtpAggClose.Value = clsCommon.GetPrintDate(myDr("Agg_Close_Date"), "dd/MMM/yyyy")
                End If

                Me.txtPriceCodeNon.Value = myDr("Price_CodeNon").ToString()

                '-----------------------------------------------------------
                Me.txtpgfnd.Value = myDr("Price_Group_Code").ToString()

                If clsCommon.myLen(txtpgfnd.Value) > 0 Then
                    chkpricegrpslctr.Checked = True
                ElseIf clsCommon.myLen(txtpgfnd.Value) = 0 Then
                    chkpricegrpslctr.Checked = False
                End If
                PriceCodeEnable()
                '---------------------------------------------------------------









                Dim transaction_type As String = myDr("transaction_type").ToString()
                If transaction_type = "R" Then
                    CmbTransaction.SelectedValue = "R"
                ElseIf transaction_type = "T" Then
                    CmbTransaction.SelectedValue = "T"
                Else
                    CmbTransaction.SelectedValue = ""
                End If

                ''richa ticket No. BM00000003109 on 19/08/2014
                Me.txtTempCreditLimit.Text = myDr("TempCreditLimit").ToString()
                Me.txttempCreditLimitFrom.Text = myDr("TempCreditLimitFrom").ToString()
                Me.txttempCreditLimitTo.Text = myDr("TempCreditLimitTo").ToString()
                '---------------------------------------------
                ''richa 12/09/2014
                If clsCommon.myCdbl(myDr("CheckCreditLimit")) = 0 Then
                    ChkCheckCreditLimit.Checked = False
                Else
                    ChkCheckCreditLimit.Checked = True
                End If

                '===========================
                ' cboCustomerClass.SelectedValue = clsCommon.myCstr(myDr("Customer_Class"))
                ''Anand 22/09/2014
                Me.txtAliesName.Text = myDr("Alies_Name").ToString()
                Me.fndZone.Value = myDr("Zone_Code").ToString()
                '===============
                LoadCus()

                Dim strcredit As String = myDr(75).ToString
                If strcredit = "N" Or strcredit = "" Then
                    chkcredit.Checked = False
                ElseIf strcredit = "Y" Then
                    chkcredit.Checked = True
                End If


                btnSave.Text = "Update"
                btnDelete.Enabled = True
                grdTax.Rows.Clear()
                If clsCommon.CompairString(clsCommon.myCstr(myDr(27)), "") <> CompairStringResult.Equal Then
                    grdTax.Rows.AddNew()
                    Me.grdTax.Rows(0).Cells(0).Value = myDr(27).ToString
                    Me.grdTax.Rows(0).Cells(1).Value = myDr(28).ToString
                End If

                If clsCommon.CompairString(clsCommon.myCstr(myDr(29)), "") <> CompairStringResult.Equal Then
                    grdTax.Rows.AddNew()
                    Me.grdTax.Rows(1).Cells(0).Value = myDr(29).ToString
                    Me.grdTax.Rows(1).Cells(1).Value = myDr(30).ToString
                End If

                Dim s As String = myDr(31).ToString
                If clsCommon.CompairString(clsCommon.myCstr(myDr(31)), "") <> CompairStringResult.Equal Then
                    grdTax.Rows.AddNew()
                    Me.grdTax.Rows(2).Cells(0).Value = myDr(31).ToString
                    Me.grdTax.Rows(2).Cells(1).Value = myDr(32).ToString
                End If

                s = myDr(33).ToString
                If clsCommon.CompairString(clsCommon.myCstr(myDr(33)), "") <> CompairStringResult.Equal Then
                    grdTax.Rows.AddNew()
                    Me.grdTax.Rows(3).Cells(0).Value = myDr(33).ToString
                    Me.grdTax.Rows(3).Cells(1).Value = myDr(34).ToString
                End If

                If clsCommon.CompairString(clsCommon.myCstr(myDr(35)), "") <> CompairStringResult.Equal Then
                    grdTax.Rows.AddNew()
                    Me.grdTax.Rows(4).Cells(0).Value = myDr(35).ToString
                    Me.grdTax.Rows(4).Cells(1).Value = myDr(36).ToString
                End If

                If clsCommon.CompairString(clsCommon.myCstr(myDr(37)), "") <> CompairStringResult.Equal Then
                    grdTax.Rows.AddNew()
                    Me.grdTax.Rows(5).Cells(0).Value = myDr(37).ToString
                    Me.grdTax.Rows(5).Cells(1).Value = myDr(38).ToString
                End If

                If clsCommon.CompairString(clsCommon.myCstr(myDr(39)), "") <> CompairStringResult.Equal Then
                    grdTax.Rows.AddNew()
                    Me.grdTax.Rows(6).Cells(0).Value = myDr(39).ToString
                    Me.grdTax.Rows(6).Cells(1).Value = myDr(40).ToString
                End If

                If clsCommon.CompairString(clsCommon.myCstr(myDr(41)), "") <> CompairStringResult.Equal Then
                    grdTax.Rows.AddNew()
                    Me.grdTax.Rows(7).Cells(0).Value = myDr(41).ToString
                    Me.grdTax.Rows(7).Cells(1).Value = myDr(42).ToString
                End If

                If clsCommon.CompairString(clsCommon.myCstr(myDr(43)), "") <> CompairStringResult.Equal Then
                    grdTax.Rows.AddNew()
                    Me.grdTax.Rows(8).Cells(0).Value = myDr(43).ToString
                    Me.grdTax.Rows(8).Cells(1).Value = myDr(44).ToString
                End If

                If clsCommon.CompairString(clsCommon.myCstr(myDr(45)), "") <> CompairStringResult.Equal Then
                    grdTax.Rows.AddNew()
                    Me.grdTax.Rows(9).Cells(0).Value = myDr(45).ToString
                    Me.grdTax.Rows(9).Cells(1).Value = myDr(46).ToString
                End If
            Next


            '------------------category detail--------------------


            LoadCrateOpening()
            '-------------------------------------------------------
            UcAttachment1.LoadData(fndCustomer.Value)
            ''For Custom Fields
            If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                UcCustomFields1.LoadData(fndCustomer.Value)
            End If
            ''End of For Custom Fields

            'If userCode <> "ADMIN" Then
            '    If funSetUserAccess() = False Then Exit Sub
            'End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Customer")
        End Try
    End Sub





    Public Sub LoadCrateOpening()
        Try
            LoadBlankGrid()
            Dim qry As String = "select * from tSPL_CUSTOMER_CRATE_ACCOUNTING where  Cust_Code='" + fndCustomer.Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim intLineNo As Integer = 0
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    gvCrate.Rows.AddNew()
                    gvCrate.Rows(gvCrate.Rows.Count - 1).Cells(colLineNo).Value = clsCommon.myCdbl(dr("LINE_NO"))
                    gvCrate.Rows(gvCrate.Rows.Count - 1).Cells(ColLocation).Value = clsCommon.myCstr(dr("Location_Code"))
                    gvCrate.Rows(gvCrate.Rows.Count - 1).Cells(ColLocation).Value = clsDBFuncationality.getSingleValue("select * from TSPL_LOCATION_MASTER where Location_Code='" & clsCommon.myCstr(dr("Location_Code")) & "' ")
                    gvCrate.Rows(gvCrate.Rows.Count - 1).Cells(ColOpeningDate).Value = clsCommon.myCstr(dr("Crate_Opening_Date"))
                    gvCrate.Rows(gvCrate.Rows.Count - 1).Cells(ColOpeningQty).Value = clsCommon.myCdbl(dr("Crate_Opening"))
                Next
            Else
                gvCrate.Rows.AddNew()
            End If

            ''------------------Code Ends Here----------------------------------

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub LoadBlankGrid()
        gvCrate.Rows.Clear()
        gvCrate.Columns.Clear()

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvCrate.MasterTemplate.Columns.Add(repoLineNo)


        Dim repoLocCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLocCode.FormatString = ""
        repoLocCode.HeaderText = "Location Code"
        repoLocCode.Name = ColLocation
        repoLocCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoLocCode.Width = 200
        gvCrate.MasterTemplate.Columns.Add(repoLocCode)

        Dim repoLocationName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLocationName.FormatString = ""
        repoLocationName.HeaderText = "Location"
        repoLocationName.Name = colLocationName
        repoLocationName.ReadOnly = True
        repoLocationName.Width = 150
        gvCrate.MasterTemplate.Columns.Add(repoLocationName)

        Dim repoOpeningDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoOpeningDate.Format = DateTimePickerFormat.Custom
        repoOpeningDate.CustomFormat = "dd-MM-yyyy"
        repoOpeningDate.HeaderText = "Opening Date"
        repoOpeningDate.WrapText = True
        repoOpeningDate.FormatString = "{0:d}"
        repoOpeningDate.Name = ColOpeningDate
        repoOpeningDate.Width = 100
        gvCrate.MasterTemplate.Columns.Add(repoOpeningDate)

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Opening Qty"
        repoQty.Name = ColOpeningQty
        repoQty.Width = 200
        'repoQty.Minimum = 0
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvCrate.MasterTemplate.Columns.Add(repoQty)


        gvCrate.AllowAddNewRow = False
        gvCrate.ShowGroupPanel = False
        gvCrate.AllowColumnReorder = False
        gvCrate.AllowRowReorder = False
        gvCrate.EnableSorting = False
        gvCrate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvCrate.MasterTemplate.ShowRowHeaderColumn = False

    End Sub

    Public Sub NewReset()
        chkpricegrpslctr.Checked = False
        txtpgfnd.Value = ""
        Me.fndCusCategory.Value = ""
        Me.fndChannel.Value = ""
        Me.fndCusgrp.Value = ""
        Me.fndCusType.Value = ""
        Me.fndRoute.Value = ""

        Me.fndTrmsCode.Value = ""
        Me.fndAccntSet.Value = ""
        Me.fndTxGrp.Value = ""
        Me.txtTxGrp.Text = ""
        Me.fndSalePerson.Value = ""

        Me.fndPayCode.Value = ""
        Me.fndCity.Value = ""
        Me.fndCusCategory.MyReadOnly = True
        Me.fndChannel.MyReadOnly = True
        Me.fndCusgrp.MyReadOnly = True
        Me.fndCusType.MyReadOnly = True
        Me.fndRoute.MyReadOnly = True
        Me.txtSalesPerson.Text = ""
        Me.fndTrmsCode.MyReadOnly = True
        Me.fndAccntSet.MyReadOnly = True
        Me.fndTxGrp.MyReadOnly = True
        Me.fndSalePerson.MyReadOnly = True

        Me.fndPayCode.MyReadOnly = True
        Me.fndCity.MyReadOnly = True
        Me.txtCustomerName.Text = ""
        dtpAggMade.Value = Nothing
        dtpAggClose.Value = Nothing
        Me.txtPriceCodeNon.Value = ""
        Me.txtCusgrp.Text = ""
        Me.txtAdd1.Text = ""
        Me.txtAdd2.Text = ""
        Me.txtAdd3.Text = ""
        Me.dtClosing.Value = connectSql.serverDate()
        Me.txtRoute.Text = ""
        chkHold.Checked = False
        chkInActive.Checked = False
        chkInActive.Enabled = False
        Me.txtPriceCode.Value = ""
        fndstate.Value = ""

        Me.fndCountry.Value = ""
        Me.TxtCountryName.Text = ""
        ''
        Me.txtPhone1.Text = "(+__)__________"
        Me.txtPhone2.Text = "(+__)__________"
        Me.txtfax.Text = ""
        Me.txtPinNo.Text = ""
        Me.txtEmail.Text = ""
        Me.txtTinNo.Text = ""
        Me.drpformtype.SelectedIndex = 0
        Me.txtWeb.Text = ""
        Me.txtContactName.Text = ""
        Me.txtContPhone.Text = "(+__)__________"
        Me.txtContactFax.Text = ""
        Me.txtContactWeb.Text = ""
        Me.txtContactEmail.Text = ""
        Me.grdTax.DataSource = Nothing
        Me.grdTax.Rows.Clear()
        Me.txtStaxNo.Text = ""
        Me.txtLstNo.Text = ""
        Me.txtChannel.Text = ""
        Me.txtRemarks1.Text = ""
        Me.txtRemarks2.Text = ""
        Me.txtAddInfo1.Text = ""
        Me.txtAddInfo2.Text = ""
        Me.txtAddInfo3.Text = ""
        'Me.txtOutletComm.Text = "0.00"
        'Me.txtBaltoDate.Text = "0.00"
        Me.txtCredit.Text = "0.00"
        Me.txtcst.Text = ""
        Me.txtecc.Text = ""
        Me.txtrange.Text = ""
        Me.txtcollect.Text = ""
        Me.txtpan.Text = ""
        Me.txtdivision.Text = ""

        Me.fndroutegroup.Value = ""
        Me.fndroutegroup.MyReadOnly = True

        Me.fndCustCurrency.Value = Nothing




        btnSave.Text = "Save"
        btnDelete.Enabled = False


        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
        End If

        FillCustName()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub fndCountry__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCountry._MYValidating
        Try
            Dim qry As String = "select country_code as Code,country_name as Country from tspl_country_master"
            fndCountry.Value = clsCommon.ShowSelectForm("CNTFND", qry, "Code", "", fndCountry.Value, "Code", isButtonClicked)

            If clsCommon.myLen(fndCountry.Value) > 0 Then
                TxtCountryName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select country_name from tspl_country_master where country_code='" + fndCountry.Value + "'"))
            Else
                TxtCountryName.Text = ""
                fndstate.Value = ""
                txtStateName.Text = ""
                txtCity.Text = ""
                fndCity.Value = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndCusgrp__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCusgrp._MYValidating

        Dim qry As String = " SELECT Cust_Group_Code as [CustomerGruopCode],Cust_Group_Desc as [Description]," & _
                            " Tax_Group as [Tax Group],Cust_Account as [Account Set],Terms_Code as [Terms Code] FROM [TSPL_CUSTOMER_GROUP_MASTER] "
        fndCusgrp.Value = clsCommon.ShowSelectForm("CUSGRP_CODE", qry, "CustomerGruopCode", "", fndCusgrp.Value, "", isButtonClicked)
        txtCusgrp.Text = clsDBFuncationality.getSingleValue(" SELECT Cust_Group_Desc as [Description] FROM [TSPL_CUSTOMER_GROUP_MASTER] where Cust_Group_Code='" + fndCusgrp.Value + "'")
        fnCusGrp()
    End Sub

    Private Sub fnCusGrp()

        Try
            Dim strCmd1 As String = " SELECT Cust_Group_Code as [Customer Gruop Code],Cust_Group_Desc as [Description]," & _
                           " Tax_Group as [Tax Group],Cust_Account as [Account Set],Terms_Code as [Terms Code] FROM [TSPL_CUSTOMER_GROUP_MASTER] where Cust_Group_Code='" + fndCusgrp.Value + "' "
            myDs = connectSql.RunSQLReturnDS(strCmd1)
            If myDs.Tables(0).Rows.Count > 0 Then
                Dim row As DataRow = myDs.Tables(0).Rows(0)
                Me.txtCusgrp.Text = clsCommon.myCstr(row(1).ToString().Trim())
                Me.fndTxGrp.Value = clsCommon.myCstr(row(2).ToString().Trim())
                Me.fndAccntSet.Value = clsCommon.myCstr(row(3).ToString().Trim())
                Me.fndTrmsCode.Value = clsCommon.myCstr(row(4).ToString().Trim())
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Customer")
        End Try

    End Sub

    Private Sub fndstate__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndstate._MYValidating
        fndstate.Value = clsStateMaster.getFinder("", fndstate.Value, isButtonClicked)
        txtStateName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  STATE_NAME  from TSPL_State_MASTER where STATE_CODE ='" + fndstate.Value + "'"))
    End Sub

    Private Sub fndCity__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCity._MYValidating
        fndCity.Value = clsCityMaster.getFinder("", fndCity.Value, isButtonClicked)
        txtCity.Text = clsDBFuncationality.getSingleValue("SELECT [City_Name] FROM [TSPL_CITY_MASTER] where [City_Code]='" + fndCity.Value + "' ")
    End Sub

    Private Sub fndTrmsCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndTrmsCode._MYValidating
        fndTrmsCode.Value = clsPaymentTerms.getFinder("", fndTrmsCode.Value, isButtonClicked)
    End Sub

    Private Sub fndAccntSet__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndAccntSet._MYValidating
        Dim qry As String = "SELECT  [Cust_Account] as [AccountSetCode],[Cust_Acct_Desc] as [Description] FROM [TSPL_CUSTOMER_ACCOUNT_SET]"
        fndAccntSet.Value = clsCommon.ShowSelectForm("CUSTACODE", qry, "AccountSetCode", "", fndAccntSet.Value, "", isButtonClicked)
        fndCustCurrency.Value = clsDBFuncationality.getSingleValue("Select COALESCE(CURRENCY_CODE,'') AS CURRENCY_CODE from TSPL_CUSTOMER_ACCOUNT_SET where CUST_ACCOUNT='" + fndAccntSet.Value + "'")
    End Sub

    Private Sub fndPayCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndPayCode._MYValidating
        fndPayCode.Value = clsPaymentCode.getFinder("", fndPayCode.Value, isButtonClicked)
    End Sub

    Private Sub fndTxGrp__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndTxGrp._MYValidating
        fndTxGrp.Value = clsTaxGroupMaster.getFinder("Tax_Group_Type='S'", fndTxGrp.Value, isButtonClicked)
        txtTxGrp.Text = clsDBFuncationality.getSingleValue("SELECT [Tax_Group_Desc] FROM [TSPL_TAX_GROUP_MASTER] where Tax_Group_Code='" + fndTxGrp.Value + "'")
        fnTaxGrp()
    End Sub

    Private Sub fnTaxGrp()
        Try
            strCmd = " SELECT TSPL_TAX_GROUP_DETAILS.Tax_Code as [Tax] FROM TSPL_TAX_GROUP_MASTER INNER JOIN TSPL_TAX_GROUP_DETAILS ON TSPL_TAX_GROUP_MASTER.Tax_Group_Code = TSPL_TAX_GROUP_DETAILS.Tax_Group_Code" & _
                      " where TSPL_TAX_GROUP_MASTER.Tax_Group_Code='" + fndTxGrp.Value + "' and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S' and TSPL_TAX_GROUP_DETAILS.Tax_Group_Type='S' order by TSPL_TAX_GROUP_DETAILS.Trans_Code"

            myDs = connectSql.RunSQLReturnDS(strCmd)
            If myDs.Tables(0).Rows.Count > 0 Then
                Dim Dr As DataRow
                i = 0
                grdTax.DataSource = Nothing
                grdTax.Rows.Clear()
                For Each Dr In myDs.Tables(0).Rows
                    Dim r As GridViewRowInfo = grdTax.Rows.AddNew()
                    r.Cells(0).Value = Dr(0).ToString()
                Next
            End If
            grdTax.AllowAddNewRow = False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Customer")
        End Try
    End Sub

    Private Sub fndCusCategory__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCusCategory._MYValidating
        Dim qry As String = "SELECT [CUST_CATEGORY_CODE] as [CustomerCategoryCode],[CUST_CATEGORY_DESC] as [Description] FROM [TSPL_CUSTOMER_CATEGORY_MASTER]"
        fndCusCategory.Value = clsCommon.ShowSelectForm("CUSTCATCODEFND", qry, "CustomerCategoryCode", "", fndCusCategory.Value, "", isButtonClicked)
    End Sub

    Private Sub fndCusType__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCusType._MYValidating
        Dim qry As String = "SELECT  [Cust_Type_Code] as [CustomerTypeCode],[Cust_Type_Desc] as [Description]FROM [TSPL_CUSTOMER_TYPE_MASTER]"
        fndCusType.Value = clsCommon.ShowSelectForm("CUSTCODEFND", qry, "CustomerTypeCode", "", fndCusType.Value, "", isButtonClicked)
        LoadCus()
    End Sub

    Private Sub fndRoute__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndRoute._MYValidating
        Dim qry As String = "SELECT [Route_No] as [RouteNo.],[Route_Desc] as [Description],Type FROM [TSPL_ROUTE_MASTER]"
        Dim WhrCls As String = " Status='A' "
        fndRoute.Value = clsCommon.ShowSelectForm("ROUTeNOFND", qry, "RouteNo.", WhrCls, fndRoute.Value, "", isButtonClicked)
        txtRoute.Text = clsDBFuncationality.getSingleValue("SELECT [Route_Desc] FROM [TSPL_ROUTE_MASTER] where Route_No='" + fndRoute.Value + "' ")
        fnRoute()
        fndroutegroup.Value = ""
        txtroutegroup.Text = ""
    End Sub
    Private Sub fnRoute()
        Dim strRouteName As String
        Dim dt As DataTable
        strRouteName = ""
        Try
            strCmd = "SELECT [Route_No] as [Route No.],[Route_Desc] as [Description],Type,Employee_Code as [Sales Person Code],Employee_Name as [Sales Person Name],Price_Code ,isnull(NonPrice_Code,'') as NonPrice FROM [TSPL_ROUTE_MASTER] where Route_No='" + fndRoute.Value + "'  "
            dt = clsDBFuncationality.GetDataTable(strCmd)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each row As DataRow In dt.Rows

                    Me.txtRoute.Text = clsCommon.myCstr(row(1).ToString().Trim())
                    Me.fndSalePerson.Value = clsCommon.myCstr(row(3).ToString().Trim())
                    Me.txtSalesPerson.Text = clsCommon.myCstr(row(4).ToString().Trim())
                    Me.txtPriceCode.Value = clsCommon.myCstr(row("Price_Code").ToString().Trim())
                    Me.txtPriceCodeNon.Value = clsCommon.myCstr(row("NonPrice").ToString().Trim())
                Next
            End If
            'fndroutegroup.ConnectionString = connectSql.SqlCon()
            'fndroutegroup.Query = "select Group_Id as [Group Id] ,Status,Route_Code as [Route Code],Description from  TSPL_ROUTE_GROUP_MASTER where Route_Code ='" + fndRoute.Value + "' "
            'fndroutegroup.ValueToSelect = "Group Id"
            'fndroutegroup.Caption = "Group Master"
            'fndroutegroup.ValueToSelect1 = "Description"



        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Customer")
        End Try

    End Sub

    Private Sub fndSalePerson__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndSalePerson._MYValidating

    End Sub

    Private Sub fndZone__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndZone._MYValidating
        fndZone.Value = ClsZoneMaster.getFinder("", fndZone.Value, isButtonClicked)
    End Sub

    Private Sub txtpgfnd__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtpgfnd._MYValidating
        Dim qry As String = "select distinct TSPL_PRICE_GROUP_MAPPING.price_group_code as GroupCode,TSPL_PRICE_GROUP_MAPPING.price_group_desc as Description from TSPL_PRICE_GROUP_MAPPING"
        Dim whrCls As String = " "
        txtpgfnd.Value = clsCommon.ShowSelectForm("PriceGRPCode", qry, "GroupCode", whrCls, txtpgfnd.Value, "GroupCode", isButtonClicked)
    End Sub

    Private Sub txtPriceCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtPriceCode._MYValidating
        Dim qry As String = "SELECT DISTINCT TSPL_ITEM_PRICE_MASTER.Price_Code as [Code], TSPL_PRICE_COMPONENT_MAPPING.Price_Code_Desc as [Price Code Description], TSPL_ITEM_PRICE_MASTER.Tax_group as [Tax Group] FROM TSPL_ITEM_PRICE_MASTER INNER JOIN TSPL_PRICE_COMPONENT_MAPPING ON TSPL_ITEM_PRICE_MASTER.Price_Code = TSPL_PRICE_COMPONENT_MAPPING.Price_Code INNER JOIN TSPL_TAX_GROUP_MASTER ON TSPL_ITEM_PRICE_MASTER.Tax_group = TSPL_TAX_GROUP_MASTER.Tax_Group_Code "
        Dim WhrCls As String = " TSPL_TAX_GROUP_MASTER.Excisable ='Y'"
        txtPriceCode.Value = clsCommon.ShowSelectForm("PriceCodeNonEx", qry, "Code", WhrCls, txtPriceCode.Value, "Code", isButtonClicked)
    End Sub

    Private Sub txtPriceCodeNon__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtPriceCodeNon._MYValidating

        Dim qry As String = "SELECT DISTINCT TSPL_ITEM_PRICE_MASTER.Price_Code as [Code], TSPL_PRICE_COMPONENT_MAPPING.Price_Code_Desc as [Price Code Description], TSPL_ITEM_PRICE_MASTER.Tax_group as [Tax Group] FROM TSPL_ITEM_PRICE_MASTER INNER JOIN TSPL_PRICE_COMPONENT_MAPPING ON TSPL_ITEM_PRICE_MASTER.Price_Code = TSPL_PRICE_COMPONENT_MAPPING.Price_Code INNER JOIN TSPL_TAX_GROUP_MASTER ON TSPL_ITEM_PRICE_MASTER.Tax_group = TSPL_TAX_GROUP_MASTER.Tax_Group_Code "
        Dim WhrCls As String = " TSPL_TAX_GROUP_MASTER.Excisable ='N'"
        txtPriceCodeNon.Value = clsCommon.ShowSelectForm("PriceCodeNFND", qry, "Code", WhrCls, txtPriceCodeNon.Value, "Code", isButtonClicked)
    End Sub

    Private Sub fndChannel__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndChannel._MYValidating
        Dim qry As String = "SELECT  [Channel_Id] as [ChannelId],[Channel_Category] as [Category],[Channel_Name] as [Name] FROM [TSPL_CHANNEL_MASTER]"
        fndChannel.Value = clsCommon.ShowSelectForm("FNDCHANNELID", qry, "ChannelId", "", fndChannel.Value, "", isButtonClicked)
        txtChannel.Text = clsDBFuncationality.getSingleValue("SELECT [Channel_Name] FROM [TSPL_CHANNEL_MASTER] where Channel_Id='" + fndChannel.Value + "'")
    End Sub

    Private Sub fndroutegroup__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndroutegroup._MYValidating
        Dim routeGrpCode As String
        Dim qry As String = "select Group_Id as [GroupId] ,Status,Route_Code as [Route Code],Description from  TSPL_ROUTE_GROUP_MASTER  "

        routeGrpCode = clsCommon.ShowSelectForm("GRUPIDFND", qry, "GroupId", "Route_Code ='" + fndRoute.Value + "' ", fndroutegroup.Value, "", isButtonClicked)
        If clsCommon.myLen(routeGrpCode) > 0 Then
            fndroutegroup.Value = routeGrpCode
            txtroutegroup.Text = clsDBFuncationality.getSingleValue("select  Description from  TSPL_ROUTE_GROUP_MASTER where Group_Id='" + fndroutegroup.Value + "'  ")
        Else
            fndroutegroup.Value = ""
            txtroutegroup.Text = ""
        End If
    End Sub


    Public Shared Function GetVendorNextCode(ByVal TableName As String, ByVal FieldName As String, ByVal StrVenName As String, ByVal trans As SqlTransaction) As String

        If clsCommon.myLen(StrVenName) <= 0 Then
            Throw New Exception("Please enter Description")
        End If
        StrVenName = StrVenName.Substring(0, 1)
        Dim qry As String = ""
        Dim DigitLen As String = ""
        Dim Digits As Double
        Dim strRetCode As String = ""

        Dim strLocatinSegmentCode As String = ""
        ' Dim dt As DataTable
        If clsCommon.myLen(StrVenName) > 0 Then
            ' Dim dt1 As DataTable
            Dim qry1 As String = "Select COUNT(*) AS Row From " + TableName + "  Where " + FieldName & " like '" + StrVenName + "%'"
            Dim VNameSeries As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry1, trans))
            If clsCommon.CompairString(TableName, "TSPL_VENDOR_MASTER") = CompairStringResult.Equal Then
                Digits = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AutoGeneratedDigitsForVendor, clsFixedParameterCode.AutoGeneratedDigitsForVendor, trans))
            ElseIf clsCommon.CompairString(TableName, "TSPL_SECONDARY_CUSTOMER_MASTER") = CompairStringResult.Equal Then
                Digits = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AutoGeneratedDigitsForCustomer, clsFixedParameterCode.AutoGeneratedDigitsForCustomer, trans))
            End If
            Digits -= clsCommon.myLen(VNameSeries)
            If clsCommon.myLen(Digits) > 0 Then
                For dig As Integer = 1 To Digits
                    DigitLen += "0"
                Next
            End If

            If VNameSeries = 0 Then
                VNameSeries = 1
            Else
                VNameSeries = 1 + VNameSeries
            End If

            strRetCode = StrVenName.ToUpper() + DigitLen + clsCommon.myCstr(VNameSeries)
            Dim dt As DataTable = Nothing
            Dim blCondition As Boolean = True
            While blCondition
                If clsCommon.CompairString(TableName, "TSPL_VENDOR_MASTER") = CompairStringResult.Equal Then
                    dt = clsDBFuncationality.GetDataTable("Select 1  From tspl_vendor_master where vendor_code='" + strRetCode + "'", trans)
                ElseIf clsCommon.CompairString(TableName, "TSPL_SECONDARY_CUSTOMER_MASTER") = CompairStringResult.Equal Then
                    dt = clsDBFuncationality.GetDataTable("Select 1  From TSPL_SECONDARY_CUSTOMER_MASTER where Cust_Code='" + strRetCode + "'", trans)
                End If

                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    blCondition = False
                Else
                    blCondition = True
                    strRetCode = clsCommon.incval(strRetCode)
                End If
            End While

        End If
        Return strRetCode
    End Function

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        funDelete()
    End Sub

    '================================================Add  27/09/2016========================================================================================='

    Private Sub btnSaveSecondaryCustomerMaster_Click(sender As Object, e As EventArgs)
        Try

            'If clsCommon.myLen(fndCustomer.Value) <= 0 Then
            '    myMessages.blankValue("Customer No")
            '    fndCustomer.Focus()
            '    Return
            'End If


            If clsCommon.myLen(txtCustomerName.Text) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please fill customer name", "Customer", MessageBoxButtons.OK)
                pageCus.SelectedPage = RadPageViewPage1
                txtCustomerName.Focus()
                Return
            End If
            If clsCommon.myLen(fndCusgrp.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Select Customer Group", "Customer", MessageBoxButtons.OK)
                pageCus.SelectedPage = RadPageViewPage1
                fndCusgrp.Focus()
                Return
            End If
            If clsCommon.myLen(fndCusCategory.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Select Customer Catageory ", "Customer", MessageBoxButtons.OK)
                pageCus.SelectedPage = RadPageViewPage3
                fndCusCategory.Focus()
                Return
            End If

            If clsCommon.myLen(fndAccntSet.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Select Customer Account Set", "Customer", MessageBoxButtons.OK)
                pageCus.SelectedPage = RadPageViewPage4
                fndAccntSet.Focus()
                Return
            End If
            Dim arrChecked As List(Of String) = New List(Of String)
            For jj As Integer = 0 To gvDB.Rows.Count - 1
                If (clsCommon.myCBool(gvDB.Rows(jj).Cells(colSelect).Value)) Then
                    arrChecked.Add(clsCommon.myCstr(gvDB.Rows(jj).Cells(colDataBaseName).Value))
                End If
            Next

            If arrChecked Is Nothing OrElse arrChecked.Count = 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Company Under Additional Info Tab")
                pageCus.SelectedPage = RadPageViewPage5
                Return
            End If



            SaveSecondaryCustomerMaster()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub SaveSecondaryCustomerMaster()

        Dim obj As New clsSecondaryCustomerMasterInfo()

        obj.Customer_Name = fndCustomer.Value
        obj.Alies_Name = txtAliesName.Text
        If clsCommon.myLen(dtpAggMade.Text) > 0 Then
            obj.Agg_Made_Date = clsCommon.GetPrintDate(dtpAggMade.Text, "MM/dd/yyyy")
        End If
        If clsCommon.myLen(dtpAggClose.Text) > 0 Then
            obj.Agg_Close_Date = clsCommon.GetPrintDate(dtpAggClose.Text, "MM/dd/yyyy")
        End If
        obj.TRANSACTION_TYPE = CmbTransaction.Text
        obj.CUSTOMER_TYPE = CmbCustomerType.Text
        If obj.CUSTOMER_TYPE = "Retailer" Or obj.CUSTOMER_TYPE = "R" Then
            obj.IsDistributor = "N"
        End If
        If obj.CUSTOMER_TYPE = "Distributer" Or obj.CUSTOMER_TYPE = "D" Then
            obj.IsDistributor = "Y"
        End If
        If isCheckCustomerType Then
            obj.POS_Type = CmbCustomerType.SelectedValue
        Else
            obj.POS_Type = Nothing
        End If
        obj.CURRENCY_CODE = fndCustCurrency.Value
        obj.Cust_Group_Code = fndCusgrp.Value
        obj.Add1 = txtAdd1.Text
        obj.Add2 = txtAdd2.Text
        obj.Add3 = txtAdd3.Text
        obj.Country = fndCountry.Value
        obj.State = fndstate.Value
        obj.City_Code = fndCity.Value
        obj.Phone1 = txtPhone1.Text
        obj.Phone2 = txtPhone2.Text
        obj.Email = txtEmail.Text
        obj.WebSite = txtWeb.Text
        obj.Fax = txtfax.Text
        obj.PIN_NO = txtPinNo.Text
        obj.Parent_Customer_No = txtParentCstNo.Value

        'Contact Person'
        obj.Contact_Person_Name = txtContactName.Text
        obj.Contact_Person_Phone = txtContPhone.Text
        obj.Contact_Person_Fax = txtContactFax.Text
        obj.Contact_Person_Email = txtContactEmail.Text
        obj.Contact_Person_Website = txtContactWeb.Text

        'Process'
        obj.Terms_Code = fndTrmsCode.Value
        obj.Cust_Account = fndAccntSet.Value
        obj.Payment_Code = fndPayCode.Value
        obj.Service_Tax_No = txtStaxNo.Text
        obj.Lst_No = txtLstNo.Text
        obj.Credit_Limit = 0
        obj.Form_Type = ""
        obj.CST = txtcst.Text
        obj.ECC = txtecc.Text
        obj.Range = txtrange.Text
        obj.Collectorate = txtcollect.Text
        obj.PAN = txtpan.Text
        obj.Division = txtdivision.Text
        obj.Tin_No = txtTinNo.Text

        If clsCommon.myCdbl(txtTempCreditLimit.Text) > 0 Then
            obj.TempCreditLimit = clsCommon.myCdbl(txtTempCreditLimit.Text)
            obj.TempCreditLimitFrom = clsCommon.myCDate(txttempCreditLimitFrom.Text)
            obj.TempCreditLimitTo = clsCommon.myCDate(txttempCreditLimitTo.Text)
        End If

        If ChkCheckCreditLimit.Checked = True Then
            obj.CheckCreditLimit = 1
        Else
            obj.CheckCreditLimit = 0
        End If

        If ChkOther.Checked = True Then
            obj.Other_For_PAN = 1
        Else
            obj.Other_For_PAN = 0
        End If

        'Modify 06 Oct By prabhakar'
        If chkHold.Checked = True Then
            obj.OnHold = "Y"                      '******* for:Hold ******** 
        ElseIf chkHold.Checked = False Then
            obj.OnHold = "N"                      '******* for:Remove Hold ********
        End If
        ''
        obj.Tax_Group = clsCommon.myCstr(fndTxGrp.Value)
        txtTxGrp.Text = clsDBFuncationality.getSingleValue("SELECT [Tax_Group_Desc] FROM [TSPL_TAX_GROUP_MASTER] where Tax_Group_Code='" + fndTxGrp.Value + "'")
        If (grdTax.Rows.Count > 0) Then
            obj.TAX1 = clsCommon.myCstr(grdTax.Rows(0).Cells(0).Value)
            obj.TAX1_Rate = clsCommon.myCdbl(grdTax.Rows(0).Cells(1).Value)
        End If
        If (grdTax.Rows.Count > 1) Then
            obj.TAX2 = clsCommon.myCstr(grdTax.Rows(1).Cells(0).Value)
            obj.TAX2_Rate = clsCommon.myCdbl(grdTax.Rows(1).Cells(1).Value)
        End If
        If (grdTax.Rows.Count > 2) Then
            obj.TAX3 = clsCommon.myCstr(grdTax.Rows(2).Cells(0).Value)
            obj.TAX3_Rate = clsCommon.myCdbl(grdTax.Rows(2).Cells(1).Value)
        End If
        If (grdTax.Rows.Count > 3) Then
            obj.TAX4 = clsCommon.myCstr(grdTax.Rows(3).Cells(0).Value)
            obj.TAX4_Rate = clsCommon.myCdbl(grdTax.Rows(3).Cells(1).Value)
        End If
        If (grdTax.Rows.Count > 4) Then
            obj.TAX5 = clsCommon.myCstr(grdTax.Rows(4).Cells(0).Value)
            obj.TAX5_Rate = clsCommon.myCdbl(grdTax.Rows(4).Cells(1).Value)
        End If
        If (grdTax.Rows.Count > 5) Then
            obj.TAX6 = clsCommon.myCstr(grdTax.Rows(5).Cells(0).Value)
            obj.TAX6_Rate = clsCommon.myCdbl(grdTax.Rows(5).Cells(1).Value)
        End If
        If (grdTax.Rows.Count > 6) Then
            obj.TAX7 = clsCommon.myCstr(grdTax.Rows(6).Cells(0).Value)
            obj.TAX7_Rate = clsCommon.myCdbl(grdTax.Rows(6).Cells(1).Value)
        End If
        If (grdTax.Rows.Count > 7) Then
            obj.TAX8 = clsCommon.myCstr(grdTax.Rows(7).Cells(0).Value)
            obj.TAX8_Rate = clsCommon.myCdbl(grdTax.Rows(7).Cells(1).Value)
        End If
        If (grdTax.Rows.Count > 8) Then
            obj.TAX9 = clsCommon.myCstr(grdTax.Rows(8).Cells(0).Value)
            obj.TAX9_Rate = clsCommon.myCdbl(grdTax.Rows(8).Cells(1).Value)
        End If
        If (grdTax.Rows.Count > 9) Then
            obj.TAX10 = clsCommon.myCstr(grdTax.Rows(9).Cells(0).Value)
            obj.TAX10_Rate = clsCommon.myCdbl(grdTax.Rows(9).Cells(1).Value)
        End If

        'Activity'
        obj.Cust_Category_Code = fndCusCategory.Value
        obj.Cust_Type_Code = fndCusType.Value
        obj.Route_No = fndRoute.Value
        obj.Salesman_Code = fndSalePerson.Value
        obj.Price_Group_Code = txtpgfnd.Value
        obj.Channel_Code = fndChannel.Value
        obj.Route_Group = fndroutegroup.Value
        obj.Zone_Code = fndZone.Value
        'Route_Desc = txtroutegroup.Text
        obj.Customer_Class = cboCustomerClass.Text
        If chkcredit.Checked = True Then
            obj.Credit_Customer = "Y"
        ElseIf chkcredit.Checked = False Then
            obj.Credit_Customer = "N"
        End If

        obj.Price_Code = clsCommon.myCstr(txtPriceCode.Value)
        obj.price_CodeNon = clsCommon.myCstr(txtPriceCodeNon.Value)
        obj.Price_Group_Code = clsCommon.myCstr(txtpgfnd.Value)

        'Additional Info'
        obj.Remarks1 = txtRemarks1.Text
        obj.Remarks2 = txtRemarks2.Text
        obj.Additional1 = txtAddInfo1.Text
        obj.Additional2 = txtAdd2.Text
        obj.Additional3 = txtAdd3.Text
        obj.Comp_Code = objCommonVar.CurrentCompanyCode
        obj.Created_By = objCommonVar.CurrentUserCode
        obj.Modified_By = objCommonVar.CurrentUserCode





        Dim arrDBName As New List(Of String)
        For ii As Integer = 0 To gvDB.Rows.Count - 1
            If (clsCommon.myCBool(gvDB.Rows(ii).Cells(colSelect).Value)) Then
                arrDBName.Add(clsCommon.myCstr(gvDB.Rows(ii).Cells(colDataBaseName).Value))
            End If
        Next

































        'obj.Customer_Class = ""
        'obj.Credit_Customer = ""
        'obj.LastInvoice_No =
        'obj.LastInvoice_Date = "01/01/2016"
        'obj.Price_Code = ""
        'obj.price_CodeNon = ""

        'obj.Inter_Branch = ""


        'obj.CURRENCY_CODE = fndCustCurrency.Value
        'obj.Franchise_Code = ""
        'obj.TDM_Code = fndTrmsCode.Value

        'obj.IsDistributor = ""
        'obj.Price_Group_Code = ""
        'obj.CSA_Type = ""

        ' obj.Category_Struct_Code = fndC


        GenratCustomerCode()
        If clsCommon.myLen(fndCustomer.Value) > 0 Then
            obj.Cust_Code = fndCustomer.Value
        Else
            common.clsCommon.MyMessageBoxShow("Customer Code cann't be blank.")
            Return
        End If
        obj.Customer_Name = txtCustomerName.Text
        obj.Cust_Group_Code = fndCusgrp.Value
        obj.Cust_Category_Code = fndCusCategory.Value
        obj.Cust_Account = fndAccntSet.Value


        If (obj.SaveData(obj, isNewEntry)) Then
            common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
            LoadDataSecondaryCustomer(obj.Cust_Code, NavigatorType.Current)

        End If
    End Sub

    Sub LoadDataSecondaryCustomer(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        fndCustomer.MyReadOnly = True
        btnSave.Enabled = True
        btnDelete.Enabled = True
        isNewEntry = False
        Dim obj As New clsSecondaryCustomerMasterInfo()
        obj = clsSecondaryCustomerMasterInfo.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Cust_Code) > 0) Then
            fndCustomer.Value = obj.Cust_Code
            txtCustomerName.Text = obj.Customer_Name
            txtAliesName.Text = obj.Alies_Name
            If IsDBNull(obj.Agg_Made_Date) = False AndAlso obj.Agg_Made_Date <> Nothing AndAlso clsCommon.myLen(obj.Agg_Made_Date) > 0 Then
                dtpAggMade.Text = clsCommon.GetPrintDate(obj.Agg_Made_Date, "dd/MM/yyyy")
            End If
            If IsDBNull(obj.Agg_Close_Date) = False AndAlso obj.Agg_Close_Date <> Nothing AndAlso clsCommon.myLen(obj.Agg_Close_Date) > 0 Then
                dtpAggClose.Text = clsCommon.GetPrintDate(obj.Agg_Close_Date, "dd/MM/yyyy")
            End If
            If obj.TRANSACTION_TYPE = "R" Then
                CmbTransaction.Text = "Retail"
            End If
            If obj.TRANSACTION_TYPE = "T" Then
                CmbTransaction.Text = "Tax"
            End If


            If obj.CUSTOMER_TYPE = "R" Or obj.CUSTOMER_TYPE = "Retailer" Then
                CmbCustomerType.Text = "Retailer"
            End If
            If obj.CUSTOMER_TYPE = "D" Or obj.CUSTOMER_TYPE = "Distributer" Then
                CmbCustomerType.Text = "Distributer"
            End If


            fndCustCurrency.Value = obj.CURRENCY_CODE
            fndCusgrp.Value = obj.Cust_Group_Code
            txtCusgrp.Text = clsDBFuncationality.getSingleValue(" SELECT Cust_Group_Desc as [Description] FROM [TSPL_CUSTOMER_GROUP_MASTER] where Cust_Group_Code='" + fndCusgrp.Value + "'")
            txtAdd1.Text = obj.Add1
            txtAdd2.Text = obj.Add2
            txtAdd3.Text = obj.Add3
            fndCountry.Value = obj.Country
            TxtCountryName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select country_name from tspl_country_master where country_code='" + fndCountry.Value + "'"))
            fndstate.Value = obj.State
            txtStateName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  STATE_NAME  from TSPL_State_MASTER where STATE_CODE ='" + fndstate.Value + "'"))
            fndCity.Value = obj.City_Code
            txtCity.Text = clsDBFuncationality.getSingleValue("SELECT [City_Name] FROM [TSPL_CITY_MASTER] where [City_Code]='" + fndCity.Value + "' ")
            txtPhone1.Text = obj.Phone1
            txtPhone2.Text = obj.Phone2
            txtEmail.Text = obj.Email
            txtWeb.Text = obj.WebSite
            txtfax.Text = obj.Fax
            txtPinNo.Text = obj.PIN_NO
            'Contact person'
            txtContactName.Text = obj.Contact_Person_Name
            txtContPhone.Text = obj.Contact_Person_Phone
            txtContactFax.Text = obj.Contact_Person_Fax
            txtContactWeb.Text = obj.Contact_Person_Website
            txtContactEmail.Text = obj.Contact_Person_Email
            'Process'
            fndTrmsCode.Value = obj.Terms_Code
            fndAccntSet.Value = obj.Cust_Account
            fndPayCode.Value = obj.Payment_Code
            txtStaxNo.Text = obj.Service_Tax_No
            txtLstNo.Text = obj.Lst_No
            txtCredit.Text = obj.Credit_Limit
            drpformtype.Text = obj.Form_Type


            txtTempCreditLimit.Text = obj.TempCreditLimit
            If IsDBNull(obj.TempCreditLimitFrom) <> True AndAlso IsNothing(obj.TempCreditLimitFrom) <> True Then
                txttempCreditLimitFrom.Text = clsCommon.GetPrintDate(obj.TempCreditLimitFrom, "dd/MM/yyyy")

            End If
            If IsDBNull(obj.TempCreditLimitTo) <> True AndAlso IsNothing(obj.TempCreditLimitTo) <> True Then
                txttempCreditLimitTo.Text = clsCommon.GetPrintDate(obj.TempCreditLimitTo, "dd/MM/yyyy")
            End If



            If clsCommon.myCdbl(obj.CheckCreditLimit) = 0 Then
                ChkCheckCreditLimit.Checked = False
            Else
                ChkCheckCreditLimit.Checked = True
            End If

            'Modify 06 Oct 2016 By prabhakar'
            If obj.OnHold = "Y" Then
                chkHold.Checked = True
            Else
                chkHold.Checked = False
            End If
            ''
            txtcst.Text = obj.CST
            txtecc.Text = obj.ECC
            txtrange.Text = obj.Range
            txtcollect.Text = obj.Collectorate
            txtpan.Text = obj.PAN
            txtdivision.Text = obj.Division

            txtTinNo.Text = obj.Tin_No
            If IsDBNull(obj.Other_For_PAN) <> True AndAlso IsNothing(obj.Other_For_PAN) <> True Then

                'ChkOther.Checked = IIf(clsCommon.myCstr(myDr(obj.Other_For_PAN)) = "1", True, False)
            End If

            If clsCommon.myLen(fndTxGrp.Value) > 0 Then
                fndTxGrp.Value = obj.Tax_Group
                txtTxGrp.Text = clsDBFuncationality.getSingleValue("SELECT [Tax_Group_Desc] FROM [TSPL_TAX_GROUP_MASTER] where Tax_Group_Code='" + fndTxGrp.Value + "'")
            End If


            grdTax.Rows.Clear()

            If IsDBNull(obj.TAX1) <> True AndAlso IsNothing(obj.TAX1) <> True AndAlso clsCommon.myLen(obj.TAX1) > 0 Then
                grdTax.Rows.AddNew()
                Me.grdTax.Rows(0).Cells(0).Value = clsCommon.myCstr(obj.TAX1)
                Me.grdTax.Rows(0).Cells(1).Value = obj.TAX1_Rate
            End If

            If IsDBNull(obj.TAX2) <> True AndAlso IsNothing(obj.TAX2) <> True AndAlso clsCommon.myLen(obj.TAX2) > 0 Then
                grdTax.Rows.AddNew()
                Me.grdTax.Rows(1).Cells(0).Value = clsCommon.myCstr(obj.TAX2)
                Me.grdTax.Rows(1).Cells(1).Value = obj.TAX2_Rate
            End If


            If IsDBNull(obj.TAX3) <> True AndAlso IsNothing(obj.TAX3) <> True AndAlso clsCommon.myLen(obj.TAX3) > 0 Then
                grdTax.Rows.AddNew()
                Me.grdTax.Rows(2).Cells(0).Value = clsCommon.myCstr(obj.TAX3)
                Me.grdTax.Rows(2).Cells(1).Value = obj.TAX3_Rate
            End If


            If IsDBNull(obj.TAX4) <> True AndAlso IsNothing(obj.TAX4) <> True AndAlso clsCommon.myLen(obj.TAX4) > 0 Then
                grdTax.Rows.AddNew()
                Me.grdTax.Rows(3).Cells(0).Value = clsCommon.myCstr(obj.TAX4)
                Me.grdTax.Rows(3).Cells(1).Value = obj.TAX4_Rate
            End If

            If IsDBNull(obj.TAX5) <> True AndAlso IsNothing(obj.TAX5) <> True AndAlso clsCommon.myLen(obj.TAX5) > 0 Then
                grdTax.Rows.AddNew()
                Me.grdTax.Rows(4).Cells(0).Value = clsCommon.myCstr(obj.TAX5)
                Me.grdTax.Rows(4).Cells(1).Value = obj.TAX5_Rate
            End If

            If IsDBNull(obj.TAX6) <> True AndAlso IsNothing(obj.TAX6) <> True AndAlso clsCommon.myLen(obj.TAX6) > 0 Then
                grdTax.Rows.AddNew()
                Me.grdTax.Rows(5).Cells(0).Value = clsCommon.myCstr(obj.TAX6)
                Me.grdTax.Rows(5).Cells(1).Value = obj.TAX6_Rate
            End If

            If IsDBNull(obj.TAX7) <> True AndAlso IsNothing(obj.TAX7) <> True AndAlso clsCommon.myLen(obj.TAX7) > 0 Then
                grdTax.Rows.AddNew()
                Me.grdTax.Rows(6).Cells(0).Value = clsCommon.myCstr(obj.TAX7)
                Me.grdTax.Rows(6).Cells(1).Value = obj.TAX7_Rate
            End If

            If IsDBNull(obj.TAX8) <> True AndAlso IsNothing(obj.TAX8) <> True AndAlso clsCommon.myLen(obj.TAX8) > 0 Then
                grdTax.Rows.AddNew()
                Me.grdTax.Rows(7).Cells(0).Value = clsCommon.myCstr(obj.TAX8)
                Me.grdTax.Rows(7).Cells(1).Value = obj.TAX8_Rate
            End If

            If IsDBNull(obj.TAX9) <> True AndAlso IsNothing(obj.TAX9) <> True AndAlso clsCommon.myLen(obj.TAX9) > 0 Then
                grdTax.Rows.AddNew()
                Me.grdTax.Rows(8).Cells(0).Value = clsCommon.myCstr(obj.TAX9)
                Me.grdTax.Rows(8).Cells(1).Value = obj.TAX9_Rate
            End If

            If IsDBNull(obj.TAX10) <> True AndAlso IsNothing(obj.TAX10) <> True AndAlso clsCommon.myLen(obj.TAX10) > 0 Then
                grdTax.Rows.AddNew()
                Me.grdTax.Rows(9).Cells(0).Value = clsCommon.myCstr(obj.TAX10)
                Me.grdTax.Rows(9).Cells(1).Value = obj.TAX10_Rate
            End If




            For ii As Integer = 0 To gvDB.Rows.Count - 1
                If clsCommon.CompairString(clsCommon.myCstr(gvDB.Rows(ii).Cells(colCompCode).Value), objCommonVar.CurrentCompanyCode) = CompairStringResult.Equal Then
                    gvDB.Rows(ii).Cells(colSelect).Value = True
                Else
                    gvDB.Rows(ii).Cells(colSelect).Value = False
                End If
            Next
            fndCusCategory.Value = obj.Cust_Category_Code
            txtCusgrp.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Cust_Group_Desc  from TSPL_CUSTOMER_GROUP_MASTER where Cust_Group_Code='" + fndCusgrp.Value + "'"))
            fndCusType.Value = obj.Cust_Type_Code
            fndRoute.Value = obj.Route_No
            fndSalePerson.Value = obj.Salesman_Code
            fndZone.Value = obj.Zone_Code
            txtPriceCode.Value = obj.Price_Code
            txtpgfnd.Value = obj.Price_Group_Code
            txtPriceCodeNon.Value = obj.price_CodeNon

            If clsCommon.myLen(txtpgfnd.Value) > 0 Then
                chkpricegrpslctr.Checked = True
            ElseIf clsCommon.myLen(txtpgfnd.Value) = 0 Then
                chkpricegrpslctr.Checked = False
            End If
            PriceCodeEnable()
            fndChannel.Value = obj.Channel_Code
            txtChannel.Text = clsDBFuncationality.getSingleValue("SELECT [Channel_Name] FROM [TSPL_CHANNEL_MASTER] where Channel_Id='" + fndChannel.Value + "'")
            fndroutegroup.Value = obj.Route_Group
            txtroutegroup.Text = clsDBFuncationality.getSingleValue("select  Description from  TSPL_ROUTE_GROUP_MASTER where Group_Id='" + fndroutegroup.Value + "'  ")
            LoadCus()
            txtRemarks1.Text = obj.Remarks1
            txtRemarks2.Text = obj.Remarks2
            txtAddInfo1.Text = obj.Additional1
            txtAddInfo2.Text = obj.Additional2
            txtAddInfo3.Text = obj.Additional3
            txtParentCstNo.Value = obj.Parent_Customer_No

            Dim qryDesc As String = ""
            If CmbCustomerType.Text = "Retailer" Or CmbCustomerType.Text = "R" Then
                qryDesc = "select Customer_name From TSPL_SECONDARY_CUSTOMER_MASTER  where Cust_Code='" + txtParentCstNo.Value + "' "
            End If
            If CmbCustomerType.Text = "Distributer" Or CmbCustomerType.Text = "D" Then
                qryDesc = "select Customer_name From TSPL_CUSTOMER_MASTER  where Cust_Code='" + txtParentCstNo.Value + "' "
            End If
            If qryDesc IsNot "" Then
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qryDesc)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    txtParentCstmrNo.Text = clsCommon.myCstr(dt.Rows(0)("Customer_name"))
                Else
                    txtParentCstmrNo.Text = ""
                End If
            Else
                txtParentCstmrNo.Text = ""
            End If
            If isCheckCustomerType Then
                CmbCustomerType.SelectedValue = obj.POS_Type
            End If








        End If

    End Sub

    Private Sub fndCustomer__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles fndCustomer._MYNavigator
        Try
            LoadDataSecondaryCustomer(fndCustomer.Value, NavType)


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndCustomer__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCustomer._MYValidating
        If isButtonClicked = True Then
            Dim qry As String = " SELECT [Cust_Code] ,[Customer_Name],[Add1],[Add2],[Add3],[Distributor],[City_Code],[State],[Country] ,[Phone1],[Phone2],[Fax],[Email],[WebSite],[Credit_Limit] ,[CURRENCY_CODE] ,[Status],[Created_By],[Created_Date],[Modified_By],[Modified_Date] ,[Comp_Code],[Closing_Date],[Cust_Category_Code],[Cust_Group_Code] ,[Cust_Type_Code],[Route_No],[Route_Desc],[Price_Code],[Contact_Person_Name],[Contact_Person_Phone],[Contact_Person_Fax],[Contact_Person_Website] ,[Contact_Person_Email],[Terms_Code],[Cust_Account],[Tax_Group],[TAX1],[TAX1_Rate],[TAX2],[TAX2_Rate],[TAX3],[TAX3_Rate],[TAX4] ,[TAX4_Rate],[TAX5] ,[TAX5_Rate] ,[TAX6],[TAX6_Rate],[TAX7],[TAX7_Rate],[TAX8],[TAX8_Rate],[TAX9],[TAX9_Rate],[TAX10],[TAX10_Rate],[Payment_Code],[Service_Tax_No],[Tin_No],[Lst_No],[Form_Type] ,[Channel_Code],[Channel_Desc],[OnHold],[Remarks1],[Remarks2],[Additional1],[Additional2],[Additional3],[Salesman_Code],[Salesman_Desc],[Visi_Id],[Visi_Desc] ,[OutLet_Commossion],[Balance_ToDate],[price_CodeNon],[Credit_Limit_Alert_Type],[PIN_Code],[Cust_DOB],[Cust_Spouse_DOB],[Anniversary_Date],[Gender],[Occation],[CST],[ECC],[Range] ,[Collectorate] ,[PAN],[Division],[Parent_Customer_No],[Route_Group],[Customer_Class],[Credit_Customer],[LastInvoice_No],[LastInvoice_Date] ,[Inter_Branch],[TRANSACTION_TYPE],[CUSTOMER_TYPE],[Agg_Made_Date],[Agg_Close_Date],[Parent_Customer_YN],[Service_Dealer_Code],[TDM_Code],[IsDistributor],[Price_Group_Code],[CSA_Type],[Category_Struct_Code],[TempCreditLimit],[TempCreditLimitFrom],[TempCreditLimitTo],[Alies_Name],[Zone_Code],[CheckCreditLimit],[PIN_NO],[Struct_Code],[Crate_Opening],[Crate_Opening_Date],[Franchise_Code],[Other_For_PAN],[Cust_IntegratedCRM]  FROM TSPL_SECONDARY_CUSTOMER_MASTER  "
            LoadDataSecondaryCustomer(clsCommon.ShowSelectForm("SECOCUST", qry, "Cust_Code", "", fndCustomer.Value, "Cust_Code", isButtonClicked), NavigatorType.Current)
        End If
    End Sub

    Private Sub fndCustCurrency__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCustCurrency._MYValidating
        fndCustCurrency.Value = clsCurrencyMaster.getFinder("", fndCustCurrency.Value, isButtonClicked)
    End Sub
    Sub GenratCustomerCode()
        Dim AllowCustCode As String = ""
        Dim AutoCustCode As String = ""
        Dim CustName As String = ""
        AllowCustCode = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AutoGeneratedCustomerCode, clsFixedParameterCode.AutoGeneratedCustomerCode, Nothing))
        If clsCommon.CompairString(AllowCustCode, "1") = CompairStringResult.Equal AndAlso clsCommon.myLen(fndCustomer.Value.Trim()) <= 0 Then
            If clsCommon.myLen(txtCustomerName.Text) > 0 Then
                CustName = txtCustomerName.Text.Substring(0, 1)
                AutoCustCode = clsSecondaryCustomerMasterInfo.GetVendorNextCode("TSPL_SECONDARY_CUSTOMER_MASTER", "Customer_Name", CustName, Nothing)
                fndCustomer.Value = AutoCustCode
            Else
                CustName = ""
                AutoCustCode = clsERPFuncationality.GetVendorNextCode("TSPL_SECONDARY_CUSTOMER_MASTER", "Customer_Name", CustName, Nothing)
                fndCustomer.Value = AutoCustCode
            End If
        Else
            fndCustomer.Value = fndCustomer.Value
        End If

    End Sub

    Public Sub PriceCodeEnable()
        If chkpricegrpslctr.Checked = True Then
            txtPriceCode.Enabled = False
            txtPriceCodeNon.Enabled = False
            txtpgfnd.Enabled = True
            txtPriceCode.Value = ""
            txtPriceCodeNon.Value = ""
        ElseIf chkpricegrpslctr.Checked = False Then
            txtPriceCode.Enabled = True
            txtPriceCodeNon.Enabled = True
            txtpgfnd.Enabled = False
            txtpgfnd.Value = ""
        End If
    End Sub

    Sub LoadCus()
        Try
            Dim strvalue As String
            Dim strquery As String = "select Cust_Type_Desc  from TSPL_CUSTOMER_TYPE_MASTER where Cust_Type_Code='" + fndCusType.Value + "'"

            strvalue = clsDBFuncationality.getSingleValue(strquery)
            If strvalue <> "" Then
                cboCustomerClass.Text = clsCommon.myCstr(strvalue)
            Else
                cboCustomerClass.Text = "Select"
            End If



        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message())
        End Try
    End Sub


    Private Sub FrmSecondaryCustomerMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        'clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/mm/yyyy")
        isCheckCustomerType = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowLoginType, clsFixedParameterCode.AllowLoginType, Nothing)) = 1, True, False)
        txtCustomerName.MaxLength = 200


        pageCus.SelectedPage = RadPageViewPage1
        Me.fndSalePerson.Enabled = False
        Me.txtSalesPerson.Enabled = False
        Me.dtClosing.Enabled = False
        txtCustomerName.ReadOnly = False

        txtCredit.MaxLength = 12

        txtTempCreditLimit.MaxLength = 12
        ''-----------------------
        btnDelete.Enabled = False
        drpformtype.SelectedIndex = 0
        chkInActive.Enabled = False

        dtClosing.Value = connectSql.serverDate()
        LoadCustomerType()
        SetDataBaseGrid()
        LoadBlankGrid()
        gvCrate.Rows.AddNew()
        Dim startdate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        startdate.Format = DateTimePickerFormat.Custom
        startdate.CustomFormat = "dd-MM-yyyy"
        startdate.HeaderText = "Start Date"
        startdate.FormatString = "{0:d}"
        startdate.Name = ColItemStartDate
        startdate.WrapText = True
        startdate.ReadOnly = False
        startdate.Width = 80


        Dim repoExpiry As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoExpiry.Format = DateTimePickerFormat.Custom
        repoExpiry.CustomFormat = "dd-MM-yyyy"
        repoExpiry.HeaderText = "Valid Upto"
        repoExpiry.FormatString = "{0:d}"
        repoExpiry.Name = ColItemValidUpto
        repoExpiry.WrapText = True
        repoExpiry.ReadOnly = False
        repoExpiry.Width = 80

        cboCustomerClass.Text = "Select"
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New Trasnaction")
        LoadTransactionType()
        ValidateLength()
        ApplyReadOPR()


        pageCus.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.Report_ID = MyBase.Form_ID
            UcCustomFields1.LoadCustomControls()
        End If





        Dim qry As String = "select IsPriceGrpCodeOnCstMst from TSPL_INV_PARAMETERS"
        Dim value As Integer = clsDBFuncationality.getSingleValue(qry)

        If value > 0 Then
            chkpricegrpslctr.Visible = True
            txtpgfnd.Visible = True
            MyLabel3.Visible = True
            chkpricegrpslctr.Checked = False
            PriceCodeEnable()
        ElseIf value <= 0 Then
            chkpricegrpslctr.Visible = False
            txtpgfnd.Visible = False
            MyLabel3.Visible = False
        End If

        '---------------------------------------------------------------------


        UcAttachment1.Form_ID = MyBase.Form_ID
        UcAttachment1.BlankAllControls()
        pageCus.Pages("Attachments").Item.Visibility = ElementVisibility.Visible

        '---------------------------------------------


        FillCustName()

        GetCustomerType()


        If DrillDown_transType IsNot Nothing AndAlso clsCommon.CompairString(clsCommon.myCstr(DrillDown_transType), "Export") = CompairStringResult.Equal Then
            Me.fndCustCurrency.Visible = True
            Me.lblBaseCurrency.Visible = True
        End If

        If clsCommon.myLen(fndCustomer.Value) > 0 Then
            LoadData()

        End If
        If DrillDown_FormName IsNot Nothing AndAlso clsCommon.myLen(DrillDown_FormName) > 0 Then
            btnNew.Enabled = False
            btnDelete.Enabled = False
            btnClose.Text = "Cancel"
            isNewEntry = True
            Me.WindowState = FormWindowState.Maximized
        End If

        txtpan.CharacterCasing = CharacterCasing.Upper


    End Sub

    Public Sub GetCustomerType()

        Dim dt As New DataTable()
        If isCheckCustomerType = True Then
            Dim qry As String = "SELECT GROUP_CODE,DESCRIPTION FROM  TSPL_POS_GROUP_MASTER  WHERE LEVEL>1 ORDER BY LEVEL ASC"
            dt = clsDBFuncationality.GetDataTable(qry)
            CmbCustomerType.DataSource = dt
            CmbCustomerType.DisplayMember = "DESCRIPTION"
            CmbCustomerType.ValueMember = "GROUP_CODE"
        Else
            If dt.Columns.Count = 0 Then
                dt.Columns.Add("GROUP_CODE")
                dt.Columns.Add("DESCRIPTION")
            End If
            dt.Rows.Add("Retailer", "Retailer")
            dt.Rows.Add("Distributer", "Distributer")
            CmbCustomerType.DataSource = dt
            CmbCustomerType.DisplayMember = "DESCRIPTION"
            CmbCustomerType.ValueMember = "GROUP_CODE"
        End If
        CmbCustomerType.SelectedValue = Nothing
    End Sub

    Private Sub LoadTransactionType()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "R"
        dr("Name") = "Retail"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "T"
        dr("Name") = "Tax"
        dt.Rows.Add(dr)

        CmbTransaction.DataSource = dt
        CmbTransaction.ValueMember = "Code"
        CmbTransaction.DisplayMember = "Name"
    End Sub
    Private Sub ValidateLength()
        fndCustomer.MyMaxLength = 12
        txtCustomerName.MaxLength = 200
        txtAdd1.MaxLength = 75
        txtAdd2.MaxLength = 75
        txtAdd3.MaxLength = 75
        txtCity.MaxLength = 50
        txtStateName.MaxLength = 50

        txtfax.MaxLength = 12

        txtEmail.MaxLength = 50
        txtWeb.MaxLength = 50
        txtContactFax.MaxLength = 20

        txtContactEmail.MaxLength = 50
        txtContactName.MaxLength = 50
        txtContactWeb.MaxLength = 50
        txtRemarks1.MaxLength = 200
        txtRemarks2.MaxLength = 200
        txtAddInfo1.MaxLength = 50
        txtAddInfo2.MaxLength = 50
        txtAddInfo3.MaxLength = 50
        txtcst.MaxLength = 30
        txtecc.MaxLength = 30
        txtrange.MaxLength = 30
        txtcollect.MaxLength = 30
        txtpan.MaxLength = 30
        txtdivision.MaxLength = 30

    End Sub

    Private Sub ApplyReadOPR()
        txtCusgrp.ReadOnly = True
        txtCity.ReadOnly = True
        txtStateName.ReadOnly = True
    End Sub

    Private Sub grdTax_EditorRequired(sender As Object, e As EditorRequiredEventArgs) Handles grdTax.EditorRequired
        Dim str As String = "select Tax_Rate as [Tax Rate] from TSPL_TAX_RATES where Tax_Code = '" + grdTax.CurrentRow.Cells(0).Value + "'"
        Dim gvMultiComboColum As GridViewComboBoxColumn = TryCast(grdTax.Columns(1), GridViewComboBoxColumn)

        myDs = connectSql.RunSQLReturnDS(str)
        gvMultiComboColum.DataSource = myDs.Tables(0)
        gvMultiComboColum.ValueMember = "Tax Rate"
    End Sub

    Sub funDelete()
        Try
            If clsCommon.myLen(fndCustomer.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("You Cannot Delete Record")
                Exit Sub
            End If
            If (myMessages.deleteConfirm()) Then
                If (clsSecondaryCustomerMasterInfo.DeleteData(fndCustomer.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    Reset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub txtParentCstNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtParentCstNo._MYValidating
        If String.IsNullOrEmpty(CmbCustomerType.Text) Then
            If CmbCustomerType.Text = "Select" Then
                common.clsCommon.MyMessageBoxShow("Firstly select Customer No")
                Return
            End If

        End If
        If isCheckCustomerType = False Then


            If CmbCustomerType.Text = "Retailer" Then
                Dim Qry As String = " select Cust_Code ,Customer_Name from TSPL_SECONDARY_CUSTOMER_MASTER "
                Dim Whrcls As String = " IsDistributor = 'Y' "

                txtParentCstNo.Value = clsCommon.ShowSelectForm("CUSTTYPE", Qry, "Cust_Code", Whrcls, txtParentCstNo.Value, "", isButtonClicked)
                ' txtRoute.Text = clsDBFuncationality.getSingleValue("SELECT [Route_Desc] FROM [TSPL_ROUTE_MASTER] where Route_No='" + fndRoute.Value + "' ")

                'txtParentCstNo.Value = clsCustomerMaster.getFinder(Whrcls, txtParentCstNo.Value, isButtonClicked)
                Dim qryDesc As String = ""
                If CmbCustomerType.Text = "Retailer" Or CmbCustomerType.Text = "R" Then
                    qryDesc = "select Customer_name From TSPL_SECONDARY_CUSTOMER_MASTER  where Cust_Code='" + txtParentCstNo.Value + "' "
                End If
                If CmbCustomerType.Text = "Distributer" Or CmbCustomerType.Text = "D" Then
                    qryDesc = "select Customer_name From TSPL_CUSTOMER_MASTER  where Cust_Code='" + txtParentCstNo.Value + "' "
                End If

                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qryDesc)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    txtParentCstmrNo.Text = clsCommon.myCstr(dt.Rows(0)("Customer_name"))



                Else
                    txtParentCstmrNo.Text = ""

                    txtCredit.Text = ""
                    txtCredit.Enabled = True

                End If

            End If


            If CmbCustomerType.Text = "Distributer" Then
                Dim Qry As String = "select Cust_Code ,Customer_Name from TSPL_CUSTOMER_MASTER "

                txtParentCstNo.Value = clsCommon.ShowSelectForm("CUSTTYPED", Qry, "Cust_Code", "", txtParentCstNo.Value, "", isButtonClicked)
                ' txtParentCstNo.Value = clsCustomerMaster.getFinder("", txtParentCstNo.Value, isButtonClicked)

                Dim qryDesc As String = "select Customer_name From TSPL_CUSTOMER_MASTER "
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qryDesc)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    txtParentCstmrNo.Text = clsCommon.myCstr(dt.Rows(0)("Customer_name"))



                Else
                    txtParentCstmrNo.Text = ""



                End If
            End If

        Else
            Dim qry As String = "SELECT level FROM  TSPL_POS_GROUP_MASTER where group_code='" + clsCommon.myCstr(CmbCustomerType.SelectedValue) + "'"
            Dim Level As Integer = clsDBFuncationality.getSingleValue(qry)
            If Level = 2 Then
                Dim Qry1 As String = "select Cust_Code ,Customer_Name from TSPL_CUSTOMER_MASTER "

                txtParentCstNo.Value = clsCommon.ShowSelectForm("CUSTTYPED", Qry1, "Cust_Code", "", clsCommon.myCstr(txtParentCstNo.Value), "", isButtonClicked)
                Dim qryDesc As String = "select Customer_name From TSPL_CUSTOMER_MASTER where Cust_Code='" + clsCommon.myCstr(txtParentCstNo.Value) + "'"
                txtParentCstmrNo.Text = clsDBFuncationality.getSingleValue(qryDesc)
            ElseIf Level > 2 Then
                Dim Qry3 As String = "select Cust_Code ,Customer_Name from TSPL_SECONDARY_CUSTOMER_MASTER join TSPL_POS_GROUP_MASTER on TSPL_POS_GROUP_MASTER.group_code=TSPL_SECONDARY_CUSTOMER_MASTER.pos_type "
                Dim whCls As String = " level= (select max(level) from TSPL_POS_GROUP_MASTER where [level]<'" + clsCommon.myCstr(Level) + "')"
                    txtParentCstNo.Value = clsCommon.ShowSelectForm("CUSTTYPED", Qry3, "Cust_Code", whCls, "Cust_Code", "", isButtonClicked)
                    Dim qryDesc As String = "select Customer_name From TSPL_SECONDARY_CUSTOMER_MASTER where Cust_Code='" + clsCommon.myCstr(txtParentCstNo.Value) + "'"
                    txtParentCstmrNo.Text = clsDBFuncationality.getSingleValue(qryDesc)
            End If
        End If


    End Sub


    Private Sub RadMenuItem3_Click(sender As Object, e As EventArgs) Handles RadMenuItem3.Click
        funExport()
    End Sub
    Public Sub funExport()
        Try
            strCmd = "select Cust_Code,Customer_Name,Add1,Add2,Add3,Phone1,Phone2,Credit_Limit,CUSTOMER_TYPE,IsDistributor,Parent_Customer_No,Parent_Customer_YN,Created_By,convert (varchar ,Created_Date,103 ) as Created_Date,    Modified_By,convert (date , Modified_Date,103 ) as Modified_Date  ,Comp_Code,OnHold, City_Code , State , Country , Fax , Email , WebSite , PIN_NO , Contact_Person_Name , Contact_Person_Phone , Contact_Person_fax , Contact_Person_Website , Contact_Person_Email , TAX1 , TAX1_Rate , TAX2 , TAX2_Rate , TAX3 , TAX3_Rate , TAX4 , TAX4_Rate , TAX5 , TAX5_Rate , TAX6 , TAX6_Rate , TAX7 , TAX7_Rate , TAX8 , TAX8_Rate , TAX9 , TAX9_Rate , TAX10 , TAX10_Rate , Remarks1 , Remarks2 , Additional1 , Additional2 , Additional3 , Payment_Code , Service_Tax_No , Tin_No , Lst_No , Form_Type , Channel_Code , Channel_Desc , Credit_Customer , Customer_Class , Cust_Category_Code , Zone_Code ,  Route_No , Salesman_Code , price_group_code , Route_Group , Terms_Code , Cust_Account , TempCreditLimit , convert (varchar ,TempCreditLimitFrom,103 ) as TempCreditLimitFrom ,  convert (varchar ,TempCreditLimitTo,103 ) as TempCreditLimitTo , Tax_Group , Alies_Name  , convert (varchar ,Agg_Made_Date,103 ) as Agg_Made_Date ,convert (varchar ,Agg_Close_Date,103 ) as Agg_Close_Date  , CURRENCY_CODE , Cust_Group_Code , CST  , ECC ,  Range ,  Collectorate , PAN ,  Division     from TSPL_SECONDARY_CUSTOMER_MASTER "
            transportSql.ExporttoExcel(strCmd, Me)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Customer")
        End Try

    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        funImport()
    End Sub
    Public Sub funImport()
        Dim AllowAutoCCode As String = ""
        Dim strCusCode As String = ""
        Dim gv As New RadGridView()

        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today

        If transportSql.importExcel(gv, "Cust_Code", "Customer_Name", "Add1", "Add2", "Add3", "Phone1", "Phone2", "Credit_Limit", "IsDistributor", "Parent_Customer_No", "Parent_Customer_YN", "Created_By", "Created_Date", "Modified_By", "Modified_Date", "Comp_Code", "OnHold", "City_Code", "State", "Country", "Fax", "Email", "WebSite", "PIN_NO", "Contact_Person_Name", "Contact_Person_Phone", "Contact_Person_fax", "Contact_Person_Website", "Contact_Person_Email", "TAX1", "TAX1_Rate", "TAX2", "TAX2_Rate", "TAX3", "TAX3_Rate", "TAX4", "TAX4_Rate", "TAX5", "TAX5_Rate", "TAX6", "TAX6_Rate", "TAX7", "TAX7_Rate", "TAX8", "TAX8_Rate", "TAX9", "TAX9_Rate", "TAX10", "TAX10_Rate", "Remarks1", "Remarks2", "Additional1", "Additional2", "Additional3", "Payment_Code", "Service_Tax_No", "Tin_No", "Lst_No", "Form_Type", "Channel_Code", "Channel_Desc", "Credit_Customer", "Customer_Class", "Cust_Category_Code", "Zone_Code", "Route_No", "Salesman_Code", "price_group_code", "Route_Group", "Terms_Code", "Cust_Account", "TempCreditLimit", "TempCreditLimitFrom", "TempCreditLimitTo", "Tax_Group", "Alies_Name", "Agg_Made_Date", "Agg_Close_Date", "CURRENCY_CODE", "Cust_Group_Code", "CST", "ECC", "Range", "Collectorate", "PAN", "Division") Then
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Dim ii As Integer = 0
            Try
                'clsCommon.ProgressBarShow()
                clsCommon.ProgressBarPercentShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim LineNo As String = clsCommon.myCstr(grow.Index) + 2
                    ii += 1
                    clsCommon.ProgressBarPercentUpdate((ii * 100) / gv.RowCount - 1, "Importing " + clsCommon.myCstr(ii) + "/" + clsCommon.myCstr(gv.RowCount - 1))
                    strCusCode = clsCommon.myCstr(grow.Cells("Cust_Code").Value)
                    Dim coll As New Hashtable()
                    'If clsCommon.myLen(strCusCode) > 0 Then

                    'AllowAutoCCode = clsCommon.myCstr(clsSecondaryCustomerMasterInfo.GetData(clsFixedParameterType.AutoGeneratedCustomerCode, clsFixedParameterCode.AutoGeneratedCustomerCode, trans))
                    'If clsCommon.CompairString(AllowAutoCCode, "0") = CompairStringResult.Equal Then
                    '    If clsCommon.myLen(strCusCode) > 0 Then


                    '        If clsCommon.myLen(strCusCode) > 12 Then
                    '            Throw New Exception("Customer code can not be greater than 12 characters at line '" + LineNo + "'.")
                    '        Else
                    '            ' clsCommon.AddColumnsForChange(coll, "Cust_Code", strCusCode)
                    '        End If
                    '    Else
                    '        Throw New Exception("Customer code can not be left blank at line '" + LineNo + "'.")
                    '    End If
                    'Else

                    'End If

                    ''End If

                    Dim CustomerCode As String = clsCommon.myCstr(grow.Cells("Cust_Code").Value)
                    'If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select count(*) from TSPL_SECONDARY_CUSTOMER_MASTER where Cust_Code= '" & clsCommon.myCstr(CustomerCode) & "'", trans)) > 0 Then
                    '    Throw New Exception("Same Customer Code is exist with another customer so please change customer Code because Customer code is unique at line '" + LineNo + "'.")
                    'End If
                    If (clsCommon.myLen(CustomerCode) < 0) Then
                        Throw New Exception(" Customer Code cannot be blank at line '" + LineNo + "'.")
                    End If
                    clsCommon.AddColumnsForChange(coll, "Cust_Code", CustomerCode)

                    Dim onHolds As Char = clsCommon.myCstr(grow.Cells("OnHold").Value)
                    If onHolds = "Y" Or onHolds = "y" Then
                        clsCommon.AddColumnsForChange(coll, "OnHold", onHolds)
                    Else
                        clsCommon.AddColumnsForChange(coll, "OnHold", "N")
                    End If



                    Dim CustName As String = clsCommon.myCstr(grow.Cells("Customer_Name").Value)
                    If clsCommon.myLen(CustName) > 0 Then
                        If clsCommon.myLen(CustName) > 200 Then
                            Throw New Exception("Please enter customer name against code '" + strCusCode + "' having max length 200.")
                        End If
                        'richa agarwal 13 july,2017 to check customer exist with same name
                        ' If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.CustomerNameUniqueOnCM & "' and Type ='" & clsFixedParameterType.CustomerNameUniqueOnCM & "'", trans)), "1") = CompairStringResult.Equal Then
                        If (clsCommon.myLen(CustName) < 0) Then
                            Throw New Exception(" Customer Name can not be blank  is unique at line '" + LineNo + "'.")
                        End If
                        'End If

                    Else
                        Throw New Exception("Please enter customer name against code '" + strCusCode + "' at Line No '" + LineNo + "'.")
                    End If
                    clsCommon.AddColumnsForChange(coll, "Customer_Name", CustName)



                    Dim strParentCstmrNo As String = clsCommon.myCstr(grow.Cells("Parent_Customer_No").Value)
                    If clsCommon.myLen(strParentCstmrNo) > 12 Then
                        Throw New Exception("Check the length  of Parent Customer No at Line No '" + LineNo + "'")
                        trans.Rollback()
                        Exit Sub
                    Else
                        ' clsCommon.AddColumnsForChange(coll, "Parent_Customer_No", strParentCstmrNo)
                    End If
                    '==shivani against[BM00000007853]
                    Dim strCusGrp As String = clsCommon.myCstr(grow.Cells("Cust_Group_Code").Value)
                    If clsCommon.myLen(strCusGrp) > 0 Then
                        Dim custgroup As String = clsDBFuncationality.getSingleValue("select Cust_Group_Code  from TSPL_CUSTOMER_GROUP_MASTER  where Cust_Group_Code ='" + strCusGrp + "'", trans)
                        If clsCommon.CompairString(custgroup, strCusGrp) = CompairStringResult.Equal Then
                            clsCommon.AddColumnsForChange(coll, "Cust_Group_Code", custgroup)
                        Else
                            Throw New Exception("This Customer Group Code does not Exist Against Customer '" + strCusCode + "' at Line No '" + LineNo + "'")
                        End If
                    Else
                        Throw New Exception("Please Fill the Group Code,it is mandatory Against Customer '" + strCusCode + "'")
                        Exit Sub
                    End If
                    '=====================================


                    ' Dim custgroup1 As String


                    'trans.Rollback()
                    'Exit Sub
                    'End If

                    Dim strCusTrms As String = clsCommon.myCstr(grow.Cells("Terms_Code").Value)
                    Dim trmscode As String = clsDBFuncationality.getSingleValue("select Terms_Code  from TSPL_TERMS_MASTER where Terms_Code ='" + strCusTrms + "'", trans)
                    'Dim trmscode1 As String
                    If clsCommon.CompairString(trmscode, strCusTrms) = CompairStringResult.Equal Then
                        clsCommon.AddColumnsForChange(coll, "Terms_Code", trmscode)
                    Else
                        Throw New Exception("This Customer Terms Code Does not Exist Against Customer '" + strCusCode + "' at Line No '" + LineNo + "'")
                        'trans.Rollback()
                        'Exit Sub
                    End If

                    Dim Cust_Category_Code As String = String.Empty
                    If clsCommon.myLen(grow.Cells("Cust_Category_Code").Value) > 0 Then
                        Dim custcategroycode As String = clsCommon.myCstr(clsCommon.myCstr(grow.Cells("Cust_Category_Code").Value))
                        Cust_Category_Code = clsDBFuncationality.getSingleValue("select Cust_Category_Code  from TSPL_CUSTOMER_CATEGORY_MASTER  where Cust_Category_Code ='" + custcategroycode + "'", trans)
                        If clsCommon.CompairString(Cust_Category_Code, custcategroycode) = CompairStringResult.Equal Then
                            clsCommon.AddColumnsForChange(coll, "Cust_Category_Code", Cust_Category_Code, True)
                        Else
                            Throw New Exception("The Customer Category is not Present in Category Master Against Customer '" + strCusCode + "' at Line No '" + LineNo + "'")
                            'trans.Rollback()
                            'Exit Sub
                        End If
                    Else
                        '  Throw New Exception("Please Fill Customer Category Code Against Customer '" + strCusCode + "' at Line No '" + LineNo + "'")
                        clsCommon.AddColumnsForChange(coll, "Cust_Category_Code", Cust_Category_Code, True)
                    End If


                    'Dim priceCode As String = clsCommon.myCstr(grow.Cells("Excisable Price Code").Value)
                    'If clsCommon.myLen(Cust_Category_Code) > 0 Then
                    '    Dim Price_Code As Integer = CInt(clsDBFuncationality.getSingleValue("select Count(*)  from TSPL_CUSTOMER_CATEGORY_MASTER  where CUST_CATEGORY_CODE  ='" + Cust_Category_Code + "'", trans))
                    '    If Price_Code > 0 Then
                    '        clsCommon.AddColumnsForChange(coll, "Price_Code", priceCode)
                    '    Else
                    '        Throw New Exception("This Customer Price Code does not exist Against Customer '" + strCusCode + "' at Line No '" + LineNo + "'")
                    '        'trans.Rollback()
                    '        'Exit Sub
                    '    End If
                    'Else
                    '    clsCommon.AddColumnsForChange(coll, "Price_Code", priceCode, True)
                    'End If


                    'clsCommon.AddColumnsForChange(coll, "Price_CodeNon", clsCommon.myCstr(grow.Cells("Price Code Non-Excise").Value))

                    '===========Added By Rohit June 06,2015 04:33 PM============
                    'If isDisplayFranchiseDetails Then
                    '    Dim FranchiseCode As String = clsCommon.myCstr(grow.Cells("Franchise Code").Value)
                    '    If clsCommon.myLen(FranchiseCode) > 0 Then
                    '        Dim Franchise_Code As Integer = CInt(clsDBFuncationality.getSingleValue("select Count(*)  from TSPL_Vendor_MASTER  where Vendor_Code  ='" & FranchiseCode & "'", trans))
                    '        If Franchise_Code > 0 Then
                    '            clsCommon.AddColumnsForChange(coll, "Franchise_Code", FranchiseCode)
                    '        Else
                    '            Throw New Exception("This Customer Franchise Code does not exist Against Customer '" + strCusCode + "' at Line No '" + LineNo + "'")
                    '            'trans.Rollback()
                    '            'Exit Sub
                    '        End If
                    '    End If
                    'End If

                    '==============================================================

                    Dim CusAccount As String = clsCommon.myCstr(grow.Cells("Cust_Account").Value)
                    Dim Cust_Account As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cust_account from TSPL_CUSTOMER_ACCOUNT_SET where Cust_Account ='" + CusAccount + "'", trans))
                    If clsCommon.CompairString(Cust_Account, CusAccount) = CompairStringResult.Equal Then
                        clsCommon.AddColumnsForChange(coll, "Cust_Account", Cust_Account, True)
                    Else
                        Throw New Exception(" This Customer Account does not exist In Master at Line No '" + LineNo + "'")
                        'trans.Rollback()
                        'Exit Sub
                    End If

                    Dim TaxGroup As String = clsCommon.myCstr(grow.Cells("Tax_Group").Value)
                    Dim Tax_Group As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select tax_group_code from TSPL_TAX_GROUP_MASTER  where Tax_Group_Code  ='" + TaxGroup + "'", trans))
                    If clsCommon.CompairString(Tax_Group, TaxGroup) = CompairStringResult.Equal Then
                        clsCommon.AddColumnsForChange(coll, "Tax_Group", Tax_Group, True)
                    Else
                        Throw New Exception("This Customer Tax Group does not exist at '" + LineNo + "'")
                        'trans.Rollback()
                        'Exit Sub
                    End If

                    Dim Route_No As String = ""
                    Dim RouteNo As String = clsCommon.myCstr(grow.Cells("Route_No").Value)
                    If clsCommon.myLen(grow.Cells("Route_No").Value) > 0 Then
                        Route_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Route_No from TSPL_ROUTE_MASTER Where Route_No='" + RouteNo + "'", trans))
                        If clsCommon.CompairString(Route_No, RouteNo) = CompairStringResult.Equal Then
                            clsCommon.AddColumnsForChange(coll, "Route_No", Route_No)
                        Else
                            Throw New Exception("The Route No Does Not Exists for Customer '" + strCusCode + "' at Line No '" + LineNo + "'")
                            'trans.Rollback()
                            'Exit Sub
                        End If
                    Else
                        ''Throw New Exception("Please fill The Route Code for Customer '" + strCusCode + "' at Line No '" + LineNo + "'")
                        ''trans.Rollback()
                        ''Exit Sub
                        clsCommon.AddColumnsForChange(coll, "Route_No", Route_No, True)
                    End If

                    Dim Salesman_Code As String = ""
                    Dim SalesmanCode As String = clsCommon.myCstr(grow.Cells("Salesman_Code").Value)
                    If SalesmanCode <> "" Then
                        Salesman_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Employee_Code  from TSPL_ROUTE_MASTER where Route_No ='" + Route_No + "'", trans))
                        If clsCommon.CompairString(Salesman_Code, SalesmanCode) = CompairStringResult.Equal Then
                            clsCommon.AddColumnsForChange(coll, "Salesman_Code", Salesman_Code)
                        Else
                            Throw New Exception("This Salesman Code does not exist with the route '" + Route_No + "' at Line No '" + LineNo + "'")
                            'trans.Rollback()
                            'Exit Sub
                        End If
                    End If

                    'Dim Salesman_Desc As String = clsDBFuncationality.getSingleValue("Select Emp_Name  from TSPL_EMPLOYEE_MASTER Where EMP_CODE='" + Salesman_Code + "'", trans)
                    'clsCommon.AddColumnsForChange(coll, "Salesman_Desc", Salesman_Desc)


                    'Dim VisiId As String = clsCommon.myCstr(grow.Cells("VIsi ID").Value)
                    'Dim Visi_Id As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Visi_Id From TSPL_VISI_MASTER Where Visi_Id='" + VisiId + "'", trans))
                    'If clsCommon.CompairString(Visi_Id, VisiId) = CompairStringResult.Equal Then
                    '    'clsDBFuncationality.ExecuteNonQuery("Update TSPL_VISI_MASTER Set Customer_Id='" + strCusCode + "', Customer_name='" + CustName + "' WHERE Visi_Id='" + Visi_Id + "'", trans)
                    '    Dim collVisi As New Hashtable()
                    '    clsCommon.AddColumnsForChange(collVisi, "Customer_Id", strCusCode)
                    '    clsCommon.AddColumnsForChange(collVisi, "Customer_name", CustName)
                    '    clsCommonFunctionality.UpdateDataTable(collVisi, "TSPL_VISI_MASTER", OMInsertOrUpdate.Update, "TSPL_VISI_MASTER.Visi_Id='" + Visi_Id + "'", trans)
                    'Else
                    '    Throw New Exception("This Visi Does not exists in Visi Master at Line No '" + LineNo + "'")
                    '    'trans.Rollback()
                    '    'Exit Sub
                    'End If

                    Dim CreditLimit As String = clsCommon.myCdbl(grow.Cells("Credit_Limit").Value)
                    clsCommon.AddColumnsForChange(coll, "Credit_Limit", CreditLimit)

                    Dim Route_Group As String
                    Dim RouteGroup As String = clsCommon.myCstr(grow.Cells("Route_Group").Value)
                    If clsCommon.myLen(RouteGroup) > 0 Then
                        Route_Group = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Group_Id  from TSPL_ROUTE_GROUP_MASTER Where Group_Id='" + RouteGroup + "'", trans))
                        If clsCommon.CompairString(Route_Group, RouteGroup) = CompairStringResult.Equal Then
                            clsCommon.AddColumnsForChange(coll, "Route_Group", Route_Group)
                        Else
                            Throw New Exception("This Route Group Does not Exist in GroupMaster at Line No '" + LineNo + "'")
                            'trans.Rollback()
                            'Exit Sub
                        End If
                    Else
                        ''Throw New Exception("Please Fill The Route Group for Customer Code '" + strCusCode + "' at Line No '" + LineNo + "'")
                        ''trans.Rollback()
                        ''Exit Sub
                    End If

                    '
                    Dim CurrencyCode As String = clsCommon.myCstr(grow.Cells("CURRENCY_CODE").Value)

                    If clsCommon.myLen(CurrencyCode) > 0 Then
                        Dim Currency_codes As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select CURRENCY_CODE from TSPL_CURRENCY_MASTER Where CURRENCY_CODE='" + CurrencyCode + "'", trans))
                        If clsCommon.CompairString(Currency_codes, CurrencyCode) = CompairStringResult.Equal Then
                            clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", Currency_codes)
                        Else
                            Throw New Exception("This Currency Code Does not Exist in CurrencyMaster at Line No '" + LineNo + "'")
                            'trans.Rollback()
                            'Exit Sub
                        End If
                    Else

                    End If

                    'Dim Cuurency_Code As String
                    'Dim CuurencyCode As String = clsCommon.myCstr(grow.Cells("CURRENCY CODE").Value)
                    'If clsCommon.myLen(CuurencyCode) > 0 Then
                    '    Cuurency_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select CURRENCY_CODE  from TSPL_CURRENCY_MASTER Where CURRENCY_CODE='" + CuurencyCode + "'", trans))
                    '    If clsCommon.CompairString(Cuurency_Code, CuurencyCode) = CompairStringResult.Equal Then


                    '        '' when customer currency is other than base currency of the company
                    '        '' match account set currency with customer selected currency
                    '        Dim qry As String
                    '        qry = "select CURRENCY_CODE from TSPL_CUSTOMER_ACCOUNT_SET where Cust_Account='" & clsCommon.myCstr(Cust_Account) & "' "
                    '        Dim accCurrCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                    '        If clsCommon.CompairString(accCurrCode, clsCommon.myCstr(Cuurency_Code)) <> CompairStringResult.Equal Then
                    '            Throw New Exception("Account Set Currency and Customer Currency must be same in case of Multicurrency  at Line No '" + LineNo + "'")
                    '        End If
                    '        '' match tax Group currency with customer currency
                    '        qry = " select TSPL_TAX_GROUP_DETAILS.Tax_Code,coalesce(TSPL_TAX_MASTER.CURRENCY_CODE,'') as CURRENCY_CODE from TSPL_TAX_GROUP_DETAILS " & _
                    '              " inner join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_DETAILS.Tax_Group_Code=TSPL_TAX_GROUP_MASTER.Tax_Group_Code " & _
                    '              " inner join TSPL_TAX_MASTER on TSPL_TAX_GROUP_DETAILS.Tax_Code=TSPL_TAX_MASTER.Tax_Code where TSPL_TAX_GROUP_MASTER.Tax_Group_Code='" & clsCommon.myCstr(Tax_Group) & "' " & _
                    '              " and coalesce(TSPL_TAX_MASTER.CURRENCY_CODE,'')<>'" & clsCommon.myCstr(Cuurency_Code) & "'"
                    '        Dim dt As DataTable
                    '        dt = clsDBFuncationality.GetDataTable(qry, trans)
                    '        Dim taxCode As String = ""
                    '        For Each dr As DataRow In dt.Rows
                    '            If dt.Rows.IndexOf(dr) = 0 Then
                    '                taxCode = dr.Item("Tax_Code")
                    '            Else
                    '                taxCode = taxCode & "," & dr.Item("Tax_Code")
                    '            End If
                    '        Next
                    '        If clsCommon.myLen(taxCode) > 0 Then
                    '            Throw New Exception("Tax Code '" & taxCode & "' in Tax Group " & clsCommon.myCstr(Tax_Group) & " are created for currency other than " & clsCommon.myCstr(Cuurency_Code) & " at Line No '" + LineNo + "'")
                    '        End If
                    '        'End If

                    '        clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", Cuurency_Code)
                    '    Else
                    '        Throw New Exception("This Currency Code Does not Exist in Currency Master at Line No '" + LineNo + "'")

                    '    End If
                    'End If
                    ''richa agarwal 28 Sep, 2016
                    'Dim stroldname As String = clsCommon.myCstr(grow.Cells("Old Name").Value)
                    'If clsCommon.myLen(stroldname) > 50 Then
                    '    Throw New Exception("Old Name should be max 50 character")
                    'End If
                    ''---------------

                    clsCommon.AddColumnsForChange(coll, "CST", clsCommon.myCstr(grow.Cells("CST").Value))
                    clsCommon.AddColumnsForChange(coll, "ECC", clsCommon.myCstr(grow.Cells("ECC").Value))
                    clsCommon.AddColumnsForChange(coll, "Range", clsCommon.myCstr(grow.Cells("Range").Value))
                    clsCommon.AddColumnsForChange(coll, "Collectorate", clsCommon.myCstr(grow.Cells("Collectorate").Value))
                    clsCommon.AddColumnsForChange(coll, "PAN", clsCommon.myCstr(grow.Cells("Pan").Value))
                    clsCommon.AddColumnsForChange(coll, "Division", clsCommon.myCstr(grow.Cells("Division").Value))

                    clsCommon.AddColumnsForChange(coll, "Add1", clsCommon.myCstr(grow.Cells(2).Value))
                    clsCommon.AddColumnsForChange(coll, "Add2", clsCommon.myCstr(grow.Cells(3).Value))
                    clsCommon.AddColumnsForChange(coll, "Add3", clsCommon.myCstr(grow.Cells(4).Value))
                    'clsCommon.AddColumnsForChange(coll, "OldName", clsCommon.myCstr(stroldname))
                    'Anand
                    clsCommon.AddColumnsForChange(coll, "Alies_Name", clsCommon.myCstr(grow.Cells("Alies_Name").Value))
                    clsCommon.AddColumnsForChange(coll, "Zone_Code", clsCommon.myCstr(grow.Cells("Zone_Code").Value))
                    '--------------

                    Dim strCustType As String = String.Empty
                    ''add ToUpper in custType 02/12/2014
                    Dim CustType As String = clsCommon.myCstr(grow.Cells("Cust_Type_Code").Value).ToUpper()
                    If clsCommon.myLen(CustType) <= 0 Then
                        '  Throw New Exception("Please fill The Customer Type for Customer Code '" + strCusCode + "' at Line No '" + LineNo + "'")
                        'trans.Rollback()
                        'Exit Sub
                        clsCommon.AddColumnsForChange(coll, "Cust_Type_Code", CustType, True)
                    Else
                        Dim Dt As DataTable = clsDBFuncationality.GetDataTable("SELECT  Cust_Type_Code FROM [TSPL_CUSTOMER_TYPE_MASTER]", trans)
                        Dim arrCustType As New List(Of String)
                        For Each dr As DataRow In Dt.Rows
                            arrCustType.Add(clsCommon.myCstr(dr("Cust_Type_Code")))
                        Next
                        If Not arrCustType.Contains(CustType) Then
                            Throw New Exception("Please Insert Customer Type among (" + clsCommon.GetMulcallString(arrCustType) + ") at Line No '" + LineNo + "'")
                        Else
                            strCustType = clsDBFuncationality.getSingleValue("SELECT  Cust_Type_Code FROM [TSPL_CUSTOMER_TYPE_MASTER] Where Cust_Type_Code='" + CustType + "'", trans)
                            clsCommon.AddColumnsForChange(coll, "Cust_Type_Code", strCustType)
                        End If
                    End If

                    Dim customerType As String

                    customerType = clsCommon.myCstr(grow.Cells("CUSTOMER_TYPE").Value)
                    If String.IsNullOrEmpty(customerType) Or clsCommon.CompairString(customerType, "R") = CompairStringResult.Equal Then
                        customerType = "R"
                    ElseIf clsCommon.CompairString(customerType, "D") = CompairStringResult.Equal Then
                        customerType = "D"
                    Else
                        Throw New Exception("Please Enter Only R Or D as Customer Type at Line No '" + LineNo + "'")
                        trans.Rollback()

                    End If
                    clsCommon.AddColumnsForChange(coll, "CUSTOMER_TYPE", customerType)


                    'Dim Route_Desc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Route_Desc from TSPL_ROUTE_MASTER Where Route_No='" + Route_No + "'", trans))
                    'clsCommon.AddColumnsForChange(coll, "Route_Desc", Route_Desc)

                    Dim ctycode As String = clsCommon.myCstr(grow.Cells("City_Code").Value)
                    Dim city_Code As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select City_Code  from TSPL_CITY_MASTER Where City_Code='" + ctycode + "'", trans))
                    If clsCommon.CompairString(city_Code, ctycode) = CompairStringResult.Equal Then
                        clsCommon.AddColumnsForChange(coll, "City_Code", city_Code)
                    Else
                        Throw New Exception("This City Code does not exist at Line No '" + LineNo + "'")
                        'trans.Rollback()
                        'Exit Sub
                    End If

                    clsCommon.AddColumnsForChange(coll, "State", clsCommon.myCstr(grow.Cells("State").Value))
                    clsCommon.AddColumnsForChange(coll, "Country", clsCommon.myCstr(grow.Cells("Country").Value))
                    clsCommon.AddColumnsForChange(coll, "Phone1", clsCommon.myCstr(grow.Cells("Phone1").Value))
                    clsCommon.AddColumnsForChange(coll, "Phone2", clsCommon.myCstr(grow.Cells("Phone2").Value))

                    clsCommon.AddColumnsForChange(coll, "Fax", clsCommon.myCstr(grow.Cells("Fax").Value))
                    clsCommon.AddColumnsForChange(coll, "Email", clsCommon.myCstr(grow.Cells("Email").Value))
                    clsCommon.AddColumnsForChange(coll, "Website", clsCommon.myCstr(grow.Cells("WebSite").Value))

                    clsCommon.AddColumnsForChange(coll, "PIN_NO", clsCommon.myCstr(grow.Cells("PIN_NO").Value))

                    clsCommon.AddColumnsForChange(coll, "Contact_Person_Name", clsCommon.myCstr(grow.Cells("Contact_Person_Name").Value))
                    clsCommon.AddColumnsForChange(coll, "Contact_Person_Phone", clsCommon.myCstr(grow.Cells("Contact_Person_Phone").Value))
                    clsCommon.AddColumnsForChange(coll, "Contact_Person_fax", clsCommon.myCstr(grow.Cells("Contact_Person_fax").Value))
                    clsCommon.AddColumnsForChange(coll, "Contact_Person_Website", clsCommon.myCstr(grow.Cells("Contact_Person_Website").Value))
                    clsCommon.AddColumnsForChange(coll, "Contact_Person_Email", clsCommon.myCstr(grow.Cells("Contact_Person_Email").Value))
                    clsCommon.AddColumnsForChange(coll, "TAX1", clsCommon.myCstr(grow.Cells("Tax1").Value))
                    clsCommon.AddColumnsForChange(coll, "TAX1_Rate", clsCommon.myCdbl(grow.Cells("TAX1_Rate").Value))
                    clsCommon.AddColumnsForChange(coll, "TAX2", clsCommon.myCstr(grow.Cells("Tax2").Value))
                    clsCommon.AddColumnsForChange(coll, "TAX2_Rate", clsCommon.myCdbl(grow.Cells("Tax2_Rate").Value))
                    clsCommon.AddColumnsForChange(coll, "TAX3", clsCommon.myCstr(grow.Cells("Tax3").Value))
                    clsCommon.AddColumnsForChange(coll, "TAX3_Rate", clsCommon.myCdbl(grow.Cells("TAX3_Rate").Value))
                    clsCommon.AddColumnsForChange(coll, "TAX4", clsCommon.myCstr(grow.Cells("Tax4").Value))
                    clsCommon.AddColumnsForChange(coll, "TAX4_Rate", clsCommon.myCdbl(grow.Cells("TAX4_Rate").Value))
                    clsCommon.AddColumnsForChange(coll, "TAX5", clsCommon.myCstr(grow.Cells("Tax5").Value))
                    clsCommon.AddColumnsForChange(coll, "TAX5_Rate", clsCommon.myCdbl(grow.Cells("TAX5_Rate").Value))
                    clsCommon.AddColumnsForChange(coll, "TAX6", clsCommon.myCstr(grow.Cells("Tax6").Value))
                    clsCommon.AddColumnsForChange(coll, "TAX6_Rate", clsCommon.myCdbl(grow.Cells("TAX6_Rate").Value))
                    clsCommon.AddColumnsForChange(coll, "TAX7", clsCommon.myCstr(grow.Cells("Tax7").Value))
                    clsCommon.AddColumnsForChange(coll, "TAX7_Rate", clsCommon.myCdbl(grow.Cells("TAX7_Rate").Value))
                    clsCommon.AddColumnsForChange(coll, "TAX8", clsCommon.myCstr(grow.Cells("Tax8").Value))
                    clsCommon.AddColumnsForChange(coll, "TAX8_Rate", clsCommon.myCdbl(grow.Cells("TAX8_Rate").Value))
                    clsCommon.AddColumnsForChange(coll, "TAX9", clsCommon.myCstr(grow.Cells("Tax9").Value))
                    clsCommon.AddColumnsForChange(coll, "TAX9_Rate", clsCommon.myCdbl(grow.Cells("TAX9_Rate").Value))
                    clsCommon.AddColumnsForChange(coll, "TAX10", clsCommon.myCstr(grow.Cells("Tax10").Value))
                    clsCommon.AddColumnsForChange(coll, "TAX10_Rate", clsCommon.myCdbl(grow.Cells("TAX10_Rate").Value))
                    clsCommon.AddColumnsForChange(coll, "Additional1", clsCommon.myCstr(grow.Cells("Additional1").Value))
                    clsCommon.AddColumnsForChange(coll, "Additional2", clsCommon.myCstr(grow.Cells("Additional2").Value))
                    clsCommon.AddColumnsForChange(coll, "Additional3", clsCommon.myCstr(grow.Cells("Additional3").Value))
                    clsCommon.AddColumnsForChange(coll, "Payment_Code", clsCommon.myCstr(grow.Cells("Payment_Code").Value))
                    clsCommon.AddColumnsForChange(coll, "Service_Tax_No", clsCommon.myCstr(grow.Cells("Service_Tax_No").Value))
                    clsCommon.AddColumnsForChange(coll, "Tin_No", clsCommon.myCstr(grow.Cells("Tin_No").Value))
                    clsCommon.AddColumnsForChange(coll, "Lst_No", clsCommon.myCstr(grow.Cells("Lst_No").Value))
                    clsCommon.AddColumnsForChange(coll, "Form_Type", clsCommon.myCstr(grow.Cells("Form_Type").Value))
                    clsCommon.AddColumnsForChange(coll, "Channel_Code", clsCommon.myCstr(grow.Cells("Channel_Code").Value))
                    clsCommon.AddColumnsForChange(coll, "Channel_Desc", clsCommon.myCstr(grow.Cells("Channel_Desc").Value))
                    'clsCommon.AddColumnsForChange(coll, "Status", clsCommon.myCstr(grow.Cells("Status").Value))
                    clsCommon.AddColumnsForChange(coll, "Remarks1", clsCommon.myCstr(grow.Cells("Remarks1").Value))
                    clsCommon.AddColumnsForChange(coll, "Remarks2", clsCommon.myCstr(grow.Cells("Remarks2").Value))
                    clsCommon.AddColumnsForChange(coll, "Customer_Class", clsCommon.myCstr(grow.Cells("Customer_Class").Value))
                    'clsCommon.AddColumnsForChange(coll, "Customer_Class", strCustType)


                    'clsCommon.AddColumnsForChange(coll, "Customer_Class", clsCommon.myCstr(grow.Cells("Customer_Class").Value))

                    clsCommon.AddColumnsForChange(coll, "TempCreditLimit", clsCommon.myCstr(grow.Cells("TempCreditLimit").Value))

                    If clsCommon.myCstr(grow.Cells("TempCreditLimitFrom").Value) <> "" AndAlso IsDBNull(clsCommon.myCstr(grow.Cells("TempCreditLimitFrom").Value)) <> True AndAlso IsNothing(clsCommon.myCstr(grow.Cells("TempCreditLimitFrom").Value)) <> True Then
                        clsCommon.AddColumnsForChange(coll, "TempCreditLimitFrom", clsCommon.GetPrintDate(clsCommon.myCstr(grow.Cells("TempCreditLimitFrom").Value), "dd/MMM/yyyy"))
                    End If
                    If clsCommon.myCstr(grow.Cells("TempCreditLimitTo").Value) <> "" AndAlso IsDBNull(clsCommon.myCstr(grow.Cells("TempCreditLimitTo").Value)) <> True AndAlso IsNothing(clsCommon.myCstr(grow.Cells("TempCreditLimitTo").Value)) <> True Then
                        clsCommon.AddColumnsForChange(coll, "TempCreditLimitTo", clsCommon.GetPrintDate(clsCommon.myCstr(grow.Cells("TempCreditLimitTo").Value), "dd/MMM/yyyy"))
                    End If

                    If clsCommon.myCstr(grow.Cells("Agg_Made_Date").Value) <> "" AndAlso IsDBNull(clsCommon.myCstr(grow.Cells("Agg_Made_Date").Value)) <> True AndAlso IsNothing(clsCommon.myCstr(grow.Cells("Agg_Made_Date").Value)) <> True Then
                        clsCommon.AddColumnsForChange(coll, "Agg_Made_Date", clsCommon.GetPrintDate(clsCommon.myCstr(grow.Cells("Agg_Made_Date").Value), "dd/MMM/yyyy"))
                    End If
                    If clsCommon.myCstr(grow.Cells("Agg_Close_Date").Value) <> "" AndAlso IsDBNull(clsCommon.myCstr(grow.Cells("Agg_Close_Date").Value)) <> True AndAlso IsNothing(clsCommon.myCstr(grow.Cells("Agg_Close_Date").Value)) <> True Then
                        clsCommon.AddColumnsForChange(coll, "Agg_Close_Date", clsCommon.GetPrintDate(clsCommon.myCstr(grow.Cells("Agg_Close_Date").Value), "dd/MMM/yyyy"))
                    End If





                    'Dim interbranch As String

                    'interbranch = clsCommon.myCstr(grow.Cells("Inter Branch").Value)
                    'If String.IsNullOrEmpty(interbranch) Or clsCommon.CompairString(interbranch, "N") = CompairStringResult.Equal Then
                    '    interbranch = "N"
                    'ElseIf clsCommon.CompairString(interbranch, "Y") = CompairStringResult.Equal Then
                    '    interbranch = "Y"
                    'Else
                    '    Throw New Exception("Please Enter Only Y Or N as Inter Branch at Line No '" + LineNo + "'")
                    '    'trans.Rollback()
                    '    'Exit Sub
                    'End If
                    'clsCommon.AddColumnsForChange(coll, "Inter_Branch", interbranch)

                    Dim parentcustomer As String
                    parentcustomer = clsCommon.myCstr(grow.Cells("Parent_Customer_YN").Value)
                    If String.IsNullOrEmpty(parentcustomer) Or clsCommon.CompairString(parentcustomer, "N") = CompairStringResult.Equal Then
                        parentcustomer = "N"
                    ElseIf clsCommon.CompairString(parentcustomer, "Y") = CompairStringResult.Equal Then
                        parentcustomer = "Y"
                    Else
                        Throw New Exception("Please Enter Only Y or N as Parent Customer at Line No '" + LineNo + "'")
                        'trans.Rollback()
                        'Exit Sub
                    End If
                    clsCommon.AddColumnsForChange(coll, "Parent_Customer_YN", parentcustomer)
                    ''richa agarwal
                    If clsCommon.CompairString(parentcustomer, "Y") = CompairStringResult.Equal Then
                        strParentCstmrNo = ""
                    End If



                    clsCommon.AddColumnsForChange(coll, "Parent_Customer_No", strParentCstmrNo)










                    ''===============
                    'Dim transaction_type As String = clsCommon.myCstr(grow.Cells("Transaction Type").Value)
                    'By Anand
                    'If Not (clsCommon.CompairString(clsCommon.myCstr(transaction_type), "R") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(transaction_type), "T") = CompairStringResult.Equal) Then
                    '    Throw New Exception("Please Check The Transaction Type of Customer Code='" + strCusCode + "' ")
                    '    'trans.Rollback()
                    '    'Exit Sub
                    'Else
                    'clsCommon.AddColumnsForChange(coll, "Transaction_Type", transaction_type)
                    'End If
                    ''richa agarwal
                    ' Remove these two fields 09-June-2015 (Amit Sir)
                    'Dim dblCrateOpeningQuantity As Double = 0
                    'Dim strCrateOpeningdate As Date? = Nothing
                    'dblCrateOpeningQuantity = clsCommon.myCdbl(grow.Cells("Crate Opening Qty").Value)

                    ''If clsCommon.myCdbl(dblCrateOpeningQuantity) < 0 Then
                    ''    Throw New Exception("Crate Opening Qty should not be zero At Line No.'" + LineNo + "'")
                    ''End If
                    'If dblCrateOpeningQuantity <> 0 Then
                    '    If (String.IsNullOrEmpty(clsCommon.myCDate(grow.Cells("Crate Opening Date").Value))) Then
                    '        Throw New Exception("Crate Opening Date should not be left blank At Line No. '" + LineNo + "'")
                    '    End If
                    '    strCrateOpeningdate = clsCommon.myCDate(grow.Cells("Crate Opening Date").Value)
                    'End If
                    'clsCommon.AddColumnsForChange(coll, "Crate_Opening", dblCrateOpeningQuantity)
                    'If strCrateOpeningdate IsNot Nothing Then
                    '    clsCommon.AddColumnsForChange(coll, "Crate_Opening_Date", clsCommon.GetPrintDate(strCrateOpeningdate, "dd/MMM/yyyy"))
                    'Else
                    'clsCommon.AddColumnsForChange(coll, "Crate_Opening_Date", Nothing, True)
                    'End If

                    ''---------------
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "price_group_code", clsCommon.myCstr(grow.Cells("price_group_code").Value))

                    'Dim strServiceDealerCode As String = clsCommon.myCstr(grow.Cells("Service Dealer Code").Value)
                    'If String.IsNullOrEmpty(strServiceDealerCode) Then

                    'Else
                    '    Dim i1 As Integer = CInt(clsDBFuncationality.getSingleValue("select count(*) from TSPL_Employee_Master where Emp_Code='" + strServiceDealerCode + "'", trans))
                    '    If i1 <> 0 Then
                    '        clsCommon.AddColumnsForChange(coll, "Service_Dealer_Code", strServiceDealerCode)
                    '    Else
                    '        Throw New Exception("This Service Dealer  Code does not Exist in Master ")
                    '        'trans.Rollback()
                    '        'Exit Sub
                    '    End If
                    'End If

                    'Dim strTDMCode As String = clsCommon.myCstr(grow.Cells("TDM Code").Value)
                    'If String.IsNullOrEmpty(strTDMCode) Then

                    'Else
                    '    Dim i2 As Integer = CInt(clsDBFuncationality.getSingleValue("select count(*) from TSPL_Employee_Master where Emp_Code='" + strTDMCode + "'", trans))
                    '    If i2 <> 0 Then
                    '        clsCommon.AddColumnsForChange(coll, "TDM_Code", strTDMCode)
                    '    Else
                    '        Throw New Exception("This TDM  Code does not Exist in Master ")
                    '        'trans.Rollback()
                    '        'Exit Sub
                    '    End If
                    'End If

                    'Dim strDistributorCode As String = clsCommon.myCstr(grow.Cells("Distributor Code").Value)
                    'If String.IsNullOrEmpty(strDistributorCode) Then

                    'Else
                    '    Dim i3 As Integer = CInt(clsDBFuncationality.getSingleValue("select count(*) from TSPL_CUSTOMER_Master where Cust_Code='" + strDistributorCode + "' and IsDistributor='Y'", trans))
                    '    If i3 <> 0 Then
                    '        clsCommon.AddColumnsForChange(coll, "Distributor_Code", strDistributorCode)
                    '    Else
                    '        Throw New Exception("This Distributor  Code does not Exist in Master ")
                    '        'trans.Rollback()
                    '        'Exit Sub
                    '    End If
                    'End If
                    Dim i As Integer = CInt(clsDBFuncationality.getSingleValue("select count(*) from TSPL_SECONDARY_CUSTOMER_MASTER where Cust_Code='" + strCusCode + "'", trans))

                    Dim strIsDistributor = clsCommon.myCstr(grow.Cells("IsDistributor").Value)
                    If String.IsNullOrEmpty(strIsDistributor) Or clsCommon.CompairString(strIsDistributor, "N") = CompairStringResult.Equal Then
                        strIsDistributor = "N"
                    ElseIf clsCommon.CompairString(strIsDistributor, "Y") = CompairStringResult.Equal Then
                        strIsDistributor = "Y"
                    Else
                        Throw New Exception("Please Enter Only Y or N as Is Distributor Field")
                        'trans.Rollback()
                        'Exit Sub
                    End If
                    clsCommon.AddColumnsForChange(coll, "IsDistributor", strIsDistributor)



                    '' Anubhooti 01-Sep-2014 BM00000003425  ******************* Check Outstanding Amount Of customer *************
                    '        Dim QryToGetOutAmt As String = ""
                    '        Dim OutStandAmt As Double = 0

                    '        QryToGetOutAmt = " Select [Customer Id], MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as Customer_Name, SUM([Due Amount]) AS [Due Amount] from (  " & _
                    '" SELECT  TSPL_CUSTOMER_MASTER.Cust_Code AS [Customer Id], TSPL_SD_SALE_INVOICE_HEAD.Document_Code as [Document Id], case when TSPL_Customer_Invoice_Head.Document_Type ='I' then TSPL_Customer_Invoice_Head.Document_Total  when TSPL_Customer_Invoice_Head.Document_Type ='D' then TSPL_Customer_Invoice_Head.Document_Total  when TSPL_Customer_Invoice_Head.Document_Type ='C' then TSPL_Customer_Invoice_Head.Document_Total*-1  end as [Due Amount], CONVERT(DATE,TSPL_Customer_Invoice_Head.Document_Date,103) as [Document Date], case when TSPL_Customer_Invoice_Head.Document_Type ='I' then 'IN'  when TSPL_Customer_Invoice_Head.Document_Type ='D' then 'DB'  when TSPL_Customer_Invoice_Head.Document_Type ='C' then 'CR' end  as [Document_Type] FROM  TSPL_Customer_Invoice_Head INNER JOIN   TSPL_CUSTOMER_MASTER ON TSPL_Customer_Invoice_Head.Customer_Code  = TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_Customer_Invoice_Head.Against_Sale_No where TSPL_Customer_Invoice_Head.Status='1'  AND TSPL_CUSTOMER_MASTER.Status='N'  " & _
                    '"          UNION ALL " & _
                    '" SELECT TSPL_SALE_RETURN_INTER_HEAD.Cust_Code AS [Customer Id] ,Document_No as [Document Id], (Total_Order_Amt)*-1 as [Due Amount] , CONVERT(DATE,Document_Date,103) as [Document Date]  , 'SR' as [Document_Type]  from TSPL_SALE_RETURN_INTER_HEAD INNER JOIN   TSPL_CUSTOMER_MASTER ON TSPL_SALE_RETURN_INTER_HEAD.Cust_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where  TSPL_SALE_RETURN_INTER_HEAD.Is_Post=1  AND TSPL_CUSTOMER_MASTER.Status='N'  " & _
                    '       " union All " & _
                    '" select TSPL_VCGL_Head.VC_Code as ACode, TSPL_VCGL_Head.Document_No as DocNo, CASE WHEN Amount_Type='Cr' THEN TSPL_VCGL_Head.Amount ELSE TSPL_VCGL_Head.Amount*-1 END, convert(date,TSPL_VCGL_Head.Document_Date,103), 'VGCL' from  TSPL_VCGL_Head  left outer JOIN   TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Head.VC_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where TSPL_VCGL_Head.Document_Type='C' and TSPL_VCGL_Head.Status=1  AND TSPL_CUSTOMER_MASTER.Status='N'  " & _
                    '       " UNION ALL " & _
                    '" select TSPL_VCGL_Detail.VCGL_Code as ACode, TSPL_VCGL_Head.Document_No as DocNo, CASE WHEN TSPL_VCGL_Detail.Cr_Amount>0 THEN TSPL_VCGL_Detail.Cr_Amount*-1 ELSE TSPL_VCGL_Detail.Dr_Amount END, convert(date,TSPL_VCGL_Head.Document_Date,103), 'VGCL' from  TSPL_VCGL_Detail left outer join TSPL_VCGL_Head on TSPL_VCGL_Head .Document_No=TSPL_VCGL_Detail.Document_No left outer JOIN   TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Detail.VCGL_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where  TSPL_VCGL_Head.Status=1 and TSPL_VCGL_Detail.Row_Type='Customer'  AND TSPL_CUSTOMER_MASTER.Status='N'  " & _
                    '        " union All " & _
                    '" select TSPL_RECEIPT_HEADER.Cust_Code, TSPL_RECEIPT_HEADER.Receipt_No , Case When TSPL_RECEIPT_HEADER.Receipt_Type='F' Then TSPL_RECEIPT_HEADER.Balance_Amt Else TSPL_RECEIPT_HEADER.Balance_Amt*-1 End,convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103), case when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'AV'  when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'OA'  when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'UC'  when TSPL_RECEIPT_HEADER.Receipt_Type='F' then 'RF' when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'RC' end from TSPL_RECEIPT_HEADER inner join TSPL_CUSTOMER_MASTER  ON TSPL_RECEIPT_HEADER.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code where TSPL_RECEIPT_HEADER.Receipt_Type NOT IN ('M') and TSPL_RECEIPT_HEADER.Posted='Y' AND TSPL_RECEIPT_HEADER.IsChkReverse='N' AND TSPL_RECEIPT_HEADER.Balance_Amt>0  AND TSPL_CUSTOMER_MASTER.Status='N'  " & _
                    '"         UNION ALL " & _
                    '" Select Customer_No as [Customer Id], Adjustment_No as [Document Id], Adjustment_Amount*-1 as [Due Amount], CONVERT(DATE,Adjustment_Date,103) as [Document Date], 'RC' as [Document_Type] from TSPL_Receipt_Adjustment_Header LEFT OUTER JOIN TSPL_CUSTOMER_MASTER on TSPL_Receipt_Adjustment_Header.Customer_No= TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_SALE_INVOICE_HEAD on TSPL_Receipt_Adjustment_Header.Doc_No= TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No WHERE TSPL_Receipt_Adjustment_Header.Is_Post ='Y'  AND TSPL_CUSTOMER_MASTER.Status='N'  " & _
                    '       " Union All  " & _
                    '" SELECT TSPL_SALE_RETURN_INTER_HEAD.Cust_Code AS [Customer Id], Document_No as [Document Id], Empty_Value*-1 AS [Due Amount], CONVERT(DATE,Document_Date,103) as [Document Date], 'SR' as [Document_Type] from TSPL_SALE_RETURN_INTER_HEAD INNER JOIN   TSPL_CUSTOMER_MASTER ON TSPL_SALE_RETURN_INTER_HEAD.Cust_Code  = TSPL_CUSTOMER_MASTER.Cust_Code where  TSPL_SALE_RETURN_INTER_HEAD.Is_Post=1  AND TSPL_CUSTOMER_MASTER.Status='N'  " & _
                    '        " UNION ALL " & _
                    '" SELECT  TSPL_ADJUSTMENT_HEADER.Customer_CODE, TSPL_ADJUSTMENT_HEADER.Adjustment_No, case when TSPL_ADJUSTMENT_HEADER.Trans_Type<>'In' then  (SELECT SUM(ISNULL(Item_Cost,0)+ISNULL(Breakage_Cost,0))*-1 FROM dbo.TSPL_ADJUSTMENT_DETAIL WHERE TSPL_ADJUSTMENT_DETAIL.Adjustment_No=TSPL_ADJUSTMENT_HEADER.Adjustment_No)*-1 else (SELECT SUM(ISNULL(Item_Cost,0)+ISNULL(Breakage_Cost,0))*-1 FROM dbo.TSPL_ADJUSTMENT_DETAIL WHERE TSPL_ADJUSTMENT_DETAIL.Adjustment_No=TSPL_ADJUSTMENT_HEADER.Adjustment_No) end, convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103), 'AD' FROM dbo.TSPL_ADJUSTMENT_HEADER LEFT OUTER JOIN TSPL_CUSTOMER_MASTER on TSPL_ADJUSTMENT_HEADER.Customer_CODE=TSPL_CUSTOMER_MASTER.Cust_Code WHERE TSPL_ADJUSTMENT_HEADER.Customer_CODE <> '' AND ISNULL(Reference_Document,'')=''  and TSPL_ADJUSTMENT_HEADER.Posted='Y'  AND TSPL_CUSTOMER_MASTER.Status='N'  " & _
                    '" ) XXX LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON XXX.[Customer Id]=TSPL_CUSTOMER_MASTER.Cust_Code where  XXX.Document_Type in ('IN','DB','CR','RC','AV','OA','UC','SR','VGCL','AD','RF','RC'  )  and convert(date,XXX.[Document Date] ,103) <= convert(date,'03/09/2014',103) AND [Due Amount] <> 0 And [Customer Id]='" & strCusCode & "' Group By XXX.[Customer Id]  ORDER BY [Customer Id] "


                    '        Dim dt2 As DataTable
                    '        dt2 = clsDBFuncationality.GetDataTable(QryToGetOutAmt, trans)
                    '        If dt2.Rows IsNot Nothing AndAlso dt2.Rows.Count > 0 Then
                    '            OutStandAmt = clsCommon.myCdbl(dt2.Rows(0)("Due Amount").ToString())
                    '        End If
                    '        If OutStandAmt > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Status").Value).ToUpper().Trim(), "Y") = CompairStringResult.Equal Then
                    '            Throw New Exception("You can not make this customer Inactive because it has outstanding amount")
                    '            'trans.Rollback()
                    '            'Exit Sub
                    '        End If
                    '' ******************* Check Outstanding Amount Of customer *************

                    '' Anubhooti 02-Sep-2014 Duplication of vendor desp
                    'Dim dt1 As DataTable
                    'dt1 = clsDBFuncationality.GetDataTable("Select * From TSPL_CUSTOMER_MASTER Where (((ISNULL( ECC,'')='" & clsCommon.myCstr(grow.Cells("Ecc").Value).Trim() & "' and LEN(ISNULL( ECC,'')) > 0))  or ((ISNULL(Email,'')='" & clsCommon.myCstr(grow.Cells("Email Id").Value).Trim() & "' ANd ISNULL(Email,'')<>'' )) or ((ISNULL(Tin_No,'')='" & clsCommon.myCstr(grow.Cells("Tin No").Value).Trim() & "' AND ISNULL(Tin_No,'')<>'' )) or ((ISNULL(Contact_Person_Email,'')='" & clsCommon.myCstr(grow.Cells("Contact Person Email").Value).Trim() & "' ANd ISNULL(Contact_Person_Email,'')<>'' )) ) and (TSPL_CUSTOMER_MASTER.Cust_Code not in ('" & strCusCode.Trim() & "'))", trans)
                    'If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    '    clsCommon.ProgressBarHide()
                    '    If common.clsCommon.MyMessageBoxShow("Customer exists with same customer description.Do you still want to continue ", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                    '        clsCommon.ProgressBarShow()
                    '    Else
                    '        clsCommon.ProgressBarShow()
                    '        Continue For
                    '    End If
                    'End If
                    ''
                    If (i = 0) Then

                        '' Anubhooti 02-Sep-2014
                        'AllowAutoCCode = clsCommon.myCstr(clsSecondaryCustomerMasterInfo.GetData(clsFixedParameterType.AutoGeneratedCustomerCode, clsFixedParameterCode.AutoGeneratedCustomerCode, trans))
                        'If clsCommon.CompairString(AllowAutoCCode, "1") = CompairStringResult.Equal AndAlso clsCommon.myLen(Custname) > 0 Then
                        '    If clsCommon.myLen(strCusCode.Trim()) <= 0 Then
                        '        strCusCode = clsERPFuncationality.GetVendorNextCode("TSPL_SECONDARY_CUSTOMER_MASTER", "Customer_Name", Custname, trans)
                        '    Else
                        '        strCusCode = strCusCode
                        '    End If
                        'Else
                        '    strCusCode = strCusCode
                        'End If

                        'clsCommon.AddColumnsForChange(coll, "Cust_Code", strCusCode)
                        clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SECONDARY_CUSTOMER_MASTER", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SECONDARY_CUSTOMER_MASTER", OMInsertOrUpdate.Update, "TSPL_SECONDARY_CUSTOMER_MASTER.Cust_Code='" + strCusCode + "'", trans)
                    End If
                    'End If
                    ''added by richa agarwal
                    If clsCommon.CompairString(parentcustomer, "Y") = CompairStringResult.Equal Then
                        clsDBFuncationality.ExecuteNonQuery("Update TSPL_SECONDARY_CUSTOMER_MASTER set Credit_Limit=" & CreditLimit & " where Parent_Customer_No ='" & strCusCode & "'", trans)
                    End If
                    If clsCommon.myLen(strParentCstmrNo) > 0 Then
                        clsDBFuncationality.ExecuteNonQuery("Update TSPL_SECONDARY_CUSTOMER_MASTER set Credit_Limit=(Select Credit_Limit from TSPL_SECONDARY_CUSTOMER_MASTER where Cust_Code='" & strParentCstmrNo & "') where Cust_Code ='" & strCusCode & "'", trans)
                    End If
                    ''=======================
                Next

                trans.Commit()
                clsCommon.ProgressBarPercentHide()

                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)

            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarPercentHide()

                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub CmbCustomerType_SelectedValueChanged(sender As Object, e As EventArgs) Handles CmbCustomerType.SelectedValueChanged
        CmbCustomerType.SelectedValue = Nothing

    End Sub
End Class
