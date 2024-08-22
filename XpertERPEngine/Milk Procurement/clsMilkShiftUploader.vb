Imports common
Imports System.Data.SqlClient

Public Class clsMilkShiftUploaderHead
#Region "Variables"
    Public Document_No As String
    Public Shift_Date As Date
    Public Shift As String
    Public MCC_Code As String
    Public MCC_Name As String
    Public Dock_Code As String
    Public Dock_Name As String
    Public Description As String
    Public Mix_Milk As Boolean
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Posting_Date As DateTime? = Nothing
    Public Arr As List(Of clsMilkShiftUploaderDetail) = Nothing
    Public dtRaj As DataTable
    Public dtRajPage As DataTable
    Public Raj_Bulk_Route_Code As String
    Public Raj_Bulk_Route_Name As String ''Not a table Column
    Public Raj_Truck_no As String
    Public Raj_Late As Integer
    Public Raj_Entered_Qty As Decimal
    Public Raj_Entered_FATKg As Decimal
    Public Raj_Entered_SNFKg As Decimal

    Public Raj_Received_Qty As Decimal ''Not a table Column
    Public Raj_Received_FATKg As Decimal ''Not a table Column
    Public Raj_Received_SNFKg As Decimal ''Not a table Column
    Public Raj_PageNo As Integer

    Public TR_No As String

