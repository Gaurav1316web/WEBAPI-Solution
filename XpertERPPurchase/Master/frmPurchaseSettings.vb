'=============BM00000003058,updated by Rohit,Create new Setting(Return Without Invoice).=============
'---------------------'---------------------BM00000003305
''updation by richa agarwal against ticket no. BM00000003876 on 13/11/2014
''CHANGES BY RICHA AGARWAL TICKET NO. BM00000006551 ON 14/05/2015
Imports common
Imports System.Data.SqlClient
Imports common.Controls
'''' <summary>
'''' ''''''''''''''''''''''''''''''''BM00000002272''''''''''''''''''
'''' BM00000003054'''''''''''''09/07/2014
'''' BM00000003051'''''''''''''09/07/2014
'''' </summary>
'''' <remarks></remarks>
Public Class frmPurchaseSettings
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False

    Private Sub frmPurchaseSettings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isNewEntry = True
        ButtonToolTip.SetToolTip(rdbtnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(rdbtnclose, "Press Alt+C Close the Window")

        SetControlsTag()
        '--------------richa 09/07/2014 Ticket No BM00000003045---------
        LoadNotificationSettinginPO()
        '----------------------------------------------------------
        '--------------richa 09/07/2014 Ticket No BM00000003042---------
        LoadNotificationSettinginPurchaseRequisition()
        '----------------------------------------------------------
        LoadData()

    End Sub

    Sub SetControlsTag()
        chkSRNPrintQtywise.Tag1 = clsFixedParameterCode.SRNReportQuantityWise
        chkSRNPrintQtywise.Tag = clsFixedParameterType.SRNReportQuantityWise

        chkQCColumnAddedonMRN.Tag1 = clsFixedParameterCode.IsQCColumnRequiredonMRN
        chkQCColumnAddedonMRN.Tag = clsFixedParameterType.IsQCColumnRequiredonMRN

        chkRemarkReasononPO.Tag1 = clsFixedParameterCode.IsRemarkReasonMandatoryOnPO
        chkRemarkReasononPO.Tag = clsFixedParameterType.IsRemarkReasonMandatoryOnPO

        chkAllstructurewiseItem.Tag1 = clsFixedParameterCode.ShowItemAllStructureWise
        chkAllstructurewiseItem.Tag = clsFixedParameterType.ShowItemAllStructureWise

        ChkShowCostCenterAndHierarchyLevelInPurchaseModule.Tag1 = clsFixedParameterCode.ShowCostCenterAndHierarchyLevelInPurchaseModule
        ChkShowCostCenterAndHierarchyLevelInPurchaseModule.Tag = clsFixedParameterType.ShowCostCenterAndHierarchyLevelInPurchaseModule


        chkPickItemFromVendorItemDetails.Tag1 = clsFixedParameterCode.PurchasePickItemFromVendorItemDetails
        chkPickItemFromVendorItemDetails.Tag = clsFixedParameterType.PurchasePickItemFromVendorItemDetails
        chkOneItemOneVendor.Tag1 = clsFixedParameterCode.PurchaseOneItemOneVendor
        chkOneItemOneVendor.Tag = clsFixedParameterType.PurchaseOneItemOneVendor

        chkmailoff.Tag1 = clsFixedParameterCode.MAILOFF
        chkmailoff.Tag = clsFixedParameterType.MAILOFF

        chkDisableShipToLocation.Tag1 = clsFixedParameterCode.DisableShipToLocation
        chkDisableShipToLocation.Tag = clsFixedParameterType.DisableShipToLocation

        chkAllowLargerItemCost.Tag1 = clsFixedParameterCode.AllowLargerItemCostThenVendorItemCost
        chkAllowLargerItemCost.Tag = clsFixedParameterType.AllowLargerItemCostThenVendorItemCost


        ChkSkipMRNGRNinCaseofMT.Tag1 = clsFixedParameterCode.SkipMRNGRNinCaseofMT
        ChkSkipMRNGRNinCaseofMT.Tag = clsFixedParameterType.SkipMRNGRNinCaseofMT

        ChkAutoGenerateMRN.Tag1 = clsFixedParameterCode.AutoGenerateMRN
        ChkAutoGenerateMRN.Tag = clsFixedParameterType.AutoGenerateMRN

        chkGRN.Tag1 = clsFixedParameterCode.ShowGRN
        chkGRN.Tag = clsFixedParameterType.ShowGRN

        chkMRN.Tag1 = clsFixedParameterCode.ShowMRN
        chkMRN.Tag = clsFixedParameterType.ShowMRN

        chkEnableProjectFinder.Tag1 = clsFixedParameterCode.EnableProjectFinder
        chkEnableProjectFinder.Tag = clsFixedParameterType.EnableProjectFinder

       

        chkstd_rate.Tag1 = clsFixedParameterCode.STDPURRATE
        chkstd_rate.Tag = clsFixedParameterType.STDPURRATE

        chkautoPO.Tag1 = clsFixedParameterCode.AutoPOAtSRN
        chkautoPO.Tag = clsFixedParameterType.AutoPOAtSRN

        chkmccPO.Tag1 = clsFixedParameterCode.MCCPurchase
        chkmccPO.Tag = clsFixedParameterType.MCCPurchase


        chkPurchaseOrderItemQtyBelow.Tag1 = clsFixedParameterCode.PurchaseOrderAutomaticallyItemQtyBelowReorderLevel
        chkPurchaseOrderItemQtyBelow.Tag = clsFixedParameterType.PurchaseOrderAutomaticallyItemQtyBelowReorderLevel

        chkVendor_Nlevel.Tag1 = clsFixedParameterCode.NLevelAtVendor
        chkVendor_Nlevel.Tag = clsFixedParameterType.NLevelAtVendor

        ''richa 28/08/2014
        ChkInvoiceBasedPO.Tag1 = clsFixedParameterCode.InvoiceBasedPO
        ChkInvoiceBasedPO.Tag = clsFixedParameterType.InvoiceBasedPO
        ''-----------------------------
        'chkSRNRejected.Tag1 = clsFixedParameterCode.SRNRejectedQty
        'chkSRNRejected.Tag = clsFixedParameterType.SRNRejectedQty

        chksmsatpost.Tag1 = clsFixedParameterCode.Purchase_SMSATPOST
        chksmsatpost.Tag = clsFixedParameterType.Purchase_SMSATPOST

        chkRFQ.Tag1 = clsFixedParameterCode.showRFQ
        chkRFQ.Tag = clsFixedParameterType.showRFQ

        chkShowInvoiceInPOfinder.Tag1 = clsFixedParameterCode.ShowSaleInvoiceNoInPOfinderInSRN
        chkShowInvoiceInPOfinder.Tag = clsFixedParameterType.ShowSaleInvoiceNoInPOfinderInSRN

        chkPOInvCreateDebitNoteForRejectAndShort.Tag1 = clsFixedParameterCode.CreateDbitNoteForShortPI
        chkPOInvCreateDebitNoteForRejectAndShort.Tag = clsFixedParameterType.CreateDbitNoteForShortPI

        chkPOInvCreateDebitNoteForReject.Tag1 = clsFixedParameterCode.CreateDbitNoteForRejectPI
        chkPOInvCreateDebitNoteForReject.Tag = clsFixedParameterType.CreateDbitNoteForRejectPI

        chkPI_debitnot_unitcost.Tag1 = clsFixedParameterCode.CreateDebitNoteForUnitCost
        chkPI_debitnot_unitcost.Tag = clsFixedParameterType.CreateDebitNoteForUnitCost

        '' Anubhooti 02-Dec-2014 (Setting For Unit Cost Editable/Non-Editable On SRN)
        ChkRateEditSRN.Tag1 = clsFixedParameterCode.IsRateEditableOnSRN
        ChkRateEditSRN.Tag = clsFixedParameterType.IsRateEditableOnSRN
        '' Anubhooti 29-Jan-2015 (Setting For Unit Cost Editable/Non-Editable On Issue/Return/Transfer)
        ChkCostEditIssue.Tag1 = clsFixedParameterCode.IsCostEditableOnIssueReturnTransfer
        ChkCostEditIssue.Tag = clsFixedParameterType.IsCostEditableOnIssueReturnTransfer
        '' Anubhooti 23-Jan-2015 (Setting For Creation of GL Acc To Item GL Account(Issue/Return/Transfer))
        ChkGLAccToItem.Tag1 = clsFixedParameterCode.CreateGLAccToItem
        ChkGLAccToItem.Tag = clsFixedParameterType.CreateGLAccToItem
        ''

        chkShortageIncludeInLandedCost.Tag1 = clsFixedParameterCode.IsShortageIncludeInLandedCost
        chkShortageIncludeInLandedCost.Tag = clsFixedParameterType.IsShortageIncludeInLandedCost
    End Sub
    '' Anubhooti 12-Mar-2015 (Fetch Alies Name On Vendor Finder)
    Private Sub txtVendorNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtVendorNo._MYValidating
        Dim qry As String = ""
        Dim whrCls As String = ""
        qry = "select Vendor_Code as Code,Vendor_Name as Name,ISNULL(alies_name,'') As [Alies Name],TSPL_VENDOR_MASTER.add1 +case when len(TSPL_VENDOR_MASTER.add2)>0 then ', '+TSPL_VENDOR_MASTER.add2 else '' end +case when LEN(isnull(TSPL_VENDOR_MASTER.Add3,''))>0 then ', '+isnull(TSPL_VENDOR_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_VENDOR_MASTER.City_Code_Desc)>0 then ', '+TSPL_VENDOR_MASTER.City_Code_Desc else ' ' end + case when len(TSPL_VENDOR_MASTER.State )>0 then TSPL_VENDOR_MASTER.State else '' end  as Address,Terms_Code as [Term Code] ,Terms_Code_Desc as [Term Description] ,Tax_Group as [Tax Group],Tax_Group_Desc as [Tax Group Description] from TSPL_VENDOR_MASTER "
        txtVendorNo.Value = clsCommon.ShowSelectForm("POVendorFndr", qry, "Code", whrCls, txtVendorNo.Value, "Code", isButtonClicked)
      
    End Sub

#Region "Functions"

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmPurchaseSetting)
        If Not (MyBase.isReadFlag) Then
            '--------------richa 15/07/2014 Ticket No BM00000003124---------
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If
        rdbtnsave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag

    End Sub

    Sub LoadData()

        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As clsPurchaseSettings = clsPurchaseSettings.GetData()
            If obj IsNot Nothing Then
                isNewEntry = False
                If obj.CREATE_ABATEMENT_BASED_PO = True Then
                    Chkabatement.Checked = True
                Else
                    Chkabatement.Checked = False
                End If

                If obj.CREATE_PO_WITH_REQ = True Then
                    chkPOReq.Checked = True
                Else
                    chkPOReq.Checked = False
                End If
                If obj.ENABLE_POPUP_REORDERLEVEL = True Then
                    chkItemReorderLevel.Checked = True
                Else
                    chkItemReorderLevel.Checked = False
                End If

                If obj.MANDATE_BATCHNO_RM = True Then
                    chkRMBatch.Checked = True
                Else
                    chkRMBatch.Checked = False
                End If

                If obj.MANDATE_BATCHNO_FG = True Then
                    chkFGBatch.Checked = True
                Else
                    chkFGBatch.Checked = False
                End If

                If obj.MANDATE_BATCHNO_ASSET = True Then
                    chkAssetBatch.Checked = True
                Else
                    chkAssetBatch.Checked = False
                End If

                If obj.MANDATE_BATCHNO_OTHERS = True Then
                    chkOtherItemsBatch.Checked = True
                Else
                    chkOtherItemsBatch.Checked = False
                End If

                If obj.MANDATE_MFG_RM = True Then
                    chkRMmfgDate.Checked = True
                Else
                    chkRMmfgDate.Checked = False
                End If

                If obj.MANDATE_MFG_FG = True Then
                    chkFGmfgDate.Checked = True
                Else
                    chkFGmfgDate.Checked = False
                End If

                If obj.MANDATE_MFG_ASSET = True Then
                    chkAssetmfgDate.Checked = True
                Else
                    chkAssetmfgDate.Checked = False
                End If

                If obj.MANDATE_MFG_OTHERS = True Then
                    chkOtherItemsmfgDate.Checked = True
                Else
                    chkOtherItemsmfgDate.Checked = False
                End If




                If obj.MANDATE_EXP_RM = True Then
                    chkRMexpDate.Checked = True
                Else
                    chkRMexpDate.Checked = False
                End If

                If obj.MANDATE_EXP_FG = True Then
                    chkFGexpDate.Checked = True
                Else
                    chkFGexpDate.Checked = False
                End If

                If obj.MANDATE_EXP_ASSET = True Then
                    chkAssetexpDate.Checked = True
                Else
                    chkAssetexpDate.Checked = False
                End If

                If obj.MANDATE_EXP_OTHERS = True Then
                    chkOtherItemsexpDate.Checked = True
                Else
                    chkOtherItemsexpDate.Checked = False
                End If

                If obj.PurchaseOrderAutomaticallyItemQtyBelowReorderLevel = True Then
                    chkPurchaseOrderItemQtyBelow.Checked = True
                Else
                    chkPurchaseOrderItemQtyBelow.Checked = False
                End If
                '' Anubhooti 12-Nov-2014 BM00000003662
                txtJobWork.Value = clsCommon.myCstr(obj.Job_Work_Account)
                If clsCommon.myCstr(clsCommon.myLen(txtJobWork.Value)) > 0 Then
                    Dim query1 As String = "Select description from tspl_gl_accounts where account_code ='" + clsCommon.myCstr(txtJobWork.Value) + "'"
                    LblJobWork.Text = clsDBFuncationality.getSingleValue(query1)
                Else
                    LblJobWork.Text = ""
                End If
                TxtSRNLim.Value = clsCommon.myCdbl(obj.SRN_Limit)
                TxtGRNLim.Value = clsCommon.myCdbl(obj.GRN_Limit)

                For Each ctrl As Control In RadGroupBox1.Controls
                    If ctrl.GetType Is GetType(MyCheckBox) Then
                        Dim chkBox As MyCheckBox = TryCast(ctrl, MyCheckBox)
                        If clsCommon.myLen(chkBox.Tag) >= 0 AndAlso clsCommon.myLen(chkBox.Tag1) >= 0 Then
                            chkBox.Checked = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(chkBox.Tag, chkBox.Tag1, Nothing)) = 1, True, False)
                        End If
                    End If
                Next


                 

                chkAllowLargerItemCost.Enabled = chkPickItemFromVendorItemDetails.Checked
                If chkAllowLargerItemCost.Enabled = False Then
                    chkAllowLargerItemCost.Checked = False
                End If


                If obj.REQUIRED_SECURITY_AMOUNT = True Then
                    chkRequiredSecurityAmt.Checked = True
                Else
                    chkRequiredSecurityAmt.Checked = False
                End If


                If obj.REQUIRED_FOC = True Then
                    chkRequiredFOC.Checked = True
                Else
                    chkRequiredFOC.Checked = False
                End If

                '============Rohit=========================
                If obj.Return_Without_Invoice = True Then
                    chkReturnWithoutInvoice.Checked = True
                Else
                    chkReturnWithoutInvoice.Checked = False
                End If

                If obj.SRN_Rejected_Store = True Then
                    chkSRNRejected.Checked = True
                Else
                    chkSRNRejected.Checked = False
                End If
                '=================================================
                '--------------richa 09/07/2014 Ticket No BM00000003045---------
                Try
                    Dim StrNotificationSettingInPO As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from tspl_fixed_parameter where type='" + clsFixedParameterType.NotificationSettingforReOrderInPO + "' and code='" + clsFixedParameterCode.NotificationSettingforReOrderInPO + "'"))

                    If clsCommon.myLen(StrNotificationSettingInPO) > 0 Then
                        If clsCommon.CompairString(clsCommon.myCstr(StrNotificationSettingInPO), "0") = CompairStringResult.Equal Then
                            cboNoticationSettingInPO.Text = "None"
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(StrNotificationSettingInPO), "1") = CompairStringResult.Equal Then
                            cboNoticationSettingInPO.Text = "Stop"
                        Else
                            cboNoticationSettingInPO.Text = "Warning"
                        End If
                    End If
                Catch ex As Exception
                End Try
                '---------------------------------------------------------------
                '--------------richa 09/07/2014 Ticket No BM00000003042---------
                Try
                    Dim StrNotificationSettingInPurchaseRequisition As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from tspl_fixed_parameter where type='" + clsFixedParameterType.NotificationSettingforReOrderInPurchaseRequisition + "' and code='" + clsFixedParameterCode.NotificationSettingforReOrderInPurchaseRequisition + "'"))

                    If clsCommon.myLen(StrNotificationSettingInPurchaseRequisition) > 0 Then
                        If clsCommon.CompairString(clsCommon.myCstr(StrNotificationSettingInPurchaseRequisition), "0") = CompairStringResult.Equal Then
                            cboNoticationSettingInPurchaseRequisition.Text = "None"
                        Else
                            cboNoticationSettingInPurchaseRequisition.Text = "Warning"
                        End If
                    End If
                Catch ex As Exception
                End Try
                '---------------------------------------------------------------
                '' Anubhooti 24-Aug-2014 Demo Setting 
                '' --------------------------------------------------- Start Demo ---------------------------------------
                Dim StrIs_AbemdmentForDemo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from tspl_fixed_parameter where type='" + clsFixedParameterType.Is_AbemdmentForDemo + "' and code='" + clsFixedParameterCode.Is_AbemdmentForDemo + "'"))
                If clsCommon.myLen(StrIs_AbemdmentForDemo) > 0 Then
                    If clsCommon.CompairString(clsCommon.myCstr(StrIs_AbemdmentForDemo), "1") = CompairStringResult.Equal Then
                        chkDemoAmendment.Checked = True
                    Else
                        chkDemoAmendment.Checked = False
                    End If
                End If

                '' Anubhooti 28-Aug-2014 Demo Setting : Purchase Module
                Dim StrShowStatusForPurchase As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from tspl_fixed_parameter where type='" + clsFixedParameterType.ShowStatusForPurchase + "' and code='" + clsFixedParameterCode.ShowStatusForPurchase + "'"))
                If clsCommon.myLen(StrShowStatusForPurchase) > 0 Then
                    If clsCommon.CompairString(clsCommon.myCstr(StrShowStatusForPurchase), "1") = CompairStringResult.Equal Then
                        ChkShowStatusPur.Checked = True
                    Else
                        ChkShowStatusPur.Checked = False
                    End If
                End If

                '' ---------------------------------------------------------------------------------- End Demo ----------------------------------------

                '' priti 28-Aug-2014 Demo Setting : Purchase Module
                txtVendorNo.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from tspl_fixed_parameter where type='" + clsFixedParameterType.Principal_Vendor + "' and code='" + clsFixedParameterCode.Principal_Vendor + "'"))
                txtDatabaseName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from tspl_fixed_parameter where type='" + clsFixedParameterType.Principal_Vendor_Database + "' and code='" + clsFixedParameterCode.Principal_Vendor_Database + "'"))
                txtCustomer.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from tspl_fixed_parameter where type='" + clsFixedParameterType.Principal_Customer + "' and code='" + clsFixedParameterCode.Principal_Customer + "'"))

                '' ---------------------------------------------------------------------------------- End Demo ----------------------------------------
                '' Anubhooti 02-Dec-2014 (Setting For Unit Cost Editable/Non-Editable On SRN)
                Dim StrIsRateEditOnSRN As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from tspl_fixed_parameter where type='" + clsFixedParameterType.IsRateEditableOnSRN + "' and code='" + clsFixedParameterCode.IsRateEditableOnSRN + "'"))
                If clsCommon.myLen(StrIsRateEditOnSRN) > 0 Then
                    If clsCommon.CompairString(clsCommon.myCstr(StrIsRateEditOnSRN), "1") = CompairStringResult.Equal Then
                        ChkRateEditSRN.Checked = True
                    Else
                        ChkRateEditSRN.Checked = False
                    End If
                End If
            End If
            '' Anubhooti 29-Jan-2015 (Setting For Unit Cost Editable/Non-Editable On Issue/Return/Transfer)
            Dim StrIsCostEditOnIRT As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from tspl_fixed_parameter where type='" + clsFixedParameterType.IsCostEditableOnIssueReturnTransfer + "' and code='" + clsFixedParameterCode.IsCostEditableOnIssueReturnTransfer + "'"))
            If clsCommon.myLen(StrIsCostEditOnIRT) > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(StrIsCostEditOnIRT), "1") = CompairStringResult.Equal Then
                    ChkCostEditIssue.Checked = True
                Else
                    ChkRateEditSRN.Checked = False
                End If
            End If
            '' Anubhooti 23-Jan-2015 (Setting For Creation of GL Acc To Item GL Account(Issue/Return/Transfer))
            Dim StrItemGLAcc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from tspl_fixed_parameter where type='" + clsFixedParameterType.CreateGLAccToItem + "' and code='" + clsFixedParameterCode.CreateGLAccToItem + "'"))
            If clsCommon.myLen(StrItemGLAcc) > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(StrItemGLAcc), "1") = CompairStringResult.Equal Then
                    ChkGLAccToItem.Checked = True
                Else
                    ChkGLAccToItem.Checked = False
                End If
            End If

            chkApplySlab.Checked = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.PurchaseSlab, clsFixedParameterCode.ApplyRange, Nothing) = 1)
            Dim str As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.PurchaseSlab, clsFixedParameterCode.RangeNotApplicable, Nothing))
            If str.Contains("-") Then
                Dim strBreak As String() = str.Split(New String() {"-"}, StringSplitOptions.None)
                If strBreak.Length > 1 Then
                    txtSlab1From.Value = clsCommon.myCDecimal(strBreak(0))
                    txtSlab1To.Value = clsCommon.myCDecimal(strBreak(1))
                End If
            End If
            str = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.PurchaseSlab, clsFixedParameterCode.RangePO, Nothing))
            If str.Contains("-") Then
                Dim strBreak As String() = str.Split(New String() {"-"}, StringSplitOptions.None)
                If strBreak.Length > 1 Then
                    txtSlab2From.Value = clsCommon.myCDecimal(strBreak(0))
                    txtSlab2To.Value = clsCommon.myCDecimal(strBreak(1))
                End If
            End If
            str = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.PurchaseSlab, clsFixedParameterCode.RangeRAL, Nothing))
            If str.Contains("-") Then
                Dim strBreak As String() = str.Split(New String() {"-"}, StringSplitOptions.None)
                If strBreak.Length > 1 Then
                    txtSlab3From.Value = clsCommon.myCDecimal(strBreak(0))
                    txtSlab3To.Value = clsCommon.myCDecimal(strBreak(1))
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Sub SaveData()
        If MyBase.isModifyonPasswordFlag Then
            If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmPurchaseSetting, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
            Else
                Return
            End If
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If AllowToSave(trans) Then
                Dim obj As New clsPurchaseSettings()
                obj.CREATE_ABATEMENT_BASED_PO = clsCommon.myCstr(IIf(Chkabatement.Checked, "1", "0"))
                obj.CREATE_PO_WITH_REQ = clsCommon.myCstr(IIf(chkPOReq.Checked, "1", "0"))
                obj.ENABLE_POPUP_REORDERLEVEL = clsCommon.myCstr(IIf(chkItemReorderLevel.Checked, "1", "0"))


                obj.MANDATE_BATCHNO_RM = clsCommon.myCstr(IIf(chkRMBatch.Checked, "1", "0"))
                obj.MANDATE_BATCHNO_FG = clsCommon.myCstr(IIf(chkFGBatch.Checked, "1", "0"))
                obj.MANDATE_BATCHNO_ASSET = clsCommon.myCstr(IIf(chkAssetBatch.Checked, "1", "0"))
                obj.MANDATE_BATCHNO_OTHERS = clsCommon.myCstr(IIf(chkOtherItemsBatch.Checked, "1", "0"))

                obj.MANDATE_MFG_RM = clsCommon.myCstr(IIf(chkRMmfgDate.Checked, "1", "0"))
                obj.MANDATE_MFG_FG = clsCommon.myCstr(IIf(chkFGmfgDate.Checked, "1", "0"))
                obj.MANDATE_MFG_ASSET = clsCommon.myCstr(IIf(chkAssetmfgDate.Checked, "1", "0"))
                obj.MANDATE_MFG_OTHERS = clsCommon.myCstr(IIf(chkOtherItemsmfgDate.Checked, "1", "0"))

                obj.MANDATE_EXP_RM = clsCommon.myCstr(IIf(chkRMexpDate.Checked, "1", "0"))
                obj.MANDATE_EXP_FG = clsCommon.myCstr(IIf(chkFGexpDate.Checked, "1", "0"))
                obj.MANDATE_EXP_ASSET = clsCommon.myCstr(IIf(chkAssetexpDate.Checked, "1", "0"))
                obj.MANDATE_EXP_OTHERS = clsCommon.myCstr(IIf(chkOtherItemsexpDate.Checked, "1", "0"))

                obj.REQUIRED_SECURITY_AMOUNT = clsCommon.myCstr(IIf(chkRequiredSecurityAmt.Checked, "1", "0"))
                obj.REQUIRED_FOC = clsCommon.myCstr(IIf(chkRequiredFOC.Checked, "1", "0"))
                obj.Return_Without_Invoice = clsCommon.myCstr(IIf(chkReturnWithoutInvoice.Checked, "1", "0"))
                obj.PurchaseOrderAutomaticallyItemQtyBelowReorderLevel = clsCommon.myCstr(IIf(chkPurchaseOrderItemQtyBelow.Checked, "1", "0"))
                obj.SRN_Rejected_Store = clsCommon.myCstr(IIf(chkSRNRejected.Checked, "1", "0"))
                '' Anubhooti 12-Nov-2014 BM00000003662
                obj.Job_Work_Account = clsCommon.myCstr(txtJobWork.Value)
                obj.SRN_Limit = clsCommon.myCdbl(TxtSRNLim.Value)
                obj.GRN_Limit = clsCommon.myCdbl(TxtGRNLim.Value)
                If (clsPurchaseSettings.SaveData(obj, trans)) Then

                    For Each ctrl As Control In RadGroupBox1.Controls
                        If ctrl.GetType Is GetType(MyCheckBox) Then
                            Dim chkBox As MyCheckBox = TryCast(ctrl, MyCheckBox)
                            Dim ddate As Date = Nothing
                            If clsCommon.myLen(chkBox.Tag) >= 0 AndAlso clsCommon.myLen(chkBox.Tag1) >= 0 Then
                                clsFixedParameter.UpdateData(chkBox.Tag, chkBox.Tag1, IIf(chkBox.Checked, "1", "0"), trans)
                            End If

                        End If
                    Next


                    'clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                    'LoadData()


                End If

                '--------------richa 09/07/2014 Ticket No BM00000003045---------
                Dim StrNotificationSettingInPO As String = 0
                If clsCommon.CompairString(clsCommon.myCstr(cboNoticationSettingInPO.SelectedValue).ToUpper(), "NONE") = CompairStringResult.Equal Then
                    StrNotificationSettingInPO = 0
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboNoticationSettingInPO.SelectedValue).ToUpper(), "STOP") = CompairStringResult.Equal Then
                    StrNotificationSettingInPO = 1
                Else
                    StrNotificationSettingInPO = 2
                End If
                clsDBFuncationality.ExecuteNonQuery("Update TSPL_FIXED_PARAMETER Set description='" + StrNotificationSettingInPO + "' where TYPE='" + clsFixedParameterType.NotificationSettingforReOrderInPO + "' and Code='" + clsFixedParameterCode.NotificationSettingforReOrderInPO + "'", trans)
                '---------------------------------------------------------------
                '--------------richa 09/07/2014 Ticket No BM00000003045---------
                Dim StrNotificationSettingInPurchaseRequisition As String = 0
                If clsCommon.CompairString(clsCommon.myCstr(cboNoticationSettingInPurchaseRequisition.SelectedValue).ToUpper(), "NONE") = CompairStringResult.Equal Then
                    StrNotificationSettingInPurchaseRequisition = 0
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboNoticationSettingInPurchaseRequisition.SelectedValue).ToUpper(), "STOP") = CompairStringResult.Equal Then
                    StrNotificationSettingInPurchaseRequisition = 1
                Else
                    StrNotificationSettingInPurchaseRequisition = 2
                End If
                clsDBFuncationality.ExecuteNonQuery("Update TSPL_FIXED_PARAMETER Set description='" + StrNotificationSettingInPurchaseRequisition + "' where TYPE='" + clsFixedParameterType.NotificationSettingforReOrderInPurchaseRequisition + "' and Code='" + clsFixedParameterCode.NotificationSettingforReOrderInPurchaseRequisition + "'", trans)
                '--------------------------------------------------------------- Start Demo --------------------------------------------------------------
                '' Anubhooti 24-Aug-2014 Demo Setting
                Dim StrIs_AbemdmentForDemo As String = 0
                If chkDemoAmendment.Checked = True Then
                    StrIs_AbemdmentForDemo = 1
                Else
                    StrIs_AbemdmentForDemo = 0
                End If
                clsDBFuncationality.ExecuteNonQuery("Update TSPL_FIXED_PARAMETER Set description='" + StrIs_AbemdmentForDemo + "' where TYPE='" + clsFixedParameterType.Is_AbemdmentForDemo + "' and Code='" + clsFixedParameterCode.Is_AbemdmentForDemo + "'", trans)

                '' Anubhooti 28-Aug-2014 Demo Setting : Purchase Module
                Dim StrShowStatusForPurchase As String = 0
                If ChkShowStatusPur.Checked = True Then
                    StrShowStatusForPurchase = 1
                Else
                    StrShowStatusForPurchase = 0
                End If
                clsDBFuncationality.ExecuteNonQuery("Update TSPL_FIXED_PARAMETER Set description='" + StrShowStatusForPurchase + "' where TYPE='" + clsFixedParameterType.ShowStatusForPurchase + "' and Code='" + clsFixedParameterCode.ShowStatusForPurchase + "'", trans)


                '--------------------------------------------------------------- End Demo----------------------------------------------------

                '' Priti 28-Aug-2014 Demo Setting : Purchase Module

                clsDBFuncationality.ExecuteNonQuery("Update TSPL_FIXED_PARAMETER Set description='" + txtVendorNo.Value + "' where TYPE='" + clsFixedParameterType.Principal_Vendor + "' and Code='" + clsFixedParameterCode.Principal_Vendor + "'", trans)
                clsDBFuncationality.ExecuteNonQuery("Update TSPL_FIXED_PARAMETER Set description='" + txtDatabaseName.Text + "' where TYPE='" + clsFixedParameterType.Principal_Vendor_Database + "' and Code='" + clsFixedParameterCode.Principal_Vendor_Database + "'", trans)
                clsDBFuncationality.ExecuteNonQuery("Update TSPL_FIXED_PARAMETER Set description='" + txtCustomer.Text + "' where TYPE='" + clsFixedParameterType.Principal_Customer + "' and Code='" + clsFixedParameterCode.Principal_Customer + "'", trans)

                '--------------------------------------------------------------- End Demo----------------------------------------------------
                '===for req setting
                Dim CreatePOWithReq As String = 0
                If chkPOReq.Checked = True Then
                    CreatePOWithReq = 1
                Else
                    CreatePOWithReq = 0
                End If
                clsDBFuncationality.ExecuteNonQuery("Update TSPL_FIXED_PARAMETER Set description='" + CreatePOWithReq + "' where TYPE='" + clsFixedParameterType.CreatePOWithRequisition + "' and Code='" + clsFixedParameterCode.POWITHREQ + "'", trans)
            End If

            If chkmailoff.Checked = True Then
                objCommonVar.IsMailSend = True
            Else
                objCommonVar.IsMailSend = False
            End If

            'If chkVendor_Nlevel.Checked Then
            '    MDI.IsCustomer_NLevel = "YES"
            'ElseIf Not chkVendor_Nlevel.Checked Then
            '    MDI.IsCustomer_NLevel = "NO"
            'End If
            clsFixedParameter.UpdateData(clsFixedParameterType.PurchaseSlab, clsFixedParameterCode.ApplyRange, IIf(chkApplySlab.Checked, "1", "0"), trans)
            clsFixedParameter.UpdateData(clsFixedParameterType.PurchaseSlab, clsFixedParameterCode.RangeNotApplicable, clsCommon.myCstr(txtSlab1From.Value) + "-" + clsCommon.myCstr(txtSlab1To.Value), trans)
            clsFixedParameter.UpdateData(clsFixedParameterType.PurchaseSlab, clsFixedParameterCode.RangePO, clsCommon.myCstr(txtSlab2From.Value) + "-" + clsCommon.myCstr(txtSlab2To.Value), trans)
            clsFixedParameter.UpdateData(clsFixedParameterType.PurchaseSlab, clsFixedParameterCode.RangeRAL, clsCommon.myCstr(txtSlab3From.Value) + "-" + clsCommon.myCstr(txtSlab3To.Value), trans)


            trans.Commit()
            clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Private Function AllowToSave(ByVal trans As SqlTransaction) As Boolean
        If chkApplySlab.Checked Then
            If txtSlab1To.Value < txtSlab1From.Value Then
                Throw New Exception("Doument Not Required From Range can't be more than To Range")
            End If
            txtSlab2From.Value = txtSlab1To.Value + 0.01
            If txtSlab2To.Value < txtSlab2From.Value Then
                Throw New Exception("PO Mandatory From Range can't be more than To Range")
            End If
            txtSlab3From.Value = txtSlab2To.Value + 0.01
            If txtSlab3To.Value < txtSlab3From.Value Then
                Throw New Exception("RAL Mandatory From Range can't be more than To Range")
            End If
        Else
            txtSlab1From.Value = 0.00
            txtSlab1To.Value = 10000

            txtSlab2From.Value = txtSlab1To.Value + 0.01
            txtSlab2To.Value = 100000

            txtSlab3From.Value = txtSlab2To.Value + 0.01
            txtSlab3To.Value = 999999999999
        End If

        Return True
    End Function

#End Region
#Region "Finders"

#End Region
#Region "EVENTS"
    Private Sub frmPurchaseSettings_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso rdbtnsave.Enabled Then
            SaveData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub
#End Region

    Private Sub rdbtnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnclose.Click
        Me.Close()
    End Sub

    Private Sub rdbtnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnsave.Click
        SaveData()
    End Sub


    Private Sub chkPickItemFromVendorItemDetails_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkPickItemFromVendorItemDetails.ToggleStateChanged
        chkAllowLargerItemCost.Enabled = chkPickItemFromVendorItemDetails.Checked
        If chkAllowLargerItemCost.Enabled = False Then
            chkAllowLargerItemCost.Checked = False
        End If

    End Sub

    
    '--------------richa 09/07/2014 Ticket No BM00000003045---------
    Sub LoadNotificationSettinginPO()
        Dim dt1 As DataTable = New DataTable()
        dt1.Columns.Add("Code", GetType(String))
        dt1.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt1.NewRow()
        dr("Code") = "None"
        dr("Name") = "None"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Stop"
        dr("Name") = "Stop"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Warning"
        dr("Name") = "Warning"
        dt1.Rows.Add(dr)

        cboNoticationSettingInPO.DataSource = dt1
        cboNoticationSettingInPO.DisplayMember = "Name"
        cboNoticationSettingInPO.ValueMember = "Code"
    End Sub
    '---------------------------------------------------------------------------
    '--------------richa 09/07/2014 Ticket No BM00000003042---------
    Sub LoadNotificationSettinginPurchaseRequisition()
        Dim dt1 As DataTable = New DataTable()
        dt1.Columns.Add("Code", GetType(String))
        dt1.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt1.NewRow()
        dr("Code") = "None"
        dr("Name") = "None"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Stop"
        dr("Name") = "Stop"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Warning"
        dr("Name") = "Warning"
        dt1.Rows.Add(dr)

        cboNoticationSettingInPurchaseRequisition.DataSource = dt1
        cboNoticationSettingInPurchaseRequisition.DisplayMember = "Name"
        cboNoticationSettingInPurchaseRequisition.ValueMember = "Code"
    End Sub

    Private Sub RadGroupBox1_Click(sender As Object, e As EventArgs) Handles RadGroupBox1.Click

    End Sub

    Private Sub txtSlab1To_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtSlab1To.Validating
        txtSlab2From.Value = txtSlab1To.Value + 0.01
    End Sub

    Private Sub txtSlab2To_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtSlab2To.Validating
        txtSlab3From.Value = txtSlab2To.Value + 0.01
    End Sub
    '---------------------------------------------------------------------------

    Private Sub txtJobWork__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtJobWork._MYValidating
        Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
        txtJobWork.Value = clsCommon.ShowSelectForm("txtjobwork", Qry, "Account_Code", " ControlAccount ='Y' ", txtJobWork.Value, "Account_Code", isButtonClicked)
        LblJobWork.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + txtJobWork.Value + "' ")
    End Sub

End Class
