Imports common
Imports System.Data.SqlClient

Public Class clsPOWeighment
#Region "Variables"
    Public Weighment_Code As String = Nothing
    Public Weighment_Date As DateTime
    Public Against_GRN_No As String = Nothing
    Public Gross_Weight As Decimal = Nothing
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Posted_Date As DateTime?
    Public Is_Auto_Weighment As Boolean
    Public Arr As List(Of clsPOWeighmentDetail)
    Public ArrGunnyBag As List(Of clsPOWeighmentGunnyBag)
#End Region

    Public Function SaveData(ByVal obj As clsPOWeighment, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "select Weighment_Code,case when Status=1 then 'Posted' else 'Pending' end as Status from TSPL_PO_WEIGHTMENT_HEAD where Against_GRN_No='" + obj.Against_GRN_No + "' and  Weighment_Code not in ('" + obj.Weighment_Code + "') "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Throw New Exception("GRN No " + obj.Against_GRN_No + " is already entered.Weightment GRN No- " + clsCommon.myCstr(dt.Rows(0)("Weighment_Code")) + " and Status " + clsCommon.myCstr(dt.Rows(0)("Status")))
            End If
            qry = "select 1 from TSPL_PO_WEIGHTMENT_HEAD where Status=1 and Weighment_Code in ('" + obj.Weighment_Code + "') "
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Throw New Exception("Gross Weighment Approved of Transaction no -" + obj.Weighment_Code)
            End If
            If Not isNewEntry Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Weighment_Code, "TSPL_PO_WEIGHTMENT_HEAD", "Weighment_Code", "TSPL_PO_WEIGHTMENT_DETAIL", "Weighment_Code", trans)
            End If
            qry = "delete from TSPL_PO_WEIGHTMENT_GUNNY where Weighment_Code='" + obj.Weighment_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PO_WEIGHTMENT_DETAIL where Weighment_Code='" + obj.Weighment_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Weighment_Date", clsCommon.GetPrintDate(obj.Weighment_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Against_GRN_No", obj.Against_GRN_No)
            clsCommon.AddColumnsForChange(coll, "Gross_Weight", obj.Gross_Weight)  '
            clsCommon.AddColumnsForChange(coll, "Is_Auto_Weighment", IIf(obj.Is_Auto_Weighment, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                qry = "select (case when len( isnull( Ship_To_Location,''))>0 then Ship_To_Location  else Bill_To_Location end ) as LocationCode,PurchaseOrder_Type from TSPL_GRN_HEAD where GRN_No='" + obj.Against_GRN_No + "' "
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    Throw New Exception("Gate Entry No:" + obj.Against_GRN_No + " is not found ")
                End If
                Dim isPODocumentTypeWise As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PurchaseCounterOnTransactionType, clsFixedParameterCode.PurchaseCounterOnTransactionType, trans)) = 0, False, True) ''Make Setting Balwinder
                If isPODocumentTypeWise Then
                    If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("PurchaseOrder_Type")), "J") = CompairStringResult.Equal Then
                        obj.Weighment_Code = clsERPFuncationality.GetNextCode(trans, obj.Weighment_Date, clsDocType.POWeightment, clsDocTransactionType.POJobWork, clsCommon.myCstr(dt.Rows(0)("LocationCode")))
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("PurchaseOrder_Type")), "I") = CompairStringResult.Equal Then
                        obj.Weighment_Code = clsERPFuncationality.GetNextCode(trans, obj.Weighment_Date, clsDocType.POWeightment, clsDocTransactionType.POImport, clsCommon.myCstr(dt.Rows(0)("LocationCode")))
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("PurchaseOrder_Type")), "L") = CompairStringResult.Equal Then
                        obj.Weighment_Code = clsERPFuncationality.GetNextCode(trans, obj.Weighment_Date, clsDocType.POWeightment, clsDocTransactionType.PODomestic, clsCommon.myCstr(dt.Rows(0)("LocationCode")))
                    Else
                        Throw New Exception("Type is Not Correct To Generate the Transaction Code")
                    End If
                Else
                    obj.Weighment_Code = clsERPFuncationality.GetNextCode(trans, obj.Weighment_Date, clsDocType.POWeightment, clsDocTransactionType.NA, clsCommon.myCstr(dt.Rows(0)("LocationCode")))
                End If

                If (clsCommon.myLen(obj.Weighment_Code) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Weighment_Code", obj.Weighment_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PO_WEIGHTMENT_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PO_WEIGHTMENT_HEAD", OMInsertOrUpdate.Update, "TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code='" + obj.Weighment_Code + "'", trans)
            End If
            If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                obj.Arr(0).SaveData(obj.Weighment_Code, obj.Arr, trans)
            End If
            If obj.ArrGunnyBag IsNot Nothing AndAlso obj.ArrGunnyBag.Count > 0 Then
                clsPOWeighmentGunnyBag.SaveData(obj.Weighment_Code, obj.ArrGunnyBag, trans)
            End If
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType) As clsPOWeighment
        Return GetData(strDocNo, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsPOWeighment
        Dim obj As clsPOWeighment = Nothing
        Dim qry As String = "select TSPL_PO_WEIGHTMENT_HEAD.* from TSPL_PO_WEIGHTMENT_HEAD right join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No  where 2=2 " + Environment.NewLine
        Dim whrCls As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrCls = " and ( TSPL_GRN_HEAD.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ") or TSPL_GRN_HEAD.Ship_To_Location in (" + objCommonVar.strCurrUserLocations + "))"
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code = (select MIN(Weighment_Code) from TSPL_PO_WEIGHTMENT_HEAD right join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code = (select Max(Weighment_Code) from TSPL_PO_WEIGHTMENT_HEAD right join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Next
                qry += " and TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code = (select Min(Weighment_Code) from TSPL_PO_WEIGHTMENT_HEAD right join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No where Weighment_Code > '" + strDocNo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code = (select Max(Weighment_Code) from TSPL_PO_WEIGHTMENT_HEAD right join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No where Weighment_Code < '" + strDocNo + "' " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code = '" + strDocNo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsPOWeighment()
            obj.Weighment_Code = clsCommon.myCstr(dt.Rows(0)("Weighment_Code"))
            obj.Weighment_Date = clsCommon.myCDate(dt.Rows(0)("Weighment_Date"))
            obj.Against_GRN_No = clsCommon.myCstr(dt.Rows(0)("Against_GRN_No"))
            obj.Gross_Weight = clsCommon.myCdbl(dt.Rows(0)("Gross_Weight"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.Is_Auto_Weighment = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Auto_Weighment")) = 1, True, False)
            If dt.Rows(0)("Posted_Date") IsNot DBNull.Value Then
                obj.Posted_Date = clsCommon.myCDate(dt.Rows(0)("Posted_Date"))
            End If
            obj.Arr = clsPOWeighmentDetail.GetData(obj.Weighment_Code, trans)
            obj.ArrGunnyBag = clsPOWeighmentGunnyBag.GetData(obj.Weighment_Code, trans)
        End If
        Return obj
    End Function

    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Entry No not found to Post")
            End If
            Dim obj As clsPOWeighment = clsPOWeighment.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Weighment_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Gross Weight Already Post on :" + clsCommon.GetPrintDate(obj.Posted_Date, "dd/MM/yyyy"))
            End If

            Dim qry As String = "select Item_Code from TSPL_PO_WEIGHTMENT_DETAIL where Weighment_Code='" + strDocNo + "' and isnull(Tare_Weight,0)<=0 "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Throw New Exception("Can't find the tare weight of item " + clsCommon.myCstr(dt.Rows(0)("Item_Code")))
            End If

            Dim objGRN As clsGRNHead = clsGRNHead.GetData(obj.Against_GRN_No, NavigatorType.Current, trans)
            Dim objMRN As New clsMRNHead
            objMRN.MRN_No = ""
            objMRN.MRN_Date = obj.Weighment_Date
            objMRN.PurchaseOrder_Type = objGRN.PurchaseOrder_Type
            objMRN.Vendor_Code = objGRN.Vendor_Code
            objMRN.Vendor_Name = objGRN.Vendor_Name
            objMRN.Remarks = objGRN.Remarks
            objMRN.Bill_To_Location = objGRN.Bill_To_Location
            objMRN.Ship_To_Location = objGRN.Ship_To_Location
            'objMRN.RAL_Tender_No = objGRN.RAL_Tender_No
            objMRN.Tax_Group = objGRN.Tax_Group
            objMRN.TAX1 = objGRN.TAX1
            objMRN.TAX1_Rate = objGRN.TAX1_Rate
            objMRN.TAX1_Base_Amt = objGRN.TAX1_Base_Amt
            objMRN.TAX1_Amt = objGRN.TAX1_Amt
            objMRN.TAX2 = objGRN.TAX2
            objMRN.TAX2_Rate = objGRN.TAX2_Rate
            objMRN.TAX2_Base_Amt = objGRN.TAX2_Base_Amt
            objMRN.TAX2_Amt = objGRN.TAX2_Amt
            objMRN.TAX3 = objGRN.TAX3
            objMRN.TAX3_Rate = objGRN.TAX3_Rate
            objMRN.TAX3_Base_Amt = objGRN.TAX3_Base_Amt
            objMRN.TAX3_Amt = objGRN.TAX3_Amt
            objMRN.TAX4 = objGRN.TAX4
            objMRN.TAX4_Rate = objGRN.TAX4_Rate
            objMRN.TAX4_Base_Amt = objGRN.TAX4_Base_Amt
            objMRN.TAX4_Amt = objGRN.TAX4_Amt
            objMRN.TAX5 = objGRN.TAX5
            objMRN.TAX5_Rate = objGRN.TAX5_Rate
            objMRN.TAX5_Base_Amt = objGRN.TAX5_Base_Amt
            objMRN.TAX5_Amt = objGRN.TAX5_Amt
            objMRN.TAX6 = objGRN.TAX6
            objMRN.TAX6_Rate = objGRN.TAX6_Rate
            objMRN.TAX6_Base_Amt = objGRN.TAX6_Base_Amt
            objMRN.TAX6_Amt = objGRN.TAX6_Amt
            objMRN.TAX7 = objGRN.TAX7
            objMRN.TAX7_Rate = objGRN.TAX7_Rate
            objMRN.TAX7_Base_Amt = objGRN.TAX7_Base_Amt
            objMRN.TAX7_Amt = objGRN.TAX7_Amt
            objMRN.TAX8 = objGRN.TAX8
            objMRN.TAX8_Rate = objGRN.TAX8_Rate
            objMRN.TAX8_Base_Amt = objGRN.TAX8_Base_Amt
            objMRN.TAX8_Amt = objGRN.TAX8_Amt
            objMRN.TAX9 = objGRN.TAX9
            objMRN.TAX9_Rate = objGRN.TAX9_Rate
            objMRN.TAX9_Base_Amt = objGRN.TAX9_Base_Amt
            objMRN.TAX9_Amt = objGRN.TAX9_Amt
            objMRN.TAX10 = objGRN.TAX10
            objMRN.TAX10_Rate = objGRN.TAX10_Rate
            objMRN.TAX10_Base_Amt = objGRN.TAX10_Base_Amt
            objMRN.TAX10_Amt = objGRN.TAX10_Amt
            objMRN.Total_Tax_Amt = objGRN.Total_Tax_Amt
            objMRN.Discount_Base = objGRN.Discount_Base
            objMRN.Discount_Amt = objGRN.Discount_Amt
            objMRN.Amount_Less_Discount = objGRN.Amount_Less_Discount
            objMRN.MRN_Total_Amt = objGRN.GRN_Total_Amt
            objMRN.Comments = objGRN.Comments
            objMRN.Comp_Code = objGRN.Comp_Code
            objMRN.Terms_Code = objGRN.Terms_Code
            objMRN.TermsName = objGRN.TermsName
            objMRN.Due_Date = objGRN.Due_Date
            objMRN.Carrier = objGRN.Carrier
            objMRN.VehicleNo = objGRN.VehicleNo
            objMRN.GRNo = objGRN.GRNo
            objMRN.GENo = objGRN.GENo
            objMRN.Dept = objGRN.Dept
            objMRN.Dept_Desc = objGRN.Dept_Desc
            objMRN.Item_Type = objGRN.Item_Type
            objMRN.Against_Requisition = objGRN.Against_Requisition
            objMRN.Against_PO = objGRN.Against_PO
            objMRN.Against_GRN = objGRN.GRN_No
            objMRN.Add_Charge_Code1 = objGRN.Add_Charge_Code1
            objMRN.Add_Charge_Name1 = objGRN.Add_Charge_Name1
            objMRN.Add_Charge_Amt1 = objGRN.Add_Charge_Amt1
            objMRN.Add_Charge_Code2 = objGRN.Add_Charge_Code2
            objMRN.Add_Charge_Name2 = objGRN.Add_Charge_Name2
            objMRN.Add_Charge_Amt2 = objGRN.Add_Charge_Amt2
            objMRN.Add_Charge_Code3 = objGRN.Add_Charge_Code3
            objMRN.Add_Charge_Name3 = objGRN.Add_Charge_Name3
            objMRN.Add_Charge_Amt3 = objGRN.Add_Charge_Amt3
            objMRN.Add_Charge_Code4 = objGRN.Add_Charge_Code4
            objMRN.Add_Charge_Name4 = objGRN.Add_Charge_Name4
            objMRN.Add_Charge_Amt4 = objGRN.Add_Charge_Amt4
            objMRN.Add_Charge_Code5 = objGRN.Add_Charge_Code5
            objMRN.Add_Charge_Name5 = objGRN.Add_Charge_Name5
            objMRN.Add_Charge_Amt5 = objGRN.Add_Charge_Amt5
            objMRN.Add_Charge_Code6 = objGRN.Add_Charge_Code6
            objMRN.Add_Charge_Name6 = objGRN.Add_Charge_Name6
            objMRN.Add_Charge_Amt6 = objGRN.Add_Charge_Amt6
            objMRN.Add_Charge_Code7 = objGRN.Add_Charge_Code7
            objMRN.Add_Charge_Name7 = objGRN.Add_Charge_Name7
            objMRN.Add_Charge_Amt7 = objGRN.Add_Charge_Amt7
            objMRN.Add_Charge_Code8 = objGRN.Add_Charge_Code8
            objMRN.Add_Charge_Name8 = objGRN.Add_Charge_Name8
            objMRN.Add_Charge_Amt8 = objGRN.Add_Charge_Amt8
            objMRN.Add_Charge_Code9 = objGRN.Add_Charge_Code9
            objMRN.Add_Charge_Name9 = objGRN.Add_Charge_Name9
            objMRN.Add_Charge_Amt9 = objGRN.Add_Charge_Amt9
            objMRN.Add_Charge_Code10 = objGRN.Add_Charge_Code10
            objMRN.Add_Charge_Name10 = objGRN.Add_Charge_Name10
            objMRN.Add_Charge_Amt10 = objGRN.Add_Charge_Amt10
            objMRN.Total_Add_Charge = objGRN.Total_Add_Charge
            objMRN.CURRENCY_CODE = objGRN.CURRENCY_CODE
            objMRN.ConvRate = objGRN.ConvRate
            objMRN.ApplicableFrom = objGRN.ApplicableFrom
            objMRN.RoadPermit_No = objGRN.RoadPermit_No
            objMRN.RoadPermit_Date = objGRN.RoadPermit_Date
            objMRN.InvoiceNo = objGRN.Invoiceno
            objMRN.InvoiceDate = objGRN.InvoiceDate
            objMRN.Total_Taxable_Amount = objGRN.Total_Taxable_Amount
            objMRN.Arr = New List(Of clsMRNDetail)
            For Each objGRNTR As clsGRNDetail In objGRN.Arr
                Dim objMRNTR As New clsMRNDetail
                objMRNTR.QC_Check = True
                objMRNTR.Line_No = objGRNTR.Line_No
                objMRNTR.Row_Type = objGRNTR.Row_Type
                objMRNTR.Item_Code = objGRNTR.Item_Code
                objMRNTR.Item_Desc = objGRNTR.Item_Desc
                objMRNTR.Leak_Qty = objGRNTR.Leak_Qty
                objMRNTR.Burst_Qty = objGRNTR.Burst_Qty
                objMRNTR.Short_Qty = objGRNTR.Short_Qty
                objMRNTR.GRN_Id = objGRNTR.GRN_No
                objMRNTR.PO_ID = objGRNTR.PO_Id
                objMRNTR.RGP_No = Nothing
                objMRNTR.Balance_Qty = objGRNTR.Balance_Qty
                objMRNTR.Unit_code = objGRNTR.Unit_code
                objMRNTR.Location = objGRNTR.Location
                objMRNTR.LocationName = objGRNTR.LocationName
                objMRNTR.Item_Cost = objGRNTR.Item_Cost
                objMRNTR.TAX1 = objGRNTR.TAX1
                objMRNTR.TAX1_Base_Amt = objGRNTR.TAX1_Base_Amt
                objMRNTR.TAX1_Rate = objGRNTR.TAX1_Rate
                objMRNTR.TAX1_Amt = objGRNTR.TAX1_Amt
                objMRNTR.TAX2 = objGRNTR.TAX2
                objMRNTR.TAX2_Base_Amt = objGRNTR.TAX2_Base_Amt
                objMRNTR.TAX2_Rate = objGRNTR.TAX2_Rate
                objMRNTR.TAX2_Amt = objGRNTR.TAX2_Amt
                objMRNTR.TAX3 = objGRNTR.TAX3
                objMRNTR.TAX3_Base_Amt = objGRNTR.TAX3_Base_Amt
                objMRNTR.TAX3_Rate = objGRNTR.TAX3_Rate
                objMRNTR.TAX3_Amt = objGRNTR.TAX3_Amt
                objMRNTR.TAX4 = objGRNTR.TAX4
                objMRNTR.TAX4_Base_Amt = objGRNTR.TAX4_Base_Amt
                objMRNTR.TAX4_Rate = objGRNTR.TAX4_Rate
                objMRNTR.TAX4_Amt = objGRNTR.TAX4_Amt
                objMRNTR.TAX5 = objGRNTR.TAX5
                objMRNTR.TAX5_Base_Amt = objGRNTR.TAX5_Base_Amt
                objMRNTR.TAX5_Rate = objGRNTR.TAX5_Rate
                objMRNTR.TAX5_Amt = objGRNTR.TAX5_Amt
                objMRNTR.TAX6 = objGRNTR.TAX6
                objMRNTR.TAX6_Base_Amt = objGRNTR.TAX6_Base_Amt
                objMRNTR.TAX6_Rate = objGRNTR.TAX6_Rate
                objMRNTR.TAX6_Amt = objGRNTR.TAX6_Amt
                objMRNTR.TAX7 = objGRNTR.TAX7
                objMRNTR.TAX7_Base_Amt = objGRNTR.TAX7_Base_Amt
                objMRNTR.TAX7_Rate = objGRNTR.TAX7_Rate
                objMRNTR.TAX7_Amt = objGRNTR.TAX7_Amt
                objMRNTR.TAX8 = objGRNTR.TAX8
                objMRNTR.TAX8_Base_Amt = objGRNTR.TAX8_Base_Amt
                objMRNTR.TAX8_Rate = objGRNTR.TAX8_Rate
                objMRNTR.TAX8_Amt = objGRNTR.TAX8_Amt
                objMRNTR.TAX9 = objGRNTR.TAX9
                objMRNTR.TAX9_Base_Amt = objGRNTR.TAX9_Base_Amt
                objMRNTR.TAX9_Rate = objGRNTR.TAX9_Rate
                objMRNTR.TAX9_Amt = objGRNTR.TAX9_Amt
                objMRNTR.TAX10 = objGRNTR.TAX10
                objMRNTR.TAX10_Base_Amt = objGRNTR.TAX10_Base_Amt
                objMRNTR.TAX10_Rate = objGRNTR.TAX10_Rate
                objMRNTR.TAX10_Amt = objGRNTR.TAX10_Amt
                objMRNTR.Amount = objGRNTR.Amount
                objMRNTR.Disc_Per = objGRNTR.Disc_Per
                objMRNTR.Disc_Amt = objGRNTR.Disc_Amt
                objMRNTR.Amt_Less_Discount = objGRNTR.Amt_Less_Discount
                objMRNTR.Taxable_Amount_Per = objGRNTR.Taxable_Amount_Per
                objMRNTR.Taxable_Amount = objGRNTR.Taxable_Amount
                objMRNTR.Total_Tax_Amt = objGRNTR.Total_Tax_Amt
                objMRNTR.Item_Net_Amt = objGRNTR.Item_Net_Amt
                objMRNTR.MRNTax_Group = objGRNTR.GRNTax_Group
                objMRNTR.MRP = objGRNTR.MRP
                objMRNTR.MFG_Date = objGRNTR.MFG_Date
                objMRNTR.Batch_No = objGRNTR.Batch_No
                objMRNTR.Expiry_Date = objGRNTR.Expiry_Date
                objMRNTR.Specification = objGRNTR.Specification
                objMRNTR.Remarks = objGRNTR.Remarks
                objMRNTR.Assessable = objGRNTR.Assessable
                objMRNTR.AssessableAmt = objGRNTR.AssessableAmt
                If clsCommon.CompairString(objGRNTR.Row_Type, "Item") = CompairStringResult.Equal Then ''VIJ/11/12/19-000117 by balwinder on 18/12/2019
                    If objGRNTR.GRN_Qty > obj.Arr(objGRNTR.Line_No - 1).Net_Weight Then
                        objMRNTR.Accept_Qty = obj.Arr(objGRNTR.Line_No - 1).Net_Weight
                        objMRNTR.Short_Qty = Math.Round(objGRNTR.GRN_Qty - obj.Arr(objGRNTR.Line_No - 1).Net_Weight, 3)
                    ElseIf objGRNTR.GRN_Qty < obj.Arr(objGRNTR.Line_No - 1).Net_Weight Then
                        objMRNTR.Excess_Qty = Math.Round(obj.Arr(objGRNTR.Line_No - 1).Net_Weight - objGRNTR.GRN_Qty, 3)
                    Else
                        objMRNTR.Accept_Qty = obj.Arr(objGRNTR.Line_No - 1).Net_Weight
                    End If
                    Dim dbNetWeight As String = clsDBFuncationality.getSingleValue("select Net_Weight from TSPL_PO_WEIGHTMENT_DETAIL where Weighment_Code='" & obj.Weighment_Code & "' and item_code='" + objGRNTR.Item_Code + "' ", trans) ''VIJ/11/12/19-000117 by balwinder on 29/01/2020
                    If clsCommon.myCstr(dbNetWeight) > 0 Then
                        objMRNTR.OrgGRNQty = dbNetWeight
                        objMRNTR.Accept_Qty = dbNetWeight
                        objMRNTR.MRN_Qty = dbNetWeight
                    End If
                End If
                objMRNTR.ItemAdd_Charge_Code1 = objGRNTR.ItemAdd_Charge_Code1
                objMRNTR.ItemAdd_Org_Charge_Amt1 = objGRNTR.ItemAdd_Org_Charge_Amt1
                objMRNTR.ItemAdd_Calc_Charge_Amt1 = objGRNTR.ItemAdd_Calc_Charge_Amt1
                objMRNTR.ItemAdd_Charge_Code9 = objGRNTR.ItemAdd_Charge_Code9
                objMRNTR.ItemAdd_Org_Charge_Amt9 = objGRNTR.ItemAdd_Org_Charge_Amt9
                objMRNTR.ItemAdd_Calc_Charge_Amt9 = objGRNTR.ItemAdd_Calc_Charge_Amt9
                objMRNTR.ItemAdd_Charge_Code8 = objGRNTR.ItemAdd_Charge_Code8
                objMRNTR.ItemAdd_Org_Charge_Amt8 = objGRNTR.ItemAdd_Org_Charge_Amt8
                objMRNTR.ItemAdd_Calc_Charge_Amt8 = objGRNTR.ItemAdd_Calc_Charge_Amt8
                objMRNTR.ItemAdd_Charge_Code7 = objGRNTR.ItemAdd_Charge_Code7
                objMRNTR.ItemAdd_Org_Charge_Amt7 = objGRNTR.ItemAdd_Org_Charge_Amt7
                objMRNTR.ItemAdd_Calc_Charge_Amt7 = objGRNTR.ItemAdd_Calc_Charge_Amt7
                objMRNTR.ItemAdd_Charge_Code6 = objGRNTR.ItemAdd_Charge_Code6
                objMRNTR.ItemAdd_Org_Charge_Amt6 = objGRNTR.ItemAdd_Org_Charge_Amt6
                objMRNTR.ItemAdd_Calc_Charge_Amt6 = objGRNTR.ItemAdd_Calc_Charge_Amt6
                objMRNTR.ItemAdd_Charge_Code5 = objGRNTR.ItemAdd_Charge_Code5
                objMRNTR.ItemAdd_Org_Charge_Amt5 = objGRNTR.ItemAdd_Org_Charge_Amt5
                objMRNTR.ItemAdd_Calc_Charge_Amt5 = objGRNTR.ItemAdd_Calc_Charge_Amt5
                objMRNTR.ItemAdd_Charge_Code4 = objGRNTR.ItemAdd_Charge_Code4
                objMRNTR.ItemAdd_Org_Charge_Amt4 = objGRNTR.ItemAdd_Org_Charge_Amt4
                objMRNTR.ItemAdd_Calc_Charge_Amt4 = objGRNTR.ItemAdd_Calc_Charge_Amt4
                objMRNTR.ItemAdd_Charge_Code3 = objGRNTR.ItemAdd_Charge_Code3
                objMRNTR.ItemAdd_Org_Charge_Amt3 = objGRNTR.ItemAdd_Org_Charge_Amt3
                objMRNTR.ItemAdd_Calc_Charge_Amt3 = objGRNTR.ItemAdd_Calc_Charge_Amt3
                objMRNTR.ItemAdd_Charge_Code2 = objGRNTR.ItemAdd_Charge_Code2
                objMRNTR.ItemAdd_Org_Charge_Amt2 = objGRNTR.ItemAdd_Org_Charge_Amt2
                objMRNTR.ItemAdd_Calc_Charge_Amt2 = objGRNTR.ItemAdd_Calc_Charge_Amt2
                objMRNTR.ItemAdd_Charge_Code10 = objGRNTR.ItemAdd_Charge_Code10
                objMRNTR.ItemAdd_Org_Charge_Amt10 = objGRNTR.ItemAdd_Org_Charge_Amt10
                objMRNTR.ItemAdd_Calc_Charge_Amt10 = objGRNTR.ItemAdd_Calc_Charge_Amt10
                objMRNTR.Total_ItemAdd_Charge = objGRNTR.Total_ItemAdd_Charge
                objMRNTR.Capex_Code = objGRNTR.Capex_Code
                objMRNTR.Capex_SubCode = objGRNTR.Capex_SubCode
                objMRNTR.Category = objGRNTR.Category
                objMRNTR.Emergency = objGRNTR.Emergency
                objMRN.Arr.Add(objMRNTR)
            Next

            objMRN.SaveData(objMRN, True, trans)
            clsMRNHead.PostData(objMRN.MRN_No, trans)

            qry = "Update TSPL_PO_WEIGHTMENT_HEAD set Status=1, Posted_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "',Posted_By='" + objCommonVar.CurrentUserCode + "' where Weighment_Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_PO_WEIGHTMENT_HEAD", "Weighment_Code", trans)
            If objCommonVar.InternalSMSEmailinPurchaseModule = True Then
                CreateInternalEmailSMS(obj, trans)
            End If


            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Private Shared Sub CreateInternalEmailSMS(ByVal obj As clsPOWeighment, ByVal trans As SqlTransaction)
        Dim itemName As String = ""
        Dim UOM As String = ""
        Dim qty As String = ""
        Dim ItemDetail As String = ""
        Dim objGRN As clsGRNHead = Nothing
        Dim dtContent As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject,Notification_Text from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.POWeighment + "2" + "'", trans)

        Dim qry As String = "select TSPL_USER_MASTER.User_Code from TSPL_USER_MASTER "
        If clsCommon.myLen(clsDBFuncationality.getSingleValue("select isnull(Against_Requisition,'') from TSPL_GRN_HEAD where TSPL_GRN_HEAD.GRN_No='" + obj.Against_GRN_No + "'", trans)) > 0 Then
            qry += " left join TSPL_REQUISITION_HEAD on TSPL_REQUISITION_HEAD.Created_By=TSPL_USER_MASTER.User_Code left join TSPL_PURCHASE_ORDER_HEAD ON TSPL_PURCHASE_ORDER_HEAD.Against_Requisition=TSPL_REQUISITION_HEAD.Requisition_Id "
        Else
            qry += " left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.Created_By=TSPL_USER_MASTER.User_Code "
        End If
        qry += " left join TSPL_GRN_HEAD on TSPL_GRN_HEAD.Against_PO=TSPL_PURCHASE_ORDER_HEAD.PURCHASEORDER_no "
        qry += " where TSPL_GRN_HEAD.GRN_No='" + obj.Against_GRN_No + "'"
        Dim StrUserCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        Dim arrMobileNo As New List(Of String)
        Dim arrMailID As List(Of String) = clsERPFuncationality.ReportingMailIdandPhone(StrUserCode, arrMobileNo, trans)

        If dtContent IsNot Nothing AndAlso dtContent.Rows.Count > 0 Then
            If (obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0) Then
                For Each objdetail As clsPOWeighmentDetail In obj.Arr
                    itemName = clsItemMaster.GetItemName(clsCommon.myCstr(objdetail.Item_Code), trans)
                    UOM = clsCommon.myCstr(objdetail.UOM)
                    qty = clsCommon.myCstr(objdetail.Net_Weight)
                    ItemDetail += itemName + " " + UOM + "-" + qty + Environment.NewLine
                Next
            End If

            objGRN = clsGRNHead.GetData(obj.Against_GRN_No, NavigatorType.Current, trans)
        End If


        If dtContent IsNot Nothing AndAlso dtContent.Rows.Count > 0 AndAlso ((arrMailID IsNot Nothing AndAlso arrMailID.Count > 0) Or (arrMobileNo IsNot Nothing AndAlso arrMobileNo.Count > 0)) Then

            'Dim qry1 As String = "select TSPL_PO_WEIGHTMENT_detail.UOM as Unit_code,CAST(TSPL_PO_WEIGHTMENT_detail.Net_Weight AS decimal(18,3)) as Qty,isnull(TSPL_ITEM_MASTER.item_desc,'') as item_desc "
            'qry1 += "  from TSPL_PO_WEIGHTMENT_detail left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_PO_WEIGHTMENT_detail.item_code "
            'qry1 += "  where TSPL_PO_WEIGHTMENT_detail.Weighment_Code='" & obj.Weighment_Code & "' ORDER BY TSPL_PO_WEIGHTMENT_detail.SNo"
            'Dim dtDocWise As DataTable = clsDBFuncationality.GetDataTable(qry1, trans)

            'For ii As Integer = 0 To dtDocWise.Rows.Count - 1
            '    itemName = clsCommon.myCstr(dtDocWise.Rows(ii)("item_desc"))
            '    UOM = clsCommon.myCstr(dtDocWise.Rows(ii)("Unit_Code"))
            '    qty = clsCommon.myCstr(dtDocWise.Rows(ii)("Qty"))

            '    ItemDetail += itemName + " " + UOM + "-" + qty + ","
            'Next

            'If (obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0) Then
            '    For Each objdetail As clsPOWeighmentDetail In obj.Arr
            '        itemName = clsItemMaster.GetItemName(clsCommon.myCstr(objdetail.Item_Code), trans)
            '        UOM = clsCommon.myCstr(objdetail.UOM)
            '        qty = clsCommon.myCstr(objdetail.Net_Weight)
            '        ItemDetail += itemName + " " + UOM + "-" + qty + Environment.NewLine
            '    Next
            'End If


            If clsCommon.myLen(dtContent.Rows(0)("Email_Text")) > 0 AndAlso (arrMailID IsNot Nothing AndAlso arrMailID.Count > 0) Then
                Dim objEmailH As New clsEMailHead()
                objEmailH.arrEMail = New List(Of String)()
                objEmailH.arrEMail = arrMailID

                objEmailH.Email_Subject = clsCommon.myCstr(dtContent.Rows(0)("Email_subject"))
                objEmailH.Email_Text = clsCommon.myCstr(dtContent.Rows(0)("Email_Text"))

                objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.DOC_NO, obj.Weighment_Code)
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.DOC_Date, clsCommon.GetPrintDate(obj.Weighment_Date, "dd/MMM/yyyy"))
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.ItemDetail, ItemDetail)

                If (objGRN IsNot Nothing AndAlso clsCommon.myLen(objGRN.GRN_No) > 0) Then
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.GRN_NO, objGRN.GRN_No)
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.GRN_Date, objGRN.GRN_Date)
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.vehicleNo, objGRN.VehicleNo)
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Vendor_Name, objGRN.Vendor_Name)
                End If

                objEmailH.SaveData(clsUserMgtCode.POWeighment, objEmailH, trans)
                objEmailH = Nothing

            End If

            If clsCommon.myLen(dtContent.Rows(0)("SMS_Text")) > 0 AndAlso (arrMobileNo IsNot Nothing AndAlso arrMobileNo.Count > 0) Then
                Dim objSMSH As New clsSMSHead()
                objSMSH.arrMobilNo = New List(Of String)()
                objSMSH.arrMobilNo = arrMobileNo

                objSMSH.SMS_Text = clsCommon.myCstr(dtContent.Rows(0)("SMS_Text"))

                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(clsEmailSMSConstants.DOC_NO, obj.Weighment_Code)
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(clsEmailSMSConstants.DOC_Date, clsCommon.GetPrintDate(obj.Weighment_Date, "dd/MMM/yyyy"))
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.ItemDetail, ItemDetail)

                If (objGRN IsNot Nothing AndAlso clsCommon.myLen(objGRN.GRN_No) > 0) Then
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.GRN_NO, objGRN.GRN_No)
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.GRN_Date, objGRN.GRN_Date)
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.vehicleNo, objGRN.VehicleNo)
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Vendor_Name, objGRN.Vendor_Name)
                End If

                objSMSH.SaveData(clsUserMgtCode.POWeighment, objSMSH, trans)
                objSMSH = Nothing
            End If
        End If

        ''Notification 
        If dtContent IsNot Nothing AndAlso dtContent.Rows.Count > 0 AndAlso clsCommon.myLen(dtContent.Rows(0)("Notification_Text")) > 0 Then

            Dim strNotifiCaption As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_Caption from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.POWeighment + "2" + "'", trans))
            Dim strNotificationOn As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_On from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.POWeighment + "2" + "'", trans))

            Dim objNotification As New clsNotificationHead()
            objNotification.Notification_Text = clsCommon.myCstr(dtContent.Rows(0)("Notification_Text"))
            objNotification.Notification_Caption = strNotifiCaption
            objNotification.Notification_On = strNotificationOn
            objNotification.Notification_Text = objNotification.Notification_Text.Replace(clsEmailSMSConstants.DOC_NO, obj.Weighment_Code)
            objNotification.Notification_Text = objNotification.Notification_Text.Replace(clsEmailSMSConstants.DOC_Date, clsCommon.GetPrintDate(obj.Weighment_Date, "dd/MMM/yyyy"))
            objNotification.Notification_Text = objNotification.Notification_Text.Replace(frmEMailAndSMSSetting.ItemDetail, ItemDetail)

            If (objGRN IsNot Nothing AndAlso clsCommon.myLen(objGRN.GRN_No) > 0) Then
                objNotification.Notification_Text = objNotification.Notification_Text.Replace(frmEMailAndSMSSetting.GRN_NO, objGRN.GRN_No)
                objNotification.Notification_Text = objNotification.Notification_Text.Replace(frmEMailAndSMSSetting.GRN_Date, objGRN.GRN_Date)
                objNotification.Notification_Text = objNotification.Notification_Text.Replace(frmEMailAndSMSSetting.vehicleNo, objGRN.VehicleNo)
                objNotification.Notification_Text = objNotification.Notification_Text.Replace(frmEMailAndSMSSetting.Vendor_Name, objGRN.Vendor_Name)
            End If

            objNotification.SaveData(clsUserMgtCode.POWeighment + "2", objNotification, trans)
            objNotification = Nothing

        End If
        ''Notification

    End Sub


    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Entry No not found to Delete")
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As clsPOWeighment = clsPOWeighment.GetData(strCode, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Weighment_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Posted on :" + clsCommon.GetPrintDate(obj.Posted_Date, "dd/MM/yyyy"))
            End If

            'Dim qry As String = "select 1 from TSPL_PO_WEIGHTMENT_DETAIL where Weighment_Code='" + strCode + "' and isnull(Unload_SNo,0)>0 "
            'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            '    Throw New Exception("Can't delete due to Unload the item")
            'End If
            Dim qry As String = ""
            Dim dt As DataTable = Nothing
            strCode = obj.Against_GRN_No
            qry = "select distinct MRN_No from TSPL_MRN_DETAIL where GRN_Id ='" + strCode + "'"
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsCommon.MyMessageBoxShow("MRN is created")
                Return True
                Exit Function
                'Else
                '    clsCommon.MyMessageBoxShow("MRN not created")

                'qry = "GRN is used in following MRN"
                'For Each dr As DataRow In dt.Rows
                '    qry += Environment.NewLine + clsCommon.myCstr(dr("MRN_No"))
                'Next
                'qry += Environment.NewLine + "Can't unpost it"
                'Throw New Exception(qry)
            End If
            'Exit Sub

            strCode = obj.Weighment_Code
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_PO_WEIGHTMENT_HEAD", "Weighment_Code", "TSPL_PO_WEIGHTMENT_DETAIL", "Weighment_Code", trans)
            qry = "delete from  TSPL_PO_WEIGHTMENT_GUNNY where Weighment_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PO_WEIGHTMENT_DETAIL where Weighment_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PO_WEIGHTMENT_HEAD where Weighment_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)



            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsPOWeighmentDetail
#Region "Variables"
    Public TR_No As String = Nothing
    Public Weighment_Code As String = Nothing
    Public SNo As Integer
    Public Item_Code As String = Nothing
    Public Item_Name As String = Nothing
    Public UOM As String = Nothing
    Public Gross_Weight As Decimal
    Public Tare_Weight As Decimal
    Public Net_Weight As Decimal
    Public Extra_Weight As Decimal
    Public Unload_SNo As Integer
    Public Is_Unload_Item As Integer
    Public Unload_By As String = Nothing
    Public Unload_Date As DateTime?
    Public Weight_By As String = Nothing
    Public Weight_Date As DateTime?
    Public GRN_Qty As Decimal
    Public Is_Auto_Weighment As Boolean
#End Region

    Public Function SaveData(ByVal strUnLoadCode As String, ByVal arr As List(Of clsPOWeighmentDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each obj As clsPOWeighmentDetail In arr
                    Dim coll As New Hashtable()
                    obj.TR_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select max(TR_No) from TSPL_PO_WEIGHTMENT_DETAIL", trans))
                    If clsCommon.myLen(obj.TR_No) <= 0 Then
                        obj.TR_No = "TR0000000000000000001"
                    Else
                        obj.TR_No = clsCommon.incval(obj.TR_No)
                    End If
                    clsCommon.AddColumnsForChange(coll, "TR_No", obj.TR_No)
                    clsCommon.AddColumnsForChange(coll, "Weighment_Code", strUnLoadCode)
                    clsCommon.AddColumnsForChange(coll, "SNo", obj.SNo)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM)
                    clsCommon.AddColumnsForChange(coll, "Gross_Weight", Math.Round(obj.Gross_Weight / 100), 2)  'obj.Gross_Weight)
                    clsCommon.AddColumnsForChange(coll, "Tare_Weight", obj.Tare_Weight)
                    clsCommon.AddColumnsForChange(coll, "Extra_Weight", obj.Extra_Weight)
                    clsCommon.AddColumnsForChange(coll, "Net_Weight", obj.Net_Weight)
                    clsCommon.AddColumnsForChange(coll, "Unload_SNo", obj.Unload_SNo)
                    clsCommon.AddColumnsForChange(coll, "GRN_Qty", obj.GRN_Qty)
                    clsCommon.AddColumnsForChange(coll, "Is_Unload_Item", IIf(obj.Is_Unload_Item, 1, 0))
                    clsCommon.AddColumnsForChange(coll, "Unload_By", obj.Unload_By)
                    If obj.Unload_Date IsNot Nothing Then
                        clsCommon.AddColumnsForChange(coll, "Unload_Date", clsCommon.GetPrintDate(obj.Unload_Date, "dd/MMM/yyyy hh:mm tt"))
                    Else
                        clsCommon.AddColumnsForChange(coll, "Unload_Date", Nothing, True)
                    End If
                    clsCommon.AddColumnsForChange(coll, "Weight_By", obj.Weight_By)
                    If obj.Weight_Date IsNot Nothing Then
                        clsCommon.AddColumnsForChange(coll, "Weight_Date", clsCommon.GetPrintDate(obj.Weight_Date, "dd/MMM/yyyy hh:mm tt"))
                    Else
                        clsCommon.AddColumnsForChange(coll, "Weight_Date", Nothing, True)
                    End If
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PO_WEIGHTMENT_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetTRData(ByVal strDocNo As String, ByVal strTRNo As String, ByVal trans As SqlTransaction) As clsPOWeighmentDetail
        Dim arr As List(Of clsPOWeighmentDetail) = GetData(strDocNo, strTRNo, trans)
        If arr IsNot Nothing AndAlso arr.Count > 0 Then
            Return arr(0)
        End If
        Return Nothing
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As List(Of clsPOWeighmentDetail)
        Return GetData(strDocNo, "", trans)
    End Function
    Public Shared Function GetData(ByVal strDocNo As String, ByVal strTRNo As String, ByVal trans As SqlTransaction) As List(Of clsPOWeighmentDetail)
        Dim arr As List(Of clsPOWeighmentDetail) = Nothing
        Dim qry As String = "select TSPL_PO_WEIGHTMENT_DETAIL.*,TSPL_ITEM_MASTER.Item_Desc from TSPL_PO_WEIGHTMENT_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_PO_WEIGHTMENT_DETAIL.Item_Code  where  Weighment_Code='" + strDocNo + "' "
        If clsCommon.myLen(strTRNo) > 0 Then
            qry += " and TR_No='" + strTRNo + "'"
        End If
        qry += " order by TSPL_PO_WEIGHTMENT_DETAIL.SNo"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            arr = New List(Of clsPOWeighmentDetail)
            For Each dr As DataRow In dt.Rows
                Dim obj As New clsPOWeighmentDetail()
                obj.TR_No = clsCommon.myCstr(dr("TR_No"))
                obj.Weighment_Code = clsCommon.myCstr(dr("Weighment_Code"))
                obj.SNo = clsCommon.myCdbl(dr("SNo"))
                obj.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                obj.Item_Name = clsCommon.myCstr(dr("Item_Desc"))
                obj.UOM = clsCommon.myCstr(dr("UOM"))
                obj.Gross_Weight = clsCommon.myCdbl(dr("Gross_Weight"))
                obj.Tare_Weight = clsCommon.myCdbl(dr("Tare_Weight"))
                obj.Extra_Weight = clsCommon.myCdbl(dr("Extra_Weight"))
                obj.Net_Weight = clsCommon.myCdbl(dr("Net_Weight"))
                obj.Unload_SNo = clsCommon.myCdbl(dr("Unload_SNo"))
                obj.Is_Unload_Item = IIf(clsCommon.myCdbl(dr("Is_Unload_Item")) = 1, True, False)
                obj.Unload_By = clsCommon.myCstr(dr("Unload_By"))
                If dr("Unload_Date") IsNot DBNull.Value Then
                    obj.Unload_Date = clsCommon.myCDate(dr("Unload_Date"))
                End If
                obj.Weight_By = clsCommon.myCstr(dr("Weight_By"))
                If dr("Weight_Date") IsNot DBNull.Value Then
                    obj.Weight_Date = clsCommon.myCDate(dr("Weight_Date"))
                End If
                obj.GRN_Qty = clsCommon.myCdbl(dr("GRN_Qty"))
                obj.Is_Auto_Weighment = IIf(clsCommon.myCdbl(dr("Is_Auto_Weighment")) = 1, True, False)
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function

    Public Shared Function SaveTareWeightment(ByVal strCode As String, ByVal strTRCode As String, ByVal obj As clsPOWeighmentDetail, ByVal arrTRNoForSameItem As List(Of String)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim Qry_Is_Unload_Item As Integer = clsDBFuncationality.getSingleValue("select Is_Unload_Item from TSPL_PO_WEIGHTMENT_DETAIL where  Weighment_Code='" + strCode + "'", trans)
            Dim Is_Unload_Item As Integer = 0
            If Qry_Is_Unload_Item > 0 Then
                Is_Unload_Item = 1
                'Is_Unload_Item = False
            End If

            If arrTRNoForSameItem IsNot Nothing AndAlso arrTRNoForSameItem.Count > 0 Then
                arrTRNoForSameItem.Insert(0, strTRCode)
                Dim objSaved As clsPOWeighmentDetail = clsPOWeighmentDetail.GetTRData(strCode, strTRCode, trans)
                If objSaved Is Nothing Then
                    Throw New Exception("Detail data not found with TR No" + strTRCode)
                End If



                Dim dblGrossWeight As Decimal = obj.Gross_Weight
                Dim dblTareWeight As Decimal = obj.Tare_Weight

                For ii As Integer = 0 To arrTRNoForSameItem.Count - 1
                    If dblGrossWeight = 0 Then
                        Exit For
                    End If
                    Dim dclGRNQty As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select GRN_Qty from TSPL_PO_WEIGHTMENT_DETAIL where TR_No='" + arrTRNoForSameItem(ii) + "'", trans))
                    Dim coll As New Hashtable()
                    If ii <> 0 Then
                        clsCommon.AddColumnsForChange(coll, "Gross_Weight", dblGrossWeight)
                        objSaved.Unload_SNo += 1
                        clsCommon.AddColumnsForChange(coll, "Unload_SNo", objSaved.Unload_SNo)
                        clsCommon.AddColumnsForChange(coll, "Unload_By", objSaved.Unload_By)
                        If objSaved.Unload_Date IsNot Nothing Then
                            clsCommon.AddColumnsForChange(coll, "Unload_Date", clsCommon.GetPrintDate(objSaved.Unload_Date, "dd/MMM/yyyy hh:mm tt"))
                        End If
                    End If
                    If ii = arrTRNoForSameItem.Count - 1 Then
                        clsCommon.AddColumnsForChange(coll, "Tare_Weight", dblTareWeight)
                        clsCommon.AddColumnsForChange(coll, "Net_Weight", dblGrossWeight - dblTareWeight)
                    Else
                        If dclGRNQty >= (dblGrossWeight - dblTareWeight) Then
                            clsCommon.AddColumnsForChange(coll, "Tare_Weight", dblTareWeight)
                            clsCommon.AddColumnsForChange(coll, "Net_Weight", dblGrossWeight - dblTareWeight)
                            dblGrossWeight = 0
                        Else
                            dblGrossWeight -= dclGRNQty
                            clsCommon.AddColumnsForChange(coll, "Tare_Weight", dblGrossWeight)
                            clsCommon.AddColumnsForChange(coll, "Net_Weight", dclGRNQty)
                        End If
                    End If
                    clsCommon.AddColumnsForChange(coll, "Extra_Weight", obj.Extra_Weight)

                    clsCommon.AddColumnsForChange(coll, "Is_Unload_Item", Is_Unload_Item)
                    clsCommon.AddColumnsForChange(coll, "Weight_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Is_Auto_Weighment", IIf(obj.Is_Auto_Weighment, 1, 0))
                    clsCommon.AddColumnsForChange(coll, "Weight_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

                    'clsCommon.AddColumnsForChange(coll, "Weighment_Code", obj.Weighment_Code)
                    'clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                    'clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM)
                    'clsCommon.AddColumnsForChange(coll, "Qty", obj.GRN_Qty)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PO_WEIGHTMENT_DETAIL", OMInsertOrUpdate.Update, "Weighment_Code='" + strCode + "' and TR_No='" + arrTRNoForSameItem(ii) + "'", trans)
                Next
            Else
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Tare_Weight", obj.Tare_Weight)
                clsCommon.AddColumnsForChange(coll, "Extra_Weight", obj.Extra_Weight)
                clsCommon.AddColumnsForChange(coll, "Net_Weight", obj.Net_Weight)
                clsCommon.AddColumnsForChange(coll, "Is_Unload_Item", Is_Unload_Item)
                clsCommon.AddColumnsForChange(coll, "Weight_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Is_Auto_Weighment", IIf(obj.Is_Auto_Weighment, 1, 0))
                clsCommon.AddColumnsForChange(coll, "Weight_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

                'clsCommon.AddColumnsForChange(coll, "Weighment_Code", obj.Weighment_Code)
                'clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                'clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM)
                'clsCommon.AddColumnsForChange(coll, "Qty", obj.GRN_Qty)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PO_WEIGHTMENT_DETAIL", OMInsertOrUpdate.Update, "Weighment_Code='" + strCode + "' and TR_No='" + strTRCode + "'", trans)

            End If



            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveUnloadItem(ByVal strCode As String, ByVal strTRCode As String, ByVal obj As clsPOWeighmentDetail) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            ''UDL/22/08/18-000216 by balwinder on 24/08/2018
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Is_Unload_Item", obj.Is_Unload_Item)
            clsCommon.AddColumnsForChange(coll, "Gross_Weight", 0)
            clsCommon.AddColumnsForChange(coll, "Unload_SNo", obj.Unload_SNo)
            clsCommon.AddColumnsForChange(coll, "Unload_By", obj.Unload_By, True)
            clsCommon.AddColumnsForChange(coll, "Unload_Date", obj.Unload_Date, True)
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PO_WEIGHTMENT_DETAIL", OMInsertOrUpdate.Update, "Weighment_Code='" + strCode + "' and Is_Unload_Item=1", trans)


            Dim qry As String = "select top 1 Unload_SNo,Tare_Weight,Is_Auto_Weighment,UOM from TSPL_PO_WEIGHTMENT_DETAIL  where TSPL_PO_WEIGHTMENT_DETAIL.Weighment_Code='" + strCode + "' and isnull(Unload_SNo,0)>0 order by Unload_SNo desc "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                obj.Unload_SNo = 1
                qry = "select Gross_Weight,Is_Auto_Weighment,'KG' as UOM from TSPL_PO_WEIGHTMENT_HEAD where Weighment_Code='" + strCode + "'"
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                obj.Gross_Weight = clsCommon.myCdbl(dt.Rows(0)("Gross_Weight"))
            Else
                obj.Unload_SNo = clsCommon.myCdbl(dt.Rows(0)("Unload_SNo")) + 1
                obj.Gross_Weight = clsCommon.myCdbl(dt.Rows(0)("Tare_Weight"))
            End If

            If clsCommon.myCdbl(dt.Rows(0)("Is_Auto_Weighment")) > 0 Then
                Dim convFat As Double = clsWeightConversionInfo.GetWeightConverionFactor(obj.Item_Code, clsCommon.myCstr(dt.Rows(0)("UOM")), "KG", trans)
                obj.Gross_Weight = convFat * obj.Gross_Weight
                convFat = clsWeightConversionInfo.GetWeightConverionFactor(obj.Item_Code, "KG", obj.UOM, trans)
                obj.Gross_Weight = convFat * obj.Gross_Weight
            End If




            coll = New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Gross_Weight", obj.Gross_Weight)
            clsCommon.AddColumnsForChange(coll, "Unload_SNo", obj.Unload_SNo)
            clsCommon.AddColumnsForChange(coll, "Is_Unload_Item", 1)
            clsCommon.AddColumnsForChange(coll, "Unload_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Unload_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PO_WEIGHTMENT_DETAIL", OMInsertOrUpdate.Update, "Weighment_Code='" + strCode + "' and TR_No='" + strTRCode + "'", trans)

            If objCommonVar.InternalSMSEmailinPurchaseModule = True Then
                CreateInternalEmailSMS_Unloading(strCode, trans)
            End If

            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Private Shared Sub CreateInternalEmailSMS_Unloading(ByVal strWeighmentCode As String, ByVal trans As SqlTransaction)
        Dim itemName As String = ""
        Dim UOM As String = ""
        Dim qty As String = ""
        Dim ItemDetail As String = ""
        Dim objGRN As clsGRNHead = Nothing

        Dim obj As clsPOWeighment = clsPOWeighment.GetData(strWeighmentCode, NavigatorType.Current, trans)

        Dim dtContent As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject,Notification_Text from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.POUnloading + "2" + "'", trans)

        Dim qry As String = "select TSPL_USER_MASTER.User_Code from TSPL_USER_MASTER "
        If clsCommon.myLen(clsDBFuncationality.getSingleValue("select isnull(Against_Requisition,'') from TSPL_GRN_HEAD where TSPL_GRN_HEAD.GRN_No='" + obj.Against_GRN_No + "'", trans)) > 0 Then
            qry += " left join TSPL_REQUISITION_HEAD on TSPL_REQUISITION_HEAD.Created_By=TSPL_USER_MASTER.User_Code left join TSPL_PURCHASE_ORDER_HEAD ON TSPL_PURCHASE_ORDER_HEAD.Against_Requisition=TSPL_REQUISITION_HEAD.Requisition_Id "
        Else
            qry += " left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.Created_By=TSPL_USER_MASTER.User_Code "
        End If
        qry += " left join TSPL_GRN_HEAD on TSPL_GRN_HEAD.Against_PO=TSPL_PURCHASE_ORDER_HEAD.PURCHASEORDER_no "
        qry += " where TSPL_GRN_HEAD.GRN_No='" + obj.Against_GRN_No + "'"
        Dim StrUserCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        Dim arrMobileNo As New List(Of String)
        Dim arrMailID As List(Of String) = clsERPFuncationality.ReportingMailIdandPhone(StrUserCode, arrMobileNo, trans)

        If dtContent IsNot Nothing AndAlso dtContent.Rows.Count > 0 Then
            If (obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0) Then
                For Each objdetail As clsPOWeighmentDetail In obj.Arr
                    itemName = clsItemMaster.GetItemName(clsCommon.myCstr(objdetail.Item_Code), trans)
                    UOM = clsCommon.myCstr(objdetail.UOM)
                    qty = clsCommon.myCstr(objdetail.Net_Weight)
                    ItemDetail += itemName + " " + UOM + "-" + qty + Environment.NewLine
                Next
            End If

            objGRN = clsGRNHead.GetData(obj.Against_GRN_No, NavigatorType.Current, trans)
        End If


        If dtContent IsNot Nothing AndAlso dtContent.Rows.Count > 0 AndAlso ((arrMailID IsNot Nothing AndAlso arrMailID.Count > 0) Or (arrMobileNo IsNot Nothing AndAlso arrMobileNo.Count > 0)) Then

            If clsCommon.myLen(dtContent.Rows(0)("Email_Text")) > 0 AndAlso (arrMailID IsNot Nothing AndAlso arrMailID.Count > 0) Then
                Dim objEmailH As New clsEMailHead()
                objEmailH.arrEMail = New List(Of String)()
                objEmailH.arrEMail = arrMailID

                objEmailH.Email_Subject = clsCommon.myCstr(dtContent.Rows(0)("Email_subject"))
                objEmailH.Email_Text = clsCommon.myCstr(dtContent.Rows(0)("Email_Text"))

                objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.DOC_NO, obj.Weighment_Code)
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.DOC_Date, clsCommon.GetPrintDate(obj.Weighment_Date, "dd/MMM/yyyy"))
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.ItemDetail, ItemDetail)

                If (objGRN IsNot Nothing AndAlso clsCommon.myLen(objGRN.GRN_No) > 0) Then
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.GRN_NO, objGRN.GRN_No)
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.GRN_Date, objGRN.GRN_Date)
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.vehicleNo, objGRN.VehicleNo)
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Vendor_Name, objGRN.Vendor_Name)
                End If

                objEmailH.SaveData(clsUserMgtCode.POUnloading + "2", objEmailH, trans)
                objEmailH = Nothing

            End If

            If clsCommon.myLen(dtContent.Rows(0)("SMS_Text")) > 0 AndAlso (arrMobileNo IsNot Nothing AndAlso arrMobileNo.Count > 0) Then
                Dim objSMSH As New clsSMSHead()
                objSMSH.arrMobilNo = New List(Of String)()
                objSMSH.arrMobilNo = arrMobileNo

                objSMSH.SMS_Text = clsCommon.myCstr(dtContent.Rows(0)("SMS_Text"))

                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(clsEmailSMSConstants.DOC_NO, obj.Weighment_Code)
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(clsEmailSMSConstants.DOC_Date, clsCommon.GetPrintDate(obj.Weighment_Date, "dd/MMM/yyyy"))
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.ItemDetail, ItemDetail)

                If (objGRN IsNot Nothing AndAlso clsCommon.myLen(objGRN.GRN_No) > 0) Then
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.GRN_NO, objGRN.GRN_No)
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.GRN_Date, objGRN.GRN_Date)
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.vehicleNo, objGRN.VehicleNo)
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Vendor_Name, objGRN.Vendor_Name)
                End If

                objSMSH.SaveData(clsUserMgtCode.POUnloading + "2", objSMSH, trans)
                objSMSH = Nothing
            End If
        End If

        ''Notification 
        If dtContent IsNot Nothing AndAlso dtContent.Rows.Count > 0 AndAlso clsCommon.myLen(dtContent.Rows(0)("Notification_Text")) > 0 Then

            Dim strNotifiCaption As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_Caption from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.POUnloading + "2" + "'", trans))
            Dim strNotificationOn As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_On from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.POUnloading + "2" + "'", trans))

            Dim objNotification As New clsNotificationHead()
            objNotification.Notification_Text = clsCommon.myCstr(dtContent.Rows(0)("Notification_Text"))
            objNotification.Notification_Caption = strNotifiCaption
            objNotification.Notification_On = strNotificationOn
            objNotification.Notification_Text = objNotification.Notification_Text.Replace(clsEmailSMSConstants.DOC_NO, obj.Weighment_Code)
            objNotification.Notification_Text = objNotification.Notification_Text.Replace(clsEmailSMSConstants.DOC_Date, clsCommon.GetPrintDate(obj.Weighment_Date, "dd/MMM/yyyy"))
            objNotification.Notification_Text = objNotification.Notification_Text.Replace(frmEMailAndSMSSetting.ItemDetail, ItemDetail)

            If (objGRN IsNot Nothing AndAlso clsCommon.myLen(objGRN.GRN_No) > 0) Then
                objNotification.Notification_Text = objNotification.Notification_Text.Replace(frmEMailAndSMSSetting.GRN_NO, objGRN.GRN_No)
                objNotification.Notification_Text = objNotification.Notification_Text.Replace(frmEMailAndSMSSetting.GRN_Date, objGRN.GRN_Date)
                objNotification.Notification_Text = objNotification.Notification_Text.Replace(frmEMailAndSMSSetting.vehicleNo, objGRN.VehicleNo)
                objNotification.Notification_Text = objNotification.Notification_Text.Replace(frmEMailAndSMSSetting.Vendor_Name, objGRN.Vendor_Name)
            End If

            objNotification.SaveData(clsUserMgtCode.POUnloading + "2", objNotification, trans)
            objNotification = Nothing

        End If
        ''Notification

    End Sub

