Imports common
Imports System.Data.SqlClient
Public Class clsGrievanceLogging
#Region "variable"
    Public Code As String = Nothing
    Public Description As String = Nothing
    Public Grievance_Type As String = Nothing
    Public Grievance_Name As String = Nothing
    Public Applied_By As String = Nothing
    Public Applied_By_Name As String = Nothing
    Public Txt_Frm_Department As String = Nothing
    Public Txt_Frm_Department_Name As String = Nothing
    Public Txt_For_Department As String = Nothing
    Public Txt_For_Department_Name As String = Nothing
    Public Remarks As String = Nothing
    Public Doc_date As Date
    Public POSTED As ERPTransactionStatus = ERPTransactionStatus.Pending

#End Region
    Public Shared Function SaveData(ByVal obj As clsGrievanceLogging, ByVal isnewentry As Boolean)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try

            Dim qry As String = "select count(*) from tspl_Grievance_Logging_Detail where Code='" + obj.Code + "'"
            Dim isexist As Integer = clsDBFuncationality.getSingleValue(qry, trans)
            If isexist = 0 Then
                isnewentry = True
            Else
                isnewentry = False

            End If
            If isnewentry Then
                obj.Code = clsERPFuncationality.GetNextCode(trans, obj.Doc_date, clsDocType.FrmGrievanceLogging, "", "")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.Doc_date, "dd-MMM-yyyy hh:mm:ss"))
            clsCommon.AddColumnsForChange(coll, "Applied_By", obj.Applied_By)
            clsCommon.AddColumnsForChange(coll, "Frm_Department", obj.Txt_Frm_Department)
            clsCommon.AddColumnsForChange(coll, "For_Department", obj.Txt_For_Department)
            clsCommon.AddColumnsForChange(coll, "Grievance_Type", obj.Grievance_Type)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Posted", "0")
            If isnewentry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

                clsCommonFunctionality.UpdateDataTable(coll, "tspl_Grievance_Logging_Detail", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "tspl_Grievance_Logging_Detail", OMInsertOrUpdate.Update, " Code='" + obj.Code + "'", trans)
            End If
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
            Return False
        End Try

    End Function



    Public Function DeleteData(ByVal strcode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strcode) >= 0) Then
                Dim qry As String = "delete from tspl_Grievance_Logging_Detail where code='" + strcode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
            Return False
        End Try

    End Function
    Public Shared Function GetData(ByVal code As String, ByVal navigatortype As NavigatorType) As clsGrievanceLogging
        Try


            Dim obj As clsGrievanceLogging = Nothing
            Dim qst As String = ("select tspl_Grievance_Logging_Detail.*,tspl_grievance_type_master.name as Grv_Name,tspl_employee_Master.emp_Name as Applied_name," _
                                 & " frm_dpt.department_name as [frm_dpt_name],to_dpt.department_name as [for_dpt_name],tspl_Grievance_Logging_Detail.doc_date from tspl_Grievance_Logging_Detail left join " _
                                 & " tspl_grievance_type_master on tspl_grievance_type_master.code=Grievance_type left join tspl_employee_Master on Applied_by=Emp_Code " _
                                 & " left join tspl_department_Master frm_dpt on  frm_dpt.department_Code=frm_Department  left join tspl_department_Master to_dpt on " _
                                 & " to_dpt.department_Code=For_Department where 2=2 ")
            Select Case navigatortype
                    Case navigatortype.Current
                    qst += "and tspl_Grievance_Logging_Detail.Code in ('" + code + "')"
                    Case navigatortype.Next
                    qst += "and tspl_Grievance_Logging_Detail.Code in (select  min(Code)from tspl_Grievance_Logging_Detail where Code >'" + code + "')"
                    Case navigatortype.First
                    qst += "and tspl_Grievance_Logging_Detail.Code in (select MIN(Code)from tspl_Grievance_Logging_Detail)"

                    Case navigatortype.Last
                    qst += "and tspl_Grievance_Logging_Detail.Code in (select Max(Code)from tspl_Grievance_Logging_Detail)"
                    Case navigatortype.Previous
                    qst += "and tspl_Grievance_Logging_Detail.Code in (select  max(Code)from tspl_Grievance_Logging_Detail where Code <'" + code + "')"

                End Select
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qst)
                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    obj = New clsGrievanceLogging
                    obj.Code = clsCommon.myCstr(dt1.Rows(0)("Code"))
                obj.Description = clsCommon.myCstr(dt1.Rows(0)("Description"))
                If clsCommon.myLen(dt1.Rows(0)("Doc_Date")) > 0 Then
                    obj.Doc_date = clsCommon.myCDate(dt1.Rows(0)("Doc_Date"))
                End If
                obj.Grievance_Type = clsCommon.myCstr(dt1.Rows(0)("Grievance_type"))
                obj.Grievance_Name = clsCommon.myCstr(dt1.Rows(0)("Grv_Name"))
                    obj.Applied_By = clsCommon.myCstr(dt1.Rows(0)("Applied_By"))
                obj.Applied_By_Name = clsCommon.myCstr(dt1.Rows(0)("Applied_name"))
                    obj.Txt_Frm_Department = clsCommon.myCstr(dt1.Rows(0)("Frm_Department"))
                obj.Txt_Frm_Department_Name = clsCommon.myCstr(dt1.Rows(0)("frm_dpt_name"))
                    obj.Txt_For_Department = clsCommon.myCstr(dt1.Rows(0)("For_Department"))
                obj.Txt_For_Department_Name = clsCommon.myCstr(dt1.Rows(0)("for_dpt_name"))
                    obj.Remarks = clsCommon.myCstr(dt1.Rows(0)("Remarks"))
                obj.POSTED = IIf(clsCommon.myCdbl(dt1.Rows(0)("Posted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
                End If
                Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


End Class