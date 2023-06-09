
Imports common
Imports System.Threading
Imports Telerik.WinControls.UI.Export
Imports Telerik.WinControls.UI.Export.ExcelML
Imports System.IO
Imports Microsoft.Office.Interop
Imports System.ComponentModel

''Check in sanjay 23/06/2020
Public Class FrmStockReco
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

    Dim isInsideLoadData As Boolean = False
    Dim RateTunning As String = "0.11"
    Dim FORMTYPE As String = Nothing
#End Region

    ''Done by Monika 27/04/2017
#Region "Partial Load Variables"
    Public objFilter As New clsStockRecoFilters
    Public _DS As New DataSet()
    ''1.
    Private m_FieldsToDisplay As String()

    '<DefaultValue(Nothing)> _
    Public Property FieldsToDisplay() As String()
        Get
            Return m_FieldsToDisplay
        End Get
        Set(value As String())
            m_FieldsToDisplay = value
        End Set
    End Property

    ''2.
    Public ReadOnly Property ValueMember() As String
        Get
            Return "Trans_ID"
        End Get
    End Property

    ''3.
    <DefaultValue(False)> _
    Public ReadOnly Property ShowValueMember() As Boolean
        Get
            Return False
        End Get
    End Property
#End Region
    ''===============End Here===================

#Region "User Defined Functions and Subroutines"
    Public Sub New(ByVal formid As String)
        InitializeComponent()
        FORMTYPE = formid
    End Sub
    Public Sub New()
        InitializeComponent()
    End Sub
#End Region

    Private Sub FrmKPIReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        RateTunning = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.StockRecoRateTunning, clsFixedParameterCode.StockRecoRateTunning, Nothing))
        '' invisible controls for Stock Reco Jobwork
        If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.stockRecoNewJR) = CompairStringResult.Equal Then
            cboFatSNFType.Visible = False
            ChkMRPWise.Visible = False
            chkIncludeGIT.Visible = False
            chkProd_WIP.Visible = False
        End If
        txtFromDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        txtToDate.Value = clsCommon.GETSERVERDATE
        arrBack = New List(Of String)

        SetUserMgmtNew()
        LoadFATSNF_Type()
        LoadReportFATSNFType()
        LoadCategory()
        LoadUnit()
        LoadLocation()
        chkPartiallyLoad.Enabled = False
        LoadType()
        LoadInOutType()
        LoadDisplayMethod()
        ButtonToolTip.SetToolTip(RadButton2, "Press Alt+C Close the Window")
        RadPageView1.SelectedPage = RadPageViewPage1
        rbtnCategoryAll.IsChecked = True
        rbtnLocationAll.IsChecked = True
        cboType.SelectedValue = "Item Type Wise Summary"
        cboInOutType.SelectedValue = "All"
        btnPrint.Visible = False
        GetMIS_ITem_GroupColumn()

        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*) as total from TSPL_INV_MOVE_DL")) > 0 AndAlso clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.UpdateInventorySummaryTable, clsFixedParameterCode.UpdateInventorySummaryTable, Nothing)) = 1 Then
            cboDisplayMethod.Enabled = True
            chkPartiallyLoad.Enabled = False
            chkProd_WIP.Enabled = True
            cboFatSNFType.Enabled = True
            vsb.Visible = False
        Else
            cboDisplayMethod.Enabled = False
            chkPartiallyLoad.Enabled = False
            chkProd_WIP.Enabled = False
            cboFatSNFType.Enabled = False
            vsb.Visible = False
        End If

        If isDataLoad Then
            txtFromDate.Value = dtFrom
            txtToDate.Value = dtTo
            cmbUnit.SelectedValue = Unit_Code
            ChkMRPWise.Checked = MRP_Wise
            If ShowFatAndSNF Then
                cboFATSNF.SelectedValue = "FS"
            Else
                cboFATSNF.SelectedValue = "B"
            End If
            cboFatSNFType.SelectedValue = "M"
            txtItem.arrValueMember = arrItem
            txtTransaction.arrValueMember = arrTransaction
            txtItemGroup.arrValueMember = arrItemGroup
            txtItemType.arrValueMember = arrItemType
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
        If clsCommon.CompairString(FORMTYPE, "MIS-STLE-RPT") = CompairStringResult.Equal Then
            btnPrint.Visible = True
            cboType.Visible = False
            lblModeofTransport.Visible = False
            cboType.SelectedValue = "Document Wise Detail Ledger"
        End If
    End Sub

    Private Sub LoadFATSNF_Type()
        cboFatSNFType.DataSource = Nothing
        'union all select 'S' as Code,'Manual V/s Standard(MI)' as Name 
        Dim qry As String = "select '' as Code,'None' as Name union all select 'M' as Code,'Actual(QC)' as Name union all select 'Q' as Code,'Stock V/s Actual(QC)' as Name "
        cboFatSNFType.DataSource = clsDBFuncationality.GetDataTable(qry)
        cboFatSNFType.ValueMember = "Code"
        cboFatSNFType.DisplayMember = "Name"
        cboFatSNFType.SelectedValue = ""
    End Sub

    Sub LoadLocation()
        gvLocation.DataSource = Nothing

        ''===============when WIP then only section/sublocation seen in location finders=====24/03/2017==========================
        Dim whrCls As String = " and ((Is_Section='N' and Is_Sub_Location='N' and Location_Type IN ('Physical','Logical','Virtual') ) or (CSA_Type='Y') )"
        If chkProd_WIP.Checked Then
            whrCls = " and location_code in (select Main_Location_Code from TSPL_LOCATION_MASTER where coalesce(Main_Location_Code,'')<>'') " ''" and (Is_Section='Y' or Is_Sub_Location='Y') "
        End If
        ''====================================================================================================================

        Dim qry As String = " select cast( 0 as bit) as SEL,Location_Code as CODE,Location_Desc as NAME,case when Is_Jobwork=1 then 'Yes' else 'No' end as [Job Location] from TSPL_LOCATION_MASTER where 1=1 " + whrCls + ""
        If Not chkIncludeGIT.Checked Then
            qry += " and  TSPL_LOCATION_MASTER.GIT_Type<>'Y' "
        End If
        'If chkExcludeConsumptionLoc.Checked = True Then
        '    qry += " and  TSPL_LOCATION_MASTER.Is_Consumption_Location =0 "
        'End If
        If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.stockRecoNewJR) = CompairStringResult.Equal Then
            'qry = " select cast( 0 as bit) as SEL,Location_Code as CODE,Location_Desc as NAME,Jobwork_Vendor,Is_Sub_Location from TSPL_LOCATION_MASTER where len(coalesce(Jobwork_Vendor,''))>0 and Is_Sub_Location='Y'"
            qry += " and TSPL_LOCATION_MASTER.Location_Code in (select distinct coalesce(Main_Location_Code,'') as Main_Location from tspl_location_master where len(coalesce(Main_Location_Code,''))>0 and len(coalesce(Jobwork_Vendor,''))>0) "
        End If
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            qry += "  and  TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
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
    Sub LoadDisplayMethod()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "None"
        dr("Name") = "None"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "AD"
        dr("Name") = "All Document"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "DL"
        dr("Name") = "Daily"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "MT"
        dr("Name") = "Monthly"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "QTR"
        dr("Name") = "Quarterly"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "YRL"
        dr("Name") = "Yearly"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "MM"
        dr("Name") = "Mixed Mode"
        dt.Rows.Add(dr)

        cboDisplayMethod.DataSource = dt
        cboDisplayMethod.ValueMember = "Code"
        cboDisplayMethod.DisplayMember = "Name"
        cboDisplayMethod.SelectedValue = "None"
    End Sub

    Sub LoadReportFATSNFType() ''BHA/02/05/19-000877 By Balwinder on 14/05/2019 
        isInsideLoadData = True
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "B"
        dr("Name") = "Both"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "FS"
        dr("Name") = "FAT & SNF"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "NFS"
        dr("Name") = "Non FAT & SNF"
        dt.Rows.Add(dr)

        cboFATSNF.DataSource = dt
        cboFATSNF.ValueMember = "Code"
        cboFATSNF.DisplayMember = "Name"

        isInsideLoadData = False
    End Sub

    Sub LoadType()
        isInsideLoadData = True
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        'clsUserMgtCode.stockRecoNewJR()
        If clsCommon.CompairString(FORMTYPE, "STO-REC-RPT") = CompairStringResult.Equal Or clsCommon.CompairString(FORMTYPE, "MIS-SRec-RPT") = CompairStringResult.Equal OrElse clsCommon.CompairString(FORMTYPE, clsUserMgtCode.stockRecoNewJR) = CompairStringResult.Equal Then

            dr("Code") = "Item Type Wise Summary"
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Code") = "Item Structure Wise Summary" ''TEC/17/05/19-000496 By balwinder on 15/05/2019
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Code") = "Item Group Wise Summary"
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Code") = "Category Wise Summary"
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Code") = "Item Wise Summary"
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Code") = "Item And Location Wise Summary"
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Code") = "Document Wise Detail"
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Code") = "Date, Item And Document Wise Detail"
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Code") = "Date and Item Wise Stock"
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Code") = "Transaction Wise"
            dt.Rows.Add(dr)
        Else
            dr = dt.NewRow()
            dr("Code") = "Document Wise Detail Ledger"
            dt.Rows.Add(dr)
        End If

        cboType.DataSource = dt
        cboType.ValueMember = "Code"
        cboType.DisplayMember = "Code"

        isInsideLoadData = False
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
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnQuickExport.Visible = MyBase.isExport
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub

    Private Sub FrmKPIReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then

        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton2.Click
        Me.Close()
    End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        Try
            gv1.EnableFiltering = True
            If cboDisplayMethod.SelectedValue = "None" Then
                LoadData(0)
            Else
                LoadDataNew(0)
            End If
            'LoadData(0)
            TemplateGridview = gv1
            PageSetupReport_ID = GetReportID()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub LoadDataNew(ByVal isPrintCrystalReport As Integer)
        clsCommon.ProgressBarShow()
        _DS = New DataSet()
        Try
            gv1.DataSource = Nothing
            gv1.Columns.Clear()
            gv1.Rows.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.ShowGroupPanel = False
            gv1.EnableFiltering = True


            objFilter = New clsStockRecoFilters
            objFilter.arrLocation = New List(Of clsCode)
            objFilter.arrCategory = New List(Of clsCode)
            objFilter.arrLoc = New ArrayList
            objFilter.From_Date = txtFromDate.Value
            objFilter.To_Date = txtToDate.Value
            objFilter.arrTransaction = txtTransaction.arrValueMember
            objFilter.arrItemGroup = txtItemGroup.arrValueMember
            objFilter.arrItem = txtItem.arrValueMember
            objFilter.ReportType = cboType.SelectedValue
            objFilter.arrItemType = txtItemType.arrValueMember
            objFilter.UOM_Code = cmbUnit.SelectedValue
            objFilter.MRPWise = ChkMRPWise.Checked
            objFilter.FatSNF = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            objFilter.IncludeGIT = chkIncludeGIT.Checked
            ''objFilter.ExcludeConsumptionLoc = chkExcludeConsumptionLoc.Checked
            objFilter.SelectLocation = rbtnLocationSelect.IsChecked
            objFilter.DisplayMethod = cboDisplayMethod.SelectedValue
            objFilter.FORMTYPE = FORMTYPE
            objFilter.isPrintCrystal = isPrintCrystalReport
            objFilter.ChkPartialyLoadData = chkPartiallyLoad.Checked
            objFilter.FAT_SNF_TYPE = cboFatSNFType.SelectedValue
            If clsCommon.myLen(objFilter.FAT_SNF_TYPE) <= 0 AndAlso objFilter.FatSNF Then
                'objFilter.FAT_SNF_TYPE = "M"
            End If
            '=============================================================

            For intLoc As Integer = 0 To gvLocation.RowCount - 1
                Dim loc As New clsCode
                If chkProd_WIP.Checked AndAlso Not rbtnLocationSelect.IsChecked Then
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

            objFilter.SelectCategory = rbtnCategorySelect.IsChecked
            For intLoc As Integer = 0 To gvCategory.RowCount - 1
                Dim loc As New clsCode
                loc.Sel = gvCategory.Rows(intLoc).Cells("Sel").Value 'gvLocation.Rows(intLoc).Cells("Sel").Value
                loc.Code = gvCategory.Rows(intLoc).Cells("Code").Value 'gvLocation.Rows(intLoc).Cells("Code").Value
                loc.Desc = gvCategory.Rows(intLoc).Cells("Name").Value 'gvLocation.Rows(intLoc).Cells("Name").Value
                objFilter.arrCategory.Add(loc)
            Next



            '====================add by Monika26/03/2017==================
            objFilter.IsProduction_WIP = chkProd_WIP.Checked
            If chkProd_WIP.Checked AndAlso Not rbtnLocationSelect.IsChecked Then ''in case of WIP,if no location select then also add location in where condition
                objFilter.SelectLocation = True
            End If
            ''=================================================================

            Dim strFinalQry As String = ""
            If chkPartiallyLoad.Checked AndAlso (clsCommon.CompairString(cboType.SelectedValue, "Date, Item And Document Wise Detail") = CompairStringResult.Equal OrElse clsCommon.CompairString(cboType.SelectedValue, "Transaction Wise") = CompairStringResult.Equal) AndAlso isPrintCrystalReport = 0 Then ''=======Done By Monika 27/04/2017, for load data in packets
                vsb.Visible = True

                gv1.GroupDescriptors.Clear()
                gv1.TableElement.TableHeaderHeight = 40
                gv1.MasterTemplate.ShowRowHeaderColumn = False
                gv1.AllowAddNewRow = False
                gv1.AllowDeleteRow = False
                gv1.SummaryRowsBottom.Clear()
                gv1.VerticalScroll.Visible = False
                gv1.HorizontalScroll.Visible = True

                _DS = clsInventoryMovement.GetDataSet_ForPartiallyLoad(0, objFilter)

                SetGridDisplay(_DS, objFilter)
                AdjustRowCount(_DS, objFilter)
                DisplayValues(_DS, objFilter)
            Else
                strFinalQry = clsInventoryMovement.GetStockRecoQry(objFilter)

                If isPrintCrystalReport <> 4 AndAlso isPrintCrystalReport <> 5 Then
                    Dim dtr As System.Data.SqlClient.SqlDataReader = Nothing
                    Try
                        dtr = clsDBFuncationality.GetDataReader(strFinalQry, Nothing)
                        If Not dtr.HasRows Then
                            Throw New Exception("No Data Found to Display")
                        End If
                        'gv1.
                        If dtr.HasRows Then
                            gv1.MasterTemplate.LoadFrom(dtr)
                        End If
                        'gv1.MasterTemplate.LoadFrom(dtr)
                    Catch ex As Exception
                        Throw New Exception(ex.Message)
                    Finally
                        dtr.Close()
                    End Try
                End If
               
            End If

            If isPrintCrystalReport <> 4 AndAlso isPrintCrystalReport <> 5 Then
                EnableDisableCtrl(False)
                SetGridFormationOFGV1()
                RadPageView1.SelectedPage = RadPageViewPage2
            End If

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
            ElseIf isPrintCrystalReport = 4 Then
                transportSql.BulkExport("Stock Reco" & "_" & clsCommon.myCstr(cboType.SelectedValue), strFinalQry, "order by Item_Code,Punching_Date", "csv")
                Exit Sub
            ElseIf isPrintCrystalReport = 5 Then
                transportSql.BulkExport("Stock Reco" & "_" & clsCommon.myCstr(cboType.SelectedValue), strFinalQry, "order by Item_Code,Punching_Date", "xls")
                Exit Sub
            End If
            gv1.AllowAddNewRow = False
            clsCommon.ProgressBarHide()
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

#Region "Load Million Data in Packets" ''Done by Monika 27/04/2017
    Private Sub SetGridDisplay(ByVal ds As DataSet, ByVal objFilter As clsStockRecoFilters)
        Dim ftd As New List(Of String)

        gv1.Rows.Clear()

        If Me.FieldsToDisplay IsNot Nothing AndAlso clsCommon.myLen(Me.FieldsToDisplay) > 0 Then
            ftd.AddRange(Me.FieldsToDisplay)
        Else
            Dim c As DataColumn
            For Each c In ds.Tables("_lookup_result").Columns
                If c.ColumnName = "_lookup_row_number" Then
                    Continue For
                End If
                ftd.Add(c.ColumnName)
            Next
        End If

        Dim s As String
        For Each s In ftd
            If s = Me.ValueMember AndAlso Not ShowValueMember Then
                Continue For
            End If

            Dim columnName As String = "ux" + s.Replace(" ", "")

            For Each dgc As GridViewColumn In gv1.Columns
                If dgc.Name = s Then
                    GoTo goAlreadyAdded
                End If
            Next

            gv1.Columns.Add(columnName)
            Dim c As GridViewColumn = gv1.Columns(columnName)
            c.Name = s
            c.IsVisible = True

