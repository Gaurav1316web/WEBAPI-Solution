Imports common
Imports System.Data.SqlClient
Public Class clsReviseMilkBill
#Region "Variables"
    Public Document_No As String = ""
    Public Document_Date As Date? = Nothing
    Public VLC_Code As String = ""
    Public MCC_Code As String = ""
    Public MCC_NAME As String = ""
    Public From_Date As String = ""
    Public To_Date As String = ""
    Public Remarks As String = ""
    Public Comments As String = ""
    Public VLC_Code_VLC_Uploader As String = ""
    Public VLC_Name As String = ""
    Public ArrReviseMilkBillDetail As List(Of clsReviseMilkBillDetail) = Nothing
    Public arrClsReviseMilkBillAddition As List(Of clsReviseMilkBillAddition) = Nothing
    Public arrClsReviseMilkBillDeductions As List(Of clsReviseMilkBillDeduction) = Nothing
    Public dtMilkBillDetail As DataTable = Nothing
    Public dtClsReviseMilkBillAddition As DataTable = Nothing
    Public dtClsReviseMilkBillDeductions As DataTable = Nothing
    Public Total_Milk_Amount As Decimal = 0
    Public Total_Addition_Amount As Decimal = 0
    Public Total_Deduction_Amount As Decimal = 0
    Public Payable_Amount As Decimal = 0
    Public Status As Integer = 0
    Public Posted_Date As DateTime? = Nothing
