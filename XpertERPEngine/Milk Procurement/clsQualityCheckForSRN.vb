Imports common
Imports System.Data.SqlClient

Public Class clsQualityCheckForSRNHead
#Region "variables"
    Public Document_Code As String = Nothing
    Public partial_rejected As Int16 = 0
    Public Document_Date As Date = Nothing
    Public Description As String = Nothing
    Public Vendor_Code As String = Nothing
    Public Vendor_Name As String = Nothing
    Public Bill_To_location As String = Nothing
    Public Location_Desc As String = Nothing
    Public Gate_Entry_No As String = Nothing
    Public Gate_Entry_Date As Date = Nothing
    Public SRN_Type As String = Nothing
    Public Item_Type As String = Nothing
    Public QC_Status As String = Nothing
    Public Posted As Integer = Nothing
    Public QC_Type As String = Nothing
    Public Template_Remarks As String = Nothing
    Public Template_Status As String = Nothing

    Public Arr As List(Of clsQualityCheckDetail) = Nothing
    Public Arr_item As List(Of clsQualityCheckForSRNDetail) = Nothing
    Public Arr_MRN As List(Of clsQualityCheckForSRN_MRNDetail) = Nothing
#End Region

    Public Shared Function Getfinder(ByVal whrCls As String, ByVal strCurrCode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "select TSPL_QC_CHECK_HEAD.document_code as [Code],TSPL_QC_CHECK_HEAD.document_date as [Document Date],TSPL_QC_CHECK_HEAD.qc_type as [QC Type],TSPL_QC_CHECK_HEAD.Description,(case when TSPL_QC_CHECK_HEAD.posted=1 then 'Posted' else 'Unposted' end) as Posted,TSPL_QC_CHECK_HEAD.vendor_code as [Vendor],tspl_vendor_master.vendor_name as [Vendor Name],tspl_location_master.location_desc as [Bill to Location],TSPL_QC_CHECK_HEAD.qc_status as [QC Status],stuff((select distinct  ',' + TSPL_QC_CHECK_DETAIL.MRN_No  from TSPL_QC_CHECK_DETAIL where TSPL_QC_CHECK_DETAIL.Document_Code = TSPL_QC_CHECK_HEAD.Document_Code    for xml path('')  ),1,1,'') [MRN No] 
                            ,stuff((select distinct  ',' + TSPL_MRN_Head.VehicleNo from TSPL_QC_CHECK_DETAIL
                            LEFT JOIN TSPL_MRN_Head ON TSPL_MRN_Head.MRN_NO=TSPL_QC_CHECK_DETAIL.MRN_NO
                            where TSPL_QC_CHECK_DETAIL.Document_Code = TSPL_QC_CHECK_HEAD.Document_Code    for xml path('')  ),1,1,'') [VehicleNo] 
                            ,stuff((select distinct  ',' + TSPL_ITEM_MASTER.Item_Desc from TSPL_QC_CHECK_DETAIL left outer join tspl_item_master on tspl_item_master.Item_Code=TSPL_QC_CHECK_DETAIL.Item_Code where TSPL_QC_CHECK_DETAIL.Document_Code = TSPL_QC_CHECK_HEAD.Document_Code    for xml path('')  ),1,1,'') [Item Name]
                            ,TSPL_QC_CHECK_HEAD.Gate_Entry_No as [Gate Entry No],convert(varchar, TSPL_QC_CHECK_HEAD.Gate_Entry_Date,103) as [Gate Entry Date]
                            ,TSPL_GRN_HEAD.Ref_No as [RAL No] 
                            ,TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code as [Weighment No]
                            ,convert(varchar,TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date,103) as [Weighment Date] "
        qry += " from TSPL_QC_CHECK_HEAD left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_QC_CHECK_HEAD.vendor_code left outer join tspl_location_master on tspl_location_master.location_code=TSPL_QC_CHECK_HEAD.bill_to_location 
                 left join TSPL_PO_WEIGHTMENT_HEAD on TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No=TSPL_QC_CHECK_HEAD.Gate_Entry_No
                 left join TSPL_GRN_HEAD on  TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No=TSPL_GRN_HEAD.GRN_No "
        str = clsCommon.myCstr(clsCommon.ShowSelectForm("QCMRNFND", qry, "Code", whrCls, strCurrCode, "TSPL_QC_CHECK_HEAD.document_date desc", isButtonClicked))

        Return str
    End Function

    Public Shared Function GetDeductionPers(ByVal Param_Code As String, ByVal Vendor_Code As String, ByVal Item_Code As String, ByVal Observed_Range As String, ByVal isNumeric As Boolean, ByVal isAlpha As Boolean, ByVal trans As SqlTransaction) As Double
        Dim qry As String = "select TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_Per from TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL left outer join TSPL_PARAMETER_RANGE_MASTER_QC on TSPL_PARAMETER_RANGE_MASTER_QC.qc_param_code=TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL.parameter_code and TSPL_PARAMETER_RANGE_MASTER_QC.trans_id='standard' left outer join TSPL_QC_LOG_SHEET_MASTER on TSPL_QC_LOG_SHEET_MASTER.code=TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL.parameter_code left outer join TSPL_QC_VENDOR_ITEM_MAPPING_HEAD on TSPL_QC_VENDOR_ITEM_MAPPING_HEAD.document_code=TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL.document_code"
        qry += " left outer join tspl_item_master on tspl_item_master.item_code=TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL.item_code where TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL.item_code in ('" + Item_Code + "') and TSPL_QC_VENDOR_ITEM_MAPPING_HEAD.vendor_code='" + Vendor_Code + "' and TSPL_QC_VENDOR_ITEM_MAPPING_DETAIL.parameter_code='" + Param_Code + "' "
        If Not isNumeric AndAlso Not isAlpha Then
            qry += " and TSPL_PARAMETER_RANGE_MASTER_QC.status='" + Observed_Range + "' "
        ElseIf isAlpha Then
            qry += " and TSPL_PARAMETER_RANGE_MASTER_QC.value1 like '%" + Observed_Range + "%' "
        ElseIf isNumeric Then
            qry += " and '" + Observed_Range + "' >= TSPL_PARAMETER_RANGE_MASTER_QC.lower_range and '" + Observed_Range + "' <= TSPL_PARAMETER_RANGE_MASTER_QC.upper_range "
        End If
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
    End Function

    Public Shared Function SaveData(ByVal obj As clsQualityCheckForSRNHead, ByVal isNewEntry As Boolean) As Boolean
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

    Public Shared Function SaveData(ByVal obj As clsQualityCheckForSRNHead, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim coll As New Hashtable()
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleQualityControl, clsUserMgtCode.frmQualityCheckForSRN, obj.Bill_To_location, obj.Document_Date, trans)

            If isNewEntry Then
                If clsCommon.CompairString(obj.QC_Type, "Incoming") = CompairStringResult.Equal Then
                    obj.Document_Code = clsCommon.myCstr(clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.QualityCheckForSRN, clsDocTransactionType.IncomingQualityCheck, obj.Bill_To_location))
                ElseIf clsCommon.CompairString(obj.QC_Type, "Outgoing") = CompairStringResult.Equal Then
                    obj.Document_Code = clsCommon.myCstr(clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.QualityCheckForSRN, clsDocTransactionType.OutgoingQualityCheck, obj.Bill_To_location))
                ElseIf clsCommon.CompairString(obj.QC_Type, "InProcess") = CompairStringResult.Equal Then
                    obj.Document_Code = clsCommon.myCstr(clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.QualityCheckForSRN, clsDocTransactionType.InprocessQualityCheck, obj.Bill_To_location))
                End If
            End If

            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "QC_Type", obj.QC_Type)
            clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code, True)
            clsCommon.AddColumnsForChange(coll, "Bill_To_location", obj.Bill_To_location, True)
            clsCommon.AddColumnsForChange(coll, "Gate_Entry_No", obj.Gate_Entry_No)
            If clsCommon.myLen(obj.Gate_Entry_No) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Gate_Entry_Date", clsCommon.GetPrintDate(obj.Gate_Entry_Date, "dd/MMM/yyyy"))
            End If
            clsCommon.AddColumnsForChange(coll, "SRN_Type", obj.SRN_Type)
            clsCommon.AddColumnsForChange(coll, "Item_Type", obj.Item_Type)
            clsCommon.AddColumnsForChange(coll, "QC_Status", obj.QC_Status)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Template_Remarks", obj.Template_Remarks)
            clsCommon.AddColumnsForChange(coll, "Template_Status", obj.Template_Status)
            clsCommon.AddColumnsForChange(coll, "partial_rejected", obj.partial_rejected)

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_QC_CHECK_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_QC_CHECK_HEAD", OMInsertOrUpdate.Update, " Document_Code='" + obj.Document_Code + "' and QC_Type='" + obj.QC_Type + "' ", trans)
            End If

            clsQualityCheckForSRNDetail.SaveData(obj.Document_Code, obj.Arr_item, trans)
            clsQualityCheckForSRN_MRNDetail.SaveData(obj.Document_Code, obj.Arr_MRN, trans)
            clsQualityCheckDetail.SaveData(obj.Document_Code, obj.Arr, trans)

            '===Sanjeet(03/01/2017) for notifiaction====
            Dim strNotificationOn As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_On from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmQualityCheckForSRN + "'", trans))
            If clsCommon.CompairString(strNotificationOn, "S") = CompairStringResult.Equal Then
                'Dim obj As New clsQualityCheckForSRNHead()
                'obj = clsQualityCheckForSRNHead.GetData(strCode, QC_Type, NavigatorType.Current, trans)
                CreateNotificationContentEMP(obj, trans)
            End If
            '=======================================
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            coll = Nothing
        End Try
    End Function

    Public Shared Function FullNameOfItemType(ByVal strItemType As String) As String
        Try
            Dim str As String = ""

            If clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                str = "Finished Goods"
            ElseIf clsCommon.CompairString(strItemType, "S") = CompairStringResult.Equal Then
                str = "Semi Finished Good"
            ElseIf clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                str = "Raw Material"
            ElseIf clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                str = "Other"
            ElseIf clsCommon.CompairString(strItemType, "A") = CompairStringResult.Equal Then
                str = "Asset"
            End If

            Return str
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function FullNameOfPurchaseOrderType(ByVal strPOType As String) As String
        Try
            Dim str As String = ""

            If clsCommon.CompairString(strPOType, "L") = CompairStringResult.Equal Then
                str = "Domestic"
            ElseIf clsCommon.CompairString(strPOType, "I") = CompairStringResult.Equal Then
                str = "Import"
            ElseIf clsCommon.CompairString(strPOType, "J") = CompairStringResult.Equal Then
                str = "Job Work"
            ElseIf clsCommon.CompairString(strPOType, "S") = CompairStringResult.Equal Then
                str = "Work Order(Service PO)"
            End If

            Return str
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function CodeOfItemType(ByVal strItemTypeName As String) As String
        Try
            Dim str As String = ""

            If clsCommon.CompairString(strItemTypeName, "Finished Goods") = CompairStringResult.Equal Then
                str = "F"
            ElseIf clsCommon.CompairString(strItemTypeName, "Semi Finished Good") = CompairStringResult.Equal Then
                str = "S"
            ElseIf clsCommon.CompairString(strItemTypeName, "Raw Material") = CompairStringResult.Equal Then
                str = "R"
            ElseIf clsCommon.CompairString(strItemTypeName, "Other") = CompairStringResult.Equal Then
                str = "O"
            ElseIf clsCommon.CompairString(strItemTypeName, "Asset") = CompairStringResult.Equal Then
                str = "A"
            End If

            Return str
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function CodeOfPurchaseOrderType(ByVal strPOTypeName As String) As String
        Try
            Dim str As String = ""

            If clsCommon.CompairString(strPOTypeName, "Domestic") = CompairStringResult.Equal Then
                str = "L"
            ElseIf clsCommon.CompairString(strPOTypeName, "Import") = CompairStringResult.Equal Then
                str = "I"
            ElseIf clsCommon.CompairString(strPOTypeName, "Job Work") = CompairStringResult.Equal Then
                str = "J"
            ElseIf clsCommon.CompairString(strPOTypeName, "Work Order(Service PO)") = CompairStringResult.Equal Then
                str = "S"
            End If

            Return str
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal QC_Type As String, ByVal NavType As NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As clsQualityCheckForSRNHead
        Return GetData(strCode, QC_Type, "", NavType, trans)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal QC_Type As String, ByVal ExtrWhr As String, ByVal NavType As NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As clsQualityCheckForSRNHead
        Dim dt As New DataTable()
        Dim dt1 As New DataTable()
        Dim objtr As New clsQualityCheckForSRNDetail()
        Dim obj_MRN As New clsQualityCheckForSRN_MRNDetail()
        Dim objtr_Detail As New clsQualityCheckDetail()
        Try
            Dim obj As New clsQualityCheckForSRNHead()
            obj.Arr_item = New List(Of clsQualityCheckForSRNDetail)
            obj.Arr_MRN = New List(Of clsQualityCheckForSRN_MRNDetail)
            obj.Arr = New List(Of clsQualityCheckDetail)

            'Dim qry As String = "select TSPL_QC_CHECK_HEAD.*,tspl_vendor_master.vendor_name,tspl_location_master.location_desc from TSPL_QC_CHECK_HEAD left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_QC_CHECK_HEAD.vendor_code left outer join tspl_location_master on tspl_location_master.location_code=TSPL_QC_CHECK_HEAD.bill_to_location where TSPL_QC_CHECK_HEAD.qc_type='" + QC_Type + "' "
            Dim qry As String = "select TSPL_QC_CHECK_HEAD.*,tspl_vendor_master.vendor_name,tspl_location_master.location_desc from TSPL_QC_CHECK_HEAD left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_QC_CHECK_HEAD.vendor_code left outer join tspl_location_master on tspl_location_master.location_code=TSPL_QC_CHECK_HEAD.bill_to_location "
            Dim whr As String = " where qc_type='" + QC_Type + "' " + ExtrWhr
            'Dim whr As String = "where " + ExtrWhr

            Select Case NavType
                Case NavigatorType.Current
                    ' qry += " and TSPL_QC_CHECK_HEAD.document_code='" + strCode + "'"
                    qry += " where TSPL_QC_CHECK_HEAD.document_code='" + strCode + "'"
                Case NavigatorType.First
                    'qry += " and TSPL_QC_CHECK_HEAD.document_code in (select min(document_code) from TSPL_QC_CHECK_HEAD " + whr + ")"
                    qry += " where TSPL_QC_CHECK_HEAD.document_code in (select min(document_code) from TSPL_QC_CHECK_HEAD " + whr + ")"
                Case NavigatorType.Last
                    'qry += " and TSPL_QC_CHECK_HEAD.document_code in (select max(document_code) from TSPL_QC_CHECK_HEAD " + whr + ")"
                    qry += " where TSPL_QC_CHECK_HEAD.document_code in (select max(document_code) from TSPL_QC_CHECK_HEAD " + whr + ")"
                Case NavigatorType.Next
                    ' qry += " and TSPL_QC_CHECK_HEAD.document_code in (select min(document_code) from TSPL_QC_CHECK_HEAD " + whr + " and document_code>'" + strCode + "')"
                    qry += "where TSPL_QC_CHECK_HEAD.document_code in (select min(document_code) from TSPL_QC_CHECK_HEAD " + whr + " and document_code>'" + strCode + "')"
                Case NavigatorType.Previous
                    'qry += " and TSPL_QC_CHECK_HEAD.document_code in (select max(document_code) from TSPL_QC_CHECK_HEAD " + whr + " and document_code<'" + strCode + "')"
                    qry += " where TSPL_QC_CHECK_HEAD.document_code in (select max(document_code) from TSPL_QC_CHECK_HEAD " + whr + " and document_code<'" + strCode + "')"
            End Select
            dt = clsDBFuncationality.GetDataTable(qry, trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
                obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
                obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
                obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
                obj.Vendor_Name = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
                obj.Bill_To_location = clsCommon.myCstr(dt.Rows(0)("Bill_To_location"))
                obj.Location_Desc = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
                obj.Gate_Entry_No = clsCommon.myCstr(dt.Rows(0)("Gate_Entry_No"))
                If clsCommon.myLen(obj.Gate_Entry_No) > 0 Then
                    obj.Gate_Entry_Date = clsCommon.myCDate(dt.Rows(0)("Gate_Entry_Date"))
                End If

                obj.Item_Type = clsCommon.myCstr(dt.Rows(0)("Item_Type"))
                obj.SRN_Type = clsCommon.myCstr(dt.Rows(0)("SRN_Type"))
                obj.QC_Status = clsCommon.myCstr(dt.Rows(0)("QC_Status"))
                obj.Posted = CInt(clsCommon.myCdbl(dt.Rows(0)("posted")))
                obj.Template_Remarks = clsCommon.myCstr(dt.Rows(0)("Template_Remarks"))
                obj.partial_rejected = Convert.ToInt16(dt.Rows(0)("partial_rejected"))



                qry = "select TSPL_QC_CHECK_SRN_DETAIL.*,tspl_item_master.item_desc,tspl_item_master.part_no,tspl_item_master.drawing_no,tspl_qc_log_sheet_master.description as param_desc from TSPL_QC_CHECK_SRN_DETAIL left outer join tspl_item_master on tspl_item_master.item_code=TSPL_QC_CHECK_SRN_DETAIL.item_code left outer join tspl_qc_log_sheet_master on tspl_qc_log_sheet_master.code=TSPL_QC_CHECK_SRN_DETAIL.qc_param_code and tspl_qc_log_sheet_master.trans_id='standard'"
                qry += " where TSPL_QC_CHECK_SRN_DETAIL.document_code='" + obj.Document_Code + "'"
                dt1 = clsDBFuncationality.GetDataTable(qry, trans)

                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For Each dr As DataRow In dt1.Rows
                        objtr = New clsQualityCheckForSRNDetail()

                        objtr.Document_Code = clsCommon.myCstr(dr("document_code"))
                        objtr.Line_no = CInt(clsCommon.myCstr(dr("Line_No")))
                        objtr.PO_No = clsCommon.myCstr(dr("PO_No"))
                        objtr.MRN_No = clsCommon.myCstr(dr("MRN_No"))
                        objtr.Row_Type = clsCommon.myCstr(dr("Row_Type"))
                        objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                        objtr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                        objtr.Unit_Code = clsCommon.myCstr(dr("Unit_Code"))
                        objtr.Drawing_No = clsCommon.myCstr(dr("Drawing_No"))
                        objtr.Part_No = clsCommon.myCstr(dr("Part_No"))
                        objtr.MRN_Qty = clsCommon.myCdbl(dr("MRN_Qty"))
                        objtr.QC_Param_Code = clsCommon.myCstr(dr("QC_Param_Code"))
                        objtr.Param_Desc = clsCommon.myCstr(dr("Param_Desc"))
                        objtr.Param_L_Range = clsCommon.myCdbl(dr("Param_L_Range"))
                        objtr.Param_U_Range = clsCommon.myCdbl(dr("Param_U_Range"))
                        objtr.Param_QC_Status = clsCommon.myCstr(dr("Param_QC_Status"))
                        objtr.Param_Status = clsCommon.myCstr(dr("Param_Status"))
                        objtr.Param_Value = clsCommon.myCstr(dr("Param_Value"))
                        objtr.Ok_Qty = clsCommon.myCdbl(dr("Ok_Qty"))
                        objtr.Reject_Qty = clsCommon.myCdbl(dr("Reject_Qty"))
                        objtr.Measured_1 = clsCommon.myCstr(dr("Measured_1"))
                        objtr.Measured_2 = clsCommon.myCstr(dr("Measured_2"))
                        objtr.Measured_3 = clsCommon.myCstr(dr("Measured_3"))
                        objtr.Measured_4 = clsCommon.myCstr(dr("Measured_4"))
                        objtr.Measured_5 = clsCommon.myCstr(dr("Measured_5"))
                        objtr.Measured_6 = clsCommon.myCstr(dr("Measured_6"))
                        objtr.Measured_7 = clsCommon.myCstr(dr("Measured_7"))
                        objtr.Measured_8 = clsCommon.myCstr(dr("Measured_8"))
                        objtr.Measured_9 = clsCommon.myCstr(dr("Measured_9"))
                        objtr.Measured_10 = clsCommon.myCstr(dr("Measured_10"))
                        objtr.Net_Measurement = clsCommon.myCstr(dr("Net_Measurement"))
                        objtr.Auto_Measured = clsCommon.myCstr(dr("Auto_Measured"))
                        objtr.Reject_Remarks = clsCommon.myCstr(dr("Reject_Remarks"))
                        objtr.Remarks = clsCommon.myCstr(dr("Remarks"))
                        objtr.InputData = clsCommon.myCdbl(dr("InputData"))
                        objtr.InputDataDeductionPer = clsCommon.myCdbl(dr("InputDataDeductionPer"))
                        objtr.Mandatory = CInt(dr("Mandatory"))
                        obj.Arr_item.Add(objtr)
                    Next
                End If 'dt1 cond.

                qry = "select TSPL_QC_CHECK_DETAIL.*,tspl_item_master.item_desc,tspl_item_master.part_no,tspl_item_master.drawing_no from TSPL_QC_CHECK_DETAIL left outer join tspl_item_master on tspl_item_master.item_code=TSPL_QC_CHECK_DETAIL.item_code "
                qry += " where TSPL_QC_CHECK_DETAIL.document_code='" + obj.Document_Code + "'"
                dt1 = clsDBFuncationality.GetDataTable(qry, trans)

                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For Each dr As DataRow In dt1.Rows
                        objtr_Detail = New clsQualityCheckDetail()
                        objtr_Detail.Document_Code = clsCommon.myCstr(dr("document_code"))
                        objtr_Detail.Line_No = CInt(clsCommon.myCstr(dr("Line_No")))
                        objtr_Detail.MRN_No = clsCommon.myCstr(dr("MRN_No"))
                        objtr_Detail.PO_No = clsCommon.myCstr(dr("PO_No"))
                        objtr_Detail.Row_Type = clsCommon.myCstr(dr("Row_Type"))
                        objtr_Detail.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                        objtr_Detail.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                        objtr_Detail.Unit_Code = clsCommon.myCstr(dr("Unit_Code"))
                        objtr_Detail.Drawing_No = clsCommon.myCstr(dr("Drawing_No"))
                        objtr_Detail.Part_No = clsCommon.myCstr(dr("Part_No"))
                        objtr_Detail.MRN_Qty = clsCommon.myCdbl(dr("MRN_Qty"))
                        objtr_Detail.Ok_Qty = clsCommon.myCdbl(dr("Ok_Qty"))
                        objtr_Detail.Reject_Qty = clsCommon.myCdbl(dr("Reject_Qty"))
                        objtr_Detail.Reject_Remarks = clsCommon.myCstr(dr("Reject_Remarks"))
                        objtr_Detail.Remarks = clsCommon.myCstr(dr("Remarks"))
                        objtr_Detail.QC_Status = clsCommon.myCstr(dr("QC_Status"))
                        objtr_Detail.Additional_Remarks = clsCommon.myCstr(dr("Additional_Remarks"))
                        obj.Arr.Add(objtr_Detail)
                    Next
                End If 'dt1 cond.

                qry = "select TSPL_QC_CHECK_MRN_DETAIL.*,tspl_mrn_head.description as mrn_desc,convert(varchar,tspl_mrn_head.MRN_Date,103) as MRN_Date,tspl_mrn_head.VehicleNo,tspl_item_master.item_desc,tspl_item_master.part_no,tspl_item_master.drawing_no from TSPL_QC_CHECK_MRN_DETAIL left outer join tspl_item_master on tspl_item_master.item_code=TSPL_QC_CHECK_MRN_DETAIL.item_code left outer join tspl_mrn_head on tspl_mrn_head.mrn_no=TSPL_QC_CHECK_MRN_DETAIL.mrn_no where TSPL_QC_CHECK_MRN_DETAIL.document_code='" + obj.Document_Code + "'"
                dt1 = New DataTable()
                dt1 = clsDBFuncationality.GetDataTable(qry, trans)

                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For Each dr As DataRow In dt1.Rows
                        obj_MRN = New clsQualityCheckForSRN_MRNDetail()

                        obj_MRN.Document_Code = clsCommon.myCstr(dr("Document_Code"))
                        obj_MRN.Status = CInt(clsCommon.myCstr(dr("Status")))
                        obj_MRN.Line_No = CInt(clsCommon.myCstr(dr("Line_No")))
                        obj_MRN.MRN_No = clsCommon.myCstr(dr("MRN_No"))

                        If dr("MRN_Date") IsNot DBNull.Value Then
                            obj_MRN.MRN_Date = clsCommon.myCDate(dr("MRN_Date"))
                        End If

                        obj_MRN.VehicleNo = clsCommon.myCstr(dr("VehicleNo"))
                        obj_MRN.MRN_Desc = clsCommon.myCstr(dr("MRN_Desc"))
                        obj_MRN.Row_Type = clsCommon.myCstr(dr("Row_Type"))
                        obj_MRN.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                        obj_MRN.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                        obj_MRN.Drawing_No = clsCommon.myCstr(dr("Drawing_No"))
                        obj_MRN.Part_No = clsCommon.myCstr(dr("Part_No"))
                        obj_MRN.Unit_Code = clsCommon.myCstr(dr("Unit_Code"))
                        obj_MRN.Qty = clsCommon.myCdbl(dr("Qty"))
                        obj_MRN.MRN_Type = clsCommon.myCstr(dr("MRN_Type"))

                        obj.Arr_MRN.Add(obj_MRN)
                    Next
                End If 'dt1 cond.

            End If

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            dt = Nothing
            dt1 = Nothing
            objtr = Nothing
            obj_MRN = Nothing
            objtr_Detail = Nothing
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal QC_Type As String) As Boolean
        Dim trans As SqlTransaction = Nothing
        Try
            DeleteData(strCode, QC_Type, trans)

            'trans.Commit()
            Return True
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal QC_Type As String, ByVal trans As SqlTransaction) As Boolean
        trans = clsDBFuncationality.GetTransactin()
        Try
            ''qc approval must be deleted.
            ' Dim qry As String = ""
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Document_Date,Bill_To_location from TSPL_QC_CHECK_HEAD where document_code='" + strCode + "'", trans)
            If dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleQualityControl, clsUserMgtCode.frmQualityCheckForSRN, clsCommon.myCstr(dt.Rows(0)("Bill_To_location")), clsCommon.myCDate(dt.Rows(0)("Document_Date")), trans)

            End If

            Dim obj As clsQualityCheckForSRNHead = clsQualityCheckForSRNHead.GetData(strCode, Nothing, NavigatorType.Current, trans)
            If (obj.Posted = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Posted Document")
            End If

            Dim qry1 As String = ""
            'Dim dt As DataTable = Nothing
            'strCode = obj.Document_Code
            'qry = "select distinct MRN_No from TSPL_MRN_DETAIL where GRN_Id ='" + strCode + "'"
            qry1 = "Select distinct SRN_No from TSPL_TENDER_PENALTY_DETAIL WHERE SRN_No IN (Select  SRN_No from TSPL_SRN_HEAD WHERE SRN_No IN (SELECT DISTINCT SRN_Id  FROM TSPL_QC_CHECK_MRN_DETAIL WHERE Document_Code='" + strCode + "'))"
            dt = clsDBFuncationality.GetDataTable(qry1, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsCommon.MyMessageBoxShow("First Delete RAL Penalty if created then delete SRN")
                Return True
                Exit Function
            End If


            Dim qry As String = "delete from TSPL_QC_CHECK_APPROVAL_ENTRY where document_code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_QC_CHECK_MRN_DETAIL where document_code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_QC_CHECK_SRN_DETAIL where document_code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_QC_CHECK_DETAIL where document_code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_QC_CHECK_HEAD where document_code='" + strCode + "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function PostData(ByVal strCode As String, ByVal QC_Type As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(strCode, QC_Type, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function PostData(ByVal strCode As String, ByVal QC_Type As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Document_Date,Bill_To_location from TSPL_QC_CHECK_HEAD where document_code='" + strCode + "'", trans)
            If dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleQualityControl, clsUserMgtCode.frmQualityCheckForSRN, clsCommon.myCstr(dt.Rows(0)("Bill_To_location")), clsCommon.myCDate(dt.Rows(0)("Document_Date")), trans)

            End If

            Dim qry As String = "select Posted from TSPL_QC_CHECK_HEAD where Document_Code='" + strCode + "' "
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) = 1 Then
                Throw New Exception("Already Posted Document [" + strCode + "]")
            End If
            qry = "update TSPL_QC_CHECK_HEAD set Posted='1',Modified_By='" + objCommonVar.CurrentUserCode + "',Modified_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy") + "' where document_code='" + strCode + "' and QC_Type='" + QC_Type + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            'qry = "update TSPL_PURCHASE_ORDER_HEAD set close_yn='N' where (coalesce((select sum(ok_qty) from TSPL_QC_CHECK_DETAIL where  Document_Code='" & strCode & "'),0)<=0 or coalesce((select sum(Reject_Qty) from TSPL_QC_CHECK_DETAIL where  Document_Code='" & strCode & "'),0)>0)"
            Dim StrPONOQry As String = "DECLARE @query  AS NVARCHAR(MAX) SELECT  STUFF((SELECT distinct ',''' + TSPL_QC_CHECK_detail.po_no+'''' as Alies_Name 
            FROM TSPL_QC_CHECK_detail where document_code in ('" & strCode & "')  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')"
            Dim StrPONO As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(StrPONOQry, trans))


            If clsCommon.myLen(StrPONO) > 0 Then
                qry = "update TSPL_PURCHASE_ORDER_HEAD set close_yn='N' where (coalesce((select sum(ok_qty) from TSPL_QC_CHECK_DETAIL where  Document_Code='" & strCode & "'),0)<=0 or coalesce((select sum(Reject_Qty) from TSPL_QC_CHECK_DETAIL where  Document_Code='" & strCode & "'),0)>0) and TSPL_PURCHASE_ORDER_HEAD.PURCHASEORDER_no in (" & StrPONO & ") "
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If


            '===Sanjeet(03/01/2017) for notifiaction====
            Dim strNotificationOn As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_On from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmQualityCheckForSRN + "'", trans))
            Dim obj As New clsQualityCheckForSRNHead()
            If clsCommon.CompairString(strNotificationOn, "P") = CompairStringResult.Equal Then
                'Dim obj As New clsQualityCheckForSRNHead()
                obj = clsQualityCheckForSRNHead.GetData(strCode, QC_Type, NavigatorType.Current, trans)
                CreateNotificationContentEMP(obj, trans)
            End If
            '=======================================
            If objCommonVar.InternalSMSEmailinPurchaseModule = True Then
                If clsCommon.myLen(obj.Document_Code) = 0 Then
                    obj = clsQualityCheckForSRNHead.GetData(strCode, QC_Type, NavigatorType.Current, trans)
                End If
                CreateInternalEmailSMS(obj, trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Private Shared Sub CreateInternalEmailSMS(ByVal obj As clsQualityCheckForSRNHead, ByVal trans As SqlTransaction)
        Dim itemName As String = ""
        Dim UOM As String = ""
        Dim qty As String = ""

        Dim dtContent As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject,Notification_Text from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmQualityCheckForSRN + "2" + "'", trans)

        Dim qry As String = "select TSPL_USER_MASTER.User_Code from TSPL_USER_MASTER "
        If clsCommon.myLen(clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT TSPL_MRN_HEAD.Against_Requisition from TSPL_MRN_HEAD left join TSPL_QC_CHECK_DETAIL on TSPL_QC_CHECK_DETAIL.MRN_No=TSPL_MRN_HEAD.MRN_No where TSPL_QC_CHECK_DETAIL.Document_Code='" + obj.Document_Code + "'", trans))) > 0 Then
            qry += " left join TSPL_REQUISITION_HEAD on TSPL_REQUISITION_HEAD.Created_By=TSPL_USER_MASTER.User_Code "
            qry += " left join TSPL_MRN_HEAD on TSPL_MRN_HEAD.Against_Requisition=TSPL_REQUISITION_HEAD.Requisition_Id left join TSPL_QC_CHECK_DETAIL ON TSPL_QC_CHECK_DETAIL.MRN_NO=TSPL_MRN_HEAD.MRN_NO "
        Else
            qry += " left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.Created_By=TSPL_USER_MASTER.User_Code left join TSPL_MRN_HEAD on TSPL_MRN_HEAD.Against_PO=TSPL_PURCHASE_ORDER_HEAD.PURCHASEORDER_no "
            qry += " left join TSPL_QC_CHECK_DETAIL on TSPL_QC_CHECK_DETAIL.MRN_No=TSPL_MRN_HEAD.MRN_No"
        End If
        qry += " where TSPL_QC_CHECK_DETAIL.Document_Code='" + obj.Document_Code + "'"
        Dim StrUserCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        Dim arrMobileNo As New List(Of String)
        Dim arrMailID As List(Of String) = clsERPFuncationality.ReportingMailIdandPhone(StrUserCode, arrMobileNo, trans)

        Dim strTempNexr As String = Nothing
        If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
            For Each objtr As clsQualityCheckDetail In obj.Arr
                Dim strTemp As String = ""
                strTemp += "PO No-" + clsCommon.myCstr(objtr.PO_No) + ","
                Dim StrIndentNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT TSPL_PURCHASE_ORDER_HEAD.Against_Requisition FROM TSPL_PURCHASE_ORDER_HEAD WHERE PURCHASEORDER_NO='" + objtr.PO_No + "'", trans))
                If clsCommon.myLen(StrIndentNo) > 0 Then
                    strTemp += " Indent No- " + clsCommon.myCstr(StrIndentNo) + ","
                Else
                    strTemp += " Indent No- " + clsCommon.myCstr("N/A") + ","
                End If
                strTemp += " MRN No- " + clsCommon.myCstr(objtr.MRN_No) + ","
                strTemp += " Item Code- " + clsCommon.myCstr(objtr.Item_Code) + ","
                strTemp += " Item Name- " + clsCommon.myCstr(objtr.Item_Desc) + ","
                strTemp += " UOM- " + clsCommon.myCstr(objtr.Unit_Code) + ","
                strTemp += " Ok Qty- " + clsCommon.myFormat(clsCommon.myCdbl(objtr.Ok_Qty)) + ","
                strTemp += " Rejected Qty- " + clsCommon.myFormat(clsCommon.myCdbl(objtr.Reject_Qty)) + ","
                strTemp += " Status- " + clsCommon.myCstr(objtr.QC_Status)
                strTempNexr += strTemp
                strTempNexr += Environment.NewLine
            Next
        End If

        If dtContent IsNot Nothing AndAlso dtContent.Rows.Count > 0 AndAlso ((arrMailID IsNot Nothing AndAlso arrMailID.Count > 0) Or (arrMobileNo IsNot Nothing AndAlso arrMobileNo.Count > 0)) Then

            If clsCommon.myLen(dtContent.Rows(0)("Email_Text")) > 0 AndAlso (arrMailID IsNot Nothing AndAlso arrMailID.Count > 0) Then
                Dim objEmailH As New clsEMailHead()
                objEmailH.arrEMail = New List(Of String)()
                objEmailH.arrEMail = arrMailID

                'Dim strTempNexr As String = Nothing
                'If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                '    For Each objtr As clsQualityCheckDetail In obj.Arr
                '        Dim strTemp As String = ""
                '        strTemp += "PO No-" + clsCommon.myCstr(objtr.PO_No) + ","
                '        Dim StrIndentNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT TSPL_PURCHASE_ORDER_HEAD.Against_Requisition FROM TSPL_PURCHASE_ORDER_HEAD WHERE PURCHASEORDER_NO='" + objtr.PO_No + "'", trans))
                '        If clsCommon.myLen(StrIndentNo) > 0 Then
                '            strTemp += " Indent No- " + clsCommon.myCstr(StrIndentNo) + ","
                '        Else
                '            strTemp += " Indent No- " + clsCommon.myCstr("N/A") + ","
                '        End If
                '        strTemp += " Item Code- " + clsCommon.myCstr(objtr.Item_Code) + ","
                '        strTemp += " Ok Qty- " + clsCommon.myFormat(clsCommon.myCdbl(objtr.Ok_Qty)) + ","
                '        strTemp += " Rejected Qty- " + clsCommon.myFormat(clsCommon.myCdbl(objtr.Reject_Qty))
                '        strTempNexr += strTemp
                '        strTempNexr += Environment.NewLine
                '    Next
                'End If

                objEmailH.Email_Subject = clsCommon.myCstr(dtContent.Rows(0)("Email_subject"))
                objEmailH.Email_Text = clsCommon.myCstr(dtContent.Rows(0)("Email_Text"))

                objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_No, clsCommon.myCstr(obj.Document_Code))
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_Date, clsCommon.myCstr(obj.Document_Date))
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Status, clsCommon.myCstr(obj.QC_Status))
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Vendor_Code, clsCommon.myCstr(obj.Vendor_Code))
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Vendor_Name, clsCommon.myCstr(obj.Vendor_Name))
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Detail_Data, clsCommon.myCstr(strTempNexr))

                objEmailH.SaveData(clsUserMgtCode.frmQualityCheckForSRN, objEmailH, trans)
                objEmailH = Nothing
            End If

            If clsCommon.myLen(dtContent.Rows(0)("SMS_Text")) > 0 AndAlso (arrMobileNo IsNot Nothing AndAlso arrMobileNo.Count > 0) Then
                Dim objSMSH As New clsSMSHead()
                objSMSH.arrMobilNo = New List(Of String)()
                objSMSH.arrMobilNo = arrMobileNo

                objSMSH.SMS_Text = clsCommon.myCstr(dtContent.Rows(0)("SMS_Text"))
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_No, clsCommon.myCstr(obj.Document_Code))
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_Date, clsCommon.myCstr(obj.Document_Date))
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Status, clsCommon.myCstr(obj.QC_Status))
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Vendor_Code, clsCommon.myCstr(obj.Vendor_Code))
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Vendor_Name, clsCommon.myCstr(obj.Vendor_Name))

                'Dim strTempNexr As String = Nothing
                'If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                '    For Each objtr As clsQualityCheckDetail In obj.Arr
                '        Dim strTemp As String = ""
                '        strTemp += "PO No-" + clsCommon.myCstr(objtr.PO_No) + ","
                '        Dim StrIndentNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT TSPL_PURCHASE_ORDER_HEAD.Against_Requisition FROM TSPL_PURCHASE_ORDER_HEAD WHERE PURCHASEORDER_NO='" + objtr.PO_No + "'", trans))
                '        If clsCommon.myLen(StrIndentNo) > 0 Then
                '            strTemp += " Indent No- " + clsCommon.myCstr(StrIndentNo) + ","
                '        Else
                '            strTemp += " Indent No- " + clsCommon.myCstr("N/A") + ","
                '        End If
                '        strTemp += " Item Code- " + clsCommon.myCstr(objtr.Item_Code) + ","
                '        strTemp += " Ok Qty- " + clsCommon.myFormat(clsCommon.myCdbl(objtr.Ok_Qty)) + ","
                '        strTemp += " Rejected Qty- " + clsCommon.myFormat(clsCommon.myCdbl(objtr.Reject_Qty))
                '        strTempNexr += strTemp
                '        strTempNexr += Environment.NewLine
                '    Next
                'End If
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Detail_Data, clsCommon.myCstr(strTempNexr))

                objSMSH.SaveData(clsUserMgtCode.frmQualityCheckForSRN, objSMSH, trans)
                objSMSH = Nothing
            End If

        End If

        ''Notification 
        If dtContent IsNot Nothing AndAlso dtContent.Rows.Count > 0 AndAlso clsCommon.myLen(dtContent.Rows(0)("Notification_Text")) > 0 Then

            Dim strNotifiCaption As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_Caption from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmQualityCheckForSRN + "2" + "'", trans))
            Dim strNotificationOn As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_On from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmQualityCheckForSRN + "2" + "'", trans))

            Dim objNotification As New clsNotificationHead()
            objNotification.Notification_Text = clsCommon.myCstr(dtContent.Rows(0)("Notification_Text"))
            objNotification.Notification_Caption = strNotifiCaption
            objNotification.Notification_On = strNotificationOn

            objNotification.Notification_Text = objNotification.Notification_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_No, clsCommon.myCstr(obj.Document_Code))
            objNotification.Notification_Text = objNotification.Notification_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_Date, clsCommon.myCstr(obj.Document_Date))
            objNotification.Notification_Text = objNotification.Notification_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Status, clsCommon.myCstr(obj.QC_Status))
            objNotification.Notification_Text = objNotification.Notification_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Vendor_Code, clsCommon.myCstr(obj.Vendor_Code))
            objNotification.Notification_Text = objNotification.Notification_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Vendor_Name, clsCommon.myCstr(obj.Vendor_Name))
            objNotification.Notification_Text = objNotification.Notification_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Detail_Data, clsCommon.myCstr(strTempNexr))

            objNotification.SaveData(clsUserMgtCode.frmQualityCheckForSRN + "2", objNotification, trans)
            objNotification = Nothing

        End If
        ''Notification

    End Sub

    Public Shared Function CreateNotificationContentEMP(ByVal obj As clsQualityCheckForSRNHead, ByVal trans As SqlTransaction) As Boolean
        Dim strNotifiContent As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_Text from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmQualityCheckForSRN + "'", trans))
        Dim strNotifi_DetalContent As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_Detail_Text from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmQualityCheckForSRN + "'", trans))
        Dim strNotifiCaption As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_Caption from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmQualityCheckForSRN + "'", trans))
        Dim strNotificationOn As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_On from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmQualityCheckForSRN + "'", trans))
        If clsCommon.myLen(strNotifiContent) > 0 Then
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0 Then
                Dim objNotification As New clsNotificationHead()
                objNotification.Notification_Text = strNotifiContent
                objNotification.Notification_Caption = strNotifiCaption
                objNotification.Notification_On = strNotificationOn
                objNotification.Notification_Detail_Text = strNotifi_DetalContent
                objNotification.Notification_Text = objNotification.Notification_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_No, clsCommon.myCstr(obj.Document_Code))
                objNotification.Notification_Text = objNotification.Notification_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_Date, clsCommon.myCstr(obj.Document_Date))

                Dim strTempNexr As String = Nothing
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objtr As clsQualityCheckDetail In obj.Arr
                        Dim strTemp As String = objNotification.Notification_Detail_Text
                        strTemp = strTemp.Replace(XpertERPEngine.frmEMailAndSMSSetting.PO_No, clsCommon.myCstr(objtr.PO_No))
                        Dim StrIndentNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT TSPL_PURCHASE_ORDER_HEAD.Against_Requisition FROM TSPL_PURCHASE_ORDER_HEAD WHERE PURCHASEORDER_NO='" + objtr.PO_No + "'", trans))
                        If clsCommon.myLen(StrIndentNo) > 0 Then
                            strTemp = strTemp.Replace(XpertERPEngine.frmEMailAndSMSSetting.Indent_No, clsCommon.myCstr(StrIndentNo))
                        Else
                            strTemp = strTemp.Replace(XpertERPEngine.frmEMailAndSMSSetting.Indent_No, clsCommon.myCstr("N/A"))
                        End If
                        strTemp = strTemp.Replace(XpertERPEngine.frmEMailAndSMSSetting.MRN_No, clsCommon.myCstr(objtr.MRN_No))
                        strTemp = strTemp.Replace(XpertERPEngine.frmEMailAndSMSSetting.Item_Code, clsCommon.myCstr(objtr.Item_Code))
                        strTemp = strTemp.Replace(XpertERPEngine.frmEMailAndSMSSetting.QCAccepted, clsCommon.myFormat(clsCommon.myCdbl(objtr.Ok_Qty)))
                        strTemp = strTemp.Replace(XpertERPEngine.frmEMailAndSMSSetting.QCRejected, clsCommon.myFormat(clsCommon.myCdbl(objtr.Reject_Qty)))
                        strTempNexr += strTemp
                        strTempNexr += Environment.NewLine
                    Next
                End If
                objNotification.Notification_Text = objNotification.Notification_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Detail_Data, clsCommon.myCstr(strTempNexr))
                objNotification.SaveData(clsUserMgtCode.frmQualityCheckForSRN, objNotification, trans)
                objNotification = Nothing
            End If
            Return True
        End If
        Return False
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            ReverseAndUnpost(strCode, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try

            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Document_Date,Bill_To_location from TSPL_QC_CHECK_HEAD where document_code='" + strCode + "'", trans)
            If dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleQualityControl, clsUserMgtCode.frmQualityCheckForSRN, clsCommon.myCstr(dt.Rows(0)("Bill_To_location")), clsCommon.myCDate(dt.Rows(0)("Document_Date")), trans)

            End If

            Dim Qry As String = "select Posted from TSPL_QC_CHECK_HEAD where Document_Code='" + strCode + "'"
            If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If

            Qry = "select SRN_ID,Document_Code from TSPL_QC_CHECK_APPROVAL_ENTRY where Document_Code='" + strCode + "' and coalesce(srn_id,'')<>''"
            dt = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Qry = "First delete SRN ID"
                For Each DR As DataRow In dt.Rows
                    Qry += Environment.NewLine + clsCommon.myCstr(DR("SRN_ID"))
                Next
                Throw New Exception(Qry)
            End If

            Qry = "delete from TSPL_QC_CHECK_APPROVAL_ENTRY where Document_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = "Update TSPL_QC_CHECK_HEAD set posted = 0,Approved_For_SRN=0 where Document_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class

