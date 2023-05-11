Imports common
Imports System.Data.SqlClient
Public Class clsMccScrapGatePass
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

    Public TotalCAN As Integer = 0
    Public TotalCrate As Integer = 0
    Public AgainstDocumentCode As String = Nothing
    Public Arr As List(Of clsMccScrapGatepassDetail) = Nothing
    Public InvoiceArr As List(Of clsMccScrapGatepassDetail) = Nothing

#End Region


    Public Function SaveData(ByVal obj As clsMccScrapGatePass, ByVal isNewEntry As Boolean, ByVal strTransType As String) As Boolean

        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Try
            Dim qry As String = "delete from TSPL_MCC_SCRAP_GATEPASS_DETAIL where GPcode='" + obj.GPCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim strDocNo As String = ""
            If isNewEntry Then
                obj.GPCode = clsERPFuncationality.GetNextCode(trans, obj.GPDate, clsDocType.FrmMCCScrapGatePass, clsDocTransactionType.frmMccScrapgatePass, obj.Location_Code)
            End If
            If (clsCommon.myLen(obj.GPCode) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "GPDate", clsCommon.GetPrintDate(obj.GPDate, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Vehicle_Id", obj.Vehicle_Id)
            clsCommon.AddColumnsForChange(coll, "Vehicle_Number", obj.Vehicle_Number)
            clsCommon.AddColumnsForChange(coll, "Doc_Type", obj.Item_Type)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Comments", obj.Comments)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Location_Desc", obj.Location_Desc)

            clsCommon.AddColumnsForChange(coll, "Route_No", obj.Route_No)
            clsCommon.AddColumnsForChange(coll, "TotalCAN", obj.TotalCAN)
            clsCommon.AddColumnsForChange(coll, "TotalCrate", obj.TotalCrate)
            '============================================================================
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "GPCode", obj.GPCode)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_SCRAP_GATEPASS_MASTER", OMInsertOrUpdate.Insert, "", trans)

            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_SCRAP_GATEPASS_MASTER", OMInsertOrUpdate.Update, "TSPL_MCC_SCRAP_GATEPASS_MASTER.GPCode='" + obj.GPCode + "'", trans)
            End If

            isSaved = isSaved AndAlso clsMccScrapGatepassDetail.SaveData(obj.GPCode, obj.Arr, trans)
            isSaved = isSaved AndAlso clsMccScrapGatepassDetail.InvoiceSave(obj.GPCode, obj.InvoiceArr, trans, obj.Item_Type)

            'If clsCommon.CompairString(obj.Item_Type, "Mcc") = CompairStringResult.Equal Then
            '    qry = "Update TSPL_SD_SHIPMENT_HEAD set GPCode='" & obj.GPCode & "' where  convert(date,Document_Date,103)='" & clsCommon.GetPrintDate(obj.GPDate, "") & "' and isnull(GPCode,'') = '' and Trans_Type='FS' and Bill_To_Location='" & obj.Location_Code & "' and Vehicle_Code='" & obj.Vehicle_Id & "' and TSPL_SD_SHIPMENT_HEAD.Document_Code  in (" + AgainstDocumentCode + ")"
            '    clsDBFuncationality.ExecuteNonQuery(qry, trans)
            'End If

            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strRetCode As String, ByVal NavType As NavigatorType, ByVal TransType As String) As clsMccScrapGatePass
        Return GetData(strRetCode, NavType, TransType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal TransType As String, ByVal trans As SqlTransaction) As clsMccScrapGatePass
        Dim obj As clsMccScrapGatePass = Nothing
        Dim qry As String = "select GPCode,GPDate,Vehicle_Id,Vehicle_Number,doc_Type,Remarks,Comments,Post,Location_Code,Location_Desc,TSPL_MCC_SCRAP_GATEPASS_MASTER.TotalCAN,TSPL_MCC_SCRAP_GATEPASS_MASTER.TotalCrate from TSPL_MCC_SCRAP_GATEPASS_MASTER  where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_MCC_SCRAP_GATEPASS_MASTER.GPCode = (select MIN(GPCode) from TSPL_MCC_SCRAP_GATEPASS_MASTER)"
            Case NavigatorType.Last
                qry += " and TSPL_MCC_SCRAP_GATEPASS_MASTER.GPCode = (select Max(GPCode) from TSPL_MCC_SCRAP_GATEPASS_MASTER)"
            Case NavigatorType.Next
                qry += " and TSPL_MCC_SCRAP_GATEPASS_MASTER.GPCode = (select Min(GPCode) from TSPL_MCC_SCRAP_GATEPASS_MASTER where GPCode>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_MCC_SCRAP_GATEPASS_MASTER.GPCode = (select Max(GPCode) from TSPL_MCC_SCRAP_GATEPASS_MASTER where GPCode<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_MCC_SCRAP_GATEPASS_MASTER.GPCode = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsMccScrapGatePass()
            obj.GPCode = clsCommon.myCstr(dt.Rows(0)("GPCode"))
            obj.GPDate = clsCommon.myCDate(dt.Rows(0)("GPDate"))
            obj.Vehicle_Id = clsCommon.myCstr(dt.Rows(0)("Vehicle_Id"))
            obj.Vehicle_Number = clsCommon.myCstr(dt.Rows(0)("Vehicle_Number"))
            obj.Item_Type = clsCommon.myCstr(dt.Rows(0)("Doc_Type"))

            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Comments = clsCommon.myCstr(dt.Rows(0)("Comments"))
            obj.Post = clsCommon.myCstr(dt.Rows(0)("Post"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Location_Desc = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))

            obj.TotalCrate = clsCommon.myCdbl(dt.Rows(0)("TotalCrate"))
            obj.TotalCAN = clsCommon.myCdbl(dt.Rows(0)("TotalCAN"))
            '===========================================================
            'obj.Arr = clsGPDetail.GetData(obj.GPCode, trans, obj.GPDate, TransType)

        End If
        Return obj
    End Function
