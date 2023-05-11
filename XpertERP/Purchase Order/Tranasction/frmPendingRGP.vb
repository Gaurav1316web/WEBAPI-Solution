Imports common
Public Class frmPendingRGP
#Region "Variables"
    Dim IsInsideLoadData As Boolean = False
    Public VendorCode As String = Nothing
    Public VendorName As String = Nothing
    Public strCurrCode As String = Nothing
    Public strRGPType As String = Nothing
    Public strJobWorkType As String = Nothing
    Public strWhrCond As String = Nothing
    Public ArrReturn As List(Of clsRGPDetail) = Nothing
    Public ArrReturn_Job As List(Of clsRGPBOMItem) = Nothing
    Dim dtAllData As DataTable = Nothing

    Const colDSelect As String = "SELECT"
    Const colDCode As String = "CODE"
    Const colDICode As String = "ICODE"
    Const colDIName As String = "INAME"
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
    Const colDBatchNo As String = "BATCHNO"
    Const colDManDate As String = "MANFACTURERDATE"
    Const colDExpiryDate As String = "EXPIRYDATE"
    Const colDDisPer As String = "DISCOUNTPER"

    Const colHSelect As String = "SELECT"
    Const colHCode As String = "CODE"
    Const colHDate As String = "DATE"
    Const colHVendorCode As String = "VENDOR"
    Const colHVendorName As String = "VENDORNAME"