goAlreadyAdded:
        Next

    End Sub

    Private Sub AdjustRowCount(ByVal ds As DataSet, ByVal objFilter As clsStockRecoFilters)
        Try
            If ds Is Nothing OrElse ds.Tables("_lookup_result") Is Nothing OrElse ds.Tables("_lookup_result").Rows.Count <= 0 Then
                Exit Sub
            End If


            Dim viewableRows As Long = CType(Math.Floor(CType((gv1.Height - gv1.TableElement.TableHeaderHeight) / gv1.TableElement.TableHeaderHeight, Decimal)), Long)

            If viewableRows <= 0 Then
                Exit Sub
            End If

            If ds IsNot Nothing AndAlso ds.Tables("_lookup_result") IsNot Nothing AndAlso ds.Tables("_lookup_result").Rows.Count > 0 Then
                Dim resultCount As Long = CType(ds.Tables("_lookup_result").Rows.Count, Long)
                If resultCount < viewableRows Then
                    viewableRows = resultCount
                End If
            End If

            If viewableRows <> gv1.Rows.Count Then
                gv1.Rows.Clear()
                If viewableRows > 0 Then
                    For ii As Integer = 0 To viewableRows - 1
                        gv1.Rows.AddNew()
                    Next
                    'gv1.Rows.Add(TryCast(viewableRows, Object))
                End If

                vsb.LargeChange = CType(viewableRows, Integer)

                vsb.Minimum = 0
                vsb.Maximum = CType(ds.Tables("_lookup_count").Rows(0)("_count") - 1, Integer)

                For Each r As GridViewRowInfo In gv1.Rows
                    r.Height = gv1.TableElement.TableHeaderHeight
                Next
            End If

            DisplayValues(ds, objFilter)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub DisplayValues(ByVal ds As DataSet, ByVal objFilter As clsStockRecoFilters)
        Try
            For r As Integer = 0 To gv1.Rows.Count - 1
                Dim absRow As Integer = r + vsb.Value

                Dim drx() As DataRow = ds.Tables("_lookup_result").Select(String.Format("_lookup_row_number = {0}", absRow))

                If absRow = CType(ds.Tables("_lookup_count").Rows(0)("_count") - 1, Integer) Then
                    MessageBox.Show("End Here")
                    Exit Sub
                End If

                If drx.Length = 0 Then
                    ds = clsInventoryMovement.GetDataSet_ForPartiallyLoad(absRow, objFilter)
                    drx = ds.Tables("_lookup_result").Select(String.Format("_lookup_row_number = {0}", absRow))
                End If


                If drx.Length = 1 Then
                    For Each c As GridViewColumn In gv1.Columns
                        gv1.Rows(r).Cells(c.Name).Value = drx(0)(c.Name) ''or c.fieldname
                        gv1.Columns(c.Name).Width = 80
                    Next
                Else
                    For Each c As GridViewColumn In gv1.Columns
                        gv1.Rows(r).Cells(c.Name).Value = Nothing
                    Next
                End If

            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub gv1_Resize(sender As Object, e As EventArgs) Handles gv1.Resize
        AdjustRowCount(_DS, objFilter)
    End Sub

    Private Sub gv1_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles gv1.KeyDown
        Try
            If gv1.CurrentCell Is Nothing Then
                Exit Sub
            End If

            If e.KeyCode = Keys.Down Then
                If gv1.CurrentCell.RowIndex = gv1.Rows.Count - 1 Then
                    If vsb.Value <= vsb.Maximum - vsb.LargeChange Then
                        e.Handled = True
                        vsb.Value = vsb.Value + 1
                    End If
                End If

            ElseIf e.KeyCode = Keys.Up Then
                If gv1.CurrentCell.RowIndex = 0 Then
                    If vsb.Value > 0 Then
                        e.Handled = True
                        vsb.Value = vsb.Value - 1
                    End If
                End If

            ElseIf e.KeyCode = Keys.PageDown Then
                If gv1.CurrentCell.RowIndex = gv1.Rows.Count - 1 Then
                    If Not (vsb.Value + vsb.LargeChange > vsb.Maximum) Then
                        Dim NewValue As Integer = vsb.Value + vsb.LargeChange

                        If vsb.Maximum - NewValue > vsb.LargeChange Then
                            vsb.Value = NewValue
                        Else
                            vsb.Value = vsb.Maximum - vsb.LargeChange + 1
                        End If
                    End If


                End If
            ElseIf e.KeyCode = Keys.PageUp Then
                If gv1.CurrentCell.RowIndex = 0 Then
                    If Not (vsb.Value - vsb.LargeChange < 0) Then
                        vsb.Value -= vsb.LargeChange
                    Else
                        vsb.Value = 0
                    End If
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gv1_MouseWheel(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles gv1.MouseWheel
        Try
            If e.Delta < 0 Then
                Dim rowCount As Long = CType(_DS.Tables("_lookup_count").Rows(0)("_count"), Long)

                If vsb.Value < rowCount - vsb.LargeChange Then
                    vsb.Value = ++vsb.Value
                End If
            ElseIf e.Delta > 0 Then
                If vsb.Value > 0 Then
                    vsb.Value = --vsb.Value
                End If
            End If
        Catch ex As Exception
            'clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub vsb_ValueChanged(sender As Object, e As EventArgs) Handles vsb.ValueChanged
        DisplayValues(_DS, objFilter)
    End Sub
#End Region

    Private Sub LoadData(ByVal isPrintCrystalReport As Integer)
        Try
            clsCommon.ProgressBarShow()
            gv1.DataSource = Nothing
            gv1.Columns.Clear()
            gv1.Rows.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.ShowGroupPanel = False
            gv1.EnableFiltering = True
            gv1.AllowAddNewRow = False
            If clsCommon.GetDateWithEndTime(txtToDate.Value) < clsCommon.GetDateWithStartTime(txtFromDate.Value) Then
                Throw New Exception("To Date cant be less than from date")
            End If


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
                ''End of Category Table start now.
            End If
            ''Virtual Category Table start now.
            '' query for structure and item group custom field
            Dim strItemGroup As String = ""
            strItemGroup = " select Struct.Structure_Code,Structure_Descq,Struct_Val.Value as Item_Group,StructDtl.Description as Group_Description from TSPL_STRUCTURE_MASTER Struct left join (" &
                           " select Custom_field_Code,Transaction_code,Value from TSPL_CUSTOM_FIELD_VALUES where Program_Code='" & clsUserMgtCode.itemStructure & "'  " &
                           " and Custom_Field_Code='" & MIS_Item_Group & "') as Struct_Val  on Struct.Structure_Code=Struct_Val.Transaction_Code" &
                           " left join (select Custom_Field_Code,SNo,Value,Description from TSPL_CUSTOM_FIELD_DETAIL where Custom_Field_Code='" & MIS_Item_Group & "') as StructDtl on Struct_Val.Value=StructDtl.Value "


            ''Base Query start Now ERO/22/06/22-001416 ADD TABLE NAME WITH FAT_AMT AND SNF_AMT COLUMN
            Dim qry As String = "select * from ( select InventroyMovement.Fat_Amt,InventroyMovement.SNF_Amt,gl1.Account_code as Inventory_Control_Acc,gl1.Description as Inventory_Control_Acc_desc ,InventroyMovement.Fat_Rate,InventroyMovement.SNF_Rate ,InventroyMovement.Trans_Id,InventroyMovement.Trans_Type, (CASE WHEN (InventroyMovement.Trans_Type='IC-AD' AND TSPL_ADJUSTMENT_HEADER.Reference_Document='JWO-SRN-JLO') THEN 'Jobwork Consumption' ELSE " &
                " TSPL_INVENTORY_SOURCE_CODE.Name END )as Trans_Type_Name,InventroyMovement.Source_Doc_No,InventroyMovement.Punching_Date, InventroyMovement.InOut,case when InventroyMovement.InOut='I' then 'In' else case when InventroyMovement.InOut='O' then 'Out' else '' end end as 'InOutView',"
            qry += " case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end as Main_Location_Code,MainLocationTable.Location_Desc as MainLocationDesc, InventroyMovement.Location_Code,TSPL_LOCATION_MASTER.Location_Desc AS [Loc Desp],TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then ''  else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_LOCATION_MASTER.Pin_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_LOCATION_MASTER.Pin_Code) End End End  as [LocAddress],SourceCode,SourceName,SourceType  ,Item_Group.Item_Group,Item_Group.Group_Description, InventroyMovement.Item_Code, InventroyMovement.MRP ,TSPL_ITEM_MASTER.Item_Desc,tspl_item_master.itf_code,TSPL_ITEM_MASTER.Structure_Code,TSPL_STRUCTURE_MASTER.Structure_Descq,"
            qry += " IsFromMilk,MilkFATKG,MilkSNFKG,case when IsFromMilk=1 then MilkFatPer else isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) end as MilkFatPer,case when IsFromMilk=1 then MilkSNFPer else isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) end as MilkSNFPer,TSPL_LOCATION_MASTER.Is_Section,TSPL_LOCATION_MASTER.Is_Sub_Location,"
            qry += " isnull((InventroyMovement.Stock_Qty * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end))  ,0) as QtyKG,"
            If clsCommon.myLen(cmbUnit.SelectedValue) > 0 Then
                qry += " '" + clsCommon.myCstr(cmbUnit.SelectedValue) + "' as Stock_UOM,cast((InventroyMovement.Stock_Qty /  (case when InventroyMovement.Custom_UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code and InventroyMovement.Custom_Coversion_Factor>0 then  InventroyMovement.Custom_Coversion_Factor else TSPL_ITEM_UOM_DETAIL.Conversion_Factor end)) as decimal(18,10)) as Stock_Qty,"
                qry += " isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end) * (TSPL_ITEM_UOM_DETAIL.Conversion_Factor)) from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) as FatPer,"
                qry += " isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end) * (TSPL_ITEM_UOM_DETAIL.Conversion_Factor)) from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) as SNFPer,"
            Else
                qry += " InventroyMovement.Stock_UOM,InventroyMovement.Stock_Qty,"
                qry += " isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)) from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) as FatPer,"
                qry += " isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)) from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) as SNFPer,"
            End If
            qry += " (case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=3 then InventroyMovement.FIFO_Cost else case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=2 then InventroyMovement.LIFO_Cost else InventroyMovement.Avg_Cost end end ) as Cost,TSPL_ITEM_MASTER.Item_Category_Struct_Code " + Environment.NewLine

            If clsCommon.myLen(strCategoryTable) > 0 Then
                qry += "," + strCodeColumnSelect + "," + strCodeDescColumnSelect
            End If
            qry += " ,TSPL_ITEM_MASTER.Item_Type,VirtualTableItemType.Name as Item_Type_Name,TSPL_INVENTORY_SOURCE_CODE.In_Category,TSPL_INVENTORY_SOURCE_CODE.Out_Category,TSPL_INVENTORY_SOURCE_CODE.Code,(case when ISNULL(InventroyMovement.Location_Code,'')='' then InventroyMovement.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end) as PrimaryLocation "
            If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Transaction Wise") = CompairStringResult.Equal Then
                qry += " ,case TSPL_PP_PRODUCTION_ENTRY_DETAIL.FAT_KG when TSPL_PP_PRODUCTION_ENTRY_DETAIL.FAT_KG then TSPL_PP_PRODUCTION_ENTRY_DETAIL.FAT_KG * case when InOut='I' then 1.00 else -1.00 end else case when InOut='I' then ( (case when IsFromMilk=1 then MilkFATKG else (Stock_Qty*isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)) from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0)) end) * case when InOut='I' then 1.00 else -1.00 end) else -1.00 end end as Prod_Fat_KG "
                qry += " ,case TSPL_PP_PRODUCTION_ENTRY_DETAIL.FAT_Per  when TSPL_PP_PRODUCTION_ENTRY_DETAIL.FAT_Per then TSPL_PP_PRODUCTION_ENTRY_DETAIL.FAT_Per else 0 end as Prod_Fat_Per  "
                qry += " ,case TSPL_PP_PRODUCTION_ENTRY_DETAIL.SNF_KG when TSPL_PP_PRODUCTION_ENTRY_DETAIL.SNF_KG then TSPL_PP_PRODUCTION_ENTRY_DETAIL.SNF_KG * case when InOut='I' then 1.00 else -1.00 end else case when InOut='I' then (( (case when IsFromMilk=1 then MilkSNFKG else (Stock_Qty*isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)) from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0)) end ) * case when InOut='I' then 1.00 else -1.00 end)) else -1.00 end end as Prod_SNF_KG "
                qry += " ,case TSPL_PP_PRODUCTION_ENTRY_DETAIL.SNF_Per  when TSPL_PP_PRODUCTION_ENTRY_DETAIL.SNF_Per then TSPL_PP_PRODUCTION_ENTRY_DETAIL.SNF_Per else 0 end as Prod_SNF_Per  "
            End If
            qry += " from ( "
            qry += " select Fat_Amt,SNF_Amt,0 AS Fat_Rate,0 AS SNF_Rate ,Trans_Id,Trans_Type,Source_Doc_No,Punching_Date,InOut,Location_Code,Item_Code,UOM, MRP,Stock_UOM,Stock_Qty,FIFO_Cost,LIFO_Cost,Avg_Cost,0 as IsFromMilk,0 as MilkFatPer,0 as MilkSNFPer,0 as MilkFATKG,0 as MilkSNFKG,case when cust_code is not null and len(cust_code)>0 then cust_code else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Code else Other_Location_Code end end as SourceCode,case when cust_code is not null and len(cust_code)>0 then Cust_Name else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Name else Other_Location_Desc end end as SourceName, case when cust_code is not null and len(cust_code)>0 then 'C' else case when Vendor_Code is not null and len(Vendor_Code)>0 then 'V' else case when Other_Location_Code is not null and len(Other_Location_Code)>0 then 'L' else '' end end end as SourceType,'' as Custom_UOM,0 as Custom_Coversion_Factor  from TSPL_INVENTORY_MOVEMENT " + Environment.NewLine
            qry += " union all " + Environment.NewLine
            qry += " select Fat_Amt,SNF_Amt,ISNULL(Fat_Rate,0) AS Fat_Rate,ISNULL(SNF_Rate,0) AS SNF_Rate,Trans_Id,Trans_Type,Source_Doc_No,Punching_Date,InOut,Location_Code,Item_Code,UOM, MRP,Stock_UOM,Stock_Qty,FIFO_Cost,LIFO_Cost,Avg_Cost,1 as IsFromMilk,Fat_Per as MilkFatPer ,SNF_Per as MilkSNFPer,Fat_KG as MilkFATKG,SNF_KG as MilkSNFKG,case when cust_code is not null and len(cust_code)>0 then cust_code else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Code else Other_Location_Code end end as SourceCode,case when cust_code is not null and len(cust_code)>0 then Cust_Name else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Name else Other_Location_Desc end end as SourceName, case when cust_code is not null and len(cust_code)>0 then 'C' else case when Vendor_Code is not null and len(Vendor_Code)>0 then 'V' else case when Other_Location_Code is not null and len(Other_Location_Code)>0 then 'L' else '' end end end as SourceType,isnull(Custom_UOM,'') as Custom_UOM,isnull(Custom_Coversion_Factor,0) as Custom_Coversion_Factor from TSPL_INVENTORY_MOVEMENT_NEW" + Environment.NewLine
            qry += ") InventroyMovement " + Environment.NewLine
            qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=InventroyMovement.Item_Code" + Environment.NewLine
            qry += " left outer join TSPL_STRUCTURE_MASTER on TSPL_STRUCTURE_MASTER.Structure_Code=TSPL_ITEM_MASTER.Structure_Code" + Environment.NewLine
            qry += " left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code" + Environment.NewLine
            qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = InventroyMovement.Location_Code " + Environment.NewLine
            If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Transaction Wise") = CompairStringResult.Equal Then
                qry += " left outer join TSPL_PP_PRODUCTION_ENTRY_DETAIL on TSPL_PP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE=InventroyMovement.Source_Doc_No AND InventroyMovement.Item_Code=TSPL_PP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE AND InventroyMovement.UOM=TSPL_PP_PRODUCTION_ENTRY_DETAIL.UNIT_CODE" + Environment.NewLine
            End If
            qry += " left outer join TSPL_LOCATION_MASTER as MainLocationTable on MainLocationTable.Location_Code =(case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end)"
            qry += " left outer join TSPL_ITEM_UOM_DETAIL as FATSNFConvertedUnit on FATSNFConvertedUnit.Item_Code=InventroyMovement.Item_Code and FATSNFConvertedUnit.UOM_Code='KG'"
            qry += " left outer join TSPL_INVENTORY_SOURCE_CODE on TSPL_INVENTORY_SOURCE_CODE.code=InventroyMovement.Trans_Type " &
                   " left outer join TSPL_ADJUSTMENT_HEADER ON TSPL_ADJUSTMENT_HEADER.Adjustment_No=InventroyMovement.Source_Doc_No  "
            If clsCommon.myLen(cmbUnit.SelectedValue) > 0 Then
                qry += " inner join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=InventroyMovement.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='" + clsCommon.myCstr(cmbUnit.SelectedValue) + "'"
            End If
            If clsCommon.myLen(strCategoryTable) > 0 Then
                qry += " left outer join (" + strCategoryTable + ") as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=InventroyMovement.Item_Code"
            End If
            qry += " left outer join (" & strItemGroup & ") as Item_Group on Item_Group.Structure_Code =TSPL_ITEM_MASTER.Structure_Code "
            qry += " left outer join (" & FrmItemMasterRMOther.LoadItemTypeQuery() & ") as VirtualTableItemType on VirtualTableItemType.Code = TSPL_ITEM_MASTER.Item_Type " &
            " left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code =TSPL_PURCHASE_ACCOUNTS .Inv_Control_Account  " &
            " left outer join TSPL_GL_ACCOUNTS gl1 on gl1.Account_Seg_Code1 =TSPL_GL_ACCOUNTS.Account_Seg_Code1  and gl1.Account_Seg_Code7 =  tspl_location_master.Loc_Segment_Code "
            qry += " Where 2=2 "
            If Not chkIncludeGIT.Checked Then
                qry += " and TSPL_LOCATION_MASTER.GIT_Type<>'Y' and MainLocationTable.GIT_Type<>'Y'"
            End If
            ''richa 22 Jan 2021
            'If chkExcludeConsumptionLoc.Checked = True Then
            '    qry += " and TSPL_LOCATION_MASTER.Is_Consumption_Location =0 and MainLocationTable.Is_Consumption_Location =0 "
            'End If
            If txtItemType.arrValueMember IsNot Nothing AndAlso txtItemType.arrValueMember.Count > 0 Then
                qry += " and TSPL_ITEM_MASTER.Item_Type in (" + clsCommon.GetMulcallString(txtItemType.arrValueMember) + ") " + Environment.NewLine
            End If
            qry += "  ) xxxxx "

            qry += " where 2=2 "

            If isDataLoad AndAlso SkipCheckFatAndSNF Then
                ''never want to check fat% and snf% cond. when open from double click in production(26/05/2014)
            Else
                If clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal Then
                    qry += " and (MilkFatPer<>0 or FatPer<>0  or  MilkSNFPer<>0 or SNFPer<>0) "
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "NFS") = CompairStringResult.Equal Then
                    qry += " and (MilkFatPer=0 and FatPer=0  and  MilkSNFPer=0 and SNFPer=0) "
                End If
            End If


            If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                qry += " and Item_Code in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ") " + Environment.NewLine
            End If
            If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
                qry += " and Trans_Type in (" + clsCommon.GetMulcallString(txtTransaction.arrValueMember) + ") " + Environment.NewLine
            ElseIf chkProd_WIP.Checked Then ''when no transaction is selected and report open as WIP, then only production transactions open(24/03/2017)
                qry += " and Trans_Type in ('" + clsUserMgtCode.frmProcessProductionIssueEntry + "','" + clsUserMgtCode.frmProcessProductionStandardization + "','" + clsUserMgtCode.frmProcessProductionStageProcess + "','" + clsUserMgtCode.frmProductionEntry + "','" + clsUserMgtCode.frmWreckageBooking + "','Prod-Scrap','PP-PR') " + Environment.NewLine
            End If

            ''richa agarwal 29 Jan ,2021
            If chkExcludeConsumptionLoc.Checked Then
                qry += " And ( location_code not in (Select Location_code from tspl_location_master where TSPL_LOCATION_MASTER.Is_Consumption_Location =1) or Trans_Type Not  in ('PP_ISSUE','PP_STD-FQC','PRD_STG_PROC','PROD_ENTRY'))" + Environment.NewLine
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
            Dim strStockUOM As String = "(case when max(CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN Stock_UOM ELSE '' end)<>'' then max(CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN Stock_UOM ELSE '' end) else MAX(Stock_UOM) end ) as Stock_UOM"
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
            Else
                If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.stockRecoNewJR) = CompairStringResult.Equal Then
                    qry += " and (Location_Code in (select Location_Code from TSPL_LOCATION_MASTER where len(coalesce(Jobwork_Vendor,''))>0 and Is_Sub_Location='Y'))"
                End If
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    qry += "  and Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
                End If
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
            OuterOpClo = " convert(decimal(18,2),[OPBal]) as [OPBal],(case when OPBal<=0.11 or abs(cast((ISNULL(OPBalCost,0))as decimal(18,2)))<" + RateTunning + " then 0 else OPBalCost/OPBal end) as OPBalrate,case when convert(decimal(18,2),[OPQTYKG])<=0.11 or abs(Convert(decimal(18,2),(isnull([OPFAT],0))))<" + RateTunning + "  then 0 else convert(decimal(18,2),([OPFAT]*100/[OPQTYKG])) end as [OPFATPER],convert(decimal(18,2),[OPFAT]) as [OPFAT],case when convert(decimal(18,2),[OPQTYKG])<=0.11 or abs(Convert(decimal(18,2),(isnull([OPSNF],0))))<" + RateTunning + " then 0 else convert(decimal(18,2),([OPSNF]*100/[OPQTYKG])) end as [OPSNFPER],convert(decimal(18,2), [OPSNF]) as [OPSNF],OPBalCost,convert(decimal(18,2),Received_Qty) as Received_Qty,(case when Received_Qty=0 then 0 else RecdCost/Received_Qty end) as RecdRate, RecdCost ,case when convert(decimal(18,2),Received_QtyKG)=0 then 0 else convert(decimal(18,2),(Received_FAT*100/Received_QtyKG)) end as Received_FATPER,convert(decimal(18,2), Received_FAT) as Received_FAT,case when convert(decimal(18,2),Received_QTYKG)=0 then 0 else convert(decimal(18,2),(Received_SNF*100/Received_QTYKG)) end as Received_SNFPER,convert(decimal(18,2), Received_SNF) as Received_SNF,convert(decimal(18,2),Issued_Qty) as Issued_Qty, (case when Issued_Qty=0 then 0 else IssueCost/Issued_Qty end) as IssueRate,IssueCost,case when convert(decimal(18,2),Issued_QTYKG)=0 then 0 else convert(decimal(18,2),(Issued_FAT*100/Issued_QTYKG)) end as Issued_FATPER,convert(decimal(18,2), Issued_FAT) as Issued_FAT,case when convert(decimal(18,2),Issued_QTYKG)=0 then 0 else convert(decimal(18,2),(Issued_SNF*100/Issued_QTYKG)) end as Issued_SNFPER,convert(decimal(18,2) ,Issued_SNF) as Issued_SNF  ,convert(decimal(18,2),[Balance_Qty]) as [Balance_Qty],convert(decimal(28,3), case when Balance_Qty<=0.11 or abs(cast((ISNULL(Cost,0))as decimal(18,2)))<" + RateTunning + "  then 0 else Cost/Balance_Qty end) as Rate,Cost,case when convert(decimal(18,2),[Balance_QTYKG])<=0.11 or abs(Convert(decimal(18,2),(isnull(Balance_FAT,0))))<" + RateTunning + " then 0 else convert(decimal(18,2),([Balance_FAT]*100/[Balance_QTYKG])) end as [Balance_FATPER],convert(decimal(18,2), [Balance_FAT]) as  [Balance_FAT],case when convert(decimal(18,2),[Balance_QTYKG])<=0.11 or abs(Convert(decimal(18,2),(isnull([Balance_SNF],0))))<" + RateTunning + " then 0 else convert(decimal(18,2),([Balance_SNF]*100/[Balance_QTYKG])) end as [Balance_SNFPER],convert(decimal(18,2), [Balance_SNF]) as [Balance_SNF] "
            InnerOpClo = "  SUM(STOCK_QTY * (CASE WHEN PUNCHING_DAte < '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1.00 ELSE 0 end) * (case when InOut='I' then 1.00 else -1.00 end))  AS [OPBal]  ," &
                            " SUM(Cost  * (CASE WHEN PUNCHING_DAte < '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1.00 ELSE 0 end) * (case when InOut='I' then 1.00 else -1.00 end))  AS [OPBalCost]  , " &
                            " SUM(QtyKG * (CASE WHEN PUNCHING_DAte < '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1.00 ELSE 0 end) * (case when InOut='I' then 1.00 else -1.00 end))  AS OPQTYKG ," &
                            " SUM((case when IsFromMilk=1 then MilkFATKG else (STOCK_QTY*FatPer) end)  * (CASE WHEN PUNCHING_DAte < '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1.00 ELSE 0 end) * (case when InOut='I' then 1.00 else -1.00 end))  AS [OPFAT], " &
                            " SUM((case when IsFromMilk=1 then MilkSNFKG else (STOCK_QTY*SNFPer) end)  * (CASE WHEN PUNCHING_DAte < '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1.00 ELSE 0 end) * (case when InOut='I' then 1.00 else -1.00 end))  AS [OPSNF], " &
                            " SUM(STOCK_QTY * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1.00 ELSE 0 end) * (case when InOut='I' then 1.00 else 0 end))  AS Received_Qty , " &
                            " SUM(Cost * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1.00 ELSE 0 end) * (case when InOut='I' then 1.00 else 0 end))  AS RecdCost , " &
                            " SUM(QtyKG * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1.00 ELSE 0 end) * (case when InOut='I' then 1.00 else 0 end))  AS Received_QtyKG , " &
                            " SUM((case when IsFromMilk=1 then MilkFATKG else (STOCK_QTY*FatPer) end) * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1.00 ELSE 0 end) * (case when InOut='I' then 1.00 else 0 end))  AS Received_FAT , " &
                            " SUM((case when IsFromMilk=1 then MilkSNFKG else (STOCK_QTY*SNFPer) end) * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1.00 ELSE 0 end) * (case when InOut='I' then 1.00 else 0 end))  AS Received_SNF , " &
                            " SUM(STOCK_QTY * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1.00 ELSE 0 end) * (case when InOut='I' then 0 else 1.00 end))  AS Issued_Qty," &
                            " SUM(Cost * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1.00 ELSE 0 end) * (case when InOut='I' then 0 else 1.00 end))  AS IssueCost," &
                            " SUM(QtyKG * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1.00 ELSE 0 end) * (case when InOut='I' then 0 else 1.00 end))  AS Issued_QtyKG," &
                            " SUM((case when IsFromMilk=1 then MilkFATKG else (STOCK_QTY*FatPer) end) * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1.00 ELSE 0 end) * (case when InOut='I' then 0 else 1.00 end))  AS Issued_FAT," &
                            " SUM((case when IsFromMilk=1 then MilkSNFKG else (STOCK_QTY*SNFPer) end ) * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1.00 ELSE 0 end) * (case when InOut='I' then 0 else 1.00 end))  AS Issued_SNF," &
                            " SUM(STOCK_QTY * (CASE WHEN PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  THEN 1.00 ELSE 0 end) * (case when InOut='I' then 1.00 else -1.00 end))  AS [Balance_Qty]," &
                            " SUM(cost * (CASE WHEN PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  THEN 1.00 ELSE 0 end) * (case when InOut='I' then 1.00 else -1.00 end))  AS [Cost], " &
                            " SUM(QtyKG * (CASE WHEN PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  THEN 1.00 ELSE 0 end) * (case when InOut='I' then 1.00 else -1.00 end))  AS Balance_QtyKG," &
                            " SUM((case when IsFromMilk=1 then MilkFATKG else (STOCK_QTY*FatPer) end ) * (CASE WHEN PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  THEN 1.00 ELSE 0 end) * (case when InOut='I' then 1.00 else -1.00 end))  AS [Balance_FAT]," &
                            " SUM((case when IsFromMilk=1 then MilkSNFKG else (STOCK_QTY*SNFPer) end ) * (CASE WHEN PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  THEN 1.00 ELSE 0 end) * (case when InOut='I' then 1.00 else -1.00 end))  AS [Balance_SNF]"

            Dim strFinalQry As String = ""
            If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item Type Wise Summary") = CompairStringResult.Equal Then
                strFinalQry = "select  Item_Type,Item_Type_Name, "
                strFinalQry += OuterOpClo
                strFinalQry += " from (" + Environment.NewLine
                strFinalQry += " select  Item_Type,max(Item_Type_Name) as Item_Type_Name,"
                strFinalQry += InnerOpClo
                strFinalQry += "  from (" + qry + ")xxx Group by Item_Type )xxxx" + Environment.NewLine
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item Structure Wise Summary") = CompairStringResult.Equal Then
                strFinalQry = "select  Item_Type,Item_Type_Name,Structure_Code,Structure_Descq, "
                strFinalQry += OuterOpClo
                strFinalQry += " from (" + Environment.NewLine
                strFinalQry += " select Item_Type,max(Item_Type_Name) as Item_Type_Name, Structure_Code,max(Structure_Descq) as Structure_Descq,"
                strFinalQry += InnerOpClo
                strFinalQry += "  from (" + qry + ")xxx Group by Item_Type,Structure_Code )xxxx" + Environment.NewLine
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
                strFinalQry += " select  Item_Type,max(Item_Type_Name) as Item_Type_Name,Item_Group,max(Group_Description) as Group_Description, " + strCodeColumn + "," + strCodeDescColumnMax + ",Item_Category_Struct_Code,SUM(Stock_Qty * case when InOut='I' then 1.00 else -1.00 end) as Stock_Qty," + strStockUOM + ","
                strFinalQry += InnerOpClo
                strFinalQry += "  from (" + qry + ")xxx Group by Item_Type,Item_Group, Item_Category_Struct_Code," + strCodeColumn + " )xxxx" + Environment.NewLine
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item Wise Summary") = CompairStringResult.Equal Then
                strFinalQry = "select Item_Type,Item_Type_Name,Item_Group,Group_Description," + strCodeColumn + "," + strCodeDescColumn + ", Item_Category_Struct_Code  as Item_Category_Struct_Code,Item_Code,Item_Desc,itf_code,Stock_Qty,Stock_UOM,"
                strFinalQry += OuterOpClo
                If ChkMRPWise.Checked = True Then
                    strFinalQry += ",MRP "
                End If
                strFinalQry += " from (" + Environment.NewLine
                strFinalQry += " select  Item_Type,max(Item_Type_Name) as Item_Type_Name,Item_Group,max(Group_Description) as Group_Description," + strCodeColumnMax + "," + strCodeDescColumnMax + ",max(Item_Category_Struct_Code) as Item_Category_Struct_Code,Item_Code,MAX(Item_Desc) as Item_Desc,max(itf_code)as itf_code, SUM(Stock_Qty * case when InOut='I' then 1.00 else -1.00 end) as Stock_Qty," + strStockUOM + ","
                strFinalQry += InnerOpClo
                If ChkMRPWise.Checked = True Then
                    strFinalQry += ",MRP "
                End If
                strFinalQry += " from (" + qry + ")xxx Group by Item_Type,Item_Group,Item_Code"
                If ChkMRPWise.Checked Then
                    strFinalQry += ",MRP "
                End If
                strFinalQry += " )xxxx" + Environment.NewLine
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item And Location Wise Summary") = CompairStringResult.Equal Then
                ''BHA/13/09/18-000548 richa 
                strFinalQry = "select Item_Type,Item_Type_Name,Item_Group,Group_Description," + strCodeColumn + "," + strCodeDescColumn + ", Item_Category_Struct_Code  as Item_Category_Struct_Code,Main_Location_Code,MainLocationDesc,Location_Code,[Loc Desp],Item_Code,Item_Desc,Inventory_Control_Acc,Inventory_Control_Acc_desc, itf_code,Stock_Qty,Stock_UOM,"
                strFinalQry += OuterOpClo
                If ChkMRPWise.Checked = True Then
                    strFinalQry += ",MRP "
                End If
                strFinalQry += "  from (" + Environment.NewLine
                strFinalQry += " select  Item_Type,max(Item_Type_Name) as Item_Type_Name,Item_Group,max(Group_Description) as Group_Description," + strCodeColumnMax + "," + strCodeDescColumnMax + ",max(Item_Category_Struct_Code) as Item_Category_Struct_Code,Main_Location_Code,max(MainLocationDesc) as MainLocationDesc, Location_Code,max([Loc Desp]) as [Loc Desp],Item_Code,MAX(Item_Desc) as Item_Desc,max(itf_code)as itf_code, SUM(Stock_Qty * case when InOut='I' then 1.00 else -1.00 end) as Stock_Qty," + strStockUOM + ", max(Inventory_Control_Acc) as Inventory_Control_Acc,max(Inventory_Control_Acc_desc) as Inventory_Control_Acc_desc, "
                strFinalQry += InnerOpClo
                If ChkMRPWise.Checked = True Then
                    strFinalQry += ",MRP "
                End If
                strFinalQry += " from (" + qry + ")xxx Group by Item_Type,Item_Group,Item_Code,Main_Location_Code,Location_Code"
                If ChkMRPWise.Checked Then
                    strFinalQry += ",MRP "
                End If
                strFinalQry += " )xxxx" + Environment.NewLine

            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Document Wise Detail") = CompairStringResult.Equal Then
                strFinalQry = "select case when '" & LocationFirstTime & "'='1' then '" & LocationAddress & "' else '" + objCommonVar.CurrentCompanyName + " ' end as CompName,'" + clsCommon.myCDate(txtFromDate.Value, "dd/MMM/yyyy") + "' as FromDate,'" + clsCommon.myCDate(txtToDate.Value, "dd/MMM/yyyy") + "' as ToDate , Trans_Id,Trans_Type,Trans_Type_Name,Source_Doc_No,convert(varchar,Punching_Date,103) as Punching_Date,InOutView, InOut,Location_Code,[Loc Desp],PrimaryLocation,SourceCode,SourceName,SourceType ,Item_Type,Item_Type_Name,Item_Group,Group_Description," + strCodeColumn + "," + strCodeDescColumn + ",Item_Code ,Item_Desc,Item_Category_Struct_Code,Stock_UOM,itf_code ,Stock_Qty,case when (Source_Doc_No='Opening Balance' and ABS(isnull(cast(Stock_Qty as decimal(18,2)),0))<=0.11 or ABS(isnull(cast(Cost as decimal(18,2)),0))<" + RateTunning + " )then 0 else Rate end as Rate,Cost,isnull((CASE when (Balance_QTYKG<=0.11 or (Source_Doc_No='Opening Balance' and [Balance_FAT]<" + RateTunning + " ) ) then 0 else cast(([Balance_FAT]*100/[Balance_QTYKG]) as decimal(18,2)) end),0) as [Balance_FATPER] ,cast(Balance_FAT as decimal(18,6)) as Balance_FAT,isnull((CASE when (Balance_QTYKG<=0.11 or (Source_Doc_No='Opening Balance' and [Balance_SNF]<" + RateTunning + " ) ) then 0 else cast(([Balance_SNF]*100/[Balance_QTYKG]) as decimal(18,2)) end),0) as [Balance_SNFPER] , cast(Balance_SNF as decimal(18,6)) as Balance_SNF , cast(FATRATE as decimal(18,2)) as FATRATE, cast(SNFRATE as decimal(18,2)) as SNFRATE,cast(FATAmount as decimal(18,2)) as FATAmount,cast(SNFAmount as decimal(18,2)) as SNFAmount "
                If ChkMRPWise.Checked Then
                    strFinalQry += ",MRP "
                End If
                strFinalQry += " from ("
                strFinalQry += "select " + Environment.NewLine
                '"( sum((case when IsFromMilk=1 then (case when MilkFATKG=0 then Fat_Amt else MilkFATKG*Fat_Rate end) else (Stock_Qty*FatPer*Fat_Rate) end) * case when InOut='I' then 1 else -1 end)) AS FATAmount," + Environment.NewLine +
                '"( sum((case when IsFromMilk=1 then (case when MilkSNFKG=0 then SNF_Amt else MilkSNFKG*SNF_Rate end)  else (Stock_Qty*SNFPer*SNF_Rate) end ) * case when InOut='I' then 1 else -1 end)) AS SNFAmount," + Environment.NewLine +
                strFinalQry += "( sum((case when IsFromMilk=1 then Fat_Amt else (Stock_Qty*FatPer*Fat_Rate) end) * case when InOut='I' then 1.00 else -1.00 end)) AS FATAmount," + Environment.NewLine +
                    "( sum((case when IsFromMilk=1 then SNF_Amt  else (Stock_Qty*SNFPer*SNF_Rate) end ) * case when InOut='I' then 1.00 else -1.00 end)) AS SNFAmount," + Environment.NewLine +
                    "(case when ABS(sum((case when IsFromMilk=1 then MilkFATKG else (Stock_Qty*FatPer) end) * case when InOut='I' then 1.00 else -1.00 end))<=0.11 then 0 else sum((case when IsFromMilk=1 then (case when MilkFATKG=0 then Fat_Amt else MilkFATKG*Fat_Rate end)  else (Stock_Qty*FatPer*Fat_Rate) end) * case when InOut='I' then 1.00 else -1.00 end)/sum((case when IsFromMilk=1 then MilkFATKG else (Stock_Qty*FatPer) end) * case when InOut='I' then 1.00 else -1.00 end) end) AS FATRATE," + Environment.NewLine +
                    "(case when ABS(sum(((case when IsFromMilk=1 then MilkSNFKG else (Stock_Qty*SNFPer) end ) * case when InOut='I' then 1.00 else -1.00 end)))<=0.11 then 0 else sum(( (case when IsFromMilk=1 then (case when MilkSNFKG=0 then SNF_Amt else MilkSNFKG*SNF_Rate end)  else (Stock_Qty*SNFPer*SNF_Rate) end ) * case when InOut='I' then 1.00 else -1.00 end))/sum(( (case when IsFromMilk=1 then MilkSNFKG else (Stock_Qty*SNFPer) end ) * case when InOut='I' then 1.00 else -1.00 end)) end ) AS SNFRATE, " + Environment.NewLine +
                    "0 as Trans_Id,'' as Trans_Type,'' as Trans_Type_Name,'Opening Balance' as Source_Doc_No,null as Punching_Date,'' as InOutView, '' as InOut,'' as Location_Code,'' as [Loc Desp],'' as [LocAddress],'' as PrimaryLocation,'' as SourceCode,'' as SourceName,'' as SourceType ,'' as Item_Type,'' as Item_Type_Name,'' as Item_Group,'' as Group_Description," + strCodeColumnMax + "," + strCodeDescColumnMax + ",max(Item_Code)  as Item_Code ,max(Item_Desc)  as Item_Desc,'' as Item_Category_Struct_Code,'' as Stock_UOM,'' as itf_code ,sum( Stock_Qty * case when InOut='I' then 1.00 else -1.00 end) as Stock_Qty,sum( QtyKG * case when InOut='I' then 1.00 else -1.00 end) as Balance_QTYKG,case when sum( Stock_Qty * case when InOut='I' then 1.00 else -1.00 end) =0 then 0 else convert(decimal(28,3), sum(Cost * case when InOut='I' then 1.00 else -1.00 end)/sum( Stock_Qty * case when InOut='I' then 1.00 else -1.00 end)) end as Rate,sum(Cost * case when InOut='I' then 1.00 else -1.00 end) as Cost,sum( (case when IsFromMilk=1.00 then MilkFATKG else (Stock_Qty*FatPer) end) * case when InOut='I' then 1.00 else -1.00 end) as Balance_FAT,sum(( (case when IsFromMilk=1.00 then MilkSNFKG else (Stock_Qty*SNFPer) end ) * case when InOut='I' then 1.00 else -1.00 end)) as Balance_SNF "
                If ChkMRPWise.Checked Then
                    strFinalQry += ",Max(MRP) as MRP "
                End If
                strFinalQry += " from (" + qry + ") xxx " + Environment.NewLine
                strFinalQry += " where Punching_Date < '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' group by Item_Code  " + Environment.NewLine
                strFinalQry += " union all "
                strFinalQry += " select (Fat_Amt * case when InOut='I' then 1.00 else -1.00 end) as FATAmount,(SNF_Amt * case when InOut='I' then 1.00 else -1.00 end) as SNFAmount,Fat_Rate AS FATRATE ,SNF_Rate AS SNFRATE, Trans_Id,Trans_Type,Trans_Type_Name,Source_Doc_No,Punching_Date,InOutView, InOut,Location_Code,[Loc Desp], [LocAddress],PrimaryLocation,SourceCode,SourceName,SourceType ,Item_Type,Item_Type_Name,Item_Group,Group_Description," + strCodeColumn + "," + strCodeDescColumn + ",Item_Code ,Item_Desc,Item_Category_Struct_Code,Stock_UOM,itf_code ,( Stock_Qty * case when InOut='I' then 1.00 else -1.00 end) as Stock_Qty,(QtyKG * case when InOut='I' then 1.00 else -1.00 end) as Balance_QTYKG,convert(decimal(28,3),case when Stock_Qty=0 then 0 else Cost/Stock_Qty end) as Rate,(Cost * case when InOut='I' then 1.00 else -1.00 end) as Cost,( (case when IsFromMilk=1.00 then MilkFATKG else (Stock_Qty*FatPer) end) * case when InOut='I' then 1.00 else -1.00 end) as Balance_FAT, ( (case when IsFromMilk=1 then MilkSNFKG else (Stock_Qty*SNFPer) end ) * case when InOut='I' then 1.00 else -1.00 end) as Balance_SNF  "
                If ChkMRPWise.Checked Then
                    strFinalQry += ",MRP "
                End If
                strFinalQry += " from (" + qry + ") xxx " + Environment.NewLine
                strFinalQry += " where Punching_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' " + Environment.NewLine
                ''richa agarwal 04 Aug,2016
                strFinalQry += ")xxxxxx Order by convert(date,Punching_Date,103),Trans_Id"
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Date, Item And Document Wise Detail") = CompairStringResult.Equal Then
                '' change by Panch Raj against Ticket No: GKD/09/05/18-000128
                'strFinalQry = "select CompName,FromDate,ToDate,Trans_Id,Location_Code,[Loc Desp],SourceType ,Item_Type,Item_Type_Name,Item_Group,Group_Description," + strCodeColumn + "," + strCodeDescColumn + ",Item_Category_Struct_Code,itf_code ,Stock_Qty,Rate,Cost,SourceCode,SourceName,Source_Doc_No,Punching_Date,Trans_Type,Trans_Type_Name,InOut,InOutView,Item_Code ,Item_Desc,Stock_UOM, (ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0)) as OPQty, case when (ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0))=0 then 0 else  ((isnull(CLCost,0)-isnull(RecCost,0)+isnull(IssCost,0))/((ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0)))) end as OPRate, (isnull(CLBalance_FAT,0)-isnull(RecFAT,0)+isnull(IssFAT,0)) as OPFAT, isnull((CASE when (CLBalance_QTYKG-Balance_QTYKG) =0 then 0 else ((CLBalance_FAT-Balance_FAT)*100/(CLBalance_QTYKG-Balance_QTYKG)) end),0) as OPFATPER, (isnull(CLBalance_SNF,0)-isnull(RecSNF,0)+isnull(IssSNF,0)) as OPSNF, isnull((CASE when (CLBalance_QTYKG-Balance_QTYKG) =0 then 0 else ((CLBalance_SNF-Balance_SNF)*100/(CLBalance_QTYKG-Balance_QTYKG)) end),0) as OPSNFPER ,(isnull(CLCost,0)-isnull(RecCost,0)+isnull(IssCost,0)) as OPCost, RecQty,RecRate,RecFAT,RecFATPER,RecSNF,RecSNFPER,RecCost ,IssQty,IssRate,IssFAT,IssFATPER,IssSNF,IssSNFPER,IssCost ,CLQty ,case when isnull(CLQty,0)=0 then 0 else CLCost/CLQty end as CLRate, CLBalance_FAT as CLFAT, isnull((CASE when CLBalance_QTYKG=0 then 0 else (CLBalance_FAT*100/CLBalance_QTYKG) end),0) as CLFATPER, CLBalance_SNF as CLSNF, isnull((CASE when CLBalance_QTYKG=0 then 0 else (CLBalance_SNF*100/CLBalance_QTYKG) end),0) as CLSNFPER, CLCost from ( "
                strFinalQry = "select CompName,FromDate,ToDate,Trans_Id,Location_Code,[Loc Desp],SourceType ,Item_Type,Item_Type_Name,Item_Group,Group_Description," + strCodeColumn + "," + strCodeDescColumn + ",Item_Category_Struct_Code,itf_code ,Stock_Qty,Rate,Cost,SourceCode,SourceName,Source_Doc_No,Punching_Date,Trans_Type,Trans_Type_Name,InOut,InOutView,Item_Code ,Item_Desc,Stock_UOM, (ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0)) as OPQty, case when (ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0))=0 then 0 else  ((isnull(CLCost,0)-isnull(RecCost,0)+isnull(IssCost,0))/((ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0)))) end as OPRate, cast((isnull(CLBalance_FAT,0)-isnull(RecFAT,0)+isnull(IssFAT,0)) as decimal(18,2)) as OPFAT, isnull((CASE when (CLBalance_QTYKG-Balance_QTYKG) =0 then 0 else cast(((CLBalance_FAT-Balance_FAT)*100/(CLBalance_QTYKG-Balance_QTYKG)) as decimal(18,2)) end),0) as OPFATPER, cast((isnull(CLBalance_SNF,0)-isnull(RecSNF,0)+isnull(IssSNF,0)) as decimal(18,2)) as OPSNF, isnull((CASE when (CLBalance_QTYKG-Balance_QTYKG) =0 then 0 else cast(((CLBalance_SNF-Balance_SNF)*100/(CLBalance_QTYKG-Balance_QTYKG)) as decimal(18,2)) end),0) as OPSNFPER ,(isnull(CLCost,0)-isnull(RecCost,0)+isnull(IssCost,0)) as OPCost, RecQty,RecRate,cast(RecFAT as decimal(18,2)) as RecFAT,cast(RecFATPER as decimal(18,2)) as RecFATPER,cast(RecSNF as decimal(18,2)) as RecSNF,cast(RecSNFPER as decimal(18,2)) as RecSNFPER,RecCost ,IssQty,IssRate,cast(IssFAT as decimal(18,2)) as IssFAT,cast(IssFATPER as decimal(18,2)) as IssFATPER,cast(IssSNF as decimal(18,2)) as IssSNF,cast(IssSNFPER as decimal(18,2)) as IssSNFPER,IssCost ,CLQty ,case when isnull(CLQty,0)=0 then 0 else CLCost/CLQty end as CLRate, cast(CLBalance_FAT as decimal(18,2)) as CLFAT, isnull((CASE when CLBalance_QTYKG=0 then 0 else cast((CLBalance_FAT*100/CLBalance_QTYKG) as decimal(18,2)) end),0) as CLFATPER, cast(CLBalance_SNF as decimal(18,2)) as CLSNF, isnull((CASE when CLBalance_QTYKG=0 then 0 else cast((CLBalance_SNF*100/CLBalance_QTYKG) as decimal(18,2)) end),0) as CLSNFPER, CLCost from ( "
                strFinalQry += "select  case when '" & LocationFirstTime & "'='1' then '" & LocationAddress & "' else '" + objCommonVar.CurrentCompanyName + " ' end as CompName,'" + clsCommon.myCDate(txtFromDate.Value, "dd/MMM/yyyy") + "' as FromDate,'" + clsCommon.myCDate(txtToDate.Value, "dd/MMM/yyyy") + "' as ToDate ,  Trans_Id,Location_Code,[Loc Desp],SourceType ,Item_Type,Item_Type_Name,Item_Group,Group_Description ," + strCodeColumn + "," + strCodeDescColumn + ", Item_Category_Struct_Code,itf_code ,Stock_Qty,Rate,Cost,SourceCode,SourceName,Source_Doc_No,Punching_Date as Punching_Date,Trans_Type,Trans_Type_Name,InOut,InOutView, Item_Code ,Item_Desc,Stock_UOM,Balance_FAT,Balance_SNF,isnull(Balance_QTYKG,0) as Balance_QTYKG, (case when InOut='I' then Stock_Qty else 0 end) as RecQty,  (case when InOut='I' then Rate else 0 end) as RecRate, (case when InOut='I' then Balance_FAT else 0 end) as RecFAT, (case when InOut='I' then isnull((CASE when Balance_QTYKG=0 then 0 else (Balance_FAT*100/Balance_QTYKG) end),0) else 0 end ) as RecFATPER, (case when InOut='I' then Balance_SNF else 0 end) as RecSNF, (case when InOut='I' then isnull((CASE when Balance_QTYKG=0 then 0 else ([Balance_SNF]*100/[Balance_QTYKG]) end),0) else 0 end) as RecSNFPER, (case when InOut='I' then Cost else 0 end) as RecCost, (case when InOut='O' then -1.00*Stock_Qty else 0 end) as IssQty, (case when InOut='O' then Rate else 0 end) as IssRate, (case when InOut='O' then -1.00*Balance_FAT else 0 end) as IssFAT,(case when InOut='O' then isnull((CASE when Balance_QTYKG=0 then 0 else (Balance_FAT*100/Balance_QTYKG) end),0) else 0 end ) as IssFATPER, (case when InOut='O' then -1.00*Balance_SNF else 0 end) as IssSNF, (case when InOut='O' then isnull((CASE when Balance_QTYKG=0 then 0 else ([Balance_SNF]*100/[Balance_QTYKG]) end),0) else 0 end) as IssSNFPER, (case when InOut='O' then -1.00*Cost else 0 end) as IssCost, SUM(Stock_Qty) OVER (Partition BY Item_Code ORDER BY Item_Code, Punching_Date,Trans_Id) as CLQty  ,SUM(Cost) OVER (Partition BY Item_Code ORDER BY Item_Code, Punching_Date,Trans_Id) as CLCost,SUM(isnull(Balance_QTYKG,0)) OVER (Partition BY Item_Code ORDER BY Item_Code, Punching_Date,Trans_Id) as CLBalance_QTYKG ,SUM(Balance_FAT) OVER (Partition BY Item_Code ORDER BY Item_Code, Punching_Date,Trans_Id) as CLBalance_FAT,SUM(Balance_SNF) OVER (Partition BY Item_Code ORDER BY Item_Code, Punching_Date,Trans_Id) as CLBalance_SNF "
                If ChkMRPWise.Checked Then
                    strFinalQry += ",MRP "
                End If
                strFinalQry += " from ("
                strFinalQry += "select 0 as Trans_Id,'' as Trans_Type,'' as Trans_Type_Name,'Opening Balance' as Source_Doc_No,null as Punching_Date,'' as InOutView, '' as InOut,'' as Location_Code,'' as [Loc Desp],'' as [LocAddress],'' as SourceCode,'' as SourceName,'' as SourceType ,'' as Item_Type,'' as Item_Type_Name,'' as Item_Group,'' as Group_Description," + strCodeColumnMax + "," + strCodeDescColumnMax + ",xxx.Item_Code as Item_Code ,max(xxx.Item_Desc) as Item_Desc,'' as Item_Category_Struct_Code,max(xxx.Stock_UOM) as Stock_UOM,'' as itf_code ,sum( Stock_Qty * case when InOut='I' then 1.00 else -1.00 end) as Stock_Qty,sum( QtyKG * case when InOut='I' then 1.00 else -1.00 end) as Balance_QTYKG, case when sum(convert(decimal(28,3),Stock_Qty* case when InOut='I' then 1.00 else -1.00 end))=0 then 0 else sum(convert(decimal(28,3),Cost* case when InOut='I' then 1.00 else -1.00 end))/sum(convert(decimal(28,3),Stock_Qty* case when InOut='I' then 1.00 else -1.00 end)) end as Rate,sum(Cost * case when InOut='I' then 1.00 else -1.00 end) as Cost,sum( (case when IsFromMilk=1.00 then MilkFATKG else (Stock_Qty*FatPer) end) * case when InOut='I' then 1.00 else -1.00 end) as Balance_FAT,sum(( (case when IsFromMilk=1 then MilkSNFKG else (Stock_Qty*SNFPer) end ) * case when InOut='I' then 1.00 else -1.00 end)) as Balance_SNF "
                If ChkMRPWise.Checked Then
                    strFinalQry += ",Max(MRP) as MRP "
                End If
                strFinalQry += " from (" + qry + ") xxx " + Environment.NewLine
                strFinalQry += " where Punching_Date < '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' group by xxx.Item_Code " + Environment.NewLine
                strFinalQry += " union all "
                strFinalQry += " select Trans_Id,Trans_Type,Trans_Type_Name,Source_Doc_No,Punching_Date,InOutView, InOut,Location_Code,[Loc Desp], [LocAddress],SourceCode,SourceName,SourceType ,Item_Type,Item_Type_Name,Item_Group,Group_Description," + strCodeColumn + "," + strCodeDescColumn + ",Item_Code ,Item_Desc,Item_Category_Struct_Code,Stock_UOM,itf_code ,( Stock_Qty * case when InOut='I' then 1.00 else -1.00 end) as Stock_Qty,(QtyKG * case when InOut='I' then 1.00 else -1.00 end) as Balance_QTYKG,convert(decimal(28,3),case when Stock_Qty=0 then 0 else Cost/Stock_Qty end) as Rate,(Cost * case when InOut='I' then 1.00 else -1.00 end) as Cost,( (case when IsFromMilk=1 then MilkFATKG else (Stock_Qty*FatPer) end) * case when InOut='I' then 1 else -1.00 end) as Balance_FAT, ( (case when IsFromMilk=1 then MilkSNFKG else (Stock_Qty*SNFPer) end ) * case when InOut='I' then 1.00 else -1.00 end) as Balance_SNF  "
                If ChkMRPWise.Checked Then
                    strFinalQry += ",MRP "
                End If
                strFinalQry += " from (" + qry + ") xxx " + Environment.NewLine
                strFinalQry += " where Punching_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' " + Environment.NewLine

                strFinalQry += ")xxxxxx  )xxxxxxx where Trans_Id<>0  Order by  Punching_Date,Trans_Id"
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Date and Item Wise Stock") = CompairStringResult.Equal Then




                strFinalQry = "select *,(case when (CLRate=0 or IssRate=0) then 0 else (IssRate-CLRate) end) as DiffRate,(case when (CLRate=0 or IssRate=0) then 0 else (IssFATPER-CLFATPER) end) as DiffFATPer,(case when (CLRate=0 or IssRate=0) then 0 else (IssSNFPER-CLSNFPER) end) as DiffSNFPer from (select xxxxxxx.Location_Code,[Loc Desp],convert(varchar, Punching_Date,103) as Punching_Date  ,Item_Code ,Item_Desc,Stock_UOM, case when ( abs(cast((ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0))as  decimal(18,2)))<=0.11 or (abs(cast((ISNULL(CLCost,0) - isnull(RecCost,0)+isnull(IssCost,0))as  decimal(18,2)))<0.11 and tspl_location_master.Is_jobWork=0) ) then 0 else  Convert(decimal(18,3),(ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0))) end as OPQty," + Environment.NewLine +
                  " case when ( abs(cast((ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0))as  decimal(18,2)))<=0.11 or abs(cast((ISNULL(CLCost,0) - isnull(RecCost,0)+isnull(IssCost,0))as  decimal(18,2)))<" + RateTunning + ") then 0 else  Convert(decimal(18,2),((isnull(CLCost,0)-isnull(RecCost,0)+isnull(IssCost,0))/((ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0))))) end as OPRate," + Environment.NewLine +
                  " case when ( abs(cast((ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0))as  decimal(18,2)))<=0.11 or (abs(cast((ISNULL(CLCost,0) - isnull(RecCost,0)+isnull(IssCost,0))as  decimal(18,2)))<0.11 and tspl_location_master.Is_jobWork=0) ) then 0 else Convert(decimal(18,2),(isnull(CLBalance_FAT,0)-isnull(RecFAT,0)+isnull(IssFAT,0))) end as OPFAT" + Environment.NewLine +
                  ",Convert(decimal(18,2),(case when Convert(decimal(18,2),(ISNULL(CLBalance_QTYKG,0) - isnull(Balance_QTYKG,0)))<=0.11 or abs(Convert(decimal(18,2),(isnull(CLBalance_FAT,0)-isnull(RecFAT,0)+isnull(IssFAT,0))))<" + RateTunning + " then 0 else (Convert(decimal(18,2),(isnull(CLBalance_FAT,0)-isnull(RecFAT,0)+isnull(IssFAT,0))*100)/Convert(decimal(18,2),(ISNULL(CLBalance_QTYKG,0) - isnull(Balance_QTYKG,0)))) end)) as OPFATPER," + Environment.NewLine +
                  " case when ( abs(cast((ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0))as  decimal(18,2)))<=0.11 or (abs(cast((ISNULL(CLCost,0) - isnull(RecCost,0)+isnull(IssCost,0))as  decimal(18,2)))<0.11 and tspl_location_master.Is_jobWork=0) ) then 0 else Convert(decimal(18,2),(isnull(CLBalance_SNF,0)-isnull(RecSNF,0)+isnull(IssSNF,0))) end as OPSNF " + Environment.NewLine +
                  ",Convert(decimal(18,2),(case when Convert(decimal(18,2),(ISNULL(CLBalance_QTYKG,0) - isnull(Balance_QTYKG,0)))<=0.11 or abs(Convert(decimal(18,2),(isnull(CLBalance_SNF,0)-isnull(RecSNF,0)+isnull(IssSNF,0))) )<" + RateTunning + "  then 0 else (Convert(decimal(18,2),(isnull(CLBalance_SNF,0)-isnull(RecSNF,0)+isnull(IssSNF,0))*100)/Convert(decimal(18,2),(ISNULL(CLBalance_QTYKG,0) - isnull(Balance_QTYKG,0)))) end)) as OPSNFPER ," + Environment.NewLine +
                  "case when ( abs(cast((ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0))as  decimal(18,2)))<=0.11 or (abs(cast((ISNULL(CLCost,0) - isnull(RecCost,0)+isnull(IssCost,0))as  decimal(18,2)))<0.11 and tspl_location_master.Is_jobWork=0) ) then 0 else Convert(decimal(18,2),(isnull(CLCost,0)-isnull(RecCost,0)+ isnull(IssCost,0))) end as OPCost" + Environment.NewLine +
                ",(case when ( abs(cast((ISNULL(CLBalance_FAT,0) - isnull(RecFAT,0)+isnull(IssFAT,0))as  decimal(18,2)))<=0.11 or abs(cast((ISNULL(CLFATAmount,0) - isnull(RecFATAmount,0)+isnull(IssFATAmount,0))as  decimal(18,2)))<0.11) then 0 else  Convert(decimal(18,2),(  Convert(decimal(18,2),(isnull(CLFATAmount,0)-isnull(RecFATAmount,0)+isnull(IssFATAmount,0)))/Convert(decimal(18,2),((ISNULL(CLBalance_FAT,0) - isnull(RecFAT,0)+isnull(IssFAT,0)))))) end ) as OPFATRate" + Environment.NewLine +
