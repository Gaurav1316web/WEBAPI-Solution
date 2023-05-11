Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsBranchMaster


#Region "Variables"
    Public Code As String
    Public Name As String
    Public BRANCH_ADDRESS As String
    Public COUNTRY_CODE As String
    Public STATE_CODE As String
    Public CITY_CODE As String
    Public PHONE_NO As String
    Public FAX_NO As String
    Public EMAIL_ID As String
    Public RESPONSIBLE_PERSION_NAME As String
    Public COUNTRY_NAME As String
    Public STATE_NAME As String
    Public CITY_NAME As String
#End Region

    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_BRANCH_MASTER.BRANCH_CODE as [Code] ,TSPL_BRANCH_MASTER.BRANCH_NAME as [Branch Name] ,TSPL_BRANCH_MASTER.BRANCH_ADDRESS as [Branch Address] ,TSPL_BRANCH_MASTER.COUNTRY_CODE as [Country Code] ,TSPL_BRANCH_MASTER.STATE_CODE as [State Code] ,TSPL_BRANCH_MASTER.CITY_CODE as [City Code] ,TSPL_BRANCH_MASTER.PHONE_NO as [Phone No] ,TSPL_BRANCH_MASTER.FAX_NO as [Fax No] ,TSPL_BRANCH_MASTER.EMAIL_ID as [Email Id] ,TSPL_BRANCH_MASTER.RESPONSIBLE_PERSION_NAME as [Responsible Person Name] ,TSPL_BRANCH_MASTER.Created_By as [Created By] ,TSPL_BRANCH_MASTER.Created_Date as [Created Date] ,TSPL_BRANCH_MASTER.Modified_By as [Modified By] ,TSPL_BRANCH_MASTER.Modified_Date as [Modified Date]  From TSPL_BRANCH_MASTER   "
        str = clsCommon.ShowSelectForm("BRNCHMST", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsBranchMaster
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
            qry = "delete from TSPL_BRANCH_MASTER where BRANCH_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)


        Catch ex As Exception

            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsBranchMaster
        Dim obj As clsBranchMaster = Nothing
        Dim qry As String = ""

        qry += " select TSPL_BRANCH_MASTER.*, TSPL_COUNTRY_MASTER.COUNTRY_NAME, TSPL_STATE_MASTER.STATE_NAME, TSPL_CITY_MASTER.CITY_NAME "
        qry += " from TSPL_BRANCH_MASTER "
        qry += " Left outer join TSPL_COUNTRY_MASTER on TSPL_COUNTRY_MASTER.COUNTRY_CODE =TSPL_BRANCH_MASTER.COUNTRY_CODE "
        qry += " Left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE =TSPL_BRANCH_MASTER.STATE_CODE "
        qry += " Left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.CITY_CODE =TSPL_BRANCH_MASTER.CITY_CODE "
        qry += " where 2=2 "

        Select Case NavType
            Case NavigatorType.First
                qry += " and BRANCH_CODE = (select MIN(BRANCH_CODE) from TSPL_BRANCH_MASTER)"
            Case NavigatorType.Last
                qry += " and BRANCH_CODE = (select Max(BRANCH_CODE) from TSPL_BRANCH_MASTER)"
            Case NavigatorType.Next
                qry += " and BRANCH_CODE = (select Min(BRANCH_CODE) from TSPL_BRANCH_MASTER where  BRANCH_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and BRANCH_CODE = (select Max(BRANCH_CODE) from TSPL_BRANCH_MASTER where BRANCH_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and BRANCH_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsBranchMaster()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("BRANCH_CODE"))
            obj.Name = clsCommon.myCstr(dt.Rows(0)("BRANCH_NAME"))
            obj.BRANCH_ADDRESS = clsCommon.myCstr(dt.Rows(0)("BRANCH_ADDRESS"))
            obj.COUNTRY_CODE = clsCommon.myCstr(dt.Rows(0)("COUNTRY_CODE"))
            obj.STATE_CODE = clsCommon.myCstr(dt.Rows(0)("STATE_CODE"))
            obj.CITY_CODE = clsCommon.myCstr(dt.Rows(0)("CITY_CODE"))
            obj.PHONE_NO = clsCommon.myCstr(dt.Rows(0)("PHONE_NO"))
            obj.FAX_NO = clsCommon.myCstr(dt.Rows(0)("FAX_NO"))
            obj.EMAIL_ID = clsCommon.myCstr(dt.Rows(0)("EMAIL_ID"))
            obj.RESPONSIBLE_PERSION_NAME = clsCommon.myCstr(dt.Rows(0)("RESPONSIBLE_PERSION_NAME"))
            obj.COUNTRY_NAME = clsCommon.myCstr(dt.Rows(0)("COUNTRY_NAME"))
            obj.STATE_NAME = clsCommon.myCstr(dt.Rows(0)("STATE_NAME"))
            obj.CITY_NAME = clsCommon.myCstr(dt.Rows(0)("CITY_NAME"))

        End If
        Return obj


    End Function
    Public Function SaveData(ByVal obj As clsBranchMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "BRANCH_NAME", obj.Name)
            clsCommon.AddColumnsForChange(coll, "BRANCH_ADDRESS", obj.BRANCH_ADDRESS)
            clsCommon.AddColumnsForChange(coll, "COUNTRY_CODE", obj.COUNTRY_CODE)
            clsCommon.AddColumnsForChange(coll, "STATE_CODE", obj.STATE_CODE)
            clsCommon.AddColumnsForChange(coll, "CITY_CODE", obj.CITY_CODE)
            clsCommon.AddColumnsForChange(coll, "PHONE_NO", obj.PHONE_NO)
            clsCommon.AddColumnsForChange(coll, "FAX_NO", obj.FAX_NO)
            clsCommon.AddColumnsForChange(coll, "EMAIL_ID", obj.EMAIL_ID)
            clsCommon.AddColumnsForChange(coll, "RESPONSIBLE_PERSION_NAME", obj.RESPONSIBLE_PERSION_NAME)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))

            If isNewEntry Then
                If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False)) Then
                    Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_BRANCH_MASTER where BRANCH_CODE='" & obj.Code & "'")
                    If ChkNewEntry = 0 Then
                        obj.Code = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"), clsDocType.BranchMaster, "", "")
                        If clsCommon.myLen(obj.Code) <= 0 Then
                            Throw New Exception("Error in Code Generation")
                        End If
                    End If
                End If
                clsCommon.AddColumnsForChange(coll, "BRANCH_CODE", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
                Dim qry As String = "SELECT Count(*) FROM TSPL_BRANCH_MASTER where BRANCH_CODE= '" & obj.Code & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BRANCH_MASTER", OMInsertOrUpdate.Insert, "")
                Else
                    Throw New Exception("This Code Is Already Exist")
                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BRANCH_MASTER", OMInsertOrUpdate.Update, "BRANCH_CODE='" + obj.Code + "'")
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetCodeByName(ByVal strName As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = " Select BRANCH_CODE from TSPL_BRANCH_MASTER where BRANCH_NAME = '" + strName + "' "
        Dim StrCode As String = clsDBFuncationality.getSingleValue(qry, trans)
        Return StrCode
    End Function

End Class
