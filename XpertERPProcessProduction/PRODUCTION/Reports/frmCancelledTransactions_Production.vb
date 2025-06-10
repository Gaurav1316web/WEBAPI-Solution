''Created By--richa agarwal-KDI/15/06/18-000371----on- 18/06/2018---------------------
'Ticket No-ERO/17/07/19-000952 ,Sanjay ,Add TS% AND TSKG
Imports common
Imports System.Data.SqlClient



Public Class frmCancelledTransactions_Production
    Inherits FrmMainTranScreen
    Dim arrUser As New ArrayList()
    Dim ButtonToolTip As New ToolTip()
    Public ModuleName As String = ""
    Public Transaction As String = ""
    Public fromdate As DateTime
    Public Todate As DateTime
    Dim dr As DataRow
    Dim dt As DataTable
    Dim strNoOfRecord As String
    Dim qry As String
    Dim arrSelectedUser As New ArrayList()
    Dim arrLoc As String = Nothing
    Dim RecordCount As Integer = 0

    Private Sub FrmPendingAproval_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LOCATIONRIGTHS()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C for Closing The Window")
        LoadLocation()
        chkLocAll.CheckState = CheckState.Checked
        ChkUserAll.CheckState = CheckState.Checked
        LoadUsers()
        arrUser = FrmUserMaster.GetSubbordinateUsers(objCommonVar.CurrentUserCode)
        SetUserMgmtNew()
        LoadBlankGrid()
        LoadModuleType()
        dtpFromDate.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        LoadModuleProduction()
        If clsCommon.myLen(ModuleName) > 0 Then
            cboModule.SelectedValue = ModuleName
            cboTransaction.SelectedValue = Transaction
            dtpFromDate.Value = fromdate
            dtpToDate.Value = Todate
            ShowData()
        End If
    End Sub

    'Public Sub LoadModuleType()
    '    Dim dt As DataTable = New DataTable()
    '    dt.Columns.Add("Code", GetType(String))
    '    dt.Columns.Add("Name", GetType(String))

    '    dr = dt.NewRow()
    '    dr("Code") = "Production"
    '    dr("Name") = "Production"
    '    dt.Rows.Add(dr)

    '    cboModule.DataSource = dt
    '    cboModule.DisplayMember = "Name"
    '    cboModule.ValueMember = "Code"

    'End Sub
    Private Sub LOCATIONRIGTHS()
        Dim obj As New clsMCCCodes()
        Try
            obj = clsMCCCodes.GetData()

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                Dim check As Integer = 0
                check = clsDBFuncationality.getSingleValue("select count(*) from tspl_location_master where type='Plant' and location_code='" + obj.Default_LocCode + "'")
                If check > 0 Then

                End If
            End If
            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
        End Try
    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub
    Sub LoadModuleProduction()
        Try
            Dim Qry As String = "select TSPL_PROGRAM_MASTER.Program_Code as Code,case when len (isnull(TSPL_PROGRAM_MASTER.Re_Name,'')) > 0 then TSPL_PROGRAM_MASTER.Re_Name else  TSPL_PROGRAM_MASTER.Program_Name end  as Name 
from TSPL_PROGRAM_MASTER
left outer join (select Program_Code, Program_Name,Parent_Code,case when len (isnull(TSPL_PROGRAM_MASTER.Re_Name,'')) > 0 then TSPL_PROGRAM_MASTER.Re_Name else  TSPL_PROGRAM_MASTER.Program_Name end As Re_Name from TSPL_PROGRAM_MASTER where Type in ('SM')) as TBL_SMODULE on TBL_SMODULE.Program_Code = TSPL_PROGRAM_MASTER.Parent_Code
left outer join (select Program_Code, Program_Name,Parent_Code,case when len (isnull(TSPL_PROGRAM_MASTER.Re_Name,'')) > 0 then TSPL_PROGRAM_MASTER.Re_Name else  TSPL_PROGRAM_MASTER.Program_Name end As Re_Name from TSPL_PROGRAM_MASTER where Type in ('M')) as TBL_MODULE on TBL_MODULE.Program_Code = TBL_SMODULE.Parent_Code
Where TBL_MODULE.Program_Code in (select  distinct Module_Name from TSPL_MODULE_PERMISSION ) and  not TSPL_PROGRAM_MASTER.Type in ('M','SM') 
and TBL_SMODULE.Parent_Code In ('" + clsUserMgtCode.ModuleProductionDairy + "','" + clsCommon.myCstr(cboModule.SelectedValue) + "') 
and TBL_SMODULE.Program_Name in ('Transaction','MCC Transaction','Bulk Transaction') And TSPL_PROGRAM_MASTER.Program_Code In ('" + clsUserMgtCode.frmProductionPlanningDairy + "','" + clsUserMgtCode.frmBatchOrderDairy + "','" + clsUserMgtCode.frmProcessProductionIssueEntry + "','" + clsUserMgtCode.frmProcessProductionStandardization + "','" + clsUserMgtCode.ProcessProductionStandardizationFinalQC + "','" + clsUserMgtCode.frmWreckageBooking + "','" + clsUserMgtCode.frmProcessProductionStageProcess + "','" + clsUserMgtCode.frmProductionEntry + "','" + clsUserMgtCode.frmAssembDis + "','" + clsUserMgtCode.frmWreckageBooking + "','" + clsUserMgtCode.frmBillOfMaterialCosting + "','" + clsUserMgtCode.frmStanderdProductionEntry + "')
 "
            dt = clsDBFuncationality.GetDataTable(Qry)
            'dr = dt.NewRow()
            'dr("Code") = dt.Rows(0)("Code")
            'dr("Name") = dt.Rows(0)("Name")
            'dt.Rows.Add(dr)

            cboTransaction.DataSource = dt
            cboTransaction.DisplayMember = "Name"
            cboTransaction.ValueMember = "Code"
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Public Sub LoadModuleType()
        Dim Qry As String = "select Distinct TBL_MODULE.Program_Code As [Module Code],case when len (isnull(TBL_MODULE.Re_Name,'')) > 0 then TBL_MODULE.Re_Name else  TSPL_PROGRAM_MASTER.Program_Name end  as [Module Name] 
