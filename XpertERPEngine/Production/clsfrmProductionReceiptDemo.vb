'-===========created by Monika=28/08/2014
Imports common
Imports System.Data.SqlClient

Public Class clsfrmProductionReceiptDemo
#Region "Variables"
    Public docno As String = Nothing
    Public docdate As Date = Nothing
    Public description As String = Nothing
    Public comment As String = Nothing
    Public recvno As String = Nothing
    Public recdate As Date = Nothing
    Public loc_code As String = Nothing
    Public loc_desc As String = Nothing
    Public recv_by As String = Nothing
    Public recv_name As String = Nothing
    Public main_icode As String = Nothing
    Public main_iname As String = Nothing
    Public build_qty As Decimal = Nothing
    Public is_post As String = Nothing
    Public arrItem As List(Of clsfrmProductionReceiptDemo_Detail) = Nothing
    Public arrMain As List(Of clsfrmProductionRecieptDetail) = Nothing
#End Region

    Public Shared Function GetFinder(ByVal whrCls As String, ByVal CurrCode As String, ByVal isButtonClicked As Boolean) As String
        Dim qry As String = "select TSPL_MF_PRINCIPLE_RECEIPT_HEAD.doc_no as [Code],TSPL_MF_PRINCIPLE_RECEIPT_HEAD.doc_date as [Date],TSPL_MF_PRINCIPLE_RECEIPT_HEAD.Description,TSPL_MF_PRINCIPLE_RECEIPT_HEAD.Comment,TSPL_MF_PRINCIPLE_RECEIPT_HEAD.Against_Receipt_No as [Receipt No],TSPL_MF_RECEIPT.RECEIPT_DATE as [Receipt Date],TSPL_MF_RECEIPT.LOCATION_CODE as [Location],TSPL_MF_RECEIPT.RECEIVED_BY as [Received By],(case when TSPL_MF_PRINCIPLE_RECEIPT_HEAD.status='1' then'Approved' else 'Pending' end) as [Status] from TSPL_MF_PRINCIPLE_RECEIPT_HEAD left outer join TSPL_MF_RECEIPT on TSPL_MF_RECEIPT.RECEIPT_CODE=TSPL_MF_PRINCIPLE_RECEIPT_HEAD.Against_Receipt_No "
        Dim str As String = ""
        str = clsCommon.ShowSelectForm("RCPFND", qry, "Code", whrCls, CurrCode, "Code", isButtonClicked)

        Return str
    End Function

    Public Shared Function SaveData(ByVal isNewEntry As Boolean, ByVal isPost As Boolean, ByVal obj As clsfrmProductionReceiptDemo, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim coll As New Hashtable()
            Dim isSaved As Boolean = True

            If isNewEntry Then
                obj.docno = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(obj.docdate, "dd/MMM/yyyy"), clsDocType.ProductionMapping, "", "")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionSTD, clsUserMgtCode.frmProductionReceiptDemo, obj.loc_code, obj.docdate, trans)
            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Doc_No", obj.docno)
            clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.docdate, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.loc_code)
            clsCommon.AddColumnsForChange(coll, "Description", obj.description)
            clsCommon.AddColumnsForChange(coll, "Comment", obj.comment)
            clsCommon.AddColumnsForChange(coll, "Against_Receipt_No", obj.recvno)
            clsCommon.AddColumnsForChange(coll, "Status", obj.is_post)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_PRINCIPLE_RECEIPT_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_PRINCIPLE_RECEIPT_HEAD", OMInsertOrUpdate.Update, " Doc_no='" + obj.docno + "'", trans)
            End If

            isSaved = isSaved AndAlso clsfrmProductionRecieptDetail.SaveData(obj.arrMain, obj.loc_code, obj.docno, isPost, obj.recvno, trans)
            ' isSaved = isSaved AndAlso clsfrmProductionReceiptDemo_Detail.SaveData(obj.arrItem, isPost, obj.recvno, obj.loc_code, obj.docno, trans)


            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsfrmProductionReceiptDemo
        Try
            Dim obj As New clsfrmProductionReceiptDemo()
            obj.arrItem = New List(Of clsfrmProductionReceiptDemo_Detail)

            Dim qry As String = "select * from TSPL_MF_PRINCIPLE_RECEIPT_HEAD"

            Select Case NavType
                Case NavigatorType.Current
                    qry += " where doc_no='" + strCode + "'"
                Case NavigatorType.First
                    qry += " where doc_no in (select min(doc_no) from TSPL_MF_PRINCIPLE_RECEIPT_HEAD)"
                Case NavigatorType.Last
                    qry += " where doc_no in (select max(doc_no) from TSPL_MF_PRINCIPLE_RECEIPT_HEAD)"
                Case NavigatorType.Next
                    qry += " where doc_no in (select min(doc_no) from TSPL_MF_PRINCIPLE_RECEIPT_HEAD where doc_no>'" + strCode + "')"
                Case NavigatorType.Previous
                    qry += " where doc_no in (select max(doc_no) from TSPL_MF_PRINCIPLE_RECEIPT_HEAD where doc_no<'" + strCode + "')"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.docno = clsCommon.myCstr(dt.Rows(0)("doc_no"))
                obj.docdate = clsCommon.myCDate(dt.Rows(0)("doc_date"))
                obj.description = clsCommon.myCstr(dt.Rows(0)("description"))
                obj.comment = clsCommon.myCstr(dt.Rows(0)("comment"))
                obj.recvno = clsCommon.myCstr(dt.Rows(0)("Against_Receipt_No"))
                obj.recdate = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select receipt_date from tspl_mf_receipt where receipt_code='" + obj.recvno + "'"))
                obj.loc_code = clsCommon.myCstr(dt.Rows(0)("location_code"))
                obj.loc_desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_desc from tspl_location_master where location_code='" + obj.loc_code + "'"))
                obj.recv_by = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select received_by from tspl_mf_receipt where receipt_code='" + obj.recvno + "'"))
                obj.recv_name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Emp_Name from tspl_employee_master where emp_code='" + obj.recv_by + "'"))
                obj.is_post = clsCommon.myCstr(dt.Rows(0)("status"))

                Dim dt1 As DataTable = New DataTable()
                '' LoadData for child by getdatachild function
                'qry = "select * from TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL where doc_no='" + obj.docno + "'"
                'Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)

                'If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                '    For Each dr As DataRow In dt1.Rows
                '        Dim objtr As New clsfrmProductionReceiptDemo_Detail()

                '        objtr.sno = CInt(dr("sno"))
                '        objtr.main_icode = clsCommon.myCstr(dr("Main_Item_Code"))
                '        objtr.main_bomcode = clsCommon.myCstr(dr("bom_code"))
                '        objtr.main_issueno = clsCommon.myCstr(dr("issue_code"))
                '        objtr.main_iname = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_desc from tspl_item_master where item_code='" + objtr.main_icode + "'"))
                '        objtr.main_serial_no = clsCommon.myCstr(dr("Main_Serial_No"))
                '        objtr.item_code = clsCommon.myCstr(dr("item_code"))
                '        objtr.iname = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_desc from tspl_item_master where item_code='" + objtr.item_code + "'"))
                '        objtr.unit = clsCommon.myCstr(dr("unit_code"))
                '        objtr.serial_no = clsCommon.myCstr(dr("serial_no"))
                '        objtr.rec_qty = clsCommon.myCdbl(dr("rec_qty"))
                '        objtr.remarks = clsCommon.myCstr(dr("remarks"))
                '        objtr.is_principle = clsCommon.myCstr(dr("Is_principle"))

                '        obj.arrItem.Add(objtr)

                '    Next
                'End If


                obj.arrMain = New List(Of clsfrmProductionRecieptDetail)()
                qry = "select * from TSPL_MF_PRINCIPLE_RECEIPT_DETAIL where doc_no='" + obj.docno + "'"
                dt1 = New DataTable()
                dt1 = clsDBFuncationality.GetDataTable(qry)

                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For Each dr As DataRow In dt1.Rows
                        Dim objtr As New clsfrmProductionRecieptDetail()

                        objtr.sno = CInt(dr("sno"))
                        objtr.bom_code = clsCommon.myCstr(dr("bom_code"))
                        objtr.icode = clsCommon.myCstr(dr("Main_Item_Code"))
                        objtr.iname = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_desc from tspl_item_master where item_code='" + objtr.icode + "'"))
                        objtr.qty = clsCommon.myCstr(dr("Bulid_Qty"))
                        objtr.remarks = clsCommon.myCstr(dr("Remarks"))
                        objtr.serialno = clsCommon.myCstr(dr("Serial_No"))
                        objtr.unit = clsCommon.myCstr(dr("Unit_Code"))
                        objtr.IsSelect = clsCommon.myCstr(dr("IsSelect"))
                        '' New Array for child items 
                        objtr.arrSrItem = clsfrmProductionReceiptDemo.GetChildData(obj.docno, objtr.icode, objtr.serialno)
                        objtr.arrPrinciItem = clsfrmProductionReceiptDemo.GetPrinciData(obj.docno, objtr.icode, objtr.serialno)
                        ''
                        obj.arrMain.Add(objtr)

                    Next
                End If

            End If

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL where doc_no='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_MF_PRINCIPLE_RECEIPT_DETAIL where doc_no='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_MF_PRINCIPLE_RECEIPT_HEAD where doc_no='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function PostData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "update TSPL_MF_PRINCIPLE_RECEIPT_HEAD set status='1',modified_by='" + objCommonVar.CurrentUserCode + "' , modified_date='" + clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")) + "' where doc_no='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetChildData(ByVal strDocNo As String, ByVal strItemNo As String, ByVal strSrNo As String) As List(Of clsfrmProductionReceiptDemo_Detail)
        Dim qry As String = "select * from TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL where ISNULL(Is_principle,'')<>'1' AND doc_no='" + strDocNo + "' AND Main_Item_Code='" + strItemNo + "' AND Main_Serial_No='" + strSrNo + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim Arr As List(Of clsfrmProductionReceiptDemo_Detail) = Nothing
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            Arr = New List(Of clsfrmProductionReceiptDemo_Detail)
            For Each dr As DataRow In dt.Rows
                Dim objTr As clsfrmProductionReceiptDemo_Detail = New clsfrmProductionReceiptDemo_Detail()
                objTr.sno = CInt(dr("sno"))
                objTr.main_icode = clsCommon.myCstr(dr("Main_Item_Code"))
                objTr.main_iname = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_desc from tspl_item_master where item_code='" + objTr.main_icode + "'"))
                objTr.main_bomcode = clsCommon.myCstr(dr("bom_code"))
                objTr.main_serial_no = clsCommon.myCstr(dr("Main_Serial_No"))
                objTr.item_code = clsCommon.myCstr(dr("item_code"))
                objTr.iname = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_desc from tspl_item_master where item_code='" + objTr.item_code + "'"))
                objTr.unit = clsCommon.myCstr(dr("unit_code"))
                objTr.serial_no = clsCommon.myCstr(dr("serial_no"))
                objTr.rec_qty = clsCommon.myCdbl(dr("rec_qty"))
                objTr.remarks = clsCommon.myCstr(dr("remarks"))
                objTr.is_principle = clsCommon.myCstr(dr("Is_principle"))
                objTr.main_issueno = clsCommon.myCstr(dr("Issue_Code"))
                Arr.Add(objTr)
            Next
        End If
        Return Arr
    End Function
    Public Shared Function GetPrinciData(ByVal strDocNo As String, ByVal strItemNo As String, ByVal strSrNo As String) As List(Of clsfrmProductionReceiptDemo_Detail)
        Dim qry As String = "select * from TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL where ISNULL(Is_principle,'')='1' AND doc_no='" + strDocNo + "' AND Main_Item_Code='" + strItemNo + "' AND Main_Serial_No='" + strSrNo + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim Arr As List(Of clsfrmProductionReceiptDemo_Detail) = Nothing
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            Arr = New List(Of clsfrmProductionReceiptDemo_Detail)
            For Each dr As DataRow In dt.Rows
                Dim objTr As clsfrmProductionReceiptDemo_Detail = New clsfrmProductionReceiptDemo_Detail()
                objTr.sno = CInt(dr("sno"))
                objTr.main_icode = clsCommon.myCstr(dr("Main_Item_Code"))
                objTr.main_iname = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_desc from tspl_item_master where item_code='" + objTr.main_icode + "'"))
                objTr.main_bomcode = clsCommon.myCstr(dr("bom_code"))
                objTr.main_serial_no = clsCommon.myCstr(dr("Main_Serial_No"))
                objTr.item_code = clsCommon.myCstr(dr("item_code"))
                objTr.iname = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_desc from tspl_item_master where item_code='" + objTr.item_code + "'"))
                objTr.unit = clsCommon.myCstr(dr("unit_code"))
                objTr.serial_no = clsCommon.myCstr(dr("serial_no"))
                objTr.rec_qty = clsCommon.myCdbl(dr("rec_qty"))
                objTr.remarks = clsCommon.myCstr(dr("remarks"))
                objTr.is_principle = clsCommon.myCstr(dr("Is_principle"))
                objTr.main_issueno = clsCommon.myCstr(dr("Issue_Code"))
                Arr.Add(objTr)
            Next
        End If
        Return Arr
    End Function
