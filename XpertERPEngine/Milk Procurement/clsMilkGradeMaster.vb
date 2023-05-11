Imports common
Imports System.Data.SqlClient

Public Class clsMilkGradeMaster
#Region "veriables"
    Public MILK_GRADE_CODE As String = Nothing
    Public DESCRIPTION As String = Nothing
    Public GRADE_TYPE As String = Nothing
    Public SequenceNo As Integer = 0
    Public CompCode As String = Nothing
    Public CompDesc As String = Nothing
    Public MILK_TYPE_CODE As String = String.Empty
    Public MILK_TYPE_Description As String = String.Empty
    Public MILK_TYPE_Type As String = String.Empty
    Public Arr As List(Of clsMilkGradeDetail) = Nothing
#End Region
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_MILK_GRADE_MASTER.MILK_GRADE_CODE as [Code] ,TSPL_MILK_GRADE_MASTER.DESCRIPTION as [Description] ,TSPL_MILK_GRADE_MASTER.GRADE_TYPE as [GRADE TYPE] ,TSPL_MILK_GRADE_MASTER.MILK_TYPE_CODE AS [MILK TYPE CODE],TSPL_MILK_TYPE_MASTER.MILK_TYPE AS [MILK TYPE],TSPL_MILK_GRADE_MASTER.SequenceNo,TSPL_MILK_GRADE_MASTER.Created_By as [Created By] ,TSPL_MILK_GRADE_MASTER.Created_Date as [Created Date] ,TSPL_MILK_GRADE_MASTER.Modified_By as [Modified By] ,TSPL_MILK_GRADE_MASTER.Modified_Date as [Modified Date]  From TSPL_MILK_GRADE_MASTER LEFT OUTER JOIN TSPL_MILK_TYPE_MASTER ON TSPL_MILK_TYPE_MASTER.MILK_TYPE_CODE=TSPL_MILK_GRADE_MASTER.MILK_TYPE_CODE "
        str = clsCommon.ShowSelectForm("MilkGradeMST", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    Public Shared Function getMilkGradeName(ByVal strCode As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim strDesc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_MILK_GRADE_MASTER.DESCRIPTION from TSPL_MILK_GRADE_MASTER where MILK_GRADE_CODE='" & strCode & "'", trans))
        Return strDesc
    End Function
    Public Shared Function getMilkGradeType(ByVal strCode As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim strDesc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_MILK_GRADE_MASTER.GRADE_TYPE from TSPL_MILK_GRADE_MASTER where MILK_GRADE_CODE='" & strCode & "'", trans))
        Return strDesc
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
    '' This Code For Retriving Data from TSPL_MILK_GRADE_MASTER


    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsMilkGradeMaster
        Dim obj As clsMilkGradeMaster = Nothing
        Dim qst As String = "select TSPL_MILK_GRADE_MASTER.SequenceNo,TSPL_MILK_GRADE_MASTER.MILK_TYPE_CODE AS [MILK_TYPE_CODE],TSPL_MILK_TYPE_MASTER.MILK_TYPE AS [MILK_TYPE_TYPE],TSPL_MILK_TYPE_MASTER.DESCRIPTION AS [MILKTYPE_DESCRIPTION], TSPL_MILK_GRADE_MASTER.MILK_GRADE_CODE , TSPL_MILK_GRADE_MASTER.description , TSPL_MILK_GRADE_MASTER.GRADE_TYPE  from  TSPL_MILK_GRADE_MASTER LEFT OUTER JOIN TSPL_MILK_TYPE_MASTER ON TSPL_MILK_TYPE_MASTER.MILK_TYPE_CODE=TSPL_MILK_GRADE_MASTER.MILK_TYPE_CODE where 2=2 "
        Select Case NavType
            Case NavigatorType.Current
                qst += " and  MILK_GRADE_CODE ='" & strCode & "' "
            Case NavigatorType.Next
                qst += " and  MILK_GRADE_CODE in (select min(t.MILK_GRADE_CODE ) from TSPL_MILK_GRADE_MASTER  as t where t.MILK_GRADE_CODE  >'" + strCode + "')"
            Case NavigatorType.First
                qst += " and  MILK_GRADE_CODE in (select min(t.MILK_GRADE_CODE ) from TSPL_MILK_GRADE_MASTER  as t )"
            Case NavigatorType.Last
                qst += " and  MILK_GRADE_CODE in (select max(t.MILK_GRADE_CODE ) from TSPL_MILK_GRADE_MASTER  as t )"
            Case NavigatorType.Previous
                qst += " and  MILK_GRADE_CODE in (select max(t.MILK_GRADE_CODE ) from TSPL_MILK_GRADE_MASTER  as t where t.MILK_GRADE_CODE  <'" + strCode + "')"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsMilkGradeMaster
            obj.SequenceNo = clsCommon.myCdbl(dt.Rows(0)("SequenceNo"))
            obj.MILK_GRADE_CODE = clsCommon.myCstr(dt.Rows(0)("MILK_GRADE_CODE"))
            obj.DESCRIPTION = clsCommon.myCstr(dt.Rows(0)("description"))
            obj.GRADE_TYPE = clsCommon.myCstr(dt.Rows(0)("GRADE_TYPE"))
            obj.MILK_TYPE_CODE = clsCommon.myCstr(dt.Rows(0)("MILK_TYPE_CODE"))
            obj.MILK_TYPE_Description = clsCommon.myCstr(dt.Rows(0)("MILKTYPE_DESCRIPTION"))
            obj.MILK_TYPE_Type = clsCommon.myCstr(dt.Rows(0)("MILK_TYPE_TYPE"))

            qst = "select 1 as ParameterStatus,Parameter_Code,Lower_Range,Upper_Range,status,value1  from TSPL_MILK_GRADE_Detail where  TSPL_MILK_GRADE_Detail.MILK_GRADE_CODE='" & obj.MILK_GRADE_CODE & "'" & _
                "union all select 0 as ParameterStatus,Code,0 as Lower_Range,0 as Upper_Range,'' as status,'' as value1  from TSPL_PARAMETER_MASTER  where IsForMilkGrade=1 and code not in (Select Parameter_Code from  TSPL_MILK_GRADE_Detail where TSPL_MILK_GRADE_Detail.MILK_GRADE_CODE='" & obj.MILK_GRADE_CODE & "')  "
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qst)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsMilkGradeDetail)
                Dim objTr As clsMilkGradeDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsMilkGradeDetail
                    objTr.ParameterStatus = clsCommon.myCdbl(clsCommon.myCstr(dr("ParameterStatus")))
                    objTr.Parameter_Code = clsCommon.myCstr(clsCommon.myCstr(dr("Parameter_Code")))
                    objTr.Lower_Range = clsCommon.myCdbl(clsCommon.myCstr(dr("Lower_Range")))
                    objTr.Upper_Range = clsCommon.myCdbl(clsCommon.myCstr(dr("Upper_Range")))
                    objTr.status = clsCommon.myCstr(clsCommon.myCstr(dr("status")))
                    objTr.value1 = clsCommon.myCstr(clsCommon.myCstr(dr("value1")))
                    obj.Arr.Add(objTr)
                Next
            End If
        End If
        Return obj
    End Function
    '' For Delete of Data in TSPL_MILK_GRADE_MASTER
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim qry As String = "Delete from TSPL_MILK_GRADE_MASTER where MILK_GRADE_CODE='" + strCode + "'"
        Return clsDBFuncationality.ExecuteNonQuery(qry)
    End Function
    '' For Save Data in TSPL_MILK_GRADE_MASTER
    Public Function SaveData(ByVal obj As clsMilkGradeMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim qry As String = ""
        Try
            If isNewEntry Then
                If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, trans)) = "1", True, False)) Then
                    Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_MILK_GRADE_MASTER where MILK_GRADE_CODE='" & obj.MILK_GRADE_CODE & "'", trans)
                    If ChkNewEntry = 0 Then
                        obj.MILK_GRADE_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"), clsDocType.MilkGradeMaster, "", "")
                        If clsCommon.myLen(obj.MILK_GRADE_CODE) <= 0 Then
                            Throw New Exception("Error in Code Generation")
                        End If
                    End If
                End If
            Else
                qry = "delete from TSPL_MILK_GRADE_Detail where MILK_GRADE_CODE='" + obj.MILK_GRADE_CODE + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Description", obj.DESCRIPTION)
            clsCommon.AddColumnsForChange(coll, "GRADE_TYPE", obj.GRADE_TYPE)
            clsCommon.AddColumnsForChange(coll, "SequenceNo", obj.SequenceNo)
            clsCommon.AddColumnsForChange(coll, "MILK_TYPE_CODE", obj.MILK_TYPE_CODE)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "MILK_GRADE_CODE", obj.MILK_GRADE_CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

                qry = "SELECT Count(*) FROM TSPL_MILK_GRADE_MASTER where MILK_GRADE_CODE = '" & obj.MILK_GRADE_CODE & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_GRADE_MASTER", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This Code Is Already Exist")
                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_GRADE_MASTER ", OMInsertOrUpdate.Update, " MILK_GRADE_CODE='" + obj.MILK_GRADE_CODE + "'", trans)
            End If
            isSaved = isSaved And clsMilkGradeDetail.SaveData(obj.MILK_GRADE_CODE, obj.Arr, trans)
            If isSaved Then
                trans.Commit()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function CheckNewEntry(ByVal Code As String, ByVal MilkGradeType As String, ByVal MILK_TYPE_CODE As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = "select MILK_GRADE_CODE from TSPL_MILK_GRADE_MASTER where MILK_GRADE_CODE ='" + Code + "' and GRADE_TYPE='" & MilkGradeType & "' and MILK_TYPE_CODE='" & MILK_TYPE_CODE & "' "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If
    End Function

End Class
Public Class clsMilkGradeDetail
    Public MILK_GRADE_CODE As String = Nothing
    Public Line_No As Integer = Nothing
    Public Parameter_Code As String = Nothing
    Public Upper_Range As Double = 0
    Public Lower_Range As Double = 0
    Public ParameterStatus As Integer = 0
    Public status As String = Nothing
    Public value1 As String = Nothing
    Public Shared Function SaveData(ByVal strCode As String, ByVal arr As List(Of clsMilkGradeDetail), ByVal trans As SqlTransaction) As Boolean
        Dim issaved As Boolean = True
        If (arr IsNot Nothing AndAlso arr.Count > 0) Then
            For Each obj As clsMilkGradeDetail In arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "MILK_GRADE_CODE", strCode)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Parameter_Code", obj.Parameter_Code, True)
                clsCommon.AddColumnsForChange(coll, "Upper_Range", obj.Upper_Range)
                clsCommon.AddColumnsForChange(coll, "Lower_Range", obj.Lower_Range)
                clsCommon.AddColumnsForChange(coll, "status", obj.status)
                clsCommon.AddColumnsForChange(coll, "value1", obj.value1)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_GRADE_Detail", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return issaved
    End Function
End Class
