Imports System.Data.SqlClient

Public Class clsMakeSavingPayment
#Region "Variables"
    Public Doc_Code As String = Nothing
    Public Doc_Date As DateTime
    Public Remarks As String = Nothing
    Public Filter_From_Date As Date
    Public Filter_To_Date As Date
    Public Filter_arrVSP As ArrayList = Nothing '' not in table
    Public Filter_arrBMC As ArrayList = Nothing '' not in table
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending

    Public arr As List(Of clsMakeSavingPaymentDetail) = Nothing

#End Region

    Public Function SaveData(ByVal obj As clsMakeSavingPayment, ByVal isNewEntry As Boolean) As Boolean
        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, tran, "")
            tran.Commit()
        Catch ex As Exception
            tran.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function SaveData(ByVal obj As clsMakeSavingPayment, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction, ByVal strVoucherNoRecreateOnly As String) As Boolean
        Dim qry As String = "delete from TSPL_MAKE_SAVING_PAYMENT_SAVING where Doc_Code='" + obj.Doc_Code + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        qry = "delete from TSPL_MAKE_SAVING_PAYMENT_DETAIL where Doc_Code='" + obj.Doc_Code + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        qry = "delete from TSPL_MAKE_SAVING_PAYMENT_DCS_SALE where Doc_Code='" + obj.Doc_Code + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        qry = "delete from TSPL_MAKE_SAVING_PAYMENT_DEDUCTION where Doc_Code='" + obj.Doc_Code + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        If obj.arr.Count <= 0 Then
            Throw New Exception("Please fill at one invoice details")
        End If

        If isNewEntry Then
            obj.Doc_Code = clsERPFuncationality.GetNextCode(trans, obj.Doc_Date, clsDocType.MakeSavingPayment, clsDocTransactionType.Transaction, "")
        End If
        If (clsCommon.myLen(obj.Doc_Date) <= 0) Then
            Throw New Exception("Error in Document Code Generation")
        End If

        Dim coll As New Hashtable()
        clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.Doc_Date, "dd/MMM/yyyy hh:mm:ss tt"))
        clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
        clsCommon.AddColumnsForChange(coll, "Filter_From_Date", clsCommon.GetPrintDate(obj.Filter_From_Date, "dd/MMM/yyyy"))
        clsCommon.AddColumnsForChange(coll, "Filter_To_Date", clsCommon.GetPrintDate(obj.Filter_To_Date, "dd/MMM/yyyy"))
        clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
        clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
        If isNewEntry Then
            clsCommon.AddColumnsForChange(coll, "Doc_Code", obj.Doc_Code)
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MAKE_SAVING_PAYMENT", OMInsertOrUpdate.Insert, "", trans)
        Else
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MAKE_SAVING_PAYMENT", OMInsertOrUpdate.Update, "Doc_Code='" + obj.Doc_Code + "'", trans)
        End If
        clsMakeSavingPaymentDetail.SaveData(obj.Doc_Code, obj.arr, trans)
        clsCommonFunctionality.SaveHistoryData(EnumSaveType.History, objCommonVar.CurrentUserCode, obj.Doc_Code, "TSPL_MAKE_SAVING_PAYMENT", "Doc_Code", "TSPL_MAKE_SAVING_PAYMENT_DETAIL", "Doc_Code", "TSPL_MAKE_SAVING_PAYMENT_SAVING", "Doc_Code", "TSPL_MAKE_SAVING_PAYMENT_DCS_SALE", "Doc_Code", "", "", "", "", "", "", trans)
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsMakeSavingPayment
        Dim obj As clsMakeSavingPayment = Nothing
        Dim qry As String = "SELECT TSPL_MAKE_SAVING_PAYMENT.* from TSPL_MAKE_SAVING_PAYMENT where 2=2"
        Dim whrClas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_MAKE_SAVING_PAYMENT.Doc_Code = (select MIN(Doc_Code) from TSPL_MAKE_SAVING_PAYMENT where 1=1 " + whrClas + ")"
            Case NavigatorType.Last
                qry += " and TSPL_MAKE_SAVING_PAYMENT.Doc_Code = (select Max(Doc_Code) from TSPL_MAKE_SAVING_PAYMENT where 1=1 " + whrClas + ")"
            Case NavigatorType.Next
                qry += " and TSPL_MAKE_SAVING_PAYMENT.Doc_Code = (select Min(Doc_Code) from TSPL_MAKE_SAVING_PAYMENT where Doc_Code>'" + strDocNo + "' " + whrClas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_MAKE_SAVING_PAYMENT.Doc_Code = (select Max(Doc_Code) from TSPL_MAKE_SAVING_PAYMENT where Doc_Code<'" + strDocNo + "' " + whrClas + ")"
            Case NavigatorType.Current
                qry += " and TSPL_MAKE_SAVING_PAYMENT.Doc_Code = '" + strDocNo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsMakeSavingPayment()
            obj.Doc_Code = clsCommon.myCstr(dt.Rows(0)("Doc_Code"))
            obj.Doc_Date = clsCommon.myCDate(dt.Rows(0)("Doc_Date"))
            obj.Status = IIf(clsCommon.myCDecimal(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Posted, ERPTransactionStatus.Pending)
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Filter_From_Date = clsCommon.myCDate(dt.Rows(0)("Filter_From_Date"))
            obj.Filter_To_Date = clsCommon.myCDate(dt.Rows(0)("Filter_To_Date"))
            obj.arr = clsMakeSavingPaymentDetail.GetData(obj.Doc_Code, trans, obj.Filter_arrVSP, obj.Filter_arrBMC)
        End If
        Return obj
    End Function

    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(strDocNo, tran)
            tran.Commit()
        Catch ex As Exception
            tran.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        If Not (clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.DoNotIncludeIncentiveInMilkPurchaseInvoice, clsFixedParameterCode.DoNotIncludeIncentiveInMilkPurchaseInvoice, trans)) = 1) Then
            Throw New Exception("Incentive entry is not for you")
        End If
        If clsCommon.myLen(strDocNo) <= 0 Then
            Throw New Exception("Please provide document no to post")
        End If

        Dim obj As clsMakeSavingPayment = clsMakeSavingPayment.GetData(strDocNo, NavigatorType.Current, trans)
        If obj.Status = ERPTransactionStatus.Posted Then
            Throw New Exception("Already posted transaction - " + obj.Doc_Code)
        End If
        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_MAKE_SAVING_PAYMENT", "Doc_Code", "TSPL_MAKE_SAVING_PAYMENT_DETAIL", "Doc_Code", trans)
        'For Each objtr As clsMakeSavingPaymentDetail In obj.arr
        '    CreateCreditNote(obj, objtr, trans)
        'Next


        Dim coll As New Hashtable()
        clsCommon.AddColumnsForChange(coll, "Post_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
        clsCommon.AddColumnsForChange(coll, "Post_By", objCommonVar.CurrentUserCode)
        clsCommon.AddColumnsForChange(coll, "Status", 1)
        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MAKE_SAVING_PAYMENT", OMInsertOrUpdate.Update, "Doc_Code='" + strDocNo + "'", trans)
        Return True
    End Function

    ''Shared Function CreateCreditNote(ByVal objHead As clsMakeSavingPayment, ByVal objtr As clsMakeSavingPaymentDetail, ByVal trans As SqlTransaction) As Boolean
    ''    ''ERO/12/07/18-000376 by balwinder on -01/10/2018
    ''    Dim objVendorInvHead As New clsVedorInvoiceHead()

    ''    'objVendorInvHead.Document_No = txtDocNo.Value'ToBeGenerated
    ''    objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(objHead.Doc_Date, "dd/MMM/yyyy")
    ''    objVendorInvHead.Vendor_Code = objtr.DCS_Code
    ''    objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(objtr.DCS_Code, trans)
    ''    objVendorInvHead.Vendor_Invoice_No = "" ''No Need to send vendor invoice no because it is of debit/Credit note type
    ''    objVendorInvHead.Invoice_Type = "AP"
    ''    objVendorInvHead.Vendor_Invoice_Date = objVendorInvHead.Invoice_Entry_Date
    ''    objVendorInvHead.loc_code = clsLocation.GetSegmentCode(objHead.MCC_Code, trans) 'obj.MCC_CODE
    ''    'objVendorInvHead.Irregular_loc_code = obj.Irregular_MCC_CODE
    ''    objVendorInvHead.Description = "AP Credit Note Against Incentive Entry:" + objtr.Doc_Code + "[" + objtr.PK_ID + "]"
    ''    'objVendorInvHead.PROJECT_ID = 1 'obj.PROJECT_ID
    ''    objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
    ''    If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
    ''        Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
    ''    End If
    ''    objVendorInvHead.Document_Type = "C" ''For Purchase Invoice Type
    ''    ''objVendorInvHead.PO_Number = obj.p
    ''    '' ''added by priti
    ''    objVendorInvHead.RefDocType = "COM-INC" ''Credit Note of Month-Incetive
    ''    objVendorInvHead.RefDocNo = objtr.PK_ID
    ''    'objVendorInvHead.Ref_SNo = ""
    ''    '' '' priti ends here
    ''    'objVendorInvHead.Order_No = txtOrderNo.Text
    ''    ' objVendorInvHead.Total_Tax = 0
    ''    objVendorInvHead.On_Hold = False
    ''    'Dim srndate As String = ""
    ''    'Dim srncode As String = ""
    ''    'Dim Vlc_Code As String = ""
    ''    'Dim Vlc_Name As String = ""
    ''    'For Each objTr As clsMilkPurchaseInvoiceMCCDetail In obj.ObjList
    ''    '    If clsCommon.myLen(objTr.SRN_CODE) > 0 Then
    ''    '        Dim query As String = "select doc_date,vd.VLC_Code,VLC_Name from TSPL_Milk_SRN_HEAD sh inner join TSPL_VLC_MASTER_HEAD vd on sh.VLC_CODE=vd.VLC_Code where DOc_Code ='" + objTr.SRN_CODE + "' "
    ''    '        Dim Dt_SRN As DataTable = clsDBFuncationality.GetDataTable(query, trans)
    ''    '        srndate = IIf(srndate = "", clsCommon.myCDate(CStr(Dt_SRN.Rows(0).Item("Doc_Date")), "dd/MMM/yyyy"), srndate & "," & clsCommon.myCDate(CStr(Dt_SRN.Rows(0).Item("Doc_Date")), "dd/MMM/yyyy"))
    ''    '        srncode = IIf(srncode = "", objTr.SRN_CODE, srncode & "," & objTr.SRN_CODE)
    ''    '        Vlc_Code = IIf(Vlc_Code = "", Dt_SRN.Rows(0).Item("VLC_Code").ToString, Vlc_Code & "," & Dt_SRN.Rows(0).Item("VLC_Code").ToString)
    ''    '        Vlc_Name = IIf(Vlc_Name = "", Dt_SRN.Rows(0).Item("VLC_Name").ToString, Vlc_Name & "," & Dt_SRN.Rows(0).Item("VLC_name").ToString)
    ''    '    End If
    ''    'Next

    ''    'objVendorInvHead.Description = "VSP : " + obj.DCS_Code + "/" + vendor_name + "VLC : " + Vlc_Code + "/" + Vlc_Name + " .Against PI Invoice No " + obj.DOC_CODE + "-" + srncode + "-" + srndate
    ''    'objVendorInvHead.Tax_Calculation_Type = Nothing
    ''    'objVendorInvHead.Tax_Group = Nothing
    ''    'If (clsCommon.myLen(obj.TAX1) > 0) Then
    ''    '    objVendorInvHead.TAX1 = obj.TAX1
    ''    '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX1, trans) Then
    ''    '        objVendorInvHead.TAX1_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX1, trans)
    ''    '        objVendorInvHead.TAX1_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX1_GLAC, obj.MCC_CODE, trans)
    ''    '    End If
    ''    '    objVendorInvHead.TAX1_Rate = obj.TAX1_Rate
    ''    '    objVendorInvHead.Tax1_BAmount = obj.TAX1_Base_Amt
    ''    '    objVendorInvHead.TAX1_Amt = obj.TAX1_Amt
    ''    'End If
    ''    'If (clsCommon.myLen(obj.TAX2) > 0) Then
    ''    '    objVendorInvHead.TAX2 = obj.TAX2
    ''    '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX2, trans) Then
    ''    '        objVendorInvHead.TAX2_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX2, trans)
    ''    '        objVendorInvHead.TAX2_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX2_GLAC, obj.MCC_CODE, trans)
    ''    '    End If
    ''    '    objVendorInvHead.TAX2_Rate = obj.TAX2_Rate
    ''    '    objVendorInvHead.Tax2_BAmount = obj.TAX2_Base_Amt
    ''    '    objVendorInvHead.TAX2_Amt = obj.TAX2_Amt
    ''    'End If
    ''    'If (clsCommon.myLen(obj.TAX3) > 0) Then
    ''    '    objVendorInvHead.TAX3 = obj.TAX3
    ''    '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX3, trans) Then
    ''    '        objVendorInvHead.TAX3_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX3, trans)
    ''    '        objVendorInvHead.TAX3_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX3_GLAC, obj.MCC_CODE, trans)
    ''    '    End If
    ''    '    objVendorInvHead.TAX3_Rate = obj.TAX3_Rate
    ''    '    objVendorInvHead.Tax3_BAmount = obj.TAX3_Base_Amt
    ''    '    objVendorInvHead.TAX3_Amt = obj.TAX3_Amt
    ''    'End If
    ''    'If (clsCommon.myLen(obj.TAX4) > 0) Then
    ''    '    objVendorInvHead.TAX4 = obj.TAX4
    ''    '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX4, trans) Then
    ''    '        objVendorInvHead.TAX4_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX4, trans)
    ''    '        objVendorInvHead.TAX4_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX4_GLAC, obj.MCC_CODE, trans)
    ''    '    End If
    ''    '    objVendorInvHead.TAX4_Rate = obj.TAX4_Rate
    ''    '    objVendorInvHead.Tax4_BAmount = obj.TAX4_Base_Amt
    ''    '    objVendorInvHead.TAX4_Amt = obj.TAX4_Amt
    ''    'End If
    ''    'If (clsCommon.myLen(obj.TAX5) > 0) Then
    ''    '    objVendorInvHead.TAX5 = obj.TAX5
    ''    '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX5, trans) Then
    ''    '        objVendorInvHead.TAX5_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX5, trans)
    ''    '        objVendorInvHead.TAX5_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX5_GLAC, obj.MCC_CODE, trans)

    ''    '    End If
    ''    '    objVendorInvHead.TAX5_Rate = obj.TAX5_Rate
    ''    '    objVendorInvHead.Tax5_BAmount = obj.TAX5_Base_Amt
    ''    '    objVendorInvHead.TAX5_Amt = obj.TAX5_Amt
    ''    'End If
    ''    'If (clsCommon.myLen(obj.TAX6) > 0) Then
    ''    '    objVendorInvHead.TAX6 = obj.TAX6
    ''    '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX6, trans) Then
    ''    '        objVendorInvHead.TAX6_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX6, trans)
    ''    '        objVendorInvHead.TAX6_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX6_GLAC, obj.MCC_CODE, trans)
    ''    '    End If
    ''    '    objVendorInvHead.TAX6_Rate = obj.TAX6_Rate
    ''    '    objVendorInvHead.Tax6_BAmount = obj.TAX6_Base_Amt
    ''    '    objVendorInvHead.TAX6_Amt = obj.TAX6_Amt
    ''    'End If
    ''    'If (clsCommon.myLen(obj.TAX7) > 0) Then
    ''    '    objVendorInvHead.TAX7 = obj.TAX7
    ''    '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX7, trans) Then
    ''    '        objVendorInvHead.TAX7_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX7, trans)
    ''    '        objVendorInvHead.TAX7_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX7_GLAC, obj.MCC_CODE, trans)

    ''    '    End If
    ''    '    objVendorInvHead.TAX7_Rate = obj.TAX7_Rate
    ''    '    objVendorInvHead.Tax7_BAmount = obj.TAX7_Base_Amt
    ''    '    objVendorInvHead.TAX7_Amt = obj.TAX7_Amt
    ''    'End If
    ''    'If (clsCommon.myLen(obj.TAX8) > 0) Then
    ''    '    objVendorInvHead.TAX8 = obj.TAX8
    ''    '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX8, trans) Then
    ''    '        objVendorInvHead.TAX8_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX8, trans)
    ''    '        objVendorInvHead.TAX8_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX8_GLAC, obj.MCC_CODE, trans)
    ''    '    End If
    ''    '    objVendorInvHead.TAX8_Rate = obj.TAX8_Rate
    ''    '    objVendorInvHead.Tax8_BAmount = obj.TAX8_Base_Amt
    ''    '    objVendorInvHead.TAX8_Amt = obj.TAX8_Amt
    ''    'End If
    ''    'If (clsCommon.myLen(obj.TAX9) > 0) Then
    ''    '    objVendorInvHead.TAX9 = obj.TAX9
    ''    '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX9, trans) Then
    ''    '        objVendorInvHead.TAX9_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX9, trans)
    ''    '        objVendorInvHead.TAX9_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX9_GLAC, obj.MCC_CODE, trans)
    ''    '    End If
    ''    '    objVendorInvHead.TAX9_Rate = obj.TAX9_Rate
    ''    '    objVendorInvHead.Tax9_BAmount = obj.TAX9_Base_Amt
    ''    '    objVendorInvHead.TAX9_Amt = obj.TAX9_Amt
    ''    'End If
    ''    'If (clsCommon.myLen(obj.TAX10) > 0) Then
    ''    '    objVendorInvHead.TAX10 = obj.TAX10
    ''    '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX10, trans) Then
    ''    '        objVendorInvHead.TAX10_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX10, trans)
    ''    '        objVendorInvHead.TAX10_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX10_GLAC, obj.MCC_CODE, trans)
    ''    '    End If
    ''    '    objVendorInvHead.TAX10_Rate = obj.TAX10_Rate
    ''    '    objVendorInvHead.Tax10_BAmount = obj.TAX10_Base_Amt
    ''    '    objVendorInvHead.TAX10_Amt = obj.TAX10_Amt
    ''    'End If

    ''    'objVendorInvHead.Terms_Code = obj.Terms_Code
    ''    'objVendorInvHead.Terms_Description = obj.TermsName
    ''    objVendorInvHead.Due_Date = objVendorInvHead.Invoice_Entry_Date

    ''    'objVendorInvHead.Against_POInvoice_No = obj.DOC_CODE
    ''    'objVendorInvHead.Against_MillkPurchaseInvoice_No = obj.DOC_CODE

    ''    Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Incentive_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
    ''    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    ''        objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
    ''        objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, objHead.MCC_Code, trans)

    ''    End If
    ''    If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
    ''        Throw New Exception("Please set the vendor payable Account")
    ''    End If

    ''    'objVendorInvHead.Total_Add_Charge = obj.Total_Add_Charge

    ''    'objVendorInvHead.Add_Charge_Code1 = obj.Add_Charge_Code1
    ''    'objVendorInvHead.Add_Charge_Name1 = obj.Add_Charge_Name1
    ''    'objVendorInvHead.Add_Charge_Amt1 = obj.Add_Charge_Amt1

    ''    'objVendorInvHead.Add_Charge_Code2 = obj.Add_Charge_Code2
    ''    'objVendorInvHead.Add_Charge_Name2 = obj.Add_Charge_Name2
    ''    'objVendorInvHead.Add_Charge_Amt2 = obj.Add_Charge_Amt2

    ''    'objVendorInvHead.Add_Charge_Code3 = obj.Add_Charge_Code3
    ''    'objVendorInvHead.Add_Charge_Name3 = obj.Add_Charge_Name3
    ''    'objVendorInvHead.Add_Charge_Amt3 = obj.Add_Charge_Amt3

    ''    'objVendorInvHead.Add_Charge_Code4 = obj.Add_Charge_Code4
    ''    'objVendorInvHead.Add_Charge_Name4 = obj.Add_Charge_Name4
    ''    'objVendorInvHead.Add_Charge_Amt4 = obj.Add_Charge_Amt4

    ''    'objVendorInvHead.Add_Charge_Code5 = obj.Add_Charge_Code5
    ''    'objVendorInvHead.Add_Charge_Name5 = obj.Add_Charge_Name5
    ''    'objVendorInvHead.Add_Charge_Amt5 = obj.Add_Charge_Amt5

    ''    'objVendorInvHead.Add_Charge_Code6 = obj.Add_Charge_Code6
    ''    'objVendorInvHead.Add_Charge_Name6 = obj.Add_Charge_Name6
    ''    'objVendorInvHead.Add_Charge_Amt6 = obj.Add_Charge_Amt6

    ''    'objVendorInvHead.Add_Charge_Code7 = obj.Add_Charge_Code7
    ''    'objVendorInvHead.Add_Charge_Name7 = obj.Add_Charge_Name7
    ''    'objVendorInvHead.Add_Charge_Amt7 = obj.Add_Charge_Amt7

    ''    'objVendorInvHead.Add_Charge_Code8 = obj.Add_Charge_Code8
    ''    'objVendorInvHead.Add_Charge_Name8 = obj.Add_Charge_Name8
    ''    'objVendorInvHead.Add_Charge_Amt8 = obj.Add_Charge_Amt8

    ''    'objVendorInvHead.Add_Charge_Code9 = obj.Add_Charge_Code9
    ''    'objVendorInvHead.Add_Charge_Name9 = obj.Add_Charge_Name9
    ''    'objVendorInvHead.Add_Charge_Amt9 = obj.Add_Charge_Amt9

    ''    'objVendorInvHead.Add_Charge_Code10 = obj.Add_Charge_Code10
    ''    'objVendorInvHead.Add_Charge_Name10 = obj.Add_Charge_Name10
    ''    'objVendorInvHead.Add_Charge_Amt10 = obj.Add_Charge_Amt10


    ''    objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
    ''    Dim ii As Integer = 0
    ''    Dim isFirstTime As Boolean = True
    ''    ' Dim strFirstItemCode As String = GetFirstItemCode(obj.ObjList)
    ''    'objVendorInvHead.Empty_Amount = obj.Tot_Empty_Amount
    ''    objVendorInvHead.Total_Landed_Amt = 0

    ''    objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()


    ''    ''Set AP Invvoice Detail Table

    ''    ii = ii + 1
    ''    Dim objVendorInvDetail As New clsVedorInvoiceDetail()
    ''    objVendorInvDetail.Detail_Line_No = ii

    ''    objVendorInvDetail.GL_Account_Code = clsCommon.myCstr(dt.Rows(0)("Incentive_ACCOUNT"))
    ''    If clsCommon.myLen(objVendorInvDetail.GL_Account_Code) <= 0 Then
    ''        Throw New Exception("Please set Incentive Account for vendor account set :" + objVendorInvHead.Account_Set)
    ''    End If
    ''    objVendorInvDetail.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvDetail.GL_Account_Code, objHead.MCC_Code, trans)
    ''    objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(objVendorInvDetail.GL_Account_Code, trans)


    ''    objVendorInvDetail.Amount = objtr.Amount
    ''    objVendorInvDetail.Discount_Per = 0
    ''    objVendorInvDetail.Discount = 0
    ''    objVendorInvDetail.Amount_less_Discount = objtr.Amount
    ''    objVendorInvDetail.Total_Tax = 0
    ''    objVendorInvDetail.Total_Amount = objtr.Amount
    ''    objVendorInvDetail.Landed_Amount = objtr.Amount
    ''    ''End of Set AP Invvoice Detail Table

    ''    If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
    ''        objVendorInvHead.Arr.Add(objVendorInvDetail)
    ''    End If

    ''    ''Set AP Invvoice Header Table
    ''    objVendorInvHead.Total_Landed_Amt += objtr.Amount
    ''    objVendorInvHead.Discount_Base += objtr.Amount
    ''    objVendorInvHead.Discount_Amount += 0
    ''    objVendorInvHead.Amount_Less_Discount += objtr.Amount
    ''    objVendorInvHead.Document_Total += objtr.Amount
    ''    objVendorInvHead.Balance_Amt += objtr.Amount
    ''    ''End of Set AP Invvoice Header Table

    ''    objVendorInvHead.Empty_Amount = 0 'obj.Tot_Empty_Amount
    ''    If objVendorInvHead.Empty_Amount > 0 Then
    ''        If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
    ''            Throw New Exception("Please set Inventory Control Empties")
    ''        End If
    ''        objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
    ''    End If

    ''    If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
    ''        Throw New Exception("No GL Account Found For AP Invoice")
    ''    End If
    ''    ''multicurrency
    ''    'objVendorInvHead.CURRENCY_CODE = obj.CURRENCY_CODE
    ''    'objVendorInvHead.ConvRate = 1
    ''    objVendorInvHead.ApplicableFrom = objVendorInvHead.Invoice_Entry_Date
    ''    ''end multicurrency
    ''    ''Set AP Invvoice Detail Table

    ''    objVendorInvHead.SaveData(objVendorInvHead, True, trans)
    ''    clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans)

    ''    Return True
    ''End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim obj As clsMakeSavingPayment = clsMakeSavingPayment.GetData(strCode, NavigatorType.Current, trans)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Doc_Code) > 0) Then
            Try
                If (obj.Status = ERPTransactionStatus.Posted) Then
                    Throw New Exception("Already Posted")
                End If

                clsCommonFunctionality.SaveHistoryData(EnumSaveType.History, objCommonVar.CurrentUserCode, obj.Doc_Code, "TSPL_MAKE_SAVING_PAYMENT", "Doc_Code", "TSPL_MAKE_SAVING_PAYMENT_DETAIL", "Doc_Code", "TSPL_MAKE_SAVING_PAYMENT_SAVING", "Doc_Code", "TSPL_MAKE_SAVING_PAYMENT_DCS_SALE", "Doc_Code", "", "", "", "", "", "", trans)
                clsCommonFunctionality.SaveHistoryData(EnumSaveType.Delete, objCommonVar.CurrentUserCode, obj.Doc_Code, "TSPL_MAKE_SAVING_PAYMENT", "Doc_Code", "TSPL_MAKE_SAVING_PAYMENT_DETAIL", "Doc_Code", "TSPL_MAKE_SAVING_PAYMENT_SAVING", "Doc_Code", "TSPL_MAKE_SAVING_PAYMENT_DCS_SALE", "Doc_Code", "", "", "", "", "", "", trans)

                Dim qry As String = "delete from TSPL_MAKE_SAVING_PAYMENT_SAVING where Doc_Code='" + obj.Doc_Code + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_MAKE_SAVING_PAYMENT_DETAIL where Doc_Code='" + obj.Doc_Code + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_MAKE_SAVING_PAYMENT_DCS_SALE where Doc_Code='" + obj.Doc_Code + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_MAKE_SAVING_PAYMENT_DEDUCTION where Doc_Code='" + obj.Doc_Code + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_MAKE_SAVING_PAYMENT where Doc_Code='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                trans.Commit()
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
        Return True
    End Function

