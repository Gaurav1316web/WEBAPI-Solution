Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports System.IO
Imports common
Imports XpertERPEngine

Public Class frmMilkPurchaseInvoiceMCC
    Inherits FrmMainTranScreen


#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim userCode, companyCode As String

    Const colCode As String = "COLCODE"
    Private objRemittance As clsRemittance
    Const colSRN_CODE As String = "colSRN_CODE"
    Const colItem_Code As String = "colItem_Code"
    Const colHSNNo As String = "COLHSNNo"
    Const colQty As String = "colQty"
    Const colAcc_Qty As String = "colAcc_Qty"
    Const colFAT_PER As String = "colFAT_PER"
    Const colSNF_PER As String = "colSNF_PER"
    Const colMCC_CODE As String = "colMCC_CODE"
    Const colVEHICLE_NO As String = "colVEHICLE_NO"
    Const colVLC_NO As String = "colVLC_NO"
    Const colCans As String = "colCans"
    Const colCorrection_Factor As String = "colCorrection_Factor"
    Const colCLR As String = "colCLR"
    Const colRATE As String = "colRATE"
    Const colAMOUNT As String = "colAMOUNT"
    Const colServiceChargeAmount As String = "colServiceChargeAmount"
    Const colHandlingCharges As String = "colHandlingCharges"
    Const colNetAMOUNT As String = "colNetAMOUNT"
    Const colNetsaveAMOUNT As String = "colNetsaveAMOUNT"
    Const colIncentive As String = "colIncentive"
    Const colIncentiveEMP As String = "colIncentiveEMP"
    Const colService_Charge As String = "colService_Charge"
    Const colCOMMISSION As String = "colCOMMISSION"
    Const colCOMMISSIONAmount As String = "colCOMMISSIONAmount"
    Const colPaymentCOMMISSION As String = "colPaymentCOMMISSION"
    Const colPaymentCOMMISSIONAmount As String = "colPaymentCOMMISSIONAmount"
    Const colDeduction As String = "colDeduction"
    Const colHead_Load_Amount As String = "colHead_Load_Amount"
    Const colOwn_Asset_Amount As String = "colOwn_Asset_Amount"

    ' Const colAMOUNT As String = "colAMOUNT"
    Const colTOTAL_AMOUNT As String = "colTOTAL_AMOUNT"
    Const colDocument_AMOUNT As String = "colDocument_AMOUNT"

    Const colItem_Desc As String = "colItem_Desc"
    Const colSrn_Date As String = "colSrn_Date"
    Const colUOM As String = "colUOM"

    Private isInsideLoadData As Boolean = False
    Private isCellValueChangedOpen As Boolean = False
    Dim is_Entered_Manually As Boolean = False
    Public DtMCC As DataTable
    ' Dim objSr As New clsWeighingMachine
    'Dim objSerial As New clsSerialPort
    Public Shared strDocumentNo As String = ""
    'Dim Payment_Cycle_value As Integer = 0
#End Region


