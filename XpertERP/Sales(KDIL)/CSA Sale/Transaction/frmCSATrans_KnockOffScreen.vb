Imports common
Imports System.Data.SqlClient
Imports System.IO

Public Class FrmCSATrans_KnockOffScreen
    Inherits FrmMainTranScreen

#Region "variables"
    Dim ShowReturnType As Boolean = False
    Public strDocCode As String = Nothing
    Public strDocDate As DateTime = Nothing
    Public strCustCode As String = Nothing
    Public strPlantLoc_Code As String = Nothing
    Public strCSAloc_code As String = Nothing
    Public colPackSize As Decimal = Nothing
    Public colStckTransferrate As Decimal = Nothing
    Public colStckTransferAmount As Decimal = Nothing
    Public colitemcode As String = Nothing
    Public colqty As Decimal = Nothing
    Public colFOC As String = Nothing
    Public colItemUOM As String = Nothing
    Public colIsScheme As String = Nothing
    Public ShowDocumentCancel As Boolean = Nothing
    Public ComeFromImport As Boolean = False

    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChanged As Boolean = False
    Public TransferManual_KnockOFF As Boolean = False

    '=============transfer grid======================
    Const colSelect As String = "Select"
    Const coltransAltQty As String = "TransAltQty"
    Const coltranslineno As String = "lineno"
    Const colTranscode As String = "transcode"
    Const coltransitemcode As String = "itemcode"
    Const coltransiname As String = "iname"
    Const coltransuom As String = "transuom"
    Const coltrans_actual_qty As String = "transqty"
    Const coltransqty As String = "qty"
    Const coltransbal_qty As String = "TransBal_Qty"
    Const coltrans_sale_uom As String = "transaleuom"
    Const coltransrate As String = "transrate"
    Const colTransConvFactor As String = "Trans_Conv_Fatcor"
    Const colTransFOC As String = "TransFOC"
    Const colTransfer_Line_No As String = "TransferLineNo"
    Const colManualOrgBal As String = "ManualOrgBal"
    '===============================================

    Public GV_ARR As New List(Of clsCSAStockTransferDetail)
