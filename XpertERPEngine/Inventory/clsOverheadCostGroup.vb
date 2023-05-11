Imports System.Data.SqlClient
Imports common
Public Class clsOverheadCostGroupHead
#Region "Variables"
    Public GROUP_CODE As String
    Public GROUP_DATE As Date?
    Public Description As String
    Public Created_By As String
    Public Created_Date As Date?
    Public Modify_By As String
    Public Modify_Date As Date?
    Public Comp_code As String
    Public Arr As List(Of clsOverheadCostGroupDetails) = Nothing
#End Region

    Public Shared Function SaveData(ByVal obj As clsOverheadCostGroupHead, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = ""
        Dim IsSaved As Boolean = False
        Try
            qry = "delete from TSPL_OVERHEAD_COST_GROUP_DETAILS where GROUP_CODE='" + obj.GROUP_CODE + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "GROUP_DATE", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Comp_code", objCommonVar.CurrentCompanyCode)
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "GROUP_CODE", obj.GROUP_CODE)
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_OVERHEAD_COST_GROUP_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_OVERHEAD_COST_GROUP_HEAD", OMInsertOrUpdate.Update, "GROUP_CODE='" + obj.GROUP_CODE + "'", trans)
            End If
            Dim objtr As New clsOverheadCostGroupDetails
            objtr.SaveData(obj.GROUP_CODE, obj.Arr, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal tran As SqlTransaction) As clsOverheadCostGroupHead
        Dim qry As String = "select * from TSPL_OVERHEAD_COST_GROUP_HEAD Where 2=2 "
        Dim whrClas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_OVERHEAD_COST_GROUP_HEAD.GROUP_CODE = (select MIN(GROUP_CODE) from TSPL_OVERHEAD_COST_GROUP_HEAD where 1=1 " + whrClas + ")"
            Case NavigatorType.Last
                qry += " and TSPL_OVERHEAD_COST_GROUP_HEAD.GROUP_CODE = (select Max(GROUP_CODE) from TSPL_OVERHEAD_COST_GROUP_HEAD where 1=1 " + whrClas + ")"
            Case NavigatorType.Next
                qry += " and TSPL_OVERHEAD_COST_GROUP_HEAD.GROUP_CODE = (select Min(GROUP_CODE) from TSPL_OVERHEAD_COST_GROUP_HEAD where GROUP_CODE >'" + strDocNo + "' " + whrClas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_OVERHEAD_COST_GROUP_HEAD.GROUP_CODE = (select Max(GROUP_CODE) from TSPL_OVERHEAD_COST_GROUP_HEAD where GROUP_CODE <'" + strDocNo + "' " + whrClas + ")"
            Case NavigatorType.Current
                qry += " and TSPL_OVERHEAD_COST_GROUP_HEAD.GROUP_CODE = '" + strDocNo + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
        Dim obj As clsOverheadCostGroupHead = Nothing
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsOverheadCostGroupHead()
            obj.GROUP_CODE = clsCommon.myCstr(dt.Rows(0)("GROUP_CODE"))
            obj.GROUP_DATE = clsCommon.myCDate(dt.Rows(0)("GROUP_DATE"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Created_By = clsCommon.myCstr(dt.Rows(0)("Created_By"))
            obj.Created_Date = clsCommon.myCstr(dt.Rows(0)("Created_Date"))
            qry = " Select SNO,GROUP_CODE,COST_CODE , RatePerHour,Hours,NO,Cost from  TSPL_OVERHEAD_COST_GROUP_DETAILS where GROUP_CODE= '" + obj.GROUP_CODE + "' "
            dt = clsDBFuncationality.GetDataTable(qry, tran)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsOverheadCostGroupDetails)
                For Each dr As DataRow In dt.Rows
                    Dim objtr As New clsOverheadCostGroupDetails
                    objtr.SNO = clsCommon.myCdbl(dr("SNO"))
                    objtr.GROUP_CODE = clsCommon.myCstr(dr("GROUP_CODE"))
                    objtr.COST_CODE = clsCommon.myCstr(dr("COST_CODE"))
                    objtr.COST_DESC = clsDBFuncationality.getSingleValue(" Select Description from TSPL_OVERHEAD_COST where Cost_Code='" + objtr.COST_CODE + "' ")
                    If String.IsNullOrEmpty(clsCommon.myCstr(dr("RatePerHour"))) = False Then
                        objtr.RatePerHour = clsCommon.myCdbl(dr("RatePerHour"))
                    Else
                        objtr.RatePerHour = 0
                    End If
                    If String.IsNullOrEmpty(clsCommon.myCstr(dr("Hours"))) = False Then
                        objtr.Hours = clsCommon.myCdbl(dr("Hours"))
                    Else
                        objtr.Hours = 0
                    End If
                    If String.IsNullOrEmpty(clsCommon.myCstr(dr("NO"))) = False Then
                        objtr.NO = clsCommon.myCdbl(dr("NO"))
                    Else
                        objtr.NO = 0
                    End If
                    If String.IsNullOrEmpty(clsCommon.myCstr(dr("Cost"))) = False Then
                        objtr.Cost = clsCommon.myCdbl(dr("Cost"))
                    Else
                        objtr.Cost = 0
                    End If
                    obj.Arr.Add(objtr)
                Next
            End If
        End If
        Return obj
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Group Code not found to Delete")
        End If
        Dim obj As clsOverheadCostGroupHead = clsOverheadCostGroupHead.GetData(strCode, NavigatorType.Current, Nothing)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.GROUP_CODE) > 0) Then
            Try
                Dim qry As String = "delete from TSPL_OVERHEAD_COST_GROUP_DETAILS where GROUP_CODE='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_OVERHEAD_COST_GROUP_HEAD  where GROUP_CODE='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
                trans.Commit()
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
        Return isSaved
    End Function

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "  select GROUP_CODE as Code ,Description , Created_By ,convert(varchar, Created_Date,103) as Created_Date  from TSPL_OVERHEAD_COST_GROUP_HEAD "
        str = clsCommon.ShowSelectForm("OverCostGroupFnd", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str

    End Function

    Public Shared Function CheckNewEntry(ByVal Code As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select GROUP_CODE from TSPL_OVERHEAD_COST_GROUP_HEAD where GROUP_CODE ='" + Code + "'   "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If
    End Function

End Class

Public Class clsOverheadCostGroupDetails
#Region "Variables"
    Public GROUP_CODE As String
    Public SNO As Integer
    Public COST_CODE As String
    Public COST_DESC As String
    Public RatePerHour As Double = Nothing
    Public Hours As Double = Nothing
    Public NO As Double = Nothing
    Public Cost As Double = Nothing
#End Region
    Public Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsOverheadCostGroupDetails), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim ii As Integer = 0
            For Each objtr As clsOverheadCostGroupDetails In Arr
                ii += 1
                objtr.SNO = ii
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "SNO", objtr.SNO)
                clsCommon.AddColumnsForChange(coll, "GROUP_CODE", objtr.GROUP_CODE)
                clsCommon.AddColumnsForChange(coll, "COST_CODE", objtr.COST_CODE)
                clsCommon.AddColumnsForChange(coll, "RatePerHour", objtr.RatePerHour, True)
                clsCommon.AddColumnsForChange(coll, "Hours", objtr.Hours, True)
                clsCommon.AddColumnsForChange(coll, "NO", objtr.NO, True)
                clsCommon.AddColumnsForChange(coll, "Cost", objtr.Cost, True)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_OVERHEAD_COST_GROUP_DETAILS", OMInsertOrUpdate.Insert, "", trans)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class
