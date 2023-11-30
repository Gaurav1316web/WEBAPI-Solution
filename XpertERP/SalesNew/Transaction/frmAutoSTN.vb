Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System.IO
Public Class FrmAutoSTN
    Inherits FrmMainTranScreen
#Region "Variables"
    Private isInsideLoadData As Boolean = False
    Public strFirstPO As String = Nothing
    Public strCurrCode As String = Nothing
    Public ArrReturn As List(Of clsPurchaseOrderDetail) = Nothing

    Dim isCellValueChanged As Boolean = False

    Const colDICode As String = "ICODE"
    Const colDIName As String = "INAME"
    Const colRowType As String = "COLTYPE"
    Const colDUnit As String = "UNIT"
    Const colDRate As String = "RATE"
    Const colDMRP As String = "MRP"
    Const colDAssessable As String = "Assessable"
    Const colAbatementRate As String = "colAbatementRate"
    Const colDQty As String = "colDQty"
    Const colDAvailableQty As String = "colDAvailableQty"
    Const colDTransferQty As String = "colDTransferQty"
    Const colDamount As String = "colDamount"

    Const colHSelect As String = "SELECT"
    Const colHCode As String = "CODE"
    Const colHDate As String = "DATE"
    Const colHCustomer As String = "Customer"
    Dim PostShipment As Boolean = False
    Dim CreateInvoice As Boolean = False
    Dim AllowChangeInvoiceType As Boolean = False
    Dim IsItemRateeditable As Boolean = False
    Dim IsTransferQTyEditable As Boolean = False