from TSPL_PROGRAM_MASTER
left outer join (select Program_Code, Program_Name,Parent_Code,case when len (isnull(TSPL_PROGRAM_MASTER.Re_Name,'')) > 0 then TSPL_PROGRAM_MASTER.Re_Name else  TSPL_PROGRAM_MASTER.Program_Name end As Re_Name from TSPL_PROGRAM_MASTER where Type in ('SM')) as TBL_SMODULE on TBL_SMODULE.Program_Code = TSPL_PROGRAM_MASTER.Parent_Code
left outer join (select Program_Code, Program_Name,Parent_Code,case when len (isnull(TSPL_PROGRAM_MASTER.Re_Name,'')) > 0 then TSPL_PROGRAM_MASTER.Re_Name else  TSPL_PROGRAM_MASTER.Program_Name end As Re_Name from TSPL_PROGRAM_MASTER where Type in ('M')) as TBL_MODULE on TBL_MODULE.Program_Code = TBL_SMODULE.Parent_Code
Where TBL_MODULE.Program_Code in (select  distinct Module_Name from TSPL_MODULE_PERMISSION ) and  not TSPL_PROGRAM_MASTER.Type in ('M','SM') 
and TBL_MODULE.Program_Code in ('" + clsUserMgtCode.ModuleProductionSTD + "') 
and TBL_SMODULE.Program_Name in ('Transaction','MCC Transaction','Bulk Transaction') 
 "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            'dr = dt.NewRow()
            'dr("Module Code") = dt.Rows(0)("Module Code")
            'dr("Module Name") = dt.Rows(0)("Module Name")
            'dt.Rows.Add(dr)
            cboModule.DataSource = dt
            cboModule.DisplayMember = "Module Name"
            cboModule.ValueMember = "Module Code"
        End If

    End Sub
    'Sub LoadModuleProduction()
    '    Dim dt1 As DataTable = New DataTable()
    '    dt1.Columns.Add("Code", GetType(String))
    '    dt1.Columns.Add("Name", GetType(String))


    '    dr = dt1.NewRow()
    '    dr("Code") = "Production Planning"
    '    dr("Name") = "Production Planning"
    '    dt1.Rows.Add(dr)

    '    dr = dt1.NewRow()
    '    dr("Code") = "Batch Order"
    '    dr("Name") = "Batch Order"
    '    dt1.Rows.Add(dr)

    '    dr = dt1.NewRow()
    '    dr("Code") = "Production Issue Entry"
    '    dr("Name") = "Production Issue Entry"
    '    dt1.Rows.Add(dr)

    '    dr = dt1.NewRow()
    '    dr("Code") = "Production Standardization"
    '    dr("Name") = "Production Standardization"
    '    dt1.Rows.Add(dr)

    '    dr = dt1.NewRow()
    '    dr("Code") = "Production Standardization Final QC"
    '    dr("Name") = "Production Standardization Final QC"
    '    dt1.Rows.Add(dr)

    '    dr = dt1.NewRow()
    '    dr("Code") = "Stage Process"
    '    dr("Name") = "Stage Process"
    '    dt1.Rows.Add(dr)

    '    dr = dt1.NewRow()
    '    dr("Code") = "Production Entry"
    '    dr("Name") = "Production Entry"
    '    dt1.Rows.Add(dr)

    '    dr = dt1.NewRow()
    '    dr("Code") = "Assembly Deassembly"
    '    dr("Name") = "Assembly Deassembly"
    '    dt1.Rows.Add(dr)

    '    dr = dt1.NewRow()
    '    dr("Code") = "Wreckage Booking"
    '    dr("Name") = "Wreckage Booking"
    '    dt1.Rows.Add(dr)

    '    cboTransaction.DataSource = dt1
    '    cboTransaction.DisplayMember = "Name"
    '    cboTransaction.ValueMember = "Code"
    'End Sub

    Private Sub cboModule_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboModule.SelectedIndexChanged
        LoadTrnsListOfSelectedModeule()
    End Sub

    Public Sub LoadTrnsListOfSelectedModeule()
        If clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Production") = CompairStringResult.Equal Then
            cboTransaction.DataSource = Nothing
            LoadModuleProduction()
        End If

    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.AllowAddNewRow = False

    End Sub
    Sub gv1Format()
        Me.gv1.MasterTemplate.Columns("Document Id").Width = 120      ''First Column
        Me.gv1.MasterTemplate.Columns("Document Date").Width = 150    ''Second Column
        Dim count As Integer = gv1.MasterTemplate.Columns.Count
        For i As Integer = 2 To count - 1
            Me.gv1.MasterTemplate.Columns(i).Width = 120
        Next i
        Me.gv1.MasterTemplate.Columns("Description").Width = 200    ''Last Column
        For j As Integer = 1 To count - 1
            Me.gv1.MasterTemplate.Columns(j).ReadOnly = True
        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub

