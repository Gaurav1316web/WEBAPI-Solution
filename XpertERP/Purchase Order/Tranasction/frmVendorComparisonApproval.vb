Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Public Class frmVendorComparisonApproval
    Inherits FrmMainTranScreen
#Region "Variables"
    Private isCellValueChangedOpen As Boolean = False
    Private isNewEntry As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
#End Region

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.VendorComparisonApproval)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnCreatePO.Visible = MyBase.isPostFlag
    End Sub

    Private Sub txtRFQNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtRFQNo._MYValidating
        Dim qry As String = "select RFQ_no as RFQNO,convert(varchar,RFQ_Date,103) as DATE from tspl_RFQ_head"
        Dim whrclas As String = " Is_Post=1"
        txtRFQNo.Value = clsCommon.ShowSelectForm("RFQVE", qry, "RFQNO", whrclas, txtRFQNo.Value, "RFQNO", isButtonClicked)
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select tspl_RFQ_head.RFQ_Date,tspl_RFQ_head.Requisition_id,TSPL_REQUISITION_HEAD.requisition_date from TSPL_REQUISITION_HEAD left outer join tspl_RFQ_head on TSPL_REQUISITION_HEAD.requisition_id=tspl_RFQ_head.Requisition_id where RFQ_no ='" + txtRFQNo.Value + "'")
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            txtRFQDate.Text = dt.Rows(0)("RFQ_Date")
            txtReqNo.Text = dt.Rows(0)("Requisition_id")
            txtReqDate.Text = dt.Rows(0)("requisition_date")
            LoadData()
        End If
    End Sub

    Public Sub LoadData()
        Try
            Dim qry As String = "select CAST(Is_Select_For_PO as bit) as sel, TSPL_VENDOR_QUOTATION_HEAD.Code,TSPL_VENDOR_QUOTATION_HEAD.VQDate,TSPL_VENDOR_QUOTATION_HEAD.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_VENDOR_QUOTATION_DETAIL.Line_No,TSPL_VENDOR_QUOTATION_DETAIL.Row_Type,TSPL_VENDOR_QUOTATION_DETAIL.Item_Code,TSPL_VENDOR_QUOTATION_DETAIL.Item_Desc, TSPL_VENDOR_QUOTATION_DETAIL.Qty,TSPL_VENDOR_QUOTATION_DETAIL.Unit_Code,TSPL_VENDOR_QUOTATION_DETAIL.Item_Cost,TSPL_VENDOR_QUOTATION_DETAIL.Item_Net_Amt,TSPL_VENDOR_QUOTATION_DETAIL.Specification,TSPL_VENDOR_QUOTATION_DETAIL.Remarks"
            qry += " from TSPL_VENDOR_QUOTATION_DETAIL "
            qry += " left outer join TSPL_VENDOR_QUOTATION_HEAD on TSPL_VENDOR_QUOTATION_HEAD.Code=TSPL_VENDOR_QUOTATION_DETAIL.Code"
            qry += " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VENDOR_QUOTATION_HEAD.Vendor_Code"
            qry += " where TSPL_VENDOR_QUOTATION_HEAD.Status=1 and TSPL_VENDOR_QUOTATION_HEAD.RFQ_NO='" + txtRFQNo.Value + "' and Is_Select_For_PO=1"
            qry += " ORDER BY Code,Line_No"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt.Rows.Count <= 0 Then
                gv1.DataSource = Nothing
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
                Exit Sub
            End If

            btnCreatePO.Enabled = True
            UsLock1.Status = FrmVendorComparison1.GetStatus(txtRFQNo.Value)
            If UsLock1.Status = ERPTransactionStatus.Approved Then
                btnCreatePO.Enabled = False
            End If


            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.DataSource = Nothing
            gv1.Columns.Clear()
            gv1.Rows.Clear()
            gv1.DataSource = dt

            gv1.AllowDeleteRow = False
            gv1.AllowAddNewRow = False
            gv1.ShowGroupPanel = True
            gv1.AllowColumnReorder = True
            gv1.AllowRowReorder = False
            gv1.EnableSorting = False

            For ii As Integer = 0 To gv1.Columns.Count - 1
                gv1.Columns(ii).ReadOnly = True
                gv1.Columns(ii).IsVisible = False
            Next

            gv1.Columns("sel").ReadOnly = False
            gv1.Columns("sel").IsVisible = True
            gv1.Columns("sel").Width = 30
            gv1.Columns("sel").HeaderText = " "

            gv1.Columns("Code").IsVisible = True
            gv1.Columns("Code").Width = 100
            gv1.Columns("Code").HeaderText = "Quotation No"

            gv1.Columns("VQDate").IsVisible = True
            gv1.Columns("VQDate").Width = 80
            gv1.Columns("VQDate").HeaderText = "Quotation Date"

            gv1.Columns("Vendor_Code").IsVisible = True
            gv1.Columns("Vendor_Code").Width = 100
            gv1.Columns("Vendor_Code").HeaderText = "Vendor Code"

            gv1.Columns("Vendor_Name").IsVisible = True
            gv1.Columns("Vendor_Name").Width = 150
            gv1.Columns("Vendor_Name").HeaderText = "Vendor"

            gv1.Columns("Line_No").IsVisible = True
            gv1.Columns("Line_No").Width = 30
            gv1.Columns("Line_No").HeaderText = "SNo"

            gv1.Columns("Row_Type").IsVisible = True
            gv1.Columns("Row_Type").Width = 80
            gv1.Columns("Row_Type").HeaderText = "Row Type"

            gv1.Columns("Item_Code").IsVisible = True
            gv1.Columns("Item_Code").Width = 100
            gv1.Columns("Item_Code").HeaderText = "Item Code"

            gv1.Columns("Item_Desc").IsVisible = True
            gv1.Columns("Item_Desc").Width = 150
            gv1.Columns("Item_Desc").HeaderText = "Item"


            gv1.Columns("Qty").IsVisible = True
            gv1.Columns("Qty").Width = 80
            gv1.Columns("Qty").HeaderText = "Quantity"
            gv1.Columns("Qty").FormatString = "{0:F2}"

            gv1.Columns("Unit_Code").IsVisible = True
            gv1.Columns("Unit_Code").Width = 100
            gv1.Columns("Unit_Code").HeaderText = "Unit Code"

            gv1.Columns("Item_Cost").IsVisible = True
            gv1.Columns("Item_Cost").Width = 100
            gv1.Columns("Item_Cost").HeaderText = "Cost"
            gv1.Columns("Item_Cost").FormatString = "{0:F2}"

            gv1.Columns("Item_Net_Amt").IsVisible = True
            gv1.Columns("Item_Net_Amt").Width = 100
            gv1.Columns("Item_Net_Amt").HeaderText = "Amount"
            gv1.Columns("Item_Net_Amt").FormatString = "{0:F2}"


            gv1.Columns("Specification").IsVisible = True
            gv1.Columns("Specification").Width = 100
            gv1.Columns("Specification").HeaderText = "Specification"

            gv1.Columns("Remarks").IsVisible = True
            gv1.Columns("Remarks").Width = 100
            gv1.Columns("Remarks").HeaderText = "Remarks"

            gv1.GroupDescriptors.Add(New GridGroupByExpression("Code as Code format ""{0}: {1}"" Group By Code"))

            gv1.MasterTemplate.ExpandAllGroups()
            gv1.ShowGroupPanel = False
            gv1.MasterTemplate.AutoExpandGroups = True

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item1 As New GridViewSummaryItem("Item_Net_Amt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)

            Dim item2 As New GridViewSummaryItem("Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    'Add Tool tip Task No- TEC/22/05/18-000245
    Private Sub frmVendorComparisonApproval_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnCreatePO, "TSPL_PURCHASE_ORDER_HEAD " + Environment.NewLine + _
                                                  "TSPL_PURCHASE_ORDER_DETAIL " + Environment.NewLine + _
                                                  "TSPL_ROADPERMIT_ISSUE_RECEIVE_DETAIL (If Road Permit) " + Environment.NewLine + _
                                                  "TSPL_CFORM_ISSUE_RECEIVE_DETAIL (If C Form) " + Environment.NewLine + _
                                                  "tspl_Purchase_Order_work_order " + Environment.NewLine + _
                                                  "TSPL_PURCHASE_ORDER_WORK_ORDER_Terms ")
        End If
    End Sub
    'Add Tool tip Task No- TEC/22/05/18-000245
    Private Sub FrmVendorComparison1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        txtReqDate.Text = ""
        txtReqNo.Text = ""
        txtRFQDate.Text = ""
        txtRFQNo.Value = ""
        UsLock1.Status = ERPTransactionStatus.Pending
        gv1.GroupDescriptors.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
    End Sub

    Private Sub gv1_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.ViewCellFormatting
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub

    Private Sub btnCreatePO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreatePO.Click
        Try
            Dim arrQuotation As New List(Of String)
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myCBool(gv1.Rows(ii).Cells("Sel").Value) AndAlso Not arrQuotation.Contains(clsCommon.myCstr(gv1.Rows(ii).Cells("Code").Value)) Then
                    arrQuotation.Add(clsCommon.myCstr(gv1.Rows(ii).Cells("Code").Value))
                End If
            Next
            If arrQuotation.Count = 0 Then
                Throw New Exception("No Quotation no found to make PO")
            End If

            Dim strLastQuotationNo As String = ""
            Dim ArrPO As New List(Of clsPurchaseOrderHead)
            Dim obj As clsPurchaseOrderHead = Nothing
            Dim lineNo As Integer = 1
            Dim objReqNo As clsRequistionHead = clsRequistionHead.GetData(txtReqNo.Text, NavigatorType.Current, "")
            If clsCommon.MyMessageBoxShow("Do you want to Create " + clsCommon.myCstr(arrQuotation.Count) + " Purchase Order" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                For ii As Integer = 0 To gv1.Rows.Count - 1
                    If clsCommon.myCBool(gv1.Rows(ii).Cells("Sel").Value) Then
                        If Not clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells("Code").Value), strLastQuotationNo) = CompairStringResult.Equal Then
                            lineNo = 1
                            If obj IsNot Nothing Then
                                ArrPO.Add(obj)
                            End If

                            obj = New clsPurchaseOrderHead()
                            obj.Arr = New List(Of clsPurchaseOrderDetail)
                            ''obj.PurchaseOrder_No =  ''To Be Geneated...
                            obj.Form_ID = clsCommon.myCstr(clsUserMgtCode.mbtnPurchaseOrder)
                            obj.PurchaseOrder_Date = clsCommon.GETSERVERDATE()
                            obj.Delivery_date = obj.PurchaseOrder_Date
                            obj.Vendor_Code = clsCommon.myCstr(gv1.Rows(ii).Cells("Vendor_Code").Value)
                            obj.Vendor_Name = clsCommon.myCstr(gv1.Rows(ii).Cells("Vendor_Name").Value)
                            obj.Ref_No = objReqNo.Ref_No
                            obj.Total_Tax_Amt = 0
                            obj.Remarks = objReqNo.Remarks
                            obj.Bill_To_Location = objReqNo.Location
                            'obj.Ship_To_Location = txtShipToLocation.Value
                            obj.Comments = objReqNo.Comments
                            obj.On_Hold = False
                            obj.Mode_Of_Transport = objReqNo.Mode_Of_Transport
                            obj.Description = objReqNo.Description
                            'obj.Tax_Group = txtTaxGroup.Value
                            If clsCommon.CompairString(objReqNo.Requisition_Type, "J") = CompairStringResult.Equal Then
                                obj.PurchaseOrder_Type = objReqNo.Requisition_Type
                            Else
                                obj.PurchaseOrder_Type = "L"
                            End If

                            obj.Item_Type = objReqNo.Item_Type
                            obj.Dept = objReqNo.Dept
                            obj.Dept_Desc = objReqNo.Dept_Desc

                            obj.Category = objReqNo.Category
                            obj.Capex_Code = objReqNo.Capex_Code
                            obj.Capex_SubCode = objReqNo.Capex_SubCode

                            'If (gv2.Rows.Count > 0) Then
                            '    obj.TAX1 = clsCommon.myCstr(gv2.Rows(0).Cells(colTTaxAutCode).Value)
                            '    obj.TAX1_Rate = clsCommon.myCdbl(gv2.Rows(0).Cells(colTTaxRate).Value)
                            '    obj.TAX1_Base_Amt = clsCommon.myCdbl(gv2.Rows(0).Cells(colTBaseAmt).Value)
                            '    obj.TAX1_Amt = clsCommon.myCdbl(gv2.Rows(0).Cells(colTTaxAmt).Value)
                            'End If
                            'If (gv2.Rows.Count > 1) Then
                            '    obj.TAX2 = clsCommon.myCstr(gv2.Rows(1).Cells(colTTaxAutCode).Value)
                            '    obj.TAX2_Rate = clsCommon.myCdbl(gv2.Rows(1).Cells(colTTaxRate).Value)
                            '    obj.TAX2_Base_Amt = clsCommon.myCdbl(gv2.Rows(1).Cells(colTBaseAmt).Value)
                            '    obj.TAX2_Amt = clsCommon.myCdbl(gv2.Rows(1).Cells(colTTaxAmt).Value)
                            'End If
                            'If (gv2.Rows.Count > 2) Then
                            '    obj.TAX3 = clsCommon.myCstr(gv2.Rows(2).Cells(colTTaxAutCode).Value)
                            '    obj.TAX3_Rate = clsCommon.myCdbl(gv2.Rows(2).Cells(colTTaxRate).Value)
                            '    obj.TAX3_Base_Amt = clsCommon.myCdbl(gv2.Rows(2).Cells(colTBaseAmt).Value)
                            '    obj.TAX3_Amt = clsCommon.myCdbl(gv2.Rows(2).Cells(colTTaxAmt).Value)
                            'End If
                            'If (gv2.Rows.Count > 3) Then
                            '    obj.TAX4 = clsCommon.myCstr(gv2.Rows(3).Cells(colTTaxAutCode).Value)
                            '    obj.TAX4_Rate = clsCommon.myCdbl(gv2.Rows(3).Cells(colTTaxRate).Value)
                            '    obj.TAX4_Base_Amt = clsCommon.myCdbl(gv2.Rows(3).Cells(colTBaseAmt).Value)
                            '    obj.TAX4_Amt = clsCommon.myCdbl(gv2.Rows(3).Cells(colTTaxAmt).Value)
                            'End If
                            'If (gv2.Rows.Count > 4) Then
                            '    obj.TAX5 = clsCommon.myCstr(gv2.Rows(4).Cells(colTTaxAutCode).Value)
                            '    obj.TAX5_Rate = clsCommon.myCdbl(gv2.Rows(4).Cells(colTTaxRate).Value)
                            '    obj.TAX5_Base_Amt = clsCommon.myCdbl(gv2.Rows(4).Cells(colTBaseAmt).Value)
                            '    obj.TAX5_Amt = clsCommon.myCdbl(gv2.Rows(4).Cells(colTTaxAmt).Value)
                            'End If
                            'If (gv2.Rows.Count > 5) Then
                            '    obj.TAX6 = clsCommon.myCstr(gv2.Rows(5).Cells(colTTaxAutCode).Value)
                            '    obj.TAX6_Rate = clsCommon.myCdbl(gv2.Rows(5).Cells(colTTaxRate).Value)
                            '    obj.TAX6_Base_Amt = clsCommon.myCdbl(gv2.Rows(5).Cells(colTBaseAmt).Value)
                            '    obj.TAX6_Amt = clsCommon.myCdbl(gv2.Rows(5).Cells(colTTaxAmt).Value)
                            'End If
                            'If (gv2.Rows.Count > 6) Then
                            '    obj.TAX7 = clsCommon.myCstr(gv2.Rows(6).Cells(colTTaxAutCode).Value)
                            '    obj.TAX7_Rate = clsCommon.myCdbl(gv2.Rows(6).Cells(colTTaxRate).Value)
                            '    obj.TAX7_Base_Amt = clsCommon.myCdbl(gv2.Rows(6).Cells(colTBaseAmt).Value)
                            '    obj.TAX7_Amt = clsCommon.myCdbl(gv2.Rows(6).Cells(colTTaxAmt).Value)
                            'End If
                            'If (gv2.Rows.Count > 7) Then
                            '    obj.TAX8 = clsCommon.myCstr(gv2.Rows(7).Cells(colTTaxAutCode).Value)
                            '    obj.TAX8_Rate = clsCommon.myCdbl(gv2.Rows(7).Cells(colTTaxRate).Value)
                            '    obj.TAX8_Base_Amt = clsCommon.myCdbl(gv2.Rows(7).Cells(colTBaseAmt).Value)
                            '    obj.TAX8_Amt = clsCommon.myCdbl(gv2.Rows(7).Cells(colTTaxAmt).Value)
                            'End If
                            'If (gv2.Rows.Count > 8) Then
                            '    obj.TAX9 = clsCommon.myCstr(gv2.Rows(8).Cells(colTTaxAutCode).Value)
                            '    obj.TAX9_Rate = clsCommon.myCdbl(gv2.Rows(8).Cells(colTTaxRate).Value)
                            '    obj.TAX9_Base_Amt = clsCommon.myCdbl(gv2.Rows(8).Cells(colTBaseAmt).Value)
                            '    obj.TAX9_Amt = clsCommon.myCdbl(gv2.Rows(8).Cells(colTTaxAmt).Value)
                            'End If
                            'If (gv2.Rows.Count > 9) Then
                            '    obj.TAX10 = clsCommon.myCstr(gv2.Rows(9).Cells(colTTaxAutCode).Value)
                            '    obj.TAX10_Rate = clsCommon.myCdbl(gv2.Rows(9).Cells(colTTaxRate).Value)
                            '    obj.TAX10_Base_Amt = clsCommon.myCdbl(gv2.Rows(9).Cells(colTBaseAmt).Value)
                            '    obj.TAX10_Amt = clsCommon.myCdbl(gv2.Rows(9).Cells(colTTaxAmt).Value)
                            'End If

                            ''obj.Terms_Code = txtTermCode.Value
                            ''obj.Due_Date = txtDueDate.Value
                            obj.Discount_Base = 0
                            obj.Discount_Amt = 0
                            obj.Amount_Less_Discount = 0
                            obj.PO_Total_Amt = 0
                            'obj.Abandonment_No = clsCommon.myCdbl(lblAbandonmentNo.Text)
                            obj.Against_Requisition = txtReqNo.Text

                            obj.Against_Vendor_Quotation = clsCommon.myCstr(gv1.Rows(ii).Cells("Code").Value)


                            'If (gvAC.Rows.Count > 0) Then
                            '    If clsCommon.myLen(gvAC.Rows(0).Cells(colACCode).Value) > 0 Then
                            '        obj.Add_Charge_Code1 = clsCommon.myCstr(gvAC.Rows(0).Cells(colACCode).Value)
                            '        obj.Add_Charge_Name1 = clsCommon.myCstr(gvAC.Rows(0).Cells(colACName).Value)
                            '        obj.Add_Charge_Amt1 = clsCommon.myCdbl(gvAC.Rows(0).Cells(colACAmount).Value)
                            '    End If
                            'End If
                            'If (gvAC.Rows.Count > 1) Then
                            '    If clsCommon.myLen(gvAC.Rows(1).Cells(colACCode).Value) > 0 Then
                            '        obj.Add_Charge_Code2 = clsCommon.myCstr(gvAC.Rows(1).Cells(colACCode).Value)
                            '        obj.Add_Charge_Name2 = clsCommon.myCstr(gvAC.Rows(1).Cells(colACName).Value)
                            '        obj.Add_Charge_Amt2 = clsCommon.myCdbl(gvAC.Rows(1).Cells(colACAmount).Value)
                            '    End If
                            'End If
                            'If (gvAC.Rows.Count > 2) Then
                            '    If clsCommon.myLen(gvAC.Rows(2).Cells(colACCode).Value) > 0 Then
                            '        obj.Add_Charge_Code3 = clsCommon.myCstr(gvAC.Rows(2).Cells(colACCode).Value)
                            '        obj.Add_Charge_Name3 = clsCommon.myCstr(gvAC.Rows(2).Cells(colACName).Value)
                            '        obj.Add_Charge_Amt3 = clsCommon.myCdbl(gvAC.Rows(2).Cells(colACAmount).Value)
                            '    End If
                            'End If
                            'If (gvAC.Rows.Count > 3) Then
                            '    If clsCommon.myLen(gvAC.Rows(3).Cells(colACCode).Value) > 0 Then
                            '        obj.Add_Charge_Code4 = clsCommon.myCstr(gvAC.Rows(3).Cells(colACCode).Value)
                            '        obj.Add_Charge_Name4 = clsCommon.myCstr(gvAC.Rows(3).Cells(colACName).Value)
                            '        obj.Add_Charge_Amt4 = clsCommon.myCdbl(gvAC.Rows(3).Cells(colACAmount).Value)
                            '    End If
                            'End If
                            'If (gvAC.Rows.Count > 4) Then
                            '    If clsCommon.myLen(gvAC.Rows(4).Cells(colACCode).Value) > 0 Then
                            '        obj.Add_Charge_Code5 = clsCommon.myCstr(gvAC.Rows(4).Cells(colACCode).Value)
                            '        obj.Add_Charge_Name5 = clsCommon.myCstr(gvAC.Rows(4).Cells(colACName).Value)
                            '        obj.Add_Charge_Amt5 = clsCommon.myCdbl(gvAC.Rows(4).Cells(colACAmount).Value)
                            '    End If
                            'End If
                            'If (gvAC.Rows.Count > 5) Then
                            '    If clsCommon.myLen(gvAC.Rows(5).Cells(colACCode).Value) > 0 Then
                            '        obj.Add_Charge_Code6 = clsCommon.myCstr(gvAC.Rows(5).Cells(colACCode).Value)
                            '        obj.Add_Charge_Name6 = clsCommon.myCstr(gvAC.Rows(5).Cells(colACName).Value)
                            '        obj.Add_Charge_Amt6 = clsCommon.myCdbl(gvAC.Rows(5).Cells(colACAmount).Value)
                            '    End If
                            'End If
                            'If (gvAC.Rows.Count > 6) Then
                            '    If clsCommon.myLen(gvAC.Rows(6).Cells(colACCode).Value) > 0 Then
                            '        obj.Add_Charge_Code7 = clsCommon.myCstr(gvAC.Rows(6).Cells(colACCode).Value)
                            '        obj.Add_Charge_Name7 = clsCommon.myCstr(gvAC.Rows(6).Cells(colACName).Value)
                            '        obj.Add_Charge_Amt7 = clsCommon.myCdbl(gvAC.Rows(6).Cells(colACAmount).Value)
                            '    End If
                            'End If
                            'If (gvAC.Rows.Count > 7) Then
                            '    If clsCommon.myLen(gvAC.Rows(7).Cells(colACCode).Value) > 0 Then
                            '        obj.Add_Charge_Code8 = clsCommon.myCstr(gvAC.Rows(7).Cells(colACCode).Value)
                            '        obj.Add_Charge_Name8 = clsCommon.myCstr(gvAC.Rows(7).Cells(colACName).Value)
                            '        obj.Add_Charge_Amt8 = clsCommon.myCdbl(gvAC.Rows(7).Cells(colACAmount).Value)
                            '    End If
                            'End If
                            'If (gvAC.Rows.Count > 8) Then
                            '    If clsCommon.myLen(gvAC.Rows(8).Cells(colACCode).Value) > 0 Then
                            '        obj.Add_Charge_Code9 = clsCommon.myCstr(gvAC.Rows(8).Cells(colACCode).Value)
                            '        obj.Add_Charge_Name9 = clsCommon.myCstr(gvAC.Rows(8).Cells(colACName).Value)
                            '        obj.Add_Charge_Amt9 = clsCommon.myCdbl(gvAC.Rows(8).Cells(colACAmount).Value)
                            '    End If
                            'End If
                            'If (gvAC.Rows.Count > 9) Then
                            '    If clsCommon.myLen(gvAC.Rows(9).Cells(colACCode).Value) > 0 Then
                            '        obj.Add_Charge_Code10 = clsCommon.myCstr(gvAC.Rows(9).Cells(colACCode).Value)
                            '        obj.Add_Charge_Name10 = clsCommon.myCstr(gvAC.Rows(9).Cells(colACName).Value)
                            '        obj.Add_Charge_Amt10 = clsCommon.myCdbl(gvAC.Rows(9).Cells(colACAmount).Value)
                            '    End If
                            'End If
                            'obj.Total_Add_Charge = clsCommon.myCdbl(lblAddCharges.Text)

                        End If
                        strLastQuotationNo = clsCommon.myCstr(gv1.Rows(ii).Cells("Code").Value)
                        Dim objTr As New clsPurchaseOrderDetail()
                        objTr.Line_No = lineNo
                        lineNo += 1
                        objTr.Row_Type = clsCommon.myCstr(gv1.Rows(ii).Cells("Row_Type").Value)
                        objTr.Item_Code = clsCommon.myCstr(gv1.Rows(ii).Cells("Item_Code").Value)
                        objTr.Item_Desc = clsCommon.myCstr(gv1.Rows(ii).Cells("Item_Desc").Value)
                        objTr.PurchaseOrder_Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells("Qty").Value)
                        objTr.Balance_Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells("Qty").Value)
                        objTr.Unit_code = clsCommon.myCstr(gv1.Rows(ii).Cells("Unit_Code").Value)
                        objTr.Requisition_Id = txtReqNo.Text
                        'objTr.Location = clsCommon.myCstr(grow.Cells(colloc).Value)
                        objTr.Item_Cost = clsCommon.myCdbl(gv1.Rows(ii).Cells("Item_Cost").Value)
                        objTr.Amount = clsCommon.myCdbl(gv1.Rows(ii).Cells("Item_Net_Amt").Value)
                        objTr.Disc_Per = 0
                        objTr.Disc_Amt = 0
                        objTr.Amt_Less_Discount = clsCommon.myCdbl(gv1.Rows(ii).Cells("Item_Net_Amt").Value)


                        obj.Discount_Base += clsCommon.myCdbl(gv1.Rows(ii).Cells("Item_Net_Amt").Value)
                        obj.Discount_Amt = 0
                        obj.Amount_Less_Discount += clsCommon.myCdbl(gv1.Rows(ii).Cells("Item_Net_Amt").Value)
                        obj.PO_Total_Amt += clsCommon.myCdbl(gv1.Rows(ii).Cells("Item_Net_Amt").Value)

                        'objTr.TAX1 = clsCommon.myCstr(grow.Cells(colTax1).Value)
                        'objTr.TAX1_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt1).Value)
                        'objTr.TAX1_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate1).Value)
                        'objTr.TAX1_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt1).Value)
                        'objTr.TAX2 = clsCommon.myCstr(grow.Cells(colTax2).Value)
                        'objTr.TAX2_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt2).Value)
                        'objTr.TAX2_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate2).Value)
                        'objTr.TAX2_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt2).Value)
                        'objTr.TAX3 = clsCommon.myCstr(grow.Cells(colTax3).Value)
                        'objTr.TAX3_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt3).Value)
                        'objTr.TAX3_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate3).Value)
                        'objTr.TAX3_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt3).Value)
                        'objTr.TAX4 = clsCommon.myCstr(grow.Cells(colTax4).Value)
                        'objTr.TAX4_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt4).Value)
                        'objTr.TAX4_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate4).Value)
                        'objTr.TAX4_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt4).Value)
                        'objTr.TAX5 = clsCommon.myCstr(grow.Cells(colTax5).Value)
                        'objTr.TAX5_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt5).Value)
                        'objTr.TAX5_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate5).Value)
                        'objTr.TAX5_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt5).Value)
                        'objTr.TAX6 = clsCommon.myCstr(grow.Cells(colTax6).Value)
                        'objTr.TAX6_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt6).Value)
                        'objTr.TAX6_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate6).Value)
                        'objTr.TAX6_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt6).Value)
                        'objTr.TAX7 = clsCommon.myCstr(grow.Cells(colTax7).Value)
                        'objTr.TAX7_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt7).Value)
                        'objTr.TAX7_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate7).Value)
                        'objTr.TAX7_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt7).Value)
                        'objTr.TAX8 = clsCommon.myCstr(grow.Cells(colTax8).Value)
                        'objTr.TAX8_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt8).Value)
                        'objTr.TAX8_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate8).Value)
                        'objTr.TAX8_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt8).Value)
                        'objTr.TAX9 = clsCommon.myCstr(grow.Cells(colTax9).Value)
                        'objTr.TAX9_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt9).Value)
                        'objTr.TAX9_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate9).Value)
                        'objTr.TAX9_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt9).Value)
                        'objTr.TAX10 = clsCommon.myCstr(grow.Cells(colTax10).Value)
                        'objTr.TAX10_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt10).Value)
                        'objTr.TAX10_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate10).Value)
                        'objTr.TAX10_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt10).Value)
                        'objTr.Total_Tax_Amt = clsCommon.myCdbl(grow.Cells(colTotTaxAmt).Value)
                        objTr.Item_Net_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells("Item_Net_Amt").Value)
                        'objTr.Specification = clsCommon.myCdbl(gv1.Rows(ii).Cells("Item_Net_Amt").Value)
                        'objTr.Remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value)
                        objTr.Location = objReqNo.Location ''txtBillToLocation.Value 'clsCommon.myCstr(grow.Cells(colLocationCode).Value)
                        'objTr.MRP =objReqNo.mr clsCommon.myCdbl(grow.Cells(colMRP).Value)
                        ''objTr.Assessable = clsCommon.myCdbl(grow.Cells(colAssessableRate).Value)
                        ''objTr.AssessableAmt = clsCommon.myCdbl(grow.Cells(colAssessableAmount).Value)
                        If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                            obj.Arr.Add(objTr)
                        End If
                    End If
                Next
                If obj IsNot Nothing Then
                    ArrPO.Add(obj)
                End If

                Dim arrCreatedPO As List(Of String) = clsPurchaseOrderHead.CreateMultiplePOs(ArrPO)
                Dim strMsg As String = "Successfully Purchase Order Created.Following PO is Created"
                For Each Str As String In arrCreatedPO
                    strMsg += Environment.NewLine + " " + Str
                Next
                clsCommon.MyMessageBoxShow(strMsg, Me.Text)
                LoadData()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub SplitContainer1_Panel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles SplitContainer1.Panel2.Paint

    End Sub
End Class