Public Class clsQualityCheckForSRNDetail
#Region "variables"
    Public Document_Code As String = Nothing
    Public Line_no As Integer = Nothing
    Public MRN_No As String = Nothing
    Public PO_No As String = Nothing
    Public Row_Type As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Unit_Code As String = Nothing
    Public Drawing_No As String = Nothing
    Public Part_No As String = Nothing
    Public MRN_Qty As Decimal = Nothing
    Public QC_Param_Code As String = Nothing
    Public Param_Desc As String = Nothing
    Public Param_L_Range As Decimal = Nothing
    Public Param_U_Range As Decimal = Nothing
    Public Param_Status As String = Nothing 'present/not present
    Public Param_Value As String = Nothing
    Public Param_QC_Status As String = Nothing 'ok/not ok
    Public Ok_Qty As Decimal = Nothing
    Public Reject_Qty As Decimal = Nothing
    Public Measured_1 As String = Nothing
    Public Measured_2 As String = Nothing
    Public Measured_3 As String = Nothing
    Public Measured_4 As String = Nothing
    Public Measured_5 As String = Nothing
    Public Measured_6 As String = Nothing
    Public Measured_7 As String = Nothing
    Public Measured_8 As String = Nothing
    Public Measured_9 As String = Nothing
    Public Measured_10 As String = Nothing
    Public Net_Measurement As String = Nothing
    Public Auto_Measured As String = Nothing
    Public Remarks As String = Nothing
    Public Reject_Remarks As String = Nothing
    Public InputData As Double = Nothing
    Public InputDataDeductionPer As Decimal = Nothing
    Public Mandatory As Integer = 1
