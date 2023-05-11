Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsBonusMaster
#Region "Variables"
    Public Code As String
    Public Description As String
    Public Name As String
    Public COND_BASIC_PER_MONTH As Double
    Public COND_MAX_EARNING_PER_MONTH As Double
    Public COND_MAX_BONUS_PER_YEAR As Double
    Public BONUS_RATE As Double
    Public Calculation_Method As String
    Public Is_Consider_Pay_Days As Boolean
#End Region

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_BONUS_MASTER.BONUS_CODE as [Code] ,TSPL_BONUS_MASTER.BONUS_NAME as [Bonus Name] ,TSPL_BONUS_MASTER.COND_BASIC_PER_MONTH as [Cond Basic Per Month] ,TSPL_BONUS_MASTER.COND_MAX_EARNING_PER_MONTH as [Cond Max Earning Per Month] ,TSPL_BONUS_MASTER.COND_MAX_BONUS_PER_YEAR as [Cond Max Bonus Per Year] ,TSPL_BONUS_MASTER.BONUS_RATE as [Bonus Rate] ,TSPL_BONUS_MASTER.DESCRIPTION as [Description] ,TSPL_BONUS_MASTER.Created_By as [Created By] ,TSPL_BONUS_MASTER.Created_Date as [Created Date] ,TSPL_BONUS_MASTER.Modified_By as [Modified By] ,TSPL_BONUS_MASTER.Modified_Date as [Modified Date]  From TSPL_BONUS_MASTER   "
        str = clsCommon.ShowSelectForm("BONUSMST", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsBonusMaster
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
            qry = "delete from TSPL_BONUS_MASTER where BONUS_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)


        Catch ex As Exception

            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsBonusMaster
        Dim obj As clsBonusMaster = Nothing
        Dim qry As String = "select * from TSPL_BONUS_MASTER where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and BONUS_CODE = (select MIN(BONUS_CODE) from TSPL_BONUS_MASTER)"
            Case NavigatorType.Last
                qry += " and BONUS_CODE = (select Max(BONUS_CODE) from TSPL_BONUS_MASTER)"
            Case NavigatorType.Next
                qry += " and BONUS_CODE = (select Min(BONUS_CODE) from TSPL_BONUS_MASTER where  BONUS_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and BONUS_CODE = (select Max(BONUS_CODE) from TSPL_BONUS_MASTER where BONUS_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and BONUS_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsBonusMaster()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("BONUS_CODE"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
            obj.Name = clsCommon.myCstr(dt.Rows(0)("BONUS_NAME"))
            obj.COND_BASIC_PER_MONTH = clsCommon.myCdbl(dt.Rows(0)("COND_BASIC_PER_MONTH"))
            obj.COND_MAX_EARNING_PER_MONTH = clsCommon.myCdbl(dt.Rows(0)("COND_MAX_EARNING_PER_MONTH"))
            obj.COND_MAX_BONUS_PER_YEAR = clsCommon.myCdbl(dt.Rows(0)("COND_MAX_BONUS_PER_YEAR"))
            obj.BONUS_RATE = clsCommon.myCdbl(dt.Rows(0)("BONUS_RATE"))
            obj.Calculation_Method = clsCommon.myCstr(dt.Rows(0)("Calculation_Method"))
            obj.Is_Consider_Pay_Days = (clsCommon.myCdbl(dt.Rows(0)("Is_Consider_Pay_Days")) = 1)
        End If
        Return obj
    End Function

    Public Function SaveData(ByVal obj As clsBonusMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "COND_BASIC_PER_MONTH", obj.COND_BASIC_PER_MONTH)
            clsCommon.AddColumnsForChange(coll, "COND_MAX_EARNING_PER_MONTH", obj.COND_MAX_EARNING_PER_MONTH)
            clsCommon.AddColumnsForChange(coll, "COND_MAX_BONUS_PER_YEAR", obj.COND_MAX_BONUS_PER_YEAR)
            clsCommon.AddColumnsForChange(coll, "BONUS_RATE", obj.BONUS_RATE)
            clsCommon.AddColumnsForChange(coll, "BONUS_NAME", obj.Name)
            clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Is_Consider_Pay_Days", IIf(obj.Is_Consider_Pay_Days, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Calculation_Method", obj.Calculation_Method)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))

            If isNewEntry Then
                If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False)) Then
                    Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_BONUS_MASTER where BONUS_CODE='" & obj.Code & "'")
                    If ChkNewEntry = 0 Then
                        obj.Code = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"), clsDocType.BonusMaster, "", "")
                        If clsCommon.myLen(obj.Code) <= 0 Then
                            Throw New Exception("Error in Code Generation")
                        End If
                    End If
                End If
                clsCommon.AddColumnsForChange(coll, "BONUS_CODE", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
                Dim qry As String = "SELECT Count(*) FROM TSPL_BONUS_MASTER where BONUS_CODE= '" & obj.Code & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BONUS_MASTER", OMInsertOrUpdate.Insert, "")
                Else
                    Throw New Exception("This Code Is Already Exist")

                End If

            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BONUS_MASTER", OMInsertOrUpdate.Update, "BONUS_CODE='" + obj.Code + "'")
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function CheckNewEntry(ByVal Code As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = "select BONUS_CODE from TSPL_BONUS_MASTER where BONUS_CODE ='" + Code + "'   "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If

    End Function

End Class
