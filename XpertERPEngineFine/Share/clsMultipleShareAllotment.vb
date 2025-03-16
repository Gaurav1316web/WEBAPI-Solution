Imports System.Data.SqlClient
Imports common

Public Class clsMultipleShareAllotment
#Region "Variables"
    Public Document_No As String = Nothing
    Public Document_date As Date? = Nothing
    Public Rate_Of_One_Share As Decimal
    Public From_Date As Date? = Nothing
    Public To_Date As Date? = Nothing
    Public Status As Integer = 0
    Public Arr As List(Of clsMultipleShareAllotmentDetail) = Nothing
    Public ArrAPInvoiceDetails As List(Of clsMultipleShareAllotmentAPInvoiceDetail) = Nothing
#End Region
    Public Function SaveData(ByVal obj As clsMultipleShareAllotment, ByVal isNewEntry As Boolean, ByVal strTransType As String, ByVal AutoSave As Boolean) As Boolean
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

    Public Function SaveData(ByVal obj As clsMultipleShareAllotment, ByVal isNewEntry As Boolean, ByVal strTransType As String, ByVal trans As SqlTransaction, ByVal AutoSave As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "delete from TSPL_MULTIPLE_SHARE_ALLOTMENT_DETAIL where Document_No='" + obj.Document_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_MULTIPLE_SHARE_ALLOTMENT_AP_INVOICE_DETAIL where Document_No='" + obj.Document_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_date", clsCommon.GetPrintDate(obj.Document_date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "From_Date", clsCommon.GetPrintDate(obj.From_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "To_Date", clsCommon.GetPrintDate(obj.To_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Rate_Of_One_Share", obj.Rate_Of_One_Share)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_date, clsDocType.MultipleShareAllotment, "", "")
                If (clsCommon.myLen(obj.Document_No) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD", OMInsertOrUpdate.Update, "TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD.Document_No='" + obj.Document_No + "'", trans)
            End If
            isSaved = isSaved AndAlso clsMultipleShareAllotmentDetail.SaveData(obj.Document_No, obj.Arr, trans)

        Catch err As Exception

            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strRetCode As String, ByVal NavType As NavigatorType) As clsMultipleShareAllotment
        Return GetData(strRetCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsMultipleShareAllotment
        Dim obj As clsMultipleShareAllotment = Nothing
        Dim qry As String = "select Document_No , Document_date,Rate_Of_One_Share  ,ISNULL( Status,0) as Status,From_Date,To_Date from TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD.Document_No = (select MIN(Document_No) from TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD)"
            Case NavigatorType.Last
                qry += " and TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD.Document_No = (select Max(Document_No) from TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD)"
            Case NavigatorType.Next
                qry += " and TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD.Document_No = (select Min(Document_No) from TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD where Document_No >'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD.Document_No = (select Max(Document_No) from TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD where Document_No <'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD.Document_No = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsMultipleShareAllotment()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Rate_Of_One_Share = clsCommon.myCdbl(dt.Rows(0)("Rate_Of_One_Share"))
            obj.Document_date = clsCommon.myCDate(dt.Rows(0)("Document_date"))
            obj.From_Date = clsCommon.myCDate(dt.Rows(0)("From_Date"))
            obj.To_Date = clsCommon.myCDate(dt.Rows(0)("To_Date"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.Arr = clsMultipleShareAllotmentDetail.GetData(obj.Document_No, trans)
            obj.ArrAPInvoiceDetails = clsMultipleShareAllotmentAPInvoiceDetail.GetData(obj.Document_No, 0, True, trans)
        End If
        Return obj
    End Function

    Public Shared Function getFinder(ByVal strCode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim sql As String = "select Document_No as DocumentNo ,convert(varchar(12),Document_date,103) as DocumentDate,From_Date as [From Date],To_Date as [To Date],Rate_Of_One_Share as [Rate of One Share],case when Status = 1 then 'posted' else 'Unposted' end as Posted from TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD"
        str = clsCommon.ShowSelectForm("HeadLoad", sql, "DocumentNo", "", strCode, "DocumentNo", isButtonClicked)
        Return str
    End Function

    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean
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
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Docume nt No not found to Post")
            End If
            Dim obj As clsMultipleShareAllotment = clsMultipleShareAllotment.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = 1) Then
                Throw New Exception("Already Posted")
            End If

            Dim objShareMaster As New ClsShareMaster
            objShareMaster.IDate = obj.Document_date
            objShareMaster.Name = "Auto Share Capital"
            objShareMaster.CertificateFrom = obj.Arr(0).Share_Certificate_From
            objShareMaster.CertificateTo = obj.Arr(obj.Arr.Count - 1).Share_Certificate_To
            objShareMaster.Shares = objShareMaster.CertificateTo
            objShareMaster.Rate = obj.Rate_Of_One_Share
            objShareMaster.Amount = objShareMaster.Rate * objShareMaster.Shares
            ClsShareMaster.SaveData(objShareMaster, True, trans)

            For Each objtr As clsMultipleShareAllotmentDetail In obj.Arr
                Dim objShareAllotment As New clsShareAllotment
                objShareAllotment.Share_Code = objShareMaster.Code
                objShareAllotment.DCS_Code = objtr.Vendor_Code
                objShareAllotment.Name = "Auto Share Allotment "
                objShareAllotment.IDate = obj.Document_date
                objShareAllotment.Qty = objtr.Allocated_Share
                objShareAllotment.Rate = objtr.Rate_Per_Share
                objShareAllotment.Amount = objShareAllotment.Rate * objShareAllotment.Qty
                clsShareAllotment.SaveData(objShareAllotment, True, trans)
            Next

            clsDBFuncationality.ExecuteNonQuery("Update TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD set Status= 1, Posted_By = '" + objCommonVar.CurrentUserCode + "',Posted_Date = '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt") + "'  where Document_No='" & obj.Document_No & "'", trans)

        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
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

    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim isResponse As Boolean = True
        Try

            Dim obj As clsMultipleShareAllotment = clsMultipleShareAllotment.GetData(strCode, NavigatorType.Current, trans)
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
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD", OMInsertOrUpdate.Update, "Document_No='" + obj.Document_No + "'", trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isResponse
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
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
    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim obj As clsMultipleShareAllotment = clsMultipleShareAllotment.GetData(strCode, NavigatorType.Current, trans)
        Try
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Document No not found to Delete")
            End If
            If clsCommon.CompairString(obj.Status, "1") = CompairStringResult.Equal Then
                Throw New Exception("Already Posted")
            End If
            Dim qry As String = Nothing
            qry = "delete from TSPL_MULTIPLE_SHARE_ALLOTMENT_AP_INVOICE_DETAIL where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_MULTIPLE_SHARE_ALLOTMENT_DETAIL where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetGridData(ByVal arrBMC As List(Of String), ByVal FromDate As Date, ToDate As Date, ByVal trans As SqlTransaction) As clsMultipleShareAllotment
        Dim obj As clsMultipleShareAllotment = Nothing
        Dim qry As String = ""
        Dim Baseqry As String = ""
        Baseqry = " select convert(varchar,TSPL_VENDOR_INVOICE_HEAD.Posting_Date,103) as Date,TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader, TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE,TSPL_VLC_MASTER_HEAD.VLC_Name, (TSPL_VENDOR_INVOICE_HEAD.Document_Total) as Deducted_Amt
						from TSPL_PAYMENT_PROCESS_DEDUCTION left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE inner join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No 
						left outer join TSPL_DCS_ADDITION_DEDUCTION  on TSPL_DCS_ADDITION_DEDUCTION.Code = TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_VLC_MASTER_HEAD.MCC
						where  TSPL_DCS_ADDITION_DEDUCTION.IsShare = 1 and TSPL_VENDOR_INVOICE_HEAD.Posting_Date >= convert(date,'" & clsCommon.GetPrintDate(FromDate, "dd/MMM/yyyy") & "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Posting_Date,103) < = '" & clsCommon.GetPrintDate(ToDate, "dd/MMM/yyyy") & "' "

        If arrBMC.Count > 0 Then
            Baseqry += " and TSPL_VLC_MASTER_HEAD.MCC in ( " & clsCommon.GetMulcallString(arrBMC) & " ) "
        End If
        qry = "select VLC_Code_VLC_Uploader,xxx.Vendor_Code,VLC_Name,case when isnull(tab2.Opening_Amt,0) = 0 then 0 else tab2.Opening_Amt end as Opening_Amt,Deducted_Amt from ( select VLC_Code_VLC_Uploader,max(Vendor_CODE)Vendor_CODE,max(VLC_Name)VLC_Name,0 as Opening_Amt,sum(Deducted_Amt)Deducted_Amt,max(Date)Date from ( " & Baseqry & " )xx group by VLC_Code_VLC_Uploader "
        qry += " )xxx outer APPLY ( SELECT TOP 1 TSPL_MULTIPLE_SHARE_ALLOTMENT_DETAIL.Vendor_Code, TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD.To_Date, TSPL_MULTIPLE_SHARE_ALLOTMENT_DETAIL.Balance_Amt as Opening_Amt,TSPL_MULTIPLE_SHARE_ALLOTMENT_DETAIL.Share_Deducted_Amt,TSPL_MULTIPLE_SHARE_ALLOTMENT_DETAIL.Balance_Amt FROM TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD LEFT outer JOIN  
                TSPL_MULTIPLE_SHARE_ALLOTMENT_DETAIL ON TSPL_MULTIPLE_SHARE_ALLOTMENT_DETAIL.Document_No = TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD.Document_No WHERE TSPL_MULTIPLE_SHARE_ALLOTMENT_DETAIL.Vendor_Code = xxx.Vendor_CODE  
			    and convert(date, TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD.To_Date,103) <=  convert(date,'" & clsCommon.GetPrintDate(FromDate, "dd/MMM/yyyy") & "',103) ORDER BY TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD.To_Date DESC  ) AS tab2 order  by Date, VLC_Code_VLC_Uploader "
        Baseqry = " select ROW_NUMBER( ) over( order by Date,VLC_Code_VLC_Uploader) as SNo, Date,AP_Invoice_No AS [AP Invoice No],VLC_Code_VLC_Uploader as [DCS Code],VLC_Name as [DCS Name],Deducted_Amt as [Balance Amount] from ( " & Baseqry & " )x order by date"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Dim dtDetails As DataTable = clsDBFuncationality.GetDataTable(Baseqry)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim objtr = New clsMultipleShareAllotmentDetail()
            objtr.VLC_Code_VLC_Uploader = clsCommon.myCdbl(dt.Rows(0)("VLC_Code_VLC_Uploader"))
            objtr.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
            objtr.VLC_Name = clsCommon.myCstr(dt.Rows(0)("VLC_Name"))
            objtr.Share_Opening_Amt = clsCommon.myCdbl(dt.Rows(0)("Opening_Amt"))
            objtr.Share_Deducted_Amt = clsCommon.myCdbl(dt.Rows(0)("Deducted_Amt"))
        End If
        If dtDetails IsNot Nothing AndAlso dtDetails.Rows.Count > 0 Then
            obj.ArrAPInvoiceDetails = New List(Of clsMultipleShareAllotmentAPInvoiceDetail)()
            For Each dr As DataRow In dtDetails.Rows
                Dim objInvoice As clsMultipleShareAllotmentAPInvoiceDetail = New clsMultipleShareAllotmentAPInvoiceDetail()
                objInvoice.AP_Date = clsCommon.myCstr(dr("Date"))
                objInvoice.VLC_Code_VLC_Uploader = clsCommon.myCstr(dr("DCS Code"))
                objInvoice.VLC_Name = clsCommon.myCstr(dr("DCS Name"))
                objInvoice.AP_Invoice_No = clsCommon.myCstr(dr("AP_Invoice_No"))
                objInvoice.Balance_Amt = clsCommon.myCdbl(dr("Balance_Amt"))
                objInvoice.Used_Amt = clsCommon.myCdbl(dr("Balance_Amt"))
                obj.ArrAPInvoiceDetails.Add(objInvoice)
            Next
        End If
        Return obj
    End Function

End Class

Public Class clsMultipleShareAllotmentDetail

#Region "Variables"
    Public Document_No As String = Nothing
    Public Vendor_Code As String = Nothing
    Public VLC_Code_VLC_Uploader As String = Nothing
    Public VLC_Name As String = Nothing
    Public Share_Opening_Amt As Decimal
    Public Share_Deducted_Amt As Decimal
    Public Balance_Amt As Decimal
    Public Rate_Per_Share As Decimal
    Public Allocated_Share As Decimal
    Public Share_Certificate_From As Decimal
    Public Share_Certificate_To As Decimal
    Public ArrAPInvoiceAllDetails As List(Of clsMultipleShareAllotmentAPInvoiceDetail) = Nothing

#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsMultipleShareAllotmentDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsMultipleShareAllotmentDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", strCode)
                clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
                clsCommon.AddColumnsForChange(coll, "Share_Opening_Amt", obj.Share_Opening_Amt)
                clsCommon.AddColumnsForChange(coll, "Share_Deducted_Amt", obj.Share_Deducted_Amt)
                clsCommon.AddColumnsForChange(coll, "Balance_Amt", obj.Balance_Amt)
                clsCommon.AddColumnsForChange(coll, "Rate_Per_Share", obj.Rate_Per_Share)
                clsCommon.AddColumnsForChange(coll, "Allocated_Share", obj.Allocated_Share)
                clsCommon.AddColumnsForChange(coll, "Share_Certificate_From", obj.Share_Certificate_From)
                clsCommon.AddColumnsForChange(coll, "Share_Certificate_To", obj.Share_Certificate_To)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MULTIPLE_SHARE_ALLOTMENT_DETAIL", OMInsertOrUpdate.Insert, "", trans)

                Dim PK_ID As Integer = clsERPFuncationality.GetScopeIdentityValue(trans)
                clsMultipleShareAllotmentAPInvoiceDetail.SaveData(strCode, PK_ID, obj.ArrAPInvoiceAllDetails, trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsMultipleShareAllotmentDetail)
        Dim arr As List(Of clsMultipleShareAllotmentDetail) = Nothing
        Dim qry As String = "select TSPL_MULTIPLE_SHARE_ALLOTMENT_DETAIL.*, TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader , TSPL_VLC_MASTER_HEAD.VLC_Name  
         from TSPL_MULTIPLE_SHARE_ALLOTMENT_DETAIL left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_MULTIPLE_SHARE_ALLOTMENT_DETAIL.Vendor_Code
         left  join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_VLC_MASTER_HEAD.MCC where  TSPL_MULTIPLE_SHARE_ALLOTMENT_DETAIL.Document_No = '" + strCode + "' order by Document_No,PK_ID "
        Dim PK_ID As Integer = 0
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsMultipleShareAllotmentDetail)()
            For Each dr As DataRow In dt.Rows
                Dim obj As clsMultipleShareAllotmentDetail = New clsMultipleShareAllotmentDetail()
                obj.Document_No = clsCommon.myCstr(dr("Document_No"))
                obj.VLC_Code_VLC_Uploader = clsCommon.myCstr(dr("VLC_Code_VLC_Uploader"))
                obj.Vendor_Code = clsCommon.myCstr(dr("Vendor_Code"))
                obj.VLC_Name = clsCommon.myCstr(dr("VLC_Name"))
                obj.Share_Opening_Amt = clsCommon.myCdbl(dr("Share_Opening_Amt"))
                obj.Share_Deducted_Amt = clsCommon.myCdbl(dr("Share_Deducted_Amt"))
                obj.Balance_Amt = clsCommon.myCdbl(dr("Balance_Amt"))
                obj.Rate_Per_Share = clsCommon.myCdbl(dr("Rate_Per_Share"))
                obj.Allocated_Share = clsCommon.myCdbl(dr("Allocated_Share"))
                obj.Share_Certificate_From = clsCommon.myCdbl(dr("Share_Certificate_From"))
                obj.Share_Certificate_To = clsCommon.myCdbl(dr("Share_Certificate_To"))
                PK_ID = clsCommon.myCdbl(dr("PK_ID"))
                obj.ArrAPInvoiceAllDetails = clsMultipleShareAllotmentAPInvoiceDetail.GetData(strCode, PK_ID, False, trans)
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function


End Class

Public Class clsMultipleShareAllotmentAPInvoiceDetail
#Region "Variables"
    Public Document_No As String = Nothing
    Public Ref_PK_ID As Integer
    Public AP_Date As Date? = Nothing
    Public VLC_Code_VLC_Uploader As String = Nothing
    Public VLC_Name As String = Nothing
    Public AP_Invoice_No As String = Nothing
    Public Balance_Amt As Decimal
    Public Used_Amt As Decimal

#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal PK_ID As Integer, ByVal Arr As List(Of clsMultipleShareAllotmentAPInvoiceDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsMultipleShareAllotmentAPInvoiceDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", strCode)
                clsCommon.AddColumnsForChange(coll, "Ref_PK_ID", PK_ID)
                clsCommon.AddColumnsForChange(coll, "AP_Invoice_No", obj.AP_Invoice_No)
                clsCommon.AddColumnsForChange(coll, "Used_Amt", obj.Used_Amt)
                clsCommon.AddColumnsForChange(coll, "Balance_Amt", obj.Balance_Amt)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MULTIPLE_SHARE_ALLOTMENT_AP_INVOICE_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal Ref_PK_ID As Integer, ByVal LoadAllData As Boolean, ByVal trans As SqlTransaction) As List(Of clsMultipleShareAllotmentAPInvoiceDetail)
        Dim arr As List(Of clsMultipleShareAllotmentAPInvoiceDetail) = Nothing

        Dim qry As String = "select TSPL_MULTIPLE_SHARE_ALLOTMENT_AP_INVOICE_DETAIL.Ref_PK_ID, TSPL_VENDOR_INVOICE_HEAD.Posting_Date,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_MULTIPLE_SHARE_ALLOTMENT_AP_INVOICE_DETAIL.AP_Invoice_No,TSPL_MULTIPLE_SHARE_ALLOTMENT_AP_INVOICE_DETAIL.Balance_Amt,TSPL_MULTIPLE_SHARE_ALLOTMENT_AP_INVOICE_DETAIL.Used_Amt from TSPL_MULTIPLE_SHARE_ALLOTMENT_AP_INVOICE_DETAIL  
        inner join TSPL_MULTIPLE_SHARE_ALLOTMENT_DETAIL on TSPL_MULTIPLE_SHARE_ALLOTMENT_DETAIL.PK_Id = TSPL_MULTIPLE_SHARE_ALLOTMENT_AP_INVOICE_DETAIL.Ref_PK_ID left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_MULTIPLE_SHARE_ALLOTMENT_DETAIL.Vendor_Code
         inner join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_MULTIPLE_SHARE_ALLOTMENT_AP_INVOICE_DETAIL.AP_Invoice_No where  TSPL_MULTIPLE_SHARE_ALLOTMENT_AP_INVOICE_DETAIL.Document_No = '" + strCode + "' "

        If Not LoadAllData Then
            qry += " and Ref_PK_ID = " + clsCommon.myCstr(Ref_PK_ID) + " "
        End If
        qry += " order by TSPL_MULTIPLE_SHARE_ALLOTMENT_DETAIL.Document_No, TSPL_MULTIPLE_SHARE_ALLOTMENT_DETAIL.PK_ID "

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsMultipleShareAllotmentAPInvoiceDetail)()
            For Each dr As DataRow In dt.Rows
                Dim obj As clsMultipleShareAllotmentAPInvoiceDetail = New clsMultipleShareAllotmentAPInvoiceDetail()
                obj.Document_No = strCode
                obj.VLC_Code_VLC_Uploader = clsCommon.myCstr(dr("VLC_Code_VLC_Uploader"))
                obj.VLC_Name = clsCommon.myCstr(dr("VLC_Name"))
                obj.AP_Date = clsCommon.myCstr(dr("Posting_Date"))
                obj.AP_Invoice_No = clsCommon.myCstr(dr("AP_Invoice_No"))
                obj.Balance_Amt = clsCommon.myCdbl(dr("Balance_Amt"))
                obj.Used_Amt = clsCommon.myCdbl(dr("Used_Amt"))
                obj.Ref_PK_ID = clsCommon.myCdbl(dr("Ref_PK_ID"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function

End Class


