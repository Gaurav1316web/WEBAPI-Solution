Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Configuration
Imports Telerik.Collections.Generic
Imports Excel = Microsoft.Office.Interop.Excel
Imports common


Public Class frmPurcahseAccountSetCode
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
    ' Dim dr As SqlDataReader
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim EnableCostingMethod As Integer = 1
    'This cunstructer is used to send usercode andd compcode data in table.
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
    Public Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.itemPurchaseAccount)
        If Not (MyBase.isReadFlag) Then
            '--------richa Ticket no. BM00000003121 15/07/2014 
            Throw New Exception("Permission Denied")
        End If
        rdbtnsave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 02/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If rdbtnsave.Visible = True Then
            menuimport.Enabled = True
            rdmenuexport.Enabled = True
        Else
            menuimport.Enabled = False
            rdmenuexport.Enabled = False
        End If
        '--------------------------------------------------
        'btnPost.Visible = MyBase.isPostFlag
        rdbtndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub frmPurcahseAccountSetCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso rdbtnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso rdbtndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Public Sub SetLength()
        fndaccountsetcode.MyMaxLength = 6
        rdtxtdescription.MaxLength = 50
    End Sub

    Private Sub PurcahseAccountSetCode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(rdbtnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(rdbtndelete, "Press Alt+D for Delete")
        ButtonToolTip.SetToolTip(rdbtnsave, "Press Alt+S for Save")
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowPurchaseControlAc, clsFixedParameterCode.ShowPurchaseControlAc, Nothing)) = 1 Then
            MyLabel5.Visible = True
            txtPurchaseCTRL_Ac.Visible = True
            txtPurchaseCtrlAcDesc.Visible = True
        Else
            MyLabel5.Visible = False
            txtPurchaseCTRL_Ac.Visible = False
            txtPurchaseCtrlAcDesc.Visible = False
        End If

        fndLossAc.Visible = True
        lblLossAc.Visible = True
        fndaccountsetcode.MyMaxLength = 6
        fndaccountsetcode.MyCharacterCasing = CharacterCasing.Upper
        If fndaccountsetcode.Value = "" Then
            rdtxtdescription.Enabled = True
            fndshipmentclearing.Enabled = True
            fndInventoryControl.Enabled = True
            fndpayableclearing.Enabled = True
            fndadjustmentwriteoff.Enabled = True
            fndassamblycostoff.Enabled = True
            fndnonstockclearing.Enabled = True
            fndtransferclearing.Enabled = True
            fnddisassamblyexpense.Enabled = True
            fndphysicalinventrycontrol.Enabled = True
            fndcreditdebitnoteclr.Enabled = True
            fndLossAc.Enabled = True
            fndWIPAcc.Enabled = True
            fndRMCons.Enabled = True
            fndOther1.Enabled = True
            fndOther2.Enabled = True
            rdtxtWIPAcc.ReadOnly = True
            rdtxtRMCons.ReadOnly = True
            rdtxtOther1.ReadOnly = True
            rdtxtOther2.ReadOnly = True
            rdtxtadjustmentwriteoff.ReadOnly = True
            rdtxtassamblycostcredit.ReadOnly = True
            rdtxtcreditdebitnoteclr.ReadOnly = True
            rdtxtdisassamblyexpense.ReadOnly = True
            rdtxtinventrycontrol.ReadOnly = True
            rdtxtnonstockclearing.ReadOnly = True
            rdtxtpayableclearing.ReadOnly = True
            rdtxtshipmentexpense.ReadOnly = True
            rdtxttransferclearing.ReadOnly = True
            rdtxtphysicalinventryadj.ReadOnly = True
            txtLossAc.ReadOnly = True
            rdbtnsave.Enabled = True
            rdbtndelete.Enabled = False
            fndTransferGainLoss.Enabled = True
            txtTransferGainLossDesc.ReadOnly = True
        End If
        rdbtnsave.TabIndex = 13
        rdbtndelete.TabIndex = 14
        rdbtnclose.TabIndex = 15
        rdbtnnew.TabIndex = 16
        LoadCostingMethod()
        '===Sanjeet(08/02/2018)Enabled Costing Method==================
        cboCostingMethod.Enabled = False
        EnableCostingMethod = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnableCostingMethod, clsFixedParameterCode.EnableCostingMethod, Nothing))
        cboCostingMethod.SelectedValue = EnableCostingMethod
        '===================

        lblLossAc.Text = "Gain/Loss A/C"
    End Sub

    Private Sub LoadCostingMethod()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(Integer))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = EnumCostingMethod.NA
        dr("Name") = "NA"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = EnumCostingMethod.Averege
        dr("Name") = "Average"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = EnumCostingMethod.FIFO
        dr("Name") = "FIFO"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = EnumCostingMethod.LIFO
        dr("Name") = "LIFO"
        dt.Rows.Add(dr)


        cboCostingMethod.DataSource = dt
        cboCostingMethod.ValueMember = "Code"
        cboCostingMethod.DisplayMember = "Name"
    End Sub
    Private Sub breakage_textchanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Not String.IsNullOrEmpty(connectSql.RunScalar("select description from tspl_gl_accounts where account_Code = '" + fndbreakageglaccount.Value + "'")) Then
            fndbreakageglaccount.Value = connectSql.RunScalar("select description from tspl_gl_accounts where account_Code = '" + fndbreakageglaccount.Value + "'")
        Else
            fndbreakageglaccount.Value = ""
        End If
    End Sub
    'This function is used to insert data
    Public Sub funinsert()
        Try
            If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False)) Then
                Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code='" & fndaccountsetcode.Value & "'")
                If ChkNewEntry = 0 Then
                    fndaccountsetcode.Value = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"), clsDocType.PurchaseAccountSetCode, "", "")
                    If clsCommon.myLen(fndaccountsetcode.Value) <= 0 Then
                        Throw New Exception("Error in Code Generation")
                    End If
                End If
            End If
           


            connectSql.RunSp("purcahase_account_insert", New SqlParameter("@acc", fndaccountsetcode.Value), New SqlParameter("@desc", lblaccountsetdesc.Text.ToString()), New SqlParameter("@invcontrol", fndInventoryControl.Value), New SqlParameter("@invpayable ", fndpayableclearing.Value), New SqlParameter("@adjustacc ", fndadjustmentwriteoff.Value), New SqlParameter("@asscostcredit", fndassamblycostoff.Value), New SqlParameter("@nonstockclearing", fndnonstockclearing.Value), New SqlParameter("@transfer", fndtransferclearing.Value), New SqlParameter("@shipclearing", fndshipmentclearing.Value), New SqlParameter("@dismexp ", fnddisassamblyexpense.Value), New SqlParameter("@Physicalinv", fndphysicalinventrycontrol.Value), New SqlParameter("@Creditdebit", fndcreditdebitnoteclr.Value), New SqlParameter("@ReserveStock", fndReserveStock.Value), New SqlParameter("@WIPAccount", fndWIPAcc.Value), New SqlParameter("@RMConsumption", fndRMCons.Value), New SqlParameter("@Other1", fndOther1.Value), New SqlParameter("@Other2", fndOther2.Value), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate()), New SqlParameter("@comp_code", companyCode), New SqlParameter("@breakaglact", fndbreakageglaccount.Value), New SqlParameter("@indentrequired", IIf(clsCommon.myCBool(chk_indentrequired.Checked) = True, 1, 0)))
            clsDBFuncationality.ExecuteNonQuery("Update TSPL_PURCHASE_ACCOUNTS Set Handling_Charge=(case when " & clsCommon.myLen(fndHandlingCharge.Value) & ">0 then '" + clsCommon.myCstr(fndHandlingCharge.Value) + "' else null end), EMP=(case when " & clsCommon.myLen(fndHandlingCharge.Value) & ">0 then '" + clsCommon.myCstr(fndEMP.Value) + "' else null end),Store_Consumption_Acc=(case when " & clsCommon.myLen(fndStoreConsumptionAcc.Value) & ">0 then '" + clsCommon.myCstr(fndStoreConsumptionAcc.Value) + "' else null end), Item_Opening_Clearing =(case when " & clsCommon.myLen(fndItemOpeningClearing.Value) & ">0 then '" + clsCommon.myCstr(fndItemOpeningClearing.Value) + "' else null end)  where Purchase_Class_Code='" + fndaccountsetcode.Value + "' ")
            updateQty()
            myMessages.insert()

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    'This function is used to update data.
    Public Sub funupdate()
        Try
            connectSql.RunSp("purcahase_account_update", New SqlParameter("@acc", fndaccountsetcode.Value), New SqlParameter("@desc", lblaccountsetdesc.Text.ToString()), New SqlParameter("@invcontrol", fndInventoryControl.Value), New SqlParameter("@invpayable ", fndpayableclearing.Value), New SqlParameter("@adjustacc ", fndadjustmentwriteoff.Value), New SqlParameter("@asscostcredit", fndassamblycostoff.Value), New SqlParameter("@nonstockclearing", fndnonstockclearing.Value), New SqlParameter("@transfer", fndtransferclearing.Value), New SqlParameter("@shipclearing", fndshipmentclearing.Value), New SqlParameter("@dismexp ", fnddisassamblyexpense.Value), New SqlParameter("@Physicalinv", fndphysicalinventrycontrol.Value), New SqlParameter("@Creditdebit", fndcreditdebitnoteclr.Value), New SqlParameter("@ReserveStock", fndReserveStock.Value), New SqlParameter("@WIPAccount", fndWIPAcc.Value), New SqlParameter("@RMConsumption", fndRMCons.Value), New SqlParameter("@Other1", fndOther1.Value), New SqlParameter("@Other2", fndOther2.Value), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate()), New SqlParameter("@comp_code", companyCode), New SqlParameter("@breakaglact", fndbreakageglaccount.Value), New SqlParameter("@indentrequired", IIf(clsCommon.myCBool(chk_indentrequired.Checked) = True, 1, 0)))
            clsDBFuncationality.ExecuteNonQuery("Update TSPL_PURCHASE_ACCOUNTS Set Handling_Charge=(case when " & clsCommon.myLen(fndHandlingCharge.Value) & ">0 then '" + clsCommon.myCstr(fndHandlingCharge.Value) + "' else null end), EMP=(case when " & clsCommon.myLen(fndHandlingCharge.Value) & ">0 then '" + clsCommon.myCstr(fndEMP.Value) + "' else null end),Store_Consumption_Acc=(case when " & clsCommon.myLen(fndStoreConsumptionAcc.Value) & ">0 then '" + clsCommon.myCstr(fndStoreConsumptionAcc.Value) + "' else null end) , Item_Opening_Clearing =(case when " & clsCommon.myLen(fndItemOpeningClearing.Value) & ">0 then '" + clsCommon.myCstr(fndItemOpeningClearing.Value) + "' else null end)   where Purchase_Class_Code='" + fndaccountsetcode.Value + "' ")
            updateQty()
            myMessages.update()

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    'This function is used to delete Data.
    Public Sub fundelete()
        Try
            connectSql.RunSp("purchase_account_delete", New SqlParameter("@acc", fndaccountsetcode.Value))

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rdbtnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnclose.Click
        Me.Close()
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

    Private Sub rdbtnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnsave.Click
        SaveData()
    End Sub
    Sub SaveData()
        If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) AndAlso fndaccountsetcode.Value = "" Then
            myMessages.blankValue("Account Set Code")
            fndaccountsetcode.Focus()
        ElseIf fndadjustmentwriteoff.Value = "" Then
            myMessages.blankValue("Adjustment Write Off")
            fndadjustmentwriteoff.Focus()
        ElseIf (fndassamblycostoff.Value = "") Then
            myMessages.blankValue("FG Shortage Account")
            fndassamblycostoff.Focus()
        ElseIf (fndcreditdebitnoteclr.Value = "") Then
            myMessages.blankValue("Credit Debit Note Clr")
            fndcreditdebitnoteclr.Focus()
        ElseIf (fnddisassamblyexpense.Value = "") Then
            myMessages.blankValue("Disassmbly Expencese")
            fnddisassamblyexpense.Focus()
        ElseIf (fndInventoryControl.Value = "") Then
            myMessages.blankValue("Inventory Control")
            fndInventoryControl.Focus()
        ElseIf (fndnonstockclearing.Value = "") Then
            myMessages.blankValue("Inventory Control Empties")
            fndnonstockclearing.Focus()
        ElseIf (fndpayableclearing.Value = "") Then
            myMessages.blankValue("Payable Clearing")
            fndpayableclearing.Focus()
        ElseIf (fndphysicalinventrycontrol.Value = "") Then
            myMessages.blankValue("Physical Inventory Control")
            fndphysicalinventrycontrol.Focus()
        ElseIf (fndshipmentclearing.Value = "") Then
            myMessages.blankValue("Shipment Clearing")
            fndshipmentclearing.Focus()
        ElseIf (fndtransferclearing.Value = "") Then
            myMessages.blankValue("Transfer Clearing")
            fndtransferclearing.Focus()
        ElseIf (fndbreakageglaccount.Value = "") Then
            myMessages.blankValue("Breakage GL Account")
            fndbreakageglaccount.Focus()
            '' Anubhooti 8-July-2014
            'ElseIf (fndWIPAcc.Value = "") Then
            '    myMessages.blankValue("WIP Account")
            '    fndWIPAcc.Focus()
            'ElseIf (fndRMCons.Value = "") Then
            '    myMessages.blankValue("RM Consumption")
            '    fndRMCons.Focus()
            'ElseIf (fndOther1.Value = "") Then
            '    myMessages.blankValue("Other 1")
            '    fndOther1.Focus()
            'ElseIf (fndOther2.Value = "") Then
            '    myMessages.blankValue("Other 2")
            '    fndOther2.Focus()
            ''
        ElseIf (txtPurchaseCTRL_Ac.Value = "") AndAlso txtPurchaseCTRL_Ac.Visible Then
            myMessages.blankValue("Purchase Control A/c")
            txtPurchaseCTRL_Ac.Focus()
        ElseIf (FndJobWork.Value = "") AndAlso LblJobwork.Visible Then
            myMessages.blankValue("JobWork A/c")
            FndJobWork.Focus()
        ElseIf CheckControlAccount("Inventory Control", fndInventoryControl.Value) = False Then
            fndInventoryControl.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Payables Clearing", fndpayableclearing.Value) = False Then
            fndpayableclearing.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Adjustment Write-Off", fndadjustmentwriteoff.Value) = False Then
            fndadjustmentwriteoff.Focus()
            Exit Sub
        ElseIf CheckControlAccount("FG Shortage Account", fndassamblycostoff.Value) = False Then
            fndassamblycostoff.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Inv. Control Empties", fndnonstockclearing.Value) = False Then
            fndnonstockclearing.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Transfer Clearing", fndtransferclearing.Value) = False Then
            fndtransferclearing.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Shipment Clearing", fndshipmentclearing.Value) = False Then
            fndshipmentclearing.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Disassembly Expense", fnddisassamblyexpense.Value) = False Then
            fnddisassamblyexpense.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Physical Inventory Adj-", fndphysicalinventrycontrol.Value) = False Then
            fndphysicalinventrycontrol.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Credit/Debit Note Clr.", fndcreditdebitnoteclr.Value) = False Then
            fndcreditdebitnoteclr.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Stock Transfer A/C", FndStockTransferAccount.Value) = False Then
            FndStockTransferAccount.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Provisional Clearing", txtProvisionClearing.Value) = False Then
            txtProvisionClearing.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Purchase Job Work", txtPurchaseJobWork.Value) = False Then
            txtPurchaseJobWork.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Difference Account", txtDifferenceAccount.Value) = False Then
            txtDifferenceAccount.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Handling Charge", fndHandlingCharge.Value) = False Then
            fndHandlingCharge.Focus()
            Exit Sub
        ElseIf CheckControlAccount("EMP", fndEMP.Value) = False Then
            fndEMP.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Reserve Stock", fndReserveStock.Value) = False Then
            fndReserveStock.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Breakage GL A/C", fndbreakageglaccount.Value) = False Then
            fndbreakageglaccount.Focus()
            Exit Sub
        ElseIf CheckControlAccount("WIP Account", fndWIPAcc.Value) = False Then
            fndWIPAcc.Focus()
            Exit Sub
        ElseIf CheckControlAccount("RM Consumption", fndRMCons.Value) = False Then
            fndRMCons.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Rejected", fndOther1.Value) = False Then
            fndOther1.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Shortage", fndOther2.Value) = False Then
            fndOther2.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Loss A/C", fndLossAc.Value) = False Then
            fndLossAc.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Transfer Profit/Loss A/c", fndTransferGainLoss.Value) = False Then
            fndTransferGainLoss.Focus()
            Exit Sub
        ElseIf CheckControlAccount("JobWork Account", FndJobWork.Value) = False Then
            FndJobWork.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Stock Transfer In", txtStockTransferIn.Value) = False Then
            txtStockTransferIn.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Chilling Charges", txtChiilingCharges.Value) = False Then
            txtChiilingCharges.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Freight Charges", txtFreightCharges.Value) = False Then
            txtFreightCharges.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Stock Transfer Job Work", txtStockTransferJobWork.Value) = False Then
            txtStockTransferJobWork.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Store Consumption A/c", fndStoreConsumptionAcc.Value) = False Then
            fndStoreConsumptionAcc.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Purchase Control Account", txtPurchaseCTRL_Ac.Value) = False Then
            txtPurchaseCTRL_Ac.Focus()
            Exit Sub
        ElseIf CheckControlAccount("FA Account", fndFAAccount.Value) = False Then
            fndFAAccount.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Purchase Loss", fndPurchaseLoss.Value) = False Then
            fndPurchaseLoss.Focus()
            Exit Sub

        Else

            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.itemPurchaseAccount, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return
                End If
            End If
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, fndaccountsetcode.Value, "TSPL_PURCHASE_ACCOUNTS", "Purchase_Class_Code", Nothing)
            If rdbtnsave.Text = "Save" Then
                funinsert()
                funfill()

            ElseIf (rdbtnsave.Text = "Update") Then
                funupdate()
            End If
        End If
    End Sub

    Private Sub updateQty()
        Dim coll As New Hashtable()
        clsCommon.AddColumnsForChange(coll, "Costing_Method", clsCommon.myCdbl(cboCostingMethod.SelectedValue))
        If clsCommon.myLen(fndLossAc.Value) > 0 Then
            clsCommon.AddColumnsForChange(coll, "Loss_Ac", clsCommon.myCstr(fndLossAc.Value))
        End If
        If clsCommon.myLen(txtPurchaseCTRL_Ac.Value) > 0 AndAlso txtPurchaseCTRL_Ac.Visible Then
            clsCommon.AddColumnsForChange(coll, "Purchase_Control_Account", clsCommon.myCstr(txtPurchaseCTRL_Ac.Value))
        End If
        If clsCommon.myLen(fndTransferGainLoss.Value) > 0 Then
            clsCommon.AddColumnsForChange(coll, "Transfer_Gain_Loss_Ac", clsCommon.myCstr(fndTransferGainLoss.Value))
        End If
        If clsCommon.myLen(FndJobWork.Value) > 0 Then
            clsCommon.AddColumnsForChange(coll, "Job_Work_Ac", clsCommon.myCstr(FndJobWork.Value))
        End If
        clsCommon.AddColumnsForChange(coll, "Stock_Transfer_In", txtStockTransferIn.Value, True)
        clsCommon.AddColumnsForChange(coll, "Stock_Transfer_Acc", FndStockTransferAccount.Value, True)
        clsCommon.AddColumnsForChange(coll, "Provision_Clearing", txtProvisionClearing.Value, True)
        clsCommon.AddColumnsForChange(coll, "Chilling_Charges", txtChiilingCharges.Value, True)
        clsCommon.AddColumnsForChange(coll, "Freight_Charges", txtFreightCharges.Value, True)
        clsCommon.AddColumnsForChange(coll, "Purchase_JobWork", txtPurchaseJobWork.Value, True)
        clsCommon.AddColumnsForChange(coll, "Difference_Account", txtDifferenceAccount.Value, True)
        clsCommon.AddColumnsForChange(coll, "Stock_Transfer_JobWork", txtStockTransferJobWork.Value, True)
        clsCommon.AddColumnsForChange(coll, "FA_CLEARING_AC", fndFAAccount.Value, True)
        clsCommon.AddColumnsForChange(coll, "Purchase_Loss", fndPurchaseLoss.Value, True)

        '' agaist ticket no.BHA/08/08/18-000396 on 08/08/2018
        clsCommon.AddColumnsForChange(coll, "Wrekage_Account", fndWrekageAccount.Value, True)


        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PURCHASE_ACCOUNTS", OMInsertOrUpdate.Update, "Purchase_Class_Code='" + fndaccountsetcode.Value + "'")
    End Sub

    Private Sub rdbtndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtndelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If myMessages.deleteConfirm() Then
            fundelete()
            rdbtnsave.Text = "Save"
            rdbtndelete.Enabled = False
        End If


    End Sub
    Private Sub rdbtnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnnew.Click
        funreset()
    End Sub

    Private Sub fndaccountsetcode_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fndaccountsetcode.Value <> "" Then
            rdtxtdescription.Enabled = True
            rdtxtdescription.ReadOnly = False
            fndshipmentclearing.Enabled = True
            fndInventoryControl.Enabled = True
            fndpayableclearing.Enabled = True
            fndadjustmentwriteoff.Enabled = True
            fndassamblycostoff.Enabled = True
            fndnonstockclearing.Enabled = True
            fndtransferclearing.Enabled = True
            fnddisassamblyexpense.Enabled = True
            fndphysicalinventrycontrol.Enabled = True
            fndcreditdebitnoteclr.Enabled = True
            fndLossAc.Enabled = True
            fndTransferGainLoss.Enabled = True

            fndWIPAcc.Enabled = True
            fndRMCons.Enabled = True
            fndOther1.Enabled = True
            fndOther2.Enabled = True

            rdtxtadjustmentwriteoff.ReadOnly = True
            rdtxtassamblycostcredit.ReadOnly = True
            rdtxtcreditdebitnoteclr.ReadOnly = True
            rdtxtdisassamblyexpense.ReadOnly = True
            rdtxtinventrycontrol.ReadOnly = True
            rdtxtnonstockclearing.ReadOnly = True
            rdtxtpayableclearing.ReadOnly = True
            rdtxtshipmentexpense.ReadOnly = True
            rdtxttransferclearing.ReadOnly = True
            rdtxtphysicalinventryadj.ReadOnly = True
            txtPurchaseCTRL_Ac.Value = ""
            txtPurchaseCtrlAcDesc.Text = ""

            FndJobWork.Value = ""
            LblJobwork.Text = ""


            txtLossAc.ReadOnly = True
            txtTransferGainLossDesc.ReadOnly = True
            '' Anubhooti 8-July-2014
            rdtxtWIPAcc.ReadOnly = True
            rdtxtRMCons.ReadOnly = True
            rdtxtOther1.ReadOnly = True
            rdtxtOther2.ReadOnly = True

            rdbtnsave.Enabled = True
            rdbtnclose.Enabled = True
            'rdbtndelete.Enabled = False

        End If
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If

    End Sub

    'Private Sub fndaccountsetcode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fndaccountsetcode.Query = "select purchase_class_code as [Purchase Account Code],purchase_class_desc as [Purchase Account Desc]from tspl_purchase_accounts"
    '    fndaccountsetcode.ConnectionString = connectSql.SqlCon()
    '    fndaccountsetcode.Caption = "Purchase Accounts"
    '    fndaccountsetcode.ValueToSelect = "Purchase Account Code"
    '    fndaccountsetcode.ValueToSelect1 = "Purchase Account Desc"
    'End Sub
    'This is function is used to retrive data in all controls.
    Public Sub funfill()
        Try
            Dim query As String = "Select purchase_class_code,purchase_class_desc,inv_control_account,inv_payable_clearing,adjustment_account,assembly_cost_credit,non_stock_clearing,transfer_clearing,shipment_clearing,disassembly_expense,physical_inv_adjustment,credit_debit_note_clearing,Reserve_Stock, Breakage_Gl_Account,Costing_Method,WIP_Account,RM_Consumption ,Other_1,Other_2,Loss_Ac, Purchase_Control_Account,Transfer_Gain_Loss_Ac,Job_Work_Ac,Stock_Transfer_In,Stock_Transfer_Acc,Provision_Clearing,Chilling_Charges,Freight_Charges,Is_IndentRequired,Purchase_JobWork,Stock_Transfer_JobWork,Difference_Account,Handling_Charge,EMP,Store_Consumption_Acc,FA_CLEARING_AC,Purchase_Loss,Wrekage_Account, Item_Opening_Clearing from tspl_purchase_accounts where purchase_class_code='" + fndaccountsetcode.Value + "'"
            Dim adp As New SqlDataAdapter(query, connectSql.SqlCon())
            Dim dt As New DataTable()
            adp.Fill(dt)
            Dim dr As DataRow = dt.Rows(0)
            fndaccountsetcode.Value = dr(0).ToString()
            lblaccountsetdesc.Text = dr(1).ToString()
            fndInventoryControl.Value = dr(2).ToString()
            inventorycontrol_textchanged(Nothing, Nothing)
            fndpayableclearing.Value = dr(3).ToString()
            paybleclearing_textchanged(Nothing, Nothing)
            fndadjustmentwriteoff.Value = dr(4).ToString()
            addjustment_textchanged(Nothing, Nothing)
            fndassamblycostoff.Value = dr(5).ToString()
            assemblycostoff_textchanged(Nothing, Nothing)
            fndnonstockclearing.Value = dr(6).ToString()
            nonstockclearing_textchanged(Nothing, Nothing)
            fndtransferclearing.Value = dr(7).ToString()
            transferclearing_textchanged(Nothing, Nothing)
            fndshipmentclearing.Value = dr(8).ToString()
            shipmentclearing_textchanged(Nothing, Nothing)
            fnddisassamblyexpense.Value = dr(9).ToString()
            disassemblyexpense_textchanged(Nothing, Nothing)
            fndphysicalinventrycontrol.Value = dr(10).ToString()
            physicalinventoryadjustment_textchanged(Nothing, Nothing)
            fndcreditdebitnoteclr.Value = dr(11).ToString()
            creditdebit_textchanged(Nothing, Nothing)
            fndReserveStock.Value = dr(12).ToString()
            txtReserveStock.Text = clsGLAccount.GetName(fndReserveStock.Value)
            fndbreakageglaccount.Value = Convert.ToString(dr(13))
            txtbreakage.Text = clsGLAccount.GetName(fndbreakageglaccount.Value)
            fndLossAc.Value = dr("Loss_Ac").ToString()
            txtLossAc.Text = clsCommon.myCstr(clsGLAccount.GetName(fndLossAc.Value))
            '' Anubhooti 08-July-2014 (BM00000003088)
            fndWIPAcc.Value = dr(15).ToString()
            WIPAccount_textchanged(Nothing, Nothing)
            fndRMCons.Value = dr(16).ToString()
            RMConsumption_textchanged(Nothing, Nothing)
            fndOther1.Value = dr(17).ToString()
            Other1_textchanged(Nothing, Nothing)
            fndOther2.Value = dr(18).ToString()
            Other2_textchanged(Nothing, Nothing)
            ''
            cboCostingMethod.SelectedValue = dr("Costing_Method")
            txtPurchaseCTRL_Ac.Value = clsCommon.myCstr(dr("Purchase_Control_Account"))
            txtPurchaseCtrlAcDesc.Text = FillAccountDesc(txtPurchaseCTRL_Ac.Value)
            fndTransferGainLoss.Value = clsCommon.myCstr(dr("Transfer_Gain_Loss_Ac"))
            txtTransferGainLossDesc.Text = FillAccountDesc(fndTransferGainLoss.Value)
            FndJobWork.Value = clsCommon.myCstr(dr("Job_Work_Ac"))
            LblJobwork.Text = FillAccountDesc(FndJobWork.Value)
            txtStockTransferIn.Value = clsCommon.myCstr(dr("Stock_Transfer_In"))
            lblStockTransferIn.Text = FillAccountDesc(txtStockTransferIn.Value)
            FndStockTransferAccount.Value = clsCommon.myCstr(dr("Stock_Transfer_Acc"))
            TxtStockTransferAccount.Text = FillAccountDesc(FndStockTransferAccount.Value)

            txtProvisionClearing.Value = clsCommon.myCstr(dr("Provision_Clearing"))
            lblProvisioinClearing.Text = FillAccountDesc(txtProvisionClearing.Value)

            txtChiilingCharges.Value = clsCommon.myCstr(dr("Chilling_Charges"))
            lblChiilingCharges.Text = FillAccountDesc(txtChiilingCharges.Value)

            txtFreightCharges.Value = clsCommon.myCstr(dr("Freight_Charges"))
            lblFreightCharges.Text = FillAccountDesc(txtFreightCharges.Value)

            txtPurchaseJobWork.Value = clsCommon.myCstr(dr("Purchase_JobWork"))
            lblPurchaseJobwork.Text = FillAccountDesc(txtPurchaseJobWork.Value)

            txtStockTransferJobWork.Value = clsCommon.myCstr(dr("Stock_Transfer_JobWork"))
            lblStockTransferJobWorkDesc.Text = FillAccountDesc(txtStockTransferJobWork.Value)

            txtDifferenceAccount.Value = clsCommon.myCstr(dr("Difference_Account"))
            lblDifferenceAccount.Text = FillAccountDesc(txtDifferenceAccount.Value)

            fndHandlingCharge.Value = clsCommon.myCstr(dr("Handling_Charge"))
            txtHandlingCharge.Text = FillAccountDesc(fndHandlingCharge.Value)

            fndEMP.Value = clsCommon.myCstr(dr("EMP"))
            txtEMP.Text = FillAccountDesc(fndEMP.Value)

            fndStoreConsumptionAcc.Value = clsCommon.myCstr(dr("Store_Consumption_Acc"))
            txtStoreConsumtion.Text = FillAccountDesc(fndStoreConsumptionAcc.Value)

            chk_indentrequired.Checked = IIf(clsCommon.myCstr(dr("Is_IndentRequired").ToString()) = "1", True, False)
            fndFAAccount.Value = clsCommon.myCstr(dr("FA_CLEARING_AC"))
            lblFaAccountDes.Text = FillAccountDesc(fndFAAccount.Value)

            fndPurchaseLoss.Value = clsCommon.myCstr(dr("Purchase_Loss"))
            txtPurchaseLoss.Text = FillAccountDesc(fndPurchaseLoss.Value)

            fndWrekageAccount.Value = clsCommon.myCstr(dr("Wrekage_Account"))
            txtWrekageAccount.Text = FillAccountDesc(fndWrekageAccount.Value)
            'ticket No : TEC/02/11/18-000359 By Prabhakar
            fndItemOpeningClearing.Value = clsCommon.myCstr(dr("Item_Opening_Clearing"))
            lblItemOpeningClearing.Text = FillAccountDesc(fndItemOpeningClearing.Value)

            rdbtnsave.Text = "Update"
            rdbtnsave.Enabled = True
            rdbtndelete.Enabled = True
            'If userCode <> "ADMIN" Then
            '    If funSetUserAccess() = False Then Exit Sub
            'End If

        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Public Sub text_changed(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim var As String = "Select purchase_class_code from tspl_purchase_accounts where purchase_class_code ='" + fndaccountsetcode.Value + "'"
        var = clsDBFuncationality.getSingleValue(var)
        'Dim var As String
        'While (dr.Read())
        '    var = dr(0).ToString()
        'End While
        If var <> "" Then
            funfill()
            rdbtnsave.Text = "Update"
            rdtxtdescription.Enabled = True
            rdtxtdescription.ReadOnly = False
            fndshipmentclearing.Enabled = True
            fndInventoryControl.Enabled = True
            fndpayableclearing.Enabled = True
            fndadjustmentwriteoff.Enabled = True
            fndassamblycostoff.Enabled = True
            fndnonstockclearing.Enabled = True
            fndtransferclearing.Enabled = True
            fnddisassamblyexpense.Enabled = True
            fndphysicalinventrycontrol.Enabled = True
            fndcreditdebitnoteclr.Enabled = True
            fndLossAc.Enabled = True
            fndTransferGainLoss.Enabled = True
            txtTransferGainLossDesc.ReadOnly = True
            '' Anubhooti 08-July-2014 (BM00000003088)
            fndWIPAcc.Enabled = True
            fndRMCons.Enabled = True
            fndOther1.Enabled = True
            fndOther2.Enabled = True

            rdbtnsave.Enabled = True
            rdbtndelete.Enabled = True
        Else
            rdbtnsave.Text = "Save"
            rdtxtdescription.Text = ""
            rdtxtdescription.ReadOnly = False
            fndshipmentclearing.Text = ""
            fndInventoryControl.Text = ""
            fndpayableclearing.Text = ""
            fndadjustmentwriteoff.Text = ""
            fndassamblycostoff.Text = ""
            fndnonstockclearing.Text = ""
            fndtransferclearing.Text = ""
            fnddisassamblyexpense.Text = ""
            fndphysicalinventrycontrol.Text = ""
            fndcreditdebitnoteclr.Text = ""
            fndReserveStock.Text = ""
            fndbreakageglaccount.Value = ""
            fndLossAc.Value = ""
            fndTransferGainLoss.Value = ""
            txtTransferGainLossDesc.Text = ""
            txtPurchaseCTRL_Ac.Value = ""
            txtPurchaseCtrlAcDesc.Text = ""
            FndJobWork.Value = ""
            LblJobwork.Text = ""
            '' Anubhooti 08-July-2014 (BM00000003088)
            fndWIPAcc.Text = ""
            fndRMCons.Text = ""
            fndOther1.Text = ""
            fndOther2.Text = ""

            rdbtndelete.Enabled = False
            rdbtnclose.Enabled = True

        End If
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub
    'This function is used to reset all controls.
    Public Sub funreset()
        fndPurchaseLoss.Value = ""
        txtPurchaseLoss.Text = ""
        fndFAAccount.Value = ""
        lblFaAccountDes.Text = ""
        fndHandlingCharge.Value = ""
        txtHandlingCharge.Text = ""
        fndEMP.Value = ""
        txtEMP.Text = ""
        fndStoreConsumptionAcc.Value = ""
        txtStoreConsumtion.Text = ""
        chk_indentrequired.Checked = False
        lblLossAc.Text = "Gain/Loss A/C"
        fndaccountsetcode.Value = ""
        lblaccountsetdesc.Text = ""
        fndaccountsetcode.MyReadOnly = False
        fndaccountsetcode.Enabled = True
        rdtxtdescription.Enabled = True
        rdtxtdescription.Text = ""
        fndshipmentclearing.Enabled = True
        fndshipmentclearing.Value = ""
        rdtxtshipmentexpense.Text = ""
        fndInventoryControl.Enabled = True
        fndInventoryControl.Value = ""
        rdtxtinventrycontrol.Text = ""
        fndpayableclearing.Enabled = True
        fndpayableclearing.Value = ""
        rdtxtpayableclearing.Text = ""
        fndadjustmentwriteoff.Enabled = True
        fndadjustmentwriteoff.Value = ""
        rdtxtadjustmentwriteoff.Text = ""
        fndassamblycostoff.Enabled = True
        fndassamblycostoff.Value = ""
        rdtxtassamblycostcredit.Text = ""
        fndnonstockclearing.Enabled = True
        fndnonstockclearing.Value = ""
        rdtxtnonstockclearing.Text = ""
        fndtransferclearing.Enabled = True
        fndtransferclearing.Value = ""
        rdtxttransferclearing.Text = ""
        fnddisassamblyexpense.Enabled = True
        fnddisassamblyexpense.Value = ""
        rdtxtassamblycostcredit.Text = ""
        fndphysicalinventrycontrol.Enabled = True
        fndphysicalinventrycontrol.Value = ""
        rdtxtphysicalinventryadj.Text = ""
        fndcreditdebitnoteclr.Enabled = True
        fndcreditdebitnoteclr.Value = ""
        rdtxtcreditdebitnoteclr.Text = ""
        rdtxtadjustmentwriteoff.ReadOnly = True
        rdtxtassamblycostcredit.ReadOnly = True
        rdtxtcreditdebitnoteclr.ReadOnly = True
        rdtxtdescription.ReadOnly = True
        rdtxtdisassamblyexpense.ReadOnly = True
        rdtxtinventrycontrol.ReadOnly = True
        rdtxtnonstockclearing.ReadOnly = True
        rdtxtpayableclearing.ReadOnly = True
        rdtxtshipmentexpense.ReadOnly = True
        rdtxttransferclearing.ReadOnly = True
        rdtxtphysicalinventryadj.ReadOnly = True
        fndWrekageAccount.Value = ""
        txtWrekageAccount.Text = ""
        fndLossAc.Enabled = True
        fndLossAc.Value = ""
        txtLossAc.Text = ""
        txtLossAc.ReadOnly = True

        fndTransferGainLoss.Enabled = True
        fndTransferGainLoss.Value = ""
        txtTransferGainLossDesc.Text = ""
        txtTransferGainLossDesc.ReadOnly = True

        txtPurchaseCTRL_Ac.Value = ""
        txtPurchaseCtrlAcDesc.Text = ""
        FndJobWork.Value = ""
        LblJobwork.Text = ""
        '' Anubhooti 08-July-2014 (BM00000003088)
        fndWIPAcc.Value = ""
        rdtxtWIPAcc.Text = ""
        fndRMCons.Value = ""
        rdtxtRMCons.Text = ""
        fndOther1.Value = ""
        rdtxtOther1.Text = ""
        fndOther2.Value = ""
        rdtxtOther2.Text = ""
        rdtxtWIPAcc.ReadOnly = True
        rdtxtRMCons.ReadOnly = True
        rdtxtOther1.ReadOnly = True
        rdtxtOther2.ReadOnly = True

        fndReserveStock.Value = ""
        txtReserveStock.Text = ""
        rdbtnsave.Enabled = True
        rdbtnsave.Text = "Save"
        rdbtndelete.Enabled = False
        fndbreakageglaccount.Value = ""
        fndReserveStock.Value = ""
        txtReserveStock.Text = ""
        fndbreakageglaccount.Value = ""
        txtbreakage.Text = ""
        rdtxtdisassamblyexpense.Text = ""
        cboCostingMethod.SelectedValue = EnableCostingMethod
        txtProvisionClearing.Value = ""
        lblProvisioinClearing.Text = ""

        txtChiilingCharges.Value = ""
        lblChiilingCharges.Text = ""

        txtFreightCharges.Value = ""
        lblFreightCharges.Text = ""

        FndStockTransferAccount.Value = ""
        TxtStockTransferAccount.Text = ""
        txtStockTransferIn.Value = ""
        lblStockTransferIn.Text = ""
        txtPurchaseJobWork.Value = ""
        lblPurchaseJobwork.Text = ""
        txtStockTransferJobWork.Value = ""
        lblStockTransferJobWorkDesc.Text = ""

        txtDifferenceAccount.Value = ""
        lblDifferenceAccount.Text = ""
        fndItemOpeningClearing.Value = Nothing
        lblItemOpeningClearing.Text = ""
    End Sub

    Private Sub fndaccountsetcode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Chr(39) Then
            e.Handled = True
        End If
    End Sub


    Private Sub fndinventrycontrol_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Chr(39) Then
            e.Handled = True
        End If
    End Sub

    Private Sub fndpayableclearing_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Chr(39) Then
            e.Handled = True
        End If
    End Sub

    Private Sub fndadjustmentwriteoff_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Chr(39) Then
            e.Handled = True
        End If
    End Sub

    Private Sub fndassamblycostoff_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Chr(39) Then
            e.Handled = True
        End If
    End Sub

    Private Sub fndnonstockclearing_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Chr(39) Then
            e.Handled = True
        End If
    End Sub

    Private Sub fndtransferclearing_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Chr(39) Then
            e.Handled = True
        End If
    End Sub


    Private Sub fndshipmentclearing_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Chr(39) Then
            e.Handled = True
        End If
    End Sub

    Private Sub fnddisassamblyexpense_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Chr(39) Then
            e.Handled = True
        End If
    End Sub

    Private Sub fndphysicalinventrycontrol_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Chr(39) Then
            e.Handled = True
        End If
    End Sub

    Private Sub fndcreditdebitnoteclr_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Chr(39) Then
            e.Handled = True
        End If
    End Sub
    '' Anubhooti 08-July-2014 (BM00000003088)
    Private Sub fndWIPAcc_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles fndWIPAcc.KeyPress
        If e.KeyChar = Chr(39) Then
            e.Handled = True
        End If
    End Sub

    Private Sub fndTransferGainLoss_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles fndTransferGainLoss.KeyPress
        If e.KeyChar = Chr(39) Then
            e.Handled = True
        End If
    End Sub

    Private Sub fndRMCons_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles fndRMCons.KeyPress
        If e.KeyChar = Chr(39) Then
            e.Handled = True
        End If
    End Sub
    Private Sub fndOther1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles fndOther1.KeyPress
        If e.KeyChar = Chr(39) Then
            e.Handled = True
        End If
    End Sub
    Private Sub fndOther2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles fndOther2.KeyPress
        If e.KeyChar = Chr(39) Then
            e.Handled = True
        End If
    End Sub

    Private Sub fndLossAc__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndLossAc._MYValidating
        Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
        fndLossAc.Value = clsCommon.ShowSelectForm("fndLossAc", Qry, "Account_Code", " ControlAccount ='Y' ", fndLossAc.Value, "Account_Code", isButtonClicked)
        txtLossAc.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + fndLossAc.Value + "' ")
    End Sub

    Private Sub fndTransferGainLoss__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndTransferGainLoss._MYValidating
        Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
        fndTransferGainLoss.Value = clsCommon.ShowSelectForm("fndTransferGainLossAc", Qry, "Account_Code", " ControlAccount ='Y' ", fndTransferGainLoss.Value, "Account_Code", isButtonClicked)
        txtTransferGainLossDesc.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + fndTransferGainLoss.Value + "' ")
    End Sub
    ''
    Private Sub fndLossAc_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles fndLossAc.KeyPress
        If e.KeyChar = Chr(39) Then
            e.Handled = True
        End If
    End Sub
    Private Sub rdmenuexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdmenuexit.Click
        Me.Close()
    End Sub

    Private Sub rdmenuexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdmenuexport.Click
        Dim query As String = "select purchase_class_code as 'Purchase Class Code',purchase_class_desc as 'Purchase Class Description',inv_control_account as 'Inventory Control Account',Inv_payable_clearing as 'Inventory Payable Clearing',Adjustment_Account as 'Adjustment Account',assembly_cost_credit as 'Assembly Cost Credit',non_stock_clearing as 'Non Stock Clearing',transfer_clearing as 'Transfer Clearing',shipment_clearing as 'Shipment Clearing',Disassembly_expense as 'Disassembly Expense',physical_inv_adjustment as 'Physical Inventory Adjustment',credit_debit_note_clearing as 'Credit Debit Note Clearing',Reserve_Stock as 'Reserve Stock',Breakage_Gl_Account as [Breakage GL Account],WIP_Account As [WIP Account],RM_Consumption As [RM Consumption],Other_1 As [Other 1],Other_2 As [Other 2],case when Costing_Method=1 then 'Averege' else case when Costing_Method=2 then 'FIFO' else case when Costing_Method=3 then 'LIFO' else 'NA' end end end as 'Costing Method',Loss_Ac,Transfer_Gain_Loss_Ac,job_work_ac as [Job Work Account],Provision_Clearing as [Provision Clearing],Chilling_Charges,Freight_Charges,Purchase_JobWork,Stock_Transfer_JobWork as [Stock Transfer JobWork],(case when Is_IndentRequired='1' then 'Y' else 'N' end) as 'Is Indent Required',Difference_Account as [Difference Account],Handling_Charge as [Handling Charge],EMP,Store_Consumption_Acc as [Store Consumption Acc],FA_CLEARING_AC AS [FA Clearing Account],Purchase_Loss as [Purchase Loss],Wrekage_Account as [Wrekage Account] , Item_Opening_Clearing as [Item Opening Clearing] from tspl_purchase_accounts"
        ListImpExpColumnsMandatory = New List(Of String)({"Purchase Class Code"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"Purchase Class Code"})
        transportSql.ExporttoExcel(query, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
    End Sub

    Private Sub radmenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuimport.Click
        Dim dgv As New RadGridView

        Me.Controls.Add(dgv)
        If transportSql.importExcel(dgv, "Purchase Class Code", "Purchase Class Description", "Inventory Control Account", "Inventory Payable Clearing", "Adjustment Account", "Assembly Cost Credit", "Non Stock Clearing", "Transfer Clearing", "Shipment Clearing", "Disassembly Expense", "Physical Inventory Adjustment", "Credit Debit Note Clearing", "Breakage GL Account", "WIP Account", "RM Consumption", "Other 1", "Other 2", "Costing Method", "Loss_Ac", "Transfer_Gain_Loss_Ac", "Job Work Account", "Provision Clearing", "Chilling_Charges", "Freight_Charges", "Purchase_JobWork", "Stock Transfer JobWork", "Is Indent Required", "Difference Account", "Handling Charge", "EMP", "Store Consumption Acc", "FA Clearing Account", "Purchase Loss", "Wrekage Account", "Item Opening Clearing") Then
            Dim linno As Integer = 0
            Dim trans As SqlTransaction = Nothing
            Try
                connectSql.OpenConnection()
                trans = connectSql.OpenConnection.BeginTransaction()
                clsCommon.ProgressBarShow()

                For Each dgrv As GridViewRowInfo In dgv.Rows
                    linno += 1
                    Dim ISIndentReq As Integer = 0
                    Dim strpurchaseclasscode As String = clsCommon.myCstr(dgrv.Cells(0).Value)
                    Dim strpurchaseclassdescription As String = clsCommon.myCstr(dgrv.Cells(1).Value)
                    Dim strinventorycontrolaccount As String = clsCommon.myCstr(dgrv.Cells(2).Value)
                    Dim IsIndentRequired As String = clsCommon.myCstr(dgrv.Cells("Is Indent Required").Value)
                    '' Anubhooti 06-Nov-2014
                    If clsCommon.myLen(strinventorycontrolaccount) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strinventorycontrolaccount + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled inventory control account (" & strinventorycontrolaccount & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strinventorycontrolaccount + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled inventory control account (" & strinventorycontrolaccount & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    ''
                    Dim strinventorypayableclearing As String = clsCommon.myCstr(dgrv.Cells(3).Value)
                    '' Anubhooti 06-Nov-2014
                    If clsCommon.myLen(strinventorypayableclearing) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strinventorypayableclearing + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled inventory payable clearing (" & strinventorypayableclearing & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strinventorypayableclearing + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled inventory payable clearing (" & strinventorypayableclearing & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    ''
                    Dim stradjustaccount As String = clsCommon.myCstr(dgrv.Cells(4).Value)
                    '' Anubhooti 06-Nov-2014
                    If clsCommon.myLen(stradjustaccount) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + stradjustaccount + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled adjustment account (" & stradjustaccount & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + stradjustaccount + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled adjustment account (" & stradjustaccount & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    ''
                    Dim strassemblycostcredit As String = clsCommon.myCstr(dgrv.Cells(5).Value)
                    '' Anubhooti 06-Nov-2014
                    If clsCommon.myLen(strassemblycostcredit) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strassemblycostcredit + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled assembly cost credit (" & strassemblycostcredit & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strassemblycostcredit + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled assembly cost credit (" & strassemblycostcredit & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    ''
                    Dim strnonstockclearing As String = clsCommon.myCstr(dgrv.Cells(6).Value)
                    '' Anubhooti 06-Nov-2014
                    If clsCommon.myLen(strnonstockclearing) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strnonstockclearing + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled non stock clearing (" & strnonstockclearing & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strnonstockclearing + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled non stock clearing (" & strnonstockclearing & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    ''
                    Dim strtransferclearing As String = clsCommon.myCstr(dgrv.Cells(7).Value)
                    '' Anubhooti 06-Nov-2014
                    If clsCommon.myLen(strtransferclearing) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strtransferclearing + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled transfer clearing (" & strtransferclearing & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strtransferclearing + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled transfer clearing (" & strtransferclearing & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    ''
                    Dim strshipmentclearing As String = clsCommon.myCstr(dgrv.Cells(8).Value)
                    '' Anubhooti 06-Nov-2014
                    If clsCommon.myLen(strshipmentclearing) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strshipmentclearing + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled shipment clearing (" & strshipmentclearing & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strshipmentclearing + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled shipment clearing (" & strshipmentclearing & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    ''
                    Dim strdisassemblyexpense As String = clsCommon.myCstr(dgrv.Cells(9).Value)
                    '' Anubhooti 06-Nov-2014
                    If clsCommon.myLen(strdisassemblyexpense) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strdisassemblyexpense + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled disassembly expense (" & strdisassemblyexpense & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strdisassemblyexpense + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled disassembly expense (" & strdisassemblyexpense & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    ''
                    Dim strphysicalinventoryadjustment As String = clsCommon.myCstr(dgrv.Cells(10).Value)
                    '' Anubhooti 06-Nov-2014
                    If clsCommon.myLen(strphysicalinventoryadjustment) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strphysicalinventoryadjustment + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled physical inventory adjustment (" & strphysicalinventoryadjustment & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strphysicalinventoryadjustment + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled physical inventory adjustment (" & strphysicalinventoryadjustment & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    ''
                    Dim strcreditdebitclearing As String = clsCommon.myCstr(dgrv.Cells(11).Value)
                    '' Anubhooti 06-Nov-2014
                    If clsCommon.myLen(strcreditdebitclearing) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strcreditdebitclearing + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled credit debit note clearing (" & strcreditdebitclearing & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strcreditdebitclearing + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled credit debit note clearing (" & strcreditdebitclearing & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    ''
                    Dim strReserveStock As String = clsCommon.myCstr(dgrv.Cells("Reserve Stock").Value)
                    '' Anubhooti 06-Nov-2014

                    If clsCommon.myLen(strReserveStock) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strReserveStock + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled reserve stock (" & strReserveStock & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strReserveStock + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled reserve stock (" & strReserveStock & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    ''
                    Dim strbreakage As String = clsCommon.myCstr(dgrv.Cells(12).Value)
                    '' Anubhooti 06-Nov-2014
                    If clsCommon.myLen(strbreakage) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strbreakage + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled breakage GL account (" & strbreakage & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strbreakage + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled breakage GL account (" & strbreakage & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    ''
                    Dim strWIPAccount As String = clsCommon.myCstr(dgrv.Cells("WIP Account").Value)
                    '' Anubhooti 06-Nov-2014
                    If clsCommon.myLen(strWIPAccount) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strWIPAccount + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled WIP account (" & strWIPAccount & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strWIPAccount + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled WIP account (" & strWIPAccount & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    ''
                    Dim strRMConsumption As String = clsCommon.myCstr(dgrv.Cells("RM Consumption").Value)
                    '' Anubhooti 06-Nov-2014
                    If clsCommon.myLen(strRMConsumption) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strRMConsumption + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled RM consumption (" & strRMConsumption & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strRMConsumption + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled RM consumption (" & strRMConsumption & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    ''
                    Dim strOther1 As String = clsCommon.myCstr(dgrv.Cells("Other 1").Value)
                    '' Anubhooti 06-Nov-2014
                    If clsCommon.myLen(strOther1) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strOther1 + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled other 1 (" & strOther1 & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strOther1 + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled other 1 (" & strOther1 & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    ''
                    Dim strOther2 As String = clsCommon.myCstr(dgrv.Cells("Other 2").Value)
                    '' Anubhooti 06-Nov-2014
                    If clsCommon.myLen(strOther2) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strOther2 + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled other 2 (" & strOther2 & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strOther2 + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled other 2 (" & strOther2 & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    ''
                    Dim strLossAc As String = clsCommon.myCstr(dgrv.Cells("Loss_Ac").Value)
                    '' Anubhooti 06-Nov-2014
                    If clsCommon.myLen(strLossAc) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strLossAc + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled Loss_Ac (" & strLossAc & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strLossAc + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled Loss_Ac (" & strLossAc & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If



                    Dim strTransferGainLossAc As String = clsCommon.myCstr(dgrv.Cells("Transfer_Gain_Loss_Ac").Value)
                    '' Pankaj jha 01/07/2015
                    If clsCommon.myLen(strTransferGainLossAc) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strTransferGainLossAc + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled Transfer-Gain-Loss-Ac (" & strTransferGainLossAc & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strTransferGainLossAc + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled Transfer-Gain-Loss-Ac (" & strTransferGainLossAc & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If

                    Dim StrJobworlAcc As String = clsCommon.myCstr(dgrv.Cells("Job Work Account").Value)
                    '' Pankaj jha 01/07/2015
                    If clsCommon.myLen(StrJobworlAcc) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + StrJobworlAcc + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled Job Work Account(" & StrJobworlAcc & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + StrJobworlAcc + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled Job Work Account (" & StrJobworlAcc & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If

                    Dim StrProvClearring As String = clsCommon.myCstr(dgrv.Cells("Provision Clearing").Value)
                    If clsCommon.myLen(StrProvClearring) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + StrProvClearring + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled Provision Clearing(" & StrProvClearring & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + StrProvClearring + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled Provision Clearing Account (" & StrProvClearring & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If

                    ''
                    Dim strCostingMethode As String = clsCommon.myCstr(dgrv.Cells("Costing Method").Value)
                    '' Anubhooti 06-Nov-2014
                    ''Balwinder 20-Nov-2014  beacause costing method is not GL Account.
                    If clsCommon.myLen(strCostingMethode) > 0 Then
                        'Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strCostingMethode + "'"
                        'Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        'If check <= 0 Then
                        '    Throw New Exception("Filled costing method (" & strCostingMethode & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        'End If
                        'Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strCostingMethode + "' AND ControlAccount ='Y'"
                        'Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        'If check1 <= 0 Then
                        '    Throw New Exception("Filled costing method (" & strCostingMethode & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        'End If
                        If clsCommon.CompairString(strCostingMethode, "Averege") = CompairStringResult.Equal Then
                            strCostingMethode = "1"
                        ElseIf clsCommon.CompairString(strCostingMethode, "FIFO") = CompairStringResult.Equal Then
                            strCostingMethode = "2"
                        ElseIf clsCommon.CompairString(strCostingMethode, "LIFO") = CompairStringResult.Equal Then
                            strCostingMethode = "3"
                        Else
                            Throw New Exception("Costing Method should be Averege or FIFO or LIFO")
                        End If
                    Else
                        strCostingMethode = "0"
                    End If



                    Dim StrChillingClearring As String = clsCommon.myCstr(dgrv.Cells("Chilling_Charges").Value)
                    If clsCommon.myLen(StrChillingClearring) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + StrChillingClearring + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled Chilling Charges(" & StrChillingClearring & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + StrChillingClearring + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled Chilling Charges Account (" & StrChillingClearring & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    Else
                        StrChillingClearring = Nothing
                    End If

                    Dim StrFreightCharges As String = clsCommon.myCstr(dgrv.Cells("Freight_Charges").Value)
                    If clsCommon.myLen(StrFreightCharges) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + StrFreightCharges + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled Freight Charges(" & StrFreightCharges & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + StrFreightCharges + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled Freight Charges Account (" & StrFreightCharges & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    Dim strPurchaseJobWork As String = clsCommon.myCstr(dgrv.Cells("Purchase_JobWork").Value)
                    If clsCommon.myLen(strPurchaseJobWork) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strPurchaseJobWork + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled Purchase JobWork Account (" & strPurchaseJobWork & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strPurchaseJobWork + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled Purchase JobWork Account(" & strPurchaseJobWork & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If

                    'Stock Transfer JobWork
                    Dim strStockTransferJobWork As String = clsCommon.myCstr(dgrv.Cells("Stock Transfer JobWork").Value)
                    If clsCommon.myLen(strStockTransferJobWork) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strStockTransferJobWork + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled Stock Transfer JobWork Account (" & strStockTransferJobWork & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strStockTransferJobWork + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled Stock Transfer JobWork Account (" & strStockTransferJobWork & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If

                    '===================================

                    If clsCommon.myLen(strReserveStock) > 50 Then
                        Throw New Exception("Reserve stock can not be greater than 50")
                    End If
                    If String.IsNullOrEmpty(strpurchaseclasscode) Or clsCommon.myLen(strpurchaseclasscode) > 6 Then
                        Throw New Exception("Purchase Acccount length should not be greater than six")

                    End If

                    If clsCommon.CompairString(clsCommon.myCstr(IsIndentRequired), "Y") = CompairStringResult.Equal Then
                        ISIndentReq = 1
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(IsIndentRequired), "N") = CompairStringResult.Equal Then
                        ISIndentReq = 0
                    Else
                        Throw New Exception("Fill 'Is Indent Required' in 'Y/N' format")
                    End If


                    Dim strDiffAccount As String = clsCommon.myCstr(dgrv.Cells("Difference Account").Value)
                    If clsCommon.myLen(strDiffAccount) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strDiffAccount + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled Difference Account(" & strDiffAccount & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strDiffAccount + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled Difference Account (" & strDiffAccount & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If

                    If clsCommon.myLen(strReserveStock) > 50 Then
                        Throw New Exception("Reserve stock can not be greater than 50")
                    End If
                    If String.IsNullOrEmpty(strpurchaseclasscode) Or clsCommon.myLen(strpurchaseclasscode) > 6 Then
                        Throw New Exception("Purchase Acccount length should not be greater than six")
                    End If
                    '===============Preeti=================
                    Dim strHandlingCharge As String = clsCommon.myCstr(dgrv.Cells("Handling Charge").Value)
                    If clsCommon.myLen(strHandlingCharge) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strHandlingCharge + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled Handling Charge Account(" & strHandlingCharge & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strHandlingCharge + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled Handling Charge Account  (" & strHandlingCharge & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        'Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strDiffAccount + "' AND ControlAccount ='Y'"
                        'Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        'If check1 <= 0 Then
                        '    Throw New Exception("Filled Difference Account (" & strDiffAccount & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        ' End If
                    End If

                    Dim strEMP As String = clsCommon.myCstr(dgrv.Cells("EMP").Value)
                    If clsCommon.myLen(strEMP) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strEMP + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled EMP Account(" & strEMP & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strEMP + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled EMP Account (" & strEMP & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        'Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strDiffAccount + "' AND ControlAccount ='Y'"
                        'Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        'If check1 <= 0 Then
                        '    Throw New Exception("Filled Difference Account (" & strDiffAccount & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        ' End If
                    End If

                    Dim StrStoreConsumptionAcc As String = clsCommon.myCstr(dgrv.Cells("Store Consumption Acc").Value)
                    '===Sanjeet(27/12/2017)===============

                    Dim strFAClearingAc As String = clsCommon.myCstr(dgrv.Cells("FA Clearing Account").Value)
                    If clsCommon.myLen(strFAClearingAc) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strFAClearingAc + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled FA Clearing Account (" & strFAClearingAc & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strFAClearingAc + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled FA Clearing Account (" & strFAClearingAc & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If

                    End If

                    ' Purchase Loss

                    Dim strPurchaseLoss As String = clsCommon.myCstr(dgrv.Cells("Purchase Loss").Value)
                    If clsCommon.myLen(strPurchaseLoss) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strPurchaseLoss + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Purchase Loss Account (" & strPurchaseLoss & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strPurchaseLoss + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Purchase Loss Account (" & strPurchaseLoss & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If

                    End If

                    '' Wrekage Account agaist ticket no. BHA/08/08/18-000396
                    Dim strWrekageAccount As String = clsCommon.myCstr(dgrv.Cells("Wrekage Account").Value)
                    If clsCommon.myLen(strWrekageAccount) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strWrekageAccount + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Purchase Wrekage Account (" & strWrekageAccount & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strWrekageAccount + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Purchase Wrekage Account (" & strWrekageAccount & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If

                    End If
                    ' Item Opening Clearing

                    Dim strItemOpeningClearing As String = clsCommon.myCstr(dgrv.Cells("Item Opening Clearing").Value)
                    If clsCommon.myLen(strItemOpeningClearing) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strItemOpeningClearing + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Item Opening Clearing Account (" & strItemOpeningClearing & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strItemOpeningClearing + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Item Opening Clearing Account (" & strItemOpeningClearing & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If

                    End If

                    '======================================
                    Dim query As String = "select count(*)from tspl_purchase_accounts where purchase_class_code='" + strpurchaseclasscode + "'"
                    Dim i As Integer = CInt(connectSql.RunScalar(trans, query))
                    Dim COLL As New Hashtable
                    clsCommon.AddColumnsForChange(COLL, "Purchase_Class_Code", strpurchaseclasscode)
                    clsCommon.AddColumnsForChange(COLL, "purchase_class_desc", strpurchaseclassdescription)
                    clsCommon.AddColumnsForChange(COLL, "inv_control_account", strinventorycontrolaccount)
                    clsCommon.AddColumnsForChange(COLL, "Inv_payable_clearing", strinventorypayableclearing)
                    clsCommon.AddColumnsForChange(COLL, "Adjustment_Account", stradjustaccount)
                    clsCommon.AddColumnsForChange(COLL, "assembly_cost_credit", strassemblycostcredit)
                    clsCommon.AddColumnsForChange(COLL, "non_stock_clearing", strnonstockclearing)
                    clsCommon.AddColumnsForChange(COLL, "transfer_clearing", strtransferclearing)
                    clsCommon.AddColumnsForChange(COLL, "shipment_clearing", strshipmentclearing)
                    clsCommon.AddColumnsForChange(COLL, "Disassembly_expense", strdisassemblyexpense)
                    clsCommon.AddColumnsForChange(COLL, "physical_inv_adjustment", strphysicalinventoryadjustment)
                    clsCommon.AddColumnsForChange(COLL, "credit_debit_note_clearing", strcreditdebitclearing)
                    clsCommon.AddColumnsForChange(COLL, "Reserve_Stock", strReserveStock)
                    clsCommon.AddColumnsForChange(COLL, "Breakage_GL_account", strbreakage)
                    clsCommon.AddColumnsForChange(COLL, "Costing_Method", strCostingMethode)
                    clsCommon.AddColumnsForChange(COLL, "WIP_Account", strWIPAccount)
                    clsCommon.AddColumnsForChange(COLL, "RM_Consumption", strRMConsumption)
                    clsCommon.AddColumnsForChange(COLL, "Other_1", strOther1)
                    clsCommon.AddColumnsForChange(COLL, "Other_2", strOther2)
                    clsCommon.AddColumnsForChange(COLL, "Loss_Ac", strLossAc)
                    clsCommon.AddColumnsForChange(COLL, "Job_Work_ac", StrJobworlAcc)
                    clsCommon.AddColumnsForChange(COLL, "Provision_Clearing", StrProvClearring, True)
                    clsCommon.AddColumnsForChange(COLL, "Chilling_Charges", StrChillingClearring, True)
                    clsCommon.AddColumnsForChange(COLL, "Freight_Charges", StrFreightCharges, True)
                    clsCommon.AddColumnsForChange(COLL, "Purchase_JobWork", strPurchaseJobWork, True)
                    ' strStockTransferJobWork
                    clsCommon.AddColumnsForChange(COLL, "Stock_Transfer_JobWork", strStockTransferJobWork, True)
                    clsCommon.AddColumnsForChange(COLL, "Difference_Account", strDiffAccount, True)
                    clsCommon.AddColumnsForChange(COLL, "comp_code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(COLL, "Modify_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(COLL, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                    clsCommon.AddColumnsForChange(COLL, "Is_IndentRequired", ISIndentReq)
                    clsCommon.AddColumnsForChange(COLL, "Handling_Charge", strHandlingCharge, True)
                    clsCommon.AddColumnsForChange(COLL, "EMP", strEMP, True)
                    clsCommon.AddColumnsForChange(COLL, "Store_Consumption_Acc", StrStoreConsumptionAcc, True)
                    clsCommon.AddColumnsForChange(COLL, "FA_CLEARING_AC", strFAClearingAc, True)
                    clsCommon.AddColumnsForChange(COLL, "Purchase_Loss", strPurchaseLoss, True)
                    clsCommon.AddColumnsForChange(COLL, "Wrekage_Account", strWrekageAccount, True)
                    clsCommon.AddColumnsForChange(COLL, "Item_Opening_Clearing", strItemOpeningClearing, True)
                    If (i = 0) Then
                        clsCommon.AddColumnsForChange(COLL, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(COLL, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                        clsCommonFunctionality.UpdateDataTable(COLL, "tspl_purchase_accounts", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strpurchaseclasscode, "TSPL_PURCHASE_ACCOUNTS", "Purchase_Class_Code", trans)
                        clsCommonFunctionality.UpdateDataTable(COLL, "tspl_purchase_accounts", OMInsertOrUpdate.Update, "tspl_purchase_accounts.Purchase_Class_Code='" + strpurchaseclasscode + "'", trans)
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
    Public Sub inventorycontrol_textchanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim str As String = "select account_code as [Account Code],description as [Description]from tspl_gl_accounts where account_code='" + fndInventoryControl.Value + "' "
            str = clsDBFuncationality.getSingleValue(str)
            'Dim str As String
            'While dr.Read()
            '    str = dr(0).ToString()
            'End While
            If str <> "" Then
                inventorycontrol_funfill()
            Else
                rdtxtinventrycontrol.Text = ""
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Public Sub paybleclearing_textchanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim str As String = "select account_code as [Account Code],description as [Description]from tspl_gl_accounts where account_code='" + fndpayableclearing.Value + "' "
            str = clsDBFuncationality.getSingleValue(str)
            'Dim str As String
            'While dr.Read()
            '    str = dr(0).ToString()
            'End While
            If str <> "" Then
                paybleclearing_funfill()
            Else
                rdtxtpayableclearing.Text = ""
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Public Sub addjustment_textchanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim str As String = "select account_code as [Account Code],description as [Description]from tspl_gl_accounts where account_code='" + fndadjustmentwriteoff.Value + "' "
            str = clsDBFuncationality.getSingleValue(str)
            'Dim str As String
            'While dr.Read()
            '    str = dr(0).ToString()
            'End While
            If str <> "" Then
                addjustment_funfill()
            Else
                rdtxtadjustmentwriteoff.Text = ""
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Public Sub assemblycostoff_textchanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim str As String = "select account_code as [Account Code],description as [Description]from tspl_gl_accounts where account_code='" + fndassamblycostoff.Value + "' "
            str = clsDBFuncationality.getSingleValue(str)
            'Dim str As String
            'While dr.Read()
            '    str = dr(0).ToString()
            'End While
            If str <> "" Then
                assemblycostoff_funfill()
            Else
                rdtxtassamblycostcredit.Text = ""
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Public Sub nonstockclearing_textchanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim str As String = "select account_code as [Account Code],description as [Description]from tspl_gl_accounts where account_code='" + fndnonstockclearing.Value + "' "
            str = clsDBFuncationality.getSingleValue(str)
            'Dim str As String
            'While dr.Read()
            '    str = dr(0).ToString()
            'End While
            If str <> "" Then
                nonstockclearing_funfill()
            Else
                rdtxtnonstockclearing.Text = ""
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Public Sub transferclearing_textchanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim str As String = "select account_code as [Account Code],description as [Description]from tspl_gl_accounts where account_code='" + fndtransferclearing.Value + "' "
            str = clsDBFuncationality.getSingleValue(str)
            'Dim str As String
            'While dr.Read()
            '    str = dr(0).ToString()
            'End While
            If str <> "" Then
                transferclearing_funfill()
            Else
                rdtxttransferclearing.Text = ""
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Public Sub shipmentclearing_textchanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim str As String = "select account_code as [Account Code],description as [Description]from tspl_gl_accounts where account_code='" + fndshipmentclearing.Value + "' "
            str = clsDBFuncationality.getSingleValue(str)
            'Dim str As String
            'While dr.Read()
            '    str = dr(0).ToString()
            'End While
            If str <> "" Then
                shipmentclearing_funfill()
            Else
                rdtxtshipmentexpense.Text = ""
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Public Sub disassemblyexpense_textchanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim str As String = "select account_code as [Account Code],description as [Description]from tspl_gl_accounts where account_code='" + fnddisassamblyexpense.Value + "' "
            str = clsDBFuncationality.getSingleValue(str)
            'Dim str As String
            'While dr.Read()
            '    str = dr(0).ToString()
            'End While
            If str <> "" Then
                disassemblyexpense_funfill()
            Else
                rdtxtdisassamblyexpense.Text = ""
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Public Sub physicalinventoryadjustment_textchanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim str As String = "select account_code as [Account Code],description as [Description]from tspl_gl_accounts where account_code='" + fndphysicalinventrycontrol.Value + "' "
            str = clsDBFuncationality.getSingleValue(str)
            'Dim str As String
            'While dr.Read()
            '    str = dr(0).ToString()
            'End While
            If str <> "" Then
                physicalinventoryadjustment_funfill()
            Else
                rdtxtphysicalinventryadj.Text = ""
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Public Sub creditdebit_textchanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim str As String = "select account_code as [Account Code],description as [Description]from tspl_gl_accounts where account_code='" + fndcreditdebitnoteclr.Value + "' "
            str = clsDBFuncationality.getSingleValue(str)
            'Dim str As String
            'While dr.Read()
            '    str = dr(0).ToString()
            'End While
            If str <> "" Then
                creditdebit_funfill()
            Else
                rdtxtcreditdebitnoteclr.Text = ""
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    '' Anubhooti 08-July-2014 (BM00000003088)
    Public Sub WIPAccount_textchanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim str As String = "select account_code as [Account Code],description as [Description]from tspl_gl_accounts where account_code='" + fndWIPAcc.Value + "' "
            str = clsDBFuncationality.getSingleValue(str)
            If str <> "" Then
                WIPAccount_funfill()
            Else
                rdtxtWIPAcc.Text = ""
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub


    Public Sub RMConsumption_textchanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim str As String = "select account_code as [Account Code],description as [Description]from tspl_gl_accounts where account_code='" + fndRMCons.Value + "' "
            str = clsDBFuncationality.getSingleValue(str)
            If str <> "" Then
                RMConsumption_funfill()
            Else
                rdtxtRMCons.Text = ""
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Public Sub Other1_textchanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim str As String = "select account_code as [Account Code],description as [Description] from tspl_gl_accounts where account_code='" + fndOther1.Value + "' "
            str = clsDBFuncationality.getSingleValue(str)
            If str <> "" Then
                Other1_funfill()
            Else
                rdtxtOther1.Text = ""
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Public Sub Other2_textchanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim str As String = "select account_code as [Account Code],description as [Description] from tspl_gl_accounts where account_code='" + fndOther2.Value + "' "
            str = clsDBFuncationality.getSingleValue(str)
            If str <> "" Then
                Other2_funfill()
            Else
                rdtxtOther2.Text = ""
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    ''
    'This function is used for retrieve data in inventory finder control. 
    Public Sub inventorycontrol_funfill()
        Dim query1 As String = "Select description from tspl_gl_accounts where account_code ='" + fndInventoryControl.Value + "'"
        rdtxtinventrycontrol.Text = clsDBFuncationality.getSingleValue(query1)
        'While dr.Read()
        '    rdtxtinventrycontrol.Text = dr(1).ToString()
        'End While
    End Sub
    'This function is used to retrieve data in payable clearing finder Control.
    Public Sub paybleclearing_funfill()
        Dim query1 As String = "Select description from tspl_gl_accounts where account_code ='" + fndpayableclearing.Value + "'"
        rdtxtpayableclearing.Text = clsDBFuncationality.getSingleValue(query1)
        'While dr.Read()
        '    rdtxtpayableclearing.Text = dr(1).ToString()
        'End While
    End Sub
    'This function is used to retrieve 
    Public Sub addjustment_funfill()
        Dim query1 As String = "Select description from tspl_gl_accounts where account_code ='" + fndadjustmentwriteoff.Value + "'"
        rdtxtadjustmentwriteoff.Text = clsDBFuncationality.getSingleValue(query1)
        'While dr.Read()
        '    rdtxtadjustmentwriteoff.Text = dr(1).ToString()
        'End While

    End Sub
    Public Sub assemblycostoff_funfill()
        Dim query1 As String = "Select description from tspl_gl_accounts where account_code ='" + fndassamblycostoff.Value + "'"
        rdtxtassamblycostcredit.Text = clsDBFuncationality.getSingleValue(query1)
        'While dr.Read()
        '    rdtxtassamblycostcredit.Text = dr(1).ToString()
        'End While
    End Sub
    'This is used for retrieve data in non stock clearing finder control.
    Public Sub nonstockclearing_funfill()
        Dim query1 As String = "Select description from tspl_gl_accounts where account_code ='" + fndnonstockclearing.Value + "'"
        rdtxtnonstockclearing.Text = clsDBFuncationality.getSingleValue(query1)
        'While dr.Read()
        '    rdtxtnonstockclearing.Text = dr(1).ToString()
        'End While
    End Sub
    'This is used to retrieve data in transfer clearing finder control.
    Public Sub transferclearing_funfill()
        Dim query1 As String = "Select description from tspl_gl_accounts where account_code ='" + fndtransferclearing.Value + "'"
        rdtxttransferclearing.Text = clsDBFuncationality.getSingleValue(query1)
        'While dr.Read()
        '    rdtxttransferclearing.Text = dr(1).ToString()
        'End While
    End Sub
    'This is used to retrieve data in shipment Clearing finder control.
    Public Sub shipmentclearing_funfill()
        Dim query1 As String = "Select description from tspl_gl_accounts where account_code ='" + fndshipmentclearing.Value + "'"
        rdtxtshipmentexpense.Text = clsDBFuncationality.getSingleValue(query1)
        'While dr.Read()
        '    rdtxtshipmentexpense.Text = dr(1).ToString()
        'End While
    End Sub
    'This is used to retrieve data in disassembly expense finder control.
    Public Sub disassemblyexpense_funfill()
        Dim query1 As String = "Select description from tspl_gl_accounts where account_code ='" + fnddisassamblyexpense.Value + "'"
        rdtxtdisassamblyexpense.Text = clsDBFuncationality.getSingleValue(query1)
        'While dr.Read()
        '    rdtxtdisassamblyexpense.Text = dr(1).ToString()
        'End While
    End Sub
    'This is used to retrieve data in physical inventory adjustment finder control. 
    Public Sub physicalinventoryadjustment_funfill()
        Dim query1 As String = "Select description from tspl_gl_accounts where account_code ='" + fndphysicalinventrycontrol.Value + "'"
        rdtxtphysicalinventryadj.Text = clsDBFuncationality.getSingleValue(query1)
        'While dr.Read()
        '    rdtxtphysicalinventryadj.Text = dr(1).ToString()
        'End While
    End Sub
    'This is used to retrieve data in credit debit finder control.
    Public Sub creditdebit_funfill()
        Dim query1 As String = "Select description from tspl_gl_accounts where account_code ='" + fndcreditdebitnoteclr.Value + "'"
        rdtxtcreditdebitnoteclr.Text = clsDBFuncationality.getSingleValue(query1)
        'While dr.Read()
        '    rdtxtcreditdebitnoteclr.Text = dr(1).ToString()
        'End While
    End Sub
    '' Anubhooti 08-July-2014 (BM00000003088)
    'This is used to retrieve data in WIP Account finder control.
    Public Sub WIPAccount_funfill()
        Dim query1 As String = "Select description from tspl_gl_accounts where account_code ='" + fndWIPAcc.Value + "'"
        rdtxtWIPAcc.Text = clsDBFuncationality.getSingleValue(query1)
    End Sub
    'This is used to retrieve data in RM Consumption finder control.
    Public Sub RMConsumption_funfill()
        Dim query1 As String = "Select description from tspl_gl_accounts where account_code ='" + fndRMCons.Value + "'"
        rdtxtRMCons.Text = clsDBFuncationality.getSingleValue(query1)
    End Sub
    'This is used to retrieve data in Other1 finder control. 
    Public Sub Other1_funfill()
        Dim query1 As String = "Select description from tspl_gl_accounts where account_code ='" + fndOther1.Value + "'"
        rdtxtOther1.Text = clsDBFuncationality.getSingleValue(query1)
    End Sub
    'This is used to retrieve data in Other2 finder control.
    Public Sub Other2_funfill()
        Dim query1 As String = "Select description from tspl_gl_accounts where account_code ='" + fndOther2.Value + "'"
        rdtxtOther2.Text = clsDBFuncationality.getSingleValue(query1)
    End Sub

    ''
    Public Sub inventorycontrol_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fndInventoryControl.Text = "" Then

        Else
            Try
                Dim str As String = "select account_code,description  from Tspl_gl_Accounts where account_code='" + fndInventoryControl.Value + "'"
                'Dim dr As SqlDataReader
                Dim strvalue As String = clsDBFuncationality.getSingleValue(str)

                'dr = connectSql.RunSqlReturnDR(str)
                'While dr.Read()
                '    strvalue = dr(0).ToString()
                'End While



                If strvalue <> "" Then

                Else : str = ""

                    rdtxtinventrycontrol.Text = ""
                    common.clsCommon.MyMessageBoxShow(Me, "This Inventory Control Account does not exist")
                    fndInventoryControl.Text = ""


                End If


            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
    End Sub
    Public Sub paybleclearing_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fndpayableclearing.Text = "" Then

        Else
            Try
                Dim str As String = "select account_code,description  from Tspl_gl_Accounts where account_code='" + fndpayableclearing.Value + "'"
                ' Dim dr As SqlDataReader
                Dim strvalue As String = clsDBFuncationality.getSingleValue(str)

                'dr = connectSql.RunSqlReturnDR(str)
                'While dr.Read()
                '    strvalue = dr(0).ToString()
                'End While



                If strvalue <> "" Then

                Else : str = ""

                    rdtxtpayableclearing.Text = ""
                    common.clsCommon.MyMessageBoxShow(Me, "This Payble Clearing Account does not exist", Me.Text)
                    fndpayableclearing.Text = ""


                End If


            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
    End Sub
    Public Sub addjustment_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fndadjustmentwriteoff.Text = "" Then

        Else
            Try
                Dim str As String = "select account_code,description  from Tspl_gl_Accounts where account_code='" + fndadjustmentwriteoff.Text + "'"
                ' Dim dr As SqlDataReader
                Dim strvalue As String = clsDBFuncationality.getSingleValue(str)

                'dr = connectSql.RunSqlReturnDR(str)
                'While dr.Read()
                '    strvalue = dr(0).ToString()
                'End While



                If strvalue <> "" Then

                Else : str = ""

                    rdtxtadjustmentwriteoff.Text = ""
                    common.clsCommon.MyMessageBoxShow(Me, "This Adjust Write Offs Account does not exist", Me.Text)
                    fndadjustmentwriteoff.Text = ""


                End If


            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If

    End Sub
    Public Sub assemblycostoff_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fndassamblycostoff.Text = "" Then

        Else
            Try
                Dim str As String = "select account_code,description  from Tspl_gl_Accounts where account_code='" + fndassamblycostoff.Value + "'"
                ' Dim dr As SqlDataReader
                Dim strvalue As String = clsDBFuncationality.getSingleValue(str)

                'dr = connectSql.RunSqlReturnDR(str)
                'While dr.Read()
                '    strvalue = dr(0).ToString()
                'End While



                If strvalue <> "" Then

                Else : str = ""

                    rdtxtassamblycostcredit.Text = ""
                    common.clsCommon.MyMessageBoxShow(Me, "This FG Shortage  Account does not exist", Me.Text)
                    fndassamblycostoff.Value = ""


                End If


            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
    End Sub
    Public Sub nonstockclearing_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fndnonstockclearing.Text = "" Then

        Else
            Try
                Dim str As String = "select account_code,description  from Tspl_gl_Accounts where account_code='" + fndnonstockclearing.Text + "'"
                ' Dim dr As SqlDataReader
                Dim strvalue As String = clsDBFuncationality.getSingleValue(str)

                'dr = connectSql.RunSqlReturnDR(str)
                'While dr.Read()
                '    strvalue = dr(0).ToString()
                'End While



                If strvalue <> "" Then

                Else : str = ""

                    rdtxtnonstockclearing.Text = ""
                    common.clsCommon.MyMessageBoxShow(Me, "This Inventory Control Empties Account does not exist", Me.Text)
                    fndnonstockclearing.Text = ""


                End If


            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
    End Sub
    Public Sub transferclearing_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fndtransferclearing.Text = "" Then

        Else
            Try
                Dim str As String = "select account_code,description  from Tspl_gl_Accounts where account_code='" + fndtransferclearing.Value + "'"
                '  Dim dr As SqlDataReader
                Dim strvalue As String = clsDBFuncationality.getSingleValue(str)

                'dr = connectSql.RunSqlReturnDR(str)
                'While dr.Read()
                '    strvalue = dr(0).ToString()
                'End While



                If strvalue <> "" Then

                Else : str = ""

                    rdtxttransferclearing.Text = ""
                    common.clsCommon.MyMessageBoxShow(Me, "This Transfer Clearing Account does not exist", Me.Text)
                    fndtransferclearing.Text = ""


                End If


            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If

    End Sub
    Public Sub shipmentclearing_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fndshipmentclearing.Text = "" Then

        Else
            Try
                Dim str As String = "select account_code,description  from Tspl_gl_Accounts where account_code='" + fndshipmentclearing.Value + "'"
                '    Dim dr As SqlDataReader
                Dim strvalue As String = clsDBFuncationality.getSingleValue(str)

                'dr = connectSql.RunSqlReturnDR(str)
                'While dr.Read()
                '    strvalue = dr(0).ToString()
                'End While



                If strvalue <> "" Then

                Else : str = ""

                    rdtxtshipmentexpense.Text = ""
                    common.clsCommon.MyMessageBoxShow(Me, "This Shipment Clearing Account does not exist", Me.Text)
                    fndshipmentclearing.Text = ""


                End If


            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
    End Sub
    Public Sub disassemblyexpense_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fnddisassamblyexpense.Text = "" Then

        Else
            Try
                Dim str As String = "select account_code,description  from Tspl_gl_Accounts where account_code='" + fnddisassamblyexpense.Value + "'"
                ' Dim dr As SqlDataReader
                Dim strvalue As String = clsDBFuncationality.getSingleValue(str)

                'dr = connectSql.RunSqlReturnDR(str)
                'While dr.Read()
                '    strvalue = dr(0).ToString()
                'End While



                If strvalue <> "" Then

                Else : str = ""

                    rdtxtdisassamblyexpense.Text = ""
                    common.clsCommon.MyMessageBoxShow(Me, "This Dissambly Expence Account does not exist", Me.Text)
                    fnddisassamblyexpense.Text = ""


                End If


            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
    End Sub
    Public Sub physicalinventoryadjustment_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fndphysicalinventrycontrol.Text = "" Then

        Else
            Try
                Dim str As String = "select account_code,description  from Tspl_gl_Accounts where account_code='" + fndphysicalinventrycontrol.Text + "'"
                ' Dim dr As SqlDataReader
                Dim strvalue As String = clsDBFuncationality.getSingleValue(str)

                'dr = connectSql.RunSqlReturnDR(str)
                'While dr.Read()
                '    strvalue = dr(0).ToString()
                'End While



                If strvalue <> "" Then

                Else : str = ""

                    rdtxtphysicalinventryadj.Text = ""
                    common.clsCommon.MyMessageBoxShow(Me, "This Physicaly Inventory Adjustment Account does not exist", Me.Text)
                    fndphysicalinventrycontrol.Text = ""


                End If


            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
    End Sub
    Public Sub creditdebit_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fndcreditdebitnoteclr.Text = "" Then

        Else
            Try
                Dim str As String = "select account_code,description  from Tspl_gl_Accounts where account_code='" + fndcreditdebitnoteclr.Text + "'"
                ' Dim dr As SqlDataReader
                Dim strvalue As String = clsDBFuncationality.getSingleValue(str)

                'dr = connectSql.RunSqlReturnDR(str)
                'While dr.Read()
                '    strvalue = dr(0).ToString()
                'End While



                If strvalue <> "" Then

                Else : str = ""

                    rdtxtcreditdebitnoteclr.Text = ""
                    common.clsCommon.MyMessageBoxShow(Me, "This Credit Debit Note Clr Account does not exist", Me.Text)
                    fndcreditdebitnoteclr.Text = ""


                End If


            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
    End Sub
    '' Anubhooti 08-July-2014 (BM00000003088)
    Public Sub WIPAccount_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fndWIPAcc.Text = "" Then

        Else
            Try
                Dim str As String = "select account_code,description  from Tspl_gl_Accounts where account_code='" + fndWIPAcc.Text + "'"
                Dim strvalue As String = clsDBFuncationality.getSingleValue(str)
                If strvalue <> "" Then
                Else : str = ""
                    rdtxtWIPAcc.Text = ""
                    common.clsCommon.MyMessageBoxShow(Me, "This WIP Account does not exist", Me.Text)
                    fndWIPAcc.Text = ""
                End If
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
    End Sub
    Public Sub RMConsumption_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fndRMCons.Text = "" Then

        Else
            Try
                Dim str As String = "select account_code,description  from Tspl_gl_Accounts where account_code='" + fndRMCons.Text + "'"
                Dim strvalue As String = clsDBFuncationality.getSingleValue(str)
                If strvalue <> "" Then
                Else : str = ""
                    rdtxtRMCons.Text = ""
                    common.clsCommon.MyMessageBoxShow(Me, "This RM Consumption Account does not exist", Me.Text)
                    fndRMCons.Text = ""
                End If
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
    End Sub
    Public Sub Other1_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fndOther1.Text = "" Then

        Else
            Try
                Dim str As String = "select account_code,description  from Tspl_gl_Accounts where account_code='" + fndOther1.Text + "'"
                Dim strvalue As String = clsDBFuncationality.getSingleValue(str)
                If strvalue <> "" Then
                Else : str = ""
                    rdtxtOther1.Text = ""
                    common.clsCommon.MyMessageBoxShow(Me, "This Other 1 Account does not exist")
                    fndOther1.Text = ""
                End If
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
    End Sub
    Public Sub Other2_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fndOther2.Text = "" Then

        Else
            Try
                Dim str As String = "select account_code,description  from Tspl_gl_Accounts where account_code='" + fndOther2.Text + "'"
                Dim strvalue As String = clsDBFuncationality.getSingleValue(str)
                If strvalue <> "" Then
                Else : str = ""
                    rdtxtOther2.Text = ""
                    common.clsCommon.MyMessageBoxShow(Me, "This Other 2 Account does not exist", Me.Text)
                    fndOther2.Text = ""
                End If
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
    End Sub

    ''

    'Private Sub fndinventrycontrol_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    'fndinventrycontrol.Query = "select account_code as [Account Code],description as [Description]from tspl_gl_accounts"
    '    fndinventrycontrol.Query = clsERPFuncationality.glaccountquery
    '    fndinventrycontrol.ConnectionString = connectSql.SqlCon()
    '    fndinventrycontrol.Caption = "Inventory Control Account"
    '    fndinventrycontrol.ValueToSelect = "Account_Code"
    '    fndinventrycontrol.ValueToSelect1 = "Description"
    'End Sub

    'Private Sub fndpayableclearing_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    'fndpayableclearing.Query = "select account_code as [Account Code],description as [Description]from tspl_gl_accounts"
    '    fndpayableclearing.Query = clsERPFuncationality.glaccountquery
    '    fndpayableclearing.ConnectionString = connectSql.SqlCon()
    '    fndpayableclearing.Caption = "Payable Clearing Control Account"
    '    fndpayableclearing.ValueToSelect = "Account_Code"
    '    fndpayableclearing.ValueToSelect1 = "Description"
    'End Sub

    'Private Sub fndadjustmentwriteoff_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    '    fndadjustmentwriteoff.Query = "select account_code as [Account Code],description as [Description]from tspl_gl_accounts"
    '    fndadjustmentwriteoff.Query = clsERPFuncationality.glaccountquery
    '    fndadjustmentwriteoff.ConnectionString = connectSql.SqlCon()
    '    fndadjustmentwriteoff.Caption = "Adjustment Writte Offs  Account"
    '    fndadjustmentwriteoff.ValueToSelect = "Account_Code"
    '    fndadjustmentwriteoff.ValueToSelect1 = "Description"
    'End Sub

    'Private Sub fndassamblycostoff_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    'fndassamblycostoff.Query = "select account_code as [Account Code],description as [Description]from tspl_gl_accounts"
    '    fndassamblycostoff.Query = clsERPFuncationality.glaccountquery
    '    fndassamblycostoff.ConnectionString = connectSql.SqlCon()
    '    fndassamblycostoff.Caption = "Assambly CostOff  Account"
    '    fndassamblycostoff.ValueToSelect = "Account_Code"
    '    fndassamblycostoff.ValueToSelect1 = "Description"
    'End Sub

    'Private Sub fndnonstockclearing_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    'fndnonstockclearing.Query = "select account_code as [Account Code],description as [Description]from tspl_gl_accounts"
    '    fndnonstockclearing.Query = clsERPFuncationality.glaccountquery
    '    fndnonstockclearing.ConnectionString = connectSql.SqlCon()
    '    fndnonstockclearing.Caption = "NonStock Clearing Account"
    '    fndnonstockclearing.ValueToSelect = "Account_Code"
    '    fndnonstockclearing.ValueToSelect1 = "Description"
    'End Sub

    'Private Sub fndtransferclearing_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    'fndtransferclearing.Query = "select account_code as [Account Code],description as [Description]from tspl_gl_accounts"
    '    fndtransferclearing.Query = clsERPFuncationality.glaccountquery
    '    fndtransferclearing.ConnectionString = connectSql.SqlCon()
    '    fndtransferclearing.Caption = "Transfer Clearing Account"
    '    fndtransferclearing.ValueToSelect = "Account_Code"
    '    fndtransferclearing.ValueToSelect1 = "Description"
    'End Sub

    'Private Sub fndshipmentclearing_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    'fndshipmentclearing.Query = "select account_code as [Account Code],description as [Description]from tspl_gl_accounts"
    '    fndshipmentclearing.Query = clsERPFuncationality.glaccountquery
    '    fndshipmentclearing.ConnectionString = connectSql.SqlCon()
    '    fndshipmentclearing.Caption = "Shipment Clearing Account"
    '    fndshipmentclearing.ValueToSelect = "Account_Code"
    '    fndshipmentclearing.ValueToSelect1 = "Description"
    'End Sub

    'Private Sub fnddisassamblyexpense_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    'fnddisassamblyexpense.Query = "select account_code as [Account Code],description as [Description]from tspl_gl_accounts"
    '    fnddisassamblyexpense.Query = clsERPFuncationality.glaccountquery
    '    fnddisassamblyexpense.ConnectionString = connectSql.SqlCon()
    '    fnddisassamblyexpense.Caption = "Disassambly Expences Account"
    '    fnddisassamblyexpense.ValueToSelect = "Account_Code"
    '    fnddisassamblyexpense.ValueToSelect1 = "Description"
    'End Sub

    'Private Sub fndphysicalinventrycontrol_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    'fndphysicalinventrycontrol.Query = "select account_code as [Account Code],description as [Description]from tspl_gl_accounts"
    '    fndphysicalinventrycontrol.Query = clsERPFuncationality.glaccountquery
    '    fndphysicalinventrycontrol.ConnectionString = connectSql.SqlCon()
    '    fndphysicalinventrycontrol.Caption = "Physicaly Inventory Control Account"
    '    fndphysicalinventrycontrol.ValueToSelect = "Account_Code"
    '    fndphysicalinventrycontrol.ValueToSelect1 = "Description"
    'End Sub

    'Private Sub fndcreditdebitnoteclr_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    'fndcreditdebitnoteclr.Query = "select account_code as [Account Code],description as [Description]from tspl_gl_accounts"
    '    fndcreditdebitnoteclr.Query = clsERPFuncationality.glaccountquery
    '    fndcreditdebitnoteclr.ConnectionString = connectSql.SqlCon()
    '    fndcreditdebitnoteclr.Caption = "Credit Debit Note Clr Account"
    '    fndcreditdebitnoteclr.ValueToSelect = "Account_Code"
    '    fndcreditdebitnoteclr.ValueToSelect1 = "Description"
    'End Sub
    ''priti added on 01-06-2011 --- To implement the access control
    ''Private Function funSetUserAccess() As Boolean
    ''    Try
    ''        'If funCheckLoginStatus() = False Then Exit Function
    ''        Dim strRights As String
    ''        Dim strTemp() As String
    ''        Dim strProgCode = "ITEM-PUR-ACC"
    ''        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
    ''        strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    ''        strTemp = Split(strRights, ",")
    ''        If strTemp(0) = "0" Then
    ''            MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    ''            funSetUserAccess = False
    ''            blnRead = False
    ''            Me.Close()
    ''            Exit Function
    ''        Else
    ''            blnRead = True
    ''        End If
    ''        If strTemp(1) = "0" Then 'Grant modify access
    ''            rdbtnsave.Enabled = False
    ''        End If
    ''        If strTemp(2) = "0" Then 'Grant modify access
    ''            rdbtndelete.Enabled = False
    ''        End If

    ''        funSetUserAccess = True
    ''    Catch er As Exception

    ''    End Try
    ''End Function
    ''Code ends here

    'Private Sub fndReserveStock_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fndReserveStock.ConnectionString = connectSql.SqlCon()
    '    fndReserveStock.Query = clsERPFuncationality.glaccountquery
    '    '    fndReserveStock.Query = "select account_code as [Account Code],description as [Description]from tspl_gl_accounts"
    '    fndReserveStock.Caption = "GL Accounts"
    '    fndReserveStock.ValueToSelect = "Account_Code"
    '    fndReserveStock.ValueToSelect1 = "Description"
    '    fndReserveStock.txtValue.MaxLength = 50
    'End Sub
    'Private Sub fndReserveStock_TaxChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        Dim strAcc_code As String = "select account_code, description from tspl_gl_accounts where account_code='" + fndReserveStock.txtValue.Text + "'"
    '        ' Dim dr As SqlDataReader
    '        Dim strvalue As String
    '        strvalue = clsDBFuncationality.getSingleValue(strAcc_code)
    '        'While dr.Read()
    '        '    strvalue = dr(0).ToString()
    '        'End While
    '        If strvalue <> "" Then
    '            funfill1Desc()
    '        Else
    '            txtReserveStock.Text = ""
    '        End If
    '    Catch ex As Exception
    '        myMessages.myExceptions(ex)
    '    End Try
    'End Sub
    'Public Sub funfill1Desc()
    '    Try
    '        Dim strDesc As String = "select  description from tspl_gl_accounts where account_code='" + fndReserveStock.txtValue.Text + "'"
    '        'Dim dr As SqlDataReader
    '        txtReserveStock.Text = clsDBFuncationality.getSingleValue(strDesc)
    '        'While dr.Read()
    '        '    txtReserveStock.Text = dr(1).ToString()
    '        'End While
    '    Catch ex As Exception
    '        myMessages.myExceptions(ex)
    '    End Try
    'End Sub
    'Private Sub fndReserveStock_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If fndReserveStock.txtValue.Text = "" Then
    '    Else
    '        Try
    '            Dim stremp_code As String = "select account_code, description from tspl_gl_accounts where account_code='" + fndReserveStock.txtValue.Text + "'"
    '            '  Dim dr As SqlDataReader
    '            Dim strvalue As String
    '            strvalue = clsDBFuncationality.getSingleValue(stremp_code)
    '            'While dr.Read()
    '            '    strvalue = dr(0).ToString()
    '            'End While
    '            If strvalue <> "" Then
    '            Else : stremp_code = ""
    '                txtReserveStock.Text = ""
    '                common.clsCommon.MyMessageBoxShow("Reserve stock does not exist in the master table")
    '                fndReserveStock.txtValue.Text = ""
    '                fndReserveStock.Focus()
    '            End If
    '        Catch ex As Exception
    '            myMessages.myExceptions(ex)
    '        End Try
    '    End If
    'End Sub


    'Private Sub fndbreakageglaccount_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fndbreakageglaccount.Load
    '    fndbreakageglaccount.ConnectionString = connectSql.SqlCon()
    '    fndbreakageglaccount.Query = clsERPFuncationality.glaccountquery
    '    fndbreakageglaccount.ValueToSelect = "Account_Code"
    '    fndbreakageglaccount.Caption = "Account Description"
    '    fndbreakageglaccount.ValueToSelect1 = "Description"
    'End Sub

    Private Sub fndaccountsetcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndaccountsetcode._MYValidating
        Dim str As String = "select count(*) from tspl_purchase_accounts where purchase_class_code ='" + fndaccountsetcode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 Then
            fndaccountsetcode.MyReadOnly = False
        Else
            fndaccountsetcode.MyReadOnly = True
        End If
        If fndaccountsetcode.MyReadOnly OrElse isButtonClicked Then

            'Dim qry As String = "select purchase_class_code as Code,purchase_class_desc as [Purchase Account Desc]from tspl_purchase_accounts"
            'fndaccountsetcode.Value = clsCommon.ShowSelectForm("PurchaseAccount", qry, "Code", "", fndaccountsetcode.Value, "", isButtonClicked)
            fndaccountsetcode.Value = clsPurchaseAccountSet.getFinder("", fndaccountsetcode.Value, isButtonClicked)
            LoadData()
        End If
    End Sub
    Public Sub LoadData()
        Dim var As String
        Dim query As String = "Select purchase_class_code from tspl_purchase_accounts where purchase_class_code ='" + fndaccountsetcode.Value + "'"
        var = clsDBFuncationality.getSingleValue(query)

        'While (dr.Read())
        '    var = dr(0).ToString()
        'End While
        If var <> "" Then
            funfill()

            rdbtnsave.Text = "Update"
            rdtxtdescription.Enabled = True
            rdtxtdescription.ReadOnly = False
            fndshipmentclearing.Enabled = True
            fndInventoryControl.Enabled = True
            fndpayableclearing.Enabled = True
            fndadjustmentwriteoff.Enabled = True
            fndassamblycostoff.Enabled = True
            fndnonstockclearing.Enabled = True
            fndtransferclearing.Enabled = True
            fnddisassamblyexpense.Enabled = True
            fndphysicalinventrycontrol.Enabled = True
            fndcreditdebitnoteclr.Enabled = True
            rdbtnsave.Enabled = True
            rdbtndelete.Enabled = True
            fndTransferGainLoss.Enabled = True

        Else
            rdbtnsave.Text = "Save"
            rdtxtdescription.Text = ""
            rdtxtdescription.ReadOnly = False
            fndshipmentclearing.Value = ""
            fndInventoryControl.Value = ""
            fndpayableclearing.Value = ""
            fndadjustmentwriteoff.Value = ""
            fndassamblycostoff.Value = ""
            fndnonstockclearing.Value = ""
            fndtransferclearing.Value = ""
            fnddisassamblyexpense.Value = ""
            fndphysicalinventrycontrol.Value = ""
            fndcreditdebitnoteclr.Value = ""
            fndReserveStock.Value = ""
            fndbreakageglaccount.Value = ""
            rdbtndelete.Enabled = False
            rdbtnclose.Enabled = True
            fndTransferGainLoss.Value = ""
            txtPurchaseCTRL_Ac.Value = ""
            txtPurchaseCtrlAcDesc.Text = ""
            FndJobWork.Value = ""
            LblJobwork.Text = ""
            fndItemOpeningClearing.Value = Nothing
            lblItemOpeningClearing.Text = ""

        End If
    End Sub

    Private Sub fndaccountsetcode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndaccountsetcode._MYNavigator
        Dim qst As String = "select purchase_class_code as Code,purchase_class_desc as [Purchase Account Desc]from tspl_purchase_accounts where 2=2 "
        Select Case NavType
            Case NavigatorType.Current
                qst += " and tspl_purchase_accounts .purchase_class_code in ('" + fndaccountsetcode.Value + "')"
            Case NavigatorType.Next
                qst += " and tspl_purchase_accounts .purchase_class_code in (select min(purchase_class_code ) from tspl_purchase_accounts where purchase_class_code  >'" + fndaccountsetcode.Value + "')"
            Case NavigatorType.First
                qst += " and tspl_purchase_accounts .purchase_class_code in (select MIN(purchase_class_code ) from tspl_purchase_accounts)"

            Case NavigatorType.Last
                qst += " and tspl_purchase_accounts .purchase_class_code in (select Max(purchase_class_code ) from tspl_purchase_accounts)"
            Case NavigatorType.Previous
                qst += " and tspl_purchase_accounts .purchase_class_code in (select Max(purchase_class_code ) from tspl_purchase_accounts where purchase_class_code  <'" + fndaccountsetcode.Value + "')"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            fndaccountsetcode.Value = clsCommon.myCstr(dt.Rows(0)("Code"))
            rdtxtdescription.Text = clsCommon.myCstr(dt.Rows(0)("Purchase Account Desc"))
        End If
        LoadData()
    End Sub

    Private Sub fndInventoryControl__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndInventoryControl._MYValidating

        Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
        fndInventoryControl.Value = clsCommon.ShowSelectForm("fndInventoryControl", Qry, "Account_Code", " ControlAccount ='Y' ", fndInventoryControl.Value, "Account_Code", isButtonClicked)
        rdtxtinventrycontrol.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + fndInventoryControl.Value + "' ")

    End Sub

    Private Sub fndpayableclearing__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndpayableclearing._MYValidating
        Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
        fndpayableclearing.Value = clsCommon.ShowSelectForm("fndpayableclearing", Qry, "Account_Code", " ControlAccount ='Y' ", fndpayableclearing.Value, "Account_Code", isButtonClicked)
        rdtxtpayableclearing.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + fndpayableclearing.Value + "' ")

    End Sub

    Private Sub fndadjustmentwriteoff__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndadjustmentwriteoff._MYValidating
        Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
        fndadjustmentwriteoff.Value = clsCommon.ShowSelectForm("fndadjustmentwriteoff", Qry, "Account_Code", " ControlAccount ='Y' ", fndadjustmentwriteoff.Value, "Account_Code", isButtonClicked)
        rdtxtadjustmentwriteoff.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + fndadjustmentwriteoff.Value + "' ")

    End Sub

    Private Sub fndWrekageAccount__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndWrekageAccount._MYValidating
        Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
        fndWrekageAccount.Value = clsCommon.ShowSelectForm("fndWrekageAccount", Qry, "Account_Code", " ControlAccount ='Y' ", fndWrekageAccount.Value, "Account_Code", isButtonClicked)
        txtWrekageAccount.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + fndWrekageAccount.Value + "' ")

    End Sub

    Private Sub fndassamblycostoff__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndassamblycostoff._MYValidating
        Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
        fndassamblycostoff.Value = clsCommon.ShowSelectForm("fndassamblycostoff", Qry, "Account_Code", " ControlAccount ='Y' ", fndassamblycostoff.Value, "Account_Code", isButtonClicked)
        rdtxtassamblycostcredit.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + fndassamblycostoff.Value + "' ")

    End Sub

    Private Sub fndnonstockclearing__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndnonstockclearing._MYValidating
        Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
        fndnonstockclearing.Value = clsCommon.ShowSelectForm("fndnonstockclearing", Qry, "Account_Code", " ControlAccount ='Y' ", fndnonstockclearing.Value, "Account_Code", isButtonClicked)
        rdtxtnonstockclearing.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + fndnonstockclearing.Value + "' ")

    End Sub

    Private Sub fndtransferclearing__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndtransferclearing._MYValidating
        Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
        fndtransferclearing.Value = clsCommon.ShowSelectForm("fndtransferclearing", Qry, "Account_Code", " ControlAccount ='Y'", fndtransferclearing.Value, "Account_Code", isButtonClicked)
        rdtxttransferclearing.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + fndtransferclearing.Value + "' ")

    End Sub

    Private Sub fndshipmentclearing__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndshipmentclearing._MYValidating
        Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
        fndshipmentclearing.Value = clsCommon.ShowSelectForm("fndshipmentclearing", Qry, "Account_Code", " ControlAccount ='Y'", fndshipmentclearing.Value, "Account_Code", isButtonClicked)
        rdtxtshipmentexpense.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + fndshipmentclearing.Value + "' ")

    End Sub

    Private Sub fnddisassamblyexpense__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fnddisassamblyexpense._MYValidating
        Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
        fnddisassamblyexpense.Value = clsCommon.ShowSelectForm("fnddisassamblyexpense", Qry, "Account_Code", " ControlAccount ='Y' ", fnddisassamblyexpense.Value, "Account_Code", isButtonClicked)
        rdtxtdisassamblyexpense.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + fnddisassamblyexpense.Value + "' ")

    End Sub

    Private Sub fndphysicalinventrycontrol__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndphysicalinventrycontrol._MYValidating
        Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
        fndphysicalinventrycontrol.Value = clsCommon.ShowSelectForm("fndphysicalinventrycontrol", Qry, "Account_Code", " ControlAccount ='Y' ", fndphysicalinventrycontrol.Value, "Account_Code", isButtonClicked)
        rdtxtphysicalinventryadj.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + fndphysicalinventrycontrol.Value + "' ")

    End Sub

    Private Sub fndcreditdebitnoteclr__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndcreditdebitnoteclr._MYValidating
        Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
        fndcreditdebitnoteclr.Value = clsCommon.ShowSelectForm("fndcreditdebitnoteclr", Qry, "Account_Code", " ControlAccount ='Y' ", fndcreditdebitnoteclr.Value, "Account_Code", isButtonClicked)
        rdtxtcreditdebitnoteclr.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + fndcreditdebitnoteclr.Value + "' ")

    End Sub

    Private Sub fndReserveStock__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndReserveStock._MYValidating
        Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
        fndReserveStock.Value = clsCommon.ShowSelectForm("fndReserveStock", Qry, "Account_Code", " ControlAccount ='Y' ", fndReserveStock.Value, "Account_Code", isButtonClicked)
        txtReserveStock.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + fndReserveStock.Value + "' ")

    End Sub

    Private Sub fndbreakageactcou__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean)
        Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
        fndbreakageglaccount.Value = clsCommon.ShowSelectForm("fndbreakageactcou", Qry, "Account_Code", " ControlAccount ='Y' ", fndbreakageglaccount.Value, "Account_Code", isButtonClicked)
        txtbreakage.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + fndbreakageglaccount.Value + "' ")

    End Sub

    Private Sub fndbreakageglaccount__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndbreakageglaccount._MYValidating
        Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
        fndbreakageglaccount.Value = clsCommon.ShowSelectForm("fndbreakageactcou", Qry, "Account_Code", " ControlAccount ='Y' ", fndbreakageglaccount.Value, "Account_Code", isButtonClicked)
        txtbreakage.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + fndbreakageglaccount.Value + "' ")
    End Sub

    Private Sub fndWIPAcc__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndWIPAcc._MYValidating
        Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
        fndWIPAcc.Value = clsCommon.ShowSelectForm("fndWIPAcc", Qry, "Account_Code", " ControlAccount ='Y' ", fndWIPAcc.Value, "Account_Code", isButtonClicked)
        rdtxtWIPAcc.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + fndWIPAcc.Value + "' ")
    End Sub

    Private Sub fndRMCons__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndRMCons._MYValidating
        Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
        fndRMCons.Value = clsCommon.ShowSelectForm("fndRMCons", Qry, "Account_Code", " ControlAccount ='Y' ", fndRMCons.Value, "Account_Code", isButtonClicked)
        rdtxtRMCons.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + fndRMCons.Value + "' ")
    End Sub

    Private Sub fndOther1__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndOther1._MYValidating
        Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
        fndOther1.Value = clsCommon.ShowSelectForm("fndOther1", Qry, "Account_Code", " ControlAccount ='Y' ", fndOther1.Value, "Account_Code", isButtonClicked)
        rdtxtOther1.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + fndOther1.Value + "' ")
    End Sub

    Private Sub fndOther2__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndOther2._MYValidating
        Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
        fndOther2.Value = clsCommon.ShowSelectForm("fndOther2", Qry, "Account_Code", " ControlAccount ='Y' ", fndOther2.Value, "Account_Code", isButtonClicked)
        rdtxtOther2.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + fndOther2.Value + "' ")
    End Sub

    Private Sub txtPurchaseCTRL_Ac_MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtPurchaseCTRL_Ac._MYValidating
        Dim qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS"
        txtPurchaseCTRL_Ac.Value = clsCommon.ShowSelectForm("PCA@PurchaseAcSetting", qry, "Account_Code", " ControlAccount ='Y' ", txtPurchaseCTRL_Ac.Value, "Account_Code", isButtonClicked)
        txtPurchaseCtrlAcDesc.Text = FillAccountDesc(txtPurchaseCTRL_Ac.Value)
    End Sub

    Private Sub FndJobWork_MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles FndJobWork._MYValidating
        Dim qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS"
        FndJobWork.Value = clsCommon.ShowSelectForm("IJW@PurchaseAcSetting", qry, "Account_Code", "ControlAccount ='Y'", FndJobWork.Value, "Account_Code", isButtonClicked)
        LblJobwork.Text = FillAccountDesc(FndJobWork.Value)
    End Sub

    Private Function FillAccountDesc(ByVal strAcCode As String) As String
        Dim qry As String = "select description from tspl_gl_accounts where account_code='" & strAcCode & "'"
        Return clsDBFuncationality.getSingleValue(qry)
    End Function

    Private Sub txtStockTransferIn__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtStockTransferIn._MYValidating
        Dim qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS"
        txtStockTransferIn.Value = clsCommon.ShowSelectForm("STA@PurchaseAcSetting", qry, "Account_Code", "ControlAccount ='Y'", txtStockTransferIn.Value, "Account_Code", isButtonClicked)
        lblStockTransferIn.Text = FillAccountDesc(txtStockTransferIn.Value)
    End Sub

    Private Sub FndStockTransferAccount__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles FndStockTransferAccount._MYValidating
        Dim qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS"
        FndStockTransferAccount.Value = clsCommon.ShowSelectForm("STA@PurchaseAcSetting", qry, "Account_Code", "ControlAccount ='Y'", FndStockTransferAccount.Value, "Account_Code", isButtonClicked)
        TxtStockTransferAccount.Text = FillAccountDesc(FndStockTransferAccount.Value)
    End Sub


    Private Sub txtProvisionClearing__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtProvisionClearing._MYValidating
        Dim qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS"
        txtProvisionClearing.Value = clsCommon.ShowSelectForm("STA@PurchaseAcSetting", qry, "Account_Code", "ControlAccount ='Y'", txtProvisionClearing.Value, "Account_Code", isButtonClicked)
        lblProvisioinClearing.Text = FillAccountDesc(txtProvisionClearing.Value)
    End Sub

    Private Sub txtChiilingCharges__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtChiilingCharges._MYValidating
        Dim qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS"
        txtChiilingCharges.Value = clsCommon.ShowSelectForm("CCh@PurchaseAcSetting", qry, "Account_Code", "ControlAccount ='Y'", txtChiilingCharges.Value, "Account_Code", isButtonClicked)
        lblChiilingCharges.Text = FillAccountDesc(txtChiilingCharges.Value)
    End Sub

    Private Sub txtFreightCharges__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtFreightCharges._MYValidating
        Dim qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS"
        txtFreightCharges.Value = clsCommon.ShowSelectForm("FCH@PurchaseAcSetting", qry, "Account_Code", "ControlAccount ='Y'", txtFreightCharges.Value, "Account_Code", isButtonClicked)
        lblFreightCharges.Text = FillAccountDesc(txtFreightCharges.Value)
    End Sub


    Private Sub txtPurchaseJobWork__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtPurchaseJobWork._MYValidating
        Dim qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS"
        txtPurchaseJobWork.Value = clsCommon.ShowSelectForm("FCH@PurchaseJobWork", qry, "Account_Code", "ControlAccount ='Y'", txtPurchaseJobWork.Value, "Account_Code", isButtonClicked)
        lblPurchaseJobwork.Text = FillAccountDesc(txtPurchaseJobWork.Value)
    End Sub

    Private Sub txtDifferenceAccount__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDifferenceAccount._MYValidating
        Dim qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS"
        txtDifferenceAccount.Value = clsCommon.ShowSelectForm("DIF@PurchaseJobWork", qry, "Account_Code", "ControlAccount ='Y'", txtDifferenceAccount.Value, "Account_Code", isButtonClicked)
        lblDifferenceAccount.Text = FillAccountDesc(txtDifferenceAccount.Value)
    End Sub

    Private Sub fndHandlingCharge__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndHandlingCharge._MYValidating
        Dim qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS"
        fndHandlingCharge.Value = clsCommon.ShowSelectForm("DIF@fndHandlingCharge", qry, "Account_Code", "ControlAccount ='Y'", fndHandlingCharge.Value, "Account_Code", isButtonClicked)
        txtHandlingCharge.Text = FillAccountDesc(fndHandlingCharge.Value)
    End Sub

    Private Sub fndEMP__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndEMP._MYValidating
        Dim qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS"
        fndEMP.Value = clsCommon.ShowSelectForm("DIF@fndEMP", qry, "Account_Code", "ControlAccount ='Y'", fndEMP.Value, "Account_Code", isButtonClicked)
        txtEMP.Text = FillAccountDesc(fndEMP.Value)
    End Sub

    Private Sub txtStockTransferJobWork__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtStockTransferJobWork._MYValidating
        Dim qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS"
        txtStockTransferJobWork.Value = clsCommon.ShowSelectForm("FCH@StockTransferJobWork", qry, "Account_Code", "ControlAccount ='Y'", txtStockTransferJobWork.Value, "Account_Code", isButtonClicked)
        lblStockTransferJobWorkDesc.Text = FillAccountDesc(txtStockTransferJobWork.Value)
    End Sub


    Private Sub fndStoreConsumptionAcc__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndStoreConsumptionAcc._MYValidating
        Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
        fndStoreConsumptionAcc.Value = clsCommon.ShowSelectForm("fndInventoryControl", Qry, "Account_Code", " ControlAccount ='Y' ", fndStoreConsumptionAcc.Value, "Account_Code", isButtonClicked)
        txtStoreConsumtion.Text = FillAccountDesc(fndStoreConsumptionAcc.Value)
    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(fndaccountsetcode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select Account Set", Me.Text)
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowHistoryData(fndaccountsetcode.Value, "Purchase_Class_Code", "TSPL_PURCHASE_ACCOUNTS")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub fndFAAccount__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndFAAccount._MYValidating
        Dim qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS"
        fndFAAccount.Value = clsCommon.ShowSelectForm("FndFAAccount", qry, "Account_Code", "ControlAccount ='Y'", fndFAAccount.Value, "Account_Code", isButtonClicked)
        lblFaAccountDes.Text = FillAccountDesc(fndFAAccount.Value)
    End Sub

    Private Sub fndPurchaseLoss__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndPurchaseLoss._MYValidating
        ' Ticket No : BHA/26/06/18-000089 By Prabhakar
        Dim qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS"
        fndPurchaseLoss.Value = clsCommon.ShowSelectForm("FndPurchaseLoss", qry, "Account_Code", "ControlAccount ='Y'", fndPurchaseLoss.Value, "Account_Code", isButtonClicked)
        txtPurchaseLoss.Text = FillAccountDesc(fndPurchaseLoss.Value)
    End Sub

    Private Sub fndItemOpeningClearing__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndItemOpeningClearing._MYValidating
        Dim qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS"
        fndItemOpeningClearing.Value = clsCommon.ShowSelectForm("FndItemOpeningClearing", qry, "Account_Code", "ControlAccount ='Y'", fndItemOpeningClearing.Value, "Account_Code", isButtonClicked)
        lblItemOpeningClearing.Text = FillAccountDesc(fndItemOpeningClearing.Value)
    End Sub
End Class
