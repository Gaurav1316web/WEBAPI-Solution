Imports common
Imports System.Data.SqlClient
Public Class clsPJVHead
#Region "Variables"
    Public PJV_No As String = Nothing
    Public Vendor_Invoice_No As String = Nothing
    Public PJV_Date As DateTime = Nothing
    Public Vendor_Code As String = Nothing
    Public Vendor_Name As String = Nothing
    Public PO_No As String = Nothing
    Public PO_Date As Date? = Nothing
    Public SRN_No As String = Nothing
    Public SRN_Date As String = Nothing
    Public Invoice_No As String = Nothing
    Public Invoice_Date As String = Nothing
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Posting_Date As String = Nothing
    Public PJV_Amount As Double = 0
    Public PJV_TDS_Amount As Double = 0
    Public PJV_Net_Amount As Double = 0
    Public Narration As String = Nothing
    Public Due_Date As String = Nothing
    Public Comp_Code As String = Nothing
    Public Dept As String = Nothing
    Public Dept_Desc As String = Nothing
    Public Arr As List(Of clsPJVDetail) = Nothing
#End Region
    Public Function SaveData(ByVal obj As clsPJVHead, ByVal strPINO As String, ByVal strLocationCode As String, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "delete from TSPL_PJV_Detail where PJV_No='" + obj.PJV_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""
            If isNewEntry Then
                obj.PJV_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.PJV_Date), clsDocType.PJV, "", strLocationCode)
            End If
            If (clsCommon.myLen(obj.PJV_No) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "PJV_Date", clsCommon.GetPrintDate(obj.PJV_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Vendor_Invoice_No", obj.Vendor_Invoice_No)
            clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
            clsCommon.AddColumnsForChange(coll, "Vendor_Name", obj.Vendor_Name)
            clsCommon.AddColumnsForChange(coll, "PO_No", obj.PO_No, True)
            If obj.PO_Date.HasValue Then
                clsCommon.AddColumnsForChange(coll, "PO_Date", clsCommon.GetPrintDate(obj.PO_Date, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "PO_Date", Nothing, True)
            End If


            clsCommon.AddColumnsForChange(coll, "SRN_No", obj.SRN_No, True)
            clsCommon.AddColumnsForChange(coll, "SRN_Date", clsCommon.GetPrintDate(obj.SRN_Date, "dd/MM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Invoice_No", strPINO, True)
            clsCommon.AddColumnsForChange(coll, "Invoice_Date", clsCommon.GetPrintDate(obj.Invoice_Date, "dd/MM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "PJV_Amount", obj.PJV_Amount)
            clsCommon.AddColumnsForChange(coll, "PJV_TDS_Amount", obj.PJV_TDS_Amount)
            clsCommon.AddColumnsForChange(coll, "PJV_Net_Amount", obj.PJV_Net_Amount)
            clsCommon.AddColumnsForChange(coll, "Narration", obj.Narration)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            If clsCommon.myLen(obj.Due_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Due_Date", clsCommon.GetPrintDate(obj.Due_Date, "dd/MM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "Due_Date", Nothing, True)
            End If
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Dept", obj.Dept)
            clsCommon.AddColumnsForChange(coll, "Dept_Desc", obj.Dept_Desc)
            If isNewEntry Then

                clsCommon.AddColumnsForChange(coll, "PJV_No", obj.PJV_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PJV_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PJV_HEAD", OMInsertOrUpdate.Update, "TSPL_PJV_HEAD.PJV_No='" + obj.PJV_No + "'", trans)
            End If
            isSaved = isSaved AndAlso clsPJVDetail.SaveData(obj.PJV_No, Arr, trans)

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsPJVHead
        Dim obj As clsPJVHead = Nothing
        Dim qry As String = "SELECT TSPL_PJV_HEAD.PJV_No,TSPL_PJV_HEAD.PJV_Date,TSPL_PJV_HEAD.Vendor_Invoice_No,TSPL_PJV_HEAD.Vendor_Code,TSPL_PJV_HEAD.Vendor_Name,TSPL_PJV_HEAD.Status,TSPL_PJV_HEAD.PO_Date,TSPL_PJV_HEAD.SRN_Date,TSPL_PJV_HEAD.Invoice_Date,TSPL_PJV_HEAD.PJV_Amount,TSPL_PJV_HEAD.PJV_TDS_Amount,TSPL_PJV_HEAD.PJV_Net_Amount,TSPL_PJV_HEAD.Narration,TSPL_PJV_HEAD.Comp_Code,TSPL_PJV_HEAD.Due_Date ,TSPL_PJV_HEAD.Posting_Date,TSPL_PJV_HEAD.PO_No,TSPL_PJV_HEAD.Invoice_No,TSPL_PJV_HEAD.SRN_No,TSPL_PJV_HEAD.Dept,TSPL_PJV_HEAD.Dept_Desc FROM TSPL_PJV_HEAD where 2=2 and TSPL_PJV_HEAD.Invoice_No= '" + strPONo + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsPJVHead()
            obj.PJV_No = clsCommon.myCstr(dt.Rows(0)("PJV_No"))
            obj.PJV_Date = clsCommon.myCstr(dt.Rows(0)("PJV_Date"))
            obj.Vendor_Invoice_No = clsCommon.myCstr(dt.Rows(0)("Vendor_Invoice_No"))
            obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
            obj.Vendor_Name = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
            obj.PO_No = clsCommon.myCstr(dt.Rows(0)("PO_No"))
            obj.Dept = clsCommon.myCstr(dt.Rows(0)("Dept"))
            obj.Dept_Desc = clsCommon.myCstr(dt.Rows(0)("Dept_Desc"))

            If dt.Rows(0)("PO_Date") IsNot DBNull.Value Then
                obj.PO_Date = clsCommon.myCDate(dt.Rows(0)("PO_Date"))
            End If


            obj.SRN_No = clsCommon.myCstr(dt.Rows(0)("SRN_No"))
            obj.SRN_Date = clsCommon.myCstr(dt.Rows(0)("SRN_Date"))
            obj.Invoice_No = clsCommon.myCstr(dt.Rows(0)("Invoice_No"))
            obj.Invoice_Date = clsCommon.myCstr(dt.Rows(0)("Invoice_Date"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.Posting_Date = clsCommon.myCstr(dt.Rows(0)("Posting_Date"))
            obj.PJV_Amount = clsCommon.myCdbl(dt.Rows(0)("PJV_Amount"))
            obj.PJV_TDS_Amount = clsCommon.myCdbl(dt.Rows(0)("PJV_TDS_Amount"))
            obj.PJV_Net_Amount = clsCommon.myCdbl(dt.Rows(0)("PJV_Net_Amount"))
            obj.Narration = clsCommon.myCstr(dt.Rows(0)("Narration"))
            obj.Comp_Code = clsCommon.myCstr(dt.Rows(0)("Comp_Code"))
            obj.Due_Date = clsCommon.myCstr(dt.Rows(0)("Due_Date"))


            qry = "SELECT TSPL_PJV_Detail.PJV_No,TSPL_PJV_Detail.Line_No,TSPL_PJV_Detail.GL_Account_Code,TSPL_PJV_Detail.GL_Account_Desc,TSPL_PJV_Detail.PJV_Amount FROM TSPL_PJV_Detail where TSPL_PJV_Detail.PJV_No='" + obj.PJV_No + "' ORDER BY TSPL_PJV_Detail.Line_No"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsPJVDetail)
                Dim objTr As clsPJVDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsPJVDetail
                    objTr.PJV_No = clsCommon.myCstr(dr("PJV_No"))
                    objTr.Line_No = clsCommon.myCstr(dr("Line_No"))
                    objTr.GL_Account_Code = clsCommon.myCstr(dr("GL_Account_Code"))
                    objTr.GL_Account_Desc = clsCommon.myCstr(dr("GL_Account_Desc"))
                    objTr.PJV_Amount = clsCommon.myCdbl(dr("PJV_Amount"))
                    obj.Arr.Add(objTr)
                Next
            End If
        End If

        Return obj
    End Function

    Private Shared Function GetFirstItemCode(ByVal Arr As List(Of clsPurchaseInvoiceDetail)) As String
        For Each objtr As clsPurchaseInvoiceDetail In Arr
            If clsCommon.CompairString(objtr.Row_Type, clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                Return objtr.Item_Code
            End If
        Next
        Return ""
    End Function

    Function GetTaxAmt(ByVal objPIDetail As clsPurchaseInvoiceDetail) As Double
        Return GetTaxAmt(objPIDetail, Nothing)
    End Function
    Function GetTaxAmt(ByVal objPIDetail As clsPurchaseInvoiceDetail, ByVal tran As SqlTransaction) As Double
        Dim dblTotalTax As Double = 0
        Dim isTaxRecoverable As Boolean = False
        If objPIDetail.TAX1_Amt > 0 AndAlso Not clsTaxMaster.IsTaxRecoverableAC(objPIDetail.TAX1, tran) Then
            dblTotalTax += objPIDetail.TAX1_Amt
        End If
        If objPIDetail.TAX2_Amt > 0 AndAlso Not clsTaxMaster.IsTaxRecoverableAC(objPIDetail.TAX2, tran) Then
            dblTotalTax += objPIDetail.TAX2_Amt
        End If
        If objPIDetail.TAX3_Amt > 0 AndAlso Not clsTaxMaster.IsTaxRecoverableAC(objPIDetail.TAX3, tran) Then
            dblTotalTax += objPIDetail.TAX3_Amt
        End If
        If objPIDetail.TAX4_Amt > 0 AndAlso Not clsTaxMaster.IsTaxRecoverableAC(objPIDetail.TAX4, tran) Then
            dblTotalTax += objPIDetail.TAX4_Amt
        End If
        If objPIDetail.TAX5_Amt > 0 AndAlso Not clsTaxMaster.IsTaxRecoverableAC(objPIDetail.TAX5, tran) Then
            dblTotalTax += objPIDetail.TAX5_Amt
        End If
        If objPIDetail.TAX6_Amt > 0 AndAlso Not clsTaxMaster.IsTaxRecoverableAC(objPIDetail.TAX6, tran) Then
            dblTotalTax += objPIDetail.TAX6_Amt
        End If
        If objPIDetail.TAX7_Amt > 0 AndAlso Not clsTaxMaster.IsTaxRecoverableAC(objPIDetail.TAX7, tran) Then
            dblTotalTax += objPIDetail.TAX7_Amt
        End If
        If objPIDetail.TAX8_Amt > 0 AndAlso Not clsTaxMaster.IsTaxRecoverableAC(objPIDetail.TAX8, tran) Then
            dblTotalTax += objPIDetail.TAX8_Amt
        End If
        If objPIDetail.TAX9_Amt > 0 AndAlso Not clsTaxMaster.IsTaxRecoverableAC(objPIDetail.TAX9, tran) Then
            dblTotalTax += objPIDetail.TAX9_Amt
        End If
        If objPIDetail.TAX10_Amt > 0 AndAlso Not clsTaxMaster.IsTaxRecoverableAC(objPIDetail.TAX10, tran) Then
            dblTotalTax += objPIDetail.TAX10_Amt
        End If
        Return dblTotalTax
    End Function


    Public Function SetPJVData(ByVal strPJVNo As String, ByVal objPI As clsPurchaseInvoiceHead, ByVal trans As SqlTransaction) As clsPJVHead
        Dim obj As New clsPJVHead
        Try
            Dim dblConvertRate As Double = IIf(objPI.ConvRate = 0, 1, objPI.ConvRate) ''BM00000007657 Ticket by balwinder on 26/08/2015 

            obj.PJV_No = strPJVNo
            obj.Vendor_Invoice_No = objPI.Vendor_Invoice_No
            obj.PJV_Date = objPI.PI_Date
            obj.Vendor_Code = objPI.Vendor_Code
            obj.Vendor_Name = objPI.Vendor_Name
            obj.PO_No = objPI.Against_PO
            obj.Dept = objPI.Dept
            obj.Dept_Desc = objPI.Dept_Desc
            obj.Narration = objPI.Description
            If clsCommon.myLen(obj.PO_No) > 0 Then
                obj.PO_Date = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select PurchaseOrder_Date from TSPL_PURCHASE_ORDER_HEAD where PurchaseOrder_No='" + obj.PO_No + "'", trans))
            End If
            obj.SRN_No = objPI.Against_SRN
            If clsCommon.myLen(obj.SRN_No) > 0 Then
                obj.SRN_Date = clsDBFuncationality.getSingleValue("select SRN_Date from TSPL_SRN_HEAD where SRN_No='" + obj.SRN_No + "'", trans)
            End If
            obj.Invoice_No = objPI.PI_No
            obj.Invoice_Date = objPI.PI_Date
            obj.PJV_Net_Amount = (objPI.PI_Total_Amt) * dblConvertRate
            If objPI.objPIRemittance IsNot Nothing Then
                obj.PJV_TDS_Amount = objPI.objPIRemittance.Calculated_Total_TDS * dblConvertRate
            End If
            obj.PJV_Amount = (objPI.PI_Total_Amt - obj.PJV_TDS_Amount) * dblConvertRate
            obj.Due_Date = objPI.Due_Date

            Dim ii As Integer = 1


            Dim ArrTemp As List(Of clsPJVDetail) = New List(Of clsPJVDetail)

            ArrTemp = New List(Of clsPJVDetail)
            Dim Account_Set As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objPI.Vendor_Code + "'", trans))
            If (clsCommon.myLen(Account_Set) < 0) Then
                Throw New Exception("Please set the vendor Account Set For Vendor : " + objPI.Vendor_Name)
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Payable_Account,TSPL_GL_ACCOUNTS.Description,Discount_Account  from TSPL_VENDOR_ACCOUNT_SET left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_VENDOR_ACCOUNT_SET.Payable_Account where TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code='" + Account_Set + "'", trans)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Vendor's Control Account not found for Purchase jouranl Voucher")
            End If

            Dim strPayableAc As String = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
            strPayableAc = clsERPFuncationality.ChangeGLAccountLocationSegment(strPayableAc, objPI.Bill_To_Location, trans)


            Dim objTR As clsPJVDetail = New clsPJVDetail()
            Dim isFirstTime As Boolean = True
            Dim strFirstItemCodeNonItemRowType As String = GetFirstItemCode(objPI.Arr)

            Dim isAddGSTAccounts As Boolean = clsERPFuncationality.GetGSTStatus(obj.PJV_Date)
            If Not (clsVendorMaster.IsGSTRegisteredVendor(obj.Vendor_Code, trans)) Then
                isAddGSTAccounts = isAddGSTAccounts AndAlso True
            Else
                isAddGSTAccounts = isAddGSTAccounts AndAlso False
            End If
            isAddGSTAccounts = isAddGSTAccounts AndAlso clsTaxGroupMaster.IsHavingRecoverableTaxAuthority(objPI.Tax_Group, "P", trans)

            

            Dim objBalAdvTaxAmt As clsPOAdvanceAdjustmentKnockOff = Nothing
            If isAddGSTAccounts Then
                objBalAdvTaxAmt = clsPOAdvanceAdjustmentKnockOff.GetBalanceAdvanceAmt(objPI.PI_No, "PI", trans)
            End If

            ''Dim dblEmpyAmount As Double = 0
            Dim strEmptyAccount As String = ""
            For Each objPIDetail As clsPurchaseInvoiceDetail In objPI.Arr
                ''Fill VendorInvoice details Data
                Dim strICode As String = objPIDetail.Item_Code
                If clsCommon.CompairString(objPIDetail.Row_Type, clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                    strICode = strFirstItemCodeNonItemRowType
                End If

                Dim qry As String = "select TSPL_PURCHASE_ACCOUNTS.Purchase_JobWork, TSPL_ITEM_MASTER.Purchase_Class_Code,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing,TSPL_GL_ACCOUNTS.Description as ClearingACName, TSPL_ITEM_MASTER.Two_Count_Status as isEmpty,TSPL_PURCHASE_ACCOUNTS.Non_Stock_Clearing as EmptyAccount from TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing where TSPL_ITEM_MASTER.Item_Code='" + strICode + "'"
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dt1 Is Nothing OrElse dt1.Rows.Count <= 0 Then
                    Throw New Exception("Please set Purchase Account set for item " + strICode + "(" + objPIDetail.Item_Desc + ")")
                End If

                Dim strPaybleCleanigCtrlAC As String = ""
                If objPI.isJobWorkOutward = 1 Then
                    If clsCommon.myLen(clsCommon.myCstr(dt1.Rows(0)("Purchase_JobWork"))) = 0 Then
                        Throw New Exception("Please set Purchase Job Work Account for Account set " + clsCommon.myCstr(dt1.Rows(0)("Purchase_Class_Code")))
                    End If
                    strPaybleCleanigCtrlAC = clsCommon.myCstr(dt1.Rows(0)("Purchase_JobWork"))
                    strPaybleCleanigCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strPaybleCleanigCtrlAC, objPI.Bill_To_Location, trans)
                Else
                    strPaybleCleanigCtrlAC = clsCommon.myCstr(dt1.Rows(0)("Inv_Payable_Clearing"))
                    strPaybleCleanigCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strPaybleCleanigCtrlAC, objPI.Bill_To_Location, trans)
                End If

                Dim strPaybleCleanigCtrlACName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + strPaybleCleanigCtrlAC + "'", trans))
                Dim dblRecoverableAmt As Double = GetTaxAmt(objPIDetail, trans)
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strPaybleCleanigCtrlAC
                objTR.GL_Account_Desc = strPaybleCleanigCtrlACName
                objTR.PJV_Amount = objPIDetail.Landed_Cost_Amount * dblConvertRate ''+ dblRecoverableAmt + IIf(isFirstTime, objPI.Total_Add_Charge, 0)
                ArrTemp.Add(objTR)
                ii = ii + 1
                'totDrAmt = totDrAmt + objPIDetail.Amount + dblRecoverableAmt + IIf(isFirstTime, objPI.Total_Add_Charge - objPI.Discount_Amt, 0)
                isFirstTime = False

                If objPIDetail.Empty_Amount > 0 AndAlso clsCommon.myLen(strEmptyAccount) <= 0 Then
                    strEmptyAccount = clsCommon.myCstr(dt1.Rows(0)("EmptyAccount"))
                    strEmptyAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(strEmptyAccount, objPI.Bill_To_Location, trans)
                End If

                'If clsCommon.CompairString(clsCommon.myCstr(dt1.Rows(0)("isEmpty")), "Y") = CompairStringResult.Equal Then
                '    Dim dblVal As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DefaultValue, objPIDetail.Unit_code, Nothing))
                '    dblEmpyAmount += dblVal * objPIDetail.PI_Qty
                'End If
            Next

            If objPI.Tot_Empty_Amount > 0 Then
                If clsCommon.myLen(strEmptyAccount) <= 0 Then
                    Throw New Exception("Please set Inventory Control Empties")
                End If
                obj.PJV_Amount += (objPI.Tot_Empty_Amount * dblConvertRate)
            End If


            Dim strRecAc As String = ""
            Dim isTaxRecoerable As Boolean = False

            Dim amtCal As Double = 0
            Dim objTM As clsTaxMaster = clsTaxMaster.GetData(objPI.TAX1, trans)
            If objTM IsNot Nothing AndAlso objTM.Tax_Recoverable Then
                isTaxRecoerable = False
                If clsCommon.myLen(objTM.Tax_Recoverable_Account) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX1_Amt * objTM.Tax_Recover_Rate / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If

                If clsCommon.myLen(objTM.Tax_Recoverable_Account2) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account2, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX1_Amt * objTM.Tax_Recover_Rate2 / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If
                If clsCommon.myLen(objTM.Tax_Recoverable_Account3) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account3, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX1_Amt * objTM.Tax_Recover_Rate3 / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If
                If clsCommon.myLen(objTM.Tax_Recoverable_Account4) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account4, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX1_Amt * objTM.Tax_Recover_Rate4 / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If
                If clsCommon.myLen(objTM.Tax_Recoverable_Account5) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account5, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX1_Amt * objTM.Tax_Recover_Rate5 / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If

                If isAddGSTAccounts AndAlso isTaxRecoerable Then
                    If clsCommon.myLen(objTM.PayableControl) <= 0 Then
                        Throw New Exception("Please map payable control A/C in Tax authority :" + objPI.TAX1)
                    End If
                    objTM.PayableControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.PayableControl, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = objTM.PayableControl
                    objTR.GL_Account_Desc = clsGLAccount.GetName(objTM.PayableControl, trans)
                    amtCal = Math.Round(-1 * objPI.TAX1_Amt, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1

                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strPayableAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strPayableAc, trans)
                    amtCal = Math.Round(objPI.TAX1_Amt, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1

                    If objBalAdvTaxAmt IsNot Nothing Then
                        If objBalAdvTaxAmt.TAX1_Amt <> 0 Then

                            objTR = New clsPJVDetail()
                            objTR.Line_No = ii
                            objTR.GL_Account_Code = objTM.PayableControl
                            objTR.GL_Account_Desc = clsGLAccount.GetName(objTM.PayableControl, trans)
                            amtCal = Math.Round(objBalAdvTaxAmt.TAX1_Amt, 2, MidpointRounding.ToEven)
                            objTR.PJV_Amount = amtCal * dblConvertRate
                            ArrTemp.Add(objTR)
                            ii = ii + 1

                            objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objPI.Bill_To_Location, trans)
                            If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                                Throw New Exception("Please map deposit control A/C in Tax authority :" + objPI.TAX1)
                            End If
                            objTR = New clsPJVDetail()
                            objTR.Line_No = ii
                            objTR.GL_Account_Code = objTM.DepositControl
                            objTR.GL_Account_Desc = clsGLAccount.GetName(objTM.DepositControl, trans)
                            amtCal = Math.Round(-1 * objBalAdvTaxAmt.TAX1_Amt, 2, MidpointRounding.ToEven)
                            objTR.PJV_Amount = amtCal * dblConvertRate
                            ArrTemp.Add(objTR)
                            ii = ii + 1
                        End If
                    End If
                End If
            End If

            objTM = clsTaxMaster.GetData(objPI.TAX2, trans)
            If objTM IsNot Nothing AndAlso objTM.Tax_Recoverable Then
                isTaxRecoerable = False
                If clsCommon.myLen(objTM.Tax_Recoverable_Account) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX2_Amt * objTM.Tax_Recover_Rate / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If

                If clsCommon.myLen(objTM.Tax_Recoverable_Account2) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account2, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX2_Amt * objTM.Tax_Recover_Rate2 / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If
                If clsCommon.myLen(objTM.Tax_Recoverable_Account3) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account3, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX2_Amt * objTM.Tax_Recover_Rate3 / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If
                If clsCommon.myLen(objTM.Tax_Recoverable_Account4) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account4, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX2_Amt * objTM.Tax_Recover_Rate4 / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If
                If clsCommon.myLen(objTM.Tax_Recoverable_Account5) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account5, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX2_Amt * objTM.Tax_Recover_Rate5 / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If

                If isAddGSTAccounts AndAlso isTaxRecoerable Then
                    If clsCommon.myLen(objTM.PayableControl) <= 0 Then
                        Throw New Exception("Please map payable control A/C in Tax authority :" + objPI.TAX2)
                    End If
                    objTM.PayableControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.PayableControl, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = objTM.PayableControl
                    objTR.GL_Account_Desc = clsGLAccount.GetName(objTM.PayableControl, trans)
                    amtCal = Math.Round(-1 * objPI.TAX2_Amt, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1

                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strPayableAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strPayableAc, trans)
                    amtCal = Math.Round(objPI.TAX2_Amt, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1

                    If objBalAdvTaxAmt IsNot Nothing Then
                        If objBalAdvTaxAmt.TAX2_Amt <> 0 Then

                            objTR = New clsPJVDetail()
                            objTR.Line_No = ii
                            objTR.GL_Account_Code = objTM.PayableControl
                            objTR.GL_Account_Desc = clsGLAccount.GetName(objTM.PayableControl, trans)
                            amtCal = Math.Round(objBalAdvTaxAmt.TAX2_Amt, 2, MidpointRounding.ToEven)
                            objTR.PJV_Amount = amtCal * dblConvertRate
                            ArrTemp.Add(objTR)
                            ii = ii + 1

                            objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objPI.Bill_To_Location, trans)
                            If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                                Throw New Exception("Please map deposit control A/C in Tax authority :" + objPI.TAX2)
                            End If
                            objTR = New clsPJVDetail()
                            objTR.Line_No = ii
                            objTR.GL_Account_Code = objTM.DepositControl
                            objTR.GL_Account_Desc = clsGLAccount.GetName(objTM.DepositControl, trans)
                            amtCal = Math.Round(-1 * objBalAdvTaxAmt.TAX2_Amt, 2, MidpointRounding.ToEven)
                            objTR.PJV_Amount = amtCal * dblConvertRate
                            ArrTemp.Add(objTR)
                            ii = ii + 1
                        End If
                    End If
                End If

            End If
            objTM = clsTaxMaster.GetData(objPI.TAX3, trans)
            If objTM IsNot Nothing AndAlso objTM.Tax_Recoverable Then
                isTaxRecoerable = False
                If clsCommon.myLen(objTM.Tax_Recoverable_Account) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX3_Amt * objTM.Tax_Recover_Rate / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If

                If clsCommon.myLen(objTM.Tax_Recoverable_Account2) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account2, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX3_Amt * objTM.Tax_Recover_Rate2 / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If
                If clsCommon.myLen(objTM.Tax_Recoverable_Account3) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account3, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX3_Amt * objTM.Tax_Recover_Rate3 / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If
                If clsCommon.myLen(objTM.Tax_Recoverable_Account4) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account4, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX3_Amt * objTM.Tax_Recover_Rate4 / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If
                If clsCommon.myLen(objTM.Tax_Recoverable_Account5) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account5, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX3_Amt * objTM.Tax_Recover_Rate5 / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If

                If isAddGSTAccounts AndAlso isTaxRecoerable Then
                    If clsCommon.myLen(objTM.PayableControl) <= 0 Then
                        Throw New Exception("Please map payable control A/C in Tax authority :" + objPI.TAX3)
                    End If
                    objTM.PayableControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.PayableControl, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = objTM.PayableControl
                    objTR.GL_Account_Desc = clsGLAccount.GetName(objTM.PayableControl, trans)
                    amtCal = Math.Round(-1 * objPI.TAX3_Amt, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1

                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strPayableAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strPayableAc, trans)
                    amtCal = Math.Round(objPI.TAX3_Amt, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1

                    If objBalAdvTaxAmt IsNot Nothing Then
                        If objBalAdvTaxAmt.TAX3_Amt <> 0 Then

                            objTR = New clsPJVDetail()
                            objTR.Line_No = ii
                            objTR.GL_Account_Code = objTM.PayableControl
                            objTR.GL_Account_Desc = clsGLAccount.GetName(objTM.PayableControl, trans)
                            amtCal = Math.Round(objBalAdvTaxAmt.TAX3_Amt, 2, MidpointRounding.ToEven)
                            objTR.PJV_Amount = amtCal * dblConvertRate
                            ArrTemp.Add(objTR)
                            ii = ii + 1

                            objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objPI.Bill_To_Location, trans)
                            If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                                Throw New Exception("Please map deposit control A/C in Tax authority :" + objPI.TAX3)
                            End If
                            objTR = New clsPJVDetail()
                            objTR.Line_No = ii
                            objTR.GL_Account_Code = objTM.DepositControl
                            objTR.GL_Account_Desc = clsGLAccount.GetName(objTM.DepositControl, trans)
                            amtCal = Math.Round(-1 * objBalAdvTaxAmt.TAX3_Amt, 2, MidpointRounding.ToEven)
                            objTR.PJV_Amount = amtCal * dblConvertRate
                            ArrTemp.Add(objTR)
                            ii = ii + 1
                        End If
                    End If
                End If
            End If
            objTM = clsTaxMaster.GetData(objPI.TAX4, trans)
            If objTM IsNot Nothing AndAlso objTM.Tax_Recoverable Then
                isTaxRecoerable = False
                If clsCommon.myLen(objTM.Tax_Recoverable_Account) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX4_Amt * objTM.Tax_Recover_Rate / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If

                If clsCommon.myLen(objTM.Tax_Recoverable_Account2) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account2, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX4_Amt * objTM.Tax_Recover_Rate2 / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If
                If clsCommon.myLen(objTM.Tax_Recoverable_Account3) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account3, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX4_Amt * objTM.Tax_Recover_Rate3 / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If
                If clsCommon.myLen(objTM.Tax_Recoverable_Account4) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account4, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX4_Amt * objTM.Tax_Recover_Rate4 / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If
                If clsCommon.myLen(objTM.Tax_Recoverable_Account5) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account5, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX4_Amt * objTM.Tax_Recover_Rate5 / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If

                If isAddGSTAccounts AndAlso isTaxRecoerable Then
                    If clsCommon.myLen(objTM.PayableControl) <= 0 Then
                        Throw New Exception("Please map payable control A/C in Tax authority :" + objPI.TAX4)
                    End If
                    objTM.PayableControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.PayableControl, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = objTM.PayableControl
                    objTR.GL_Account_Desc = clsGLAccount.GetName(objTM.PayableControl, trans)
                    amtCal = Math.Round(-1 * objPI.TAX4_Amt, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1

                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strPayableAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strPayableAc, trans)
                    amtCal = Math.Round(objPI.TAX4_Amt, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1

                    If objBalAdvTaxAmt IsNot Nothing Then
                        If objBalAdvTaxAmt.TAX4_Amt <> 0 Then

                            objTR = New clsPJVDetail()
                            objTR.Line_No = ii
                            objTR.GL_Account_Code = objTM.PayableControl
                            objTR.GL_Account_Desc = clsGLAccount.GetName(objTM.PayableControl, trans)
                            amtCal = Math.Round(objBalAdvTaxAmt.TAX4_Amt, 2, MidpointRounding.ToEven)
                            objTR.PJV_Amount = amtCal * dblConvertRate
                            ArrTemp.Add(objTR)
                            ii = ii + 1

                            objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objPI.Bill_To_Location, trans)
                            If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                                Throw New Exception("Please map deposit control A/C in Tax authority :" + objPI.TAX4)
                            End If
                            objTR = New clsPJVDetail()
                            objTR.Line_No = ii
                            objTR.GL_Account_Code = objTM.DepositControl
                            objTR.GL_Account_Desc = clsGLAccount.GetName(objTM.DepositControl, trans)
                            amtCal = Math.Round(-1 * objBalAdvTaxAmt.TAX4_Amt, 2, MidpointRounding.ToEven)
                            objTR.PJV_Amount = amtCal * dblConvertRate
                            ArrTemp.Add(objTR)
                            ii = ii + 1
                        End If
                    End If
                End If
            End If
            objTM = clsTaxMaster.GetData(objPI.TAX5, trans)
            If objTM IsNot Nothing AndAlso objTM.Tax_Recoverable Then
                isTaxRecoerable = False
                If clsCommon.myLen(objTM.Tax_Recoverable_Account) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX5_Amt * objTM.Tax_Recover_Rate / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If

                If clsCommon.myLen(objTM.Tax_Recoverable_Account2) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account2, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX5_Amt * objTM.Tax_Recover_Rate2 / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If
                If clsCommon.myLen(objTM.Tax_Recoverable_Account3) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account3, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX5_Amt * objTM.Tax_Recover_Rate3 / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If
                If clsCommon.myLen(objTM.Tax_Recoverable_Account4) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account4, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX5_Amt * objTM.Tax_Recover_Rate4 / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If
                If clsCommon.myLen(objTM.Tax_Recoverable_Account5) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account5, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX5_Amt * objTM.Tax_Recover_Rate5 / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If

                If isAddGSTAccounts AndAlso isTaxRecoerable Then
                    If clsCommon.myLen(objTM.PayableControl) <= 0 Then
                        Throw New Exception("Please map payable control A/C in Tax authority :" + objPI.TAX5)
                    End If
                    objTM.PayableControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.PayableControl, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = objTM.PayableControl
                    objTR.GL_Account_Desc = clsGLAccount.GetName(objTM.PayableControl, trans)
                    amtCal = Math.Round(-1 * objPI.TAX5_Amt, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1

                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strPayableAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strPayableAc, trans)
                    amtCal = Math.Round(objPI.TAX5_Amt, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1

                    If objBalAdvTaxAmt IsNot Nothing Then
                        If objBalAdvTaxAmt.TAX5_Amt <> 0 Then

                            objTR = New clsPJVDetail()
                            objTR.Line_No = ii
                            objTR.GL_Account_Code = objTM.PayableControl
                            objTR.GL_Account_Desc = clsGLAccount.GetName(objTM.PayableControl, trans)
                            amtCal = Math.Round(objBalAdvTaxAmt.TAX5_Amt, 2, MidpointRounding.ToEven)
                            objTR.PJV_Amount = amtCal * dblConvertRate
                            ArrTemp.Add(objTR)
                            ii = ii + 1

                            objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objPI.Bill_To_Location, trans)
                            If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                                Throw New Exception("Please map deposit control A/C in Tax authority :" + objPI.TAX5)
                            End If
                            objTR = New clsPJVDetail()
                            objTR.Line_No = ii
                            objTR.GL_Account_Code = objTM.DepositControl
                            objTR.GL_Account_Desc = clsGLAccount.GetName(objTM.DepositControl, trans)
                            amtCal = Math.Round(-1 * objBalAdvTaxAmt.TAX5_Amt, 2, MidpointRounding.ToEven)
                            objTR.PJV_Amount = amtCal * dblConvertRate
                            ArrTemp.Add(objTR)
                            ii = ii + 1
                        End If
                    End If
                End If
            End If
            objTM = clsTaxMaster.GetData(objPI.TAX6, trans)
            If objTM IsNot Nothing AndAlso objTM.Tax_Recoverable Then
                isTaxRecoerable = False
                If clsCommon.myLen(objTM.Tax_Recoverable_Account) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX6_Amt * objTM.Tax_Recover_Rate / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If

                If clsCommon.myLen(objTM.Tax_Recoverable_Account2) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account2, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX6_Amt * objTM.Tax_Recover_Rate2 / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If
                If clsCommon.myLen(objTM.Tax_Recoverable_Account3) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account3, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX6_Amt * objTM.Tax_Recover_Rate3 / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If
                If clsCommon.myLen(objTM.Tax_Recoverable_Account4) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account4, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX6_Amt * objTM.Tax_Recover_Rate4 / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If
                If clsCommon.myLen(objTM.Tax_Recoverable_Account5) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account5, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX6_Amt * objTM.Tax_Recover_Rate5 / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If

                If isAddGSTAccounts AndAlso isTaxRecoerable Then
                    If clsCommon.myLen(objTM.PayableControl) <= 0 Then
                        Throw New Exception("Please map payable control A/C in Tax authority :" + objPI.TAX6)
                    End If
                    objTM.PayableControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.PayableControl, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = objTM.PayableControl
                    objTR.GL_Account_Desc = clsGLAccount.GetName(objTM.PayableControl, trans)
                    amtCal = Math.Round(-1 * objPI.TAX6_Amt, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1

                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strPayableAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strPayableAc, trans)
                    amtCal = Math.Round(objPI.TAX6_Amt, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1

                    If objBalAdvTaxAmt IsNot Nothing Then
                        If objBalAdvTaxAmt.TAX6_Amt <> 0 Then

                            objTR = New clsPJVDetail()
                            objTR.Line_No = ii
                            objTR.GL_Account_Code = objTM.PayableControl
                            objTR.GL_Account_Desc = clsGLAccount.GetName(objTM.PayableControl, trans)
                            amtCal = Math.Round(objBalAdvTaxAmt.TAX6_Amt, 2, MidpointRounding.ToEven)
                            objTR.PJV_Amount = amtCal * dblConvertRate
                            ArrTemp.Add(objTR)
                            ii = ii + 1

                            objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objPI.Bill_To_Location, trans)
                            If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                                Throw New Exception("Please map deposit control A/C in Tax authority :" + objPI.TAX6)
                            End If
                            objTR = New clsPJVDetail()
                            objTR.Line_No = ii
                            objTR.GL_Account_Code = objTM.DepositControl
                            objTR.GL_Account_Desc = clsGLAccount.GetName(objTM.DepositControl, trans)
                            amtCal = Math.Round(-1 * objBalAdvTaxAmt.TAX6_Amt, 2, MidpointRounding.ToEven)
                            objTR.PJV_Amount = amtCal * dblConvertRate
                            ArrTemp.Add(objTR)
                            ii = ii + 1
                        End If
                    End If
                End If
            End If



            objTM = clsTaxMaster.GetData(objPI.TAX7, trans)
            If objTM IsNot Nothing AndAlso objTM.Tax_Recoverable Then
                isTaxRecoerable = False
                If clsCommon.myLen(objTM.Tax_Recoverable_Account) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX7_Amt * objTM.Tax_Recover_Rate / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If

                If clsCommon.myLen(objTM.Tax_Recoverable_Account2) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account2, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX7_Amt * objTM.Tax_Recover_Rate2 / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If
                If clsCommon.myLen(objTM.Tax_Recoverable_Account3) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account3, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX7_Amt * objTM.Tax_Recover_Rate3 / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If
                If clsCommon.myLen(objTM.Tax_Recoverable_Account4) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account4, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX7_Amt * objTM.Tax_Recover_Rate4 / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If
                If clsCommon.myLen(objTM.Tax_Recoverable_Account5) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account5, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX7_Amt * objTM.Tax_Recover_Rate5 / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If
                If isAddGSTAccounts AndAlso isTaxRecoerable Then
                    If clsCommon.myLen(objTM.PayableControl) <= 0 Then
                        Throw New Exception("Please map payable control A/C in Tax authority :" + objPI.TAX7)
                    End If
                    objTM.PayableControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.PayableControl, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = objTM.PayableControl
                    objTR.GL_Account_Desc = clsGLAccount.GetName(objTM.PayableControl, trans)
                    amtCal = Math.Round(-1 * objPI.TAX7_Amt, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1

                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strPayableAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strPayableAc, trans)
                    amtCal = Math.Round(objPI.TAX7_Amt, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1

                    If objBalAdvTaxAmt IsNot Nothing Then
                        If objBalAdvTaxAmt.TAX7_Amt <> 0 Then

                            objTR = New clsPJVDetail()
                            objTR.Line_No = ii
                            objTR.GL_Account_Code = objTM.PayableControl
                            objTR.GL_Account_Desc = clsGLAccount.GetName(objTM.PayableControl, trans)
                            amtCal = Math.Round(objBalAdvTaxAmt.TAX7_Amt, 2, MidpointRounding.ToEven)
                            objTR.PJV_Amount = amtCal * dblConvertRate
                            ArrTemp.Add(objTR)
                            ii = ii + 1

                            objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objPI.Bill_To_Location, trans)
                            If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                                Throw New Exception("Please map deposit control A/C in Tax authority :" + objPI.TAX7)
                            End If
                            objTR = New clsPJVDetail()
                            objTR.Line_No = ii
                            objTR.GL_Account_Code = objTM.DepositControl
                            objTR.GL_Account_Desc = clsGLAccount.GetName(objTM.DepositControl, trans)
                            amtCal = Math.Round(-1 * objBalAdvTaxAmt.TAX7_Amt, 2, MidpointRounding.ToEven)
                            objTR.PJV_Amount = amtCal * dblConvertRate
                            ArrTemp.Add(objTR)
                            ii = ii + 1
                        End If
                    End If
                End If
            End If
            objTM = clsTaxMaster.GetData(objPI.TAX8, trans)
            If objTM IsNot Nothing AndAlso objTM.Tax_Recoverable Then
                isTaxRecoerable = False
                If clsCommon.myLen(objTM.Tax_Recoverable_Account) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX8_Amt * objTM.Tax_Recover_Rate / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If

                If clsCommon.myLen(objTM.Tax_Recoverable_Account2) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account2, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX8_Amt * objTM.Tax_Recover_Rate2 / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If
                If clsCommon.myLen(objTM.Tax_Recoverable_Account3) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account3, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX8_Amt * objTM.Tax_Recover_Rate3 / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If
                If clsCommon.myLen(objTM.Tax_Recoverable_Account4) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account4, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX8_Amt * objTM.Tax_Recover_Rate4 / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If
                If clsCommon.myLen(objTM.Tax_Recoverable_Account5) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account5, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX8_Amt * objTM.Tax_Recover_Rate5 / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If

                If isAddGSTAccounts AndAlso isTaxRecoerable Then
                    If clsCommon.myLen(objTM.PayableControl) <= 0 Then
                        Throw New Exception("Please map payable control A/C in Tax authority :" + objPI.TAX8)
                    End If
                    objTM.PayableControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.PayableControl, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = objTM.PayableControl
                    objTR.GL_Account_Desc = clsGLAccount.GetName(objTM.PayableControl, trans)
                    amtCal = Math.Round(-1 * objPI.TAX8_Amt, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1

                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strPayableAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strPayableAc, trans)
                    amtCal = Math.Round(objPI.TAX8_Amt, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1

                    If objBalAdvTaxAmt IsNot Nothing Then
                        If objBalAdvTaxAmt.TAX8_Amt <> 0 Then

                            objTR = New clsPJVDetail()
                            objTR.Line_No = ii
                            objTR.GL_Account_Code = objTM.PayableControl
                            objTR.GL_Account_Desc = clsGLAccount.GetName(objTM.PayableControl, trans)
                            amtCal = Math.Round(objBalAdvTaxAmt.TAX8_Amt, 2, MidpointRounding.ToEven)
                            objTR.PJV_Amount = amtCal * dblConvertRate
                            ArrTemp.Add(objTR)
                            ii = ii + 1

                            objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objPI.Bill_To_Location, trans)
                            If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                                Throw New Exception("Please map deposit control A/C in Tax authority :" + objPI.TAX8)
                            End If
                            objTR = New clsPJVDetail()
                            objTR.Line_No = ii
                            objTR.GL_Account_Code = objTM.DepositControl
                            objTR.GL_Account_Desc = clsGLAccount.GetName(objTM.DepositControl, trans)
                            amtCal = Math.Round(-1 * objBalAdvTaxAmt.TAX8_Amt, 2, MidpointRounding.ToEven)
                            objTR.PJV_Amount = amtCal * dblConvertRate
                            ArrTemp.Add(objTR)
                            ii = ii + 1
                        End If
                    End If
                End If

            End If
            objTM = clsTaxMaster.GetData(objPI.TAX9, trans)
            If objTM IsNot Nothing AndAlso objTM.Tax_Recoverable Then
                isTaxRecoerable = False
                If clsCommon.myLen(objTM.Tax_Recoverable_Account) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX9_Amt * objTM.Tax_Recover_Rate / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If

                If clsCommon.myLen(objTM.Tax_Recoverable_Account2) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account2, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX9_Amt * objTM.Tax_Recover_Rate2 / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If
                If clsCommon.myLen(objTM.Tax_Recoverable_Account3) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account3, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX9_Amt * objTM.Tax_Recover_Rate3 / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If
                If clsCommon.myLen(objTM.Tax_Recoverable_Account4) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account4, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX9_Amt * objTM.Tax_Recover_Rate4 / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If
                If clsCommon.myLen(objTM.Tax_Recoverable_Account5) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account5, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX9_Amt * objTM.Tax_Recover_Rate5 / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If

                If isAddGSTAccounts AndAlso isTaxRecoerable Then
                    If clsCommon.myLen(objTM.PayableControl) <= 0 Then
                        Throw New Exception("Please map payable control A/C in Tax authority :" + objPI.TAX9)
                    End If
                    objTM.PayableControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.PayableControl, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = objTM.PayableControl
                    objTR.GL_Account_Desc = clsGLAccount.GetName(objTM.PayableControl, trans)
                    amtCal = Math.Round(-1 * objPI.TAX9_Amt, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1

                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strPayableAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strPayableAc, trans)
                    amtCal = Math.Round(objPI.TAX9_Amt, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1

                    If objBalAdvTaxAmt IsNot Nothing Then
                        If objBalAdvTaxAmt.TAX9_Amt <> 0 Then

                            objTR = New clsPJVDetail()
                            objTR.Line_No = ii
                            objTR.GL_Account_Code = objTM.PayableControl
                            objTR.GL_Account_Desc = clsGLAccount.GetName(objTM.PayableControl, trans)
                            amtCal = Math.Round(objBalAdvTaxAmt.TAX9_Amt, 2, MidpointRounding.ToEven)
                            objTR.PJV_Amount = amtCal * dblConvertRate
                            ArrTemp.Add(objTR)
                            ii = ii + 1

                            objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objPI.Bill_To_Location, trans)
                            If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                                Throw New Exception("Please map deposit control A/C in Tax authority :" + objPI.TAX9)
                            End If
                            objTR = New clsPJVDetail()
                            objTR.Line_No = ii
                            objTR.GL_Account_Code = objTM.DepositControl
                            objTR.GL_Account_Desc = clsGLAccount.GetName(objTM.DepositControl, trans)
                            amtCal = Math.Round(-1 * objBalAdvTaxAmt.TAX9_Amt, 2, MidpointRounding.ToEven)
                            objTR.PJV_Amount = amtCal * dblConvertRate
                            ArrTemp.Add(objTR)
                            ii = ii + 1
                        End If
                    End If
                End If
            End If
            objTM = clsTaxMaster.GetData(objPI.TAX10, trans)
            If objTM IsNot Nothing AndAlso objTM.Tax_Recoverable Then
                isTaxRecoerable = False
                If clsCommon.myLen(objTM.Tax_Recoverable_Account) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX10_Amt * objTM.Tax_Recover_Rate / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If

                If clsCommon.myLen(objTM.Tax_Recoverable_Account2) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account2, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX10_Amt * objTM.Tax_Recover_Rate2 / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If
                If clsCommon.myLen(objTM.Tax_Recoverable_Account3) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account3, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX10_Amt * objTM.Tax_Recover_Rate3 / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If
                If clsCommon.myLen(objTM.Tax_Recoverable_Account4) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account4, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX10_Amt * objTM.Tax_Recover_Rate4 / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If
                If clsCommon.myLen(objTM.Tax_Recoverable_Account5) > 0 Then
                    strRecAc = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account5, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strRecAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strRecAc, trans)
                    amtCal = Math.Round(objPI.TAX10_Amt * objTM.Tax_Recover_Rate5 / 100, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + amtCal
                    isTaxRecoerable = True
                End If

                If isAddGSTAccounts AndAlso isTaxRecoerable Then
                    If clsCommon.myLen(objTM.PayableControl) <= 0 Then
                        Throw New Exception("Please map payable control A/C in Tax authority :" + objPI.TAX10)
                    End If
                    objTM.PayableControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.PayableControl, objPI.Bill_To_Location, trans)
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = objTM.PayableControl
                    objTR.GL_Account_Desc = clsGLAccount.GetName(objTM.PayableControl, trans)
                    amtCal = Math.Round(-1 * objPI.TAX10_Amt, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1

                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    objTR.GL_Account_Code = strPayableAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strPayableAc, trans)
                    amtCal = Math.Round(objPI.TAX10_Amt, 2, MidpointRounding.ToEven)
                    objTR.PJV_Amount = amtCal * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1

                    If objBalAdvTaxAmt IsNot Nothing Then
                        If objBalAdvTaxAmt.TAX10_Amt <> 0 Then

                            objTR = New clsPJVDetail()
                            objTR.Line_No = ii
                            objTR.GL_Account_Code = objTM.PayableControl
                            objTR.GL_Account_Desc = clsGLAccount.GetName(objTM.PayableControl, trans)
                            amtCal = Math.Round(objBalAdvTaxAmt.TAX10_Amt, 2, MidpointRounding.ToEven)
                            objTR.PJV_Amount = amtCal * dblConvertRate
                            ArrTemp.Add(objTR)
                            ii = ii + 1

                            objTM.DepositControl = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.DepositControl, objPI.Bill_To_Location, trans)
                            If clsCommon.myLen(objTM.DepositControl) <= 0 Then
                                Throw New Exception("Please map deposit control A/C in Tax authority :" + objPI.TAX10)
                            End If
                            objTR = New clsPJVDetail()
                            objTR.Line_No = ii
                            objTR.GL_Account_Code = objTM.DepositControl
                            objTR.GL_Account_Desc = clsGLAccount.GetName(objTM.DepositControl, trans)
                            amtCal = Math.Round(-1 * objBalAdvTaxAmt.TAX10_Amt, 2, MidpointRounding.ToEven)
                            objTR.PJV_Amount = amtCal * dblConvertRate
                            ArrTemp.Add(objTR)
                            ii = ii + 1
                        End If
                    End If
                End If
            End If
            ''GKD/09/05/18-000130 BY BALWINDER ON 27/05/2018
            If objPI.RoundOffAmt <> 0 Then
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Round_Off from TSPL_VENDOR_ACCOUNT_SET where Acct_Set_Code = '" + Account_Set + "'", trans))
                If clsCommon.myLen(objTR.GL_Account_Code) <= 0 Then
                    Throw New Exception("Please set the Roundoff account in vendor account set for-" + Account_Set)
                End If
                objTR.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objTR.GL_Account_Code, objPI.Bill_To_Location, False, trans)

                'Dim AccRoundInvCR() As String = {strACRoundInvCr, objPI.RoundOffAmount * dblConvRate}
                'ArryLst.Add(AccRoundInvCR)


                objTR.GL_Account_Desc = clsGLAccount.GetName(objTR.GL_Account_Code, trans)
                objTR.PJV_Amount = objPI.RoundOffAmt * dblConvertRate
                ArrTemp.Add(objTR)
                ii = ii + 1
            End If
            objTR = New clsPJVDetail()
            objTR.Line_No = ii
            'Dim strPayableAc As String = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
            'strPayableAc = clsERPFuncationality.ChangeGLAccountLocationSegment(strPayableAc, objPI.Bill_To_Location, Nothing)
            objTR.GL_Account_Code = strPayableAc
            objTR.GL_Account_Desc = clsGLAccount.GetName(strPayableAc, trans) ' 
            objTR.PJV_Amount = -1 * (objPI.PI_Total_Amt + objPI.Tot_Empty_Amount) * dblConvertRate
            'totCrAmt = totCrAmt + (objPI.PI_Total_Amt + objPI.Tot_Empty_Amount)
            ii = ii + 1
            ArrTemp.Add(objTR)

            If objPI.Tot_Empty_Amount > 0 Then
                objTR = New clsPJVDetail()
                objTR.Line_No = ii
                objTR.GL_Account_Code = strEmptyAccount
                objTR.GL_Account_Desc = clsGLAccount.GetName(strEmptyAccount, trans) ' 
                objTR.PJV_Amount = objPI.Tot_Empty_Amount * dblConvertRate
                'totDrAmt = totDrAmt + objPI.Tot_Empty_Amount
                ii = ii + 1
                ArrTemp.Add(objTR)
            End If


            ''If objPI.Discount_Amt > 0 Then
            ''    If clsCommon.myLen(clsCommon.myCstr(dt.Rows(0)("Discount_Account"))) <= 0 Then
            ''        Throw New Exception("Discount GL Account Not found")
            ''    End If
            ''    objTR = New clsPJVDetail()
            ''    objTR.Line_No = ii
            ''    Dim strDisAccount As String = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
            ''    strDisAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(strDisAccount, objPI.Bill_To_Location,trans)
            ''    objTR.GL_Account_Code = strDisAccount

            ''    objTR.GL_Account_Desc = clsGLAccount.GetName(strDisAccount)
            ''    objTR.PJV_Amount = -1 * objPI.Discount_Amt
            ''    totCrAmt = totCrAmt + objPI.Discount_Amt
            ''    ArrTemp.Add(objTR)
            ''    ii = ii + 1
            ''End If

            If (objPI.objPIRemittance IsNot Nothing) Then
                If objPI.objPIRemittance.Actual_Total_TDS <> 0 Then
                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    Dim strPayableAC1 As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(dt.Rows(0)("Payable_Account")), objPI.Bill_To_Location, trans)
                    objTR.GL_Account_Code = strPayableAC1
                    objTR.GL_Account_Desc = clsGLAccount.GetName(strPayableAC1, trans)
                    objTR.PJV_Amount = objPI.objPIRemittance.Actual_Total_TDS * dblConvertRate
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                    'totDrAmt = totDrAmt + objPI.objPIRemittance.Actual_Total_TDS

                    objTR = New clsPJVDetail()
                    objTR.Line_No = ii
                    'Dim STRBranchGLAc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Tax_Account from TSPL_TDS_BRANCH_MASTER where Branch_Code='" + objPI.objPIRemittance.Branch_Code + "'"))
                    Dim STRBranchGLAc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Gl_Account from TSPL_TDS_DEDUCTION_HEAD Where Deduction_Code='" + objPI.objPIRemittance.Deduction_Code + "'", trans))
                    If clsCommon.myLen(STRBranchGLAc) <= 0 Then
                        Throw New Exception("Please set GL Account for Deduction code" + objPI.objPIRemittance.Deduction_Code)
                    End If
                    STRBranchGLAc = clsERPFuncationality.ChangeGLAccountLocationSegment(STRBranchGLAc, objPI.Bill_To_Location, trans)
                    objTR.GL_Account_Code = STRBranchGLAc
                    objTR.GL_Account_Desc = clsGLAccount.GetName(STRBranchGLAc, trans)
                    objTR.PJV_Amount = -1 * objPI.objPIRemittance.Actual_Total_TDS * dblConvertRate
                    'totCrAmt = totCrAmt + objPI.objPIRemittance.Actual_Total_TDS
                    ArrTemp.Add(objTR)
                    ii = ii + 1
                End If
            End If

            obj.Arr = MergePJV(ArrTemp)

            If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                Throw New Exception("No GL Account Found For PJV")
            End If

            Dim dblTotDrAmt As Decimal = 0
            Dim dblTotCrAmt As Decimal = 0
            For jj As Integer = 0 To obj.Arr.Count - 1
                If obj.Arr(jj).PJV_Amount > 0 Then
                    dblTotDrAmt += Math.Round(clsCommon.myCdbl(obj.Arr(jj).PJV_Amount), 2, MidpointRounding.ToEven)
                Else
                    dblTotCrAmt += -1 * Math.Round(clsCommon.myCdbl(obj.Arr(jj).PJV_Amount), 2, MidpointRounding.ToEven)
                End If
            Next
            Dim dblDiffence As Double = dblTotDrAmt - dblTotCrAmt
            dblDiffence = Math.Round(dblDiffence, 2, MidpointRounding.ToEven)
            If Math.Abs(dblDiffence) <= 0.99 Then ''Against ticket BM00000002283 change differece 1 paise to 99 paise
                obj.Arr(0).PJV_Amount = obj.Arr(0).PJV_Amount - dblDiffence ''Working for all four conditions.
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function

    Function MergePJV(ByVal ArrTemp As List(Of clsPJVDetail)) As List(Of clsPJVDetail)
        Dim ArrReturn As List(Of clsPJVDetail) = Nothing
        If ArrTemp IsNot Nothing AndAlso ArrTemp.Count > 0 Then
            ArrReturn = New List(Of clsPJVDetail)
            For Each Str As clsPJVDetail In ArrTemp
                Dim isFound As Boolean = False
                If ArrReturn IsNot Nothing AndAlso ArrReturn.Count > 0 Then
                    For ii As Integer = 0 To ArrReturn.Count - 1
                        If clsCommon.CompairString(ArrReturn(ii).GL_Account_Code, Str.GL_Account_Code) = CompairStringResult.Equal Then
                            isFound = True
                            ArrReturn(ii).PJV_Amount += Str.PJV_Amount
                            Exit For
                        End If
                    Next
                End If

                If Not isFound Then
                    Dim objTR As clsPJVDetail = New clsPJVDetail()
                    objTR.GL_Account_Code = Str.GL_Account_Code
                    objTR.GL_Account_Desc = Str.GL_Account_Desc
                    objTR.PJV_Amount = Str.PJV_Amount
                    ArrReturn.Add(objTR)
                End If
            Next
        End If
        Return ArrReturn
    End Function
End Class
Public Class clsPJVDetail
#Region "Variables"
    Public PJV_No As String = Nothing
    Public Line_No As Integer = 0
    Public GL_Account_Code As String = Nothing
    Public GL_Account_Desc As String = Nothing
    Public PJV_Amount As Double = 0
#End Region
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsPJVDetail), ByVal trans As SqlTransaction) As Boolean

        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim ii As Integer = 1
            For Each obj As clsPJVDetail In Arr
                If obj.PJV_Amount <> 0 Then
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "PJV_No", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "Line_No", ii)
                    clsCommon.AddColumnsForChange(coll, "GL_Account_Code", obj.GL_Account_Code)
                    clsCommon.AddColumnsForChange(coll, "GL_Account_Desc", obj.GL_Account_Desc)
                    clsCommon.AddColumnsForChange(coll, "PJV_Amount", obj.PJV_Amount)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PJV_Detail", OMInsertOrUpdate.Insert, "", trans)
                    ii += 1
                End If
            Next
        End If
        Return True
    End Function
End Class
