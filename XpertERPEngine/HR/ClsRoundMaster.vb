Imports common
Imports System.Data
Imports System.Data.SqlClient

Public Class ClsRoundMaster
#Region "Variables"
    Public Round_Code As String
    Public Description As String
    Public Clearing_Score As Double

    Public ObjList As List(Of ClsRoundDetail) = Nothing
    Dim objDetail As New ClsRoundDetail()
#End Region

    Public Shared Function SaveData(ByVal arr As List(Of ClsRoundMaster)) As Boolean
        Dim trans As SqlTransaction = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin()
            If ClsRoundMaster.SaveData(arr, trans) Then
                trans.Commit()

            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal arr As List(Of ClsRoundMaster), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            For Each obj As ClsRoundMaster In arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Round_Code", obj.Round_Code)
                clsCommon.AddColumnsForChange(coll, "Round_Name", obj.Description)
                clsCommon.AddColumnsForChange(coll, "Clearing_Score", obj.Clearing_Score)
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)

                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                If clsDBFuncationality.getSingleValue("Select COUNT(*) from TSPL_HR_ROUND_MASTER WHERE Round_Code='" + obj.Round_Code + "'", trans) <= 0 Then
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                    Dim qry As String = "SELECT Count(*) FROM TSPL_HR_ROUND_MASTER where Round_Code= '" & obj.Round_Code & "'"
                    Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                    If check = 0 Then
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_ROUND_MASTER", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        Throw New Exception("This Code Is Already Exist")

                    End If
                Else
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_ROUND_MASTER", OMInsertOrUpdate.Update, "Round_Code='" + obj.Round_Code + "'", trans)
                End If
                ClsRoundDetail.SaveData(obj.Round_Code, obj.ObjList, trans)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsRoundMaster
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsRoundMaster
        Dim obj As ClsRoundMaster = Nothing

        Dim qry As String = "Select TSPL_HR_ROUND_MASTER.Round_Code,TSPL_HR_ROUND_MASTER.Round_Name,TSPL_HR_ROUND_MASTER.Clearing_Score from TSPL_HR_ROUND_MASTER LEFT OUTER JOIN  TSPL_HR_ROUND_DETAIL on TSPL_HR_ROUND_MASTER.Round_Code =TSPL_HR_ROUND_DETAIL.Round_Code where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_HR_ROUND_MASTER.Round_Code = (select MIN(Round_Code) from TSPL_HR_ROUND_MASTER)"
            Case NavigatorType.Last
                qry += " and TSPL_HR_ROUND_MASTER.Round_Code = (select Max(Round_Code) from TSPL_HR_ROUND_MASTER)"
            Case NavigatorType.Next
                qry += " and TSPL_HR_ROUND_MASTER.Round_Code = (select Min(Round_Code) from TSPL_HR_ROUND_MASTER where  Round_Code >'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_HR_ROUND_MASTER.Round_Code = (select Max(Round_Code) from TSPL_HR_ROUND_MASTER where Round_Code <'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_HR_ROUND_MASTER.Round_Code = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsRoundMaster()
            obj.Round_Code = clsCommon.myCstr(dt.Rows(0)("Round_Code"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Round_Name"))
            obj.Clearing_Score = clsCommon.myCdbl(dt.Rows(0)("Clearing_Score"))
            obj.ObjList = ClsRoundDetail.GetData(obj.Round_Code, trans)
        End If
        Return obj
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
            qry = "Delete From TSPL_HR_ROUND_DETAIL Where Round_Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete From TSPL_HR_ROUND_MASTER Where Round_Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If isSaved Then
                trans.Commit()
            Else
                trans.Rollback()
                isSaved = False
            End If
        Catch ex As Exception
            trans.Rollback()
            isSaved = False
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
End Class
Public Class ClsRoundDetail

#Region "Variables"
    Public Round_Code As String
    Public Parameter_Code As String
#End Region
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = " delete from TSPL_HR_ROUND_DETAIL where Round_Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function SaveData(ByVal strCode As String, ByVal ObjList As List(Of ClsRoundDetail), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "DELETE FROM TSPL_HR_ROUND_DETAIL where Round_Code = '" & strCode & "'  "
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            For Each obj As ClsRoundDetail In ObjList
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Round_Code", strCode)
                clsCommon.AddColumnsForChange(coll, "Parameter_Code", obj.Parameter_Code)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_ROUND_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of ClsRoundDetail)
        Dim obj As ClsRoundDetail = Nothing
        Dim ObjList As New List(Of ClsRoundDetail)
        Dim qry As String = " select *  from TSPL_HR_ROUND_DETAIL WHERE Round_Code = '" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                obj = New ClsRoundDetail()
                obj.Round_Code = clsCommon.myCstr(dr("Round_Code"))
                obj.Parameter_Code = clsCommon.myCstr(dr("Parameter_Code"))
                ObjList.Add(obj)
            Next
        End If
        Return ObjList
    End Function

End Class
