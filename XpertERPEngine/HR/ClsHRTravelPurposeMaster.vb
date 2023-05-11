Imports common
Imports System.Data
Imports System.Data.SqlClient

Public Class ClsHRTravelPurposeMaster
#Region "Variables"
    Public Travel_Code As String = Nothing
    Public Travel_Type As String = Nothing
    Public Travel_Desp As String = Nothing
#End Region

    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function GetFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "select TSPL_HR_TRAVEL_PURPOSE_MASTER.Travel_Code as [Code] ,TSPL_HR_TRAVEL_PURPOSE_MASTER.Travel_Desp as [Description],CASE WHEN ISNULL(TSPL_HR_TRAVEL_PURPOSE_MASTER.Travel_Type,'')='D' THEN 'Domestic' WHEN ISNULL(TSPL_HR_TRAVEL_PURPOSE_MASTER.Travel_Type,'')='I' THEN 'International' WHEN ISNULL(TSPL_HR_TRAVEL_PURPOSE_MASTER.Travel_Type,'')='I' THEN 'International' END AS [Travel Type] ,TSPL_HR_TRAVEL_PURPOSE_MASTER.Created_By as [Created By] ,CONVERT(VARCHAR,TSPL_HR_TRAVEL_PURPOSE_MASTER.Created_Date,103) as [Created Date] ,TSPL_HR_TRAVEL_PURPOSE_MASTER.Modified_By as [Modified By] ,CONVERT(VARCHAR,TSPL_HR_TRAVEL_PURPOSE_MASTER.Modified_Date,103) as [Modified Date]  From TSPL_HR_TRAVEL_PURPOSE_MASTER  "
        str = clsCommon.ShowSelectForm("TPHRFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
    Public Shared Function SaveData(ByVal obj As ClsHRTravelPurposeMaster, ByVal strCode As String) As Boolean
        Try
            Dim issaved As Boolean = True

            Dim coll As New Hashtable()

            Dim qry As String = "SELECT Count(*) FROM TSPL_HR_TRAVEL_PURPOSE_MASTER where Travel_Code= '" & obj.Travel_Code & "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

            clsCommon.AddColumnsForChange(coll, "Travel_Desp", obj.Travel_Desp)
            clsCommon.AddColumnsForChange(coll, "Travel_Type", obj.Travel_Type)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)

            If check = 0 Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Travel_Code", obj.Travel_Code)
                issaved = issaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_TRAVEL_PURPOSE_MASTER", OMInsertOrUpdate.Insert, "")
            Else
                issaved = issaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_TRAVEL_PURPOSE_MASTER", OMInsertOrUpdate.Update, "Travel_Code='" + obj.Travel_Code + "'")
            End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsHRTravelPurposeMaster
        Try
            Dim obj As ClsHRTravelPurposeMaster = Nothing
            Dim qry As String = "select * from TSPL_HR_TRAVEL_PURPOSE_MASTER where 2=2"
            Select Case NavType
                Case NavigatorType.First
                    qry += " and Travel_Code = (select MIN(Travel_Code) from TSPL_HR_TRAVEL_PURPOSE_MASTER)"
                Case NavigatorType.Last
                    qry += " and Travel_Code = (select Max(Travel_Code) from TSPL_HR_TRAVEL_PURPOSE_MASTER)"
                Case NavigatorType.Next
                    qry += " and Travel_Code = (select Min(Travel_Code) from TSPL_HR_TRAVEL_PURPOSE_MASTER where  Travel_Code>'" + strCode + "')"
                Case NavigatorType.Previous
                    qry += " and Travel_Code = (select Max(Travel_Code) from TSPL_HR_TRAVEL_PURPOSE_MASTER where Travel_Code<'" + strCode + "')"
                Case NavigatorType.Current
                    qry += " and Travel_Code = '" + strCode + "'"

            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New ClsHRTravelPurposeMaster()
                obj.Travel_Code = clsCommon.myCstr(dt.Rows(0)("Travel_Code"))
                obj.Travel_Desp = clsCommon.myCstr(dt.Rows(0)("Travel_Desp"))
                obj.Travel_Type = clsCommon.myCstr(dt.Rows(0)("Travel_Type"))
            End If
            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean

        Try
            isSaved = False
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = "DELETE FROM TSPL_HR_TRAVEL_PURPOSE_MASTER WHERE Travel_Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    ' ----------------- Get Travel Type ------------------------
    Public Shared Function GetTT() As DataTable
        Dim DT_TT As DataTable = New DataTable
        DT_TT.Columns.Add("Code", GetType(String))
        DT_TT.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT_TT.NewRow()
        DR = DT_TT.NewRow()
        DR("Name") = "Domestic"
        DR("Code") = "D"
        DT_TT.Rows.Add(DR)

        DR = DT_TT.NewRow()
        DR("Name") = "International"
        DR("Code") = "I"
        DT_TT.Rows.Add(DR)

        DT_TT.AcceptChanges()

        Return DT_TT
    End Function
End Class
