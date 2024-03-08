Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI
'Report Created by sanjay , Ticket No-  TEC/14/02/19-000425 
'TEC/15/05/19-000480 add Document wise option
Public Class RptInventoryRecoReport
    Inherits FrmMainTranScreen
#Region "Varibales"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim strQry As String = ""
#End Region

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub
    Private Sub RptInventoryRecoReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        'ToDate.Value = clsCommon.GETSERVERDATE()
        'fromDate.Value = ToDate.Value.AddMonths(-1)
        'MyLabel4.Visible = False
        'lblDocumentNo.Visible = False
        Reset()
        LoadLocation()
        If clsCommon.myLen(Me.Tag) > 0 Then
            lblDocumentNo.Text = clsCommon.myCstr(Me.Tag)
            Dim docDate As Date? = clsCommon.myCDate(clsDBFuncationality.getSingleValue(" select Top 1  XXX.Punching_Date  from ( select Punching_Date from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No = '" + lblDocumentNo.Text + "' union All select Punching_Date from TSPL_INVENTORY_MOVEMENT where Source_Doc_No = '" + lblDocumentNo.Text + "' ) XXX "))
            ToDate.Value = docDate
            fromDate.Value = docDate
            chkSummary.IsChecked = True
            MyLabel4.Visible = True
            lblDocumentNo.Visible = True
            ChkDetail.Enabled = False
            'LoadData(lblDocumentNo.Text)
        End If
    End Sub
    Sub Reset()
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
        txtItem.arrValueMember = Nothing
        txtTransaction.arrValueMember = Nothing
        txtAccount.arrValueMember = Nothing
        txtSourceCode.arrValueMember = Nothing
        lblDocumentNo.Text = ""
        MyLabel4.Visible = False
        lblDocumentNo.Visible = False
        chkSummary.IsChecked = True
        ChkDetail.Enabled = True
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Function Getreport_id()
        Dim REPORTID As String = ""
        If chkSummary.IsChecked = True Then
            REPORTID = MyBase.Form_ID + "S"
        ElseIf ChkDetail.IsChecked = True Then
            REPORTID = MyBase.Form_ID + "D"
        ElseIf chkDocWise.IsChecked = True Then
            REPORTID = MyBase.Form_ID + "DOC"
        End If
        Return REPORTID
    End Function
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = Getreport_id()
        TemplateGridview = Gv1
        LoadData()
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

    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        Try
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
                    common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
                End If

                ''richa agarwal regarding memory leakage
                obj.GridLayout.Close()
                obj.GridLayout.Dispose()
                ''---------------
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        Try
            clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
            common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptInvReco & "'"))
                arrHeader.Add("Date Range : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy"))
                Dim arrLocation As ArrayList = GetLocation()
                If arrLocation IsNot Nothing AndAlso arrLocation.Count > 0 Then
                    arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(arrLocation))
                End If
                If txtItem.arrDispalyMember IsNot Nothing AndAlso txtItem.arrDispalyMember.Count > 0 Then
                    arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember))
                End If

                If txtTransaction.arrDispalyMember IsNot Nothing AndAlso txtTransaction.arrDispalyMember.Count > 0 Then
                    arrHeader.Add("Transaction : " + clsCommon.GetMulcallStringWithComma(txtTransaction.arrDispalyMember))
                End If
                If exporter = EnumExportTo.Excel Then
                    transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
                Else
                    transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("Inventory Reco Report", Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub Gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        Try

            If e.Column Is Gv1.Columns("Item Code") Then 'OrElse e.Column Is Gv1.Columns("Item Desc")
                Dim itemcode As String = ""
                itemcode = clsCommon.myCstr(Gv1.CurrentRow.Cells("Item Code").Value)
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmItemMasterRMOther, itemcode)
            ElseIf e.Column Is Gv1.Columns("Inv Source Doc No") Then
                DrillDown()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub DrillDown()
        Try
            If Gv1.CurrentRow.Index >= 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "BulkSRN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmBulkMilkSRN, clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "CRATE-REC") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCrateReceviedDairySale, clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "Disassembly") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmAssembDis, clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "DispChallan") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCDispatch, clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "FS-SH") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "PS-SH") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "MCC-MSALE") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmShipmentProductSale, clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "FS-SR") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "PS-SR") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSaleReturndairy, clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "IC-AD") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnStoreAdjustment, clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "ISSTRAN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnIssueReturn, clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "MCC-MSRN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkSRN, clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "MilkTransferIn") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkTransferIn, clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "NRGP") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "RGP") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnGatePass, clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "PP_ISSUE") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProcessProductionIssueEntry, clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "PP_STD-FQC") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ProcessProductionStandardizationFinalQC, clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "PROD_ENTRY") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProductionEntry, clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "SRN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnSRN, clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "SRN-RET") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.SRNReturn, clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "Transfer") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.Transfer, clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "Purchase Return") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseReturn, clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "PRD_STG_PROC") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProcessProductionStageProcess, clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "MCC-MSR") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCMaterialSaleReturn, clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "BulkSRNRet") = CompairStringResult.Equal Then
                    'No separate screen for display record
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "DisCanSale") = CompairStringResult.Equal Then
                    Dim strDocNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select CanSale_Doc_No from TSPL_CANSALE_DISPATCH_HEAD where Document_No='" + clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value) + "'"))
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmCanSale, clsCommon.myCstr(strDocNo))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "DispatchBS") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmDispatchBulkSale, clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "ScrapIn") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ScrapSale, clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value))

                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "BulkSRNTrade") = CompairStringResult.Equal Then
                    'Class clsBulkMilkSRNTrade
                    'No separate screen for display record
                    'Dim strDocNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ISNULL(tspl_bulk_milk_srn.Challan_No,'') AS Challan_No From tspl_bulk_milk_srn where tspl_bulk_milk_srn.srn_no='" + clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value) + "'"))
                    'clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmDispatchBulkSaleTrade, strDocNo)
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "CSA-SALE") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSASaleInvoice, clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "CSA-SALEPATTI-RETURN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSASalePattiReturn, clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "DispatchBSTrade") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm("", clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value))
                    ''''''''''''''''''''''''''''''''''''
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "DispChallanRet") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm("", clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "DispChallan-RET") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm("", clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "EX_SALE_IN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmEXSalesInvoice, clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "JWO-SRN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.JWO_SRN, clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "MCC-AISSUE") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "MCC-ARETURN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmVSPAssetIssue, clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value))

                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "MCC-IISSUE") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm("", clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "MilkTransferInReturn") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkTransferInReturn, clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "MilkTransferJobWork") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkJobWorkTransfer, clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "MilkTransJWOReturn") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkJobWorkTransferReturn, clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "MJ-SR") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmJobMilkSRN, clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value))
                    'above screen does not Use by any client
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "M-PURRETURN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmMilkPurchaseReturn, clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "MS-SR") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ScrapSaleRetrun, clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "MT_SALE_IN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSalesInvoiceMT, clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value))
                    'Merchant Trade - Merchant Sale Invoice,above screen does not Use by any client
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "PP_STDN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProcessProductionStandardization, clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "PROD_WR") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "Prod-Scrap") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmWreckageBooking, clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value))

                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "Sale Return") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSaleReturnProductSale, clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "SaleReturnBS") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmBulkSaleReturn, clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "SD-CSATRANS") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSATransfer, clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "SD-CSATRANS-RETURN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSATransferReturn, clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "TRN-RET") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.TransferReturn, clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value), "SI-MT") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSiloMilkTransfer, clsCommon.myCstr(Gv1.CurrentRow.Cells("Inv Source Doc No").Value))
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub LoadData()
        Try
            Dim qry As String = ""
            Dim dt As New DataTable
            Dim arrLocation As ArrayList = GetLocation()
            If chkDocWise.IsChecked Then
                qry = " WITH CTEINV(Source_Doc_No,Trans_Type,JE_SOURCE_DOC_NO " &
               " ,[Inv Control Ac],[Inv Ac Description],[Inv Dr],[Inv Cr])  as ( select INVSUM.Source_Doc_No,INVSUM.Trans_Type,INVSUM.JE_SOURCE_DOC_NO " &
               " ,max(INVSUM.[Inv Control Ac]) as [Inv Control Ac],max(INVSUM.[Inv Ac Description] ) as [Inv Ac Description], sum(INVSUM.[Inv Dr]) as [Inv Dr] ,sum(INVSUM.[Inv Cr]) as [Inv Cr] from( select  " &
               " INV.Source_Doc_No  as Source_Doc_No,coalesce(TSPL_Customer_Invoice_Head.Document_No ,TSPL_VENDOR_INVOICE_HEAD.Document_No) as JE_SOURCE_DOC_NO  " &
               " ,INV.Trans_Type,INV.[Inv Control Ac] ,TSPL_GL_ACCOUNTS.Description+','+TSPL_LOCATION_MASTER.Location_Desc as [Inv Ac Description] ,INV.[Inv Dr] ,INV.[Inv Cr] from (   " &
               " select Source_Doc_Date,Trans_Id,tspl_inventory_movement.Item_Code, Source_Doc_No,Trans_Type,(case when InOut='O' THEN isnull(Inventory_CrAcc,'') " &
               " ELSE isnull(Inventory_DrAcc,'') end) as [Inv Control Ac]  ,(case when InOut='I' THEN Avg_Cost ELSE 0 end) as [Inv Dr],(case when InOut='O' THEN Avg_Cost ELSE 0 end) as [Inv Cr] " &
               " from tspl_inventory_movement   where ((Inventory_CrAcc is not null and Inventory_CrAcc<>'') or (Inventory_DrAcc is not null and Inventory_DrAcc<>''))  "

                qry += " and convert(date,Punching_Date,103) >='" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "' AND convert(date,Punching_Date,103) <='" + clsCommon.GetPrintDate(ToDate.Value, "dd-MMM-yyyy") + "' "
                If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                    qry += " and Item_Code in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ")"
                End If
                If arrLocation IsNot Nothing AndAlso arrLocation.Count > 0 Then
                    qry += " and Location_Code in (" + clsCommon.GetMulcallString(arrLocation) + ")"
                End If
                If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
                    qry += " and Trans_Type in (" + clsCommon.GetMulcallString(txtTransaction.arrValueMember) + ")"
                End If
                If chkExcludeConsumptionLoc.Checked Then
                    qry += " And ( location_code not in (Select Location_code from tspl_location_master where TSPL_LOCATION_MASTER.Is_Consumption_Location =1) or Trans_Type Not  in ('PP_ISSUE','PP_STD-FQC','PRD_STG_PROC','PROD_ENTRY'))" + Environment.NewLine
                End If
                qry += " union all  select Source_Doc_Date,Trans_Id,tspl_inventory_movement_new.Item_Code,  Source_Doc_No,Trans_Type,(case when InOut='O' THEN isnull(Inventory_CrAcc,'') ELSE  " &
                " isnull(Inventory_DrAcc,'') end) as [Inv Control Ac] ,(case when InOut='I' THEN Avg_Cost ELSE 0 end) as [Inv Dr],(case when InOut='O' THEN Avg_Cost ELSE 0 end) as [Inv Cr]  " &
                " from tspl_inventory_movement_new    where ((Inventory_CrAcc is not null and Inventory_CrAcc<>'') or (Inventory_DrAcc is not null and Inventory_DrAcc<>''))  "

                qry += " and convert(date,Punching_Date,103) >='" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "' AND convert(date,Punching_Date,103) <='" + clsCommon.GetPrintDate(ToDate.Value, "dd-MMM-yyyy") + "' "
                If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                    qry += " and Item_Code in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ")"
                End If
                If arrLocation IsNot Nothing AndAlso arrLocation.Count > 0 Then
                    qry += " and case when len(isnull(main_location,''))>0 then main_location else  Location_Code end in (" + clsCommon.GetMulcallString(arrLocation) + ")"
                End If
                If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
                    qry += " and Trans_Type in (" + clsCommon.GetMulcallString(txtTransaction.arrValueMember) + ")"
                End If
                If chkExcludeConsumptionLoc.Checked Then
                    qry += " And ( location_code not in (Select Location_code from tspl_location_master where TSPL_LOCATION_MASTER.Is_Consumption_Location =1) or Trans_Type Not  in ('PP_ISSUE','PP_STD-FQC','PRD_STG_PROC','PROD_ENTRY'))" + Environment.NewLine
                End If
                qry += " )INV  left join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=INV.[Inv Control Ac] left join tspl_location_master  " &
                " on tspl_location_master.location_code=TSPL_GL_ACCOUNTS.Account_Seg_Code7 left join TSPL_INVENTORY_SOURCE_CODE on TSPL_INVENTORY_SOURCE_CODE.code=INV.Trans_Type  " &
                "  left join TSPL_VENDOR_INVOICE_HEAD on  case when TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No is not null then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No   " &
                "   when TSPL_VENDOR_INVOICE_HEAD.RefDocType='BM-PR' then TSPL_VENDOR_INVOICE_HEAD.RefDocNo  else TSPL_VENDOR_INVOICE_HEAD.Document_No end=INV.Source_Doc_No   " &
                "   left join TSPL_Customer_Invoice_Head on  case when isnull(TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'' )<>''  " &
                "   then TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return  when isnull(TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'' )<>''  " &
                "   then TSPL_Customer_Invoice_Head.Against_Sale_Return_No when isnull(TSPL_Customer_Invoice_Head.AgainstScrapReturn ,'')<>'' then TSPL_Customer_Invoice_Head.AgainstScrapReturn " &
                "   when isnull(TSPL_Customer_Invoice_Head.AgainstScrap ,'')<>'' then TSPL_Customer_Invoice_Head.AgainstScrap else TSPL_Customer_Invoice_Head.Document_No end=INV.Source_Doc_No " &
                "   )INVSUM where source_doc_no  not in (select adjustment_no from tspl_adjustment_header where  AdjustType='Adj' and isnull(Against_PI_No_Difference,'')<>'') group by INVSUM.Source_Doc_No,INVSUM.Trans_Type,INVSUM.JE_SOURCE_DOC_NO ),  " &
                " CTEJV(Source_Doc_No,[Gl Account] " &
                " ,[Gl Dr],[Gl Cr])  as   (select   TSPL_JOURNAL_MASTER.Source_Doc_No,max(TSPL_JOURNAL_DETAILS.Account_code) as [Gl Account] " &
                " ,sum(case when TSPL_JOURNAL_DETAILS.Amount>0 THEN TSPL_JOURNAL_DETAILS.Amount ELSE 0 end) as [Gl Dr]  ,sum(case when TSPL_JOURNAL_DETAILS.Amount<0  " &
                " THEN (-1 *TSPL_JOURNAL_DETAILS.Amount)  " &
                " ELSE 0 end) as [Gl Cr]  from TSPL_JOURNAL_MASTER   left join TSPL_JOURNAL_DETAILS on TSPL_JOURNAL_DETAILS.Voucher_No=TSPL_JOURNAL_MASTER.Voucher_No " &
                " where Reco_Control_Account='I' group by TSPL_JOURNAL_MASTER.Source_Doc_No ) " &
                " ,CTEFINAL(Source_Doc_No,Trans_Type,[Inv Control Ac],[Inv Ac Description],[Inv Dr],[Inv Cr],[Gl Dr],[Gl Cr]  )  as   " &
                " (SELECT CTEINV.Source_Doc_No,CTEINV.Trans_Type,CTEINV.[Inv Control Ac],CTEINV.[Inv Ac Description],CTEINV.[Inv Dr],CTEINV.[Inv Cr],CTEJV.[Gl Dr],CTEJV.[Gl Cr]   " &
                " FROM CTEINV  LEFT JOIN CTEJV ON   " &
                "  coalesce(JE_SOURCE_DOC_NO,CTEINV.Source_Doc_No,'')=CTEJV.Source_Doc_No   " &
                " where 2=2   ) " &
                "select CTEFINAL.Source_Doc_No as [Inv Source Doc No],Trans_Type as [Trans Type],max(isnull(RefDocNo,'')) as RefDocNo,CTEFINAL.Voucher_No ,convert(varchar,CTEFINAL.voucher_date,103) as voucher_date,isnull(sum(CTEFINAL.[Inv Dr]),0) as [Inv Dr],isnull(sum(CTEFINAL.[Inv Cr]),0) as [Inv Cr]  ,isnull(sum(CTEFINAL.[Gl Dr]),0) as [Gl Dr],isnull(sum(CTEFINAL.[Gl Cr]),0) as [Gl Cr]  ,isnull(sum(CTEFINAL.[Gl Dr]),0)-isnull(sum(CTEFINAL.[Inv Dr]),0) as [Diff Dr],isnull(sum(CTEFINAL.[Gl Cr]),0)-isnull(sum(CTEFINAL.[Inv Cr]),0) as [Diff Cr] from