#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal arr As List(Of clsQualityCheckForSRNDetail), ByVal trans As SqlTransaction) As Boolean
        Dim coll As New Hashtable()
        Try
            Dim qry As String = "delete from TSPL_QC_CHECK_SRN_DETAIL where document_code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsQualityCheckForSRNDetail In arr
                    coll = New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "Document_Code", strCode)
                    clsCommon.AddColumnsForChange(coll, "Line_no", objtr.Line_no)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.Item_Code, True)
                    clsCommon.AddColumnsForChange(coll, "PO_No", objtr.PO_No, True)
                    clsCommon.AddColumnsForChange(coll, "MRN_No", objtr.MRN_No, True)
                    clsCommon.AddColumnsForChange(coll, "Row_Type", objtr.Row_Type)
                    clsCommon.AddColumnsForChange(coll, "Unit_Code", objtr.Unit_Code, True)
                    clsCommon.AddColumnsForChange(coll, "MRN_Qty", objtr.MRN_Qty)
                    clsCommon.AddColumnsForChange(coll, "QC_Param_Code", objtr.QC_Param_Code, True)
                    clsCommon.AddColumnsForChange(coll, "Param_L_Range", objtr.Param_L_Range)
                    clsCommon.AddColumnsForChange(coll, "Param_U_Range", objtr.Param_U_Range)
                    clsCommon.AddColumnsForChange(coll, "Param_Status", objtr.Param_Status)
                    clsCommon.AddColumnsForChange(coll, "Param_Value", objtr.Param_Value)
                    clsCommon.AddColumnsForChange(coll, "Param_QC_Status", objtr.Param_QC_Status)
                    clsCommon.AddColumnsForChange(coll, "Ok_Qty", objtr.Ok_Qty)
                    clsCommon.AddColumnsForChange(coll, "Reject_Qty", objtr.Reject_Qty)
                    clsCommon.AddColumnsForChange(coll, "Measured_1", objtr.Measured_1)
                    clsCommon.AddColumnsForChange(coll, "Measured_2", objtr.Measured_2)
                    clsCommon.AddColumnsForChange(coll, "Measured_3", objtr.Measured_3)
                    clsCommon.AddColumnsForChange(coll, "Measured_4", objtr.Measured_4)
                    clsCommon.AddColumnsForChange(coll, "Measured_5", objtr.Measured_5)
                    clsCommon.AddColumnsForChange(coll, "Measured_6", objtr.Measured_6)
                    clsCommon.AddColumnsForChange(coll, "Measured_7", objtr.Measured_7)
                    clsCommon.AddColumnsForChange(coll, "Measured_8", objtr.Measured_8)
                    clsCommon.AddColumnsForChange(coll, "Measured_9", objtr.Measured_9)
                    clsCommon.AddColumnsForChange(coll, "Measured_10", objtr.Measured_10)
                    clsCommon.AddColumnsForChange(coll, "Net_Measurement", objtr.Net_Measurement)
                    clsCommon.AddColumnsForChange(coll, "Auto_Measured", objtr.Auto_Measured)
                    clsCommon.AddColumnsForChange(coll, "Remarks", objtr.Remarks)
                    clsCommon.AddColumnsForChange(coll, "Reject_Remarks", objtr.Reject_Remarks)
                    clsCommon.AddColumnsForChange(coll, "InputData", objtr.InputData)
                    clsCommon.AddColumnsForChange(coll, "InputDataDeductionPer", objtr.InputDataDeductionPer)
                    clsCommon.AddColumnsForChange(coll, "Mandatory", objtr.Mandatory)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_QC_CHECK_SRN_DETAIL", OMInsertOrUpdate.Insert, "", trans)
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