End Class

Public Class clsPOWeighmentGunnyBag
#Region "Variables"
    'Public SNo As Integer
    Public Weighment_Code As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Name As String = Nothing
    Public UOM As String = Nothing
    Public GRN_Qty As String = Nothing
#End Region


    Public Shared Function GetData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As List(Of clsPOWeighmentGunnyBag)
        Dim ArrGunnyBag As List(Of clsPOWeighmentGunnyBag) = Nothing
        Dim qry As String = "select TSPL_PO_WEIGHTMENT_GUNNY.*,TSPL_ITEM_MASTER.Item_Desc from TSPL_PO_WEIGHTMENT_GUNNY left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_PO_WEIGHTMENT_GUNNY.Item_Code  where  Weighment_Code='" + strDocNo + "'"
        qry += " order by TSPL_PO_WEIGHTMENT_GUNNY.PK_Id"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            ArrGunnyBag = New List(Of clsPOWeighmentGunnyBag)
            For Each dr As DataRow In dt.Rows
                Dim obj As New clsPOWeighmentGunnyBag()

                'obj.SNo = clsCommon.myCdbl(dr("SNo"))
                obj.Weighment_Code = clsCommon.myCstr(dr("Weighment_Code"))
                obj.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                obj.Item_Name = clsCommon.myCstr(dr("Item_Desc"))
                obj.UOM = clsCommon.myCstr(dr("UOM"))
                obj.GRN_Qty = clsCommon.myCdbl(dr("Qty"))

                ArrGunnyBag.Add(obj)
            Next
        End If
        Return ArrGunnyBag
    End Function


    Public Shared Function SaveData(ByVal strWeighmentCode As String, ByVal arr As List(Of clsPOWeighmentGunnyBag), ByVal trans As SqlTransaction) As Boolean
        Try
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each obj As clsPOWeighmentGunnyBag In arr
                    Dim coll As New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "Weighment_Code", strWeighmentCode)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM)
                    clsCommon.AddColumnsForChange(coll, "Qty", obj.GRN_Qty)

                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PO_WEIGHTMENT_GUNNY", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

End Class