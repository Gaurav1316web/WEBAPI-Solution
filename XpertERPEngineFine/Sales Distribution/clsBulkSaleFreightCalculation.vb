Imports System.Data.SqlClient
Imports common

Public Class clsBulkSaleFreightCalculation

#Region "Variables"

    Public Document_Code As String = Nothing
    Public From_Date As Date? = Nothing
    Public To_Date As Date? = Nothing
    Public Document_date As Date? = Nothing
    Public Status As Integer = 0
    Public Total_Amt As Decimal = 0
    Public Customer_Code As String = Nothing
    Public Arr As List(Of clsBulkSaleFreightCalculationDetail) = Nothing
#End Region
    Public Function SaveData(ByVal obj As clsBulkSaleFreightCalculation, ByVal isNewEntry As Boolean, ByVal strTransType As String, ByVal AutoSave As Boolean) As Boolean
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

    Public Function SaveData(ByVal obj As clsBulkSaleFreightCalculation, ByVal isNewEntry As Boolean, ByVal strTransType As String, ByVal trans As SqlTransaction, ByVal AutoSave As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "delete from TSPL_BLK_FREIGHT_CALC_DETAIL where Document_Code='" + obj.Document_Code + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_date", clsCommon.GetPrintDate(obj.Document_date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Customer_Code", clsCommon.myCstr(obj.Customer_Code))
            clsCommon.AddColumnsForChange(coll, "Total_Amt", clsCommon.myCdbl(obj.Total_Amt))
            clsCommon.AddColumnsForChange(coll, "From_Date", clsCommon.GetPrintDate(obj.From_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "To_Date", clsCommon.GetPrintDate(obj.To_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            If isNewEntry Then
                If clsCommon.CompairString(AutoSave, False) = CompairStringResult.Equal Then
                    obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_date, clsDocType.BulkSaleFreightMaster, "", "")
                End If
                If (clsCommon.myLen(obj.Document_Code) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BLK_FREIGHT_CALC_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BLK_FREIGHT_CALC_HEAD", OMInsertOrUpdate.Update, "TSPL_BLK_FREIGHT_CALC_HEAD.Document_Code='" + obj.Document_Code + "'", trans)
            End If
            isSaved = isSaved AndAlso clsBulkSaleFreightCalculationDetail.SaveData(obj.Document_Code, obj.Arr, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_Code, "TSPL_BLK_FREIGHT_CALC_HEAD", "Document_Code", "TSPL_BLK_FREIGHT_CALC_DETAIL", "Document_Code", trans)

        Catch err As Exception

            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strRetCode As String, ByVal NavType As NavigatorType, ByVal TransType As String) As clsBulkSaleFreightCalculation
        Return GetData(strRetCode, NavType, TransType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal TransType As String, ByVal trans As SqlTransaction) As clsBulkSaleFreightCalculation
        Dim obj As clsBulkSaleFreightCalculation = Nothing
        Dim qry As String = "select Document_Code,Total_Amt ,Customer_Code, Document_date,From_Date as 'From Date',To_Date as 'To Date' ,ISNULL( Status,0) as Status from TSPL_BLK_FREIGHT_CALC_HEAD where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_BLK_FREIGHT_CALC_HEAD.Document_Code = (select MIN(Document_Code) from TSPL_BLK_FREIGHT_CALC_HEAD)"
            Case NavigatorType.Last
                qry += " and TSPL_BLK_FREIGHT_CALC_HEAD.Document_Code = (select Max(Document_Code) from TSPL_BLK_FREIGHT_CALC_HEAD)"
            Case NavigatorType.Next
                qry += " and TSPL_BLK_FREIGHT_CALC_HEAD.Document_Code = (select Min(Document_Code) from TSPL_BLK_FREIGHT_CALC_HEAD where Document_Code >'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_BLK_FREIGHT_CALC_HEAD.Document_Code = (select Max(Document_Code) from TSPL_BLK_FREIGHT_CALC_HEAD where Document_Code <'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_BLK_FREIGHT_CALC_HEAD.Document_Code = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsBulkSaleFreightCalculation()
            obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
            obj.Customer_Code = clsCommon.myCstr(dt.Rows(0)("Customer_Code"))
            obj.Document_date = clsCommon.myCDate(dt.Rows(0)("Document_date"))
            obj.From_Date = clsCommon.myCDate(dt.Rows(0)("From Date"))
            obj.To_Date = clsCommon.myCDate(dt.Rows(0)("To Date"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.Total_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Amt"))

            qry = "select *  from TSPL_BLK_FREIGHT_CALC_DETAIL where Document_Code='" + obj.Document_Code + "' order by SNo "
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsBulkSaleFreightCalculationDetail)
                For Each dr As DataRow In dt.Rows
                    Dim objtr As New clsBulkSaleFreightCalculationDetail
                    objtr.SNO = clsCommon.myCdbl(dr("SNO"))
                    objtr.Document_Code = clsCommon.myCstr(dr("Document_Code"))
                    objtr.Dispatch_Date = clsCommon.myCDate(dr("Dispatch_Date"))
                    objtr.Bulk_Dispatch_Document = clsCommon.myCstr(dr("Bulk_Dispatch_Document"))
                    objtr.Bulk_Dispatch_Tanker = clsCommon.myCstr(dr("Bulk_Dispatch_Tanker"))
                    objtr.Bulk_Dispatch_Transporter = clsCommon.myCstr(dr("Bulk_Dispatch_Transporter"))
                    objtr.Ack_Qty = clsCommon.myCdbl(dr("Ack_Qty"))
                    objtr.Ack_Fat = clsCommon.myCDecimal(dr("Ack_Fat"))
                    objtr.Ack_Snf = clsCommon.myCDecimal(dr("Ack_Snf"))
                    objtr.Tender_Qty = clsCommon.myCdbl(dr("Tender_Qty"))
                    objtr.Rate = clsCommon.myCDecimal(dr("Rate"))
                    objtr.Pro_Rate = clsCommon.myCDecimal(dr("Pro_Rate"))
                    objtr.DieselPetrol = clsCommon.myCDecimal(dr("DieselPetrol"))
                    objtr.Applicable_Rate = clsCommon.myCDecimal(dr("Applicable_Rate"))
                    objtr.GPS_KM = clsCommon.myCDecimal(dr("GPS_KM"))
                    objtr.Payable_Amount = clsCommon.myCDecimal(dr("Payable_Amount"))
                    obj.Arr.Add(objtr)
                Next
            End If
        End If


        Return obj
    End Function

    Public Shared Function getFinder(ByVal strCode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim sql As String = "select Document_Code as DocumentNo ,convert(varchar(12),From_Date,103) AS [From Date],convert(varchar(12),To_Date,103) AS [To Date],convert(varchar(12),Document_date,103) as DocumentDate,case when Status = 1 then 'Posted' else 'Unposted' end as Posted from TSPL_BLK_FREIGHT_CALC_HEAD"
        str = clsCommon.ShowSelectForm("BulkSaleFreightMaster", sql, "DocumentNo", "", strCode, "DocumentNo", isButtonClicked)
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
            Dim obj As clsBulkSaleFreightCalculation = clsBulkSaleFreightCalculation.GetData(strDocNo, NavigatorType.Current, "", trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = 1) Then
                Throw New Exception("Already Posted")
            End If

            clsDBFuncationality.ExecuteNonQuery("Update TSPL_BLK_FREIGHT_CALC_HEAD set Status= 1, Posted_By = '" + objCommonVar.CurrentUserCode + "',Posted_Date = '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt") + "'  where Document_Code='" & obj.Document_Code & "'", trans)

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

            Dim obj As clsBulkSaleFreightCalculation = clsBulkSaleFreightCalculation.GetData(strCode, NavigatorType.Current, Nothing, trans)
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
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BLK_FREIGHT_CALC_HEAD", OMInsertOrUpdate.Update, "Document_Code='" + obj.Document_Code + "'", trans)

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
        Dim obj As clsBulkSaleFreightCalculation = clsBulkSaleFreightCalculation.GetData(strCode, NavigatorType.Current, "", trans)
        Try
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("Document No not found to Delete")
            End If
            If clsCommon.CompairString(obj.Status, "1") = CompairStringResult.Equal Then
                Throw New Exception("Already Posted")
            End If
            Dim qry As String = Nothing
            qry = "delete from TSPL_BLK_FREIGHT_CALC_DETAIL where Document_Code='" + obj.Document_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_BLK_FREIGHT_CALC_HEAD where Document_Code='" + obj.Document_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function LoadDispatchAcknowledgeData(ByVal FromDate As String, ByVal ToDate As String, ByVal Customer As String) As DataTable
        Dim dt As DataTable = New DataTable()
        Try
            Dim qry As String = ""
            qry = "SELECT ROW_NUMBER() Over (Order By (Document_Date)) As SNo,  xx.Date,xx.Tanker_No,xx.Transporter,xx.Dispatch_No, xx.Qty, xx.Fat, xx.Snf,isnull(tab2.Tender_Qty,0)Tender_Qty,
            isnull(tab2.Rate,0)Rate,isnull(tab2.Pro_Rate,0)Pro_Rate,isnull(tab2.DieselPetrol,0)DieselPetrol,isnull(tab2.Applicable_Rate,0)Applicable_Rate,isnull(tab2.GPS_KM,0)GPS_KM ,isnull(tab2.Payable_Amount,0)Payable_Amount
            FROM ( SELECT  CONVERT(VARCHAR, TSPL_BULK_SALE_ACKNOWLEDGEMENT.Document_Date, 103) AS Date,TSPL_Dispatch_BulkSale.Tanker_Code AS Tanker_No,	TSPL_Dispatch_BulkSale.Transporter as Transporter,TSPL_Dispatch_BulkSale.Document_No AS Dispatch_No,
            ISNULL(TSPL_BULK_SALE_ACKNOWLEDGEMENT.Qty, 0) AS Qty,ISNULL(TSPL_BULK_SALE_ACKNOWLEDGEMENT.FAT, 0) AS Fat,ISNULL(TSPL_BULK_SALE_ACKNOWLEDGEMENT.SNF, 0) AS Snf,TSPL_Dispatch_BulkSale.Customer_Code,TSPL_BULK_SALE_ACKNOWLEDGEMENT.Document_Date
            FROM TSPL_BULK_SALE_ACKNOWLEDGEMENT LEFT OUTER JOIN TSPL_Dispatch_BulkSale ON TSPL_Dispatch_BulkSale.Document_No = TSPL_BULK_SALE_ACKNOWLEDGEMENT.Bulk_Dispatch_Document
            LEFT OUTER JOIN TSPL_Dispatch_Detail_BulkSale ON TSPL_Dispatch_BulkSale.Document_No = TSPL_Dispatch_Detail_BulkSale.Document_No where TSPL_BULK_SALE_ACKNOWLEDGEMENT.Status = 1 ) AS xx
            cross APPLY ( SELECT TOP 1 TSPL_BLK_FREIGHT_MASTER.Start_Date, TSPL_BLK_FREIGHT_detail.Tender_Qty,TSPL_BLK_FREIGHT_detail.Rate,TSPL_BLK_FREIGHT_detail.DieselPetrol, TSPL_BLK_FREIGHT_detail.Payable_Amount, TSPL_BLK_FREIGHT_detail.Applicable_Rate,
            TSPL_BLK_FREIGHT_detail.Pro_Rate,TSPL_BLK_FREIGHT_detail.GPS_KM  FROM TSPL_BLK_FREIGHT_MASTER LEFT outer JOIN  TSPL_BLK_FREIGHT_detail ON TSPL_BLK_FREIGHT_detail.Document_Code = TSPL_BLK_FREIGHT_MASTER.Document_Code
            WHERE TSPL_BLK_FREIGHT_MASTER.Status = 1 and TSPL_BLK_FREIGHT_MASTER.Customer_Code = xx.Customer_Code  AND TSPL_BLK_FREIGHT_MASTER.Start_Date <= xx.Document_Date ORDER BY TSPL_BLK_FREIGHT_MASTER.Start_Date DESC ) AS tab2
            WHERE CONVERT(DATE, xx.Document_Date, 103) >=  CONVERT(DATE, '" & clsCommon.GetPrintDate(FromDate, "dd/MMM/yyyy") & "', 103) AND  CONVERT(DATE, xx.Document_Date, 103) <= CONVERT(DATE, '" & clsCommon.GetPrintDate(ToDate, "dd/MMM/yyyy") & "', 103) AND xx.Customer_Code = '" & Customer & "' "
            dt = clsDBFuncationality.GetDataTable(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return dt
    End Function
End Class

Public Class clsBulkSaleFreightCalculationDetail

#Region "Variables"
    Public SNO As Integer
    Public Document_Code As String = Nothing
    Public Dispatch_Date As Date? = Nothing
    Public Bulk_Dispatch_Document As String = Nothing
    Public Bulk_Dispatch_Tanker As String = Nothing
    Public Bulk_Dispatch_Transporter As String = Nothing
    Public Ack_Qty As Double = 0
    Public Ack_Fat As Decimal = 0
    Public Ack_Snf As Decimal = 0
    Public Tender_Qty As Double = 0
    Public Rate As Double = 0
    Public Applicable_Rate As Double = 0
    Public Pro_Rate As Double = 0
    Public DieselPetrol As Double = 0
    Public GPS_KM As Double = 0
    Public Payable_Amount As Double = 0

#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsBulkSaleFreightCalculationDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsBulkSaleFreightCalculationDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_Code", strCode)
                clsCommon.AddColumnsForChange(coll, "SNO", obj.SNO)
                clsCommon.AddColumnsForChange(coll, "Dispatch_Date", clsCommon.GetPrintDate(obj.Dispatch_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Bulk_Dispatch_Tanker", obj.Bulk_Dispatch_Tanker)
                clsCommon.AddColumnsForChange(coll, "Bulk_Dispatch_Document", obj.Bulk_Dispatch_Document)
                clsCommon.AddColumnsForChange(coll, "Bulk_Dispatch_Transporter", obj.Bulk_Dispatch_Transporter)
                clsCommon.AddColumnsForChange(coll, "Ack_Qty", obj.Ack_Qty)
                clsCommon.AddColumnsForChange(coll, "Ack_Fat", obj.Ack_Fat)
                clsCommon.AddColumnsForChange(coll, "Ack_Snf", obj.Ack_Snf)
                clsCommon.AddColumnsForChange(coll, "Tender_Qty", obj.Tender_Qty)
                clsCommon.AddColumnsForChange(coll, "Rate", obj.Rate)
                clsCommon.AddColumnsForChange(coll, "Pro_Rate", obj.Pro_Rate)
                clsCommon.AddColumnsForChange(coll, "DieselPetrol", obj.DieselPetrol)
                clsCommon.AddColumnsForChange(coll, "Applicable_Rate", obj.Applicable_Rate)
                clsCommon.AddColumnsForChange(coll, "GPS_KM", obj.GPS_KM)
                clsCommon.AddColumnsForChange(coll, "Payable_Amount", obj.Payable_Amount)

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BLK_FREIGHT_CALC_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

End Class



