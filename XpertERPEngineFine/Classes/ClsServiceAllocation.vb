Imports System.Data.SqlClient
Imports common

Public Class ClsServiceAllocation
#Region "Variables"
    Public Service_Allocation_Code As String = String.Empty
    Public Service_Allocation_Date As String = String.Empty
    Public Service_Enquiry_Code As String = String.Empty
    Public Engineer_Code As String = String.Empty
    Public Created_By As String = String.Empty
    Public Created_Date As Date? = Nothing
    Public Modified_By As String = String.Empty
    Public Modified_Date As Date? = Nothing
#End Region
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function GetFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " Select Service_Allocation_Code AS [Code],CONVERT (VARCHAR,Service_Allocation_Date,103) AS [Date],Service_Enquiry_Code AS [Service Enquiry Code],Engineer_Code AS [Engineer Code],Created_By AS [Created By],CONVERT(varchar,Created_Date ,103) As [Created Date],Modified_By AS [Modified By],CONVERT(VARCHAR,modified_date,103) AS [Modified Date] From TSPL_SW_SERVICE_ALLOCATION  "
        str = clsCommon.ShowSelectForm("SWFauM", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    ''
    Public Shared Function SaveData(ByVal arr As List(Of ClsServiceAllocation)) As Boolean
        Dim trans As SqlTransaction = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin()

            If ClsServiceAllocation.SaveData(arr, trans, Nothing) Then
                trans.Commit()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function SaveData(ByVal arr As List(Of ClsServiceAllocation), ByVal trans As SqlTransaction, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = ""
        Dim IsSaved As Boolean = False
        Try
            For Each obj As ClsServiceAllocation In arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Service_Enquiry_Code", obj.Service_Enquiry_Code)
                clsCommon.AddColumnsForChange(coll, "Service_Allocation_Date", clsCommon.GetPrintDate(obj.Service_Allocation_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Engineer_Code", obj.Engineer_Code, True)
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select COUNT(*) from TSPL_SW_SERVICE_ALLOCATION WHERE Service_Allocation_Code='" + obj.Service_Allocation_Code + "'", trans)) <= 0 Then
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                    Dim qry1 As String = "SELECT Count(*) FROM TSPL_SW_SERVICE_ALLOCATION WHERE Service_Allocation_Code= '" & obj.Service_Allocation_Code & "'"
                    Dim check As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                    If check = 0 Then
                        obj.Service_Allocation_Code = clsERPFuncationality.GetNextCode(trans, obj.Service_Allocation_Date, clsDocType.SWServiceAllocation, "", "")
                        clsCommon.AddColumnsForChange(coll, "Service_Allocation_Code", obj.Service_Allocation_Code)
                        IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SW_SERVICE_ALLOCATION", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        Throw New Exception("This Code Is Already Exist")
                    End If
                Else
                    IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SW_SERVICE_ALLOCATION", OMInsertOrUpdate.Update, "TSPL_SW_SERVICE_ALLOCATION.Service_Allocation_Code='" + obj.Service_Allocation_Code + "'", trans)
                End If
                clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_SW_SERVICE_ENQUIRY SET Allocated=1 WHERE Service_Enquiry_Code ='" & obj.Service_Enquiry_Code & "'", trans)
            Next
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsServiceAllocation
        Dim obj As ClsServiceAllocation = Nothing
        Dim Arr As List(Of ClsServiceAllocation) = Nothing
        Dim qry As String = "SELECT * FROM TSPL_SW_SERVICE_ALLOCATION where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " AND TSPL_SW_SERVICE_ALLOCATION.Service_Allocation_Code = (select MIN(Service_Allocation_Code) FROM TSPL_SW_SERVICE_ALLOCATION WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " AND TSPL_SW_SERVICE_ALLOCATION.Service_Allocation_Code = (select Max(Service_Allocation_Code) FROM TSPL_SW_SERVICE_ALLOCATION WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " AND TSPL_SW_SERVICE_ALLOCATION.Service_Allocation_Code = (select TOP 1 Service_Allocation_Code FROM TSPL_SW_SERVICE_ALLOCATION WHERE 1=1 " + whrclas + " AND Service_Allocation_Code='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " AND TSPL_SW_SERVICE_ALLOCATION.Service_Allocation_Code = (select Min(Service_Allocation_Code) FROM TSPL_SW_SERVICE_ALLOCATION where Service_Allocation_Code > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " AND TSPL_SW_SERVICE_ALLOCATION.Service_Allocation_Code = (select Max(Service_Allocation_Code) FROM TSPL_SW_SERVICE_ALLOCATION where Service_Allocation_Code < '" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsServiceAllocation()
            obj.Service_Allocation_Code = clsCommon.myCstr(dt.Rows(0)("Service_Allocation_Code"))
            obj.Service_Allocation_Date = clsCommon.myCDate(dt.Rows(0)("Service_Allocation_Date"))
            obj.Service_Enquiry_Code = clsCommon.myCstr(dt.Rows(0)("Service_Enquiry_Code"))
            obj.Engineer_Code = clsCommon.myCstr(dt.Rows(0)("Engineer_Code"))
        End If
        Return obj
    End Function
End Class
