Imports common
Imports System.Data
Imports System.Data.SqlClient

Public Class ClsHRDamageDetail
    Public Damage_Detail_Code As String
    Public Damage_Detail_Description As String
    Public Damage_Code As String
    Public Damage_Detail_Date As Date
    Public Deduction_Imposed As String
    Public No_Of_Installment As String
    Public Amt_Realised_Date As Date
    Public Emp_Code As String
    Public Posting_Date As Date
    Public PAY_PERIOD_CODE As String
    Public POSTED As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select DAMAGE_DETAIL_CODE as Code,EMP_CODE as [Employee Code],Damage_Code as [Damage Code],Created_By as [Created By],Created_Date as [Created Date],Modified_By as [Modified By], Modified_Date as [Modified Date]" & _
                            " from TSPL_HR_DAMAGE_DETAIL "
        str = clsCommon.ShowSelectForm("DAMAG", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    Public Shared Function SaveData(ByVal obj As ClsHRDamageDetail, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = String.Empty
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try

            Dim coll As New Hashtable()
            If isNewEntry Then
                obj.Damage_Detail_Code = clsERPFuncationality.GetNextCode(trans, obj.Damage_Detail_Date, clsDocType.DamageDetail, "", "")
            End If

            clsCommon.AddColumnsForChange(coll, "Damage_Detail_Code", obj.Damage_Detail_Code)
            clsCommon.AddColumnsForChange(coll, "Damage_Detail_Date", clsCommon.GetPrintDate(obj.Damage_Detail_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Amt_Realised_Date", clsCommon.GetPrintDate(obj.Amt_Realised_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Damage_Detail_Description", obj.Damage_Detail_Description)
            clsCommon.AddColumnsForChange(coll, "Damage_Code", obj.Damage_Code)

            clsCommon.AddColumnsForChange(coll, "Deduction_Imposed", obj.Deduction_Imposed)
            clsCommon.AddColumnsForChange(coll, "No_Of_Installment", obj.No_Of_Installment)
            clsCommon.AddColumnsForChange(coll, "Emp_Code", obj.Emp_Code)
            'clsCommon.AddColumnsForChange(coll, "PAY_PERIOD_CODE", obj.PAY_PERIOD_CODE)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Posted", "0")
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_DAMAGE_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_DAMAGE_DETAIL", OMInsertOrUpdate.Update, "TSPL_HR_DAMAGE_DETAIL.Damage_Detail_Code='" + obj.Damage_Detail_Code + "'", trans)
            End If


            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction, ByVal NavType As NavigatorType) As ClsHRDamageDetail
        Dim obj As ClsHRDamageDetail = Nothing
        Dim Arr As List(Of ClsHRDamageDetail) = Nothing
        Dim qry As String = "select * from TSPL_HR_DAMAGE_DETAIL where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_HR_DAMAGE_DETAIL.Damage_Detail_Code = (select MIN(Damage_Detail_Code) from TSPL_HR_DAMAGE_DETAIL WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_HR_DAMAGE_DETAIL.Damage_Detail_Code = (select Max(Damage_Detail_Code) from TSPL_HR_DAMAGE_DETAIL WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_HR_DAMAGE_DETAIL.Damage_Detail_Code = (select TOP 1 Damage_Detail_Code from TSPL_HR_DAMAGE_DETAIL WHERE 1=1 " + whrclas + " and Damage_Detail_Code='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_HR_DAMAGE_DETAIL.Damage_Detail_Code = (select Min(Damage_Detail_Code) from TSPL_HR_DAMAGE_DETAIL where Damage_Detail_Code > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_HR_DAMAGE_DETAIL.Damage_Detail_Code = (select Max(Damage_Detail_Code) from TSPL_HR_DAMAGE_DETAIL where Damage_Detail_Code < '" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsHRDamageDetail()
            obj.Damage_Detail_Code = clsCommon.myCstr(dt.Rows(0)("Damage_Detail_Code"))
            obj.Damage_Detail_Description = clsCommon.myCstr(dt.Rows(0)("Damage_Detail_Description"))
            obj.Damage_Code = clsCommon.myCstr(dt.Rows(0)("Damage_Code"))
            obj.Damage_Detail_Date = clsCommon.myCDate(dt.Rows(0)("Damage_Detail_Date"))
            obj.Deduction_Imposed = clsCommon.myCstr(dt.Rows(0)("Deduction_Imposed"))
            obj.No_Of_Installment = clsCommon.myCstr(dt.Rows(0)("No_Of_Installment"))
            obj.Amt_Realised_Date = clsCommon.myCDate(dt.Rows(0)("Amt_Realised_Date"))
            obj.Emp_Code = clsCommon.myCstr(dt.Rows(0)("Emp_Code"))
            'obj.PAY_PERIOD_CODE = clsCommon.myCstr(dt.Rows(0)("PAY_PERIOD_CODE"))
           obj.POSTED = IIf(clsCommon.myCdbl(dt.Rows(0)("Posted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
        End If

        Return obj
    End Function
    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean) As Boolean

        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt")
            Dim obj As ClsHRDamageDetail = ClsHRDamageDetail.GetData(strDocNo, Nothing, NavigatorType.Current)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Damage_Detail_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.POSTED = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If

            Dim qry As String = "Update TSPL_HR_DAMAGE_DETAIL set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where Damage_Detail_Code ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
            'trans.Commit()
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class