#Region "Functions"
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmMilkPurchaseInvoice)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        'btnsave.Visible = MyBase.isModifyFlag
        'BtnPost.Visible = MyBase.isPostFlag
        'btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    'Sub ViewTDS()
    '    Try
    '        Dim frm As New FrmViewTDS()
    '        UpdateTDSAmount(Nothing)
    '        frm.ObjIn = objRemittance
    '        frm.ShowDialog()
    '        'If (frm.ObjReturn IsNot Nothing) Then
    '        objRemittance = frm.ObjReturn
    '        'End If
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
    '    End Try
    'End Sub

    Function SaveData()
        'Dim trans As SqlTransaction
        Try
            If (AllowToSave(Nothing)) Then
                ' trans = clsDBFuncationality.GetTransactin()
                Dim objHead As clsMilkPurchaseInvoiceMCC
                Dim TotHeadLoad As Double = 0
                Dim TotOwnAsset As Double = 0
                Dim TotDeduction_Amount As Double = 0
                '' asign screen vaules in objHead
                objHead = New clsMilkPurchaseInvoiceMCC
                objHead.DOC_CODE = clsCommon.myCstr(txtCode.Value)
                objHead.DOC_DATE = clsCommon.myCDate(dtpDocDate.Value)
                objHead.Description = clsCommon.myCstr(Me.txtDesc.Text)
                objHead.ROUTE_CODE = clsCommon.myCstr(fndRouteCOde.Text)
                objHead.VSP_CODE = clsCommon.myCstr(fndVSPCode.Value)
                objHead.Payment = clsCommon.myCstr(txtPayment.Text)
                objHead.Amount = clsCommon.myCdbl(TxtTotlAMount.Text)
                objHead.Commission = clsCommon.myCdbl(txtTotComm.Text)
                objHead.Total_Amount_Acc = clsCommon.myCdbl(TxtAccTot.Text)
                objHead.Total_PaymentCommission = clsCommon.myCdbl(TxtPaymentComm.Text)
                objHead.MCC_CODE = clsCommon.myCstr(FndMccCode.Value)
                objHead.VENDOR_INVOICE_NO = clsCommon.myCstr(TxtVendorInvoiceNo.Text)
                objHead.VENDOR_INVOICE_DATE = clsCommon.myCDate(Vendor_Invoice_Date.Value)

                Dim objList As New List(Of clsMilkPurchaseInvoiceMCCDetail)

                Dim obj As clsMilkPurchaseInvoiceMCCDetail
                For Each grow As GridViewRowInfo In gv1.Rows
                    obj = New clsMilkPurchaseInvoiceMCCDetail
                    obj.DOC_CODE = clsCommon.myCstr(txtCode.Value)
                    obj.AMOUNT = clsCommon.myCdbl(grow.Cells(colAMOUNT).Value)
                    obj.Cans = clsCommon.myCdbl(grow.Cells(colCans).Value)
                    obj.CLR = clsCommon.myCdbl(grow.Cells(colCLR).Value)
                    obj.COMMISSION = clsCommon.myCdbl(grow.Cells(colCOMMISSION).Value)
                    obj.Payment_COMMISSION = clsCommon.myCdbl(grow.Cells(colPaymentCOMMISSION).Value)
                    obj.Deduction = clsCommon.myCdbl(grow.Cells(colDeduction).Value)
                    obj.Own_Asset_Amount = clsCommon.myCdbl(grow.Cells(colOwn_Asset_Amount).Value)
                    obj.Head_Load_Amount = clsCommon.myCdbl(grow.Cells(colHead_Load_Amount).Value)
                    obj.Correction_Factor = clsCommon.myCdbl(grow.Cells(colCorrection_Factor).Value)
                    obj.FAT_PER = clsCommon.myCdbl(grow.Cells(colFAT_PER).Value)
                    'obj.Incentive = clsCommon.myCdbl(grow.Cells(colIncentive).Value)
                    'obj.IncentiveEMP = clsCommon.myCdbl(grow.Cells(colIncentiveEMP).Value)
                    obj.Item_Code = clsCommon.myCstr(grow.Cells(colItem_Code).Value)
                    obj.MCC_CODE = FndMccCode.Value
                    obj.Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    obj.Acc_Qty = clsCommon.myCdbl(grow.Cells(colAcc_Qty).Value)
                    obj.Service_Charge = clsCommon.myCstr(grow.Cells(colService_Charge).Value)
                    obj.RATE = clsCommon.myCdbl(grow.Cells(colRATE).Value)
                    obj.SNF_PER = clsCommon.myCdbl(grow.Cells(colSNF_PER).Value)
                    obj.Head_Load_Amount = clsCommon.myCdbl(grow.Cells(colHead_Load_Amount).Value)
                    obj.Own_Asset_Amount = clsCommon.myCdbl(grow.Cells(colOwn_Asset_Amount).Value)

                    obj.SRN_CODE = clsCommon.myCstr(grow.Cells(colSRN_CODE).Value)
                    obj.TOTAL_AMOUNT = clsCommon.myCdbl(grow.Cells(colTOTAL_AMOUNT).Value)
                    obj.VEHICLE_NO = clsCommon.myCstr(grow.Cells(colVEHICLE_NO).Value)
                    obj.VLC_NO = clsCommon.myCstr(grow.Cells(colVLC_NO).Value)
                    obj.Net_AMOUNT = clsCommon.myCdbl(grow.Cells(colNetsaveAMOUNT).Value)

                    objList.Add(obj)
                    TotHeadLoad += obj.Head_Load_Amount
                    TotOwnAsset += obj.Own_Asset_Amount
                    TotDeduction_Amount += obj.Deduction
                Next
                objHead.Total_Head_Load_Amount = TotHeadLoad
                objHead.Total_Own_Asset_Amount = TotOwnAsset
                objHead.Total_Deduction_Amount = TotDeduction_Amount
                If objRemittance IsNot Nothing Then
                    objHead.objPIRemittance = New clsPIRemittance()
                    objHead.objPIRemittance.Vendor_Code = objRemittance.Vendor_Code
                    objHead.objPIRemittance.Vendor_Name = objRemittance.Vendor_Name
                    objHead.objPIRemittance.Document_No = objRemittance.Document_No
                    objHead.objPIRemittance.Document_Date = objRemittance.Document_Date
                    objHead.objPIRemittance.Document_Type = objRemittance.Document_Type
                    objHead.objPIRemittance.Document_Amount = objRemittance.Document_Amount
                    objHead.objPIRemittance.Service_Type = objRemittance.Service_Type
                    objHead.objPIRemittance.Actual_TDS_Base = objRemittance.Actual_TDS_Base
                    objHead.objPIRemittance.Calculated_TDS_Base = objRemittance.Calculated_TDS_Base
                    objHead.objPIRemittance.Actual_TDS = objRemittance.Actual_TDS
                    objHead.objPIRemittance.Calculated_TDS = objRemittance.Calculated_TDS
                    objHead.objPIRemittance.Actual_Surcharge = objRemittance.Actual_Surcharge
                    objHead.objPIRemittance.Calculated_Surcharge = objRemittance.Calculated_Surcharge
                    objHead.objPIRemittance.Actual_Edu_Cess = objRemittance.Actual_Edu_Cess
                    objHead.objPIRemittance.Calculated_Edu_Cess = objRemittance.Calculated_Edu_Cess
                    objHead.objPIRemittance.Actual_Sec_Educess = objRemittance.Actual_Sec_Educess
                    objHead.objPIRemittance.Calculated_Sec_Educess = objRemittance.Calculated_Sec_Educess
                    objHead.objPIRemittance.Actual_Total_TDS = objRemittance.Actual_Total_TDS
                    objHead.objPIRemittance.Calculated_Total_TDS = objRemittance.Calculated_Total_TDS
                    objHead.objPIRemittance.Fiscal_Year = objRemittance.Fiscal_Year
                    objHead.objPIRemittance.Quarter = objRemittance.Quarter
                    objHead.objPIRemittance.Section_Code = objRemittance.Section_Code
                    objHead.objPIRemittance.Section_Description = objRemittance.Section_Description
                    objHead.objPIRemittance.Branch_Code = objRemittance.Branch_Code
                    objHead.objPIRemittance.Deduction_Code = objRemittance.Deduction_Code
                    objHead.objPIRemittance.TDS_Per = objRemittance.TDS_Per
                    objHead.objPIRemittance.Surcharge_Per = objRemittance.Surcharge_Per
                    objHead.objPIRemittance.Edu_Cess_Per = objRemittance.Edu_Cess_Per
                    objHead.objPIRemittance.Sec_Educess_Per = objRemittance.Sec_Educess_Per
                    objHead.objPIRemittance.Select_By = objRemittance.Select_By
                    objHead.objPIRemittance.IsTDSOverride = objRemittance.IsTDSOverride
                    objHead.objPIRemittance.IsApplyTDS = objRemittance.IsApplyTDS
                End If
                If clsMilkPurchaseInvoiceMCC.SaveData(objHead, objList) Then
                    txtCode.Value = objHead.DOC_CODE
                    UcAttachment1.SaveData(objHead.DOC_CODE)
                    Return True
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return False
    End Function

    Function SaveDataForVspPayment(ByVal trans As SqlTransaction)
        'Dim trans As SqlTransaction
        Try
            If (AllowToSave(trans)) Then
                ' trans = clsDBFuncationality.GetTransactin()
                Dim objHead As clsMilkPurchaseInvoiceMCC
                Dim TotHeadLoad As Double = 0
                Dim TotOwnAsset As Double = 0
                Dim TotDeduction_Amount As Double = 0
                '' asign screen vaules in objHead
                objHead = New clsMilkPurchaseInvoiceMCC
                objHead.DOC_CODE = clsCommon.myCstr(txtCode.Value)
                objHead.DOC_DATE = clsCommon.myCDate(dtpDocDate.Value)
                objHead.Description = clsCommon.myCstr(Me.txtDesc.Text)
                objHead.ROUTE_CODE = clsCommon.myCstr(fndRouteCOde.Text)
                objHead.VSP_CODE = clsCommon.myCstr(fndVSPCode.Value)
                objHead.Payment = clsCommon.myCstr(txtPayment.Text)
                objHead.Amount = clsCommon.myCdbl(TxtTotlAMount.Text)
                objHead.Basic_Amount = clsCommon.myCdbl(TxtTotlAMount.Text) - clsCommon.myCdbl(txtTotComm.Text)
                objHead.Commission = clsCommon.myCdbl(txtTotComm.Text)
                objHead.Total_Amount_Acc = clsCommon.myCdbl(TxtAccTot.Text)
                objHead.Total_PaymentCommission = clsCommon.myCdbl(TxtPaymentComm.Text)
                objHead.Irregular_MCC_CODE = clsCommon.myCstr(FndMccCode.Value)
                objHead.VENDOR_INVOICE_NO = clsCommon.myCstr(TxtVendorInvoiceNo.Text)
                objHead.VENDOR_INVOICE_DATE = clsCommon.myCDate(Vendor_Invoice_Date.Value)


                Dim objList As New List(Of clsMilkPurchaseInvoiceMCCDetail)

                Dim obj As clsMilkPurchaseInvoiceMCCDetail
                For Each grow As GridViewRowInfo In gv1.Rows
                    obj = New clsMilkPurchaseInvoiceMCCDetail
                    obj.DOC_CODE = clsCommon.myCstr(txtCode.Value)
                    obj.AMOUNT = clsCommon.myCdbl(grow.Cells(colAMOUNT).Value)
                    obj.Cans = clsCommon.myCdbl(grow.Cells(colCans).Value)
                    obj.CLR = clsCommon.myCdbl(grow.Cells(colCLR).Value)
                    obj.COMMISSION = clsCommon.myCdbl(grow.Cells(colCOMMISSION).Value)
                    obj.Payment_COMMISSION = clsCommon.myCdbl(grow.Cells(colPaymentCOMMISSION).Value)
                    obj.Deduction = clsCommon.myCdbl(grow.Cells(colDeduction).Value)
                    obj.Own_Asset_Amount = clsCommon.myCdbl(grow.Cells(colOwn_Asset_Amount).Value)
                    obj.Head_Load_Amount = clsCommon.myCdbl(grow.Cells(colHead_Load_Amount).Value)
                    obj.Correction_Factor = clsCommon.myCdbl(grow.Cells(colCorrection_Factor).Value)
                    obj.FAT_PER = clsCommon.myCdbl(grow.Cells(colFAT_PER).Value)
                    'obj.Incentive = clsCommon.myCdbl(grow.Cells(colIncentive).Value)
                    'obj.IncentiveEMP = clsCommon.myCdbl(grow.Cells(colIncentiveEMP).Value)
                    obj.Item_Code = clsCommon.myCstr(grow.Cells(colItem_Code).Value)
                    obj.MCC_CODE = FndMccCode.Value
                    obj.Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    obj.Acc_Qty = clsCommon.myCdbl(grow.Cells(colAcc_Qty).Value)
                    obj.Service_Charge = clsCommon.myCstr(grow.Cells(colService_Charge).Value)
                    obj.RATE = clsCommon.myCdbl(grow.Cells(colRATE).Value)
                    obj.SNF_PER = clsCommon.myCdbl(grow.Cells(colSNF_PER).Value)
                    obj.Head_Load_Amount = clsCommon.myCdbl(grow.Cells(colHead_Load_Amount).Value)
                    obj.Own_Asset_Amount = clsCommon.myCdbl(grow.Cells(colOwn_Asset_Amount).Value)

                    obj.SRN_CODE = clsCommon.myCstr(grow.Cells(colSRN_CODE).Value)
                    obj.TOTAL_AMOUNT = clsCommon.myCdbl(grow.Cells(colTOTAL_AMOUNT).Value)
                    obj.VEHICLE_NO = clsCommon.myCstr(grow.Cells(colVEHICLE_NO).Value)
                    obj.VLC_NO = clsCommon.myCstr(grow.Cells(colVLC_NO).Value)
                    obj.Net_AMOUNT = clsCommon.myCdbl(grow.Cells(colNetsaveAMOUNT).Value)
                    objHead.MCC_CODE = clsMilkPurchaseInvoiceMCC.GetIrregular_Location(obj.SRN_CODE, trans)
                    If clsCommon.CompairString(objHead.MCC_CODE, objHead.Irregular_MCC_CODE) = CompairStringResult.Equal Then
                        objHead.Irregular_MCC_CODE = ""
                    End If

                    objList.Add(obj)
                    TotHeadLoad += obj.Head_Load_Amount
                    TotOwnAsset += obj.Own_Asset_Amount
                    TotDeduction_Amount += obj.Deduction
                Next
                objHead.Total_Head_Load_Amount = TotHeadLoad
                objHead.Total_Own_Asset_Amount = TotOwnAsset
                objHead.Total_Deduction_Amount = TotDeduction_Amount
                'End If
                'Next
                ' ''For Custom Fields
                ''Dim obj As New clsMilkPurchaseInvoiceMCC()
                'obj = New clsMilkPurchaseInvoiceMCC
                'obj.Form_ID = MyBase.Form_ID
                'obj.arrCustomFields = New List(Of clsCustomFieldValues)
                'If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                '    UcCustomFields1.GetData(obj.arrCustomFields)
                'End If
                'If MyBase.ArrDetailFields IsNot Nothing AndAlso MyBase.ArrDetailFields.Count > 0 Then
                '    clsCustomFieldGrid.GetData(obj.arrCustomFields, gv1, MyBase.ArrDetailFields, colCode)
                'End If
                ' ''End of For Custom Fields

                If clsMilkPurchaseInvoiceMCC.SaveData(objHead, objList, trans) Then
                    'trans.Commit()
                    'Dim transs As SqlTransaction
                    txtCode.Value = objHead.DOC_CODE
                    'UcAttachment1.SaveData(objHead.DOC_CODE)
                    Dim incentive As ArrayList = clsMilkPurchaseInvoiceMCC.LoadDataQuery_For_Incentive(txtCode.Value, fndVSPCode.Value, FndMccCode.Value, dtpDocDate.Value, Today.Date, False, trans, 0)
                    Dim sQuery As String = ""
                    Dim totincentiveEMP As Double = 0
                    If incentive.Count > 0 Then
                        For Each row As GridViewRowInfo In gv1.Rows
                            gv1.Rows(0).Cells(colIncentive).Value = incentive(1)
                            sQuery = "Update tspl_Milk_Purchase_Invoice_Detail set Total_Amount='" & clsCommon.myCdbl(row.Cells(colAMOUNT).Value) & "',Total_Amount_Acc='" & clsCommon.myCdbl(row.Cells(colDocument_AMOUNT).Value) & "',Net_Amount='" & clsCommon.myCdbl(row.Cells(colNetsaveAMOUNT).Value) & "',incentive='" & row.Cells(colIncentive).Value & "' , incentiveEMP='" & row.Cells(colIncentiveEMP).Value & "' where srn_code='" & row.Cells(colSRN_CODE).Value & "'"
                            clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
                            Exit For
                        Next
                        totincentiveEMP += clsCommon.myCdbl(gv1.Rows(0).Cells(colIncentiveEMP).Value)
                        sQuery = "select * from tspl_Milk_Purchase_Invoice_Head where doc_code='" & clsCommon.myCstr(txtCode.Value) & "'"
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(sQuery, trans)
                        sQuery = "Update tspl_Milk_Purchase_Invoice_Head set Total_Amount='" & clsCommon.myCdbl(TxtTotlAMount.Text) & "',Total_Amount_Acc='" & clsCommon.myCdbl(TxtAccTot.Text) & "' ,incentive_Head='" & incentive(1) & "' , incentiveEMP_Head='" & totincentiveEMP & "' where doc_code='" & clsCommon.myCstr(txtCode.Value) & "'"
                        clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
                    End If
                    Return True
                End If
                
            End If
            Return True
        Catch ex As Exception
            ' trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
    End Function

    Private Function AllowToSave(ByVal trans As SqlTransaction) As Boolean
        Try
            ' = KUNAL > TICKET : BM00000009575 =====
            If AllowFutureDateTransaction(dtpDocDate.Value, trans) = False Then
                dtpDocDate.Focus()
                Return False
            End If

            If btnsave.Text = "Update" Then
                Dim strchk As String = "select POSTED from TSPL_MILK_PURCHASE_INVOICE_HEAD where DOC_COde='" + txtCode.Value + "'"
                Dim chkpost As String = clsDBFuncationality.getSingleValue(strchk, trans)
                If chkpost = "1" Then
                    clsCommon.MyMessageBoxShow(Me, "Transection already posted", Me.Text)
                    Return False
                End If
            End If

            If clsCommon.myLen(Me.FndMccCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Enter MCC", Me.Text)
                FndMccCode.Focus()
                Return False
            End If


            If clsCommon.myLen(Me.fndVSPCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Enter VSP Code", Me.Text)
                fndVSPCode.Focus()
                Return False
            End If
            ' If objRemittance Is Nothing Then
            UpdateTDSAmount(trans)
            ' End If
            'If UsLock1.Status = ERPTransactionStatus.Approved Then
            '    clsCommon.MyMessageBoxShow("This Document is Approved and can not take new entries..", Me.Text)
            '    Return False
            'End If
            'For Each grow As GridViewRowInfo In gv1.Rows
            '    'If LCase(txtPayment.Text) <> "different" Then
            '    If clsCommon.myCstr(grow.Cells(colService_Charge).Value) = "%(Percentage)" Then
            '        grow.Cells(colCOMMISSIONAmount).Value = grow.Cells(colAMOUNT).Value * grow.Cells(colCOMMISSION).Value / 100
            '        grow.Cells(colPaymentCOMMISSIONAmount).Value = grow.Cells(colAMOUNT).Value * grow.Cells(colPaymentCOMMISSION).Value / 100
            '        grow.Cells(colIncentiveEMP).Value = clsCommon.myCdbl(grow.Cells(colIncentive).Value) * grow.Cells(colPaymentCOMMISSION).Value / 100
            '    ElseIf clsCommon.myCstr(grow.Cells(colService_Charge).Value) = "Rate/Kg" Then
            '        grow.Cells(colCOMMISSIONAmount).Value = grow.Cells(colAcc_Qty).Value * grow.Cells(colCOMMISSION).Value
            '        grow.Cells(colPaymentCOMMISSIONAmount).Value = grow.Cells(colAcc_Qty).Value * grow.Cells(colPaymentCOMMISSION).Value
            '        grow.Cells(colIncentiveEMP).Value = clsCommon.myCdbl(grow.Cells(colIncentive).Value) * grow.Cells(colPaymentCOMMISSION).Value / 100
            '    ElseIf clsCommon.myCstr(grow.Cells(colService_Charge).Value) = "Rate/Ltr" And clsCommon.myCstr(grow.Cells(colUOM).Value) = "LTR" Then
            '        grow.Cells(colCOMMISSIONAmount).Value = grow.Cells(colQty).Value * grow.Cells(colCOMMISSION).Value
            '        grow.Cells(colPaymentCOMMISSIONAmount).Value = grow.Cells(colQty).Value * grow.Cells(colPaymentCOMMISSION).Value
            '        grow.Cells(colIncentiveEMP).Value = clsCommon.myCdbl(grow.Cells(colIncentive).Value) * grow.Cells(colPaymentCOMMISSION).Value / 100
            '    End If
            '    grow.Cells(colDocument_AMOUNT).Value = clsCommon.myCdbl(grow.Cells(colAMOUNT).Value) + clsCommon.myCdbl(grow.Cells(colCOMMISSIONAmount).Value) + clsCommon.myCdbl(grow.Cells(colIncentive).Value) + clsCommon.myCdbl(grow.Cells(colIncentiveEMP).Value) '- clsCommon.myCdbl(grow.Cells(colDeduction).Value)
            '    grow.Cells(colTOTAL_AMOUNT).Value = clsCommon.myCdbl(grow.Cells(colAMOUNT).Value) + clsCommon.myCdbl(grow.Cells(colPaymentCOMMISSIONAmount).Value) + clsCommon.myCdbl(grow.Cells(colIncentive).Value) + clsCommon.myCdbl(grow.Cells(colIncentiveEMP).Value) '- clsCommon.myCdbl(grow.Cells(colDeduction).Value)
            '    grow.Cells(colNetAMOUNT).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colAMOUNT).Value) + clsCommon.myCdbl(grow.Cells(colPaymentCOMMISSIONAmount).Value) + clsCommon.myCdbl(grow.Cells(colIncentive).Value) + clsCommon.myCdbl(grow.Cells(colIncentiveEMP).Value)) ' - clsCommon.myCdbl(grow.Cells(colDeduction).Value), CInt(clsCommon.myCdbl(DtMCC.Rows(0).Item("FAT_SNF_CALC"))))
            '    grow.Cells(colNetsaveAMOUNT).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colAMOUNT).Value) + clsCommon.myCdbl(grow.Cells(colPaymentCOMMISSIONAmount).Value) + clsCommon.myCdbl(grow.Cells(colIncentive).Value) + clsCommon.myCdbl(grow.Cells(colIncentiveEMP).Value)) ' - clsCommon.myCdbl(grow.Cells(colDeduction).Value), CInt(clsCommon.myCdbl(DtMCC.Rows(0).Item("FAT_SNF_SAVE"))))
            '    'Else
            '    'grow.Cells(colCOMMISSIONAmount).Value = grow.Cells(colAMOUNT).Value * grow.Cells(colCOMMISSION).Value / 100
            '    'grow.Cells(colPaymentCOMMISSIONAmount).Value = grow.Cells(colAMOUNT).Value * grow.Cells(colPaymentCOMMISSION).Value / 100
            '    'grow.Cells(colDocument_AMOUNT).Value = clsCommon.myCdbl(grow.Cells(colAMOUNT).Value) + clsCommon.myCdbl(grow.Cells(colIncentive).Value) '- clsCommon.myCdbl(grow.Cells(colDeduction).Value)
            '    'grow.Cells(colTOTAL_AMOUNT).Value = clsCommon.myCdbl(grow.Cells(colAMOUNT).Value) + clsCommon.myCdbl(grow.Cells(colIncentive).Value) '- clsCommon.myCdbl(grow.Cells(colDeduction).Value)
            '    'grow.Cells(colNetAMOUNT).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colAMOUNT).Value) + clsCommon.myCdbl(grow.Cells(colIncentive).Value) - clsCommon.myCdbl(grow.Cells(colDeduction).Value), CInt(clsCommon.myCdbl(DtMCC.Rows(0).Item("FAT_SNF_CALC"))))
            '    'grow.Cells(colNetsaveAMOUNT).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colAMOUNT).Value) + clsCommon.myCdbl(grow.Cells(colIncentive).Value) - clsCommon.myCdbl(grow.Cells(colDeduction).Value), CInt(clsCommon.myCdbl(DtMCC.Rows(0).Item("FAT_SNF_SAVE"))))
            '    'End If

            'Next
            'Dim totAmount As Double = 0
            'Dim totCommssion As Double = 0
            'Dim totPaymentCommssion As Double = 0
            'Dim totAmountwithPaymentCommssion As Double = 0
            'Dim totAmountIncentive As Double = 0
            'Dim totAmountIncentiveEMP As Double = 0
            'Dim totBasicAmount As Double = 0
            'For Each groww As GridViewRowInfo In gv1.Rows
            '    totAmount += groww.Cells(colDocument_AMOUNT).Value
            '    totBasicAmount += groww.Cells(colAMOUNT).Value
            '    totCommssion += groww.Cells(colCOMMISSIONAmount).Value
            '    totPaymentCommssion += groww.Cells(colPaymentCOMMISSIONAmount).Value
            '    totAmountwithPaymentCommssion += groww.Cells(colNetAMOUNT).Value
            '    totAmountIncentive += groww.Cells(colIncentive).Value
            '    totAmountIncentiveEMP += groww.Cells(colIncentiveEMP).Value
            'Next
            'TxtTotlAMount.Text = totAmount
            'txtTotComm.Text = totCommssion
            'TxtAccTot.Text = totAmountwithPaymentCommssion
            'TxtPaymentComm.Text = totPaymentCommssion
            'LblEMP.Text = totPaymentCommssion
            'lblAmount.Text = totBasicAmount
            'lblIncentive.Text = totAmountIncentive
            'lblIncentiveEMP.Text = totAmountIncentiveEMP
            'lblTotRAmt.Text = totAmountwithPaymentCommssion
            UcCustomFields1.AllowToSave()
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function

    Sub AddNew()
        ' isNewEntry = True
        FndMccCode.Enabled = True
        txtCode.Value = ""
        btnsave.Text = "Save"
        gv1.Rows.Clear()
        objRemittance = Nothing
        gv1.Columns.Clear()
        gv1.DataSource = Nothing
        txtDesc.Text = ""
        gv1.DataSource = Nothing
        Me.dtpDocDate.Value = clsCommon.GETSERVERDATE()
        'If Payment_Cycle_value > 0 Then
        '    Me.ToDate.Value = clsCommon.GETSERVERDATE().AddDays(-1)
        '    Me.Fromdate.Value = clsCommon.GETSERVERDATE().AddDays(-1 - Payment_Cycle_value)
        'Else
        Me.ToDate.Value = clsCommon.GETSERVERDATE()
        Me.Fromdate.Value = clsCommon.GETSERVERDATE()
        'End If
        Me.txtDesc.Text = ""
        Me.txtPayment.Text = ""
        Me.TxtTotlAMount.Text = 0
        Me.txtTotComm.Text = 0
        Me.TxtAccTot.Text = 0
        Me.TxtPaymentComm.Text = 0
        Me.TxtVendorInvoiceNo.Text = ""
        lblHandlingChargesROAmount.Text = ""
        lblRoundOffAmount.Text = ""
        Me.Vendor_Invoice_Date.Value = clsCommon.GETSERVERDATE()
        Me.lblVSPDesc.Text = ""
        lblRouteDesc.Text = Nothing
        fndRouteCOde.Text = Nothing
        FndSRNNO.Value = Nothing
        fndVSPCode.Value = Nothing

        btnsave.Enabled = True
        BtnPost.Enabled = True
        btndelete.Enabled = True
        btnViewTDSDetails.Enabled = False
        'For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
        End If
        UcAttachment1.BlankAllControls()
        ''End of For Custom Fields
    End Sub



#End Region

