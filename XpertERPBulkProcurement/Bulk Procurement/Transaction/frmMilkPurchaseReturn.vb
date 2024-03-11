Imports common
Imports System.Data.SqlClient

' Create By Prabhakar . Ticket Ref : BM00000007375
Public Class FrmMilkPurchaseReturn
    Inherits FrmMainTranScreen
    Dim AllowJobWorkonGateEntryBulkProc As Integer = 0
    Dim IsItemMilkType As Integer = 0
    Dim isInvoicefinder As Integer = 0
    Dim IsPriceChartGradeWise As Integer = 0
    Dim TankerFromMaster As Integer = 0
    Dim isSRNselected As Boolean = False
    Public isLoadData As Boolean = False
    Public strDocCode As String = Nothing
    Public Const colSelect As String = "colSelect"
    Public Const colSlNo As String = "SLNO"
    Public Const colSRNNo As String = "SRNNO"
    Public Const colSRNDATe As String = "SRNDate"
    Public Const colItemCode As String = "ItemCode"
    Public Const colHSN As String = "HSNCode"
    Public Const colItemDesc As String = "ItemDesc"
    Public Const colChamberDesc As String = "colChamberDesc"
    Public Const colQty As String = "Qty"
    Public Const colUOM As String = "UOM"
    Public Const colFat As String = "colFAT"
    Dim AllowDateChanged As Boolean = False
    Public Const colSNF As String = "colSNF"
    Public Const colFatKG As String = "colFATKG"
    Public Const colPendingQty As String = "colPendingQty"
    Public Const colSNFKG As String = "colSNFKG"
    Public Const colGrossWeight As String = "colGrossWeight"
    Public Const colTareWeight As String = "colTareWeight"
    Public Const colNetWeight As String = "colNetWeight"
    Public Const colFatRate As String = "colFATRate"
    Public Const colSNFRate As String = "colSNFRate"
    Public Const colAmt As String = "colAmt"
    Public Const colDeduc As String = "colDeduc"
    Public Const colIncen As String = "colIncen"
    Public Const colSpecialDeduc As String = "colSpecialDeduc"
    Public Const colActAmt As String = "colActAmt"
    Public Const colNetRate As String = "colNetRate"
    Public Const colPriceCode As String = "colPriceCode"
    Public Const colTnkrNo As String = "colTnkrNo"
    Public isCellValueChangedOpen As Boolean
    Public errorControl As clsErrorControl = New clsErrorControl()
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim obj As clsMilkPurchaseReturnHead = Nothing

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FrmMilkPurchaseReturn)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub fndDocNoReturn__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndDocNoReturn._MYValidating
        Dim whrCls As String = String.Empty
        whrCls = "2=2"
        If Not clsMccMaster.isCurrentUserHO() Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrCls = " and loc_code in(" & objCommonVar.strCurrUserLocations & ")"
            End If
        End If
        fndInvoiceNo.Value = clsMilkPurchaseReturnHead.getFinder(whrCls, fndInvoiceNo.Value, isButtonClicked)
        loadData(fndInvoiceNo.Value, NavigatorType.Current)
    End Sub

    Sub loadInvoiceData(ByVal str As String, ByVal navtype As NavigatorType)
        obj = clsMilkPurchaseReturnHead.getInvoiceData(str, navtype)
        If obj IsNot Nothing Then
            Reset()
            If obj.isPosted = 0 Then
                isSRNselected = True
            Else
                isSRNselected = False
            End If
            isLoadData = True
            'fndLocation.Enabled = False
            txtSubLocation.Value = obj.Joblocation_Code
            lblSubLocation.Text = clsLocation.GetName(txtSubLocation.Value, Nothing)
            chkJobWork.Checked = IIf(obj.IsAgainstJobWork = 1, True, False)
            fndInvoiceNo.Value = obj.Invoice_No
            dtpDocDate.Value = obj.Invoice_Date
            txtVendor.Text = obj.vendor_code
            lblVendorName.Text = clsVendorMaster.GetName(obj.vendor_code, Nothing)
            txtLocation.Text = obj.Loc_Code
            lblLocationName.Text = clsLocation.GetName(obj.Loc_Code, Nothing)
            dtpFromDate.Value = clsCommon.GetPrintDate(obj.SRN_From_Date, "dd/MMM/yyyy")
            dtpToDate.Value = clsCommon.GetPrintDate(obj.SRN_TO_Date, "dd/MMM/yyyy")
            txtTotalFatKg.Text = clsCommon.myFormat(MyMath.RoundDown(obj.Total_FAT_KG, 3), False, True, False, 3, True)
            txtTotalSNFKg.Text = clsCommon.myFormat(MyMath.RoundDown(obj.Total_SNF_KG, 3), False, True, False, 3, True)
            txtTotalQty.Text = clsCommon.myFormat(obj.Total_QTY)
            txtRoundOffAmt.Text = clsCommon.myFormat(obj.RoundOffAmount)
            txtTotalAmt.Text = clsCommon.myFormat(obj.Total_AMT)
            txtVendorInvoiceNo.Text = obj.Vendor_Invoice_No
            loadBlankGrid()
            If obj.isSRNTradeInvoice = 1 Then
                chkSRNTrade.Checked = True
            Else
                chkSRNTrade.Checked = False
            End If
            chkSRNTrade.Enabled = False
            Dim SRNs As String = ""

            If obj.arrDetail IsNot Nothing Then
                Dim arr As New List(Of String)
                For i As Integer = 0 To obj.arrDetail.Count - 1
                    gv.Rows.AddNew()
                    gv.Rows(i).Cells(colSlNo).Value = obj.arrDetail(i).SL_NO
                    gv.Rows(i).Cells(colSRNNo).Value = obj.arrDetail(i).SRN_NO
                    If Not arr.Contains(obj.arrDetail(i).SRN_NO) Then
                        arr.Add(obj.arrDetail(i).SRN_NO)
                        SRNs = SRNs & obj.arrDetail(i).SRN_NO
                    End If
                    If i <> obj.arrDetail.Count - 1 Then
                        SRNs = SRNs & ", "
                    End If
                    gv.Rows(i).Cells(colSRNDATe).Value = obj.arrDetail(i).SRN_Date
                    gv.Rows(i).Cells(colItemCode).Value = obj.arrDetail(i).Item_Code
                    gv.Rows(i).Cells(colItemDesc).Value = obj.arrDetail(i).Item_Desc
                    gv.Rows(i).Cells(colHSN).Value = clsItemMaster.GetItemHSNCode(obj.arrDetail(i).Item_Code, Nothing)
                    gv.Rows(i).Cells(colUOM).Value = obj.arrDetail(i).UOM
                    gv.Rows(i).Cells(colGrossWeight).Value = clsCommon.myFormat(obj.arrDetail(i).Gross_Weight)
                    gv.Rows(i).Cells(colTareWeight).Value = clsCommon.myFormat(obj.arrDetail(i).Tare_Weight)
                    gv.Rows(i).Cells(colNetWeight).Value = clsCommon.myFormat(obj.arrDetail(i).Net_Weight)
                    gv.Rows(i).Cells(colQty).Value = clsCommon.myFormat(obj.arrDetail(i).Invoice_Qty)
                    gv.Rows(i).Cells(colFat).Value = clsCommon.myFormat(obj.arrDetail(i).fat_per)
                    gv.Rows(i).Cells(colFatKG).Value = clsCommon.myFormat(obj.arrDetail(i).fat_KG, False, True, False, 3, True)
                    gv.Rows(i).Cells(colFatRate).Value = clsCommon.myFormat(obj.arrDetail(i).fat_Rate)
                    gv.Rows(i).Cells(colSNF).Value = clsCommon.myFormat(obj.arrDetail(i).snf_Per)
                    gv.Rows(i).Cells(colSNFKG).Value = clsCommon.myFormat(obj.arrDetail(i).SNF_KG, False, True, False, 3, True)
                    gv.Rows(i).Cells(colSNFRate).Value = clsCommon.myFormat(obj.arrDetail(i).SNF_Rate)
                    gv.Rows(i).Cells(colAmt).Value = clsCommon.myFormat(obj.arrDetail(i).Amount)
                    gv.Rows(i).Cells(colDeduc).Value = clsCommon.myFormat(obj.arrDetail(i).Deduction)
                    gv.Rows(i).Cells(colIncen).Value = clsCommon.myFormat(obj.arrDetail(i).Incentive)
                    gv.Rows(i).Cells(colSpecialDeduc).Value = clsCommon.myFormat(obj.arrDetail(i).Special_Deduction)
                    gv.Rows(i).Cells(colActAmt).Value = clsCommon.myFormat(obj.arrDetail(i).Actual_Amount)
                    gv.Rows(i).Cells(colPriceCode).Value = obj.arrDetail(i).price_code
                    gv.Rows(i).Cells(colNetRate).Value = clsCommon.myFormat(obj.arrDetail(i).NetRate)
                    gv.Rows(i).Cells(colTnkrNo).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select tanker_no from TSPL_Bulk_MILK_SRN where SRN_NO='" & obj.arrDetail.Item(i).SRN_NO & "' "))    '' RICHA AGARWAL IIf(chkSRNTrade.Checked, "NA", clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select tanker_no from TSPL_Bulk_MILK_SRN where SRN_NO='" & obj.arrDetail.Item(i).SRN_NO & "' ")))
                    gv.Rows(i).Cells(colChamberDesc).Value = obj.arrDetail(i).CHAMBER_DESC
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable("select TSPL_Bulk_MILK_SRN.SRN_NO, (TSPL_Bulk_MILK_SRN.Net_Weight-coalesce(SUM(tdl.invoice_qty),0)) as pending_qty from TSPL_Bulk_MILK_SRN left join ( select invoice_qty,SRN_NO  from Tspl_bulk_milk_purchase_invoice_detail left outer join Tspl_bulk_milk_purchase_invoice_head on Tspl_bulk_milk_purchase_invoice_head.DOC_NO=tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO  where Tspl_bulk_milk_purchase_invoice_head.isPosted=1) as  tdl  on TSPL_Bulk_MILK_SRN.SRN_NO=tdl.srn_no  where TSPL_Bulk_MILK_SRN.isPosted=1 and TSPL_Bulk_MILK_SRN.SRN_NO='" & obj.arrDetail(i).SRN_NO & "'  group by TSPL_Bulk_MILK_SRN.SRN_NO ,TSPL_Bulk_MILK_SRN.Net_Weight	")
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        gv.Rows(i).Cells(colPendingQty).Value = clsCommon.myFormat(clsCommon.myCdbl(dt.Rows(0)("pending_qty")))
                    End If

                Next
                txtSRNNo.Text = SRNs
            End If
            'btnSave.Text = "Update"
            'btnDelete.Enabled = True
            'btnPrint.Enabled = True
            'If obj.isPosted = 1 Then
            '    btnSave.Enabled = False
            '    btnPost.Enabled = False
            '    btnDelete.Enabled = False
            '    lblPending.Status = ERPTransactionStatus.Approved
            'Else
            '    btnSave.Enabled = True
            '    btnPost.Enabled = True
            '    btnDelete.Enabled = True
            '    lblPending.Status = ERPTransactionStatus.Pending
            'End If
        End If
        isLoadData = False
    End Sub
    Sub loadBlankGrid()
        gv.Rows.Clear()
        gv.Columns.Clear()


        Dim repoSLNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSLNo.FormatString = ""
        repoSLNo.HeaderText = "SL.No"
        repoSLNo.Name = colSlNo
        repoSLNo.Width = 60
        repoSLNo.ReadOnly = True
        repoSLNo.BestFit()
        gv.MasterTemplate.Columns.Add(repoSLNo)

        Dim repoTnkrNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTnkrNo.FormatString = ""
        repoTnkrNo.HeaderText = "Tanker No"
        repoTnkrNo.Name = colTnkrNo
        repoTnkrNo.Width = 100
        repoTnkrNo.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoTnkrNo)

        Dim repoSRNNO As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSRNNO.FormatString = ""
        repoSRNNO.HeaderText = "SRN No"
        repoSRNNO.Name = colSRNNo
        repoSRNNO.Width = 200
        repoSRNNO.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoSRNNO)

        Dim repoSRNDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoSRNDate.FormatString = "{0:d}"
        repoSRNDate.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
        repoSRNDate.HeaderText = "SRN Date"
        repoSRNDate.Name = colSRNDATe
        repoSRNDate.Width = 100
        repoSRNDate.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoSRNDate)

        Dim repoChName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoChName.FormatString = ""
        repoChName.HeaderText = "Chamber Desc"
        repoChName.Name = colChamberDesc
        repoChName.Width = 180
        repoChName.ReadOnly = True
        If TankerFromMaster = 0 Then repoChName.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoChName)

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colItemCode
        repoICode.Width = 100
        repoICode.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Desc"
        repoIName.Name = colItemDesc
        repoIName.Width = 180
        repoIName.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoIName)

        Dim repoHSNCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoHSNCode.FormatString = ""
        repoHSNCode.HeaderText = "HSN Code"
        repoHSNCode.Name = colHSN
        repoHSNCode.Width = 100
        repoHSNCode.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoHSNCode)

        Dim repoUOM As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUOM.FormatString = ""
        repoUOM.HeaderText = "UOM"
        repoUOM.Name = colUOM
        repoUOM.ReadOnly = True
        repoUOM.Width = 80
        repoUOM.WrapText = True
        repoUOM.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoUOM)

        Dim repoGrossWeight As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoGrossWeight.FormatString = ""
        repoGrossWeight.HeaderText = "Gross Weight"
        repoGrossWeight.Name = colGrossWeight
        repoGrossWeight.Width = 100
        repoGrossWeight.ReadOnly = True
        repoGrossWeight.IsVisible = False
        repoGrossWeight.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoGrossWeight)

        Dim repoTareWeight As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTareWeight.FormatString = ""
        repoTareWeight.HeaderText = "Tare Weight"
        repoTareWeight.Name = colTareWeight
        repoTareWeight.ReadOnly = True
        repoTareWeight.Width = 100
        repoTareWeight.WrapText = True
        repoTareWeight.IsVisible = False
        repoTareWeight.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoTareWeight)

        Dim repoNetWeight As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoNetWeight.FormatString = ""
        repoNetWeight.HeaderText = "Net Weight"
        repoNetWeight.Name = colNetWeight
        repoNetWeight.ReadOnly = True
        repoNetWeight.Width = 100
        repoNetWeight.WrapText = True
        repoNetWeight.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoNetWeight)

        Dim repoPendingQty As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPendingQty.FormatString = ""
        repoPendingQty.HeaderText = "Pending Qty"
        repoPendingQty.Name = colPendingQty
        repoPendingQty.ReadOnly = True
        repoPendingQty.WrapText = True
        repoPendingQty.Width = 100
        repoPendingQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoPendingQty)

        Dim repoInvoiceQty As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoInvoiceQty.FormatString = ""
        repoInvoiceQty.HeaderText = "Invoice Qty"
        repoInvoiceQty.Name = colQty
        repoInvoiceQty.ReadOnly = True
        repoInvoiceQty.WrapText = True
        repoInvoiceQty.Width = 100
        repoInvoiceQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoInvoiceQty)


        Dim repoPriceCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPriceCode.FormatString = ""
        repoPriceCode.HeaderText = "Price Code"
        repoPriceCode.Name = colPriceCode
        repoPriceCode.ReadOnly = True
        repoPriceCode.Width = 80
        repoPriceCode.WrapText = True
        repoPriceCode.IsVisible = False
        repoPriceCode.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoPriceCode)

        Dim repoFatPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFatPer.FormatString = ""
        repoFatPer.HeaderText = "FAT(%)"
        repoFatPer.Name = colFat
        repoFatPer.ReadOnly = True
        repoFatPer.Width = 80
        repoFatPer.WrapText = True
        'repoFatPer.IsVisible = False
        repoFatPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoFatPer)

        Dim repoSNFPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSNFPer.FormatString = ""
        repoSNFPer.HeaderText = "SNF(%)"
        repoSNFPer.Name = colSNF
        repoSNFPer.ReadOnly = True
        repoSNFPer.Width = 80
        repoSNFPer.WrapText = True
        'repoSNFPer.IsVisible = False
        repoSNFPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoSNFPer)


        Dim repoFATKG As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoFATKG.FormatString = ""
        repoFATKG.HeaderText = "FAT(KG)"
        repoFATKG.Name = colFatKG
        repoFATKG.ReadOnly = True
        repoFATKG.Width = 80
        repoFATKG.WrapText = True
        repoFATKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoFATKG)

        Dim repoSNFKG As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSNFKG.FormatString = ""
        repoSNFKG.HeaderText = "SNF(KG)"
        repoSNFKG.Name = colSNFKG
        repoSNFKG.ReadOnly = True
        repoSNFKG.Width = 80
        repoSNFKG.WrapText = True
        repoSNFKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoSNFKG)






        Dim repoAmount As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAmount.FormatString = ""
        repoAmount.HeaderText = "Amount"
        repoAmount.Name = colAmt
        repoAmount.ReadOnly = True
        repoAmount.Width = 100
        repoAmount.WrapText = True
        repoAmount.IsVisible = False
        repoAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoAmount)

        Dim repoDeduc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDeduc.FormatString = ""
        repoDeduc.HeaderText = "Deduction"
        repoDeduc.Name = colDeduc
        repoDeduc.ReadOnly = True
        repoDeduc.Width = 80
        repoDeduc.WrapText = True
        repoDeduc.IsVisible = False
        repoDeduc.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoDeduc)

        Dim repoIncen As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIncen.FormatString = ""
        repoIncen.HeaderText = "Incentive"
        repoIncen.Name = colIncen
        repoIncen.ReadOnly = True
        repoIncen.Width = 80
        repoIncen.WrapText = True
        repoIncen.IsVisible = False
        repoIncen.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoIncen)

        Dim repoSpclDeduc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSpclDeduc.FormatString = ""
        repoSpclDeduc.HeaderText = "Special Deduction"
        repoSpclDeduc.Name = colSpecialDeduc
        repoSpclDeduc.ReadOnly = True
        repoSpclDeduc.Width = 80
        repoSpclDeduc.WrapText = True
        repoSpclDeduc.IsVisible = False
        repoSpclDeduc.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoSpclDeduc)


        Dim repoActAmt As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoActAmt.FormatString = ""
        repoActAmt.HeaderText = "Net Rate"
        repoActAmt.Name = colNetRate
        repoActAmt.ReadOnly = True
        repoActAmt.Width = 80
        repoActAmt.WrapText = True
        repoActAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoActAmt)

        Dim repoFATRate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoFATRate.FormatString = ""
        repoFATRate.HeaderText = "FAT Rate"
        repoFATRate.Name = colFatRate
        repoFATRate.ReadOnly = True
        repoFATRate.Width = 80
        repoFATRate.WrapText = True
        repoFATRate.IsVisible = True
        repoFATRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoFATRate)


        Dim repoSNFRate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSNFRate.FormatString = ""
        repoSNFRate.HeaderText = "SNF Rate"
        repoSNFRate.Name = colSNFRate
        repoSNFRate.ReadOnly = True
        repoSNFRate.Width = 80
        repoSNFRate.WrapText = True
        repoSNFRate.IsVisible = True
        repoSNFRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoSNFRate)

        repoActAmt = New GridViewTextBoxColumn()
        repoActAmt.FormatString = ""
        repoActAmt.HeaderText = "Amount"
        repoActAmt.Name = colActAmt
        repoActAmt.ReadOnly = True
        repoActAmt.Width = 80
        repoActAmt.WrapText = True
        repoActAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoActAmt)



        gv.AllowAddNewRow = False
        ' gv.AllowDeleteRow = False
        gv.AllowColumnChooser = True
        ' gv.ShowGroupPanel = False
        gv.AllowColumnReorder = True
        gv.AllowRowReorder = True
        gv.EnableSorting = False
        gv.MasterTemplate.ShowRowHeaderColumn = False
        gv.MasterTemplate.ShowColumnHeaders = True
        gv.EnableAlternatingRowColor = True
        '   gv.EnableFiltering = True
        '    gv.ShowFilteringRow = True
        ' gv.TableElement.TableHeaderHeight = 40
    End Sub
   
    Private Sub fndInvoiceNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndInvoiceNo._MYValidating
        If isInvoicefinder = 1 Then

            Dim whrCls As String = String.Empty
            whrCls = "2=2"
            If Not clsMccMaster.isCurrentUserHO() Then
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    whrCls = " and loc_code in(" & objCommonVar.strCurrUserLocations & ")"
                End If
            End If
            fndInvoiceNo.Value = clsMilkPurchaseReturnHead.getInvoiceFinder(whrCls, fndInvoiceNo.Value, isButtonClicked)
            loadInvoiceData(fndInvoiceNo.Value, NavigatorType.Current)
        End If
    End Sub

    Public StrDocNo As String = Nothing
    Private Sub FrmMilkPurchaseReturn_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        reset()
        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D To Delete ")
        'ButtonToolTip.SetToolTip(btnClose, "Press Alt+C To Close the Window")
        'ButtonToolTip.SetToolTip(btnReset, "Press Alt+N For New")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P to Post the Transaction")

        'If clsCommon.myLen(Me.Tag) > 0 Then
        '    loadInvoiceData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        'End If

        'If TankerFromMaster = 1 Then
        '    lblMonth.Visible = True
        '    txtMonth.Visible = True
        'End If
        'AllowDateChanged = True
        SetUserMgmtNew()
        
        loadBlankGrid()
        '===================added by preeti gupta[22/12/2016]
        If clsCommon.myLen(StrDocNo) > 0 Then
            loadData(StrDocNo, NavigatorType.Current)
        End If
        If clsCommon.myLen(MyBase.Tag) > 0 Then
            loadData(MyBase.Tag, NavigatorType.Current)
        End If
        Panel3.Enabled = False
        AllowJobWorkonGateEntryBulkProc = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowJobWorkonGateEntryBulkProc, clsFixedParameterCode.AllowJobWorkonGateEntryBulkProc, Nothing))
        If AllowJobWorkonGateEntryBulkProc = 1 Then
            Panel3.Visible = True
        Else
            Panel3.Visible = False
        End If
    End Sub

    Sub SaveData(ByVal isPost As Boolean)
        Try
            Dim trans As SqlTransaction = Nothing
            obj = New clsMilkPurchaseReturnHead
            If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                obj.isNewEntry = True
            Else
                obj.isNewEntry = False
            End If
            trans = clsDBFuncationality.GetTransactin()
            Dim dt As Date = clsCommon.GETSERVERDATE(trans, "dd/MMM/yyyy hh:mm:ss tt")
            Dim PIDate As Date = dtpDocReturnDate.Value
            If obj.isNewEntry Then
                'Dim isJobWork As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select   isnull(IsAgainstJobWork,0) from tspl_Bulk_milk_purchase_Invoice_head " & _
                '"left outer join tspl_Bulk_milk_purchase_Invoice_Detail on tspl_Bulk_milk_purchase_Invoice_head.DOC_NO=.tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO  " & _
                '"left outer join TSPL_Bulk_MILK_SRN on tspl_Bulk_milk_purchase_Invoice_Detail.srn_no=TSPL_Bulk_MILK_SRN.SRN_NO left outer join tspl_gate_entry_details on " & _
                '"TSPL_Bulk_MILK_SRN.Gate_Entry_No=tspl_gate_entry_details.gate_entry_no   where tspl_Bulk_milk_purchase_Invoice_head.DOC_NO='" & fndInvoiceNo.Value & "' and SL_NO=1 " & _
                '"order by DOC_DATE desc", trans))
                   
                If chkJobWork.Checked Then
                    obj.Pur_Return_No = clsERPFuncationality.GetNextCode(trans, PIDate, clsDocType.BulkMilkPurchaseReturn, clsDocTransactionType.BulkProcJobWorkOutward, txtSubLocation.Value)
                Else
                    obj.Pur_Return_No = clsERPFuncationality.GetNextCode(trans, PIDate, clsDocType.BulkMilkPurchaseReturn, clsDocTransactionType.NA, txtLocation.Text)
                End If
                If clsCommon.myLen(obj.Pur_Return_No) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Error In Document No Genertion", Me.Text)
                    Exit Sub
                End If

            Else
                obj.Pur_Return_No = clsCommon.myCstr(fndDocNoReturn.Value)
            End If
            obj.IsAgainstJobWork = IIf(chkJobWork.Checked, 1, 0)
            obj.Joblocation_Code = txtSubLocation.Value
            fndDocNoReturn.Value = obj.Pur_Return_No
            obj.Pur_Return_Date = clsCommon.GetPrintDate(dtpDocReturnDate.Value, "dd/MMM/yyyy")
            obj.Invoice_No = clsCommon.myCstr(fndInvoiceNo.Value)
            obj.Invoice_Date = clsCommon.GetPrintDate(dtpDocDate.Value, "dd/MMM/yyyy")
            obj.vendor_code = clsCommon.myCstr(txtVendor.Text)
            obj.Loc_Code = clsCommon.myCstr(txtLocation.Text)
            obj.Vendor_Invoice_No = clsCommon.myCstr(txtVendorInvoiceNo.Text)
            obj.SRN_From_Date = clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy hh:mm:ss tt")
            obj.SRN_TO_Date = clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy hh:mm:ss tt")
            obj.Total_FAT_KG = clsCommon.myCdbl(txtTotalFatKg.Text)
            obj.Total_SNF_KG = clsCommon.myCdbl(txtTotalSNFKg.Text)
            obj.Total_QTY = clsCommon.myCdbl(txtTotalQty.Text)
            obj.Total_AMT = clsCommon.myCdbl(txtTotalAmt.Text)
            obj.RoundOffAmount = clsCommon.myCdbl(txtRoundOffAmt.Text)
            obj.isSRNTradeInvoice = IIf(chkSRNTrade.Checked, 1, 0)
            If Not isPost Then
                obj.isPosted = 0
                ' Else
                'obj.isPosted = 1
                'obj.Posting_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
            End If
            obj.Modified_By = objCommonVar.CurrentUserCode
            obj.Modified_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
            obj.Comp_Code = objCommonVar.CurrentCompanyCode
            If obj.isNewEntry Then
                obj.Created_By = objCommonVar.CurrentUserCode
                obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
            End If
            Dim i As Integer = 0
            Dim objDetail As New clsMilkPurchaseReturnDetail
            obj.arrDetail = New List(Of clsMilkPurchaseReturnDetail)
            For i = 0 To gv.Rows.Count - 1
                objDetail = New clsMilkPurchaseReturnDetail()
                objDetail.Pur_Return_No = clsCommon.myCstr(obj.Pur_Return_No)
                objDetail.Invoice_No = clsCommon.myCstr(obj.Invoice_No)
                objDetail.SL_NO = clsCommon.myCstr(gv.Rows(i).Cells(colSlNo).Value)
                objDetail.SRN_NO = clsCommon.myCstr(gv.Rows(i).Cells(colSRNNo).Value)
                objDetail.SRN_Date = clsCommon.GetPrintDate(gv.Rows(i).Cells(colSRNDATe).Value, "dd/MMM/yyyy hh:mm:ss tt")
                objDetail.Item_Code = clsCommon.myCstr(gv.Rows(i).Cells(colItemCode).Value)
                objDetail.Item_Desc = clsCommon.myCstr(gv.Rows(i).Cells(colItemDesc).Value)
                objDetail.UOM = clsCommon.myCstr(gv.Rows(i).Cells(colUOM).Value)
                objDetail.Gross_Weight = clsCommon.myCdbl(gv.Rows(i).Cells(colGrossWeight).Value)
                objDetail.Tare_Weight = clsCommon.myCdbl(gv.Rows(i).Cells(colTareWeight).Value)
                objDetail.Net_Weight = clsCommon.myCdbl(gv.Rows(i).Cells(colNetWeight).Value)
                objDetail.Invoice_Qty = clsCommon.myCdbl(gv.Rows(i).Cells(colQty).Value)
                objDetail.fat_per = clsCommon.myCdbl(gv.Rows(i).Cells(colFat).Value)
                objDetail.fat_KG = clsCommon.myCdbl(gv.Rows(i).Cells(colFatKG).Value)
                objDetail.fat_Rate = clsCommon.myCdbl(gv.Rows(i).Cells(colFatRate).Value)
                objDetail.snf_Per = clsCommon.myCdbl(gv.Rows(i).Cells(colSNF).Value)
                objDetail.SNF_KG = clsCommon.myCdbl(gv.Rows(i).Cells(colSNFKG).Value)
                objDetail.SNF_Rate = clsCommon.myCdbl(gv.Rows(i).Cells(colSNFRate).Value)
                objDetail.Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colAmt).Value)
                objDetail.Deduction = clsCommon.myCdbl(gv.Rows(i).Cells(colDeduc).Value)
                objDetail.Incentive = clsCommon.myCdbl(gv.Rows(i).Cells(colIncen).Value)
                objDetail.Special_Deduction = clsCommon.myCdbl(gv.Rows(i).Cells(colSpecialDeduc).Value)
                objDetail.Actual_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colActAmt).Value)
                objDetail.price_code = clsCommon.myCstr(gv.Rows(i).Cells(colPriceCode).Value)
                objDetail.NetRate = clsCommon.myCdbl(gv.Rows(i).Cells(colNetRate).Value)
                objDetail.CHAMBER_DESC = clsCommon.myCstr(gv.Rows(i).Cells(colChamberDesc).Value)
                obj.arrDetail.Add(objDetail)
            Next
            If clsMilkPurchaseReturnHead.saveData(obj, trans) Then
                trans.Commit()
                If Not isPost Then
                    If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                        clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    Else
                        clsCommon.MyMessageBoxShow(Me, "Data Updated Successfully", Me.Text)
                    End If
                End If
                loadData(obj.Pur_Return_No, NavigatorType.Current)
                btnSave.Text = "Update"  'PANAND'
                fndDocNoReturn.MyReadOnly = True
                btnDelete.Enabled = True
                btnPost.Enabled = True
                btnPrint.Enabled = True
                Exit Sub
            End If
            clsCommon.MyMessageBoxShow(Me, "Data Not Saved ", Me.Text)
            btnSave.Text = "Update" 'PANAND
            btnDelete.Enabled = False
            btnPost.Enabled = False
            btnPrint.Enabled = False
            fndDocNoReturn.MyReadOnly = False
            trans.Rollback()
        Catch ex As Exception
            If isPost Then
                Throw New Exception(ex.Message)
            Else
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End If

        End Try
    End Sub

    Public Function isVendorInvoiceNo(ByVal strVendor As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = " select isvendorInvoiceNo from tspl_vendor_master where Vendor_Code='" & strVendor & "'"
        Dim rValue As Double = 0
        rValue = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        If rValue = 1 Then
            Return True
        Else
            Return False
        End If
    End Function

    Sub loadData(ByVal str As String, ByVal navtype As NavigatorType)
        obj = clsMilkPurchaseReturnHead.getData(str, navtype)
        If obj IsNot Nothing Then
            reset()
            If obj.isPosted = 0 Then
                isSRNselected = True
            Else
                isSRNselected = False
            End If
            isLoadData = True
            'fndLocation.Enabled = False
            chkJobWork.Checked = IIf(obj.IsAgainstJobWork = 1, True, False)
            txtSubLocation.Value = obj.Joblocation_Code
            If clsCommon.myLen(txtSubLocation.Value) > 0 Then
                lblSubLocation.Text = clsLocation.GetName(txtSubLocation.Value, Nothing)
            End If
            fndDocNoReturn.Value = obj.Pur_Return_No
            dtpDocReturnDate.Value = obj.Pur_Return_Date
            fndInvoiceNo.Value = obj.Invoice_No
            dtpDocDate.Value = obj.Invoice_Date
            txtVendor.Text = obj.vendor_code
            lblVendorName.Text = clsVendorMaster.GetName(obj.vendor_code, Nothing)
            txtLocation.Text = obj.Loc_Code
            lblLocationName.Text = clsLocation.GetName(obj.Loc_Code, Nothing)
            dtpFromDate.Value = obj.SRN_From_Date
            dtpToDate.Value = obj.SRN_TO_Date
            txtTotalFatKg.Text = clsCommon.myFormat(MyMath.RoundDown(obj.Total_FAT_KG, 3), False, True, False, 3, True)
            txtTotalSNFKg.Text = clsCommon.myFormat(MyMath.RoundDown(obj.Total_SNF_KG, 3), False, True, False, 3, True)
            txtTotalQty.Text = clsCommon.myFormat(obj.Total_QTY)
            txtRoundOffAmt.Text = clsCommon.myFormat(obj.RoundOffAmount)
            txtTotalAmt.Text = clsCommon.myFormat(obj.Total_AMT)
            txtVendorInvoiceNo.Text = obj.Vendor_Invoice_No
            loadBlankGrid()
            If obj.isSRNTradeInvoice = 1 Then
                chkSRNTrade.Checked = True
            Else
                chkSRNTrade.Checked = False
            End If
            chkSRNTrade.Enabled = False
            Dim SRNs As String = ""

            If obj.arrDetail IsNot Nothing Then
                Dim arr As New List(Of String)
                For i As Integer = 0 To obj.arrDetail.Count - 1
                    gv.Rows.AddNew()
                    gv.Rows(i).Cells(colSlNo).Value = obj.arrDetail(i).SL_NO
                    gv.Rows(i).Cells(colSRNNo).Value = obj.arrDetail(i).SRN_NO
                    If Not arr.Contains(obj.arrDetail(i).SRN_NO) Then
                        arr.Add(obj.arrDetail(i).SRN_NO)
                        SRNs = SRNs & obj.arrDetail(i).SRN_NO
                    End If
                    If i <> obj.arrDetail.Count - 1 Then
                        SRNs = SRNs & ", "
                    End If
                    gv.Rows(i).Cells(colSRNDATe).Value = obj.arrDetail(i).SRN_Date
                    gv.Rows(i).Cells(colItemCode).Value = obj.arrDetail(i).Item_Code
                    gv.Rows(i).Cells(colItemDesc).Value = obj.arrDetail(i).Item_Desc
                    gv.Rows(i).Cells(colHSN).Value = clsItemMaster.GetItemHSNCode(obj.arrDetail(i).Item_Code, Nothing)
                    gv.Rows(i).Cells(colUOM).Value = obj.arrDetail(i).UOM
                    gv.Rows(i).Cells(colGrossWeight).Value = clsCommon.myFormat(obj.arrDetail(i).Gross_Weight)
                    gv.Rows(i).Cells(colTareWeight).Value = clsCommon.myFormat(obj.arrDetail(i).Tare_Weight)
                    gv.Rows(i).Cells(colNetWeight).Value = clsCommon.myFormat(obj.arrDetail(i).Net_Weight)
                    gv.Rows(i).Cells(colQty).Value = clsCommon.myFormat(obj.arrDetail(i).Invoice_Qty)
                    gv.Rows(i).Cells(colFat).Value = clsCommon.myFormat(obj.arrDetail(i).fat_per)
                    gv.Rows(i).Cells(colFatKG).Value = clsCommon.myFormat(obj.arrDetail(i).fat_KG, False, True, False, 3, True)
                    gv.Rows(i).Cells(colFatRate).Value = clsCommon.myFormat(obj.arrDetail(i).fat_Rate)
                    gv.Rows(i).Cells(colSNF).Value = clsCommon.myFormat(obj.arrDetail(i).snf_Per)
                    gv.Rows(i).Cells(colSNFKG).Value = clsCommon.myFormat(obj.arrDetail(i).SNF_KG, False, True, False, 3, True)
                    gv.Rows(i).Cells(colSNFRate).Value = clsCommon.myFormat(obj.arrDetail(i).SNF_Rate)
                    gv.Rows(i).Cells(colAmt).Value = clsCommon.myFormat(obj.arrDetail(i).Amount)
                    gv.Rows(i).Cells(colDeduc).Value = clsCommon.myFormat(obj.arrDetail(i).Deduction)
                    gv.Rows(i).Cells(colIncen).Value = clsCommon.myFormat(obj.arrDetail(i).Incentive)
                    gv.Rows(i).Cells(colSpecialDeduc).Value = clsCommon.myFormat(obj.arrDetail(i).Special_Deduction)
                    gv.Rows(i).Cells(colActAmt).Value = clsCommon.myFormat(obj.arrDetail(i).Actual_Amount)
                    gv.Rows(i).Cells(colPriceCode).Value = obj.arrDetail(i).price_code
                    gv.Rows(i).Cells(colNetRate).Value = clsCommon.myFormat(obj.arrDetail(i).NetRate)
                    gv.Rows(i).Cells(colTnkrNo).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select tanker_no from TSPL_Bulk_MILK_SRN where SRN_NO='" & obj.arrDetail.Item(i).SRN_NO & "' "))    '' RICHA AGARWAL IIf(chkSRNTrade.Checked, "NA", clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select tanker_no from TSPL_Bulk_MILK_SRN where SRN_NO='" & obj.arrDetail.Item(i).SRN_NO & "' ")))
                    gv.Rows(i).Cells(colChamberDesc).Value = obj.arrDetail(i).CHAMBER_DESC
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable("select TSPL_Bulk_MILK_SRN.SRN_NO, (TSPL_Bulk_MILK_SRN.Net_Weight-coalesce(SUM(tdl.invoice_qty),0)) as pending_qty from TSPL_Bulk_MILK_SRN left join ( select invoice_qty,SRN_NO  from Tspl_bulk_milk_purchase_invoice_detail left outer join Tspl_bulk_milk_purchase_invoice_head on Tspl_bulk_milk_purchase_invoice_head.DOC_NO=tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO  where Tspl_bulk_milk_purchase_invoice_head.isPosted=1) as  tdl  on TSPL_Bulk_MILK_SRN.SRN_NO=tdl.srn_no  where TSPL_Bulk_MILK_SRN.isPosted=1 and TSPL_Bulk_MILK_SRN.SRN_NO='" & obj.arrDetail(i).SRN_NO & "'  group by TSPL_Bulk_MILK_SRN.SRN_NO ,TSPL_Bulk_MILK_SRN.Net_Weight	")
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        gv.Rows(i).Cells(colPendingQty).Value = clsCommon.myFormat(clsCommon.myCdbl(dt.Rows(0)("pending_qty")))
                    End If

                Next
                txtSRNNo.Text = SRNs
            End If
            btnSave.Text = "Update"
            btnDelete.Enabled = True
            btnPrint.Enabled = True
            If obj.isPosted = 1 Then
                btnSave.Enabled = False
                btnPost.Enabled = False
                btnDelete.Enabled = False
                lblPending.Status = ERPTransactionStatus.Approved
            Else
                btnSave.Enabled = True
                btnPost.Enabled = True
                btnDelete.Enabled = True
                lblPending.Status = ERPTransactionStatus.Pending
            End If
        End If
        isLoadData = False
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If allowToSave() Then SaveData(False)
    End Sub
    Function allowToSave() As Boolean
        Try

            If AllowFutureDateTransaction(dtpDocReturnDate.Value, Nothing) = False Then
                dtpDocReturnDate.Focus()
                Return False
            End If
            If clsCommon.myLen(fndInvoiceNo.Value) <= 0 Then
                Throw New Exception("Please select Invoice No")
            End If

            If clsCommon.myLen(txtVendor.Text) <= 0 Then
                Throw New Exception("Vendor Name Can't left blank")
            End If

            If clsCommon.myLen(txtVendorInvoiceNo.Text) <= 0 AndAlso isVendorInvoiceNo(txtVendor.Text, Nothing) Then
                Throw New Exception("Vendor invoice no Can't left blank")
            End If
            If gv.Rows.Count <= 0 Then
                Throw New Exception(" Atleast one SRN required")
            End If
            'Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return True
    End Function

    Private Sub fndDocNoReturn__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles fndDocNoReturn._MYNavigator
        loadData(fndDocNoReturn.Value, NavType)
        isInvoicefinder = 0
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click

        Reset()
        isInvoicefinder = 1
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If myMessages.deleteConfirm() Then
            deleteData()
        End If

    End Sub

    Sub deleteData()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(fndDocNoReturn.Value) > 0 Then

                If clsMilkPurchaseReturnHead.deleteData(fndDocNoReturn.Value, trans) Then
                    myMessages.delete()
                    trans.Commit()
                    Reset()
                Else
                    clsCommon.MyMessageBoxShow(Me, "Can't delete the record", Me.Text)
                    trans.Rollback()
                End If
            Else

                clsCommon.MyMessageBoxShow(Me, "Please Select a document to delete", Me.Text)
                trans.Rollback()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            trans.Rollback()
        End Try
    End Sub

    Sub reset()
        txtSubLocation.Value = ""
        lblSubLocation.Text = ""
        chkJobWork.Checked = False
        chkSRNTrade.Enabled = True
        'TxtVendorUpdate.Visible = False
        ' btnUpdateVendor.Visible = False
        'TxtVendorUpdate.Value = ""
        txtLocation.Text = ""
        lblLocationName.Text = ""
        fndDocNoReturn.Value = ""
        fndInvoiceNo.Value = ""
        fndDocNoReturn.MyReadOnly = False
        Dim dt As Date = clsCommon.GETSERVERDATE()
        dtpDocReturnDate.Value = dt
        dtpDocDate.Value = Nothing
        txtVendor.Text = ""
        'fndVendor.Enabled = True
        'fndVendor.MyReadOnly = False
        lblVendorName.Text = ""

        If TankerFromMaster = 0 Then
            dtpFromDate.Value = DateAdd(DateInterval.Day, -10, dt)
            dtpToDate.Value = dt
        Else
            txtMonth.Value = clsCommon.GETSERVERDATE
            dtpFromDate.Value = clsCommon.GetDateWithStartTime(clsCommon.GETSERVERDATE())
            dtpToDate.Value = clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE)
        End If

        dtpFromDate.Enabled = True
        dtpToDate.Enabled = True
        txtSRNNo.Text = ""
        txtTotalFatKg.Text = ""
        txtTotalSNFKg.Text = ""
        txtTotalQty.Text = ""
        txtTotalAmt.Text = ""
        loadBlankGrid()
        lblPending.Status = ERPTransactionStatus.Pending
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = False
        btnPost.Enabled = False
        btnPrint.Enabled = False
        'btnReverse.Visible = False
        'fndLocation.Enabled = True
        txtVendorInvoiceNo.Text = ""
        txtRoundOffAmt.Text = ""
        chkSRNTrade.Checked = False
        isInvoicefinder = 1
        FindAndRestoreGridLayout(Me)
        FindAndSetTabStopFalse(Me)

        Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, Nothing)

        If DateTime = "1" Then
            dtpFromDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
            dtpFromDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Else
            dtpFromDate.CustomFormat = "dd/MM/yyyy"
            dtpToDate.CustomFormat = "dd/MM/yyyy"

        End If
        '==========================================================
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Try
            If clsCommon.myLen(fndDocNoReturn.Value) > 0 Then
                postData()
            Else
                Throw New Exception("Please Select Document No to Post.")
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
       

    End Sub

    Sub postData()
        Try
            ' Dim qry As String = ""
            Dim msg As String = ""
            'Dim dt As DataTable = Nothing

            If (myMessages.postConfirm()) Then
                If Not allowToSave() Then
                    Exit Sub
                End If
                'If btnSave.Text = "Save" Then
                '    SaveData(True)
                'End If


                If (clsMilkPurchaseReturnHead.PostData(fndDocNoReturn.Value, MyBase.Form_ID)) Then
                    '                 
                    msg = "Successfully Posted"

                End If
                common.clsCommon.MyMessageBoxShow(Me, msg, Me.Text)
                loadData(fndDocNoReturn.Value, NavigatorType.Current)
              
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    ' Ticket : TEC/29/10/18-000351 By Sanjay
    Private Sub btnShowInventory_Click(sender As Object, e As EventArgs) Handles btnShowInventory.Click
        clsOpenInventory.ShowInventoryDatails(fndDocNoReturn.Value)
    End Sub
End Class
