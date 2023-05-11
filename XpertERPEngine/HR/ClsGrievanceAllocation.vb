Imports Common
Imports System.Data
Imports System.Data.SqlClient
Public Class ClsGrievanceAllocation

#Region "Variables"

    Public DOC_CODE As String

    Public DOC_DATE As DateTime
    
    Public Description As String
    Public Grievance_Type_Code As String
    Public Greivance_Logging_Code As String
    Public Grievance_Type_Name As String
    Public Applied_By_Code As String
    Public Applied_By_Name As String
    Public Logging_Date As Date
    Public Frm_Dpt_Code As String
    Public Frm_Dpt_Name As String

    Public For_Dpt_Code As String
    Public For_Dpt_Name As String
    Public Remarks As String

    Public Allocated_to As String
    Public Allocated_to_name As String
    Public Allocated_Date As Date

    Public CREATED_BY As String
    Public Posting_Date As Date
    Public POSTED As ERPTransactionStatus = ERPTransactionStatus.Pending
    '' grid columns

    Public Shared ObjList As List(Of ClsGrievanceAllocation)

#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal strRoute_Code As String, ByVal NavType As NavigatorType) As List(Of ClsGrievanceAllocation)
        Return GetData(Nothing)
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


            qry = "delete from TSPL_Grievance_Logging_Allocation where DOCument_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)



            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal trans As SqlTransaction) As List(Of ClsGrievanceAllocation)

        Dim obj As New ClsGrievanceAllocation()
        Dim objtr As New ClsGrievanceAllocation

        ObjList = New List(Of ClsGrievanceAllocation)

        Dim qry As String = ""
        Dim Dt As DataTable

        qry = "select tspl_Grievance_Logging_Detail.*,tspl_grievance_type_master.name as Grv_Name,tspl_employee_Master.emp_Name as Applied_name, frm_dpt.department_name " _
          & " as [frm_dpt_name],to_dpt.department_name as [for_dpt_name],Allocated_to,Alloated_Date as Allocated_date,all_t.emp_name as [Allocated_To_Name],tspl_Grievance_Logging_Detail.Doc_Date as Log_date from tspl_Grievance_Logging_Detail left join  tspl_grievance_type_master on " _
          & " tspl_grievance_type_master.code=Grievance_type left join tspl_employee_Master on Applied_by=Emp_Code  left join tspl_department_Master frm_dpt on " _
          & " frm_dpt.department_Code=frm_Department  left join tspl_department_Master to_dpt on  to_dpt.department_Code=For_Department left join " _
          & " Tspl_Grievance_Logging_Allocation on Tspl_Grievance_Logging_Allocation.grievance_logging_code=tspl_Grievance_Logging_Detail.code Left join tspl_employee_master all_t on all_t.emp_code=Allocated_to where 2=2  "
        Dt = clsDBFuncationality.GetDataTable(qry, trans)
        If (Dt IsNot Nothing AndAlso Dt.Rows.Count > 0) Then
            For Each row As DataRow In Dt.Rows
                obj = New ClsGrievanceAllocation()

                obj.Greivance_Logging_Code = clsCommon.myCstr(row.Item("Code"))
                obj.Description = clsCommon.myCstr(row.Item("Description"))
                If clsCommon.myLen(row.Item("Log_date")) > 0 Then
                    obj.Logging_Date = clsCommon.myCDate(row.Item("Log_date"))
                Else
                    obj.Logging_Date = clsCommon.GETSERVERDATE()
                End If
                obj.Grievance_Type_Code = clsCommon.myCstr(row.Item("Grievance_Type"))
                obj.Grievance_Type_Name = clsCommon.myCstr(row.Item("Grv_Name"))
                obj.Applied_By_Code = clsCommon.myCstr(row.Item("Applied_By"))
                obj.Applied_By_Name = clsCommon.myCstr(row.Item("Applied_Name"))
                obj.Frm_Dpt_Code = clsCommon.myCstr(row.Item("Frm_Department"))
                obj.Frm_Dpt_Name = clsCommon.myCstr(row.Item("Frm_Dpt_Name"))
                obj.For_Dpt_Code = clsCommon.myCstr(row.Item("For_Department"))
                obj.For_Dpt_Name = clsCommon.myCstr(row.Item("For_Dpt_Name"))

                obj.Remarks = clsCommon.myCstr(row.Item("Remarks"))
                obj.Allocated_to = clsCommon.myCstr(row.Item("Allocated_To"))
                obj.Allocated_to_name = clsCommon.myCstr(row.Item("Allocated_To_Name"))
                If clsCommon.myLen(row.Item("Allocated_Date")) > 0 Then
                    obj.Allocated_Date = clsCommon.myCDate(row.Item("Allocated_Date"))
                End If




                obj.CREATED_BY = clsCommon.myCstr(row.Item("CREATED_BY"))
                obj.POSTED = IIf(clsCommon.myCdbl(row.Item("Posted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)


                'If clsCommon.myLen(row.Item("Posting_Date")) > 0 Then
                '    obj.Posting_Date = clsCommon.myCDate(row.Item("Posting_Date"))
                'Else
                obj.Posting_Date = Nothing
                'End If
                ObjList.Add(obj)
            Next

        End If
        Return ObjList
    End Function



    Public Shared Function SaveData(ByVal objList As List(Of ClsGrievanceAllocation), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim isNewEntry As Boolean
            Dim txtcode As String = String.Empty
            For Each obj As ClsGrievanceAllocation In objList
                'If clsCommon.myLen(txtcode) > 0 Then
                '    obj.DOC_CODE = txtcode
                'End If
                'If clsCommon.myLen(obj.DOC_CODE) <= 0 Then
                '    isNewEntry = True
                '    txtcode = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(clsCommon.GETSERVERDATE(trans)), clsDocType.VLC_Target, "", "")
                '    obj.DOC_CODE = txtcode
                'Else
                '    isNewEntry = False
                '    Dim Strqrys As String = "SELECT Count(*) FROM TSPL_Grievance_Logging_Allocation where Document_Code = '" & obj.DOC_CODE & "' and posted=1"
                '    Dim checks As Integer = clsDBFuncationality.getSingleValue(Strqrys, trans)
                '    If checks = 0 Then
                '    Else
                '        common.clsCommon.MyMessageBoxShow("This Code:" + obj.DOC_CODE + " Is Already Exist")
                '        Exit Function
                '    End If    'obj.DOC_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(clsCommon.GETSERVERDATE(trans)), clsDocType.MilkReceipt, "", "")
                'End If


                'If (clsCommon.myLen(obj.DOC_CODE) <= 0) Then
                '    Throw New Exception("Error in Document Code Generation")
                'End If

                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Allocated_to", obj.Allocated_to)
                clsCommon.AddColumnsForChange(coll, "Grievance_Logging_Code", obj.Greivance_Logging_Code)
                clsCommon.AddColumnsForChange(coll, "Alloated_Date", clsCommon.GetPrintDate(obj.Allocated_Date, "dd/MMM/yyyy hh:mm:ss tt"))


                'clsCommon.AddColumnsForChange(coll, "POSTED", "0")
                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                Dim Strqry As String = "SELECT Count(*) FROM TSPL_Grievance_Logging_Allocation where Grievance_Logging_Code = '" & obj.Greivance_Logging_Code & "' "
                Dim check As Integer = clsDBFuncationality.getSingleValue(Strqry, trans)
                If check = 0 Then
                    isNewEntry = True
                Else
                    isNewEntry = False
                End If
                If isNewEntry Then
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                    'Dim Strqry As String = "SELECT Count(*) FROM TSPL_Grievance_Logging_Allocation where Document_Code = '" & obj.DOC_CODE & "'"
                    'Dim check As Integer = clsDBFuncationality.getSingleValue(Strqry, trans)
                    'If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Grievance_Logging_Allocation", OMInsertOrUpdate.Insert, "", trans)
                    'Else
                    '    common.clsCommon.MyMessageBoxShow("This Code:" + obj.DOC_CODE + " Is Already Exist")
                    '    Exit Function
                    'End If
                Else
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Grievance_Logging_Allocation", OMInsertOrUpdate.Update, "TSPL_Grievance_Logging_Allocation.Grievance_Logging_Code='" + obj.Greivance_Logging_Code + "' ", trans)
                End If

            Next

            'If isSaved Then
            '    trans.Commit()
            'End If
        Catch err As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(err.Message)
            Return False
        End Try
        Return isSaved
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal strRouteNo As String, ByVal isCheckForPosted As Boolean) As Boolean
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt")
            Dim obj As List(Of ClsGrievanceAllocation) = ClsGrievanceAllocation.GetData(Nothing)
            For Each objl As ClsGrievanceAllocation In obj
                If (obj Is Nothing OrElse clsCommon.myLen(objl.DOC_CODE) <= 0) Then
                    Throw New Exception("No Data found to Post")
                End If
                If (isCheckForPosted AndAlso objl.POSTED = 1) Then
                    Throw New Exception("Already Post on :" + objl.Posting_Date)
                End If

                Dim qry As String = "Update TSPL_Grievance_Logging_Allocation set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where DOCument_CODE ='" + strDocNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry)
                Exit For
            Next
            'trans.Commit()
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetPost(ByVal DocDate As Date, ByVal MCC_Code As String, ByVal Shift As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim qry As String
        qry = "SELECT Posted FROM TSPL_MILK_SAMPLE_HEAD WHERE MCC_CODE='" & MCC_Code & "' AND convert(date,DOC_DATE,103)='" & clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") & "' AND SHIFT='" & Shift & "' and Posted=1"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

End Class


