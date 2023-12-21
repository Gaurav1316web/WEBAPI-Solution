''Created By Monika
Imports common
Imports System.Data.SqlClient

Public Class FrmPOSchChild
    Inherits FrmMainTranScreen

#Region "Varaibles"
    Public Arr As List(Of clsPurchaseScheduleDetail) = Nothing
    Public _PO_Type As String = Nothing
    Public _Sch_Month As String = Nothing
    Public _Vendor_Code As String = Nothing
    Public _Sch_Doc_No As String = Nothing
    Public _Sch_type As String = Nothing
    Dim isInsideLoadData As Boolean = False

    Const colHSelect As String = "Select"
    Const colHDocNo As String = "DocNo"
    Const colHDate As String = "Date"
    Const colHPOType As String = "POType"

    Const colDSelect As String = "DSelect"
    Const colDPOCode As String = "DPOCode"
    Const colDPODate As String = "DPODate"
    Const colDICode As String = "DIcode"
    Const colDIname As String = "DIname"
    Const colDIUnit As String = "DIunit"
    Const colDQty As String = "DQty"
    Const colDSchQty As String = "DSchQty"
    Const colDPendingQty As String = "DPendingQty"
    Const colDLastSchMonth As String = "DSchMonth"
    Const colDGRNSRNQty As String = "DGRNQty"
