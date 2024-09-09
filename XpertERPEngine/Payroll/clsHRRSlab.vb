Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsHRRSlab
#Region "Variables"
    Public HRR_CODE As String
    Public HRR_NAME As String
    Public APPLY_ON_BASIC As Integer
    Public APPLICABLE_FROM As Date
    Public STATE_CODE As String
    Public REMARKS As String
    Public ObjList As List(Of clsHRRSlabDetails) = Nothing
    Dim objSlabDetails As New clsHRRSlabDetails()
#End Region
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " Select HRR_CODE as Code,HRR_NAME as Name,APPLICABLE_FROM as [Applicable From],STATE_CODE as [State Code],REMARKS as Remarks from TSPL_HRR_RULE_MASTER "
        str = clsCommon.ShowSelectForm("OTSLABFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsHRRSlab
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = False
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            Dim qry As String
            qry = "delete from TSPL_HRR_DETAIL where HRR_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_HRR_RULE_MASTER where HRR_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsHRRSlab
        Dim obj As clsHRRSlab = Nothing
        Dim qry As String = " select TSPL_HRR_RULE_MASTER.* from TSPL_HRR_RULE_MASTER where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_HRR_RULE_MASTER.HRR_CODE = (select MIN(HRR_CODE) from TSPL_HRR_RULE_MASTER)"
            Case NavigatorType.Last
                qry += " and TSPL_HRR_RULE_MASTER.HRR_CODE = (select Max(HRR_CODE) from TSPL_HRR_RULE_MASTER)"
            Case NavigatorType.Next
                qry += " and TSPL_HRR_RULE_MASTER.HRR_CODE = (select Min(HRR_CODE) from TSPL_HRR_RULE_MASTER where  HRR_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_HRR_RULE_MASTER.HRR_CODE = (select Max(HRR_CODE) from TSPL_HRR_RULE_MASTER where HRR_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_HRR_RULE_MASTER.HRR_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsHRRSlab()
            obj.HRR_CODE = clsCommon.myCstr(dt.Rows(0)("HRR_CODE"))
            obj.HRR_NAME = clsCommon.myCstr(dt.Rows(0)("HRR_NAME"))
            obj.STATE_CODE = clsCommon.myCstr(dt.Rows(0)("STATE_CODE"))
            obj.APPLICABLE_FROM = clsCommon.myCDate(dt.Rows(0)("APPLICABLE_FROM"))
            obj.REMARKS = clsCommon.myCstr(dt.Rows(0)("REMARKS"))
            obj.ObjList = clsHRRSlabDetails.GetData(obj.HRR_CODE, trans)
        End If
        Return obj
    End Function
    Public Shared Function SaveData(ByVal obj As clsHRRSlab, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin()
            If clsHRRSlab.SaveData(obj, isNewEntry, trans) Then
                trans.Commit()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function SaveData(ByVal obj As clsHRRSlab, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "HRR_NAME", obj.HRR_NAME)
            clsCommon.AddColumnsForChange(coll, "REMARKS", obj.REMARKS)
            clsCommon.AddColumnsForChange(coll, "STATE_CODE", obj.STATE_CODE)
            clsCommon.AddColumnsForChange(coll, "APPLICABLE_FROM", clsCommon.GetPrintDate(obj.APPLICABLE_FROM, "dd-MMM-yyyy"))
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, trans)) = "1", True, False)) Then
                    obj.HRR_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"), clsDocType.HRRSLab, "", "")
                Else
                    If clsCommon.myLen(obj.HRR_CODE) <= 0 Then
                        Throw New Exception("HRR Code not entered from screen.")
                    End If
                End If
            End If
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "HRR_CODE", obj.HRR_CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                Dim qry As String = "SELECT Count(*) FROM TSPL_HRR_RULE_MASTER where HRR_CODE= '" & obj.HRR_CODE & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HRR_RULE_MASTER", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This Code Is Already Exist")
                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HRR_RULE_MASTER", OMInsertOrUpdate.Update, "HRR_CODE='" + obj.HRR_CODE + "'", trans)
            End If
            clsHRRSlabDetails.SaveData(obj.HRR_CODE, obj.ObjList, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
End Class
Public Class clsHRRSlabDetails
#Region "Variables"
    Public HRR_CODE As String
    Public _FROM As Decimal
    Public _TO As Decimal
    Public Percentage As Decimal
    Public PAYHEADS As String
#End Region
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            Dim qry As String
            qry = " delete from TSPL_HRR_DETAIL where HRR_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsHRRSlabDetails)
        Dim obj As clsHRRSlabDetails = Nothing
        Dim ObjList As New List(Of clsHRRSlabDetails)
        Dim qry As String = " select *  from TSPL_HRR_DETAIL WHERE HRR_CODE = '" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                obj = New clsHRRSlabDetails()
                obj.HRR_CODE = clsCommon.myCstr(dr("HRR_CODE"))
                obj._FROM = clsCommon.myCdbl(dr("SLAB_FROM"))
                obj._TO = clsCommon.myCdbl(dr("SLAB_TO"))
                obj.PAYHEADS = clsCommon.myCstr(dr("PAYHEADS"))
                obj.Percentage = clsCommon.myCstr(dr("Percentage"))
                ObjList.Add(obj)
            Next
        End If
        Return ObjList
    End Function
    Public Shared Function SaveData(ByVal strCode As String, ByVal ObjList As List(Of clsHRRSlabDetails), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "DELETE FROM TSPL_HRR_DETAIL where HRR_CODE = '" & strCode & "'  "
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            For Each obj As clsHRRSlabDetails In ObjList
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "HRR_CODE", strCode)
                clsCommon.AddColumnsForChange(coll, "SLAB_FROM", obj._FROM)
                clsCommon.AddColumnsForChange(coll, "SLAB_TO", obj._TO)
                clsCommon.AddColumnsForChange(coll, "PAYHEADS", obj.PAYHEADS)
                clsCommon.AddColumnsForChange(coll, "Percentage", obj.Percentage)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HRR_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetOTRateType() As DataTable
        Dim DT As DataTable = New DataTable
        DT.Columns.Add("Code", GetType(String))
        DT.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT.NewRow()
        DR("Name") = "% Basic"
        DR("Code") = "BS"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Name") = "% Gross"
        DR("Code") = "GS"
        DT.Rows.Add(DR)

        DT.AcceptChanges()

        Return DT
    End Function

End Class
