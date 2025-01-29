Imports common
Public Class frmPendingQuotation
#Region "Variables"
    Public _ISExport_Sale As Boolean = False
    Public VendorCode As String = Nothing
    Public VendorName As String = Nothing
    Public strCurrCode As String = Nothing
    Public ArrReturn As List(Of clsSalesQuotationsDetail) = Nothing
    Public ArrReturn1 As List(Of clsEXSalesQuotationDetail) = Nothing

    Const colDSelect As String = "SELECT"
    Const colDCode As String = "CODE"
    Const colDICode As String = "ICODE"
    Const colDIName As String = "INAME"
    Const colDUnit As String = "UNIT"
    Const colDRate As String = "RATE"
    Const colDOrderQty As String = "ORDERQTY"
    Const colDApprovedQty As String = "APPROVEDQTY"
    Const colDUnApprovedQty As String = "UNAPPROVEDQTY"
    Const colDPendingQty As String = "PENDINGQTY"
    Const colDVendor As String = "VENDOR"
    Const colDVendorName As String = "VENDORNAME"

    Const colHSelect As String = "SELECT"
    Const colHCode As String = "CODE"
    Const colHDate As String = "DATE"

    Dim dtAllData As DataTable = Nothing
    Dim IsInsideLoadData As Boolean = False