#End Region

    Public Function SaveData(ByVal obj As clsMilkShiftUploaderHead, ByVal isNewEntry As Boolean) As Boolean
        Return SaveData(obj, isNewEntry, False)
    End Function
    Public Function SaveData(ByVal obj As clsMilkShiftUploaderHead, ByVal isNewEntry As Boolean, ByVal isForRaj As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, isForRaj, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Function SaveData(ByVal obj As clsMilkShiftUploaderHead, ByVal isNewEntry As Boolean, ByVal isForRaj As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.MilkShiftUploader, obj.MCC_Code, obj.Shift_Date, trans)
            clsMCCPaymentCycleLockForScheduler.CheckForSchedulerLock(obj.MCC_Code, obj.Shift_Date, trans)
            Dim qry As String = "delete from TSPL_MILK_SHIFT_UPLOADER_QC_PARAMETER_DETAIL where Document_No='" & obj.Document_No & "'"
            If isForRaj Then
                qry += " and Sample_No='" + clsCommon.myCstr(obj.Arr(0).SNo) + "'"
            End If
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_MILK_SHIFT_UPLOADER_DETAIL where Document_No='" + obj.Document_No + "'"
            If isForRaj Then
                qry += " and SNo='" + clsCommon.myCstr(obj.Arr(0).SNo) + "'"
            End If
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If clsCommon.myLen(obj.MCC_Code) <= 0 Then
                Throw New Exception("Please first select MCC")
            Else
                qry = "select Code from TSPL_DOCK_MASTER where MCC_Code='" + obj.MCC_Code + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    If clsCommon.myLen(obj.Dock_Code) <= 0 Then
                        Throw New Exception("Please first select Dock")
                    End If
                    Dim isfound As Boolean = False
                    For Each dr As DataRow In dt.Rows
                        If clsCommon.CompairString(clsCommon.myCstr(dr("Code")), obj.Dock_Code) = CompairStringResult.Equal Then
                            isfound = True
                            Exit For
                        End If
                    Next
                    If Not isfound Then
                        Throw New Exception("Dock [" + obj.Dock_Code + "] is not for MCC [" + obj.MCC_Code + "]")
                    End If
                Else
                    If clsCommon.myLen(obj.Dock_Code) > 0 Then
                        Throw New Exception("Dock [" + obj.Dock_Code + "] is not For MCC [" + obj.MCC_Code + "]")
                    End If
                End If
            End If

            If isNewEntry Then
                obj.Document_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select max(Document_No) from TSPL_MILK_SHIFT_UPLOADER_HEAD where Document_No like '" + obj.MCC_Code + "%' ", trans))
                If clsCommon.myLen(obj.Document_No) <= 0 Then
                    obj.Document_No = obj.MCC_Code + "0000000000"
                End If
                obj.Document_No = clsCommon.incval(obj.Document_No)
            End If
            If (clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Shift_Date", clsCommon.GetPrintDate(obj.Shift_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Shift", obj.Shift)
            clsCommon.AddColumnsForChange(coll, "MCC_Code", obj.MCC_Code)
            clsCommon.AddColumnsForChange(coll, "Dock_Code", obj.Dock_Code, True)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Mix_Milk", IIf(obj.Mix_Milk, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))

            clsCommon.AddColumnsForChange(coll, "Raj_Bulk_Route_Code", obj.Raj_Bulk_Route_Code, True)
            clsCommon.AddColumnsForChange(coll, "Raj_Truck_no", obj.Raj_Truck_no)
            clsCommon.AddColumnsForChange(coll, "Raj_Late", obj.Raj_Late)
            clsCommon.AddColumnsForChange(coll, "Raj_Entered_Qty", obj.Raj_Entered_Qty)
            clsCommon.AddColumnsForChange(coll, "Raj_Entered_FATKg", obj.Raj_Entered_FATKg)
            clsCommon.AddColumnsForChange(coll, "Raj_Entered_SNFKg", obj.Raj_Entered_SNFKg)

            '' update Sync Satatus
            clsCommon.AddColumnsForChange(coll, "SYNC_STATUS", 0)


            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_SHIFT_UPLOADER_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_SHIFT_UPLOADER_HEAD", OMInsertOrUpdate.Update, "TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No='" + obj.Document_No + "'", trans)
            End If
            clsMilkShiftUploaderDetail.SaveData(obj.Document_No, obj.MCC_Code, obj.Arr, trans)

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_No, "TSPL_MILK_SHIFT_UPLOADER_HEAD", "Document_No", "TSPL_MILK_SHIFT_UPLOADER_DETAIL", "Document_No", trans)

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsMilkShiftUploaderHead
        Return GetData(strPONo, NavType, trans, False, False)
    End Function
    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction, ByVal ForRaj As Boolean, ByVal isPickCLRInsteadOfSNF As Boolean) As clsMilkShiftUploaderHead
        Dim obj As clsMilkShiftUploaderHead = Nothing
        Dim qry As String = "SELECT TSPL_MILK_SHIFT_UPLOADER_HEAD.*,TSPL_MCC_MASTER.MCC_NAME,TSPL_DOCK_MASTER.Description as Dock_Name,TSPL_BULK_ROUTE_MASTER.ROUTE_NAME as Raj_Bulk_Route_Name " &
        " FROM TSPL_MILK_SHIFT_UPLOADER_HEAD left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_SHIFT_UPLOADER_HEAD.MCC_Code left outer join TSPL_DOCK_MASTER on TSPL_DOCK_MASTER.Code=TSPL_MILK_SHIFT_UPLOADER_HEAD.Dock_Code  left outer join TSPL_BULK_ROUTE_MASTER on TSPL_BULK_ROUTE_MASTER.ROUTE_NO=TSPL_MILK_SHIFT_UPLOADER_HEAD.Raj_Bulk_Route_Code where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No = (select MIN(Document_No) from TSPL_MILK_SHIFT_UPLOADER_HEAD where 1=1  )"
            Case NavigatorType.Last
                qry += " and TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No = (select Max(Document_No) from TSPL_MILK_SHIFT_UPLOADER_HEAD where 1=1  )"
            Case NavigatorType.Next
                qry += " and TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No = (select Min(Document_No) from TSPL_MILK_SHIFT_UPLOADER_HEAD where Document_No>'" + strPONo + "'  )"
            Case NavigatorType.Previous
                qry += " and TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No = (select Max(Document_No) from TSPL_MILK_SHIFT_UPLOADER_HEAD where Document_No<'" + strPONo + "'  )"
            Case NavigatorType.Current
                qry += " and TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No = '" + strPONo + "'"
        End Select
        If clsCommon.myLen(strPONo) > 0 And NavType = 0 Then
            qry += " and TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No = '" + strPONo + "'"
        End If
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsMilkShiftUploaderHead()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Shift_Date = clsCommon.myCDate(dt.Rows(0)("Shift_Date"))
            obj.Shift = clsCommon.myCstr(dt.Rows(0)("Shift"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.MCC_Code = clsCommon.myCstr(dt.Rows(0)("MCC_Code"))
            obj.MCC_Name = clsCommon.myCstr(dt.Rows(0)("MCC_NAME"))
            obj.Dock_Code = clsCommon.myCstr(dt.Rows(0)("Dock_Code"))
            obj.Dock_Name = clsCommon.myCstr(dt.Rows(0)("Dock_Name"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 2, ERPTransactionStatus.Posted, ERPTransactionStatus.Pending))
            obj.Mix_Milk = (clsCommon.myCdbl(dt.Rows(0)("Mix_Milk")) = 1)

            obj.Raj_Bulk_Route_Code = clsCommon.myCstr(dt.Rows(0)("Raj_Bulk_Route_Code"))
            obj.Raj_Bulk_Route_Name = clsCommon.myCstr(dt.Rows(0)("Raj_Bulk_Route_Name"))
            obj.Raj_Truck_no = clsCommon.myCstr(dt.Rows(0)("Raj_Truck_no"))
            obj.Raj_Late = clsCommon.myCdbl(dt.Rows(0)("Raj_Late"))
            obj.Raj_Entered_Qty = clsCommon.myCdbl(dt.Rows(0)("Raj_Entered_Qty"))
            obj.Raj_Entered_FATKg = clsCommon.myCdbl(dt.Rows(0)("Raj_Entered_FATKg"))
            obj.Raj_Entered_SNFKg = clsCommon.myCdbl(dt.Rows(0)("Raj_Entered_SNFKg"))
            'obj.Raj_PageNo = clsCommon.myCstr(dt.Rows(0)("PageNo"))
            If ForRaj Then
                Dim DocRejectType As String = ""
                qry = "select sum(Milk_Weight) as Milk_Weight,cast(sum(Milk_Weight*FAT/100) as decimal(18,3)) as FAT,cast(sum(Milk_Weight*SNF/100) as decimal(18,3)) as SNF from TSPL_MILK_SHIFT_UPLOADER_DETAIL where Document_No='" + obj.Document_No + "'"
                obj.dtRaj = clsDBFuncationality.GetDataTable(qry, trans)
                If obj.dtRaj IsNot Nothing AndAlso obj.dtRaj.Rows.Count > 0 Then
                    obj.Raj_Received_Qty = clsCommon.myCdbl(obj.dtRaj.Rows(0)("Milk_Weight"))
                    obj.Raj_Received_FATKg = clsCommon.myCdbl(obj.dtRaj.Rows(0)("FAT"))
                    obj.Raj_Received_SNFKg = clsCommon.myCdbl(obj.dtRaj.Rows(0)("SNF"))
                End If

                qry = "select TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type,max(TSPL_MILK_REJECT_TYPE.Description) as Description,max(TSPL_MILK_REJECT_TYPE.SNo) as SNo from TSPL_MILK_SHIFT_UPLOADER_DETAIL inner join TSPL_MILK_REJECT_TYPE on TSPL_MILK_REJECT_TYPE.Code=TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type where Document_No='" + obj.Document_No + "' group by TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type   order by SNo"
                obj.dtRaj = clsDBFuncationality.GetDataTable(qry, trans)
                qry = "select TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No,TSPL_MILK_SHIFT_UPLOADER_DETAIL.SNo,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VLC, TSPL_MILK_SHIFT_UPLOADER_DETAIL.VLC_Code as [VLC Code],TSPL_VLC_MASTER_HEAD.VLC_Name as [VLC Name],TSPL_MILK_SHIFT_UPLOADER_DETAIL.No_Of_Cans as [No of Cans],TSPL_MILK_SHIFT_UPLOADER_DETAIL.BULK_ROUTE_NO as [Route Code],TSPL_BULK_ROUTE_MASTER.ROUTE_NAME as [Route]
,case When isnull(Reject_Type,'')='' then Milk_Weight else 0 end as [Good Qty]
,case When isnull(Reject_Type,'')='' then FAT else 0 end as [Good FAT %]
,case When isnull(Reject_Type,'')='' then cast(Milk_Weight*FAT/100 as decimal(18,3)) else 0 end as [Good FATKg]
                ,case When isnull(Reject_Type,'')='' then SNF else 0 end as [Good SNF %]
                ,case When isnull(Reject_Type,'')='' then cast (Milk_Weight*SNF/100 as decimal(18,3)) else 0 end as [Good SNFKG]"


                If isPickCLRInsteadOfSNF = True Then
                    qry += "  ,SNF/4+0.2*FAT+(select Description from TSPL_Fixed_parameter where type='defaultCorrectionFactor' and code='MilkSetting') as [SNF_PER]
                     ,((Milk_Weight* (SNF/4+0.2*FAT+(select Description from TSPL_Fixed_parameter where type='defaultCorrectionFactor' and code='MilkSetting')))/100) as SNF_kg,
                Convert(Decimal(18, 2), ((Milk_Weight * FAT) / 100)) As [Fat_KG] "
                    'Else
                    '    qry += " ,case When (isnull(Reject_Type,''))='CURD' then (cast (Milk_Weight*SNF/100 as decimal(18,3))) else 0 end as [CURD SNFKG] "
                End If

                If obj.dtRaj IsNot Nothing AndAlso obj.dtRaj.Rows.Count > 0 Then
                    For Each dr As DataRow In obj.dtRaj.Rows
                        qry += ",case When isnull(Reject_Type,'')='" + clsCommon.myCstr(dr("Reject_Type")) + "' then Milk_Weight else 0 end as [" + clsCommon.myCstr(dr("Reject_Type")) + " Qty]
,case When isnull(Reject_Type,'')='" + clsCommon.myCstr(dr("Reject_Type")) + "' then FAT else 0 end as [" + clsCommon.myCstr(dr("Reject_Type")) + " FAT %]
,case When isnull(Reject_Type,'')='" + clsCommon.myCstr(dr("Reject_Type")) + "' then cast (Milk_Weight*FAT/100 as decimal(18,3)) else 0 end as [" + clsCommon.myCstr(dr("Reject_Type")) + " FATKg]
,case When isnull(Reject_Type,'')='" + clsCommon.myCstr(dr("Reject_Type")) + "' then SNF else 0 end as [" + clsCommon.myCstr(dr("Reject_Type")) + " SNF %],case When isnull(Reject_Type,'')='" + clsCommon.myCstr(dr("Reject_Type")) + "' then cast (Milk_Weight*SNF/100 as decimal(18,3)) else 0 end as [" + clsCommon.myCstr(dr("Reject_Type")) + " SNFKG]"
                        ',case When isnull(Reject_Type,'')='" + clsCommon.myCstr(dr("Reject_Type")) + "' then cast (Milk_Weight*SNF/100 as decimal(18,3)) else 0 end as [" + clsCommon.myCstr(dr("Reject_Type")) + " SNFKG]"

                        DocRejectType += ",  sum ([" + clsCommon.myCstr(dr("Reject_Type")) + " Qty] ) as [" + clsCommon.myCstr(dr("Reject_Type")) + " Qty]
, isnull (convert(decimal(18,2), ( sum( [" + clsCommon.myCstr(dr("Reject_Type")) + " FATKg]) * 100/ nullif((sum([" + clsCommon.myCstr(dr("Reject_Type")) + " Qty])),0)    )),0)   as [" + clsCommon.myCstr(dr("Reject_Type")) + " FAT %]
, sum([" + clsCommon.myCstr(dr("Reject_Type")) + " FATKg]) as [" + clsCommon.myCstr(dr("Reject_Type")) + " FATKg]
                        , isnull ( convert(decimal(18,2), ( sum( [" + clsCommon.myCstr(dr("Reject_Type")) + " SNFKG]) * 100/ nullif( (sum([" + clsCommon.myCstr(dr("Reject_Type")) + " Qty])),0)    )),0)   as [" + clsCommon.myCstr(dr("Reject_Type")) + " SNF %]
, sum ([" + clsCommon.myCstr(dr("Reject_Type")) + " SNFKG]) as [" + clsCommon.myCstr(dr("Reject_Type")) + " SNFKG]"

                    Next
                End If
                qry += " ,TSPL_MILK_SHIFT_UPLOADER_DETAIL.PageNo from TSPL_MILK_SHIFT_UPLOADER_DETAIL 
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_SHIFT_UPLOADER_DETAIL.VLC_Code
left outer join TSPL_BULK_ROUTE_MASTER on TSPL_BULK_ROUTE_MASTER.ROUTE_NO=TSPL_MILK_SHIFT_UPLOADER_DETAIL.BULK_ROUTE_NO
where Document_No='" + obj.Document_No + "' "

                Dim qryPage As String = "select PageNo, sum([Good Qty]) as  [Good Qty], isnull( convert(decimal(18,2), ( sum( [Good FATKg]) * 100/ nullif((sum([Good Qty])),0)  )),0) as [Good FAT %], sum([Good FATKg]) as [Good FATKg] , 
isnull (convert(decimal(18,2), ( sum( [Good SNFKG]) * 100/ nullif((sum([Good Qty])),0)    )),0) as [Good SNF %],sum([Good SNFKG]) as [Good SNFKG] " + DocRejectType + "  from ( " + qry + " ) XXXFinal group by PageNo"

                qry += " order by SNo "
                obj.dtRaj = clsDBFuncationality.GetDataTable(qry, trans)
                obj.dtRajPage = clsDBFuncationality.GetDataTable(qryPage, trans)
            Else
                obj.Arr = clsMilkShiftUploaderDetail.GetData(obj.Document_No, "", trans)
            End If
        End If
        Return obj
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date,TSPL_MILK_SHIFT_UPLOADER_HEAD.MCC_Code from TSPL_MILK_SHIFT_UPLOADER_HEAD where Document_No='" + strCode + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.MilkShiftUploader, clsCommon.myCstr(dt.Rows(0)("MCC_Code")), clsCommon.myCDate(dt.Rows(0)("Shift_Date")), trans)
            End If

            Dim obj As clsMilkShiftUploaderHead = clsMilkShiftUploaderHead.GetData(strCode, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Document No: " + strCode + " not found to Delete")
            End If

            If (obj.Status = ERPTransactionStatus.Approved OrElse obj.Status = ERPTransactionStatus.Posted) Then
                Throw New Exception("Already Posted on :" + obj.Posting_Date)
            End If
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_MILK_SHIFT_UPLOADER_QC_PARAMETER_DETAIL where Document_No='" + strCode + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_MILK_SHIFT_UPLOADER_DETAIL where Document_No='" + strCode + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_MILK_SHIFT_UPLOADER_HEAD where Document_No='" + strCode + "'", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteAndCleanData(ByVal strCode As String) As Boolean
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As clsMilkShiftUploaderHead = clsMilkShiftUploaderHead.GetData(strCode, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Document No: " + strCode + " not found to Delete")
            End If


            clsDBFuncationality.ExecuteNonQuery("update TSPL_MILK_RECEIPT_DETAIL set Against_Uploader_TR_No=null where Against_Uploader_TR_No in (select TR_No from TSPL_MILK_SHIFT_UPLOADER_DETAIL where Document_No='" + strCode + "')", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_MILK_SHIFT_UPLOADER_QC_PARAMETER_DETAIL where Document_No='" + strCode + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_MILK_SHIFT_UPLOADER_DETAIL where Document_No='" + strCode + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_MILK_SHIFT_UPLOADER_HEAD where Document_No='" + strCode + "'", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostDataForBatch(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If

            Dim obj As clsMilkProcurementUploaderHead = clsMilkProcurementUploaderHead.GetData(strCode, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Document No: " + strCode + " not found to Post")
            End If
            If (obj.Status = ERPTransactionStatus.Approved OrElse obj.Status = ERPTransactionStatus.Posted) Then
                Throw New Exception("Already Posted on :" + obj.Posting_Date)
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Status", 2)
            clsCommon.AddColumnsForChange(coll, "Approve_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Approve_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_SHIFT_UPLOADER_HEAD", OMInsertOrUpdate.Update, "Document_No='" + obj.Document_No + "'", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
            Dim obj As clsMilkShiftUploaderHead = clsMilkShiftUploaderHead.GetData(strCode, NavigatorType.Current, trans)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.MilkShiftUploader, obj.MCC_Code, obj.Shift_Date, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Document No: " + strCode + " not found to Post")
            End If
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Posted on :" + obj.Posting_Date)
            End If
            clsMCCPaymentCycleLockForScheduler.CheckForSchedulerLock(obj.MCC_Code, obj.Shift_Date, trans)

            MilkProcurementUploader(obj, trans)
            'MilkRejectUploader(obj, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Status", 1)
            clsCommon.AddColumnsForChange(coll, "Posted_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Posted_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_SHIFT_UPLOADER_HEAD", OMInsertOrUpdate.Update, "Document_No='" + obj.Document_No + "'", trans)
            'Throw New Exception("Balwinder Singh Premi")

        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function MilkRejectUploader(ByVal obj As clsMilkShiftUploaderHead, ByVal trans As SqlTransaction) As Boolean
        Dim dblSNFDeductionPer As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SNFDeductionPercent, clsFixedParameterCode.SNFDeductionPercent, trans))
        Dim dblFATDeductionPer As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FATDeductionPercent, clsFixedParameterCode.FATDeductionPercent, trans))
        Dim objMilkRejectHead As clsMilkRejectHead = New clsMilkRejectHead
        objMilkRejectHead.DOC_DATE = obj.Shift_Date
        objMilkRejectHead.SHIFT = obj.Shift
        objMilkRejectHead.MCC_CODE = obj.MCC_Code
        objMilkRejectHead.TOTAL_WEIGHT = 0
        objMilkRejectHead.Arr = New List(Of clsMilkRejectDetail)
        Dim qry As String
        Dim dt As DataTable
        Dim settMilkCollectionPickBulkRoute As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkCollectionPickBulkRoute, clsFixedParameterCode.MilkCollectionPickBulkRoute, trans)) = 1)
        For Each objTr As clsMilkShiftUploaderDetail In obj.Arr
            If clsCommon.myLen(objTr.Reject_Type) > 0 Then
                qry = " select TSPL_Primary_Vehicle_Master.Vehicle_Weight,TSPL_VLC_MASTER_HEAD.Route_Code,TSPL_VLC_MASTER_HEAD.VSP_Code,TSPL_MCC_ROUTE_MASTER.Vehicle_Code,TSPL_VENDOR_MASTER.EMP_Type,TSPL_VENDOR_MASTER.EMP_Fixed_Amount ,TSPL_VENDOR_MASTER.Actual_charges_Slab,TSPL_VENDOR_MASTER.Actual_charges,TSPL_VENDOR_MASTER.Actual_charges_Slab2,TSPL_VENDOR_MASTER.Actual_charges2,TSPL_VENDOR_MASTER.Actual_charges_Slab3,TSPL_VENDOR_MASTER.Actual_charges3,TSPL_VENDOR_MASTER.Actual_charges_Slab4,TSPL_VENDOR_MASTER.Actual_charges4 ,TSPL_VENDOR_MASTER.Actual_charges_Slab5,TSPL_VENDOR_MASTER.Actual_charges5,TSPL_VENDOR_MASTER.Service_Charge_Per_Unit,coalesce(TSPL_VENDOR_MASTER.Rate_Head_Load,0) as Rate_Head_Load,coalesce(TSPL_VENDOR_MASTER.Rate_Own_Asset,0) as Rate_Own_Asset,TSPL_VENDOR_MASTER.Service_Basis_Head_Load,TSPL_VENDOR_MASTER.Service_Basis_Own_Asset,TSPL_Primary_Vehicle_Master.Vendor_Code as TransporterCode,TSPL_VENDOR_MASTER.Service_Charge_Type,TSPL_VENDOR_MASTER.TIP_Buffalo,TSPL_VENDOR_MASTER.TIP_Cow,TSPL_VENDOR_MASTER.TIP_Mix " + Environment.NewLine +
                                " from TSPL_VLC_MASTER_HEAD " + Environment.NewLine +
                                " left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_VLC_MASTER_HEAD.Route_Code " + Environment.NewLine +
                                " left join TSPL_VENDOR_MASTER on   TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_CODE " + Environment.NewLine +
                                " left outer join TSPL_Primary_Vehicle_Master on TSPL_Primary_Vehicle_Master.Vehicle_Code=TSPL_MCC_ROUTE_MASTER.Vehicle_Code " + Environment.NewLine +
                                " where TSPL_VLC_MASTER_HEAD.VLC_Code='" + objTr.VLC_Code + "'"
                Dim dtVLC As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dtVLC Is Nothing OrElse dtVLC.Rows.Count <= 0 Then
                    Throw New Exception("VLC Code not found :" + objTr.VLC_Code)
                End If
                If objTr.Against_Milk_Collection_DCS_Detail > 0 OrElse settMilkCollectionPickBulkRoute Then
                    Dim dttemp As DataTable = clsMilkCollectionDCS.GetRouteDetails(objTr.Against_Milk_Collection_DCS_Detail, trans, settMilkCollectionPickBulkRoute)
                    If dttemp IsNot Nothing AndAlso dttemp.Rows.Count > 0 Then
                        If clsCommon.myLen(dtVLC.Rows(0)("Route_Code")) <= 0 Then
                            dtVLC.Rows(0)("Route_Code") = dttemp.Rows(0)("Route_Code")
                        End If
                        If clsCommon.myLen(dtVLC.Rows(0)("TransporterCode")) <= 0 Then
                            dtVLC.Rows(0)("TransporterCode") = dttemp.Rows(0)("Tanker_Transporter_Code")
                        End If
                        If clsCommon.myLen(dtVLC.Rows(0)("Vehicle_Code")) <= 0 Then
                            dtVLC.Rows(0)("Vehicle_Code") = dttemp.Rows(0)("Vehicle_No")
                        End If
                        dtVLC.AcceptChanges()
                    End If

                End If
                ''Check no purhcase invoice found for that day if found means bills are generated
                qry = "select distinct TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE from  TSPL_MILK_PURCHASE_INVOICE_DETAIL left outer join TSPL_MILK_PURCHASE_INVOICE_HEAD on TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE=TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE where TSPL_MILK_SRN_HEAD.MCC_CODE='" + obj.MCC_Code + "' and TSPL_MILK_SRN_HEAD.SHIFT='" + obj.Shift + "' and convert(date, TSPL_MILK_SRN_HEAD.DOC_DATE,103)=convert(date, '" + clsCommon.GetPrintDate(obj.Shift_Date, "dd/MMM/yyyy") + "',103) and TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE='" + clsCommon.myCstr(dtVLC.Rows(0)("VSP_Code")) + "'"
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Throw New Exception("Milk Purchase invoice :" + clsCommon.myCstr(dt.Rows(0)("DOC_CODE")) + " created for VSP:" + clsCommon.myCstr(dtVLC.Rows(0)("VSP_Code")))
                End If
                qry = "select UOM_Code from TSPL_Mcc_UOM_DETAIL where stocking_unit='Y' and MCC_CODE='" & obj.MCC_Code & "' "
                Dim Unit_Code As String = clsDBFuncationality.getSingleValue(qry, trans)
                If Unit_Code = "" Then
                    Throw New Exception("Fill UOM of Mcc" + obj.MCC_Code)
                End If
                Dim conv_fac As Double = clsWeightConversionInfo.GetConversion_factor(Unit_Code, IIf(clsCommon.CompairString(Unit_Code, "KG") = CompairStringResult.Equal, "LTR", "KG"), trans)

                Dim objMilkRejectDetail As clsMilkRejectDetail = New clsMilkRejectDetail
                objMilkRejectDetail.Item_CODE = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Code from TSPL_MILK_REJECT_TYPE where code = '" + objTr.Reject_Type + "'", trans)) ''Will come on based of rejection.
                objMilkRejectDetail.SAMPLE_NO = objMilkRejectHead.Arr.Count + 1
                objMilkRejectDetail.VLC_CODE = objTr.VLC_Code
                objMilkRejectDetail.ROUTE_CODE = clsCommon.myCstr(dtVLC.Rows(0)("Route_Code"))
                objMilkRejectDetail.VSP_CODE = clsCommon.myCstr(dtVLC.Rows(0)("VSP_Code"))
                objMilkRejectDetail.VEHICLE_CODE = clsCommon.myCstr(dtVLC.Rows(0)("Vehicle_Code"))
                objMilkRejectDetail.Other_VEHICLE = False
                objMilkRejectDetail.Other_VLC = False
                objMilkRejectDetail.NO_OF_CANS = objTr.No_Of_Cans
                objMilkRejectDetail.MILK_WEIGHT = objTr.Milk_Weight
                If clsCommon.CompairString(Unit_Code, "KG") = CompairStringResult.Equal Then
                    objMilkRejectDetail.ACC_WEIGHT_KG = clsCommon.myCdbl(objTr.Milk_Weight)
                    objMilkRejectDetail.ACC_WEIGHT_LTR = clsCommon.myCdbl(objTr.Milk_Weight * conv_fac)
                Else
                    objMilkRejectDetail.ACC_WEIGHT_LTR = clsCommon.myCdbl(objTr.Milk_Weight)
                    objMilkRejectDetail.ACC_WEIGHT_KG = clsCommon.myCdbl(objTr.Milk_Weight * conv_fac)
                End If
                objMilkRejectDetail.Conversion_Factor = conv_fac
                objMilkRejectDetail.UOM_Code = Unit_Code
                objMilkRejectDetail.FAT = objTr.FAT
                objMilkRejectDetail.SNF = objTr.SNF
                objMilkRejectDetail.SNF_Deduction_Per = dblSNFDeductionPer
                objMilkRejectDetail.FAT_Deduction_Per = dblFATDeductionPer
                objMilkRejectDetail.Reject_Type = objTr.Reject_Type
                objMilkRejectDetail.Is_Return = 0
                objMilkRejectDetail.Defaulter = objTr.Reject_Defaulter
                objMilkRejectDetail.Against_Shift_Uploader_TR_No = objTr.TR_No
                objMilkRejectHead.TOTAL_WEIGHT += objMilkRejectDetail.MILK_WEIGHT
                objMilkRejectHead.Arr.Add(objMilkRejectDetail)
            End If
        Next
        If objMilkRejectHead.Arr IsNot Nothing AndAlso objMilkRejectHead.Arr.Count > 0 Then
            qry = "select DOC_CODE from TSPL_MILK_REJECT_HEAD where convert(date, DOC_DATE,103)='" + clsCommon.GetPrintDate(obj.Shift_Date, "dd/MMM/yyyy") + "' and SHIFT='" + obj.Shift + "' and MCC_CODE='" + obj.MCC_Code + "'"
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Throw New Exception("Milk Reject No [" + clsCommon.myCstr(dt.Rows(0)("DOC_CODE") + "] already Created"))
            End If
            clsMilkRejectHead.SaveData(objMilkRejectHead, True, trans)
            clsMilkRejectHead.PostData(objMilkRejectHead.DOC_CODE, trans)
        End If
        Return True
    End Function

    Public Shared Function MilkProcurementUploader(ByVal obj As clsMilkShiftUploaderHead, ByVal trans As SqlTransaction) As Boolean
        Dim dblEmptyCanWeight As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EmptyCanWeight, clsFixedParameterCode.EmptyCanWeight, trans))
        Dim dblMinuteInLastVehicleForGateEntry As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MinuteInLastVehicleForGateEntry, clsFixedParameterCode.MinuteInLastVehicleForGateEntry, trans))
        Dim dblMinuteGateEntryToGrossWeight As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MinuteGateEntryToGrossWeight, clsFixedParameterCode.MinuteGateEntryToGrossWeight, trans))
        Dim dblMinuteGrossWeightToTareWeight As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MinuteGrossWeightToTareWeight, clsFixedParameterCode.MinuteGrossWeightToTareWeight, trans))
        Dim CreateNewDocumentOnUploader As Boolean = True
        Dim WeighmentNotMandatoryInMCC As Boolean = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.WeighmentNotMandatoryInMCC, clsFixedParameterCode.WeighmentNotMandatoryInMCC, trans)) = 1
        Dim IsRoundOffPaiseAmount As Boolean = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RoundOffPaiseAmount, clsFixedParameterCode.RoundOffPaiseAmount, trans)) = 1
        Dim isPickCLRInsteadOfSNF As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProcuremntPickCLRInsteadOfSNF, clsFixedParameterCode.MilkProcuremntPickCLRInsteadOfSNF, trans)) > 0)
        Dim settMilkCollectionPickBulkRoute As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkCollectionPickBulkRoute, clsFixedParameterCode.MilkCollectionPickBulkRoute, trans)) = 1)
        Dim PickPriceFromFATAndSNF As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProcuremntPickCLRInsteadOfSNF, clsFixedParameterCode.PickPriceFromFATAndSNF, trans)) > 0)
        Try
            Dim qry As String
            Dim dt As DataTable
            Dim corrFactor As Double = clsFixedParameter.GetData(clsFixedParameterType.defaultCorrectionFactor, clsFixedParameterCode.MilkSetting, trans)
            Dim strICode = clsFixedParameter.GetData(clsFixedParameterType.MCCDefaultMilkItem, clsFixedParameterCode.MilkSetting, trans)
            Dim strShift As String = obj.Shift
            Dim strShiftDate As String = clsCommon.GetPrintDate(obj.Shift_Date, "dd/MMM/yyyy")
            Dim dtShiftDate As DateTime = obj.Shift_Date

            If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                For Each objtr As clsMilkShiftUploaderDetail In obj.Arr
                    Dim strDockCollectionMilkType As String = objtr.Dock_Collection_Milk_Type
                    If Not objCommonVar.DisplayTypeInMilkReceipt Then
                        strDockCollectionMilkType = "M"
                    End If
                    qry = " select TSPL_Primary_Vehicle_Master.Vehicle_Weight,TSPL_VLC_MASTER_HEAD.Route_Code,TSPL_VLC_MASTER_HEAD.VSP_Code,TSPL_MCC_ROUTE_MASTER.Vehicle_Code,TSPL_VENDOR_MASTER.EMP_Type,TSPL_VENDOR_MASTER.EMP_Fixed_Amount ,TSPL_VENDOR_MASTER.Actual_charges_Slab,TSPL_VENDOR_MASTER.Actual_charges,TSPL_VENDOR_MASTER.Actual_charges_Slab2,TSPL_VENDOR_MASTER.Actual_charges2,TSPL_VENDOR_MASTER.Actual_charges_Slab3,TSPL_VENDOR_MASTER.Actual_charges3,TSPL_VENDOR_MASTER.Actual_charges_Slab4,TSPL_VENDOR_MASTER.Actual_charges4 ,TSPL_VENDOR_MASTER.Actual_charges_Slab5,TSPL_VENDOR_MASTER.Actual_charges5,TSPL_VENDOR_MASTER.Service_Charge_Per_Unit,coalesce(TSPL_VENDOR_MASTER.Rate_Head_Load,0) as Rate_Head_Load,coalesce(TSPL_VENDOR_MASTER.Rate_Own_Asset,0) as Rate_Own_Asset,TSPL_VENDOR_MASTER.Service_Basis_Head_Load,TSPL_VENDOR_MASTER.Service_Basis_Own_Asset,TSPL_Primary_Vehicle_Master.Vendor_Code as TransporterCode,TSPL_VENDOR_MASTER.Service_Charge_Type,TSPL_VENDOR_MASTER.TIP_Buffalo,TSPL_VENDOR_MASTER.TIP_Cow,TSPL_VENDOR_MASTER.TIP_Mix,TSPL_VLC_MASTER_HEAD.Milk_Receive_UOM,TSPL_VENDOR_MASTER.DistanceKM_Head_Load " + Environment.NewLine +
                            " from TSPL_VLC_MASTER_HEAD " + Environment.NewLine +
                            " left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_VLC_MASTER_HEAD.Route_Code " + Environment.NewLine +
                            " left join TSPL_VENDOR_MASTER on   TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_CODE " + Environment.NewLine +
                            " left outer join TSPL_Primary_Vehicle_Master on TSPL_Primary_Vehicle_Master.Vehicle_Code=TSPL_MCC_ROUTE_MASTER.Vehicle_Code " + Environment.NewLine +
                            " where TSPL_VLC_MASTER_HEAD.VLC_Code='" + objtr.VLC_Code + "'"
                    Dim dtVLC As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtVLC Is Nothing OrElse dtVLC.Rows.Count <= 0 Then
                        Throw New Exception("VLC Code not found :" + objtr.VLC_Code)
                    End If
                    If objtr.Against_Milk_Collection_DCS_Detail > 0 OrElse settMilkCollectionPickBulkRoute Then
                        Dim dttemp As DataTable = clsMilkCollectionDCS.GetRouteDetails(objtr.Against_Milk_Collection_DCS_Detail, trans, settMilkCollectionPickBulkRoute)
                        If dttemp IsNot Nothing AndAlso dttemp.Rows.Count > 0 Then
                            If clsCommon.myLen(dtVLC.Rows(0)("Route_Code")) <= 0 Then
                                dtVLC.Rows(0)("Route_Code") = dttemp.Rows(0)("Route_Code")
                            End If
                            If clsCommon.myLen(dtVLC.Rows(0)("TransporterCode")) <= 0 Then
                                dtVLC.Rows(0)("TransporterCode") = dttemp.Rows(0)("Tanker_Transporter_Code")
                            End If
                            If clsCommon.myLen(dtVLC.Rows(0)("Vehicle_Code")) <= 0 Then
                                dtVLC.Rows(0)("Vehicle_Code") = dttemp.Rows(0)("Vehicle_No")
                            End If
                            dtVLC.AcceptChanges()
                        End If
                    End If

                    If clsCommon.myLen(dtVLC.Rows(0)("Route_Code")) <= 0 Then
                        Throw New Exception("Route not defined for VLC Code [" + objtr.VLC_Code + "]")
                    End If
                    If clsCommon.myLen(dtVLC.Rows(0)("TransporterCode")) <= 0 Then
                        Throw New Exception("Transporter not defined for VLC Code [" + objtr.VLC_Code + "]")
                    End If

                    ''Check no purhcase invoice found for that day if found means bills are generated
                    qry = "select distinct TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE 