End Class

Public Class clsMakeSavingPaymentDetail
#Region "Variables"
    Public PK_ID As Integer
    Public Doc_Code As String
    Public DCS_Code As String
    Public DCS_Name As String ''Not a table Column
    Public DCS_UploaderNo As String ''Not a table Column
    Public Saving_Amt As Decimal
    Public DCS_Sale_Amt As Decimal
    Public Deduction_Amt As Decimal
    Public Payable_Amt As Decimal
    Public ArrDCSSaving As List(Of clsMakeSavingPaymentSaving)
    Public ArrDCSSale As List(Of clsMakeSavingPaymentDCSSale)
    Public ArrDCSDeduction As List(Of clsMakeSavingPaymentDeduction)
#End Region
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsMakeSavingPaymentDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsMakeSavingPaymentDetail In Arr
                If obj.Payable_Amt < 0 Then
                    Throw New Exception("Payable amount should be greater than zero of DCS [" + obj.DCS_Code + "]")
                End If
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Doc_Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "DCS_Code", obj.DCS_Code)
                clsCommon.AddColumnsForChange(coll, "Saving_Amt", obj.Saving_Amt)
                clsCommon.AddColumnsForChange(coll, "DCS_Sale_Amt", obj.DCS_Sale_Amt)
                clsCommon.AddColumnsForChange(coll, "Deduction_Amt", obj.Deduction_Amt)
                clsCommon.AddColumnsForChange(coll, "Payable_Amt", obj.Payable_Amt)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MAKE_SAVING_PAYMENT_DETAIL", OMInsertOrUpdate.Insert, "", trans)

                Dim intPKID As Integer = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select SCOPE_IDENTITY()", trans))
                clsMakeSavingPaymentSaving.SaveData(strDocNo, intPKID, obj.ArrDCSSaving, trans)
                clsMakeSavingPaymentDCSSale.SaveData(strDocNo, intPKID, obj.ArrDCSSale, trans)
                clsMakeSavingPaymentDeduction.SaveData(strDocNo, intPKID, obj.ArrDCSDeduction, trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal trans As SqlTransaction, ByRef arrVSP As ArrayList, ByRef arrBMC As ArrayList) As List(Of clsMakeSavingPaymentDetail)
        arrVSP = New ArrayList
        arrBMC = New ArrayList
        Dim arr As List(Of clsMakeSavingPaymentDetail) = Nothing
        Dim qry As String = "select TSPL_MAKE_SAVING_PAYMENT_DETAIL.*,TSPL_VENDOR_MASTER.Vendor_Name as DCS_Name, TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VLC_MASTER_HEAD.MCC
