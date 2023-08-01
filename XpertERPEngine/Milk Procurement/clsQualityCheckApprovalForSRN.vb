'created by Monika
Imports common
Imports System.Data.SqlClient


Public Class clsQualityCheckApprovalForSRN
#Region "variables"
    Public Document_Code As String = Nothing
    Public Document_Date As Date = Nothing
    Public Description As String = Nothing
    Public Vendor_Code As String = Nothing
    Public Vendor_Name As String = Nothing
    Public Bill_To_Location As String = Nothing
    Public SRN_Type As String = Nothing
    Public Item_Type As String = Nothing
    Public QC_Status As String = Nothing
    Public Line_No As Integer = Nothing
    Public Deduction As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_desc As String = Nothing


    Public Arr As List(Of clsQualityCheckApprovalForSRN) = Nothing
#End Region

    Public Shared Function SaveData(ByVal obj As clsQualityCheckApprovalForSRN) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SaveData(ByVal obj As clsQualityCheckApprovalForSRN, ByVal trans As SqlTransaction) As Boolean
        Dim coll As New Hashtable()
        Dim arrdocumentcode As List(Of String) = Nothing
        Dim arrdocumentcode_WithoutRejected As List(Of String) = Nothing
        Dim qry As String
        Try
            arrdocumentcode = New List(Of String)
            arrdocumentcode_WithoutRejected = New List(Of String)
            If obj IsNot Nothing AndAlso obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                For Each objtr As clsQualityCheckApprovalForSRN In obj.Arr

                    qry = "select Approved_For_SRN from TSPL_QC_CHECK_HEAD where Document_Code='" + objtr.Document_Code + "' "
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) = 1 Then
                        Throw New Exception("Already Approved Document [" + objtr.Document_Code + "]")
                    End If
                    coll = New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "Document_Code", objtr.Document_Code, True)
                    clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(objtr.Document_Date, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Description", objtr.Description)
                    clsCommon.AddColumnsForChange(coll, "Vendor_Code", objtr.Vendor_Code, True)
                    clsCommon.AddColumnsForChange(coll, "Bill_To_Location", objtr.Bill_To_Location, True)
                    clsCommon.AddColumnsForChange(coll, "QC_Status", objtr.QC_Status)
                    clsCommon.AddColumnsForChange(coll, "SRN_Type", objtr.SRN_Type)
                    clsCommon.AddColumnsForChange(coll, "Item_Type", objtr.Item_Type)
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

                    If Not arrdocumentcode.Contains(objtr.Document_Code) Then
                        arrdocumentcode.Add(objtr.Document_Code)
                    End If
                    If (Not arrdocumentcode_WithoutRejected.Contains(objtr.Document_Code)) AndAlso clsCommon.CompairString(objtr.QC_Status, "Rejected") <> CompairStringResult.Equal Then
                        arrdocumentcode_WithoutRejected.Add(objtr.Document_Code)
                    End If
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_QC_CHECK_APPROVAL_ENTRY", OMInsertOrUpdate.Insert, "", trans)

                Next
            End If
            'If clsCommon.CompairString(objtr.QC_Status, "Rejected") = CompairStringResult.Equal Then
            '    clsCommon.MyMessageBoxShow("Data cannot be saved.")
            'Else
            'End If

            'SaveDataForSRN(obj, arrdocumentcode, trans)
            If arrdocumentcode_WithoutRejected.Count > 0 Then
                SaveDataForSRN(obj, arrdocumentcode_WithoutRejected, trans)
            End If

            ''====================update the approval status to qc check table
            qry = "update TSPL_QC_CHECK_HEAD set Approved_For_SRN=1,modified_by='" + objCommonVar.CurrentUserCode + "',modified_date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy") + "' where document_code in (" + clsCommon.GetMulcallString(arrdocumentcode) + ")"

            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            coll = Nothing
        End Try
    End Function

    Public Shared Function SaveDataForSRN(ByVal obj1 As clsQualityCheckApprovalForSRN, ByVal arrdocumentcode As List(Of String), ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = ""
        Dim dt As New DataTable()
        Dim arrIcode As List(Of String) = Nothing
        Dim arrPONO As List(Of String) = Nothing
        Dim obj As New clsSRNHead()
        Dim objTr As New clsSRNDetail()
        Dim counter As Integer = 0
        Dim dt_MRN As New DataTable()
        Dim dr_MRN As DataRow = Nothing
        Dim arrQcNo As New List(Of String)
        Dim isSaved As Boolean = True

        Try
            If obj1 IsNot Nothing AndAlso obj1.Arr IsNot Nothing AndAlso obj1.Arr.Count > 0 Then
                arrQcNo = New List(Of String)
                arrQcNo = arrdocumentcode

                qry = "select mrn_no,PO_NO from TSPL_QC_CHECK_DETAIL where document_code in (" + clsCommon.GetMulcallString(arrdocumentcode) + ")"
                dt = New DataTable()
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                arrdocumentcode = New List(Of String)
                arrPONO = New List(Of String)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        If Not arrdocumentcode.Contains(clsCommon.myCstr(dr("mrn_no"))) AndAlso clsCommon.myLen(dr("mrn_no")) > 0 Then
                            arrdocumentcode.Add(clsCommon.myCstr(dr("mrn_no")))
                        End If
                        If Not arrPONO.Contains(clsCommon.myCstr(dr("PO_no"))) AndAlso clsCommon.myLen(dr("PO_no")) > 0 Then
                            arrPONO.Add(clsCommon.myCstr(dr("PO_no")))
                        End If
                    Next
                End If
                qry = "select item_code from TSPL_QC_CHECK_DETAIL where document_code in (" + clsCommon.GetMulcallString(arrQcNo) + ") and mrn_no in (" + clsCommon.GetMulcallString(arrdocumentcode) + ") and coalesce(po_no,'') in (" + clsCommon.GetMulcallString(arrPONO) + ")"
                dt = New DataTable()
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                arrIcode = New List(Of String)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        If Not arrIcode.Contains(clsCommon.myCstr(dr("item_code"))) Then
                            arrIcode.Add(clsCommon.myCstr(dr("item_code")))
                        End If
                    Next
                End If
                '************************************

                ''taking tax group,tax rate wise mrn document for making SRN.
                dt_MRN = New DataTable()
                dr_MRN = Nothing
                dt_MRN.Columns.Add("MRN_No", GetType(String))

                qry = "select distinct ROW_NUMBER() over (partition by  Tax_Group,TAX1,TAX2,TAX3,TAX4,TAX5,TAX6,TAX7,TAX8,TAX9,TAX10,TAX1_Rate,TAX2_Rate,TAX3_Rate,TAX4_Rate,TAX5_Rate,TAX6_Rate,TAX7_Rate,TAX8_Rate,TAX9_Rate,TAX10_Rate order by Tax_Group,TAX1,TAX2,TAX3,TAX4,TAX5,TAX6,TAX7,TAX8,TAX9,TAX10,TAX1_Rate,TAX2_Rate,TAX3_Rate,TAX4_Rate,TAX5_Rate,TAX6_Rate,TAX7_Rate,TAX8_Rate,TAX9_Rate,TAX10_Rate) as sno,mrn_no from TSPL_MRN_HEAD where mrn_no in (" + clsCommon.GetMulcallString(arrdocumentcode) + ")"
                dt = New DataTable()
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                Dim xmrn_no As String = ""
                counter = 0
                Dim sno As Integer = 0
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        If counter > 0 AndAlso clsCommon.myCdbl(dr("sno")) >= 1 AndAlso clsCommon.myLen(xmrn_no) > 0 Then
                            If xmrn_no.Contains("','") AndAlso xmrn_no.Substring(0, 3) = "','" Then
                                xmrn_no = xmrn_no.Substring(2, xmrn_no.Length - 2) + "'"
                            End If
                            dr_MRN = dt_MRN.NewRow()
                            dr_MRN("MRN_No") = xmrn_no
                            dt_MRN.Rows.Add(dr_MRN)

                            xmrn_no = ""
                        End If

                        sno = clsCommon.myCdbl(dr("sno"))
                        xmrn_no = xmrn_no + "','" + clsCommon.myCstr(dr("mrn_no"))
                        counter += 1
                    Next
                End If
                ''-----------------when hve single mrn value then not filled in dtatable by above code so solved it by below code
                If (clsCommon.myLen(xmrn_no) > 0 AndAlso sno = 1) OrElse (counter <> dt_MRN.Rows.Count) Then
                    If xmrn_no.Contains("','") AndAlso xmrn_no.Substring(0, 3) = "','" Then
                        xmrn_no = xmrn_no.Substring(2, xmrn_no.Length - 2) + "'"
                    End If

                    dr_MRN = dt_MRN.NewRow()
                    dr_MRN("MRN_No") = xmrn_no
                    dt_MRN.Rows.Add(dr_MRN)
                End If


                '***************************************************
                counter = 0
                Dim mrnno As String = ""
                If dt_MRN IsNot Nothing AndAlso dt_MRN.Rows.Count > 0 Then
                    For Each drMRN As DataRow In dt_MRN.Rows
                        qry = "select TSPL_MRN_HEAD.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_MRN_HEAD.Ref_No,TSPL_MRN_HEAD.Bill_To_Location,TSPL_MRN_HEAD.Ship_To_Location,TSPL_MRN_HEAD.Comments,TSPL_MRN_HEAD.On_Hold,TSPL_MRN_HEAD.Description,TSPL_MRN_HEAD.Tax_Group,TSPL_MRN_HEAD.TAX1,TSPL_MRN_HEAD.TAX1_Rate,TSPL_MRN_HEAD.TAX2,TSPL_MRN_HEAD.TAX2_Rate,TSPL_MRN_HEAD.TAX3,TSPL_MRN_HEAD.TAX3_Rate,TSPL_MRN_HEAD.TAX4,TSPL_MRN_HEAD.TAX4_Rate,TSPL_MRN_HEAD.TAX5,TSPL_MRN_HEAD.TAX5_Rate,TSPL_MRN_HEAD.TAX6,TSPL_MRN_HEAD.TAX6_Rate,TSPL_MRN_HEAD.TAX7,TSPL_MRN_HEAD.TAX7_Rate,TSPL_MRN_HEAD.TAX8,TSPL_MRN_HEAD.TAX8_Rate,TSPL_MRN_HEAD.TAX9,TSPL_MRN_HEAD.TAX9_Rate,TSPL_MRN_HEAD.TAX10,TSPL_MRN_HEAD.TAX10_Rate,TSPL_MRN_HEAD.Terms_Code,TSPL_MRN_HEAD.Due_Date,TSPL_MRN_HEAD.Carrier,TSPL_MRN_HEAD.VehicleNo,TSPL_MRN_HEAD.GRNo,TSPL_MRN_HEAD.GENo,TSPL_MRN_HEAD.Against_RGP_No,TSPL_MRN_HEAD.GEDate,TSPL_MRN_HEAD.Item_Type,TSPL_MRN_HEAD.Dept,TSPL_MRN_HEAD.Dept_Desc,TSPL_MRN_HEAD.Against_PO,TSPL_MRN_HEAD.MRN_No,TSPL_MRN_HEAD.Against_Schedule_Code,TSPL_MRN_HEAD.PurchaseOrder_Type,TSPL_MRN_HEAD.Add_Charge_Code1,TSPL_MRN_HEAD.Add_Charge_Name1,TSPL_MRN_HEAD.Add_Charge_Code2,TSPL_MRN_HEAD.Add_Charge_Name2,TSPL_MRN_HEAD.Add_Charge_Code3,TSPL_MRN_HEAD.Add_Charge_Name3,TSPL_MRN_HEAD.Add_Charge_Code4,TSPL_MRN_HEAD.Add_Charge_Name4,TSPL_MRN_HEAD.Add_Charge_Code5,TSPL_MRN_HEAD.Add_Charge_Name5,TSPL_MRN_HEAD.Add_Charge_Code6,TSPL_MRN_HEAD.Add_Charge_Name6,TSPL_MRN_HEAD.Add_Charge_Code7,TSPL_MRN_HEAD.Add_Charge_Name7,TSPL_MRN_HEAD.Add_Charge_Code8,TSPL_MRN_HEAD.Add_Charge_Name8,TSPL_MRN_HEAD.Add_Charge_Code9,TSPL_MRN_HEAD.Add_Charge_Name9,TSPL_MRN_HEAD.Add_Charge_Code10,TSPL_MRN_HEAD.Add_Charge_Name10,TSPL_MRN_HEAD.CURRENCY_CODE,TSPL_MRN_HEAD.ConvRate,TSPL_MRN_HEAD.ApplicableFrom " & _
                        ",sum(isnull(TSPL_MRN_HEAD.TAX1_Base_Amt,0)) as TAX1_Base_Amt,sum(isnull(TSPL_MRN_HEAD.TAX1_Amt,0)) as TAX1_Amt,sum(isnull(TSPL_MRN_HEAD.TAX2_Base_Amt,0)) as TAX2_Base_Amt,sum(isnull(TSPL_MRN_HEAD.TAX2_Amt,0)) as TAX2_Amt,sum(isnull(TSPL_MRN_HEAD.TAX3_Base_Amt,0)) as TAX3_Base_Amt,sum(isnull(TSPL_MRN_HEAD.TAX3_Amt,0)) as TAX3_Amt,sum(isnull(TSPL_MRN_HEAD.TAX4_Base_Amt,0)) as TAX4_Base_Amt,sum(isnull(TSPL_MRN_HEAD.TAX4_Amt,0)) as TAX4_Amt,sum(isnull(TSPL_MRN_HEAD.TAX5_Base_Amt,0)) as TAX5_Base_Amt,sum(isnull(TSPL_MRN_HEAD.TAX5_Amt,0)) as TAX5_Amt,sum(isnull(TSPL_MRN_HEAD.TAX6_Base_Amt,0)) as TAX6_Base_Amt,sum(isnull(TSPL_MRN_HEAD.TAX6_Amt,0)) as TAX6_Amt,sum(isnull(TSPL_MRN_HEAD.TAX7_Base_Amt,0)) as TAX7_Base_Amt,sum(isnull(TSPL_MRN_HEAD.TAX7_Amt,0)) as TAX7_Amt,sum(isnull(TSPL_MRN_HEAD.TAX8_Base_Amt,0)) as TAX8_Base_Amt,sum(isnull(TSPL_MRN_HEAD.TAX8_Amt,0)) as TAX8_Amt,sum(isnull(TSPL_MRN_HEAD.TAX9_Base_Amt,0)) as TAX9_Base_Amt,sum(isnull(TSPL_MRN_HEAD.TAX9_Amt,0)) as TAX9_Amt,sum(isnull(TSPL_MRN_HEAD.TAX10_Base_Amt,0)) as TAX10_Base_Amt,sum(isnull(TSPL_MRN_HEAD.TAX10_Amt,0)) as TAX10_Amt,sum(isnull(TSPL_MRN_HEAD.Discount_Base,0)) as Discount_Base,sum(isnull(TSPL_MRN_HEAD.Discount_Amt,0)) as Discount_Amt,sum(isnull(TSPL_MRN_HEAD.Amount_Less_Discount,0)) as Amount_Less_Discount,sum(isnull(TSPL_MRN_HEAD.MRN_Total_Amt,0)) as MRN_Total_Amt,sum(isnull(TSPL_MRN_HEAD.Total_Add_Charge,0)) as Total_Add_Charge,sum(isnull(TSPL_MRN_HEAD.Add_Charge_Amt1,0)) as Add_Charge_Amt1,sum(isnull(TSPL_MRN_HEAD.Add_Charge_Amt2,0)) as Add_Charge_Amt2,sum(isnull(TSPL_MRN_HEAD.Add_Charge_Amt3,0)) as Add_Charge_Amt3,sum(isnull(TSPL_MRN_HEAD.Add_Charge_Amt4,0)) as Add_Charge_Amt4,sum(isnull(TSPL_MRN_HEAD.Add_Charge_Amt5,0)) as Add_Charge_Amt5,sum(isnull(TSPL_MRN_HEAD.Add_Charge_Amt6,0)) as Add_Charge_Amt6,sum(isnull(TSPL_MRN_HEAD.Add_Charge_Amt7,0)) as Add_Charge_Amt7,sum(isnull(TSPL_MRN_HEAD.Add_Charge_Amt8,0)) as Add_Charge_Amt8,sum(isnull(TSPL_MRN_HEAD.Add_Charge_Amt9,0)) as Add_Charge_Amt9,sum(isnull(TSPL_MRN_HEAD.Add_Charge_Amt10,0)) as Add_Charge_Amt10,sum(isnull(TSPL_MRN_HEAD.Total_Tax_Amt,0)) as Total_Tax_Amt " & _
                        "from TSPL_MRN_HEAD left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_MRN_HEAD.Vendor_Code where TSPL_MRN_HEAD.mrn_no in (" + clsCommon.myCstr(drMRN("mrn_no")) + ") group by TSPL_MRN_HEAD.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_MRN_HEAD.Ref_No,TSPL_MRN_HEAD.Bill_To_Location,TSPL_MRN_HEAD.Ship_To_Location,TSPL_MRN_HEAD.Comments,TSPL_MRN_HEAD.On_Hold,TSPL_MRN_HEAD.Description,TSPL_MRN_HEAD.Tax_Group,TSPL_MRN_HEAD.TAX1,TSPL_MRN_HEAD.TAX1_Rate,TSPL_MRN_HEAD.TAX2,TSPL_MRN_HEAD.TAX2_Rate,TSPL_MRN_HEAD.TAX3,TSPL_MRN_HEAD.TAX3_Rate,TSPL_MRN_HEAD.TAX4,TSPL_MRN_HEAD.TAX4_Rate,TSPL_MRN_HEAD.TAX5,TSPL_MRN_HEAD.TAX5_Rate,TSPL_MRN_HEAD.TAX6,TSPL_MRN_HEAD.TAX6_Rate,TSPL_MRN_HEAD.TAX7,TSPL_MRN_HEAD.TAX7_Rate,TSPL_MRN_HEAD.TAX8,TSPL_MRN_HEAD.TAX8_Rate,TSPL_MRN_HEAD.TAX9,TSPL_MRN_HEAD.TAX9_Rate,TSPL_MRN_HEAD.TAX10,TSPL_MRN_HEAD.TAX10_Rate,TSPL_MRN_HEAD.Terms_Code,TSPL_MRN_HEAD.Due_Date,TSPL_MRN_HEAD.Carrier,TSPL_MRN_HEAD.VehicleNo,TSPL_MRN_HEAD.GRNo,TSPL_MRN_HEAD.GENo,TSPL_MRN_HEAD.Against_RGP_No,TSPL_MRN_HEAD.GEDate,TSPL_MRN_HEAD.Item_Type,TSPL_MRN_HEAD.Dept,TSPL_MRN_HEAD.Dept_Desc,TSPL_MRN_HEAD.Against_PO,TSPL_MRN_HEAD.MRN_No,TSPL_MRN_HEAD.Against_Schedule_Code,TSPL_MRN_HEAD.PurchaseOrder_Type,TSPL_MRN_HEAD.Add_Charge_Code1,TSPL_MRN_HEAD.Add_Charge_Name1,TSPL_MRN_HEAD.Add_Charge_Code2,TSPL_MRN_HEAD.Add_Charge_Name2,TSPL_MRN_HEAD.Add_Charge_Code3,TSPL_MRN_HEAD.Add_Charge_Name3,TSPL_MRN_HEAD.Add_Charge_Code4,TSPL_MRN_HEAD.Add_Charge_Name4,TSPL_MRN_HEAD.Add_Charge_Code5,TSPL_MRN_HEAD.Add_Charge_Name5,TSPL_MRN_HEAD.Add_Charge_Code6,TSPL_MRN_HEAD.Add_Charge_Name6,TSPL_MRN_HEAD.Add_Charge_Code7,TSPL_MRN_HEAD.Add_Charge_Name7,TSPL_MRN_HEAD.Add_Charge_Code8,TSPL_MRN_HEAD.Add_Charge_Name8,TSPL_MRN_HEAD.Add_Charge_Code9,TSPL_MRN_HEAD.Add_Charge_Name9,TSPL_MRN_HEAD.Add_Charge_Code10,TSPL_MRN_HEAD.Add_Charge_Name10,TSPL_MRN_HEAD.CURRENCY_CODE,TSPL_MRN_HEAD.ConvRate,TSPL_MRN_HEAD.ApplicableFrom "
                        dt = New DataTable()
                        dt = clsDBFuncationality.GetDataTable(qry, trans)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            mrnno = clsCommon.myCstr(drMRN("mrn_no"))
                            obj = New clsSRNHead()
                            obj.SRN_No = Nothing
                            Dim srndate As Date = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select top 1 document_date from TSPL_QC_CHECK_HEAD left outer join TSPL_QC_CHECK_DETAIL on TSPL_QC_CHECK_HEAD.Document_Code=TSPL_QC_CHECK_DETAIL.Document_Code where MRN_No='" + clsCommon.myCstr(dt.Rows(0)("mrn_no")) + "'", trans))

                            Dim QcFincalYear As Integer = srndate.Year.ToString()
                            If srndate.Month >= 1 AndAlso srndate.Month <= 3 Then
                                QcFincalYear -= 1
                            End If
                            Dim CurrentFincalYear As Date = clsCommon.GETSERVERDATE(trans)

                            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then

                                If clsCommon.CompairString(QcFincalYear, CurrentFincalYear.Year) = CompairStringResult.Equal Then
                                    obj.SRN_Date = clsCommon.GETSERVERDATE(trans)
                                Else
                                    obj.SRN_Date = srndate
                                End If
                            Else
                                If clsCommon.myLen(srndate) > 0 AndAlso IsDate(srndate) AndAlso Not IsDBNull(srndate) Then
                                    obj.SRN_Date = srndate
                                Else
                                    obj.SRN_Date = clsCommon.GETSERVERDATE(trans)
                                End If
                            End If

                            
                            'obj.SRN_Date = clsCommon.myCstr(dt.Rows(0)("Due_Date"))
                            obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("vendor_code"))
                            obj.Vendor_Name = clsCommon.myCstr(dt.Rows(0)("vendor_name"))
                            obj.Ref_No = clsCommon.myCstr(dt.Rows(0)("ref_no"))
                            obj.Inv_Date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy")
                            obj.Challan_Date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy")
                            obj.Total_Tax_Amt = clsCommon.myCdbl(dt.Rows(0)("total_tax_amt"))
                            obj.Inv_No = Nothing
                            obj.is_RGP_Non_Inventory = False
                            obj.Bill_To_Location = clsCommon.myCstr(dt.Rows(0)("bill_to_location"))
                            obj.Ship_To_Location = clsCommon.myCstr(dt.Rows(0)("ship_to_location"))
                            obj.Comments = clsCommon.myCstr(dt.Rows(0)("comments"))
                            obj.On_Hold = IIf(clsCommon.myCstr(dt.Rows(0)("on_hold")) = "1", True, False)
                            obj.Description = clsCommon.myCstr(dt.Rows(0)("description"))
                            obj.Tax_Group = clsCommon.myCstr(dt.Rows(0)("tax_group"))
                            obj.Form_38 = Nothing
                            obj.Is_Internal = False
                            obj.autosrnfromrgp = Nothing

                            If clsCommon.myLen(dt.Rows(0)("tax1")) > 0 Then
                                obj.TAX1 = clsCommon.myCstr(dt.Rows(0)("tax1"))
                                obj.TAX1_Rate = clsCommon.myCdbl(dt.Rows(0)("tax1_rate"))
                                obj.TAX1_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("tax1_base_amt"))
                                obj.TAX1_Amt = clsCommon.myCdbl(dt.Rows(0)("tax1_amt"))
                                obj.AssessableAmt = obj.TAX1_Base_Amt
                            End If
                            If clsCommon.myLen(dt.Rows(0)("tax2")) > 0 Then
                                obj.TAX2 = clsCommon.myCstr(dt.Rows(0)("tax2"))
                                obj.TAX2_Rate = clsCommon.myCdbl(dt.Rows(0)("tax2_rate"))
                                obj.TAX2_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("tax2_base_amt"))
                                obj.TAX2_Amt = clsCommon.myCdbl(dt.Rows(0)("tax2_amt"))
                                obj.AssessableAmt = obj.TAX2_Base_Amt
                            End If
                            If clsCommon.myLen(dt.Rows(0)("tax3")) > 0 Then
                                obj.TAX3 = clsCommon.myCstr(dt.Rows(0)("tax3"))
                                obj.TAX3_Rate = clsCommon.myCdbl(dt.Rows(0)("tax3_rate"))
                                obj.TAX3_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("tax3_base_amt"))
                                obj.TAX3_Amt = clsCommon.myCdbl(dt.Rows(0)("tax3_amt"))
                                obj.AssessableAmt = obj.TAX3_Base_Amt
                            End If
                            If clsCommon.myLen(dt.Rows(0)("tax4")) > 0 Then
                                obj.TAX4 = clsCommon.myCstr(dt.Rows(0)("tax4"))
                                obj.TAX4_Rate = clsCommon.myCdbl(dt.Rows(0)("tax4_rate"))
                                obj.TAX4_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("tax4_base_amt"))
                                obj.TAX4_Amt = clsCommon.myCdbl(dt.Rows(0)("tax4_amt"))
                                obj.AssessableAmt = obj.TAX4_Base_Amt
                            End If
                            If clsCommon.myLen(dt.Rows(0)("tax5")) > 0 Then
                                obj.TAX5 = clsCommon.myCstr(dt.Rows(0)("tax5"))
                                obj.TAX5_Rate = clsCommon.myCdbl(dt.Rows(0)("tax5_rate"))
                                obj.TAX5_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("tax5_base_amt"))
                                obj.TAX5_Amt = clsCommon.myCdbl(dt.Rows(0)("tax5_amt"))
                                obj.AssessableAmt = obj.TAX5_Base_Amt
                            End If
                            If clsCommon.myLen(dt.Rows(0)("tax6")) > 0 Then
                                obj.TAX6 = clsCommon.myCstr(dt.Rows(0)("tax6"))
                                obj.TAX6_Rate = clsCommon.myCdbl(dt.Rows(0)("tax6_rate"))
                                obj.TAX6_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("tax6_base_amt"))
                                obj.TAX6_Amt = clsCommon.myCdbl(dt.Rows(0)("tax6_amt"))
                                obj.AssessableAmt = obj.TAX6_Base_Amt
                            End If
                            If clsCommon.myLen(dt.Rows(0)("tax7")) > 0 Then
                                obj.TAX7 = clsCommon.myCstr(dt.Rows(0)("tax7"))
                                obj.TAX7_Rate = clsCommon.myCdbl(dt.Rows(0)("tax7_rate"))
                                obj.TAX7_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("tax7_base_amt"))
                                obj.TAX7_Amt = clsCommon.myCdbl(dt.Rows(0)("tax7_amt"))
                                obj.AssessableAmt = obj.TAX7_Base_Amt
                            End If
                            If clsCommon.myLen(dt.Rows(0)("tax8")) > 0 Then
                                obj.TAX8 = clsCommon.myCstr(dt.Rows(0)("tax8"))
                                obj.TAX8_Rate = clsCommon.myCdbl(dt.Rows(0)("tax8_rate"))
                                obj.TAX8_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("tax8_base_amt"))
                                obj.TAX8_Amt = clsCommon.myCdbl(dt.Rows(0)("tax8_amt"))
                                obj.AssessableAmt = obj.TAX8_Base_Amt
                            End If
                            If clsCommon.myLen(dt.Rows(0)("tax9")) > 0 Then
                                obj.TAX9 = clsCommon.myCstr(dt.Rows(0)("tax9"))
                                obj.TAX9_Rate = clsCommon.myCdbl(dt.Rows(0)("tax9_rate"))
                                obj.TAX9_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("tax9_base_amt"))
                                obj.TAX9_Amt = clsCommon.myCdbl(dt.Rows(0)("tax9_amt"))
                                obj.AssessableAmt = obj.TAX9_Base_Amt
                            End If
                            If clsCommon.myLen(dt.Rows(0)("tax10")) > 0 Then
                                obj.TAX10 = clsCommon.myCstr(dt.Rows(0)("tax10"))
                                obj.TAX10_Rate = clsCommon.myCdbl(dt.Rows(0)("tax10_rate"))
                                obj.TAX10_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("tax10_base_amt"))
                                obj.TAX10_Amt = clsCommon.myCdbl(dt.Rows(0)("tax10_amt"))
                                obj.AssessableAmt = obj.TAX10_Base_Amt
                            End If

                            obj.Terms_Code = clsCommon.myCstr(dt.Rows(0)("terms_code"))
                            obj.Due_Date = Nothing
                            If dt.Rows(0)("due_date") IsNot Nothing AndAlso Not IsDBNull(dt.Rows(0)("due_date")) AndAlso clsCommon.myLen(dt.Rows(0)("due_date")) > 0 AndAlso IsDate(dt.Rows(0)("due_date")) Then
                                obj.Due_Date = clsCommon.myCDate(dt.Rows(0)("due_date"))
                            End If
                            obj.Discount_Base = clsCommon.myCdbl(dt.Rows(0)("discount_base"))
                            obj.Discount_Amt = clsCommon.myCdbl(dt.Rows(0)("discount_amt"))
                            obj.Amount_Less_Discount = clsCommon.myCdbl(dt.Rows(0)("amount_less_discount"))
                            obj.SRN_Total_Amt = clsCommon.myCdbl(dt.Rows(0)("mrn_total_amt"))

                            obj.Carrier = clsCommon.myCstr(dt.Rows(0)("carrier"))
                            obj.VehicleNo = clsCommon.myCstr(dt.Rows(0)("VehicleNo"))
                            obj.GRNo = clsCommon.myCstr(dt.Rows(0)("grno"))
                            obj.GENo = clsCommon.myCstr(dt.Rows(0)("geno"))
                            obj.Against_RGP = clsCommon.myCstr(dt.Rows(0)("against_rgp_no"))
                            obj.Landed_Add_Cost = Nothing
                            obj.Total_Landed_Cost = Nothing
                            If clsCommon.myLen(obj.GENo) > 0 Then
                                If dt.Rows(0)("gedate") IsNot Nothing AndAlso Not IsDBNull(dt.Rows(0)("gedate")) AndAlso clsCommon.myLen(dt.Rows(0)("gedate")) > 0 AndAlso IsDate(dt.Rows(0)("gedate")) Then
                                    obj.GEDate = clsCommon.myCDate(dt.Rows(0)("gedate"))
                                End If
                            End If
                            obj.Item_Type = clsCommon.myCstr(dt.Rows(0)("item_type"))
                            obj.Dept = clsCommon.myCstr(dt.Rows(0)("dept"))
                            obj.Dept_Desc = clsCommon.myCstr(dt.Rows(0)("dept_desc"))

                            obj.Against_PO = clsCommon.myCstr(dt.Rows(0)("against_po"))
                            obj.Against_MRN = clsCommon.myCstr(dt.Rows(0)("mrn_no"))
                            obj.PROJECT_ID = Nothing
                            

                            obj.Against_Schedule_Code = clsCommon.myCstr(dt.Rows(0)("against_schedule_code"))
                            obj.PurchaseOrder_Type = clsCommon.myCstr(dt.Rows(0)("purchaseorder_type"))

                            If clsCommon.myLen(dt.Rows(0)("add_charge_code1")) > 0 Then
                                obj.Add_Charge_Code1 = clsCommon.myCstr(dt.Rows(0)("add_charge_code1"))
                                obj.Add_Charge_Name1 = clsCommon.myCstr(dt.Rows(0)("add_charge_name1"))
                                obj.Add_Charge_Amt1 = clsCommon.myCdbl(dt.Rows(0)("add_charge_amt1"))
                            End If
                            If clsCommon.myLen(dt.Rows(0)("add_charge_code2")) > 0 Then
                                obj.Add_Charge_Code2 = clsCommon.myCstr(dt.Rows(0)("add_charge_code2"))
                                obj.Add_Charge_Name2 = clsCommon.myCstr(dt.Rows(0)("add_charge_name2"))
                                obj.Add_Charge_Amt2 = clsCommon.myCdbl(dt.Rows(0)("add_charge_amt2"))
                            End If
                            If clsCommon.myLen(dt.Rows(0)("add_charge_code3")) > 0 Then
                                obj.Add_Charge_Code3 = clsCommon.myCstr(dt.Rows(0)("add_charge_code3"))
                                obj.Add_Charge_Name3 = clsCommon.myCstr(dt.Rows(0)("add_charge_name3"))
                                obj.Add_Charge_Amt3 = clsCommon.myCdbl(dt.Rows(0)("add_charge_amt3"))
                            End If
                            If clsCommon.myLen(dt.Rows(0)("add_charge_code4")) > 0 Then
                                obj.Add_Charge_Code4 = clsCommon.myCstr(dt.Rows(0)("add_charge_code4"))
                                obj.Add_Charge_Name4 = clsCommon.myCstr(dt.Rows(0)("add_charge_name4"))
                                obj.Add_Charge_Amt4 = clsCommon.myCdbl(dt.Rows(0)("add_charge_amt4"))
                            End If
                            If clsCommon.myLen(dt.Rows(0)("add_charge_code5")) > 0 Then
                                obj.Add_Charge_Code5 = clsCommon.myCstr(dt.Rows(0)("add_charge_code5"))
                                obj.Add_Charge_Name5 = clsCommon.myCstr(dt.Rows(0)("add_charge_name5"))
                                obj.Add_Charge_Amt5 = clsCommon.myCdbl(dt.Rows(0)("add_charge_amt5"))
                            End If
                            If clsCommon.myLen(dt.Rows(0)("add_charge_code6")) > 0 Then
                                obj.Add_Charge_Code6 = clsCommon.myCstr(dt.Rows(0)("add_charge_code6"))
                                obj.Add_Charge_Name6 = clsCommon.myCstr(dt.Rows(0)("add_charge_name6"))
                                obj.Add_Charge_Amt6 = clsCommon.myCdbl(dt.Rows(0)("add_charge_amt6"))
                            End If
                            If clsCommon.myLen(dt.Rows(0)("add_charge_code7")) > 0 Then
                                obj.Add_Charge_Code7 = clsCommon.myCstr(dt.Rows(0)("add_charge_code7"))
                                obj.Add_Charge_Name7 = clsCommon.myCstr(dt.Rows(0)("add_charge_name7"))
                                obj.Add_Charge_Amt7 = clsCommon.myCdbl(dt.Rows(0)("add_charge_amt7"))
                            End If
                            If clsCommon.myLen(dt.Rows(0)("add_charge_code8")) > 0 Then
                                obj.Add_Charge_Code8 = clsCommon.myCstr(dt.Rows(0)("add_charge_code8"))
                                obj.Add_Charge_Name8 = clsCommon.myCstr(dt.Rows(0)("add_charge_name8"))
                                obj.Add_Charge_Amt8 = clsCommon.myCdbl(dt.Rows(0)("add_charge_amt8"))
                            End If
                            If clsCommon.myLen(dt.Rows(0)("add_charge_code9")) > 0 Then
                                obj.Add_Charge_Code9 = clsCommon.myCstr(dt.Rows(0)("add_charge_code9"))
                                obj.Add_Charge_Name9 = clsCommon.myCstr(dt.Rows(0)("add_charge_name9"))
                                obj.Add_Charge_Amt9 = clsCommon.myCdbl(dt.Rows(0)("add_charge_amt9"))
                            End If
                            If clsCommon.myLen(dt.Rows(0)("add_charge_code10")) > 0 Then
                                obj.Add_Charge_Code10 = clsCommon.myCstr(dt.Rows(0)("add_charge_code10"))
                                obj.Add_Charge_Name10 = clsCommon.myCstr(dt.Rows(0)("add_charge_name10"))
                                obj.Add_Charge_Amt10 = clsCommon.myCdbl(dt.Rows(0)("add_charge_amt10"))
                            End If

                            obj.Total_Add_Charge = clsCommon.myCdbl(dt.Rows(0)("total_add_charge"))
                            obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic
                            obj.is_Excise_On_Qty = False
                            obj.IsAbatementPO = 0

                            If clsCommon.myLen(obj.Against_MRN) > 0 Then
                                obj.Against_GRN = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_GRN FROM TSPL_MRN_HEAD WHERE MRN_No='" + obj.Against_MRN + "'", trans))
                            End If
                            If clsCommon.myLen(obj.Against_GRN) > 0 Then
                                obj.Against_PO = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_PO FROM TSPL_GRN_HEAD WHERE GRN_No='" + obj.Against_GRN + "'", trans))
                                obj.Against_Schedule_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_Schedule_Code FROM TSPL_GRN_HEAD WHERE GRN_No='" + obj.Against_GRN + "'", trans))
                                obj.Against_RGP = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_RGP_No FROM TSPL_GRN_HEAD WHERE GRN_No='" + obj.Against_GRN + "'", trans))
                            End If
                            If clsCommon.myLen(obj.Against_RGP) > 0 Then
                                obj.Against_Schedule_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_Schedule_Code FROM TSPL_RGP_HEAD WHERE RGP_No='" + obj.Against_RGP + "'", trans))
                                obj.Against_PO = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT PO_Id FROM TSPL_RGP_HEAD WHERE RGP_No='" + obj.Against_RGP + "'", trans))
                            End If
                            If clsCommon.myLen(obj.Against_Schedule_Code) > 0 Then
                                obj.Against_PO = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT PO_Code FROM TSPL_PO_SCH_HEAD WHERE document_code='" + obj.Against_Schedule_Code + "'", trans))
                            End If

                            If clsCommon.myLen(obj.Against_PO) > 0 Then
                                obj.Against_Requisition = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Against_Requisition FROM TSPL_PURCHASE_ORDER_HEAD WHERE PurchaseOrder_No='" + obj.Against_PO + "'", trans))
                            End If


                            '' CurrencConversion
                            If clsCommon.myLen(dt.Rows(0)("currency_code")) > 0 Then
                                obj.CURRENCY_CODE = clsCommon.myCstr(dt.Rows(0)("currency_code"))
                                obj.ConvRate = clsCommon.myCdbl(dt.Rows(0)("convrate"))

                                If clsCommon.myLen(dt.Rows(0)("applicablefrom")) > 0 Then
                                    obj.ApplicableFrom = clsCommon.GetPrintDate(clsCommon.myCstr(dt.Rows(0)("applicablefrom")), "dd/MMM/yyyy")
                                Else
                                    obj.ApplicableFrom = Nothing
                                End If
                            Else
                                obj.CURRENCY_CODE = Nothing
                                obj.ConvRate = 1
                                obj.ApplicableFrom = Nothing
                            End If
                            '' end CurrencyConversion
                            obj.against_roadpermit = "0"
                            obj.Arr_Road = New List(Of clsSRNRoadPermitDetail)
                            obj.agnst_cform = "0"
                            obj.Arr_CFORM = New List(Of clsSRNCFORMDetail)


                            obj.Arr = New List(Of clsSRNDetail)

                            ''query for detail
                            Dim AllowSRNWithoutShortageRejection As Integer = 0
                            qry = "select TSPL_MRN_DETAIL.Row_Type,TSPL_MRN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Weight_UOM,TSPL_ITEM_MASTER.Weight_Value,TSPL_ITEM_MASTER.Item_Desc,sum(QC_Final.ok_qty) as ok_qty,sum(QC_Final.rej_qty) as rej_qty,TSPL_MRN_DETAIL.Unit_code,TSPL_MRN_DETAIL.PO_ID,TSPL_MRN_DETAIL.MRN_No,TSPL_MRN_DETAIL.GRN_Id,TSPL_MRN_DETAIL.TAX1,TSPL_MRN_DETAIL.TAX1_Rate,TSPL_MRN_DETAIL.TAX2,TSPL_MRN_DETAIL.TAX2_Rate,TSPL_MRN_DETAIL.TAX3,TSPL_MRN_DETAIL.TAX3_Rate,TSPL_MRN_DETAIL.TAX4,TSPL_MRN_DETAIL.TAX4_Rate,TSPL_MRN_DETAIL.TAX5,TSPL_MRN_DETAIL.TAX5_Rate,TSPL_MRN_DETAIL.TAX6,TSPL_MRN_DETAIL.TAX6_Rate,TSPL_MRN_DETAIL.TAX7,TSPL_MRN_DETAIL.TAX7_Rate,TSPL_MRN_DETAIL.TAX8,TSPL_MRN_DETAIL.TAX8_Rate,TSPL_MRN_DETAIL.TAX9,TSPL_MRN_DETAIL.TAX9_Rate,TSPL_MRN_DETAIL.TAX10,TSPL_MRN_DETAIL.TAX10_Rate,TSPL_MRN_DETAIL.MRP,TSPL_MRN_DETAIL.Batch_No,TSPL_MRN_DETAIL.Expiry_Date,TSPL_MRN_DETAIL.Specification,TSPL_MRN_DETAIL.Remarks " & _
                                ",sum(isnull(TSPL_MRN_DETAIL.Item_Cost,0))/COUNT(TSPL_MRN_DETAIL.Item_Code) as Item_Cost,sum(isnull(TSPL_MRN_DETAIL.Amount,0)) as Amount,sum(isnull(TSPL_MRN_DETAIL.Disc_Per,0))/COUNT(TSPL_MRN_DETAIL.Item_Code) as Disc_Per,sum(isnull(TSPL_MRN_DETAIL.Disc_Amt,0)) as Disc_Amt,sum(isnull(TSPL_MRN_DETAIL.Amt_Less_Discount,0)) as Amt_Less_Discount,sum(isnull(TSPL_MRN_DETAIL.TAX1_Amt,0)) as TAX1_Amt,sum(isnull(TSPL_MRN_DETAIL.TAX1_Base_Amt,0)) as TAX1_Base_Amt,sum(isnull(TSPL_MRN_DETAIL.TAX2_Amt,0)) as TAX2_Amt,sum(isnull(TSPL_MRN_DETAIL.TAX2_Base_Amt,0)) as TAX2_Base_Amt,sum(isnull(TSPL_MRN_DETAIL.TAX3_Amt,0)) as TAX3_Amt,sum(isnull(TSPL_MRN_DETAIL.TAX3_Base_Amt,0)) as TAX3_Base_Amt,sum(isnull(TSPL_MRN_DETAIL.TAX4_Amt,0)) as TAX4_Amt,sum(isnull(TSPL_MRN_DETAIL.TAX4_Base_Amt,0)) as TAX4_Base_Amt,sum(isnull(TSPL_MRN_DETAIL.TAX5_Amt,0)) as TAX5_Amt,sum(isnull(TSPL_MRN_DETAIL.TAX5_Base_Amt,0)) as TAX5_Base_Amt,sum(isnull(TSPL_MRN_DETAIL.TAX6_Amt,0)) as TAX6_Amt,sum(isnull(TSPL_MRN_DETAIL.TAX6_Base_Amt,0)) as TAX6_Base_Amt,sum(isnull(TSPL_MRN_DETAIL.TAX7_Amt,0)) as TAX7_Amt,sum(isnull(TSPL_MRN_DETAIL.TAX7_Base_Amt,0)) as TAX7_Base_Amt,sum(isnull(TSPL_MRN_DETAIL.TAX8_Amt,0)) as TAX8_Amt,sum(isnull(TSPL_MRN_DETAIL.TAX8_Base_Amt,0)) as TAX8_Base_Amt,sum(isnull(TSPL_MRN_DETAIL.TAX9_Amt,0)) as TAX9_Amt,sum(isnull(TSPL_MRN_DETAIL.TAX9_Base_Amt,0)) as TAX9_Base_Amt,sum(isnull(TSPL_MRN_DETAIL.TAX10_Amt,0)) as TAX10_Amt,sum(isnull(TSPL_MRN_DETAIL.TAX10_Base_Amt,0)) as TAX10_Base_Amt,sum(isnull(TSPL_MRN_DETAIL.Total_Tax_Amt,0)) as Total_Tax_Amt,sum(isnull(TSPL_MRN_DETAIL.Item_Net_Amt,0)) as Item_Net_Amt,sum(isnull(TSPL_MRN_DETAIL.AssessableAmt,0)) as AssessableAmt,sum(isnull(TSPL_MRN_DETAIL.Leak_Qty,0)) as Leak_Qty,sum(isnull(TSPL_MRN_DETAIL.Burst_Qty,0)) as Burst_Qty,sum(isnull(TSPL_MRN_DETAIL.Short_Qty,0)) as Short_Qty,sum(isnull(TSPL_MRN_DETAIL.MRN_Qty,0)) as MRN_Qty " & _
                                " ,max(TSPL_MRN_DETAIL.Category) as Category ,max(TSPL_MRN_DETAIL.Emergency) as Emergency,max(TSPL_MRN_DETAIL.Capex_Code) as Capex_Code,max(TSPL_MRN_DETAIL.Capex_SubCode) as Capex_SubCode,max(TSPL_MRN_DETAIL.Taxable_Amount_Per) as Taxable_Amount_Per  " & _
                                " from TSPL_MRN_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_MRN_DETAIL.Item_Code right outer join " & _
                                "(select finalqc.mrn_no,finalqc.PONO,finalqc.item_code,finalqc.unit_code,sum(finalqc.ok_qty) as ok_qty,sum(finalqc.reject_qty) as rej_qty,max(finalqc.remarks) as remarks from ( " & _
                                "select TSPL_QC_CHECK_DETAIL.PO_No AS PONO,TSPL_QC_CHECK_DETAIL.mrn_no,TSPL_QC_CHECK_DETAIL.item_code,TSPL_QC_CHECK_DETAIL.unit_code,isnull(TSPL_QC_CHECK_DETAIL.ok_qty,0) as ok_qty,isnull(TSPL_QC_CHECK_DETAIL.reject_qty,0) as reject_qty,(case when len(isnull(TSPL_QC_CHECK_DETAIL.Reject_Remarks,''))>0 then TSPL_QC_CHECK_DETAIL.Reject_Remarks else TSPL_QC_CHECK_DETAIL.remarks end) as remarks from TSPL_QC_CHECK_DETAIL where document_code in (" + clsCommon.GetMulcallString(arrQcNo) + ") and TSPL_QC_CHECK_DETAIL.mrn_no in (" + clsCommon.myCstr(drMRN("mrn_no")) + ") and coalesce(TSPL_QC_CHECK_DETAIL.PO_No,'') in (" + clsCommon.GetMulcallString(arrPONO) + ") " & _
                                "union all select PONO AS PONO,mrn_no,(icode) as item_code,(unit) as unit_code,SUM((Qty *RI)- Unapproved-DamageQty) as ok_qty,0 as reject_qty,'' as remarks from ( " & _
                                "select TSPL_MRN_DETAIL.Item_Code as ICode,TSPL_MRN_DETAIL.MRN_Qty as Qty,0 as Unapproved,TSPL_MRN_DETAIL.Unit_Code as Unit,1 as RI,1 as Chk,TSPL_MRN_Head.MRN_Date as TransDate,ISNULL(TSPL_MRN_DETAIL.Assessable,0) AS Assessable,ISNULL(TSPL_MRN_DETAIL.MRP,0) as MRP,0 as DamageQty,pod.Abatementrate,TSPL_MRN_HEAD.MRN_No,gh.GRN_No,MRN_Date,Grn_date,TSPL_MRN_DETAIL.PO_ID  AS PONO from TSPL_MRN_DETAIL left outer join TSPL_MRN_Head on TSPL_MRN_Head.mrn_no=TSPL_MRN_DETAIL.MRN_No left join TSPL_GRN_HEAD gh on gh.GRN_No=TSPL_MRN_Head.Against_GRN left join TSPL_PURCHASE_ORDER_DETAIL pod on coalesce(TSPL_MRN_DETAIL.PO_ID,'')=coalesce(pod.PurchaseOrder_No,'') and pod.Item_Code=TSPL_MRN_DETAIL.Item_Code left join TSPL_SRN_DETAIL sd on sd.MRN_Id=TSPL_MRN_DETAIL.MRN_No and sd.Item_Code=TSPL_MRN_DETAIL.Item_Code where TSPL_MRN_DETAIL.Status=0 and TSPL_MRN_Head.Status=1  and coalesce(sd.SRN_Qty,0) < coalesce(TSPL_MRN_DETAIL.MRN_Qty,0) and tspl_mrn_head.mrn_no in (" + clsCommon.myCstr(drMRN("mrn_no")) + ") and TSPL_MRN_DETAIL.Item_Code not in (" + clsCommon.GetMulcallString(arrIcode) + ") " & _
                                "union all select TSPL_SRN_DETAIL.Item_Code as ICode,(TSPL_SRN_DETAIL.SRN_Qty+TSPL_SRN_DETAIL.Leak_Qty+TSPL_SRN_DETAIL.Burst_Qty+TSPL_SRN_DETAIL.Short_Qty+TSPL_SRN_DETAIL.Rejected_Qty) as Qty,0 as Unapproved,TSPL_SRN_DETAIL.Unit_code as Unit,-1 as RI,0 as Chk,null as TransDate,isnull(TSPL_SRN_DETAIL.Assessable,0) as Assessable,isnull(TSPL_SRN_DETAIL.MRP,0) as MRP,(isnull(TSPL_SRN_DETAIL.Leak_Qty,0)+isnull(TSPL_SRN_DETAIL.Burst_Qty,0) +isnull(TSPL_SRN_DETAIL.Short_Qty,0)) as DamageQty,TSPL_SRN_DETAIL.AbatementRate,TSPL_SRN_DETAIL.MRN_Id as MRN_No,'',null,null,TSPL_SRN_DETAIL.PO_ID  AS PONO  from TSPL_SRN_DETAIL  left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  where TSPL_SRN_HEAD.Status=1 and TSPL_SRN_DETAIL.MRN_Id in (" + clsCommon.myCstr(drMRN("mrn_no")) + ") and tspl_srn_detail.item_code not in (" + clsCommon.GetMulcallString(arrIcode) + ") " & _
                                "union all select TSPL_SRN_DETAIL.Item_Code as ICode,0  as Qty,(TSPL_SRN_DETAIL.SRN_Qty+TSPL_SRN_DETAIL.Leak_Qty+TSPL_SRN_DETAIL.Burst_Qty+TSPL_SRN_DETAIL.Short_Qty+TSPL_SRN_DETAIL.Rejected_Qty) as Unapproved,TSPL_SRN_DETAIL.Unit_code as Unit,-1 as RI,0 as Chk,null as TransDate,isnull(TSPL_SRN_DETAIL.Assessable,0) as Assessable,isnull(TSPL_SRN_DETAIL.MRP,0) as MRP,(isnull(TSPL_SRN_DETAIL.Leak_Qty,0)+isnull(TSPL_SRN_DETAIL.Burst_Qty,0) +isnull(TSPL_SRN_DETAIL.Short_Qty,0)) as DamageQty,TSPL_SRN_DETAIL.AbatementRate,TSPL_SRN_DETAIL.MRN_Id as MRN_No,'',null,null,TSPL_SRN_DETAIL.PO_ID  AS PONO  from TSPL_SRN_DETAIL  left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  where TSPL_SRN_HEAD.Status=0 and TSPL_SRN_DETAIL.MRN_Id in (" + clsCommon.myCstr(drMRN("mrn_no")) + ") and tspl_srn_detail.item_code not in (" + clsCommon.GetMulcallString(arrIcode) + ") " & _
                                ")Final GROUP BY MRN_NO,ICode,PONO,Unit,MRP having SUM(Chk)>0 and SUM((Qty *RI)-Unapproved-DamageQty) <>0 )finalqc group by finalqc.mrn_no,finalqc.item_code,finalqc.unit_code,finalqc.PONO)QC_Final on QC_Final.MRN_No=TSPL_MRN_DETAIL.MRN_No and QC_Final.Item_Code=TSPL_MRN_DETAIL.Item_Code and QC_Final.Unit_Code=TSPL_MRN_DETAIL.Unit_code and coalesce(QC_Final.PONO,'') = coalesce(TSPL_MRN_DETAIL.PO_ID,'') " & _
                                "where tspl_mrn_detail.mrn_no in (" + clsCommon.myCstr(drMRN("mrn_no")) + ") group by TSPL_MRN_DETAIL.Row_Type,TSPL_MRN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Weight_UOM,TSPL_ITEM_MASTER.Weight_Value,TSPL_ITEM_MASTER.Item_Desc,TSPL_MRN_DETAIL.Unit_code,TSPL_MRN_DETAIL.PO_ID,TSPL_MRN_DETAIL.MRN_No,TSPL_MRN_DETAIL.GRN_Id,TSPL_MRN_DETAIL.TAX1,TSPL_MRN_DETAIL.TAX1_Rate,TSPL_MRN_DETAIL.TAX2,TSPL_MRN_DETAIL.TAX2_Rate,TSPL_MRN_DETAIL.TAX3,TSPL_MRN_DETAIL.TAX3_Rate,TSPL_MRN_DETAIL.TAX4,TSPL_MRN_DETAIL.TAX4_Rate,TSPL_MRN_DETAIL.TAX5,TSPL_MRN_DETAIL.TAX5_Rate,TSPL_MRN_DETAIL.TAX6,TSPL_MRN_DETAIL.TAX6_Rate,TSPL_MRN_DETAIL.TAX7,TSPL_MRN_DETAIL.TAX7_Rate,TSPL_MRN_DETAIL.TAX8,TSPL_MRN_DETAIL.TAX8_Rate,TSPL_MRN_DETAIL.TAX9,TSPL_MRN_DETAIL.TAX9_Rate,TSPL_MRN_DETAIL.TAX10,TSPL_MRN_DETAIL.TAX10_Rate,TSPL_MRN_DETAIL.MRP,TSPL_MRN_DETAIL.Batch_No,TSPL_MRN_DETAIL.Expiry_Date,TSPL_MRN_DETAIL.Specification,TSPL_MRN_DETAIL.Remarks "
                            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                                For Each dr As DataRow In dt1.Rows
                                    objTr = New clsSRNDetail()
                                    counter += 1

                                    objTr.Line_No = counter
                                    objTr.Row_Type = clsCommon.myCstr(dr("row_type"))
                                    objTr.Item_Code = clsCommon.myCstr(dr("item_code"))
                                    AllowSRNWithoutShortageRejection = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select AllowSRNWithoutShortReject from tspl_Item_Master where Item_code='" & clsCommon.myCstr(objTr.Item_Code) & "'", trans))
                                    objTr.UOMWeight = clsCommon.myCstr(dr("weight_uom"))
                                    objTr.UOMWeightValue = clsCommon.myCdbl(dr("weight_value"))
                                    objTr.Item_Desc = clsCommon.myCstr(dr("item_desc"))
                                    objTr.Bar_Code = Nothing
                                    objTr.SRN_Qty = clsCommon.myCdbl(dr("ok_qty"))
                                    objTr.Rejected_Qty = clsCommon.myCdbl(dr("rej_qty"))
                                    objTr.Freeqty = 0
                                    objTr.Unit_code = clsCommon.myCstr(dr("unit_code"))
                                    objTr.PO_ID = clsCommon.myCstr(dr("po_id"))
                                    objTr.MRN_Id = clsCommon.myCstr(dr("mrn_no"))
                                    objTr.Category = clsCommon.myCstr(dr("Category"))
                                    objTr.Emergency = IIf(clsCommon.myCdbl(dr("Emergency")) = 1, True, False)
                                    objTr.Capex_Code = clsCommon.myCstr(dr("Capex_Code"))
                                    objTr.Capex_SubCode = clsCommon.myCstr(dr("Capex_SubCode"))
                                    objTr.GRN_ID = clsCommon.myCstr(dr("grn_id"))
                                    If clsCommon.myLen(objTr.MRN_Id) > 0 AndAlso clsCommon.myLen(obj.Against_MRN) <= 0 Then
                                        obj.Against_MRN = objTr.MRN_Id
                                    End If

                                    objTr.RGP_Id = Nothing
                                    objTr.Req_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select top 1 isnull(Requisition_Id,'')  from tspl_purchase_order_detail where purchaseOrder_No='" & objTr.PO_ID & "'  and Item_code='" & objTr.Item_Code & "'", trans))
                                    objTr.Item_Cost = clsCommon.myCdbl(dr("item_cost"))
                                    objTr.Amount = clsCommon.myCdbl(dr("amount"))
                                    objTr.Disc_Type = "0"
                                    objTr.Disc_Per = clsCommon.myCdbl(dr("disc_per"))
                                    objTr.Disc_Amt = clsCommon.myCdbl(dr("disc_amt"))
                                    objTr.Amt_Less_Discount = clsCommon.myCdbl(dr("amt_less_discount"))
                                    objTr.Taxable_Amount_Per = clsCommon.myCdbl(dr("Taxable_Amount_Per"))
                                    objTr.Taxable_Amount = objTr.Taxable_Amount_Per * objTr.Amt_Less_Discount / 100
                                    obj.Total_Taxable_Amount += objTr.Taxable_Amount
                                    objTr.TAX1 = clsCommon.myCstr(dr("tax1"))
                                    objTr.TAX1_Base_Amt = clsCommon.myCdbl(dr("tax1_base_amt"))
                                    objTr.TAX1_Rate = clsCommon.myCdbl(dr("tax1_rate"))
                                    objTr.TAX1_Amt = clsCommon.myCdbl(dr("tax1_amt"))
                                    objTr.TAX2 = clsCommon.myCstr(dr("tax2"))
                                    objTr.TAX2_Base_Amt = clsCommon.myCdbl(dr("tax2_base_amt"))
                                    objTr.TAX2_Rate = clsCommon.myCdbl(dr("tax2_rate"))
                                    objTr.TAX2_Amt = clsCommon.myCdbl(dr("tax2_amt"))
                                    objTr.TAX3 = clsCommon.myCstr(dr("tax3"))
                                    objTr.TAX3_Base_Amt = clsCommon.myCdbl(dr("tax3_base_amt"))
                                    objTr.TAX3_Rate = clsCommon.myCdbl(dr("tax3_rate"))
                                    objTr.TAX3_Amt = clsCommon.myCdbl(dr("tax3_amt"))
                                    objTr.TAX4 = clsCommon.myCstr(dr("tax4"))
                                    objTr.TAX4_Base_Amt = clsCommon.myCdbl(dr("tax4_base_amt"))
                                    objTr.TAX4_Rate = clsCommon.myCdbl(dr("tax4_rate"))
                                    objTr.TAX4_Amt = clsCommon.myCdbl(dr("tax4_amt"))
                                    objTr.TAX5 = clsCommon.myCstr(dr("tax5"))
                                    objTr.TAX5_Base_Amt = clsCommon.myCdbl(dr("tax5_base_amt"))
                                    objTr.TAX5_Rate = clsCommon.myCdbl(dr("tax5_rate"))
                                    objTr.TAX5_Amt = clsCommon.myCdbl(dr("tax5_amt"))
                                    objTr.TAX6 = clsCommon.myCstr(dr("tax6"))
                                    objTr.TAX6_Base_Amt = clsCommon.myCdbl(dr("tax6_base_amt"))
                                    objTr.TAX6_Rate = clsCommon.myCdbl(dr("tax6_rate"))
                                    objTr.TAX6_Amt = clsCommon.myCdbl(dr("tax6_amt"))
                                    objTr.TAX7 = clsCommon.myCstr(dr("tax7"))
                                    objTr.TAX7_Base_Amt = clsCommon.myCdbl(dr("tax7_base_amt"))
                                    objTr.TAX7_Rate = clsCommon.myCdbl(dr("tax7_rate"))
                                    objTr.TAX7_Amt = clsCommon.myCdbl(dr("tax7_amt"))
                                    objTr.TAX8 = clsCommon.myCstr(dr("tax8"))
                                    objTr.TAX8_Base_Amt = clsCommon.myCdbl(dr("tax8_base_amt"))
                                    objTr.TAX8_Rate = clsCommon.myCdbl(dr("tax8_rate"))
                                    objTr.TAX8_Amt = clsCommon.myCdbl(dr("tax8_amt"))
                                    objTr.TAX9 = clsCommon.myCstr(dr("tax9"))
                                    objTr.TAX9_Base_Amt = clsCommon.myCdbl(dr("tax9_base_amt"))
                                    objTr.TAX9_Rate = clsCommon.myCdbl(dr("tax9_rate"))
                                    objTr.TAX9_Amt = clsCommon.myCdbl(dr("tax9_amt"))
                                    objTr.TAX10 = clsCommon.myCstr(dr("tax10"))
                                    objTr.TAX10_Base_Amt = clsCommon.myCdbl(dr("tax10_base_amt"))
                                    objTr.TAX10_Rate = clsCommon.myCdbl(dr("tax10_rate"))
                                    objTr.TAX10_Amt = clsCommon.myCdbl(dr("tax10_amt"))
                                    objTr.Total_Tax_Amt = clsCommon.myCdbl(dr("total_tax_amt"))
                                    objTr.Item_Net_Amt = clsCommon.myCdbl(dr("item_net_amt"))
                                    objTr.Location = obj.Bill_To_Location

                                    objTr.Landed_Cost_Rate = Nothing
                                    objTr.Landed_Cost_Amount = Nothing

                                    objTr.MRP = clsCommon.myCdbl(dr("mrp"))
                                    objTr.AssessableAmt = clsCommon.myCdbl(dr("assessableamt"))
                                    objTr.Batch_No = clsCommon.myCstr(dr("batch_no"))
                                    objTr.Bin_No = Nothing
                                    If dr("expiry_date") IsNot Nothing AndAlso Not IsDBNull(dr("expiry_date")) AndAlso clsCommon.myLen(dr("expiry_date")) > 0 AndAlso IsDate(dr("expiry_date")) Then
                                        objTr.Expiry_Date = clsCommon.GetPrintDate(clsCommon.myCDate(dr("expiry_date")), "dd-MM-yyyy")
                                    End If
                                    objTr.MFG_Date = Nothing
                                    objTr.Specification = clsCommon.myCstr(dr("specification"))
                                    objTr.Remarks = clsCommon.myCstr(dr("remarks"))
                                    objTr.Is_Mannual_Amt = Nothing
                                    objTr.Fater_Code = Nothing
                                    objTr.Fater_Rate = Nothing
                                    objTr.Fater_Amt = Nothing


                                    objTr.Leak_Qty = clsCommon.myCdbl(dr("leak_qty"))
                                    objTr.Burst_Qty = clsCommon.myCdbl(dr("burst_qty"))
                                    objTr.Short_Qty = clsCommon.myCdbl(dr("short_qty"))
                                    If AllowSRNWithoutShortageRejection = 1 Then
                                        'objTr.SRN_Qty = clsCommon.myCdbl(dr("ok_qty")) + objTr.Rejected_Qty + objTr.Short_Qty
                                        objTr.Rejected_Qty = 0
                                        objTr.Short_Qty = 0
                                    End If
                                    objTr.Balance_Qty = objTr.SRN_Qty + objTr.Rejected_Qty


                                    objTr.PO_Qty = clsPurchaseOrderDetail.GetBalancePOQtyByGRN(objTr.PO_ID, objTr.Item_Code, objTr.GRN_ID, objTr.Unit_code, objTr.MRP, objTr.Assessable, trans)
                                    objTr.GRN_Qty = clsGRNDetail.GetBalanceGRNQty(objTr.GRN_ID, objTr.Item_Code, objTr.MRN_Id, objTr.Unit_code, objTr.MRP, objTr.Assessable, trans, clsCommon.myCstr(dr("po_id")))
                                    objTr.MRN_Qty = clsCommon.myCdbl(dr("mrn_qty"))

                                    objTr.RGP_Qty = Nothing
                                    objTr.Schedule_Qty = Nothing
                                    objTr.Against_Schedule_Code = Nothing

                                    objTr.Total_AddtionalCost_PerUnit = Nothing
                                    objTr.Total_NonRecTax_PerUnit = Nothing
                                    objTr.Total_RecTax_PerUnit = Nothing

                                    objTr.AbatementRate = Nothing
                                    objTr.AssessableMRP = Nothing
                                    objTr.TotalAssessableMRP = Nothing

                                    If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                                        obj.Arr.Add(objTr)
                                    End If
                                Next
                            End If ''dt1 cond.

                            If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                                Throw New Exception("No record found for Auto SRN.")
                            End If

                            obj.Document_Type = "SRN"
                            isSaved = isSaved AndAlso obj.SaveData(obj, True, trans)

                            If isSaved Then

                                qry = "update TSPL_QC_CHECK_MRN_DETAIL set SRN_ID='" + obj.SRN_No + "' where document_code in (" + clsCommon.GetMulcallString(arrQcNo) + ") and mrn_no in (" + mrnno + ")"
                                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

                                qry = "UPDATE TSPL_QC_CHECK_APPROVAL_ENTRY SET  TSPL_QC_CHECK_APPROVAL_ENTRY.SRN_Id=TSPL_QC_CHECK_MRN_DETAIL.SRN_Id from TSPL_QC_CHECK_APPROVAL_ENTRY left outer join TSPL_QC_CHECK_MRN_DETAIL on  TSPL_QC_CHECK_APPROVAL_ENTRY.Document_Code= TSPL_QC_CHECK_MRN_DETAIL.Document_Code where TSPL_QC_CHECK_APPROVAL_ENTRY.document_code in (" + clsCommon.GetMulcallString(arrQcNo) + ")"
                                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

                                qry = "update TSPL_SRN_HEAD set against_Qc_code=(select max(document_code) from TSPL_QC_CHECK_MRN_DETAIL where TSPL_QC_CHECK_MRN_DETAIL.SRN_Id=TSPL_SRN_HEAD.SRN_No) where len(ISNULL(against_qc_code,''))<=0 "
                                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

                                qry = "update TSPL_SRN_HEAD set against_qc_date=convert(varchar,(select max(document_date) from TSPL_QC_CHECK_HEAD where TSPL_QC_CHECK_HEAD.document_code=TSPL_SRN_HEAD.against_Qc_code),103) where len(ISNULL(against_qc_date,''))<=0 "
                                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                            End If

                        End If ''dt cond.

                    Next ''drMRN cond.
                End If ''end dt_mrn cond.




            End If ''obj cond.

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            qry = Nothing
            dt = Nothing
            dt_MRN = Nothing
            dr_MRN = Nothing
            arrIcode = Nothing
            obj = Nothing
            objTr = Nothing
        End Try
    End Function

    Public Shared Function GetData(Optional ByVal trans As SqlTransaction = Nothing) As clsQualityCheckApprovalForSRN
        Dim qry As String = ""
        Dim dt As New DataTable()
        Dim objtr As New clsQualityCheckApprovalForSRN()
        Try
            Dim obj As New clsQualityCheckApprovalForSRN()
            obj.Arr = New List(Of clsQualityCheckApprovalForSRN)

            qry = "select distinct TSPL_QC_CHECK_HEAD.*,TSPL_QC_CHECK_DETAIL.Item_Code,Item_Desc,tspl_vendor_master.vendor_name ,Deduction from TSPL_QC_CHECK_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_QC_CHECK_DETAIL.Item_Code left outer join  TSPL_QC_CHECK_HEAD on TSPL_QC_CHECK_DETAIL.Document_Code=TSPL_QC_CHECK_HEAD.Document_Code   left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_QC_CHECK_HEAD.vendor_code left outer join  (select Document_Code,sum(InputDataDeductionPer) as deduction from TSPL_QC_CHECK_sRN_DETAIL 
                   group by Document_Code) as TSPL_QC_CHECK_MRN_DETAIL on TSPL_QC_CHECK_MRN_DETAIL.Document_Code=TSPL_QC_CHECK_DETAIL.Document_Code"

            qry += " where isnull(TSPL_QC_CHECK_HEAD.Approved_For_SRN,0)<>1 and TSPL_QC_CHECK_HEAD.posted=1  and (2 = case when TSPL_QC_CHECK_DETAIL.QC_Status ='Rejected' then case when isnull(TSPL_ITEM_MASTER.Is_Power_And_Fuel,0)=0 then 2 else 3 end  else 2 end)"

            If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                qry += "  and TSPL_QC_CHECK_HEAD.Bill_To_location in (" + objCommonVar.strCurrUserLocations + ")"
            End If

            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    objtr = New clsQualityCheckApprovalForSRN()

                    objtr.Document_Code = clsCommon.myCstr(dr("Document_Code"))
                    objtr.Document_Date = clsCommon.myCDate(dr("Document_Date"))
                    objtr.Description = clsCommon.myCstr(dr("Description"))
                    objtr.Vendor_Code = clsCommon.myCstr(dr("Vendor_Code"))
                    objtr.Vendor_Name = clsCommon.myCstr(dr("Vendor_Name"))
                    objtr.Bill_To_Location = clsCommon.myCstr(dr("bill_to_location"))
                    objtr.QC_Status = clsCommon.myCstr(dr("QC_Status"))
                    objtr.SRN_Type = clsCommon.myCstr(dr("SRN_Type"))
                    objtr.Item_Type = clsCommon.myCstr(dr("item_type"))
                    objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objtr.Item_desc = clsCommon.myCstr(dr("Item_desc"))
                    objtr.Deduction = clsCommon.myCstr(dr("Deduction"))


                    obj.Arr.Add(objtr)
                Next
            End If

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            qry = Nothing
            dt = Nothing
            objtr = Nothing
        End Try
    End Function
End Class
