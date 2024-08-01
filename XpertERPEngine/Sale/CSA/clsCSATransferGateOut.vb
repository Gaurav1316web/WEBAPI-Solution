Imports common
Imports System.Data.SqlClient
Public Class clsCSATransferGateOut
#Region "Variable"

    Public Document_No As String = Nothing
    Public Gate_Out_Date As Date = Nothing
    Public CSATransfer_No As String = Nothing
    Public CSATransfer_Date As Date = Nothing
    Public Cust_Code As String = Nothing
    Public Customer_Name As String = Nothing
    Public Vehicle_Mannual_No As String = Nothing
    Public Vehicle_Id As String = Nothing          ' Not Table Column 
    Public From_Location As String = Nothing
    Public From_Location_Desc As String = Nothing  ' Not Table Column 
    Public To_Location As String = Nothing
    Public To_Location_Desc As String = Nothing    ' Not Table Column 
    Public Transport_Id As String = Nothing
    Public Transporter_Name As String = Nothing    ' not Table Column
    Public Created_By As String = Nothing
    Public Created_Date As Date = Nothing
    Public Modify_By As String = Nothing
    Public Modify_Date As Date = Nothing
    Public Comp_Code As String = Nothing
    Public isNewEntry As Boolean = False

#End Region

    Public Shared Function deleteData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleCSASale, clsUserMgtCode.FrmCSATransferGateOut, clsCommon.myCstr(dt.Rows(0)("From_Location")), clsCommon.myCDate(dt.Rows(0)("CSATransfer_Date")), trans)

            Dim qry As String = "delete from TSPL_CSATransfer_Gate_Out where Document_No='" & strDocNo & "'"
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
            Dim qry As String = "  select TSPL_CSATransfer_Gate_Out.Document_No,TSPL_CSATransfer_Gate_Out.Gate_Out_Date,TSPL_CSATransfer_Gate_Out.CSATransfer_No,TSPL_CSATransfer_Gate_Out.CSATransfer_Date,TSPL_CSATransfer_Gate_Out.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_VEHICLE_MASTER.Vehicle_Id,TSPL_CSATransfer_Gate_Out.Vehicle_Mannual_No,TSPL_CSATransfer_Gate_Out.From_Location,TSPL_LOCATION_MASTER_1.Location_Desc as From_Location_Desc,TSPL_CSATransfer_Gate_Out.To_Location,TSPL_LOCATION_MASTER_2.Location_Desc as To_Location_Desc,TSPL_CSATransfer_Gate_Out.Transport_Id,TSPL_TRANSPORT_MASTER.Transporter_Name from TSPL_CSATransfer_Gate_Out left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_CSATransfer_Gate_Out.Cust_Code left outer join TSPL_VEHICLE_MASTER  on TSPL_VEHICLE_MASTER.Number=TSPL_CSATransfer_Gate_Out.Vehicle_Mannual_No left outer join  TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_1 on TSPL_LOCATION_MASTER_1 .Location_Code=TSPL_CSATransfer_Gate_Out.From_Location  left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_2 on TSPL_LOCATION_MASTER_2 .Location_Code=TSPL_CSATransfer_Gate_Out.To_Location  left outer join TSPL_TRANSPORT_MASTER on  TSPL_TRANSPORT_MASTER.Transport_Id =TSPL_CSATransfer_Gate_Out.Transport_Id  "
            str = customFinder.getFinder("DOCCSATRNGATOUT", qry, "", curcode, "", "Document_No")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str
    End Function

    Public Shared Function getGateOutData(ByVal strCode As String, ByVal navtype As NavigatorType, Optional trans As SqlTransaction = Nothing) As clsCSATransferGateOut
        Dim obj As New clsCSATransferGateOut
        Try
            Dim whrCls As String = String.Empty
            Dim qst As String = " select TSPL_CSATransfer_Gate_Out.Document_No,TSPL_CSATransfer_Gate_Out.Gate_Out_Date,TSPL_CSATransfer_Gate_Out.CSATransfer_No,TSPL_CSATransfer_Gate_Out.CSATransfer_Date,TSPL_CSATransfer_Gate_Out.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_VEHICLE_MASTER.Vehicle_Id,TSPL_CSATransfer_Gate_Out.Vehicle_Mannual_No,TSPL_CSATransfer_Gate_Out.From_Location,TSPL_LOCATION_MASTER_1.Location_Desc as From_Location_Desc,TSPL_CSATransfer_Gate_Out.To_Location,TSPL_LOCATION_MASTER_2.Location_Desc as To_Location_Desc,TSPL_CSATransfer_Gate_Out.Transport_Id,TSPL_TRANSPORT_MASTER.Transporter_Name from TSPL_CSATransfer_Gate_Out left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_CSATransfer_Gate_Out.Cust_Code left outer join TSPL_VEHICLE_MASTER  on TSPL_VEHICLE_MASTER.Number=TSPL_CSATransfer_Gate_Out.Vehicle_Mannual_No left outer join  TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_1 on TSPL_LOCATION_MASTER_1 .Location_Code=TSPL_CSATransfer_Gate_Out.From_Location  left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_2 on TSPL_LOCATION_MASTER_2 .Location_Code=TSPL_CSATransfer_Gate_Out.To_Location  left outer join TSPL_TRANSPORT_MASTER on  TSPL_TRANSPORT_MASTER.Transport_Id =TSPL_CSATransfer_Gate_Out.Transport_Id where 1=1  and TSPL_CSATransfer_Gate_Out.comp_code='" & objCommonVar.CurrentCompanyCode & "' "

            qst = qst & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and TSPL_CSATransfer_Gate_Out.Document_No in ('" + strCode + "') "
                Case NavigatorType.Next
                    qst += " and TSPL_CSATransfer_Gate_Out.Document_No in (select min(Document_No ) from TSPL_CSATransfer_Gate_Out where Document_No  >'" + strCode + "'   " & whrCls & ")"
                Case NavigatorType.First
                    qst += " and TSPL_CSATransfer_Gate_Out.Document_No in (select MIN(Document_No ) from TSPL_CSATransfer_Gate_Out where 1=1  " & whrCls & ")"
                Case NavigatorType.Last
                    qst += " and TSPL_CSATransfer_Gate_Out.Document_No in (select Max(Document_No ) from TSPL_CSATransfer_Gate_Out where 1=1  " & whrCls & ")"
                Case NavigatorType.Previous
                    qst += " and TSPL_CSATransfer_Gate_Out.Document_No in (select Max(Document_No ) from TSPL_CSATransfer_Gate_Out where Document_No  <'" + strCode + "'   " & whrCls & ")"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
                obj.Gate_Out_Date = clsCommon.myCDate(dt.Rows(0)("Gate_Out_Date"))
                obj.CSATransfer_No = clsCommon.myCstr(dt.Rows(0)("CSATransfer_No"))
                obj.CSATransfer_Date = clsCommon.myCDate(dt.Rows(0)("CSATransfer_Date"))
                obj.Cust_Code = clsCommon.myCstr(dt.Rows(0)("Cust_Code"))
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

    Public Shared Function getCSATransferFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try
            Dim qry As String = " Select TSPL_CSA_TRANSFER_HEAD.DOC_CODE as CSATransfer_No ,Convert (varchar ,TSPL_CSA_TRANSFER_HEAD.Transfer_Date,103) as CSATransfer_Date ,TSPL_CSA_TRANSFER_HEAD.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_CSA_TRANSFER_HEAD.From_Location_Code,TSPL_LOCATION_MASTER1.Location_Desc as From_Location_Desc,TSPL_CSA_TRANSFER_HEAD.To_Location_Code,TSPL_LOCATION_MASTER2.Location_Desc as To_Location_Desc ,TSPL_CSA_TRANSFER_HEAD.Transport_Id,TSPL_CSA_TRANSFER_HEAD.Transporter_Name_Manual,TSPL_CSA_TRANSFER_HEAD.Vehicle_Id as Vehicle_Mannual_No,TSPL_VEHICLE_MASTER.Vehicle_Id from TSPL_CSA_TRANSFER_HEAD left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_CSA_TRANSFER_HEAD.Cust_Code left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER1 on TSPL_LOCATION_MASTER1.Location_Code=TSPL_CSA_TRANSFER_HEAD.From_Location_Code left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER2 on TSPL_LOCATION_MASTER2.Location_Code=TSPL_CSA_TRANSFER_HEAD.To_Location_Code left outer join TSPL_VEHICLE_MASTER  on TSPL_VEHICLE_MASTER.Number=TSPL_CSA_TRANSFER_HEAD.Vehicle_Id "
            str = customFinder.getFinder("CSATRNGATOUT", qry, " TSPL_CSA_TRANSFER_HEAD.Status=1 and TSPL_CSA_TRANSFER_HEAD.DOC_CODE not in (select TSPL_CSATransfer_Gate_Out.CSATransfer_No from TSPL_CSATransfer_Gate_Out) ", curcode, "", "CSATransfer_No")

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str
    End Function

    Public Shared Function getCSATransferData(ByVal strCode As String, ByVal navtype As NavigatorType, Optional trans As SqlTransaction = Nothing) As clsCSATransferGateOut
        Dim obj As New clsCSATransferGateOut
        Try
            Dim whrCls As String = String.Empty
            Dim qst As String = "  Select TSPL_CSA_TRANSFER_HEAD.DOC_CODE as CSATransfer_No ,Convert (varchar ,TSPL_CSA_TRANSFER_HEAD.Transfer_Date,103) as CSATransfer_Date ,TSPL_CSA_TRANSFER_HEAD.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_CSA_TRANSFER_HEAD.From_Location_Code,TSPL_LOCATION_MASTER1.Location_Desc as From_Location_Desc,TSPL_CSA_TRANSFER_HEAD.To_Location_Code,TSPL_LOCATION_MASTER2.Location_Desc as To_Location_Desc ,TSPL_CSA_TRANSFER_HEAD.Transport_Id,TSPL_CSA_TRANSFER_HEAD.Transporter_Name_Manual,TSPL_CSA_TRANSFER_HEAD.Vehicle_Id as Vehicle_Mannual_No,TSPL_VEHICLE_MASTER.Vehicle_Id from TSPL_CSA_TRANSFER_HEAD left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_CSA_TRANSFER_HEAD.Cust_Code left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER1 on TSPL_LOCATION_MASTER1.Location_Code=TSPL_CSA_TRANSFER_HEAD.From_Location_Code left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER2 on TSPL_LOCATION_MASTER2.Location_Code=TSPL_CSA_TRANSFER_HEAD.To_Location_Code left outer join TSPL_VEHICLE_MASTER  on TSPL_VEHICLE_MASTER.Number=TSPL_CSA_TRANSFER_HEAD.Vehicle_Id  where 1=1 and TSPL_CSA_TRANSFER_HEAD.Status=1 and TSPL_CSA_TRANSFER_HEAD.DOC_CODE not in (select TSPL_CSATransfer_Gate_Out.CSATransfer_No from TSPL_CSATransfer_Gate_Out)  and TSPL_CSA_TRANSFER_HEAD.comp_code='" & objCommonVar.CurrentCompanyCode & "' "

            qst = qst & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and TSPL_CSA_TRANSFER_HEAD.DOC_CODE in ('" + strCode + "') "
                Case NavigatorType.Next
                    qst += " and TSPL_CSA_TRANSFER_HEAD.DOC_CODE in (select min(Document_No ) from TSPL_CSA_TRANSFER_HEAD where DOC_CODE  >'" + strCode + "'   " & whrCls & ")"
                Case NavigatorType.First
                    qst += " and TSPL_CSA_TRANSFER_HEAD.DOC_CODE in (select MIN(Document_No ) from TSPL_CSA_TRANSFER_HEAD where 1=1  " & whrCls & ")"
                Case NavigatorType.Last
                    qst += " and TSPL_CSA_TRANSFER_HEAD.DOC_CODE in (select Max(Document_No ) from TSPL_CSA_TRANSFER_HEAD where 1=1  " & whrCls & ")"
                Case NavigatorType.Previous
                    qst += " and TSPL_CSA_TRANSFER_HEAD.DOC_CODE in (select Max(Document_No ) from TSPL_CSA_TRANSFER_HEAD where DOC_CODE  <'" + strCode + "'   " & whrCls & ")"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.CSATransfer_No = clsCommon.myCstr(dt.Rows(0)("CSATransfer_No"))
                obj.CSATransfer_Date = clsCommon.myCDate(dt.Rows(0)("CSATransfer_Date"))
                obj.Cust_Code = clsCommon.myCstr(dt.Rows(0)("Cust_Code"))
                obj.Customer_Name = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
                obj.Vehicle_Id = clsCommon.myCstr(dt.Rows(0)("Vehicle_Id"))
                obj.Vehicle_Mannual_No = clsCommon.myCstr(dt.Rows(0)("Vehicle_Mannual_No"))
                obj.From_Location = clsCommon.myCstr(dt.Rows(0)("From_Location_Code"))
                obj.From_Location_Desc = clsCommon.myCstr(dt.Rows(0)("From_Location_Desc"))
                obj.To_Location = clsCommon.myCstr(dt.Rows(0)("To_Location_Code"))
                obj.To_Location_Desc = clsCommon.myCstr(dt.Rows(0)("To_Location_Desc"))
                obj.Transport_Id = clsCommon.myCstr(dt.Rows(0)("Transport_Id"))
                obj.Transporter_Name = clsCommon.myCstr(dt.Rows(0)("Transporter_Name_Manual"))
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function

    Public Shared Function saveData(ByVal obj As clsCSATransferGateOut, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim issaved As Boolean = True

        Try

            'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleCSASale, clsUserMgtCode.FrmCSATransferGateOut, clsCommon.myCstr(dt.Rows(0)("From_Location")), clsCommon.myCDate(dt.Rows(0)("CSATransfer_Date")), trans)

            Dim coll As New Hashtable()
            'trans = clsDBFuncationality.GetTransactin()
            'obj.Document_No = "DOC0001"
            If clsCommon.myLen(obj.Document_No) < 1 Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"), clsDocType.CSATransferGateOut, "", "")
                If (clsCommon.myLen(obj.Document_No) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
            End If
            clsCommon.AddColumnsForChange(coll, "Document_No", clsCommon.myCstr(obj.Document_No))
            clsCommon.AddColumnsForChange(coll, "Gate_Out_Date", clsCommon.GetPrintDate(obj.Gate_Out_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "CSATransfer_No", clsCommon.myCstr(obj.CSATransfer_No))
            clsCommon.AddColumnsForChange(coll, "CSATransfer_Date", clsCommon.GetPrintDate(obj.CSATransfer_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Cust_Code", clsCommon.myCstr(obj.Cust_Code))
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
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CSATransfer_Gate_Out", OMInsertOrUpdate.Insert, "", trans)
            Else
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CSATransfer_Gate_Out", OMInsertOrUpdate.Update, "TSPL_CSATransfer_Gate_Out.Document_No='" + obj.Document_No + "'", trans)
            End If
            'trans.Commit()
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return issaved
    End Function


End Class