",Convert(decimal(18,2),(isnull(CLFATAmount,0)-isnull(RecFATAmount,0)+ isnull(IssFATAmount,0))) as OPFATAmt" + Environment.NewLine +
",(case when ( abs(cast((ISNULL(CLBalance_SNF,0) - isnull(RecSNF,0)+isnull(IssSNF,0))as  decimal(18,2)))<=0.11 or abs(cast((ISNULL(CLSNFAmount,0) - isnull(RecSNFAmount,0)+isnull(IssSNFAmount,0))as  decimal(18,2)))<0.11) then 0 else  Convert(decimal(18,2),(  Convert(decimal(18,2),(isnull(CLSNFAmount,0)-isnull(RecSNFAmount,0)+isnull(IssSNFAmount,0)))/Convert(decimal(18,2),((ISNULL(CLBalance_SNF,0) - isnull(RecSNF,0)+isnull(IssSNF,0)))))) end ) as OPSNFRate" + Environment.NewLine +
",Convert(decimal(18,2),(isnull(CLSNFAmount,0)-isnull(RecSNFAmount,0)+ isnull(IssSNFAmount,0))) as OPSNFAmt" + Environment.NewLine +
                ", RecPurQty,RecPurRate,cast(RecPurFAT as decimal(18,2)) as RecPurFAT,cast(RecPurFATPER as decimal(18,2)) as RecPurFATPER,cast(RecPurSNF as decimal(18,2)) as RecPurSNF,cast(RecPurSNFPER as decimal(18,2)) as RecPurSNFPER,RecPurCost ,RecProQty, RecProRate ,cast( RecProFAT as decimal(18,2)) as  RecProFAT,cast( RecProFATPER as decimal(18,2)) as  RecProFATPER,cast( RecProSNF as decimal(18,2)) as  RecProSNF,cast( RecProSNFPER  as decimal(18,2)) as  RecProSNFPER, RecProCost,RecAdjQty,RecAdjRate ,cast(RecAdjFAT as decimal(18,2)) as RecAdjFAT,cast(RecAdjFATPER as decimal(18,2)) as RecAdjFATPER,cast(RecAdjSNF as decimal(18,2)) as RecAdjSNF,cast(RecAdjSNFPER  as decimal(18,2)) as RecAdjSNFPER,RecAdjCost ,RecOthQty,RecOthRate ,cast(RecOthFAT as decimal(18,2)) as RecOthFAT,cast(RecOthFATPER as decimal(18,2)) as RecOthFATPER,cast(RecOthSNF as decimal(18,2)) as RecOthSNF,cast(RecOthSNFPER as decimal(18,2)) as RecOthSNFPER,RecOthCost,RecQty,RecRate,cast(RecFAT as decimal(18,2)) as RecFAT,cast(RecFATPER as decimal(18,2)) as RecFATPER,cast(RecSNF as decimal(18,2)) as RecSNF,cast(RecSNFPER as decimal(18,2)) as RecSNFPER,RecCost  ,IssTransferQty ,IssTransferRate ,cast(IssTransferFAT as decimal(18,2)) as IssTransferFAT,cast(IssTransferFATPER as decimal(18,2)) as IssTransferFATPER,cast(IssTransferSNF as decimal(18,2)) as IssTransferSNF,cast(IssTransferSNFPER as decimal(18,2)) as IssTransferSNFPER ,IssTransferCost ,IssSaleQty ,IssSaleRate ,cast(IssSaleFAT as decimal(18,2)) as IssSaleFAT,cast(IssSaleFATPER as decimal(18,2)) as IssSaleFATPER,cast(IssSaleSNF as decimal(18,2)) as IssSaleSNF,cast(IssSaleSNFPER as decimal(18,2)) as IssSaleSNFPER ,IssSaleCost , IssIssAdjQty , IssIssAdjRate , cast(IssIssAdjFAT as decimal(18,2)) as IssIssAdjFAT, cast(IssIssAdjFATPER as decimal(18,2)) as IssIssAdjFATPER,cast(IssIssAdjSNF as decimal(18,2)) as IssIssAdjSNF, cast(IssIssAdjSNFPER as decimal(18,2)) as IssIssAdjSNFPER,IssIssAdjCost , IssOthQty , IssOthRate , cast(IssOthFAT as decimal(18,2)) as IssOthFAT,cast(IssOthFATPER as decimal(18,2)) as IssOthFATPER, cast(IssOthSNF as decimal(18,2)) as IssOthSNF,cast(IssOthSNFPER as decimal(18,2)) as IssOthSNFPER,IssOthCost ,IssQty,IssRate,cast(IssFAT as decimal(18,2)) as IssFAT,cast(IssFATPER as decimal(18,2)) as IssFATPER,cast(IssSNF as decimal(18,2)) as IssSNF,cast(IssSNFPER as decimal(18,2)) as IssSNFPER,IssCost ,case when (ABS(isnull(cast(CLQty as decimal(18,2)),0))<=0.11 or (ABS(isnull(cast(CLCost as decimal(18,2)),0))<0.11 and tspl_location_master.Is_jobWork=0) ) then 0 else CLQty end as CLQty," + Environment.NewLine +
                  "case when (ABS(isnull(cast(CLQty as decimal(18,2)),0))<=0.11 or ABS(isnull(cast(CLCost as decimal(18,2)),0))<" + RateTunning + " ) then 0 else CLCost/CLQty end as CLRate," + Environment.NewLine +
                  " cast(CLBalance_FAT as decimal(18,2)) as CLFAT, isnull((CASE when CLBalance_QTYKG<=0.11 or CLBalance_FAT<" + RateTunning + " then 0 else cast((CLBalance_FAT*100/CLBalance_QTYKG) as decimal(18,2)) end),0) as CLFATPER, cast(CLBalance_SNF as decimal(18,2)) as CLSNF, isnull((CASE when CLBalance_QTYKG<=0.11 or CLBalance_SNF<" + RateTunning + " then 0 else cast((CLBalance_SNF*100/CLBalance_QTYKG) as decimal(18,2)) end),0) as CLSNFPER, CLCost" + Environment.NewLine +
                ",case when (ABS(isnull(cast(CLBalance_FAT as decimal(18,2)),0))<=0.11 or ABS(isnull(cast(CLFATAmount as decimal(18,2)),0))<0.11 ) then 0 else convert(decimal(18,2),convert(decimal(18,2),CLFATAmount)/cast(CLBalance_FAT as decimal(18,2))) end as CLFATRate" + Environment.NewLine +
",case when (ABS(isnull(cast(CLQty as decimal(18,2)),0))<=0.11 or (ABS(isnull(cast(CLCost as decimal(18,2)),0))<0.11 and tspl_location_master.Is_jobWork=0) ) then 0 else convert(decimal(18,2),CLFATAmount) end as CLFATAmount " + Environment.NewLine +
",case when (ABS(isnull(cast(CLBalance_SNF as decimal(18,2)),0))<=0.11 or ABS(isnull(cast(CLSNFAmount as decimal(18,2)),0))<0.11 ) then 0 else convert(decimal(18,2),convert(decimal(18,2), CLSNFAmount)/cast(CLBalance_SNF as decimal(18,2))) end as CLSNFRate" + Environment.NewLine +
",case when (ABS(isnull(cast(CLQty as decimal(18,2)),0))<=0.11 or (ABS(isnull(cast(CLCost as decimal(18,2)),0))<0.11 and tspl_location_master.Is_jobWork=0) ) then 0 else convert(decimal(18,2), CLSNFAmount) end as CLSNFAmount " + Environment.NewLine +
                " from ( "
                strFinalQry += "select  Location_Code,max([Loc Desp]) as [Loc Desp],Punching_Date as Punching_Date, Item_Code ,max(Item_Desc) as Item_Desc, " + strStockUOM + ", sum(Balance_FAT) as Balance_FAT,sum(Balance_SNF) as Balance_SNF  ,sum(isnull(Balance_QTYKG,0)) as Balance_QTYKG ,sum(case when InOut='I' and In_Category in ('PU') then Stock_Qty else 0 end) as RecPurQty ,case when sum(case when InOut='I' and In_Category in ('PU') then Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='I' and In_Category in ('PU') then Cost else 0 end)/sum(case when InOut='I' and In_Category in ('PU') then Stock_Qty else 0 end)) end as RecPurRate  ,sum(case when InOut='I' and In_Category in ('PU') then Balance_FAT else 0 end) as RecPurFAT  ,(case when sum(case when InOut='I' and In_Category in ('PU') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category in ('PU') then  Balance_FAT else 0 end)*100/sum(case when InOut='I' and In_Category in ('PU') then  Balance_QTYKG else 0 end) end)  as RecPurFATPER  ,sum(case when InOut='I' and In_Category in ('PU') then Balance_SNF else 0 end) as RecPurSNF  ,(case when sum(case when InOut='I' and In_Category in ('PU') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category in ('PU') then  Balance_SNF else 0 end)*100/sum(case when InOut='I' and In_Category in ('PU') then  Balance_QTYKG else 0 end) end)  as RecPurSNFPER  ,sum(case when InOut='I' and In_Category in ('PU') then Cost else 0 end) as RecPurCost  ,sum(case when InOut='I' and Trans_Type='IC-AD' then Stock_Qty else 0 end) as RecAdjQty   ,case when sum(case when InOut='I' and Trans_Type='IC-AD' then Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='I' and Trans_Type='IC-AD' then Cost else 0 end)/sum(case when InOut='I' and Trans_Type='IC-AD' then Stock_Qty else 0 end)) end as RecAdjRate  ,sum(case when InOut='I' and Trans_Type='IC-AD' then Balance_FAT else 0 end) as RecAdjFAT  ,(case when sum(case when InOut='I' and Trans_Type='IC-AD' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and Trans_Type='IC-AD' then  Balance_FAT else 0 end)*100/sum(case when InOut='I' and Trans_Type='IC-AD' then  Balance_QTYKG else 0 end) end)  as RecAdjFATPER  ,sum(case when InOut='I' and Trans_Type='IC-AD' then Balance_SNF else 0 end) as RecAdjSNF  ,(case when sum(case when InOut='I' and Trans_Type='IC-AD' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and Trans_Type='IC-AD' then  Balance_SNF else 0 end)*100/sum(case when InOut='I' and Trans_Type='IC-AD' then  Balance_QTYKG else 0 end) end)  as RecAdjSNFPER  ,sum(case when InOut='I' and Trans_Type='IC-AD' then Cost else 0 end) as RecAdjCost , sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then Stock_Qty else 0 end) as RecProQty   ,case when sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then Cost else 0 end)/sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then Stock_Qty else 0 end)) end as RecProRate  ,sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then Balance_FAT else 0 end) as RecProFAT  ,(case when sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then  Balance_FAT else 0 end)*100/sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then  Balance_QTYKG else 0 end) end)  as RecProFATPER  ,sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then Balance_SNF else 0 end) as RecProSNF  ,(case when sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then  Balance_SNF else 0 end)*100/sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then  Balance_QTYKG else 0 end) end)  as RecProSNFPER  , sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then Cost else 0 end) as RecProCost ,sum(case when InOut='I' and In_Category not in ('AD','PU') then Stock_Qty else 0 end) as RecOthQty  ,case when sum(case when InOut='I' and In_Category not in ('AD','PU') then Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='I' and In_Category not in ('AD','PU') then Cost else 0 end)/sum(case when InOut='I' and In_Category not in ('AD','PU') then Stock_Qty else 0 end)) end as RecOthRate  ,sum(case when InOut='I' and In_Category not in ('AD','PU') then Balance_FAT else 0 end) as RecOthFAT  ,(case when sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_FAT else 0 end)*100/sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_QTYKG else 0 end) end)  as RecOthFATPER  ,sum(case when InOut='I' and In_Category not in ('AD','PU') then Balance_SNF else 0 end) as RecOthSNF ,(case when sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_SNF else 0 end)*100/sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_QTYKG else 0 end) end)  as RecOthSNFPER  ,sum(case when InOut='I' and In_Category not in ('AD','PU') then Cost else 0 end) as RecOthCost ,sum(case when InOut='I' then Stock_Qty else 0 end) as RecQty  ,case when cast(sum(case when InOut='I' then Stock_Qty else 0 end)as  decimal(18,2))=0 then 0 else (sum(case when InOut='I' then Cost else 0 end)/sum(case when InOut='I' then Stock_Qty else 0 end)) end as RecRate  ,sum(case when InOut='I' then Balance_FAT else 0 end) as RecFAT,(case when sum(case when InOut='I' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' then  Balance_FAT else 0 end)*100/sum(case when InOut='I' then  Balance_QTYKG else 0 end) end)  as RecFATPER  ,sum(case when InOut='I' then Balance_SNF else 0 end) as RecSNF  ,(case when sum(case when InOut='I' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' then  Balance_SNF else 0 end)*100/sum(case when InOut='I' then  Balance_QTYKG else 0 end) end)  as RecSNFPER  ,sum(case when InOut='I' then Cost else 0 end) as RecCost" + Environment.NewLine +
                ",sum(case when InOut='I' then FATAmount else 0 end) as RecFATAmount " + Environment.NewLine +
                ",sum(case when InOut='I' then SNFAmount else 0 end) as RecSNFAmount " + Environment.NewLine +
                "  ,sum(case when InOut='O' and Trans_Type='FS-SH' then -1.00*Stock_Qty else 0 end) as IssSaleQty  ,case when sum(case when InOut='O' and Trans_Type='FS-SH' then -1.00*Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='O' and Trans_Type='FS-SH' then -1.00*Cost else 0 end)/sum(case when InOut='O' and Trans_Type='FS-SH' then -1.00*Stock_Qty else 0 end)) end as IssSaleRate  ,sum(case when InOut='O' and Trans_Type='FS-SH' then -1.00*Balance_FAT else 0 end) as IssSaleFAT  ,(case when sum(case when InOut='O' and Trans_Type='FS-SH' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Trans_Type='FS-SH' then  Balance_FAT else 0 end)*100/sum(case when InOut='O' and Trans_Type='FS-SH' then  Balance_QTYKG else 0 end) end)  as IssSaleFATPER  ,sum(case when InOut='O' and Trans_Type='FS-SH' then -1.00*Balance_SNF else 0 end) as IssSaleSNF  ,(case when sum(case when InOut='O' and Trans_Type='FS-SH' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Trans_Type='FS-SH' then  Balance_SNF else 0 end)*100/sum(case when InOut='O' and Trans_Type='FS-SH' then  Balance_QTYKG else 0 end) end)  as IssSaleSNFPER  ,sum(case when InOut='O' and Trans_Type='FS-SH' then -1.00*Cost else 0 end) as IssSaleCost  ,sum(case when InOut='O' and Trans_Type='Transfer' then -1.00*Stock_Qty else 0 end) as IssTransferQty  ,case when sum(case when InOut='O' and Trans_Type='Transfer' then -1.00*Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='O' and Trans_Type='Transfer' then -1.00*Cost else 0 end)/sum(case when InOut='O' and Trans_Type='Transfer' then -1.00*Stock_Qty else 0 end)) end as IssTransferRate  ,sum(case when InOut='O' and Trans_Type='Transfer' then -1.00*Balance_FAT else 0 end) as IssTransferFAT  ,(case when sum(case when InOut='O' and Trans_Type='Transfer' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Trans_Type='Transfer' then  Balance_FAT else 0 end)*100/sum(case when InOut='O' and Trans_Type='Transfer' then  Balance_QTYKG else 0 end) end)  as IssTransferFATPER  ,sum(case when InOut='O' and Trans_Type='Transfer' then -1.00*Balance_SNF else 0 end) as IssTransferSNF  ,(case when sum(case when InOut='O' and Trans_Type='Transfer' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Trans_Type='Transfer' then  Balance_SNF else 0 end)*100/sum(case when InOut='O' and Trans_Type='Transfer' then  Balance_QTYKG else 0 end) end)  as IssTransferSNFPER  ,sum(case when InOut='O' and Trans_Type='Transfer' then -1.00*Cost else 0 end) as IssTransferCost,sum(case when InOut='O' and Out_Category in ('IS') then -1.00*Stock_Qty else 0 end) as IssIssAdjQty  ,case when sum(case when InOut='O' and Out_Category in ('IS') then -1.00*Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='O' and Out_Category in ('IS') then -1.00*Cost else 0 end)/sum(case when InOut='O' and Out_Category in ('IS') then -1.00*Stock_Qty else 0 end)) end as IssIssAdjRate  ,sum(case when InOut='O' and Out_Category in ('IS') then -1.00*Balance_FAT else 0 end) as IssIssAdjFAT  ,(case when sum(case when InOut='O' and Out_Category in ('IS') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category in ('IS') then  Balance_FAT else 0 end)*100/sum(case when InOut='O' and Out_Category in ('IS') then  Balance_QTYKG else 0 end) end)  as IssIssAdjFATPER  ,sum(case when InOut='O' and Out_Category in ('IS') then -1.00*Balance_SNF else 0 end) as IssIssAdjSNF  ,(case when sum(case when InOut='O' and Out_Category in ('IS') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category in ('IS') then  Balance_SNF else 0 end)*100/sum(case when InOut='O' and Out_Category in ('IS') then  Balance_QTYKG else 0 end) end)  as IssIssAdjSNFPER  ,sum(case when InOut='O' and Out_Category in ('IS') then -1.00*Cost else 0 end) as IssIssAdjCost ,sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1.00*Stock_Qty else 0 end) as IssOthQty  ,case when sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1.00*Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1.00*Cost else 0 end)/sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1.00*Stock_Qty else 0 end)) end as IssOthRate  ,sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1.00*Balance_FAT else 0 end) as IssOthFAT  ,(case when sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_FAT else 0 end)*100/sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_QTYKG else 0 end) end)  as IssOthFATPER  ,sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1.00*Balance_SNF else 0 end) as IssOthSNF  ,(case when sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_SNF else 0 end)*100/sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_QTYKG else 0 end) end)  as IssOthSNFPER  ,sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1.00*Cost else 0 end) as IssOthCost ,sum(case when InOut='O' then -1.00*Stock_Qty else 0 end) as IssQty  ,case when cast(sum(case when InOut='O' then -1.00*Stock_Qty else 0 end)as  decimal(18,2))=0 then 0 else (sum(case when InOut='O' then -1.00*Cost else 0 end)/sum(case when InOut='O' then -1.00*Stock_Qty else 0 end)) end as IssRate  ,sum(case when InOut='O' then -1.00*Balance_FAT else 0 end) as IssFAT  ,(case when sum(case when InOut='O' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' then  Balance_FAT else 0 end)*100/sum(case when InOut='O' then  Balance_QTYKG else 0 end) end)  as IssFATPER ,sum(case when InOut='O' then -1.00*Balance_SNF else 0 end) as IssSNF  ,(case when sum(case when InOut='O' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' then  Balance_SNF else 0 end)*100/sum(case when InOut='O' then  Balance_QTYKG else 0 end) end)  as IssSNFPER  ,sum(case when InOut='O' then -1.00*Cost else 0 end) as IssCost " + Environment.NewLine +
 ",sum(case when InOut='O' then -1.00*FATAmount else 0 end) as IssFATAmount" + Environment.NewLine +
",sum(case when InOut='O' then -1.00*SNFAmount else 0 end) as IssSNFAmount" + Environment.NewLine +
                " ,SUM(sum(Stock_Qty)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code,Punching_Date) as CLQty   ,SUM(sum(Cost)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code, Item_Code,Punching_Date) as CLCost ,SUM(sum(isnull(Balance_QTYKG,0))) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code, Punching_Date) as CLBalance_QTYKG  ,SUM(sum(Balance_FAT)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code, Punching_Date) as CLBalance_FAT ,SUM(sum(Balance_SNF)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code,Punching_Date) as CLBalance_SNF " + Environment.NewLine +
                ",SUM(sum(FATAmount)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code, Item_Code,Punching_Date) as CLFATAmount " + Environment.NewLine +
