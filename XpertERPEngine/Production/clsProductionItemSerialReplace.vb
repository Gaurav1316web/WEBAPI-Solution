'===============created by Monika-===================01/09/2014
Imports common
Imports System.Data.SqlClient

Public Class clsProductionItemSerialReplace
#Region "Variables"
    Public docno As String = Nothing
    Public docdate As Date = Nothing
    Public Descrptn As String = Nothing
    Public loc_code As String = Nothing
    Public loc_desc As String = Nothing
    Public is_post As String = Nothing

    Public Arr As List(Of clsProductionItemSerialReplace_Prod) = Nothing
    Public Arr_Other As List(Of clsProductionItemSerialReplace_Detail) = Nothing
#End Region

    Public Shared Function GetFinder(ByVal whrCls As String, ByVal CurrCode As String, ByVal isButtonClicked As Boolean) As String
        Dim qry As String = "select TSPL_MF_PRINCIPLE_CHANGE_SERIALIZED_HEAD.doc_no as Code,TSPL_MF_PRINCIPLE_CHANGE_SERIALIZED_HEAD.doc_date as [Date],TSPL_MF_PRINCIPLE_CHANGE_SERIALIZED_HEAD.Description,TSPL_MF_PRINCIPLE_CHANGE_SERIALIZED_HEAD.location_code as [Location],(case when TSPL_MF_PRINCIPLE_CHANGE_SERIALIZED_HEAD.status='1' then 'Approved' else 'Pending' end) as Status from TSPL_MF_PRINCIPLE_CHANGE_SERIALIZED_HEAD "
        Dim str As String = ""

        str = clsCommon.ShowSelectForm("RVSFND", qry, "Code", whrCls, CurrCode, "Code", isButtonClicked)

        Return str
    End Function

    Public Shared Function SaveData(ByVal obj As clsProductionItemSerialReplace, ByVal isNewEntry As Boolean, ByVal ispost As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            Dim coll As New Hashtable()

            If isNewEntry Then
                obj.docno = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(obj.docdate, "dd/MMM/yyyy"), clsDocType.SerializedReplace, "", "")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Standard Production", "Replace Serializing Entry", obj.loc_code, obj.docdate, trans)
            clsCommon.AddColumnsForChange(coll, "Comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Doc_No", obj.docno)
            clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.docdate, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Description", obj.Descrptn)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.loc_code)
            clsCommon.AddColumnsForChange(coll, "Status", obj.is_post)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_PRINCIPLE_CHANGE_SERIALIZED_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_PRINCIPLE_CHANGE_SERIALIZED_HEAD", OMInsertOrUpdate.Update, " doc_no='" + obj.docno + "'", trans)
            End If

            isSaved = isSaved AndAlso clsProductionItemSerialReplace_Prod.SaveData(obj.docno, obj.Arr, obj.loc_code, trans)
            isSaved = isSaved AndAlso clsProductionItemSerialReplace_Detail.SaveData(obj.docno, obj.Arr_Other, obj.loc_code, ispost, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsProductionItemSerialReplace
        Try
            Dim obj As New clsProductionItemSerialReplace()
            obj.Arr = New List(Of clsProductionItemSerialReplace_Prod)
            obj.Arr_Other = New List(Of clsProductionItemSerialReplace_Detail)

            Dim qry As String = "select * from TSPL_MF_PRINCIPLE_CHANGE_SERIALIZED_HEAD where 2=2"

            Select Case NavType
                Case NavigatorType.Current
                    qry += " and doc_no ='" + strCode + "'"
                Case NavigatorType.First
                    qry += " and doc_no in (select min(doc_no) from TSPL_MF_PRINCIPLE_CHANGE_SERIALIZED_HEAD)"
                Case NavigatorType.Last
                    qry += " and doc_no in (select max(doc_no) from TSPL_MF_PRINCIPLE_CHANGE_SERIALIZED_HEAD)"
                Case NavigatorType.Next
                    qry += " and doc_no in (select min(doc_no) from TSPL_MF_PRINCIPLE_CHANGE_SERIALIZED_HEAD where doc_no>'" + strCode + "')"
                Case NavigatorType.Previous
                    qry += " and doc_no in (select max(doc_no) from TSPL_MF_PRINCIPLE_CHANGE_SERIALIZED_HEAD where doc_no<'" + strCode + "')"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.docno = clsCommon.myCstr(dt.Rows(0)("doc_no"))
                obj.docdate = clsCommon.myCDate(dt.Rows(0)("Doc_Date"))
                obj.Descrptn = clsCommon.myCstr(dt.Rows(0)("Description"))
                obj.loc_code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
                obj.loc_desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_desc from tspl_location_master where location_code='" + obj.loc_code + "'"))
                obj.is_post = clsCommon.myCstr(dt.Rows(0)("Status"))

                '===============prod. item====================
                qry = "select * from TSPL_MF_PRINCIPLE_CHANGE_SERIALIZED_PROD_DETAIL where doc_no='" + obj.docno + "'"
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)

                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For Each dr As DataRow In dt1.Rows
                        Dim objtr As New clsProductionItemSerialReplace_Prod()

                        objtr.sno = CInt(dr("Sno"))
                        objtr.icode = clsCommon.myCstr(dr("Main_Item_Code"))
                        objtr.iname = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_desc from tspl_item_master where item_code='" + objtr.icode + "'"))
                        objtr.unit = clsCommon.myCstr(dr("Unit_code"))
                        objtr.serialno = clsCommon.myCstr(dr("Serial_No"))
                        objtr.remarks = clsCommon.myCstr(dr("Remarks"))
                        objtr.bomcode = clsCommon.myCstr(dr("bom_code"))

                        obj.Arr.Add(objtr)
                    Next
                End If

                '=========================raw item=======================
                qry = "select * from TSPL_MF_PRINCIPLE_CHANGE_SERIALIZED_ITEM_DETAIL where doc_no='" + obj.docno + "'"
                dt1 = New DataTable()
                dt1 = clsDBFuncationality.GetDataTable(qry)

                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For Each dr As DataRow In dt1.Rows
                        Dim objtr As New clsProductionItemSerialReplace_Detail()

                        objtr.sno = CInt(dr("Sno"))
                        objtr.main_icode = clsCommon.myCstr(dr("Main_Item_Code"))
                        objtr.main_iname = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_desc from tspl_item_master where item_code='" + objtr.main_icode + "'"))
                        objtr.main_Serial_No = clsCommon.myCstr(dr("Main_Serial_No"))
                        objtr.icode = clsCommon.myCstr(dr("Main_Item_Code"))
                        objtr.iname = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_desc from tspl_item_master where item_code='" + objtr.icode + "'"))
                        objtr.unit = clsCommon.myCstr(dr("Unit_code"))
                        objtr.serialno = clsCommon.myCstr(dr("Old_Serial_No"))
                        objtr.New_serialno = clsCommon.myCstr(dr("New_Serial_No"))
                        objtr.is_principle = clsCommon.myCstr(dr("Is_principle"))
                        objtr.remarks = clsCommon.myCstr(dr("Remarks"))
                        objtr.Issuecode = clsCommon.myCstr(dr("issue_code"))
                        objtr.Bomcode = clsCommon.myCstr(dr("bom_code"))

                        obj.Arr_Other.Add(objtr)
                    Next
                End If
                '=========================================

            End If

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_MF_PRINCIPLE_CHANGE_SERIALIZED_ITEM_DETAIL where doc_no='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_MF_PRINCIPLE_CHANGE_SERIALIZED_PROD_DETAIL where doc_no='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_MF_PRINCIPLE_CHANGE_SERIALIZED_HEAD where doc_no='" + strCode + "'"
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
            Dim qry As String = "update TSPL_MF_PRINCIPLE_CHANGE_SERIALIZED_HEAD set status='1',modified_by='" + objCommonVar.CurrentUserCode + "',modified_date='" + clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")) + "' where doc_no='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

