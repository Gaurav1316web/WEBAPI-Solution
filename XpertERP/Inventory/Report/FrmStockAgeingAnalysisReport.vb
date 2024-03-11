Imports common
Imports System.Threading
Imports Telerik.WinControls.UI.Export
Imports Telerik.WinControls.UI.Export.ExcelML
Imports System.IO
Imports Microsoft.Office.Interop


Public Class FrmStockAgeingAnalysisReport
    Inherits FrmMainTranScreen
#Region "Varaibels"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public dtFrom As Date
    Public dtTo As Date
    Public strType As String
    Public arrItemGroup As ArrayList
    Public arrItem As ArrayList
    Public arrItemType As ArrayList
    Dim MIS_Item_Group As String
    Dim dtCategory As DataTable
#End Region

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub FrmKPIReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadCategory()
        GetMIS_ITem_GroupColumn()
        txtFromDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        txtToDate.Value = clsCommon.GETSERVERDATE
        ButtonToolTip.SetToolTip(RadButton2, "Press Alt+C Close the Window")
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub SetUserMgmtNew()

        ' MyBase.SetUserMgmt(clsUserMgtCode.stockRecoBatch)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If

        btnExport.Visible = MyBase.isExport
        btnQuickExport.Visible = MyBase.isExport

    End Sub
    Sub LoadCategory()
        dtCategory = New DataTable()
        Dim strQRY As String = "select ITEM_CATEGORY_CODE AS CodeColumn,ITEM_CATEGORY_CODE+'DESC' as CodeDescColumn,DESCRIPTION as DescColumn  from TSPL_ITEM_CATEGORY_LEVEL order by CATEGORY_LEVEL"
        dtCategory = clsDBFuncationality.GetDataTable(strQRY)
    End Sub


    Private Sub FrmKPIReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then

        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton2.Click
        Me.Close()
    End Sub

    Function GetMIS_ITem_GroupColumn() As String
        Dim qry As String = ""
        qry = " select MAP.Custom_Field_Code from TSPL_CUSTOM_FIELD_MAPPING MAP " & _
            " left join TSPL_CUSTOM_FIELD_HEAD CF on MAP.Custom_Field_Code=CF.Code " & _
            " where CF.Name='MIS Item Group' and MAP.PROGRAM_CODE='" & clsUserMgtCode.itemStructure & "'"
        MIS_Item_Group = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        Return MIS_Item_Group
    End Function

    Private Sub LoadData(ByVal isPrintCrystalReport As Integer)
        Try
            gv1.DataSource = Nothing
            gv1.Columns.Clear()
            gv1.Rows.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.ShowGroupPanel = False
            gv1.EnableFiltering = True


            clsCommon.ProgressBarShow()

            Dim strCodeColumn As String = ""
            Dim strCodeColumnMax As String = ""
            Dim strCodeDescColumn As String = ""
            Dim strCodeDescColumnMax As String = ""

            Dim strCodeColumnSelect As String = ""
            Dim strCodeDescColumnSelect As String = ""

            Dim strCodeColumnNull As String = ""
            Dim strCodeDescColumnNull As String = ""

            Dim strCategoryTable As String = ""
            If dtCategory IsNot Nothing AndAlso dtCategory.Rows.Count > 0 Then
                For ii As Integer = 0 To dtCategory.Rows.Count - 1
                    If ii <> 0 Then
                        strCodeColumn += ","
                        strCodeColumnMax += ","
                        strCodeDescColumn += ","
                        strCodeDescColumnMax += ","

                        strCodeColumnSelect += ","
                        strCodeDescColumnSelect += ","

                        strCodeColumnNull += ","
                        strCodeDescColumnNull += ","
                    End If
                    strCodeColumn += "[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                    strCodeColumnMax += "max([" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]) as [" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                    strCodeDescColumn += "[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")) + "]"
                    strCodeDescColumnMax += "max([" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")).Trim() + "]) as [" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")).Trim() + "]"

                    strCodeColumnSelect += "VirtualCategoryTabel.[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                    strCodeDescColumnSelect += "VirtualCategoryTabel.[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")) + "]"

                    strCodeColumnNull += "null as [" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                    strCodeDescColumnNull += "null as [" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")) + "]"
                Next
                strCategoryTable = "select Item_Code," + strCodeColumnMax + "," + strCodeDescColumnMax + "   from (" + Environment.NewLine & _
                " select * from ( " + Environment.NewLine & _
                " select TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code " + Environment.NewLine & _
                " ,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code+'DESC' as Item_Category_CodeDesc " + Environment.NewLine & _
                " ,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values " + Environment.NewLine & _
                " ,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as Category_Value_Desc " + Environment.NewLine & _
                " from  TSPL_ITEM_MASTER  " + Environment.NewLine & _
                " left outer join TSPL_ITEM_MASTER_CATEGORY on  TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code " + Environment.NewLine & _
                " left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values" + Environment.NewLine & _
                " where 2=2 " + Environment.NewLine & _
                " )xx" + Environment.NewLine & _
                " Pivot " + Environment.NewLine & _
                " ( max(Item_Cagetory_Values) for Item_Category_Code   in ( " + strCodeColumn + ")" + Environment.NewLine & _
                " ) Pivt" + Environment.NewLine & _
                " Pivot " + Environment.NewLine & _
                " (" + Environment.NewLine & _
                " max(Category_Value_Desc) for Item_Category_CodeDesc in (" + strCodeDescColumn + ")" + Environment.NewLine & _
                " ) Pivt1 " + Environment.NewLine & _
                " ) xxx group by Item_Code "

                ''End of Category Table start now.
            End If
            ''Virtual Category Table start now.
            '' query for structure and item group custom field
            Dim strItemGroup As String = ""
            strItemGroup = " select Struct.Structure_Code,Structure_Descq,Struct_Val.Value as Item_Group,StructDtl.Description as Group_Description from TSPL_STRUCTURE_MASTER Struct left join (" & _
                           " select Custom_field_Code,Transaction_code,Value from TSPL_CUSTOM_FIELD_VALUES where Program_Code='" & clsUserMgtCode.itemStructure & "'  " & _
                           " and Custom_Field_Code='" & MIS_Item_Group & "') as Struct_Val  on Struct.Structure_Code=Struct_Val.Transaction_Code" & _
                           " left join (select Custom_Field_Code,SNo,Value,Description from TSPL_CUSTOM_FIELD_DETAIL where Custom_Field_Code='" & MIS_Item_Group & "') as StructDtl on Struct_Val.Value=StructDtl.Value "


            ''Base Query start Now
            Dim qry As String = "select * from ( select TSPL_ITEM_MASTER.tech_shelf_life,InventroyMovement.Trans_Id,InventroyMovement.Trans_Type,TSPL_INVENTORY_SOURCE_CODE.Name as Trans_Type_Name,InventroyMovement.Source_Doc_No,InventroyMovement.Punching_Date, InventroyMovement.InOut,case when InventroyMovement.InOut='I' then 'In' else case when InventroyMovement.InOut='O' then 'Out' else '' end end as 'InOutView',"
            qry += " case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end as Main_Location_Code,MainLocationTable.Location_Desc as MainLocationDesc, InventroyMovement.Location_Code,TSPL_LOCATION_MASTER.Location_Desc AS [Loc Desp],TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then ''  else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_LOCATION_MASTER.Pin_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_LOCATION_MASTER.Pin_Code) End End End  as [LocAddress],SourceCode,SourceName,SourceType  ,Item_Group.Item_Group,Item_Group.Group_Description, InventroyMovement.Item_Code, InventroyMovement.Batch_No,InventroyMovement.Manufacture_Date,InventroyMovement.Expiry_Date, InventroyMovement.MRP ,TSPL_ITEM_MASTER.Item_Desc,tspl_item_master.itf_code,"
            qry += " IsFromMilk,MilkFATKG,MilkSNFKG,case when IsFromMilk=1 then MilkFatPer else isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) end as MilkFatPer,case when IsFromMilk=1 then MilkSNFPer else isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) end as MilkSNFPer,TSPL_LOCATION_MASTER.Is_Section,TSPL_LOCATION_MASTER.Is_Sub_Location,"
            qry += " isnull((InventroyMovement.Stock_Qty * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end))  ,0) as QtyKG,"
            'If clsCommon.myLen(cmbUnit.SelectedValue) > 0 Then
            '    qry += " '" + clsCommon.myCstr(cmbUnit.SelectedValue) + "' as Stock_UOM,(InventroyMovement.Stock_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as Stock_Qty,"
            '    qry += " isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end) * (TSPL_ITEM_UOM_DETAIL.Conversion_Factor)) from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) as FatPer,"
            '    qry += " isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end) * (TSPL_ITEM_UOM_DETAIL.Conversion_Factor)) from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) as SNFPer,"
            'Else
            qry += " InventroyMovement.Stock_UOM,InventroyMovement.Stock_Qty,"
            qry += " isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)) from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) as FatPer,"
            qry += " isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)) from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) as SNFPer,"
            'End If
            qry += " (case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=3 then FIFO_Cost else case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=2 then LIFO_Cost else Avg_Cost end end ) as Cost,TSPL_ITEM_MASTER.Item_Category_Struct_Code " + Environment.NewLine

            'If clsCommon.myLen(strCategoryTable) > 0 Then
            '    qry += "," + strCodeColumn + "," + strCodeDescColumn
            'End If

            If clsCommon.myLen(strCategoryTable) > 0 Then
                qry += "," + strCodeColumnSelect + "," + strCodeDescColumnSelect
            End If
            'If ChkMRPWise.Checked = True AndAlso (clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item And Batch Wise Summary") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Document Wise Detail") = CompairStringResult.Equal) Then
            '    qry += ",MRP "
            'End If
            qry += " ,TSPL_ITEM_MASTER.Item_Type,VirtualTableItemType.Name as Item_Type_Name,TSPL_INVENTORY_SOURCE_CODE.In_Category,TSPL_INVENTORY_SOURCE_CODE.Out_Category "
            qry += " from ( " + Environment.NewLine + _
            " select TSPL_INVENTORY_MOVEMENT.Trans_Id,TSPL_INVENTORY_MOVEMENT.Trans_Type,TSPL_INVENTORY_MOVEMENT.Source_Doc_No,TSPL_INVENTORY_MOVEMENT.Punching_Date,TSPL_INVENTORY_MOVEMENT.InOut,TSPL_INVENTORY_MOVEMENT.Location_Code,TSPL_INVENTORY_MOVEMENT.Item_Code,BatchTABLE.Batch_No,BatchTABLE.Manufacture_Date,BatchTABLE.Expiry_Date,   BatchTABLE.MRP,TSPL_INVENTORY_MOVEMENT.Stock_UOM,convert(decimal(18,2), TSPL_INVENTORY_MOVEMENT.Stock_Qty*BatchTABLE.ConvRatio) as Stock_Qty,convert(decimal(18,2), TSPL_INVENTORY_MOVEMENT.FIFO_Cost*BatchTABLE.ConvRatio) as FIFO_Cost,convert(decimal(18,2), TSPL_INVENTORY_MOVEMENT.LIFO_Cost*BatchTABLE.ConvRatio) as LIFO_Cost,convert(decimal(18,2), TSPL_INVENTORY_MOVEMENT.Avg_Cost*BatchTABLE.ConvRatio) as Avg_Cost,0 as IsFromMilk,0 as MilkFatPer,0 as MilkSNFPer,0 as MilkFATKG,0 as MilkSNFKG,case when TSPL_INVENTORY_MOVEMENT.cust_code is not null and len(TSPL_INVENTORY_MOVEMENT.cust_code)>0 then cust_code else case when TSPL_INVENTORY_MOVEMENT.Vendor_Code is not null and len(TSPL_INVENTORY_MOVEMENT.Vendor_Code)>0 then Vendor_Code else TSPL_INVENTORY_MOVEMENT.Other_Location_Code end end as SourceCode,case when TSPL_INVENTORY_MOVEMENT.cust_code is not null and len(TSPL_INVENTORY_MOVEMENT.cust_code)>0 then TSPL_INVENTORY_MOVEMENT.Cust_Name else case when TSPL_INVENTORY_MOVEMENT.Vendor_Code is not null and len(TSPL_INVENTORY_MOVEMENT.Vendor_Code)>0 then TSPL_INVENTORY_MOVEMENT.Vendor_Name else TSPL_INVENTORY_MOVEMENT.Other_Location_Desc end end as SourceName, case when TSPL_INVENTORY_MOVEMENT.cust_code is not null and len(TSPL_INVENTORY_MOVEMENT.cust_code)>0 then 'C' else case when TSPL_INVENTORY_MOVEMENT.Vendor_Code is not null and len(TSPL_INVENTORY_MOVEMENT.Vendor_Code)>0 then 'V' else case when TSPL_INVENTORY_MOVEMENT.Other_Location_Code is not null and len(TSPL_INVENTORY_MOVEMENT.Other_Location_Code)>0 then 'L' else '' end end end as SourceType from  (" + Environment.NewLine + _
            " select *,convert(decimal(18,8), case when TSPL_BATCH_ITEM.Qty=0 then 0 else  TSPL_BATCH_ITEM.Qty/(select sum(Qty) from TSPL_BATCH_ITEM as inn where inn.Against_Inv_Movement_Trans_Id=TSPL_BATCH_ITEM.Against_Inv_Movement_Trans_Id) end) as ConvRatio from TSPL_BATCH_ITEM  " + Environment.NewLine + _
            " ) as BatchTABLE " + Environment.NewLine + _
            " inner join TSPL_INVENTORY_MOVEMENT on TSPL_INVENTORY_MOVEMENT.Trans_Id=BatchTABLE.Against_Inv_Movement_Trans_Id and TSPL_INVENTORY_MOVEMENT.Source_Doc_No=BatchTABLE.Document_Code" + Environment.NewLine
            qry += ") InventroyMovement " + Environment.NewLine
            qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=InventroyMovement.Item_Code" + Environment.NewLine
            qry += " left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code" + Environment.NewLine
            qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = InventroyMovement.Location_Code " + Environment.NewLine
            qry += " left outer join TSPL_LOCATION_MASTER as MainLocationTable on MainLocationTable.Location_Code =(case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end)"
            qry += " left outer join TSPL_ITEM_UOM_DETAIL as FATSNFConvertedUnit on FATSNFConvertedUnit.Item_Code=InventroyMovement.Item_Code and FATSNFConvertedUnit.UOM_Code='KG'"
            qry += " left outer join TSPL_INVENTORY_SOURCE_CODE on TSPL_INVENTORY_SOURCE_CODE.code=InventroyMovement.Trans_Type"
            'If clsCommon.myLen(cmbUnit.SelectedValue) > 0 Then
            '    qry += " inner join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=InventroyMovement.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='" + clsCommon.myCstr(cmbUnit.SelectedValue) + "'"
            'End If
            If clsCommon.myLen(strCategoryTable) > 0 Then
                qry += " left outer join (" + strCategoryTable + ") as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=InventroyMovement.Item_Code"
            End If
            qry += " left outer join (" & strItemGroup & ") as Item_Group on Item_Group.Structure_Code =TSPL_ITEM_MASTER.Structure_Code "
            qry += " left outer join (" & FrmItemMasterRMOther.LoadItemTypeQuery() & ") as VirtualTableItemType on VirtualTableItemType.Code = TSPL_ITEM_MASTER.Item_Type "
            qry += " Where 2=2 "

            If txtItemType.arrValueMember IsNot Nothing AndAlso txtItemType.arrValueMember.Count > 0 Then
                qry += " and TSPL_ITEM_MASTER.Item_Type in (" + clsCommon.GetMulcallString(txtItemType.arrValueMember) + ") " + Environment.NewLine
            End If
            qry += "  ) xxxxx "

            qry += " where 2=2 "

            If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                qry += " and Item_Code in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ") " + Environment.NewLine
            End If

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                qry += " and Location_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") " + Environment.NewLine
            End If

            If txtItemGroup.arrValueMember IsNot Nothing AndAlso txtItemGroup.arrValueMember.Count > 0 Then
                qry += " and coalesce(xxxxx.Item_Group,'') in (" + clsCommon.GetMulcallString(txtItemGroup.arrValueMember) + ") "
            End If

            Dim strWhrCatg As String = ""
            Dim LocationFirstTime As Integer = 0
            Dim LocationAddress As String = String.Empty

            'If rbtnLocationSelect.IsChecked Then
            '    Dim IsApplicable As Boolean = False
            '    For ii As Integer = 0 To gvLocation.RowCount - 1
            '        If clsCommon.myCBool(gvLocation.Rows(ii).Cells("SEL").Value) Then
            '            LocationFirstTime += 1
            '            If LocationFirstTime = 1 Then
            '                LocationAddress = clsDBFuncationality.getSingleValue("select TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then ''  else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_LOCATION_MASTER.Pin_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_LOCATION_MASTER.Pin_Code) End End End  as [LocAddress] from TSPL_LOCATION_MASTER where Location_Code= '" & clsCommon.myCstr(gvLocation.Rows(ii).Cells("CODE").Value) & "'")
            '            End If
            '            If IsApplicable Then
            '                strWhrCatg += " Or "
            '            End If
            '            strWhrCatg += " ((case when Is_Section='N' and Is_Sub_Location='N' then Location_Code else Main_Location_Code end) = '" + clsCommon.myCstr(gvLocation.Rows(ii).Cells("CODE").Value) + "') "
            '            IsApplicable = True
            '            Dim arr As Dictionary(Of String, Object) = gvLocation.Rows(ii).Tag
            '            If arr IsNot Nothing AndAlso arr.Count > 0 Then
            '                strWhrCatg += " and Location_Code in ("
            '                Dim isFirstTime As Boolean = True
            '                For Each strInn As String In arr.Keys
            '                    If Not isFirstTime Then
            '                        strWhrCatg += ","
            '                    End If
            '                    strWhrCatg += "'" + strInn + "'"
            '                    isFirstTime = False
            '                Next
            '                strWhrCatg += ")"
            '            End If
            '        End If
            '    Next
            '    If Not IsApplicable Then
            '        Throw New Exception("Please select at least one location")
            '    End If
            '    qry += " and (" + strWhrCatg + ")"
            'End If

            strWhrCatg = ""
            'If rbtnCategorySelect.IsChecked Then
            '    Dim IsApplicable As Boolean = False
            '    For ii As Integer = 0 To gvCategory.RowCount - 1
            '        If clsCommon.myCBool(gvCategory.Rows(ii).Cells("SEL").Value) Then
            '            If IsApplicable Then
            '                strWhrCatg += " and "
            '            End If
            '            IsApplicable = True
            '            strWhrCatg += "("
            '            Dim arr As Dictionary(Of String, Object) = gvCategory.Rows(ii).Tag
            '            If arr IsNot Nothing AndAlso arr.Count > 0 Then
            '                strWhrCatg += " [" + clsCommon.myCstr(gvCategory.Rows(ii).Cells("CODE").Value) + "] in ("
            '                Dim isFirstTime As Boolean = True
            '                For Each strInn As String In arr.Keys
            '                    If Not isFirstTime Then
            '                        strWhrCatg += ","
            '                    End If
            '                    strWhrCatg += "'" + strInn + "'"
            '                    isFirstTime = False
            '                Next
            '                strWhrCatg += ")"
            '            Else
            '                strWhrCatg += " 2=2  "
            '            End If
            '            strWhrCatg += ")"
            '        End If
            '    Next
            '    If Not IsApplicable Then
            '        Throw New Exception("Please select at least one category")
            '    End If
            '    qry += " and (" + strWhrCatg + ")"
            'End If


            ' ''End of Base Quert start Now

            Dim OuterOpClo As String = String.Empty
            Dim InnerOpClo As String = String.Empty
            ' OuterOpClo = " [OPBal],(case when OPBal=0 then 0 else OPBalCost/OPBal end) as OPBalrate,case when convert(decimal(18,2),[OPQTYKG])=0 then 0 else convert(decimal(18,2),([OPFAT]*100/[OPQTYKG])) end as [OPFATPER],convert(decimal(18,2),[OPFAT]) as [OPFAT],case when convert(decimal(18,2),[OPQTYKG])=0 then 0 else convert(decimal(18,2),([OPSNF]*100/[OPQTYKG])) end as [OPSNFPER],convert(decimal(18,2), [OPSNF]) as [OPSNF],OPBalCost,Received_Qty,(case when Received_Qty=0 then 0 else RecdCost/Received_Qty end) as RecdRate, RecdCost ,case when convert(decimal(18,2),Received_QtyKG)=0 then 0 else convert(decimal(18,2),(Received_FAT*100/Received_QtyKG)) end as Received_FATPER,convert(decimal(18,2), Received_FAT) as Received_FAT,case when convert(decimal(18,2),Received_QTYKG)=0 then 0 else convert(decimal(18,2),(Received_SNF*100/Received_QTYKG)) end as Received_SNFPER,convert(decimal(18,2), Received_SNF) as Received_SNF,convert(decimal(18,2),Issued_Qty) as Issued_Qty, (case when Issued_Qty=0 then 0 else IssueCost/Issued_Qty end) as IssueRate,IssueCost,case when convert(decimal(18,2),Issued_QTYKG)=0 then 0 else convert(decimal(18,2),(Issued_FAT*100/Issued_QTYKG)) end as Issued_FATPER,convert(decimal(18,2), Issued_FAT) as Issued_FAT,case when convert(decimal(18,2),Issued_QTYKG)=0 then 0 else convert(decimal(18,2),(Issued_SNF*100/Issued_QTYKG)) end as Issued_SNFPER,convert(decimal(18,2) ,Issued_SNF) as Issued_SNF  ,[Balance_Qty],convert(decimal(18,3), case when Balance_Qty=0 then 0 else Cost/Balance_Qty end) as Rate,Cost,case when convert(decimal(18,2),[Balance_QTYKG])=0 then 0 else convert(decimal(18,2),([Balance_FAT]*100/[Balance_QTYKG])) end as [Balance_FATPER],convert(decimal(18,2), [Balance_FAT]) as  [Balance_FAT],case when convert(decimal(18,2),[Balance_QTYKG])=0 then 0 else convert(decimal(18,2),([Balance_SNF]*100/[Balance_QTYKG])) end as [Balance_SNFPER],convert(decimal(18,2), [Balance_SNF]) as [Balance_SNF] "
            InnerOpClo = "  SUM(STOCK_QTY * (CASE WHEN PUNCHING_DAte < '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' THEN 1 ELSE 0 end) * (case when InOut='I' then 1 else -1 end))  AS [OPBal]  ," & _
                            " SUM(Cost  * (CASE WHEN PUNCHING_DAte < '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' THEN 1 ELSE 0 end) * (case when InOut='I' then 1 else -1 end))  AS [OPBalCost]  , " & _
                            " SUM(QtyKG * (CASE WHEN PUNCHING_DAte < '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' THEN 1 ELSE 0 end) * (case when InOut='I' then 1 else -1 end))  AS OPQTYKG ," & _
                            " SUM((case when IsFromMilk=1 then MilkFATKG else (STOCK_QTY*FatPer) end)  * (CASE WHEN PUNCHING_DAte < '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' THEN 1 ELSE 0 end) * (case when InOut='I' then 1 else -1 end))  AS [OPFAT], " & _
                            " SUM((case when IsFromMilk=1 then MilkSNFKG else (STOCK_QTY*SNFPer) end)  * (CASE WHEN PUNCHING_DAte < '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' THEN 1 ELSE 0 end) * (case when InOut='I' then 1 else -1 end))  AS [OPSNF], " & _
                            " SUM(STOCK_QTY * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' THEN 1 ELSE 0 end) * (case when InOut='I' then 1 else 0 end))  AS Received_Qty , " & _
                            " SUM(Cost * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' THEN 1 ELSE 0 end) * (case when InOut='I' then 1 else 0 end))  AS RecdCost , " & _
                            " SUM(QtyKG * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' THEN 1 ELSE 0 end) * (case when InOut='I' then 1 else 0 end))  AS Received_QtyKG , " & _
                            " SUM((case when IsFromMilk=1 then MilkFATKG else (STOCK_QTY*FatPer) end) * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' THEN 1 ELSE 0 end) * (case when InOut='I' then 1 else 0 end))  AS Received_FAT , " & _
                            " SUM((case when IsFromMilk=1 then MilkSNFKG else (STOCK_QTY*SNFPer) end) * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' THEN 1 ELSE 0 end) * (case when InOut='I' then 1 else 0 end))  AS Received_SNF , " & _
                            " SUM(STOCK_QTY * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' THEN 1 ELSE 0 end) * (case when InOut='I' then 0 else 1 end))  AS Issued_Qty," & _
                            " SUM(Cost * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' THEN 1 ELSE 0 end) * (case when InOut='I' then 0 else 1 end))  AS IssueCost," & _
                            " SUM(QtyKG * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' THEN 1 ELSE 0 end) * (case when InOut='I' then 0 else 1 end))  AS Issued_QtyKG," & _
                            " SUM((case when IsFromMilk=1 then MilkFATKG else (STOCK_QTY*FatPer) end) * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' THEN 1 ELSE 0 end) * (case when InOut='I' then 0 else 1 end))  AS Issued_FAT," & _
                            " SUM((case when IsFromMilk=1 then MilkSNFKG else (STOCK_QTY*SNFPer) end ) * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' THEN 1 ELSE 0 end) * (case when InOut='I' then 0 else 1 end))  AS Issued_SNF," & _
                            " SUM(STOCK_QTY * (CASE WHEN PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'  THEN 1 ELSE 0 end) * (case when InOut='I' then 1 else -1 end))  AS [Balance_Qty]," & _
                            " SUM(cost * (CASE WHEN PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'  THEN 1 ELSE 0 end) * (case when InOut='I' then 1 else -1 end))  AS [Cost], " & _
                            " SUM(QtyKG * (CASE WHEN PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'  THEN 1 ELSE 0 end) * (case when InOut='I' then 1 else -1 end))  AS Balance_QtyKG," & _
                            " SUM((case when IsFromMilk=1 then MilkFATKG else (STOCK_QTY*FatPer) end ) * (CASE WHEN PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'  THEN 1 ELSE 0 end) * (case when InOut='I' then 1 else -1 end))  AS [Balance_FAT]," & _
                            " SUM((case when IsFromMilk=1 then MilkSNFKG else (STOCK_QTY*SNFPer) end ) * (CASE WHEN PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'  THEN 1 ELSE 0 end) * (case when InOut='I' then 1 else -1 end))  AS [Balance_SNF],max(tech_shelf_life) as Shelf_Life "

            Dim strFinalQry As String = ""

            '  strFinalQry = "select Item_Type,Item_Type_Name,Item_Group,Group_Description," + strCodeColumn + "," + strCodeDescColumn + ", Item_Category_Struct_Code  as Item_Category_Struct_Code,Main_Location_Code,MainLocationDesc,Location_Code,[Loc Desp],Item_Code,Item_Desc,itf_code,Batch_No,Manufacture_Date,Expiry_Date,MRP,Stock_Qty,Stock_UOM,"
            strFinalQry = "select  [Loc Desp], [SUB BRAND],[PACK SIZE] as [SKU],([OPBal]+Received_Qty)-Issued_Qty as [Stocks (Quantity)],Stock_UOM as UOM,convert(varchar(15),Manufacture_Date,103) as [PKD/MFG DT],Shelf_Life," & _
           "cast (cast( (case when isnull(Shelf_Life,0)=0 then 1 else COALESCE(cast((datediff(day,cast(Manufacture_Date as date),cast(getdate() as date)))as decimal(10,3))/Shelf_Life,'') end)as decimal(10,2)) as varchar(10)) as [% Shelf Life Remaining] "
            ' strFinalQry += OuterOpClo
            strFinalQry += "  from (" + Environment.NewLine
            strFinalQry += " select  Item_Type,max(Item_Type_Name) as Item_Type_Name,Item_Group,max(Group_Description) as Group_Description," + strCodeColumnMax + "," + strCodeDescColumnMax + ",max(Item_Category_Struct_Code) as Item_Category_Struct_Code,Main_Location_Code,max(MainLocationDesc) as MainLocationDesc, Location_Code,max([Loc Desp]) as [Loc Desp],Item_Code,MAX(Item_Desc) as Item_Desc,max(itf_code)as itf_code,Batch_No,min(Manufacture_Date) as Manufacture_Date,max(Expiry_Date) as Expiry_Date,max(MRP) as MRP, SUM(Stock_Qty * case when InOut='I' then 1 else -1 end) as Stock_Qty,MAX(Stock_UOM) Stock_UOM,"
            strFinalQry += InnerOpClo
            strFinalQry += " from (" + qry + ")xxx Group by Item_Type,Item_Group,Item_Code,Batch_No,Main_Location_Code,Location_Code"
            strFinalQry += " )xxxx" + Environment.NewLine

            clsCommon.ProgressBarUpdate("Loading Data.Please Wait...")


            'Dim StrQuery As String = Nothing


            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strFinalQry)
            If dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                gv1.ShowRowHeaderColumn = False
                gv1.AllowAddNewRow = False
                gv1.AllowDeleteRow = False
                gv1.ReadOnly = True
                gv1.BestFitColumns()
                ReStoreGridLayout()
            End If
            RadPageView1.SelectedPage = RadPageViewPage2

        Catch ex As Exception
            clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            clsCommon.ProgressBarHide()
        End Try

    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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

    Private Sub gv1_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.ViewCellFormatting
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub

    Sub print(ByVal exporter As EnumExportTo)
        Try
            LoadData(0)

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmStockAgeingAnalysisReport & "'"))
            If txtItemType.arrDispalyMember IsNot Nothing AndAlso txtItemType.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Item Type : " + clsCommon.GetMulcallStringWithComma(txtItemType.arrDispalyMember))
            End If
            If txtItemGroup.arrDispalyMember IsNot Nothing AndAlso txtItemGroup.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Item Group : " + clsCommon.GetMulcallStringWithComma(txtItemGroup.arrDispalyMember))
            End If
            If txtItem.arrDispalyMember IsNot Nothing AndAlso txtItem.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember))
            End If
            If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If
           
            If exporter = EnumExportTo.Excel Then
                'Dim sfd As SaveFileDialog = New SaveFileDialog()
                'Dim filePath As String
                'sfd.FileName = Me.Text
                'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                '    filePath = sfd.FileName
                'Else
                '    Exit Sub
                'End If
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
                'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)
            Else
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        print(EnumExportTo.Excel)

    End Sub

    Private Sub btnPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        print(EnumExportTo.PDF)
    End Sub

    Private Sub RadButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton3.Click
        EnableDisableCtrl(True)
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        LoadCategory()
        GetMIS_ITem_GroupColumn()

        gv1.GroupDescriptors.Clear()
        txtItemGroup.arrValueMember = Nothing
        txtItemType.arrValueMember = Nothing
        txtItem.arrValueMember = Nothing
        txtLocation.arrValueMember = Nothing
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value
        RadPageView1.SelectedPage = RadPageViewPage1

        gv1.MasterTemplate.SummaryRowsBottom.Clear()

        RadPageViewPage2.Text = "Report"

    End Sub

    Sub EnableDisableCtrl(ByVal val As Boolean)

        'txtTransaction.Enabled = val

    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        clsGridLayout.DeleteData(Me.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub
    Private Sub TreeView_NodeCheckedChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.TreeNodeCheckedEventArgs)
        TreeCheckBoxes(e.Node, e.Node.Checked)
    End Sub

    Public Sub TreeCheckBoxes(ByVal CurrentNode As RadTreeNode, ByVal val As Boolean)
        For Each Ctr As RadTreeNode In CurrentNode.Nodes
            Ctr.Checked = val
            TreeCheckBoxes(Ctr, val)
        Next
    End Sub

    Private Sub btnQuickExport_Click(sender As Object, e As EventArgs) Handles btnQuickExport.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            'arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmStockAgeingAnalysisReport & "'"))
            'If txtItem.arrDispalyMember IsNot Nothing AndAlso txtItem.arrDispalyMember.Count > 0 Then
            '    arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember))
            'End If
            'If clsCommon.myLen(cmbUnit.SelectedValue) > 0 Then
            '    arrHeader.Add("Unit : " + cmbUnit.SelectedValue)
            'End If
            'If rbtnLocationSelect.IsChecked Then
            '    Dim strLoca As String = ""
            '    For Each grow As GridViewRowInfo In gvLocation.Rows
            '        If clsCommon.myCBool(grow.Cells("SEL").Value) = True Then
            '            strLoca += "," + clsCommon.myCstr(grow.Cells("NAME").Value)
            '        End If
            '    Next
            '    arrHeader.Add("Location : " + strLoca)
            'End If

            'Dim sfd As SaveFileDialog = New SaveFileDialog()
            'Dim filePath As String
            'sfd.FileName = Me.Text
            'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    filePath = sfd.FileName
            'Else
            '    Exit Sub
            'End If
            'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
            transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton8_Click(sender As Object, e As EventArgs)
        Try
            LoadData(2)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Shared Sub ExportBulkData(ByVal qry As String, ByVal arrVisibleColumAndCaption As Dictionary(Of String, String), ByVal strReportNameInSaveDialog As String)
        'clsCommon.ProgressBarPercentShow()
        clsCommon.ProgressBarUpdate("Fatching data...")
        Dim reader As System.Data.SqlClient.SqlDataReader = Nothing
        Try
            If arrVisibleColumAndCaption Is Nothing OrElse arrVisibleColumAndCaption.Count <= 0 Then
                Throw New Exception("Please provice column and caption for export")
            End If

            Dim rowsPerSheet As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.QuickExport, clsFixedParameterCode.MaxRowsForQuickExport, Nothing))

            Dim FilePath As String = "C:\ERPTempFolder"
            Dim IsExists As Boolean = System.IO.Directory.Exists(FilePath)
            If Not IsExists Then
                System.IO.Directory.CreateDirectory(FilePath)
            End If
            strReportNameInSaveDialog += clsCommon.GetPrintDate(DateTime.Now, "yyyyMMddhhmmss")
            FilePath = "C:\ERPTempFolder\" + strReportNameInSaveDialog.Replace("/", "_").Replace("\\", "_") + ".xlsx"

            Dim intTotalRows As Integer = 0
            Dim intSheetCounter As Integer = 1
            Dim intReaderCounter As Integer = 0
            reader = clsDBFuncationality.GetDataReader(qry, Nothing)
            Dim ResultsData As DataTable = Nothing
            Dim c As Integer = 0
            Dim firstTime As Boolean = True

            'Get the Columns names, types, this will help when we need to format the cells in the excel sheet.
            Dim dtSchema As DataTable = reader.GetSchemaTable()
            'Dim listCols = New List(Of DataColumn)()

            If dtSchema IsNot Nothing Then
                ResultsData = New DataTable()
                For Each drow As DataRow In dtSchema.Rows
                    Dim columnName As String = clsCommon.myCstr(drow("ColumnName"))
                    If arrVisibleColumAndCaption.ContainsKey(columnName) Then
                        Dim column = New DataColumn(columnName, DirectCast(drow("DataType"), Type))
                        column.Unique = CBool(drow("IsUnique"))
                        column.AllowDBNull = CBool(drow("AllowDBNull"))
                        column.AutoIncrement = CBool(drow("IsAutoIncrement"))
                        column.Caption = arrVisibleColumAndCaption(columnName)
                        'listCols.Add(column)
                        ResultsData.Columns.Add(column)
                    End If
                Next
            End If
            Dim rowData(rowsPerSheet, ResultsData.Columns.Count) As Object
            Dim workBook As Microsoft.Office.Interop.Excel.Workbook = Nothing

            While reader.Read()
                intReaderCounter += 1
                clsCommon.ProgressBarUpdate("Fatching Data for Sheet No " + clsCommon.myCstr(intSheetCounter) + " Row(s) " + clsCommon.myCstr(intReaderCounter))
                For i As Integer = 0 To ResultsData.Columns.Count - 1
                    rowData(c, i) = reader(ResultsData.Columns(i).ColumnName)
                Next
                c += 1
                If c = rowsPerSheet Then
                    clsCommon.ProgressBarUpdate("Writing data in excel sheet.Sheet No " + clsCommon.myCstr(intSheetCounter) + " Row(s) " + clsCommon.myCstr(intReaderCounter))
                    workBook = ExportToOxml(intSheetCounter, firstTime, c, ResultsData, rowData, FilePath, workBook)
                    c = 0
                    ResultsData.Clear()
                    firstTime = False
                    intSheetCounter += 1
                End If
            End While

            If c <> 0 Then

                clsCommon.ProgressBarUpdate("Writing data in excel sheet.Sheet No " + clsCommon.myCstr(intSheetCounter) + " Row(s) " + clsCommon.myCstr(intReaderCounter))
                workBook = ExportToOxml(intSheetCounter, firstTime, c, ResultsData, rowData, FilePath, workBook)
            End If

            workBook = Nothing
            GC.Collect()
            GC.WaitForPendingFinalizers()

            If intReaderCounter > 0 Then
                clsCommon.ProgressBarUpdate("Data exported.Opening File " + FilePath + ".Please wait...")
                Process.Start(FilePath)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            reader.Close()
        End Try
    End Sub

    Private Shared Function ExportToOxml(ByVal intSheetNo As Integer, ByVal firstTime As Boolean, ByVal RowsToWrite As Integer, ByVal Schema As DataTable, ByVal rawData(,) As Object, ByVal FilePath As String, ByRef wbook As Microsoft.Office.Interop.Excel.Workbook) As Microsoft.Office.Interop.Excel.Workbook
        Try
            Dim dt As New System.Data.DataTable()
            For i As Integer = 0 To Schema.Columns.Count - 1
                dt.Columns.Add("Column" & (i + 1))
                If clsCommon.myLen(Schema.Columns(i).Caption) > 0 Then
                    dt.Columns("Column" & (i + 1)).Caption = Schema.Columns(i).Caption
                Else
                    dt.Columns("Column" & (i + 1)).Caption = Schema.Columns(i).ColumnName
                End If
            Next

            Dim excel As New Microsoft.Office.Interop.Excel.Application
            If wbook Is Nothing Then
                Dim wBook1 As Microsoft.Office.Interop.Excel.Workbook = Nothing
                wbook = wBook1
                wbook = excel.Workbooks.Add()
            Else
                wbook = excel.Workbooks.Open(FilePath)
            End If
            Dim wSheet As Microsoft.Office.Interop.Excel.Worksheet = Nothing
            Dim GridCurrentRowIndex As Int64 = -1
            Dim GridLastSavedRowIndex As Int64 = -1
            wSheet = wbook.Sheets.Add(, , 1)
            wbook.ActiveSheet.Move(After:=wbook.Sheets(wbook.Sheets.Count))
            If firstTime Then
                Try
                    CType(wbook.Sheets("Sheet1"), Microsoft.Office.Interop.Excel.Worksheet).Delete()
                    CType(wbook.Sheets("Sheet2"), Microsoft.Office.Interop.Excel.Worksheet).Delete()
                    CType(wbook.Sheets("Sheet3"), Microsoft.Office.Interop.Excel.Worksheet).Delete()
                Catch ex As Exception
                End Try
            End If
            wSheet.Name = "Sheet" & intSheetNo

            Dim jk As Integer = 0
            For i As Integer = 0 To Schema.Columns.Count - 1
                jk += 1
                Dim MyType As TypeCode = Type.GetTypeCode(Schema.Columns(i).DataType)
                If MyType = TypeCode.String Then
                    wSheet.Range(ColumnIndexToColumnLetter(jk) & ":" & ColumnIndexToColumnLetter(jk)).Cells.NumberFormat = "@"
                End If
            Next

            Dim colnum As Integer = -1
            Dim PrevCol As Integer = -1
            Dim ColNums(0 To Schema.Columns.Count - 1) As Integer

            Dim colIndex As Integer = 1
            Dim rowIndex As Integer = 1

            Dim dc As System.Data.DataColumn
            colIndex = 0
            For Each dc In Schema.Columns
                colIndex = colIndex + 1
                excel.Cells(rowIndex, colIndex) = dc.Caption
            Next

            Dim LastColumn As String = ColumnIndexToColumnLetter(Schema.Columns.Count)
            Dim Lastrow As Integer = RowsToWrite

            Dim row As Integer = 0
            Dim col As Integer = 0

            wSheet.Range("A2", LastColumn & (Lastrow + 1)).Value2 = rawData
            rawData = Nothing
            GC.Collect()
            GC.WaitForPendingFinalizers()
            wSheet.Columns.AutoFit()
            CType(wbook.Sheets("Sheet1"), Microsoft.Office.Interop.Excel.Worksheet).Select()
            excel.DisplayAlerts = False
            wbook.SaveAs(FilePath, , , , , , Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive)
            wbook.Close(True)

            excel.Quit()
            excel = Nothing
            rawData = Nothing
            GC.Collect()
            GC.WaitForPendingFinalizers()
        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return wbook
    End Function

    Private Shared Function ColumnIndexToColumnLetter(ByVal colIndex As Integer) As String
        Dim div As Integer = colIndex
        Dim colLetter As String = [String].Empty
        Dim [mod] As Integer = 0
        While div > 0
            [mod] = (div - 1) Mod 26
            colLetter = (Convert.ToChar(65 + [mod])).ToString & colLetter
            div = CInt((div - [mod]) / 26)
        End While
        Return colLetter
    End Function


    Private Sub LoadDataInGridViaDataReader(ByVal qry As String)
        'clsCommon.ProgressBarPercentShow()
        'clsCommon.ProgressBarUpdate("Fatching data...")
        Dim reader As System.Data.SqlClient.SqlDataReader = Nothing
        Try
            reader = clsDBFuncationality.GetDataReader(qry, Nothing)
            If reader Is Nothing OrElse Not reader.HasRows Then
                Throw New Exception("No Data found")
            End If
            RadPageView1.SelectedPage = RadPageViewPage2
            Dim dtTest As New DataTable
            dtTest.Load(reader)
            gv1.DataSource = dtTest
            GC.Collect()
            GC.WaitForPendingFinalizers()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            reader.Close()
        End Try
    End Sub



    Private Sub BtnGo_Click(sender As Object, e As EventArgs) Handles BtnGo.Click
        Try
            gv1.EnableFiltering = True
            PageSetupReport_ID = MyBase.Form_ID
            TemplateGridview = gv1
            LoadData(0)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub txtItemType__My_Click(sender As Object, e As EventArgs) Handles txtItemType._My_Click
        txtItemType.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemTypestoreco", FrmItemMasterRMOther.LoadItemTypeQuery(), "Code", "Name", txtItemType.arrValueMember, txtItemType.arrDispalyMember)
    End Sub

    Private Sub txtItemGroup__My_Click(sender As Object, e As EventArgs) Handles txtItemGroup._My_Click
        Dim qry As String = " select SNo,Value,Description as Name,Custom_Field_Code as [Code] from TSPL_CUSTOM_FIELD_DETAIL where Custom_Field_Code='" & MIS_Item_Group & "' "
        txtItemGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemGroupMulSel", qry, "Value", "Name", txtItemGroup.arrValueMember, txtItemGroup.arrDispalyMember)
    End Sub

    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Dim qry As String
        If txtItemType.arrValueMember Is Nothing OrElse clsCommon.GetMulcallString(txtItemType.arrValueMember) = "All" Then
            qry = " select Item_Code,Item_Desc from TSPL_ITEM_MASTER where Is_Batch_Item=1  order by Item_Code "
        Else
            qry = " select Item_Code,Item_Desc from TSPL_ITEM_MASTER where Is_Batch_Item=1 and Item_Type in (" + clsCommon.GetMulcallString(txtItemType.arrValueMember) + ") order by Item_Code "

        End If

        txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Item_Code", "Item_Desc", txtItem.arrValueMember, txtItem.arrDispalyMember)

    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " select  Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("LOCATION", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub

   
    Private Sub RadMenuItemSett1_Click(sender As Object, e As EventArgs) Handles RadMenuItemSett1.Click
        If clsCommon.myLen(Me.Form_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = Me.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub
End Class
