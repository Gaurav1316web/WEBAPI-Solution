Imports common
Imports System.Data.SqlClient
Public Class clsTankerDispatchPriceMaster
#Region "Variables"
    Public PRICE_CODE As String = Nothing
    Public PRICE_DATE As Date = Nothing
    Public PRICE_DESC As String = Nothing
    Public TOTAL_SOLID_RATE As Decimal = Nothing
    Public EFFECTIVE_DATE As Date = Nothing
    Public Posted As Integer = Nothing
    Public Posting_Date As Date? = Nothing
    Public Posted_By As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As Date = Nothing
    Public Modified_By As String = Nothing
    Public Modified_Date As Date = Nothing
    Public Comp_Code As Decimal = Nothing
    Public isJobWork As Boolean = False
    Public Item_Code As String = Nothing
    Public ArrMccCode As List(Of clsTankerDispatchPriceDetails) = Nothing
#End Region

    Public Shared Function SaveData(ByVal isNewEntry As Boolean, ByVal obj As clsTankerDispatchPriceMaster) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(isNewEntry, obj, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal isNewEntry As Boolean, ByVal obj As clsTankerDispatchPriceMaster, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            Dim qry As String = "delete from TSPL_TANKER_DISPATCH_PRICE_DETAILS where PRICE_CODE='" + obj.PRICE_CODE + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If isNewEntry = True Then
                obj.PRICE_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.TankerDispatchPriceMaster, "", "")
            End If
            If (clsCommon.myLen(obj.PRICE_CODE) <= 0) Then
                Throw New Exception("Error in Price Code Generation")
            End If

            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "PRICE_CODE", obj.PRICE_CODE) ' 
            clsCommon.AddColumnsForChange(coll, "PRICE_DATE", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "PRICE_DESC", obj.PRICE_DESC)
            clsCommon.AddColumnsForChange(coll, "EFFECTIVE_DATE", clsCommon.GetPrintDate(obj.EFFECTIVE_DATE, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "TOTAL_SOLID_RATE", obj.TOTAL_SOLID_RATE)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "isJobWork", obj.isJobWork)
            clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code, True)
            If isNewEntry = True Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TANKER_DISPATCH_PRICE_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TANKER_DISPATCH_PRICE_MASTER", OMInsertOrUpdate.Update, " TSPL_TANKER_DISPATCH_PRICE_MASTER.PRICE_CODE='" + obj.PRICE_CODE + "' ", trans)
            End If

            isSaved = isSaved AndAlso clsTankerDispatchPriceDetails.SaveData(obj.PRICE_CODE, obj.ArrMccCode, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(strDocNo, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception(" Price Code not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")

            Dim qry As String = "Update TSPL_TANKER_DISPATCH_PRICE_MASTER set Posted=1, Posting_Date='" + strPostDate + "',Posted_By='" + objCommonVar.CurrentUserCode + "' where PRICE_CODE='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strPriceCode As String, ByVal NavType As NavigatorType) As clsTankerDispatchPriceMaster
        Return GetData(strPriceCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsTankerDispatchPriceMaster
        Try
            Dim obj As clsTankerDispatchPriceMaster = Nothing

            Dim qry As String = " select * from TSPL_TANKER_DISPATCH_PRICE_MASTER "

            Select Case NavType
                Case NavigatorType.Current
                    qry += " where PRICE_CODE='" + strCode + "' "
                Case NavigatorType.First
                    qry += " where PRICE_CODE in (select min(PRICE_CODE) from TSPL_TANKER_DISPATCH_PRICE_MASTER )"
                Case NavigatorType.Last
                    qry += " where PRICE_CODE in (select max(PRICE_CODE) from TSPL_TANKER_DISPATCH_PRICE_MASTER )"
                Case NavigatorType.Next
                    qry += " where PRICE_CODE in (select min(PRICE_CODE) from TSPL_TANKER_DISPATCH_PRICE_MASTER where PRICE_CODE >'" + strCode + "' )"
                Case NavigatorType.Previous
                    qry += " where PRICE_CODE in (select max(PRICE_CODE) from TSPL_TANKER_DISPATCH_PRICE_MASTER where PRICE_CODE <'" + strCode + "' )"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New clsTankerDispatchPriceMaster()

                obj.PRICE_CODE = clsCommon.myCstr(dt.Rows(0)("PRICE_CODE"))
                obj.PRICE_DATE = clsCommon.myCDate(dt.Rows(0)("PRICE_DATE"))
                obj.PRICE_DESC = clsCommon.myCstr(dt.Rows(0)("PRICE_DESC"))
                obj.EFFECTIVE_DATE = clsCommon.myCDate(dt.Rows(0)("EFFECTIVE_DATE"))
                obj.TOTAL_SOLID_RATE = clsCommon.myCdbl(dt.Rows(0)("TOTAL_SOLID_RATE"))
                obj.Posted = clsCommon.myCdbl(dt.Rows(0)("Posted"))
                obj.isJobWork = clsCommon.myCBool(dt.Rows(0)("isJobWork"))
                obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))


                qry = "select * from TSPL_TANKER_DISPATCH_PRICE_DETAILS "
                qry += " where TSPL_TANKER_DISPATCH_PRICE_DETAILS.PRICE_CODE='" + obj.PRICE_CODE + "' ORDER BY TSPL_TANKER_DISPATCH_PRICE_DETAILS.PRICE_CODE asc"
                dt = New DataTable()
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    obj.ArrMccCode = New List(Of clsTankerDispatchPriceDetails)
                    Dim objTrTr As clsTankerDispatchPriceDetails
                    For Each dr As DataRow In dt.Rows
                        objTrTr = New clsTankerDispatchPriceDetails

                        objTrTr.PRICE_CODE = clsCommon.myCstr(dr("PRICE_CODE"))
                        objTrTr.MCC_CODE = clsCommon.myCstr(dr("MCC_CODE"))
                        obj.ArrMccCode.Add(objTrTr)
                    Next
                End If
            End If
            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Price Code not found to Delete")
        End If
        Dim obj As clsTankerDispatchPriceMaster = clsTankerDispatchPriceMaster.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.PRICE_CODE) > 0) Then
            Try
                If (obj.Posted = 1) Then
                    Throw New Exception("Already Posted")
                End If

                Dim qry As String = "delete from TSPL_TANKER_DISPATCH_PRICE_DETAILS where PRICE_CODE='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_TANKER_DISPATCH_PRICE_MASTER where PRICE_CODE='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

                If (isSaved) Then
                    trans.Commit()
                Else
                    trans.Rollback()
                End If
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
        Return isSaved
    End Function

    Public Shared Function GetLastestPriceChart(ByVal strMCCCode As String, ByVal TransDate As DateTime, ByVal trans As SqlTransaction) As clsTankerDispatchPriceMaster
        Dim obj As clsTankerDispatchPriceMaster = Nothing
        Dim qry As String = "select top 1 TSPL_TANKER_DISPATCH_PRICE_MASTER.PRICE_CODE,TSPL_TANKER_DISPATCH_PRICE_MASTER.TOTAL_SOLID_RATE from TSPL_TANKER_DISPATCH_PRICE_DETAILS" + Environment.NewLine + _
        "left outer join TSPL_TANKER_DISPATCH_PRICE_MASTER on TSPL_TANKER_DISPATCH_PRICE_MASTER.PRICE_CODE=TSPL_TANKER_DISPATCH_PRICE_DETAILS.PRICE_CODE" + Environment.NewLine + _
        "where TSPL_TANKER_DISPATCH_PRICE_DETAILS.MCC_CODE='" + strMCCCode + "' and TSPL_TANKER_DISPATCH_PRICE_MASTER.posted=1 and  TSPL_TANKER_DISPATCH_PRICE_MASTER.EFFECTIVE_DATE <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(TransDate), "dd/MMM/yyyy hh:mm:ss tt") + "' order by TSPL_TANKER_DISPATCH_PRICE_MASTER.EFFECTIVE_DATE desc,TSPL_TANKER_DISPATCH_PRICE_MASTER.PRICE_CODE desc"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsTankerDispatchPriceMaster()
            obj.PRICE_CODE = clsCommon.myCstr(dt.Rows(0)("PRICE_CODE"))
            obj.TOTAL_SOLID_RATE = clsCommon.myCdbl(dt.Rows(0)("TOTAL_SOLID_RATE"))
        End If
        Return obj
    End Function
    ''richa agarwal ERO/01/04/19-000537
    Public Shared Function GetLastestPriceChart(ByVal strMCCCode As String, ByVal strItemCode As String, ByVal TransDate As DateTime, ByVal trans As SqlTransaction) As clsTankerDispatchPriceMaster
        Dim obj As clsTankerDispatchPriceMaster = Nothing
        Dim qry As String = "select top 1 TSPL_TANKER_DISPATCH_PRICE_MASTER.PRICE_CODE,TSPL_TANKER_DISPATCH_PRICE_MASTER.TOTAL_SOLID_RATE from TSPL_TANKER_DISPATCH_PRICE_DETAILS" + Environment.NewLine + _
        "left outer join TSPL_TANKER_DISPATCH_PRICE_MASTER on TSPL_TANKER_DISPATCH_PRICE_MASTER.PRICE_CODE=TSPL_TANKER_DISPATCH_PRICE_DETAILS.PRICE_CODE" + Environment.NewLine + _
        "where TSPL_TANKER_DISPATCH_PRICE_DETAILS.MCC_CODE='" + strMCCCode + "' and TSPL_TANKER_DISPATCH_PRICE_MASTER.Item_Code='" + strItemCode + "' and TSPL_TANKER_DISPATCH_PRICE_MASTER.posted=1 and  TSPL_TANKER_DISPATCH_PRICE_MASTER.EFFECTIVE_DATE <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(TransDate), "dd/MMM/yyyy hh:mm:ss tt") + "' order by TSPL_TANKER_DISPATCH_PRICE_MASTER.EFFECTIVE_DATE desc,TSPL_TANKER_DISPATCH_PRICE_MASTER.PRICE_CODE desc"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsTankerDispatchPriceMaster()
            obj.PRICE_CODE = clsCommon.myCstr(dt.Rows(0)("PRICE_CODE"))
            obj.TOTAL_SOLID_RATE = clsCommon.myCdbl(dt.Rows(0)("TOTAL_SOLID_RATE"))
        End If
        Return obj
    End Function
End Class

Public Class clsTankerDispatchPriceDetails
#Region "Variables"
    Public PRICE_CODE As String = Nothing
    Public MCC_CODE As String = Nothing
#End Region
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsTankerDispatchPriceDetails), ByVal trans As SqlTransaction) As Boolean

        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsTankerDispatchPriceDetails In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "PRICE_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "MCC_CODE", obj.MCC_CODE)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TANKER_DISPATCH_PRICE_DETAILS", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    
End Class


