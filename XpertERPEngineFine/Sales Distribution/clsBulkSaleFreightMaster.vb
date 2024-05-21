Imports System.Data.SqlClient
Imports common

Public Class clsBulkSaleFreightMaster

#Region "Variables"

    Public Document_Code As String = Nothing
    Public Start_Date As Date? = Nothing
    Public Document_date As Date? = Nothing
    Public Status As Integer = 0
    Public Customer_Code As String = Nothing
    Public Arr As List(Of clsBulkSaleFreightDetail) = Nothing
#End Region
    Public Function SaveData(ByVal obj As clsBulkSaleFreightMaster, ByVal isNewEntry As Boolean, ByVal strTransType As String, ByVal AutoSave As Boolean) As Boolean
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

    Public Function SaveData(ByVal obj As clsBulkSaleFreightMaster, ByVal isNewEntry As Boolean, ByVal strTransType As String, ByVal trans As SqlTransaction, ByVal AutoSave As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "delete from TSPL_BLK_FREIGHT_DETAIL where Document_Code='" + obj.Document_Code + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_date", clsCommon.GetPrintDate(obj.Document_date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Customer_Code", clsCommon.myCstr(obj.Customer_Code))
            clsCommon.AddColumnsForChange(coll, "Start_Date", clsCommon.GetPrintDate(obj.Start_Date, "dd/MMM/yyyy"))
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

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BLK_FREIGHT_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BLK_FREIGHT_MASTER", OMInsertOrUpdate.Update, "TSPL_BLK_FREIGHT_MASTER.Document_Code='" + obj.Document_Code + "'", trans)
            End If
            isSaved = isSaved AndAlso clsBulkSaleFreightDetail.SaveData(obj.Document_Code, obj.Arr, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_Code, "TSPL_BLK_FREIGHT_MASTER", "Document_Code", "TSPL_BLK_FREIGHT_DETAIL", "Document_Code", trans)

        Catch err As Exception

            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strRetCode As String, ByVal NavType As NavigatorType, ByVal TransType As String) As clsBulkSaleFreightMaster
        Return GetData(strRetCode, NavType, TransType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal TransType As String, ByVal trans As SqlTransaction) As clsBulkSaleFreightMaster
        Dim obj As clsBulkSaleFreightMaster = Nothing
        Dim qry As String = "select Document_Code ,Customer_Code, Document_date,Start_Date ,ISNULL( Status,0) as Status from TSPL_BLK_FREIGHT_MASTER where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_BLK_FREIGHT_MASTER.Document_Code = (select MIN(Document_Code) from TSPL_BLK_FREIGHT_MASTER)"
            Case NavigatorType.Last
                qry += " and TSPL_BLK_FREIGHT_MASTER.Document_Code = (select Max(Document_Code) from TSPL_BLK_FREIGHT_MASTER)"
            Case NavigatorType.Next
                qry += " and TSPL_BLK_FREIGHT_MASTER.Document_Code = (select Min(Document_Code) from TSPL_BLK_FREIGHT_MASTER where Document_Code >'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_BLK_FREIGHT_MASTER.Document_Code = (select Max(Document_Code) from TSPL_BLK_FREIGHT_MASTER where Document_Code <'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_BLK_FREIGHT_MASTER.Document_Code = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsBulkSaleFreightMaster()
            obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
            obj.Customer_Code = clsCommon.myCstr(dt.Rows(0)("Customer_Code"))

            obj.Document_date = clsCommon.myCDate(dt.Rows(0)("Document_date"))
            obj.Start_Date = clsCommon.myCDate(dt.Rows(0)("Start_Date"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)

            qry = "select *  from TSPL_BLK_FREIGHT_DETAIL where Document_Code='" + obj.Document_Code + "' order by SNo "
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsBulkSaleFreightDetail)
                For Each dr As DataRow In dt.Rows
                    Dim objtr As New clsBulkSaleFreightDetail
                    objtr.SNO = clsCommon.myCdbl(dr("SNO"))
                    objtr.Document_Code = clsCommon.myCstr(dr("Document_Code"))
                    objtr.Tender_Qty = clsCommon.myCstr(dr("Tender_Qty"))
                    objtr.Rate = clsCommon.myCstr(dr("Rate"))
                    objtr.Pro_Rate = clsCommon.myCstr(dr("Pro_Rate"))
                    objtr.DieselPetrol = clsCommon.myCstr(dr("DieselPetrol"))
                    objtr.Applicable_Rate = clsCommon.myCstr(dr("Applicable_Rate"))
                    objtr.GPS_KM = clsCommon.myCstr(dr("GPS_KM"))
                    objtr.Payable_Amount = clsCommon.myCstr(dr("Payable_Amount"))
                    obj.Arr.Add(objtr)
                Next
            End If
        End If


        Return obj
    End Function

    Public Shared Function getFinder(ByVal strCode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim sql As String = "select Document_Code as DocumentNo ,convert(varchar(12),Start_Date,103) AS [Start Date],convert(varchar(12),Document_date,103) as DocumentDate,case when Status = 1 then 'Posted' else 'Unposted' end as Posted from TSPL_BLK_FREIGHT_MASTER"
        str = clsCommon.ShowSelectForm("BulkSaleFreightMaster", sql, "DocumentNo", "", strCode, "DocumentNo", isButtonClicked, "TSPL_BLK_FREIGHT_MASTER.Document_date")
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
            Dim obj As clsBulkSaleFreightMaster = clsBulkSaleFreightMaster.GetData(strDocNo, NavigatorType.Current, "", trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = 1) Then
                Throw New Exception("Already Posted")
            End If

            clsDBFuncationality.ExecuteNonQuery("Update TSPL_BLK_FREIGHT_MASTER set Status= 1, Posted_By = '" + objCommonVar.CurrentUserCode + "',Posted_Date = '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt") + "'  where Document_Code='" & obj.Document_Code & "'", trans)

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

            Dim obj As clsBulkSaleFreightMaster = clsBulkSaleFreightMaster.GetData(strCode, NavigatorType.Current, Nothing, trans)
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
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BLK_FREIGHT_MASTER", OMInsertOrUpdate.Update, "Document_Code='" + obj.Document_Code + "'", trans)

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
        Dim obj As clsBulkSaleFreightMaster = clsBulkSaleFreightMaster.GetData(strCode, NavigatorType.Current, "", trans)
        Try
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("Document No not found to Delete")
            End If
            If clsCommon.CompairString(obj.Status, "1") = CompairStringResult.Equal Then
                Throw New Exception("Already Posted")
            End If
            Dim qry As String = Nothing
            qry = "delete from TSPL_BLK_FREIGHT_DETAIL where Document_Code='" + obj.Document_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_BLK_FREIGHT_MASTER where Document_Code='" + obj.Document_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)


        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class

