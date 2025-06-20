Imports System.Data.SqlClient
Public Class clsDCSTransportationCharges
#Region "Variables"
    Public Document_No As String = Nothing
    Public Document_Date As DateTime = Nothing
    Public Items As ArrayList = Nothing
    Public Remarks As String = Nothing
    Public Comments As String = Nothing
    Public Start_Date As Date = Nothing
    Public End_Date As  Date? = Nothing 
    Public In_Active As Boolean = False
    Public Posted_Date As DateTime = Nothing
    Public Status As Integer = 0
    Public InActive_Date As DateTime = Nothing
    Public Arr As List(Of clsDCSTransportationChargesDetail)
#End Region
    Public Function SaveData(ByVal obj As clsDCSTransportationCharges, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Function SaveData(ByVal obj As clsDCSTransportationCharges, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_DCS_Transportation_Charges_Detail where Document_No='" + clsCommon.myCstr(obj.Document_No) + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_DCS_Transportation_Charges_Items where Document_No='" + clsCommon.myCstr(obj.Document_No) + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks, True)
            clsCommon.AddColumnsForChange(coll, "Comments", obj.Comments, True)
            clsCommon.AddColumnsForChange(coll, "Start_Date", clsCommon.GetPrintDate(obj.Start_Date, "dd/MMM/yyyy"), True)
            If Not obj.End_Date Is Nothing Then
                clsCommon.AddColumnsForChange(coll, "End_Date", clsCommon.GetPrintDate(obj.End_Date, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "End_Date", "", True)
            End If
            clsCommon.AddColumnsForChange(coll, "In_Active", IIf(obj.In_Active, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.DCSTransportationCharges, "", "")
                If clsCommon.myLen(obj.Document_No) <= 0 Then
                    Throw New Exception("Error in Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)

                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DCS_Transportation_Charges_Head", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DCS_Transportation_Charges_Head", OMInsertOrUpdate.Update, "Document_No='" + clsCommon.myCstr(obj.Document_No) + "'", trans)
            End If
            clsDCSTransportationChargesItems.SaveData(obj.Document_No, obj.Items, trans)
            clsDCSTransportationChargesDetail.SaveData(obj.Document_No, obj.Arr, False, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_No, "TSPL_DCS_Transportation_Charges_Head", "Document_No", "TSPL_DCS_Transportation_Charges_Detail", "Document_No", trans)

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal Document_No As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsDCSTransportationCharges
        Dim obj As clsDCSTransportationCharges = Nothing

        Try
            Dim Whrcls As String = ""
            Dim strQry As String = "select Document_No,Document_Date,Start_Date,End_Date,Remarks,Comments,Status,Posted_Date,In_Active,InActive_Date,InActive_By from TSPL_DCS_Transportation_Charges_Head  where 2=2"

            Select Case NavType
                Case NavigatorType.First
                    strQry += " and TSPL_DCS_Transportation_Charges_Head.Document_No = (select MIN(Document_No) from TSPL_DCS_Transportation_Charges_Head where 1=1 " + Whrcls + "  )"
                Case NavigatorType.Last
                    strQry += " and TSPL_DCS_Transportation_Charges_Head.Document_No = (select Max(Document_No) from TSPL_DCS_Transportation_Charges_Head where 1=1 " + Whrcls + "  )"
                Case NavigatorType.Next
                    strQry += " and TSPL_DCS_Transportation_Charges_Head.Document_No = (select Min(Document_No) from TSPL_DCS_Transportation_Charges_Head where Document_No>'" + clsCommon.myCstr(Document_No) + "' " + Whrcls + "   )"
                Case NavigatorType.Previous
                    strQry += " and TSPL_DCS_Transportation_Charges_Head.Document_No = (select Max(Document_No) from TSPL_DCS_Transportation_Charges_Head where Document_No<'" + clsCommon.myCstr(Document_No) + "' " + Whrcls + "  )"
                Case NavigatorType.Current
                    strQry += " and TSPL_DCS_Transportation_Charges_Head.Document_No = '" + clsCommon.myCstr(Document_No) + "'  " + Whrcls + " "
            End Select

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

                obj = New clsDCSTransportationCharges()
                obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
                obj.Document_Date = clsCommon.GetPrintDate(dt.Rows(0)("Document_Date"), "dd/MMM/yyyy")
                If dt.Rows(0)("InActive_Date") IsNot DBNull.Value Then
                    obj.InActive_Date = clsCommon.myCDate(dt.Rows(0)("InActive_Date"))
                End If
                If dt.Rows(0)("Start_Date") IsNot DBNull.Value Then
                    obj.Start_Date = clsCommon.myCDate(dt.Rows(0)("Start_Date"))
                End If
                If IsDBNull(dt.Rows(0)("End_Date")) = True Then
                    obj.End_Date = Nothing
                Else
                    obj.End_Date = clsCommon.GetPrintDate(dt.Rows(0)("End_Date"), "dd/MMM/yyyy")
                End If
                obj.In_Active = clsCommon.myCBool(IIf(clsCommon.myCdbl(dt.Rows(0)("In_Active")) = 1, True, False))
                obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
                obj.Comments = clsCommon.myCstr(dt.Rows(0)("Comments"))
                obj.Status = IIf(clsCommon.myCDecimal(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
                If dt.Rows(0)("Posted_Date") IsNot DBNull.Value Then
                    obj.Posted_Date = clsCommon.myCDate(dt.Rows(0)("Posted_Date"))
                End If
                Dim countItems As Integer = clsDBFuncationality.getSingleValue("select count(Item_code) as NOofItems from TSPL_ITEM_MASTER where Item_Type='F'", trans)
                Dim countDCitems As Integer = clsDBFuncationality.getSingleValue("select count(Item_code) as NOofItems from TSPL_DCS_Transportation_Charges_Items where Document_No='" & obj.Document_No & "'", trans)
                If countItems = countDCitems Then
                    obj.Items = Nothing
                Else
                    strQry = "select Item_code from TSPL_DCS_Transportation_Charges_Items where Document_No='" & obj.Document_No & "'"
                    dt = New DataTable()
                    dt = clsDBFuncationality.GetDataTable(strQry, trans)
                    If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                        obj.Items = New ArrayList()
                        For Each dr As DataRow In dt.Rows
                            obj.Items.Add(clsCommon.myCstr(dr("Item_Code")))
                        Next
                    End If
                End If

                obj.Arr = clsDCSTransportationChargesDetail.GetData(obj.Document_No, trans)


            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function

    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
            Dim obj As clsDCSTransportationCharges = clsDCSTransportationCharges.GetData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Document No : " + strDocNo + " not found to Post")
            End If
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Posted on :" + obj.Posted_Date)
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Status", 1)
            clsCommon.AddColumnsForChange(coll, "Posted_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Posted_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DCS_Transportation_Charges_Head", OMInsertOrUpdate.Update, "Document_No='" + clsCommon.myCstr(obj.Document_No) + "'", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_DCS_Transportation_Charges_Head", "Document_No", trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As clsDCSTransportationCharges = clsDCSTransportationCharges.GetData(strCode, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
            If Not (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Transaction status should be posted.")
            End If
            Dim qry As String
            If obj.Status = 1 Then
                qry = "update TSPL_DCS_Transportation_Charges_Head set Status=0,Posted_Date=null,Posted_By=null where Document_No='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_DCS_Transportation_Charges_Head", "Document_No", trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function InActiveDocument(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As clsDCSTransportationCharges = clsDCSTransportationCharges.GetData(strCode, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Document No not found to In Active")
            End If
            If Not (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Transaction status should be posted for In Active the Document No")
            End If
            If obj.In_Active Then
                Throw New Exception("Already In Active on :" + obj.InActive_Date)
            End If
            Dim qry As String
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "In_Active", 1)
            clsCommon.AddColumnsForChange(coll, "InActive_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "InActive_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DCS_Transportation_Charges_Head", OMInsertOrUpdate.Update, "Document_No='" + clsCommon.myCstr(obj.Document_No) + "'", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_DCS_Transportation_Charges_Head", "Document_No", trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim obj As New clsDCSTransportationCharges()
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Document No not found to Delete")
            End If
            clsCommonFunctionality.SaveDeletedData(objCommonVar.CurrentUserCode, strCode, "TSPL_DCS_Transportation_Charges_Head", "Document_No", "TSPL_DCS_Transportation_Charges_Detail", "Document_No", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_DCS_Transportation_Charges_Head", "Document_No", "TSPL_DCS_Transportation_Charges_Detail", "Document_No", trans)

            Dim Status As Integer = 0
            Status = clsDBFuncationality.getSingleValue("SELECT Count(*) FROM TSPL_DCS_Transportation_Charges_Head where Document_No = '" & strCode & "' and Status=1", trans)
            If (Status = 1) Then
                Throw New Exception("Already Posted on :" + obj.Posted_Date)
            End If

            Dim qry As String

            qry = "delete from TSPL_DCS_Transportation_Charges_ITEMS where Document_No ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "DELETE FROM TSPL_DCS_Transportation_Charges_Detail WHERE Document_No='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_DCS_Transportation_Charges_Head where Document_No ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function PickTransportationRate(ByVal strDCSCode As String, ByVal strItemCode As String, ByVal DocumentDate As String) As DataTable
        Dim dt As New DataTable()
        Try
            Dim qry As String = "Select top 1 TSPL_DCS_Transportation_Charges_Detail.Transportation_Rate,TSPL_DCS_Transportation_Charges_Detail.PK_ID from TSPL_DCS_Transportation_Charges_Detail left outer join TSPL_DCS_TRANSPORTATION_CHARGES_HEAD On TSPL_DCS_TRANSPORTATION_CHARGES_HEAD.Document_No=TSPL_DCS_Transportation_Charges_Detail.Document_No left outer join TSPL_DCS_Transportation_Charges_items  On TSPL_DCS_Transportation_Charges_items.Document_No = TSPL_DCS_Transportation_Charges_Detail.Document_No 
           where  TSPL_DCS_Transportation_Charges_items.Item_Code='" + strItemCode + "' and TSPL_DCS_Transportation_Charges_Detail.DCS_Code='" + strDCSCode + "' and  TSPL_DCS_TRANSPORTATION_CHARGES_HEAD.Status=1 and TSPL_DCS_TRANSPORTATION_CHARGES_HEAD.In_Active = 0 and convert(date,TSPL_DCS_TRANSPORTATION_CHARGES_HEAD.Start_Date,103) <='" + clsCommon.GetPrintDate(DocumentDate, "dd/MMM/yyyy") + "' and 2 =(case when  convert(date,TSPL_DCS_TRANSPORTATION_CHARGES_HEAD.End_Date,103) is null then 2 else ( case when convert(date,TSPL_DCS_TRANSPORTATION_CHARGES_HEAD.End_Date,103) >='" + clsCommon.GetPrintDate(DocumentDate, "dd/MMM/yyyy") + "'  then 2 else 3 end )  end )
          order by TSPL_DCS_TRANSPORTATION_CHARGES_HEAD.Start_Date desc,TSPL_DCS_TRANSPORTATION_CHARGES_HEAD.Document_No"
            dt = clsDBFuncationality.GetDataTable(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return dt
    End Function
End Class
Public Class clsDCSTransportationChargesDetail

#Region "Variables"
    Public SNO As Integer = 0
    Public PK_ID As Integer
    Public Document_No As String = Nothing
    Public VLC_Code_VLC_Uploader As String
    Public DCS_Code As String = Nothing
    Public VLC_Name As String = Nothing
    Public Transportation_Rate As Double = 0
#End Region
    Public Shared Function SaveData(ByVal Document_No As String, ByVal Arr As List(Of clsDCSTransportationChargesDetail), ByVal IsUpdatedFromCorrection As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                Dim SNo As Integer = 1
                For Each obj As clsDCSTransportationChargesDetail In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_No", Document_No)
                    clsCommon.AddColumnsForChange(coll, "SNO", SNo)
                    clsCommon.AddColumnsForChange(coll, "DCS_Code", obj.DCS_Code)
                    clsCommon.AddColumnsForChange(coll, "Transportation_Rate", obj.Transportation_Rate)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DCS_Transportation_Charges_Detail", OMInsertOrUpdate.Insert, "", trans)
                    SNo += 1
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return True
    End Function
    Public Shared Function GetData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As List(Of clsDCSTransportationChargesDetail)
        Dim arr As List(Of clsDCSTransportationChargesDetail) = Nothing
        Try
            Dim dt As DataTable
            Dim strQry As String = "select TSPL_DCS_Transportation_Charges_Detail.Document_No,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_DCS_Transportation_Charges_Detail.DCS_Code,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_DCS_Transportation_Charges_Detail.Transportation_Rate from TSPL_DCS_Transportation_Charges_Detail LEFT outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_DCS_Transportation_Charges_Detail.DCS_Code where TSPL_DCS_Transportation_Charges_Detail.Document_No='" & strDocNo & "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(strQry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                arr = New List(Of clsDCSTransportationChargesDetail)
                Dim objTr As clsDCSTransportationChargesDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsDCSTransportationChargesDetail
                    objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                    objTr.VLC_Code_VLC_Uploader = clsCommon.myCstr(dr("VLC_Code_VLC_Uploader"))
                    objTr.DCS_Code = clsCommon.myCstr(dr("DCS_Code"))
                    objTr.VLC_Name = clsCommon.myCstr(dr("VLC_Name"))
                    objTr.Transportation_Rate = clsCommon.myCDecimal(dr("Transportation_Rate"))
                    arr.Add(objTr)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return arr
    End Function
End Class
Public Class clsDCSTransportationChargesItems
    Public Document_No As String = Nothing
    Public Item_Code As String = Nothing
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As ArrayList, ByVal trans As SqlTransaction) As Boolean
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each strMCC As String In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", strMCC)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DCS_Transportation_Charges_Items", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class
