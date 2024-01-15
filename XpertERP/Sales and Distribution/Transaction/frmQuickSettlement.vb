'Developed By -BibhuPrasad Parida
'Database - TSPLERP
'Table - tspl_QuickSettleMent
'Start Date -
'End Date -
'--14/12/2012-1:15PM--Updation By--Pankaj Kumar----Change SalesMan in Sale Invoice, Shipment, Empty Transaction Against Transfer------Fwd By---Ranjana Mam
'by vipin for check post status on update on 04/02/2013

Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports common

Public Class FrmQuickSettlement
    Inherits FrmMainTranScreen
#Region "Constructor"
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
#End Region
#Region "Variables"

    Const colSettlementCode As String = "SettleMentCode"
    Const colDescription As String = "Description"
    Const colAmount As String = "Amount1"
    Const colRemarks As String = "Remarks"
    Const ColSettlementType As String = "SettlementType"

    Const colItemCode As String = "colItemCode"
    Const colItemName As String = "colItemName"
    Const colLoadOutQty As String = "colLoadQty"
    Const colLoadInQtyFC As String = "colLoadInQtyFC"
    Const colLoadInQtyFB As String = "colLoadInQtyFB"
    Const colTotalLoadInQtyFC As String = "colTotalLoadInQtyFC"
    Const colProvisionalSale As String = "colProvisionalSale"
    Const colRetailerPrice As String = "colRetailerPrice"
    Const colAmountLoadOut As String = "colAmountLoadOut"


    Public LoadInNO As String = Nothing
    Public IsFillProceed As Boolean = False

    Dim sql As String
    Dim ds As DataSet
    Dim dr As DataTable
    Dim balanceamt1 As Decimal = 0

    Dim tableName As String = "tspl_SettleMent_Master"
    Dim userCode, companyCode As String
    Dim btntooltip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Public clicked As Boolean = False