from  TSPL_MILK_PURCHASE_INVOICE_DETAIL 
left outer join TSPL_MILK_PURCHASE_INVOICE_HEAD on TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE=TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE 
left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE 
where TSPL_MILK_SRN_HEAD.MCC_CODE='" + obj.MCC_Code + "' and TSPL_MILK_SRN_HEAD.SHIFT='" + strShift + "' and convert(date, TSPL_MILK_SRN_HEAD.DOC_DATE,103)=convert(date, '" + strShiftDate + "',103) 
and TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE='" + clsCommon.myCstr(dtVLC.Rows(0)("VSP_Code")) + "'"
                    dt = clsDBFuncationality.GetDataTable(qry, trans)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        Throw New Exception("Milk Purchase invoice :" + clsCommon.myCstr(dt.Rows(0)("DOC_CODE")) + " created for VSP:" + clsCommon.myCstr(dtVLC.Rows(0)("VSP_Code")) + " .For Payment Cycle Date :" + strShiftDate + " Shift :" + strShift)
                    End If

                    qry = "select UOM_Code from TSPL_Mcc_UOM_DETAIL where stocking_unit='Y' and MCC_CODE='" & obj.MCC_Code & "' "
                    Dim Unit_Code As String = clsDBFuncationality.getSingleValue(qry, trans)
                    If Unit_Code = "" Then
                        Throw New Exception("Fill UOM of Mcc" + obj.MCC_Code)
                    End If
                    qry = "select UOM_Code from TSPL_Item_UOM_DETAIL where Item_CODE='" & strICode & "' and UOM_code='" & Unit_Code & "' "
                    Dim Item_Unit_Code As String = clsDBFuncationality.getSingleValue(qry, trans)
                    If Item_Unit_Code = "" Then
                        Throw New Exception("Fill " & Unit_Code & " UOM of Item " + strICode)
                    End If
                    Dim conv_fac As Double = clsWeightConversionInfo.GetConversion_factor(Unit_Code, IIf(clsCommon.CompairString(Unit_Code, "KG") = CompairStringResult.Equal, "LTR", "KG"), trans)

                    ''Milk SRN Save
                    Dim objMilkSRNHead As clsMilkSRNMCC = New clsMilkSRNMCC
                    'objMilkSRNHead.MILK_SAMPLE_CODE = MilkSampleNo
                    objMilkSRNHead.DOC_DATE = dtShiftDate
                    objMilkSRNHead.SHIFT = strShift
                    objMilkSRNHead.COMM_PORT = ""
                    objMilkSRNHead.MCC_CODE = obj.MCC_Code
                    objMilkSRNHead.SAMPLE_NO = 1
                    objMilkSRNHead.VLC_DOC_CODE = objtr.VLC_Code
                    objMilkSRNHead.VEHICLE_CODE = clsCommon.myCstr(dtVLC.Rows(0)("Vehicle_Code"))
                    objMilkSRNHead.VLC_CODE = objtr.VLC_Code
                    objMilkSRNHead.ROUTE_CODE = clsCommon.myCstr(dtVLC.Rows(0)("Route_Code"))
                    objMilkSRNHead.VSP_CODE = clsCommon.myCstr(dtVLC.Rows(0)("VSP_Code"))
                    objMilkSRNHead.TransPorter = clsCommon.myCstr(dtVLC.Rows(0)("TransporterCode"))
                    objMilkSRNHead.Dock_Collection_Milk_Type = strDockCollectionMilkType
                    objMilkSRNHead.Against_Shift_Uploader_TR_No = objtr.TR_No

                    Dim objMilkSRNDetail As clsMilkSRNMCCDetail = New clsMilkSRNMCCDetail()
                    objMilkSRNDetail.Item_CODE = strICode
                    Dim Unit_CodeApply As String = Unit_Code
                    Dim conv_facApply As String = conv_fac
                    If clsCommon.myLen(clsCommon.myCstr(dtVLC.Rows(0)("Milk_Receive_UOM"))) > 0 Then
                        Unit_CodeApply = clsCommon.myCstr(dtVLC.Rows(0)("Milk_Receive_UOM"))
                        conv_facApply = clsWeightConversionInfo.GetConversion_factor(Unit_CodeApply, IIf(clsCommon.CompairString(Unit_CodeApply, "KG") = CompairStringResult.Equal, "LTR", "KG"), trans)
                    End If
                    objMilkSRNDetail.MILK_Qty = objtr.Milk_Weight
                    objMilkSRNDetail.UOM = Unit_CodeApply
                    If clsCommon.CompairString(Unit_CodeApply, "KG") = CompairStringResult.Equal Then
                        objMilkSRNDetail.ACC_Qty = clsCommon.myCdbl(objtr.Milk_Weight)
                        objMilkSRNDetail.ACC_Qty_LTR = clsCommon.myCdbl(objtr.Milk_Weight * conv_facApply)
                    Else
                        objMilkSRNDetail.ACC_Qty_LTR = clsCommon.myCdbl(objtr.Milk_Weight)
                        objMilkSRNDetail.ACC_Qty = clsCommon.myCdbl(objtr.Milk_Weight * conv_facApply)
                    End If
                    objMilkSRNDetail.FAT = objtr.FAT
                    If isPickCLRInsteadOfSNF Then
                        objMilkSRNDetail.CLR = Math.Truncate(objtr.SNF * 10) / 10
                        objMilkSRNDetail.SNF = clsEkoPro.getSnfOnCalculation(objtr.FAT, objMilkSRNDetail.CLR, corrFactor)
                        If PickPriceFromFATAndSNF Then
                            objMilkSRNDetail.SNF = clsCommon.myRoundOFF(objMilkSRNDetail.SNF, 1, 4)
                            objMilkSRNDetail.RATE = clsEkoPro.getRateAndPriceCodeFromUploaderShiftWise(objMilkSRNDetail.MILK_Qty, objMilkSRNDetail.Price_Code, objMilkSRNDetail.FAT, objMilkSRNDetail.SNF, obj.MCC_Code, objMilkSRNHead.VLC_CODE, objMilkSRNHead.SHIFT, dtShiftDate, trans, strDockCollectionMilkType, objMilkSRNDetail.QAT_Rate, objMilkSRNDetail.Negative_Rate)
                        Else
                            objMilkSRNDetail.SNF = clsCommon.myRoundOFF(objMilkSRNDetail.SNF, 2, 9)
                            objMilkSRNDetail.RATE = clsEkoPro.getRateFromUploaderShiftWiseCLR(objMilkSRNDetail.FAT, objMilkSRNDetail.CLR, obj.MCC_Code, objtr.VLC_Code, obj.Shift, dtShiftDate, trans, strDockCollectionMilkType, objMilkSRNDetail.Price_Code)
                        End If
                    Else
                        objMilkSRNDetail.SNF = objtr.SNF
                        objMilkSRNDetail.CLR = clsEkoPro.getClrOnCalculation(objMilkSRNDetail.FAT, objMilkSRNDetail.SNF, corrFactor)
                        If PickPriceFromFATAndSNF Then
                            objMilkSRNDetail.RATE = clsEkoPro.getRateAndPriceCodeFromUploaderShiftWise(objMilkSRNDetail.MILK_Qty, objMilkSRNDetail.Price_Code, objMilkSRNDetail.FAT, objMilkSRNDetail.SNF, obj.MCC_Code, objtr.VLC_Code, obj.Shift, dtShiftDate, trans, strDockCollectionMilkType, objMilkSRNDetail.QAT_Rate, objMilkSRNDetail.Negative_Rate)
                        Else
                            objMilkSRNDetail.CLR = clsCommon.myRoundOFF(objMilkSRNDetail.CLR, 0, 4)
                            objMilkSRNDetail.RATE = clsEkoPro.getRateFromUploaderShiftWiseCLR(objMilkSRNDetail.FAT, objMilkSRNDetail.CLR, obj.MCC_Code, objtr.VLC_Code, obj.Shift, dtShiftDate, trans, strDockCollectionMilkType, objMilkSRNDetail.Price_Code)
                        End If
                    End If
                    If Not objtr.QAT Then
                        objMilkSRNDetail.QAT_Rate = 0
                    End If

                    objMilkSRNDetail.MCC_CODE = obj.MCC_Code
                    objMilkSRNDetail.Correction_Factor = corrFactor

                    objMilkSRNDetail.AMOUNT = Math.Round(clsCommon.myCdbl(objMilkSRNDetail.RATE * objMilkSRNDetail.MILK_Qty), 2, MidpointRounding.AwayFromZero)
                    objMilkSRNDetail.Own_Asset_Rate = clsCommon.myCdbl(dtVLC.Rows(0)("Rate_Own_Asset"))

                    objMilkSRNDetail.QAT_Amt = clsCommon.myRoundOFF(objMilkSRNDetail.QAT_Rate * objMilkSRNDetail.MILK_Qty, 2, 4)
                    objMilkSRNDetail.Negative_Amount = clsCommon.myRoundOFF(objMilkSRNDetail.Negative_Rate * objMilkSRNDetail.MILK_Qty, 2, 4)
                    objMilkSRNDetail.Commission = 0 ' because nature is always E and it is never C 'clsCommon.myCdbl(dr(0)("Actual_charges"))
                    objMilkSRNDetail.Commission_Amount = Math.Round(objMilkSRNDetail.AMOUNT * objMilkSRNDetail.Commission / 100, 2)
                    objMilkSRNDetail.Std_Qty = clsInventoryMovementNew.GetStdQty(trans, Math.Round(objMilkSRNDetail.ACC_Qty * objMilkSRNDetail.FAT / 100, 2), Math.Round(objMilkSRNDetail.ACC_Qty * objMilkSRNDetail.SNF / 100, 2), objMilkSRNHead.DOC_DATE)

                    If clsCommon.myLen(objtr.Reject_Type) > 0 Then
                        Dim objMRT As clsMilkRejectType = clsMilkRejectType.GetData(objtr.Reject_Type, NavigatorType.Current, trans)
                        If objMRT Is Nothing Then
                            Throw New Exception("Invalid rejection type [" + objtr.Reject_Type + "]")
                        End If
                        objMilkSRNDetail.Item_CODE = objMRT.Item_Code
                        Dim dclRate As Decimal = objMilkSRNDetail.RATE
                        Dim dclAmt As Decimal = 0
                        Dim CalKG As Decimal = 0
                        If objMRT.Applicable_On = 1 Then  ''RAte
                            dclRate = objMRT.Applicable_Per
                            dclAmt = Math.Round((dclRate * objtr.Milk_Weight), 2, MidpointRounding.AwayFromZero)
                        ElseIf objMRT.Applicable_On = 2 Then  ''FAT KG RAte
                            CalKG = (objtr.Milk_Weight * objtr.FAT) / 100
                            dclAmt = Math.Round((objMRT.Applicable_Per * CalKG), 2, MidpointRounding.AwayFromZero)
                            dclRate = clsCommon.myCDivide(dclAmt, objtr.Milk_Weight)
                        ElseIf objMRT.Applicable_On = 3 Then  ''SNF KG RAte
                            CalKG = (objtr.Milk_Weight * objtr.SNF) / 100
                            dclAmt = Math.Round((objMRT.Applicable_Per * CalKG), 2, MidpointRounding.AwayFromZero)
                            dclRate = clsCommon.myCDivide(dclAmt, objtr.Milk_Weight)
                        Else ''%Age
                            dclRate = Math.Round(dclRate * objMRT.Applicable_Per / 100, 2, MidpointRounding.AwayFromZero)
                            dclAmt = Math.Round((dclRate * objtr.Milk_Weight), 2, MidpointRounding.AwayFromZero)
                        End If
                        objMilkSRNDetail.RATE = dclRate
                        objMilkSRNDetail.AMOUNT = dclAmt
                    End If

                    If clsCommon.CompairString(clsCommon.myCstr(dtVLC.Rows(0)("EMP_Type")), "FP") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(dtVLC.Rows(0)("EMP_Type")), "FAFP") = CompairStringResult.Equal Then
                        objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges"))
                        If clsCommon.CompairString(clsCommon.myCstr(dtVLC.Rows(0)("Service_Charge_Type")), "%(Percentage)") = CompairStringResult.Equal Then
                            objMilkSRNDetail.Emp_Amount = Math.Round(objMilkSRNDetail.AMOUNT * objMilkSRNDetail.Payment_Commission / 100, 2)
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dtVLC.Rows(0)("Service_Charge_Type")), "Rate/Kg") = CompairStringResult.Equal Then
                            objMilkSRNDetail.Emp_Amount = Math.Round(objMilkSRNDetail.ACC_Qty * objMilkSRNDetail.Payment_Commission, 2)
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dtVLC.Rows(0)("Service_Charge_Type")), "Rate/Ltr") = CompairStringResult.Equal Then
                            objMilkSRNDetail.Emp_Amount = Math.Round(objMilkSRNDetail.MILK_Qty * objMilkSRNDetail.Payment_Commission, 2)
                        End If
                        If clsCommon.CompairString(clsCommon.myCstr(dtVLC.Rows(0)("EMP_Type")), "FAFP") = CompairStringResult.Equal Then
                            objMilkSRNDetail.Emp_Amount += clsCommon.myCdbl(dtVLC.Rows(0)("EMP_Fixed_Amount"))
                        End If
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(dtVLC.Rows(0)("EMP_Type")), "SWP") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(dtVLC.Rows(0)("EMP_Type")), "FASWP") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(clsCommon.myCstr(dtVLC.Rows(0)("Service_Charge_Type")), "%(Percentage)") = CompairStringResult.Equal Then
                            If clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab5")) > 0 AndAlso objMilkSRNDetail.AMOUNT >= clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab5")) Then
                                objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges5"))
                            ElseIf clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab4")) > 0 AndAlso objMilkSRNDetail.AMOUNT >= clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab4")) Then
                                objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges4"))
                            ElseIf clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab3")) > 0 AndAlso objMilkSRNDetail.AMOUNT >= clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab3")) Then
                                objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges3"))
                            ElseIf clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab2")) > 0 AndAlso objMilkSRNDetail.AMOUNT >= clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab2")) Then
                                objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges2"))
                            Else
                                objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges"))
                            End If
                            objMilkSRNDetail.Emp_Amount = Math.Round(objMilkSRNDetail.AMOUNT * objMilkSRNDetail.Payment_Commission / 100, 2)
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dtVLC.Rows(0)("Service_Charge_Type")), "Rate/Kg") = CompairStringResult.Equal Then
                            If clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab5")) > 0 AndAlso objMilkSRNDetail.ACC_Qty >= clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab5")) Then
                                objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges5"))
                            ElseIf clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab4")) > 0 AndAlso objMilkSRNDetail.ACC_Qty >= clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab4")) Then
                                objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges4"))
                            ElseIf clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab3")) > 0 AndAlso objMilkSRNDetail.ACC_Qty >= clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab3")) Then
                                objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges3"))
                            ElseIf clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab2")) > 0 AndAlso objMilkSRNDetail.ACC_Qty >= clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab2")) Then
                                objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges2"))
                            Else
                                objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges"))
                            End If
                            objMilkSRNDetail.Emp_Amount = Math.Round(objMilkSRNDetail.ACC_Qty * objMilkSRNDetail.Payment_Commission, 2)
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dtVLC.Rows(0)("Service_Charge_Type")), "Rate/Ltr") = CompairStringResult.Equal Then
                            If clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab5")) > 0 AndAlso objMilkSRNDetail.MILK_Qty >= clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab5")) Then
                                objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges5"))
                            ElseIf clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab4")) > 0 AndAlso objMilkSRNDetail.MILK_Qty >= clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab4")) Then
                                objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges4"))
                            ElseIf clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab3")) > 0 AndAlso objMilkSRNDetail.MILK_Qty >= clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab3")) Then
                                objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges3"))
                            ElseIf clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab2")) > 0 AndAlso objMilkSRNDetail.MILK_Qty >= clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges_Slab2")) Then
                                objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges2"))
                            Else
                                objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC.Rows(0)("Actual_charges"))
                            End If
                            objMilkSRNDetail.Emp_Amount = Math.Round(objMilkSRNDetail.MILK_Qty * objMilkSRNDetail.Payment_Commission, 2)
                        End If
                        If clsCommon.CompairString(clsCommon.myCstr(dtVLC.Rows(0)("EMP_Type")), "FASWP") = CompairStringResult.Equal Then
                            objMilkSRNDetail.Emp_Amount += clsCommon.myCdbl(dtVLC.Rows(0)("EMP_Fixed_Amount"))
                        End If

                    ElseIf clsCommon.CompairString(clsCommon.myCstr(dtVLC(0)("EMP_Type")), "FPSP") = CompairStringResult.Equal Then
                        objMilkSRNDetail.Payment_Commission = clsCommon.myCdbl(dtVLC(0)("Actual_charges"))
                        Dim objSPR As clsStandardPrice = clsStandardPrice.GetStandartPrice(objMilkSRNDetail.Price_Code, trans)
                        If objSPR IsNot Nothing Then
                            If (objSPR.Std_Percent_FAT <> 0 AndAlso objSPR.Std_Percent_SNF <> 0) Then
                                If clsCommon.CompairString(clsCommon.myCstr(dtVLC(0)("Service_Charge_Type")), "Rate/Kg") = CompairStringResult.Equal Then
                                    objMilkSRNDetail.Emp_Amount = Math.Round((Math.Round(objMilkSRNDetail.ACC_Qty * objMilkSRNDetail.FAT / 100, 3) * objMilkSRNDetail.Payment_Commission * objSPR.Weightage_FAT / objSPR.Std_Percent_FAT) + (Math.Round(objMilkSRNDetail.ACC_Qty * objMilkSRNDetail.SNF / 100, 3) * objMilkSRNDetail.Payment_Commission * objSPR.Weightage_SNF / objSPR.Std_Percent_SNF), 2)
                                ElseIf clsCommon.CompairString(clsCommon.myCstr(dtVLC(0)("Service_Charge_Type")), "Rate/Ltr") = CompairStringResult.Equal Then
                                    Dim qty As Decimal = objMilkSRNDetail.ACC_Qty
                                    If conv_facApply <> 0 Then
                                        qty = objMilkSRNDetail.ACC_Qty / conv_facApply
                                    End If
                                    objMilkSRNDetail.Emp_Amount = Math.Round((Math.Round(qty * objMilkSRNDetail.FAT / 100, 3) * objMilkSRNDetail.Payment_Commission * objSPR.Weightage_FAT / objSPR.Std_Percent_FAT) + (Math.Round(qty * objMilkSRNDetail.SNF / 100, 3) * objMilkSRNDetail.Payment_Commission * objSPR.Weightage_SNF / objSPR.Std_Percent_SNF), 2)
                                Else
                                    objMilkSRNDetail.Emp_Amount = 0
                                End If
                            End If
                        End If
                    Else
                        Throw New Exception("EMP Type is Not a valid")
                    End If

                    If clsCommon.CompairString(strDockCollectionMilkType, "C") = CompairStringResult.Equal Then
                        objMilkSRNDetail.TIP_Amount = Math.Round(clsCommon.myCdbl(dtVLC(0)("TIP_Cow")) * (objMilkSRNDetail.FAT + objMilkSRNDetail.SNF) * objMilkSRNDetail.ACC_Qty / 100, 2, MidpointRounding.AwayFromZero)
                    ElseIf clsCommon.CompairString(strDockCollectionMilkType, "B") = CompairStringResult.Equal Then
                        objMilkSRNDetail.TIP_Amount = Math.Round(clsCommon.myCdbl(dtVLC(0)("TIP_Buffalo")) * objMilkSRNDetail.FAT * objMilkSRNDetail.ACC_Qty / 100, 2, MidpointRounding.AwayFromZero)
                    Else
                        objMilkSRNDetail.TIP_Amount = Math.Round(clsCommon.myCdbl(dtVLC(0)("TIP_Mix")) * objMilkSRNDetail.FAT * objMilkSRNDetail.ACC_Qty / 100, 2, MidpointRounding.AwayFromZero)
                    End If

                    objMilkSRNDetail.Service_Charge_Type = clsCommon.myCstr(dtVLC.Rows(0)("Service_Charge_Type"))
                    '==================Head Load==========================
                    Dim MinimumQtyForHeadLoad As Decimal = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.MinimumQtyForHeadLoad, clsFixedParameterCode.MinimumQtyForHeadLoad, trans))
                    Dim dclDistanceKM As Decimal = clsCommon.myCdbl(dtVLC.Rows(0)("DistanceKM_Head_Load"))
                    If dclDistanceKM = 0 Then
                        dclDistanceKM = 1
                    End If
                    Dim ExcluetHeadLoad As Boolean = False
                    If clsCommon.myLen(objtr.Reject_Type) > 0 Then
                        qry = "select ISNULL(Exclude_Head,0) as Exclude_Head from TSPL_MILK_REJECT_TYPE where Code='" + objtr.Reject_Type + "' "
                        ExcluetHeadLoad = (clsCommon.myCDecimal(clsDBFuncationality.getSingleValue(qry, trans)) = 1)
                    End If
                    If ExcluetHeadLoad Then
                        objMilkSRNDetail.Head_Load_Rate = 0
                        objMilkSRNDetail.Head_Load_Amount = 0
                        objMilkSRNDetail.Head_Load_Type = ""
                    Else
                        Dim objHeadLoad As New clsHeadLoadDCS()
                        objHeadLoad = clsHeadLoadDCS.GetDcsData(objMilkSRNHead.VLC_CODE, dtShiftDate, trans)
                        objMilkSRNDetail.Head_Load_Rate = clsCommon.myCdbl(objHeadLoad.Head_Load_Rate)
                        objMilkSRNDetail.Head_Load_Type = clsCommon.myCstr(objHeadLoad.Head_Load_Basis)
                        objMilkSRNDetail.Head_Load_Cycle = 0
                        objMilkSRNDetail.Head_Load_Amount = 0
                        If clsCommon.CompairString(clsCommon.myCstr(objHeadLoad.Head_Load_Basis), "K") = CompairStringResult.Equal Then
                            If objMilkSRNDetail.ACC_Qty >= MinimumQtyForHeadLoad Then
                                objMilkSRNDetail.Head_Load_Amount = Math.Round(objMilkSRNDetail.ACC_Qty * objHeadLoad.Head_Load_Rate * dclDistanceKM, 2)
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(objHeadLoad.Head_Load_Basis), "L") = CompairStringResult.Equal Then
                            If objMilkSRNDetail.ACC_Qty_LTR >= MinimumQtyForHeadLoad Then
                                objMilkSRNDetail.Head_Load_Amount = Math.Round(objMilkSRNDetail.ACC_Qty_LTR * objHeadLoad.Head_Load_Rate * dclDistanceKM, 2)
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(objHeadLoad.Head_Load_Basis), "CK") = CompairStringResult.Equal Then
                            objMilkSRNDetail.Head_Load_Cycle = Math.Ceiling(clsCommon.myCDivide(objMilkSRNDetail.ACC_Qty, objHeadLoad.Cycle_Frequency))
                            objMilkSRNDetail.Head_Load_Amount = Math.Round(objMilkSRNDetail.Head_Load_Cycle * objHeadLoad.Head_Load_Rate, 2)
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(objHeadLoad.Head_Load_Basis), "CL") = CompairStringResult.Equal Then
                            objMilkSRNDetail.Head_Load_Cycle = Math.Ceiling(clsCommon.myCDivide(objMilkSRNDetail.ACC_Qty_LTR, objHeadLoad.Cycle_Frequency))
                            objMilkSRNDetail.Head_Load_Amount = Math.Round(objMilkSRNDetail.Head_Load_Cycle * objHeadLoad.Head_Load_Rate, 2)
                        End If
                    End If

                    '============================================
                    '==================Own Asset==========================
                    If clsCommon.CompairString(clsCommon.myCstr(dtVLC.Rows(0)("Service_Basis_Own_Asset")), "K") = CompairStringResult.Equal Then
                        objMilkSRNDetail.Own_Asset_Amount = Math.Round(objMilkSRNDetail.ACC_Qty * objMilkSRNDetail.Own_Asset_Rate, 2)
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(dtVLC.Rows(0)("Service_Basis_Own_Asset")), "L") = CompairStringResult.Equal Then
                        objMilkSRNDetail.Own_Asset_Amount = Math.Round(objMilkSRNDetail.MILK_Qty * objMilkSRNDetail.Own_Asset_Rate, 2)
                    End If
                    objMilkSRNDetail.Own_Asset_Type = clsCommon.myCstr(dtVLC.Rows(0)("Service_Basis_Own_Asset"))
                    '============================================
                    objMilkSRNDetail.Service_Charge_Amount = Math.Round(objMilkSRNDetail.MILK_Qty * clsCommon.myCdbl(dtVLC.Rows(0)("Service_Charge_Per_Unit")), 2)
                    objMilkSRNDetail.NET_AMOUNT = Math.Round(objMilkSRNDetail.AMOUNT + objMilkSRNDetail.Emp_Amount + objMilkSRNDetail.TIP_Amount - objMilkSRNDetail.Service_Charge_Amount, 2)
                    If IsRoundOffPaiseAmount Then
                        objMilkSRNDetail.Round_Off = (objMilkSRNDetail.NET_AMOUNT Mod 1)
                        objMilkSRNDetail.NET_AMOUNT = objMilkSRNDetail.NET_AMOUNT - (objMilkSRNDetail.NET_AMOUNT Mod 1)
                    End If
                    objMilkSRNDetail.COMM_PORT = ""
                    Dim arrMilkSRNDetail As New List(Of clsMilkSRNMCCDetail)
                    arrMilkSRNDetail.Add(objMilkSRNDetail)
                    clsMilkSRNMCC.SaveData(objMilkSRNHead, arrMilkSRNDetail, trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function


    Public Shared Function DeleteCollectionData(arrMCC As ArrayList, FromDate As Date, ToDate As Date, strShift As String) As Boolean
        Return DeleteCollectionData(arrMCC, FromDate, ToDate, strShift, True, True)
    End Function
    Public Shared Function DeleteCollectionData(arrMCC As ArrayList, FromDate As Date, ToDate As Date, strShift As String, DeleteBMCCollection As Boolean, checkForPreviousShift As Boolean) As Boolean
        Try
            clsCommon.ProgressBarShow()
            Dim strShiftCon As String = ""
            If checkForPreviousShift Then
                strShiftCon = " and SHIFT='E'"
                DeleteCollectionBulk(FromDate.AddDays(-1), FromDate.AddDays(-1), strShiftCon, arrMCC, DeleteBMCCollection)

                strShiftCon = ""
                DeleteCollectionBulk(FromDate, ToDate.AddDays(-1), strShiftCon, arrMCC, DeleteBMCCollection)

                strShiftCon = " and SHIFT='M'"
                DeleteCollectionBulk(ToDate, ToDate, strShiftCon, arrMCC, DeleteBMCCollection)
            Else
                If Not clsCommon.CompairString(clsCommon.myCstr(strShift), "B") = CompairStringResult.Equal Then
                    strShiftCon = " and SHIFT='" + clsCommon.myCstr(strShift) + "'"
                End If
                DeleteCollectionBulk(FromDate, ToDate, strShiftCon, arrMCC, DeleteBMCCollection)
            End If
            clsCommon.ProgressBarHide()
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    'Public Shared Function DeleteCollectionData(arrMCC As ArrayList, FromDate As Date, ToDate As Date, strShift As String, DeleteBMCCollection As Boolean, checkForPreviousShift As Boolean) As Boolean
    '    Try
    '        clsCommon.ProgressBarPercentShow()
    '        If arrMCC Is Nothing OrElse arrMCC.Count < 0 Then
    '            Throw New Exception("Please Provide at least one MCC")
    '        End If
    '        Dim qry As String = "select  * from ExplodeDates('" + clsCommon.GetPrintDate(FromDate, "dd/MMM/yyyy") + "','" + clsCommon.GetPrintDate(ToDate, "dd/MMM/yyyy") + "')"
    '        Dim dtDate As DataTable = clsDBFuncationality.GetDataTable(qry)
    '        If dtDate Is Nothing OrElse dtDate.Rows.Count <= 0 Then
    '            Throw New Exception("No Date found between from and To Date")
    '        End If
    '        Dim ii As Integer = 0
    '        Dim Total As Integer = arrMCC.Count * dtDate.Rows.Count
    '        For Each drDate As DataRow In dtDate.Rows
    '            Dim TransDate As Date = clsCommon.myCDate(drDate(0))
    '            For Each strMCCcode As String In arrMCC
    '                ii = ii + 1
    '                clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Date [" & clsCommon.GetPrintDate(TransDate, "dd/MMM/yyyy") & "] BMC [" & strMCCcode & "]")
    '                Dim strShiftCon As String = ""
    '                If checkForPreviousShift Then
    '                    strShiftCon = " and SHIFT='E'"
    '                    DeleteCollection(TransDate.AddDays(-1), strShiftCon, strMCCcode, DeleteBMCCollection)
    '                    strShiftCon = " and SHIFT='M'"
    '                    DeleteCollection(TransDate, strShiftCon, strMCCcode, DeleteBMCCollection)
    '                Else
    '                    If Not clsCommon.CompairString(clsCommon.myCstr(strShift), "B") = CompairStringResult.Equal Then
    '                        strShiftCon = " and SHIFT='" + clsCommon.myCstr(strShift) + "'"
    '                    End If
    '                    DeleteCollection(TransDate, strShiftCon, strMCCcode, DeleteBMCCollection)
    '                End If
    '            Next
    '        Next
    '        clsCommon.ProgressBarPercentHide()
    '    Catch ex As Exception
    '        clsCommon.ProgressBarPercentHide()
    '        Throw New Exception(ex.Message)
    '    End Try
    '    Return True
    'End Function

    '    Private Shared Sub DeleteCollection(tDate As Date, strShiftCon As String, strMCCcode As String, DeleteBMCCollection As Boolean)
    '        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
    '        Try
    '            DeleteCollection(tDate, strShiftCon, strMCCcode, DeleteBMCCollection, tran)
    '            tran.Commit()
    '        Catch ex As Exception
    '            tran.Rollback()
    '            Throw New Exception(ex.Message)
    '        End Try
    '    End Sub
    '    Public Shared Sub DeleteCollection(tDate As Date, strShiftCon As String, strMCCcode As String, DeleteBMCCollection As Boolean, tran As SqlTransaction)
    '        Dim transDate As String = "'" + clsCommon.GetPrintDate(tDate, "dd/MMM/yyyy") + "'"
    '        Dim qry As String = ""

    '        qry = "select * into TEMP_TSPL_MILK_RECEIPT_DETAIL from (select Against_Uploader_TR_No,Against_Shift_Uploader_TR_No from TSPL_MILK_RECEIPT_DETAIL where DOC_CODE in ( select DOC_CODE from TSPL_MILK_RECEIPT_HEAD where convert(date, DOC_DATE,103)=" + transDate + " and MCC_CODE='" + strMCCcode + "' " + strShiftCon + ")
    'union all
    'select null as Against_Uploader_TR_No, TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No as Against_Shift_Uploader_TR_No  from TSPL_MILK_SHIFT_UPLOADER_DETAIL 
    'left outer join TSPL_MILK_SHIFT_UPLOADER_HEAD on TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No=TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No
    'where len(isnull( TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type,''))>0 and convert(date,TSPL_MILK_SHIFT_UPLOADER_HEAD. Shift_Date,103)=" + transDate + " and TSPL_MILK_SHIFT_UPLOADER_HEAD.MCC_Code='" + strMCCcode + "' " + strShiftCon + "
    'union all
    'select TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No as Against_Uploader_TR_No,null as Against_Shift_Uploader_TR_No  from TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL
    'left outer join TSPL_MILK_PROCUREMENT_UPLOADER_HEAD on TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No
    'where len(isnull( TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Reject_Type,''))>0 and convert(date,TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Shift_Date,103)=" + transDate + " and TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.MCC_Code='" + strMCCcode + "' " + strShiftCon + "
    ')xx"
    '        clsDBFuncationality.ExecuteNonQuery(qry, tran)
    '        qry = "select * into TEMP_TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL from (
    'select TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No,TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No,TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail from TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL where TR_No in (select Against_Uploader_TR_No from TEMP_TSPL_MILK_RECEIPT_DETAIL))X"
    '        clsDBFuncationality.ExecuteNonQuery(qry, tran)
    '        qry = "select * into TEMP_TSPL_MILK_SHIFT_UPLOADER_DETAIL from (
    'select TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No,TR_No,Against_Milk_Collection_DCS_Detail from TSPL_MILK_SHIFT_UPLOADER_DETAIL where TR_No in (select Against_Shift_Uploader_TR_No from TEMP_TSPL_MILK_RECEIPT_DETAIL))X"
    '        clsDBFuncationality.ExecuteNonQuery(qry, tran)
    '        qry = "select * into TEMP_TSPL_MILK_COLLECTION_DCS_DETAIL from (  
    'select Document_No ,PK_Id from TSPL_MILK_COLLECTION_DCS_DETAIL where PK_Id in  (select Against_Milk_Collection_DCS_Detail from TEMP_TSPL_MILK_SHIFT_UPLOADER_DETAIL union all select Against_Milk_Collection_DCS_Detail from TEMP_TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL))X"
    '        clsDBFuncationality.ExecuteNonQuery(qry, tran)
    '        qry = "select * into TEMP_TSPL_MILK_COLLECTION_DCS_MCC_DETAIL from (
    'select TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No as MCCDocument_No,TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No,TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail,TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.PK_Id from TSPL_MILK_COLLECTION_DCS_MCC_DETAIL
    'left outer join  TSPL_MILK_COLLECTION_MCC_DETAIL on TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id=TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail
    'where TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No in (
    'select Document_No from TEMP_TSPL_MILK_COLLECTION_DCS_DETAIL))X"
    '        clsDBFuncationality.ExecuteNonQuery(qry, tran)

    '        qry = "delete from TSPL_MILK_SRN_detail_SYNC where doc_code in (select doc_code from TSPL_MILK_SRN_HEAD_SYNC where mcc_code='" + strMCCcode + "')"
    '        clsDBFuncationality.ExecuteNonQuery(qry, tran)

    '        qry = "delete from TSPL_MILK_SRN_HEAD_SYNC where mcc_code='" + strMCCcode + "'"
    '        clsDBFuncationality.ExecuteNonQuery(qry, tran)


    '        qry = "delete from TSPL_PROVISION_ENTRY where Ref_Doc_No in (select DOC_CODE from TSPL_MILK_Shift_End_HEAD where MCC_CODE='" + strMCCcode + "' and convert(date, DOC_DATE,103)=" + transDate + " " + strShiftCon + ") "
    '        clsDBFuncationality.ExecuteNonQuery(qry, tran)
    '        qry = "delete from TSPL_MILK_Shift_End_Route_DETAIL where DOC_CODE in ( select DOC_CODE from TSPL_MILK_Shift_End_HEAD where MCC_CODE='" + strMCCcode + "' and convert(date, DOC_DATE,103)=" + transDate + " " + strShiftCon + ")"
    '        clsDBFuncationality.ExecuteNonQuery(qry, tran)
    '        qry = "delete from TSPL_MILK_Shift_End_DETAIL where DOC_CODE in( select DOC_CODE from TSPL_MILK_Shift_End_HEAD where MCC_CODE='" + strMCCcode + "' and convert(date, DOC_DATE,103)=" + transDate + " " + strShiftCon + ")"
    '        clsDBFuncationality.ExecuteNonQuery(qry, tran)
    '        qry = "delete from TSPL_MILK_Shift_End_HEAD where MCC_CODE='" + strMCCcode + "' and convert(date, DOC_DATE,103)=" + transDate + " " + strShiftCon + ""
    '        clsDBFuncationality.ExecuteNonQuery(qry, tran)
    '        '----Milk Sample
    '        qry = "delete from TSPL_INVENTORY_MOVEMENT_new where Source_Doc_No in ( select DOC_CODE from TSPL_MILK_SRN_HEAD where MILK_SAMPLE_CODE in (select DOC_CODE from TSPL_MILK_SAMPLE_HEAD where convert(date, DOC_DATE,103)=" + transDate + " and MCC_CODE='" + strMCCcode + "' " + strShiftCon + "))"
    '        clsDBFuncationality.ExecuteNonQuery(qry, tran)
    '        qry = "delete from TSPL_JOURNAL_DETAILS  where Voucher_No in ( select Voucher_No from TSPL_JOURNAL_MASTER  where Source_Doc_No in ( select DOC_CODE from TSPL_MILK_SRN_HEAD where MILK_SAMPLE_CODE in (select DOC_CODE from TSPL_MILK_SAMPLE_HEAD where convert(date, DOC_DATE,103)=" + transDate + " and MCC_CODE='" + strMCCcode + "' " + strShiftCon + ")))"
    '        clsDBFuncationality.ExecuteNonQuery(qry, tran)
    '        qry = "delete from TSPL_JOURNAL_MASTER  where Source_Doc_No in ( select DOC_CODE from TSPL_MILK_SRN_HEAD where MILK_SAMPLE_CODE in (select DOC_CODE from TSPL_MILK_SAMPLE_HEAD where convert(date, DOC_DATE,103)=" + transDate + " and MCC_CODE='" + strMCCcode + "' " + strShiftCon + "))"
    '        clsDBFuncationality.ExecuteNonQuery(qry, tran)
    '        qry = "delete from TSPL_MILK_SRN_DETAIL where DOC_CODE in ( select DOC_CODE from TSPL_MILK_SRN_HEAD where MILK_SAMPLE_CODE in (select DOC_CODE from TSPL_MILK_SAMPLE_HEAD where convert(date, DOC_DATE,103)=" + transDate + " and MCC_CODE='" + strMCCcode + "' " + strShiftCon + "))"
    '        clsDBFuncationality.ExecuteNonQuery(qry, tran)
    '        qry = "delete from TSPL_MILK_SRN_HEAD where MILK_SAMPLE_CODE in (select DOC_CODE from TSPL_MILK_SAMPLE_HEAD where convert(date, DOC_DATE,103)=" + transDate + " and MCC_CODE='" + strMCCcode + "' " + strShiftCon + ")"
    '        clsDBFuncationality.ExecuteNonQuery(qry, tran)
    '        qry = "delete from TSPL_MILK_SAMPLE_DETAIL where DOC_CODE in (select DOC_CODE from TSPL_MILK_SAMPLE_HEAD where convert(date, DOC_DATE,103)=" + transDate + " and MCC_CODE='" + strMCCcode + "' " + strShiftCon + ")"
    '        clsDBFuncationality.ExecuteNonQuery(qry, tran)
    '        qry = "delete from TSPL_MILK_SAMPLE_DETAIL_History where DOC_CODE in (select DOC_CODE from TSPL_MILK_SAMPLE_HEAD where convert(date, DOC_DATE,103)=" + transDate + " and MCC_CODE='" + strMCCcode + "' " + strShiftCon + ")"
    '        clsDBFuncationality.ExecuteNonQuery(qry, tran)
    '        qry = "delete from TSPL_MILK_SAMPLE_READING_LOG where Sample_Code in (select DOC_CODE from TSPL_MILK_SAMPLE_HEAD where convert(date, DOC_DATE,103)=" + transDate + " and MCC_CODE='" + strMCCcode + "' " + strShiftCon + ")"
    '        clsDBFuncationality.ExecuteNonQuery(qry, tran)
    '        qry = "delete from TSPL_MILK_SAMPLE_QC_PARAMETER_DETAIL where DOC_CODE in (select DOC_CODE from TSPL_MILK_SAMPLE_HEAD where convert(date, DOC_DATE,103)=" + transDate + " and MCC_CODE='" + strMCCcode + "' " + strShiftCon + ")"
    '        clsDBFuncationality.ExecuteNonQuery(qry, tran)
    '        qry = "delete from TSPL_MILK_SAMPLE_HEAD where convert(date, DOC_DATE,103)=" + transDate + " and MCC_CODE='" + strMCCcode + "' " + strShiftCon + ""
    '        clsDBFuncationality.ExecuteNonQuery(qry, tran)
    '        '----End of Milk Sample


    '        '----Milk Rejection
    '        qry = "delete from TSPL_INVENTORY_MOVEMENT_new where Source_Doc_No in ( select DOC_CODE from TSPL_MILK_SRN_HEAD where against_reject_no in (select DOC_CODE from TSPL_MILK_REJECT_HEAD where convert(date, DOC_DATE,103)=" + transDate + " and MCC_CODE='" + strMCCcode + "' " + strShiftCon + "))"
    '        clsDBFuncationality.ExecuteNonQuery(qry, tran)
    '        qry = "delete from TSPL_JOURNAL_DETAILS  where Voucher_No in ( select Voucher_No from TSPL_JOURNAL_MASTER  where Source_Doc_No in ( select DOC_CODE from TSPL_MILK_SRN_HEAD where against_reject_no in (select DOC_CODE from TSPL_MILK_REJECT_HEAD where convert(date, DOC_DATE,103)=" + transDate + " and MCC_CODE='" + strMCCcode + "' " + strShiftCon + ")))"
    '        clsDBFuncationality.ExecuteNonQuery(qry, tran)
    '        qry = "delete from TSPL_JOURNAL_MASTER  where Source_Doc_No in ( select DOC_CODE from TSPL_MILK_SRN_HEAD where against_reject_no in (select DOC_CODE from TSPL_MILK_REJECT_HEAD where convert(date, DOC_DATE,103)=" + transDate + " and MCC_CODE='" + strMCCcode + "' " + strShiftCon + "))"
    '        clsDBFuncationality.ExecuteNonQuery(qry, tran)
    '        qry = "delete from TSPL_MILK_SRN_DETAIL where DOC_CODE in ( select DOC_CODE from TSPL_MILK_SRN_HEAD where against_reject_no in (select DOC_CODE from TSPL_MILK_REJECT_HEAD where convert(date, DOC_DATE,103)=" + transDate + " and MCC_CODE='" + strMCCcode + "' " + strShiftCon + "))"
    '        clsDBFuncationality.ExecuteNonQuery(qry, tran)
    '        qry = "delete from TSPL_MILK_SRN_HEAD where against_reject_no in (select DOC_CODE from TSPL_MILK_REJECT_HEAD where convert(date, DOC_DATE,103)=" + transDate + " and MCC_CODE='" + strMCCcode + "' " + strShiftCon + ")"
    '        clsDBFuncationality.ExecuteNonQuery(qry, tran)
    '        qry = "delete from TSPL_MILK_REJECT_Detail where DOC_CODE in (select DOC_CODE from TSPL_MILK_REJECT_HEAD where convert(date, DOC_DATE,103)=" + transDate + " and MCC_CODE='" + strMCCcode + "' " + strShiftCon + ")"
    '        clsDBFuncationality.ExecuteNonQuery(qry, tran)
    '        qry = "delete from TSPL_MILK_REJECT_HEAD where convert(date, DOC_DATE,103)=" + transDate + " and MCC_CODE='" + strMCCcode + "' " + strShiftCon + ""
    '        clsDBFuncationality.ExecuteNonQuery(qry, tran)
    '        '----End of Milk Rejection




    '        qry = "delete from TSPL_MILK_RECEIPT_DETAIL where DOC_CODE in ( select DOC_CODE from TSPL_MILK_RECEIPT_HEAD where convert(date, DOC_DATE,103)=" + transDate + " and MCC_CODE='" + strMCCcode + "' " + strShiftCon + ")"
    '        clsDBFuncationality.ExecuteNonQuery(qry, tran)
    '        qry = "delete from TSPL_MILK_RECEIPT_HEAD where convert(date, DOC_DATE,103)=" + transDate + " and MCC_CODE='" + strMCCcode + "' " + strShiftCon + ""
    '        clsDBFuncationality.ExecuteNonQuery(qry, tran)
    '        qry = "delete from TSPL_OPEN_MCC_SHIFT where MCC_CODE='" + strMCCcode + "' and convert(date, MCC_SHIFT_DATE,103)=" + transDate + " " + strShiftCon + ""
    '        clsDBFuncationality.ExecuteNonQuery(qry, tran)

    '        qry = "delete from TSPL_MILK_PROCUREMENT_UPLOADER_QC_PARAMETER_DETAIL where TR_No in (select Against_Uploader_TR_No from TEMP_TSPL_MILK_RECEIPT_DETAIL)"
    '        clsDBFuncationality.ExecuteNonQuery(qry, tran)
    '        qry = "delete from TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL where TR_No in (select Against_Uploader_TR_No from TEMP_TSPL_MILK_RECEIPT_DETAIL)"
    '        clsDBFuncationality.ExecuteNonQuery(qry, tran)
    '        qry = "delete from TSPL_MILK_PROCUREMENT_UPLOADER_HEAD where not exists(select 1 from TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL where TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No=TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No)"
    '        clsDBFuncationality.ExecuteNonQuery(qry, tran)

    '        qry = "delete from TSPL_MILK_SHIFT_UPLOADER_QC_PARAMETER_DETAIL where TR_No in (select Against_Shift_Uploader_TR_No from TEMP_TSPL_MILK_RECEIPT_DETAIL)"
    '        clsDBFuncationality.ExecuteNonQuery(qry, tran)
    '        qry = "delete from TSPL_MILK_SHIFT_UPLOADER_DETAIL where TR_No in (select Against_Shift_Uploader_TR_No from TEMP_TSPL_MILK_RECEIPT_DETAIL)"
    '        clsDBFuncationality.ExecuteNonQuery(qry, tran)
    '        qry = "delete from TSPL_MILK_SHIFT_UPLOADER_HEAD where not exists(select 1 from TSPL_MILK_SHIFT_UPLOADER_DETAIL where TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No=TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No)"
    '        clsDBFuncationality.ExecuteNonQuery(qry, tran)

    '        qry = "delete from TSPL_MILK_COLLECTION_DCS_DETAIL where PK_Id in (select PK_Id from TEMP_TSPL_MILK_COLLECTION_DCS_DETAIL)"
    '        clsDBFuncationality.ExecuteNonQuery(qry, tran)
    '        qry = "delete from TSPL_MILK_COLLECTION_DCS_MCC_DETAIL where Document_No in (select Document_No from TSPL_MILK_COLLECTION_DCS where not exists(select 1 from TSPL_MILK_COLLECTION_DCS_DETAIL where TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No=TSPL_MILK_COLLECTION_DCS.Document_No))"
    '        clsDBFuncationality.ExecuteNonQuery(qry, tran)
    '        qry = "delete from TSPL_MILK_COLLECTION_DCS where not exists(select 1 from TSPL_MILK_COLLECTION_DCS_DETAIL where TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No=TSPL_MILK_COLLECTION_DCS.Document_No)"
    '        clsDBFuncationality.ExecuteNonQuery(qry, tran)

    '        If DeleteBMCCollection Then
    '            qry = "select * into TEMP_TSPL_MILK_COLLECTION_MCC_DETAIL from (  
    'select Against_Milk_Collection_MCC_Detail as PK_ID,TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No from TEMP_TSPL_MILK_COLLECTION_DCS_MCC_DETAIL 
    'left outer join TSPL_MILK_COLLECTION_MCC_DETAIL on TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id=TEMP_TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail
    'where not exists(select 1 from TSPL_MILK_COLLECTION_DCS_MCC_DETAIL where TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.PK_Id=TEMP_TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.PK_Id))X"
    '            clsDBFuncationality.ExecuteNonQuery(qry, tran)

    '            qry = "delete from TSPL_MILK_COLLECTION_MCC_DETAIL where PK_Id in (select PK_ID from TEMP_TSPL_MILK_COLLECTION_MCC_DETAIL)"
    '            clsDBFuncationality.ExecuteNonQuery(qry, tran)
    '            qry = "delete from TSPL_MILK_COLLECTION_MCC where not exists(select 1 from TSPL_MILK_COLLECTION_MCC_DETAIL where TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No=TSPL_MILK_COLLECTION_MCC.Document_No) and TSPL_MILK_COLLECTION_MCC.Document_No in (select Document_No from TEMP_TSPL_MILK_COLLECTION_MCC_DETAIL)"
    '            clsDBFuncationality.ExecuteNonQuery(qry, tran)

    '            qry = "Drop table TEMP_TSPL_MILK_COLLECTION_MCC_DETAIL"
    '            clsDBFuncationality.ExecuteNonQuery(qry, tran)
    '        End If

    '        qry = "Drop table TEMP_TSPL_MILK_RECEIPT_DETAIL "
    '        clsDBFuncationality.ExecuteNonQuery(qry, tran)
    '        qry = "Drop table TEMP_TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL"
    '        clsDBFuncationality.ExecuteNonQuery(qry, tran)
    '        qry = "Drop table TEMP_TSPL_MILK_SHIFT_UPLOADER_DETAIL"
    '        clsDBFuncationality.ExecuteNonQuery(qry, tran)
    '        qry = "Drop table TEMP_TSPL_MILK_COLLECTION_DCS_DETAIL"
    '        clsDBFuncationality.ExecuteNonQuery(qry, tran)
    '        qry = "Drop table TEMP_TSPL_MILK_COLLECTION_DCS_MCC_DETAIL"
    '        clsDBFuncationality.ExecuteNonQuery(qry, tran)

    '    End Sub

    Private Shared Sub DeleteCollectionBulk(FromDate As Date, ToDate As Date, strShiftCon As String, ArrMCC As ArrayList, DeleteBMCCollection As Boolean)
        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteCollectionBulk(FromDate, ToDate, strShiftCon, ArrMCC, DeleteBMCCollection, tran)
            tran.Commit()
        Catch ex As Exception
            tran.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Public Shared Sub DeleteCollectionBulk(FromDate As Date, ToDate As Date, strShiftCon As String, ArrMCC As ArrayList, DeleteBMCCollection As Boolean, tran As SqlTransaction)
        Dim transDate As String = " between '" + clsCommon.GetPrintDate(FromDate, "dd/MMM/yyyy") + "' and '" + clsCommon.GetPrintDate(ToDate, "dd/MMM/yyyy") + "' "
        Dim strMCCcode As String = " "
        If ArrMCC IsNot Nothing AndAlso ArrMCC.Count > 0 Then
            strMCCcode = " and MCC_CODE in (" + clsCommon.GetMulcallString(ArrMCC) + ") "
        End If
        Dim qry As String = "select * into TEMP_TSPL_MILK_RECEIPT_DETAIL from (
        select Against_Uploader_TR_No,Against_Shift_Uploader_TR_No from TSPL_MILK_SRN_HEAD where ( Against_Uploader_TR_No is not null or Against_Shift_Uploader_TR_No is not null) and convert(date, DOC_DATE,103) " + transDate + "" + strMCCcode + "" + strShiftCon + "
        union all
        select null as Against_Uploader_TR_No, TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No as Against_Shift_Uploader_TR_No  from TSPL_MILK_SHIFT_UPLOADER_DETAIL 
        left outer join TSPL_MILK_SHIFT_UPLOADER_HEAD on TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No=TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No
        where len(isnull( TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type,''))>0 and convert(date,TSPL_MILK_SHIFT_UPLOADER_HEAD. Shift_Date,103) " + transDate + "" + strMCCcode + "" + strShiftCon + "
        union all
        select TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No as Against_Uploader_TR_No,null as Against_Shift_Uploader_TR_No  from TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL
        left outer join TSPL_MILK_PROCUREMENT_UPLOADER_HEAD on TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No
        where len(isnull( TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Reject_Type,''))>0 and convert(date,TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Shift_Date,103)  " + transDate + "" + strMCCcode + "" + strShiftCon + "
        )xx"
        clsDBFuncationality.ExecuteNonQuery(qry, tran)
        qry = "select * into TEMP_TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL from (
select TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No,TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No,TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail from TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL where TR_No in (select Against_Uploader_TR_No from TEMP_TSPL_MILK_RECEIPT_DETAIL))X"
        clsDBFuncationality.ExecuteNonQuery(qry, tran)
        qry = "select * into TEMP_TSPL_MILK_SHIFT_UPLOADER_DETAIL from (
select TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No,TR_No,Against_Milk_Collection_DCS_Detail from TSPL_MILK_SHIFT_UPLOADER_DETAIL where TR_No in (select Against_Shift_Uploader_TR_No from TEMP_TSPL_MILK_RECEIPT_DETAIL))X"
        clsDBFuncationality.ExecuteNonQuery(qry, tran)
        qry = "select * into TEMP_TSPL_MILK_COLLECTION_DCS_DETAIL from (  
select Document_No ,PK_Id from TSPL_MILK_COLLECTION_DCS_DETAIL where PK_Id in  (select Against_Milk_Collection_DCS_Detail from TEMP_TSPL_MILK_SHIFT_UPLOADER_DETAIL union all select Against_Milk_Collection_DCS_Detail from TEMP_TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL))X"
        clsDBFuncationality.ExecuteNonQuery(qry, tran)
        qry = "select * into TEMP_TSPL_MILK_COLLECTION_DCS_MCC_DETAIL from (
select TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No as MCCDocument_No,TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No,TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail,TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.PK_Id from TSPL_MILK_COLLECTION_DCS_MCC_DETAIL
left outer join  TSPL_MILK_COLLECTION_MCC_DETAIL on TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id=TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail
where TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No in (
select Document_No from TEMP_TSPL_MILK_COLLECTION_DCS_DETAIL))X"
        clsDBFuncationality.ExecuteNonQuery(qry, tran)

        'qry = "delete from TSPL_MILK_SRN_detail_SYNC where doc_code in (select doc_code from TSPL_MILK_SRN_HEAD_SYNC where 2=2 and MCC_Code='" + strMCCcode + "')"
        'clsDBFuncationality.ExecuteNonQuery(qry, tran)

        'qry = "delete from TSPL_MILK_SRN_HEAD_SYNC where 2=2 and MCC_Code='" + strMCCcode + "'"
        'clsDBFuncationality.ExecuteNonQuery(qry, tran)


        'qry = "delete from TSPL_PROVISION_ENTRY where Ref_Doc_No in (select DOC_CODE from TSPL_MILK_Shift_End_HEAD where convert(date, DOC_DATE,103) " + transDate + "" + strMCCcode + " " + strShiftCon + ") "
        'clsDBFuncationality.ExecuteNonQuery(qry, tran)
        'qry = "delete from TSPL_MILK_Shift_End_Route_DETAIL where DOC_CODE in ( select DOC_CODE from TSPL_MILK_Shift_End_HEAD where  convert(date, DOC_DATE,103) " + transDate + "" + strMCCcode + " " + strShiftCon + ")"
        'clsDBFuncationality.ExecuteNonQuery(qry, tran)
        'qry = "delete from TSPL_MILK_Shift_End_DETAIL where DOC_CODE in( select DOC_CODE from TSPL_MILK_Shift_End_HEAD where convert(date, DOC_DATE,103) " + transDate + "" + strMCCcode + "" + strShiftCon + ")"
        'clsDBFuncationality.ExecuteNonQuery(qry, tran)
        'qry = "delete from TSPL_MILK_Shift_End_HEAD where  convert(date, DOC_DATE,103) " + transDate + "" + strMCCcode + " " + strShiftCon + ""
        'clsDBFuncationality.ExecuteNonQuery(qry, tran)
        '----Milk Sample
        qry = "delete from TSPL_INVENTORY_MOVEMENT_new where Source_Doc_No in ( select DOC_CODE from TSPL_MILK_SRN_HEAD where ( Against_Uploader_TR_No is not null or Against_Shift_Uploader_TR_No is not null) and convert(date, DOC_DATE,103) " + transDate + "" + strMCCcode + "" + strShiftCon + " )"
        clsDBFuncationality.ExecuteNonQuery(qry, tran)
        qry = "delete from TSPL_JOURNAL_DETAILS  where Voucher_No in ( select Voucher_No from TSPL_JOURNAL_MASTER  where Source_Doc_No in ( select DOC_CODE from TSPL_MILK_SRN_HEAD where ( Against_Uploader_TR_No is not null or Against_Shift_Uploader_TR_No is not null) and convert(date, DOC_DATE,103) " + transDate + "" + strMCCcode + "" + strShiftCon + " ))"
        clsDBFuncationality.ExecuteNonQuery(qry, tran)
        qry = "delete from TSPL_JOURNAL_MASTER  where Source_Doc_No in ( select DOC_CODE from TSPL_MILK_SRN_HEAD where ( Against_Uploader_TR_No is not null or Against_Shift_Uploader_TR_No is not null) and convert(date, DOC_DATE,103) " + transDate + "" + strMCCcode + "" + strShiftCon + " )"
        clsDBFuncationality.ExecuteNonQuery(qry, tran)
        qry = "delete from TSPL_MILK_SRN_DETAIL where DOC_CODE in ( select DOC_CODE from TSPL_MILK_SRN_HEAD where ( Against_Uploader_TR_No is not null or Against_Shift_Uploader_TR_No is not null) and convert(date, DOC_DATE,103) " + transDate + "" + strMCCcode + "" + strShiftCon + " )"
        clsDBFuncationality.ExecuteNonQuery(qry, tran)
        qry = "delete from TSPL_MILK_SRN_HEAD where DOC_CODE in ( select DOC_CODE from TSPL_MILK_SRN_HEAD where ( Against_Uploader_TR_No is not null or Against_Shift_Uploader_TR_No is not null) and convert(date, DOC_DATE,103) " + transDate + "" + strMCCcode + "" + strShiftCon + " )"
        clsDBFuncationality.ExecuteNonQuery(qry, tran)

        'qry = "delete from TSPL_MILK_SAMPLE_DETAIL where DOC_CODE in (select DOC_CODE from TSPL_MILK_SAMPLE_HEAD where convert(date, DOC_DATE,103) " + transDate + "" + strMCCcode + "" + strShiftCon + ")"
        'clsDBFuncationality.ExecuteNonQuery(qry, tran)
        'qry = "delete from TSPL_MILK_SAMPLE_DETAIL_History where DOC_CODE in (select DOC_CODE from TSPL_MILK_SAMPLE_HEAD where convert(date, DOC_DATE,103) " + transDate + "" + strMCCcode + "" + strShiftCon + ")"
        'clsDBFuncationality.ExecuteNonQuery(qry, tran)
        'qry = "delete from TSPL_MILK_SAMPLE_READING_LOG where Sample_Code in (select DOC_CODE from TSPL_MILK_SAMPLE_HEAD where convert(date, DOC_DATE,103) " + transDate + "" + strMCCcode + "" + strShiftCon + ")"
        'clsDBFuncationality.ExecuteNonQuery(qry, tran)
        'qry = "delete from TSPL_MILK_SAMPLE_QC_PARAMETER_DETAIL where DOC_CODE in (select DOC_CODE from TSPL_MILK_SAMPLE_HEAD where convert(date, DOC_DATE,103) " + transDate + "" + strMCCcode + "" + strShiftCon + ")"
        'clsDBFuncationality.ExecuteNonQuery(qry, tran)
        'qry = "delete from TSPL_MILK_SAMPLE_HEAD where convert(date, DOC_DATE,103) " + transDate + "" + strMCCcode + "" + strShiftCon + ""
        'clsDBFuncationality.ExecuteNonQuery(qry, tran)
        '----End of Milk Sample


        '----Milk Rejection
        'qry = "delete from TSPL_INVENTORY_MOVEMENT_new where Source_Doc_No in ( select DOC_CODE from TSPL_MILK_SRN_HEAD where against_reject_no in (select DOC_CODE from TSPL_MILK_REJECT_HEAD where convert(date, DOC_DATE,103) " + transDate + "" + strMCCcode + "" + strShiftCon + "))"
        'clsDBFuncationality.ExecuteNonQuery(qry, tran)
        'qry = "delete from TSPL_JOURNAL_DETAILS  where Voucher_No in ( select Voucher_No from TSPL_JOURNAL_MASTER  where Source_Doc_No in ( select DOC_CODE from TSPL_MILK_SRN_HEAD where against_reject_no in (select DOC_CODE from TSPL_MILK_REJECT_HEAD where convert(date, DOC_DATE,103) " + transDate + "" + strMCCcode + "" + strShiftCon + ")))"
        'clsDBFuncationality.ExecuteNonQuery(qry, tran)
        'qry = "delete from TSPL_JOURNAL_MASTER  where Source_Doc_No in ( select DOC_CODE from TSPL_MILK_SRN_HEAD where against_reject_no in (select DOC_CODE from TSPL_MILK_REJECT_HEAD where convert(date, DOC_DATE,103) " + transDate + "" + strMCCcode + "" + strShiftCon + "))"
        'clsDBFuncationality.ExecuteNonQuery(qry, tran)
        'qry = "delete from TSPL_MILK_SRN_DETAIL where DOC_CODE in ( select DOC_CODE from TSPL_MILK_SRN_HEAD where against_reject_no in (select DOC_CODE from TSPL_MILK_REJECT_HEAD where convert(date, DOC_DATE,103) " + transDate + "" + strMCCcode + "" + strShiftCon + "))"
        'clsDBFuncationality.ExecuteNonQuery(qry, tran)
        'qry = "delete from TSPL_MILK_SRN_HEAD where against_reject_no in (select DOC_CODE from TSPL_MILK_REJECT_HEAD where convert(date, DOC_DATE,103) " + transDate + "" + strMCCcode + "" + strShiftCon + ")"
        'clsDBFuncationality.ExecuteNonQuery(qry, tran)
        'qry = "delete from TSPL_MILK_REJECT_Detail where DOC_CODE in (select DOC_CODE from TSPL_MILK_REJECT_HEAD where convert(date, DOC_DATE,103) " + transDate + "" + strMCCcode + "" + strShiftCon + ")"
        'clsDBFuncationality.ExecuteNonQuery(qry, tran)
        'qry = "delete from TSPL_MILK_REJECT_HEAD where convert(date, DOC_DATE,103) " + transDate + "" + strMCCcode + "" + strShiftCon + ""
        'clsDBFuncationality.ExecuteNonQuery(qry, tran)
        '----End of Milk Rejection

        'qry = "delete from TSPL_MILK_RECEIPT_DETAIL where DOC_CODE in ( select DOC_CODE from TSPL_MILK_RECEIPT_HEAD where convert(date, DOC_DATE,103) " + transDate + "" + strMCCcode + "" + strShiftCon + ")"
        'clsDBFuncationality.ExecuteNonQuery(qry, tran)
        'qry = "delete from TSPL_MILK_RECEIPT_HEAD where convert(date, DOC_DATE,103) " + transDate + "" + strMCCcode + "" + strShiftCon + ""
        'clsDBFuncationality.ExecuteNonQuery(qry, tran)
        'qry = "delete from TSPL_OPEN_MCC_SHIFT where convert(date, MCC_SHIFT_DATE,103) " + transDate + "" + strMCCcode + "" + strShiftCon + ""
        'clsDBFuncationality.ExecuteNonQuery(qry, tran)

        qry = "delete from TSPL_MILK_PROCUREMENT_UPLOADER_QC_PARAMETER_DETAIL where TR_No in (select Against_Uploader_TR_No from TEMP_TSPL_MILK_RECEIPT_DETAIL)"
        clsDBFuncationality.ExecuteNonQuery(qry, tran)
        qry = "delete from TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL where TR_No in (select Against_Uploader_TR_No from TEMP_TSPL_MILK_RECEIPT_DETAIL)"
        clsDBFuncationality.ExecuteNonQuery(qry, tran)
        qry = "delete from TSPL_MILK_PROCUREMENT_UPLOADER_HEAD where not exists(select 1 from TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL where TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Document_No=TSPL_MILK_PROCUREMENT_UPLOADER_HEAD.Document_No)"
        clsDBFuncationality.ExecuteNonQuery(qry, tran)

        qry = "delete from TSPL_MILK_SHIFT_UPLOADER_QC_PARAMETER_DETAIL where TR_No in (select Against_Shift_Uploader_TR_No from TEMP_TSPL_MILK_RECEIPT_DETAIL)"
        clsDBFuncationality.ExecuteNonQuery(qry, tran)
        qry = "delete from TSPL_MILK_SHIFT_UPLOADER_DETAIL where TR_No in (select Against_Shift_Uploader_TR_No from TEMP_TSPL_MILK_RECEIPT_DETAIL)"
        clsDBFuncationality.ExecuteNonQuery(qry, tran)
        qry = "delete from TSPL_MILK_SHIFT_UPLOADER_HEAD where not exists(select 1 from TSPL_MILK_SHIFT_UPLOADER_DETAIL where TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No=TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No)"
        clsDBFuncationality.ExecuteNonQuery(qry, tran)

        qry = "delete from TSPL_MILK_COLLECTION_DCS_DETAIL where PK_Id in (select PK_Id from TEMP_TSPL_MILK_COLLECTION_DCS_DETAIL)"
        clsDBFuncationality.ExecuteNonQuery(qry, tran)
        qry = "delete from TSPL_MILK_COLLECTION_DCS_MCC_DETAIL where Document_No in (select Document_No from TSPL_MILK_COLLECTION_DCS where not exists(select 1 from TSPL_MILK_COLLECTION_DCS_DETAIL where TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No=TSPL_MILK_COLLECTION_DCS.Document_No))"
        clsDBFuncationality.ExecuteNonQuery(qry, tran)
        qry = "delete from TSPL_MILK_COLLECTION_DCS where not exists(select 1 from TSPL_MILK_COLLECTION_DCS_DETAIL where TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No=TSPL_MILK_COLLECTION_DCS.Document_No)"
        clsDBFuncationality.ExecuteNonQuery(qry, tran)

        If DeleteBMCCollection Then
            qry = "select * into TEMP_TSPL_MILK_COLLECTION_MCC_DETAIL from (  