End Class


Public Class clsfrmProductionReceiptDemo_Detail
#Region "Variables"
    Public sno As Integer = Nothing
    Public item_code As String = Nothing
    Public iname As String = Nothing
    Public unit As String = Nothing
    Public serial_no As String = Nothing
    Public rec_qty As Decimal = Nothing
    Public remarks As String = Nothing
    Public is_principle As String = Nothing
    Public main_icode As String = Nothing
    Public main_iname As String = Nothing
    Public main_serial_no As String = Nothing
    Public main_bomcode As String = Nothing
    Public main_issueno As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal arr As List(Of clsfrmProductionReceiptDemo_Detail), ByVal ispost As Boolean, ByVal recpt_no As String, ByVal locCode As String, ByVal DocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim coll As New Hashtable()
            Dim isSaved As Boolean = True
            Dim qry As String = String.Empty
            'Dim qry As String = "delete from TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL where doc_no='" + DocNo + "' AND main_serial_no='" + +"'"
            'isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim RowCount As Integer = 0
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsfrmProductionReceiptDemo_Detail In arr
                    coll = New Hashtable()
                    RowCount += 1
                    If RowCount = 1 AndAlso objtr.is_principle <> "1" Then
                        qry = "delete from TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL where doc_no='" + DocNo + "' AND main_serial_no='" + objtr.main_serial_no + "'"
                        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If

                    clsCommon.AddColumnsForChange(coll, "Doc_No", DocNo)
                    clsCommon.AddColumnsForChange(coll, "Sno", objtr.sno)
                    clsCommon.AddColumnsForChange(coll, "Location_Code", locCode)
                    clsCommon.AddColumnsForChange(coll, "Main_Item_Code", objtr.main_icode)
                    clsCommon.AddColumnsForChange(coll, "Main_Serial_No", objtr.main_serial_no)
                    clsCommon.AddColumnsForChange(coll, "bom_code", objtr.main_bomcode)
                    clsCommon.AddColumnsForChange(coll, "issue_code", objtr.main_issueno)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.item_code)
                    clsCommon.AddColumnsForChange(coll, "unit_code", objtr.unit)
                    clsCommon.AddColumnsForChange(coll, "Rec_Qty", objtr.rec_qty)
                    clsCommon.AddColumnsForChange(coll, "Serial_No", objtr.serial_no)
                    clsCommon.AddColumnsForChange(coll, "Remarks", objtr.remarks)
                    clsCommon.AddColumnsForChange(coll, "Is_principle", objtr.is_principle)

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL", OMInsertOrUpdate.Insert, "", trans)

                    If ispost AndAlso objtr.is_principle = "1" Then
                        qry = "update tspl_serial_item set auto_sr_no='" + objtr.serial_no + "' where document_type='Production' and document_code='" + recpt_no + "' and item_code='" + objtr.main_icode + "' and location_code='" + locCode + "'"
                        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

                        qry = "update TSPL_MF_PRINCIPLE_RECEIPT_DETAIL set serial_no='" + objtr.serial_no + "' where doc_no='" + DocNo + "' and main_item_code='" + objtr.main_icode + "' and location_code='" + locCode + "' and bom_code='" + objtr.main_bomcode + "'"
                        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If

                Next
            End If

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class

