Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsConveyanceRateMaster
#Region "Variables"
    Public CONV_RATE_CODE As String
    Public Description As String
    Public CONV_TYPE As String
    Public CONV_RATE As Double
    Public DIST_LIMIT As Double
    Public Comp_Code As String
#End Region

    '----------------CONV_RATE_CODE For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_CONVEYANCE_RATE_MASTER.CONV_RATE_CODE as [Code],TSPL_CONVEYANCE_RATE_MASTER.DESCRIPTION as [Description] ,TSPL_CONVEYANCE_RATE_MASTER.CONV_TYPE as [Conveyance Type] ,TSPL_CONVEYANCE_RATE_MASTER.CONV_RATE as [Conveyance Rate] ,TSPL_CONVEYANCE_RATE_MASTER.DIST_LIMIT as [Distance Limit] ,TSPL_CONVEYANCE_RATE_MASTER.Created_By as [Created By] ,TSPL_CONVEYANCE_RATE_MASTER.Created_Date as [Created Date] ,TSPL_CONVEYANCE_RATE_MASTER.Modified_By as [Modified By] ,TSPL_CONVEYANCE_RATE_MASTER.Modified_Date as [Modified Date]  From TSPL_CONVEYANCE_RATE_MASTER "
        str = clsCommon.ShowSelectForm("OTMST", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of CONV_RATE_CODE For Get Finder--------------------------------------------------------------'
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsConveyanceRateMaster
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function GetName(ByVal strCode As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select DESCRIPTION from TSPL_CONVEYANCE_RATE_MASTER where CONV_RATE_CODE = '" + strCode + "'"
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean

        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("CONV_RATE_CODE not found to Delete")
            End If

            Dim qry As String
            qry = "delete from TSPL_CONVEYANCE_RATE_MASTER where CONV_RATE_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)


        Catch ex As Exception

            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsConveyanceRateMaster
        Dim obj As clsConveyanceRateMaster = Nothing
        Dim qry As String = "select * from TSPL_CONVEYANCE_RATE_MASTER where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and CONV_RATE_CODE = (select MIN(CONV_RATE_CODE) from TSPL_CONVEYANCE_RATE_MASTER)"
            Case NavigatorType.Last
                qry += " and CONV_RATE_CODE = (select Max(CONV_RATE_CODE) from TSPL_CONVEYANCE_RATE_MASTER)"
            Case NavigatorType.Next
                qry += " and CONV_RATE_CODE = (select Min(CONV_RATE_CODE) from TSPL_CONVEYANCE_RATE_MASTER where  CONV_RATE_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and CONV_RATE_CODE = (select Max(CONV_RATE_CODE) from TSPL_CONVEYANCE_RATE_MASTER where CONV_RATE_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and CONV_RATE_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsConveyanceRateMaster()
            obj.CONV_RATE_CODE = clsCommon.myCstr(dt.Rows(0)("CONV_RATE_CODE"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
            obj.CONV_TYPE = clsCommon.myCstr(dt.Rows(0)("CONV_TYPE"))
            obj.CONV_RATE = clsCommon.myCdbl(dt.Rows(0)("CONV_RATE"))
            obj.DIST_LIMIT = clsCommon.myCdbl(dt.Rows(0)("DIST_LIMIT"))
            obj.Comp_Code = clsCommon.myCstr(dt.Rows(0)("Comp_Code"))

        End If
        Return obj


    End Function

    Public Function SaveData(ByVal obj As clsConveyanceRateMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "CONV_TYPE", obj.CONV_TYPE)
            clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.Description)
            clsCommon.AddColumnsForChange(coll, "CONV_RATE", obj.CONV_RATE)
            clsCommon.AddColumnsForChange(coll, "DIST_LIMIT", obj.DIST_LIMIT)
            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
            If isNewEntry Then
                If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False)) Then
                    Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_CONVEYANCE_RATE_MASTER where CONV_RATE_CODE='" & obj.CONV_RATE_CODE & "'")
                    If ChkNewEntry = 0 Then
                        obj.CONV_RATE_CODE = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"), clsDocType.ConveyanceRateMaster, "", "")
                        If clsCommon.myLen(obj.CONV_RATE_CODE) <= 0 Then
                            Throw New Exception("Error in Code Generation")
                        End If
                    End If
                End If
                clsCommon.AddColumnsForChange(coll, "CONV_RATE_CODE", obj.CONV_RATE_CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
                Dim qry As String = "SELECT Count(*) FROM TSPL_CONVEYANCE_RATE_MASTER where CONV_RATE_CODE= '" & obj.CONV_RATE_CODE & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CONVEYANCE_RATE_MASTER", OMInsertOrUpdate.Insert, "")
                Else
                    Throw New Exception("This CONV RATE CODE Already Exists")

                End If

            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CONVEYANCE_RATE_MASTER", OMInsertOrUpdate.Update, "CONV_RATE_CODE='" + obj.CONV_RATE_CODE + "'")
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function CheckNewEntry(ByVal Code As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = "select CONV_RATE_CODE from TSPL_CONVEYANCE_RATE_MASTER where CONV_RATE_CODE ='" + Code + "'   "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If

    End Function
    Public Shared Function GetCboConvTypeDataTable() As DataTable
        Dim DT As DataTable = New DataTable
        DT.Columns.Add("Code", GetType(String))
        DT.Columns.Add("Name", GetType(String))
        Dim DR As DataRow = DT.NewRow()
        DR("Code") = "TW"
        DR("Name") = "Two Wheeler"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Code") = "FW"
        DR("Name") = "Four Wheeler"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Code") = "None"
        DR("Name") = "None"
        DT.Rows.Add(DR)

        DT.AcceptChanges()
        Return DT
    End Function
End Class