Public Class clsQualityCheckForSRN_MRNDetail
#Region "variables"
    Public Document_Code As String = Nothing
    Public Line_No As Integer = Nothing
    Public Status As Integer = Nothing
    Public MRN_No As String = Nothing
    Public MRN_Date As Date = Nothing
    Public VehicleNo As String = Nothing
    Public MRN_Desc As String = Nothing
    Public Row_Type As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Unit_Code As String = Nothing
    Public Drawing_No As String = Nothing
    Public Part_No As String = Nothing
    Public Qty As Decimal = Nothing
    Public MRN_Type As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal arr As List(Of clsQualityCheckForSRN_MRNDetail), ByVal trans As SqlTransaction) As Boolean
        Dim coll As New Hashtable()
        Try
            Dim qry As String = "delete from TSPL_QC_CHECK_MRN_DETAIL where document_code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsQualityCheckForSRN_MRNDetail In arr
                    coll = New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "Document_Code", strCode)
                    clsCommon.AddColumnsForChange(coll, "Status", objtr.Status)
                    clsCommon.AddColumnsForChange(coll, "Line_No", objtr.Line_No)
                    clsCommon.AddColumnsForChange(coll, "MRN_No", objtr.MRN_No, True)
                    clsCommon.AddColumnsForChange(coll, "Row_Type", objtr.Row_Type)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.Item_Code, True)
                    clsCommon.AddColumnsForChange(coll, "Unit_Code", objtr.Unit_Code, True)
                    clsCommon.AddColumnsForChange(coll, "Qty", objtr.Qty)
                    clsCommon.AddColumnsForChange(coll, "MRN_Type", objtr.MRN_Type)

                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_QC_CHECK_MRN_DETAIL", OMInsertOrUpdate.Insert, "", trans)
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

