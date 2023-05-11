Imports common
Imports System.Data.SqlClient

Public Class clsGatePassEntry
#Region "Variables"
    Public GPCode As String = Nothing
    Public GPDate As DateTime
    Public Vehicle_Id As String = Nothing
    Public Vehicle_Number As String = Nothing
    Public DocNo As String = Nothing
    Public Docdate As Date = Nothing
    Public ToSalesmanCode As String = Nothing
    Public ToSalesmanname As String = Nothing
    Public Route_No As String = Nothing
    Public Route_Desc As String = Nothing
    Public Item_Type As String = Nothing
    Public Transporter As String = Nothing
    Public Remarks As String = Nothing
    Public Comments As String = Nothing
    Public Post As String = Nothing
    Public Location_Code As String = Nothing
    Public Location_Desc As String = Nothing

    Public Arr As List(Of clsGPDetail) = Nothing


#End Region

    Public Function SaveData(ByVal obj As clsGatePassEntry, ByVal isNewEntry As Boolean, ByVal strTransType As String) As Boolean

        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Fresh Sale", "Fresh Gatepass Entry", obj.Location_Code, obj.GPDate, trans)
        Try
            Dim qry As String = "delete from TSPL_GATEPASS_DETAIL_FRESHSALE where GPcode='" + obj.GPCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim strDocNo As String = ""
            If isNewEntry Then
                If clsCommon.CompairString(strTransType, "PS") = CompairStringResult.Equal Then
                    obj.GPCode = clsERPFuncationality.GetNextCode(trans, obj.GPDate, clsDocType.FrmGatePassPS, "", "")
                Else
                    obj.GPCode = clsERPFuncationality.GetNextCode(trans, obj.GPDate, clsDocType.FrmGatePassFS, "", "")
                End If

            End If
            If (clsCommon.myLen(obj.GPCode) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "GPDate", clsCommon.GetPrintDate(obj.GPDate, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Vehicle_Id", obj.Vehicle_Id)
            clsCommon.AddColumnsForChange(coll, "Vehicle_Number", obj.Vehicle_Number)
            clsCommon.AddColumnsForChange(coll, "Item_Type", obj.Item_Type)
            clsCommon.AddColumnsForChange(coll, "Transporter", obj.Transporter)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Comments", obj.Comments)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Location_Desc", obj.Location_Desc)
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "GPCode", obj.GPCode)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GATEPASS_MASTER_FRESHSALE", OMInsertOrUpdate.Insert, "", trans)

            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GATEPASS_MASTER_FRESHSALE", OMInsertOrUpdate.Update, "TSPL_GATEPASS_MASTER_FRESHSALE.GPCode='" + obj.GPCode + "'", trans)
            End If

            isSaved = isSaved AndAlso clsGPDetail.SaveData(obj.GPCode, obj.Arr, trans)

            qry = "Update TSPL_SD_SHIPMENT_HEAD set GPCode='" & obj.GPCode & "' where  convert(date,Document_Date,103)='" & clsCommon.GetPrintDate(obj.GPDate, "") & "' and isnull(GPCode,'') = '' and Trans_Type='FS' and Bill_To_Location='" & obj.Location_Code & "' and Vehicle_Code='" & obj.Vehicle_Id & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strRetCode As String, ByVal NavType As NavigatorType, ByVal TransType As String) As clsGatePassEntry
        Return GetData(strRetCode, NavType, TransType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal TransType As String, ByVal trans As SqlTransaction) As clsGatePassEntry
        Dim obj As clsGatePassEntry = Nothing
        Dim qry As String = "select GPCode,GPDate,Vehicle_Id,Vehicle_Number,Item_Type,Transporter,Remarks,Comments,Post,Location_Code,Location_Desc from TSPL_GATEPASS_MASTER_FRESHSALE where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_GATEPASS_MASTER_FRESHSALE.GPCode = (select MIN(GPCode) from TSPL_GATEPASS_MASTER_FRESHSALE)"
            Case NavigatorType.Last
                qry += " and TSPL_GATEPASS_MASTER_FRESHSALE.GPCode = (select Max(GPCode) from TSPL_GATEPASS_MASTER_FRESHSALE)"
            Case NavigatorType.Next
                qry += " and TSPL_GATEPASS_MASTER_FRESHSALE.GPCode = (select Min(GPCode) from TSPL_GATEPASS_MASTER_FRESHSALE where GPCode>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_GATEPASS_MASTER_FRESHSALE.GPCode = (select Max(GPCode) from TSPL_GATEPASS_MASTER_FRESHSALE where GPCode<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_GATEPASS_MASTER_FRESHSALE.GPCode = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsGatePassEntry()
            obj.GPCode = clsCommon.myCstr(dt.Rows(0)("GPCode"))
            obj.GPDate = clsCommon.myCDate(dt.Rows(0)("GPDate"))
            obj.Vehicle_Id = clsCommon.myCstr(dt.Rows(0)("Vehicle_Id"))
            obj.Vehicle_Number = clsCommon.myCstr(dt.Rows(0)("Vehicle_Number"))
            obj.Item_Type = clsCommon.myCstr(dt.Rows(0)("Item_Type"))
            obj.Transporter = clsCommon.myCstr(dt.Rows(0)("Transporter"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Comments = clsCommon.myCstr(dt.Rows(0)("Comments"))
            obj.Post = clsCommon.myCstr(dt.Rows(0)("Post"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Location_Desc = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
            'obj.Arr = clsGPDetail.GetData(obj.GPCode, trans, obj.GPDate, TransType)

        End If
        Return obj
    End Function


End Class
Public Class clsGPDetail
#Region "Variables"
    Public GPCode As String = Nothing
    Public DocNo As String = Nothing
    Public Docdate As Date = Nothing
    Public ToSalesmanCode As String = Nothing
    Public ToSalesmanname As String = Nothing
    Public Route_No As String = Nothing
    Public Route_Desc As String = Nothing
    Public Status As String = Nothing
    Public Type As String = Nothing
    Public Vehicle_Id As String = Nothing
    Public Vehicle_Number As String = Nothing
    Public Price_Code As String = Nothing
    Public Price_Desc As String = Nothing
    Public Cust_Code As String = Nothing
    Public Customer_Name As String = Nothing

    Public Item_Code As String = Nothing
    Public Unit_Code As String = Nothing
    Public Qty As Double = 0
    Public HSN_Code As String = Nothing

#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsGPDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsGPDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "GPCode", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Unit_Code", obj.Unit_Code)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                clsCommon.AddColumnsForChange(coll, "HSN_Code", obj.HSN_Code)

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GATEPASS_DETAIL_FRESHSALE", OMInsertOrUpdate.Insert, "", trans)

            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction, ByVal strdate As Date, ByVal strType As String) As List(Of clsGPDetail)
        Dim arr As List(Of clsGPDetail) = Nothing
        Dim qry As String
        If clsCommon.CompairString(strType, "PS") = CompairStringResult.Equal Then
            qry = "select GPCode,DocNo,convert(date,Docdate,103) as Docdate,ToSalesmanCode,ToSalesmanname, " & _
            "Route_No,Route_Desc,1 as Status,Type,Price_Code,Price_Desc,Cust_Code,Customer_Name from " & _
            "TSPL_GATEPASS_DETAIL_FRESHSALE where TSPL_GATEPASS_DETAIL_FRESHSALE.GPCode='" + strCode + "' " & _
            " Union all " & _
            "select '' as GPCode,Document_Code as DOCNo,Document_Date as Docdate,'' as ToSalesmanCode ,'' as ToSalesmanname,'' as Route_No,'' as Route_Desc, " & _
                    "0  as Status," & strType & " as Type,'' as Price_Code,'' as Price_Desc,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name from TSPL_SD_SALE_INVOICE_HEAD  " & _
                    "left outer join TSPL_CUSTOMER_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code where Trans_Type='" & strType & "' and " & _
                    "convert(date,Document_Date,103)='" & clsCommon.GetPrintDate(strdate, "dd/MMM/yyyy") & "' AND  " & _
                    "Document_Code not in (select DOCno From TSPL_GATEPASS_DETAIL_FRESHSALE ) "
        Else
            qry = "select GPCode,DocNo,convert(date,Docdate,103) as Docdate,ToSalesmanCode,ToSalesmanname, " & _
                      "Route_No,Route_Desc,1 as Status,Type,Price_Code,Price_Desc,Cust_Code,Customer_Name from " & _
                      "TSPL_GATEPASS_DETAIL_FRESHSALE where TSPL_GATEPASS_DETAIL_FRESHSALE.GPCode='" + strCode + "' " & _
                      " Union all " & _
                      "select '' as GPCode,Document_Code as DOCNo,Document_Date as Docdate,'' as ToSalesmanCode ,'' as ToSalesmanname, TSPL_SD_SALE_INVOICE_HEAD.Route_No, TSPL_ROUTE_MASTER.Route_Desc, " & _
                      "0  as Status,'" & strType & "' as Type, TSPL_SD_SALE_INVOICE_HEAD.Price_Code,(select distinct Price_Code_Desc from  TSPL_PRICE_COMPONENT_MAPPING  where price_code=TSPL_SD_SALE_INVOICE_HEAD.price_code)  as Price_Desc,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name from TSPL_SD_SALE_INVOICE_HEAD " & _
                     "left outer join TSPL_CUSTOMER_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code  left outer join TSPL_ROUTE_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Route_No=TSPL_ROUTE_MASTER.Route_No where Trans_Type='" & strType & "' and " & _
                     "convert(date,Document_Date,103)='" & clsCommon.GetPrintDate(strdate, "dd/MMM/yyyy") & "' AND  " & _
                     "Document_Code not in (select DOCno From TSPL_GATEPASS_DETAIL_FRESHSALE ) "
        End If

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsGPDetail)()
            For Each dr As DataRow In dt.Rows
                Dim obj As clsGPDetail = New clsGPDetail()
                obj.GPCode = clsCommon.myCstr(dr("GPCode"))
                obj.DocNo = clsCommon.myCstr(dr("DocNo"))
                obj.Docdate = clsCommon.myCstr(dr("Docdate"))
                obj.ToSalesmanCode = clsCommon.myCstr(dr("ToSalesmanCode"))
                obj.ToSalesmanname = clsCommon.myCstr(dr("ToSalesmanname"))
                obj.Route_No = clsCommon.myCstr(dr("Route_No"))
                obj.Route_Desc = clsCommon.myCstr(dr("Route_Desc"))
                obj.Status = clsCommon.myCstr(dr("Status"))
                obj.Type = clsCommon.myCstr(dr("Type"))
                obj.Price_Code = clsCommon.myCstr(dr("Price_Code"))
                obj.Price_Desc = clsCommon.myCstr(dr("Price_Desc"))

                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class
