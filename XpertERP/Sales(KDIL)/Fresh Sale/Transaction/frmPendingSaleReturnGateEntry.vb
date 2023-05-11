Imports common
Public Class frmPendingSaleReturnGateEntry
#Region "Variables"
    Dim IsInsideLoadData As Boolean = False
    Public VendorCode As String = Nothing
    Public VendorName As String = Nothing
    Public strCurrCode As String = Nothing
    Public strDocType As String = Nothing
    Public strExport_Merchant As String = Nothing
    Public strDate As Date
    Public LocationCode As String = Nothing

    Public ArrReturn As List(Of clsSalesReturnFreshSaleDetail) = Nothing
    Public ArrMisSaleReturn As List(Of ClsScrapSaleDetail) = Nothing
    Public ArrBulkSaleReturn As List(Of ClsInvoiceDetailBulkSale) = Nothing
    Dim dtAllData As DataTable = Nothing
    Public DocType As String = Nothing
    Const colDSelect As String = "SELECT"
    Const colDCode As String = "CODE"
    Const colDICode As String = "ICODE"
    Const colDIName As String = "INAME"
    Const colDIType As String = "IType"
    Const colDTaxGroup As String = "TAXGROUP"
    Const colDTaxGroupName As String = "TAXGROUPNAME"
    Const colDUnit As String = "UNIT"
    Const colDRate As String = "RATE"
    Const colDOrderQty As String = "ORDERQTY"
    Const colDApprovedQty As String = "APPROVEDQTY"
    Const colDUnApprovedQty As String = "UNAPPROVEDQTY"
    Const colDPendingQty As String = "PENDINGQTY"
    Const colDTaxRate1 As String = "TaxRate1"
    Const colDTaxRate2 As String = "TaxRate2"
    Const colDTaxRate3 As String = "TaxRate3"
    Const colDTaxRate4 As String = "TaxRate4"
    Const colDTaxRate5 As String = "TaxRate5"
    Const colDTaxRate6 As String = "TaxRate6"
    Const colDTaxRate7 As String = "TaxRate7"
    Const colDTaxRate8 As String = "TaxRate8"
    Const colDTaxRate9 As String = "TaxRate9"
    Const colDTaxRate10 As String = "TaxRate10"
    Const colDMRP As String = "MRP"
    Const colDAssessable As String = "ASSESSABLE"
    Const colDDisPer As String = "DISCOUNTPER"
    Const colDBatchNo As String = "COLDBATCHNO"
    Const colDMfgDate As String = "COLDMFGDATE"
    Const colDExpDate As String = "COLDEXPDATE"


    Const colHSelect As String = "SELECT"
    Const colHCode As String = "CODE"
    Const colHDate As String = "DATE"
    Const colHVendorCode As String = "VENDOR"
    Const colHVendorName As String = "VENDORNAME"
    Const colSchemCode As String = "colSchemCode"

