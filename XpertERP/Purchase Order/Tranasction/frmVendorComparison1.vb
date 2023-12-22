Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Public Class FrmVendorComparison1
    Inherits FrmMainTranScreen
#Region "Variables"
    Private isCellValueChangedOpen As Boolean = False
    Private isNewEntry As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
#End Region

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.VendorComparison)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isPostFlag
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
            qry += " where TSPL_VENDOR_QUOTATION_HEAD.Status=1 and TSPL_VENDOR_QUOTATION_HEAD.RFQ_NO='" + txtRFQNo.Value + "'"
            qry += " ORDER BY Code,Line_No"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt.Rows.Count <= 0 Then
                gv1.DataSource = Nothing
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
                Exit Sub
            End If

            btnSave.Enabled = True
            UsLock1.Status = GetStatus(txtRFQNo.Value)
            If UsLock1.Status = ERPTransactionStatus.Approved Then
                btnSave.Enabled = False
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
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FrmVendorComparison1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        'Add Tool tip Task No- TEC/22/05/18-000245
        If e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnSave, "TSPL_VENDOR_QUOTATION_DETAIL (selected Quotation for PO Creation)")
        End If
        'Add Tool tip Task No- TEC/22/05/18-000245
    End Sub
  
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
        btnSave.Enabled = True
    End Sub

    Private Sub gv1_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.ViewCellFormatting
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub

   

    Public Shared Function GetStatus(ByVal strCode As String) As ERPTransactionStatus
        Dim qry As String = "select 1 from TSPL_PURCHASE_ORDER_HEAD where Against_Vendor_Quotation in (select Code from TSPL_VENDOR_QUOTATION_HEAD where RFQ_NO ='" + strCode + "')"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Return ERPTransactionStatus.Approved
        End If
        Return ERPTransactionStatus.Pending
    End Function


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If clsCommon.myLen(txtRFQNo.Value) <= 0 Then
                txtRFQNo.Focus()
                Throw New Exception("Please select RFQ No")
            End If

            If GetStatus(txtRFQNo.Value) = ERPTransactionStatus.Approved Then
                Throw New Exception("Posted Transaction")
            End If


            Dim arrQuotation As New List(Of String)
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myCBool(gv1.Rows(ii).Cells("Sel").Value) AndAlso Not arrQuotation.Contains(clsCommon.myCstr(gv1.Rows(ii).Cells("Code").Value)) Then
                    arrQuotation.Add(clsCommon.myCstr(gv1.Rows(ii).Cells("Code").Value))
                End If
            Next
            If arrQuotation.Count = 0 Then
                Throw New Exception("No Quotation found to Post")
            End If

            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Dim qry As String = "update TSPL_VENDOR_QUOTATION_DETAIL set Is_Select_For_PO=0 where Code in (select Code from TSPL_VENDOR_QUOTATION_HEAD where RFQ_NO='" + txtRFQNo.Value + "')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Try
                For Each strQuotNo As String In arrQuotation
                    qry = ""
                    For ii As Integer = 0 To gv1.Rows.Count - 1
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells("Code").Value), strQuotNo) = CompairStringResult.Equal AndAlso clsCommon.myCBool(gv1.Rows(ii).Cells("Sel").Value) Then
                            If clsCommon.myLen(qry) > 0 Then
                                qry += ","
                            End If
                            qry += clsCommon.myCstr(gv1.Rows(ii).Cells("Line_No").Value)
                        End If
                    Next
                    If clsCommon.myLen(qry) > 0 Then
                        qry = "update TSPL_VENDOR_QUOTATION_DETAIL set Is_Select_For_PO=1 where Code='" + strQuotNo + "' and Line_No in(" + qry + ")"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If
                Next
                trans.Commit()
                RadMessageBox.Show("Data Saved Successfully", Me.Text)
                LoadData()
            Catch ex As Exception
                trans.Rollback()
                RadMessageBox.Show(ex.Message, Me.Text)
            End Try

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            If clsCommon.myLen(txtRFQNo) <= 0 Then
                Throw New Exception("Document number not found")
            End If
            Dim frmCRV As New frmCrystalReportViewer()
            Dim qry As String = "  select Final.*,TSPL_VENDOR_MASTER.Vendor_Name as LowwestAmountVendorName,  TBL_Final_COUNT.Item_Count as Item_Code_Count, Final.Comments as Taxes,TBL_Final_COUNT. Comments_Lowest_Price From ( " & _
                                "  select CAST(Is_Select_For_PO as bit) as sel, TSPL_VENDOR_QUOTATION_HEAD.Code,TSPL_VENDOR_QUOTATION_HEAD.VQDate,TSPL_VENDOR_QUOTATION_HEAD.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_VENDOR_QUOTATION_DETAIL.Line_No,TSPL_VENDOR_QUOTATION_DETAIL.Row_Type,TSPL_VENDOR_QUOTATION_DETAIL.Item_Code,TSPL_VENDOR_QUOTATION_DETAIL.Item_Desc, TSPL_VENDOR_QUOTATION_DETAIL.Qty,TSPL_VENDOR_QUOTATION_DETAIL.Unit_Code,TSPL_VENDOR_QUOTATION_DETAIL.Item_Cost,TSPL_VENDOR_QUOTATION_DETAIL.Item_Net_Amt,TSPL_VENDOR_QUOTATION_DETAIL.Specification,TSPL_VENDOR_QUOTATION_DETAIL.Remarks, TSPL_VENDOR_QUOTATION_HEAD.Total_Amt, " & _
                                " (Select ABCD.Vendor_Code From (SELECT top 1 TSPL_VENDOR_QUOTATION_HEAD_For_Low_Price.Vendor_Code,TSPL_VENDOR_QUOTATION_HEAD_For_Low_Price.Total_Amt,DENSE_RANK() OVER (ORDER BY TSPL_VENDOR_QUOTATION_HEAD_For_Low_Price.Total_Amt asc) AS LowestAmount FROM TSPL_VENDOR_QUOTATION_HEAD as TSPL_VENDOR_QUOTATION_HEAD_For_Low_Price where TSPL_VENDOR_QUOTATION_HEAD_For_Low_Price.RFQ_NO =  TSPL_VENDOR_QUOTATION_HEAD.RFQ_NO) as ABCD) as TBL_LowwestAmountVendor, TSPL_VENDOR_MASTER.Terms_Code , TSPL_VENDOR_QUOTATION_HEAD.Description as Terms_Desc,TSPL_VENDOR_MASTER.Payment_Code, TSPL_VENDOR_QUOTATION_HEAD.Remarks as Payment_Desc,TSPL_VENDOR_QUOTATION_HEAD.Comments, TSPL_REQUISITION_HEAD.Dept_Desc,TSPL_VENDOR_QUOTATION_HEAD.Requisition_Id ,TSPL_REQUISITION_HEAD.Comments as REQUISITION_Comments" & _
                                " from TSPL_VENDOR_QUOTATION_DETAIL " & _
                                " left outer join TSPL_VENDOR_QUOTATION_HEAD on TSPL_VENDOR_QUOTATION_HEAD.Code=TSPL_VENDOR_QUOTATION_DETAIL.Code  " & _
                                " left Outer Join TSPL_REQUISITION_HEAD on TSPL_REQUISITION_HEAD.Requisition_Id = TSPL_VENDOR_QUOTATION_HEAD.Requisition_Id " & _
                                " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VENDOR_QUOTATION_HEAD.Vendor_Code  " & _
                                " Left Outer Join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code =  TSPL_VENDOR_MASTER.Terms_Code  " & _
                                " Left Outer Join TSPL_PAYMENT_CODE on TSPL_PAYMENT_CODE.Payment_Code = TSPL_VENDOR_MASTER.Payment_Code  " & _
                                " where TSPL_VENDOR_QUOTATION_HEAD.Status=1 and TSPL_VENDOR_QUOTATION_HEAD.RFQ_NO='" + txtRFQNo.Value + "' " & _
                                " )Final  Left Outer Join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = Final.TBL_LowwestAmountVendor  " & _
                                " left Outer Join (Select Vendor_Code as Vendor_Code, count(Item_Code) as Item_Count,max(Comments_Lowest_Price) as Comments_Lowest_Price From (Select  (TSPL_VENDOR_QUOTATION_HEAD.Vendor_Code) as Vendor_Code,Item_Code as Item_Code,max(TSPL_VENDOR_QUOTATION_HEAD.Comments) as Comments_Lowest_Price from TSPL_VENDOR_QUOTATION_DETAIL left outer join TSPL_VENDOR_QUOTATION_HEAD on TSPL_VENDOR_QUOTATION_HEAD.Vendor_Code=TSPL_VENDOR_QUOTATION_HEAD.Vendor_Code where TSPL_VENDOR_QUOTATION_HEAD.Status=1 and TSPL_VENDOR_QUOTATION_HEAD.RFQ_NO='" + txtRFQNo.Value + "' Group by Vendor_Code,Item_Code)   Tbl_Count group by Vendor_Code ) TBL_Final_COUNT on TBL_Final_COUNT.Vendor_Code = Final.TBL_LowwestAmountVendor  " & _
                                " ORDER BY Final.Code,Final.Line_No  "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Record Found", Me.Text)
            Else
                frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "rptVendorComparison", "COMPARATIVE STATEMENT FOR PURCHASE", clsCommon.myCDate(dt.Rows(0)("VQDate")))
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
End Class
