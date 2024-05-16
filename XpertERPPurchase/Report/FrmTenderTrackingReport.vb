
Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System
Imports System.IO

Public Class FrmTenderTrackingReport
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim buttontooltip As ToolTip = New ToolTip()
#End Region


    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Private Sub FrmTenderTrackingReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.R AndAlso btnReport.Enabled Then
            fillGridReport()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.E AndAlso btnreset.Enabled Then
            reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub
    Private Sub FrmTenderTrackingReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        buttontooltip.SetToolTip(btnReport, "Press Alt+R for Summary ")
        buttontooltip.SetToolTip(btnreset, "Press Alt+E for Reset ")
        buttontooltip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        txtDate.Value = clsCommon.GETSERVERDATE()
    End Sub


    Private Sub fndSrcCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndSrcCode._MYValidating
        Try
            If clsCommon.myLen(txtTenderNo.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Select Tender First!", "Information")
                txtTenderNo.Focus()
                Exit Sub
            End If

            Dim qry As String = "select distinct (case when len(TSPL_ITEM_MASTER.Short_Description)>0 then TSPL_ITEM_MASTER.Short_Description else TSPL_ITEM_MASTER.item_code end) as Code 
                ,TSPL_ITEM_MASTER.Item_Desc as [Desc] from TSPL_TENDER_DETAIL left outer join 
                TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_TENDER_DETAIL.Item_Code"
            Dim StrWhere As String = " TSPL_TENDER_DETAIL.DocumentCode='" + txtTenderNo.Value + "'"
            fndSrcCode.Value = clsCommon.ShowSelectForm("TTRItemD", qry, "Code", StrWhere, fndSrcCode.Value, "Code", True)
            txtItem.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_code  from TSPL_ITEM_MASTER   where Item_Code ='" + fndSrcCode.Value + "' or Short_Description ='" + fndSrcCode.Value + "' "))
            txtSrcDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Desc  from TSPL_ITEM_MASTER   where Item_Code ='" + fndSrcCode.Value + "' or Short_Description ='" + fndSrcCode.Value + "' "))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Sub fillGridReport()
        Try
            If clsCommon.myLen(txtTenderNo.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Select Tender First!", "Information")
                txtTenderNo.Focus()
                Exit Sub
            End If
            If clsCommon.myLen(fndSrcCode.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Select Item First!", "Information")
                fndSrcCode.Focus()
                Exit Sub
            End If
            Dim Strquery As String = "select sum(TSPL_TENDER_DETAIL.Qty) as qty  from TSPL_TENDER_DETAIL where TSPL_TENDER_DETAIL.Item_Code ='" + txtItem.Text + "' and  TSPL_TENDER_DETAIL.DocumentCode ='" + txtTenderNo.Value + "' "

            Dim query As String
            gv1.DataSource = Nothing
            '  ,TSPL_TENDER_DETAIL.Qty as [RAL Qty]
            '        	left outer join TSPL_PAYMENT_DETAIL on TSPL_PAYMENT_DETAIL.Document_no=TSPL_SRN_HEAD.SRN_No
            'and TSPL_PAYMENT_DETAIL.Payment_Type='PY'
            query = "select ROW_NUMBER() OVER(ORDER BY convert(varchar, TSPL_GRN_HEAD.GRN_Date,103),TSPL_GRN_HEAD.GRN_NO ASC) as SNo
                ,tspl_tender_header.DocumentCode as [RAL No]
                ,convert(varchar, tspl_tender_header.DocumentDate,103) as [RAL Date]
                ,TSPL_TENDER_DETAIL.location as [Location Code],tspl_location_master.Location_Desc AS [Location Name]
                ,TSPL_TENDER_DETAIL.Item_Code  as [Item Code],tspl_item_master.Item_Desc as [Item Name]
                ,convert(varchar, TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103) as [PO Date],TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No as [PO No]
                ,convert(varchar, TSPL_GRN_HEAD.GRN_Date,103) as [GRN Date],TSPL_GRN_HEAD.GRN_No as [GRN No]
                ,TSPL_GRN_HEAD.VehicleNo as [Vehicle No]
                ,TSPL_GRN_HEAD.Vendor_Code as [Vendor Code],TSPL_VENDOR_MASTER.Vendor_Name as [Vendor Name]
                ,Cast(TSPL_GRN_DETAIL.GRN_Qty as decimal(18,2)) AS [GRN Qty]
                ,(case when TSPL_GRN_HEAD.Status=1 then 'Posted' else 'UnPosted' end) as [GRN Status] 
                ,convert(varchar, TSPL_GRN_HEAD.VisualQCUpdatedDate,103) as [Random QC Date]
                ,(case when TSPL_GRN_HEAD.VisualQCStatus=5 then 'Not Applicable' when TSPL_GRN_HEAD.VisualQCStatus=1 then 'Ok' 
				when TSPL_GRN_HEAD.VisualQCStatus='2' then 'Not Ok' when TSPL_GRN_HEAD.VisualQCStatus='3' then 'Partial Ok'  
				when TSPL_GRN_HEAD.VisualQCStatus='4' then 'On Hold' else 'Pending' end) as [Random QC Status]
                ,TSPL_GRN_HEAD.VisualQCRemarks as [Random QC Remarks]
                ,convert(varchar, TSPL_GRN_HEAD.VisualQCUpdatedDateSecond,103) as [Random QC Date Second]
                ,case when TSPL_GRN_HEAD.VisualQCUpdatedDateSecond is not null then (case when TSPL_GRN_HEAD.VisualQCStatusSecond=5 then 'Not Applicable' when TSPL_GRN_HEAD.VisualQCStatusSecond=1 then 'Ok' 
                when TSPL_GRN_HEAD.VisualQCStatusSecond='2' then 'Not Ok' when TSPL_GRN_HEAD.VisualQCStatusSecond='3' then 'Partial Ok'  
                when TSPL_GRN_HEAD.VisualQCStatusSecond='4' then 'On Hold' else 'Pending' end) else '' end as [Random QC Status Second]
                ,VisualQCRemarksSecond as [Random QC Remarks Second]
                ,convert(varchar, TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date,103) as [Weighment Date],TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code as [Weighment No]
                ,TSPL_PO_WEIGHTMENT_DETAIL.Gross_Weight as [Weighment Gross Weight],TSPL_PO_WEIGHTMENT_DETAIL.Tare_Weight as [Weighment Tare Weight]
                ,TSPL_PO_WEIGHTMENT_DETAIL.Extra_Weight as [Gunny Bag Weight],TSPL_PO_WEIGHTMENT_DETAIL.Net_Weight as [Weighment Net Weight]
                ,TSPL_PO_WEIGHTMENT_HEAD.Type as [Weighment Type]
				,(case when TSPL_PO_WEIGHTMENT_HEAD.Status=1 then 'Posted' when TSPL_PO_WEIGHTMENT_HEAD.Status=0 then 'UnPosted' end) as [Weighment Status]
                ,convert(varchar, TSPL_MRN_HEAD.MRN_Date,103) as [MRN Date],TSPL_MRN_HEAD.MRN_No as [MRN No]
                ,convert(varchar, TSPL_QC_CHECK_HEAD.Document_Date,103) as [Chemical QC Date],TSPL_QC_CHECK_HEAD.Document_Code as [Chemical QC No]
                ,TSPL_QC_CHECK_HEAD.QC_Status AS [Chemical QC Result]
                ,(case when TSPL_QC_CHECK_HEAD.Posted=1 then 'Posted' when TSPL_QC_CHECK_HEAD.Posted=0 then 'UnPosted' end) as [Chemical QC Status]
                ,(case when TSPL_QC_CHECK_HEAD.Approved_For_SRN=1 then 'Approved' when TSPL_QC_CHECK_HEAD.Approved_For_SRN=0 then 'UnApproved' end) as [Chemical QC Approved Status]
                ,convert(varchar, TSPL_SRN_HEAD.SRN_Date,103) as [SRN Date],TSPL_SRN_HEAD.SRN_No as [SRN No]
                ,TSPL_SRN_DETAIL.Item_Cost AS [SRN Rate]
                ,TSPL_SRN_DETAIL.MRN_Qty AS [SRN Received Qty]
                ,Cast(TSPL_SRN_DETAIL.Rejected_Qty as decimal(18,2)) as [SRN Rejected Qty]
                ,TSPL_SRN_DETAIL.MRN_Qty-TSPL_SRN_DETAIL.Rejected_Qty  as [SRN Accepted Qty]
                ,TSPL_SRN_DETAIL.Amount as [SRN Amount]
                ,TSPL_SRN_DETAIL.Total_Tax_Amt as [SRN Tax Amount]
                ,TSPL_SRN_DETAIL.Item_Net_Amt as [SRN Net Amount]
                ,(case when TSPL_SRN_HEAD.Status=1 then 'Posted' when TSPL_SRN_HEAD.Status=0 then 'UnPosted' end) as [SRN Status] 
                ,convert(varchar, TSPL_PI_HEAD.PI_Date,103) as [Purchase Invoice Date]
                ,TSPL_PI_HEAD.PI_No as [Purchase Invoice No]
                ,(case when TSPL_PI_HEAD.Status=1 then 'Posted' when TSPL_PI_HEAD.Status=0 then 'UnPosted' end) as [Purchase Invoice Status]
				,Penalty.PenaltyAmt as [Penalty Amount]
				,DEDUCTION_SECURITY.Ded_Amt as [Security Deduction Amount]
				,TSPL_PAYMENT_DETAIL.Applied_Amount as [Payment Amount]
                from TSPL_TENDER_DETAIL
				left outer join tspl_tender_header on tspl_tender_header.DocumentCode=TSPL_TENDER_DETAIL.DocumentCode
				left outer join tspl_purchase_order_head on tspl_purchase_order_head.RefTendorNo=tspl_tender_header.DocumentCode 
				and TSPL_TENDER_DETAIL.Location=tspl_purchase_order_head.Bill_To_Location
                and tspl_purchase_order_head.Against_Tender='Y'
				left outer join TSPL_PURCHASE_ORDER_DETAIL on TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No=tspl_purchase_order_head.PurchaseOrder_No
				and TSPL_PURCHASE_ORDER_DETAIL.Item_Code=TSPL_TENDER_DETAIL.Item_Code
				AND tspl_purchase_order_head.Bill_To_Location= TSPL_TENDER_DETAIL.Location
			   left outer join TSPL_GRN_DETAIL on TSPL_GRN_DETAIL.PO_Id=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No
				 and TSPL_GRN_DETAIL.Item_Code=TSPL_PURCHASE_ORDER_DETAIL.Item_Code
				 and TSPL_GRN_DETAIL.Item_Code=TSPL_TENDER_DETAIL.Item_Code
                  left outer join TSPL_GRN_HEAD ON TSPL_GRN_DETAIL.GRN_No=TSPL_GRN_HEAD.GRN_No
                 and TSPL_TENDER_DETAIL.Location=TSPL_GRN_HEAD.Bill_To_Location
                 and tspl_purchase_order_head.Bill_To_Location=TSPL_TENDER_DETAIL.Location
                left outer join TSPL_PO_WEIGHTMENT_HEAD on TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No=TSPL_GRN_HEAD.GRN_No
                left outer join TSPL_PO_WEIGHTMENT_DETAIL on TSPL_PO_WEIGHTMENT_DETAIL.Weighment_Code=TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code
                and TSPL_PO_WEIGHTMENT_DETAIL.Item_Code=TSPL_GRN_DETAIL.item_code
				 and TSPL_GRN_DETAIL.Item_Code=TSPL_PURCHASE_ORDER_DETAIL.Item_Code
				 and TSPL_GRN_DETAIL.Item_Code=TSPL_TENDER_DETAIL.Item_Code
                left outer join TSPL_MRN_HEAD on TSPL_MRN_HEAD.Against_GRN=TSPL_GRN_HEAD.GRN_No
                left outer join TSPL_QC_CHECK_HEAD on TSPL_QC_CHECK_HEAD.Gate_Entry_No=TSPL_GRN_HEAD.GRN_No
                left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.Against_GRN=TSPL_GRN_HEAD.GRN_No and TSPL_SRN_HEAD.Against_MRN=TSPL_MRN_HEAD.MRN_No
				and TSPL_SRN_HEAD.Status=1
                left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.Against_GRN=TSPL_GRN_HEAD.GRN_No and TSPL_PI_HEAD.Against_MRN=TSPL_MRN_HEAD.MRN_No 
                and TSPL_PI_HEAD.Against_SRN=TSPL_SRN_HEAD.SRN_No
                left outer join tspl_location_master on tspl_location_master.location_code=TSPL_TENDER_DETAIL.location
                left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_GRN_HEAD.Vendor_Code
                left outer join TSPL_SRN_DETAIL ON TSPL_SRN_DETAIL.SRN_NO =TSPL_SRN_HEAD.SRN_NO
                AND TSPL_SRN_DETAIL.Item_Code=TSPL_GRN_DETAIL.Item_Code
				and TSPL_SRN_DETAIL.item_code = TSPL_PO_WEIGHTMENT_DETAIL.Item_Code
				 and TSPL_SRN_DETAIL.item_code=TSPL_PURCHASE_ORDER_DETAIL.Item_Code
				 and TSPL_SRN_DETAIL.item_code=TSPL_TENDER_DETAIL.Item_Code
				left outer join (select SRN_No,Against_TenderNo,Item_Code,SUM(Penalty) as PenaltyAmt  from TSPL_SRN_TENDER
				group by SRN_No,Against_TenderNo,Item_Code)Penalty on Penalty.SRN_No=TSPL_SRN_DETAIL.SRN_No
				AND Penalty.Against_TenderNo=TSPL_TENDER_DETAIL.DocumentCode
				AND Penalty.Item_Code = TSPL_SRN_DETAIL.Item_Code
				left outer join (select SRN_No,Item_Code,SUM(Ded_Amt) as Ded_Amt  from TSPL_SRN_DEDUCTION_SECURITY
				group by SRN_No,Item_Code)DEDUCTION_SECURITY on DEDUCTION_SECURITY.SRN_No=TSPL_SRN_DETAIL.SRN_No
				AND DEDUCTION_SECURITY.Item_Code = TSPL_SRN_DETAIL.Item_Code
				left outer join (select TSPL_PAYMENT_DETAIL.Document_no,sum(TSPL_PAYMENT_DETAIL.Applied_Amount) as Applied_Amount from TSPL_PAYMENT_DETAIL
				WHERE TSPL_PAYMENT_DETAIL.Payment_Type='PY'
				group by TSPL_PAYMENT_DETAIL.Document_no
				) TSPL_PAYMENT_DETAIL on TSPL_PAYMENT_DETAIL.Document_no=TSPL_PI_HEAD.PI_NO
                left outer join tspl_item_master ON tspl_item_master.item_code=TSPL_TENDER_DETAIL.ITEM_CODE
				where TSPL_TENDER_HEADER.Posted=1 "
            query += " AND TSPL_TENDER_HEADER.DocumentCode='" + txtTenderNo.Value + "'"
            query += " AND TSPL_TENDER_DETAIL.Item_Code='" + txtItem.Text + "'"
            If multiLocationFinder.arrValueMember IsNot Nothing AndAlso multiLocationFinder.arrValueMember.Count > 0 Then
                query += " and TSPL_TENDER_DETAIL.Location in (" + clsCommon.GetMulcallString(multiLocationFinder.arrValueMember) + ") " + Environment.NewLine
                Strquery += " and TSPL_TENDER_DETAIL.Location in (" + clsCommon.GetMulcallString(multiLocationFinder.arrValueMember) + ") " + Environment.NewLine
            Else
                If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    query += " and TSPL_TENDER_DETAIL.Location in (" + objCommonVar.strCurrUserLocations + ")"
                    Strquery += " and TSPL_TENDER_DETAIL.Location in (" + objCommonVar.strCurrUserLocations + ")"
                End If
            End If


            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(query)
            If (dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0) Then
                gv1.Visible = True
                gv1.DataSource = dt2
                gv1.ReadOnly = True
                SetGridFormat(gv1)
                ReStoreGridLayout()

                'If dt2.Columns.Contains("RAL Qty") Then
                '    txtQty.Value = clsCommon.myCdbl(dt2.Rows(0).Item("RAL Qty"))
                'End If
                txtQty.Value = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue(Strquery))
                If dt2.Columns.Contains("SRN Rejected Qty") Then
                    txtRejQty.Value = clsCommon.myCDecimal(dt2.Compute("SUM([SRN Rejected Qty])", ""))
                End If
                If dt2.Columns.Contains("SRN Accepted Qty") Then
                    txtAccQty.Value = clsCommon.myCDecimal(dt2.Compute("SUM([SRN Accepted Qty])", ""))
                End If
                If clsCommon.myCDecimal(txtAccQty.Value) >= clsCommon.myCDecimal(txtQty.Value) Then
                    txtPenQty.Value = "0"
                Else
                    txtPenQty.Value = clsCommon.myCDecimal(txtQty.Value) - clsCommon.myCDecimal(txtAccQty.Value)
                End If

            Else
                common.clsCommon.MyMessageBoxShow("No Data Found for this Item ")
                gv1.DataSource = Nothing
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub SetGridFormat(ByRef Gv1 As RadGridView)
        Gv1.ShowGroupPanel = True
        Gv1.ShowRowHeaderColumn = False
        Gv1.AllowAddNewRow = False
        Gv1.AllowDeleteRow = False
        Gv1.EnableFiltering = True
        Gv1.ShowFilteringRow = True
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        Dim summaryRowItem As New GridViewSummaryRowItem()
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).BestFit()
            If Gv1.Columns(ii).HeaderText.Contains("Qty") = True OrElse Gv1.Columns(ii).HeaderText.Contains("Amount") = True Then
                Dim TempQtyAmount As String = Gv1.Columns(ii).HeaderText
                Dim TempSumQtyAmount As New GridViewSummaryItem(TempQtyAmount, "{0:n2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(TempSumQtyAmount)
                Gv1.Columns(TempQtyAmount).FormatString = "{0:n2}"
            End If
        Next

        'If Gv1.Columns.Contains("RAL Qty") Then
        '    Gv1.Columns("RAL Qty").IsVisible = False
        'End If

        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        Gv1.AutoSizeRows = False
        Gv1.BestFitColumns()
    End Sub


    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        reset()
    End Sub
    Sub reset()
        txtTenderNo.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        fndSrcCode.Value = ""
        txtSrcDesc.Text = ""
        txtItem.Text = ""
        txtQty.Value = "0"
        txtAccQty.Value = "0"
        txtPenQty.Value = "0"
        txtRejQty.Value = "0"
        multiLocationFinder.arrValueMember = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        gv1.Visible = False
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub


    Private Sub btnReport_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReport.Click
        Try
            PageSetupReport_ID = Me.Form_ID
            TemplateGridview = gv1
            fillGridReport()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub RadMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem4.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub ReStoreGridLayoutDetails()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then

                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)

                End If

            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub



    Private Sub print(ByVal exporter As EnumExportTo)


        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmTenderTrackingReport & "'"))
            arrHeader.Add("Tender No : " + txtTenderNo.Value)
            arrHeader.Add("Tender Date : " + clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Item : " + txtSrcDesc.Text)

            If multiLocationFinder.arrDispalyMember IsNot Nothing AndAlso multiLocationFinder.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(multiLocationFinder.arrDispalyMember))
            End If

            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
            Else
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    'KUNAL > TICKET : 
    Private Sub multiLocationFinder__My_Click(sender As Object, e As EventArgs) Handles multiLocationFinder._My_Click
        Dim qry As String = "SELECT LOCATION_CODE , Location_Desc  FROM TSPL_LOCATION_MASTER where 1=1"
        Try
            If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                qry += " and TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            multiLocationFinder.arrValueMember = clsCommon.ShowMultipleSelectForm("purchaseHistoryLocsFinder", qry, "LOCATION_CODE", "Location_Desc", multiLocationFinder.arrValueMember, multiLocationFinder.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        print(EnumExportTo.PDF)
    End Sub

    Private Sub txtTenderNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtTenderNo._MYValidating
        Try
            Dim qry As String = "select tspl_tender_header.DocumentCode as Code,tspl_tender_header.DocumentDate as Date from tspl_tender_header "
            txtTenderNo.Value = clsCommon.ShowSelectForm("TTRNo", qry, "Code", "Posted=1", fndSrcCode.Value, "Code", True)
            txtDate.Value = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select tspl_tender_header.DocumentDate from tspl_tender_header where tspl_tender_header.DocumentCode ='" + txtTenderNo.Value + "'"))
            fndSrcCode.Value = ""
            txtSrcDesc.Text = ""
            txtItem.Text = ""
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class
