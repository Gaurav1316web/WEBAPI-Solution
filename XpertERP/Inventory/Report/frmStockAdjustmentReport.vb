'' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
'---Preeti Gupta ticket no.-BM00000003031--03/07/014

Imports common
Imports System.IO
Public Class FrmStockAdjustmentReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim strSql As String
    Dim dt As DataTable
    Dim dtCategory As DataTable
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.mbtnStockAdjustmentReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        '' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
        btnExportSplit.Visible = MyBase.isExport
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnprint, "Press Ctrl+P Print the Report")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+R Reset the Window")
        btnprint.Visible = MyBase.isPrintFlag
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub FrmStockAdjustmentReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        SetUserMgmtNew()
        
        'dtpStarttime.Value = DateTime.Now
        'dtpStarttime.ShowUpDown = True
        'dtpendtime.Value = DateTime.Now
        'dtpendtime.ShowUpDown = True
        dtpstart.Focus()
        'LoadType()
        'LoadCategory()
        LoadLocation()
        chktypeAll.IsChecked = True
        RadioBtnSummary.IsChecked = True
        chkLocationAll.IsChecked = True
        reset()
        RadPageView1.SelectedPage = RadPageViewPage1
        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If

    End Sub

    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "ST-ADJ-RPT"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
    '        strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '        strTemp = Split(strRights, ",")
    '        If strTemp(0) = "0" Then
    '            MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    '            funSetUserAccess = False
    '            blnRead = False
    '            Me.Close()
    '            Exit Function
    '        Else
    '            blnRead = True
    '        End If
    '        If strTemp(1) = "0" Then 'Grant modify access

    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access

    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception
    '        myMessages.myExceptions(er)
    '    End Try
    'End Function

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        reset()
    End Sub

    Public Sub reset()
        dtpstart.Value = clsCommon.GetDateWithStartTime(clsCommon.GETSERVERDATE())
        dtpend.Value = clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE())
        'dtpStarttime.Value = DateTime.Now
        ' dtpendtime.Value = DateTime.Now
        'LoadType()
        'LoadCategory()
        LoadItem()
        chktypeAll.IsChecked = True
        chkLocationAll.IsChecked = True
        chkIemAll.IsChecked = True
        RadPageView1.SelectedPage = RadPageViewPage1
        gv1.Columns.Clear()
    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        strSql = funPrint()
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(strSql)
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "No Record Found", Me.Text)
        Else
            dt = clsDBFuncationality.GetDataTable(strSql)
            Dim frmCRV As New frmCrystalReportViewer()
            If RadioBtnSummary.IsChecked Then
                frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, "crptStockAdjustmentSummary", "Stock Adjustment Summary")
            Else
                frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, "crptStockAdjustmentDetail", "Stock Adjustment Detail")
            End If
            frmCRV = Nothing
        End If

    End Sub

    Private Function funPrint() As String
        Dim qry As String = ""
        Try
            Dim fromdate As String = clsCommon.GetPrintDate(dtpstart.Value, "dd/MMM/yyyy hh:mm tt")
            Dim enddate As String = clsCommon.GetPrintDate(dtpend.Value, "dd/MMM/yyyy hh:mm tt")

            If chkItemSelect.IsChecked AndAlso cbgItem.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one Item")
            End If

            If chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one Location")
            End If
            If (dtpstart.Value > dtpend.Value) Then
                common.clsCommon.MyMessageBoxShow(Me, "'Start Date' Cann't be more than 'End date'", Me.Text)
                'ElseIf (dtpStarttime.Value > dtpendtime.Value) Then
                '    common.clsCommon.MyMessageBoxShow("'Start Time' Cann't be more than 'End Time'")
            Else
                Dim filter As String = ""

                If chkItemSelect.IsChecked = True Then
                    filter += " and TSPL_ADJUSTMENT_DETAIL.Item_Code IN  (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ")  "
                End If
                Dim strPost As String = "2=2"
                If optPosted.IsChecked Then
                    strPost = " TSPL_ADJUSTMENT_HEADER.posted = 'Y'"
                End If

                If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count = 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please select atleast one Location", Me.Text)

                    Return False
                    Exit Function
                ElseIf chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0 Then
                    filter += " and (TSPL_ADJUSTMENT_HEADER.Loc_Code in ( Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) or TSPL_ADJUSTMENT_HEADER.MainLocationCode in ( Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")))"
                End If
                Dim LocNcmpAdd As String = ""
                If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count = 1 Then
                    LocNcmpAdd = " (Select MAX(TSPL_LOCATION_MASTER.Add1 + case When TSPL_LOCATION_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.Add2, 103) End + Case When TSPL_LOCATION_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_LOCATION_MASTER.Add3,103) end + case When TSPL_LOCATION_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.City_Code, 103) end+ Case When TSPL_LOCATION_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_LOCATION_MASTER.State) end +  Case When TSPL_LOCATION_MASTER.Pin_Code='' Then '' Else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.Pin_Code, 103)  end) as Address from TSPL_LOCATION_MASTER WHERE Loc_Segment_Code  IN (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "))"
                Else
                    LocNcmpAdd = " ISNULL(tspl_company_Master.ADD1,'')"
                End If

                ''''''''''''''''''''''''''''''''''''''
                dtCategory = clsDBFuncationality.GetDataTable("select ITEM_CATEGORY_CODE AS CodeColumn,ITEM_CATEGORY_CODE+'DESC' as CodeDescColumn,DESCRIPTION as DescColumn  from TSPL_ITEM_CATEGORY_LEVEL order by CATEGORY_LEVEL")
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
                    strCategoryTable = "select Item_Code," + strCodeColumnMax + "," + strCodeDescColumnMax + "  from (" + Environment.NewLine &
                " select * from ( " + Environment.NewLine &
                " select TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code " + Environment.NewLine &
                " ,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code+'DESC' as Item_Category_CodeDesc " + Environment.NewLine &
                " ,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values " + Environment.NewLine &
                " ,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as Category_Value_Desc " + Environment.NewLine &
                " from  TSPL_ITEM_MASTER  " + Environment.NewLine &
                " left outer join TSPL_ITEM_MASTER_CATEGORY on  TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code " + Environment.NewLine &
                " left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values" + Environment.NewLine &
                " where 2=2 " + Environment.NewLine &
                " )xx" + Environment.NewLine &
                " Pivot " + Environment.NewLine &
                " ( max(Item_Cagetory_Values) for Item_Category_Code   in ( " + strCodeColumn + ")" + Environment.NewLine &
                " ) Pivt" + Environment.NewLine &
                " Pivot " + Environment.NewLine &
                " (" + Environment.NewLine &
                " max(Category_Value_Desc) for Item_Category_CodeDesc in (" + strCodeDescColumn + ")" + Environment.NewLine &
                " ) Pivt1 " + Environment.NewLine &
                " ) xxx group by Item_Code "
                End If


                If RadioBtnDetail.IsChecked = True Then ''TSPL_ADJUSTMENT_HEADER.Created_time, (hide column said by client)
                    qry = "SELECT  TSPL_ADJUSTMENT_HEADER.Adjustment_No," &
                    " convert(varchar,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) as Adjustment_Date, " &
                    " TSPL_ADJUSTMENT_HEADER.Description as [DocumentNo],tspl_item_master.itf_code as itf_code, " &
                    " case when isnull(TSPL_ADJUSTMENT_HEADER.IsMilkType,0)=1 then 'Y' ELSE 'N' end as [Milk Type],(case when TSPL_ADJUSTMENT_HEADER.Adjustment_Type='ADJ' then 'Adjustment' when TSPL_ADJUSTMENT_HEADER.Adjustment_Type='FLG' then 'Flushing' when TSPL_ADJUSTMENT_HEADER.Adjustment_Type='OPG' then 'Opening' when TSPL_ADJUSTMENT_HEADER.Adjustment_Type='CLG' then 'Closing' when TSPL_ADJUSTMENT_HEADER.Adjustment_Type='AAD' then 'Auto Adjustment' when TSPL_ADJUSTMENT_HEADER.Adjustment_Type='' and TSPL_ADJUSTMENT_HEADER.Description like '%Auto Adjustment%' then 'Auto Adjustment' else 'Other' end) as [Adjustment Type]," &
                    " TSPL_ADJUSTMENT_HEADER.Trans_Type as [In/Out],TSPL_ADJUSTMENT_DETAIL.Item_Code,TSPL_ADJUSTMENT_DETAIL.Item_Description as [Description],TSPL_ADJUSTMENT_DETAIL.Unit_Code,TSPL_ADJUSTMENT_DETAIL.mrp," &
                    " case when TSPL_ADJUSTMENT_DETAIL.Adjustment_Type IN('BI','QI') then TSPL_ADJUSTMENT_DETAIL.Item_Quantity else (TSPL_ADJUSTMENT_DETAIL.Item_Quantity)*-1 end as [Quantity], " &
                    " case when TSPL_ADJUSTMENT_HEADER.Trans_Type='In' then TSPL_ADJUSTMENT_DETAIL.Item_Quantity else case when  TSPL_ADJUSTMENT_HEADER.Trans_Type='Out' then TSPL_ADJUSTMENT_DETAIL.Item_Quantity*(-1) else 0 end end as [InOutQuantity],   " &
                    " TSPL_COMPANY_MASTER.Comp_Name as compname, TSPL_COMPANY_MASTER.Logo_Img, TSPL_COMPANY_MASTER.Logo_Img2, " + LocNcmpAdd + " as address, CONVERT(date, '" & dtpstart.Value & "', 103) AS startdate, CONVERT(date, '" & dtpend.Value & "', 103) AS enddate, substring('" & fromdate & "',12,12) as startTime, substring('" & enddate & "',12,12) as endTime,TSPL_ADJUSTMENT_HEADER.MainLocationCode as [Main Location],TSPL_ADJUSTMENT_HEADER.Loc_Code as Location, (case when TSPL_ADJUSTMENT_HEADER.Trans_Type='Out' then (-1*TSPL_ADJUSTMENT_DETAIL.SNF_KG)  else TSPL_ADJUSTMENT_DETAIL.SNF_KG end) as [SNF KG]," &
                    " TSPL_ADJUSTMENT_DETAIL.SNF_Pers as [SNF Per],  (case when TSPL_ADJUSTMENT_HEADER.Trans_Type='Out' then  (-1*TSPL_ADJUSTMENT_DETAIL.FAT_KG)  else TSPL_ADJUSTMENT_DETAIL.FAT_KG end)  as [FAT KG],  TSPL_ADJUSTMENT_DETAIL.FAT_Pers as [FAT Per], " &
                    " TSPL_ADJUSTMENT_DETAIL.Unit_Cost  as [Unit_Cost], case when TSPL_ADJUSTMENT_HEADER.Trans_Type='Out' then (-1)*TSPL_ADJUSTMENT_DETAIL.Item_Cost else TSPL_ADJUSTMENT_DETAIL.Item_Cost end as Amount  " &
                    " ,case when TSPL_ADJUSTMENT_HEADER.posted='Y' then 'Y' else 'N' end as [Is Posted],created.User_Name as [Created By],convert(varchar,TSPL_ADJUSTMENT_HEADER.Created_Date,103) as [Created Date], modified.User_Name as [Modify By],convert(varchar, TSPL_ADJUSTMENT_HEADER.Modify_Date,103) as [Modify Date],TSPL_ADJUSTMENT_HEADER.Against_Physical_Stock_No as [Physical Stock No]"
                    If clsCommon.myLen(strCategoryTable) > 0 Then
                        qry += "," + strCodeColumnSelect + "," + strCodeDescColumnSelect
                    End If
                    qry += " FROM  TSPL_ADJUSTMENT_HEADER LEFT OUTER JOIN TSPL_ADJUSTMENT_DETAIL ON TSPL_ADJUSTMENT_HEADER.Adjustment_No = TSPL_ADJUSTMENT_DETAIL.Adjustment_No " &
                    "  left join tspl_user_master created on created.user_code=TSPL_ADJUSTMENT_HEADER.Created_By  left join tspl_user_master modified on modified.user_code=TSPL_ADJUSTMENT_HEADER.Modify_By " &
                    " left outer join TSPL_ITEM_MASTER on TSPL_ADJUSTMENT_DETAIL.Item_Code =TSPL_ITEM_MASTER .Item_Code " &
                    " LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_ADJUSTMENT_HEADER.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code "
                    If clsCommon.myLen(strCategoryTable) > 0 Then
                        qry += " left outer join (" + strCategoryTable + ") as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=TSPL_ADJUSTMENT_DETAIL.Item_Code"
                    End If
                    qry += " where " + strPost + " " &
                    " And TSPL_ADJUSTMENT_HEADER.EntryDateTime >= ( '" & fromdate & "') AND TSPL_ADJUSTMENT_HEADER.EntryDateTime <= ( '" & enddate & "')" + filter + " "
                    qry += " order By  convert(datetime,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) , TSPL_ADJUSTMENT_HEADER.Adjustment_No"
                ElseIf RadioBtnSummary.IsChecked = True Then
                    qry = "select  Item_code,[Description],Itf_code,[Milk Type],UnitCode,MRP,[Main Location],Location,Quantity,InQuantity,OutQuantity,[StartDAte],[EndDate],[StartTime],[EndTime],[FAT KG]," + Environment.NewLine +
                          " case when [FAT Per]<0 then (-1)*[FAT Per] else [FAT Per] end as  [FAT Per],[SNF KG],case when [SNF Per]<0 then (-1)*[SNF Per] else [SNF Per]  end as [SNF Per] " + Environment.NewLine +
                          " ,case when [Unit Cost]<0 then (-1)*[Unit Cost] else [Unit Cost] end as  [Unit Cost],Amount,TSPL_COMPANY_MASTER.Comp_Code as [CompNamae],TSPL_COMPANY_MASTER.Comp_Name as [compname], " + LocNcmpAdd + " as [address],TSPL_COMPANY_MASTER .Logo_Img ,TSPL_COMPANY_MASTER .Logo_Img2  from ( " &
                    " select  max([Milk Type]) as [Milk Type],Item_code,max(Description) as [Description], max(itf_code)  as Itf_code, MAX(Unit_Code) as UnitCode, MAX(mrp) as MRP,([Main Location]) as [Main Location],Max(Location) as Location,sum(Quantity) as  Quantity,sum(InQuantity) as InQuantity,SUM(OutQuantity) as OutQuantity,  max(startdate) as [StartDAte], max(enddate) as [EndDate], max(startTime) as [StartTime], max(endTime) as [EndTime] " +
                    " ,convert(decimal(18,2), sum([FAT KG])) as [FAT KG]" + Environment.NewLine +
                    " ,convert(decimal(18,2),case when sum(Qty_KG)=0 then 0 else sum([FAT KG]) *100/sum(Qty_KG) end) as [FAT Per] " + Environment.NewLine +
                    " ,convert(decimal(18,2), sum([SNF KG])) as [SNF KG]" + Environment.NewLine +
                    " ,convert(decimal(18,2),case when sum(Qty_KG)=0 then 0 else sum([SNF KG]) *100/sum(Qty_KG) end) as [SNF Per] " + Environment.NewLine +
                    " ,convert(decimal(18,2),case when sum(Quantity)=0 then 0 else sum(Amount)/sum(Quantity) end) as [Unit Cost]" + Environment.NewLine +
                    ", sum(Amount)  as Amount " + Environment.NewLine +
                    " from " &
                    " (SELECT case when isnull(TSPL_ADJUSTMENT_HEADER.IsMilkType,0)=1 then 'Y' ELSE 'N' end as [Milk Type],TSPL_ADJUSTMENT_HEADER.Adjustment_No as [Adjustment_No], TSPL_ADJUSTMENT_HEADER.Adjustment_Date as [Adjustment_Date], TSPL_ADJUSTMENT_HEADER.Created_time as [Created_time], TSPL_ADJUSTMENT_DETAIL.Item_Code as [Item_Code],tspl_item_master.itf_code as [itf_code], TSPL_ADJUSTMENT_HEADER.Document_No as [Document_No]," &
                    " case when (TSPL_ADJUSTMENT_DETAIL.Adjustment_Type) IN('BI','QI') then TSPL_ADJUSTMENT_DETAIL.Item_Quantity else -1* TSPL_ADJUSTMENT_DETAIL.Item_Quantity end as [Quantity], " &
                    " case when TSPL_ADJUSTMENT_HEADER.Trans_Type='In' then TSPL_ADJUSTMENT_DETAIL.Item_Quantity else  0 end as [InQuantity],case when TSPL_ADJUSTMENT_HEADER.Trans_Type='Out' then TSPL_ADJUSTMENT_DETAIL.Item_Quantity else  0 end as [OutQuantity],   " &
                    " TSPL_ADJUSTMENT_DETAIL.Item_Description as [Description], CONVERT(date, '" + dtpstart.Value.Date + "', 103) AS [startdate], CONVERT(date, '" + dtpend.Value.Date + "', 103) AS [enddate], substring('" + fromdate + "',12,12) as [startTime], substring('" + enddate + "',12,12) as [endTime] , TSPL_ADJUSTMENT_HEADER.Comp_Code  AS [COMPCODE], TSPL_ADJUSTMENT_DETAIL.Unit_Code, TSPL_ADJUSTMENT_DETAIL.mrp,TSPL_ADJUSTMENT_HEADER.MainLocationCode as [Main Location],TSPL_ADJUSTMENT_HEADER.Loc_Code as Location " &
                    " ,(TSPL_ADJUSTMENT_DETAIL.SNF_KG * case when TSPL_ADJUSTMENT_HEADER.Trans_Type='Out' then -1 else 1 end) as [SNF KG], " + Environment.NewLine +
                    " TSPL_ADJUSTMENT_DETAIL.SNF_Pers, " + Environment.NewLine +
                    " (TSPL_ADJUSTMENT_DETAIL.FAT_KG * case when TSPL_ADJUSTMENT_HEADER.Trans_Type='Out' then  -1  else 1 end)  as [FAT KG]" + Environment.NewLine +
                    " ,TSPL_ADJUSTMENT_DETAIL.FAT_Pers,  " + Environment.NewLine +
                    " TSPL_ADJUSTMENT_DETAIL.Item_Cost * case when TSPL_ADJUSTMENT_HEADER.Trans_Type='Out' then  -1  else 1 end   as Amount " + Environment.NewLine +
                    " ,(convert(decimal(18,2), (case when FAT_Pers=0 then 0 else  FAT_KG*100/FAT_Pers end))* case when TSPL_ADJUSTMENT_HEADER.Trans_Type='Out' then -1 else 1 end) as Qty_KG " + Environment.NewLine +
                    " FROM  TSPL_ADJUSTMENT_HEADER " &
                                       " LEFT OUTER JOIN TSPL_ADJUSTMENT_DETAIL ON TSPL_ADJUSTMENT_HEADER.Adjustment_No = TSPL_ADJUSTMENT_DETAIL.Adjustment_No left outer join TSPL_ITEM_MASTER on TSPL_ADJUSTMENT_DETAIL.Item_Code =TSPL_ITEM_MASTER.Item_Code where " + strPost + " " &
                                       " and TSPL_ADJUSTMENT_HEADER.EntryDateTime >= ('" + fromdate + "') AND " &
                                       " TSPL_ADJUSTMENT_HEADER.EntryDateTime <=  ('" + enddate + "') " + filter + " " &
                                       " )xxx" &
                                       " GROUP by Item_code ,[Main Location] " &
                                       " )xxxx " &
                                       " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code ='" + objCommonVar.CurrentCompanyCode + "'" &
                                       " order By Created_Date "

                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            qry = ""
        End Try

        Return qry
    End Function


    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    'Sub LoadCategory()
    '    'dtCategory = clsDBFuncationality.GetDataTable("select ITEM_CATEGORY_CODE AS CodeColumn,ITEM_CATEGORY_CODE+'DESC' as CodeDescColumn,DESCRIPTION as DescColumn  from TSPL_ITEM_CATEGORY_LEVEL order by CATEGORY_LEVEL")

    '    cbgtype.DataSource = Nothing
    '    Dim qry As String = "select cast( 0 as bit) as SEL,ITEM_CATEGORY_CODE AS Code,DESCRIPTION as NAME from TSPL_ITEM_CATEGORY_LEVEL ORDER BY CATEGORY_LEVEL"
    '    cbgtype.DataSource = clsDBFuncationality.GetDataTable(qry)

    '    cbgtype.Columns("SEL").ReadOnly = False
    '    cbgtype.Columns("SEL").Width = 30
    '    cbgtype.Columns("SEL").HeaderText = " "

    '    cbgtype.Columns("CODE").ReadOnly = True
    '    cbgtype.Columns("CODE").Width = 100
    '    cbgtype.Columns("CODE").HeaderText = "Code"

    '    cbgtype.Columns("NAME").ReadOnly = True
    '    cbgtype.Columns("NAME").Width = 200
    '    cbgtype.Columns("NAME").HeaderText = "Description"

    '    cbgtype.ShowGroupPanel = False
    '    cbgtype.AllowAddNewRow = False
    '    cbgtype.AllowColumnReorder = False
    '    cbgtype.AllowRowReorder = False
    '    cbgtype.EnableSorting = False
    '    cbgtype.ShowFilteringRow = True
    '    cbgtype.EnableFiltering = True
    '    cbgtype.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
    '    cbgtype.MasterTemplate.ShowRowHeaderColumn = True

    'End Sub

    Sub LoadType()
        Dim dt As DataTable = New DataTable()
        'dt = GetItemType()
        '' Dim qry As String = "select CUST_CATEGORY_CODE,CUST_CATEGORY_DESC from TSPL_CUSTOMER_CATEGORY_MASTER order by CUST_CATEGORY_CODE "
        ''cbgtype.DataSource = clsDBFuncationality.GetDataTable(qry)

        'cbgtype.DataSource = dt
        'cbgtype.ValueMember = "Code"
        'cbgtype.DisplayMember = "Code"
        Dim qry As String = "select ITEM_CATEGORY_CODE AS Code,DESCRIPTION as NAME from TSPL_ITEM_CATEGORY_LEVEL ORDER BY CATEGORY_LEVEL "
        'cbgtype.DataSource = clsDBFuncationality.GetDataTable(qry)
        dt = clsDBFuncationality.GetDataTable(qry)
        cbgtype.DataSource = dt
        cbgtype.ValueMember = "Code"
        cbgtype.DisplayMember = "NAME"
    End Sub
    Public Sub LoadLocation()
        'Dim Qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        'cbgLocation.DataSource = clsDBFuncationality.GetDataTable(Qry)
        'cbgLocation.ValueMember = "Code"
        'cbgLocation.DisplayMember = "Description"
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub

    Sub LoadItem()
        Dim qry As String = " select item_code ,item_Desc  from TSPL_ITEM_MASTER order by Item_Code "
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem.ValueMember = "item_code"
        cbgItem.DisplayMember = "item_Desc"
    End Sub

    Public Shared Function GetItemType() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "EC"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "FC"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "EB"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "FB"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "SH"
        dt.Rows.Add(dr)


        Return dt
    End Function
    Private Sub chktypeAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chktypeAll.ToggleStateChanged
        cbgtype.Enabled = Not chktypeAll.IsChecked
    End Sub
    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgLocation.Enabled = False
    End Sub
    Private Sub chkLocationSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationSelect.ToggleStateChanged
        cbgLocation.Enabled = True
    End Sub

    Private Sub FrmStockAdjustmentReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.Control And e.KeyCode = Keys.P Then
            btnprint.PerformClick()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
                reset()
        End If
    End Sub

    
    Private Sub chkIemAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkIemAll.ToggleStateChanged
        cbgItem.Enabled = Not chkIemAll.IsChecked
    End Sub

    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        Try
            PageSetupReport_ID = MyBase.Form_ID + IIf(RadioBtnSummary.IsChecked = True, "S", "D")
            TemplateGridview = gv1
            strSql = funPrint()
            If clsCommon.myLen(strSql) > 0 Then
                dt = clsDBFuncationality.GetDataTable(strSql)
                gv1.DataSource = Nothing
                gv1.Columns.Clear()
                gv1.Rows.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()

                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                    Exit Sub
                End If
                gv1.DataSource = dt
                SetGridFormationOFGV1()
                ReStoreGridLayout()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub SetGridFormationOFGV1()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = False
        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()

        If RadioBtnSummary.IsChecked Then
            gv1.Columns("Milk Type").IsVisible = True
            gv1.Columns("Milk Type").Width = 100
            gv1.Columns("Milk Type").HeaderText = "Milk Type"

            gv1.Columns("Item_code").IsVisible = True
            gv1.Columns("Item_code").Width = 100
            gv1.Columns("Item_code").HeaderText = "Item code"

            gv1.Columns("Description").IsVisible = True
            gv1.Columns("Description").Width = 350
            gv1.Columns("Description").HeaderText = "Item Description"

            gv1.Columns("itf_code").IsVisible = True
            gv1.Columns("itf_code").Width = 80
            gv1.Columns("itf_code").HeaderText = "ITF Code"

            gv1.Columns("InQuantity").IsVisible = True
            gv1.Columns("InQuantity").Width = 100
            gv1.Columns("InQuantity").HeaderText = "In Quantity"

            gv1.Columns("OutQuantity").IsVisible = True
            gv1.Columns("OutQuantity").Width = 100
            gv1.Columns("OutQuantity").HeaderText = "Out Quantity"

            gv1.Columns("Quantity").IsVisible = True
            gv1.Columns("Quantity").Width = 100
            gv1.Columns("Quantity").HeaderText = "Quantity"


            gv1.Columns("UnitCode").IsVisible = True
            gv1.Columns("UnitCode").Width = 100
            gv1.Columns("UnitCode").HeaderText = "Unit Code"

            gv1.Columns("MRP").IsVisible = True
            gv1.Columns("MRP").Width = 100
            gv1.Columns("MRP").HeaderText = "MRP"

            gv1.Columns("Main Location").IsVisible = True
            gv1.Columns("Main Location").Width = 100
            gv1.Columns("Main Location").HeaderText = "Main Location"

            gv1.Columns("Location").IsVisible = True
            gv1.Columns("Location").Width = 100
            gv1.Columns("Location").HeaderText = "Location"

            gv1.Columns("SNF KG").IsVisible = True
            gv1.Columns("SNF KG").Width = 80
            gv1.Columns("SNF KG").HeaderText = "SNF KG"

            gv1.Columns("SNF Per").IsVisible = True
            gv1.Columns("SNF Per").Width = 80
            gv1.Columns("SNF Per").HeaderText = "SNF Per"

            gv1.Columns("FAT KG").IsVisible = True
            gv1.Columns("FAT KG").Width = 80
            gv1.Columns("FAT KG").HeaderText = "FAT KG"

            gv1.Columns("FAT Per").IsVisible = True
            gv1.Columns("FAT Per").Width = 80
            gv1.Columns("FAT Per").HeaderText = "FAT Per"

            gv1.Columns("Unit Cost").IsVisible = True
            gv1.Columns("Unit Cost").Width = 80
            gv1.Columns("Unit Cost").HeaderText = "Unit Cost"

            gv1.Columns("Amount").IsVisible = True
            gv1.Columns("Amount").Width = 80
            gv1.Columns("Amount").HeaderText = "Amount"

            Dim item10 As New GridViewSummaryItem("Quantity", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item10)

            'Dim item11 As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(item11)

        Else
            gv1.Columns("In/Out").IsVisible = True
            gv1.Columns("In/Out").Width = 80
            gv1.Columns("In/Out").HeaderText = "In/Out"

            gv1.Columns("Is Posted").IsVisible = True
            gv1.Columns("Is Posted").Width = 100
            gv1.Columns("Is Posted").HeaderText = "Is Posted"

            gv1.Columns("Milk Type").IsVisible = True
            gv1.Columns("Milk Type").Width = 100
            gv1.Columns("Milk Type").HeaderText = "Milk Type"

            gv1.Columns("Adjustment Type").IsVisible = True
            gv1.Columns("Adjustment Type").Width = 100
            gv1.Columns("Adjustment Type").HeaderText = "Adjustment Type"

            gv1.Columns("Created By").IsVisible = True
            gv1.Columns("Created By").Width = 100
            gv1.Columns("Created By").HeaderText = "Created By"

            gv1.Columns("Created Date").IsVisible = True
            gv1.Columns("Created Date").Width = 100
            gv1.Columns("Created Date").HeaderText = "Created Date"

            gv1.Columns("Modify By").IsVisible = True
            gv1.Columns("Modify By").Width = 100
            gv1.Columns("Modify By").HeaderText = "Modify By"

            gv1.Columns("Modify Date").IsVisible = True
            gv1.Columns("Modify Date").Width = 100
            gv1.Columns("Modify Date").HeaderText = "Modify Date"

            gv1.Columns("Adjustment_Date").IsVisible = True
            gv1.Columns("Adjustment_Date").Width = 80
            gv1.Columns("Adjustment_Date").HeaderText = "Adjustment Date"

            'gv1.Columns("Created_time").IsVisible = True
            'gv1.Columns("Created_time").Width = 80
            'gv1.Columns("Created_time").HeaderText = "time"

            gv1.Columns("Adjustment_No").IsVisible = True
            gv1.Columns("Adjustment_No").Width = 80
            gv1.Columns("Adjustment_No").HeaderText = "Adjustment No"

            gv1.Columns("DocumentNo").IsVisible = True
            gv1.Columns("DocumentNo").Width = 80
            gv1.Columns("DocumentNo").HeaderText = "Description"

            gv1.Columns("Item_Code").IsVisible = True
            gv1.Columns("Item_Code").Width = 80
            gv1.Columns("Item_Code").HeaderText = "Item Code"

            gv1.Columns("itf_code").IsVisible = True
            gv1.Columns("itf_code").Width = 80
            gv1.Columns("itf_code").HeaderText = "ITF Code"

            gv1.Columns("Description").IsVisible = True
            gv1.Columns("Description").Width = 80
            gv1.Columns("Description").HeaderText = "Item Description"

            gv1.Columns("mrp").IsVisible = True
            gv1.Columns("mrp").Width = 80
            gv1.Columns("mrp").HeaderText = "MRP"

            gv1.Columns("Unit_Code").IsVisible = True
            gv1.Columns("Unit_Code").Width = 80
            gv1.Columns("Unit_Code").HeaderText = "Unit"

            gv1.Columns("InOutQuantity").IsVisible = True
            gv1.Columns("InOutQuantity").Width = 150
            gv1.Columns("InOutQuantity").HeaderText = "Quantity"

           
            gv1.Columns("SNF KG").IsVisible = True
            gv1.Columns("SNF KG").Width = 80
            gv1.Columns("SNF KG").HeaderText = "SNF KG"

            gv1.Columns("SNF Per").IsVisible = True
            gv1.Columns("SNF Per").Width = 80
            gv1.Columns("SNF Per").HeaderText = "SNF Per"

            gv1.Columns("FAT KG").IsVisible = True
            gv1.Columns("FAT KG").Width = 80
            gv1.Columns("FAT KG").HeaderText = "FAT KG"

            gv1.Columns("FAT Per").IsVisible = True
            gv1.Columns("FAT Per").Width = 80
            gv1.Columns("FAT Per").HeaderText = "FAT Per"

            gv1.Columns("Unit_Cost").IsVisible = True
            gv1.Columns("Unit_Cost").Width = 80
            gv1.Columns("Unit_Cost").HeaderText = "Unit Cost"

            gv1.Columns("Amount").IsVisible = True
            gv1.Columns("Amount").Width = 80
            gv1.Columns("Amount").HeaderText = "Amount"

            
        End If

        Dim item1 As New GridViewSummaryItem("InQuantity", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("OutQuantity", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("SNF KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        'Dim item4 As New GridViewSummaryItem("SNF Per", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("FAT KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        'Dim item6 As New GridViewSummaryItem("FAT Per", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item6)
        'Dim item7 As New GridViewSummaryItem("Unit Cost", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item7)
        Dim item8 As New GridViewSummaryItem("InOutQuantity", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)

        Dim item9 As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item9)
        
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        gv1.EnableFiltering = True
        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub

    Private Sub gv1_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellDoubleClick
        If RadioBtnDetail.IsChecked Then
            If clsCommon.myLen(gv1.CurrentRow.Cells("Adjustment_No").Value) > 0 Then
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnStoreAdjustment, gv1.CurrentRow.Cells("Adjustment_No").Value)

            End If
        End If

    End Sub

    Private Sub Print(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(dtpstart.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpend.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.mbtnStockAdjustmentReport & "'"))
            If chkLocationSelect.IsChecked Then
                If cbgLocation.CheckedDisplayMember IsNot Nothing AndAlso cbgLocation.CheckedDisplayMember.Count > 0 Then
                    arrHeader.Add(RadGroupBox13.Text + " : " + clsCommon.GetMulcallStringWithComma(cbgLocation.CheckedDisplayMember))
                End If
            End If

            If chkItemSelect.IsChecked Then
                If cbgItem.CheckedDisplayMember IsNot Nothing AndAlso cbgItem.CheckedDisplayMember.Count > 0 Then
                    arrHeader.Add(RadGroupBox2.Text + " : " + clsCommon.GetMulcallStringWithComma(cbgItem.CheckedDisplayMember))
                End If
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
                clsCommon.MyExportToPDF("Stock adjustment", gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                gv1.MasterTemplate.FilterDescriptors.Clear()
                Dim obj As New clsGridLayout()
                obj.ReportID = PageSetupReport_ID
                obj.UserID = objCommonVar.CurrentUserCode
                obj.GridLayout = New MemoryStream()
                gv1.SaveLayout(obj.GridLayout)
                obj.GridColumns = gv1.ColumnCount
                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                If obj.SaveData() Then
                    common.clsCommon.MyMessageBoxShow("Layout Saved Successfully", Me.Text)
                End If
                ''stuti regarding memory leakage
                obj.GridLayout.Close()
                obj.GridLayout.Dispose()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        Try
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            clsGridLayout.DeleteData(obj.ReportID, objCommonVar.CurrentUserCode)
            common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Print(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Print(EnumExportTo.PDF)
    End Sub
End Class