from TSPL_MAKE_SAVING_PAYMENT_DETAIL 
left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_MAKE_SAVING_PAYMENT_DETAIL.DCS_Code 
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_MAKE_SAVING_PAYMENT_DETAIL.DCS_Code
where Doc_Code='" + strDocNo + "' order by PK_ID"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsMakeSavingPaymentDetail)
            For Each dr As DataRow In dt.Rows
                Dim obj As New clsMakeSavingPaymentDetail
                obj.PK_ID = clsCommon.myCDecimal(dr("PK_ID"))
                obj.Doc_Code = clsCommon.myCstr(dr("Doc_Code"))
                obj.DCS_Code = clsCommon.myCstr(dr("DCS_Code"))
                obj.DCS_UploaderNo = clsCommon.myCstr(dr("VLC_Code_VLC_Uploader"))
                obj.DCS_Name = clsCommon.myCstr(dr("DCS_Name"))
                obj.Saving_Amt = clsCommon.myCDecimal(dr("Saving_Amt"))
                obj.DCS_Sale_Amt = clsCommon.myCDecimal(dr("DCS_Sale_Amt"))
                obj.Deduction_Amt = clsCommon.myCDecimal(dr("Deduction_Amt"))
                obj.Payable_Amt = clsCommon.myCDecimal(dr("Payable_Amt"))
                obj.ArrDCSSaving = clsMakeSavingPaymentSaving.GetData(obj.Doc_Code, obj.PK_ID, trans)
                obj.ArrDCSSale = clsMakeSavingPaymentDCSSale.GetData(obj.Doc_Code, obj.PK_ID, trans)
                obj.ArrDCSDeduction = clsMakeSavingPaymentDeduction.GetData(obj.Doc_Code, obj.PK_ID, trans)

                arr.Add(obj)

                arrVSP.Add(obj.DCS_Code)
                arrBMC.Add(clsCommon.myCstr(dr("MCC")))
            Next
        End If
        Return arr
    End Function
