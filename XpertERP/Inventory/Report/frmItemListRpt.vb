'--------------created by Monika-------------BM00000003620
Imports common
Imports System.Data.SqlClient
Imports System.IO

Public Class FrmItemListRpt
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ButtonToolTip As New ToolTip()
    Const colSno As String = "colSno"
    Const colStructureCode As String = "colStructureCode"
    Const colItemCode As String = "colItemCode"
    Const colItemName As String = "colItemName"
    Const colWeightUOM As String = "colWeightUOM"
    Const colWeightValue As String = "colWeightValue"
    Const colStockingUOM As String = "colStockingUOM"
    Const colStockingConversion As String = "colStockingConversion"
    Const colDefaultUOM As String = "colDefaultUOM"
    Const colDefaultConversion As String = "colDefaultConversion"
    Const colWeightUOM1 As String = "colWeightUOM1"
    Const colWeightConversion1 As String = "colWeightConversion1"
    Const colWeightUOM2 As String = "colWeightUOM2"
    Const colWeightConversion2 As String = "colWeightConversion2"
    Const colOtherUOM1 As String = "colOtherUOM1"
    Const colOtherConversion1 As String = "colOtherConversion1"
    Const colOtherUOM2 As String = "colOtherUOM2"
    Const colOtherConversion2 As String = "colOtherConversion2"
    Const colItemType As String = "colItemType"
    Const colPurchaseAccountSet As String = "colPurchaseAccountSet"
    Const colSaleAccountSet As String = "colSaleAccountSet"
    Const colBatchWise As String = "colBatchWise"
    Const colFresh_Ambient As String = "colFresh_Ambient"
    Const colTaxable As String = "colTaxable"
    Const colMRPWise As String = "colMRPWise"
    Const colFatRate As String = "colFatRate"
    Const colSnfRate As String = "colSnfRate"
    Const colItemCost As String = "colItemCost"
    Const colItemProductType As String = "colItemProductType"
    Const colItemProductSubType As String = "colItemProductSubType"
    Dim isLoadData As Boolean = True
