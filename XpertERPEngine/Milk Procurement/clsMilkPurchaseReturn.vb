Imports common
Imports System.Data.SqlClient

' Create By Prabhakar . Ticket Ref : BM00000007375
Public Class clsMilkPurchaseReturnHead
    Public IsAgainstJobWork As Integer = 0
    Public Joblocation_Code As String = Nothing
    Public isNewEntry As Boolean = False
    Public Pur_Return_No As String = Nothing
    Public Pur_Return_Date As Date = Nothing
    Public Invoice_No As String = Nothing
    Public Loc_Code As String = Nothing
    Public Invoice_Date As Date = Nothing
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
    Public arrDetail As List(Of clsMilkPurchaseReturnDetail) = Nothing

    Public RoundOffAmount As Double = 0
    Public isSRNTradeInvoice As Double = 0

    Public Shared Function postData(ByVal strDocNo As String, ByVal formId As String, Optional trans As SqlTransaction = Nothing) As Boolean

        Dim isTransLocallyInitiatted As Boolean = False
        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Purchase Return No not found to Post")
            End If

            If trans Is Nothing Then
                trans = clsDBFuncationality.GetTransactin()
                isTransLocallyInitiatted = True
            End If

            Dim obj As clsMilkPurchaseReturnHead = clsMilkPurchaseReturnHead.getData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Pur_Return_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.FrmMilkPurchaseReturn, obj.Loc_Code, obj.Pur_Return_Date, trans)

            If (obj.isPosted = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If


            Dim vendor_name As String = clsVendorMaster.GetName(obj.vendor_code, trans)
            Dim qry As String = ""

            '======================================================== Bulk Milk Purchase Return Entry =================================
            'Dim dtVendorHead As DataTable = clsDBFuncationality.GetDataTable("SELECT  Vendor_Code ,Vendor_Name,Vendor_Invoice_No,Vendor_Invoice_Date,Document_No,Invoice_Entry_Date,Posting_Date,Account_Set,Document_Type,PO_Number,Order_No ,Document_Total,On_Hold,Remarks,Description,Tax_Group,TAX1,TAX1_Rate,TAX1_Amt,TAX2,TAX2_Rate,TAX2_Amt,TAX3,TAX3_Rate,TAX3_Amt,TAX4,TAX4_Rate,TAX4_Amt,TAX5,TAX5_Rate,TAX5_Amt,TAX6,TAX6_Rate,TAX6_Amt,TAX7,TAX7_Rate,TAX7_Amt,TAX8,TAX8_Rate,TAX8_Amt,TAX9,TAX9_Rate,TAX9_Amt,TAX10,TAX10_Rate,TAX10_Amt,Total_Tax,Tax1_BAmount,Tax2_BAmount,Tax3_BAmount,Tax4_BAmount,Tax5_BAmount,Tax6_BAmount,Tax7_BAmount,Tax8_BAmount,Tax9_BAmount,Tax10_BAmount,Balance_Amt,Terms_Code,Terms_Description,Due_Date,Discount_Percentage,Discount_Base,Discount_Amount,Amount_Less_Discount,TDS_Base_Actual_Amount,TDS_Base_Calculated_Amount,TDS_Percentage,TDS_Actual_Amount,TDS_Calculated_Amount,Nature_of_deduction,Branch_Code,Section_Code,Created_By,Created_Date,Modify_By,Modify_Date,Comp_Code,Vendor_Control_AC,TAX1_GLAC,TAX2_GLAC,TAX3_GLAC,TAX4_GLAC,TAX5_GLAC,TAX6_GLAC,TAX7_GLAC,TAX8_GLAC,TAX9_GLAC,TAX10_GLAC,Discount_GL_AC,RemitanceBranch_GL_AC,RefDocType,RefDocNo,Against_POInvoice_No,Against_PurchaseReturn_No,Add_Charge_Code1,Add_Charge_Name1,Add_Charge_Amt1,Add_Charge_Code2,Add_Charge_Name2,Add_Charge_Amt2,Add_Charge_Code3,Add_Charge_Name3,Add_Charge_Amt3,Add_Charge_Code4,Add_Charge_Name4,Add_Charge_Amt4,Add_Charge_Code5,Add_Charge_Name5,Add_Charge_Amt5,Add_Charge_Code6,Add_Charge_Name6,Add_Charge_Amt6,Add_Charge_Code7,Add_Charge_Name7,Add_Charge_Amt7,Add_Charge_Code8,Add_Charge_Name8,Add_Charge_Amt8,Add_Charge_Code9,Add_Charge_Name9,Add_Charge_Amt9,Add_Charge_Code10,Add_Charge_Name10,Add_Charge_Amt10,Total_Add_Charge,Tax_Calculation_Type,FIFO_Balance,FIFO_KnockOff,TAX1_GLAC_Amt,TAX1_GLAC2,TAX1_GLAC2_Amt,TAX1_GLAC3,TAX1_GLAC3_Amt,TAX1_GLAC4,TAX1_GLAC4_Amt,TAX1_GLAC5,TAX1_GLAC5_Amt,TAX2_GLAC_Amt,TAX2_GLAC2,TAX2_GLAC2_Amt,TAX2_GLAC3,TAX2_GLAC3_Amt,TAX2_GLAC4,TAX2_GLAC4_Amt,TAX2_GLAC5,TAX2_GLAC5_Amt,TAX3_GLAC_Amt,TAX3_GLAC2,TAX3_GLAC2_Amt,TAX3_GLAC3,TAX3_GLAC3_Amt,TAX3_GLAC4,TAX3_GLAC4_Amt,TAX3_GLAC5,TAX3_GLAC5_Amt,TAX4_GLAC_Amt,TAX4_GLAC2,TAX4_GLAC2_Amt,TAX4_GLAC3,TAX4_GLAC3_Amt,TAX4_GLAC4,TAX4_GLAC4_Amt,TAX4_GLAC5,TAX4_GLAC5_Amt,TAX5_GLAC_Amt,TAX5_GLAC2,TAX5_GLAC2_Amt,TAX5_GLAC3,TAX5_GLAC3_Amt,TAX5_GLAC4,TAX5_GLAC4_Amt,TAX5_GLAC5,TAX5_GLAC5_Amt,TAX6_GLAC_Amt,TAX6_GLAC2,TAX6_GLAC2_Amt,TAX6_GLAC3,TAX6_GLAC3_Amt,TAX6_GLAC4,TAX6_GLAC4_Amt,TAX6_GLAC5,TAX6_GLAC5_Amt,TAX7_GLAC_Amt,TAX7_GLAC2,TAX7_GLAC2_Amt,TAX7_GLAC3,TAX7_GLAC3_Amt,TAX7_GLAC4,TAX7_GLAC4_Amt,TAX7_GLAC5,TAX7_GLAC5_Amt,TAX8_GLAC_Amt,TAX8_GLAC2,TAX8_GLAC2_Amt,TAX8_GLAC3,TAX8_GLAC3_Amt,TAX8_GLAC4,TAX8_GLAC4_Amt,TAX8_GLAC5,TAX8_GLAC5_Amt,TAX9_GLAC_Amt,TAX9_GLAC2,TAX9_GLAC2_Amt,TAX9_GLAC3,TAX9_GLAC3_Amt,TAX9_GLAC4,TAX9_GLAC4_Amt,TAX9_GLAC5,TAX9_GLAC5_Amt,TAX10_GLAC_Amt,TAX10_GLAC2,TAX10_GLAC2_Amt,TAX10_GLAC3,TAX10_GLAC3_Amt,TAX10_GLAC4,TAX10_GLAC4_Amt,TAX10_GLAC5,TAX10_GLAC5_Amt,Loc_Code,Empty_Amount,Empty_Account,Total_Landed_Amt,is_For_TDS,Against_Acquisition,CURRENCY_CODE,ConvRate,ApplicableFrom,PROJECT_ID,Approval_Level,Level1_User,Level2_User,Level3_User,Invoice_Type,entry_Type,Against_MillkPurchaseInvoice_No,Against_BulkMillkPurchaseInvoice_No,RoundOffAmount,Against_VSPItemIssue_No,Is_Security,is_For_Provision,Prov_From_Date,Prov_To_Date,Prov_Amt,isDeduction,Deduction_Code,Deduction_Desc,Irregular_Loc_Code,is_proRated,Against_Asset_Work,Against_VCGL,Hirerachy_Level_Code,Cost_Centre_Fin_Level_Code,Cash_Discount_Amt,Amt_After_Cash_Discount,VI_Due_Date,Ref_SNo,Addition_Doc_Type FROM TSPL_VENDOR_INVOICE_HEAD where Against_BulkMillkPurchaseInvoice_No ='" + obj.Invoice_No + "'", trans)
            'If dtVendorHead.Rows.Count > 0 Then
            '    qry = "Insert into TSPL_VENDOR_INVOICE_HEAD (Vendor_Code ,Vendor_Name,Vendor_Invoice_No,Vendor_Invoice_Date,Document_No,Invoice_Entry_Date,Posting_Date,Account_Set,Document_Type,PO_Number,Order_No ,Document_Total,On_Hold,Remarks,Description,Tax_Group,TAX1,TAX1_Rate,TAX1_Amt,TAX2,TAX2_Rate,TAX2_Amt,TAX3,TAX3_Rate,TAX3_Amt,TAX4,TAX4_Rate,TAX4_Amt,TAX5,TAX5_Rate,TAX5_Amt,TAX6,TAX6_Rate,TAX6_Amt,TAX7,TAX7_Rate,TAX7_Amt,TAX8,TAX8_Rate,TAX8_Amt,TAX9,TAX9_Rate,TAX9_Amt,TAX10,TAX10_Rate,TAX10_Amt,Total_Tax,Tax1_BAmount,Tax2_BAmount,Tax3_BAmount,Tax4_BAmount,Tax5_BAmount,Tax6_BAmount,Tax7_BAmount,Tax8_BAmount,Tax9_BAmount,Tax10_BAmount,Balance_Amt,Terms_Code,Terms_Description,Due_Date,Discount_Percentage,Discount_Base,Discount_Amount,Amount_Less_Discount,TDS_Base_Actual_Amount,TDS_Base_Calculated_Amount,TDS_Percentage,TDS_Actual_Amount,TDS_Calculated_Amount,Nature_of_deduction,Branch_Code,Section_Code,Created_By,Created_Date,Modify_By,Modify_Date,Comp_Code,Vendor_Control_AC,TAX1_GLAC,TAX2_GLAC,TAX3_GLAC,TAX4_GLAC,TAX5_GLAC,TAX6_GLAC,TAX7_GLAC,TAX8_GLAC,TAX9_GLAC,TAX10_GLAC,Discount_GL_AC,RemitanceBranch_GL_AC,RefDocType,RefDocNo,Against_POInvoice_No,Against_PurchaseReturn_No,Add_Charge_Code1,Add_Charge_Name1,Add_Charge_Amt1,Add_Charge_Code2,Add_Charge_Name2,Add_Charge_Amt2,Add_Charge_Code3,Add_Charge_Name3,Add_Charge_Amt3,Add_Charge_Code4,Add_Charge_Name4,Add_Charge_Amt4,Add_Charge_Code5,Add_Charge_Name5,Add_Charge_Amt5,Add_Charge_Code6,Add_Charge_Name6,Add_Charge_Amt6,Add_Charge_Code7,Add_Charge_Name7,Add_Charge_Amt7,Add_Charge_Code8,Add_Charge_Name8,Add_Charge_Amt8,Add_Charge_Code9,Add_Charge_Name9,Add_Charge_Amt9,Add_Charge_Code10,Add_Charge_Name10,Add_Charge_Amt10,Total_Add_Charge,Tax_Calculation_Type,FIFO_Balance,FIFO_KnockOff,TAX1_GLAC_Amt,TAX1_GLAC2,TAX1_GLAC2_Amt,TAX1_GLAC3,TAX1_GLAC3_Amt,TAX1_GLAC4,TAX1_GLAC4_Amt,TAX1_GLAC5,TAX1_GLAC5_Amt,TAX2_GLAC_Amt,TAX2_GLAC2,TAX2_GLAC2_Amt,TAX2_GLAC3,TAX2_GLAC3_Amt,TAX2_GLAC4,TAX2_GLAC4_Amt,TAX2_GLAC5,TAX2_GLAC5_Amt,TAX3_GLAC_Amt,TAX3_GLAC2,TAX3_GLAC2_Amt,TAX3_GLAC3,TAX3_GLAC3_Amt,TAX3_GLAC4,TAX3_GLAC4_Amt,TAX3_GLAC5,TAX3_GLAC5_Amt,TAX4_GLAC_Amt,TAX4_GLAC2,TAX4_GLAC2_Amt,TAX4_GLAC3,TAX4_GLAC3_Amt,TAX4_GLAC4,TAX4_GLAC4_Amt,TAX4_GLAC5,TAX4_GLAC5_Amt,TAX5_GLAC_Amt,TAX5_GLAC2,TAX5_GLAC2_Amt,TAX5_GLAC3,TAX5_GLAC3_Amt,TAX5_GLAC4,TAX5_GLAC4_Amt,TAX5_GLAC5,TAX5_GLAC5_Amt,TAX6_GLAC_Amt,TAX6_GLAC2,TAX6_GLAC2_Amt,TAX6_GLAC3,TAX6_GLAC3_Amt,TAX6_GLAC4,TAX6_GLAC4_Amt,TAX6_GLAC5,TAX6_GLAC5_Amt,TAX7_GLAC_Amt,TAX7_GLAC2,TAX7_GLAC2_Amt,TAX7_GLAC3,TAX7_GLAC3_Amt,TAX7_GLAC4,TAX7_GLAC4_Amt,TAX7_GLAC5,TAX7_GLAC5_Amt,TAX8_GLAC_Amt,TAX8_GLAC2,TAX8_GLAC2_Amt,TAX8_GLAC3,TAX8_GLAC3_Amt,TAX8_GLAC4,TAX8_GLAC4_Amt,TAX8_GLAC5,TAX8_GLAC5_Amt,TAX9_GLAC_Amt,TAX9_GLAC2,TAX9_GLAC2_Amt,TAX9_GLAC3,TAX9_GLAC3_Amt,TAX9_GLAC4,TAX9_GLAC4_Amt,TAX9_GLAC5,TAX9_GLAC5_Amt,TAX10_GLAC_Amt,TAX10_GLAC2,TAX10_GLAC2_Amt,TAX10_GLAC3,TAX10_GLAC3_Amt,TAX10_GLAC4,TAX10_GLAC4_Amt,TAX10_GLAC5,TAX10_GLAC5_Amt,Loc_Code,Empty_Amount,Empty_Account,Total_Landed_Amt,is_For_TDS,Against_Acquisition,CURRENCY_CODE,ConvRate,ApplicableFrom,PROJECT_ID,Approval_Level,Level1_User,Level2_User,Level3_User,Invoice_Type,entry_Type,Against_MillkPurchaseInvoice_No,Against_BulkMillkPurchaseInvoice_No,RoundOffAmount,Against_VSPItemIssue_No,Is_Security,is_For_Provision,Prov_From_Date,Prov_To_Date,Prov_Amt,isDeduction,Deduction_Code,Deduction_Desc,Irregular_Loc_Code,is_proRated,Against_Asset_Work,Against_VCGL,Hirerachy_Level_Code,Cost_Centre_Fin_Level_Code,Cash_Discount_Amt,Amt_After_Cash_Discount,VI_Due_Date,Ref_SNo,Addition_Doc_Type) values ()"
            '    clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '    Dim VendorHeadDocumentNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_No from TSPL_VENDOR_INVOICE_HEAD where Against_BulkMillkPurchaseInvoice_No ='" + obj.Invoice_No + "' and Document_Type ='I' ", trans))
            '    Dim dtVendorDetails As DataTable = clsDBFuncationality.GetDataTable("SELECT  Document_No ,Detail_Line_No,GL_Account_Code,GL_Account_Desc,Amount,Discount_Per,Discount,Amount_less_Discount,TAX1,TAX1_Rate,TAX1_Amt,TAX2,TAX2_Rate,TAX2_Amt,TAX3,TAX3_Rate,TAX3_Amt,TAX4,TAX4_Rate,TAX4_Amt,TAX5,TAX5_Rate,TAX5_Amt,TAX6,TAX6_Rate,TAX6_Amt,TAX7,TAX7_Rate,TAX7_Amt,TAX8,TAX8_Rate,TAX8_Amt,TAX9,TAX9_Rate,TAX9_Amt,TAX10,TAX10_Rate,TAX10_Amt,Total_Tax,Total_Amount,Remarks,Comments,AddChargeCode,AddChargeDesc,is_Unclaimed_Tax,Invoice_No,Invoice_Type,Landed_Amount,Deduction_Code,item_code,item_desc,charge_cat_code,charge_cat_desc,charge_cat_charges,Item_Rate,Abatement_Per,Abatement_Amt,TAX1_Base_Amt,TAX2_Base_Amt,TAX3_Base_Amt,TAX4_Base_Amt,TAX5_Base_Amt,TAX6_Base_Amt,TAX7_Base_Amt,TAX8_Base_Amt,TAX9_Base_Amt,TAX10_Base_Amt,DeductionCode,Deduction_Desc,PK_Id,Reverse_Charge_Per,Reverse_Charge_Amount,AgainstPayment_No,Payment_Amount,TDS_Per,Hirerachy_Code,Cost_Centre_Code,Against_Milk_SRN_Commission_No,Asset_Code,Item_Type   FROM TSPL_VENDOR_INVOICE_DETAIL where Document_No='" + obj.Invoice_No + "'", trans)
            '    If dtVendorDetails.Rows.Count > 0 Then
            '        For Each dr As DataRow In dtVendorDetails.Rows
            '            qry = "Insert into TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL () values () "
            '            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '        Next
            '    End If
            'End If
            '===========================================================End=============================================================


            Dim objVendorInvHead As New clsVedorInvoiceHead()

            objVendorInvHead.Invoice_Entry_Date = clsCommon.myCDate(obj.Pur_Return_Date, "dd/MMM/yyyy")
            objVendorInvHead.Vendor_Code = obj.vendor_code
            objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(obj.vendor_code, trans)
            objVendorInvHead.Vendor_Invoice_No = obj.Vendor_Invoice_No
            objVendorInvHead.Invoice_Type = "AP"
            objVendorInvHead.Vendor_Invoice_Date = clsCommon.myCDate(obj.Pur_Return_Date, "dd/MMM/yyyy")

            objVendorInvHead.loc_code = clsLocation.GetSegmentCode(obj.Loc_Code, trans)

            objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + obj.vendor_code + "'", trans))
            If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                Throw New Exception("Please set the vendor Account Set For Vendor : " + vendor_name)
            End If

            objVendorInvHead.Document_Type = "D" ''For Purchase Return  Type


            objVendorInvHead.On_Hold = False
            Dim srndate As String = ""
            Dim srncode As String = ""
            Dim Vlc_Code As String = ""
            Dim Vlc_Name As String = ""
            For Each objTr As clsMilkPurchaseReturnDetail In obj.arrDetail
                If clsCommon.myLen(objTr.SRN_NO) > 0 Then
                    ' Dim query As String = "select doc_date,vd.VLC_Code,VLC_Name from TSPL_Milk_SRN_HEAD sh inner join TSPL_VLC_MASTER_HEAD vd on sh.VLC_CODE=vd.VLC_Code where DOc_Code ='" + objTr.SRN_CODE + "' "
                    'Dim Dt_SRN As DataTable = clsDBFuncationality.GetDataTable(query, trans)
                    srndate = IIf(srndate = "", clsCommon.myCDate(CStr(objTr.SRN_Date), "dd/MM/yyyy"), srndate & "," & clsCommon.myCDate(CStr(objTr.SRN_Date), "dd/MM/yyyy"))
                    srncode = IIf(srncode = "", objTr.SRN_NO, srncode & "," & objTr.SRN_NO)
                    'Vlc_Code = obj.vendor_code  'IIf(Vlc_Code = "", obj.vendor_code , Vlc_Code & "," & Dt_SRN.Rows(0).Item("VLC_Code").ToString)
                    'Vlc_Name = clsVendorMaster.GetName(obj.vendor_code, trans) 'IIf(Vlc_Name = "", Dt_SRN.Rows(0).Item("VLC_Name").ToString, Vlc_Name & "," & Dt_SRN.Rows(0).Item("VLC_name").ToString)
                End If
            Next



            objVendorInvHead.Description = "Vendor : " + obj.vendor_code + "/" + vendor_name + " .Return Against Bulk Milk Purchase Return No " + srncode + "-" + obj.Pur_Return_No + "-" + obj.Pur_Return_Date

            objVendorInvHead.Due_Date = obj.Pur_Return_Date
            objVendorInvHead.Discount_Base = 0 'obj.Discount_Base
            objVendorInvHead.Discount_Amount = 0 'obj.Discount_Amt
            objVendorInvHead.Amount_Less_Discount = obj.Total_AMT
            objVendorInvHead.Document_Total = obj.Total_AMT
            objVendorInvHead.Balance_Amt = obj.Total_AMT
            objVendorInvHead.RoundOffAmount = obj.RoundOffAmount
            'objVendorInvHead.Against_POInvoice_No = obj.DOC_CODE
            objVendorInvHead.RefDocNo = obj.Pur_Return_No
            objVendorInvHead.RefDocType = "BM-PR"
            'objVendorInvHead.Against_BulkMillkPurchaseInvoice_No = obj.Invoice_No

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
            For Each objPIDetail As clsMilkPurchaseReturnDetail In obj.arrDetail
                Dim strICode As String = objPIDetail.Item_Code

                ''Fill VendorInvoice details Data
                qry = "select TSPL_ITEM_MASTER.Purchase_Class_Code,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing,TSPL_GL_ACCOUNTS.Description as ClearingACName, TSPL_PURCHASE_ACCOUNTS.Non_Stock_Clearing as EmptyAccount from  TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing where TSPL_ITEM_MASTER.Item_Code='" + strICode + "'"
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    Throw New Exception("Please set Purchase Account set for item " + strICode + "(" + objPIDetail.Item_Desc + ")")
                End If
                Dim strPaybleCleanigCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Inv_Control_Account"))   ''BHA/21/08/18-000470 by balwinder on 21/08/2018
                strPaybleCleanigCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strPaybleCleanigCtrlAC, obj.Loc_Code, trans)
                Dim strPaybleCleanigCtrlACName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + strPaybleCleanigCtrlAC + "'", trans))


                If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                    objVendorInvHead.Empty_Account = clsCommon.myCstr(dt.Rows(0)("EmptyAccount"))
                    objVendorInvHead.Empty_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Empty_Account, obj.Loc_Code, trans)
                End If


                Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                ii = ii + 1
                objVendorInvDetail.Detail_Line_No = ii
                objVendorInvDetail.GL_Account_Code = strPaybleCleanigCtrlAC
                objVendorInvDetail.GL_Account_Desc = strPaybleCleanigCtrlACName
                objVendorInvDetail.Amount = objPIDetail.Actual_Amount   'objPIDetail.AMOUNT * obj.Commission / 100
                objVendorInvDetail.Discount_Per = 0
                objVendorInvDetail.Discount = 0
                objVendorInvDetail.Amount_less_Discount = objPIDetail.Actual_Amount   'objPIDetail.AMOUNT * obj.Commission / 100
                objVendorInvDetail.Total_Tax = 0
                objVendorInvDetail.Total_Amount = objPIDetail.Actual_Amount  'objPIDetail.AMOUNT * obj.Commission / 100
                objVendorInvDetail.Landed_Amount = objPIDetail.Actual_Amount   'objPIDetail.AMOUNT * obj.Commission / 100
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

            isSaved = isSaved AndAlso objVendorInvHead.SaveData(objVendorInvHead, True, trans)
            isSaved = isSaved AndAlso clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans, obj.Pur_Return_Date)

            'Sanjay Update I for Inventory Control Account in JV 
            Dim isApplyPurchaseAccounting As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0, False, True)
            If Not isApplyPurchaseAccounting Then
                Dim VOUCHER_NO As String = ""
                VOUCHER_NO = clsDBFuncationality.getSingleValue("select ISNULL(Voucher_No,'') from tspl_journal_master where Source_Doc_No='" + objVendorInvHead.Document_No + "' ", trans)
                For Each objPIDetail As clsMilkPurchaseReturnDetail In obj.arrDetail
                    Dim strICode As String = objPIDetail.Item_Code

                    qry = "select TSPL_ITEM_MASTER.Purchase_Class_Code,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing,TSPL_GL_ACCOUNTS.Description as ClearingACName, TSPL_PURCHASE_ACCOUNTS.Non_Stock_Clearing as EmptyAccount from  TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing where TSPL_ITEM_MASTER.Item_Code='" + strICode + "'"
                    dt = clsDBFuncationality.GetDataTable(qry, trans)
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        Throw New Exception("Please set Purchase Account set for item " + strICode + "(" + objPIDetail.Item_Desc + ")")
                    End If
                    Dim strInvCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Inv_Control_Account"))   ''BHA/21/08/18-000470 by balwinder on 21/08/2018
                    strInvCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvCtrlAC, obj.Loc_Code, trans)

                    If clsCommon.myLen(objVendorInvHead.Document_No) > 0 AndAlso clsCommon.myLen(strInvCtrlAC) > 0 Then
                        Dim coll As New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "Reco_Control_Account", "I")
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JOURNAL_DETAILS", OMInsertOrUpdate.Update, "Voucher_No='" + VOUCHER_NO + "' and Account_Code ='" + strInvCtrlAC + "'", trans)
                    End If
                Next
            End If
            'Sanjay Update I for Inventory Control Account

            'Dim isAmountDecreased As Boolean = False
            ''Create GL Entry
            'Dim TankerFromMaster As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GateEntryTankerFromTankerMaster, clsFixedParameterCode.GateEntryTankerFromTankerMaster, trans))

            'For i As Integer = 0 To obj.arrDetail.Count - 1
            '    Dim SrnAmt As Double = 0
            '    Dim ApprovedAmt As Double = 0
            '    Dim diffamt As Double = 0
            '    Dim tnkrNo As String = ""
            '    Dim Loc As String = ""
            '    Dim Silo As String = ""
            '    Dim vendorNo As String = ""
            '    Dim q As String = ""
            '    If obj.isSRNTradeInvoice = 0 Then
            '        If TankerFromMaster = 0 Then
            '            q = " select tspl_Bulk_milk_purchase_Invoice_Detail.Actual_Amount as InvAmt,TSPL_Bulk_MILK_SRN.Actual_Amount as SrnAmt,TSPL_Bulk_MILK_SRN.Vendor_Code,TSPL_Bulk_MILK_SRN.Loc_Code,tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO,TSPL_Bulk_MILK_SRN.Tanker_No,isnull(TSPL_Bulk_MILK_SRN.Actual_Amount,0)-isnull(tspl_Bulk_milk_purchase_Invoice_Detail.Actual_Amount,0) as diffamt,TSPL_MILK_UNLOADING.Sub_location_Code      from tspl_Bulk_milk_purchase_Invoice_Detail  left outer join TSPL_Bulk_MILK_SRN on TSPL_Bulk_MILK_SRN.SRN_NO=tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO left outer join TSPL_MILK_UNLOADING on TSPL_MILK_UNLOADING.Gate_Entry_No=TSPL_Bulk_MILK_SRN.Gate_Entry_No   where TSPL_Bulk_MILK_SRN.SRN_NO='" & obj.arrDetail.Item(i).SRN_NO & "' "
            '        Else
            '            q = " select tspl_Bulk_milk_purchase_Invoice_Detail.Actual_Amount as InvAmt,TSPL_Bulk_MILK_SRN.Actual_Amount as SrnAmt,TSPL_Bulk_MILK_SRN.Vendor_Code, " & _
            '                "TSPL_Bulk_MILK_SRN.Loc_Code,tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO,TSPL_Bulk_MILK_SRN.Tanker_No, " & _
            '                "isnull(TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Actual_Amount,0)-isnull(tspl_Bulk_milk_purchase_Invoice_Detail.Actual_Amount,0) as diffamt, " & _
            '                "TSPL_MILK_UNLOADING.Sub_location_Code      from tspl_Bulk_milk_purchase_Invoice_Detail  left outer join " & _
            '                "TSPL_Bulk_MILK_SRN on TSPL_Bulk_MILK_SRN.SRN_NO=tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO left outer join " & _
            '                "TSPL_BULK_MILK_SRN_CHEMBER_DETAILS on TSPL_Bulk_MILK_SRN.SRN_NO=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SRN_NO and " & _
            '                "tspl_Bulk_milk_purchase_Invoice_Detail.SL_NO=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Line_No  left outer join " & _
            '                "TSPL_MILK_UNLOADING on TSPL_MILK_UNLOADING.Gate_Entry_No=TSPL_Bulk_MILK_SRN.Gate_Entry_No   " & _
            '                "where TSPL_Bulk_MILK_SRN.SRN_NO='" & obj.arrDetail.Item(i).SRN_NO & "' "
            '        End If
            '    Else
            '        q = " select tspl_Bulk_milk_purchase_Invoice_Detail.Actual_Amount as InvAmt,TSPL_Bulk_MILK_SRN.Actual_Amount as SrnAmt,TSPL_Bulk_MILK_SRN.Vendor_Code,TSPL_Bulk_MILK_SRN.Loc_Code,tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO,TSPL_Bulk_MILK_SRN.Tanker_No,isnull(TSPL_Bulk_MILK_SRN.Actual_Amount,0)-isnull(tspl_Bulk_milk_purchase_Invoice_Detail.Actual_Amount,0) as diffamt,TSPL_Bulk_MILK_SRN.Sub_location as  Sub_location_Code      from tspl_Bulk_milk_purchase_Invoice_Detail  left outer join TSPL_Bulk_MILK_SRN on TSPL_Bulk_MILK_SRN.SRN_NO=tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO    where TSPL_Bulk_MILK_SRN.SRN_NO='" & obj.arrDetail.Item(i).SRN_NO & "' "
            '    End If

            '    Dim dtt As DataTable = clsDBFuncationality.GetDataTable(q, trans)
            '    If dtt IsNot Nothing AndAlso dtt.Rows.Count > 0 Then
            '        SrnAmt = clsCommon.myCdbl(dtt.Rows(0)("SrnAmt"))
            '        ApprovedAmt = clsCommon.myCdbl(dtt.Rows(0)("InvAmt"))
            '        diffamt = clsCommon.myCdbl(dtt.Rows(0)("diffamt"))
            '        tnkrNo = clsCommon.myCstr(dtt.Rows(0)("Tanker_No"))
            '        Loc = clsCommon.myCstr(dtt.Rows(0)("Loc_Code"))
            '        Silo = clsCommon.myCstr(dtt.Rows(0)("Sub_location_Code"))
            '        vendorNo = clsCommon.myCstr(dtt.Rows(0)("Vendor_Code"))
            '        If diffamt > 0 Then
            '            isAmountDecreased = False
            '        Else
            '            isAmountDecreased = True
            '            diffamt = diffamt * -1
            '        End If
            '        If diffamt > 0 Then
            '            If isAmountDecreased Then
            '                ''Adjustment Out Type document
            '                Dim objAdjOut As New ClsAdjustments
            '                objAdjOut.Adjustment_Date = obj.Pur_Return_Date
            '                objAdjOut.Posting_Date = obj.Pur_Return_Date
            '                objAdjOut.EntryDateTime = obj.Pur_Return_Date
            '                objAdjOut.Against_Bulk_Srn_PI_adjustment = obj.Invoice_No
            '                objAdjOut.Loc_Code = obj.Loc_Code
            '                objAdjOut.Loc_Desc = clsLocation.GetName(obj.Loc_Code, trans)
            '                objAdjOut.Trans_Type = "Out"
            '                objAdjOut.IsMilkType = 1
            '                objAdjOut.Loc_Code = Silo
            '                objAdjOut.MainLocationCode = Loc
            '                objAdjOut.Description = "Return Adjustment Against Bulk Milk SRN-PI Cost Adjustment For SRN NO: " & obj.arrDetail.Item(i).SRN_NO & " PI No: " & obj.Invoice_No & " Tanker No: " & tnkrNo & " Vendor : " & clsVendorMaster.GetName(vendorNo, trans) & " Location: " & clsLocation.GetName(Loc, trans)
            '                objAdjOut.Arr = New List(Of ClsAdjustmentsDetails)
            '                Dim objAdjOutTR As New ClsAdjustmentsDetails()

            '                objAdjOutTR.Item_Code = obj.arrDetail.Item(0).Item_Code
            '                objAdjOutTR.Item_Description = obj.arrDetail.Item(0).Item_Desc
            '                objAdjOutTR.Adjustment_Type = "CD"
            '                objAdjOutTR.Item_Quantity = 0
            '                objAdjOutTR.Item_Cost = diffamt
            '                objAdjOutTR.mrp = 0
            '                objAdjOutTR.Unit_Code = obj.arrDetail.Item(0).UOM
            '                objAdjOut.Arr.Add(objAdjOutTR)
            '                objAdjOut.SaveData(objAdjOut, True, "", trans)
            '                ClsAdjustments.PostData(objAdjOut.Adjustment_No, objAdjOut.Trans_Type, trans, False)
            '            Else
            '                ''Adjustment IN Type document
            '                Dim objAdjIn As New ClsAdjustments
            '                objAdjIn.Adjustment_Date = obj.Pur_Return_Date
            '                objAdjIn.Posting_Date = obj.Pur_Return_Date
            '                objAdjIn.EntryDateTime = obj.Pur_Return_Date
            '                objAdjIn.Against_Bulk_Srn_PI_adjustment = obj.Invoice_No
            '                objAdjIn.Loc_Code = obj.Loc_Code
            '                objAdjIn.Loc_Desc = clsLocation.GetName(obj.Loc_Code, trans)
            '                objAdjIn.Trans_Type = "In"
            '                objAdjIn.IsMilkType = 1
            '                objAdjIn.Loc_Code = Silo
            '                objAdjIn.MainLocationCode = Loc
            '                objAdjIn.Description = "Return Adjustment Against Bulk Milk SRN-PI Cost Adjustment For SRN NO: " & obj.arrDetail.Item(i).SRN_NO & " PI No: " & obj.Invoice_No & " Tanker No: " & tnkrNo & " Vendor : " & clsVendorMaster.GetName(vendorNo, trans) & " Location: " & clsLocation.GetName(Loc, trans)
            '                objAdjIn.Arr = New List(Of ClsAdjustmentsDetails)
            '                Dim objAdjInTR As New ClsAdjustmentsDetails()

            '                objAdjInTR.Item_Code = obj.arrDetail.Item(0).Item_Code
            '                objAdjInTR.Item_Description = obj.arrDetail.Item(0).Item_Desc
            '                objAdjInTR.Adjustment_Type = "CI"
            '                objAdjInTR.Item_Quantity = 0
            '                objAdjInTR.Item_Cost = diffamt
            '                objAdjInTR.mrp = 0
            '                objAdjInTR.Unit_Code = obj.arrDetail.Item(0).UOM
            '                objAdjIn.Arr.Add(objAdjInTR)
            '                objAdjIn.SaveData(objAdjIn, True, "", trans)
            '                ClsAdjustments.PostData(objAdjIn.Adjustment_No, objAdjIn.Trans_Type, trans, False)

            '            End If

            '        End If

            '    End If
            'Next

            'qry = " select TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing ,TSPL_Bulk_MILK_SRN.Actual_Amount,TSPL_Bulk_MILK_SRN.Loc_Code   from TSPL_PURCHASE_ACCOUNTS left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Purchase_Class_Code =TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code left outer join TSPL_Bulk_MILK_SRN on TSPL_Bulk_MILK_SRN .Item_Code=TSPL_ITEM_MASTER.Item_Code where TSPL_Bulk_MILK_SRN.SRN_NO='" & obj.arrDetail.Item(0).SRN_NO & "' "
            'Dim ArryLst As ArrayList = New ArrayList()
            'dt = clsDBFuncationality.GetDataTable(qry, trans)
            'Dim TotActualAmount As Double = 0
            'If TankerFromMaster = 0 Then
            '    TotActualAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select SUM(TSPL_Bulk_MILK_SRN.Actual_Amount ) as acTAmt from TSPL_Bulk_MILK_SRN   where SRN_NO in (select SRN_NO from tspl_Bulk_milk_purchase_Invoice_Detail where DOC_NO='" & obj.Invoice_No & "')", trans))
            'Else
            '    TotActualAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select SUM(TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Actual_Amount ) as acTAmt from TSPL_BULK_MILK_SRN_CHEMBER_DETAILS   where TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SRN_NO in (select tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO from tspl_Bulk_milk_purchase_Invoice_Detail where DOC_NO='" & obj.Invoice_No & "')", trans))
            'End If
            'Dim DiffAmount As Double = 0
            'DiffAmount = (TotActualAmount + obj.RoundOffAmount) - (obj.Total_AMT)

            'If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0 Then
            '    If DiffAmount > 0 Then
            '        isAmountDecreased = False
            '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            '            Dim strInvCntrlAc As String = dt.Rows(0)("Inv_Control_Account")
            '            Dim strPaybleClrAc As String = dt.Rows(0)("Inv_Payable_Clearing")
            '            strInvCntrlAc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvCntrlAc, dt.Rows(0)("Loc_Code"), trans)
            '            strPaybleClrAc = clsERPFuncationality.ChangeGLAccountLocationSegment(strPaybleClrAc, dt.Rows(0)("Loc_Code"), trans)
            '            ArryLst.Add(New String() {strInvCntrlAc, DiffAmount * -1})
            '            ArryLst.Add(New String() {strPaybleClrAc, DiffAmount})
            '            clsJournalMaster.FunGrnlEntryWithTrans(obj.Loc_Code, False, trans, clsCommon.GetPrintDate(obj.Pur_Return_Date, "dd/MMM/yyyy"), "Return Adjustment Against Bulk Milk Purchase Invoice  -" + obj.Invoice_No + "", "BM-PR", "Bulk Milk Purchase Return", obj.Invoice_No, "", "C", obj.Invoice_No, obj.Invoice_No, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, "", " Vendor - " & obj.vendor_code & ", " & clsVendorMaster.GetName(obj.vendor_code, trans))
            '        End If
            '    ElseIf DiffAmount < 0 Then
            '        isAmountDecreased = True
            '        DiffAmount = DiffAmount * -1
            '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            '            Dim strInvCntrlAc As String = dt.Rows(0)("Inv_Control_Account")
            '            Dim strPaybleClrAc As String = dt.Rows(0)("Inv_Payable_Clearing")
            '            strInvCntrlAc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvCntrlAc, dt.Rows(0)("Loc_Code"), trans)
            '            strPaybleClrAc = clsERPFuncationality.ChangeGLAccountLocationSegment(strPaybleClrAc, dt.Rows(0)("Loc_Code"), trans)
            '            ArryLst.Add(New String() {strInvCntrlAc, DiffAmount})
            '            ArryLst.Add(New String() {strPaybleClrAc, DiffAmount * -1})
            '            clsJournalMaster.FunGrnlEntryWithTrans(obj.Loc_Code, False, trans, clsCommon.GetPrintDate(obj.Pur_Return_Date, "dd/MMM/yyyy"), "Return Adjustment Against Bulk Milk Purchase Invoice  -" + obj.Invoice_No + "", "BM-PI", "Bulk Milk Purchase Invoice", obj.Invoice_No, "", "C", obj.Invoice_No, obj.Invoice_No, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, "", " Vendor - " & obj.vendor_code & ", " & clsVendorMaster.GetName(obj.vendor_code, trans))
            '        End If
            '    End If
            'End If

            '==========================================Stock will be updated as reverse of SRN=============================================
            Dim ArrInventoryMovement As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)

            For Each objPRDetail As clsMilkPurchaseReturnDetail In obj.arrDetail

                Dim objInventoryMovement As New clsInventoryMovementNew()
                Dim iii As Integer = 0
                objInventoryMovement.Trans_Type = "BULKMILK-RETURN"

                'sanjay Ticket No-TEC/03/05/19-000473
                objInventoryMovement.CalculateAvgCost = False
                objInventoryMovement.FIFO_Cost = 0
                objInventoryMovement.LIFO_Cost = 0
                objInventoryMovement.Avg_Cost = objPRDetail.Actual_Amount
                'sanjay

                objInventoryMovement.InOut = "O"
                objInventoryMovement.Location_Code = obj.Loc_Code
                objInventoryMovement.Item_Code = objPRDetail.Item_Code
                objInventoryMovement.Item_Desc = objPRDetail.Item_Desc
                objInventoryMovement.Qty = objPRDetail.Invoice_Qty
                objInventoryMovement.UOM = objPRDetail.UOM
                objInventoryMovement.Source_Doc_No = obj.Pur_Return_No
                objInventoryMovement.Source_Doc_Date = obj.Pur_Return_Date
                objInventoryMovement.FAT_Per = objPRDetail.fat_per
                objInventoryMovement.SNF_Per = objPRDetail.snf_Per
                objInventoryMovement.FAT_KG = objPRDetail.fat_KG
                objInventoryMovement.SNF_KG = objPRDetail.SNF_KG
                ' objInventoryMovement.Batch_No = 

                '' UPDATE PRODUCTION COST
                objInventoryMovement.Fat_Rate = objPRDetail.fat_Rate
                objInventoryMovement.SNF_Rate = objPRDetail.SNF_Rate
                'objInventoryMovement.Fat_Amt = obj.f
                'objInventoryMovement.SNF_Amt = objPRDetail.S

                'objInventoryMovement.Avg_Cost = objPRDetail.Fat_Amt + objBack.SNF_Amt
                'objInventoryMovement.FIFO_Cost = objPRDetail.Fat_Amt + objBack.SNF_Amt
                'objInventoryMovement.LIFO_Cost = objPRDetail.Fat_Amt + objBack.SNF_Amt
                objInventoryMovement.Vendor_Code = obj.vendor_code
                'objInventoryMovement.Vendor_Name=obj.v

                '' Work done by sanjay on Against ticket no.TEC/05/02/19-000414 
                'Dim item_Purchase_Class As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Purchase_Class_Code from TSPL_ITEM_MASTER where Item_Code='" & objInventoryMovement.Item_Code & "'", trans))
                'Dim qry1 As String = "select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code='" + objInventoryMovement.Location_Code + "'"
                'Dim strLocatinSegment As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry1, trans))
                'If clsCommon.myLen(item_Purchase_Class) > 0 Then
                '    Dim Inventory_Purchase_code As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Inv_Control_Account from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code='" & item_Purchase_Class & "'", trans))
                '    If clsCommon.myLen(Inventory_Purchase_code) > 0 Then
                '        objInventoryMovement.Inventory_CrAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(Inventory_Purchase_code, strLocatinSegment, trans)               
                '    End If
                'End If
                '' end

                ArrInventoryMovement.Add(objInventoryMovement)
            Next

            If ArrInventoryMovement.Count > 0 Then
                clsInventoryMovementNew.SaveData(formId, strDocNo, clsCommon.GetPrintDate(obj.Pur_Return_Date, "dd/MMM/yyyy"), clsCommon.GetPrintDate(obj.Pur_Return_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
            End If

            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0 Then
                '' Work done by sanjay on Against ticket no.TEC/05/02/19-000414 
                Dim TempInventory_DrAcc = ""
                Dim TempInventory_CrAcc = ""
                For Each objtemp As clsInventoryMovementNew In ArrInventoryMovement

                    Dim qry1 As String = "select TSPL_INVENTORY_MOVEMENT_new.Inventory_DrAcc,TSPL_INVENTORY_MOVEMENT_new.Inventory_CrAcc " & _
                                          " from TSPL_INVENTORY_MOVEMENT_new " & _
                                          " left outer join tspl_Bulk_milk_purchase_Invoice_Detail on tspl_Bulk_milk_purchase_Invoice_Detail.Item_Code=TSPL_INVENTORY_MOVEMENT_new.Item_Code " & _
                                          " and tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO=TSPL_INVENTORY_MOVEMENT_new.Source_Doc_No " & _
                                          " left outer join TSPL_Bulk_MILK_SRN on TSPL_Bulk_MILK_SRN.SRN_NO=tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO  " & _
                                          " left outer join TSPL_BULK_MILK_SRN_CHEMBER_DETAILS on TSPL_Bulk_MILK_SRN.SRN_NO=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SRN_NO and  " & _
                                          " tspl_Bulk_milk_purchase_Invoice_Detail.SL_NO = TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Line_No "
                    '" left outer join TSPL_BULK_MILK_PURCHASE_RETURN_HEAD on TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Invoice_No=tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO "
                    ' " where TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_No='" + strDocNo + "' and tspl_Bulk_milk_purchase_Invoice_Detail.Item_Code='" + objtemp.Item_Code + "'"
                    qry1 += " where tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO='" + obj.Invoice_No + "' and tspl_Bulk_milk_purchase_Invoice_Detail.Item_Code='" + objtemp.Item_Code + "'"
                    Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry1, trans)
                    If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                        'sanjay Ticket No-TEC/12/06/19-000532  Always credit account is null
                        'TempInventory_DrAcc = clsCommon.myCstr(dt1.Rows(0)("Inventory_CrAcc"))
                        TempInventory_CrAcc = clsCommon.myCstr(dt1.Rows(0)("Inventory_DrAcc"))
                    End If
                    clsInventoryMovement.UpdateInvControlAccount(strDocNo, "M-PURRETURN", objtemp.Item_Code, TempInventory_DrAcc, TempInventory_CrAcc, "", trans)
                Next
            End If
            '' Work done by sanjay on Against ticket no.TEC/05/02/19-000414 
            '================================================================End============================================================

            '===========================if Journal Voucher is created at the time of SRN then opposite jv will be created===================
            'Dim VendorDocumentNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_No from TSPL_VENDOR_INVOICE_HEAD where Against_BulkMillkPurchaseInvoice_No='" + obj.Invoice_No + "'", trans))
            'Dim VoucherNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No='" + VendorDocumentNo + "'", trans))
            'Dim JournalNo As Integer = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select journal_no from TSPL_JOURNAL_MASTER where Source_Doc_No='" + VendorDocumentNo + "'", trans))

            'Dim dtVoucherHead As DataTable = clsDBFuncationality.GetDataTable("SELECT [Journal_No],[Voucher_No],[Voucher_Date],[Source_Code],[Source_Desc],[Source_Doc_No],[Source_Doc_Date],[Posting_Date],[Voucher_Desc],[Source_Narration],[Remarks],[Comments],[Auto_Reverse],[Reverse_Date],[Source_Type],[CustVend_Code],[CustVend_Name],[Transaction_Type],[Provisional_Post],[Authorized],[Total_Debit_Amt],[Total_Credit_Amt],[Created_By],[Created_Date],[Modify_By],[Modify_Date],[Comp_Code],[Type],[SendToTally],[CURRENCY_CODE],[ConvRate],[ApplicableFrom],[ConvRateOld],[Segment_code] FROM TSPL_JOURNAL_MASTER Where [Voucher_No]='" + VoucherNo + "'", trans)

            'connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_MASTER_INSERT", New SqlParameter("@Journal_No", JournalNo),
            '                           New SqlParameter("@Voucher_No", VoucherNo),
            '                           New SqlParameter("@Voucher_Date", clsCommon.GetPrintDate(dtVoucherHead.Rows(0)("Voucher_Date").ToString(), "dd/MMM/yyyy")),
            '                           New SqlParameter("@Source_Code", "AP-DN"), New SqlParameter("@Source_Desc", "AP Invoice"),
            '                           New SqlParameter("@Source_Doc_No", obj.Pur_Return_No), New SqlParameter("@Source_Doc_Date", clsCommon.GetPrintDate(obj.Pur_Return_Date, "dd/MMM/yyyy")),
            '                           New SqlParameter("@Posting_Date", clsCommon.GetPrintDate(connectSql.serverDate(trans), "dd/MMM/yyyy")),
            '                           New SqlParameter("@Voucher_Desc", "Return " + dtVoucherHead.Rows(0)("Voucher_Desc").ToString()), New SqlParameter("@Source_Narration", ""),
            '                           New SqlParameter("@Remarks", dtVoucherHead.Rows(0)("Remarks")), New SqlParameter("@Comments", dtVoucherHead.Rows(0)("Comments")),
            '                           New SqlParameter("@Auto_Reverse", dtVoucherHead.Rows(0)("Auto_Reverse")), New SqlParameter("@Reverse_Date", dtVoucherHead.Rows(0)("Reverse_Date")),
            '                           New SqlParameter("@Source_Type", dtVoucherHead.Rows(0)("Source_Type")), New SqlParameter("@CustVend_Code", obj.vendor_code),
            '                           New SqlParameter("@CustVend_Name", ""), New SqlParameter("@Transaction_Type", dtVoucherHead.Rows(0)("Transaction_Type")),
            '                           New SqlParameter("@Total_Debit_Amt", dtVoucherHead.Rows(0)("Total_Debit_Amt")), New SqlParameter("@Total_Credit_Amt", dtVoucherHead.Rows(0)("Total_Credit_Amt")),
            '                           New SqlParameter("@Created_By", objCommonVar.CurrentUserCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)),
            '                           New SqlParameter("@Modify_By", objCommonVar.CurrentUserCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)),
            '                           New SqlParameter("@Comp_Code", objCommonVar.CurrentCompanyCode))

            'Dim dtVoucherDetails As DataTable = clsDBFuncationality.GetDataTable("SELECT  Journal_No ,Voucher_No,Voucher_Date,Detail_Line_No,Account_code,Account_Desc   ,Amount,Description,Reference,Posting_Date,Account_Type,Account_Group_Code,Account_Seg_Code1,Account_Seg_Desc1,Account_Seg_Code2,Account_Seg_Desc2 ,Account_Seg_Code3,Account_Seg_Desc3,Account_Seg_Code4,Account_Seg_Desc4,Account_Seg_Code5,Account_Seg_Desc5,Account_Seg_Code6,Account_Seg_Desc6,Account_Seg_Code7,Account_Seg_Desc7,Account_Seg_Code8,Account_Seg_Desc8,Account_Seg_Code9,Account_Seg_Desc9,Account_Seg_Code10,Account_Seg_Desc10,CustVend_Code,CustVend_Name,PK_Id FROM TSPL_JOURNAL_DETAILS Where [Voucher_No]='" + VoucherNo + "'", trans)
            'If dtVoucherDetails.Rows.Count > 0 Then
            '    Dim i As Integer = 0
            '    Dim detailLineNo = dtVoucherDetails.Rows.Count + 1
            '    For Each dr As DataRow In dtVoucherDetails.Rows
            '        Dim amount As Double = 0
            '        If clsCommon.myCdbl(dr("Amount")) <> 0 Then
            '            amount = -(clsCommon.myCdbl(dr("Amount")))

            '        End If
            '        Dim yyyy As String = dr("Voucher_Date")
            '        connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", JournalNo), New SqlParameter("@Voucher_No", VoucherNo), New SqlParameter("@Voucher_Date", clsCommon.GetPrintDate(dr("Voucher_Date"), "dd/MMM/yyyy")), New SqlParameter("@Detail_Line_No", detailLineNo), New SqlParameter("@Account_code", dr("Account_code")), New SqlParameter("@Account_Desc", dr("Account_Desc")), New SqlParameter("@Amount", amount), New SqlParameter("@Description", dr("Description")), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", clsCommon.GetPrintDate(connectSql.serverDate(trans), "dd/MMM/yyyy")), New SqlParameter("@Account_Type", dr("Account_Type")), New SqlParameter("@Account_Group_Code", dr("Account_Group_Code")), New SqlParameter("@Account_Seg_Code1", dr("Account_Seg_Code1")), New SqlParameter("@Account_Seg_Desc1", dr("Account_Seg_Desc1")), New SqlParameter("@Account_Seg_Code2", dr("Account_Seg_Code2")), New SqlParameter("@Account_Seg_Desc2", dr("Account_Seg_Desc2")), New SqlParameter("@Account_Seg_Code3", dr("Account_Seg_Code3")), New SqlParameter("@Account_Seg_Desc3", dr("Account_Seg_Desc3")), New SqlParameter("@Account_Seg_Code4", dr("Account_Seg_Code4")), New SqlParameter("@Account_Seg_Desc4", dr("Account_Seg_Desc4")), New SqlParameter("@Account_Seg_Code5", dr("Account_Seg_Code5")), New SqlParameter("@Account_Seg_Desc5", dr("Account_Seg_Desc5")), New SqlParameter("@Account_Seg_Code6", dr("Account_Seg_Code6")), New SqlParameter("@Account_Seg_Desc6", dr("Account_Seg_Desc6")), New SqlParameter("@Account_Seg_Code7", dr("Account_Seg_Code7")), New SqlParameter("@Account_Seg_Desc7", dr("Account_Seg_Desc7")), New SqlParameter("@Account_Seg_Code8", dr("Account_Seg_Code8")), New SqlParameter("@Account_Seg_Desc8", dr("Account_Seg_Desc8")), New SqlParameter("@Account_Seg_Code9", dr("Account_Seg_Code9")), New SqlParameter("@Account_Seg_Desc9", dr("Account_Seg_Desc9")), New SqlParameter("@Account_Seg_Code10", dr("Account_Seg_Code10")), New SqlParameter("@Account_Seg_Desc10", dr("Account_Seg_Desc10")))
            '        detailLineNo = detailLineNo + 1
            '    Next
            'End If







            '=========================================================END====================================================================
            '=========================================================New====================================================================

            '''''''''''''Sanjay commented asked by Ranjana Man
            ' ''Journal Entry
            'Dim SRN_No As String = ""
            ' ''* comment below point because if any GL made from SRN screen only then reverse entry created,but below code get the AP no. of invoice *==========
            ''SRN_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_No from TSPL_VENDOR_INVOICE_HEAD where Against_BulkMillkPurchaseInvoice_No='" + obj.Invoice_No + "'", trans))
            ' ''=========================================================================

            'qry = " select TSPL_JOURNAL_DETAILS.Account_code,-1*TSPL_JOURNAL_DETAILS.Amount as Amount,ISNULL(Reco_Control_Account,'') as Reco_Control_Account from TSPL_JOURNAL_DETAILS " & _
            '" left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No" & _
            '" where TSPL_JOURNAL_MASTER.Source_Doc_No in (select srn_no from TSPL_BULK_MILK_PURCHASE_INVOICE_detail where doc_no='" + obj.Invoice_No + "')  and Source_Code in ('BM-SR')"
            'dt = clsDBFuncationality.GetDataTable(qry, trans)
            'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            '    Dim ArryLstGLAC As ArrayList = New ArrayList()
            '    For Each dr As DataRow In dt.Rows
            '        Dim Acc() As String = {clsCommon.myCstr(dr("Account_code")), clsCommon.myCdbl(dr("Amount")), "", "", "", "", "", "", clsCommon.myCstr(dr("Reco_Control_Account"))}
            '        ArryLstGLAC.Add(Acc)
            '    Next
            '    clsJournalMaster.FunGrnlEntryWithTrans(obj.Loc_Code, False, trans, obj.Pur_Return_Date, "Against Bulk Milk Purchase Return " + obj.Pur_Return_No, "BM-PR", "Bulk Milk Store Received Note Return", obj.Pur_Return_No, "", "V", obj.vendor_code, vendor_name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC)

            'End If
            '''''''''''''Sanjay commented asked by Ranjana Man

            '========================================================New END ================================================================






            qry = "Update TSPL_Bulk_MILK_PURCHASE_RETURN_HEAD set isPosted=1, Posting_Date='" + clsCommon.GetPrintDate(obj.Pur_Return_Date, "dd/MMM/yyyy") + "' where Pur_Return_No='" + strDocNo + "'"
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

    'Public Function SaveData111(ByVal obj As clsMilkPurchaseReturnHead, ByVal isNewEntry As Boolean) As Boolean
    '    Dim isSaved As Boolean = True
    '    Try

    '        Dim objSRN As clsSRNHead = clsSRNHead.GetData(obj.SRN_No, NavigatorType.Current)
    '        AllowToSave(obj, objSRN)
    '        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
    '        clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase Order", "Store Received Note Return", obj.Bill_To_Location, Document_Date, trans)
    '        Try
    '            If isNewEntry Then
    '                obj.Document_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Document_Date), clsDocType.SRNReturn, "", obj.Bill_To_Location)
    '            End If
    '            If (clsCommon.myLen(obj.Document_No) <= 0) Then
    '                Throw New Exception("Error in Document Code Generation")
    '            End If

    '            Dim coll As New Hashtable()
    '            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
    '            clsCommon.AddColumnsForChange(coll, "SRN_No", obj.SRN_No)
    '            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
    '            If isNewEntry Then
    '                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
    '                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
    '                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
    '                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SRN_RETURN", OMInsertOrUpdate.Insert, "", trans)
    '            Else
    '                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SRN_RETURN", OMInsertOrUpdate.Update, "TSPL_SRN_RETURN.Document_No='" + obj.Document_No + "'", trans)
    '            End If
    '            isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.Document_No, obj.arrCustomFields, trans)

    '            ''Revese Inventory movement
    '            Dim qry As String = "select * from TSPL_INVENTORY_MOVEMENT where Trans_Type='SRN' and Source_Doc_No='" + obj.SRN_No + "'"
    '            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
    '            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '                Dim ArrInventoryMovement As New List(Of clsInventoryMovement)
    '                Dim objInvMov As clsInventoryMovement
    '                For Each dr As DataRow In dt.Rows
    '                    objInvMov = New clsInventoryMovement
    '                    objInvMov.InOut = "O"
    '                    objInvMov.Location_Code = clsCommon.myCstr(dr("Location_Code"))
    '                    objInvMov.Item_Code = clsCommon.myCstr(dr("Item_Code"))
    '                    objInvMov.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
    '                    objInvMov.Qty = clsCommon.myCstr(dr("Qty"))
    '                    objInvMov.UOM = clsCommon.myCstr(dr("UOM"))
    '                    objInvMov.Source_Doc_No = obj.Document_No
    '                    objInvMov.Source_Doc_Date = obj.Document_Date
    '                    objInvMov.Entry_Date = clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy")
    '                    objInvMov.Basic_Cost = clsCommon.myCdbl(dr("Basic_Cost"))
    '                    objInvMov.Rec_Cost = clsCommon.myCdbl(dr("Rec_Cost"))
    '                    objInvMov.Add_Cost = clsCommon.myCdbl(dr("Add_Cost"))
    '                    objInvMov.Net_Cost = clsCommon.myCdbl(dr("Net_Cost"))
    '                    objInvMov.ItemType = clsCommon.myCstr(dr("ItemType"))
    '                    objInvMov.Punching_Date = obj.Document_Date
    '                    objInvMov.MRP = clsCommon.myCdbl(dr("MRP"))
    '                    objInvMov.Batch_No = clsCommon.myCstr(dr("Batch_No"))
    '                    objInvMov.FIFO_Cost = clsCommon.myCdbl(dr("FIFO_Cost"))
    '                    objInvMov.LIFO_Cost = clsCommon.myCdbl(dr("LIFO_Cost"))
    '                    objInvMov.Avg_Cost = clsCommon.myCdbl(dr("Avg_Cost"))
    '                    objInvMov.Posting_Date = obj.Document_Date
    '                    objInvMov.Stock_UOM = clsCommon.myCstr(dr("Stock_UOM"))
    '                    objInvMov.Stock_Qty = clsCommon.myCdbl(dr("Stock_Qty"))
    '                    If dr("MFG_Date") IsNot DBNull.Value Then
    '                        objInvMov.MFG_Date = clsCommon.myCstr(dr("MFG_Date"))
    '                    End If
    '                    If dr("Expiry_Date") IsNot DBNull.Value Then
    '                        objInvMov.Expiry_Date = clsCommon.myCDate(dr("Expiry_Date"))
    '                    End If
    '                    objInvMov.IS_CONSUMPTION = clsCommon.myCdbl(dr("IS_CONSUMPTION"))
    '                    objInvMov.Cust_Code = clsCommon.myCstr(dr("Cust_Code"))
    '                    objInvMov.Cust_Name = clsCommon.myCstr(dr("Cust_Name"))
    '                    objInvMov.Vendor_Code = clsCommon.myCstr(dr("Vendor_Code"))
    '                    objInvMov.Vendor_Name = clsCommon.myCstr(dr("Vendor_Name"))
    '                    objInvMov.Other_Location_Code = clsCommon.myCstr(dr("Other_Location_Code"))
    '                    objInvMov.Other_Location_Desc = clsCommon.myCstr(dr("Other_Location_Desc"))
    '                    ArrInventoryMovement.Add(objInvMov)
    '                Next
    '                isSaved = isSaved AndAlso clsInventoryMovement.SaveData("SRN-RET", obj.Document_No, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
    '            End If
    '            ''Journal Entry
    '            qry = " select TSPL_JOURNAL_DETAILS.Account_code,-1*TSPL_JOURNAL_DETAILS.Amount as Amount from TSPL_JOURNAL_DETAILS " & _
    '            " left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No" & _
    '            " where TSPL_JOURNAL_MASTER.Source_Doc_No='" + obj.SRN + "'  and Source_Code in ('SR-RG','PO-RC')"
    '            dt = clsDBFuncationality.GetDataTable(qry, trans)
    '            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '                Dim ArryLstGLAC As ArrayList = New ArrayList()
    '                For Each dr As DataRow In dt.Rows
    '                    Dim Acc() As String = {clsCommon.myCstr(dr("Account_code")), clsCommon.myCdbl(dr("Amount"))}
    '                    ArryLstGLAC.Add(Acc)
    '                Next
    '                clsJournalMaster.FunGrnlEntryWithTrans(obj.Bill_To_Location, False, trans, obj.Document_Date, "Against SRN Return " + obj.Document_No, "SN-RT", "Store Received Note Return", obj.Document_No, obj.Remarks, "V", objSRN.Vendor_Code, objSRN.Vendor_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC)
    '            End If
    '            trans.Commit()
    '        Catch ex As Exception
    '            trans.Rollback()
    '            Throw New Exception(ex.Message)
    '        End Try
    '    Catch err As Exception
    '        Throw New Exception(err.Message)
    '    End Try
    '    Return isSaved
    'End Function
    'Private Shared Function AllowToSave(ByVal obj As clsSRNReturn, ByVal objSRN As clsSRNHead) As Boolean
    '    Dim Qry As String = "select Document_No from TSPL_SRN_RETURN where SRN_No='" + obj.SRN_No + "'"
    '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
    '    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '        Throw New Exception("SRN Return No " + clsCommon.myCstr(clsDBFuncationality.getSingleValue(dt.Rows(0)("Document_No")) + " already created"))
    '    End If

    '    Qry = "select distinct PI_No from TSPL_PI_DETAIL where SRN_Id ='" + obj.SRN_No + "'"
    '    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '        Throw New Exception("SRN is used in Purchase Invoice No " + clsCommon.myCstr(clsDBFuncationality.getSingleValue(dt.Rows(0)("PI_No")) + " can not return it."))
    '    End If


    '    If objSRN.Arr IsNot Nothing AndAlso objSRN.Arr.Count > 0 Then
    '        For Each objsrntr As clsSRNDetail In objSRN.Arr
    '            Dim bal As Double = clsItemLocationDetails.getBalanceWithUnapprove(objsrntr.Item_Code, objSRN.Bill_To_Location, objsrntr.MRP, objsrntr.Unit_code, obj.Document_No, obj.Document_Date)
    '            If bal < 0 Then
    '                Throw New Exception("Balance is going to -ve for item " + objsrntr.Item_Code)
    '            End If
    '        Next
    '    End If
    '    Return True
    'End Function

    Public Shared Function deleteData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry2 As String = "delete from TSPL_BULK_MILK_PURCHASE_RETURN_HEAD where Pur_Return_No='" & strDocNo & "'"
            Dim qry1 As String = "delete from TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL where Pur_Return_No='" & strDocNo & "'"
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
            'BHA/21/08/18-000470 by balwinder on 13/09/2018
            Dim qry As String = " select TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_No as [DocNo] ,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_Date as [Doc Date] ,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Invoice_No as [Invoice No] ,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Invoice_Date as [Invoice Date] ,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.vendor_code as [Vendor Code],tspl_vendor_master.vendor_name as [Vendor Name],TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.loc_code as [Location Code],tspl_location_master.location_desc as [Location Desc] ,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Total_FAT_KG as [Total Fat Kg] ,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Total_SNF_KG as [Total SNF Kg] ,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Total_QTY as [Total Qty] ,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Total_AMT as [Total Amt] ,case when isnull(TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.isPosted,0)=0 then 'No' else 'Yes' end as [Is Posted] ,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Posting_Date as [Posting Date] ,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Created_By as [Created By] ,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Created_Date as [Created Date] ,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Modified_By as [Modified By] ,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Modified_Date as [Modified Date] ,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Comp_Code as [Comp Code],case when isnull(isSRNTradeInvoice,0)=0 then 'BULK MILK SRN' else 'BULK MILK SRN TRADE' end  as DocType,stuff((select ',' + isnull(SRN_NO ,'') from TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL  where TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Pur_Return_No =TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_No   for xml path ('')),1,1,'' )as  [SRN No.] From TSPL_BULK_MILK_PURCHASE_RETURN_HEAD  left outer join tspl_vendor_master on TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.vendor_code=tspl_vendor_master.vendor_code left outer join tspl_location_master on TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.loc_code=tspl_location_master.location_code "
            str = clsCommon.ShowSelectForm("PBMRTNFND", qry, "DocNo", whrcls, curcode, "TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_Date desc", isButtonClicked)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return str
    End Function

    Public Shared Function saveData(ByVal obj As clsMilkPurchaseReturnHead, ByVal trans As SqlTransaction) As Boolean
        Try
            ' clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Milk Procurement Bulk", "Bulk Milk Purchase Invoice", obj.Loc_Code, obj.DOC_DATE, trans)
            ' clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Milk Procurement MCC", "Milk Purchase Invoice", obj.Loc_Code, obj.DOC_DATE, trans)

            Dim issaved As Boolean = True
            ' clsERPFuncationality.IsDocumentAlreadyPosted("TSPL_BULK_MILK_PURCHASE_RETURN_HEAD", "DOC_NO", obj.DOC_NO, "isPosted=1", trans)
            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "Pur_Return_No", clsCommon.myCstr(obj.Pur_Return_No))
            If clsCommon.myLen(obj.Pur_Return_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Pur_Return_Date", clsCommon.GetPrintDate(obj.Pur_Return_Date, "dd/MMM/yyyy hh:mm tt"))
            End If

            clsCommon.AddColumnsForChange(coll, "Invoice_No", clsCommon.myCstr(obj.Invoice_No))
            If clsCommon.myLen(obj.Invoice_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Invoice_Date", clsCommon.GetPrintDate(obj.Invoice_Date, "dd/MMM/yyyy hh:mm tt"))
            End If
            clsCommon.AddColumnsForChange(coll, "vendor_code", clsCommon.myCstr(obj.vendor_code))
            clsCommon.AddColumnsForChange(coll, "Loc_Code", clsCommon.myCstr(obj.Loc_Code))
            If clsCommon.myLen(obj.SRN_From_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "SRN_From_Date", clsCommon.GetPrintDate(obj.SRN_From_Date, "dd/MMM/yyyy hh:mm tt"))
            End If

            If clsCommon.myLen(obj.SRN_TO_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "SRN_TO_Date", clsCommon.GetPrintDate(obj.SRN_TO_Date, "dd/MMM/yyyy hh:mm tt"))
            End If
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
            If obj.isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", clsCommon.myCstr(obj.Created_By))
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(obj.Created_Date))
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULK_MILK_PURCHASE_RETURN_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULK_MILK_PURCHASE_RETURN_HEAD", OMInsertOrUpdate.Update, "TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_No='" + obj.Pur_Return_No + "'", trans)
            End If
            issaved = issaved AndAlso clsMilkPurchaseReturnDetail.saveData(obj.arrDetail, obj.Pur_Return_No, trans)
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function getData(ByVal strCode As String, ByVal navtype As NavigatorType, Optional trans As SqlTransaction = Nothing) As clsMilkPurchaseReturnHead
        Dim obj As New clsMilkPurchaseReturnHead
        Try
            Dim whrCls As String = String.Empty
            Dim qst As String = " select *   From TSPL_BULK_MILK_PURCHASE_RETURN_HEAD   where 1=1  and comp_code='" & objCommonVar.CurrentCompanyCode & "' "
            If Not clsMccMaster.isCurrentUserHO(trans) Then
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    whrCls = " and TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Loc_Code in (" & objCommonVar.strCurrUserLocations & ")"
                End If
            End If
            qst = qst & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_No in ('" + strCode + "') "
                Case NavigatorType.Next
                    qst += " and TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_No in (select min(Pur_Return_No ) from TSPL_BULK_MILK_PURCHASE_RETURN_HEAD where Pur_Return_No  >'" + strCode + "'   " & whrCls & ")"
                Case NavigatorType.First
                    qst += " and TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_No in (select MIN(Pur_Return_No ) from TSPL_BULK_MILK_PURCHASE_RETURN_HEAD where 1=1  " & whrCls & ")"
                Case NavigatorType.Last
                    qst += " and TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_No in (select Max(Pur_Return_No ) from TSPL_BULK_MILK_PURCHASE_RETURN_HEAD where 1=1  " & whrCls & ")"
                Case NavigatorType.Previous
                    qst += " and TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_No in (select Max(Pur_Return_No ) from TSPL_BULK_MILK_PURCHASE_RETURN_HEAD where Pur_Return_No  <'" + strCode + "'   " & whrCls & ")"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.IsAgainstJobWork = clsCommon.myCdbl(dt.Rows(0)("IsAgainstJobWork"))
                obj.Joblocation_Code = clsCommon.myCstr(dt.Rows(0)("Joblocation_Code"))
                obj.Pur_Return_No = clsCommon.myCstr(dt.Rows(0)("Pur_Return_No"))
                obj.Pur_Return_Date = clsCommon.myCDate(dt.Rows(0)("Pur_Return_Date"))
                obj.Invoice_No = clsCommon.myCstr(dt.Rows(0)("Invoice_No"))
                obj.Invoice_Date = clsCommon.myCDate(dt.Rows(0)("Invoice_Date"))
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
                obj.arrDetail = clsMilkPurchaseReturnDetail.getData(obj.Pur_Return_No, trans)
            Else
                obj = Nothing
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function

    ' Invice Get Data 
    Public Shared Function getInvoiceData(ByVal strCode As String, ByVal navtype As NavigatorType, Optional trans As SqlTransaction = Nothing) As clsMilkPurchaseReturnHead
        Dim obj As New clsMilkPurchaseReturnHead
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
                obj.Invoice_No = clsCommon.myCstr(dt.Rows(0)("DOC_NO"))
                obj.Invoice_Date = clsCommon.myCDate(dt.Rows(0)("DOC_DATE"))
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
                obj.arrDetail = clsMilkPurchaseReturnDetail.geInvoicetData(obj.Invoice_No, trans)
            Else
                obj = Nothing
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function

    Public Shared Function getInvoiceFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try
            Dim qry As String = "select TSPL_BULK_MILK_PURCHASE_INVOICE_head.DOC_NO as [DocNo] ,TSPL_BULK_MILK_PURCHASE_INVOICE_head.DOC_DATE as [Doc Date] ,TSPL_BULK_MILK_PURCHASE_INVOICE_head.vendor_code as [Vendor Code],tspl_vendor_master.vendor_name as [Vendor Name],tspl_bulk_milk_purchase_invoice_head.loc_code as [Location Code],tspl_location_master.location_desc as [Location Desc] ,TSPL_BULK_MILK_PURCHASE_INVOICE_head.Total_FAT_KG as [Total Fat Kg] ,TSPL_BULK_MILK_PURCHASE_INVOICE_head.Total_SNF_KG as [Total SNF Kg] ,TSPL_BULK_MILK_PURCHASE_INVOICE_head.Total_QTY as [Total Qty] ,TSPL_BULK_MILK_PURCHASE_INVOICE_head.Total_AMT as [Total Amt] ,case when isnull(TSPL_BULK_MILK_PURCHASE_INVOICE_head.isPosted,0)=0 then 'No' else 'Yes' end as [Is Posted] ,TSPL_BULK_MILK_PURCHASE_INVOICE_head.Posting_Date as [Posting Date] ,TSPL_BULK_MILK_PURCHASE_INVOICE_head.Created_By as [Created By] ,TSPL_BULK_MILK_PURCHASE_INVOICE_head.Created_Date as [Created Date] ,TSPL_BULK_MILK_PURCHASE_INVOICE_head.Modified_By as [Modified By] ,TSPL_BULK_MILK_PURCHASE_INVOICE_head.Modified_Date as [Modified Date] ,TSPL_BULK_MILK_PURCHASE_INVOICE_head.Comp_Code as [Comp Code],case when isnull(TSPL_BULK_MILK_PURCHASE_INVOICE_head.isSRNTradeInvoice,0)=0 then 'BULK MILK SRN' else 'BULK MILK SRN TRADE' end  as DocType,stuff((select ',' + isnull(SRN_NO ,'') from tspl_Bulk_milk_purchase_Invoice_Detail  where tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO  =TSPL_BULK_MILK_PURCHASE_INVOICE_head .DOC_NO   for xml path ('')),1,1,'' )as  [SRN No.] From TSPL_BULK_MILK_PURCHASE_INVOICE_head  left outer join tspl_vendor_master on TSPL_BULK_MILK_PURCHASE_INVOICE_head.vendor_code=tspl_vendor_master.vendor_code left outer join tspl_location_master on tspl_bulk_milk_purchase_invoice_head.loc_code=tspl_location_master.location_code left outer join TSPL_BULK_MILK_PURCHASE_RETURN_HEAD on TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Invoice_No=tspl_Bulk_milk_purchase_Invoice_head.DOC_NO  "
            str = clsCommon.ShowSelectForm("PBMINVFND", qry, "DocNo", "  tspl_Bulk_milk_purchase_Invoice_head.isPosted =1 and tspl_Bulk_milk_purchase_Invoice_head.DOC_NO not in (select Invoice_No from TSPL_BULK_MILK_PURCHASE_RETURN_HEAD) and  tspl_Bulk_milk_purchase_Invoice_head.DOC_NO not in( select TSPL_BULK_MILK_PURCHASE_Invoice_HEAD.DOC_NO from TSPL_PAYMENT_DETAIL left outer join TSPL_VENDOR_INVOICE_HEAD  on TSPL_PAYMENT_DETAIL.Document_No =TSPL_VENDOR_INVOICE_HEAD.Document_No left outer join TSPL_BULK_MILK_PURCHASE_Invoice_HEAD on TSPL_BULK_MILK_PURCHASE_Invoice_HEAD.DOC_NO=TSPL_VENDOR_INVOICE_HEAD.Against_BulkMillkPurchaseInvoice_No where TSPL_BULK_MILK_PURCHASE_Invoice_HEAD.DOC_NO is not null and TSPL_PAYMENT_DETAIL.Post=1) ", curcode, "DocNo", isButtonClicked)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return str
    End Function


End Class

Public Class clsMilkPurchaseReturnDetail
    Public Pur_Return_No As String = Nothing
    Public Invoice_No As String = Nothing
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
    Public CHAMBER_DESC As String = Nothing



    Public Shared Function saveData(ByVal arrObj As List(Of clsMilkPurchaseReturnDetail), ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim issaved As Boolean = True
        Try
            Dim coll As Hashtable
            If arrObj IsNot Nothing Then
                Dim qry As String = "delete from TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL where Pur_Return_No='" & strDocNo & "'"
                issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                For Each obj As clsMilkPurchaseReturnDetail In arrObj
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Pur_Return_No", obj.Pur_Return_No)
                    clsCommon.AddColumnsForChange(coll, "Invoice_No", obj.Invoice_No)
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
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return issaved
    End Function

    Public Shared Function getData(ByVal strDocNo As String, Optional trans As SqlTransaction = Nothing) As List(Of clsMilkPurchaseReturnDetail)
        Dim arrObj As List(Of clsMilkPurchaseReturnDetail) = Nothing
        Try
            Dim obj As clsMilkPurchaseReturnDetail = Nothing
            Dim qry As String = "select * from TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL where Pur_Return_No='" & strDocNo & "' order by sl_no"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of clsMilkPurchaseReturnDetail)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New clsMilkPurchaseReturnDetail()
                    obj.Pur_Return_No = clsCommon.myCstr(dt.Rows(i)("Pur_Return_No"))
                    obj.Invoice_No = clsCommon.myCstr(dt.Rows(i)("Invoice_No"))
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
                    obj.price_code = clsCommon.myCstr(dt.Rows(i)("Price_code"))
                    obj.CHAMBER_DESC = clsCommon.myCstr(dt.Rows(i)("CHAMBER_DESC"))
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
            Dim qry As String = "delete from TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL where Pur_Return_No='" & strDocNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isDeleted
    End Function

    Public Shared Function geInvoicetData(ByVal strDocNo As String, Optional trans As SqlTransaction = Nothing) As List(Of clsMilkPurchaseReturnDetail)
        Dim arrObj As List(Of clsMilkPurchaseReturnDetail) = Nothing
        Try
            Dim obj As clsMilkPurchaseReturnDetail = Nothing
            Dim qry As String = "select * from TSPL_BULK_MILK_PURCHASE_INVOICE_detail where doc_No='" & strDocNo & "' order by sl_no"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of clsMilkPurchaseReturnDetail)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New clsMilkPurchaseReturnDetail()
                    obj.Invoice_No = clsCommon.myCstr(dt.Rows(i)("DOC_NO"))
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
                    obj.price_code = clsCommon.myCstr(dt.Rows(i)("Price_code"))
                    obj.CHAMBER_DESC = clsCommon.myCstr(dt.Rows(i)("CHAMBER_DESC"))
                    arrObj.Add(obj)
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return arrObj
    End Function


End Class