#End Region

    Private Sub LoadBlankGrid()
        gv.Columns.Clear()
        gv.Rows.Clear()


        Dim reposelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        reposelect.Name = colHSelect
        reposelect.HeaderText = "Select"
        reposelect.Width = 60
        reposelect.FormatString = ""
        gv.MasterTemplate.Columns.Add(reposelect)

        Dim repodocno As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repodocno.FormatString = ""
        repodocno.HeaderText = "Order No"
        repodocno.Name = colHDocNo
        repodocno.Width = 130
        repodocno.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repodocno)

        repodocno = New GridViewTextBoxColumn()
        repodocno.FormatString = ""
        repodocno.HeaderText = "Order Date"
        repodocno.Name = colHDate
        repodocno.Width = 100
        repodocno.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repodocno)

        repodocno = New GridViewTextBoxColumn()
        repodocno.FormatString = ""
        repodocno.HeaderText = "Order Type"
        repodocno.Name = colHPOType
        repodocno.Width = 130
        repodocno.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repodocno)

        gv.AllowDeleteRow = False
        gv.AllowAddNewRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = True
        gv.AllowRowReorder = False
        gv.EnableSorting = False
        gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv.MasterTemplate.ShowRowHeaderColumn = False
        gv.TableElement.TableHeaderHeight = 40
    End Sub

    Private Sub LoadDetailBlankGrid()
        gv_detail.Columns.Clear()
        gv_detail.Rows.Clear()


        Dim reposelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        reposelect.Name = colDSelect
        reposelect.HeaderText = "Select"
        reposelect.Width = 60
        reposelect.FormatString = ""
        gv_detail.MasterTemplate.Columns.Add(reposelect)

        Dim repodocno As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repodocno.FormatString = ""
        repodocno.HeaderText = "Order No"
        repodocno.Name = colDPOCode
        repodocno.Width = 130
        repodocno.ReadOnly = True
        gv_detail.MasterTemplate.Columns.Add(repodocno)

        repodocno = New GridViewTextBoxColumn()
        repodocno.FormatString = ""
        repodocno.HeaderText = "Order Date"
        repodocno.Name = colDPODate
        repodocno.Width = 80
        repodocno.ReadOnly = True
        gv_detail.MasterTemplate.Columns.Add(repodocno)

        repodocno = New GridViewTextBoxColumn()
        repodocno.FormatString = ""
        repodocno.HeaderText = "Item Code"
        repodocno.Name = colDICode
        repodocno.Width = 130
        repodocno.ReadOnly = True
        gv_detail.MasterTemplate.Columns.Add(repodocno)

        repodocno = New GridViewTextBoxColumn()
        repodocno.FormatString = ""
        repodocno.HeaderText = "Description"
        repodocno.Name = colDIname
        repodocno.Width = 260
        repodocno.ReadOnly = True
        gv_detail.MasterTemplate.Columns.Add(repodocno)

        repodocno = New GridViewTextBoxColumn()
        repodocno.FormatString = ""
        repodocno.HeaderText = "Unit"
        repodocno.Name = colDIUnit
        repodocno.Width = 100
        repodocno.ReadOnly = True
        gv_detail.MasterTemplate.Columns.Add(repodocno)

        repodocno = New GridViewTextBoxColumn()
        repodocno.FormatString = ""
        repodocno.HeaderText = "Order Qty"
        repodocno.Name = colDQty
        repodocno.Width = 80
        repodocno.ReadOnly = True
        gv_detail.MasterTemplate.Columns.Add(repodocno)

        repodocno = New GridViewTextBoxColumn()
        repodocno.FormatString = ""
        repodocno.HeaderText = "Schedule Qty"
        repodocno.Name = colDSchQty
        repodocno.Width = 80
        repodocno.ReadOnly = True
        gv_detail.MasterTemplate.Columns.Add(repodocno)

        repodocno = New GridViewTextBoxColumn()
        repodocno.FormatString = ""
        repodocno.HeaderText = "Used in GRN/SRN/RGP(WO Sch.)"
        repodocno.WrapText = True
        repodocno.Name = colDGRNSRNQty
        repodocno.Width = 120
        repodocno.ReadOnly = True
        gv_detail.MasterTemplate.Columns.Add(repodocno)

        repodocno = New GridViewTextBoxColumn()
        repodocno.FormatString = ""
        repodocno.HeaderText = "Pending Qty"
        repodocno.Name = colDPendingQty
        repodocno.Width = 80
        repodocno.ReadOnly = True
        gv_detail.MasterTemplate.Columns.Add(repodocno)

        repodocno = New GridViewTextBoxColumn()
        repodocno.FormatString = ""
        repodocno.HeaderText = "Schedule Last Month"
        repodocno.Name = colDLastSchMonth
        repodocno.WrapText = True
        repodocno.Width = 100
        repodocno.ReadOnly = True
        gv_detail.MasterTemplate.Columns.Add(repodocno)

        gv_detail.AllowDeleteRow = False
        gv_detail.AllowAddNewRow = False
        gv_detail.ShowGroupPanel = False
        gv_detail.AllowColumnReorder = True
        gv_detail.AllowRowReorder = False
        gv_detail.EnableSorting = False
        gv_detail.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv_detail.MasterTemplate.ShowRowHeaderColumn = False
        gv_detail.TableElement.TableHeaderHeight = 40
    End Sub

    Private Sub FrmPOSchChild_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                btnclose.PerformClick()
            ElseIf e.KeyCode = Keys.F5 Then
                btnsave.PerformClick()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FrmPOSchChild_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadBlankGrid()
        LoadDetailBlankGrid()
        LoadData()
    End Sub

    Private Sub LoadData()
        Try
            Dim whrCls As String = "" 'no open po for schedule
            If clsCommon.myLen(_PO_Type) > 0 Then
                whrCls = " and axa.purchaseorder_type='" + _PO_Type + "'"
            End If

            Dim qry As String = "select distinct axa1.purchaseorder_no,axa1.purchaseorder_date,(case when axa1.purchaseorder_type='L' then 'Domestic' else case when axa1.purchaseorder_type='I' then 'Import' else case when axa1.purchaseorder_type='J' then 'Job Work' else case when axa1.purchaseorder_type='S' then 'Work Order(Service PO)' end end end end) as purchaseorder_type from ("
            qry += " select axa.purchaseorder_no,axa.purchaseorder_date,axa.purchaseorder_type,axa.item_code,axa.unit_code,sum(isnull(axa.Schedule_Qty,0)) as sch_qty,sum(axa.PurchaseOrder_Qty) as po_qty,sum(isnull(axa.grn_qty,0))+sum(isnull(axa.srn_qty,0))+sum(isnull(axa.rgp_qty,0)) as other_qty from ("
            qry += " select 0 as rgp_qty,0 as grn_qty,0 as srn_qty,0 as Schedule_Qty,TSPL_PURCHASE_ORDER_HEAD.purchaseorder_no,TSPL_PURCHASE_ORDER_HEAD.purchaseorder_date,TSPL_PURCHASE_ORDER_HEAD.purchaseorder_type,TSPL_PURCHASE_ORDER_DETAIL.item_code,TSPL_PURCHASE_ORDER_DETAIL.unit_code,isnull(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty,0) as PurchaseOrder_Qty "
            qry += " from TSPL_PURCHASE_ORDER_HEAD left outer join TSPL_PURCHASE_ORDER_DETAIL on TSPL_PURCHASE_ORDER_DETAIL.purchaseorder_no=TSPL_PURCHASE_ORDER_HEAD.purchaseorder_no where TSPL_PURCHASE_ORDER_HEAD.is_open_po=0 and TSPL_PURCHASE_ORDER_HEAD.vendor_code='" + _Vendor_Code + "' and TSPL_PURCHASE_ORDER_HEAD.status=1 "
            If clsCommon.CompairString(_Sch_type, "M") = CompairStringResult.Equal Then 'only year in case of monthly schedule , add date comparision cond. to check PO validation period ,is schedule month is in valid period or not.
                qry += " and (datepart(year,'" + _Sch_Month + "') between datepart(year,TSPL_PURCHASE_ORDER_HEAD.purchaseorder_date) and datepart(year,isnull(TSPL_PURCHASE_ORDER_HEAD.expiry_date,dateadd(year,1,TSPL_PURCHASE_ORDER_HEAD.purchaseorder_date))))"
            Else
                qry += " and (convert(date,'" + clsCommon.GetPrintDate(_Sch_Month, "dd/MMM/yyyy") + "',101) between convert(date,TSPL_PURCHASE_ORDER_HEAD.purchaseorder_date,101) and convert(date,isnull(TSPL_PURCHASE_ORDER_HEAD.expiry_date,dateadd(year,1,TSPL_PURCHASE_ORDER_HEAD.purchaseorder_date)),101)) "
            End If
            qry += " union all "
            '==============add grn and srn and rgp cond. if any po used in transaction then exclude the qty of used po.
            qry += " select 0 as rgp_qty,(isnull(grn_qty,0)) as grn_qty,0 as srn_qty,0 as Schedule_Qty,po_id as purchaseorder_no,TSPL_PURCHASE_ORDER_HEAD.purchaseorder_date,TSPL_PURCHASE_ORDER_HEAD.purchaseorder_type,item_code,unit_code,0 as PurchaseOrder_Qty from TSPL_GRN_DETAIL left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_GRN_DETAIL.PO_Id where len(isnull(TSPL_GRN_DETAIL.po_id,''))>0 and len(isnull(TSPL_GRN_DETAIL.Against_Schedule_Code,''))<=0 and len(isnull(TSPL_GRN_DETAIL.Against_RGP_No,''))<=0 " '+isnull(leak_qty,0)+isnull(burst_qty,0)+isnull(short_qty,0)
            qry += " union all "
            qry += " select 0 as rgp_qty,0 as grn_qty,(isnull(srn_qty,0)) as srn_qty,0 as Schedule_Qty,po_id as purchaseorder_no,TSPL_PURCHASE_ORDER_HEAD.purchaseorder_date,TSPL_PURCHASE_ORDER_HEAD.purchaseorder_type,item_code,unit_code,0 as PurchaseOrder_Qty from TSPL_SRN_DETAIL left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_SRN_DETAIL.PO_Id where len(isnull(TSPL_SRN_DETAIL.po_id,''))>0 and len(isnull(TSPL_SRN_DETAIL.Against_Schedule_Code,''))<=0 and len(isnull(TSPL_SRN_DETAIL.RGP_Id,''))<=0 and len(isnull(TSPL_SRN_DETAIL.MRN_Id,''))<=0 " '+isnull(rejected_qty,0)+isnull(leak_qty,0)+isnull(burst_qty,0)+isnull(short_qty,0)
            qry += " union all "
            qry += " select (isnull(rgp_qty,0)) as rgp_qty,0 as grn_qty,0 as srn_qty,0 as schedule_qty,po_id as purchaseorder_no,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Type,item_code,unit_code,0 as purchaseorder_qty from TSPL_RGP_JOB_WORK_DETAIL left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_RGP_JOB_WORK_DETAIL.PO_Id where len(isnull(TSPL_RGP_JOB_WORK_DETAIL.po_id,''))>0 and len(isnull(TSPL_RGP_JOB_WORK_DETAIL.Against_Schedule_Code,''))<=0 "
            '==============================================================================================================
            qry += " union all "
            qry += " select 0 as rgp_qty,0 as grn_qty,0 as srn_qty,isnull(Schedule_Qty,0) as Schedule_Qty,po_code as purchaseorder_no,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Type,item_code,unit_code,0 as purchaseorder_qty from TSPL_PO_SCH_DETAIL left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PO_SCH_DETAIL.PO_Code where TSPL_PO_SCH_DETAIL.document_code not in ('" + _Sch_Doc_No + "') "

            qry += "  )axa "
            qry += " where 1=1 " + whrCls + " group by axa.purchaseorder_no,axa.purchaseorder_date,axa.purchaseorder_type,axa.item_code,axa.unit_code "
            qry += " )axa1 where (axa1.po_qty - axa1.sch_qty-axa1.other_qty)>0"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            isInsideLoadData = True
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    gv.Rows.AddNew()

                    gv.Rows(gv.Rows.Count - 1).Cells(colHSelect).Value = False
                    gv.Rows(gv.Rows.Count - 1).Cells(colHDocNo).Value = clsCommon.myCstr(dr("purchaseorder_no"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colHDate).Value = clsCommon.myCstr(dr("purchaseorder_date"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colHPOType).Value = clsCommon.myCstr(dr("purchaseorder_type"))
                Next
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found.", Me.Text)
            End If

            isInsideLoadData = False
        Catch ex As Exception
            isInsideLoadData = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Try
            Arr = New List(Of clsPurchaseScheduleDetail)

            For Each grow As GridViewRowInfo In gv_detail.Rows
                If clsCommon.myCBool(grow.Cells(colDSelect).Value) = True Then
                    Dim objtr As New clsPurchaseScheduleDetail()

                    objtr.Item_Code = clsCommon.myCstr(grow.Cells(colDICode).Value)
                    objtr.Unit_Code = clsCommon.myCstr(grow.Cells(colDIUnit).Value)
                    objtr.PO_Code = clsCommon.myCstr(grow.Cells(colDPOCode).Value)
                    objtr.PO_Date = clsCommon.myCDate(grow.Cells(colDPODate).Value)
                    objtr.PO_Qty = clsCommon.myCdbl(grow.Cells(colDPendingQty).Value)

                    Arr.Add(objtr)
                End If
            Next
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

        Me.Close()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Arr = Nothing
        Me.Close()
    End Sub

    Private Sub gv_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellValueChanged
        
    End Sub

    Private Sub RemoveDetailRow(ByVal PO_Code As String)
        If clsCommon.myLen(PO_Code) > 0 Then
            For ii As Integer = gv_detail.Rows.Count - 1 To 0 Step -1
                If clsCommon.myLen(gv_detail.Rows(ii).Cells(colDPOCode).Value) > 0 AndAlso clsCommon.CompairString(PO_Code, clsCommon.myCstr(gv_detail.Rows(ii).Cells(colDPOCode).Value)) = CompairStringResult.Equal Then
                    gv_detail.Rows.RemoveAt(ii)
                End If
            Next
        End If
    End Sub

    Private Sub LoadDetail(ByVal PO_Code As String)
        If clsCommon.myLen(PO_Code) > 0 Then
            Dim qry As String = "select axa1.purchaseorder_no,axa1.purchaseorder_date,axa1.purchaseorder_type,axa1.item_code,axa1.unit_code,axa1.po_qty,axa1.sch_qty,(axa1.po_qty - axa1.sch_qty-axa1.other_qty) as Pending_qty,axa1.other_qty from ("
            qry += " select axa.purchaseorder_no,axa.purchaseorder_date,axa.purchaseorder_type,axa.item_code,axa.unit_code,sum(isnull(axa.Schedule_Qty,0)) as sch_qty,sum(axa.PurchaseOrder_Qty) as po_qty,sum(isnull(axa.grn_qty,0))+sum(isnull(axa.srn_qty,0))+sum(isnull(axa.rgp_qty,0)) as other_qty from ("
            qry += " select 0 as rgp_qty,0 as grn_qty,0 as srn_qty,0 as Schedule_Qty,TSPL_PURCHASE_ORDER_HEAD.purchaseorder_no,TSPL_PURCHASE_ORDER_HEAD.purchaseorder_date,TSPL_PURCHASE_ORDER_HEAD.purchaseorder_type,TSPL_PURCHASE_ORDER_DETAIL.item_code,TSPL_PURCHASE_ORDER_DETAIL.unit_code,isnull(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty,0) as PurchaseOrder_Qty "
            qry += " from TSPL_PURCHASE_ORDER_HEAD left outer join TSPL_PURCHASE_ORDER_DETAIL on TSPL_PURCHASE_ORDER_DETAIL.purchaseorder_no=TSPL_PURCHASE_ORDER_HEAD.purchaseorder_no where TSPL_PURCHASE_ORDER_HEAD.is_open_po=0 and TSPL_PURCHASE_ORDER_HEAD.vendor_code='" + _Vendor_Code + "' and TSPL_PURCHASE_ORDER_HEAD.status=1 "
            If clsCommon.CompairString(_Sch_type, "M") = CompairStringResult.Equal Then
                qry += " and (datepart(year,'" + _Sch_Month + "') between datepart(year,TSPL_PURCHASE_ORDER_HEAD.purchaseorder_date) and datepart(year,isnull(TSPL_PURCHASE_ORDER_HEAD.expiry_date,dateadd(year,1,TSPL_PURCHASE_ORDER_HEAD.purchaseorder_date))))"
            Else
                qry += " and (convert(date,'" + clsCommon.GetPrintDate(_Sch_Month, "dd/MMM/yyyy") + "',101) between convert(date,TSPL_PURCHASE_ORDER_HEAD.purchaseorder_date,101) and convert(date,isnull(TSPL_PURCHASE_ORDER_HEAD.expiry_date,dateadd(year,1,TSPL_PURCHASE_ORDER_HEAD.purchaseorder_date)),101))"
            End If
            qry += " union all "
            '==============add grn and srn and rgp cond. if any po used in transaction then exclude the qty of used po.
            qry += " select 0 as rgp_qty,(isnull(grn_qty,0)) as grn_qty,0 as srn_qty,0 as Schedule_Qty,po_id as purchaseorder_no,TSPL_PURCHASE_ORDER_HEAD.purchaseorder_date,TSPL_PURCHASE_ORDER_HEAD.purchaseorder_type,item_code,unit_code,0 as PurchaseOrder_Qty from TSPL_GRN_DETAIL left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_GRN_DETAIL.PO_Id where len(isnull(TSPL_GRN_DETAIL.po_id,''))>0 and len(isnull(TSPL_GRN_DETAIL.Against_Schedule_Code,''))<=0 and len(isnull(TSPL_GRN_DETAIL.Against_RGP_No,''))<=0 " '+isnull(leak_qty,0)+isnull(burst_qty,0)+isnull(short_qty,0)
            qry += " union all "
            qry += " select 0 as rgp_qty,0 as grn_qty,(isnull(srn_qty,0)) as srn_qty,0 as Schedule_Qty,po_id as purchaseorder_no,TSPL_PURCHASE_ORDER_HEAD.purchaseorder_date,TSPL_PURCHASE_ORDER_HEAD.purchaseorder_type,item_code,unit_code,0 as PurchaseOrder_Qty from TSPL_SRN_DETAIL left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_SRN_DETAIL.PO_Id where len(isnull(TSPL_SRN_DETAIL.po_id,''))>0 and len(isnull(TSPL_SRN_DETAIL.Against_Schedule_Code,''))<=0 and len(isnull(TSPL_SRN_DETAIL.RGP_Id,''))<=0 and len(isnull(TSPL_SRN_DETAIL.MRN_Id,''))<=0 " '+isnull(rejected_qty,0)+isnull(leak_qty,0)+isnull(burst_qty,0)+isnull(short_qty,0)
            qry += " union all "
            qry += " select (isnull(rgp_qty,0)) as rgp_qty,0 as grn_qty,0 as srn_qty,0 as Schedule_Qty,po_id as purchaseorder_no,TSPL_PURCHASE_ORDER_HEAD.purchaseorder_date,TSPL_PURCHASE_ORDER_HEAD.purchaseorder_type,item_code,unit_code,0 as PurchaseOrder_Qty from TSPL_RGP_JOB_WORK_DETAIL left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_RGP_JOB_WORK_DETAIL.PO_Id where len(isnull(TSPL_RGP_JOB_WORK_DETAIL.po_id,''))>0 and len(isnull(TSPL_RGP_JOB_WORK_DETAIL.Against_Schedule_Code,''))<=0 "
            '==============================================================================================================
            qry += " union all "
            qry += " select 0 as rgp_qty,0 as grn_qty,0 as srn_qty,isnull(Schedule_Qty,0) as Schedule_Qty,po_code as purchaseorder_no,TSPL_PURCHASE_ORDER_HEAD.purchaseorder_date,TSPL_PURCHASE_ORDER_HEAD.purchaseorder_type,item_code,unit_code,0 as PurchaseOrder_Qty from TSPL_PO_SCH_DETAIL left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PO_SCH_DETAIL.PO_Code where TSPL_PO_SCH_DETAIL.document_code not in ('" + _Sch_Doc_No + "') "

            qry += "  )axa "
            qry += " group by axa.purchaseorder_no,axa.purchaseorder_date,axa.purchaseorder_type,axa.item_code,axa.unit_code "
            qry += " )axa1 where purchaseorder_no='" + PO_Code + "' and (axa1.po_qty - axa1.sch_qty-axa1.other_qty)>0"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            '=in (select document_code from TSPL_PO_SCH_HEAD where month(schedule_month)=month('" + clsCommon.GetPrintDate(_Sch_Month, "dd/MMM/yyyy") + "') and year(schedule_month)=year('" + clsCommon.GetPrintDate(_Sch_Month, "dd/MMM/yyyy") + "'))

            isInsideLoadData = True
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    gv_detail.Rows.AddNew()

                    gv_detail.Rows(gv_detail.Rows.Count - 1).Cells(colDSelect).Value = True
                    gv_detail.Rows(gv_detail.Rows.Count - 1).Cells(colDPOCode).Value = clsCommon.myCstr(dr("purchaseorder_no"))
                    gv_detail.Rows(gv_detail.Rows.Count - 1).Cells(colDPODate).Value = clsCommon.myCstr(dr("purchaseorder_date"))
                    gv_detail.Rows(gv_detail.Rows.Count - 1).Cells(colDICode).Value = clsCommon.myCstr(dr("item_code"))
                    gv_detail.Rows(gv_detail.Rows.Count - 1).Cells(colDIname).Value = clsItemMaster.GetItemName(clsCommon.myCstr(dr("item_code")), Nothing)
                    gv_detail.Rows(gv_detail.Rows.Count - 1).Cells(colDIUnit).Value = clsCommon.myCstr(dr("unit_code"))
                    gv_detail.Rows(gv_detail.Rows.Count - 1).Cells(colDQty).Value = clsCommon.myCdbl(dr("po_qty"))
                    gv_detail.Rows(gv_detail.Rows.Count - 1).Cells(colDPendingQty).Value = clsCommon.myCdbl(dr("pending_qty"))
                    gv_detail.Rows(gv_detail.Rows.Count - 1).Cells(colDSchQty).Value = clsCommon.myCdbl(dr("sch_qty")) 'clsCommon.myCstr(clsDBFuncationality.getSingleValue("select sum(schedule_qty) from TSPL_PO_SCH_DETAIL where item_code='" + clsCommon.myCstr(dr("item_code")) + "' and document_code in (select document_code from TSPL_PO_SCH_HEAD where month(schedule_month)<month('" + _Sch_Month + "') and year(schedule_month)<year('" + _Sch_Month + "'))"))
                    gv_detail.Rows(gv_detail.Rows.Count - 1).Cells(colDGRNSRNQty).Value = clsCommon.myCdbl(dr("other_qty"))

                    Dim lastmonth As String = ""
                    If clsCommon.CompairString(_Sch_type, "M") = CompairStringResult.Equal Then
                        lastmonth = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 schedule_month from TSPL_PO_SCH_HEAD where year(schedule_month)<year('" + _Sch_Month + "') and document_code in (select document_code from TSPL_PO_SCH_DETAIL where item_code='" + clsCommon.myCstr(dr("item_code")) + "') order by schedule_month desc"))
                    Else
                        lastmonth = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 schedule_month from TSPL_PO_SCH_HEAD where schedule_month < '" + clsCommon.GetPrintDate(_Sch_Month, "dd/MMM/yyyy") + "' and document_code in (select document_code from TSPL_PO_SCH_DETAIL where item_code='" + clsCommon.myCstr(dr("item_code")) + "') order by schedule_month desc"))
                    End If

                    If clsCommon.myLen(lastmonth) > 0 Then
                        gv_detail.Rows(gv_detail.Rows.Count - 1).Cells(colDLastSchMonth).Value = clsCommon.GetPrintDate(lastmonth, "MMM yyyy")
                    Else
                        gv_detail.Rows(gv_detail.Rows.Count - 1).Cells(colDLastSchMonth).Value = Nothing
                    End If

                Next
            End If
            isInsideLoadData = False
        End If
    End Sub

    Private Sub gv_ValueChanging(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.ValueChangingEventArgs) Handles gv.ValueChanging
        Try
            If Not isInsideLoadData Then
                If e.NewValue Then
                    Dim potype As String = clsCommon.myCstr(gv.CurrentRow.Cells(colHPOType).Value)

                    For Each grow As GridViewRowInfo In gv.Rows
                        If grow.Index <> gv.CurrentRow.Index AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colHPOType).Value), potype) <> CompairStringResult.Equal Then
                            Throw New Exception("Cannot select current document.it is PO Type: " + potype + ".")
                        End If
                    Next

                    LoadDetail(clsCommon.myCstr(gv.CurrentRow.Cells(colHDocNo).Value))
                Else
                    RemoveDetailRow(clsCommon.myCstr(gv.CurrentRow.Cells(colHDocNo).Value))
                End If
            End If
        Catch ex As Exception
            isInsideLoadData = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
