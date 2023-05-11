Imports common
Imports System.Data
Imports System.Data.SqlClient

Public Class ClsHRTravelModeTypeMaster
#Region "Variables"
    Public Travel_Mode_Code As String = Nothing
    Public Desp As String = Nothing
    Public Is_Applicable As Integer
#End Region
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function GetFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "SELECT TSPL_HR_TRAVEL_MODE_TYPE_MASTER.Travel_Mode_Code as [Code],TSPL_HR_TRAVEL_MODE_TYPE_MASTER.Description as [Description],TSPL_HR_TRAVEL_MODE_TYPE_MASTER.Is_Applicable AS [Is Applicable] ,TSPL_HR_TRAVEL_MODE_TYPE_MASTER.Created_By as [Created By] ,CONVERT(VARCHAR,TSPL_HR_TRAVEL_MODE_TYPE_MASTER.Created_Date,103) as [Created Date] ,TSPL_HR_TRAVEL_MODE_TYPE_MASTER.Modified_By as [Modified By] ,CONVERT(VARCHAR,TSPL_HR_TRAVEL_MODE_TYPE_MASTER.Modified_Date,103) as [Modified Date]  From TSPL_HR_TRAVEL_MODE_TYPE_MASTER  "
        str = clsCommon.ShowSelectForm("TMTHRFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
    Public Shared Function SaveData(ByVal obj As ClsHRTravelModeTypeMaster, ByVal strCode As String) As Boolean
        Try
            Dim issaved As Boolean = True

            Dim coll As New Hashtable()

            Dim qry As String = "SELECT Count(*) FROM TSPL_HR_TRAVEL_MODE_TYPE_MASTER where Travel_Mode_Code= '" & obj.Travel_Mode_Code & "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

            clsCommon.AddColumnsForChange(coll, "Description", obj.Desp)
            clsCommon.AddColumnsForChange(coll, "Is_Applicable", obj.Is_Applicable)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)

            If check = 0 Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Travel_Mode_Code", obj.Travel_Mode_Code)
                issaved = issaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_TRAVEL_MODE_TYPE_MASTER", OMInsertOrUpdate.Insert, "")
            Else
                issaved = issaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_TRAVEL_MODE_TYPE_MASTER", OMInsertOrUpdate.Update, "Travel_Mode_Code='" + obj.Travel_Mode_Code + "'")
            End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsHRTravelModeTypeMaster
        Try
            Dim obj As ClsHRTravelModeTypeMaster = Nothing
            Dim qry As String = "select * from TSPL_HR_TRAVEL_MODE_TYPE_MASTER where 2=2"
            Select Case NavType
                Case NavigatorType.First
                    qry += " and Travel_Mode_Code = (select MIN(Travel_Mode_Code) from TSPL_HR_TRAVEL_MODE_TYPE_MASTER)"
                Case NavigatorType.Last
                    qry += " and Travel_Mode_Code = (select Max(Travel_Mode_Code) from TSPL_HR_TRAVEL_MODE_TYPE_MASTER)"
                Case NavigatorType.Next
                    qry += " and Travel_Mode_Code = (select Min(Travel_Mode_Code) from TSPL_HR_TRAVEL_MODE_TYPE_MASTER where  Travel_Mode_Code>'" + strCode + "')"
                Case NavigatorType.Previous
                    qry += " and Travel_Mode_Code = (select Max(Travel_Mode_Code) from TSPL_HR_TRAVEL_MODE_TYPE_MASTER where Travel_Mode_Code<'" + strCode + "')"
                Case NavigatorType.Current
                    qry += " and Travel_Mode_Code = '" + strCode + "'"

            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New ClsHRTravelModeTypeMaster()
                obj.Travel_Mode_Code = clsCommon.myCstr(dt.Rows(0)("Travel_Mode_Code"))
                obj.Desp = clsCommon.myCstr(dt.Rows(0)("Description"))
                obj.Is_Applicable = clsCommon.myCdbl(dt.Rows(0)("Is_Applicable"))
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
                Throw New Exception("Code not found to delete")
            End If

            Dim qry As String
            qry = "DELETE FROM TSPL_HR_TRAVEL_MODE_TYPE_MASTER WHERE Travel_Mode_Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
End Class