",SUM(sum(SNFAmount)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code, Item_Code,Punching_Date) as CLSNFAmount " + Environment.NewLine +
                "  from ("
                strFinalQry += "select 0 as Trans_Id,'' as Trans_Type,'' as Trans_Type_Name,'Opening Balance' as Source_Doc_No,null as Punching_Date,'' as InOutView, '' as InOut,xxx.Location_Code ,'' as [Loc Desp],'' as [LocAddress],'' as SourceCode,'' as SourceName,'' as SourceType ,'' as Item_Type,'' as Item_Type_Name,'' as Item_Group,'' as Group_Description," + strCodeColumnMax + "," + strCodeDescColumnMax + ",xxx.Item_Code as Item_Code ,max(xxx.Item_Desc) as Item_Desc,'' as Item_Category_Struct_Code,max(xxx.Stock_UOM) as Stock_UOM,'' as itf_code ,sum( Stock_Qty * case when InOut='I' then 1.00 else -1.00 end) as Stock_Qty,sum( QtyKG * case when InOut='I' then 1.00 else -1 end) as Balance_QTYKG, case when sum(convert(decimal(28,3),Stock_Qty* case when InOut='I' then 1 else -1 end))=0 then 0 else sum(convert(decimal(28,3),Cost* case when InOut='I' then 1 else -1 end))/sum(convert(decimal(28,3),Stock_Qty* case when InOut='I' then 1 else -1 end)) end as Rate,sum(Cost * case when InOut='I' then 1 else -1 end) as Cost,sum( (case when IsFromMilk=1 then MilkFATKG else (Stock_Qty*FatPer) end) * case when InOut='I' then 1 else -1 end) as Balance_FAT,sum(( (case when IsFromMilk=1 then MilkSNFKG else (Stock_Qty*SNFPer) end ) * case when InOut='I' then 1 else -1 end)) as Balance_SNF " + Environment.NewLine
                strFinalQry += ",( sum((case when IsFromMilk=1 then Fat_Amt  else (Stock_Qty*FatPer*Fat_Rate) end) * case when InOut='I' then 1 else -1 end)) AS FATAmount" + Environment.NewLine +
",(sum((case when IsFromMilk=1 then SNF_Amt else (Stock_Qty*SNFPer*SNF_Rate) end ) * case when InOut='I' then 1 else -1 end)) AS SNFAmount " + Environment.NewLine +
                " ,   '' as In_Category,'' as Out_Category"
                If ChkMRPWise.Checked Then
                    strFinalQry += ",Max(MRP) as MRP "
                End If
                strFinalQry += " from (" + qry + ") xxx " + Environment.NewLine
                strFinalQry += " where Punching_Date < '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' group by xxx.Item_Code,xxx.Location_Code " + Environment.NewLine
                strFinalQry += " union all "
                strFinalQry += " select Trans_Id,Trans_Type,Trans_Type_Name,Source_Doc_No,cast(Punching_Date as date) as Punching_Date,InOutView, InOut,Location_Code,[Loc Desp], [LocAddress],SourceCode,SourceName,SourceType ,Item_Type," &
                    " Item_Type_Name,Item_Group,Group_Description," + strCodeColumn + "," + strCodeDescColumn + ",Item_Code ,Item_Desc,Item_Category_Struct_Code,Stock_UOM,itf_code ," &
                    " ( Stock_Qty * case when InOut='I' then 1 else -1 end) as Stock_Qty,(QtyKG * case when InOut='I' then 1 else -1 end) as Balance_QTYKG, " &
                    " convert(decimal(28,3),case when Stock_Qty=0 then 0 else Cost/Stock_Qty end) as Rate,(Cost * case when InOut='I' then 1 else -1 end) as Cost," &
                    " ( (case when IsFromMilk=1 then MilkFATKG else (Stock_Qty*FatPer) end) * case when InOut='I' then 1 else -1 end) as Balance_FAT, " &
                    " ( (case when IsFromMilk=1 then MilkSNFKG else (Stock_Qty*SNFPer) end ) * case when InOut='I' then 1 else -1 end) as Balance_SNF" + Environment.NewLine +
",(Fat_Amt * case when InOut='I' then 1 else -1 end) as FATAmount" + Environment.NewLine +
",(SNF_Amt * case when InOut='I' then 1 else -1 end) as SNFAmount" + Environment.NewLine +
                " ,In_Category,Out_Category  "
                If ChkMRPWise.Checked Then
                    strFinalQry += ",MRP "
                End If
                strFinalQry += " from (" + qry + ") xxx " + Environment.NewLine
                strFinalQry += " where Punching_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' " + Environment.NewLine

                strFinalQry += " union  all "
                strFinalQry += "select Trans_Id,Trans_Type,Trans_Type_Name,Source_Doc_No,Punching_Date,InOutView,InOut,Location_Code,[Loc Desp],[LocAddress],SourceCode,SourceName,SourceType ,Item_Type," &
               " Item_Type_Name,Item_Group,Group_Description," + strCodeColumnNull + "," + strCodeDescColumnNull + ",Items.Item_Code,Item_Desc, Item_Category_Struct_Code, " &
               " Items.Stock_UOM ,itf_code ,Stock_Qty,Balance_QTYKG,Rate,Cost,Balance_FAT, Balance_SNF " + Environment.NewLine +
