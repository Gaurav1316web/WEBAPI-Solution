Imports common
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports Telerik.WinControls
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.IO

Public Class clsIncentiveEntryBySRNHead
#Region "Variables"
    Public Doc_Code As String = Nothing
    Public Doc_Date As DateTime
    Public MCC_Code As String = Nothing
    Public MCC_Name As String = Nothing
    Public Filter_Month As Date
    Public Is_Post As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public arr As List(Of clsIncentiveEntryBySRNDetail) = Nothing
    Public arrVSP As ArrayList = Nothing
#End Region

    Public Function SaveData(ByVal obj As clsIncentiveEntryBySRNHead, ByVal isNewEntry As Boolean) As Boolean
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

    Public Function SaveData(ByVal obj As clsIncentiveEntryBySRNHead, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction, ByVal strVoucherNoRecreateOnly As String) As Boolean
        If clsCommon.myLen(obj.MCC_Code) <= 0 Then
            Throw New Exception("Please first select MCC Code")
        End If
        Dim qry As String = "delete from TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL where Doc_Code='" + obj.Doc_Code + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        If obj.arr.Count <= 0 Then
            Throw New Exception("Please fill at one VSP details")
        End If
        If isNewEntry Then
            obj.Doc_Code = clsERPFuncationality.GetNextCode(trans, obj.Doc_Date, clsDocType.IncentiveEntry, clsDocTransactionType.Transaction, obj.MCC_Code)
        End If
        If (clsCommon.myLen(obj.Doc_Date) <= 0) Then
            Throw New Exception("Error in Document Code Generation")
        End If

        Dim coll As New Hashtable()
        clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.Doc_Date, "dd/MMM/yyyy hh:mm:ss tt"))
        clsCommon.AddColumnsForChange(coll, "MCC_Code", obj.MCC_Code)
        clsCommon.AddColumnsForChange(coll, "Filter_Month", clsCommon.GetPrintDate(obj.Filter_Month, "dd/MMM/yyyy"))
        clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
        clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
        If isNewEntry Then
            clsCommon.AddColumnsForChange(coll, "Doc_Code", obj.Doc_Code)
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD", OMInsertOrUpdate.Insert, "", trans)
        Else
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD", OMInsertOrUpdate.Update, "Doc_Code='" + obj.Doc_Code + "'", trans)
        End If
        clsIncentiveEntryBySRNDetail.SaveData(obj.Doc_Code, obj.Filter_Month, obj.Doc_Date, obj.arr, trans)
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsIncentiveEntryBySRNHead
        Dim obj As clsIncentiveEntryBySRNHead = Nothing
        Dim qry As String = "SELECT TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.*,TSPL_MCC_MASTER.MCC_NAME from TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.MCC_Code where 2=2"
        Dim whrClas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.Doc_Code = (select MIN(Doc_Code) from TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD where 1=1 " + whrClas + ")"
            Case NavigatorType.Last
                qry += " and TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.Doc_Code = (select Max(Doc_Code) from TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD where 1=1 " + whrClas + ")"
            Case NavigatorType.Next
                qry += " and TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.Doc_Code = (select Min(Doc_Code) from TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD where Doc_Code>'" + strDocNo + "' " + whrClas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.Doc_Code = (select Max(Doc_Code) from TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD where Doc_Code<'" + strDocNo + "' " + whrClas + ")"
            Case NavigatorType.Current
                qry += " and TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.Doc_Code = '" + strDocNo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsIncentiveEntryBySRNHead()
            obj.Doc_Code = clsCommon.myCstr(dt.Rows(0)("Doc_Code"))
            obj.Doc_Date = clsCommon.myCDate(dt.Rows(0)("Doc_Date"))
            obj.MCC_Code = clsCommon.myCstr(dt.Rows(0)("MCC_Code"))
            obj.MCC_Name = clsCommon.myCstr(dt.Rows(0)("MCC_Name"))
            obj.Is_Post = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Post")) = 1, ERPTransactionStatus.Posted, ERPTransactionStatus.Pending)
            obj.Filter_Month = clsCommon.myCDate(dt.Rows(0)("Filter_Month"))
            obj.arr = clsIncentiveEntryBySRNDetail.GetData(obj.Doc_Code, trans, obj.arrVSP)
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
        If Not (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DoNotIncludeIncentiveInMilkPurchaseInvoice, clsFixedParameterCode.DoNotIncludeIncentiveInMilkPurchaseInvoice, trans)) = 1) Then
            Throw New Exception("Incentive entry is not for you")
        End If
        If clsCommon.myLen(strDocNo) <= 0 Then
            Throw New Exception("Please provide document no to post")
        End If

        Dim obj As clsIncentiveEntryBySRNHead = clsIncentiveEntryBySRNHead.GetData(strDocNo, NavigatorType.Current, trans)
        If obj.Is_Post = ERPTransactionStatus.Posted Then
            Throw New Exception("Already posted transaction - " + obj.Doc_Code)
        End If

        For Each objtr As clsIncentiveEntryBySRNDetail In obj.arr
            CreateCreditNote(obj, objtr, trans)
            CreateDebitNote(obj, objtr, trans)
        Next


        Dim coll As New Hashtable()
        clsCommon.AddColumnsForChange(coll, "Post_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
        clsCommon.AddColumnsForChange(coll, "Post_By", objCommonVar.CurrentUserCode)
        clsCommon.AddColumnsForChange(coll, "Is_Post", 1)
        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD", OMInsertOrUpdate.Update, "Doc_Code='" + strDocNo + "'", trans)
        Return True
    End Function

    Shared Function CreateDebitNote(ByVal objHead As clsIncentiveEntryBySRNHead, ByVal objtr As clsIncentiveEntryBySRNDetail, ByVal trans As SqlTransaction) As Boolean
        If objtr.Deduction > 0 Then
            Dim objVendorInvHead As New clsVedorInvoiceHead()

            'objVendorInvHead.Document_No = txtDocNo.Value'ToBeGenerated
            objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(objHead.Doc_Date, "dd/MMM/yyyy")
            objVendorInvHead.Vendor_Code = objtr.Vsp_Code
            objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(objtr.Vsp_Code, trans)
            objVendorInvHead.Vendor_Invoice_No = "" ''No Need to send vendor invoice no because it is of debit/Credit note type
            objVendorInvHead.Invoice_Type = "AP"
            objVendorInvHead.Vendor_Invoice_Date = objVendorInvHead.Invoice_Entry_Date
            objVendorInvHead.loc_code = clsLocation.GetSegmentCode(objHead.MCC_Code, trans) 'obj.MCC_CODE
            'objVendorInvHead.Irregular_loc_code = obj.Irregular_MCC_CODE
            objVendorInvHead.Description = "AP Debit Note Against Incentive Entry:" + objtr.Doc_Code + "[" + objtr.TR_Code + "]"
            'objVendorInvHead.PROJECT_ID = 1 'obj.PROJECT_ID
            objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
            If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
            End If
            objVendorInvHead.Document_Type = "D" ''For Purchase Invoice Type
            ''objVendorInvHead.PO_Number = obj.p
            '' ''added by priti
            objVendorInvHead.RefDocType = "DOM-INC" ''Credit Note of Month-Incetive
            objVendorInvHead.RefDocNo = objtr.TR_Code
            'objVendorInvHead.Ref_SNo = ""
            '' '' priti ends here
            'objVendorInvHead.Order_No = txtOrderNo.Text
            ' objVendorInvHead.Total_Tax = 0
            objVendorInvHead.On_Hold = False
            'Dim srndate As String = ""
            'Dim srncode As String = ""
            'Dim Vlc_Code As String = ""
            'Dim Vlc_Name As String = ""
            'For Each objTr As clsMilkPurchaseInvoiceMCCDetail In obj.ObjList
            '    If clsCommon.myLen(objTr.SRN_CODE) > 0 Then
            '        Dim query As String = "select doc_date,vd.VLC_Code,VLC_Name from TSPL_Milk_SRN_HEAD sh inner join TSPL_VLC_MASTER_HEAD vd on sh.VLC_CODE=vd.VLC_Code where DOc_Code ='" + objTr.SRN_CODE + "' "
            '        Dim Dt_SRN As DataTable = clsDBFuncationality.GetDataTable(query, trans)
            '        srndate = IIf(srndate = "", clsCommon.myCDate(CStr(Dt_SRN.Rows(0).Item("Doc_Date")), "dd/MMM/yyyy"), srndate & "," & clsCommon.myCDate(CStr(Dt_SRN.Rows(0).Item("Doc_Date")), "dd/MMM/yyyy"))
            '        srncode = IIf(srncode = "", objTr.SRN_CODE, srncode & "," & objTr.SRN_CODE)
            '        Vlc_Code = IIf(Vlc_Code = "", Dt_SRN.Rows(0).Item("VLC_Code").ToString, Vlc_Code & "," & Dt_SRN.Rows(0).Item("VLC_Code").ToString)
            '        Vlc_Name = IIf(Vlc_Name = "", Dt_SRN.Rows(0).Item("VLC_Name").ToString, Vlc_Name & "," & Dt_SRN.Rows(0).Item("VLC_name").ToString)
            '    End If
            'Next

            'objVendorInvHead.Description = "VSP : " + obj.VSP_CODE + "/" + vendor_name + "VLC : " + Vlc_Code + "/" + Vlc_Name + " .Against PI Invoice No " + obj.DOC_CODE + "-" + srncode + "-" + srndate
            'objVendorInvHead.Tax_Calculation_Type = Nothing
            'objVendorInvHead.Tax_Group = Nothing
            'If (clsCommon.myLen(obj.TAX1) > 0) Then
            '    objVendorInvHead.TAX1 = obj.TAX1
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX1, trans) Then
            '        objVendorInvHead.TAX1_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX1, trans)
            '        objVendorInvHead.TAX1_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX1_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX1_Rate = obj.TAX1_Rate
            '    objVendorInvHead.Tax1_BAmount = obj.TAX1_Base_Amt
            '    objVendorInvHead.TAX1_Amt = obj.TAX1_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX2) > 0) Then
            '    objVendorInvHead.TAX2 = obj.TAX2
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX2, trans) Then
            '        objVendorInvHead.TAX2_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX2, trans)
            '        objVendorInvHead.TAX2_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX2_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX2_Rate = obj.TAX2_Rate
            '    objVendorInvHead.Tax2_BAmount = obj.TAX2_Base_Amt
            '    objVendorInvHead.TAX2_Amt = obj.TAX2_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX3) > 0) Then
            '    objVendorInvHead.TAX3 = obj.TAX3
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX3, trans) Then
            '        objVendorInvHead.TAX3_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX3, trans)
            '        objVendorInvHead.TAX3_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX3_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX3_Rate = obj.TAX3_Rate
            '    objVendorInvHead.Tax3_BAmount = obj.TAX3_Base_Amt
            '    objVendorInvHead.TAX3_Amt = obj.TAX3_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX4) > 0) Then
            '    objVendorInvHead.TAX4 = obj.TAX4
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX4, trans) Then
            '        objVendorInvHead.TAX4_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX4, trans)
            '        objVendorInvHead.TAX4_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX4_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX4_Rate = obj.TAX4_Rate
            '    objVendorInvHead.Tax4_BAmount = obj.TAX4_Base_Amt
            '    objVendorInvHead.TAX4_Amt = obj.TAX4_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX5) > 0) Then
            '    objVendorInvHead.TAX5 = obj.TAX5
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX5, trans) Then
            '        objVendorInvHead.TAX5_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX5, trans)
            '        objVendorInvHead.TAX5_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX5_GLAC, obj.MCC_CODE, trans)

            '    End If
            '    objVendorInvHead.TAX5_Rate = obj.TAX5_Rate
            '    objVendorInvHead.Tax5_BAmount = obj.TAX5_Base_Amt
            '    objVendorInvHead.TAX5_Amt = obj.TAX5_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX6) > 0) Then
            '    objVendorInvHead.TAX6 = obj.TAX6
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX6, trans) Then
            '        objVendorInvHead.TAX6_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX6, trans)
            '        objVendorInvHead.TAX6_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX6_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX6_Rate = obj.TAX6_Rate
            '    objVendorInvHead.Tax6_BAmount = obj.TAX6_Base_Amt
            '    objVendorInvHead.TAX6_Amt = obj.TAX6_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX7) > 0) Then
            '    objVendorInvHead.TAX7 = obj.TAX7
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX7, trans) Then
            '        objVendorInvHead.TAX7_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX7, trans)
            '        objVendorInvHead.TAX7_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX7_GLAC, obj.MCC_CODE, trans)

            '    End If
            '    objVendorInvHead.TAX7_Rate = obj.TAX7_Rate
            '    objVendorInvHead.Tax7_BAmount = obj.TAX7_Base_Amt
            '    objVendorInvHead.TAX7_Amt = obj.TAX7_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX8) > 0) Then
            '    objVendorInvHead.TAX8 = obj.TAX8
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX8, trans) Then
            '        objVendorInvHead.TAX8_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX8, trans)
            '        objVendorInvHead.TAX8_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX8_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX8_Rate = obj.TAX8_Rate
            '    objVendorInvHead.Tax8_BAmount = obj.TAX8_Base_Amt
            '    objVendorInvHead.TAX8_Amt = obj.TAX8_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX9) > 0) Then
            '    objVendorInvHead.TAX9 = obj.TAX9
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX9, trans) Then
            '        objVendorInvHead.TAX9_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX9, trans)
            '        objVendorInvHead.TAX9_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX9_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX9_Rate = obj.TAX9_Rate
            '    objVendorInvHead.Tax9_BAmount = obj.TAX9_Base_Amt
            '    objVendorInvHead.TAX9_Amt = obj.TAX9_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX10) > 0) Then
            '    objVendorInvHead.TAX10 = obj.TAX10
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX10, trans) Then
            '        objVendorInvHead.TAX10_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX10, trans)
            '        objVendorInvHead.TAX10_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX10_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX10_Rate = obj.TAX10_Rate
            '    objVendorInvHead.Tax10_BAmount = obj.TAX10_Base_Amt
            '    objVendorInvHead.TAX10_Amt = obj.TAX10_Amt
            'End If

            'objVendorInvHead.Terms_Code = obj.Terms_Code
            'objVendorInvHead.Terms_Description = obj.TermsName
            objVendorInvHead.Due_Date = objVendorInvHead.Invoice_Entry_Date

            'objVendorInvHead.Against_POInvoice_No = obj.DOC_CODE
            'objVendorInvHead.Against_MillkPurchaseInvoice_No = obj.DOC_CODE

            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Incentive_ACCOUNT,Monthly_Rent_Account,Arrear_Account,Deduction_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, objHead.MCC_Code, trans)

            End If
            If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                Throw New Exception("Please set the vendor payable Account")
            End If

            'objVendorInvHead.Total_Add_Charge = obj.Total_Add_Charge

            'objVendorInvHead.Add_Charge_Code1 = obj.Add_Charge_Code1
            'objVendorInvHead.Add_Charge_Name1 = obj.Add_Charge_Name1
            'objVendorInvHead.Add_Charge_Amt1 = obj.Add_Charge_Amt1

            'objVendorInvHead.Add_Charge_Code2 = obj.Add_Charge_Code2
            'objVendorInvHead.Add_Charge_Name2 = obj.Add_Charge_Name2
            'objVendorInvHead.Add_Charge_Amt2 = obj.Add_Charge_Amt2

            'objVendorInvHead.Add_Charge_Code3 = obj.Add_Charge_Code3
            'objVendorInvHead.Add_Charge_Name3 = obj.Add_Charge_Name3
            'objVendorInvHead.Add_Charge_Amt3 = obj.Add_Charge_Amt3

            'objVendorInvHead.Add_Charge_Code4 = obj.Add_Charge_Code4
            'objVendorInvHead.Add_Charge_Name4 = obj.Add_Charge_Name4
            'objVendorInvHead.Add_Charge_Amt4 = obj.Add_Charge_Amt4

            'objVendorInvHead.Add_Charge_Code5 = obj.Add_Charge_Code5
            'objVendorInvHead.Add_Charge_Name5 = obj.Add_Charge_Name5
            'objVendorInvHead.Add_Charge_Amt5 = obj.Add_Charge_Amt5

            'objVendorInvHead.Add_Charge_Code6 = obj.Add_Charge_Code6
            'objVendorInvHead.Add_Charge_Name6 = obj.Add_Charge_Name6
            'objVendorInvHead.Add_Charge_Amt6 = obj.Add_Charge_Amt6

            'objVendorInvHead.Add_Charge_Code7 = obj.Add_Charge_Code7
            'objVendorInvHead.Add_Charge_Name7 = obj.Add_Charge_Name7
            'objVendorInvHead.Add_Charge_Amt7 = obj.Add_Charge_Amt7

            'objVendorInvHead.Add_Charge_Code8 = obj.Add_Charge_Code8
            'objVendorInvHead.Add_Charge_Name8 = obj.Add_Charge_Name8
            'objVendorInvHead.Add_Charge_Amt8 = obj.Add_Charge_Amt8

            'objVendorInvHead.Add_Charge_Code9 = obj.Add_Charge_Code9
            'objVendorInvHead.Add_Charge_Name9 = obj.Add_Charge_Name9
            'objVendorInvHead.Add_Charge_Amt9 = obj.Add_Charge_Amt9

            'objVendorInvHead.Add_Charge_Code10 = obj.Add_Charge_Code10
            'objVendorInvHead.Add_Charge_Name10 = obj.Add_Charge_Name10
            'objVendorInvHead.Add_Charge_Amt10 = obj.Add_Charge_Amt10


            objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
            Dim ii As Integer = 0
            Dim isFirstTime As Boolean = True
            ' Dim strFirstItemCode As String = GetFirstItemCode(obj.ObjList)
            'objVendorInvHead.Empty_Amount = obj.Tot_Empty_Amount
            objVendorInvHead.Total_Landed_Amt = 0

            objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()


            ''Set AP Invvoice Detail Table


            Dim objVendorInvDetail As clsVedorInvoiceDetail

            If objtr.Deduction > 0 Then
                ii = ii + 1
                objVendorInvDetail = New clsVedorInvoiceDetail()
                objVendorInvDetail.Detail_Line_No = ii

                objVendorInvDetail.GL_Account_Code = clsCommon.myCstr(dt.Rows(0)("Deduction_ACCOUNT"))
                If clsCommon.myLen(objVendorInvDetail.GL_Account_Code) <= 0 Then
                    Throw New Exception("Please set Deduction Account for vendor account set :" + objVendorInvHead.Account_Set)
                End If
                objVendorInvDetail.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvDetail.GL_Account_Code, objHead.MCC_Code, trans)
                objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(objVendorInvDetail.GL_Account_Code, trans)


                objVendorInvDetail.Amount = objtr.Deduction
                objVendorInvDetail.Discount_Per = 0
                objVendorInvDetail.Discount = 0
                objVendorInvDetail.Amount_less_Discount = objtr.Deduction
                objVendorInvDetail.Total_Tax = 0
                objVendorInvDetail.Total_Amount = objtr.Deduction
                objVendorInvDetail.Landed_Amount = objtr.Deduction
                ''End of Set AP Invvoice Detail Table

                If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                    objVendorInvHead.Arr.Add(objVendorInvDetail)
                End If

                ''Set AP Invvoice Header Table
                objVendorInvHead.Total_Landed_Amt += objtr.Deduction
                objVendorInvHead.Discount_Base += objtr.Deduction
                objVendorInvHead.Discount_Amount += 0
                objVendorInvHead.Amount_Less_Discount += objtr.Deduction
                objVendorInvHead.Document_Total += objtr.Deduction
                objVendorInvHead.Balance_Amt += objtr.Deduction
                ''End of Set AP Invvoice Header Table
            End If

            If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                Throw New Exception("No GL Account Found For AP Invoice")
            End If
            objVendorInvHead.ApplicableFrom = objVendorInvHead.Invoice_Entry_Date
            objVendorInvHead.SaveData(objVendorInvHead, True, trans)
            clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans)
        End If
        Return True
    End Function
    Shared Function CreateCreditNote(ByVal objHead As clsIncentiveEntryBySRNHead, ByVal objtr As clsIncentiveEntryBySRNDetail, ByVal trans As SqlTransaction) As Boolean
        ''ERO/12/07/18-000376 by balwinder on -01/10/2018
        Dim objVendorInvHead As New clsVedorInvoiceHead()

        'objVendorInvHead.Document_No = txtDocNo.Value'ToBeGenerated
        objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(objHead.Doc_Date, "dd/MMM/yyyy")
        objVendorInvHead.Vendor_Code = objtr.Vsp_Code
        objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(objtr.Vsp_Code, trans)
        objVendorInvHead.Vendor_Invoice_No = "" ''No Need to send vendor invoice no because it is of debit/Credit note type
        objVendorInvHead.Invoice_Type = "AP"
        objVendorInvHead.Vendor_Invoice_Date = objVendorInvHead.Invoice_Entry_Date
        objVendorInvHead.loc_code = clsLocation.GetSegmentCode(objHead.MCC_Code, trans) 'obj.MCC_CODE
        'objVendorInvHead.Irregular_loc_code = obj.Irregular_MCC_CODE
        objVendorInvHead.Description = "AP Credit Note Against Incentive Entry:" + objtr.Doc_Code + "[" + objtr.TR_Code + "]"
        'objVendorInvHead.PROJECT_ID = 1 'obj.PROJECT_ID
        objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
        If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
            Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
        End If
        objVendorInvHead.Document_Type = "C" ''For Purchase Invoice Type
        ''objVendorInvHead.PO_Number = obj.p
        '' ''added by priti
        objVendorInvHead.RefDocType = "COM-INC" ''Credit Note of Month-Incetive
        objVendorInvHead.RefDocNo = objtr.TR_Code
        'objVendorInvHead.Ref_SNo = ""
        '' '' priti ends here
        'objVendorInvHead.Order_No = txtOrderNo.Text
        ' objVendorInvHead.Total_Tax = 0
        objVendorInvHead.On_Hold = False
        'Dim srndate As String = ""
        'Dim srncode As String = ""
        'Dim Vlc_Code As String = ""
        'Dim Vlc_Name As String = ""
        'For Each objTr As clsMilkPurchaseInvoiceMCCDetail In obj.ObjList
        '    If clsCommon.myLen(objTr.SRN_CODE) > 0 Then
        '        Dim query As String = "select doc_date,vd.VLC_Code,VLC_Name from TSPL_Milk_SRN_HEAD sh inner join TSPL_VLC_MASTER_HEAD vd on sh.VLC_CODE=vd.VLC_Code where DOc_Code ='" + objTr.SRN_CODE + "' "
        '        Dim Dt_SRN As DataTable = clsDBFuncationality.GetDataTable(query, trans)
        '        srndate = IIf(srndate = "", clsCommon.myCDate(CStr(Dt_SRN.Rows(0).Item("Doc_Date")), "dd/MMM/yyyy"), srndate & "," & clsCommon.myCDate(CStr(Dt_SRN.Rows(0).Item("Doc_Date")), "dd/MMM/yyyy"))
        '        srncode = IIf(srncode = "", objTr.SRN_CODE, srncode & "," & objTr.SRN_CODE)
        '        Vlc_Code = IIf(Vlc_Code = "", Dt_SRN.Rows(0).Item("VLC_Code").ToString, Vlc_Code & "," & Dt_SRN.Rows(0).Item("VLC_Code").ToString)
        '        Vlc_Name = IIf(Vlc_Name = "", Dt_SRN.Rows(0).Item("VLC_Name").ToString, Vlc_Name & "," & Dt_SRN.Rows(0).Item("VLC_name").ToString)
        '    End If
        'Next

        'objVendorInvHead.Description = "VSP : " + obj.VSP_CODE + "/" + vendor_name + "VLC : " + Vlc_Code + "/" + Vlc_Name + " .Against PI Invoice No " + obj.DOC_CODE + "-" + srncode + "-" + srndate
        'objVendorInvHead.Tax_Calculation_Type = Nothing
        'objVendorInvHead.Tax_Group = Nothing
        'If (clsCommon.myLen(obj.TAX1) > 0) Then
        '    objVendorInvHead.TAX1 = obj.TAX1
        '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX1, trans) Then
        '        objVendorInvHead.TAX1_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX1, trans)
        '        objVendorInvHead.TAX1_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX1_GLAC, obj.MCC_CODE, trans)
        '    End If
        '    objVendorInvHead.TAX1_Rate = obj.TAX1_Rate
        '    objVendorInvHead.Tax1_BAmount = obj.TAX1_Base_Amt
        '    objVendorInvHead.TAX1_Amt = obj.TAX1_Amt
        'End If
        'If (clsCommon.myLen(obj.TAX2) > 0) Then
        '    objVendorInvHead.TAX2 = obj.TAX2
        '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX2, trans) Then
        '        objVendorInvHead.TAX2_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX2, trans)
        '        objVendorInvHead.TAX2_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX2_GLAC, obj.MCC_CODE, trans)
        '    End If
        '    objVendorInvHead.TAX2_Rate = obj.TAX2_Rate
        '    objVendorInvHead.Tax2_BAmount = obj.TAX2_Base_Amt
        '    objVendorInvHead.TAX2_Amt = obj.TAX2_Amt
        'End If
        'If (clsCommon.myLen(obj.TAX3) > 0) Then
        '    objVendorInvHead.TAX3 = obj.TAX3
        '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX3, trans) Then
        '        objVendorInvHead.TAX3_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX3, trans)
        '        objVendorInvHead.TAX3_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX3_GLAC, obj.MCC_CODE, trans)
        '    End If
        '    objVendorInvHead.TAX3_Rate = obj.TAX3_Rate
        '    objVendorInvHead.Tax3_BAmount = obj.TAX3_Base_Amt
        '    objVendorInvHead.TAX3_Amt = obj.TAX3_Amt
        'End If
        'If (clsCommon.myLen(obj.TAX4) > 0) Then
        '    objVendorInvHead.TAX4 = obj.TAX4
        '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX4, trans) Then
        '        objVendorInvHead.TAX4_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX4, trans)
        '        objVendorInvHead.TAX4_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX4_GLAC, obj.MCC_CODE, trans)
        '    End If
        '    objVendorInvHead.TAX4_Rate = obj.TAX4_Rate
        '    objVendorInvHead.Tax4_BAmount = obj.TAX4_Base_Amt
        '    objVendorInvHead.TAX4_Amt = obj.TAX4_Amt
        'End If
        'If (clsCommon.myLen(obj.TAX5) > 0) Then
        '    objVendorInvHead.TAX5 = obj.TAX5
        '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX5, trans) Then
        '        objVendorInvHead.TAX5_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX5, trans)
        '        objVendorInvHead.TAX5_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX5_GLAC, obj.MCC_CODE, trans)

        '    End If
        '    objVendorInvHead.TAX5_Rate = obj.TAX5_Rate
        '    objVendorInvHead.Tax5_BAmount = obj.TAX5_Base_Amt
        '    objVendorInvHead.TAX5_Amt = obj.TAX5_Amt
        'End If
        'If (clsCommon.myLen(obj.TAX6) > 0) Then
        '    objVendorInvHead.TAX6 = obj.TAX6
        '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX6, trans) Then
        '        objVendorInvHead.TAX6_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX6, trans)
        '        objVendorInvHead.TAX6_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX6_GLAC, obj.MCC_CODE, trans)
        '    End If
        '    objVendorInvHead.TAX6_Rate = obj.TAX6_Rate
        '    objVendorInvHead.Tax6_BAmount = obj.TAX6_Base_Amt
        '    objVendorInvHead.TAX6_Amt = obj.TAX6_Amt
        'End If
        'If (clsCommon.myLen(obj.TAX7) > 0) Then
        '    objVendorInvHead.TAX7 = obj.TAX7
        '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX7, trans) Then
        '        objVendorInvHead.TAX7_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX7, trans)
        '        objVendorInvHead.TAX7_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX7_GLAC, obj.MCC_CODE, trans)

        '    End If
        '    objVendorInvHead.TAX7_Rate = obj.TAX7_Rate
        '    objVendorInvHead.Tax7_BAmount = obj.TAX7_Base_Amt
        '    objVendorInvHead.TAX7_Amt = obj.TAX7_Amt
        'End If
        'If (clsCommon.myLen(obj.TAX8) > 0) Then
        '    objVendorInvHead.TAX8 = obj.TAX8
        '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX8, trans) Then
        '        objVendorInvHead.TAX8_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX8, trans)
        '        objVendorInvHead.TAX8_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX8_GLAC, obj.MCC_CODE, trans)
        '    End If
        '    objVendorInvHead.TAX8_Rate = obj.TAX8_Rate
        '    objVendorInvHead.Tax8_BAmount = obj.TAX8_Base_Amt
        '    objVendorInvHead.TAX8_Amt = obj.TAX8_Amt
        'End If
        'If (clsCommon.myLen(obj.TAX9) > 0) Then
        '    objVendorInvHead.TAX9 = obj.TAX9
        '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX9, trans) Then
        '        objVendorInvHead.TAX9_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX9, trans)
        '        objVendorInvHead.TAX9_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX9_GLAC, obj.MCC_CODE, trans)
        '    End If
        '    objVendorInvHead.TAX9_Rate = obj.TAX9_Rate
        '    objVendorInvHead.Tax9_BAmount = obj.TAX9_Base_Amt
        '    objVendorInvHead.TAX9_Amt = obj.TAX9_Amt
        'End If
        'If (clsCommon.myLen(obj.TAX10) > 0) Then
        '    objVendorInvHead.TAX10 = obj.TAX10
        '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX10, trans) Then
        '        objVendorInvHead.TAX10_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX10, trans)
        '        objVendorInvHead.TAX10_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX10_GLAC, obj.MCC_CODE, trans)
        '    End If
        '    objVendorInvHead.TAX10_Rate = obj.TAX10_Rate
        '    objVendorInvHead.Tax10_BAmount = obj.TAX10_Base_Amt
        '    objVendorInvHead.TAX10_Amt = obj.TAX10_Amt
        'End If

        'objVendorInvHead.Terms_Code = obj.Terms_Code
        'objVendorInvHead.Terms_Description = obj.TermsName
        objVendorInvHead.Due_Date = objVendorInvHead.Invoice_Entry_Date

        'objVendorInvHead.Against_POInvoice_No = obj.DOC_CODE
        'objVendorInvHead.Against_MillkPurchaseInvoice_No = obj.DOC_CODE

        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Incentive_ACCOUNT,Monthly_Rent_Account,Arrear_Account from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
            objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, objHead.MCC_Code, trans)

        End If
        If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
            Throw New Exception("Please set the vendor payable Account")
        End If

        'objVendorInvHead.Total_Add_Charge = obj.Total_Add_Charge

        'objVendorInvHead.Add_Charge_Code1 = obj.Add_Charge_Code1
        'objVendorInvHead.Add_Charge_Name1 = obj.Add_Charge_Name1
        'objVendorInvHead.Add_Charge_Amt1 = obj.Add_Charge_Amt1

        'objVendorInvHead.Add_Charge_Code2 = obj.Add_Charge_Code2
        'objVendorInvHead.Add_Charge_Name2 = obj.Add_Charge_Name2
        'objVendorInvHead.Add_Charge_Amt2 = obj.Add_Charge_Amt2

        'objVendorInvHead.Add_Charge_Code3 = obj.Add_Charge_Code3
        'objVendorInvHead.Add_Charge_Name3 = obj.Add_Charge_Name3
        'objVendorInvHead.Add_Charge_Amt3 = obj.Add_Charge_Amt3

        'objVendorInvHead.Add_Charge_Code4 = obj.Add_Charge_Code4
        'objVendorInvHead.Add_Charge_Name4 = obj.Add_Charge_Name4
        'objVendorInvHead.Add_Charge_Amt4 = obj.Add_Charge_Amt4

        'objVendorInvHead.Add_Charge_Code5 = obj.Add_Charge_Code5
        'objVendorInvHead.Add_Charge_Name5 = obj.Add_Charge_Name5
        'objVendorInvHead.Add_Charge_Amt5 = obj.Add_Charge_Amt5

        'objVendorInvHead.Add_Charge_Code6 = obj.Add_Charge_Code6
        'objVendorInvHead.Add_Charge_Name6 = obj.Add_Charge_Name6
        'objVendorInvHead.Add_Charge_Amt6 = obj.Add_Charge_Amt6

        'objVendorInvHead.Add_Charge_Code7 = obj.Add_Charge_Code7
        'objVendorInvHead.Add_Charge_Name7 = obj.Add_Charge_Name7
        'objVendorInvHead.Add_Charge_Amt7 = obj.Add_Charge_Amt7

        'objVendorInvHead.Add_Charge_Code8 = obj.Add_Charge_Code8
        'objVendorInvHead.Add_Charge_Name8 = obj.Add_Charge_Name8
        'objVendorInvHead.Add_Charge_Amt8 = obj.Add_Charge_Amt8

        'objVendorInvHead.Add_Charge_Code9 = obj.Add_Charge_Code9
        'objVendorInvHead.Add_Charge_Name9 = obj.Add_Charge_Name9
        'objVendorInvHead.Add_Charge_Amt9 = obj.Add_Charge_Amt9

        'objVendorInvHead.Add_Charge_Code10 = obj.Add_Charge_Code10
        'objVendorInvHead.Add_Charge_Name10 = obj.Add_Charge_Name10
        'objVendorInvHead.Add_Charge_Amt10 = obj.Add_Charge_Amt10


        objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
        Dim ii As Integer = 0
        Dim isFirstTime As Boolean = True
        ' Dim strFirstItemCode As String = GetFirstItemCode(obj.ObjList)
        'objVendorInvHead.Empty_Amount = obj.Tot_Empty_Amount
        objVendorInvHead.Total_Landed_Amt = 0

        objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()


        ''Set AP Invvoice Detail Table


        Dim objVendorInvDetail As clsVedorInvoiceDetail

        If objtr.Incentive_Amount > 0 Then
            ii = ii + 1
            objVendorInvDetail = New clsVedorInvoiceDetail()
            objVendorInvDetail.Detail_Line_No = ii

            objVendorInvDetail.GL_Account_Code = clsCommon.myCstr(dt.Rows(0)("Incentive_ACCOUNT"))
            If clsCommon.myLen(objVendorInvDetail.GL_Account_Code) <= 0 Then
                Throw New Exception("Please set Incentive Account for vendor account set :" + objVendorInvHead.Account_Set)
            End If
            objVendorInvDetail.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvDetail.GL_Account_Code, objHead.MCC_Code, trans)
            objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(objVendorInvDetail.GL_Account_Code, trans)


            objVendorInvDetail.Amount = objtr.Incentive_Amount
            objVendorInvDetail.Discount_Per = 0
            objVendorInvDetail.Discount = 0
            objVendorInvDetail.Amount_less_Discount = objtr.Incentive_Amount
            objVendorInvDetail.Total_Tax = 0
            objVendorInvDetail.Total_Amount = objtr.Incentive_Amount
            objVendorInvDetail.Landed_Amount = objtr.Incentive_Amount
            ''End of Set AP Invvoice Detail Table

            If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                objVendorInvHead.Arr.Add(objVendorInvDetail)
            End If

            ''Set AP Invvoice Header Table
            objVendorInvHead.Total_Landed_Amt += objtr.Incentive_Amount
            objVendorInvHead.Discount_Base += objtr.Incentive_Amount
            objVendorInvHead.Discount_Amount += 0
            objVendorInvHead.Amount_Less_Discount += objtr.Incentive_Amount
            objVendorInvHead.Document_Total += objtr.Incentive_Amount
            objVendorInvHead.Balance_Amt += objtr.Incentive_Amount
            ''End of Set AP Invvoice Header Table
        End If

        If objtr.Rent_Amount > 0 Then
            ii = ii + 1
            objVendorInvDetail = New clsVedorInvoiceDetail()
            objVendorInvDetail.Detail_Line_No = ii

            objVendorInvDetail.GL_Account_Code = clsCommon.myCstr(dt.Rows(0)("Monthly_Rent_Account"))
            If clsCommon.myLen(objVendorInvDetail.GL_Account_Code) <= 0 Then
                Throw New Exception("Please set Monthly Rent Account for vendor account set :" + objVendorInvHead.Account_Set)
            End If
            objVendorInvDetail.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvDetail.GL_Account_Code, objHead.MCC_Code, trans)
            objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(objVendorInvDetail.GL_Account_Code, trans)


            objVendorInvDetail.Amount = objtr.Rent_Amount
            objVendorInvDetail.Discount_Per = 0
            objVendorInvDetail.Discount = 0
            objVendorInvDetail.Amount_less_Discount = objtr.Rent_Amount
            objVendorInvDetail.Total_Tax = 0
            objVendorInvDetail.Total_Amount = objtr.Rent_Amount
            objVendorInvDetail.Landed_Amount = objtr.Rent_Amount
            ''End of Set AP Invvoice Detail Table

            If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                objVendorInvHead.Arr.Add(objVendorInvDetail)
            End If

            ''Set AP Invvoice Header Table
            objVendorInvHead.Total_Landed_Amt += objtr.Rent_Amount
            objVendorInvHead.Discount_Base += objtr.Rent_Amount
            objVendorInvHead.Discount_Amount += 0
            objVendorInvHead.Amount_Less_Discount += objtr.Rent_Amount
            objVendorInvHead.Document_Total += objtr.Rent_Amount
            objVendorInvHead.Balance_Amt += objtr.Rent_Amount
            ''End of Set AP Invvoice Header Table
        End If

        If objtr.Arrear > 0 Then
            ii = ii + 1
            objVendorInvDetail = New clsVedorInvoiceDetail()
            objVendorInvDetail.Detail_Line_No = ii

            objVendorInvDetail.GL_Account_Code = clsCommon.myCstr(dt.Rows(0)("Arrear_Account"))
            If clsCommon.myLen(objVendorInvDetail.GL_Account_Code) <= 0 Then
                Throw New Exception("Please set Arrear Account for vendor account set :" + objVendorInvHead.Account_Set)
            End If
            objVendorInvDetail.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvDetail.GL_Account_Code, objHead.MCC_Code, trans)
            objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(objVendorInvDetail.GL_Account_Code, trans)


            objVendorInvDetail.Amount = objtr.Arrear
            objVendorInvDetail.Discount_Per = 0
            objVendorInvDetail.Discount = 0
            objVendorInvDetail.Amount_less_Discount = objtr.Arrear
            objVendorInvDetail.Total_Tax = 0
            objVendorInvDetail.Total_Amount = objtr.Arrear
            objVendorInvDetail.Landed_Amount = objtr.Arrear
            ''End of Set AP Invvoice Detail Table

            If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                objVendorInvHead.Arr.Add(objVendorInvDetail)
            End If

            ''Set AP Invvoice Header Table
            objVendorInvHead.Total_Landed_Amt += objtr.Arrear
            objVendorInvHead.Discount_Base += objtr.Arrear
            objVendorInvHead.Discount_Amount += 0
            objVendorInvHead.Amount_Less_Discount += objtr.Arrear
            objVendorInvHead.Document_Total += objtr.Arrear
            objVendorInvHead.Balance_Amt += objtr.Arrear
            ''End of Set AP Invvoice Header Table
        End If


        If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
            Throw New Exception("No GL Account Found For AP Invoice")
        End If
        objVendorInvHead.ApplicableFrom = objVendorInvHead.Invoice_Entry_Date
        objVendorInvHead.SaveData(objVendorInvHead, True, trans)
        clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans)
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Purchase Order No not found to Delete")
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim obj As clsIncentiveEntryBySRNHead = clsIncentiveEntryBySRNHead.GetData(strCode, NavigatorType.Current, trans)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Doc_Code) > 0) Then
            Try
                'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase Order", "Purchase Order", obj.Bill_To_Location, obj.PurchaseOrder_Date, trans)
                If (obj.Is_Post = ERPTransactionStatus.Posted) Then
                    Throw New Exception("Already Posted")
                End If
                Dim qry As String = "delete from TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL where Doc_Code='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD where Doc_Code='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                trans.Commit()
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
        Return True
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As clsIncentiveEntryBySRNHead = clsIncentiveEntryBySRNHead.GetData(strCode, NavigatorType.Current, trans)
            If Not obj.Is_Post = ERPTransactionStatus.Posted Then
                Throw New Exception("Should be posted transaction - " + obj.Doc_Code)
            End If
            For Each objtr As clsIncentiveEntryBySRNDetail In obj.arr
                Dim strDocCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Payment_No from TSPL_PAYMENT_HEADER where Against_Incentive_Detail_No ='" + objtr.TR_Code + "'", trans))
                If clsCommon.myLen(strDocCode) > 0 Then
                    Dim objPayment As clsPaymentHeader = clsPaymentHeader.GetData(strDocCode, NavigatorType.Current, trans)
                    clsPaymentHeader.ReverseAndUnpost(strDocCode, trans)
                    clsPaymentHeader.fundelete(clsCommon.myCstr(objPayment.Payment_Type), strDocCode, clsCommon.myCstr(objPayment.Vendor_Code), trans)
                    objPayment = Nothing
                End If

                strDocCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_No from tspl_vendor_invoice_head where RefDocType = 'COM-INC' and Document_Type = 'C' and RefDocNo ='" + objtr.TR_Code + "'", trans))
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Doc_No from TSPL_PAYMENT_PROCESS_CREDIT_NOTE where AP_Invoice_No='" + strDocCode + "'", trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Throw New Exception("Used In Payment Process No [" + clsCommon.myCstr(dt.Rows(0)("Doc_No")) + "]")
                End If
                clsVedorInvoiceHead.ReverseAndUnpost(strDocCode, trans)
                clsVedorInvoiceHead.DeleteData(strDocCode, trans)

                strDocCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_No from tspl_vendor_invoice_head where RefDocType = 'DOM-INC' and Document_Type = 'D' and RefDocNo ='" + objtr.TR_Code + "'", trans))
                If clsCommon.myLen(strDocCode) > 0 Then
                    dt = clsDBFuncationality.GetDataTable("select Doc_No from TSPL_PAYMENT_PROCESS_DEDUCTION where AP_Invoice_No='" + strDocCode + "'", trans)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        Throw New Exception("Used In Payment Process No [" + clsCommon.myCstr(dt.Rows(0)("Doc_No")) + "]")
                    End If
                    clsVedorInvoiceHead.ReverseAndUnpost(strDocCode, trans)
                    clsVedorInvoiceHead.DeleteData(strDocCode, trans)
                End If
            Next
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Post_Date", Nothing, True)
            clsCommon.AddColumnsForChange(coll, "Post_By", Nothing, True)
            clsCommon.AddColumnsForChange(coll, "Is_Post", 0)
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD", OMInsertOrUpdate.Update, "Doc_Code='" + strCode + "'", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function CreatePaymentEntry(ByVal arr As ArrayList, ByVal BankCode As String, ByVal paymentMethod As String, ByVal PaymentDate As DateTime) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "select TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Doc_Code, TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Vsp_Code,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Incentive_Amount,TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Rent_Amount, TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Amount,TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Deduction,TSPL_VENDOR_INVOICE_HEAD.Document_No as CreditNoteAmt,TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.TR_Code,TSPL_PAYMENT_HEADER.Payment_No,TSPL_VENDOR_INVOICE_HEAD.Document_No as CreditNoteNo,TabDN.Document_No as DebitNoteNo,TSPL_LOCATION_MASTER.Loc_Segment_Code" + Environment.NewLine +
                    "from TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL " + Environment.NewLine +
                    "left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Vsp_Code" + Environment.NewLine +
                    "left outer join TSPL_VENDOR_INVOICE_HEAD  on TSPL_VENDOR_INVOICE_HEAD.RefDocNo=TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.TR_Code and TSPL_VENDOR_INVOICE_HEAD.RefDocType = 'COM-INC' " + Environment.NewLine +
                    "left outer join TSPL_VENDOR_INVOICE_HEAD as TabDN on TabDN.RefDocNo=TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.TR_Code and TabDN.RefDocType = 'DOM-INC' " + Environment.NewLine +
                    "left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Against_Incentive_Detail_No=TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.TR_Code " + Environment.NewLine +
                    "left outer join TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD on TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.Doc_Code=TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Doc_Code" + Environment.NewLine +
                    "left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.MCC_Code" + Environment.NewLine +
                    "where  len(isnull( TSPL_VENDOR_INVOICE_HEAD.Document_No,''))>0 and len(isnull(TSPL_PAYMENT_HEADER.Payment_No,''))<=0 and TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.TR_Code in  (" + clsCommon.GetMulcallString(arr) + ")"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim objPay As New clsPaymentHeader()
                    objPay.Against_Incentive_Detail_No = clsCommon.myCstr(dr("TR_Code"))
                    objPay.Payment_No = ""
                    objPay.Entry_Desc = "Incentive Payment No [" + clsCommon.myCstr(dr("Doc_Code")) + "] Detail No [" + clsCommon.myCstr(dr("TR_Code")) + "] Credit Note No [" + clsCommon.myCstr(dr("CreditNoteNo")) + "] of VSP [" + clsCommon.myCstr(dr("Vsp_Code")) + "]"
                    objPay.Payment_Date = clsCommon.myCDate(PaymentDate)
                    objPay.Payment_Post_Date = clsCommon.myCDate(PaymentDate)
                    objPay.Bank_Code = BankCode
                    objPay.Payment_Type = "PY"
                    objPay.Vendor_Code = clsCommon.myCstr(dr("Vsp_Code"))
                    objPay.Vendor_Name = clsCommon.myCstr(dr("Vendor_Name"))
                    objPay.Payment_Code = paymentMethod
                    'objPay.Cheque_No = obj.ArrPPDetail.Item(i).Cheque_No
                    'If Not obj.ArrPPDetail.Item(i).Cheque_Dated Is Nothing Then
                    '    objPay.Cheque_Date = obj.ArrPPDetail.Item(i).Cheque_Dated
                    'End If
                    objPay.Account_Payee = 0
                    objPay.memorndmamt = "0"
                    'objPay.Applied_Payment = clsCommon.myCstr(obj.ArrPPDetail.Item(i).AP_Invoice_No)
                    objPay.Is_Security = 0
                    objPay.IsChkReverse = "N"
                    objPay.Bank_Charges = 0
                    objPay.Payment_Amount = clsCommon.myCdbl(dr("Amount"))
                    objPay.Balance_Amt = clsCommon.myCdbl(dr("Amount"))
                    objPay.Location_Code = clsCommon.myCstr(dr("Loc_Segment_Code"))
                    objPay.ArrTr = New List(Of clsPaymentDetail)

                    Dim dclDeduction As Decimal = clsCommon.myCdbl(dr("Deduction"))

                    Dim objtr As New clsPaymentDetail()
                    objtr.Apply = "1"
                    objtr.Payment_Type = "PY"
                    objtr.Document_No = clsCommon.myCstr(dr("CreditNoteNo"))
                    objtr.Original_Invoice_Amt = clsCommon.myCdbl(dr("Amount")) + dclDeduction
                    objtr.Applied_Amount = clsCommon.myCdbl(dr("Amount")) + dclDeduction
                    objtr.Pending_Balance = 0
                    'objtr.Vendor_Invoice_No = obj.ArrPPDetail.Item(i).Milk_Purchase_Invoice_No
                    objtr.Net_Balance = 0
                    objtr.Security_Amount = 0
                    objPay.ArrTr.Add(objtr)

                    If dclDeduction > 0 Then
                        objtr = New clsPaymentDetail()
                        objtr.Apply = "1"
                        objtr.Payment_Type = "PY"
                        objtr.Document_No = clsCommon.myCstr(dr("DebitNoteNo"))
                        objtr.Original_Invoice_Amt = dclDeduction
                        objtr.Applied_Amount = dclDeduction
                        objtr.Pending_Balance = 0
                        objtr.Net_Balance = 0
                        objtr.Security_Amount = 0
                        objPay.ArrTr.Add(objtr)
                    End If

                    objPay.SaveData(objPay, True, trans, True)
                    clsPaymentHeader.PostData(objPay.Payment_No, "Payable", trans)
                Next
            End If
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class