#Region "Events"

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        If SaveData() Then
            clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
            LoadData(txtCode.Value)
        End If
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Sub DeleteData()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If clsCancelLog.CheckForReasonOnDelete() Then
                    '' REASON FOR DELETE 
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Delete"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If
                End If
                If (clsMilkPurchaseInvoiceMCC.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub frmMilkReceiptMCC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        'ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
        ' globalFunc.mandatoryText(fnddesig.txtValue, txtdes)

        ''For Custom Fields
        RadPageView1.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.Report_ID = MyBase.Form_ID
            UcCustomFields1.LoadCustomControls()
        End If
        ''For Attachment
        If objCommonVar.IsDemoERP Then
            UcAttachment1.Form_ID = MyBase.Form_ID
            RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Visible
        Else
            RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Collapsed
        End If
        ''End of For Attachment

        ''End of For Custom Fields
        Dim sQuery As String = "select Default_Location,Payment_Cycle from TSPL_USER_MASTER " _
        & " Left join tspl_Mcc_master mm on mm.mcc_COde=TSPL_USER_MASTER.Default_Location where User_Code='" + objCommonVar.CurrentUserCode + "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(sQuery)
        If dt.Rows.Count > 0 Then
            Me.FndMccCode.Value = clsCommon.myCstr(dt.Rows(0).Item("Default_Location"))
        Else
            Me.FndMccCode.Value = ""
        End If

        'Payment_Cycle_value = clsCommon.myCdbl(dt.Rows(0).Item("Payment_Cycle"))
        AddNew()
        ' LoadData(Me.txtCode.Value)
        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = False
        gv1.AllowEditRow = False
        gv1.MasterTemplate.AllowCellContextMenu = True
        gv1.MasterTemplate.AllowColumnHeaderContextMenu = True
        gv1.MasterTemplate.AllowDeleteRow = True
        ReStoreGridLayout()
        If clsCommon.myLen(strDocumentNo) > 0 Then
            LoadData(strDocumentNo)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag))
        End If
        txtCode.MyMaxLength = 100
    End Sub


    Private Sub frmMilkReceiptMCC_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            'SaveData()
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            '    PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            'DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            AddNew()
        ElseIf e.Control And e.Alt And e.KeyCode = Keys.I Then
            Dim strPoInvcNo As String = connectSql.RunScalar("select Voucher_No from tspl_journal_master where Source_Doc_No= (select Document_No from TSPL_VENDOR_INVOICE_HEAD where RefDocType='MI-PI' and Against_MillkPurchaseInvoice_No ='" + txtCode.Value + "')")
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.journalEntry, strPoInvcNo)
        ElseIf e.Control And e.Alt And e.KeyCode = Keys.M Then
            Dim strPoInvcNo As String = connectSql.RunScalar("select Voucher_No from tspl_journal_master where Source_Doc_No= (select Document_No from TSPL_VENDOR_INVOICE_HEAD where RefDocType='MI-CO' and Against_MillkPurchaseInvoice_No ='" + txtCode.Value + "')")
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.journalEntry, strPoInvcNo)
        ElseIf e.Control And e.Alt And e.KeyCode = Keys.J Then
            ViewTDS()
        End If
    End Sub