End Class


Public Class clsMakeSavingPaymentSaving
#Region "Variables"
    Public SNo As Integer ''No a Table Column
    Public PK_ID As Integer
    Public Ref_PK_ID As Integer
    Public Doc_Code As String = Nothing
    Public AP_Invoice_No As String = Nothing
    Public Amount As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Ref_PK_ID As Integer, ByVal Arr As List(Of clsMakeSavingPaymentSaving), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim ii As Integer = 1
            For Each obj As clsMakeSavingPaymentSaving In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Ref_PK_ID", Ref_PK_ID)
                clsCommon.AddColumnsForChange(coll, "Doc_Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "AP_Invoice_No", obj.AP_Invoice_No)
                clsCommon.AddColumnsForChange(coll, "AMOUNT", obj.Amount)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MAKE_SAVING_PAYMENT_SAVING", OMInsertOrUpdate.Insert, "", trans)
                ii += 1
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal Ref_PK_ID As Integer, ByVal trans As SqlTransaction) As List(Of clsMakeSavingPaymentSaving)
        Dim arr As List(Of clsMakeSavingPaymentSaving) = Nothing
        Dim qry As String = "select TSPL_MAKE_SAVING_PAYMENT_SAVING.*  from TSPL_MAKE_SAVING_PAYMENT_SAVING  
