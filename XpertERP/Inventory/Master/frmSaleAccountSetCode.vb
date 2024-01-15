'' updation by richa agarwal (add gain/loss account field)
'' updation by Pankaj Jha on 10-04-2015 (add Cost Of Goods Transfer and Stock Transfer A/c  field)

Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Configuration
Imports Telerik.Collections.Generic
Imports Excel = Microsoft.Office.Interop.Excel
Imports common
Public Class frmSaleAccountSetCode
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()

    'Start date =18/5/2011
    'End date =20/5/2011
    'Last modify date = 30/5/2011
    'Database =TSPLERP
    ' Tables=Tspl_Sales_accounts
    Dim userCode, companyCode As String
    Dim dt As DataTable
    'This cunstructer is used to send usercode and compcode  in table.
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.itemSaleAccount)
        If Not (MyBase.isReadFlag) Then
            '--------richa Ticket no. BM00000003121 15/07/2014 
            Throw New Exception("Permission Denied")
        End If
        rdbtnSave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 02/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If rdbtnSave.Visible = True Then
            rdmenuimport.Enabled = True
            RadMenuexport.Enabled = True
        Else
            rdmenuimport.Enabled = False
            RadMenuexport.Enabled = False
        End If
        '--------------------------------------------------
        'btnPost.Visible = MyBase.isPostFlag
        rdbtndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub frmSaleAccountSetCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso rdbtnSave.Enabled Then
            SaveData()
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            '    PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso rdbtndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub
    Private Sub SaleAccountSetCode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        ButtonToolTip.SetToolTip(rdbtnClose, "Press Alt+C Close the Window")
        'ButtonToolTip.SetToolTip(rdbtnreset, "Press Alt+R Reset the Window")
        ButtonToolTip.SetToolTip(rdbtnSave, "Press Alt+R For Save")
        ButtonToolTip.SetToolTip(rdbtndelete, "Press Alt+D For Delete")
        fndaccountsetcode.MyMaxLength = 6
        rdtxtdesc.MaxLength = 50
        If fndaccountsetcode.Value = "" Then
            rdtxtdesc.Enabled = True
            'fndsales.txtValue.Enabled = True
            rdtxtsales.Enabled = True
            'fndreturns.txtValue.Enabled = True
            rdtxtreturns.Enabled = True
            'fndcostofgoods.txtValue.Enabled = True
            rdtxtcostofgoodssold.Enabled = True
            'fndcostofvariance.txtValue.Enabled = True
            rdtxtcostofvariance.Enabled = True
            'fnddamagedgoods.txtValue.Enabled = True
            rdtxtdamegedgoods.Enabled = True
            'fndinternalusage.txtValue.Enabled = True
            rdtxtinternalusage.Enabled = True
            FndCogsInterBranch1.Enabled = True
            txtcogsInterbranch.Enabled = True
            rdbtnSave.Enabled = True
            rdbtndelete.Enabled = False
        End If
        AddHandler fndaccountsetcode.TextChanged, AddressOf text_changed
        AddHandler fndaccountsetcode.Leave, AddressOf fndaccountsetcode_Leave
        'AddHandler fndsales.txtValue.TextChanged, AddressOf sales_textchanged
        'AddHandler fndreturns.txtValue.TextChanged, AddressOf returns_textchanged
        'AddHandler fndcostofgoods.txtValue.TextChanged, AddressOf costofgood_textchanged
        'AddHandler fndcostofvariance.txtValue.TextChanged, AddressOf costofvariance_textchanged
        'AddHandler fnddamagedgoods.txtValue.TextChanged, AddressOf damagedgoods_textchanged
        'AddHandler fndinternalusage.txtValue.TextChanged, AddressOf internalusages_textchanged
        'AddHandler fndsales.txtValue.Leave, AddressOf sales_leave
        'AddHandler fndcostofgoods.txtValue.Leave, AddressOf cosiofgoodsold_leave
        'AddHandler fndcostofvariance.txtValue.Leave, AddressOf costofgoodvariance_leave
        'AddHandler fnddamagedgoods.txtValue.Leave, AddressOf damagedgoods_leave
        'AddHandler fndinternalusage.txtValue.Leave, AddressOf internalusages_leave
        'AddHandler fndreturns.txtValue.Leave, AddressOf returns_leave
        'AddHandler fndSchemes.txtValue.TextChanged, AddressOf fndSchemes_TextChanged
        'AddHandler fndSchemes.txtValue.Leave, AddressOf fndSchemes_Leave
        'AddHandler fndPromotional.txtValue.TextChanged, AddressOf fndPromotional_TextChanged
        'AddHandler fndPromotional.txtValue.TextChanged, AddressOf fndPromotional_Leave
        'AddHandler fndreturn.txtValue.TextChanged, AddressOf fndreturn_textchange
        'AddHandler fndreturn.txtValue.Leave, AddressOf fndreturn_leave
        'AddHandler fndreturn.txtValue.KeyPress, AddressOf fndreturn_press
        'AddHandler fndCogsInterBranch.txtValue.TextChanged, AddressOf fndCogsInterBranch_txtchanged
        'AddHandler fndCogsInterBranch.txtValue.Leave, AddressOf fndCogsinterbranch_leave
        'AddHandler fndCogsInterBranch.KeyPress, AddressOf fndcogsINterbranch_press


        fndaccountsetcode.MyMaxLength = 6
        fndaccountsetcode.MyCharacterCasing = CharacterCasing.Upper
        fndaccountsetcode.TabIndex = 0
        rdtxtdesc.TabIndex = 1
        'fndsales.txtValue.TabIndex = 2
        'fndreturns.txtValue.TabIndex = 3
        'fndcostofgoods.txtValue.TabIndex = 4
        'fndcostofvariance.txtValue.TabIndex = 5
        'fnddamagedgoods.txtValue.TabIndex = 6
        'fndinternalusage.txtValue.TabIndex = 7
        'globalFunc.mandatoryText(fndcostofgoods.txtValue, fndcostofvariance.txtValue, fnddamagedgoods.txtValue, fndinternalusage.txtValue, fndreturn.txtValue, fndreturns.txtValue, fndsales.txtValue, fndSchemes.txtValue, fndPromotional.txtValue)



    End Sub



    'Public Sub fndreturn_press(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    fndreturn.txtValue.CharacterCasing = CharacterCasing.Upper
    '    If (e.KeyChar = Chr(39)) Then
    '        e.Handled = True
    '    End If
    'End Sub
    'Public Sub fndreturn_textchange(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    finderTextChanged(fndreturn, txtreturn)
    'End Sub

    'Public Sub fndreturn_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    finderLeave(fndreturn, txtreturn)

    'End Sub

    'Public Sub fndcogsINterbranch_press(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    fndCogsInterBranch.txtValue.CharacterCasing = CharacterCasing.Upper
    '    If (e.KeyChar = Chr(39)) Then
    '        e.Handled = True
    '    End If
    'End Sub
    'Public Sub fndCogsInterBranch_txtchanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    finderTextChanged(fndCogsInterBranch, txtcogsInterbranch)
    'End Sub
    'Public Sub fndCogsinterbranch_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    finderLeave(fndCogsInterBranch, txtcogsInterbranch)
    'End Sub






    'This function is used to Insert data.  
    Public Sub funinsert()
        Try
            If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False)) Then
                Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_SALES_ACCOUNTS where Sales_Class_Code='" & fndaccountsetcode.Value.ToString() & "'")
                If ChkNewEntry = 0 Then
                    fndaccountsetcode.Value = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"), clsDocType.AreaMaster, "", "")
                    If clsCommon.myLen(fndaccountsetcode.Value) <= 0 Then
                        Throw New Exception("Error in Code Generation")
                    End If
                End If
            End If
            connectSql.RunSp("Saleaccount_Insert", New SqlParameter("@acc", fndaccountsetcode.Value.ToString()), New SqlParameter("@desc", rdtxtdesc.Text.ToString()), New SqlParameter("@saleacc", fndSales.Value.ToString()), New SqlParameter("@returnacc", fndReturns.Value), New SqlParameter("@costofgoodsold", fndcostofgoods.Value.ToString()), New SqlParameter("@costvariance ", fndcostofvariance.Value.ToString()), New SqlParameter("@damagedgoods", fndDamagedGoods.Value.ToString()), New SqlParameter("@internal ", fndinternalusage.Value.ToString()), New SqlParameter("@Returnable_Container ", fndReturnableCont.Value.ToString()), New SqlParameter("@Schemes", fndSchemes.Value), New SqlParameter("@Promotional", fndPromotional.Value), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode), New SqlParameter("@cogs_interbranch", FndCogsInterBranch1.Value))
            clsDBFuncationality.ExecuteNonQuery("Update TSPL_SALES_ACCOUNTS set Suspence_Account='" + txtSalesSuspenceAC.Value + "',Gain_Loss_Account ='" + FndGainLossaccount.Value + "',Stock_Transfer_AC ='" + fndStockTransferAc.Value + "',COGT_AC ='" + fndCostOfGoodsTranfer.Value + "',DisplayPurpose_Account='" + clsCommon.myCstr(fnddisplaypurposeacct.Value) + "',Cost_Of_Goods_Scheme = (case when " & clsCommon.myLen(txtCostOfGoodsScheme.Value) & ">0 then '" + clsCommon.myCstr(txtCostOfGoodsScheme.Value) + "' else null end) where Sales_Class_Code='" + fndaccountsetcode.Value + "'")
            myMessages.insert()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    'This function is used to Update data.
    Public Sub funupadte()
        Try
            connectSql.RunSp("Saleaccount_update", New SqlParameter("@acc", fndaccountsetcode.Value.ToString()), New SqlParameter("@desc", rdtxtdesc.Text.ToString()), New SqlParameter("@saleacc", fndSales.Value.ToString()), New SqlParameter("@returnacc", fndReturns.Value), New SqlParameter("@costofgoodsold", fndcostofgoods.Value.ToString()), New SqlParameter("@costvariance ", fndcostofvariance.Value.ToString()), New SqlParameter("@damagedgoods", fndDamagedGoods.Value.ToString()), New SqlParameter("@internal ", fndinternalusage.Value.ToString()), New SqlParameter("@Returnable_Container ", fndReturnableCont.Value.ToString()), New SqlParameter("@Schemes", fndSchemes.Value), New SqlParameter("@Promotional", fndPromotional.Value), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode), New SqlParameter("@Cogs_interbranch ", FndCogsInterBranch1.Value))
            clsDBFuncationality.ExecuteNonQuery("Update TSPL_SALES_ACCOUNTS set Suspence_Account='" + txtSalesSuspenceAC.Value + "',Gain_Loss_Account ='" + FndGainLossaccount.Value + "',Stock_Transfer_AC ='" + fndStockTransferAc.Value + "',COGT_AC ='" + fndCostOfGoodsTranfer.Value + "',DisplayPurpose_Account='" + clsCommon.myCstr(fnddisplaypurposeacct.Value) + "',Cost_Of_Goods_Scheme = (case when " & clsCommon.myLen(txtCostOfGoodsScheme.Value) & ">0 then '" + clsCommon.myCstr(txtCostOfGoodsScheme.Value) + "' else null end)  where Sales_Class_Code='" + fndaccountsetcode.Value + "'")

            myMessages.update()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    'This function is used to delete data.
    Public Sub fundelete()
        Try
            connectSql.RunSp("saleaccount_delete", New SqlParameter("@acc", fndaccountsetcode.Value.ToString()))


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    'This function is used to reset all controls.
    Public Sub funreset()
        fndaccountsetcode.Enabled = True
        fndaccountsetcode.MyReadOnly = False
        fndaccountsetcode.Value = ""
        rdtxtdesc.Enabled = True
        rdtxtdesc.Text = ""
      
        rdtxtsales.ReadOnly = True
        rdtxtsales.Text = ""
        
        rdtxtreturns.ReadOnly = True
        rdtxtreturns.Text = ""
        
        rdtxtcostofgoodssold.ReadOnly = True
        rdtxtcostofgoodssold.Text = ""
        
        rdtxtcostofvariance.ReadOnly = True
        rdtxtcostofvariance.Text = ""
      
        rdtxtdamegedgoods.ReadOnly = True
        rdtxtdamegedgoods.Text = ""
       
        rdtxtinternalusage.ReadOnly = True
        rdtxtinternalusage.Text = ""
        txtreturn.Text = ""
        rdbtnSave.Enabled = True
        rdbtnSave.Text = "Save"
        fndaccountsetcode.MyReadOnly = False
        rdbtndelete.Enabled = False
        txtSchemes.Text = ""
        txtPromotional.Text = ""
        FndCogsInterBranch1.Value = ""
        FndCogsInterBranch1.Enabled = True
        txtcogsInterbranch.Text = ""





        fndaccountsetcode.Value = ""
        rdtxtdesc.Text = ""
        fndSales.Value = ""
        rdtxtsales.Text = ""
        fndReturns.Value = ""
        rdtxtreturns.Text = ""
        fndcostofgoods.Value = ""
        rdtxtcostofgoodssold.Text = ""
        fndcostofvariance.Value = ""
        rdtxtcostofvariance.Text = ""
        fndDamagedGoods.Value = ""
        rdtxtdamegedgoods.Text = ""
        fndinternalusage.Value = ""
        rdtxtinternalusage.Text = ""
        fndReturnableCont.Value = ""
        txtreturn.Text = ""
        fndSchemes.Value = ""
        txtSchemes.Text = ""
        fndPromotional.Value = ""
        txtPromotional.Text = ""
        FndCogsInterBranch1.Value = ""
        txtcogsInterbranch.Text = ""
        txtSalesSuspenceAC.Value = ""
        lblSaleSuspenceAC.Text = ""

        FndGainLossaccount.Value = ""
        lblgainlossaccount.Text = ""

        fndStockTransferAc.Value = ""
        txtStockTransferAc.Text = ""

        fndCostOfGoodsTranfer.Value = ""
        txtCostOfGoodsTransfer.Text = ""

        fnddisplaypurposeacct.Value = ""
        txtdisplaypurposeact.Text = ""
        txtCostOfGoodsScheme.Value = ""
        lblCostOfGoodsScheme.Text = ""
    End Sub

    Private Sub rdbtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnClose.Click
        Me.Close()
    End Sub

    Private Sub fndaccountsetcode_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fndaccountsetcode.Value <> "" Then
            rdtxtdesc.Enabled = True
            'fndsales.txtValue.Enabled = True
            rdtxtsales.ReadOnly = True
            'fndreturns.txtValue.Enabled = True
            rdtxtreturns.ReadOnly = True
            'fndcostofgoods.txtValue.Enabled = True
            rdtxtcostofgoodssold.ReadOnly = True
            'fndcostofvariance.txtValue.Enabled = True
            rdtxtcostofvariance.ReadOnly = True
            'fnddamagedgoods.txtValue.Enabled = True
            rdtxtdamegedgoods.ReadOnly = True
            'fndinternalusage.txtValue.Enabled = True
            rdtxtinternalusage.ReadOnly = True
            'fndreturn.txtValue.Enabled = True
            FndCogsInterBranch1.Enabled = True
            txtcogsInterbranch.ReadOnly = True
            txtreturn.ReadOnly = True
            rdbtnSave.Enabled = True
            rdbtndelete.Enabled = True

        End If

        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub
    Private Function CheckControlAccount(ByVal AccountType As String, ByVal AccountCode As String) As Boolean
        Try
            If clsCommon.myLen(AccountCode) > 0 Then
                Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + AccountCode + "' AND ControlAccount ='Y'"
                Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                If check1 <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Filled " + AccountType + " (" & AccountCode & ") must be control account.", Me.Text)
                    'Throw New Exception("Filled " + AccountType + " (" & AccountCode & ") must be control account.")
                    Return False
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Private Sub rdbtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnSave.Click
        SaveData()
    End Sub
    Sub SaveData()
        If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) AndAlso fndaccountsetcode.Value = "" Then
            myMessages.blankValue("Account Set Code")
            fndaccountsetcode.Focus()
        ElseIf fndSales.Value = "" Then
            myMessages.blankValue("Sale")
            fndSales.Focus()
        ElseIf (fndReturns.Value = "") Then
            myMessages.blankValue("Returns")
            fndReturns.Focus()
        ElseIf (fndcostofgoods.Value = "") Then
            myMessages.blankValue("cost of good")
            fndcostofgoods.Focus()
        ElseIf (fndcostofvariance.Value = "") Then
            myMessages.blankValue("Cost of variance")
            fndcostofvariance.Focus()
        ElseIf (fndDamagedGoods.Value = "") Then
            myMessages.blankValue("Damaged goods")
            fndDamagedGoods.Focus()
        ElseIf (fndinternalusage.Value = "") Then
            myMessages.blankValue("Internal Usages")
            fndinternalusage.Focus()
        ElseIf (fndReturnableCont.Value = "") Then
            myMessages.blankValue("Returnable Container")
            fndReturnableCont.Focus()
        ElseIf (FndCogsInterBranch1.Value = "") Then
            myMessages.blankValue("Cogs InterBranch")
            FndCogsInterBranch1.Focus()
        ElseIf (fnddisplaypurposeacct.Value = "") Then
            myMessages.blankValue("Display Purpose")
            fnddisplaypurposeacct.Focus()
        ElseIf CheckControlAccount("Sale", fndSales.Value) = False Then
            fndSales.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Returns", fndReturns.Value) = False Then
            fndReturns.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Cost of Goods Sold", fndcostofgoods.Value) = False Then
            fndcostofgoods.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Cost of Variance", fndcostofvariance.Value) = False Then
            fndcostofvariance.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Damaged Goods", fndDamagedGoods.Value) = False Then
            fndDamagedGoods.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Internal Usage", fndinternalusage.Value) = False Then
            fndinternalusage.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Returnable Container", fndReturnableCont.Value) = False Then
            fndReturnableCont.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Schemes", fndSchemes.Value) = False Then
            fndSchemes.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Promotional", fndPromotional.Value) = False Then
            fndPromotional.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Cogs InterBranch", FndCogsInterBranch1.Value) = False Then
            FndCogsInterBranch1.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Sale Suspence A/C", txtSalesSuspenceAC.Value) = False Then
            txtSalesSuspenceAC.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Gain/Loss A/C", FndGainLossaccount.Value) = False Then
            FndGainLossaccount.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Stock Transfer A/C", fndStockTransferAc.Value) = False Then
            fndStockTransferAc.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Cost Of Goods Transfer", fndCostOfGoodsTranfer.Value) = False Then
            fndCostOfGoodsTranfer.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Display Purpose Accounts", fnddisplaypurposeacct.Value) = False Then
            fnddisplaypurposeacct.Focus()
            Exit Sub
        Else

            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.itemSaleAccount, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return
                End If
            End If
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, fndaccountsetcode.Value, "TSPL_SALES_ACCOUNTS", "Sales_Class_Code", Nothing)
            If rdbtnSave.Text = "Save" Then
                funinsert()
                funfill()
            ElseIf (rdbtnSave.Text = "Update") Then
                funupadte()
            End If
        End If
    End Sub
    Private Sub rdbtnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnreset.Click
        funreset()
    End Sub

    Private Sub rdbtndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtndelete.Click
        DeleteData()

    End Sub
    Sub DeleteData()
        If myMessages.deleteConfirm() Then
            fundelete()
            myMessages.delete()
            rdbtnSave.Text = "Save"
            fndaccountsetcode.MyReadOnly = False
            rdbtndelete.Enabled = False
        End If

    End Sub

    'Private Sub fndaccountsetcode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fndaccountsetcode.Query = "Select sales_class_code as [Account Code],sales_class_desc as [Account description]from tspl_sales_accounts"
    '    ' fndaccountsetcode.Query = clsERPFuncationality.UserAvailableAccountQuery
    '    fndaccountsetcode.ConnectionString = connectSql.SqlCon()
    '    fndaccountsetcode.Caption = "Sales Account"
    '    fndaccountsetcode.ValueToSelect = "Account Code"
    '    fndaccountsetcode.ValueToSelect1 = "Account description"
    'End Sub
    'This  function is used to retrieve data in all control.
    Public Sub funfill()
        Try
            Dim query As String = "Select sales_class_code,sales_class_desc,sales_account,sales_return_account,cost_of_goods_sold,cost_variance,damaged_goods,internal_usage,Returnable_Container,Schemes,Promotional,Cogs_InterBranch,Suspence_Account,Gain_Loss_Account,Stock_Transfer_AC,COGT_AC,TSPL_SALES_ACCOUNTS.DisplayPurpose_Account,Cost_Of_Goods_Scheme from tspl_sales_accounts where sales_class_code='" + fndaccountsetcode.Value + "'"
            Dim adp As New SqlDataAdapter(query, connectSql.SqlCon())
            Dim dt As New DataTable()
            adp.Fill(dt)
            Dim dr As DataRow = dt.Rows(0)
            fndaccountsetcode.Value = dr(0).ToString()
            rdtxtdesc.Text = dr(1).ToString()
            fndSales.Value = dr(2).ToString()
            rdtxtsales.Text = clsGLAccount.GetName(fndSales.Value)
            fndReturns.Value = dr(3).ToString()
            rdtxtreturns.Text = clsGLAccount.GetName(fndReturns.Value)
            fndcostofgoods.Value = dr(4).ToString()
            rdtxtcostofgoodssold.Text = clsGLAccount.GetName(fndcostofgoods.Value)
            fndcostofvariance.Value = dr(5).ToString()
            rdtxtcostofvariance.Text = clsGLAccount.GetName(fndcostofvariance.Value)
            fndDamagedGoods.Value = dr(6).ToString()
            rdtxtdamegedgoods.Text = clsGLAccount.GetName(fndDamagedGoods.Value)
            fndinternalusage.Value = dr(7).ToString()
            rdtxtinternalusage.Text = clsGLAccount.GetName(fndinternalusage.Value)
            fndReturnableCont.Value = dr(8).ToString()
            txtreturn.Text = clsGLAccount.GetName(fndReturnableCont.Value)
            fndSchemes.Value = dr(9).ToString()
            txtSchemes.Text = clsGLAccount.GetName(fndSchemes.Value)
            fndPromotional.Value = dr(10).ToString()
            txtPromotional.Text = clsGLAccount.GetName(fndPromotional.Value)
            FndCogsInterBranch1.Value = dr("Cogs_InterBranch").ToString()
            txtcogsInterbranch.Text = clsGLAccount.GetName(FndCogsInterBranch1.Value)
            'txtcogsInterbranch.Text = clsDBFuncationality.getSingleValue("select Description from tspl_gl_accounts where Account_Code ='" + FndCogsInterBranch1.Value + "'")
            'Dim qry As String = "select Description from tspl_gl_accounts where Account_Code ='" + FndCogsInterBranch1.Value + "'"
            'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            'If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            '    txtcogsInterbranch.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
            'Else
            '    txtcogsInterbranch.Text = ""
            'End If
            'FndCogsInterBranch1.Value = dr("Cogs_InterBranch").ToString()
            'getdescription(FndCogsInterBranch1.Value)

            txtSalesSuspenceAC.Value = dr("Suspence_Account").ToString()
            lblSaleSuspenceAC.Text = clsGLAccount.GetName(txtSalesSuspenceAC.Value)

            FndGainLossaccount.Value = clsCommon.myCstr(dr("Gain_Loss_Account"))
            lblgainlossaccount.Text = clsGLAccount.GetName(FndGainLossaccount.Value)

            fndStockTransferAc.Value = clsCommon.myCstr(dr("Stock_Transfer_AC"))
            txtStockTransferAc.Text = clsGLAccount.GetName(fndStockTransferAc.Value)
            'lblSaleSuspenceAC.Text = clsDBFuncationality.getSingleValue("select Description from tspl_gl_accounts where Account_Code ='" + txtSalesSuspenceAC.Value + "'")

            fndCostOfGoodsTranfer.Value = clsCommon.myCstr(dr("COGT_AC"))
            txtCostOfGoodsTransfer.Text = clsGLAccount.GetName(fndCostOfGoodsTranfer.Value)

            fnddisplaypurposeacct.Value = clsCommon.myCstr(dr("DisplayPurpose_Account"))
            txtdisplaypurposeact.Text = clsGLAccount.GetName(fnddisplaypurposeacct.Value)

            txtCostOfGoodsScheme.Value = clsCommon.myCstr(dr("Cost_Of_Goods_Scheme"))
            lblCostOfGoodsScheme.Text = clsGLAccount.GetName(txtCostOfGoodsScheme.Value)

            rdbtnSave.Text = "Update"
            fndaccountsetcode.MyReadOnly = True
            rdbtnSave.Enabled = True
            rdbtndelete.Enabled = True


        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Public Sub text_changed(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim query As String = "select * from tspl_sales_accounts where Sales_Class_Code='" + fndaccountsetcode.Value + "'"
        dt = clsDBFuncationality.GetDataTable(query)
        Dim str As String = ""
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                str = dr(0).ToString()
            Next
        End If
        If str <> "" Then
            funfill()
            rdbtnSave.Text = "Update"
            fndaccountsetcode.MyReadOnly = True
            rdtxtdesc.Enabled = True
            fndSales.Enabled = True
            rdtxtsales.ReadOnly = True
            fndReturns.Enabled = True
            rdtxtreturns.ReadOnly = True
            fndcostofgoods.Enabled = True
            rdtxtcostofgoodssold.ReadOnly = True
            fndcostofvariance.Enabled = True
            rdtxtcostofvariance.ReadOnly = True
            fndDamagedGoods.Enabled = True
            rdtxtdamegedgoods.ReadOnly = True
            fndinternalusage.Enabled = True
            rdtxtinternalusage.ReadOnly = True
            fndReturnableCont.Enabled = True
            txtreturn.ReadOnly = True
            FndCogsInterBranch1.Enabled = True
            txtcogsInterbranch.ReadOnly = True
            rdbtndelete.Enabled = True

        Else
            rdbtnSave.Text = "Save"
            fndaccountsetcode.MyReadOnly = False
            rdtxtdesc.Text = ""
            fndcostofgoods.Value = ""
            fndcostofvariance.Value = ""
            fndDamagedGoods.Value = ""
            fndinternalusage.Value = ""
            fndReturns.Value = ""
            fndSales.Value = ""
            fndReturnableCont.Value = ""
            txtreturn.ReadOnly = False
            rdtxtcostofgoodssold.ReadOnly = True
            rdtxtcostofvariance.ReadOnly = True
            rdtxtdamegedgoods.ReadOnly = True
            rdtxtinternalusage.ReadOnly = True
            rdtxtreturns.ReadOnly = True
            rdtxtsales.ReadOnly = True
            rdbtndelete.Enabled = False
            fndSchemes.Value = ""
            fndPromotional.Value = ""
            FndCogsInterBranch1.Value = ""
            txtcogsInterbranch.ReadOnly = True

        End If
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub

    Private Sub radmenuexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radmenuexit.Click
        Me.Close()
    End Sub

    Private Sub RadMenuexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuexport.Click
        ' Ticket No : TEC/14/06/19-000535 By Prabhakar
        Dim query As String = "select sales_class_code as 'Sales Class Code',sales_class_desc as 'Sales Class Description',sales_account as 'Sales Account',sales_return_account as 'Sales Return Account',Cost_of_goods_sold as 'Cost Of Goods Sold',Cost_Variance as 'Cost Variance',Damaged_goods as 'Damaged Goods',Internal_Usage  as 'Internal Usages',Returnable_Container as 'Returnable Container',schemes as [Schemes],promotional as [Promotional],Cogs_InterBranch as [Cogs InterBranch], Suspence_Account as 'Suspence Account',Stock_transfer_ac as [Stock Transfer AC], COGT_AC as [Cost Of Goods Transfer AC], Cost_Of_Goods_Scheme as [Cost Of Goods Scheme] from tspl_sales_accounts "
        ListImpExpColumnsMandatory = New List(Of String)({"Sales Class Code"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"Sales Class Code"})
        transportSql.ExporttoExcel(query, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
    End Sub

    Private Sub rdmenuimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdmenuimport.Click
        Dim dgv As New RadGridView
        Me.Controls.Add(dgv)
        If transportSql.importExcel(dgv, "Sales Class Code", "Sales Class Description", "Sales Account", "Sales Return Account", "Cost Of Goods Sold", "Cost Variance", "Damaged Goods", "Internal Usages", "Returnable Container", "Schemes", "Promotional", "Cogs InterBranch", "Suspence Account", "Stock Transfer AC", "Cost Of Goods Transfer AC", "Cost Of Goods Scheme") Then
            Dim linno As Integer = 0
            Dim trans As SqlTransaction = Nothing
            Try
                connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()


                For Each dgrv As GridViewRowInfo In dgv.Rows
                    linno += 1
                    Dim strsalesclasscode As String = clsCommon.myCstr(dgrv.Cells(0).Value)
                    Dim strdescription As String = clsCommon.myCstr(dgrv.Cells(1).Value)
                    Dim strsalesaccount As String = clsCommon.myCstr(dgrv.Cells(2).Value)

                    '' Anubhooti 06-Nov-2014
                    If clsCommon.myLen(clsCommon.myCstr(dgrv.Cells("Stock Transfer AC").Value)) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + clsCommon.myCstr(dgrv.Cells("Stock Transfer AC").Value) + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled Stock Transfer account ( " & clsCommon.myCstr(dgrv.Cells("Stock Transfer AC").Value) & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If

                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + clsCommon.myCstr(dgrv.Cells("Stock Transfer AC").Value) + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled Stock Transfer account ( " & clsCommon.myCstr(dgrv.Cells("Stock Transfer AC").Value) & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If

                    End If

                    If clsCommon.myLen(clsCommon.myCstr(dgrv.Cells("Cost Of Goods Transfer AC").Value)) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + clsCommon.myCstr(dgrv.Cells("Cost Of Goods Transfer AC").Value) + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled Cost Of Goods Transfer AC ( " & clsCommon.myCstr(dgrv.Cells("Cost Of Goods Transfer AC").Value) & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If

                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + clsCommon.myCstr(dgrv.Cells("Cost Of Goods Transfer AC").Value) + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled Cost Of Goods Transfer AC ( " & clsCommon.myCstr(dgrv.Cells("Cost Of Goods Transfer AC").Value) & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If

                    End If

                    If clsCommon.myLen(strsalesaccount) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strsalesaccount + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled sales account ( " & strsalesaccount & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strsalesaccount + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled sales account ( " & strsalesaccount & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    ''
                    Dim strsalesreturnaccount As String = clsCommon.myCstr(dgrv.Cells(3).Value)
                    '' Anubhooti 06-Nov-2014
                    If clsCommon.myLen(strsalesreturnaccount) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strsalesreturnaccount + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled sales return account ( " & strsalesreturnaccount & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strsalesreturnaccount + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled sales return account ( " & strsalesreturnaccount & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    Dim strcostofgoodsold As String = clsCommon.myCstr(dgrv.Cells(4).Value)
                    '' Anubhooti 06-Nov-2014
                    If clsCommon.myLen(strcostofgoodsold) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strcostofgoodsold + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled cost of goods sold ( " & strcostofgoodsold & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strcostofgoodsold + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled cost of goods sold ( " & strcostofgoodsold & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    Dim strcostofvarience As String = clsCommon.myCstr(dgrv.Cells(5).Value)
                    '' Anubhooti 06-Nov-2014
                    If clsCommon.myLen(strcostofvarience) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strcostofvarience + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled cost variance ( " & strcostofvarience & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strcostofvarience + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled cost variance ( " & strcostofvarience & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    Dim strdamagedgoods As String = clsCommon.myCstr(dgrv.Cells(6).Value)
                    '' Anubhooti 06-Nov-2014
                    If clsCommon.myLen(strdamagedgoods) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strdamagedgoods + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled damaged goods ( " & strdamagedgoods & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strdamagedgoods + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled damaged goods ( " & strdamagedgoods & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    Dim strinternalusages As String = clsCommon.myCstr(dgrv.Cells(7).Value)
                    '' Anubhooti 06-Nov-2014
                    If clsCommon.myLen(strinternalusages) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strinternalusages + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled internal usages ( " & strinternalusages & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strinternalusages + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled internal usages ( " & strinternalusages & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    Dim strreturn As String = clsCommon.myCstr(dgrv.Cells(8).Value)
                    '' Anubhooti 06-Nov-2014
                    If clsCommon.myLen(strreturn) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strreturn + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled returnable container ( " & strreturn & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strreturn + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled returnable container ( " & strreturn & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    Dim strschemes As String = clsCommon.myCstr(dgrv.Cells(9).Value)
                    '' Anubhooti 06-Nov-2014
                    If clsCommon.myLen(strschemes) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strschemes + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled schemes ( " & strschemes & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strschemes + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled schemes ( " & strschemes & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    Dim strpromotional As String = clsCommon.myCstr(dgrv.Cells(10).Value)
                    '' Anubhooti 06-Nov-2014
                    If clsCommon.myLen(strpromotional) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strpromotional + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled promotional ( " & strpromotional & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strpromotional + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled promotional ( " & strpromotional & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    Dim cogsInterBranch As String = clsCommon.myCstr(dgrv.Cells("Cogs InterBranch").Value)
                    '' Anubhooti 06-Nov-2014
                    If clsCommon.myLen(cogsInterBranch) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + cogsInterBranch + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled cogs interBranch ( " & cogsInterBranch & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + cogsInterBranch + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled cogs interBranch ( " & cogsInterBranch & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    Dim strSuspenceAc As String = clsCommon.myCstr(dgrv.Cells("Suspence Account").Value)
                    '' Anubhooti 06-Nov-2014
                    If clsCommon.myLen(strSuspenceAc) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strSuspenceAc + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled suspence account ( " & strSuspenceAc & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strSuspenceAc + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled suspence account ( " & strSuspenceAc & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    ' cost of goods Scheme ERO/26/12/18-000451 ====================
                    Dim strCostOfGoodsScheme As String = clsCommon.myCstr(dgrv.Cells("Cost Of Goods Scheme").Value)
                    If clsCommon.myLen(strCostOfGoodsScheme) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strCostOfGoodsScheme + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled Cost Of Goods Scheme account ( " & strCostOfGoodsScheme & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strCostOfGoodsScheme + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled Cost Of Goods Scheme account ( " & strCostOfGoodsScheme & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If

                    '=====================================================
                    If String.IsNullOrEmpty(strsalesclasscode) Or clsCommon.myLen(strsalesclasscode) > 6 Then
                        Throw New Exception("Sales Acccount has some incorrect values")

                    End If
                    Dim sql1 As String = "select count(*)from tspl_sales_accounts where Sales_class_code='" + strsalesclasscode + "'"
                    Dim i As Integer = CInt(connectSql.RunScalar(trans, sql1))
                    If (i = 0) Then
                        ' Dim query As String = "insert into tspl_Sales_accounts values('" + strsalesclasscode + "','" + strdescription + "','" + strsalesaccount + "','" + strsalesreturnaccount + "','" + strcostofgoodsold + "','" + strcostofvarience + "','" + strdamagedgoods + "','" + strinternalusages + "','" + strreturn + "','" + userCode + "','" + connectSql.serverDate(trans) + "','" + userCode + "','" + connectSql.serverDate(trans) + "','" + companyCode + "','" + strschemes + "','" + strpromotional + "','" + cogsInterBranch + "','" + strSuspenceAc + "','" + clsCommon.myCstr(dgrv.Cells("Stock Transfer AC").Value) + "','" + clsCommon.myCstr(dgrv.Cells("Cost Of Goods Transfer AC").Value) + "')"
                        Dim query As String = "insert into tspl_Sales_accounts(Sales_Class_Code,Sales_Class_Desc,Sales_Account,Sales_Return_Account,Cost_Of_Goods_Sold,Cost_Variance,Damaged_Goods,Internal_Usage,Returnable_container,Created_By,Created_Date,Modify_By,Modify_Date,Comp_Code,Schemes,Promotional,Cogs_InterBranch,suspence_Account,Gain_Loss_Account,Stock_Transfer_Ac,COGT_AC,Cost_Of_Goods_Scheme) values('" + strsalesclasscode + "','" + strdescription + "','" + strsalesaccount + "','" + strsalesreturnaccount + "','" + strcostofgoodsold + "','" + strcostofvarience + "','" + strdamagedgoods + "','" + strinternalusages + "','" + strreturn + "','" + userCode + "','" + connectSql.serverDate(trans) + "','" + userCode + "','" + connectSql.serverDate(trans) + "','" + companyCode + "','" + strschemes + "','" + strpromotional + "','" + cogsInterBranch + "','" + strSuspenceAc + "','" + clsCommon.myCstr(dgrv.Cells("Stock Transfer AC").Value) + "','" + clsCommon.myCstr(dgrv.Cells("Cost Of Goods Transfer AC").Value) + "','',(case when " & clsCommon.myLen(strCostOfGoodsScheme) & ">0 then '" + clsCommon.myCstr(strCostOfGoodsScheme) + "' else null end) )"
                        connectSql.RunSqlTransaction(trans, query)
                    Else
                        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strsalesclasscode, "TSPL_SALES_ACCOUNTS", "Sales_Class_Code", trans)
                        Dim query1 As String = "update tspl_sales_accounts set sales_class_desc='" + strdescription + "',sales_account='" + strsalesaccount + "',sales_return_account='" + strsalesreturnaccount + "',Cost_of_goods_sold='" + strcostofgoodsold + "',Cost_Variance='" + strcostofvarience + "',Damaged_goods='" + strdamagedgoods + "',Internal_Usage='" + strinternalusages + "',Returnable_Container='" + strreturn + "',Schemes='" + strschemes + "',Promotional='" + strpromotional + "',Cogs_InterBranch='" + cogsInterBranch + "',Suspence_Account='" + strSuspenceAc + "',Stock_transfer_Ac='" + clsCommon.myCstr(dgrv.Cells("Stock Transfer AC").Value) + "',COGT_AC='" + clsCommon.myCstr(dgrv.Cells("Cost Of Goods Transfer AC").Value) + "', Cost_Of_Goods_Scheme = (case when " & clsCommon.myLen(strCostOfGoodsScheme) & ">0 then '" + clsCommon.myCstr(strCostOfGoodsScheme) + "' else null end) where sales_class_code='" + strsalesclasscode + "'"
                        connectSql.RunSqlTransaction(trans, query1)
                    End If
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transferred Completed", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()

                myMessages.myExceptions(ex)

            End Try
        End If
        Me.Controls.Remove(dgv)
    End Sub

    'Private Sub fndsales_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fillAccounts(fndSales)
    'End Sub

    'Private Sub fndreturns_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fillAccounts(fndReturns)
    'End Sub

    'Private Sub fndcostofgoods_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fillAccounts(fndcostofgoods)
    'End Sub

    'Private Sub fndcostofvariance_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fillAccounts(fndcostofvariance)
    'End Sub

    'Private Sub fnddamagedgoods_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fnddamagedgoods.Load
    '    fillAccounts(fnddamagedgoods)
    'End Sub

    'Private Sub fndinternalusage_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fillAccounts(fndinternalusage)
    'End Sub

    'Private Sub fndSchemes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fillAccounts(fndSchemes)
    'End Sub

    'Private Sub fndpromotional_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fillAccounts(fndPromotional)
    'End Sub
    'Private Sub fndCogsInterBranch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fndCogsInterBranch.Load
    '    fillAccounts(fndCogsInterBranch)
    'End Sub

    'Public Sub sales_textchanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    finderTextChanged(fndsales, rdtxtsales)
    'End Sub
    'Public Sub returns_textchanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    finderTextChanged(fndreturns, rdtxtreturns)
    'End Sub
    'Public Sub costofgood_textchanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    finderTextChanged(fndcostofgoods, rdtxtcostofgoodssold)
    'End Sub
    'Public Sub costofvariance_textchanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    finderTextChanged(fndcostofvariance, rdtxtcostofvariance)
    'End Sub
    'Public Sub damagedgoods_textchanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    finderTextChanged(fnddamagedgoods, rdtxtdamegedgoods)

    'End Sub

    'Public Sub internalusages_textchanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    finderTextChanged(fndinternalusage, rdtxtinternalusage)

    'End Sub

    'Public Sub fndSchemes_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    finderTextChanged(fndSchemes, txtSchemes)
    'End Sub

    'Public Sub fndPromotional_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    finderTextChanged(fndPromotional, txtPromotional)
    'End Sub

    'Public Sub sales_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    finderLeave(fndsales, rdtxtsales)
    'End Sub
    'Public Sub returns_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    finderLeave(fndreturns, rdtxtreturns)
    'End Sub
    'Public Sub cosiofgoodsold_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    finderLeave(fndcostofgoods, rdtxtcostofgoodssold)
    'End Sub
    'Public Sub costofgoodvariance_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    finderLeave(fndcostofvariance, rdtxtcostofvariance)
    'End Sub
    'Public Sub damagedgoods_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    finderLeave(fnddamagedgoods, rdtxtdamegedgoods)
    'End Sub
    'Public Sub internalusages_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    finderLeave(fndinternalusage, rdtxtinternalusage)
    'End Sub

    'Public Sub fndSchemes_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    finderLeave(fndSchemes, txtSchemes)
    'End Sub

    'Public Sub fndPromotional_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    finderLeave(fndPromotional, txtPromotional)
    'End Sub

    'priti added on 01-06-2011 --- To implement the access control
    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        'If funCheckLoginStatus() = False Then Exit Function
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "ITEM-SAL-ACC"
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
    '            rdbtnSave.Enabled = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            rdbtndelete.Enabled = False
    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception

    '    End Try
    'End Function
    'Code ends here


    Private Sub fndreturn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fillAccounts(fndreturn)
    End Sub
    'Private Sub fillAccounts(ByVal finder As finder.finder)
    '    finder.ConnectionString = connectSql.SqlCon()
    '    'finder.Query = "select account_code as [Account Code],description as [Description] from Tspl_gl_Accounts"
    '    finder.Query = clsERPFuncationality.glaccountquery
    '    finder.ValueToSelect = "Account_Code"
    '    finder.Caption = "Account"
    '    finder.ValueToSelect1 = "Description"
    'End Sub

    'Private Sub finderTextChanged(ByVal finder As finder.finder, ByVal txtdesc As RadTextBox)
    '    Try
    '        Dim query As String = "Select description as [Description] from tspl_gl_accounts where account_code='" + finder.txtValue.Text + "'"
    '        txtdesc.Text = connectSql.RunScalar(query)
    '    Catch ex As Exception
    '        myMessages.myExceptions(ex)
    '    End Try
    'End Sub

    'Private Sub finderLeave(ByVal finder As finder.finder, ByVal txtdesc As RadTextBox)
    '    If fndSales.Value = "" Then

    '    Else
    '        Try

    '            Dim str As String = "select count(account_code)  from Tspl_gl_Accounts where account_code='" + finder.txtValue.Text + "'"
    '            Dim strvalue As Integer = connectSql.RunScalar(str)
    '            If (clsCommon.myLen(finder.txtValue.Text) > 0) Then
    '                If strvalue = 0 Then
    '                    txtdesc.Text = ""
    '                    common.clsCommon.MyMessageBoxShow("This Account does not exist")
    '                    finder.txtValue.Text = ""
    '                    finder.Focus()
    '                End If
    '            End If
    '        Catch ex As Exception
    '            myMessages.myExceptions(ex)
    '        End Try
    '    End If
    'End Sub


    Private Sub FndCogsInterBranch1__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles FndCogsInterBranch1._MYValidating
        Dim query As String = "select Account_Code as [Account],Description from tspl_gl_accounts"
        FndCogsInterBranch1.Value = clsCommon.ShowSelectForm("CogsInterBranchfd", query, "Account", " ControlAccount ='Y' ", FndCogsInterBranch1.Value, "Account", isButtonClicked)
        getdescription(FndCogsInterBranch1.Value)
    End Sub

    Private Sub FndCogsInterBranch1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles FndCogsInterBranch1.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub
    Public Sub getdescription(ByVal account As String)
        Dim qry As String = "select Description from tspl_gl_accounts where Account_Code ='" + FndCogsInterBranch1.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            txtcogsInterbranch.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
        Else
            txtcogsInterbranch.Text = ""
        End If
    End Sub

    Private Sub fndaccountsetcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndaccountsetcode._MYValidating
        Dim str As String = "select count(*) from tspl_sales_accounts where sales_class_code ='" + fndaccountsetcode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 Then
            fndaccountsetcode.MyReadOnly = False
        Else
            fndaccountsetcode.MyReadOnly = True
        End If
        If fndaccountsetcode.MyReadOnly OrElse isButtonClicked Then

            'Dim qry As String = "Select sales_class_code as Code,sales_class_desc as [Account description]from tspl_sales_accounts"
            'fndaccountsetcode.Value = clsCommon.ShowSelectForm("AccSetCode", qry, "Code", "", fndaccountsetcode.Value, "", isButtonClicked)
            fndaccountsetcode.Value = clsSalesAccountSet.getFinder("", fndaccountsetcode.Value, isButtonClicked)
            LoadData()
        End If
    End Sub
    Sub LoadData()
        Dim query As String = "select * from tspl_sales_accounts where Sales_Class_Code='" + fndaccountsetcode.Value + "'"
        dt = clsDBFuncationality.GetDataTable(query)
        Dim str As String = ""
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                str = dr(0).ToString()
            Next
        End If
      
        If str <> "" Then
            funfill()
            rdbtnSave.Text = "Update"
            fndaccountsetcode.MyReadOnly = True
            rdtxtdesc.Enabled = True
            fndSales.Enabled = True
            rdtxtsales.ReadOnly = True
            fndReturns.Enabled = True
            rdtxtreturns.ReadOnly = True
            fndcostofgoods.Enabled = True
            rdtxtcostofgoodssold.ReadOnly = True
            fndcostofvariance.Enabled = True
            rdtxtcostofvariance.ReadOnly = True
            fndDamagedGoods.Enabled = True
            rdtxtdamegedgoods.ReadOnly = True
            fndinternalusage.Enabled = True
            rdtxtinternalusage.ReadOnly = True
            fndReturnableCont.Enabled = True
            txtreturn.ReadOnly = True
            FndCogsInterBranch1.Enabled = True
            txtcogsInterbranch.ReadOnly = True
            rdbtndelete.Enabled = True

        Else
            rdbtnSave.Text = "Save"
            fndaccountsetcode.MyReadOnly = False
            rdtxtdesc.Text = ""
            fndcostofgoods.Value = ""
            fndcostofvariance.Value = ""
            fndDamagedGoods.Value = ""
            fndinternalusage.Value = ""
            fndReturns.Value = ""
            fndSales.Value = ""
            fndReturnableCont.Value = ""
            txtreturn.ReadOnly = False
            rdtxtcostofgoodssold.ReadOnly = True
            rdtxtcostofvariance.ReadOnly = True
            rdtxtdamegedgoods.ReadOnly = True
            rdtxtinternalusage.ReadOnly = True
            rdtxtreturns.ReadOnly = True
            rdtxtsales.ReadOnly = True
            rdbtndelete.Enabled = False
            fndSchemes.Value = ""
            fndPromotional.Value = ""
            FndCogsInterBranch1.Value = ""
            txtcogsInterbranch.ReadOnly = True
            fndStockTransferAc.Value = ""
            txtStockTransferAc.Text = ""
            fndCostOfGoodsTranfer.Value = ""
            txtCostOfGoodsTransfer.Text = ""
            fnddisplaypurposeacct.Value = ""
            txtdisplaypurposeact.Text = ""
        End If
    End Sub

    Private Sub fndaccountsetcode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndaccountsetcode._MYNavigator
        Dim qst As String = "Select sales_class_code as Code,sales_class_desc as [Account description] from tspl_sales_accounts where 2=2 "
        Select Case NavType
            Case NavigatorType.Current
                qst += " and tspl_sales_accounts .sales_class_code in ('" + fndaccountsetcode.Value + "')"
            Case NavigatorType.Next
                qst += " and tspl_sales_accounts .sales_class_code in (select min(sales_class_code ) from tspl_sales_accounts where sales_class_code  >'" + fndaccountsetcode.Value + "')"
            Case NavigatorType.First
                qst += " and tspl_sales_accounts .sales_class_code in (select MIN(sales_class_code ) from tspl_sales_accounts)"

            Case NavigatorType.Last
                qst += " and tspl_sales_accounts .sales_class_code in (select Max(sales_class_code ) from tspl_sales_accounts)"
            Case NavigatorType.Previous
                qst += " and tspl_sales_accounts .sales_class_code in (select Max(sales_class_code ) from tspl_sales_accounts where sales_class_code  <'" + fndaccountsetcode.Value + "')"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            fndaccountsetcode.Value = clsCommon.myCstr(dt.Rows(0)("Code"))
            rdtxtdesc.Text = clsCommon.myCstr(dt.Rows(0)("Account description"))
        End If
        LoadData()
    End Sub

    Private Sub txtSalesSuspenceAC__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtSalesSuspenceAC._MYValidating
        Dim query As String = "select Account_Code as [Account],Description from tspl_gl_accounts"
        txtSalesSuspenceAC.Value = clsCommon.ShowSelectForm("CogsInterBranchfd", query, "Account", " ControlAccount ='Y' ", txtSalesSuspenceAC.Value, "Account", isButtonClicked)
        query = "select Description from tspl_gl_accounts where Account_Code ='" + txtSalesSuspenceAC.Value + "'"
        lblSaleSuspenceAC.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(query))
    End Sub

    Private Sub fndSales__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndSales._MYValidating
        Dim query As String = "select Account_Code as [Account],Description from tspl_gl_accounts"
        fndSales.Value = clsCommon.ShowSelectForm("CogsInterBranchfd", query, "Account", " ControlAccount ='Y' ", fndSales.Value, "Account", isButtonClicked)
        query = "select Description from tspl_gl_accounts where Account_Code ='" + fndSales.Value + "'"
        rdtxtsales.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(query))
    End Sub

    Private Sub fndReturns__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndReturns._MYValidating
        Dim query As String = "select Account_Code as [Account],Description from tspl_gl_accounts"
        fndReturns.Value = clsCommon.ShowSelectForm("CogsInterBranchfd", query, "Account", " ControlAccount ='Y' ", fndReturns.Value, "Account", isButtonClicked)
        query = "select Description from tspl_gl_accounts where Account_Code ='" + fndReturns.Value + "'"
        rdtxtreturns.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(query))
    End Sub

    Private Sub fndcostofgoods__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndcostofgoods._MYValidating
        Dim query As String = "select Account_Code as [Account],Description from tspl_gl_accounts"
        fndcostofgoods.Value = clsCommon.ShowSelectForm("CogsInterBranchfd", query, "Account", " ControlAccount ='Y' ", fndcostofgoods.Value, "Account", isButtonClicked)
        query = "select Description from tspl_gl_accounts where Account_Code ='" + fndcostofgoods.Value + "'"
        rdtxtcostofgoodssold.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(query))
    End Sub

    Private Sub fndcostofvariance__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndcostofvariance._MYValidating
        Dim query As String = "select Account_Code as [Account],Description from tspl_gl_accounts"
        fndcostofvariance.Value = clsCommon.ShowSelectForm("CogsInterBranchfd", query, "Account", " ControlAccount ='Y' ", fndcostofvariance.Value, "Account", isButtonClicked)
        query = "select Description from tspl_gl_accounts where Account_Code ='" + fndcostofvariance.Value + "'"
        rdtxtcostofvariance.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(query))
    End Sub

    Private Sub v__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndDamagedGoods._MYValidating
        Dim query As String = "select Account_Code as [Account],Description from tspl_gl_accounts"
        fndDamagedGoods.Value = clsCommon.ShowSelectForm("CogsInterBranchfd", query, "Account", " ControlAccount ='Y' ", fndDamagedGoods.Value, "Account", isButtonClicked)
        query = "select Description from tspl_gl_accounts where Account_Code ='" + fndDamagedGoods.Value + "'"
        rdtxtdamegedgoods.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(query))
    End Sub

    Private Sub fndinternalusage__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndinternalusage._MYValidating
        Dim query As String = "select Account_Code as [Account],Description from tspl_gl_accounts"
        fndinternalusage.Value = clsCommon.ShowSelectForm("CogsInterBranchfd", query, "Account", " ControlAccount ='Y' ", fndinternalusage.Value, "Account", isButtonClicked)
        query = "select Description from tspl_gl_accounts where Account_Code ='" + fndinternalusage.Value + "'"
        rdtxtinternalusage.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(query))
    End Sub

    Private Sub fndReturnableCont__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndReturnableCont._MYValidating
        Dim query As String = "select Account_Code as [Account],Description from tspl_gl_accounts"
        fndReturnableCont.Value = clsCommon.ShowSelectForm("CogsInterBranchfd", query, "Account", " ControlAccount ='Y' ", fndReturnableCont.Value, "Account", isButtonClicked)
        query = "select Description from tspl_gl_accounts where Account_Code ='" + fndReturnableCont.Value + "'"
        txtreturn.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(query))
    End Sub

    Private Sub fndSchemes__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndSchemes._MYValidating
        Dim query As String = "select Account_Code as [Account],Description from tspl_gl_accounts"
        fndSchemes.Value = clsCommon.ShowSelectForm("CogsInterBranchfd", query, "Account", " ControlAccount ='Y' ", fndSchemes.Value, "Account", isButtonClicked)
        query = "select Description from tspl_gl_accounts where Account_Code ='" + fndSchemes.Value + "'"
        txtSchemes.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(query))
    End Sub

    Private Sub fndPromotional__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndPromotional._MYValidating
        Dim query As String = "select Account_Code as [Account],Description from tspl_gl_accounts"
        fndPromotional.Value = clsCommon.ShowSelectForm("CogsInterBranchfd", query, "Account", " ControlAccount ='Y' ", fndPromotional.Value, "Account", isButtonClicked)
        query = "select Description from tspl_gl_accounts where Account_Code ='" + fndPromotional.Value + "'"
        txtPromotional.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(query))
    End Sub

    Private Sub FndGainLossaccount__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles FndGainLossaccount._MYValidating
        Dim query As String = "select Account_Code as [Account],Description from tspl_gl_accounts"
        FndGainLossaccount.Value = clsCommon.ShowSelectForm("srcGainLossaccount", query, "Account", " ControlAccount ='Y' ", FndGainLossaccount.Value, "Account", isButtonClicked)
        query = "select Description from tspl_gl_accounts where Account_Code ='" + FndGainLossaccount.Value + "'"
        lblgainlossaccount.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(query))
    End Sub
    Private Sub FndStockTransferAc__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndStockTransferAc._MYValidating
        Dim query As String = "select Account_Code as [Account],Description from tspl_gl_accounts"
        fndStockTransferAc.Value = clsCommon.ShowSelectForm("srcStkTRNaccount", query, "Account", " ControlAccount ='Y' ", fndStockTransferAc.Value, "Account", isButtonClicked)
        query = "select Description from tspl_gl_accounts where Account_Code ='" + fndStockTransferAc.Value + "'"
        txtStockTransferAc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(query))
    End Sub
    Private Sub FndCostOfGoodsTransferAC__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndCostOfGoodsTranfer._MYValidating
        Dim query As String = "select Account_Code as [Account],Description from tspl_gl_accounts"
        fndCostOfGoodsTranfer.Value = clsCommon.ShowSelectForm("srcCOGTaccount", query, "Account", " ControlAccount ='Y' ", fndCostOfGoodsTranfer.Value, "Account", isButtonClicked)
        query = "select Description from tspl_gl_accounts where Account_Code ='" + fndCostOfGoodsTranfer.Value + "'"
        txtCostOfGoodsTransfer.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(query))
    End Sub

    Private Sub fnddisplaypurposeacct__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fnddisplaypurposeacct._MYValidating
        Dim query As String = "select Account_Code as [Account],Description from tspl_gl_accounts"
        fnddisplaypurposeacct.Value = clsCommon.ShowSelectForm("srcCOGTaccount", query, "Account", " ControlAccount ='Y' ", fnddisplaypurposeacct.Value, "Account", isButtonClicked)
        query = "select Description from tspl_gl_accounts where Account_Code ='" + fnddisplaypurposeacct.Value + "'"
        txtdisplaypurposeact.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(query))
    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(fndaccountsetcode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select Account Set", Me.Text)
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowHistoryData(fndaccountsetcode.Value, "Sales_Class_Code", "TSPL_sales_ACCOUNTS")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub txtCostOfGoodsScheme__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCostOfGoodsScheme._MYValidating
        Dim query As String = "select Account_Code as [Account],Description from tspl_gl_accounts"
        txtCostOfGoodsScheme.Value = clsCommon.ShowSelectForm("srcCostOfGoodsAccount", query, "Account", " ControlAccount ='Y' ", txtCostOfGoodsScheme.Value, "Account", isButtonClicked)
        query = "select Description from tspl_gl_accounts where Account_Code ='" + txtCostOfGoodsScheme.Value + "'"
        lblCostOfGoodsScheme.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(query))
    End Sub
End Class
