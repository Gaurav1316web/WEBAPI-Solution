'' created by Richa Agarwal against ticket no  UDL/09/06/21-001020,UDL/16/06/21-001021
Imports common
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Public Class frmInventoryAgeingReportNew
    Inherits FrmMainTranScreen
#Region "variables"
    Dim dtCategory As DataTable
    Dim isDataLoad As Boolean = False
    Public asofdt As Date
    Public cuttoff As Date
    Public strType As String
    Public arrLocation As ArrayList
    Public arrItem As ArrayList
    Public arrCategory As Array
    Dim IsDrillDown As Boolean = False
    Dim BackProcess As Boolean = False
    Dim stritemcode As String = String.Empty
#End Region
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        radbtnBulkExp.Visible = MyBase.isQuickExportFlag
    End Sub
    Private Sub frmInventoryAgeingReportNew_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        SetUserMgmtNew()
        LoadUnit()
        LoadLocation()
        rbtnLocationAll.IsChecked = True
        radbtnBulkExp.Visible = False
        btnBack.Enabled = False
        txtToDate.Value = clsCommon.GETSERVERDATE()
    End Sub

    Sub LoadLocation()
        gvLocation.DataSource = Nothing

        ''===============when WIP then only section/sublocation seen in location finders=====24/03/2017==========================
        Dim whrCls As String = " and ((Is_Section='N' and Is_Sub_Location='N' and Location_Type IN ('Physical','Logical','Virtual') ) or (CSA_Type='Y') )"

        ''====================================================================================================================
        'If chkGITLocation.Checked = False Then
        '    whrCls += "  and isnull(TSPL_LOCATION_MASTER.GIT_Type,'') <>'Y'"
        'End If
        Dim qry As String = " select cast( 0 as bit) as SEL,Location_Code as CODE,Location_Desc as NAME from TSPL_LOCATION_MASTER where 1=1 " + whrCls + ""

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

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click

        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        gv1.GroupDescriptors.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        RadGroupBox3.Enabled = True
        RadPageView1.SelectedPage = RadPageViewPage3
        IsDrillDown = False
        BackProcess = False
        btnBack.Enabled = False
        chkBatchWise.Checked = False
    End Sub
    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add(("Date Range:  To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where PROGRAM_CODE='" & clsUserMgtCode.frmInventoryAgeingReport & "'"))
            If txtItemType.arrDispalyMember IsNot Nothing AndAlso txtItemType.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Item Type : " + clsCommon.GetMulcallStringWithComma(txtItemType.arrDispalyMember))
            End If
            If txtItem.arrDispalyMember IsNot Nothing AndAlso txtItem.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember))
            End If
            If exporter = EnumExportTo.Excel Then
                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
            Else
                clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        GetData()
    End Sub
    Public Sub GetData()
        Try
            clsCommon.ProgressBarShow()
            'If clsCommon.CompairString(cmbUnit.SelectedValue, "") = CompairStringResult.Equal Then
            '    clsCommon.MyMessageBoxShow("Plz select UOM")
            '    Exit Sub
            'End If


            Dim dtLoc As New DataTable()
            clsCommon.ProgressBarUpdate("Loading Data.Please Wait...")
            Dim Location As String = String.Empty
            Dim objFilter As New clsStockAgeingFilters
            objFilter.UOM_Code = cmbUnit.SelectedValue
            objFilter.arrLocation = New List(Of clsCode)
            objFilter.arrLoc = New ArrayList
            objFilter.AsOnDate = clsCommon.GetPrintDate(txtToDate.Value, "dd-MMM-yyyy")
            objFilter.arrItem = txtItem.arrValueMember
            objFilter.arrItemType = txtItemType.arrValueMember
            objFilter.SelectLocation = rbtnLocationSelect.IsChecked
            Dim arrBucket As New ArrayList
            arrBucket.Add(clsCommon.myCdbl(txt1.Text))
            arrBucket.Add(clsCommon.myCdbl(txt2.Text))
            arrBucket.Add(clsCommon.myCdbl(txt3.Text))
            arrBucket.Add(clsCommon.myCdbl(txt4th.Text))
            objFilter.arrAgeingBucket = arrBucket

            For intLoc As Integer = 0 To gvLocation.RowCount - 1
                Dim loc As New clsCode
                If Not rbtnLocationSelect.IsChecked Then
                    loc.Sel = True
                Else
                    loc.Sel = gvLocation.Rows(intLoc).Cells("Sel").Value
                End If

                loc.Code = gvLocation.Rows(intLoc).Cells("Code").Value
                loc.Desc = gvLocation.Rows(intLoc).Cells("Name").Value
                loc.arrOut = gvLocation.Rows(intLoc).Tag
                objFilter.arrLocation.Add(loc)
                If loc.Sel Then
                    objFilter.arrLoc.Add(loc.Code)
                End If
            Next
            Dim qry As String = String.Empty
            If chkBatchWise.Checked Then
                qry = GetBaseQryStockAgeingForBatchItem(objFilter, False, False)
            Else
                qry = GetBaseQryStockAgeing(objFilter, False, False)
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt.Rows.Count > 0 Then
                gv1.DataSource = Nothing
                'gv1.Rows.Clear()
                gv1.Columns.Clear()
                gv1.DataSource = dt
                For Each col As GridViewColumn In gv1.Columns
                    col.Width = 100
                Next
                gv1.AllowAddNewRow = False
                gv1.AllowDeleteRow = False
                gv1.EnableFiltering = True
                gv1.ShowGroupedColumns = False
                gv1.ReadOnly = True
                gv1.ShowGroupPanel = False

                SetGridFormationOFGV1()
                RadPageView1.SelectedPage = RadPageViewPage2
                RadGroupBox3.Enabled = False
            Else
                gv1.DataSource = Nothing
                gv1.Rows.Clear()
                gv1.Columns.Clear()
                clsCommon.MyMessageBoxShow("Data Not Found")
            End If
            FindAndRestoreGridLayout(Me, gv1)
            BackProcess = False
            IsDrillDown = False
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        Finally
            clsCommon.ProgressBarHide()
        End Try
    End Sub
    Public Shared Function GetBaseQryStockAgeing(ByVal objFilter As clsStockAgeingFilters, ByVal isGITLocation As Boolean, ByVal isShelfLifeWise As Boolean) As String
        Dim arrOutput As New ArrayList
        Dim qry As String = ""
        Try
            Dim ItemFilter As String = ""
            Dim locFilter As String = ""
            Dim ItemTypeFilter As String = ""
            If Not objFilter.arrItem Is Nothing AndAlso objFilter.arrItem.Count > 0 Then
                ItemFilter = "(" & clsCommon.GetMulcallString(objFilter.arrItem) & ")"
            End If
            If Not objFilter.arrItemType Is Nothing AndAlso objFilter.arrItemType.Count > 0 Then
                ItemTypeFilter = " and tspl_item_master.Item_Type in (" & clsCommon.GetMulcallString(objFilter.arrItemType) & ")"
            End If
            If objFilter.arrAgeingBucket Is Nothing OrElse objFilter.arrAgeingBucket.Count <= 0 Then
                Throw New Exception("Ageing Bucket can not be blank")
            ElseIf objFilter.arrAgeingBucket.Count < 4 Then
                Throw New Exception("Ageing Bucket size must not be less than 4")
            ElseIf objFilter.arrAgeingBucket.Count > 4 Then
                Throw New Exception("Ageing Bucket size must not be greater than 4")
            End If
            Dim bucket1 As Decimal = 0
            Dim bucket2 As Decimal = 0
            Dim bucket3 As Decimal = 0
            Dim bucket4 As Decimal = 0
            bucket1 = clsCommon.myCdbl(objFilter.arrAgeingBucket(0))
            bucket2 = clsCommon.myCdbl(objFilter.arrAgeingBucket(1))
            bucket3 = clsCommon.myCdbl(objFilter.arrAgeingBucket(2))
            bucket4 = clsCommon.myCdbl(objFilter.arrAgeingBucket(3))
            If bucket1 > bucket2 Then
                Throw New Exception("Ageing Bucket 1 Value must be less than Bucket 2 value")
            End If
            If bucket2 > bucket3 Then
                Throw New Exception("Ageing Bucket 2 Value must be less than Bucket 3 value")
            End If
            If bucket3 > bucket4 Then
                Throw New Exception("Ageing Bucket 3 Value must be less than Bucket 4 value")
            End If

            Dim bucket11 As Decimal = bucket1 + 1
            Dim bucket21 As Decimal = bucket2 + 1
            Dim bucket31 As Decimal = bucket3 + 1

            Dim dtCategory As DataTable = clsDBFuncationality.GetDataTable("select ITEM_CATEGORY_CODE AS CodeColumn,ITEM_CATEGORY_CODE+'DESC' as CodeDescColumn,DESCRIPTION as DescColumn  from TSPL_ITEM_CATEGORY_LEVEL order by CATEGORY_LEVEL")
            Dim strCategoryTable As String = ""

            If objFilter.SelectLocation = True Then
                Dim IsApplicable As Boolean = False
                For ii As Integer = 0 To objFilter.arrLocation.Count - 1
                    If clsCommon.myCBool(objFilter.arrLocation(ii).Sel) Then
                        If IsApplicable Then
                            locFilter += " Or "
                        End If
                        locFilter += " ( (case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then Inv.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end) = '" + clsCommon.myCstr(objFilter.arrLocation(ii).Code) + "') "
                        IsApplicable = True
                        Dim arr As Dictionary(Of String, Object) = objFilter.arrLocation(ii).arrOut
                        If arr IsNot Nothing AndAlso arr.Count > 0 Then
                            locFilter += " and Inv.Location_Code in ("
                            Dim isFirstTime As Boolean = True
                            For Each strInn As String In arr.Keys
                                If Not isFirstTime Then
                                    locFilter += ","
                                End If
                                locFilter += "'" + strInn + "'"
                                isFirstTime = False
                            Next
                            locFilter += ")"
                        End If
                    End If
                Next
                If Not IsApplicable Then
                    Throw New Exception("Please select at least one location")
                End If
            End If


            qry = "select Item_Code ,max(Item_Desc) as Item_Desc,location_Code,sum(qty) as Qty,sum([0-" & (bucket1) & " Qty]) as [0-" & (bucket1) & " Qty],sum([" & (bucket1 + 1) & "-" & (bucket2) & " Qty]) as [" & (bucket1 + 1) & "-" & (bucket2) & " Qty], sum([" & (bucket2 + 1) & "-" & (bucket3) & " Qty]) as [" & (bucket2 + 1) & "-" & (bucket3) & " Qty],sum([" & (bucket3 + 1) & "-" & (bucket4) & " Qty]) as [" & (bucket3 + 1) & "-" & (bucket4) & " Qty],sum([Over " & (bucket4) & " Qty]) as [Over " & (bucket4) & " Qty]  From (select Item_Code,Item_Desc,location_Code,Punching_Date,Trans_Type,trans_age ,finalQty as qty,
    CASE WHEN trans_age<" & (bucket1 + 1) & " THEN finalQty else 0  END [0-" & (bucket1) & " Qty],
    CASE WHEN trans_age>" & (bucket1) & " and trans_age<" & (bucket2 + 1) & " THEN finalQty else 0  END [" & (bucket1 + 1) & "-" & (bucket2) & " Qty],
    CASE WHEN trans_age>" & (bucket2) & " and trans_age<" & (bucket3 + 1) & " THEN finalQty else 0  END [" & (bucket2 + 1) & "-" & (bucket3) & " Qty],
    CASE WHEN trans_age>" & (bucket3) & " and trans_age<" & (bucket4 + 1) & " THEN finalQty else 0  END [" & (bucket3 + 1) & "-" & (bucket4) & " Qty],
    CASE WHEN trans_age>" & (bucket4) & " THEN finalQty else 0  END [Over " & (bucket4) & " Qty]
    from(Select Item_Code,Item_Desc,Punching_Date,Trans_Type,trans_age ,case when qty>finalQty  then convert(decimal(18,2),finalQty) else convert(decimal(18,2),qty) end as finalQty,location_Code from  (select *,SUM (qty) OVER(PARTITION BY Item_Code,location_Code ORDER BY rowno asc ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) as finalQty
    from (
    ------------ in Query
    SELECT Inv.Item_Code,tspl_item_master.Item_Desc , convert(date,Punching_Date,103) As Punching_Date, trans_type,(CASE WHEN InOut='I' THEN 1 ELSE -1 END) * Inv.Stock_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor as  qty,
    SUM ((CASE WHEN InOut='I' THEN 1 ELSE -1 END) * Inv.Stock_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor) OVER (PARTITION BY tspl_item_master.Item_Desc,Inv.location_Code
    ORDER BY Punching_Date ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS CurrentQtySum,
    DATEDIFF (DAY, Punching_Date, '" & clsCommon.GetPrintDate(objFilter.AsOnDate, "dd-MMM-yyyy") & "') AS trans_age,row_number() over (order by Inv.Item_Code,convert(date, Inv.punching_date,101)  ) as RowNo,Inv.location_Code
    FROM TSPL_INVENTORY_MOVEMENT as Inv
    left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =Inv.Location_Code
    left outer join tspl_item_master on tspl_item_master.item_code=Inv.Item_code
    inner join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=Inv.Item_Code " & If(clsCommon.myLen(objFilter.UOM_Code) > 0, " and TSPL_ITEM_UOM_DETAIL.UOM_Code= '" & objFilter.UOM_Code & "' ", " AND TSPL_ITEM_UOM_DETAIL.Stocking_unit='Y' ") & " 
    WHERE InOut='I' " & ItemTypeFilter & " and cast(Punching_Date as date)<='" & clsCommon.GetPrintDate(objFilter.AsOnDate, "dd-MMM-yyyy") & "' " & If(clsCommon.myLen(ItemFilter) > 0, " and Inv.Item_Code in " & ItemFilter & "", "") & " " & If(clsCommon.myLen(locFilter) > 0, " and " & locFilter & " ", "") & "
    union all
    ---------Out Query
    SELECT Inv.Item_Code, max(tspl_item_master.Item_Desc ) as Item_Desc,  convert(date,min(Punching_Date),103) As Punching_Date, max(trans_type) as trans_type,
    SUM ((CASE WHEN InOut='I' THEN 1 ELSE -1 END) * Inv.Stock_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor) 
    AS InQty, 0 AS CurrentQtySum,0 AS trans_age,0 as rowno,Inv.location_Code FROM TSPL_INVENTORY_MOVEMENT as Inv
    left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =Inv.Location_Code
    left outer join tspl_item_master on tspl_item_master.item_code=Inv.Item_code
    inner join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=Inv.Item_Code " & If(clsCommon.myLen(objFilter.UOM_Code) > 0, " and TSPL_ITEM_UOM_DETAIL.UOM_Code= '" & objFilter.UOM_Code & "' ", " AND TSPL_ITEM_UOM_DETAIL.Stocking_unit='Y' ") & " 
    WHERE InOut='O' " & ItemTypeFilter & " and cast(Punching_Date as date)<='" & clsCommon.GetPrintDate(objFilter.AsOnDate, "dd-MMM-yyyy") & "' " & If(clsCommon.myLen(ItemFilter) > 0, " and Inv.Item_Code in " & ItemFilter & "", "") & " " & If(clsCommon.myLen(locFilter) > 0, " and " & locFilter & "", "") & "
    group by Inv.Item_Code ,Inv.Location_Code
    union all
    ------------ in Query
    SELECT Inv.Item_Code,tspl_item_master.Item_Desc , convert(date,Punching_Date,103) As Punching_Date, trans_type,(CASE WHEN InOut='I' THEN 1 ELSE -1 END) * Inv.Stock_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor as  qty,
    SUM ((CASE WHEN InOut='I' THEN 1 ELSE -1 END) * Inv.Stock_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor) OVER (PARTITION BY tspl_item_master.Item_Desc, Inv.location_Code
    ORDER BY Punching_Date ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS CurrentQtySum,
    DATEDIFF (DAY, Punching_Date, '" & clsCommon.GetPrintDate(objFilter.AsOnDate, "dd-MMM-yyyy") & "') AS trans_age,row_number() over (order by Inv.Item_Code,convert(date, Inv.punching_date,101)  ) as RowNo, Inv.location_Code
    FROM TSPL_INVENTORY_MOVEMENT_New as Inv
    left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =Inv.Location_Code
    left outer join tspl_item_master on tspl_item_master.item_code=Inv.Item_code
    inner join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=Inv.Item_Code " & If(clsCommon.myLen(objFilter.UOM_Code) > 0, " and TSPL_ITEM_UOM_DETAIL.UOM_Code= '" & objFilter.UOM_Code & "'", " AND TSPL_ITEM_UOM_DETAIL.Stocking_unit='Y' ") & " 
    WHERE InOut='I' " & ItemTypeFilter & " and cast(Punching_Date as date)<='" & clsCommon.GetPrintDate(objFilter.AsOnDate, "dd-MMM-yyyy") & "' " & If(clsCommon.myLen(ItemFilter) > 0, " and Inv.Item_Code in " & ItemFilter & "", "") & " " & If(clsCommon.myLen(locFilter) > 0, " and  " & locFilter & "", "") & "
    union all
    ---------Out Query
    SELECT Inv.Item_Code, max(tspl_item_master.Item_Desc ) as Item_Desc,  convert(date,min(Punching_Date),103) As Punching_Date, max(trans_type) as trans_type,
    SUM ((CASE WHEN InOut='I' THEN 1 ELSE -1 END) * Inv.Stock_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor) 
    AS InQty, 0 AS CurrentQtySum,0 AS trans_age,0 as rowno, Inv.location_Code FROM TSPL_INVENTORY_MOVEMENT_New as Inv
    left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =Inv.Location_Code
    left outer join tspl_item_master on tspl_item_master.item_code=Inv.Item_code
    inner join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=Inv.Item_Code  " & If(clsCommon.myLen(objFilter.UOM_Code) > 0, " and TSPL_ITEM_UOM_DETAIL.UOM_Code= '" & objFilter.UOM_Code & "'", " AND TSPL_ITEM_UOM_DETAIL.Stocking_unit='Y' ") & " 
    WHERE InOut='O' " & ItemTypeFilter & " AND cast(Punching_Date as date)<='" & clsCommon.GetPrintDate(objFilter.AsOnDate, "dd-MMM-yyyy") & "' " & If(clsCommon.myLen(ItemFilter) > 0, " and Inv.Item_Code in " & ItemFilter & "", "") & " " & If(clsCommon.myLen(locFilter) > 0, " and " & locFilter & "", "") & "
    group by Inv.Item_Code ,Inv.Location_Code 
    ) Final
    )FinalInv where finalQty >=0 )FinalQryQty
    )OuterQry group by Item_Code ,Location_Code"

            Return qry
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try


    End Function
    Public Shared Function GetBaseQryStockAgeingForBatchItem(ByVal objFilter As clsStockAgeingFilters, ByVal isGITLocation As Boolean, ByVal isShelfLifeWise As Boolean) As String
        Dim arrOutput As New ArrayList
        Dim qry As String = ""
        Try
            Dim ItemFilter As String = ""
            Dim ItemTypeFilter As String = ""
            Dim locFilter As String = ""
            If Not objFilter.arrItem Is Nothing AndAlso objFilter.arrItem.Count > 0 Then
                ItemFilter = "(" & clsCommon.GetMulcallString(objFilter.arrItem) & ")"
            End If

            If Not objFilter.arrItemType Is Nothing AndAlso objFilter.arrItemType.Count > 0 Then
                ItemTypeFilter = " and tspl_item_master.Item_Type in (" & clsCommon.GetMulcallString(objFilter.arrItemType) & ")"
            End If

            If objFilter.arrAgeingBucket Is Nothing OrElse objFilter.arrAgeingBucket.Count <= 0 Then
                Throw New Exception("Ageing Bucket can not be blank")
            ElseIf objFilter.arrAgeingBucket.Count < 4 Then
                Throw New Exception("Ageing Bucket size must not be less than 4")
            ElseIf objFilter.arrAgeingBucket.Count > 4 Then
                Throw New Exception("Ageing Bucket size must not be greater than 4")
            End If
            Dim bucket1 As Decimal = 0
            Dim bucket2 As Decimal = 0
            Dim bucket3 As Decimal = 0
            Dim bucket4 As Decimal = 0
            bucket1 = clsCommon.myCdbl(objFilter.arrAgeingBucket(0))
            bucket2 = clsCommon.myCdbl(objFilter.arrAgeingBucket(1))
            bucket3 = clsCommon.myCdbl(objFilter.arrAgeingBucket(2))
            bucket4 = clsCommon.myCdbl(objFilter.arrAgeingBucket(3))
            If bucket1 > bucket2 Then
                Throw New Exception("Ageing Bucket 1 Value must be less than Bucket 2 value")
            End If
            If bucket2 > bucket3 Then
                Throw New Exception("Ageing Bucket 2 Value must be less than Bucket 3 value")
            End If
            If bucket3 > bucket4 Then
                Throw New Exception("Ageing Bucket 3 Value must be less than Bucket 4 value")
            End If

            Dim bucket11 As Decimal = bucket1 + 1
            Dim bucket21 As Decimal = bucket2 + 1
            Dim bucket31 As Decimal = bucket3 + 1

            Dim dtCategory As DataTable = clsDBFuncationality.GetDataTable("select ITEM_CATEGORY_CODE AS CodeColumn,ITEM_CATEGORY_CODE+'DESC' as CodeDescColumn,DESCRIPTION as DescColumn  from TSPL_ITEM_CATEGORY_LEVEL order by CATEGORY_LEVEL")
            Dim strCategoryTable As String = ""

            If objFilter.SelectLocation = True Then
                Dim IsApplicable As Boolean = False
                For ii As Integer = 0 To objFilter.arrLocation.Count - 1
                    If clsCommon.myCBool(objFilter.arrLocation(ii).Sel) Then
                        If IsApplicable Then
                            locFilter += " Or "
                        End If
                        locFilter += " ( (case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then Inv.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end) = '" + clsCommon.myCstr(objFilter.arrLocation(ii).Code) + "') "
                        IsApplicable = True
                        Dim arr As Dictionary(Of String, Object) = objFilter.arrLocation(ii).arrOut
                        If arr IsNot Nothing AndAlso arr.Count > 0 Then
                            locFilter += " and Inv.Location_Code in ("
                            Dim isFirstTime As Boolean = True
                            For Each strInn As String In arr.Keys
                                If Not isFirstTime Then
                                    locFilter += ","
                                End If
                                locFilter += "'" + strInn + "'"
                                isFirstTime = False
                            Next
                            locFilter += ")"
                        End If
                    End If
                Next
                If Not IsApplicable Then
                    Throw New Exception("Please select at least one location")
                End If
            End If





            qry = "select Batch_No, Item_Code ,max(Item_Desc) as Item_Desc,location_Code,sum(qty) as Qty,sum([0-" & (bucket1) & " Qty]) as [0-" & (bucket1) & " Qty],sum([" & (bucket1 + 1) & "-" & (bucket2) & " Qty]) as [" & (bucket1 + 1) & "-" & (bucket2) & " Qty], sum([" & (bucket2 + 1) & "-" & (bucket3) & " Qty]) as [" & (bucket2 + 1) & "-" & (bucket3) & " Qty],sum([" & (bucket3 + 1) & "-" & (bucket4) & " Qty]) as [" & (bucket3 + 1) & "-" & (bucket4) & " Qty],sum([Over " & (bucket4) & " Qty]) as [Over " & (bucket4) & " Qty]  From (select Item_Code,Batch_No,Item_Desc,location_Code,Punching_Date,Trans_Type,trans_age ,finalQty as qty,

CASE WHEN trans_age<" & (bucket1 + 1) & " THEN finalQty else 0  END [0-" & (bucket1) & " Qty],

CASE WHEN trans_age>" & (bucket1) & " and trans_age<" & (bucket2 + 1) & " THEN finalQty else 0  END [" & (bucket1 + 1) & "-" & (bucket2) & " Qty],
CASE WHEN trans_age>" & (bucket2) & " and trans_age<" & (bucket3 + 1) & " THEN finalQty else 0  END [" & (bucket2 + 1) & "-" & (bucket3) & " Qty],
CASE WHEN trans_age>" & (bucket3) & " and trans_age<" & (bucket4 + 1) & " THEN finalQty else 0  END [" & (bucket3 + 1) & "-" & (bucket4) & " Qty],
CASE WHEN trans_age>" & (bucket4) & " THEN finalQty else 0  END [Over " & (bucket4) & " Qty]
from(Select Item_Code,Batch_No,Item_Desc,Punching_Date,Trans_Type,trans_age ,case when qty>finalQty  then convert(decimal(18,2),finalQty) else convert(decimal(18,2),qty) end as finalQty,location_Code from  (select *,SUM (qty) OVER(PARTITION BY Item_Code,Batch_No,location_Code ORDER BY rowno asc ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) as finalQty

from (
------------ in Query
SELECT TSPL_BATCH_ITEM.Batch_No,Inv.Item_Code,tspl_item_master.Item_Desc , convert(date,Inv.Punching_Date,103) As Punching_Date, trans_type,case when TSPL_BATCH_ITEM.In_Out_Type='O' then -1 ELSE 1 END * (isnull(TSPL_BATCH_ITEM.Qty,0) * isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/ ConvertedUnit.Conversion_Factor as  qty,DATEDIFF (DAY, Punching_Date, '" & clsCommon.GetPrintDate(objFilter.AsOnDate, "dd-MMM-yyyy") & "') AS trans_age,
row_number() over (order by Inv.Item_Code,convert(date, Inv.punching_date,101)  ) as RowNo,Inv.location_Code
FROM TSPL_BATCH_ITEM 
LEFT OUTER JOIN TSPL_INVENTORY_MOVEMENT as Inv ON Inv.TRANS_ID=TSPL_BATCH_ITEM.Against_Inv_Movement_Trans_Id
left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =Inv.Location_Code
left outer join tspl_item_master on tspl_item_master.item_code=TSPL_BATCH_ITEM.Item_code
left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.item_code=TSPL_BATCH_ITEM.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_BATCH_ITEM.UOM 
left outer join TSPL_ITEM_UOM_DETAIL AS ConvertedUnit on ConvertedUnit.Item_Code=TSPL_BATCH_ITEM.Item_Code " & If(clsCommon.myLen(objFilter.UOM_Code) > 0, " and ConvertedUnit.UOM_Code= '" & objFilter.UOM_Code & "' ", " AND ConvertedUnit.Stocking_unit='Y' ") & "  
WHERE TSPL_BATCH_ITEM.In_Out_type='I' " & ItemTypeFilter & " and cast(Inv.Punching_Date as date)<='" & clsCommon.GetPrintDate(objFilter.AsOnDate, "dd-MMM-yyyy") & "' " & If(clsCommon.myLen(ItemFilter) > 0, " and Inv.Item_Code in " & ItemFilter & "", "") & " " & If(clsCommon.myLen(locFilter) > 0, " and " & locFilter & "", "") & "
union all
---------Out Query
SELECT TSPL_BATCH_ITEM.Batch_No,TSPL_BATCH_ITEM.Item_Code, max(tspl_item_master.Item_Desc ) as Item_Desc,  convert(date,min(Punching_Date),103) As Punching_Date, max(trans_type) as trans_type,
SUM ((CASE WHEN InOut='I' THEN 1 ELSE -1 END) * (isnull(TSPL_BATCH_ITEM.Qty,0) * isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/ ConvertedUnit.Conversion_Factor) 
AS InQty,0 AS trans_age,0 as rowno,Inv.location_Code FROM TSPL_BATCH_ITEM 
LEFT OUTER JOIN TSPL_INVENTORY_MOVEMENT as Inv ON Inv.TRANS_ID=TSPL_BATCH_ITEM.Against_Inv_Movement_Trans_Id
left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =Inv.Location_Code
left outer join tspl_item_master on tspl_item_master.item_code=TSPL_BATCH_ITEM.Item_code
left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.item_code=TSPL_BATCH_ITEM.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_BATCH_ITEM.UOM 
left outer join TSPL_ITEM_UOM_DETAIL AS ConvertedUnit on ConvertedUnit.Item_Code=TSPL_BATCH_ITEM.Item_Code " & If(clsCommon.myLen(objFilter.UOM_Code) > 0, " and ConvertedUnit.UOM_Code= '" & objFilter.UOM_Code & "' ", " AND ConvertedUnit.Stocking_unit='Y' ") & "   
WHERE InOut='O' " & ItemTypeFilter & " and cast(Punching_Date as date)<='" & clsCommon.GetPrintDate(objFilter.AsOnDate, "dd-MMM-yyyy") & "' " & If(clsCommon.myLen(ItemFilter) > 0, " and Inv.Item_Code in " & ItemFilter & "", "") & " " & If(clsCommon.myLen(locFilter) > 0, " and " & locFilter & "", "") & "
 group by TSPL_BATCH_ITEM.Item_Code,TSPL_BATCH_ITEM.Batch_No  ,Inv.Location_Code
) Final
)FinalInv where finalQty >=0 )FinalQryQty
)OuterQry group by Item_Code,Batch_No ,Location_Code"



            Return qry
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function
    Sub BulkExport(ByVal FormatType As String)
        Try
            clsCommon.ProgressBarPercentShow()
            clsCommon.ProgressBarPercentUpdate(0, "Generating query for the report..")
            Dim qry As String = ""

            clsCommon.ProgressBarPercentUpdate(10, "Query generated..starting Inventory Ageing export..")
            transportSql.BulkExport("Summary", qry, "", FormatType)

            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow("Data exported successfully")
        Catch ex As Exception
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            clsCommon.ProgressBarPercentHide()
        End Try
    End Sub

    Sub SetGridFormationOFGV1()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).Width = 125
        Next

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        gv1.MasterTemplate.SummaryRowsBottom.Clear()

        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        RadPageView1.SelectedPage = RadPageViewPage2
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

    Private Sub txt1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (IsNumeric(e.KeyChar) = False And e.KeyChar <> CChar(ChrW(Keys.Back))) Then
            MsgBox("Only Numeric Values")
            e.KeyChar = ""
            txt1.Focus()
            txt1.Select()
            Return
        End If
    End Sub

    Private Sub txt2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (IsNumeric(e.KeyChar) = False And e.KeyChar <> CChar(ChrW(Keys.Back))) Then
            MsgBox("Only Numeric Values")
            e.KeyChar = ""
            txt2.Focus()
            txt2.Select()
            Return
        End If
    End Sub

    Private Sub txt3_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (IsNumeric(e.KeyChar) = False And e.KeyChar <> CChar(ChrW(Keys.Back))) Then
            MsgBox("Only Numeric Values")
            e.KeyChar = ""
            txt3.Focus()
            txt3.Select()
            Return
        End If
    End Sub

    Private Sub txt3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.txt4th.Text = Me.txt3.Text
    End Sub


    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Try
            Dim qry As String = "select Item_Code as Code,Item_Desc as Description from tspl_item_master " & IIf(chkBatchWise.Checked = True, " where Is_Batch_Item=1 ", "") & "  order by item_code"
            txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("Item", qry, "Code", "Description", txtItem.arrValueMember, txtItem.arrDispalyMember)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub txt4th_TextChanged_1(sender As Object, e As EventArgs) Handles txt4th.TextChanged
        txtOver.Text = txt4th.Text

    End Sub

    Private Sub cbType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs)
        txtItem.arrValueMember = Nothing
    End Sub

    Private Sub rbtnLocationSelect_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnLocationSelect.ToggleStateChanged
        gvLocation.Enabled = rbtnLocationSelect.IsChecked
        RadButton4.Enabled = rbtnLocationSelect.IsChecked
        RadButton5.Enabled = rbtnLocationSelect.IsChecked
    End Sub

    Private Sub rbtnLocationAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnLocationAll.ToggleStateChanged
        gvLocation.Enabled = rbtnLocationSelect.IsChecked
        RadButton4.Enabled = rbtnLocationSelect.IsChecked
        RadButton5.Enabled = rbtnLocationSelect.IsChecked
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

    Sub SetFiscalYear()
        'txtFromDate.MinDate = New Date(2001, 4, 1)
        'txtFromDate.MaxDate = New Date(3000, 12, 1)
        'txtToDate.MinDate = txtFromDate.MinDate
        'txtToDate.MaxDate = txtFromDate.MaxDate

        'txtToDate.Value = clsCommon.GETSERVERDATE()
        'txtFromDate.Value = txtToDate.Value.AddDays(-121)

    End Sub

    Private Sub txtItemType__My_Click(sender As Object, e As EventArgs) Handles txtItemType._My_Click
        Try
            Dim qry As String = "select ITEM_TYPE_CODE as Code,ITEM_TYPE_NAME as Description from TSPL_ITEM_TYPE_MASTER order by Id"
            txtItemType.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemType", qry, "Code", "Description", txtItemType.arrValueMember, txtItemType.arrDispalyMember)
        Catch ex As Exception
        End Try
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

    Private Sub chkGITLocation_ToggleStateChanged(sender As Object, args As StateChangedEventArgs)
        Try
            LoadLocation()
        Catch ex As Exception

        End Try
    End Sub



    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            IsDrillDown = False
            BackProcess = False
            GetData()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub rmExcel_Click(sender As Object, e As EventArgs) Handles rmExcel.Click
        print(EnumExportTo.Excel)
    End Sub


    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        print(EnumExportTo.PDF)
    End Sub

    Private Sub BulkExcel_Click(sender As Object, e As EventArgs) Handles BulkExcel.Click
        BulkExport("xls")
    End Sub


    Private Sub BulkCSV_Click(sender As Object, e As EventArgs) Handles BulkCSV.Click
        BulkExport("csv")
    End Sub

    Private Sub chkBatchWise_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkBatchWise.ToggleStateChanged
        txtItem.arrValueMember = Nothing
    End Sub
End Class
