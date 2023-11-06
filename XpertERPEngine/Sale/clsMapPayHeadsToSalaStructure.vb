Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsMapPayHeadsToSalaStructure

#Region "Variables"
    Public SALARY_STRUCTURE_CODE As String
    Public SALARY_STRUCTURE_NAME As String
    Public LINE_NO As Int16
    Public PAY_HEAD_CODE As String
    Public PAY_HEAD_NAME As String
    Public VALID_FROM As DateTime? = Nothing
    Public VALID_TO As DateTime? = Nothing
    Public HEAD_TYPE As String
    Public SUB_HEAD_TYPE As String
    Public CALC_BASIS As String
    Public PAYHEAD_FORMULA As String
    Public RATE_AMOUNT As Double
    Public DESCRIPTION As String
    Public Shared ObjList As List(Of clsMapPayHeadsToSalaStructure)
    Public IsHiddenComponent As Boolean
    Public Location_Code As String

#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsMapPayHeadsToSalaStructure
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
            qry = "delete from TSPL_SALSTRUCT_PAYHEADS where SALARY_STRUCTURE_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception

            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsMapPayHeadsToSalaStructure
        Dim obj As New clsMapPayHeadsToSalaStructure()
        Dim objtr As New clsMapPayHeadsToSalaStructure()
        Dim whrQry As String = Nothing
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrQry = " and Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        ObjList = New List(Of clsMapPayHeadsToSalaStructure)

        Dim qry As String = "select SALARY_STRUCTURE_CODE,SALARY_STRUCTURE_NAME,Location_Code from TSPL_SALARY_STRUCTURE where 2=2 " + whrQry
        Select Case NavType
            Case NavigatorType.First
                qry += " and SALARY_STRUCTURE_CODE = (select MIN(SALARY_STRUCTURE_CODE) from TSPL_SALARY_STRUCTURE)"
            Case NavigatorType.Last
                qry += " and SALARY_STRUCTURE_CODE = (select Max(SALARY_STRUCTURE_CODE) from TSPL_SALARY_STRUCTURE)"
            Case NavigatorType.Next
                qry += " and SALARY_STRUCTURE_CODE = (select Min(SALARY_STRUCTURE_CODE) from TSPL_SALARY_STRUCTURE where  SALARY_STRUCTURE_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and SALARY_STRUCTURE_CODE = (select Max(SALARY_STRUCTURE_CODE) from TSPL_SALARY_STRUCTURE where SALARY_STRUCTURE_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and SALARY_STRUCTURE_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj.SALARY_STRUCTURE_NAME = dt.Rows(0)("SALARY_STRUCTURE_NAME")
            obj.SALARY_STRUCTURE_CODE = dt.Rows(0)("SALARY_STRUCTURE_CODE")
            strCode = dt.Rows(0)("SALARY_STRUCTURE_CODE")
            obj.Location_Code = dt.Rows(0)("Location_Code")
        End If
        'Dim whrQry1 As String = Nothing
        'If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 And  Then
        '    whrQry1 = " and TSPL_SALSTRUCT_PAYHEADS.Location_Code='" + objCommonVar.strCurrUserLocations + "'"
        'End If

        qry = "select TSPL_SALSTRUCT_PAYHEADS.*,TSPL_PAYHEAD_MASTER.PAY_HEAD_NAME from TSPL_SALSTRUCT_PAYHEADS left outer join TSPL_PAYHEAD_MASTER on TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE =TSPL_SALSTRUCT_PAYHEADS.PAY_HEAD_CODE where 2=2"
        qry += " and TSPL_SALSTRUCT_PAYHEADS.SALARY_STRUCTURE_CODE = '" + strCode + "'" + whrQry + " order by TSPL_SALSTRUCT_PAYHEADS.SALARY_STRUCTURE_CODE,TSPL_SALSTRUCT_PAYHEADS.LINE_NO"
        dt = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsMapPayHeadsToSalaStructure()
                objtr.SALARY_STRUCTURE_CODE = clsCommon.myCstr(dr("SALARY_STRUCTURE_CODE"))
                objtr.LINE_NO = Convert.ToInt16(clsCommon.myCdbl(dr("LINE_NO")))
                objtr.PAY_HEAD_CODE = clsCommon.myCstr(dr("PAY_HEAD_CODE"))
                objtr.PAY_HEAD_NAME = clsCommon.myCstr(dr("PAY_HEAD_NAME"))
                objtr.HEAD_TYPE = clsCommon.myCstr(dr("HEAD_TYPE"))
                objtr.SUB_HEAD_TYPE = clsCommon.myCstr(dr("SUB_HEAD_TYPE"))
                objtr.CALC_BASIS = clsCommon.myCstr(dr("CALC_BASIS"))
                objtr.RATE_AMOUNT = clsCommon.myCdbl(dr("RATE_AMOUNT"))
                objtr.PAYHEAD_FORMULA = clsCommon.myCstr(dr("PAYHEAD_FORMULA"))
                objtr.DESCRIPTION = clsCommon.myCstr(dr("DESCRIPTION"))
                objtr.IsHiddenComponent = clsCommon.myCBool(dr("IsHiddenComponent"))
                If clsCommon.myLen(dt.Rows(0)("VALID_FROM")) > 0 AndAlso clsCommon.myCstr(dt.Rows(0)("VALID_FROM")) <> "01/01/1900 12:00:00 AM" Then
                    objtr.VALID_FROM = clsCommon.GetPrintDate(dt.Rows(0)("VALID_FROM"), "dd/MMM/yyyy")
                Else
                    objtr.VALID_FROM = Nothing
                End If
                If clsCommon.myLen(dt.Rows(0)("VALID_TO")) > 0 AndAlso clsCommon.myCstr(dt.Rows(0)("VALID_TO")) <> "01/01/1900 12:00:00 AM" Then
                    objtr.VALID_TO = clsCommon.GetPrintDate(dt.Rows(0)("VALID_TO"), "dd/MMM/yyyy")
                Else
                    objtr.VALID_TO = Nothing
                End If
                objtr.Location_Code = clsCommon.myCstr(dr("Location_Code"))
                ObjList.Add(objtr)
            Next
        End If
        clsMapPayHeadsToSalaStructure.ObjList = ObjList
        Return obj
    End Function

    Public Function SaveData(ByVal strCode As String, ByVal objList As List(Of clsMapPayHeadsToSalaStructure)) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
            Dim qry As String

            isSaved = isSaved AndAlso clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_SALSTRUCT_PAYHEADS", "SALARY_STRUCTURE_CODE", trans)
            ''  delete existing data
            qry = "delete from TSPL_SALSTRUCT_PAYHEADS where SALARY_STRUCTURE_CODE = '" & strCode & "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As Hashtable
            For Each Obj As clsMapPayHeadsToSalaStructure In objList

                coll = New Hashtable()
                clsCommon.AddColumnsForChange(coll, "SALARY_STRUCTURE_CODE", Obj.SALARY_STRUCTURE_CODE)
                clsCommon.AddColumnsForChange(coll, "LINE_NO", Obj.LINE_NO)
                clsCommon.AddColumnsForChange(coll, "PAY_HEAD_CODE", Obj.PAY_HEAD_CODE)
                clsCommon.AddColumnsForChange(coll, "HEAD_TYPE", Obj.HEAD_TYPE)
                clsCommon.AddColumnsForChange(coll, "SUB_HEAD_TYPE", Obj.SUB_HEAD_TYPE)
                clsCommon.AddColumnsForChange(coll, "CALC_BASIS", Obj.CALC_BASIS)
                clsCommon.AddColumnsForChange(coll, "PAYHEAD_FORMULA", Obj.PAYHEAD_FORMULA)
                clsCommon.AddColumnsForChange(coll, "RATE_AMOUNT", Obj.RATE_AMOUNT)
                clsCommon.AddColumnsForChange(coll, "DESCRIPTION", Obj.DESCRIPTION)
                If Obj.VALID_FROM IsNot Nothing Then
                    clsCommon.AddColumnsForChange(coll, "VALID_FROM", clsCommon.GetPrintDate(Obj.VALID_FROM, "dd/MMM/yyyy"))
                Else
                    clsCommon.AddColumnsForChange(coll, "VALID_FROM", Nothing)
                End If
                If Obj.VALID_TO IsNot Nothing Then
                    clsCommon.AddColumnsForChange(coll, "VALID_TO", clsCommon.GetPrintDate(Obj.VALID_TO, "dd/MMM/yyyy"))
                Else
                    clsCommon.AddColumnsForChange(coll, "VALID_TO", Nothing)
                End If
                clsCommon.AddColumnsForChange(coll, "IsHiddenComponent", Obj.IsHiddenComponent)

                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Location_Code", Obj.Location_Code)
                qry = "SELECT Count(*) FROM TSPL_SALSTRUCT_PAYHEADS where SALARY_STRUCTURE_CODE = '" & Obj.SALARY_STRUCTURE_CODE & "' and PAY_HEAD_CODE = '" & Obj.PAY_HEAD_CODE & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                If check = 0 Then
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SALSTRUCT_PAYHEADS", OMInsertOrUpdate.Insert, "", trans)
                Else
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SALSTRUCT_PAYHEADS", OMInsertOrUpdate.Update, "SALARY_STRUCTURE_CODE='" + Obj.SALARY_STRUCTURE_CODE + "' and PAY_HEAD_CODE = '" + Obj.PAY_HEAD_CODE + "'", trans)
                End If
            Next
            '' update all employee salary sequence 
            qry = " UPDATE TSPL_EMPLOYEE_SALARY_PAYHEADS SET TSPL_EMPLOYEE_SALARY_PAYHEADS.LINE_NO=TSPL_SALSTRUCT_PAYHEADS.LINE_NO FROM  " &
                " TSPL_SALSTRUCT_PAYHEADS INNER JOIN TSPL_EMPLOYEE_SALARY " &
                " ON TSPL_SALSTRUCT_PAYHEADS.SALARY_STRUCTURE_CODE=TSPL_EMPLOYEE_SALARY.SALARY_STRUCTURE_CODE  " &
                " WHERE(TSPL_EMPLOYEE_SALARY.SALARY_STRUCTURE_CODE = TSPL_SALSTRUCT_PAYHEADS.SALARY_STRUCTURE_CODE)  " &
                " AND TSPL_SALSTRUCT_PAYHEADS.PAY_HEAD_CODE=TSPL_EMPLOYEE_SALARY_PAYHEADS.PAY_HEAD_CODE AND TSPL_EMPLOYEE_SALARY.SALARY_STRUCTURE_CODE='" & strCode & "' "

            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetFormula(ByVal strSalaryStructurCode As String, ByVal strPayHeadCode As String) As String
        Dim qry As String = "select PAYHEAD_FORMULA from TSPL_SALSTRUCT_PAYHEADS where SALARY_STRUCTURE_CODE='" + strSalaryStructurCode + "' and PAY_HEAD_CODE= '" + strPayHeadCode + "' "
        Dim StrFormula As String = clsDBFuncationality.getSingleValue(qry)
        Return StrFormula
    End Function

End Class
