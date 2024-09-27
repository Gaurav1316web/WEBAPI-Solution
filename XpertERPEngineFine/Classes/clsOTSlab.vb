Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsOTSlab

#Region "Variables"
    Public OT_CODE As String
    Public SLAB_DESCRIPTION As String
    Public IS_ASPER_ACTUAL_CALC As Integer

    Public ObjList As List(Of clsOTSlabDetails) = Nothing
    Dim objSlabDetails As New clsOTSlabDetails()

#End Region

    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_OT_MASTER.OT_CODE as [Code] ,TSPL_OT_MASTER.OT_NAME as [OT Name] ,TSPL_OT_MASTER.HOUR_MULTIPLIER as [Hour Multiplier] ,TSPL_OT_MASTER.OT_RATE as [OT Rate] ,TSPL_OT_MASTER.IS_ASPER_ACTUAL_CALC as [Is Asper Actual Calc] ,COALESCE(TSPL_OT_SLAB.SLAB_DESCRIPTION,TSPL_OT_MASTER.DESCRIPTION) AS [Slab Description] ,TSPL_OT_MASTER.Created_By as [Created By] ,TSPL_OT_MASTER.Created_Date as [Created Date] ,TSPL_OT_MASTER.Modified_By as [Modified By] ,TSPL_OT_MASTER.Modified_Date as [Modified Date]  From TSPL_OT_MASTER LEFT JOIN TSPL_OT_SLAB ON TSPL_OT_MASTER.OT_CODE=TSPL_OT_SLAB.OT_CODE   "
        str = clsCommon.ShowSelectForm("OTSLABFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsOTSlab
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Try
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            'isSaved = clsOTSlabDetails.DeleteData(strCode)
            Dim qry As String
            qry = "delete from TSPL_OT_SLAB_DETAIL where OT_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_OT_SLAB where OT_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If isSaved Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
        Catch ex As Exception

            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsOTSlab
        Dim obj As clsOTSlab = Nothing
        Dim qry As String = " select TSPL_OT_SLAB.*,TSPL_OT_MASTER.IS_ASPER_ACTUAL_CALC from TSPL_OT_SLAB INNER JOIN TSPL_OT_MASTER ON TSPL_OT_SLAB.OT_CODE=TSPL_OT_MASTER.OT_CODE  where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_OT_SLAB.OT_CODE = (select MIN(OT_CODE) from TSPL_OT_SLAB)"
            Case NavigatorType.Last
                qry += " and TSPL_OT_SLAB.OT_CODE = (select Max(OT_CODE) from TSPL_OT_SLAB)"
            Case NavigatorType.Next
                qry += " and TSPL_OT_SLAB.OT_CODE = (select Min(OT_CODE) from TSPL_OT_SLAB where  OT_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_OT_SLAB.OT_CODE = (select Max(OT_CODE) from TSPL_OT_SLAB where OT_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_OT_SLAB.OT_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsOTSlab()
            obj.OT_CODE = clsCommon.myCstr(dt.Rows(0)("OT_CODE"))
            obj.SLAB_DESCRIPTION = clsCommon.myCstr(dt.Rows(0)("SLAB_DESCRIPTION"))
            obj.IS_ASPER_ACTUAL_CALC = clsCommon.myCdbl(dt.Rows(0)("IS_ASPER_ACTUAL_CALC"))
            obj.ObjList = clsOTSlabDetails.GetData(obj.OT_CODE, trans)
        End If
        Return obj
    End Function

    Public Shared Function SaveData(ByVal arr As List(Of clsOTSlab)) As Boolean
        Dim trans As SqlTransaction = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin()
            If clsOTSlab.SaveData(arr, trans) Then
                trans.Commit()

            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal arr As List(Of clsOTSlab), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            For Each obj As clsOTSlab In arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "SLAB_DESCRIPTION", obj.SLAB_DESCRIPTION)
                'clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)

                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                If clsDBFuncationality.getSingleValue("Select COUNT(*) from TSPL_OT_SLAB WHERE OT_CODE='" + obj.OT_CODE + "'", trans) <= 0 Then

                    If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, trans)) = "1", True, False)) Then
                        obj.OT_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"), clsDocType.OTSLab, "", "")
                    End If

                    clsCommon.AddColumnsForChange(coll, "OT_CODE", obj.OT_CODE)
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                    Dim qry As String = "SELECT Count(*) FROM TSPL_OT_SLAB where OT_CODE= '" & obj.OT_CODE & "'"
                    Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                    If check = 0 Then
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_OT_SLAB", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        Throw New Exception("This Code Is Already Exist")

                    End If
                Else
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_OT_SLAB", OMInsertOrUpdate.Update, "OT_CODE='" + obj.OT_CODE + "'", trans)
                End If
                clsOTSlabDetails.SaveData(obj.OT_CODE, obj.ObjList, trans)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

