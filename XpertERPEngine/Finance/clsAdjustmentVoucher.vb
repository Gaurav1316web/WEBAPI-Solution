Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsAdjustmentVoucher

#Region "Variables"

    Public ADJUSTMENT_CODE As String
    Public PAY_PERIOD_CODE As String
    Public ADJUSTMENT_BY_Code As String
    Public ADJUSTMENT_BY_Name As String
    Public EMP_CODE As String
    Public EMP_NAME As String
    Public ADJUSTMENT_REMARK As String
    Public POSTED As Boolean
    Public Posting_Date As DateTime
    Public PAY_PERIOD_NAME As String
    Public ADJUSTMENT_DATE As Date

    Public ObjList As List(Of clsAdjustmentVoucherDetail)
    Public Objtr As clsAdjustmentVoucherDetail

#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsAdjustmentVoucher
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = False
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            Dim qry As String
            qry = "delete from TSPL_EMPADJUSTMENT_DETAIL where ADJUSTMENT_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_ADJUSTMENT_VOUCHER where ADJUSTMENT_CODE ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsAdjustmentVoucher
        Dim obj As New clsAdjustmentVoucher()
        Dim qry As String = "SELECT TAV.ADJUSTMENT_CODE,TAV.ADJUSTMENT_DATE,TAV.PAY_PERIOD_CODE,TPM.PAY_PERIOD_NAME,TAV.ADJUSTMENT_BY as ADJUSTMENT_BY_CODE, " _
                            & " EMP1.EMP_CODE AS ADJUSTMENT_BY_NAME ,TAV.POSTED, TAV.Posting_Date," _
                            & " TAV.ADJUSTMENT_REMARK,TAV.EMP_CODE,EMP.Emp_Name  FROM TSPL_ADJUSTMENT_VOUCHER TAV " _
                            & " INNER JOIN TSPL_PAYPERIOD_MASTER TPM ON TAV.PAY_PERIOD_CODE=TPM.PAY_PERIOD_CODE " _
                            & " LEFT JOIN TSPL_EMPLOYEE_MASTER EMP ON TAV.EMP_CODE=EMP.EMP_CODE " _
                            & " LEFT JOIN TSPL_EMPLOYEE_MASTER EMP1 ON TAV.ADJUSTMENT_BY=EMP1.EMP_CODE where 2=2"

        Select Case NavType
            Case NavigatorType.First
                qry += " and ADJUSTMENT_CODE = (select MIN(ADJUSTMENT_CODE) from TSPL_ADJUSTMENT_VOUCHER)"
            Case NavigatorType.Last
                qry += " and ADJUSTMENT_CODE = (select Max(ADJUSTMENT_CODE) from TSPL_ADJUSTMENT_VOUCHER)"
            Case NavigatorType.Next
                qry += " and ADJUSTMENT_CODE = (select Min(ADJUSTMENT_CODE) from TSPL_ADJUSTMENT_VOUCHER where  ADJUSTMENT_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and ADJUSTMENT_CODE = (select Max(ADJUSTMENT_CODE) from TSPL_ADJUSTMENT_VOUCHER where ADJUSTMENT_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and ADJUSTMENT_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            'obj.SALARY_STRUCTURE_NAME = dt.Rows(0)("SALARY_STRUCTURE_NAME")
            obj.ADJUSTMENT_CODE = dt.Rows(0)("ADJUSTMENT_CODE")
            obj.ADJUSTMENT_DATE = dt.Rows(0)("ADJUSTMENT_DATE")
            obj.PAY_PERIOD_CODE = dt.Rows(0)("PAY_PERIOD_CODE")
            obj.ADJUSTMENT_BY_Code = clsCommon.myCstr(dt.Rows(0)("ADJUSTMENT_BY_CODE"))
            obj.ADJUSTMENT_BY_Name = clsCommon.myCstr(dt.Rows(0)("ADJUSTMENT_BY_NAME"))
            obj.ADJUSTMENT_REMARK = clsCommon.myCstr(dt.Rows(0)("ADJUSTMENT_REMARK"))
            obj.PAY_PERIOD_NAME = clsCommon.myCstr(dt.Rows(0)("PAY_PERIOD_NAME"))
            obj.EMP_CODE = clsCommon.myCstr(dt.Rows(0)("EMP_CODE"))
            obj.EMP_NAME = clsCommon.myCstr(dt.Rows(0)("EMP_NAME"))
            obj.POSTED = clsCommon.myCBool(dt.Rows(0)("POSTED"))
            If clsCommon.myLen(dt.Rows(0)("Posting_Date")) > 0 Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            Else
                obj.Posting_Date = Nothing
            End If
            obj.ObjList = clsAdjustmentVoucherDetail.GetData(obj.ADJUSTMENT_CODE, Nothing)
        End If
        Return obj
    End Function

    Public Function SaveData(ByVal obj As clsAdjustmentVoucher, ByVal isNewEntry As Boolean, Optional ByVal strCode As String = "") As Boolean
        Dim isSaved As Boolean = True

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If isNewEntry Then
                If strCode = "" Then
                    obj.ADJUSTMENT_CODE = clsERPFuncationality.GetNextCode(trans, obj.ADJUSTMENT_DATE, clsDocType.SalaryAdjustment, "", "")
                Else
                    obj.ADJUSTMENT_CODE = strCode
                End If
            End If

            Dim qry As String = "delete from TSPL_EMPADJUSTMENT_DETAIL where ADJUSTMENT_CODE='" + obj.ADJUSTMENT_CODE + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""

            If (clsCommon.myLen(obj.ADJUSTMENT_CODE) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            'clsCommon.AddColumnsForChange(coll, "ADJUSTMENT_CODE", obj.ADJUSTMENT_CODE)
            clsCommon.AddColumnsForChange(coll, "PAY_PERIOD_CODE", obj.PAY_PERIOD_CODE, True)
            clsCommon.AddColumnsForChange(coll, "ADJUSTMENT_BY", obj.ADJUSTMENT_BY_Code, True)
            clsCommon.AddColumnsForChange(coll, "ADJUSTMENT_REMARK", obj.ADJUSTMENT_REMARK)
            clsCommon.AddColumnsForChange(coll, "EMP_CODE", obj.EMP_CODE, True)
            clsCommon.AddColumnsForChange(coll, "ADJUSTMENT_DATE", clsCommon.GetPrintDate(obj.ADJUSTMENT_DATE, "dd/MMM/yyyy"))

            clsCommon.AddColumnsForChange(coll, "POSTED", "0")
            'clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            'clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

            If isNewEntry Then

                clsCommon.AddColumnsForChange(coll, "ADJUSTMENT_CODE", obj.ADJUSTMENT_CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ADJUSTMENT_VOUCHER", OMInsertOrUpdate.Insert, "", trans)
            Else

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ADJUSTMENT_VOUCHER", OMInsertOrUpdate.Update, "TSPL_ADJUSTMENT_VOUCHER.ADJUSTMENT_CODE='" + obj.ADJUSTMENT_CODE + "'", trans)
            End If


            isSaved = isSaved AndAlso clsAdjustmentVoucherDetail.SaveData(obj.ADJUSTMENT_CODE, obj.ObjList, trans)
            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(err.Message)
            Return False
        End Try
        Return isSaved
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsAdjustmentVoucher = clsAdjustmentVoucher.GetData(strDocNo, NavigatorType.Current)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.ADJUSTMENT_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.POSTED = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If

            Dim qry As String = "Update TSPL_ADJUSTMENT_VOUCHER set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where ADJUSTMENT_CODE ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetRegisterDT(ByVal strFromPP As String, ByVal strToPP As String) As DataTable
        Dim dt As DataTable
        Try
            Dim qry As String = ""

            qry += " SELECT TT1.ADJUSTMENT_CODE ,TT1.ADJUSTMENT_DATE ,"
            qry += " TT2.ADJUSTMENT_PLUS ,TT2.ADJUSTMENT_MINUS ,TT1.ADJUSTMENT_REMARK,"
            qry += " TT1.PAY_PERIOD_CODE ,TT1.PAY_PERIOD_NAME ,"
            qry += " TT1.EMP_CODE ,TT1.Emp_Name ,TT1.ADJUSTMENT_BY ,"
            qry += " TT1.ADJUSTMENT_BY_NAME ,TT1.POSTED  FROM ("
            qry += " SELECT T1.ADJUSTMENT_CODE,T1.ADJUSTMENT_DATE,T1.PAY_PERIOD_CODE,T2.PAY_PERIOD_NAME,T1.EMP_CODE,T3.Emp_Name,"
            qry += " T1.ADJUSTMENT_BY,T4.Emp_Name AS ADJUSTMENT_BY_NAME,T1.ADJUSTMENT_REMARK,T1.POSTED "
            qry += " FROM TSPL_ADJUSTMENT_VOUCHER T1 LEFT JOIN TSPL_PAYPERIOD_MASTER T2 ON T1.PAY_PERIOD_CODE=T2.PAY_PERIOD_CODE "
            qry += " LEFT JOIN TSPL_EMPLOYEE_MASTER T3 ON T1.EMP_CODE=T3.EMP_CODE "
            qry += " LEFT JOIN TSPL_EMPLOYEE_MASTER T4 ON T1.EMP_CODE=T4.EMP_CODE "
            If clsCommon.myLen(strFromPP) > 0 AndAlso clsCommon.myLen(strToPP) > 0 Then
                qry += " WHERE T2.DATE_FROM BETWEEN "
                qry += " (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" + strFromPP + "') AND "
                qry += " (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" + strFromPP + "')"
            End If
            qry += " ) AS TT1 LEFT JOIN (SELECT ADJUSTMENT_CODE,SUM(ADJUSTMENT_PLUS) AS ADJUSTMENT_PLUS,SUM(ADJUSTMENT_MINUS) AS ADJUSTMENT_MINUS "
            qry += " FROM TSPL_EMPADJUSTMENT_DETAIL GROUP BY ADJUSTMENT_CODE) AS TT2"
            qry += " ON TT1.ADJUSTMENT_CODE=TT2.ADJUSTMENT_CODE ;"


            dt = clsDBFuncationality.GetDataTable(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return dt
    End Function

    Public Shared Function GetRegisterDTDetailed(ByVal strFromPP As String, ByVal strToPP As String, ByVal Location_Code As String, ByVal Division_Code As String) As DataTable
        Dim dt As DataTable
        Try
            Dim qry As String = ""
            qry += " SELECT Ded.ADJUSTMENT_CODE as [Document Code] ,Dedd.PAY_HEAD_CODE as [Pay Head Code] ,PHM.PAY_HEAD_NAME as [Pay Head Name] ," & _
                   " Dedd.EMP_CODE  as [Employee Code],EMP.Emp_Name as [Employee Name],EMP.Location_Code as [Location Code],Loc.Location_Desc as [Location Name] ,EMP.DEVISION_CODE as                     [Division Code],Div.Devision_Name as [Division Name],Dedd.ADJUSTMENT_PLUS as [Adjustment Plus],dedD.ADJUSTMENT_MINUS as [Adjustment Minus]  " & _
                   " FROM TSPL_ADJUSTMENT_VOUCHER Ded INNER JOIN TSPL_EMPADJUSTMENT_DETAIL Dedd ON Ded.ADJUSTMENT_CODE=Dedd.ADJUSTMENT_CODE " & _
                   " LEFT JOIN TSPL_PAYPERIOD_MASTER PP ON Ded.PAY_PERIOD_CODE=PP.PAY_PERIOD_CODE " & _
                   " LEFT JOIN TSPL_EMPLOYEE_MASTER EMP ON Dedd.EMP_CODE=EMP.EMP_CODE " & _
                   " LEFT JOIN TSPL_PAYHEAD_MASTER PHM ON Dedd.PAY_HEAD_CODE=PHM.PAY_HEAD_CODE " & _
                   " left join TSPL_LOCATION_MASTER Loc on EMP.LOCATION_CODE=Loc.Location_Code " & _
                   " left join TSPL_DEVISION_MASTER Div on EMP.DEVISION_CODE=Div.DEVISION_CODE where 2=2 "
            If clsCommon.myLen(strFromPP) > 0 AndAlso clsCommon.myLen(strToPP) > 0 Then
                qry += " and PP.DATE_FROM BETWEEN "
                qry += " (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" + strFromPP + "') AND "
                qry += " (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" + strToPP + "') "
            End If
            If clsCommon.myLen(Location_Code) > 0 Then
                qry += " and EMP.Location_Code='" & Location_Code & "' "
            End If
            If clsCommon.myLen(Division_Code) > 0 Then
                qry += " and EMP.DEVISION_CODE='" & Division_Code & "' "
            End If
            dt = clsDBFuncationality.GetDataTable(qry)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return dt
    End Function
    Public Shared Function GetAdjustTypeDataTable() As DataTable
        Dim DT As DataTable = New DataTable
        DT.Columns.Add("Code", GetType(String))
        DT.Columns.Add("Name", GetType(String))
        Dim DR As DataRow = DT.NewRow()
        DR("Code") = "PA"
        DR("Name") = "Principle Amount"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Code") = "AR"
        DR("Name") = "Arrear"
        DT.Rows.Add(DR)
        DT.AcceptChanges()
        Return DT
    End Function
End Class

Public Class clsAdjustmentVoucherDetail
#Region "Variables"
    Public empCode As String
    Public PayHeadCode As String
    Public PayHeadName As String
    Public ADJUSTMENT_TYPE As String
    Public adjust_plus As Decimal
    Public adjust_minus As Decimal
    'Public Shared ObjList As List(Of clsAdjustmentVoucher)
    'Public Const AttendanceCode As String = "MT"
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal ObjList As List(Of clsAdjustmentVoucherDetail), ByVal trans As SqlTransaction) As Boolean
        If (ObjList IsNot Nothing AndAlso ObjList.Count > 0) Then
            For Each obj As clsAdjustmentVoucherDetail In ObjList
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "ADJUSTMENT_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "EMP_CODE", obj.empCode)
                clsCommon.AddColumnsForChange(coll, "PAY_HEAD_CODE", obj.PayHeadCode, False)
                clsCommon.AddColumnsForChange(coll, "ADJUSTMENT_TYPE", obj.ADJUSTMENT_TYPE, True)
                clsCommon.AddColumnsForChange(coll, "ADJUSTMENT_PLUS", obj.adjust_plus)
                clsCommon.AddColumnsForChange(coll, "ADJUSTMENT_MINUS", obj.adjust_minus)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPADJUSTMENT_DETAIL", OMInsertOrUpdate.Insert, "TSPL_EMPADJUSTMENT_DETAIL.ADJUSTMENT_CODE='" + strDocNo + "'", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsAdjustmentVoucherDetail)
        Dim obj As New clsAdjustmentVoucher()
        Dim qry As String = ""
        qry = "select TAV.ADJUSTMENT_CODE,TAVD.EMP_CODE,EMP.EMP_NAME,TAVD.PAY_HEAD_CODE,TPH.PAY_HEAD_NAME,TAVD.ADJUSTMENT_TYPE," _
                     & " TAVD.ADJUSTMENT_PLUS,TAVD.ADJUSTMENT_MINUS FROM TSPL_EMPADJUSTMENT_DETAIL TAVD " _
                     & " INNER JOIN  TSPL_ADJUSTMENT_VOUCHER TAV ON TAVD.ADJUSTMENT_CODE=TAV.ADJUSTMENT_CODE " _
                     & " INNER JOIN TSPL_EMPLOYEE_MASTER EMP ON TAVD.EMP_CODE=EMP.EMP_CODE " _
                     & " LEFT JOIN TSPL_PAYHEAD_MASTER TPH ON TAVD.PAY_HEAD_CODE=TPH.PAY_HEAD_CODE where 2=2"

        qry += " and TAV.ADJUSTMENT_CODE = '" + strCode + "'"

        Dim dt As New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        Dim ObjList As New List(Of clsAdjustmentVoucherDetail)
        Dim Objtr As clsAdjustmentVoucherDetail
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                Objtr = New clsAdjustmentVoucherDetail()
                Objtr.empCode = clsCommon.myCstr(dr("EMP_CODE"))
                Objtr.PayHeadCode = clsCommon.myCstr(dr("PAY_HEAD_CODE"))
                Objtr.PayHeadName = clsCommon.myCstr(dr("PAY_HEAD_NAME"))
                Objtr.ADJUSTMENT_TYPE = clsCommon.myCstr(dr("ADJUSTMENT_TYPE"))
                Objtr.adjust_plus = clsCommon.myCstr(dr("ADJUSTMENT_PLUS"))
                Objtr.adjust_minus = clsCommon.myCstr(dr("ADJUSTMENT_MINUS"))
                ObjList.Add(Objtr)
            Next
        End If
        Return ObjList
    End Function
End Class
