Imports common
Imports System.Data.SqlClient
Public Class clsBankAdvise
    Public Document_No As String = ""
    Public Document_Date As Date
    Public Payment_Process_Document_No As String = ""
    Public Created_By As String = ""
    Public Created_Date As Date
    Public Modified_By As String = ""
    Public Modified_Date As Date
    Public Remarks As String = ""
    Public Status As Integer


    Public Shared Function SaveData(ByVal obj As clsBankAdvise, ByVal isNewEntry As Boolean) As Boolean
        Dim issaved As Boolean = True
        Try
            Dim dt As Date = clsCommon.myCDate(clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(Nothing, dt, clsDocType.BankAdvise, "", "", False)
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Payment_Process_Document_No", clsCommon.myCstr(obj.Payment_Process_Document_No))
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Status", obj.Status)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm:ss tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm:ss tt"))
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BANK_ADVISE", OMInsertOrUpdate.Insert, "", Nothing)
            Else
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BANK_ADVISE", OMInsertOrUpdate.Update, "Document_No= '" + obj.Document_No + "'", Nothing)
            End If
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_No, "TSPL_BANK_ADVISE", "Document_No", "TSPL_BANK_ADVISE", "Document_No", Nothing)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return issaved
    End Function

    Public Shared Function GetBankAdviseData(ByVal strCode As String, ByVal navtype As NavigatorType) As clsBankAdvise
        Dim Qry As String = ""
        Dim obj As clsBankAdvise = Nothing
        Try
            Dim whrCls As String = String.Empty
            Qry = "Select * from TSPL_BANK_ADVISE where 1=1"
            Select Case navtype
                Case NavigatorType.Current
                    Qry += " and TSPL_BANK_ADVISE.Document_No in ('" + strCode + "')"
                Case NavigatorType.Next
                    Qry += " and TSPL_BANK_ADVISE.Document_No in (select min(Document_No ) from TSPL_BANK_ADVISE where Document_No  >'" + strCode + "' " & whrCls & ") "
                Case NavigatorType.First
                    Qry += " and TSPL_BANK_ADVISE.Document_No in (select MIN(Document_No ) from TSPL_BANK_ADVISE where 1=1 " & whrCls & ") "
                Case NavigatorType.Last
                    Qry += " and TSPL_BANK_ADVISE.Document_No in (select Max(Document_No ) from TSPL_BANK_ADVISE where 1=1 " & whrCls & ") "
                Case NavigatorType.Previous
                    Qry += " and TSPL_BANK_ADVISE.Document_No in (select Max(Document_No ) from TSPL_BANK_ADVISE where Document_No  <'" + strCode + "' " & whrCls & ") "
            End Select

            If navtype = 0 AndAlso clsCommon.myLen(strCode) > 0 Then
                Qry += " and TSPL_BANK_ADVISE.Document_No in ('" + strCode + "')"
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New clsBankAdvise
                obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
                obj.Document_Date = clsCommon.GetPrintDate(dt.Rows(0)("Document_Date"))
                obj.Payment_Process_Document_No = clsCommon.myCstr(clsCommon.myCstr(dt.Rows(0)("Payment_Process_Document_No")))
                obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
                If clsCommon.myCDecimal(dt.Rows(0)("Status")) > 0 Then
                    obj.Status = 1
                Else
                    obj.Status = 0
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function

    Public Shared Function deleteData(ByVal strCode As String) As Boolean
        Try
            Dim Qry As String = "delete from TSPL_BANK_ADVISE where  Document_No='" & strCode & "'"
            clsDBFuncationality.ExecuteNonQuery(Qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function postData(ByVal strCode As String) As Boolean
        Try
            Dim Qry As String = "Update TSPL_BANK_ADVISE Set Status=1 where  Document_No='" & strCode & "'"
            clsDBFuncationality.ExecuteNonQuery(Qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function paymentProcessDetails(ByVal PPDocNo As String) As String
        Dim Qry As String = "Select TSPL_PAYMENT_PROCESS_HEAD.Doc_No As  [Document Code],TSPL_PAYMENT_PROCESS_HEAD.From_Date As [From Date],TSPL_PAYMENT_PROCESS_HEAD.To_Date As [To Date],TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader As [DCS Code],TSPL_PAYMENT_PROCESS_DETAIL.MCC_Code As [MCC Code],TSPL_LOCATION_MASTER.Location_Desc As [Area] from TSPL_PAYMENT_PROCESS_DETAIL
                                Left Outer Join TSPL_PAYMENT_PROCESS_HEAD On TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
                                left outer join TSPL_LOCATION_MASTER  On TSPL_LOCATION_MASTER.Location_Code=TSPL_PAYMENT_PROCESS_DETAIL.MCC_Code "
        If clsCommon.myLen(PPDocNo) > 0 Then
            Qry += " Where TSPL_PAYMENT_PROCESS_HEAD.FarmType='PP' And TSPL_PAYMENT_PROCESS_HEAD.Doc_No='" + PPDocNo + "'"
        End If
        Return Qry
    End Function

End Class