select Against_Milk_Collection_MCC_Detail as PK_ID,TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No from TEMP_TSPL_MILK_COLLECTION_DCS_MCC_DETAIL 
left outer join TSPL_MILK_COLLECTION_MCC_DETAIL on TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id=TEMP_TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail
where not exists(select 1 from TSPL_MILK_COLLECTION_DCS_MCC_DETAIL where TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.PK_Id=TEMP_TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.PK_Id))X"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)

            qry = "delete from TSPL_MILK_COLLECTION_MCC_DETAIL where PK_Id in (select PK_ID from TEMP_TSPL_MILK_COLLECTION_MCC_DETAIL)"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)
            qry = "delete from TSPL_MILK_COLLECTION_MCC where not exists(select 1 from TSPL_MILK_COLLECTION_MCC_DETAIL where TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No=TSPL_MILK_COLLECTION_MCC.Document_No) and TSPL_MILK_COLLECTION_MCC.Document_No in (select Document_No from TEMP_TSPL_MILK_COLLECTION_MCC_DETAIL)"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)

            qry = "Drop table TEMP_TSPL_MILK_COLLECTION_MCC_DETAIL"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)
        End If

        qry = "Drop table TEMP_TSPL_MILK_RECEIPT_DETAIL "
        clsDBFuncationality.ExecuteNonQuery(qry, tran)
        qry = "Drop table TEMP_TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL"
        clsDBFuncationality.ExecuteNonQuery(qry, tran)
        qry = "Drop table TEMP_TSPL_MILK_SHIFT_UPLOADER_DETAIL"
        clsDBFuncationality.ExecuteNonQuery(qry, tran)
        qry = "Drop table TEMP_TSPL_MILK_COLLECTION_DCS_DETAIL"
        clsDBFuncationality.ExecuteNonQuery(qry, tran)
        qry = "Drop table TEMP_TSPL_MILK_COLLECTION_DCS_MCC_DETAIL"
        clsDBFuncationality.ExecuteNonQuery(qry, tran)

    End Sub

    Public Shared Sub MultipleDateSingleExport(ByRef frm As RadForm)
        Try
            Dim isPickCLRInsteadOfSNF As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProcuremntPickCLRInsteadOfSNF, clsFixedParameterCode.MilkProcuremntPickCLRInsteadOfSNF, Nothing)) > 0)
            Dim qry As String = "select REPLACE( convert(varchar, GETDATE(),106),' ','/') as Date,'' as [Shift],'' as Route,'' as DCSNo,"

            If Not isPickCLRInsteadOfSNF Then
                If objCommonVar.DisplayTypeInMilkReceipt Then
                    qry += " '' As [Mix/Cow],"
                End If
            End If


            qry += "'Good' as MilkType,0.00 as Qty,0.0 as FAT"

            Dim ListImpExpColumnsMandatory As List(Of String) = New List(Of String)({"Date", "Shift", "DCSNo", "Qty", "FAT"})
            If isPickCLRInsteadOfSNF Then
                qry += ",0.0 as CLR "
                ListImpExpColumnsMandatory.Add("CLR")
            Else
                qry += ",0.0 as SNF "
                ListImpExpColumnsMandatory.Add("SNF")
            End If
            transportSql.ExporttoExcel(qry, "", "", frm, ListImpExpColumnsMandatory)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(frm, ex.Message, frm.Text)
        End Try
    End Sub
    Public Shared Sub MultipleDateSingleImport(ByRef frm As RadForm)
        Dim isPickCLRInsteadOfSNF As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProcuremntPickCLRInsteadOfSNF, clsFixedParameterCode.MilkProcuremntPickCLRInsteadOfSNF, Nothing)) > 0)
        Dim gv As New RadGridView()
        frm.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        Dim ii As Integer = 1
        Dim indxSuccess As Integer = 0
        Dim indxError As Integer = 0
        Dim arr As New Dictionary(Of String, clsMilkShiftUploaderHead)
        Dim qry As String
        Dim flag As Boolean = False
        If isPickCLRInsteadOfSNF Then
            flag = transportSql.importExcel(gv, "Date", "Shift", "Route", "DCSNo", "MilkType", "Qty", "FAT", "CLR")
        Else
            If objCommonVar.DisplayTypeInMilkReceipt Then
                flag = transportSql.importExcel(gv, "Date", "Shift", "Route", "DCSNo", "Mix/Cow", "MilkType", "Qty", "FAT", "SNF")
            Else
                flag = transportSql.importExcel(gv, "Date", "Shift", "Route", "DCSNo", "MilkType", "Qty", "FAT", "SNF")
            End If
        End If
        If flag Then
            Try
                clsCommon.ProgressBarPercentShow()
                Try
                    For Each grow As GridViewRowInfo In gv.Rows
                        ii += 1
                        clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (gv.Rows.Count)), "Validating Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(gv.Rows.Count) & "")
                        If clsCommon.myLen(grow.Cells("DCSNo").Value) > 0 Then
                            Dim objtemp As New clsTempFATSNFAmt
                            objtemp.IDate = clsCommon.myCDate(grow.Cells("Date").Value)
                            objtemp.IShift = clsCommon.myCstr(grow.Cells("Shift").Value)
                            If Not (clsCommon.CompairString(objtemp.IShift, "M") = CompairStringResult.Equal OrElse clsCommon.CompairString(objtemp.IShift, "E") = CompairStringResult.Equal) Then
                                Throw New Exception("Shift Should be [M/E]")
                            End If
                            objtemp.BulkRoute = clsCommon.myCstr(grow.Cells("Route").Value)
                            If clsCommon.myLen(objtemp.BulkRoute) > 0 Then
                                objtemp.BulkRoute = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ROUTE_NO from TSPL_BULK_ROUTE_MASTER where ROUTE_NO='" + objtemp.BulkRoute + "'"))
                                If clsCommon.myLen(objtemp.BulkRoute) <= 0 Then
                                    Throw New Exception("Invalid Bulk Route [" + objtemp.BulkRoute + "]")
                                End If
                            End If
                            objtemp.VLCUploader = clsCommon.myCstr(grow.Cells("DCSNo").Value)
                            objtemp.Qty = clsCommon.myCDecimal(grow.Cells("Qty").Value)
                            objtemp.FAT = clsCommon.myCDecimal(grow.Cells("FAT").Value)
                            If isPickCLRInsteadOfSNF Then
                                objtemp.SNF = clsCommon.myCDecimal(grow.Cells("CLR").Value)
                            Else
                                objtemp.SNF = clsCommon.myCDecimal(grow.Cells("SNF").Value)
                            End If
                            objtemp.RejectType = clsCommon.myCstr(grow.Cells("MilkType").Value)
                            If clsCommon.myLen(objtemp.RejectType) > 0 Then
                                If clsCommon.CompairString(objtemp.RejectType, "Good") = CompairStringResult.Equal Then
                                    objtemp.RejectType = ""
                                Else
                                    qry = "select Code from TSPL_MILK_REJECT_TYPE where Code='" + objtemp.RejectType + "'"
                                    objtemp.RejectType = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                                    If clsCommon.myLen(objtemp.RejectType) <= 0 Then
                                        Throw New Exception("Invalid Milk Reject Type [" + clsCommon.myCstr(grow.Cells("MilkType").Value) + "]")
                                    End If
                                End If
                            End If


                            qry = "select VLC_Code,MCC from TSPL_VLC_MASTER_HEAD where VLC_Code_VLC_Uploader='" + objtemp.VLCUploader + "'"
                            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                                Throw New Exception("Invalid DCSNo " + objtemp.VLCUploader)
                            End If
                            If dt IsNot Nothing AndAlso dt.Rows.Count > 1 Then
                                Throw New Exception("DCSNo " + objtemp.VLCUploader + " Mapped more than one MCC/BMC")
                            End If

                            objtemp.VLC = clsCommon.myCstr(dt.Rows(0)("VLC_Code"))
                            objtemp.MCC = clsCommon.myCstr(dt.Rows(0)("MCC"))
                            If clsCommon.myLen(objtemp.MCC) <= 0 Then
                                Throw New Exception("DCSNo " + objtemp.VLCUploader + " is not mapped with any MCC/BMC")
                            End If

                            Dim UniqueCombination As String = objtemp.MCC + clsCommon.GetPrintDate(objtemp.IDate, "dd/MM/yyyy") + objtemp.IShift
                            If Not arr.ContainsKey(UniqueCombination) Then
                                qry = "select Document_No from TSPL_MILK_SHIFT_UPLOADER_HEAD where Shift_Date='" + clsCommon.GetPrintDate(objtemp.IDate, "dd/MMM/yyyy") + "' and Shift='" + objtemp.IShift + "' and MCC_Code='" + objtemp.MCC + "'"
                                dt = clsDBFuncationality.GetDataTable(qry)
                                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                    Throw New Exception("Shift uploader No " + clsCommon.myCstr(dt.Rows(0)("Document_No")) + " is already exist for Date [" + clsCommon.GetPrintDate(objtemp.IDate, "dd/MM/yyyy") + "] Shift [" + objtemp.IShift + "] and MCC [" + objtemp.MCC + "]")
                                End If

                                Dim obj As New clsMilkShiftUploaderHead()
                                obj.Shift_Date = objtemp.IDate
                                obj.Shift = objtemp.IShift
                                obj.MCC_Code = objtemp.MCC

                                obj.Arr = New List(Of clsMilkShiftUploaderDetail)
                                arr.Add(UniqueCombination, obj)
                            End If

                            Dim objtr As New clsMilkShiftUploaderDetail
                            objtr.SNo = arr(UniqueCombination).Arr.Count + 1
                            objtr.BULK_ROUTE_NO = objtemp.BulkRoute
                            objtr.VLC_Code = objtemp.VLC
                            objtr.No_Of_Cans = 1
                            objtr.Reject_Type = objtemp.RejectType
                            If clsCommon.myLen(objtr.Reject_Type) > 0 Then
                                objtr.Reject_Defaulter = "VSP"
                            End If
                            objtr.Milk_Weight = objtemp.Qty
                            objtr.FAT = objtemp.FAT
                            objtr.SNF = objtemp.SNF
                            If objCommonVar.DisplayTypeInMilkReceipt Then
                                objtr.Dock_Collection_Milk_Type = clsCommon.myCstr(grow.Cells("Mix/Cow").Value)
                            Else
                                objtr.Dock_Collection_Milk_Type = "M"
                            End If
                            'objtr.Dock_Collection_Milk_Type = "M"
                            arr(UniqueCombination).Arr.Add(objtr)
                            indxSuccess += 1
                        End If
                    Next
                Catch ex As Exception
                    indxError += 1
                    Throw New Exception("At Row No" + clsCommon.myCstr(ii) + " " + ex.Message)
                End Try
                clsCommon.ProgressBarPercentHide()

            Catch ex As Exception
                clsCommon.ProgressBarPercentHide()
                clsCommon.MyMessageBoxShow(frm, ex.Message, frm.Text)
            End Try

            Try
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    qry = "Valid Row [" + clsCommon.myCstr(indxSuccess) + "]" + Environment.NewLine + "Invalid Rows [" + clsCommon.myCstr(indxError) + "] " + Environment.NewLine + "Total Documents To be Generate [" + clsCommon.myCstr(arr.Count) + "]" + Environment.NewLine + "Do You want to Proceed"
                    If clsCommon.MyMessageBoxShow(frm, qry, frm.Text, MessageBoxButtons.YesNo) = DialogResult.Yes Then
                        clsCommon.ProgressBarPercentShow()
                        ii = 0
                        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                        Try
                            For Each key As String In arr.Keys
                                ii += 1
                                clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (arr.Count)), "Saving Document..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(arr.Count) & "")
                                Dim obj As clsMilkShiftUploaderHead = arr.Item(key)
                                obj.SaveData(obj, True, True, trans)
                            Next
                            trans.Commit()
                        Catch ex As Exception
                            trans.Rollback()
                            Throw New Exception(ex.Message)
                        Finally
                            clsCommon.ProgressBarPercentHide()
                        End Try
                    End If
                Else
                    Throw New Exception("No Valid Rows Found to Save")
                End If
                clsCommon.MyMessageBoxShow(frm, "Data Transfer Completed!", frm.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.MyMessageBoxShow(frm, ex.Message, frm.Text)
            End Try
        End If
        frm.Controls.Remove(gv)
    End Sub


    Public Shared Sub MultipleDateSingleImportDBF(ByRef frm As RadForm)
        Dim gv As New RadGridView()
        frm.Controls.Add(gv)
        Try
            Dim dtRejctType As DataTable = clsDBFuncationality.GetDataTable("select Code from TSPL_MILK_REJECT_TYPE")
            Dim arrColumnName As New List(Of String)
            Dim arrColumnHeader As New List(Of String)
            Dim arrColumnMandatory As New List(Of Integer)
            Dim qry As String = "Select Max(Export_Code) As Export_Code from TSPL_EXPORT_TEMPLATE_HEAD where Program_Code='IMP-DBF' and Report_Type='' and Is_Default_Value=1"
            Dim code As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            If clsCommon.myLen(code) <= 0 Then
                Throw New Exception("Please Set DBF Templete")
            End If
            qry = "select Column_Name,Column_Header,Column_Mandatory from TSPL_EXPORT_TEMPLATE_DETAIL where Export_Code='" + code + "' and len(isnull( Column_Header  ,''))>0"
            Dim dtTemp As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dtTemp Is Nothing OrElse dtTemp.Rows.Count <= 0 Then
                Throw New Exception("Please set Details of DBF Templete")
            End If
            Dim inputs(dtTemp.Rows.Count - 1) As String
            For indx As Integer = 0 To dtTemp.Rows.Count - 1
                inputs(indx) = clsCommon.myCstr(dtTemp.Rows(indx)("Column_Header"))
                arrColumnName.Add(dtTemp.Rows(indx)("Column_Name"))
                arrColumnHeader.Add(dtTemp.Rows(indx)("Column_Header"))
                arrColumnMandatory.Add(dtTemp.Rows(indx)("Column_Mandatory"))
            Next
            Dim inputsStrs As List(Of String) = New List(Of String)(inputs)

            Dim isPickCLRInsteadOfSNF As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProcuremntPickCLRInsteadOfSNF, clsFixedParameterCode.MilkProcuremntPickCLRInsteadOfSNF, Nothing)) > 0)

            Dim SettDBFFATDivideBy As Integer = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.DBF, clsFixedParameterCode.FATDivideBy, Nothing))
            Dim SettDBFSNFDivideBy As Integer = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.DBF, clsFixedParameterCode.SNFDivideBy, Nothing))


            Dim currentdate As Date = Date.Today
            Dim ii As Integer = 1
            Dim indxSuccess As Integer = 0
            Dim indxError As Integer = 0
            Dim arr As New Dictionary(Of String, clsMilkShiftUploaderHead)
            Dim flag As Boolean = False
            flag = transportSql.importExcel(True, "", "", gv, inputsStrs.ToArray())

            Dim colSNFOrCLR As String = "SNF"
            If isPickCLRInsteadOfSNF Then
                colSNFOrCLR = "CLR"
            End If
            Dim dtError As New DataTable
            dtError.Columns.Add("Date", GetType(String))
            dtError.Columns.Add("Shift", GetType(String))
            dtError.Columns.Add("Route", GetType(String))
            dtError.Columns.Add("DCSNo", GetType(String))
            dtError.Columns.Add("Qty", GetType(String))
            dtError.Columns.Add("FAT", GetType(String))
            dtError.Columns.Add(colSNFOrCLR, GetType(String))
            dtError.Columns.Add("Error", GetType(String))
            If flag Then
                Try
                    clsCommon.ProgressBarPercentShow()
                    For Each grow As GridViewRowInfo In gv.Rows
                        ii += 1
                        clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (gv.Rows.Count)), "Validating Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(gv.Rows.Count) & "")
                        Try
                            If clsCommon.myLen(grow.Cells(arrColumnName.IndexOf(clsDBFTemplate.DCS)).Value) > 0 Then
                                Dim objtemp As New clsTempFATSNFAmt
                                objtemp.IDate = clsCommon.myCDate(grow.Cells(arrColumnName.IndexOf(clsDBFTemplate.DDate)).Value)
                                objtemp.IShift = IIf(clsCommon.myCDecimal(grow.Cells(arrColumnName.IndexOf(clsDBFTemplate.Shift)).Value) = 0, "M", "E")
                                If arrColumnName.IndexOf(clsDBFTemplate.Route) >= 0 Then
                                    objtemp.BulkRoute = clsCommon.myCstr(grow.Cells(arrColumnName.IndexOf(clsDBFTemplate.Route)).Value)
                                    If clsCommon.myLen(objtemp.BulkRoute) > 0 Then
                                        objtemp.BulkRoute = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ROUTE_NO from TSPL_BULK_ROUTE_MASTER where ROUTE_NO='" + objtemp.BulkRoute + "'"))
                                        If clsCommon.myLen(objtemp.BulkRoute) <= 0 Then
                                            Throw New Exception("Invalid Bulk Route [" + clsCommon.myCstr(grow.Cells(arrColumnName.IndexOf(clsDBFTemplate.Route)).Value) + "]")
                                        End If
                                    End If
                                End If

                                objtemp.VLCUploader = clsCommon.myCstr(grow.Cells(arrColumnName.IndexOf(clsDBFTemplate.DCS)).Value)
                                objtemp.Qty = clsCommon.myCDecimal(grow.Cells(arrColumnName.IndexOf(clsDBFTemplate.Qty)).Value)
                                objtemp.FAT = clsCommon.myCDecimal(grow.Cells(arrColumnName.IndexOf(clsDBFTemplate.FAT)).Value) / SettDBFFATDivideBy
                                objtemp.SNF = clsCommon.myCDecimal(grow.Cells(arrColumnName.IndexOf(clsDBFTemplate.SNF)).Value) / SettDBFSNFDivideBy
                                objtemp.SNF = clsCommon.myRoundOFF(objtemp.SNF, IIf(objCommonVar.MilkProcurementSNF2DecimalPlaces, 2, 1), 4)
                                objtemp.DockCollectionMilkType = "M"
                                If objCommonVar.DisplayTypeInMilkReceipt Then
                                    If Not arrColumnName.Contains(clsDBFTemplate.DockCollectionMilkType) Then
                                        Throw New Exception("Please set " + clsDBFTemplate.DockCollectionMilkType + " in DBF Template")
                                    End If
                                    If clsCommon.myCDecimal(grow.Cells(arrColumnName.IndexOf(clsDBFTemplate.DockCollectionMilkType)).Value) = 2 Then
                                        objtemp.DockCollectionMilkType = "C"
                                    End If
                                End If
                                If arrColumnName.IndexOf(clsDBFTemplate.EmpatyCAN) >= 0 Then
                                    objtemp.QAT = clsCommon.myCDecimal(grow.Cells(arrColumnName.IndexOf(clsDBFTemplate.EmpatyCAN)).Value)
                                End If

                                qry = "select VLC_Code,MCC from TSPL_VLC_MASTER_HEAD where VLC_Code_VLC_Uploader='" + objtemp.VLCUploader + "'"
                                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                                    Throw New Exception("Invalid DCSNo " + objtemp.VLCUploader)
                                End If
                                If dt IsNot Nothing AndAlso dt.Rows.Count > 1 Then
                                    Throw New Exception("DCSNo " + objtemp.VLCUploader + " Mapped more than one MCC/BMC")
                                End If

                                objtemp.VLC = clsCommon.myCstr(dt.Rows(0)("VLC_Code"))
                                objtemp.MCC = clsCommon.myCstr(dt.Rows(0)("MCC"))
                                If clsCommon.myLen(objtemp.MCC) <= 0 Then
                                    Throw New Exception("DCSNo " + objtemp.VLCUploader + " is not mapped with any MCC/BMC")
                                End If

                                Dim UniqueCombination As String = objtemp.MCC + clsCommon.GetPrintDate(objtemp.IDate, "dd/MM/yyyy") + objtemp.IShift
                                If Not arr.ContainsKey(UniqueCombination) Then
                                    qry = "select Document_No from TSPL_MILK_SHIFT_UPLOADER_HEAD where Shift_Date='" + clsCommon.GetPrintDate(objtemp.IDate, "dd/MMM/yyyy") + "' and Shift='" + objtemp.IShift + "' and MCC_Code='" + objtemp.MCC + "'"
                                    dt = clsDBFuncationality.GetDataTable(qry)
                                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                        Throw New Exception("Shift uploader No " + clsCommon.myCstr(dt.Rows(0)("Document_No")) + " is already exist for Date [" + clsCommon.GetPrintDate(objtemp.IDate, "dd/MM/yyyy") + "] Shift [" + objtemp.IShift + "] and MCC [" + objtemp.MCC + "]")
                                    End If

                                    Dim obj As New clsMilkShiftUploaderHead()
                                    obj.Shift_Date = objtemp.IDate
                                    obj.Shift = objtemp.IShift
                                    obj.MCC_Code = objtemp.MCC

                                    obj.Arr = New List(Of clsMilkShiftUploaderDetail)
                                    arr.Add(UniqueCombination, obj)
                                End If

                                Dim objtr As New clsMilkShiftUploaderDetail
                                objtr.SNo = arr(UniqueCombination).Arr.Count + 1
                                objtr.VLC_Code = objtemp.VLC
                                objtr.No_Of_Cans = 1
                                objtr.Milk_Weight = objtemp.Qty
                                objtr.FAT = objtemp.FAT
                                objtr.SNF = objtemp.SNF
                                'objtr.Dock_Collection_Milk_Type = objtemp.IShift
                                objtr.BULK_ROUTE_NO = objtemp.BulkRoute
                                objtr.QAT = IIf(objtemp.QAT = 1, True, False)
                                objtr.Dock_Collection_Milk_Type = objtemp.DockCollectionMilkType
                                arr(UniqueCombination).Arr.Add(objtr)
                                If dtRejctType IsNot Nothing AndAlso dtRejctType.Rows.Count > 0 Then
                                    For Each drRejctType As DataRow In dtRejctType.Rows
                                        If arrColumnName.IndexOf(clsCommon.myCstr(drRejctType("Code")) + "#" + clsDBFTemplate.Qty) >= 0 Then
                                            If clsCommon.myCDecimal(grow.Cells(arrColumnName.IndexOf(clsCommon.myCstr(drRejctType("Code")) + "#" + clsDBFTemplate.Qty)).Value) > 0 Then
                                                objtr = New clsMilkShiftUploaderDetail
                                                objtr.SNo = arr(UniqueCombination).Arr.Count + 1
                                                objtr.Reject_Type = clsCommon.myCstr(drRejctType("Code"))
                                                objtr.VLC_Code = objtemp.VLC
                                                objtr.Dock_Collection_Milk_Type = objtemp.DockCollectionMilkType
                                                objtr.BULK_ROUTE_NO = objtemp.BulkRoute
                                                objtr.No_Of_Cans = 1
                                                objtr.Milk_Weight = clsCommon.myCDecimal(grow.Cells(arrColumnName.IndexOf(clsCommon.myCstr(drRejctType("Code")) + "#" + clsDBFTemplate.Qty)).Value)
                                                If arrColumnName.IndexOf(clsCommon.myCstr(drRejctType("Code")) + "#" + clsDBFTemplate.FAT) >= 0 Then
                                                    objtr.FAT = clsCommon.myCDecimal(grow.Cells(arrColumnName.IndexOf(clsCommon.myCstr(drRejctType("Code")) + "#" + clsDBFTemplate.FAT)).Value) / 10
                                                End If
                                                If arrColumnName.IndexOf(clsCommon.myCstr(drRejctType("Code")) + "#" + clsDBFTemplate.SNF) >= 0 Then
                                                    objtr.SNF = clsCommon.myCDecimal(grow.Cells(arrColumnName.IndexOf(clsCommon.myCstr(drRejctType("Code")) + "#" + clsDBFTemplate.SNF)).Value) / 10
                                                End If
                                                arr(UniqueCombination).Arr.Add(objtr)
                                            End If
                                        End If
                                    Next
                                End If
                                indxSuccess += 1
                            End If
                        Catch ex As Exception
                            indxError += 1
                            Dim dr As DataRow = dtError.NewRow()
                            dr("Date") = clsCommon.myCstr(grow.Cells(arrColumnName.IndexOf(clsDBFTemplate.DDate)).Value)
                            dr("Shift") = IIf(clsCommon.myCDecimal(grow.Cells(arrColumnName.IndexOf(clsDBFTemplate.Shift)).Value) = 0, "M", "E")
                            dr("Route") = clsCommon.myCstr(grow.Cells(arrColumnName.IndexOf(clsDBFTemplate.Route)).Value)
                            dr("DCSNo") = clsCommon.myCstr(grow.Cells(arrColumnName.IndexOf(clsDBFTemplate.DCS)).Value)
                            dr("Qty") = clsCommon.myCstr(grow.Cells(arrColumnName.IndexOf(clsDBFTemplate.Qty)).Value)
                            dr("FAT") = clsCommon.myCstr(clsCommon.myCDecimal(grow.Cells(arrColumnName.IndexOf(clsDBFTemplate.FAT)).Value) / 10)
                            dr(colSNFOrCLR) = clsCommon.myCstr(clsCommon.myCDecimal(grow.Cells(arrColumnName.IndexOf(clsDBFTemplate.SNF)).Value) / 10)
                            dr("Error") = "Error At Row No " + clsCommon.myCstr(ii) + ex.Message
                            dtError.Rows.Add(dr)
                        End Try
                    Next
                    clsCommon.ProgressBarPercentHide()
                Catch ex As Exception
                    clsCommon.ProgressBarPercentHide()
                    clsCommon.MyMessageBoxShow(frm, ex.Message, frm.Text)
                End Try

                Try
                    If dtError.Rows.Count > 0 Then
                        Dim ff As New FrmFreeGrid
                        ff.ReportID = "MilkShiftUploader"
                        ff.Text = "DBF Fill Errors"
                        ff.dt = dtError
                        ff.ShowDialog()
                    ElseIf arr IsNot Nothing AndAlso arr.Count > 0 Then
                        qry = "Valid Row [" + clsCommon.myCstr(indxSuccess) + "]" + Environment.NewLine + " " + Environment.NewLine + "Total Documents To be Generate [" + clsCommon.myCstr(arr.Count) + "]" + Environment.NewLine + "Do You want to Proceed"
                        If clsCommon.MyMessageBoxShow(frm, qry, frm.Text, MessageBoxButtons.YesNo) = DialogResult.Yes Then
                            clsCommon.ProgressBarPercentShow()
                            ii = 0
                            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                            Try
                                For Each key As String In arr.Keys
                                    ii += 1
                                    clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (arr.Count)), "Saving Document..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(arr.Count) & "")
                                    Dim obj As clsMilkShiftUploaderHead = arr.Item(key)
                                    obj.SaveData(obj, True, True, trans)
                                Next
                                trans.Commit()
                            Catch ex As Exception
                                trans.Rollback()
                                Throw New Exception(ex.Message)
                            Finally
                                clsCommon.ProgressBarPercentHide()
                            End Try
                            clsCommon.MyMessageBoxShow(frm, "Data Transfer Completed!", frm.Text, MessageBoxButtons.OK)
                        End If
                    Else
                        Throw New Exception("No Valid Rows Found to Save")
                    End If
                Catch ex As Exception
                    clsCommon.MyMessageBoxShow(frm, ex.Message, frm.Text)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(frm, ex.Message, frm.Text)
        Finally
            frm.Controls.Remove(gv)
        End Try
    End Sub