Public Class clsBulkSaleFreightDetail

#Region "Variables"
    Public SNO As Integer
    Public Document_Code As String = Nothing
    Public Tender_Qty As Double = 0
    Public Rate As Double = 0
    Public Applicable_Rate As Double = 0
    Public Pro_Rate As Double = 0
    Public DieselPetrol As Double = 0
    Public GPS_KM As Double = 0
    Public Payable_Amount As Double = 0

#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsBulkSaleFreightDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsBulkSaleFreightDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_Code", strCode)
                clsCommon.AddColumnsForChange(coll, "SNO", obj.SNO)
                clsCommon.AddColumnsForChange(coll, "Tender_Qty", obj.Tender_Qty)
                clsCommon.AddColumnsForChange(coll, "Rate", obj.Rate)
                clsCommon.AddColumnsForChange(coll, "Pro_Rate", obj.Pro_Rate)
                clsCommon.AddColumnsForChange(coll, "DieselPetrol", obj.DieselPetrol)
                clsCommon.AddColumnsForChange(coll, "Applicable_Rate", obj.Applicable_Rate)
                clsCommon.AddColumnsForChange(coll, "GPS_KM", obj.GPS_KM)
                clsCommon.AddColumnsForChange(coll, "Payable_Amount", obj.Payable_Amount)

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BLK_FREIGHT_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

End Class



