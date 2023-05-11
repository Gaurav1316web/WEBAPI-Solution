Imports common
Imports System.Data.SqlClient
Public Class clsProductDispatchGateOut
#Region "Variable"

    Public Document_No As String = Nothing
    Public Gate_Out_Date As Date = Nothing
    Public Dispatch_No As String = Nothing
    Public Dispatch_Date As Date = Nothing
    Public Customer_Code As String = Nothing
    Public Customer_Name As String = Nothing
    Public Vehicle_Mannual_No As String = Nothing
    Public Vehicle_Id As String = Nothing          ' Not Table Column 
    Public From_Location As String = Nothing
    Public From_Location_Desc As String = Nothing  ' Not Table Column 
    Public To_Location As String = Nothing
    Public To_Location_Desc As String = Nothing    ' Not Table Column 
    Public Transport_Id As String = Nothing
    Public Transporter_Name As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As Date = Nothing
    Public Modify_By As String = Nothing
    Public Modify_Date As Date = Nothing
    Public Comp_Code As String = Nothing
    Public isNewEntry As Boolean = False

#End Region

    Public Shared Function deleteData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_Product_Dispatch_Gate_Out where Document_No='" & strDocNo & "'"
            Dim isDeleted As Boolean = True
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Return isDeleted
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function getGateOutFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try
            Dim qry As String = "  select TSPL_Product_Dispatch_Gate_Out.Document_No,TSPL_Product_Dispatch_Gate_Out.Gate_Out_Date,TSPL_Product_Dispatch_Gate_Out.Dispatch_No,TSPL_Product_Dispatch_Gate_Out.Dispatch_Date,TSPL_Product_Dispatch_Gate_Out.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_VEHICLE_MASTER.Vehicle_Id,TSPL_Product_Dispatch_Gate_Out.Vehicle_Mannual_No,TSPL_Product_Dispatch_Gate_Out.From_Location,TSPL_LOCATION_MASTER_1.Location_Desc as From_Location_Desc,TSPL_Product_Dispatch_Gate_Out.To_Location,TSPL_LOCATION_MASTER_2.Location_Desc as To_Location_Desc,TSPL_Product_Dispatch_Gate_Out.Transport_Id,TSPL_TRANSPORT_MASTER.Transporter_Name from TSPL_Product_Dispatch_Gate_Out left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_Product_Dispatch_Gate_Out.Cust_Code left outer join TSPL_VEHICLE_MASTER  on TSPL_VEHICLE_MASTER.Number=TSPL_Product_Dispatch_Gate_Out.Vehicle_Mannual_No left outer join  TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_1 on TSPL_LOCATION_MASTER_1 .Location_Code=TSPL_Product_Dispatch_Gate_Out.From_Location  left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_2 on TSPL_LOCATION_MASTER_2 .Location_Code=TSPL_Product_Dispatch_Gate_Out.To_Location left outer join TSPL_TRANSPORT_MASTER on  TSPL_TRANSPORT_MASTER.Transport_Id =TSPL_Product_Dispatch_Gate_Out.Transport_Id  "
            str = customFinder.getFinder("DOCDISPGATOUT", qry, "", curcode, "", "Document_No")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str
    End Function

    Public Shared Function getGateOutData(ByVal strCode As String, ByVal navtype As NavigatorType, Optional trans As SqlTransaction = Nothing) As clsProductDispatchGateOut
        Dim obj As New clsProductDispatchGateOut
        Try
            Dim whrCls As String = String.Empty
            Dim qst As String = " select TSPL_Product_Dispatch_Gate_Out.Document_No,TSPL_Product_Dispatch_Gate_Out.Gate_Out_Date,TSPL_Product_Dispatch_Gate_Out.Dispatch_No,TSPL_Product_Dispatch_Gate_Out.Dispatch_Date,TSPL_Product_Dispatch_Gate_Out.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_VEHICLE_MASTER.Vehicle_Id,TSPL_Product_Dispatch_Gate_Out.Vehicle_Mannual_No,TSPL_Product_Dispatch_Gate_Out.From_Location,TSPL_LOCATION_MASTER_1.Location_Desc as From_Location_Desc,TSPL_Product_Dispatch_Gate_Out.To_Location,TSPL_LOCATION_MASTER_2.Location_Desc as To_Location_Desc,TSPL_Product_Dispatch_Gate_Out.Transport_Id,TSPL_TRANSPORT_MASTER.Transporter_Name from TSPL_Product_Dispatch_Gate_Out left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_Product_Dispatch_Gate_Out.Cust_Code left outer join TSPL_VEHICLE_MASTER  on TSPL_VEHICLE_MASTER.Number=TSPL_Product_Dispatch_Gate_Out.Vehicle_Mannual_No left outer join  TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_1 on TSPL_LOCATION_MASTER_1 .Location_Code=TSPL_Product_Dispatch_Gate_Out.From_Location  left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_2 on TSPL_LOCATION_MASTER_2 .Location_Code=TSPL_Product_Dispatch_Gate_Out.To_Location left outer join TSPL_TRANSPORT_MASTER on  TSPL_TRANSPORT_MASTER.Transport_Id =TSPL_Product_Dispatch_Gate_Out.Transport_Id    where 1=1  and TSPL_Product_Dispatch_Gate_Out.comp_code='" & objCommonVar.CurrentCompanyCode & "' "

            qst = qst & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and TSPL_Product_Dispatch_Gate_Out.Document_No in ('" + strCode + "') "
                Case NavigatorType.Next
                    qst += " and TSPL_Product_Dispatch_Gate_Out.Document_No in (select min(Document_No ) from TSPL_Product_Dispatch_Gate_Out where Document_No  >'" + strCode + "'   " & whrCls & ")"
                Case NavigatorType.First
                    qst += " and TSPL_Product_Dispatch_Gate_Out.Document_No in (select MIN(Document_No ) from TSPL_Product_Dispatch_Gate_Out where 1=1  " & whrCls & ")"
                Case NavigatorType.Last
                    qst += " and TSPL_Product_Dispatch_Gate_Out.Document_No in (select Max(Document_No ) from TSPL_Product_Dispatch_Gate_Out where 1=1  " & whrCls & ")"
                Case NavigatorType.Previous
                    qst += " and TSPL_Product_Dispatch_Gate_Out.Document_No in (select Max(Document_No ) from TSPL_Product_Dispatch_Gate_Out where Document_No  <'" + strCode + "'   " & whrCls & ")"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
                obj.Gate_Out_Date = clsCommon.myCDate(dt.Rows(0)("Gate_Out_Date"))
                obj.Dispatch_No = clsCommon.myCstr(dt.Rows(0)("Dispatch_No"))
                obj.Dispatch_Date = clsCommon.myCDate(dt.Rows(0)("Dispatch_Date"))
                obj.Customer_Code = clsCommon.myCstr(dt.Rows(0)("Cust_Code"))
                obj.Customer_Name = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
                obj.Vehicle_Id = clsCommon.myCstr(dt.Rows(0)("Vehicle_Id"))
                obj.Vehicle_Mannual_No = clsCommon.myCstr(dt.Rows(0)("Vehicle_Mannual_No"))
                obj.From_Location = clsCommon.myCstr(dt.Rows(0)("From_Location"))
                obj.From_Location_Desc = clsCommon.myCstr(dt.Rows(0)("From_Location_Desc"))
                obj.To_Location = clsCommon.myCstr(dt.Rows(0)("To_Location"))
                obj.To_Location_Desc = clsCommon.myCstr(dt.Rows(0)("To_Location_Desc"))
                obj.Transport_Id = clsCommon.myCstr(dt.Rows(0)("Transport_Id"))
                obj.Transporter_Name = clsCommon.myCstr(dt.Rows(0)("Transporter_Name"))
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function

    Public Shared Function getDispatchFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try
            Dim qry As String = " Select TSPL_SD_SHIPMENT_HEAD.Document_Code as Dispatch_No , Convert (varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Dispatch_Date ,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location,TSPL_LOCATION_MASTER1.Location_Desc as Bill_To_Location_Desc,TSPL_SD_SHIPMENT_HEAD.Ship_To_Location,TSPL_LOCATION_MASTER2.Location_Desc as Ship_To_Location_Desc,TSPL_SD_SHIPMENT_HEAD.Transport_Id,TSPL_SD_SHIPMENT_HEAD.Transporter_Name ,TSPL_SD_SHIPMENT_HEAD.VehicleNo,Vehicle_Code,TSPL_SD_SHIPMENT_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name from TSPL_SD_SHIPMENT_HEAD left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER1 on TSPL_LOCATION_MASTER1.Location_Code=TSPL_SD_SHIPMENT_HEAD.Bill_To_Location left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER2 on TSPL_LOCATION_MASTER2.Location_Code=TSPL_SD_SHIPMENT_HEAD.Ship_To_Location "
            str = customFinder.getFinder("DISPGATOUT", qry, " TSPL_SD_SHIPMENT_HEAD.Trans_Type='PS' and TSPL_SD_SHIPMENT_HEAD.Status=1 and TSPL_SD_SHIPMENT_HEAD.Document_Code not in ( select TSPL_Product_Dispatch_Gate_Out.Dispatch_No from TSPL_Product_Dispatch_Gate_Out) ", curcode, "", "Dispatch_No")

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str
    End Function

    Public Shared Function getDispatchData(ByVal strCode As String, ByVal navtype As NavigatorType, Optional trans As SqlTransaction = Nothing) As clsProductDispatchGateOut
        Dim obj As New clsProductDispatchGateOut
        Try
            Dim whrCls As String = String.Empty
            Dim qst As String = "  Select TSPL_SD_SHIPMENT_HEAD.Document_Code as Dispatch_No , Convert (varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Dispatch_Date ,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location,TSPL_LOCATION_MASTER1.Location_Desc as Bill_To_Location_Desc,TSPL_SD_SHIPMENT_HEAD.Ship_To_Location,TSPL_LOCATION_MASTER2.Location_Desc as Ship_To_Location_Desc,TSPL_SD_SHIPMENT_HEAD.Transport_Id,TSPL_SD_SHIPMENT_HEAD.Transporter_Name ,TSPL_SD_SHIPMENT_HEAD.VehicleNo,Vehicle_Code,TSPL_SD_SHIPMENT_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name from TSPL_SD_SHIPMENT_HEAD left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER1 on TSPL_LOCATION_MASTER1.Location_Code=TSPL_SD_SHIPMENT_HEAD.Bill_To_Location left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER2 on TSPL_LOCATION_MASTER2.Location_Code=TSPL_SD_SHIPMENT_HEAD.Ship_To_Location where 1=1 and TSPL_SD_SHIPMENT_HEAD.Status=1 and TSPL_SD_SHIPMENT_HEAD.Document_Code not in ( select TSPL_Product_Dispatch_Gate_Out.Dispatch_No from TSPL_Product_Dispatch_Gate_Out)   and TSPL_SD_SHIPMENT_HEAD.comp_code='" & objCommonVar.CurrentCompanyCode & "' "

            qst = qst & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and TSPL_SD_SHIPMENT_HEAD.Document_Code in ('" + strCode + "') "
                Case NavigatorType.Next
                    qst += " and TSPL_SD_SHIPMENT_HEAD.Document_Code in (select min(Document_Code ) from TSPL_SD_SHIPMENT_HEAD where Document_Code  >'" + strCode + "'   " & whrCls & ")"
                Case NavigatorType.First
                    qst += " and TSPL_SD_SHIPMENT_HEAD.Document_Code in (select MIN(Document_Code ) from TSPL_SD_SHIPMENT_HEAD where 1=1  " & whrCls & ")"
                Case NavigatorType.Last
                    qst += " and TSPL_SD_SHIPMENT_HEAD.Document_Code in (select Max(Document_Code ) from TSPL_SD_SHIPMENT_HEAD where 1=1  " & whrCls & ")"
                Case NavigatorType.Previous
                    qst += " and TSPL_SD_SHIPMENT_HEAD.Document_Code in (select Max(Document_Code ) from TSPL_SD_SHIPMENT_HEAD where Document_Code  <'" + strCode + "'   " & whrCls & ")"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Dispatch_No = clsCommon.myCstr(dt.Rows(0)("Dispatch_No"))
                obj.Dispatch_Date = clsCommon.myCDate(dt.Rows(0)("Dispatch_Date"))
                obj.Customer_Code = clsCommon.myCstr(dt.Rows(0)("Customer_Code"))
                obj.Customer_Name = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
                obj.Vehicle_Id = clsCommon.myCstr(dt.Rows(0)("Vehicle_Code"))
                obj.Vehicle_Mannual_No = clsCommon.myCstr(dt.Rows(0)("VehicleNo"))
                obj.From_Location = clsCommon.myCstr(dt.Rows(0)("Bill_To_Location"))
                obj.From_Location_Desc = clsCommon.myCstr(dt.Rows(0)("Bill_To_Location_Desc"))
                obj.To_Location = clsCommon.myCstr(dt.Rows(0)("Ship_To_Location"))
                obj.To_Location_Desc = clsCommon.myCstr(dt.Rows(0)("Ship_To_Location_Desc"))
                obj.Transport_Id = clsCommon.myCstr(dt.Rows(0)("Transport_Id"))
                obj.Transporter_Name = clsCommon.myCstr(dt.Rows(0)("Transporter_Name"))
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function

    Public Shared Function saveData(ByVal obj As clsProductDispatchGateOut, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim issaved As Boolean = True

        Try


            Dim coll As New Hashtable()
            'trans = clsDBFuncationality.GetTransactin()
            'obj.Document_No = "DOC0001"
            If clsCommon.myLen(obj.Document_No) < 1 Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"), clsDocType.ProductDispatchGateOut, "", "")
                If (clsCommon.myLen(obj.Document_No) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
            End If
            clsCommon.AddColumnsForChange(coll, "Document_No", clsCommon.myCstr(obj.Document_No))
            clsCommon.AddColumnsForChange(coll, "Gate_Out_Date", clsCommon.GetPrintDate(obj.Gate_Out_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Dispatch_No", clsCommon.myCstr(obj.Dispatch_No))
            clsCommon.AddColumnsForChange(coll, "Dispatch_Date", clsCommon.GetPrintDate(obj.Dispatch_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Cust_Code", clsCommon.myCstr(obj.Customer_Code))
            clsCommon.AddColumnsForChange(coll, "Vehicle_Mannual_No", clsCommon.myCstr(obj.Vehicle_Mannual_No))
            clsCommon.AddColumnsForChange(coll, "From_Location", clsCommon.myCstr(obj.From_Location))
            clsCommon.AddColumnsForChange(coll, "To_Location", clsCommon.myCstr(obj.To_Location))
            clsCommon.AddColumnsForChange(coll, "Transport_Id", clsCommon.myCstr(obj.Transport_Id))
            clsCommon.AddColumnsForChange(coll, "Modify_By", clsCommon.myCstr(obj.Modify_By))
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(obj.Modify_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", clsCommon.myCstr(obj.Comp_Code))
            If obj.isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", clsCommon.myCstr(obj.Created_By))
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(obj.Created_Date, "dd/MMM/yyyy"))
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Product_Dispatch_Gate_Out", OMInsertOrUpdate.Insert, "", trans)
            Else
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Product_Dispatch_Gate_Out", OMInsertOrUpdate.Update, "TSPL_Product_Dispatch_Gate_Out.Document_No='" + obj.Document_No + "'", trans)
            End If
            'trans.Commit()
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return issaved
    End Function


End Class
