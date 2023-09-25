Imports System.Data.SqlClient

Public Class clsNIRQC
#Region "Variables"
    Public Document_No As String = Nothing
    Public Document_Date As DateTime
    Public MRN_No As String = Nothing
    Public QC_Status As Integer = Nothing
    Public QC_Remarks As String = Nothing
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Posted_Date As DateTime? = Nothing
#End Region
    Public Function SaveData(ByVal obj As clsNIRQC, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase Order", "Store Received Note Return", obj.Bill_To_Location, Document_Date, trans)
            Try
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "MRN_No", obj.MRN_No)
                clsCommon.AddColumnsForChange(coll, "QC_Status", obj.QC_Status)
                clsCommon.AddColumnsForChange(coll, "QC_Remarks", obj.QC_Remarks)
                clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                If isNewEntry Then
                    obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.NIRQC, "", "")
                    If (clsCommon.myLen(obj.Document_No) <= 0) Then
                        Throw New Exception("Error in Document Code Generation")
                    End If
                    clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_NIR_QC", OMInsertOrUpdate.Insert, "", trans)
                Else
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_NIR_QC", OMInsertOrUpdate.Update, "TSPL_NIR_QC.Document_No='" + obj.Document_No + "'", trans)
                End If
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_No, "TSPL_NIR_QC", "Document_No", trans)
                trans.Commit()
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsNIRQC
        Dim obj As clsNIRQC = Nothing
        Dim qry As String = "SELECT TSPL_NIR_QC.* from TSPL_NIR_QC  where 2=2"
        Dim whrCls As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_NIR_QC.Document_No = (select MIN(Document_No) from TSPL_NIR_QC WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and TSPL_NIR_QC.Document_No = (select Max(Document_No) from TSPL_NIR_QC WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_NIR_QC.Document_No = '" + strDocNo + "'"
            Case NavigatorType.Next
                qry += " and TSPL_NIR_QC.Document_No = (select Min(Document_No) from TSPL_NIR_QC where Document_No>'" + strDocNo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_NIR_QC.Document_No = (select Max(Document_No) from TSPL_NIR_QC where Document_No<'" + strDocNo + "' " + whrCls + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsNIRQC()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.MRN_No = clsCommon.myCstr(dt.Rows(0)("MRN_No"))
            obj.QC_Status = clsCommon.myCDecimal(dt.Rows(0)("QC_Status"))
            obj.QC_Remarks = clsCommon.myCstr(dt.Rows(0)("QC_Remarks"))
            If clsCommon.myCDecimal(dt.Rows(0)("Status")) = 1 Then
                obj.Status = ERPTransactionStatus.Approved
                obj.Posted_Date = clsCommon.myCDate(dt.Rows(0)("Posted_Date"))
            End If
        End If
        Return obj
    End Function
    Public Shared Function DeleteData(ByVal Doc_No As String) As Boolean
        Dim qry As String
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim obj As clsNIRQC = clsNIRQC.GetData(Doc_No, NavigatorType.Current, trans)
        Try
            If Not (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
                Throw New Exception("Document not found")
            End If

            If (obj.Status = 1) Then
                Throw New Exception("Already Posted on :" + obj.Posted_Date)
            End If

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_No, "TSPL_NIR_QC", "Document_No", trans)
            qry = "delete from TSPL_NIR_QC where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsNIRQC = clsNIRQC.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Post on :" + clsCommon.GetPrintDate(obj.Posted_Date, "dd/MM/yyyy"))
            End If
            If obj.QC_Status = 1 Then
                CreateSRN(obj, trans)
            End If
            Dim qry As String = "Update TSPL_NIR_QC set Status=1, Posted_Date='" + strPostDate + "',Posted_By='" + objCommonVar.CurrentUserCode + "'  where Document_No='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Private Shared Function CreateSRN(ByVal objNIRQc As clsNIRQC, ByVal trans As SqlTransaction) As Boolean
        Dim objMRN As clsMRNHead = clsMRNHead.GetData(objNIRQc.MRN_No, NavigatorType.Current, trans)
        If objMRN IsNot Nothing AndAlso clsCommon.myLen(objMRN.MRN_No) > 0 Then
            Dim obj As clsSRNHead = New clsSRNHead()
            obj.SRN_Date = objNIRQc.Document_Date
            obj.Vendor_Code = objMRN.Vendor_Code
            obj.Vendor_Name = objMRN.Vendor_Name
            obj.Ref_No = objMRN.Ref_No
            obj.Inv_Date = clsCommon.GetPrintDate(obj.SRN_Date, "dd/MMM/yyyy")
            obj.Challan_Date = clsCommon.GetPrintDate(obj.SRN_Date, "dd/MMM/yyyy")
            obj.Total_Tax_Amt = objMRN.Total_Tax_Amt
            obj.Inv_No = Nothing
            obj.is_RGP_Non_Inventory = False
            obj.Bill_To_Location = objMRN.Bill_To_Location
            obj.Ship_To_Location = objMRN.Ship_To_Location
            obj.Comments = objMRN.Comments
            obj.On_Hold = objMRN.On_Hold
            obj.Description = objMRN.Description
            obj.Tax_Group = objMRN.Tax_Group
            obj.Form_38 = Nothing
            obj.Is_Internal = False
            obj.autosrnfromrgp = Nothing

            If clsCommon.myLen(objMRN.TAX1) > 0 Then
                obj.TAX1 = objMRN.TAX1
                obj.TAX1_Rate = objMRN.TAX1_Rate
                obj.TAX1_Base_Amt = objMRN.TAX1_Base_Amt
                obj.TAX1_Amt = objMRN.TAX1_Amt
                obj.AssessableAmt = obj.TAX1_Base_Amt
            End If
            If clsCommon.myLen(objMRN.TAX2) > 0 Then
                obj.TAX2 = objMRN.TAX2
                obj.TAX2_Rate = objMRN.TAX2_Rate
                obj.TAX2_Base_Amt = objMRN.TAX2_Base_Amt
                obj.TAX2_Amt = objMRN.TAX2_Amt
                obj.AssessableAmt = obj.TAX2_Base_Amt
            End If
            If clsCommon.myLen(objMRN.TAX3) > 0 Then
                obj.TAX3 = objMRN.TAX3
                obj.TAX3_Rate = objMRN.TAX3_Rate
                obj.TAX3_Base_Amt = objMRN.TAX3_Base_Amt
                obj.TAX3_Amt = objMRN.TAX3_Amt
                obj.AssessableAmt = obj.TAX3_Base_Amt
            End If
            If clsCommon.myLen(objMRN.TAX4) > 0 Then
                obj.TAX4 = objMRN.TAX4
                obj.TAX4_Rate = objMRN.TAX4_Rate
                obj.TAX4_Base_Amt = objMRN.TAX4_Base_Amt
                obj.TAX4_Amt = objMRN.TAX4_Amt
                obj.AssessableAmt = obj.TAX4_Base_Amt
            End If
            If clsCommon.myLen(objMRN.TAX5) > 0 Then
                obj.TAX5 = objMRN.TAX5
                obj.TAX5_Rate = objMRN.TAX5_Rate
                obj.TAX5_Base_Amt = objMRN.TAX5_Base_Amt
                obj.TAX5_Amt = objMRN.TAX5_Amt
                obj.AssessableAmt = obj.TAX5_Base_Amt
            End If
            If clsCommon.myLen(objMRN.TAX6) > 0 Then
                obj.TAX6 = objMRN.TAX6
                obj.TAX6_Rate = objMRN.TAX6_Rate
                obj.TAX6_Base_Amt = objMRN.TAX6_Base_Amt
                obj.TAX6_Amt = objMRN.TAX6_Amt
                obj.AssessableAmt = obj.TAX6_Base_Amt
            End If
            If clsCommon.myLen(objMRN.TAX7) > 0 Then
                obj.TAX7 = objMRN.TAX7
                obj.TAX7_Rate = objMRN.TAX7_Rate
                obj.TAX7_Base_Amt = objMRN.TAX7_Base_Amt
                obj.TAX7_Amt = objMRN.TAX7_Amt
                obj.AssessableAmt = obj.TAX7_Base_Amt
            End If
            If clsCommon.myLen(objMRN.TAX8) > 0 Then
                obj.TAX8 = objMRN.TAX8
                obj.TAX8_Rate = objMRN.TAX8_Rate
                obj.TAX8_Base_Amt = objMRN.TAX8_Base_Amt
                obj.TAX8_Amt = objMRN.TAX8_Amt
                obj.AssessableAmt = obj.TAX8_Base_Amt
            End If
            If clsCommon.myLen(objMRN.TAX9) > 0 Then
                obj.TAX9 = objMRN.TAX9
                obj.TAX9_Rate = objMRN.TAX9_Rate
                obj.TAX9_Base_Amt = objMRN.TAX9_Base_Amt
                obj.TAX9_Amt = objMRN.TAX9_Amt
                obj.AssessableAmt = obj.TAX9_Base_Amt
            End If
            If clsCommon.myLen(objMRN.TAX10) > 0 Then
                obj.TAX10 = objMRN.TAX10
                obj.TAX10_Rate = objMRN.TAX10_Rate
                obj.TAX10_Base_Amt = objMRN.TAX10_Base_Amt
                obj.TAX10_Amt = objMRN.TAX10_Amt
                obj.AssessableAmt = obj.TAX10_Base_Amt
            End If

            obj.Terms_Code = objMRN.Terms_Code
            obj.Due_Date = objMRN.Due_Date
            obj.Discount_Base = objMRN.Discount_Base
            obj.Discount_Amt = objMRN.Discount_Amt
            obj.Amount_Less_Discount = objMRN.Amount_Less_Discount
            obj.SRN_Total_Amt = objMRN.MRN_Total_Amt

            obj.Carrier = objMRN.Carrier
            obj.VehicleNo = objMRN.VehicleNo
            obj.GRNo = objMRN.GRNo
            obj.GENo = objMRN.GENo

            obj.Landed_Add_Cost = Nothing
            obj.Total_Landed_Cost = Nothing
            obj.GEDate = objMRN.GEDate
            obj.Item_Type = objMRN.Item_Type
            obj.Dept = objMRN.Dept
            obj.Dept_Desc = objMRN.Dept_Desc


            obj.PROJECT_ID = Nothing


            obj.Against_Schedule_Code = objMRN.Against_Schedule_Code
            obj.PurchaseOrder_Type = objMRN.PurchaseOrder_Type

            If clsCommon.myLen(objMRN.Add_Charge_Code1) > 0 Then
                obj.Add_Charge_Code1 = objMRN.Add_Charge_Code1
                obj.Add_Charge_Name1 = objMRN.Add_Charge_Name1
                obj.Add_Charge_Amt1 = objMRN.Add_Charge_Amt1
            End If
            If clsCommon.myLen(objMRN.Add_Charge_Code2) > 0 Then
                obj.Add_Charge_Code2 = objMRN.Add_Charge_Code2
                obj.Add_Charge_Name2 = objMRN.Add_Charge_Name2
                obj.Add_Charge_Amt2 = objMRN.Add_Charge_Amt2
            End If
            If clsCommon.myLen(objMRN.Add_Charge_Code3) > 0 Then
                obj.Add_Charge_Code3 = objMRN.Add_Charge_Code3
                obj.Add_Charge_Name3 = objMRN.Add_Charge_Name3
                obj.Add_Charge_Amt3 = objMRN.Add_Charge_Amt3
            End If
            If clsCommon.myLen(objMRN.Add_Charge_Code4) > 0 Then
                obj.Add_Charge_Code4 = objMRN.Add_Charge_Code4
                obj.Add_Charge_Name4 = objMRN.Add_Charge_Name4
                obj.Add_Charge_Amt4 = objMRN.Add_Charge_Amt4
            End If
            If clsCommon.myLen(objMRN.Add_Charge_Code5) > 0 Then
                obj.Add_Charge_Code5 = objMRN.Add_Charge_Code5
                obj.Add_Charge_Name5 = objMRN.Add_Charge_Name5
                obj.Add_Charge_Amt5 = objMRN.Add_Charge_Amt5
            End If
            If clsCommon.myLen(objMRN.Add_Charge_Code6) > 0 Then
                obj.Add_Charge_Code6 = objMRN.Add_Charge_Code6
                obj.Add_Charge_Name6 = objMRN.Add_Charge_Name6
                obj.Add_Charge_Amt6 = objMRN.Add_Charge_Amt6
            End If
            If clsCommon.myLen(objMRN.Add_Charge_Code7) > 0 Then
                obj.Add_Charge_Code7 = objMRN.Add_Charge_Code7
                obj.Add_Charge_Name7 = objMRN.Add_Charge_Name7
                obj.Add_Charge_Amt7 = objMRN.Add_Charge_Amt7
            End If
            If clsCommon.myLen(objMRN.Add_Charge_Code8) > 0 Then
                obj.Add_Charge_Code8 = objMRN.Add_Charge_Code8
                obj.Add_Charge_Name8 = objMRN.Add_Charge_Name8
                obj.Add_Charge_Amt8 = objMRN.Add_Charge_Amt8
            End If
            If clsCommon.myLen(objMRN.Add_Charge_Code9) > 0 Then
                obj.Add_Charge_Code9 = objMRN.Add_Charge_Code9
                obj.Add_Charge_Name9 = objMRN.Add_Charge_Name9
                obj.Add_Charge_Amt9 = objMRN.Add_Charge_Amt9
            End If
            If clsCommon.myLen(objMRN.Add_Charge_Code10) > 0 Then
                obj.Add_Charge_Code10 = objMRN.Add_Charge_Code10
                obj.Add_Charge_Name10 = objMRN.Add_Charge_Name10
                obj.Add_Charge_Amt10 = objMRN.Add_Charge_Amt10
            End If

            obj.Total_Add_Charge = objMRN.Total_Add_Charge
            obj.Tax_Calculation_Type = objMRN.Tax_Calculation_Type
            obj.is_Excise_On_Qty = False
            obj.IsAbatementPO = 0

            obj.Against_RGP = objMRN.Against_RGP_No
            obj.Against_PO = objMRN.Against_PO
            obj.Against_MRN = objMRN.MRN_No
            If clsCommon.myLen(obj.Against_MRN) > 0 Then
                obj.Against_GRN = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_GRN FROM TSPL_MRN_HEAD WHERE MRN_No='" + obj.Against_MRN + "'", trans))
            End If
            If clsCommon.myLen(obj.Against_GRN) > 0 Then
                obj.Against_PO = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_PO FROM TSPL_GRN_HEAD WHERE GRN_No='" + obj.Against_GRN + "'", trans))
                obj.Against_Schedule_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_Schedule_Code FROM TSPL_GRN_HEAD WHERE GRN_No='" + obj.Against_GRN + "'", trans))
                obj.Against_RGP = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_RGP_No FROM TSPL_GRN_HEAD WHERE GRN_No='" + obj.Against_GRN + "'", trans))
            End If
            If clsCommon.myLen(obj.Against_RGP) > 0 Then
                obj.Against_Schedule_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_Schedule_Code FROM TSPL_RGP_HEAD WHERE RGP_No='" + obj.Against_RGP + "'", trans))
                obj.Against_PO = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT PO_Id FROM TSPL_RGP_HEAD WHERE RGP_No='" + obj.Against_RGP + "'", trans))
            End If
            If clsCommon.myLen(obj.Against_Schedule_Code) > 0 Then
                obj.Against_PO = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT PO_Code FROM TSPL_PO_SCH_HEAD WHERE document_code='" + obj.Against_Schedule_Code + "'", trans))
            End If

            If clsCommon.myLen(obj.Against_PO) > 0 Then
                obj.Against_Requisition = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_Requisition FROM TSPL_PURCHASE_ORDER_HEAD WHERE PurchaseOrder_No='" + obj.Against_PO + "'", trans))
            End If


            '' CurrencConversion
            If clsCommon.myLen(objMRN.CURRENCY_CODE) > 0 Then
                obj.CURRENCY_CODE = objMRN.CURRENCY_CODE
                obj.ConvRate = objMRN.ConvRate

                obj.ApplicableFrom = objMRN.ApplicableFrom
            Else
                obj.CURRENCY_CODE = Nothing
                obj.ConvRate = 1
                obj.ApplicableFrom = Nothing
            End If
            '' end CurrencyConversion
            obj.against_roadpermit = objMRN.RoadPermit_No
            obj.Arr_Road = New List(Of clsSRNRoadPermitDetail)
            obj.agnst_cform = ""
            obj.Arr_CFORM = New List(Of clsSRNCFORMDetail)


            obj.Arr = New List(Of clsSRNDetail)

            If objMRN.Arr IsNot Nothing AndAlso objMRN.Arr.Count > 0 Then
                For Each objMRNTR As clsMRNDetail In objMRN.Arr
                    Dim objTr As New clsSRNDetail()
                    objTr.Line_No = objMRNTR.Line_No
                    objTr.Row_Type = objMRNTR.Row_Type
                    objTr.Item_Code = objMRNTR.Item_Code
                    objTr.UOMWeight = clsItemMaster.GetItemWeightUnit(objMRNTR.Item_Code, trans)
                    objTr.UOMWeightValue = clsItemMaster.GetItemWeightValue(objMRNTR.Item_Code, trans)
                    objTr.Item_Desc = objMRNTR.Item_Desc
                    objTr.Bar_Code = Nothing
                    objTr.SRN_Qty = objMRNTR.MRN_Qty
                    objTr.Rejected_Qty = objMRNTR.Reject_Qty
                    objTr.Freeqty = 0
                    objTr.Unit_code = objMRNTR.Unit_code
                    objTr.PO_ID = objMRNTR.PO_ID
                    objTr.MRN_Id = objMRNTR.MRN_No
                    objTr.Category = objMRNTR.Category
                    objTr.Emergency = IIf(objMRNTR.Emergency = 1, True, False)
                    objTr.Capex_Code = objMRNTR.Capex_Code
                    objTr.Capex_SubCode = objMRNTR.Capex_SubCode
                    objTr.GRN_ID = objMRNTR.GRN_Id
                    If clsCommon.myLen(objTr.MRN_Id) > 0 AndAlso clsCommon.myLen(obj.Against_MRN) <= 0 Then
                        obj.Against_MRN = objTr.MRN_Id
                    End If

                    objTr.RGP_Id = Nothing
                    objTr.Req_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select top 1 isnull(Requisition_Id,'')  from tspl_purchase_order_detail where purchaseOrder_No='" & objTr.PO_ID & "'  and Item_code='" & objTr.Item_Code & "'", trans))
                    objTr.Item_Cost = objMRNTR.Item_Cost
                    objTr.Amount = objMRNTR.Amount
                    objTr.Disc_Type = "0"
                    objTr.Disc_Per = objMRNTR.Disc_Per
                    objTr.Disc_Amt = objMRNTR.Disc_Amt
                    objTr.Amt_Less_Discount = objMRNTR.Amt_Less_Discount
                    objTr.Taxable_Amount_Per = objMRNTR.Taxable_Amount_Per
                    objTr.Taxable_Amount = objTr.Taxable_Amount_Per * objTr.Amt_Less_Discount / 100
                    obj.Total_Taxable_Amount += objTr.Taxable_Amount
                    objTr.TAX1 = objMRNTR.TAX1
                    objTr.TAX1_Base_Amt = objMRNTR.TAX1_Base_Amt
                    objTr.TAX1_Rate = objMRNTR.TAX1_Rate
                    objTr.TAX1_Amt = objMRNTR.TAX1_Amt
                    objTr.TAX2 = objMRNTR.TAX2
                    objTr.TAX2_Base_Amt = objMRNTR.TAX2_Base_Amt
                    objTr.TAX2_Rate = objMRNTR.TAX2_Rate
                    objTr.TAX2_Amt = objMRNTR.TAX2_Amt
                    objTr.TAX3 = objMRNTR.TAX3
                    objTr.TAX3_Base_Amt = objMRNTR.TAX3_Base_Amt
                    objTr.TAX3_Rate = objMRNTR.TAX3_Rate
                    objTr.TAX3_Amt = objMRNTR.TAX3_Amt
                    objTr.TAX4 = objMRNTR.TAX4
                    objTr.TAX4_Base_Amt = objMRNTR.TAX4_Base_Amt
                    objTr.TAX4_Rate = objMRNTR.TAX4_Rate
                    objTr.TAX4_Amt = objMRNTR.TAX4_Amt
                    objTr.TAX5 = objMRNTR.TAX5
                    objTr.TAX5_Base_Amt = objMRNTR.TAX5_Base_Amt
                    objTr.TAX5_Rate = objMRNTR.TAX5_Rate
                    objTr.TAX5_Amt = objMRNTR.TAX5_Amt
                    objTr.TAX6 = objMRNTR.TAX6
                    objTr.TAX6_Base_Amt = objMRNTR.TAX6_Base_Amt
                    objTr.TAX6_Rate = objMRNTR.TAX6_Rate
                    objTr.TAX6_Amt = objMRNTR.TAX6_Amt
                    objTr.TAX7 = objMRNTR.TAX7
                    objTr.TAX7_Base_Amt = objMRNTR.TAX7_Base_Amt
                    objTr.TAX7_Rate = objMRNTR.TAX7_Rate
                    objTr.TAX7_Amt = objMRNTR.TAX7_Amt
                    objTr.TAX8 = objMRNTR.TAX8
                    objTr.TAX8_Base_Amt = objMRNTR.TAX8_Base_Amt
                    objTr.TAX8_Rate = objMRNTR.TAX8_Rate
                    objTr.TAX8_Amt = objMRNTR.TAX8_Amt
                    objTr.TAX9 = objMRNTR.TAX9
                    objTr.TAX9_Base_Amt = objMRNTR.TAX9_Base_Amt
                    objTr.TAX9_Rate = objMRNTR.TAX9_Rate
                    objTr.TAX9_Amt = objMRNTR.TAX9_Amt
                    objTr.TAX10 = objMRNTR.TAX10
                    objTr.TAX10_Base_Amt = objMRNTR.TAX10_Base_Amt
                    objTr.TAX10_Rate = objMRNTR.TAX10_Rate
                    objTr.TAX10_Amt = objMRNTR.TAX10_Amt
                    objTr.Total_Tax_Amt = objMRNTR.Total_Tax_Amt
                    objTr.Item_Net_Amt = objMRNTR.Item_Net_Amt
                    objTr.Location = obj.Bill_To_Location

                    objTr.Landed_Cost_Rate = Nothing
                    objTr.Landed_Cost_Amount = Nothing

                    objTr.MRP = objMRNTR.MRP
                    objTr.AssessableAmt = objMRNTR.AssessableAmt
                    objTr.Batch_No = objMRNTR.Batch_No
                    objTr.Bin_No = Nothing
                    objTr.Expiry_Date = objMRNTR.Expiry_Date

                    objTr.MFG_Date = Nothing
                    objTr.Specification = objMRNTR.Specification
                    objTr.Remarks = objMRNTR.Remarks
                    objTr.Is_Mannual_Amt = Nothing
                    objTr.Fater_Code = Nothing
                    objTr.Fater_Rate = Nothing
                    objTr.Fater_Amt = Nothing


                    objTr.Leak_Qty = objMRNTR.Leak_Qty
                    objTr.Burst_Qty = objMRNTR.Burst_Qty
                    objTr.Short_Qty = objMRNTR.Short_Qty
                    objTr.Rejected_Qty = 0
                    objTr.Short_Qty = 0
                    objTr.Balance_Qty = objTr.SRN_Qty + objTr.Rejected_Qty
                    objTr.PO_Qty = clsPurchaseOrderDetail.GetBalancePOQtyByGRN(objTr.PO_ID, objTr.Item_Code, objTr.GRN_ID, objTr.Unit_code, objTr.MRP, objTr.Assessable, trans)
                    objTr.GRN_Qty = clsGRNDetail.GetBalanceGRNQty(objTr.GRN_ID, objTr.Item_Code, objTr.MRN_Id, objTr.Unit_code, objTr.MRP, objTr.Assessable, trans, objMRNTR.PO_ID)
                    objTr.MRN_Qty = objMRNTR.MRN_Qty

                    objTr.RGP_Qty = Nothing
                    objTr.Schedule_Qty = Nothing
                    objTr.Against_Schedule_Code = Nothing

                    objTr.Total_AddtionalCost_PerUnit = Nothing
                    objTr.Total_NonRecTax_PerUnit = Nothing
                    objTr.Total_RecTax_PerUnit = Nothing

                    objTr.AbatementRate = Nothing
                    objTr.AssessableMRP = Nothing
                    objTr.TotalAssessableMRP = Nothing

                    If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                        obj.Arr.Add(objTr)
                    End If
                Next
            End If

            If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                Throw New Exception("No record found for Auto SRN.")
            End If

            obj.Document_Type = "SRN"
            obj.SaveData(obj, True, trans)
        End If
        Return True
    End Function
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim qry As String = "select TSPL_NIR_QC.Document_No,TSPL_NIR_QC.Document_Date,case when TSPL_NIR_QC.QC_Status=1 then 'OK' else 'Not OK' end as QC_Status,TSPL_NIR_QC.QC_Remarks,case when TSPL_NIR_QC.Status=1 then 'Approved' else 'Pending' end as Status  ,TSPL_NIR_QC.MRN_No from TSPL_NIR_QC
                                    left outer join TSPL_MRN_HEAD on TSPL_MRN_HEAD.mrn_no=TSPL_NIR_QC.mrn_no "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrcls = "  TSPL_MRN_HEAD.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        Dim Str As String = clsCommon.ShowSelectForm("NIRQCFnd", qry, "Document_No", whrcls, curcode, "Document_No", isButtonClicked)
        Return Str
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As clsNIRQC = clsNIRQC.GetData(strCode, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
            If Not (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Transaction status should be posted.")
            End If
            Dim qry As String
            Dim dt As DataTable
            If obj.QC_Status = 1 Then
                qry = "select distinct TSPL_SRN_DETAIL.SRN_No,TSPL_SRN_HEAD.Status from TSPL_SRN_DETAIL 
left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No where TSPL_SRN_DETAIL.MRN_Id ='" + obj.MRN_No + "'"
                dt = New DataTable()
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        If clsCommon.myCDecimal(dr("Status")) = 1 Then
                            clsSRNHead.ReverseAndUnpost(clsCommon.myCstr(dr("SRN_No")), trans)
                        End If
                        clsSRNHead.DeleteData(clsCommon.myCstr(dr("SRN_No")), trans)
                    Next
                End If
            End If
            qry = "update TSPL_NIR_QC set Status=0,Posted_Date=null,Posted_By=null where Document_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class