#End Region

    Private Sub FrmAutoSTN_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnOKPressed()
        ElseIf e.KeyCode = Keys.Escape Then
            btnCancelPressed()
        End If
    End Sub

    Private Sub FrmAutoSTN_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        txtTransferDate.Value = clsCommon.GETSERVERDATE()
        IsTransferQTyEditable = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.IsTransferQtyEditableOnAutoSTN & "'")) = 0, False, True)
        IsItemRateeditable = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.IsItemRateEditableOnTransfer & "'")) = 0, False, True)
        PostShipment = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.PostShipmentonAutoSTN & "'")) = 0, False, True)
        CreateInvoice = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.CreateInvoicewithShipmentonAutoSTN & "'")) = 0, False, True)
        AllowChangeInvoiceType = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Allow_Change_InvoiceType from TSPL_inv_parameters")) = 0, False, True)
        If CreateInvoice = True AndAlso AllowChangeInvoiceType = True Then
            lblInvoiceType.Visible = True
            ddlInvoiceType.Visible = True
            LoadInvoiceType()
        Else
            lblInvoiceType.Visible = False
            ddlInvoiceType.Visible = False
        End If
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmAutoSTN)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
           
        End If
        btnOk.Visible = MyBase.isModifyFlag
       
    End Sub
    Public Shared Function GetInvoiceType() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "R"
        dr("Name") = "Retail"
        dt.Rows.Add(dr)


        dr = dt.NewRow()
        dr("Code") = "T"
        dr("Name") = "Tax"
        dt.Rows.Add(dr)


        Return dt
    End Function
    Sub LoadInvoiceType()
        ddlInvoiceType.DataSource = GetInvoiceType()
        ddlInvoiceType.ValueMember = "Code"
        ddlInvoiceType.DisplayMember = "Name"
    End Sub
    Private Sub txToLoc__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtToLoc._MYValidating
        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = " Location_Type='Physical'  "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtToLoc.Value = clsCommon.ShowSelectForm("To Location", qry, "Code", WhrCls, txtToLoc.Value, "Code", isButtonClicked)
        lblToLoc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtToLoc.Value + "'"))


    End Sub

    Private Sub txtFromLoc__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtFromLoc._MYValidating
        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = " Location_Type='Physical'  "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtFromLoc.Value = clsCommon.ShowSelectForm("From Location", qry, "Code", WhrCls, txtFromLoc.Value, "Code", isButtonClicked)
        lblFromLoc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtFromLoc.Value + "'"))

    End Sub


    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        If clsCommon.myLen(txtToLoc.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Order Location", Me.Text)
            Exit Sub
        End If
        '-------richa 13/08/2014 Ticket No. BM00000003242---------
        Dim strwherecls As String = ""
        Dim strcondition As String = ""
        strwherecls = Xtra.CustomerPermission()
        If clsCommon.myLen(strwherecls) > 0 Then
            strcondition = " and TSPL_SD_SALES_ORDER_HEAD.Customer_Code in (" + strwherecls + ")"
        End If
        '-----------------------------------------------------
        '-----------------------------------------------------
        Dim qry As String = "Select CAST(0 as bit) as Sel,TSPL_SD_SALES_ORDER_HEAD.Document_Code as Code,TSPL_SD_SALES_ORDER_HEAD.Document_Date as Date,TSPL_SD_SALES_ORDER_HEAD.Customer_Code as Customer from " & _
        "TSPL_SD_SALES_ORDER_HEAD where Bill_To_Location='" & txtToLoc.Value & "' and Document_Code not in (select isnull(Order_Code,'') from TSPL_SD_SHIPMENT_DETAIL) " + strcondition + ""

        LoadHeadData(clsDBFuncationality.GetDataTable(qry))
        LoadBlankGridDetail()
    End Sub
    Sub LoadHeadData(ByVal dtAllData As DataTable)
        IsInsideLoadData = True
        LoadBlankHeadGrid()
        Dim arr As New List(Of String)
        For Each dr As DataRow In dtAllData.Rows
            Dim strCode As String = clsCommon.myCstr(dr("code"))
            'If Not arr.Contains(strCode) Then
            '    arr.Add(strCode)
            gvHead.Rows.AddNew()
            gvHead.Rows(gvHead.RowCount - 1).Cells(colHSelect).Value = False
            gvHead.Rows(gvHead.RowCount - 1).Cells(colHCode).Value = strCode
            gvHead.Rows(gvHead.RowCount - 1).Cells(colHDate).Value = clsCommon.myCstr(dr("Date"))
            gvHead.Rows(gvHead.RowCount - 1).Cells(colHCustomer).Value = clsCommon.myCstr(dr("Customer"))
            'End If
        Next
        IsInsideLoadData = False
    End Sub

    Sub LoadBlankHeadGrid()
        gvHead.Rows.Clear()
        gvHead.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.HeaderText = " "
        repoSelect.Name = colHSelect
        repoSelect.ReadOnly = False
        repoSelect.Width = 25
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvHead.MasterTemplate.Columns.Add(repoSelect)


        Dim repoCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCode.FormatString = ""
        repoCode.HeaderText = "Sale Order No"
        repoCode.Name = colHCode
        repoCode.Width = 150
        repoCode.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoCode)

        Dim repoDate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDate.FormatString = ""
        repoDate.HeaderText = "Date"
        repoDate.Name = colHDate
        repoDate.Width = 100
        repoDate.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoDate)


        Dim repoCustomer As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCustomer.FormatString = ""
        repoCustomer.HeaderText = "Customer"
        repoCustomer.Name = colHCustomer
        repoCustomer.Width = 150
        repoCustomer.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoCustomer)

        gvHead.ShowFilteringRow = True
        gvHead.EnableFiltering = True
        gvHead.AllowDeleteRow = False
        gvHead.AllowAddNewRow = False
        gvHead.ShowGroupPanel = False
        gvHead.AllowColumnReorder = False
        gvHead.AllowRowReorder = False
        gvHead.EnableSorting = False
        gvHead.EnableAlternatingRowColor = True
        gvHead.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvHead.MasterTemplate.ShowRowHeaderColumn = False
        gvHead.TableElement.TableHeaderHeight = 40
    End Sub

    Sub LoadBlankGridDetail()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colDICode
        repoICode.Width = 100
        repoICode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item"
        repoIName.Name = colDIName
        repoIName.Width = 180
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)

        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "Unit"
        repoUnit.Name = colDUnit
        repoUnit.Width = 100
        repoUnit.ReadOnly = True
        repoUnit.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoUnit)


        Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "Rate"
        repoRate.Name = colDRate
        If IsItemRateeditable = True Then
            repoRate.ReadOnly = False
        Else
            repoRate.ReadOnly = True
        End If
        repoRate.IsVisible = True
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRate)

        Dim repoMRP As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMRP.FormatString = ""
        repoMRP.HeaderText = "MRP"
        repoMRP.Name = colDMRP
        repoMRP.ReadOnly = True
        repoMRP.IsVisible = True
        repoMRP.WrapText = True
        repoMRP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoMRP)


        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Quantity"
        repoQty.Name = colDQty
        repoQty.ReadOnly = True
        repoQty.IsVisible = True
        repoQty.WrapText = True
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoQty)

        Dim repoAvailabe As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAvailabe.FormatString = ""
        repoAvailabe.HeaderText = "Available Quantity"
        repoAvailabe.Name = colDAvailableQty
        repoAvailabe.ReadOnly = True
        repoAvailabe.IsVisible = True
        repoAvailabe.WrapText = True
        repoAvailabe.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAvailabe)

        Dim repoTransferQTy As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTransferQTy.FormatString = ""
        repoTransferQTy.HeaderText = "Transfer Quantity"
        repoTransferQTy.Name = colDTransferQty
        If IsTransferQTyEditable = True Then
            repoTransferQTy.ReadOnly = False
        Else
            repoTransferQTy.ReadOnly = True
        End If
        repoTransferQTy.IsVisible = True
        repoTransferQTy.WrapText = True
        repoTransferQTy.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTransferQTy)

        Dim repoAmount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmount.FormatString = ""
        repoAmount.HeaderText = "Amount"
        repoAmount.Name = colDamount
        repoAmount.ReadOnly = True
        repoAmount.IsVisible = True
        repoAmount.WrapText = True
        repoAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAmount)

        gv1.BestFitColumns()
        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = True
        gv1.EnableSorting = False
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.MasterTemplate.ShowColumnHeaders = True
        gv1.EnableAlternatingRowColor = True
        gv1.EnableFiltering = True
        gv1.ShowFilteringRow = True
        gv1.TableElement.TableHeaderHeight = 40
    End Sub
    Sub LoadDetailData()
        LoadBlankGridDetail()
        Dim arrPONo As New ArrayList()
        For ii As Integer = 0 To gvHead.RowCount - 1
            If clsCommon.myCBool(gvHead.Rows(ii).Cells(colHSelect).Value) Then
                arrPONo.Add(clsCommon.myCstr(gvHead.Rows(ii).Cells(colHCode).Value))
            End If
        Next
        If arrPONo Is Nothing OrElse arrPONo.Count <= 0 Then
            Exit Sub
        Else
            strFirstPO = arrPONo(0)
        End If
        isInsideLoadData = True
        Dim qry As String = "  select ICode,MAX(IName) as IName,MAX(IType) as IType,SUM(Qty) as Qty,MAX(Unit) as Unit,'' as Rate, " + Environment.NewLine
        qry += " MRP  as MRP  from (Select TSPL_SD_SALES_ORDER_DETAIL.Document_Code as Code,TSPL_SD_SALES_ORDER_DETAIL.Item_Code as ICode, " + Environment.NewLine
        qry += " TSPL_ITEM_MASTER.Item_Desc as IName,TSPL_SD_SALES_ORDER_DETAIL.Row_Type as IType, " + Environment.NewLine
        qry += " (TSPL_SD_SALES_ORDER_DETAIL.Qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as Qty, " + Environment.NewLine
        qry += " (select UOM_Code from TSPL_ITEM_UOM_DETAIL as innUOM where innUOM.Item_Code=TSPL_SD_SALES_ORDER_DETAIL.Item_Code and innUOM.Stocking_Unit='Y') as Unit, " + Environment.NewLine
        qry += " TSPL_SD_SALES_ORDER_DETAIL.Location as Location,TSPL_SD_SALES_ORDER_DETAIL.OrgRate as Rate,TSPL_SD_SALES_ORDER_HEAD.Tax_Group, " + Environment.NewLine
        qry += " ISNULL(TSPL_SD_SALES_ORDER_DETAIL.Assessable,0) AS Assessable,ISNULL(TSPL_SD_SALES_ORDER_DETAIL.MRP,0) as MRP " + Environment.NewLine
        qry += " from TSPL_SD_SALES_ORDER_DETAIL " + Environment.NewLine
        qry += " left outer join TSPL_SD_SALES_ORDER_HEAD on TSPL_SD_SALES_ORDER_HEAD.Document_Code=TSPL_SD_SALES_ORDER_DETAIL.Document_Code " + Environment.NewLine
        qry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SALES_ORDER_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SD_SALES_ORDER_DETAIL.Unit_code  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALES_ORDER_DETAIL.Item_Code" + Environment.NewLine
        qry += " where  TSPL_SD_SALES_ORDER_HEAD.Document_Code in (" + clsCommon.GetMulcallString(arrPONo) + ")" + Environment.NewLine
        qry += " )xxx group by ICode,MRP"
        Dim dtAllData As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dtAllData IsNot Nothing AndAlso dtAllData.Rows.Count > 0 Then
            For Each dr As DataRow In dtAllData.Rows
                gv1.Rows.AddNew()
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDICode).Value = clsCommon.myCstr(dr("ICode"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDIName).Value = clsCommon.myCstr(dr("IName"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDUnit).Value = clsCommon.myCstr(dr("Unit"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDRate).Value = clsCommon.myCdbl(dr("Rate"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDMRP).Value = clsCommon.myCdbl(dr("MRP"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDQty).Value = clsCommon.myCdbl(dr("Qty"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDAvailableQty).Value = clsItemLocationDetails.getBalance(clsCommon.myCstr(dr("ICode")), txtToLoc.Value, txtTransferNo.Value, txtTransferDate.Value, Nothing, clsCommon.myCstr(dr("Unit")), clsCommon.myCdbl(dr("MRP")))

                Dim dblOrderQty As Double = gv1.Rows(gv1.Rows.Count - 1).Cells(colDQty).Value
                Dim dblActualQty As Double = gv1.Rows(gv1.Rows.Count - 1).Cells(colDAvailableQty).Value
                Dim dblTransferQty As Double = 0
                Dim dblRate As Double = gv1.Rows(gv1.Rows.Count - 1).Cells(colDRate).Value
                If dblActualQty >= dblOrderQty Then
                    dblTransferQty = 0
                Else
                    dblTransferQty = dblOrderQty - dblActualQty
                End If
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDTransferQty).Value = dblTransferQty
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDamount).Value = dblTransferQty * dblRate

                If IsItemRateeditable = False Then
                    Dim dblCostMethod As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Costing_Method as Costing_Method from TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code where Item_Code='" & clsCommon.myCstr(dr("ICode")) & "' "))
                    If dblCostMethod <> 0 Then
                        Dim dblUnitCost As Double = clsInventoryMovement.GetCost(dblCostMethod, clsCommon.myCstr(dr("ICode")), txtFromLoc.Value, 1, txtTransferDate.Value, txtTransferDate.Value, False, Nothing)
                        gv1.CurrentRow.Cells(colDRate).Value = dblUnitCost
                    Else
                        gv1.CurrentRow.Cells(colDRate).Value = 0
                    End If
                End If
            Next
            UpdateAllTotals()
        End If
        isInsideLoadData = False
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        btnCancelPressed()
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        btnOKPressed()

    End Sub

    Sub btnCancelPressed()
        Me.Close()
    End Sub
    Private Function btnOKPressed()
        Dim blnCreated As Boolean = False
        Dim strTransferOut As String
        Dim strGITLoc As String
        If clsCommon.myLen(txtFromLoc.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select From Location")
            txtFromLoc.Focus()
            Return False
        ElseIf CreateInvoice = True AndAlso AllowChangeInvoiceType = True Then
            If clsCommon.myLen(ddlInvoiceType.SelectedValue) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select Invoice Type")
                ddlInvoiceType.Focus()
                Return False
            End If

        End If

        ''''' TRANSFR START HERE
        If common.clsCommon.MyMessageBoxShow("Are you sure to Create Auto Transfer from location " & txtFromLoc.Value & " To " & txtToLoc.Value & "  ?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try
                strGITLoc = clsDBFuncationality.getSingleValue("select isnull(GIT_Location,'') from TSPL_LOCATION_MASTER where Location_Code='" & txtToLoc.Value & "'", trans)
                If clsCommon.myLen(strGITLoc) <= 0 Then
                    clsCommon.MyMessageBoxShow("Pls create GIT location for " & txtToLoc.Value & " ")
                    trans.Rollback()
                    Return False
                End If

                Dim obj As New clsTransferDCC
                obj.Form38 = chkForm38.Checked
                obj.Is_AgainstFormF = IIf(chkAgainst_Form.Checked, 1, 0)
                obj.Document_Date = txtTransferDate.Value
                obj.Delivery_date = txtTransferDate.Value
                obj.Delivery_Duration = 0
                obj.AutoTransfer = 1
                obj.Ref_No = txtRemarks.Text
                obj.Total_Tax_Amt = 0
                obj.Remarks = "Auto Transfer Out"
                obj.From_Location = txtFromLoc.Value
                obj.To_Location = strGITLoc
                obj.Comments = txtRemarks.Text
                obj.On_Hold = 0
                obj.Mode_Of_Transport = "By Road"

                obj.Tax_Group = ""
                obj.Transfer_Type = "O"


                obj.Terms_Code = ""
                obj.Terms_Remark = ""
                obj.Due_Date = txtTransferDate.Value
                obj.Discount_Base = 0
                obj.Total_Amt_Less_Tax = 0
                obj.Discount_Amt = 0
                obj.Amount_Less_Discount = lblTotRAmt1.Text
                obj.DOC_Total_Amt = lblTotRAmt1.Text

                obj.TransferOutNo = ""
                obj.Vehicle_Code = txtVehicleCode.Value
                obj.Vehicle_No = lblVehicleNo.Text
                obj.Km_Reading = 1
                obj.Is_AgainstFormF = 0
                obj.Type = ""
                obj.Tax_Calculation_Type = 0

                obj.Arr = New List(Of clsTransferDCCDetail)
                Dim arr As New List(Of String)
                'Dim strCode As String
                Dim intLineNo As Integer = 0
                For ii As Integer = 0 To gv1.Rows.Count - 1

                    Dim dblTransferQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTransferQty).Value)
                    If dblTransferQty > 0 Then
                        If IsItemRateeditable = True Then
                            If clsCommon.myCdbl(gv1.Rows(ii).Cells(colDRate).Value) = 0 Then
                                common.clsCommon.MyMessageBoxShow("Please enter Item Rate for " + clsCommon.myCstr(gv1.Rows(ii).Cells(colDICode).Value) + ". At Line No" + clsCommon.myCstr(ii + 1))
                                trans.Rollback()
                                Return False
                            End If
                        End If

                        Dim objTr As New clsTransferDCCDetail()


                        'objTr.Line_No = clsCommon.myCdbl(gv1.Rows(ii).Cells(colLineNo).Value)

                        objTr.Item_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDICode).Value)
                        objTr.Item_Desc = clsCommon.myCstr(gv1.Rows(ii).Cells(colDIName).Value)
                        objTr.Out_Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTransferQty).Value)
                        objTr.Unit_code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDUnit).Value)
                        objTr.MRP = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDMRP).Value)

                        Dim strICode As String = objTr.Item_Code
                        Dim dblEnteredQty As Double = objTr.Out_Qty
                        Dim dblMRP As Double = objTr.MRP
                        Dim dblBalQty As Double = clsItemLocationDetails.getBalance(objTr.Item_Code, txtFromLoc.Value, txtTransferNo.Value, txtTransferDate.Value, trans, objTr.Unit_code, objTr.MRP)

                        If dblEnteredQty > dblBalQty Then
                            Throw New Exception("Item - " + strICode + " , MRP - " + clsCommon.myCstr(dblMRP) + Environment.NewLine + "Entered Quantity - " + clsCommon.myCstr(dblEnteredQty) + " and Balance Quantity - " + clsCommon.myCstr(dblBalQty) + " at Location " + txtFromLoc.Value)
                        Else
                            intLineNo += 1
                            objTr.Line_No = intLineNo
                        End If
                        objTr.In_Qty = 0
                        objTr.Breakage = 0
                        objTr.Leak = 0
                        objTr.Shortage = 0
                        objTr.TransferOutNo = ""
                        objTr.Item_Cost = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDRate).Value)
                        objTr.Amount = Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colDamount).Value), 2)
                        objTr.Disc_Per = 0
                        objTr.Disc_Amt = 0
                        objTr.Amt_Less_Discount = objTr.Amount
                        objTr.Item_Net_Amt = objTr.Amount
                        objTr.Specification = ""
                        objTr.Remarks = ""
                        objTr.Location = txtFromLoc.Value
                        If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                            obj.Arr.Add(objTr)
                        End If
                    End If
                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow("Please Fill at list one Item")
                    trans.Rollback()
                    Return True
                    Exit Function
                End If




                obj.SaveData(obj, True, False, trans)
                obj.Document_No = obj.Document_No
                clsTransferDCC.postTransfer(obj.Document_No, trans)
                strTransferOut = obj.Document_No

                '' for load in 

                Dim objIn As New clsTransferDCC
                Dim objTransfer As New clsTransferDCC
                objTransfer = clsTransferDCC.GetData(strTransferOut, NavigatorType.Current, trans)

                If (objTransfer IsNot Nothing AndAlso clsCommon.myLen(objTransfer.Document_No) > 0) Then
                    objIn.Form38 = chkForm38.Checked
                    objIn.Is_AgainstFormF = IIf(chkAgainst_Form.Checked, 1, 0)
                    objIn.Document_Date = objTransfer.Document_Date
                    objIn.Delivery_date = objTransfer.Delivery_date
                    objIn.Delivery_Duration = 0

                    objIn.Ref_No = objTransfer.Ref_No
                    objIn.Total_Tax_Amt = objTransfer.Total_Tax_Amt
                    objIn.Remarks = "Auto Transfer In"
                    objIn.From_Location = strGITLoc
                    objIn.To_Location = txtToLoc.Value
                    objIn.Comments = objTransfer.Comments
                    objIn.On_Hold = 0
                    objIn.Mode_Of_Transport = "By Road"
                    objIn.AutoTransfer = 1
                    objIn.Tax_Group = ""
                    objIn.Transfer_Type = "I"


                    objIn.Terms_Code = objTransfer.Terms_Code
                    objIn.Terms_Remark = objTransfer.Terms_Remark
                    objIn.Due_Date = objTransfer.Due_Date
                    objIn.Discount_Base = objTransfer.Discount_Base
                    objIn.Total_Amt_Less_Tax = objTransfer.Total_Amt_Less_Tax
                    objIn.Discount_Amt = 0
                    objIn.Amount_Less_Discount = objTransfer.Amount_Less_Discount
                    objIn.DOC_Total_Amt = objTransfer.DOC_Total_Amt

                    objIn.TransferOutNo = strTransferOut
                    objIn.Vehicle_Code = objTransfer.Vehicle_Code
                    objIn.Vehicle_No = objTransfer.Vehicle_No
                    objIn.Km_Reading = 1
                    objIn.Is_AgainstFormF = 0
                    objIn.Type = ""
                    objIn.Tax_Calculation_Type = objTransfer.Tax_Calculation_Type

                    objIn.Arr = New List(Of clsTransferDCCDetail)
                    For Each objInDetail As clsTransferDCCDetail In objTransfer.Arr

                        Dim objTrIn As New clsTransferDCCDetail()


                        objTrIn.Line_No = objInDetail.Line_No
                        objTrIn.Item_Code = objInDetail.Item_Code
                        objTrIn.Item_Desc = objInDetail.Item_Desc
                        objTrIn.In_Qty = objInDetail.Out_Qty
                        objTrIn.Unit_code = objInDetail.Unit_code
                        objTrIn.MRP = objInDetail.MRP
                        objTrIn.Breakage = 0
                        objTrIn.Leak = 0
                        objTrIn.Shortage = 0

                        objTrIn.TransferOutNo = strTransferOut
                        objTrIn.Item_Cost = objInDetail.Item_Cost
                        objTrIn.Amount = objInDetail.Amount
                        objTrIn.Disc_Per = objInDetail.Disc_Per
                        objTrIn.Disc_Amt = objInDetail.Disc_Amt
                        objTrIn.Amt_Less_Discount = objInDetail.Amt_Less_Discount

                        objTrIn.Item_Net_Amt = objInDetail.Item_Net_Amt
                        objTrIn.Specification = ""
                        objTrIn.Remarks = ""
                        objTrIn.Location = txtFromLoc.Value 'clsCommon.myCstr(grow.Cells(colLocationCode).Value)

                        If (clsCommon.myLen(objTrIn.Item_Code) > 0) Then
                            objIn.Arr.Add(objTrIn)
                        End If

                    Next
                    If (objIn.Arr Is Nothing OrElse objIn.Arr.Count <= 0) Then
                        common.clsCommon.MyMessageBoxShow("Please Fill at list one Item")
                        trans.Rollback()

                    End If

                End If


                objIn.SaveData(objIn, True, False, trans)
                objIn.Document_No = objIn.Document_No
                clsTransferDCC.postTransfer(objIn.Document_No, trans)


                '''''TRANSFER COMPLETED
                ''''' SHIPMENT START HERE
                For ii As Integer = 0 To gvHead.Rows.Count - 1
                    If clsCommon.myCBool(gvHead.Rows(ii).Cells(colHSelect).Value) Then
                        Dim strOrderNo As String = gvHead.Rows(ii).Cells(colHCode).Value

                        Dim objShipment As New clsSNShipmentHead
                        Dim objOrder As New clsSNSalesOrderHead
                        objOrder = clsSNSalesOrderHead.GetData(strOrderNo, NavigatorType.Current, trans)

                        If (objOrder IsNot Nothing AndAlso clsCommon.myLen(objOrder.Document_Code) > 0) Then
                            objShipment.Cust_PO_No = objOrder.Cust_PO_No
                            objShipment.Podate = objOrder.Document_Date
                            objShipment.Invoice_Type = ddlInvoiceType.SelectedValue
                            objShipment.Route_No = objOrder.Route_No
                            objShipment.Route_Desc = objOrder.Route_Desc
                            objShipment.Price_Group_Code = objOrder.Price_Group_Code
                            objShipment.Price_Code = objOrder.Price_Code
                            objShipment.HeadDisc_Per = objOrder.HeadDisc_Per
                            objShipment.HeadDisc_Amt = objOrder.HeadDisc_Amt

                            objShipment.Document_Date = txtTransferDate.Value
                            objShipment.Customer_Code = objOrder.Customer_Code
                            objShipment.Customer_Name = objOrder.Customer_Name
                            objShipment.Ref_No = objOrder.Ref_No
                            objShipment.Inv_Date = txtTransferDate.Value
                            objShipment.Challan_Date = txtTransferDate.Value
                            objShipment.Total_Tax_Amt = objOrder.Total_Tax_Amt
                            objShipment.Inv_No = objShipment.Inv_No
                            objShipment.Bill_To_Location = objOrder.Bill_To_Location
                            objShipment.Ship_To_Location = objOrder.Ship_To_Location
                            objShipment.Comments = "Auto STN shipment for Transfer No " & strTransferOut & ""
                            objShipment.On_Hold = objOrder.On_Hold
                            objShipment.Description = objOrder.Description
                            objShipment.Tax_Group = objOrder.Tax_Group
                            objShipment.Salesman_Code = objOrder.Salesman_Code
                            objShipment.Salesman_Name = objOrder.Salesman_Name
                            objShipment.Is_Internal = objShipment.Is_Internal
                            objShipment.PROJECT_ID = objOrder.PROJECT_ID

                            If clsCommon.myLen(objOrder.TAX1) > 0 Then
                                objShipment.TAX1 = objOrder.TAX1
                                objShipment.TAX1_Rate = objOrder.TAX1_Rate
                                objShipment.TAX1_Base_Amt = objOrder.TAX1_Base_Amt
                                objShipment.TAX1_Amt = objOrder.TAX1_Amt
                            End If
                            If clsCommon.myLen(objOrder.TAX2) > 0 Then
                                objShipment.TAX2 = objOrder.TAX2
                                objShipment.TAX2_Rate = objOrder.TAX2_Rate
                                objShipment.TAX2_Base_Amt = objOrder.TAX2_Base_Amt
                                objShipment.TAX2_Amt = objOrder.TAX2_Amt
                            End If
                            If clsCommon.myLen(objOrder.TAX3) > 0 Then
                                objShipment.TAX3 = objOrder.TAX3
                                objShipment.TAX3_Rate = objOrder.TAX3_Rate
                                objShipment.TAX3_Base_Amt = objOrder.TAX3_Base_Amt
                                objShipment.TAX3_Amt = objOrder.TAX3_Amt
                            End If
                            If clsCommon.myLen(objOrder.TAX4) > 0 Then
                                objShipment.TAX4 = objOrder.TAX4
                                objShipment.TAX4_Rate = objOrder.TAX4_Rate
                                objShipment.TAX4_Base_Amt = objOrder.TAX4_Base_Amt
                                objShipment.TAX4_Amt = objOrder.TAX4_Amt
                            End If
                            If clsCommon.myLen(objOrder.TAX5) > 0 Then
                                objShipment.TAX5 = objOrder.TAX5
                                objShipment.TAX5_Rate = objOrder.TAX5_Rate
                                objShipment.TAX5_Base_Amt = objOrder.TAX5_Base_Amt
                                objShipment.TAX5_Amt = objOrder.TAX5_Amt
                            End If
                            If clsCommon.myLen(objOrder.TAX6) > 0 Then
                                objShipment.TAX6 = objOrder.TAX6
                                objShipment.TAX6_Rate = objOrder.TAX6_Rate
                                objShipment.TAX6_Base_Amt = objOrder.TAX6_Base_Amt
                                objShipment.TAX6_Amt = objOrder.TAX6_Amt
                            End If
                            If clsCommon.myLen(objOrder.TAX7) > 0 Then
                                objShipment.TAX7 = objOrder.TAX7
                                objShipment.TAX7_Rate = objOrder.TAX7_Rate
                                objShipment.TAX7_Base_Amt = objOrder.TAX7_Base_Amt
                                objShipment.TAX7_Amt = objOrder.TAX7_Amt
                            End If
                            If clsCommon.myLen(objOrder.TAX8) > 0 Then
                                objShipment.TAX8 = objOrder.TAX8
                                objShipment.TAX8_Rate = objOrder.TAX8_Rate
                                objShipment.TAX8_Base_Amt = objOrder.TAX8_Base_Amt
                                objShipment.TAX8_Amt = objOrder.TAX8_Amt
                            End If
                            If clsCommon.myLen(objOrder.TAX9) > 0 Then
                                objShipment.TAX9 = objOrder.TAX9
                                objShipment.TAX9_Rate = objOrder.TAX9_Rate
                                objShipment.TAX9_Base_Amt = objOrder.TAX9_Base_Amt
                                objShipment.TAX9_Amt = objOrder.TAX9_Amt
                            End If
                            If clsCommon.myLen(objOrder.TAX10) > 0 Then
                                objShipment.TAX10 = objOrder.TAX10
                                objShipment.TAX10_Rate = objOrder.TAX10_Rate
                                objShipment.TAX10_Base_Amt = objOrder.TAX10_Base_Amt
                                objShipment.TAX10_Amt = objOrder.TAX10_Amt
                            End If

                            objShipment.Terms_Code = objOrder.Terms_Code
                            objShipment.Due_Date = objOrder.Due_Date
                            objShipment.Discount_Base = objOrder.Discount_Base
                            objShipment.Discount_Amt = objOrder.Discount_Amt
                            objShipment.Amount_Less_Discount = objOrder.Amount_Less_Discount
                            objShipment.Total_Amt = objOrder.Total_Amt
                            objShipment.Carrier = ""
                            objShipment.Vehicle_Code = txtVehicleCode.Value
                            objShipment.VehicleNo = lblVehicleNo.Text
                            objShipment.GRNo = ""
                            objShipment.GEDate = objShipment.GEDate

                            objShipment.Item_Type = objOrder.Item_Type
                            objShipment.Dept = objOrder.Dept
                            objShipment.Dept_Desc = objOrder.Dept_Desc
                            objShipment.Against_Sales_Order = objOrder.Document_Code


                            If (clsCommon.myLen(objShipment.Add_Charge_Code1) > 0) Then
                                objShipment.Add_Charge_Code1 = objOrder.Add_Charge_Code1
                                objShipment.Add_Charge_Name1 = objOrder.Add_Charge_Name1
                                objShipment.Add_Charge_Amt1 = objOrder.Add_Charge_Amt1
                            End If
                            If (clsCommon.myLen(objShipment.Add_Charge_Code2) > 0) Then

                                objShipment.Add_Charge_Code2 = objOrder.Add_Charge_Code2
                                objShipment.Add_Charge_Name2 = objOrder.Add_Charge_Name2
                                objShipment.Add_Charge_Amt2 = objOrder.Add_Charge_Amt2

                            End If
                            If (clsCommon.myLen(objShipment.Add_Charge_Code3) > 0) Then
                                objShipment.Add_Charge_Code3 = objShipment.Add_Charge_Code3
                                objShipment.Add_Charge_Name3 = objShipment.Add_Charge_Name3
                                objShipment.Add_Charge_Amt3 = objShipment.Add_Charge_Amt3
                            End If
                            If (clsCommon.myLen(objShipment.Add_Charge_Code4) > 0) Then
                                objShipment.Add_Charge_Code4 = objShipment.Add_Charge_Code4
                                objShipment.Add_Charge_Name4 = objShipment.Add_Charge_Name4
                                objShipment.Add_Charge_Amt4 = objShipment.Add_Charge_Amt4
                            End If
                            If (clsCommon.myLen(objShipment.Add_Charge_Code5) > 0) Then
                                objShipment.Add_Charge_Code5 = objShipment.Add_Charge_Code5
                                objShipment.Add_Charge_Name5 = objShipment.Add_Charge_Name5
                                objShipment.Add_Charge_Amt5 = objShipment.Add_Charge_Amt5
                            End If
                            If (clsCommon.myLen(objShipment.Add_Charge_Code6) > 0) Then
                                objShipment.Add_Charge_Code6 = objShipment.Add_Charge_Code6
                                objShipment.Add_Charge_Name6 = objShipment.Add_Charge_Name6
                                objShipment.Add_Charge_Amt6 = objShipment.Add_Charge_Amt6
                            End If
                            If (clsCommon.myLen(objShipment.Add_Charge_Code7) > 0) Then
                                objShipment.Add_Charge_Code7 = objShipment.Add_Charge_Code7
                                objShipment.Add_Charge_Name7 = objShipment.Add_Charge_Name7
                                objShipment.Add_Charge_Amt7 = objShipment.Add_Charge_Amt7
                            End If
                            If (clsCommon.myLen(objShipment.Add_Charge_Code8) > 0) Then
                                objShipment.Add_Charge_Code8 = objShipment.Add_Charge_Code8
                                objShipment.Add_Charge_Name8 = objShipment.Add_Charge_Name8
                                objShipment.Add_Charge_Amt8 = objShipment.Add_Charge_Amt8
                            End If
                            If (clsCommon.myLen(objShipment.Add_Charge_Code9) > 0) Then
                                objShipment.Add_Charge_Code9 = objShipment.Add_Charge_Code9
                                objShipment.Add_Charge_Name9 = objShipment.Add_Charge_Name9
                                objShipment.Add_Charge_Amt9 = objShipment.Add_Charge_Amt9
                            End If
                            If (clsCommon.myLen(objShipment.Add_Charge_Code10) > 0) Then
                                objShipment.Add_Charge_Code10 = objShipment.Add_Charge_Code10
                                objShipment.Add_Charge_Name10 = objShipment.Add_Charge_Name10
                                objShipment.Add_Charge_Amt10 = objShipment.Add_Charge_Amt10
                            End If
                            objShipment.Total_Add_Charge = objShipment.Total_Add_Charge

                            objShipment.Tax_Calculation_Type = objShipment.Tax_Calculation_Type
                            If PostShipment = True And CreateInvoice = True Then
                                objShipment.Is_Create_Auto_Invoice = True
                            End If
                            objShipment.Is_Create_Auto_Invoice = objShipment.Is_Create_Auto_Invoice
                            objShipment.Is_Create_Auto_Receipt = objShipment.Is_Create_Auto_Receipt
                            objShipment.PROJECT_ID = objShipment.PROJECT_ID

                            objShipment.Arr = New List(Of clsSNShipmentDetail)
                            For Each objOrderDetail As clsSNSalesOrderDetail In objOrder.Arr
                                Dim objShipmentDetail As New clsSNShipmentDetail()
                                objShipmentDetail.Line_No = objOrderDetail.Line_No
                                objShipmentDetail.Row_Type = objOrderDetail.Row_Type
                                objShipmentDetail.Item_Code = objOrderDetail.Item_Code
                                objShipmentDetail.Item_Desc = objOrderDetail.Item_Desc
                                'objTr.Bar_Code = clsCommon.myCstr(grow.Cells(colBarCode).Value)
                                objShipmentDetail.Qty = objOrderDetail.Qty

                                objShipmentDetail.Free_Qty = objShipmentDetail.Free_Qty

                                objShipmentDetail.Unit_code = objOrderDetail.Unit_code
                                objShipmentDetail.Order_Code = objOrderDetail.Document_Code
                                'objTr.Location = clsCommon.myCstr(grow.Cells(colloc).Value)
                                objShipmentDetail.Item_Cost = objOrderDetail.Item_Cost
                                objShipmentDetail.Amount = objOrderDetail.Amount
                                objShipmentDetail.Disc_Per = objOrderDetail.Disc_Per
                                objShipmentDetail.Disc_Amt = objOrderDetail.Disc_Amt
                                objShipmentDetail.Amt_Less_Discount = objOrderDetail.Amt_Less_Discount
                                objShipmentDetail.TAX1 = objOrderDetail.TAX1
                                objShipmentDetail.TAX1_Base_Amt = objOrderDetail.TAX1_Base_Amt
                                objShipmentDetail.TAX1_Rate = objOrderDetail.TAX1_Rate
                                objShipmentDetail.TAX1_Amt = objOrderDetail.TAX1_Amt
                                objShipmentDetail.TAX2 = objOrderDetail.TAX2
                                objShipmentDetail.TAX2_Base_Amt = objOrderDetail.TAX2_Base_Amt
                                objShipmentDetail.TAX2_Rate = objOrderDetail.TAX2_Rate
                                objShipmentDetail.TAX2_Amt = objOrderDetail.TAX2_Amt
                                objShipmentDetail.TAX3 = objOrderDetail.TAX3
                                objShipmentDetail.TAX3_Base_Amt = objOrderDetail.TAX3_Base_Amt
                                objShipmentDetail.TAX3_Rate = objOrderDetail.TAX3_Rate
                                objShipmentDetail.TAX3_Amt = objOrderDetail.TAX3_Amt
                                objShipmentDetail.TAX4 = objOrderDetail.TAX4
                                objShipmentDetail.TAX4_Base_Amt = objOrderDetail.TAX4_Base_Amt
                                objShipmentDetail.TAX4_Rate = objOrderDetail.TAX4_Rate
                                objShipmentDetail.TAX4_Amt = objOrderDetail.TAX4_Amt
                                objShipmentDetail.TAX5 = objOrderDetail.TAX5
                                objShipmentDetail.TAX5_Base_Amt = objOrderDetail.TAX5_Base_Amt
                                objShipmentDetail.TAX5_Rate = objOrderDetail.TAX5_Rate
                                objShipmentDetail.TAX5_Amt = objOrderDetail.TAX5_Amt
                                objShipmentDetail.TAX6 = objOrderDetail.TAX6
                                objShipmentDetail.TAX6_Base_Amt = objOrderDetail.TAX6_Base_Amt
                                objShipmentDetail.TAX6_Rate = objOrderDetail.TAX6_Rate
                                objShipmentDetail.TAX6_Amt = objOrderDetail.TAX6_Amt
                                objShipmentDetail.TAX7 = objOrderDetail.TAX7
                                objShipmentDetail.TAX7_Base_Amt = objOrderDetail.TAX7_Base_Amt
                                objShipmentDetail.TAX7_Rate = objOrderDetail.TAX7_Rate
                                objShipmentDetail.TAX7_Amt = objOrderDetail.TAX7_Amt
                                objShipmentDetail.TAX8 = objOrderDetail.TAX8
                                objShipmentDetail.TAX8_Base_Amt = objOrderDetail.TAX8_Base_Amt
                                objShipmentDetail.TAX8_Rate = objOrderDetail.TAX8_Rate
                                objShipmentDetail.TAX8_Amt = objOrderDetail.TAX8_Amt
                                objShipmentDetail.TAX9 = objOrderDetail.TAX9
                                objShipmentDetail.TAX9_Base_Amt = objOrderDetail.TAX9_Base_Amt
                                objShipmentDetail.TAX9_Rate = objOrderDetail.TAX9_Rate
                                objShipmentDetail.TAX9_Amt = objOrderDetail.TAX9_Amt
                                objShipmentDetail.TAX10 = objOrderDetail.TAX10
                                objShipmentDetail.TAX10_Base_Amt = objOrderDetail.TAX10_Base_Amt
                                objShipmentDetail.TAX10_Rate = objOrderDetail.TAX10_Rate
                                objShipmentDetail.TAX10_Amt = objOrderDetail.TAX10_Amt
                                objShipmentDetail.Total_Tax_Amt = objOrderDetail.Total_Tax_Amt
                                objShipmentDetail.Item_Net_Amt = objOrderDetail.Item_Net_Amt
                                objShipmentDetail.Location = objOrderDetail.Location
                                objShipmentDetail.MRP = objOrderDetail.MRP

                                objShipmentDetail.Scheme_Applicable = objOrderDetail.Scheme_Applicable
                                objShipmentDetail.Scheme_Code = objOrderDetail.Scheme_Code
                                objShipmentDetail.Scheme_Item = objOrderDetail.Scheme_Item
                                objShipmentDetail.Item_Tax = objOrderDetail.Item_Tax
                                objShipmentDetail.Total_MRP_Amt = objOrderDetail.Total_MRP_Amt
                                objShipmentDetail.Total_Basic_Amt = objOrderDetail.Total_Basic_Amt
                                objShipmentDetail.Total_Disc_Amt = objOrderDetail.Total_Disc_Amt
                                objShipmentDetail.Cust_Discount = objOrderDetail.Cust_Discount
                                objShipmentDetail.Total_Cust_Discount = objOrderDetail.Total_Cust_Discount
                                objShipmentDetail.ActualRate = objOrderDetail.ActualRate
                                objShipmentDetail.Cust_DiscountQty = objOrderDetail.Cust_DiscountQty
                                objShipmentDetail.Price_Date = objOrderDetail.Price_Date
                                objShipmentDetail.Price_code = objOrderDetail.Price_code
                                objShipmentDetail.Abatement_Per = objOrderDetail.Abatement_Per
                                objShipmentDetail.Abatement_Amt = objOrderDetail.Abatement_Amt
                                objShipmentDetail.FOC_Item = objOrderDetail.FOC_Item
                                objShipmentDetail.Item_Weight = objOrderDetail.Item_Weight
                                objShipmentDetail.Conv_Factor = objOrderDetail.Conv_Factor
                                objShipmentDetail.TotalItem_Weight = objOrderDetail.TotalItem_Weight
                                objShipmentDetail.Markup_On = objOrderDetail.Markup_On
                                objShipmentDetail.Markup_Percent = objOrderDetail.Markup_Percent
                                objShipmentDetail.Landing_Cost = objOrderDetail.Landing_Cost
                                objShipmentDetail.CustDiscPer = objOrderDetail.CustDiscPer
                                objShipmentDetail.HeadDiscAmt = objOrderDetail.HeadDiscAmt
                                objShipmentDetail.CasdDiscScheme_Code = objOrderDetail.CasdDiscScheme_Code
                                objShipmentDetail.Purchase_Cost = objOrderDetail.Purchase_Cost
                                objShipmentDetail.OrgRate = objOrderDetail.OrgRate
                                objShipmentDetail.PrincipleCode = objOrderDetail.PrincipleCode
                                objShipmentDetail.PrincipleDesc = objOrderDetail.PrincipleDesc
                                objShipmentDetail.vendor_code = objOrderDetail.vendor_code
                                objShipmentDetail.vendor_desc = objOrderDetail.vendor_desc

                                ''objShipmentDetail.Assessable = clsCommon.myCdbl(grow.Cells(colAssessableRate).Value)
                                ''objShipmentDetail.AssessableAmt = clsCommon.myCdbl(grow.Cells(colAssessableAmount).Value)
                                objShipmentDetail.Batch_No = objOrderDetail.Batch_No

                                objShipmentDetail.Expiry_Date = objOrderDetail.Expiry_Date


                                objShipmentDetail.MFG_Date = objOrderDetail.MFG_Date

                                objShipmentDetail.Specification = objOrderDetail.Specification
                                objShipmentDetail.Remarks = objOrderDetail.Remarks
                                'objShipmentDetail.Is_Mannual_Amt = objOrderDetail.Is_Mannual_Amt

                                objShipmentDetail.Balance_Qty = objOrderDetail.Balance_Qty





                                If (clsCommon.myLen(objShipmentDetail.Item_Code) > 0) Then
                                    objShipment.Arr.Add(objShipmentDetail)
                                End If
                            Next
                            If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                                common.clsCommon.MyMessageBoxShow("Please Fill at list one Item")
                                trans.Rollback()
                                Return False
                            End If





                            objShipment.CURRENCY_CODE = objOrder.CURRENCY_CODE
                            objShipment.ApplicableFrom = objOrder.ApplicableFrom
                            objShipment.ConvRate = objOrder.ConvRate

                            '' end CurrencyConversion

                        End If

                        objShipment.Form_ID = ""
                        If (objShipment.SaveData(objShipment, True, trans)) Then
                            If PostShipment Then
                                If (clsSNShipmentHead.PostData("", objShipment.Document_Code, trans)) Then
                                    blnCreated = True
                                End If
                            Else
                                blnCreated = True
                            End If

                        End If
                    End If
                Next
                If blnCreated = True Then
                    trans.Commit()
                    common.clsCommon.MyMessageBoxShow("Transfer No " & strTransferOut & "  created Successfully")
                    LoadBlankHeadGrid()
                    LoadBlankGridDetail()

                End If

                ''''' SHIPMENT ENDS HERE


                Return True
            Catch ex As Exception
                trans.Rollback()
                common.clsCommon.MyMessageBoxShow(ex.Message)
            End Try
        End If
        Return True
    End Function
    Private Sub UpdateAllTotals()
        Dim dblNetAmt As Double = 0
        For ii As Integer = 0 To gv1.Rows.Count - 1
            If (clsCommon.myLen(gv1.Rows(ii).Cells(colDICode).Value) > 0) Then
                dblNetAmt = dblNetAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colDamount).Value)
            End If
        Next
        lblTotRAmt1.Text = clsCommon.myFormat(dblNetAmt)
    End Sub

    Private Sub gvHead_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvHead.CellValueChanged
        'If e.Column Is gvHead.Columns(colHSelect) Then
        '    gvHead.CurrentColumn = gvHead.Columns(colHCode)
        '    gvHead.CurrentColumn = gvHead.Columns(colHSelect)
        'End If

        'LoadDetailData()
    End Sub

    Private Sub gvHead_ValueChanging(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.ValueChangingEventArgs) Handles gvHead.ValueChanging
        Try
            If Not IsInsideLoadData Then
                If Not isCellValueChanged Then
                    isCellValueChanged = True
                    If gvHead.CurrentColumn Is gvHead.Columns(colHSelect) Then
                        gvHead.CurrentRow.Cells(colHSelect).Value = e.NewValue
                    End If
                    LoadDetailData()
                    isCellValueChanged = False
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtVehicleCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtVehicleCode._MYValidating
        Try
            Dim qry As String = "Select distinct  vehicle_id ,Description from TSPL_VEHICLE_MASTER"
            txtVehicleCode.Value = clsCommon.ShowSelectForm("Vehicle No", qry, "vehicle_id", "", txtVehicleCode.Value, "vehicle_id", isButtonClicked)
            lblVehicleNo.Text = connectSql.RunScalar("Select Description  from TSPL_VEHICLE_MASTER where Vehicle_Id = '" + Convert.ToString(txtVehicleCode.Value) + "'")
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        chkAgainst_Form.Checked = False
        chkForm38.Checked = False
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        If (Not IsInsideLoadData) Then
            If e.Column Is gv1.Columns(colDRate) OrElse e.Column Is gv1.Columns(colDTransferQty) Then
                Dim dblRate As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colDRate).Value)
                If dblRate = 0 Then
                    common.clsCommon.MyMessageBoxShow("Item cost should br greater than 0 . ")
                Else
                    Dim dblTransferQty As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colDTransferQty).Value)
                    Dim dblAmt As Double = dblRate * dblTransferQty
                    gv1.CurrentRow.Cells(colDamount).Value = dblAmt
                    UpdateAllTotals()
                End If

            End If
        End If
    End Sub
End Class
