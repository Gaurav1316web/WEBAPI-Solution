Imports common
Imports System.IO
Imports Telerik.Charting
 

Public Class FrmBIReport
    Inherits FrmMainTranScreen
#Region "Variables"

    Public obj As clsCreateBIReport
    Public strOuterOrderBy As String = ""
    Public isDrillDown As Boolean = False
    Public isLoadData As Boolean = True

#End Region

    Private Sub FrmBIReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        isLoadData = True
        SetUserMgmtNew()
        Try
            If clsCommon.CompairString(obj.Code, "CRRTRENDB") = CompairStringResult.Equal Then
                txtToDate.Enabled = False
            ElseIf clsCommon.CompairString(obj.Code, "CRRTRENDL") = CompairStringResult.Equal Then
                txtToDate.Enabled = False
            End If
            Dim qry As String = ""
            SplitContainer3.Panel1Collapsed = True
            GroupBox5.Text = ""
            RadPageView1.SelectedPage = RadPageView1.Pages("RadPageViewPage1")
            If obj IsNot Nothing Then
                Me.Text = obj.Description
                If clsCommon.CompairString(obj.Type, "Grid") = CompairStringResult.Equal Then
                    RadPageView1.Pages("RadPageViewPage3").Item.Visibility = Telerik.WinControls.ElementVisibility.Collapsed
                    RadPageView1.Pages("RadPageViewPage4").Item.Visibility = Telerik.WinControls.ElementVisibility.Collapsed
                ElseIf clsCommon.CompairString(obj.Type, "Pivot Grid") = CompairStringResult.Equal Then
                    RadPageView1.Pages("RadPageViewPage2").Item.Visibility = Telerik.WinControls.ElementVisibility.Collapsed
                    RadPageView1.Pages("RadPageViewPage4").Item.Visibility = Telerik.WinControls.ElementVisibility.Collapsed
                ElseIf clsCommon.CompairString(obj.Type, "Chart") = CompairStringResult.Equal Then
                    RadPageView1.Pages("RadPageViewPage3").Item.Visibility = Telerik.WinControls.ElementVisibility.Collapsed
                    RadPageView1.Pages("RadPageViewPage2").Item.Visibility = Telerik.WinControls.ElementVisibility.Collapsed
                End If

                Dim isDateFilterExist As Boolean = False
                Dim ii As Integer = 0
                Dim arrTreeLevel As New Dictionary(Of Integer, String)
                For Each objtr As clsCreateBIReportFilterDetails In obj.arr
                    If objtr.Is_Select Then
                        If objtr.Is_Date_Range Then
                            If Not isDrillDown Then
                                txtToDate.Value = clsCommon.GETSERVERDATE()
                                If clsCommon.CompairString(obj.Code, "CRRTRENDB") = CompairStringResult.Equal Then
                                    txtFromDate.Value = txtToDate.Value.AddDays(-10)
                                ElseIf clsCommon.CompairString(obj.Code, "CRRTRENDL") = CompairStringResult.Equal Then
                                    txtFromDate.Value = txtToDate.Value.AddDays(-10)
                                Else
                                    txtFromDate.Value = txtToDate.Value.AddMonths(-1)
                                End If

                            End If
                            txtFromDate.Tag = objtr.Filter_Column
                            txtToDate.Tag = objtr.Filter_Column
                            isDateFilterExist = True
                            Continue For
                        End If

                        ii += 1
                        Dim strValueMembter As String = ""
                        Dim strDisplayMembter As String = ""
                        Dim strParentValue As String = ""
                        Dim isLocationSecurity As Boolean = False
                        qry = clsCreateBIFilter.GetQuery(objtr.Against_Filter, strValueMembter, strDisplayMembter, strParentValue, isLocationSecurity)

                        If clsCommon.myLen(qry) > 0 Then
                            If objtr.Tree_Level > 0 Then
                                SplitContainer3.Panel1Collapsed = False
                                GroupBox5.Text += " " + objtr.Filter_Column
                                cbt.ValueMember = strValueMembter
                                cbt.DisplayMember = strDisplayMembter
                                cbt.ParentValue = strParentValue
                                cbt.DataSource = clsDBFuncationality.GetDataTable(qry)
                                arrTreeLevel.Add(objtr.Tree_Level, objtr.Filter_Column)
                            Else
                                Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry)
                                If isLocationSecurity Then
                                    Dim obj As New clsMCCCodes()
                                    obj = clsMCCCodes.GetData()
                                    If obj.arrLocCodeListOfString IsNot Nothing AndAlso obj.arrLocCodeListOfString.Count > 0 Then
                                        For jj As Integer = dtNew.Rows.Count - 1 To 0 Step -1
                                            If Not obj.arrLocCodeListOfString.Contains(clsCommon.myCstr(dtNew.Rows(jj)(strValueMembter))) Then
                                                dtNew.Rows.Remove(dtNew.Rows(jj))
                                            End If
                                        Next
                                    End If
                                End If
                                If ii = 1 Then
                                    GroupBox1.Text = objtr.Filter_Column
                                    GroupBox1.Tag = isLocationSecurity
                                    cbg1.DataSource = dtNew
                                    cbg1.ValueMember = strValueMembter
                                    cbg1.Tag = objtr.Filter_Column
                                    GroupBox1.Visible = True
                                    If isDrillDown Then
                                        If objtr.tempObj IsNot Nothing Then
                                            rbtnGrp1Select.IsChecked = True
                                            cbg1.CheckedValue = TryCast(objtr.tempObj, ArrayList)
                                        End If
                                    End If
                                    rbtnGrp1All.IsChecked = True
                                ElseIf ii = 2 Then
                                    GroupBox2.Text = objtr.Filter_Column
                                    GroupBox2.Tag = isLocationSecurity
                                    cbg2.DataSource = dtNew
                                    cbg2.ValueMember = strValueMembter
                                    cbg2.Tag = objtr.Filter_Column
                                    GroupBox2.Visible = True
                                    If isDrillDown Then
                                        If objtr.tempObj IsNot Nothing Then
                                            rbtnGrp2Select.IsChecked = True
                                            cbg2.CheckedValue = TryCast(objtr.tempObj, ArrayList)
                                        End If
                                    End If
                                    rbtnGrp2All.IsChecked = True
                                ElseIf ii = 3 Then
                                    GroupBox3.Text = objtr.Filter_Column
                                    GroupBox3.Tag = isLocationSecurity
                                    cbg3.DataSource = dtNew
                                    cbg3.ValueMember = strValueMembter
                                    cbg3.Tag = objtr.Filter_Column
                                    GroupBox3.Visible = True
                                    If isDrillDown Then
                                        If objtr.tempObj IsNot Nothing Then
                                            rbtnGrp3Select.IsChecked = True
                                            cbg3.CheckedValue = TryCast(objtr.tempObj, ArrayList)
                                        End If
                                    End If
                                    rbtnGrp3All.IsChecked = True
                                ElseIf ii = 4 Then
                                    GroupBox4.Text = objtr.Filter_Column
                                    GroupBox4.Tag = isLocationSecurity
                                    cbg4.DataSource = dtNew
                                    cbg4.ValueMember = strValueMembter
                                    cbg4.Tag = objtr.Filter_Column
                                    GroupBox4.Visible = True
                                    If isDrillDown Then
                                        If objtr.tempObj IsNot Nothing Then
                                            rbtnGrp4Select.IsChecked = True
                                            cbg4.CheckedValue = TryCast(objtr.tempObj, ArrayList)
                                        End If
                                    End If
                                    rbtnGrp4All.IsChecked = True
                                End If
                            End If
                        End If
                    End If
                Next
                If obj.arrInner IsNot Nothing AndAlso obj.arrInner.Count > 0 Then
                    For Each objtr As clsCreateBIReportInnerFilterDetails In obj.arrInner
                        If objtr.Is_Date_Range Then
                            txtToDate.Value = clsCommon.GETSERVERDATE()
                            If clsCommon.CompairString(obj.Code, "CRRTRENDB") = CompairStringResult.Equal Then
                                txtFromDate.Value = txtToDate.Value.AddDays(-10)
                            ElseIf clsCommon.CompairString(obj.Code, "CRRTRENDL") = CompairStringResult.Equal Then
                                txtFromDate.Value = txtToDate.Value.AddDays(-10)
                            Else
                                txtFromDate.Value = txtToDate.Value.AddMonths(-1)
                            End If

                            txtFromDate.Tag = objtr.Filter_SNo
                            txtToDate.Tag = objtr.Filter_SNo
                            isDateFilterExist = True
                            Continue For
                        End If
                        ii += 1
                        Dim strValueMembter As String = ""
                        Dim strDisplayMembter As String = ""
                        Dim strParentValue As String = ""
                        Dim isLocationSecurity As Boolean = False
                        qry = clsCreateBIFilter.GetQuery(objtr.Against_Filter, strValueMembter, strDisplayMembter, strParentValue, isLocationSecurity)
                        If clsCommon.myLen(qry) > 0 Then
                            If objtr.Tree_Level > 0 Then
                                'SplitContainer3.Panel1Collapsed = False
                                'GroupBox5.Text += " " + objtr.Against_Filter_Name
                                'cbt.ValueMember = strValueMembter
                                'cbt.DisplayMember = strDisplayMembter
                                'cbt.ParentValue = strParentValue
                                'cbt.DataSource = clsDBFuncationality.GetDataTable(qry)
                                'arrTreeLevel.Add(objtr.Tree_Level, objtr.Filter_Column)
                            Else
                                Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry)
                                If isLocationSecurity Then
                                    Dim obj As New clsMCCCodes()
                                    obj = clsMCCCodes.GetData()
                                    If obj.arrLocCodes IsNot Nothing AndAlso obj.arrLocCodes.Count > 0 Then
                                        For jj As Integer = dtNew.Rows.Count - 1 To 0 Step -1
                                            If Not obj.arrLocCodes.Contains(clsCommon.myCstr(dtNew.Rows(jj)(strValueMembter))) Then
                                                dtNew.Rows.Remove(dtNew.Rows(jj))
                                            End If
                                        Next
                                    End If
                                End If
                                If ii = 1 Then
                                    GroupBox1.Text = objtr.Against_Filter_Name
                                    GroupBox1.Tag = isLocationSecurity
                                    cbg1.DataSource = dtNew
                                    cbg1.ValueMember = strValueMembter
                                    cbg1.Tag = objtr.Filter_SNo
                                    GroupBox1.Visible = True
                                    rbtnGrp1All.IsChecked = True
                                ElseIf ii = 2 Then
                                    GroupBox2.Text = objtr.Against_Filter_Name
                                    GroupBox2.Tag = isLocationSecurity
                                    cbg2.DataSource = dtNew
                                    cbg2.ValueMember = strValueMembter
                                    cbg2.Tag = objtr.Filter_SNo
                                    GroupBox2.Visible = True
                                    rbtnGrp2All.IsChecked = True
                                ElseIf ii = 3 Then
                                    GroupBox3.Text = objtr.Against_Filter_Name
                                    GroupBox3.Tag = isLocationSecurity
                                    cbg3.DataSource = dtNew
                                    cbg3.ValueMember = strValueMembter
                                    cbg3.Tag = objtr.Filter_SNo
                                    GroupBox3.Visible = True
                                    rbtnGrp3All.IsChecked = True
                                ElseIf ii = 4 Then
                                    GroupBox4.Text = objtr.Against_Filter_Name
                                    GroupBox4.Tag = isLocationSecurity
                                    cbg4.DataSource = dtNew
                                    cbg4.ValueMember = strValueMembter
                                    cbg4.Tag = objtr.Filter_SNo
                                    GroupBox4.Visible = True
                                    rbtnGrp4All.IsChecked = True
                                End If
                            End If
                        End If
                    Next
                End If
                strOuterOrderBy = ""
                qry = "select Filter_Column,Is_Order_Desc from TSPL_CREATE_BI_REPORT_FILTERS  where code ='" + obj.Code + "' and Order_By>0 order by Order_By"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        If clsCommon.myLen(strOuterOrderBy) > 0 Then
                            strOuterOrderBy += ","
                        End If
                        strOuterOrderBy += "[" + clsCommon.myCstr(dr("Filter_Column")) + "]"
                        If clsCommon.myCdbl(dr("Is_Order_Desc")) > 0 Then
                            strOuterOrderBy += " Desc "
                        End If
                    Next
                End If

                SplitContainer2.Panel1Collapsed = Not isDateFilterExist
                cbt.Tag = arrTreeLevel
                If isDrillDown Then
                    LoadData()
                End If
            End If
            'Dim qryExport As String = clsDBFuncationality.getSingleValue("select Type from TSPL_CREATE_BI_REPORT where 2=2  and Type ='Pivot Grid'")
            If clsCommon.CompairString(obj.Type, "Pivot Grid") = CompairStringResult.Equal Then
                rmPDF.Visibility = ElementVisibility.Collapsed
            Else
                rmPDF.Visibility = ElementVisibility.Visible
            End If
            isLoadData = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
            isLoadData = False
        End Try
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(obj.Code)
        If Not (MyBase.isReadFlag) Then
            If MDI.blnShowAllMenu = False Then
                Throw New Exception("Permission Denied")
            Else
                Throw New Exception("Can't Access in demo version. " + Environment.NewLine + " For any queries/details, contact tecxpert@tecxpert.in. ")
            End If
        End If
        btnRefresh.Visible = MyBase.isReadFlag
        RadSplitButton1.Visible = MyBase.isExport
        'btnQuickExport.Visible = MyBase.isQuickExportFlag
    End Sub

    

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        PageSetupReport_ID = obj.Code
        TemplateGridview = gv1
        LoadData()

    End Sub

    Sub LoadData()
        Try
            Dim strQryPre As String = Nothing
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.Description, "Tanker Status Report") = CompairStringResult.Equal Then
                strQryPre = " Select Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Bulk In' Else 'MCC In' End As [Proc Type], Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then Tspl_Gate_Entry_Details.Vendor_Code End As [Vendor Code], Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then TSPL_VENDOR_MASTER.Vendor_Name End As [Vendor Name], IsNull(TSPL_MCC_Dispatch_Challan.MCC_Code, 'Not Req') As [From Location Code], IsNull(fromLocation.Location_Desc, 'Not Req') As [From Location Desc], IsNull(Tspl_Gate_Entry_Details.location_Code, '') As [To Location Code], IsNull(toLocation.Location_Desc, '') As [To Location Desc], IsNull(Tspl_Gate_Entry_Details.Tanker_No, '') As [Tanker No], IsNull(Tspl_Gate_Entry_Details.Gate_Entry_No, '') As [Gate Entry No], Tspl_Gate_Entry_Details.Date_And_Time As [Gate Entry Date], Tspl_Gate_Entry_Details.Date_And_Time As [Entry Date], IsNull(GrossWeight.Weighment_No, '') As [Gross Weightment],GrossWeight.Weighment_date As [Weighment Date], IsNull(TareWeight.Weighment_No, '') As [Tare Weighment No], Case When IsNull(TareWeight.isPosted, 0) = 0 Then 'Not Done' Else 'Done' End As [Weighment Posting Status], IsNull(TSPL_QUALITY_CHECK.QC_No, '') As [QC No], TSPL_QUALITY_CHECK.QC_In_Date_Time As [QC Date], Case When IsNull(TSPL_QUALITY_CHECK.isPosted, 0) = 0 Then 'Not Done' Else 'Done' End As [QC Posting Status], Case When IsNull(TSPL_QUALITY_CHECK.is_Param_Accepted, 0) = 0 And IsNull(TSPL_QUALITY_CHECK.isPosted, 0) = 0 And IsNull(TSPL_QUALITY_CHECK.QC_No, '') = '' Then 'QC Pending' When IsNull(TSPL_QUALITY_CHECK.is_Param_Accepted, 0) = 1 And IsNull(TSPL_QUALITY_CHECK.isPosted, 0) = 1 Then 'QC Accepted' When IsNull(TSPL_QUALITY_CHECK.is_Param_Accepted, 0) = 0 And IsNull(TSPL_QUALITY_CHECK.isPosted, 0) = 1 Then 'QC Rejected' When IsNull(TSPL_QUALITY_CHECK.is_Param_Accepted, 0) = 2 And IsNull(TSPL_QUALITY_CHECK.isPosted, 0) = 1 Then 'Accepted With Special Approval' Else 'QC Pending' End As [QC Status], IsNull(TSPL_MILK_UNLOADING.Unloading_No, '') As [Unloading No], TSPL_MILK_UNLOADING.Unloading_Date_Time As [Unloading Date], Case When IsNull(TSPL_MILK_UNLOADING.isPosted, 0) = 0 Then 'Not Done' Else 'Done' End As [Unloading Posting Status], Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Not Req' Else IsNull(TSPL_Cleaning.Doc_No, '') End As [Cleaning No], TSPL_Cleaning.Start_Date_Time As [Cleaning Date], Case When IsNull(TSPL_Cleaning.isPosted, 0) = 0 Then 'Not Done' Else 'Done' End As [Cleaning Posting Status], IsNull(TSPL_Gate_Out.Doc_No, '') As [Gate Out No],TSPL_Gate_Out.Doc_Date As [Gate Out Date], Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') <> 'BulkProc' Then 'Not Req' Else IsNull(TSPL_Bulk_MILK_SRN.SRN_NO, '') End As [SRN No], TSPL_Bulk_MILK_SRN.SRN_Date As [SRN Date], Case When IsNull(TSPL_Bulk_MILK_SRN.isPosted, 0) = 0 Then 'Not Done' Else 'Done' End As [SRN Posting Status], Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Not Req' Else IsNull(TSPL_MILK_TRANSFER_IN.Receipt_Challan_No, '') End As [Milk Receipt Challan No], TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date As [Milk Receipt Challan Date], Case When IsNull(TSPL_MILK_TRANSFER_IN.isPosted, 0) = 0 Then 'Not Done' Else 'Done' End As [Milk Receipt Posting Staus], Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') <> 'BulkProc' Then 'Not Req' Else IsNull(tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO, '') End As [PI NO], tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE As [PI Date] From Tspl_Gate_Entry_Details Left Outer Join TSPL_Weighment_Detail GrossWeight On GrossWeight.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No Left Outer Join TSPL_Weighment_Detail TareWeight On TareWeight.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No Left Outer Join TSPL_QUALITY_CHECK On TSPL_QUALITY_CHECK.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No Left Outer Join TSPL_MILK_UNLOADING On TSPL_MILK_UNLOADING.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No Left Outer Join TSPL_Cleaning On TSPL_Cleaning.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No Left Outer Join TSPL_Gate_Out On TSPL_Gate_Out.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No Left Outer Join TSPL_Bulk_MILK_SRN On TSPL_Bulk_MILK_SRN.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No Left Outer Join TSPL_LOCATION_MASTER As toLocation On toLocation.Location_Code = Tspl_Gate_Entry_Details.location_Code Left Outer Join TSPL_MILK_TRANSFER_IN On TSPL_MILK_TRANSFER_IN.Gate_Entry_no = Tspl_Gate_Entry_Details.Gate_Entry_No Left Outer Join tspl_Bulk_milk_purchase_Invoice_Detail On tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO Left Outer Join tspl_Bulk_milk_purchase_Invoice_head On tspl_Bulk_milk_purchase_Invoice_head.DOC_NO = tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO Left Outer Join TSPL_MCC_Dispatch_Challan On Tspl_Gate_Entry_Details.Challan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO Left Outer Join TSPL_LOCATION_MASTER As fromLocation On fromLocation.Location_Code = TSPL_MCC_Dispatch_Challan.MCC_Code Left Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = Tspl_Gate_Entry_Details.Vendor_Code "
            Else
                strQryPre = obj.Qry
            End If
            If obj.arrInner IsNot Nothing AndAlso obj.arrInner.Count > 0 Then
                For Each objtr As clsCreateBIReportInnerFilterDetails In obj.arrInner
                    Dim strReplaceFromText As String = " '" + objtr.Filter_SNo + "' = '" + objtr.Filter_SNo + "'"
                    Dim strReplaceToText As String = " 2=2 "
                    If objtr.Is_Date_Range Then
                        strReplaceToText = "   " + objtr.Table_Name + "." + objtr.Column_Name + " >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") & _
                           "' and " + objtr.Table_Name + "." + objtr.Column_Name + " <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'   "
                    ElseIf objtr.Is_From_Date Then
                        strReplaceToText = "   " + objtr.Table_Name + "." + objtr.Column_Name + " " + objtr.Operator_Name + " '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "   "
                    ElseIf objtr.Is_To_Date Then
                        strReplaceToText = "   " + objtr.Table_Name + "." + objtr.Column_Name + " " + objtr.Operator_Name + " '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "   "
                    Else
                        If clsCommon.myLen(cbg1.Tag) > 0 AndAlso (rbtnGrp1Select.IsChecked OrElse clsCommon.myCBool(GroupBox1.Tag)) AndAlso clsCommon.CompairString(clsCommon.myCstr(cbg1.Tag), objtr.Filter_SNo) = CompairStringResult.Equal Then
                            strReplaceToText += " and " + objtr.Table_Name + "." + objtr.Column_Name + "  in (" + clsCommon.GetMulcallString(cbg1.CheckedValue) + ")"
                            If cbg1.CheckedValue Is Nothing OrElse cbg1.CheckedValue.Count <= 0 Then
                                Throw New Exception("Please select at least one " + cbg1.Tag)
                            End If
                        End If
                        If clsCommon.myLen(cbg2.Tag) > 0 AndAlso (rbtnGrp2Select.IsChecked OrElse clsCommon.myCBool(GroupBox2.Tag)) AndAlso clsCommon.CompairString(clsCommon.myCstr(cbg2.Tag), objtr.Filter_SNo) = CompairStringResult.Equal Then
                            strReplaceToText += " and " + objtr.Table_Name + "." + objtr.Column_Name + " in (" + clsCommon.GetMulcallString(cbg2.CheckedValue) + ")"
                            If cbg2.CheckedValue Is Nothing OrElse cbg2.CheckedValue.Count <= 0 Then
                                Throw New Exception("Please select at least one " + cbg2.Tag)
                            End If
                        End If
                        If clsCommon.myLen(cbg3.Tag) > 0 AndAlso (rbtnGrp3Select.IsChecked OrElse clsCommon.myCBool(GroupBox3.Tag)) AndAlso clsCommon.CompairString(clsCommon.myCstr(cbg3.Tag), objtr.Filter_SNo) = CompairStringResult.Equal Then
                            strReplaceToText += " and " + objtr.Table_Name + "." + objtr.Column_Name + " in (" + clsCommon.GetMulcallString(cbg3.CheckedValue) + ")"
                            If cbg3.CheckedValue Is Nothing OrElse cbg3.CheckedValue.Count <= 0 Then
                                Throw New Exception("Please select at least one " + cbg3.Tag)
                            End If
                        End If
                        If clsCommon.myLen(cbg4.Tag) > 0 AndAlso (rbtnGrp4Select.IsChecked OrElse clsCommon.myCBool(GroupBox4.Tag)) AndAlso clsCommon.CompairString(clsCommon.myCstr(cbg4.Tag), objtr.Filter_SNo) = CompairStringResult.Equal Then
                            strReplaceToText += " and " + objtr.Table_Name + "." + objtr.Column_Name + " in (" + clsCommon.GetMulcallString(cbg4.CheckedValue) + ")"
                            If cbg4.CheckedValue Is Nothing OrElse cbg4.CheckedValue.Count <= 0 Then
                                Throw New Exception("Please select at least one " + cbg4.Tag)
                            End If


                        End If
                    End If
                    strQryPre = strQryPre.Replace(strReplaceFromText, strReplaceToText)
                Next
            End If
            'KUNAL > TICKET : BM00000009892 > DATE : 18-NOV-2016
            Dim strQry As String = " select DISTINCT  * from (" + strQryPre + ")xxx where 2=2 "
            If clsCommon.myLen(cbg1.Tag) > 0 AndAlso (rbtnGrp1Select.IsChecked OrElse clsCommon.myCBool(GroupBox1.Tag)) AndAlso Not clsCommon.myCstr(cbg1.Tag).Contains("#$") Then
                strQry += " and [" + clsCommon.myCstr(cbg1.Tag) + "] in (" + clsCommon.GetMulcallString(cbg1.CheckedValue) + ")"
                If cbg1.CheckedValue Is Nothing OrElse cbg1.CheckedValue.Count <= 0 Then
                    Throw New Exception("Please select at least one " + cbg1.Tag)
                End If
            End If
            If clsCommon.myLen(cbg2.Tag) > 0 AndAlso (rbtnGrp2Select.IsChecked OrElse clsCommon.myCBool(GroupBox2.Tag)) AndAlso Not clsCommon.myCstr(cbg2.Tag).Contains("#$") Then
                strQry += " and [" + clsCommon.myCstr(cbg2.Tag) + "] in (" + clsCommon.GetMulcallString(cbg2.CheckedValue) + ")"
                If cbg2.CheckedValue Is Nothing OrElse cbg2.CheckedValue.Count <= 0 Then
                    Throw New Exception("Please select at least one " + cbg2.Tag)
                End If
            End If
            If clsCommon.myLen(cbg3.Tag) > 0 AndAlso (rbtnGrp3Select.IsChecked OrElse clsCommon.myCBool(GroupBox3.Tag)) AndAlso Not clsCommon.myCstr(cbg3.Tag).Contains("#$") Then
                strQry += " and [" + clsCommon.myCstr(cbg3.Tag) + "] in (" + clsCommon.GetMulcallString(cbg3.CheckedValue) + ")"
                If cbg3.CheckedValue Is Nothing OrElse cbg3.CheckedValue.Count <= 0 Then
                    Throw New Exception("Please select at least one " + cbg3.Tag)
                End If
            End If
            If clsCommon.myLen(cbg4.Tag) > 0 AndAlso (rbtnGrp4Select.IsChecked OrElse clsCommon.myCBool(GroupBox4.Tag)) AndAlso Not clsCommon.myCstr(cbg4.Tag).Contains("#$") Then
                strQry += " and [" + clsCommon.myCstr(cbg4.Tag) + "] in (" + clsCommon.GetMulcallString(cbg4.CheckedValue) + ")"
                If cbg4.CheckedValue Is Nothing OrElse cbg4.CheckedValue.Count <= 0 Then
                    Throw New Exception("Please select at least one " + cbg4.Tag)
                End If
            End If
            If Not SplitContainer2.Panel1Collapsed AndAlso Not clsCommon.myCstr(txtFromDate.Tag).Contains("#$") Then
                strQry += " and [" + clsCommon.myCstr(txtFromDate.Tag) + "] >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "'"
                strQry += " and [" + clsCommon.myCstr(txtToDate.Tag) + "] <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'"
            End If
            If Not SplitContainer3.Panel1Collapsed Then
                If rbtnGrp5Select.IsChecked = True Then
                    Dim arrTreeLevel As Dictionary(Of Integer, String) = TryCast(cbt.Tag, Dictionary(Of Integer, String))
                    If cbt.CheckedValue Is Nothing OrElse cbt.CheckedValue.Count <= 0 Then
                        Throw New Exception("Please select at least one " + GroupBox5.Text)
                    Else
                        For ii As Integer = 1 To cbt.CheckedValue.Count
                            If cbt.CheckedValue(ii) Is Nothing OrElse cbt.CheckedValue(ii).Count <= 0 Then
                                Throw New Exception("Please select at least one " + arrTreeLevel(ii))
                            End If
                            strQry += " and [" + clsCommon.myCstr(arrTreeLevel(ii)) + "] in (" + clsCommon.GetMulcallString(cbt.CheckedValue(ii)) + ")"
                        Next
                    End If
                End If
            End If
            If clsCommon.myLen(strOuterOrderBy) > 0 Then
                strQry += " order by  " + strOuterOrderBy
            End If
            Dim objLayout As clsGridLayout = New clsGridLayout()
            objLayout = CType(objLayout.GetData(obj.Code, "", objCommonVar.CurrentUserCode), clsGridLayout)

            If RadPageView1.Pages("RadPageViewPage2").Item.Visibility = Telerik.WinControls.ElementVisibility.Visible Then
                ''For Grid
                gv1.DataSource = Nothing
                gv1.Columns.Clear()
                gv1.Rows.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.DataSource = clsDBFuncationality.GetDataTable(strQry)
                gv1.TableElement.TableHeaderHeight = 30
                gv1.MasterTemplate.ShowRowHeaderColumn = False
                For ii As Integer = 0 To gv1.Columns.Count - 1
                    gv1.Columns(ii).ReadOnly = True
                Next
                gv1.AllowAddNewRow = False
                gv1.ShowGroupPanel = True
                gv1.BestFitColumns()

                If objLayout IsNot Nothing Then
                    gv1.LoadLayout(objLayout.GridLayout)
                Else
                    gv1.LoadLayout(obj.Layout)
                End If

                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                Dim summaryRowItem As New GridViewSummaryRowItem()
                For Each objDetail As clsCreateBIReportFilterDetails In obj.arr
                    If objDetail.Is_Show_Total Then
                        If clsCommon.myLen(objDetail.Total_Formula) > 0 Then
                            Dim summaryItem As New GridViewSummaryItem()
                            summaryItem.FormatString = "{0:F2}"
                            summaryItem.Name = objDetail.Filter_Column
                            summaryItem.AggregateExpression = objDetail.Total_Formula
                            summaryRowItem.Add(summaryItem)
                        Else
                            Dim item1 As New GridViewSummaryItem(objDetail.Filter_Column, "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(item1)
                        End If
                    End If
                    'End If
                Next
                gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                RadPageView1.SelectedPage = RadPageView1.Pages("RadPageViewPage2")
            ElseIf RadPageView1.Pages("RadPageViewPage3").Item.Visibility = Telerik.WinControls.ElementVisibility.Visible Then
                ''For Pivot Grid
                pg1.DataSource = clsDBFuncationality.GetDataTable(strQry)

                If objLayout IsNot Nothing Then
                    pg1.LoadLayout(objLayout.GridLayout)
                Else
                    pg1.LoadLayout(obj.Layout)
                End If
                RadPageView1.SelectedPage = RadPageView1.Pages("RadPageViewPage3")
            ElseIf RadPageView1.Pages("RadPageViewPage4").Item.Visibility = Telerik.WinControls.ElementVisibility.Visible Then
                ShoChartsData(strQry)
                RadPageView1.SelectedPage = RadPageView1.Pages("RadPageViewPage4")
            End If
            EnableDisableConrols(False)
            'ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Sub ShoChartsData(ByVal qry As String)
        Try
            AddHandler RadChartView2.LabelFormatting, AddressOf radChartView_LabelFormatting
            RadChartView2.Area.View.Palette = New CustomPalette()
            RadChartView2.Series.Clear()
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim arrRow As New List(Of String)
                Dim strColumn As String = ""
                For Each objtr As clsCreateBIReportFilterDetails In obj.arr
                    If objtr.Chart_Column Then
                        strColumn = objtr.Filter_Column
                    ElseIf objtr.Chart_Row Then
                        arrRow.Add(objtr.Filter_Column)
                    End If
                Next

                RadChartView2.ShowTitle = True
                RadChartView2.ChartElement.TitlePosition = TitlePosition.Top
                RadChartView2.ChartElement.TitleElement.TextAlignment = ContentAlignment.MiddleCenter
                RadChartView2.Title = obj.Description
                Dim smartLabelsController As New SmartLabelsController()
                RadChartView2.Controllers.Add(smartLabelsController)
                RadChartView2.ShowSmartLabels = True
                Dim strategy As SmartLabelsStrategyBase = Nothing

                Dim combineMode As ChartSeriesCombineMode = ChartSeriesCombineMode.None
                If clsCommon.CompairString(obj.Chart_Combine_Mode, "Cluster") = CompairStringResult.Equal Then
                    combineMode = ChartSeriesCombineMode.Cluster
                ElseIf clsCommon.CompairString(obj.Chart_Combine_Mode, "Stack") = CompairStringResult.Equal Then
                    combineMode = ChartSeriesCombineMode.Stack
                ElseIf clsCommon.CompairString(obj.Chart_Combine_Mode, "Stack100") = CompairStringResult.Equal Then
                    combineMode = ChartSeriesCombineMode.Stack100
                End If

                If clsCommon.CompairString(obj.Chart_Type, "Bar") = CompairStringResult.Equal Then
                    RadChartView2.AreaType = ChartAreaType.Cartesian
                    strategy = New VerticalAdjusmentLabelsStrategy()
                    If arrRow IsNot Nothing AndAlso arrRow.Count > 0 Then
                        For Each strVal As String In arrRow
                            Dim barSeries As New BarSeries()
                            barSeries.Name = strVal
                            barSeries.LegendTitle = strVal
                            barSeries.ValueMember = strVal
                            barSeries.CategoryMember = strColumn
                            barSeries.DataSource = dt
                            barSeries.CombineMode = combineMode
                            barSeries.ShowLabels = obj.Chart_Show_Label
                            'barSeries.BorderBoxStyle = BorderBoxStyle.FourBorders
                            barSeries.DrawLinesToLabels = True
                            barSeries.SyncLinesToLabelsColor = False
                            RadChartView2.Series.Add(barSeries)
                        Next
                        smartLabelsController.Strategy = strategy
                        RadChartView2.ShowLegend = IIf(arrRow.Count > 1, True, False)
                        setOrientation(RadChartView2, obj.Chart_Orientation, obj.Chart_Label_Rotation)
                    End If
                ElseIf clsCommon.CompairString(obj.Chart_Type, "Line") = CompairStringResult.Equal Then
                    RadChartView2.AreaType = ChartAreaType.Cartesian
                    strategy = New VerticalAdjusmentLabelsStrategy()

                    'KUNAL > REQUEST NO : KLREQ000669 > TICKET : BM00000009261 
                    'BUG WAS : NO GRAPHICAL LINES WERE VISIBLES 

                    RadChartView2.Area.View.Palette = KnownPalette.Natural
                    If arrRow IsNot Nothing AndAlso arrRow.Count > 0 Then
                        For Each strVal As String In arrRow
                            Dim lineSeries As New LineSeries()
                            lineSeries.Name = strVal
                            lineSeries.LegendTitle = strVal
                            lineSeries.ValueMember = strVal
                            lineSeries.CategoryMember = strColumn
                            lineSeries.DataSource = dt
                            lineSeries.ShowLabels = True
                            lineSeries.CombineMode = combineMode
                            lineSeries.ShowLabels = obj.Chart_Show_Label
                            lineSeries.DrawLinesToLabels = True
                            lineSeries.SyncLinesToLabelsColor = True
                            RadChartView2.Series.Add(lineSeries)
                        Next
                        RadChartView2.ShowLegend = IIf(arrRow.Count > 1, True, False)
                        setOrientation(RadChartView2, obj.Chart_Orientation, obj.Chart_Label_Rotation)
                    End If
                ElseIf clsCommon.CompairString(obj.Chart_Type, "Area") = CompairStringResult.Equal Then
                    RadChartView2.AreaType = ChartAreaType.Cartesian
                    If arrRow IsNot Nothing AndAlso arrRow.Count > 0 Then
                        For Each strVal As String In arrRow
                            Dim AreaSeries As New AreaSeries()
                            AreaSeries.Name = strVal
                            AreaSeries.LegendTitle = strVal
                            AreaSeries.ValueMember = strVal
                            AreaSeries.CategoryMember = strColumn
                            AreaSeries.DataSource = dt
                            AreaSeries.BorderWidth = 2
                            AreaSeries.ShowLabels = True
                            AreaSeries.CombineMode = combineMode
                            AreaSeries.ShowLabels = obj.Chart_Show_Label
                            AreaSeries.DrawLinesToLabels = True
                            AreaSeries.SyncLinesToLabelsColor = True
                            RadChartView2.Series.Add(AreaSeries)
                        Next
                        RadChartView2.ShowLegend = IIf(arrRow.Count > 1, True, False)
                        setOrientation(RadChartView2, obj.Chart_Orientation, obj.Chart_Label_Rotation)
                    End If
                ElseIf clsCommon.CompairString(obj.Chart_Type, "Pie") = CompairStringResult.Equal Then
                    strategy = New PieTwoLabelColumnsStrategy()
                    RadChartView2.AreaType = ChartAreaType.Pie
                    RadChartView2.ShowLegend = obj.Chart_Show_Label
                    RadChartView2.View.Margin = New Padding(60, 0, 50, 0)
                    Dim series As New PieSeries()
                    series.Range = New AngleRange(270, 360)
                    series.LabelFormat = "{0:P2}"
                    series.RadiusFactor = 0.9F
                    series.ValueMember = arrRow(0)
                    series.DataSource = dt
                    series.ShowLabels = True
                    series.DrawLinesToLabels = True
                    series.SyncLinesToLabelsColor = True
                    series.DisplayMember = strColumn
                    RadChartView2.Series.Add(series)
                    'For Each item As LegendItem In Me.RadChartView2.ChartElement.LegendElement.Provider.LegendInfos
                    '    Dim pointElement As PiePointElement = DirectCast(item.Element, PiePointElement)
                    '    Dim row As DataRowView = DirectCast(pointElement.DataPoint.DataItem, DataRowView)
                    '    item.Title = clsCommon.myCstr(row(strColumn))
                    'Next
                ElseIf clsCommon.CompairString(obj.Chart_Type, "Donut") = CompairStringResult.Equal Then
                    strategy = New PieTwoLabelColumnsStrategy()
                    Dim series As New DonutSeries()
                    series.Range = New AngleRange(270, 360)
                    series.LabelFormat = "{0:P2}"
                    series.RadiusFactor = 0.9F
                    series.InnerRadiusFactor = 50 / 100
                    series.ValueMember = arrRow(0)
                    series.DataSource = dt
                    series.ShowLabels = True
                    series.DrawLinesToLabels = True
                    series.SyncLinesToLabelsColor = True
                    series.DisplayMember = strColumn
                    RadChartView2.ShowLegend = obj.Chart_Show_Label
                    RadChartView2.AreaType = ChartAreaType.Pie
                    RadChartView2.Series.Add(series)
                    RadChartView2.View.Margin = New Padding(60, 0, 50, 0)
                    'For Each item As LegendItem In Me.RadChartView2.ChartElement.LegendElement.Provider.LegendInfos
                    '    Dim pointElement As PiePointElement = DirectCast(item.Element, PiePointElement)
                    '    Dim row As DataRowView = DirectCast(pointElement.DataPoint.DataItem, DataRowView)
                    '    item.Title = clsCommon.myCstr(row(strColumn))
                    'Next
                End If
            End If
            'btnQuickExport.Visible = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub radChartView_LabelFormatting(ByVal sender As Object, ByVal e As ChartViewLabelFormattingEventArgs)
        'e.LabelElement.BorderColor = (CType(e.LabelElement.Parent, DataPointElement)).BackColor

        e.LabelElement.BackColor = Color.White
        e.LabelElement.BorderColor = Color.White
    End Sub

    Sub setOrientation(ByVal chv As RadChartView, ByVal strOrient As String, ByVal LableRotationAngel As Integer)
        Dim grid As CartesianGrid = chv.GetArea(Of CartesianArea)().GetGrid(Of CartesianGrid)()
        If clsCommon.CompairString(strOrient, "Horizontal") = CompairStringResult.Equal Then
            chv.GetArea(Of CartesianArea)().Orientation = Orientation.Horizontal
            grid.DrawVerticalStripes = True
            grid.DrawHorizontalStripes = False
        Else
            chv.GetArea(Of CartesianArea)().Orientation = Orientation.Vertical
            grid.DrawVerticalStripes = False
            grid.DrawHorizontalStripes = True
        End If


        Dim categoricalAxis As CategoricalAxis = TryCast(chv.Axes(0), CategoricalAxis)
        categoricalAxis.PlotMode = AxisPlotMode.OnTicksPadded
        categoricalAxis.LabelFitMode = AxisLabelFitMode.Rotate
        categoricalAxis.LabelRotationAngle = LableRotationAngel
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton2.Click
        CloseForm()
    End Sub

    Sub CloseForm()
        clsERPFuncationality.closeForm(Me)
    End Sub

    Sub reset()
        Try
            EnableDisableConrols(True)
            gv1.DataSource = Nothing
            Try
                pg1.DataSource = Nothing
            Catch ex As Exception
            End Try
            RadPageView1.SelectedPage = RadPageViewPage1
            If clsCommon.CompairString(obj.Code, "CRRTRENDB") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Code, "CRRTRENDL") = CompairStringResult.Equal Then
                txtToDate.Enabled = False
            End If
            'btnQuickExport.Visible = True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
        
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
    End Sub

    Private Sub EnableDisableConrols(ByVal val As Boolean)
        txtFromDate.Enabled = val
        txtToDate.Enabled = val
        GroupBox1.Enabled = val
        GroupBox2.Enabled = val
        GroupBox3.Enabled = val
        GroupBox4.Enabled = val
    End Sub

    Private Sub rbtnGrp1All_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnGrp1All.ToggleStateChanged, rbtnGrp1Select.ToggleStateChanged
        cbg1.Enabled = rbtnGrp1Select.IsChecked
        If clsCommon.myCBool(GroupBox1.Tag) AndAlso rbtnGrp1All.IsChecked Then
            cbg1.CheckedAll()
        Else
            cbg1.UnCheckedAll()
        End If
    End Sub

    Private Sub rbtnGrp2All_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnGrp2All.ToggleStateChanged, rbtnGrp2Select.ToggleStateChanged
        cbg2.Enabled = rbtnGrp2Select.IsChecked
        If clsCommon.myCBool(GroupBox2.Tag) AndAlso rbtnGrp2All.IsChecked Then
            cbg2.CheckedAll()
        Else
            cbg2.UnCheckedAll()
        End If
    End Sub

    Private Sub rbtnGrp3All_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnGrp3All.ToggleStateChanged, rbtnGrp3Select.ToggleStateChanged
        cbg3.Enabled = rbtnGrp3Select.IsChecked
        If clsCommon.myCBool(GroupBox3.Tag) AndAlso rbtnGrp3All.IsChecked Then
            cbg3.CheckedAll()
        Else
            cbg3.UnCheckedAll()
        End If
    End Sub

    Private Sub rbtnGrp4All_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnGrp4All.ToggleStateChanged, rbtnGrp4Select.ToggleStateChanged
        cbg4.Enabled = rbtnGrp4Select.IsChecked
        If clsCommon.myCBool(GroupBox4.Tag) AndAlso rbtnGrp4All.IsChecked Then
            cbg4.CheckedAll()
        Else
            cbg4.UnCheckedAll()
        End If
    End Sub

    Private Sub RadButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton3.Click

        Try
            transportSql.exportdataInCSV(gv1, Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\Balwinder.xls", "Balwinder", False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
        'Dim exporter As ExportToExcelML = New ExportToExcelML(Me.gv1)
        'exporter.ExportVisualSettings = True

        'Dim tempPath As String = Path.GetTempPath()
        'tempPath &= "tempgrid.xls"
        'exporter.RunExport(tempPath)

        'Dim app As Microsoft.Office.Interop.Excel.Application = New Microsoft.Office.Interop.Excel.Application()

        'If app Is Nothing Then
        '    Console.WriteLine("EXCEL could not be started. Check that your office installation and project references are correct.")
        '    Return
        'End If

        'app.Visible = False
        'app.Interactive = False

        'Dim wb As Microsoft.Office.Interop.Excel.Workbook = app.Workbooks.Open(tempPath)

        'Dim desktopPath As String = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        'desktopPath &= "\grid.xlsx"

        'wb.SaveAs(desktopPath, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal)
        'wb.Close()
        'app.Quit()

        'Runtime.InteropServices.Marshal.ReleaseComObject(wb)
        'Runtime.InteropServices.Marshal.ReleaseComObject(app)

        'File.Delete(tempPath)
    End Sub

    Private Sub rbtnGrp5All_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnGrp5All.ToggleStateChanged, rbtnGrp5Select.ToggleStateChanged
        cbt.Enabled = rbtnGrp5Select.IsChecked
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        clsGridLayout.DeleteData(obj.Code, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        If clsCommon.myLen(obj.Code) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim objLayout As New clsGridLayout()
            objLayout.ReportID = obj.Code
            objLayout.UserID = objCommonVar.CurrentUserCode
            objLayout.GridLayout = New MemoryStream()
            gv1.SaveLayout(objLayout.GridLayout)
            objLayout.GridColumns = gv1.ColumnCount
            objLayout.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If objLayout.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''richa agarwal regarding memory leakage
            objLayout.GridLayout.Close()
            objLayout.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(obj.Code) > 0 Then
                Dim objLayout As clsGridLayout = New clsGridLayout()
                objLayout = CType(objLayout.GetData(obj.Code, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not objLayout Is Nothing AndAlso objLayout.GridColumns >= gv1.ColumnCount Then
                    If RadPageView1.Pages("RadPageViewPage2").Item.Visibility = Telerik.WinControls.ElementVisibility.Visible Then
                        gv1.LoadLayout(objLayout.GridLayout)
                    ElseIf RadPageView1.Pages("RadPageViewPage3").Item.Visibility = Telerik.WinControls.ElementVisibility.Visible Then
                        pg1.LoadLayout(objLayout.GridLayout)
                    ElseIf RadPageView1.Pages("RadPageViewPage4").Item.Visibility = Telerik.WinControls.ElementVisibility.Visible Then
                        'RadChartView2.Print(False)
                    End If
                   
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    

    Private Sub gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Try
            If obj.Drill_Down_Type = 1 Then
                If clsCommon.myLen(obj.Drill_Down_Report) > 0 AndAlso clsCommon.myLen(obj.Drill_Down_Filter) > 0 Then
                    Dim frm As New FrmBIReport
                    frm.SetUserMgmt("BI-RPT")
                    frm.obj = clsCreateBIReport.GetData(obj.Drill_Down_Report, True, NavigatorType.Current)
                    frm.Text = frm.obj.Description
                    frm.isDrillDown = True
                    frm.txtFromDate.Value = Me.txtFromDate.Value
                    frm.txtToDate.Value = Me.txtToDate.Value
                    For ii As Integer = 0 To obj.arr.Count - 1
                        For jj As Integer = 0 To frm.obj.arr.Count - 1
                            If clsCommon.myLen(obj.arr(ii).Filter_Column) > 0 AndAlso clsCommon.myLen(frm.obj.arr(jj).Filter_Column) > 0 Then
                                If clsCommon.CompairString(obj.arr(ii).Filter_Column, frm.obj.arr(jj).Filter_Column) = CompairStringResult.Equal Then
                                    Dim arr As ArrayList = Nothing
                                    If clsCommon.CompairString(frm.obj.arr(jj).Filter_Column, obj.Drill_Down_Column) = CompairStringResult.Equal Then
                                        arr = New ArrayList()
                                        arr.Add(clsCommon.myCstr(gv1.CurrentRow.Cells(obj.Drill_Down_Column).Value))
                                    Else
                                        If clsCommon.CompairString(GroupBox1.Text, frm.obj.arr(jj).Filter_Column) = CompairStringResult.Equal AndAlso rbtnGrp1Select.IsChecked Then
                                            arr = cbg1.CheckedValue
                                        ElseIf clsCommon.CompairString(GroupBox2.Text, frm.obj.arr(jj).Filter_Column) = CompairStringResult.Equal AndAlso rbtnGrp2Select.IsChecked Then
                                            arr = cbg2.CheckedValue
                                        ElseIf clsCommon.CompairString(GroupBox3.Text, frm.obj.arr(jj).Filter_Column) = CompairStringResult.Equal AndAlso rbtnGrp3Select.IsChecked Then
                                            arr = cbg3.CheckedValue
                                        ElseIf clsCommon.CompairString(GroupBox4.Text, frm.obj.arr(jj).Filter_Column) = CompairStringResult.Equal AndAlso rbtnGrp4Select.IsChecked Then
                                            arr = cbg4.CheckedValue
                                        End If
                                    End If
                                    frm.obj.arr(jj).tempObj = arr
                                End If
                            End If
                        Next
                    Next
                    frm.Show()
                End If
            ElseIf obj.Drill_Down_Type = 2 Then
                Dim strTransType As String = clsCommon.myCstr(gv1.CurrentRow.Cells(obj.Drill_Down_Transaction_Type).Value)
                Dim strTransCode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(obj.Drill_Down_Column).Value)
                If clsCommon.myLen(strTransType) > 0 AndAlso clsCommon.myLen(strTransCode) > 0 Then
                    MDI.ShowForm(strTransType, strTransCode, False, strTransCode)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmExcel_Click(sender As Object, e As EventArgs) Handles rmExcel.Click
        print(Exporter.Excel)
    End Sub

    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        print(Exporter.PDF)
    End Sub

    Sub print(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            'arrHeader.Add(Me.Text)
            arrHeader.Add("Company Name : " & objCommonVar.CurrentCompanyName)
            If Not SplitContainer2.Panel1Collapsed Then
                arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
            End If
            If clsCommon.myLen(cbg1.Tag) > 0 AndAlso rbtnGrp1Select.IsChecked Then
                If cbg1.CheckedDisplayMember IsNot Nothing AndAlso cbg1.CheckedDisplayMember.Count > 0 Then
                    arrHeader.Add(GroupBox1.Text + " : " + clsCommon.GetMulcallStringWithComma(cbg1.CheckedDisplayMember))
                End If
            End If
            If clsCommon.myLen(cbg2.Tag) > 0 AndAlso rbtnGrp2Select.IsChecked Then
                If cbg2.CheckedDisplayMember IsNot Nothing AndAlso cbg2.CheckedDisplayMember.Count > 0 Then
                    arrHeader.Add(GroupBox2.Text + " : " + clsCommon.GetMulcallStringWithComma(cbg2.CheckedDisplayMember))
                End If
            End If
            If clsCommon.myLen(cbg3.Tag) > 0 AndAlso rbtnGrp3Select.IsChecked Then
                If cbg3.CheckedDisplayMember IsNot Nothing AndAlso cbg3.CheckedDisplayMember.Count > 0 Then
                    arrHeader.Add(GroupBox3.Text + " : " + clsCommon.GetMulcallStringWithComma(cbg3.CheckedDisplayMember))
                End If
            End If
            If clsCommon.myLen(cbg4.Tag) > 0 AndAlso rbtnGrp4Select.IsChecked Then
                If cbg4.CheckedDisplayMember IsNot Nothing AndAlso cbg4.CheckedDisplayMember.Count > 0 Then
                    arrHeader.Add(GroupBox4.Text + " : " + clsCommon.GetMulcallStringWithComma(cbg4.CheckedDisplayMember))
                End If
            End If
            If Not SplitContainer3.Panel1Collapsed Then
                If rbtnGrp5Select.IsChecked = True Then
                    Dim arrTreeLevel As Dictionary(Of Integer, String) = TryCast(cbt.Tag, Dictionary(Of Integer, String))

                    For ii As Integer = 1 To cbt.CheckedText.Count
                        If cbt.CheckedText(ii) Is Nothing OrElse cbt.CheckedText(ii).Count <= 0 Then
                            Throw New Exception("Please select at least one " + arrTreeLevel(ii))
                        End If
                        arrHeader.Add(arrTreeLevel(ii) + " : " + clsCommon.GetMulcallStringWithComma(cbt.CheckedText(ii)))
                    Next

                End If
            End If

            If exporter = EnumExportTo.Excel Then
                If RadPageView1.Pages("RadPageViewPage2").Item.Visibility = Telerik.WinControls.ElementVisibility.Visible Then
                    'Dim sfd As SaveFileDialog = New SaveFileDialog()
                    'Dim filePath As String
                    'sfd.FileName = Me.Text
                    'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                    'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    '    filePath = sfd.FileName
                    'Else
                    '    Exit Sub
                    'End If
                    transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
                    'transportSql.exportdata(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                    'transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                    'clsCommon.MyExportToExcelGrid(Me.Text, gv1, arrHeader, Me.Text, False)
                ElseIf RadPageView1.Pages("RadPageViewPage3").Item.Visibility = Telerik.WinControls.ElementVisibility.Visible Then
                    clsCommon.MyExportToExcelPivotGrid(Me.Text, pg1, arrHeader, Me.Text, True)
                ElseIf RadPageView1.Pages("RadPageViewPage4").Item.Visibility = Telerik.WinControls.ElementVisibility.Visible Then
                    RadChartView2.Print(False)
                End If
            Else
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtFromDate_ValueChanged(sender As Object, e As EventArgs) Handles txtFromDate.ValueChanged
        Try
            If isLoadData = False Then
                If (clsCommon.CompairString(obj.Code, "CRRTRENDB") = CompairStringResult.Equal) Then
                    txtToDate.Value = txtFromDate.Value.AddDays(10)
                ElseIf (clsCommon.CompairString(obj.Code, "CRRTRENDL") = CompairStringResult.Equal) Then
                    txtToDate.Value = txtFromDate.Value.AddDays(10)
                End If
            End If
        Catch ex As Exception

        End Try
        
    End Sub


End Class