#End Region

    Public Sub LoadData(ByVal strDoc As String, Optional ByVal navType As NavigatorType = NavigatorType.Current)
        Try
            AddNew()
            LoadBlankGrid()
            btnsave.Enabled = True
            BtnPost.Enabled = True
            btndelete.Enabled = True
            Dim obj As clsMilkPurchaseInvoiceMCC = clsMilkPurchaseInvoiceMCC.GetData(strDoc, navType)
            If IsNothing(obj) Then
                Exit Sub
            End If
            FndMccCode.Enabled = False
            btnsave.Text = "Update"
            txtCode.Value = obj.DOC_CODE
            dtpDocDate.Value = obj.DOC_DATE
            FndMccCode.Value = obj.MCC_CODE
            DtMCC = clsDBFuncationality.GetDataTable("select * from tspl_Mcc_Master where Mcc_Code='" & clsCommon.myCstr(FndMccCode.Value) & "'")
            lblMccName.Text = DtMCC.Rows(0).Item("mcc_name") 'clsDBFuncationality.getSingleValue("select mcc_name from tspl_mcc_master where mcc_Code='" & obj.MCC_CODE & "'")
            txtDesc.Text = obj.Description
            fndVSPCode.Value = obj.VSP_CODE
            FndSRNNO.Value = ""
            txtPayment.Text = obj.Payment
            TxtTotlAMount.Text = obj.Amount
            txtTotComm.Text = obj.Commission
            fndRouteCOde.Text = obj.ROUTE_CODE
            TxtVendorInvoiceNo.Text = obj.VENDOR_INVOICE_NO
            Vendor_Invoice_Date.Value = obj.VENDOR_INVOICE_DATE
            UsLock1.Status = obj.POSTED
            lblHandlingChrgesPer.Text = clsCommon.myCstr(obj.Handling_Charges_Per)
            lblHandlingChargesAmount.Text = clsCommon.myCstr(obj.Handling_Charges_Amount)
            lblHandlingChargesROAmount.Text = clsCommon.myCstr(obj.Handling_Charges_RO_Amount)
            lblTotRAmt.Text = clsCommon.myCstr(obj.Total_Amount_Acc)
            lblRoundOffAmount.Text = clsCommon.myCstr(obj.RoundOffAmount)
            lblRouteDesc.Text = clsDBFuncationality.getSingleValue("select Route_Name from TSPL_MCC_ROUTE_MASTER where Route_Code='" & fndRouteCOde.Text & "'")
            lblVSPDesc.Text = clsDBFuncationality.getSingleValue("select vendor_name from TSPL_VENDOR_MASTER where Vendor_Code='" & fndVSPCode.Value & "'")


            If obj.POSTED = ERPTransactionStatus.Approved Then
                btnsave.Enabled = False
                BtnPost.Enabled = False
                btndelete.Enabled = False
            End If

            If (clsMilkPurchaseInvoiceMCC.ObjList IsNot Nothing AndAlso clsMilkPurchaseInvoiceMCC.ObjList.Count > 0) Then
                objRemittance = Nothing
                If obj.objPIRemittance IsNot Nothing Then
                    btnViewTDSDetails.Enabled = True
                    'If objRemittance IsNot Nothing Then
                    objRemittance = New clsRemittance()
                    objRemittance.Vendor_Code = obj.objPIRemittance.Vendor_Code
                    objRemittance.Vendor_Name = obj.objPIRemittance.Vendor_Name
                    objRemittance.Document_No = obj.objPIRemittance.Document_No
                    objRemittance.Document_Date = obj.objPIRemittance.Document_Date
                    objRemittance.Document_Type = obj.objPIRemittance.Document_Type
                    objRemittance.Document_Amount = obj.objPIRemittance.Document_Amount
                    objRemittance.Service_Type = obj.objPIRemittance.Service_Type
                    objRemittance.Actual_TDS_Base = obj.objPIRemittance.Actual_TDS_Base
                    objRemittance.Calculated_TDS_Base = obj.objPIRemittance.Calculated_TDS_Base
                    objRemittance.Actual_TDS = obj.objPIRemittance.Actual_TDS
                    objRemittance.Calculated_TDS = obj.objPIRemittance.Calculated_TDS
                    objRemittance.Actual_Surcharge = obj.objPIRemittance.Actual_Surcharge
                    objRemittance.Calculated_Surcharge = obj.objPIRemittance.Calculated_Surcharge
                    objRemittance.Actual_Edu_Cess = obj.objPIRemittance.Actual_Edu_Cess
                    objRemittance.Calculated_Edu_Cess = obj.objPIRemittance.Calculated_Edu_Cess
                    objRemittance.Actual_Sec_Educess = obj.objPIRemittance.Actual_Sec_Educess
                    objRemittance.Calculated_Sec_Educess = obj.objPIRemittance.Calculated_Sec_Educess
                    objRemittance.Actual_Total_TDS = obj.objPIRemittance.Actual_Total_TDS
                    objRemittance.Calculated_Total_TDS = obj.objPIRemittance.Calculated_Total_TDS
                    objRemittance.Fiscal_Year = obj.objPIRemittance.Fiscal_Year
                    objRemittance.Quarter = obj.objPIRemittance.Quarter
                    objRemittance.Section_Code = obj.objPIRemittance.Section_Code
                    objRemittance.Section_Description = obj.objPIRemittance.Section_Description
                    objRemittance.Branch_Code = obj.objPIRemittance.Branch_Code
                    objRemittance.Deduction_Code = obj.objPIRemittance.Deduction_Code
                    objRemittance.TDS_Per = obj.objPIRemittance.TDS_Per
                    objRemittance.Surcharge_Per = obj.objPIRemittance.Surcharge_Per
                    objRemittance.Edu_Cess_Per = obj.objPIRemittance.Edu_Cess_Per
                    objRemittance.Sec_Educess_Per = obj.objPIRemittance.Sec_Educess_Per
                    objRemittance.Select_By = obj.objPIRemittance.Select_By
                    objRemittance.IsTDSOverride = obj.objPIRemittance.IsTDSOverride
                    objRemittance.IsApplyTDS = obj.objPIRemittance.IsApplyTDS
                    'End If
                End If

                For Each obj1 As clsMilkPurchaseInvoiceMCCDetail In clsMilkPurchaseInvoiceMCC.ObjList
                    gv1.Rows.AddNew()
                    FndSRNNO.Value = IIf(FndSRNNO.Value = "", obj1.SRN_CODE, FndSRNNO.Value & "," & obj1.SRN_CODE)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAMOUNT).Value = obj1.AMOUNT
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCans).Value = obj1.Cans
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCLR).Value = obj1.CLR
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCode).Value = obj1.DOC_CODE
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = obj1.Qty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAcc_Qty).Value = obj1.Acc_Qty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colService_Charge).Value = obj1.Service_Charge
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCOMMISSION).Value = Math.Round(obj1.COMMISSION, 2)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPaymentCOMMISSION).Value = Math.Round(obj1.Payment_COMMISSION, 2)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDeduction).Value = obj1.Deduction
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colOwn_Asset_Amount).Value = obj1.Own_Asset_Amount
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colHead_Load_Amount).Value = obj1.Head_Load_Amount
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCorrection_Factor).Value = obj1.Correction_Factor
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colFAT_PER).Value = obj1.FAT_PER
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIncentive).Value = obj1.Incentive
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIncentiveEMP).Value = obj1.IncentiveEMP
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItem_Code).Value = obj1.Item_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItem_Desc).Value = obj1.Item_Desc
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(obj1.Item_Code, Nothing)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRATE).Value = obj1.RATE
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSNF_PER).Value = obj1.SNF_PER
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSRN_CODE).Value = obj1.SRN_CODE
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSrn_Date).Value = obj1.SRN_DATE
                    ' gv1.Rows(gv1.Rows.Count - 1).Cells(colTOTAL_AMOUNT).Value = obj1.TOTAL_AMOUNT
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUOM).Value = obj1.UOM
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colVEHICLE_NO).Value = obj1.VEHICLE_NO
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colVLC_NO).Value = obj1.VLC_NO
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colServiceChargeAmount).Value = obj1.Service_Charge_Amount
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colHandlingCharges).Value = obj1.Handling_Charges_Amount
                    ' gv1.Rows(gv1.Rows.Count - 1).Cells(colNetAMOUNT).Value = Math.Round(clsCommon.myCdbl(obj1.Net_AMOUNT), DtMCC.Rows(0).Item("FAT_SNF_CALC")) '- clsCommon.myCdbl(obj1.Deduction)
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colNetsaveAMOUNT).Value = Math.Round(obj1.Net_AMOUNT, DtMCC.Rows(0).Item("FAT_SNF_save"))
                Next
            Else
                gv1.Rows.AddNew()
            End If
            UcAttachment1.LoadData(obj.DOC_CODE)
            btnPrint.Enabled = True
            btnBillOfSupply.Enabled = True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetVendorTDSDetails(ByVal trans As SqlTransaction)
        '  btnViewTDSDetails.Enabled = False
        objRemittance = Nothing
        Dim objVendor As clsTDSVendorDetails = clsTDSVendorDetails.GetData(fndVSPCode.Value, trans)
        If objVendor IsNot Nothing Then
            ' btnViewTDSDetails.Enabled = True
            Dim objDedDetails As clsTDSDeductionDetails = clsTDSDeductionDetails.GetApplicableTDRate(objVendor.Nature_Of_Deduction, clsCommon.myCdbl(txtTotComm.Text), trans, False, fndVSPCode.Value)
            If (objDedDetails IsNot Nothing) Then
                objRemittance = New clsRemittance()
                objRemittance.Branch_Code = objVendor.Branch_Code
                objRemittance.Deduction_Code = objVendor.Nature_Of_Deduction
                objRemittance.TDS_Per = objDedDetails.TDS
                objRemittance.Surcharge_Per = objDedDetails.Surcharge
                objRemittance.Edu_Cess_Per = objDedDetails.Educess
                objRemittance.Sec_Educess_Per = objDedDetails.Seceducess
                objRemittance.IsTDSOverride = False
                If isNewEntry Then
                    objRemittance.IsApplyTDS = True
                Else
                    objRemittance.IsApplyTDS = clsPIRemittance.IsTDSApplied(txtCode.Value)
                End If

                objRemittance.Section_Code = objVendor.TDSSection
                objRemittance.Section_Description = objVendor.TDSSectionDescription
                objRemittance.Select_By = objVendor.VendorTypeCode

                Dim qry As String = "select Year_Name from TSPL_TDS_FINANCIAL_YEAR where convert(date,'" + dtpDocDate.Value + "',103)>=  convert(date,From_Date,103)  and convert(date,'" + dtpDocDate.Value + "',103)<=convert(date,To_Date,103) "
                objRemittance.Fiscal_Year = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                objRemittance.Quarter = "First"
            End If
        End If
    End Sub

    Sub UpdateTDSAmount(ByVal trans As SqlTransaction)
        If (objRemittance Is Nothing) Then
            SetVendorTDSDetails(trans)
        Else
            Dim objDedDetails As clsTDSDeductionDetails = clsTDSDeductionDetails.GetApplicableTDRate(objRemittance.Deduction_Code, clsCommon.myCdbl(txtTotComm.Text), trans, False, fndVSPCode.Value)
            If (objDedDetails IsNot Nothing AndAlso objRemittance.IsApplyTDS) Then
                objRemittance.TDS_Per = objDedDetails.TDS
                objRemittance.Surcharge_Per = objDedDetails.Surcharge
                objRemittance.Edu_Cess_Per = objDedDetails.Educess
                objRemittance.Sec_Educess_Per = objDedDetails.Seceducess
            End If
        End If
        If (objRemittance IsNot Nothing) Then
            objRemittance.Vendor_Code = fndVSPCode.Value
            objRemittance.Vendor_Name = lblVSPDesc.Text
            '' objRemittance.Document_No = txtDocNo.SelectedValue   Should pass when saveing the data
            objRemittance.Document_Date = dtpDocDate.Value
            objRemittance.Document_Type = "I" 'Purchase Invoice Type
            objRemittance.Document_Amount = clsCommon.myCdbl(txtTotComm.Text)
            'objRemittance.Service_Type = txtDate.Value

            objRemittance.Calculated_TDS_Base = clsCommon.myCdbl(txtTotComm.Text)
            If Not objRemittance.IsTDSOverride Then
                objRemittance.Actual_TDS_Base = clsCommon.myCdbl(txtTotComm.Text)
            End If

            objRemittance.Calculated_TDS = (objRemittance.Calculated_TDS_Base * objRemittance.TDS_Per) / 100
            objRemittance.Actual_TDS = (objRemittance.Actual_TDS_Base * objRemittance.TDS_Per) / 100

            objRemittance.Calculated_Surcharge = (objRemittance.Calculated_TDS_Base * objRemittance.Surcharge_Per) / 100
            objRemittance.Actual_Surcharge = (objRemittance.Actual_TDS_Base * objRemittance.Surcharge_Per) / 100

            objRemittance.Calculated_Edu_Cess = (objRemittance.Calculated_TDS_Base * objRemittance.Edu_Cess_Per) / 100
            objRemittance.Actual_Edu_Cess = (objRemittance.Actual_TDS_Base * objRemittance.Edu_Cess_Per) / 100

            objRemittance.Calculated_Sec_Educess = (objRemittance.Calculated_TDS_Base * objRemittance.Sec_Educess_Per) / 100
            objRemittance.Actual_Sec_Educess = (objRemittance.Actual_TDS_Base * objRemittance.Sec_Educess_Per) / 100

            objRemittance.Calculated_Total_TDS = objRemittance.Calculated_TDS + objRemittance.Calculated_Surcharge + objRemittance.Calculated_Edu_Cess + objRemittance.Calculated_Sec_Educess
            objRemittance.Actual_Total_TDS = objRemittance.Actual_TDS + objRemittance.Actual_Surcharge + objRemittance.Actual_Edu_Cess + objRemittance.Actual_Sec_Educess
        End If
    End Sub

    'Public Sub SelectMilkSRNItems()
    '    Dim obj As New clsMilkSRNMCC
    '    isInsideLoadData = True
    '    Dim frm As New frmMILKPendingSRN()
    '    frm.VendorCode = fndVSPCode.Value
    '    frm.strCurrCode = FndSRNNO.Value
    '    frm.Frm_date = Fromdate.Value
    '    frm.To_date = ToDate.Value
    '    frm.ShowDialog()
    '    If frm.ArrReturn IsNot Nothing AndAlso frm.ArrReturn.Count > 0 Then
    '        If clsCommon.myLen(frm.ArrReturn(0).DOC_CODE) > 0 Then
    '            obj = clsMilkSRNMCC.GetData(frm.ArrReturn(0).DOC_CODE, NavigatorType.Current)
    '            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.DOC_CODE) > 0 Then
    '                '            txtCode.Value = obj.DOC_CODE
    '                If dtpDocDate.MinDate < obj.DOC_DATE Then
    '                    dtpDocDate.MinDate = obj.DOC_DATE
    '                End If
    '                FndMccCode.Value = obj.MCC_CODE
    '                'If clsCommon.myLen(obj.MCC_CODE) > 0 Then
    '                '    Payment_Cycle_value = clsDBFuncationality.getSingleValue("SELECT payment_cycle from tspl_mcc_master where mcc_Code='" & obj.MCC_CODE & "'")
    '                'End If
    '                DtMCC = clsDBFuncationality.GetDataTable("select * from tspl_Mcc_Master where Mcc_Code='" & clsCommon.myCstr(FndMccCode.Value) & "'")
    '                lblMccName.Text = DtMCC.Rows(0).Item("mcc_name") 'clsDBFuncationality.getSingleValue("select mcc_name from tspl_mcc_master where mcc_Code='" & obj.MCC_CODE & "'")

    '                FndSRNNO.Value = ""
    '                fndVSPCode.Value = obj.VSP_CODE

    '                txtPayment.Text = clsDBFuncationality.getSingleValue("select vsp_Payment from TSPL_VENDOR_MASTER where form_type='VSP' and Vendor_Code='" & fndVSPCode.Value & "'")
    '                fndRouteCOde.Text = obj.ROUTE_CODE


    '                lblRouteDesc.Text = clsDBFuncationality.getSingleValue("select Route_Name from TSPL_MCC_ROUTE_MASTER where Route_Code='" & fndRouteCOde.Text & "'")
    '                ' If LCase(txtPayment.Text) = "different" Then
    '                '   lblVSPDesc.Text = clsDBFuncationality.getSingleValue("select joint_name from TSPL_VENDOR_MASTER where form_type='VSP' and Vendor_Code='" & fndVSPCode.text & "'")
    '                'Else
    '                lblVSPDesc.Text = clsDBFuncationality.getSingleValue("select vendor_name from TSPL_VENDOR_MASTER where  Vendor_Code='" & fndVSPCode.Value & "'")
    '                'End If

    '                'If (obj.ObjList IsNot Nothing AndAlso obj.ObjList.Count > 0) Then
    '                LoadBlankGrid()
    '                ' Dim sQuery As String = "select * from TSPL_MILK_Shift_End_DETAIL where  MCC_CODE='" & clsCommon.myCstr(obj.MCC_CODE) & "' " _
    '                '& "and convert(date,DOC_DATE,103)='" & clsCommon.GetPrintDate(obj.DOC_DATE, "dd-MMM-yyyy") & "' and SHIFT='" & IIf(clsCommon.myCstr(obj.SHIFT) = "M", "Morning", "Evening") & "'"
    '                Dim sQuery As String = "select TSPL_MILK_Shift_End_DETAIL.*,TSPL_MILK_SRN_HEAD.doc_code as srn_code from TSPL_MILK_Shift_End_DETAIL inner join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_head.VLC_DOC_CODE=TSPL_MILK_Shift_End_DETAIL.VLC_DOC_CODE where  TSPL_MILK_Shift_End_DETAIL.MCC_CODE='" & clsCommon.myCstr(obj.MCC_CODE) & "' " _
    '              & "and convert(date,TSPL_MILK_Shift_End_DETAIL.DOC_DATE,103)='" & clsCommon.GetPrintDate(obj.DOC_DATE, "dd-MMM-yyyy") & "' and TSPL_MILK_Shift_End_DETAIL.SHIFT='" & IIf(clsCommon.myCstr(obj.SHIFT) = "M", "Morning", "Evening") & "'"

    '                Dim DtShiftEnd As DataTable = clsDBFuncationality.GetDataTable(sQuery)
    '                For Each obj1 As clsMilkSRNMCCDetail In frm.ArrReturn
    '                    gv1.Rows.AddNew()

    '                    FndSRNNO.Value = IIf(FndSRNNO.Value = "", obj1.DOC_CODE, FndSRNNO.Value & "," & obj1.DOC_CODE)
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCans).Value = obj1.Cans
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCLR).Value = obj1.CLR
    '                    ' gv1.Rows(gv1.Rows.Count - 1).Cells(colCode).Value = obj1.DOC_CODE
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCOMMISSION).Value = obj1.Commission
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colService_Charge).Value = obj1.Service_Charge_Type
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPaymentCOMMISSION).Value = obj1.Payment_Commission

    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCorrection_Factor).Value = obj1.Correction_Factor
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colFAT_PER).Value = obj1.FAT
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIncentive).Value = 0
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIncentiveEMP).Value = 0
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItem_Code).Value = obj1.Item_CODE
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItem_Desc).Value = obj1.Item_Desc
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(obj1.Item_CODE, Nothing)
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = obj1.MILK_Qty
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAcc_Qty).Value = obj1.ACC_Qty
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRATE).Value = obj1.RATE
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSNF_PER).Value = obj1.SNF
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSRN_CODE).Value = obj1.DOC_CODE
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSrn_Date).Value = obj.DOC_DATE
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTOTAL_AMOUNT).Value = obj1.AMOUNT
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUOM).Value = obj1.UOM
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colVEHICLE_NO).Value = obj.VEHICLE_CODE
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colVLC_NO).Value = obj1.VlC_Code
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAMOUNT).Value = obj1.AMOUNT
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colHead_Load_Amount).Value = obj1.Head_Load_Amount
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colOwn_Asset_Amount).Value = obj1.Own_Asset_Amount

    '                    If DtShiftEnd.Rows.Count > 0 Then
    '                        Dim dr() As DataRow = DtShiftEnd.Select("vlc_code='" & clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colVLC_NO).Value) & "' and srn_code='" & clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colSRN_CODE).Value) & "'")
    '                        If dr.Length > 0 Then
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colDeduction).Value = IIf(clsCommon.myCstr(dr(0)("A_Or_R")) = "R", clsCommon.myCdbl(gv1.Rows(gv1.Rows.Count - 1).Cells(colAMOUNT).Value) * clsCommon.myCdbl(dr(0)("Deduction_of_VSP")) / 100, clsCommon.myCdbl(dr(0)("Deduction_of_VSP")))
    '                        End If
    '                    End If
    '                    ' gv1.Rows(gv1.Rows.Count - 1).Cells(colDeduction).Value = 0
    '                Next
    '                'Else
    '                '    gv1.Rows.AddNew()
    '                'End If



    '                'If rbtnTaxCalManual.IsChecked Then ''For Calcuation custom tax according to ratio of amount
    '                '    For ii As Integer = 0 To gv1.RowCount - 1
    '                '        UpdateCurrentRow(ii)
    '                '    Next
    '                'End If
    '            End If
    '        End If
    '    End If
    '    isInsideLoadData = False
    '    ' UpdateAllTotals()
    '    'RefreshReqNo()
    '    'SetVendorTDSDetails()
    'End Sub

    'Public Sub SelectMilkSRNItemsForVspPayment(ByVal strSRN_No As List(Of String), ByVal Vsp_Name As String, ByVal frm_date As Date, ByVal End_date As Date, ByVal Is_With_Bill As Boolean, ByVal trans As SqlTransaction)
    '    Dim obj As New clsMilkSRNMCC
    '    isInsideLoadData = True
    '    Dim frm As New frmMILKPendingSRN()
    '    frm.VendorCode = Vsp_Name
    '    frm.strCurrCode = FndSRNNO.Value
    '    frm.Frm_date = frm_date
    '    frm.To_date = End_date
    '    Dim StrDoc As New List(Of String)
    '    If Is_With_Bill Then
    '        If Not frm.LoaDHeadDataQuery(trans) Then
    '            Exit Sub
    '        End If
    '    Else
    '        If Not frm.LoaDHeadDataQueryVsp(trans) Then
    '            Exit Sub
    '        End If
    '    End If
    '    'frm.ShowDialog()
    '    'For Each Get_srn_no As String In strSRN_No
    '    For Each row As GridViewRowInfo In frm.gvHead.Rows()
    '        If strSRN_No.Contains(clsCommon.myCstr(row.Cells(frmMILKPendingSRN.colHCode).Value)) Then
    '            frm.gvHead.CurrentRow = row
    '            row.Cells(frmMILKPendingSRN.colHSelect).Value = True
    '            'frm.LoadDetailData(True, clsCommon.myCstr(row.Cells(frm.colHCode).Value))
    '        End If
    '    Next
    '    'Next
    '    frm.btnOKPressed()
    '    If frm.ArrReturn IsNot Nothing AndAlso frm.ArrReturn.Count > 0 Then
    '        If clsCommon.myLen(frm.ArrReturn(0).DOC_CODE) > 0 Then
    '            obj = clsMilkSRNMCC.GetData(frm.ArrReturn(0).DOC_CODE, NavigatorType.Current, trans)
    '            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.DOC_CODE) > 0 Then
    '                '            txtCode.Value = obj.DOC_CODE
    '                If dtpDocDate.MinDate < obj.DOC_DATE Then
    '                    dtpDocDate.MinDate = obj.DOC_DATE
    '                End If
    '                FndMccCode.Value = obj.MCC_CODE
    '                'If clsCommon.myLen(obj.MCC_CODE) > 0 Then
    '                '    Payment_Cycle_value = clsDBFuncationality.getSingleValue("SELECT payment_cycle from tspl_mcc_master where mcc_Code='" & obj.MCC_CODE & "'")
    '                'End If
    '                DtMCC = clsDBFuncationality.GetDataTable("select * from tspl_Mcc_Master where Mcc_Code='" & clsCommon.myCstr(FndMccCode.Value) & "'", trans)
    '                lblMccName.Text = DtMCC.Rows(0).Item("mcc_name") 'clsDBFuncationality.getSingleValue("select mcc_name from tspl_mcc_master where mcc_Code='" & obj.MCC_CODE & "'")

    '                'dtpDocDate.Value = obj.DOC_DATE

    '                FndSRNNO.Value = ""
    '                fndVSPCode.Value = obj.VSP_CODE

    '                txtPayment.Text = clsDBFuncationality.getSingleValue("select vsp_Payment from TSPL_VENDOR_MASTER where form_type='VSP' and Vendor_Code='" & fndVSPCode.Value & "'", trans)
    '                fndRouteCOde.Text = obj.ROUTE_CODE


    '                lblRouteDesc.Text = clsDBFuncationality.getSingleValue("select Route_Name from TSPL_MCC_ROUTE_MASTER where Route_Code='" & fndRouteCOde.Text & "'", trans)
    '                ' If LCase(txtPayment.Text) = "different" Then
    '                '   lblVSPDesc.Text = clsDBFuncationality.getSingleValue("select joint_name from TSPL_VENDOR_MASTER where form_type='VSP' and Vendor_Code='" & fndVSPCode.text & "'")
    '                'Else
    '                lblVSPDesc.Text = clsDBFuncationality.getSingleValue("select vendor_name from TSPL_VENDOR_MASTER where form_type='VSP' and Vendor_Code='" & fndVSPCode.Value & "'", trans)
    '                'End If

    '                'If (obj.ObjList IsNot Nothing AndAlso obj.ObjList.Count > 0) Then
    '                LoadBlankGridVSpPay()
    '                ' Dim sQuery As String = "select * from TSPL_MILK_Shift_End_DETAIL where  MCC_CODE='" & clsCommon.myCstr(obj.MCC_CODE) & "' " _
    '                '& "and convert(date,DOC_DATE,103)='" & clsCommon.GetPrintDate(obj.DOC_DATE, "dd-MMM-yyyy") & "' and SHIFT='" & IIf(clsCommon.myCstr(obj.SHIFT) = "M", "Morning", "Evening") & "'"
    '                Dim sQuery As String = "select TSPL_MILK_Shift_End_DETAIL.*,TSPL_MILK_SRN_HEAD.doc_code as srn_code from TSPL_MILK_Shift_End_DETAIL inner join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_head.VLC_DOC_CODE=TSPL_MILK_Shift_End_DETAIL.VLC_DOC_CODE where  TSPL_MILK_Shift_End_DETAIL.MCC_CODE='" & clsCommon.myCstr(obj.MCC_CODE) & "' " _
    '              & "and convert(date,TSPL_MILK_Shift_End_DETAIL.DOC_DATE,103)='" & clsCommon.GetPrintDate(obj.DOC_DATE, "dd-MMM-yyyy") & "' and TSPL_MILK_Shift_End_DETAIL.SHIFT='" & IIf(clsCommon.myCstr(obj.SHIFT) = "M", "Morning", "Evening") & "'"

    '                Dim DtShiftEnd As DataTable = clsDBFuncationality.GetDataTable(sQuery, trans)
    '                For Each obj1 As clsMilkSRNMCCDetail In frm.ArrReturn
    '                    gv1.Rows.AddNew()

    '                    FndSRNNO.Value = IIf(FndSRNNO.Value = "", obj1.DOC_CODE, FndSRNNO.Value & "," & obj1.DOC_CODE)
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCans).Value = obj1.Cans
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCLR).Value = obj1.CLR
    '                    ' gv1.Rows(gv1.Rows.Count - 1).Cells(colCode).Value = obj1.DOC_CODE
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = obj1.MILK_Qty
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAcc_Qty).Value = obj1.ACC_Qty
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colService_Charge).Value = obj1.Service_Charge_Type


    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCorrection_Factor).Value = obj1.Correction_Factor
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colFAT_PER).Value = obj1.FAT
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIncentive).Value = 0 '0
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIncentiveEMP).Value = 0
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItem_Code).Value = obj1.Item_CODE
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItem_Desc).Value = obj1.Item_Desc
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(obj1.Item_CODE, Nothing)
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRATE).Value = obj1.RATE
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSNF_PER).Value = obj1.SNF
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSRN_CODE).Value = obj1.DOC_CODE
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSrn_Date).Value = obj.DOC_DATE
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTOTAL_AMOUNT).Value = obj1.AMOUNT
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUOM).Value = obj1.UOM
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colVEHICLE_NO).Value = obj.VEHICLE_CODE
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colVLC_NO).Value = obj1.VlC_Code
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAMOUNT).Value = obj1.AMOUNT
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colHead_Load_Amount).Value = obj1.Head_Load_Amount
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colOwn_Asset_Amount).Value = obj1.Own_Asset_Amount

    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPaymentCOMMISSION).Value = obj1.Payment_Commission
    '                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCOMMISSION).Value = obj1.Commission
    '                    If DtShiftEnd.Rows.Count > 0 Then
    '                        Dim dr() As DataRow = DtShiftEnd.Select("vlc_code='" & clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colVLC_NO).Value) & "' and srn_code='" & clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colSRN_CODE).Value) & "'")
    '                        'Dim dr() As DataRow = DtShiftEnd.Select("vlc_code='" & clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colVLC_NO).Value) & "'")
    '                        If dr.Length > 0 Then
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colDeduction).Value = IIf(clsCommon.myCstr(dr(0)("A_Or_R")) = "R", clsCommon.myCdbl(gv1.Rows(gv1.Rows.Count - 1).Cells(colAMOUNT).Value) * clsCommon.myCdbl(dr(0)("Deduction_of_VSP")) / 100, clsCommon.myCdbl(dr(0)("Deduction_of_VSP")))
    '                        End If
    '                    End If
    '                    If Not Is_With_Bill Then
    '                        If Not StrDoc.Contains(obj1.Invoice_Code) Then
    '                            StrDoc.Add(obj1.Invoice_Code)
    '                        End If
    '                    End If
    '                    ' gv1.Rows(gv1.Rows.Count - 1).Cells(colDeduction).Value = 0
    '                Next
    '                'Else
    '                '    gv1.Rows.AddNew()
    '                'End If



    '                'If rbtnTaxCalManual.IsChecked Then ''For Calcuation custom tax according to ratio of amount
    '                '    For ii As Integer = 0 To gv1.RowCount - 1
    '                '        UpdateCurrentRow(ii)
    '                '    Next
    '                'End If
    '            End If
    '        End If
    '        If Is_With_Bill Then
    '            SaveDataForVspPayment(trans)
    '            ' Dim dt_Is_iregular As DataTable = clsDBFuncationality.getSingleValue("select * from TSPL_Open_Mcc_Shift where Mcc_Code='" & obj.MCC_CODE & "' and status='O' and is_regular=0", trans)
    '            'If dt_Is_iregular.Rows.Count <= 0 Then
    '            '    clsMilkPurchaseInvoiceMCC.PostData("M-PURINVOICE", txtCode.Value, trans)
    '            'Else
    '            clsMilkPurchaseInvoiceMCC.PostData("M-PURINVOICE", txtCode.Value, trans)
    '            'End If

    '            'clsMilkPurchaseInvoiceMCC.PostIncentiveData(Form_ID, txtCode.Value, fndVSPCode.Value, frm_date, End_date, trans)
    '        Else
    '            clsMilkPurchaseInvoiceMCC.PostIncentiveData_Month_and_Year_Wise(Form_ID, StrDoc, fndVSPCode.Value, frm_date, End_date, trans)
    '        End If

    '    End If
    '    isInsideLoadData = False

    '    ' UpdateAllTotals()
    '    'RefreshReqNo()
    '    'SetVendorTDSDetails()
    'End Sub

    'Public Sub Save_VSP_incentive(ByVal strSRN_No As List(Of String), ByVal Vsp_Name As String, ByVal frm_date As Date, ByVal End_date As Date, ByVal Payment_cycle As Integer, ByVal trans As SqlTransaction)
    '    Dim obj As New clsMilkSRNMCC
    '    Dim DtIncentive As DataTable = clsMilkPurchaseInvoiceMCC.GetIncentive(Vsp_Name)
    '    If DtIncentive.Rows.Count > 0 Then
    '        If DtIncentive.Rows(0).Item("Scheme_For") = "Day" Or DtIncentive.Rows(0).Item("Scheme_For") = "PC" Then
    '            isInsideLoadData = True
    '            Dim frm As New frmMILKPendingSRN()
    '            frm.VendorCode = Vsp_Name
    '            frm.strCurrCode = FndSRNNO.Value
    '            frm.Frm_date = frm_date
    '            frm.To_date = End_date
    '            If Not frm.LoaDHeadDataQuery_For_Incentive(trans) Then
    '                Exit Sub
    '            End If
    '            'frm.ShowDialog()
    '            'For Each Get_srn_no As String In strSRN_No
    '            For Each row As GridViewRowInfo In frm.gvHead.Rows()
    '                If strSRN_No.Contains(clsCommon.myCstr(row.Cells(frm.colHCode).Value)) Then
    '                    frm.gvHead.CurrentRow = row
    '                    row.Cells(frm.colHSelect).Value = True
    '                    'frm.LoadDetailData(True, clsCommon.myCstr(row.Cells(frm.colHCode).Value))
    '                End If
    '            Next
    '            'Next
    '            frm.btnOKPressed()
    '            If frm.ArrReturn IsNot Nothing AndAlso frm.ArrReturn.Count > 0 Then
    '                If clsCommon.myLen(frm.ArrReturn(0).DOC_CODE) > 0 Then
    '                    obj = clsMilkSRNMCC.GetData(frm.ArrReturn(0).DOC_CODE, NavigatorType.Current, trans)
    '                    If obj IsNot Nothing AndAlso clsCommon.myLen(obj.DOC_CODE) > 0 Then
    '                        '            txtCode.Value = obj.DOC_CODE
    '                        If dtpDocDate.MinDate < obj.DOC_DATE Then
    '                            dtpDocDate.MinDate = obj.DOC_DATE
    '                        End If
    '                        fndMccCode.value = obj.MCC_CODE
    '                        'If clsCommon.myLen(obj.MCC_CODE) > 0 Then
    '                        '    Payment_Cycle_value = clsDBFuncationality.getSingleValue("SELECT payment_cycle from tspl_mcc_master where mcc_Code='" & obj.MCC_CODE & "'")
    '                        'End If
    '                        DtMCC = clsDBFuncationality.GetDataTable("select * from tspl_Mcc_Master where Mcc_Code='" & clsCommon.myCstr(fndMccCode.value) & "'", trans)
    '                        lblMccName.Text = DtMCC.Rows(0).Item("mcc_name") 'clsDBFuncationality.getSingleValue("select mcc_name from tspl_mcc_master where mcc_Code='" & obj.MCC_CODE & "'")

    '                        FndSRNNO.Value = ""
    '                        fndVSPCode.Value = obj.VSP_CODE

    '                        txtPayment.Text = clsDBFuncationality.getSingleValue("select vsp_Payment from TSPL_VENDOR_MASTER where form_type='VSP' and Vendor_Code='" & fndVSPCode.Value & "'", trans)
    '                        fndRouteCOde.Text = obj.ROUTE_CODE


    '                        lblRouteDesc.Text = clsDBFuncationality.getSingleValue("select Route_Name from TSPL_MCC_ROUTE_MASTER where Route_Code='" & fndRouteCOde.Text & "'", trans)
    '                        ' If LCase(txtPayment.Text) = "different" Then
    '                        '   lblVSPDesc.Text = clsDBFuncationality.getSingleValue("select joint_name from TSPL_VENDOR_MASTER where form_type='VSP' and Vendor_Code='" & fndVSPCode.text & "'")
    '                        'Else
    '                        lblVSPDesc.Text = clsDBFuncationality.getSingleValue("select vendor_name from TSPL_VENDOR_MASTER where form_type='VSP' and Vendor_Code='" & fndVSPCode.Value & "'", trans)
    '                        'End If

    '                        'If (obj.ObjList IsNot Nothing AndAlso obj.ObjList.Count > 0) Then
    '                        LoadBlankGridVSpPay()
    '                        Dim sQuery As String = "select * from TSPL_MILK_Shift_End_DETAIL where  MCC_CODE='" & clsCommon.myCstr(obj.MCC_CODE) & "' " _
    '                       & "and convert(date,DOC_DATE,103)='" & clsCommon.GetPrintDate(obj.DOC_DATE, "dd-MMM-yyyy") & "' and SHIFT='" & IIf(clsCommon.myCstr(obj.SHIFT) = "M", "Morning", "Evening") & "'"
    '                        Dim DtShiftEnd As DataTable = clsDBFuncationality.GetDataTable(sQuery, trans)
    '                        For Each obj1 As clsMilkSRNMCCDetail In frm.ArrReturn
    '                            gv1.Rows.AddNew()

    '                            FndSRNNO.Value = IIf(FndSRNNO.Value = "", obj1.DOC_CODE, FndSRNNO.Value & "," & obj1.DOC_CODE)
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCans).Value = obj1.Cans
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCLR).Value = obj1.CLR
    '                            ' gv1.Rows(gv1.Rows.Count - 1).Cells(colCode).Value = obj1.DOC_CODE
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCOMMISSION).Value = obj1.Commission
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colPaymentCOMMISSION).Value = obj1.Payment_Commission

    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCorrection_Factor).Value = obj1.Correction_Factor
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colFAT_PER).Value = obj1.FAT
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIncentive).Value = 0
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colItem_Code).Value = obj1.Item_CODE
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colItem_Desc).Value = obj1.Item_Desc
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value += obj1.MILK_Qty
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colRATE).Value = obj1.RATE
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSNF_PER).Value = obj1.SNF
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSRN_CODE).Value = obj1.DOC_CODE
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSrn_Date).Value = obj.DOC_DATE
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTOTAL_AMOUNT).Value = obj1.AMOUNT
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colUOM).Value = obj1.UOM
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colVEHICLE_NO).Value = obj.VEHICLE_CODE
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colVLC_NO).Value = obj1.VlC_Code
    '                            gv1.Rows(gv1.Rows.Count - 1).Cells(colAMOUNT).Value = obj1.AMOUNT

    '                            If DtShiftEnd.Rows.Count > 0 Then
    '                                Dim dr() As DataRow = DtShiftEnd.Select("vlc_code='" & clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colVLC_NO).Value) & "'")
    '                                If dr.Length > 0 Then
    '                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDeduction).Value = IIf(clsCommon.myCstr(dr(0)("A_Or_R")) = "R", clsCommon.myCdbl(gv1.Rows(gv1.Rows.Count - 1).Cells(colAMOUNT).Value) * clsCommon.myCdbl(dr(0)("Deduction_of_VSP")) / 100, clsCommon.myCdbl(dr(0)("Deduction_of_VSP")))
    '                                End If
    '                            End If
    '                            ' gv1.Rows(gv1.Rows.Count - 1).Cells(colDeduction).Value = 0
    '                        Next
    '                    End If
    '                End If
    '                SaveDataForVspPayment(trans)
    '                clsMilkPurchaseInvoiceMCC.PostData("M-PURINVOICE", txtCode.Value, trans)
    '            End If
    '            isInsideLoadData = False
    '        End If
    '    End If
    'End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCode.FormatString = ""
        repoCode.HeaderText = "Code"
        repoCode.Name = colCode
        repoCode.Width = 0
        repoCode.IsVisible = False
        repoCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCode)

        Dim repoEmpCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoEmpCode.FormatString = ""
        repoEmpCode.HeaderText = "SRN No"
        repoEmpCode.Name = colSRN_CODE
        repoEmpCode.Width = 100
        repoEmpCode.IsVisible = True
        repoEmpCode.ReadOnly = True
        repoEmpCode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repoEmpCode)

        Dim repoTaskDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoTaskDate.Format = DateTimePickerFormat.Custom
        repoTaskDate.CustomFormat = "dd-MMM-yyyy"
        repoTaskDate.HeaderText = "SRN Date"
        repoTaskDate.FormatString = "{0:d}"
        repoTaskDate.Name = colSrn_Date
        repoTaskDate.WrapText = True
        repoTaskDate.ReadOnly = True
        repoTaskDate.Width = 100
        gv1.MasterTemplate.Columns.Add(repoTaskDate)


        Dim repoProjectCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoProjectCode.FormatString = ""
        repoProjectCode.HeaderText = "Vehicle Code"
        repoProjectCode.Name = colVEHICLE_NO
        repoProjectCode.Width = 100
        repoProjectCode.ReadOnly = True
        repoProjectCode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repoProjectCode)

        Dim repoProjectDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoProjectDesc.FormatString = ""
        repoProjectDesc.HeaderText = "VlC Code"
        repoProjectDesc.Name = colVLC_NO
        repoProjectDesc.Width = 120
        repoProjectDesc.ReadOnly = True
        repoProjectDesc.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repoProjectDesc)

        Dim repoCustDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCustDesc.FormatString = ""
        repoCustDesc.HeaderText = "Item Code"
        repoCustDesc.Name = colItem_Code
        repoCustDesc.Width = 120
        repoCustDesc.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCustDesc)


        Dim repoItemDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoItemDesc.FormatString = ""
        repoItemDesc.HeaderText = "Item Desc"
        repoItemDesc.Name = colItem_Desc
        repoItemDesc.Width = 120
        repoItemDesc.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoItemDesc)

        Dim repoHSNCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoHSNCode.FormatString = ""
        repoHSNCode.HeaderText = "HSN Code"
        repoHSNCode.Name = colHSNNo
        repoHSNCode.Width = 120
        repoHSNCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoHSNCode)

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Qty"
        repoQty.Name = colQty
        repoQty.IsVisible = True
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoQty.ReadOnly = True
        repoQty.WrapText = True
        repoQty.Width = 70
        gv1.MasterTemplate.Columns.Add(repoQty)


        Dim repoUOM As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUOM.FormatString = ""
        repoUOM.HeaderText = "UOM"
        repoUOM.Name = colUOM
        repoUOM.Width = 120
        repoUOM.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoUOM)

        Dim repoAccQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAccQty = New GridViewDecimalColumn()
        repoAccQty.FormatString = ""
        repoAccQty.HeaderText = "Actual Qty(KG)"
        repoAccQty.Name = colAcc_Qty
        repoAccQty.IsVisible = True
        repoAccQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAccQty.ReadOnly = True
        repoAccQty.WrapText = True
        repoAccQty.Width = 70
        gv1.MasterTemplate.Columns.Add(repoAccQty)

        Dim repoCustCode As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCustCode.FormatString = ""
        repoCustCode.HeaderText = "Cans"
        repoCustCode.Name = colCans
        repoCustCode.Width = 100
        repoCustCode.ReadOnly = True
        repoCustCode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repoCustCode)

        Dim repoFAT As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFAT = New GridViewDecimalColumn()
        repoFAT.FormatString = ""
        repoFAT.HeaderText = "FAT"
        repoFAT.Name = colFAT_PER
        repoFAT.IsVisible = True
        repoFAT.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoFAT.ReadOnly = True
        repoFAT.WrapText = True
        repoFAT.Width = 70
        gv1.MasterTemplate.Columns.Add(repoFAT)


        Dim repoSNF As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSNF = New GridViewDecimalColumn()
        repoSNF.FormatString = ""
        repoSNF.HeaderText = "SNF"
        repoSNF.Name = colSNF_PER
        repoSNF.IsVisible = True
        repoSNF.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoSNF.ReadOnly = True
        repoSNF.WrapText = True
        repoSNF.Width = 70
        gv1.MasterTemplate.Columns.Add(repoSNF)

        Dim repoCORR As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCORR = New GridViewDecimalColumn()
        repoCORR.FormatString = ""
        repoCORR.HeaderText = "Correction Factor"
        repoCORR.Name = colCorrection_Factor
        repoCORR.IsVisible = True
        repoCORR.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoCORR.ReadOnly = True
        repoCORR.WrapText = True
        repoCORR.Width = 70
        gv1.MasterTemplate.Columns.Add(repoCORR)

        Dim repoCLR As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCLR = New GridViewDecimalColumn()
        repoCLR.FormatString = ""
        repoCLR.HeaderText = "CLR"
        repoCLR.Name = colCLR
        repoCLR.IsVisible = True
        repoCLR.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoCLR.ReadOnly = True
        repoCLR.WrapText = True
        repoCLR.Width = 70
        gv1.MasterTemplate.Columns.Add(repoCLR)


        Dim repoUnitCost As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoUnitCost.FormatString = ""
        repoUnitCost.HeaderText = "Rate"
        repoUnitCost.Name = colRATE
        repoUnitCost.IsVisible = True
        repoUnitCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoUnitCost.ReadOnly = True
        repoUnitCost.WrapText = True
        repoUnitCost.Width = 80
        gv1.MasterTemplate.Columns.Add(repoUnitCost)

        Dim repoTotalCost As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalCost.FormatString = ""
        repoTotalCost.HeaderText = "Amount"
        repoTotalCost.Name = colAMOUNT
        repoTotalCost.IsVisible = True
        repoTotalCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTotalCost.ReadOnly = True
        repoTotalCost.WrapText = True
        repoTotalCost.Width = 100
        gv1.MasterTemplate.Columns.Add(repoTotalCost)

        Dim repoBillingRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBillingRate.FormatString = ""
        repoBillingRate.HeaderText = "Incentive"
        repoBillingRate.Name = colIncentive
        repoBillingRate.IsVisible = True
        repoBillingRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoBillingRate.IsVisible = False
        repoBillingRate.WrapText = True
        repoBillingRate.Width = 0
        gv1.MasterTemplate.Columns.Add(repoBillingRate)

        Dim repoIncentiveEMP As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoIncentiveEMP.FormatString = ""
        repoIncentiveEMP.HeaderText = "Incentive EMP"
        repoIncentiveEMP.Name = colIncentiveEMP
        repoIncentiveEMP.IsVisible = True
        repoIncentiveEMP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoIncentiveEMP.IsVisible = False
        repoIncentiveEMP.WrapText = True
        repoIncentiveEMP.Width = 0
        gv1.MasterTemplate.Columns.Add(repoIncentiveEMP)

        Dim repoService_Charge As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoService_Charge.FormatString = ""
        repoService_Charge.HeaderText = "Service Charge Type"
        repoService_Charge.Name = colService_Charge
        repoService_Charge.Width = 120
        repoService_Charge.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoService_Charge)

        Dim repoTotalBilling As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalBilling.FormatString = ""
        repoTotalBilling.HeaderText = "Service Charge(%)"
        repoTotalBilling.Name = colCOMMISSION
        repoTotalBilling.IsVisible = True
        repoTotalBilling.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTotalBilling.ReadOnly = True
        repoTotalBilling.WrapText = True
        repoTotalBilling.IsVisible = False
        repoTotalBilling.Width = 0
        gv1.MasterTemplate.Columns.Add(repoTotalBilling)

        Dim repoCommissionAmount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCommissionAmount.FormatString = ""
        repoCommissionAmount.HeaderText = "Service Charge Amount"
        repoCommissionAmount.Name = colCOMMISSIONAmount
        repoCommissionAmount.IsVisible = True
        repoCommissionAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoCommissionAmount.ReadOnly = True
        repoCommissionAmount.WrapText = True
        repoCommissionAmount.Width = 100
        repoCommissionAmount.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoCommissionAmount)

        Dim repoPaymentComm As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoPaymentComm.FormatString = ""
        repoPaymentComm.HeaderText = "EMP(%)"
        repoPaymentComm.Name = colPaymentCOMMISSION
        repoPaymentComm.IsVisible = True
        repoPaymentComm.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoPaymentComm.ReadOnly = True
        repoPaymentComm.WrapText = True
        repoPaymentComm.Width = 0
        repoPaymentComm.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoPaymentComm)

        Dim repoPaymentCommAmount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoPaymentCommAmount.FormatString = ""
        repoPaymentCommAmount.HeaderText = "EMP"
        repoPaymentCommAmount.Name = colPaymentCOMMISSIONAmount
        repoPaymentCommAmount.IsVisible = True
        repoPaymentCommAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoPaymentCommAmount.ReadOnly = True
        repoPaymentCommAmount.WrapText = True
        ' repoPaymentCommAmount.IsVisible = False
        repoPaymentCommAmount.Width = 100
        gv1.MasterTemplate.Columns.Add(repoPaymentCommAmount)


        Dim repoTotalAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalAmt.FormatString = ""
        repoTotalAmt.HeaderText = "Total Amount(Document)"
        repoTotalAmt.Name = colDocument_AMOUNT
        repoTotalAmt.IsVisible = True
        repoTotalAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTotalAmt.ReadOnly = True
        repoTotalAmt.WrapText = True
        repoTotalAmt.Width = 100
        repoTotalAmt.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTotalAmt)


       
        Dim repoTotalAmtAcc As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalAmtAcc.FormatString = ""
        repoTotalAmtAcc.HeaderText = "Total Amount(Actual)"
        repoTotalAmtAcc.Name = colTOTAL_AMOUNT
        repoTotalAmtAcc.IsVisible = True
        repoTotalAmtAcc.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTotalAmtAcc.ReadOnly = True
        repoTotalAmtAcc.WrapText = True
        repoTotalAmtAcc.Width = 120
        'repoTotalAmtAcc.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTotalAmtAcc)

        Dim repodeduction As GridViewDecimalColumn = New GridViewDecimalColumn()
        repodeduction.FormatString = ""
        repodeduction.HeaderText = "Deduction"
        repodeduction.Name = colDeduction
        repodeduction.IsVisible = True
        repodeduction.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repodeduction.ReadOnly = True
        repodeduction.WrapText = True
        repodeduction.Width = 100
        gv1.MasterTemplate.Columns.Add(repodeduction)

        Dim repoHeadLoad As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoHeadLoad.FormatString = ""
        repoHeadLoad.HeaderText = "Head Load Amount"
        repoHeadLoad.Name = colHead_Load_Amount
        repoHeadLoad.IsVisible = True
        repoHeadLoad.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoHeadLoad.ReadOnly = True
        repoHeadLoad.WrapText = True
        repoHeadLoad.Width = 100
        gv1.MasterTemplate.Columns.Add(repoHeadLoad)

        Dim repoOwnAsset As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoOwnAsset.FormatString = ""
        repoOwnAsset.HeaderText = "Own Asset Amount"
        repoOwnAsset.Name = colOwn_Asset_Amount
        repoOwnAsset.IsVisible = True
        repoOwnAsset.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoOwnAsset.ReadOnly = True
        repoOwnAsset.WrapText = True
        repoOwnAsset.Width = 100
        gv1.MasterTemplate.Columns.Add(repoOwnAsset)

        Dim repoNetTotalCost As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNetTotalCost.FormatString = ""
        repoNetTotalCost.HeaderText = "Service Charge Amount"
        repoNetTotalCost.Name = colServiceChargeAmount
        repoNetTotalCost.IsVisible = True
        repoNetTotalCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoNetTotalCost.ReadOnly = True
        repoNetTotalCost.WrapText = True
        repoNetTotalCost.Width = 100
        gv1.MasterTemplate.Columns.Add(repoNetTotalCost)

        repoNetTotalCost = New GridViewDecimalColumn()
        repoNetTotalCost.FormatString = ""
        repoNetTotalCost.HeaderText = "Handling Charges"
        repoNetTotalCost.Name = colHandlingCharges
        repoNetTotalCost.IsVisible = True
        repoNetTotalCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoNetTotalCost.ReadOnly = True
        repoNetTotalCost.WrapText = True
        ' repoNetTotalCost.IsVisible = False
        repoNetTotalCost.Width = 100
        gv1.MasterTemplate.Columns.Add(repoNetTotalCost)

        repoNetTotalCost = New GridViewDecimalColumn()
        repoNetTotalCost.FormatString = ""
        repoNetTotalCost.HeaderText = "Net Amount"
        repoNetTotalCost.Name = colNetAMOUNT
        repoNetTotalCost.IsVisible = True
        repoNetTotalCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoNetTotalCost.ReadOnly = True
        repoNetTotalCost.WrapText = True
        ' repoNetTotalCost.IsVisible = False
        repoNetTotalCost.Width = 100
        gv1.MasterTemplate.Columns.Add(repoNetTotalCost)


        Dim repoNetSaveTotalCost As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNetSaveTotalCost.FormatString = ""
        repoNetSaveTotalCost.HeaderText = "Net Save Amount"
        repoNetSaveTotalCost.Name = colNetsaveAMOUNT
        repoNetSaveTotalCost.IsVisible = True
        repoNetSaveTotalCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoNetSaveTotalCost.ReadOnly = True
        repoNetSaveTotalCost.WrapText = True
        repoNetSaveTotalCost.IsVisible = False
        repoNetSaveTotalCost.Width = 0
        gv1.MasterTemplate.Columns.Add(repoNetSaveTotalCost)



        clsCustomFieldGrid.LoadBlankGrid(gv1, MyBase.ArrDetailFields)

        gv1.AllowDeleteRow = False
        gv1.AllowAddNewRow = False
        gv1.AllowEditRow = True
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = True
        gv1.EnableFiltering = True
        gv1.EnableAlternatingRowColor = True
        gv1.AutoSizeRows = False
        gv1.AllowRowResize = True
        gv1.VerticalScrollState = ScrollState.AlwaysShow
        gv1.HorizontalScrollState = ScrollState.AlwaysShow
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.ShowFilteringRow = True
        ReStoreGridLayout()
    End Sub

    Sub LoadBlankGridVSpPay()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCode.FormatString = ""
        repoCode.HeaderText = "Code"
        repoCode.Name = colCode
        repoCode.Width = 0
        repoCode.IsVisible = False
        repoCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCode)

        Dim repoEmpCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoEmpCode.FormatString = ""
        repoEmpCode.HeaderText = "SRN No"
        repoEmpCode.Name = colSRN_CODE
        repoEmpCode.Width = 100
        repoEmpCode.IsVisible = True
        repoEmpCode.ReadOnly = True
        repoEmpCode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repoEmpCode)

        Dim repoTaskDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoTaskDate.Format = DateTimePickerFormat.Custom
        repoTaskDate.CustomFormat = "dd-MMM-yyyy"
        repoTaskDate.HeaderText = "SRN Date"
        repoTaskDate.FormatString = "{0:d}"
        repoTaskDate.Name = colSrn_Date
        repoTaskDate.WrapText = True
        repoTaskDate.ReadOnly = True
        repoTaskDate.Width = 100
        gv1.MasterTemplate.Columns.Add(repoTaskDate)


        Dim repoProjectCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoProjectCode.FormatString = ""
        repoProjectCode.HeaderText = "Vehicle Code"
        repoProjectCode.Name = colVEHICLE_NO
        repoProjectCode.Width = 100
        repoProjectCode.ReadOnly = True
        repoProjectCode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repoProjectCode)

        Dim repoProjectDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoProjectDesc.FormatString = ""
        repoProjectDesc.HeaderText = "VlC Code"
        repoProjectDesc.Name = colVLC_NO
        repoProjectDesc.Width = 120
        repoProjectDesc.ReadOnly = True
        repoProjectDesc.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repoProjectDesc)

        Dim repoCustDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCustDesc.FormatString = ""
        repoCustDesc.HeaderText = "Item Code"
        repoCustDesc.Name = colItem_Code
        repoCustDesc.Width = 120
        repoCustDesc.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCustDesc)


        Dim repoItemDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoItemDesc.FormatString = ""
        repoItemDesc.HeaderText = "Item Desc"
        repoItemDesc.Name = colItem_Desc
        repoItemDesc.Width = 120
        repoItemDesc.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoItemDesc)

        Dim repoHSNCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoHSNCode.FormatString = ""
        repoHSNCode.HeaderText = "HSN Code"
        repoHSNCode.Name = colHSNNo
        repoHSNCode.Width = 120
        repoHSNCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoHSNCode)

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Qty"
        repoQty.Name = colQty
        repoQty.IsVisible = True
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoQty.ReadOnly = True
        repoQty.WrapText = True
        repoQty.Width = 70
        gv1.MasterTemplate.Columns.Add(repoQty)


        Dim repoUOM As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUOM.FormatString = ""
        repoUOM.HeaderText = "UOM"
        repoUOM.Name = colUOM
        repoUOM.Width = 120
        repoUOM.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoUOM)


        Dim repoAccQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAccQty = New GridViewDecimalColumn()
        repoAccQty.FormatString = ""
        repoAccQty.HeaderText = "Actual Qty(KG)"
        repoAccQty.Name = colAcc_Qty
        repoAccQty.IsVisible = True
        repoAccQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAccQty.ReadOnly = True
        repoAccQty.WrapText = True
        repoAccQty.Width = 70
        gv1.MasterTemplate.Columns.Add(repoAccQty)

        Dim repoCustCode As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCustCode.FormatString = ""
        repoCustCode.HeaderText = "Cans"
        repoCustCode.Name = colCans
        repoCustCode.Width = 100
        repoCustCode.ReadOnly = True
        repoCustCode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repoCustCode)

        Dim repoFAT As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFAT = New GridViewDecimalColumn()
        repoFAT.FormatString = ""
        repoFAT.HeaderText = "FAT"
        repoFAT.Name = colFAT_PER
        repoFAT.IsVisible = True
        repoFAT.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoFAT.ReadOnly = True
        repoFAT.WrapText = True
        repoFAT.Width = 70
        gv1.MasterTemplate.Columns.Add(repoFAT)


        Dim repoSNF As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSNF = New GridViewDecimalColumn()
        repoSNF.FormatString = ""
        repoSNF.HeaderText = "SNF"
        repoSNF.Name = colSNF_PER
        repoSNF.IsVisible = True
        repoSNF.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoSNF.ReadOnly = True
        repoSNF.WrapText = True
        repoSNF.Width = 70
        gv1.MasterTemplate.Columns.Add(repoSNF)

        Dim repoCORR As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCORR = New GridViewDecimalColumn()
        repoCORR.FormatString = ""
        repoCORR.HeaderText = "Correction Factor"
        repoCORR.Name = colCorrection_Factor
        repoCORR.IsVisible = True
        repoCORR.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoCORR.ReadOnly = True
        repoCORR.WrapText = True
        repoCORR.Width = 70
        gv1.MasterTemplate.Columns.Add(repoCORR)

        Dim repoCLR As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCLR = New GridViewDecimalColumn()
        repoCLR.FormatString = ""
        repoCLR.HeaderText = "CLR"
        repoCLR.Name = colCLR
        repoCLR.IsVisible = True
        repoCLR.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoCLR.ReadOnly = True
        repoCLR.WrapText = True
        repoCLR.Width = 70
        gv1.MasterTemplate.Columns.Add(repoCLR)


        Dim repoUnitCost As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoUnitCost.FormatString = ""
        repoUnitCost.HeaderText = "Rate"
        repoUnitCost.Name = colRATE
        repoUnitCost.IsVisible = True
        repoUnitCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoUnitCost.ReadOnly = True
        repoUnitCost.WrapText = True
        repoUnitCost.Width = 80
        gv1.MasterTemplate.Columns.Add(repoUnitCost)

        Dim repoTotalCost As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalCost.FormatString = ""
        repoTotalCost.HeaderText = "Amount"
        repoTotalCost.Name = colAMOUNT
        repoTotalCost.IsVisible = True
        repoTotalCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTotalCost.ReadOnly = True
        repoTotalCost.WrapText = True
        repoTotalCost.Width = 100
        gv1.MasterTemplate.Columns.Add(repoTotalCost)

        Dim repoBillingRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBillingRate.FormatString = ""
        repoBillingRate.HeaderText = "Incentive"
        repoBillingRate.Name = colIncentive
        repoBillingRate.IsVisible = True
        repoBillingRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoBillingRate.ReadOnly = True
        repoBillingRate.WrapText = True
        repoBillingRate.Width = 80
        gv1.MasterTemplate.Columns.Add(repoBillingRate)

        Dim repoIncentiveEMP As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoIncentiveEMP.FormatString = ""
        repoIncentiveEMP.HeaderText = "Incentive EMP"
        repoIncentiveEMP.Name = colIncentiveEMP
        repoIncentiveEMP.IsVisible = True
        repoIncentiveEMP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoIncentiveEMP.IsVisible = False
        repoIncentiveEMP.WrapText = True
        repoIncentiveEMP.Width = 0
        gv1.MasterTemplate.Columns.Add(repoIncentiveEMP)

        Dim repoService_Charge As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoService_Charge.FormatString = ""
        repoService_Charge.HeaderText = "Service Charge Type"
        repoService_Charge.Name = colService_Charge
        repoService_Charge.Width = 120
        repoService_Charge.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoService_Charge)

        Dim repoTotalBilling As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalBilling.FormatString = ""
        repoTotalBilling.HeaderText = "Service Charge(%)"
        repoTotalBilling.Name = colCOMMISSION
        repoTotalBilling.IsVisible = True
        repoTotalBilling.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTotalBilling.ReadOnly = True
        repoTotalBilling.WrapText = True
        repoTotalBilling.IsVisible = False
        repoTotalBilling.Width = 0
        gv1.MasterTemplate.Columns.Add(repoTotalBilling)

        Dim repoCommissionAmount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCommissionAmount.FormatString = ""
        repoCommissionAmount.HeaderText = "Service Charge Amount"
        repoCommissionAmount.Name = colCOMMISSIONAmount
        repoCommissionAmount.IsVisible = True
        repoCommissionAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoCommissionAmount.ReadOnly = True
        repoCommissionAmount.WrapText = True
        repoCommissionAmount.Width = 0
        repoCommissionAmount.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoCommissionAmount)

        Dim repoTotalAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalAmt.FormatString = ""
        repoTotalAmt.HeaderText = "Total Amount(Document)"
        repoTotalAmt.Name = colDocument_AMOUNT
        repoTotalAmt.IsVisible = True
        repoTotalAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTotalAmt.ReadOnly = True
        repoTotalAmt.WrapText = True
        repoTotalAmt.Width = 0
        repoTotalAmt.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTotalAmt)


        Dim repoPaymentComm As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoPaymentComm.FormatString = ""
        repoPaymentComm.HeaderText = "EMP(%)"
        repoPaymentComm.Name = colPaymentCOMMISSION
        repoPaymentComm.IsVisible = True
        repoPaymentComm.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoPaymentComm.ReadOnly = True
        repoPaymentComm.WrapText = True
        repoPaymentComm.Width = 0
        repoPaymentComm.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoPaymentComm)

        Dim repoPaymentCommAmount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoPaymentCommAmount.FormatString = ""
        repoPaymentCommAmount.HeaderText = "EMP"
        repoPaymentCommAmount.Name = colPaymentCOMMISSIONAmount
        repoPaymentCommAmount.IsVisible = True
        repoPaymentCommAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoPaymentCommAmount.ReadOnly = True
        repoPaymentCommAmount.WrapText = True
        repoPaymentCommAmount.Width = 100
        gv1.MasterTemplate.Columns.Add(repoPaymentCommAmount)



        Dim repoTotalAmtAcc As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalAmtAcc.FormatString = ""
        repoTotalAmtAcc.HeaderText = "Total Amount(Actual)"
        repoTotalAmtAcc.Name = colTOTAL_AMOUNT
        repoTotalAmtAcc.IsVisible = True
        repoTotalAmtAcc.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTotalAmtAcc.ReadOnly = True
        repoTotalAmtAcc.WrapText = True
        repoTotalAmtAcc.Width = 120
        gv1.MasterTemplate.Columns.Add(repoTotalAmtAcc)

        Dim repodeduction As GridViewDecimalColumn = New GridViewDecimalColumn()
        repodeduction.FormatString = ""
        repodeduction.HeaderText = "Deduction"
        repodeduction.Name = colDeduction
        repodeduction.IsVisible = True
        repodeduction.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repodeduction.ReadOnly = True
        repodeduction.WrapText = True
        repodeduction.Width = 100
        gv1.MasterTemplate.Columns.Add(repodeduction)

        Dim repoHeadLoad As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoHeadLoad.FormatString = ""
        repoHeadLoad.HeaderText = "Head Load Amount"
        repoHeadLoad.Name = colHead_Load_Amount
        repoHeadLoad.IsVisible = True
        repoHeadLoad.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoHeadLoad.ReadOnly = True
        repoHeadLoad.WrapText = True
        repoHeadLoad.Width = 100
        gv1.MasterTemplate.Columns.Add(repoHeadLoad)

        Dim repoOwnAsset As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoOwnAsset.FormatString = ""
        repoOwnAsset.HeaderText = "Own Asset Amount"
        repoOwnAsset.Name = colOwn_Asset_Amount
        repoOwnAsset.IsVisible = True
        repoOwnAsset.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoOwnAsset.ReadOnly = True
        repoOwnAsset.WrapText = True
        repoOwnAsset.Width = 100
        gv1.MasterTemplate.Columns.Add(repoOwnAsset)

        Dim repoNetTotalCost As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNetTotalCost.FormatString = ""
        repoNetTotalCost.HeaderText = "Net Amount"
        repoNetTotalCost.Name = colNetAMOUNT
        repoNetTotalCost.IsVisible = True
        repoNetTotalCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoNetTotalCost.ReadOnly = True
        repoNetTotalCost.WrapText = True
        repoNetTotalCost.Width = 100
        gv1.MasterTemplate.Columns.Add(repoNetTotalCost)

        Dim repoNetSaveTotalCost As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNetSaveTotalCost.FormatString = ""
        repoNetSaveTotalCost.HeaderText = "Net Save Amount"
        repoNetSaveTotalCost.Name = colNetsaveAMOUNT
        repoNetSaveTotalCost.IsVisible = True
        repoNetSaveTotalCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoNetSaveTotalCost.ReadOnly = True
        repoNetSaveTotalCost.WrapText = True
        repoNetSaveTotalCost.IsVisible = False
        repoNetSaveTotalCost.Width = 0
        gv1.MasterTemplate.Columns.Add(repoNetSaveTotalCost)



        clsCustomFieldGrid.LoadBlankGrid(gv1, MyBase.ArrDetailFields)

        gv1.AllowDeleteRow = False
        gv1.AllowAddNewRow = False
        gv1.AllowEditRow = True
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = True
        gv1.EnableFiltering = True
        gv1.EnableAlternatingRowColor = True
        gv1.AutoSizeRows = False
        gv1.AllowRowResize = True
        gv1.VerticalScrollState = ScrollState.AlwaysShow
        gv1.HorizontalScrollState = ScrollState.AlwaysShow
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.ShowFilteringRow = True
    End Sub
    Private Sub txtCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        LoadData(txtCode.Value, NavType)
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCode._MYValidating
        Try
            Dim qry As String = "SELECT Distinct TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as Code,convert(date,DOC_DATE,103) as Date,MCC_NAME as [MCC],Vendor_Name as [Secretary]," _
            & " Route_Name as [Route],VENDOR_INVOICE_NO as [Vendor Invoice No],convert(date,VENDOR_INVOICE_DATE,103) as [Vendor Invoice Date],TSPL_MILK_PURCHASE_INVOICE_Head.TOTAL_AMOUNT_ACC as [Payment Amount] FROM TSPL_MILK_PURCHASE_INVOICE_HEAD inner join TSPL_MILK_PURCHASE_INVOICE_DETAIL on " _
            & " TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE=TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=VSP_CODE " _
            & "  Left join TSPL_MCC_ROUTE_MASTER on " _
            & " TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_MILK_PURCHASE_INVOICE_Head.Route_CODE  Left join TSPL_MCC_MASTER on TSPL_MCC_MASTER.Mcc_Code=TSPL_MILK_PURCHASE_INVOICE_Head.Mcc_CODE "
            txtCode.Value = clsCommon.ShowSelectForm("MILK Invoice", qry, "Code", "", txtCode.Value, "Code", isButtonClicked, "DOC_DATE")


            If clsCommon.myLen(txtCode.Value) > 0 Then
                LoadData(txtCode.Value, NavigatorType.Current)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        AddNew()
    End Sub

    Private Sub gv1_CellFormatting(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.CellFormatting
        'Try
        '    Dim totAmount As Double = 0
        '    Dim totCommssion As Double = 0
        '    Dim totPaymentCommssion As Double = 0
        '    Dim totAmountwithPaymentCommssion As Double = 0
        '    For Each grow As GridViewRowInfo In gv1.Rows
        '        totAmount += grow.Cells(colDocument_AMOUNT).Value
        '        totCommssion += grow.Cells(colCOMMISSIONAmount).Value
        '        totPaymentCommssion += grow.Cells(colPaymentCOMMISSIONAmount).Value
        '        totAmountwithPaymentCommssion += grow.Cells(colNetAMOUNT).Value
        '    Next
        '    TxtTotlAMount.Text = totAmount
        '    txtTotComm.Text = totCommssion
        '    TxtAccTot.Text = totAmountwithPaymentCommssion
        '    TxtPaymentComm.Text = totPaymentCommssion
        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If e.Column Is gv1.Columns(colAMOUNT) Or e.Column Is gv1.Columns(colIncentive) Or e.Column Is gv1.Columns(colPaymentCOMMISSION) Or e.Column Is gv1.Columns(colCOMMISSION) Or e.Column Is gv1.Columns(colDeduction) Then
                For Each grow As GridViewRowInfo In gv1.Rows
                    'If LCase(txtPayment.Text) <> "different" Then
                    'If clsCommon.myCstr(grow.Cells(colService_Charge).Value) = "%(Percentage)" Then
                    '    grow.Cells(colPaymentCOMMISSIONAmount).Value = Math.Round(grow.Cells(colAMOUNT).Value * grow.Cells(colCOMMISSION).Value / 100, 2)
                    '    grow.Cells(colCOMMISSIONAmount).Value = Math.Round(grow.Cells(colAMOUNT).Value * grow.Cells(colPaymentCOMMISSION).Value / 100, 2)
                    '    grow.Cells(colIncentiveEMP).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colIncentive).Value) * grow.Cells(colPaymentCOMMISSION).Value / 100, 2)
                    'ElseIf clsCommon.myCstr(grow.Cells(colService_Charge).Value) = "Rate/Kg" Then
                    '    grow.Cells(colPaymentCOMMISSIONAmount).Value = Math.Round(grow.Cells(colAcc_Qty).Value * grow.Cells(colCOMMISSION).Value, 2)
                    '    grow.Cells(colCOMMISSIONAmount).Value = Math.Round(grow.Cells(colAcc_Qty).Value * grow.Cells(colPaymentCOMMISSION).Value, 2)
                    '    grow.Cells(colIncentiveEMP).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colIncentive).Value) * grow.Cells(colPaymentCOMMISSION).Value / 100, 2)
                    'ElseIf clsCommon.myCstr(grow.Cells(colService_Charge).Value) = "Rate/Ltr" And clsCommon.myCstr(grow.Cells(colUOM).Value) = "LTR" Then
                    '    grow.Cells(colPaymentCOMMISSIONAmount).Value = Math.Round(grow.Cells(colQty).Value * grow.Cells(colCOMMISSION).Value, 2) 'grow.Cells(colCOMMISSIONAmount).Value 
                    '    grow.Cells(colCOMMISSIONAmount).Value = Math.Round(grow.Cells(colQty).Value * grow.Cells(colPaymentCOMMISSION).Value, 2) 'grow.Cells(colPaymentCOMMISSIONAmount).Value
                    '    grow.Cells(colIncentiveEMP).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colIncentive).Value) * grow.Cells(colPaymentCOMMISSION).Value / 100, 2)
                    'End If

                    If clsCommon.myCstr(grow.Cells(colService_Charge).Value) = "%(Percentage)" Then
                        grow.Cells(colCOMMISSIONAmount).Value = Math.Round(grow.Cells(colAMOUNT).Value * grow.Cells(colCOMMISSION).Value / 100, 2)
                        grow.Cells(colPaymentCOMMISSIONAmount).Value = Math.Round(grow.Cells(colAMOUNT).Value * grow.Cells(colPaymentCOMMISSION).Value / 100, 2)
                        grow.Cells(colIncentiveEMP).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colIncentive).Value) * grow.Cells(colPaymentCOMMISSION).Value / 100, 2)
                    ElseIf clsCommon.myCstr(grow.Cells(colService_Charge).Value) = "Rate/Kg" Then
                        grow.Cells(colCOMMISSIONAmount).Value = Math.Round(grow.Cells(colAcc_Qty).Value * grow.Cells(colCOMMISSION).Value, 2)
                        grow.Cells(colPaymentCOMMISSIONAmount).Value = Math.Round(grow.Cells(colAcc_Qty).Value * grow.Cells(colPaymentCOMMISSION).Value, 2)
                        grow.Cells(colIncentiveEMP).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colIncentive).Value) * grow.Cells(colPaymentCOMMISSION).Value / 100, 2)
                    ElseIf clsCommon.myCstr(grow.Cells(colService_Charge).Value) = "Rate/Ltr" And clsCommon.myCstr(grow.Cells(colUOM).Value) = "LTR" Then
                        grow.Cells(colCOMMISSIONAmount).Value = Math.Round(grow.Cells(colQty).Value * grow.Cells(colCOMMISSION).Value, 2) 'grow.Cells(colCOMMISSIONAmount).Value 
                        grow.Cells(colPaymentCOMMISSIONAmount).Value = Math.Round(grow.Cells(colQty).Value * grow.Cells(colPaymentCOMMISSION).Value, 2) 'grow.Cells(colPaymentCOMMISSIONAmount).Value
                        grow.Cells(colIncentiveEMP).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colIncentive).Value) * grow.Cells(colPaymentCOMMISSION).Value / 100, 2)
                    End If
                    grow.Cells(colDocument_AMOUNT).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colAMOUNT).Value) + clsCommon.myCdbl(grow.Cells(colCOMMISSIONAmount).Value) + clsCommon.myCdbl(grow.Cells(colIncentive).Value) + clsCommon.myCdbl(grow.Cells(colIncentiveEMP).Value), 2) '- clsCommon.myCdbl(grow.Cells(colDeduction).Value)
                    grow.Cells(colTOTAL_AMOUNT).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colAMOUNT).Value) + clsCommon.myCdbl(grow.Cells(colPaymentCOMMISSIONAmount).Value) + clsCommon.myCdbl(grow.Cells(colIncentive).Value) + clsCommon.myCdbl(grow.Cells(colIncentiveEMP).Value), 2) '- clsCommon.myCdbl(grow.Cells(colDeduction).Value)
                    ' grow.Cells(cola).Value = clsCommon.myCdbl(grow.Cells(colAMOUNT).Value) + clsCommon.myCdbl(grow.Cells(colPaymentCOMMISSIONAmount).Value) + clsCommon.myCdbl(grow.Cells(colIncentive).Value) + clsCommon.myCdbl(grow.Cells(colIncentiveEMP).Value) '- clsCommon.myCdbl(grow.Cells(colDeduction).Value)
                    grow.Cells(colNetAMOUNT).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colAMOUNT).Value) + clsCommon.myCdbl(grow.Cells(colPaymentCOMMISSIONAmount).Value) + clsCommon.myCdbl(grow.Cells(colIncentive).Value) + clsCommon.myCdbl(grow.Cells(colIncentiveEMP).Value), 2) ' - clsCommon.myCdbl(grow.Cells(colDeduction).Value), CInt(clsCommon.myCdbl(DtMCC.Rows(0).Item("FAT_SNF_CALC"))))
                    grow.Cells(colNetsaveAMOUNT).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colAMOUNT).Value) + clsCommon.myCdbl(grow.Cells(colPaymentCOMMISSIONAmount).Value) + clsCommon.myCdbl(grow.Cells(colIncentive).Value) + clsCommon.myCdbl(grow.Cells(colIncentiveEMP).Value), 2) ' - clsCommon.myCdbl(grow.Cells(colDeduction).Value), CInt(clsCommon.myCdbl(DtMCC.Rows(0).Item("FAT_SNF_SAVE"))))
                    'Else
                    'grow.Cells(colCOMMISSIONAmount).Value = grow.Cells(colAMOUNT).Value * grow.Cells(colCOMMISSION).Value / 100
                    'grow.Cells(colPaymentCOMMISSIONAmount).Value = grow.Cells(colAMOUNT).Value * grow.Cells(colPaymentCOMMISSION).Value / 100
                    'grow.Cells(colDocument_AMOUNT).Value = clsCommon.myCdbl(grow.Cells(colAMOUNT).Value) + clsCommon.myCdbl(grow.Cells(colIncentive).Value) '- clsCommon.myCdbl(grow.Cells(colDeduction).Value)
                    'grow.Cells(colTOTAL_AMOUNT).Value = clsCommon.myCdbl(grow.Cells(colAMOUNT).Value) + clsCommon.myCdbl(grow.Cells(colIncentive).Value) '- clsCommon.myCdbl(grow.Cells(colDeduction).Value)
                    'grow.Cells(colNetAMOUNT).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colAMOUNT).Value) + clsCommon.myCdbl(grow.Cells(colIncentive).Value) - clsCommon.myCdbl(grow.Cells(colDeduction).Value), CInt(clsCommon.myCdbl(DtMCC.Rows(0).Item("FAT_SNF_CALC"))))
                    'grow.Cells(colNetsaveAMOUNT).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colAMOUNT).Value) + clsCommon.myCdbl(grow.Cells(colIncentive).Value) - clsCommon.myCdbl(grow.Cells(colDeduction).Value), CInt(clsCommon.myCdbl(DtMCC.Rows(0).Item("FAT_SNF_SAVE"))))
                    'End If

                Next
                Dim totAmount As Double = 0
                Dim totCommssion As Double = 0
                Dim totPaymentCommssion As Double = 0
                Dim totAmountwithPaymentCommssion As Double = 0
                Dim totAmountIncentive As Double = 0
                Dim totAmountIncentiveEMP As Double = 0
                Dim totBasicAmount As Double = 0
                For Each groww As GridViewRowInfo In gv1.Rows
                    totAmount += groww.Cells(colDocument_AMOUNT).Value
                    totBasicAmount += groww.Cells(colAMOUNT).Value
                    totCommssion += groww.Cells(colCOMMISSIONAmount).Value
                    totPaymentCommssion += groww.Cells(colPaymentCOMMISSIONAmount).Value
                    totAmountwithPaymentCommssion += groww.Cells(colNetAMOUNT).Value
                    totAmountIncentive += groww.Cells(colIncentive).Value
                    totAmountIncentiveEMP += groww.Cells(colIncentiveEMP).Value
                Next
                TxtTotlAMount.Text = totAmount
                txtTotComm.Text = totCommssion
                TxtAccTot.Text = totAmountwithPaymentCommssion
                TxtPaymentComm.Text = totPaymentCommssion
                LblEMP.Text = totPaymentCommssion
                lblAmount.Text = totBasicAmount
                lblIncentive.Text = totAmountIncentive
                lblIncentiveEMP.Text = totAmountIncentiveEMP
                'lblTotRAmt.Text = totAmountwithPaymentCommssion
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        Try
            If clsCommon.MyMessageBoxShow("Do You want to delete this Row Permanently . Are You Sure.?", "Delete Row", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                Dim sQuery As String = "delete from TSPL_MILK_PURCHASE_INVOICE_DETAIL where DOC_Code='" & gv1.CurrentRow.Cells("DOC CODE").Value & "' and VLC_DOC_Code='" & gv1.CurrentRow.Cells("VLC DOC CODE").Value & "'"
                clsDBFuncationality.ExecuteNonQuery(sQuery)
            Else
                e.Cancel = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            Dim qry As String = ""
            Dim msg As String = ""
            Dim dt As DataTable = Nothing

            If (myMessages.postConfirm()) Then
                'SaveData()
                '              Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                If (clsMilkPurchaseInvoiceMCC.PostData("M-PURINVOICE", txtCode.Value)) Then
                    '                   trans.Commit()
                    msg = "Successfully Posted"
                Else
                    'trans.Rollback()
                    qry = "select No_Of_Level, LEVEL from TSPL_APPROVAL_LEVEL_SCREEN where User_Code='" + objCommonVar.CurrentUserCode + "' and Trans_Code='" + MyBase.Form_ID + "' "
                    dt = clsDBFuncationality.GetDataTable(qry)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        Dim level As String = dt.Rows(0)("LEVEL").ToString()
                        Dim NoOflevel As Integer = clsCommon.myCdbl(dt.Rows(0)("No_Of_Level"))
                        If clsCommon.CompairString(level, "Level1") = CompairStringResult.Equal Then
                            msg = "Level 1 Approval done. "
                            If NoOflevel = 1 Then
                                msg += "Successfully Posted. "
                            Else
                                msg += "Level 2 Approval Required."
                            End If
                        ElseIf clsCommon.CompairString(level, "Level2") = CompairStringResult.Equal Then
                            msg = "Level 2 Approval done. "
                            If NoOflevel = 2 Then
                                msg += "Successfully Posted "
                            Else
                                msg += "Level 3 Approval Required."
                            End If
                        Else
                            msg = "Level 3 Approval done. Successfully Posted. "
                        End If
                    End If
                End If
                common.clsCommon.MyMessageBoxShow(msg)
                LoadData(txtCode.Value)
                'If (common.clsCommon.MyMessageBoxShow("Do you want to print", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes) Then
                '    PrintDataNew()
                'End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FndSRNNO__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles FndSRNNO._MYValidating
        'Try
        '    SelectMilkSRNItems()
        'Catch ex As Exception
        'End Try
    End Sub


    Private Sub BtnSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnsaveLayout.Click
        '    If clsCommon.myLen(GetReportID()) > 0 Then
        gv1.MasterTemplate.FilterDescriptors.Clear()
        Dim obj As New clsGridLayout()
        obj.ReportID = "MilkPIGrid"
        obj.UserID = objCommonVar.CurrentUserCode
        obj.GridLayout = New MemoryStream()
        gv1.SaveLayout(obj.GridLayout)
        obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
        obj.GridColumns = gv1.ColumnCount
        If obj.SaveData() Then
            common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
        End If
        ''stuti regarding memory leakage
        obj.GridLayout.Close()
        obj.GridLayout.Dispose()
        'End If
    End Sub

    Private Sub BtnDeleteLayout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDeleteLayout.Click
        clsGridLayout.DeleteData("MilkPIGrid", objCommonVar.CurrentUserCode)
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            'If clsCommon.myLen("LoadinMainGrid") > 0 Then
            Dim obj As clsGridLayout = New clsGridLayout()
            obj = CType(obj.GetData("MilkPIGrid", "", objCommonVar.CurrentUserCode), clsGridLayout)
            If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                Dim ii As Integer
                For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                    gv1.Columns(ii).IsVisible = False
                    gv1.Columns(ii).VisibleInColumnChooser = True
                Next

                gv1.LoadLayout(obj.GridLayout)
                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            End If
            'End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub Fromdate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Fromdate.ValueChanged, ToDate.ValueChanged
        ToDate.MinDate = "01-Jan-00001"
        ToDate.MinDate = Fromdate.Value
    End Sub

    Private Sub fndVSPCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndVSPCode._MYValidating
        Try
            Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name,Terms_Code as [Term Code] ,Terms_Code_Desc as [Term Description] ,Tax_Group as [Tax Group],Tax_Group_Desc as [Tax Group Description],Add1 as [Address 1],Add2 as [Address 2],Add3 as [Address 3],City_Code_Desc as City,State,Country from TSPL_VENDOR_MASTER"
            fndVSPCode.Value = clsCommon.ShowSelectForm("POVeFNDID", qry, "Code", " upper(Form_type)='VSP'", fndVSPCode.Value, "Code", isButtonClicked)
            ''lblVendorName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Name from TSPL_VENDOR_MASTER where Vendor_Code='" + txtVendorNo.Value + "'"))
            qry = "select  Vendor_Code,Vendor_Name,Terms_Code,Terms_Code_Desc ,Vendor_Account ,Tax_Group,Tax_Group_Desc from TSPL_VENDOR_MASTER where Vendor_Code ='" + fndVSPCode.Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                lblVSPDesc.Text = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
            Else
                lblVSPDesc.Text = ""
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnDrillDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDrillDown.Click
        Dim strPoInvcNo As String = connectSql.RunScalar("select Document_No from TSPL_VENDOR_INVOICE_HEAD where Vendor_Invoice_No='" + txtCode.Value + "'")
        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmVendorService, strPoInvcNo)
    End Sub

    Private Sub fndMCCCode_Validating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles FndMccCode._MYValidating
        Dim sQuery As String = "select Location_Category from tspl_location_master where Location_Code='" & fndMCCCode.Value & "'"
        'If clsDBFuncationality.getSingleValue(sQuery) = "HO" Then
        Dim qry As String = "select Location_Code as Location,Location_Desc as Description  from TSPL_LOCATION_MASTER "
        ' fndMCCCode.Value = clsCommon.ShowSelectForm("LocatMast", qry, "Location", "  upper(location_category)='MCC' ", fndMCCCode.Value, "Location_Code", isButtonClicked)
        fndMCCCode.Value = clsMccMaster.getFinder("", fndMCCCode.Value, isButtonClicked)
        Me.lblMccName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Mcc_Name from TSPL_Mcc_MASTER where MCC_Code='" + FndMccCode.Value + "' "))
    End Sub


    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        PrintData(False)
    End Sub

    '==========added by Shivani Tyagi
    Sub PrintData(ByVal isBillofSupply As Boolean)
        Dim frmCRV As New frmCrystalReportViewer()
        If clsCommon.myLen(txtCode.Value) > 0 Then
            Dim Qry As String = "select isnull(TSPL_MILK_PURCHASE_INVOICE_HEAD.Purchase_Tax_Invoice,'') as Purchase_Tax_Invoice,TSPL_VENDOR_MASTER.GSTRegistered as Vendor_isRegister ,TSPL_STATE_MASTER_For_MCC.GST_STATE_Code as MCC_GST_STATE_Code ,TSPL_STATE_MASTER_For_Ven.GST_STATE_Code as Vendor_GST_STATE_Code,TSPL_LOCATION_MASTER.GSTNO as MCC_GSTINNo,TSPL_VENDOR_MASTER.GSTFinalNo AS Vendor_GSTIN_NO, TSPL_LOCATION_MASTER .City_Code  as MCC_City,TSPL_STATE_MASTER_For_MCC.STATE_NAME as MCC_State, TSPL_MILK_PURCHASE_INVOICE_HEAD. DOC_CODE ,convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as DOC_DATE,SRN_CODE ,convert(varchar,TSPL_MILK_SRN_HEAD.DOC_DATE,103) as SRN_Date ,TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER ,TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER ,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty ,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Rate,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Net_AMOUNT ,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Item_Code ,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.HSN_Code ,VENDOR_INVOICE_NO ,TSPL_COMPANY_MASTER.Comp_Name  ,TSPL_MCC_MASTER .MCC_Name ,TSPL_STATE_MASTER_For_Ven.STATE_NAME as Vend_State_Name,TSPL_CITY_MASTER_fOR_Ven .City_Name as Vend_City_Name,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2  ,TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add4,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add4,'') else ' ' end  as Loc_Add,TSPL_LOCATION_MASTER.TIN_No as Loc_TinNo,TSPL_LOCATION_MASTER.GSTNO as Comp_Gstinno,TSPL_LOCATION_MASTER.Pin_Code  as Loc_PINCode,TSPL_LOCATION_MASTER.Email as Loc_Email,case when ISNULL(TSPL_LOCATION_MASTER.Phone1,'')='(+__)__________' then '' else  TSPL_LOCATION_MASTER.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_LOCATION_MASTER.Phone2 Else'' End as  Loc_Phn,TSPL_VENDOR_MASTER.Vendor_Name ,TSPL_VENDOR_MASTER.add1 +case when len(TSPL_VENDOR_MASTER.add2)>0 then ', '+TSPL_VENDOR_MASTER.add2 else '' end +case when LEN(isnull(TSPL_VENDOR_MASTER.Add3,''))>0 then ', '+isnull(TSPL_VENDOR_MASTER.Add3,'') else ' ' end   as Ven_Add,TSPL_VENDOR_MASTER.Tin_No as Ven_TINNo ,TSPL_VENDOR_MASTER.Email as Ven_Email ,case when ISNULL(TSPL_VENDOR_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_VENDOR_MASTER.Phone1 end +  Case When   ISNULL(TSPL_VENDOR_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_VENDOR_MASTER.Phone2 Else '' End as Ven_Phn,'For  FAT' +convert(varchar,TSPL_MILK_PRICE_MASTER.Ratio) + ' % &  SNF' +  convert(varchar,TSPL_MILK_PRICE_MASTER.SNF_Ratio)+ ' %' as 'MilkRate',VEHICLE_NO,TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE ,TSPL_VENDOR_MASTER.Vendor_Name   from TSPL_MILK_PURCHASE_INVOICE_DETAIL left outer join TSPL_MILK_PURCHASE_INVOICE_HEAD on TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE =TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code =TSPL_MILK_PURCHASE_INVOICE_DETAIL.Item_Code  left outer join TSPL_COMPANY_MASTER  on TSPL_COMPANY_MASTER.Comp_Code =TSPL_MILK_PURCHASE_INVOICE_HEAD.Comp_Code  left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER .Location_Code =TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_CODE  left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER .Vendor_Code =TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE left outer join TSPL_MILK_SRN_HEAD  on TSPL_MILK_SRN_HEAD.DOC_CODE  =TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE  left join   TSPL_MILK_SRN_DETAIL on TSPL_MILK_SRN_DETAIL.DOC_CODE =TSPL_MILK_SRN_HEAD.DOC_CODE left join TSPL_MILK_PRICE_MASTER on TSPL_MILK_PRICE_MASTER.Price_Code =TSPL_MILK_SRN_DETAIL.Price_Code left join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code =TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_CODE LEFT OUTER JOIN TSPL_CITY_MASTER  AS TSPL_CITY_MASTER_fOR_Comp ON TSPL_CITY_MASTER_fOR_Comp.City_Code =TSPL_COMPANY_MASTER.City_Code    LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_For_Comp  ON TSPL_STATE_MASTER_For_Comp.STATE_CODE  =TSPL_COMPANY_MASTER.State "
            Qry += " LEFT OUTER JOIN TSPL_CITY_MASTER  AS TSPL_CITY_MASTER_fOR_Ven ON TSPL_CITY_MASTER_fOR_Ven.City_Code =TSPL_VENDOR_MASTER.City_Code    LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_For_Ven  ON TSPL_STATE_MASTER_For_Ven.STATE_CODE  =TSPL_VENDOR_MASTER.State_Code"
            Qry += " LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_For_MCC  ON TSPL_STATE_MASTER_For_MCC.STATE_CODE  =TSPL_LOCATION_MASTER.State "
            Qry += " where  TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE='" & txtCode.Value & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If clsERPFuncationality.GetGSTStatus(clsCommon.myCDate(dtpDocDate.Value)) Then
                If isBillofSupply = False Then
                    frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "rptMilkPurchaseInvoice", "Purchase Invoice", clsCommon.myCDate(dtpDocDate.Value))
                Else
                    frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "rptMilkPurchaseInvoice_Bill_of_Supply", "Purchase Invoice", clsCommon.myCDate(dtpDocDate.Value))
                End If

            Else
                If isBillofSupply = False Then
                    frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "rptMilkPurchaseInvoice", "Purchase Invoice")
                End If

            End If
        Else
            clsCommon.MyMessageBoxShow(Me, "Please select an invoice to print", Me.Text)
        End If
        frmCRV = Nothing
    End Sub

    Private Sub lblTotRAmt_Click(sender As Object, e As EventArgs) Handles lblTotRAmt.DoubleClick
        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                txtCode.Focus()
                Throw New Exception("PI No not found")
            End If

            clsMilkPurchaseInvoiceMCC.createDebitNoteMP(txtCode.Value, tran)
            tran.Rollback()
        Catch ex As Exception
            tran.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnBillOfSupply_Click(sender As Object, e As EventArgs) Handles btnBillOfSupply.Click
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "No data found to print", Me.Text)
        Else
            Dim isVendorRegister As Boolean = clsDBFuncationality.getSingleValue("select  GSTRegistered from TSPL_VENDOR_MASTER where vendor_code='" + fndVSPCode.Value + "' ")
            If isVendorRegister = False Then
                Dim strPurchaseTaxInvoiceNo As String = clsDBFuncationality.getSingleValue(" select isnull(TSPL_MILK_PURCHASE_INVOICE_HEAD.Purchase_Tax_Invoice,'') as Purchase_Tax_Invoice from TSPL_MILK_PURCHASE_INVOICE_HEAD where TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = '" + txtCode.Value + "'  ")
                If clsCommon.myLen(strPurchaseTaxInvoiceNo) > 0 Then
                    PrintData(True)
                Else
                    clsCommon.MyMessageBoxShow(Me, "No data found to print", Me.Text)
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to print", Me.Text)
            End If

        End If
    End Sub

    Private Sub RadLabel27_Click(sender As Object, e As EventArgs) Handles RadLabel27.Click

    End Sub

    Private Sub btnViewTDS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewTDSDetails.Click
        ViewTDS()
    End Sub
    Sub ViewTDS()
        Try
            Dim frm As New FrmViewTDS()
            'UpdateTDSAmount()
            frm.ObjIn = objRemittance
            frm.ShowDialog()
            'If (frm.ObjReturn IsNot Nothing) Then
            'objRemittance = frm.ObjReturn
            'End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

End Class