",0 as FATAmount" + Environment.NewLine +
",0 as SNFAmount" + Environment.NewLine +
                " ,In_Category,Out_Category "
                If ChkMRPWise.Checked Then
                    strFinalQry += ",MRP "
                End If
                strFinalQry += " from (SELECT 0 as Trans_Id,null as Trans_Type,null as Trans_Type_Name, null as Source_Doc_No, thedate as Punching_Date,'In' as InOutView,'I' as InOut,TSPL_LOCATION_MASTER.Location_Code as Location_Code,TSPL_LOCATION_MASTER.Location_Desc as [Loc Desp],null as [LocAddress],null as SourceCode,null as SourceName,null as SourceType ,TSPL_ITEM_MASTER.Item_Type,null as Item_Type_Name,null as Item_Group,null as Group_Description," + strCodeColumnNull + "," + strCodeDescColumnNull + ",TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER.Item_Desc,null as Item_Category_Struct_Code,TSPL_ITEM_UOM_DETAIL.UOM_Code as Stock_UOM ,null as  itf_code ,0 as Stock_Qty,0 as Balance_QTYKG,0 as Rate,0 as Cost,0 as Balance_FAT, 0 as Balance_SNF ,null as In_Category,null as Out_Category,TSPL_ITEM_MASTER.Product_Type  "
                If ChkMRPWise.Checked Then
                    strFinalQry += ",0 as MRP "
                End If
                strFinalQry += " FROM ExplodeDates( " + IIf(chkNoTransaction.Checked, "'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'", "'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "'") + ",'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "') as d,TSPL_ITEM_MASTER,TSPL_LOCATION_MASTER,TSPL_ITEM_UOM_DETAIL where 2=2 "
                If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                    strFinalQry += " and TSPL_ITEM_MASTER.Item_Code in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ") "
                End If
                ''richa  ERO/14/06/21-001409 
                If txtItemType.arrValueMember IsNot Nothing AndAlso txtItemType.arrValueMember.Count > 0 Then
                    strFinalQry += " and TSPL_ITEM_MASTER.Item_Type in (" + clsCommon.GetMulcallString(txtItemType.arrValueMember) + ") " + Environment.NewLine
                End If

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
                    strFinalQry += " and (" + strWhrCatg + ")"
                Else
                    If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                        strFinalQry += "  and Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
                    End If
                End If

                strFinalQry += "  and TSPL_ITEM_UOM_DETAIL.Stocking_Unit='Y' and TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code) Items" &
                " left join (select TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code,TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range as Fat_Per  from TSPL_ITEM_QC_PARAMETER_MASTER " &
                " left join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where TSPL_PARAMETER_MASTER.Type='FAT') Fat on Items.Item_Code=Fat.Item_Code " &
                " left join  (select TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code,TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range as SNF_Per  from TSPL_ITEM_QC_PARAMETER_MASTER " &
                " left join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where TSPL_PARAMETER_MASTER.Type='SNF') as SNF on Items.Item_Code=SNF.Item_Code where 2=2 "
                If chkShowTransactionData.Checked = True Then
                    strFinalQry += " and Stock_Qty<>0 "
                End If
                If isDataLoad AndAlso SkipCheckFatAndSNF Then
                    ''never want to check fat% and snf% cond. when open from double click in production(26/05/2014)
                Else
                    If clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal Then
                        strFinalQry += " and (coalesce(Fat.Fat_Per,0)<>0  or  coalesce(SNF.SNF_Per,0)<>0 or Items.Product_Type='MI') "
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "NFS") = CompairStringResult.Equal Then
                        strFinalQry += " and (coalesce(Fat.Fat_Per,0)=0  and  coalesce(SNF.SNF_Per,0)=0 and Items.Product_Type<>'MI') "
                    End If
                End If
                strFinalQry += " )xxxxxx Group by  Item_Code,Location_Code,Punching_Date )xxxxxxx left outer join tspl_location_master on tspl_location_master.Location_Code=xxxxxxx.Location_code where Punching_Date is not null )x Order by  convert(date,  Punching_Date,103),Location_Code"
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Transaction Wise") = CompairStringResult.Equal Then
                Dim strTransCatg As String = ""
                Dim strTransName As String = ""
                Dim TransQry As String = "select code,Name,case when inouttype='In' then 'I' else 'O' end as InOutType,In_Category,Out_Category,isnull(Sequence,500) as Sequence,Type as Typed from TSPL_INVENTORY_SOURCE_CODE order by Sequence asc "
                Dim TransResult As DataTable = clsDBFuncationality.GetDataTable(TransQry)
                If TransResult IsNot Nothing AndAlso TransResult.Rows.Count > 0 Then
                    For ii As Integer = 0 To TransResult.Rows.Count - 1
                        If ii <> 0 Then
                            strTransCatg += ","
                            strTransName += ","
                        End If
                        strTransCatg += " sum(case when InOut='" + TransResult.Rows(ii)("InOutType") + "' and code in ('" + TransResult.Rows(ii)("Code") + "') then Stock_Qty else 0 end) as '" + TransResult.Rows(ii)("Name") + "'"
                        strTransCatg += " ,MAX(isnull(convert(decimal(18,2),(CASE when Balance_QTYKG=0 then 0 else ([Balance_FAT]*100/[Balance_QTYKG]) end)),0)) as '" + TransResult.Rows(ii)("Name") + " Fat_%', sum(convert(decimal(18,2),Balance_FAT))  as [" + TransResult.Rows(ii)("Name") + "_Fat_KG]"
                        strTransCatg += " ,max(isnull(convert(decimal(18,2),(CASE when Balance_QTYKG=0 then 0 else ([Balance_SNF]*100/[Balance_QTYKG]) end)),0)) as '" + TransResult.Rows(ii)("Name") + " SNF_%', sum(convert(decimal(18,2),Balance_SNF)) as [" + TransResult.Rows(ii)("Name") + "_SNF_KG]"
                        strTransName += "[" + TransResult.Rows(ii)("Name") + "]"
                        strTransName += " ,[" + TransResult.Rows(ii)("Name") + " Fat_%], [" + TransResult.Rows(ii)("Name") + "_Fat_KG]"
                        strTransName += " ,[" + TransResult.Rows(ii)("Name") + " SNF_%], [" + TransResult.Rows(ii)("Name") + "_SNF_KG]"
                        'Ticket No-ERO/05/06/19-000634 Sanjay,Add TS% ,TSKG
                        strTransName += " , [" + TransResult.Rows(ii)("Name") + " Fat_%] + [" + TransResult.Rows(ii)("Name") + " SNF_%] as [" + TransResult.Rows(ii)("Name") + " TS_%]"
                        strTransName += " ,[" + TransResult.Rows(ii)("Name") + "_Fat_KG] + [" + TransResult.Rows(ii)("Name") + "_SNF_KG] as [" + TransResult.Rows(ii)("Name") + "_TS_KG]"
                    Next
                End If
                strWhrCatg = ""
                Dim IsApplicable As Boolean = False
                For ii As Integer = 0 To gvLocation.RowCount - 1
                    If clsCommon.myCBool(gvLocation.Rows(ii).Cells("SEL").Value) Then
                        If IsApplicable Then
                            strWhrCatg += " , "
                        End If
                        strWhrCatg += "'" + clsCommon.myCstr(gvLocation.Rows(ii).Cells("CODE").Value) + "'"

                    End If
                Next
                'strFinalQry = "select Location_Code as [Location Code],[Loc Desc],Main_Location_Code as [Main Location],MainLocationDesc as [Main Location Desc],Item_Code as [Item Code] ,Item_Desc as [Item Description],Stock_UOM as UOM," + strCodeColumn + "," + strCodeDescColumn + ", (ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0)) as OPQty, case when (ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0))=0 then 0 else  ((isnull(CLCost,0)-isnull(RecCost,0)+isnull(IssCost,0))/((ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0)))) end as OPRate , (isnull(CLBalance_FAT,0)-isnull(RecFAT,0)+isnull(IssFAT,0)) as OPFAT,isnull((CASE when (CLBalance_QTYKG+Balance_QTYKG) =0 then 0 else ((CLBalance_FAT+Balance_FAT)*100/(CLBalance_QTYKG+Balance_QTYKG)) end),0) as OPFATPER,(isnull(CLBalance_SNF,0)-isnull(RecSNF,0)+isnull(IssSNF,0)) as OPSNF,isnull((CASE when (CLBalance_QTYKG+Balance_QTYKG) =0 then 0 else ((CLBalance_SNF+Balance_SNF)*100/(CLBalance_QTYKG+Balance_QTYKG)) end),0) as OPSNFPER ,(isnull(CLCost,0)-isnull(RecCost,0)+isnull(IssCost,0)) as OPCost," + strTransName + " ,CLQty ,case when isnull(CLQty,0)=0 then 0 else CLCost/CLQty end as CLRate, CLBalance_FAT as CLFAT, isnull((CASE when CLBalance_QTYKG=0 then 0 else (CLBalance_FAT*100/CLBalance_QTYKG) end),0) as CLFATPER, CLBalance_SNF as CLSNF, isnull((CASE when CLBalance_QTYKG=0 then 0 else (CLBalance_SNF*100/CLBalance_QTYKG) end),0) as CLSNFPER, CLCost from ( "
                strFinalQry = "select Location_Code as [Location Code],[Loc Desc],Main_Location_Code as [Main Location],MainLocationDesc as [Main Location Desc],Item_Code as [Item Code] ,Item_Desc as [Item Description],Stock_UOM as UOM," + strCodeColumn + "," + strCodeDescColumn + ", (ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0)) as OPQty, case when (ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0))=0 then 0 else  ((isnull(CLCost,0)-isnull(RecCost,0)+isnull(IssCost,0))/((ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0)))) end as OPRate , cast((isnull(CLBalance_FAT,0)-isnull(RecFAT,0)+isnull(IssFAT,0)) as decimal(18,2)) as OPFAT,isnull((CASE when (CLBalance_QTYKG+Balance_QTYKG) =0 then 0 else cast(((CLBalance_FAT+Balance_FAT)*100/(CLBalance_QTYKG+Balance_QTYKG)) as decimal(18,2)) end),0) as OPFATPER,cast((isnull(CLBalance_SNF,0)-isnull(RecSNF,0)+isnull(IssSNF,0)) as decimal(18,2)) as OPSNF,isnull((CASE when (CLBalance_QTYKG+Balance_QTYKG) =0 then 0 else cast(((CLBalance_SNF+Balance_SNF)*100/(CLBalance_QTYKG+Balance_QTYKG)) as decimal(18,2)) end),0) as OPSNFPER ,(isnull(CLCost,0)-isnull(RecCost,0)+isnull(IssCost,0)) as OPCost," + strTransName + " ,CLQty ,case when isnull(CLQty,0)=0 then 0 else CLCost/CLQty end as CLRate, cast(CLBalance_FAT as decimal(18,2)) as CLFAT, isnull((CASE when CLBalance_QTYKG=0 then 0 else cast((CLBalance_FAT*100/CLBalance_QTYKG) as decimal(18,2)) end),0) as CLFATPER, cast(CLBalance_SNF as decimal(18,2)) as CLSNF, isnull((CASE when CLBalance_QTYKG=0 then 0 else cast((CLBalance_SNF*100/CLBalance_QTYKG) as decimal(18,2)) end),0) as CLSNFPER, CLCost from ( "
                strFinalQry += "select  Location_Code,max([Loc Desc]) as [Loc Desc],max(Main_Location_Code) as Main_Location_Code,max(MainLocationDesc) as MainLocationDesc, Item_Code ,max(Item_Desc) as Item_Desc, MAX(Stock_UOM) as Stock_UOM," + strCodeColumnMax + "," + strCodeDescColumnMax + ", sum(Balance_FAT) as Balance_FAT,sum(Balance_SNF) as Balance_SNF ,sum(case when InOut='I' then Stock_Qty else 0 end) as RecQty ,SUM(sum(Stock_Qty)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code) as CLQty,sum(case when InOut='O' then -1*Stock_Qty else 0 end) as IssQty,SUM(sum(Cost)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code, Item_Code,Location_Code) as CLCost,(case when sum(case when InOut='I' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' then  Balance_SNF else 0 end)*100/sum(case when InOut='I' then  Balance_QTYKG else 0 end) end)  as RecSNFPER  ,sum(case when InOut='I' then Cost else 0 end) as RecCost,sum(case when InOut='O' then -1*Cost else 0 end) as IssCost,SUM(sum(Balance_FAT)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code, Location_Code) as CLBalance_FAT ,SUM(sum(Balance_SNF)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code) as CLBalance_SNF ,sum(case when InOut='O' then -1*Balance_FAT else 0 end) as IssFAT  ,(case when sum(case when InOut='O' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' then  Balance_FAT else 0 end)*100/sum(case when InOut='O' then  Balance_QTYKG else 0 end) end)  as IssFATPER ,sum(case when InOut='O' then -1*Balance_SNF else 0 end) as IssSNF  ,(case when sum(case when InOut='O' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' then  Balance_SNF else 0 end)*100/sum(case when InOut='O' then  Balance_QTYKG else 0 end) end)  as IssSNFPER ,sum(case when InOut='I' then Balance_FAT else 0 end) as RecFAT ,sum(case when InOut='I' then Balance_SNF else 0 end) as RecSNF ,SUM(sum(isnull(Balance_QTYKG,0))) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code, Location_Code) as CLBalance_QTYKG , " + strTransCatg + "  ,sum(isnull(Balance_QTYKG,0)) as Balance_QTYKG  from ("
                strFinalQry += "select 0 as Trans_Id,'' as Trans_Type,'' as Trans_Type_Name,'Opening Balance' as Source_Doc_No,xxx.Location_Code,max(xxx.[Loc Desp]) as [Loc Desc],max(xxx.Main_Location_Code) as Main_Location_Code,max(xxx.MainLocationDesc) as MainLocationDesc,'' as InOutView, '' as InOut,'' as SourceCode,'' as SourceName,'' as SourceType ,'' as Item_Type,'' as Item_Type_Name,'' as Item_Group,'' as Group_Description," + strCodeColumnMax + "," + strCodeDescColumnMax + ",xxx.Item_Code as Item_Code ,max(xxx.Item_Desc) as Item_Desc,'' as Item_Category_Struct_Code,max(xxx.Stock_UOM) as Stock_UOM,'' as itf_code ,sum( Stock_Qty * case when InOut='I' then 1 else -1 end) as Stock_Qty,sum( QtyKG * case when InOut='I' then 1 else -1 end) as Balance_QTYKG, case when sum(convert(decimal(28,3),Stock_Qty* case when InOut='I' then 1 else -1 end))=0 then 0 else sum(convert(decimal(28,3),Cost* case when InOut='I' then 1 else -1 end))/sum(convert(decimal(28,3),Stock_Qty* case when InOut='I' then 1 else -1 end)) end as Rate,sum(Cost * case when InOut='I' then 1 else -1 end) as Cost,sum( (case when IsFromMilk=1 then MilkFATKG else (Stock_Qty*FatPer) end) * case when InOut='I' then 1 else -1 end) as Balance_FAT,sum(( (case when IsFromMilk=1 then MilkSNFKG else (Stock_Qty*SNFPer) end ) * case when InOut='I' then 1 else -1 end)) as Balance_SNF,'' as In_Category,'' as Out_Category,'' as Code "
                If ChkMRPWise.Checked Then
                    strFinalQry += ",Max(MRP) as MRP "
                End If
                strFinalQry += " from (" + qry + ") xxx " + Environment.NewLine
                strFinalQry += " where Punching_Date < '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' group by xxx.Item_Code,xxx.Location_Code " + Environment.NewLine
                strFinalQry += " union all "
                strFinalQry += " select Trans_Id,Trans_Type,Trans_Type_Name,Source_Doc_No,Location_Code,[Loc Desp],Main_Location_Code,MainLocationDesc,InOutView, InOut,SourceCode,SourceName,SourceType ,Item_Type,Item_Type_Name,Item_Group,Group_Description," + strCodeColumn + "," + strCodeDescColumn + ",Item_Code ,Item_Desc,Item_Category_Struct_Code,Stock_UOM,itf_code ,( Stock_Qty * case when InOut='I' then 1 else -1 end) as Stock_Qty,(QtyKG * case when InOut='I' then 1 else -1 end) as Balance_QTYKG,convert(decimal(28,3),case when Stock_Qty=0 then 0 else Cost/Stock_Qty end) as Rate,(Cost * case when InOut='I' then 1 else -1 end) as Cost,( (case when IsFromMilk=1 then MilkFATKG else (Stock_Qty*FatPer) end) * case when InOut='I' then 1 else -1 end) as Balance_FAT, ( (case when IsFromMilk=1 then MilkSNFKG else (Stock_Qty*SNFPer) end ) * case when InOut='I' then 1 else -1 end) as Balance_SNF ,In_Category,Out_Category,code  "
                If ChkMRPWise.Checked Then
                    strFinalQry += ",MRP "
                End If
                strFinalQry += " from (" + qry + ") xxx " + Environment.NewLine
                strFinalQry += " where Punching_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' " + Environment.NewLine
                strFinalQry += " )xxxxxx Group by  Item_Code,Location_Code )xxxxxxx where 2=2  Order by item_code" ''Punching_Date is not null
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Document Wise Detail Ledger") = CompairStringResult.Equal Then
                strFinalQry = "select  case when '" & LocationFirstTime & "'='1' then '" & LocationAddress & "' else '" + objCommonVar.CurrentCompanyName + " ' end as CompName,'" + clsCommon.myCDate(txtFromDate.Value, "dd/MMM/yyyy") + "' as FromDate,'" + clsCommon.myCDate(txtToDate.Value, "dd/MMM/yyyy") + "' as ToDate ,  Trans_Id,Location_Code,[Loc Desp],SourceType ,Item_Type,Item_Type_Name,Item_Group,Group_Description ," + strCodeColumn + "," + strCodeDescColumn + ", Item_Category_Struct_Code,itf_code ,Stock_Qty,Rate,Cost,isnull((CASE when Balance_QTYKG=0 then 0 else (Balance_FAT*100/Balance_QTYKG) end),0) as Balance_FATPER ,Balance_FAT,isnull((CASE when Balance_QTYKG=0 then 0 else ([Balance_SNF]*100/[Balance_QTYKG]) end),0) as [Balance_SNFPER] , Balance_SNF,SourceCode,SourceName,Source_Doc_No,convert(varchar,Punching_Date,103) as Punching_Date,Trans_Type,Trans_Type_Name,InOut,InOutView, Item_Code ,Item_Desc,Stock_UOM, (case when InOut='I' then Stock_Qty else 0 end) as RecQty, (case when InOut='I' then Rate else 0 end) as RecRate, (case when InOut='I' then Cost else 0 end) as RecCost, (case when InOut='O' then -1*Stock_Qty else 0 end) as IssQty, (case when InOut='O' then Rate else 0 end) as IssRate, (case when InOut='O' then -1*Cost else 0 end) as IssCost, SUM(Stock_Qty) OVER (Partition BY Item_Code ORDER BY Item_Code, Punching_Date) as CLQty ,( case when SUM(Stock_Qty) OVER (Partition BY Item_Code ORDER BY Item_Code, Punching_Date)=0 then 0  else SUM(Cost) OVER (Partition BY Item_Code ORDER BY Item_Code, Punching_Date)/SUM(Stock_Qty) OVER (Partition BY Item_Code ORDER BY Item_Code, Punching_Date) end) as CLRate ,SUM(Cost) OVER (Partition BY Item_Code ORDER BY Item_Code, Punching_Date) as CLCost "
                If ChkMRPWise.Checked Then
                    strFinalQry += ",MRP "
                End If
                strFinalQry += " from ("
                strFinalQry += "select 0 as Trans_Id,'' as Trans_Type,'' as Trans_Type_Name,'Opening Balance' as Source_Doc_No,null as Punching_Date,'' as InOutView, '' as InOut,'' as Location_Code,'' as [Loc Desp],'' as [LocAddress],'' as SourceCode,'' as SourceName,'' as SourceType ,'' as Item_Type,'' as Item_Type_Name,'' as Item_Group,'' as Group_Description," + strCodeColumnMax + "," + strCodeDescColumnMax + ",xxx.Item_Code as Item_Code ,max(xxx.Item_Desc) as Item_Desc,'' as Item_Category_Struct_Code,max(xxx.Stock_UOM) as Stock_UOM,'' as itf_code ,sum( Stock_Qty * case when InOut='I' then 1 else -1 end) as Stock_Qty,sum( QtyKG * case when InOut='I' then 1 else -1 end) as Balance_QTYKG, case when sum(convert(decimal(28,3),Stock_Qty* case when InOut='I' then 1 else -1 end))=0 then 0 else sum(convert(decimal(28,3),Cost* case when InOut='I' then 1 else -1 end))/sum(convert(decimal(28,3),Stock_Qty* case when InOut='I' then 1 else -1 end)) end as Rate,sum(Cost * case when InOut='I' then 1 else -1 end) as Cost,sum( (case when IsFromMilk=1 then MilkFATKG else (Stock_Qty*FatPer) end) * case when InOut='I' then 1 else -1 end) as Balance_FAT,sum(( (case when IsFromMilk=1 then MilkSNFKG else (Stock_Qty*SNFPer) end ) * case when InOut='I' then 1 else -1 end)) as Balance_SNF "
                If ChkMRPWise.Checked Then
                    strFinalQry += ",Max(MRP) as MRP "
                End If
                strFinalQry += " from (" + qry + ") xxx " + Environment.NewLine
                strFinalQry += " where Punching_Date < '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' group by xxx.Item_Code " + Environment.NewLine
                strFinalQry += " union all "
                strFinalQry += " select Trans_Id,Trans_Type,Trans_Type_Name,Source_Doc_No,Punching_Date,InOutView, InOut,Location_Code,[Loc Desp], [LocAddress],SourceCode,SourceName,SourceType ,Item_Type,Item_Type_Name,Item_Group,Group_Description," + strCodeColumn + "," + strCodeDescColumn + ",Item_Code ,Item_Desc,Item_Category_Struct_Code,Stock_UOM,itf_code ,( Stock_Qty * case when InOut='I' then 1 else -1 end) as Stock_Qty,(QtyKG * case when InOut='I' then 1 else -1 end) as Balance_QTYKG,convert(decimal(28,3),case when Stock_Qty=0 then 0 else Cost/Stock_Qty end) as Rate,(Cost * case when InOut='I' then 1 else -1 end) as Cost,( (case when IsFromMilk=1 then MilkFATKG else (Stock_Qty*FatPer) end) * case when InOut='I' then 1 else -1 end) as Balance_FAT, ( (case when IsFromMilk=1 then MilkSNFKG else (Stock_Qty*SNFPer) end ) * case when InOut='I' then 1 else -1 end) as Balance_SNF  "
                If ChkMRPWise.Checked Then
                    strFinalQry += ",MRP "
                End If
                strFinalQry += " from (" + qry + ") xxx " + Environment.NewLine
                strFinalQry += " where Punching_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' " + Environment.NewLine

                strFinalQry += ")xxxxxx Order by Item_Code,Punching_Date"
            End If
            '' check for bulk export
            If isPrintCrystalReport = 4 Then
                transportSql.BulkExport("Stock Reco" & "_" & clsCommon.myCstr(cboType.SelectedValue), strFinalQry, "order by Item_Code,Punching_Date", "csv")
                Exit Sub
            ElseIf isPrintCrystalReport = 5 Then
                transportSql.BulkExport("Stock Reco" & "_" & clsCommon.myCstr(cboType.SelectedValue), strFinalQry, "order by Item_Code,Punching_Date", "xls")
                Exit Sub
            End If
            RadPageViewPage2.Text = "Report ( " + clsCommon.myCstr(cboType.SelectedValue) + " )"
            clsCommon.ProgressBarUpdate("Loading Data.Please Wait...")

            ''richa agarwal 26 feb,2016 BM00000008708
            Dim dtr As System.Data.SqlClient.SqlDataReader = Nothing
            Try

                dtr = clsDBFuncationality.GetDataReader(strFinalQry, Nothing)
                If Not dtr.HasRows Then
                    Throw New Exception("No Data Found to Display")
                End If
                If dtr.HasRows Then
                    gv1.MasterTemplate.LoadFrom(dtr)
                End If
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
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
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
            If clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal Then
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
                ''==Added Parteek
                arrVisibleColumAndCaption.Add("Prod_Fat_KG", "Production Fat (KG)")
                arrVisibleColumAndCaption.Add("Prod_Fat_Per", "Production Fat %")
                arrVisibleColumAndCaption.Add("Prod_SNF_KG", "Production SNF (KG)")
                arrVisibleColumAndCaption.Add("Prod_SNF_Per", "Production SNF %")
                arrVisibleColumAndCaption.Add("Diff_SNF", "Difference SNF")
                arrVisibleColumAndCaption.Add("Diff_Fat", "Difference Fat")
            End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item Structure Wise Summary") = CompairStringResult.Equal Then
            arrVisibleColumAndCaption.Add("Item_Type_Name", "Item Type")
            arrVisibleColumAndCaption.Add("Structure_Descq", "Structure Description")
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
            If clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal Then
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
                ''==Added Parteek
                arrVisibleColumAndCaption.Add("Prod_Fat_KG", "Production Fat (KG)")
                arrVisibleColumAndCaption.Add("Prod_Fat_Per", "Production Fat %")
                arrVisibleColumAndCaption.Add("Prod_SNF_KG", "Production SNF (KG)")
                arrVisibleColumAndCaption.Add("Prod_SNF_Per", "Production SNF %")
                arrVisibleColumAndCaption.Add("Diff_SNF", "Difference SNF")
                arrVisibleColumAndCaption.Add("Diff_Fat", "Difference Fat")
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
            If clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal Then
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
                ''==Added Parteek
                arrVisibleColumAndCaption.Add("Prod_Fat_KG", "Production Fat (KG)")
                arrVisibleColumAndCaption.Add("Prod_Fat_Per", "Production Fat %")
                arrVisibleColumAndCaption.Add("Prod_SNF_KG", "Production SNF (KG)")
                arrVisibleColumAndCaption.Add("Prod_SNF_Per", "Production SNF %")
                arrVisibleColumAndCaption.Add("Diff_SNF", "Difference SNF")
                arrVisibleColumAndCaption.Add("Diff_Fat", "Difference Fat")
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
            If clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal Then
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
                ''==Added Parteek
                arrVisibleColumAndCaption.Add("Prod_Fat_KG", "Production Fat (KG)")
                arrVisibleColumAndCaption.Add("Prod_Fat_Per", "Production Fat %")
                arrVisibleColumAndCaption.Add("Prod_SNF_KG", "Production SNF (KG)")
                arrVisibleColumAndCaption.Add("Prod_SNF_Per", "Production SNF %")
                arrVisibleColumAndCaption.Add("Diff_SNF", "Difference SNF")
                arrVisibleColumAndCaption.Add("Diff_Fat", "Difference Fat")
            End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item Wise Summary") = CompairStringResult.Equal Then
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
            If clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal Then
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
                ''==Added Parteek
                arrVisibleColumAndCaption.Add("Prod_Fat_KG", "Production Fat (KG)")
                arrVisibleColumAndCaption.Add("Prod_Fat_Per", "Production Fat %")
                arrVisibleColumAndCaption.Add("Prod_SNF_KG", "Production SNF (KG)")
                arrVisibleColumAndCaption.Add("Prod_SNF_Per", "Production SNF %")
                arrVisibleColumAndCaption.Add("Diff_SNF", "Difference SNF")
                arrVisibleColumAndCaption.Add("Diff_Fat", "Difference Fat")
            End If
            If ChkMRPWise.Checked = True Then
                arrVisibleColumAndCaption.Add("MRP", "MRP")
            End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item And Location Wise Summary") = CompairStringResult.Equal Then
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
            If clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal Then
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
                ''==Added Parteek
                arrVisibleColumAndCaption.Add("Prod_Fat_KG", "Production Fat (KG)")
                arrVisibleColumAndCaption.Add("Prod_Fat_Per", "Production Fat %")
                arrVisibleColumAndCaption.Add("Prod_SNF_KG", "Production SNF (KG)")
                arrVisibleColumAndCaption.Add("Prod_SNF_Per", "Production SNF %")
                arrVisibleColumAndCaption.Add("Diff_SNF", "Difference SNF")
                arrVisibleColumAndCaption.Add("Diff_Fat", "Difference Fat")
            End If
            If ChkMRPWise.Checked = True Then
                arrVisibleColumAndCaption.Add("MRP", "MRP")
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
            If clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal Then
                arrVisibleColumAndCaption.Add("Balance_FATPER", "Closing FAT %")
                arrVisibleColumAndCaption.Add("Balance_FAT", "FAT (KG)")
                arrVisibleColumAndCaption.Add("Balance_SNFPER", "Closing SNF %")
                arrVisibleColumAndCaption.Add("Balance_SNF", "SNF (KG)")
                ''==Added Parteek
                arrVisibleColumAndCaption.Add("Prod_Fat_KG", "Production Fat (KG)")
                arrVisibleColumAndCaption.Add("Prod_Fat_Per", "Production Fat %")
                arrVisibleColumAndCaption.Add("Prod_SNF_KG", "Production SNF (KG)")
                arrVisibleColumAndCaption.Add("Prod_SNF_Per", "Production SNF %")
                arrVisibleColumAndCaption.Add("Diff_SNF", "Difference SNF")
                arrVisibleColumAndCaption.Add("Diff_Fat", "Difference Fat")
            End If
            If ChkMRPWise.Checked Then
                arrVisibleColumAndCaption.Add("MRP", "MRP")
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
            If clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal Then
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
            If ChkMRPWise.Checked = True Then
                arrVisibleColumAndCaption.Add("MRP", "MRP")
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

            arrVisibleColumAndCaption.Add("RecProQty", "Received Production Quantity")
            arrVisibleColumAndCaption.Add("RecProRate", "Received Production Rate")
            arrVisibleColumAndCaption.Add("RecProCost", "Received Production Amount")

            arrVisibleColumAndCaption.Add("RecAdjQty", "Received Adjustment Quantity")
            arrVisibleColumAndCaption.Add("RecAdjRate", "Received Adjustment Rate")
            arrVisibleColumAndCaption.Add("RecAdjCost", "Received Adjustment Amount")

            arrVisibleColumAndCaption.Add("RecOthQty", "Received Other Quantity")
            arrVisibleColumAndCaption.Add("RecOthRate", "Received Other Rate")
            arrVisibleColumAndCaption.Add("RecOthCost", "Received Other Amount")

            arrVisibleColumAndCaption.Add("IssSaleQty", "Issued Sale Quantity")
            arrVisibleColumAndCaption.Add("IssSaleRate", "Issued Sale Rate")
            arrVisibleColumAndCaption.Add("IssSaleCost", "Issued Sale Amount")

            arrVisibleColumAndCaption.Add("IssTransferQty", "Issued/Transfer Quantity")
            arrVisibleColumAndCaption.Add("IssTransferRate", "Issued/Transfer Rate")
            arrVisibleColumAndCaption.Add("IssTransferCost", "Issued/Transfer Amount")


            arrVisibleColumAndCaption.Add("IssIssAdjQty", "Issued/Adjustment Quantity")
            arrVisibleColumAndCaption.Add("IssIssAdjRate", "Issued/Adjustment Rate")
            arrVisibleColumAndCaption.Add("IssIssAdjCost", "Issued/Adjustment Amount")

            arrVisibleColumAndCaption.Add("IssOthQty", "Issued Other Quantity")
            arrVisibleColumAndCaption.Add("IssOthRate", "Issued Other Rate")
            arrVisibleColumAndCaption.Add("IssOthCost", "Issued Other Amount")


            arrVisibleColumAndCaption.Add("CLQty", "Balance Quantity")
            arrVisibleColumAndCaption.Add("CLRate", "Balance Rate")
            arrVisibleColumAndCaption.Add("CLCost", "Balance Amount")
            If clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal Then
                arrVisibleColumAndCaption.Add("OPFAT", "Opening FAT")
                arrVisibleColumAndCaption.Add("OPFATPER", "Opening Fat %")
                arrVisibleColumAndCaption.Add("OPSNF", "Opening SNF")
                arrVisibleColumAndCaption.Add("OPSNFPER", "Opening SNF%")

                arrVisibleColumAndCaption.Add("RecPurFAT", "Received Purchase FAT")
                arrVisibleColumAndCaption.Add("RecPurFATPER", "Received Purchase Fat %")
                arrVisibleColumAndCaption.Add("RecPurSNF", "Received Purchase SNF")
                arrVisibleColumAndCaption.Add("RecPurSNFPER", "Received Purchase SNF %")

                arrVisibleColumAndCaption.Add("RecProFAT", "Received Production FAT")
                arrVisibleColumAndCaption.Add("RecProFATPER", "Received Production Fat %")
                arrVisibleColumAndCaption.Add("RecProSNF", "Received Production SNF")
                arrVisibleColumAndCaption.Add("RecProSNFPER", "Received Production SNF %")

                arrVisibleColumAndCaption.Add("RecAdjFAT", "Received Adjustment FAT")
                arrVisibleColumAndCaption.Add("RecAdjFATPER", "Received Adjustment Fat %")
                arrVisibleColumAndCaption.Add("RecAdjSNF", "Received Adjustment SNF")
                arrVisibleColumAndCaption.Add("RecAdjSNFPER", "Received Adjustment SNF %")

                arrVisibleColumAndCaption.Add("RecOthFAT", "Received Other FAT")
                arrVisibleColumAndCaption.Add("RecOthFATPER", "Received Other Fat %")
                arrVisibleColumAndCaption.Add("RecOthSNF", "Received Other SNF")
                arrVisibleColumAndCaption.Add("RecOthSNFPER", "Received Other SNF %")

                arrVisibleColumAndCaption.Add("IssSaleFAT", "Issued Sale FAT")
                arrVisibleColumAndCaption.Add("IssSaleFATPER", "Issued Sale Fat %")
                arrVisibleColumAndCaption.Add("IssSaleSNF", "Issued Sale SNF")
                arrVisibleColumAndCaption.Add("IssSaleSNFPER", "Issued Sale SNF%")


                arrVisibleColumAndCaption.Add("IssTransferFAT", "Issued/TransferFAT")
                arrVisibleColumAndCaption.Add("IssTransferFATPER", "Issued/TransferFat %")
                arrVisibleColumAndCaption.Add("IssTransferSNF", "Issued/TransferSNF")
                arrVisibleColumAndCaption.Add("IssTransferSNFPER", "Issued/TransferSNF%")

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
            If ChkMRPWise.Checked = True Then
                arrVisibleColumAndCaption.Add("MRP", "MRP")
            End If
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
        If Not (clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item Group Wise Summary") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item Structure Wise Summary") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item Type Wise Summary") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Document Wise Detail Ledger") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Date, Item And Document Wise Detail") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Date and Item Wise Stock") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Transaction Wise") = CompairStringResult.Equal) Then
            For Each dr As DataRow In dtCategory.Rows
                If clsCommon.myCstr(dr("CodeDescColumn")) IsNot Nothing Then

                    Dim strCol As String = clsCommon.myCstr(dr("CodeDescColumn"))
                    gv1.Columns(strCol).IsVisible = True
                    gv1.Columns(strCol).Width = 100
                    gv1.Columns(strCol).HeaderText = clsCommon.myCstr(dr("DescColumn"))
                End If

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

            gv1.Columns("OPFATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("OPFATPER").Width = 100
            gv1.Columns("OPFATPER").HeaderText = "Opening FAT %"
            gv1.Columns("OPFATPER").FormatString = "{0:n2}"

            gv1.Columns("OPFAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("OPFAT").Width = 100
            gv1.Columns("OPFAT").HeaderText = "Opening FAT (KG)"
            gv1.Columns("OPFAT").FormatString = "{0:n2}"

            gv1.Columns("OPSNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("OPSNFPER").Width = 100
            gv1.Columns("OPSNFPER").HeaderText = "Opening SNF %"
            gv1.Columns("OPSNFPER").FormatString = "{0:n2}"

            gv1.Columns("OPSNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("OPSNF").Width = 100
            gv1.Columns("OPSNF").HeaderText = "Opening SNF (KG)"
            gv1.Columns("OPSNF").FormatString = "{0:n2}"

            gv1.Columns("Received_FATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Received_FATPER").Width = 100
            gv1.Columns("Received_FATPER").HeaderText = "Received FAT %"
            gv1.Columns("Received_FATPER").FormatString = "{0:n2}"

            gv1.Columns("Received_FAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Received_FAT").Width = 100
            gv1.Columns("Received_FAT").HeaderText = "Received FAT (KG)"
            gv1.Columns("Received_FAT").FormatString = "{0:n2}"

            gv1.Columns("Received_SNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Received_SNFPER").Width = 100
            gv1.Columns("Received_SNFPER").HeaderText = "Received SNF %"
            gv1.Columns("Received_SNFPER").FormatString = "{0:n2}"

            gv1.Columns("Received_SNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Received_SNF").Width = 100
            gv1.Columns("Received_SNF").HeaderText = "Received SNF (KG)"
            gv1.Columns("Received_SNF").FormatString = "{0:n2}"

            gv1.Columns("Issued_FATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Issued_FATPER").Width = 100
            gv1.Columns("Issued_FATPER").HeaderText = "Issued FAT %"
            gv1.Columns("Issued_FATPER").FormatString = "{0:n2}"

            gv1.Columns("Issued_FAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Issued_FAT").Width = 100
            gv1.Columns("Issued_FAT").HeaderText = "Issued FAT (KG)"
            gv1.Columns("Issued_FAT").FormatString = "{0:n2}"

            gv1.Columns("Issued_SNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Issued_SNFPER").Width = 100
            gv1.Columns("Issued_SNFPER").HeaderText = "Issued SNF %"
            gv1.Columns("Issued_SNFPER").FormatString = "{0:n2}"

            gv1.Columns("Issued_SNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Issued_SNF").Width = 100
            gv1.Columns("Issued_SNF").HeaderText = "Issued SNF (KG)"
            gv1.Columns("Issued_SNF").FormatString = "{0:n2}"

            gv1.Columns("Balance_FATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Balance_FATPER").Width = 100
            gv1.Columns("Balance_FATPER").HeaderText = "Closing FAT %"
            gv1.Columns("Balance_FATPER").FormatString = "{0:n2}"

            gv1.Columns("Balance_FAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Balance_FAT").Width = 100
            gv1.Columns("Balance_FAT").HeaderText = "Closing FAT (KG)"
            gv1.Columns("Balance_FAT").FormatString = "{0:n2}"

            gv1.Columns("Balance_SNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Balance_SNFPER").Width = 100
            gv1.Columns("Balance_SNFPER").HeaderText = "Closing SNF %"
            gv1.Columns("Balance_SNFPER").FormatString = "{0:n2}"

            gv1.Columns("Balance_SNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Balance_SNF").Width = 100
            gv1.Columns("Balance_SNF").HeaderText = "Closing SNF (KG)"
            gv1.Columns("Balance_SNF").FormatString = "{0:n2}"

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


            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim Smitem As New GridViewSummaryItem("OPBal", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("OPBalCost", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("Received_Qty", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("RecdCost", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("Issued_Qty", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("IssueCost", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("Balance_Qty", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Cost", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)


            Smitem = New GridViewSummaryItem("OPFAT", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("OPSNF", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Received_FAT", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Received_SNF", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Issued_FAT", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Issued_SNF", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Balance_FAT", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Balance_SNF", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item Structure Wise Summary") = CompairStringResult.Equal Then
            gv1.Columns("Item_Type").IsVisible = False
            gv1.Columns("Item_Type").HeaderText = "Item Type Code"

            gv1.Columns("Item_Type_Name").IsVisible = True
            gv1.Columns("Item_Type_Name").Width = 100
            gv1.Columns("Item_Type_Name").HeaderText = "Item Type"

            gv1.Columns("Structure_Code").IsVisible = False
            gv1.Columns("Structure_Code").HeaderText = "Structure Code"

            gv1.Columns("Structure_Descq").IsVisible = True
            gv1.Columns("Structure_Descq").Width = 100
            gv1.Columns("Structure_Descq").HeaderText = "Structure"

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

            gv1.Columns("OPFATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("OPFATPER").Width = 100
            gv1.Columns("OPFATPER").HeaderText = "Opening FAT %"
            gv1.Columns("OPFATPER").FormatString = "{0:n2}"

            gv1.Columns("OPFAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("OPFAT").Width = 100
            gv1.Columns("OPFAT").HeaderText = "Opening FAT (KG)"
            gv1.Columns("OPFAT").FormatString = "{0:n2}"

            gv1.Columns("OPSNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("OPSNFPER").Width = 100
            gv1.Columns("OPSNFPER").HeaderText = "Opening SNF %"
            gv1.Columns("OPSNFPER").FormatString = "{0:n2}"

            gv1.Columns("OPSNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
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

            gv1.Columns("Received_FATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Received_FATPER").Width = 100
            gv1.Columns("Received_FATPER").HeaderText = "Received FAT %"
            gv1.Columns("Received_FATPER").FormatString = "{0:n2}"

            gv1.Columns("Received_FAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Received_FAT").Width = 100
            gv1.Columns("Received_FAT").HeaderText = "Received FAT (KG)"
            gv1.Columns("Received_FAT").FormatString = "{0:n2}"


            gv1.Columns("Received_SNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Received_SNFPER").Width = 100
            gv1.Columns("Received_SNFPER").HeaderText = "Received SNF %"
            gv1.Columns("Received_SNFPER").FormatString = "{0:n2}"

            gv1.Columns("Received_SNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
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

            gv1.Columns("Issued_FATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Issued_FATPER").Width = 100
            gv1.Columns("Issued_FATPER").HeaderText = "Issued FAT %"
            gv1.Columns("Issued_FATPER").FormatString = "{0:n2}"

            gv1.Columns("Issued_FAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Issued_FAT").Width = 100
            gv1.Columns("Issued_FAT").HeaderText = "Issued FAT (KG)"
            gv1.Columns("Issued_FAT").FormatString = "{0:n2}"

            gv1.Columns("Issued_SNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Issued_SNFPER").Width = 100
            gv1.Columns("Issued_SNFPER").HeaderText = "Issued SNF %"
            gv1.Columns("Issued_SNFPER").FormatString = "{0:n2}"

            gv1.Columns("Issued_SNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
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

            gv1.Columns("Balance_FATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Balance_FATPER").Width = 100
            gv1.Columns("Balance_FATPER").HeaderText = "Closing FAT %"
            gv1.Columns("Balance_FATPER").FormatString = "{0:n2}"

            gv1.Columns("Balance_FAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Balance_FAT").Width = 100
            gv1.Columns("Balance_FAT").HeaderText = "Closing FAT (KG)"
            gv1.Columns("Balance_FAT").FormatString = "{0:n2}"

            gv1.Columns("Balance_SNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Balance_SNFPER").Width = 100
            gv1.Columns("Balance_SNFPER").HeaderText = "Closing SNF %"
            gv1.Columns("Balance_SNFPER").FormatString = "{0:n2}"

            gv1.Columns("Balance_SNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Balance_SNF").Width = 100
            gv1.Columns("Balance_SNF").HeaderText = "Closing SNF (KG)"
            gv1.Columns("Balance_SNF").FormatString = "{0:n2}"
            ''

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim Smitem As New GridViewSummaryItem("OPBal", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("OPBalCost", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("Received_Qty", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("RecdCost", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("Issued_Qty", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("IssueCost", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("Balance_Qty", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Cost", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)


            Smitem = New GridViewSummaryItem("OPFAT", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("OPSNF", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Received_FAT", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Received_SNF", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Issued_FAT", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Issued_SNF", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Balance_FAT", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Balance_SNF", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

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

            gv1.Columns("OPFATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("OPFATPER").Width = 100
            gv1.Columns("OPFATPER").HeaderText = "Opening FAT %"
            gv1.Columns("OPFATPER").FormatString = "{0:n2}"

            gv1.Columns("OPFAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("OPFAT").Width = 100
            gv1.Columns("OPFAT").HeaderText = "Opening FAT (KG)"
            gv1.Columns("OPFAT").FormatString = "{0:n2}"

            gv1.Columns("OPSNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("OPSNFPER").Width = 100
            gv1.Columns("OPSNFPER").HeaderText = "Opening SNF %"
            gv1.Columns("OPSNFPER").FormatString = "{0:n2}"

            gv1.Columns("OPSNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
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

            gv1.Columns("Received_FATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Received_FATPER").Width = 100
            gv1.Columns("Received_FATPER").HeaderText = "Received FAT %"
            gv1.Columns("Received_FATPER").FormatString = "{0:n2}"

            gv1.Columns("Received_FAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Received_FAT").Width = 100
            gv1.Columns("Received_FAT").HeaderText = "Received FAT (KG)"
            gv1.Columns("Received_FAT").FormatString = "{0:n2}"


            gv1.Columns("Received_SNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Received_SNFPER").Width = 100
            gv1.Columns("Received_SNFPER").HeaderText = "Received SNF %"
            gv1.Columns("Received_SNFPER").FormatString = "{0:n2}"

            gv1.Columns("Received_SNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
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

            gv1.Columns("Issued_FATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Issued_FATPER").Width = 100
            gv1.Columns("Issued_FATPER").HeaderText = "Issued FAT %"
            gv1.Columns("Issued_FATPER").FormatString = "{0:n2}"

            gv1.Columns("Issued_FAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Issued_FAT").Width = 100
            gv1.Columns("Issued_FAT").HeaderText = "Issued FAT (KG)"
            gv1.Columns("Issued_FAT").FormatString = "{0:n2}"

            gv1.Columns("Issued_SNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Issued_SNFPER").Width = 100
            gv1.Columns("Issued_SNFPER").HeaderText = "Issued SNF %"
            gv1.Columns("Issued_SNFPER").FormatString = "{0:n2}"

            gv1.Columns("Issued_SNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
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

            gv1.Columns("Balance_FATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Balance_FATPER").Width = 100
            gv1.Columns("Balance_FATPER").HeaderText = "Closing FAT %"
            gv1.Columns("Balance_FATPER").FormatString = "{0:n2}"

            gv1.Columns("Balance_FAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Balance_FAT").Width = 100
            gv1.Columns("Balance_FAT").HeaderText = "Closing FAT (KG)"
            gv1.Columns("Balance_FAT").FormatString = "{0:n2}"

            gv1.Columns("Balance_SNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Balance_SNFPER").Width = 100
            gv1.Columns("Balance_SNFPER").HeaderText = "Closing SNF %"
            gv1.Columns("Balance_SNFPER").FormatString = "{0:n2}"

            gv1.Columns("Balance_SNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Balance_SNF").Width = 100
            gv1.Columns("Balance_SNF").HeaderText = "Closing SNF (KG)"
            gv1.Columns("Balance_SNF").FormatString = "{0:n2}"
            ''

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim Smitem As New GridViewSummaryItem("OPBal", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("OPBalCost", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("Received_Qty", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("RecdCost", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("Issued_Qty", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("IssueCost", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("Balance_Qty", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Cost", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)


            Smitem = New GridViewSummaryItem("OPFAT", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("OPSNF", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Received_FAT", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Received_SNF", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Issued_FAT", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Issued_SNF", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Balance_FAT", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Balance_SNF", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

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

            gv1.Columns("OPFATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("OPFATPER").Width = 100
            gv1.Columns("OPFATPER").HeaderText = "Opening FAT %"
            gv1.Columns("OPFATPER").FormatString = "{0:n2}"

            gv1.Columns("OPFAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("OPFAT").Width = 100
            gv1.Columns("OPFAT").HeaderText = "Opening FAT (KG)"
            gv1.Columns("OPFAT").FormatString = "{0:n2}"

            gv1.Columns("OPSNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("OPSNFPER").Width = 100
            gv1.Columns("OPSNFPER").HeaderText = "Opening SNF %"
            gv1.Columns("OPSNFPER").FormatString = "{0:n2}"

            gv1.Columns("OPSNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
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

            gv1.Columns("Received_FATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Received_FATPER").Width = 100
            gv1.Columns("Received_FATPER").HeaderText = "Received FAT %"
            gv1.Columns("Received_FATPER").FormatString = "{0:n2}"

            gv1.Columns("Received_FAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Received_FAT").Width = 100
            gv1.Columns("Received_FAT").HeaderText = "Received FAT (KG)"
            gv1.Columns("Received_FAT").FormatString = "{0:n2}"


            gv1.Columns("Received_SNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Received_SNFPER").Width = 100
            gv1.Columns("Received_SNFPER").HeaderText = "Received SNF %"
            gv1.Columns("Received_SNFPER").FormatString = "{0:n2}"

            gv1.Columns("Received_SNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
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

            gv1.Columns("Issued_FATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Issued_FATPER").Width = 100
            gv1.Columns("Issued_FATPER").HeaderText = "Issued FAT %"
            gv1.Columns("Issued_FATPER").FormatString = "{0:n2}"

            gv1.Columns("Issued_FAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Issued_FAT").Width = 100
            gv1.Columns("Issued_FAT").HeaderText = "Issued FAT (KG)"
            gv1.Columns("Issued_FAT").FormatString = "{0:n2}"

            gv1.Columns("Issued_SNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Issued_SNFPER").Width = 100
            gv1.Columns("Issued_SNFPER").HeaderText = "Issued SNF %"
            gv1.Columns("Issued_SNFPER").FormatString = "{0:n2}"

            gv1.Columns("Issued_SNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
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

            gv1.Columns("Balance_FATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Balance_FATPER").Width = 100
            gv1.Columns("Balance_FATPER").HeaderText = "Closing FAT %"
            gv1.Columns("Balance_FATPER").FormatString = "{0:n2}"

            gv1.Columns("Balance_FAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Balance_FAT").Width = 100
            gv1.Columns("Balance_FAT").HeaderText = "Closing FAT (KG)"
            gv1.Columns("Balance_FAT").FormatString = "{0:n2}"

            gv1.Columns("Balance_SNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Balance_SNFPER").Width = 100
            gv1.Columns("Balance_SNFPER").HeaderText = "Closing SNF %"
            gv1.Columns("Balance_SNFPER").FormatString = "{0:n2}"

            gv1.Columns("Balance_SNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Balance_SNF").Width = 100
            gv1.Columns("Balance_SNF").HeaderText = "Closing SNF (KG)"
            gv1.Columns("Balance_SNF").FormatString = "{0:n2}"
            ''

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim Smitem As New GridViewSummaryItem("OPBal", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("OPBalCost", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("Received_Qty", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("RecdCost", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("Issued_Qty", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("IssueCost", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("Balance_Qty", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Cost", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)


            Smitem = New GridViewSummaryItem("OPFAT", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("OPSNF", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Received_FAT", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Received_SNF", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Issued_FAT", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Issued_SNF", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Balance_FAT", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Balance_SNF", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item Wise Summary") = CompairStringResult.Equal Then
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

            gv1.Columns("OPFATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("OPFATPER").Width = 100
            gv1.Columns("OPFATPER").HeaderText = "Opening FAT %"
            gv1.Columns("OPFATPER").FormatString = "{0:n2}"

            gv1.Columns("OPFAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("OPFAT").Width = 100
            gv1.Columns("OPFAT").HeaderText = "Opening FAT (KG)"
            gv1.Columns("OPFAT").FormatString = "{0:n2}"

            gv1.Columns("OPSNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("OPSNFPER").Width = 100
            gv1.Columns("OPSNFPER").HeaderText = "Opening SNF %"
            gv1.Columns("OPSNFPER").FormatString = "{0:n2}"

            gv1.Columns("OPSNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
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

            gv1.Columns("Received_FATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Received_FATPER").Width = 100
            gv1.Columns("Received_FATPER").HeaderText = "Received FAT %"
            gv1.Columns("Received_FATPER").FormatString = "{0:n2}"

            gv1.Columns("Received_FAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Received_FAT").Width = 100
            gv1.Columns("Received_FAT").HeaderText = "Received FAT (KG)"
            gv1.Columns("Received_FAT").FormatString = "{0:n2}"

            gv1.Columns("Received_SNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Received_SNFPER").Width = 100
            gv1.Columns("Received_SNFPER").HeaderText = "Received SNF %"
            gv1.Columns("Received_SNFPER").FormatString = "{0:n2}"

            gv1.Columns("Received_SNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
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

            gv1.Columns("Issued_FATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Issued_FATPER").Width = 100
            gv1.Columns("Issued_FATPER").HeaderText = "Issued FAT %"
            gv1.Columns("Issued_FATPER").FormatString = "{0:n2}"

            gv1.Columns("Issued_FAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Issued_FAT").Width = 100
            gv1.Columns("Issued_FAT").HeaderText = "Issued FAT"
            gv1.Columns("Issued_FAT").FormatString = "{0:n2}"

            gv1.Columns("Issued_SNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Issued_SNFPER").Width = 100
            gv1.Columns("Issued_SNFPER").HeaderText = "Issued SNF %"
            gv1.Columns("Issued_SNFPER").FormatString = "{0:n2}"

            gv1.Columns("Issued_SNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
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

            gv1.Columns("Balance_FATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Balance_FATPER").Width = 100
            gv1.Columns("Balance_FATPER").HeaderText = "Closing FAT %"
            gv1.Columns("Balance_FATPER").FormatString = "{0:n2}"

            gv1.Columns("Balance_FAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Balance_FAT").Width = 100
            gv1.Columns("Balance_FAT").HeaderText = "Closing FAT (KG)"
            gv1.Columns("Balance_FAT").FormatString = "{0:n2}"

            gv1.Columns("Balance_SNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Balance_SNFPER").Width = 100
            gv1.Columns("Balance_SNFPER").HeaderText = "Closing SNF %"
            gv1.Columns("Balance_SNFPER").FormatString = "{0:n2}"

            gv1.Columns("Balance_SNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Balance_SNF").Width = 100
            gv1.Columns("Balance_SNF").HeaderText = "Closing SNF (KG)"
            gv1.Columns("Balance_SNF").FormatString = "{0:n2}"


            If ChkMRPWise.Checked = True Then
                gv1.Columns("MRP").IsVisible = True
                gv1.Columns("MRP").Width = 100
                gv1.Columns("MRP").HeaderText = "MRP"
                gv1.Columns("MRP").FormatString = "{0:n2}"
            End If
            ''


            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim Smitem As New GridViewSummaryItem("OPBal", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("OPBalCost", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("Received_Qty", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("RecdCost", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("Issued_Qty", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("IssueCost", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("Balance_Qty", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Cost", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("OPFAT", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("OPSNF", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Received_FAT", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Received_SNF", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Issued_FAT", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Issued_SNF", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Balance_FAT", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Balance_SNF", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item And Location Wise Summary") = CompairStringResult.Equal Then
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

            gv1.Columns("Main_Location_Code").HeaderText = "Main Location Code"

            gv1.Columns("MainLocationDesc").IsVisible = True
            gv1.Columns("MainLocationDesc").Width = 150
            gv1.Columns("MainLocationDesc").HeaderText = "Main Location"

            gv1.Columns("Location_Code").HeaderText = "Location Code"

            gv1.Columns("Loc Desp").IsVisible = True
            gv1.Columns("Loc Desp").Width = 150
            gv1.Columns("Loc Desp").HeaderText = "Location"

            gv1.Columns("Item_Code").HeaderText = "Item Code"

            gv1.Columns("Item_Desc").IsVisible = True
            gv1.Columns("Item_Desc").Width = 150
            gv1.Columns("Item_Desc").HeaderText = "Item"

            gv1.Columns("Stock_UOM").IsVisible = True
            gv1.Columns("Stock_UOM").Width = 100
            gv1.Columns("Stock_UOM").HeaderText = "UOM"

            ''BHA/13/09/18-000548 richa 
            gv1.Columns("Inventory_Control_Acc").IsVisible = True
            gv1.Columns("Inventory_Control_Acc").Width = 150
            gv1.Columns("Inventory_Control_Acc").HeaderText = "Inv Control Acc"

            gv1.Columns("Inventory_Control_Acc_desc").IsVisible = True
            gv1.Columns("Inventory_Control_Acc_desc").Width = 250
            gv1.Columns("Inventory_Control_Acc_desc").HeaderText = "Inv Control Acc Desc"
            ''----------------

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

            gv1.Columns("OPFAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("OPFAT").Width = 100
            gv1.Columns("OPFAT").HeaderText = "Opening FAT (KG)"
            gv1.Columns("OPFAT").FormatString = "{0:n2}"

            gv1.Columns("OPFATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("OPFATPER").Width = 100
            gv1.Columns("OPFATPER").HeaderText = "Opening FAT %"
            gv1.Columns("OPFATPER").FormatString = "{0:n2}"

            gv1.Columns("OPSNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("OPSNF").Width = 100
            gv1.Columns("OPSNF").HeaderText = "Opening SNF (KG)"
            gv1.Columns("OPSNF").FormatString = "{0:n2}"

            gv1.Columns("OPSNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
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

            gv1.Columns("Received_FATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Received_FATPER").Width = 100
            gv1.Columns("Received_FATPER").HeaderText = "Received FAT %"
            gv1.Columns("Received_FATPER").FormatString = "{0:n2}"

            gv1.Columns("Received_FAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Received_FAT").Width = 100
            gv1.Columns("Received_FAT").HeaderText = "Received FAT (KG)"
            gv1.Columns("Received_FAT").FormatString = "{0:n2}"

            gv1.Columns("Received_SNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Received_SNFPER").Width = 100
            gv1.Columns("Received_SNFPER").HeaderText = "Received SNF %"
            gv1.Columns("Received_SNFPER").FormatString = "{0:n2}"

            gv1.Columns("Received_SNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
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

            gv1.Columns("Issued_FATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Issued_FATPER").Width = 100
            gv1.Columns("Issued_FATPER").HeaderText = "Issued FAT %"
            gv1.Columns("Issued_FATPER").FormatString = "{0:n2}"

            gv1.Columns("Issued_FAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Issued_FAT").Width = 100
            gv1.Columns("Issued_FAT").HeaderText = "Issued FAT (KG)"
            gv1.Columns("Issued_FAT").FormatString = "{0:n2}"

            gv1.Columns("Issued_SNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Issued_SNFPER").Width = 100
            gv1.Columns("Issued_SNFPER").HeaderText = "Issued SNF %"
            gv1.Columns("Issued_SNFPER").FormatString = "{0:n2}"

            gv1.Columns("Issued_SNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
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

            gv1.Columns("Balance_FATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Balance_FATPER").Width = 100
            gv1.Columns("Balance_FATPER").HeaderText = "Closing FAT %"
            gv1.Columns("Balance_FATPER").FormatString = "{0:n2}"

            gv1.Columns("Balance_FAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Balance_FAT").Width = 100
            gv1.Columns("Balance_FAT").HeaderText = "Closing FAT (KG)"
            gv1.Columns("Balance_FAT").FormatString = "{0:n2}"

            gv1.Columns("Balance_SNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Balance_SNFPER").Width = 100
            gv1.Columns("Balance_SNFPER").HeaderText = "Closing SNF %"
            gv1.Columns("Balance_SNFPER").FormatString = "{0:n2}"

            gv1.Columns("Balance_SNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Balance_SNF").Width = 100
            gv1.Columns("Balance_SNF").HeaderText = "Closing SNF (KG)"
            gv1.Columns("Balance_SNF").FormatString = "{0:n2}"

            If ChkMRPWise.Checked = True Then
                gv1.Columns("MRP").IsVisible = True
                gv1.Columns("MRP").Width = 100
                gv1.Columns("MRP").HeaderText = "MRP"
                gv1.Columns("MRP").FormatString = "{0:n2}"
            End If


            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim Smitem As New GridViewSummaryItem("OPBal", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("OPBalCost", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("Received_Qty", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("RecdCost", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("Issued_Qty", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("IssueCost", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("Balance_Qty", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Cost", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)

            Smitem = New GridViewSummaryItem("OPFAT", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("OPSNF", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Received_FAT", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Received_SNF", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Issued_FAT", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Issued_SNF", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Balance_FAT", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Balance_SNF", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
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

            gv1.Columns("Punching_Date").IsVisible = True
            gv1.Columns("Punching_Date").Width = 100
            gv1.Columns("Punching_Date").HeaderText = "Document Date"

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

            gv1.Columns("PrimaryLocation").IsVisible = True
            gv1.Columns("PrimaryLocation").Width = 100
            gv1.Columns("PrimaryLocation").HeaderText = "Main Location"

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

            gv1.Columns("Balance_FATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Balance_FATPER").Width = 100
            gv1.Columns("Balance_FATPER").HeaderText = "Closing FAT %"
            gv1.Columns("Balance_FATPER").FormatString = "{0:n2}"

            gv1.Columns("Balance_FAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Balance_FAT").Width = 100
            gv1.Columns("Balance_FAT").HeaderText = "FAT (KG)"
            gv1.Columns("Balance_FAT").FormatString = "{0:n2}"

            gv1.Columns("Balance_SNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Balance_SNFPER").Width = 100
            gv1.Columns("Balance_SNFPER").HeaderText = "Closing SNF %"
            gv1.Columns("Balance_SNFPER").FormatString = "{0:n2}"

            gv1.Columns("Balance_SNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("Balance_SNF").Width = 100
            gv1.Columns("Balance_SNF").HeaderText = "SNF (KG)"
            gv1.Columns("Balance_SNF").FormatString = "{0:n2}"

            If (clsCommon.CompairString(cboDisplayMethod.SelectedValue, "None") = CompairStringResult.Equal) Then
                gv1.Columns("FATRATE").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
                gv1.Columns("FATRATE").Width = 100
                gv1.Columns("FATRATE").HeaderText = "FAT Rate"
                gv1.Columns("FATRATE").FormatString = "{0:n2}"

                gv1.Columns("SNFRATE").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
                gv1.Columns("SNFRATE").Width = 100
                gv1.Columns("SNFRATE").HeaderText = "SNF Rate"
                gv1.Columns("SNFRATE").FormatString = "{0:n2}"
            End If

            gv1.Columns("FATAmount").IsVisible = True
            gv1.Columns("FATAmount").Width = 100
            gv1.Columns("FATAmount").HeaderText = "FAT Amount"
            gv1.Columns("FATAmount").FormatString = "{0:n2}"

            gv1.Columns("SNFAmount").IsVisible = True
            gv1.Columns("SNFAmount").Width = 100
            gv1.Columns("SNFAmount").HeaderText = "SNF Amount"
            gv1.Columns("SNFAmount").FormatString = "{0:n2}"


            If ChkMRPWise.Checked = True Then
                gv1.Columns("MRP").IsVisible = True
                gv1.Columns("MRP").Width = 100
                gv1.Columns("MRP").HeaderText = "MRP"
                gv1.Columns("MRP").FormatString = "{0:n2}"
            End If



            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item1 As New GridViewSummaryItem("Stock_Qty", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("Cost", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("Balance_FAT", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("Balance_SNF", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("Prod_SNF_KG", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("Prod_Fat_KG", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("FATAmount", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("SNFAmount", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            If (clsCommon.CompairString(cboDisplayMethod.SelectedValue, "None") = CompairStringResult.Equal) Then
                item2 = New GridViewSummaryItem("FATRATE", "{0:n2}", GridAggregateFunction.Avg)
                summaryRowItem.Add(item2)
                item2 = New GridViewSummaryItem("SNFRATE", "{0:n2}", GridAggregateFunction.Avg)
                summaryRowItem.Add(item2)
            End If

            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Date, Item And Document Wise Detail") = CompairStringResult.Equal Then
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

            gv1.Columns("OPQty").IsVisible = True
            gv1.Columns("OPQty").Width = 100
            gv1.Columns("OPQty").HeaderText = "Opening Quantity"
            gv1.Columns("OPQty").FormatString = "{0:n2}"

            gv1.Columns("OPRate").IsVisible = True
            gv1.Columns("OPRate").Width = 100
            gv1.Columns("OPRate").HeaderText = "Opening Rate"
            gv1.Columns("OPRate").FormatString = "{0:n3}"

            gv1.Columns("OPFAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("OPFAT").Width = 100
            gv1.Columns("OPFAT").HeaderText = "Opening FAT"
            gv1.Columns("OPFAT").FormatString = "{0:n2}"

            gv1.Columns("OPFATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("OPFATPER").Width = 100
            gv1.Columns("OPFATPER").HeaderText = "Opening Fat %"
            gv1.Columns("OPFATPER").FormatString = "{0:n2}"

            gv1.Columns("OPSNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("OPSNF").Width = 100
            gv1.Columns("OPSNF").HeaderText = "Opening SNF"
            gv1.Columns("OPSNF").FormatString = "{0:n2}"

            gv1.Columns("OPSNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
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

            gv1.Columns("RecFAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("RecFAT").Width = 100
            gv1.Columns("RecFAT").HeaderText = "Received FAT"
            gv1.Columns("RecFAT").FormatString = "{0:n2}"

            gv1.Columns("RecFATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("RecFATPER").Width = 100
            gv1.Columns("RecFATPER").HeaderText = "Received Fat %"
            gv1.Columns("RecFATPER").FormatString = "{0:n2}"

            gv1.Columns("RecSNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("RecSNF").Width = 100
            gv1.Columns("RecSNF").HeaderText = "Received SNF"
            gv1.Columns("RecSNF").FormatString = "{0:n2}"

            gv1.Columns("RecSNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
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

            gv1.Columns("IssFAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("IssFAT").Width = 100
            gv1.Columns("IssFAT").HeaderText = "Issued FAT"
            gv1.Columns("IssFAT").FormatString = "{0:n2}"

            gv1.Columns("IssFATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("IssFATPER").Width = 100
            gv1.Columns("IssFATPER").HeaderText = "Issued Fat %"
            gv1.Columns("IssFATPER").FormatString = "{0:n2}"

            gv1.Columns("IssSNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("IssSNF").Width = 100
            gv1.Columns("IssSNF").HeaderText = "Issued SNF"
            gv1.Columns("IssSNF").FormatString = "{0:n2}"

            gv1.Columns("IssSNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
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

            gv1.Columns("CLFAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("CLFAT").Width = 100
            gv1.Columns("CLFAT").HeaderText = "Balance FAT"
            gv1.Columns("CLFAT").FormatString = "{0:n2}"

            gv1.Columns("CLFATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("CLFATPER").Width = 100
            gv1.Columns("CLFATPER").HeaderText = "Balance Fat %"
            gv1.Columns("CLFATPER").FormatString = "{0:n2}"

            gv1.Columns("CLSNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("CLSNF").Width = 100
            gv1.Columns("CLSNF").HeaderText = "Balance SNF"
            gv1.Columns("CLSNF").FormatString = "{0:n2}"

            gv1.Columns("CLSNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("CLSNFPER").Width = 100
            gv1.Columns("CLSNFPER").HeaderText = "Balance SNF%"
            gv1.Columns("CLSNFPER").FormatString = "{0:n2}"

            gv1.Columns("CLCost").IsVisible = True
            gv1.Columns("CLCost").Width = 100
            gv1.Columns("CLCost").HeaderText = "Balance Amount"
            gv1.Columns("CLCost").FormatString = "{0:n2}"

            If cboFatSNFType.SelectedValue = "Q" Then
                gv1.Columns("Diff Fat KG").IsVisible = True
                gv1.Columns("Diff Fat KG").Width = 100
                gv1.Columns("Diff Fat KG").HeaderText = "Diff Fat KG"
                gv1.Columns("Diff Fat KG").FormatString = "{0:n2}"

                gv1.Columns("Diff SNF KG").IsVisible = True
                gv1.Columns("Diff SNF KG").Width = 100
                gv1.Columns("Diff SNF KG").HeaderText = "Diff Fat KG"
                gv1.Columns("Diff SNF KG").FormatString = "{0:n2}"
            End If
            If ChkMRPWise.Checked = True Then
                gv1.Columns("MRP").IsVisible = True
                gv1.Columns("MRP").Width = 100
                gv1.Columns("MRP").HeaderText = "MRP"
                gv1.Columns("MRP").FormatString = "{0:n2}"
            End If



            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item2 As New GridViewSummaryItem("RecQty", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("RecCost", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("IssQty", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("IssCost", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("CLQty", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("CLCost", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)


            item2 = New GridViewSummaryItem("OPFAT", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("OPSNF", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("RecFAT", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("RecSNF", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("IssFAT", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("IssSNF", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("CLFAT", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("CLSNF", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)


            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

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

            gv1.Columns("OPFAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("OPFAT").Width = 100
            gv1.Columns("OPFAT").HeaderText = "Opening FAT"
            gv1.Columns("OPFAT").FormatString = "{0:n2}"

            gv1.Columns("OPFATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("OPFATPER").Width = 100
            gv1.Columns("OPFATPER").HeaderText = "Opening Fat %"
            gv1.Columns("OPFATPER").FormatString = "{0:n2}"

            gv1.Columns("OPSNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("OPSNF").Width = 100
            gv1.Columns("OPSNF").HeaderText = "Opening SNF"
            gv1.Columns("OPSNF").FormatString = "{0:n2}"

            gv1.Columns("OPSNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("OPSNFPER").Width = 100
            gv1.Columns("OPSNFPER").HeaderText = "Opening SNF%"
            gv1.Columns("OPSNFPER").FormatString = "{0:n2}"

            gv1.Columns("OPCost").IsVisible = True
            gv1.Columns("OPCost").Width = 100
            gv1.Columns("OPCost").HeaderText = "Opening Amount"
            gv1.Columns("OPCost").FormatString = "{0:n2}"


            gv1.Columns("OPFATRate").IsVisible = True
            gv1.Columns("OPFATRate").Width = 100
            gv1.Columns("OPFATRate").HeaderText = "Opening FAT Rate"
            gv1.Columns("OPFATRate").FormatString = "{0:n2}"

            gv1.Columns("OPFATAmt").IsVisible = True
            gv1.Columns("OPFATAmt").Width = 100
            gv1.Columns("OPFATAmt").HeaderText = "Opening FAT Amt"
            gv1.Columns("OPFATAmt").FormatString = "{0:n2}"

            gv1.Columns("OPSNFRate").IsVisible = True
            gv1.Columns("OPSNFRate").Width = 100
            gv1.Columns("OPSNFRate").HeaderText = "Opening SNF Rate"
            gv1.Columns("OPSNFRate").FormatString = "{0:n2}"

            gv1.Columns("OPSNFAmt").IsVisible = True
            gv1.Columns("OPSNFAmt").Width = 100
            gv1.Columns("OPSNFAmt").HeaderText = "Opening SNF Amt"
            gv1.Columns("OPSNFAmt").FormatString = "{0:n2}"



            gv1.Columns("RecPurQty").IsVisible = True
            gv1.Columns("RecPurQty").Width = 100
            gv1.Columns("RecPurQty").HeaderText = "Received Purchase Quantity"
            gv1.Columns("RecPurQty").FormatString = "{0:n2}"

            gv1.Columns("RecPurRate").IsVisible = True
            gv1.Columns("RecPurRate").Width = 100
            gv1.Columns("RecPurRate").HeaderText = "Received Purchase Rate"
            gv1.Columns("RecPurRate").FormatString = "{0:n3}"

            gv1.Columns("RecPurFAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("RecPurFAT").Width = 100
            gv1.Columns("RecPurFAT").HeaderText = "Received Purchase FAT"
            gv1.Columns("RecPurFAT").FormatString = "{0:n2}"

            gv1.Columns("RecPurFATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("RecPurFATPER").Width = 100
            gv1.Columns("RecPurFATPER").HeaderText = "Received Purchase Fat %"
            gv1.Columns("RecPurFATPER").FormatString = "{0:n2}"

            gv1.Columns("RecPurSNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("RecPurSNF").Width = 100
            gv1.Columns("RecPurSNF").HeaderText = "Received Purchase SNF"
            gv1.Columns("RecPurSNF").FormatString = "{0:n2}"

            gv1.Columns("RecPurSNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("RecPurSNFPER").Width = 100
            gv1.Columns("RecPurSNFPER").HeaderText = "Received Purchase SNF%"
            gv1.Columns("RecPurSNFPER").FormatString = "{0:n2}"

            gv1.Columns("RecPurCost").IsVisible = True
            gv1.Columns("RecPurCost").Width = 100
            gv1.Columns("RecPurCost").HeaderText = "Received Purchase Amount"
            gv1.Columns("RecPurCost").FormatString = "{0:n2}"


            gv1.Columns("RecProQty").IsVisible = True
            gv1.Columns("RecProQty").Width = 100
            gv1.Columns("RecProQty").HeaderText = "Received Production Quantity"
            gv1.Columns("RecProQty").FormatString = "{0:n2}"

            gv1.Columns("RecProRate").IsVisible = True
            gv1.Columns("RecProRate").Width = 100
            gv1.Columns("RecProRate").HeaderText = "Received Production Rate"
            gv1.Columns("RecProRate").FormatString = "{0:n3}"

            gv1.Columns("RecProFAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("RecProFAT").Width = 100
            gv1.Columns("RecProFAT").HeaderText = "Received Production FAT"
            gv1.Columns("RecProFAT").FormatString = "{0:n2}"

            gv1.Columns("RecProFATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("RecProFATPER").Width = 100
            gv1.Columns("RecProFATPER").HeaderText = "Received Production Fat %"
            gv1.Columns("RecProFATPER").FormatString = "{0:n2}"

            gv1.Columns("RecProSNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("RecProSNF").Width = 100
            gv1.Columns("RecProSNF").HeaderText = "Received Production SNF"
            gv1.Columns("RecProSNF").FormatString = "{0:n2}"

            gv1.Columns("RecProSNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("RecProSNFPER").Width = 100
            gv1.Columns("RecProSNFPER").HeaderText = "Received Production SNF%"
            gv1.Columns("RecProSNFPER").FormatString = "{0:n2}"

            gv1.Columns("RecProCost").IsVisible = True
            gv1.Columns("RecProCost").Width = 100
            gv1.Columns("RecProCost").HeaderText = "Received Production Amount"
            gv1.Columns("RecProCost").FormatString = "{0:n2}"


            gv1.Columns("RecAdjQty").IsVisible = True
            gv1.Columns("RecAdjQty").Width = 100
            gv1.Columns("RecAdjQty").HeaderText = "Received Adjustment Quantity"
            gv1.Columns("RecAdjQty").FormatString = "{0:n2}"

            gv1.Columns("RecAdjRate").IsVisible = True
            gv1.Columns("RecAdjRate").Width = 100
            gv1.Columns("RecAdjRate").HeaderText = "Received Adjustment Rate"
            gv1.Columns("RecAdjRate").FormatString = "{0:n3}"

            gv1.Columns("RecAdjFAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("RecAdjFAT").Width = 100
            gv1.Columns("RecAdjFAT").HeaderText = "Received Adjustment FAT"
            gv1.Columns("RecAdjFAT").FormatString = "{0:n2}"

            gv1.Columns("RecAdjFATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("RecAdjFATPER").Width = 100
            gv1.Columns("RecAdjFATPER").HeaderText = "Received Adjustment Fat %"
            gv1.Columns("RecAdjFATPER").FormatString = "{0:n2}"

            gv1.Columns("RecAdjSNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("RecAdjSNF").Width = 100
            gv1.Columns("RecAdjSNF").HeaderText = "Received Production SNF"
            gv1.Columns("RecAdjSNF").FormatString = "{0:n2}"

            gv1.Columns("RecAdjSNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("RecAdjSNFPER").Width = 100
            gv1.Columns("RecAdjSNFPER").HeaderText = "Received Adjustment SNF%"
            gv1.Columns("RecAdjSNFPER").FormatString = "{0:n2}"

            gv1.Columns("RecAdjCost").IsVisible = True
            gv1.Columns("RecAdjCost").Width = 100
            gv1.Columns("RecAdjCost").HeaderText = "Received Adjustment Amount"
            gv1.Columns("RecAdjCost").FormatString = "{0:n2}"


            gv1.Columns("RecOthQty").IsVisible = True
            gv1.Columns("RecOthQty").Width = 100
            gv1.Columns("RecOthQty").HeaderText = "Received Other Quantity"
            gv1.Columns("RecOthQty").FormatString = "{0:n2}"

            gv1.Columns("RecOthRate").IsVisible = True
            gv1.Columns("RecOthRate").Width = 100
            gv1.Columns("RecOthRate").HeaderText = "Received Other Rate"
            gv1.Columns("RecOthRate").FormatString = "{0:n3}"

            gv1.Columns("RecOthFAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("RecOthFAT").Width = 100
            gv1.Columns("RecOthFAT").HeaderText = "Received Other FAT"
            gv1.Columns("RecOthFAT").FormatString = "{0:n2}"

            gv1.Columns("RecOthFATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("RecOthFATPER").Width = 100
            gv1.Columns("RecOthFATPER").HeaderText = "Received Other Fat %"
            gv1.Columns("RecOthFATPER").FormatString = "{0:n2}"

            gv1.Columns("RecOthSNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("RecOthSNF").Width = 100
            gv1.Columns("RecOthSNF").HeaderText = "Received Other SNF"
            gv1.Columns("RecOthSNF").FormatString = "{0:n2}"

            gv1.Columns("RecOthSNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("RecOthSNFPER").Width = 100
            gv1.Columns("RecOthSNFPER").HeaderText = "Received Other SNF%"
            gv1.Columns("RecOthSNFPER").FormatString = "{0:n2}"

            gv1.Columns("RecOthCost").IsVisible = True
            gv1.Columns("RecOthCost").Width = 100
            gv1.Columns("RecOthCost").HeaderText = "Received Other Amount"
            gv1.Columns("RecOthCost").FormatString = "{0:n2}"


            gv1.Columns("DiffRate").IsVisible = False
            gv1.Columns("DiffRate").HeaderText = "Diff Rate"
            gv1.Columns("DiffRate").FormatString = "{0:n2}"

            gv1.Columns("DiffFATPer").IsVisible = False
            gv1.Columns("DiffFATPer").HeaderText = "Diff FAT %"
            gv1.Columns("DiffFATPer").FormatString = "{0:n2}"

            gv1.Columns("DiffSNFPer").IsVisible = False
            gv1.Columns("DiffSNFPer").HeaderText = "Diff SNF %"
            gv1.Columns("DiffSNFPer").FormatString = "{0:n2}"



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

            gv1.Columns("IssFAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("IssFAT").Width = 100
            gv1.Columns("IssFAT").HeaderText = "Issued FAT"
            gv1.Columns("IssFAT").FormatString = "{0:n2}"

            gv1.Columns("IssFATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("IssFATPER").Width = 100
            gv1.Columns("IssFATPER").HeaderText = "Issued Fat %"
            gv1.Columns("IssFATPER").FormatString = "{0:n2}"

            gv1.Columns("IssSNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("IssSNF").Width = 100
            gv1.Columns("IssSNF").HeaderText = "Issued SNF"
            gv1.Columns("IssSNF").FormatString = "{0:n2}"

            gv1.Columns("IssSNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
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

            gv1.Columns("IssSaleFAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("IssSaleFAT").Width = 100
            gv1.Columns("IssSaleFAT").HeaderText = "Issued Sale FAT"
            gv1.Columns("IssSaleFAT").FormatString = "{0:n2}"

            gv1.Columns("IssSaleFATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("IssSaleFATPER").Width = 100
            gv1.Columns("IssSaleFATPER").HeaderText = "Issued Sale Fat %"
            gv1.Columns("IssSaleFATPER").FormatString = "{0:n2}"

            gv1.Columns("IssSaleSNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("IssSaleSNF").Width = 100
            gv1.Columns("IssSaleSNF").HeaderText = "Issued Sale SNF"
            gv1.Columns("IssSaleSNF").FormatString = "{0:n2}"

            gv1.Columns("IssSaleSNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("IssSaleSNFPER").Width = 100
            gv1.Columns("IssSaleSNFPER").HeaderText = "Issued Sale SNF%"
            gv1.Columns("IssSaleSNFPER").FormatString = "{0:n2}"

            gv1.Columns("IssSaleCost").IsVisible = True
            gv1.Columns("IssSaleCost").Width = 100
            gv1.Columns("IssSaleCost").HeaderText = "Issued Sale Amount"
            gv1.Columns("IssSaleCost").FormatString = "{0:n2}"

            gv1.Columns("IssTransferQty").IsVisible = True
            gv1.Columns("IssTransferQty").Width = 100
            gv1.Columns("IssTransferQty").HeaderText = "Issued/Transfer Quantity"
            gv1.Columns("IssTransferQty").FormatString = "{0:n2}"

            gv1.Columns("IssTransferRate").IsVisible = True
            gv1.Columns("IssTransferRate").Width = 100
            gv1.Columns("IssTransferRate").HeaderText = "Issued/Transfer Rate"
            gv1.Columns("IssTransferRate").FormatString = "{0:n3}"

            gv1.Columns("IssTransferFAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("IssTransferFAT").Width = 100
            gv1.Columns("IssTransferFAT").HeaderText = "Issued/Transfer FAT"
            gv1.Columns("IssTransferFAT").FormatString = "{0:n2}"

            gv1.Columns("IssTransferFATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("IssTransferFATPER").Width = 100
            gv1.Columns("IssTransferFATPER").HeaderText = "Issued/Transfer Fat %"
            gv1.Columns("IssTransferFATPER").FormatString = "{0:n2}"

            gv1.Columns("IssTransferSNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("IssTransferSNF").Width = 100
            gv1.Columns("IssTransferSNF").HeaderText = "Issued/Transfer SNF"
            gv1.Columns("IssTransferSNF").FormatString = "{0:n2}"

            gv1.Columns("IssTransferSNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("IssTransferSNFPER").Width = 100
            gv1.Columns("IssTransferSNFPER").HeaderText = "Issued/Transfer SNF%"
            gv1.Columns("IssTransferSNFPER").FormatString = "{0:n2}"

            gv1.Columns("IssTransferCost").IsVisible = True
            gv1.Columns("IssTransferCost").Width = 100
            gv1.Columns("IssTransferCost").HeaderText = "Issued/Transfer Amount"
            gv1.Columns("IssTransferCost").FormatString = "{0:n2}"


            gv1.Columns("IssIssAdjQty").IsVisible = True
            gv1.Columns("IssIssAdjQty").Width = 100
            gv1.Columns("IssIssAdjQty").HeaderText = "Issued/Adjustment Quantity"
            gv1.Columns("IssIssAdjQty").FormatString = "{0:n2}"

            gv1.Columns("IssIssAdjRate").IsVisible = True
            gv1.Columns("IssIssAdjRate").Width = 100
            gv1.Columns("IssIssAdjRate").HeaderText = "Issued/Adjustment Rate"
            gv1.Columns("IssIssAdjRate").FormatString = "{0:n3}"

            gv1.Columns("IssIssAdjFAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("IssIssAdjFAT").Width = 100
            gv1.Columns("IssIssAdjFAT").HeaderText = "Issued/Adjustment FAT"
            gv1.Columns("IssIssAdjFAT").FormatString = "{0:n2}"

            gv1.Columns("IssIssAdjFATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("IssIssAdjFATPER").Width = 100
            gv1.Columns("IssIssAdjFATPER").HeaderText = "Issued/Adjustment Fat %"
            gv1.Columns("IssIssAdjFATPER").FormatString = "{0:n2}"

            gv1.Columns("IssIssAdjSNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("IssIssAdjSNF").Width = 100
            gv1.Columns("IssIssAdjSNF").HeaderText = "Issued/Adjustment SNF"
            gv1.Columns("IssIssAdjSNF").FormatString = "{0:n2}"

            gv1.Columns("IssIssAdjSNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
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

            gv1.Columns("IssOthFAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("IssOthFAT").Width = 100
            gv1.Columns("IssOthFAT").HeaderText = "Issued Other FAT"
            gv1.Columns("IssOthFAT").FormatString = "{0:n2}"

            gv1.Columns("IssOthFATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("IssOthFATPER").Width = 100
            gv1.Columns("IssOthFATPER").HeaderText = "Issued Other Fat %"
            gv1.Columns("IssOthFATPER").FormatString = "{0:n2}"

            gv1.Columns("IssOthSNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("IssOthSNF").Width = 100
            gv1.Columns("IssOthSNF").HeaderText = "Issued Other SNF"
            gv1.Columns("IssOthSNF").FormatString = "{0:n2}"

            gv1.Columns("IssOthSNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
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




            If cboFatSNFType.SelectedValue = "Q" Then
                gv1.Columns("Diff Fat KG").IsVisible = True
                gv1.Columns("Diff Fat KG").Width = 100
                gv1.Columns("Diff Fat KG").HeaderText = "Diff Fat KG"
                gv1.Columns("Diff Fat KG").FormatString = "{0:n2}"

                gv1.Columns("Diff SNF KG").IsVisible = True
                gv1.Columns("Diff SNF KG").Width = 100
                gv1.Columns("Diff SNF KG").HeaderText = "Diff Fat KG"
                gv1.Columns("Diff SNF KG").FormatString = "{0:n2}"
            End If

            gv1.Columns("CLCost").IsVisible = True
            gv1.Columns("CLCost").Width = 100
            gv1.Columns("CLCost").HeaderText = "Balance Amount"
            gv1.Columns("CLCost").FormatString = "{0:n2}"

            gv1.Columns("CLFATRate").IsVisible = True
            gv1.Columns("CLFATRate").Width = 100
            gv1.Columns("CLFATRate").HeaderText = "Balance FAT Rate"
            gv1.Columns("CLFATRate").FormatString = "{0:n2}"

            gv1.Columns("CLFATAmount").IsVisible = True
            gv1.Columns("CLFATAmount").Width = 100
            gv1.Columns("CLFATAmount").HeaderText = "Balance FAT Amt"
            gv1.Columns("CLFATAmount").FormatString = "{0:n2}"

            gv1.Columns("CLSNFRate").IsVisible = True
            gv1.Columns("CLSNFRate").Width = 100
            gv1.Columns("CLSNFRate").HeaderText = "Balance SNF Rate"
            gv1.Columns("CLSNFRate").FormatString = "{0:n2}"

            gv1.Columns("CLSNFAmount").IsVisible = True
            gv1.Columns("CLSNFAmount").Width = 100
            gv1.Columns("CLSNFAmount").HeaderText = "Balance SNF Amt"
            gv1.Columns("CLSNFAmount").FormatString = "{0:n2}"


            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item2 As New GridViewSummaryItem("RecQty", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("RecCost", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("IssQty", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("IssCost", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("CLQty", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("CLCost", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)


            item2 = New GridViewSummaryItem("OPFAT", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("OPSNF", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("RecFAT", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("RecSNF", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("IssFAT", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("IssSNF", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("CLFAT", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("CLSNF", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)

            ''richa ERO/14/07/21-001448
            item2 = New GridViewSummaryItem("OPQty", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("RecPurQty", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("RecProQty", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("RecAdjQty", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("RecOthQty", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)

            item2 = New GridViewSummaryItem("IssTransferQty", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("IssSaleQty", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("IssIssAdjQty", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("IssOthQty", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)


            item2 = New GridViewSummaryItem("RecPurCost", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("RecProCost", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("RecAdjCost", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("RecOthCost", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("RecCost", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("IssTransferCost", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("IssSaleCost", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("IssIssAdjCost", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("IssOthCost", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("IssCost", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

            If (True) Then
                Dim test As String = Nothing
            End If

        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Transaction Wise") = CompairStringResult.Equal Then
            'Dim dt As SqlClient.SqlDataReader
            If cboFatSNFType.SelectedValue = "Q" Then
                gv1.Columns("Diff Fat KG").IsVisible = True
                gv1.Columns("Diff Fat KG").Width = 100
                gv1.Columns("Diff Fat KG").HeaderText = "Diff Fat KG"
                gv1.Columns("Diff Fat KG").FormatString = "{0:n2}"

                gv1.Columns("Diff SNF KG").IsVisible = True
                gv1.Columns("Diff SNF KG").Width = 100
                gv1.Columns("Diff SNF KG").HeaderText = "Diff Fat KG"
                gv1.Columns("Diff SNF KG").FormatString = "{0:n2}"
            End If


            For ii As Integer = 0 To gv1.Columns.Count - 1
                gv1.Columns(ii).ReadOnly = True
                gv1.Columns(ii).IsVisible = True
                If gv1.Columns(ii).Name.Contains("Cost") = True Or gv1.Columns(ii).Name.Contains("Rate") = True Or gv1.Columns(ii).Name.Contains("amount") = True Or gv1.Columns(ii).Name.Contains("amt") = True Or gv1.Columns(ii).Name.Contains("FAT") = True Or gv1.Columns(ii).Name.Contains("SNF") = True Or gv1.Columns(ii).Name.Contains("Cost") = True Then
                    gv1.Columns(gv1.Columns(ii).Name).FormatString = "{0:n2}"
                End If
            Next


            'gv1.Columns("Punching_Date").IsVisible = False
            Dim summary As Decimal = 0
            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim TransQry As String = "select code,Name,InOutType,In_Category,Out_Category,isnull(Sequence,500) as Sequence,Type as Typed from TSPL_INVENTORY_SOURCE_CODE order by Sequence asc,inouttype "
            Dim TransResult As DataTable = clsDBFuncationality.GetDataTable(TransQry)
            If TransResult IsNot Nothing AndAlso TransResult.Rows.Count > 0 Then
                For ii As Integer = 0 To TransResult.Rows.Count - 1
                    Dim item2 As New GridViewSummaryItem(TransResult.Rows(ii)("Name"), "{0:n2}", GridAggregateFunction.Sum)
                    Dim summaryItm As Decimal = item2.Evaluate(gv1.MasterTemplate)
                    'parteek 
                    If clsCommon.myCdbl(summaryItm) = 0 Then
                        gv1.Columns(TransResult.Rows(ii)("Name")).isVisible = False
                        gv1.Columns(TransResult.Rows(ii)("Name") + " Fat_%").isVisible = False
                        gv1.Columns(TransResult.Rows(ii)("Name") + "_Fat_KG").isVisible = False
                        gv1.Columns(TransResult.Rows(ii)("Name") + " SNF_%").isVisible = False
                        gv1.Columns(TransResult.Rows(ii)("Name") + "_SNF_KG").isVisible = False
                        gv1.Columns(TransResult.Rows(ii)("Name") + " TS_%").isVisible = False
                        gv1.Columns(TransResult.Rows(ii)("Name") + "_TS_KG").isVisible = False
                    End If
                    summaryRowItem.Add(item2)
                    item2 = New GridViewSummaryItem(TransResult.Rows(ii)("Name") + " Fat_%", "{0:n2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item2)
                    item2 = New GridViewSummaryItem(TransResult.Rows(ii)("Name") + "_Fat_KG", "{0:n2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item2)
                    item2 = New GridViewSummaryItem(TransResult.Rows(ii)("Name") + " SNF_%", "{0:n2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item2)
                    item2 = New GridViewSummaryItem(TransResult.Rows(ii)("Name") + "_SNF_KG", "{0:n2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item2)

                    ''======================================for QC COlumns======06/04/2017============================
                    If clsCommon.myCdbl(summaryItm) = 0 Then
                        If gv1.Columns.Contains(clsCommon.myCstr("QC " + TransResult.Rows(ii)("Name") + " Fat_%")) Then
                            gv1.Columns("QC " + TransResult.Rows(ii)("Name") + " Fat_%").isVisible = False
                        End If
                        If gv1.Columns.Contains(clsCommon.myCstr("QC " + TransResult.Rows(ii)("Name") + "_Fat_KG")) Then
                            gv1.Columns("QC " + TransResult.Rows(ii)("Name") + "_Fat_KG").isVisible = False
                        End If
                        If gv1.Columns.Contains(clsCommon.myCstr("QC " + TransResult.Rows(ii)("Name") + " SNF_%")) Then
                            gv1.Columns("QC " + TransResult.Rows(ii)("Name") + " SNF_%").isVisible = False
                        End If
                        If gv1.Columns.Contains(clsCommon.myCstr("QC " + TransResult.Rows(ii)("Name") + "_SNF_KG")) Then
                            gv1.Columns("QC " + TransResult.Rows(ii)("Name") + "_SNF_KG").isVisible = False
                        End If
                    End If
                    summaryRowItem.Add(item2)
                    If gv1.Columns.Contains(clsCommon.myCstr("QC " + TransResult.Rows(ii)("Name") + " Fat_%")) Then
                        item2 = New GridViewSummaryItem("QC " + TransResult.Rows(ii)("Name") + " Fat_%", "{0:n2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item2)
                    End If
                    If gv1.Columns.Contains(clsCommon.myCstr("QC " + TransResult.Rows(ii)("Name") + "_Fat_KG")) Then
                        item2 = New GridViewSummaryItem("QC " + TransResult.Rows(ii)("Name") + "_Fat_KG", "{0:n2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item2)
                    End If
                    If gv1.Columns.Contains(clsCommon.myCstr("QC " + TransResult.Rows(ii)("Name") + " SNF_%")) Then
                        item2 = New GridViewSummaryItem("QC " + TransResult.Rows(ii)("Name") + " SNF_%", "{0:n2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item2)
                    End If
                    If gv1.Columns.Contains(clsCommon.myCstr("QC " + TransResult.Rows(ii)("Name") + "_SNF_KG")) Then
                        item2 = New GridViewSummaryItem("QC " + TransResult.Rows(ii)("Name") + "_SNF_KG", "{0:n2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item2)
                    End If
                    For Each grow As GridViewRowInfo In gv1.Rows
                        If clsCommon.myCdbl(grow.Cells(TransResult.Rows(ii)("Name")).Value) = "0" Then
                            grow.Cells(TransResult.Rows(ii)("Name") + " Fat_%").Value = "0.00"
                            grow.Cells(TransResult.Rows(ii)("Name") + "_Fat_KG").Value = "0.00"
                            grow.Cells(TransResult.Rows(ii)("Name") + " SNF_%").Value = "0.00"
                            grow.Cells(TransResult.Rows(ii)("Name") + "_SNF_KG").Value = "0.00"

                            'sanjay
                            grow.Cells(TransResult.Rows(ii)("Name") + " TS_%").Value = "0.00"
                            grow.Cells(TransResult.Rows(ii)("Name") + "_TS_KG").Value = "0.00"
                            'sanjay

                            If gv1.Columns.Contains(clsCommon.myCstr("QC " + TransResult.Rows(ii)("Name") + " Fat_%")) Then
                                grow.Cells("QC " + TransResult.Rows(ii)("Name") + " Fat_%").Value = "0.00"
                            End If
                            If gv1.Columns.Contains(clsCommon.myCstr("QC " + TransResult.Rows(ii)("Name") + "_Fat_KG")) Then
                                grow.Cells("QC " + TransResult.Rows(ii)("Name") + "_Fat_KG").Value = "0.00"
                            End If
                            If gv1.Columns.Contains(clsCommon.myCstr("QC " + TransResult.Rows(ii)("Name") + " SNF_%")) Then
                                grow.Cells("QC " + TransResult.Rows(ii)("Name") + " SNF_%").Value = "0.00"
                            End If
                            If gv1.Columns.Contains(clsCommon.myCstr("QC " + TransResult.Rows(ii)("Name") + "_SNF_KG")) Then
                                grow.Cells("QC " + TransResult.Rows(ii)("Name") + "_SNF_KG").Value = "0.00"
                            End If
                        End If
                    Next
                Next
                gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)


            End If
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

            If ChkMRPWise.Checked = True Then
                gv1.Columns("MRP").IsVisible = True
                gv1.Columns("MRP").Width = 100
                gv1.Columns("MRP").HeaderText = "MRP"
                gv1.Columns("MRP").FormatString = "{0:n2}"
            End If



            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item2 As New GridViewSummaryItem("RecQty", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("RecCost", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("IssQty", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("IssCost", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            ''richa agarwal 05-feb,2016 changes from sum to last because report showing wrong balance
            item2 = New GridViewSummaryItem("CLQty", "{0:n2}", GridAggregateFunction.Last)
            summaryRowItem.Add(item2)
            item2 = New GridViewSummaryItem("CLCost", "{0:n2}", GridAggregateFunction.Last)
            ''------------------------------------
            summaryRowItem.Add(item2)
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

            Dim descriptor3 As New GroupDescriptor()
            descriptor3.GroupNames.Add("Item_Code", System.ComponentModel.ListSortDirection.Ascending)
            gv1.GroupDescriptors.Add(descriptor3)
            gv1.MasterTemplate.AutoExpandGroups = True
        End If

        If gv1.Columns.Contains("Main_Location_Code") Then
            gv1.Columns("Main_Location_Code").HeaderText = "Main Location Code"
            gv1.Columns("MainLocationDesc").IsVisible = True
            gv1.Columns("MainLocationDesc").Width = 100
        End If
        If gv1.Columns.Contains("MainLocationDesc") Then
            gv1.Columns("MainLocationDesc").IsVisible = True
            gv1.Columns("MainLocationDesc").Width = 150
            gv1.Columns("MainLocationDesc").HeaderText = "Main Location"
        End If



        SetFormat_QC_FATSNF_COL()
        gv1.BestFitColumns()
        ReStoreGridLayout()
    End Sub

    Private Sub SetFormat_QC_FATSNF_COL() ''Add by Monika 29/03/2017
        Dim strColName As String = ""
        If gv1.Columns.Contains("Diff FAT") Then
            strColName = "Diff FAT"
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Diff FAT"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("Diff SNF") Then
            strColName = "Diff SNF"
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Diff SNF"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("OPQCFATPER") OrElse gv1.Columns.Contains("ACTOPFATPER") Then
            If gv1.Columns.Contains("OPQCFATPER") Then
                strColName = "OPQCFATPER"
            ElseIf gv1.Columns.Contains("ACTOPFATPER") Then
                strColName = "ACTOPFATPER"
            End If
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Op. QC FAT%"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("OPQCFAT") OrElse gv1.Columns.Contains("ACTOPFAT") Then
            If gv1.Columns.Contains("OPQCFAT") Then
                strColName = "OPQCFAT"
            ElseIf gv1.Columns.Contains("ACTOPFAT") Then
                strColName = "ACTOPFAT"
            End If
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Op. QC FAT KG"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("OPQCSNFPER") OrElse gv1.Columns.Contains("ACTOPSNFPER") Then
            If gv1.Columns.Contains("OPQCSNFPER") Then
                strColName = "OPQCSNFPER"
            ElseIf gv1.Columns.Contains("ACTOPSNFPER") Then
                strColName = "ACTOPSNFPER"
            End If
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Op. QC SNF%"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("OPQCSNF") OrElse gv1.Columns.Contains("ACTOPSNF") Then
            If gv1.Columns.Contains("OPQCSNF") Then
                strColName = "OPQCSNF"
            ElseIf gv1.Columns.Contains("ACTOPSNF") Then
                strColName = "ACTOPSNF"
            End If
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Op. QC SNF KG"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("ACT_Received_FATPER") OrElse gv1.Columns.Contains("ACTRecFATPER") Then
            If gv1.Columns.Contains("ACT_Received_FATPER") Then
                strColName = "ACT_Received_FATPER"
            ElseIf gv1.Columns.Contains("ACTRecFATPER") Then
                strColName = "ACTRecFATPER"
            End If
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Rec. QC FAT%"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("ACT_Received_FAT") OrElse gv1.Columns.Contains("ACTRecFAT") Then
            If gv1.Columns.Contains("ACT_Received_FAT") Then
                strColName = "ACT_Received_FAT"
            ElseIf gv1.Columns.Contains("ACTRecFAT") Then
                strColName = "ACTRecFAT"
            End If
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Rec. QC FAT KG"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("ACT_Received_SNFPER") OrElse gv1.Columns.Contains("ACTRecSNFPER") Then
            If gv1.Columns.Contains("ACT_Received_SNFPER") Then
                strColName = "ACT_Received_SNFPER"
            ElseIf gv1.Columns.Contains("ACTRecSNFPER") Then
                strColName = "ACTRecSNFPER"
            End If
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Rec. QC SNF%"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("ACT_Received_SNF") OrElse gv1.Columns.Contains("ACTRecSNF") Then
            If gv1.Columns.Contains("ACT_Received_SNF") Then
                strColName = "ACT_Received_SNF"
            ElseIf gv1.Columns.Contains("ACTRecSNF") Then
                strColName = "ACTRecSNF"
            End If
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Rec. QC SNF KG"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("ACT_Issued_FATPER") OrElse gv1.Columns.Contains("ACTIssFATPER") Then
            If gv1.Columns.Contains("ACT_Issued_FATPER") Then
                strColName = "ACT_Issued_FATPER"
            ElseIf gv1.Columns.Contains("ACTIssFATPER") Then
                strColName = "ACTIssFATPER"
            End If
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Iss. QC FAT%"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("ACT_Issued_FAT") OrElse gv1.Columns.Contains("ACTIssFAT") Then
            If gv1.Columns.Contains("ACT_Issued_FAT") Then
                strColName = "ACT_Issued_FAT"
            ElseIf gv1.Columns.Contains("ACTIssFAT") Then
                strColName = "ACTIssFAT"
            End If
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Iss. QC FAT KG"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("ACT_Issued_SNFPER") OrElse gv1.Columns.Contains("ACTIssSNFPER") Then
            If gv1.Columns.Contains("ACT_Issued_SNFPER") Then
                strColName = "ACT_Issued_SNFPER"
            ElseIf gv1.Columns.Contains("ACTIssSNFPER") Then
                strColName = "ACTIssSNFPER"
            End If
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Iss. QC SNF%"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("ACT_Issued_SNF") OrElse gv1.Columns.Contains("ACTIssSNF") Then
            If gv1.Columns.Contains("ACT_Issued_SNF") Then
                strColName = "ACT_Issued_SNF"
            ElseIf gv1.Columns.Contains("ACTIssSNF") Then
                strColName = "ACTIssSNF"
            End If
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Iss. QC SNF KG"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("ACT_Balance_FATPER") OrElse gv1.Columns.Contains("ACTBalance_FATPER") Then
            If gv1.Columns.Contains("ACT_Balance_FATPER") Then
                strColName = "ACT_Balance_FATPER"
            ElseIf gv1.Columns.Contains("ACTBalance_FATPER") Then
                strColName = "ACTBalance_FATPER"
            End If
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Bal. QC FAT%"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("ACT_Balance_FAT") OrElse gv1.Columns.Contains("ACTBalance_FAT") Then
            If gv1.Columns.Contains("ACT_Balance_FAT") Then
                strColName = "ACT_Balance_FAT"
            ElseIf gv1.Columns.Contains("ACTBalance_FAT") Then
                strColName = "ACTBalance_FAT"
            End If
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Bal. QC FAT KG"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("ACT_Balance_SNFPER") OrElse gv1.Columns.Contains("ACTBalance_SNFPER") Then
            If gv1.Columns.Contains("ACT_Balance_SNFPER") Then
                strColName = "ACT_Balance_SNFPER"
            ElseIf gv1.Columns.Contains("ACTBalance_SNFPER") Then
                strColName = "ACTBalance_SNFPER"
            End If
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Bal. QC SNF%"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("ACT_Balance_SNF") OrElse gv1.Columns.Contains("ACTBalance_SNF") Then
            If gv1.Columns.Contains("ACT_Balance_SNF") Then
                strColName = "ACT_Balance_SNF"
            ElseIf gv1.Columns.Contains("ACTBalance_SNF") Then
                strColName = "ACTBalance_SNF"
            End If
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Bal. QC SNF KG"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("ACTCLFATPER") Then
            strColName = "ACTCLFATPER"
            gv1.Columns("ACTCLFATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("ACTCLFATPER").Width = 100
            gv1.Columns("ACTCLFATPER").HeaderText = "Closing QC FAT%"
            gv1.Columns("ACTCLFATPER").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("ACTCLFAT") Then
            strColName = "ACTCLFAT"
            gv1.Columns("ACTCLFAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("ACTCLFAT").Width = 100
            gv1.Columns("ACTCLFAT").HeaderText = "Closing QC FAT KG"
            gv1.Columns("ACTCLFAT").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("ACTCLSNFPER") Then
            strColName = "ACTCLSNFPER"
            gv1.Columns("ACTCLSNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("ACTCLSNFPER").Width = 100
            gv1.Columns("ACTCLSNFPER").HeaderText = "Closing QC SNF%"
            gv1.Columns("ACTCLSNFPER").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("ACTCLSNF") Then
            strColName = "ACTCLSNF"
            gv1.Columns("ACTCLSNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("ACTCLSNF").Width = 100
            gv1.Columns("ACTCLSNF").HeaderText = "Closing QC SNF KG"
            gv1.Columns("ACTCLSNF").FormatString = "{0:n2}"
        End If
        ''=======================
        If gv1.Columns.Contains("ACTRecPurFATPER") Then
            strColName = "ACTRecPurFATPER"
            gv1.Columns("ACTRecPurFATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("ACTRecPurFATPER").Width = 100
            gv1.Columns("ACTRecPurFATPER").HeaderText = "Rec. QC Purchase FAT%"
            gv1.Columns("ACTRecPurFATPER").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("ACTRecPurFAT") Then
            strColName = "ACTRecPurFAT"
            gv1.Columns("ACTRecPurFAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("ACTRecPurFAT").Width = 100
            gv1.Columns("ACTRecPurFAT").HeaderText = "Rec. QC Purchase FAT KG"
            gv1.Columns("ACTRecPurFAT").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("ACTRecPurSNFPER") Then
            strColName = "ACTRecPurSNFPER"
            gv1.Columns("ACTRecPurSNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("ACTRecPurSNFPER").Width = 100
            gv1.Columns("ACTRecPurSNFPER").HeaderText = "Rec. QC Purchase SNF%"
            gv1.Columns("ACTRecPurSNFPER").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("ACTRecPurSNF") Then
            strColName = "ACTRecPurSNF"
            gv1.Columns("ACTRecPurSNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("ACTRecPurSNF").Width = 100
            gv1.Columns("ACTRecPurSNF").HeaderText = "Rec. QC Purchase SNF KG"
            gv1.Columns("ACTRecPurSNF").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("ACTRecAdjProFATPER") Then
            strColName = "ACTRecAdjProFATPER"
            gv1.Columns("ACTRecAdjProFATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("ACTRecAdjProFATPER").Width = 100
            gv1.Columns("ACTRecAdjProFATPER").HeaderText = "Rec. QC Adjustment/Production FAT%"
            gv1.Columns("ACTRecAdjProFATPER").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("ACTRecAdjProFAT") Then
            strColName = "ACTRecAdjProFAT"
            gv1.Columns("ACTRecAdjProFAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("ACTRecAdjProFAT").Width = 100
            gv1.Columns("ACTRecAdjProFAT").HeaderText = "Rec. QC Adjustment/Production FAT KG"
            gv1.Columns("ACTRecAdjProFAT").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("ACTRecAdjProSNFPER") Then
            strColName = "ACTRecAdjProSNFPER"
            gv1.Columns("ACTRecAdjProSNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("ACTRecAdjProSNFPER").Width = 100
            gv1.Columns("ACTRecAdjProSNFPER").HeaderText = "Rec. QC Adjustment/Production SNF%"
            gv1.Columns("ACTRecAdjProSNFPER").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("ACTRecAdjProSNF") Then
            strColName = "ACTRecAdjProSNF"
            gv1.Columns("ACTRecAdjProSNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("ACTRecAdjProSNF").Width = 100
            gv1.Columns("ACTRecAdjProSNF").HeaderText = "Rec. QC Adjustment/Production SNF KG"
            gv1.Columns("ACTRecAdjProSNF").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("ACTRecOthFATPER") Then
            strColName = "ACTRecOthFATPER"
            gv1.Columns("ACTRecOthFATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("ACTRecOthFATPER").Width = 100
            gv1.Columns("ACTRecOthFATPER").HeaderText = "Rec. QC Other FAT%"
            gv1.Columns("ACTRecOthFATPER").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("ACTRecOthFAT") Then
            strColName = "ACTRecOthFAT"
            gv1.Columns("ACTRecOthFAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("ACTRecOthFAT").Width = 100
            gv1.Columns("ACTRecOthFAT").HeaderText = "Rec. QC Other FAT KG"
            gv1.Columns("ACTRecOthFAT").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("ACTRecOthSNFPER") Then
            strColName = "ACTRecOthSNFPER"
            gv1.Columns("ACTRecOthSNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("ACTRecOthSNFPER").Width = 100
            gv1.Columns("ACTRecOthSNFPER").HeaderText = "Rec. QC Other SNF%"
            gv1.Columns("ACTRecOthSNFPER").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("ACTRecOthSNF") Then
            strColName = "ACTRecOthSNF"
            gv1.Columns("ACTRecOthSNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("ACTRecOthSNF").Width = 100
            gv1.Columns("ACTRecOthSNF").HeaderText = "Rec. QC Other SNF KG"
            gv1.Columns("ACTRecOthSNF").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("ACTRecFATPER") Then
            strColName = "ACTRecFATPER"
            gv1.Columns("ACTRecFATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("ACTRecFATPER").Width = 100
            gv1.Columns("ACTRecFATPER").HeaderText = "Rec. QC FAT%"
            gv1.Columns("ACTRecFATPER").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("ACTRecFAT") Then
            strColName = "ACTRecFAT"
            gv1.Columns("ACTRecFAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("ACTRecFAT").Width = 100
            gv1.Columns("ACTRecFAT").HeaderText = "Rec. QC FAT KG"
            gv1.Columns("ACTRecFAT").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("ACTRecSNFPER") Then
            strColName = "ACTRecSNFPER"
            gv1.Columns("ACTRecSNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("ACTRecSNFPER").Width = 100
            gv1.Columns("ACTRecSNFPER").HeaderText = "Rec. QC SNF%"
            gv1.Columns("ACTRecSNFPER").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("ACTRecSNF") Then
            strColName = "ACTRecSNF"
            gv1.Columns("ACTRecSNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("ACTRecSNF").Width = 100
            gv1.Columns("ACTRecSNF").HeaderText = "Rec. QC SNF KG"
            gv1.Columns("ACTRecSNF").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("ACTIssSaleFATPER") Then
            strColName = "ACTIssSaleFATPER"
            gv1.Columns("ACTIssSaleFATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("ACTIssSaleFATPER").Width = 100
            gv1.Columns("ACTIssSaleFATPER").HeaderText = "Iss. QC Sale FAT%"
            gv1.Columns("ACTIssSaleFATPER").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("ACTIssSaleFAT") Then
            strColName = "ACTIssSaleFAT"
            gv1.Columns("ACTIssSaleFAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("ACTIssSaleFAT").Width = 100
            gv1.Columns("ACTIssSaleFAT").HeaderText = "Iss. QC Sale FAT KG"
            gv1.Columns("ACTIssSaleFAT").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("ACTIssSaleSNFPER") Then
            strColName = "ACTIssSaleSNFPER"
            gv1.Columns("ACTIssSaleSNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("ACTIssSaleSNFPER").Width = 100
            gv1.Columns("ACTIssSaleSNFPER").HeaderText = "Iss. QC Sale SNF%"
            gv1.Columns("ACTIssSaleSNFPER").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("ACTIssSaleSNF") Then
            strColName = "ACTIssSaleSNF"
            gv1.Columns("ACTIssSaleSNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("ACTIssSaleSNF").Width = 100
            gv1.Columns("ACTIssSaleSNF").HeaderText = "Iss. QC Sale SNF KG"
            gv1.Columns("ACTIssSaleSNF").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("ACTIssIssAdjFATPER") Then
            strColName = "ACTIssIssAdjFATPER"
            gv1.Columns("ACTIssIssAdjFATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("ACTIssIssAdjFATPER").Width = 100
            gv1.Columns("ACTIssIssAdjFATPER").HeaderText = "Iss./Adjustment QC FAT%"
            gv1.Columns("ACTIssIssAdjFATPER").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("ACTIssIssAdjFAT") Then
            strColName = "ACTIssIssAdjFAT"
            gv1.Columns("ACTIssIssAdjFAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("ACTIssIssAdjFAT").Width = 100
            gv1.Columns("ACTIssIssAdjFAT").HeaderText = "Iss./Adjustment QC FAT KG"
            gv1.Columns("ACTIssIssAdjFAT").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("ACTIssIssAdjSNFPER") Then
            strColName = "ACTIssIssAdjSNFPER"
            gv1.Columns("ACTIssIssAdjSNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("ACTIssIssAdjSNFPER").Width = 100
            gv1.Columns("ACTIssIssAdjSNFPER").HeaderText = "Iss./Adjustment QC SNF%"
            gv1.Columns("ACTIssIssAdjSNFPER").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("ACTIssIssAdjSNF") Then
            strColName = "ACTIssIssAdjSNF"
            gv1.Columns("ACTIssIssAdjSNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("ACTIssIssAdjSNF").Width = 100
            gv1.Columns("ACTIssIssAdjSNF").HeaderText = "Iss./Adjustment QC SNF KG"
            gv1.Columns("ACTIssIssAdjSNF").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("ACTIssOthFATPER") Then
            strColName = "ACTIssOthFATPER"
            gv1.Columns("ACTIssOthFATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("ACTIssOthFATPER").Width = 100
            gv1.Columns("ACTIssOthFATPER").HeaderText = "Iss. QC Other FAT%"
            gv1.Columns("ACTIssOthFATPER").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("ACTIssOthFAT") Then
            strColName = "ACTIssOthFAT"
            gv1.Columns("ACTIssOthFAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("ACTIssOthFAT").Width = 100
            gv1.Columns("ACTIssOthFAT").HeaderText = "Iss. QC Other FAT KG"
            gv1.Columns("ACTIssOthFAT").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("ACTIssOthSNFPER") Then
            strColName = "ACTIssOthSNFPER"
            gv1.Columns("ACTIssOthSNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("ACTIssOthSNFPER").Width = 100
            gv1.Columns("ACTIssOthSNFPER").HeaderText = "Iss. QC Other SNF%"
            gv1.Columns("ACTIssOthSNFPER").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("ACTIssOthSNF") Then
            strColName = "ACTIssOthSNF"
            gv1.Columns("ACTIssOthSNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("ACTIssOthSNF").Width = 100
            gv1.Columns("ACTIssOthSNF").HeaderText = "Iss. QC Other SNF KG"
            gv1.Columns("ACTIssOthSNF").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("ACTIssFATPER") Then
            strColName = "ACTIssFATPER"
            gv1.Columns("ACTIssFATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("ACTIssFATPER").Width = 100
            gv1.Columns("ACTIssFATPER").HeaderText = "Issue QC FAT%"
            gv1.Columns("ACTIssFATPER").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("ACTIssFAT") Then
            strColName = "ACTIssFAT"
            gv1.Columns("ACTIssFAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("ACTIssFAT").Width = 100
            gv1.Columns("ACTIssFAT").HeaderText = "Issue QC FAT KG"
            gv1.Columns("ACTIssFAT").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("ACTIssSNFPER") Then
            strColName = "ACTIssSNFPER"
            gv1.Columns("ACTIssSNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("ACTIssSNFPER").Width = 100
            gv1.Columns("ACTIssSNFPER").HeaderText = "Issue QC SNF%"
            gv1.Columns("ACTIssSNFPER").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("ACTIssSNF") Then
            strColName = "ACTIssSNF"
            gv1.Columns("ACTIssSNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("ACTIssSNF").Width = 100
            gv1.Columns("ACTIssSNF").HeaderText = "Issue QC SNF KG"
            gv1.Columns("ACTIssSNF").FormatString = "{0:n2}"
        End If

        If gv1.Columns.Contains("Op Diff. Fat%") Then
            strColName = "Op Diff. Fat%"
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Op Diff. QC Fat%"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If

        If gv1.Columns.Contains("Op Diff. FAT KG") Then
            strColName = "Op Diff. FAT KG"
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Op Diff. QC FAT KG"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If

        If gv1.Columns.Contains("Op Diff. SNF%") Then
            strColName = "Op Diff. SNF%"
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Op Diff. QC SNF%"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If

        If gv1.Columns.Contains("Op Diff. SNF KG") Then
            strColName = "Op Diff. SNF KG"
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Op Diff. QC SNF KG"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("Diff. Rec. FAT%") Then
            strColName = "Diff. Rec. FAT%"
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Diff. QC Rec. FAT%"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If

        If gv1.Columns.Contains("Diff. Rec. FAT KG") Then
            strColName = "Diff. Rec. FAT KG"
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Diff. QC Rec. FAT KG"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If

        If gv1.Columns.Contains("Diff. Rec. SNF%") Then
            strColName = "Diff. Rec. SNF%"
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Diff. QC Rec. SNF%"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If

        If gv1.Columns.Contains("Diff. Rec. SNF KG") Then
            strColName = "Diff. Rec. SNF KG"
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Diff. QC Rec. SNF KG"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If

        If gv1.Columns.Contains("Diff. Iss. FAT%") Then
            strColName = "Diff. Iss. FAT%"
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Diff. QC Iss. FAT%"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If

        If gv1.Columns.Contains("Diff. Iss. FAT KG") Then
            strColName = "Diff. Iss. FAT KG"
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Diff. QC Iss. FAT KG"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If

        If gv1.Columns.Contains("Diff. Iss. SNF%") Then
            strColName = "Diff. Iss. SNF%"
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Diff. QC Iss. SNF%"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If

        If gv1.Columns.Contains("Diff. Iss. SNF KG") Then
            strColName = "Diff. Iss. SNF KG"
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Diff. QC Iss. SNF KG"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If

        If gv1.Columns.Contains("Diff. Bal. FAT%") Then
            strColName = "Diff. Bal. FAT%"
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Diff. QC Bal. FAT%"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If

        If gv1.Columns.Contains("Diff. Bal. FAT KG") Then
            strColName = "Diff. Bal. FAT KG"
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Diff. QC Bal. FAT KG"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If

        If gv1.Columns.Contains("Diff. Bal. SNF%") Then
            strColName = "Diff. Bal. SNF%"
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Diff. QC Bal. SNF%"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If

        If gv1.Columns.Contains("Diff. Bal. SNF KG") Then
            strColName = "Diff. Bal. SNF KG"
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Diff. QC Bal. SNF KG"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If


        ''================standard columns

        If gv1.Columns.Contains("OPSTDFATPER") OrElse gv1.Columns.Contains("STDOPFATPER") Then
            If gv1.Columns.Contains("OPSTDFATPER") Then
                strColName = "OPSTDFATPER"
            ElseIf gv1.Columns.Contains("STDOPFATPER") Then
                strColName = "STDOPFATPER"
            End If
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Op. STD. FAT%"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("OPSTDFAT") OrElse gv1.Columns.Contains("STDOPFAT") Then
            If gv1.Columns.Contains("OPSTDFAT") Then
                strColName = "OPSTDFAT"
            ElseIf gv1.Columns.Contains("STDOPFAT") Then
                strColName = "STDOPFAT"
            End If
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Op. STD. FAT KG"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("OPSTDSNFPER") OrElse gv1.Columns.Contains("STDOPSNFPER") Then
            If gv1.Columns.Contains("OPSTDSNFPER") Then
                strColName = "OPSTDSNFPER"
            ElseIf gv1.Columns.Contains("STDOPSNFPER") Then
                strColName = "STDOPSNFPER"
            End If
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Op. STD SNF%"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("OPSTDSNF") OrElse gv1.Columns.Contains("STDOPSNF") Then
            If gv1.Columns.Contains("OPSTDSNF") Then
                strColName = "OPSTDSNF"
            ElseIf gv1.Columns.Contains("STDOPSNF") Then
                strColName = "STDOPSNF"
            End If
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Op. STD SNF KG"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("STD_Received_FATPER") OrElse gv1.Columns.Contains("STDRecFATPER") Then
            If gv1.Columns.Contains("STD_Received_FATPER") Then
                strColName = "STD_Received_FATPER"
            ElseIf gv1.Columns.Contains("STDRecFATPER") Then
                strColName = "STDRecFATPER"
            End If
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Rec. STD FAT%"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("STD_Received_FAT") OrElse gv1.Columns.Contains("STDRecFAT") Then
            If gv1.Columns.Contains("STD_Received_FAT") Then
                strColName = "STD_Received_FAT"
            ElseIf gv1.Columns.Contains("STDRecFAT") Then
                strColName = "STDRecFAT"
            End If
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Rec. STD FAT KG"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("STD_Received_SNFPER") OrElse gv1.Columns.Contains("STDRecSNFPER") Then
            If gv1.Columns.Contains("STD_Received_SNFPER") Then
                strColName = "STD_Received_SNFPER"
            ElseIf gv1.Columns.Contains("STDRecSNFPER") Then
                strColName = "STDRecSNFPER"
            End If
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Rec. STD SNF%"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("STD_Received_SNF") OrElse gv1.Columns.Contains("STDRecSNF") Then
            If gv1.Columns.Contains("STD_Received_SNF") Then
                strColName = "STD_Received_SNF"
            ElseIf gv1.Columns.Contains("STDRecSNF") Then
                strColName = "STDRecSNF"
            End If
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Rec. STD SNF KG"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("STD_Issued_FATPER") OrElse gv1.Columns.Contains("STDIssFATPER") Then
            If gv1.Columns.Contains("STD_Issued_FATPER") Then
                strColName = "STD_Issued_FATPER"
            ElseIf gv1.Columns.Contains("STDIssFATPER") Then
                strColName = "STDIssFATPER"
            End If
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Iss. STD FAT%"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("STD_Issued_FAT") OrElse gv1.Columns.Contains("STDIssFAT") Then
            If gv1.Columns.Contains("STD_Issued_FAT") Then
                strColName = "STD_Issued_FAT"
            ElseIf gv1.Columns.Contains("STDIssFAT") Then
                strColName = "STDIssFAT"
            End If
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Iss. STD FAT KG"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("STD_Issued_SNFPER") OrElse gv1.Columns.Contains("STDIssSNFPER") Then
            If gv1.Columns.Contains("STD_Issued_SNFPER") Then
                strColName = "STD_Issued_SNFPER"
            ElseIf gv1.Columns.Contains("STDIssSNFPER") Then
                strColName = "STDIssSNFPER"
            End If
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Iss. STD SNF%"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("STD_Issued_SNF") OrElse gv1.Columns.Contains("STDIssSNF") Then
            If gv1.Columns.Contains("STD_Issued_SNF") Then
                strColName = "STD_Issued_SNF"
            ElseIf gv1.Columns.Contains("STDIssSNF") Then
                strColName = "STDIssSNF"
            End If
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Iss. STD SNF KG"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("STD_Balance_FATPER") OrElse gv1.Columns.Contains("STDBalance_FATPER") Then
            If gv1.Columns.Contains("STD_Balance_FATPER") Then
                strColName = "STD_Balance_FATPER"
            ElseIf gv1.Columns.Contains("STDBalance_FATPER") Then
                strColName = "STDBalance_FATPER"
            End If
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Bal. STD FAT%"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("STD_Balance_FAT") OrElse gv1.Columns.Contains("STDBalance_FAT") Then
            If gv1.Columns.Contains("STD_Balance_FAT") Then
                strColName = "STD_Balance_FAT"
            ElseIf gv1.Columns.Contains("STDBalance_FAT") Then
                strColName = "STDBalance_FAT"
            End If
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Bal. STD FAT KG"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("STD_Balance_SNFPER") OrElse gv1.Columns.Contains("STDBalance_SNFPER") Then
            If gv1.Columns.Contains("STD_Balance_SNFPER") Then
                strColName = "STD_Balance_SNFPER"
            ElseIf gv1.Columns.Contains("STDBalance_SNFPER") Then
                strColName = "STDBalance_SNFPER"
            End If
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Bal. STD SNF%"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("STD_Balance_SNF") OrElse gv1.Columns.Contains("STDBalance_SNF") Then
            If gv1.Columns.Contains("STD_Balance_SNF") Then
                strColName = "STD_Balance_SNF"
            ElseIf gv1.Columns.Contains("STDBalance_SNF") Then
                strColName = "STDBalance_SNF"
            End If
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Bal. STD SNF KG"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("STDCLFATPER") Then
            strColName = "STDCLFATPER"
            gv1.Columns("STDCLFATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("STDCLFATPER").Width = 100
            gv1.Columns("STDCLFATPER").HeaderText = "Closing STD FAT%"
            gv1.Columns("STDCLFATPER").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("STDCLFAT") Then
            strColName = "STDCLFAT"
            gv1.Columns("STDCLFAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("STDCLFAT").Width = 100
            gv1.Columns("STDCLFAT").HeaderText = "Closing STD FAT KG"
            gv1.Columns("STDCLFAT").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("STDCLSNFPER") Then
            strColName = "STDCLSNFPER"
            gv1.Columns("STDCLSNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("STDCLSNFPER").Width = 100
            gv1.Columns("STDCLSNFPER").HeaderText = "Closing STD SNF%"
            gv1.Columns("STDCLSNFPER").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("STDCLSNF") Then
            strColName = "STDCLSNF"
            gv1.Columns("STDCLSNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("STDCLSNF").Width = 100
            gv1.Columns("STDCLSNF").HeaderText = "Closing STD SNF KG"
            gv1.Columns("STDCLSNF").FormatString = "{0:n2}"
        End If
        ''=======================
        If gv1.Columns.Contains("STDRecPurFATPER") Then
            strColName = "STDRecPurFATPER"
            gv1.Columns("STDRecPurFATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("STDRecPurFATPER").Width = 100
            gv1.Columns("STDRecPurFATPER").HeaderText = "Rec. STD Purchase FAT%"
            gv1.Columns("STDRecPurFATPER").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("STDRecPurFAT") Then
            strColName = "STDRecPurFAT"
            gv1.Columns("STDRecPurFAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("STDRecPurFAT").Width = 100
            gv1.Columns("STDRecPurFAT").HeaderText = "Rec. STD Purchase FAT KG"
            gv1.Columns("STDRecPurFAT").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("STDRecPurSNFPER") Then
            strColName = "STDRecPurSNFPER"
            gv1.Columns("STDRecPurSNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("STDRecPurSNFPER").Width = 100
            gv1.Columns("STDRecPurSNFPER").HeaderText = "Rec. STD Purchase SNF%"
            gv1.Columns("STDRecPurSNFPER").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("STDRecPurSNF") Then
            strColName = "STDRecPurSNF"
            gv1.Columns("STDRecPurSNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("STDRecPurSNF").Width = 100
            gv1.Columns("STDRecPurSNF").HeaderText = "Rec. STD Purchase SNF KG"
            gv1.Columns("STDRecPurSNF").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("STDRecAdjProFATPER") Then
            strColName = "STDRecAdjProFATPER"
            gv1.Columns("STDRecAdjProFATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("STDRecAdjProFATPER").Width = 100
            gv1.Columns("STDRecAdjProFATPER").HeaderText = "Rec. STD Adjustment/Production FAT%"
            gv1.Columns("STDRecAdjProFATPER").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("STDRecAdjProFAT") Then
            strColName = "STDRecAdjProFAT"
            gv1.Columns("STDRecAdjProFAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("STDRecAdjProFAT").Width = 100
            gv1.Columns("STDRecAdjProFAT").HeaderText = "Rec. STD Adjustment/Production FAT KG"
            gv1.Columns("STDRecAdjProFAT").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("STDRecAdjProSNFPER") Then
            strColName = "STDRecAdjProSNFPER"
            gv1.Columns("STDRecAdjProSNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("STDRecAdjProSNFPER").Width = 100
            gv1.Columns("STDRecAdjProSNFPER").HeaderText = "Rec. STD Adjustment/Production SNF%"
            gv1.Columns("STDRecAdjProSNFPER").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("STDRecAdjProSNF") Then
            strColName = "STDRecAdjProSNF"
            gv1.Columns("STDRecAdjProSNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("STDRecAdjProSNF").Width = 100
            gv1.Columns("STDRecAdjProSNF").HeaderText = "Rec. STD Adjustment/Production SNF KG"
            gv1.Columns("STDRecAdjProSNF").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("STDRecOthFATPER") Then
            strColName = "STDRecOthFATPER"
            gv1.Columns("STDRecOthFATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("STDRecOthFATPER").Width = 100
            gv1.Columns("STDRecOthFATPER").HeaderText = "Rec. STD Other FAT%"
            gv1.Columns("STDRecOthFATPER").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("STDRecOthFAT") Then
            strColName = "STDRecOthFAT"
            gv1.Columns("STDRecOthFAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("STDRecOthFAT").Width = 100
            gv1.Columns("STDRecOthFAT").HeaderText = "Rec. STD Other FAT KG"
            gv1.Columns("STDRecOthFAT").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("STDRecOthSNFPER") Then
            strColName = "STDRecOthSNFPER"
            gv1.Columns("STDRecOthSNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("STDRecOthSNFPER").Width = 100
            gv1.Columns("STDRecOthSNFPER").HeaderText = "Rec. STD Other SNF%"
            gv1.Columns("STDRecOthSNFPER").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("STDRecOthSNF") Then
            strColName = "STDRecOthSNF"
            gv1.Columns("STDRecOthSNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("STDRecOthSNF").Width = 100
            gv1.Columns("STDRecOthSNF").HeaderText = "Rec. STD Other SNF KG"
            gv1.Columns("STDRecOthSNF").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("STDRecFATPER") Then
            strColName = "STDRecFATPER"
            gv1.Columns("STDRecFATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("STDRecFATPER").Width = 100
            gv1.Columns("STDRecFATPER").HeaderText = "Rec. STD FAT%"
            gv1.Columns("STDRecFATPER").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("STDRecFAT") Then
            strColName = "STDRecFAT"
            gv1.Columns("STDRecFAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("STDRecFAT").Width = 100
            gv1.Columns("STDRecFAT").HeaderText = "Rec. STD FAT KG"
            gv1.Columns("STDRecFAT").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("STDRecSNFPER") Then
            strColName = "STDRecSNFPER"
            gv1.Columns("STDRecSNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("STDRecSNFPER").Width = 100
            gv1.Columns("STDRecSNFPER").HeaderText = "Rec. STD SNF%"
            gv1.Columns("STDRecSNFPER").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("STDRecSNF") Then
            strColName = "STDRecSNF"
            gv1.Columns("STDRecSNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("STDRecSNF").Width = 100
            gv1.Columns("STDRecSNF").HeaderText = "Rec. STD SNF KG"
            gv1.Columns("STDRecSNF").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("STDIssSaleFATPER") Then
            strColName = "STDIssSaleFATPER"
            gv1.Columns("STDIssSaleFATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("STDIssSaleFATPER").Width = 100
            gv1.Columns("STDIssSaleFATPER").HeaderText = "Iss. STD Sale FAT%"
            gv1.Columns("STDIssSaleFATPER").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("STDIssSaleFAT") Then
            strColName = "STDIssSaleFAT"
            gv1.Columns("STDIssSaleFAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("STDIssSaleFAT").Width = 100
            gv1.Columns("STDIssSaleFAT").HeaderText = "Iss. STD Sale FAT KG"
            gv1.Columns("STDIssSaleFAT").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("STDIssSaleSNFPER") Then
            strColName = "STDIssSaleSNFPER"
            gv1.Columns("STDIssSaleSNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("STDIssSaleSNFPER").Width = 100
            gv1.Columns("STDIssSaleSNFPER").HeaderText = "Iss. STD Sale SNF%"
            gv1.Columns("STDIssSaleSNFPER").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("STDIssSaleSNF") Then
            strColName = "STDIssSaleSNF"
            gv1.Columns("STDIssSaleSNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("STDIssSaleSNF").Width = 100
            gv1.Columns("STDIssSaleSNF").HeaderText = "Iss. STD Sale SNF KG"
            gv1.Columns("STDIssSaleSNF").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("STDIssIssAdjFATPER") Then
            strColName = "STDIssIssAdjFATPER"
            gv1.Columns("STDIssIssAdjFATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("STDIssIssAdjFATPER").Width = 100
            gv1.Columns("STDIssIssAdjFATPER").HeaderText = "Iss./Adjustment STD FAT%"
            gv1.Columns("STDIssIssAdjFATPER").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("STDIssIssAdjFAT") Then
            strColName = "STDIssIssAdjFAT"
            gv1.Columns("STDIssIssAdjFAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("STDIssIssAdjFAT").Width = 100
            gv1.Columns("STDIssIssAdjFAT").HeaderText = "Iss./Adjustment STD FAT KG"
            gv1.Columns("STDIssIssAdjFAT").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("STDIssIssAdjSNFPER") Then
            strColName = "STDIssIssAdjSNFPER"
            gv1.Columns("STDIssIssAdjSNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("STDIssIssAdjSNFPER").Width = 100
            gv1.Columns("STDIssIssAdjSNFPER").HeaderText = "Iss./Adjustment STD SNF%"
            gv1.Columns("STDIssIssAdjSNFPER").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("STDIssIssAdjSNF") Then
            strColName = "STDIssIssAdjSNF"
            gv1.Columns("STDIssIssAdjSNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("STDIssIssAdjSNF").Width = 100
            gv1.Columns("STDIssIssAdjSNF").HeaderText = "Iss./Adjustment STD SNF KG"
            gv1.Columns("STDIssIssAdjSNF").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("STDIssOthFATPER") Then
            strColName = "STDIssOthFATPER"
            gv1.Columns("STDIssOthFATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("STDIssOthFATPER").Width = 100
            gv1.Columns("STDIssOthFATPER").HeaderText = "Iss. STD Other FAT%"
            gv1.Columns("STDIssOthFATPER").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("STDIssOthFAT") Then
            strColName = "STDIssOthFAT"
            gv1.Columns("STDIssOthFAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("STDIssOthFAT").Width = 100
            gv1.Columns("STDIssOthFAT").HeaderText = "Iss. STD Other FAT KG"
            gv1.Columns("STDIssOthFAT").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("STDIssOthSNFPER") Then
            strColName = "STDIssOthSNFPER"
            gv1.Columns("STDIssOthSNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("STDIssOthSNFPER").Width = 100
            gv1.Columns("STDIssOthSNFPER").HeaderText = "Iss. STD Other SNF%"
            gv1.Columns("STDIssOthSNFPER").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("STDIssOthSNF") Then
            strColName = "STDIssOthSNF"
            gv1.Columns("STDIssOthSNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("STDIssOthSNF").Width = 100
            gv1.Columns("STDIssOthSNF").HeaderText = "Iss. STD Other SNF KG"
            gv1.Columns("STDIssOthSNF").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("STDIssFATPER") Then
            strColName = "STDIssFATPER"
            gv1.Columns("STDIssFATPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("STDIssFATPER").Width = 100
            gv1.Columns("STDIssFATPER").HeaderText = "Issue STD FAT%"
            gv1.Columns("STDIssFATPER").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("STDIssFAT") Then
            strColName = "STDIssFAT"
            gv1.Columns("STDIssFAT").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("STDIssFAT").Width = 100
            gv1.Columns("STDIssFAT").HeaderText = "Issue STD FAT KG"
            gv1.Columns("STDIssFAT").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("STDIssSNFPER") Then
            strColName = "STDIssSNFPER"
            gv1.Columns("STDIssSNFPER").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("STDIssSNFPER").Width = 100
            gv1.Columns("STDIssSNFPER").HeaderText = "Issue STD SNF%"
            gv1.Columns("STDIssSNFPER").FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("STDIssSNF") Then
            strColName = "STDIssSNF"
            gv1.Columns("STDIssSNF").IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns("STDIssSNF").Width = 100
            gv1.Columns("STDIssSNF").HeaderText = "Issue STD SNF KG"
            gv1.Columns("STDIssSNF").FormatString = "{0:n2}"
        End If

        If gv1.Columns.Contains("Op Diff. STD Fat%") Then
            strColName = "Op Diff. STD Fat%"
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Op Diff. STD Fat%"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If

        If gv1.Columns.Contains("Op Diff. STD FAT KG") Then
            strColName = "Op Diff. STD FAT KG"
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Op Diff. STD FAT KG"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If

        If gv1.Columns.Contains("Op Diff. STD SNF%") Then
            strColName = "Op Diff. STD SNF%"
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Op Diff. STD SNF%"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If

        If gv1.Columns.Contains("Op Diff. STD SNF KG") Then
            strColName = "Op Diff. STD SNF KG"
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Op Diff. STD SNF KG"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If
        If gv1.Columns.Contains("Diff. STD Rec. FAT%") Then
            strColName = "Diff. STD Rec. FAT%"
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Diff. STD Rec. FAT%"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If

        If gv1.Columns.Contains("Diff. STD Rec. FAT KG") Then
            strColName = "Diff. STD Rec. FAT KG"
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Diff. STD Rec. FAT KG"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If

        If gv1.Columns.Contains("Diff. STD Rec. SNF%") Then
            strColName = "Diff. STD Rec. SNF%"
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Diff. STD Rec. SNF%"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If

        If gv1.Columns.Contains("Diff. STD Rec. SNF KG") Then
            strColName = "Diff. STD Rec. SNF KG"
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Diff. STD Rec. SNF KG"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If

        If gv1.Columns.Contains("Diff. STD Iss. FAT%") Then
            strColName = "Diff. STD Iss. FAT%"
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Diff. STD Iss. FAT%"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If

        If gv1.Columns.Contains("Diff. STD Iss. FAT KG") Then
            strColName = "Diff. STD Iss. FAT KG"
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Diff. STD Iss. FAT KG"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If

        If gv1.Columns.Contains("Diff. STD Iss. SNF%") Then
            strColName = "Diff. STD Iss. SNF%"
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Diff. STD Iss. SNF%"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If

        If gv1.Columns.Contains("Diff. STD Iss. SNF KG") Then
            strColName = "Diff. STD Iss. SNF KG"
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Diff. STD Iss. SNF KG"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If

        If gv1.Columns.Contains("Diff. STD Bal. FAT%") Then
            strColName = "Diff. STD Bal. FAT%"
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Diff. STD Bal. FAT%"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If

        If gv1.Columns.Contains("Diff. STD Bal. FAT KG") Then
            strColName = "Diff. STD Bal. FAT KG"
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Diff. STD Bal. FAT KG"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If

        If gv1.Columns.Contains("Diff. STD Bal. SNF%") Then
            strColName = "Diff. STD Bal. SNF%"
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Diff. STD Bal. SNF%"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If

        If gv1.Columns.Contains("Diff. STD Bal. SNF KG") Then
            strColName = "Diff. STD Bal. SNF KG"
            gv1.Columns(strColName).IsVisible = (clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal)
            gv1.Columns(strColName).Width = 100
            gv1.Columns(strColName).HeaderText = "Diff. STD Bal. SNF KG"
            gv1.Columns(strColName).FormatString = "{0:n2}"
        End If
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
            common.clsCommon.MyMessageBoxShow(err.Message)
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
            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Stock Reco (" + cboType.Text + ")", gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Stock Reco (" + cboType.Text + ")", gv1, arrHeader, Me.Text, True)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Private Sub RadButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton3.Click
        EnableDisableCtrl(True)
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        gv1.GroupDescriptors.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        arrBack = New List(Of String)
        RadPageViewPage2.Text = "Report"
        vsb.Visible = False
    End Sub

    Sub EnableDisableCtrl(ByVal val As Boolean)
        txtFromDate.Enabled = val
        txtToDate.Enabled = val
        RadGroupBox3.Enabled = val
        txtItem.Enabled = val
        RadGroupBox2.Enabled = val
        cmbUnit.Enabled = val
        cboType.Enabled = val
        ChkMRPWise.Enabled = val
        cboFATSNF.Enabled = val
        txtTransaction.Enabled = val
        cboInOutType.Enabled = val
        txtItemGroup.Enabled = val
        chkIncludeGIT.Enabled = val
        chkExcludeConsumptionLoc.Enabled = val
        txtItemType.Enabled = val
        chkProd_WIP.Enabled = val
        cboFatSNFType.Enabled = val
        chkPartiallyLoad.Enabled = False
        If val = True AndAlso (clsCommon.CompairString(cboType.SelectedValue, "Date, Item And Document Wise Detail") = CompairStringResult.Equal OrElse clsCommon.CompairString(cboType.SelectedValue, "Transaction Wise") = CompairStringResult.Equal) Then
            chkPartiallyLoad.Enabled = True
        End If
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
                ReportID = "stReNity" + clsCommon.myCstr(cboFATSNF.SelectedValue) + IIf(ChkMRPWise.Checked, "M", "")
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Category Wise Summary") = CompairStringResult.Equal Then
                ReportID = "stReNCatg" + clsCommon.myCstr(cboFATSNF.SelectedValue) + IIf(ChkMRPWise.Checked, "M", "")
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item Wise Summary") = CompairStringResult.Equal Then
                ReportID = "stReNItem" + clsCommon.myCstr(cboFATSNF.SelectedValue) + IIf(ChkMRPWise.Checked, "M", "")
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item And Location Wise Summary") = CompairStringResult.Equal Then
                ReportID = "stReNLOCItem" + clsCommon.myCstr(cboFATSNF.SelectedValue) + IIf(ChkMRPWise.Checked, "M", "")
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Document Wise Detail") = CompairStringResult.Equal Then
                ReportID = "stReNDoc" + clsCommon.myCstr(cboFATSNF.SelectedValue) + IIf(ChkMRPWise.Checked, "M", "")

            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item Group Wise Summary") = CompairStringResult.Equal Then
                ReportID = "stReIGWS" + clsCommon.myCstr(cboFATSNF.SelectedValue) + IIf(ChkMRPWise.Checked, "M", "")
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Date, Item And Document Wise Detail") = CompairStringResult.Equal Then
                ReportID = "stReNDIDW" + clsCommon.myCstr(cboFATSNF.SelectedValue) + IIf(ChkMRPWise.Checked, "M", "")
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Date and Item Wise Stock") = CompairStringResult.Equal Then
                ReportID = "stReNDIWS" + clsCommon.myCstr(cboFATSNF.SelectedValue) + IIf(ChkMRPWise.Checked, "M", "")
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Document Wise Detail Ledger") = CompairStringResult.Equal Then
                ReportID = "stReNDWDL" + clsCommon.myCstr(cboFATSNF.SelectedValue) + IIf(ChkMRPWise.Checked, "M", "")
            End If
        End If
        Return ReportID
    End Function

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        clsGridLayout.DeleteData(GetReportID(), objCommonVar.CurrentUserCode)
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
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
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
            'frm.lvl = If(Form_ID = clsUserMgtCode.stockRecoNewJR, 4, IIf(chkExcludeConsumptionLoc.Checked, 5, 3))
            frm.lvl = If(Form_ID = clsUserMgtCode.stockRecoNewJR, 4, 3)
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
            qry = " select Item_Code,Item_Desc from TSPL_ITEM_MASTER  order by Item_Code "
        Else
            qry = " select Item_Code,Item_Desc from TSPL_ITEM_MASTER where Item_Type in (" + clsCommon.GetMulcallString(txtItemType.arrValueMember) + ") order by Item_Code "

        End If

        txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Item_Code", "Item_Desc", txtItem.arrValueMember, txtItem.arrDispalyMember)

    End Sub

    Private Sub txtTransaction__My_Click(sender As Object, e As EventArgs) Handles txtTransaction._My_Click
        Dim qry As String = " select Code,Name,InOutType as [In/Out Type],Type from TSPL_INVENTORY_SOURCE_CODE where 2=2 "
        If Not clsCommon.CompairString(clsCommon.myCstr(cboInOutType.SelectedValue), "All") = CompairStringResult.Equal Then
            qry += " and InOutType='" + clsCommon.myCstr(cboInOutType.SelectedValue) + "'"
        End If
        ''=======================do work for WIP===24/03/2017=======================================================
        If chkProd_WIP.Checked Then
            qry += " and code in ('" + clsUserMgtCode.frmProcessProductionIssueEntry + "','" + clsUserMgtCode.frmProcessProductionStandardization + "','" + clsUserMgtCode.frmProcessProductionStageProcess + "','" + clsUserMgtCode.frmProductionEntry + "','" + clsUserMgtCode.frmWreckageBooking + "','Prod-Scrap','PP-PR') "
        End If
        ''================================================================================================

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
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item Wise Summary") = CompairStringResult.Equal AndAlso arrBack.Contains("Category Wise Summary") Then
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
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item And Location Wise Summary") = CompairStringResult.Equal AndAlso arrBack.Contains("Item Wise Summary") Then
                arrBack.Remove("Item Wise Summary")
                cboType.SelectedValue = "Item Wise Summary"
                txtItem.arrValueMember = arrItem
                LoadData(0)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Document Wise Detail") = CompairStringResult.Equal AndAlso arrBack.Contains("Item And Location Wise Summary") Then
                arrBack.Remove("Item And Location Wise Summary")
                cboType.SelectedValue = "Item And Location Wise Summary"
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
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
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
                        Case "DispatchBSTrade"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmDispatchBulkSaleTrade, strTransCode)
                        Case "DispatchBSTrdReturn"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmDispatchBulkSaleTradeReturn, strTransCode)
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
                        Case "MCC-IISSUE"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmVSPItemIssue, strTransCode)
                        Case "FS-SH"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmDispatchMultipleFreshSale, strTransCode)
                        Case "PS-SH"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmShipmentProductSale, strTransCode)
                        Case "Transfer"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.Transfer, strTransCode)
                        Case "ITransfer"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.Transfer, strTransCode)
                        Case "BulkSRN"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmBulkMilkSRN, strTransCode)
                        Case "MCC-MSRN"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkSRN, strTransCode)
                        Case "SD-CSATRANS-RETURN"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSATransferReturn, strTransCode)
                        Case "JWO-SRN"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.JWO_SRN, strTransCode)
                        Case "JWO-SRN-RET"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.JWO_SRN_Return, strTransCode)
                        Case "MilkTransferInReturn"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkTransferInReturn, strTransCode)
                        Case "JW-TO"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkJobWorkTransferOther, strTransCode)
                        Case "JWO-Transfer-RET"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkJobWorkTransferOtherReturn, strTransCode)
                        Case "MilkTransferJobWork"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkJobWorkTransfer, strTransCode)
                        Case "MilkTransJWOReturn"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkJobWorkTransferReturn, strTransCode)
                        Case "RGPR"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnGatePass, strTransCode)
                        Case "BulkSRNRet"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmBulkMilkSRNReturn, strTransCode)
                        Case "BulkSRNTrade"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmDispatchBulkSaleTradeReturn, strTransCode)
                        Case "Purchase Return"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseReturn, strTransCode)
                        Case "SRN-RET"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.SRNReturn, strTransCode)
                    End Select
                Else
                    If (strTransCode.Contains("Opening") OrElse clsCommon.myLen(strTransCode) <= 0) Then
                        If (clsCommon.CompairString(cboDisplayMethod.SelectedValue, "None") = CompairStringResult.Equal OrElse clsCommon.CompairString(cboDisplayMethod.SelectedValue, "AD") = CompairStringResult.Equal) Then
                            Exit Sub
                        End If
                        Dim Punching_Date As String = ""
                        If gv1.CurrentRow.Cells("Punching_Date").Value IsNot Nothing AndAlso IsDate(gv1.CurrentRow.Cells("Punching_Date").Value) Then
                            Punching_Date = clsCommon.GetPrintDate(gv1.CurrentRow.Cells("Punching_Date").Value, "dd-MMM-yyyy")
                        End If
                        Dim from_Date As Date = Nothing
                        Dim To_Date As Date = Nothing
                        If clsCommon.myLen(Punching_Date) >= 10 Then
                            from_Date = Punching_Date
                            To_Date = from_Date
                        ElseIf clsCommon.myLen(Punching_Date) >= 8 Then
                            from_Date = "01" & "-" & Punching_Date
                            To_Date = clsCommon.myCDate(from_Date).AddMonths(1).AddDays(-1)
                        ElseIf clsCommon.myLen(Punching_Date) >= 6 Then
                            Dim arrSp() As String = Punching_Date.Split("-")
                            If arrSp.Length > 0 Then
                                Dim quarter As Integer = arrSp(0)
                                Dim yer As Integer = arrSp(1)
                                If quarter = 1 Then
                                    from_Date = "01" & "-" & "APR" & "-" & yer
                                    To_Date = clsCommon.myCDate(from_Date).AddMonths(3).AddDays(-1)
                                ElseIf quarter = 2 Then
                                    from_Date = "01" & "-" & "JUL" & "-" & yer
                                    To_Date = clsCommon.myCDate(from_Date).AddMonths(3).AddDays(-1)
                                ElseIf quarter = 3 Then
                                    from_Date = "01" & "-" & "OCT" & "-" & yer
                                    To_Date = clsCommon.myCDate(from_Date).AddMonths(3).AddDays(-1)
                                ElseIf quarter = 4 Then
                                    from_Date = "01" & "-" & "JAN" & "-" & yer
                                    To_Date = clsCommon.myCDate(from_Date).AddMonths(3).AddDays(-1)
                                End If
                            End If
                        ElseIf clsCommon.myLen(Punching_Date) >= 4 Then
                            Dim yer As Integer = clsCommon.myCdbl(Punching_Date)
                        End If
                        Dim Location As String = clsCommon.myCstr(gv1.CurrentRow.Cells("Location_Code").Value)
                        Dim Item_Code As String = clsCommon.myCstr(gv1.CurrentRow.Cells("Item_Code").Value)
                        Dim arr As New ArrayList
                        arr.Add(Location)
                        Dim qry As String = ""
                        If Not (from_Date = Nothing AndAlso To_Date = Nothing) Then
                            qry = clsInventoryMovement.GetStockDetailQry(arr, from_Date, To_Date, Item_Code)
                            If clsCommon.myLen(qry) > 0 Then
                                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                                gvDetail.DataSource = Nothing
                                gvDetail.DataSource = dt
                                gvDetail.BestFitColumns()
                                gvDetail.ReadOnly = True
                                RadPageView1.SelectedPage = RadPageView1.Pages(2)
                            End If
                        End If
                    End If
                End If
            End If
            PageSetupReport_ID = GetReportID()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub chkIncludeGIT_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkIncludeGIT.ToggleStateChanged
        LoadLocation()
    End Sub
    Private Sub chkExcludeConsumptionLoc_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkExcludeConsumptionLoc.ToggleStateChanged
        LoadLocation()
    End Sub
    Private Sub txtItemType__My_Click(sender As Object, e As EventArgs) Handles txtItemType._My_Click
        txtItemType.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemTypestoreco", FrmItemMasterRMOther.LoadItemTypeQuery(), "Code", "Name", txtItemType.arrValueMember, txtItemType.arrDispalyMember)
    End Sub

    Private Sub btnQuickExport_Click(sender As Object, e As EventArgs)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.MISStockReco & "'"))
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
            transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        '=====================Added by Preeti Gupta====
        Try
            LoadData(1)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton8_Click(sender As Object, e As EventArgs)
        Try
            LoadData(2)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Shared Sub ExportBulkData(ByVal qry As String, ByVal arrVisibleColumAndCaption As Dictionary(Of String, String), ByVal strReportNameInSaveDialog As String)
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

    Private Sub cboType_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboType.SelectedValueChanged
        If Not isInsideLoadData Then
            chkNoTransaction.Enabled = False
            If clsCommon.CompairString(cboType.SelectedValue, "Date, Item And Document Wise Detail") = CompairStringResult.Equal OrElse clsCommon.CompairString(cboType.SelectedValue, "Transaction Wise") = CompairStringResult.Equal Then
                chkPartiallyLoad.Enabled = True
                If cboDisplayMethod.Enabled = True Then
                    cboDisplayMethod.SelectedValue = "AD"
                End If
            ElseIf clsCommon.CompairString(cboType.SelectedValue, "Date and Item Wise Stock") = CompairStringResult.Equal Then
                chkNoTransaction.Enabled = True
                If cboDisplayMethod.Enabled = True Then
                    cboDisplayMethod.SelectedValue = "None"
                End If
                chkPartiallyLoad.Enabled = False
            Else
                ''richa BHA/10/09/18-000529 11 Sep,2018
                If cboDisplayMethod.Enabled = True Then
                    cboDisplayMethod.SelectedValue = "None"
                End If
                chkPartiallyLoad.Enabled = False
            End If
        End If
    End Sub

    Private Sub LoadDataInGridViaDataReader(ByVal qry As String)
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

    Private Sub chkProd_WIP_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkProd_WIP.ToggleStateChanged
        If chkProd_WIP.Checked Then
            txtTransaction.arrValueMember = Nothing
            chkIncludeGIT.Checked = False
        End If
        LoadLocation()
        chkIncludeGIT.Enabled = Not chkProd_WIP.Checked
    End Sub

    Private Sub QExpExcel_Click(sender As Object, e As EventArgs) Handles QExpExcel.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.MISStockReco & "'"))
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
            transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
            transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub QExpCSV_Click(sender As Object, e As EventArgs) Handles QExpCSV.Click
        Try
            If gv1 Is Nothing OrElse gv1.RowCount <= 0 Then
                MsgBox("Grid is empty!!!")
                Exit Sub
            End If
            ExportCSV(gv1, True)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        
    End Sub

    Public Sub ExportCSV(ByVal sender As RadGridView, Optional ByVal AddHeader As Boolean = False)
        Try
            Dim sfd As SaveFileDialog = New SaveFileDialog()
            Dim filePath As String
            sfd.FileName = Me.Text
            sfd.Filter = "CSV Files (*.csv) |*.csv"
            If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                filePath = sfd.FileName
            Else
                Exit Sub
            End If
            Dim OpenInExcel As Boolean = True
            If gv1.Rows.Count * gv1.Columns.Count > 22000000 Then
                OpenInExcel = False
            Else
                OpenInExcel = True
            End If
            clsCommon.ProgressBarShow()
            IO.File.WriteAllLines(filePath, transportSql.ExportCSV(sender, AddHeader))
            clsCommon.ProgressBarHide()
            If OpenInExcel Then
                clsCommon.MyMessageBoxShow("Data Exported successfully")
                Process.Start(filePath)
            Else
                clsCommon.MyMessageBoxShow("Data Exported successfully but can not open through excel, use other utility to open the file.")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
       
    End Sub

    Private Sub PDF_Click(sender As Object, e As EventArgs) Handles PDF.Click
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
        arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
        arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)


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
            transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
            clsCommon.MyExportToPDF("Stock Reco (" + cboType.Text + ")", gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub BulkCSV_Click(sender As Object, e As EventArgs) Handles BulkCSV.Click
        LoadData(4)
    End Sub

    Private Sub BulkExcel_Click(sender As Object, e As EventArgs) Handles BulkExcel.Click

        If cboDisplayMethod.SelectedValue = "None" Then
            LoadData(5)
        Else
            LoadDataNew(5)
        End If
    End Sub

    Private Sub cboFATSNF_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboFATSNF.SelectedValueChanged
        If clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "FS") = CompairStringResult.Equal AndAlso cboFatSNFType.DataSource IsNot Nothing Then
            cboFatSNFType.SelectedValue = "M"
            cboFatSNFType.Enabled = True
        Else
            cboFatSNFType.SelectedValue = ""
            cboFatSNFType.Enabled = False
        End If
    End Sub

    Private Sub ExcelGrid_Click(sender As Object, e As EventArgs) Handles ExcelGrid.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & FORMTYPE & "'"))
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
        
            clsCommon.MyExportToExcelGrid("Stock Reco (" + cboType.Text + ")", gv1, arrHeader, Me.Text, True)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Private Sub PDFGrid_Click(sender As Object, e As EventArgs) Handles PDFGrid.Click
        Try
            Dim FilePath As String = "C:\\ERPTempFolder\\" + Me.Text + clsCommon.GetPrintDate(DateTime.Now, "yyyyMMddhhmmss") + ".pdf"
            Dim pdfExporter As New ExportToPDF(gv1)
            pdfExporter.Font = New System.Drawing.Font("Verdana", 6)
            pdfExporter.TableBorderThickness = 1
            pdfExporter.FitToPageWidth = True
            pdfExporter.ExportVisualSettings = True
            pdfExporter.ExportHierarchy = True
            pdfExporter.HiddenColumnOption = HiddenOption.DoNotExport
            pdfExporter.PageTitle = "Stock Reco (" + cboType.Text + ")"
            pdfExporter.RunExport(FilePath)
            System.Diagnostics.Process.Start(FilePath)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

End Class
