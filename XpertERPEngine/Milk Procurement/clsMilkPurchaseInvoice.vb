Imports common
Imports System.Data.SqlClient
Public Class clsMilkPurchaseInvoiceHead
    Public IsAgainstJobWork As Integer = 0
    Public Joblocation_Code As String = Nothing
    Public Purchase_Tax_Invoice As String = Nothing
    Public isNewEntry As Boolean = False
    Public DOC_NO As String = Nothing
    Public Loc_Code As String = Nothing
    Public DOC_DATE As Date = Nothing
    Public vendor_code As String = Nothing
    Public SRN_From_Date As Date = Nothing
    Public SRN_TO_Date As Date = Nothing
    Public Total_FAT_KG As Double = 0
    Public Total_SNF_KG As Double = 0
    Public Total_QTY As Double = 0
    Public Total_AMT As Double = 0
    Public isPosted As Integer
    Public Posting_Date As Date = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As String = Nothing
    Public Modified_By As String = Nothing
    Public Modified_Date As String = Nothing
    Public Comp_Code As String = Nothing
    Public Vendor_Invoice_No As String = Nothing
    Public arrDetail As List(Of clsMilkPurchaseInvoiceDetail) = Nothing
    Public RoundOffAmount As Double = 0
    Public isSRNTradeInvoice As Double = 0
    Public EWayBillNo As String = Nothing
    Public EWayBillDate As Date?
    Public Electronic_Ref_No As String = Nothing
    Public Tax_Calculation_Type As EnumTaxCalucationType
    Public Tax_Group As String = Nothing
    Public TaxGroupName As String = Nothing 'Not a table field
    Public TAX1 As String = Nothing
    Public TAX1_Rate As Double = 0
    Public TAX1_Base_Amt As Double = 0
    Public TAX1_Amt As Double = 0
    Public TAX2 As String = Nothing
    Public TAX2_Rate As Double = 0
    Public TAX2_Base_Amt As Double = 0
    Public TAX2_Amt As Double = 0
    Public TAX3 As String = Nothing
    Public TAX3_Rate As Double = 0
    Public TAX3_Base_Amt As Double = 0
    Public TAX3_Amt As Double = 0
    Public TAX4 As String = Nothing
    Public TAX4_Rate As Double = 0
    Public TAX4_Base_Amt As Double = 0
    Public TAX4_Amt As Double = 0
    Public TAX5 As String = Nothing
    Public TAX5_Rate As Double = 0
    Public TAX5_Base_Amt As Double = 0
    Public TAX5_Amt As Double = 0
    Public TAX6 As String = Nothing
    Public TAX6_Rate As Double = 0
    Public TAX6_Base_Amt As Double = 0
    Public TAX6_Amt As Double = 0
    Public TAX7 As String = Nothing
    Public TAX7_Rate As Double = 0
    Public TAX7_Base_Amt As Double = 0
    Public TAX7_Amt As Double = 0
    Public TAX8 As String = Nothing
    Public TAX8_Rate As Double = 0
    Public TAX8_Base_Amt As Double = 0
    Public TAX8_Amt As Double = 0
    Public TAX9 As String = Nothing
    Public TAX9_Rate As Double = 0
    Public TAX9_Base_Amt As Double = 0
    Public TAX9_Amt As Double = 0
    Public TAX10 As String = Nothing
    Public TAX10_Rate As Double = 0
    Public TAX10_Base_Amt As Double = 0
    Public TAX10_Amt As Double = 0
    Public Total_Tax_Amt As Double = 0
    Public Total_Taxable_Amount As Decimal

    Public ActualTCSBaseAmount As Decimal
    Public ChangedTCSBaseAmount As Decimal
    Public objPIRemittance As clsPIRemittance = Nothing

    'Public Shared Function ReverseAndUnpost(ByVal strDocNo As String) As Boolean
    '    'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
    '    'Try
    '    '    If clsCommon.myLen(strDocNo) <= 0 Then
    '    '        Throw New Exception("Please select a QC No")
    '    '    End If
    '    '    Dim Qry As String = "select isPosted from tspl_quality_check where Qc_no='" + strDocNo + "'"
    '    '    If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
    '    '        Throw New Exception("Transaction status should be posted for reverse and unpost")
    '    '    End If
    '    '    Dim isUsed As Integer = clsDBFuncationality.getSingleValue(" select count(*) from tspl_milk_unloading where Qc_no='" & strDocNo & "'", trans)
    '    '    If isUsed > 0 Then
    '    '        Throw New Exception("QC No is in use")
    '    '    End If
    '    '    Qry = "Update tspl_quality_check set isPosted = 0,Posting_Date=null where QC_No='" + strDocNo + "'"
    '    '    clsDBFuncationality.ExecuteNonQuery(Qry, trans)
    '    '    trans.Commit()
    '    '    Return True
    '    'Catch ex As Exception
    '    '    trans.Rollback()
    '    '    Throw New Exception(ex.Message)
    '    'End Try
    'End Function
    Public Shared Function postData(ByVal strDocNo As String, ByVal formId As String, Optional trans As SqlTransaction = Nothing) As Boolean
        Return postData(formId, strDocNo, trans, "", "")
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal trans As SqlTransaction, ByVal strAPInvNo As String, ByVal strAPInvJENo As String) As Boolean
        'Dim trans As SqlTransaction = Nothing
        Dim isTransLocallyInitiatted As Boolean = False
        Try
            Dim isRecreateAPInvoice As Boolean = False
            If clsCommon.myLen(strAPInvNo) > 0 Then
                If clsCommon.myLen(strAPInvJENo) > 0 Then
                    isRecreateAPInvoice = True
                Else
                    Throw New Exception("Please provice Journal Entry voucher no for AP Invoice Entry No " + strAPInvNo)
                End If
            End If

            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Purchase Invoice No not found to Post")
            End If
            ''Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")
            If trans Is Nothing Then
                trans = clsDBFuncationality.GetTransactin()
                isTransLocallyInitiatted = True
            End If
            Dim ApplyTransportChargeAddInActualAmount As Boolean = IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ApplyTransportChargeAddInActualAmount, clsFixedParameterCode.ApplyTransportChargeAddInActualAmount, trans)) = "1", True, False)
            Dim obj As clsMilkPurchaseInvoiceHead = clsMilkPurchaseInvoiceHead.getData(strDocNo, NavigatorType.Current, trans)
            '' Dim strPostDate As String = clsCommon.GetPrintDate(obj.Doc_Date, "dd/MM/yyyy")
            If (obj Is Nothing OrElse clsCommon.myLen(obj.DOC_NO) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If


            If Not isRecreateAPInvoice Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkMilkProcurement, clsUserMgtCode.frmBulkMilkPurchaseInvoice, obj.Loc_Code, obj.DOC_DATE, trans)
                If (obj.isPosted = 1) Then
                    Throw New Exception("Already Post on :" + obj.Posting_Date)
                End If
            End If

            Dim vendor_name As String = clsVendorMaster.GetName(obj.vendor_code, trans)
            Dim qry As String = ""

            If Not isRecreateAPInvoice Then
                Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel(FormId, "Tspl_bulk_milk_purchase_invoice_head", "DOC_no", obj.DOC_NO, trans)
                If isResult = False Then
                    If isTransLocallyInitiatted Then
                        trans.Commit()
                    End If
                    Return False
                End If
            End If





            Dim objVendorInvHead As New clsVedorInvoiceHead()
            'objVendorInvHead.Document_No = txtDocNo.Value'ToBeGenerated
            objVendorInvHead.Invoice_Entry_Date = clsCommon.myCDate(obj.DOC_DATE, "dd/MMM/yyyy")
            objVendorInvHead.Vendor_Code = obj.vendor_code
            objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(obj.vendor_code, trans)
            objVendorInvHead.Vendor_Invoice_No = obj.Vendor_Invoice_No
            objVendorInvHead.Invoice_Type = "AP"
            objVendorInvHead.Vendor_Invoice_Date = obj.DOC_DATE
            'objVendorInvHead.loc_code = IIf(obj.isSRNTradeInvoice = 1, clsLocation.GetSegmentCode(obj.Loc_Code, trans), obj.Loc_Code)
            ''By Balwinder on 11/07/2016 because on screen loc_Code is comming from location master and Here should pass the segment of that location.
            'If obj.IsAgainstJobWork = 0 Then
            '    objVendorInvHead.loc_code = clsLocation.GetSegmentCode(obj.Loc_Code, trans)
            'Else
            '    objVendorInvHead.loc_code = clsLocation.GetSegmentCode(obj.Joblocation_Code, trans)
            'End If
            objVendorInvHead.loc_code = clsLocation.GetSegmentCode(obj.Loc_Code, trans)

            'objVendorInvHead.PROJECT_ID = 1 'obj.PROJECT_ID
            objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + obj.vendor_code + "'", trans))
            If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                Throw New Exception("Please set the vendor Account Set For Vendor : " + vendor_name)
            End If

            objVendorInvHead.Document_Type = "I" ''For Purchase Invoice Type


            objVendorInvHead.On_Hold = False
            Dim srndate As String = ""
            Dim srncode As String = ""
            Dim Vlc_Code As String = ""
            Dim Vlc_Name As String = ""
            Dim xUOM As String = ""
            For Each objTr As clsMilkPurchaseInvoiceDetail In obj.arrDetail
                If clsCommon.myLen(objTr.SRN_NO) > 0 Then
                    xUOM = objTr.UOM
                    ' Dim query As String = "select doc_date,vd.VLC_Code,VLC_Name from TSPL_Milk_SRN_HEAD sh inner join TSPL_VLC_MASTER_HEAD vd on sh.VLC_CODE=vd.VLC_Code where DOc_Code ='" + objTr.SRN_CODE + "' "
                    'Dim Dt_SRN As DataTable = clsDBFuncationality.GetDataTable(query, trans)
                    srndate = IIf(srndate = "", clsCommon.myCDate(CStr(objTr.SRN_Date), "dd/MM/yyyy"), srndate & "," & clsCommon.myCDate(CStr(objTr.SRN_Date), "dd/MM/yyyy"))
                    srncode = IIf(srncode = "", objTr.SRN_NO, srncode & "," & objTr.SRN_NO)
                    'Vlc_Code = obj.vendor_code  'IIf(Vlc_Code = "", obj.vendor_code , Vlc_Code & "," & Dt_SRN.Rows(0).Item("VLC_Code").ToString)
                    'Vlc_Name = clsVendorMaster.GetName(obj.vendor_code, trans) 'IIf(Vlc_Name = "", Dt_SRN.Rows(0).Item("VLC_Name").ToString, Vlc_Name & "," & Dt_SRN.Rows(0).Item("VLC_name").ToString)
                End If
            Next



            objVendorInvHead.Description = "Vendor : " + obj.vendor_code + "/" + vendor_name + " .Against Bulk Milk Purchase Invoice No [" + obj.DOC_NO + "] - [" + srncode + "] - [" + srndate + "]"
            objVendorInvHead.Description = objVendorInvHead.Description + " having Total Qty : " + clsCommon.myCstr(obj.Total_QTY) + " with UOM : " + xUOM + ""

            If clsCommon.myLen(objVendorInvHead.Description) > 2000 Then
                objVendorInvHead.Description = objVendorInvHead.Description.Substring(0, 2000)
            End If


            objVendorInvHead.Due_Date = obj.DOC_DATE
            objVendorInvHead.Discount_Base = 0 'obj.Discount_Base
            objVendorInvHead.Discount_Amount = 0 'obj.Discount_Amt
            objVendorInvHead.Amount_Less_Discount = obj.Total_AMT
            objVendorInvHead.Document_Total = obj.Total_AMT
            objVendorInvHead.Balance_Amt = obj.Total_AMT
            objVendorInvHead.RoundOffAmount = obj.RoundOffAmount
            'objVendorInvHead.Against_POInvoice_No = obj.DOC_CODE
            objVendorInvHead.RefDocNo = ""
            objVendorInvHead.RefDocType = "BM-PI"
            objVendorInvHead.Against_BulkMillkPurchaseInvoice_No = obj.DOC_NO

            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Payable_Account,Discount_Account from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, obj.Loc_Code, trans)
                If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                    objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                    objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, obj.Loc_Code, trans)
                End If
            End If
            If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                Throw New Exception("Please set the vendor payable Account")
            End If



            objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
            Dim ii As Integer = 0
            Dim isFirstTime As Boolean = True
            objVendorInvHead.Total_Landed_Amt = 0


            objVendorInvHead.Tax_Calculation_Type = obj.Tax_Calculation_Type
            objVendorInvHead.Tax_Group = obj.Tax_Group
            If (clsCommon.myLen(obj.TAX1) > 0) Then
                objVendorInvHead.TAX1 = obj.TAX1
                If clsTaxMaster.ISTaxRecoverableAC(obj.TAX1, trans) Then
                    objVendorInvHead.TAX1_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX1, trans)
                    objVendorInvHead.TAX1_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX1_GLAC, obj.Loc_Code, trans)
                End If
                objVendorInvHead.TAX1_Rate = obj.TAX1_Rate
                objVendorInvHead.Tax1_BAmount = obj.TAX1_Base_Amt
                objVendorInvHead.TAX1_Amt = obj.TAX1_Amt
            End If
            If (clsCommon.myLen(obj.TAX2) > 0) Then
                objVendorInvHead.TAX2 = obj.TAX2
                If clsTaxMaster.ISTaxRecoverableAC(obj.TAX2, trans) Then
                    objVendorInvHead.TAX2_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX2, trans)
                    objVendorInvHead.TAX2_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX2_GLAC, obj.Loc_Code, trans)
                End If
                objVendorInvHead.TAX2_Rate = obj.TAX2_Rate
                objVendorInvHead.Tax2_BAmount = obj.TAX2_Base_Amt
                objVendorInvHead.TAX2_Amt = obj.TAX2_Amt
            End If
            If (clsCommon.myLen(obj.TAX3) > 0) Then
                objVendorInvHead.TAX3 = obj.TAX3
                If clsTaxMaster.ISTaxRecoverableAC(obj.TAX3, trans) Then
                    objVendorInvHead.TAX3_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX3, trans)
                    objVendorInvHead.TAX3_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX3_GLAC, obj.Loc_Code, trans)
                End If
                objVendorInvHead.TAX3_Rate = obj.TAX3_Rate
                objVendorInvHead.Tax3_BAmount = obj.TAX3_Base_Amt
                objVendorInvHead.TAX3_Amt = obj.TAX3_Amt
            End If
            If (clsCommon.myLen(obj.TAX4) > 0) Then
                objVendorInvHead.TAX4 = obj.TAX4
                If clsTaxMaster.ISTaxRecoverableAC(obj.TAX4, trans) Then
                    objVendorInvHead.TAX4_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX4, trans)
                    objVendorInvHead.TAX4_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX4_GLAC, obj.Loc_Code, trans)
                End If
                objVendorInvHead.TAX4_Rate = obj.TAX4_Rate
                objVendorInvHead.Tax4_BAmount = obj.TAX4_Base_Amt
                objVendorInvHead.TAX4_Amt = obj.TAX4_Amt
            End If
            If (clsCommon.myLen(obj.TAX5) > 0) Then
                objVendorInvHead.TAX5 = obj.TAX5
                If clsTaxMaster.ISTaxRecoverableAC(obj.TAX5, trans) Then
                    objVendorInvHead.TAX5_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX5, trans)
                    objVendorInvHead.TAX5_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX5_GLAC, obj.Loc_Code, trans)

                End If
                objVendorInvHead.TAX5_Rate = obj.TAX5_Rate
                objVendorInvHead.Tax5_BAmount = obj.TAX5_Base_Amt
                objVendorInvHead.TAX5_Amt = obj.TAX5_Amt
            End If
            If (clsCommon.myLen(obj.TAX6) > 0) Then
                objVendorInvHead.TAX6 = obj.TAX6
                If clsTaxMaster.ISTaxRecoverableAC(obj.TAX6, trans) Then
                    objVendorInvHead.TAX6_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX6, trans)
                    objVendorInvHead.TAX6_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX6_GLAC, obj.Loc_Code, trans)
                End If
                objVendorInvHead.TAX6_Rate = obj.TAX6_Rate
                objVendorInvHead.Tax6_BAmount = obj.TAX6_Base_Amt
                objVendorInvHead.TAX6_Amt = obj.TAX6_Amt
            End If
            If (clsCommon.myLen(obj.TAX7) > 0) Then
                objVendorInvHead.TAX7 = obj.TAX7
                If clsTaxMaster.ISTaxRecoverableAC(obj.TAX7, trans) Then
                    objVendorInvHead.TAX7_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX7, trans)
                    objVendorInvHead.TAX7_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX7_GLAC, obj.Loc_Code, trans)

                End If
                objVendorInvHead.TAX7_Rate = obj.TAX7_Rate
                objVendorInvHead.Tax7_BAmount = obj.TAX7_Base_Amt
                objVendorInvHead.TAX7_Amt = obj.TAX7_Amt
            End If
            If (clsCommon.myLen(obj.TAX8) > 0) Then
                objVendorInvHead.TAX8 = obj.TAX8
                If clsTaxMaster.ISTaxRecoverableAC(obj.TAX8, trans) Then
                    objVendorInvHead.TAX8_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX8, trans)
                    objVendorInvHead.TAX8_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX8_GLAC, obj.Loc_Code, trans)
                End If
                objVendorInvHead.TAX8_Rate = obj.TAX8_Rate
                objVendorInvHead.Tax8_BAmount = obj.TAX8_Base_Amt
                objVendorInvHead.TAX8_Amt = obj.TAX8_Amt
            End If
            If (clsCommon.myLen(obj.TAX9) > 0) Then
                objVendorInvHead.TAX9 = obj.TAX9
                If clsTaxMaster.ISTaxRecoverableAC(obj.TAX9, trans) Then
                    objVendorInvHead.TAX9_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX9, trans)
                    objVendorInvHead.TAX9_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX9_GLAC, obj.Loc_Code, trans)
                End If
                objVendorInvHead.TAX9_Rate = obj.TAX9_Rate
                objVendorInvHead.Tax9_BAmount = obj.TAX9_Base_Amt
                objVendorInvHead.TAX9_Amt = obj.TAX9_Amt
            End If
            If (clsCommon.myLen(obj.TAX10) > 0) Then
                objVendorInvHead.TAX10 = obj.TAX10
                If clsTaxMaster.ISTaxRecoverableAC(obj.TAX10, trans) Then
                    objVendorInvHead.TAX10_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX10, trans)
                    objVendorInvHead.TAX10_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX10_GLAC, obj.Loc_Code, trans)
                End If
                objVendorInvHead.TAX10_Rate = obj.TAX10_Rate
                objVendorInvHead.Tax10_BAmount = obj.TAX10_Base_Amt
                objVendorInvHead.TAX10_Amt = obj.TAX10_Amt
            End If


            For Each objPIDetail As clsMilkPurchaseInvoiceDetail In obj.arrDetail
                Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                Dim strICode As String = objPIDetail.Item_Code
                Dim strPaybleCleanigCtrlAC As String = ""
                ''Fill VendorInvoice details Data
                qry = "select TSPL_PURCHASE_ACCOUNTS.Purchase_JobWork,TSPL_ITEM_MASTER.Purchase_Class_Code,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing,TSPL_GL_ACCOUNTS.Description as ClearingACName, TSPL_PURCHASE_ACCOUNTS.Non_Stock_Clearing as EmptyAccount from  TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing where TSPL_ITEM_MASTER.Item_Code='" + strICode + "'"
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    Throw New Exception("Please set Purchase Account set for item " + strICode + "(" + objPIDetail.Item_Desc + ")")
                End If
                If obj.IsAgainstJobWork = 1 Then
                    strPaybleCleanigCtrlAC = clsCommon.myCstr(dt.Rows(0)("Purchase_JobWork"))
                    If clsCommon.myLen(strPaybleCleanigCtrlAC) = 0 Then
                        Dim strPurchaseaccountset = clsDBFuncationality.getSingleValue("select Purchase_Class_Code from TSPL_ITEM_MASTER where TSPL_ITEM_MASTER.Item_Code='" + strICode + "'", trans)
                        Throw New Exception("Please set Purchase Job work Account for item " + strICode + " Purchase Account Set  -  " + strPurchaseaccountset)
                    End If
                    strPaybleCleanigCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strPaybleCleanigCtrlAC, obj.Loc_Code, trans)
                Else
                    strPaybleCleanigCtrlAC = clsCommon.myCstr(dt.Rows(0)("Inv_Payable_Clearing"))
                    strPaybleCleanigCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strPaybleCleanigCtrlAC, obj.Loc_Code, trans)
                    objVendorInvDetail.Comments = "Y"
                End If

                Dim strPaybleCleanigCtrlACName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + strPaybleCleanigCtrlAC + "'", trans))


                If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                    objVendorInvHead.Empty_Account = clsCommon.myCstr(dt.Rows(0)("EmptyAccount"))
                    objVendorInvHead.Empty_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Empty_Account, obj.Loc_Code, trans)
                End If



                ii = ii + 1
                objVendorInvDetail.Detail_Line_No = ii
                objVendorInvDetail.GL_Account_Code = strPaybleCleanigCtrlAC
                objVendorInvDetail.GL_Account_Desc = strPaybleCleanigCtrlACName
                If ApplyTransportChargeAddInActualAmount = True Then
                    objVendorInvDetail.Amount = objPIDetail.Actual_Amount + objPIDetail.Transport_Charges
                    objVendorInvDetail.Amount_less_Discount = objPIDetail.Actual_Amount + objPIDetail.Transport_Charges
                Else
                    objVendorInvDetail.Amount = objPIDetail.Actual_Amount
                    objVendorInvDetail.Amount_less_Discount = objPIDetail.Actual_Amount
                End If
                'objVendorInvDetail.Amount = objPIDetail.Actual_Amount
                objVendorInvDetail.Discount_Per = 0
                objVendorInvDetail.Discount = 0
                'objVendorInvDetail.Amount_less_Discount = objPIDetail.Actual_Amount
                objVendorInvDetail.Total_Tax = 0
                'objVendorInvDetail.Total_Amount = objPIDetail.Actual_Amount






                objVendorInvDetail.TAX1 = objPIDetail.TAX1
                objVendorInvDetail.TAX1_Rate = objPIDetail.TAX1_Rate
                objVendorInvDetail.TAX1_Amt = objPIDetail.TAX1_Amt
                objVendorInvDetail.TAX1_Base_Amt = objPIDetail.TAX1_Base_Amt
                objVendorInvDetail.TAX2 = objPIDetail.TAX2
                objVendorInvDetail.TAX2_Rate = objPIDetail.TAX2_Rate
                objVendorInvDetail.TAX2_Amt = objPIDetail.TAX2_Amt
                objVendorInvDetail.TAX2_Base_Amt = objPIDetail.TAX2_Base_Amt
                objVendorInvDetail.TAX3 = objPIDetail.TAX3
                objVendorInvDetail.TAX3_Rate = objPIDetail.TAX3_Rate
                objVendorInvDetail.TAX3_Amt = objPIDetail.TAX3_Amt
                objVendorInvDetail.TAX3_Base_Amt = objPIDetail.TAX3_Base_Amt
                objVendorInvDetail.TAX4 = objPIDetail.TAX4
                objVendorInvDetail.TAX4_Rate = objPIDetail.TAX4_Rate
                objVendorInvDetail.TAX4_Amt = objPIDetail.TAX4_Amt
                objVendorInvDetail.TAX4_Base_Amt = objPIDetail.TAX4_Base_Amt
                objVendorInvDetail.TAX5 = objPIDetail.TAX5
                objVendorInvDetail.TAX5_Rate = objPIDetail.TAX5_Rate
                objVendorInvDetail.TAX5_Amt = objPIDetail.TAX5_Amt
                objVendorInvDetail.TAX5_Base_Amt = objPIDetail.TAX5_Base_Amt
                objVendorInvDetail.TAX6 = objPIDetail.TAX6
                objVendorInvDetail.TAX6_Rate = objPIDetail.TAX6_Rate
                objVendorInvDetail.TAX6_Amt = objPIDetail.TAX6_Amt
                objVendorInvDetail.TAX6_Base_Amt = objPIDetail.TAX6_Base_Amt
                objVendorInvDetail.TAX7 = objPIDetail.TAX7
                objVendorInvDetail.TAX7_Rate = objPIDetail.TAX7_Rate
                objVendorInvDetail.TAX7_Amt = objPIDetail.TAX7_Amt
                objVendorInvDetail.TAX7_Base_Amt = objPIDetail.TAX7_Base_Amt
                objVendorInvDetail.TAX8 = objPIDetail.TAX8
                objVendorInvDetail.TAX8_Rate = objPIDetail.TAX8_Rate
                objVendorInvDetail.TAX8_Amt = objPIDetail.TAX8_Amt
                objVendorInvDetail.TAX8_Base_Amt = objPIDetail.TAX8_Base_Amt
                objVendorInvDetail.TAX9 = objPIDetail.TAX9
                objVendorInvDetail.TAX9_Rate = objPIDetail.TAX9_Rate
                objVendorInvDetail.TAX9_Amt = objPIDetail.TAX9_Amt
                objVendorInvDetail.TAX9_Base_Amt = objPIDetail.TAX9_Base_Amt
                objVendorInvDetail.TAX10 = objPIDetail.TAX10
                objVendorInvDetail.TAX10_Rate = objPIDetail.TAX10_Rate
                objVendorInvDetail.TAX10_Amt = objPIDetail.TAX10_Amt
                objVendorInvDetail.TAX10_Base_Amt = objPIDetail.TAX10_Base_Amt
                objVendorInvDetail.Total_Tax = objPIDetail.Total_Tax_Amt
                If objPIDetail.Item_Net_Amt = 0 Then
                    objPIDetail.Item_Net_Amt = objPIDetail.Actual_Amount
                End If
                objVendorInvDetail.Total_Amount = objPIDetail.Item_Net_Amt


                objVendorInvDetail.Landed_Amount = objPIDetail.Item_Net_Amt

                objVendorInvHead.Total_Landed_Amt += objVendorInvDetail.Landed_Amount

                If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                    objVendorInvHead.Arr.Add(objVendorInvDetail)
                End If
                ''End of Fill Vendor Invoice Detail Data
            Next

            objVendorInvHead.Empty_Amount = 0 'obj.Tot_Empty_Amount
            If objVendorInvHead.Empty_Amount > 0 Then
                If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                    Throw New Exception("Please set Inventory Control Empties")
                End If
                objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
            End If

            If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                Throw New Exception("No GL Account Found For AP Invoice")
            End If
            ' objVendorInvHead.ApplicableFrom = clsCommon.myCDate(obj.DOC_DATE, "dd/MMM/yyyy") 'obj.DOC_DATE


            If obj.objPIRemittance IsNot Nothing Then
                objVendorInvHead.RemittanceObject = New clsRemittance()
                objVendorInvHead.RemittanceObject.Vendor_Code = obj.objPIRemittance.Vendor_Code
                objVendorInvHead.RemittanceObject.Vendor_Name = obj.objPIRemittance.Vendor_Name
                objVendorInvHead.RemittanceObject.Document_No = obj.objPIRemittance.Document_No
                objVendorInvHead.RemittanceObject.Document_Date = obj.objPIRemittance.Document_Date
                objVendorInvHead.RemittanceObject.Document_Type = obj.objPIRemittance.Document_Type
                objVendorInvHead.RemittanceObject.Document_Amount = obj.objPIRemittance.Document_Amount
                objVendorInvHead.RemittanceObject.Service_Type = obj.objPIRemittance.Service_Type
                objVendorInvHead.RemittanceObject.Actual_TDS_Base = obj.objPIRemittance.Actual_TDS_Base
                objVendorInvHead.RemittanceObject.Calculated_TDS_Base = obj.objPIRemittance.Calculated_TDS_Base
                objVendorInvHead.RemittanceObject.Actual_TDS = obj.objPIRemittance.Actual_TDS
                objVendorInvHead.RemittanceObject.Calculated_TDS = obj.objPIRemittance.Calculated_TDS
                objVendorInvHead.RemittanceObject.Actual_Surcharge = obj.objPIRemittance.Actual_Surcharge
                objVendorInvHead.RemittanceObject.Calculated_Surcharge = obj.objPIRemittance.Calculated_Surcharge
                objVendorInvHead.RemittanceObject.Actual_Edu_Cess = obj.objPIRemittance.Actual_Edu_Cess
                objVendorInvHead.RemittanceObject.Calculated_Edu_Cess = obj.objPIRemittance.Calculated_Edu_Cess
                objVendorInvHead.RemittanceObject.Actual_Sec_Educess = obj.objPIRemittance.Actual_Sec_Educess
                objVendorInvHead.RemittanceObject.Calculated_Sec_Educess = obj.objPIRemittance.Calculated_Sec_Educess
                objVendorInvHead.RemittanceObject.Actual_Total_TDS = obj.objPIRemittance.Actual_Total_TDS
                objVendorInvHead.RemittanceObject.Calculated_Total_TDS = obj.objPIRemittance.Calculated_Total_TDS
                objVendorInvHead.RemittanceObject.Fiscal_Year = obj.objPIRemittance.Fiscal_Year
                objVendorInvHead.RemittanceObject.Quarter = obj.objPIRemittance.Quarter
                objVendorInvHead.RemittanceObject.Section_Code = obj.objPIRemittance.Section_Code
                objVendorInvHead.RemittanceObject.Section_Description = obj.objPIRemittance.Section_Description
                objVendorInvHead.RemittanceObject.Branch_Code = obj.objPIRemittance.Branch_Code
                objVendorInvHead.RemittanceObject.Deduction_Code = obj.objPIRemittance.Deduction_Code
                objVendorInvHead.RemittanceObject.TDS_Per = obj.objPIRemittance.TDS_Per
                objVendorInvHead.RemittanceObject.Surcharge_Per = obj.objPIRemittance.Surcharge_Per
                objVendorInvHead.RemittanceObject.Edu_Cess_Per = obj.objPIRemittance.Edu_Cess_Per
                objVendorInvHead.RemittanceObject.Sec_Educess_Per = obj.objPIRemittance.Sec_Educess_Per
                objVendorInvHead.RemittanceObject.Select_By = obj.objPIRemittance.Select_By
                objVendorInvHead.RemittanceObject.IsTDSOverride = obj.objPIRemittance.IsTDSOverride
                objVendorInvHead.RemittanceObject.IsApplyTDS = obj.objPIRemittance.IsApplyTDS
                objVendorInvHead.TDS_Base_Actual_Amount = obj.objPIRemittance.Actual_TDS_Base
                objVendorInvHead.TDS_Base_Calculated_Amount = obj.objPIRemittance.Calculated_TDS_Base
                objVendorInvHead.TDS_Percentage = obj.objPIRemittance.TDS_Per
                objVendorInvHead.TDS_Actual_Amount = obj.objPIRemittance.Actual_Total_TDS
                objVendorInvHead.TDS_Calculated_Amount = obj.objPIRemittance.Calculated_Total_TDS
                objVendorInvHead.Nature_of_deduction = obj.objPIRemittance.Deduction_Code
                objVendorInvHead.Branch_Code = obj.objPIRemittance.Branch_Code
                objVendorInvHead.Balance_Amt = obj.Total_AMT - obj.objPIRemittance.Actual_Total_TDS
                objVendorInvHead.Section_Code = obj.objPIRemittance.Section_Code
            End If

            If isRecreateAPInvoice Then
                objVendorInvHead.Document_No = strAPInvNo
                isSaved = isSaved AndAlso objVendorInvHead.SaveData(objVendorInvHead, True, trans, strAPInvJENo)
                isSaved = isSaved AndAlso clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans, strAPInvJENo)
            Else

                isSaved = isSaved AndAlso objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                isSaved = isSaved AndAlso clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans, obj.DOC_DATE)
            End If


            Dim isAmountDecreased As Boolean = False
            'Create GL Entry
            Dim TankerFromMaster As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GateEntryTankerFromTankerMaster, clsFixedParameterCode.GateEntryTankerFromTankerMaster, trans))
            Dim intHighClassVendor As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select isHighClass from TSPL_VENDOR_MASTER where Vendor_Code='" & obj.vendor_code & "'", trans))

            For i As Integer = 0 To obj.arrDetail.Count - 1
                Dim SrnAmt As Double = 0
                Dim ApprovedAmt As Double = 0
                Dim diffamt As Double = 0
                Dim tnkrNo As String = ""
                Dim Loc As String = ""
                Dim Silo As String = ""
                Dim vendorNo As String = ""
                Dim q As String = ""
                If obj.isSRNTradeInvoice = 0 Then
                    If TankerFromMaster = 0 Then
                        If ApplyTransportChargeAddInActualAmount = True Then
                            q = " select tspl_Bulk_milk_purchase_Invoice_Detail.Actual_Amount as InvAmt,TSPL_Bulk_MILK_SRN.Milk_Amount as SrnAmt,TSPL_Bulk_MILK_SRN.Vendor_Code,TSPL_Bulk_MILK_SRN.Loc_Code,tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO,TSPL_Bulk_MILK_SRN.Tanker_No,isnull(TSPL_Bulk_MILK_SRN.Milk_Amount,0)-isnull(tspl_Bulk_milk_purchase_Invoice_Detail.Actual_Amount,0) "
                            q += " as diffamt,TSPL_MILK_UNLOADING.Sub_location_Code      from tspl_Bulk_milk_purchase_Invoice_Detail  left outer join TSPL_Bulk_MILK_SRN on TSPL_Bulk_MILK_SRN.SRN_NO=tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO left outer join TSPL_MILK_UNLOADING on TSPL_MILK_UNLOADING.Gate_Entry_No=TSPL_Bulk_MILK_SRN.Gate_Entry_No   where TSPL_Bulk_MILK_SRN.SRN_NO='" & obj.arrDetail.Item(i).SRN_NO & "' "
                        Else
                            q = " select tspl_Bulk_milk_purchase_Invoice_Detail.Actual_Amount as InvAmt,TSPL_Bulk_MILK_SRN.Actual_Amount as SrnAmt,TSPL_Bulk_MILK_SRN.Vendor_Code,TSPL_Bulk_MILK_SRN.Loc_Code,tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO,TSPL_Bulk_MILK_SRN.Tanker_No,isnull(TSPL_Bulk_MILK_SRN.Actual_Amount,0)-isnull(tspl_Bulk_milk_purchase_Invoice_Detail.Actual_Amount,0) as diffamt,TSPL_MILK_UNLOADING.Sub_location_Code      from tspl_Bulk_milk_purchase_Invoice_Detail  left outer join TSPL_Bulk_MILK_SRN on TSPL_Bulk_MILK_SRN.SRN_NO=tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO left outer join TSPL_MILK_UNLOADING on TSPL_MILK_UNLOADING.Gate_Entry_No=TSPL_Bulk_MILK_SRN.Gate_Entry_No   where TSPL_Bulk_MILK_SRN.SRN_NO='" & obj.arrDetail.Item(i).SRN_NO & "' "
                        End If

                    Else
                        q = " select tspl_Bulk_milk_purchase_Invoice_Detail.Actual_Amount as InvAmt,TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Actual_Amount as SrnAmt,TSPL_Bulk_MILK_SRN.Vendor_Code, " &
                            "TSPL_Bulk_MILK_SRN.Loc_Code,tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO,TSPL_Bulk_MILK_SRN.Tanker_No, " &
                            "isnull(TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Actual_Amount,0)-isnull(tspl_Bulk_milk_purchase_Invoice_Detail.Actual_Amount,0) as diffamt, " &
                            "TSPL_MILK_UNLOADING.Sub_location_Code      from tspl_Bulk_milk_purchase_Invoice_Detail  left outer join " &
                            "TSPL_Bulk_MILK_SRN on TSPL_Bulk_MILK_SRN.SRN_NO=tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO left outer join " &
                            "TSPL_BULK_MILK_SRN_CHEMBER_DETAILS on TSPL_Bulk_MILK_SRN.SRN_NO=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SRN_NO and " &
                            "tspl_Bulk_milk_purchase_Invoice_Detail.SL_NO=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Line_No  left outer join " &
                            "TSPL_MILK_UNLOADING on TSPL_MILK_UNLOADING.Gate_Entry_No=TSPL_Bulk_MILK_SRN.Gate_Entry_No   " &
                            "where TSPL_Bulk_MILK_SRN.SRN_NO='" & obj.arrDetail.Item(i).SRN_NO & "' and TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.CHAMBER_DESC='" & obj.arrDetail.Item(i).CHAMBER_DESC & "'"
                    End If
                Else
                    q = " select tspl_Bulk_milk_purchase_Invoice_Detail.Actual_Amount as InvAmt,TSPL_Bulk_MILK_SRN.Actual_Amount as SrnAmt,TSPL_Bulk_MILK_SRN.Vendor_Code,TSPL_Bulk_MILK_SRN.Loc_Code,tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO,TSPL_Bulk_MILK_SRN.Tanker_No,isnull(TSPL_Bulk_MILK_SRN.Actual_Amount,0)-isnull(tspl_Bulk_milk_purchase_Invoice_Detail.Actual_Amount,0) as diffamt,TSPL_Bulk_MILK_SRN.Sub_location as  Sub_location_Code      from tspl_Bulk_milk_purchase_Invoice_Detail  left outer join TSPL_Bulk_MILK_SRN on TSPL_Bulk_MILK_SRN.SRN_NO=tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO    where TSPL_Bulk_MILK_SRN.SRN_NO='" & obj.arrDetail.Item(i).SRN_NO & "' "
                End If

                Dim dtt As DataTable = clsDBFuncationality.GetDataTable(q, trans)
                If dtt IsNot Nothing AndAlso dtt.Rows.Count > 0 Then
                    SrnAmt = clsCommon.myCdbl(dtt.Rows(0)("SrnAmt"))
                    ApprovedAmt = clsCommon.myCdbl(dtt.Rows(0)("InvAmt"))
                    diffamt = clsCommon.myCdbl(dtt.Rows(0)("diffamt"))
                    tnkrNo = clsCommon.myCstr(dtt.Rows(0)("Tanker_No"))
                    Loc = clsCommon.myCstr(dtt.Rows(0)("Loc_Code"))
                    Silo = clsCommon.myCstr(dtt.Rows(0)("Sub_location_Code"))
                    vendorNo = clsCommon.myCstr(dtt.Rows(0)("Vendor_Code"))

                    If diffamt > 0 Then
                        isAmountDecreased = False
                    Else
                        isAmountDecreased = True
                        diffamt = diffamt * -1
                    End If
                    ''richa agarwal  chnage setting (remove tankerFromMaster setting) store adjustment not required in case of high class vendor
                    'If Not (TankerFromMaster = 1 And intHighClassVendor = 1) Then 
                    If intHighClassVendor = 0 Then
                        If diffamt > 0 Then
                            If isAmountDecreased = False Then '' richa as per ranjana mam store adjust IN type when invoice amt is greater than srn amount
                                ''Adjustment Out Type document
                                Dim objAdjOut As New ClsAdjustments
                                objAdjOut.Adjustment_Date = obj.DOC_DATE
                                objAdjOut.Posting_Date = obj.DOC_DATE
                                objAdjOut.EntryDateTime = obj.DOC_DATE
                                objAdjOut.Against_Bulk_Srn_PI_adjustment = obj.DOC_NO
                                objAdjOut.Loc_Code = obj.Loc_Code
                                objAdjOut.Loc_Desc = clsLocation.GetName(obj.Loc_Code, trans)
                                objAdjOut.Trans_Type = "Out"
                                objAdjOut.IsMilkType = 1
                                objAdjOut.Loc_Code = Silo
                                objAdjOut.MainLocationCode = Loc
                                objAdjOut.Description = " Adjustment Against Bulk Milk SRN-PI Cost Adjustment For SRN NO: " & obj.arrDetail.Item(i).SRN_NO & " PI No: " & obj.DOC_NO & " Tanker No: " & tnkrNo & " Vendor : " & clsVendorMaster.GetName(vendorNo, trans) & " Location: " & clsLocation.GetName(Loc, trans)
                                objAdjOut.Arr = New List(Of ClsAdjustmentsDetails)
                                Dim objAdjOutTR As New ClsAdjustmentsDetails()

                                objAdjOutTR.Item_Code = obj.arrDetail.Item(0).Item_Code
                                objAdjOutTR.Item_Description = obj.arrDetail.Item(0).Item_Desc
                                objAdjOutTR.Adjustment_Type = "CD"
                                objAdjOutTR.Item_Quantity = 0
                                objAdjOutTR.Item_Cost = diffamt
                                objAdjOutTR.mrp = 0
                                objAdjOutTR.Unit_Code = obj.arrDetail.Item(0).UOM

                                objAdjOutTR.fat_Amt = 0
                                If ((obj.arrDetail.Item(0).fat_KG * obj.arrDetail.Item(0).fat_Rate) + (obj.arrDetail.Item(0).SNF_KG * obj.arrDetail.Item(0).SNF_Rate)) > 0 Then
                                    objAdjOutTR.fat_Amt = Math.Round(diffamt * ((obj.arrDetail.Item(0).fat_KG * obj.arrDetail.Item(0).fat_Rate) / ((obj.arrDetail.Item(0).fat_KG * obj.arrDetail.Item(0).fat_Rate) + (obj.arrDetail.Item(0).SNF_KG * obj.arrDetail.Item(0).SNF_Rate))), 2)
                                End If
                                objAdjOutTR.snf_Amt = diffamt - objAdjOutTR.fat_Amt

                                objAdjOut.Arr.Add(objAdjOutTR)
                                objAdjOut.SaveData(objAdjOut, True, "", trans)
                                ClsAdjustments.PostData(objAdjOut.Adjustment_No, objAdjOut.Trans_Type, trans, False)
                            Else
                                ''Adjustment IN Type document
                                Dim objAdjIn As New ClsAdjustments
                                objAdjIn.Adjustment_Date = obj.DOC_DATE
                                objAdjIn.Posting_Date = obj.DOC_DATE
                                objAdjIn.EntryDateTime = obj.DOC_DATE
                                objAdjIn.Against_Bulk_Srn_PI_adjustment = obj.DOC_NO
                                objAdjIn.Loc_Code = obj.Loc_Code
                                objAdjIn.Loc_Desc = clsLocation.GetName(obj.Loc_Code, trans)
                                objAdjIn.Trans_Type = "In"
                                objAdjIn.IsMilkType = 1
                                objAdjIn.Loc_Code = Silo
                                objAdjIn.MainLocationCode = Loc
                                objAdjIn.Description = " Adjustment Against Bulk Milk SRN-PI Cost Adjustment For SRN NO: " & obj.arrDetail.Item(i).SRN_NO & " PI No: " & obj.DOC_NO & " Tanker No: " & tnkrNo & " Vendor : " & clsVendorMaster.GetName(vendorNo, trans) & " Location: " & clsLocation.GetName(Loc, trans)
                                objAdjIn.Arr = New List(Of ClsAdjustmentsDetails)
                                Dim objAdjInTR As New ClsAdjustmentsDetails()

                                objAdjInTR.Item_Code = obj.arrDetail.Item(0).Item_Code
                                objAdjInTR.Item_Description = obj.arrDetail.Item(0).Item_Desc
                                objAdjInTR.Adjustment_Type = "CI"
                                objAdjInTR.Item_Quantity = 0
                                objAdjInTR.Item_Cost = diffamt
                                objAdjInTR.mrp = 0
                                objAdjInTR.Unit_Code = obj.arrDetail.Item(0).UOM

                                objAdjInTR.fat_Amt = 0
                                If ((obj.arrDetail.Item(0).fat_KG * obj.arrDetail.Item(0).fat_Rate) + (obj.arrDetail.Item(0).SNF_KG * obj.arrDetail.Item(0).SNF_Rate)) > 0 Then
                                    objAdjInTR.fat_Amt = Math.Round(diffamt * ((obj.arrDetail.Item(0).fat_KG * obj.arrDetail.Item(0).fat_Rate) / ((obj.arrDetail.Item(0).fat_KG * obj.arrDetail.Item(0).fat_Rate) + (obj.arrDetail.Item(0).SNF_KG * obj.arrDetail.Item(0).SNF_Rate))), 2)
                                End If
                                objAdjInTR.snf_Amt = diffamt - objAdjInTR.fat_Amt

                                objAdjIn.Arr.Add(objAdjInTR)
                                objAdjIn.SaveData(objAdjIn, True, "", trans)
                                ClsAdjustments.PostData(objAdjIn.Adjustment_No, objAdjIn.Trans_Type, trans, False)

                            End If
                        End If
                    End If


                End If
            Next
            ''richa agarwal  chnage setting (remove tankerFromMaster setting)
            'If Not (TankerFromMaster = 1 And intHighClassVendor = 1) Then
            If intHighClassVendor = 0 Then
                qry = " select TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing ,TSPL_Bulk_MILK_SRN.Actual_Amount,TSPL_Bulk_MILK_SRN.Loc_Code   from TSPL_PURCHASE_ACCOUNTS left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Purchase_Class_Code =TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code left outer join TSPL_Bulk_MILK_SRN on TSPL_Bulk_MILK_SRN .Item_Code=TSPL_ITEM_MASTER.Item_Code where TSPL_Bulk_MILK_SRN.SRN_NO='" & obj.arrDetail.Item(0).SRN_NO & "' "
                Dim ArryLst As ArrayList = New ArrayList()
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                Dim TotActualAmount As Double = 0
                If TankerFromMaster = 0 Then
                    If ApplyTransportChargeAddInActualAmount = True Then
                        TotActualAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select SUM(TSPL_Bulk_MILK_SRN.Milk_Amount ) as acTAmt from TSPL_Bulk_MILK_SRN   where SRN_NO in (select SRN_NO from tspl_Bulk_milk_purchase_Invoice_Detail where DOC_NO='" & obj.DOC_NO & "')", trans))
                    Else
                        TotActualAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select SUM(TSPL_Bulk_MILK_SRN.Actual_Amount ) as acTAmt from TSPL_Bulk_MILK_SRN   where SRN_NO in (select SRN_NO from tspl_Bulk_milk_purchase_Invoice_Detail where DOC_NO='" & obj.DOC_NO & "')", trans))
                    End If
                Else
                    TotActualAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select SUM(TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Actual_Amount ) as acTAmt from TSPL_BULK_MILK_SRN_CHEMBER_DETAILS   where TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SRN_NO in (select tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO from tspl_Bulk_milk_purchase_Invoice_Detail where DOC_NO='" & obj.DOC_NO & "')", trans))
                End If
                Dim DiffAmount As Double = 0
                If ApplyTransportChargeAddInActualAmount = True Then
                    DiffAmount = (TotActualAmount - obj.Total_Taxable_Amount)
                Else
                    DiffAmount = (TotActualAmount + obj.RoundOffAmount) - (obj.Total_AMT)
                End If


                If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0 Then
                    If DiffAmount > 0 Then
                        isAmountDecreased = False
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            Dim strInvCntrlAc As String = dt.Rows(0)("Inv_Control_Account")
                            Dim strPaybleClrAc As String = dt.Rows(0)("Inv_Payable_Clearing")
                            strInvCntrlAc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvCntrlAc, dt.Rows(0)("Loc_Code"), trans)
                            strPaybleClrAc = clsERPFuncationality.ChangeGLAccountLocationSegment(strPaybleClrAc, dt.Rows(0)("Loc_Code"), trans)
                            ArryLst.Add(New String() {strInvCntrlAc, DiffAmount * -1})
                            ArryLst.Add(New String() {strPaybleClrAc, DiffAmount, "", "", "", "", "", "", "Y"})
                            transportSql.FunGrnlEntryWithTrans(obj.Loc_Code, False, trans, clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy"), "Adjustment Against Bulk Milk Purchase Invoice  -" + obj.DOC_NO + "", "BM-PI", "Bulk Milk Purchase Invoice", obj.DOC_NO, "", "C", obj.DOC_NO, obj.DOC_NO, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, "", " Vendor - " & obj.vendor_code & ", " & clsVendorMaster.GetName(obj.vendor_code, trans))
                        End If
                    ElseIf DiffAmount < 0 Then
                        isAmountDecreased = True
                        DiffAmount = DiffAmount * -1
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            Dim strInvCntrlAc As String = dt.Rows(0)("Inv_Control_Account")
                            Dim strPaybleClrAc As String = dt.Rows(0)("Inv_Payable_Clearing")
                            strInvCntrlAc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvCntrlAc, dt.Rows(0)("Loc_Code"), trans)
                            strPaybleClrAc = clsERPFuncationality.ChangeGLAccountLocationSegment(strPaybleClrAc, dt.Rows(0)("Loc_Code"), trans)
                            ArryLst.Add(New String() {strInvCntrlAc, DiffAmount})
                            ArryLst.Add(New String() {strPaybleClrAc, DiffAmount * -1, "", "", "", "", "", "", "Y"})
                            transportSql.FunGrnlEntryWithTrans(obj.Loc_Code, False, trans, clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy"), "Adjustment Against Bulk Milk Purchase Invoice  -" + obj.DOC_NO + "", "BM-PI", "Bulk Milk Purchase Invoice", obj.DOC_NO, "", "C", obj.DOC_NO, obj.DOC_NO, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, "", " Vendor - " & obj.vendor_code & ", " & clsVendorMaster.GetName(obj.vendor_code, trans))
                        End If
                    End If
                End If

            Else
                ' done by priti BHA/21/06/18-000070 ERO/31/07/19-000974
                ' in case of chamber wise and high class  vendor for bharat
                Dim ArryLst As ArrayList = New ArrayList()
                For Each objTr As clsMilkPurchaseInvoiceDetail In obj.arrDetail
                    Dim SrnAmt As Double = 0
                    Dim InvAmt As Double = objTr.Actual_Amount
                    If ApplyTransportChargeAddInActualAmount = True Then
                        qry = "select TSPL_Bulk_MILK_SRN.Loc_Code,isnull(TSPL_PURCHASE_ACCOUNTS.Loss_Ac,'') as Loss_Ac,isnull(TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing,'') as Inv_Payable_Clearing,TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Milk_Amount as SrnAmt  " &
                        "from tspl_bulk_milk_srn left outer join TSPL_BULK_MILK_SRN_CHEMBER_DETAILS on tspl_bulk_milk_srn.SRN_NO =TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SRN_NO " &
                        "left outer join tspl_item_master on TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Item_Code=TSPL_ITEM_MASTER.Item_Code left outer join  " &
                        "TSPL_PURCHASE_ACCOUNTS on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code " &
                        "where tspl_bulk_milk_srn.SRN_NO='" & objTr.SRN_NO & "'  and TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.CHAMBER_DESC='" & objTr.CHAMBER_DESC & "' "
                    Else
                        qry = "select TSPL_Bulk_MILK_SRN.Loc_Code,isnull(TSPL_PURCHASE_ACCOUNTS.Loss_Ac,'') as Loss_Ac,isnull(TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing,'') as Inv_Payable_Clearing,TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Actual_Amount as SrnAmt  " &
                       "from tspl_bulk_milk_srn left outer join TSPL_BULK_MILK_SRN_CHEMBER_DETAILS on tspl_bulk_milk_srn.SRN_NO =TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SRN_NO " &
                       "left outer join tspl_item_master on TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Item_Code=TSPL_ITEM_MASTER.Item_Code left outer join  " &
                       "TSPL_PURCHASE_ACCOUNTS on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code " &
                       "where tspl_bulk_milk_srn.SRN_NO='" & objTr.SRN_NO & "'  and TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.CHAMBER_DESC='" & objTr.CHAMBER_DESC & "' "
                    End If

                    Dim dtt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtt IsNot Nothing AndAlso dtt.Rows.Count > 0 Then
                        SrnAmt = clsCommon.myCdbl(dtt.Rows(0)("SrnAmt"))
                    End If
                    Dim DiffAmt As Double = InvAmt - SrnAmt
                    ' done by priti BHA/12/07/18-000153 for bharat
                    If DiffAmt <> 0 Then
                        Dim strLossAc As String = clsCommon.myCstr(dtt.Rows(0)("Loss_Ac"))
                        Dim strPaybleClrAc As String = clsCommon.myCstr(dtt.Rows(0)("Inv_Payable_Clearing"))
                        strLossAc = clsERPFuncationality.ChangeGLAccountLocationSegment(strLossAc, dtt.Rows(0)("Loc_Code"), trans)
                        strPaybleClrAc = clsERPFuncationality.ChangeGLAccountLocationSegment(strPaybleClrAc, dtt.Rows(0)("Loc_Code"), trans)
                        ' done by priti for bharat BHA/13/07/18-000155 
                        If clsCommon.myLen(strLossAc) = 0 Then
                            Throw New Exception("Please set Loss Account for item - " & objTr.Item_Code)
                        End If
                        If clsCommon.myLen(strPaybleClrAc) = 0 Then
                            Throw New Exception("Please set Payable Account for item - " & objTr.Item_Code)
                        End If
                        ArryLst.Add(New String() {strLossAc, DiffAmt})
                        ArryLst.Add(New String() {strPaybleClrAc, DiffAmt * -1, "", "", "", "", "", "", "Y"})
                    End If
                Next
                transportSql.FunGrnlEntryWithTrans(obj.Loc_Code, False, trans, clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy"), "Adjustment Against Bulk Milk Purchase Invoice  -" + obj.DOC_NO + "", "BM-PI", "Bulk Milk Purchase Invoice", obj.DOC_NO, "", "C", obj.DOC_NO, obj.DOC_NO, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, "", " Vendor - " & obj.vendor_code & ", " & clsVendorMaster.GetName(obj.vendor_code, trans))

            End If


            qry = "Update TSPL_Bulk_MILK_PURCHASE_INVOICE_HEAD set isPosted=1, Posting_Date='" + clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy") + "'"
            qry += " where DOC_no='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If isTransLocallyInitiatted Then
                If isSaved Then
                    trans.Commit()
                Else
                    trans.Rollback()
                End If
            End If
        Catch ex As Exception
            If isTransLocallyInitiatted Then
                trans.Rollback()
            End If
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(strCode) <= 0 Then
                Throw New Exception("Transaction No not found for reverse and unpost")
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Loc_Code,DOC_DATE from TSPL_BULK_MILK_PURCHASE_INVOICE_head where DOC_NO='" + strCode + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkMilkProcurement, clsUserMgtCode.frmBulkMilkPurchaseInvoice, clsCommon.myCstr(dt.Rows(0)("Loc_Code")), clsCommon.myCDate(dt.Rows(0)("DOC_DATE")), trans)

            End If
            Dim Qry As String = "select isPosted from tspl_Bulk_milk_purchase_Invoice_head where DOC_NO='" + strCode + "'"
            If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If

            Qry = "select top 1 Pur_Return_No from TSPL_BULK_MILK_PURCHASE_RETURN_HEAD  where  Invoice_No='" + strCode + "'"
            dt = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Qry = "Current Invoice is used in following Bulk Milk Purchase Return -"
                For Each dr As DataRow In dt.Rows
                    Qry += Environment.NewLine + clsCommon.myCstr(dr("Pur_Return_No"))
                Next
                Throw New Exception(Qry)
            End If

            '' Get Payment Entry Against Purchase  Invoice

            Qry = "select Payment_No from TSPL_PAYMENT_DETAIL  where Document_No in ('" + strCode + "')"
            dt = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Qry = "Purchase-Invoice " + strCode + " is used in following Payment -"
                For Each drAP As DataRow In dt.Rows
                    Qry += Environment.NewLine + clsCommon.myCstr(drAP("Payment_No"))
                Next
                Throw New Exception(Qry)
            End If

            Dim APInvoiceNo As String = clsDBFuncationality.getSingleValue("SELECT Document_No FROM TSPL_VENDOR_INVOICE_HEAD WHERE Against_BulkMillkPurchaseInvoice_No='" + strCode + "'", trans)
            If clsCommon.myLen(APInvoiceNo) > 0 Then

                clsVedorInvoiceHead.ReverseAndUnpost(APInvoiceNo, trans)
                clsVedorInvoiceHead.DeleteData(APInvoiceNo, trans)


                '' Get Payment Entry Against AP Invoice

                'Qry = "select Payment_No from TSPL_PAYMENT_DETAIL  where Document_No in ('" + APInvoiceNo + "')"
                'dt = clsDBFuncationality.GetDataTable(Qry, trans)
                'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                '    Qry = "AP-Invoice " + APInvoiceNo + " is used in following Payment -"
                '    For Each drAP As DataRow In dt.Rows
                '        Qry += Environment.NewLine + clsCommon.myCstr(drAP("Payment_No"))
                '    Next
                '    Throw New Exception(Qry)
                'End If

                'Dim APVoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='AP-IN' and Source_Doc_No='" + APInvoiceNo + "'", trans)
                'If clsCommon.myLen(APVoucherNo) > 0 Then
                '    Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + APVoucherNo + "'"
                '    clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                '    Qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + APVoucherNo + "'"
                '    clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                'End If

                'Qry = "delete from TSPL_VENDOR_INVOICE_DETAIL where Document_No ='" + APInvoiceNo + "'"
                'clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                'Qry = "delete from TSPL_VENDOR_INVOICE_HEAD where Document_No ='" + APInvoiceNo + "'"
                'clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If

            Dim AdjustmentNo As String = clsDBFuncationality.getSingleValue("SELECT top 1 Adjustment_No FROM TSPL_ADJUSTMENT_HEADER WHERE Against_Bulk_Srn_PI_adjustment='" + strCode + "'", trans)
            If clsCommon.myLen(AdjustmentNo) > 0 Then
                Qry = "delete from TSPL_ADJUSTMENT_DETAIL where Adjustment_No IN (SELECT Adjustment_No FROM TSPL_ADJUSTMENT_HEADER WHERE Against_Bulk_Srn_PI_adjustment = '" + strCode + "')"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_ADJUSTMENT_HEADER where Against_Bulk_Srn_PI_adjustment ='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No IN (SELECT Adjustment_No FROM TSPL_ADJUSTMENT_HEADER WHERE Against_Bulk_Srn_PI_adjustment = '" + strCode + "') and Trans_Type='IC-AD'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If

            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='BM-PI' and Source_Doc_No='" + strCode + "'", trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If
            'Qry = "delete from tspl_Bulk_milk_purchase_Invoice_Detail where DOC_NO='" + strCode + "'"
            'clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            'Qry = "delete from tspl_Bulk_milk_purchase_Invoice_head where DOC_NO='" + strCode + "'"
            'clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            ''Change status to unpost
            ' Ticket No - BHA/10/09/18-000531 By Prabhakar 
            Qry = "update tspl_Bulk_milk_purchase_Invoice_head set isPosted=0 where DOC_NO in ('" + strCode + "')"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function deleteData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Loc_Code,DOC_DATE from TSPL_BULK_MILK_PURCHASE_INVOICE_head where DOC_NO='" + strDocNo + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkMilkProcurement, clsUserMgtCode.frmBulkMilkPurchaseInvoice, clsCommon.myCstr(dt.Rows(0)("Loc_Code")), clsCommon.myCDate(dt.Rows(0)("DOC_DATE")), trans)

            End If
            clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_PI_REMITTANCE where Document_No='" + strDocNo + "'", trans)
            Dim qry2 As String = "delete from TSPL_BULK_MILK_PURCHASE_INVOICE_head where DOc_no='" & strDocNo & "'"
            Dim qry1 As String = "delete from TSPL_BULK_MILK_PURCHASE_INVOICE_detail where doc_no='" & strDocNo & "'"
            Dim isDeleted As Boolean = True
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry1, trans)
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry2, trans)
            Return isDeleted
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try
            Dim qry As String = "select TSPL_BULK_MILK_PURCHASE_INVOICE_head.DOC_NO as [DocNo] ,convert(varchar,TSPL_BULK_MILK_PURCHASE_INVOICE_head.DOC_DATE,103) as [Doc Date] ,TSPL_BULK_MILK_PURCHASE_INVOICE_head.vendor_code as [Vendor Code],tspl_vendor_master.vendor_name as [Vendor Name],tspl_bulk_milk_purchase_invoice_head.loc_code as [Location Code],tspl_location_master.location_desc as [Location Desc] ,TSPL_BULK_MILK_PURCHASE_INVOICE_head.Total_FAT_KG as [Total Fat Kg] ,TSPL_BULK_MILK_PURCHASE_INVOICE_head.Total_SNF_KG as [Total SNF Kg] ,TSPL_BULK_MILK_PURCHASE_INVOICE_head.Total_QTY as [Total Qty] ,TSPL_BULK_MILK_PURCHASE_INVOICE_head.Total_AMT as [Total Amt] ,case when isnull(TSPL_BULK_MILK_PURCHASE_INVOICE_head.isPosted,0)=0 then 'No' else 'Yes' end as [Is Posted] ,TSPL_BULK_MILK_PURCHASE_INVOICE_head.Posting_Date as [Posting Date] ,TSPL_BULK_MILK_PURCHASE_INVOICE_head.Created_By as [Created By] ,TSPL_BULK_MILK_PURCHASE_INVOICE_head.Created_Date as [Created Date] ,TSPL_BULK_MILK_PURCHASE_INVOICE_head.Modified_By as [Modified By] ,TSPL_BULK_MILK_PURCHASE_INVOICE_head.Modified_Date as [Modified Date] ,TSPL_BULK_MILK_PURCHASE_INVOICE_head.Comp_Code as [Comp Code],case when isnull(isSRNTradeInvoice,0)=0 then 'BULK MILK SRN' else 'BULK MILK SRN TRADE' end  as DocType,stuff((select ',' + isnull(SRN_NO ,'') from tspl_Bulk_milk_purchase_Invoice_Detail  where tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO  =TSPL_BULK_MILK_PURCHASE_INVOICE_head .DOC_NO   for xml path ('')),1,1,'' )as  [SRN No.] From TSPL_BULK_MILK_PURCHASE_INVOICE_head  left outer join tspl_vendor_master on TSPL_BULK_MILK_PURCHASE_INVOICE_head.vendor_code=tspl_vendor_master.vendor_code left outer join tspl_location_master on tspl_bulk_milk_purchase_invoice_head.loc_code=tspl_location_master.location_code "
            str = clsCommon.ShowSelectForm("PBMINVFND", qry, "DocNo", whrcls, curcode, "TSPL_BULK_MILK_PURCHASE_INVOICE_head.DOC_DATE desc", isButtonClicked)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return str
    End Function

    Public Shared Function saveData(ByVal obj As clsMilkPurchaseInvoiceHead, ByVal trans As SqlTransaction) As Boolean
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkMilkProcurement, clsUserMgtCode.frmBulkMilkPurchaseInvoice, obj.Loc_Code, obj.DOC_DATE, trans)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.frmMilkPurchaseInvoice, obj.Loc_Code, obj.DOC_DATE, trans)
            clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_PI_REMITTANCE where Document_No='" + obj.DOC_NO + "'", trans)

            clsERPFuncationality.IsDocumentAlreadyPosted("TSPL_BULK_MILK_PURCHASE_INVOICE_HEAD", "DOC_NO", obj.DOC_NO, "isPosted=1", trans)
            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "DOC_NO", clsCommon.myCstr(obj.DOC_NO))
            If clsCommon.myLen(obj.DOC_DATE) > 0 Then
                clsCommon.AddColumnsForChange(coll, "DOC_DATE", clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy hh:mm tt"))
            End If
            clsCommon.AddColumnsForChange(coll, "vendor_code", clsCommon.myCstr(obj.vendor_code))
            clsCommon.AddColumnsForChange(coll, "Loc_Code", clsCommon.myCstr(obj.Loc_Code))
            If clsCommon.myLen(obj.SRN_From_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "SRN_From_Date", clsCommon.GetPrintDate(obj.SRN_From_Date, "dd/MMM/yyyy hh:mm tt"))
            End If

            If clsCommon.myLen(obj.SRN_TO_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "SRN_TO_Date", clsCommon.GetPrintDate(obj.SRN_TO_Date, "dd/MMM/yyyy hh:mm tt"))
            End If
            'isSRNTradeInvoice
            clsCommon.AddColumnsForChange(coll, "IsAgainstJobWork", obj.IsAgainstJobWork)
            clsCommon.AddColumnsForChange(coll, "Joblocation_Code", obj.Joblocation_Code)
            clsCommon.AddColumnsForChange(coll, "isSRNTradeInvoice", clsCommon.myCdbl(obj.isSRNTradeInvoice))
            clsCommon.AddColumnsForChange(coll, "Total_FAT_KG", clsCommon.myCdbl(obj.Total_FAT_KG))
            clsCommon.AddColumnsForChange(coll, "Total_SNF_KG", clsCommon.myCdbl(obj.Total_SNF_KG))
            clsCommon.AddColumnsForChange(coll, "Total_QTY", clsCommon.myCdbl(obj.Total_QTY))
            clsCommon.AddColumnsForChange(coll, "Total_AMT", clsCommon.myCdbl(obj.Total_AMT))
            clsCommon.AddColumnsForChange(coll, "RoundOffAmount", clsCommon.myCdbl(obj.RoundOffAmount))
            clsCommon.AddColumnsForChange(coll, "Vendor_Invoice_No", clsCommon.myCstr(obj.Vendor_Invoice_No))
            clsCommon.AddColumnsForChange(coll, "isPosted", clsCommon.myCstr(obj.isPosted))
            If obj.isPosted = 1 Then
                clsCommon.AddColumnsForChange(coll, "Posting_Date", clsCommon.GetPrintDate(obj.Posting_Date, "dd/MMM/yyyy "))
            End If
            clsCommon.AddColumnsForChange(coll, "Modified_By", clsCommon.myCstr(obj.Modified_By))
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.myCstr(obj.Modified_Date))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", clsCommon.myCstr(obj.Comp_Code))


            clsCommon.AddColumnsForChange(coll, "Tax_Group", obj.Tax_Group)
            clsCommon.AddColumnsForChange(coll, "Tax_Calculation_Type", IIf(obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic, 0, 1))
            clsCommon.AddColumnsForChange(coll, "TAX1", obj.TAX1)
            clsCommon.AddColumnsForChange(coll, "TAX1_Rate", obj.TAX1_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX1_Base_Amt", obj.TAX1_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX1_Amt", obj.TAX1_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX2", obj.TAX2)
            clsCommon.AddColumnsForChange(coll, "TAX2_Rate", obj.TAX2_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX2_Base_Amt", obj.TAX2_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX2_Amt", obj.TAX2_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX3", obj.TAX3)
            clsCommon.AddColumnsForChange(coll, "TAX3_Rate", obj.TAX3_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX3_Base_Amt", obj.TAX3_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX3_Amt", obj.TAX3_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX4", obj.TAX4)
            clsCommon.AddColumnsForChange(coll, "TAX4_Rate", obj.TAX4_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX4_Base_Amt", obj.TAX4_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX4_Amt", obj.TAX4_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX5", obj.TAX5)
            clsCommon.AddColumnsForChange(coll, "TAX5_Rate", obj.TAX5_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX5_Base_Amt", obj.TAX5_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX5_Amt", obj.TAX5_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX6", obj.TAX6)
            clsCommon.AddColumnsForChange(coll, "TAX6_Rate", obj.TAX6_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX6_Base_Amt", obj.TAX6_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX6_Amt", obj.TAX6_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX7", obj.TAX7)
            clsCommon.AddColumnsForChange(coll, "TAX7_Rate", obj.TAX7_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX7_Base_Amt", obj.TAX7_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX7_Amt", obj.TAX7_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX8", obj.TAX8)
            clsCommon.AddColumnsForChange(coll, "TAX8_Rate", obj.TAX8_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX8_Base_Amt", obj.TAX8_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX8_Amt", obj.TAX8_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX9", obj.TAX9)
            clsCommon.AddColumnsForChange(coll, "TAX9_Rate", obj.TAX9_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX9_Base_Amt", obj.TAX9_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX9_Amt", obj.TAX9_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX10", obj.TAX10)
            clsCommon.AddColumnsForChange(coll, "TAX10_Rate", obj.TAX10_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX10_Base_Amt", obj.TAX10_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX10_Amt", obj.TAX10_Amt)
            clsCommon.AddColumnsForChange(coll, "Total_Taxable_Amount", obj.Total_Taxable_Amount)
            clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", obj.Total_Tax_Amt)

            clsCommon.AddColumnsForChange(coll, "ActualTCSBaseAmount", obj.ActualTCSBaseAmount)
            clsCommon.AddColumnsForChange(coll, "ChangedTCSBaseAmount", obj.ChangedTCSBaseAmount)


            If obj.isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", clsCommon.myCstr(obj.Created_By))
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(obj.Created_Date))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULK_MILK_PURCHASE_INVOICE_head", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULK_MILK_PURCHASE_INVOICE_head", OMInsertOrUpdate.Update, "TSPL_BULK_MILK_PURCHASE_INVOICE_head.DOC_No='" + obj.DOC_NO + "'", trans)
            End If
            clsMilkPurchaseInvoiceDetail.saveData(obj.arrDetail, obj.DOC_NO, trans)
            clsPIRemittance.SaveData(obj.objPIRemittance, obj.DOC_NO, obj.DOC_DATE, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function



    Public Shared Function getData(ByVal strCode As String, ByVal navtype As NavigatorType, Optional trans As SqlTransaction = Nothing) As clsMilkPurchaseInvoiceHead
        Dim obj As New clsMilkPurchaseInvoiceHead
        Try
            Dim whrCls As String = String.Empty
            Dim qst As String = " select *   From TSPL_BULK_MILK_PURCHASE_INVOICE_head   where 1=1  and comp_code='" & objCommonVar.CurrentCompanyCode & "' "
            If Not clsMccMaster.isCurrentUserHO(trans) Then
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    whrCls = " and TSPL_BULK_MILK_PURCHASE_INVOICE_head.Loc_Code in (" & objCommonVar.strCurrUserLocations & ")"
                End If
            End If
            qst = qst & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and TSPL_BULK_MILK_PURCHASE_INVOICE_head.doc_No in ('" + strCode + "') "
                Case NavigatorType.Next
                    qst += " and TSPL_BULK_MILK_PURCHASE_INVOICE_head.doc_No in (select min(doc_No ) from TSPL_BULK_MILK_PURCHASE_INVOICE_head where doc_No  >'" + strCode + "'   " & whrCls & ")"
                Case NavigatorType.First
                    qst += " and TSPL_BULK_MILK_PURCHASE_INVOICE_head.doc_No in (select MIN(doc_No ) from TSPL_BULK_MILK_PURCHASE_INVOICE_head where 1=1  " & whrCls & ")"
                Case NavigatorType.Last
                    qst += " and TSPL_BULK_MILK_PURCHASE_INVOICE_head.doc_No in (select Max(doc_No ) from TSPL_BULK_MILK_PURCHASE_INVOICE_head where 1=1  " & whrCls & ")"
                Case NavigatorType.Previous
                    qst += " and TSPL_BULK_MILK_PURCHASE_INVOICE_head.doc_No in (select Max(doc_No ) from TSPL_BULK_MILK_PURCHASE_INVOICE_head where doc_No  <'" + strCode + "'   " & whrCls & ")"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.IsAgainstJobWork = clsCommon.myCdbl(dt.Rows(0)("IsAgainstJobWork"))
                obj.Joblocation_Code = clsCommon.myCstr(dt.Rows(0)("Joblocation_Code"))
                obj.Purchase_Tax_Invoice = clsCommon.myCstr(dt.Rows(0)("Purchase_Tax_Invoice"))
                obj.DOC_NO = clsCommon.myCstr(dt.Rows(0)("DOC_NO"))
                obj.DOC_DATE = clsCommon.myCDate(dt.Rows(0)("DOC_DATE"))
                obj.vendor_code = clsCommon.myCstr(dt.Rows(0)("vendor_code"))
                obj.Loc_Code = clsCommon.myCstr(dt.Rows(0)("Loc_Code"))
                obj.Vendor_Invoice_No = clsCommon.myCstr(dt.Rows(0)("Vendor_Invoice_No"))
                obj.SRN_From_Date = clsCommon.myCDate(dt.Rows(0)("SRN_From_Date"))
                obj.SRN_TO_Date = clsCommon.myCDate(dt.Rows(0)("SRN_TO_Date"))
                obj.Total_FAT_KG = clsCommon.myCdbl(dt.Rows(0)("Total_FAT_KG"))
                obj.Total_SNF_KG = clsCommon.myCdbl(dt.Rows(0)("Total_SNF_KG"))
                obj.Total_QTY = clsCommon.myCdbl(dt.Rows(0)("Total_QTY"))
                obj.Total_AMT = clsCommon.myCdbl(dt.Rows(0)("Total_AMT"))
                obj.RoundOffAmount = clsCommon.myCdbl(dt.Rows(0)("RoundOffAmount"))
                obj.isSRNTradeInvoice = clsCommon.myCdbl(dt.Rows(0)("isSRNTradeInvoice"))
                obj.isPosted = clsCommon.myCstr(dt.Rows(0)("isPosted"))
                If obj.isPosted = 1 Then
                    obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
                End If

                obj.Tax_Group = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
                obj.Tax_Calculation_Type = IIf(clsCommon.myCdbl(dt.Rows(0)("Tax_Calculation_Type")) = 0, EnumTaxCalucationType.Automatic, EnumTaxCalucationType.Mannual)
                obj.TAX1 = clsCommon.myCstr(dt.Rows(0)("TAX1"))
                obj.TAX1_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX1_Rate"))
                obj.TAX1_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX1_Base_Amt"))
                obj.TAX1_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX1_Amt"))
                obj.TAX2 = clsCommon.myCstr(dt.Rows(0)("TAX2"))
                obj.TAX2_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX2_Rate"))
                obj.TAX2_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX2_Base_Amt"))
                obj.TAX2_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX2_Amt"))
                obj.TAX3 = clsCommon.myCstr(dt.Rows(0)("TAX3"))
                obj.TAX3_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX3_Base_Amt"))
                obj.TAX3_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX3_Rate"))
                obj.TAX3_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX3_Amt"))
                obj.TAX4 = clsCommon.myCstr(dt.Rows(0)("TAX4"))
                obj.TAX4_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX4_Rate"))
                obj.TAX4_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX4_Base_Amt"))
                obj.TAX4_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX4_Amt"))
                obj.TAX5 = clsCommon.myCstr(dt.Rows(0)("TAX5"))
                obj.TAX5_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX5_Rate"))
                obj.TAX5_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX5_Base_Amt"))
                obj.TAX5_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX5_Amt"))
                obj.TAX6 = clsCommon.myCstr(dt.Rows(0)("TAX6"))
                obj.TAX6_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX6_Rate"))
                obj.TAX6_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX6_Base_Amt"))
                obj.TAX6_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX6_Amt"))
                obj.TAX7 = clsCommon.myCstr(dt.Rows(0)("TAX7"))
                obj.TAX7_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX7_Rate"))
                obj.TAX7_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX7_Base_Amt"))
                obj.TAX7_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX7_Amt"))
                obj.TAX8 = clsCommon.myCstr(dt.Rows(0)("TAX8"))
                obj.TAX8_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX8_Rate"))
                obj.TAX8_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX8_Base_Amt"))
                obj.TAX8_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX8_Amt"))
                obj.TAX9 = clsCommon.myCstr(dt.Rows(0)("TAX9"))
                obj.TAX9_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX9_Rate"))
                obj.TAX9_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX9_Base_Amt"))
                obj.TAX9_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX9_Amt"))
                obj.TAX10 = clsCommon.myCstr(dt.Rows(0)("TAX10"))
                obj.TAX10_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX10_Rate"))
                obj.TAX10_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX10_Base_Amt"))
                obj.TAX10_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX10_Amt"))
                obj.Total_Tax_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Tax_Amt"))
                obj.Total_Taxable_Amount = clsCommon.myCdbl(dt.Rows(0)("Total_Taxable_Amount"))

                obj.ActualTCSBaseAmount = clsCommon.myCdbl(dt.Rows(0)("ActualTCSBaseAmount"))
                obj.ChangedTCSBaseAmount = clsCommon.myCdbl(dt.Rows(0)("ChangedTCSBaseAmount"))


                obj.arrDetail = clsMilkPurchaseInvoiceDetail.getData(obj.DOC_NO, trans)
                obj.objPIRemittance = clsPIRemittance.GetData(obj.DOC_NO, trans)
            Else
                obj = Nothing
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function

    Public Shared Function UpdateAfterPosting(ByVal obj As clsMilkPurchaseInvoiceHead, ByVal trans As SqlTransaction) As Boolean
        Try
            If obj IsNot Nothing And clsCommon.myLen(obj.DOC_NO) > 0 Then
                Dim coll As New Hashtable()

                clsCommon.AddColumnsForChange(coll, "EWayBillNo", obj.EWayBillNo)
                If clsCommon.myLen(obj.EWayBillDate) > 0 Then
                    clsCommon.AddColumnsForChange(coll, "EWayBillDate", clsCommon.GetPrintDate(obj.EWayBillDate, "dd/MMM/yyyy"))
                Else
                    clsCommon.AddColumnsForChange(coll, "EWayBillDate", Nothing, True)
                End If
                clsCommon.AddColumnsForChange(coll, "Electronic_Ref_No", obj.Electronic_Ref_No)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULK_MILK_PURCHASE_INVOICE_HEAD", OMInsertOrUpdate.Update, "TSPL_BULK_MILK_PURCHASE_INVOICE_HEAD.DOC_NO='" + obj.DOC_NO + "'", trans)


            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class



Public Class clsMilkPurchaseInvoiceDetail
    Public DOC_NO As String = Nothing
    Public SL_NO As Integer
    Public SRN_NO As String = Nothing
    Public SRN_Date As Date = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public UOM As String = Nothing
    Public Gross_Weight As Double = 0
    Public Tare_Weight As Double = 0
    Public Net_Weight As Double = 0
    Public Invoice_Qty As Double = 0
    Public snf_Per As Double = 0
    Public fat_per As Double = 0
    Public fat_KG As Double = 0
    Public SNF_KG As Double = 0
    Public fat_Rate As Double = 0
    Public SNF_Rate As Double = 0
    Public Amount As Double = 0
    Public Deduction As Double = 0
    Public Incentive As Double = 0
    Public Special_Deduction As Double = 0
    Public Actual_Amount As Double = 0
    Public price_code As String = String.Empty
    Public NetRate As Double = 0
    Public Xtra_Rate As Decimal = 0
    Public CHAMBER_DESC As String = Nothing

    Public TAX1 As String = Nothing
    Public TAX1_Base_Amt As Double = 0
    Public TAX1_Rate As Double = 0
    Public TAX1_Amt As Double = 0
    Public TAX2 As String = Nothing
    Public TAX2_Base_Amt As Double = 0
    Public TAX2_Rate As Double = 0
    Public TAX2_Amt As Double = 0
    Public TAX3 As String = Nothing
    Public TAX3_Base_Amt As Double = 0
    Public TAX3_Rate As Double = 0
    Public TAX3_Amt As Double = 0
    Public TAX4 As String = Nothing
    Public TAX4_Base_Amt As Double = 0
    Public TAX4_Rate As Double = 0
    Public TAX4_Amt As Double = 0
    Public TAX5 As String = Nothing
    Public TAX5_Base_Amt As Double = 0
    Public TAX5_Rate As Double = 0
    Public TAX5_Amt As Double = 0
    Public TAX6 As String = Nothing
    Public TAX6_Base_Amt As Double = 0
    Public TAX6_Rate As Double = 0
    Public TAX6_Amt As Double = 0
    Public TAX7 As String = Nothing
    Public TAX7_Base_Amt As Double = 0
    Public TAX7_Rate As Double = 0
    Public TAX7_Amt As Double = 0
    Public TAX8 As String = Nothing
    Public TAX8_Base_Amt As Double = 0
    Public TAX8_Rate As Double = 0
    Public TAX8_Amt As Double = 0
    Public TAX9 As String = Nothing
    Public TAX9_Base_Amt As Double = 0
    Public TAX9_Rate As Double = 0
    Public TAX9_Amt As Double = 0
    Public TAX10 As String = Nothing
    Public TAX10_Base_Amt As Double = 0
    Public TAX10_Rate As Double = 0
    Public TAX10_Amt As Double = 0
    Public Total_Tax_Amt As Double = 0
    Public Transport_Charges As Decimal = 0
    Public Item_Net_Amt As Double = 0

    Public Shared Function saveData(ByVal arrObj As List(Of clsMilkPurchaseInvoiceDetail), ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim issaved As Boolean = True
        Try
            Dim coll As Hashtable
            If arrObj IsNot Nothing Then
                Dim qry As String = "delete from TSPL_BULK_MILK_PURCHASE_INVOICE_detail where doc_No='" & strDocNo & "'"
                issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                For Each obj As clsMilkPurchaseInvoiceDetail In arrObj
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "DOC_NO", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "CHAMBER_DESC", obj.CHAMBER_DESC)
                    clsCommon.AddColumnsForChange(coll, "SL_NO", obj.SL_NO)
                    clsCommon.AddColumnsForChange(coll, "SRN_NO", obj.SRN_NO)
                    clsCommon.AddColumnsForChange(coll, "SRN_Date", clsCommon.GetPrintDate(obj.SRN_Date, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc)
                    clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM)
                    clsCommon.AddColumnsForChange(coll, "Gross_Weight", obj.Gross_Weight)
                    clsCommon.AddColumnsForChange(coll, "Tare_Weight", obj.Tare_Weight)
                    clsCommon.AddColumnsForChange(coll, "Net_Weight", obj.Net_Weight)
                    clsCommon.AddColumnsForChange(coll, "Invoice_Qty", obj.Invoice_Qty)
                    clsCommon.AddColumnsForChange(coll, "fat_per", obj.fat_per)
                    clsCommon.AddColumnsForChange(coll, "fat_KG", obj.fat_KG)
                    clsCommon.AddColumnsForChange(coll, "fat_Rate", obj.fat_Rate)
                    clsCommon.AddColumnsForChange(coll, "snf_Per", obj.snf_Per)
                    clsCommon.AddColumnsForChange(coll, "SNF_KG", obj.SNF_KG)
                    clsCommon.AddColumnsForChange(coll, "SNF_Rate", obj.SNF_Rate)
                    clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                    clsCommon.AddColumnsForChange(coll, "Deduction", obj.Deduction)
                    clsCommon.AddColumnsForChange(coll, "Incentive", obj.Incentive)
                    clsCommon.AddColumnsForChange(coll, "Special_Deduction", obj.Special_Deduction)
                    clsCommon.AddColumnsForChange(coll, "Actual_Amount", obj.Actual_Amount)
                    clsCommon.AddColumnsForChange(coll, "Price_code", obj.price_code)
                    clsCommon.AddColumnsForChange(coll, "NetRate", obj.NetRate)
                    clsCommon.AddColumnsForChange(coll, "Xtra_Rate", obj.Xtra_Rate)

                    clsCommon.AddColumnsForChange(coll, "TAX1", obj.TAX1)
                    clsCommon.AddColumnsForChange(coll, "TAX1_Base_Amt", obj.TAX1_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX1_Rate", obj.TAX1_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX1_Amt", obj.TAX1_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX2", obj.TAX2)
                    clsCommon.AddColumnsForChange(coll, "TAX2_Base_Amt", obj.TAX2_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX2_Rate", obj.TAX2_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX2_Amt", obj.TAX2_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX3", obj.TAX3)
                    clsCommon.AddColumnsForChange(coll, "TAX3_Base_Amt", obj.TAX3_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX3_Rate", obj.TAX3_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX3_Amt", obj.TAX3_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX4", obj.TAX4)
                    clsCommon.AddColumnsForChange(coll, "TAX4_Base_Amt", obj.TAX4_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX4_Rate", obj.TAX4_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX4_Amt", obj.TAX4_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX5", obj.TAX5)
                    clsCommon.AddColumnsForChange(coll, "TAX5_Base_Amt", obj.TAX5_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX5_Rate", obj.TAX5_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX5_Amt", obj.TAX5_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX6", obj.TAX6)
                    clsCommon.AddColumnsForChange(coll, "TAX6_Base_Amt", obj.TAX6_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX6_Rate", obj.TAX6_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX6_Amt", obj.TAX6_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX7", obj.TAX7)
                    clsCommon.AddColumnsForChange(coll, "TAX7_Base_Amt", obj.TAX7_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX7_Rate", obj.TAX7_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX7_Amt", obj.TAX7_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX8", obj.TAX8)
                    clsCommon.AddColumnsForChange(coll, "TAX8_Base_Amt", obj.TAX8_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX8_Rate", obj.TAX8_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX8_Amt", obj.TAX8_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX9", obj.TAX9)
                    clsCommon.AddColumnsForChange(coll, "TAX9_Base_Amt", obj.TAX9_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX9_Rate", obj.TAX9_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX9_Amt", obj.TAX9_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX10", obj.TAX10)
                    clsCommon.AddColumnsForChange(coll, "TAX10_Base_Amt", obj.TAX10_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX10_Rate", obj.TAX10_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX10_Amt", obj.TAX10_Amt)
                    clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", obj.Total_Tax_Amt)
                    clsCommon.AddColumnsForChange(coll, "Transport_Charges", obj.Transport_Charges)
                    clsCommon.AddColumnsForChange(coll, "Item_Net_Amt", obj.Item_Net_Amt)

                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULK_MILK_PURCHASE_INVOICE_detail", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return issaved
    End Function
    Public Shared Function getData(ByVal strDocNo As String, Optional trans As SqlTransaction = Nothing) As List(Of clsMilkPurchaseInvoiceDetail)
        Dim arrObj As List(Of clsMilkPurchaseInvoiceDetail) = Nothing
        Try
            Dim obj As clsMilkPurchaseInvoiceDetail = Nothing
            Dim qry As String = "select * from TSPL_BULK_MILK_PURCHASE_INVOICE_detail where doc_No='" & strDocNo & "' order by sl_no"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of clsMilkPurchaseInvoiceDetail)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New clsMilkPurchaseInvoiceDetail()
                    obj.DOC_NO = clsCommon.myCstr(dt.Rows(i)("DOC_NO"))
                    obj.SL_NO = clsCommon.myCstr(dt.Rows(i)("SL_NO"))
                    obj.SRN_NO = clsCommon.myCstr(dt.Rows(i)("SRN_NO"))
                    obj.SRN_Date = clsCommon.myCDate(dt.Rows(i)("SRN_Date"))
                    obj.CHAMBER_DESC = clsCommon.myCstr(dt.Rows(i)("CHAMBER_DESC"))
                    obj.Item_Code = clsCommon.myCstr(dt.Rows(i)("Item_Code"))
                    obj.Item_Desc = clsCommon.myCstr(dt.Rows(i)("Item_Desc"))
                    obj.UOM = clsCommon.myCstr(dt.Rows(i)("UOM"))
                    obj.Gross_Weight = clsCommon.myCdbl(dt.Rows(i)("Gross_Weight"))
                    obj.Tare_Weight = clsCommon.myCdbl(dt.Rows(i)("Tare_Weight"))
                    obj.Net_Weight = clsCommon.myCdbl(dt.Rows(i)("Net_Weight"))
                    obj.Invoice_Qty = clsCommon.myCdbl(dt.Rows(i)("Invoice_Qty"))
                    obj.fat_per = clsCommon.myCdbl(dt.Rows(i)("fat_per"))
                    obj.fat_KG = clsCommon.myCdbl(dt.Rows(i)("fat_KG"))
                    obj.fat_Rate = clsCommon.myCdbl(dt.Rows(i)("fat_Rate"))
                    obj.snf_Per = clsCommon.myCdbl(dt.Rows(i)("snf_Per"))
                    obj.SNF_KG = clsCommon.myCdbl(dt.Rows(i)("SNF_KG"))
                    obj.SNF_Rate = clsCommon.myCdbl(dt.Rows(i)("SNF_Rate"))
                    obj.Amount = clsCommon.myCdbl(dt.Rows(i)("Amount"))
                    obj.Deduction = clsCommon.myCdbl(dt.Rows(i)("Deduction"))
                    obj.Incentive = clsCommon.myCdbl(dt.Rows(i)("Incentive"))
                    obj.Special_Deduction = clsCommon.myCdbl(dt.Rows(i)("Special_Deduction"))
                    obj.Actual_Amount = clsCommon.myCdbl(dt.Rows(i)("Actual_Amount"))
                    obj.NetRate = clsCommon.myCdbl(dt.Rows(i)("NetRate"))
                    obj.Xtra_Rate = clsCommon.myCdbl(dt.Rows(i)("Xtra_Rate"))
                    obj.price_code = clsCommon.myCstr(dt.Rows(i)("Price_code"))
                    obj.CHAMBER_DESC = clsCommon.myCstr(dt.Rows(i)("CHAMBER_DESC"))


                    obj.TAX1 = clsCommon.myCstr(dt.Rows(i)("TAX1"))
                    obj.TAX1_Base_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX1_Base_Amt"))
                    obj.TAX1_Rate = clsCommon.myCdbl(dt.Rows(i)("TAX1_Rate"))
                    obj.TAX1_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX1_Amt"))
                    obj.TAX2 = clsCommon.myCstr(dt.Rows(i)("TAX2"))
                    obj.TAX2_Base_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX2_Base_Amt"))
                    obj.TAX2_Rate = clsCommon.myCdbl(dt.Rows(i)("TAX2_Rate"))
                    obj.TAX2_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX2_Amt"))
                    obj.TAX3 = clsCommon.myCstr(dt.Rows(i)("TAX3"))
                    obj.TAX3_Base_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX3_Base_Amt"))
                    obj.TAX3_Rate = clsCommon.myCdbl(dt.Rows(i)("TAX3_Rate"))
                    obj.TAX3_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX3_Amt"))
                    obj.TAX4 = clsCommon.myCstr(dt.Rows(i)("TAX4"))
                    obj.TAX4_Base_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX4_Base_Amt"))
                    obj.TAX4_Rate = clsCommon.myCdbl(dt.Rows(i)("TAX4_Rate"))
                    obj.TAX4_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX4_Amt"))
                    obj.TAX5 = clsCommon.myCstr(dt.Rows(i)("TAX5"))
                    obj.TAX5_Base_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX5_Base_Amt"))
                    obj.TAX5_Rate = clsCommon.myCdbl(dt.Rows(i)("TAX5_Rate"))
                    obj.TAX5_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX5_Amt"))
                    obj.TAX6 = clsCommon.myCstr(dt.Rows(i)("TAX6"))
                    obj.TAX6_Base_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX6_Base_Amt"))
                    obj.TAX6_Rate = clsCommon.myCdbl(dt.Rows(i)("TAX6_Rate"))
                    obj.TAX6_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX6_Amt"))
                    obj.TAX7 = clsCommon.myCstr(dt.Rows(i)("TAX7"))
                    obj.TAX7_Base_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX7_Base_Amt"))
                    obj.TAX7_Rate = clsCommon.myCdbl(dt.Rows(i)("TAX7_Rate"))
                    obj.TAX7_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX7_Amt"))
                    obj.TAX8 = clsCommon.myCstr(dt.Rows(i)("TAX8"))
                    obj.TAX8_Base_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX8_Base_Amt"))
                    obj.TAX8_Rate = clsCommon.myCdbl(dt.Rows(i)("TAX8_Rate"))
                    obj.TAX8_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX8_Amt"))
                    obj.TAX9 = clsCommon.myCstr(dt.Rows(i)("TAX9"))
                    obj.TAX9_Base_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX9_Base_Amt"))
                    obj.TAX9_Rate = clsCommon.myCdbl(dt.Rows(i)("TAX9_Rate"))
                    obj.TAX9_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX9_Amt"))
                    obj.TAX10 = clsCommon.myCstr(dt.Rows(i)("TAX10"))
                    obj.TAX10_Base_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX10_Base_Amt"))
                    obj.TAX10_Rate = clsCommon.myCdbl(dt.Rows(i)("TAX10_Rate"))
                    obj.TAX10_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX10_Amt"))
                    obj.Total_Tax_Amt = clsCommon.myCdbl(dt.Rows(i)("Total_Tax_Amt"))
                    obj.Transport_Charges = clsCommon.myCdbl(dt.Rows(i)("Transport_Charges"))
                    obj.Item_Net_Amt = clsCommon.myCdbl(dt.Rows(i)("Item_Net_Amt"))

                    arrObj.Add(obj)
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return arrObj
    End Function
    Public Shared Function deleteData(ByVal strDocNo As String) As Boolean
        Dim isDeleted As Boolean = True
        Try
            Dim qry As String = "delete from TSPL_BULK_MILK_PURCHASE_INVOICE_detail where doc_No='" & strDocNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isDeleted
    End Function

End Class