#Region "Showing Details on GRID"
    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        If dtpFromDate.Value > dtpToDate.Value Then
            common.clsCommon.MyMessageBoxShow(Me, "'From date' Can't Be Greater Than 'To Date'")
        Else
            qry = Nothing
            ShowData()
        End If


    End Sub

    Sub ShowData()
        Try
            If cbgUser.CheckedValue.Count > 0 Then
                arrSelectedUser = cbgUser.CheckedValue
            Else
                arrSelectedUser = arrUser
            End If

            'If clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Production") = CompairStringResult.Equal Then
            '    gv1.DataSource = Nothing
            FillProduction()
            ' End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub FillProduction()
        If clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), clsUserMgtCode.frmProductionPlanningSTD) = CompairStringResult.Equal Then
            gv1.DataSource = Nothing
            qry = " Select TSPL_PP_PRODUCTION_PLAN_HEAD_CANCEL_DATA.Plan_Code as [Document Id],convert(varchar,TSPL_PP_PRODUCTION_PLAN_HEAD_CANCEL_DATA.Plan_Date ,103) as [Document Date]," &
            " TSPL_PP_PRODUCTION_PLAN_HEAD_CANCEL_DATA.Section_Code as [Section Code] ,TSPL_PP_PRODUCTION_PLAN_HEAD_CANCEL_DATA.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_PP_PRODUCTION_PLAN_HEAD_CANCEL_DATA.Created_By as [Created By],convert(varchar,TSPL_PP_PRODUCTION_PLAN_HEAD_CANCEL_DATA.Created_Date,103) as [Created Date],'' as Description,TSPL_PP_PRODUCTION_PLAN_HEAD_CANCEL_DATA.Cancel_By as [Cancelled By],convert(varchar,TSPL_PP_PRODUCTION_PLAN_HEAD_CANCEL_DATA.Cancel_On,103) as [Cancelled Date] from TSPL_PP_PRODUCTION_PLAN_HEAD_CANCEL_DATA " &
            " Left Outer Join TSPL_LOCATION_MASTER  on TSPL_PP_PRODUCTION_PLAN_HEAD_CANCEL_DATA.Location_Code  =TSPL_LOCATION_MASTER.Location_Code " &
            " WHERE  convert(date,TSPL_PP_PRODUCTION_PLAN_HEAD_CANCEL_DATA.Plan_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) " &
            " and convert(date,TSPL_PP_PRODUCTION_PLAN_HEAD_CANCEL_DATA.Plan_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) "

            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                qry += " and TSPL_PP_PRODUCTION_PLAN_HEAD_CANCEL_DATA.Location_Code  in   (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If
            If chkUserSelect.IsChecked AndAlso cbgUser.CheckedValue.Count > 0 Then
                qry += " and TSPL_PP_PRODUCTION_PLAN_HEAD_CANCEL_DATA.Created_By IN (" + clsCommon.GetMulcallString(cbgUser.CheckedValue) + ")"
            Else
                qry += " and TSPL_PP_PRODUCTION_PLAN_HEAD_CANCEL_DATA.Created_By IN (" + clsCommon.GetMulcallString(arrSelectedUser) + ")"
            End If
            qry += " ORDER BY TSPL_PP_PRODUCTION_PLAN_HEAD_CANCEL_DATA.Plan_Date,TSPL_PP_PRODUCTION_PLAN_HEAD_CANCEL_DATA.Plan_Code "
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), clsUserMgtCode.frmProcessProductionStandardization) = CompairStringResult.Equal Then
            gv1.DataSource = Nothing
            qry = " Select TSPL_PP_STANDARDIZATION_HEAD_CANCEL_DATA.Standardization_Code  as [Document Id],convert(varchar,TSPL_PP_STANDARDIZATION_HEAD_CANCEL_DATA.Standardization_Date ,103) as [Document Date]," &
                " TSPL_PP_STANDARDIZATION_HEAD_CANCEL_DATA.Child_Batch_Code as [Child Batch Code],TSPL_PP_STANDARDIZATION_HEAD_CANCEL_DATA.Main_Batch_Code as [Main Batch Code] ," &
                " TSPL_PP_STANDARDIZATION_HEAD_CANCEL_DATA.Loaction_Code  as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_PP_STANDARDIZATION_HEAD_CANCEL_DATA.Created_By as [Created By]," &
                " convert(varchar,TSPL_PP_STANDARDIZATION_HEAD_CANCEL_DATA.Created_Date,103) as [Created Date],'' as Description,TSPL_PP_STANDARDIZATION_HEAD_CANCEL_DATA.Cancel_By as [Cancelled By],convert(varchar,TSPL_PP_STANDARDIZATION_HEAD_CANCEL_DATA.Cancel_On,103) as [Cancelled Date] from TSPL_PP_STANDARDIZATION_HEAD_CANCEL_DATA " &
                " Left Outer Join TSPL_LOCATION_MASTER  on TSPL_PP_STANDARDIZATION_HEAD_CANCEL_DATA.Loaction_Code  =TSPL_LOCATION_MASTER.Location_Code " &
                  " WHERE  convert(date,TSPL_PP_STANDARDIZATION_HEAD_CANCEL_DATA.Standardization_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) " &
                  " and convert(date,TSPL_PP_STANDARDIZATION_HEAD_CANCEL_DATA.Standardization_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) "


            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                qry += " and TSPL_PP_STANDARDIZATION_HEAD_CANCEL_DATA.Loaction_Code  in   (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If

            If chkUserSelect.IsChecked AndAlso cbgUser.CheckedValue.Count > 0 Then

                qry += " and TSPL_PP_STANDARDIZATION_HEAD_CANCEL_DATA.Created_By IN (" + clsCommon.GetMulcallString(cbgUser.CheckedValue) + ")"
            Else
                qry += " and TSPL_PP_STANDARDIZATION_HEAD_CANCEL_DATA.Created_By IN (" + clsCommon.GetMulcallString(arrSelectedUser) + ")"
            End If

            qry += "  ORDER BY TSPL_PP_STANDARDIZATION_HEAD_CANCEL_DATA.Standardization_Date,TSPL_PP_STANDARDIZATION_HEAD_CANCEL_DATA.Standardization_Code "

        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), clsUserMgtCode.ProcessProductionStandardizationFinalQC) = CompairStringResult.Equal Then
            gv1.DataSource = Nothing
            qry = " Select TSPL_PP_STD_FINALQC_HEAD_CANCEL_DATA.QC_Code  as [Document Id],convert(varchar,TSPL_PP_STD_FINALQC_HEAD_CANCEL_DATA.QC_Date ,103) as [Document Date]," &
                " TSPL_PP_STD_FINALQC_HEAD_CANCEL_DATA.Child_Batch_Code as [Child Batch Code],TSPL_PP_STD_FINALQC_HEAD_CANCEL_DATA.Main_Batch_Code as [Main Batch Code] ," &
                " TSPL_PP_STD_FINALQC_HEAD_CANCEL_DATA.Loaction_Code  as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_PP_STD_FINALQC_HEAD_CANCEL_DATA.Created_By as [Created By]," &
                " convert(varchar,TSPL_PP_STD_FINALQC_HEAD_CANCEL_DATA.Created_Date,103) as [Created Date],'' as Description,TSPL_PP_STD_FINALQC_HEAD_CANCEL_DATA.Cancel_By as [Cancelled By],convert(varchar,TSPL_PP_STD_FINALQC_HEAD_CANCEL_DATA.Cancel_On,103) as [Cancelled Date] from TSPL_PP_STD_FINALQC_HEAD_CANCEL_DATA " &
                " Left Outer Join TSPL_LOCATION_MASTER  on TSPL_PP_STD_FINALQC_HEAD_CANCEL_DATA.Loaction_Code  =TSPL_LOCATION_MASTER.Location_Code " &
                  " WHERE  convert(date,TSPL_PP_STD_FINALQC_HEAD_CANCEL_DATA.QC_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) " &
                  " and convert(date,TSPL_PP_STD_FINALQC_HEAD_CANCEL_DATA.QC_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) "


            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                qry += " and TSPL_PP_STD_FINALQC_HEAD_CANCEL_DATA.Loaction_Code  in   (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If

            If chkUserSelect.IsChecked AndAlso cbgUser.CheckedValue.Count > 0 Then

                qry += " and TSPL_PP_STD_FINALQC_HEAD_CANCEL_DATA.Created_By IN (" + clsCommon.GetMulcallString(cbgUser.CheckedValue) + ")"
            Else
                qry += " and TSPL_PP_STD_FINALQC_HEAD_CANCEL_DATA.Created_By IN (" + clsCommon.GetMulcallString(arrSelectedUser) + ")"
            End If

            qry += "  ORDER BY TSPL_PP_STD_FINALQC_HEAD_CANCEL_DATA.QC_Date,TSPL_PP_STD_FINALQC_HEAD_CANCEL_DATA.QC_Code "

        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), clsUserMgtCode.frmProcessProductionStageProcess) = CompairStringResult.Equal Then
            gv1.DataSource = Nothing
            qry = " Select TSPL_PP_STAGE_PROCESS_HEAD_CANCEL_DATA.STAGE_PROCESS_CODE as[Document Id],convert(varchar,TSPL_PP_STAGE_PROCESS_HEAD_CANCEL_DATA.STAGE_PROCESS_DATE ,103) as [Document Date], " &
                " TSPL_PP_STAGE_PROCESS_HEAD_CANCEL_DATA.Section_Stage_Map_Code as [Section Stage Map Code],TSPL_PP_STAGE_PROCESS_HEAD_CANCEL_DATA.Main_Batch_Code as [Main Batch Code] , " &
                " TSPL_PP_STAGE_PROCESS_HEAD_CANCEL_DATA.Created_By as [Created By],convert(varchar,TSPL_PP_STAGE_PROCESS_HEAD_CANCEL_DATA.Created_Date,103) as [Created Date],'' as Description  " &
                " ,TSPL_PP_STAGE_PROCESS_HEAD_CANCEL_DATA.Cancel_By as [Cancelled By],convert(varchar,TSPL_PP_STAGE_PROCESS_HEAD_CANCEL_DATA.Cancel_On,103) as [Cancelled Date] from TSPL_PP_STAGE_PROCESS_HEAD_CANCEL_DATA " &
                  " WHERE  convert(date,TSPL_PP_STAGE_PROCESS_HEAD_CANCEL_DATA.STAGE_PROCESS_DATE ,103) >= convert(date,'" + dtpFromDate.Value + "',103) " &
                  " and convert(date,TSPL_PP_STAGE_PROCESS_HEAD_CANCEL_DATA.STAGE_PROCESS_DATE ,103) <= convert(date,'" + dtpToDate.Value + "',103) "

            If chkUserSelect.IsChecked AndAlso cbgUser.CheckedValue.Count > 0 Then

                qry += " and TSPL_PP_STAGE_PROCESS_HEAD_CANCEL_DATA.Created_By IN (" + clsCommon.GetMulcallString(cbgUser.CheckedValue) + ")"
            Else
                qry += " and TSPL_PP_STAGE_PROCESS_HEAD_CANCEL_DATA.Created_By IN (" + clsCommon.GetMulcallString(arrSelectedUser) + ")"
            End If

            qry += " ORDER BY TSPL_PP_STAGE_PROCESS_HEAD_CANCEL_DATA.STAGE_PROCESS_CODE,TSPL_PP_STAGE_PROCESS_HEAD_CANCEL_DATA.STAGE_PROCESS_DATE "
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), clsUserMgtCode.frmAssembDis) = CompairStringResult.Equal Then
            gv1.DataSource = Nothing
            qry = "  Select TSPL_PROD_ASSEMBLIES_Cancel_Data.CODE  as [Document Id],convert(varchar,TSPL_PROD_ASSEMBLIES_Cancel_Data.ASSEMBLY_DATE ,103) as [Document Date],  " &
            " TSPL_PROD_ASSEMBLIES_Cancel_Data.LOCATION_CODE  as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_PROD_ASSEMBLIES_Cancel_Data.TRANSACTION_TYPE  as [TRANSACTION TYPE],  " &
            " TSPL_PROD_ASSEMBLIES_Cancel_Data.QUANTITY ,TSPL_PROD_ASSEMBLIES_Cancel_Data.FAT_KG as [FAT KG],TSPL_PROD_ASSEMBLIES_Cancel_Data.snf_KG as[SNF KG],TSPL_PROD_ASSEMBLIES_Cancel_Data.FAT_KG+TSPL_PROD_ASSEMBLIES_Cancel_Data.snf_KG as [TS KG],TSPL_PROD_ASSEMBLIES_Cancel_Data.FAT_PER AS [FAT %],TSPL_PROD_ASSEMBLIES_Cancel_Data.snf_PER AS [SNF %],TSPL_PROD_ASSEMBLIES_Cancel_Data.FAT_PER+TSPL_PROD_ASSEMBLIES_Cancel_Data.snf_PER as [TS %], " &
            " TSPL_PROD_ASSEMBLIES_Cancel_Data.Created_By as [Created By],convert(varchar,TSPL_PROD_ASSEMBLIES_Cancel_Data.Created_Date,103) as [Created Date],'' as Description,TSPL_PROD_ASSEMBLIES_Cancel_Data.Cancel_By as [Cancelled By],convert(varchar,TSPL_PROD_ASSEMBLIES_Cancel_Data.Cancel_On,103) as [Cancelled Date] from TSPL_PROD_ASSEMBLIES_Cancel_Data " &
            " Left Outer Join TSPL_LOCATION_MASTER  on TSPL_PROD_ASSEMBLIES_Cancel_Data.LOCATION_CODE   =TSPL_LOCATION_MASTER.Location_Code " &
            " WHERE  convert(date,TSPL_PROD_ASSEMBLIES_Cancel_Data.ASSEMBLY_DATE ,103) >= convert(date,'" + dtpFromDate.Value + "',103) " &
            " and convert(date,TSPL_PROD_ASSEMBLIES_Cancel_Data.ASSEMBLY_DATE,103) <= convert(date,'" + dtpToDate.Value + "',103) "


            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                qry += " and TSPL_PROD_ASSEMBLIES_Cancel_Data.LOCATION_CODE  in   (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If

            If chkUserSelect.IsChecked AndAlso cbgUser.CheckedValue.Count > 0 Then
                qry += " and TSPL_PROD_ASSEMBLIES_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(cbgUser.CheckedValue) + ")"
            Else
                qry += " and TSPL_PROD_ASSEMBLIES_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(arrSelectedUser) + ")"
            End If
            qry += " ORDER BY TSPL_PROD_ASSEMBLIES_Cancel_Data.ASSEMBLY_DATE, TSPL_PROD_ASSEMBLIES_Cancel_Data.CODE "
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), clsUserMgtCode.frmWreckageBooking) = CompairStringResult.Equal Then
            gv1.DataSource = Nothing
            qry = " Select TSPL_WRECKAGE_ENTRY_Cancel_Data.WRECKAGE_ENTRY_CODE as[Document Id],convert(varchar,TSPL_WRECKAGE_ENTRY_Cancel_Data.PROD_DATE ,103) as [Document Date], " &
            " TSPL_WRECKAGE_ENTRY_Cancel_Data.LOCATION_CODE  as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_WRECKAGE_ENTRY_Cancel_Data.Section_Stage_Map_Code as [Section Stage Map Code], " &
            " TSPL_WRECKAGE_ENTRY_Cancel_Data.CONSM_SECTION_CODE  as [CONSM SECTION CODE] ,TSPL_WRECKAGE_ENTRY_Cancel_Data.Created_By as [Created By], " &
            " convert(varchar,TSPL_WRECKAGE_ENTRY_Cancel_Data.Created_Date,103) as [Created Date],'' as Description,TSPL_WRECKAGE_ENTRY_Cancel_Data.Cancel_By as [Cancelled By],convert(varchar,TSPL_WRECKAGE_ENTRY_Cancel_Data.Cancel_On,103) as [Cancelled Date] from TSPL_WRECKAGE_ENTRY_Cancel_Data " &
            " Left Outer Join TSPL_LOCATION_MASTER  on TSPL_WRECKAGE_ENTRY_Cancel_Data.LOCATION_CODE   =TSPL_LOCATION_MASTER.Location_Code " &
            " WHERE  convert(date,TSPL_WRECKAGE_ENTRY_Cancel_Data.PROD_DATE ,103) >= convert(date,'" + dtpFromDate.Value + "',103) " &
            " and convert(date,TSPL_WRECKAGE_ENTRY_Cancel_Data.PROD_DATE,103) <= convert(date,'" + dtpToDate.Value + "',103) "


            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                qry += " and TSPL_WRECKAGE_ENTRY_Cancel_Data.LOCATION_CODE    in   (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If

            If chkUserSelect.IsChecked AndAlso cbgUser.CheckedValue.Count > 0 Then

                qry += " and TSPL_WRECKAGE_ENTRY_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(cbgUser.CheckedValue) + ")"
            Else
                qry += " and TSPL_WRECKAGE_ENTRY_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(arrSelectedUser) + ")"
            End If

            qry += " ORDER BY TSPL_WRECKAGE_ENTRY_Cancel_Data.PROD_DATE, TSPL_WRECKAGE_ENTRY_Cancel_Data.WRECKAGE_ENTRY_CODE "

            ''''''''''''''''''''''''''''''''''''''
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), clsUserMgtCode.frmBatchOrderDairy) = CompairStringResult.Equal Then

            gv1.DataSource = Nothing
            qry = "Select TSPL_PP_BATCH_ORDER_HEAD_CANCEL_DATA.BATCH_CODE as[Document Id],convert(varchar,TSPL_PP_BATCH_ORDER_HEAD_CANCEL_DATA.BATCH_DATE ,103) as [Document Date] " &
                ",TSPL_PP_BATCH_ORDER_HEAD_CANCEL_DATA.STRUCTURE_CODE as [Structure Code],TSPL_PP_BATCH_ORDER_HEAD_CANCEL_DATA.Section_Code as [Section Code] " &
                ",TSPL_PP_BATCH_ORDER_HEAD_CANCEL_DATA.Plan_CODE as [Plan Code],TSPL_PP_BATCH_ORDER_HEAD_CANCEL_DATA.Main_Batch_Code as [Main Batch Code] " &
                ",TSPL_PP_BATCH_ORDER_HEAD_CANCEL_DATA.Sub_Batch_Code as [Sub Batch Code] , TSPL_PP_BATCH_ORDER_HEAD_CANCEL_DATA.Created_By as [Created By] " &
                ",convert(varchar,TSPL_PP_BATCH_ORDER_HEAD_CANCEL_DATA.Created_Date,103) as [Created Date],TSPL_PP_BATCH_ORDER_HEAD_CANCEL_DATA.Description as Description" &
                ",TSPL_PP_BATCH_ORDER_HEAD_CANCEL_DATA.Cancel_By as [Cancelled By],convert(varchar,TSPL_PP_BATCH_ORDER_HEAD_CANCEL_DATA.Cancel_On,103) as [Cancelled Date] from TSPL_PP_BATCH_ORDER_HEAD_CANCEL_DATA " &
                  " WHERE  convert(date,TSPL_PP_BATCH_ORDER_HEAD_CANCEL_DATA.BATCH_DATE ,103) >= convert(date,'" + dtpFromDate.Value + "',103) " &
                  " and convert(date,TSPL_PP_BATCH_ORDER_HEAD_CANCEL_DATA.BATCH_DATE,103) <= convert(date,'" + dtpToDate.Value + "',103) "

            If chkUserSelect.IsChecked AndAlso cbgUser.CheckedValue.Count > 0 Then

                qry += " and TSPL_PP_BATCH_ORDER_HEAD_CANCEL_DATA.Created_By IN (" + clsCommon.GetMulcallString(cbgUser.CheckedValue) + ")"
            Else
                qry += " and TSPL_PP_BATCH_ORDER_HEAD_CANCEL_DATA.Created_By IN (" + clsCommon.GetMulcallString(arrSelectedUser) + ")"
            End If

            qry += " ORDER BY TSPL_PP_BATCH_ORDER_HEAD_CANCEL_DATA.BATCH_CODE,TSPL_PP_BATCH_ORDER_HEAD_CANCEL_DATA.BATCH_DATE "

        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), clsUserMgtCode.frmProcessProductionIssueEntry) = CompairStringResult.Equal Then

            gv1.DataSource = Nothing
            qry = "Select TSPL_PP_ISSUE_HEAD_CANCEL_DATA.ISSUE_CODE as[Document Id],convert(varchar,TSPL_PP_ISSUE_HEAD_CANCEL_DATA.ISSUE_DATE ,103) as [Document Date]" &
                " ,TSPL_PP_ISSUE_HEAD_CANCEL_DATA.Standardization_Code as [Standardization Code],TSPL_PP_ISSUE_HEAD_CANCEL_DATA.STAGE_PROCESS_CODE as [Stage Process Code]" &
                " ,TSPL_PP_ISSUE_HEAD_CANCEL_DATA.Batch_Code as [Batch Code] , TSPL_PP_ISSUE_HEAD_CANCEL_DATA.Created_By as [Created By],convert(varchar,TSPL_PP_ISSUE_HEAD_CANCEL_DATA.Created_Date,103) as [Created Date],TSPL_PP_ISSUE_HEAD_CANCEL_DATA.Description as Description" &
                " ,TSPL_PP_ISSUE_HEAD_CANCEL_DATA.Cancel_By as [Cancelled By],convert(varchar,TSPL_PP_ISSUE_HEAD_CANCEL_DATA.Cancel_On,103) as [Cancelled Date] from TSPL_PP_ISSUE_HEAD_CANCEL_DATA " &
                  " WHERE  convert(date,TSPL_PP_ISSUE_HEAD_CANCEL_DATA.ISSUE_DATE ,103) >= convert(date,'" + dtpFromDate.Value + "',103) " &
                  " and convert(date,TSPL_PP_ISSUE_HEAD_CANCEL_DATA.ISSUE_DATE,103) <= convert(date,'" + dtpToDate.Value + "',103) "

            If chkUserSelect.IsChecked AndAlso cbgUser.CheckedValue.Count > 0 Then

                qry += " and TSPL_PP_ISSUE_HEAD_CANCEL_DATA.Created_By IN (" + clsCommon.GetMulcallString(cbgUser.CheckedValue) + ")"
            Else
                qry += " and TSPL_PP_ISSUE_HEAD_CANCEL_DATA.Created_By IN (" + clsCommon.GetMulcallString(arrSelectedUser) + ")"
            End If

            qry += " ORDER BY TSPL_PP_ISSUE_HEAD_CANCEL_DATA.ISSUE_CODE,TSPL_PP_ISSUE_HEAD_CANCEL_DATA.ISSUE_DATE "

        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), clsUserMgtCode.frmProductionEntry) = CompairStringResult.Equal Then

            gv1.DataSource = Nothing
            qry = "Select TSPL_PP_PRODUCTION_ENTRY_CANCEL_DATA.PROD_ENTRY_CODE as[Document Id],convert(varchar,TSPL_PP_PRODUCTION_ENTRY_CANCEL_DATA.PROD_DATE ,103) as [Document Date] " &
                ",TSPL_PP_PRODUCTION_ENTRY_CANCEL_DATA.STRUCTURE_CODE as [Structure Code],TSPL_PP_PRODUCTION_ENTRY_CANCEL_DATA.Section_Stage_Map_Code as [Section Stage Map Code] " &
                ",TSPL_PP_PRODUCTION_ENTRY_CANCEL_DATA.CONSM_LOCATION_CODE as [Consumption Location Code],TSPL_PP_PRODUCTION_ENTRY_CANCEL_DATA.CONSM_SECTION_CODE as [Consumption Section Code]  " &
                ",TSPL_PP_PRODUCTION_ENTRY_CANCEL_DATA.Batch_Code as [Batch Code] , TSPL_PP_PRODUCTION_ENTRY_CANCEL_DATA.Created_By as [Created By] " &
                ",convert(varchar,TSPL_PP_PRODUCTION_ENTRY_CANCEL_DATA.Created_Date,103) as [Created Date],TSPL_PP_PRODUCTION_ENTRY_CANCEL_DATA.Description as Description" &
                ",TSPL_PP_PRODUCTION_ENTRY_CANCEL_DATA.Cancel_By as [Cancelled By],convert(varchar,TSPL_PP_PRODUCTION_ENTRY_CANCEL_DATA.Cancel_On,103) as [Cancelled Date] from TSPL_PP_PRODUCTION_ENTRY_CANCEL_DATA " &
                  " WHERE  convert(date,TSPL_PP_PRODUCTION_ENTRY_CANCEL_DATA.PROD_DATE ,103) >= convert(date,'" + dtpFromDate.Value + "',103) " &
                  " and convert(date,TSPL_PP_PRODUCTION_ENTRY_CANCEL_DATA.PROD_DATE,103) <= convert(date,'" + dtpToDate.Value + "',103) "

            If chkUserSelect.IsChecked AndAlso cbgUser.CheckedValue.Count > 0 Then

                qry += " and TSPL_PP_PRODUCTION_ENTRY_CANCEL_DATA.Created_By IN (" + clsCommon.GetMulcallString(cbgUser.CheckedValue) + ")"
            Else
                qry += " and TSPL_PP_PRODUCTION_ENTRY_CANCEL_DATA.Created_By IN (" + clsCommon.GetMulcallString(arrSelectedUser) + ")"
            End If

            qry += " ORDER BY TSPL_PP_PRODUCTION_ENTRY_CANCEL_DATA.PROD_ENTRY_CODE,TSPL_PP_PRODUCTION_ENTRY_CANCEL_DATA.PROD_DATE "


        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), clsUserMgtCode.frmStanderdProductionEntry) = CompairStringResult.Equal Then

            gv1.DataSource = Nothing
            qry = "Select TSPL_SPP_PRODUCTION_ENTRY_Cancel_Data.PROD_ENTRY_CODE as[Document Id],convert(varchar,TSPL_SPP_PRODUCTION_ENTRY_Cancel_Data.PROD_DATE ,103) as [Document Date] " &
                ",TSPL_SPP_PRODUCTION_ENTRY_Cancel_Data.STRUCTURE_CODE as [Structure Code],TSPL_SPP_PRODUCTION_ENTRY_Cancel_Data.Section_Stage_Map_Code as [Section Stage Map Code] " &
                ",TSPL_SPP_PRODUCTION_ENTRY_Cancel_Data.CONSM_LOCATION_CODE as [Consumption Location Code],TSPL_SPP_PRODUCTION_ENTRY_Cancel_Data.CONSM_SECTION_CODE as [Consumption Section Code]  " &
                ",TSPL_SPP_PRODUCTION_ENTRY_Cancel_Data.Batch_Code as [Batch Code] , TSPL_SPP_PRODUCTION_ENTRY_Cancel_Data.Created_By as [Created By] " &
                ",convert(varchar,TSPL_SPP_PRODUCTION_ENTRY_Cancel_Data.Created_Date,103) as [Created Date],TSPL_SPP_PRODUCTION_ENTRY_Cancel_Data.Description as Description" &
                ",TSPL_SPP_PRODUCTION_ENTRY_Cancel_Data.Cancel_By as [Cancelled By],convert(varchar,TSPL_SPP_PRODUCTION_ENTRY_Cancel_Data.Cancel_On,103) as [Cancelled Date] from TSPL_SPP_PRODUCTION_ENTRY_Cancel_Data " &
                  " WHERE  convert(date,TSPL_SPP_PRODUCTION_ENTRY_Cancel_Data.PROD_DATE ,103) >= convert(date,'" + dtpFromDate.Value + "',103) " &
                  " and convert(date,TSPL_SPP_PRODUCTION_ENTRY_Cancel_Data.PROD_DATE,103) <= convert(date,'" + dtpToDate.Value + "',103) "

            If chkUserSelect.IsChecked AndAlso cbgUser.CheckedValue.Count > 0 Then

                qry += " and TSPL_SPP_PRODUCTION_ENTRY_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(cbgUser.CheckedValue) + ")"
            Else
                qry += " and TSPL_SPP_PRODUCTION_ENTRY_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(arrSelectedUser) + ")"
            End If

            qry += " ORDER BY TSPL_SPP_PRODUCTION_ENTRY_Cancel_Data.PROD_ENTRY_CODE,TSPL_SPP_PRODUCTION_ENTRY_Cancel_Data.PROD_DATE "



        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), clsUserMgtCode.frmBillOfMaterialCosting) = CompairStringResult.Equal Then

            gv1.DataSource = Nothing
            qry = "Select TSPL_MF_BOM_HEAD_cancel_data.BOM_CODE as[Document Id],convert(varchar,TSPL_MF_BOM_HEAD_cancel_data.BOM_DATE ,103) as [Document Date] " &
                ",''as [Structure Code],'' as [Section Stage Map Code] " &
                ",'' as [Consumption Location Code],'' as [Consumption Section Code]  " &
                ",'' as [Batch Code] , TSPL_MF_BOM_HEAD_cancel_data.Created_By as [Created By] " &
                ",convert(varchar,TSPL_MF_BOM_HEAD_cancel_data.Created_Date,103) as [Created Date],TSPL_MF_BOM_HEAD_cancel_data.Description as Description" &
                ",TSPL_MF_BOM_HEAD_cancel_data.Cancel_By as [Cancelled By],convert(varchar,TSPL_MF_BOM_HEAD_cancel_data.Cancel_On,103) as [Cancelled Date] from TSPL_MF_BOM_HEAD_cancel_data " &
                  " WHERE  convert(date,TSPL_MF_BOM_HEAD_cancel_data.BOM_DATE ,103) >= convert(date,'" + dtpFromDate.Value + "',103) " &
                  " and convert(date,TSPL_MF_BOM_HEAD_cancel_data.BOM_DATE,103) <= convert(date,'" + dtpToDate.Value + "',103) "

            If chkUserSelect.IsChecked AndAlso cbgUser.CheckedValue.Count > 0 Then

                qry += " and TSPL_MF_BOM_HEAD_cancel_data.Created_By IN (" + clsCommon.GetMulcallString(cbgUser.CheckedValue) + ")"
            Else
                qry += " and TSPL_MF_BOM_HEAD_cancel_data.Created_By IN (" + clsCommon.GetMulcallString(arrSelectedUser) + ")"
            End If

            qry += " ORDER BY TSPL_MF_BOM_HEAD_cancel_data.BOM_CODE,TSPL_MF_BOM_HEAD_cancel_data.BOM_DATE "

        End If
        If clsCommon.CompairString(clsCommon.myCstr(qry), Nothing) <> CompairStringResult.Equal Then

            dt = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = dt
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1Format()

            If dt.Rows.Count <= 0 Then
                lblNoOfRecords.Text = "No Record Found"
            Else
                strNoOfRecord = clsCommon.myCstr(dt.Rows.Count)
                lblNoOfRecords.Text = "" + strNoOfRecord + " Records Found"
            End If
        End If
        '-------------------------------------Code Ends Here--------------------------------------------
    End Sub

