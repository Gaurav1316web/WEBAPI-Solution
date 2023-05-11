Imports common
Imports System.Data.SqlClient
Public Class clsTDMWiseTarget
#Region "Variables"

 
    Public TDMID As String = Nothing
    Public Employee_Code As String = Nothing
    Public Employee_desc As String = Nothing
    Public Flavour As String = Nothing
    Public Flavour_desc As String = Nothing
    Public Qty As Double = Nothing
    Public TargetQty As Double = Nothing
    Public FlavourWise As String = Nothing
    Public TargetDate As DateTime? = Nothing

#End Region

    Public Function SaveData(ByVal Emp_Code As String, ByVal Emp_desc As String, ByVal TargetQty As String, ByVal FlavourYSEorNO As String, ByVal Tdate As Date, ByVal Arr As List(Of clsTDMWiseTarget)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Dim isSaved As Boolean = True
        Try

            Dim qry As String = "delete from TSPL_TDMWISE_TARGET_DETAIL where Employee_Code='" + Employee_Code + "' and Target_Date = '" + clsCommon.GetPrintDate(Tdate, "dd/MMM/yyyy") + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

          
            qry = "select  MAX(TDMID)  from TSPL_TDMWISE_TARGET_DETAIL"
            TDMID = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            If clsCommon.myLen(TDMID) <= 0 Then
                TDMID = "TM000000001"
            Else
                TDMID = clsCommon.incval(TDMID)
            End If


            For Each obj As clsTDMWiseTarget In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Employee_Code", Emp_Code)
                clsCommon.AddColumnsForChange(coll, "Employee_desc", Emp_desc)
                clsCommon.AddColumnsForChange(coll, "TargetQty", TargetQty)
                clsCommon.AddColumnsForChange(coll, "Flavourwise", FlavourYSEorNO)
                clsCommon.AddColumnsForChange(coll, "Target_Date", clsCommon.GetPrintDate(Tdate, "dd/MMM/yyyy "))
                clsCommon.AddColumnsForChange(coll, "TDMID", TDMID)
                clsCommon.AddColumnsForChange(coll, "Flavour", obj.Flavour)
                clsCommon.AddColumnsForChange(coll, "Flavour_desc", obj.Flavour_desc)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)




                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TDMWISE_TARGET_DETAIL", OMInsertOrUpdate.Insert, "", trans)

            Next
            If (Arr.Count = 0) Then
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Employee_Code", Emp_Code)
                clsCommon.AddColumnsForChange(coll, "Employee_desc", Emp_desc)
                clsCommon.AddColumnsForChange(coll, "TargetQty", TargetQty)
                clsCommon.AddColumnsForChange(coll, "Flavourwise", FlavourYSEorNO)
                clsCommon.AddColumnsForChange(coll, "Target_Date", clsCommon.GetPrintDate(Tdate, "dd/MMM/yyyy "))
                clsCommon.AddColumnsForChange(coll, "TDMID", TDMID)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TDMWISE_TARGET_DETAIL", OMInsertOrUpdate.Insert, "", trans)

            End If

            If isSaved Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function


    Public Shared Function GetData(ByVal strCode As String, ByVal Tdate As Date) As List(Of clsTDMWiseTarget)
        Dim obj As clsTDMWiseTarget = Nothing
        Dim qry As String = "select Flavour ,Flavour_desc ,Qty,TargetQty from TSPL_TDMWISE_TARGET_DETAIL WHERE Employee_Code = '" + strCode + "'and Target_Date = '" + clsCommon.GetPrintDate(Tdate, "dd/MMM/yyyy") + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        dt = clsDBFuncationality.GetDataTable(qry)
        Dim Arr As New List(Of clsTDMWiseTarget)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            Dim objTr As clsTDMWiseTarget
            For Each dr As DataRow In dt.Rows
                objTr = New clsTDMWiseTarget()
                objTr.Flavour = clsCommon.myCstr(dr("Flavour"))
                objTr.Flavour_desc = clsCommon.myCstr(dr("Flavour_desc"))
                objTr.Qty = clsCommon.myCdbl(dr("Qty"))
                'objTr.TargetQty = clsCommon.myCstr(dr("TargetQty"))
                Arr.Add(objTr)
            Next
        End If
        Return Arr
    End Function

End Class
