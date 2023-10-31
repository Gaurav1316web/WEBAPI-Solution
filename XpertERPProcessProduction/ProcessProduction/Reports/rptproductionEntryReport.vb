Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
'=============Created By Preeti Gupta==============
'sanjay Ticket No-ERO/05/06/19-000633 add TS_Per,TS_KG
Public Class RptproductionEntryReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim ArrLocation As ArrayList
    Dim RequiredFinalQCofstandardization As Boolean = False
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptproductionEntryReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub
    Sub LoadLocation()
        'Dim qry As String = " select Location_Code  as [Code],Location_Desc as [Name] , case when TSPL_LOCATION_MASTER.Is_Section='y'then 'Section wise Tanker'  when TSPL_LOCATION_MASTER .Is_Sub_Location='Y' then 'Sub Location' else 'Main Location' end  as [Type] from TSPL_LOCATION_MASTER  "
        'cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        'cbgLocation.ValueMember = "Code"
        'cbgLocation.DisplayMember = "Name"
        ''cbgLocation.DisplayMember = "Type"


        ''''''''''''''''''''
        gvLocation.DataSource = Nothing
        Dim qry As String = " select cast( 0 as bit) as SEL,Location_Code as CODE,Location_Desc as NAME,case when Is_Jobwork=1 then 'Yes' else 'No' end as [Job Location] from TSPL_LOCATION_MASTER where 1=1 and ((Is_Section='N' and Is_Sub_Location='N' and Location_Type IN ('Physical','Logical','Virtual') ) or (CSA_Type='Y') ) "
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
    Sub LoadItem()
        Dim qry As String = "select Item_Code as [Code] ,Item_Desc as [Name]  from TSPL_ITEM_MASTER  where Item_Type ='S' or Item_Type= 'F'"
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem.ValueMember = "Code"
        cbgItem.DisplayMember = "Name"
    End Sub
    Sub LoadSection()
        Dim qry As String = "select Section_Code as [Code],Description as [Name] from TSPL_SECTION_MASTER "
        cbgSection.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgSection.ValueMember = "Code"
        cbgSection.DisplayMember = "Name"
    End Sub
    Sub LoadCategory()
        Dim qry As String = "select Code,Name,Parent from ("
        qry += " select ITEM_CATEGORY_STRUCT_CODE as Code,DESCRIPTION as Name, null as Parent,0 as Sno from TSPL_ITEM_CATEGORY_STRUCTURE"
        qry += " union all"
        qry += " select TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE as Code,TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION as Name,ITEM_CATEGORY_STRUCT_CODE as Parent,TSPL_ITEM_CATEGORY_STRUCT_DETAIL.CATEGORY_LEVEL as SNo from TSPL_ITEM_CATEGORY_STRUCT_DETAIL"
        qry += " left outer join TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE=TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE"
        qry += " Union all"
        qry += " select CODE,DESCRIPTION as Name,ITEM_CATEGORY_CODE as Parent,100 as SNo from TSPL_ITEM_CATEGORY_LEVEL_VALUES"
        qry += " )xxx order by Sno"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        tvCategory.DataSource = Nothing
        tvCategory.TreeViewElement.AutoSizeItems = True
        tvCategory.ShowLines = True
        tvCategory.ShowRootLines = True
        tvCategory.TreeViewElement.ViewElement.Margin = New Padding(4)
        tvCategory.ShowExpandCollapse = True
        tvCategory.TreeIndent = 15
        tvCategory.FullRowSelect = False
        tvCategory.ShowLines = True
        tvCategory.LineStyle = TreeLineStyle.Dot
        tvCategory.LineColor = Color.FromArgb(110, 153, 210)
        tvCategory.ExpandAnimation = ExpandAnimation.Opacity
        tvCategory.AllowEdit = False
        tvCategory.ShowRootLines = False
        tvCategory.TreeViewElement.AllowAlternatingRowColor = True
        tvCategory.TreeViewElement.AlternatingRowColor = Color.AliceBlue
        'TreeView.TreeViewElement.AngleTransform = 270
        'TreeView.TreeViewElement.RightToLeft = True
        tvCategory.TreeViewElement.DrawBorder = True
        tvCategory.ValueMember = "Code"
        tvCategory.DisplayMember = "Name"
        tvCategory.ChildMember = "Code"
        tvCategory.ParentMember = "Parent"
        tvCategory.DataSource = dt
        tvCategory.CheckBoxes = True


        'tvCategory.ExpandAll()
    End Sub
    Sub reset()

        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        gv1.GroupDescriptors.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtfromDate.Value = txtToDate.Value
        LoadCategory()
        LoadItem()
        LoadLocation()
        LoadSection()
        ChkItemAll.CheckState = CheckState.Checked
        ChkSectionAll.CheckState = CheckState.Checked
        chkAllLocation.CheckState = CheckState.Checked
        rbtnCategoryAll.CheckState = CheckState.Checked
        chk_stockingunit.Checked = False
        chk_stockingunit.Enabled = True
        chkPosted.Checked = True
    End Sub

    Private Sub LoadData()
        If ChkItemSelect.IsChecked AndAlso cbgItem.CheckedValue.Count <= 0 Then
            Throw New Exception("Please select atleast one Item")
        End If
        'If chkSelectLocation.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
        '    Throw New Exception("Please select atleast one Location")
        'End If
        If chkSectionSelect.IsChecked AndAlso cbgSection.CheckedValue.Count <= 0 Then
            Throw New Exception("Please select atleast one Location")
        End If
        'If rbtnCategorySelect.IsChecked AndAlso tvCategory.CheckedValue.Count <= 0 Then
        '    Throw New Exception("Please select atleast one Location")
        'End If



        '' Work done byb Parteek agaist ticket no.BHA/05/09/18-000514
        Dim str As String = ""
        Dim str_UOM As String = ""
        Dim Sql As String = "select (select DISTINCT ',['+UOM_Code+']' " &
        " FROM (SELECT DISTINCT UOM_CODE from TSPL_ITEM_UOM_DETAIL " &
        " inner join TSPL_PP_PRODUCTION_ENTRY_DETAIL on TSPL_PP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE=TSPL_ITEM_UOM_DETAIL.ITEM_CODE " &
        " left join TSPL_PP_PRODUCTION_ENTRY on TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_PP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE " &
        " where convert(date,TSPL_PP_PRODUCTION_ENTRY.PROD_DATE,103) >= convert(date,('" + txtfromDate.Value + "'),103) and convert(date,TSPL_PP_PRODUCTION_ENTRY.PROD_DATE,103) <= convert(date,('" & txtToDate.Value & "'),103) "
        If ChkItemSelect.IsChecked And cbgItem.CheckedValue.Count > 0 Then
            Sql += "and TSPL_ITEM_UOM_DETAIL.Item_Code IN (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
        End If
        Sql += " UNION ALL
                select DISTINCT UOM_Code 
                from TSPL_ITEM_UOM_DETAIL  inner join TSPL_PP_STD_FINALQC_DETAIL on TSPL_PP_STD_FINALQC_DETAIL.ITEM_CODE=TSPL_ITEM_UOM_DETAIL.ITEM_CODE  
                left join TSPL_PP_STD_FINALQC_HEAD on TSPL_PP_STD_FINALQC_HEAD.QC_CODE=TSPL_PP_STD_FINALQC_DETAIL.QC_CODE  where
                 convert(date,TSPL_PP_STD_FINALQC_HEAD.QC_DATE,103) >= convert(date,('" + txtfromDate.Value + "'),103) and convert(date,TSPL_PP_STD_FINALQC_HEAD.QC_DATE,103) <= convert(date,('" & txtToDate.Value & "'),103)"
        If ChkItemSelect.IsChecked And cbgItem.CheckedValue.Count > 0 Then
            Sql += "and TSPL_ITEM_UOM_DETAIL.Item_Code IN (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
        End If

        Sql += ")XX for xml path('')) "
        str_UOM = clsCommon.myCstr(clsDBFuncationality.getSingleValue(Sql))
        If String.IsNullOrEmpty(str_UOM) = True Then
            clsCommon.MyMessageBoxShow("No Data Found")
            Return
        End If
        str_UOM = str_UOM.Remove(0, 1)

        Dim array() As String = str_UOM.Split(",")

        'Dim obj As clsSaleRegisterParameterType = ReturnFilterData()

        ArrLocation = GetLocation()



        ''richa BHA/16/07/18-000168 show manual batch no 
        '=update by preeti gupta Against ticket No[BHA/12/09/18-000539]
        Dim qry As String = "select * from (select final.*,max(convert(varchar,TSPL_PP_PRODUCTION_PLAN_HEAD.Plan_Date,103)) as Plan_Date, max(TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_CODE) as STAGE_PROCESS_CODE,max(convert(varchar,TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_DATE,103))  as STAGE_PROCESS_DATE,max(TSPL_PP_ISSUE_HEAD.Issue_Code) as Issue_Code ,max(convert(varchar,TSPL_PP_ISSUE_HEAD.Issue_Date,103)) as Issue_Date,max(TSPL_PP_ISSUE_HEAD.ManualBatchNo ) as ManualBatchNo  from ( "

        qry += " select  TSPL_ITEM_MASTER.Item_Code ,TSPL_ITEM_MASTER.Item_Desc,main_location.Location_Code as main_loc_code,main_location.Location_Desc as main_loc_desc,TSPL_PP_PRODUCTION_ENTRY.CONSM_SECTION_CODE,TSPL_SECTION_MASTER.description as Section_Desc ,TSPL_PP_PRODUCTION_ENTRY.Batch_Code ,convert(varchar,TSPL_PP_PRODUCTION_ENTRY.BATCH_DATE,103) as BATCH_DATE  ,TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE ,convert(varchar,TSPL_PP_PRODUCTION_ENTRY.PROD_DATE,103)  as PROD_DATE, 'Production' as Doc_type"
        If chk_stockingunit.Checked Then
            qry += " ,stockunitconv. UOM_Code as unit_code,((isnull(unitconv.Conversion_Factor,1)*TSPL_PP_PRODUCTION_ENTRY_DETAIL.BATCH_QTY)/isnull(stockunitconv.Conversion_Factor,1)) as BATCH_QTY,((isnull(unitconv.Conversion_Factor,1)*TSPL_PP_PRODUCTION_ENTRY_DETAIL.Receipt_QTY)/isnull(stockunitconv.Conversion_Factor,1)) as RECEIPT_QTY"
        Else
            qry += " ,TSPL_PP_PRODUCTION_ENTRY_DETAIL.unit_code,TSPL_PP_PRODUCTION_ENTRY_DETAIL.BATCH_QTY,TSPL_PP_PRODUCTION_ENTRY_DETAIL.RECEIPT_QTY "
        End If
        For Each value As String In array
            qry += ",case when I." & clsCommon.myCstr(value) & "=0 then 0 else ( round((isnull(TSPL_PP_PRODUCTION_ENTRY_DETAIL.RECEIPT_QTY,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I."
            qry += clsCommon.myCstr(value)
            qry += ",2) ) end  as "
            qry += clsCommon.myCstr(value)
        Next

        qry += " ,TSPL_PP_PRODUCTION_ENTRY_DETAIL.LOCATION_CODE as Batch_location_code,Batch_location.Location_Desc as Batch_location_desc," & _
               " main_location.Is_Section ,main_location .Is_Sub_Location ,case when main_location.Is_Section='y'then 'Section wise Tanker'  when main_location .Is_Sub_Location='Y' then 'Sub Location' else 'Main Location' end as Production_Location_type,TSPL_PP_PRODUCTION_ENTRY_DETAIL.FAT_Per ,TSPL_PP_PRODUCTION_ENTRY_DETAIL.SNF_Per,TSPL_PP_PRODUCTION_ENTRY_DETAIL.FAT_Per + TSPL_PP_PRODUCTION_ENTRY_DETAIL.SNF_Per as TS_Per ,TSPL_PP_PRODUCTION_ENTRY_DETAIL.FAT_KG ,TSPL_PP_PRODUCTION_ENTRY_DETAIL.SNF_KG,TSPL_PP_PRODUCTION_ENTRY_DETAIL.FAT_KG + TSPL_PP_PRODUCTION_ENTRY_DETAIL.SNF_KG as TS_KG ,TSPL_PP_BATCH_ORDER_HEAD.Plan_Code " & _
               " from TSPL_PP_PRODUCTION_ENTRY " & _
               " left outer join TSPL_PP_PRODUCTION_ENTRY_DETAIL on TSPL_PP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE =TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE " & _
               " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code =TSPL_PP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE " & _
               " left outer join TSPL_LOCATION_MASTER as main_location on main_location.Location_Code =TSPL_PP_PRODUCTION_ENTRY.LOCATION_CODE " & _
               " left outer join TSPL_LOCATION_MASTER as Batch_location on Batch_location.Location_Code =TSPL_PP_PRODUCTION_ENTRY_DETAIL.LOCATION_CODE " & _
               " left outer join TSPL_SECTION_MASTER on TSPL_SECTION_MASTER.Section_Code =TSPL_PP_PRODUCTION_ENTRY.CONSM_SECTION_CODE " & _
               " left join TSPL_PP_BATCH_ORDER_HEAD on TSPL_PP_BATCH_ORDER_HEAD.Batch_Code =TSPL_PP_PRODUCTION_ENTRY.Batch_Code "


        '' Work done byb Parteek agaist ticket no.BHA/05/09/18-000514
        qry += "  left join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_PP_PRODUCTION_ENTRY_DETAIL.Item_Code "
        qry += "and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_PP_PRODUCTION_ENTRY_DETAIL.Unit_Code "
        qry += " left join (SELECT item_code,conversion_factor from TSPL_ITEM_UOM_DETAIL where Default_UOM=1) J ON J.item_code=TSPL_PP_PRODUCTION_ENTRY_DETAIL.Item_Code" & _
           " left join ( " & _
           " SELECT * FROM (select item_code,uom_code,conversion_factor from TSPL_ITEM_UOM_DETAIL) I " & _
           " PIVOT (Max(conversion_factor) FOR uom_code IN ( "
        qry += str_UOM
        qry += " )) P ) I ON TSPL_PP_PRODUCTION_ENTRY_DETAIL.Item_Code = I.item_code "

        If chk_stockingunit.Checked Then
            qry += " left outer join (select Item_Code,max(UOM_Code) as UOM_Code,max(Conversion_Factor) as Conversion_Factor from TSPL_ITEM_UOM_DETAIL where  Stocking_Unit='Y'  group by Item_Code) as stockunitconv on stockunitconv.Item_Code=TSPL_PP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE " & _
                   " left outer join TSPL_ITEM_UOM_DETAIL unitconv on unitconv.Item_Code=TSPL_PP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE and unitconv.UOM_Code=TSPL_PP_PRODUCTION_ENTRY_DETAIL.unit_code"
        End If
        qry += " where 2=2 "
        If chkPosted.Checked Then
            qry += " and TSPL_PP_PRODUCTION_ENTRY.POSTED=1 "
        End If

        qry += " and convert(date,TSPL_PP_PRODUCTION_ENTRY.PROD_DATE,103)>=convert(date,'" + txtfromDate.Value + "',103) and convert(date,TSPL_PP_PRODUCTION_ENTRY.PROD_DATE,103) <=convert(date,'" + txtToDate.Value + "' ,103)"
        If ChkItemSelect.IsChecked And cbgItem.CheckedValue.Count > 0 Then
            qry += "and TSPL_ITEM_MASTER.Item_Code IN (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
        End If
        'If chkSelectLocation.IsChecked And cbgLocation.CheckedValue.Count > 0 Then
        '    qry += " and main_location.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")  "
        'End If
        If ArrLocation IsNot Nothing AndAlso ArrLocation.Count > 0 Then
            qry += " and Batch_location.Location_Code in (" + clsCommon.GetMulcallString(ArrLocation) + ")  "
        End If
        If chkSectionSelect.IsChecked And cbgSection.CheckedValue.Count > 0 Then
            qry += " and TSPL_PP_PRODUCTION_ENTRY.CONSM_SECTION_CODE in (" + clsCommon.GetMulcallString(cbgSection.CheckedValue) + ")  "
        End If

        qry += " Union all" & _
               " select  TSPL_ITEM_MASTER.Item_Code ,TSPL_ITEM_MASTER.Item_Desc,main_location.Location_Code as main_loc_code,main_location.Location_Desc as main_loc_desc,TSPL_PP_PRODUCTION_ENTRY.CONSM_SECTION_CODE,TSPL_SECTION_MASTER.description as Section_Desc ,TSPL_PP_PRODUCTION_ENTRY.Batch_Code ,convert(varchar,TSPL_PP_PRODUCTION_ENTRY.BATCH_DATE,103) as BATCH_DATE  ,TSPL_PP_PRODUCTION_RETURN.PROD_RETURN_CODE ,convert(varchar,TSPL_PP_PRODUCTION_RETURN.RETURN_DATE,103)  as PROD_DATE, 'Production Return' as Doc_type"
        If chk_stockingunit.Checked Then
            qry += " ,stockunitconv. UOM_Code as unit_code,((isnull(unitconv.Conversion_Factor,1)*TSPL_PP_PRODUCTION_ENTRY_DETAIL.BATCH_QTY)/isnull(stockunitconv.Conversion_Factor,1)) as BATCH_QTY,-((isnull(unitconv.Conversion_Factor,1)*TSPL_PP_PRODUCTION_ENTRY_DETAIL.Receipt_QTY)/isnull(stockunitconv.Conversion_Factor,1)) as RECEIPT_QTY"
        Else
            qry += " ,TSPL_PP_PRODUCTION_ENTRY_DETAIL.unit_code,TSPL_PP_PRODUCTION_ENTRY_DETAIL.BATCH_QTY,-TSPL_PP_PRODUCTION_ENTRY_DETAIL.RECEIPT_QTY as RECEIPT_QTY "
        End If
        For Each value As String In array
            qry += ",case when I." & clsCommon.myCstr(value) & "=0 then 0 else (round((isnull(TSPL_PP_PRODUCTION_ENTRY_DETAIL.BATCH_QTY,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I."
            qry += clsCommon.myCstr(value)
            qry += ",2)) end  as "
            qry += clsCommon.myCstr(value)
        Next
        qry += " ,TSPL_PP_PRODUCTION_ENTRY_DETAIL.LOCATION_CODE as Batch_location_code,Batch_location.Location_Desc as Batch_location_desc," & _
               " main_location.Is_Section ,main_location .Is_Sub_Location ,case when main_location.Is_Section='y'then 'Section wise Tanker'  when main_location .Is_Sub_Location='Y' then 'Sub Location' else 'Main Location' end as Production_Location_type,TSPL_PP_PRODUCTION_ENTRY_DETAIL.FAT_Per ,TSPL_PP_PRODUCTION_ENTRY_DETAIL.SNF_Per,TSPL_PP_PRODUCTION_ENTRY_DETAIL.FAT_Per + TSPL_PP_PRODUCTION_ENTRY_DETAIL.SNF_Per as TS_Per ,-TSPL_PP_PRODUCTION_ENTRY_DETAIL.FAT_KG as FAT_KG ,-TSPL_PP_PRODUCTION_ENTRY_DETAIL.SNF_KG as SNF_KG,-(TSPL_PP_PRODUCTION_ENTRY_DETAIL.FAT_KG  + TSPL_PP_PRODUCTION_ENTRY_DETAIL.SNF_KG) as TS_KG ,TSPL_PP_BATCH_ORDER_HEAD.Plan_Code " & _
               " from TSPL_PP_PRODUCTION_ENTRY inner join TSPL_PP_PRODUCTION_RETURN on TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_PP_PRODUCTION_RETURN.PROD_ENTRY_CODE " & _
               " left outer join TSPL_PP_PRODUCTION_ENTRY_DETAIL on TSPL_PP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE =TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE " & _
               " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code =TSPL_PP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE " & _
               " left outer join TSPL_LOCATION_MASTER as main_location on main_location.Location_Code =TSPL_PP_PRODUCTION_ENTRY.LOCATION_CODE " & _
               " left outer join TSPL_LOCATION_MASTER as Batch_location on Batch_location.Location_Code =TSPL_PP_PRODUCTION_ENTRY_DETAIL.LOCATION_CODE " & _
               " left outer join TSPL_SECTION_MASTER on TSPL_SECTION_MASTER.Section_Code =TSPL_PP_PRODUCTION_ENTRY.CONSM_SECTION_CODE " & _
               " left join TSPL_PP_BATCH_ORDER_HEAD on TSPL_PP_BATCH_ORDER_HEAD.Batch_Code =TSPL_PP_PRODUCTION_ENTRY.Batch_Code "
        '' work done by parteek on 07/09/2018
        qry += "  left join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_PP_PRODUCTION_ENTRY_DETAIL.Item_Code "
        qry += "and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_PP_PRODUCTION_ENTRY_DETAIL.Unit_Code "
        qry += " left join (SELECT item_code,conversion_factor from TSPL_ITEM_UOM_DETAIL where Default_UOM=1) J ON J.item_code=TSPL_PP_PRODUCTION_ENTRY_DETAIL.Item_Code" & _
               " left join ( " & _
               " SELECT * FROM (select item_code,uom_code,conversion_factor from TSPL_ITEM_UOM_DETAIL) I " & _
               " PIVOT (Max(conversion_factor) FOR uom_code IN ( "
        qry += str_UOM
        qry += " )) P ) I ON TSPL_PP_PRODUCTION_ENTRY_DETAIL.Item_Code = I.item_code "
        If chk_stockingunit.Checked Then
            qry += " left outer join (select Item_Code,max(UOM_Code) as UOM_Code,max(Conversion_Factor) as Conversion_Factor from TSPL_ITEM_UOM_DETAIL where  Stocking_Unit='Y'  group by Item_Code) as stockunitconv on stockunitconv.Item_Code=TSPL_PP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE " & _
                   " left outer join TSPL_ITEM_UOM_DETAIL unitconv on unitconv.Item_Code=TSPL_PP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE and unitconv.UOM_Code=TSPL_PP_PRODUCTION_ENTRY_DETAIL.unit_code"
        End If
        qry += " where 2=2 "
        If chkPosted.Checked Then
            qry += " and TSPL_PP_PRODUCTION_RETURN.POSTED=1 "
        End If

        qry += " and convert(date,TSPL_PP_PRODUCTION_RETURN.RETURN_DATE,103)>=convert(date,'" + txtfromDate.Value + "',103) and convert(date,TSPL_PP_PRODUCTION_RETURN.RETURN_DATE,103) <=convert(date,'" + txtToDate.Value + "' ,103)"
        If ChkItemSelect.IsChecked And cbgItem.CheckedValue.Count > 0 Then
            qry += " and TSPL_ITEM_MASTER.Item_Code IN (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
        End If
        'If chkSelectLocation.IsChecked And cbgLocation.CheckedValue.Count > 0 Then
        '    qry += " and main_location.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")  "
        'End If
        If ArrLocation IsNot Nothing AndAlso ArrLocation.Count > 0 Then
            qry += " and Batch_location.Location_Code in (" + clsCommon.GetMulcallString(ArrLocation) + ")  "
        End If
        If chkSectionSelect.IsChecked And cbgSection.CheckedValue.Count > 0 Then
            qry += " and TSPL_PP_PRODUCTION_RETURN.CONSM_SECTION_CODE in (" + clsCommon.GetMulcallString(cbgSection.CheckedValue) + ")  "
        End If

        qry += " Union all"

        qry += " select "


        qry += " TSPL_ITEM_MASTER.Item_Code ,TSPL_ITEM_MASTER.Item_Desc,main_location.Location_Code as main_loc_code,main_location.Location_Desc as main_loc_desc,TSPL_PP_STANDARDIZATION_HEAD.CONSM_SECTION_CODE,TSPL_SECTION_MASTER.description as Section_Desc ,TSPL_PP_STANDARDIZATION_HEAD.Child_Batch_Code  ,convert(varchar,TSPL_PP_BATCH_ORDER_HEAD.BATCH_DATE,103) as BATCH_DATE  ,TSPL_PP_STANDARDIZATION_HEAD.Standardization_Code  as PROD_ENTRY_CODE ,convert(varchar,TSPL_PP_STANDARDIZATION_HEAD.Standardization_Date ,103)  as PROD_DATE, 'Standardization' as Doc_type"
        If chk_stockingunit.Checked Then
            qry += " ,stockunitconv. UOM_Code as unit_code,((isnull(unitconv.Conversion_Factor,1)*TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Quantity)/isnull(stockunitconv.Conversion_Factor,1)) as BATCH_QTY,((isnull(unitconv.Conversion_Factor,1)*TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Produced_Qty)/isnull(stockunitconv.Conversion_Factor,1)) as Produced_Qty"
        Else
            qry += " ,TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.unit_code,TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Quantity  as BATCH_QTY,TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Produced_Qty  "
        End If
        For Each value As String In array
            qry += ",case when I." & clsCommon.myCstr(value) & "=0 then 0 else ( round((isnull(TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Produced_Qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I."
            qry += clsCommon.myCstr(value)
            qry += ",2)) end as "
            qry += clsCommon.myCstr(value)
        Next
        qry += " ,TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.STD_Loaction_Code  as Batch_location_code,Batch_location.Location_Desc as Batch_location_desc,"
        qry += " main_location.Is_Section ,main_location .Is_Sub_Location ,case when main_location.Is_Section='y'then 'Section wise Tanker'  when main_location .Is_Sub_Location='Y' then 'Sub Location' else 'Main Location' end as Production_Location_type "
        If RequiredFinalQCofstandardization = True Then
            qry += ",TSPL_PP_STD_FINALQC_detail.fat_per as FAT_Per,TSPL_PP_STD_FINALQC_detail.SNF_Per as SNF_Per
                    ,TSPL_PP_STD_FINALQC_detail.fat_per+TSPL_PP_STD_FINALQC_detail.SNF_Per as TS_Per
                    ,TSPL_PP_STD_FINALQC_HEAD.Tot_Produce_FATKG as FAT_KG,TSPL_PP_STD_FINALQC_HEAD.Tot_Produce_SNFKG as SNF_KG
                    ,TSPL_PP_STD_FINALQC_HEAD.Tot_Produce_FATKG+TSPL_PP_STD_FINALQC_HEAD.Tot_Produce_SNFKG as TS_KG"
        Else
            qry += ",TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Requir_FAT_per   as FAT_Per ,TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Requir_SNF_Per  as SNF_Per,TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Requir_FAT_per+TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Requir_SNF_Per as TS_Per ,TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Requir_FAT_KG  as FAT_KG ,TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Requir_SNF_KG  as SNF_KG ,TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Requir_FAT_KG+TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Requir_SNF_KG  as TS_KG"
        End If



        qry += ", TSPL_PP_BATCH_ORDER_HEAD.Plan_Code "

        qry += " from TSPL_PP_STANDARDIZATION_HEAD"
        qry += " left outer join TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL on TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Standardization_Code =TSPL_PP_STANDARDIZATION_HEAD.Standardization_Code "
        qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code =TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.ITEM_CODE "
        qry += " left outer join TSPL_LOCATION_MASTER as main_location on main_location.Location_Code =TSPL_PP_STANDARDIZATION_HEAD.Loaction_Code  "
        qry += " left outer join TSPL_LOCATION_MASTER as Batch_location on Batch_location.Location_Code =TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.STD_Loaction_Code   "
        qry += " left outer join TSPL_SECTION_MASTER on TSPL_SECTION_MASTER.Section_Code =TSPL_PP_STANDARDIZATION_HEAD.CONSM_SECTION_CODE "
        qry += " left outer join TSPL_PP_BATCH_ORDER_HEAD on TSPL_PP_BATCH_ORDER_HEAD.Batch_Code =TSPL_PP_STANDARDIZATION_HEAD.Child_Batch_Code"
        If RequiredFinalQCofstandardization = True Then
            qry += "  left outer join TSPL_PP_STD_FINALQC_HEAD on  TSPL_PP_STD_FINALQC_HEAD.Child_Batch_Code = TSPL_PP_STANDARDIZATION_HEAD.Child_Batch_Code
                      left outer join TSPL_PP_STD_FINALQC_detail on TSPL_PP_STD_FINALQC_detail.QC_Code=TSPL_PP_STD_FINALQC_HEAD.QC_Code "
        End If
        qry += "  left join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Item_Code "
        qry += "and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Unit_Code "
        '' work done by parteek on 07/09/2018
        qry += " left join (SELECT item_code,conversion_factor from TSPL_ITEM_UOM_DETAIL where Default_UOM=1) J ON J.item_code=TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Item_Code" & _
               " left join ( " & _
               " SELECT * FROM (select item_code,uom_code,conversion_factor from TSPL_ITEM_UOM_DETAIL) I " & _
               " PIVOT (Max(conversion_factor) FOR uom_code IN ( "
        qry += str_UOM
        qry += " )) P ) I ON TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Item_Code = I.item_code "
        If chk_stockingunit.Checked Then
            qry += " left outer join (select Item_Code,max(UOM_Code) as UOM_Code,max(Conversion_Factor) as Conversion_Factor from TSPL_ITEM_UOM_DETAIL where  Stocking_Unit='Y'  group by Item_Code) as stockunitconv on stockunitconv.Item_Code=TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.ITEM_CODE  "
            qry += " left outer join TSPL_ITEM_UOM_DETAIL unitconv on unitconv.Item_Code=TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.ITEM_CODE and unitconv.UOM_Code=TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.unit_code "
        End If
        qry += " where 2=2 "
        If chkPosted.Checked Then
            qry += " and TSPL_PP_STANDARDIZATION_HEAD.POSTED=1 "
        End If

        qry += " and convert(date,TSPL_PP_STANDARDIZATION_HEAD.Standardization_Date,103)>=convert(date,'" + txtfromDate.Value + "',103) and convert(date,TSPL_PP_STANDARDIZATION_HEAD.Standardization_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103)"
        If ChkItemSelect.IsChecked And cbgItem.CheckedValue.Count > 0 Then
            qry += " and TSPL_ITEM_MASTER.Item_Code IN (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
        End If
        'If chkSelectLocation.IsChecked And cbgLocation.CheckedValue.Count > 0 Then
        '    qry += " and main_location.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")  "
        'End If
        If ArrLocation IsNot Nothing AndAlso ArrLocation.Count > 0 Then
            qry += " and Batch_location.Location_Code in (" + clsCommon.GetMulcallString(ArrLocation) + ")  "
        End If
        If chkSectionSelect.IsChecked And cbgSection.CheckedValue.Count > 0 Then
            qry += " and TSPL_PP_STANDARDIZATION_HEAD.CONSM_SECTION_CODE in (" + clsCommon.GetMulcallString(cbgSection.CheckedValue) + ")  "
        End If
        qry += " ) as Final"
        qry += " left join TSPL_PP_PRODUCTION_PLAN_HEAD on TSPL_PP_PRODUCTION_PLAN_HEAD.Plan_Code  =final.Plan_Code  "
        qry += " left join TSPL_PP_STAGE_PROCESS_HEAD on TSPL_PP_STAGE_PROCESS_HEAD.Main_Batch_Code =final.Batch_Code"
        '=======================added by preeti gupta[27/03/2017]=========
        qry += " left join TSPL_PP_ISSUE_HEAD on TSPL_PP_ISSUE_HEAD.Batch_Code =final.Batch_Code "
        '==================================================================

        qry += " group by final.Plan_Code , final.Item_Code ,final.Item_Desc,final. main_loc_code,final. main_loc_desc,final.CONSM_SECTION_CODE,final. Section_Desc ,final.Batch_Code ,final.BATCH_DATE  ,final.PROD_ENTRY_CODE ,final.PROD_DATE, final.Doc_type,final.unit_code ,final.BATCH_QTY,final.RECEIPT_QTY ,final. Batch_location_code,final.Batch_location_desc, final.Is_Section ,final .Is_Sub_Location ,final.Production_Location_type,final.FAT_Per ,final.SNF_Per ,final.FAT_KG ,final.SNF_KG, final.TS_Per ,final.TS_KG  "
        For Each value As String In array
            qry += " ,"
            qry += clsCommon.myCstr(value)
        Next
        qry += " ) as"
        qry += "  xx"



        qry += " where 2=2 and convert(date,xx.PROD_DATE,103)>=convert(date,'" + txtfromDate.Value + "',103) and convert(date,xx.PROD_DATE,103) <=convert(date,'" + txtToDate.Value + "' ,103)"
        If ChkItemSelect.IsChecked And cbgItem.CheckedValue.Count > 0 Then
            qry += "and xx.Item_Code  IN (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
        End If
        'If chkSelectLocation.IsChecked And cbgLocation.CheckedValue.Count > 0 Then
        '    qry += " and xx.main_loc_code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")  "
        'End If
        If ArrLocation IsNot Nothing AndAlso ArrLocation.Count > 0 Then
            qry += " and xx.Batch_location_code in (" + clsCommon.GetMulcallString(ArrLocation) + ")  "
        End If
        If chkSectionSelect.IsChecked And cbgSection.CheckedValue.Count > 0 Then
            qry += " and xx.CONSM_SECTION_CODE in (" + clsCommon.GetMulcallString(cbgSection.CheckedValue) + ")  "
        End If
        qry += "order by convert(date,xx.PROD_DATE,103) "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        If dt IsNot Nothing And dt.Rows.Count > 0 Then
            gv1.DataSource = Nothing
            gv1.Columns.Clear()
            gv1.Rows.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.ShowGroupPanel = False

            gv1.EnableFiltering = True

            RadPageView1.SelectedPage = RadPageViewPage2
        Else
            clsCommon.MyMessageBoxShow("No Data Found")
        End If

        gv1.DataSource = dt
        'SetGridFormationOFGV1()
        ReStoreGridLayout()
        gv1.BestFitColumns()
        chk_stockingunit.Enabled = False

    End Sub
    Sub SetGridFormationOFGV1()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
        Next

        Gv1.Columns("Item_Code").Width = 100
        Gv1.Columns("Item_Code").IsVisible = False
        Gv1.Columns("Item_Code").HeaderText = "Item Code"

        Gv1.Columns("Item_Desc").IsVisible = True
        Gv1.Columns("Item_Desc").Width = 150
        Gv1.Columns("Item_Desc").HeaderText = "Item Name"

        Gv1.Columns("main_loc_code").IsVisible = True
        Gv1.Columns("main_loc_code").Width = 100
        Gv1.Columns("main_loc_code").HeaderText = "Main Location Code"

        Gv1.Columns("main_loc_desc").IsVisible = True
        Gv1.Columns("main_loc_desc").Width = 100
        Gv1.Columns("main_loc_desc").HeaderText = "Main Location Name"

        Gv1.Columns("Batch_Code").IsVisible = True
        Gv1.Columns("Batch_Code").Width = 100
        Gv1.Columns("Batch_Code").HeaderText = "Batch No"


        Gv1.Columns("BATCH_DATE").IsVisible = True
        Gv1.Columns("BATCH_DATE").Width = 100
        Gv1.Columns("BATCH_DATE").HeaderText = "Batch Date"

        Gv1.Columns("CONSM_SECTION_CODE").IsVisible = True
        Gv1.Columns("CONSM_SECTION_CODE").Width = 100
        Gv1.Columns("CONSM_SECTION_CODE").HeaderText = "Section"

        Gv1.Columns("CONSM_SECTION_CODE").IsVisible = True
        Gv1.Columns("CONSM_SECTION_CODE").Width = 100
        Gv1.Columns("CONSM_SECTION_CODE").HeaderText = "Section Code"


        Gv1.Columns("Section_Desc").IsVisible = True
        Gv1.Columns("Section_Desc").Width = 100
        gv1.Columns("Section_Desc").HeaderText = "Section Name"

        gv1.Columns("PROD_ENTRY_CODE").IsVisible = True
        gv1.Columns("PROD_ENTRY_CODE").Width = 100
        gv1.Columns("PROD_ENTRY_CODE").HeaderText = "Doc Code"

        Gv1.Columns("PROD_DATE").IsVisible = True
        Gv1.Columns("PROD_DATE").Width = 100
        Gv1.Columns("PROD_DATE").HeaderText = "Doc Date"

        Gv1.Columns("Doc_type").IsVisible = True
        Gv1.Columns("Doc_type").Width = 100
        Gv1.Columns("Doc_type").HeaderText = "Doc Type"

        Gv1.Columns("unit_code").IsVisible = True
        Gv1.Columns("unit_code").Width = 100
        Gv1.Columns("unit_code").HeaderText = "UOM"

        Gv1.Columns("BATCH_QTY").IsVisible = True
        Gv1.Columns("BATCH_QTY").Width = 100
        gv1.Columns("BATCH_QTY").HeaderText = "Batch qty"

        gv1.Columns("RECEIPT_QTY").IsVisible = True
        gv1.Columns("RECEIPT_QTY").Width = 100
        gv1.Columns("RECEIPT_QTY").HeaderText = "Produced qty"


        Gv1.Columns("Batch_location_code").IsVisible = True
        Gv1.Columns("Batch_location_code").Width = 100
        Gv1.Columns("Batch_location_code").HeaderText = "Produced Location Code"


        Gv1.Columns("Batch_location_desc").IsVisible = True
        Gv1.Columns("Batch_location_desc").Width = 100
        Gv1.Columns("Batch_location_desc").HeaderText = "Produced Location Name"

        Gv1.Columns("Production_Location_type").IsVisible = True
        Gv1.Columns("Production_Location_type").Width = 100
        Gv1.Columns("Production_Location_type").HeaderText = "Produced Location Type"

        Gv1.Columns("FAT_Per").IsVisible = True
        Gv1.Columns("FAT_Per").Width = 100
        Gv1.Columns("FAT_Per").HeaderText = "Fat %"


        Gv1.Columns("SNF_Per").IsVisible = True
        Gv1.Columns("SNF_Per").Width = 100
        Gv1.Columns("SNF_Per").HeaderText = "Snf %"


        Gv1.Columns("FAT_KG").IsVisible = True
        Gv1.Columns("FAT_KG").Width = 100
        Gv1.Columns("FAT_KG").HeaderText = "Fat Kg"


        Gv1.Columns("SNF_KG").IsVisible = True
        Gv1.Columns("SNF_KG").Width = 100
        gv1.Columns("SNF_KG").HeaderText = "Snf Kg"

        gv1.Columns("Plan_Code").IsVisible = True
        gv1.Columns("Plan_Code").Width = 100
        gv1.Columns("Plan_Code").HeaderText = "Plan Code"

        gv1.Columns("Plan_Date").IsVisible = True
        gv1.Columns("Plan_Date").Width = 100
        gv1.Columns("Plan_Date").HeaderText = "Plan Date"

        gv1.Columns("Is_Section").IsVisible = False
        gv1.Columns("Is_Section").Width = 100
        gv1.Columns("Is_Section").HeaderText = "Is_Section"

        gv1.Columns("Is_Sub_location").IsVisible = False
        gv1.Columns("Is_Sub_location").Width = 100
        gv1.Columns("Is_Sub_location").HeaderText = "Is_Sub_location"

        gv1.Columns("STAGE_PROCESS_CODE").IsVisible = True
        gv1.Columns("STAGE_PROCESS_CODE").Width = 100
        gv1.Columns("STAGE_PROCESS_CODE").HeaderText = "Stage process code"

        gv1.Columns("STAGE_PROCESS_DATE").IsVisible = True
        gv1.Columns("STAGE_PROCESS_DATE").Width = 100
        gv1.Columns("STAGE_PROCESS_DATE").HeaderText = "Stage process date"

        gv1.Columns("Issue_Code").IsVisible = True
        gv1.Columns("Issue_Code").Width = 100
        gv1.Columns("Issue_Code").HeaderText = "Issue Code"

        gv1.Columns("Issue_Date").IsVisible = True
        gv1.Columns("Issue_Date").Width = 100
        gv1.Columns("Issue_Date").HeaderText = "Issue Date"

        ''richa BHA/16/07/18-000168 show manual batch no 
        gv1.Columns("ManualBatchNo").IsVisible = True
        gv1.Columns("ManualBatchNo").Width = 150
        gv1.Columns("ManualBatchNo").HeaderText = "Manual Batch No"

        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As New GridViewSummaryItem("RECEIPT_QTY", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("FAT_KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("SNF_KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)


        ReStoreGridLayout()
    End Sub

     Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= Gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To Gv1.Columns.Count - 1 Step ii + 1
                        Gv1.Columns(ii).IsVisible = False
                        Gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    Gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtfromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            Dim CompName As String = clsDBFuncationality.getSingleValue("Select Comp_Name from TSPL_COMPANY_MASTER Where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'")
            arrHeader.Add(CompName)
            If ChkItemSelect.IsChecked Then
                Dim strItemName As String = ""
                For Each StrName As String In cbgItem.CheckedDisplayMember
                    If clsCommon.myLen(strItemName) > 0 Then
                        strItemName += ", "
                    End If
                    strItemName += StrName
                Next
                Dim strItemCode As String = ""
                For Each StrCode As String In cbgItem.CheckedValue
                    If clsCommon.myLen(strItemCode) > 0 Then
                        strItemCode += ", "
                    End If
                    strItemCode += StrCode
                Next
                arrHeader.Add((" Item Name: " + strItemName + " "))
            End If
            'If chkSelectLocation.IsChecked Then
            '    Dim strLocationName As String = ""
            '    For Each StrName As String In cbgLocation.CheckedDisplayMember
            '        If clsCommon.myLen(strLocationName) > 0 Then
            '            strLocationName += ", "
            '        End If
            '        strLocationName += StrName
            '    Next

            '    arrHeader.Add(("Location Name: " + strLocationName + " "))
            'End If
            If rbtnLocationSelect.IsChecked Then
                Dim strLoca As String = ""
                For Each grow As GridViewRowInfo In gvLocation.Rows
                    If clsCommon.myCBool(grow.Cells("SEL").Value) = True Then
                        strLoca += "," + clsCommon.myCstr(grow.Cells("NAME").Value)
                    End If
                Next
                arrHeader.Add("Location Name: " + strLoca)
            End If
            If chkSectionSelect.IsChecked Then
                Dim strSelectName As String = ""
                For Each StrName As String In cbgSection.CheckedDisplayMember
                    If clsCommon.myLen(strSelectName) > 0 Then
                        strSelectName += ", "
                    End If
                    strSelectName += StrName
                Next

                arrHeader.Add(("Section Name: " + strSelectName + " "))
            End If

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Production Entry Report", Gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Production Entry Report", Gv1, arrHeader, Me.Text, True)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv1
        LoadData()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        reset()
    End Sub

    Private Sub rmExcel_Click(sender As Object, e As EventArgs) Handles rmExcel.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtfromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptproductionEntryReport & "'"))
            If ChkItemSelect.IsChecked Then
                Dim strItemName As String = ""
                For Each StrName As String In cbgItem.CheckedDisplayMember
                    If clsCommon.myLen(strItemName) > 0 Then
                        strItemName += ", "
                    End If
                    strItemName += StrName
                Next
                Dim strItemCode As String = ""
                For Each StrCode As String In cbgItem.CheckedValue
                    If clsCommon.myLen(strItemCode) > 0 Then
                        strItemCode += ", "
                    End If
                    strItemCode += StrCode
                Next
                arrHeader.Add((" Item Name: " + strItemName + " "))
            End If
            'If chkSelectLocation.IsChecked Then
            '    Dim strLocationName As String = ""
            '    For Each StrName As String In cbgLocation.CheckedDisplayMember
            '        If clsCommon.myLen(strLocationName) > 0 Then
            '            strLocationName += ", "
            '        End If
            '        strLocationName += StrName
            '    Next

            '    arrHeader.Add(("Location Name: " + strLocationName + " "))
            'End If
            If rbtnLocationSelect.IsChecked Then
                Dim strLoca As String = ""
                For Each grow As GridViewRowInfo In gvLocation.Rows
                    If clsCommon.myCBool(grow.Cells("SEL").Value) = True Then
                        strLoca += "," + clsCommon.myCstr(grow.Cells("NAME").Value)
                    End If
                Next
                arrHeader.Add("Location Name: " + strLoca)
            End If
            If chkSectionSelect.IsChecked Then
                Dim strSelectName As String = ""
                For Each StrName As String In cbgSection.CheckedDisplayMember
                    If clsCommon.myLen(strSelectName) > 0 Then
                        strSelectName += ", "
                    End If
                    strSelectName += StrName
                Next

                arrHeader.Add(("Section Name: " + strSelectName + " "))
            End If

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
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    'Private Sub rmPDF_Click(sender As Object, e As EventArgs)
    '    print(EnumExportTo.PDF)
    'End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = Gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub ChkItemAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles ChkItemAll.ToggleStateChanged
        cbgItem.Enabled = ChkItemSelect.IsChecked
    End Sub

    Private Sub chkAllLocation_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkAllLocation.ToggleStateChanged
        cbgLocation.Enabled = chkSelectLocation.IsChecked
    End Sub

    Private Sub ChkSectionAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles ChkSectionAll.ToggleStateChanged
        cbgSection.Enabled = chkSectionSelect.IsChecked
    End Sub

    Private Sub rbtnCategoryAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnCategoryAll.ToggleStateChanged
        tvCategory.Enabled = rbtnCategorySelect.IsChecked
    End Sub

    Private Sub RptproductionEntryReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        'ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P For Print")
        ButtonToolTip.SetToolTip(btnReset, "Pres%s Alt+N Adding New")
        RequiredFinalQCofstandardization = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RequiredFinalQCofstandardization, clsFixedParameterCode.RequiredFinalQCofstandardization, Nothing)) = 0, False, True)
        reset()
        rbtnLocationAll.IsChecked = True
        RadGroupBox4.Visible = False
    End Sub

    Private Sub RptproductionEntryReport_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

    End Sub


    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtfromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptproductionEntryReport & "'"))
            If ChkItemSelect.IsChecked Then
                Dim strItemName As String = ""
                For Each StrName As String In cbgItem.CheckedDisplayMember
                    If clsCommon.myLen(strItemName) > 0 Then
                        strItemName += ", "
                    End If
                    strItemName += StrName
                Next
                Dim strItemCode As String = ""
                For Each StrCode As String In cbgItem.CheckedValue
                    If clsCommon.myLen(strItemCode) > 0 Then
                        strItemCode += ", "
                    End If
                    strItemCode += StrCode
                Next
                arrHeader.Add((" Item Name: " + strItemName + " "))
            End If
            'If chkSelectLocation.IsChecked Then
            '    Dim strLocationName As String = ""
            '    For Each StrName As String In cbgLocation.CheckedDisplayMember
            '        If clsCommon.myLen(strLocationName) > 0 Then
            '            strLocationName += ", "
            '        End If
            '        strLocationName += StrName
            '    Next

            '    arrHeader.Add(("Location Name: " + strLocationName + " "))
            'End If
            If rbtnLocationSelect.IsChecked Then
                Dim strLoca As String = ""
                For Each grow As GridViewRowInfo In gvLocation.Rows
                    If clsCommon.myCBool(grow.Cells("SEL").Value) = True Then
                        strLoca += "," + clsCommon.myCstr(grow.Cells("NAME").Value)
                    End If
                Next
                arrHeader.Add("Location Name: " + strLoca)
            End If
            If chkSectionSelect.IsChecked Then
                Dim strSelectName As String = ""
                For Each StrName As String In cbgSection.CheckedDisplayMember
                    If clsCommon.myLen(strSelectName) > 0 Then
                        strSelectName += ", "
                    End If
                    strSelectName += StrName
                Next

                arrHeader.Add(("Section Name: " + strSelectName + " "))
            End If

            transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
            clsCommon.MyExportToPDF("Production Entry Report", gv1, arrHeader, "Production Entry Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
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

    Private Sub RadButton4_Click(sender As Object, e As EventArgs) Handles RadButton4.Click
        CheckedAll(gvLocation)
    End Sub

    Private Sub RadButton5_Click(sender As Object, e As EventArgs) Handles RadButton5.Click
        UnCheckedAll(gvLocation)
    End Sub

    Private Sub RbtnLocationAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnLocationAll.ToggleStateChanged
        gvLocation.Enabled = rbtnLocationSelect.IsChecked
        RadButton4.Enabled = rbtnLocationSelect.IsChecked
        RadButton5.Enabled = rbtnLocationSelect.IsChecked
    End Sub

    Private Sub RbtnLocationSelect_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnLocationSelect.ToggleStateChanged
        gvLocation.Enabled = rbtnLocationSelect.IsChecked
        RadButton4.Enabled = rbtnLocationSelect.IsChecked
        RadButton5.Enabled = rbtnLocationSelect.IsChecked
    End Sub

    Private Sub GvLocation_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gvLocation.CellDoubleClick
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

    Function GetLocation() As ArrayList
        Dim strWhrCatg As String = ""
        Dim qry As String
        Dim arrLocation As ArrayList = Nothing
        If rbtnLocationSelect.IsChecked Then
            Dim IsApplicable As Boolean = False
            For ii As Integer = 0 To gvLocation.RowCount - 1
                If clsCommon.myCBool(gvLocation.Rows(ii).Cells("SEL").Value) Then
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
            qry = "select Location_Code from TSPL_LOCATION_MASTER where 2=2 and (" + strWhrCatg + ")"
            Dim dtLoc As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dtLoc IsNot Nothing AndAlso dtLoc.Rows.Count > 0 Then
                arrLocation = New ArrayList
                For Each dr As DataRow In dtLoc.Rows
                    arrLocation.Add(dr("Location_Code"))
                Next
            End If
        End If
        Return arrLocation
    End Function

End Class