#End Region


    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeForm()
    End Sub


    Sub closeForm()
        Me.Close()
    End Sub


    Private Sub FrmPendingAproval_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.C Then
            closeForm()
        End If
    End Sub

    Private Sub chkLOcAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        cbgLocation.Enabled = False
    End Sub

    Private Sub chkLOcSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        cbgLocation.Enabled = True
    End Sub
    Public Sub LoadLocation()
        Dim qry As String = Nothing
        If clsCommon.myLen(arrLoc) > 0 Then
            qry = " Select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER Where Location_Type='Physical' and Location_Code in (" + arrLoc + ")"
        End If
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Description"
    End Sub

    Public Sub LoadUsers()
        Dim qry As String = clsUserMaster.GetSubbordinateUsersQry(objCommonVar.CurrentUserCode)
        cbgUser.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgUser.ValueMember = "User_Code"
        cbgUser.DisplayMember = "User_Name"
    End Sub

    Private Sub chkLocAll_ToggleStateChanged_1(sender As Object, args As StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocAll.IsChecked
    End Sub

    Private Sub ChkUserAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles ChkUserAll.ToggleStateChanged
        cbgUser.Enabled = Not ChkUserAll.IsChecked
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        dtpFromDate.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        chkLocAll.CheckState = CheckState.Checked
        ChkUserAll.CheckState = CheckState.Checked
        gv1.DataSource = Nothing
    End Sub

    Private Sub gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Try
            If clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), clsUserMgtCode.frmProductionPlanningSTD) = CompairStringResult.Equal Then
                clsProductionPlanning.funPPPrint(MyBase.Form_ID, True, Nothing, clsCommon.myCstr(gv1.Rows(gv1.CurrentCell.RowIndex).Cells("Document ID").Value))
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), clsUserMgtCode.frmAssembDis) = CompairStringResult.Equal Then
                clsProductionPlanning.funAssembDisPrint(MyBase.Form_ID, True, Nothing, clsCommon.myCstr(gv1.Rows(gv1.CurrentCell.RowIndex).Cells("Document ID").Value))

            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), clsUserMgtCode.frmBillOfMaterialCosting) = CompairStringResult.Equal Then
                clsBillOfMaterial.funBOMCancelPrint(MyBase.Form_ID, True, Nothing, clsCommon.myCstr(gv1.Rows(gv1.CurrentCell.RowIndex).Cells("Document ID").Value))
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), clsUserMgtCode.frmStanderdProductionEntry) = CompairStringResult.Equal Then
                clsStanderdProductionEntry.funCancleSPEPrint(MyBase.Form_ID, True, Nothing, clsCommon.myCstr(gv1.Rows(gv1.CurrentCell.RowIndex).Cells("Document ID").Value))

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub
End Class




