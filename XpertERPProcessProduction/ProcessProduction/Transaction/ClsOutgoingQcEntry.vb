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
    Public QC_Type As String = Nothing
    Public Status As String = Nothing
    Public Arr_Pd As List(Of ClsTSPL_PROD_QC_CHECK_DETAIL) = Nothing
    Public Arr_Prod As List(Of clsProductionEntry) = Nothing
    Public Template_Status As String = Nothing
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
                ' obj.document_code = clsCommon.myCstr(clsERPFuncationality.GetNextCode(trans, obj.document_date, clsDocType.QualityCheckForSRN, clsDocTransactionType.IncomingQualityCheck, obj.bill_to_location))
                'Else
                'obj.document_code = clsCommon.myCstr(clsERPFuncationality.GetNextCode(trans, obj.document_date, clsDocType.frmOutgoingQCEntry, clsDocTransactionType.ProductionQC, obj.bill_to_location))
                'obj.document_code = clsERPFuncationality.GetNextCode(trans, obj.document_date, clsDocType.QCOutgoingEntry, "", "")
                obj.document_code = clsERPFuncationality.GetNextCode(trans, obj.document_date, clsDocType.OutgoingProduction, "", "")
            End If

            clsCommon.AddColumnsForChange(coll, "Document_Code", obj.document_code)
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.document_date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_code)
            clsCommon.AddColumnsForChange(coll, "Comments", obj.comments)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Location_code", obj.bill_to_location)
            clsCommon.AddColumnsForChange(coll, "QC_Status", obj.Template_Status)
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
            Dim qry As String = "select document_Code,document_date,TSPL_PROD_QC_CHECK_HEAD.item_code,item_desc,QC_Status,TSPL_PROD_QC_CHECK_HEAD.Location_code,location_desc,comments,remarks,status from TSPL_PROD_QC_CHECK_HEAD
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
                qry = "select  Row_Number() Over (Order By (SELECT 1) Asc) as [S No], TSPL_QC_LOG_SHEET_MASTER.Description as QCNAme, TSPL_QC_CHECK_PARA_DETAIL.Param_L_Range,TSPL_QC_CHECK_PARA_DETAIL.Param_U_Range,TSPL_QC_CHECK_PARA_DETAIL.Description,TSPL_QC_CHECK_PARA_DETAIL.Remarks,TSPL_QC_CHECK_PARA_DETAIL.Description_Status,TSPL_QC_CHECK_PARA_DETAIL.InputData from TSPL_QC_CHECK_PARA_DETAIL
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
                    'objpd.comments = clsCommon.myCstr(dr("COMMENTS"))
                    'objpd.Description = clsCommon.myCstr(dr("DESCRIPTION"))
                    'objpd.Shift = clsCommon.myCstr(dr("Shift_Code"))
                    obj.Arr_Pd.Add(objpd)
                Next
                'qry = "select PROD_ENTRY_CODE from TSPL_PROD_QC_CHECK_PRODUCTION_ENTRY "
                'qry += " where Document_Code='" + obj.document_code + "'"
                'dt2 = New DataTable()
                'dt2 = clsDBFuncationality.GetDataTable(qry, trans)
                'For Each dr As DataRow In dt1.Rows
                '    objPR = New clsProductionEntry()
                '    objPR.ProdcutionCode = clsCommon.myCstr(dr("Prod_Entry_Code"))
                '    obj.Arr_Prod.Add(objPR)
                'Next
            End If 'dt1 cond.
            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
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
            'clsDBFuncationality.ExecuteNonQuery("Update TSPL_PROD_QC_CHECK_HEAD set posted='1', Modified_By = '" + objCommonVar.CurrentUserCode + "',Modified_Date = '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "yyyy-MM-dd") + "'  where document_code='" & obj.document_code & "'", trans)
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
    Public InputData As Decimal
    Public Description_Status As String = Nothing

#End Region
    Public Shared Function SaveData(ByVal strCode As String, ByVal arr As List(Of ClsTSPL_PROD_QC_CHECK_DETAIL), ByVal trans As SqlTransaction) As Boolean
        Dim coll As New Hashtable()
        Try


            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As ClsTSPL_PROD_QC_CHECK_DETAIL In arr
                    coll = New Hashtable()
                    'clsCommon.AddColumnsForChange(coll, "PK_Id", objtr.PK_Id)
                    clsCommon.AddColumnsForChange(coll, "Document_Code", strCode)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.item_code)
                    clsCommon.AddColumnsForChange(coll, "Unit_code", objtr.Unit_code)
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

