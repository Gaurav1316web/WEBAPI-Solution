Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsOperationMaster

#Region "Variables"

    Public OPERATION_CODE As String
    Public OPERATION_TYPE As String
    Public Descraption As String
    Public COMMENTS As String
    Public Created_By As String
    Public Created_Date As String
    Public ObjList As List(Of clsOperationMasterDetail)
#End Region
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_MF_OPERATION.OPERATION_CODE as [Code] ,TSPL_MF_OPERATION.DESCRIPTION as [Description] ,TSPL_MF_OPERATION.OPERATION_TYPE as [Operation Type] ,TSPL_MF_OPERATION.COMMENTS as [Comments] ,TSPL_MF_OPERATION.Created_By as [Created By] ,TSPL_MF_OPERATION.Created_Date as [Created Date] ,TSPL_MF_OPERATION.Modified_By as [Modified By] ,TSPL_MF_OPERATION.Modified_Date as [Modified Date]  From TSPL_MF_OPERATION   "
        str = clsCommon.ShowSelectForm("OPERMSTFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'

    Public Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsOperationMaster
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
            qry = "delete from TSPL_MF_OPERATION_WORK_CENTER where OPERATION_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_MF_OPERATION where OPERATION_CODE ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsOperationMaster
        Dim obj As New clsOperationMaster()
        Dim objtr As New clsOperationMasterDetail()
        ObjList = New List(Of clsOperationMasterDetail)

        Dim qry As String = " SELECT * FROM TSPL_MF_OPERATION  where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and OPERATION_CODE = (select MIN(OPERATION_CODE) from TSPL_MF_OPERATION)"
            Case NavigatorType.Last
                qry += " and OPERATION_CODE = (select Max(OPERATION_CODE) from TSPL_MF_OPERATION)"
            Case NavigatorType.Next
                qry += " and OPERATION_CODE = (select Min(OPERATION_CODE) from TSPL_MF_OPERATION where OPERATION_CODE > '" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and OPERATION_CODE = (select Max(OPERATION_CODE) from TSPL_MF_OPERATION where OPERATION_CODE < '" + strCode + "')"
            Case NavigatorType.Current
                qry += " and OPERATION_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj.OPERATION_CODE = clsCommon.myCstr(dt.Rows(0)("OPERATION_CODE"))
            obj.OPERATION_TYPE = clsCommon.myCstr(dt.Rows(0)("OPERATION_TYPE"))
            obj.Descraption = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
            obj.COMMENTS = clsCommon.myCstr(dt.Rows(0)("COMMENTS"))
            obj.Created_By = clsCommon.myCstr(dt.Rows(0)("Created_By"))
            obj.Created_Date = clsCommon.myCstr(clsCommon.GetPrintDate(dt.Rows(0)("Created_Date"), "dd/MMM/yyyy"))
            obj.ObjList = objtr.GetData(obj.OPERATION_CODE, trans)

        End If
        Return obj
    End Function
    Public Function SaveData(ByVal obj As clsOperationMaster, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "OPERATION_TYPE", obj.OPERATION_TYPE)
            clsCommon.AddColumnsForChange(coll, "COMMENTS", obj.COMMENTS)
            clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.Descraption)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, trans)) = "1", True, False)) Then
                    Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_MF_OPERATION where OPERATION_CODE='" & obj.OPERATION_CODE & "'", trans)
                    If ChkNewEntry = 0 Then
                        obj.OPERATION_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"), clsDocType.OperationMaster, "", "")
                        If clsCommon.myLen(obj.OPERATION_CODE) <= 0 Then
                            Throw New Exception("Error in Code Generation")
                        End If
                    End If
                End If
                clsCommon.AddColumnsForChange(coll, "OPERATION_CODE", obj.OPERATION_CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_OPERATION", OMInsertOrUpdate.Insert, "", trans)
            Else

                isSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_OPERATION", OMInsertOrUpdate.Update, "TSPL_MF_OPERATION.OPERATION_CODE='" + obj.OPERATION_CODE + "'", trans)
            End If
            isSaved = isSaved AndAlso clsOperationMasterDetail.SaveData(obj.OPERATION_CODE, obj.ObjList, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

End Class

Public Class clsOperationMasterDetail
#Region "Variables"

    Public OPERATION_CODE As String
    Public WORK_CENTER_CODE As String
    Public Descraption As String
    Public ObjList As List(Of clsOperationMasterDetail)
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsOperationMasterDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String
            qry = " delete from TSPL_MF_OPERATION_WORK_CENTER where OPERATION_CODE ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsOperationMasterDetail In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "OPERATION_CODE", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "WORK_CENTER_CODE", obj.WORK_CENTER_CODE)
                    clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.Descraption)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_OPERATION_WORK_CENTER", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function GetData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As List(Of clsOperationMasterDetail)
        Dim qry As String = " "
        qry += " select * FROM TSPL_MF_OPERATION_WORK_CENTER "
        qry += " where OPERATION_CODE = '" + strDocNo + "'"
        Dim dt As DataTable = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        Dim objtr As clsOperationMasterDetail
        ObjList = New List(Of clsOperationMasterDetail)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsOperationMasterDetail()
                objtr.OPERATION_CODE = clsCommon.myCstr(dr("OPERATION_CODE"))
                objtr.WORK_CENTER_CODE = clsCommon.myCstr(dr("WORK_CENTER_CODE"))
                objtr.Descraption = clsCommon.myCstr(dr("DESCRIPTION"))
                ObjList.Add(objtr)
            Next
        End If
        Return ObjList
    End Function

End Class