#End Region

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmCSASaleInvoice)
        'If Not (MyBase.isReadFlag) Then
        '    Throw New Exception("Permission Denied")
        'End If
        'btnsave.Visible = MyBase.isModifyFlag
    End Sub

    Public Sub LoadTransGrid()
        gv_trans.Columns.Clear()

        Dim repocheckbox As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repocheckbox.Width = 50
        repocheckbox.HeaderText = "   "
        repocheckbox.Name = colSelect
        repocheckbox.ThreeState = False
        repocheckbox.FormatString = ""
        repocheckbox.ReadOnly = Not TransferManual_KnockOFF
        gv_trans.MasterTemplate.Columns.Add(repocheckbox)

        Dim repolineno As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repolineno.FormatString = ""
        repolineno.Width = 70
        repolineno.ReadOnly = True
        repolineno.Name = coltranslineno
        repolineno.HeaderText = "S.No."
        gv_trans.MasterTemplate.Columns.Add(repolineno)

        repolineno = New GridViewTextBoxColumn()
        repolineno.FormatString = ""
        repolineno.Width = 70
        repolineno.ReadOnly = True
        repolineno.Name = colTransfer_Line_No
        repolineno.HeaderText = "Transfer Line No"
        repolineno.IsVisible = False
        repolineno.VisibleInColumnChooser = False
        gv_trans.MasterTemplate.Columns.Add(repolineno)

        Dim repolineno1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repolineno1.FormatString = ""
        repolineno1.Width = 90
        repolineno1.ReadOnly = True
        repolineno1.Name = colTranscode
        repolineno1.HeaderText = "Transfer Code"
        gv_trans.MasterTemplate.Columns.Add(repolineno1)

        Dim repolineno2 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repolineno2.FormatString = ""
        repolineno2.Width = 90
        repolineno2.ReadOnly = True
        repolineno2.Name = coltransitemcode
        repolineno2.HeaderText = "Item Code"
        gv_trans.MasterTemplate.Columns.Add(repolineno2)

        Dim repolineno3 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repolineno3.FormatString = ""
        repolineno3.Width = 110
        repolineno3.ReadOnly = True
        repolineno3.Name = coltransiname
        repolineno3.HeaderText = "Description"
        gv_trans.MasterTemplate.Columns.Add(repolineno3)

        Dim repolineno41 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repolineno41.FormatString = ""
        repolineno41.Width = 70
        repolineno41.ReadOnly = True
        repolineno41.Name = coltrans_actual_qty
        repolineno41.HeaderText = "Transfer Qty"
        gv_trans.MasterTemplate.Columns.Add(repolineno41)

        Dim repolineno42 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repolineno42.FormatString = ""
        repolineno42.Width = 70
        repolineno42.ReadOnly = True
        repolineno42.Name = coltransuom
        repolineno42.HeaderText = "Transfer UOM"
        gv_trans.MasterTemplate.Columns.Add(repolineno42)

        Dim repolineno421 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repolineno421.FormatString = ""
        repolineno421.Width = 70
        repolineno421.ReadOnly = True
        repolineno421.Name = colTransConvFactor
        repolineno421.IsVisible = False
        repolineno421.DecimalPlaces = 2
        repolineno421.HeaderText = "Conversion Factor"
        gv_trans.MasterTemplate.Columns.Add(repolineno421)

        Dim repoIsSchmItem2 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIsSchmItem2.FormatString = ""
        repoIsSchmItem2.HeaderText = "Is FOC"
        repoIsSchmItem2.Name = colTransFOC
        repoIsSchmItem2.Width = 50
        repoIsSchmItem2.ReadOnly = True
        gv_trans.MasterTemplate.Columns.Add(repoIsSchmItem2)

        Dim repolineno4 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repolineno4.FormatString = ""
        repolineno4.Width = 70
        repolineno4.ReadOnly = Not TransferManual_KnockOFF ''when manual knock-off then editable
        repolineno4.Name = coltransqty
        repolineno4.HeaderText = "Sale Quantity"
        gv_trans.MasterTemplate.Columns.Add(repolineno4)

        Dim repolineno44 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repolineno44.FormatString = ""
        repolineno44.Width = 70
        repolineno44.ReadOnly = True
        repolineno44.Name = coltrans_sale_uom
        repolineno44.HeaderText = "Sale UOM"
        gv_trans.MasterTemplate.Columns.Add(repolineno44)

        Dim repolineno45 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repolineno45.FormatString = ""
        repolineno45.Width = 70
        repolineno45.ReadOnly = True
        repolineno45.Name = coltransbal_qty
        repolineno45.HeaderText = "Balance Qty"
        repolineno45.IsVisible = False
        gv_trans.MasterTemplate.Columns.Add(repolineno45)

        Dim repolineno457 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repolineno457.FormatString = ""
        repolineno457.Width = 70
        repolineno457.ReadOnly = True
        repolineno457.Name = coltransAltQty
        repolineno457.HeaderText = "Alt. Qty"
        gv_trans.MasterTemplate.Columns.Add(repolineno457)

        Dim repolineno5 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repolineno5.FormatString = ""
        repolineno5.Width = 70
        repolineno5.ReadOnly = True
        repolineno5.Name = coltransrate
        repolineno5.HeaderText = "Stock Transfer Rate"
        gv_trans.MasterTemplate.Columns.Add(repolineno5)


        repolineno45 = New GridViewTextBoxColumn()
        repolineno45.FormatString = ""
        repolineno45.Width = 70
        repolineno45.ReadOnly = True
        repolineno45.Name = colManualOrgBal
        repolineno45.HeaderText = "Manual Balance Qty"
        repolineno45.IsVisible = False
        gv_trans.MasterTemplate.Columns.Add(repolineno45)

        gv_trans.AllowAddNewRow = False
        gv_trans.ShowGroupPanel = False
        gv_trans.AllowColumnReorder = True
        gv_trans.AllowRowReorder = False
        gv_trans.EnableSorting = False
        gv_trans.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv_trans.MasterTemplate.ShowRowHeaderColumn = False
        gv_trans.TableElement.TableHeaderHeight = 40
    End Sub

    Private Sub FrmCSATrans_KnockOffScreen_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
                btnsave.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
                Me.Close()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FrmCSATrans_KnockOffScreen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '' against ticket :BM00000009263
        TransferManual_KnockOFF = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.PickManual_CSATransfer_OnCSASalePatti, clsFixedParameterCode.PickManual_CSATransfer_OnCSASalePatti, Nothing)) = "1", True, False))
        ''====================================================

        ShowReturnType = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowCSAReturnTypeOnScreen, clsFixedParameterCode.ShowCSAReturnTypeOnScreen, Nothing)) = "1", True, False))
        ShowDocumentCancel = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CSADocumentCancel, clsFixedParameterCode.CSADocumentCancel, Nothing)) = 1, True, False)
        RadGroupBox2.Visible = True
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        SaveData()
        Me.Close()
    End Sub

    Public Sub FillTransfergrid(ByVal Line_No As String, ByVal Arr As List(Of clsCSAStockTransferDetail))
        Try
            isInsideLoadData = True

            gv_trans.Rows.Clear()

            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                Dim qqty As Decimal = clsCommon.myCdbl(colqty)
                Dim actualqty As Decimal = 0
                txtsaleqty.Text = clsCommon.myCstr(qqty)

                For Each obj As clsCSAStockTransferDetail In Arr
                    gv_trans.Rows.AddNew()

                    gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(colSelect).Value = True
                    gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(coltranslineno).Value = obj.lineno
                    gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(colTranscode).Value = obj.transcode
                    gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(coltransitemcode).Value = obj.icode
                    gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(coltransiname).Value = clsItemMaster.GetItemName(obj.icode, Nothing)
                    gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(coltransqty).Value = obj.qty
                    actualqty = actualqty + obj.qty
                    gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(coltransrate).Value = obj.rate
                    gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(coltrans_actual_qty).Value = obj.act_qty
                    gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(coltransuom).Value = obj.uom
                    gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(coltransbal_qty).Value = obj.bal_qty
                    gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(coltrans_sale_uom).Value = obj.sale_uom
                    gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(colTransConvFactor).Value = obj.conv_factor
                    gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(coltransAltQty).Value = obj.alt_qty
                    gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(colTransFOC).Value = obj.FOC

                    gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(colTransfer_Line_No).Value = obj.Transfer_Line_No

                    gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(colManualOrgBal).Value = GetBalance_of_Transfer(Line_No, gv_trans.Rows.Count - 1)
                Next

                If actualqty > qqty Then
                    FillTransMoreItems(True, False, Line_No)
                ElseIf actualqty < qqty Then
                    FillTransMoreItems(False, False, Line_No)
                End If
            Else
                FillTransMoreItems(False, True, Line_No)
            End If ''end gv_arr

            'Dim qry As String = "select Transfer_Line_No,line_no,Against_Transfer_Code,qty,transfer_rate,item_code,Transfer_Qty,Transfer_UOM,Balance_Qty,Sale_UOM,Conv_Factor,Alt_Qty,FOC from CSA_SALE_TRANSFER where document_code='" + strDocCode + "' and item_code='" + colitemcode + "' and line_no='" + Line_No + "' and sale_uom='" + colItemUOM + "'"
            'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            '    Dim qqty As Decimal = clsCommon.myCdbl(colqty)
            '    Dim actualqty As Decimal = 0
            '    txtsaleqty.Text = clsCommon.myCstr(qqty)

            '    For Each dr As DataRow In dt.Rows
            '        gv_trans.Rows.AddNew()

            '        gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(colSelect).Value = True
            '        gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(coltranslineno).Value = clsCommon.myCstr(dr("line_no"))
            '        gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(colTranscode).Value = clsCommon.myCstr(dr("Against_Transfer_Code"))
            '        gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(coltransitemcode).Value = clsCommon.myCstr(dr("item_code"))
            '        gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(coltransiname).Value = clsItemMaster.GetItemName(clsCommon.myCstr(dr("item_code")), Nothing)
            '        gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(coltransqty).Value = clsCommon.myCstr(dr("qty"))
            '        actualqty = actualqty + clsCommon.myCdbl(dr("qty"))
            '        gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(coltransrate).Value = clsCommon.myCstr(dr("transfer_rate"))
            '        gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(coltrans_actual_qty).Value = clsCommon.myCdbl(dr("Transfer_Qty"))
            '        gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(coltransuom).Value = clsCommon.myCstr(dr("Transfer_UOM"))
            '        gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(coltransbal_qty).Value = clsCommon.myCdbl(dr("Balance_Qty"))
            '        gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(coltrans_sale_uom).Value = clsCommon.myCstr(dr("Sale_UOM"))
            '        gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(colTransConvFactor).Value = clsCommon.myCdbl(dr("Conv_Factor"))
            '        gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(coltransAltQty).Value = clsCommon.myCdbl(dr("Alt_Qty"))
            '        gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(colTransFOC).Value = clsCommon.myCstr(dr("FOC"))

            '        gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(colTransfer_Line_No).Value = clsCommon.myCstr(dr("Transfer_Line_No"))

            '        gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(colManualOrgBal).Value = ""
            '    Next

            '    If actualqty > qqty Then
            '        FillTransMoreItems(True, False, Line_No)
            '    ElseIf actualqty < qqty Then
            '        FillTransMoreItems(False, False, Line_No)
            '    End If
            'Else
            '    FillTransMoreItems(False, True, Line_No)
            'End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Public Sub FillTransMoreItems(ByVal isMore As Boolean, ByVal IsNothing As Boolean, ByVal Line_No As String)
        Dim qry As String = ""
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)

        Dim strReturnCond As String = ""
        Try
            isInsideLoadData = True

            If ShowReturnType Then
                strReturnCond = " and tspl_sd_sale_return_head.return_type not in ('D','S') "
            End If

            gv_trans.Rows.Clear()
            '====BM00000008235
            '======BM00000008575 (replace to_locatio_code,bill_to_location condition with from_location and csa_plant_location in below query.
            '======now only see stock of transfer location and customer not customer location receive stock. And no change in case of Adjustment data

            'qry = "select final.doc_code,final.transfer_line_no,final.item_code,final.unit_code,final.actual_qty,final.qty-isnull(return1.trans_qty,0) as qty,final.transfer_rate,final.including_tax,final.calc_type,final.FOC from ( " + Environment.NewLine & _
            qry = "select axa.doc_code,axa.transfer_line_no,axa.item_code,axa.unit_code,isnull(axa.qty,0) as actual_qty,(isnull(axa.qty,0)-isnull(axa.trans_qty,0)) as qty,axa.transfer_rate,axa.including_tax,axa.calc_type,isnull(axa.FOC,'N') as FOC from ( " + Environment.NewLine & _
              "select TSPL_CSA_TRANSFER_DETAIL.doc_code,convert(date,TSPL_CSA_TRANSFER_HEAD.transfer_date,103) as transfer_date,TSPL_CSA_TRANSFER_DETAIL.Line_No as transfer_line_no,TSPL_CSA_TRANSFER_DETAIL.item_code,TSPL_CSA_TRANSFER_DETAIL.unit_code,sum(TSPL_CSA_TRANSFER_DETAIL.qty) as qty,aa.trans_qty,TSPL_CSA_TRANSFER_DETAIL.transfer_rate,TSPL_CSA_TRANSFER_DETAIL.including_tax,TSPL_CSA_TRANSFER_DETAIL.calc_type,TSPL_CSA_TRANSFER_DETAIL.FOC from TSPL_CSA_TRANSFER_DETAIL " + Environment.NewLine & _
              " left outer join TSPL_CSA_TRANSFER_HEAD on TSPL_CSA_TRANSFER_HEAD.doc_code=TSPL_CSA_TRANSFER_DETAIL.doc_code " & _
              " left outer join (select transfer.Against_Transfer_Code,transfer.transfer_line_no,transfer.item_code,transfer.FOC,sum(transfer.trans_qty) as trans_qty from (select Against_Transfer_Code,transfer_line_no,item_code,FOC,sum(alt_qty) as trans_qty from TSPL_CSA_SALE_TRANSFER_DETAIL where document_code in (select Document_Code from TSPL_SD_SALE_INVOICE_HEAD where Trans_Type='CSA' and Document_Code <>'" + strDocCode + "' and Customer_Code='" + strCustCode + "' and CSA_PLANT_LOCATION='" + strPlantLoc_Code + "') group by Against_Transfer_Code,item_code,FOC,transfer_line_no " + Environment.NewLine & _
              " union all select Against_Transfer_Code,transfer_line_no,item_code,FOC,sum(alt_qty) as trans_qty from CSA_SALE_TRANSFER where (isnull(document_code,'')='' or document_code in (select Document_Code from TSPL_SD_SALE_INVOICE_HEAD where trans_type='CSA' and Customer_Code='" + strCustCode + "' and CSA_PLANT_LOCATION='" + strPlantLoc_Code + "')) and item_code<>'" + colitemcode + "' group by Against_Transfer_Code,item_code,FOC,transfer_line_no " + Environment.NewLine & _
              " union all select Against_Transfer_Code,transfer_line_no,item_code,FOC,sum(alt_qty) as trans_qty from CSA_SALE_TRANSFER where (isnull(document_code,'')='' or document_code in (select Document_Code from TSPL_SD_SALE_INVOICE_HEAD where trans_type='CSA' and Customer_Code='" + strCustCode + "' and CSA_PLANT_LOCATION='" + strPlantLoc_Code + "')) and line_no<>'" + Line_No + "' group by Against_Transfer_Code,item_code,FOC,transfer_line_no " + Environment.NewLine & _
              " )transfer group by transfer.Against_Transfer_Code,transfer.item_code,transfer.FOC,transfer.transfer_line_no)aa " + Environment.NewLine & _
              " on aa.Against_Transfer_Code=TSPL_CSA_TRANSFER_DETAIL.doc_code and TSPL_CSA_TRANSFER_DETAIL.item_code=aa.item_code and aa.FOC=TSPL_CSA_TRANSFER_DETAIL.FOC and aa.transfer_line_no=TSPL_CSA_TRANSFER_DETAIL.Line_No " + Environment.NewLine & _
              " where TSPL_CSA_TRANSFER_HEAD.Status=1 and TSPL_CSA_TRANSFER_HEAD.From_Location_Code='" + strPlantLoc_Code + "' and TSPL_CSA_TRANSFER_HEAD.Cust_Code='" + strCustCode + "' and convert(date,TSPL_CSA_TRANSFER_HEAD.transfer_date,103) <='" + clsCommon.GetPrintDate(clsCommon.myCDate(strDocDate), "dd/MMM/yyyy") + "' " + Environment.NewLine

            ''======= Parteek added on 01-02-20117
            If ShowDocumentCancel = True Then
                qry += " and tspl_csa_transfer_head.CancelFlag is null "
            End If
            ''============End

            qry += " group by TSPL_CSA_TRANSFER_DETAIL.doc_code,TSPL_CSA_TRANSFER_HEAD.transfer_date,TSPL_CSA_TRANSFER_DETAIL.item_code ,TSPL_CSA_TRANSFER_DETAIL.transfer_rate,TSPL_CSA_TRANSFER_DETAIL.unit_code,TSPL_CSA_TRANSFER_DETAIL.including_tax,TSPL_CSA_TRANSFER_DETAIL.calc_type,aa.trans_qty,TSPL_CSA_TRANSFER_DETAIL.FOC,TSPL_CSA_TRANSFER_DETAIL.line_no "
            ''" where TSPL_CSA_TRANSFER_DETAIL.DOC_CODE in (select DOC_CODE from TSPL_CSA_TRANSFER_HEAD where Status=1 and From_Location_Code='" + strPlantLoc_Code + "' and Cust_Code='" + strCustCode + "') " + Environment.NewLine & _

            ''===================below is data pick from adjustment
            qry += Environment.NewLine + Environment.NewLine + " Union all " + Environment.NewLine & _
                  "select TSPL_ADJUSTMENT_DETAIL.adjustment_no as doc_code,convert(date,TSPL_ADJUSTMENT_HEADER.adjustment_date,103) as transfer_date,TSPL_ADJUSTMENT_DETAIL.adjustment_line_no as transfer_line_no,TSPL_ADJUSTMENT_DETAIL.item_code,TSPL_ADJUSTMENT_DETAIL.unit_code,sum((case when TSPL_ADJUSTMENT_HEADER.trans_type='In' then isnull(TSPL_ADJUSTMENT_DETAIL.item_quantity,0) else (0 - isnull(TSPL_ADJUSTMENT_DETAIL.item_quantity,0)) end)) as qty,aa.trans_qty,(case when TSPL_ADJUSTMENT_DETAIL.unit_cost>0 then TSPL_ADJUSTMENT_DETAIL.unit_cost else TSPL_ADJUSTMENT_DETAIL.mrp end) as transfer_rate,'Yes' as including_tax,'Backward' as calc_type,aa.FOC from TSPL_ADJUSTMENT_DETAIL " + Environment.NewLine & _
                  " left outer join (select transfer.Against_Transfer_Code,transfer.transfer_line_no,transfer.item_code,sum(transfer.trans_qty) as trans_qty,transfer.FOC from (select Against_Transfer_Code,transfer_line_no,item_code,sum(alt_qty) as trans_qty,FOC from TSPL_CSA_SALE_TRANSFER_DETAIL where document_code in (select Document_Code from TSPL_SD_SALE_INVOICE_HEAD where trans_type='CSA' and Document_Code <>'" + strDocCode + "' and Customer_Code='" + strCustCode + "' and Bill_To_Location='" + strCSAloc_code + "') group by Against_Transfer_Code,item_code,FOC,transfer_line_no " + Environment.NewLine & _
                  " union all select Against_Transfer_Code,transfer_line_no,item_code,sum(alt_qty) as trans_qty,FOC from CSA_SALE_TRANSFER where (isnull(document_code,'')='' or document_code in (select Document_Code from TSPL_SD_SALE_INVOICE_HEAD where trans_type='CSA' and Customer_Code='" + strCustCode + "' and Bill_To_Location='" + strCSAloc_code + "')) and item_code<>'" + colitemcode + "' group by Against_Transfer_Code,item_code,FOC,transfer_line_no " + Environment.NewLine & _
                  " union all select Against_Transfer_Code,transfer_line_no,item_code,sum(alt_qty) as trans_qty,FOC from CSA_SALE_TRANSFER where (isnull(document_code,'')='' or document_code in (select Document_Code from TSPL_SD_SALE_INVOICE_HEAD where trans_type='CSA' and Customer_Code='" + strCustCode + "' and Bill_To_Location='" + strCSAloc_code + "')) and line_no<>'" + Line_No + "' group by Against_Transfer_Code,item_code,FOC,transfer_line_no " + Environment.NewLine & _
                  " )transfer group by transfer.Against_Transfer_Code,transfer.item_code,transfer.FOC,transfer.transfer_line_no)aa " + Environment.NewLine & _
                  " on aa.Against_Transfer_Code=TSPL_ADJUSTMENT_DETAIL.adjustment_no and TSPL_ADJUSTMENT_DETAIL.item_code=aa.item_code and aa.transfer_line_no=TSPL_ADJUSTMENT_DETAIL.adjustment_line_no " + Environment.NewLine & _
                  " left outer join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_DETAIL.adjustment_no=TSPL_ADJUSTMENT_HEADER.adjustment_no " + Environment.NewLine & _
                  " where TSPL_ADJUSTMENT_HEADER.posted='Y' and TSPL_ADJUSTMENT_HEADER.loc_code='" + strCSAloc_code + "' and convert(date,TSPL_ADJUSTMENT_HEADER.adjustment_date,103)<='" + clsCommon.GetPrintDate(clsCommon.myCDate(strDocDate), "dd/MMM/yyyy") + "' " + Environment.NewLine & _
                  " group by TSPL_ADJUSTMENT_DETAIL.adjustment_no,TSPL_ADJUSTMENT_HEADER.adjustment_date,TSPL_ADJUSTMENT_DETAIL.item_code ,TSPL_ADJUSTMENT_DETAIL.unit_cost,TSPL_ADJUSTMENT_DETAIL.mrp,TSPL_ADJUSTMENT_DETAIL.unit_code,aa.trans_qty,aa.FOC,TSPL_ADJUSTMENT_DETAIL.adjustment_line_no " + Environment.NewLine

            ''===================below is data pick from CSA Sale Patti Return
            qry += Environment.NewLine + Environment.NewLine + " Union all " + Environment.NewLine & _
                  "select TSPL_SD_SALE_RETURN_DETAIL.document_code as doc_code,convert(date,TSPL_SD_SALE_RETURN_HEAD.document_date,103) as transfer_date,TSPL_SD_SALE_RETURN_DETAIL.line_no as transfer_line_no,TSPL_SD_SALE_RETURN_DETAIL.item_code,TSPL_SD_SALE_RETURN_DETAIL.unit_code,sum(isnull(TSPL_SD_SALE_RETURN_DETAIL.qty,0)) as qty,aa.trans_qty,TSPL_SD_SALE_RETURN_DETAIL.item_cost as transfer_rate,'Yes' as including_tax,'Backward' as calc_type,case when TSPL_SD_SALE_RETURN_DETAIL.FOC_Item=1 then 'Y' else 'N' end as FOC from TSPL_SD_SALE_RETURN_DETAIL " + Environment.NewLine & _
                  " left outer join (select transfer.Against_Transfer_Code,transfer.transfer_line_no,transfer.item_code,sum(transfer.trans_qty) as trans_qty,transfer.FOC from (select Against_Transfer_Code,transfer_line_no,item_code,sum(alt_qty) as trans_qty,FOC from TSPL_CSA_SALE_TRANSFER_DETAIL where document_code in (select Document_Code from TSPL_SD_SALE_INVOICE_HEAD where trans_type='CSA' and Document_Code <>'" + strDocCode + "' and Customer_Code='" + strCustCode + "' and Bill_To_Location='" + strCSAloc_code + "') group by Against_Transfer_Code,item_code,FOC,transfer_line_no " + Environment.NewLine & _
                  " union all select Against_Transfer_Code,transfer_line_no,item_code,sum(alt_qty) as trans_qty,FOC from CSA_SALE_TRANSFER where (isnull(document_code,'')='' or document_code in (select Document_Code from TSPL_SD_SALE_INVOICE_HEAD where trans_type='CSA' and Customer_Code='" + strCustCode + "' and Bill_To_Location='" + strCSAloc_code + "')) and item_code<>'" + colitemcode + "' group by Against_Transfer_Code,item_code,FOC,transfer_line_no " + Environment.NewLine & _
                  " union all select Against_Transfer_Code,transfer_line_no,item_code,sum(alt_qty) as trans_qty,FOC from CSA_SALE_TRANSFER where (isnull(document_code,'')='' or document_code in (select Document_Code from TSPL_SD_SALE_INVOICE_HEAD where trans_type='CSA' and Customer_Code='" + strCustCode + "' and Bill_To_Location='" + strCSAloc_code + "')) and line_no<>'" + Line_No + "' group by Against_Transfer_Code,item_code,FOC,transfer_line_no " + Environment.NewLine & _
                  " )transfer group by transfer.Against_Transfer_Code,transfer.item_code,transfer.FOC,transfer.transfer_line_no)aa " + Environment.NewLine & _
                  " on aa.Against_Transfer_Code=TSPL_SD_SALE_RETURN_DETAIL.document_code and TSPL_SD_SALE_RETURN_DETAIL.item_code=aa.item_code and aa.transfer_line_no=TSPL_SD_SALE_RETURN_DETAIL.line_no " + Environment.NewLine & _
                  " left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_DETAIL.document_code=TSPL_SD_SALE_RETURN_HEAD.document_code " + Environment.NewLine & _
                  " where TSPL_SD_SALE_RETURN_HEAD.trans_type='CPR' " + strReturnCond + " and TSPL_SD_SALE_RETURN_HEAD.status=1 and TSPL_SD_SALE_RETURN_HEAD.csa_loc_code='" + strCSAloc_code + "' and convert(date,TSPL_SD_SALE_RETURN_HEAD.document_date,103)<='" + clsCommon.GetPrintDate(clsCommon.myCDate(strDocDate), "dd/MMM/yyyy") + "' " + Environment.NewLine & _
                  " group by TSPL_SD_SALE_RETURN_DETAIL.document_code,TSPL_SD_SALE_RETURN_HEAD.document_date,TSPL_SD_SALE_RETURN_DETAIL.item_code ,TSPL_SD_SALE_RETURN_DETAIL.item_cost,TSPL_SD_SALE_RETURN_DETAIL.unit_code,aa.trans_qty,TSPL_SD_SALE_RETURN_DETAIL.FOC_Item,TSPL_SD_SALE_RETURN_DETAIL.line_no " + Environment.NewLine & _
                  " )axa where (isnull(axa.qty,0)-isnull(axa.trans_qty,0))>0 and axa.item_code='" + colitemcode + "' and isnull(axa.FOC,'N')='" + colFOC + "' order by axa.transfer_date,axa.doc_code,axa.FOC "
            '" )final left outer join " + Environment.NewLine & _
            '" (select case when isnull(tspl_sd_sale_return_detail.Adjustment_No,'')<>'' then tspl_sd_sale_return_detail.Adjustment_No else tspl_sd_sale_return_detail.Transfer_No end as doc_code,tspl_sd_sale_return_detail.item_code,sum(isnull(tspl_sd_sale_return_detail.org_transfer_qty,0)) as trans_qty,(case when isnull(tspl_sd_sale_return_detail.FOC_item,0)=1 then 'Y' else 'N' end) as FOC from tspl_sd_sale_return_detail where tspl_sd_sale_return_detail.document_code in (select document_code from tspl_sd_sale_return_head where TSPL_SD_SALE_RETURN_HEAD.trans_type='CSA' and tspl_sd_sale_return_head.customer_code='" + strCustCode + "' and tspl_sd_sale_return_head.csa_loc_code='" + strCSAloc_code + "') group by tspl_sd_sale_return_detail.Adjustment_No,tspl_sd_sale_return_detail.item_code,tspl_sd_sale_return_detail.FOC_item,tspl_sd_sale_return_detail.Transfer_No) return1 on return1.doc_code=final.doc_code and return1.Item_Code=final.Item_Code and return1.FOC=final.FOC order by final.doc_code"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            Dim qqty As Decimal = clsCommon.myCdbl(colqty)
            txtsaleqty.Text = qqty

            Dim strQry As String = "select fin.* from (select final.doc_code,final.item_code,sum(isnull(final.trans_qty,0)) as trans_qty,final.unit_code,final.FOC from (select tspl_sd_sale_return_detail.Transfer_No as doc_code,tspl_sd_sale_return_detail.item_code,sum(isnull(tspl_sd_sale_return_detail.org_transfer_qty,0)) as trans_qty,tspl_sd_sale_return_detail.org_transfer_Uom as unit_code,(case when isnull(tspl_sd_sale_return_detail.FOC_item,0)=1 then 'Y' else 'N' end) as FOC from tspl_sd_sale_return_detail where isnull(tspl_sd_sale_return_detail.Transfer_No,'')<>'' and tspl_sd_sale_return_detail.document_code in (select document_code from tspl_sd_sale_return_head where TSPL_SD_SALE_RETURN_HEAD.trans_type='CSA' and tspl_sd_sale_return_head.customer_code='" + strCustCode + "' and tspl_sd_sale_return_head.bill_to_location='" + strPlantLoc_Code + "') group by tspl_sd_sale_return_detail.Adjustment_No,tspl_sd_sale_return_detail.item_code,tspl_sd_sale_return_detail.FOC_item,tspl_sd_sale_return_detail.Transfer_No,tspl_sd_sale_return_detail.org_transfer_Uom " & _
                " union all " & _
                " select tspl_sd_sale_return_detail.Adjustment_No as doc_code,tspl_sd_sale_return_detail.item_code,sum(isnull(tspl_sd_sale_return_detail.org_transfer_qty,0)) as trans_qty,tspl_sd_sale_return_detail.org_transfer_Uom as unit_code,(case when isnull(tspl_sd_sale_return_detail.FOC_item,0)=1 then 'Y' else 'N' end) as FOC from tspl_sd_sale_return_detail where isnull(tspl_sd_sale_return_detail.Adjustment_No,'')<>'' and tspl_sd_sale_return_detail.document_code in (select document_code from tspl_sd_sale_return_head where TSPL_SD_SALE_RETURN_HEAD.trans_type='CSA' and tspl_sd_sale_return_head.customer_code='" + strCustCode + "' and tspl_sd_sale_return_head.csa_loc_code='" + strCSAloc_code + "') group by tspl_sd_sale_return_detail.Adjustment_No,tspl_sd_sale_return_detail.item_code,tspl_sd_sale_return_detail.FOC_item,tspl_sd_sale_return_detail.Transfer_No,tspl_sd_sale_return_detail.org_transfer_Uom " & _
                " union all " & _
                " select tspl_sd_sale_return_detail.CSA_SalePatti_Return_No as doc_code,tspl_sd_sale_return_detail.item_code,sum(isnull(tspl_sd_sale_return_detail.org_transfer_qty,0)) as trans_qty,tspl_sd_sale_return_detail.org_transfer_Uom as unit_code,(case when isnull(tspl_sd_sale_return_detail.FOC_item,0)=1 then 'Y' else 'N' end) as FOC from tspl_sd_sale_return_detail where isnull(tspl_sd_sale_return_detail.CSA_SalePatti_Return_No,'')<>'' and tspl_sd_sale_return_detail.document_code in (select document_code from tspl_sd_sale_return_head where TSPL_SD_SALE_RETURN_HEAD.trans_type='CSA' and tspl_sd_sale_return_head.customer_code='" + strCustCode + "' and tspl_sd_sale_return_head.csa_loc_code='" + strCSAloc_code + "') group by tspl_sd_sale_return_detail.CSA_SalePatti_Return_No,tspl_sd_sale_return_detail.item_code,tspl_sd_sale_return_detail.FOC_item,tspl_sd_sale_return_detail.Transfer_No,tspl_sd_sale_return_detail.org_transfer_Uom " & _
                ")final group by final.doc_code,final.item_code,final.unit_code,final.FOC)fin where fin.item_code='" + colitemcode + "' and isnull(fin.FOC,'N')='" + colFOC + "' order by fin.doc_code,fin.FOC"
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(strQry)
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.Columns.Add("doc_code")
            gv.Columns.Add("item_code")
            gv.Columns.Add("unit_code")
            gv.Columns.Add("trans_qty")
            gv.Columns.Add("FOC")
            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                For Each dr1 As DataRow In dt1.Rows
                    gv.Rows.AddNew()
                    gv.Rows(gv.Rows.Count - 1).Cells("doc_code").Value = clsCommon.myCstr(dr1("doc_code"))
                    gv.Rows(gv.Rows.Count - 1).Cells("item_code").Value = clsCommon.myCstr(dr1("item_code"))
                    gv.Rows(gv.Rows.Count - 1).Cells("unit_code").Value = clsCommon.myCstr(dr1("unit_code"))
                    gv.Rows(gv.Rows.Count - 1).Cells("foc").Value = clsCommon.myCstr(dr1("foc"))
                    gv.Rows(gv.Rows.Count - 1).Cells("trans_qty").Value = clsCommon.myCstr(dr1("trans_qty"))
                Next
            End If

            Dim iCode As String = ""
            Dim iFOC As String = ""
            Dim iDoc_Code As String = ""

            Dim Doc_Code As String = ""
            Dim RCode As String = ""
            Dim RFOC As String = ""
            Dim RQty As Decimal = 0
            Dim RUnit As String = ""
            Dim CF As Decimal = 0
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    If qqty = 0 AndAlso Not TransferManual_KnockOFF Then ''when auto knock-off then,after 0 bal qty no more rows insert,but in manual case all rows insert.
                        Exit For
                    End If

                    '==================================knock-off on the basis of return
                    iCode = clsCommon.myCstr(dr("item_code"))
                    iFOC = clsCommon.myCstr(dr("FOC"))
                    iDoc_Code = clsCommon.myCstr(dr("doc_code"))
                    '==================qty comes after conversion===========
                    Dim iCF As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(dr("item_code")) + "' and uom_code='" + clsCommon.myCstr(dr("unit_code")) + "'")) / clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(dr("item_code")) + "' and uom_code='" + colItemUOM + "'"))
                    If iCF <= 0 Then
                        iCF = 1
                    End If
                    Dim transqty As Decimal = Nothing
                    Dim NewBal_Qty As Decimal = Nothing
                    transqty = System.Math.Round(clsCommon.myCdbl(dr("qty")) * clsCommon.myCdbl(iCF), 2)
                    NewBal_Qty = clsCommon.myCdbl(dr("qty"))
                    '==============================================

                    If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                        For ii As Integer = 0 To gv.Rows.Count - 1
                            RCode = clsCommon.myCstr(gv.Rows(ii).Cells("item_code").Value)
                            RFOC = clsCommon.myCstr(gv.Rows(ii).Cells("FOC").Value)
                            RUnit = clsCommon.myCstr(gv.Rows(ii).Cells("unit_code").Value)
                            RQty = clsCommon.myCdbl(gv.Rows(ii).Cells("trans_qty").Value)
                            Doc_Code = clsCommon.myCstr(gv.Rows(ii).Cells("doc_code").Value)

                            If clsCommon.myLen(RCode) > 0 AndAlso clsCommon.CompairString(RCode, iCode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(Doc_Code, iDoc_Code) = CompairStringResult.Equal AndAlso RQty > 0 Then
                                CF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + RCode + "' and uom_code='" + RUnit + "'")) / clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + RCode + "' and uom_code='" + colItemUOM + "'"))
                                RQty = System.Math.Round(clsCommon.myCdbl(RQty) * clsCommon.myCdbl(CF), 2)

                                If transqty >= RQty Then
                                    transqty = transqty - RQty
                                    NewBal_Qty = NewBal_Qty - RQty
                                    gv.Rows(ii).Cells("trans_qty").Value = 0
                                ElseIf transqty < RQty Then
                                    gv.Rows(ii).Cells("trans_qty").Value = (RQty - transqty) / IIf(CF > 0, CF, 1)
                                    transqty = 0
                                End If
                            End If ''end comp.
                        Next
                    End If ''end dt1
                    If transqty <= 0 Then
                        Continue For
                    End If
                    '==================================================================================

                    gv_trans.Rows.AddNew()

                    gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(colSelect).Value = IIf(Not TransferManual_KnockOFF, True, False)
                    gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(colTransfer_Line_No).Value = clsCommon.myCdbl(dr("transfer_line_no")) ' clsCommon.myCstr(gv_trans.Rows.Count)
                    gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(coltranslineno).Value = Line_No ' clsCommon.myCstr(gv_trans.Rows.Count)
                    gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(colTranscode).Value = clsCommon.myCstr(dr("doc_code"))
                    gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(coltransitemcode).Value = clsCommon.myCstr(dr("item_code"))
                    gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(coltransiname).Value = clsItemMaster.GetItemName(clsCommon.myCstr(dr("item_code")), Nothing)
                    gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(coltrans_actual_qty).Value = clsCommon.myCdbl(dr("actual_qty"))
                    gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(coltransuom).Value = clsCommon.myCstr(dr("unit_code"))
                    gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(colTransFOC).Value = clsCommon.myCstr(dr("FOC"))
                    gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(coltrans_sale_uom).Value = colItemUOM


                    '==================qty comes after conversion===========
                    gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(colTransConvFactor).Value = iCF
                    '==============================================
                    gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(colManualOrgBal).Value = transqty

                    If qqty >= transqty Then
                        gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(coltransqty).Value = transqty
                        gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(coltransbal_qty).Value = NewBal_Qty ' clsCommon.myCdbl(dr("qty"))
                        gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(coltransAltQty).Value = NewBal_Qty ' clsCommon.myCdbl(dr("qty"))
                        qqty = qqty - transqty
                    ElseIf qqty < transqty Then
                        gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(coltransqty).Value = qqty
                        gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(coltransbal_qty).Value = System.Math.Round((clsCommon.myCdbl(transqty) - clsCommon.myCdbl(qqty)) / clsCommon.myCdbl(gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(colTransConvFactor).Value), 2)
                        gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(coltransAltQty).Value = System.Math.Round(clsCommon.myCdbl(qqty) / clsCommon.myCdbl(gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(colTransConvFactor).Value), 2)
                        qqty = 0
                    End If
                    gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(coltransrate).Value = clsCommon.myCdbl(dr("transfer_rate")) ''/ clsCommon.myCdbl(gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(colTransConvFactor).Value), 2)

                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            Me.Controls.Remove(gv)
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
        Me.Close()
    End Sub

    Private Sub SaveData()
        Try
            Dim totalTranRate As Decimal = 0
            Dim TotalCOunt = 0
            Dim totalqty As Decimal = 0
            Dim totalOrgQty As Decimal = 0
            Dim amt As Decimal = 0
            Dim totalamt As Decimal = 0
            Dim obj As New clsCSAStockTransferDetail()
            obj.arr = New List(Of clsCSAStockTransferDetail)
            GV_ARR = New List(Of clsCSAStockTransferDetail)

            For Each grow As GridViewRowInfo In gv_trans.Rows
                If clsCommon.myCBool(grow.Cells(colSelect).Value) = False AndAlso TransferManual_KnockOFF Then
                    Continue For
                ElseIf clsCommon.myCBool(grow.Cells(colSelect).Value) = True AndAlso TransferManual_KnockOFF AndAlso clsCommon.myCdbl(grow.Cells(coltransqty).Value) <= 0 Then
                    Continue For
                End If

                Dim objtr As New clsCSAStockTransferDetail()

                objtr.code = clsCommon.myCstr(strDocCode)
                objtr.lineno = CInt(clsCommon.myCstr(grow.Cells(coltranslineno).Value))
                objtr.transcode = clsCommon.myCstr(grow.Cells(colTranscode).Value)
                objtr.icode = clsCommon.myCstr(grow.Cells(coltransitemcode).Value)
                objtr.qty = clsCommon.myCdbl(clsCommon.myCstr(grow.Cells(coltransqty).Value))
                objtr.rate = clsCommon.myCdbl(clsCommon.myCstr(grow.Cells(coltransrate).Value))
                objtr.packsize = clsCommon.myCdbl(colPackSize)
                objtr.act_qty = clsCommon.myCdbl(grow.Cells(coltrans_actual_qty).Value)
                objtr.uom = clsCommon.myCstr(grow.Cells(coltransuom).Value)
                objtr.bal_qty = clsCommon.myCdbl(grow.Cells(coltransbal_qty).Value)
                objtr.sale_uom = clsCommon.myCstr(grow.Cells(coltrans_sale_uom).Value)
                objtr.conv_factor = clsCommon.myCdbl(grow.Cells(colTransConvFactor).Value)
                objtr.alt_qty = clsCommon.myCdbl(grow.Cells(coltransAltQty).Value)
                objtr.FOC = clsCommon.myCstr(grow.Cells(colTransFOC).Value)
                objtr.Transfer_Line_No = clsCommon.myCdbl(grow.Cells(colTransfer_Line_No).Value)

                colIsScheme = "N"
                If clsCommon.CompairString(objtr.FOC, "Y") = CompairStringResult.Equal Then
                    colIsScheme = "Y"
                End If



                If clsCommon.myLen(objtr.transcode) > 0 Then

                    If TransferManual_KnockOFF Then
                        grow.Cells(colManualOrgBal).Value = GetBalance_of_Transfer(clsCommon.myCstr(grow.Cells(coltranslineno).Value), grow.Index)
                        If clsCommon.myCdbl(grow.Cells(coltransqty).Value) > clsCommon.myCdbl(grow.Cells(colManualOrgBal).Value) Then
                            gv_trans.CurrentRow = gv_trans.Rows(grow.Index)
                            gv_trans.CurrentColumn = gv_trans.Columns(coltransqty)
                            Throw New Exception("Entered Sale Qty can-not exceed balance qty i.e. (" + clsCommon.myCstr(grow.Cells(colManualOrgBal).Value) + " " + clsCommon.myCstr(grow.Cells(coltrans_sale_uom).Value) + ")")
                        End If
                    End If

                    totalqty = totalqty + clsCommon.myCdbl(clsCommon.myCstr(grow.Cells(coltransqty).Value))
                    totalOrgQty = totalOrgQty + clsCommon.myCdbl(clsCommon.myCstr(grow.Cells(coltransAltQty).Value))
                    ''amount is multiple of org uom qty and amount, not of converted qty,so multiply with transAltqty,i.e of org. transfer UOM
                    '' changed by Panch Raj
                    If clsCommon.myCdbl(grow.Cells(colTransConvFactor).Value) <= 1 Then
                        amt = clsCommon.myCdbl(clsCommon.myCstr(grow.Cells(coltransAltQty).Value)) * clsCommon.myCdbl(clsCommon.myCstr(grow.Cells(coltransrate).Value))
                    Else
                        'amt = clsCommon.myCdbl(clsCommon.myCstr(grow.Cells(coltransqty).Value)) * (clsCommon.myCdbl(clsCommon.myCstr(grow.Cells(coltransrate).Value)) / clsCommon.myCdbl(grow.Cells(colTransConvFactor).Value))
                        amt = System.Math.Round(clsCommon.myCdbl(clsCommon.myCstr(grow.Cells(coltransqty).Value)) / clsCommon.myCdbl(grow.Cells(colTransConvFactor).Value), 2) * clsCommon.myCdbl(clsCommon.myCstr(grow.Cells(coltransrate).Value))
                    End If

                    totalamt = totalamt + amt

                    ''=====================
                    totalTranRate = totalTranRate + objtr.rate
                    TotalCOunt += 1
                    ''=========================

                    obj.arr.Add(objtr)
                End If
            Next


            If totalqty > clsCommon.myCdbl(txtsaleqty.Text) OrElse totalqty < clsCommon.myCdbl(txtsaleqty.Text) Then
                Dim msg As String = "Sum of Transfer qty. (" + clsCommon.myCstr(totalqty) + ") is not same as sale qty (" + clsCommon.myCstr(txtsaleqty.Text) + ")."
                If totalqty < clsCommon.myCdbl(txtsaleqty.Text) Then
                    msg += Environment.NewLine + "Do more transfer for meet requirement."
                End If

                'txtsaleqty.Text = ""
                Throw New Exception(msg)
            End If

            GV_ARR = obj.arr

            'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            If clsCSAStockTransferDetail.SaveData(obj.arr) Then
                If clsCommon.myCdbl(totalamt) > 0 Then
                    'colStckTransferrate = System.Math.Round(clsCommon.myCdbl(totalamt) / clsCommon.myCdbl(totalqty), 2)
                    colStckTransferrate = System.Math.Round(clsCommon.myCdbl(totalamt) / clsCommon.myCdbl(totalOrgQty), 2)
                    'colStckTransferrate = System.Math.Round(clsCommon.myCdbl(totalTranRate) / clsCommon.myCdbl(TotalCOunt), 2)
                    colStckTransferAmount = totalamt


                    'gv.CurrentRow.Cells(colstckratevalue).Value = System.Math.Round(clsCommon.myCdbl(gv.CurrentRow.Cells(colqty).Value) * clsCommon.myCdbl(gv.CurrentRow.Cells(colStckTransferrate).Value), 2)
                    'gv.CurrentRow.Cells(colGainLoss).Value = System.Math.Round(clsCommon.myCdbl(gv.CurrentRow.Cells(colSaleValue).Value) - clsCommon.myCdbl(gv.CurrentRow.Cells(colstckratevalue).Value), 2)
                End If

                'If clsCommon.CompairString(clsCommon.myCstr(gv.CurrentRow.Cells(colFOC).Value), "Y") = CompairStringResult.Equal Then
                '    gv.CurrentRow.Cells(colStckTransferrate).Value = clsCommon.myCdbl(gv.Rows(gv.CurrentRow.Index - 1).Cells(colStckTransferrate).Value)
                'End If

                txtsaleqty.Text = ""
            End If

        Catch ex As Exception
            If ComeFromImport Then
                Throw New Exception(ex.Message)
            Else
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End If
        End Try
    End Sub

    Private Sub FrmCSATrans_KnockOffScreen_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If clsCommon.myLen(txtsaleqty.Text) > 0 Then
            SaveData()
        End If
    End Sub

    Private Sub gv_trans_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv_trans.CellValueChanged
        Try
            If Not isInsideLoadData Then
                If Not isCellValueChanged Then
                    If (e.Column Is gv_trans.Columns(coltransqty)) AndAlso TransferManual_KnockOFF Then
                        isCellValueChanged = True

                        If clsCommon.myCdbl(colqty) >= clsCommon.myCdbl(gv_trans.CurrentRow.Cells(coltransqty).Value) Then
                            gv_trans.CurrentRow.Cells(coltransAltQty).Value = System.Math.Round(clsCommon.myCdbl(gv_trans.CurrentRow.Cells(coltransqty).Value) / clsCommon.myCdbl(gv_trans.CurrentRow.Cells(colTransConvFactor).Value), 2)
                        ElseIf clsCommon.myCdbl(colqty) < clsCommon.myCdbl(gv_trans.CurrentRow.Cells(coltransqty).Value) Then
                            gv_trans.CurrentRow.Cells(coltransAltQty).Value = System.Math.Round(clsCommon.myCdbl(colqty) / clsCommon.myCdbl(gv_trans.CurrentRow.Cells(colTransConvFactor).Value), 2)
                        End If
                        gv_trans.CurrentRow.Cells(coltransbal_qty).Value = System.Math.Round((clsCommon.myCdbl(gv_trans.CurrentRow.Cells(colManualOrgBal).Value) / clsCommon.myCdbl(gv_trans.CurrentRow.Cells(colTransConvFactor).Value)) - clsCommon.myCdbl(gv_trans.CurrentRow.Cells(coltransAltQty).Value), 2)


                        If clsCommon.myCBool(gv_trans.CurrentRow.Cells(colSelect).Value) = True AndAlso clsCommon.myCdbl(gv_trans.CurrentRow.Cells(coltransqty).Value) > clsCommon.myCdbl(gv_trans.CurrentRow.Cells(colManualOrgBal).Value) Then
                            gv_trans.CurrentRow.Cells(coltransqty).Value = 0
                            Throw New Exception("Entered Sale Qty must not exceed balance qty i.e. (" + clsCommon.myCstr(gv_trans.CurrentRow.Cells(colManualOrgBal).Value) + " " + clsCommon.myCstr(gv_trans.CurrentRow.Cells(coltrans_sale_uom).Value) + ")")
                        End If
                        isCellValueChanged = False
                    End If
                End If
            End If
        Catch ex As Exception
            isCellValueChanged = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Function GetBalance_of_Transfer(ByVal Line_No As String, ByVal IntRow As Integer) As Decimal
        Dim qry As String = ""
        Dim gv As New RadGridView()
        Dim strReturnCond As String = ""
        Me.Controls.Add(gv)
        Try
            If ShowReturnType Then
                strReturnCond = " and tspl_sd_sale_return_head.return_type not in ('D','S') "
            End If

            qry = "select axa.doc_code,axa.transfer_line_no,axa.item_code,axa.unit_code,isnull(axa.qty,0) as actual_qty,(isnull(axa.qty,0)-isnull(axa.trans_qty,0)) as qty,axa.transfer_rate,axa.including_tax,axa.calc_type,isnull(axa.FOC,'N') as FOC from ( " + Environment.NewLine & _
              "select TSPL_CSA_TRANSFER_DETAIL.doc_code,convert(date,TSPL_CSA_TRANSFER_HEAD.transfer_date,103) as transfer_date,TSPL_CSA_TRANSFER_DETAIL.Line_No as transfer_line_no,TSPL_CSA_TRANSFER_DETAIL.item_code,TSPL_CSA_TRANSFER_DETAIL.unit_code,sum(TSPL_CSA_TRANSFER_DETAIL.qty) as qty,aa.trans_qty,TSPL_CSA_TRANSFER_DETAIL.transfer_rate,TSPL_CSA_TRANSFER_DETAIL.including_tax,TSPL_CSA_TRANSFER_DETAIL.calc_type,TSPL_CSA_TRANSFER_DETAIL.FOC from TSPL_CSA_TRANSFER_DETAIL " + Environment.NewLine & _
              " left outer join TSPL_CSA_TRANSFER_HEAD on TSPL_CSA_TRANSFER_HEAD.doc_code=TSPL_CSA_TRANSFER_DETAIL.doc_code " & _
              " left outer join (select transfer.Against_Transfer_Code,transfer.transfer_line_no,transfer.item_code,transfer.FOC,sum(transfer.trans_qty) as trans_qty from (select Against_Transfer_Code,transfer_line_no,item_code,FOC,sum(alt_qty) as trans_qty from TSPL_CSA_SALE_TRANSFER_DETAIL where document_code in (select Document_Code from TSPL_SD_SALE_INVOICE_HEAD where Trans_Type='CSA' and Document_Code <>'" + strDocCode + "' and Customer_Code='" + strCustCode + "' and CSA_PLANT_LOCATION='" + strPlantLoc_Code + "') group by Against_Transfer_Code,item_code,FOC,transfer_line_no " + Environment.NewLine & _
              " union all select Against_Transfer_Code,transfer_line_no,item_code,FOC,sum(alt_qty) as trans_qty from CSA_SALE_TRANSFER where (isnull(document_code,'')='' or document_code in (select Document_Code from TSPL_SD_SALE_INVOICE_HEAD where trans_type='CSA' and Customer_Code='" + strCustCode + "' and CSA_PLANT_LOCATION='" + strPlantLoc_Code + "')) and item_code<>'" + colitemcode + "' group by Against_Transfer_Code,item_code,FOC,transfer_line_no " + Environment.NewLine & _
              " union all select Against_Transfer_Code,transfer_line_no,item_code,FOC,sum(alt_qty) as trans_qty from CSA_SALE_TRANSFER where (isnull(document_code,'')='' or document_code in (select Document_Code from TSPL_SD_SALE_INVOICE_HEAD where trans_type='CSA' and Customer_Code='" + strCustCode + "' and CSA_PLANT_LOCATION='" + strPlantLoc_Code + "')) and line_no<>'" + Line_No + "' group by Against_Transfer_Code,item_code,FOC,transfer_line_no " + Environment.NewLine & _
              " )transfer group by transfer.Against_Transfer_Code,transfer.item_code,transfer.FOC,transfer.transfer_line_no)aa " + Environment.NewLine & _
              " on aa.Against_Transfer_Code=TSPL_CSA_TRANSFER_DETAIL.doc_code and TSPL_CSA_TRANSFER_DETAIL.item_code=aa.item_code and aa.FOC=TSPL_CSA_TRANSFER_DETAIL.FOC and aa.transfer_line_no=TSPL_CSA_TRANSFER_DETAIL.Line_No " + Environment.NewLine & _
              " where TSPL_CSA_TRANSFER_HEAD.Status=1 and TSPL_CSA_TRANSFER_HEAD.From_Location_Code='" + strPlantLoc_Code + "' and TSPL_CSA_TRANSFER_HEAD.Cust_Code='" + strCustCode + "' and convert(date,TSPL_CSA_TRANSFER_HEAD.transfer_date,103) <='" + clsCommon.GetPrintDate(clsCommon.myCDate(strDocDate), "dd/MMM/yyyy") + "' " + Environment.NewLine

            ''======= Parteek added on 01-02-20117
            If ShowDocumentCancel = True Then
                qry += " and tspl_csa_transfer_head.CancelFlag is null "
            End If
            ''============End

            qry += " group by TSPL_CSA_TRANSFER_DETAIL.doc_code,TSPL_CSA_TRANSFER_HEAD.transfer_date,TSPL_CSA_TRANSFER_DETAIL.item_code ,TSPL_CSA_TRANSFER_DETAIL.transfer_rate,TSPL_CSA_TRANSFER_DETAIL.unit_code,TSPL_CSA_TRANSFER_DETAIL.including_tax,TSPL_CSA_TRANSFER_DETAIL.calc_type,aa.trans_qty,TSPL_CSA_TRANSFER_DETAIL.FOC,TSPL_CSA_TRANSFER_DETAIL.line_no "
            ''" where TSPL_CSA_TRANSFER_DETAIL.DOC_CODE in (select DOC_CODE from TSPL_CSA_TRANSFER_HEAD where Status=1 and From_Location_Code='" + strPlantLoc_Code + "' and Cust_Code='" + strCustCode + "') " + Environment.NewLine & _

            ''======================below data is picked from Adjustment
            qry += Environment.NewLine + Environment.NewLine + " Union all " + Environment.NewLine & _
                  "select TSPL_ADJUSTMENT_DETAIL.adjustment_no as doc_code,convert(date,TSPL_ADJUSTMENT_HEADER.adjustment_date,103) as transfer_date,TSPL_ADJUSTMENT_DETAIL.adjustment_line_no as transfer_line_no,TSPL_ADJUSTMENT_DETAIL.item_code,TSPL_ADJUSTMENT_DETAIL.unit_code,sum((case when TSPL_ADJUSTMENT_HEADER.trans_type='In' then TSPL_ADJUSTMENT_DETAIL.item_quantity else (0-TSPL_ADJUSTMENT_DETAIL.item_quantity) end)) as qty,aa.trans_qty,(case when TSPL_ADJUSTMENT_DETAIL.unit_cost>0 then TSPL_ADJUSTMENT_DETAIL.unit_cost else TSPL_ADJUSTMENT_DETAIL.mrp end) as transfer_rate,'Yes' as including_tax,'Backward' as calc_type,aa.FOC from TSPL_ADJUSTMENT_DETAIL " + Environment.NewLine & _
                  " left outer join (select transfer.Against_Transfer_Code,transfer.transfer_line_no,transfer.item_code,sum(transfer.trans_qty) as trans_qty,transfer.FOC from (select Against_Transfer_Code,transfer_line_no,item_code,sum(alt_qty) as trans_qty,FOC from TSPL_CSA_SALE_TRANSFER_DETAIL where document_code in (select Document_Code from TSPL_SD_SALE_INVOICE_HEAD where trans_type='CSA' and Document_Code <>'" + strDocCode + "' and Customer_Code='" + strCustCode + "' and Bill_To_Location='" + strCSAloc_code + "') group by Against_Transfer_Code,item_code,FOC,transfer_line_no " + Environment.NewLine & _
                  " union all select Against_Transfer_Code,transfer_line_no,item_code,sum(alt_qty) as trans_qty,FOC from CSA_SALE_TRANSFER where (isnull(document_code,'')='' or document_code in (select Document_Code from TSPL_SD_SALE_INVOICE_HEAD where trans_type='CSA' and Customer_Code='" + strCustCode + "' and Bill_To_Location='" + strCSAloc_code + "')) and item_code<>'" + colitemcode + "' group by Against_Transfer_Code,item_code,FOC,transfer_line_no " + Environment.NewLine & _
                  " union all select Against_Transfer_Code,transfer_line_no,item_code,sum(alt_qty) as trans_qty,FOC from CSA_SALE_TRANSFER where (isnull(document_code,'')='' or document_code in (select Document_Code from TSPL_SD_SALE_INVOICE_HEAD where trans_type='CSA' and Customer_Code='" + strCustCode + "' and Bill_To_Location='" + strCSAloc_code + "')) and line_no<>'" + Line_No + "' group by Against_Transfer_Code,item_code,FOC,transfer_line_no " + Environment.NewLine & _
                  " )transfer group by transfer.Against_Transfer_Code,transfer.item_code,transfer.FOC,transfer.transfer_line_no)aa " + Environment.NewLine & _
                  " on aa.Against_Transfer_Code=TSPL_ADJUSTMENT_DETAIL.adjustment_no and TSPL_ADJUSTMENT_DETAIL.item_code=aa.item_code and aa.transfer_line_no=TSPL_ADJUSTMENT_DETAIL.adjustment_line_no " + Environment.NewLine & _
                  " left outer join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_DETAIL.adjustment_no=TSPL_ADJUSTMENT_HEADER.adjustment_no " + Environment.NewLine & _
                  " where TSPL_ADJUSTMENT_HEADER.posted='Y' and TSPL_ADJUSTMENT_HEADER.loc_code='" + strCSAloc_code + "' and convert(date,TSPL_ADJUSTMENT_HEADER.adjustment_date,103)<='" + clsCommon.GetPrintDate(clsCommon.myCDate(strDocDate), "dd/MMM/yyyy") + "' " + Environment.NewLine & _
                  " group by TSPL_ADJUSTMENT_DETAIL.adjustment_no,TSPL_ADJUSTMENT_HEADER.adjustment_date,TSPL_ADJUSTMENT_DETAIL.item_code ,TSPL_ADJUSTMENT_DETAIL.unit_cost,TSPL_ADJUSTMENT_DETAIL.mrp,TSPL_ADJUSTMENT_DETAIL.unit_code,aa.trans_qty,aa.FOC,TSPL_ADJUSTMENT_DETAIL.adjustment_line_no " + Environment.NewLine

            ''======================below data is picked from CSA Sale Patti Return
            qry += Environment.NewLine + Environment.NewLine + " Union all " + Environment.NewLine & _
                  "select TSPL_SD_SALE_RETURN_DETAIL.document_code as doc_code,convert(date,TSPL_SD_SALE_RETURN_HEAD.document_date,103) as transfer_date,TSPL_SD_SALE_RETURN_DETAIL.line_no as transfer_line_no,TSPL_SD_SALE_RETURN_DETAIL.item_code,TSPL_SD_SALE_RETURN_DETAIL.unit_code,sum(isnull(TSPL_SD_SALE_RETURN_DETAIL.qty,0)) as qty,aa.trans_qty,isnull(TSPL_SD_SALE_RETURN_DETAIL.item_cost,0) as transfer_rate,'Yes' as including_tax,'Backward' as calc_type,aa.FOC from TSPL_SD_SALE_RETURN_DETAIL " + Environment.NewLine & _
                  " left outer join (select transfer.Against_Transfer_Code,transfer.transfer_line_no,transfer.item_code,sum(transfer.trans_qty) as trans_qty,transfer.FOC from (select Against_Transfer_Code,transfer_line_no,item_code,sum(alt_qty) as trans_qty,FOC from TSPL_CSA_SALE_TRANSFER_DETAIL where document_code in (select Document_Code from TSPL_SD_SALE_INVOICE_HEAD where trans_type='CSA' and Document_Code <>'" + strDocCode + "' and Customer_Code='" + strCustCode + "' and Bill_To_Location='" + strCSAloc_code + "') group by Against_Transfer_Code,item_code,FOC,transfer_line_no " + Environment.NewLine & _
                  " union all select Against_Transfer_Code,transfer_line_no,item_code,sum(alt_qty) as trans_qty,FOC from CSA_SALE_TRANSFER where (isnull(document_code,'')='' or document_code in (select Document_Code from TSPL_SD_SALE_INVOICE_HEAD where trans_type='CSA' and Customer_Code='" + strCustCode + "' and Bill_To_Location='" + strCSAloc_code + "')) and item_code<>'" + colitemcode + "' group by Against_Transfer_Code,item_code,FOC,transfer_line_no " + Environment.NewLine & _
                  " union all select Against_Transfer_Code,transfer_line_no,item_code,sum(alt_qty) as trans_qty,FOC from CSA_SALE_TRANSFER where (isnull(document_code,'')='' or document_code in (select Document_Code from TSPL_SD_SALE_INVOICE_HEAD where trans_type='CSA' and Customer_Code='" + strCustCode + "' and Bill_To_Location='" + strCSAloc_code + "')) and line_no<>'" + Line_No + "' group by Against_Transfer_Code,item_code,FOC,transfer_line_no " + Environment.NewLine & _
                  " )transfer group by transfer.Against_Transfer_Code,transfer.item_code,transfer.FOC,transfer.transfer_line_no)aa " + Environment.NewLine & _
                  " on aa.Against_Transfer_Code=TSPL_SD_SALE_RETURN_DETAIL.document_code and TSPL_SD_SALE_RETURN_DETAIL.item_code=aa.item_code and aa.transfer_line_no=TSPL_SD_SALE_RETURN_DETAIL.line_no " + Environment.NewLine & _
                  " left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_DETAIL.document_code=TSPL_SD_SALE_RETURN_HEAD.document_code " + Environment.NewLine & _
                  " where TSPL_SD_SALE_RETURN_HEAD.trans_type='CPR' " + strReturnCond + " and TSPL_SD_SALE_RETURN_HEAD.status=1 and TSPL_SD_SALE_RETURN_HEAD.CSA_loc_code='" + strCSAloc_code + "' and convert(date,TSPL_SD_SALE_RETURN_HEAD.document_date,103)<='" + clsCommon.GetPrintDate(clsCommon.myCDate(strDocDate), "dd/MMM/yyyy") + "' " + Environment.NewLine & _
                  " group by TSPL_SD_SALE_RETURN_DETAIL.document_code,TSPL_SD_SALE_RETURN_HEAD.document_date,TSPL_SD_SALE_RETURN_DETAIL.item_code ,TSPL_SD_SALE_RETURN_DETAIL.item_cost,TSPL_SD_SALE_RETURN_DETAIL.unit_code,aa.trans_qty,aa.FOC,TSPL_SD_SALE_RETURN_DETAIL.line_no " + Environment.NewLine & _
                  " )axa where axa.doc_code='" + clsCommon.myCstr(gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(colTranscode).Value) + "' and axa.item_code='" + clsCommon.myCstr(gv_trans.Rows(IntRow).Cells(coltransitemcode).Value) + "' and axa.transfer_line_no='" + clsCommon.myCstr(gv_trans.Rows(IntRow).Cells(colTransfer_Line_No).Value) + "' and isnull(axa.FOC,'N')='" + colFOC + "' order by axa.transfer_date,axa.doc_code,axa.FOC "
            '" )final left outer join " + Environment.NewLine & _
            '" (select case when isnull(tspl_sd_sale_return_detail.Adjustment_No,'')<>'' then tspl_sd_sale_return_detail.Adjustment_No else tspl_sd_sale_return_detail.Transfer_No end as doc_code,tspl_sd_sale_return_detail.item_code,sum(isnull(tspl_sd_sale_return_detail.org_transfer_qty,0)) as trans_qty,(case when isnull(tspl_sd_sale_return_detail.FOC_item,0)=1 then 'Y' else 'N' end) as FOC from tspl_sd_sale_return_detail where tspl_sd_sale_return_detail.document_code in (select document_code from tspl_sd_sale_return_head where TSPL_SD_SALE_RETURN_HEAD.trans_type='CSA' and tspl_sd_sale_return_head.customer_code='" + strCustCode + "' and tspl_sd_sale_return_head.csa_loc_code='" + strCSAloc_code + "') group by tspl_sd_sale_return_detail.Adjustment_No,tspl_sd_sale_return_detail.item_code,tspl_sd_sale_return_detail.FOC_item,tspl_sd_sale_return_detail.Transfer_No) return1 on return1.doc_code=final.doc_code and return1.Item_Code=final.Item_Code and return1.FOC=final.FOC order by final.doc_code"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            Dim qqty As Decimal = clsCommon.myCdbl(gv_trans.Rows(IntRow).Cells(coltransqty).Value)

            Dim strQry As String = "select fin.* from (select final.doc_code,final.item_code,sum(isnull(final.trans_qty,0)) as trans_qty,final.unit_code,final.FOC from (select tspl_sd_sale_return_detail.Transfer_No as doc_code,tspl_sd_sale_return_detail.item_code,sum(isnull(tspl_sd_sale_return_detail.org_transfer_qty,0)) as trans_qty,tspl_sd_sale_return_detail.org_transfer_Uom as unit_code,(case when isnull(tspl_sd_sale_return_detail.FOC_item,0)=1 then 'Y' else 'N' end) as FOC from tspl_sd_sale_return_detail where isnull(tspl_sd_sale_return_detail.Transfer_No,'')<>'' and tspl_sd_sale_return_detail.document_code in (select document_code from tspl_sd_sale_return_head where TSPL_SD_SALE_RETURN_HEAD.trans_type='CSA' and tspl_sd_sale_return_head.customer_code='" + strCustCode + "' and tspl_sd_sale_return_head.bill_to_location='" + strPlantLoc_Code + "') group by tspl_sd_sale_return_detail.Adjustment_No,tspl_sd_sale_return_detail.item_code,tspl_sd_sale_return_detail.FOC_item,tspl_sd_sale_return_detail.Transfer_No,tspl_sd_sale_return_detail.org_transfer_Uom " & _
                " union all " & _
                " select tspl_sd_sale_return_detail.Adjustment_No as doc_code,tspl_sd_sale_return_detail.item_code,sum(isnull(tspl_sd_sale_return_detail.org_transfer_qty,0)) as trans_qty,tspl_sd_sale_return_detail.org_transfer_Uom as unit_code,(case when isnull(tspl_sd_sale_return_detail.FOC_item,0)=1 then 'Y' else 'N' end) as FOC from tspl_sd_sale_return_detail where isnull(tspl_sd_sale_return_detail.Adjustment_No,'')<>'' and tspl_sd_sale_return_detail.document_code in (select document_code from tspl_sd_sale_return_head where TSPL_SD_SALE_RETURN_HEAD.trans_type='CSA' and tspl_sd_sale_return_head.customer_code='" + strCustCode + "' and tspl_sd_sale_return_head.csa_loc_code='" + strCSAloc_code + "') group by tspl_sd_sale_return_detail.Adjustment_No,tspl_sd_sale_return_detail.item_code,tspl_sd_sale_return_detail.FOC_item,tspl_sd_sale_return_detail.Transfer_No,tspl_sd_sale_return_detail.org_transfer_Uom " & _
                " union all " & _
                " select tspl_sd_sale_return_detail.CSA_SalePatti_Return_No as doc_code,tspl_sd_sale_return_detail.item_code,sum(isnull(tspl_sd_sale_return_detail.org_transfer_qty,0)) as trans_qty,tspl_sd_sale_return_detail.org_transfer_Uom as unit_code,(case when isnull(tspl_sd_sale_return_detail.FOC_item,0)=1 then 'Y' else 'N' end) as FOC from tspl_sd_sale_return_detail where isnull(tspl_sd_sale_return_detail.CSA_SalePatti_Return_No,'')<>'' and tspl_sd_sale_return_detail.document_code in (select document_code from tspl_sd_sale_return_head where TSPL_SD_SALE_RETURN_HEAD.trans_type='CSA' and tspl_sd_sale_return_head.customer_code='" + strCustCode + "' and tspl_sd_sale_return_head.csa_loc_code='" + strCSAloc_code + "') group by tspl_sd_sale_return_detail.CSA_SalePatti_Return_No,tspl_sd_sale_return_detail.item_code,tspl_sd_sale_return_detail.FOC_item,tspl_sd_sale_return_detail.Transfer_No,tspl_sd_sale_return_detail.org_transfer_Uom " & _
                ")final group by final.doc_code,final.item_code,final.unit_code,final.FOC)fin where fin.doc_code='" + clsCommon.myCstr(gv_trans.Rows(gv_trans.Rows.Count - 1).Cells(colTranscode).Value) + "' and fin.item_code='" + clsCommon.myCstr(gv_trans.Rows(IntRow).Cells(coltransitemcode).Value) + "' and isnull(fin.FOC,'N')='" + colFOC + "' order by fin.doc_code,fin.FOC"
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(strQry)
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.Columns.Add("doc_code")
            gv.Columns.Add("item_code")
            gv.Columns.Add("unit_code")
            gv.Columns.Add("trans_qty")
            gv.Columns.Add("FOC")
            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                For Each dr1 As DataRow In dt1.Rows
                    gv.Rows.AddNew()
                    gv.Rows(gv.Rows.Count - 1).Cells("doc_code").Value = clsCommon.myCstr(dr1("doc_code"))
                    gv.Rows(gv.Rows.Count - 1).Cells("item_code").Value = clsCommon.myCstr(dr1("item_code"))
                    gv.Rows(gv.Rows.Count - 1).Cells("unit_code").Value = clsCommon.myCstr(dr1("unit_code"))
                    gv.Rows(gv.Rows.Count - 1).Cells("foc").Value = clsCommon.myCstr(dr1("foc"))
                    gv.Rows(gv.Rows.Count - 1).Cells("trans_qty").Value = clsCommon.myCstr(dr1("trans_qty"))
                Next
            End If

            Dim iCode As String = ""
            Dim iFOC As String = ""
            Dim iDoc_Code As String = ""

            Dim Doc_Code As String = ""
            Dim RCode As String = ""
            Dim RFOC As String = ""
            Dim RQty As Decimal = 0
            Dim RUnit As String = ""
            Dim CF As Decimal = 0
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    If qqty = 0 Then
                        Exit For
                    End If

                    '==================================knock-off on the basis of return
                    iCode = clsCommon.myCstr(dr("item_code"))
                    iFOC = clsCommon.myCstr(dr("FOC"))
                    iDoc_Code = clsCommon.myCstr(dr("doc_code"))
                    '==================qty comes after conversion===========
                    Dim iCF As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(dr("item_code")) + "' and uom_code='" + clsCommon.myCstr(dr("unit_code")) + "'")) / clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(dr("item_code")) + "' and uom_code='" + colItemUOM + "'"))
                    Dim transqty As Decimal = Nothing
                    Dim NewBal_Qty As Decimal = Nothing
                    transqty = System.Math.Round(clsCommon.myCdbl(dr("qty")) * clsCommon.myCdbl(iCF), 2)
                    NewBal_Qty = clsCommon.myCdbl(dr("qty"))
                    '==============================================

                    If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                        For ii As Integer = 0 To gv.Rows.Count - 1
                            RCode = clsCommon.myCstr(gv.Rows(ii).Cells("item_code").Value)
                            RFOC = clsCommon.myCstr(gv.Rows(ii).Cells("FOC").Value)
                            RUnit = clsCommon.myCstr(gv.Rows(ii).Cells("unit_code").Value)
                            RQty = clsCommon.myCdbl(gv.Rows(ii).Cells("trans_qty").Value)
                            Doc_Code = clsCommon.myCstr(gv.Rows(ii).Cells("doc_code").Value)

                            If clsCommon.myLen(RCode) > 0 AndAlso clsCommon.CompairString(RCode, iCode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(Doc_Code, iDoc_Code) = CompairStringResult.Equal AndAlso RQty > 0 Then
                                CF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + RCode + "' and uom_code='" + RUnit + "'")) / clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select conversion_factor from TSPL_ITEM_UOM_DETAIL where item_code='" + RCode + "' and uom_code='" + colItemUOM + "'"))
                                RQty = System.Math.Round(clsCommon.myCdbl(RQty) * clsCommon.myCdbl(CF), 2)

                                If transqty >= RQty Then
                                    transqty = transqty - RQty
                                    NewBal_Qty = NewBal_Qty - RQty
                                    gv.Rows(ii).Cells("trans_qty").Value = 0
                                ElseIf transqty < RQty Then
                                    transqty = 0
                                    gv.Rows(ii).Cells("trans_qty").Value = RQty - transqty
                                End If
                            End If ''end comp.
                        Next
                    End If ''end dt1

                    Return transqty
                Next
            End If

            Return 0
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            Me.Controls.Remove(gv)
        End Try
    End Function
End Class
