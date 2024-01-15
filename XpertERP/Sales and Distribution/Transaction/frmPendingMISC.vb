Imports common
Public Class frmPendingMISC

#Region "Variables"
    Public CustomerCode As String = Nothing
    Public CustomerName As String = Nothing
    Public strCurrCode As String = Nothing
    Public PurchaseOrder_Type As String = Nothing
    Public ArrReturn As List(Of ClsScrapSaleDetail) = Nothing
    ''Public objGRNHead As clsGRNHead = Nothing
    Public strIsCashSale As String = Nothing


    Dim dtAllData As DataTable = Nothing
    Dim IsInsideLoadData As Boolean = False

    Const colDSelect As String = "SELECT"
    Const colDCode As String = "CODE"
    Const colDPOID As String = "colDPOID"
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
    Const colDLeakQty As String = "LEAKQTY"
    Const colDBurstQty As String = "BURSTQTY"
    Const colDShortQty As String = "SHORTQTY"
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
    Const colDBatchNo As String = "BATCHNO"
    Const colDManDate As String = "MANFACTURERDATE"
    Const colDExpiryDate As String = "EXPIRYDATE"
    Const colDDisPer As String = "DISCOUNTPER"
    Const colHSelect As String = "SELECT"
    Const colHCode As String = "CODE"
    Const colHDate As String = "DATE"
    Const colHVendorCode As String = "VENDOR"
    Const colHVendorName As String = "VENDORNAME"
    Const colHPOType As String = "POTYPE"
    Const colISCASHSALE As String = "colISCASHSALE"
    Const colTaxGroup As String = "colTaxGroup"
    Const colLocation As String = "colLocation"
    Const colCategoryType As String = "COLCATEGORYTYPE"
    Const colEmergency As String = "COLEMERGENCY"
    Const colCapexSubCode As String = "COLCAPEXSUBCODE"
    Const colCapexCode As String = "COLCAPEXCODE"
