Imports System.IO
Imports common


Public Class rptCostCenterReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim strQry As String = ""

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub


    Private Sub RptInventoryMovement_Load(sender As Object, e As EventArgs) Handles Me.Load
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Sub Reset()
        ' ToDate.Value = clsCommon.GETSERVERDATE()
        ' fromDate.Value = ToDate.Value.AddMonths(-1)
        'txtItem.arrValueMember = Nothing
        'txtLocation.arrValueMember = Nothing
        'txtTransaction.arrValueMember = Nothing
        'txtItemType.arrValueMember = Nothing
        txtCostCenter.arrValueMember = Nothing
        txtCostCenter.arrDispalyMember = Nothing

        txtAccount.arrValueMember = Nothing
        txtAccount.arrDispalyMember = Nothing

        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            PageSetupReport_ID = MyBase.Form_ID
            TemplateGridview = Gv1
            Dim qry As String = ""
            Dim dt As New DataTable
            Dim whr As String = ""
            Dim strItemType As String = ""
            If txtCostCenter.arrValueMember IsNot Nothing AndAlso txtCostCenter.arrValueMember.Count > 0 Then
                whr += " and TSPL_JOURNAL_DETAILS.Cost_Centre_Code in (" + clsCommon.GetMulcallString(txtCostCenter.arrValueMember) + ")"
            End If

            If txtAccount.arrValueMember IsNot Nothing AndAlso txtAccount.arrValueMember.Count > 0 Then
                whr += " and TSPL_JOURNAL_DETAILS.Account_code in (" + clsCommon.GetMulcallString(txtAccount.arrValueMember) + ")"
            End If

            qry = " select Final.Hirerachy_Code as [Hirerachy Level Code] , max(Final.[Hirerachy Name]) as [Hirerachy Name],Final.Cost_Centre_Code as [Cost Centre Code], max(Final.[Cost Center Name]) as [Cost Center Name], Final.Account_code as [Account Code],max ( Final.[Account Name]) as [Account Description], Final.Voucher_No as [Voucher No],  (Final.Voucher_Date) as [Voucher Date], max(Final.Voucher_Desc) as [Voucher Description], max(Source_Code) as [Trans Type] ,max (Final.[Vendor Code]) as [Vendor Code], max(Vendor) as Vendor, max(Final.[Customer Code]) as [Customer Code], max(Final.[Customer]) as [Customer], max(Final.Source_Narration) as [Narration], Final.Source_Doc_No as [Source Doc No], max (final.Source_Doc_Date) as [Source Doc Date], max(Vendor_Invoice_No) as [Vendor Invoice No], max(Vendor_Invoice_Date) as [Vendor Invoice Date], sum (DrAmt) as DrAmt,sum (CrAmt) as CrAmt , sum (DrAmt) - sum (CrAmt) as Closing  from (

                    select TSPL_JOURNAL_DETAILS.Hirerachy_Code,TSPL_HIRERACHY_LEVEL_MASTER.Description as [Hirerachy Name] , TSPL_JOURNAL_DETAILS.Cost_Centre_Code,TSPL_COST_CENTRE_FINANCIAL.name as [Cost Center Name],TSPL_JOURNAL_DETAILS.Account_code,TSPL_GL_ACCOUNTS.Description as [Account Name],TSPL_JOURNAL_MASTER.Voucher_No, convert (varchar,TSPL_JOURNAL_MASTER.Voucher_Date,103) as Voucher_Date, TSPL_JOURNAL_MASTER.Voucher_Desc ,TSPL_JOURNAL_MASTER.Source_Code ,Case When TSPL_JOURNAL_MASTER.Source_Type ='V' Then TSPL_JOURNAL_MASTER.CustVend_Code  Else '' End as [Vendor Code],Case When TSPL_JOURNAL_MASTER.Source_Type ='V' Then TSPL_JOURNAL_MASTER.CustVend_name Else '' End as [Vendor],Case When TSPL_JOURNAL_MASTER.Source_Type ='C' Then TSPL_JOURNAL_MASTER.CustVend_Code  Else '' End as [Customer Code],Case When TSPL_JOURNAL_MASTER.Source_Type ='C' Then TSPL_JOURNAL_MASTER.CustVend_name Else '' End as [Customer] ,  TSPL_JOURNAL_MASTER.Source_Narration,TSPL_JOURNAL_MASTER.Source_Doc_No, convert (varchar,TSPL_JOURNAL_MASTER.Source_Doc_Date,103) as Source_Doc_Date,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No , case when len (isnull (TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No,'')  ) >0 then convert (varchar,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) else '' end as Vendor_Invoice_Date  ,case  when TSPL_JOURNAL_DETAILS.Amount >=0 then TSPL_JOURNAL_DETAILS.Amount else 0 end as DrAmt , case  when TSPL_JOURNAL_DETAILS.Amount <0 then TSPL_JOURNAL_DETAILS.Amount*-1 else 0 end as CrAmt,  case  when TSPL_JOURNAL_DETAILS.Amount >=0 then TSPL_JOURNAL_DETAILS.Amount else 0 end - case  when TSPL_JOURNAL_DETAILS.Amount <0 then TSPL_JOURNAL_DETAILS.Amount*-1 else 0 end as Closing from TSPL_JOURNAL_DETAILS left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No = TSPL_JOURNAL_DETAILS.Voucher_No 
                     LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_JOURNAL_MASTER.Source_Doc_No 
                     left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code = TSPL_JOURNAL_DETAILS.Account_code
                     left outer join (select TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code as Code , TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name as Name from TSPL_COST_CENTRE_FINANCIAL union select TSPL_FA_COST_CENTER_MASTER.CostCenter_Code as Code , TSPL_FA_COST_CENTER_MASTER.CostCenter_Name as Name from TSPL_FA_COST_CENTER_MASTER) as TSPL_COST_CENTRE_FINANCIAL on TSPL_COST_CENTRE_FINANCIAL.Code = TSPL_JOURNAL_DETAILS.Cost_Centre_Code
                     left outer join TSPL_HIRERACHY_LEVEL_MASTER on TSPL_HIRERACHY_LEVEL_MASTER.Hirerachy_Code = TSPL_JOURNAL_DETAILS.Hirerachy_Code
                    where  len (isnull ( TSPL_JOURNAL_DETAILS.Cost_Centre_Code,'')) > 0  and convert (date, TSPL_JOURNAL_MASTER.Voucher_Date,103) > =  convert (date,'" + fromDate.Value + "',103) and convert (date, TSPL_JOURNAL_MASTER.Voucher_Date,103) < = convert (date,'" + ToDate.Value + "',103)  " + whr + "
                    ) as Final group by Hirerachy_Code , Cost_Centre_Code , Account_code , Voucher_No,Voucher_Date,Source_Doc_No
                    order by Voucher_No, convert (date,Voucher_Date,103) asc 
                     "


            'If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
            '    qry += " and Final.[Item Code] in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ")"
            'End If
            'If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            '    qry += " and Final.[Location Code] in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
            'End If
            'If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
            '    qry += " and Final.[Document Type Code] in (" + clsCommon.GetMulcallString(txtTransaction.arrValueMember) + ")"
            'End If

            'If txtItemType.arrValueMember IsNot Nothing AndAlso txtItemType.arrValueMember.Count > 0 Then
            '    qry += " and Final.Item_Type in (" + clsCommon.GetMulcallString(txtItemType.arrValueMember) + ")"
            'End If

            'If ChkPosted.IsChecked = True Then
            '    qry += " and Final.[Document Status] = 'Posted' "
            'ElseIf ChkUnPosted.IsChecked = True Then
            '    qry += " and Final.[Document Status] = 'Not Posted'"
            'End If

            'qry += " order by   Final.[Batch No]  asc "

            dt = clsDBFuncationality.GetDataTable(qry)
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()

            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                Gv1.DataSource = dt
                For ii As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(ii).ReadOnly = True
                Next
                ' Gv1.Columns("Trans Type").IsVisible = False
                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.BestFitColumns()
                Gv1.EnableFiltering = True
                Gv1.Columns("DrAmt").FormatString = "{0:n2}"
                Gv1.Columns("CrAmt").FormatString = "{0:n2}"
                Gv1.Columns("Closing").FormatString = "{0:n2}"
                Gv1.Columns("Trans Type").AllowHide = True
                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim itemDRAmt As New GridViewSummaryItem("DrAmt", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(itemDRAmt)
                Dim itemCRAmt As New GridViewSummaryItem("CrAmt", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(itemCRAmt)
                Dim itemClosing As New GridViewSummaryItem("Closing", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(itemClosing)

                Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            Else
                clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            End If

            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= Gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To Gv1.Columns.Count - 1 Step ii + 1
                        Gv1.Columns(ii).IsVisible = False
                        Gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    Gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    'Private Sub txtTransaction__My_Click(sender As Object, e As EventArgs)
    '    Dim qry As String = " select Code,Name from TSPL_INVENTORY_SOURCE_CODE "

    '    txtTransaction.arrValueMember = clsCommon.ShowMultipleSelectForm("TransMulSe@Batch", qry, "Code", "Name", txtTransaction.arrValueMember, txtTransaction.arrDispalyMember)
    'End Sub
    'Private Sub txtItem__My_Click(sender As Object, e As EventArgs)
    '    Dim qry As String
    '    qry = " select Item_Code,Item_Desc from TSPL_ITEM_MASTER  order by Item_Code "
    '    txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel@Batch", qry, "Item_Code", "Item_Desc", txtItem.arrValueMember, txtItem.arrDispalyMember)
    'End Sub
    'Private Sub txtLocation__My_Click(sender As Object, e As EventArgs)
    '    Dim qry As String = " select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER where Location_Type='Physical'"
    '    txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransMulSe@Batch", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    'End Sub
    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        Dim ReportID As String = MyBase.Form_ID
        If clsCommon.myLen(ReportID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = Gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If


            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub


    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptCostCenterReport & "'"))
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            If txtCostCenter.arrDispalyMember IsNot Nothing AndAlso txtCostCenter.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Cost Center : " + clsCommon.GetMulcallStringWithComma(txtCostCenter.arrDispalyMember))
            End If
            If txtAccount.arrDispalyMember IsNot Nothing AndAlso txtAccount.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Account : " + clsCommon.GetMulcallStringWithComma(txtAccount.arrDispalyMember))
            End If
            'If txtTransaction.arrDispalyMember IsNot Nothing AndAlso txtTransaction.arrDispalyMember.Count > 0 Then
            '    arrHeader.Add("Transaction : " + clsCommon.GetMulcallStringWithComma(txtTransaction.arrDispalyMember))
            'End If
            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("Cost Center Report", Gv1, arrHeader, Me.Text)
            Else
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Cost Center Report", Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Export(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Export(EnumExportTo.PDF)
    End Sub

    Private Sub Gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        Try

            If e.Column Is Gv1.Columns("Voucher No") OrElse e.Column Is Gv1.Columns("Final.Voucher_Desc") Then
                Dim itemcode As String = ""
                itemcode = clsCommon.myCstr(Gv1.CurrentRow.Cells("Voucher No").Value)
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.journalEntry, itemcode)
            ElseIf e.Column Is Gv1.Columns("Source Doc No") Then
                DrillDown()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    'Private Sub txtItemType__My_Click(sender As Object, e As EventArgs)
    '    txtItemType.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemTypForBatchItemRep", FrmItemMasterRMOther.LoadItemTypeQuery(), "Code", "Name", txtItemType.arrValueMember, txtItemType.arrDispalyMember)
    'End Sub
    Sub DrillDown()
        Try
            If Gv1.CurrentRow.Index >= 0 Then



                'If clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "BulkSRN") = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmBulkMilkSRN, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "CRATE-REC") = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCrateReceviedDairySale, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "Disassembly") = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmAssembDis, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "DispChallan") = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCDispatch, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "FS-SH") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "PS-SH") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "MCC-MSALE") = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmShipmentProductSale, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "FS-SR") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "PS-SR") = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSaleReturndairy, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "IC-AD") = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnStoreAdjustment, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "ISSTRAN") = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnIssueReturn, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "MCC-MSRN") = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkSRN, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "MilkTransferIn") = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkTransferIn, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "NRGP") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "RGP") = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnGatePass, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "PP_ISSUE") = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProcessProductionIssueEntry, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "PP_STD-FQC") = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ProcessProductionStandardizationFinalQC, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "PROD_ENTRY") = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProductionEntry, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "SRN") = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnSRN, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "SRN-RET") = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.SRNReturn, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "Transfer") = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.Transfer, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "Purchase Return") = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseReturn, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "PRD_STG_PROC") = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProcessProductionStageProcess, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "MCC-MSR") = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCMaterialSaleReturn, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "BulkSRNRet") = CompairStringResult.Equal Then
                '    'No separate screen for display record
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "DisCanSale") = CompairStringResult.Equal Then
                '    Dim strDocNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select CanSale_Doc_No from TSPL_CANSALE_DISPATCH_HEAD where Document_No='" + clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value) + "'"))
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmCanSale, clsCommon.myCstr(strDocNo))
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "DispatchBS") = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmDispatchBulkSale, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "ScrapIn") = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ScrapSale, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))

                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "BulkSRNTrade") = CompairStringResult.Equal Then
                '    'No separate screen for display record
                '    'Dim strDocNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ISNULL(tspl_bulk_milk_srn.Challan_No,'') AS Challan_No From tspl_bulk_milk_srn where tspl_bulk_milk_srn.srn_no='" + clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value) + "'"))
                '    'clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmDispatchBulkSaleTrade, strDocNo)
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "CSA-SALE") = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSASaleInvoice, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "CSA-SALEPATTI-RETURN") = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSASalePattiReturn, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "DispatchBSTrade") = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmDispatchBulkSaleTrade, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                '    ''''''''''''''''''''''''''''''''''''
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "DispChallanRet") = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCTankerDispatchReturn, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "DispChallan-RET") = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.MCCDispatchReturn, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "EX_SALE_IN") = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmEXSalesInvoice, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "JWO-SRN") = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.JWO_SRN, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "MCC-AISSUE") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "MCC-ARETURN") = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmVSPAssetIssue, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))

                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "MCC-IISSUE") = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmVSPItemIssue, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "MilkTransferInReturn") = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkTransferInReturn, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "MilkTransferJobWork") = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkJobWorkTransfer, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "MilkTransJWOReturn") = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkJobWorkTransferReturn, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "MJ-SR") = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmJobMilkSRN, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "M-PURRETURN") = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmMilkPurchaseReturn, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "MS-SR") = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ScrapSaleRetrun, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "MT_SALE_IN") = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSalesInvoiceMT, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "PP_STDN") = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProcessProductionStandardization, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "PROD_WR") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "Prod-Scrap") = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmWreckageBooking, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))

                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "Sale Return") = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSaleReturnProductSale, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "SaleReturnBS") = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmBulkSaleReturn, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "SD-CSATRANS") = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSATransfer, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "SD-CSATRANS-RETURN") = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSATransferReturn, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "TRN-RET") = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.TransferReturn, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "SI-MT") = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSiloMilkTransfer, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                'End If
                If clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "AR-PY") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "AR-PI") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "AR-MI") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "AR-UC") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "AR-OA") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "AR-DC") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ReceiptEntry, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "AP-PY") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "AP-MI") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.PaymentEntryNew, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "AP-IN") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "AP-DN") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "AP-CN") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "MI-PI") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "MI-CO") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmVendorService, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "SD-IN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSNSaleInvoice, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "BK-TF") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.bankTransfer, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "IC-AD") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnStoreAdjustment, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "PO-RC") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnSRN, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "SD-LO") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.LoadOut, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "MM-TF") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.Transfer, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "RV-TA") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.reverseTransaction, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "AR-AD") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ReceiptAdjustmentEntry, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "PO-RT") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseReturn, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "SD-SR") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.saleReturn, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "AR-IN") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "AR-DN") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "AR-CN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnARInvoiceEntry, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "AR-SI") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ScrapSale, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))

                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "EX-AD") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmExpiryDateEntry, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "MI-SR") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkSRN, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                Else
                    Return
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCostCenter__My_Click(sender As Object, e As EventArgs) Handles txtCostCenter._My_Click
        Try
            strQry = "select TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code as [Code] ,TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name as Name,ISNULL(TSPL_COST_CENTRE_FINANCIAL.Hirerachy_Level_Code,'') AS [Hirerachy Level Code],ISNULL(TSPL_COST_CENTRE_FINANCIAL.Cost_Centre_Fin_Level_Code,'') AS [Cost Centre Fin Level Code],ISNULL(TSPL_COST_CENTRE_FINANCIAL.Hirerachy_Level,'') AS [Hirerachy Level] ,TSPL_COST_CENTRE_FINANCIAL.Created_By as [Created By] ,Convert(varchar,TSPL_COST_CENTRE_FINANCIAL.Created_Date,103) as [Created Date] ,TSPL_COST_CENTRE_FINANCIAL.Modified_By as [Modified By] ,Convert(varchar,TSPL_COST_CENTRE_FINANCIAL.Modified_Date,103) as [Modified Date]  From TSPL_COST_CENTRE_FINANCIAL "
            txtCostCenter.arrValueMember = clsCommon.ShowMultipleSelectForm("Accounts@CostCenterReport", strQry, "Code", "Name", txtCostCenter.arrValueMember, txtCostCenter.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtAccount__My_Click(sender As Object, e As EventArgs) Handles txtAccount._My_Click
        Try
            strQry = " select Account_Code as [Code] ,Description as [Description],TSPL_GL_ACCOUNTS.GL_Main_Code as [GL Main Account Code],TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account_Desc AS [GL Main Account Description],TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Sub_Group_Code As [Sub Group Code],TSPL_ACCOUNT_SUB_GROUPS.Account_Sub_Group_Desc AS [Sub Group Description],TSPL_ACCOUNT_SUB_GROUPS.Account_Group_Code As [Account Group Code],TSPL_ACCOUNT_GROUPS.Account_Group_Desc as [Account Group Desc],TSPL_ACCOUNT_GROUPS.Account_Main_Group_Code as [Main Group Code],TSPL_ACCOUNT_MAIN_GROUPS.Account_Main_Group_Desc as [Account Main Group Description],TSPL_ACCOUNT_MAIN_GROUPS.Group_Type as [Group Type] ,Str_Code as [Account Structure Code] ,Str_Description as [Account Structure Description] ,(case when status='Y' then 'Active' else 'In Active' end) as [Status] ,ControlAccount as [Control Account]  ,multicurrency as [Multi Currency] ,Account_Seg_Code1 as [Account Segment Code1] ,Account_Seg_Desc1 as [Account Segment Description1] ,TSPL_GL_ACCOUNTS.Created_By as [Created By] ,TSPL_GL_ACCOUNTS.Created_Date as [Created Date] ,TSPL_GL_ACCOUNTS.Modify_By as [Modify By] ,TSPL_GL_ACCOUNTS.Modify_Date as [Modify Date] ,TSPL_GL_ACCOUNTS.Comp_Code as [Company Code]  from TSPL_GL_ACCOUNTS  left outer join TSPL_ACCOUNT_MAIN_GL_ACCOUNT on TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account=TSPL_GL_ACCOUNTS.GL_Main_Code  left outer join TSPL_ACCOUNT_SUB_GROUPS on TSPL_ACCOUNT_SUB_GROUPS.Account_Sub_Group_Code=TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Sub_Group_Code  left outer join TSPL_ACCOUNT_GROUPS on TSPL_ACCOUNT_GROUPS.Account_Group_Code= TSPL_ACCOUNT_SUB_GROUPS.Account_Group_Code left outer join  TSPL_ACCOUNT_MAIN_GROUPS on TSPL_ACCOUNT_MAIN_GROUPS.Account_Main_Group_Code=TSPL_ACCOUNT_GROUPS.Account_Main_Group_Code " ' Account_Seg_Code2 as [Account Segment Code2] ,Account_Seg_Desc2 as [Account Segment Description2] ,Account_Seg_Code3 as [Account Segment Code3] ,Account_Seg_Desc3 as [Account Segment Description3] ,Account_Seg_Code4 as [Account Segment Code4] ,Account_Seg_Desc4 as [Account Segment Description4] ,Account_Seg_Code5 as [Account Segment Code5] ,Account_Seg_Desc5 as [Account Segment Description5] ,Account_Seg_Code6 as [Account Segment Code6] ,Account_Seg_Desc6 as [Account Segment Description6] ,Account_Seg_Code7 as [Account Segment Code7] ,Account_Seg_Desc7 as [Account Segment Description7] ,Account_Seg_Code8 as [Account Segment Code8] ,Account_Seg_Desc8 as [Account Segment Description8] ,Account_Seg_Code9 as [Account Segment Code9] ,Account_Seg_Desc9 as [Account Segment Description9] ,Account_Seg_Code10 as [Account Segment Code10] ,Account_Seg_Desc10 as [Account Segment Description10] ,Close_To_Seg as [Close To Segment] ,Close_To_Acct as [Close To Account]  ,Rollup_Seq as [Roll Up Sequence] ,TallyAccName as [Tally Account Name] ,Tax_Type as [Tax Type] ,Purchase_Sale_Type as [Purchase Sale Type],
            txtAccount.arrValueMember = clsCommon.ShowMultipleSelectForm("Accounts@CostCenterReport", strQry, "Code", "Description", txtAccount.arrValueMember, txtAccount.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
End Class
