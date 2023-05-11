Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsSubDepartmentMaster

#Region "Variables"
    Public Code As String
    Public Description As String
    Public DEPARTMENT_CODE As String
    Public Main_Department_Desc As String
#End Region

    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_SUB_DEPARTMENT_MASTER.SUB_DEPARTMENT_CODE as [Code] , " & _
        " TSPL_SUB_DEPARTMENT_MASTER.DESCRIPTION as [Description],TSPL_SUB_DEPARTMENT_MASTER.DEPARTMENT_CODE as [Main Department Code], " & _
        " TSPL_SUB_DEPARTMENT_MASTER.Created_By as [Created By] ,TSPL_SUB_DEPARTMENT_MASTER.Created_Date as [Created Date] , " & _
        " TSPL_SUB_DEPARTMENT_MASTER.Modified_By as [Modified By] ,TSPL_SUB_DEPARTMENT_MASTER.Modified_Date as [Modified Date]  From TSPL_SUB_DEPARTMENT_MASTER   "
        str = clsCommon.ShowSelectForm("SDEPTMSTFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsSubDepartmentMaster
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean

        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Department Code not found to Delete")
            End If

            Dim qry As String
            qry = "delete from TSPL_SUB_DEPARTMENT_MASTER where SUB_DEPARTMENT_CODE='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)


        Catch ex As Exception

            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsSubDepartmentMaster
        Dim obj As clsSubDepartmentMaster = Nothing
        Dim qry As String = "select SUB_DEPARTMENT_CODE, TSPL_SUB_DEPARTMENT_MASTER.DESCRIPTION,TSPL_SUB_DEPARTMENT_MASTER.DEPARTMENT_CODE,TSPL_DEPARTMENT_MASTER.DEPARTMENT_NAME AS MAIN_DEPARTMENT_DESC from TSPL_SUB_DEPARTMENT_MASTER " & _
        " LEFT JOIN TSPL_DEPARTMENT_MASTER ON TSPL_SUB_DEPARTMENT_MASTER.DEPARTMENT_CODE=TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and SUB_DEPARTMENT_CODE = (select MIN(SUB_DEPARTMENT_CODE) from TSPL_SUB_DEPARTMENT_MASTER)"
            Case NavigatorType.Last
                qry += " and SUB_DEPARTMENT_CODE = (select Max(SUB_DEPARTMENT_CODE) from TSPL_SUB_DEPARTMENT_MASTER)"
            Case NavigatorType.Next
                qry += " and SUB_DEPARTMENT_CODE = (select Min(SUB_DEPARTMENT_CODE) from TSPL_SUB_DEPARTMENT_MASTER where  SUB_DEPARTMENT_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and SUB_DEPARTMENT_CODE = (select Max(SUB_DEPARTMENT_CODE) from TSPL_SUB_DEPARTMENT_MASTER where SUB_DEPARTMENT_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and SUB_DEPARTMENT_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsSubDepartmentMaster()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("SUB_DEPARTMENT_CODE"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
            obj.Main_Department_Desc = clsCommon.myCstr(dt.Rows(0)("MAIN_DEPARTMENT_DESC"))
            obj.DEPARTMENT_CODE = clsCommon.myCstr(dt.Rows(0)("DEPARTMENT_CODE"))
        End If
        Return obj


    End Function

    Public Function SaveData(ByVal obj As clsSubDepartmentMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            'clsCommon.AddColumnsForChange(coll, "DEPARTMENT_NAME", obj.Name)
            clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.Description)
            clsCommon.AddColumnsForChange(coll, "DEPARTMENT_CODE", obj.DEPARTMENT_CODE)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
            If isNewEntry Then
                If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False)) Then
                    Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_SUB_DEPARTMENT_MASTER where SUB_DEPARTMENT_CODE='" & obj.Code & "'")
                    If ChkNewEntry = 0 Then
                        obj.Code = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"), clsDocType.SubDepartmentMaster, "", "")
                        If clsCommon.myLen(obj.Code) <= 0 Then
                            Throw New Exception("Error in Code Generation")
                        End If
                    End If
                End If
                clsCommon.AddColumnsForChange(coll, "SUB_DEPARTMENT_CODE", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
                Dim qry As String = "SELECT Count(*) FROM TSPL_SUB_DEPARTMENT_MASTER where SUB_DEPARTMENT_CODE= '" & obj.Code & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SUB_DEPARTMENT_MASTER", OMInsertOrUpdate.Insert, "")
                Else
                    Throw New Exception("This Code Is Already Exist")
                End If

            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SUB_DEPARTMENT_MASTER", OMInsertOrUpdate.Update, "SUB_DEPARTMENT_CODE='" + obj.Code + "'")
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetCodeByName(ByVal strName As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = " Select SUB_DEPARTMENT_CODE from TSPL_SUB_DEPARTMENT_MASTER where Description = '" + strName + "' "
        Dim StrCode As String = clsDBFuncationality.getSingleValue(qry, trans)
        Return StrCode
    End Function
    Public Shared Function CheckNewEntry(ByVal Code As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = "select SUB_DEPARTMENT_CODE from TSPL_SUB_DEPARTMENT_MASTER where SUB_DEPARTMENT_CODE ='" + Code + "'   "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If

    End Function
    
End Class
