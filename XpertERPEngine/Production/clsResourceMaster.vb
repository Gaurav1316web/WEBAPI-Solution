Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsResourceMaster

#Region "Variables"

    Public RESOURCE_CODE As String
    Public DESCRIPTION As String
    Public STATUS As String
    ' Public INACTIVE_DATE",
    Public RESOURCE_TYPE As String
    Public UNIT_CODE As String
    Public UNIT_CODE_OTHER As String
    Public COST As Double
    Public COMMENTS As String
    Public Modified_By As String
    Public Modified_Date As String

#End Region
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_MF_RESOURCE_MASTER.RESOURCE_CODE as [Code] ,TSPL_MF_RESOURCE_MASTER.DESCRIPTION as [Description] ,TSPL_MF_RESOURCE_MASTER.STATUS as [Status] ,TSPL_MF_RESOURCE_MASTER.RESOURCE_TYPE as [Resource Type] ,TSPL_MF_RESOURCE_MASTER.UNIT_CODE as [Unit Code] ,TSPL_MF_RESOURCE_MASTER.COST as [Cost] ,TSPL_MF_RESOURCE_MASTER.COMMENTS as [Comments] ,TSPL_MF_RESOURCE_MASTER.Created_By as [Created By] ,TSPL_MF_RESOURCE_MASTER.Created_Date as [Created Date] ,TSPL_MF_RESOURCE_MASTER.Modified_By as [Modified By] ,TSPL_MF_RESOURCE_MASTER.Modified_Date as [Modified Date] ,TSPL_MF_RESOURCE_MASTER.UNIT_CODE_OTHER as [Unit Code Other]  From TSPL_MF_RESOURCE_MASTER   "
        str = clsCommon.ShowSelectForm("RESMSTFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'

    Public Shared Function GetData(ByVal strRESOURCE_CODE As String, ByVal NavType As NavigatorType) As clsResourceMaster
        Return GetData(strRESOURCE_CODE, NavType, Nothing)
    End Function
    Public Shared Function DeleteData(ByVal strRESOURCE_CODE As String) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False
            If (clsCommon.myLen(strRESOURCE_CODE) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            Dim qry As String
            qry = "delete from TSPL_MF_RESOURCE_MASTER where RESOURCE_CODE ='" + strRESOURCE_CODE + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strRESOURCE_CODE As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsResourceMaster
        Dim obj As clsResourceMaster = Nothing
        Dim qry As String = "select * from TSPL_MF_RESOURCE_MASTER where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and RESOURCE_CODE = (select MIN(RESOURCE_CODE) from TSPL_MF_RESOURCE_MASTER)"
            Case NavigatorType.Last
                qry += " and RESOURCE_CODE = (select Max(RESOURCE_CODE) from TSPL_MF_RESOURCE_MASTER)"
            Case NavigatorType.Next
                qry += " and RESOURCE_CODE = (select Min(RESOURCE_CODE) from TSPL_MF_RESOURCE_MASTER where  RESOURCE_CODE > '" + strRESOURCE_CODE + "')"
            Case NavigatorType.Previous
                qry += " and RESOURCE_CODE = (select Max(RESOURCE_CODE) from TSPL_MF_RESOURCE_MASTER where RESOURCE_CODE < '" + strRESOURCE_CODE + "')"
            Case NavigatorType.Current
                qry += " and RESOURCE_CODE = '" + strRESOURCE_CODE + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsResourceMaster()
            obj.RESOURCE_CODE = clsCommon.myCstr(dt.Rows(0)("RESOURCE_CODE"))
            obj.DESCRIPTION = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
            obj.COMMENTS = clsCommon.myCstr(dt.Rows(0)("COMMENTS"))
            obj.STATUS = clsCommon.myCstr(dt.Rows(0)("STATUS"))
            obj.RESOURCE_TYPE = clsCommon.myCstr(dt.Rows(0)("RESOURCE_TYPE"))
            obj.UNIT_CODE = clsCommon.myCstr(dt.Rows(0)("UNIT_CODE"))
            obj.UNIT_CODE_OTHER = clsCommon.myCstr(dt.Rows(0)("UNIT_CODE_OTHER"))
            obj.COST = clsCommon.myCdbl(dt.Rows(0)("COST"))
            obj.Modified_By = clsCommon.myCstr(dt.Rows(0)("Modified_By"))
            obj.Modified_Date = clsCommon.myCstr(clsCommon.GetPrintDate(dt.Rows(0)("Modified_Date"), "dd/MMM/yyyy"))
        End If
        Return obj
    End Function

    Public Function SaveData(ByVal obj As clsResourceMaster, ByVal isNewEntry As Boolean, ByVal isFromImport As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "COMMENTS", obj.COMMENTS)
            clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.DESCRIPTION)
            clsCommon.AddColumnsForChange(coll, "RESOURCE_TYPE", obj.RESOURCE_TYPE, True)
            clsCommon.AddColumnsForChange(coll, "UNIT_CODE", obj.UNIT_CODE, True)
            clsCommon.AddColumnsForChange(coll, "UNIT_CODE_OTHER", obj.UNIT_CODE_OTHER)
            clsCommon.AddColumnsForChange(coll, "STATUS", obj.STATUS)
            clsCommon.AddColumnsForChange(coll, "COST", obj.COST)

            If isFromImport Then
                clsCommon.AddColumnsForChange(coll, "Modified_By", obj.Modified_By)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(obj.Modified_Date, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
            End If
            If isNewEntry Then
                If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False)) Then
                    Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_MF_RESOURCE_MASTER where RESOURCE_CODE='" & obj.RESOURCE_CODE & "'")
                    If ChkNewEntry = 0 Then
                        obj.RESOURCE_CODE = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"), clsDocType.ResourceMaster, "", "")
                        If clsCommon.myLen(obj.RESOURCE_CODE) <= 0 Then
                            Throw New Exception("Error in Code Generation")
                        End If
                    End If
                End If
                clsCommon.AddColumnsForChange(coll, "RESOURCE_CODE", obj.RESOURCE_CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
                Dim qry As String = "SELECT Count(*) FROM TSPL_MF_RESOURCE_MASTER where RESOURCE_CODE= '" & obj.RESOURCE_CODE & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_RESOURCE_MASTER", OMInsertOrUpdate.Insert, "")
                Else
                    'common.clsCommon.MyMessageBoxShow("This RESOURCE_CODE Is Already Exist")
                    'Exit Function
                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_RESOURCE_MASTER", OMInsertOrUpdate.Update, "RESOURCE_CODE='" + obj.RESOURCE_CODE + "'")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
End Class
