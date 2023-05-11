''created by Monika 19/02/2015
Imports common
Imports System.Data.SqlClient

Public Class clsVendorItemQCMapping

#Region "variables"
    Public Document_Code As String = Nothing
    Public Doc_Date As Date = Nothing
    Public Description As String = Nothing
    Public Vendor_Code As String = Nothing
    Public Vendor_Name As String = Nothing

    Public Arr As List(Of clsVendorItemQCMappingDetail) = Nothing
#End Region

    Public Shared Function GetFinder(ByVal whrCls As String, ByVal strCurrCode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "select document_code as Code,doc_date as [Date],[Description],TSPL_QC_VENDOR_ITEM_MAPPING_HEAD.vendor_code as [Vendor Code],tspl_vendor_master.vendor_name as [Vendor Name] from TSPL_QC_VENDOR_ITEM_MAPPING_HEAD "
        qry += " left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_QC_VENDOR_ITEM_MAPPING_HEAD.vendor_code "
        str = clsCommon.myCstr(clsCommon.ShowSelectForm("VNDITMMAPFND", qry, "Code", whrCls, strCurrCode, "Code", isButtonClicked))

        Return str
    End Function

    Public Shared Function SaveData(ByVal obj As clsVendorItemQCMapping, ByVal isNewentry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewentry, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SaveData(ByVal obj As clsVendorItemQCMapping, ByVal isNewentry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim coll As New Hashtable()

            If isNewentry Then
                obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Doc_Date, clsDocType.QCVendorItemMapping, "", "")
            End If

            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
            clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.Doc_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code, True)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

            If isNewentry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_QC_VENDOR_ITEM_MAPPING_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_QC_VENDOR_ITEM_MAPPING_HEAD", OMInsertOrUpdate.Update, " Document_Code='" + obj.Document_Code + "'", trans)
            End If
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(obj.Document_Code), "TSPL_QC_VENDOR_ITEM_MAPPING_HEAD", "Document_Code", trans)
            clsVendorItemQCMappingDetail.SaveData(obj.Document_Code, obj.Arr, trans)

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(strCode, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL where document_code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_QC_VENDOR_ITEM_MAPPING_HEAD where document_code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As clsVendorItemQCMapping
        Dim objtr As New clsVendorItemQCMappingDetail()
        Dim dt As New DataTable()
        Dim dt1 As New DataTable()
        Try
            Dim obj As New clsVendorItemQCMapping()
            obj.Arr = New List(Of clsVendorItemQCMappingDetail)

            Dim qry As String = "select TSPL_QC_VENDOR_ITEM_MAPPING_HEAD.*,tspl_vendor_master.vendor_name from TSPL_QC_VENDOR_ITEM_MAPPING_HEAD left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_QC_VENDOR_ITEM_MAPPING_HEAD.vendor_code where 1=1 "

            Select Case NavType
                Case NavigatorType.Current
                    qry += " and TSPL_QC_VENDOR_ITEM_MAPPING_HEAD.document_code='" + strCode + "' "
                Case NavigatorType.First
                    qry += " and TSPL_QC_VENDOR_ITEM_MAPPING_HEAD.document_code in (select min(document_code) from TSPL_QC_VENDOR_ITEM_MAPPING_HEAD) "
                Case NavigatorType.Last
                    qry += " and TSPL_QC_VENDOR_ITEM_MAPPING_HEAD.document_code in (select max(document_code) from TSPL_QC_VENDOR_ITEM_MAPPING_HEAD) "
                Case NavigatorType.Next
                    qry += " and TSPL_QC_VENDOR_ITEM_MAPPING_HEAD.document_code in (select min(document_code) from TSPL_QC_VENDOR_ITEM_MAPPING_HEAD where document_code>'" + strCode + "') "
                Case NavigatorType.Previous
                    qry += " and TSPL_QC_VENDOR_ITEM_MAPPING_HEAD.document_code in (select max(document_code) from TSPL_QC_VENDOR_ITEM_MAPPING_HEAD where document_code<'" + strCode + "') "
            End Select
            dt = clsDBFuncationality.GetDataTable(qry, trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
                obj.Doc_Date = clsCommon.myCDate(dt.Rows(0)("Doc_Date"))
                obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
                obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
                obj.Vendor_Name = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))

                qry = "select TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL.*,tspl_item_master.item_desc,tspl_qc_log_sheet_master.description as parameter_Desc from TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL left outer join tspl_item_master on tspl_item_master.item_code=TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL.item_code "
                qry += " left outer join tspl_qc_log_sheet_master on tspl_qc_log_sheet_master.code=TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL.parameter_code where TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL.document_code='" + obj.Document_Code + "' and tspl_qc_log_sheet_master.trans_id='STANDARD' "
                dt1 = clsDBFuncationality.GetDataTable(qry, trans)

                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For Each dr As DataRow In dt1.Rows
                        objtr = New clsVendorItemQCMappingDetail()

                        objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                        objtr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                        objtr.Line_No = clsCommon.myCstr(dr("Line_No"))
                        objtr.Nature = clsCommon.myCstr(dr("Nature"))
                        If clsCommon.CompairString(objtr.Nature, "R") = CompairStringResult.Equal Then
                            objtr.Nature = "Range"
                        End If
                        If clsCommon.CompairString(objtr.Nature, "B") = CompairStringResult.Equal Then
                            objtr.Nature = "Boolean"
                        End If
                        If clsCommon.CompairString(objtr.Nature, "A") = CompairStringResult.Equal Then
                            objtr.Nature = "Alphanumeric"
                        End If
                        If clsCommon.CompairString(objtr.Nature, "M") = CompairStringResult.Equal Then
                            objtr.Nature = "Mannual Input"
                        End If
                        objtr.Parameter_Code = clsCommon.myCstr(dr("Parameter_Code"))
                        objtr.Parameter_Desc = clsCommon.myCstr(dr("Parameter_Desc"))
                        objtr.Remarks = clsCommon.myCstr(dr("Remarks"))
                        objtr.Unit_Code = clsCommon.myCstr(dr("Unit_Code"))

                        obj.Arr.Add(objtr)
                    Next
                End If 'dt1 cond.
            End If

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            objtr = Nothing
            dt = Nothing
        End Try
    End Function
End Class

Public Class clsVendorItemQCMappingDetail
#Region "vriables"
    Public Line_No As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Unit_Code As String = Nothing
    Public Parameter_Code As String = Nothing
    Public Parameter_Desc As String = Nothing
    Public Nature As String = Nothing
    Public Remarks As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal arr As List(Of clsVendorItemQCMappingDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim coll As New Hashtable()

            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL where document_code='" + strCode + "'", trans)

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsVendorItemQCMappingDetail In arr
                    coll = New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "document_code", strCode)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.Item_Code, True)
                    clsCommon.AddColumnsForChange(coll, "Line_No", objtr.Line_No)
                    clsCommon.AddColumnsForChange(coll, "Nature", objtr.Nature)
                    clsCommon.AddColumnsForChange(coll, "Parameter_Code", objtr.Parameter_Code, True)
                    clsCommon.AddColumnsForChange(coll, "Remarks", objtr.Remarks)
                    clsCommon.AddColumnsForChange(coll, "Unit_Code", objtr.Unit_Code, True)

                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL", "document_code", trans)
                Next
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