Public Class clsQualityCheckDetail
#Region "variables"
    Public Document_Code As String = Nothing
    Public Line_No As Integer = Nothing
    Public MRN_No As String = Nothing
    Public PO_No As String = Nothing
    Public MRN_Desc As String = Nothing
    Public Row_Type As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Unit_Code As String = Nothing
    Public Drawing_No As String = Nothing
    Public Part_No As String = Nothing
    Public MRN_Qty As Decimal = Nothing
    Public Ok_Qty As Decimal = Nothing
    Public Reject_Qty As Decimal = Nothing
    Public Remarks As String = Nothing
    Public Reject_Remarks As String = Nothing
    Public QC_Status As String = Nothing
    Public Additional_Remarks As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal arr As List(Of clsQualityCheckDetail), ByVal trans As SqlTransaction) As Boolean
        Dim coll As New Hashtable()
        Try
            Dim qry As String = "delete from TSPL_QC_CHECK_DETAIL where document_code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsQualityCheckDetail In arr
                    coll = New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "Document_Code", strCode)
                    clsCommon.AddColumnsForChange(coll, "Line_No", objtr.Line_No)
                    clsCommon.AddColumnsForChange(coll, "MRN_No", objtr.MRN_No, True)
                    clsCommon.AddColumnsForChange(coll, "PO_No", objtr.PO_No, True)
                    clsCommon.AddColumnsForChange(coll, "Row_Type", objtr.Row_Type)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.Item_Code, True)
                    clsCommon.AddColumnsForChange(coll, "Unit_Code", objtr.Unit_Code, True)
                    clsCommon.AddColumnsForChange(coll, "MRN_Qty", objtr.MRN_Qty)
                    clsCommon.AddColumnsForChange(coll, "Ok_Qty", objtr.Ok_Qty)
                    clsCommon.AddColumnsForChange(coll, "Reject_Qty", objtr.Reject_Qty)
                    clsCommon.AddColumnsForChange(coll, "Remarks", objtr.Remarks)
                    clsCommon.AddColumnsForChange(coll, "Reject_Remarks", objtr.Reject_Remarks)
                    clsCommon.AddColumnsForChange(coll, "QC_Status", objtr.QC_Status)
                    clsCommon.AddColumnsForChange(coll, "Additional_Remarks", objtr.Additional_Remarks)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_QC_CHECK_DETAIL", OMInsertOrUpdate.Insert, "", trans)
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