(
select CTEFINAL.Source_Doc_No as Source_Doc_No,Trans_Type ,Null as RefDocNo,TSPL_JOURNAL_MASTER.Voucher_No ,convert(varchar,TSPL_JOURNAL_MASTER.voucher_date,103) as voucher_date,isnull(CTEFINAL.[Inv Dr],0) as [Inv Dr],isnull(CTEFINAL.[Inv Cr],0) as [Inv Cr]  ,isnull(CTEFINAL.[Gl Dr],0) as [Gl Dr],isnull(CTEFINAL.[Gl Cr],0) as [Gl Cr]  ,isnull(CTEFINAL.[Gl Dr],0)-isnull(CTEFINAL.[Inv Dr],0) as [Diff Dr],isnull(CTEFINAL.[Gl Cr],0)-isnull(CTEFINAL.[Inv Cr],0) as [Diff Cr] from CTEFINAL
left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER .Source_Doc_No =CTEFINAL.Source_Doc_No 
union all
select max(tspl_pR_detail.srn_id)as INVSourceDocNo,'SRN' as [Trans Type],max(tspl_pR_detail.PR_No) as RefDocNo, max(SRNJE.Voucher_No) as Voucher_No,convert(varchar,max(SRNJE.Voucher_date),103) as Voucher_date,0 AS InDR,0 as InCR,sum(case when TSPL_JOURNAL_DETAILS.Amount<0   THEN (1 *TSPL_JOURNAL_DETAILS.Amount)   ELSE 0 end) as GLDR,0 as [Gl Cr],0 ,0 from tspl_pR_detail INNER JOIN TSPL_PR_HEAD ON tspl_pR_HEAD.pr_NO=tspl_pR_DETAIL.PR_NO 
inner join tspl_vendor_invoice_head on tspl_vendor_invoice_head.Against_PurchaseReturn_No=tspl_pR_HEAD.pr_NO
lEFT oUTER jOIN TSPL_JOURNAL_mASTER ON TSPL_JOURNAL_mASTER.SOURCE_DOC_NO=tspl_vendor_invoice_head.Document_No
left join TSPL_JOURNAL_DETAILS on TSPL_JOURNAL_DETAILS.Voucher_No=TSPL_JOURNAL_MASTER.Voucher_No  
left outer join TSPL_JOURNAL_mASTER as SRNJE ON SRNJE.SOURCE_DOC_NO=tspl_pR_detail.srn_id
left outer join tspL_srn_detail on tspL_srn_detail.SRn_NO= tspl_pR_detail.srn_id
where ISNULL(tspl_pR_HEAD.AUTO_GEN_AGAINNT_PI_NO,'')<>'' AND tspl_pR_HEAD.TRANSACTION_TYPE='P' and isnull(tspl_pR_detail.srn_id,'')<>'' and (tspL_srn_detail.Rejected_Qty>0 or tspL_srn_detail.Short_Qty>0) AND TSPL_JOURNAL_DETAILS.Reco_Control_Account='I' 
and convert(date,tspl_pR_HEAD.PR_Date,103) >='" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "' AND convert(date,tspl_pR_HEAD.PR_Date,103) <='" + clsCommon.GetPrintDate(ToDate.Value, "dd-MMM-yyyy") + "' "
                If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                    qry += " and tspL_srn_detail.Item_Code in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ")"
                End If
                If arrLocation IsNot Nothing AndAlso arrLocation.Count > 0 Then
                    qry += " and tspl_pR_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(arrLocation) + ")"
                End If
                qry += " and 1=2 " ''Added by Balwinder on 02/06/2021  because Its Impact Wrong in EROD
                '' when PI and Storeadjustment has created against SRN
                qry += "group by TSPL_JOURNAL_MASTER.Source_Doc_No 