#End Region

    Private Sub FrmPendingRequistion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        If clsCommon.myLen(CustomerName) > 0 Then
            Me.Text = Me.Text + " For " + CustomerName
        End If
       

        Dim qry As String = "select * from (select CAST(0 as bit) as Sel,code,ICode,max(IName) as IName ,max(Unit)as Unit,max(Location) as Location,MAX (TSPL_LOCATION_MASTER.Location_Desc) as LocationName ,SUM(Qty* case when RI=1 then 1 else 0 end) as ShipmentQty ,SUM(Qty* case when RI=-1 then 1 else 0 end) as ReturnQty ,SUM(Unapproved) as UnapprovedQty ,SUM((Qty *RI)- Unapproved) as PendingQty ,MAX(Rate) as Rate,max(TransDate) as TransDate,max(final.Customer) as Customer,MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as CustomerName,MAX(Specification) as Specification,MAX(Is_CashSale) as Is_CashSale,max(final.Tax_Group) as Tax_Group from  ( " + Environment.NewLine & _
        " select TSPL_SCRAPSALE_DETAIL.shipment_No as Code,TSPL_SCRAPSALE_head.cust_Code as Customer,TSPL_SCRAPSALE_DETAIL.Item_Code as ICode,TSPL_SCRAPSALE_DETAIL.Item_Desc as IName,TSPL_SCRAPSALE_DETAIL.shipped_Qty as Qty,0 as Unapproved,TSPL_SCRAPSALE_DETAIL.Unit_Code as Unit,TSPL_SCRAPSALE_HEAD.Loc_Code as Location,1 as RI,TSPL_SCRAPSALE_DETAIL.ItemAmt as Rate,1 as Chk,TSPL_SCRAPSALE_head.shipment_Date as TransDate,TSPL_SCRAPSALE_DETAIL.Specification,isnull(TSPL_SCRAPSALE_HEAD.Is_CashSale,'N') as Is_CashSale,TSPL_SCRAPSALE_head.Tax_Group as Tax_Group from TSPL_SCRAPSALE_DETAIL left outer join TSPL_SCRAPSALE_HEAD on TSPL_SCRAPSALE_HEAD.shipment_No=TSPL_SCRAPSALE_DETAIL.shipment_No where  TSPL_SCRAPSALE_head.ispost=1  "
        If clsCommon.myLen(CustomerCode) > 0 Then
            qry += "and TSPL_SCRAPSALE_head.cust_Code='" + CustomerCode + "'" + Environment.NewLine
        End If
      
        qry += " union all" + Environment.NewLine & _
        " select TSPL_SCRAPSALE_detail_Return.Shipment_No as Code,TSPL_SCRAPSALE_head_Return.cust_code as Customer,TSPL_SCRAPSALE_detail_Return.Item_Code as ICode,TSPL_SCRAPSALE_detail_Return.Item_Desc as IName,TSPL_SCRAPSALE_detail_Return.shipped_Qty as Qty,0 as Unapproved,'' as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,null as TransDate ,TSPL_SCRAPSALE_detail_Return.Specification,isnull(TSPL_SCRAPSALE_head_Return.Is_CashSale,'N') as Is_CashSale,TSPL_SCRAPSALE_head_Return.Tax_Group as Tax_Group from TSPL_SCRAPSALE_detail_Return left outer join TSPL_SCRAPSALE_head_Return on TSPL_SCRAPSALE_head_Return.Document_No=TSPL_SCRAPSALE_detail_Return.Document_No where TSPL_SCRAPSALE_head_Return.ispost=1 and len(isnull(TSPL_SCRAPSALE_detail_Return.Shipment_No,''))>0    "

        '" select TSPL_SCRAPSALE_detail_Return.Shipment_No as Code,TSPL_SCRAPSALE_head_Return.cust_code as Customer,TSPL_SCRAPSALE_detail_Return.Item_Code as ICode,TSPL_SCRAPSALE_detail_Return.Item_Desc as IName,TSPL_SCRAPSALE_detail_Return.shipped_Qty as Qty,0 as Unapproved,'' as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,null as TransDate ,TSPL_SCRAPSALE_detail_Return.Specification  from TSPL_SCRAPSALE_detail_Return left outer join TSPL_SCRAPSALE_head_Return on TSPL_SCRAPSALE_head_Return.Document_No=TSPL_SCRAPSALE_detail_Return.Document_No where TSPL_SCRAPSALE_head_Return.ispost=1 and len(isnull(TSPL_SCRAPSALE_detail_Return.Shipment_No,''))>0 and TSPL_SCRAPSALE_detail_Return.document_no not in  ('" + strCurrCode + "')  " + Environment.NewLine & _
        qry += " )Final left outer join TSPL_Customer_MASTER on TSPL_Customer_MASTER.cust_code=final.customer left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=Final.Location where 1=1 group by Code,ICode having SUM(Chk)>0 and (SUM(Qty *RI)-SUM(Unapproved)) >0) as xyz where 1=1 order by Code,ICode "

        dtAllData = clsDBFuncationality.GetDataTable(qry)
        If dtAllData Is Nothing OrElse dtAllData.Rows.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "No item found for customer " + CustomerName + "")
            Me.Close()
        End If
        LoadHeadData()
        LoadBlankGridDetail()
    End Sub

    Sub LoadHeadData()
        IsInsideLoadData = True
        LoadBlankHeadGrid()
        Dim arr As New List(Of String)
        For Each dr As DataRow In dtAllData.Rows
            Dim strCode As String = clsCommon.myCstr(dr("code"))
            If Not arr.Contains(strCode) Then
                arr.Add(strCode)
                gvHead.Rows.AddNew()
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHSelect).Value = False
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHCode).Value = strCode
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHDate).Value = clsCommon.myCstr(dr("TransDate"))
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHVendorCode).Value = clsCommon.myCstr(dr("Customer"))
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHVendorName).Value = clsCommon.myCstr(dr("CustomerName"))
                '  gvHead.Rows(gvHead.RowCount - 1).Cells(colHPOType).Value = clsPurchaseOrderHead.GetPurchaseTypeName(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select purchaseorder_type from tspl_grn_head where grn_no='" + strCode + "'")))
                gvHead.Rows(gvHead.RowCount - 1).Cells(colISCASHSALE).Value = clsCommon.myCstr(dr("Is_CashSale"))
                gvHead.Rows(gvHead.RowCount - 1).Cells(colTaxGroup).Value = clsCommon.myCstr(dr("Tax_Group"))
                gvHead.Rows(gvHead.RowCount - 1).Cells(colLocation).Value = clsCommon.myCstr(dr("Location"))
            End If
        Next
        IsInsideLoadData = False
    End Sub

    Private Function isAllowed() As Boolean

        Dim arrVendor As New List(Of String)
        Dim arrPOType As New List(Of String)
        Dim arrIsCashSale As New List(Of String)
        Dim arrTaxGroup As New List(Of String)
        Dim arrLocation As New List(Of String)
        For ii As Integer = 0 To gvHead.RowCount - 1
            If clsCommon.myCBool(gvHead.Rows(ii).Cells(colHSelect).Value) Then
                Dim strCode As String = clsCommon.myCstr(gvHead.Rows(ii).Cells(colHVendorCode).Value)
                Dim strPOType As String = clsCommon.myCstr(gvHead.Rows(ii).Cells(colHPOType).Value)

                Dim strTaxGroup As String = clsCommon.myCstr(gvHead.Rows(ii).Cells(colTaxGroup).Value)
                Dim strLocation As String = clsCommon.myCstr(gvHead.Rows(ii).Cells(colLocation).Value)

                strIsCashSale = clsCommon.myCstr(gvHead.Rows(ii).Cells(colISCASHSALE).Value)
                CustomerCode = strCode
                CustomerName = clsCommon.myCstr(gvHead.Rows(ii).Cells(colHVendorName).Value)

                For jj As Integer = ii + 1 To gvHead.RowCount - 1
                    If clsCommon.myCBool(gvHead.Rows(jj).Cells(colHSelect).Value) Then
                        If clsCommon.CompairString(strCode, clsCommon.myCstr(gvHead.Rows(jj).Cells(colHVendorCode).Value)) <> CompairStringResult.Equal Then
                            arrVendor.Add(clsCommon.myCstr(gvHead.Rows(jj).Cells(colHVendorCode).Value))
                        End If
                        If clsCommon.CompairString(strPOType, clsCommon.myCstr(gvHead.Rows(jj).Cells(colHPOType).Value)) <> CompairStringResult.Equal Then
                            arrPOType.Add(clsCommon.myCstr(gvHead.Rows(jj).Cells(colHPOType).Value))
                        End If
                        If clsCommon.CompairString(strIsCashSale, clsCommon.myCstr(gvHead.Rows(jj).Cells(colISCASHSALE).Value)) <> CompairStringResult.Equal Then
                            arrIsCashSale.Add(clsCommon.myCstr(gvHead.Rows(jj).Cells(colISCASHSALE).Value))
                        End If
                        If clsCommon.CompairString(strTaxGroup, clsCommon.myCstr(gvHead.Rows(jj).Cells(colTaxGroup).Value)) <> CompairStringResult.Equal Then
                            arrTaxGroup.Add(clsCommon.myCstr(gvHead.Rows(jj).Cells(colTaxGroup).Value))
                        End If
                        If clsCommon.CompairString(strLocation, clsCommon.myCstr(gvHead.Rows(jj).Cells(colLocation).Value)) <> CompairStringResult.Equal Then
                            arrLocation.Add(clsCommon.myCstr(gvHead.Rows(jj).Cells(colLocation).Value))
                        End If
                    End If '===detail and head doc no cond.

                Next '==detail for loop

                If arrVendor.Count > 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Item more than one customer are not allowed.", Me.Text)
                    Return False
                End If
                If arrPOType.Count > 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Item more than one type are not allowed.", Me.Text)
                    Return False
                End If
                If arrIsCashSale.Count > 0 Then
                    clsCommon.MyMessageBoxShow(Me, "All Document must be Same Type(Cash/Non Cash).", Me.Text)
                    Return False
                End If
                If arrTaxGroup.Count > 0 Then
                    clsCommon.MyMessageBoxShow(Me, "All Document must have same Tax Group.", Me.Text)
                    Return False
                End If
                If arrLocation.Count > 0 Then
                    clsCommon.MyMessageBoxShow(Me, "All Document must be from same Location.", Me.Text)
                    Return False
                End If

                Return True
            End If '==check status of head

        Next '===head for loop
        Return True
    End Function

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
        repoCode.HeaderText = "Document No"
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

        repoVendorName = New GridViewTextBoxColumn()
        repoVendorName.FormatString = ""
        repoVendorName.HeaderText = "Type"
        repoVendorName.Name = colHPOType
        repoVendorName.Width = 100
        repoVendorName.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoVendorName)

        'sanjay
        Dim repoIs_CashSale As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIs_CashSale.FormatString = ""
        repoIs_CashSale.HeaderText = "Is Cash Sale"
        repoIs_CashSale.Name = colISCASHSALE
        repoIs_CashSale.Width = 100
        repoIs_CashSale.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoIs_CashSale)

        Dim repoTaxGroup As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTaxGroup.FormatString = ""
        repoTaxGroup.HeaderText = "Tax Group"
        repoTaxGroup.Name = colTaxGroup
        repoTaxGroup.Width = 100
        repoTaxGroup.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoTaxGroup)

        Dim repoLocation As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLocation.FormatString = ""
        repoLocation.HeaderText = "Location"
        repoLocation.Name = colLocation
        repoLocation.Width = 100
        repoLocation.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoLocation)
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
        repoCode.HeaderText = "Document No"
        repoCode.Name = colDCode
        repoCode.Width = 180
        repoCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCode)

        repoCode = New GridViewTextBoxColumn()
        repoCode.FormatString = ""
        repoCode.HeaderText = "Shipment No"
        repoCode.Name = colDPOID
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


        Dim repoIType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIType.FormatString = ""
        repoIType.HeaderText = "Row Type"
        repoIType.Name = colDIType
        repoIType.Width = 180
        repoIType.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIType)

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
        repoRate.IsVisible = False
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRate)

        Dim repoOrderQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoOrderQty.FormatString = ""
        repoOrderQty.HeaderText = "Shipment Qty"
        repoOrderQty.Name = colDOrderQty
        repoOrderQty.ReadOnly = True
        repoOrderQty.Width = 80
        repoOrderQty.WrapText = True
        repoOrderQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoOrderQty)

        Dim repoAppQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAppQty.FormatString = ""
        repoAppQty.HeaderText = "Used in Return"
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


        Dim repoTaxCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTaxCode.FormatString = ""
        repoTaxCode.HeaderText = "Tax Group Code"
        repoTaxCode.Name = colDTaxGroup
        repoTaxCode.Width = 100
        repoTaxCode.ReadOnly = True
        repoTaxCode.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxCode)

        Dim repoTaxName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTaxName.FormatString = ""
        repoTaxName.HeaderText = "Tax Group"
        repoTaxName.Name = colDTaxGroupName
        repoTaxName.Width = 100
        repoTaxName.ReadOnly = True
        repoTaxName.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxName)

        Dim repoTaxRate1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate1 = New GridViewDecimalColumn()
        repoTaxRate1.FormatString = ""
        repoTaxRate1.HeaderText = "Tax Rate 1"
        repoTaxRate1.Name = colDTaxRate1
        repoTaxRate1.ReadOnly = True
        repoTaxRate1.IsVisible = False
        repoTaxRate1.WrapText = True
        repoTaxRate1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate1)

        Dim repoTaxRate2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate2 = New GridViewDecimalColumn()
        repoTaxRate2.FormatString = ""
        repoTaxRate2.HeaderText = "Tax Rate 2"
        repoTaxRate2.Name = colDTaxRate2
        repoTaxRate2.ReadOnly = True
        repoTaxRate2.IsVisible = False
        repoTaxRate2.WrapText = True
        repoTaxRate2.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate2)

        Dim repoTaxRate3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate3 = New GridViewDecimalColumn()
        repoTaxRate3.FormatString = ""
        repoTaxRate3.HeaderText = "Tax Rate 3"
        repoTaxRate3.Name = colDTaxRate3
        repoTaxRate3.ReadOnly = True
        repoTaxRate3.IsVisible = False
        repoTaxRate3.WrapText = True
        repoTaxRate3.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate3)

        Dim repoTaxRate4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate4 = New GridViewDecimalColumn()
        repoTaxRate4.FormatString = ""
        repoTaxRate4.HeaderText = "Tax Rate 4"
        repoTaxRate4.Name = colDTaxRate4
        repoTaxRate4.ReadOnly = True
        repoTaxRate4.IsVisible = False
        repoTaxRate4.WrapText = True
        repoTaxRate4.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate4)

        Dim repoTaxRate5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate5 = New GridViewDecimalColumn()
        repoTaxRate5.FormatString = ""
        repoTaxRate5.HeaderText = "Tax Rate 5"
        repoTaxRate5.Name = colDTaxRate5
        repoTaxRate5.ReadOnly = True
        repoTaxRate5.IsVisible = False
        repoTaxRate5.WrapText = True
        repoTaxRate5.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate5)

        Dim repoTaxRate6 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate6 = New GridViewDecimalColumn()
        repoTaxRate6.FormatString = ""
        repoTaxRate6.HeaderText = "Tax Rate 6"
        repoTaxRate6.Name = colDTaxRate6
        repoTaxRate6.ReadOnly = True
        repoTaxRate6.IsVisible = False
        repoTaxRate6.WrapText = True
        repoTaxRate6.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate6)

        Dim repoTaxRate7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate7 = New GridViewDecimalColumn()
        repoTaxRate7.FormatString = ""
        repoTaxRate7.HeaderText = "Tax Rate 7"
        repoTaxRate7.Name = colDTaxRate7
        repoTaxRate7.ReadOnly = True
        repoTaxRate7.IsVisible = False
        repoTaxRate7.WrapText = True
        repoTaxRate7.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate7)

        Dim repoTaxRate8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate8 = New GridViewDecimalColumn()
        repoTaxRate8.FormatString = ""
        repoTaxRate8.HeaderText = "Tax Rate 8"
        repoTaxRate8.Name = colDTaxRate8
        repoTaxRate8.ReadOnly = True
        repoTaxRate8.IsVisible = False
        repoTaxRate8.WrapText = True
        repoTaxRate8.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate8)

        Dim repoTaxRate9 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate9 = New GridViewDecimalColumn()
        repoTaxRate9.FormatString = ""
        repoTaxRate9.HeaderText = "Tax Rate 9"
        repoTaxRate9.Name = colDTaxRate9
        repoTaxRate9.ReadOnly = True
        repoTaxRate9.IsVisible = False
        repoTaxRate9.WrapText = True
        repoTaxRate9.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate9)

        Dim repoTaxRate10 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate10 = New GridViewDecimalColumn()
        repoTaxRate10.FormatString = ""
        repoTaxRate10.HeaderText = "Tax Rate 10"
        repoTaxRate10.Name = colDTaxRate10
        repoTaxRate10.ReadOnly = True
        repoTaxRate10.IsVisible = False
        repoTaxRate10.WrapText = True
        repoTaxRate10.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate10)

        Dim repoMRP As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMRP.FormatString = ""
        repoMRP.HeaderText = "MRP"
        repoMRP.Name = colDMRP
        repoMRP.ReadOnly = True
        repoMRP.IsVisible = False
        repoMRP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoMRP)

        Dim repoAssessable As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAssessable.FormatString = ""
        repoAssessable.HeaderText = "Assessable"
        repoAssessable.Name = colDAssessable
        repoAssessable.ReadOnly = True
        repoAssessable.IsVisible = False
        repoAssessable.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAssessable)


        Dim repoBatchNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBatchNo.FormatString = ""
        repoBatchNo.HeaderText = "Batch No"
        repoBatchNo.Name = colDBatchNo
        repoBatchNo.IsVisible = False
        repoBatchNo.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoBatchNo)

        Dim repoExpiry As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoExpiry.Format = DateTimePickerFormat.Custom
        repoExpiry.CustomFormat = "dd-MM-yyyy"
        repoExpiry.HeaderText = "Expiry Date"
        repoExpiry.FormatString = "{0:d}"
        repoExpiry.Name = colDExpiryDate
        repoExpiry.WrapText = True
        repoExpiry.ReadOnly = True
        repoExpiry.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoExpiry)

        Dim repoManDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoManDate.Format = DateTimePickerFormat.Custom
        repoManDate.CustomFormat = "dd-MM-yyyy"
        repoManDate.HeaderText = "Manufacturer Date"
        repoManDate.WrapText = True
        repoManDate.FormatString = "{0:d}"
        repoManDate.Name = colDManDate
        repoManDate.ReadOnly = True
        repoManDate.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoManDate)

        Dim repoDiscPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDiscPer.FormatString = ""
        repoDiscPer.HeaderText = "Discount Per"
        repoDiscPer.Name = colDDisPer
        repoDiscPer.ReadOnly = True
        repoDiscPer.IsVisible = False
        repoDiscPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoDiscPer)


        Dim repoLeakQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLeakQty.FormatString = ""
        repoLeakQty.HeaderText = "Leakage"
        repoLeakQty.Name = colDLeakQty
        repoLeakQty.ReadOnly = True
        repoLeakQty.IsVisible = False
        repoLeakQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLeakQty)

        Dim repoBurst As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBurst.FormatString = ""
        repoBurst.HeaderText = "Burst"
        repoBurst.Name = colDBurstQty
        repoBurst.ReadOnly = True
        repoBurst.IsVisible = False
        repoBurst.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoBurst)

        Dim repoShort As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoShort.FormatString = ""
        repoShort.HeaderText = "Shortage"
        repoShort.Name = colDShortQty
        repoShort.ReadOnly = True
        repoShort.IsVisible = False
        repoShort.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoShort)

      

    

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
        btnOk.Focus()
        ArrReturn = New List(Of ClsScrapSaleDetail)

        If Not isAllowed() Then
            Exit Sub
        End If

        Dim obj As ClsScrapSaleDetail = Nothing
        For ii As Integer = 0 To gv1.RowCount - 1
            If (clsCommon.myCBool(gv1.Rows(ii).Cells(colDSelect).Value)) Then
                obj = New ClsScrapSaleDetail()

                'obj.Category = clsCommon.myCstr(gv1.Rows(ii).Cells(colCategoryType).Value)
                'obj.Emergency = CInt(gv1.Rows(ii).Cells(colEmergency).Value)
            

                obj.document_No = clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)
                obj.shipment_No = clsCommon.myCstr(gv1.Rows(ii).Cells(colDPOID).Value)
                obj.Item_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDICode).Value)
                obj.Item_Desc = clsCommon.myCstr(gv1.Rows(ii).Cells(colDIName).Value)
                ' obj.Row_Type = clsCommon.myCstr(gv1.Rows(ii).Cells(colDIType).Value)
                obj.price = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDRate).Value)
                obj.Unit_code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDUnit).Value)
                ''obj.Location = clsCommon.myCstr(gv1.Rows(ii).Cells(cold).Value)
                ''obj.LocationName = clsCommon.myCstr(gv1.Rows(ii).Cells("LocationName").Value)

                obj.pending_Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDPendingQty).Value)
                obj.shipped_Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDOrderQty).Value)
                'obj.Leak_Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDLeakQty).Value)
                'obj.Burst_Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDBurstQty).Value)
                'obj.Short_Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDShortQty).Value)

                '    obj.GRNTax_Group = clsCommon.myCstr(gv1.Rows(ii).Cells(colDTaxGroup).Value)
                obj.TAX1_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxRate1).Value)
                obj.TAX2_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxRate2).Value)
                obj.TAX3_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxRate3).Value)
                obj.TAX4_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxRate4).Value)
                obj.TAX5_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxRate5).Value)
                obj.TAX6_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxRate6).Value)
                obj.TAX7_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxRate7).Value)
                obj.TAX8_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxRate8).Value)
                obj.TAX9_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxRate9).Value)
                obj.TAX10_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxRate10).Value)
                obj.DiscountPer = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDDisPer).Value)
                'obj.MRP = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDMRP).Value)
                'obj.Assessable = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDAssessable).Value)
                'obj.Batch_No = clsCommon.myCstr(gv1.Rows(ii).Cells(colDBatchNo).Value)
                obj.shipment_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select top 1 isnull(Shipment_No,'')  from TSPL_SCRAPSALE_detail where Shipment_No='" & obj.document_No & "'  and Item_code='" & obj.Item_Code & "'"))
                'If clsCommon.myLen(gv1.Rows(ii).Cells(colDManDate).Value) > 0 Then
                '    obj.MFG_Date = clsCommon.myCDate(gv1.Rows(ii).Cells(colDManDate).Value)
                'End If
                'If clsCommon.myLen(gv1.Rows(ii).Cells(colDExpiryDate).Value) > 0 Then
                '    obj.Expiry_Date = clsCommon.myCDate(gv1.Rows(ii).Cells(colDExpiryDate).Value)
                'End If
                If (obj.shipped_Qty > 0) Then
                    ArrReturn.Add(obj)
                End If
            End If
        Next
        If ArrReturn.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select at least one non zero Pending Shipment item", Me.Text)
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
        If gv1.CurrentColumn Is gv1.Columns(colDCode) Then
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
        If Not IsInsideLoadData Then
            If gvHead.CurrentColumn Is gvHead.Columns(colHSelect) Then
                Dim strVendorCode As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHVendorCode).Value)
                Dim strVendorName As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHVendorName).Value)
                If clsCommon.myLen(CustomerCode) <= 0 Then
                    CustomerCode = strVendorCode
                    CustomerName = strVendorName
                End If
                If clsCommon.CompairString(strVendorCode, CustomerCode) = CompairStringResult.Equal Then
                    Dim strCode As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHCode).Value)
                    If clsCommon.myLen(strCode) > 0 Then
                        LoadDetailData(e.NewValue, strCode)
                    End If
                Else
                    common.clsCommon.MyMessageBoxShow(Me, "Document Customer should be `" + CustomerName, Me.Text)
                    e.Cancel = True
                End If
            End If
        End If

        ''If Not IsInsideLoadData Then
        ''    If gvHead.CurrentColumn Is gvHead.Columns(colHSelect) Then
        ''        Dim strCode As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHCode).Value)
        ''        If clsCommon.myLen(strCode) > 0 Then
        ''            LoadDetailData(e.NewValue, strCode)
        ''        End If
        ''    End If
        ''End If
    End Sub

    Sub LoadDetailData(ByVal NewVal As Boolean, ByVal strCode As String)

        If NewVal Then
            For Each dr As DataRow In dtAllData.Rows
                If clsCommon.CompairString(strCode, clsCommon.myCstr(dr("Code"))) = CompairStringResult.Equal Then
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDSelect).Value = True
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDCode).Value = clsCommon.myCstr(dr("code"))
                    '  gv1.Rows(gv1.Rows.Count - 1).Cells(colDPOID).Value = IIf(clsCommon.myLen(clsCommon.myCstr(dr("Against_RGP_No"))), clsCommon.myCstr(dr("Against_RGP_No")), clsCommon.myCstr(dr("PO_ID")))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDICode).Value = clsCommon.myCstr(dr("ICode"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDIName).Value = clsCommon.myCstr(dr("IName"))
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colDIType).Value = clsCommon.myCstr(dr("IType"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDUnit).Value = clsCommon.myCstr(dr("Unit"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDRate).Value = clsCommon.myCdbl(dr("Rate"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDOrderQty).Value = clsCommon.myCdbl(dr("ShipmentQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDApprovedQty).Value = clsCommon.myCdbl(dr("ReturnQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDUnApprovedQty).Value = clsCommon.myCdbl(dr("UnapprovedQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDPendingQty).Value = clsCommon.myCdbl(dr("PendingQty"))
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxGroup).Value = clsCommon.myCstr(dr("Tax_Group"))
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxGroupName).Value = clsCommon.myCstr(dr("TaxGroupName"))
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate1).Value = clsCommon.myCdbl(dr("TAX1_Rate"))
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate2).Value = clsCommon.myCdbl(dr("TAX2_Rate"))
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate3).Value = clsCommon.myCdbl(dr("TAX3_Rate"))
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate4).Value = clsCommon.myCdbl(dr("TAX4_Rate"))
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate5).Value = clsCommon.myCdbl(dr("TAX5_Rate"))
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate6).Value = clsCommon.myCdbl(dr("TAX6_Rate"))
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate7).Value = clsCommon.myCdbl(dr("TAX7_Rate"))
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate8).Value = clsCommon.myCdbl(dr("TAX8_Rate"))
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate9).Value = clsCommon.myCdbl(dr("TAX9_Rate"))
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate10).Value = clsCommon.myCdbl(dr("TAX10_Rate"))

                   
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colDMRP).Value = clsCommon.myCdbl(dr("MRP"))
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colDAssessable).Value = clsCommon.myCdbl(dr("Assessable"))
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colDBatchNo).Value = clsCommon.myCstr(dr("Batch_No"))
                    'If clsCommon.myLen(dr("MFG_Date")) > 0 Then
                    '    gv1.Rows(gv1.Rows.Count - 1).Cells(colDManDate).Value = clsCommon.myCDate(dr("MFG_Date"))
                    'End If
                    'If clsCommon.myLen(dr("Expiry_Date")) > 0 Then
                    '    gv1.Rows(gv1.Rows.Count - 1).Cells(colDExpiryDate).Value = clsCommon.myCDate(dr("Expiry_Date"))
                    'End If
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colDDisPer).Value = clsCommon.myCdbl(dr("Disc_Per"))

                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colDLeakQty).Value = clsCommon.myCdbl(dr("Leak_Qty"))
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colDBurstQty).Value = clsCommon.myCdbl(dr("Burst_Qty"))
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colDShortQty).Value = clsCommon.myCdbl(dr("Short_Qty"))

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

    Private Sub gvHead_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gvHead.Click

    End Sub

End Class

