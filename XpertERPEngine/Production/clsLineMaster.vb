Imports common
Imports System.Data.SqlClient
Public Class clsLineMaster
#Region "Variable"
    Public LINE_NO As String = Nothing
    Public MACHINE_NAME As String = Nothing
    Public MACHINE_RATED As String = Nothing
    Public CAPACITY As String = Nothing
    Public TIME_FRAME As String = Nothing
    Public Comp_Code As String = Nothing
    Public isNewEntry As Boolean = False
    'LINE_NO , MACHINE_NAME,  MACHINE_RATED , CAPACITY , TIME_FRAME
    'TSPL_LINE_MASTER
#End Region

    Public Shared Function deleteData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "delete from TSPL_LINE_MASTER where LINE_NO='" & strDocNo & "'"
            Dim isDeleted As Boolean = True
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function getLineNoFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try
            Dim qry As String = "  select TSPL_LINE_MASTER.LINE_NO as [LINE NO],TSPL_LINE_MASTER.MACHINE_NAME as [MACHINE NAME],TSPL_LINE_MASTER.MACHINE_RATED as [MACHINE RATED],TSPL_LINE_MASTER.CAPACITY,TSPL_LINE_MASTER.TIME_FRAME as [TIME FRAME] From TSPL_LINE_MASTER "
            str = customFinder.getFinder("LINE@NO", qry, "", curcode, "", "LINE NO")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str
    End Function

    Public Shared Function getLineNoData(ByVal strCode As String, ByVal navtype As NavigatorType, Optional trans As SqlTransaction = Nothing) As clsLineMaster
        Dim obj As New clsLineMaster
        Try
            Dim whrCls As String = String.Empty
            Dim qst As String = "select TSPL_LINE_MASTER.LINE_NO ,TSPL_LINE_MASTER.MACHINE_NAME ,TSPL_LINE_MASTER.MACHINE_RATED ,TSPL_LINE_MASTER.CAPACITY,TSPL_LINE_MASTER.TIME_FRAME  From TSPL_LINE_MASTER where 2=2 "

            qst = qst & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and TSPL_LINE_MASTER.LINE_NO in ('" + strCode + "') "
                Case NavigatorType.Next
                    qst += " and TSPL_LINE_MASTER.LINE_NO in (select min(LINE_NO ) from TSPL_LINE_MASTER where LINE_NO  >'" + strCode + "'   " & whrCls & ")"
                Case NavigatorType.First
                    qst += " and TSPL_LINE_MASTER.LINE_NO in (select MIN(LINE_NO ) from TSPL_LINE_MASTER where 1=1  " & whrCls & ")"
                Case NavigatorType.Last
                    qst += " and TSPL_LINE_MASTER.LINE_NO in (select Max(LINE_NO ) from TSPL_LINE_MASTER where 1=1  " & whrCls & ")"
                Case NavigatorType.Previous
                    qst += " and TSPL_LINE_MASTER.LINE_NO in (select Max(LINE_NO ) from TSPL_LINE_MASTER where LINE_NO  <'" + strCode + "'   " & whrCls & ")"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.LINE_NO = clsCommon.myCstr(dt.Rows(0)("LINE_NO"))
                obj.MACHINE_NAME = clsCommon.myCstr(dt.Rows(0)("MACHINE_NAME"))
                obj.MACHINE_RATED = clsCommon.myCstr(dt.Rows(0)("MACHINE_RATED"))
                obj.CAPACITY = clsCommon.myCstr(dt.Rows(0)("CAPACITY")) ' 
                obj.TIME_FRAME = clsCommon.myCstr(dt.Rows(0)("TIME_FRAME"))
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function

    Public Shared Function SaveData(ByVal obj As clsLineMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
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

    Public Shared Function saveData(ByVal obj As clsLineMaster, ByVal isNewEntry As Boolean, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim issaved As Boolean = True

        Try
            Dim coll As New Hashtable()
            ' trans = clsDBFuncationality.GetTransactin()
            obj.isNewEntry = isNewEntry
            clsCommon.AddColumnsForChange(coll, "LINE_NO", clsCommon.myCstr(obj.LINE_NO))
            'clsCommon.AddColumnsForChange(coll, "GATE_OUT_DATE", clsCommon.GetPrintDate(obj.GATE_OUT_DATE, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "MACHINE_NAME", clsCommon.myCstr(obj.MACHINE_NAME))
            clsCommon.AddColumnsForChange(coll, "MACHINE_RATED", clsCommon.myCstr(obj.MACHINE_RATED))
            clsCommon.AddColumnsForChange(coll, "CAPACITY", clsCommon.myCstr(obj.CAPACITY))
            clsCommon.AddColumnsForChange(coll, "TIME_FRAME", clsCommon.myCstr(obj.TIME_FRAME))
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            If obj.isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LINE_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LINE_MASTER", OMInsertOrUpdate.Update, "TSPL_LINE_MASTER.LINE_NO='" + obj.LINE_NO + "'", trans)
            End If
            'trans.Commit()
        Catch ex As Exception
            ' trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return issaved
    End Function
End Class