Public Class clsIncentiveEntryBySRNDetail
#Region "Variables"
    Public TR_Code As String
    Public Doc_Code As String
    Public Vsp_Code As String
    Public Vsp_Name As String ''Not a Table Column
    Public VLC_Code As String ''Not a Table Column
    Public VLC_Name As String ''Not a Table Column
    Public Incentive_Code As String
    Public Incentive_Qty As Decimal
    Public Incentive_UOM As String
    Public Incentive_Rate As Decimal
    Public Incentive_Amount As Decimal
    Public Monthly_Rent_Amount As Decimal
    Public Milk_Received_Days As Decimal
    Public Rent_Amount As Decimal

    Public Arrear As Decimal
    Public Deduction As Decimal

    Public Amount As Decimal

    Public CreditNoteNo As String ''Not a Table Column
    Public DebitNoteNo As String ''Not a Table Column
    Public PaymentNo As String ''Not a Table Column
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal dtFilterMonth As DateTime, ByVal dtDocDate As Date, ByVal Arr As List(Of clsIncentiveEntryBySRNDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim arrVSP As New List(Of String)
            For Each obj As clsIncentiveEntryBySRNDetail In Arr
                Dim coll As New Hashtable()
                obj.TR_Code = clsERPFuncationality.GetNextCode(trans, dtDocDate, clsDocType.Detail, clsDocTransactionType.Detail, "")
                clsCommon.AddColumnsForChange(coll, "TR_Code", obj.TR_Code)
                clsCommon.AddColumnsForChange(coll, "Doc_Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Vsp_Code", obj.Vsp_Code)
                clsCommon.AddColumnsForChange(coll, "Incentive_Code", obj.Incentive_Code, True)
                clsCommon.AddColumnsForChange(coll, "Incentive_Qty", obj.Incentive_Qty, True)
                clsCommon.AddColumnsForChange(coll, "Incentive_UOM", obj.Incentive_UOM, True)
                clsCommon.AddColumnsForChange(coll, "Incentive_Rate", obj.Incentive_Rate, True)
                clsCommon.AddColumnsForChange(coll, "Incentive_Amount", obj.Incentive_Amount)
                clsCommon.AddColumnsForChange(coll, "Monthly_Rent_Amount", obj.Monthly_Rent_Amount)
                clsCommon.AddColumnsForChange(coll, "Milk_Received_Days", obj.Milk_Received_Days)
                clsCommon.AddColumnsForChange(coll, "Rent_Amount", obj.Rent_Amount)
                clsCommon.AddColumnsForChange(coll, "Arrear", obj.Arrear)
                clsCommon.AddColumnsForChange(coll, "Deduction", obj.Deduction)
                clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                arrVSP.Add(obj.Vsp_Code)
            Next

            Dim qry As String = "select TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Doc_Code,TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Vsp_Code from TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL" + Environment.NewLine + _
                "left outer join TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD on TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.Doc_Code=TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Doc_Code" + Environment.NewLine + _
                "where DATEPART(YEAR ,TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.Filter_Month)='" + clsCommon.myCstr(dtFilterMonth.Year) + "' and DATEPART(MONTH ,TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.Filter_Month)='" + clsCommon.myCstr(dtFilterMonth.Month) + "' and TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Vsp_Code in (" + clsCommon.GetMulcallString(arrVSP) + ") and TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.Doc_Code not in ('" + strDocNo + "')"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                qry = "Alreday Incentive Give to VSP.Details are"
                For Each dr As DataRow In dt.Rows
                    qry += Environment.NewLine + "Incentive No [" + clsCommon.myCstr(dr("Doc_Code")) + "] and VSP No [" + clsCommon.myCstr(dr("Vsp_Code")) + "]"
                Next
                dt = Nothing
                Throw New Exception(qry)
            End If
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal trans As SqlTransaction, ByRef arrVSP As ArrayList) As List(Of clsIncentiveEntryBySRNDetail)
        arrVSP = New ArrayList
        Dim arr As List(Of clsIncentiveEntryBySRNDetail) = Nothing
        Dim qry As String = "select TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.*,TSPL_VENDOR_MASTER.Vendor_Name as Vsp_Name,TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VENDOR_INVOICE_HEAD.Document_No as CreditNoteNo,TablDN.Document_No as DebitNoteNo,TSPL_PAYMENT_HEADER.Payment_No" + Environment.NewLine +
            " from TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL " + Environment.NewLine +
            " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Vsp_Code " + Environment.NewLine +
            " left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_MASTER.Vendor_Code " + Environment.NewLine +
            " left outer join TSPL_VENDOR_INVOICE_HEAD  on TSPL_VENDOR_INVOICE_HEAD.RefDocNo=TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.TR_Code and TSPL_VENDOR_INVOICE_HEAD.RefDocType = 'COM-INC'" + Environment.NewLine +
            " left outer join TSPL_VENDOR_INVOICE_HEAD as TablDN on TablDN.RefDocNo=TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.TR_Code and TablDN.RefDocType = 'DOM-INC'" + Environment.NewLine +
            " left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Against_Incentive_Detail_No=TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.TR_Code " + Environment.NewLine +
            " " + Environment.NewLine +
            " where Doc_Code='" + strDocNo + "' order by TR_Code"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsIncentiveEntryBySRNDetail)
            For Each dr As DataRow In dt.Rows
                Dim obj As New clsIncentiveEntryBySRNDetail
                obj.TR_Code = clsCommon.myCstr(dr("TR_Code"))
                obj.Doc_Code = clsCommon.myCstr(dr("Doc_Code"))
                obj.Vsp_Code = clsCommon.myCstr(dr("VSP_CODE"))
                obj.Vsp_Name = clsCommon.myCstr(dr("Vsp_Name"))
                obj.VLC_Code = clsCommon.myCstr(dr("VLC_Code"))
                obj.VLC_Name = clsCommon.myCstr(dr("VLC_Name"))
                obj.Incentive_Code = clsCommon.myCstr(dr("Incentive_Code"))
                obj.Incentive_Qty = clsCommon.myCdbl(dr("Incentive_Qty"))
                obj.Incentive_UOM = clsCommon.myCstr(dr("Incentive_UOM"))
                obj.Incentive_Rate = clsCommon.myCdbl(dr("Incentive_Rate"))
                obj.Incentive_Amount = clsCommon.myCdbl(dr("Incentive_Amount"))
                obj.Monthly_Rent_Amount = clsCommon.myCdbl(dr("Monthly_Rent_Amount"))
                obj.Milk_Received_Days = clsCommon.myCdbl(dr("Milk_Received_Days"))
                obj.Rent_Amount = clsCommon.myCdbl(dr("Rent_Amount"))

                obj.Arrear = clsCommon.myCdbl(dr("Arrear"))
                obj.Deduction = clsCommon.myCdbl(dr("Deduction"))

                obj.Amount = clsCommon.myCdbl(dr("AMOUNT"))
                obj.CreditNoteNo = clsCommon.myCstr(dr("CreditNoteNo"))
                obj.DebitNoteNo = clsCommon.myCstr(dr("DebitNoteNo"))
                obj.PaymentNo = clsCommon.myCstr(dr("Payment_No"))
                arr.Add(obj)
                arrVSP.Add(obj.Vsp_Code)
            Next
        End If
        Return arr
    End Function

    Public Shared Function GetCalculateIncentive(ByVal strCurrentDocNo As String, ByVal strMCC As String, ByVal dtCurrentMonth As Date, ByVal arrVSP As ArrayList) As List(Of clsIncentiveEntryBySRNDetail)
        Dim arrReturn As List(Of clsIncentiveEntryBySRNDetail) = Nothing
        Dim objReturn As New clsIncentiveEntryBySRNDetail
        Dim qry As String = "select TSPL_VENDOR_MASTER.Vendor_Code, TSPL_VENDOR_MASTER.Vendor_Name,TSPL_VENDOR_MASTER.Monthly_Rent,TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name from TSPL_VENDOR_MASTER left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_MASTER.Vendor_Code where Vendor_Code in (" + clsCommon.GetMulcallString(arrVSP) + ") " + Environment.NewLine +
        "and not exists (select 1 from TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL" + Environment.NewLine +
        "left outer join TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD on TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.Doc_Code=TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Doc_Code" + Environment.NewLine +
        "where DATEPART(YEAR ,TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.Filter_Month)='" + clsCommon.myCstr(dtCurrentMonth.Year) + "' and DATEPART(MONTH ,TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.Filter_Month)='" + clsCommon.myCstr(dtCurrentMonth.Month) + "' and TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Vsp_Code=TSPL_VENDOR_MASTER.Vendor_Code and TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.Doc_Code not in ('" + strCurrentDocNo + "'))"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arrReturn = New List(Of clsIncentiveEntryBySRNDetail)
            For Each dr As DataRow In dt.Rows
                objReturn = New clsIncentiveEntryBySRNDetail
                objReturn.Vsp_Code = clsCommon.myCstr(dr("Vendor_Code"))
                objReturn.Vsp_Name = clsCommon.myCstr(dr("Vendor_Name"))
                objReturn.VLC_Code = clsCommon.myCstr(dr("VLC_Code"))
                objReturn.VLC_Name = clsCommon.myCstr(dr("VLC_Name"))
                Dim incentive As ArrayList = LoadDataQuery_For_Incentive(strMCC, objReturn.Vsp_Code, dtCurrentMonth, Nothing, objReturn.Incentive_Code, objReturn.Incentive_Qty, objReturn.Incentive_UOM, objReturn.Incentive_Rate)
                If incentive.Count > 0 Then
                    If incentive(1) > 0 Then
                        objReturn.Incentive_Amount = Math.Round(clsCommon.myCdbl(incentive(1)), 2, MidpointRounding.ToEven)
                    End If
                End If
                objReturn.Monthly_Rent_Amount = clsCommon.myCdbl(dr("Monthly_Rent"))
                Dim dtFromDate As DateTime = New DateTime(dtCurrentMonth.Year, dtCurrentMonth.Month, 1)
                Dim dtToDate As DateTime = New DateTime(dtCurrentMonth.Year, dtCurrentMonth.Month, 1).AddMonths(1).AddDays(-1)
                qry = "select count(1) from (select d from (select convert(date, DOC_DATE,103) as D from tspl_milk_srn_head where vsp_code='" + clsCommon.myCstr(dr("Vendor_Code")) + "' and DOC_DATE>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFromDate), "dd/MMM/yyyy hh:mm:ss tt") + "' and DOC_DATE<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtToDate), "dd/MMM/yyyy hh:mm:ss tt") + "')x group by d)xx"
                objReturn.Milk_Received_Days = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
                objReturn.Rent_Amount = Math.Round(objReturn.Monthly_Rent_Amount * objReturn.Milk_Received_Days / System.DateTime.DaysInMonth(dtCurrentMonth.Year, dtCurrentMonth.Month), 2, MidpointRounding.ToEven)
                objReturn.Amount = objReturn.Rent_Amount + objReturn.Incentive_Amount
                If objReturn.Amount > 0 Then
                    arrReturn.Add(objReturn)
                End If
            Next
        End If
        Return arrReturn
    End Function

    Public Shared Function LoadDataQuery_For_Incentive(ByVal MCC_Code As String, ByVal VspCode As String, ByVal dtCurrentMonth As Date, ByVal trans As SqlTransaction, ByRef strIncentiveCode As String, ByRef dclIncentiveQty As Decimal, ByRef strIncentiveUOM As String, ByRef dclIncentiveRate As Decimal) As ArrayList
        Dim days_count As Integer = DateTime.DaysInMonth(dtCurrentMonth.Year, dtCurrentMonth.Month)
        Dim ArrReturn As New ArrayList
        Dim Qty As Double = 0
        Dim qry As String = ""
        Dim DtIncentiveMaster As DataTable = clsMilkPurchaseInvoiceMCC.GetVSPIncentiveMaster(MCC_Code, VspCode, trans)
        If DtIncentiveMaster.Rows.Count <= 0 Then
            Return ArrReturn
            Exit Function
        End If

        If DtIncentiveMaster.Rows(0).Item("Scheme_For") = "PC" Then
            For Each Incrow As DataRow In DtIncentiveMaster.Rows()
                Dim Whrcls As String = ""
                qry = "select distinct CAST(0 as bit) as Sel,code,convert(date,Final.DOC_DATE,103) AS DOC_DATE,DENSE_RANK() over (order by convert(date,Final.DOC_DATE,103)) as Date_Day,MONTH(convert(date,Final.DOC_DATE,103)) as Date_Month,Year(convert(date,Final.DOC_DATE,103)) as Date_Year,ICode,Final.MCC_code,Final.VLC_Code,VLC_Name,Vendor,Final.Vendor_Name,max(IName) as IName" _
                & " ,Unit ,Qty as POQty, SUM(Qty* case when RI=-1 then 1 else 0 end) " _
                & " as GRNQty,SUM(Unapproved) as UnapprovedQty,SUM((Qty *RI)- Unapproved) as PedningQty ,MAX(Rate) as Rate,MAX(Vendor) as Vendor,MAX(TSPL_VENDOR_MASTER.Vendor_Name) as VendorName" _
                & " ,0 as Assessable,max(Amount) as Amount,FAT_PER,SNF_PER,CLR,FAT_KG,SNF_KG,cans,Route_Code,route_name,Final.VEHICLE_CODE,Vehicle_Name,Correction_factor,Final.shift,commision_pers as Commission,payment_commision_pers as Payment_Commission,0.00 as Incentive_value from (  select distinct TSPL_MILK_SRN_DETAIL.DOC_CODE as Code,TSPL_MILK_SRN_HEAD.DOC_DATE" _
                & " ,TSPL_MILK_SRN_HEAD.MCC_code,TSPL_VLC_MASTER_HEAD.VLC_CODE,vlc_name,TSPL_MILK_SRN_HEAD.VSP_CODE as Vendor,Vendor_name,TSPL_MILK_SRN_DETAIL.Item_Code as ICode" _
                & " ,Item_Desc as IName,(case when TSPL_INCENTIVE_MASTER_HEAD.Qty_Type='ACTQ' then TSPL_MILK_SRN_DETAIL.Qty when TSPL_INCENTIVE_MASTER_HEAD.Qty_Type='STDQ' then ((TSPL_MILK_SRN_detail.FAT_KG/Price_Chart.FAT_Pers) * (Price_Chart.Fat_ratio/100.00)+ " _
                & " (TSPL_MILK_SRN_detail.SNF_KG/Price_Chart.SNF_Pers) * (Price_Chart.SNF_Ratio/100.00))*100 else TSPL_MILK_SRN_DETAIL.Qty end)  as Qty,0 as Unapproved,TSPL_MILK_SRN_DETAIL.UOM_Code as Unit,1 as RI,TSPL_MILK_SRN_DETAIL.RATE as Rate,1 as Chk " _
                & " ,TSPL_MILK_SRN_DETAIL.Amount,TSPL_MILK_SRN_DETAIL.FAT_PER,TSPL_MILK_SRN_DETAIL.SNF_PER,NO_OF_CANS as cans,TSPL_MILK_SAMPLE_DETAIL.CLR,TSPL_MILK_SRN_HEAD.Route_Code " _
                & " ,route_name,TSPL_MILK_SRN_HEAD.VEHICLE_CODE,TSPL_VEHICLE_MASTER.Vehicle_Name,tspl_Milk_Srn_Detail.Correction_factor,case when TSPL_MILK_SRN_HEAD.SHIFT='M' then 'Morning' else 'Evening' end as shift " _
                & " ,TSPL_INCENTIVE_MASTER_HEAD.Qty_Type,TSPL_MILK_SRN_DETAIL.FAT_KG,TSPL_MILK_SRN_DETAIL.SNF_KG " + Environment.NewLine + _
                " from TSPL_MILK_SRN_DETAIL " + Environment.NewLine + _
                " left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE =TSPL_MILK_SRN_DETAIL.DOC_CODE " + Environment.NewLine + _
                " Left join tspl_item_Master on tspl_item_Master.Item_Code=TSPL_MILK_SRN_DETAIL.Item_Code " + Environment.NewLine + _
                " left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_SRN_HEAD.VLC_CODE " + Environment.NewLine + _
                " left join (SELECT TSPL_VENDOR_MASTER.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_VENDOR_MASTER.Form_Type,TSPL_VENDOR_MASTER.Apply_Mult_Incentive, CASE WHEN TSPL_VENDOR_MASTER.Apply_Mult_Incentive=1 THEN TSPL_VSP_INCENTIVE.INCENTIVE_CODE ELSE TSPL_VENDOR_MASTER.incentive END AS Incentive  FROM TSPL_VENDOR_MASTER LEFT JOIN TSPL_VSP_INCENTIVE ON TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VSP_INCENTIVE.VENDOR_CODE WHERE coalesce(TSPL_VSP_INCENTIVE.INCENTIVE_CODE,TSPL_VENDOR_MASTER.incentive)= '" & clsCommon.myCstr(Incrow.Item("INCENTIVE_CODE")) & "') AS TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.vendor_Code=TSPL_MILK_SRN_HEAD.Vsp_CODE " + Environment.NewLine + _
                " left join TSPL_MILK_SAMPLE_DETAIL on TSPL_MILK_SAMPLE_DETAIL.DOC_CODE=TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE and TSPL_MILK_SAMPLE_DETAIL.Item_Code =TSPL_MILK_SRN_DETAIL.Item_Code and TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE =TSPL_MILK_SRN_HEAD.VLC_DOC_CODE and TSPL_MILK_SAMPLE_DETAIL.sample_No =TSPL_MILK_SRN_HEAD.sample_No " + Environment.NewLine + _
                " Left join TSPL_MILK_SAMPLE_HEAD on TSPL_MILK_SAMPLE_HEAD.DOC_CODE=TSPL_MILK_SAMPLE_DETAIL.DOC_CODE  " + Environment.NewLine + _
                " left join TSPL_MILK_Receipt_DETAIL on TSPL_MILK_Receipt_DETAIL.DOC_CODE=TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE and TSPL_MILK_Receipt_DETAIL.Item_Code=TSPL_MILK_SAMPLE_Detail.Item_Code  and TSPL_MILK_Receipt_DETAIL.VLC_DOC_CODE=TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE and TSPL_MILK_Receipt_DETAIL.sample_No=TSPL_MILK_SAMPLE_DETAIL.sample_No   " + Environment.NewLine + _
                " left join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_MILK_SRN_HEAD.VEHICLE_CODE " + Environment.NewLine + _
                " left join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_MILK_SRN_HEAD.ROUTE_CODE " + Environment.NewLine + _
                " left join (select distinct FAT_Pers,SNF_Pers,Ratio as Fat_ratio,SNF_Ratio, Milk_Rate,TSPL_MILK_PRICE_MASTER.Price_Code,TSPL_FAT_SNF_UPLOADER_MASTER.code  from TSPL_FAT_SNF_UPLOADER_MASTER inner join  TSPL_MILK_PRICE_MASTER  on TSPL_MILK_PRICE_MASTER.Price_Code=TSPL_FAT_SNF_UPLOADER_MASTER.Price_Code) as  Price_Chart  on TSPL_MILK_SRN_DETAIL.Price_Code=Price_Chart.Code " + Environment.NewLine + _
                " Inner join TSPL_INCENTIVE_MASTER_HEAD on INCENTIVE_CODE= TSPL_VENDOR_MASTER.Incentive and TSPL_INCENTIVE_MASTER_HEAD.start_date<=convert(date,TSPL_MILK_SRN_HEAD.doc_date,103) and TSPL_INCENTIVE_MASTER_HEAD.End_date>=convert(date,TSPL_MILK_SRN_HEAD.doc_date,103) " + Environment.NewLine + _
                " where  TSPL_MILK_SRN_HEAD.Posted=1 " & Whrcls & " and datepart(YEAR,TSPL_MILK_SRN_HEAD.DOC_DATE)='" + clsCommon.myCstr(dtCurrentMonth.Year) + "'  and datepart(MONTH,TSPL_MILK_SRN_HEAD.DOC_DATE)='" + clsCommon.myCstr(dtCurrentMonth.Month) + "'  and TSPL_MILK_SRN_HEAD.Against_Reject_No is null"
                If clsCommon.myLen(VspCode) > 0 Then
                    qry += " and TSPL_MILK_SRN_HEAD.VSP_Code='" + VspCode + "'"
                End If
                qry &= " )Final Left join tspl_milk_Shift_End_Detail sed on sed.mcc_Code=Final.MCC_CODE and convert(date,sed.DOC_DATE,103)=convert(date,Final.DOC_DATE,103) and sed.SHIFT=Final.shift left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=final.Vendor  group by Code,Final.DOC_DATE,Final.MCC_code,ICode,Unit,Final.VLC_Code,VLC_Name,Final.Vendor,Final.Vendor_Name,FAT_PER,SNF_PER,CLR,FAT_KG,SNF_KG,cans,Correction_factor,Route_Code,route_name,Final.VEHICLE_CODE,Vehicle_Name,Final.shift,commision_pers,payment_commision_pers,Qty  having SUM(Chk)>0 and   (SUM(Qty*RI) <>0 or (SUM(Qty*RI)=0  and (SUM((Qty *RI)- Unapproved)<>0 )))             order by Code "
                Dim dtAllData As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dtAllData.Rows.Count <= 0 Then
                    Continue For
                End If
                If clsCommon.CompairString(Incrow.Item("Starting_Shift"), "E") = CompairStringResult.Equal Or dtAllData.Select("Doc_Date<'" & Incrow.Item("Start_date") & "'").Count > 0 Then
                    For Each row1 As DataRow In dtAllData.Select("Doc_Date='" & Incrow.Item("Start_date") & "' and shift='Morning'")
                        dtAllData.Rows.Remove(row1)
                    Next
                    For Each row1 As DataRow In dtAllData.Select("Doc_Date<'" & Incrow.Item("Start_date") & "'")
                        dtAllData.Rows.Remove(row1)
                    Next
                End If
                If clsCommon.CompairString(Incrow.Item("Ending_Shift"), "M") = CompairStringResult.Equal Or dtAllData.Select("Doc_Date>'" & Incrow.Item("End_date") & "'").Count > 0 Then
                    For Each row1 As DataRow In dtAllData.Select("Doc_Date='" & Incrow.Item("End_date") & "' and shift='Evening'")
                        dtAllData.Rows.Remove(row1)
                    Next
                    For Each row1 As DataRow In dtAllData.Select("Doc_Date>'" & Incrow.Item("End_date") & "'")
                        dtAllData.Rows.Remove(row1)
                    Next
                End If
                Dim Dtincentive As DataTable = clsMilkPurchaseInvoiceMCC.GetIncentive(MCC_Code, VspCode, clsCommon.myCstr(Incrow.Item("INCENTIVE_CODE")), trans)
                '' calculate quality incentive
                If clsCommon.CompairString(Incrow.Item("INCENTIVE_TYPE"), "QLTY") = CompairStringResult.Equal Then
                    ArrReturn.Add(0)
                    clsMilkPurchaseInvoiceMCC.Calculate_Quality_Incentive(dtAllData, Nothing, clsCommon.myCstr(Incrow.Item("INCENTIVE_CODE")), trans)
                    ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                    Continue For
                End If
                Dim DaysSetting As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.NoOfDaysForMultiInceForSameVSPForSamePayCycle, clsFixedParameterCode.NoOfDaysForMultiInceForSameVSPForSamePayCycle, trans))
                If DaysSetting = 1 Then
                    days_count = clsCommon.myCdbl(dtAllData.Compute("Max(Date_Day)", ""))
                End If
                For Each row As DataRow In Dtincentive.Rows()
                    strIncentiveCode = clsCommon.myCstr(row("INCENTIVE_CODE"))
                    dclIncentiveRate = clsCommon.myCdbl(row("Rate"))
                    strIncentiveUOM = clsCommon.myCdbl(row("RATE_UOM"))
                    dclIncentiveQty = clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", ""))

                    If clsCommon.CompairString(clsCommon.myCstr(row.Item("Calc_TYPE")), "A") = CompairStringResult.Equal Then '================Avg. Calculation============Payment Cycle================
                        If clsCommon.CompairString(clsCommon.myCstr(row.Item("Rate_TYPE")), "F") = CompairStringResult.Equal Then '================FAT Rate Calculation============Payment Cycle================
                            If clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QB") = CompairStringResult.Equal Then '================Quantity Based============Payment Cycle================
                                If clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                                    ArrReturn.Add(row("Rate"))
                                    clsMilkPurchaseInvoiceMCC.Calculate_Incentive(dtAllData, Nothing, row, trans)
                                    ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLAB") = CompairStringResult.Equal Then '=================Slab Based===========Payment Cycle==========================
                                If clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("Slab_From")) And clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) < clsCommon.myCdbl(row.Item("Slab_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    clsMilkPurchaseInvoiceMCC.Calculate_Incentive(dtAllData, Nothing, row, trans)
                                    ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "TSB") = CompairStringResult.Equal Then '=============Total Solid===========Payment Cycle=====================
                                If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("Slab_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("Slab_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    clsMilkPurchaseInvoiceMCC.Calculate_Incentive(dtAllData, Nothing, row, trans)
                                    ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QTSSLAB") = CompairStringResult.Equal Then '=============Quantity and Total Solid===========Payment Cycle=====================
                                If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("TS_To")) And clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                                    ArrReturn.Add(row("Rate"))
                                    clsMilkPurchaseInvoiceMCC.Calculate_Incentive(dtAllData, Nothing, row, trans)
                                    ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLABTSSLAB") = CompairStringResult.Equal Then '=============Slab Based and Total Solid===========Payment Cycle=====================
                                If clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("SLAB_From")) And clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) < clsCommon.myCdbl(row.Item("SLAB_To")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("TS_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    clsMilkPurchaseInvoiceMCC.Calculate_Incentive(dtAllData, Nothing, row, trans)
                                    ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                    Exit For
                                End If
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("Rate_TYPE")), "Q") = CompairStringResult.Equal Then '================Quantitative Rate Calculation============Payment Cycle================
                            If clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QB") = CompairStringResult.Equal Then '================Quantity Based============Payment Cycle================
                                If clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Dim incentive_value As Decimal = clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", ""))
                                    ArrReturn.Add(incentive_value)
                                    clsMilkPurchaseInvoiceMCC.Calculate_Incentive2(dtAllData, Nothing, incentive_value, clsCommon.myCdbl(row("Rate")), trans, "")
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLAB") = CompairStringResult.Equal Then '=================Slab Based===========Payment Cycle==========================
                                If clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("Slab_From")) And clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) < clsCommon.myCdbl(row.Item("Slab_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Dim incentive_value As Decimal = clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", ""))
                                    ArrReturn.Add(incentive_value)
                                    clsMilkPurchaseInvoiceMCC.Calculate_Incentive2(dtAllData, Nothing, incentive_value, clsCommon.myCdbl(row("Rate")), trans, "")
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "TSB") = CompairStringResult.Equal Then '=============Total Solid===========Payment Cycle=====================
                                If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("Slab_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("Slab_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Dim incentive_value As Decimal = clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", ""))
                                    ArrReturn.Add(incentive_value)
                                    clsMilkPurchaseInvoiceMCC.Calculate_Incentive2(dtAllData, Nothing, incentive_value, clsCommon.myCdbl(row("Rate")), trans, "")
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QTSSLAB") = CompairStringResult.Equal Then '=============Quantity and Total Solid===========Payment Cycle=====================
                                If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("TS_To")) And clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Dim incentive_value As Decimal = clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", ""))
                                    ArrReturn.Add(incentive_value)
                                    clsMilkPurchaseInvoiceMCC.Calculate_Incentive2(dtAllData, Nothing, incentive_value, clsCommon.myCdbl(row("Rate")), trans, "")
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLABTSSLAB") = CompairStringResult.Equal Then '=============Slab Based and Total Solid===========Payment Cycle=====================
                                If clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) >= clsCommon.myCdbl(row.Item("SLAB_From")) And clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) / IIf(days_count <= 0, 1, days_count)) < clsCommon.myCdbl(row.Item("SLAB_To")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("TS_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Dim incentive_value As Decimal = clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", ""))
                                    ArrReturn.Add(incentive_value)
                                    clsMilkPurchaseInvoiceMCC.Calculate_Incentive2(dtAllData, Nothing, incentive_value, clsCommon.myCdbl(row("Rate")), trans, "")
                                    Exit For
                                End If
                            End If
                        End If
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("Calc_TYPE")), "F") = CompairStringResult.Equal Then '================Flat Calculation============Payment Cycle================
                        If clsCommon.CompairString(clsCommon.myCstr(row.Item("Rate_TYPE")), "F") = CompairStringResult.Equal Then '================FAT Rate Calculation============Payment Cycle================
                            If clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QB") = CompairStringResult.Equal Then '================Quantity Based============Payment Cycle================
                                If clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                                    ArrReturn.Add(row("Rate"))
                                    clsMilkPurchaseInvoiceMCC.Calculate_Incentive(dtAllData, Nothing, row, trans)
                                    ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLAB") = CompairStringResult.Equal Then '=================Slab Based===========Payment Cycle==========================
                                If clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) >= clsCommon.myCdbl(row.Item("Slab_From")) And clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) < clsCommon.myCdbl(row.Item("Slab_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    clsMilkPurchaseInvoiceMCC.Calculate_Incentive(dtAllData, Nothing, row, trans)
                                    ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "TSB") = CompairStringResult.Equal Then '=============Total Solid===========Payment Cycle=====================
                                If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("Slab_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("Slab_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    clsMilkPurchaseInvoiceMCC.Calculate_Incentive(dtAllData, Nothing, row, trans)
                                    ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QTSSLAB") = CompairStringResult.Equal Then '=============Quantity and Total Solid===========Payment Cycle=====================
                                If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("TS_To")) And clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                                    ArrReturn.Add(row("Rate"))
                                    clsMilkPurchaseInvoiceMCC.Calculate_Incentive(dtAllData, Nothing, row, trans)
                                    ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLABTSSLAB") = CompairStringResult.Equal Then '=============Slab Based and Total Solid===========Payment Cycle=====================
                                If clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) >= clsCommon.myCdbl(row.Item("SLAB_From")) And clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) < clsCommon.myCdbl(row.Item("SLAB_To")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("TS_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    clsMilkPurchaseInvoiceMCC.Calculate_Incentive(dtAllData, Nothing, row, trans)
                                    ArrReturn.Add(clsCommon.myCdbl(clsCommon.myCdbl(dtAllData.Compute("SUM(Incentive_Value)", ""))))
                                    Exit For
                                End If
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("Rate_TYPE")), "Q") = CompairStringResult.Equal Then '================Quantitative Rate Calculation============Payment Cycle================
                            If clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QB") = CompairStringResult.Equal Then '================Quantity Based============Payment Cycle================
                                If clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                                    ArrReturn.Add(row("Rate"))
                                    ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")))
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLAB") = CompairStringResult.Equal Then '=================Slab Based===========Payment Cycle==========================
                                If clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) >= clsCommon.myCdbl(row.Item("Slab_From")) And clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) < clsCommon.myCdbl(row.Item("Slab_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Dim incentive_value As Decimal = clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", ""))
                                    ArrReturn.Add(incentive_value)
                                    clsMilkPurchaseInvoiceMCC.Calculate_Incentive2(dtAllData, Nothing, incentive_value, clsCommon.myCdbl(row("Rate")), trans, "")
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "TSB") = CompairStringResult.Equal Then '=============Total Solid===========Payment Cycle=====================
                                If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("Slab_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("Slab_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    ArrReturn.Add(clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")))
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QTSSLAB") = CompairStringResult.Equal Then '=============Quantity and Total Solid===========Payment Cycle=====================
                                If (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("TS_To")) And clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) >= clsCommon.myCdbl(row.Item("Slab_From")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Dim incentive_value As Decimal = clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", ""))
                                    ArrReturn.Add(incentive_value)

                                    clsMilkPurchaseInvoiceMCC.Calculate_Incentive2(dtAllData, Nothing, incentive_value, clsCommon.myCdbl(row("Rate")), trans, "")
                                    Exit For
                                End If
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(row.Item("INCENTIVE_TYPE")), "QSLABTSSLAB") = CompairStringResult.Equal Then '=============Slab Based and Total Solid===========Payment Cycle=====================
                                If clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) >= clsCommon.myCdbl(row.Item("SLAB_From")) And clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", "")) < clsCommon.myCdbl(row.Item("SLAB_To")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) >= clsCommon.myCdbl(row.Item("TS_From")) And (clsCommon.myCdbl(dtAllData.Compute("SUM(FAT_Per)", "")) + clsCommon.myCdbl(dtAllData.Compute("SUM(SNF_Per)", ""))) <= clsCommon.myCdbl(row.Item("TS_To")) Then
                                    ArrReturn.Add(row("Rate"))
                                    Dim incentive_value As Decimal = clsCommon.myCdbl(row("Rate")) * clsCommon.myCdbl(dtAllData.Compute("SUM(POQty)", ""))
                                    ArrReturn.Add(incentive_value)
                                    clsMilkPurchaseInvoiceMCC.Calculate_Incentive2(dtAllData, Nothing, incentive_value, clsCommon.myCdbl(row("Rate")), trans, "")
                                    Exit For
                                End If
                            End If
                        End If
                    End If
                Next
            Next
        End If
        If ArrReturn.Count > 2 Then
            Dim counter As Integer = 1
            Dim Incentive_Value As Double = 0
            For Each row As String In ArrReturn
                If counter > 2 And counter Mod 2 = 0 Then
                    Incentive_Value += clsCommon.myCdbl(row)
                End If
                counter += 1
            Next
            ArrReturn(1) += Incentive_Value
        End If
        Return ArrReturn

    End Function

End Class

