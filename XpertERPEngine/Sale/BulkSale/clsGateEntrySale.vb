''changes by richa agarwal against ticket no. BM00000006545,BM00000005378
Imports common
Imports System.Data.SqlClient
Public Class clsGateEntrySale
#Region "Variables"
    Public Document_No As String = Nothing
    Public Document_Date As Date?
    Public Tanker_No As String = Nothing
    Public Dispatch_No As String = Nothing
    Public IsSaleReturn As String = Nothing
    Public Location_Code As String = Nothing
    Public Customer_Code As String = Nothing
    Public Tanker_Transporter_Code As String = Nothing
    Public Transporter_name As String = ""
    Public Tanker_name As String = ""
    Public SalesOrder_Code As String = Nothing
    Public Posted As Integer = 0
    Public Posting_Date As Date?
    Public Modified_Date As Date?
    Public Created_Date As Date?
    Public SaleReturnAgaintGEN As String = Nothing
    Public BulkSONo As String = Nothing
#End Region


    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        'Dim qry As String = "Select Document_No as Code,Document_Date as Date from TSPL_GATEENTRY_SALE  "
        Dim qry As String = "Select TSPL_GATEENTRY_SALE.Document_No as Code,Convert(varchar,TSPL_GATEENTRY_SALE.Document_Date,103) as Date,TSPL_GATEENTRY_SALE.Tanker_No as [Tanker No],TSPL_TANKER_MASTER.Tanker_Name AS [Tanker Name],TSPL_GATEENTRY_SALE.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Description],TSPL_GATEENTRY_SALE.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],TSPL_GATEENTRY_SALE.Tanker_Transporter_Code as [Transporter No],TSPL_VENDOR_MASTER.Vendor_Name as [Transporter Description] from TSPL_GATEENTRY_SALE Left Outer join TSPL_TANKER_MASTER on TSPL_GATEENTRY_SALE.Tanker_No =TSPL_TANKER_MASTER.Tanker_No Left Outer Join TSPL_LOCATION_MASTER on TSPL_GATEENTRY_SALE.Location_Code=TSPL_LOCATION_MASTER.Location_Code Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_GATEENTRY_SALE.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code Left Outer Join TSPL_VENDOR_MASTER on TSPL_GATEENTRY_SALE.Tanker_Transporter_Code=TSPL_VENDOR_MASTER.Vendor_Code and TSPL_VENDOR_MASTER.Form_Type='TTM'"
        str = clsCommon.ShowSelectForm("GateEntry", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function

    Public Shared Function getTankerFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try
            Dim qry As String = "Select TSPL_GATEENTRY_SALE.Tanker_No as [TankerNo],TSPL_GATEENTRY_SALE.Document_No as [GateEntryNo],Convert(varchar,TSPL_GATEENTRY_SALE.Document_Date,103) as Date,TSPL_GATEENTRY_SALE.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Description],TSPL_GATEENTRY_SALE.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name] from TSPL_GATEENTRY_SALE  Left Outer Join TSPL_LOCATION_MASTER on TSPL_GATEENTRY_SALE.Location_Code=TSPL_LOCATION_MASTER.Location_Code Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_GATEENTRY_SALE.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code"
            ' str = clsCommon.ShowSelectForm("GateEntry", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
            str = customFinder.getFinder("TNKRFNDGES", qry, whrcls, "TankerNo", curcode, "GateEntryNo")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

        Return str
    End Function
    Public Shared Function getItemsDetail() As String
        Dim str As String = ""
        Try
            str += " select TSPL_SALES_ORDER_MASTER_BULKSALE.Document_No as sale_order_no,TSPL_SALES_ORDER_DETAIL_BULKSALE.item_code,TSPL_SALES_ORDER_DETAIL_BULKSALE.item_name,TSPL_SALES_ORDER_DETAIL_BULKSALE.Unit_code  from TSPL_GATEENTRY_SALE "
            str += " left join TSPL_SALES_ORDER_MASTER_BULKSALE on TSPL_GATEENTRY_SALE.Bulk_SO_No=TSPL_SALES_ORDER_MASTER_BULKSALE.Document_No"
            str += " left join TSPL_SALES_ORDER_DETAIL_BULKSALE on TSPL_SALES_ORDER_MASTER_BULKSALE.Document_No=TSPL_SALES_ORDER_DETAIL_BULKSALE.Document_No "
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str
    End Function
    Public Shared Function SaveData(ByVal obj As clsGateEntrySale, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal obj As clsGateEntrySale, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkSale, clsUserMgtCode.FrmGateEntrySale, obj.Location_Code, clsCommon.myCDate(obj.Document_Date), trans)
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.GatePassEntrySale, "", obj.Location_Code)
            End If
            isSaved = clsDBFuncationality.ExecuteNonQuery("update TSPL_GATEENTRY_SALE set IsSaleReturn ='N' where Document_No =(select SaleReturnAgaintGEN from TSPL_GATEENTRY_SALE where Document_No='" & obj.Document_No & "') ", trans)

            ''richa 09/09/2014
            'Dim Tankervalue As String = clsDBFuncationality.getSingleValue("Select Tanker_Name  from TSPL_TANKER_MASTER where Tanker_No='" + obj.Tanker_No + "'")
            'If Tankervalue Is Nothing Then
            '    Dim qry As String = " insert into TSPL_TANKER_MASTER(Tanker_Transporter_Code,Description,Tanker_No,Tanker_Name,Storage_Capacity,Year,Inner_SS,Outer_SS,Shift_Charges,Avg_KM_Ltr,Diesel_Rate,Price_KM,Price_Ltr,Ltr_KG,Rental_Day,Rental_Week,Rental_Month,Created_By,Created_Date,Modified_By,Modified_Date,comp_code,StorageCapacityDesc ) values ('" + obj.Tanker_Transporter_Code + "','" + obj.Transporter_name + "','" + obj.Tanker_No + "','" + obj.Tanker_name + "',0,'2014','NO','NO',0,0,0,0,0,0,0,0,0,'" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy") + "','" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy") + "','" + objCommonVar.CurrentCompanyCode + "','')"
            '    clsDBFuncationality.ExecuteNonQuery(qry)
            'End If
            ''==================================
            Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, trans)
            Dim coll As New Hashtable()
            ''added  by shivani
            If DateTime = "1" Then
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
            End If

            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code, True)
            clsCommon.AddColumnsForChange(coll, "Customer_Code", obj.Customer_Code, True)
            clsCommon.AddColumnsForChange(coll, "Tanker_No", obj.Tanker_No, True)
            clsCommon.AddColumnsForChange(coll, "Dispatch_No", obj.Dispatch_No, True)
            If clsCommon.CompairString(obj.IsSaleReturn, "Y") = CompairStringResult.Equal Then
                clsCommon.AddColumnsForChange(coll, "IsSaleReturn", obj.IsSaleReturn)
            Else
                clsCommon.AddColumnsForChange(coll, "IsSaleReturn", "N")
            End If

            clsCommon.AddColumnsForChange(coll, "SaleReturnAgaintGEN", obj.SaleReturnAgaintGEN, True)
            clsCommon.AddColumnsForChange(coll, "Tanker_Transporter_Code", obj.Tanker_Transporter_Code, True)
            clsCommon.AddColumnsForChange(coll, "SalesOrder_Code", obj.SalesOrder_Code, True)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Bulk_SO_No", obj.BulkSONo, True)
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                isSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GATEENTRY_SALE", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GATEENTRY_SALE", OMInsertOrUpdate.Update, "TSPL_GATEENTRY_SALE.Document_No='" + obj.Document_No + "'", trans)
            End If

            isSaved = clsDBFuncationality.ExecuteNonQuery("update TSPL_GATEENTRY_SALE set IsSaleReturn ='Y' where Document_No =(select SaleReturnAgaintGEN from TSPL_GATEENTRY_SALE where Document_No='" & obj.Document_No & "') ", trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function SaveDataHistory(ByVal obj As clsGateEntrySale, ByVal trans As SqlTransaction, ByVal isHistory As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code, True)
            clsCommon.AddColumnsForChange(coll, "Customer_Code", obj.Customer_Code, True)
            clsCommon.AddColumnsForChange(coll, "Tanker_No", obj.Tanker_No, True)
            clsCommon.AddColumnsForChange(coll, "Dispatch_No", obj.Dispatch_No, True)
            If clsCommon.CompairString(obj.IsSaleReturn, "Y") = CompairStringResult.Equal Then
                clsCommon.AddColumnsForChange(coll, "IsSaleReturn", obj.IsSaleReturn)
            Else
                clsCommon.AddColumnsForChange(coll, "IsSaleReturn", "N")
            End If

            clsCommon.AddColumnsForChange(coll, "SaleReturnAgaintGEN", obj.SaleReturnAgaintGEN, True)
            clsCommon.AddColumnsForChange(coll, "Tanker_Transporter_Code", obj.Tanker_Transporter_Code, True)
            clsCommon.AddColumnsForChange(coll, "SalesOrder_Code", obj.SalesOrder_Code, True)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Posted", obj.Posted)
            If clsCommon.myLen(obj.Posting_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Posting_Date", clsCommon.GetPrintDate(obj.Posting_Date, "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "Posting_Date", Nothing, True)
            End If
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(obj.Modified_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Bulk_SO_No", obj.BulkSONo, True)
            If isHistory Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(obj.Created_Date, "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                isSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GATEENTRY_SALE_HISTORY", OMInsertOrUpdate.Insert, "", trans)
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal arrLoc As String, ByVal NavType As NavigatorType) As clsGateEntrySale
        Return GetData(strCode, arrLoc, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal arrLoc As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsGateEntrySale
        Dim obj As clsGateEntrySale = Nothing
        Dim Arr As List(Of clsGateEntrySale) = Nothing
        Dim qry As String = "select * from TSPL_GATEENTRY_SALE where 2=2  "
        If clsCommon.myLen(arrLoc) > 0 Then
            qry += " and TSPL_GATEENTRY_SALE.Location_Code in (" + arrLoc + ")"
        End If
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_GATEENTRY_SALE.Document_No = (select MIN(Document_No) from TSPL_GATEENTRY_SALE WHERE 1=1 " + whrclas + " and TSPL_GATEENTRY_SALE.Location_Code in (" + arrLoc + ") )"
            Case NavigatorType.Last
                qry += " and TSPL_GATEENTRY_SALE.Document_No = (select Max(Document_No) from TSPL_GATEENTRY_SALE WHERE 1=1 " + whrclas + " and TSPL_GATEENTRY_SALE.Location_Code in (" + arrLoc + ") )"
            Case NavigatorType.Current
                qry += " and TSPL_GATEENTRY_SALE.Document_No = '" + strCode + "' "
            Case NavigatorType.Next
                qry += " and TSPL_GATEENTRY_SALE.Document_No = (select Min(Document_No) from TSPL_GATEENTRY_SALE where Document_No > '" + strCode + "' " + whrclas + " and TSPL_GATEENTRY_SALE.Location_Code in (" + arrLoc + ") )"
            Case NavigatorType.Previous
                qry += " and TSPL_GATEENTRY_SALE.Document_No = (select Max(Document_No) from TSPL_GATEENTRY_SALE where Document_No < '" + strCode + "' " + whrclas + " and TSPL_GATEENTRY_SALE.Location_Code in (" + arrLoc + ") )"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsGateEntrySale()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Tanker_No = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Tanker_Transporter_Code = clsCommon.myCstr(dt.Rows(0)("Tanker_Transporter_Code"))
            obj.Posted = clsCommon.myCdbl(dt.Rows(0)("Posted"))
            obj.Customer_Code = clsCommon.myCstr(dt.Rows(0)("Customer_Code"))
            obj.Dispatch_No = clsCommon.myCstr(dt.Rows(0)("Dispatch_No"))
            obj.IsSaleReturn = clsCommon.myCstr(dt.Rows(0)("IsSaleReturn"))
            obj.SaleReturnAgaintGEN = clsCommon.myCstr(dt.Rows(0)("SaleReturnAgaintGEN"))
            obj.SalesOrder_Code = clsCommon.myCstr(dt.Rows(0)("SalesOrder_Code"))
            If dt.Rows(0)("Posting_Date") IsNot DBNull.Value Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            End If
            If dt.Rows(0)("Created_Date") IsNot DBNull.Value Then
                obj.Created_Date = clsCommon.myCDate(dt.Rows(0)("Created_Date"))
            End If
            If dt.Rows(0)("Modified_Date") IsNot DBNull.Value Then
                obj.Modified_Date = clsCommon.myCDate(dt.Rows(0)("Modified_Date"))
            End If
            obj.BulkSONo = clsCommon.myCstr(dt.Rows(0)("Bulk_SO_No"))
        End If
        Return obj
    End Function

    Public Shared Function DeleteData(ByVal strDocNo As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Document_Date,Location_Code from TSPL_GATEENTRY_SALE where Document_No='" + strDocNo + "'")
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkSale, clsUserMgtCode.FrmGateEntrySale, clsCommon.myCstr(dt.Rows(0)("Location_Code")), clsCommon.myCDate(dt.Rows(0)("Document_Date")), Nothing)

        End If

        Try
            Dim qry As String = "delete from TSPL_GATEENTRY_SALE where Document_No='" + strDocNo + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return isSaved
    End Function
    ''----------Added By Richa 
    Public Shared Function PostData(ByVal FormId As String, ByVal arrLoc As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(FormId, arrLoc, strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal arrLoc As String, ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean

        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Loading No not found to Post")
            End If
            Dim obj As clsGateEntrySale = clsGateEntrySale.GetData(strDocNo, arrLoc, NavigatorType.Current, trans)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkSale, clsUserMgtCode.FrmGateEntrySale, obj.Location_Code, clsCommon.myCDate(obj.Document_Date), trans)


            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            Dim qry = "Update TSPL_GATEENTRY_SALE set Posted=1, " & _
            "Posting_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "' " & _
            " where Document_No='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    '' Richa Code Ends Here

End Class
'' Created By Richa Agarwal Against Ticket No.BM00000003852 on 10/09/2014
Public Class ClsTankerOut
#Region "Variables"
    Public Document_No As String = Nothing
    Public Document_Date As String = Nothing
    Public GateEntryNo As String = Nothing
    Public Tanker_No As String = Nothing
    Public Location_Code As String = Nothing
    Public Customer_Code As String = Nothing
    Public IsGateOut As Integer = 0
    Public Customer_Name As String = Nothing
    Public Tanker_Name As String = Nothing
    Public Location_Name As String = Nothing
    Public SalesOrder_Code As String = String.Empty

    Public Seal_No1 As String = String.Empty
    Public Seal_No2 As String = String.Empty
    Public Seal_No3 As String = String.Empty
    Public Seal_No4 As String = String.Empty
    Public Seal_No5 As String = String.Empty

#End Region

    Public Shared Function getTankerFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try
            Dim qry As String = "Select TSPL_GATEENTRY_SALE.Tanker_No as [TankerNo],TSPL_GATEENTRY_SALE.Document_No as [GateEntryNo],Convert(varchar,TSPL_GATEENTRY_SALE.Document_Date,103) as Date,TSPL_GATEENTRY_SALE.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Description],TSPL_GATEENTRY_SALE.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name] from TSPL_Quality_Check_BulkSale left outer join TSPL_Dispatch_BulkSale on TSPL_Dispatch_BulkSale.QC_Code =TSPL_Quality_Check_BulkSale.QC_No left outer join TSPL_GATEENTRY_SALE on TSPL_GATEENTRY_SALE.Document_No =TSPL_Quality_Check_BulkSale.GateEntry_Document_No Left Outer Join TSPL_LOCATION_MASTER on TSPL_GATEENTRY_SALE.Location_Code=TSPL_LOCATION_MASTER.Location_Code Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_GATEENTRY_SALE.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code"
            str = customFinder.getFinder("TNKRFNDGOS", qry, whrcls, "TankerNo", curcode, "GateEntryNo")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str
    End Function
    Public Shared Function getTankerFinderForPavitra(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try
            Dim qry As String = "Select TSPL_GATEENTRY_SALE.Tanker_No as [TankerNo],TSPL_GATEENTRY_SALE.SalesOrder_Code as [SalesOrder],TSPL_GATEENTRY_SALE.Document_No as [GateEntryNo],Convert(varchar,TSPL_GATEENTRY_SALE.Document_Date,103) as Date,TSPL_GATEENTRY_SALE.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Description],TSPL_GATEENTRY_SALE.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name] from TSPL_Quality_Check_BulkSale left outer join TSPL_Dispatch_BulkSale on TSPL_Dispatch_BulkSale.QC_Code =TSPL_Quality_Check_BulkSale.QC_No left outer join TSPL_GATEENTRY_SALE on TSPL_GATEENTRY_SALE.Document_No =TSPL_Quality_Check_BulkSale.GateEntry_Document_No Left Outer Join TSPL_LOCATION_MASTER on TSPL_GATEENTRY_SALE.Location_Code=TSPL_LOCATION_MASTER.Location_Code Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_GATEENTRY_SALE.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code"
            str = customFinder.getFinder("SOFNDGOS", qry, whrcls, "SalesOrder", curcode, "GateEntryNo")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str
    End Function

    Public Shared Function SaveData(ByVal obj As ClsTankerOut, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function SaveData(ByVal obj As ClsTankerOut, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkSale, clsUserMgtCode.FrmTankerOut, obj.Location_Code, clsCommon.myCDate(obj.Document_Date), trans)
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.TankerOut, "", obj.Location_Code)
            End If
            Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, trans)
            Dim coll As New Hashtable()
            If DateTime = "1" Then
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
            End If

            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Customer_Code", obj.Customer_Code)
            clsCommon.AddColumnsForChange(coll, "Tanker_No", obj.Tanker_No)
            clsCommon.AddColumnsForChange(coll, "GateEntryNo", obj.GateEntryNo)
            clsCommon.AddColumnsForChange(coll, "IsGateOut", obj.IsGateOut)
            clsCommon.AddColumnsForChange(coll, "SalesOrder_Code", obj.SalesOrder_Code, True)
            If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowSealNumberForTunkerOut, clsFixedParameterCode.ShowSealNumberForTunkerOut, trans)) = "1", True, False)) Then
                clsCommon.AddColumnsForChange(coll, "Seal_No1", obj.Seal_No1)
                clsCommon.AddColumnsForChange(coll, "Seal_No2", obj.Seal_No2)
                clsCommon.AddColumnsForChange(coll, "Seal_No3", obj.Seal_No3)
                clsCommon.AddColumnsForChange(coll, "Seal_No4", obj.Seal_No4)
                clsCommon.AddColumnsForChange(coll, "Seal_No5", obj.Seal_No5)
            End If
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                isSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GATEOUT_SALE", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GATEOUT_SALE", OMInsertOrUpdate.Update, "TSPL_GATEOUT_SALE.Document_No='" + obj.Document_No + "'", trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal arrLoc As String, ByVal NavType As NavigatorType) As ClsTankerOut
        Return GetData(strCode, arrLoc, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal arrLoc As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsTankerOut
        Dim obj As ClsTankerOut = Nothing
        Dim Arr As List(Of ClsTankerOut) = Nothing
        Dim qry As String = "Select TSPL_GATEOUT_SALE.SalesOrder_Code,TSPL_GATEOUT_SALE.Document_No,TSPL_GATEOUT_SALE.Document_Date,TSPL_GATEOUT_SALE.GateEntryNo,TSPL_GATEOUT_SALE.Tanker_No,TSPL_TANKER_MASTER.Tanker_Name,TSPL_GATEOUT_SALE.Location_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_GATEOUT_SALE.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_GATEOUT_SALE.IsGateOut,TSPL_GATEOUT_SALE.Seal_No1,TSPL_GATEOUT_SALE.Seal_No2,TSPL_GATEOUT_SALE.Seal_No3,TSPL_GATEOUT_SALE.Seal_No4,TSPL_GATEOUT_SALE.Seal_No5 from TSPL_GATEOUT_SALE Left Outer join TSPL_TANKER_MASTER on TSPL_GATEOUT_SALE.Tanker_No =TSPL_TANKER_MASTER.Tanker_No Left Outer Join TSPL_LOCATION_MASTER on TSPL_GATEOUT_SALE.Location_Code=TSPL_LOCATION_MASTER.Location_Code Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_GATEOUT_SALE.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code where 2=2 and TSPL_GATEOUT_SALE.Location_Code in (" + arrLoc + ") "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_GATEOUT_SALE.Document_No = (select MIN(Document_No) from TSPL_GATEOUT_SALE WHERE 1=1 " + whrclas + " and TSPL_GATEOUT_SALE.Location_Code in (" + arrLoc + ") )"
            Case NavigatorType.Last
                qry += " and TSPL_GATEOUT_SALE.Document_No = (select Max(Document_No) from TSPL_GATEOUT_SALE WHERE 1=1 " + whrclas + " and TSPL_GATEOUT_SALE.Location_Code in (" + arrLoc + ") )"
            Case NavigatorType.Current
                qry += " and TSPL_GATEOUT_SALE.Document_No = '" + strCode + "' "
            Case NavigatorType.Next
                qry += " and TSPL_GATEOUT_SALE.Document_No = (select Min(Document_No) from TSPL_GATEOUT_SALE where Document_No > '" + strCode + "' " + whrclas + " and TSPL_GATEOUT_SALE.Location_Code in (" + arrLoc + ") )"
            Case NavigatorType.Previous
                qry += " and TSPL_GATEOUT_SALE.Document_No = (select Max(Document_No) from TSPL_GATEOUT_SALE where Document_No < '" + strCode + "' " + whrclas + " and TSPL_GATEOUT_SALE.Location_Code in (" + arrLoc + ") )"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsTankerOut()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.GateEntryNo = clsCommon.myCstr(dt.Rows(0)("GateEntryNo"))
            obj.Tanker_No = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
            obj.Tanker_Name = clsCommon.myCstr(dt.Rows(0)("Tanker_Name"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Location_Name = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
            obj.Customer_Code = clsCommon.myCstr(dt.Rows(0)("Customer_Code"))
            obj.Customer_Name = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
            obj.IsGateOut = clsCommon.myCdbl(dt.Rows(0)("IsGateOut"))
            obj.SalesOrder_Code = clsCommon.myCstr(dt.Rows(0)("SalesOrder_Code"))
            obj.Seal_No1 = clsCommon.myCstr(dt.Rows(0)("Seal_No1"))
            obj.Seal_No2 = clsCommon.myCstr(dt.Rows(0)("Seal_No2"))
            obj.Seal_No3 = clsCommon.myCstr(dt.Rows(0)("Seal_No3"))
            obj.Seal_No4 = clsCommon.myCstr(dt.Rows(0)("Seal_No4"))
            obj.Seal_No5 = clsCommon.myCstr(dt.Rows(0)("Seal_No5"))

        End If
        Return obj
    End Function

  
End Class