#End Region

    Private Sub FrmPendingRequistion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtFromDate.Text = clsCommon.GETSERVERDATE().AddMonths(-1)
        txtToDate.Value = clsCommon.GETSERVERDATE()
        Dim strFromDate As String = clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy")
        Dim strToDate As String = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")
        LoadData(strFromDate, strToDate, "Y")
    End Sub


    Sub LoadData(ByVal strFromDate As String, ByVal strToDate As String, ByVal IsDetailON As String)
        setGridPropery()

        Dim strwherecls As String = ""
        Dim StrCondition As String = ""

        If clsCommon.CompairString(DocType, "FS") = CompairStringResult.Equal Then
            If clsCommon.myLen(VendorCode) > 0 Then
                StrCondition = "   and Customer_Code IN (" + VendorCode + ")"
            Else
                StrCondition = ""
            End If
        Else
            If clsCommon.myLen(strwherecls) > 0 Then
                StrCondition = "   WHERE cust_Code IN (" + strwherecls + ")"
            Else
                StrCondition = ""
            End If
        End If
       
        Dim qry As String = ""

        '-------------------------------------preeti [KDI/31/05/18-000339]
        If clsCommon.CompairString(DocType, "FS") = CompairStringResult.Equal OrElse clsCommon.CompairString(DocType, "PS") = CompairStringResult.Equal OrElse clsCommon.CompairString(DocType, "EXPS") = CompairStringResult.Equal OrElse clsCommon.CompairString(DocType, "MCCS") = CompairStringResult.Equal Then
            qry = " SELECT TSPL_ITEM_MASTER.Item_Desc ,TSPL_SD_SALE_INVOICE_HEAD.DOCUMENT_CODE,TSPL_CUSTOMER_MASTER.Customer_Name  ,convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Document_Date ,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code ,TSPL_SD_SALE_INVOICE_DETAIL.Line_No ,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code ,TSPL_SD_SALE_INVOICE_DETAIL.Qty ,TSPL_SD_SALE_INVOICE_DETAIL.Unit_code  " & _
                  " FROM TSPL_SD_SALE_INVOICE_DETAIL" & _
                 " left join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE " & _
                " left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.cust_code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code " & _
                  " left join TSPL_ITEM_MASTER on  TSPL_ITEM_MASTER.Item_Code =TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " & _
                  " WHERE  convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) > =convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= convert(date,'" + txtToDate.Value + "',103) " & _
                  " and not EXISTS " & _
                  "(SELECT Invoice_No FROM TSPL_Sale_Return_Gate_Entry_Invoice_Wise left join TSPL_Sale_Return_Gate_Entry_Head on TSPL_Sale_Return_Gate_Entry_Head.Gate_Entry_No=TSPL_Sale_Return_Gate_Entry_Invoice_Wise.Gate_Entry_No  WHERE TSPL_Sale_Return_Gate_Entry_Invoice_Wise.Invoice_No=TSPL_SD_SALE_INVOICE_HEAD.DOCUMENT_CODE and TSPL_Sale_Return_Gate_Entry_Head.isCancel =0)"
            If clsCommon.CompairString(DocType, "FS") = CompairStringResult.Equal Then
                qry += " and  TSPL_SD_SALE_INVOICE_HEAD.TRANS_TYPE ='FS'"
            End If
            If clsCommon.CompairString(DocType, "PS") = CompairStringResult.Equal Then
                qry += " and  TSPL_SD_SALE_INVOICE_HEAD.TRANS_TYPE ='PS'"
            End If
            If clsCommon.CompairString(DocType, "EXPS") = CompairStringResult.Equal Then
                qry += " and  TSPL_SD_SALE_INVOICE_HEAD.TRANS_TYPE ='EXP'"
            End If
            If clsCommon.CompairString(DocType, "MCCS") = CompairStringResult.Equal Then
                qry += " and  TSPL_SD_SALE_INVOICE_HEAD.TRANS_TYPE ='MCC'"
            End If
            If clsCommon.myLen(VendorCode) > 0 Then
                qry += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code='" + VendorCode + "'"
            End If
            ' Ticket No : KDI/11/05/18-000309 By Prabhakar
            If clsCommon.myLen(LocationCode) > 0 Then
                qry += " and TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location='" + LocationCode + "'"
            End If
        End If
        If clsCommon.CompairString(DocType, "MISS") = CompairStringResult.Equal Then
            qry = " SELECT TSPL_ITEM_MASTER.Item_Desc ,TSPL_SCRAPSALE_DETAIL.shipment_no as DOCUMENT_CODE,TSPL_CUSTOMER_MASTER.Customer_Name  ," & _
                         " convert(varchar,TSPL_SCRAPSALE_HEAD.shipment_Date ,103) as Document_Date ,TSPL_SCRAPSALE_HEAD.cust_Code as Customer_Code ,TSPL_SCRAPSALE_DETAIL.Line_No ," & _
                         " TSPL_SCRAPSALE_DETAIL.Item_Code ,TSPL_SCRAPSALE_DETAIL.shipped_Qty as Qty ,TSPL_SCRAPSALE_DETAIL.Unit_code   " & _
                         " FROM TSPL_SCRAPSALE_DETAIL" & _
                           " left join TSPL_SCRAPSALE_HEAD on TSPL_SCRAPSALE_HEAD.shipment_no =TSPL_SCRAPSALE_DETAIL.shipment_no  " & _
                             " left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.cust_code=TSPL_SCRAPSALE_HEAD.cust_Code  " & _
                              "left join TSPL_ITEM_MASTER on  TSPL_ITEM_MASTER.Item_Code =TSPL_SCRAPSALE_DETAIL.Item_Code  " & _
                            " WHERE  convert(date,TSPL_SCRAPSALE_HEAD.shipment_Date,103) > =convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_SCRAPSALE_HEAD.shipment_Date,103) <= convert(date,'" + txtToDate.Value + "',103)  and  Not EXISTS" & _
                           " (SELECT Invoice_No FROM TSPL_Sale_Return_Gate_Entry_Invoice_Wise left join TSPL_Sale_Return_Gate_Entry_Head on TSPL_Sale_Return_Gate_Entry_Head.Gate_Entry_No=TSPL_Sale_Return_Gate_Entry_Invoice_Wise.Gate_Entry_No WHERE TSPL_Sale_Return_Gate_Entry_Invoice_Wise.Invoice_No=TSPL_SCRAPSALE_HEAD.shipment_no and TSPL_Sale_Return_Gate_Entry_Head.isCancel =0) "
            If clsCommon.myLen(VendorCode) > 0 Then
                qry += " and TSPL_SCRAPSALE_HEAD.cust_Code ='" + VendorCode + "'"
            End If
            ' Ticket No : KDI/11/05/18-000309 By Prabhakar
            If clsCommon.myLen(LocationCode) > 0 Then
                qry += " and TSPL_SCRAPSALE_HEAD.Loc_Code ='" + LocationCode + "'"
            End If
        End If

        If clsCommon.CompairString(DocType, "BS") = CompairStringResult.Equal Then
            qry = " SELECT TSPL_ITEM_MASTER.Item_Desc ,TSPL_INVOICE_DETAIL_BulKSALE.Document_No as DOCUMENT_CODE ,TSPL_CUSTOMER_MASTER.Customer_Name  ," & _
                " convert(varchar,TSPL_INVOICE_MASTER_BULKSAlE.Document_Date  ,103) as Document_Date ,TSPL_INVOICE_MASTER_BULKSAlE.Customer_Code   ,0 as Line_No ," & _
                " TSPL_INVOICE_DETAIL_BulKSALE.Item_Code ,TSPL_INVOICE_DETAIL_BulKSALE.InvoiceQty  as Qty ,TSPL_INVOICE_DETAIL_BulKSALE.Unit_code   " & _
                  "FROM TSPL_INVOICE_DETAIL_BulKSALE" & _
                    " left join TSPL_INVOICE_MASTER_BULKSAlE on TSPL_INVOICE_MASTER_BULKSAlE.document_no =TSPL_INVOICE_DETAIL_BulKSALE.document_no  " & _
                  " left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.cust_code=TSPL_INVOICE_MASTER_BULKSAlE.Customer_Code   " & _
                  " left join TSPL_ITEM_MASTER on  TSPL_ITEM_MASTER.Item_Code =TSPL_INVOICE_DETAIL_BulKSALE.Item_Code  " & _
                   " WHERE  convert(date,TSPL_INVOICE_MASTER_BULKSAlE.Document_Date,103) > =convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_INVOICE_MASTER_BULKSAlE.Document_Date,103) <= convert(date,'" + txtToDate.Value + "',103) " & _
           " and Not EXISTS" & _
            " (SELECT Invoice_No FROM TSPL_Sale_Return_Gate_Entry_Invoice_Wise left join TSPL_Sale_Return_Gate_Entry_Head on TSPL_Sale_Return_Gate_Entry_Head.Gate_Entry_No=TSPL_Sale_Return_Gate_Entry_Invoice_Wise.Gate_Entry_No WHERE TSPL_Sale_Return_Gate_Entry_Invoice_Wise.Invoice_No=TSPL_INVOICE_MASTER_BULKSAlE.document_no and TSPL_Sale_Return_Gate_Entry_Head.isCancel =0)"


        End If
        If clsCommon.CompairString(DocType, "CSATRAN") = CompairStringResult.Equal Then
            qry = "SELECT TSPL_ITEM_MASTER.Item_Desc ,TSPL_CSA_TRANSFER_HEAD.DOC_CODE AS DOCUMENT_CODE,TSPL_CUSTOMER_MASTER.Customer_Name,convert(varchar,TSPL_CSA_TRANSFER_HEAD.Transfer_Date ,103) as Document_Date ," & _
            " TSPL_CSA_TRANSFER_HEAD.Cust_Code AS Customer_Code ,TSPL_CSA_TRANSFER_DETAIL.Line_No ,TSPL_CSA_TRANSFER_DETAIL.Item_Code ,TSPL_CSA_TRANSFER_DETAIL.Qty ,TSPL_CSA_TRANSFER_DETAIL.Unit_code " & _
            " FROM TSPL_CSA_TRANSFER_DETAIL left join TSPL_CSA_TRANSFER_HEAD on TSPL_CSA_TRANSFER_HEAD.DOC_CODE  =TSPL_CSA_TRANSFER_DETAIL.DOC_CODE  " & _
            " left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.cust_code=TSPL_CSA_TRANSFER_HEAD.Cust_Code   left join TSPL_ITEM_MASTER on  TSPL_ITEM_MASTER.Item_Code =TSPL_CSA_TRANSFER_DETAIL.Item_Code  " & _
            " WHERE  convert(date,TSPL_CSA_TRANSFER_HEAD.Transfer_Date ,103) > =convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_CSA_TRANSFER_HEAD.Transfer_Date ,103) <= convert(date,'" + txtToDate.Value + "',103)  " & _
            "  and not EXISTS (SELECT Invoice_No FROM TSPL_Sale_Return_Gate_Entry_Invoice_Wise left join TSPL_Sale_Return_Gate_Entry_Head on TSPL_Sale_Return_Gate_Entry_Head.Gate_Entry_No=TSPL_Sale_Return_Gate_Entry_Invoice_Wise.Gate_Entry_No WHERE TSPL_Sale_Return_Gate_Entry_Invoice_Wise.Invoice_No=TSPL_CSA_TRANSFER_HEAD.DOC_CODE and TSPL_Sale_Return_Gate_Entry_Head.isCancel =0 )"
            If clsCommon.myLen(VendorCode) > 0 Then
                qry += "   AND TSPL_CSA_TRANSFER_HEAD.cust_Code IN ('" + VendorCode + "')"
            End If
            If clsCommon.myLen(LocationCode) > 0 Then
                qry += "   AND TSPL_CSA_TRANSFER_HEAD.From_Location_Code IN ('" + LocationCode + "')"
            End If
        End If

        dtAllData = clsDBFuncationality.GetDataTable(qry)
        If dtAllData Is Nothing OrElse dtAllData.Rows.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("No item found for Customer " + VendorName + "")
            '  Me.Close()
        End If
        LoadHeadData()
        If clsCommon.CompairString(IsDetailON, "N") = CompairStringResult.Equal Then
            gv1.Rows.Clear()
            'gv1.Columns.Clear()
        Else
            LoadBlankGridDetail()
        End If

    End Sub
    Sub LoadHeadData()
        IsInsideLoadData = True
        LoadBlankHeadGrid()
        Dim arr As New List(Of String)
        For Each dr As DataRow In dtAllData.Rows
            Dim strCode As String = clsCommon.myCstr(dr("DOCUMENT_CODE"))
            If Not arr.Contains(strCode) Then
                arr.Add(strCode)
                gvHead.Rows.AddNew()
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHSelect).Value = False
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHCode).Value = strCode
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHDate).Value = clsCommon.myCstr(dr("Document_Date"))
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHVendorCode).Value = clsCommon.myCstr(dr("Customer_Code"))
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHVendorName).Value = clsCommon.myCstr(dr("Customer_Name"))
            End If
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
        repoCode.HeaderText = "Invoice No"
        repoCode.Name = colHCode
        repoCode.Width = 170
        repoCode.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoCode)

        Dim repoDate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDate.FormatString = ""
        repoDate.HeaderText = "Date"
        repoDate.Name = colHDate
        repoDate.Width = 70
        repoDate.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoDate)

        Dim repoVendor As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVendor.FormatString = ""
        repoVendor.HeaderText = "Customer"
        repoVendor.Name = colHVendorCode
        repoVendor.Width = 170
        repoVendor.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoVendor)

        Dim repoVendorName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVendorName.FormatString = ""
        repoVendorName.HeaderText = "Customer Name"
        repoVendorName.Name = colHVendorName
        repoVendorName.Width = 170
        repoVendorName.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoVendorName)

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

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.HeaderText = " "
        repoSelect.Name = colDSelect
        repoSelect.ReadOnly = False
        repoSelect.Width = 25
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoSelect)


        Dim repoCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCode.FormatString = ""
        repoCode.HeaderText = "Invoice No"
        repoCode.Name = colDCode
        repoCode.Width = 180
        repoCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCode)

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colDICode
        repoICode.Width = 100
        repoICode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Name"
        repoIName.Name = colDIName
        repoIName.Width = 180
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)

        

        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "Unit"
        repoUnit.Name = colDUnit
        repoUnit.Width = 60
        repoUnit.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoUnit)



        Dim repoOrderQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoOrderQty.FormatString = ""
        repoOrderQty.HeaderText = "Qty"
        repoOrderQty.Name = colDOrderQty
        repoOrderQty.ReadOnly = True
        repoOrderQty.Width = 80
        repoOrderQty.WrapText = True
        repoOrderQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoOrderQty)


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

    Sub setGridPropery()
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = True
        gv1.EnableSorting = False
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.MasterTemplate.ShowColumnHeaders = True
        ''gv1.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill
        gv1.EnableAlternatingRowColor = True
        gv1.EnableFiltering = True
        gv1.ShowFilteringRow = True
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        btnCancelPressed()
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        btnOKPressed()
    End Sub

    Sub btnCancelPressed()
        clsERPFuncationality.closeForm(Me)
    End Sub
    Sub OkpressedFS()
        ArrReturn = New List(Of clsSalesReturnFreshSaleDetail)
        Dim obj As clsSalesReturnFreshSaleDetail = Nothing
        For ii As Integer = 0 To gv1.RowCount - 1
            If (clsCommon.myCBool(gv1.Rows(ii).Cells(colDSelect).Value)) Then
                obj = New clsSalesReturnFreshSaleDetail()
                obj.Document_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)
                obj.Item_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDICode).Value)
                obj.Item_Desc = clsCommon.myCstr(gv1.Rows(ii).Cells(colDIName).Value)
                obj.Unit_code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDUnit).Value)
                obj.Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDOrderQty).Value)

                If (obj.Qty > 0) Then
                    ArrReturn.Add(obj)
                End If
            End If
        Next

        If ArrReturn.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one non zero Pending Sale item")
        Else
            clsERPFuncationality.closeForm(Me)
        End If
    End Sub
    Sub OkpressedMisSale()
        ArrMisSaleReturn = New List(Of ClsScrapSaleDetail)
        Dim obj As ClsScrapSaleDetail = Nothing
        For ii As Integer = 0 To gv1.RowCount - 1
            If (clsCommon.myCBool(gv1.Rows(ii).Cells(colDSelect).Value)) Then
                obj = New ClsScrapSaleDetail()
                obj.shipment_No = clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)
                obj.Item_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDICode).Value)
                obj.Item_Desc = clsCommon.myCstr(gv1.Rows(ii).Cells(colDIName).Value)
                obj.Unit_code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDUnit).Value)
                obj.shipped_Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDOrderQty).Value)

                If (obj.shipped_Qty > 0) Then
                    ArrMisSaleReturn.Add(obj)
                End If
            End If
        Next

        If ArrMisSaleReturn.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one non zero Pending Sale item")
        Else
            clsERPFuncationality.closeForm(Me)
        End If
    End Sub
    Sub OkpressedBulkSale()
        ArrBulkSaleReturn = New List(Of ClsInvoiceDetailBulkSale)
        Dim obj As ClsInvoiceDetailBulkSale = Nothing
        For ii As Integer = 0 To gv1.RowCount - 1
            If (clsCommon.myCBool(gv1.Rows(ii).Cells(colDSelect).Value)) Then
                obj = New ClsInvoiceDetailBulkSale()
                obj.Document_No = clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)
                obj.Item_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDICode).Value)
                'obj.Item_des = clsCommon.myCstr(gv1.Rows(ii).Cells(colDIName).Value)
                obj.Unit_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDUnit).Value)
                obj.InvoiceQty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDOrderQty).Value)

                If (obj.InvoiceQty > 0) Then
                    ArrBulkSaleReturn.Add(obj)
                End If
            End If
        Next

        If ArrBulkSaleReturn.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one non zero Pending Sale item")
        Else
            clsERPFuncationality.closeForm(Me)
        End If
    End Sub
    Sub btnOKPressed()
        If clsCommon.CompairString(DocType, "FS") = CompairStringResult.Equal OrElse clsCommon.CompairString(DocType, "PS") = CompairStringResult.Equal OrElse clsCommon.CompairString(DocType, "EXPS") = CompairStringResult.Equal OrElse clsCommon.CompairString(DocType, "MCCS") = CompairStringResult.Equal OrElse clsCommon.CompairString(DocType, "CSATRAN") = CompairStringResult.Equal Then
            okpressedFS()
        ElseIf clsCommon.CompairString(DocType, "MISS") = CompairStringResult.Equal Then
            OkpressedMisSale()
        ElseIf clsCommon.CompairString(DocType, "BS") = CompairStringResult.Equal Then
            OkpressedBulkSale()
        End If
        

    End Sub

    Private Sub FrmPendingRequistion_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnOKPressed()
        ElseIf e.KeyCode = Keys.Escape Then
            btnCancelPressed()
        End If
    End Sub

    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
        If gv1.CurrentColumn Is gv1.Columns(colDSelect) Then
            Dim strPONO As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colDCode).Value)
            Dim SelectStatus As Boolean = clsCommon.myCBool(gv1.CurrentRow.Cells(colDSelect).Value)
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.CompairString(strPONO, clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)) = CompairStringResult.Equal Then
                    gv1.Rows(ii).Cells(colDSelect).Value = Not SelectStatus
                End If
            Next
        End If
    End Sub

    Private Sub gvHead_ValueChanging(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.ValueChangingEventArgs) Handles gvHead.ValueChanging
         If gvHead.CurrentRow.Index >= 0 AndAlso Not IsInsideLoadData Then
            Dim strCode As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHCode).Value)
            If clsCommon.myLen(strCode) > 0 Then
                LoadDetailData(e.NewValue, strCode)
            End If
        End If
    End Sub

    Sub LoadDetailData(ByVal NewVal As Boolean, ByVal strCode As String)
        If NewVal Then
            For Each dr As DataRow In dtAllData.Rows
                If clsCommon.CompairString(strCode, clsCommon.myCstr(dr("DOCUMENT_CODE"))) = CompairStringResult.Equal Then
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDSelect).Value = True
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDCode).Value = clsCommon.myCstr(dr("DOCUMENT_CODE"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDICode).Value = clsCommon.myCstr(dr("Item_Code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDIName).Value = clsCommon.myCstr(dr("Item_Desc"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDUnit).Value = clsCommon.myCstr(dr("Unit_code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDOrderQty).Value = clsCommon.myCdbl(dr("Qty"))

                End If
            Next
        Else
            For ii As Integer = gv1.Rows.Count - 1 To 0 Step -1
                If clsCommon.CompairString(strCode, clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)) = CompairStringResult.Equal Then
                    gv1.Rows.RemoveAt(ii)
                End If
            Next
        End If
    End Sub

    Private Sub BtnGo_Click(sender As Object, e As EventArgs) Handles BtnGo.Click

        Dim strFromDate As String = clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy")
        Dim strToDate As String = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")
        LoadData(strFromDate, strToDate, "N")
    End Sub
End Class