End Class
Public Class clsMccScrapGatepassDetail
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
    Public InvoiceNo As String = Nothing
    Public Item_Code As String = Nothing
    Public Unit_Code As String = Nothing
    Public Qty As Double = 0
    Public HSN_Code As String = Nothing

#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsMccScrapGatepassDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsMccScrapGatepassDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "GPCode", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Unit_Code", obj.Unit_Code)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                clsCommon.AddColumnsForChange(coll, "HSN_Code", obj.HSN_Code)

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_SCRAP_GATEPASS_DETAIL", OMInsertOrUpdate.Insert, "", trans)

            Next
        End If
        Return True
    End Function
    Public Shared Function InvoiceSave(ByVal strDocNo As String, ByVal InvoiceArr As List(Of clsMccScrapGatepassDetail), ByVal trans As SqlTransaction, Optional ByVal TransType As String = Nothing) As Boolean
        If (InvoiceArr IsNot Nothing AndAlso InvoiceArr.Count > 0) Then
            For Each obj As clsMccScrapGatepassDetail In InvoiceArr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "GPCode", strDocNo)
                clsCommon.AddColumnsForChange(coll, "InvoiceNo", obj.InvoiceNo)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Mcc_Scrap_Invoice_GATEPASS_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Dim qry As String = ""
                If clsCommon.CompairString(TransType, "Mcc") = CompairStringResult.Equal Then
                    qry = "Update TSPL_SD_SHIPMENT_HEAD set GPCode='" & strDocNo & "' where 2=2 and isnull(GPCode,'') = '' and Trans_Type='Mcc' and Vehicle_Code='" & obj.Vehicle_Id & "' and TSPL_SD_SHIPMENT_HEAD.Document_Code  in ('" + obj.InvoiceNo + "')"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                Else
                    qry = "Update TSPL_SCRAPSALE_HEAD set GPCode='" & strDocNo & "' where 2=2 and TSPL_SCRAPSALE_HEAD.Shipment_no  in ('" + obj.InvoiceNo + "')"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                End If


            Next
        End If
        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction, ByVal strdate As Date, ByVal strType As String) As List(Of clsMccScrapGatepassDetail)
        Dim arr As List(Of clsMccScrapGatepassDetail) = Nothing
        Dim qry As String = ""
        If clsCommon.CompairString(strType, "PS") = CompairStringResult.Equal Then
            qry = "select GPCode,DocNo,convert(date,Docdate,103) as Docdate,ToSalesmanCode,ToSalesmanname, " & _
            "Route_No,Route_Desc,1 as Status,Type,Price_Code,Price_Desc,Cust_Code,Customer_Name from " & _
            "TSPL_MCC_SCRAP_GATEPASS_DETAIL where TSPL_MCC_SCRAP_GATEPASS_DETAIL.GPCode='" + strCode + "' " & _
            " Union all " & _
            "select '' as GPCode,Document_Code as DOCNo,Document_Date as Docdate,'' as ToSalesmanCode ,'' as ToSalesmanname,'' as Route_No,'' as Route_Desc, " & _
                    "0  as Status," & strType & " as Type,'' as Price_Code,'' as Price_Desc,TSPL_SD_SHIPMENT_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name from TSPL_SD_SHIPMENT_HEAD  " & _
                    "left outer join TSPL_CUSTOMER_MASTER on TSPL_SD_SHIPMENT_HEAD.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code where Trans_Type='" & strType & "' and " & _
                    "convert(date,Document_Date,103)='" & clsCommon.GetPrintDate(strdate, "dd/MMM/yyyy") & "' AND  " & _
                    "Document_Code not in (select DOCno From TSPL_MCC_SCRAP_GATEPASS_DETAIL ) "

        End If

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsMccScrapGatepassDetail)()
            For Each dr As DataRow In dt.Rows
                Dim obj As clsMccScrapGatepassDetail = New clsMccScrapGatepassDetail()
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

