'-changes By--[Pankaj Kumar Chaudhary]--Against Ticket No -[BM00000002083]
Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System.IO
Imports System.Net


Public Class frmPendingDeliveryDairySale
#Region "Variables"
    Public Sampling As Integer = 0
    Public DOItemType As String = Nothing
    Public Trans_Type As String = Nothing
    Dim IsInsideLoadData As Boolean = False
    Public VendorCode As String = Nothing
    Public LocationCode As String = Nothing
    Public LocationName As String = Nothing
    Public VendorName As String = Nothing
    Public formDate As Date? = Nothing
    Public strCurrCode As String = Nothing
    Public ArrReturn As List(Of clsDeliveryNoteDairySaleDetail) = Nothing
    Dim dtAllData As DataTable = Nothing
    Dim dtHeadTable As DataTable = Nothing
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
    Const colDPriceCode As String = "colDPriceCode"
    Const colDPriceDate As String = "colDPriceDate"
    Const colDConvF As String = "colDConvF"

    Const colHPONo As String = "colHPONo"
    Const colHPODate As String = "colHPODate"
    Const colHSalesmanCode As String = "colHSalesmanCode"

    Const colHSelect As String = "SELECT"
    Const colHCode As String = "CODE"
    Const colHDate As String = "DATE"
    Const colHVendorCode As String = "VENDOR"
    Const colHVendorName As String = "VENDORNAME"
    Const colSchemCode As String = "colSchemCode"
    Const ReportIDHead As String = "PSShipmentDOGridH"
    Const ReportIDDT As String = "PSShipmentDOGridD"
    Dim EnableCustomerPODetailonDairyBooking As Integer = 0