End Class

Public Class clsMilkShiftUploaderDetail
#Region "Variables"

    Public TR_No As String
    Public Document_No As String
    Public SNo As Integer
    Public VLC_Uploader_Code As String
    Public VLC_Code As String
    Public VLC_Name As String
    Public No_Of_Cans As Integer
    Public Milk_Weight As Decimal
    Public FAT As Decimal
    Public SNF As Decimal
    Public Dock_Collection_Milk_Type As String
    Public Dock_Collection_Milk_Type_Auto As Boolean = True
    Public Reject_Type As String
    Public Reject_Defaulter As String
    Public PageNo As Integer
    Public arrQCParameter As List(Of clsMilkShiftUploaderQCParameterDetail) = Nothing
    Public Against_Milk_Collection_DCS_Detail As Integer
    Public BULK_ROUTE_NO As String = Nothing
    Public QAT As Boolean = False
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal strMCCCode As String, ByVal Arr As List(Of clsMilkShiftUploaderDetail), ByVal trans As SqlTransaction) As Boolean
        Return SaveData(strDocNo, strMCCCode, Arr, trans, "")
    End Function
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal strMCCCode As String, ByVal Arr As List(Of clsMilkShiftUploaderDetail), ByVal trans As SqlTransaction, ByVal strTR_No As String) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim settMaxFATPerLimit As Decimal = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.MaxFATPerLimit, clsFixedParameterCode.MaxFATPerLimit, trans))
            Dim settMaxSNFPerLimit As Decimal = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MaxSNFPerLimit, clsFixedParameterCode.MaxSNFPerLimit, trans))
            Dim isPickCLRInsteadOfSNF As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProcuremntPickCLRInsteadOfSNF, clsFixedParameterCode.MilkProcuremntPickCLRInsteadOfSNF, trans)) > 0)
            Dim qry As String = "select TSPL_MCC_MASTER.Is_Seprate_Dock_Cow_Buffalo from TSPL_MILK_SHIFT_UPLOADER_HEAD left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_SHIFT_UPLOADER_HEAD.MCC_Code where TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No='" + strDocNo + "'"
            Dim Is_Seprate_Dock_Cow_Buffalo As Boolean = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) = 1
            For Each obj As clsMilkShiftUploaderDetail In Arr
                Dim coll As New Hashtable()
                If clsCommon.myLen(strTR_No) > 0 Then
                    obj.TR_No = strTR_No
                Else
                    obj.TR_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select max(TR_No) from TSPL_MILK_SHIFT_UPLOADER_DETAIL where TR_No like '" + strMCCCode + "%'", trans))
                    If clsCommon.myLen(obj.TR_No) <= 0 Then
                        obj.TR_No = strMCCCode + "TR0000000000000000"
                    End If
                    obj.TR_No = clsCommon.incval(obj.TR_No)
                End If

                clsCommon.AddColumnsForChange(coll, "TR_No", obj.TR_No)
                clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "SNo", obj.SNo)
                clsCommon.AddColumnsForChange(coll, "VLC_Code", obj.VLC_Code)
                clsCommon.AddColumnsForChange(coll, "No_Of_Cans", obj.No_Of_Cans)
                clsCommon.AddColumnsForChange(coll, "Milk_Weight", obj.Milk_Weight)
                If settMaxFATPerLimit > 0 Then
                    If obj.FAT > settMaxFATPerLimit Then
                        Throw New Exception("FAT % Can't be more than " + clsCommon.myCstr(settMaxFATPerLimit) + ".")
                    End If
                End If
                If settMaxSNFPerLimit > 0 AndAlso Not isPickCLRInsteadOfSNF Then
                    If obj.SNF > settMaxSNFPerLimit Then
                        Throw New Exception("SNF % Can't be more than " + clsCommon.myCstr(settMaxSNFPerLimit) + ".")
                    End If
                End If
                clsCommon.AddColumnsForChange(coll, "FAT", obj.FAT)
                clsCommon.AddColumnsForChange(coll, "SNF", obj.SNF)
                clsCommon.AddColumnsForChange(coll, "Reject_Type", obj.Reject_Type, True)
                clsCommon.AddColumnsForChange(coll, "Reject_Defaulter", obj.Reject_Defaulter)
                clsCommon.AddColumnsForChange(coll, "PageNo", obj.PageNo)
                clsCommon.AddColumnsForChange(coll, "QAT", IIf(obj.QAT, 1, 0), True)
                clsCommon.AddColumnsForChange(coll, "Against_Milk_Collection_DCS_Detail", obj.Against_Milk_Collection_DCS_Detail, True)
                clsCommon.AddColumnsForChange(coll, "BULK_ROUTE_NO", obj.BULK_ROUTE_NO, True)
                If obj.Dock_Collection_Milk_Type_Auto Then
                    If Not objCommonVar.DisplayTypeInMilkReceipt Then
                        If Is_Seprate_Dock_Cow_Buffalo Then
                            If Not (clsCommon.CompairString(obj.Dock_Collection_Milk_Type, "M") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Dock_Collection_Milk_Type, "C") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Dock_Collection_Milk_Type, "B") = CompairStringResult.Equal) Then
                                Throw New Exception("Milk Type can be B/M or C")
                            End If
                        Else
                            obj.Dock_Collection_Milk_Type = "M"
                        End If
                    End If
                End If
                clsCommon.AddColumnsForChange(coll, "Dock_Collection_Milk_Type", obj.Dock_Collection_Milk_Type)
                If clsCommon.myLen(strTR_No) > 0 Then
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_SHIFT_UPLOADER_DETAIL", OMInsertOrUpdate.Update, "TR_No='" + strTR_No + "'", trans)
                Else
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_SHIFT_UPLOADER_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                End If
                clsMilkShiftUploaderQCParameterDetail.saveData(strDocNo, obj.TR_No, obj.arrQCParameter, trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal strExtraWhrclas As String, ByVal trans As SqlTransaction) As List(Of clsMilkShiftUploaderDetail)
        Dim arr As List(Of clsMilkShiftUploaderDetail) = Nothing
        Dim qry As String = "SELECT TSPL_MILK_SHIFT_UPLOADER_DETAIL.*,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [Uploader_Code] FROM TSPL_MILK_SHIFT_UPLOADER_DETAIL left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_SHIFT_UPLOADER_DETAIL.VLC_Code  where  TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No='" + strPONo + "' "
        If clsCommon.myLen(strExtraWhrclas) > 0 Then
            qry += " and " + strExtraWhrclas
        End If
        qry += " ORDER BY TSPL_MILK_SHIFT_UPLOADER_DETAIL.SNO"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            arr = New List(Of clsMilkShiftUploaderDetail)
            Dim objTr As clsMilkShiftUploaderDetail
            For Each dr As DataRow In dt.Rows
                objTr = New clsMilkShiftUploaderDetail
                objTr.TR_No = clsCommon.myCstr(dr("TR_No"))
                objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                objTr.SNo = clsCommon.myCdbl(dr("SNo"))
                objTr.VLC_Code = clsCommon.myCstr(dr("VLC_Code"))
                objTr.VLC_Name = clsCommon.myCstr(dr("VLC_Name"))
                objTr.No_Of_Cans = clsCommon.myCdbl(dr("No_Of_Cans"))
                objTr.Milk_Weight = clsCommon.myCdbl(dr("Milk_Weight"))
                objTr.FAT = clsCommon.myCdbl(dr("FAT"))
                objTr.SNF = clsCommon.myCdbl(dr("SNF"))
                objTr.Dock_Collection_Milk_Type = clsCommon.myCstr(dr("Dock_Collection_Milk_Type"))
                objTr.Against_Milk_Collection_DCS_Detail = clsCommon.myCdbl(dr("Against_Milk_Collection_DCS_Detail"))
                objTr.BULK_ROUTE_NO = clsCommon.myCstr(dr("BULK_ROUTE_NO"))
                objTr.Reject_Type = clsCommon.myCstr(dr("Reject_Type"))
                objTr.Reject_Defaulter = clsCommon.myCstr(dr("Reject_Defaulter"))
                objTr.VLC_Uploader_Code = clsCommon.myCstr(dr("Uploader_Code"))
                objTr.QAT = IIf(clsCommon.myCDecimal(dr("QAT")) = 1, True, False)
                arr.Add(objTr)
            Next
        End If
        Return arr
    End Function
