Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class ClsHRAExemptionRule
#Region "Variables"
    Public HRA_Code As String
    Public Particulars As String
    Public Location_City As String
    Public Formula As String
    Public SALARY_STRUCTURE_CODE As String

    Public LINE_NO As String
    Public PAY_HEAD_CODE As String
    Public PAY_HEAD_NAME As String
    Public HEAD_TYPE As String
    Public SUB_HEAD_TYPE As String
    Public CALC_BASIS As String
    Public PAYHEAD_FORMULA As String
    Public Shared ObjList As List(Of ClsHRAExemptionRule)
    Public Shared objtr As List(Of ClsHRAExemptionRule)
#End Region
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsHRAExemptionRule
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Try
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            isSaved = False
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = "delete from TSPL_HRA_EXEMPTION where HRA_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If isSaved Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsHRAExemptionRule
        '
        Dim obj As ClsHRAExemptionRule = Nothing


        Dim qry As String = "select * From TSPL_HRA_EXEMPTION where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and HRA_CODE = (select MIN(HRA_CODE) from TSPL_HRA_EXEMPTION)"
            Case NavigatorType.Last
                qry += " and HRA_CODE = (select Max(HRA_CODE) from TSPL_HRA_EXEMPTION)"
            Case NavigatorType.Next
                qry += " and HRA_CODE = (select Min(HRA_CODE) from TSPL_HRA_EXEMPTION where  HRA_CODE >'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and HRA_CODE = (select Max(HRA_CODE) from TSPL_HRA_EXEMPTION where HRA_CODE <'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and HRA_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsHRAExemptionRule()
            obj.HRA_Code = clsCommon.myCstr(dt.Rows(0)("HRA_CODE"))
            obj.Particulars = clsCommon.myCstr(dt.Rows(0)("Particulars"))
            obj.Location_City = clsCommon.myCstr(dt.Rows(0)("Location_City"))
            obj.Formula = clsCommon.myCstr(dt.Rows(0)("HRA_FORMULA"))
            obj.SALARY_STRUCTURE_CODE = clsCommon.myCstr(dt.Rows(0)("SALARY_STRUCTURE_CODE"))
            ClsHRAExemptionRule.ObjList = GetPayHeadData(obj.SALARY_STRUCTURE_CODE)
        End If


        Return obj

    End Function
    Public Shared Function GetPayHeadData(ByVal strCode As String) As List(Of ClsHRAExemptionRule)
        Dim qry As String = ""
        Dim Line As Integer = 0
        Dim dt As DataTable
        Dim objtr As ClsHRAExemptionRule = Nothing
        qry = "select TSPL_SALSTRUCT_PAYHEADS.*,TSPL_PAYHEAD_MASTER.PAY_HEAD_NAME from TSPL_SALSTRUCT_PAYHEADS left outer join TSPL_PAYHEAD_MASTER on TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE =TSPL_SALSTRUCT_PAYHEADS.PAY_HEAD_CODE where 2=2"
        qry += " and TSPL_SALSTRUCT_PAYHEADS.SALARY_STRUCTURE_CODE = '" + strCode + "'"
        dt = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry)
        Dim Arr As New List(Of ClsHRAExemptionRule)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New ClsHRAExemptionRule()
                Line = Line + 1
                'objtr.LINE_NO = Convert.ToInt16(clsCommon.myCdbl(dr("LINE_NO")))
                objtr.LINE_NO = Line
                objtr.PAY_HEAD_CODE = clsCommon.myCstr(dr("PAY_HEAD_CODE"))
                objtr.PAY_HEAD_NAME = clsCommon.myCstr(dr("PAY_HEAD_NAME"))
                objtr.HEAD_TYPE = clsCommon.myCstr(dr("HEAD_TYPE"))
                objtr.SUB_HEAD_TYPE = clsCommon.myCstr(dr("SUB_HEAD_TYPE"))
                objtr.CALC_BASIS = clsCommon.myCstr(dr("CALC_BASIS"))
                objtr.PAYHEAD_FORMULA = clsCommon.myCstr(dr("PAYHEAD_FORMULA"))
                Arr.Add(objtr)
            Next
        End If
        Return Arr
    End Function
    Public Function SaveData(ByVal obj As ClsHRAExemptionRule, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "SALARY_STRUCTURE_CODE", obj.SALARY_STRUCTURE_CODE)
            clsCommon.AddColumnsForChange(coll, "Particulars", obj.Particulars)
            clsCommon.AddColumnsForChange(coll, "Location_City", obj.Location_City)
            clsCommon.AddColumnsForChange(coll, "HRA_FORMULA", obj.Formula)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "HRA_CODE", obj.HRA_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                Dim qry As String = "SELECT Count(*) FROM TSPL_HRA_EXEMPTION where HRA_CODE= '" & obj.HRA_Code & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HRA_EXEMPTION", OMInsertOrUpdate.Insert, "", trans)
                Else
                    common.clsCommon.MyMessageBoxShow("This Code Is Already Exist")
                    Return False
                End If

            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HRA_EXEMPTION", OMInsertOrUpdate.Update, "HRA_CODE='" + obj.HRA_Code + "'", trans)
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function CheckNewEntry(ByVal Code As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select HRA_CODE from TSPL_HRA_EXEMPTION where HRA_CODE ='" + Code + "'   "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If
    End Function

End Class