#End Region

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmQuickSettlement)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
           
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub uslock()
        Dim qry As String = clsDBFuncationality.getSingleValue("select isnull(tspl_QuickSettleMent.post,'') from tspl_QuickSettleMent where Quick_SettleMent_Id='" + fndQuickSettlement.Value + "'")
        If qry = "Y" Then
            UsLock1.Status = ERPTransactionStatus.Approved
        Else
            UsLock1.Status = ERPTransactionStatus.Pending
        End If
    End Sub
    Public Sub SetLength()
        fndQuickSettlement.MyMaxLength = 30
        txtComments.MaxLength = 500

    End Sub
    Private Sub FrmQuickSettlement_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetLength()
        SetUserMgmtNew()
        funReset()
        ' fndTransferNumber.txtValue.MaxLength = 30
        'Dim cd1 As GridViewDecimalColumn = TryCast(dgvQuickSettleMent.Columns(colAmount), GridViewDecimalColumn)
        Dim cd As GridViewTextBoxColumn = TryCast(dgvQuickSettleMent.Columns(colRemarks), GridViewTextBoxColumn)
        cd.MaxLength = 50
        'globalFunc.mandatoryText(fndTransferNumber.txtValue)
        btntooltip.SetToolTip(btnSave, "Press Alt+S for save/update data")
        btntooltip.SetToolTip(btnDelete, "Press Alt+D for delete data")
        btntooltip.SetToolTip(btnNew, "Press Alt+N for new transaction")
        btntooltip.SetToolTip(btnPost, "Press Alt+P for Post data")
        btntooltip.SetToolTip(btnClose, "Press Esc for close data")
        btntooltip.SetToolTip(btnPrint, "Press Alt+R for Print report")
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        uslock()
        btnPostfinnancial.Visible = True
        If clsCommon.myLen(Me.Tag) > 0 Then
            fndQuickSettlement.Value = clsCommon.myCstr(Me.Tag)
            funFill(clsCommon.myCstr(Me.Tag))
            End If
    End Sub

    'Sub LoadBlankGrid()
    '    gv1.MasterTemplate.SummaryRowsBottom.Clear()
    '    gv1.Rows.Clear()
    '    gv1.Columns.Clear()

    '    Dim repoItemCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    '    repoItemCode = New GridViewTextBoxColumn()
    '    repoItemCode.FormatString = ""
    '    repoItemCode.HeaderText = "Item Code"
    '    repoItemCode.Name = colItemCode
    '    repoItemCode.Width = 80
    '    repoItemCode.ReadOnly = True
    '    repoItemCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
    '    gv1.MasterTemplate.Columns.Add(repoItemCode)


    '    Dim repoItemName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    '    repoItemName = New GridViewTextBoxColumn()
    '    repoItemName.FormatString = ""
    '    repoItemName.HeaderText = "Item Name"
    '    repoItemName.Name = colItemName
    '    repoItemName.Width = 150
    '    repoItemName.ReadOnly = True
    '    repoItemName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
    '    gv1.MasterTemplate.Columns.Add(repoItemName)


    '    Dim repoLoadOutQty As GridViewDecimalColumn = New GridViewDecimalColumn()
    '    repoLoadOutQty = New GridViewDecimalColumn()
    '    repoLoadOutQty.FormatString = ""
    '    repoLoadOutQty.HeaderText = "LoadOut Qty"
    '    repoLoadOutQty.Name = colLoadOutQty
    '    repoLoadOutQty.Width = 83
    '    repoLoadOutQty.ReadOnly = True
    '    repoLoadOutQty.FormatString = "{0:F2}"
    '    repoLoadOutQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '    gv1.MasterTemplate.Columns.Add(repoLoadOutQty)


    '    Dim repoLoadInFC As GridViewDecimalColumn = New GridViewDecimalColumn()
    '    repoLoadInFC = New GridViewDecimalColumn()
    '    repoLoadInFC.FormatString = ""
    '    repoLoadInFC.HeaderText = "LoadIn Qty(FC)"
    '    repoLoadInFC.Name = colLoadInQtyFC
    '    repoLoadInFC.Width = 80
    '    repoLoadInFC.ReadOnly = True
    '    repoLoadInFC.FormatString = "{0:F2}"
    '    repoLoadInFC.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '    gv1.MasterTemplate.Columns.Add(repoLoadInFC)


    '    Dim repoLoadInFB As GridViewDecimalColumn = New GridViewDecimalColumn()
    '    repoLoadInFB = New GridViewDecimalColumn()
    '    repoLoadInFB.FormatString = ""
    '    repoLoadInFB.HeaderText = "LoadIn Qty(FB)"
    '    repoLoadInFB.Name = colLoadInQtyFB
    '    repoLoadInFB.Width = 90
    '    repoLoadInFB.ReadOnly = True
    '    repoLoadInFB.FormatString = "{0:F2}"
    '    repoLoadInFB.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '    gv1.MasterTemplate.Columns.Add(repoLoadInFB)


    '    Dim repoTotLoadInFC As GridViewDecimalColumn = New GridViewDecimalColumn()
    '    repoTotLoadInFC = New GridViewDecimalColumn()
    '    repoTotLoadInFC.FormatString = ""
    '    repoTotLoadInFC.HeaderText = "Total LoadIn Qty(FC)"
    '    repoTotLoadInFC.Name = colTotalLoadInQtyFC
    '    repoTotLoadInFC.Width = 90
    '    repoTotLoadInFC.ReadOnly = True
    '    repoTotLoadInFC.FormatString = "{0:F2}"
    '    repoTotLoadInFC.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '    gv1.MasterTemplate.Columns.Add(repoTotLoadInFC)

    '    Dim repoPSQty As GridViewDecimalColumn = New GridViewDecimalColumn()
    '    repoPSQty = New GridViewDecimalColumn()
    '    repoPSQty.FormatString = ""
    '    repoPSQty.HeaderText = "Provisional Sale Qty(FC)"
    '    repoPSQty.Name = colProvisionalSale
    '    repoPSQty.Width = 90
    '    repoPSQty.ReadOnly = True
    '    repoPSQty.FormatString = "{0:F2}"
    '    repoPSQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '    gv1.MasterTemplate.Columns.Add(repoPSQty)

    '    Dim repoRetailerPrice As GridViewDecimalColumn = New GridViewDecimalColumn()
    '    repoRetailerPrice = New GridViewDecimalColumn()
    '    repoRetailerPrice.FormatString = ""
    '    repoRetailerPrice.HeaderText = "Retailer Price"
    '    repoRetailerPrice.Name = colRetailerPrice
    '    repoRetailerPrice.Width = 90
    '    repoRetailerPrice.ReadOnly = True
    '    repoRetailerPrice.FormatString = "{0:F2}"
    '    repoRetailerPrice.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '    gv1.MasterTemplate.Columns.Add(repoRetailerPrice)

    '    Dim repoLoadOutAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
    '    repoLoadOutAmt = New GridViewDecimalColumn()
    '    repoLoadOutAmt.FormatString = ""
    '    repoLoadOutAmt.HeaderText = "Amount"
    '    repoLoadOutAmt.Name = colAmountLoadOut
    '    repoLoadOutAmt.Width = 90
    '    repoLoadOutAmt.ReadOnly = True
    '    repoLoadOutAmt.FormatString = "{0:F2}"
    '    repoLoadOutAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '    gv1.MasterTemplate.Columns.Add(repoLoadOutAmt)

    '    gv1.AllowDeleteRow = True
    '    gv1.AllowAddNewRow = True
    '    gv1.EnableFiltering = True
    '    gv1.ShowGroupPanel = False
    '    gv1.AllowColumnReorder = False
    '    gv1.AllowRowReorder = False
    '    gv1.EnableSorting = False
    '    gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
    '    gv1.MasterTemplate.ShowRowHeaderColumn = False
    '    gv1.TableElement.TableHeaderHeight = 40

    '    Dim summaryRowItem As New GridViewSummaryRowItem()
    '    Dim item1 As New GridViewSummaryItem(colLoadOutQty, "{0:F2}", GridAggregateFunction.Sum)
    '    summaryRowItem.Add(item1)
    '    Dim item2 As New GridViewSummaryItem(colLoadInQtyFC, "{0:F2}", GridAggregateFunction.Sum)
    '    summaryRowItem.Add(item2)
    '    Dim item3 As New GridViewSummaryItem(colLoadInQtyFB, "{0:F2}", GridAggregateFunction.Sum)
    '    summaryRowItem.Add(item3)
    '    Dim item4 As New GridViewSummaryItem(colTotalLoadInQtyFC, "{0:F2}", GridAggregateFunction.Sum)
    '    summaryRowItem.Add(item4)
    '    Dim item5 As New GridViewSummaryItem(colProvisionalSale, "{0:F2}", GridAggregateFunction.Sum)
    '    summaryRowItem.Add(item5)
    '    Dim item6 As New GridViewSummaryItem(colRetailerPrice, "{0:F2}", GridAggregateFunction.Sum)
    '    summaryRowItem.Add(item6)
    '    Dim item7 As New GridViewSummaryItem(colAmountLoadOut, "{0:F2}", GridAggregateFunction.Sum)
    '    summaryRowItem.Add(item7)
    '    gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    'End Sub

    Private Sub fndTransferNumber_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        'If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
        '    e.Handled = True
        'End If
    End Sub
    Private Sub GridBind()
        ds = connectSql.RunSQLReturnDS(" select  SettleMentCode ,Description,0.00 as Amount ,isnull(SettleMent_Type,'Null') as SettleMent_Type from tspl_SettleMent_Master where Type='Q' or Type='B' order by Sequence_No ")
        dgvQuickSettleMent.DataSource = ds.Tables(0)
        dgvQuickSettleMent.Columns(colSettlementCode).FieldName = "SettleMentCode"
        dgvQuickSettleMent.Columns(colDescription).FieldName = "Description"
        dgvQuickSettleMent.Columns(colAmount).FieldName = "Amount"
        dgvQuickSettleMent.Columns(ColSettlementType).FieldName = "SettleMent_Type"
    End Sub
    Sub fndTransferNumber_TextChanged()

    Dim balanceamt As Decimal = 0
    Dim loadinamt As Double = 0
    Dim as1 As String = "SELECT sum(ISNULL(D.BasicPrice_WithTax*D.LoadIn_Qty  ,0) + (CASE WHEN D.Uom = 'FC' THEN ISNULL(D.Empty_Value *LoadIn_Qty ,0) ELSE ISNULL(D.Empty_Value,0) END )+ ISNULL(D.TPT_Value *LoadIn_Qty ,0)) AS total FROM TSPL_TRANSFER_DETAIL D JOIN TSPL_TRANSFER_HEAD H ON H.Transfer_No = D.Transfer_No WHERE H.Load_Out_No  = '" + fndTransferNumber.Value + "' AND H.Transfer_Type = 'LI'"
    Dim adjustmentamt As Decimal = 0
    loadinamt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Total_Transfer_Amount from TSPL_TRANSFER_HEAD where Load_Out_No='" + fndTransferNumber.Value + "' and Transfer_Type = 'LI'"))
        adjustmentamt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum( case when Trans_Type ='IN' then  ISNULL(d.Item_Cost ,0) + ISNULL(d.Breakage_Cost ,0) else (ISNULL(d.Item_Cost ,0) + ISNULL(d.Breakage_Cost ,0)) *-1  end) as total from TSPL_ADJUSTMENT_HEADER h join TSPL_ADJUSTMENT_DETAIL d on h.Adjustment_No = d.Adjustment_No where h.Document_No = '" + fndTransferNumber.Value + "'"))
        dr = clsDBFuncationality.GetDataTable("select * from TSPL_TRANSFER_HEAD where Transfer_No ='" + fndTransferNumber.Value + "'")
        Dim s As String = ""
        If dr IsNot Nothing AndAlso dr.Rows.Count > 0 Then
            s = dr.Rows(0)(0).ToString()
        End If

        If s <> fndTransferNumber.Value Then
            txtTransferDate.Text = ""
            txtAmount.Text = ""
        Else
            Dim strvalue As String = "select TSPL_LOCATION_MASTER.Location_Desc,TSPL_ROUTE_MASTER.Route_No ,TSPL_ROUTE_MASTER.Route_Desc,convert(varchar(10),TSPL_TRANSFER_HEAD.Transfer_Date,103) as date   from  TSPL_TRANSFER_HEAD  join TSPL_LOCATION_MASTER on TSPL_TRANSFER_HEAD.To_Location =TSPL_LOCATION_MASTER.Location_Code  left outer join TSPL_ROUTE_MASTER  on TSPL_TRANSFER_HEAD.Route_No=TSPL_ROUTE_MASTER.Route_No where  TSPL_TRANSFER_HEAD.Transfer_No ='" + fndTransferNumber.Value + "'"
            dr = clsDBFuncationality.GetDataTable(strvalue)

            If dr IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                txtSalesman.Text = dr.Rows(0)(0).ToString()
                txtRouteNo.Text = dr.Rows(0)(1).ToString()
                txtRoutedescription.Text = dr.Rows(0)(2).ToString()
                txtTransferDate.Text = dr.Rows(0)(3).ToString()
                txtQSDate.Value = clsCommon.myCDate(dr.Rows(0)(3))
            End If
            Dim qry As String = "select Total_Transfer_Amount from TSPL_TRANSFER_HEAD where Transfer_No='" + fndTransferNumber.Value + "'"

            txtAmount.Text = clsCommon.myFormat(Math.Round(clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)), 2))
            balanceamt = clsCommon.myCdbl(txtAmount.Text) - loadinamt - adjustmentamt
            txtBalanceAmount.Text = Math.Round(balanceamt, 2)
            balanceamt1 = balanceamt
    '--------------------------Show Load In Amount-----------------------
            txtLoadInAmount.Text = clsCommon.myFormat(Math.Round(loadinamt, 2))
            txtempty.Text = clsCommon.myFormat(Math.Round(adjustmentamt, 2))
    '  ------------------------ Vehicle Number------------------
    Dim strVehicle As String = clsCommon.myCstr(connectSql.RunScalar(" select Vehicle_Code from TSPL_TRANSFER_HEAD where Transfer_No ='" + fndTransferNumber.Value + "'"))
            txtVehicleNo.Text = strVehicle

            fndSalesmanCode.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select To_Location   from TSPL_TRANSFER_HEAD where Transfer_No ='" + fndTransferNumber.Value + "'"))
            ' ------------------------ End Vehicle ------------------
        End If
        RadLabel4.Text = txtBalanceAmount.Text
    End Sub
    Private Sub LoadEmptyVal()
        Dim dt As DataTable
        Dim strempty As String = "select Adjustment_No,Item_Code,Item_Cost from TSPL_ADJUSTMENT_DETAIL where Adjustment_No in (select Adjustment_No  from TSPL_ADJUSTMENT_HEADER  where ItemType='e' and Reference_Document='Load Out/Transfer' and Document_No='" + fndTransferNumber.Value + "')"
        dt = clsDBFuncationality.GetDataTable(strempty)
        Dim emptyamt As Double = 0
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                emptyamt = emptyamt + clsCommon.myCdbl(dt.Rows(i)("Item_cost"))
            Next
        End If
        txtempty.Text = Math.Round(emptyamt, 2)
    End Sub

    Private Sub funFill(ByVal StrDocNo As String)
        Dim obj As New ClsQuickSettlementMaster()
        obj = ClsQuickSettlementMaster.GetData(StrDocNo)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Quick_SettleMent_Id) > 0) Then
            IsFillProceed = True
            fndTransferNumber.Enabled = False
            fndQuickSettlement.Value = obj.Quick_SettleMent_Id
            txtQSDate.Value = obj.Quick_Settlement_Date
            fndTransferNumber.Value = obj.Transfer_Number
            txtRouteNo.Text = obj.RouteNo
            txtRoutedescription.Text = obj.RouteDescription
            txtVehicleNo.Text = obj.VehicleNo
            txtSalesman.Text = obj.Salesman
            txtTransferDate.Text = obj.Transfer_Date
            txtAmount.Text = Math.Round(obj.Transfer_Amount, 2)
            txtComments.Text = obj.Comments
            txtLoadInAmount.Text = Math.Round(obj.Load_In_Amount, 2)
            txtempty.Text = Math.Round(clsCommon.myCdbl(obj.Empty_Load_In), 2)
            fndSalesmanCode.Value = obj.Salesman_code
            txtCashMemo.Text = obj.CashMemo
            If clsCommon.myCdbl(obj.Empty_Load_In) > 0 Then
                txtBalanceAmount.Text = Math.Round(obj.Balance_Amount, 2)
            Else
                txtBalanceAmount.Text = Math.Round(obj.Balance_Amount - clsCommon.myCdbl(txtempty.Text), 2)
            End If
            RadLabel4.Text = clsCommon.myCdbl(txtAmount.Text) - clsCommon.myCdbl(txtLoadInAmount.Text) - clsCommon.myCdbl(txtempty.Text)
            isNewEntry = False
            If obj.Post Then
                btnSave.Text = "Update"
                btnSave.Enabled = True
                btnPost.Enabled = True
                btnDelete.Enabled = False
                btnPrint.Enabled = True
                UsLock1.Status = ERPTransactionStatus.Approved
            Else
                btnSave.Text = "Update"
                btnSave.Enabled = True
                btnDelete.Enabled = True
                btnPost.Enabled = True
                btnPrint.Enabled = True
                UsLock1.Status = ERPTransactionStatus.Pending
            End If
        End If

        If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
            dgvQuickSettleMent.DataSource = Nothing
            dgvQuickSettleMent.Rows.Clear()
            For Each objTr As ClsQuickSettlementDetail In obj.Arr
                dgvQuickSettleMent.Rows.AddNew()
                dgvQuickSettleMent.Rows(dgvQuickSettleMent.Rows.Count - 1).Cells(colSettlementCode).Value = objTr.SettleMent_Code
                dgvQuickSettleMent.Rows(dgvQuickSettleMent.Rows.Count - 1).Cells(colDescription).Value = objTr.Description
                dgvQuickSettleMent.Rows(dgvQuickSettleMent.Rows.Count - 1).Cells(colAmount).Value = objTr.Amount
                dgvQuickSettleMent.Rows(dgvQuickSettleMent.Rows.Count - 1).Cells(colRemarks).Value = objTr.Remarks
                dgvQuickSettleMent.Rows(dgvQuickSettleMent.Rows.Count - 1).Cells(ColSettlementType).Value = objTr.SettlementType
            Next
        End If

        Dim frm As New FrmViewLoadOutDetail(fndTransferNumber.Value, fndQuickSettlement.Value, userCode, companyCode)
        frm.Show()
        frm.Close()
        'fndTransferNumber_TextChanged()
        txtProvisionalSaleAmt.Text = Math.Round(frm.ProvisionalSale_Amount, 2)
        txtNetSaleAmount.Text = Math.Round(frm.NetSale_Amount, 2)
        txtSchemeAmt.Text = Math.Round(frm.Scheme_Amount, 2)

        IsFillProceed = False
    End Sub
    Dim iiDeadlockErrors As Integer
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        iiDeadlockErrors = 1
        savedata(clicked)
    End Sub

    Public Sub savedata(ByVal clicked As Boolean)
        Try

            If btnSave.Text = "Update" Then
                Dim strchk As String = "select Post from tspl_QuickSettleMent where Quick_SettleMent_Id='" + fndQuickSettlement.Value + "'"
                Dim chkpost As String = clsDBFuncationality.getSingleValue(strchk)
                If chkpost = "Y" Then
                    clsCommon.MyMessageBoxShow(Me, "Transection already posted", Me.Text)
                    Exit Sub
                End If
            End If
            If fndTransferNumber.Value = "" Then
                myMessages.blankValue("Load Out Number")
                fndTransferNumber.Focus()
            ElseIf clsCommon.myLen(fndSalesmanCode.Value) <= 0 Then
                myMessages.blankValue("Salesman Code")
                fndSalesmanCode.Focus()
            ElseIf funSave() Then
                If clicked = False Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function funSave() As Boolean
        Try
            Dim obj As New ClsQuickSettlementMaster()
            obj.Quick_SettleMent_Id = fndQuickSettlement.Value
            obj.Quick_Settlement_Date = txtQSDate.Value
            obj.Transfer_Number = fndTransferNumber.Value
            obj.Transfer_Date = clsCommon.myCDate(txtTransferDate.Text, "dd/MM/yyyy")
            obj.Transfer_Amount = clsCommon.myCdbl(txtAmount.Text)
            obj.Load_In_Amount = clsCommon.myCdbl(txtLoadInAmount.Text)
            If UsLock1.Status = ERPTransactionStatus.Approved Then
                obj.Post = True
            Else
                obj.Post = False
            End If
            obj.Salesman = txtSalesman.Text
            obj.RouteNo = txtRouteNo.Text
            obj.VehicleNo = txtVehicleNo.Text
            obj.RouteDescription = txtRoutedescription.Text
            obj.Empty_Load_In = clsCommon.myCdbl(txtempty.Text)
            obj.CashMemo = Convert.ToInt32(txtCashMemo.Text)
            obj.Salesman_code = clsCommon.myCstr(fndSalesmanCode.Value)
            obj.Balance_Amount = clsCommon.myCdbl(txtBalanceAmount.Text)
            obj.Comments = txtComments.Text

            obj.Arr = New List(Of ClsQuickSettlementDetail)
            For Each grow As GridViewRowInfo In dgvQuickSettleMent.Rows
                Dim objTr As New ClsQuickSettlementDetail()
                objTr.SettleMent_Code = grow.Cells(colSettlementCode).Value
                objTr.Description = grow.Cells(colDescription).Value
                objTr.Amount = clsCommon.myCdbl(grow.Cells(colAmount).Value)
                objTr.Remarks = grow.Cells(colRemarks).Value
                If clsCommon.myCdbl(objTr.Amount) > 0 Then
                    obj.Arr.Add(objTr)
                End If
            Next
            Dim LoadIn As String = clsDBFuncationality.getSingleValue("select Transfer_No  from TSPL_TRANSFER_HEAD where Load_Out_No ='" + obj.Transfer_Number + "'")
            If ClsQuickSettlementMaster.funSave(obj, isNewEntry) Then
                funFill(obj.Quick_SettleMent_Id)
                Return True
            End If
            Return False
        Catch ex As Exception
            If ex.Message.Contains("deadlocked") Then
                iiDeadlockErrors += 1
                If iiDeadlockErrors >= 15 Then
                    Me.Close()
                    Exit Function
                End If
                System.Threading.Thread.Sleep(3000)
                funSave()
            Else
                Throw New Exception(ex.Message)
            End If
        End Try
    End Function

    Private Sub funUpdate()
        Dim trans As SqlTransaction = Nothing
        Try
            Dim strdelqry As String = "delete from [tspl_QuickSettleMent] where Quick_SettleMent_Id='" + fndQuickSettlement.Value + "'"
            clsDBFuncationality.ExecuteNonQuery(strdelqry)
            trans = clsDBFuncationality.GetTransactin()
            Dim grow As GridViewRowInfo
            For Each grow In dgvQuickSettleMent.Rows
                clsDBFuncationality.SaveAStorePorcedure(trans, "sp_tspl_QuickSettleMent_insert", New SqlParameter("@QuickSettleMentId", fndQuickSettlement.Value), New SqlParameter("@SettleMentCode", grow.Cells(colSettlementCode).Value), New SqlParameter("@Description", grow.Cells(colDescription).Value), New SqlParameter("@Amount", clsCommon.myCdbl(grow.Cells(colAmount).Value)), New SqlParameter("@Remarks", grow.Cells(colRemarks).Value), New SqlParameter("@TransferNumber", fndTransferNumber.Value), New SqlParameter("@TransferDate", clsCommon.myCDate(txtTransferDate.Text)), New SqlParameter("@TransferAmount", clsCommon.myCdbl(txtAmount.Text)), New SqlParameter("@CreatedBy", userCode), New SqlParameter("@CreatedDate", connectSql.serverDate()), New SqlParameter("@ModifyBy", userCode), New SqlParameter("@ModifyDate", connectSql.serverDate()), New SqlParameter("@CompCode", companyCode), New SqlParameter("@Salesman", txtSalesman.Text), New SqlParameter("@RouteDesc", txtRoutedescription.Text), New SqlParameter("@Quick_Settlement_Date", txtQSDate.Value), New SqlParameter("@Balance_Amount", clsCommon.myCdbl(txtBalanceAmount.Text)), New SqlParameter("@Comments", txtComments.Text), New SqlParameter("@Loadinamount", clsCommon.myCdbl(txtLoadInAmount.Text)), New SqlParameter("@Empty_Load_In", clsCommon.myCdbl(txtempty.Text)))
            Next
            trans.Commit()
            myMessages.update()
        Catch ex As Exception
            trans.Rollback()
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub txtTransferDate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTransferDate.TextChanged

    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        funReset()
    End Sub

    Private Sub funReset()
        txtRouteNo.Text = ""
        isNewEntry = True
        txtComments.Text = ""
        txtVehicleNo.Text = ""
        txtRouteNo.Text = ""
        txtRoutedescription.Text = ""
        txtQSDate.Value = clsCommon.GETSERVERDATE()
        fndQuickSettlement.Value = ""
        fndTransferNumber.Value = ""
        txtTransferDate.Text = ""
        txtAmount.Text = ""
        txtRoutedescription.Text = ""
        txtSalesman.Text = ""
        txtCashMemo.Text = "0"
        txtempty.Text = ""
        txtLoadInAmount.Text = ""
        txtTotalAmount.Text = "0.00"
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = False
        btnPost.Enabled = False
        btnPrint.Enabled = False
        RadLabel4.Text = ""
        fndSalesmanCode.Value = ""
        dgvQuickSettleMent.DataSource = Nothing
        ColumnAdd()
        GridBind()
        txtBalanceAmount.Text = 0
        txtProvisionalSaleAmt.Text = "0.00"
        txtSchemeAmt.Text = "0.00"
        txtNetSaleAmount.Text = "0.00"
        'gv1.DataSource = Nothing
        ' LoadBlankGrid()
        UsLock1.Status = ERPTransactionStatus.Pending
        fndTransferNumber.Enabled = True
        IsFillProceed = False
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeform()
    End Sub
    Public Sub ColumnAdd()
        If dgvQuickSettleMent.Columns.Contains(ColSettlementType) = False Then
            dgvQuickSettleMent.Columns.Add(ColSettlementType, "Settlement Type")
            dgvQuickSettleMent.Columns(ColSettlementType).IsVisible = False
        End If
    End Sub
    Public Sub closeform()
        Me.Close()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        deletedata()
    End Sub

    Public Sub deletedata()
        funDelete()
    End Sub

    Private Sub funDelete()
        Try
           

            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                '' REASON FOR DELETE 
                If clsCancelLog.CheckForReasonOnDelete() Then
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Delete"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If
                End If
                If (ClsQuickSettlementMaster.DeleteData(fndQuickSettlement.Value)) Then
                    saveCancelLog(Reason, Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    funReset()
                End If
            End If
            
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Function saveCancelLog(ByVal Reason As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.fndQuickSettlement.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = "Delete"
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    Private Sub fndQuickSettlement__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndQuickSettlement._MYNavigator
        Try
            Dim val As String = fndQuickSettlement.Value

            Dim minval As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select  Min(Quick_SettleMent_Id) from [tspl_QuickSettleMent] "))
            Dim maxval As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Max(Quick_SettleMent_Id) from [tspl_QuickSettleMent]"))
            Dim qry As String = "select  Quick_SettleMent_Id from [tspl_QuickSettleMent] where Quick_SettleMent_Id="
            Dim WhrCls As String = ""
            Dim strJoin As String = ""
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                strJoin = " Left Outer Join TSPL_TRANSFER_HEAD on tspl_QuickSettleMent.Transfer_Number=TSPL_TRANSFER_HEAD.Transfer_No "
                WhrCls += " AND TSPL_TRANSFER_HEAD.From_Location in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            Select Case NavType
                Case NavigatorType.First
                    qry += "(select  Min(Quick_SettleMent_Id) from [tspl_QuickSettleMent] " + strJoin + " Where 1=1 " + WhrCls + ")"
                Case NavigatorType.Last
                    qry += "(select  Max(Quick_SettleMent_Id) from [tspl_QuickSettleMent] " + strJoin + " Where 1=1 " + WhrCls + ")"
                Case NavigatorType.Next

                    If val = maxval Then
                        qry += "(select  Max(Quick_SettleMent_Id) from [tspl_QuickSettleMent] " + strJoin + " Where 1=1 " + WhrCls + ")"
                    Else
                        qry += "(select  Min(Quick_SettleMent_Id) from [tspl_QuickSettleMent] " + strJoin + "  where Quick_SettleMent_Id>'" + fndQuickSettlement.Value + "'  " + WhrCls + ")"
                    End If

                Case NavigatorType.Previous
                    If val = minval Then
                        qry += "(select  Min(Quick_SettleMent_Id) from [tspl_QuickSettleMent] " + strJoin + " Where 1=1 " + WhrCls + " )"
                    Else
                        qry += "(select  Max(Quick_SettleMent_Id) from [tspl_QuickSettleMent] " + strJoin + " where Quick_SettleMent_Id<'" + fndQuickSettlement.Value + "'  " + WhrCls + ")"
                    End If

            End Select
            fndQuickSettlement.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            If clsCommon.myLen(fndQuickSettlement.Value) > 0 Then
                funFill(fndQuickSettlement.Value)
            End If
            'LoadData(clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry)))
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndQuickSettlement__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndQuickSettlement._MYValidating
        Dim qry As String = "select distinct(QS.Quick_SettleMent_Id) as [QuickSettlement],QS.Transfer_Number as [Load Out Number],convert(varchar(10),QS.Transfer_Date,103) as [Load Out Date] from [tspl_QuickSettleMent] QS LEFT OUTER JOIN TSPL_TRANSFER_HEAD TH on TH.Transfer_No=QS.Transfer_Number "
        Dim WhrCls As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  TH.From_Location in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        fndQuickSettlement.Value = clsCommon.ShowSelectForm("QuickSetFND", qry, "QuickSettlement", WhrCls, fndQuickSettlement.Value, "Quick_SettleMent_Id", isButtonClicked)
        'LoadData(clsCommon.ShowSelectForm("QuickSettlement", qry, "Quick_SettleMent_Id", "", fndQuickSettlement.Value, "Quick_SettleMent_Id", isButtonClicked))
        If fndQuickSettlement.Value <> "" Then
            funFill(fndQuickSettlement.Value)
        Else
            funReset()
        End If

    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        clicked = True
        postdata()
    End Sub

    Public Sub postdata()
        Try
            If clsCommon.myLen(fndQuickSettlement.Value) < 0 Then
                Throw New Exception("Document no not found to post")
            End If
            If myMessages.postConfirm() Then
                savedata(clicked)
                postdataNew()
                funFill(fndQuickSettlement.Value)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, Me.Text)
        End Try
        clicked = False
    End Sub
    Public Sub postdataNew()
        Try
            If ClsQuickSettlement.PostData(fndQuickSettlement.Value) Then
                myMessages.post()
                btnSave.Text = "Update"
                btnSave.Enabled = True
                btnDelete.Enabled = False
                btnPost.Enabled = True
                UsLock1.Status = ERPTransactionStatus.Approved
            Else
                btnSave.Enabled = True
                btnDelete.Enabled = True
                btnPost.Enabled = True
                UsLock1.Status = ERPTransactionStatus.Pending
            End If
        Catch ex As Exception
            If ex.Message.Contains("deadlocked") Then
                iiDeadlockErrors += 1
                If iiDeadlockErrors >= 15 Then
                    Me.Close()
                    Exit Sub
                End If
                System.Threading.Thread.Sleep(3000)
                postdataNew()
            Else
                Throw New Exception(ex.Message)
            End If
        End Try
    End Sub

    'Private Sub funPost()
    '    Dim strqry As String = "update  tspl_QuickSettleMent set Post='Y' where Quick_SettleMent_Id='" + fndQuickSettlement.Value + "'"
    '    clsDBFuncationality.ExecuteNonQuery(strqry)
    '    myMessages.post()
    '    btnSave.Enabled = False
    '    btnDelete.Enabled = False
    '    btnPost.Enabled = False
    'End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim routeno As String = clsCommon.myCstr(txtRouteNo.Text)
        Dim veichleno As String = clsCommon.myCstr(txtVehicleNo.Text)
        Dim balanceamount As Double = clsCommon.myCdbl(txtBalanceAmount.Text)
        Dim comments As String = clsCommon.myCstr(txtComments.Text)
        If clsCommon.myLen(fndQuickSettlement.Value) > 0 Then
            funPrint(routeno, veichleno, balanceamount, comments)
        Else
            myMessages.blankValue("Quick Settlement Code!")
        End If
    End Sub

    Private Sub funPrint(ByVal routeno As String, ByVal veichleno As String, ByVal balanceamount As Double, ByVal comments As String)
        Dim qry As String = " select '" + routeno.ToString() + "' as RouteNo,'" + clsCommon.myCstr(txtVehicleNo.Text) + "' as VehicleNo," + balanceamount.ToString() + " as BalanceAmount ,'" + comments + "' as Comments, tspl_QuickSettleMent.Quick_SettleMent_Id,Quick_Settlement_Date ,tspl_QuickSettleMent_Detail.SettleMent_Code  ,tspl_QuickSettleMent_Detail.Description ,Amount,Amount* case when tspl_SettleMent_Master.Calculate ='N' then 0  else case when tspl_SettleMent_Master.Calculate='A' then 1 else case when tspl_SettleMent_Master.Calculate='S' then -1 end end end  as  totalamount ,Remarks ,Transfer_Number ,Convert(varchar(10),Transfer_Date,103) as Transfer_Date,Transfer_Amount ,tspl_QuickSettleMent.Salesman,tspl_QuickSettleMent.RouteDescription,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2   from tspl_QuickSettleMent left outer join tspl_QuickSettleMent_Detail on tspl_QuickSettleMent.Quick_SettleMent_Id =tspl_QuickSettleMent_Detail.Quick_SettleMent_Id  left outer join tspl_SettleMent_Master on tspl_QuickSettleMent_Detail.SettleMent_Code =tspl_SettleMent_Master.SettleMentCode  left outer join TSPL_COMPANY_MASTER  on  tspl_QuickSettleMent.Comp_Code=TSPL_COMPANY_MASTER.comp_code " & _
                          "   where  2=2  and  tspl_QuickSettleMent.Quick_SettleMent_Id='" + fndQuickSettlement.Value + "'"
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.SalesReport, dt, "QuickSettlement", "Quick Settlement")
            frmCRV = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndTransferNumber_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndTransferNumber.ConnectionString = connectSql.SqlCon()
        ''fndTransferNumber.Query = " select Transfer_No as [Load Out Number], convert(varchar(10),Transfer_Date,103) as [Load Out Date]  from TSPL_TRANSFER_HEAD  where Transfer_Type ='LO' order by [Load Out Number] "

        'fndTransferNumber.Query = " select Load_Out_No  as [Load Out Number] , convert(varchar(10),Transfer_Date,103) as [Load Out Date] ,Route_No as [Route No],From_Location as [From Location],To_Location as [To Location],Transfer_No as [LoadIn No],Salesmancode as [Salesman Code] ,Reference " & _
        '                          " from TSPL_TRANSFER_HEAD left outer join TSPL_LOCATION_MASTER on TSPL_TRANSFER_HEAD.To_Location  =TSPL_LOCATION_MASTER.Location_Code   where Post='N' and Transfer_Type ='LI'  and From_Location   in (select location_code from TSPL_LOCATION_MASTER where Location_Type ='logical') order by [Load Out Number]"
        'fndTransferNumber.ValueToSelect = "Load Out Number"
        'fndTransferNumber.ValueToSelect1 = "Load Out Date"
        'fndTransferNumber.Caption = "Load Out Details"

        uslock()
    End Sub

    'Sub BindLoadOutDetails()
    '    Dim Qry As String = "select xxx.Item_Code,xxx.Item_Desc,xxx.LoadOutQty,xxx.LoadOInFC ,xxx.LoadInFB ,xxx.TotQtyFC , xxx.LoadOutQty-xxx.TotQtyFC as [PSQty] ,xxx.[Retailer Price] as [Retailer Price],(xxx.[Retailer Price]*(xxx.LoadOutQty-xxx.TotQtyFC)) as [Amount] from (select TSPL_TRANSFER_DETAIL.Item_Code,TSPL_TRANSFER_DETAIL.Item_Desc ,(LoadIn_Qty+Leak+Burst+Shortage ) as [LoadOInFC],(select Item_Qty  from TSPL_TRANSFER_DETAIL L where l.Transfer_No =TSPL_TRANSFER_HEAD.Load_Out_No and L.Item_Code=TSPL_TRANSFER_DETAIL.Item_Code  ) as [LoadOutQty],( select (LoadIn_Qty+Leak+Burst+Shortage )  from TSPL_TRANSFER_DETAIL L1  where L1.Uom='FB' and L1.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No and L1.Item_Code=TSPL_TRANSFER_DETAIL.Item_Code  ) as [LoadInFB],(select sum(Total_QtyInCase)  from TSPL_TRANSFER_DETAIL L1  where  L1.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No and L1.Item_Code=TSPL_TRANSFER_DETAIL.Item_Code  ) as [TotQtyFC] ,(BasicPrice_WithTax+TPT_Value  ) as [Retailer Price] from TSPL_TRANSFER_DETAIL inner join TSPL_TRANSFER_HEAD on TSPL_TRANSFER_DETAIL.Transfer_No =TSPL_TRANSFER_HEAD.Transfer_No where TSPL_TRANSFER_HEAD.Load_Out_No='" + fndTransferNumber.Value + "' and TSPL_TRANSFER_DETAIL.Uom='FC') as xxx inner join TSPL_ITEM_MASTER on xxx.Item_Code =TSPL_ITEM_MASTER.Item_Code order by TSPL_ITEM_MASTER.Sku_Seq "
    '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
    '    gv1.DataSource = Nothing
    '    gv1.AutoGenerateColumns = False
    '    'gv1.MasterTemplate.SummaryRowsBottom.Clear()
    '    gv1.DataSource = dt
    '    gv1.Columns(colItemCode).FieldName = "Item_Code"
    '    gv1.Columns(colItemName).FieldName = "Item_Desc"
    '    gv1.Columns(colLoadOutQty).FieldName = "LoadOutQty"
    '    gv1.Columns(colLoadInQtyFC).FieldName = "LoadOInFC"
    '    gv1.Columns(colLoadInQtyFB).FieldName = "LoadInFB"
    '    gv1.Columns(colTotalLoadInQtyFC).FieldName = "TotQtyFC"
    '    gv1.Columns(colProvisionalSale).FieldName = "PSQty"
    '    gv1.Columns(colRetailerPrice).FieldName = "Retailer Price"
    '    gv1.Columns(colAmountLoadOut).FieldName = "Amount"
    'End Sub


    Private Sub fndTransferNumber_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fndTransferNumber.Value <> "" Then
            Dim s As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Transfer_No  from TSPL_TRANSFER_HEAD  where Transfer_No='" + fndTransferNumber.Value + "'"))
            If s <> fndTransferNumber.Value Then
                common.clsCommon.MyMessageBoxShow(Me, "Load Out Number doesn't exist", Me.Text)
                fndTransferNumber.Value = ""
                txtTransferDate.Text = ""
                txtAmount.Text = ""
                fndTransferNumber.Focus()
            Else


            End If
        Else

        End If

    End Sub

    Private Sub dgvQuickSettleMent_CellFormatting(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles dgvQuickSettleMent.CellFormatting
        Try
            Dim grow As GridViewRowInfo = TryCast(e.Row, GridViewRowInfo)
            If clsCommon.CompairString(clsCommon.myCstr(grow.Cells(ColSettlementType).Value), "ESE") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(grow.Cells(ColSettlementType).Value), "CSE") = CompairStringResult.Equal Then
                grow.Cells(colAmount).ReadOnly = True
            Else
                grow.Cells(colAmount).ReadOnly = False
            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Sub dgvQuickSettleMent_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles dgvQuickSettleMent.CellValueChanged
        GridCellValueChanged()
    End Sub
    Private Sub GridCellValueChanged()
        If IsFillProceed = False Then
            Dim stra As Decimal
            Dim strs As Decimal
            Dim s As String
            Dim grow As GridViewRowInfo
            For Each grow In dgvQuickSettleMent.Rows
                s = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Calculate  from tspl_SettleMent_Master  where SettleMentCode ='" + grow.Cells(0).Value + "'"))
                If s = "A" Then
                    stra += clsCommon.myCdbl(grow.Cells(2).Value)
                ElseIf s = "S" Then
                    strs += clsCommon.myCdbl(grow.Cells(2).Value)
                End If
            Next
            Dim totalamount As Double = clsCommon.myCdbl(stra - strs)
            txtTotalAmount.Text = totalamount
            Dim amount As Double = clsCommon.myCdbl(txtAmount.Text)
            txtBalanceAmount.Text = clsCommon.myCstr((CDec(RadLabel4.Text)) + (CDec(txtTotalAmount.Text)))
            uslock()
        End If
    End Sub
    Private Sub FrmQuickSettlement_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            savedata(clicked)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            deletedata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            postdata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
            funReset()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            closeform()
        End If
    End Sub



    'Private Sub txtTotalAmount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTotalAmount.TextChanged
    '    If txtAmount.Text = "" Then
    '        txtAmount.Text = 0

    '    End If
    '    If txtBalanceAmount.Text = " " Or String.IsNullOrEmpty(txtBalanceAmount.Text) Then
    '        txtBalanceAmount.Text = 0
    '    End If
    '    If txtTotalAmount.Text = " " Then
    '        txtTotalAmount.Text = 0
    '    End If
    '    ' txtBalanceAmount.Text = Math.Abs(CDec(txtAmount.Text)) - Math.Abs(CDec(txtTotalAmount.Text))

    '    '---------------------------------- add by bibhu said by ranjana mam------------------
    '    txtBalanceAmount.Text = Math.Abs(CDec(txtBalanceAmount.Text)) - Math.Abs(CDec(txtTotalAmount.Text))
    '    '---------------------------------- ends here -------------------------------------
    'End Sub

    Private Sub fndTransferNumber__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndTransferNumber._MYValidating

        If fndTransferNumber.MyReadOnly OrElse isButtonClicked Then

            Dim qry As String = " select Load_Out_No  as [LoadOutNumber] , convert(varchar(10),Transfer_Date,103) as [Load Out Date] ,Route_No as [Route No],To_Location as [From Location],From_Location as [To Location],Transfer_No as [LoadIn No],Salesmancode as [Salesman Code] ,Reference, Reference_Doc_No as [Reference Document] " & _
                                  " from TSPL_TRANSFER_HEAD left outer join TSPL_LOCATION_MASTER on TSPL_TRANSFER_HEAD.To_Location  =TSPL_LOCATION_MASTER.Location_Code "
            Dim WhrCls As String = " Post='N' and Transfer_Type ='LI'  and From_Location   in (select location_code from TSPL_LOCATION_MASTER where Location_Type ='logical') and Load_Out_No not in (select Transfer_Number   from tspl_QuickSettleMent )"
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls = WhrCls + "  and   To_Location in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            fndTransferNumber.Value = clsCommon.ShowSelectForm("QuickSettlementTransfer", qry, "LoadOutNumber", WhrCls, fndTransferNumber.Value, "LoadOutNumber", isButtonClicked)
            'fndTransferNumber.Value = fndTransferNumber.Value.ToUpper
            uslock()
        End If
        Dim LoadOutNO As String = clsDBFuncationality.getSingleValue(" select Load_Out_No  as [LoadOutNumber]   from TSPL_TRANSFER_HEAD left outer join TSPL_LOCATION_MASTER on TSPL_TRANSFER_HEAD.To_Location  =TSPL_LOCATION_MASTER.Location_Code where  Post='N' and Transfer_Type ='LI'  and From_Location   in (select location_code from TSPL_LOCATION_MASTER where Location_Type ='logical') and Load_Out_No not in (select Transfer_Number   from tspl_QuickSettleMent ) and Load_Out_No ='" + fndTransferNumber.Value + "'")
        If clsCommon.myLen(LoadOutNO) > 0 Then
            fndTransferNumber_TextChanged()
        Else
            fndTransferNumber.Value = ""
        End If

    End Sub
    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If gv1.Rows.Count > 0 Then
        '    Dim LoadInNo As String = clsDBFuncationality.getSingleValue("select Transfer_No  from TSPL_TRANSFER_HEAD where Load_Out_No='" + fndTransferNumber.Value + "'")
        '    Dim ItemCOde As String = gv1.CurrentRow.Cells(colItemCode).Value
        '    If clsCommon.myLen(LoadInNo) > 0 Then
        '        Dim frm As New frmTransfer(LoadInNo, ItemCOde, userCode, companyCode)
        '        frm.ShowDialog()
        '        fndTransferNumber_TextChanged()
        '    End If
        'End If
    End Sub

    Private Sub btnUpdateLoadIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadOutView.Click
        If clsCommon.myLen(fndQuickSettlement.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please Save This Transfer First To View Details.", "QuickSettlement", MessageBoxButtons.OK)
            Return
        Else
            If clsCommon.myLen(fndTransferNumber.Value) > 0 Then
                Dim frm As New FrmViewLoadOutDetail(fndTransferNumber.Value, fndQuickSettlement.Value, userCode, companyCode)
                frm.ShowDialog()
                fndTransferNumber_TextChanged()
                txtProvisionalSaleAmt.Text = Math.Round(frm.ProvisionalSale_Amount, 2)
                txtNetSaleAmount.Text = Math.Round(frm.NetSale_Amount, 2)
                txtSchemeAmt.Text = Math.Round(frm.Scheme_Amount, 2)
            End If
        End If
    End Sub

    Private Sub gv1_ViewCellFormatting(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs)
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub

    Private Sub fndSalesmanCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndSalesmanCode._MYValidating
        If fndSalesmanCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = "select Location_Code,Location_Desc from TSPL_LOCATION_MASTER "
            Dim WhrCls As String = " Location_Type ='Logical'"
            fndSalesmanCode.Value = clsCommon.ShowSelectForm("Salesman", qry, "Location_Code", WhrCls, fndSalesmanCode.Value, "Location_Code", isButtonClicked)
        End If
        Dim SalesmanNAme As String = clsDBFuncationality.getSingleValue(" select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + fndSalesmanCode.Value + "'")
        If clsCommon.myLen(SalesmanNAme) > 0 Then
            txtSalesman.Text = SalesmanNAme
        Else
            fndSalesmanCode.Value = ""
            txtSalesman.Text = ""
        End If
    End Sub

    Private Sub btnPostfinnancial_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPostfinnancial.Click
        If clsCommon.myLen(fndQuickSettlement.Value) > 0 Then
            If POstFinancialEnrty() Then
                common.clsCommon.MyMessageBoxShow(Me, "Entry Posted Successfully.", "Financial Entry", MessageBoxButtons.OK)

            End If
        Else
            common.clsCommon.MyMessageBoxShow(Me, "Please Save QuickSettlement Entry First!", "Financial Entry", MessageBoxButtons.OK)
        End If
    End Sub

    Public Function POstFinancialEnrty() As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim ChkRecordFlag As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT 1 FROM dbo.TSPL_PAYMENT_HEADER WHERE LoadOutNo='" + fndTransferNumber.Value + "'", trans))
            If ChkRecordFlag = 1 Then
                Throw New Exception("Entry Already Exist For This Transfer No.")
            End If

            Dim qry As String = "select Bank_type,BANKACC from TSPL_BANK_MASTER where BANK_CODE='" + clsFixedParameter.GetData(clsFixedParameterType.LOReceiptDefaultBankForSettlement, clsFixedParameterCode.LOReceiptDefaultBankForSettlement, trans) + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)


            Dim Loc As String = clsDBFuncationality.getSingleValue("SELECT From_Location FROM dbo.TSPL_TRANSFER_HEAD WHERE Transfer_No='" + fndTransferNumber.Value + "'", trans)
            Dim LocDesc As String = clsDBFuncationality.getSingleValue("SELECT FromLoc_Desc FROM dbo.TSPL_TRANSFER_HEAD WHERE Transfer_No='" + fndTransferNumber.Value + "'", trans)


            Dim strbankacct As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select BANKACC  from TSPL_BANK_MASTER  where BANK_CODE ='SETTLEMENT'", trans))
            If clsCommon.myLen(Loc) > 0 Then
                strbankacct = clsERPFuncationality.ChangeGLAccountLocationSegment(strbankacct, Loc, False, trans)
            End If
            'Dim Bankname As String
            Dim Bank As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select BANK_CODE  from TSPL_BANK_MASTER  where BANKACC ='" + strbankacct + "'", trans))
            If Bank Is Nothing Or Bank = "" Then
                Throw New Exception("Bank Not Found For Location " + Loc + "")
            End If
            Dim EntryDesc As String = "" + fndTransferNumber.Value + " Financial Settlement On Dated:'" + clsCommon.GetPrintDate(txtQSDate.Value, "dd/MM/yyyy") + "',With QuickSettlement: '" + fndQuickSettlement.Value + "'."
            Dim PaymentType As String = clsFixedParameter.GetData(clsFixedParameterType.LOReceiptPaymentTypeForSettlement, clsFixedParameterCode.LOReceiptPaymentTypeForSettlement, trans)



            Dim obj As New clsPaymentHeader()
            obj.Payment_No = "" ''To be Generated
            obj.Entry_Desc = EntryDesc
            obj.Payment_Date = txtQSDate.Value
            obj.Payment_Post_Date = txtQSDate.Value
            obj.Bank_Code = Bank
            obj.Payment_Type = "MI"
            obj.Vendor_Code = ""
            obj.Vendor_Name = ""
            obj.Payment_Code = PaymentType
            obj.Cheque_No = ""
            obj.Cheque_Date = Nothing
            obj.LoadOutNo = fndTransferNumber.Value

            obj.Payment_Amount = 0
            obj.Total_Applied_Amount = 0
            obj.Remit_To = "Settlement For LoadOut No.:" + fndTransferNumber.Value + " of SalesMan " + fndSalesmanCode.Value + ". "

            obj.IsChkReverse = "N"
            'obj.objRemittance = objRemittance
            obj.ArrTr = New List(Of clsPaymentDetail)


            For Each grow As GridViewRowInfo In dgvQuickSettleMent.Rows

                Dim AccCode As String = clsDBFuncationality.getSingleValue("SELECT Account_Code FROM dbo.tspl_SettleMent_Master WHERE financial_entry ='Y'  and SettleMentCode='" + grow.Cells(colSettlementCode).Value + "' ", trans)
                Dim SettlementType As String = clsDBFuncationality.getSingleValue("SELECT Calculate FROM dbo.tspl_SettleMent_Master WHERE SettleMentCode='" + grow.Cells(colSettlementCode).Value + "'", trans)
                If clsCommon.myLen(AccCode) = 0 Then
                    Continue For
                End If
                Dim Acc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(AccCode, Loc, False, trans)
                Dim AccDesc As String = clsCommon.myCstr(connectSql.RunScalar(trans, "select Description  from TSPL_GL_ACCOUNTS where Account_Code ='" + Acc + "'"))
                Dim SettlementAmt As Decimal = 0
                If SettlementType = "S" Then
                    SettlementAmt = grow.Cells(colAmount).Value
                Else
                    SettlementAmt = grow.Cells(colAmount).Value * -1
                End If
                Dim objTr As New clsPaymentDetail()
                objTr.Payment_Type = obj.Payment_Type
                objTr.Account_Code = Acc
                objTr.Description = AccDesc
                'objTr.Applied_Amount = clsCommon.myCdbl(grow.Cells(colAmount).Value)
                objTr.Net_Balance = SettlementAmt
                objTr.Remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value)
                objTr.Settlement_code = clsCommon.myCstr(grow.Cells(colSettlementCode).Value)
                objTr.Settlement_Description = clsCommon.myCstr(grow.Cells(colSettlementCode).Value)
                If clsCommon.myLen(objTr.Account_Code) > 0 Then
                    obj.ArrTr.Add(objTr)
                    obj.Payment_Amount += SettlementAmt
                End If
            Next

            obj.SaveData(obj, True, trans)
            clsPayment.PostData(obj.Payment_No, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function

   


End Class