Public Class clsProductionItemSerialReplace_Prod
#Region "Variables"
    Public sno As Integer = Nothing
    Public icode As String = Nothing
    Public iname As String = Nothing
    Public unit As String = Nothing
    Public serialno As String = Nothing
    Public remarks As String = Nothing
    Public bomcode As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strcode As String, ByVal arr As List(Of clsProductionItemSerialReplace_Prod), ByVal LocCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            Dim coll As New Hashtable()

            Dim qry As String = "delete from TSPL_MF_PRINCIPLE_CHANGE_SERIALIZED_PROD_DETAIL where doc_no='" + strcode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsProductionItemSerialReplace_Prod In arr

                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Doc_No", strcode)
                    clsCommon.AddColumnsForChange(coll, "Sno", objtr.sno)
                    clsCommon.AddColumnsForChange(coll, "Location_Code", LocCode)
                    clsCommon.AddColumnsForChange(coll, "Main_Item_Code", objtr.icode)
                    clsCommon.AddColumnsForChange(coll, "Unit_code", objtr.unit)
                    clsCommon.AddColumnsForChange(coll, "Serial_No", objtr.serialno)
                    clsCommon.AddColumnsForChange(coll, "Remarks", objtr.remarks)
                    clsCommon.AddColumnsForChange(coll, "BOM_Code", objtr.bomcode)

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_PRINCIPLE_CHANGE_SERIALIZED_PROD_DETAIL", OMInsertOrUpdate.Insert, "", trans)

                Next
            End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

