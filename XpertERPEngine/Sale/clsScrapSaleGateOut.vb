Imports common
Imports System.Data.SqlClient
Public Class clsScrapSaleGateOut
#Region "Variable"

    Public Document_No As String = Nothing
    Public Gate_Out_Date As Date = Nothing
    Public Shipment_No As String = Nothing
    Public Shipment_Date As Date = Nothing
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
            Dim qry As String = "delete from TSPL_SCRAPSALE_GATE_OUT where Document_No='" & strDocNo & "'"
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
            Dim qry As String = " select TSPL_SCRAPSALE_GATE_OUT.Document_No,convert (varchar, TSPL_SCRAPSALE_GATE_OUT.Gate_Out_Date,103) as Gate_Out_Date ,TSPL_SCRAPSALE_GATE_OUT.Shipment_No,convert(varchar, TSPL_SCRAPSALE_GATE_OUT.Shipment_Date,103) as Shipment_Date ,TSPL_SCRAPSALE_GATE_OUT.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_VEHICLE_MASTER.Vehicle_Id,TSPL_SCRAPSALE_GATE_OUT.Vehicle_Mannual_No,TSPL_SCRAPSALE_GATE_OUT.From_Location,TSPL_LOCATION_MASTER1.Location_Desc as From_Location_Desc,TSPL_SCRAPSALE_GATE_OUT.To_Location,TSPL_LOCATION_MASTER2.Location_Desc as To_Location_Desc,TSPL_SCRAPSALE_GATE_OUT.Transport_Id , TSPL_TRANSPORT_MASTER.Transporter_Name from TSPL_SCRAPSALE_GATE_OUT left outer join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Number =TSPL_SCRAPSALE_GATE_OUT.Vehicle_Mannual_No left outer join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_SCRAPSALE_GATE_OUT.Transport_Id left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER1 on TSPL_LOCATION_MASTER1.Location_Code=TSPL_SCRAPSALE_GATE_OUT.From_Location left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER2 on TSPL_LOCATION_MASTER2.Location_Code=TSPL_SCRAPSALE_GATE_OUT.To_Location left Outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SCRAPSALE_GATE_OUT.Cust_Code  "
            str = customFinder.getFinder("DOCSCRAPGATOUT", qry, "", curcode, "", "Document_No")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str
    End Function

    Public Shared Function getGateOutData(ByVal strCode As String, ByVal navtype As NavigatorType, Optional trans As SqlTransaction = Nothing) As clsScrapSaleGateOut
        Dim obj As New clsScrapSaleGateOut
        Try
            Dim whrCls As String = String.Empty
            Dim qst As String = " select TSPL_SCRAPSALE_GATE_OUT.Document_No,convert(varchar, TSPL_SCRAPSALE_GATE_OUT.Gate_Out_Date,103) as Gate_Out_Date ,TSPL_SCRAPSALE_GATE_OUT.Shipment_No,convert(varchar, TSPL_SCRAPSALE_GATE_OUT.Shipment_Date,103) as Shipment_Date ,TSPL_SCRAPSALE_GATE_OUT.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_VEHICLE_MASTER.Vehicle_Id,TSPL_SCRAPSALE_GATE_OUT.Vehicle_Mannual_No,TSPL_SCRAPSALE_GATE_OUT.From_Location,TSPL_LOCATION_MASTER1.Location_Desc as From_Location_Desc,TSPL_SCRAPSALE_GATE_OUT.To_Location,TSPL_LOCATION_MASTER2.Location_Desc as To_Location_Desc,TSPL_SCRAPSALE_GATE_OUT.Transport_Id , TSPL_TRANSPORT_MASTER.Transporter_Name from TSPL_SCRAPSALE_GATE_OUT left outer join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Number =TSPL_SCRAPSALE_GATE_OUT.Vehicle_Mannual_No left outer join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_SCRAPSALE_GATE_OUT.Transport_Id left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER1 on TSPL_LOCATION_MASTER1.Location_Code=TSPL_SCRAPSALE_GATE_OUT.From_Location left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER2 on TSPL_LOCATION_MASTER2.Location_Code=TSPL_SCRAPSALE_GATE_OUT.To_Location left Outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SCRAPSALE_GATE_OUT.Cust_Code where 1=1  and TSPL_SCRAPSALE_GATE_OUT.comp_code='" & objCommonVar.CurrentCompanyCode & "' "

            qst = qst & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and TSPL_SCRAPSALE_GATE_OUT.Document_No in ('" + strCode + "') "
                Case NavigatorType.Next
                    qst += " and TSPL_SCRAPSALE_GATE_OUT.Document_No in (select min(Document_No ) from TSPL_SCRAPSALE_GATE_OUT where Document_No  >'" + strCode + "'   " & whrCls & ")"
                Case NavigatorType.First
                    qst += " and TSPL_SCRAPSALE_GATE_OUT.Document_No in (select MIN(Document_No ) from TSPL_SCRAPSALE_GATE_OUT where 1=1  " & whrCls & ")"
                Case NavigatorType.Last
                    qst += " and TSPL_SCRAPSALE_GATE_OUT.Document_No in (select Max(Document_No ) from TSPL_SCRAPSALE_GATE_OUT where 1=1  " & whrCls & ")"
                Case NavigatorType.Previous
                    qst += " and TSPL_SCRAPSALE_GATE_OUT.Document_No in (select Max(Document_No ) from TSPL_SCRAPSALE_GATE_OUT where Document_No  <'" + strCode + "'   " & whrCls & ")"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
                obj.Gate_Out_Date = clsCommon.myCDate(dt.Rows(0)("Gate_Out_Date"))
                obj.Shipment_No = clsCommon.myCstr(dt.Rows(0)("Shipment_No"))
                obj.Shipment_Date = clsCommon.myCDate(dt.Rows(0)("Shipment_Date"))
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

    Public Shared Function getShipmentFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try
            Dim qry As String = " select TSPL_SCRAPSALE_HEAD.shipment_No,convert(varchar, TSPL_SCRAPSALE_HEAD.shipment_Date,103) as shipment_Date ,TSPL_SCRAPSALE_HEAD.cust_Code,TSPL_SCRAPSALE_HEAD.cust_Name,TSPL_SCRAPSALE_HEAD.Loc_Code ,TSPL_SCRAPSALE_HEAD. Loc_Name,TSPL_SCRAPSALE_HEAD.ToLoc_Code,TSPL_LOCATION_MASTER.Location_Desc as ToLoc_Name,TSPL_VEHICLE_MASTER.Vehicle_Id,TSPL_SCRAPSALE_HEAD.Vehicle_code,TSPL_SCRAPSALE_HEAD.Transport_code,TSPL_TRANSPORT_MASTER.Transporter_Name from TSPL_SCRAPSALE_HEAD left outer join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Number =TSPL_SCRAPSALE_HEAD.Vehicle_code left outer join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_SCRAPSALE_HEAD.Transport_code left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SCRAPSALE_HEAD.ToLoc_Code "
            str = customFinder.getFinder("SHIPGATOUT", qry, " TSPL_SCRAPSALE_HEAD.ispost=1 and TSPL_SCRAPSALE_HEAD.shipment_No not in (select TSPL_SCRAPSALE_GATE_OUT.Shipment_No from TSPL_SCRAPSALE_GATE_OUT) ", curcode, "", "shipment_No")

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str
    End Function

    Public Shared Function getShipmentData(ByVal strCode As String, ByVal navtype As NavigatorType, Optional trans As SqlTransaction = Nothing) As clsScrapSaleGateOut
        Dim obj As New clsScrapSaleGateOut
        Try
            Dim whrCls As String = String.Empty
            Dim qst As String = "  select TSPL_SCRAPSALE_HEAD.shipment_No,convert(varchar, TSPL_SCRAPSALE_HEAD.shipment_Date,103) as shipment_Date ,TSPL_SCRAPSALE_HEAD.cust_Code,TSPL_SCRAPSALE_HEAD.cust_Name,TSPL_SCRAPSALE_HEAD.Loc_Code ,TSPL_SCRAPSALE_HEAD. Loc_Name,TSPL_SCRAPSALE_HEAD.ToLoc_Code,TSPL_LOCATION_MASTER.Location_Desc as ToLoc_Name,TSPL_VEHICLE_MASTER.Vehicle_Id,TSPL_SCRAPSALE_HEAD.Vehicle_code,TSPL_SCRAPSALE_HEAD.Transport_code,TSPL_TRANSPORT_MASTER.Transporter_Name from TSPL_SCRAPSALE_HEAD left outer join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Number =TSPL_SCRAPSALE_HEAD.Vehicle_code left outer join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_SCRAPSALE_HEAD.Transport_code left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SCRAPSALE_HEAD.ToLoc_Code  where 1=1 and TSPL_SCRAPSALE_HEAD.ispost=1 and TSPL_SCRAPSALE_HEAD.shipment_No not in (select TSPL_SCRAPSALE_GATE_OUT.Shipment_No from TSPL_SCRAPSALE_GATE_OUT)  and TSPL_SCRAPSALE_HEAD.comp_code='" & objCommonVar.CurrentCompanyCode & "' "

            qst = qst & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and TSPL_SCRAPSALE_HEAD.shipment_No in ('" + strCode + "') "
                Case NavigatorType.Next
                    qst += " and TSPL_SCRAPSALE_HEAD.shipment_No in (select min(shipment_No ) from TSPL_SCRAPSALE_HEAD where shipment_No  >'" + strCode + "'   " & whrCls & ")"
                Case NavigatorType.First
                    qst += " and TSPL_SCRAPSALE_HEAD.shipment_No in (select MIN(shipment_No ) from TSPL_SCRAPSALE_HEAD where 1=1  " & whrCls & ")"
                Case NavigatorType.Last
                    qst += " and TSPL_SCRAPSALE_HEAD.shipment_No in (select Max(shipment_No ) from TSPL_SCRAPSALE_HEAD where 1=1  " & whrCls & ")"
                Case NavigatorType.Previous
                    qst += " and TSPL_SCRAPSALE_HEAD.shipment_No in (select Max(shipment_No ) from TSPL_SCRAPSALE_HEAD where shipment_No  <'" + strCode + "'   " & whrCls & ")"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Shipment_No = clsCommon.myCstr(dt.Rows(0)("shipment_No"))
                obj.Shipment_Date = clsCommon.myCDate(dt.Rows(0)("shipment_Date"))
                obj.Customer_Code = clsCommon.myCstr(dt.Rows(0)("Cust_Code"))
                obj.Customer_Name = clsCommon.myCstr(dt.Rows(0)("Cust_Name"))
                obj.Vehicle_Id = clsCommon.myCstr(dt.Rows(0)("Vehicle_Id"))
                obj.Vehicle_Mannual_No = clsCommon.myCstr(dt.Rows(0)("Vehicle_code"))
                obj.From_Location = clsCommon.myCstr(dt.Rows(0)("Loc_Code"))
                obj.From_Location_Desc = clsCommon.myCstr(dt.Rows(0)("Loc_Name"))
                obj.To_Location = clsCommon.myCstr(dt.Rows(0)("ToLoc_Code"))
                obj.To_Location_Desc = clsCommon.myCstr(dt.Rows(0)("ToLoc_Name"))
                obj.Transport_Id = clsCommon.myCstr(dt.Rows(0)("Transport_code"))
                obj.Transporter_Name = clsCommon.myCstr(dt.Rows(0)("Transporter_Name"))
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function

    Public Shared Function saveData(ByVal obj As clsScrapSaleGateOut, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim issaved As Boolean = True

        Try


            Dim coll As New Hashtable()
            'trans = clsDBFuncationality.GetTransactin()
            'obj.Document_No = "DOC0001"
            If clsCommon.myLen(obj.Document_No) < 1 Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"), clsDocType.ScrapSaleGateOut, "", "")
                If (clsCommon.myLen(obj.Document_No) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
            End If
            clsCommon.AddColumnsForChange(coll, "Document_No", clsCommon.myCstr(obj.Document_No))
            clsCommon.AddColumnsForChange(coll, "Gate_Out_Date", clsCommon.GetPrintDate(obj.Gate_Out_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Shipment_No", clsCommon.myCstr(obj.Shipment_No))
            clsCommon.AddColumnsForChange(coll, "Shipment_Date", clsCommon.GetPrintDate(obj.Shipment_Date, "dd/MMM/yyyy"))
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
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCRAPSALE_GATE_OUT", OMInsertOrUpdate.Insert, "", trans)
            Else
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCRAPSALE_GATE_OUT", OMInsertOrUpdate.Update, "TSPL_SCRAPSALE_GATE_OUT.Document_No='" + obj.Document_No + "'", trans)
            End If
            'trans.Commit()
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return issaved
    End Function

    Public Shared Function isGateOutDone(ByVal strVehicleCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = String.Empty
        qry = "select count(*) from TSPL_SCRAPSALE_GATE_OUT where Shipment_No = ( select Top 1 shipment_No from  TSPL_SCRAPSALE_HEAD where Vehicle_code='" & strVehicleCode & "' order by shipment_Date desc)"
        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) <= 0 Then
            Return False
        Else
            Return True
        End If
    End Function


End Class