#End Region

    Private Sub FrmPendingRequistion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If clsCommon.myLen(VendorName) > 0 Then
            Me.Text = Me.Text + " For " + VendorName
        End If
        Dim qry As String = "select CAST(0 as bit) as Sel,code,ICode,max(IName) as IName" + Environment.NewLine & _
                ",max(Unit)as Unit,max(Location) as Location,MAX(TSPL_LOCATION_MASTER.Location_Desc) as LocationName" + Environment.NewLine & _
                ",SUM(Qty* case when RI=1 then 1 else 0 end) as RequitionQty" + Environment.NewLine & _
                ",SUM(Qty* case when RI=-1 then 1 else 0 end) as POQty" + Environment.NewLine & _
                ",SUM(Unapproved) as UnapprovedQty" + Environment.NewLine & _
                ",SUM((Qty *RI)- Unapproved) as PedningQty ,MAX(Rate) as Rate,max(TransDate) as TransDate,max(final.Vendor) as Vendor,MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as VendorName " + Environment.NewLine & _
                " from (" + Environment.NewLine & _
                " select TSPL_SD_QUOTATION_DETAIL.Document_Code as Code,TSPL_SD_QUOTATION_HEAD.Customer_Code as Vendor,TSPL_SD_QUOTATION_DETAIL.Item_Code as ICode,TSPL_ITEM_MASTER.Item_Desc as IName,TSPL_SD_QUOTATION_DETAIL.Qty as Qty,0 as Unapproved,TSPL_SD_QUOTATION_DETAIL.Unit_Code as Unit,TSPL_SD_QUOTATION_DETAIL.Location as Location,1 as RI,TSPL_SD_QUOTATION_DETAIL.Item_Cost as Rate,1 as Chk,TSPL_SD_QUOTATION_HEAD.Document_Date as TransDate from TSPL_SD_QUOTATION_DETAIL left outer join TSPL_SD_QUOTATION_HEAD on TSPL_SD_QUOTATION_HEAD.Document_Code=TSPL_SD_QUOTATION_DETAIL.Document_Code  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_QUOTATION_DETAIL.Item_Code where TSPL_SD_QUOTATION_DETAIL.Status='N' and TSPL_SD_QUOTATION_HEAD.Status=1 and TSPL_SD_QUOTATION_HEAD.close_yn='N'"
        If clsCommon.myLen(VendorCode) > 0 Then
            qry += " and (len(ISNULL(TSPL_SD_QUOTATION_HEAD.Customer_Code,''))=0 or TSPL_SD_QUOTATION_HEAD.Customer_Code='" + VendorCode + "')" + Environment.NewLine
        End If
        qry += " union all" + Environment.NewLine & _
                " select TSPL_SD_SALES_ORDER_DETAIL.Quotation_Code as Code,TSPL_SD_SALES_ORDER_HEAD.Customer_Code as Vendor,TSPL_SD_SALES_ORDER_DETAIL.Item_Code as ICode,'' as IName,TSPL_SD_SALES_ORDER_DETAIL.Qty as Qty,0 as Unapproved,'' as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,null as TransDate  " + Environment.NewLine & _
                " from TSPL_SD_SALES_ORDER_DETAIL left outer join TSPL_SD_SALES_ORDER_HEAD on TSPL_SD_SALES_ORDER_HEAD.Document_Code=TSPL_SD_SALES_ORDER_DETAIL.Document_Code where TSPL_SD_SALES_ORDER_HEAD.Status=1 and len(isnull(TSPL_SD_SALES_ORDER_DETAIL.Document_Code,''))>0 " + Environment.NewLine & _
                " union all " + Environment.NewLine & _
                " select TSPL_SD_SALES_ORDER_DETAIL.Quotation_Code as Code,TSPL_SD_SALES_ORDER_HEAD.Customer_Code as Vendor,TSPL_SD_SALES_ORDER_DETAIL.Item_Code as ICode,'' as IName,0  as Qty,TSPL_SD_SALES_ORDER_DETAIL.Qty as Unapproved,'' as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,null as TransDate from TSPL_SD_SALES_ORDER_DETAIL left outer join TSPL_SD_SALES_ORDER_HEAD on TSPL_SD_SALES_ORDER_HEAD.Document_Code=TSPL_SD_SALES_ORDER_DETAIL.Document_Code where TSPL_SD_SALES_ORDER_HEAD.Status=0 and len(isnull(TSPL_SD_SALES_ORDER_DETAIL.Document_Code,''))>0 and TSPL_SD_SALES_ORDER_DETAIL.Document_Code not in ('" + strCurrCode + "')  " + Environment.NewLine & _
                " )Final" + Environment.NewLine & _
                " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code    =final.Vendor" + Environment.NewLine & _
                " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=Final.Location" + Environment.NewLine & _
                " group by Code,ICode" + Environment.NewLine & _
                " having SUM(Chk)>0 and SUM(Qty *RI) <>0" + Environment.NewLine & _
                " order by Code,ICode"
        dtAllData = clsDBFuncationality.GetDataTable(qry)


        If dtAllData Is Nothing OrElse dtAllData.Rows.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("No Pending item found ")
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
        repoCode.HeaderText = "Requisition No"
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
        repoCode.HeaderText = "Requisition No"
        repoCode.Name = colDCode
        repoCode.Width = 180
        repoCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCode)

        Dim repoVendor As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVendor.FormatString = ""
        repoVendor.HeaderText = "Customer Code"
        repoVendor.Name = colDVendor
        repoVendor.Width = 100
        repoVendor.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoVendor)

        Dim repoVendorName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVendorName.FormatString = ""
        repoVendorName.HeaderText = "Customer"
        repoVendorName.Name = colDVendorName
        repoVendorName.Width = 150
        repoVendorName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoVendorName)


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
        repoUnit.Width = 60
        repoUnit.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoUnit)


        Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "Rate"
        repoRate.Name = colDRate
        repoRate.ReadOnly = True
        repoUnit.IsVisible = False
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRate)

        Dim repoOrderQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoOrderQty.FormatString = ""
        repoOrderQty.HeaderText = "Quotation Qty"
        repoOrderQty.Name = colDOrderQty
        repoOrderQty.ReadOnly = True
        repoOrderQty.Width = 80
        repoOrderQty.WrapText = True
        repoOrderQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoOrderQty)

        Dim repoAppQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAppQty.FormatString = ""
        repoAppQty.HeaderText = "Used in Sales Order"
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
        Dim ClassName As String = ""
        If _ISExport_Sale = False Then
            
            ArrReturn = New List(Of clsSalesQuotationsDetail)
            Dim obj As clsSalesQuotationsDetail = Nothing
            For ii As Integer = 0 To gv1.RowCount - 1
                If (clsCommon.myCBool(gv1.Rows(ii).Cells(colDSelect).Value)) Then
                    obj = New clsSalesQuotationsDetail()
                    obj.Document_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)
                    obj.Item_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDICode).Value)
                    obj.Item_Desc = clsCommon.myCstr(gv1.Rows(ii).Cells(colDIName).Value)
                    obj.Item_Cost = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDRate).Value)
                    obj.Unit_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDUnit).Value)
                    'obj.Location = clsCommon.myCstr(gv1.Rows(ii).Cells(coldlo "Location").Value)
                    'obj.LocationName = clsCommon.myCstr(gv1.Rows(ii).Cells("LocationName").Value)
                    obj.Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDOrderQty).Value)
                    obj.Balance_Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDPendingQty).Value)
                    If (obj.Qty > 0) Then
                        ArrReturn.Add(obj)
                    End If
                End If
            Next

            If ArrReturn.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one non zero Pending Requition item")
            Else
                Me.Close()
            End If
        Else

            ArrReturn1 = New List(Of clsEXSalesQuotationDetail)
            Dim obj1 As clsEXSalesQuotationDetail = Nothing
            For ii As Integer = 0 To gv1.RowCount - 1
                If (clsCommon.myCBool(gv1.Rows(ii).Cells(colDSelect).Value)) Then
                    obj1 = New clsEXSalesQuotationDetail()
                    obj1.Document_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)
                    obj1.Item_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDICode).Value)
                    obj1.Item_Desc = clsCommon.myCstr(gv1.Rows(ii).Cells(colDIName).Value)
                    obj1.Item_Cost = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDRate).Value)
                    obj1.Unit_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDUnit).Value)
                    'obj.Location = clsCommon.myCstr(gv1.Rows(ii).Cells(coldlo "Location").Value)
                    'obj.LocationName = clsCommon.myCstr(gv1.Rows(ii).Cells("LocationName").Value)
                    obj1.Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDOrderQty).Value)
                    obj1.Balance_Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDPendingQty).Value)
                    If (obj1.Qty > 0) Then
                        ArrReturn1.Add(obj1)
                    End If
                End If
            Next

            If ArrReturn1.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one non zero Pending Requition item")
            Else
                Me.Close()
            End If
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
            Dim strCode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colDCode).Value)
            Dim SelectStatus As Boolean = clsCommon.myCBool(gv1.CurrentRow.Cells(colDSelect).Value)
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.CompairString(strCode, clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)) = CompairStringResult.Equal Then
                    gv1.Rows(ii).Cells(colDSelect).Value = Not SelectStatus
                End If
            Next
        End If
    End Sub

    Private Sub gvHead_ValueChanging(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.ValueChangingEventArgs) Handles gvHead.ValueChanging
        If Not IsInsideLoadData Then
            If gvHead.CurrentColumn Is gvHead.Columns(colHSelect) Then
                Dim strCode As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHCode).Value)
                If clsCommon.myLen(strCode) > 0 Then
                    LoadDetailData(e.NewValue, strCode)
                End If
            End If
        End If
    End Sub

    Sub LoadDetailData(ByVal NewVal As Boolean, ByVal strCode As String)
        IsInsideLoadDataOfItem = True
        If NewVal Then
            For Each dr As DataRow In dtAllData.Rows
                If clsCommon.CompairString(strCode, clsCommon.myCstr(dr("Code"))) = CompairStringResult.Equal Then
                    Dim strVendorCode As String = clsCommon.myCstr(dr("Vendor"))
                    If clsCommon.myLen(VendorCode) <= 0 AndAlso clsCommon.myLen(strVendorCode) > 0 Then
                        VendorCode = strVendorCode
                    End If

                    gv1.Rows.AddNew()
                    If (clsCommon.myLen(strVendorCode) > 0 Xor clsCommon.CompairString(VendorCode, strVendorCode) = CompairStringResult.Equal) Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDSelect).Value = False
                    Else
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDSelect).Value = True
                    End If
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDCode).Value = clsCommon.myCstr(dr("code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDICode).Value = clsCommon.myCstr(dr("ICode"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDIName).Value = clsCommon.myCstr(dr("IName"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDUnit).Value = clsCommon.myCstr(dr("Unit"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDVendor).Value = clsCommon.myCstr(dr("Vendor"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDVendorName).Value = clsCommon.myCstr(dr("VendorName"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDRate).Value = clsCommon.myCdbl(dr("Rate"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDOrderQty).Value = clsCommon.myCdbl(dr("RequitionQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDApprovedQty).Value = clsCommon.myCdbl(dr("POQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDUnApprovedQty).Value = clsCommon.myCdbl(dr("UnapprovedQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDPendingQty).Value = clsCommon.myCdbl(dr("PedningQty"))
                End If
            Next
        Else
            For ii As Integer = gv1.Rows.Count - 1 To 0 Step -1
                If clsCommon.CompairString(strCode, clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)) = CompairStringResult.Equal Then
                    gv1.Rows.RemoveAt(ii)
                End If
            Next
        End If
        IsInsideLoadDataOfItem = False
    End Sub
    Dim IsInsideLoadDataOfItem As Boolean = False
    Private Sub gv1_ValueChanging(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.ValueChangingEventArgs) Handles gv1.ValueChanging
        If Not IsInsideLoadDataOfItem Then
            If gv1.CurrentColumn Is gv1.Columns(colDSelect) Then
                If e.NewValue Then
                    Dim strVendorCode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colDVendor).Value)
                    Dim strVendoeName As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colDVendorName).Value)
                    If clsCommon.myLen(strVendorCode) > 0 AndAlso Not clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colDVendor).Value), VendorCode) = CompairStringResult.Equal Then
                        common.clsCommon.MyMessageBoxShow("Can't select Current item.It is For Customer:" + strVendoeName)
                        e.Cancel = True
                    End If
                End If
            End If
        End If
    End Sub
End Class