Public Class clsProductionItemSerialReplace_Detail
#Region "Variables"
    Public sno As Integer = Nothing
    Public main_icode As String = Nothing
    Public main_iname As String = Nothing
    Public icode As String = Nothing
    Public iname As String = Nothing
    Public unit As String = Nothing
    Public serialno As String = Nothing
    Public New_serialno As String = Nothing
    Public is_principle As String = Nothing
    Public remarks As String = Nothing
    Public main_Serial_No As String = Nothing
    Public Bomcode As String = Nothing
    Public Issuecode As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strcode As String, ByVal arr As List(Of clsProductionItemSerialReplace_Detail), ByVal LocCode As String, ByVal ispost As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            Dim coll As New Hashtable()

            Dim qry As String = "delete from TSPL_MF_PRINCIPLE_CHANGE_SERIALIZED_ITEM_DETAIL where doc_no='" + strcode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsProductionItemSerialReplace_Detail In arr

                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Doc_No", strcode)
                    clsCommon.AddColumnsForChange(coll, "Sno", objtr.sno)
                    clsCommon.AddColumnsForChange(coll, "Location_Code", LocCode)
                    clsCommon.AddColumnsForChange(coll, "Main_Item_Code", objtr.main_icode)
                    clsCommon.AddColumnsForChange(coll, "Main_Serial_No", objtr.main_Serial_No)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.icode)
                    clsCommon.AddColumnsForChange(coll, "Unit_code", objtr.unit)
                    clsCommon.AddColumnsForChange(coll, "Old_Serial_No", objtr.serialno)
                    clsCommon.AddColumnsForChange(coll, "New_Serial_No", objtr.New_serialno)
                    clsCommon.AddColumnsForChange(coll, "Remarks", objtr.remarks)
                    clsCommon.AddColumnsForChange(coll, "Is_principle", objtr.is_principle)
                    clsCommon.AddColumnsForChange(coll, "BOM_Code", objtr.Bomcode)
                    clsCommon.AddColumnsForChange(coll, "Issue_Code", objtr.Issuecode)

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_PRINCIPLE_CHANGE_SERIALIZED_ITEM_DETAIL", OMInsertOrUpdate.Insert, "", trans)

                    If ispost AndAlso objtr.is_principle = "1" AndAlso clsCommon.myLen(objtr.New_serialno) > 0 Then
                        qry = "update tspl_serial_item set auto_sr_no='" + objtr.New_serialno + "' where document_type='Production' and auto_sr_no='" + objtr.serialno + "' and item_code='" + objtr.main_icode + "' and location_code='" + LocCode + "'"
                        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

                        qry = "update TSPL_MF_PRINCIPLE_RECEIPT_DETAIL set serial_no='" + objtr.New_serialno + "' where doc_no='" + strcode + "' and main_item_code='" + objtr.main_icode + "' and location_code='" + LocCode + "' and bom_code='" + objtr.Bomcode + "'"
                        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

                        qry = "update TSPL_MF_PRINCIPLE_CHANGE_SERIALIZED_PROD_DETAIL set serial_no='" + objtr.New_serialno + "' where doc_no='" + strcode + "' and main_item_code='" + objtr.main_icode + "' and location_code='" + LocCode + "' and bom_code='" + objtr.Bomcode + "'"
                        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If
                Next
            End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class