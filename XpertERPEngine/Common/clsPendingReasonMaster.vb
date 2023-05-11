
Imports common
Imports System.Data.SqlClient


Public Class clsPendingReasonMaster
#Region "veriables"
    Public Pending_Reason_code As String = Nothing
    Public description As String = Nothing

#End Region
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_PENDING_REASON_MASTER.PENDING_REASON_CODE as [Code] ,TSPL_PENDING_REASON_MASTER.DESCRIPTION as [Description] ,TSPL_PENDING_REASON_MASTER.Created_By as [Created By] ,TSPL_PENDING_REASON_MASTER.Created_Date as [Created Date] ,TSPL_PENDING_REASON_MASTER.Modified_By as [Modified By] ,TSPL_PENDING_REASON_MASTER.Modified_Date as [Modified Date]  From TSPL_PENDING_REASON_MASTER   "
        str = clsCommon.ShowSelectForm("PENDRSNMST", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
    '' This Code For Retriving Data from TSPL_PENDING_REASON_MASTER


    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsPendingReasonMaster
        Dim obj As clsPendingReasonMaster = Nothing
        Dim qst As String = "select  PENDING_REASON_CODE , description   from  TSPL_PENDING_REASON_MASTER "
        Select Case NavType
            Case NavigatorType.Current
                qst += " where  Pending_Reason_code ='" & strCode & "' "
            Case NavigatorType.Next
                qst += " where  Pending_Reason_code in (select min(t.Pending_Reason_Code ) from TSPL_PENDING_REASON_MASTER  as t where t.pending_reason_Code  >'" + strCode + "')"
            Case NavigatorType.First
                qst += " where  pending_reason_code in (select min(t.pending_reason_Code ) from TSPL_PENDING_REASON_MASTER  as t)"
            Case NavigatorType.Last
                qst += " where  pending_reason_code in (select max(t.pending_reason_Code ) from TSPL_PENDING_REASON_MASTER  as t)"
            Case NavigatorType.Previous
                qst += " where  pending_reason_code in (select max(t.pending_reason_Code ) from TSPL_PENDING_REASON_MASTER  as t where t.pending_reason_Code  <'" + strCode + "')"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsPendingReasonMaster
            obj.Pending_Reason_code = clsCommon.myCstr(dt.Rows(0)("PENDING_REASON_CODE"))
            obj.description = clsCommon.myCstr(dt.Rows(0)("description"))
        End If
        Return obj
    End Function
    '' For Delete of Data in TSPL_PENDING_REASON_MASTER
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim qry As String = "Delete from TSPL_PENDING_REASON_MASTER where pending_reason_Code='" + strCode + "'"
        Return clsDBFuncationality.ExecuteNonQuery(qry)
    End Function
    '' For Save Data in TSPL_PENDING_REASON_MASTER
    Public Function SaveData(ByVal obj As clsPendingReasonMaster, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Pending_Reason_code", obj.Pending_Reason_code)
            clsCommon.AddColumnsForChange(coll, "Description", obj.description)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
                If clsCommon.myLen(obj.Pending_Reason_code) = 0 Then
                    Throw New Exception("Please fill Code")
                End If
                Dim qry As String = "SELECT Count(*) FROM TSPL_PENDING_REASON_MASTER where Pending_Reason_Code = '" & obj.Pending_Reason_code & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PENDING_REASON_MASTER", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This Code Is Already Exist")
                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PENDING_REASON_MASTER ", OMInsertOrUpdate.Update, " Pending_Reason_Code='" + obj.Pending_Reason_code + "'", trans)
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function CheckNewEntry(ByVal Code As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = "select Pending_reason_code from TSPL_PENDING_REASON_MASTER where PENDING_REASON_CODE ='" + Code + "'   "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If

    End Function
End Class