End Class

Public Class clsMilkShiftUploaderQCParameterDetail
    Public Document_No As String = String.Empty
    Public TR_No As String = String.Empty
    Public Sample_No As Integer = 0
    Public Param_Field_Code As String = String.Empty
    Public Param_Field_Desc As String = String.Empty
    Public Param_Field_Value As String = String.Empty
    Public Param_Type As String = String.Empty

    Public Shared Function saveData(ByVal strQCNo As String, ByVal strTRCode As String, ByVal arrObj As List(Of clsMilkShiftUploaderQCParameterDetail)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            saveData(strQCNo, strTRCode, arrObj, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function saveData(ByVal strQCNo As String, ByVal strTRCode As String, ByVal arrObj As List(Of clsMilkShiftUploaderQCParameterDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim coll As Hashtable
            If arrObj IsNot Nothing Then
                For Each obj As clsMilkShiftUploaderQCParameterDetail In arrObj
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_No", strQCNo)
                    clsCommon.AddColumnsForChange(coll, "TR_No", strTRCode)
                    clsCommon.AddColumnsForChange(coll, "Param_Field_Code", obj.Param_Field_Code)
                    clsCommon.AddColumnsForChange(coll, "Param_Field_Desc", obj.Param_Field_Desc)
                    clsCommon.AddColumnsForChange(coll, "Param_Field_Value", obj.Param_Field_Value)
                    clsCommon.AddColumnsForChange(coll, "Param_Type", obj.Param_Type)
                    clsCommon.AddColumnsForChange(coll, "Sample_No", obj.Sample_No)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_SHIFT_UPLOADER_QC_PARAMETER_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function getData(ByVal strQCNo As String, ByVal trans As SqlTransaction, ByVal intMilkProcSRNo As Integer) As List(Of clsMilkShiftUploaderQCParameterDetail)
        Dim arrObj As List(Of clsMilkShiftUploaderQCParameterDetail) = Nothing
        Try
            Dim obj As clsMilkShiftUploaderQCParameterDetail = Nothing
            Dim qry As String = "select * from TSPL_MILK_SHIFT_UPLOADER_QC_PARAMETER_DETAIL where Document_No='" & strQCNo & "'"
            If intMilkProcSRNo > 0 Then
                qry += " and Sample_No='" + clsCommon.myCstr(intMilkProcSRNo) + "'"
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of clsMilkShiftUploaderQCParameterDetail)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New clsMilkShiftUploaderQCParameterDetail()
                    obj.Document_No = clsCommon.myCstr(dt.Rows(i)("Document_No"))
                    obj.TR_No = clsCommon.myCstr(dt.Rows(i)("TR_No"))
                    obj.Param_Field_Code = clsCommon.myCstr(dt.Rows(i)("Param_Field_Code"))
                    obj.Param_Field_Desc = clsCommon.myCstr(dt.Rows(i)("Param_Field_Desc"))
                    obj.Param_Field_Value = clsCommon.myCstr(dt.Rows(i)("Param_Field_Value"))
                    obj.Param_Type = clsCommon.myCstr(dt.Rows(i)("Param_Type"))
                    obj.Sample_No = clsCommon.myCdbl(dt.Rows(i)("Sample_No"))
                    arrObj.Add(obj)

                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return arrObj
    End Function

    Public Shared Function DeleteRowData(ByVal strTR_No As String) As Boolean
        If (clsCommon.myLen(strTR_No) <= 0) Then
            Throw New Exception("Row Not selected.")
        End If
        Dim Tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim strQry As String = "select Document_No from TSPL_MILK_SHIFT_UPLOADER_DETAIL where TR_No = '" + strTR_No + "'"
            Dim strDocNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQry, Tran))
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Invalid TR No [" + strTR_No + "]")
            End If

            strQry = "delete From TSPL_MILK_SHIFT_UPLOADER_DETAIL Where TR_No = '" + strTR_No + "'"
            clsDBFuncationality.ExecuteNonQuery(strQry, Tran)

            strQry = "update TSPL_MILK_SHIFT_UPLOADER_DETAIL set sno=xx.NewSNO from (
select ROW_NUMBER() over (order by sno) as NewSNO,TR_No  from TSPL_MILK_SHIFT_UPLOADER_DETAIL where Document_No='A10000020000000167'  
)xx inner join TSPL_MILK_SHIFT_UPLOADER_DETAIL on TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No=xx.TR_No"
            clsDBFuncationality.ExecuteNonQuery(strQry, Tran)

            Tran.Commit()
        Catch ex As Exception
            Tran.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class