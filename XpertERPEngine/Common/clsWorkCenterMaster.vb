Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsWorkCenterMaster

#Region "Variables"
    Public WORK_CENTER_CODE As String
    Public Description As String
    Public SETUP_TIME As Double
    Public SETUP_TIME_TYPE As String
    Public RUN_TIME As Double
    Public RUN_TIME_TYPE As String
    Public CLEANUP_TIME As Double
    Public CLEANUP_TIME_TYPE As String
    Public WAIT_TIME As Double
    Public WAIT_TIME_TYPE As String
    Public WORK_AREA As String
    Public NO_OF_STATIONS As Int16
    Public STD_SETUP_LABOR As Double
    Public STD_RUN_LABOR As Double
    Public STD_EFFICIENCY As Double
    Public STD_UTILIZATION As Double
    Public COMMENTS As String
    Public Modified_By As String
    Public Modified_Date As String
    Public TotalResourceCost As Double
    Public TotalToolCost As Double


    Dim ObjShift As New clsWorkCenterShiftDetail
    Public ObjList_ShiftDetails As List(Of clsWorkCenterShiftDetail)

    Dim ObjResource As New clsWorkCenterResourceDetail
    Public ObjList_resourceDetails As List(Of clsWorkCenterResourceDetail)

    Dim ObjToolDetails As New clsWorkCenterToolDetail
    Public ObjList_ToolDetails As List(Of clsWorkCenterToolDetail)

