Imports common
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Public Class clsCreateDashboard
#Region "Variables"
    Public Code As String = ""
    Public Description As String = ""
    Public Report_Module As String = ""
    Public arr As List(Of clsCreateDashboardDetails)
#End Region

    Public Function SaveData(ByVal obj As clsCreateDashboard, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = ""
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Report_Module", obj.Report_Module)

            If isNewEntry Then
                If clsCommon.myLen(obj.Code) <= 0 Then
                    qry = " select max(Code) from TSPL_CREATE_DASHBOARD"
                    obj.Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                    If clsCommon.myLen(obj.Code) > 0 Then
                        obj.Code = clsCommon.incval(obj.Code)
                    Else
                        obj.Code = "BIDB000001"
                    End If
                End If
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CREATE_DASHBOARD", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CREATE_DASHBOARD", OMInsertOrUpdate.Update, " code ='" + obj.Code + "'", trans)
            End If
            clsCreateDashboardDetails.SaveData(obj.Code, obj.arr, trans)
            ProgramCodeNew.AddCustomLinks(obj.Code, obj.Description, obj.Report_Module, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsCreateDashboard
        Dim qry As String = "select TSPL_CREATE_DASHBOARD.* from TSPL_CREATE_DASHBOARD  where 2=2"
        Dim whrclas As String = ""
        'If Not ShowAllReport Then
        '    whrclas += " and TSPL_CREATE_DASHBOARD.Is_Create_By_Developer=0"
        'End If
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_CREATE_DASHBOARD.Code = (select MIN(Code) from TSPL_CREATE_DASHBOARD WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_CREATE_DASHBOARD.Code = (select Max(Code) from TSPL_CREATE_DASHBOARD WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_CREATE_DASHBOARD.Code = '" + strCode + "'  " + whrclas + ""
            Case NavigatorType.Next
                qry += " and TSPL_CREATE_DASHBOARD.Code = (select Min(Code) from TSPL_CREATE_DASHBOARD where  Code>'" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_CREATE_DASHBOARD.Code = (select Max(Code) from TSPL_CREATE_DASHBOARD where Code<'" + strCode + "' " + whrclas + ")"
        End Select
        Dim obj As clsCreateDashboard = Nothing
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim Arr As List(Of clsCreateDashboard) = Nothing
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsCreateDashboard()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Report_Module = clsCommon.myCstr(dt.Rows(0)("Report_Module"))
            obj.arr = New List(Of clsCreateDashboardDetails)
            qry = "select TSPL_CREATE_DASHBOARD_DETAIL.*,TSPL_CREATE_BI_REPORT.Description as Report_Description from TSPL_CREATE_DASHBOARD_DETAIL left outer join TSPL_CREATE_BI_REPORT on TSPL_CREATE_BI_REPORT.Code=TSPL_CREATE_DASHBOARD_DETAIL.Report_Code  where TSPL_CREATE_DASHBOARD_DETAIL.code='" + obj.Code + "'"
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                For Each dr As DataRow In dt1.Rows
                    Dim objtr As New clsCreateDashboardDetails()
                    objtr.Code = clsCommon.myCstr(dr("Code"))
                    objtr.Report_Code = clsCommon.myCstr(dr("Report_Code"))
                    objtr.Report_Description = clsCommon.myCstr(dr("Report_Description"))
                    obj.arr.Add(objtr)
                Next
            End If
        End If
        Return obj
    End Function

    Public Shared Function DeleteData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "delete from TSPL_GROUP_PROGRAM_MAPPING where Program_Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "Delete from TSPL_PROGRAM_MASTER where Program_Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "Delete from TSPL_CREATE_DASHBOARD_DETAIL where Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "Delete from TSPL_CREATE_DASHBOARD where Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

        Return True
    End Function

End Class

Public Class clsCreateDashboardDetails
#Region "Variables"
    Public Code As String = Nothing
    Public Report_Code As String = Nothing
    Public Report_Description As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal arr As List(Of clsCreateDashboardDetails), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_CREATE_DASHBOARD_DETAIL where Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            For Each objtr As clsCreateDashboardDetails In arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Code", strCode)
                clsCommon.AddColumnsForChange(coll, "Report_Code", objtr.Report_Code)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CREATE_DASHBOARD_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class
