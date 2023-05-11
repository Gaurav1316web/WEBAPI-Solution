Imports common
Imports System.Data
Imports System.Data.SqlClient

Public Class clsDispatchChecklist

#Region "Variables"
    Public Code As String
    Public Description As String
#End Region
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_DISPATCH_CHECKLIST_MASTER.CODE as [Code],TSPL_DISPATCH_CHECKLIST_MASTER.DESCRIPTION as [DESCRIPTION],TSPL_DISPATCH_CHECKLIST_MASTER.Created_By as [Created By],TSPL_DISPATCH_CHECKLIST_MASTER.Created_Date as [Created Date],TSPL_DISPATCH_CHECKLIST_MASTER.Modified_By as [Modify By],TSPL_DISPATCH_CHECKLIST_MASTER.Modified_Date as [Modify Date]  from TSPL_DISPATCH_CHECKLIST_MASTER"
        str = clsCommon.ShowSelectForm("STMTRFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str

    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'

    Public Shared Function GetName(ByVal strcode As String, ByVal trans As SqlTransaction) As String
        Try
            Dim qry As String = "select TSPL_DISPATCH_CHECKLIST_MASTER.DESCRIPTION from TSPL_DISPATCH_CHECKLIST_MASTER where TSPL_DISPATCH_CHECKLIST_MASTER.Code='" + strcode + "'"
            Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsDispatchChecklist
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean

        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = "delete from TSPL_DISPATCH_CHECKLIST_MASTER where CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)

            Return True
        Catch ex As Exception

            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsDispatchChecklist
        Dim obj As clsDispatchChecklist = Nothing
        Dim qry As String = "select TSPL_DISPATCH_CHECKLIST_MASTER.CODE, TSPL_DISPATCH_CHECKLIST_MASTER.DESCRIPTION FROM TSPL_DISPATCH_CHECKLIST_MASTER  where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_DISPATCH_CHECKLIST_MASTER.CODE = (select MIN(TSPL_DISPATCH_CHECKLIST_MASTER.CODE) from TSPL_DISPATCH_CHECKLIST_MASTER)"
            Case NavigatorType.Last
                qry += " and TSPL_DISPATCH_CHECKLIST_MASTER.CODE = (select Max(TSPL_DISPATCH_CHECKLIST_MASTER.CODE) from TSPL_DISPATCH_CHECKLIST_MASTER)"
            Case NavigatorType.Next
                qry += " and TSPL_DISPATCH_CHECKLIST_MASTER.CODE = (select Min(TSPL_DISPATCH_CHECKLIST_MASTER.CODE) from TSPL_DISPATCH_CHECKLIST_MASTER where  TSPL_DISPATCH_CHECKLIST_MASTER.CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_DISPATCH_CHECKLIST_MASTER.CODE = (select Max(TSPL_DISPATCH_CHECKLIST_MASTER.CODE) from TSPL_DISPATCH_CHECKLIST_MASTER where TSPL_DISPATCH_CHECKLIST_MASTER.CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_DISPATCH_CHECKLIST_MASTER.CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsDispatchChecklist()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("CODE"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
        End If
        Return obj
    End Function

    Public Shared Function SaveData(ByVal obj As clsDispatchChecklist, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If SaveData(obj, isNewEntry, trans) Then
                trans.Commit()
                isSaved = True
            End If
        Catch ex As Exception
            trans.Rollback()
            isSaved = False
            clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function SaveData(ByVal obj As clsDispatchChecklist, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, trans)) = "1", True, False)) Then
                    Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_DISPATCH_CHECKLIST_MASTER where CODE='" & obj.Code & "'", trans)
                    If ChkNewEntry = 0 Then
                        obj.Code = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"), clsDocType.DispatchCheckList, "", "")
                        If clsCommon.myLen(obj.Code) <= 0 Then
                            Throw New Exception("Error in Code Generation")
                        End If
                    End If
                End If
                clsCommon.AddColumnsForChange(coll, "CODE", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                Dim qry As String = "SELECT Count(*) FROM TSPL_DISPATCH_CHECKLIST_MASTER where TSPL_DISPATCH_CHECKLIST_MASTER.CODE= '" & obj.Code & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DISPATCH_CHECKLIST_MASTER", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This Code Is Already Exist")
                End If

            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DISPATCH_CHECKLIST_MASTER", OMInsertOrUpdate.Update, "TSPL_DISPATCH_CHECKLIST_MASTER.CODE='" + obj.Code + "'", trans)
            End If
            '--------------------------------------------------------------------------------------------------------

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function CheckNewEntry(ByVal Code As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = "select TSPL_DISPATCH_CHECKLIST_MASTER.CODE from TSPL_DISPATCH_CHECKLIST_MASTER where TSPL_DISPATCH_CHECKLIST_MASTER.CODE ='" + Code + "'   "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If

    End Function
End Class