#End Region

    Private Sub frmPendingDelivery_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        tp_ToDate.Value = clsCommon.GetPrintDate(formDate, "dd/MM/yyyy")
        tp_FromDate.Value = clsCommon.GetPrintDate(formDate, "dd/MM/yyyy")
        LoadFormData()
    End Sub
    Sub LoadFormData()
        If clsCommon.myLen(tp_FromDate.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "From Date Can't be Blank. ", Me.Text)
            tp_FromDate.Focus()
            Return
        End If
        If clsCommon.myLen(tp_ToDate.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "To Date Can't be Blank.", Me.Text)
            tp_ToDate.Focus()
            Return
        End If
        If tp_FromDate.Value > tp_ToDate.Value Then
            common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater then To Date", Me.Text)
            tp_FromDate.Focus()
            Return
        End If

        setGridPropery()

        '-------richa 30/07/2014 Ticket No. BM00000003242---------
        'Dim strwherecls As String = ""
        'Dim StrCondition As String = ""
        'Dim StrCondition1 As String = ""
        'strwherecls = Xtra.CustomerPermission()
        'If clsCommon.myLen(strwherecls) > 0 Then
        '    StrCondition = "   AND Final.Vendor IN (" + strwherecls + ")"
        '    StrCondition1 = "   WHERE Final.Vendor IN (" + strwherecls + ")"
        'Else
        '    StrCondition = ""
        '    StrCondition1 = ""
        'End If
        '-------------------------------------
        Dim CreateCommonDairyDispatchforFreshAmbient As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateCommonDairyDispatchforFreshAmbient, clsFixedParameterCode.CreateCommonDairyDispatchforFreshAmbient, Nothing))
        EnableCustomerPODetailonDairyBooking = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnableCustomerPODetailonDairyBooking, clsFixedParameterCode.EnableCustomerPODetailonDairyBooking, Nothing))
        Dim ShowMulMRPOfSameItemOnDairyBookingCustomer As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowMulMRPOfSameItemOnDairyBookingCustomer, clsFixedParameterCode.ShowMulMRPOfSameItemOnDairyBookingCustomer, Nothing)) = 1, True, False)
        Dim qry As String = "select CAST(0 as bit) as Sel,code,ICode,max(IName) as IName "
        ',(Scheme_Code) as 'Scheme Code' 
        qry += ",Unit,max(Location) as Location,MAX(TSPL_LOCATION_MASTER.Location_Desc) as LocationName,max(TransDate) as TransDate , " & _
        "SUM(Qty* case when RI=1 then 1 else 0 end) as DeliverQty, SUM(Qty* case when RI=-1 then 1 else 0 end) as DispatchQty, SUM(Unapproved) as UnapprovedQty, " & _
        "SUM((Qty *RI)- Unapproved) as PedningQty ,MAX(Rate) as Rate,MAX(Vendor) as Vendor,MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as VendorName, " & _
        "MAX(MRP) as MRP,max(final.Price_Code) as Price_Code,max(price_date) as price_date,max(Conv_Factor) as Conv_Factor,max(Cust_PO_No) as Cust_PO_No,max(cust_po_date) as cust_po_date,max(SalesmanCode) as SalesmanCode  from ( " + Environment.NewLine
        qry += " select TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Line_No,isnull(TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Scheme_Code,'') as Scheme_Code,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Document_No as Code,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code as Vendor, " &
        "TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Item_Code as ICode,TSPL_ITEM_MASTER.Item_Desc as IName, TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Qty  as Qty, " &
        "0 as Unapproved,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Unit_Code as Unit,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Location_Code as Location, " &
        "1 as RI,(case when isnull(TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Disc_Scheme_Type,'')='A' then TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.OrgRate-Disc_Scheme_Amount" &
         " else case when TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Sampling=0 then  TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.OrgRate else 0 end  end) as Rate,1 as Chk,Document_Date as TransDate," &
        "TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Price_code,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Price_Date,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Conv_Factor,TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.MRP,isnull(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.CustPO_No,'') as Cust_PO_No,ISNULL(CONVERT(VARCHAR,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.custpo_date,103),'') as cust_po_date,isnull(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.SalesmanCode,'') as SalesmanCode from TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE " &
        "left outer join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No=TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Document_No   " &
        "left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Item_Code " &
        "where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Sampling=" & Sampling & " and TSPL_DELIVERY_NOTE_master_FRESHSALE.Posted=1  and TSPL_DELIVERY_NOTE_master_FRESHSALE.Short_Close='N'  and TSPL_DELIVERY_NOTE_master_FRESHSALE.OnHold='N' and isnull(TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Scheme_Item,'N')<>'Y' and isnull(TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.FOC_Item,'0')<>'1'  " + Environment.NewLine &
         " and convert(date,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_Date,103) > =convert(date,'" + tp_FromDate.Value + "',103) and convert(date,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_Date,103) <= convert(date,'" + tp_ToDate.Value + "',103) " + Environment.NewLine

        If CreateCommonDairyDispatchforFreshAmbient = 0 Then
            qry += " and TSPL_DELIVERY_NOTE_master_FRESHSALE.TRANSACTION_TYPE='" + Trans_Type + "'"
        End If
        If clsCommon.myLen(VendorCode) > 0 Then
            qry += " and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code='" + VendorCode + "'"
        End If
        If clsCommon.myLen(LocationCode) > 0 Then
            qry += " and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Location_Code='" + LocationCode + "'"
        End If
        If clsCommon.myLen(DOItemType) > 0 Then
            If clsCommon.CompairString(DOItemType, "T") = CompairStringResult.Equal Then
                qry += " and TSPL_ITEM_MASTER.IsTaxable =1 "
            ElseIf clsCommon.CompairString(DOItemType, "NT") = CompairStringResult.Equal Then
                qry += " and TSPL_ITEM_MASTER.IsTaxable <> 1  "
                'ElseIf clsCommon.CompairString(cmbGPItemType.SelectedValue, "E") = CompairStringResult.Equal Then
                '    qry += " and TSPL_ITEM_MASTER.Is_Tax_Exempted =2"
            End If
        End If
        'qry += " and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.TRANSACTION_TYPE ='" + Trans_Type + "'"

        qry += " union all " + Environment.NewLine & _
        " select TSPL_SD_SHIPMENT_DETAIL.Line_No,TSPL_SD_SHIPMENT_DETAIL.Scheme_Code,TSPL_SD_SHIPMENT_DETAIL.Delivery_Code as Code,TSPL_SD_SHIPMENT_Head.Customer_Code as Vendor,TSPL_SD_SHIPMENT_DETAIL.Item_Code as ICode,'' as IName,isnull(TSPL_SD_SHIPMENT_DETAIL.Qty,0) as Qty,0 as Unapproved,TSPL_SD_SHIPMENT_DETAIL.Unit_code as Unit,'' as Location,-1 as RI," & IIf(ShowMulMRPOfSameItemOnDairyBookingCustomer = True, " TSPL_SD_SHIPMENT_DETAIL.Item_Cost ", "0") & " as Rate,0 as Chk,null as TransDate, " + Environment.NewLine & _
        " TSPL_SD_SHIPMENT_DETAIL.Price_code,TSPL_SD_SHIPMENT_DETAIL.Price_Date,TSPL_SD_SHIPMENT_DETAIL.Conv_Factor,TSPL_SD_SHIPMENT_DETAIL.MRP " + Environment.NewLine & _
        " ,isnull(TSPL_SD_SHIPMENT_Head.Cust_PO_No,'') as Cust_PO_No,ISNULL(CONVERT(VARCHAR,TSPL_SD_SHIPMENT_Head.cust_po_date,103),'') as cust_po_date,isnull(TSPL_SD_SHIPMENT_Head.Salesman_Code,'') as SalesmanCode " + Environment.NewLine & _
        " from TSPL_SD_SHIPMENT_DETAIL " + Environment.NewLine & _
        " left outer join TSPL_SD_SHIPMENT_Head on TSPL_SD_SHIPMENT_Head.Document_Code=TSPL_SD_SHIPMENT_DETAIL.Document_Code" + Environment.NewLine & _
        " left outer join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No=TSPL_SD_SHIPMENT_DETAIL.Delivery_Code " + Environment.NewLine & _
        " where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Sampling=" & Sampling & " and TSPL_SD_SHIPMENT_Head.Status=1 and Scheme_Item='N' " + Environment.NewLine & _
        " and len(isnull(TSPL_SD_SHIPMENT_DETAIL.Delivery_Code,''))>0   " + Environment.NewLine & _
        " and convert(date,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_Date,103) > =convert(date,'" + tp_FromDate.Value + "',103) and convert(date,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_Date,103) <= convert(date,'" + tp_ToDate.Value + "',103) " + Environment.NewLine

        If CreateCommonDairyDispatchforFreshAmbient = 0 Then
            qry += " and TSPL_SD_SHIPMENT_Head.Trans_Type='" + Trans_Type + "'"
        End If

        If clsCommon.myLen(DOItemType) > 0 Then
            qry += " and TSPL_SD_SHIPMENT_Head.DO_Item_Type = '" + clsCommon.myCstr(DOItemType) + "'"
        End If

        qry += " union all   " + Environment.NewLine & _
        " select TSPL_SD_SHIPMENT_DETAIL.Line_No,TSPL_SD_SHIPMENT_DETAIL.Scheme_Code,TSPL_SD_SHIPMENT_DETAIL.Delivery_Code as Code,TSPL_SD_SHIPMENT_Head.Customer_Code as Vendor,TSPL_SD_SHIPMENT_DETAIL.Item_Code as ICode,'' as IName,0 as Qty,isnull(TSPL_SD_SHIPMENT_DETAIL.Qty,0) as Unapproved,TSPL_SD_SHIPMENT_DETAIL.Unit_code as Unit,'' as Location,-1 as RI," & IIf(ShowMulMRPOfSameItemOnDairyBookingCustomer = True, " TSPL_SD_SHIPMENT_DETAIL.Item_Cost ", "0") & " as Rate,0 as Chk,null as TransDate, " + Environment.NewLine & _
         " TSPL_SD_SHIPMENT_DETAIL.Price_code,TSPL_SD_SHIPMENT_DETAIL.Price_Date,TSPL_SD_SHIPMENT_DETAIL.Conv_Factor,TSPL_SD_SHIPMENT_DETAIL.MRP " & _
       " ,isnull(TSPL_SD_SHIPMENT_Head.Cust_PO_No,'') as Cust_PO_No,ISNULL(CONVERT(VARCHAR,TSPL_SD_SHIPMENT_Head.cust_po_date,103),'') as cust_po_date,isnull(TSPL_SD_SHIPMENT_Head.Salesman_Code,'') as SalesmanCode " & _
        " from TSPL_SD_SHIPMENT_DETAIL " + Environment.NewLine & _
        " left outer join TSPL_SD_SHIPMENT_Head on TSPL_SD_SHIPMENT_Head.Document_Code=TSPL_SD_SHIPMENT_DETAIL.Document_Code" + Environment.NewLine & _
         " left outer join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No=TSPL_SD_SHIPMENT_DETAIL.Delivery_Code " + Environment.NewLine & _
         " where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Sampling=" & Sampling & " and TSPL_SD_SHIPMENT_Head.Status=0 and Scheme_Item='N' and len(isnull(TSPL_SD_SHIPMENT_DETAIL.Delivery_Code,''))>0 and TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE not in ('" + strCurrCode + "')  " + Environment.NewLine & _
        " and convert(date,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_Date,103) > =convert(date,'" + tp_FromDate.Value + "',103) and convert(date,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_Date,103) <= convert(date,'" + tp_ToDate.Value + "',103) " + Environment.NewLine

        If CreateCommonDairyDispatchforFreshAmbient = 0 Then
            qry += " and TSPL_SD_SHIPMENT_Head.Trans_Type='" + Trans_Type + "'"
        End If

        If clsCommon.myLen(DOItemType) > 0 Then
            qry += " and TSPL_SD_SHIPMENT_Head.DO_Item_Type = '" + clsCommon.myCstr(DOItemType) + "'"
        End If

        qry += " )Final " + Environment.NewLine & _
        " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=final.Vendor " + Environment.NewLine & _
     " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=Final.Location " + Environment.NewLine

        'If IsAllowSingleSI4SingleSO = True Then
        '    qry += " WHERE Code Not in (Select Distinct Order_Code from TSPL_SD_SHIPMENT_DETAIL WHERE ISNULL(Order_Code,'')<>'') " + StrCondition + " "
        'Else
        '    qry += "" + StrCondition1 + "  "
        'End If
        'Scheme_Code,
        qry += " group by Code,ICode,Unit " & IIf(ShowMulMRPOfSameItemOnDairyBookingCustomer = True, " ,Rate", "") & " having SUM(Chk)>0 and (SUM(Qty *RI)- SUM(Unapproved)) <>0 "

        Dim strHeadQuery = "select Code,TransDate,Location,Vendor,VendorName,Cust_PO_No,cust_po_date,SalesmanCode from ( " & qry & " ) aa group by Code,Location,Vendor,VendorName,TransDate,Cust_PO_No,cust_po_date,SalesmanCode order by Code"
        dtHeadTable = clsDBFuncationality.GetDataTable(strHeadQuery)

        qry += "order by Code,max(Line_No)"
        dtAllData = clsDBFuncationality.GetDataTable(qry)
        If dtAllData Is Nothing OrElse dtAllData.Rows.Count <= 0 Then
            If clsCommon.myLen(VendorName) > 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No record found for Customer " + VendorName + "", Me.Text)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No record found.", Me.Text)
            End If
            'Me.Close()
        End If
        LoadHeadData()
        LoadBlankGridDetail()
        If EnableCustomerPODetailonDairyBooking = 1 Then
            gvHead.Columns(colHPONo).IsVisible = True
            gvHead.Columns(colHPODate).IsVisible = True
            gvHead.Columns(colHSalesmanCode).IsVisible = True
        Else
            gvHead.Columns(colHPONo).IsVisible = False
            gvHead.Columns(colHPODate).IsVisible = False
            gvHead.Columns(colHSalesmanCode).IsVisible = False
        End If
    End Sub
    Sub LoadHeadData()
        IsInsideLoadData = True
        LoadBlankHeadGrid()
        Dim arr As New List(Of String)
        For Each dr As DataRow In dtHeadTable.Rows
            Dim strCode As String = clsCommon.myCstr(dr("code"))
            'If Not arr.Contains(strCode) Then
            '    arr.Add(strCode)
            gvHead.Rows.AddNew()
            gvHead.Rows(gvHead.RowCount - 1).Cells(colHSelect).Value = False
            gvHead.Rows(gvHead.RowCount - 1).Cells(colHCode).Value = strCode
            gvHead.Rows(gvHead.RowCount - 1).Cells(colHDate).Value = clsCommon.myCstr(dr("TransDate"))
            gvHead.Rows(gvHead.RowCount - 1).Cells(colHVendorCode).Value = clsCommon.myCstr(dr("Vendor"))
            gvHead.Rows(gvHead.RowCount - 1).Cells(colHVendorName).Value = clsCommon.myCstr(dr("VendorName"))

            gvHead.Rows(gvHead.RowCount - 1).Cells(colHPONo).Value = clsCommon.myCstr(dr("Cust_PO_No"))
            gvHead.Rows(gvHead.RowCount - 1).Cells(colHPODate).Value = clsCommon.myCstr(dr("cust_po_date"))
            gvHead.Rows(gvHead.RowCount - 1).Cells(colHSalesmanCode).Value = clsCommon.myCstr(dr("SalesmanCode"))
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
        repoCode.HeaderText = "Delivery No"
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

        'sanjay
        Dim repoPONo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPONo.FormatString = ""
        repoPONo.HeaderText = "PO No"
        repoPONo.Name = colHPONo
        repoPONo.Width = 100
        repoPONo.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoPONo)

        Dim repoPODate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPODate.FormatString = ""
        repoPODate.HeaderText = "PO Date"
        repoPODate.Name = colHPODate
        repoPODate.Width = 100
        repoPODate.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoPODate)

        Dim repoSalesman As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSalesman.FormatString = ""
        repoSalesman.HeaderText = "Salesman Code"
        repoSalesman.Name = colHSalesmanCode
        repoSalesman.Width = 100
        repoSalesman.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoSalesman)

        'sanjay

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

        ReStoreGridLayoutHead()
    End Sub

    Private Sub ReStoreGridLayoutHead()
        Try
            If clsCommon.myLen(ReportIDHead) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportIDHead, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvHead.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvHead.Columns.Count - 1 Step ii + 1
                        gvHead.Columns(ii).IsVisible = False
                        gvHead.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gvHead.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
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
        repoCode.HeaderText = "Delivery No"
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
        repoIName.HeaderText = "Item"
        repoIName.Name = colDIName
        repoIName.Width = 180
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)

        'Dim repoScheme As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'repoScheme.FormatString = ""
        'repoScheme.HeaderText = "Scheme Code"
        'repoScheme.Name = colSchemCode
        'repoScheme.Width = 180
        'repoScheme.ReadOnly = True
        'repoScheme.IsVisible = True
        'gv1.MasterTemplate.Columns.Add(repoScheme)


        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "Unit"
        repoUnit.Name = colDUnit
        repoUnit.Width = 60
        repoUnit.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoUnit)


        Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "Rate"
        repoRate.Name = colDRate
        repoRate.ReadOnly = True
        repoRate.IsVisible = True
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRate)

        Dim repoOrderQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoOrderQty.FormatString = ""
        repoOrderQty.HeaderText = "Deliver Qty"
        repoOrderQty.Name = colDOrderQty
        repoOrderQty.ReadOnly = True
        repoOrderQty.Width = 80
        repoOrderQty.WrapText = True
        repoOrderQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoOrderQty)

        Dim repoAppQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAppQty.FormatString = ""
        repoAppQty.HeaderText = "Used Qty"
        repoAppQty.Name = colDApprovedQty
        repoAppQty.ReadOnly = True
        repoAppQty.Width = 100
        repoAppQty.WrapText = True
        repoAppQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAppQty)

        Dim repoUnAppQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoUnAppQty.FormatString = ""
        repoUnAppQty.HeaderText = "Unapproved Qty"
        repoUnAppQty.Name = colDUnApprovedQty
        repoUnAppQty.ReadOnly = True
        repoUnAppQty.Width = 80
        repoUnAppQty.WrapText = True
        repoUnAppQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoUnAppQty)

        Dim repoPendingQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoPendingQty = New GridViewDecimalColumn()
        repoPendingQty.FormatString = ""
        repoPendingQty.HeaderText = "Pending"
        repoPendingQty.Name = colDPendingQty
        repoPendingQty.ReadOnly = True
        repoPendingQty.Width = 80
        repoPendingQty.WrapText = True
        repoPendingQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoPendingQty)


        Dim repoMRP As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMRP.FormatString = ""
        repoMRP.HeaderText = "MRP"
        repoMRP.Name = colDMRP
        repoMRP.Width = 80
        repoMRP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoMRP.ReadOnly = False
        repoMRP.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoMRP)


        Dim repoConv As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoConv = New GridViewDecimalColumn()
        repoConv.FormatString = ""
        repoConv.HeaderText = "Conv. Factor"
        repoConv.Name = colDConvF
        repoConv.Width = 80
        repoConv.Minimum = 0
        repoConv.ReadOnly = False
        repoConv.IsVisible = False
        repoConv.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoConv)

        Dim repoPriceCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPriceCode.FormatString = ""
        repoPriceCode.HeaderText = "Price Code"
        repoPriceCode.Name = colDPriceCode
        repoPriceCode.IsVisible = True
        repoPriceCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoPriceCode)


        Dim repoPriceDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoPriceDate.Format = DateTimePickerFormat.Custom
        repoPriceDate.CustomFormat = "dd-MM-yyyy"
        repoPriceDate.HeaderText = "Price Date"
        repoPriceDate.WrapText = True
        repoPriceDate.FormatString = "{0:d}"
        repoPriceDate.Name = colDPriceDate
        repoPriceDate.ReadOnly = True
        repoPriceDate.Width = 80
        gv1.MasterTemplate.Columns.Add(repoPriceDate)



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

        ReStoreGridLayoutDT()
    End Sub

    Private Sub ReStoreGridLayoutDT()
        Try
            If clsCommon.myLen(ReportIDDT) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportIDDT, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
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
        Me.Close()
    End Sub

    Sub btnOKPressed()

        'sanjay
        If EnableCustomerPODetailonDairyBooking = 1 Then
            Dim PONo As String = ""
            Dim PODate As String = ""
            Dim SalesmanCode As String = ""
            Dim arrPONo As New List(Of String)
            Dim arrPODate As New List(Of String)
            Dim arrSalesmanCode As New List(Of String)
            For ii As Integer = 0 To gvHead.RowCount - 1
                If (clsCommon.myCBool(gvHead.Rows(ii).Cells(colHSelect).Value)) Then
                    PONo = clsCommon.myCstr(gvHead.Rows(ii).Cells(colHPONo).Value)
                    PODate = clsCommon.myCstr(gvHead.Rows(ii).Cells(colHPODate).Value)
                    SalesmanCode = clsCommon.myCstr(gvHead.Rows(ii).Cells(colHSalesmanCode).Value)

                    If Not arrPONo.Contains(PONo) Then
                        arrPONo.Add(PONo)
                    End If

                    If Not arrPODate.Contains(PODate) Then
                        arrPODate.Add(PODate)
                    End If

                    If Not arrSalesmanCode.Contains(SalesmanCode) Then
                        arrSalesmanCode.Add(SalesmanCode)
                    End If
                End If
            Next

            If arrPONo.Count > 1 Then
                common.clsCommon.MyMessageBoxShow(Me, "PO Number should be the same", Me.Text)
                Exit Sub
            End If
            If arrPODate.Count > 1 Then
                common.clsCommon.MyMessageBoxShow(Me, "PO Date should be the same", Me.Text)
                Exit Sub
            End If
            If arrSalesmanCode.Count > 1 Then
                common.clsCommon.MyMessageBoxShow(Me, "Salesman should be the same", Me.Text)
                Exit Sub
            End If
        End If
        'sanjay


        Dim dblBalQty As Double = 0
        ArrReturn = New List(Of clsDeliveryNoteDairySaleDetail)
        Dim obj As clsDeliveryNoteDairySaleDetail = Nothing
        For ii As Integer = 0 To gv1.RowCount - 1
            If (clsCommon.myCBool(gv1.Rows(ii).Cells(colDSelect).Value)) Then
                obj = New clsDeliveryNoteDairySaleDetail()
                obj.Document_No = clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)
                obj.Item_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDICode).Value)
                obj.Item_Desc = clsCommon.myCstr(gv1.Rows(ii).Cells(colDIName).Value)
                obj.Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDRate).Value)
                obj.Unit_code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDUnit).Value)

                obj.Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDOrderQty).Value)
                obj.Balance_Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDPendingQty).Value)
                obj.MRP = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDMRP).Value)
                obj.Conv_Factor = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDConvF).Value)
                obj.Price_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDPriceCode).Value)
                obj.Price_Date = clsCommon.myCDate(gv1.Rows(ii).Cells(colDPriceDate).Value)
                'obj.Scheme_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colSchemCode).Value)
                obj.Amount = obj.Qty * obj.Rate
                If (obj.Balance_Qty > 0) Then
                    ArrReturn.Add(obj)
                End If
            End If
        Next

        If ArrReturn.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select at least one non zero Pending Delivery item", Me.Text)
        Else
            Me.Close()
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

    Dim IsAllowSingleSI4SingleSO As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData("AllowSingleInvoiceAgainstSingleOrder", "AllowSingleInvoiceAgainstSingleOrder", Nothing)) = 1, True, False)
    Private Sub gvHead_ValueChanging(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.ValueChangingEventArgs) Handles gvHead.ValueChanging
        If Not IsInsideLoadData Then
            If Not IsAllowSingleSI4SingleSO Then
                If gvHead.CurrentColumn Is gvHead.Columns(colHSelect) Then
                    ''richa MIL/17/04/19-000063
                    Dim strvendorcount As Boolean = False
                    For Each grow As GridViewRowInfo In gvHead.Rows
                        If grow.Cells(colHSelect).Value = True Then
                            strvendorcount = True
                        End If
                    Next
                    If strvendorcount = False Then
                        VendorCode = ""
                    End If
                    Dim strVendorCode As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHVendorCode).Value)
                    Dim strVendorName As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHVendorName).Value)
                    If clsCommon.myLen(VendorCode) <= 0 Then
                        VendorCode = strVendorCode
                        VendorName = strVendorName
                    End If
                  
                    If clsCommon.CompairString(strVendorCode, VendorCode) = CompairStringResult.Equal Then
                        Dim strCode As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHCode).Value)
                        If clsCommon.myLen(strCode) > 0 Then
                            LoadDetailData(e.NewValue, strCode)
                        End If
                    Else
                        common.clsCommon.MyMessageBoxShow(Me, "Order's Customer should be `" + VendorName, Me.Text)
                        e.Cancel = True
                    End If

                End If
            Else
                gv1.Rows.Clear()
                Dim strCode1 As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHCode).Value)
                If e.NewValue = True Then
                    For Each grow As GridViewRowInfo In gvHead.Rows
                        grow.Cells(colHSelect).Value = False
                    Next
                    LoadDetailData(e.NewValue, strCode1)
                End If
            End If

        End If
    End Sub

    Sub LoadDetailData(ByVal NewVal As Boolean, ByVal strCode As String)
        If NewVal Then
            For Each dr As DataRow In dtAllData.Rows
                If clsCommon.CompairString(strCode, clsCommon.myCstr(dr("Code"))) = CompairStringResult.Equal Then
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDSelect).Value = True
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDCode).Value = clsCommon.myCstr(dr("code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDICode).Value = clsCommon.myCstr(dr("ICode"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDIName).Value = clsCommon.myCstr(dr("IName"))
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemCode).Value = clsCommon.myCstr(dr("Scheme Code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDUnit).Value = clsCommon.myCstr(dr("Unit"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDRate).Value = clsCommon.myCdbl(dr("Rate"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDOrderQty).Value = clsCommon.myCdbl(dr("DeliverQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDApprovedQty).Value = clsCommon.myCdbl(dr("DispatchQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDUnApprovedQty).Value = clsCommon.myCdbl(dr("UnapprovedQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDPendingQty).Value = clsCommon.myCdbl(dr("PedningQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDMRP).Value = clsCommon.myCdbl(dr("MRP"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDConvF).Value = clsCommon.myCdbl(dr("Conv_Factor"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDPriceCode).Value = clsCommon.myCstr(dr("Price_code"))
                    If dr("Price_Date") IsNot DBNull.Value Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDPriceDate).Value = clsCommon.myCDate(dr("Price_Date"))
                    End If


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

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
    End Sub

    Private Sub RadMenuSaveLayout_Click(sender As Object, e As EventArgs) Handles RadMenuSaveLayout.Click
        If clsCommon.myLen(ReportIDHead) > 0 Then
            gvHead.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportIDHead
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gvHead.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gvHead.ColumnCount
            obj.SaveData()
            'If obj.SaveData() Then
            '    common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            'End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
        If clsCommon.myLen(ReportIDDT) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportIDDT
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub RadMenuRadMenuDeleteLayout_Click(sender As Object, e As EventArgs) Handles RadMenuRadMenuDeleteLayout.Click
        clsGridLayout.DeleteData(ReportIDHead, objCommonVar.CurrentUserCode)
        clsGridLayout.DeleteData(ReportIDDT, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout delete successfully", "Information", Me.Text)
    End Sub

    'Private Sub RadMenuItemSaveLayoutDetail_Click(sender As Object, e As EventArgs)
    '    If clsCommon.myLen(ReportIDDT) > 0 Then
    '        gv1.MasterTemplate.FilterDescriptors.Clear()
    '        Dim obj As New clsGridLayout()
    '        obj.ReportID = ReportIDDT
    '        obj.UserID = objCommonVar.CurrentUserCode
    '        obj.GridLayout = New MemoryStream()
    '        gv1.SaveLayout(obj.GridLayout)
    '        obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
    '        obj.GridColumns = gv1.ColumnCount
    '        If obj.SaveData() Then
    '            common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
    '        End If
    '        obj.GridLayout.Close()
    '        obj.GridLayout.Dispose()
    '    End If
    'End Sub

    'Private Sub RadMenuItemDeleteLayoutDetail_Click(sender As Object, e As EventArgs)
    '    clsGridLayout.DeleteData(ReportIDDT, objCommonVar.CurrentUserCode)
    'End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadFormData()
    End Sub
End Class