Public Class clsfrmProductionRecieptDetail
#Region "Variables"
    Public sno As Integer = Nothing
    Public icode As String = Nothing
    Public iname As String = Nothing
    Public qty As Decimal = Nothing
    Public unit As String = Nothing
    Public serialno As String = Nothing
    Public remarks As String = Nothing
    Public bom_code As String = Nothing
    Public IsSelect As Integer = 0
    Public arrSrItem As List(Of clsfrmProductionReceiptDemo_Detail) = Nothing
    Public arrPrinciItem As List(Of clsfrmProductionReceiptDemo_Detail) = Nothing
#End Region

    Public Shared Function SaveData(ByVal arr As List(Of clsfrmProductionRecieptDetail), ByVal locCode As String, ByVal DocNo As String, ByVal isPost As Boolean, ByVal RcvNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim coll As New Hashtable()
            Dim isSaved As Boolean = True

            Dim qry As String = "delete from TSPL_MF_PRINCIPLE_RECEIPT_DETAIL where doc_no='" + DocNo + "' "
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim RowCount As Integer = 0
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsfrmProductionRecieptDetail In arr
                    coll = New Hashtable()
                   
                    clsCommon.AddColumnsForChange(coll, "Doc_No", DocNo)
                    clsCommon.AddColumnsForChange(coll, "Sno", objtr.sno)
                    clsCommon.AddColumnsForChange(coll, "Location_Code", locCode)
                    clsCommon.AddColumnsForChange(coll, "bom_Code", objtr.bom_code)
                    clsCommon.AddColumnsForChange(coll, "Main_Item_Code", objtr.icode)
                    clsCommon.AddColumnsForChange(coll, "Serial_No", objtr.serialno)
                    clsCommon.AddColumnsForChange(coll, "Bulid_Qty", objtr.qty)
                    clsCommon.AddColumnsForChange(coll, "Unit_Code", objtr.unit)
                    clsCommon.AddColumnsForChange(coll, "Remarks", objtr.remarks)
                    clsCommon.AddColumnsForChange(coll, "IsSelect", objtr.IsSelect)

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_PRINCIPLE_RECEIPT_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                    isSaved = isSaved AndAlso clsfrmProductionReceiptDemo_Detail.SaveData(objtr.arrSrItem, isPost, RcvNo, locCode, DocNo, trans)
                    isSaved = isSaved AndAlso clsfrmProductionReceiptDemo_Detail.SaveData(objtr.arrPrinciItem, isPost, RcvNo, locCode, DocNo, trans)

                Next
            End If

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class