Imports common
Imports System.Data.SqlClient
Public Class clsRMProcessLoss
#Region "variables"
    Public Location As String = Nothing
    Public document_code As String = Nothing
    Public document_date As Date? = Nothing
    Public Comments As String = Nothing
    Public Locationdesc As String = Nothing
    Public Item_desc As String = Nothing
    Public Item_code As String = Nothing
    Public Remarks As String = Nothing
    Public QC_Status As String = Nothing
    Public Status As String = Nothing
    Public Arr_Pd As List(Of ClsRmProcessLossDetail) = Nothing
    Public Template_Status As String = Nothing
    Public Posting_Date As DateTime?
    Public Fromdate As DateTime?
    Public Todate As DateTime?
#End Region
    Public Shared Function SaveData(ByVal obj As clsRMProcessLoss, ByVal isNewEntry As Boolean) As Boolean
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
    Public Shared Function SaveData(ByVal obj As clsRMProcessLoss, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim coll As New Hashtable()
        Try
            If isNewEntry Then
                obj.document_code = clsERPFuncationality.GetNextCode(trans, obj.document_date, clsDocType.RMProcessLoss, "", "")
            End If

            clsCommon.AddColumnsForChange(coll, "Document_Code", obj.document_code)
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.document_date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Comment", obj.Comments)
            clsCommon.AddColumnsForChange(coll, "Location", obj.Location)
            clsCommon.AddColumnsForChange(coll, "From_Date", clsCommon.GetPrintDate(obj.Fromdate, "dd/MMM/yyyy hh:mm:ss tt "))
            clsCommon.AddColumnsForChange(coll, "To_Date", clsCommon.GetPrintDate(obj.Todate, "dd/MMM/yyyy hh:mm:ss tt "))
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RM_PROCESS_LOSS", OMInsertOrUpdate.Insert, "", trans)
            Else

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RM_PROCESS_LOSS", OMInsertOrUpdate.Update, "  Document_Code='" + obj.document_code + "'", trans)
            End If
            ClsRmProcessLossDetail.SaveData(obj.document_code, obj.Arr_Pd, trans)
            'clsProductionEntry.SaveData(obj.document_code, obj.Arr_Prod, trans)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            coll = Nothing
        End Try
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As clsRMProcessLoss
        Dim dt As New DataTable()
        Dim dt1 As New DataTable()
        Dim dt2 As New DataTable()
        'Dim objtr As New clsQualityCheckForSRNDetail()
        Dim objpd As New ClsRmProcessLossDetail()

        'Dim objtr_Detail As New clsQualityCheckDetail()
        Try
            Dim obj As New clsRMProcessLoss()
            obj.Arr_Pd = New List(Of ClsRmProcessLossDetail)
            Dim qry As String = "select TSPL_RM_PROCESS_LOSS.document_Code,document_date,from_date,To_date,TSPL_RM_PROCESS_LOSS.location,location_desc,comment,status from TSPL_RM_PROCESS_LOSS
left outer join tspl_location_master on tspl_location_master.location_code=TSPL_RM_PROCESS_LOSS.location "
            Select Case NavType
                Case NavigatorType.Current
                    qry += " where TSPL_RM_PROCESS_LOSS.document_code='" + strCode + "'"
                Case NavigatorType.First
                    qry += " where TSPL_RM_PROCESS_LOSS.document_code in (select min(document_code) from TSPL_RM_PROCESS_LOSS )"
                Case NavigatorType.Last
                    qry += " where TSPL_RM_PROCESS_LOSS.document_code in (select max(document_code) from TSPL_RM_PROCESS_LOSS )"
                Case NavigatorType.Next
                    qry += "where TSPL_RM_PROCESS_LOSS.document_code in (select min(document_code) from TSPL_RM_PROCESS_LOSS where  document_code>'" + strCode + "')"
                Case NavigatorType.Previous
                    qry += " where TSPL_RM_PROCESS_LOSS.document_code in (select max(document_code) from TSPL_RM_PROCESS_LOSS Where document_code <'" + strCode + "')"
            End Select
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.document_code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
                obj.document_date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
                obj.Location = clsCommon.myCstr(dt.Rows(0)("Location"))
                obj.Comments = clsCommon.myCstr(dt.Rows(0)("Comment"))
                obj.Locationdesc = clsCommon.myCstr(dt.Rows(0)("location_desc"))
                obj.Status = clsCommon.myCstr(dt.Rows(0)("Status"))
                If Not IsDBNull(dt.Rows(0)("From_date")) Then
                    obj.Fromdate = clsCommon.myCDate(dt.Rows(0)("From_date"))
                End If
                If Not IsDBNull(dt.Rows(0)("To_date")) Then
                    obj.Todate = clsCommon.myCDate(dt.Rows(0)("To_date"))
                End If
                'obj.QC_Start_date = clsCommon.myCDate(dt.Rows(0)("QC_Start_Date"))
                'bj.QC_end_date = clsCommon.myCDate(dt.Rows(0)("QC_END_Date"))
                qry = "select tspl_rm_process_loss_detail.Item_Code,Item_Desc,uom,op_qty,Op_cost,rec_qty,rec_cost,issprod_qty,issprod_cost,otheriss_qty,otherIss_cost,stktrns_qty,StkTrns_cost,cl_qty,cl_cost,Pl_qty,pl_per,tspl_rm_process_loss_detail.rate,fnlStk_qty from tspl_rm_process_loss_detail
                        left outer join tspl_item_master on tspl_item_master.Item_Code=tspl_rm_process_loss_detail.item_code "
                qry += "   where tspl_rm_process_loss_detail.document_code ='" + obj.document_code + "'"
                dt1 = New DataTable()
                dt1 = clsDBFuncationality.GetDataTable(qry, trans)

                For Each dr As DataRow In dt1.Rows
                    objpd = New ClsRmProcessLossDetail()
                    objpd.item_code = clsCommon.myCstr(dr("item_code"))
                    objpd.item_Desc = clsCommon.myCstr(dr("item_Desc"))
                    objpd.UOM = clsCommon.myCstr(dr("UOM"))
                    objpd.OP_Qty = clsCommon.myCDecimal(dr("OP_Qty"))
                    objpd.OP_Cost = clsCommon.myCdbl(dr("OP_Cost"))
                    objpd.Rec_Qty = clsCommon.myCDecimal(dr("Rec_Qty"))
                    objpd.Rec_Cost = clsCommon.myCdbl(dr("Rec_Cost"))
                    objpd.IssProd_Qty = clsCommon.myCDecimal(dr("IssProd_Qty"))
                    objpd.IssProd_Cost = clsCommon.myCdbl(dr("IssProd_Cost"))
                    objpd.OtherIss_Qty = clsCommon.myCDecimal(dr("OtherIss_Qty"))
                    objpd.OtherIss_Cost = clsCommon.myCdbl(dr("OtherIss_Cost"))
                    objpd.StkTrns_Qty = clsCommon.myCDecimal(dr("StkTrns_Qty"))
                    objpd.StkTrns_Cost = clsCommon.myCdbl(dr("StkTrns_Cost"))
                    objpd.PL_Qty = clsCommon.myCDecimal(dr("PL_Qty"))
                    objpd.PL_Per = clsCommon.myCDecimal(dr("PL_Per"))
                    objpd.Rate = clsCommon.myCDecimal(dr("Rate"))
                    objpd.FnlStk_Qty = clsCommon.myCdbl(dr("FnlStk_Qty"))
                    objpd.CL_Qty = clsCommon.myCDecimal(dr("CL_Qty"))
                    objpd.Cl_Cost = clsCommon.myCdbl(dr("Cl_Cost"))
                    obj.Arr_Pd.Add(objpd)
                Next
            End If 'dt1 cond.
            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim obj As New clsRMProcessLoss()
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim isPosted As Integer = 0
            isPosted = clsDBFuncationality.getSingleValue("SELECT Count(*) FROM TSPL_RM_PROCESS_LOSS where Document_code = '" & strCode & "' and Status=1", trans)
            If (isPosted = 1) Then
                Throw New Exception("Already Posted on :" + obj.Posting_Date)
            End If


            Dim qry As String
            qry = "delete from TSPL_RM_PROCESS_LOSS_DETAIL where Document_code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "DELETE FROM TSPL_RM_PROCESS_LOSS WHERE Document_code='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsRMProcessLoss = clsRMProcessLoss.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.document_code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = "1") Then
                Throw New Exception("Already Posted")
            End If
            Dim qry As String = "Update TSPL_RM_PROCESS_LOSS set Status=1, Posted_Date='" + strPostDate + "',Posted_By='" + objCommonVar.CurrentUserCode + "'  where Document_code ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            'clsDBFuncationality.ExecuteNonQuery("Update TSPL_PROD_QC_CHECK_HEAD set posted='1', Modified_By = '" + objCommonVar.CurrentUserCode + "',Modified_Date = '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "yyyy-MM-dd") + "'  where document_code='" & obj.document_code & "'", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As clsRMProcessLoss = clsRMProcessLoss.GetData(strCode, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.document_code) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
            If Not (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Transaction status should be posted.")
            End If
            Dim qry As String
            If obj.Status = 1 Then
                qry = "update TSPL_RM_PROCESS_LOSS set Status=0,Posted_Date=null,Posted_By=null where document_code='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class
Public Class ClsRmProcessLossDetail
#Region "variables"
    Public Document_Code As String = Nothing
    Public item_code As String = Nothing
    Public item_Desc As String = Nothing
    Public UOM As String = Nothing
    Public OP_Qty As Decimal?
    Public OP_Cost As Double
    Public Rec_Qty As Decimal?
    Public Rec_Cost As Double
    Public IssProd_Qty As Decimal?
    Public IssProd_Cost As Double
    Public OtherIss_Qty As Decimal?
    Public OtherIss_Cost As Double
    Public StkTrns_Qty As Decimal?
    Public StkTrns_Cost As Double
    Public CL_Qty As Decimal?
    Public Cl_Cost As Double
    Public PL_Per As Decimal?
    Public PL_Qty As Double
    Public Rate As Decimal?
    Public FnlStk_Qty As Double
#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal arr As List(Of ClsRmProcessLossDetail), ByVal trans As SqlTransaction) As Boolean
        Dim coll As New Hashtable()
        Try
            Dim qry As String = "delete from TSPL_RM_PROCESS_LOSS_DETAIL where document_code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As ClsRmProcessLossDetail In arr
                    coll = New Hashtable()
                    'clsCommon.AddColumnsForChange(coll, "PK_Id", objtr.PK_Id)
                    clsCommon.AddColumnsForChange(coll, "Document_Code", strCode)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.item_code)
                    clsCommon.AddColumnsForChange(coll, "Uom", objtr.UOM)
                    clsCommon.AddColumnsForChange(coll, "OP_Qty", objtr.OP_Qty)
                    clsCommon.AddColumnsForChange(coll, "OP_Cost", objtr.OP_Cost)
                    clsCommon.AddColumnsForChange(coll, "Rec_Qty", objtr.Rec_Qty)
                    clsCommon.AddColumnsForChange(coll, "Rec_Cost", objtr.Rec_Cost)
                    clsCommon.AddColumnsForChange(coll, "IssProd_Qty", objtr.IssProd_Qty)
                    clsCommon.AddColumnsForChange(coll, "IssProd_Cost", objtr.IssProd_Cost)
                    clsCommon.AddColumnsForChange(coll, "OtherIss_Qty", objtr.OtherIss_Qty)
                    clsCommon.AddColumnsForChange(coll, "OtherIss_Cost", objtr.OtherIss_Cost)
                    clsCommon.AddColumnsForChange(coll, "StkTrns_Qty", objtr.StkTrns_Qty)
                    clsCommon.AddColumnsForChange(coll, "StkTrns_Cost", objtr.StkTrns_Cost)
                    clsCommon.AddColumnsForChange(coll, "CL_Qty", objtr.CL_Qty)
                    clsCommon.AddColumnsForChange(coll, "Cl_Cost", objtr.Cl_Cost)
                    clsCommon.AddColumnsForChange(coll, "PL_Qty", objtr.PL_Qty)
                    clsCommon.AddColumnsForChange(coll, "PL_Per", objtr.PL_Per)
                    clsCommon.AddColumnsForChange(coll, "Rate", objtr.Rate)
                    clsCommon.AddColumnsForChange(coll, "FnlStk_Qty", objtr.FnlStk_Qty)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RM_PROCESS_LOSS_DETAIL", OMInsertOrUpdate.Insert, "", trans)
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

