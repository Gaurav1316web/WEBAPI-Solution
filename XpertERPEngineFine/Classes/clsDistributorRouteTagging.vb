Imports System.Data.SqlClient

Public Class clsDistributorRouteTagging
    Public Code As String = Nothing
    Public Start_Date As Date
    Public End_Date As Date? = Nothing
    Public Remarks As String
    Public IS_Transpoter As Boolean = False
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Created_By As String = Nothing
    Public Created_Date As DateTime
    Public Modified_By As String = Nothing
    Public Modified_Date As DateTime
    Public Post_By As String = Nothing
    Public Post_Date As DateTime
    Public Arr As List(Of clsDistributorRouteTaggingDetail) = Nothing
    Public Function SaveData(ByVal obj As clsDistributorRouteTagging, ByVal isNewEntry As Boolean) As Boolean
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
    Public Shared Function SaveData(ByVal obj As clsDistributorRouteTagging, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim IsSaved As Boolean = True
        Try
            IsSaved = True
            Dim StrQry As String = "delete from TSPL_DISTRIBUTOR_ROUTE_CUSTOMER where Code='" + obj.Code + "'"
            clsDBFuncationality.ExecuteNonQuery(StrQry, trans)


            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "IS_Transpoter", IIf(obj.IS_Transpoter, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Start_Date", clsCommon.GetPrintDate(obj.Start_Date, "dd/MMM/yyyy"))
            If obj.End_Date IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "End_Date", clsCommon.GetPrintDate(obj.End_Date, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "End_Date", Nothing, True)
            End If
            If isNewEntry Then
                obj.Code = clsERPFuncationality.GetNextCode(trans, DateTime.Now, clsDocType.DistributorRouteTagging, "", "")
                If clsCommon.myLen(obj.Code) <= 0 Then
                    Throw New Exception("Error in Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DISTRIBUTOR_ROUTE", OMInsertOrUpdate.Insert, "", trans)
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DISTRIBUTOR_ROUTE", OMInsertOrUpdate.Update, "TSPL_DISTRIBUTOR_ROUTE.Code='" + obj.Code + "'", trans)
            End If

            IsSaved = IsSaved AndAlso clsDistributorRouteTaggingDetail.SaveData(obj.Code, obj.Arr, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsDistributorRouteTagging
        Dim obj As clsDistributorRouteTagging = Nothing

        Try
            Dim strQry As String = "SELECT Code,Start_Date,End_Date,Remarks,Status,IS_Transpoter FROM TSPL_DISTRIBUTOR_ROUTE where 1=1 "
            Select Case NavType
                Case NavigatorType.First
                    strQry += " and Code = (select MIN(Code) from TSPL_DISTRIBUTOR_ROUTE where 1=1  )"
                Case NavigatorType.Last
                    strQry += " And Code = (Select Max(Code) from TSPL_DISTRIBUTOR_ROUTE where 1=1 )"
                Case NavigatorType.Next
                    strQry += " And Code = (Select Min(Code) from TSPL_DISTRIBUTOR_ROUTE where Code>'" + clsCommon.myCstr(strCode) + "' )"
                Case NavigatorType.Previous
                    strQry += " and Code = (select Max(Code) from TSPL_DISTRIBUTOR_ROUTE where Code<'" + clsCommon.myCstr(strCode) + "' )"
                Case NavigatorType.Current
                    strQry += " and Code = '" + clsCommon.myCstr(strCode) + "' "
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New clsDistributorRouteTagging()
                obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
                obj.Start_Date = clsCommon.GetPrintDate(dt.Rows(0)("Start_Date"), "dd/MMM/yyyy")
                If dt.Rows(0)("End_Date") IsNot DBNull.Value Then
                    obj.End_Date = clsCommon.GetPrintDate(dt.Rows(0)("End_Date"), "dd/MMM/yyyy")

                End If
                obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
                obj.IS_Transpoter = IIf(clsCommon.myCdbl(dt.Rows(0)("IS_Transpoter")) = 1, True, False)
                obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
                obj.Arr = clsDistributorRouteTaggingDetail.GetData(obj.Code, trans)
            End If

        Catch err As Exception
            Throw New Exception(err.Message)
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
                Throw New Exception("Code not found to Post")
            End If
            Dim obj As clsDistributorRouteTagging = clsDistributorRouteTagging.GetData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Code) <= 0) Then
                Throw New Exception("Code : " + strDocNo + " not found to Post")
            End If
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Posted on :" + obj.Post_Date)
            End If


            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Status", 1)
            clsCommon.AddColumnsForChange(coll, "Post_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Post_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DISTRIBUTOR_ROUTE", OMInsertOrUpdate.Update, "Code='" + clsCommon.myCstr(obj.Code) + "'", trans)

        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        'whrcls = "  IsDistributor='Y' "
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
    Public Shared Function DeleteData(ByVal StrCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            isSaved = DeleteData(StrCode, trans)
            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal StrCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = False
        Try
            If (clsCommon.myLen(StrCode) <= 0) Then
                Throw New Exception("Code No. not found to Delete")
            End If
            Dim qry As String = ""
            qry = "delete from TSPL_DISTRIBUTOR_ROUTE_CUSTOMER where Code='" + StrCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_DISTRIBUTOR_ROUTE where Code='" + StrCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
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
Public Class clsDistributorRouteTaggingDetail
    Public Cust_Code As String = Nothing
    Public Route_No As String = Nothing
    Public Route_Desc As String = Nothing
    Public Customer_Name As String = Nothing
    Public Code As String = Nothing

    Public Shared Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsDistributorRouteTaggingDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsDistributorRouteTaggingDetail In Arr
                    Dim colm As New Hashtable()
                    clsCommon.AddColumnsForChange(colm, "Code", strCode)
                    clsCommon.AddColumnsForChange(colm, "Route_No", obj.Route_No)
                    clsCommon.AddColumnsForChange(colm, "Cust_Code", obj.Cust_Code)
                    clsCommonFunctionality.UpdateDataTable(colm, "TSPL_DISTRIBUTOR_ROUTE_CUSTOMER", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return True
    End Function
    Public Shared Function GetData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As List(Of clsDistributorRouteTaggingDetail)
        Dim arr As List(Of clsDistributorRouteTaggingDetail) = Nothing

        Try
            Dim dt As DataTable
            Dim strQry As String = "select Code,Route_No,Cust_Code from TSPL_DISTRIBUTOR_ROUTE_CUSTOMER where Code='" & strDocNo & "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(strQry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                arr = New List(Of clsDistributorRouteTaggingDetail)
                Dim objTr As clsDistributorRouteTaggingDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsDistributorRouteTaggingDetail
                    objTr.Code = clsCommon.myCstr(dr("Code"))
                    objTr.Route_No = clsCommon.myCstr(dr("Route_No"))
                    objTr.Route_Desc = clsDBFuncationality.getSingleValue("select Route_Desc from TSPL_ROUTE_MASTER where Route_no='" + clsCommon.myCstr(dr("Route_No")) + "'", trans)
                    objTr.Cust_Code = clsCommon.myCstr(dr("Cust_Code"))
                    objTr.Customer_Name = clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" + clsCommon.myCstr(dr("Cust_Code")) + "'", trans)
                    arr.Add(objTr)
                Next
            End If



        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return arr
    End Function
End Class
