Imports common
Imports System.Threading
Imports Telerik.WinControls.UI.Export
Imports Telerik.WinControls.UI.Export.ExcelML
Imports System.IO
Imports Microsoft.Office.Interop

'Ticket No-ERO/05/06/19-000634 Sanjay,Add TS% ,TSKG
Public Class FrmStockRecoBatch
    Inherits FrmMainTranScreen
#Region "Varaibels"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public isDataLoad As Boolean = False
    Public dtFrom As Date
    Public dtTo As Date
    Public strType As String
    Public arrLoc As Dictionary(Of String, Object) = Nothing
    Public arrItemGroup As ArrayList
    Public arrItem As ArrayList
    Public arrTransaction As ArrayList
    Public arrCat As Dictionary(Of String, Object) = Nothing
    Public Unit_Code As String = Nothing
    Public MRP_Wise As Boolean = False
    Public ShowFatAndSNF As Boolean = False
    Dim dtCategory As DataTable
    Dim MIS_Item_Group As String
    Public InOutType As String = Nothing
    Public SkipCheckFatAndSNF As Boolean = False
    Public arrItemType As ArrayList
    Dim arrBack As List(Of String)
#End Region

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub FrmKPIReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        txtFromDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        txtToDate.Value = clsCommon.GETSERVERDATE
        arrBack = New List(Of String)
        SetUserMgmtNew()
        LoadCategory()
        LoadUnit()
        LoadLocation()
        LoadType()
        LoadInOutType()
        ButtonToolTip.SetToolTip(RadButton2, "Press Alt+C Close the Window")
        RadPageView1.SelectedPage = RadPageViewPage1
        rbtnCategoryAll.IsChecked = True
        rbtnLocationAll.IsChecked = True
        cboType.SelectedValue = "Item And Batch Wise Summary"
        cboInOutType.SelectedValue = "All"
        btnPrint.Visible = False
        GetMIS_ITem_GroupColumn()
        'If clsCommon.myLen(MIS_Item_Group) <= 0 Then
        '    clsCommon.MyMessageBoxShow("MIS Item Group Custom field is not create in Item Structure.")
        'End If
        If isDataLoad Then
            txtFromDate.Value = dtFrom
            txtToDate.Value = dtTo
            cmbUnit.SelectedValue = Unit_Code
            chkFATAndSNF.Checked = ShowFatAndSNF
            txtItem.arrValueMember = arrItem
            txtTransaction.arrValueMember = arrTransaction
            txtItemGroup.arrValueMember = arrItemGroup
            If arrLoc IsNot Nothing AndAlso arrLoc.Count > 0 Then
                rbtnLocationSelect.IsChecked = True
                For Each str As String In arrLoc.Keys
                    For ii As Integer = 0 To gvLocation.RowCount - 1
                        If clsCommon.CompairString(clsCommon.myCstr(gvLocation.Rows(ii).Cells("CODE").Value), str) = CompairStringResult.Equal Then
                            gvLocation.Rows(ii).Cells("SEL").Value = True
                            gvLocation.Rows(ii).Tag = arrLoc(str)
                        End If
                    Next
                Next
            End If
            If arrCat IsNot Nothing AndAlso arrCat.Count > 0 Then
                rbtnCategorySelect.IsChecked = True
                For Each str As String In arrCat.Keys
                    For ii As Integer = 0 To gvCategory.RowCount - 1
                        If clsCommon.CompairString(clsCommon.myCstr(gvCategory.Rows(ii).Cells("CODE").Value), str) = CompairStringResult.Equal Then
                            gvCategory.Rows(ii).Cells("SEL").Value = True
                            gvCategory.Rows(ii).Tag = arrCat(str)
                        End If
                    Next
                Next
            End If
            cboInOutType.SelectedValue = InOutType
            cboType.SelectedValue = strType

            LoadData(False)

        End If
    End Sub

    Sub LoadLocation()
        gvLocation.DataSource = Nothing
        Dim qry As String = " select cast( 0 as bit) as SEL,Location_Code as CODE,Location_Desc as NAME from TSPL_LOCATION_MASTER where ((Is_Section='N' and Is_Sub_Location='N' and Location_Type IN ('Physical','Logical','Virtual') ) or (CSA_Type='Y') ) "
        If Not chkIncludeGIT.Checked Then
            qry += " and  TSPL_LOCATION_MASTER.GIT_Type<>'Y' "
        End If
        qry += " order by Location_Code"
        gvLocation.DataSource = clsDBFuncationality.GetDataTable(qry)

        gvLocation.Columns("SEL").ReadOnly = False
        gvLocation.Columns("SEL").Width = 30
        gvLocation.Columns("SEL").HeaderText = " "

        gvLocation.Columns("CODE").ReadOnly = True
        gvLocation.Columns("CODE").Width = 100
        gvLocation.Columns("CODE").HeaderText = "Code"

        gvLocation.Columns("NAME").ReadOnly = True
        gvLocation.Columns("NAME").Width = 200
        gvLocation.Columns("NAME").HeaderText = "Description"

        gvLocation.ShowGroupPanel = False
        gvLocation.AllowAddNewRow = False
        gvLocation.AllowColumnReorder = False
        gvLocation.AllowRowReorder = False
        gvLocation.EnableSorting = False
        gvLocation.ShowFilteringRow = True
        gvLocation.EnableFiltering = True
        gvLocation.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvLocation.MasterTemplate.ShowRowHeaderColumn = True
    End Sub

    Sub LoadInOutType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "All"
        dr("Name") = "Both"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "In"
        dr("Name") = "In"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Out"
        dr("Name") = "Out"
        dt.Rows.Add(dr)

        cboInOutType.DataSource = dt
        cboInOutType.ValueMember = "Code"
        cboInOutType.DisplayMember = "Name"
    End Sub

    Sub LoadType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        Dim dr As DataRow = dt.NewRow()

        'dr("Code") = "Item Type Wise Summary"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "Item Group Wise Summary"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "Category Wise Summary"
        'dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Item And Batch Wise Summary"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Item Batch And Location Wise Summary"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Document Wise Detail"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Date, Item And Document Wise Detail"
        dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "Date and Item Wise Stock"
        'dt.Rows.Add(dr)

        cboType.DataSource = dt
        cboType.ValueMember = "Code"
        cboType.DisplayMember = "Code"

    End Sub

    Sub LoadCategory()
        dtCategory = clsDBFuncationality.GetDataTable("select ITEM_CATEGORY_CODE AS CodeColumn,ITEM_CATEGORY_CODE+'DESC' as CodeDescColumn,DESCRIPTION as DescColumn  from TSPL_ITEM_CATEGORY_LEVEL order by CATEGORY_LEVEL")

        gvCategory.DataSource = Nothing
        Dim qry As String = "select cast( 0 as bit) as SEL,ITEM_CATEGORY_CODE AS Code,DESCRIPTION as NAME from TSPL_ITEM_CATEGORY_LEVEL ORDER BY CATEGORY_LEVEL"
        gvCategory.DataSource = clsDBFuncationality.GetDataTable(qry)

        gvCategory.Columns("SEL").ReadOnly = False
        gvCategory.Columns("SEL").Width = 30
        gvCategory.Columns("SEL").HeaderText = " "

        gvCategory.Columns("CODE").ReadOnly = True
        gvCategory.Columns("CODE").Width = 100
        gvCategory.Columns("CODE").HeaderText = "Code"

        gvCategory.Columns("NAME").ReadOnly = True
        gvCategory.Columns("NAME").Width = 200
        gvCategory.Columns("NAME").HeaderText = "Description"

        gvCategory.ShowGroupPanel = False
        gvCategory.AllowAddNewRow = False
        gvCategory.AllowColumnReorder = False
        gvCategory.AllowRowReorder = False
        gvCategory.EnableSorting = False
        gvCategory.ShowFilteringRow = True
        gvCategory.EnableFiltering = True
        gvCategory.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvCategory.MasterTemplate.ShowRowHeaderColumn = True

    End Sub

    Sub LoadUnit()
        Dim qry As String = "select Unit_Code as Code,Unit_Desc as Description from TSPL_UNIT_MASTER"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim dr As DataRow = dt.NewRow
        dr("Code") = ""
        dr("Description") = "Select"
        dt.Rows.InsertAt(dr, 0)
        cmbUnit.DataSource = Nothing
        cmbUnit.DataSource = dt
        cmbUnit.DisplayMember = "Description"
        cmbUnit.ValueMember = "Code"
    End Sub

    Private Sub SetUserMgmtNew()
        ' ''MyBase.SetUserMgmt(clsUserMgtCode.stockRecoNew)
        ''MyBase.SetUserMgmt(clsUserMgtCode.stockRecoBatch)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        '' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
        radbtnExp.Visible = MyBase.isExport
        ' RadButton8.Visible = MyBase.isExport
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
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
        'LoadData(3)
        Me.Close()
    End Sub
    Sub GetReportGridID()
        Dim VarID As String = ""
        If chkFATAndSNF.Checked = True Then
            VarID += "_FS"
        End If
        If chkIncludeGIT.Checked = True Then
            VarID += "_GT"
        End If
        If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item Type Wise Summary") = CompairStringResult.Equal Then
            VarID += "_TW"
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item Group Wise Summary") = CompairStringResult.Equal Then
            VarID += "_TG"
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Category Wise Summary") = CompairStringResult.Equal Then
            VarID += "_CW"
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item And Batch Wise Summary") = CompairStringResult.Equal Then
            VarID += "_IA"
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item Batch And Location Wise Summary") = CompairStringResult.Equal Then
            VarID += "_IB"
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Document Wise Detail") = CompairStringResult.Equal Then
            VarID += "_DW"
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Date, Item And Document Wise Detail") = CompairStringResult.Equal Then
            VarID += "_DI"
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Date and Item Wise Stock") = CompairStringResult.Equal Then
            VarID += "_DS"
        End If

        gv1.VarID = VarID
    End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        Try
            GetReportGridID()
            gv1.EnableFiltering = True
            TemplateGridview = gv1
            PageSetupReport_ID = GetReportID()
            LoadData(0)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub LoadData(ByVal isPrintCrystalReport As Integer)
        Try
            ' Ticket No : TEC/27/06/19-000569 by prabhakar
            '===================update by preeti gupta Against Ticket No[BHA/16/07/18-000166][Add column Manual Batch No]
            gv1.DataSource = Nothing
            gv1.Columns.Clear()
            gv1.Rows.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.ShowGroupPanel = False
            gv1.EnableFiltering = True

            If clsCommon.GetDateWithEndTime(txtToDate.Value) < clsCommon.GetDateWithStartTime(txtFromDate.Value) Then
                clsCommon.MyMessageBoxShow(Me, "To Date cant be less than from date", Me.Text)
                Exit Sub
            End If
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
                strCategoryTable = "select Item_Code," + strCodeColumnMax + "," + strCodeDescColumnMax + "  from (" + Environment.NewLine & _
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
            Dim qry As String = "select * from ( select InventroyMovement.Trans_Id,InventroyMovement.Trans_Type,TSPL_INVENTORY_SOURCE_CODE.Name as Trans_Type_Name,InventroyMovement.Source_Doc_No,InventroyMovement.Punching_Date, InventroyMovement.InOut,case when InventroyMovement.InOut='I' then 'In' else case when InventroyMovement.InOut='O' then 'Out' else '' end end as 'InOutView',"
            qry += " case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end as Main_Location_Code,MainLocationTable.Location_Desc as MainLocationDesc, InventroyMovement.Location_Code,TSPL_LOCATION_MASTER.Location_Desc AS [Loc Desp],TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then ''  else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_LOCATION_MASTER.Pin_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_LOCATION_MASTER.Pin_Code) End End End  as [LocAddress],SourceCode,SourceName,SourceType  ,Item_Group.Item_Group,Item_Group.Group_Description, InventroyMovement.Item_Code, InventroyMovement.Batch_No,InventroyMovement.Manual_BatchNo,InventroyMovement.Manufacture_Date,InventroyMovement.Expiry_Date, InventroyMovement.MRP ,TSPL_ITEM_MASTER.Item_Desc,tspl_item_master.itf_code,"
            qry += " IsFromMilk,MilkFATKG,MilkSNFKG,case when IsFromMilk=1 then MilkFatPer else isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) end as MilkFatPer,case when IsFromMilk=1 then MilkSNFPer else isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) end as MilkSNFPer,TSPL_LOCATION_MASTER.Is_Section,TSPL_LOCATION_MASTER.Is_Sub_Location,"
            qry += " isnull((InventroyMovement.Stock_Qty * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end))  ,0) as QtyKG,"
            If clsCommon.myLen(cmbUnit.SelectedValue) > 0 Then
                qry += " '" + clsCommon.myCstr(cmbUnit.SelectedValue) + "' as Stock_UOM,(InventroyMovement.Stock_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as Stock_Qty,"
                qry += " isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end) * (TSPL_ITEM_UOM_DETAIL.Conversion_Factor)) from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) as FatPer,"
                qry += " isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end) * (TSPL_ITEM_UOM_DETAIL.Conversion_Factor)) from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) as SNFPer,"
            Else
                qry += " InventroyMovement.Stock_UOM,InventroyMovement.Stock_Qty,"
                qry += " isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)) from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) as FatPer,"
                qry += " isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)) from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) as SNFPer,"
            End If
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
            " select TSPL_INVENTORY_MOVEMENT.Trans_Id,TSPL_INVENTORY_MOVEMENT.Trans_Type,TSPL_INVENTORY_MOVEMENT.Source_Doc_No,TSPL_INVENTORY_MOVEMENT.Punching_Date,TSPL_INVENTORY_MOVEMENT.InOut,TSPL_INVENTORY_MOVEMENT.Location_Code,TSPL_INVENTORY_MOVEMENT.Item_Code,BatchTABLE.Batch_No,BatchTABLE.Manual_BatchNo,Convert (varchar,BatchTABLE.Manufacture_Date,103) as Manufacture_Date,Convert(varchar,BatchTABLE.Expiry_Date,103) as Expiry_Date,   BatchTABLE.MRP,TSPL_INVENTORY_MOVEMENT.Stock_UOM,convert(decimal(18,2), TSPL_INVENTORY_MOVEMENT.Stock_Qty*BatchTABLE.ConvRatio) as Stock_Qty,convert(decimal(18,2), TSPL_INVENTORY_MOVEMENT.FIFO_Cost*BatchTABLE.ConvRatio) as FIFO_Cost,convert(decimal(18,2), TSPL_INVENTORY_MOVEMENT.LIFO_Cost*BatchTABLE.ConvRatio) as LIFO_Cost,convert(decimal(18,2), TSPL_INVENTORY_MOVEMENT.Avg_Cost*BatchTABLE.ConvRatio) as Avg_Cost,0 as IsFromMilk,0 as MilkFatPer,0 as MilkSNFPer,0 as MilkFATKG,0 as MilkSNFKG,case when TSPL_INVENTORY_MOVEMENT.cust_code is not null and len(TSPL_INVENTORY_MOVEMENT.cust_code)>0 then cust_code else case when TSPL_INVENTORY_MOVEMENT.Vendor_Code is not null and len(TSPL_INVENTORY_MOVEMENT.Vendor_Code)>0 then Vendor_Code else TSPL_INVENTORY_MOVEMENT.Other_Location_Code end end as SourceCode,case when TSPL_INVENTORY_MOVEMENT.cust_code is not null and len(TSPL_INVENTORY_MOVEMENT.cust_code)>0 then TSPL_INVENTORY_MOVEMENT.Cust_Name else case when TSPL_INVENTORY_MOVEMENT.Vendor_Code is not null and len(TSPL_INVENTORY_MOVEMENT.Vendor_Code)>0 then TSPL_INVENTORY_MOVEMENT.Vendor_Name else TSPL_INVENTORY_MOVEMENT.Other_Location_Desc end end as SourceName, case when TSPL_INVENTORY_MOVEMENT.cust_code is not null and len(TSPL_INVENTORY_MOVEMENT.cust_code)>0 then 'C' else case when TSPL_INVENTORY_MOVEMENT.Vendor_Code is not null and len(TSPL_INVENTORY_MOVEMENT.Vendor_Code)>0 then 'V' else case when TSPL_INVENTORY_MOVEMENT.Other_Location_Code is not null and len(TSPL_INVENTORY_MOVEMENT.Other_Location_Code)>0 then 'L' else '' end end end as SourceType from  (" + Environment.NewLine + _
            " select *,convert(decimal(18,8), case when TSPL_BATCH_ITEM.Qty=0 then 0 else  TSPL_BATCH_ITEM.Qty/(select sum(Qty) from TSPL_BATCH_ITEM as inn where inn.Against_Inv_Movement_Trans_Id=TSPL_BATCH_ITEM.Against_Inv_Movement_Trans_Id) end) as ConvRatio from TSPL_BATCH_ITEM  " + Environment.NewLine + _
            " ) as BatchTABLE " + Environment.NewLine + _
            " inner join TSPL_INVENTORY_MOVEMENT on TSPL_INVENTORY_MOVEMENT.Trans_Id=BatchTABLE.Against_Inv_Movement_Trans_Id and TSPL_INVENTORY_MOVEMENT.Source_Doc_No=BatchTABLE.Document_Code" + Environment.NewLine + _
            " Union All " + Environment.NewLine + _
            " select TSPL_INVENTORY_MOVEMENT_NEW.Trans_Id,TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type,TSPL_INVENTORY_MOVEMENT_NEW.Source_Doc_No,TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date,TSPL_INVENTORY_MOVEMENT_NEW.InOut,TSPL_INVENTORY_MOVEMENT_NEW.Location_Code,TSPL_INVENTORY_MOVEMENT_NEW.Item_Code,BatchTABLE_New.Batch_No,'' as Manual_BatchNo,'' as Manufacture_Date,'' as Expiry_Date,   0 as MRP,TSPL_INVENTORY_MOVEMENT_NEW.Stock_UOM,convert(decimal(18,2), TSPL_INVENTORY_MOVEMENT_NEW.Stock_Qty*BatchTABLE_New.ConvRatio) as Stock_Qty,convert(decimal(18,2), TSPL_INVENTORY_MOVEMENT_NEW.FIFO_Cost*BatchTABLE_New.ConvRatio) as FIFO_Cost,convert(decimal(18,2), TSPL_INVENTORY_MOVEMENT_NEW.LIFO_Cost*BatchTABLE_New.ConvRatio) as LIFO_Cost,convert(decimal(18,2), TSPL_INVENTORY_MOVEMENT_NEW.Avg_Cost*BatchTABLE_New.ConvRatio) as Avg_Cost,1 as IsFromMilk,TSPL_INVENTORY_MOVEMENT_NEW.Fat_Per as MilkFatPer,TSPL_INVENTORY_MOVEMENT_NEW.SNF_Per as MilkSNFPer,(TSPL_INVENTORY_MOVEMENT_NEW.FAT_KG*BatchTABLE_New.ConvRatio) as MilkFATKG,(TSPL_INVENTORY_MOVEMENT_NEW.SNF_KG*BatchTABLE_New.ConvRatio) as MilkSNFKG, case when TSPL_INVENTORY_MOVEMENT_NEW.cust_code is not null and len(TSPL_INVENTORY_MOVEMENT_NEW.cust_code)>0 then cust_code else case when TSPL_INVENTORY_MOVEMENT_NEW.Vendor_Code is not null and len(TSPL_INVENTORY_MOVEMENT_NEW.Vendor_Code)>0 then Vendor_Code else TSPL_INVENTORY_MOVEMENT_NEW.Other_Location_Code end end as SourceCode ,case when TSPL_INVENTORY_MOVEMENT_NEW.cust_code is not null and len(TSPL_INVENTORY_MOVEMENT_NEW.cust_code)>0 then TSPL_INVENTORY_MOVEMENT_NEW.Cust_Name else case when TSPL_INVENTORY_MOVEMENT_NEW.Vendor_Code is not null and len(TSPL_INVENTORY_MOVEMENT_NEW.Vendor_Code)>0 then TSPL_INVENTORY_MOVEMENT_NEW.Vendor_Name else TSPL_INVENTORY_MOVEMENT_NEW.Other_Location_Desc end end as SourceName, case when TSPL_INVENTORY_MOVEMENT_NEW.cust_code is not null and len(TSPL_INVENTORY_MOVEMENT_NEW.cust_code)>0 then 'C' else case when TSPL_INVENTORY_MOVEMENT_NEW.Vendor_Code is not null and len(TSPL_INVENTORY_MOVEMENT_NEW.Vendor_Code)>0 then 'V' else case when TSPL_INVENTORY_MOVEMENT_NEW.Other_Location_Code is not null and len(TSPL_INVENTORY_MOVEMENT_NEW.Other_Location_Code)>0 then 'L' else '' end end end as SourceType from  ( " + Environment.NewLine + _
            " select *,convert(decimal(18,8), case when TSPL_BATCH_ITEM_NEW.Qty=0 then 0 else  TSPL_BATCH_ITEM_NEW.Qty/(select sum(Qty) from TSPL_BATCH_ITEM_NEW as inn where inn.Against_Inv_Movement_New_Trans_Id=TSPL_BATCH_ITEM_NEW.Against_Inv_Movement_New_Trans_Id) end) as ConvRatio from TSPL_BATCH_ITEM_NEW  " + Environment.NewLine + _
            " ) as BatchTABLE_New  " + Environment.NewLine + _
            " inner join TSPL_INVENTORY_MOVEMENT_NEW on TSPL_INVENTORY_MOVEMENT_NEW.Trans_Id=BatchTABLE_New.Against_Inv_Movement_New_Trans_Id and TSPL_INVENTORY_MOVEMENT_NEW.Source_Doc_No=BatchTABLE_New.Document_Code " + Environment.NewLine
            qry += ") InventroyMovement " + Environment.NewLine
            qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=InventroyMovement.Item_Code" + Environment.NewLine
            qry += " left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code" + Environment.NewLine
            qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = InventroyMovement.Location_Code " + Environment.NewLine
            qry += " left outer join TSPL_LOCATION_MASTER as MainLocationTable on MainLocationTable.Location_Code =(case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end)"
            qry += " left outer join TSPL_ITEM_UOM_DETAIL as FATSNFConvertedUnit on FATSNFConvertedUnit.Item_Code=InventroyMovement.Item_Code and FATSNFConvertedUnit.UOM_Code='KG'"
            qry += " left outer join TSPL_INVENTORY_SOURCE_CODE on TSPL_INVENTORY_SOURCE_CODE.code=InventroyMovement.Trans_Type"
            If clsCommon.myLen(cmbUnit.SelectedValue) > 0 Then
                qry += " inner join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=InventroyMovement.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='" + clsCommon.myCstr(cmbUnit.SelectedValue) + "'"
            End If
            If clsCommon.myLen(strCategoryTable) > 0 Then
                qry += " left outer join (" + strCategoryTable + ") as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=InventroyMovement.Item_Code"
            End If
            qry += " left outer join (" & strItemGroup & ") as Item_Group on Item_Group.Structure_Code =TSPL_ITEM_MASTER.Structure_Code "
            qry += " left outer join (" & FrmItemMasterRMOther.LoadItemTypeQuery() & ") as VirtualTableItemType on VirtualTableItemType.Code = TSPL_ITEM_MASTER.Item_Type "
            qry += " Where 2=2 "
            If Not chkIncludeGIT.Checked Then
                qry += " and TSPL_LOCATION_MASTER.GIT_Type<>'Y' and MainLocationTable.GIT_Type<>'Y'"
            End If
            If txtItemType.arrValueMember IsNot Nothing AndAlso txtItemType.arrValueMember.Count > 0 Then
                qry += " and TSPL_ITEM_MASTER.Item_Type in (" + clsCommon.GetMulcallString(txtItemType.arrValueMember) + ") " + Environment.NewLine
            End If
            qry += "  ) xxxxx "

            qry += " where 2=2 "

            If isDataLoad AndAlso SkipCheckFatAndSNF Then
                ''never want to check fat% and snf% cond. when open from double click in production(26/05/2014)
            Else
                If chkFATAndSNF.Checked Then
                    qry += " and (MilkFatPer<>0 or FatPer<>0  or  MilkSNFPer<>0 or SNFPer<>0) "
                Else
                    qry += " and (MilkFatPer=0 and FatPer=0  and  MilkSNFPer=0 and SNFPer=0) "
                End If
            End If


            If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                qry += " and Item_Code in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ") " + Environment.NewLine
            End If
            If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
                qry += " and Trans_Type in (" + clsCommon.GetMulcallString(txtTransaction.arrValueMember) + ") " + Environment.NewLine
            End If
            If txtItemGroup.arrValueMember IsNot Nothing AndAlso txtItemGroup.arrValueMember.Count > 0 Then
                qry += " and coalesce(xxxxx.Item_Group,'') in (" + clsCommon.GetMulcallString(txtItemGroup.arrValueMember) + ") "
            End If
            If clsCommon.CompairString(clsCommon.myCstr(cboInOutType.SelectedValue), "In") = CompairStringResult.Equal Then
                qry += " and xxxxx.InOut='I'"
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboInOutType.SelectedValue), "Out") = CompairStringResult.Equal Then
                qry += " and xxxxx.InOut='O'"
            End If


            Dim strWhrCatg As String = ""
            Dim LocationFirstTime As Integer = 0
            Dim LocationAddress As String = String.Empty

            If rbtnLocationSelect.IsChecked Then
                Dim IsApplicable As Boolean = False
                For ii As Integer = 0 To gvLocation.RowCount - 1
                    If clsCommon.myCBool(gvLocation.Rows(ii).Cells("SEL").Value) Then
                        LocationFirstTime += 1
                        If LocationFirstTime = 1 Then
                            LocationAddress = clsDBFuncationality.getSingleValue("select TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then ''  else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_LOCATION_MASTER.Pin_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_LOCATION_MASTER.Pin_Code) End End End  as [LocAddress] from TSPL_LOCATION_MASTER where Location_Code= '" & clsCommon.myCstr(gvLocation.Rows(ii).Cells("CODE").Value) & "'")
                        End If
                        If IsApplicable Then
                            strWhrCatg += " Or "
                        End If
                        strWhrCatg += " ((case when Is_Section='N' and Is_Sub_Location='N' then Location_Code else Main_Location_Code end) = '" + clsCommon.myCstr(gvLocation.Rows(ii).Cells("CODE").Value) + "') "
                        IsApplicable = True
                        Dim arr As Dictionary(Of String, Object) = gvLocation.Rows(ii).Tag
                        If arr IsNot Nothing AndAlso arr.Count > 0 Then
                            strWhrCatg += " and Location_Code in ("
                            Dim isFirstTime As Boolean = True
                            For Each strInn As String In arr.Keys
                                If Not isFirstTime Then
                                    strWhrCatg += ","
                                End If
                                strWhrCatg += "'" + strInn + "'"
                                isFirstTime = False
                            Next
                            strWhrCatg += ")"
                        End If
                    End If
                Next
                If Not IsApplicable Then
                    Throw New Exception("Please select at least one location")
                End If
                qry += " and (" + strWhrCatg + ")"
            End If



            strWhrCatg = ""
            If rbtnCategorySelect.IsChecked Then
                Dim IsApplicable As Boolean = False
                For ii As Integer = 0 To gvCategory.RowCount - 1
                    If clsCommon.myCBool(gvCategory.Rows(ii).Cells("SEL").Value) Then
                        If IsApplicable Then
                            strWhrCatg += " and "
                        End If
                        IsApplicable = True
                        strWhrCatg += "("
                        Dim arr As Dictionary(Of String, Object) = gvCategory.Rows(ii).Tag
                        If arr IsNot Nothing AndAlso arr.Count > 0 Then
                            strWhrCatg += " [" + clsCommon.myCstr(gvCategory.Rows(ii).Cells("CODE").Value) + "] in ("
                            Dim isFirstTime As Boolean = True
                            For Each strInn As String In arr.Keys
                                If Not isFirstTime Then
                                    strWhrCatg += ","
                                End If
                                strWhrCatg += "'" + strInn + "'"
                                isFirstTime = False
                            Next
                            strWhrCatg += ")"
                        Else
                            strWhrCatg += " 2=2  "
                        End If
                        strWhrCatg += ")"
                    End If
                Next
                If Not IsApplicable Then
                    Throw New Exception("Please select at least one category")
                End If
                qry += " and (" + strWhrCatg + ")"
            End If


            ''End of Base Quert start Now

            Dim OuterOpClo As String = String.Empty
            Dim InnerOpClo As String = String.Empty
            'OuterOpClo = " [OPBal],(case when OPBal=0 then 0 else OPBalCost/OPBal end) as OPBalrate,case when convert(decimal(18,2),[OPQTYKG])=0 then 0 else convert(decimal(18,2),([OPFAT]*100/[OPQTYKG])) end as [OPFATPER],convert(decimal(18,2),[OPFAT]) as [OPFAT],case when convert(decimal(18,2),[OPQTYKG])=0 then 0 else convert(decimal(18,2),([OPSNF]*100/[OPQTYKG])) end as [OPSNFPER],convert(decimal(18,2), [OPSNF]) as [OPSNF],OPBalCost,Received_Qty,(case when Received_Qty=0 then 0 else RecdCost/Received_Qty end) as RecdRate, RecdCost ,case when convert(decimal(18,2),Received_QtyKG)=0 then 0 else convert(decimal(18,2),(Received_FAT*100/Received_QtyKG)) end as Received_FATPER,convert(decimal(18,2), Received_FAT) as Received_FAT,case when convert(decimal(18,2),Received_QTYKG)=0 then 0 else convert(decimal(18,2),(Received_SNF*100/Received_QTYKG)) end as Received_SNFPER,convert(decimal(18,2), Received_SNF) as Received_SNF,convert(decimal(18,2),Issued_Qty) as Issued_Qty, (case when Issued_Qty=0 then 0 else IssueCost/Issued_Qty end) as IssueRate,IssueCost,case when convert(decimal(18,2),Issued_QTYKG)=0 then 0 else convert(decimal(18,2),(Issued_FAT*100/Issued_QTYKG)) end as Issued_FATPER,convert(decimal(18,2), Issued_FAT) as Issued_FAT,case when convert(decimal(18,2),Issued_QTYKG)=0 then 0 else convert(decimal(18,2),(Issued_SNF*100/Issued_QTYKG)) end as Issued_SNFPER,convert(decimal(18,2) ,Issued_SNF) as Issued_SNF  ,[Balance_Qty],convert(decimal(18,3), case when Balance_Qty=0 then 0 else Cost/Balance_Qty end) as Rate,Cost,case when convert(decimal(18,2),[Balance_QTYKG])=0 then 0 else convert(decimal(18,2),([Balance_FAT]*100/[Balance_QTYKG])) end as [Balance_FATPER],convert(decimal(18,2), [Balance_FAT]) as  [Balance_FAT],case when convert(decimal(18,2),[Balance_QTYKG])=0 then 0 else convert(decimal(18,2),([Balance_SNF]*100/[Balance_QTYKG])) end as [Balance_SNFPER],convert(decimal(18,2), [Balance_SNF]) as [Balance_SNF] "
            OuterOpClo = " [OPBal],(case when OPBal=0 then 0 else OPBalCost/OPBal end) as OPBalrate,case when convert(decimal(18,2),[OPQTYKG])=0 then 0 else convert(decimal(18,2),([OPFAT]*100/[OPQTYKG])) end as [OPFATPER],convert(decimal(18,2),[OPFAT]) as [OPFAT],case when convert(decimal(18,2),[OPQTYKG])=0 then 0 else convert(decimal(18,2),([OPSNF]*100/[OPQTYKG])) end as [OPSNFPER],convert(decimal(18,2), [OPSNF]) as [OPSNF] " & _
            ",case when convert(decimal(18,2),[OPQTYKG])=0 then 0 else convert(decimal(18,2),([OPFAT]*100/[OPQTYKG])) end +case when convert(decimal(18,2),[OPQTYKG])=0 then 0 else convert(decimal(18,2),([OPSNF]*100/[OPQTYKG])) end as [OP TS %] ,convert(decimal(18,2),[OPFAT])+convert(decimal(18,2), [OPSNF]) as [OP TS (KG)] " & _
            ",OPBalCost,Received_Qty,(case when Received_Qty=0 then 0 else RecdCost/Received_Qty end) as RecdRate, RecdCost ,case when convert(decimal(18,2),Received_QtyKG)=0 then 0 else convert(decimal(18,2),(Received_FAT*100/Received_QtyKG)) end as Received_FATPER,convert(decimal(18,2), Received_FAT) as Received_FAT,case when convert(decimal(18,2),Received_QTYKG)=0 then 0 else convert(decimal(18,2),(Received_SNF*100/Received_QTYKG)) end as Received_SNFPER,convert(decimal(18,2), Received_SNF) as Received_SNF" & _
            ",case when convert(decimal(18,2),Received_QtyKG)=0 then 0 else convert(decimal(18,2),(Received_FAT*100/Received_QtyKG)) end+case when convert(decimal(18,2),Received_QTYKG)=0 then 0 else convert(decimal(18,2),(Received_SNF*100/Received_QTYKG)) end as [Received TS %],convert(decimal(18,2), Received_FAT)+convert(decimal(18,2), Received_SNF) as [Received TS (KG)]" & _
            ",convert(decimal(18,2),Issued_Qty) as Issued_Qty, (case when Issued_Qty=0 then 0 else IssueCost/Issued_Qty end) as IssueRate,IssueCost,case when convert(decimal(18,2),Issued_QTYKG)=0 then 0 else convert(decimal(18,2),(Issued_FAT*100/Issued_QTYKG)) end as Issued_FATPER,convert(decimal(18,2), Issued_FAT) as Issued_FAT,case when convert(decimal(18,2),Issued_QTYKG)=0 then 0 else convert(decimal(18,2),(Issued_SNF*100/Issued_QTYKG)) end as Issued_SNFPER,convert(decimal(18,2) ,Issued_SNF) as Issued_SNF " & _
            ",case when convert(decimal(18,2),Issued_QTYKG)=0 then 0 else convert(decimal(18,2),(Issued_FAT*100/Issued_QTYKG)) end+case when convert(decimal(18,2),Issued_QTYKG)=0 then 0 else convert(decimal(18,2),(Issued_SNF*100/Issued_QTYKG)) end as [Issued TS %],convert(decimal(18,2), Issued_FAT)+convert(decimal(18,2) ,Issued_SNF) as [Issued TS (KG)]" & _
            " ,[Balance_Qty],convert(decimal(18,3), case when Balance_Qty=0 then 0 else Cost/Balance_Qty end) as Rate,Cost,case when convert(decimal(18,2),[Balance_QTYKG])=0 then 0 else convert(decimal(18,2),([Balance_FAT]*100/[Balance_QTYKG])) end as [Balance_FATPER],convert(decimal(18,2), [Balance_FAT]) as  [Balance_FAT],case when convert(decimal(18,2),[Balance_QTYKG])=0 then 0 else convert(decimal(18,2),([Balance_SNF]*100/[Balance_QTYKG])) end as [Balance_SNFPER],convert(decimal(18,2), [Balance_SNF]) as [Balance_SNF] " & _
            ",case when convert(decimal(18,2),[Balance_QTYKG])=0 then 0 else convert(decimal(18,2),([Balance_FAT]*100/[Balance_QTYKG])) end + case when convert(decimal(18,2),[Balance_QTYKG])=0 then 0 else convert(decimal(18,2),([Balance_SNF]*100/[Balance_QTYKG])) end as [Balance TS %],convert(decimal(18,2), [Balance_FAT])+convert(decimal(18,2), [Balance_SNF]) as [Balance TS (KG)]"

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
                            " SUM((case when IsFromMilk=1 then MilkSNFKG else (STOCK_QTY*SNFPer) end ) * (CASE WHEN PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'  THEN 1 ELSE 0 end) * (case when InOut='I' then 1 else -1 end))  AS [Balance_SNF]"

            Dim strFinalQry As String = ""
            If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item Type Wise Summary") = CompairStringResult.Equal Then
                strFinalQry = "select  Item_Type,Item_Type_Name, "
                strFinalQry += OuterOpClo
                strFinalQry += " from (" + Environment.NewLine
                strFinalQry += " select  Item_Type,max(Item_Type_Name) as Item_Type_Name,"
                strFinalQry += InnerOpClo
                strFinalQry += "  from (" + qry + ")xxx Group by Item_Type )xxxx" + Environment.NewLine
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item Group Wise Summary") = CompairStringResult.Equal Then
                strFinalQry = "select  Item_Type,Item_Type_Name,Item_Group,Group_Description, "
                strFinalQry += OuterOpClo
                strFinalQry += " from (" + Environment.NewLine
                strFinalQry += " select Item_Type,max(Item_Type_Name) as Item_Type_Name, Item_Group,max(Group_Description) as Group_Description,"
                strFinalQry += InnerOpClo
                strFinalQry += "  from (" + qry + ")xxx Group by Item_Type,Item_Group )xxxx" + Environment.NewLine
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Category Wise Summary") = CompairStringResult.Equal Then
                strFinalQry = "select  Item_Type,Item_Type_Name,Item_Group,Group_Description," + strCodeColumn + "," + strCodeDescColumn + ",Item_Category_Struct_Code,Stock_Qty,Stock_UOM, "
                strFinalQry += OuterOpClo
                strFinalQry += " from (" + Environment.NewLine
                strFinalQry += " select  Item_Type,max(Item_Type_Name) as Item_Type_Name,Item_Group,max(Group_Description) as Group_Description, " + strCodeColumn + "," + strCodeDescColumnMax + ",Item_Category_Struct_Code,SUM(Stock_Qty * case when InOut='I' then 1 else -1 end) as Stock_Qty,MAX(Stock_UOM) Stock_UOM,"
                strFinalQry += InnerOpClo
                strFinalQry += "  from (" + qry + ")xxx Group by Item_Type,Item_Group, Item_Category_Struct_Code," + strCodeColumn + " )xxxx" + Environment.NewLine
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item And Batch Wise Summary") = CompairStringResult.Equal Then
                strFinalQry = "select Item_Type,Item_Type_Name,Item_Group,Group_Description," + strCodeColumn + "," + strCodeDescColumn + ", Item_Category_Struct_Code  as Item_Category_Struct_Code,Item_Code,Item_Desc,itf_code,Batch_No,Manual_BatchNo,Manufacture_Date,Expiry_Date,MRP,Stock_Qty,Stock_UOM,"
                strFinalQry += OuterOpClo
                strFinalQry += " from (" + Environment.NewLine
                strFinalQry += " select  Item_Type,max(Item_Type_Name) as Item_Type_Name,Item_Group,max(Group_Description) as Group_Description," + strCodeColumnMax + "," + strCodeDescColumnMax + ",max(Item_Category_Struct_Code) as Item_Category_Struct_Code,Item_Code,MAX(Item_Desc) as Item_Desc,max(itf_code)as itf_code,Batch_No,max(Manual_BatchNo) as Manual_BatchNo,min(Manufacture_Date) as Manufacture_Date,max(Expiry_Date) as Expiry_Date,max(MRP) as MRP,SUM(Stock_Qty * case when InOut='I' then 1 else -1 end) as Stock_Qty,MAX(Stock_UOM) as Stock_UOM,"
                strFinalQry += InnerOpClo
                strFinalQry += " from (" + qry + ")xxx Group by Item_Type,Item_Group,Item_Code,Batch_No"
                strFinalQry += " )xxxx" + Environment.NewLine
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item Batch And Location Wise Summary") = CompairStringResult.Equal Then
                strFinalQry = "select Item_Type,Item_Type_Name,Item_Group,Group_Description," + strCodeColumn + "," + strCodeDescColumn + ", Item_Category_Struct_Code  as Item_Category_Struct_Code,Main_Location_Code,MainLocationDesc,Location_Code,[Loc Desp],Item_Code,Item_Desc,itf_code,Batch_No,Manual_BatchNo,Manufacture_Date,Expiry_Date,MRP,Stock_Qty,Stock_UOM,"
                strFinalQry += OuterOpClo
                strFinalQry += "  from (" + Environment.NewLine
                strFinalQry += " select  Item_Type,max(Item_Type_Name) as Item_Type_Name,Item_Group,max(Group_Description) as Group_Description," + strCodeColumnMax + "," + strCodeDescColumnMax + ",max(Item_Category_Struct_Code) as Item_Category_Struct_Code,Main_Location_Code,max(MainLocationDesc) as MainLocationDesc, Location_Code,max([Loc Desp]) as [Loc Desp],Item_Code,MAX(Item_Desc) as Item_Desc,max(itf_code)as itf_code,Batch_No,max(Manual_BatchNo) as Manual_BatchNo,min(Manufacture_Date) as Manufacture_Date,max(Expiry_Date) as Expiry_Date,max(MRP) as MRP, SUM(Stock_Qty * case when InOut='I' then 1 else -1 end) as Stock_Qty,MAX(Stock_UOM) Stock_UOM,"
                strFinalQry += InnerOpClo
                strFinalQry += " from (" + qry + ")xxx Group by Item_Type,Item_Group,Item_Code,Batch_No,Main_Location_Code,Location_Code"
                strFinalQry += " )xxxx" + Environment.NewLine
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Document Wise Detail") = CompairStringResult.Equal Then
                'strFinalQry = " select  case when '" & LocationFirstTime & "'='1' then '" & LocationAddress & "' else '" + objCommonVar.CurrentCompanyName + " ' end as CompName,'" + clsCommon.myCDate(txtFromDate.Value, "dd/MMM/yyyy") + "' as FromDate,'" + clsCommon.myCDate(txtToDate.Value, "dd/MMM/yyyy") + "' as ToDate , Trans_Id,Trans_Type,Trans_Type_Name,Source_Doc_No,convert(varchar,Punching_Date,103) as Punching_Date,Batch_no,Manual_BatchNo,InOutView, InOut,Location_Code,[Loc Desp],SourceCode,SourceName,SourceType ,Item_Type,Item_Type_Name,Item_Group,Group_Description," + strCodeColumn + "," + strCodeDescColumn + ",Item_Code ,Item_Desc,Item_Category_Struct_Code,Stock_UOM,itf_code ,Stock_Qty,Rate,Cost,isnull((CASE when Balance_QTYKG=0 then 0 else (Balance_FAT*100/Balance_QTYKG) end),0) as Balance_FATPER ,Balance_FAT,isnull((CASE when Balance_QTYKG=0 then 0 else ([Balance_SNF]*100/[Balance_QTYKG]) end),0) as [Balance_SNFPER] , Balance_SNF  "
                strFinalQry = " select  case when '" & LocationFirstTime & "'='1' then '" & LocationAddress & "' else '" + objCommonVar.CurrentCompanyName + " ' end as CompName,'" + clsCommon.myCDate(txtFromDate.Value, "dd/MMM/yyyy") + "' as FromDate,'" + clsCommon.myCDate(txtToDate.Value, "dd/MMM/yyyy") + "' as ToDate , Trans_Id,Trans_Type,Trans_Type_Name,Source_Doc_No,convert(varchar,Punching_Date,103) as Punching_Date,Batch_no,Manual_BatchNo,InOutView, InOut,Location_Code,[Loc Desp],SourceCode,SourceName,SourceType ,Item_Type,Item_Type_Name,Item_Group,Group_Description," + strCodeColumn + "," + strCodeDescColumn + ",Item_Code ,Item_Desc,Item_Category_Struct_Code,Stock_UOM,itf_code ,Stock_Qty,Rate,Cost,isnull((CASE when Balance_QTYKG=0 then 0 else (Balance_FAT*100/Balance_QTYKG) end),0) as Balance_FATPER ,Balance_FAT,isnull((CASE when Balance_QTYKG=0 then 0 else ([Balance_SNF]*100/[Balance_QTYKG]) end),0) as [Balance_SNFPER] , Balance_SNF ,isnull((CASE when Balance_QTYKG=0 then 0 else (Balance_FAT*100/Balance_QTYKG) end),0)+isnull((CASE when Balance_QTYKG=0 then 0 else ([Balance_SNF]*100/[Balance_QTYKG]) end),0) as [Balance TS %],Balance_FAT+Balance_SNF as [Balance TS (KG)] "
                strFinalQry += " from ("
                strFinalQry += " select 0 as Trans_Id,'' as Trans_Type,'' as Trans_Type_Name,'Opening Balance' as Source_Doc_No,null as Punching_Date ,'' as Batch_no ,'' as Manual_BatchNo,'' as InOutView, '' as InOut,'' as Location_Code,'' as [Loc Desp],'' as [LocAddress],'' as SourceCode,'' as SourceName,'' as SourceType ,'' as Item_Type,'' as Item_Type_Name,'' as Item_Group,'' as Group_Description," + strCodeColumnMax + "," + strCodeDescColumnMax + ",max(Item_Code)  as Item_Code ,max(Item_Desc)  as Item_Desc,'' as Item_Category_Struct_Code,'' as Stock_UOM,'' as itf_code ,sum( Stock_Qty * case when InOut='I' then 1 else -1 end) as Stock_Qty,sum( QtyKG * case when InOut='I' then 1 else -1 end) as Balance_QTYKG,sum(convert(decimal(18,3),case when Stock_Qty=0 then 0 else Cost/Stock_Qty end)) as Rate,sum(Cost * case when InOut='I' then 1 else -1 end) as Cost,sum( (case when IsFromMilk=1 then MilkFATKG else (Stock_Qty*FatPer) end) * case when InOut='I' then 1 else -1 end) as Balance_FAT,sum(( (case when IsFromMilk=1 then MilkSNFKG else (Stock_Qty*SNFPer) end ) * case when InOut='I' then 1 else -1 end)) as Balance_SNF "
                strFinalQry += " from (" + qry + ") xxx " + Environment.NewLine
                strFinalQry += " where Punching_Date < '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' group by Item_Code,Batch_No  " + Environment.NewLine
                strFinalQry += " union all "
                strFinalQry += " select Trans_Id,Trans_Type,Trans_Type_Name,Source_Doc_No,Punching_Date,Batch_No,Manual_BatchNo,InOutView, InOut,Location_Code,[Loc Desp], [LocAddress],SourceCode,SourceName,SourceType ,Item_Type,Item_Type_Name,Item_Group,Group_Description," + strCodeColumn + "," + strCodeDescColumn + ",Item_Code ,Item_Desc,Item_Category_Struct_Code,Stock_UOM,itf_code ,( Stock_Qty * case when InOut='I' then 1 else -1 end) as Stock_Qty,(QtyKG * case when InOut='I' then 1 else -1 end) as Balance_QTYKG,convert(decimal(18,3),case when Stock_Qty=0 then 0 else Cost/Stock_Qty end) as Rate,(Cost * case when InOut='I' then 1 else -1 end) as Cost,( (case when IsFromMilk=1 then MilkFATKG else (Stock_Qty*FatPer) end) * case when InOut='I' then 1 else -1 end) as Balance_FAT, ( (case when IsFromMilk=1 then MilkSNFKG else (Stock_Qty*SNFPer) end ) * case when InOut='I' then 1 else -1 end) as Balance_SNF  "
                strFinalQry += " from (" + qry + ") xxx " + Environment.NewLine
                strFinalQry += " where Punching_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' " + Environment.NewLine
                strFinalQry += " )xxxxxx Order by convert(date,Punching_Date,103)"
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Date, Item And Document Wise Detail") = CompairStringResult.Equal Then
                'strFinalQry = " select CompName,FromDate,ToDate,Batch_No,Manual_BatchNo,Trans_Id,Location_Code,[Loc Desp],SourceType ,Item_Type,Item_Type_Name,Item_Group,Group_Description," + strCodeColumn + "," + strCodeDescColumn + ",Item_Category_Struct_Code,itf_code ,Stock_Qty,Rate,Cost,SourceCode,SourceName,Source_Doc_No,Punching_Date,Trans_Type,Trans_Type_Name,InOut,InOutView,Item_Code ,Item_Desc,Stock_UOM, (ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0)) as OPQty, case when (ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0))=0 then 0 else  ((isnull(CLCost,0)-isnull(RecCost,0)+isnull(IssCost,0))/((ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0)))) end as OPRate, (isnull(CLBalance_FAT,0)-isnull(RecFAT,0)+isnull(IssFAT,0)) as OPFAT, isnull((CASE when (CLBalance_QTYKG+Balance_QTYKG) =0 then 0 else ((CLBalance_FAT+Balance_FAT)*100/(CLBalance_QTYKG+Balance_QTYKG)) end),0) as OPFATPER, isnull((CASE when (CLBalance_QTYKG+Balance_QTYKG) =0 then 0 else ((CLBalance_SNF+Balance_SNF)*100/(CLBalance_QTYKG+Balance_QTYKG)) end),0) as OPSNF, isnull((CASE when (CLBalance_QTYKG+Balance_QTYKG) =0 then 0 else ((CLBalance_SNF+Balance_SNF)*100/(CLBalance_QTYKG+Balance_QTYKG)) end),0) as OPSNFPER ,(isnull(CLCost,0)-isnull(RecCost,0)+isnull(IssCost,0)) as OPCost, RecQty,RecRate,RecFAT,RecFATPER,RecSNF,RecSNFPER,RecCost ,IssQty,IssRate,IssFAT,IssFATPER,IssSNF,IssSNFPER,IssCost ,CLQty ,case when isnull(CLQty,0)=0 then 0 else CLCost/CLQty end as CLRate, CLBalance_FAT as CLFAT, isnull((CASE when CLBalance_QTYKG=0 then 0 else (CLBalance_FAT*100/CLBalance_QTYKG) end),0) as CLFATPER, CLBalance_SNF as CLSNF, isnull((CASE when CLBalance_QTYKG=0 then 0 else (CLBalance_SNF*100/CLBalance_QTYKG) end),0) as CLSNFPER, CLCost from ( "
                strFinalQry = " select CompName,FromDate,ToDate,Batch_No,Manual_BatchNo,Trans_Id,Location_Code,[Loc Desp],SourceType ,Item_Type,Item_Type_Name,Item_Group,Group_Description," + strCodeColumn + "," + strCodeDescColumn + ",Item_Category_Struct_Code,itf_code ,Stock_Qty,Rate,Cost,SourceCode,SourceName,Source_Doc_No,Punching_Date,Trans_Type,Trans_Type_Name,InOut,InOutView,Item_Code ,Item_Desc,Stock_UOM, (ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0)) as OPQty, case when (ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0))=0 then 0 else  ((isnull(CLCost,0)-isnull(RecCost,0)+isnull(IssCost,0))/((ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0)))) end as OPRate, (isnull(CLBalance_FAT,0)-isnull(RecFAT,0)+isnull(IssFAT,0)) as OPFAT, isnull((CASE when (CLBalance_QTYKG+Balance_QTYKG) =0 then 0 else ((CLBalance_FAT+Balance_FAT)*100/(CLBalance_QTYKG+Balance_QTYKG)) end),0) as OPFATPER, isnull((CASE when (CLBalance_QTYKG+Balance_QTYKG) =0 then 0 else ((CLBalance_SNF+Balance_SNF)*100/(CLBalance_QTYKG+Balance_QTYKG)) end),0) as OPSNF, isnull((CASE when (CLBalance_QTYKG+Balance_QTYKG) =0 then 0 else ((CLBalance_SNF+Balance_SNF)*100/(CLBalance_QTYKG+Balance_QTYKG)) end),0) as OPSNFPER ,(isnull(CLCost,0)-isnull(RecCost,0)+isnull(IssCost,0)) as OPCost" & _
                ",isnull((CASE when (CLBalance_QTYKG+Balance_QTYKG) =0 then 0 else ((CLBalance_FAT+Balance_FAT)*100/(CLBalance_QTYKG+Balance_QTYKG)) end),0)+isnull((CASE when (CLBalance_QTYKG+Balance_QTYKG) =0 then 0 else ((CLBalance_SNF+Balance_SNF)*100/(CLBalance_QTYKG+Balance_QTYKG)) end),0) as [OP TS %],(isnull(CLBalance_FAT,0)-isnull(RecFAT,0)+isnull(IssFAT,0))+isnull((CASE when (CLBalance_QTYKG+Balance_QTYKG) =0 then 0 else ((CLBalance_SNF+Balance_SNF)*100/(CLBalance_QTYKG+Balance_QTYKG)) end),0) as [OP TS (KG)]" & _
                ", RecQty,RecRate,RecFAT,RecFATPER,RecSNF,RecSNFPER,RecCost " & _
                ",RecFATPER+RecSNFPER as [Received TS %],RecFAT+RecSNF as [Received TS (KG)]" & _
                ",IssQty,IssRate,IssFAT,IssFATPER,IssSNF,IssSNFPER,IssCost " & _
                ",IssFATPER+IssSNFPER as [Issued TS %],IssFAT+IssSNF as [Issued TS (KG)]" & _
                ",CLQty ,case when isnull(CLQty,0)=0 then 0 else CLCost/CLQty end as CLRate, CLBalance_FAT as CLFAT, isnull((CASE when CLBalance_QTYKG=0 then 0 else (CLBalance_FAT*100/CLBalance_QTYKG) end),0) as CLFATPER, CLBalance_SNF as CLSNF, isnull((CASE when CLBalance_QTYKG=0 then 0 else (CLBalance_SNF*100/CLBalance_QTYKG) end),0) as CLSNFPER, CLCost" & _
                ",isnull((CASE when CLBalance_QTYKG=0 then 0 else (CLBalance_FAT*100/CLBalance_QTYKG) end),0)+isnull((CASE when CLBalance_QTYKG=0 then 0 else (CLBalance_SNF*100/CLBalance_QTYKG) end),0) as [Balance TS %],CLBalance_FAT+CLBalance_SNF as [Balance TS (KG)]" & _
                " from ( "
                strFinalQry += " select  case when '" & LocationFirstTime & "'='1' then '" & LocationAddress & "' else '" + objCommonVar.CurrentCompanyName + " ' end as CompName,'" + clsCommon.myCDate(txtFromDate.Value, "dd/MMM/yyyy") + "' as FromDate,'" + clsCommon.myCDate(txtToDate.Value, "dd/MMM/yyyy") + "' as ToDate,Batch_No ,Manual_BatchNo,  Trans_Id,Location_Code,[Loc Desp],SourceType ,Item_Type,Item_Type_Name,Item_Group,Group_Description ," + strCodeColumn + "," + strCodeDescColumn + ", Item_Category_Struct_Code,itf_code ,Stock_Qty,Rate,Cost,SourceCode,SourceName,Source_Doc_No,Punching_Date as Punching_Date,Trans_Type,Trans_Type_Name,InOut,InOutView, Item_Code ,Item_Desc,Stock_UOM,Balance_FAT,Balance_SNF,isnull(Balance_QTYKG,0) as Balance_QTYKG, (case when InOut='I' then Stock_Qty else 0 end) as RecQty,  (case when InOut='I' then Rate else 0 end) as RecRate, (case when InOut='I' then Balance_FAT else 0 end) as RecFAT, (case when InOut='I' then isnull((CASE when Balance_QTYKG=0 then 0 else (Balance_FAT*100/Balance_QTYKG) end),0) else 0 end ) as RecFATPER, (case when InOut='I' then Balance_SNF else 0 end) as RecSNF, (case when InOut='I' then isnull((CASE when Balance_QTYKG=0 then 0 else ([Balance_SNF]*100/[Balance_QTYKG]) end),0) else 0 end) as RecSNFPER, (case when InOut='I' then Cost else 0 end) as RecCost, (case when InOut='O' then -1*Stock_Qty else 0 end) as IssQty, (case when InOut='O' then Rate else 0 end) as IssRate, (case when InOut='O' then -1*Balance_FAT else 0 end) as IssFAT,(case when InOut='O' then isnull((CASE when Balance_QTYKG=0 then 0 else (Balance_FAT*100/Balance_QTYKG) end),0) else 0 end ) as IssFATPER, (case when InOut='O' then -1*Balance_SNF else 0 end) as IssSNF, (case when InOut='O' then isnull((CASE when Balance_QTYKG=0 then 0 else ([Balance_SNF]*100/[Balance_QTYKG]) end),0) else 0 end) as IssSNFPER, (case when InOut='O' then -1*Cost else 0 end) as IssCost, SUM(Stock_Qty) OVER (Partition BY Item_Code ORDER BY Item_Code, Punching_Date,Trans_Id) as CLQty  ,SUM(Cost) OVER (Partition BY Item_Code ORDER BY Item_Code, Punching_Date,Trans_Id) as CLCost,SUM(isnull(Balance_QTYKG,0)) OVER (Partition BY Item_Code ORDER BY Item_Code, Punching_Date,Trans_Id) as CLBalance_QTYKG ,SUM(Balance_FAT) OVER (Partition BY Item_Code ORDER BY Item_Code, Punching_Date,Trans_Id) as CLBalance_FAT,SUM(Balance_SNF) OVER (Partition BY Item_Code ORDER BY Item_Code, Punching_Date,Trans_Id) as CLBalance_SNF "
                strFinalQry += " from ("
                strFinalQry += " select 0 as Trans_Id,'' as Trans_Type,'' as Trans_Type_Name,'Opening Balance' as Source_Doc_No,null as Punching_Date,'' as Batch_No,'' as Manual_BatchNo,'' as InOutView, '' as InOut,'' as Location_Code,'' as [Loc Desp],'' as [LocAddress],'' as SourceCode,'' as SourceName,'' as SourceType ,'' as Item_Type,'' as Item_Type_Name,'' as Item_Group,'' as Group_Description," + strCodeColumnMax + "," + strCodeDescColumnMax + ",xxx.Item_Code as Item_Code ,max(xxx.Item_Desc) as Item_Desc,'' as Item_Category_Struct_Code,max(xxx.Stock_UOM) as Stock_UOM,'' as itf_code ,sum( Stock_Qty * case when InOut='I' then 1 else -1 end) as Stock_Qty,sum( QtyKG * case when InOut='I' then 1 else -1 end) as Balance_QTYKG, case when sum(convert(decimal(18,3),Stock_Qty* case when InOut='I' then 1 else -1 end))=0 then 0 else sum(convert(decimal(18,3),Cost* case when InOut='I' then 1 else -1 end))/sum(convert(decimal(18,3),Stock_Qty* case when InOut='I' then 1 else -1 end)) end as Rate,sum(Cost * case when InOut='I' then 1 else -1 end) as Cost,sum( (case when IsFromMilk=1 then MilkFATKG else (Stock_Qty*FatPer) end) * case when InOut='I' then 1 else -1 end) as Balance_FAT,sum(( (case when IsFromMilk=1 then MilkSNFKG else (Stock_Qty*SNFPer) end ) * case when InOut='I' then 1 else -1 end)) as Balance_SNF "
                strFinalQry += " from (" + qry + ") xxx " + Environment.NewLine
                strFinalQry += " where Punching_Date < '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' group by xxx.Item_Code,xxx.Batch_No " + Environment.NewLine
                strFinalQry += " union all "
                strFinalQry += " select Trans_Id,Trans_Type,Trans_Type_Name,Source_Doc_No,Punching_Date,Batch_No,Manual_BatchNo,InOutView, InOut,Location_Code,[Loc Desp], [LocAddress],SourceCode,SourceName,SourceType ,Item_Type,Item_Type_Name,Item_Group,Group_Description," + strCodeColumn + "," + strCodeDescColumn + ",Item_Code ,Item_Desc,Item_Category_Struct_Code,Stock_UOM,itf_code ,( Stock_Qty * case when InOut='I' then 1 else -1 end) as Stock_Qty,(QtyKG * case when InOut='I' then 1 else -1 end) as Balance_QTYKG,convert(decimal(18,3),case when Stock_Qty=0 then 0 else Cost/Stock_Qty end) as Rate,(Cost * case when InOut='I' then 1 else -1 end) as Cost,( (case when IsFromMilk=1 then MilkFATKG else (Stock_Qty*FatPer) end) * case when InOut='I' then 1 else -1 end) as Balance_FAT, ( (case when IsFromMilk=1 then MilkSNFKG else (Stock_Qty*SNFPer) end ) * case when InOut='I' then 1 else -1 end) as Balance_SNF  "
                strFinalQry += " from (" + qry + ") xxx " + Environment.NewLine
                strFinalQry += " where Punching_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' " + Environment.NewLine
                strFinalQry += " )xxxxxx  )xxxxxxx where Trans_Id<>0  Order by  Punching_Date,Trans_Id"
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Date and Item Wise Stock") = CompairStringResult.Equal Then
                strFinalQry = " select Location_Code,[Loc Desp],convert(varchar, Punching_Date,103) as Punching_Date  ,Item_Code ,Item_Desc,Stock_UOM, (ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0)) as OPQty, case when (ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0))=0 then 0 else  ((isnull(CLCost,0)-isnull(RecCost,0)+isnull(IssCost,0))/((ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0)))) end as OPRate , (isnull(CLBalance_FAT,0)-isnull(RecFAT,0)+isnull(IssFAT,0)) as OPFAT,isnull((CASE when (CLBalance_QTYKG+Balance_QTYKG) =0 then 0 else ((CLBalance_FAT+Balance_FAT)*100/(CLBalance_QTYKG+Balance_QTYKG)) end),0) as OPFATPER,(isnull(CLBalance_SNF,0)-isnull(RecSNF,0)+isnull(IssSNF,0)) as OPSNF,isnull((CASE when (CLBalance_QTYKG+Balance_QTYKG) =0 then 0 else ((CLBalance_SNF+Balance_SNF)*100/(CLBalance_QTYKG+Balance_QTYKG)) end),0) as OPSNFPER ,(isnull(CLCost,0)-isnull(RecCost,0)+isnull(IssCost,0)) as OPCost, RecPurQty,RecPurRate,RecPurFAT,RecPurFATPER,RecPurSNF,RecPurSNFPER,RecPurCost ,RecAdjProQty,RecAdjProRate ,RecAdjProFAT ,RecAdjProFATPER,RecAdjProSNF ,RecAdjProSNFPER ,RecAdjProCost ,RecOthQty,RecOthRate ,RecOthFAT ,RecOthFATPER ,RecOthSNF,RecOthSNFPER ,RecOthCost,RecQty,RecRate,RecFAT,RecFATPER,RecSNF,RecSNFPER,RecCost  ,IssSaleQty ,IssSaleRate ,IssSaleFAT ,IssSaleFATPER,IssSaleSNF,IssSaleSNFPER ,IssSaleCost , IssIssAdjQty , IssIssAdjRate , IssIssAdjFAT , IssIssAdjFATPER ,IssIssAdjSNF , IssIssAdjSNFPER ,IssIssAdjCost , IssOthQty , IssOthRate , IssOthFAT ,IssOthFATPER , IssOthSNF ,IssOthSNFPER ,IssOthCost ,IssQty,IssRate,IssFAT,IssFATPER,IssSNF,IssSNFPER,IssCost ,CLQty ,case when isnull(CLQty,0)=0 then 0 else CLCost/CLQty end as CLRate, CLBalance_FAT as CLFAT, isnull((CASE when CLBalance_QTYKG=0 then 0 else (CLBalance_FAT*100/CLBalance_QTYKG) end),0) as CLFATPER, CLBalance_SNF as CLSNF, isnull((CASE when CLBalance_QTYKG=0 then 0 else (CLBalance_SNF*100/CLBalance_QTYKG) end),0) as CLSNFPER, CLCost from ( "
                strFinalQry += " select  Location_Code,max([Loc Desp]) as [Loc Desp],Punching_Date as Punching_Date, Item_Code ,max(Item_Desc) as Item_Desc, max(Stock_UOM) as Stock_UOM, sum(Balance_FAT) as Balance_FAT,sum(Balance_SNF) as Balance_SNF  ,sum(isnull(Balance_QTYKG,0)) as Balance_QTYKG ,sum(case when InOut='I' and In_Category in ('PU') then Stock_Qty else 0 end) as RecPurQty ,sum(case when InOut='I' and In_Category in ('PU') then Rate else 0 end) as RecPurRate  ,sum(case when InOut='I' and In_Category in ('PU') then Balance_FAT else 0 end) as RecPurFAT  ,(case when sum(case when InOut='I' and In_Category in ('PU') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category in ('PU') then  Balance_FAT else 0 end)*100/sum(case when InOut='I' and In_Category in ('PU') then  Balance_QTYKG else 0 end) end)  as RecPurFATPER  ,sum(case when InOut='I' and In_Category in ('PU') then Balance_SNF else 0 end) as RecPurSNF  ,(case when sum(case when InOut='I' and In_Category in ('PU') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category in ('PU') then  Balance_SNF else 0 end)*100/sum(case when InOut='I' and In_Category in ('PU') then  Balance_QTYKG else 0 end) end)  as RecPurSNFPER  ,sum(case when InOut='I' and In_Category in ('PU') then Cost else 0 end) as RecPurCost  ,sum(case when InOut='I' and In_Category in ('AD') then Stock_Qty else 0 end) as RecAdjProQty  ,sum(case when InOut='I' and In_Category in ('AD') then Rate else 0 end) as RecAdjProRate  ,sum(case when InOut='I' and In_Category in ('AD') then Balance_FAT else 0 end) as RecAdjProFAT  ,(case when sum(case when InOut='I' and In_Category in ('AD') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category in ('AD') then  Balance_FAT else 0 end)*100/sum(case when InOut='I' and In_Category in ('AD') then  Balance_QTYKG else 0 end) end)  as RecAdjProFATPER  ,sum(case when InOut='I' and In_Category in ('AD') then Balance_SNF else 0 end) as RecAdjProSNF  ,(case when sum(case when InOut='I' and In_Category in ('AD') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category in ('AD') then  Balance_SNF else 0 end)*100/sum(case when InOut='I' and In_Category in ('AD') then  Balance_QTYKG else 0 end) end)  as RecAdjProSNFPER  ,sum(case when InOut='I' and In_Category in ('AD') then Cost else 0 end) as RecAdjProCost ,sum(case when InOut='I' and In_Category not in ('AD','PU') then Stock_Qty else 0 end) as RecOthQty  ,sum(case when InOut='I' and In_Category not in ('AD','PU') then Rate else 0 end) as RecOthRate  ,sum(case when InOut='I' and In_Category not in ('AD','PU') then Balance_FAT else 0 end) as RecOthFAT  ,(case when sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_FAT else 0 end)*100/sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_QTYKG else 0 end) end)  as RecOthFATPER  ,sum(case when InOut='I' and In_Category not in ('AD','PU') then Balance_SNF else 0 end) as RecOthSNF ,(case when sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_SNF else 0 end)*100/sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_QTYKG else 0 end) end)  as RecOthSNFPER  ,sum(case when InOut='I' and In_Category not in ('AD','PU') then Cost else 0 end) as RecOthCost ,sum(case when InOut='I' then Stock_Qty else 0 end) as RecQty  ,sum(case when InOut='I' then Rate else 0 end) as RecRate  ,sum(case when InOut='I' then Balance_FAT else 0 end) as RecFAT,(case when sum(case when InOut='I' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' then  Balance_FAT else 0 end)*100/sum(case when InOut='I' then  Balance_QTYKG else 0 end) end)  as RecFATPER  ,sum(case when InOut='I' then Balance_SNF else 0 end) as RecSNF  ,(case when sum(case when InOut='I' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' then  Balance_SNF else 0 end)*100/sum(case when InOut='I' then  Balance_QTYKG else 0 end) end)  as RecSNFPER  ,sum(case when InOut='I' then Cost else 0 end) as RecCost,sum(case when InOut='O' and Out_Category in ('SA') then -1*Stock_Qty else 0 end) as IssSaleQty  ,sum(case when InOut='O' and Out_Category in ('SA') then Rate else 0 end) as IssSaleRate  ,sum(case when InOut='O' and Out_Category in ('SA') then -1*Balance_FAT else 0 end) as IssSaleFAT  ,(case when sum(case when InOut='O' and Out_Category in ('SA') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category in ('SA') then  Balance_FAT else 0 end)*100/sum(case when InOut='O' and Out_Category in ('SA') then  Balance_QTYKG else 0 end) end)  as IssSaleFATPER  ,sum(case when InOut='O' and Out_Category in ('SA') then -1*Balance_SNF else 0 end) as IssSaleSNF  ,(case when sum(case when InOut='O' and Out_Category in ('SA') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category in ('SA') then  Balance_SNF else 0 end)*100/sum(case when InOut='O' and Out_Category in ('SA') then  Balance_QTYKG else 0 end) end)  as IssSaleSNFPER  ,sum(case when InOut='O' and Out_Category in ('SA') then -1*Cost else 0 end) as IssSaleCost  ,sum(case when InOut='O' and Out_Category in ('IS') then -1*Stock_Qty else 0 end) as IssIssAdjQty  ,sum(case when InOut='O' and Out_Category in ('IS') then Rate else 0 end) as IssIssAdjRate  ,sum(case when InOut='O' and Out_Category in ('IS') then -1*Balance_FAT else 0 end) as IssIssAdjFAT  ,(case when sum(case when InOut='O' and Out_Category in ('IS') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category in ('IS') then  Balance_FAT else 0 end)*100/sum(case when InOut='O' and Out_Category in ('IS') then  Balance_QTYKG else 0 end) end)  as IssIssAdjFATPER  ,sum(case when InOut='O' and Out_Category in ('IS') then -1*Balance_SNF else 0 end) as IssIssAdjSNF  ,(case when sum(case when InOut='O' and Out_Category in ('IS') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category in ('IS') then  Balance_SNF else 0 end)*100/sum(case when InOut='O' and Out_Category in ('IS') then  Balance_QTYKG else 0 end) end)  as IssIssAdjSNFPER  ,sum(case when InOut='O' and Out_Category in ('IS') then -1*Cost else 0 end) as IssIssAdjCost ,sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1*Stock_Qty else 0 end) as IssOthQty  ,sum(case when InOut='O' and Out_Category not in ('SA','IS') then Rate else 0 end) as IssOthRate  ,sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1*Balance_FAT else 0 end) as IssOthFAT  ,(case when sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_FAT else 0 end)*100/sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_QTYKG else 0 end) end)  as IssOthFATPER  ,sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1*Balance_SNF else 0 end) as IssOthSNF  ,(case when sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_SNF else 0 end)*100/sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_QTYKG else 0 end) end)  as IssOthSNFPER  ,sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1*Cost else 0 end) as IssOthCost ,sum(case when InOut='O' then -1*Stock_Qty else 0 end) as IssQty  ,sum(case when InOut='O' then Rate else 0 end) as IssRate  ,sum(case when InOut='O' then -1*Balance_FAT else 0 end) as IssFAT  ,(case when sum(case when InOut='O' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' then  Balance_FAT else 0 end)*100/sum(case when InOut='O' then  Balance_QTYKG else 0 end) end)  as IssFATPER ,sum(case when InOut='O' then -1*Balance_SNF else 0 end) as IssSNF  ,(case when sum(case when InOut='O' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' then  Balance_SNF else 0 end)*100/sum(case when InOut='O' then  Balance_QTYKG else 0 end) end)  as IssSNFPER  ,sum(case when InOut='O' then -1*Cost else 0 end) as IssCost ,SUM(sum(Stock_Qty)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code,Punching_Date) as CLQty   ,SUM(sum(Cost)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code, Item_Code,Punching_Date) as CLCost ,SUM(sum(isnull(Balance_QTYKG,0))) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code, Punching_Date) as CLBalance_QTYKG  ,SUM(sum(Balance_FAT)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code, Punching_Date) as CLBalance_FAT ,SUM(sum(Balance_SNF)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code,Punching_Date) as CLBalance_SNF   from ("
                strFinalQry += " select 0 as Trans_Id,'' as Trans_Type,'' as Trans_Type_Name,'Opening Balance' as Source_Doc_No,null as Punching_Date,'' as InOutView, '' as InOut,xxx.Location_Code ,'' as [Loc Desp],'' as [LocAddress],'' as SourceCode,'' as SourceName,'' as SourceType ,'' as Item_Type,'' as Item_Type_Name,'' as Item_Group,'' as Group_Description," + strCodeColumnMax + "," + strCodeDescColumnMax + ",xxx.Item_Code as Item_Code ,max(xxx.Item_Desc) as Item_Desc,'' as Item_Category_Struct_Code,max(xxx.Stock_UOM) as Stock_UOM,'' as itf_code ,sum( Stock_Qty * case when InOut='I' then 1 else -1 end) as Stock_Qty,sum( QtyKG * case when InOut='I' then 1 else -1 end) as Balance_QTYKG, case when sum(convert(decimal(18,3),Stock_Qty* case when InOut='I' then 1 else -1 end))=0 then 0 else sum(convert(decimal(18,3),Cost* case when InOut='I' then 1 else -1 end))/sum(convert(decimal(18,3),Stock_Qty* case when InOut='I' then 1 else -1 end)) end as Rate,sum(Cost * case when InOut='I' then 1 else -1 end) as Cost,sum( (case when IsFromMilk=1 then MilkFATKG else (Stock_Qty*FatPer) end) * case when InOut='I' then 1 else -1 end) as Balance_FAT,sum(( (case when IsFromMilk=1 then MilkSNFKG else (Stock_Qty*SNFPer) end ) * case when InOut='I' then 1 else -1 end)) as Balance_SNF,'' as In_Category,'' as Out_Category "
                strFinalQry += " from (" + qry + ") xxx " + Environment.NewLine
                strFinalQry += " where Punching_Date < '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' group by xxx.Item_Code,xxx.Location_Code,xxx.Batch_No " + Environment.NewLine
                strFinalQry += " union all "
                strFinalQry += " select Trans_Id,Trans_Type,Trans_Type_Name,Source_Doc_No,Punching_Date,InOutView, InOut,Location_Code,[Loc Desp], [LocAddress],SourceCode,SourceName,SourceType ,Item_Type,Item_Type_Name,Item_Group,Group_Description," + strCodeColumn + "," + strCodeDescColumn + ",Item_Code ,Item_Desc,Item_Category_Struct_Code,Stock_UOM,itf_code ,( Stock_Qty * case when InOut='I' then 1 else -1 end) as Stock_Qty,(QtyKG * case when InOut='I' then 1 else -1 end) as Balance_QTYKG,convert(decimal(18,3),case when Stock_Qty=0 then 0 else Cost/Stock_Qty end) as Rate,(Cost * case when InOut='I' then 1 else -1 end) as Cost,( (case when IsFromMilk=1 then MilkFATKG else (Stock_Qty*FatPer) end) * case when InOut='I' then 1 else -1 end) as Balance_FAT, ( (case when IsFromMilk=1 then MilkSNFKG else (Stock_Qty*SNFPer) end ) * case when InOut='I' then 1 else -1 end) as Balance_SNF ,In_Category,Out_Category  "
                strFinalQry += " from (" + qry + ") xxx " + Environment.NewLine
                strFinalQry += " where Punching_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' " + Environment.NewLine
                strFinalQry += " union  all "
                strFinalQry += " SELECT 0 as Trans_Id,null as Trans_Type,null as Trans_Type_Name, null as Source_Doc_No, thedate as Punching_Date,'In' as InOutView,'I' as InOut,TSPL_LOCATION_MASTER.Location_Code as Location_Code,TSPL_LOCATION_MASTER.Location_Desc as [Loc Desp],null as [LocAddress],null as SourceCode,null as SourceName,null as SourceType ,TSPL_ITEM_MASTER.Item_Type,null as Item_Type_Name,null as Item_Group,null as Group_Description," + strCodeColumnNull + "," + strCodeDescColumnNull + ",TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER.Item_Desc,null as Item_Category_Struct_Code,TSPL_ITEM_UOM_DETAIL.UOM_Code as Stock_UOM ,null as  itf_code ,0 as Stock_Qty,0 as Balance_QTYKG,0 as Rate,0 as Cost,0 as Balance_FAT, 0 as Balance_SNF ,null as In_Category,null as Out_Category  "
                strFinalQry += " FROM ExplodeDates('" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "','" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "') as d,TSPL_ITEM_MASTER,TSPL_LOCATION_MASTER,TSPL_ITEM_UOM_DETAIL  where 2=2 "
                If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                    strFinalQry += " and TSPL_ITEM_MASTER.Item_Code in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ") "
                End If
                If rbtnLocationSelect.IsChecked Then
                    strWhrCatg = ""
                    Dim IsApplicable As Boolean = False
                    For ii As Integer = 0 To gvLocation.RowCount - 1
                        If clsCommon.myCBool(gvLocation.Rows(ii).Cells("SEL").Value) Then
                            If IsApplicable Then
                                strWhrCatg += " , "
                            End If
                            strWhrCatg += "'" + clsCommon.myCstr(gvLocation.Rows(ii).Cells("CODE").Value) + "'"
                            IsApplicable = True
                        End If
                    Next
                    strFinalQry += "  and TSPL_LOCATION_MASTER.Location_Code in (" + strWhrCatg + ") "
                End If
                strFinalQry += " and ((TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' and (TSPL_LOCATION_MASTER.Location_Type='Physical' or TSPL_LOCATION_MASTER.Location_Type='Logical') ) or (TSPL_LOCATION_MASTER.CSA_Type='Y') )  and TSPL_ITEM_UOM_DETAIL.Stocking_Unit='Y' and TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code"
                strFinalQry += " )xxxxxx Group by  Item_Code,Location_Code,Punching_Date )xxxxxxx where Punching_Date is not null  Order by  Punching_Date,Location_Code"
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Document Wise Detail Ledger") = CompairStringResult.Equal Then
                strFinalQry = " select  case when '" & LocationFirstTime & "'='1' then '" & LocationAddress & "' else '" + objCommonVar.CurrentCompanyName + " ' end as CompName,'" + clsCommon.myCDate(txtFromDate.Value, "dd/MMM/yyyy") + "' as FromDate,'" + clsCommon.myCDate(txtToDate.Value, "dd/MMM/yyyy") + "' as ToDate ,  Trans_Id,Location_Code,[Loc Desp],SourceType ,Item_Type,Item_Type_Name,Item_Group,Group_Description ," + strCodeColumn + "," + strCodeDescColumn + ", Item_Category_Struct_Code,itf_code ,Stock_Qty,Rate,Cost,isnull((CASE when Balance_QTYKG=0 then 0 else (Balance_FAT*100/Balance_QTYKG) end),0) as Balance_FATPER ,Balance_FAT,isnull((CASE when Balance_QTYKG=0 then 0 else ([Balance_SNF]*100/[Balance_QTYKG]) end),0) as [Balance_SNFPER] , Balance_SNF,SourceCode,SourceName,Source_Doc_No,convert(varchar,Punching_Date,103) as Punching_Date,Trans_Type,Trans_Type_Name,InOut,InOutView, Item_Code ,Item_Desc,Stock_UOM, (case when InOut='I' then Stock_Qty else 0 end) as RecQty, (case when InOut='I' then Rate else 0 end) as RecRate, (case when InOut='I' then Cost else 0 end) as RecCost, (case when InOut='O' then -1*Stock_Qty else 0 end) as IssQty, (case when InOut='O' then Rate else 0 end) as IssRate, (case when InOut='O' then -1*Cost else 0 end) as IssCost, SUM(Stock_Qty) OVER (Partition BY Item_Code ORDER BY Item_Code, Punching_Date) as CLQty ,( case when SUM(Stock_Qty) OVER (Partition BY Item_Code ORDER BY Item_Code, Punching_Date)=0 then 0  else SUM(Cost) OVER (Partition BY Item_Code ORDER BY Item_Code, Punching_Date)/SUM(Stock_Qty) OVER (Partition BY Item_Code ORDER BY Item_Code, Punching_Date) end) as CLRate ,SUM(Cost) OVER (Partition BY Item_Code ORDER BY Item_Code, Punching_Date) as CLCost "
                strFinalQry += " from ("
                strFinalQry += " select 0 as Trans_Id,'' as Trans_Type,'' as Trans_Type_Name,'Opening Balance' as Source_Doc_No,null as Punching_Date,'' as InOutView, '' as InOut,'' as Location_Code,'' as [Loc Desp],'' as [LocAddress],'' as SourceCode,'' as SourceName,'' as SourceType ,'' as Item_Type,'' as Item_Type_Name,'' as Item_Group,'' as Group_Description," + strCodeColumnMax + "," + strCodeDescColumnMax + ",xxx.Item_Code as Item_Code ,max(xxx.Item_Desc) as Item_Desc,'' as Item_Category_Struct_Code,max(xxx.Stock_UOM) as Stock_UOM,'' as itf_code ,sum( Stock_Qty * case when InOut='I' then 1 else -1 end) as Stock_Qty,sum( QtyKG * case when InOut='I' then 1 else -1 end) as Balance_QTYKG, case when sum(convert(decimal(18,3),Stock_Qty* case when InOut='I' then 1 else -1 end))=0 then 0 else sum(convert(decimal(18,3),Cost* case when InOut='I' then 1 else -1 end))/sum(convert(decimal(18,3),Stock_Qty* case when InOut='I' then 1 else -1 end)) end as Rate,sum(Cost * case when InOut='I' then 1 else -1 end) as Cost,sum( (case when IsFromMilk=1 then MilkFATKG else (Stock_Qty*FatPer) end) * case when InOut='I' then 1 else -1 end) as Balance_FAT,sum(( (case when IsFromMilk=1 then MilkSNFKG else (Stock_Qty*SNFPer) end ) * case when InOut='I' then 1 else -1 end)) as Balance_SNF "
                strFinalQry += " from (" + qry + ") xxx " + Environment.NewLine
                strFinalQry += " where Punching_Date < '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' group by xxx.Item_Code " + Environment.NewLine
                strFinalQry += " union all "
                strFinalQry += " select Trans_Id,Trans_Type,Trans_Type_Name,Source_Doc_No,Punching_Date,InOutView, InOut,Location_Code,[Loc Desp], [LocAddress],SourceCode,SourceName,SourceType ,Item_Type,Item_Type_Name,Item_Group,Group_Description," + strCodeColumn + "," + strCodeDescColumn + ",Item_Code ,Item_Desc,Item_Category_Struct_Code,Stock_UOM,itf_code ,( Stock_Qty * case when InOut='I' then 1 else -1 end) as Stock_Qty,(QtyKG * case when InOut='I' then 1 else -1 end) as Balance_QTYKG,convert(decimal(18,3),case when Stock_Qty=0 then 0 else Cost/Stock_Qty end) as Rate,(Cost * case when InOut='I' then 1 else -1 end) as Cost,( (case when IsFromMilk=1 then MilkFATKG else (Stock_Qty*FatPer) end) * case when InOut='I' then 1 else -1 end) as Balance_FAT, ( (case when IsFromMilk=1 then MilkSNFKG else (Stock_Qty*SNFPer) end ) * case when InOut='I' then 1 else -1 end) as Balance_SNF  "
                strFinalQry += " from (" + qry + ") xxx " + Environment.NewLine
                strFinalQry += " where Punching_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' " + Environment.NewLine
                strFinalQry += " )xxxxxx Order by Item_Code,Punching_Date"
            End If

            If isPrintCrystalReport = 4 Then
                transportSql.BulkExport("Stock Reco" & "_" & clsCommon.myCstr(cboType.SelectedValue), strFinalQry, "order by Item_Code,Punching_Date", "csv")
                Exit Sub
            ElseIf isPrintCrystalReport = 5 Then
                transportSql.BulkExport("Stock Reco" & "_" & clsCommon.myCstr(cboType.SelectedValue), strFinalQry, "order by Item_Code,Punching_Date", "xls")
                Exit Sub
            End If
            RadPageViewPage2.Text = "Report ( " + clsCommon.myCstr(cboType.SelectedValue) + " )"
            clsCommon.ProgressBarUpdate("Loading Data.Please Wait...")

            Dim dtr As System.Data.SqlClient.SqlDataReader = Nothing
            Try
                dtr = clsDBFuncationality.GetDataReader(strFinalQry, Nothing)
                If Not dtr.HasRows Then
                    Throw New Exception("No Data Found to Display")
                End If
                gv1.MasterTemplate.LoadFrom(dtr)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                dtr.Close()
            End Try
            EnableDisableCtrl(False)
            SetGridFormationOFGV1()
            RadPageView1.SelectedPage = RadPageViewPage2
            ''-----------------------------------

            If isPrintCrystalReport = 1 Then
                If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Document Wise Detail Ledger") = CompairStringResult.Equal Then
                    Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(strFinalQry)
                    If dt1 Is Nothing OrElse dt1.Rows.Count <= 0 Then
                        Throw New Exception("No Data Found to Display")
                    End If
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt1, "rptStockLedgerReport", "Stock Reco Report")
                    frmCRV = Nothing
                End If
            ElseIf isPrintCrystalReport = 2 Then
                ExportBulkData(strFinalQry, GetCollectionForVisibleColumns(), "test")

            ElseIf isPrintCrystalReport = 3 Then
                LoadDataInGridViaDataReader(strFinalQry)
                EnableDisableCtrl(False)
                SetGridFormationOFGV1()
            Else
            End If
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            clsCommon.ProgressBarHide()

        End Try
    End Sub

    Function GetCollectionForVisibleColumns() As Dictionary(Of String, String)
        Dim arrVisibleColumAndCaption As New Dictionary(Of String, String)
        If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item Type Wise Summary") = CompairStringResult.Equal Then
            arrVisibleColumAndCaption.Add("Item_Type_Name", "Item Type")
            arrVisibleColumAndCaption.Add("OPBal", "Opening Qty")
            arrVisibleColumAndCaption.Add("OPBalrate", "Opening Rate")
            arrVisibleColumAndCaption.Add("OPBalCost", "Opening cost")
            arrVisibleColumAndCaption.Add("Received_Qty", "Received Qty")
            arrVisibleColumAndCaption.Add("RecdRate", "Received Rate")
            arrVisibleColumAndCaption.Add("RecdCost", "Received Cost")
            arrVisibleColumAndCaption.Add("Issued_Qty", "Issued Qty")
            arrVisibleColumAndCaption.Add("IssueRate", "Issued Rate")
            arrVisibleColumAndCaption.Add("IssueCost", "Issued Cost")
            arrVisibleColumAndCaption.Add("Balance_Qty", "Closing Qty")
            arrVisibleColumAndCaption.Add("Rate", "Closing Rate")
            arrVisibleColumAndCaption.Add("Cost", "Closing Cost")
            If chkFATAndSNF.Checked Then
                arrVisibleColumAndCaption.Add("OPFATPER", "Opening FAT %")
                arrVisibleColumAndCaption.Add("OPFAT", "Opening FAT (KG)")
                arrVisibleColumAndCaption.Add("OPSNFPER", "Opening SNF %")
                arrVisibleColumAndCaption.Add("OPSNF", "Opening SNF (KG)")
                arrVisibleColumAndCaption.Add("Received_FATPER", "Received FAT %")
                arrVisibleColumAndCaption.Add("Received_FAT", "Received FAT (KG)")
                arrVisibleColumAndCaption.Add("Received_SNFPER", "Received SNF %")
                arrVisibleColumAndCaption.Add("Received_SNF", "Received SNF (KG)")
                arrVisibleColumAndCaption.Add("Issued_FATPER", "Issued FAT %")
                arrVisibleColumAndCaption.Add("Issued_FAT", "Issued FAT (KG)")
                arrVisibleColumAndCaption.Add("Issued_SNFPER", "Issued SNF %")
                arrVisibleColumAndCaption.Add("Issued_SNF", "Issued SNF (KG)")
                arrVisibleColumAndCaption.Add("Balance_FATPER", "Closing FAT %")
                arrVisibleColumAndCaption.Add("Balance_FAT", "Closing FAT (KG)")
                arrVisibleColumAndCaption.Add("Balance_SNFPER", "Closing SNF %")
                arrVisibleColumAndCaption.Add("Balance_SNF", "Closing SNF (KG)")
            End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item Group Wise Summary") = CompairStringResult.Equal Then
            arrVisibleColumAndCaption.Add("Item_Type_Name", "Item Type")
            arrVisibleColumAndCaption.Add("Group_Description", "Item Group")
            arrVisibleColumAndCaption.Add("OPBal", "Opening Qty")
            arrVisibleColumAndCaption.Add("OPBalrate", "Opening Rate")
            arrVisibleColumAndCaption.Add("OPBalCost", "Opening cost")
            arrVisibleColumAndCaption.Add("Received_Qty", "Received Qty")
            arrVisibleColumAndCaption.Add("RecdRate", "Received Rate")
            arrVisibleColumAndCaption.Add("RecdCost", "Received Cost")
            arrVisibleColumAndCaption.Add("Issued_Qty", "Issued Qty")
            arrVisibleColumAndCaption.Add("IssueRate", "Issued Rate")
            arrVisibleColumAndCaption.Add("IssueCost", "Issued Cost")
            arrVisibleColumAndCaption.Add("Balance_Qty", "Closing Qty")
            arrVisibleColumAndCaption.Add("Rate", "Closing Rate")
            arrVisibleColumAndCaption.Add("Cost", "Closing Cost")
            If chkFATAndSNF.Checked Then
                arrVisibleColumAndCaption.Add("OPFATPER", "Opening FAT %")
                arrVisibleColumAndCaption.Add("OPFAT", "Opening FAT (KG)")
                arrVisibleColumAndCaption.Add("OPSNFPER", "Opening SNF %")
                arrVisibleColumAndCaption.Add("OPSNF", "Opening SNF (KG)")
                arrVisibleColumAndCaption.Add("Received_FATPER", "Received FAT %")
                arrVisibleColumAndCaption.Add("Received_FAT", "Received FAT (KG)")
                arrVisibleColumAndCaption.Add("Received_SNFPER", "Received SNF %")
                arrVisibleColumAndCaption.Add("Received_SNF", "Received SNF (KG)")
                arrVisibleColumAndCaption.Add("Issued_FATPER", "Issued FAT %")
                arrVisibleColumAndCaption.Add("Issued_FAT", "Issued FAT (KG)")
                arrVisibleColumAndCaption.Add("Issued_SNFPER", "Issued SNF %")
                arrVisibleColumAndCaption.Add("Issued_SNF", "Issued SNF (KG)")
                arrVisibleColumAndCaption.Add("Balance_FATPER", "Closing FAT %")
                arrVisibleColumAndCaption.Add("Balance_FAT", "Closing FAT (KG)")
                arrVisibleColumAndCaption.Add("Balance_SNFPER", "Closing SNF %")
                arrVisibleColumAndCaption.Add("Balance_SNF", "Closing SNF (KG)")
            End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Category Wise Summary") = CompairStringResult.Equal Then
            arrVisibleColumAndCaption.Add("Item_Type_Name", "Item Type")
            arrVisibleColumAndCaption.Add("Group_Description", "Item Group")
            arrVisibleColumAndCaption.Add("Stock_UOM", "UOM")
            arrVisibleColumAndCaption.Add("OPBal", "Opening Qty")
            arrVisibleColumAndCaption.Add("OPBalrate", "Opening Rate")
            arrVisibleColumAndCaption.Add("OPBalCost", "Opening cost")
            arrVisibleColumAndCaption.Add("Received_Qty", "Received Qty")
            arrVisibleColumAndCaption.Add("RecdRate", "Received Rate")
            arrVisibleColumAndCaption.Add("RecdCost", "Received Cost")
            arrVisibleColumAndCaption.Add("Issued_Qty", "Issued Qty")
            arrVisibleColumAndCaption.Add("IssueRate", "Issued Rate")
            arrVisibleColumAndCaption.Add("IssueCost", "Issued Cost")
            arrVisibleColumAndCaption.Add("Balance_Qty", "Closing Qty")
            arrVisibleColumAndCaption.Add("Rate", "Closing Rate")
            arrVisibleColumAndCaption.Add("Cost", "Closing Cost")
            If chkFATAndSNF.Checked Then
                arrVisibleColumAndCaption.Add("OPFATPER", "Opening FAT %")
                arrVisibleColumAndCaption.Add("OPFAT", "Opening FAT (KG)")
                arrVisibleColumAndCaption.Add("OPSNFPER", "Opening SNF %")
                arrVisibleColumAndCaption.Add("OPSNF", "Opening SNF (KG)")
                arrVisibleColumAndCaption.Add("Received_FATPER", "Received FAT %")
                arrVisibleColumAndCaption.Add("Received_FAT", "Received FAT (KG)")
                arrVisibleColumAndCaption.Add("Received_SNFPER", "Received SNF %")
                arrVisibleColumAndCaption.Add("Received_SNF", "Received SNF (KG)")
                arrVisibleColumAndCaption.Add("Issued_FATPER", "Issued FAT %")
                arrVisibleColumAndCaption.Add("Issued_FAT", "Issued FAT (KG)")
                arrVisibleColumAndCaption.Add("Issued_SNFPER", "Issued SNF %")
                arrVisibleColumAndCaption.Add("Issued_SNF", "Issued SNF (KG)")
                arrVisibleColumAndCaption.Add("Balance_FATPER", "Closing FAT %")
                arrVisibleColumAndCaption.Add("Balance_FAT", "Closing FAT (KG)")
                arrVisibleColumAndCaption.Add("Balance_SNFPER", "Closing SNF %")
                arrVisibleColumAndCaption.Add("Balance_SNF", "Closing SNF (KG)")
            End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item And Batch Wise Summary") = CompairStringResult.Equal Then
            arrVisibleColumAndCaption.Add("Item_Type_Name", "Item Type")
            arrVisibleColumAndCaption.Add("Group_Description", "Item Group")
            arrVisibleColumAndCaption.Add("Item_Desc", "Item")
            arrVisibleColumAndCaption.Add("Stock_UOM", "UOM")
            arrVisibleColumAndCaption.Add("OPBal", "Opening Qty")
            arrVisibleColumAndCaption.Add("OPBalrate", "Opening Rate")
            arrVisibleColumAndCaption.Add("OPBalCost", "Opening Cost")
            arrVisibleColumAndCaption.Add("Received_Qty", "Received Qty")
            arrVisibleColumAndCaption.Add("RecdRate", "Received Rate")
            arrVisibleColumAndCaption.Add("RecdCost", "Received Cost")
            arrVisibleColumAndCaption.Add("Issued_Qty", "Issued Qty")
            arrVisibleColumAndCaption.Add("IssueRate", "Issued Rate")
            arrVisibleColumAndCaption.Add("IssueCost", "Issued Cost")
            arrVisibleColumAndCaption.Add("Balance_Qty", "Closing Qty")
            arrVisibleColumAndCaption.Add("Rate", "Closing Rate")
            arrVisibleColumAndCaption.Add("Cost", "Closing Cost")
            If chkFATAndSNF.Checked Then
                arrVisibleColumAndCaption.Add("OPFATPER", "Opening FAT %")
                arrVisibleColumAndCaption.Add("OPFAT", "Opening FAT (KG)")
                arrVisibleColumAndCaption.Add("OPSNFPER", "Opening SNF %")
                arrVisibleColumAndCaption.Add("OPSNF", "Opening SNF (KG)")
                arrVisibleColumAndCaption.Add("Received_FATPER", "Received FAT %")
                arrVisibleColumAndCaption.Add("Received_FAT", "Received FAT (KG)")
                arrVisibleColumAndCaption.Add("Received_SNFPER", "Received SNF %")
                arrVisibleColumAndCaption.Add("Received_SNF", "Received SNF (KG)")
                arrVisibleColumAndCaption.Add("Issued_FATPER", "Issued FAT %")
                arrVisibleColumAndCaption.Add("Issued_FAT", "Issued FAT")
                arrVisibleColumAndCaption.Add("Issued_SNFPER", "Issued SNF %")
                arrVisibleColumAndCaption.Add("Issued_SNF", "Issued SNF")
                arrVisibleColumAndCaption.Add("Balance_FATPER", "Closing FAT %")
                arrVisibleColumAndCaption.Add("Balance_FAT", "Closing FAT (KG)")
                arrVisibleColumAndCaption.Add("Balance_SNFPER", "Closing SNF %")
                arrVisibleColumAndCaption.Add("Balance_SNF", "Balance_SNF")
            End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item Batch And Location Wise Summary") = CompairStringResult.Equal Then
            arrVisibleColumAndCaption.Add("Item_Type_Name", "Item Type")
            arrVisibleColumAndCaption.Add("Item_Group", "Item Group Code")
            arrVisibleColumAndCaption.Add("Group_Description", "Item Group")
            arrVisibleColumAndCaption.Add("MainLocationDesc", "Main Location")
            arrVisibleColumAndCaption.Add("Location_Code", "Location Code")
            arrVisibleColumAndCaption.Add("Loc Desp", "Location")
            arrVisibleColumAndCaption.Add("Item_Desc", "Item")
            arrVisibleColumAndCaption.Add("Stock_UOM", "UOM")
            arrVisibleColumAndCaption.Add("OPBal", "Opening Qty")
            arrVisibleColumAndCaption.Add("OPBalrate", "Opening Rate")
            arrVisibleColumAndCaption.Add("OPBalCost", "Opening Cost")
            arrVisibleColumAndCaption.Add("Received_Qty", "Received Qty")
            arrVisibleColumAndCaption.Add("RecdRate", "Received Rate")
            arrVisibleColumAndCaption.Add("RecdCost", "Received Cost")
            arrVisibleColumAndCaption.Add("Issued_Qty", "Issued Qty")
            arrVisibleColumAndCaption.Add("IssueRate", "Issued Rate")
            arrVisibleColumAndCaption.Add("IssueCost", "Issued Cost")
            arrVisibleColumAndCaption.Add("Balance_Qty", "Closing Qty")
            arrVisibleColumAndCaption.Add("Rate", "Closing Rate")
            arrVisibleColumAndCaption.Add("Cost", "Closing Cost")
            If chkFATAndSNF.Checked Then
                arrVisibleColumAndCaption.Add("OPFAT", "Opening FAT (KG)")
                arrVisibleColumAndCaption.Add("OPFATPER", "Opening FAT %")
                arrVisibleColumAndCaption.Add("OPSNF", "Opening SNF (KG)")
                arrVisibleColumAndCaption.Add("OPSNFPER", "Opening SNF %")
                arrVisibleColumAndCaption.Add("Received_FATPER", "Received FAT %")
                arrVisibleColumAndCaption.Add("Received_FAT", "Received FAT (KG)")
                arrVisibleColumAndCaption.Add("Received_SNFPER", "Received SNF %")
                arrVisibleColumAndCaption.Add("Received_SNF", "Received SNF (KG)")
                arrVisibleColumAndCaption.Add("Issued_FATPER", "Issued FAT %")
                arrVisibleColumAndCaption.Add("Issued_FAT", "Issued FAT (KG)")
                arrVisibleColumAndCaption.Add("Issued_SNFPER", "Issued SNF %")
                arrVisibleColumAndCaption.Add("Issued_SNF", "Issued SNF (KG)")
                arrVisibleColumAndCaption.Add("Balance_FATPER", "Closing FAT %")
                arrVisibleColumAndCaption.Add("Balance_FAT", "Closing FAT (KG)")
                arrVisibleColumAndCaption.Add("Balance_SNFPER", "Closing SNF %")
                arrVisibleColumAndCaption.Add("Balance_SNF", "Closing SNF (KG)")
            End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Document Wise Detail") = CompairStringResult.Equal Then
            arrVisibleColumAndCaption.Add("Trans_Type_Name", "Transaction Type")
            arrVisibleColumAndCaption.Add("Source_Doc_No", "Source Doc No")
            arrVisibleColumAndCaption.Add("Punching_Date", "Document Date")
            arrVisibleColumAndCaption.Add("InOutView", "Type")
            arrVisibleColumAndCaption.Add("SourceName", "Source")
            arrVisibleColumAndCaption.Add("Item_Type_Name", "Item Type")
            arrVisibleColumAndCaption.Add("Group_Description", "Item Group")
            arrVisibleColumAndCaption.Add("Item_Desc", "Item")
            arrVisibleColumAndCaption.Add("Stock_UOM", "UOM")
            arrVisibleColumAndCaption.Add("Stock_Qty", "Quantity")
            arrVisibleColumAndCaption.Add("Rate", "Rate")
            arrVisibleColumAndCaption.Add("Cost", "Amount")
            If chkFATAndSNF.Checked Then
                arrVisibleColumAndCaption.Add("Balance_FATPER", "Closing FAT %")
                arrVisibleColumAndCaption.Add("Balance_FAT", "FAT (KG)")
                arrVisibleColumAndCaption.Add("Balance_SNFPER", "Closing SNF %")
                arrVisibleColumAndCaption.Add("Balance_SNF", "SNF (KG)")
            End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Date, Item And Document Wise Detail") = CompairStringResult.Equal Then
            arrVisibleColumAndCaption.Add("Source_Doc_No", "Source Doc No")
            arrVisibleColumAndCaption.Add("Punching_Date", "Document Date")
            arrVisibleColumAndCaption.Add("Item_Type_Name", "Item Type")
            arrVisibleColumAndCaption.Add("InOutView", "Type")
            arrVisibleColumAndCaption.Add("Item_Code", "Item Code")
            arrVisibleColumAndCaption.Add("Item_Desc", "Item")
            arrVisibleColumAndCaption.Add("Stock_UOM", "UOM")
            arrVisibleColumAndCaption.Add("OPQty", "Opening Quantity")
            arrVisibleColumAndCaption.Add("OPRate", "Opening Rate")
            arrVisibleColumAndCaption.Add("OPCost", "Opening Amount")
            arrVisibleColumAndCaption.Add("RecQty", "Received Quantity")
            arrVisibleColumAndCaption.Add("RecRate", "Received Rate")
            arrVisibleColumAndCaption.Add("RecCost", "Received Amount")
            arrVisibleColumAndCaption.Add("IssQty", "Issued Quantity")
            arrVisibleColumAndCaption.Add("IssRate", "Issued Rate")
            arrVisibleColumAndCaption.Add("IssCost", "Issued Amount")
            arrVisibleColumAndCaption.Add("CLQty", "Balance Quantity")
            arrVisibleColumAndCaption.Add("CLRate", "Balance Rate")
            arrVisibleColumAndCaption.Add("CLCost", "Balance Amount")
            If chkFATAndSNF.Checked Then
                arrVisibleColumAndCaption.Add("OPFAT", "Opening FAT")
                arrVisibleColumAndCaption.Add("OPFATPER", "Opening Fat %")
                arrVisibleColumAndCaption.Add("OPSNF", "Opening SNF")
                arrVisibleColumAndCaption.Add("OPSNFPER", "Opening SNF%")
                arrVisibleColumAndCaption.Add("RecFAT", "Received FAT")
                arrVisibleColumAndCaption.Add("RecFATPER", "Received Fat %")
                arrVisibleColumAndCaption.Add("RecSNF", "Received SNF")
                arrVisibleColumAndCaption.Add("RecSNFPER", "Received SNF%")
                arrVisibleColumAndCaption.Add("IssFAT", "Issued FAT")
                arrVisibleColumAndCaption.Add("IssFATPER", "Issued Fat %")
                arrVisibleColumAndCaption.Add("IssSNF", "Issued SNF")
                arrVisibleColumAndCaption.Add("IssSNFPER", "Issued SNF%")
                arrVisibleColumAndCaption.Add("CLFAT", "Balance FAT")
                arrVisibleColumAndCaption.Add("CLFATPER", "Balance Fat %")
                arrVisibleColumAndCaption.Add("CLSNF", "Balance SNF")
                arrVisibleColumAndCaption.Add("CLSNFPER", "Balance SNF%")
            End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Date and Item Wise Stock") = CompairStringResult.Equal Then
            arrVisibleColumAndCaption.Add("Location_Code", "Location Code")
            arrVisibleColumAndCaption.Add("Loc Desp", "Location")
            arrVisibleColumAndCaption.Add("Punching_Date", "Date")
            arrVisibleColumAndCaption.Add("Item_Code", "Item Code")
            arrVisibleColumAndCaption.Add("Item_Desc", "Item")
            arrVisibleColumAndCaption.Add("Stock_UOM", "UOM")

            arrVisibleColumAndCaption.Add("OPQty", "Opening Quantity")
            arrVisibleColumAndCaption.Add("OPRate", "Opening Rate")
            arrVisibleColumAndCaption.Add("OPCost", "Opening Amount")

            arrVisibleColumAndCaption.Add("RecPurQty", "Received Purchase Quantity")
            arrVisibleColumAndCaption.Add("RecPurRate", "Received Purchase Rate")
            arrVisibleColumAndCaption.Add("RecPurCost", "Received Purchase Amount")

            arrVisibleColumAndCaption.Add("RecAdjProQty", "Received Adjustment/Production Quantity")
            arrVisibleColumAndCaption.Add("RecAdjProRate", "Received Adjustment/Production Rate")
            arrVisibleColumAndCaption.Add("RecAdjProCost", "Received Adjustment/Production Amount")

            arrVisibleColumAndCaption.Add("RecOthQty", "Received Other Quantity")
            arrVisibleColumAndCaption.Add("RecOthRate", "Received Other Rate")
            arrVisibleColumAndCaption.Add("RecOthCost", "Received Other Amount")

            arrVisibleColumAndCaption.Add("IssSaleQty", "Issued Sale Quantity")
            arrVisibleColumAndCaption.Add("IssSaleRate", "Issued Sale Rate")
            arrVisibleColumAndCaption.Add("IssSaleCost", "Issued Sale Amount")

            arrVisibleColumAndCaption.Add("IssIssAdjQty", "Issued/Adjustment Quantity")
            arrVisibleColumAndCaption.Add("IssIssAdjRate", "Issued/Adjustment Rate")
            arrVisibleColumAndCaption.Add("IssIssAdjCost", "Issued/Adjustment Amount")

            arrVisibleColumAndCaption.Add("IssOthQty", "Issued Other Quantity")
            arrVisibleColumAndCaption.Add("IssOthRate", "Issued Other Rate")
            arrVisibleColumAndCaption.Add("IssOthCost", "Issued Other Amount")


            arrVisibleColumAndCaption.Add("CLQty", "Balance Quantity")
            arrVisibleColumAndCaption.Add("CLRate", "Balance Rate")
            arrVisibleColumAndCaption.Add("CLCost", "Balance Amount")
            If chkFATAndSNF.Checked Then
                arrVisibleColumAndCaption.Add("OPFAT", "Opening FAT")
                arrVisibleColumAndCaption.Add("OPFATPER", "Opening Fat %")
                arrVisibleColumAndCaption.Add("OPSNF", "Opening SNF")
                arrVisibleColumAndCaption.Add("OPSNFPER", "Opening SNF%")

                arrVisibleColumAndCaption.Add("RecPurFAT", "Received Purchase FAT")
                arrVisibleColumAndCaption.Add("RecPurFATPER", "Received Purchase Fat %")
                arrVisibleColumAndCaption.Add("RecPurSNF", "Received Purchase SNF")
                arrVisibleColumAndCaption.Add("RecPurSNFPER", "Received Purchase SNF %")

                arrVisibleColumAndCaption.Add("RecAdjProFAT", "Received Adjustment/Production FAT")
                arrVisibleColumAndCaption.Add("RecAdjProFATPER", "Received Adjustment/Production Fat %")
                arrVisibleColumAndCaption.Add("RecAdjProSNF", "Received Adjustment/Production SNF")
                arrVisibleColumAndCaption.Add("RecAdjProSNFPER", "Received Adjustment/Production SNF %")

                arrVisibleColumAndCaption.Add("RecOthFAT", "Received Other FAT")
                arrVisibleColumAndCaption.Add("RecOthFATPER", "Received Other Fat %")
                arrVisibleColumAndCaption.Add("RecOthSNF", "Received Other SNF")
                arrVisibleColumAndCaption.Add("RecOthSNFPER", "Received Other SNF %")

                arrVisibleColumAndCaption.Add("IssSaleFAT", "Issued Sale FAT")
                arrVisibleColumAndCaption.Add("IssSaleFATPER", "Issued Sale Fat %")
                arrVisibleColumAndCaption.Add("IssSaleSNF", "Issued Sale SNF")
                arrVisibleColumAndCaption.Add("IssSaleSNFPER", "Issued Sale SNF%")

                arrVisibleColumAndCaption.Add("IssIssAdjFAT", "Issued/Adjustment FAT")
                arrVisibleColumAndCaption.Add("IssIssAdjFATPER", "Issued Fat/Adjustment %")
                arrVisibleColumAndCaption.Add("IssIssAdjSNF", "Issued/Adjustment SNF")
                arrVisibleColumAndCaption.Add("IssIssAdjSNFPER", "Issued/Adjustment SNF%")

                arrVisibleColumAndCaption.Add("IssOthFAT", "Issued other FAT")
                arrVisibleColumAndCaption.Add("IssOthFATPER", "Issued other Fat %")
                arrVisibleColumAndCaption.Add("IssOthSNF", "Issued other SNF")
                arrVisibleColumAndCaption.Add("IssOthSNFPER", "Issued other SNF%")


                arrVisibleColumAndCaption.Add("CLFAT", "Balance FAT")
                arrVisibleColumAndCaption.Add("CLFATPER", "Balance Fat %")
                arrVisibleColumAndCaption.Add("CLSNF", "Balance SNF")
                arrVisibleColumAndCaption.Add("CLSNFPER", "Balance SNF%")
            End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Document Wise Detail Ledger") = CompairStringResult.Equal Then
            arrVisibleColumAndCaption.Add("Source_Doc_No", "Source Doc No")
            arrVisibleColumAndCaption.Add("Punching_Date", "Document Date")
            arrVisibleColumAndCaption.Add("Item_Type_Name", "Item Type")
            arrVisibleColumAndCaption.Add("InOutView", "Type")
            arrVisibleColumAndCaption.Add("Item_Code", "Item Code")
            arrVisibleColumAndCaption.Add("Item_Desc", "Item")
            arrVisibleColumAndCaption.Add("Stock_UOM", "UOM")
            arrVisibleColumAndCaption.Add("RecQty", "Received Quantity")
            arrVisibleColumAndCaption.Add("RecRate", "Received Rate")
            arrVisibleColumAndCaption.Add("RecCost", "Received Amount")
            arrVisibleColumAndCaption.Add("IssQty", "Issued Quantity")
            arrVisibleColumAndCaption.Add("IssRate", "Issued Rate")
            arrVisibleColumAndCaption.Add("IssCost", "Issued Amount")
            arrVisibleColumAndCaption.Add("CLQty", "Balance Quantity")
            arrVisibleColumAndCaption.Add("CLRate", "Balance Rate")
            arrVisibleColumAndCaption.Add("CLCost", "Balance Amount")
        End If
        Return arrVisibleColumAndCaption
    End Function

    Sub SetGridFormationOFGV1()
        gv1.GroupDescriptors.Clear()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = False
        Next
        If Not (clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item Group Wise Summary") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item Type Wise Summary") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Document Wise Detail Ledger") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Date, Item And Document Wise Detail") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Date and Item Wise Stock") = CompairStringResult.Equal) Then
            For Each dr As DataRow In dtCategory.Rows
                Dim strCol As String = clsCommon.myCstr(dr("CodeDescColumn"))
                gv1.Columns(strCol).IsVisible = True
                gv1.Columns(strCol).Width = 100
                gv1.Columns(strCol).HeaderText = clsCommon.myCstr(dr("DescColumn"))
            Next
        End If

        If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item Type Wise Summary") = CompairStringResult.Equal Then
            gv1.Columns("Item_Type").IsVisible = False
            gv1.Columns("Item_Type").HeaderText = "Item Type Code"

            gv1.Columns("Item_Type_Name").IsVisible = True
            gv1.Columns("Item_Type_Name").Width = 100
            gv1.Columns("Item_Type_Name").HeaderText = "Item Type"

            gv1.Columns("OPBal").IsVisible = True
            gv1.Columns("OPBal").Width = 100
            gv1.Columns("OPBal").HeaderText = "Opening Qty"
            gv1.Columns("OPBal").FormatString = "{0:n2}"

            gv1.Columns("OPBalrate").IsVisible = True
            gv1.Columns("OPBalrate").Width = 100
            gv1.Columns("OPBalrate").HeaderText = "Opening Rate"
            gv1.Columns("OPBalrate").FormatString = "{0:n3}"

            gv1.Columns("OPBalCost").IsVisible = True
            gv1.Columns("OPBalCost").Width = 100
            gv1.Columns("OPBalCost").HeaderText = "Opening cost"
            gv1.Columns("OPBalCost").FormatString = "{0:n2}"

            gv1.Columns("OPFATPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("OPFATPER").Width = 100
            gv1.Columns("OPFATPER").HeaderText = "Opening FAT %"
            gv1.Columns("OPFATPER").FormatString = "{0:n2}"

            gv1.Columns("OPFAT").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("OPFAT").Width = 100
            gv1.Columns("OPFAT").HeaderText = "Opening FAT (KG)"
            gv1.Columns("OPFAT").FormatString = "{0:n2}"

            gv1.Columns("OPSNFPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("OPSNFPER").Width = 100
            gv1.Columns("OPSNFPER").HeaderText = "Opening SNF %"
            gv1.Columns("OPSNFPER").FormatString = "{0:n2}"

            gv1.Columns("OPSNF").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("OPSNF").Width = 100
            gv1.Columns("OPSNF").HeaderText = "Opening SNF (KG)"
            gv1.Columns("OPSNF").FormatString = "{0:n2}"

            gv1.Columns("Received_Qty").IsVisible = True
            gv1.Columns("Received_Qty").Width = 100
            gv1.Columns("Received_Qty").HeaderText = "Received Qty"
            gv1.Columns("Received_Qty").FormatString = "{0:n2}"

            gv1.Columns("RecdRate").IsVisible = True
            gv1.Columns("RecdRate").Width = 100
            gv1.Columns("RecdRate").HeaderText = "Received Rate"
            gv1.Columns("RecdRate").FormatString = "{0:n3}"

            gv1.Columns("RecdCost").IsVisible = True
            gv1.Columns("RecdCost").Width = 100
            gv1.Columns("RecdCost").HeaderText = "Received Cost"
            gv1.Columns("RecdCost").FormatString = "{0:n2}"

            gv1.Columns("Received_FATPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Received_FATPER").Width = 100
            gv1.Columns("Received_FATPER").HeaderText = "Received FAT %"
            gv1.Columns("Received_FATPER").FormatString = "{0:n2}"

            gv1.Columns("Received_FAT").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Received_FAT").Width = 100
            gv1.Columns("Received_FAT").HeaderText = "Received FAT (KG)"
            gv1.Columns("Received_FAT").FormatString = "{0:n2}"

            gv1.Columns("Received_SNFPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Received_SNFPER").Width = 100
            gv1.Columns("Received_SNFPER").HeaderText = "Received SNF %"
            gv1.Columns("Received_SNFPER").FormatString = "{0:n2}"

            gv1.Columns("Received_SNF").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Received_SNF").Width = 100
            gv1.Columns("Received_SNF").HeaderText = "Received SNF (KG)"
            gv1.Columns("Received_SNF").FormatString = "{0:n2}"

            gv1.Columns("Issued_Qty").IsVisible = True
            gv1.Columns("Issued_Qty").Width = 100
            gv1.Columns("Issued_Qty").HeaderText = "Issued Qty"
            gv1.Columns("Issued_Qty").FormatString = "{0:n2}"

            gv1.Columns("IssueRate").IsVisible = True
            gv1.Columns("IssueRate").Width = 100
            gv1.Columns("IssueRate").HeaderText = "Issued Rate"
            gv1.Columns("IssueRate").FormatString = "{0:n3}"

            gv1.Columns("IssueCost").IsVisible = True
            gv1.Columns("IssueCost").Width = 100
            gv1.Columns("IssueCost").HeaderText = "Issued Cost"
            gv1.Columns("IssueCost").FormatString = "{0:n2}"

            gv1.Columns("Issued_FATPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Issued_FATPER").Width = 100
            gv1.Columns("Issued_FATPER").HeaderText = "Issued FAT %"
            gv1.Columns("Issued_FATPER").FormatString = "{0:n2}"

            gv1.Columns("Issued_FAT").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Issued_FAT").Width = 100
            gv1.Columns("Issued_FAT").HeaderText = "Issued FAT (KG)"
            gv1.Columns("Issued_FAT").FormatString = "{0:n2}"

            gv1.Columns("Issued_SNFPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Issued_SNFPER").Width = 100
            gv1.Columns("Issued_SNFPER").HeaderText = "Issued SNF %"
            gv1.Columns("Issued_SNFPER").FormatString = "{0:n2}"

            gv1.Columns("Issued_SNF").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Issued_SNF").Width = 100
            gv1.Columns("Issued_SNF").HeaderText = "Issued SNF (KG)"
            gv1.Columns("Issued_SNF").FormatString = "{0:n2}"

            gv1.Columns("Balance_Qty").IsVisible = True
            gv1.Columns("Balance_Qty").Width = 100
            gv1.Columns("Balance_Qty").HeaderText = "Closing Qty"
            gv1.Columns("Balance_Qty").FormatString = "{0:n2}"

            gv1.Columns("Rate").IsVisible = True
            gv1.Columns("Rate").Width = 100
            gv1.Columns("Rate").HeaderText = "Closing Rate"
            gv1.Columns("Rate").FormatString = "{0:n3}"

            gv1.Columns("Cost").IsVisible = True
            gv1.Columns("Cost").Width = 100
            gv1.Columns("Cost").HeaderText = "Closing Cost"
            gv1.Columns("Cost").FormatString = "{0:n2}"

            gv1.Columns("Balance_FATPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Balance_FATPER").Width = 100
            gv1.Columns("Balance_FATPER").HeaderText = "Closing FAT %"
            gv1.Columns("Balance_FATPER").FormatString = "{0:n2}"

            gv1.Columns("Balance_FAT").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Balance_FAT").Width = 100
            gv1.Columns("Balance_FAT").HeaderText = "Closing FAT (KG)"
            gv1.Columns("Balance_FAT").FormatString = "{0:n2}"

            gv1.Columns("Balance_SNFPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Balance_SNFPER").Width = 100
            gv1.Columns("Balance_SNFPER").HeaderText = "Closing SNF %"
            gv1.Columns("Balance_SNFPER").FormatString = "{0:n2}"

            gv1.Columns("Balance_SNF").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Balance_SNF").Width = 100
            gv1.Columns("Balance_SNF").HeaderText = "Closing SNF (KG)"
            gv1.Columns("Balance_SNF").FormatString = "{0:n2}"

            ''
            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim Smitem As New GridViewSummaryItem("OPBal", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("OPBalCost", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("Received_Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("RecdCost", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("Issued_Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("IssueCost", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("Balance_Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Cost", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)


            Smitem = New GridViewSummaryItem("OPFAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("OPSNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Received_FAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Received_SNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Issued_FAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Issued_SNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Balance_FAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Balance_SNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)


            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item Group Wise Summary") = CompairStringResult.Equal Then
            gv1.Columns("Item_Type").IsVisible = False
            gv1.Columns("Item_Type").HeaderText = "Item Type Code"

            gv1.Columns("Item_Type_Name").IsVisible = True
            gv1.Columns("Item_Type_Name").Width = 100
            gv1.Columns("Item_Type_Name").HeaderText = "Item Type"

            gv1.Columns("Item_Group").IsVisible = False
            gv1.Columns("Item_Group").HeaderText = "Item Group Code"

            gv1.Columns("Group_Description").IsVisible = True
            gv1.Columns("Group_Description").Width = 100
            gv1.Columns("Group_Description").HeaderText = "Item Group"

            gv1.Columns("OPBal").IsVisible = True
            gv1.Columns("OPBal").Width = 100
            gv1.Columns("OPBal").HeaderText = "Opening Qty"
            gv1.Columns("OPBal").FormatString = "{0:n2}"

            gv1.Columns("OPBalrate").IsVisible = True
            gv1.Columns("OPBalrate").Width = 100
            gv1.Columns("OPBalrate").HeaderText = "Opening Rate"
            gv1.Columns("OPBalrate").FormatString = "{0:n3}"

            gv1.Columns("OPBalCost").IsVisible = True
            gv1.Columns("OPBalCost").Width = 100
            gv1.Columns("OPBalCost").HeaderText = "Opening cost"
            gv1.Columns("OPBalCost").FormatString = "{0:n2}"

            gv1.Columns("OPFATPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("OPFATPER").Width = 100
            gv1.Columns("OPFATPER").HeaderText = "Opening FAT %"
            gv1.Columns("OPFATPER").FormatString = "{0:n2}"

            gv1.Columns("OPFAT").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("OPFAT").Width = 100
            gv1.Columns("OPFAT").HeaderText = "Opening FAT (KG)"
            gv1.Columns("OPFAT").FormatString = "{0:n2}"

            gv1.Columns("OPSNFPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("OPSNFPER").Width = 100
            gv1.Columns("OPSNFPER").HeaderText = "Opening SNF %"
            gv1.Columns("OPSNFPER").FormatString = "{0:n2}"

            gv1.Columns("OPSNF").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("OPSNF").Width = 100
            gv1.Columns("OPSNF").HeaderText = "Opening SNF (KG)"
            gv1.Columns("OPSNF").FormatString = "{0:n2}"

            gv1.Columns("Received_Qty").IsVisible = True
            gv1.Columns("Received_Qty").Width = 100
            gv1.Columns("Received_Qty").HeaderText = "Received Qty"
            gv1.Columns("Received_Qty").FormatString = "{0:n2}"

            gv1.Columns("RecdRate").IsVisible = True
            gv1.Columns("RecdRate").Width = 100
            gv1.Columns("RecdRate").HeaderText = "Received Rate"
            gv1.Columns("RecdRate").FormatString = "{0:n3}"

            gv1.Columns("RecdCost").IsVisible = True
            gv1.Columns("RecdCost").Width = 100
            gv1.Columns("RecdCost").HeaderText = "Received Cost"
            gv1.Columns("RecdCost").FormatString = "{0:n2}"

            gv1.Columns("Received_FATPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Received_FATPER").Width = 100
            gv1.Columns("Received_FATPER").HeaderText = "Received FAT %"
            gv1.Columns("Received_FATPER").FormatString = "{0:n2}"

            gv1.Columns("Received_FAT").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Received_FAT").Width = 100
            gv1.Columns("Received_FAT").HeaderText = "Received FAT (KG)"
            gv1.Columns("Received_FAT").FormatString = "{0:n2}"


            gv1.Columns("Received_SNFPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Received_SNFPER").Width = 100
            gv1.Columns("Received_SNFPER").HeaderText = "Received SNF %"
            gv1.Columns("Received_SNFPER").FormatString = "{0:n2}"

            gv1.Columns("Received_SNF").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Received_SNF").Width = 100
            gv1.Columns("Received_SNF").HeaderText = "Received SNF (KG)"
            gv1.Columns("Received_SNF").FormatString = "{0:n2}"

            gv1.Columns("Issued_Qty").IsVisible = True
            gv1.Columns("Issued_Qty").Width = 100
            gv1.Columns("Issued_Qty").HeaderText = "Issued Qty"
            gv1.Columns("Issued_Qty").FormatString = "{0:n2}"

            gv1.Columns("IssueRate").IsVisible = True
            gv1.Columns("IssueRate").Width = 100
            gv1.Columns("IssueRate").HeaderText = "Issued Rate"
            gv1.Columns("IssueRate").FormatString = "{0:n3}"

            gv1.Columns("IssueCost").IsVisible = True
            gv1.Columns("IssueCost").Width = 100
            gv1.Columns("IssueCost").HeaderText = "Issued Cost"
            gv1.Columns("IssueCost").FormatString = "{0:n2}"

            gv1.Columns("Issued_FATPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Issued_FATPER").Width = 100
            gv1.Columns("Issued_FATPER").HeaderText = "Issued FAT %"
            gv1.Columns("Issued_FATPER").FormatString = "{0:n2}"

            gv1.Columns("Issued_FAT").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Issued_FAT").Width = 100
            gv1.Columns("Issued_FAT").HeaderText = "Issued FAT (KG)"
            gv1.Columns("Issued_FAT").FormatString = "{0:n2}"

            gv1.Columns("Issued_SNFPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Issued_SNFPER").Width = 100
            gv1.Columns("Issued_SNFPER").HeaderText = "Issued SNF %"
            gv1.Columns("Issued_SNFPER").FormatString = "{0:n2}"

            gv1.Columns("Issued_SNF").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Issued_SNF").Width = 100
            gv1.Columns("Issued_SNF").HeaderText = "Issued SNF (KG)"
            gv1.Columns("Issued_SNF").FormatString = "{0:n2}"

            gv1.Columns("Balance_Qty").IsVisible = True
            gv1.Columns("Balance_Qty").Width = 100
            gv1.Columns("Balance_Qty").HeaderText = "Closing Qty"
            gv1.Columns("Balance_Qty").FormatString = "{0:n2}"

            gv1.Columns("Rate").IsVisible = True
            gv1.Columns("Rate").Width = 100
            gv1.Columns("Rate").HeaderText = "Closing Rate"
            gv1.Columns("Rate").FormatString = "{0:n3}"

            gv1.Columns("Cost").IsVisible = True
            gv1.Columns("Cost").Width = 100
            gv1.Columns("Cost").HeaderText = "Closing Cost"
            gv1.Columns("Cost").FormatString = "{0:n2}"

            gv1.Columns("Balance_FATPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Balance_FATPER").Width = 100
            gv1.Columns("Balance_FATPER").HeaderText = "Closing FAT %"
            gv1.Columns("Balance_FATPER").FormatString = "{0:n2}"

            gv1.Columns("Balance_FAT").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Balance_FAT").Width = 100
            gv1.Columns("Balance_FAT").HeaderText = "Closing FAT (KG)"
            gv1.Columns("Balance_FAT").FormatString = "{0:n2}"

            gv1.Columns("Balance_SNFPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Balance_SNFPER").Width = 100
            gv1.Columns("Balance_SNFPER").HeaderText = "Closing SNF %"
            gv1.Columns("Balance_SNFPER").FormatString = "{0:n2}"

            gv1.Columns("Balance_SNF").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Balance_SNF").Width = 100
            gv1.Columns("Balance_SNF").HeaderText = "Closing SNF (KG)"
            gv1.Columns("Balance_SNF").FormatString = "{0:n2}"
            ''
            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim Smitem As New GridViewSummaryItem("OPBal", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("OPBalCost", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("Received_Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("RecdCost", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("Issued_Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("IssueCost", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("Balance_Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Cost", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)


            Smitem = New GridViewSummaryItem("OPFAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("OPSNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Received_FAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Received_SNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Issued_FAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Issued_SNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Balance_FAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Balance_SNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)


            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Category Wise Summary") = CompairStringResult.Equal Then
            gv1.Columns("Item_Type").IsVisible = False
            gv1.Columns("Item_Type").HeaderText = "Item Type Code"

            gv1.Columns("Item_Type_Name").IsVisible = True
            gv1.Columns("Item_Type_Name").Width = 100
            gv1.Columns("Item_Type_Name").HeaderText = "Item Type"
            gv1.Columns("Item_Group").IsVisible = False
            gv1.Columns("Item_Group").HeaderText = "Item Group Code"

            gv1.Columns("Group_Description").IsVisible = True
            gv1.Columns("Group_Description").Width = 100
            gv1.Columns("Group_Description").HeaderText = "Item Group"

            gv1.Columns("Stock_UOM").IsVisible = True
            gv1.Columns("Stock_UOM").Width = 100
            gv1.Columns("Stock_UOM").HeaderText = "UOM"

            gv1.Columns("OPBal").IsVisible = True
            gv1.Columns("OPBal").Width = 100
            gv1.Columns("OPBal").HeaderText = "Opening Qty"
            gv1.Columns("OPBal").FormatString = "{0:n2}"

            gv1.Columns("OPBalrate").IsVisible = True
            gv1.Columns("OPBalrate").Width = 100
            gv1.Columns("OPBalrate").HeaderText = "Opening Rate"
            gv1.Columns("OPBalrate").FormatString = "{0:n3}"

            gv1.Columns("OPBalCost").IsVisible = True
            gv1.Columns("OPBalCost").Width = 100
            gv1.Columns("OPBalCost").HeaderText = "Opening cost"
            gv1.Columns("OPBalCost").FormatString = "{0:n2}"

            gv1.Columns("OPFATPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("OPFATPER").Width = 100
            gv1.Columns("OPFATPER").HeaderText = "Opening FAT %"
            gv1.Columns("OPFATPER").FormatString = "{0:n2}"

            gv1.Columns("OPFAT").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("OPFAT").Width = 100
            gv1.Columns("OPFAT").HeaderText = "Opening FAT (KG)"
            gv1.Columns("OPFAT").FormatString = "{0:n2}"

            gv1.Columns("OPSNFPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("OPSNFPER").Width = 100
            gv1.Columns("OPSNFPER").HeaderText = "Opening SNF %"
            gv1.Columns("OPSNFPER").FormatString = "{0:n2}"

            gv1.Columns("OPSNF").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("OPSNF").Width = 100
            gv1.Columns("OPSNF").HeaderText = "Opening SNF (KG)"
            gv1.Columns("OPSNF").FormatString = "{0:n2}"

            gv1.Columns("Received_Qty").IsVisible = True
            gv1.Columns("Received_Qty").Width = 100
            gv1.Columns("Received_Qty").HeaderText = "Received Qty"
            gv1.Columns("Received_Qty").FormatString = "{0:n2}"

            gv1.Columns("RecdRate").IsVisible = True
            gv1.Columns("RecdRate").Width = 100
            gv1.Columns("RecdRate").HeaderText = "Received Rate"
            gv1.Columns("RecdRate").FormatString = "{0:n3}"

            gv1.Columns("RecdCost").IsVisible = True
            gv1.Columns("RecdCost").Width = 100
            gv1.Columns("RecdCost").HeaderText = "Received Cost"
            gv1.Columns("RecdCost").FormatString = "{0:n2}"

            gv1.Columns("Received_FATPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Received_FATPER").Width = 100
            gv1.Columns("Received_FATPER").HeaderText = "Received FAT %"
            gv1.Columns("Received_FATPER").FormatString = "{0:n2}"

            gv1.Columns("Received_FAT").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Received_FAT").Width = 100
            gv1.Columns("Received_FAT").HeaderText = "Received FAT (KG)"
            gv1.Columns("Received_FAT").FormatString = "{0:n2}"


            gv1.Columns("Received_SNFPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Received_SNFPER").Width = 100
            gv1.Columns("Received_SNFPER").HeaderText = "Received SNF %"
            gv1.Columns("Received_SNFPER").FormatString = "{0:n2}"

            gv1.Columns("Received_SNF").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Received_SNF").Width = 100
            gv1.Columns("Received_SNF").HeaderText = "Received SNF (KG)"
            gv1.Columns("Received_SNF").FormatString = "{0:n2}"

            gv1.Columns("Issued_Qty").IsVisible = True
            gv1.Columns("Issued_Qty").Width = 100
            gv1.Columns("Issued_Qty").HeaderText = "Issued Qty"
            gv1.Columns("Issued_Qty").FormatString = "{0:n2}"

            gv1.Columns("IssueRate").IsVisible = True
            gv1.Columns("IssueRate").Width = 100
            gv1.Columns("IssueRate").HeaderText = "Issued Rate"
            gv1.Columns("IssueRate").FormatString = "{0:n3}"

            gv1.Columns("IssueCost").IsVisible = True
            gv1.Columns("IssueCost").Width = 100
            gv1.Columns("IssueCost").HeaderText = "Issued Cost"
            gv1.Columns("IssueCost").FormatString = "{0:n2}"

            gv1.Columns("Issued_FATPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Issued_FATPER").Width = 100
            gv1.Columns("Issued_FATPER").HeaderText = "Issued FAT %"
            gv1.Columns("Issued_FATPER").FormatString = "{0:n2}"

            gv1.Columns("Issued_FAT").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Issued_FAT").Width = 100
            gv1.Columns("Issued_FAT").HeaderText = "Issued FAT (KG)"
            gv1.Columns("Issued_FAT").FormatString = "{0:n2}"

            gv1.Columns("Issued_SNFPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Issued_SNFPER").Width = 100
            gv1.Columns("Issued_SNFPER").HeaderText = "Issued SNF %"
            gv1.Columns("Issued_SNFPER").FormatString = "{0:n2}"

            gv1.Columns("Issued_SNF").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Issued_SNF").Width = 100
            gv1.Columns("Issued_SNF").HeaderText = "Issued SNF (KG)"
            gv1.Columns("Issued_SNF").FormatString = "{0:n2}"

            gv1.Columns("Balance_Qty").IsVisible = True
            gv1.Columns("Balance_Qty").Width = 100
            gv1.Columns("Balance_Qty").HeaderText = "Closing Qty"
            gv1.Columns("Balance_Qty").FormatString = "{0:n2}"

            gv1.Columns("Rate").IsVisible = True
            gv1.Columns("Rate").Width = 100
            gv1.Columns("Rate").HeaderText = "Closing Rate"
            gv1.Columns("Rate").FormatString = "{0:n3}"

            gv1.Columns("Cost").IsVisible = True
            gv1.Columns("Cost").Width = 100
            gv1.Columns("Cost").HeaderText = "Closing Cost"
            gv1.Columns("Cost").FormatString = "{0:n2}"

            gv1.Columns("Balance_FATPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Balance_FATPER").Width = 100
            gv1.Columns("Balance_FATPER").HeaderText = "Closing FAT %"
            gv1.Columns("Balance_FATPER").FormatString = "{0:n2}"

            gv1.Columns("Balance_FAT").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Balance_FAT").Width = 100
            gv1.Columns("Balance_FAT").HeaderText = "Closing FAT (KG)"
            gv1.Columns("Balance_FAT").FormatString = "{0:n2}"

            gv1.Columns("Balance_SNFPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Balance_SNFPER").Width = 100
            gv1.Columns("Balance_SNFPER").HeaderText = "Closing SNF %"
            gv1.Columns("Balance_SNFPER").FormatString = "{0:n2}"

            gv1.Columns("Balance_SNF").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Balance_SNF").Width = 100
            gv1.Columns("Balance_SNF").HeaderText = "Closing SNF (KG)"
            gv1.Columns("Balance_SNF").FormatString = "{0:n2}"
            ''
            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim Smitem As New GridViewSummaryItem("OPBal", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("OPBalCost", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("Received_Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("RecdCost", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("Issued_Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("IssueCost", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("Balance_Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Cost", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)


            Smitem = New GridViewSummaryItem("OPFAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("OPSNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Received_FAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Received_SNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Issued_FAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Issued_SNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Balance_FAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Balance_SNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)


            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item And Batch Wise Summary") = CompairStringResult.Equal Then
            gv1.Columns("Item_Type").IsVisible = False
            gv1.Columns("Item_Type").HeaderText = "Item Type Code"

            gv1.Columns("Item_Type_Name").IsVisible = True
            gv1.Columns("Item_Type_Name").Width = 100
            gv1.Columns("Item_Type_Name").HeaderText = "Item Type"

            gv1.Columns("Item_Group").IsVisible = False
            gv1.Columns("Item_Group").HeaderText = "Item Group Code"

            gv1.Columns("Group_Description").IsVisible = True
            gv1.Columns("Group_Description").Width = 100
            gv1.Columns("Group_Description").HeaderText = "Item Group"

            gv1.Columns("Item_Code").HeaderText = "Item Code"

            gv1.Columns("Item_Desc").IsVisible = True
            gv1.Columns("Item_Desc").Width = 150
            gv1.Columns("Item_Desc").HeaderText = "Item"


            gv1.Columns("Batch_No").IsVisible = True
            gv1.Columns("Batch_No").Width = 150
            gv1.Columns("Batch_No").HeaderText = "Batch No"

            gv1.Columns("Manual_BatchNo").IsVisible = True
            gv1.Columns("Manual_BatchNo").Width = 150
            gv1.Columns("Manual_BatchNo").HeaderText = "Manual Batch No"

            gv1.Columns("Manufacture_Date").IsVisible = True
            gv1.Columns("Manufacture_Date").Width = 150
            gv1.Columns("Manufacture_Date").HeaderText = "Manufacture Date"
            gv1.Columns("Manufacture_Date").FormatString = "{0:dd/MM/yyyy}"

            gv1.Columns("Expiry_Date").IsVisible = True
            gv1.Columns("Expiry_Date").Width = 150
            gv1.Columns("Expiry_Date").HeaderText = "Expiry Date"
            gv1.Columns("Expiry_Date").FormatString = "{0:dd/MM/yyyy}"

            gv1.Columns("MRP").IsVisible = True
            gv1.Columns("MRP").Width = 150
            gv1.Columns("MRP").HeaderText = "MRP"



            gv1.Columns("Stock_UOM").IsVisible = True
            gv1.Columns("Stock_UOM").Width = 100
            gv1.Columns("Stock_UOM").HeaderText = "UOM"

            'gv1.Columns("itf_code").IsVisible = True
            'gv1.Columns("itf_code").Width = 150
            gv1.Columns("itf_code").HeaderText = "ITF Code"
            '' Anubhooti 13-Feb-2015

            gv1.Columns("OPBal").IsVisible = True
            gv1.Columns("OPBal").Width = 100
            gv1.Columns("OPBal").HeaderText = "Opening Qty"
            gv1.Columns("OPBal").FormatString = "{0:n2}"

            gv1.Columns("OPBalrate").IsVisible = True
            gv1.Columns("OPBalrate").Width = 100
            gv1.Columns("OPBalrate").HeaderText = "Opening Rate"
            gv1.Columns("OPBalrate").FormatString = "{0:n3}"

            gv1.Columns("OPBalCost").IsVisible = True
            gv1.Columns("OPBalCost").Width = 100
            gv1.Columns("OPBalCost").HeaderText = "Opening Cost"
            gv1.Columns("OPBalCost").FormatString = "{0:n2}"

            gv1.Columns("OPFATPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("OPFATPER").Width = 100
            gv1.Columns("OPFATPER").HeaderText = "Opening FAT %"
            gv1.Columns("OPFATPER").FormatString = "{0:n2}"

            gv1.Columns("OPFAT").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("OPFAT").Width = 100
            gv1.Columns("OPFAT").HeaderText = "Opening FAT (KG)"
            gv1.Columns("OPFAT").FormatString = "{0:n2}"

            gv1.Columns("OPSNFPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("OPSNFPER").Width = 100
            gv1.Columns("OPSNFPER").HeaderText = "Opening SNF %"
            gv1.Columns("OPSNFPER").FormatString = "{0:n2}"

            gv1.Columns("OPSNF").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("OPSNF").Width = 100
            gv1.Columns("OPSNF").HeaderText = "Opening SNF (KG)"
            gv1.Columns("OPSNF").FormatString = "{0:n2}"

            gv1.Columns("Received_Qty").IsVisible = True
            gv1.Columns("Received_Qty").Width = 100
            gv1.Columns("Received_Qty").HeaderText = "Received Qty"
            gv1.Columns("Received_Qty").FormatString = "{0:n2}"

            gv1.Columns("RecdRate").IsVisible = True
            gv1.Columns("RecdRate").Width = 100
            gv1.Columns("RecdRate").HeaderText = "Received Rate"
            gv1.Columns("RecdRate").FormatString = "{0:n3}"

            gv1.Columns("RecdCost").IsVisible = True
            gv1.Columns("RecdCost").Width = 100
            gv1.Columns("RecdCost").HeaderText = "Received Cost"
            gv1.Columns("RecdCost").FormatString = "{0:n2}"

            gv1.Columns("Received_FATPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Received_FATPER").Width = 100
            gv1.Columns("Received_FATPER").HeaderText = "Received FAT %"
            gv1.Columns("Received_FATPER").FormatString = "{0:n2}"

            gv1.Columns("Received_FAT").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Received_FAT").Width = 100
            gv1.Columns("Received_FAT").HeaderText = "Received FAT (KG)"
            gv1.Columns("Received_FAT").FormatString = "{0:n2}"

            gv1.Columns("Received_SNFPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Received_SNFPER").Width = 100
            gv1.Columns("Received_SNFPER").HeaderText = "Received SNF %"
            gv1.Columns("Received_SNFPER").FormatString = "{0:n2}"

            gv1.Columns("Received_SNF").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Received_SNF").Width = 100
            gv1.Columns("Received_SNF").HeaderText = "Received SNF (KG)"
            gv1.Columns("Received_SNF").FormatString = "{0:n2}"

            gv1.Columns("Issued_Qty").IsVisible = True
            gv1.Columns("Issued_Qty").Width = 100
            gv1.Columns("Issued_Qty").HeaderText = "Issued Qty"
            gv1.Columns("Issued_Qty").FormatString = "{0:n2}"

            gv1.Columns("IssueRate").IsVisible = True
            gv1.Columns("IssueRate").Width = 100
            gv1.Columns("IssueRate").HeaderText = "Issued Rate"
            gv1.Columns("IssueRate").FormatString = "{0:n3}"

            gv1.Columns("IssueCost").IsVisible = True
            gv1.Columns("IssueCost").Width = 100
            gv1.Columns("IssueCost").HeaderText = "Issued Cost"
            gv1.Columns("IssueCost").FormatString = "{0:n2}"

            gv1.Columns("Issued_FATPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Issued_FATPER").Width = 100
            gv1.Columns("Issued_FATPER").HeaderText = "Issued FAT %"
            gv1.Columns("Issued_FATPER").FormatString = "{0:n2}"

            gv1.Columns("Issued_FAT").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Issued_FAT").Width = 100
            gv1.Columns("Issued_FAT").HeaderText = "Issued FAT"
            gv1.Columns("Issued_FAT").FormatString = "{0:n2}"

            gv1.Columns("Issued_SNFPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Issued_SNFPER").Width = 100
            gv1.Columns("Issued_SNFPER").HeaderText = "Issued SNF %"
            gv1.Columns("Issued_SNFPER").FormatString = "{0:n2}"

            gv1.Columns("Issued_SNF").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Issued_SNF").Width = 100
            gv1.Columns("Issued_SNF").HeaderText = "Issued SNF"
            gv1.Columns("Issued_SNF").FormatString = "{0:n2}"

            gv1.Columns("Balance_Qty").IsVisible = True
            gv1.Columns("Balance_Qty").Width = 100
            gv1.Columns("Balance_Qty").HeaderText = "Closing Qty"
            gv1.Columns("Balance_Qty").FormatString = "{0:n2}"


            gv1.Columns("Rate").IsVisible = True
            gv1.Columns("Rate").Width = 100
            gv1.Columns("Rate").HeaderText = "Closing Rate"
            gv1.Columns("Rate").FormatString = "{0:n3}"

            gv1.Columns("Cost").IsVisible = True
            gv1.Columns("Cost").Width = 100
            gv1.Columns("Cost").HeaderText = "Closing Cost"
            gv1.Columns("Cost").FormatString = "{0:n2}"

            gv1.Columns("Balance_FATPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Balance_FATPER").Width = 100
            gv1.Columns("Balance_FATPER").HeaderText = "Closing FAT %"
            gv1.Columns("Balance_FATPER").FormatString = "{0:n2}"

            gv1.Columns("Balance_FAT").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Balance_FAT").Width = 100
            gv1.Columns("Balance_FAT").HeaderText = "Closing FAT (KG)"
            gv1.Columns("Balance_FAT").FormatString = "{0:n2}"

            gv1.Columns("Balance_SNFPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Balance_SNFPER").Width = 100
            gv1.Columns("Balance_SNFPER").HeaderText = "Closing SNF %"
            gv1.Columns("Balance_SNFPER").FormatString = "{0:n2}"

            gv1.Columns("Balance_SNF").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Balance_SNF").Width = 100
            gv1.Columns("Balance_SNF").HeaderText = "Closing SNF (KG)"
            gv1.Columns("Balance_SNF").FormatString = "{0:n2}"

            gv1.Columns("OP TS %").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("OP TS %").Width = 100
            gv1.Columns("OP TS %").FormatString = "{0:n2}"
            gv1.Columns("OP TS (KG)").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("OP TS (KG)").Width = 100
            gv1.Columns("OP TS (KG)").FormatString = "{0:n2}"
            gv1.Columns("Received TS %").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Received TS %").Width = 100
            gv1.Columns("Received TS %").FormatString = "{0:n2}"
            gv1.Columns("Received TS (KG)").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Received TS (KG)").Width = 100
            gv1.Columns("Received TS (KG)").FormatString = "{0:n2}"
            gv1.Columns("Issued TS %").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Issued TS %").Width = 100
            gv1.Columns("Issued TS %").FormatString = "{0:n2}"
            gv1.Columns("Issued TS (KG)").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Issued TS (KG)").Width = 100
            gv1.Columns("Issued TS (KG)").FormatString = "{0:n2}"
            gv1.Columns("Balance TS %").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Balance TS %").Width = 100
            gv1.Columns("Balance TS %").HeaderText = "Closing TS %"
            gv1.Columns("Balance TS %").FormatString = "{0:n2}"
            gv1.Columns("Balance TS (KG)").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Balance TS (KG)").Width = 100
            gv1.Columns("Balance TS (KG)").HeaderText = "Closing TS (KG)"
            gv1.Columns("Balance TS (KG)").FormatString = "{0:n2}"

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim Smitem As New GridViewSummaryItem("OPBal", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("OPBalCost", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("Received_Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("RecdCost", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("Issued_Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("IssueCost", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("Balance_Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Cost", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("OPFAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("OPSNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Received_FAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Received_SNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Issued_FAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Issued_SNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Balance_FAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Balance_SNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("OP TS (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Received TS (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Issued TS (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Balance TS (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item Batch And Location Wise Summary") = CompairStringResult.Equal Then
            gv1.Columns("Item_Type").IsVisible = False
            gv1.Columns("Item_Type").HeaderText = "Item Type Code"

            gv1.Columns("Item_Type_Name").IsVisible = True
            gv1.Columns("Item_Type_Name").Width = 100
            gv1.Columns("Item_Type_Name").HeaderText = "Item Type"

            gv1.Columns("Item_Group").IsVisible = False
            gv1.Columns("Item_Group").HeaderText = "Item Group Code"

            gv1.Columns("Group_Description").IsVisible = True
            gv1.Columns("Group_Description").Width = 100
            gv1.Columns("Group_Description").HeaderText = "Item Group"

            'gv1.Columns("Main_Location_Code").IsVisible = True
            'gv1.Columns("Main_Location_Code").Width = 100
            gv1.Columns("Main_Location_Code").HeaderText = "Main Location Code"

            gv1.Columns("MainLocationDesc").IsVisible = True
            gv1.Columns("MainLocationDesc").Width = 150
            gv1.Columns("MainLocationDesc").HeaderText = "Main Location"

            'gv1.Columns("Location_Code").IsVisible = True
            'gv1.Columns("Location_Code").Width = 100
            gv1.Columns("Location_Code").HeaderText = "Location Code"

            gv1.Columns("Loc Desp").IsVisible = True
            gv1.Columns("Loc Desp").Width = 150
            gv1.Columns("Loc Desp").HeaderText = "Location"

            'gv1.Columns("Item_Code").IsVisible = True
            'gv1.Columns("Item_Code").Width = 100
            gv1.Columns("Item_Code").HeaderText = "Item Code"

            gv1.Columns("Item_Desc").IsVisible = True
            gv1.Columns("Item_Desc").Width = 150
            gv1.Columns("Item_Desc").HeaderText = "Item"

            gv1.Columns("Batch_No").IsVisible = True
            gv1.Columns("Batch_No").Width = 150
            gv1.Columns("Batch_No").HeaderText = "Batch No"

            gv1.Columns("Manual_BatchNo").IsVisible = True
            gv1.Columns("Manual_BatchNo").Width = 150
            gv1.Columns("Manual_BatchNo").HeaderText = "Manual Batch No"

            gv1.Columns("Manufacture_Date").IsVisible = True
            gv1.Columns("Manufacture_Date").Width = 150
            gv1.Columns("Manufacture_Date").HeaderText = "Manufacture Date"
            gv1.Columns("Manufacture_Date").FormatString = "{0:dd/MM/yyyy}"

            gv1.Columns("Expiry_Date").IsVisible = True
            gv1.Columns("Expiry_Date").Width = 150
            gv1.Columns("Expiry_Date").HeaderText = "Expiry Date"
            gv1.Columns("Expiry_Date").FormatString = "{0:dd/MM/yyyy}"

            gv1.Columns("MRP").IsVisible = True
            gv1.Columns("MRP").Width = 150
            gv1.Columns("MRP").HeaderText = "MRP"

            gv1.Columns("Stock_UOM").IsVisible = True
            gv1.Columns("Stock_UOM").Width = 100
            gv1.Columns("Stock_UOM").HeaderText = "UOM"

            'gv1.Columns("itf_code").IsVisible = True
            'gv1.Columns("itf_code").Width = 150
            gv1.Columns("itf_code").HeaderText = "ITF Code"
            '' Anubhooti 13-Feb-2015

            gv1.Columns("OPBal").IsVisible = True
            gv1.Columns("OPBal").Width = 100
            gv1.Columns("OPBal").HeaderText = "Opening Qty"
            gv1.Columns("OPBal").FormatString = "{0:n2}"

            gv1.Columns("OPBalrate").IsVisible = True
            gv1.Columns("OPBalrate").Width = 100
            gv1.Columns("OPBalrate").HeaderText = "Opening Rate"
            gv1.Columns("OPBalrate").FormatString = "{0:n3}"

            gv1.Columns("OPBalCost").IsVisible = True
            gv1.Columns("OPBalCost").Width = 100
            gv1.Columns("OPBalCost").HeaderText = "Opening Cost"
            gv1.Columns("OPBalCost").FormatString = "{0:n2}"

            gv1.Columns("OPFAT").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("OPFAT").Width = 100
            gv1.Columns("OPFAT").HeaderText = "Opening FAT (KG)"
            gv1.Columns("OPFAT").FormatString = "{0:n2}"

            gv1.Columns("OPFATPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("OPFATPER").Width = 100
            gv1.Columns("OPFATPER").HeaderText = "Opening FAT %"
            gv1.Columns("OPFATPER").FormatString = "{0:n2}"

            gv1.Columns("OPSNF").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("OPSNF").Width = 100
            gv1.Columns("OPSNF").HeaderText = "Opening SNF (KG)"
            gv1.Columns("OPSNF").FormatString = "{0:n2}"

            gv1.Columns("OPSNFPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("OPSNFPER").Width = 100
            gv1.Columns("OPSNFPER").HeaderText = "Opening SNF %"
            gv1.Columns("OPSNFPER").FormatString = "{0:n2}"

            gv1.Columns("Received_Qty").IsVisible = True
            gv1.Columns("Received_Qty").Width = 100
            gv1.Columns("Received_Qty").HeaderText = "Received Qty"
            gv1.Columns("Received_Qty").FormatString = "{0:n2}"

            gv1.Columns("RecdRate").IsVisible = True
            gv1.Columns("RecdRate").Width = 100
            gv1.Columns("RecdRate").HeaderText = "Received Rate"
            gv1.Columns("RecdRate").FormatString = "{0:n3}"

            gv1.Columns("RecdCost").IsVisible = True
            gv1.Columns("RecdCost").Width = 100
            gv1.Columns("RecdCost").HeaderText = "Received Cost"
            gv1.Columns("RecdCost").FormatString = "{0:n2}"

            gv1.Columns("Received_FATPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Received_FATPER").Width = 100
            gv1.Columns("Received_FATPER").HeaderText = "Received FAT %"
            gv1.Columns("Received_FATPER").FormatString = "{0:n2}"

            gv1.Columns("Received_FAT").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Received_FAT").Width = 100
            gv1.Columns("Received_FAT").HeaderText = "Received FAT (KG)"
            gv1.Columns("Received_FAT").FormatString = "{0:n2}"

            gv1.Columns("Received_SNFPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Received_SNFPER").Width = 100
            gv1.Columns("Received_SNFPER").HeaderText = "Received SNF %"
            gv1.Columns("Received_SNFPER").FormatString = "{0:n2}"

            gv1.Columns("Received_SNF").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Received_SNF").Width = 100
            gv1.Columns("Received_SNF").HeaderText = "Received SNF (KG)"
            gv1.Columns("Received_SNF").FormatString = "{0:n2}"

            gv1.Columns("Issued_Qty").IsVisible = True
            gv1.Columns("Issued_Qty").Width = 100
            gv1.Columns("Issued_Qty").HeaderText = "Issued Qty"
            gv1.Columns("Issued_Qty").FormatString = "{0:n2}"

            gv1.Columns("IssueRate").IsVisible = True
            gv1.Columns("IssueRate").Width = 100
            gv1.Columns("IssueRate").HeaderText = "Issued Rate"
            gv1.Columns("IssueRate").FormatString = "{0:n3}"

            gv1.Columns("IssueCost").IsVisible = True
            gv1.Columns("IssueCost").Width = 100
            gv1.Columns("IssueCost").HeaderText = "Issued Cost"
            gv1.Columns("IssueCost").FormatString = "{0:n2}"

            gv1.Columns("Issued_FATPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Issued_FATPER").Width = 100
            gv1.Columns("Issued_FATPER").HeaderText = "Issued FAT %"
            gv1.Columns("Issued_FATPER").FormatString = "{0:n2}"

            gv1.Columns("Issued_FAT").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Issued_FAT").Width = 100
            gv1.Columns("Issued_FAT").HeaderText = "Issued FAT (KG)"
            gv1.Columns("Issued_FAT").FormatString = "{0:n2}"

            gv1.Columns("Issued_SNFPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Issued_SNFPER").Width = 100
            gv1.Columns("Issued_SNFPER").HeaderText = "Issued SNF %"
            gv1.Columns("Issued_SNFPER").FormatString = "{0:n2}"

            gv1.Columns("Issued_SNF").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Issued_SNF").Width = 100
            gv1.Columns("Issued_SNF").HeaderText = "Issued SNF (KG)"
            gv1.Columns("Issued_SNF").FormatString = "{0:n2}"

            gv1.Columns("Balance_Qty").IsVisible = True
            gv1.Columns("Balance_Qty").Width = 100
            gv1.Columns("Balance_Qty").HeaderText = "Closing Qty"
            gv1.Columns("Balance_Qty").FormatString = "{0:n2}"


            gv1.Columns("Rate").IsVisible = True
            gv1.Columns("Rate").Width = 100
            gv1.Columns("Rate").HeaderText = "Closing Rate"
            gv1.Columns("Rate").FormatString = "{0:n3}"

            gv1.Columns("Cost").IsVisible = True
            gv1.Columns("Cost").Width = 100
            gv1.Columns("Cost").HeaderText = "Closing Cost"
            gv1.Columns("Cost").FormatString = "{0:n2}"

            gv1.Columns("Balance_FATPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Balance_FATPER").Width = 100
            gv1.Columns("Balance_FATPER").HeaderText = "Closing FAT %"
            gv1.Columns("Balance_FATPER").FormatString = "{0:n2}"

            gv1.Columns("Balance_FAT").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Balance_FAT").Width = 100
            gv1.Columns("Balance_FAT").HeaderText = "Closing FAT (KG)"
            gv1.Columns("Balance_FAT").FormatString = "{0:n2}"

            gv1.Columns("Balance_SNFPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Balance_SNFPER").Width = 100
            gv1.Columns("Balance_SNFPER").HeaderText = "Closing SNF %"
            gv1.Columns("Balance_SNFPER").FormatString = "{0:n2}"

            gv1.Columns("Balance_SNF").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Balance_SNF").Width = 100
            gv1.Columns("Balance_SNF").HeaderText = "Closing SNF (KG)"
            gv1.Columns("Balance_SNF").FormatString = "{0:n2}"

            gv1.Columns("OP TS %").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("OP TS %").Width = 100
            gv1.Columns("OP TS %").FormatString = "{0:n2}"
            gv1.Columns("OP TS (KG)").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("OP TS (KG)").Width = 100
            gv1.Columns("OP TS (KG)").FormatString = "{0:n2}"
            gv1.Columns("Received TS %").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Received TS %").Width = 100
            gv1.Columns("Received TS %").FormatString = "{0:n2}"
            gv1.Columns("Received TS (KG)").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Received TS (KG)").Width = 100
            gv1.Columns("Received TS (KG)").FormatString = "{0:n2}"
            gv1.Columns("Issued TS %").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Issued TS %").Width = 100
            gv1.Columns("Issued TS %").FormatString = "{0:n2}"
            gv1.Columns("Issued TS (KG)").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Issued TS (KG)").Width = 100
            gv1.Columns("Issued TS (KG)").FormatString = "{0:n2}"
            gv1.Columns("Balance TS %").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Balance TS %").Width = 100
            gv1.Columns("Balance TS %").HeaderText = "Closing TS %"
            gv1.Columns("Balance TS %").FormatString = "{0:n2}"
            gv1.Columns("Balance TS (KG)").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Balance TS (KG)").Width = 100
            gv1.Columns("Balance TS (KG)").HeaderText = "Closing TS (KG)"
            gv1.Columns("Balance TS (KG)").FormatString = "{0:n2}"

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim Smitem As New GridViewSummaryItem("OPBal", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("OPBalCost", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("Received_Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("RecdCost", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("Issued_Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("IssueCost", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("Balance_Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Cost", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("OPFAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("OPSNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Received_FAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Received_SNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Issued_FAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Issued_SNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Balance_FAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Balance_SNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("OP TS (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Received TS (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Issued TS (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Balance TS (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Document Wise Detail") = CompairStringResult.Equal Then
            gv1.Columns("Trans_Id").HeaderText = "Trans_Id"

            gv1.Columns("Trans_Type").HeaderText = "Transaction Type Code"

            gv1.Columns("CompName").IsVisible = False
            gv1.Columns("FromDate").IsVisible = False
            gv1.Columns("ToDate").IsVisible = False

            gv1.Columns("Trans_Type_Name").IsVisible = True
            gv1.Columns("Trans_Type_Name").Width = 100
            gv1.Columns("Trans_Type_Name").HeaderText = "Transaction Type"

            gv1.Columns("Source_Doc_No").IsVisible = True
            gv1.Columns("Source_Doc_No").Width = 100
            gv1.Columns("Source_Doc_No").HeaderText = "Source Doc No"

            gv1.Columns("Batch_No").IsVisible = True
            gv1.Columns("Batch_No").Width = 100
            gv1.Columns("Batch_No").HeaderText = "Batch No"

            gv1.Columns("Punching_Date").IsVisible = True
            gv1.Columns("Punching_Date").Width = 100
            gv1.Columns("Punching_Date").HeaderText = "Document Date"

            gv1.Columns("Manual_BatchNo").IsVisible = True
            gv1.Columns("Manual_BatchNo").Width = 100
            gv1.Columns("Manual_BatchNo").HeaderText = "Manual Batch No"

            gv1.Columns("InOutView").IsVisible = True
            gv1.Columns("InOutView").Width = 100
            gv1.Columns("InOutView").HeaderText = "Type"
            ' kunal > ticket : BM00000009881 > Date : 01 - 0ct - 2016
            gv1.Columns("Location_Code").IsVisible = True
            gv1.Columns("Location_Code").Width = 100
            gv1.Columns("Location_Code").HeaderText = "Location Code"
            ' kunal > ticket : BM00000009881 > Date : 01 - 0ct - 2016
            gv1.Columns("Loc Desp").IsVisible = True
            gv1.Columns("Loc Desp").Width = 100
            gv1.Columns("Loc Desp").HeaderText = "In Location Desp"
            ' kunal > ticket : BM00000009881 > Date : 01 - 0ct - 2016
            gv1.Columns("SourceName").IsVisible = True
            gv1.Columns("SourceName").Width = 100
            gv1.Columns("SourceName").HeaderText = "From Location"

            gv1.Columns("Item_Type").IsVisible = False
            gv1.Columns("Item_Type").HeaderText = "Item Type Code"

            gv1.Columns("Item_Type_Name").IsVisible = True
            gv1.Columns("Item_Type_Name").Width = 100
            gv1.Columns("Item_Type_Name").HeaderText = "Item Type"

            gv1.Columns("Item_Group").IsVisible = False
            gv1.Columns("Item_Group").HeaderText = "Item Group Code"

            gv1.Columns("Group_Description").IsVisible = True
            gv1.Columns("Group_Description").Width = 100
            gv1.Columns("Group_Description").HeaderText = "Item Group"

            gv1.Columns("Item_Code").HeaderText = "Item Code"

            gv1.Columns("Item_Desc").IsVisible = True
            gv1.Columns("Item_Desc").Width = 150
            gv1.Columns("Item_Desc").HeaderText = "Item"

            gv1.Columns("Stock_UOM").IsVisible = True
            gv1.Columns("Stock_UOM").Width = 100
            gv1.Columns("Stock_UOM").HeaderText = "UOM"

            gv1.Columns("itf_code").HeaderText = "ITF Code"

            gv1.Columns("Stock_Qty").IsVisible = True
            gv1.Columns("Stock_Qty").Width = 100
            gv1.Columns("Stock_Qty").HeaderText = "Quantity"
            gv1.Columns("Stock_Qty").FormatString = "{0:n2}"

            gv1.Columns("Rate").IsVisible = True
            gv1.Columns("Rate").Width = 100
            gv1.Columns("Rate").HeaderText = "Rate"
            gv1.Columns("Rate").FormatString = "{0:n3}"

            gv1.Columns("Cost").IsVisible = True
            gv1.Columns("Cost").Width = 100
            gv1.Columns("Cost").HeaderText = "Amount"
            gv1.Columns("Cost").FormatString = "{0:n2}"

            gv1.Columns("Balance_FATPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Balance_FATPER").Width = 100
            gv1.Columns("Balance_FATPER").HeaderText = "Closing FAT %"
            gv1.Columns("Balance_FATPER").FormatString = "{0:n2}"

            gv1.Columns("Balance_FAT").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Balance_FAT").Width = 100
            gv1.Columns("Balance_FAT").HeaderText = "FAT (KG)"
            gv1.Columns("Balance_FAT").FormatString = "{0:n2}"

            gv1.Columns("Balance_SNFPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Balance_SNFPER").Width = 100
            gv1.Columns("Balance_SNFPER").HeaderText = "Closing SNF %"
            gv1.Columns("Balance_SNFPER").FormatString = "{0:n2}"

            gv1.Columns("Balance_SNF").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Balance_SNF").Width = 100
            gv1.Columns("Balance_SNF").HeaderText = "SNF (KG)"
            gv1.Columns("Balance_SNF").FormatString = "{0:n2}"

            gv1.Columns("Balance TS %").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Balance TS %").Width = 100
            gv1.Columns("Balance TS %").HeaderText = "Closing TS %"
            gv1.Columns("Balance TS %").FormatString = "{0:n2}"
            gv1.Columns("Balance TS (KG)").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Balance TS (KG)").Width = 100
            gv1.Columns("Balance TS (KG)").HeaderText = "Closing TS (KG)"
            gv1.Columns("Balance TS (KG)").FormatString = "{0:n2}"

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item1 As New GridViewSummaryItem("Stock_Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("Cost", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("Balance_FAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("Balance_SNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)

            item1 = New GridViewSummaryItem("Balance TS (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)

            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Date, Item And Document Wise Detail") = CompairStringResult.Equal Then
            gv1.Columns("Source_Doc_No").IsVisible = True
            gv1.Columns("Source_Doc_No").Width = 100
            gv1.Columns("Source_Doc_No").HeaderText = "Source Doc No"

            gv1.Columns("Punching_Date").IsVisible = True
            gv1.Columns("Punching_Date").Width = 100
            gv1.Columns("Punching_Date").HeaderText = "Document Date"

            gv1.Columns("Batch_No").IsVisible = True
            gv1.Columns("Batch_No").Width = 100
            gv1.Columns("Batch_No").HeaderText = "Batch No"

            gv1.Columns("Manual_BatchNo").IsVisible = True
            gv1.Columns("Manual_BatchNo").Width = 100
            gv1.Columns("Manual_BatchNo").HeaderText = "Manual Batch No"

            gv1.Columns("Item_Type_Name").IsVisible = True
            gv1.Columns("Item_Type_Name").Width = 100
            gv1.Columns("Item_Type_Name").HeaderText = "Item Type"

            gv1.Columns("InOutView").IsVisible = True
            gv1.Columns("InOutView").Width = 100
            gv1.Columns("InOutView").HeaderText = "Type"

            gv1.Columns("Item_Code").IsVisible = True
            gv1.Columns("Item_Code").Width = 100
            gv1.Columns("Item_Code").HeaderText = "Item Code"

            gv1.Columns("Item_Desc").IsVisible = True
            gv1.Columns("Item_Desc").Width = 150
            gv1.Columns("Item_Desc").HeaderText = "Item"

            gv1.Columns("Stock_UOM").IsVisible = True
            gv1.Columns("Stock_UOM").Width = 100
            gv1.Columns("Stock_UOM").HeaderText = "UOM"

            gv1.Columns("OPQty").IsVisible = True
            gv1.Columns("OPQty").Width = 100
            gv1.Columns("OPQty").HeaderText = "Opening Quantity"
            gv1.Columns("OPQty").FormatString = "{0:n2}"

            gv1.Columns("OPRate").IsVisible = True
            gv1.Columns("OPRate").Width = 100
            gv1.Columns("OPRate").HeaderText = "Opening Rate"
            gv1.Columns("OPRate").FormatString = "{0:n3}"

            gv1.Columns("OPFAT").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("OPFAT").Width = 100
            gv1.Columns("OPFAT").HeaderText = "Opening FAT"
            gv1.Columns("OPFAT").FormatString = "{0:n2}"

            gv1.Columns("OPFATPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("OPFATPER").Width = 100
            gv1.Columns("OPFATPER").HeaderText = "Opening Fat %"
            gv1.Columns("OPFATPER").FormatString = "{0:n2}"

            gv1.Columns("OPSNF").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("OPSNF").Width = 100
            gv1.Columns("OPSNF").HeaderText = "Opening SNF"
            gv1.Columns("OPSNF").FormatString = "{0:n2}"

            gv1.Columns("OPSNFPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("OPSNFPER").Width = 100
            gv1.Columns("OPSNFPER").HeaderText = "Opening SNF%"
            gv1.Columns("OPSNFPER").FormatString = "{0:n2}"

            gv1.Columns("OPCost").IsVisible = True
            gv1.Columns("OPCost").Width = 100
            gv1.Columns("OPCost").HeaderText = "Opening Amount"
            gv1.Columns("OPCost").FormatString = "{0:n2}"

            gv1.Columns("RecQty").IsVisible = True
            gv1.Columns("RecQty").Width = 100
            gv1.Columns("RecQty").HeaderText = "Received Quantity"
            gv1.Columns("RecQty").FormatString = "{0:n2}"

            gv1.Columns("RecRate").IsVisible = True
            gv1.Columns("RecRate").Width = 100
            gv1.Columns("RecRate").HeaderText = "Received Rate"
            gv1.Columns("RecRate").FormatString = "{0:n3}"

            gv1.Columns("RecFAT").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("RecFAT").Width = 100
            gv1.Columns("RecFAT").HeaderText = "Received FAT"
            gv1.Columns("RecFAT").FormatString = "{0:n2}"

            gv1.Columns("RecFATPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("RecFATPER").Width = 100
            gv1.Columns("RecFATPER").HeaderText = "Received Fat %"
            gv1.Columns("RecFATPER").FormatString = "{0:n2}"

            gv1.Columns("RecSNF").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("RecSNF").Width = 100
            gv1.Columns("RecSNF").HeaderText = "Received SNF"
            gv1.Columns("RecSNF").FormatString = "{0:n2}"

            gv1.Columns("RecSNFPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("RecSNFPER").Width = 100
            gv1.Columns("RecSNFPER").HeaderText = "Received SNF%"
            gv1.Columns("RecSNFPER").FormatString = "{0:n2}"

            gv1.Columns("RecCost").IsVisible = True
            gv1.Columns("RecCost").Width = 100
            gv1.Columns("RecCost").HeaderText = "Received Amount"
            gv1.Columns("RecCost").FormatString = "{0:n2}"


            gv1.Columns("IssQty").IsVisible = True
            gv1.Columns("IssQty").Width = 100
            gv1.Columns("IssQty").HeaderText = "Issued Quantity"
            gv1.Columns("IssQty").FormatString = "{0:n2}"

            gv1.Columns("IssRate").IsVisible = True
            gv1.Columns("IssRate").Width = 100
            gv1.Columns("IssRate").HeaderText = "Issued Rate"
            gv1.Columns("IssRate").FormatString = "{0:n3}"

            gv1.Columns("IssFAT").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("IssFAT").Width = 100
            gv1.Columns("IssFAT").HeaderText = "Issued FAT"
            gv1.Columns("IssFAT").FormatString = "{0:n2}"

            gv1.Columns("IssFATPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("IssFATPER").Width = 100
            gv1.Columns("IssFATPER").HeaderText = "Issued Fat %"
            gv1.Columns("IssFATPER").FormatString = "{0:n2}"

            gv1.Columns("IssSNF").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("IssSNF").Width = 100
            gv1.Columns("IssSNF").HeaderText = "Issued SNF"
            gv1.Columns("IssSNF").FormatString = "{0:n2}"

            gv1.Columns("IssSNFPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("IssSNFPER").Width = 100
            gv1.Columns("IssSNFPER").HeaderText = "Issued SNF%"
            gv1.Columns("IssSNFPER").FormatString = "{0:n2}"

            gv1.Columns("IssCost").IsVisible = True
            gv1.Columns("IssCost").Width = 100
            gv1.Columns("IssCost").HeaderText = "Issued Amount"
            gv1.Columns("IssCost").FormatString = "{0:n2}"


            gv1.Columns("CLQty").IsVisible = True
            gv1.Columns("CLQty").Width = 100
            gv1.Columns("CLQty").HeaderText = "Balance Quantity"
            gv1.Columns("CLQty").FormatString = "{0:n2}"

            gv1.Columns("CLRate").IsVisible = True
            gv1.Columns("CLRate").Width = 100
            gv1.Columns("CLRate").HeaderText = "Balance Rate"
            gv1.Columns("CLRate").FormatString = "{0:n3}"

            gv1.Columns("CLFAT").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("CLFAT").Width = 100
            gv1.Columns("CLFAT").HeaderText = "Balance FAT"
            gv1.Columns("CLFAT").FormatString = "{0:n2}"

            gv1.Columns("CLFATPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("CLFATPER").Width = 100
            gv1.Columns("CLFATPER").HeaderText = "Balance Fat %"
            gv1.Columns("CLFATPER").FormatString = "{0:n2}"

            gv1.Columns("CLSNF").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("CLSNF").Width = 100
            gv1.Columns("CLSNF").HeaderText = "Balance SNF"
            gv1.Columns("CLSNF").FormatString = "{0:n2}"

            gv1.Columns("CLSNFPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("CLSNFPER").Width = 100
            gv1.Columns("CLSNFPER").HeaderText = "Balance SNF%"
            gv1.Columns("CLSNFPER").FormatString = "{0:n2}"

            gv1.Columns("CLCost").IsVisible = True
            gv1.Columns("CLCost").Width = 100
            gv1.Columns("CLCost").HeaderText = "Balance Amount"
            gv1.Columns("CLCost").FormatString = "{0:n2}"

            gv1.Columns("OP TS %").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("OP TS %").Width = 100
            gv1.Columns("OP TS %").FormatString = "{0:n2}"
            gv1.Columns("OP TS (KG)").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("OP TS (KG)").Width = 100
            gv1.Columns("OP TS (KG)").FormatString = "{0:n2}"
            gv1.Columns("Received TS %").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Received TS %").Width = 100
            gv1.Columns("Received TS %").FormatString = "{0:n2}"
            gv1.Columns("Received TS (KG)").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Received TS (KG)").Width = 100
            gv1.Columns("Received TS (KG)").FormatString = "{0:n2}"
            gv1.Columns("Issued TS %").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Issued TS %").Width = 100
            gv1.Columns("Issued TS %").FormatString = "{0:n2}"
            gv1.Columns("Issued TS (KG)").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Issued TS (KG)").Width = 100
            gv1.Columns("Issued TS (KG)").FormatString = "{0:n2}"
            gv1.Columns("Balance TS %").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Balance TS %").Width = 100
            gv1.Columns("Balance TS %").HeaderText = "Balance TS %"
            gv1.Columns("Balance TS %").FormatString = "{0:n2}"
            gv1.Columns("Balance TS (KG)").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("Balance TS (KG)").Width = 100
            gv1.Columns("Balance TS (KG)").HeaderText = "Balance TS (KG)"
            gv1.Columns("Balance TS (KG)").FormatString = "{0:n2}"


            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item2 As New GridViewSummaryItem("RecQty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("RecCost", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("IssQty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("IssCost", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("CLQty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("CLCost", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)


            item2 = New GridViewSummaryItem("OPFAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("OPSNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("RecFAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("RecSNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("IssFAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("IssSNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("CLFAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("CLSNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)

            item2 = New GridViewSummaryItem("OP TS (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("Received TS (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("Issued TS (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("Balance TS (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)

            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Date and Item Wise Stock") = CompairStringResult.Equal Then
            gv1.Columns("Location_Code").IsVisible = True
            gv1.Columns("Location_Code").Width = 100
            gv1.Columns("Location_Code").HeaderText = "Location Code"

            gv1.Columns("Loc Desp").IsVisible = True
            gv1.Columns("Loc Desp").Width = 100
            gv1.Columns("Loc Desp").HeaderText = "Location"

            gv1.Columns("Punching_Date").IsVisible = True
            gv1.Columns("Punching_Date").Width = 100
            gv1.Columns("Punching_Date").HeaderText = "Date"

            gv1.Columns("Item_Code").IsVisible = True
            gv1.Columns("Item_Code").Width = 100
            gv1.Columns("Item_Code").HeaderText = "Item Code"

            gv1.Columns("Item_Desc").IsVisible = True
            gv1.Columns("Item_Desc").Width = 150
            gv1.Columns("Item_Desc").HeaderText = "Item"

            gv1.Columns("Stock_UOM").IsVisible = True
            gv1.Columns("Stock_UOM").Width = 100
            gv1.Columns("Stock_UOM").HeaderText = "UOM"

            gv1.Columns("OPQty").IsVisible = True
            gv1.Columns("OPQty").Width = 100
            gv1.Columns("OPQty").HeaderText = "Opening Quantity"
            gv1.Columns("OPQty").FormatString = "{0:n2}"

            gv1.Columns("OPRate").IsVisible = True
            gv1.Columns("OPRate").Width = 100
            gv1.Columns("OPRate").HeaderText = "Opening Rate"
            gv1.Columns("OPRate").FormatString = "{0:n3}"

            gv1.Columns("OPFAT").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("OPFAT").Width = 100
            gv1.Columns("OPFAT").HeaderText = "Opening FAT"
            gv1.Columns("OPFAT").FormatString = "{0:n2}"

            gv1.Columns("OPFATPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("OPFATPER").Width = 100
            gv1.Columns("OPFATPER").HeaderText = "Opening Fat %"
            gv1.Columns("OPFATPER").FormatString = "{0:n2}"

            gv1.Columns("OPSNF").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("OPSNF").Width = 100
            gv1.Columns("OPSNF").HeaderText = "Opening SNF"
            gv1.Columns("OPSNF").FormatString = "{0:n2}"

            gv1.Columns("OPSNFPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("OPSNFPER").Width = 100
            gv1.Columns("OPSNFPER").HeaderText = "Opening SNF%"
            gv1.Columns("OPSNFPER").FormatString = "{0:n2}"

            gv1.Columns("OPCost").IsVisible = True
            gv1.Columns("OPCost").Width = 100
            gv1.Columns("OPCost").HeaderText = "Opening Amount"
            gv1.Columns("OPCost").FormatString = "{0:n2}"


            gv1.Columns("RecPurQty").IsVisible = True
            gv1.Columns("RecPurQty").Width = 100
            gv1.Columns("RecPurQty").HeaderText = "Received Purchase Quantity"
            gv1.Columns("RecPurQty").FormatString = "{0:n2}"

            gv1.Columns("RecPurRate").IsVisible = True
            gv1.Columns("RecPurRate").Width = 100
            gv1.Columns("RecPurRate").HeaderText = "Received Purchase Rate"
            gv1.Columns("RecPurRate").FormatString = "{0:n3}"

            gv1.Columns("RecPurFAT").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("RecPurFAT").Width = 100
            gv1.Columns("RecPurFAT").HeaderText = "Received Purchase FAT"
            gv1.Columns("RecPurFAT").FormatString = "{0:n2}"

            gv1.Columns("RecPurFATPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("RecPurFATPER").Width = 100
            gv1.Columns("RecPurFATPER").HeaderText = "Received Purchase Fat %"
            gv1.Columns("RecPurFATPER").FormatString = "{0:n2}"

            gv1.Columns("RecPurSNF").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("RecPurSNF").Width = 100
            gv1.Columns("RecPurSNF").HeaderText = "Received Purchase SNF"
            gv1.Columns("RecPurSNF").FormatString = "{0:n2}"

            gv1.Columns("RecPurSNFPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("RecPurSNFPER").Width = 100
            gv1.Columns("RecPurSNFPER").HeaderText = "Received Purchase SNF%"
            gv1.Columns("RecPurSNFPER").FormatString = "{0:n2}"

            gv1.Columns("RecPurCost").IsVisible = True
            gv1.Columns("RecPurCost").Width = 100
            gv1.Columns("RecPurCost").HeaderText = "Received Purchase Amount"
            gv1.Columns("RecPurCost").FormatString = "{0:n2}"


            gv1.Columns("RecAdjProQty").IsVisible = True
            gv1.Columns("RecAdjProQty").Width = 100
            gv1.Columns("RecAdjProQty").HeaderText = "Received Adjustment/Production Quantity"
            gv1.Columns("RecAdjProQty").FormatString = "{0:n2}"

            gv1.Columns("RecAdjProRate").IsVisible = True
            gv1.Columns("RecAdjProRate").Width = 100
            gv1.Columns("RecAdjProRate").HeaderText = "Received Adjustment/Production Rate"
            gv1.Columns("RecAdjProRate").FormatString = "{0:n3}"

            gv1.Columns("RecAdjProFAT").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("RecAdjProFAT").Width = 100
            gv1.Columns("RecAdjProFAT").HeaderText = "Received Adjustment/Production FAT"
            gv1.Columns("RecAdjProFAT").FormatString = "{0:n2}"

            gv1.Columns("RecAdjProFATPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("RecAdjProFATPER").Width = 100
            gv1.Columns("RecAdjProFATPER").HeaderText = "Received Adjustment/Production Fat %"
            gv1.Columns("RecAdjProFATPER").FormatString = "{0:n2}"

            gv1.Columns("RecAdjProSNF").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("RecAdjProSNF").Width = 100
            gv1.Columns("RecAdjProSNF").HeaderText = "Received Adjustment/Production SNF"
            gv1.Columns("RecAdjProSNF").FormatString = "{0:n2}"

            gv1.Columns("RecAdjProSNFPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("RecAdjProSNFPER").Width = 100
            gv1.Columns("RecAdjProSNFPER").HeaderText = "Received Adjustment/Production SNF%"
            gv1.Columns("RecAdjProSNFPER").FormatString = "{0:n2}"

            gv1.Columns("RecAdjProCost").IsVisible = True
            gv1.Columns("RecAdjProCost").Width = 100
            gv1.Columns("RecAdjProCost").HeaderText = "Received Adjustment/Production Amount"
            gv1.Columns("RecAdjProCost").FormatString = "{0:n2}"


            gv1.Columns("RecOthQty").IsVisible = True
            gv1.Columns("RecOthQty").Width = 100
            gv1.Columns("RecOthQty").HeaderText = "Received Other Quantity"
            gv1.Columns("RecOthQty").FormatString = "{0:n2}"

            gv1.Columns("RecOthRate").IsVisible = True
            gv1.Columns("RecOthRate").Width = 100
            gv1.Columns("RecOthRate").HeaderText = "Received Other Rate"
            gv1.Columns("RecOthRate").FormatString = "{0:n3}"

            gv1.Columns("RecOthFAT").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("RecOthFAT").Width = 100
            gv1.Columns("RecOthFAT").HeaderText = "Received Other FAT"
            gv1.Columns("RecOthFAT").FormatString = "{0:n2}"

            gv1.Columns("RecOthFATPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("RecOthFATPER").Width = 100
            gv1.Columns("RecOthFATPER").HeaderText = "Received Other Fat %"
            gv1.Columns("RecOthFATPER").FormatString = "{0:n2}"

            gv1.Columns("RecOthSNF").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("RecOthSNF").Width = 100
            gv1.Columns("RecOthSNF").HeaderText = "Received Other SNF"
            gv1.Columns("RecOthSNF").FormatString = "{0:n2}"

            gv1.Columns("RecOthSNFPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("RecOthSNFPER").Width = 100
            gv1.Columns("RecOthSNFPER").HeaderText = "Received Other SNF%"
            gv1.Columns("RecOthSNFPER").FormatString = "{0:n2}"

            gv1.Columns("RecOthCost").IsVisible = True
            gv1.Columns("RecOthCost").Width = 100
            gv1.Columns("RecOthCost").HeaderText = "Received Other Amount"
            gv1.Columns("RecOthCost").FormatString = "{0:n2}"

            gv1.Columns("RecQty").HeaderText = "Received Quantity"
            gv1.Columns("RecQty").FormatString = "{0:n2}"

            gv1.Columns("RecRate").HeaderText = "Received Rate"
            gv1.Columns("RecRate").FormatString = "{0:n3}"

            gv1.Columns("RecFAT").HeaderText = "Received FAT"
            gv1.Columns("RecFAT").FormatString = "{0:n2}"

            gv1.Columns("RecFATPER").HeaderText = "Received Fat %"
            gv1.Columns("RecFATPER").FormatString = "{0:n2}"

            gv1.Columns("RecSNF").HeaderText = "Received SNF"
            gv1.Columns("RecSNF").FormatString = "{0:n2}"

            gv1.Columns("RecSNFPER").HeaderText = "Received SNF%"
            gv1.Columns("RecSNFPER").FormatString = "{0:n2}"

            gv1.Columns("RecCost").HeaderText = "Received Amount"
            gv1.Columns("RecCost").FormatString = "{0:n2}"


            gv1.Columns("IssQty").IsVisible = True
            gv1.Columns("IssQty").Width = 100
            gv1.Columns("IssQty").HeaderText = "Issued Quantity"
            gv1.Columns("IssQty").FormatString = "{0:n2}"

            gv1.Columns("IssRate").IsVisible = True
            gv1.Columns("IssRate").Width = 100
            gv1.Columns("IssRate").HeaderText = "Issued Rate"
            gv1.Columns("IssRate").FormatString = "{0:n3}"

            gv1.Columns("IssFAT").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("IssFAT").Width = 100
            gv1.Columns("IssFAT").HeaderText = "Issued FAT"
            gv1.Columns("IssFAT").FormatString = "{0:n2}"

            gv1.Columns("IssFATPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("IssFATPER").Width = 100
            gv1.Columns("IssFATPER").HeaderText = "Issued Fat %"
            gv1.Columns("IssFATPER").FormatString = "{0:n2}"

            gv1.Columns("IssSNF").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("IssSNF").Width = 100
            gv1.Columns("IssSNF").HeaderText = "Issued SNF"
            gv1.Columns("IssSNF").FormatString = "{0:n2}"

            gv1.Columns("IssSNFPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("IssSNFPER").Width = 100
            gv1.Columns("IssSNFPER").HeaderText = "Issued SNF%"
            gv1.Columns("IssSNFPER").FormatString = "{0:n2}"

            gv1.Columns("IssCost").IsVisible = True
            gv1.Columns("IssCost").Width = 100
            gv1.Columns("IssCost").HeaderText = "Issued Amount"
            gv1.Columns("IssCost").FormatString = "{0:n2}"


            gv1.Columns("IssSaleQty").IsVisible = True
            gv1.Columns("IssSaleQty").Width = 100
            gv1.Columns("IssSaleQty").HeaderText = "Issued Sale Quantity"
            gv1.Columns("IssSaleQty").FormatString = "{0:n2}"

            gv1.Columns("IssSaleRate").IsVisible = True
            gv1.Columns("IssSaleRate").Width = 100
            gv1.Columns("IssSaleRate").HeaderText = "Issued Sale Rate"
            gv1.Columns("IssSaleRate").FormatString = "{0:n3}"

            gv1.Columns("IssSaleFAT").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("IssSaleFAT").Width = 100
            gv1.Columns("IssSaleFAT").HeaderText = "Issued Sale FAT"
            gv1.Columns("IssSaleFAT").FormatString = "{0:n2}"

            gv1.Columns("IssSaleFATPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("IssSaleFATPER").Width = 100
            gv1.Columns("IssSaleFATPER").HeaderText = "Issued Sale Fat %"
            gv1.Columns("IssSaleFATPER").FormatString = "{0:n2}"

            gv1.Columns("IssSaleSNF").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("IssSaleSNF").Width = 100
            gv1.Columns("IssSaleSNF").HeaderText = "Issued Sale SNF"
            gv1.Columns("IssSaleSNF").FormatString = "{0:n2}"

            gv1.Columns("IssSaleSNFPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("IssSaleSNFPER").Width = 100
            gv1.Columns("IssSaleSNFPER").HeaderText = "Issued Sale SNF%"
            gv1.Columns("IssSaleSNFPER").FormatString = "{0:n2}"

            gv1.Columns("IssSaleCost").IsVisible = True
            gv1.Columns("IssSaleCost").Width = 100
            gv1.Columns("IssSaleCost").HeaderText = "Issued Sale Amount"
            gv1.Columns("IssSaleCost").FormatString = "{0:n2}"


            gv1.Columns("IssIssAdjQty").IsVisible = True
            gv1.Columns("IssIssAdjQty").Width = 100
            gv1.Columns("IssIssAdjQty").HeaderText = "Issued/Adjustment Quantity"
            gv1.Columns("IssIssAdjQty").FormatString = "{0:n2}"

            gv1.Columns("IssIssAdjRate").IsVisible = True
            gv1.Columns("IssIssAdjRate").Width = 100
            gv1.Columns("IssIssAdjRate").HeaderText = "Issued/Adjustment Rate"
            gv1.Columns("IssIssAdjRate").FormatString = "{0:n3}"

            gv1.Columns("IssIssAdjFAT").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("IssIssAdjFAT").Width = 100
            gv1.Columns("IssIssAdjFAT").HeaderText = "Issued/Adjustment FAT"
            gv1.Columns("IssIssAdjFAT").FormatString = "{0:n2}"

            gv1.Columns("IssIssAdjFATPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("IssIssAdjFATPER").Width = 100
            gv1.Columns("IssIssAdjFATPER").HeaderText = "Issued/Adjustment Fat %"
            gv1.Columns("IssIssAdjFATPER").FormatString = "{0:n2}"

            gv1.Columns("IssIssAdjSNF").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("IssIssAdjSNF").Width = 100
            gv1.Columns("IssIssAdjSNF").HeaderText = "Issued/Adjustment SNF"
            gv1.Columns("IssIssAdjSNF").FormatString = "{0:n2}"

            gv1.Columns("IssIssAdjSNFPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("IssIssAdjSNFPER").Width = 100
            gv1.Columns("IssIssAdjSNFPER").HeaderText = "Issued/Adjustment SNF%"
            gv1.Columns("IssIssAdjSNFPER").FormatString = "{0:n2}"

            gv1.Columns("IssIssAdjCost").IsVisible = True
            gv1.Columns("IssIssAdjCost").Width = 100
            gv1.Columns("IssIssAdjCost").HeaderText = "Issued/Adjustment Amount"
            gv1.Columns("IssIssAdjCost").FormatString = "{0:n2}"

            gv1.Columns("IssOthQty").IsVisible = True
            gv1.Columns("IssOthQty").Width = 100
            gv1.Columns("IssOthQty").HeaderText = "Issued Other Quantity"
            gv1.Columns("IssOthQty").FormatString = "{0:n2}"

            gv1.Columns("IssOthRate").IsVisible = True
            gv1.Columns("IssOthRate").Width = 100
            gv1.Columns("IssOthRate").HeaderText = "Issued Other Rate"
            gv1.Columns("IssOthRate").FormatString = "{0:n3}"

            gv1.Columns("IssOthFAT").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("IssOthFAT").Width = 100
            gv1.Columns("IssOthFAT").HeaderText = "Issued Other FAT"
            gv1.Columns("IssOthFAT").FormatString = "{0:n2}"

            gv1.Columns("IssOthFATPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("IssOthFATPER").Width = 100
            gv1.Columns("IssOthFATPER").HeaderText = "Issued Other Fat %"
            gv1.Columns("IssOthFATPER").FormatString = "{0:n2}"

            gv1.Columns("IssOthSNF").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("IssOthSNF").Width = 100
            gv1.Columns("IssOthSNF").HeaderText = "Issued Other SNF"
            gv1.Columns("IssOthSNF").FormatString = "{0:n2}"

            gv1.Columns("IssOthSNFPER").IsVisible = chkFATAndSNF.Checked
            gv1.Columns("IssOthSNFPER").Width = 100
            gv1.Columns("IssOthSNFPER").HeaderText = "Issued Other SNF%"
            gv1.Columns("IssOthSNFPER").FormatString = "{0:n2}"

            gv1.Columns("IssOthCost").IsVisible = True
            gv1.Columns("IssOthCost").Width = 100
            gv1.Columns("IssOthCost").HeaderText = "Issued Other Amount"
            gv1.Columns("IssOthCost").FormatString = "{0:n2}"

            gv1.Columns("CLQty").IsVisible = True
            gv1.Columns("CLQty").Width = 100
            gv1.Columns("CLQty").HeaderText = "Balance Quantity"
            gv1.Columns("CLQty").FormatString = "{0:n2}"

            gv1.Columns("CLRate").IsVisible = True
            gv1.Columns("CLRate").Width = 100
            gv1.Columns("CLRate").HeaderText = "Balance Rate"
            gv1.Columns("CLRate").FormatString = "{0:n3}"

            gv1.Columns("CLFAT").IsVisible = True
            gv1.Columns("CLFAT").Width = 100
            gv1.Columns("CLFAT").HeaderText = "Balance FAT"
            gv1.Columns("CLFAT").FormatString = "{0:n2}"

            gv1.Columns("CLFATPER").IsVisible = True
            gv1.Columns("CLFATPER").Width = 100
            gv1.Columns("CLFATPER").HeaderText = "Balance Fat %"
            gv1.Columns("CLFATPER").FormatString = "{0:n2}"

            gv1.Columns("CLSNF").IsVisible = True
            gv1.Columns("CLSNF").Width = 100
            gv1.Columns("CLSNF").HeaderText = "Balance SNF"
            gv1.Columns("CLSNF").FormatString = "{0:n2}"

            gv1.Columns("CLSNFPER").IsVisible = True
            gv1.Columns("CLSNFPER").Width = 100
            gv1.Columns("CLSNFPER").HeaderText = "Balance SNF%"
            gv1.Columns("CLSNFPER").FormatString = "{0:n2}"

            gv1.Columns("CLCost").IsVisible = True
            gv1.Columns("CLCost").Width = 100
            gv1.Columns("CLCost").HeaderText = "Balance Amount"
            gv1.Columns("CLCost").FormatString = "{0:n2}"

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item2 As New GridViewSummaryItem("RecQty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("RecCost", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("IssQty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("IssCost", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("CLQty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("CLCost", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)


            item2 = New GridViewSummaryItem("OPFAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("OPSNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("RecFAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("RecSNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("IssFAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("IssSNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("CLFAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("CLSNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)


            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Document Wise Detail Ledger") = CompairStringResult.Equal Then
            gv1.Columns("Source_Doc_No").IsVisible = True
            gv1.Columns("Source_Doc_No").Width = 100
            gv1.Columns("Source_Doc_No").HeaderText = "Source Doc No"

            gv1.Columns("Punching_Date").IsVisible = True
            gv1.Columns("Punching_Date").Width = 100
            gv1.Columns("Punching_Date").HeaderText = "Document Date"

            gv1.Columns("Item_Type_Name").IsVisible = True
            gv1.Columns("Item_Type_Name").Width = 100
            gv1.Columns("Item_Type_Name").HeaderText = "Item Type"

            gv1.Columns("InOutView").IsVisible = True
            gv1.Columns("InOutView").Width = 100
            gv1.Columns("InOutView").HeaderText = "Type"

            gv1.Columns("Item_Code").IsVisible = True
            gv1.Columns("Item_Code").Width = 100
            gv1.Columns("Item_Code").HeaderText = "Item Code"

            gv1.Columns("Item_Desc").IsVisible = True
            gv1.Columns("Item_Desc").Width = 150
            gv1.Columns("Item_Desc").HeaderText = "Item"

            gv1.Columns("Stock_UOM").IsVisible = True
            gv1.Columns("Stock_UOM").Width = 100
            gv1.Columns("Stock_UOM").HeaderText = "UOM"

            gv1.Columns("RecQty").IsVisible = True
            gv1.Columns("RecQty").Width = 100
            gv1.Columns("RecQty").HeaderText = "Received Quantity"
            gv1.Columns("RecQty").FormatString = "{0:n2}"

            gv1.Columns("RecRate").IsVisible = True
            gv1.Columns("RecRate").Width = 100
            gv1.Columns("RecRate").HeaderText = "Received Rate"
            gv1.Columns("RecRate").FormatString = "{0:n3}"

            gv1.Columns("RecCost").IsVisible = True
            gv1.Columns("RecCost").Width = 100
            gv1.Columns("RecCost").HeaderText = "Received Amount"
            gv1.Columns("RecCost").FormatString = "{0:n2}"

            gv1.Columns("IssQty").IsVisible = True
            gv1.Columns("IssQty").Width = 100
            gv1.Columns("IssQty").HeaderText = "Issued Quantity"
            gv1.Columns("IssQty").FormatString = "{0:n2}"

            gv1.Columns("IssRate").IsVisible = True
            gv1.Columns("IssRate").Width = 100
            gv1.Columns("IssRate").HeaderText = "Issued Rate"
            gv1.Columns("IssRate").FormatString = "{0:n3}"

            gv1.Columns("IssCost").IsVisible = True
            gv1.Columns("IssCost").Width = 100
            gv1.Columns("IssCost").HeaderText = "Issued Amount"
            gv1.Columns("IssCost").FormatString = "{0:n2}"


            gv1.Columns("CLQty").IsVisible = True
            gv1.Columns("CLQty").Width = 100
            gv1.Columns("CLQty").HeaderText = "Balance Quantity"
            gv1.Columns("CLQty").FormatString = "{0:n2}"

            gv1.Columns("CLRate").IsVisible = True
            gv1.Columns("CLRate").Width = 100
            gv1.Columns("CLRate").HeaderText = "Balance Rate"
            gv1.Columns("CLRate").FormatString = "{0:n3}"

            gv1.Columns("CLCost").IsVisible = True
            gv1.Columns("CLCost").Width = 100
            gv1.Columns("CLCost").HeaderText = "Balance Amount"
            gv1.Columns("CLCost").FormatString = "{0:n2}"

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item2 As New GridViewSummaryItem("RecQty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("RecCost", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("IssQty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("IssCost", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            ''richa agarwal 05-feb,2016 changes from sum to last because report showing wrong balance
            item2 = New GridViewSummaryItem("CLQty", "{0:F2}", GridAggregateFunction.Last)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("CLCost", "{0:F2}", GridAggregateFunction.Last)
            ''------------------------------------
            summaryRowItem.Add(item2)
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
            Dim descriptor3 As New GroupDescriptor()
            descriptor3.GroupNames.Add("Item_Code", System.ComponentModel.ListSortDirection.Ascending)
            gv1.GroupDescriptors.Add(descriptor3)
            gv1.MasterTemplate.AutoExpandGroups = True
        End If
        gv1.BestFitColumns()
        ReStoreGridLayout()
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            Dim ReportID As String = GetReportID()
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
            common.clsCommon.MyMessageBoxShow(Me, err.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.ViewCellFormatting
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub

    Sub print(ByVal exporter As EnumExportTo)
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
                arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.stockRecoBatch & "'"))
                If txtItem.arrDispalyMember IsNot Nothing AndAlso txtItem.arrDispalyMember.Count > 0 Then
                    arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember))
                End If
                If clsCommon.myLen(cmbUnit.SelectedValue) > 0 Then
                    arrHeader.Add("Unit : " + cmbUnit.SelectedValue)
                End If
                If rbtnLocationSelect.IsChecked Then
                    Dim strLoca As String = ""
                    For Each grow As GridViewRowInfo In gvLocation.Rows
                        If clsCommon.myCBool(grow.Cells("SEL").Value) = True Then
                            strLoca += "," + clsCommon.myCstr(grow.Cells("NAME").Value)
                        End If
                    Next
                    arrHeader.Add("Location : " + strLoca)
                End If
                PageSetupReport_ID = GetReportID()
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
                    clsCommon.MyExportToPDF("Stock Reco (" + cboType.Text + ")", gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub RadButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton3.Click
        EnableDisableCtrl(True)
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        gv1.GroupDescriptors.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        'txtToDate.Value = clsCommon.GETSERVERDATE()
        'txtFromDate.Value = txtToDate.Value
        'ChkMRPWise.Checked = False
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        arrBack = New List(Of String)
        RadPageViewPage2.Text = "Report"

    End Sub

    Sub EnableDisableCtrl(ByVal val As Boolean)
        txtFromDate.Enabled = val
        txtToDate.Enabled = val
        RadGroupBox3.Enabled = val
        txtItem.Enabled = val
        RadGroupBox2.Enabled = val
        cmbUnit.Enabled = val
        cboType.Enabled = val
        chkFATAndSNF.Enabled = val
        txtTransaction.Enabled = val
        cboInOutType.Enabled = val
        txtItemGroup.Enabled = val
        chkIncludeGIT.Enabled = val
        txtItemType.Enabled = val
    End Sub

    Private Sub rbtnLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnLocationAll.ToggleStateChanged, rbtnLocationSelect.ToggleStateChanged
        gvLocation.Enabled = rbtnLocationSelect.IsChecked
        RadButton4.Enabled = rbtnLocationSelect.IsChecked
        RadButton5.Enabled = rbtnLocationSelect.IsChecked
    End Sub

    Private Sub rbtnCustomerAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCategoryAll.ToggleStateChanged, rbtnCategorySelect.ToggleStateChanged
        gvCategory.Enabled = rbtnCategorySelect.IsChecked
        RadButton6.Enabled = rbtnCategorySelect.IsChecked
        RadButton7.Enabled = rbtnCategorySelect.IsChecked
    End Sub

    Private Function GetReportID() As String
        Dim ReportID As String = ""
        If cboType.Enabled = False Then
            If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item Type Wise Summary") = CompairStringResult.Equal Then
                ReportID = "stReNityBatch" + IIf(chkFATAndSNF.Checked, "F", "")
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Category Wise Summary") = CompairStringResult.Equal Then
                ReportID = "stReNCatgBatch" + IIf(chkFATAndSNF.Checked, "F", "")
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item And Batch Wise Summary") = CompairStringResult.Equal Then
                ReportID = "stReNItemBatch" + IIf(chkFATAndSNF.Checked, "F", "")
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item Batch And Location Wise Summary") = CompairStringResult.Equal Then
                ReportID = "stReNLOCItemBatch" + IIf(chkFATAndSNF.Checked, "F", "")
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Document Wise Detail") = CompairStringResult.Equal Then
                ReportID = "stReNDocBatch" + IIf(chkFATAndSNF.Checked, "F", "")

            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item Group Wise Summary") = CompairStringResult.Equal Then
                ReportID = "stReIGWSBatch" + IIf(chkFATAndSNF.Checked, "F", "")
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Date, Item And Document Wise Detail") = CompairStringResult.Equal Then
                ReportID = "stReNDIDWBatch" + IIf(chkFATAndSNF.Checked, "F", "")
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Date and Item Wise Stock") = CompairStringResult.Equal Then
                ReportID = "stReNDIWSBatch" + IIf(chkFATAndSNF.Checked, "F", "")
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Document Wise Detail Ledger") = CompairStringResult.Equal Then
                ReportID = "stReNDWDLBatch" + IIf(chkFATAndSNF.Checked, "F", "")
            End If
        End If
        Return ReportID
    End Function

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        '=========update by preeti gupta Against ticket no[BHA/16/07/18-000166]
        clsGridLayout.DeleteData("stReNCatg" + IIf(chkFATAndSNF.Checked, "F", ""), objCommonVar.CurrentUserCode)
        clsGridLayout.DeleteData("stReNItem" + IIf(chkFATAndSNF.Checked, "F", ""), objCommonVar.CurrentUserCode)
        clsGridLayout.DeleteData("stReNLOCItem" + IIf(chkFATAndSNF.Checked, "F", ""), objCommonVar.CurrentUserCode)
        clsGridLayout.DeleteData("stReNDoc" + IIf(chkFATAndSNF.Checked, "F", ""), objCommonVar.CurrentUserCode)
    End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemSett1.Click
        Dim ReportID As String = GetReportID()
        If clsCommon.myLen(ReportID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
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

    Private Sub gvCategory_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gvCategory.CellDoubleClick
        If clsCommon.myCBool(gvCategory.CurrentRow.Cells("SEL").Value) Then
            Dim frm As New FrmCategorySelect()
            frm.lvl = 2
            frm.strCode = clsCommon.myCstr(gvCategory.CurrentRow.Cells("CODE").Value)
            frm.arrIn = gvCategory.CurrentRow.Tag
            frm.ShowDialog()
            If Not frm.isCancel Then
                gvCategory.CurrentRow.Tag = frm.arrOut
            End If
        End If
    End Sub

    Private Sub gvLocation_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gvLocation.CellDoubleClick
        If clsCommon.myCBool(gvLocation.CurrentRow.Cells("SEL").Value) Then
            Dim frm As New FrmCategorySelect()
            frm.lvl = 3
            frm.strCode = clsCommon.myCstr(gvLocation.CurrentRow.Cells("CODE").Value)
            frm.arrIn = gvLocation.CurrentRow.Tag
            frm.ShowDialog()
            If Not frm.isCancel Then
                gvLocation.CurrentRow.Tag = frm.arrOut
            End If
        End If
    End Sub

    Private Sub TxtMultiSelectFinder1__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Dim qry As String
        If txtItemType.arrValueMember Is Nothing OrElse clsCommon.GetMulcallString(txtItemType.arrValueMember) = "All" Then
            qry = " select Item_Code,Item_Desc from TSPL_ITEM_MASTER where Is_Batch_Item=1  order by Item_Code "
        Else
            qry = " select Item_Code,Item_Desc from TSPL_ITEM_MASTER where Is_Batch_Item=1 and Item_Type in (" + clsCommon.GetMulcallString(txtItemType.arrValueMember) + ") order by Item_Code "

        End If

        txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Item_Code", "Item_Desc", txtItem.arrValueMember, txtItem.arrDispalyMember)

    End Sub

    Private Sub txtTransaction__My_Click(sender As Object, e As EventArgs) Handles txtTransaction._My_Click
        Dim qry As String = " select Code,Name,InOutType as [In/Out Type],Type from TSPL_INVENTORY_SOURCE_CODE where 2=2 "
        If Not clsCommon.CompairString(clsCommon.myCstr(cboInOutType.SelectedValue), "All") = CompairStringResult.Equal Then
            qry += " and InOutType='" + clsCommon.myCstr(cboInOutType.SelectedValue) + "'"
        End If
        txtTransaction.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSe", qry, "Code", "Name", txtTransaction.arrValueMember, txtTransaction.arrDispalyMember)
    End Sub

    Function GetMIS_ITem_GroupColumn() As String
        Dim qry As String = ""
        qry = " select MAP.Custom_Field_Code from TSPL_CUSTOM_FIELD_MAPPING MAP " & _
            " left join TSPL_CUSTOM_FIELD_HEAD CF on MAP.Custom_Field_Code=CF.Code " & _
            " where CF.Name='MIS Item Group' and MAP.PROGRAM_CODE='" & clsUserMgtCode.itemStructure & "'"
        MIS_Item_Group = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        Return MIS_Item_Group
    End Function

    Private Sub txtItemGroup__My_Click(sender As Object, e As EventArgs) Handles txtItemGroup._My_Click
        Dim qry As String = " select SNo,Value,Description as Name,Custom_Field_Code as [Code] from TSPL_CUSTOM_FIELD_DETAIL where Custom_Field_Code='" & MIS_Item_Group & "' "
        txtItemGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemGroupMulSel", qry, "Value", "Name", txtItemGroup.arrValueMember, txtItemGroup.arrDispalyMember)
    End Sub

    Private Sub RadButton4_Click(sender As Object, e As EventArgs) Handles RadButton4.Click
        CheckedAll(gvLocation)
    End Sub

    Private Sub CheckedAll(ByVal gv As RadGridView)
        For ii As Integer = 0 To gv.RowCount - 1
            gv.Rows(ii).Cells("SEL").Value = False
        Next
        For ii As Integer = 0 To gv.ChildRows.Count - 1
            gv.ChildRows(ii).Cells("SEL").Value = True
        Next
    End Sub

    Private Sub UnCheckedAll(ByVal gv As RadGridView)
        For ii As Integer = 0 To gv.RowCount - 1
            gv.Rows(ii).Cells("SEL").Value = False
        Next
    End Sub

    Private Sub RadButton5_Click(sender As Object, e As EventArgs) Handles RadButton5.Click
        UnCheckedAll(gvLocation)
    End Sub

    Private Sub RadButton7_Click(sender As Object, e As EventArgs) Handles RadButton7.Click
        CheckedAll(gvCategory)
    End Sub

    Private Sub RadButton6_Click(sender As Object, e As EventArgs) Handles RadButton6.Click
        UnCheckedAll(gvCategory)
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item Type Wise Summary") = CompairStringResult.Equal Then

            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item Group Wise Summary") = CompairStringResult.Equal Then
                arrBack.Remove("Item Type Wise Summary")
                cboType.SelectedValue = "Item Type Wise Summary"
                txtItemType.arrValueMember = arrItemType
                LoadData(0)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Category Wise Summary") = CompairStringResult.Equal AndAlso arrBack.Contains("Item Group Wise Summary") Then
                arrBack.Remove("Item Group Wise Summary")
                cboType.SelectedValue = "Item Group Wise Summary"
                txtItemGroup.arrValueMember = arrItemGroup
                LoadData(0)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item And Batch Wise Summary") = CompairStringResult.Equal AndAlso arrBack.Contains("Category Wise Summary") Then
                arrBack.Remove("Category Wise Summary")
                cboType.SelectedValue = "Category Wise Summary"
                UnCheckedAll(gvCategory)
                If arrCat IsNot Nothing AndAlso arrCat.Count > 0 Then
                    rbtnCategorySelect.IsChecked = True
                    For Each str As String In arrCat.Keys
                        For ii As Integer = 0 To gvCategory.RowCount - 1
                            If clsCommon.CompairString(clsCommon.myCstr(gvCategory.Rows(ii).Cells("CODE").Value), str) = CompairStringResult.Equal Then
                                gvCategory.Rows(ii).Cells("SEL").Value = True
                                gvCategory.Rows(ii).Tag = arrCat(str)
                            End If
                        Next
                    Next
                Else
                    rbtnCategoryAll.IsChecked = True
                End If
                LoadData(0)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item Batch And Location Wise Summary") = CompairStringResult.Equal AndAlso arrBack.Contains("Item And Batch Wise Summary") Then
                arrBack.Remove("Item And Batch Wise Summary")
                cboType.SelectedValue = "Item And Batch Wise Summary"
                txtItem.arrValueMember = arrItem
                LoadData(0)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Document Wise Detail") = CompairStringResult.Equal AndAlso arrBack.Contains("Item Batch And Location Wise Summary") Then
                arrBack.Remove("Item Batch And Location Wise Summary")
                cboType.SelectedValue = "Item Batch And Location Wise Summary"
                UnCheckedAll(gvLocation)
                If arrLoc IsNot Nothing AndAlso arrLoc.Count > 0 Then
                    rbtnLocationSelect.IsChecked = True
                    For Each str As String In arrLoc.Keys
                        For ii As Integer = 0 To gvLocation.RowCount - 1
                            If clsCommon.CompairString(clsCommon.myCstr(gvLocation.Rows(ii).Cells("CODE").Value), str) = CompairStringResult.Equal Then
                                gvLocation.Rows(ii).Cells("SEL").Value = True
                                gvLocation.Rows(ii).Tag = arrLoc(str)
                            End If
                        Next
                    Next
                Else
                    rbtnLocationAll.IsChecked = True
                End If
                LoadData(0)
            End If
            PageSetupReport_ID = GetReportID()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Try
            If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item Type Wise Summary") = CompairStringResult.Equal Then
                If Not arrBack.Contains("Item Type Wise Summary") Then
                    arrBack.Add("Item Type Wise Summary")
                End If
                cboType.SelectedValue = "Item Group Wise Summary"
                arrItemType = New ArrayList()
                arrItemType = txtItemType.arrValueMember()

                Dim tmp As New ArrayList()
                tmp.Add(clsCommon.myCstr(gv1.CurrentRow.Cells("Item_Type").Value))
                txtItemType.arrValueMember = tmp
                LoadData(0)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item Group Wise Summary") = CompairStringResult.Equal Then
                If Not arrBack.Contains("Item Group Wise Summary") Then
                    arrBack.Add("Item Group Wise Summary")
                End If
                cboType.SelectedValue = "Category Wise Summary"
                arrItemGroup = New ArrayList()
                arrItemGroup = txtItemGroup.arrValueMember
                Dim tmp As New ArrayList()
                tmp.Add(clsCommon.myCstr(gv1.CurrentRow.Cells("Item_Group").Value))
                txtItemGroup.arrValueMember = tmp
                LoadData(0)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Category Wise Summary") = CompairStringResult.Equal Then
                If Not arrBack.Contains("Category Wise Summary") Then
                    arrBack.Add("Category Wise Summary")
                End If
                cboType.SelectedValue = "Item Wise Summary"
                arrCat = Nothing
                If rbtnCategorySelect.IsChecked Then
                    arrCat = New Dictionary(Of String, Object)
                    For ii As Integer = 0 To gvCategory.RowCount - 1
                        If clsCommon.myCBool(gvCategory.Rows(ii).Cells("SEL").Value) Then
                            arrCat.Add(clsCommon.myCstr(gvCategory.Rows(ii).Cells("CODE").Value), gvCategory.Rows(ii).Tag)
                        End If
                    Next
                End If
                UnCheckedAll(gvCategory)
                Dim arrCatTemp As New Dictionary(Of String, Object)
                For Each dr As DataRow In dtCategory.Rows
                    If clsCommon.myLen(gv1.CurrentRow.Cells(clsCommon.myCstr(dr("CodeColumn"))).Value) > 0 Then
                        Dim arrIn As New Dictionary(Of String, Object)
                        arrIn.Add(gv1.CurrentRow.Cells(clsCommon.myCstr(dr("CodeColumn"))).Value, Nothing)
                        arrCatTemp.Add(clsCommon.myCstr(dr("CodeColumn")), arrIn)
                    End If
                Next
                If arrCatTemp IsNot Nothing AndAlso arrCatTemp.Count > 0 Then
                    rbtnCategorySelect.IsChecked = True
                    For Each str As String In arrCatTemp.Keys
                        For ii As Integer = 0 To gvCategory.RowCount - 1
                            If clsCommon.CompairString(clsCommon.myCstr(gvCategory.Rows(ii).Cells("CODE").Value), str) = CompairStringResult.Equal Then
                                gvCategory.Rows(ii).Cells("SEL").Value = True
                                gvCategory.Rows(ii).Tag = arrCatTemp(str)
                            End If
                        Next
                    Next
                End If
                LoadData(0)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item Wise Summary") = CompairStringResult.Equal Then
                If Not arrBack.Contains("Item Wise Summary") Then
                    arrBack.Add("Item Wise Summary")
                End If
                cboType.SelectedValue = "Item And Location Wise Summary"
                arrItem = New ArrayList()
                arrItem = txtItem.arrValueMember
                Dim tmp As New ArrayList()
                tmp.Add(clsCommon.myCstr(gv1.CurrentRow.Cells("Item_Code").Value))
                txtItem.arrValueMember = tmp
                LoadData(0)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item And Location Wise Summary") = CompairStringResult.Equal Then
                If Not arrBack.Contains("Item And Location Wise Summary") Then
                    arrBack.Add("Item And Location Wise Summary")
                End If
                cboType.SelectedValue = "Document Wise Detail"
                arrLoc = Nothing
                If rbtnLocationSelect.IsChecked Then
                    arrLoc = New Dictionary(Of String, Object)
                    For ii As Integer = 0 To gvLocation.RowCount - 1
                        If clsCommon.myCBool(gvLocation.Rows(ii).Cells("SEL").Value) Then
                            arrLoc.Add(clsCommon.myCstr(gvLocation.Rows(ii).Cells("CODE").Value), gvLocation.Rows(ii).Tag)
                        End If
                    Next
                End If
                UnCheckedAll(gvLocation)
                rbtnLocationSelect.IsChecked = True
                Dim arrInn As New Dictionary(Of String, Object)
                arrInn.Add(clsCommon.myCstr(gv1.CurrentRow.Cells("Location_Code").Value), Nothing)
                For ii As Integer = 0 To gvLocation.RowCount - 1
                    If clsCommon.CompairString(clsCommon.myCstr(gvLocation.Rows(ii).Cells("CODE").Value), clsCommon.myCstr(gv1.CurrentRow.Cells("Main_Location_Code").Value)) = CompairStringResult.Equal Then
                        gvLocation.Rows(ii).Cells("SEL").Value = True
                        gvLocation.Rows(ii).Tag = IIf(clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Location_Code").Value), clsCommon.myCstr(gv1.CurrentRow.Cells("Main_Location_Code").Value)) = CompairStringResult.Equal, Nothing, arrInn)
                    End If
                Next
                LoadData(0)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Document Wise Detail") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Document Wise Detail Ledger") = CompairStringResult.Equal Then
                Dim strTransType As String = clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value)
                Dim strTransCode As String = clsCommon.myCstr(gv1.CurrentRow.Cells("Source_Doc_No").Value)
                If clsCommon.myLen(strTransType) > 0 AndAlso clsCommon.myLen(strTransCode) > 0 Then
                    Select Case strTransType
                        Case "IC-AD"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnStoreAdjustment, strTransCode)
                        Case "ISSTRAN"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnIssueReturn, strTransCode)
                        Case "SRN"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnSRN, strTransCode)
                        Case "SD-IN"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmShipmentProductSale, strTransCode)
                        Case "Sale Return"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSNSaleReturn, strTransCode)
                        Case "SD-CSATRANS"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSATransfer, strTransCode)
                        Case "SD-SH"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmShipmentProductSale, strTransCode)
                        Case "CSA-SALE"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSASaleInvoice, strTransCode)
                        Case "RICE-MIX"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmRiceMixingEntry, strTransCode)
                        Case "RICE-PROC"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmRiceProcessingEntry, strTransCode)
                        Case "PP_ISSUE"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProcessProductionIssueEntry, strTransCode)
                        Case "PP_STDN"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProcessProductionStandardization, strTransCode)
                            'Case "BulkSRNTrade"
                            '    clsOpenTransactionForm.OpenTransacionForm(EnumTransType.BulkSRNTrade, strTransCode)
                        'Case "DispatchBSTrade"
                        '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmDispatchBulkSaleTrade, strTransCode)
                        Case "DispChallan"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCDispatch, strTransCode)
                        Case "MilkTransferIn"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkTransferIn, strTransCode)
                        Case "IC-AD"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnStoreAdjustment, strTransCode)
                        Case "DispatchBS"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmDispatchBulkSale, strTransCode)
                        Case "PRD_STG_PROC"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProcessProductionStageProcess, strTransCode)
                        Case "RGP"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnGatePass, strTransCode)
                        Case "NRGP"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnGatePass, strTransCode)
                        Case "PROD_ENTRY"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProductionEntry, strTransCode)
                        'Case "MCC-IISSUE"
                        '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmVSPItemIssue, strTransCode)
                        Case "FS-SH"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmDispatchMultipleFreshSale, strTransCode)
                        Case "PS-SH"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmShipmentProductSale, strTransCode)
                        Case "Transfer"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.Transfer, strTransCode)
                        Case "BulkSRN"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmBulkMilkSRN, strTransCode)
                        Case "MCC-MSRN"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkSRN, strTransCode)
                        Case "SD-CSATRANS-RETURN"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSATransferReturn, strTransCode)
                    End Select

                End If
            End If
            PageSetupReport_ID = GetReportID()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub chkIncludeGIT_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkIncludeGIT.ToggleStateChanged
        LoadLocation()
    End Sub

    Private Sub txtItemType__My_Click(sender As Object, e As EventArgs) Handles txtItemType._My_Click
        txtItemType.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemTypestoreco", FrmItemMasterRMOther.LoadItemTypeQuery(), "Code", "Name", txtItemType.arrValueMember, txtItemType.arrDispalyMember)
    End Sub


    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        '=====================Added by Preeti Gupta====
        Try
            LoadData(1)
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

    Private Sub cboType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles cboType.SelectedIndexChanged
        'SetBulkExport()
    End Sub

    Private Sub cboType_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboType.SelectedValueChanged
        'SetBulkExport()
    End Sub

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

    Private Sub BulkExportCSV_Click(sender As Object, e As EventArgs) Handles BulkExportCSV.Click
        LoadData(4)
    End Sub

    Private Sub BulkExportXls_Click(sender As Object, e As EventArgs) Handles BulkExportXls.Click
        LoadData(5)
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        print(EnumExportTo.PDF)
    End Sub
End Class