#End Region

    Public Shared Function SaveData(ByVal obj As clsReviseMilkBill, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, trans, isNewEntry)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal obj As clsReviseMilkBill, ByVal trans As SqlTransaction, ByVal isNewEntry As Boolean) As Boolean
        Dim issaved As Boolean = True
        Try
            clsReviseMilkBillDeduction.deleteData(obj.Document_No, trans)
            clsReviseMilkBillAddition.deleteData(obj.Document_No, trans)
            clsReviseMilkBillDetail.deleteData(obj.Document_No, trans)
            Dim dt As Date = clsCommon.myCDate(clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "VLC_Code", clsCommon.myCstr(obj.VLC_Code))
            clsCommon.AddColumnsForChange(coll, "MCC_Code", clsCommon.myCstr(obj.MCC_Code))
            clsCommon.AddColumnsForChange(coll, "From_Date", clsCommon.GetPrintDate(obj.From_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "To_Date", clsCommon.GetPrintDate(obj.To_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Comments", clsCommon.myCstr(obj.Comments), True)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks, True)
            clsCommon.AddColumnsForChange(coll, "Total_Milk_Amount", obj.Total_Milk_Amount)
            clsCommon.AddColumnsForChange(coll, "Total_Addition_Amount", obj.Total_Addition_Amount)
            clsCommon.AddColumnsForChange(coll, "Total_Deduction_Amount", obj.Total_Deduction_Amount)
            clsCommon.AddColumnsForChange(coll, "Payable_Amount", obj.Payable_Amount, True)

            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.ReviseMilkBill, "", obj.MCC_Code, False, True, False, False, True)
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                If clsCommon.myLen(obj.Document_No) <= 0 Then
                    Throw New Exception("Error In Doc No Generation")
                End If
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_BILL_REVISE", OMInsertOrUpdate.Insert, "", trans)
            Else
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_BILL_REVISE", OMInsertOrUpdate.Update, "Document_No= '" + obj.Document_No + "'", trans)
            End If

            issaved = issaved AndAlso clsReviseMilkBillDeduction.SaveData(obj.Document_No, obj.arrClsReviseMilkBillDeductions, trans)
            issaved = issaved AndAlso clsReviseMilkBillAddition.SaveData(obj.Document_No, obj.arrClsReviseMilkBillAddition, trans)
            issaved = issaved AndAlso clsReviseMilkBillDetail.SaveData(obj.Document_No, obj.ArrReviseMilkBillDetail, trans)
            clsCommonFunctionality.SaveHistoryData(EnumSaveType.History, objCommonVar.CurrentUserCode, obj.Document_No, "TSPL_MILK_BILL_REVISE", "Document_No", "TSPL_MILK_BILL_REVISE_DETAIL", "Document_No", "TSPL_MILK_BILL_REVISE_DEDUCTION", "Document_No", "TSPL_MILK_BILL_REVISE_ADDITION", "Document_No", "", "", "", "", "", "", trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return issaved
    End Function
    Public Shared Function getData(ByVal strCode As String, ByVal navtype As NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As clsReviseMilkBill
        Dim obj As clsReviseMilkBill = Nothing
        Try
            Dim whrCls As String = String.Empty
            Dim qst As String = " select TSPL_MILK_BILL_REVISE.*,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_MCC_MASTER.MCC_Code,TSPL_MCC_MASTER.MCC_NAME From TSPL_MILK_BILL_REVISE left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_CODE = TSPL_MILK_BILL_REVISE.VLC_CODE  left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_BILL_REVISE.MCC_Code where 1=1 " & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and TSPL_MILK_BILL_REVISE.Document_No in ('" + strCode + "')"
                Case NavigatorType.Next
                    qst += " and TSPL_MILK_BILL_REVISE.Document_No in (select min(Document_No ) from TSPL_MILK_BILL_REVISE where Document_No  >'" + strCode + "' " & whrCls & ") "
                Case NavigatorType.First
                    qst += " and TSPL_MILK_BILL_REVISE.Document_No in (select MIN(Document_No ) from TSPL_MILK_BILL_REVISE where 1=1 " & whrCls & ") "
                Case NavigatorType.Last
                    qst += " and TSPL_MILK_BILL_REVISE.Document_No in (select Max(Document_No ) from TSPL_MILK_BILL_REVISE where 1=1 " & whrCls & ") "
                Case NavigatorType.Previous
                    qst += " and TSPL_MILK_BILL_REVISE.Document_No in (select Max(Document_No ) from TSPL_MILK_BILL_REVISE where Document_No  <'" + strCode + "' " & whrCls & ") "
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New clsReviseMilkBill
                obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
                obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
                obj.From_Date = clsCommon.myCDate(dt.Rows(0)("From_Date"))
                obj.To_Date = clsCommon.myCDate(dt.Rows(0)("To_Date"))
                obj.VLC_Code = clsCommon.myCstr(dt.Rows(0)("VLC_Code"))
                obj.VLC_Code_VLC_Uploader = clsCommon.myCstr(dt.Rows(0)("VLC_Code_VLC_Uploader"))
                obj.VLC_Name = clsCommon.myCstr(dt.Rows(0)("VLC_Name"))
                obj.MCC_Code = clsCommon.myCstr(dt.Rows(0)("MCC_Code"))
                obj.MCC_NAME = clsCommon.myCstr(dt.Rows(0)("MCC_NAME"))
                obj.Total_Milk_Amount = clsCommon.myCDecimal(dt.Rows(0)("Total_Milk_Amount"))
                obj.Total_Addition_Amount = clsCommon.myCDecimal(dt.Rows(0)("Total_Addition_Amount"))
                obj.Total_Deduction_Amount = clsCommon.myCDecimal(dt.Rows(0)("Total_Deduction_Amount"))
                obj.Payable_Amount = clsCommon.myCDecimal(dt.Rows(0)("Payable_Amount"))
                obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
                obj.Status = clsCommon.myCdbl(dt.Rows(0)("Status"))
                obj.Comments = clsCommon.myCstr(dt.Rows(0)("Comments"))
                If dt.Rows(0)("Posted_Date") IsNot DBNull.Value Then
                    obj.Posted_Date = clsCommon.myCDate(dt.Rows(0)("Posted_Date"))
                End If
                obj.ArrReviseMilkBillDetail = clsReviseMilkBillDetail.getData(obj.Document_No, trans)
                obj.arrClsReviseMilkBillDeductions = clsReviseMilkBillDeduction.getData(obj.Document_No, trans)
                obj.arrClsReviseMilkBillAddition = clsReviseMilkBillAddition.getData(obj.Document_No, trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function

    Public Shared Function deleteData(ByVal DocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            deleteData(DocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function deleteData(ByVal DocNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim isDeleted As Boolean = True
        Try
            clsCommonFunctionality.SaveDeletedData(objCommonVar.CurrentUserCode, DocNo, "TSPL_MILK_BILL_REVISE", "Document_No", "TSPL_MILK_BILL_REVISE_DETAIL", "Document_No", trans)

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, DocNo, "TSPL_MILK_BILL_REVISE", "Document_No", "TSPL_MILK_BILL_REVISE_DETAIL", "Document_No", trans)
            isDeleted = isDeleted AndAlso clsReviseMilkBillDeduction.deleteData(DocNo, trans)
            isDeleted = isDeleted AndAlso clsReviseMilkBillAddition.deleteData(DocNo, trans)
            isDeleted = isDeleted AndAlso clsReviseMilkBillDetail.deleteData(DocNo, trans)
            Dim qry As String = "delete from TSPL_MILK_BILL_REVISE where  Document_No='" & DocNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Try
            Dim str As String = ""
            Dim qry As String = ""
            qry = " select TSPL_MILK_BILL_REVISE.Document_No as [DocumentNo] ,TSPL_MILK_BILL_REVISE.Document_Date as [Document Date] ,TSPL_MILK_BILL_REVISE.From_Date as [From Date], TSPL_MILK_BILL_REVISE.MCC_Code AS [MCC Code],TSPL_MCC_MASTER.MCC_NAME as [MCC Name],TSPL_MILK_BILL_REVISE.To_Date as [To Date] ,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader AS [DCS Uploader Code] ,TSPL_MILK_BILL_REVISE.VLC_Code as [DCS Code],TSPL_VLC_MASTER_HEAD.VLC_NAME AS [DCS Name] ,TSPL_MILK_BILL_REVISE.Remarks as Remarks,TSPL_MILK_BILL_REVISE.Comments,case when TSPL_MILK_BILL_REVISE.Status=0 then 'Pending' else 'Approved' end as [Status] ,TSPL_MILK_BILL_REVISE.Created_By as [Created By] ,TSPL_MILK_BILL_REVISE.Created_Date as [Created Date] ,TSPL_MILK_BILL_REVISE.Modified_By as [Modified By] ,TSPL_MILK_BILL_REVISE.Modified_Date as [Modified Date] ,TSPL_MILK_BILL_REVISE.Posted_Date as [Posting Date] " &
                " From TSPL_MILK_BILL_REVISE left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_CODE = TSPL_MILK_BILL_REVISE.MCC_CODE left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_CODE = TSPL_MILK_BILL_REVISE.VLC_CODE"
            str = clsCommon.ShowSelectForm("fndReviseMilkBill", qry, "DocumentNo", whrcls, curcode, "DocumentNo", isButtonClicked, "Document_Date")
            Return str
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
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
    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If clsCommon.myLen(strCode) <= 0 Then
                Throw New Exception("Transaction No not found for reverse and unpost")
            End If

            Dim Qry As String = "select Status from TSPL_MILK_BILL_REVISE where Document_No='" + strCode + "'"
            If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If

            Qry = "Update TSPL_MILK_BILL_REVISE set Status = 0 where Document_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_MILK_BILL_REVISE", "Document_No", trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function Unpost(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(strDocNo) <= 0 Then
                Throw New Exception("Revise Milk Bill Document no Not found to  unpost")
            End If

            Dim qry As String = "select TSPL_MILK_BILL_REVISE.Status,TSPL_MILK_BILL_REVISE.VLC_Code,TSPL_MILK_BILL_REVISE.Document_Date from TSPL_MILK_BILL_REVISE where Document_No='" + strDocNo + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Revise Milk Bill Document no Not found to  unpost")
            Else
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.frmPaymentProcess, clsCommon.myCstr(dt.Rows(0)("Area_Location_Code")), clsCommon.myCDate(dt.Rows(0)("Document_Date")), trans)
            End If
            If clsCommon.myCdbl(dt.Rows(0)("Status")) = 1 Then
                Throw New Exception("Revise Milk Bill processed can't unpost it")
            End If
            'clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_MILK_BILL_REVISE", "Document_No", "TSPL_MILK_BILL_REVISE_DETAIL", "Document_No", trans)

            qry = "update TSPL_MILK_BILL_REVISE set Status=0, Posted_Date=null where Document_No='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_MILK_BILL_REVISE", "Document_No", "TSPL_MILK_BILL_REVISE_DETAIL", "Document_No", trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(FormId, strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
            Dim obj As clsReviseMilkBill = clsReviseMilkBill.getData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = 1) Then
                Throw New Exception("Already Post on :" + obj.Posted_Date)
            End If
            Dim qry As String = ""

            qry = "Update TSPL_MILK_BILL_REVISE set  Status=1, Posted_Date='" + clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") + "',Posted_By='" + objCommonVar.CurrentUserCode + "' where Document_No='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_MILK_BILL_REVISE", "Document_No", "TSPL_MILK_BILL_REVISE_DETAIL", "Document_No", trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class
Public Class clsReviseMilkBillDetail
#Region "Variables"
    Public Document_No As String = ""
    Public SNo As Integer = 0
    Public Shift_Date As Date? = Nothing
    Public Shift As String = ""
    Public Qty As Decimal = 0
    Public FAT As Decimal = 0
    Public SNF As Decimal = 0
    Public Rate As Decimal = 0
    Public Price_Code As String
    Public Amount As Decimal = 0

#End Region

    Public Shared Function SaveData(ByVal DocNo As String, ByVal arr As List(Of clsReviseMilkBillDetail), ByVal tran As SqlTransaction) As Boolean
        Try
            Dim i As Integer = 0
            Dim issaved As Boolean = True
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For i = 0 To arr.Count - 1
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_No", DocNo)
                    clsCommon.AddColumnsForChange(coll, "SNo", arr.Item(i).SNo)
                    clsCommon.AddColumnsForChange(coll, "Shift", arr.Item(i).Shift)
                    clsCommon.AddColumnsForChange(coll, "Shift_Date", clsCommon.GetPrintDate(arr.Item(i).Shift_Date, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Qty", arr.Item(i).Qty)
                    clsCommon.AddColumnsForChange(coll, "FAT", arr.Item(i).FAT)
                    clsCommon.AddColumnsForChange(coll, "SNF", arr.Item(i).SNF)
                    clsCommon.AddColumnsForChange(coll, "Rate", clsCommon.myCDecimal(arr.Item(i).Rate))
                    clsCommon.AddColumnsForChange(coll, "Price_Code", clsCommon.myCstr(arr.Item(i).Price_Code))
                    clsCommon.AddColumnsForChange(coll, "Amount", clsCommon.myCDecimal(arr.Item(i).Amount))
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_BILL_REVISE_DETAIL", OMInsertOrUpdate.Insert, "", tran)
                Next
            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function getDataDT(ByVal Document_No As String, ByVal trans As SqlTransaction) As DataTable
        Try
            Dim q As String = "select * from TSPL_MILK_BILL_REVISE_DETAIL where TSPL_MILK_BILL_REVISE_DETAIL.Document_No ='" & Document_No & "' order by TSPL_MILK_BILL_REVISE_DETAIL.SNo"
            Return clsDBFuncationality.GetDataTable(q, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function getData(ByVal Document_No As String, ByVal trans As SqlTransaction) As List(Of clsReviseMilkBillDetail)
        Try
            Dim obj As clsReviseMilkBill = Nothing
            Dim arr As New List(Of clsReviseMilkBillDetail)
            Dim qry As String = "select * from TSPL_MILK_BILL_REVISE_DETAIL where TSPL_MILK_BILL_REVISE_DETAIL.Document_No ='" & Document_No & "' order by TSPL_MILK_BILL_REVISE_DETAIL.SNo"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New clsReviseMilkBill()
                obj.ArrReviseMilkBillDetail = New List(Of clsReviseMilkBillDetail)
                Dim objTr As clsReviseMilkBillDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsReviseMilkBillDetail
                    objTr.SNo = clsCommon.myCdbl(dr("SNo"))
                    objTr.Shift_Date = clsCommon.myCDate(dr("Shift_Date"))
                    objTr.Shift = clsCommon.myCstr(dr("Shift"))
                    objTr.Qty = clsCommon.myCDecimal(dr("Qty"))
                    objTr.FAT = clsCommon.myCDecimal(dr("FAT"))
                    objTr.SNF = clsCommon.myCDecimal(dr("SNF"))
                    objTr.Rate = clsCommon.myCDecimal(dr("Rate"))
                    objTr.Price_Code = clsCommon.myCstr(dr("Price_Code"))
                    objTr.Amount = clsCommon.myCDecimal(dr("Amount"))
                    obj.ArrReviseMilkBillDetail.Add(objTr)
                Next
            End If
            Return obj.ArrReviseMilkBillDetail
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function deleteData(ByVal DocNo As String, ByVal tran As SqlTransaction) As Boolean
        Try
            Dim isDeleted As Boolean = True
            Dim qry As String = "delete from TSPL_MILK_BILL_REVISE_DETAIL where  Document_No='" & DocNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, tran)
            Return isDeleted
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

Public Class clsReviseMilkBillDeduction
#Region "Variables"
    Public Document_No As String = ""
    Public SNo As Integer = 0
    Public Ded_Code As String = ""
    Public Ded_Desc As String = ""
    Public Amount As Double = 0
#End Region

    Public Shared Function SaveData(ByVal DocNo As String, ByVal arr As List(Of clsReviseMilkBillDeduction), Optional ByVal tran As SqlTransaction = Nothing) As Boolean
        Try
            Dim i As Integer = 0
            Dim issaved As Boolean = True
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For i = 0 To arr.Count - 1
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_No", DocNo)
                    clsCommon.AddColumnsForChange(coll, "SNo", arr.Item(i).SNo)
                    clsCommon.AddColumnsForChange(coll, "Ded_Code", arr.Item(i).Ded_Code)
                    clsCommon.AddColumnsForChange(coll, "Amount", clsCommon.myCdbl(arr.Item(i).Amount))
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_BILL_REVISE_DEDUCTION", OMInsertOrUpdate.Insert, "", tran)
                Next
            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function getData(ByVal Document_No As String, ByVal trans As SqlTransaction) As List(Of clsReviseMilkBillDeduction)
        Try
            Dim arr As New List(Of clsReviseMilkBillDeduction)
            Dim obj As New clsReviseMilkBillDeduction
            Dim qry As String = "select *, ded.Description AS Ded_Desc from TSPL_MILK_BILL_REVISE_DEDUCTION  left join ( select distinct Code,Description from ( select code,Description from TSPL_DCS_ADDITION_DEDUCTION  Where 2=2 and Nature_Type=1  
 union all select Code,Description from TSPL_DEDUCTION_MASTER where Ded_Grp_Code = 'DEDUCTION' ) xx )Ded on Ded.Code=TSPL_MILK_BILL_REVISE_DEDUCTION.Ded_Code where TSPL_MILK_BILL_REVISE_DEDUCTION.Document_No ='" & Document_No & "' order by  TSPL_MILK_BILL_REVISE_DEDUCTION.SNo  "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New clsReviseMilkBillDeduction
                    obj.Document_No = clsCommon.myCstr(dt.Rows(i)("Document_No"))
                    obj.SNo = clsCommon.myCstr(dt.Rows(i)("SNo"))
                    obj.Ded_Code = clsCommon.myCstr(dt.Rows(i)("Ded_Code"))
                    obj.Ded_Desc = clsCommon.myCstr(dt.Rows(i)("Ded_Desc"))
                    obj.Amount = clsCommon.myCdbl(dt.Rows(i)("Amount"))
                    arr.Add(obj)
                Next
            End If
            Return arr
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function getDataDT(ByVal doc_No As String, ByVal trans As SqlTransaction) As DataTable
        Try
            Dim q As String = "select *, ded.Description AS Ded_Desc from TSPL_MILK_BILL_REVISE_DEDUCTION  left join ( select distinct Code,Description from ( select code,Description from TSPL_DCS_ADDITION_DEDUCTION  Where 2=2 and Nature_Type=1  
 union all select Code,Description from TSPL_DEDUCTION_MASTER where Ded_Grp_Code = 'DEDUCTION' ) xx )Ded on Ded.Code=TSPL_MILK_BILL_REVISE_DEDUCTION.Ded_Code where TSPL_MILK_BILL_REVISE_DEDUCTION.Document_No ='" & doc_No & "' order by  TSPL_MILK_BILL_REVISE_DEDUCTION.SNo  "
            Return clsDBFuncationality.GetDataTable(q, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function deleteData(ByVal DocNo As String, ByVal tran As SqlTransaction) As Boolean
        Try
            Dim isDeleted As Boolean = True
            Dim qry As String = "delete from TSPL_MILK_BILL_REVISE_DEDUCTION where  Document_No='" & DocNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, tran)
            Return isDeleted
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

Public Class clsReviseMilkBillAddition
#Region "Variables"
    Public Document_No As String = ""
    Public SNo As Integer = 0
    Public Addition_Code As String = ""
    Public Addition_Desc As String = ""
    Public Amount As Decimal = 0
#End Region

    Public Shared Function SaveData(ByVal DocNo As String, ByVal arr As List(Of clsReviseMilkBillAddition), Optional ByVal tran As SqlTransaction = Nothing) As Boolean
        Try
            Dim i As Integer = 0
            Dim issaved As Boolean = True
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For i = 0 To arr.Count - 1
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_No", DocNo)
                    clsCommon.AddColumnsForChange(coll, "SNo", arr.Item(i).SNo)
                    clsCommon.AddColumnsForChange(coll, "Addition_Code", arr.Item(i).Addition_Code)
                    clsCommon.AddColumnsForChange(coll, "Amount", arr.Item(i).Amount)
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_BILL_REVISE_ADDITION", OMInsertOrUpdate.Insert, "", tran)
                Next
            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function getData(ByVal Document_No As String, ByVal trans As SqlTransaction) As List(Of clsReviseMilkBillAddition)
        Try
            Dim arr As New List(Of clsReviseMilkBillAddition)
            Dim obj As New clsReviseMilkBillAddition
            Dim qry As String = "select *, Addition.Description AS Addition_Desc  from TSPL_MILK_BILL_REVISE_ADDITION  left join ( select distinct Code,Description from ( select code,Description from TSPL_DCS_ADDITION_DEDUCTION  Where 2=2 and Nature_Type=0  
 union all select Code,Description from TSPL_DEDUCTION_MASTER where Ded_Grp_Code = 'ADDITION' ) xx )Addition on Addition.Code=TSPL_MILK_BILL_REVISE_ADDITION.Addition_Code where TSPL_MILK_BILL_REVISE_ADDITION.Document_No='" & Document_No & "' order by  TSPL_MILK_BILL_REVISE_ADDITION.SNo"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New clsReviseMilkBillAddition
                    obj.Document_No = clsCommon.myCstr(dt.Rows(i)("Document_No"))
                    obj.SNo = clsCommon.myCstr(dt.Rows(i)("SNo"))
                    obj.Addition_Code = clsCommon.myCstr(dt.Rows(i)("Addition_Code"))
                    obj.Addition_Desc = clsCommon.myCstr(dt.Rows(i)("Addition_Desc"))
                    obj.Amount = clsCommon.myCdbl(dt.Rows(i)("Amount"))
                    arr.Add(obj)
                Next
            End If
            Return arr
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function getDataDT(ByVal doc_No As String, ByVal trans As SqlTransaction) As DataTable
        Try
            Dim qry As String = "select *, Addition.Description AS Addition_Desc  from TSPL_MILK_BILL_REVISE_ADDITION  left join ( select distinct Code,Description from ( select code,Description from TSPL_DCS_ADDITION_DEDUCTION  Where 2=2 and Nature_Type=0  
 union all select Code,Description from TSPL_DEDUCTION_MASTER where Ded_Grp_Code = 'ADDITION' ) xx )Addition on Addition.Code=TSPL_MILK_BILL_REVISE_ADDITION.Addition_Code where TSPL_MILK_BILL_REVISE_ADDITION.Document_No='" & doc_No & "' order by  TSPL_MILK_BILL_REVISE_ADDITION.SNo"
            Return clsDBFuncationality.GetDataTable(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function deleteData(ByVal DocNo As String, ByVal tran As SqlTransaction) As Boolean
        Try
            Dim isDeleted As Boolean = True
            Dim qry As String = "delete from TSPL_MILK_BILL_REVISE_ADDITION where Document_No='" & DocNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, tran)
            Return isDeleted
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