#End Region

    Private Sub FrmPendingRequistion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        setGridPropery()
        Dim strSRNCondition As String = ""
        Dim strGRNCondtition As String = ""
        If clsCommon.myLen(strRGPType) > 0 Then
            strSRNCondition = " and TSPL_SRN_HEAD.purchaseorder_type='" + strRGPType + "' "
            strGRNCondtition = " and TSPL_GRN_HEAD.purchaseorder_type='" + strRGPType + "' "
        End If

        Dim qry As String = "select CAST(0 as bit) as Sel,code,ICode,max(IName) as IName,max(Unit)as Unit," & _
        " SUM(Qty* case when RI=1 then 1 else 0 end) as POQty," & _
        " SUM(Qty* case when RI=-1 then 1 else 0 end) as GRNQty," & _
        " SUM(Unapproved) as UnapprovedQty," & _
        " SUM((Qty *RI)- Unapproved) as PedningQty ,MAX(Rate) as Rate," & _
        " max(TransDate) as TransDate ,MAX(Vendor) as Vendor,MAX(TSPL_VENDOR_MASTER.Vendor_Name) as VendorName  from ( "
        If clsCommon.CompairString(strRGPType, "J") = CompairStringResult.Equal Then
            qry += " select TSPL_RGP_JOB_WORK_DETAIL.RGP_No as Code,TSPL_RGP_HEAD.Vendor_Code as Vendor,TSPL_RGP_JOB_WORK_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,TSPL_RGP_JOB_WORK_DETAIL.RGP_Qty as Qty,0 as Unapproved,TSPL_RGP_JOB_WORK_DETAIL.Unit_Code as Unit,1 as RI,TSPL_RGP_JOB_WORK_DETAIL.rate as Rate,1 as Chk,RGP_Date as TransDate from TSPL_RGP_JOB_WORK_DETAIL left outer join TSPL_RGP_HEAD on TSPL_RGP_HEAD.RGP_No=TSPL_RGP_JOB_WORK_DETAIL.RGP_No left outer join tspl_item_master on tspl_item_master.item_code=TSPL_RGP_JOB_WORK_DETAIL.item_code where TSPL_RGP_HEAD.Status=1 and TSPL_RGP_HEAD.Doc_Type='RGP'"
        Else
            qry += " select TSPL_RGP_DETAIL.RGP_No as Code,TSPL_RGP_HEAD.Vendor_Code as Vendor,TSPL_RGP_DETAIL.Item_Code as ICode,TSPL_RGP_DETAIL.Item_Desc as IName,TSPL_RGP_DETAIL.RGP_Qty as Qty,0 as Unapproved,TSPL_RGP_DETAIL.Unit_Code as Unit,1 as RI,TSPL_RGP_DETAIL.Item_Cost as Rate,1 as Chk,RGP_Date as TransDate from TSPL_RGP_DETAIL left outer join TSPL_RGP_HEAD on TSPL_RGP_HEAD.RGP_No=TSPL_RGP_DETAIL.RGP_No where TSPL_RGP_HEAD.Status=1 and TSPL_RGP_HEAD.Doc_Type='RGP' "
            If clsCommon.myLen(strWhrCond) > 0 Then
                qry += " and isnull(tspl_rgp_head.against_jobwork,'0')='0'"
            End If
        End If
        If clsCommon.myLen(VendorCode) > 0 Then
            qry += " and TSPL_RGP_HEAD.Vendor_Code='" + VendorCode + "'"
        End If
        If clsCommon.myLen(strRGPType) <= 0 Then
            qry += " and isnull(tspl_rgp_head.Against_JobWork,'0')='0' "
        End If
        If clsCommon.myLen(strRGPType) > 0 AndAlso clsCommon.CompairString(strRGPType, "J") = CompairStringResult.Equal AndAlso clsCommon.CompairString(strJobWorkType, "AI") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(strJobWorkType, "AR") <> CompairStringResult.Equal Then
            qry += " and isnull(tspl_rgp_head.Against_JobWork,'0')='1' and isnull(tspl_rgp_head.Against_BOM,'0')='0' "
        ElseIf (clsCommon.myLen(strJobWorkType) > 0 AndAlso clsCommon.CompairString(strJobWorkType, "AR") = CompairStringResult.Equal) AndAlso clsCommon.CompairString(strJobWorkType, "AI") <> CompairStringResult.Equal Then
            qry += " and isnull(tspl_rgp_head.Against_JobWork,'0')='1' and isnull(tspl_rgp_head.Against_BOM,'0')='0' and isnull(tspl_rgp_head.Against_As_It_Is,'0')='0' "
        ElseIf clsCommon.myLen(strJobWorkType) > 0 AndAlso clsCommon.CompairString(strJobWorkType, "AI") = CompairStringResult.Equal Then
            qry += " and isnull(tspl_rgp_head.Against_JobWork,'0')='1' and isnull(tspl_rgp_head.Against_BOM,'0')='0' and isnull(tspl_rgp_head.Against_As_It_Is,'0')='1' "
        Else
            qry += " and isnull(tspl_rgp_head.Against_BOM,'0')='0' "
        End If
        If clsCommon.CompairString(strRGPType, "J") = CompairStringResult.Equal Then
            qry += " and  isnull( TSPL_RGP_HEAD.Is_Repair,0)=0" + Environment.NewLine
            qry += " union all" + Environment.NewLine
            qry += " select TSPL_RGP_DETAIL.RGP_No as Code,TSPL_RGP_HEAD.Vendor_Code as Vendor,TSPL_RGP_DETAIL.Item_Code as ICode,TSPL_RGP_DETAIL.Item_Desc as IName,TSPL_RGP_DETAIL.RGP_Qty as Qty,0 as Unapproved,TSPL_RGP_DETAIL.Unit_Code as Unit,1 as RI,TSPL_RGP_DETAIL.Item_Cost as Rate,1 as Chk,RGP_Date as TransDate from TSPL_RGP_DETAIL left outer join TSPL_RGP_HEAD on TSPL_RGP_HEAD.RGP_No=TSPL_RGP_DETAIL.RGP_No where TSPL_RGP_HEAD.Status=1 and TSPL_RGP_HEAD.Doc_Type='RGP' "
            If clsCommon.myLen(strWhrCond) > 0 Then
                qry += " and isnull(tspl_rgp_head.against_jobwork,'0')='0'"
            End If
            If clsCommon.myLen(VendorCode) > 0 Then
                qry += " and TSPL_RGP_HEAD.Vendor_Code='" + VendorCode + "'"
            End If
            If clsCommon.myLen(strRGPType) <= 0 Then
                qry += " and isnull(tspl_rgp_head.Against_JobWork,'0')='0' "
            End If
            If clsCommon.myLen(strRGPType) > 0 AndAlso clsCommon.CompairString(strRGPType, "J") = CompairStringResult.Equal AndAlso clsCommon.CompairString(strJobWorkType, "AI") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(strJobWorkType, "AR") <> CompairStringResult.Equal Then
                qry += " and isnull(tspl_rgp_head.Against_JobWork,'0')='1' and isnull(tspl_rgp_head.Against_BOM,'0')='0' "
            ElseIf (clsCommon.myLen(strJobWorkType) > 0 AndAlso clsCommon.CompairString(strJobWorkType, "AR") = CompairStringResult.Equal) AndAlso clsCommon.CompairString(strJobWorkType, "AI") <> CompairStringResult.Equal Then
                qry += " and isnull(tspl_rgp_head.Against_JobWork,'0')='1' and isnull(tspl_rgp_head.Against_BOM,'0')='0' and isnull(tspl_rgp_head.Against_As_It_Is,'0')='0' "
            ElseIf clsCommon.myLen(strJobWorkType) > 0 AndAlso clsCommon.CompairString(strJobWorkType, "AI") = CompairStringResult.Equal Then
                qry += " and isnull(tspl_rgp_head.Against_JobWork,'0')='1' and isnull(tspl_rgp_head.Against_BOM,'0')='0' and isnull(tspl_rgp_head.Against_As_It_Is,'0')='1' "
            Else
                qry += " and isnull(tspl_rgp_head.Against_BOM,'0')='0' "
            End If
            qry += " and  isnull( TSPL_RGP_HEAD.Is_Repair,0)=1" + Environment.NewLine
        End If

        qry += " union all" & _
        " select TSPL_SRN_DETAIL.RGP_Id as Code,TSPL_SRN_HEAD.Vendor_Code as Vendor,TSPL_SRN_DETAIL.Item_Code as ICode,TSPL_SRN_DETAIL.Item_Desc as IName,isnull(TSPL_SRN_DETAIL.SRN_Qty,0)+isnull(TSPL_SRN_DETAIL.Rejected_Qty,0) as Qty,0 as Unapproved,'' as Unit,-1 as RI,0 as Rate,0 as Chk,null as TransDate from TSPL_SRN_DETAIL left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No where TSPL_SRN_HEAD.Status=1 and len(isnull(TSPL_SRN_DETAIL.RGP_Id,''))>0 " + strSRNCondition + " " & _
        " union all  " & _
        " select TSPL_SRN_DETAIL.RGP_Id as Code,TSPL_SRN_HEAD.Vendor_Code as Vendor,TSPL_SRN_DETAIL.Item_Code as ICode,TSPL_SRN_DETAIL.Item_Desc as IName,0  as Qty,isnull(TSPL_SRN_DETAIL.SRN_Qty,0)+isnull(TSPL_SRN_DETAIL.Rejected_Qty,0) as Unapproved,'' as Unit,-1 as RI,0 as Rate,0 as Chk,null as TransDate from TSPL_SRN_DETAIL left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  where TSPL_SRN_HEAD.Status=0 and len(isnull(TSPL_SRN_DETAIL.RGP_Id,''))>0 and TSPL_SRN_DETAIL.SRN_No not in ('" + strCurrCode + "') " + strSRNCondition + "  "
        '============add grn code=====================================
        qry += " union all" & _
        " select TSPL_GRN_DETAIL.Against_RGP_No as Code,TSPL_GRN_HEAD.Vendor_Code as Vendor,TSPL_GRN_DETAIL.Item_Code as ICode,TSPL_GRN_DETAIL.Item_Desc as IName,isnull(Case when coalesce(TSPL_SRN_Head.srn_No,'')='' then TSPL_GRN_DETAIL.GRN_Qty else 0 end,0) as Qty,0 as Unapproved,'' as Unit,-1 as RI,0 as Rate,0 as Chk,null as TransDate from TSPL_GRN_DETAIL left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_GRN_DETAIL.GRN_No left join TSPL_SRN_Head on TSPL_SRN_Head.Against_GRN=TSPL_GRN_HEAD.GRN_No where TSPL_GRN_HEAD.Status=1 and len(isnull(TSPL_GRN_DETAIL.Against_RGP_No,''))>0 " + strGRNCondtition + " " & _
        " union all  " & _
        " select TSPL_GRN_DETAIL.Against_RGP_No as Code,TSPL_GRN_HEAD.Vendor_Code as Vendor,TSPL_GRN_DETAIL.Item_Code as ICode,TSPL_GRN_DETAIL.Item_Desc as IName,0  as Qty,isnull(TSPL_GRN_DETAIL.GRN_Qty,0) as Unapproved,'' as Unit,-1 as RI,0 as Rate,0 as Chk,null as TransDate from TSPL_GRN_DETAIL left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_GRN_DETAIL.GRN_No where TSPL_GRN_HEAD.Status=0 and len(isnull(TSPL_GRN_DETAIL.Against_RGP_No,''))>0 and TSPL_GRN_DETAIL.GRN_No not in ('" + strCurrCode + "') " + strGRNCondtition + "  " & _
        " )Final left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=final.Vendor  group by Code,ICode having SUM(Chk)>0 and SUM(Qty *RI) <>0 order by Code,ICode "
        dtAllData = clsDBFuncationality.GetDataTable(qry)
        If dtAllData Is Nothing OrElse dtAllData.Rows.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("No item found for vendor " + VendorName + "")
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
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHVendorCode).Value = clsCommon.myCstr(dr("Vendor"))
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHVendorName).Value = clsCommon.myCstr(dr("VendorName"))
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
        repoCode.HeaderText = "RGP No"
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
        repoVendor.HeaderText = "Vendor"
        repoVendor.Name = colHVendorCode
        repoVendor.Width = 170
        repoVendor.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoVendor)

        Dim repoVendorName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVendorName.FormatString = ""
        repoVendorName.HeaderText = "Vendor Name"
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
        repoCode.HeaderText = "RGP No"
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
        repoOrderQty.HeaderText = "RGP Qty"
        repoOrderQty.Name = colDOrderQty
        repoOrderQty.ReadOnly = True
        repoOrderQty.Width = 80
        repoOrderQty.WrapText = True
        repoOrderQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoOrderQty)

        Dim repoAppQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAppQty.FormatString = ""
        repoAppQty.HeaderText = "Used in GRN/SRN"
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
        btnOk.Focus()
        ArrReturn = New List(Of clsRGPDetail)
        ArrReturn_Job = New List(Of clsRGPBOMItem)

        If clsCommon.CompairString(strRGPType, "J") = CompairStringResult.Equal Then
            Dim obj As clsRGPBOMItem = Nothing
            For ii As Integer = 0 To gv1.RowCount - 1
                If (clsCommon.myCBool(gv1.Rows(ii).Cells(colDSelect).Value)) Then
                    obj = New clsRGPBOMItem()
                    obj.RGP_No = clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)
                    obj.Item_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDICode).Value)
                    obj.Iname = clsCommon.myCstr(gv1.Rows(ii).Cells(colDIName).Value)
                    obj.Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDRate).Value)
                    obj.Unit_code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDUnit).Value)
                    ''obj.Location = clsCommon.myCstr(gv1.Rows(ii).Cells("Location").Value)
                    ''obj.LocationName = clsCommon.myCstr(gv1.Rows(ii).Cells("LocationName").Value)
                    obj.RGP_Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDPendingQty).Value)
                    obj.Balance_Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDPendingQty).Value)

                    If (obj.RGP_Qty > 0) Then
                        ArrReturn_Job.Add(obj)
                    End If
                End If
            Next

            If ArrReturn_Job.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one non zero Pending RGP item")
            Else
                Me.Close()
            End If
        Else
            Dim obj As clsRGPDetail = Nothing
            For ii As Integer = 0 To gv1.RowCount - 1
                If (clsCommon.myCBool(gv1.Rows(ii).Cells(colDSelect).Value)) Then
                    obj = New clsRGPDetail()
                    obj.RGP_No = clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)
                    obj.Item_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDICode).Value)
                    obj.Item_Desc = clsCommon.myCstr(gv1.Rows(ii).Cells(colDIName).Value)
                    obj.Item_Cost = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDRate).Value)
                    obj.Unit_code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDUnit).Value)
                    ''obj.Location = clsCommon.myCstr(gv1.Rows(ii).Cells("Location").Value)
                    ''obj.LocationName = clsCommon.myCstr(gv1.Rows(ii).Cells("LocationName").Value)
                    obj.RGP_Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDPendingQty).Value)
                    obj.Balance_Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDPendingQty).Value)

                    If (obj.RGP_Qty > 0) Then
                        ArrReturn.Add(obj)
                    End If
                End If
            Next

            If ArrReturn.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one non zero Pending RGP item")
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
        If Not IsInsideLoadData Then
            If gvHead.CurrentColumn Is gvHead.Columns(colHSelect) Then
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
                    common.clsCommon.MyMessageBoxShow("RGP's Vendor should be `" + VendorName)
                    e.Cancel = True
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
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDUnit).Value = clsCommon.myCstr(dr("Unit"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDRate).Value = clsCommon.myCdbl(dr("Rate"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDOrderQty).Value = clsCommon.myCdbl(dr("POQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDApprovedQty).Value = clsCommon.myCdbl(dr("GRNQty"))
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
    End Sub
End Class

