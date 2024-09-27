Imports common
Imports System.Data
Imports System.Data.SqlClient

Public Class ClsReimbursementTypeMaster
#Region "Variables"
    Public Reimbursement_Code As String = Nothing
    Public Description As String = Nothing
    Public Reimbursement_Type As String = Nothing
    Public Travel_Type As String = Nothing
#End Region
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "select TSPL_HR_REIMBURSEMENT_TYPE_MASTER.Reimbursement_Code as [Code] ,TSPL_HR_REIMBURSEMENT_TYPE_MASTER.Description as [Description],CASE WHEN ISNULL(TSPL_HR_REIMBURSEMENT_TYPE_MASTER.Reimbursement_Type,'')='T' THEN 'Travel' WHEN ISNULL(TSPL_HR_REIMBURSEMENT_TYPE_MASTER.Reimbursement_Type,'')='F' THEN 'Food' WHEN ISNULL(TSPL_HR_REIMBURSEMENT_TYPE_MASTER.Reimbursement_Type,'')='C' THEN 'Conveyence' WHEN ISNULL(TSPL_HR_REIMBURSEMENT_TYPE_MASTER.Reimbursement_Type,'')='O' THEN 'Others' WHEN ISNULL(TSPL_HR_REIMBURSEMENT_TYPE_MASTER.Reimbursement_Type,'')='M' THEN 'Miscellaneous' END as [Reimbursement Type],CASE WHEN ISNULL(TSPL_HR_REIMBURSEMENT_TYPE_MASTER.Travel_Type,'')='D' THEN 'Domestic' WHEN ISNULL(TSPL_HR_REIMBURSEMENT_TYPE_MASTER.Travel_Type,'')='I' THEN 'International' WHEN ISNULL(TSPL_HR_REIMBURSEMENT_TYPE_MASTER.Travel_Type,'')='I' THEN 'International' ELSE ''  END AS [Travel Type] ,TSPL_HR_REIMBURSEMENT_TYPE_MASTER.Created_By as [Created By] ,CONVERT(VARCHAR,TSPL_HR_REIMBURSEMENT_TYPE_MASTER.Created_Date,103) as [Created Date] ,TSPL_HR_REIMBURSEMENT_TYPE_MASTER.Modified_By as [Modified By] ,CONVERT(VARCHAR,TSPL_HR_REIMBURSEMENT_TYPE_MASTER.Modified_Date,103) as [Modified Date]  From TSPL_HR_REIMBURSEMENT_TYPE_MASTER  "
        str = clsCommon.ShowSelectForm("RMHRFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
    Public Shared Function SaveData(ByVal obj As ClsReimbursementTypeMaster, ByVal strCode As String) As Boolean
        Try
            Dim issaved As Boolean = True

            Dim coll As New Hashtable()

            Dim qry As String = "SELECT Count(*) FROM TSPL_HR_REIMBURSEMENT_TYPE_MASTER where Reimbursement_Code= '" & obj.Reimbursement_Code & "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Reimbursement_Type", obj.Reimbursement_Type)
            clsCommon.AddColumnsForChange(coll, "Travel_Type", obj.Travel_Type)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)

            If check = 0 Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Reimbursement_Code", obj.Reimbursement_Code)
                issaved = issaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_REIMBURSEMENT_TYPE_MASTER", OMInsertOrUpdate.Insert, "")
            Else
                issaved = issaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_REIMBURSEMENT_TYPE_MASTER", OMInsertOrUpdate.Update, "Reimbursement_Code='" + obj.Reimbursement_Code + "'")
            End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsReimbursementTypeMaster
        Try
            Dim obj As ClsReimbursementTypeMaster = Nothing
            Dim qry As String = "select * from TSPL_HR_REIMBURSEMENT_TYPE_MASTER where 2=2"
            Select Case NavType
                Case NavigatorType.First
                    qry += " and Reimbursement_Code = (select MIN(Reimbursement_Code) from TSPL_HR_REIMBURSEMENT_TYPE_MASTER)"
                Case NavigatorType.Last
                    qry += " and Reimbursement_Code = (select Max(Reimbursement_Code) from TSPL_HR_REIMBURSEMENT_TYPE_MASTER)"
                Case NavigatorType.Next
                    qry += " and Reimbursement_Code = (select Min(Reimbursement_Code) from TSPL_HR_REIMBURSEMENT_TYPE_MASTER where  Reimbursement_Code>'" + strCode + "')"
                Case NavigatorType.Previous
                    qry += " and Reimbursement_Code = (select Max(Reimbursement_Code) from TSPL_HR_REIMBURSEMENT_TYPE_MASTER where Reimbursement_Code<'" + strCode + "')"
                Case NavigatorType.Current
                    qry += " and Reimbursement_Code = '" + strCode + "'"

            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New ClsReimbursementTypeMaster()
                obj.Reimbursement_Code = clsCommon.myCstr(dt.Rows(0)("Reimbursement_Code"))
                obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
                obj.Reimbursement_Type = clsCommon.myCstr(dt.Rows(0)("Reimbursement_Type"))
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
            qry = "DELETE FROM TSPL_HR_REIMBURSEMENT_TYPE_MASTER WHERE Reimbursement_Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    ' ----------------- Get Reimbursement Type ------------------------
    Public Shared Function GetRT() As DataTable
        Dim DT_RT As DataTable = New DataTable
        DT_RT.Columns.Add("Code", GetType(String))
        DT_RT.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT_RT.NewRow()
        DR("Name") = "Travel"
        DR("Code") = "T"
        DT_RT.Rows.Add(DR)

        DR = DT_RT.NewRow()
        DR("Name") = "Food"
        DR("Code") = "F"
        DT_RT.Rows.Add(DR)

        DR = DT_RT.NewRow()
        DR("Name") = "Conveyance"
        DR("Code") = "C"
        DT_RT.Rows.Add(DR)

        DR = DT_RT.NewRow()
        DR("Name") = "Others"
        DR("Code") = "O"
        DT_RT.Rows.Add(DR)

        DR = DT_RT.NewRow()
        DR("Name") = "Miscellaneous"
        DR("Code") = "M"
        DT_RT.Rows.Add(DR)
        DT_RT.AcceptChanges()

        Return DT_RT
    End Function
    ' ----------------- Get Travel Type ------------------------
    Public Shared Function GetTT() As DataTable
        Dim DT_TT As DataTable = New DataTable
        DT_TT.Columns.Add("Code", GetType(String))
        DT_TT.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT_TT.NewRow()
        DR("Name") = ""
        DR("Code") = ""
        DT_TT.Rows.Add(DR)

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