union all
select INVSUM.Source_Doc_No,INVSUM.Trans_Type,MAx(JE_SOURCE_DOC_NO) as RefDocNo,max(TSPL_JOURNAL_mASTER.Voucher_No) as Voucher_No,convert(varchar,max(TSPL_JOURNAL_mASTER.Voucher_date),103) as Voucher_date  ,sum(INVSUM.[Inv Dr]) as [Inv Dr] ,sum(INVSUM.[Inv Cr]) as [Inv Cr],0,0,0,0 from( select  TSPL_PI_HEAD.Against_SRN  as Source_Doc_No,INV.Source_doc_no+', '+tspl_adjustment_header.Against_PI_No_Difference as JE_SOURCE_DOC_NO   ,INV.Trans_Type,INV.[Inv Control Ac] ,INV.[Inv Dr] ,INV.[Inv Cr] from ( 
select Source_Doc_Date,Trans_Id,tspl_inventory_movement.Item_Code, Source_Doc_No,'SRN' as Trans_Type,(case when InOut='O' THEN isnull(Inventory_CrAcc,'')  ELSE isnull(Inventory_DrAcc,'') end) as [Inv Control Ac]  ,(case when InOut='I' THEN Avg_Cost ELSE 0 end) as [Inv Dr],(case when InOut='O' THEN Avg_Cost ELSE 0 end) as [Inv Cr]  from tspl_inventory_movement   where ((Inventory_CrAcc is not null and Inventory_CrAcc<>'') or (Inventory_DrAcc is not null and Inventory_DrAcc<>''))  
 and convert(date,Punching_Date,103) >='" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "' AND convert(date,Punching_Date,103) <='" + clsCommon.GetPrintDate(ToDate.Value, "dd-MMM-yyyy") + "' "
                If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                    qry += " and Item_Code in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ")"
                End If
                If arrLocation IsNot Nothing AndAlso arrLocation.Count > 0 Then
                    qry += " and Location_Code in (" + clsCommon.GetMulcallString(arrLocation) + ")"
                End If
                qry += " and Trans_Type in ('IC-AD') )INV 
inner join tspl_adjustment_header on INV.source_doc_no =tspl_adjustment_header.adjustment_no
inner join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_NO=tspl_adjustment_header.Against_PI_No_Difference
left join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=INV.[Inv Control Ac] left join tspl_location_master   on tspl_location_master.location_code=TSPL_GL_ACCOUNTS.Account_Seg_Code7 left join TSPL_INVENTORY_SOURCE_CODE on TSPL_INVENTORY_SOURCE_CODE.code=INV.Trans_Type  
where  tspl_adjustment_header.AdjustType='Adj' and isnull(tspl_adjustment_header.Against_PI_No_Difference,'')<>''  
)INVSUM lEFT oUTER jOIN TSPL_JOURNAL_mASTER ON TSPL_JOURNAL_mASTER.SOURCE_DOC_NO=INVSUM.Source_Doc_No
group by INVSUM.Source_Doc_No,INVSUM.Trans_Type,INVSUM.JE_SOURCE_DOC_NO 
union all
select max(tspl_pi_head.Against_SRN ) as Against_SRN,'SRN' as [Trans Type],null as RefDocNo,max(SRNJE.Voucher_No) as Voucher_No,convert(varchar,max(SRNJE.Voucher_date),103) as Voucher_date,0 AS InDR,0 as InCR,sum(case when TSPL_JOURNAL_DETAILS.Amount>0 THEN TSPL_JOURNAL_DETAILS.Amount ELSE 0 end) as [Gl Dr]  ,sum(case when TSPL_JOURNAL_DETAILS.Amount<0   THEN (-1 *TSPL_JOURNAL_DETAILS.Amount)   ELSE 0 end) as [Gl Cr],0 ,0 from tspl_pi_head 
lEFT oUTER jOIN TSPL_JOURNAL_mASTER ON TSPL_JOURNAL_mASTER.SOURCE_DOC_NO=tspl_pi_head.PI_No
left join TSPL_JOURNAL_DETAILS on TSPL_JOURNAL_DETAILS.Voucher_No=TSPL_JOURNAL_MASTER.Voucher_No
left outer join TSPL_JOURNAL_mASTER as SRNJE ON SRNJE.SOURCE_DOC_NO=tspl_pi_head.Against_SRN
where 1=1 and  isnull(tspl_pi_head.Against_SRN,'')<>'' AND TSPL_JOURNAL_DETAILS.Reco_Control_Account='I' 
and convert(date,tspl_pi_head.PI_Date,103) >='" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "' AND convert(date,tspl_pi_head.PI_Date,103) <='" + clsCommon.GetPrintDate(ToDate.Value, "dd-MMM-yyyy") + "' "
                If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                    qry += " and tspl_pi_detail.Item_Code in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ")"
                End If
                If arrLocation IsNot Nothing AndAlso arrLocation.Count > 0 Then
                    qry += " and tspl_pi_head.Bill_To_Location in (" + clsCommon.GetMulcallString(arrLocation) + ")"
                End If


                qry += " and tspl_pi_head.PI_No in (select Against_PI_No_Difference from tspl_adjustment_header where  AdjustType='Adj' and isnull(Against_PI_No_Difference,'')<>'')  group by tspl_pi_head.PI_No )CTEFINAL left outer join tspl_journal_master on tspl_journal_master.voucher_no =CTEFINAL.voucher_no  where 1=1 "

                If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
                    qry += " and  CTEFINAL.Trans_Type in (" + clsCommon.GetMulcallString(txtTransaction.arrValueMember) + ")"
                End If
                If txtSourceCode.arrValueMember IsNot Nothing AndAlso txtSourceCode.arrValueMember.Count > 0 Then
                    qry += " and  tspl_journal_master.source_code in (" + clsCommon.GetMulcallString(txtSourceCode.arrValueMember) + ")"
                End If
                qry += " Group by CTEFINAL.Source_Doc_No, Trans_Type, CTEFINAL.Voucher_No, CTEFINAL.voucher_date"


            ElseIf ChkDetail.IsChecked Then
                qry = "  WITH CTEINV(Source_Doc_Date,RowNo,Item_Code,Source_Doc_No,JE_SOURCE_DOC_NO,Name,Trans_Type,[Inv Control Ac],[Inv Ac Description],[Inv Dr],[Inv Cr])  as ( " &
                         "select Source_Doc_Date, ROW_NUMBER() OVER (PARTITION BY INVSUM.Source_Doc_No ORDER BY INVSUM.Source_Doc_No, INVSUM.Trans_Id)  as RowNo, " &
                         "INVSUM.Item_Code,INVSUM.Source_Doc_No,INVSUM.JE_SOURCE_DOC_NO,INVSUM.Name ,INVSUM.Trans_Type,INVSUM.[Inv Control Ac],INVSUM.[Inv Ac Description] , " &
                         "INVSUM.[Inv Dr] as [Inv Dr] ,INVSUM.[Inv Cr] as [Inv Cr] from( " &
                         "select INV.Source_Doc_Date,INV.Trans_Id,INV.Item_Code, " &
                          "TSPL_INVENTORY_SOURCE_CODE.Name,"
                ' "case when TSPL_VENDOR_INVOICE_HEAD.Document_No is not null then TSPL_VENDOR_INVOICE_HEAD.Document_No " & _
                '" when isnull(TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'' )<>'' then TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return  " & _
                '" when isnull(TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'' )<>'' then TSPL_Customer_Invoice_Head.Against_Sale_Return_No  " & _
                '" when isnull(TSPL_Customer_Invoice_Head.AgainstScrapReturn ,'')<>'' then TSPL_Customer_Invoice_Head.AgainstScrapReturn " & _
                '" when isnull(TSPL_Customer_Invoice_Head.AgainstScrap ,'')<>'' then TSPL_Customer_Invoice_Head.AgainstScrap else INV.Source_Doc_No end as Source_Doc_No" & _
                qry += " INV.Source_Doc_No  as Source_Doc_No,coalesce(TSPL_Customer_Invoice_Head.Document_No ,TSPL_VENDOR_INVOICE_HEAD.Document_No) as JE_SOURCE_DOC_NO " &
                         ",INV.Trans_Type,INV.[Inv Control Ac] ,TSPL_GL_ACCOUNTS.Description+','+TSPL_LOCATION_MASTER.Location_Desc as [Inv Ac Description] ,INV.[Inv Dr] ,INV.[Inv Cr] from (  " &
                          "select Source_Doc_Date,Trans_Id,tspl_inventory_movement.Item_Code, Source_Doc_No,Trans_Type,(case when InOut='O' THEN isnull(Inventory_CrAcc,'') ELSE isnull(Inventory_DrAcc,'') end) as [Inv Control Ac]  ,(case when InOut='I' THEN Avg_Cost ELSE 0 end) as [Inv Dr],(case when InOut='O' THEN Avg_Cost ELSE 0 end) as [Inv Cr] from tspl_inventory_movement  " &
                          " where ((Inventory_CrAcc is not null and Inventory_CrAcc<>'') or (Inventory_DrAcc is not null and Inventory_DrAcc<>''))  "
                qry += " and convert(date,Punching_Date,103) >='" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "' AND convert(date,Punching_Date,103) <='" + clsCommon.GetPrintDate(ToDate.Value, "dd-MMM-yyyy") + "' "
                If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                    qry += " and Item_Code in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ")"
                End If
                If arrLocation IsNot Nothing AndAlso arrLocation.Count > 0 Then
                    qry += " and Location_Code in (" + clsCommon.GetMulcallString(arrLocation) + ")"
                End If
                If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
                    qry += " and Trans_Type in (" + clsCommon.GetMulcallString(txtTransaction.arrValueMember) + ")"
                End If
                If chkExcludeConsumptionLoc.Checked Then
                    qry += " And ( location_code not in (Select Location_code from tspl_location_master where TSPL_LOCATION_MASTER.Is_Consumption_Location =1) or Trans_Type Not  in ('PP_ISSUE','PP_STD-FQC','PRD_STG_PROC','PROD_ENTRY'))" + Environment.NewLine
                End If
                qry += " union all  " &
                "select Source_Doc_Date,max(Trans_Id) as Trans_Id,Item_Code,  Source_Doc_No,Trans_Type,[Inv Control Ac],sum([Inv Dr]) as [Inv Dr],sum([Inv Cr]) from ( select Source_Doc_Date,Trans_Id,tspl_inventory_movement_new.Item_Code,  " &
                "Source_Doc_No,Trans_Type,(case when InOut='O' THEN isnull(Inventory_CrAcc,'') ELSE isnull(Inventory_DrAcc,'') end) as [Inv Control Ac] ,(case when InOut='I' THEN Avg_Cost ELSE 0 end) as [Inv Dr],(case when InOut='O' THEN Avg_Cost ELSE 0 end) as [Inv Cr] from tspl_inventory_movement_new   " &
                             " where ((Inventory_CrAcc is not null and Inventory_CrAcc<>'') or (Inventory_DrAcc is not null and Inventory_DrAcc<>'')) "
                qry += " and convert(date,Punching_Date,103) >='" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "' AND convert(date,Punching_Date,103) <='" + clsCommon.GetPrintDate(ToDate.Value, "dd-MMM-yyyy") + "' "
                If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                    qry += " and Item_Code in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ")"
                End If

                If arrLocation IsNot Nothing AndAlso arrLocation.Count > 0 Then
                    qry += " and case when len(isnull(main_location,''))>0 then main_location else  Location_Code end in (" + clsCommon.GetMulcallString(arrLocation) + ")"
                End If

                If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
                    qry += " and Trans_Type in (" + clsCommon.GetMulcallString(txtTransaction.arrValueMember) + ")"
                End If
                If chkExcludeConsumptionLoc.Checked Then
                    qry += " And ( location_code not in (Select Location_code from tspl_location_master where TSPL_LOCATION_MASTER.Is_Consumption_Location =1) or Trans_Type Not  in ('PP_ISSUE','PP_STD-FQC','PRD_STG_PROC','PROD_ENTRY'))" + Environment.NewLine
                End If
                'qry += ")INV left join TSPL_ACCOUNT_MAIN_GL_ACCOUNT on TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account=left(INV.[Inv Control Ac],len(INV.[Inv Control Ac])-4)  left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=right(INV.[Inv Control Ac],3)   left join TSPL_INVENTORY_SOURCE_CODE on TSPL_INVENTORY_SOURCE_CODE.code=INV.Trans_Type " & _
                qry += " )InnerQry group by Source_Doc_Date,Item_Code,  Source_Doc_No,Trans_Type,[Inv Control Ac] )INV "
                '" left join TSPL_ACCOUNT_MAIN_GL_ACCOUNT on TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Main_GL_Account=(case when len(INV.[Inv Control Ac])>0 then left(INV.[Inv Control Ac],len(INV.[Inv Control Ac])-4) else '' end) " & _
                '" left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=(case when len(INV.[Inv Control Ac])>0 then right(INV.[Inv Control Ac],3) else '' end) " & _
                qry += " left join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=INV.[Inv Control Ac]"
                qry += " left join tspl_location_master on tspl_location_master.location_code=TSPL_GL_ACCOUNTS.Account_Seg_Code7"
                qry += " left join TSPL_INVENTORY_SOURCE_CODE on TSPL_INVENTORY_SOURCE_CODE.code=INV.Trans_Type " &
                      " left join TSPL_VENDOR_INVOICE_HEAD on " &
                      " case when TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No is not null then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  " &
                      " when TSPL_VENDOR_INVOICE_HEAD.RefDocType='BM-PR' then TSPL_VENDOR_INVOICE_HEAD.RefDocNo " &
                      " else TSPL_VENDOR_INVOICE_HEAD.Document_No end=INV.Source_Doc_No " &
                      " left join TSPL_Customer_Invoice_Head on " &
                      " case when isnull(TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'' )<>'' then TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return  when isnull(TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'' )<>'' then TSPL_Customer_Invoice_Head.Against_Sale_Return_No when isnull(TSPL_Customer_Invoice_Head.AgainstScrapReturn ,'')<>'' then TSPL_Customer_Invoice_Head.AgainstScrapReturn when isnull(TSPL_Customer_Invoice_Head.AgainstScrap ,'')<>'' then TSPL_Customer_Invoice_Head.AgainstScrap else TSPL_Customer_Invoice_Head.Document_No end=INV.Source_Doc_No " &
                      ")INVSUM " &
                      "),  CTEJV(JVRowNo,Voucher_No,Source_Code,Source_Doc_No,[Gl Account],[Gl Account Descriptioin],[Gl Dr],[Gl Cr])  as   (select  " &
                      " ROW_NUMBER() OVER (PARTITION BY Source_Doc_No ORDER BY Source_Doc_No, Detail_Line_No) as JVRowNo, " &
                      " TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_MASTER.Source_Code,TSPL_JOURNAL_MASTER.Source_Doc_No,TSPL_JOURNAL_DETAILS.Account_code as [Gl Account],TSPL_JOURNAL_DETAILS.Account_Desc as [Gl Account Descriptioin],(case when TSPL_JOURNAL_DETAILS.Amount>0 THEN TSPL_JOURNAL_DETAILS.Amount ELSE 0 end) as [Gl Dr]  ,(case when TSPL_JOURNAL_DETAILS.Amount<0 THEN (-1 *TSPL_JOURNAL_DETAILS.Amount) ELSE 0 end) as [Gl Cr]  from TSPL_JOURNAL_MASTER   left join TSPL_JOURNAL_DETAILS on TSPL_JOURNAL_DETAILS.Voucher_No=TSPL_JOURNAL_MASTER.Voucher_No  where Reco_Control_Account='I'   " &
                      " ) " &
                      " SELECT CTEINV.Source_Doc_No as [Inv Source Doc No],CTEINV.Item_Code as [Item Code] ,CTEINV.Trans_Type as [Trans Type],CTEINV.Name as [Trans Name] " &
                      " ,CTEINV.[Inv Control Ac],CTEINV.[Inv Ac Description],CTEINV.[Inv Dr],CTEINV.[Inv Cr],CTEINV.JE_SOURCE_DOC_NO as [AR-AP Document],CTEJV.Voucher_No as [Voucher No]  " &
                      " ,CTEJV.Source_Doc_No as [Source Doc No],CTEJV.Source_Code as [Source Code],CTEJV.[Gl Account],CTEJV.[Gl Account Descriptioin],CTEJV.[Gl Dr],CTEJV.[Gl Cr]  " &
                      " ,isnull(CTEJV.[Gl Dr],0)-isnull(CTEINV.[Inv Dr],0) as [Diff Dr],isnull(CTEJV.[Gl Cr],0)-isnull(CTEINV.[Inv Cr],0) as [Diff Cr]" &
                      " FROM CTEINV  LEFT JOIN CTEJV ON CTEINV.[Inv Control Ac] = CTEJV.[Gl Account]  and coalesce(JE_SOURCE_DOC_NO,CTEINV.Source_Doc_No,'')=CTEJV.Source_Doc_No   " &
                      " and CTEINV.RowNo=CTEJV.JVRowNo " &
                      " where 2=2   "

                If txtAccount.arrValueMember IsNot Nothing AndAlso txtAccount.arrValueMember.Count > 0 Then
                    qry += " AND CTEINV.[Inv Control Ac] in (" + clsCommon.GetMulcallString(txtAccount.arrValueMember) + ")"
                End If
                If txtSourceCode.arrValueMember IsNot Nothing AndAlso txtSourceCode.arrValueMember.Count > 0 Then
                    qry += " and CTEJV.Source_Code in (" + clsCommon.GetMulcallString(txtSourceCode.arrValueMember) + ")"
                End If

                qry += "  order by CTEINV.Source_Doc_Date "

            ElseIf chkSummary.IsChecked Then

                qry = " WITH CTEINV(Source_Doc_No,JE_SOURCE_DOC_NO " &
                " ,[Inv Control Ac],[Inv Ac Description],[Inv Dr],[Inv Cr])  as ( select INVSUM.Source_Doc_No,INVSUM.JE_SOURCE_DOC_NO " &
                " ,INVSUM.[Inv Control Ac],INVSUM.[Inv Ac Description] , sum(INVSUM.[Inv Dr]) as [Inv Dr] ,sum(INVSUM.[Inv Cr]) as [Inv Cr] from( select  " &
                " INV.Source_Doc_No  as Source_Doc_No,coalesce(TSPL_Customer_Invoice_Head.Document_No ,TSPL_VENDOR_INVOICE_HEAD.Document_No) as JE_SOURCE_DOC_NO  " &
                " ,INV.Trans_Type,INV.[Inv Control Ac] ,TSPL_GL_ACCOUNTS.Description+','+TSPL_LOCATION_MASTER.Location_Desc as [Inv Ac Description] ,INV.[Inv Dr] ,INV.[Inv Cr] from (   " &
                " select Source_Doc_Date,Trans_Id,tspl_inventory_movement.Item_Code, Source_Doc_No,Trans_Type,(case when InOut='O' THEN isnull(Inventory_CrAcc,'') " &
                " ELSE isnull(Inventory_DrAcc,'') end) as [Inv Control Ac]  ,(case when InOut='I' THEN Avg_Cost ELSE 0 end) as [Inv Dr],(case when InOut='O' THEN Avg_Cost ELSE 0 end) as [Inv Cr] " &
                " from tspl_inventory_movement   where ((Inventory_CrAcc is not null and Inventory_CrAcc<>'') or (Inventory_DrAcc is not null and Inventory_DrAcc<>''))  "

                qry += " and convert(date,Punching_Date,103) >='" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "' AND convert(date,Punching_Date,103) <='" + clsCommon.GetPrintDate(ToDate.Value, "dd-MMM-yyyy") + "' "
                If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                    qry += " and Item_Code in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ")"
                End If
                If arrLocation IsNot Nothing AndAlso arrLocation.Count > 0 Then
                    qry += " and Location_Code in (" + clsCommon.GetMulcallString(arrLocation) + ")"
                End If
                If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
                    qry += " and Trans_Type in (" + clsCommon.GetMulcallString(txtTransaction.arrValueMember) + ")"
                End If
                If chkExcludeConsumptionLoc.Checked Then
                    qry += " And ( location_code not in (Select Location_code from tspl_location_master where TSPL_LOCATION_MASTER.Is_Consumption_Location =1) or Trans_Type Not  in ('PP_ISSUE','PP_STD-FQC','PRD_STG_PROC','PROD_ENTRY'))" + Environment.NewLine
                End If
                qry += " union all  select Source_Doc_Date,max(Trans_Id) as Trans_Id,Item_Code,  Source_Doc_No,Trans_Type,[Inv Control Ac],sum([Inv Dr]) as [Inv Dr],sum([Inv Cr]) from ( select Source_Doc_Date,Trans_Id,tspl_inventory_movement_new.Item_Code,  Source_Doc_No,Trans_Type,(case when InOut='O' THEN isnull(Inventory_CrAcc,'') ELSE  " &
                " isnull(Inventory_DrAcc,'') end) as [Inv Control Ac] ,(case when InOut='I' THEN Avg_Cost ELSE 0 end) as [Inv Dr],(case when InOut='O' THEN Avg_Cost ELSE 0 end) as [Inv Cr]  " &
                " from tspl_inventory_movement_new    where ((Inventory_CrAcc is not null and Inventory_CrAcc<>'') or (Inventory_DrAcc is not null and Inventory_DrAcc<>''))  "

                qry += " and convert(date,Punching_Date,103) >='" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "' AND convert(date,Punching_Date,103) <='" + clsCommon.GetPrintDate(ToDate.Value, "dd-MMM-yyyy") + "' "
                If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                    qry += " and Item_Code in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ")"
                End If
                If arrLocation IsNot Nothing AndAlso arrLocation.Count > 0 Then
                    qry += " and case when len(isnull(main_location,''))>0 then main_location else  Location_Code end in (" + clsCommon.GetMulcallString(arrLocation) + ")"
                End If

                If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
                    qry += " and Trans_Type in (" + clsCommon.GetMulcallString(txtTransaction.arrValueMember) + ")"
                End If
                If chkExcludeConsumptionLoc.Checked Then
                    qry += " And ( location_code not in (Select Location_code from tspl_location_master where TSPL_LOCATION_MASTER.Is_Consumption_Location =1) or Trans_Type Not  in ('PP_ISSUE','PP_STD-FQC','PRD_STG_PROC','PROD_ENTRY'))" + Environment.NewLine
                End If
                qry += " )InnerQry group by Source_Doc_Date,Item_Code,  Source_Doc_No,Trans_Type,[Inv Control Ac] )INV  left join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=INV.[Inv Control Ac] left join tspl_location_master  " &
                " on tspl_location_master.location_code=TSPL_GL_ACCOUNTS.Account_Seg_Code7 left join TSPL_INVENTORY_SOURCE_CODE on TSPL_INVENTORY_SOURCE_CODE.code=INV.Trans_Type  " &
                "  left join TSPL_VENDOR_INVOICE_HEAD on  case when TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No is not null then TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No   " &
                "   when TSPL_VENDOR_INVOICE_HEAD.RefDocType='BM-PR' then TSPL_VENDOR_INVOICE_HEAD.RefDocNo  else TSPL_VENDOR_INVOICE_HEAD.Document_No end=INV.Source_Doc_No   " &
                "   left join TSPL_Customer_Invoice_Head on  case when isnull(TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'' )<>''  " &
                "   then TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return  when isnull(TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'' )<>''  " &
                "   then TSPL_Customer_Invoice_Head.Against_Sale_Return_No when isnull(TSPL_Customer_Invoice_Head.AgainstScrapReturn ,'')<>'' then TSPL_Customer_Invoice_Head.AgainstScrapReturn " &
                "   when isnull(TSPL_Customer_Invoice_Head.AgainstScrap ,'')<>'' then TSPL_Customer_Invoice_Head.AgainstScrap else TSPL_Customer_Invoice_Head.Document_No end=INV.Source_Doc_No " &
                "   )INVSUM group by INVSUM.Source_Doc_No,INVSUM.JE_SOURCE_DOC_NO  ,INVSUM.[Inv Control Ac],INVSUM.[Inv Ac Description]),  " &
                " CTEJV(Source_Doc_No,[Gl Account] " &
                " ,[Gl Dr],[Gl Cr])  as   (select   TSPL_JOURNAL_MASTER.Source_Doc_No,TSPL_JOURNAL_DETAILS.Account_code as [Gl Account] " &
                " ,sum(case when TSPL_JOURNAL_DETAILS.Amount>0 THEN TSPL_JOURNAL_DETAILS.Amount ELSE 0 end) as [Gl Dr]  ,sum(case when TSPL_JOURNAL_DETAILS.Amount<0  " &
                " THEN (-1 *TSPL_JOURNAL_DETAILS.Amount)  " &
                " ELSE 0 end) as [Gl Cr]  from TSPL_JOURNAL_MASTER   left join TSPL_JOURNAL_DETAILS on TSPL_JOURNAL_DETAILS.Voucher_No=TSPL_JOURNAL_MASTER.Voucher_No " &
                " where Reco_Control_Account='I' group by TSPL_JOURNAL_MASTER.Source_Doc_No,TSPL_JOURNAL_DETAILS.Account_code ) " &
                " ,CTEFINAL([Inv Control Ac],[Inv Ac Description],[Inv Dr],[Inv Cr],[Gl Dr],[Gl Cr]  )  as   " &
                " (SELECT CTEINV.[Inv Control Ac],CTEINV.[Inv Ac Description],CTEINV.[Inv Dr],CTEINV.[Inv Cr],CTEJV.[Gl Dr],CTEJV.[Gl Cr]   " &
                " FROM CTEINV  LEFT JOIN CTEJV ON CTEINV.[Inv Control Ac] = CTEJV.[Gl Account]  " &
                " and coalesce(JE_SOURCE_DOC_NO,CTEINV.Source_Doc_No,'')=CTEJV.Source_Doc_No   " &
                " where 2=2   ) " &
                " select [Inv Control Ac],[Inv Ac Description],isnull(sum(CTEFINAL.[Inv Dr]),0) as [Inv Dr],isnull(sum(CTEFINAL.[Inv Cr]),0) as [Inv Cr] " &
                " ,isnull(sum(CTEFINAL.[Gl Dr]),0) as [Gl Dr],isnull(sum(CTEFINAL.[Gl Cr]),0) as [Gl Cr] " &
                " ,isnull(sum(CTEFINAL.[Gl Dr]),0)-isnull(sum(CTEFINAL.[Inv Dr]),0) as [Diff Dr],isnull(sum(CTEFINAL.[Gl Cr]),0)-isnull(sum(CTEFINAL.[Inv Cr]),0) as [Diff Cr]  " &
                " from CTEFINAL where 1=1 "
                If txtAccount.arrValueMember IsNot Nothing AndAlso txtAccount.arrValueMember.Count > 0 Then
                    qry += " AND CTEFINAL.[Inv Control Ac] in (" + clsCommon.GetMulcallString(txtAccount.arrValueMember) + ")"
                End If
                qry += " Group by [Inv Control Ac],[Inv Ac Description] "
            End If


            dt = clsDBFuncationality.GetDataTable(qry)
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()



            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                Gv1.DataSource = dt
                For ii As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(ii).ReadOnly = True
                Next
                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.BestFitColumns()
                Gv1.EnableFiltering = True
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim InventoryDr As New GridViewSummaryItem("Inv Dr", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(InventoryDr)
            Dim InventoryCr As New GridViewSummaryItem("Inv Cr", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(InventoryCr)

            Dim GlDr As New GridViewSummaryItem("Gl Dr", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(GlDr)
            Dim GlCr As New GridViewSummaryItem("Gl Cr", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(GlCr)


            Dim Diff_Dr As New GridViewSummaryItem("Diff Dr", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Diff_Dr)
            Dim Diff_Cr As New GridViewSummaryItem("Diff Cr", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Diff_Cr)

            Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            PageSetupReport_ID = Getreport_id()
            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub View()
        Try
            If Gv1.Rows.Count > 0 Then
                Dim view As New ColumnGroupsViewDefinition()

                view.ColumnGroups.Add(New GridViewColumnGroup("Inventory Control Account"))
                view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Inventory Control Ac").Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Inventory Ac Description").Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Inventory Dr").Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Inventory Cr").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("From General Ledger"))
                view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Gl Account").Name)
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Gl Account Descriptioin").Name)
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Gl Dr").Name)
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Gl Cr").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("Difference"))
                view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("Dr").Name)
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("Cr").Name)
                Gv1.ViewDefinition = view
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Export(EnumExportTo.Excel)
    End Sub
    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Export(EnumExportTo.PDF)
    End Sub
    Private Sub txtAccount__My_Click(sender As Object, e As EventArgs) Handles txtAccount._My_Click
        LoadAccounts()
    End Sub
    Private Sub txtSourceCode__My_Click(sender As Object, e As EventArgs) Handles txtSourceCode._My_Click
        LoadSourceCode()
    End Sub
    Sub LoadSourceCode()
        strQry = "select SourceCode as Code,SourceDescription as Description from TSPL_GL_SOURCECODE "
        txtSourceCode.arrValueMember = clsCommon.ShowMultipleSelectForm("SourceCoce@IN", strQry, "Code", "Description", txtSourceCode.arrValueMember, txtSourceCode.arrDispalyMember)
    End Sub
    Sub LoadAccounts()
        strQry = "select Account_Code as Code,[Description] from TSPL_GL_ACCOUNTS"
        txtAccount.arrValueMember = clsCommon.ShowMultipleSelectForm("Accounts@IN", strQry, "Code", "Description", txtAccount.arrValueMember, txtAccount.arrDispalyMember)
    End Sub

    Sub LoadLocation()
        gvLocation.DataSource = Nothing
        Dim whrCls As String = " and ((Is_Section='N' and Is_Sub_Location='N' and Location_Type IN ('Physical','Logical','Virtual') ) or (CSA_Type='Y') )"
        Dim qry As String = " select cast( 0 as bit) as SEL,Location_Code as CODE,Location_Desc as NAME from TSPL_LOCATION_MASTER where 1=1 " + whrCls + ""
        qry += " and  TSPL_LOCATION_MASTER.GIT_Type<>'Y' "
        qry += " order by Location_Code"
        gvLocation.DataSource = clsDBFuncationality.GetDataTable(qry)

        gvLocation.Columns("SEL").ReadOnly = False
        gvLocation.Columns("SEL").Width = 30
        gvLocation.Columns("SEL").HeaderText = " "

        gvLocation.Columns("CODE").ReadOnly = True
        gvLocation.Columns("CODE").Width = 100
        gvLocation.Columns("CODE").HeaderText = "Code"

        gvLocation.Columns("NAME").ReadOnly = True
        gvLocation.Columns("NAME").Width = 200
        gvLocation.Columns("NAME").HeaderText = "Description"

        gvLocation.ShowGroupPanel = False
        gvLocation.AllowAddNewRow = False
        gvLocation.AllowColumnReorder = False
        gvLocation.AllowRowReorder = False
        gvLocation.EnableSorting = False
        gvLocation.ShowFilteringRow = True
        gvLocation.EnableFiltering = True
        gvLocation.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvLocation.MasterTemplate.ShowRowHeaderColumn = True
    End Sub
    Private Sub rbtnLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnLocationAll.ToggleStateChanged, rbtnLocationSelect.ToggleStateChanged
        gvLocation.Enabled = rbtnLocationSelect.IsChecked
        RadButton4.Enabled = rbtnLocationSelect.IsChecked
        RadButton5.Enabled = rbtnLocationSelect.IsChecked
    End Sub
    Private Sub RadButton4_Click(sender As Object, e As EventArgs) Handles RadButton4.Click
        CheckedAll(gvLocation)
    End Sub
    Private Sub RadButton5_Click(sender As Object, e As EventArgs) Handles RadButton5.Click
        UnCheckedAll(gvLocation)
    End Sub
    Private Sub CheckedAll(ByVal gv As RadGridView)
        For ii As Integer = 0 To gv.RowCount - 1
            gv.Rows(ii).Cells("SEL").Value = False
        Next
        For ii As Integer = 0 To gv.ChildRows.Count - 1
            gv.ChildRows(ii).Cells("SEL").Value = True
        Next
    End Sub
    Private Sub UnCheckedAll(ByVal gv As RadGridView)
        For ii As Integer = 0 To gv.RowCount - 1
            gv.Rows(ii).Cells("SEL").Value = False
        Next
    End Sub
    Private Sub gvLocation_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gvLocation.CellDoubleClick
        If clsCommon.myCBool(gvLocation.CurrentRow.Cells("SEL").Value) Then
            Dim frm As New FrmCategorySelect()
            frm.lvl = 3
            frm.strCode = clsCommon.myCstr(gvLocation.CurrentRow.Cells("CODE").Value)
            frm.arrIn = gvLocation.CurrentRow.Tag
            frm.ShowDialog()
            If Not frm.isCancel Then
                gvLocation.CurrentRow.Tag = frm.arrOut
            End If
        End If
    End Sub
    Function GetLocation() As ArrayList
        Dim strWhrCatg As String = ""
        Dim qry As String
        Dim arrLocation As ArrayList = Nothing
        If rbtnLocationSelect.IsChecked Then
            Dim IsApplicable As Boolean = False
            For ii As Integer = 0 To gvLocation.RowCount - 1
                If clsCommon.myCBool(gvLocation.Rows(ii).Cells("SEL").Value) Then
                    If IsApplicable Then
                        strWhrCatg += " Or "
                    End If
                    strWhrCatg += " ((case when Is_Section='N' and Is_Sub_Location='N' then Location_Code else Main_Location_Code end) = '" + clsCommon.myCstr(gvLocation.Rows(ii).Cells("CODE").Value) + "') "
                    IsApplicable = True
                    Dim arr As Dictionary(Of String, Object) = gvLocation.Rows(ii).Tag
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        strWhrCatg += " and Location_Code in ("
                        Dim isFirstTime As Boolean = True
                        For Each strInn As String In arr.Keys
                            If Not isFirstTime Then
                                strWhrCatg += ","
                            End If
                            strWhrCatg += "'" + strInn + "'"
                            isFirstTime = False
                        Next
                        strWhrCatg += ")"
                    End If
                End If
            Next
            If Not IsApplicable Then
                Throw New Exception("Please select at least one location")
            End If
            qry = "select Location_Code from TSPL_LOCATION_MASTER where 2=2 and (" + strWhrCatg + ")"
            Dim dtLoc As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dtLoc IsNot Nothing AndAlso dtLoc.Rows.Count > 0 Then
                arrLocation = New ArrayList
                For Each dr As DataRow In dtLoc.Rows
                    arrLocation.Add(dr("Location_Code"))
                Next
            End If
        End If
        Return arrLocation
    End Function

End Class
