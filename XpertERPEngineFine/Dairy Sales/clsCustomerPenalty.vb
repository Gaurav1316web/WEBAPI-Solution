Imports System.Data.SqlClient
Imports common

Public Class clsCustomerPenalty
#Region "Variables"
    Public Document_No As String = Nothing
    Public Document_date As Date? = Nothing
    Public Cust_Code As String = Nothing
    Public Penalty_Per As Decimal = 0
    Public From_Date As Date? = Nothing
    Public To_Date As Date? = Nothing
    Public Remarks As String = Nothing
    Public Status As Integer = 0
    Public Total_Penalty As Decimal = 0
    Public Arr As List(Of clsCustomerPenaltyDetail) = Nothing
    Public ArrInvoiceDetails As List(Of clsCustomerPenaltyInvoiceDetail) = Nothing
    Public ArrReceiptDetails As List(Of clsCustomerPenaltyReceiptDetail) = Nothing
#End Region
    Public Function SaveData(ByVal obj As clsCustomerPenalty, ByVal isNewEntry As Boolean, ByVal strTransType As String, ByVal AutoSave As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, Nothing, trans, AutoSave)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function SaveData(ByVal obj As clsCustomerPenalty, ByVal isNewEntry As Boolean, ByVal strTransType As String, ByVal trans As SqlTransaction, ByVal AutoSave As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "delete from TSPL_CUSTOMER_PENALTY_RECEIPT where Document_No='" & obj.Document_No & "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_CUSTOMER_PENALTY_INVOICE where Document_No='" & obj.Document_No & "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_CUSTOMER_PENALTY_DETAIL where Document_No='" & obj.Document_No & "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_date", clsCommon.GetPrintDate(obj.Document_date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code)
            clsCommon.AddColumnsForChange(coll, "From_Date", clsCommon.GetPrintDate(obj.From_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "To_Date", clsCommon.GetPrintDate(obj.To_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Penalty_Per", obj.Penalty_Per)
            clsCommon.AddColumnsForChange(coll, "Total_Penalty", obj.Total_Penalty)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks, True)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_date, clsDocType.CustomerPenalty, "", "")
                If (clsCommon.myLen(obj.Document_No) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_PENALTY", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_PENALTY", OMInsertOrUpdate.Update, "TSPL_CUSTOMER_PENALTY.Document_No='" & obj.Document_No & "'", trans)
            End If
            Dim objDetail As New clsCustomerPenaltyDetail()
            isSaved = isSaved AndAlso objDetail.SaveData(obj.Document_No, obj.Arr, trans)

        Catch err As Exception

            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Function GetData(ByVal strRetCode As String, ByVal NavType As NavigatorType) As clsCustomerPenalty
        Return GetData(strRetCode, NavType, Nothing)
    End Function

    Public Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsCustomerPenalty
        Dim obj As clsCustomerPenalty = Nothing
        Dim qry As String = "select * from TSPL_CUSTOMER_PENALTY where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry &= " and TSPL_CUSTOMER_PENALTY.Document_No = (select MIN(Document_No) from TSPL_CUSTOMER_PENALTY)"
            Case NavigatorType.Last
                qry &= " and TSPL_CUSTOMER_PENALTY.Document_No = (select Max(Document_No) from TSPL_CUSTOMER_PENALTY)"
            Case NavigatorType.Next
                qry &= " and TSPL_CUSTOMER_PENALTY.Document_No = (select Min(Document_No) from TSPL_CUSTOMER_PENALTY where Document_No >'" & strCode & "')"
            Case NavigatorType.Previous
                qry &= " and TSPL_CUSTOMER_PENALTY.Document_No = (select Max(Document_No) from TSPL_CUSTOMER_PENALTY where Document_No <'" & strCode & "')"
            Case NavigatorType.Current
                qry &= " and TSPL_CUSTOMER_PENALTY.Document_No = '" & strCode & "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsCustomerPenalty()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_date = clsCommon.myCDate(dt.Rows(0)("Document_date"))
            obj.Cust_Code = clsCommon.myCstr(dt.Rows(0)("Cust_Code"))
            obj.Penalty_Per = clsCommon.myCdbl(dt.Rows(0)("Penalty_Per"))
            obj.Total_Penalty = clsCommon.myCdbl(dt.Rows(0)("Total_Penalty"))
            obj.From_Date = clsCommon.myCDate(dt.Rows(0)("From_Date"))
            obj.To_Date = clsCommon.myCDate(dt.Rows(0)("To_Date"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            Dim objDetail As New clsCustomerPenaltyDetail()
            obj.Arr = objDetail.GetData(obj.Document_No, trans)
            Dim objInvoiceDetail As New clsCustomerPenaltyInvoiceDetail()
            obj.ArrInvoiceDetails = objInvoiceDetail.GetData(obj.Document_No, 0, True, trans)
            Dim objReceiptDetail As New clsCustomerPenaltyReceiptDetail()
            obj.ArrReceiptDetails = objReceiptDetail.GetData(obj.Document_No, 0, True, trans)
        End If
        Return obj
    End Function

    Public Function getFinder(ByVal strCode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim sql As String = "select Document_No as DocumentNo ,convert(varchar(12),Document_date,103) as DocumentDate,convert(varchar,From_Date,103) as [From Date],convert(varchar,To_Date,103) as [To Date],Penalty_Per as [Penalty Per],Total_Penalty as [Total Penalty],case when Status = 1 then 'posted' else 'Unposted' end as Posted from TSPL_CUSTOMER_PENALTY"
        str = clsCommon.ShowSelectForm("CustPnlty", sql, "DocumentNo", "", strCode, "DocumentNo", isButtonClicked)
        Return str
    End Function

    Public Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            isSaved = isSaved AndAlso PostData(FormId, strDocNo, trans)

            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Docume nt No not found to Post")
            End If
            Dim obj As clsCustomerPenalty = New clsCustomerPenalty()
            obj = obj.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = 1) Then
                Throw New Exception("Already Posted")
            End If

            clsDBFuncationality.ExecuteNonQuery("Update TSPL_CUSTOMER_PENALTY set Status= 1, Posted_By = '" & objCommonVar.CurrentUserCode & "',Posted_Date = '" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt") & "'  where Document_No='" & obj.Document_No & "'", trans)

        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim isResponse As Boolean = False
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If ReverseAndUnpost(strCode, trans) Then
                isResponse = True
            Else
                isResponse = False
            End If
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return isResponse
    End Function

    Public Function ReverseAndUnpost(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim isResponse As Boolean = True
        Try

            Dim obj As clsCustomerPenalty = New clsCustomerPenalty
            obj = obj.GetData(strCode, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Status) <= 0) Then
                clsCommon.MyMessageBoxShow("No Data found to Reverse And UnPost")
                isResponse = False
            End If

            If Not obj.Status = 1 Then
                clsCommon.MyMessageBoxShow("Transaction status should be posted for reverse and unpost")
                isResponse = False
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Status", 0)
            clsCommon.AddColumnsForChange(coll, "Posted_By", Nothing, True)
            clsCommon.AddColumnsForChange(coll, "Posted_Date", Nothing, True)
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_PENALTY", OMInsertOrUpdate.Update, "Document_No='" & obj.Document_No & "'", trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isResponse
    End Function

    Public Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim obj As clsCustomerPenalty = New clsCustomerPenalty()
        obj = obj.GetData(strCode, NavigatorType.Current, trans)
        Try
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Document No not found to Delete")
            End If
            If clsCommon.CompairString(obj.Status, "1") = CompairStringResult.Equal Then
                Throw New Exception("Already Posted")
            End If
            Dim qry As String = Nothing
            qry = "delete from TSPL_CUSTOMER_PENALTY_RECEIPT where Document_No='" & obj.Document_No & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_CUSTOMER_PENALTY_INVOICE where Document_No='" & obj.Document_No & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_CUSTOMER_PENALTY_DETAIL where Document_No='" & obj.Document_No & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_CUSTOMER_PENALTY where Document_No='" & obj.Document_No & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PrintDOSData(ByVal ArrRoute As ArrayList, ByVal ItemType As String, ByVal DocDate As Date, ByVal Status As Integer, ByVal IsIndividualCustomer As Boolean, isMultipleRoutes As Boolean, ByVal isRouteSummary As Boolean, ByVal CharColumn As Integer, ByVal CharRows As Integer, ByVal EnumPageSize As DosPaperSize, ByVal enumPageSetup As PageSetup, ByVal isPdf As Boolean) As String
        Try
            Dim whrcls As String = ""
            If clsCommon.myLen(ItemType) > 0 Then
                whrcls += " and TSPL_Product_DEMAND_BOOKING_MASTER.ItemType='" + ItemType + "' "
            End If
            Dim qry As String = "( SELECT TSPL_COMPANY_MASTER.Logo_Img2,TSPL_COMPANY_MASTER.Logo_Img,Access_officer,Comp_Code1,Is_FreshItem,Is_Ambient ,TSPL_ITEM_MASTER.IsTaxable,TSPL_VEHICLE_MASTER.Description,Vehicle_Id,TSPL_COMPANY_MASTER.Comp_Name ,tspl_transport_master.Transporter_Name,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.City_Code,TSPL_COMPANY_MASTER.Pincode,TSPL_COMPANY_MASTER.State,TSPL_COMPANY_MASTER.Phone1 ,TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Cust_Code  ,(TSPL_ITEM_MASTER.Alies_Name)Short_Description,TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Cust_Code as Booth, "
            qry += "TSPL_Product_DEMAND_BOOKING_MASTER.Route_No,TSPL_ROUTE_MASTER.Route_Desc,TSPL_Product_DEMAND_BOOKING_MASTER.Document_Date, TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,
TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.ItemNetAmount as Amount,TSPL_ITEM_MASTER.Short_Description + 'Amt' AS Item_Description,"
            qry += " TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Unit_code, Case When TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Unit_code='Crate' Then TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Qty Else 0 end CRATE,TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Qty,TSPL_ITEM_MASTER.Sku_Seq,
		    		Case When TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Unit_code='Pouch' Then TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Qty Else 0 End Pouch,0 AS Receipt_Amount "
            qry += ",TSPL_CUSTOMER_MASTER.Display_Seq FROM TSPL_PRODUCT_DEMAND_BOOKING_DETAIL "

            qry += " 
left outer join TSPL_Product_DEMAND_BOOKING_MASTER on TSPL_Product_DEMAND_BOOKING_MASTER.Document_No=TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Document_No
Left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = TSPL_Product_DEMAND_BOOKING_MASTER.Route_No 
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Item_Code
LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Cust_Code
left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.zone_code = TSPL_CUSTOMER_MASTER.zone_code
left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_Product_DEMAND_BOOKING_MASTER.Comp_Code
left outer join tspl_vehicle_master on tspl_vehicle_master.vehicle_id =TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.vehicle_code
left outer join tspl_transport_master on tspl_transport_master.Transport_Id=tspl_vehicle_master.Transport_Id
where 2 = 2 "

            qry += " and TSPL_Product_DEMAND_BOOKING_MASTER.Posted = 1 "
            qry += "" & whrcls & "  "
            qry += " and Cast(TSPL_Product_DEMAND_BOOKING_MASTER.Document_Date as Date) >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(clsCommon.myCDate(DocDate)), "dd/MMM/yyyy") + "' and Cast(TSPL_Product_DEMAND_BOOKING_MASTER.Document_Date as Date) <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.myCDate(DocDate)), "dd/MMM/yyyy") + "'"
            If Not IsIndividualCustomer Then
                qry += " and TSPL_Product_DEMAND_BOOKING_MASTER.IsIndividualCustomer=0 "
            End If
            If ArrRoute IsNot Nothing AndAlso ArrRoute.Count > 0 Then
                qry += " And TSPL_Product_DEMAND_BOOKING_MASTER.Route_No In (" & clsCommon.GetMulcallString(ArrRoute) & ") "
            End If
            qry += " ) "
            Dim dtPrint As DataTable = clsDBFuncationality.GetDataTable(qry + " order by Sku_Seq")
            If dtPrint Is Nothing OrElse dtPrint.Rows.Count <= 0 Then
                Throw New Exception("No Data Found to Print")
            ElseIf dtPrint.Rows.Count > 0 Then
                Dim dtItems As DataTable = clsDBFuncationality.GetDataTable("select Item_Code,max(Sku_Seq) as Sku_Seq,max(Short_Description) as Short_Description,max(Unit_code)Unit_code from (" + qry + ") x group by Item_Code order by Sku_Seq")
                Dim FinalQuery As String = " With CTERawData as ( " + qry + "  )" + Environment.NewLine + Environment.NewLine


                FinalQuery += " select " + clsCommon.myCstr(1) + " as Grp , ROW_NUMBER() over (order by max(Display_Seq)) As SNo,'" & IIf(ItemType = "PRODUCT", "PRODUCT", "ICECREAM") & "' as ProductOrIceCream,'" & IIf(Status = 1, "Approved", "Pending") & "' as Status,'" + clsCommon.GetPrintDate(clsCommon.myCDate(DocDate), "dd/MM/yyyy") + "' AS Date, max(Access_officer) as Access_officer,max(Comp_Code1) as Comp_Code1,max(Description) as Description,max(Vehicle_Id) as Vehicle_Id,max(Comp_Name) as Comp_Name,max(Transporter_Name) as Transporter_Name,max(Add1) as Add1,max(City_Code) as City_Code,max(Pincode) as Pincode,max(State) as State,max(Phone1) as Phone1 ,max(Booth) as Booth,"
                If isRouteSummary Then
                        FinalQuery += "max(Cust_Code)Cust_Code, Route_No,"
                    Else
                        FinalQuery += "Cust_Code,max(Route_No) as Route_No,"
                    End If
                FinalQuery += " max(Route_Desc) as Route_Desc,max(Document_Date) as Document_Date"

                For ii As Integer = 0 To dtItems.Rows.Count - 1
                    Dim strShortDesc As String = clsCommon.myCstr(dtItems.Rows(ii)("Short_Description"))
                    If clsCommon.myLen(strShortDesc) <= 0 Then
                        Throw New Exception("Please set Short Description for item [" + clsCommon.myCstr(dtItems.Rows(ii)("Item_Code")) + "]")
                    End If
                    FinalQuery += ",CEILING(sum(case when Item_Code='" + clsCommon.myCstr(dtItems.Rows(ii)("Item_Code")) + "'  then Qty else null end )) as [" + strShortDesc + "] "
                Next

                FinalQuery += " ,sum(Amount*case when IsTaxable=0 then 1 else 0 end) as Amount,sum(Amount*case when IsTaxable=0 then 0 else 1 end) as ProductAmount,sum(crate)TotalCrates ,max(Display_Seq) as Display_Seq,convert(varchar, max(Document_Date),103) as Document_Date,FORMAT(GETDATE(), 'dd/MM/yyyy hh:mm tt') as PrintDateTime from (
select xx.*,Qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor as QtyStock,TabDefaultUOM.Conversion_Factor ConvFacNo,TabCrateUOM.Conversion_Factor as ConvFacCrate	from CTERawData xx
left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xx.Item_Code and  TSPL_ITEM_UOM_DETAIL.UOM_Code=xx.Unit_code
left outer join TSPL_ITEM_UOM_DETAIL as TabDefaultUOM on TabDefaultUOM .Item_Code=xx.Item_Code and  TabDefaultUOM .Default_UOM=1
left outer join TSPL_ITEM_UOM_DETAIL as TabCrateUOM on TabCrateUOM.Item_Code=xx.Item_Code and  TabCrateUOM.UOM_Code='Crate' 
) x group by "

                If isRouteSummary Then
                    FinalQuery += " route_no"
                Else
                    FinalQuery += " Cust_Code"
                End If

                dtPrint = clsDBFuncationality.GetDataTable(FinalQuery)


                Dim obj As clsDosPrint = New clsDosPrint()
                obj.ReportName = objCommonVar.CurrentCompanyName
                obj.ApplyPrintCommand = True
                obj.HideGroupHeader = True
                obj.HideLastGroupTotal = True
                obj.ShowPageNo = True
                obj.PageSetupCustomizeCharColumn = CharColumn
                obj.PageSetupCustomizeCharRows = CharRows
                obj.objReportGroup = New clsDosPrintReportGroup
                obj.objReportGroup.Name = "Route_No"
                obj.objReportGroup.HeaderText1 = ItemType.ToUpper + " DEMAND SHEET FOR AREA NO: #$Route_No$# Date: #$Document_Date$#  Status: #$DocStatus$#"
                obj.objReportGroup.arrHeaderText1 = New List(Of clsDosPrintReportGroupReplaceHeader)

                Dim objGRH As New clsDosPrintReportGroupReplaceHeader
                objGRH.ColumnName = "Route_No"
                objGRH.ConstString = "#$Route_No$#"
                obj.objReportGroup.arrHeaderText1.Add(objGRH)
                objGRH = New clsDosPrintReportGroupReplaceHeader
                objGRH.ColumnName = "Date"
                objGRH.ConstString = "#$Document_Date$#"
                obj.objReportGroup.arrHeaderText1.Add(objGRH)
                objGRH = New clsDosPrintReportGroupReplaceHeader
                objGRH.ColumnName = "Status"
                objGRH.ConstString = "#$DocStatus$#"
                obj.objReportGroup.arrHeaderText1.Add(objGRH)
                obj.objReportGroup.HeaderText2 = "#$Route_Desc$# (ROUTE:#$Route_No$#)#$TranspoterName$# VEHICLE NO:#$Vehicle_No$# [#$PrintDateTime$#]"
                obj.objReportGroup.arrHeaderText2 = New List(Of clsDosPrintReportGroupReplaceHeader)
                objGRH = New clsDosPrintReportGroupReplaceHeader
                objGRH.ColumnName = "Route_Desc"
                objGRH.ConstString = "#$Route_Desc$#"
                obj.objReportGroup.arrHeaderText2.Add(objGRH)
                objGRH = New clsDosPrintReportGroupReplaceHeader
                objGRH.ColumnName = "Route_No"
                objGRH.ConstString = "#$Route_No$#"
                obj.objReportGroup.arrHeaderText2.Add(objGRH)
                objGRH = New clsDosPrintReportGroupReplaceHeader
                objGRH.ColumnName = "Transporter_Name"
                objGRH.ConstString = "#$TranspoterName$#"
                obj.objReportGroup.arrHeaderText2.Add(objGRH)
                objGRH = New clsDosPrintReportGroupReplaceHeader
                objGRH.ColumnName = "Description"
                objGRH.ConstString = "#$Vehicle_No$#"
                obj.objReportGroup.arrHeaderText2.Add(objGRH)
                objGRH = New clsDosPrintReportGroupReplaceHeader
                objGRH.ColumnName = "PrintDateTime"
                objGRH.ConstString = "#$PrintDateTime$#"
                obj.objReportGroup.arrHeaderText2.Add(objGRH)
                obj.arrFilter = New List(Of clsDosPrintHeaderFilter)()
                obj.arrColumn = New List(Of clsDosPrintColumn)()
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Cust_Code", "Booth", False, DosPrintAlignment.Left, 8, False, DecimalPlaces.NA))
                For ii As Integer = 0 To dtItems.Rows.Count - 1
                    If ii < 15 Then
                        Dim strShortDesc As String = clsCommon.myCstr(dtItems.Rows(ii)("Short_Description"))
                        obj.arrColumn.Add(clsDosPrintColumn.SetColumn(strShortDesc, strShortDesc.Replace(" ", ""), True, DosPrintAlignment.Right, 10, True, DecimalPlaces.Zero, clsCommon.myCstr(dtItems.Rows(ii)("Unit_code")), False))
                    End If
                Next

                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("ProductAmount", "Total Amt", True, DosPrintAlignment.Right, 15, True, DecimalPlaces.Two))
                Return obj.Print(obj, dtPrint, enumPageSetup, "", "", EnumPageSize, isPdf)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return Nothing
    End Function
End Class

Public Class clsCustomerPenaltyDetail

#Region "Variables"
    Public Document_No As String = Nothing
    Public Invoice_Date As Date
    Public Sale_Amt As Decimal
    Public Deposit_Amt As Decimal
    Public Curr_Balance_Amt As Decimal
    Public Balance_Amt As Decimal
    Public Penalty As Decimal
    Public ArrInvoiceAllDetails As List(Of clsCustomerPenaltyInvoiceDetail) = Nothing
    Public ArrReceiptAllDetails As List(Of clsCustomerPenaltyReceiptDetail) = Nothing
#End Region

    Public Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsCustomerPenaltyDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsCustomerPenaltyDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", strCode)
                clsCommon.AddColumnsForChange(coll, "Invoice_Date", clsCommon.GetPrintDate(obj.Invoice_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Sale_Amt", obj.Sale_Amt)
                clsCommon.AddColumnsForChange(coll, "Deposit_Amt", obj.Deposit_Amt)
                clsCommon.AddColumnsForChange(coll, "Curr_Balance_Amt", obj.Curr_Balance_Amt)
                clsCommon.AddColumnsForChange(coll, "Balance_Amt", obj.Balance_Amt)
                clsCommon.AddColumnsForChange(coll, "Penalty", obj.Penalty)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_PENALTY_DETAIL", OMInsertOrUpdate.Insert, "", trans)

                Dim PK_ID As Integer = clsERPFuncationality.GetScopeIdentityValue(trans)
                Dim objInvoiceDetail As New clsCustomerPenaltyInvoiceDetail()
                objInvoiceDetail.SaveData(strCode, PK_ID, obj.ArrInvoiceAllDetails, trans)

                Dim objReceiptDetail As New clsCustomerPenaltyReceiptDetail()
                objReceiptDetail.SaveData(strCode, PK_ID, obj.ArrReceiptAllDetails, trans)
            Next
        End If
        Return True
    End Function

    Public Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsCustomerPenaltyDetail)
        Dim arr As List(Of clsCustomerPenaltyDetail) = Nothing
        Dim qry As String = "select TSPL_CUSTOMER_PENALTY_DETAIL.* 
         from TSPL_CUSTOMER_PENALTY_DETAIL  where  TSPL_CUSTOMER_PENALTY_DETAIL.Document_No = '" & strCode & "' order by Document_No,PK_ID "
        Dim PK_ID As Integer = 0
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsCustomerPenaltyDetail)()
            For Each dr As DataRow In dt.Rows
                Dim obj As clsCustomerPenaltyDetail = New clsCustomerPenaltyDetail()
                obj.Document_No = clsCommon.myCstr(dr("Document_No"))
                obj.Invoice_Date = clsCommon.myCstr(dr("Invoice_Date"))
                obj.Sale_Amt = clsCommon.myCDecimal(dr("Sale_Amt"))
                obj.Deposit_Amt = clsCommon.myCDecimal(dr("Deposit_Amt"))
                obj.Curr_Balance_Amt = clsCommon.myCDecimal(dr("Curr_Balance_Amt"))
                obj.Balance_Amt = clsCommon.myCDecimal(dr("Balance_Amt"))
                obj.Penalty = clsCommon.myCDecimal(dr("Penalty"))
                PK_ID = clsCommon.myCdbl(dr("PK_ID"))
                Dim objAPInvoiceDetail As New clsCustomerPenaltyInvoiceDetail()
                obj.ArrInvoiceAllDetails = objAPInvoiceDetail.GetData(strCode, PK_ID, False, trans)
                Dim objReceiptDetail As New clsCustomerPenaltyReceiptDetail()
                obj.ArrReceiptAllDetails = objReceiptDetail.GetData(obj.Document_No, PK_ID, False, trans)
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function

End Class

Public Class clsCustomerPenaltyInvoiceDetail
#Region "Variables"
    Public Document_No As String = Nothing
    Public Ref_PK_ID As Integer
    Public Invoice_No As String = Nothing
    Public Invoice_Amt As Decimal
#End Region
    Public Function SaveData(ByVal strCode As String, ByVal PK_ID As Integer, ByVal Arr As List(Of clsCustomerPenaltyInvoiceDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsCustomerPenaltyInvoiceDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", strCode)
                clsCommon.AddColumnsForChange(coll, "Ref_PK_ID", PK_ID)
                clsCommon.AddColumnsForChange(coll, "Invoice_No", obj.Invoice_No)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_PENALTY_INVOICE", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Function GetData(ByVal strCode As String, ByVal Ref_PK_ID As Integer, ByVal LoadAllData As Boolean, ByVal trans As SqlTransaction) As List(Of clsCustomerPenaltyInvoiceDetail)
        Dim arr As List(Of clsCustomerPenaltyInvoiceDetail) = Nothing

        Dim qry As String = "select TSPL_CUSTOMER_PENALTY_INVOICE.Ref_PK_ID,TSPL_CUSTOMER_PENALTY_INVOICE.Invoice_No,tspl_sd_sale_invoice_head.Total_Amt as Invoice_Amt from TSPL_CUSTOMER_PENALTY_INVOICE  
        inner join TSPL_CUSTOMER_PENALTY_DETAIL on TSPL_CUSTOMER_PENALTY_DETAIL.PK_Id = TSPL_CUSTOMER_PENALTY_INVOICE.Ref_PK_ID left outer join tspl_sd_sale_invoice_head on tspl_sd_sale_invoice_head.Document_Code=TSPL_CUSTOMER_PENALTY_INVOICE.Invoice_No   where  TSPL_CUSTOMER_PENALTY_INVOICE.Document_No = '" & strCode & "' "

        If Not LoadAllData Then
            qry &= " and Ref_PK_ID = " & clsCommon.myCstr(Ref_PK_ID) & " "
        End If
        qry &= " order by TSPL_CUSTOMER_PENALTY_DETAIL.Document_No, TSPL_CUSTOMER_PENALTY_DETAIL.PK_ID "

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsCustomerPenaltyInvoiceDetail)()
            For Each dr As DataRow In dt.Rows
                Dim obj As clsCustomerPenaltyInvoiceDetail = New clsCustomerPenaltyInvoiceDetail()
                obj.Document_No = strCode
                obj.Invoice_No = clsCommon.myCstr(dr("Invoice_No"))
                obj.Ref_PK_ID = clsCommon.myCdbl(dr("Ref_PK_ID"))
                obj.Invoice_Amt = clsCommon.myCDecimal(dr("Invoice_Amt"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class


Public Class clsCustomerPenaltyReceiptDetail
#Region "Variables"
    Public Document_No As String = Nothing
    Public Ref_PK_ID As Integer
    Public Receipt_No As String = Nothing
    Public Receipt_Amt As Decimal
#End Region
    Public Function SaveData(ByVal strCode As String, ByVal PK_ID As Integer, ByVal Arr As List(Of clsCustomerPenaltyReceiptDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsCustomerPenaltyReceiptDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", strCode)
                clsCommon.AddColumnsForChange(coll, "Ref_PK_ID", PK_ID)
                clsCommon.AddColumnsForChange(coll, "Receipt_No", obj.Receipt_No)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_PENALTY_RECEIPT", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Function GetData(ByVal strCode As String, ByVal Ref_PK_ID As Integer, ByVal LoadAllData As Boolean, ByVal trans As SqlTransaction) As List(Of clsCustomerPenaltyReceiptDetail)
        Dim arr As List(Of clsCustomerPenaltyReceiptDetail) = Nothing

        Dim qry As String = "select TSPL_CUSTOMER_PENALTY_RECEIPT.Ref_PK_ID,TSPL_CUSTOMER_PENALTY_RECEIPT.Receipt_No,TSPL_RECEIPT_HEADER.Receipt_Amount from TSPL_CUSTOMER_PENALTY_RECEIPT  
        inner join TSPL_CUSTOMER_PENALTY_DETAIL on TSPL_CUSTOMER_PENALTY_DETAIL.PK_Id = TSPL_CUSTOMER_PENALTY_RECEIPT.Ref_PK_ID left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=TSPL_CUSTOMER_PENALTY_RECEIPT.Receipt_No    where  TSPL_CUSTOMER_PENALTY_RECEIPT.Document_No = '" & strCode & "' "

        If Not LoadAllData Then
            qry &= " and Ref_PK_ID = " & clsCommon.myCstr(Ref_PK_ID) & " "
        End If
        qry &= " order by TSPL_CUSTOMER_PENALTY_DETAIL.Document_No, TSPL_CUSTOMER_PENALTY_DETAIL.PK_ID "

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsCustomerPenaltyReceiptDetail)()
            For Each dr As DataRow In dt.Rows
                Dim obj As clsCustomerPenaltyReceiptDetail = New clsCustomerPenaltyReceiptDetail()
                obj.Document_No = strCode
                obj.Receipt_No = clsCommon.myCstr(dr("Receipt_No"))
                obj.Ref_PK_ID = clsCommon.myCdbl(dr("Ref_PK_ID"))
                obj.Receipt_Amt = clsCommon.myCDecimal(dr("Receipt_Amount"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class