#End Region

    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_MF_WORK_CENTER.WORK_CENTER_CODE as [Code] ,TSPL_MF_WORK_CENTER.DESCRIPTION as [Description] ,TSPL_MF_WORK_CENTER.SETUP_TIME as [Setup Time] ,TSPL_MF_WORK_CENTER.SETUP_TIME_TYPE as [Setup Time Type] ,TSPL_MF_WORK_CENTER.RUN_TIME as [Run Time] ,TSPL_MF_WORK_CENTER.RUN_TIME_TYPE as [Run Time Type] ,TSPL_MF_WORK_CENTER.CLEANUP_TIME as [Cleanup Time] ,TSPL_MF_WORK_CENTER.CLEANUP_TIME_TYPE as [Cleanup Time Type] ,TSPL_MF_WORK_CENTER.WAIT_TIME as [Wait Time] ,TSPL_MF_WORK_CENTER.WAIT_TIME_TYPE as [Wait Time Type] ,TSPL_MF_WORK_CENTER.WORK_AREA as [Work Area] ,TSPL_MF_WORK_CENTER.NO_OF_STATIONS as [No Of Stations] ,TSPL_MF_WORK_CENTER.STD_SETUP_LABOR as [Std Setup Labor] ,TSPL_MF_WORK_CENTER.STD_RUN_LABOR as [Std Run Labor] ,TSPL_MF_WORK_CENTER.STD_EFFICIENCY as [Std Efficiency] ,TSPL_MF_WORK_CENTER.STD_UTILIZATION as [Std Utilization] ,TSPL_MF_WORK_CENTER.COMMENTS as [Comments] ,TSPL_MF_WORK_CENTER.Created_By as [Created By] ,TSPL_MF_WORK_CENTER.Created_Date as [Created Date] ,TSPL_MF_WORK_CENTER.Modified_By as [Modified By] ,TSPL_MF_WORK_CENTER.Modified_Date as [Modified Date] ,TSPL_MF_WORK_CENTER.TotalResourceCost as [Total Resource Cost] ,TSPL_MF_WORK_CENTER.TotalToolCost as [Total Tool Cost]  From TSPL_MF_WORK_CENTER   "
        str = clsCommon.ShowSelectForm("WRKCNTRMST", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsWorkCenterMaster
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean

        Try
            isSaved = True
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception(" Code not found to Delete")
            End If

            Dim qry As String

            qry = " delete from TSPL_MF_WORK_CENTER_SHIFT_DETAIL where WORK_CENTER_CODE ='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry)

            qry = " delete from TSPL_MF_WORK_CENTER_RESOURCE_DETAIL where WORK_CENTER_CODE ='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry)

            qry = " delete from TSPL_MF_WORK_CENTER_TOOL_DETAIL where WORK_CENTER_CODE ='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry)

            qry = "delete from TSPL_MF_WORK_CENTER where WORK_CENTER_CODE ='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsWorkCenterMaster
        Dim obj As clsWorkCenterMaster = Nothing
        Dim qry As String = "select * from TSPL_MF_WORK_CENTER  where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and WORK_CENTER_CODE = (select MIN(WORK_CENTER_CODE) from TSPL_MF_WORK_CENTER)"
            Case NavigatorType.Last
                qry += " and WORK_CENTER_CODE = (select Max(WORK_CENTER_CODE) from TSPL_MF_WORK_CENTER)"
            Case NavigatorType.Next
                qry += " and WORK_CENTER_CODE = (select Min(WORK_CENTER_CODE) from TSPL_MF_WORK_CENTER where  WORK_CENTER_CODE > '" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and WORK_CENTER_CODE = (select Max(WORK_CENTER_CODE) from TSPL_MF_WORK_CENTER where WORK_CENTER_CODE < '" + strCode + "')"
            Case NavigatorType.Current
                qry += " and WORK_CENTER_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsWorkCenterMaster()
            obj.WORK_CENTER_CODE = clsCommon.myCstr(dt.Rows(0)("WORK_CENTER_CODE"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
            obj.SETUP_TIME = clsCommon.myCdbl(dt.Rows(0)("SETUP_TIME"))
            obj.SETUP_TIME_TYPE = clsCommon.myCstr(dt.Rows(0)("SETUP_TIME_TYPE"))
            obj.RUN_TIME = clsCommon.myCdbl(dt.Rows(0)("RUN_TIME"))
            obj.RUN_TIME_TYPE = clsCommon.myCstr(dt.Rows(0)("RUN_TIME_TYPE"))
            obj.CLEANUP_TIME = clsCommon.myCdbl(dt.Rows(0)("CLEANUP_TIME"))
            obj.CLEANUP_TIME_TYPE = clsCommon.myCstr(dt.Rows(0)("CLEANUP_TIME_TYPE"))
            obj.WAIT_TIME = clsCommon.myCdbl(dt.Rows(0)("WAIT_TIME"))
            obj.WAIT_TIME_TYPE = clsCommon.myCstr(dt.Rows(0)("WAIT_TIME_TYPE"))
            obj.WORK_AREA = clsCommon.myCstr(dt.Rows(0)("WORK_AREA"))
            obj.NO_OF_STATIONS = Convert.ToInt16(clsCommon.myCdbl(dt.Rows(0)("NO_OF_STATIONS")))
            obj.STD_SETUP_LABOR = clsCommon.myCdbl(dt.Rows(0)("STD_SETUP_LABOR"))
            obj.STD_RUN_LABOR = clsCommon.myCdbl(dt.Rows(0)("STD_RUN_LABOR"))
            obj.STD_EFFICIENCY = clsCommon.myCdbl(dt.Rows(0)("STD_EFFICIENCY"))
            obj.STD_UTILIZATION = clsCommon.myCdbl(dt.Rows(0)("STD_UTILIZATION"))
            obj.COMMENTS = clsCommon.myCstr(dt.Rows(0)("COMMENTS"))
            obj.Modified_By = clsCommon.myCstr(dt.Rows(0)("Modified_By"))
            obj.Modified_Date = clsCommon.myCstr(dt.Rows(0)("Modified_Date"))
            obj.TotalResourceCost = clsCommon.myCdbl(dt.Rows(0)("TotalResourceCost"))
            obj.TotalToolCost = clsCommon.myCdbl(dt.Rows(0)("TotalToolCost"))

            obj.ObjList_ShiftDetails = clsWorkCenterShiftDetail.GetData(obj.WORK_CENTER_CODE, trans)
            obj.ObjList_resourceDetails = clsWorkCenterResourceDetail.GetData(obj.WORK_CENTER_CODE, trans)
            obj.ObjList_ToolDetails = clsWorkCenterToolDetail.GetData(obj.WORK_CENTER_CODE, trans)

        End If
        Return obj


    End Function

    Public Function SaveData(ByVal obj As clsWorkCenterMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.Description)
            clsCommon.AddColumnsForChange(coll, "SETUP_TIME", obj.SETUP_TIME)
            clsCommon.AddColumnsForChange(coll, "SETUP_TIME_TYPE", obj.SETUP_TIME_TYPE)
            clsCommon.AddColumnsForChange(coll, "RUN_TIME", obj.RUN_TIME)
            clsCommon.AddColumnsForChange(coll, "RUN_TIME_TYPE", obj.RUN_TIME_TYPE)
            clsCommon.AddColumnsForChange(coll, "CLEANUP_TIME", obj.CLEANUP_TIME)
            clsCommon.AddColumnsForChange(coll, "CLEANUP_TIME_TYPE", obj.CLEANUP_TIME_TYPE)
            clsCommon.AddColumnsForChange(coll, "WAIT_TIME", obj.WAIT_TIME)
            clsCommon.AddColumnsForChange(coll, "WAIT_TIME_TYPE", obj.WAIT_TIME_TYPE)
            clsCommon.AddColumnsForChange(coll, "WORK_AREA", obj.WORK_AREA)
            clsCommon.AddColumnsForChange(coll, "NO_OF_STATIONS", obj.NO_OF_STATIONS)
            clsCommon.AddColumnsForChange(coll, "STD_SETUP_LABOR", obj.STD_SETUP_LABOR)
            clsCommon.AddColumnsForChange(coll, "STD_RUN_LABOR", obj.STD_RUN_LABOR)
            clsCommon.AddColumnsForChange(coll, "STD_EFFICIENCY", obj.STD_EFFICIENCY)
            clsCommon.AddColumnsForChange(coll, "STD_UTILIZATION", obj.STD_UTILIZATION)
            clsCommon.AddColumnsForChange(coll, "COMMENTS", obj.COMMENTS)
            clsCommon.AddColumnsForChange(coll, "TotalResourceCost", obj.TotalResourceCost)
            clsCommon.AddColumnsForChange(coll, "TotalToolCost", obj.TotalToolCost)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
            If isNewEntry Then
                If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False)) Then
                    Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_MF_WORK_CENTER where WORK_CENTER_CODE='" & obj.WORK_CENTER_CODE & "'")
                    If ChkNewEntry = 0 Then
                        obj.WORK_CENTER_CODE = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"), clsDocType.WorkCenterMaster, "", "")
                        If clsCommon.myLen(obj.WORK_CENTER_CODE) <= 0 Then
                            Throw New Exception("Error in Code Generation")
                        End If
                    End If
                End If
                clsCommon.AddColumnsForChange(coll, "WORK_CENTER_CODE", obj.WORK_CENTER_CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
                Dim qry As String = "SELECT Count(*) FROM TSPL_MF_WORK_CENTER where WORK_CENTER_CODE= '" & obj.WORK_CENTER_CODE & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_WORK_CENTER", OMInsertOrUpdate.Insert, "")
                Else
                    Throw New Exception("This WORK_CENTER_CODE Is Already Exist")
                    Exit Function
                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_WORK_CENTER", OMInsertOrUpdate.Update, "WORK_CENTER_CODE='" + obj.WORK_CENTER_CODE + "'")
            End If
            isSaved = isSaved AndAlso ObjShift.SaveData(obj.WORK_CENTER_CODE, obj)
            isSaved = isSaved AndAlso ObjResource.SaveData(obj.WORK_CENTER_CODE, obj)
            isSaved = isSaved AndAlso ObjToolDetails.SaveData(obj.WORK_CENTER_CODE, obj)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

End Class

Public Class clsWorkCenterShiftDetail

#Region "Variables"
    Public WORK_CENTER_CODE As String
    Public SHIFT As Int16
    Public REGULAR_HOURS As Double
    Public OVERTIME_HOURS As Double
    Public STATIONS_IN_USE As Int16
#End Region

    Public Shared Function GetData(ByVal strCode As String) As List(Of clsWorkCenterShiftDetail)
        Return GetData(strCode, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsWorkCenterShiftDetail)
        Dim obj As clsWorkCenterShiftDetail = Nothing
        Dim qry As String = "select * from TSPL_MF_WORK_CENTER_SHIFT_DETAIL where 2=2 "
        qry += " and WORK_CENTER_CODE = '" + strCode + "'"
        Dim objList As New List(Of clsWorkCenterShiftDetail)
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each DR As DataRow In dt.Rows
                obj = New clsWorkCenterShiftDetail()
                obj.WORK_CENTER_CODE = clsCommon.myCstr(DR("WORK_CENTER_CODE"))
                obj.SHIFT = Convert.ToInt16(clsCommon.myCdbl(DR("SHIFT")))
                obj.REGULAR_HOURS = clsCommon.myCdbl(DR("REGULAR_HOURS"))
                obj.OVERTIME_HOURS = clsCommon.myCdbl(DR("OVERTIME_HOURS"))
                obj.STATIONS_IN_USE = Convert.ToInt16(clsCommon.myCdbl(DR("STATIONS_IN_USE")))
                objList.Add(obj)
            Next
        End If
        Return objList
    End Function

    Public Function SaveData(ByVal strCode As String, ByVal obj As clsWorkCenterMaster) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String
            qry = " delete from TSPL_MF_WORK_CENTER_SHIFT_DETAIL where WORK_CENTER_CODE ='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry)

            For Each objtr As clsWorkCenterShiftDetail In obj.ObjList_ShiftDetails
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "SHIFT", objtr.SHIFT)
                clsCommon.AddColumnsForChange(coll, "REGULAR_HOURS", objtr.REGULAR_HOURS)
                clsCommon.AddColumnsForChange(coll, "OVERTIME_HOURS", objtr.OVERTIME_HOURS)
                clsCommon.AddColumnsForChange(coll, "STATIONS_IN_USE", objtr.STATIONS_IN_USE)
                clsCommon.AddColumnsForChange(coll, "WORK_CENTER_CODE", obj.WORK_CENTER_CODE)
                isSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_WORK_CENTER_SHIFT_DETAIL", OMInsertOrUpdate.Insert, "")
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
End Class

Public Class clsWorkCenterResourceDetail

#Region "Variables"
    Public WORK_CENTER_CODE As String
    Public RESOURCE_CODE As String
    Public DESCRIPTION As String
    Public RESOURCE_TYPE As String
    Public UNIT_CODE_OTHER As String
    Public Basis As String
    Public QUANTITY As Double
    Public UNIT_COST As Double
    Public TOTAL_COST As Double
#End Region

    Public Shared Function GetData(ByVal strCode As String) As List(Of clsWorkCenterResourceDetail)
        Return GetData(strCode, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsWorkCenterResourceDetail)
        Dim obj As clsWorkCenterResourceDetail = Nothing
        Dim qry As String = "select TSPL_MF_WORK_CENTER_RESOURCE_DETAIL.*,TSPL_MF_RESOURCE_MASTER.DESCRIPTION,TSPL_MF_RESOURCE_MASTER.RESOURCE_TYPE,TSPL_MF_RESOURCE_MASTER.UNIT_CODE_OTHER from TSPL_MF_WORK_CENTER_RESOURCE_DETAIL left outer join TSPL_MF_RESOURCE_MASTER on TSPL_MF_RESOURCE_MASTER.RESOURCE_CODE = TSPL_MF_WORK_CENTER_RESOURCE_DETAIL.RESOURCE_CODE where 2=2 "
        qry += " and WORK_CENTER_CODE = '" + strCode + "'"
        Dim objList As New List(Of clsWorkCenterResourceDetail)
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each DR As DataRow In dt.Rows
                obj = New clsWorkCenterResourceDetail()
                obj.WORK_CENTER_CODE = clsCommon.myCstr(DR("WORK_CENTER_CODE"))
                obj.RESOURCE_CODE = clsCommon.myCstr(DR("RESOURCE_CODE"))
                obj.DESCRIPTION = clsCommon.myCstr(DR("DESCRIPTION"))
                obj.RESOURCE_TYPE = clsCommon.myCstr(DR("RESOURCE_TYPE"))
                obj.Basis = clsCommon.myCstr(DR("Basis"))
                obj.UNIT_CODE_OTHER = clsCommon.myCstr(DR("UNIT_CODE_OTHER"))
                obj.QUANTITY = clsCommon.myCdbl(DR("QUANTITY"))
                obj.UNIT_COST = clsCommon.myCdbl(DR("UNIT_COST"))
                obj.TOTAL_COST = clsCommon.myCdbl(DR("TOTAL_COST"))
                objList.Add(obj)
            Next
        End If
        Return objList
    End Function

    Public Function SaveData(ByVal strCode As String, ByVal obj As clsWorkCenterMaster) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String
            qry = " delete from TSPL_MF_WORK_CENTER_RESOURCE_DETAIL where WORK_CENTER_CODE ='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry)
            For Each objtr As clsWorkCenterResourceDetail In obj.ObjList_resourceDetails
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "RESOURCE_CODE", objtr.RESOURCE_CODE)
                clsCommon.AddColumnsForChange(coll, "QUANTITY", objtr.QUANTITY)
                clsCommon.AddColumnsForChange(coll, "Basis", objtr.Basis)
                clsCommon.AddColumnsForChange(coll, "UNIT_COST", objtr.UNIT_COST)
                clsCommon.AddColumnsForChange(coll, "TOTAL_COST", objtr.TOTAL_COST)
                clsCommon.AddColumnsForChange(coll, "WORK_CENTER_CODE", obj.WORK_CENTER_CODE)
                isSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_WORK_CENTER_RESOURCE_DETAIL", OMInsertOrUpdate.Insert, "")
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
End Class


