Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO

Public Class RptInventoryMovement
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim strQry As String = ""
    'Dim strDocumentNo As String = ""

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub


    Private Sub RptInventoryMovement_Load(sender As Object, e As EventArgs) Handles Me.Load
        'ToDate.Value = clsCommon.GETSERVERDATE()
        'fromDate.Value = ToDate.Value.AddMonths(-1)
        'MyLabel4.Visible = False
        'lblDocumentNo.Visible = False
        Reset()
        If clsCommon.myLen(Me.Tag) > 0 Then
            lblDocumentNo.Text = clsCommon.myCstr(Me.Tag)
            Dim docDate As Date? = clsCommon.myCDate(clsDBFuncationality.getSingleValue(" select Top 1  XXX.Punching_Date  from ( select Punching_Date from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No = '" + lblDocumentNo.Text + "' union All select Punching_Date from TSPL_INVENTORY_MOVEMENT where Source_Doc_No = '" + lblDocumentNo.Text + "' ) XXX "))
            ToDate.Value = docDate
            fromDate.Value = docDate
            chkBoth.IsChecked = True
            MyLabel4.Visible = True
            lblDocumentNo.Visible = True
            ChkInvenoryNew.Enabled = False
            ChkInventory.Enabled = False
            LoadData(lblDocumentNo.Text)
        End If
    End Sub
    Sub Reset()
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
        txtItem.arrValueMember = Nothing
        txtLocation.arrValueMember = Nothing
        txtTransaction.arrValueMember = Nothing
        lblDocumentNo.Text = ""
        MyLabel4.Visible = False
        lblDocumentNo.Visible = False
        chkBoth.IsChecked = True
        ChkInvenoryNew.Enabled = True
        ChkInventory.Enabled = True
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Function Getreport_id()
        Dim REPORTID As String = ""
        If chkBoth.IsChecked = True Then
            REPORTID = MyBase.Form_ID + "B"
        ElseIf ChkInventory.IsChecked = True Then
            REPORTID = MyBase.Form_ID + "OTM"
        ElseIf ChkInvenoryNew.IsChecked = True Then
            REPORTID = MyBase.Form_ID + "M"
        End If
        Return REPORTID
    End Function


    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = Getreport_id()
        TemplateGridview = Gv1
        LoadData(lblDocumentNo.Text)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
    Private Sub txtTransaction__My_Click(sender As Object, e As EventArgs) Handles txtTransaction._My_Click
        Dim qry As String = " select Code,Name from TSPL_INVENTORY_SOURCE_CODE "

        txtTransaction.arrValueMember = clsCommon.ShowMultipleSelectForm("TransMulSe", qry, "Code", "Name", txtTransaction.arrValueMember, txtTransaction.arrDispalyMember)
    End Sub
    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Dim qry As String
        qry = " select Item_Code,Item_Desc from TSPL_ITEM_MASTER  order by Item_Code "
        txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Item_Code", "Item_Desc", txtItem.arrValueMember, txtItem.arrDispalyMember)
    End Sub
    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER where Location_Type='Physical'"
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransMulSe", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub
    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        Dim ReportID As String = PageSetupReport_ID
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

            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub


    Private Sub Export(ByVal exporter As EnumExportTo)
        'Try
        '    Dim arrHeader As List(Of String) = New List(Of String)()
        '    arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
        '    arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

        '    clsCommon.MyExportToExcelGrid("Inventory Movement Report", Gv1, arrHeader, Me.Text)
        'Catch ex As Exception
        '    common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        'End Try
        Try
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptInventoryMovement & "'"))

                If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                    arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
                End If

                If txtItem.arrDispalyMember IsNot Nothing AndAlso txtItem.arrDispalyMember.Count > 0 Then
                    arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember))
                End If

                If txtTransaction.arrDispalyMember IsNot Nothing AndAlso txtTransaction.arrDispalyMember.Count > 0 Then
                    arrHeader.Add("Transaction : " + clsCommon.GetMulcallStringWithComma(txtTransaction.arrDispalyMember))
                End If
                If exporter = EnumExportTo.Excel Then
                    'Dim sfd As SaveFileDialog = New SaveFileDialog()
                    'Dim filePath As String
                    'sfd.FileName = Me.Text
                    'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                    'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    '    filePath = sfd.FileName
                    'Else
                    '    Exit Sub
                    'End If
                    transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
                    'transportSql.exportdataChilRows(Gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                Else
                    transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("Inventory Movement Report", Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub Gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        Try

            If e.Column Is Gv1.Columns("Item Code") OrElse e.Column Is Gv1.Columns("Item Desc") Then
                Dim itemcode As String = ""
                itemcode = clsCommon.myCstr(Gv1.CurrentRow.Cells("Item Code").Value)
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmItemMasterRMOther, itemcode)
            ElseIf e.Column Is Gv1.Columns("Source Doc No") Then
                DrillDown()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Sub DrillDown()
        Try
            If Gv1.CurrentRow.Index >= 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "BulkSRN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmBulkMilkSRN, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "CRATE-REC") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCrateReceviedDairySale, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "Disassembly") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmAssembDis, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "DispChallan") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCDispatch, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "FS-SH") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "PS-SH") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "MCC-MSALE") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmShipmentProductSale, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "FS-SR") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "PS-SR") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSaleReturndairy, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "IC-AD") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnStoreAdjustment, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "ISSTRAN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnIssueReturn, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "MCC-MSRN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkSRN, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "MilkTransferIn") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkTransferIn, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "NRGP") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "RGP") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnGatePass, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "PP_ISSUE") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProcessProductionIssueEntry, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "PP_STD-FQC") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ProcessProductionStandardizationFinalQC, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "PROD_ENTRY") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProductionEntry, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "SRN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnSRN, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "SRN-RET") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.SRNReturn, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "Transfer") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.Transfer, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "Purchase Return") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseReturn, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "PRD_STG_PROC") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProcessProductionStageProcess, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "MCC-MSR") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCMaterialSaleReturn, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "BulkSRNRet") = CompairStringResult.Equal Then
                    'No separate screen for display record
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "DisCanSale") = CompairStringResult.Equal Then
                    Dim strDocNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select CanSale_Doc_No from TSPL_CANSALE_DISPATCH_HEAD where Document_No='" + clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value) + "'"))
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmCanSale, clsCommon.myCstr(strDocNo))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "DispatchBS") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmDispatchBulkSale, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "ScrapIn") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ScrapSale, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))

                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "BulkSRNTrade") = CompairStringResult.Equal Then
                    'No separate screen for display record
                    'Dim strDocNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ISNULL(tspl_bulk_milk_srn.Challan_No,'') AS Challan_No From tspl_bulk_milk_srn where tspl_bulk_milk_srn.srn_no='" + clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value) + "'"))
                    'clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmDispatchBulkSaleTrade, strDocNo)
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "CSA-SALE") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSASaleInvoice, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "CSA-SALEPATTI-RETURN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSASalePattiReturn, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "DispatchBSTrade") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmDispatchBulkSaleTrade, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                    ''''''''''''''''''''''''''''''''''''
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "DispChallanRet") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCTankerDispatchReturn, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "DispChallan-RET") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.MCCDispatchReturn, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "EX_SALE_IN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmEXSalesInvoice, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "JWO-SRN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.JWO_SRN, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "MCC-AISSUE") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "MCC-ARETURN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmVSPAssetIssue, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))

                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "MCC-IISSUE") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmVSPItemIssue, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "MilkTransferInReturn") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkTransferInReturn, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "MilkTransferJobWork") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkJobWorkTransfer, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "MilkTransJWOReturn") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkJobWorkTransferReturn, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "MJ-SR") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmJobMilkSRN, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "M-PURRETURN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmMilkPurchaseReturn, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "MS-SR") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ScrapSaleRetrun, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "MT_SALE_IN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSalesInvoiceMT, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "PP_STDN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProcessProductionStandardization, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "PROD_WR") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "Prod-Scrap") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmWreckageBooking, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))

                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "Sale Return") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSaleReturnProductSale, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "SaleReturnBS") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmBulkSaleReturn, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "SD-CSATRANS") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSATransfer, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "SD-CSATRANS-RETURN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSATransferReturn, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "TRN-RET") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.TransferReturn, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "SI-MT") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSiloMilkTransfer, clsCommon.myCstr(Gv1.CurrentRow.Cells("Source Doc No").Value))
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub LoadData(ByVal strDocumentNo As String)
        Try
            Dim qry As String = ""
            Dim dt As New DataTable
            'Ticket No- BHA/04/09/18-000506 Remove column standard cost
            'Ticket no- BHA/12/09/18-000541 remove cost columns, show out transaction Qty and amount in negative 
            'Ticket No- GKD/03/10/18-000168 If Basic cost is zero in inventory movement table then (Avg_cost/Stock Qty) 
            'Ticket No- GKD/11/10/18-000170 1. Basic cost -> (Avg_Cost/Qty) if basic_cost=0. 2. Add new column Stock Cost (Avg_Cost/Stock Qty).
            'Ticket No : TEC/22/07/19-000949 By prabhakar add column Item Master Cost , FAT , SNF and Diff
            If clsCommon.CompairString(ChkInventory.IsChecked, "True") = CompairStringResult.Equal Then
                qry = "Select xx.* , TBL_ITEM_UOM_DETAIL.Item_Cost as [Item Master Cost],abs(XX.[Stock Cost]) - TBL_ITEM_UOM_DETAIL.Item_Cost as [Diff (Stock Cost - Item Master Cost)], TBL_ITEM_QC_PARAMETER_MASTER_FAT.StandardRate as [Item Master FAT RATE], convert(decimal(18,2),[Fat Rate]) - convert (decimal(18,2),TBL_ITEM_QC_PARAMETER_MASTER_FAT.StandardRate) as [Diff (Fat Rate - Item Master Fat Rate)],TBL_ITEM_QC_PARAMETER_MASTER_SNF.StandardRate as  [Item Master SNF RATE],convert(decimal(18,2),xx.[SNF Rate]) - convert (decimal(18,2),TBL_ITEM_QC_PARAMETER_MASTER_SNF.StandardRate) as [Diff (SNF Rate - Item Master SNF Rate)] from (  "
                qry += " select Trans_Id,Trans_Type as [Trans Type],TSPL_INVENTORY_SOURCE_CODE.Name as [Trans Name],InOut,Location_Code as [Location Code],Item_Code as [Item Code],Item_Desc as [Item Desc],(case when InOut='O' THEN (-1 * Qty) ELSE Qty end) AS Qty,UOM"
                qry += " ,Source_Doc_No as [Source Doc No],convert(date,Source_Doc_Date,103) as [Source Doc Date],(case when InOut='O' THEN (-1 * Avg_Cost) ELSE Avg_Cost end) AS [Avg Cost]"
                qry += " ,cast(case when Basic_Cost=0 then ((case when InOut='O' THEN (-1 * Avg_Cost) ELSE Avg_Cost end)/ (case when Qty=0 then 1 else Qty end) ) else "
                qry += " (case when InOut='O' THEN (-1 * Basic_Cost) ELSE Basic_Cost end) end as  decimal(16,2)) as [Basic Cost] "
                qry += " ,cast((case when InOut='O' THEN (-1 * Avg_Cost) ELSE Avg_Cost end)/ (case when Stock_Qty=0 then 1 else Stock_Qty end) as  decimal(16,2)) as [Stock Cost] "
                qry += " ,MRP,Batch_No"
                qry += " ,Stock_UOM as [Stock UOM],(case when InOut='O' THEN (-1 * Stock_Qty) ELSE Stock_Qty end) as [Stock Qty],Item_Status as [Item Status],Assmbly_Status as [Assmbly Status],IS_CONSUMPTION,Cust_Code as [Customer Code]"
                qry += " ,Cust_Name as [Customer Name],Vendor_Code as [Vendor Code],Vendor_Name as [Vendor Name],Other_Location_Code as [Other Location Code],Other_Location_Desc as [Other Location Desc]"
                qry += " ,Fat_Per as [Fat %],SNF_Per as [SNF %],(case when InOut='O' THEN (-1 * Fat_KG) ELSE Fat_KG end) as[Fat KG],(case when InOut='O' THEN (-1 * SNF_KG) ELSE SNF_KG end) as [SNF KG],convert(decimal(18,2),Fat_Rate) as [Fat Rate],convert(decimal(18,2),SNF_Rate) as [SNF Rate],(case when InOut='O' THEN (-1 * Fat_Amt) ELSE Fat_Amt end) as [Fat Amt],(case when InOut='O' THEN (-1 * SNF_Amt) ELSE SNF_Amt end) as [SNF Amt]"
                qry += " ,tspl_inventory_movement.Created_By as [Created By],convert(date,Punching_Date,103) as [Punching Date] "
                qry += " ,isnull(Inventory_DrAcc,'') as Inventory_DrAcc,isnull(Inventory_CrAcc,'') as Inventory_CrAcc from tspl_inventory_movement "
                qry += "  left outer join TSPL_INVENTORY_SOURCE_CODE on TSPL_INVENTORY_SOURCE_CODE.code=tspl_inventory_movement.Trans_Type "
                qry += " where 2=2 "
                qry += " and convert(date,tspl_inventory_movement.Punching_Date ,103)>=convert(date,'" + fromDate.Value + "',103) AND convert(date,tspl_inventory_movement.Punching_Date,103)<=convert(date,'" + ToDate.Value + "',103) "
                If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                    qry += " and Item_Code in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ")"
                End If
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    qry += " and Location_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
                End If
                If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
                    qry += " and Trans_Type in (" + clsCommon.GetMulcallString(txtTransaction.arrValueMember) + ")"
                End If
                qry += " )xx " & _
                        " left outer join (Select TSPL_ITEM_UOM_DETAIL.ITem_Code,isnull (TSPL_ITEM_UOM_DETAIL.Item_Cost,0) as Item_Cost from TSPL_ITEM_UOM_DETAIL where Stocking_Unit = 'Y' ) as TBL_ITEM_UOM_DETAIL on TBL_ITEM_UOM_DETAIL.Item_Code = XX.[Item Code] " & _
                        " left outer join (select Item_Code , isnull (StandardRate ,0) as StandardRate  from TSPL_ITEM_QC_PARAMETER_MASTER where Code = 'FAT') as TBL_ITEM_QC_PARAMETER_MASTER_FAT on TBL_ITEM_QC_PARAMETER_MASTER_FAT.Item_Code = XX.[Item Code] " & _
                        " left outer join (select Item_Code , isnull (StandardRate,0)  as StandardRate from TSPL_ITEM_QC_PARAMETER_MASTER where Code = 'SNF') as TBL_ITEM_QC_PARAMETER_MASTER_SNF on TBL_ITEM_QC_PARAMETER_MASTER_SNF.Item_Code = XX.[Item Code] "

            ElseIf clsCommon.CompairString(ChkInvenoryNew.IsChecked, "True") = CompairStringResult.Equal Then
                qry = "Select xx.* , TBL_ITEM_UOM_DETAIL.Item_Cost as [Item Master Cost],abs(XX.[Stock Cost]) - TBL_ITEM_UOM_DETAIL.Item_Cost as [Diff (Stock Cost - Item Master Cost)], TBL_ITEM_QC_PARAMETER_MASTER_FAT.StandardRate as [Item Master FAT RATE], convert(decimal(18,2),[Fat Rate]) - convert (decimal(18,2),TBL_ITEM_QC_PARAMETER_MASTER_FAT.StandardRate) as [Diff (Fat Rate - Item Master Fat Rate)],TBL_ITEM_QC_PARAMETER_MASTER_SNF.StandardRate as  [Item Master SNF RATE],convert(decimal(18,2),xx.[SNF Rate]) - convert (decimal(18,2),TBL_ITEM_QC_PARAMETER_MASTER_SNF.StandardRate) as [Diff (SNF Rate - Item Master SNF Rate)] from (  "
                qry += " select Trans_Id,Trans_Type as [Trans Type],TSPL_INVENTORY_SOURCE_CODE.Name as [Trans Name],tspl_inventory_movement_new.InOut,Location_Code as [Location Code]"
                qry += " ,Item_Code as [Item Code],Item_Desc as [Item Desc],(case when InOut='O' THEN (-1 * Qty) ELSE Qty end) as Qty,UOM,Source_Doc_No as [Source Doc No],convert(date,Source_Doc_Date,103) as [Source Doc Date],Entry_Date as [Entry Date]"
                'qry += " ,(case when InOut='O' THEN (-1 * Basic_Cost) ELSE Basic_Cost end) as [Basic Cost]"
                'qry += " ,cast(case when Basic_Cost=0 then ((case when InOut='O' THEN (-1 * Avg_Cost) ELSE Avg_Cost end)/ (case when Stock_Qty=0 then 1 else Stock_Qty end) ) else "
                'qry += " (case when InOut='O' THEN (-1 * Basic_Cost) ELSE Basic_Cost end) end as  decimal(16,2)) as [Basic Cost] "

                qry += " ,cast(case when Basic_Cost=0 then ((case when InOut='O' THEN (-1 * Avg_Cost) ELSE Avg_Cost end)/ (case when Qty=0 then 1 else Qty end) ) else "
                qry += " (case when InOut='O' THEN (-1 * Basic_Cost) ELSE Basic_Cost end) end as  decimal(16,2)) as [Basic Cost] "
                qry += " ,cast((case when InOut='O' THEN (-1 * Avg_Cost) ELSE Avg_Cost end)/ (case when Stock_Qty=0 then 1 else Stock_Qty end) as  decimal(16,2)) as [Stock Cost] "

                qry += " ,tspl_inventory_movement_new.Created_By as [Created By],tspl_inventory_movement_new.Comp_Code as [Comp Code]"
                qry += " ,ItemType as [Item Type],convert(date,Punching_Date,103) as [Punching Date],MRP,Batch_No"
                qry += " ,(case when InOut='O' THEN (-1 * Avg_Cost) ELSE Avg_Cost end) as [Avg Cost],Posting_Date as [Posting Date],Stock_UOM as [Stock UOM],(case when InOut='O' THEN (-1 * Stock_Qty) ELSE Stock_Qty end) as [Stock Qty]"
                qry += " ,Item_Status as [Item Status],Assmbly_Status as [Assmbly Status],Fat_Per as [Fat %],SNF_Per as [SNF %],(case when InOut='O' THEN (-1 * Fat_KG) ELSE Fat_KG end) as [Fat KG],(case when InOut='O' THEN (-1 * SNF_KG) ELSE SNF_KG end) as [SNF KG],main_location as [Main Location],IS_CONSUMPTION,Cust_Code as [Customer Code],Cust_Name as [Customer Name],Vendor_Code as [Vendor Code],Vendor_Name as [Vendor Name],Other_Location_Code as [Other Location Code],Other_Location_Desc as [Other Location Desc],convert(decimal(18,2),Fat_Rate) as [Fat Rate]"
                qry += " ,convert(decimal(18,2),SNF_Rate) as [SNF Rate],(case when InOut='O' THEN (-1 * Fat_Amt) ELSE Fat_Amt end) as [Fat Amt],(case when InOut='O' THEN (-1 * SNF_Amt) ELSE SNF_Amt end) as [SNF Amt],(case when InOut='O' THEN (-1 * Std_Qty) ELSE Std_Qty end) as [Standard Qty],SYNC_STATUS"
                qry += " ,isnull(Inventory_DrAcc,'') as Inventory_DrAcc,isnull(Inventory_CrAcc,'') as Inventory_CrAcc from tspl_inventory_movement_new"
                qry += " left outer join TSPL_INVENTORY_SOURCE_CODE on TSPL_INVENTORY_SOURCE_CODE.code=TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type "
                qry += " where 2=2 "
                qry += " and convert(date,Punching_Date,103) >='" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "' AND convert(date,Punching_Date,103) <='" + clsCommon.GetPrintDate(ToDate.Value, "dd-MMM-yyyy") + "' "
                If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                    qry += " and Item_Code in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ")"
                End If
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    qry += " and Location_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
                End If
                If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
                    qry += " and Trans_Type in (" + clsCommon.GetMulcallString(txtTransaction.arrValueMember) + ")"
                End If
                qry += " )xx " & _
                        " left outer join (Select TSPL_ITEM_UOM_DETAIL.ITem_Code,isnull (TSPL_ITEM_UOM_DETAIL.Item_Cost,0) as Item_Cost from TSPL_ITEM_UOM_DETAIL where Stocking_Unit = 'Y' ) as TBL_ITEM_UOM_DETAIL on TBL_ITEM_UOM_DETAIL.Item_Code = XX.[Item Code] " & _
                        " left outer join (select Item_Code , isnull (StandardRate ,0) as StandardRate  from TSPL_ITEM_QC_PARAMETER_MASTER where Code = 'FAT') as TBL_ITEM_QC_PARAMETER_MASTER_FAT on TBL_ITEM_QC_PARAMETER_MASTER_FAT.Item_Code = XX.[Item Code] " & _
                        " left outer join (select Item_Code , isnull (StandardRate,0)  as StandardRate from TSPL_ITEM_QC_PARAMETER_MASTER where Code = 'SNF') as TBL_ITEM_QC_PARAMETER_MASTER_SNF on TBL_ITEM_QC_PARAMETER_MASTER_SNF.Item_Code = XX.[Item Code] "
            Else
                qry = "select xx.*, TBL_ITEM_UOM_DETAIL.Item_Cost as [Item Master Cost],abs(XX.[Stock Cost]) - TBL_ITEM_UOM_DETAIL.Item_Cost as [Diff (Stock Cost - Item Master Cost)], TBL_ITEM_QC_PARAMETER_MASTER_FAT.StandardRate as [Item Master FAT RATE], convert(decimal(18,2),[Fat Rate]) - convert (decimal(18,2),TBL_ITEM_QC_PARAMETER_MASTER_FAT.StandardRate) as [Diff (Fat Rate - Item Master Fat Rate)],TBL_ITEM_QC_PARAMETER_MASTER_SNF.StandardRate as  [Item Master SNF RATE],convert(decimal(18,2),xx.[SNF Rate]) - convert (decimal(18,2),TBL_ITEM_QC_PARAMETER_MASTER_SNF.StandardRate) as [Diff (SNF Rate - Item Master SNF Rate)]  from ("

                qry += " select Trans_Id,Trans_Type as [Trans Type],TSPL_INVENTORY_SOURCE_CODE.Name as [Trans Name],InOut,Location_Code as [Location Code],Item_Code as [Item Code],Item_Desc as [Item Desc],(case when InOut='O' THEN (-1 * Qty) ELSE Qty end) AS Qty,UOM ,Source_Doc_No as [Source Doc No],convert(date,Source_Doc_Date,103) as [Source Doc Date],Entry_Date as [Entry Date] ,cast(case when Basic_Cost=0 then ((case when InOut='O' THEN (-1 * Avg_Cost) ELSE Avg_Cost end)/ (case when Qty=0 then 1 else Qty end) ) else  (case when InOut='O' THEN (-1 * Basic_Cost) ELSE Basic_Cost end) end as  decimal(16,2)) as [Basic Cost]  ,cast((case when InOut='O' THEN (-1 * Avg_Cost) ELSE Avg_Cost end)/ (case when Stock_Qty=0 then 1 else Stock_Qty end) as  decimal(16,2)) as [Stock Cost] ,tspl_inventory_movement.Created_By as [Created By],tspl_inventory_movement.Comp_Code as [Comp Code],ItemType as [Item Type] " & _
                    " ,convert(date,Punching_Date,103) as [Punching Date],MRP,Batch_No " & _
                    " ,(case when InOut='O' THEN (-1 * Avg_Cost) ELSE Avg_Cost end) AS [Avg Cost],Posting_Date as [Posting Date] " & _
                    " ,Stock_UOM as [Stock UOM],(case when InOut='O' THEN (-1 * Stock_Qty) ELSE Stock_Qty end) as [Stock Qty],Item_Status as [Item Status],Assmbly_Status as [Assmbly Status],Fat_Per as [Fat %],SNF_Per as [SNF %],(case when InOut='O' THEN (-1 * Fat_KG) ELSE Fat_KG end) as[Fat KG] " & _
                    " ,(case when InOut='O' THEN (-1 * SNF_KG) ELSE SNF_KG end) as [SNF KG] ,'' as [Main Location] " & _
                    " ,IS_CONSUMPTION,Cust_Code as [Customer Code] ,Cust_Name as [Customer Name],Vendor_Code as [Vendor Code],Vendor_Name as [Vendor Name],Other_Location_Code as [Other Location Code],Other_Location_Desc as [Other Location Desc],convert(decimal(18,2),Fat_Rate) as [Fat Rate],convert(decimal(18,2),SNF_Rate) as [SNF Rate],(case when InOut='O' THEN (-1 * Fat_Amt) ELSE Fat_Amt end) as [Fat Amt],(case when InOut='O' THEN (-1 * SNF_Amt) ELSE SNF_Amt end)  " & _
                    " as [SNF Amt] ,0 as [Standard Qty],0 as SYNC_STATUS  " & _
                    " ,isnull(Inventory_DrAcc,'') as Inventory_DrAcc,isnull(Inventory_CrAcc,'') as Inventory_CrAcc from tspl_inventory_movement   left outer join TSPL_INVENTORY_SOURCE_CODE on TSPL_INVENTORY_SOURCE_CODE.code=tspl_inventory_movement.Trans_Type  where 2=2 "
                qry += " and convert(date,Punching_Date,103) >='" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "' AND convert(date,Punching_Date,103) <='" + clsCommon.GetPrintDate(ToDate.Value, "dd-MMM-yyyy") + "' "
                If clsCommon.myLen(strDocumentNo) > 0 Then
                    qry += " and Source_Doc_No in ('" + strDocumentNo + "')"
                End If

                If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                    qry += " and Item_Code in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ")"
                End If
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    qry += " and Location_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
                End If
                If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
                    qry += " and Trans_Type in (" + clsCommon.GetMulcallString(txtTransaction.arrValueMember) + ")"
                End If
                qry += " union all "
                qry += " select Trans_Id,Trans_Type as [Trans Type],TSPL_INVENTORY_SOURCE_CODE.Name as [Trans Name],tspl_inventory_movement_new.InOut,Location_Code as [Location Code] ,Item_Code as [Item Code],Item_Desc as [Item Desc],(case when InOut='O' THEN (-1 * Qty) ELSE Qty end) as Qty,UOM,Source_Doc_No as [Source Doc No],convert(date,Source_Doc_Date,103) as [Source Doc Date],Entry_Date as [Entry Date] ,cast(case when Basic_Cost=0 then ((case when InOut='O' THEN (-1 * Avg_Cost) ELSE Avg_Cost end)/ (case when Qty=0 then 1 else Qty end) ) else  (case when InOut='O' THEN (-1 * Basic_Cost) ELSE Basic_Cost end) end as  decimal(16,2)) as [Basic Cost]  ,cast((case when InOut='O' THEN (-1 * Avg_Cost) ELSE Avg_Cost end)/ (case when Stock_Qty=0 then 1 else Stock_Qty end) as  decimal(16,2)) as [Stock Cost]  ,tspl_inventory_movement_new.Created_By as [Created By],tspl_inventory_movement_new.Comp_Code as [Comp Code] ,ItemType as [Item Type],convert(date,Punching_Date,103) as [Punching Date],MRP,Batch_No,(case when InOut='O' THEN (-1 * Avg_Cost) ELSE Avg_Cost end) as [Avg Cost] " & _
                  ",Posting_Date as [Posting Date],Stock_UOM as [Stock UOM],(case when InOut='O' THEN (-1 * Stock_Qty) ELSE Stock_Qty end) as [Stock Qty] ,Item_Status as [Item Status],Assmbly_Status as [Assmbly Status],Fat_Per as [Fat %],SNF_Per as [SNF %],(case when InOut='O' THEN (-1 * Fat_KG) ELSE Fat_KG end) as [Fat KG],(case when InOut='O' THEN (-1 * SNF_KG) ELSE SNF_KG end) as [SNF KG],main_location as [Main Location],IS_CONSUMPTION,Cust_Code as [Customer Code],Cust_Name as [Customer Name],Vendor_Code as [Vendor Code],Vendor_Name as [Vendor Name],Other_Location_Code as [Other Location Code],Other_Location_Desc as [Other Location Desc],convert(decimal(18,2),Fat_Rate) as [Fat Rate] ,convert(decimal(18,2),SNF_Rate) as [SNF Rate],(case when InOut='O' THEN (-1 * Fat_Amt) ELSE Fat_Amt end) as [Fat Amt],(case when InOut='O' THEN (-1 * SNF_Amt) ELSE SNF_Amt end) as [SNF Amt],(case when InOut='O' THEN (-1 * Std_Qty) ELSE Std_Qty end) as [Standard Qty],SYNC_STATUS,isnull(Inventory_DrAcc,'') as Inventory_DrAcc,isnull(Inventory_CrAcc,'') as Inventory_CrAcc from tspl_inventory_movement_new left outer join TSPL_INVENTORY_SOURCE_CODE on TSPL_INVENTORY_SOURCE_CODE.code=TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type  where 2=2 "

                qry += " and convert(date,Punching_Date,103) >='" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "' AND convert(date,Punching_Date,103) <='" + clsCommon.GetPrintDate(ToDate.Value, "dd-MMM-yyyy") + "' "
                If clsCommon.myLen(strDocumentNo) > 0 Then
                    qry += " and Source_Doc_No in ('" + strDocumentNo + "')"
                End If

                If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                    qry += " and Item_Code in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ")"
                End If
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    qry += " and Location_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
                End If
                If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
                    qry += " and Trans_Type in (" + clsCommon.GetMulcallString(txtTransaction.arrValueMember) + ")"
                End If

                qry += " )xx " & _
                       " left outer join (Select TSPL_ITEM_UOM_DETAIL.ITem_Code,isnull (TSPL_ITEM_UOM_DETAIL.Item_Cost,0) as Item_Cost from TSPL_ITEM_UOM_DETAIL where Stocking_Unit = 'Y' ) as TBL_ITEM_UOM_DETAIL on TBL_ITEM_UOM_DETAIL.Item_Code = XX.[Item Code] " & _
                       " left outer join (select Item_Code , isnull (StandardRate ,0) as StandardRate  from TSPL_ITEM_QC_PARAMETER_MASTER where Code = 'FAT') as TBL_ITEM_QC_PARAMETER_MASTER_FAT on TBL_ITEM_QC_PARAMETER_MASTER_FAT.Item_Code = XX.[Item Code] " & _
                       " left outer join (select Item_Code , isnull (StandardRate,0)  as StandardRate from TSPL_ITEM_QC_PARAMETER_MASTER where Code = 'SNF') as TBL_ITEM_QC_PARAMETER_MASTER_SNF on TBL_ITEM_QC_PARAMETER_MASTER_SNF.Item_Code = XX.[Item Code] "
            End If

            qry += " order by [Source Doc Date]"
            dt = clsDBFuncationality.GetDataTable(qry)
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()


            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim itemQty As New GridViewSummaryItem("Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(itemQty)
            Dim StockQty As New GridViewSummaryItem("Stock Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(StockQty)
            Dim Avg_Cost As New GridViewSummaryItem("Avg Cost", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Avg_Cost)
            Dim Basic_Cost As New GridViewSummaryItem("Basic Cost", "{0:F2}", GridAggregateFunction.Avg)
            summaryRowItem.Add(Basic_Cost)
            Dim Stock_Cost As New GridViewSummaryItem("Stock Cost", "{0:F2}", GridAggregateFunction.Avg)
            summaryRowItem.Add(Stock_Cost)
            Dim FatAmount As New GridViewSummaryItem("Fat Amt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(FatAmount)
            Dim SNFAmount As New GridViewSummaryItem("SNF Amt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SNFAmount)
            Dim StdQty As New GridViewSummaryItem("Standard Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(StdQty)
            Dim strFATKG As New GridViewSummaryItem("Fat KG", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(strFATKG)
            Dim strSNFKG As New GridViewSummaryItem("SNF KG", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(strSNFKG)
            Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

            Gv1.MasterView.Refresh()

            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                Gv1.DataSource = dt
                For ii As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(ii).ReadOnly = True
                Next
                Gv1.Columns("Trans Type").IsVisible = False
                Gv1.Columns("Trans_Id").IsVisible = False
                Gv1.Columns("Batch_No").HeaderText = "Batch Order No"
                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.BestFitColumns()
                Gv1.EnableFiltering = True
            Else
                clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            End If
            PageSetupReport_ID = Getreport_id()
            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub


    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Export(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Export(EnumExportTo.PDF)
    End Sub
End Class