#End Region

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FrmItemListRpt)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnexport.Visible = MyBase.isExport
        btnprint.Visible = MyBase.isPrintFlag
    End Sub

    Private Sub FrmItemListRpt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.R Then
            FunReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P Then
            btnprint.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then ''BHA/30/10/18-000652, by balwinder on 31/10/2018  
            If btnImport.Visible = False OrElse btnUpdate.Visible = False Then
                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SIRC"
                frm.strCode = "SIReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnImport.Visible = True
                    btnUpdate.Visible = True
                End If
            End If
        End If
    End Sub

    Private Sub FrmItemListRpt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        FunReset()
        LoadItems()
        chkUOMWise.Checked = True
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+R for reset window")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C for close window")
        ButtonToolTip.SetToolTip(btnprint, "Press Alt+P for view report")
    End Sub

    Private Sub LoadItems()
        Dim qry As String = "select item_code as Code,item_desc as Description from tspl_item_master"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        cbgItem.DataSource = Nothing
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            cbgItem.DataSource = dt
            cbgItem.DisplayMember = "Description"
            cbgItem.ValueMember = "Code"
        End If
    End Sub

    Private Sub FunReset()
        gv.Columns.Clear()
        gv.DataSource = Nothing
        chkAll.IsChecked = True
        chkitemAll.IsChecked = True
        cbgItem.Enabled = False
        chkUOMWise.Checked = False
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        If chkUOMWise.Checked = True Then
            PageSetupReport_ID = MyBase.Form_ID + "UOM"
        Else
            PageSetupReport_ID = MyBase.Form_ID
        End If
        TemplateGridview = gv
        isLoadData = True
        Print()
        isLoadData = False
    End Sub

    Private Sub Print()
        Try
            If Not chkActive.IsChecked AndAlso Not chkInactive.IsChecked AndAlso Not chkAll.IsChecked Then
                Throw New Exception("Please select any one from Active/In Active or All")
            End If

            If chkSelect.IsChecked AndAlso cbgItem.CheckedValue.Count <= 0 Then
                Throw New Exception("Select atleast one item from list")
            End If

            ' Work done agaist ticket no.BHA/07/09/18-000520 
            'Sanjay Ticket No-BHA/28/08/18-000484 add UOM Wise
            'update by preeti Gupta [Add item Cost][BHA/18/09/18-000557]
            'Ticket No- BHA/01/11/18-000654 Update Sales Account Sets,Purchase account sets,Batchwise
            Dim qry As String
            If chkUOMWise.Checked = True Then
                qry = "select ROW_NUMBER() OVER (ORDER BY tspl_item_master.item_code asc) AS [Sno], " & _
                " tspl_item_master.Structure_Code as [Structure Code] " & _
                " ,tspl_item_master.item_code AS [Item Code],tspl_item_master.Item_Desc as [Item Name] " & _
                  ",Weight_UOM as [Weight UOM],Weight_Value as [Weight Value], item_default.[Stocking UOM] " & _
                ",item_default.[Stocking Conversion],item_default.[Default UOM],item_default.[Default Conversion] " & _
                ",WUOM_outer.[Weight UOM1],WUOM_outer.[Weight Conversion1],WUOM_outer.[Weight UOM2],WUOM_outer.[Weight Conversion2] " & _
                ",OUOM_outer.[Other UOM1],OUOM_outer.[Other Conversion1],OUOM_outer.[Other UOM2],OUOM_outer.[Other Conversion2] " & _
                " , (case when tspl_item_master.Item_Type= 'R' then 'Raw Material' " & _
                " when tspl_item_master.Item_Type= 'S' then 'Semi Finished Good' " & _
                " when tspl_item_master.Item_Type= 'T' then 'Trading Good' " & _
                " when tspl_item_master.Item_Type= 'A' then 'Asset' " & _
                " when tspl_item_master.Item_Type= 'F' then 'Finished Good' " & _
                " when tspl_item_master.Item_Type= 'J' then 'Job Work' " & _
                " when tspl_item_master.Item_Type= 'N' then 'Non-Inventory' " & _
                " when tspl_item_master.Item_Type= 'O' then 'Other' " & _
                " when tspl_item_master.Item_Type= 'P' then 'Packing Material'" & _
                "            Else '' end) as [Item Type]" & _
                ",Purchase_Class_Code as [Purchase Account Set],Sale_Class_Code as [Sale Account Set],(case when is_batch_item=1 then 'Yes' else 'No' end) as [Batch Wise]" & _
                   ",(case when Is_FreshItem=1 then 'Fresh' else (case when Is_Ambient=1 then 'Ambient' else '' end) end) as [Fresh/Ambient] " & _
                   ",(case when IsTaxable=1 then 'Yes' else 'No' end) as [Taxable]" & _
                  ",(case when Is_MRP=1 then 'Yes' else 'No' end) as [MRP Wise],[Fat %] as [Fat Rate],[Snf %] as [SNF Rate] " & _
                  ",item_default.item_cost as [Item Cost],tspl_item_master.product_type as [Product Type],tspl_item_master.Cheapter_Heads as [Product Sub Type]" & _
                 " from tspl_item_master left outer join " & _
                " (select tspl_item_uom_detail.Item_Code " & _
                 ",max(case when tspl_item_uom_detail.Stocking_unit='Y' then tspl_item_uom_detail.uom_code else '' end) [Stocking UOM] " & _
                 ",max(case when tspl_item_uom_detail.Stocking_unit='Y' then tspl_item_uom_detail.Conversion_Factor else 0 end) [Stocking Conversion] " & _
                 ",max(case when tspl_item_uom_detail.Default_UOM=1 then tspl_item_uom_detail.uom_code else '' end) [Default UOM] " & _
                  ",max(case when tspl_item_uom_detail.Default_UOM=1 then tspl_item_uom_detail.Conversion_Factor else 0 end) [Default Conversion] " & _
                  ",max(case when tspl_item_uom_detail.Stocking_unit='Y' then tspl_item_uom_detail.item_cost else 0 end) [item_cost]" & _
                 " from tspl_item_uom_detail where 1=1  "
                If chkSelect.IsChecked Then
                    qry += " and item_code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ")"
                End If
                qry += " group by Item_Code) as item_default " & _
                 " on tspl_item_master.item_code=item_default.Item_Code " & _
                 "  Left Join " & _
                   " (select WUOM.item_code " & _
                  " , max(case when WUOM.ln=1 then WUOM.UOM_Code else '' end) as [Weight UOM1] " & _
                  " ,max(case when WUOM.ln=1 then WUOM.Conversion_factor else 0 end) as [Weight Conversion1] " & _
                   " ,max(case when WUOM.ln=2 then WUOM.UOM_Code else '' end ) as [Weight UOM2] " & _
                    ",max(case when WUOM.ln=2 then WUOM.Conversion_factor else 0 end) as [Weight Conversion2] " & _
                    "  from  ( select " & _
                " ROW_NUMBER() OVER (PARTITION BY tspl_item_uom_detail.item_code ORDER BY tspl_item_uom_detail.item_code) AS ln, " & _
                "  tspl_item_uom_detail.item_code, tspl_item_uom_detail.UOM_Code, tspl_item_uom_detail.Conversion_factor " & _
                  " from tspl_item_uom_detail left join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.unit_code = tspl_item_uom_detail.UOM_code " & _
                "  where 1 = 1 " & _
                 " and TSPL_UNIT_MASTER.Weight_Type='Y'" & _
                 " and tspl_item_uom_detail.Stocking_unit='N'" & _
                 " and tspl_item_uom_detail.Default_UOM=0"
                If chkSelect.IsChecked Then
                    qry += " and item_code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ")"
                End If

                qry += " )WUOM group by item_code )WUOM_outer on WUOM_outer.item_code = tspl_item_master.item_code " & _
                    "  Left Join " & _
                   " (select OUOM.item_code" & _
                  ", max(case when OUOM.ln=1 then OUOM.UOM_Code else '' end) as [Other UOM1] " & _
                   ",max(case when OUOM.ln=1 then OUOM.Conversion_factor else 0 end) as [Other Conversion1] " & _
                    ",max(case when OUOM.ln=2 then OUOM.UOM_Code else '' end ) as [Other UOM2] " & _
                    ",max(case when OUOM.ln=2 then OUOM.Conversion_factor else 0 end) as [Other Conversion2] " & _
                 " from  ( select " & _
                 " ROW_NUMBER() OVER (PARTITION BY tspl_item_uom_detail.item_code ORDER BY tspl_item_uom_detail.item_code) AS ln, " & _
                " tspl_item_uom_detail.item_code, tspl_item_uom_detail.UOM_Code, tspl_item_uom_detail.Conversion_factor " & _
                " from tspl_item_uom_detail " & _
                 " left join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.unit_code = tspl_item_uom_detail.UOM_code" & _
                 " where 1 = 1 " & _
                 " and TSPL_UNIT_MASTER.Weight_Type='N' " & _
                 " and tspl_item_uom_detail.Stocking_unit='N' " & _
                 " and tspl_item_uom_detail.Default_UOM=0 "

                If chkSelect.IsChecked Then
                    qry += " and item_code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ")"
                End If

                qry += " )OUOM group by item_code )OUOM_outer on OUOM_outer.item_code = tspl_item_master.item_code "
                ' Work done agaist ticket no.BHA/07/09/18-000520 
                qry += "   left join (select * from (select TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code,TSPL_ITEM_QC_PARAMETER_MASTER.StandardRate,TSPL_PARAMETER_MASTER.Description from TSPL_ITEM_QC_PARAMETER_MASTER left join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where 1=1 "
                If chkSelect.IsChecked Then
                    qry += " and item_code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ")"
                End If
                qry += " )aaa"
                qry += " pivot (sum(StandardRate) for Description in ([Fat %],[SNF %])) as piot"
                qry += " )QCITem on QCITem.Item_Code=TSPL_ITEM_MASTER.Item_Code"
                qry += " where 1=1 "
                '' End
            Else

                qry = " select Item_Code as [Code] ,Item_Desc as [Item Description] ,Structure_Code as [Structure Code] ,Structure_Desc as [Structure Description] ,Purchase_Class_Code as [Purchase Class Code] ,Sale_Class_Code as [Sale Class Code] ,Unit_Code as [Unit Code] ,Deafult_Price as [Deafult Price] ,Father_Code as [Father Code] ,Father_QTy as [Father Quantity] ,Cheapter_Heads as [Cheapter Heads] ,Mother_Code as [Mother Code] ,Mother_Qty as [Mother Quantity] ,Opening_Balance as [Opening Balance] ,Two_Count_Status as [Two Count Status] ,Three_Count_Status as [Three Count Status] ,Server_Type as [Server Type] ,Mfg_Date as [Manufacturing Date] ,Batch_No as [Batch No] ,Best_Befor_UseDate as [Best Befor Use Date] ,Item_Type as [Item Type] ,Created_By as [Created By] ,Created_Date as [Created Date] ,Modify_By as [Modify By] ,Modify_Date as [Modify Date] ,Comp_Code as [Company Code] ,Flavour_Seq as [Flavour Sequence] ,Pack_Seq as [Pack Sequence] ,Sku_Seq as [SKU Sequence] ,show as [Show] ,item_category as [Item Category] ,Tolerence as [Tolerence] ,tech_shelf_life as [Tech Shelf Life] ,Cost as [Cost] ,Sub_item_category as [Item Sub Category] ,TypeOfItm as [Type Of Item] ,NoMRP as [No MRP] ,Morning as [Morning] ,PROD_ITEM_CATEGORY_CODE as [Prod Item Category Code] ,Rate as [Rate] ,Active as [Active] ,AlternativeItem as [Alternative Item] ,ItemSpecification as [Item Specification] ,Item_Category_Struct_Code as [Item Category Structure Code] ,SubItemType as [item Sub Type] ,Is_Serial_Item as [Is Serial Item] ,Serial_Counter as [Serial Counter] ,WARRANTY_CODE as [Warranty Code] ,Is_Pick_Auto_SrNo as [Is Pick Auto Srno] ,Weight_UOM as [Weight UOM] ,Weight_Value as [Weight Value] ,Asset_Life as [Asset Life] ,Warranty_Period as [Warranty Period] ,Is_MRP as [Is MRP] ,ITF_CODE as [ITF Code],Is_FreshItem as [Fresh Item],Product_type as [Product Type],Cheapter_Heads as [Product Sub Type] From tspl_item_master where 2=2"
            End If
            If chkActive.IsChecked Then
                qry += " and isnull(tspl_item_master.Active,0)=1"
            ElseIf chkInactive.IsChecked Then
                qry += " and isnull(tspl_item_master.Active,0)=0"
            End If
            If chkSelect.IsChecked Then
                qry += " and tspl_item_master.item_code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ")"
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)


            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                If chkUOMWise.Checked = True Then
                    FormatGridUOM()

                    For Each row As DataRow In dt.Rows
                        'objTr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                        'objTr.Weight_UOM = clsCommon.myCstr(grow.Cells(colWeightUOM).Value)
                        'objTr.Weight_Value = clsCommon.myCdbl(grow.Cells(colWeightValue).Value)
                        'objTr.Stocking_UOM = clsCommon.myCstr(grow.Cells(colStockingUOM).Value)
                        'objTr.Stocking_Conversion = clsCommon.myCdbl(grow.Cells(colStockingConversion).Value)
                        'objTr.Default_UOM = clsCommon.myCstr(grow.Cells(colDefaultUOM).Value)
                        'objTr.Default_Conversion = clsCommon.myCdbl(grow.Cells(colDefaultConversion).Value)
                        'objTr.Weight_UOM1 = clsCommon.myCstr(grow.Cells(colWeightUOM1).Value)
                        'objTr.Weight_Conversion1 = clsCommon.myCdbl(grow.Cells(colWeightConversion1).Value)
                        'objTr.Weight_UOM2 = clsCommon.myCstr(grow.Cells(colWeightUOM2).Value)
                        'objTr.Weight_Conversion2 = clsCommon.myCdbl(grow.Cells(colWeightConversion2).Value)
                        'objTr.Other_UOM1 = clsCommon.myCstr(grow.Cells(colOtherUOM1).Value)
                        'objTr.Other_Conversion1 = clsCommon.myCdbl(grow.Cells(colOtherConversion1).Value)
                        'objTr.Other_UOM2 = clsCommon.myCstr(grow.Cells(colOtherUOM2).Value)
                        'objTr.Other_Conversion2 = clsCommon.myCdbl(grow.Cells(colOtherConversion2).Value)


                        gv.Rows.AddNew()
                        gv.Rows(gv.Rows.Count - 1).Cells(colSno).Value = clsCommon.myCdbl(row("Sno").ToString())
                        gv.Rows(gv.Rows.Count - 1).Cells(colStructureCode).Value = clsCommon.myCstr(row("Structure Code").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colItemCode).Value = clsCommon.myCstr(row("Item Code").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colItemName).Value = clsCommon.myCstr(row("Item Name").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colWeightUOM).Value = clsCommon.myCstr(row("Weight UOM").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colWeightValue).Value = clsCommon.myCdbl(row("Weight Value").ToString())

                        gv.Rows(gv.Rows.Count - 1).Cells(colStockingUOM).Value = clsCommon.myCstr(row("Stocking UOM").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colStockingConversion).Value = clsCommon.myCdbl(row("Stocking Conversion").ToString())

                        gv.Rows(gv.Rows.Count - 1).Cells(colDefaultUOM).Value = clsCommon.myCstr(row("Default UOM").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colDefaultConversion).Value = clsCommon.myCdbl(row("Default Conversion").ToString())

                        gv.Rows(gv.Rows.Count - 1).Cells(colWeightUOM1).Value = clsCommon.myCstr(row("Weight UOM1").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colWeightConversion1).Value = clsCommon.myCdbl(row("Weight Conversion1").ToString())

                        gv.Rows(gv.Rows.Count - 1).Cells(colWeightUOM2).Value = clsCommon.myCstr(row("Weight UOM2").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colWeightConversion2).Value = clsCommon.myCdbl(row("Weight Conversion2").ToString())

                        gv.Rows(gv.Rows.Count - 1).Cells(colOtherUOM1).Value = clsCommon.myCstr(row("Other UOM1").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colOtherConversion1).Value = clsCommon.myCdbl(row("Other Conversion1").ToString())

                        gv.Rows(gv.Rows.Count - 1).Cells(colOtherUOM2).Value = clsCommon.myCstr(row("Other UOM2").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colOtherConversion2).Value = clsCommon.myCdbl(row("Other Conversion2").ToString())

                        gv.Rows(gv.Rows.Count - 1).Cells(colItemType).Value = clsCommon.myCstr(row("Item Type").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colPurchaseAccountSet).Value = clsCommon.myCstr(row("Purchase Account Set").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colSaleAccountSet).Value = clsCommon.myCstr(row("Sale Account Set").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colBatchWise).Value = clsCommon.myCstr(row("Batch Wise").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colFresh_Ambient).Value = clsCommon.myCstr(row("Fresh/Ambient").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxable).Value = clsCommon.myCstr(row("Taxable").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colMRPWise).Value = clsCommon.myCstr(row("MRP Wise").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colFatRate).Value = clsCommon.myCdbl(row("Fat Rate").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colSnfRate).Value = clsCommon.myCdbl(row("Snf Rate").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colItemCost).Value = clsCommon.myCdbl(row("Item Cost").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colItemProductType).Value = clsCommon.myCstr(row("Product Type").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colItemProductSubType).Value = clsCommon.myCstr(row("Product Sub Type").ToString().Trim())
                    Next

                Else
                    gv.DataSource = dt
                    FormatGrid()
                End If
                ReStoreGridLayout()

                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                Throw New Exception("No Data Found.")
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            Dim TempFormId As String = ""
            If chkUOMWise.Checked = True Then
                TempFormId = Form_ID + "UOM"
            Else
                TempFormId = Form_ID
            End If
            If clsCommon.myLen(TempFormId) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(TempFormId, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv.Columns.Count - 1 Step ii + 1
                        gv.Columns(ii).IsVisible = False
                        gv.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub FormatGrid()
        ' Dim strItemCode, head2 As String

        gv.TableElement.TableHeaderHeight = 40
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = True
            gv.Columns(ii).Width = 100
        Next
        'If chkUOMWise.Checked = True Then
        '    gv.Columns(3).Width = 250
        '    FormatGridUOM()
        'Else
        gv.Columns(1).Width = 250
        'End If

    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        FunReset()
    End Sub

    Private Sub btnexcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexcel.Click
        Print()

        If gv.Rows.Count > 0 Then
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : Item List Report")
            transportSql.applyExportTemplate(gv, PageSetupReport_ID)
            clsCommon.MyExportToExcelGrid("Item List", gv, arrHeader, "Item List Report")
        End If
    End Sub

    Private Sub btnpdf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpdf.Click
        Print()
        If gv.Rows.Count > 0 Then
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : Item List Report")
            transportSql.applyExportTemplate(gv, PageSetupReport_ID)
            clsCommon.MyExportToPDF("Item List", gv, arrHeader, "Item List Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
        End If
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub chkitemAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkitemAll.ToggleStateChanged, chkSelect.ToggleStateChanged
        cbgItem.Enabled = chkSelect.IsChecked
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        'Dim ReportID As String = MyBase.Form_ID
        Dim TempFormId As String = ""
        If chkUOMWise.Checked = True Then
            TempFormId = Form_ID + "UOM"
        Else
            TempFormId = Form_ID
        End If
        If clsCommon.myLen(TempFormId) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = TempFormId
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If

            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        Dim TempFormId As String = ""
        If chkUOMWise.Checked = True Then
            TempFormId = Form_ID + "UOM"
        Else
            TempFormId = Form_ID
        End If
        clsGridLayout.DeleteData(TempFormId, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub

    Private Sub FormatGridUOMImport()
        gv.Columns("Sno").HeaderText = "Sno"
        gv.Columns("Sno").Width = 50
        'gv.Columns("Sno").WrapText = True
        gv.Columns("Sno").ReadOnly = True
        gv.Columns("Sno").Name = colSno

        gv.Columns("Structure Code").HeaderText = "Structure Code"
        gv.Columns("Structure Code").Width = 100
        'gv.Columns("Structure Code").WrapText = True
        gv.Columns("Structure Code").ReadOnly = True
        gv.Columns("Structure Code").Name = colStructureCode

        gv.Columns("Item Code").HeaderText = "Item Code"
        gv.Columns("Item Code").Width = 100
        'gv.Columns("Item Code").WrapText = True
        gv.Columns("Item Code").ReadOnly = True
        gv.Columns("Item Code").Name = colItemCode

        gv.Columns("Item Name").HeaderText = "Item Name"
        gv.Columns("Item Name").Width = 250
        'gv.Columns("Item Name").WrapText = True
        gv.Columns("Item Name").ReadOnly = True
        gv.Columns("Item Name").Name = colItemName

        gv.Columns("Weight UOM").HeaderText = "Weight UOM"
        gv.Columns("Weight UOM").Width = 100
        'gv.Columns("Weight UOM").WrapText = True
        gv.Columns("Weight UOM").ReadOnly = True
        gv.Columns("Weight UOM").Name = colWeightUOM

        gv.Columns("Weight Value").HeaderText = "Weight Value"
        gv.Columns("Weight Value").Width = 100
        'gv.Columns("Weight Value").WrapText = True
        gv.Columns("Weight Value").ReadOnly = True
        gv.Columns("Weight Value").Name = colWeightValue
        'gv.Columns("Weight Value").FormatString = "{0:n2}"

        gv.Columns("Stocking UOM").HeaderText = "Stocking UOM"
        gv.Columns("Stocking UOM").Width = 100
        'gv.Columns("Stocking UOM").WrapText = True
        gv.Columns("Stocking UOM").ReadOnly = True
        gv.Columns("Stocking UOM").Name = colStockingUOM

        gv.Columns("Stocking Conversion").HeaderText = "Stocking Conversion"
        gv.Columns("Stocking Conversion").Width = 100
        'gv.Columns("Stocking Conversion").WrapText = True
        gv.Columns("Stocking Conversion").ReadOnly = True
        gv.Columns("Stocking Conversion").Name = colStockingConversion
        'gv.Columns("Stocking Conversion").FormatString = "{0:n6}"

        gv.Columns("Default UOM").HeaderText = "Default UOM"
        gv.Columns("Default UOM").Width = 100
        'gv.Columns("Default UOM").WrapText = True
        gv.Columns("Default UOM").ReadOnly = True
        gv.Columns("Default UOM").Name = colDefaultUOM

        gv.Columns("Default Conversion").HeaderText = "Default Conversion"
        gv.Columns("Default Conversion").Width = 100
        'gv.Columns("Default Conversion").WrapText = True
        gv.Columns("Default Conversion").ReadOnly = True
        gv.Columns("Default Conversion").Name = colDefaultConversion
        'gv.Columns("Default Conversion").FormatString = "{0:n6}"

        gv.Columns("Weight UOM1").HeaderText = "Weight UOM1"
        gv.Columns("Weight UOM1").Width = 100
        'gv.Columns("Weight UOM1").WrapText = True
        gv.Columns("Weight UOM1").ReadOnly = True
        gv.Columns("Weight UOM1").Name = colWeightUOM1

        gv.Columns("Weight Conversion1").HeaderText = "Weight Conversion1"
        gv.Columns("Weight Conversion1").Width = 100
        'gv.Columns("Weight Conversion1").WrapText = True
        gv.Columns("Weight Conversion1").ReadOnly = True
        gv.Columns("Weight Conversion1").Name = colWeightConversion1
        'gv.Columns("Weight Conversion1").FormatString = "{0:n6}"

        gv.Columns("Weight UOM2").HeaderText = "Weight UOM2"
        gv.Columns("Weight UOM2").Width = 100
        'gv.Columns("Weight UOM2").WrapText = True
        gv.Columns("Weight UOM2").ReadOnly = True
        gv.Columns("Weight UOM2").Name = colWeightUOM2

        gv.Columns("Weight Conversion2").HeaderText = "Weight Conversion2"
        gv.Columns("Weight Conversion2").Width = 100
        'gv.Columns("Weight Conversion2").WrapText = True
        gv.Columns("Weight Conversion2").ReadOnly = True
        gv.Columns("Weight Conversion2").Name = colWeightConversion2
        'gv.Columns("Weight Conversion2").FormatString = "{0:n6}"

        gv.Columns("Other UOM1").HeaderText = "Other UOM1"
        gv.Columns("Other UOM1").Width = 100
        'gv.Columns("Other UOM1").WrapText = True
        gv.Columns("Other UOM1").ReadOnly = True
        gv.Columns("Other UOM1").Name = colOtherUOM1

        gv.Columns("Other Conversion1").HeaderText = "Other Conversion1"
        gv.Columns("Other Conversion1").Width = 100
        'gv.Columns("Other Conversion1").WrapText = True
        gv.Columns("Other Conversion1").ReadOnly = True
        gv.Columns("Other Conversion1").Name = colOtherConversion1
        'gv.Columns("Other Conversion1").FormatString = "{0:n6}"

        gv.Columns("Other UOM2").HeaderText = "Other UOM2"
        gv.Columns("Other UOM2").Width = 100
        'gv.Columns("Other UOM2").WrapText = True
        gv.Columns("Other UOM2").ReadOnly = True
        gv.Columns("Other UOM2").Name = colOtherUOM2

        gv.Columns("Other Conversion2").HeaderText = "Other Conversion2"
        gv.Columns("Other Conversion2").Width = 100
        'gv.Columns("Other Conversion2").WrapText = True
        gv.Columns("Other Conversion2").ReadOnly = True
        gv.Columns("Other Conversion2").Name = colOtherConversion2
        'gv.Columns("Other Conversion2").FormatString = "{0:n6}"

        gv.Columns("Item Type").HeaderText = "Item Type"
        gv.Columns("Item Type").Width = 100
        'gv.Columns("Item Type").WrapText = True
        gv.Columns("Item Type").ReadOnly = True
        gv.Columns("Item Type").Name = colItemType

        gv.Columns("Purchase Account Set").HeaderText = "Purchase Account Set"
        gv.Columns("Purchase Account Set").Width = 100
        'gv.Columns("Purchase Account Set").WrapText = True
        gv.Columns("Purchase Account Set").ReadOnly = True
        gv.Columns("Purchase Account Set").Name = colPurchaseAccountSet

        gv.Columns("Sale Account Set").HeaderText = "Sale Account Set"
        gv.Columns("Sale Account Set").Width = 100
        'gv.Columns("Sale Account Set").WrapText = True
        gv.Columns("Sale Account Set").ReadOnly = True
        gv.Columns("Sale Account Set").Name = colSaleAccountSet

        gv.Columns("Batch Wise").HeaderText = "Batch Wise"
        gv.Columns("Batch Wise").Width = 100
        'gv.Columns("Batch Wise").WrapText = True
        gv.Columns("Batch Wise").ReadOnly = True
        gv.Columns("Batch Wise").Name = colBatchWise

        gv.Columns("Fresh/Ambient").HeaderText = "Fresh/Ambient"
        gv.Columns("Fresh/Ambient").Width = 100
        'gv.Columns("Fresh/Ambient").WrapText = True
        gv.Columns("Fresh/Ambient").ReadOnly = True
        gv.Columns("Fresh/Ambient").Name = colFresh_Ambient

        gv.Columns("Taxable").HeaderText = "Taxable"
        gv.Columns("Taxable").Width = 100
        'gv.Columns("Taxable").WrapText = True
        gv.Columns("Taxable").ReadOnly = True
        gv.Columns("Taxable").Name = colTaxable

        gv.Columns("MRP Wise").HeaderText = "MRP Wise"
        gv.Columns("MRP Wise").Width = 100
        'gv.Columns("MRP Wise").WrapText = True
        gv.Columns("MRP Wise").ReadOnly = True
        gv.Columns("MRP Wise").Name = colMRPWise

        gv.Columns("Fat Rate").HeaderText = "Fat Rate"
        gv.Columns("Fat Rate").Width = 100
        gv.Columns("Fat Rate").ReadOnly = True
        gv.Columns("Fat Rate").Name = colFatRate

        gv.Columns("SNF Rate").HeaderText = "SNF Rate"
        gv.Columns("SNF Rate").Width = 100
        gv.Columns("SNF Rate").ReadOnly = True
        gv.Columns("SNF Rate").Name = colSnfRate

        gv.Columns("Item Cost").HeaderText = "Item Cost"
        gv.Columns("Item Cost").Width = 100
        gv.Columns("Item Cost").ReadOnly = True
        gv.Columns("Item Cost").Name = colItemCost

        gv.Columns("Product Type").HeaderText = "Product Type"
        gv.Columns("Product Type").Width = 100
        gv.Columns("Product Type").ReadOnly = True
        gv.Columns("Product Type").Name = colItemCost

        gv.Columns("Product Sub Type").HeaderText = "Product Sub Type"
        gv.Columns("Product Sub Type").Width = 100
        gv.Columns("Product Sub Type").ReadOnly = True
        gv.Columns("Product Sub Type").Name = colItemCost


    End Sub

    Private Sub FormatGridUOM()

        Dim repoLineNo As New GridViewTextBoxColumn()
        Dim repoString As New GridViewTextBoxColumn()
        Dim repoConvFactor As New GridViewDecimalColumn()

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Sno"
        repoLineNo.Name = colSno
        repoLineNo.Width = 50
        'repoLineNo.WrapText = True
        repoLineNo.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoLineNo)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Structure Code"
        repoString.Name = colStructureCode
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Item Code"
        repoString.Name = colItemCode
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Item Name"
        repoString.Name = colItemName
        repoString.Width = 250
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Weight UOM"
        repoString.Name = colWeightUOM
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoConvFactor = New GridViewDecimalColumn()
        repoConvFactor.FormatString = ""
        repoConvFactor.HeaderText = "Weight Value"
        repoConvFactor.Name = colWeightValue
        repoConvFactor.Minimum = 0
        repoConvFactor.Width = 100
        repoConvFactor.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoConvFactor.DecimalPlaces = 2
        repoConvFactor.FormatString = "{0:n2}"
        gv.MasterTemplate.Columns.Add(repoConvFactor)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Stocking UOM"
        repoString.Name = colStockingUOM
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoConvFactor = New GridViewDecimalColumn()
        repoConvFactor.FormatString = ""
        repoConvFactor.HeaderText = "Stocking Conversion"
        repoConvFactor.Name = colStockingConversion
        repoConvFactor.Minimum = 0
        repoConvFactor.Width = 100
        repoConvFactor.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoConvFactor.DecimalPlaces = 6
        repoConvFactor.FormatString = "{0:n6}"
        gv.MasterTemplate.Columns.Add(repoConvFactor)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Default UOM"
        repoString.Name = colDefaultUOM
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoConvFactor = New GridViewDecimalColumn()
        repoConvFactor.FormatString = ""
        repoConvFactor.HeaderText = "Default Conversion"
        repoConvFactor.Name = colDefaultConversion
        repoConvFactor.Minimum = 0
        repoConvFactor.Width = 100
        repoConvFactor.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoConvFactor.DecimalPlaces = 6
        repoConvFactor.FormatString = "{0:n6}"
        gv.MasterTemplate.Columns.Add(repoConvFactor)


        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Weight UOM1"
        repoString.Name = colWeightUOM1
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)


        repoConvFactor = New GridViewDecimalColumn()
        repoConvFactor.FormatString = ""
        repoConvFactor.HeaderText = "Weight Conversion1"
        repoConvFactor.Name = colWeightConversion1
        repoConvFactor.Minimum = 0
        repoConvFactor.Width = 100
        repoConvFactor.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoConvFactor.DecimalPlaces = 6
        repoConvFactor.FormatString = "{0:n6}"
        gv.MasterTemplate.Columns.Add(repoConvFactor)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Weight UOM2"
        repoString.Name = colWeightUOM2
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoConvFactor = New GridViewDecimalColumn()
        repoConvFactor.FormatString = ""
        repoConvFactor.HeaderText = "Weight Conversion2"
        repoConvFactor.Name = colWeightConversion2
        repoConvFactor.Minimum = 0
        repoConvFactor.Width = 100
        repoConvFactor.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoConvFactor.DecimalPlaces = 6
        repoConvFactor.FormatString = "{0:n6}"
        gv.MasterTemplate.Columns.Add(repoConvFactor)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Other UOM1"
        repoString.Name = colOtherUOM1
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoConvFactor = New GridViewDecimalColumn()
        repoConvFactor.FormatString = ""
        repoConvFactor.HeaderText = "Other Conversion1"
        repoConvFactor.Name = colOtherConversion1
        repoConvFactor.Minimum = 0
        repoConvFactor.Width = 100
        repoConvFactor.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoConvFactor.DecimalPlaces = 6
        repoConvFactor.FormatString = "{0:n6}"
        gv.MasterTemplate.Columns.Add(repoConvFactor)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Other UOM2"
        repoString.Name = colOtherUOM2
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoConvFactor = New GridViewDecimalColumn()
        repoConvFactor.FormatString = ""
        repoConvFactor.HeaderText = "Other Conversion2"
        repoConvFactor.Name = colOtherConversion2
        repoConvFactor.Minimum = 0
        repoConvFactor.Width = 100
        repoConvFactor.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoConvFactor.DecimalPlaces = 6
        repoConvFactor.FormatString = "{0:n6}"
        gv.MasterTemplate.Columns.Add(repoConvFactor)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Item Type"
        repoString.Name = colItemType
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Purchase Account Set"
        repoString.Name = colPurchaseAccountSet
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Sale Account Set"
        repoString.Name = colSaleAccountSet
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Batch Wise"
        repoString.Name = colBatchWise
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Fresh/Ambient"
        repoString.Name = colFresh_Ambient
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Taxable"
        repoString.Name = colTaxable
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "MRP Wise"
        repoString.Name = colMRPWise
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Fat Rate"
        repoString.Name = colFatRate
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "SNF Rate"
        repoString.Name = colSnfRate
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Item Cost"
        repoString.Name = colItemCost
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Product Type"
        repoString.Name = colItemProductType
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Product Sub Type"
        repoString.Name = colItemProductSubType
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        UpdateData()
    End Sub

    Function AllowToSave() As Boolean
        Try
            Dim LineNo As String
            Dim TempAc_CodeGrid As String = ""
            Dim TempAc_Code As String = ""
            For Each grow As GridViewRowInfo In gv.Rows
                LineNo = clsCommon.myCstr(grow.Index + 1)
                'clsCommon.myCstr(grow.Cells("Structure_Code").Value)
                Dim Structure_Code As String = ""
                Dim StructureCode As String = clsCommon.myCstr(grow.Cells(colStructureCode).Value)
                If StructureCode <> "" Then
                    Structure_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Structure_Code from TSPL_STRUCTURE_MASTER WHERE Structure_Code='" + StructureCode + "'"))
                    If Not clsCommon.CompairString(Structure_Code, StructureCode) = CompairStringResult.Equal Then
                        Throw New Exception("Structure Code at line '" + LineNo + "' does not exist.")
                    End If
                Else
                    Throw New Exception("Structure Code can not be blank at line '" + LineNo + "'.")
                End If

                'Purchase Account set
                TempAc_CodeGrid = clsCommon.myCstr(grow.Cells(colPurchaseAccountSet).Value)
                If TempAc_CodeGrid <> "" Then
                    TempAc_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Purchase_Class_Code from TSPL_PURCHASE_ACCOUNTS WHERE Purchase_Class_Code='" + TempAc_CodeGrid + "'"))
                    If Not clsCommon.CompairString(TempAc_Code, TempAc_CodeGrid) = CompairStringResult.Equal Then
                        Throw New Exception("Purchase Account set at line '" + LineNo + "' does not exist.")
                    End If
                Else
                    Throw New Exception("Purchase Account set can not be blank at line '" + LineNo + "'.")
                End If

                'Sale Account set
                TempAc_CodeGrid = clsCommon.myCstr(grow.Cells(colSaleAccountSet).Value)
                If TempAc_CodeGrid <> "" Then
                    TempAc_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Sales_Class_Code from TSPL_SALES_ACCOUNTS WHERE Sales_Class_Code='" + TempAc_CodeGrid + "'"))
                    If Not clsCommon.CompairString(TempAc_Code, TempAc_CodeGrid) = CompairStringResult.Equal Then
                        Throw New Exception("Sale Account set at line '" + LineNo + "' does not exist.")
                    End If
                Else
                    Throw New Exception("Sale Account set can not be blank at line '" + LineNo + "'.")
                End If

            Next
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function

    Sub UpdateData()
        Try
            If (AllowToSave()) Then
                Dim qry As String = ""
                Dim obj As New clsItemUpdateMaster()
                obj.ArrTr = New List(Of clsItemUpdateDetail)
                For Each grow As GridViewRowInfo In gv.Rows
                    If (clsCommon.myLen(grow.Cells(colItemCode).Value) > 0) Then
                        Dim objTr As New clsItemUpdateDetail()
                        objTr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                        objTr.Structure_Code = clsCommon.myCstr(grow.Cells(colStructureCode).Value)
                        objTr.Weight_UOM = clsCommon.myCstr(grow.Cells(colWeightUOM).Value)
                        objTr.Weight_Value = clsCommon.myCdbl(grow.Cells(colWeightValue).Value)
                        objTr.Stocking_UOM = clsCommon.myCstr(grow.Cells(colStockingUOM).Value)
                        objTr.Stocking_Conversion = clsCommon.myCdbl(grow.Cells(colStockingConversion).Value)
                        objTr.Default_UOM = clsCommon.myCstr(grow.Cells(colDefaultUOM).Value)
                        objTr.Default_Conversion = clsCommon.myCdbl(grow.Cells(colDefaultConversion).Value)
                        objTr.Weight_UOM1 = clsCommon.myCstr(grow.Cells(colWeightUOM1).Value)
                        objTr.Weight_Conversion1 = clsCommon.myCdbl(grow.Cells(colWeightConversion1).Value)
                        objTr.Weight_UOM2 = clsCommon.myCstr(grow.Cells(colWeightUOM2).Value)
                        objTr.Weight_Conversion2 = clsCommon.myCdbl(grow.Cells(colWeightConversion2).Value)
                        objTr.Other_UOM1 = clsCommon.myCstr(grow.Cells(colOtherUOM1).Value)
                        objTr.Other_Conversion1 = clsCommon.myCdbl(grow.Cells(colOtherConversion1).Value)
                        objTr.Other_UOM2 = clsCommon.myCstr(grow.Cells(colOtherUOM2).Value)
                        objTr.Other_Conversion2 = clsCommon.myCdbl(grow.Cells(colOtherConversion2).Value)
                        objTr.SNfRate = clsCommon.myCdbl(grow.Cells(colSnfRate).Value)
                        objTr.FatRate = clsCommon.myCdbl(grow.Cells(colFatRate).Value)
                        objTr.Item_cost = clsCommon.myCdbl(grow.Cells(colItemCost).Value)
                        objTr.Purchase_Account_Set = clsCommon.myCstr(grow.Cells(colPurchaseAccountSet).Value)
                        objTr.Sale_Account_Set = clsCommon.myCstr(grow.Cells(colSaleAccountSet).Value)
                        objTr.Batch_Wise = clsCommon.myCdbl(IIf(clsCommon.myCstr(grow.Cells(colBatchWise).Value) = "Yes", 1, 0))
                        objTr.Product_Type = clsCommon.myCstr(grow.Cells(colItemProductType).Value)
                        objTr.Cheapter_Heads = clsCommon.myCstr(grow.Cells(colItemProductSubType).Value)
                        If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                            obj.ArrTr.Add(objTr)
                        End If
                    End If
                Next

                If (obj.ArrTr Is Nothing OrElse obj.ArrTr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Fill at list one Item", Me.Text)
                    Return
                End If


                If (obj.Update(obj)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Update Successfully", Me.Text)
                    'Print()
                    FunReset()
                End If

            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
    End Sub


    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        Try
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()

            If transportSql.importExcel(gv, "Sno", "Structure Code", "Item Code", "Item Name", "Weight UOM", "Weight Value", "Stocking UOM", "Stocking Conversion", "Default UOM", "Default Conversion", "Weight UOM1", "Weight Conversion1", "Weight UOM2", "Weight Conversion2", "Other UOM1", "Other Conversion1", "Other UOM2", "Other Conversion2", "Item Type", "Purchase Account Set", "Sale Account Set", "Batch Wise", "Fresh/Ambient", "Taxable", "MRP Wise", "Fat Rate", "SNF Rate", "Item Cost", "Product Type", "Product Sub Type") Then
                clsCommon.ProgressBarPercentShow()

                ''do sorting of records for easy saving purpose.
                Dim dt As New DataTable()
                dt = gv.DataSource()
                dt.DefaultView.Sort = "Sno"
                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                gv.DataSource = dt.DefaultView.ToTable()
                ''======================end here========================

                FormatGridUOMImport()
                RadPageView1.SelectedPage = RadPageViewPage2
                clsCommon.ProgressBarPercentHide()
                clsCommon.MyMessageBoxShow(Me, "Data Transfered Successfully.", Me.Text)

            End If
        Catch ex As Exception
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            clsCommon.ProgressBarPercentHide()
        End Try
    End Sub

    Private Sub gv_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv.CellDoubleClick
        Try
            If e.Column Is gv.Columns(colItemCode) OrElse e.Column Is gv.Columns(colItemName) Then 'Ticket No-TEC/19/07/19-000947 ,Sanjay
                Dim itemcode As String = ""
                itemcode = clsCommon.myCstr(gv.CurrentRow.Cells(colItemCode).Value)
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmItemMasterRMOther, itemcode)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv.CellValueChanged
        If isLoadData = False Then
            If e.Column Is gv.Columns(colItemProductType) Then
                OpenProductType(False)
            ElseIf e.Column Is gv.Columns(colItemProductSubType) Then
                OpenProductSubType(False)
            End If
        End If
    End Sub

    Sub OpenProductType(ByVal isButtonClick As Boolean)
        Dim qry As String = "Select Final.Code ,Final.Description From ( Select '' as Code , 'Other'  as Description union all  Select 'MI' as Code , 'Milk'  as Description union all  Select 'MB' as Code , 'Melted Butter'  as Description union all  Select 'CH' as Code , 'Cheese'  as Description union all  Select 'CU' as Code , 'Curd' as Description union all Select 'CA' as Code , 'Cream'  as Description union all  Select 'DG' as Code , 'Desi-Ghee'  as Description union all  Select 'BU' as Code , 'Butter'  as Description union all  Select 'BM' as Code , 'Butter Milk'  as Description union all  Select 'PS' as Code , 'Paper Seal'  as Description union all  Select 'MS' as Code , 'Manual Seal' as Description union all  Select 'MP' as Code , 'Milk Product' as Description ) Final  "
        Dim whrCls As String = "  "
        gv.CurrentRow.Cells(colItemProductType).Value = clsCommon.ShowSelectForm("ItemListProductType@Finder", qry, "Code", whrCls, clsCommon.myCstr(gv.CurrentRow.Cells(colItemProductType).Value), "Code", isButtonClick)
    End Sub
    Sub OpenProductSubType(ByVal isButtonClick As Boolean)
        Dim qry As String = "select Chapter_Head_Code as Code,Description from TSPL_CHAPTER_HEAD  "
        Dim whrCls As String = "  "
        gv.CurrentRow.Cells(colItemProductSubType).Value = clsCommon.ShowSelectForm("ItemListProductSubType@Finder", qry, "Code", whrCls, clsCommon.myCstr(gv.CurrentRow.Cells(colItemProductSubType).Value), "Code", isButtonClick)
    End Sub

End Class
