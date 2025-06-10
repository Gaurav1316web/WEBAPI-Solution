Imports common
Imports System.Data.SqlClient

Public Class ClsOutgoingQcEntry
#Region "variables"
    Public comp_code As String = Nothing
    Public document_code As String = Nothing
    Public document_date As Date? = Nothing
    Public bill_to_location As String = Nothing
    Public Locationdesd As String = Nothing
    Public Item_desc As String = Nothing
    ' Public FGCode As String = Nothing
    Public Item_code As String = Nothing
    Public comments As String = Nothing
    Public Remarks As String = Nothing
    Public QC_Status As String = Nothing
    Public Status As String = Nothing
    Public Arr_Pd As List(Of ClsTSPL_PROD_QC_CHECK_DETAIL) = Nothing
    Public Arr_Prod As List(Of clsProductionEntry) = Nothing
    Public Template_Status As String = Nothing
    Public Posting_Date As Date
    Public QC_Start_date As DateTime?
    Public QC_end_date As DateTime

#End Region
    Public Shared Function SaveData(ByVal obj As ClsOutgoingQcEntry, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Shared Function SaveData(ByVal obj As ClsOutgoingQcEntry, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim coll As New Hashtable()
        Try
            If isNewEntry Then
                obj.document_code = clsERPFuncationality.GetNextCode(trans, obj.document_date, clsDocType.OutgoingProduction, "", "")
            End If

            clsCommon.AddColumnsForChange(coll, "Document_Code", obj.document_code)
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.document_date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_code)
            clsCommon.AddColumnsForChange(coll, "Comments", obj.comments)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Location_code", obj.bill_to_location)
            clsCommon.AddColumnsForChange(coll, "QC_Status", obj.Template_Status)
            clsCommon.AddColumnsForChange(coll, "QC_Start_Date", clsCommon.GetPrintDate(obj.QC_Start_date, "dd/MMM/yyyy hh:mm:ss tt "))
            clsCommon.AddColumnsForChange(coll, "QC_END_Date", clsCommon.GetPrintDate(obj.QC_end_date, "dd/MMM/yyyy hh:mm:ss tt "))
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PROD_QC_CHECK_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PROD_QC_CHECK_HEAD", OMInsertOrUpdate.Update, "  Document_Code='" + obj.document_code + "'", trans)
            End If

            ClsTSPL_PROD_QC_CHECK_DETAIL.SaveData(obj.document_code, obj.Arr_Pd, trans)

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.document_code, "TSPL_PROD_QC_CHECK_HEAD", "Document_Code", "TSPL_QC_CHECK_PARA_DETAIL", "Document_Code", trans)

            clsProductionEntry.SaveData(obj.document_code, obj.Arr_Prod, trans)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            coll = Nothing
        End Try
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As ClsOutgoingQcEntry
        Dim dt As New DataTable()
        Dim dt1 As New DataTable()
        Dim dt2 As New DataTable()
        'Dim objtr As New clsQualityCheckForSRNDetail()
        Dim objpd As New ClsTSPL_PROD_QC_CHECK_DETAIL()
        Dim objPR As New clsProductionEntry

        'Dim objtr_Detail As New clsQualityCheckDetail()
        Try
            Dim obj As New ClsOutgoingQcEntry()
            obj.Arr_Pd = New List(Of ClsTSPL_PROD_QC_CHECK_DETAIL)
            Dim qry As String = "select document_Code,document_date,TSPL_PROD_QC_CHECK_HEAD.item_code,item_desc,QC_Status,TSPL_PROD_QC_CHECK_HEAD.Location_code,location_desc,comments,TSPL_PROD_QC_CHECK_HEAD.remarks,status,TSPL_PROD_QC_CHECK_HEAD.QC_Start_Date,TSPL_PROD_QC_CHECK_HEAD.QC_END_Date from TSPL_PROD_QC_CHECK_HEAD
                                    left outer join tspl_item_master on tspl_item_master.item_code=TSPL_PROD_QC_CHECK_HEAD.item_code
                                    left outer join tspl_location_master on tspl_location_master.location_code=TSPL_PROD_QC_CHECK_HEAD.location_code "
            Select Case NavType
                Case NavigatorType.Current
                    qry += " where TSPL_PROD_QC_CHECK_HEAD.document_code='" + strCode + "'"
                Case NavigatorType.First
                    qry += " where TSPL_PROD_QC_CHECK_HEAD.document_code in (select min(document_code) from TSPL_PROD_QC_CHECK_HEAD )"
                Case NavigatorType.Last
                    qry += " where TSPL_PROD_QC_CHECK_HEAD.document_code in (select max(document_code) from TSPL_PROD_QC_CHECK_HEAD )"
                Case NavigatorType.Next
                    qry += "where TSPL_PROD_QC_CHECK_HEAD.document_code in (select min(document_code) from TSPL_PROD_QC_CHECK_HEAD where  document_code>'" + strCode + "')"
                Case NavigatorType.Previous
                    qry += " where TSPL_PROD_QC_CHECK_HEAD.document_code in (select max(document_code) from TSPL_PROD_QC_CHECK_HEAD Where document_code <'" + strCode + "')"
            End Select
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.document_code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
                obj.document_date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
                obj.bill_to_location = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
                obj.Item_code = clsCommon.myCstr(dt.Rows(0)("item_code"))
                obj.comments = clsCommon.myCstr(dt.Rows(0)("Comments"))
                obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
                obj.Locationdesd = clsCommon.myCstr(dt.Rows(0)("location_desc"))
                obj.Item_desc = clsCommon.myCstr(dt.Rows(0)("item_desc"))
                obj.Template_Status = clsCommon.myCstr(dt.Rows(0)("QC_Status"))
                obj.Status = clsCommon.myCstr(dt.Rows(0)("Status"))
                If Not IsDBNull(dt.Rows(0)("QC_Start_Date")) Then
                    obj.QC_Start_date = clsCommon.myCDate(dt.Rows(0)("QC_Start_Date"))
                End If
                If Not IsDBNull(dt.Rows(0)("QC_end_date")) Then
                    obj.QC_end_date = clsCommon.myCDate(dt.Rows(0)("QC_end_date"))
                End If
                'obj.QC_Start_date = clsCommon.myCDate(dt.Rows(0)("QC_Start_Date"))
                'bj.QC_end_date = clsCommon.myCDate(dt.Rows(0)("QC_END_Date"))
                qry = "select  Row_Number() Over (Order By (SELECT 1) Asc) as [S No], TSPL_QC_LOG_SHEET_MASTER.Description as QCNAme, TSPL_QC_LOG_SHEET_MASTER.Code,TSPL_QC_CHECK_PARA_DETAIL.Item_Code,TSPL_QC_CHECK_PARA_DETAIL.Param_L_Range,TSPL_QC_CHECK_PARA_DETAIL.Param_U_Range,TSPL_QC_CHECK_PARA_DETAIL.Description,TSPL_QC_CHECK_PARA_DETAIL.Remarks,TSPL_QC_CHECK_PARA_DETAIL.Description_Status,TSPL_QC_CHECK_PARA_DETAIL.InputData,TSPL_QC_LOG_SHEET_MASTER.Nature from TSPL_QC_CHECK_PARA_DETAIL
                     Left outer join TSPL_QC_LOG_SHEET_MASTER on TSPL_QC_LOG_SHEET_MASTER.Code=TSPL_QC_CHECK_PARA_DETAIL.QC_Param_Code "
                qry += "   where TSPL_QC_CHECK_PARA_DETAIL.document_code ='" + obj.document_code + "'"
                dt1 = New DataTable()
                dt1 = clsDBFuncationality.GetDataTable(qry, trans)

                For Each dr As DataRow In dt1.Rows
                    objpd = New ClsTSPL_PROD_QC_CHECK_DETAIL()

                    'objpd.Line_No = clsCommon.myCstr(dr("SNo"))
                    objpd.QCparamNAme = clsCommon.myCstr(dr("QCNAme"))
                    objpd.Param_L_Range = clsCommon.myCDecimal(dr("Param_L_Range"))
                    objpd.Param_U_Range = clsCommon.myCstr(dr("Param_U_Range"))
                    objpd.Description = clsCommon.myCstr(dr("Description"))
                    objpd.Remarks = clsCommon.myCstr(dr("Remarks"))
                    objpd.Description_Status = clsCommon.myCstr(dr("Description_Status"))
                    objpd.InputData = clsCommon.myCDecimal(dr("InputData"))
                    objpd.QC_Param_Code = clsCommon.myCstr(dr("Code"))
                    objpd.item_code = clsCommon.myCstr(dr("Item_Code"))
                    objpd.nature = clsCommon.myCstr(dr("Nature"))

                    obj.Arr_Pd.Add(objpd)
                Next
            End If 'dt1 cond.
            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    'Public Shared Function funOutGoingQcEntryPrint(ByVal Form_ID As String, ByVal isCancel As Boolean, ByVal strDate As DateTime, ByVal StrCode As String, ByVal strBatch As String, ByVal shift As String, ByVal whr As String) As Boolean
    '    'Dim whr As String
    '    Dim dt As DataTable = Nothing
    '    'Dim dtt As DataTable = Nothing
    '    'Dim strBatch As String = Nothing
    '    'Dim shift As String = Nothing
    '    'Dim StrShift As String = Nothing
    '    'If clsCommon.myLen(StrCode) <= 0 Then

    '    '    Throw New Exception("Document number not found")
    '    'End If
    '    'If txtprodCode.arrValueMember IsNot Nothing AndAlso txtprodCode.arrValueMember.Count > 0 Then
    '    '    whr = " where TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE in (" + clsCommon.GetMulcallString(txtprodCode.arrValueMember) + ")"
    '    'End If
    '    'Dim batch As String = " select  batch_code_manual from TSPL_SPP_PRODUCTION_ENTRY " + whr
    '    'dt = clsDBFuncationality.GetDataTable(batch)

    '    'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '    '    For Each btch In dt.Rows
    '    '        If clsCommon.myLen(strBatch) > 0 Then
    '    '            strBatch += "," + (clsCommon.myCstr(btch("batch_code_manual")))
    '    '        Else
    '    '            strBatch = (clsCommon.myCstr(btch("batch_code_manual")))
    '    '        End If
    '    '    Next
    '    'End If
    '    'StrShift = " select distinct  Shift_Code from TSPL_SPP_PRODUCTION_ENTRY where PROD_ENTRY_CODE in (
    '    '                              select PROD_ENTRY_CODE from TSPL_PROD_QC_CHECK_PRODUCTION_ENTRY where Document_Code='" + txtDocNo.Value + "') "
    '    'dtt = clsDBFuncationality.GetDataTable(StrShift)
    '    'If dtt IsNot Nothing AndAlso dtt.Rows.Count > 0 Then
    '    '    For Each sft In dtt.Rows
    '    '        If clsCommon.myLen(shift) > 0 Then
    '    '            shift += "," + (clsCommon.myCstr(sft("Shift_Code")))
    '    '        Else
    '    '            shift = (clsCommon.myCstr(sft("Shift_Code")))
    '    '        End If
    '    '    Next
    '    'End If

    '    Dim frmCRV As New frmCrystalReportViewer()
    '    Dim qry As String = "  select  '" + strBatch + "' as [Batch_Code], '" + shift + "' as [Shift_Code],tSPL_PROD_QC_CHECK_HEAD.document_code,tSPL_PROD_QC_CHECK_HEAD.document_date,
    '                        tspl_location_master.location_desc,tspl_location_master.add1,tspl_location_master.division_code,division_Name,division_address,TSPL_SPP_PRODUCTION_ENTRY1.prod_date_from, prod_date_to,TSPL_QC_LOG_SHEET_MASTER.description as QCPARAMNAME,TSPL_QC_LOG_SHEET_MASTER.clause_ref,TSPL_QC_LOG_SHEET_MASTER.is_ref,TSPL_QC_CHECK_PARA_DETAIL.inputdata,param_L_range,Param_U_range,TSPL_QC_CHECK_PARA_DETAIL.remarks,TSPL_PARAMETER_RANGE_MASTER_QC.description,TSPL_ITEM_MASTER.item_desc ,TSPL_QC_CHECK_PARA_DETAIL.Description_Status,TSPL_QC_LOG_SHEET_MASTER.AliasName,tSPL_PROD_QC_CHECK_HEAD.qc_start_date,tSPL_PROD_QC_CHECK_HEAD.qc_end_date,tspl_location_master.CMA_CML,tspl_location_master.QC_IS,tspl_location_master.ValidUpto,tspl_location_master.GradeType,TSPL_QC_LOG_SHEET_MASTER.Nature,tspl_spp_production_entry.Shift_Code from tSPL_PROD_QC_CHECK_HEAD --header
    '                        left outer join TSPL_QC_CHECK_PARA_DETAIL  on TSPL_QC_CHECK_PARA_DETAIL.document_code=tSPL_PROD_QC_CHECK_HEAD.document_code --save prooduction code
    '                        left outer join tspl_location_master on tspl_location_master.location_code=tSPL_PROD_QC_CHECK_HEAD.location_code
    '                        left outer join TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER on TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.item_code=tSPL_PROD_QC_CHECK_HEAD.item_code and TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.qc_code=TSPL_QC_CHECK_PARA_DETAIL.qc_param_code 
    '                        LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.ITEM_CODE=TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.ITEM_CODE
    '                        left outer join TSPL_QC_LOG_SHEET_MASTER on TSPL_QC_LOG_SHEET_MASTER.code=TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.qc_code
    '                        left outer join TSPL_PROD_QC_CHECK_PRODUCTION_ENTRY on TSPL_PROD_QC_CHECK_PRODUCTION_ENTRY.document_code=tSPL_PROD_QC_CHECK_HEAD.document_code
    '                        left outer join tspl_spp_production_entry on tspl_spp_production_entry.PROD_ENTRY_CODE=TSPL_PROD_QC_CHECK_PRODUCTION_ENTRY.PROD_ENTRY_CODE
    '                        left outer join TSPL_PARAMETER_RANGE_MASTER_QC on TSPL_PARAMETER_RANGE_MASTER_QC.qc_param_code=TSPL_QC_LOG_SHEET_MASTER.code
    '                        left outer join (select min(prod_date) as prod_date_from,max(prod_date) as prod_date_to,max(prod_entry_code) as prod_entry_code from TSPL_SPP_PRODUCTION_ENTRY " + whr + " ) TSPL_SPP_PRODUCTION_ENTRY1 on TSPL_SPP_PRODUCTION_ENTRY1.prod_entry_code=TSPL_PROD_QC_CHECK_PRODUCTION_ENTRY.PROD_ENTRY_CODE 
    '        where tSPL_PROD_QC_CHECK_HEAD.document_code='" + StrCode + "' and TSPL_SPP_PRODUCTION_ENTRY1.PROD_ENTRY_CODE is not null order by TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.SNO"

    '    dt = clsDBFuncationality.GetDataTable(qry)

    '    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
    '        common.clsCommon.MyMessageBoxShow("No Record Found")
    '    Else
    '        frmCRV.funreport(Form_ID, CrystalReportFolder.PRODUCTION, dt, "rptOutgoingQCCheckEntry", "Outgoing Quality Control Report")
    '    End If

    '    Return True
    'End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            Dim obj As ClsOutgoingQcEntry = ClsOutgoingQcEntry.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.document_code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = "1") Then
                Throw New Exception("Already Posted")
            End If
            Dim qry As String = "Update TSPL_PROD_QC_CHECK_HEAD set Status=1, Posted_Date='" + strPostDate + "',Posted_By='" + objCommonVar.CurrentUserCode + "'  where Document_code ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.document_code, "TSPL_PROD_QC_CHECK_HEAD", "Document_Code", trans)

            'clsDBFuncationality.ExecuteNonQuery("Update TSPL_PROD_QC_CHECK_HEAD set posted='1', Modified_By = '" + objCommonVar.CurrentUserCode + "',Modified_Date = '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "yyyy-MM-dd") + "'  where document_code='" & obj.document_code & "'", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim obj As New ClsOutgoingQcEntry()
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim isPosted As Integer = 0
            isPosted = clsDBFuncationality.getSingleValue("SELECT Count(*) FROM TSPL_PROD_QC_CHECK_HEAD where Document_code = '" & strCode & "' and Status=1", trans)
            If (isPosted = 1) Then
                Throw New Exception("Already Posted on :" + obj.Posting_Date)
            End If
            clsCommonFunctionality.SaveDeletedData(objCommonVar.CurrentUserCode, obj.document_code, "TSPL_PROD_QC_CHECK_HEAD", "Document_Code", "TSPL_QC_CHECK_PARA_DETAIL", "Document_Code", trans)

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.document_code, "TSPL_PROD_QC_CHECK_HEAD", "Document_Code", "TSPL_QC_CHECK_PARA_DETAIL", "Document_Code", trans)

            clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, strCode, "TSPL_PROD_QC_CHECK_HEAD", "Document_Code", "TSPL_QC_CHECK_PARA_DETAIL", "Document_Code", "TSPL_PROD_QC_CHECK_PRODUCTION_ENTRY", "Document_Code", trans)

            Dim qry As String
            qry = "delete from TSPL_QC_CHECK_PARA_DETAIL where Document_code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "DELETE FROM TSPL_PROD_QC_CHECK_PRODUCTION_ENTRY WHERE Document_code='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)


            qry = "DELETE FROM TSPL_PROD_QC_CHECK_HEAD WHERE Document_code='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)




            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As ClsOutgoingQcEntry = ClsOutgoingQcEntry.GetData(strCode, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.document_code) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
            If Not (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Transaction status should be posted.")
            End If
            '.SaveHistoryData(objCommonVar.CurrentUserCode, obj.document_code, "TSPL_PROD_QC_CHECK_HEAD", "Document_Code", "TSPL_QC_CHECK_PARA_DETAIL", "Document_Code", trans)
            Dim qry As String
            If obj.Status = 1 Then
                qry = "update TSPL_PROD_QC_CHECK_HEAD set Status=0,Posted_Date=null,Posted_By=null where document_code='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.document_code, "TSPL_PROD_QC_CHECK_HEAD", "Document_Code", trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class ClsTSPL_PROD_QC_CHECK_DETAIL
#Region "variables"
    Public Document_Code As String = Nothing
    Public Line_No As Integer = Nothing
    Public Status As Integer = Nothing
    Public item_code As String = Nothing
    Public Nature As String = Nothing
    Public Row_type As Date = Nothing
    Public Unit_code As String = Nothing
    Public QCparamNAme As String = Nothing
    Public BagQty As Double
    Public Param_L_Range As Decimal?
    Public Batch_Code_Manual As String = Nothing
    Public Description As String = Nothing
    Public Prod_qty As Double
    Public Shift As String = Nothing
    Public OK_qty As Decimal = Nothing
    Public Reject_qty As String = 0
    Public Remarks As String = Nothing
    Public Reject_remarks As String = Nothing
    Public comments As String = Nothing
    Public Location As String = Nothing
    Public QC_status As String = Nothing
    Public Bom_code As String = Nothing
    Public Lot_no As String = Nothing
    Public PK_Id As Integer
    Public QC_Param_Code As String = Nothing
    ' Public Param_L_Range As Decimal
    Public Param_U_Range As Decimal
    Public InputData As Decimal?
    Public Description_Status As String = Nothing

#End Region
    Public Shared Function SaveData(ByVal strCode As String, ByVal arr As List(Of ClsTSPL_PROD_QC_CHECK_DETAIL), ByVal trans As SqlTransaction) As Boolean
        Dim coll As New Hashtable()
        Try
            Dim qry As String = "delete from TSPL_QC_CHECK_PARA_DETAIL where document_code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As ClsTSPL_PROD_QC_CHECK_DETAIL In arr
                    coll = New Hashtable()
                    'clsCommon.AddColumnsForChange(coll, "PK_Id", objtr.PK_Id)
                    clsCommon.AddColumnsForChange(coll, "Document_Code", strCode)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.item_code)
                    clsCommon.AddColumnsForChange(coll, "QC_Param_Code", objtr.QC_Param_Code)
                    clsCommon.AddColumnsForChange(coll, "Param_L_Range", objtr.Param_L_Range)
                    clsCommon.AddColumnsForChange(coll, "Param_U_Range", objtr.Param_U_Range)
                    clsCommon.AddColumnsForChange(coll, "Description", objtr.Description)
                    clsCommon.AddColumnsForChange(coll, "InputData", objtr.InputData)
                    clsCommon.AddColumnsForChange(coll, "Remarks", objtr.Remarks)
                    clsCommon.AddColumnsForChange(coll, "Description_Status", objtr.Description_Status)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_QC_CHECK_PARA_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            coll = Nothing
        End Try
    End Function
End Class
Public Class clsProductionEntry
#Region "variables"
    Public Document_Code As String = Nothing
    Public ProdcutionCode As String = Nothing
#End Region
    Public Shared Function SaveData(ByVal strCode As String, ByVal arr As List(Of clsProductionEntry), ByVal trans As SqlTransaction) As Boolean
        Dim coll As New Hashtable()
        Try
            Dim qry As String = "delete from TSPL_PROD_QC_CHECK_PRODUCTION_ENTRY where document_code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsProductionEntry In arr
                    coll = New Hashtable()
                    'clsCommon.AddColumnsForChange(coll, "PK_Id", objtr.PK_Id)
                    clsCommon.AddColumnsForChange(coll, "Document_Code", strCode)
                    clsCommon.AddColumnsForChange(coll, "Prod_Entry_Code", objtr.ProdcutionCode)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PROD_QC_CHECK_PRODUCTION_ENTRY", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            coll = Nothing
        End Try
    End Function
End Class

