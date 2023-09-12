Imports common
Imports System.Data.SqlClient
Public Class clsDistributeRateTagging
    Public Code As String = Nothing
    Public Start_Date As Date
    Public End_Date As Date? = Nothing
    Public Remarks As String
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Created_By As String = Nothing
    Public Created_Date As DateTime
    Public Modified_By As String = Nothing
    Public Modified_Date As DateTime
    Public Post_By As String = Nothing
    Public Post_Date As DateTime
    Public arr As List(Of clsDistributeRateTaggingDetail) = Nothing

    Public Shared Function SaveData(ByVal obj As clsDistributeRateTagging, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function Code_Navigation(ByVal NavType As NavigatorType, ByVal Code As String)

        Dim sql As String = "select Code from TSPL_DISTRIBUTOR_ROUTE   where  2=2 "
        Select Case NavType
            Case NavigatorType.Current
            Case NavigatorType.Next
                sql += "and Code in (select min(Code) from TSPL_DISTRIBUTOR_ROUTE where Code>'" + Code + "'   ) "
            Case NavigatorType.First
                sql += "and Code in (select MIN(Code) from TSPL_DISTRIBUTOR_ROUTE  )"
            Case NavigatorType.Last
                sql += "and Code in (select Max(Code) from TSPL_DISTRIBUTOR_ROUTE  )"
            Case NavigatorType.Previous
                sql += "and Code in (select max(Code) from TSPL_DISTRIBUTOR_ROUTE where Code<'" + Code + "'   )"
        End Select

        Return clsDBFuncationality.getSingleValue(sql)

    End Function


    Public Shared Function PostData(ByVal obj As clsDistributeRateTagging) As Boolean
        Dim qry As String = ""
        Dim IsSaved As Boolean = False
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Status", clsCommon.myCdbl(ERPTransactionStatus.Approved))
            clsCommon.AddColumnsForChange(coll, "Post_By", obj.Post_By)
            clsCommon.AddColumnsForChange(coll, "Post_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
            IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DISTRIBUTOR_ROUTE", OMInsertOrUpdate.Update, "TSPL_DISTRIBUTOR_ROUTE.Code='" + obj.Code + "'", Nothing)

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    Public Shared Function SaveData(ByVal obj As clsDistributeRateTagging, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = ""
        Dim IsSaved As Boolean = True
        Dim strCode As String = ""
        Dim chkqry As String = "delete from TSPL_DISTRIBUTOR_ROUTE_CUSTOMER where Code='" + obj.Code + "'"
        IsSaved = IsSaved AndAlso clsDBFuncationality.ExecuteNonQuery(chkqry, trans)

        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Start_Date", clsCommon.GetPrintDate(obj.Start_Date, "dd/MMM/yyyy"))
            If obj.End_Date IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "End_Date", clsCommon.GetPrintDate(obj.End_Date, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "End_Date", Nothing, True)
            End If
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DISTRIBUTOR_ROUTE", OMInsertOrUpdate.Insert, "", trans)
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DISTRIBUTOR_ROUTE", OMInsertOrUpdate.Update, "TSPL_DISTRIBUTOR_ROUTE.Code='" + obj.Code + "'", trans)
            End If

            IsSaved = IsSaved AndAlso clsDistributeRateTaggingDetail.SaveData(obj.Code, obj.arr, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsDistributeRateTagging
        Try
            Dim obj As clsDistributeRateTagging = Nothing
            Dim qry As String = "SELECT 
                    TSPL_DISTRIBUTOR_ROUTE.Code,TSPL_DISTRIBUTOR_ROUTE.Start_Date,TSPL_DISTRIBUTOR_ROUTE.End_Date,TSPL_DISTRIBUTOR_ROUTE.Remarks,TSPL_DISTRIBUTOR_ROUTE.Status
                    FROM TSPL_DISTRIBUTOR_ROUTE                
                     WHERE TSPL_DISTRIBUTOR_ROUTE.Code='" + strCode + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New clsDistributeRateTagging()
                obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
                obj.Start_Date = clsCommon.GetPrintDate(dt.Rows(0)("Start_Date"), "dd/MMM/yyyy")
                If dt.Rows(0)("End_Date") IsNot DBNull.Value Then
                    obj.End_Date = clsCommon.GetPrintDate(dt.Rows(0)("End_Date"), "dd/MMM/yyyy")

                End If
                obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
                obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)

                qry = "	select ROW_NUMBER() OVER(PARTITION BY 1 ORDER BY TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.CODE) AS Sno
                   ,TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Route_No,TSPL_ROUTE_MASTER.Route_Desc,
                   TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name
                   from TSPL_DISTRIBUTOR_ROUTE_CUSTOMER

  left outer join TSPL_ROUTE_MASTER on TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Route_No=TSPL_ROUTE_MASTER.Route_No
    left outer join TSPL_CUSTOMER_MASTER on TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code
                   WHERE TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Code='" & strCode & "'"
                dt = New DataTable()
                dt = clsDBFuncationality.GetDataTable(qry)
                If (dt.Rows.Count > 0) Then
                    obj.arr = New List(Of clsDistributeRateTaggingDetail)
                    Dim objTr As clsDistributeRateTaggingDetail
                    For Each dr As DataRow In dt.Rows
                        objTr = New clsDistributeRateTaggingDetail
                        objTr.SNo = clsCommon.myCdbl(dr("SNo"))
                        objTr.Route_No = clsCommon.myCstr(dr("Route_No"))
                        objTr.Route_Desc = clsCommon.myCstr(dr("Route_Desc"))
                        objTr.Cust_Code = clsCommon.myCstr(dr("Cust_Code"))
                        objTr.Customer_Name = clsCommon.myCstr(dr("Customer_Name"))
                        obj.arr.Add(objTr)
                    Next
                Else
                    clsCommon.MyMessageBoxShow("Data not found")
                End If
            End If
            Return obj
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try

    End Function
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        whrcls = "  IsDistributor='Y' "
        Dim qry As String = "select Cust_Code as [Code] ,Customer_Name as [Customer Name] ,Add1 as [Address1] ,Add2 as [Address2] ,Add3 as [Address3] ,City_Code as [City Code] ,State as [State] ,Country as [Country] ,Phone1 as [Phone1] ,Phone2 as [Phone2] ,Fax as [Fax] ,Email as [Email] ,WebSite as [Website] ,Credit_Limit as [Credit Limit] ,CURRENCY_CODE as [Currency Code] ,Status as [Status] ,Created_By as [Created By] ,Created_Date as [Created Date] ,Modify_By as [Modified By] ,Modify_Date as [Modified Date] ,Comp_Code as [Company Code]  From TSPL_CUSTOMER_MASTER "
        str = clsCommon.ShowSelectForm("SECCUSTFIND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    Public Shared Function getRouteFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "select Route_No as Route_Code,Route_Desc as Route_Name  ,City_Code as [City Code] ,Comp_Code as [Company Code]  From TSPL_ROUTE_MASTER"
        str = clsCommon.ShowSelectForm("SEROUTEFIND", qry, "Route_Code", "", curcode, "Route_Code", isButtonClicked)
        Return str
    End Function
    Public Shared Function getCodeFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        'whrcls = "  IsDistributor='Y' "
        Dim qry As String = "select Code as [Code] ,Start_Date as [Start Date] ,End_Date as [End Date] ,Remarks as [Remarks]  From TSPL_DISTRIBUTOR_ROUTE "
        str = clsCommon.ShowSelectForm("AreaFnd", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    Public Shared Function DeleteData(ByVal code As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            isSaved = isSaved AndAlso DeleteData(code, trans)
            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal Code As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = False

        If (clsCommon.myLen(Code) <= 0) Then
            Throw New Exception("Code No. not found to Delete")
        End If
        Dim qry As String = ""
        qry = "delete from TSPL_DISTRIBUTOR_ROUTE_CUSTOMER where Code='" + Code + "'"
        isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
        qry = "delete from TSPL_DISTRIBUTOR_ROUTE where Code='" + Code + "'"
        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Return isSaved
    End Function
    Public Shared Function CheckForReasonOnDelete(Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim strq As String
        strq = "SELECT Code FROM TSPL_DISTRIBUTOR_ROUTE_CUSTOMER where Code= 'DisplayReasonOnDelete'"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(strq, trans)
        If dt.Rows.Count = 0 Then
            Return False
        ElseIf dt.Rows(0).Item("Code") = 0 Then
            Return False
        ElseIf dt.Rows(0).Item("Code") = 1 Then
            Return True
        End If
        Return True
    End Function
End Class

Public Class clsDistributeRateTaggingDetail
    Public Cust_Code As String = Nothing
    Public Route_No As String = Nothing
    Public Route_Desc As String = Nothing
    Public Customer_Name As String = Nothing
    Public SNo As Double = 0
    Public Code As String = Nothing
    Public strCode As String = Nothing

    Public Shared Function SaveData(ByVal code As String, ByVal Arr As List(Of clsDistributeRateTaggingDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsDistributeRateTaggingDetail In Arr
                Dim colm As New Hashtable()
                clsCommon.AddColumnsForChange(colm, "Code", code)
                clsCommon.AddColumnsForChange(colm, "Route_No", obj.Route_No)
                clsCommon.AddColumnsForChange(colm, "Cust_Code", obj.Cust_Code)
                clsCommonFunctionality.UpdateDataTable(colm, "TSPL_DISTRIBUTOR_ROUTE_CUSTOMER", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

End Class