where TSPL_MAKE_SAVING_PAYMENT_SAVING.Doc_Code='" + strDocNo + "' and Ref_PK_ID='" + clsCommon.myCstr(Ref_PK_ID) + "' order by PK_ID"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsMakeSavingPaymentSaving)
            For Each dr As DataRow In dt.Rows
                Dim obj As New clsMakeSavingPaymentSaving
                obj.SNo = arr.Count + 1
                obj.Doc_Code = clsCommon.myCstr(dr("Doc_Code"))
                obj.AP_Invoice_No = clsCommon.myCstr(dr("AP_Invoice_No"))
                obj.Amount = clsCommon.myCDecimal(dr("AMOUNT"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class

Public Class clsMakeSavingPaymentDCSSale
#Region "Variables"
    Public SNo As Integer ''No a Table Column
    Public PK_ID As Integer
    Public Ref_PK_ID As Integer
    Public Doc_Code As String = Nothing
    Public AR_Invoice_No As String = Nothing
    Public Amount As String = Nothing
    Public Red_Ded_Amount As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Ref_PK_ID As Integer, ByVal Arr As List(Of clsMakeSavingPaymentDCSSale), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim ii As Integer = 1
            For Each obj As clsMakeSavingPaymentDCSSale In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Ref_PK_ID", Ref_PK_ID)
                clsCommon.AddColumnsForChange(coll, "Doc_Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "AR_Invoice_No", obj.AR_Invoice_No)
                clsCommon.AddColumnsForChange(coll, "AMOUNT", obj.Amount)
                clsCommon.AddColumnsForChange(coll, "Red_Ded_Amount", obj.Red_Ded_Amount, True)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MAKE_SAVING_PAYMENT_DCS_SALE", OMInsertOrUpdate.Insert, "", trans)
                ii += 1
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal Ref_PK_ID As Integer, ByVal trans As SqlTransaction) As List(Of clsMakeSavingPaymentDCSSale)
        Dim arr As List(Of clsMakeSavingPaymentDCSSale) = Nothing
        Dim qry As String = "select TSPL_MAKE_SAVING_PAYMENT_DCS_SALE.*  from TSPL_MAKE_SAVING_PAYMENT_DCS_SALE  
where TSPL_MAKE_SAVING_PAYMENT_DCS_SALE.Doc_Code='" + strDocNo + "' and Ref_PK_ID='" + clsCommon.myCstr(Ref_PK_ID) + "' order by PK_ID"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsMakeSavingPaymentDCSSale)
            For Each dr As DataRow In dt.Rows
                Dim obj As New clsMakeSavingPaymentDCSSale
                obj.SNo = arr.Count + 1
                obj.Doc_Code = clsCommon.myCstr(dr("Doc_Code"))
                obj.AR_Invoice_No = clsCommon.myCstr(dr("AR_Invoice_No"))
                obj.Amount = clsCommon.myCDecimal(dr("AMOUNT"))
                obj.Red_Ded_Amount = clsCommon.myCDecimal(dr("Red_Ded_Amount"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class

Public Class clsMakeSavingPaymentDeduction
#Region "Variables"
    Public SNo As Integer ''No a Table Column
    Public PK_ID As Integer
    Public Ref_PK_ID As Integer
    Public Doc_Code As String = Nothing
    Public AP_Invoice_No As String = Nothing
    Public Amount As String = Nothing
    Public Red_Ded_Amount As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Ref_PK_ID As Integer, ByVal Arr As List(Of clsMakeSavingPaymentDeduction), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim ii As Integer = 1
            For Each obj As clsMakeSavingPaymentDeduction In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Ref_PK_ID", Ref_PK_ID)
                clsCommon.AddColumnsForChange(coll, "Doc_Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "AP_Invoice_No", obj.AP_Invoice_No)
                clsCommon.AddColumnsForChange(coll, "AMOUNT", obj.Amount)
                clsCommon.AddColumnsForChange(coll, "Red_Ded_Amount", obj.Red_Ded_Amount, True)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MAKE_SAVING_PAYMENT_DEDUCTION", OMInsertOrUpdate.Insert, "", trans)
                ii += 1
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal Ref_PK_ID As Integer, ByVal trans As SqlTransaction) As List(Of clsMakeSavingPaymentDeduction)
        Dim arr As List(Of clsMakeSavingPaymentDeduction) = Nothing
        Dim qry As String = "select TSPL_MAKE_SAVING_PAYMENT_DEDUCTION.*  from TSPL_MAKE_SAVING_PAYMENT_DEDUCTION  
where TSPL_MAKE_SAVING_PAYMENT_DEDUCTION.Doc_Code='" + strDocNo + "' and Ref_PK_ID='" + clsCommon.myCstr(Ref_PK_ID) + "' order by PK_ID"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsMakeSavingPaymentDeduction)
            For Each dr As DataRow In dt.Rows
                Dim obj As New clsMakeSavingPaymentDeduction
                obj.SNo = arr.Count + 1
                obj.Doc_Code = clsCommon.myCstr(dr("Doc_Code"))
                obj.AP_Invoice_No = clsCommon.myCstr(dr("AP_Invoice_No"))
                obj.Amount = clsCommon.myCDecimal(dr("AMOUNT"))
                obj.Red_Ded_Amount = clsCommon.myCDecimal(dr("Red_Ded_Amount"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class