Public Class clsWorkCenterToolDetail

#Region "Variables"
    Public WORK_CENTER_CODE As String
    Public TOOL_TYPE_CODE As String
    Public DESCRIPTION As String
    Public Basis As String
    Public UNIT_CODE As String
    Public QUANTITY As Double
    Public UNIT_COST As Double
    Public TOTAL_COST As Double
#End Region

    Public Shared Function GetData(ByVal strCode As String) As List(Of clsWorkCenterToolDetail)
        Return GetData(strCode, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsWorkCenterToolDetail)
        Dim obj As clsWorkCenterToolDetail = Nothing
        Dim qry As String = "select TSPL_MF_WORK_CENTER_TOOL_DETAIL.*,TSPL_MF_TOOL_TYPE.DESCRIPTION, TSPL_MF_TOOL_TYPE.UNIT_CODE from TSPL_MF_WORK_CENTER_TOOL_DETAIL left outer join TSPL_MF_TOOL_TYPE on TSPL_MF_TOOL_TYPE.TOOL_TYPE_CODE = TSPL_MF_WORK_CENTER_TOOL_DETAIL.TOOL_TYPE_CODE where 2=2 "
        qry += " and TSPL_MF_WORK_CENTER_TOOL_DETAIL.WORK_CENTER_CODE = '" + strCode + "'"
        Dim objList As New List(Of clsWorkCenterToolDetail)
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each DR As DataRow In dt.Rows
                obj = New clsWorkCenterToolDetail()
                obj.WORK_CENTER_CODE = clsCommon.myCstr(DR("WORK_CENTER_CODE"))
                obj.TOOL_TYPE_CODE = clsCommon.myCstr(DR("TOOL_TYPE_CODE"))
                obj.DESCRIPTION = clsCommon.myCstr(DR("DESCRIPTION"))
                obj.Basis = clsCommon.myCstr(DR("Basis"))
                obj.UNIT_CODE = clsCommon.myCstr(DR("UNIT_CODE"))
                obj.QUANTITY = clsCommon.myCdbl(DR("QUANTITY"))
                obj.UNIT_COST = clsCommon.myCdbl(DR("UNIT_COST"))
                obj.TOTAL_COST = clsCommon.myCdbl(DR("TOTAL_COST"))
                objList.Add(obj)
            Next
        End If
        Return objList
    End Function

    Public Function SaveData(ByVal strCode As String, ByVal obj As clsWorkCenterMaster) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String
            qry = " delete from TSPL_MF_WORK_CENTER_TOOL_DETAIL where WORK_CENTER_CODE ='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry)
            For Each objtr As clsWorkCenterToolDetail In obj.ObjList_ToolDetails
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "TOOL_TYPE_CODE", objtr.TOOL_TYPE_CODE)
                clsCommon.AddColumnsForChange(coll, "QUANTITY", objtr.QUANTITY)
                clsCommon.AddColumnsForChange(coll, "Basis", objtr.Basis)
                clsCommon.AddColumnsForChange(coll, "UNIT_COST", objtr.UNIT_COST)
                clsCommon.AddColumnsForChange(coll, "TOTAL_COST", objtr.TOTAL_COST)
                clsCommon.AddColumnsForChange(coll, "WORK_CENTER_CODE", obj.WORK_CENTER_CODE)
                isSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_WORK_CENTER_TOOL_DETAIL", OMInsertOrUpdate.Insert, "")
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
End Class
