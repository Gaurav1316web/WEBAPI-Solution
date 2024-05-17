Imports System.Data.SqlClient

Public Class clsDBTCaping
#Region "variables"
    Public Document_Code As String = Nothing
    Public Document_Date As Date
    Public Reco_Code As String = Nothing
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Posting_Date As Date? = Nothing
    Public arr As List(Of clsDBTCapingDetail) = Nothing

#End Region

    Public Shared Function SaveData(ByVal obj As clsDBTCaping, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = ""
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.DBTCappingCheck, "", obj.Document_Date, trans)
            qry = "delete from TSPL_DBT_CAPING_DETAIL where Document_Code='" & obj.Document_Code & "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "Reco_Code", obj.Reco_Code)
            If isNewEntry Then
                obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.DBTCaping, "", "")
                clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DBT_CAPING", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DBT_CAPING", OMInsertOrUpdate.Update, "TSPL_DBT_CAPING.Document_Code='" + obj.Document_Code + "'", trans)
            End If
            clsDBTCapingDetail.saveData(obj.Document_Code, obj.arr, trans)

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_Code, "TSPL_DBT_CAPING", "Document_Code", "TSPL_DBT_CAPING_DETAIL", "Document_Code", "", "", trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsDBTCaping
        Dim obj As clsDBTCaping = Nothing
        Dim Arr As List(Of clsDBTCaping) = Nothing
        Dim qry As String = "Select TSPL_DBT_CAPING.* from TSPL_DBT_CAPING  where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_DBT_CAPING.Document_Code = (select MIN(Document_Code) from TSPL_DBT_CAPING WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_DBT_CAPING.Document_Code = (select Max(Document_Code) from TSPL_DBT_CAPING WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_DBT_CAPING.Document_Code = '" + strCode + "' " + whrclas + " "
            Case NavigatorType.Next
                qry += " and TSPL_DBT_CAPING.Document_Code = (select Min(Document_Code) from TSPL_DBT_CAPING where Document_Code>'" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_DBT_CAPING.Document_Code = (select Max(Document_Code) from TSPL_DBT_CAPING where Document_Code<'" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsDBTCaping()
            obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Reco_Code = clsCommon.myCstr(dt.Rows(0)("Reco_Code"))
            obj.Status = IIf(clsCommon.myCDecimal(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            If dt.Rows(0)("Posting_Date") IsNot DBNull.Value Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            End If
            'obj.dt = clsDBTCapingDetail.getData(obj.Document_Code, SelectedZone, trans)
        End If
        Return obj
    End Function
    Public Shared Function DeleteData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function DeleteData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Try

            Dim Location_code As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 MCC_Code from TSPL_DBT_CAPING_DETAIL where Document_Code='" + strDocNo + "'", trans))

            If (clsCommon.myLen(Location_code) <= 0) Then
                Location_code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 MCC_Code from TSPL_DCS_MP_INCENTIVE_RECO_DETAIL_INVALID where Document_Code='" + strDocNo + "'", trans))
            End If
            Dim Document_Date As DateTime = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select top 1 Document_Date from TSPL_DBT_CAPING where Document_Code='" + strDocNo + "'", trans))
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.DCSMPIncentiveReco, Location_code, Document_Date, trans)


            Dim qry As String = ""
            qry = "delete from TSPL_ATTACHMENTS where FormId='" + clsUserMgtCode.DCSMPIncentiveReco + "' and TransactionId='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_DBT_CAPING_DETAIL where Document_Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_DBT_CAPING where Document_Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "  Select TSPL_DBT_CAPING.Document_Code as Code,Convert(varchar,TSPL_DBT_CAPING.Document_Date,103) as Date
            Convert(varchar,TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Reco_Date,103) as [From Date],Convert(varchar,TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Reco_Date_To,103) as [To Date],Reco_Code as Zone
          ,case when isnull(Status,0)=0 then 'Pending' else 'Approved' end as Status 
          from TSPL_DBT_CAPING 
left outer join TSPL_DCS_MP_INCENTIVE_RECO_HEAD on TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Document_Code=TSPL_DBT_CAPING.Reco_Code "
        str = clsCommon.ShowSelectForm("DCMPInc#F", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If

            Dim Location_code As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 MCC_Code from TSPL_DBT_CAPING_DETAIL where Document_Code='" + strDocNo + "'", trans))

            If (clsCommon.myLen(Location_code) <= 0) Then
                Location_code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 MCC_Code from TSPL_DCS_MP_INCENTIVE_RECO_DETAIL_INVALID where Document_Code='" + strDocNo + "'", trans))
            End If

            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")

            Dim obj As clsDBTCaping = clsDBTCaping.GetData(strDocNo, NavigatorType.Current, trans)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.DCSMPIncentiveReco, Location_code, obj.Document_Date, trans)


            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            Dim qry As String = "Update TSPL_DBT_CAPING set Status=1 , Posting_Date='" + strPostDate + "',Posted_By='" + objCommonVar.CurrentUserCode + "' where Document_Code='" + strDocNo + "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            ReverseAndUnpost(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Throw New Exception("Not implemented")
            'Dim obj As clsMilkCollectionMCC = clsMilkCollectionMCC.GetData(strDocNo, NavigatorType.Current, trans)
            'If (obj Is Nothing OrElse clsCommon.myLen(obj.Status) <= 0) Then
            '    clsCommon.MyMessageBoxShow("No Data found to Reverse And UnPost")
            'End If

            'If Not obj.Status = ERPTransactionStatus.Approved Then
            '    clsCommon.MyMessageBoxShow("Transaction status should be posted for reverse and unpost")
            'End If

            ''Dim qry As String = "select Document_No from TSPL_MILK_COLLECTION_DCS_MCC_DETAIL where Against_Milk_Collection_MCC_Detail in (
            ''select PK_Id from TSPL_MILK_COLLECTION_MCC_DETAIL where Document_No='" + strDocNo + "')"
            ''Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            ''If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            ''    Throw New Exception("BMC Truck Sheet Document No [" + strDocNo + "] is used in DCS Trcuk Sheet No [" + clsCommon.myCstr(dt.Rows(0)("Document_No")) + "]")
            ''End If

            'Dim coll As New Hashtable()
            'clsCommon.AddColumnsForChange(coll, "Status", 0)
            'clsCommon.AddColumnsForChange(coll, "Posted_By", Nothing, True)
            'clsCommon.AddColumnsForChange(coll, "Posting_Date", Nothing, True)
            'clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DBT_CAPING", OMInsertOrUpdate.Update, "Document_Code='" + obj.Document_No + "'", trans)


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsDBTCapingDetail
#Region "Variable"
    Public PK_Id As Integer = Nothing
    Public Document_Code As String = Nothing
    Public MP_Code As String = Nothing
    Public MP_Uploader_Code As String = Nothing ''Not a Table Column
    Public MP_Name As String = Nothing ''Not a Table Column
    Public Qty As Decimal = 0
    Public Caping_Qty As Decimal = 0
    Public Capping_Status As Integer = False
#End Region

    Public Shared Function saveData(ByVal strDocNo As String, ByVal arrObj As List(Of clsDBTCapingDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            If arrObj IsNot Nothing Then
                For Each obj As clsDBTCapingDetail In arrObj
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_Code", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "MP_Code", obj.MP_Code)
                    clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                    clsCommon.AddColumnsForChange(coll, "Caping_Qty", obj.Caping_Qty)
                    clsCommon.AddColumnsForChange(coll, "Capping_Status", obj.Capping_Status)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DBT_CAPING_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function


    '    Public Shared Function getData(ByVal strDocNo As String, ByVal SelectedZone As String, ByVal trans As SqlTransaction) As List(Of clsDBTCapingDetail)
    '        Try
    '            Dim arrObj As List(Of clsDBTCapingDetail) = Nothing
    '            Dim obj As clsDBTCapingDetail = Nothing
    '            Dim qry As String = "Select TSPL_VLC_MASTER_HEAD.Route_Code,TSPL_BULK_ROUTE_MASTER.ROUTE_NAME, TSPL_DBT_CAPING_DETAIL.*,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader
    ',TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_MCC_MASTER.MCC_NAME,TSPL_VENDOR_MASTER.Reco_Code,TSPL_ZONE_MASTER.Description as Zone_Name
    'from (select PK_Id,Document_Code,SNo,Cycle_Year,Cycle_Month,Cycle_No,MCC_Code,MP_Code,Qty,UOM,FAT,SNF,Amount,MP_Count,Caping_Qty,MP_FAT,MP_SNF,MP_Amount,Diff_Qty,Diff_FAT,Diff_SNF,Diff_Amount,1 as Reco_Staus from TSPL_DBT_CAPING_DETAIL where TSPL_DBT_CAPING_DETAIL.Document_Code='" & strDocNo & "' ) as TSPL_DBT_CAPING_DETAIL 
    'left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.MP_Code=TSPL_DBT_CAPING_DETAIL.MP_Code
    'left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code
    'left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Reco_Code=TSPL_VENDOR_MASTER.Reco_Code
    'left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
    'left outer join TSPL_BULK_ROUTE_MASTER on TSPL_BULK_ROUTE_MASTER.ROUTE_NO=TSPL_VLC_MASTER_HEAD.Route_Code where 2=2 "

    '            If clsCommon.myLen(SelectedZone) > 0 Then
    '                qry += " and TSPL_VENDOR_MASTER.Reco_Code in (" + SelectedZone + ")"
    '            End If
    '            qry += "Order by TSPL_DBT_CAPING_DETAIL.SNo"
    '            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
    '            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '                arrObj = New List(Of clsDBTCapingDetail)
    '                For i As Integer = 0 To dt.Rows.Count - 1
    '                    obj = New clsDBTCapingDetail()
    '                    obj.PK_Id = clsCommon.myCDecimal(dt.Rows(i)("PK_Id"))
    '                    obj.Document_Code = clsCommon.myCstr(dt.Rows(i)("Document_Code"))
    '                    obj.SNo = i + 1
    '                    obj.Route_Code = clsCommon.myCstr(dt.Rows(i)("Route_Code"))
    '                    obj.Route_Name = clsCommon.myCstr(dt.Rows(i)("Route_Name"))

    '                    obj.MCC_Code = clsCommon.myCstr(dt.Rows(i)("MCC_Code"))
    '                    obj.MCC_Name = clsCommon.myCstr(dt.Rows(i)("MCC_Name"))

    '                    obj.MP_Code = clsCommon.myCstr(dt.Rows(i)("MP_Code"))
    '                    obj.VLC_Uploader_Code = clsCommon.myCstr(dt.Rows(i)("VLC_Code_VLC_Uploader"))
    '                    obj.VLC_Name = clsCommon.myCstr(dt.Rows(i)("VLC_Name"))

    '                    obj.Reco_Code = clsCommon.myCstr(dt.Rows(i)("Reco_Code"))
    '                    obj.Zone_Name = clsCommon.myCstr(dt.Rows(i)("Zone_Name"))

    '                    obj.Cycle_Year = clsCommon.myCDecimal(dt.Rows(i)("Cycle_Year"))
    '                    obj.Cycle_Month = clsCommon.myCDecimal(dt.Rows(i)("Cycle_Month"))
    '                    obj.Cycle_No = clsCommon.myCDecimal(dt.Rows(i)("Cycle_No"))

    '                    obj.Qty = clsCommon.myCDecimal(dt.Rows(i)("Qty"))
    '                    obj.UOM = clsCommon.myCstr(dt.Rows(i)("UOM"))
    '                    obj.FAT = clsCommon.myCDecimal(dt.Rows(i)("FAT"))
    '                    obj.SNF = clsCommon.myCDecimal(dt.Rows(i)("SNF"))
    '                    obj.Amount = clsCommon.myCDecimal(dt.Rows(i)("Amount"))

    '                    obj.MP_Count = clsCommon.myCDecimal(dt.Rows(i)("MP_Count"))
    '                    obj.Caping_Qty = clsCommon.myCDecimal(dt.Rows(i)("Caping_Qty"))
    '                    obj.MP_FAT = clsCommon.myCDecimal(dt.Rows(i)("MP_FAT"))
    '                    obj.MP_SNF = clsCommon.myCDecimal(dt.Rows(i)("MP_SNF"))
    '                    obj.MP_Amount = clsCommon.myCDecimal(dt.Rows(i)("MP_Amount"))

    '                    obj.Diff_Qty = clsCommon.myCDecimal(dt.Rows(i)("Diff_Qty"))
    '                    obj.Diff_FAT = clsCommon.myCDecimal(dt.Rows(i)("Diff_FAT"))
    '                    obj.Diff_SNF = clsCommon.myCDecimal(dt.Rows(i)("Diff_SNF"))
    '                    obj.Diff_Amount = clsCommon.myCDecimal(dt.Rows(i)("Diff_Amount"))

    '                    obj.Reco_Staus = (clsCommon.myCDecimal(dt.Rows(i)("Reco_Staus")) = 1)
    '                    arrObj.Add(obj)
    '                Next
    '            End If
    '            Return arrObj
    '        Catch ex As Exception
    '            Throw New Exception(ex.Message)
    '        End Try
    '    End Function
End Class