End Class
Public Class clsOTSlabDetails

#Region "Variables"
    Public OT_CODE As String
    Public CRITERIA_TYPE As String
    Public _FROM As Decimal
    Public _TO As Decimal
    Public OT_RATE As Decimal
    Public RATE_TYPE As String

#End Region

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = " delete from TSPL_OT_SLAB_DETAIL where OT_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsOTSlabDetails)
        Dim obj As clsOTSlabDetails = Nothing
        Dim ObjList As New List(Of clsOTSlabDetails)
        Dim qry As String = " select *  from TSPL_OT_SLAB_DETAIL WHERE OT_CODE = '" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                obj = New clsOTSlabDetails()
                obj.OT_CODE = clsCommon.myCstr(dr("OT_CODE"))
                obj.CRITERIA_TYPE = clsCommon.myCstr(dr("CRITERIA_TYPE"))
                obj._FROM = clsCommon.myCdbl(dr("_FROM"))
                obj._TO = clsCommon.myCdbl(dr("_TO"))
                obj.OT_RATE = clsCommon.myCdbl(dr("OT_RATE"))
                obj.RATE_TYPE = clsCommon.myCstr(dr("RATE_TYPE"))
                ObjList.Add(obj)
            Next
        End If
        Return ObjList

    End Function

    Public Shared Function SaveData(ByVal strCode As String, ByVal ObjList As List(Of clsOTSlabDetails), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "DELETE FROM TSPL_OT_SLAB_DETAIL where OT_CODE = '" & strCode & "'  "
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            For Each obj As clsOTSlabDetails In ObjList
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "OT_CODE", strCode)
                clsCommon.AddColumnsForChange(coll, "CRITERIA_TYPE", obj.CRITERIA_TYPE)
                clsCommon.AddColumnsForChange(coll, "_FROM", obj._FROM)
                clsCommon.AddColumnsForChange(coll, "_TO", obj._TO)
                clsCommon.AddColumnsForChange(coll, "OT_RATE", obj.OT_RATE)
                clsCommon.AddColumnsForChange(coll, "RATE_TYPE", obj.RATE_TYPE)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_OT_SLAB_DETAIL", OMInsertOrUpdate.Insert, "", trans)

                'If check = 0 Then
                '    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_OT_SLAB_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                'Else
                '    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_OT_SLAB_DETAIL", OMInsertOrUpdate.Update, " OT_CODE = '" & strCode & "' ", trans)
                'End If
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetOTCriteriaType() As DataTable
        Dim DT As DataTable = New DataTable
        DT.Columns.Add("Code", GetType(String))
        DT.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT.NewRow()
        DR("Name") = "OT Hours"
        DR("Code") = "OTH"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Name") = "Basic"
        DR("Code") = "Basic"
        DT.Rows.Add(DR)

        DT.AcceptChanges()

        Return DT
    End Function
    Public Shared Function GetOTRateType() As DataTable
        Dim DT As DataTable = New DataTable
        DT.Columns.Add("Code", GetType(String))
        DT.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT.NewRow()
        DR("Name") = "% Basic"
        DR("Code") = "PBS"
        DT.Rows.Add(DR)

        DR = DT.NewRow() ''BHA/01/02/19-000798 by balwinder on 01/02/2019
        DR("Name") = "% Gross"
        DR("Code") = "PGS"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Name") = "Per OT Hours"
        DR("Code") = "POTH"
        DT.Rows.Add(DR)

        DT.AcceptChanges()

        Return DT
    End Function

End Class
