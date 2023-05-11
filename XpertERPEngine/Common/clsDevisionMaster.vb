Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsDevisionMaster
#Region "Variables"
    Public Code As String
    Public Description As String
    Public Name As String
    Public Location_code As String
    Public Location_Name As String
#End Region

    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_DEVISION_MASTER.DEVISION_CODE as [Code] ,TSPL_DEVISION_MASTER.DEVISION_NAME as [Division Name] ,TSPL_DEVISION_MASTER.DESCRIPTION as [Description] ,TSPL_DEVISION_MASTER.Created_By as [Created By] ,TSPL_DEVISION_MASTER.Created_Date as [Created Date] ,TSPL_DEVISION_MASTER.Modified_By as [Modified By] ,TSPL_DEVISION_MASTER.Modified_Date as [Modified Date]  From TSPL_DEVISION_MASTER   "
        str = clsCommon.ShowSelectForm("DIVMSTFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    Public Shared Function GetName(ByVal strDivison As String, ByVal trans As SqlTransaction) As String
        Try
            Dim qry As String = "select DEVISION_NAME from TSPL_DEVISION_MASTER where DEVISION_CODE='" + strDivison + "'"
            Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsDevisionMaster
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
            qry = "delete from TSPL_DEVISION_MASTER where DEVISION_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)


        Catch ex As Exception

            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsDevisionMaster
        Dim obj As clsDevisionMaster = Nothing
        Dim qry As String = "select DEVISION_CODE, DEVISION_NAME, DESCRIPTION,TSPL_DEVISION_MASTER.Location_Code,TSPL_LOCATION_MASTER.Location_Desc   from TSPL_DEVISION_MASTER"
        qry += " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_DEVISION_MASTER.Location_Code where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and DEVISION_CODE = (select MIN(DEVISION_CODE) from TSPL_DEVISION_MASTER)"
            Case NavigatorType.Last
                qry += " and DEVISION_CODE = (select Max(DEVISION_CODE) from TSPL_DEVISION_MASTER)"
            Case NavigatorType.Next
                qry += " and DEVISION_CODE = (select Min(DEVISION_CODE) from TSPL_DEVISION_MASTER where  DEVISION_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and DEVISION_CODE = (select Max(DEVISION_CODE) from TSPL_DEVISION_MASTER where DEVISION_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and DEVISION_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsDevisionMaster()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("DEVISION_CODE"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
            obj.Name = clsCommon.myCstr(dt.Rows(0)("DEVISION_NAME"))
            obj.Location_code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Location_Name = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
        End If
        Return obj


    End Function

    Public Function SaveData(ByVal obj As clsDevisionMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "DEVISION_NAME", obj.Name)
            clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Location_code", obj.Location_code)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
            If isNewEntry Then
                If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False)) Then
                    Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_DEVISION_MASTER where DEVISION_CODE='" & obj.Code & "'")
                    If ChkNewEntry = 0 Then
                        obj.Code = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"), clsDocType.DevisionMaster, "", "")
                        If clsCommon.myLen(obj.Code) <= 0 Then
                            Throw New Exception("Error in Code Generation")
                        End If
                    End If
                End If
                clsCommon.AddColumnsForChange(coll, "DEVISION_CODE", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
                Dim qry As String = "SELECT Count(*) FROM TSPL_DEVISION_MASTER where DEVISION_CODE= '" & obj.Code & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DEVISION_MASTER", OMInsertOrUpdate.Insert, "")
                Else
                    Throw New Exception("This Code Is Already Exist")
                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DEVISION_MASTER", OMInsertOrUpdate.Update, "DEVISION_CODE='" + obj.Code + "'")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetCodeByName(ByVal strName As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = " Select DEVISION_CODE from TSPL_DEVISION_MASTER where DEVISION_NAME = '" + strName + "' "
        Dim StrCode As String = clsDBFuncationality.getSingleValue(qry, trans)
        Return StrCode
    End Function
    Public Shared Function CheckNewEntry(ByVal Code As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = "select DEVISION_CODE from TSPL_DEVISION_MASTER where DEVISION_CODE ='" + Code + "'   "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If

    End Function
End Class
