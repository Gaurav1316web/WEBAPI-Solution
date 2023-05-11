Imports System.Data.SqlClient
Imports common
Public Class clsDCSFinancialEntry
#Region "variables"
    Public Document_Code As String = Nothing
    Public Document_Date As Date
    Public DCS_Code As String
    Public DCS_Name As String ''Not a Table Column
    Public Fiscal_Code As String
    Public Fiscal_Name As String ''Not a Table Column
    Public Remarks As String = ""
    Public Type As String = ""
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Posted_Date As Date? = Nothing
    Public arr As List(Of clsDCSFinancialEntryDetail) = Nothing
#End Region
    Public Shared Function SaveData(ByVal obj As clsDCSFinancialEntry, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = ""
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            qry = "delete from TSPL_DCS_FINANCIAL_ENTRY_DETAIL where Document_Code='" & obj.Document_Code & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "DCS_Code", obj.DCS_Code)
            clsCommon.AddColumnsForChange(coll, "Fiscal_Code", obj.Fiscal_Code)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks, True, True)
            clsCommon.AddColumnsForChange(coll, "Type", obj.Type)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.DSCFinancialEntry, "", "")
                If (clsCommon.myLen(obj.Document_Code) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DCS_FINANCIAL_ENTRY", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DCS_FINANCIAL_ENTRY", OMInsertOrUpdate.Update, "TSPL_DCS_FINANCIAL_ENTRY.Document_Code='" + obj.Document_Code + "'", trans)
            End If
            clsDCSFinancialEntryDetail.saveData(obj.arr, obj.Document_Code, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsDCSFinancialEntry
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsDCSFinancialEntry
        Dim obj As clsDCSFinancialEntry = Nothing
        Dim Arr As List(Of clsDCSFinancialEntry) = Nothing
        Dim qry As String = "Select TSPL_DCS_FINANCIAL_ENTRY.*,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_Fiscal_Year_Master.Fiscal_Name from TSPL_DCS_FINANCIAL_ENTRY 
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_DCS_FINANCIAL_ENTRY.DCS_Code
left outer join TSPL_Fiscal_Year_Master on TSPL_Fiscal_Year_Master.Fiscal_Code=TSPL_DCS_FINANCIAL_ENTRY.Fiscal_Code
where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_DCS_FINANCIAL_ENTRY.Document_Code = (select MIN(Document_Code) from TSPL_DCS_FINANCIAL_ENTRY WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_DCS_FINANCIAL_ENTRY.Document_Code = (select Max(Document_Code) from TSPL_DCS_FINANCIAL_ENTRY WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_DCS_FINANCIAL_ENTRY.Document_Code = '" + strCode + "' "
            Case NavigatorType.Next
                qry += " and TSPL_DCS_FINANCIAL_ENTRY.Document_Code = (select Min(Document_Code) from TSPL_DCS_FINANCIAL_ENTRY where Document_Code>'" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_DCS_FINANCIAL_ENTRY.Document_Code = (select Max(Document_Code) from TSPL_DCS_FINANCIAL_ENTRY where Document_Code<'" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsDCSFinancialEntry()
            obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.DCS_Code = clsCommon.myCstr(dt.Rows(0)("DCS_Code"))
            obj.DCS_Name = clsCommon.myCstr(dt.Rows(0)("VLC_Name"))
            obj.Fiscal_Code = clsCommon.myCstr(dt.Rows(0)("Fiscal_Code"))
            obj.Fiscal_Name = clsCommon.myCstr(dt.Rows(0)("Fiscal_Name"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Type = clsCommon.myCstr(dt.Rows(0)("Type"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            If dt.Rows(0)("Posted_Date") IsNot DBNull.Value Then
                obj.Posted_Date = clsCommon.myCDate(dt.Rows(0)("Posted_Date"))
            End If
            obj.arr = clsDCSFinancialEntryDetail.getData(obj.Document_Code, trans)
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
            Dim qry As String = ""
            qry = "delete from TSPL_DCS_FINANCIAL_ENTRY_DETAIL where Document_Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_DCS_FINANCIAL_ENTRY where Document_Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "Select TSPL_DCS_FINANCIAL_ENTRY.Document_Code as Code,Convert(varchar,TSPL_DCS_FINANCIAL_ENTRY.Document_Date,103) as Date
          ,TSPL_DCS_FINANCIAL_ENTRY.Remarks as [Remarks],TSPL_DCS_FINANCIAL_ENTRY.Type,TSPL_DCS_FINANCIAL_ENTRY.DCS_Code,TSPL_DCS_FINANCIAL_ENTRY.Fiscal_Code
          ,case when isnull(Status,0)=0 then 'Pending' else 'Approved' end as Status 
          from TSPL_DCS_FINANCIAL_ENTRY "
        str = clsCommon.ShowSelectForm("DCSFE#F", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")

            Dim obj As clsDCSFinancialEntry = clsDCSFinancialEntry.GetData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Post on :" + obj.Posted_Date)
            End If
            Dim qry As String = "Update TSPL_DCS_FINANCIAL_ENTRY set Status=1, Posted_Date='" + strPostDate + "',Posted_By='" + objCommonVar.CurrentUserCode + "' where Document_Code='" + strDocNo + "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsDCSFinancialEntryDetail
#Region "Variable"
    Public PK_Id As Integer
    Public Document_Code As String = Nothing
    Public SNo1 As Decimal
    Public Head_Code1 As String = Nothing
    Public Head_Name1 As String = Nothing ''Not a Table Column
    Public Amount_Type1 As Integer = 0
    Public Head_Sub_Amount1 As Decimal = 0
    Public Head_Amount1 As Decimal = 0
    Public SNo2 As Decimal
    Public Head_Code2 As String = Nothing
    Public Head_Name2 As String = Nothing ''Not a Table Column
    Public Amount_Type2 As Integer = 0
    Public Head_Sub_Amount2 As Decimal = 0
    Public Head_Amount2 As Decimal = 0

#End Region
    Public Shared Function saveData(ByVal arrObj As List(Of clsDCSFinancialEntryDetail), ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim coll As Hashtable
            If arrObj IsNot Nothing Then
                For Each obj As clsDCSFinancialEntryDetail In arrObj
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_Code", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "SNo1", obj.SNo1)
                    clsCommon.AddColumnsForChange(coll, "Head_Code1", obj.Head_Code1, True)
                    clsCommon.AddColumnsForChange(coll, "Amount_Type1", obj.Amount_Type1)
                    clsCommon.AddColumnsForChange(coll, "Head_Sub_Amount1", obj.Head_Sub_Amount1)
                    clsCommon.AddColumnsForChange(coll, "Head_Amount1", obj.Head_Amount1)
                    clsCommon.AddColumnsForChange(coll, "SNo2", obj.SNo2)
                    clsCommon.AddColumnsForChange(coll, "Head_Code2", obj.Head_Code2, True)
                    clsCommon.AddColumnsForChange(coll, "Amount_Type2", obj.Amount_Type2)
                    clsCommon.AddColumnsForChange(coll, "Head_Sub_Amount2", obj.Head_Sub_Amount2)
                    clsCommon.AddColumnsForChange(coll, "Head_Amount2", obj.Head_Amount2)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DCS_FINANCIAL_ENTRY_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function getData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As List(Of clsDCSFinancialEntryDetail)
        Try
            Dim arrObj As List(Of clsDCSFinancialEntryDetail) = Nothing
            Dim obj As clsDCSFinancialEntryDetail = Nothing
            Dim qry As String = "Select TSPL_DCS_FINANCIAL_ENTRY_DETAIL.*,TSPL_DCS_FINANCIAL_HEAD1.Description as Head_Name1,TSPL_DCS_FINANCIAL_HEAD2.Description as Head_Name2
from TSPL_DCS_FINANCIAL_ENTRY_DETAIL 
Left Outer Join TSPL_DCS_FINANCIAL_HEAD as TSPL_DCS_FINANCIAL_HEAD1 On TSPL_DCS_FINANCIAL_HEAD1.Code=TSPL_DCS_FINANCIAL_ENTRY_DETAIL.Head_Code1   
Left Outer Join TSPL_DCS_FINANCIAL_HEAD as TSPL_DCS_FINANCIAL_HEAD2 On TSPL_DCS_FINANCIAL_HEAD2.Code=TSPL_DCS_FINANCIAL_ENTRY_DETAIL.Head_Code2  
where TSPL_DCS_FINANCIAL_ENTRY_DETAIL.Document_Code='" & strDocNo & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of clsDCSFinancialEntryDetail)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New clsDCSFinancialEntryDetail()
                    obj.PK_Id = clsCommon.myCdbl(dt.Rows(i)("PK_Id"))
                    obj.Document_Code = clsCommon.myCstr(dt.Rows(i)("Document_Code"))
                    obj.SNo1 = clsCommon.myCDecimal(dt.Rows(i)("SNo1"))
                    obj.Head_Code1 = clsCommon.myCstr(dt.Rows(i)("Head_Code1"))
                    obj.Head_Name1 = clsCommon.myCstr(dt.Rows(i)("Head_Name1"))
                    obj.Amount_Type1 = clsCommon.myCDecimal(dt.Rows(i)("Amount_Type1"))
                    obj.Head_Sub_Amount1 = clsCommon.myCstr(dt.Rows(i)("Head_Sub_Amount1"))
                    obj.Head_Amount1 = clsCommon.myCstr(dt.Rows(i)("Head_Amount1"))

                    obj.SNo2 = clsCommon.myCDecimal(dt.Rows(i)("SNo2"))
                    obj.Head_Code2 = clsCommon.myCstr(dt.Rows(i)("Head_Code2"))
                    obj.Head_Name2 = clsCommon.myCstr(dt.Rows(i)("Head_Name2"))
                    obj.Amount_Type2 = clsCommon.myCDecimal(dt.Rows(i)("Amount_Type2"))
                    obj.Head_Sub_Amount2 = clsCommon.myCstr(dt.Rows(i)("Head_Sub_Amount2"))
                    obj.Head_Amount2 = clsCommon.myCstr(dt.Rows(i)("Head_Amount2"))

                    arrObj.Add(obj)
                Next
            End If
            Return arrObj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
