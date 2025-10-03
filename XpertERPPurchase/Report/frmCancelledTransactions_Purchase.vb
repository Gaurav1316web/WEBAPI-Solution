''Created By--Sanjay----on- 21/10/2021---------------------
Imports common
Imports System.Data.SqlClient



Public Class frmCancelledTransactions_Purchase
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
        Try
            LOCATIONRIGTHS()
            ButtonToolTip.SetToolTip(btnClose, "Press Alt+C for Closing The Window")
            LoadLocation()
            chkLocAll.CheckState = CheckState.Checked
            ChkUserAll.CheckState = CheckState.Checked
            LoadUsers()
            arrUser = GetSubbordinateUsers(objCommonVar.CurrentUserCode)
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
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    'Public Sub LoadModuleType()
    '    Dim dt As DataTable = New DataTable()
    '    dt.Columns.Add("Code", GetType(String))
    '    dt.Columns.Add("Name", GetType(String))

    '    dr = dt.NewRow()
    '    dr("Code") = "Purchase"
    '    dr("Name") = "Purchase"
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
    Public Sub LoadModuleType()
        Dim Qry As String = "select Distinct TBL_MODULE.Program_Code As [Module Code],case when len (isnull(TBL_MODULE.Re_Name,'')) > 0 then TBL_MODULE.Re_Name else  TSPL_PROGRAM_MASTER.Program_Name end  as [Module Name] 
from TSPL_PROGRAM_MASTER
left outer join (select Program_Code, Program_Name,Parent_Code,case when len (isnull(TSPL_PROGRAM_MASTER.Re_Name,'')) > 0 then TSPL_PROGRAM_MASTER.Re_Name else  TSPL_PROGRAM_MASTER.Program_Name end As Re_Name from TSPL_PROGRAM_MASTER where Type in ('SM')) as TBL_SMODULE on TBL_SMODULE.Program_Code = TSPL_PROGRAM_MASTER.Parent_Code
left outer join (select Program_Code, Program_Name,Parent_Code,case when len (isnull(TSPL_PROGRAM_MASTER.Re_Name,'')) > 0 then TSPL_PROGRAM_MASTER.Re_Name else  TSPL_PROGRAM_MASTER.Program_Name end As Re_Name from TSPL_PROGRAM_MASTER where Type in ('M')) as TBL_MODULE on TBL_MODULE.Program_Code = TBL_SMODULE.Parent_Code
Where TBL_MODULE.Program_Code in (select  distinct Module_Name from TSPL_MODULE_PERMISSION ) and  not TSPL_PROGRAM_MASTER.Type in ('M','SM') 
and TBL_MODULE.Program_Code in ('" & clsUserMgtCode.ModulePurchase & "','" & clsUserMgtCode.ModuleQualityControl & "') 
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
    Sub LoadModuleProduction()
        Try
            Dim Qry As String = "select TSPL_PROGRAM_MASTER.Program_Code as Code,case when len (isnull(TSPL_PROGRAM_MASTER.Re_Name,'')) > 0 then TSPL_PROGRAM_MASTER.Re_Name else  TSPL_PROGRAM_MASTER.Program_Name end  as Name 
        from TSPL_PROGRAM_MASTER
        left outer join (select Program_Code, Program_Name,Parent_Code,case when len (isnull(TSPL_PROGRAM_MASTER.Re_Name,'')) > 0 then TSPL_PROGRAM_MASTER.Re_Name else  TSPL_PROGRAM_MASTER.Program_Name end As Re_Name from TSPL_PROGRAM_MASTER where Type in ('SM')) as TBL_SMODULE on TBL_SMODULE.Program_Code = TSPL_PROGRAM_MASTER.Parent_Code
        left outer join (select Program_Code, Program_Name,Parent_Code,case when len (isnull(TSPL_PROGRAM_MASTER.Re_Name,'')) > 0 then TSPL_PROGRAM_MASTER.Re_Name else  TSPL_PROGRAM_MASTER.Program_Name end As Re_Name from TSPL_PROGRAM_MASTER where Type in ('M')) as TBL_MODULE on TBL_MODULE.Program_Code = TBL_SMODULE.Parent_Code
        Where TBL_MODULE.Program_Code in (select  distinct Module_Name from TSPL_MODULE_PERMISSION ) and  not TSPL_PROGRAM_MASTER.Type in ('M','SM') 
        and TBL_SMODULE.Parent_Code In ('" & clsCommon.myCstr(cboModule.SelectedValue) & "') 
        and TBL_SMODULE.Program_Name in ('Transaction','MCC Transaction','Bulk Transaction') And TSPL_PROGRAM_MASTER.Program_Code In ('" & clsUserMgtCode.frmStoreRequistion & "','" & clsUserMgtCode.mbtnGRN & "','" & clsUserMgtCode.POWeighment & "','" & clsUserMgtCode.mbtnMRN & "','" & clsUserMgtCode.mbtnSRN & "','" & clsUserMgtCode.mbtnPurchaseInvoice & "','" & clsUserMgtCode.mbtnPurchaseReturn & "','" & clsUserMgtCode.mbtnPurchaseRequistion & "','" & clsUserMgtCode.mbtnIssueReturn & "','" & clsUserMgtCode.mbtnGatePass & "','" & clsUserMgtCode.NIRQC & "')
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
        'Dim dt1 As DataTable = New DataTable()
        '    dt1.Columns.Add("Code", GetType(String))
        '    dt1.Columns.Add("Name", GetType(String))

        '    dr = dt1.NewRow()
        '    dr("Code") = "Store Requisition"
        '    dr("Name") = "Store Requisition"
        '    dt1.Rows.Add(dr)

        '    dr = dt1.NewRow()
        '    dr("Code") = "Gate Received Note"
        '    dr("Name") = "Gate Received Note"
        '    dt1.Rows.Add(dr)

        '    dr = dt1.NewRow()
        '    dr("Code") = "Material Received Note"
        '    dr("Name") = "Material Received Note"
        '    dt1.Rows.Add(dr)

        '    dr = dt1.NewRow()
        '    dr("Code") = "Store Received Note"
        '    dr("Name") = "Store Received Note"
        '    dt1.Rows.Add(dr)


        '    dr = dt1.NewRow()
        '    dr("Code") = "Purchase Invoice"
        '    dr("Name") = "Purchase Invoice"
        '    dt1.Rows.Add(dr)

        '    dr = dt1.NewRow()
        '    dr("Code") = "Purchase Return"
        '    dr("Name") = "Purchase Return"
        '    dt1.Rows.Add(dr)


        '    dr = dt1.NewRow()
        '    dr("Code") = "Issue/Return/Transfer"
        '    dr("Name") = "Issue/Return/Transfer"
        '    dt1.Rows.Add(dr)

        ''dr = dt1.NewRow()
        ''dr("Code") = "Tender"
        ''dr("Name") = "Tender"
        ''dt1.Rows.Add(dr)

        'cboTransaction.DataSource = dt1
        '    cboTransaction.DisplayMember = "Name"
        '    cboTransaction.ValueMember = "Code"
    End Sub

    Private Sub cboModule_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboModule.SelectedIndexChanged
        LoadTrnsListOfSelectedModeule()
    End Sub

    Public Sub LoadTrnsListOfSelectedModeule()
        Try
            'If clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Purchase") = CompairStringResult.Equal Then
            cboTransaction.DataSource = Nothing
            LoadModuleProduction()
            'End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.AllowAddNewRow = False

    End Sub
    Sub gv1Format()
        Me.gv1.MasterTemplate.Columns("Document Id").Width = 120      ''First Column
        Me.gv1.MasterTemplate.Columns("Document Date").Width = 150    ''Second Column
        'If clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Purchase Invoice") = CompairStringResult.Equal Then
        '    Me.gv1.MasterTemplate.Columns("item code").Width = 150    ''Second Column

        'End If

        Dim count As Integer = gv1.MasterTemplate.Columns.Count
        For i As Integer = 2 To count - 2
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
            common.clsCommon.MyMessageBoxShow("'From date' Can't Be Greater Than 'To Date'")
        Else
            qry = Nothing
            ShowData()
        End If


    End Sub
    Function GetSubbordinateUsers(ByVal strUserCode As String) As ArrayList
        Dim arrUser As New ArrayList
        Try
            Dim qry As String
            If clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal Then
                qry = "Select User_Code from TSPL_User_MASTER Where 1=1"
            Else
                qry = "Select User_Code from TSPL_User_MASTER Where Level4_Code='" + strUserCode + "' OR User_Code='" + strUserCode + "'"
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            For Each dr As DataRow In dt.Rows
                arrUser.Add(clsCommon.myCstr(dr("User_Code")))
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return arrUser
    End Function
    Sub ShowData()
        Try
            If cbgUser.CheckedValue.Count > 0 Then
                arrSelectedUser = cbgUser.CheckedValue
                'Else
                '    arrSelectedUser = arrUser
            End If
            gv1.DataSource = Nothing
            If clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), clsUserMgtCode.ModulePurchase) = CompairStringResult.Equal Then
                FillPurchase()
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), clsUserMgtCode.ModuleQualityControl) = CompairStringResult.Equal Then
                FillQualityControl()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub FillQualityControl()
        Try
            If clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), clsUserMgtCode.NIRQC) = CompairStringResult.Equal Then
                qry = "Select TSPL_NIR_QC_Cancel_Data.Document_No as [Document Id],convert(varchar,TSPL_NIR_QC_Cancel_Data.Document_Date ,103) as [Document Date],TSPL_MRN_HEAD_Cancel_Data.Against_Requisition as [Against Requisition],TSPL_NIR_QC_Cancel_Data.MRN_No as [Against MRN] 
                    ,TSPL_MRN_HEAD_Cancel_Data.Bill_To_Location as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_NIR_QC_Cancel_Data.Created_By as [Created By],convert(varchar,TSPL_NIR_QC_Cancel_Data.Created_Date,103) as [Created Date],TSPL_MRN_HEAD_Cancel_Data.DESCRIPTION as Description 
	,TSPL_NIR_QC_Cancel_Data.Cancel_By as [Cancelled By],TSPL_NIR_QC_Cancel_Data.Cancel_On as [Cancelled Date] from TSPL_NIR_QC_Cancel_Data 
					Left Outer Join TSPL_MRN_HEAD_Cancel_Data On TSPL_MRN_HEAD_Cancel_Data.MRN_No=TSPL_NIR_QC_Cancel_Data.MRN_No
                    Left Outer Join TSPL_LOCATION_MASTER  on TSPL_MRN_HEAD_Cancel_Data.Bill_To_Location  =TSPL_LOCATION_MASTER.Location_Code 
                     WHERE  1=1"
                If rbtnCancelDate.IsChecked Then
                    qry += " And convert(date,TSPL_NIR_QC_Cancel_Data.cancel_on ,103) >= convert(date,'" & dtpFromDate.Value & "',103) 
                     And Convert(Date, TSPL_NIR_QC_Cancel_Data.cancel_on,103) <= Convert(Date,'" & dtpToDate.Value & "',103)   "
                Else
                    qry += " And convert(date,TSPL_NIR_QC_Cancel_Data.Document_Date ,103) >= convert(date,'" & dtpFromDate.Value & "',103) 
                     And Convert(Date, TSPL_NIR_QC_Cancel_Data.Document_Date,103) <= Convert(Date,'" & dtpToDate.Value & "',103)   "
                End If

                If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                    qry += " and TSPL_MRN_HEAD_Cancel_Data.Bill_To_Location  in   (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                End If
                If chkUserSelect.IsChecked AndAlso cbgUser.CheckedValue.Count > 0 Then
                    qry += " and TSPL_NIR_QC_Cancel_Data.Cancel_By IN (" + clsCommon.GetMulcallString(cbgUser.CheckedValue) + ")"
                End If
                qry += " ORDER BY TSPL_NIR_QC_Cancel_Data.Document_Date,TSPL_NIR_QC_Cancel_Data.Document_No "
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
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub FillPurchase()
        Try
            If clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), clsUserMgtCode.mbtnGRN) = CompairStringResult.Equal Then
                gv1.DataSource = Nothing
                qry = "Select TSPL_GRN_HEAD_Cancel_Data.GRN_NO as [Document Id],convert(varchar,TSPL_GRN_HEAD_Cancel_Data.GRN_date ,103) as [Document Date]
                    ,TSPL_GRN_HEAD_Cancel_Data.Against_Requisition as [Against Requisition],TSPL_GRN_HEAD_Cancel_Data.Against_PO as [Against PO] 
                    ,TSPL_GRN_HEAD_Cancel_Data.Bill_To_Location as [Location Code]
                    ,TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_GRN_HEAD_Cancel_Data.Created_By as [Created By]
                    ,convert(varchar,TSPL_GRN_HEAD_Cancel_Data.Created_Date,103) as [Created Date],TSPL_GRN_HEAD_Cancel_Data.DESCRIPTION as Description 
	,Cancel_By as [Cancelled By],
					Cancel_On as [Cancelled Date] from TSPL_GRN_HEAD_Cancel_Data 
                    Left Outer Join TSPL_LOCATION_MASTER  on TSPL_GRN_HEAD_Cancel_Data.Bill_To_Location  =TSPL_LOCATION_MASTER.Location_Code 
                     WHERE  "
                If rbtnCancelDate.IsChecked Then
                    qry += " convert(date,TSPL_GRN_HEAD_Cancel_Data.Cancel_On ,103) >= convert(date,'" + dtpFromDate.Value + "',103) 
                     And Convert(Date, TSPL_GRN_HEAD_Cancel_Data.cancel_on,103) <= Convert(Date,'" + dtpToDate.Value + "',103) "
                Else
                    qry += " convert(date,TSPL_GRN_HEAD_Cancel_Data.GRN_date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) 
                     And Convert(Date, TSPL_GRN_HEAD_Cancel_Data.GRN_date,103) <= Convert(Date,'" + dtpToDate.Value + "',103) "
                End If

                If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                    qry += " and TSPL_GRN_HEAD_Cancel_Data.Bill_To_Location  in   (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                End If
                If chkUserSelect.IsChecked AndAlso cbgUser.CheckedValue.Count > 0 Then
                    qry += " and TSPL_GRN_HEAD_Cancel_Data.Cancel_By IN (" + clsCommon.GetMulcallString(cbgUser.CheckedValue) + ")"
                    'Else
                    '    qry += " and TSPL_GRN_HEAD_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(arrSelectedUser) + ")"
                End If
                qry += " ORDER BY TSPL_GRN_HEAD_Cancel_Data.GRN_DATE,TSPL_GRN_HEAD_Cancel_Data.GRN_NO "
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), clsUserMgtCode.POWeighment) = CompairStringResult.Equal Then
                gv1.DataSource = Nothing
                qry = "Select TSPL_PO_WEIGHTMENT_HEAD_Cancel_Data.Weighment_Code as [Document Id],convert(varchar,TSPL_PO_WEIGHTMENT_HEAD_Cancel_Data.Weighment_Date ,103) as [Document Date],TSPL_GRN_HEAD_Cancel_Data.Against_Requisition as [Against Requisition],TSPL_PO_WEIGHTMENT_HEAD_Cancel_Data.Against_GRN_No as [Against GRN] 
                    ,TSPL_GRN_HEAD_Cancel_Data.Bill_To_Location as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_PO_WEIGHTMENT_HEAD_Cancel_Data.Created_By as [Created By],convert(varchar,TSPL_PO_WEIGHTMENT_HEAD_Cancel_Data.Created_Date,103) as [Created Date],TSPL_GRN_HEAD_Cancel_Data.DESCRIPTION as Description 
	,TSPL_PO_WEIGHTMENT_HEAD_Cancel_Data.Cancel_By as [Cancelled By],
					TSPL_PO_WEIGHTMENT_HEAD_Cancel_Data.Cancel_On as [Cancelled Date] from TSPL_PO_WEIGHTMENT_HEAD_Cancel_Data 
					Left Outer Join TSPL_GRN_HEAD_Cancel_Data On TSPL_GRN_HEAD_Cancel_Data.GRN_No=TSPL_PO_WEIGHTMENT_HEAD_Cancel_Data.Against_GRN_No
                    Left Outer Join TSPL_LOCATION_MASTER  on TSPL_PO_WEIGHTMENT_HEAD_Cancel_Data.Location_Code  =TSPL_LOCATION_MASTER.Location_Code 
                     WHERE  1=1 "
                If rbtnCancelDate.IsChecked Then
                    qry += " And convert(date,TSPL_PO_WEIGHTMENT_HEAD_Cancel_Data.Cancel_On ,103) >= convert(date,'" & dtpFromDate.Value & "',103) 
                     And Convert(Date, TSPL_PO_WEIGHTMENT_HEAD_Cancel_Data.Cancel_On,103) <= Convert(Date,'" & dtpToDate.Value & "',103)  "
                Else
                    qry += " And convert(date,TSPL_PO_WEIGHTMENT_HEAD_Cancel_Data.Weighment_Date ,103) >= convert(date,'" & dtpFromDate.Value & "',103) 
                     And Convert(Date, TSPL_PO_WEIGHTMENT_HEAD_Cancel_Data.Weighment_Date,103) <= Convert(Date,'" & dtpToDate.Value & "',103)  "
                End If

                If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                    qry += " and TSPL_PO_WEIGHTMENT_HEAD_Cancel_Data.Bill_To_Location  in   (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                End If
                If chkUserSelect.IsChecked AndAlso cbgUser.CheckedValue.Count > 0 Then
                    qry += " and TSPL_PO_WEIGHTMENT_HEAD_Cancel_Data.Cancel_By IN (" + clsCommon.GetMulcallString(cbgUser.CheckedValue) + ")"
                    'Else
                    '    qry += " and TSPL_PO_WEIGHTMENT_HEAD_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(arrSelectedUser) + ")"
                End If
                qry += " ORDER BY TSPL_PO_WEIGHTMENT_HEAD_Cancel_Data.Weighment_Date,TSPL_PO_WEIGHTMENT_HEAD_Cancel_Data.Weighment_Code "
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), clsUserMgtCode.mbtnMRN) = CompairStringResult.Equal Then
                gv1.DataSource = Nothing
                qry = "Select TSPL_MRN_HEAD_Cancel_Data.MRN_NO as [Document Id],convert(varchar,TSPL_MRN_HEAD_Cancel_Data.MRN_date ,103) as [Document Date]
                    ,TSPL_MRN_HEAD_Cancel_Data.Against_PO as [Against PO] ,TSPL_MRN_HEAD_Cancel_Data.Against_GRN as [Against GRN]
                    ,TSPL_MRN_HEAD_Cancel_Data.Bill_To_Location as [Location Code]
                    ,TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_MRN_HEAD_Cancel_Data.Created_By as [Created By]
                    ,convert(varchar,TSPL_MRN_HEAD_Cancel_Data.Created_Date,103) as [Created Date],TSPL_MRN_HEAD_Cancel_Data.DESCRIPTION as Description ,Cancel_By as [Cancelled By],
					Cancel_On as [Cancelled Date]  from TSPL_MRN_HEAD_Cancel_Data 
                    Left Outer Join TSPL_LOCATION_MASTER  on TSPL_MRN_HEAD_Cancel_Data.Bill_To_Location  =TSPL_LOCATION_MASTER.Location_Code 
                     WHERE"
                If rbtnCancelDate.IsChecked Then
                    qry += " convert(date,TSPL_MRN_HEAD_Cancel_Data.Cancel_On ,103) >= convert(date,'" + dtpFromDate.Value + "',103) 
                     And Convert(Date, TSPL_MRN_HEAD_Cancel_Data.Cancel_On,103) <= Convert(Date,'" + dtpToDate.Value + "',103) "
                Else
                    qry += "  convert(date,TSPL_MRN_HEAD_Cancel_Data.MRN_date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) 
                     And Convert(Date, TSPL_MRN_HEAD_Cancel_Data.MRN_date,103) <= Convert(Date,'" + dtpToDate.Value + "',103) "
                End If

                If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                    qry += " and TSPL_MRN_HEAD_Cancel_Data.Bill_To_Location  in   (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                End If
                If chkUserSelect.IsChecked AndAlso cbgUser.CheckedValue.Count > 0 Then
                    qry += " and TSPL_MRN_HEAD_Cancel_Data.Cancel_By IN (" + clsCommon.GetMulcallString(cbgUser.CheckedValue) + ")"
                    'Else
                    '    qry += " and TSPL_MRN_HEAD_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(arrSelectedUser) + ")"
                End If

                qry += " ORDER BY TSPL_MRN_HEAD_Cancel_Data.MRN_DATE,TSPL_MRN_HEAD_Cancel_Data.MRN_NO "
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), clsUserMgtCode.mbtnSRN) = CompairStringResult.Equal Then
                gv1.DataSource = Nothing
                qry = "Select TSPL_SRN_HEAD_Cancel_Data.SRN_NO as [Document Id],convert(varchar,TSPL_SRN_HEAD_Cancel_Data.SRN_date ,103) as [Document Date]
                    ,TSPL_SRN_HEAD_Cancel_Data.Against_PO as [Against PO] ,TSPL_SRN_HEAD_Cancel_Data.Against_GRN as [Against GRN]
                    ,TSPL_SRN_HEAD_Cancel_Data.Against_MRN as [Against MRN],TSPL_SRN_HEAD_Cancel_Data.Bill_To_Location as [Location Code]
                    ,TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_SRN_HEAD_Cancel_Data.Created_By as [Created By]
                    ,convert(varchar,TSPL_SRN_HEAD_Cancel_Data.Created_Date,103) as [Created Date],TSPL_SRN_HEAD_Cancel_Data.DESCRIPTION as Description 
,Cancel_By as [Cancelled By],
					Cancel_On as [Cancelled Date] 
from TSPL_SRN_HEAD_Cancel_Data 
                    Left Outer Join TSPL_LOCATION_MASTER  on TSPL_SRN_HEAD_Cancel_Data.Bill_To_Location  =TSPL_LOCATION_MASTER.Location_Code 
                     WHERE "
                If rbtnCancelDate.IsChecked Then
                    qry += "convert(date,TSPL_SRN_HEAD_Cancel_Data.Cancel_On ,103) >= convert(date,'" + dtpFromDate.Value + "',103) 
                     And Convert(Date, TSPL_SRN_HEAD_Cancel_Data.Cancel_On,103) <= Convert(Date,'" + dtpToDate.Value + "',103) "
                Else
                    qry += "convert(date,TSPL_SRN_HEAD_Cancel_Data.SRN_date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) 
                     And Convert(Date, TSPL_SRN_HEAD_Cancel_Data.SRN_date,103) <= Convert(Date,'" + dtpToDate.Value + "',103) "
                End If


                If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                    qry += " and TSPL_SRN_HEAD_Cancel_Data.Bill_To_Location  in   (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                End If
                If chkUserSelect.IsChecked AndAlso cbgUser.CheckedValue.Count > 0 Then
                    qry += " and TSPL_SRN_HEAD_Cancel_Data.Cancel_By IN (" + clsCommon.GetMulcallString(cbgUser.CheckedValue) + ")"
                    'Else
                    '    qry += " and TSPL_SRN_HEAD_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(arrSelectedUser) + ")"
                End If
                qry += " ORDER BY TSPL_SRN_HEAD_Cancel_Data.SRN_DATE,TSPL_SRN_HEAD_Cancel_Data.SRN_NO "

            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), clsUserMgtCode.frmStoreRequistion) = CompairStringResult.Equal Then
                gv1.DataSource = Nothing
                qry = "Select TSPL_REQUISITION_HEAD_Cancel_Data.Requisition_Id as [Document Id],convert(varchar,TSPL_REQUISITION_HEAD_Cancel_Data.Requisition_Date ,103) as [Document Date]
                    ,TSPL_REQUISITION_HEAD_Cancel_Data.Location as [Location Code]
                    ,TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_REQUISITION_HEAD_Cancel_Data.Created_By as [Created By]
                    ,convert(varchar,TSPL_REQUISITION_HEAD_Cancel_Data.Created_Date,103) as [Created Date]
                     ,TSPL_REQUISITION_HEAD_Cancel_Data.Dept as [Department Code]
                     ,TSPL_REQUISITION_HEAD_Cancel_Data.Dept_Desc as [Department Name]
                    ,TSPL_REQUISITION_HEAD_Cancel_Data.Request_By as [Request By],	Cancel_By as [Cancelled By],
					Cancel_On as [Cancelled Date]
                    ,TSPL_REQUISITION_HEAD_Cancel_Data.DESCRIPTION as Description from TSPL_REQUISITION_HEAD_Cancel_Data 
                    Left Outer Join TSPL_LOCATION_MASTER  on TSPL_REQUISITION_HEAD_Cancel_Data.Location  =TSPL_LOCATION_MASTER.Location_Code 
                     WHERE  "
                If rbtnCancelDate.IsChecked Then
                    qry += "convert(date,TSPL_REQUISITION_HEAD_Cancel_Data.Cancel_On ,103) >= convert(date,'" + dtpFromDate.Value + "',103) 
                     And Convert(Date, TSPL_REQUISITION_HEAD_Cancel_Data.Cancel_On,103) <= Convert(Date,'" + dtpToDate.Value + "',103) "
                Else
                    qry += "convert(date,TSPL_REQUISITION_HEAD_Cancel_Data.Requisition_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) 
                     And Convert(Date, TSPL_REQUISITION_HEAD_Cancel_Data.Requisition_Date,103) <= Convert(Date,'" + dtpToDate.Value + "',103) "
                End If

                If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                    qry += " and TSPL_REQUISITION_HEAD_Cancel_Data.Location  in   (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                End If
                If chkUserSelect.IsChecked AndAlso cbgUser.CheckedValue.Count > 0 Then
                    qry += " and TSPL_REQUISITION_HEAD_Cancel_Data.Cancel_By IN (" + clsCommon.GetMulcallString(cbgUser.CheckedValue) + ")"
                    'Else
                    '    qry += " and TSPL_REQUISITION_HEAD_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(arrSelectedUser) + ")"
                End If
                qry += " ORDER BY TSPL_REQUISITION_HEAD_Cancel_Data.Requisition_Date,TSPL_REQUISITION_HEAD_Cancel_Data.Requisition_Id "

            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), clsUserMgtCode.mbtnIssueReturn) = CompairStringResult.Equal Then
                gv1.DataSource = Nothing
                qry = "Select TSPL_IssueReturn_HEAD_Cancel_Data.Doc_No as [Document Id],convert(varchar,TSPL_IssueReturn_HEAD_Cancel_Data.Doc_Date ,103) as [Document Date]
                    ,TSPL_IssueReturn_HEAD_Cancel_Data.Doc_Type as [Document Type]                 
                    ,TSPL_IssueReturn_HEAD_Cancel_Data.From_Location as [Location Code]
                    ,TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_IssueReturn_HEAD_Cancel_Data.Created_By as [Created By]
                    ,convert(varchar,TSPL_IssueReturn_HEAD_Cancel_Data.Created_Date,103) as [Created Date]
                    ,TSPL_IssueReturn_HEAD_Cancel_Data.Req_IssueNo as [Issue No],TSPL_IssueReturn_HEAD_Cancel_Data.RequisitionNo as [Requisition No]
                     ,TSPL_IssueReturn_HEAD_Cancel_Data.Dept as [Department Code]
                     ,TSPL_IssueReturn_HEAD_Cancel_Data.Dept_Desc as [Department Name]
                    ,TSPL_IssueReturn_HEAD_Cancel_Data.Request_By as [Request By]
                    ,TSPL_IssueReturn_HEAD_Cancel_Data.Againt_Cleaning_No as [Against Cleaning No]
,	Cancel_By as [Cancelled By],
					Cancel_On as [Cancelled Date] 
                    ,TSPL_IssueReturn_HEAD_Cancel_Data.Remarks as Description from TSPL_IssueReturn_HEAD_Cancel_Data 
                    Left Outer Join TSPL_LOCATION_MASTER  on TSPL_IssueReturn_HEAD_Cancel_Data.From_Location  =TSPL_LOCATION_MASTER.Location_Code 
                     WHERE  "
                If rbtnCancelDate.IsChecked Then
                    qry += "convert(date,TSPL_IssueReturn_HEAD_Cancel_Data.Cancel_On ,103) >= convert(date,'" + dtpFromDate.Value + "',103) 
                     And Convert(Date, TSPL_IssueReturn_HEAD_Cancel_Data.Cancel_On,103) <= Convert(Date,'" + dtpToDate.Value + "',103) "
                Else
                    qry += "convert(date,TSPL_IssueReturn_HEAD_Cancel_Data.Doc_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) 
                     And Convert(Date, TSPL_IssueReturn_HEAD_Cancel_Data.Doc_Date,103) <= Convert(Date,'" + dtpToDate.Value + "',103) "
                End If

                If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                    qry += " and TSPL_IssueReturn_HEAD_Cancel_Data.From_Location  in   (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                End If
                If chkUserSelect.IsChecked AndAlso cbgUser.CheckedValue.Count > 0 Then
                    qry += " and TSPL_IssueReturn_HEAD_Cancel_Data.Cancel_By IN (" + clsCommon.GetMulcallString(cbgUser.CheckedValue) + ")"
                    'Else
                    '    qry += " and TSPL_IssueReturn_HEAD_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(arrSelectedUser) + ")"
                End If
                qry += " ORDER BY TSPL_IssueReturn_HEAD_Cancel_Data.Doc_Date,TSPL_IssueReturn_HEAD_Cancel_Data.Doc_No "

            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), clsUserMgtCode.mbtnPurchaseInvoice) = CompairStringResult.Equal Then
                gv1.DataSource = Nothing
                qry = "Select TSPL_PI_HEAD_Cancel_Data.PI_No as [Document Id],convert(varchar,TSPL_PI_HEAD_Cancel_Data.PI_Date ,103) as [Document Date]                
                    ,TSPL_PI_HEAD_Cancel_Data.Bill_To_Location as [Location Code]
                    ,TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_PI_HEAD_Cancel_Data.Created_By as [Created By]
                    ,convert(varchar,TSPL_PI_HEAD_Cancel_Data.Created_Date,103) as [Created Date]
                     ,TSPL_PI_HEAD_Cancel_Data.Dept as [Department Code]
                     ,TSPL_PI_HEAD_Cancel_Data.Dept_Desc as [Department Name] 
                    ,TSPL_PI_HEAD_Cancel_Data.Against_Requisition as [Against Requisition]
                    ,TSPL_PI_HEAD_Cancel_Data.Against_PO as [Against PO] ,TSPL_PI_HEAD_Cancel_Data.Against_GRN as [Against GRN]
                    ,TSPL_PI_HEAD_Cancel_Data.Against_MRN as [Against MRN]
                    ,TSPL_PI_HEAD_Cancel_Data.Against_SRN as [Against SRN]
                    ,TSPL_PI_HEAD_Cancel_Data.Description as Description
,Cancel_By as [Cancelled By],
					Cancel_On as [Cancelled Date] 
,TSPL_PI_HEAD_Cancel_Data.Vendor_Code as [Vendor Code],TSPL_PI_HEAD_Cancel_Data.Ref_No as [Ref_No],'' as [Item code]
from TSPL_PI_HEAD_Cancel_Data 
                    Left Outer Join TSPL_LOCATION_MASTER  on TSPL_PI_HEAD_Cancel_Data.Bill_To_Location  =TSPL_LOCATION_MASTER.Location_Code 
                     WHERE  "
                If rbtnCancelDate.IsChecked Then
                    qry += "convert(date,TSPL_PI_HEAD_Cancel_Data.Cancel_On ,103) >= convert(date,'" + dtpFromDate.Value + "',103) 
                     And Convert(Date, TSPL_PI_HEAD_Cancel_Data.Cancel_On,103) <= Convert(Date,'" + dtpToDate.Value + "',103) "
                Else
                    qry += "convert(date,TSPL_PI_HEAD_Cancel_Data.PI_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) 
                     And Convert(Date, TSPL_PI_HEAD_Cancel_Data.PI_Date,103) <= Convert(Date,'" + dtpToDate.Value + "',103) "
                End If
                If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                        qry += " and TSPL_PI_HEAD_Cancel_Data.Bill_To_Location  in   (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                    End If
                    If chkUserSelect.IsChecked AndAlso cbgUser.CheckedValue.Count > 0 Then
                        qry += " and TSPL_PI_HEAD_Cancel_Data.Cancel_By IN (" + clsCommon.GetMulcallString(cbgUser.CheckedValue) + ")"
                        'Else
                        '    qry += " and TSPL_PI_HEAD_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(arrSelectedUser) + ")"
                    End If
                    qry += " ORDER BY TSPL_PI_HEAD_Cancel_Data.PI_Date,TSPL_PI_HEAD_Cancel_Data.PI_No "

                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), clsUserMgtCode.mbtnPurchaseReturn) = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing
                    qry = "Select TSPL_PR_HEAD_Cancel_Data.PR_No as [Document Id],convert(varchar,TSPL_PR_HEAD_Cancel_Data.PR_Date ,103) as [Document Date]                
                    ,TSPL_PR_HEAD_Cancel_Data.Bill_To_Location as [Location Code]
                    ,TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_PR_HEAD_Cancel_Data.Created_By as [Created By]
                    ,convert(varchar,TSPL_PR_HEAD_Cancel_Data.Created_Date,103) as [Created Date]
                    ,TSPL_PR_HEAD_Cancel_Data.Against_Requisition as [Against Requisition]
                    ,TSPL_PR_HEAD_Cancel_Data.Against_PO as [Against PO] ,TSPL_PR_HEAD_Cancel_Data.Against_GRN as [Against GRN]
                    ,TSPL_PR_HEAD_Cancel_Data.Against_MRN as [Against MRN]
                    ,TSPL_PR_HEAD_Cancel_Data.Against_SRN as [Against SRN]
                    ,TSPL_PR_HEAD_Cancel_Data.Against_PI as [Against PI]
                    ,TSPL_PR_HEAD_Cancel_Data.Description as Description 
,Cancel_By as [Cancelled By],
					Cancel_On as [Cancelled Date]
from TSPL_PR_HEAD_Cancel_Data 
                    Left Outer Join TSPL_LOCATION_MASTER  on TSPL_PR_HEAD_Cancel_Data.Bill_To_Location  =TSPL_LOCATION_MASTER.Location_Code 
                     WHERE"
                If rbtnCancelDate.IsChecked Then
                    qry += "convert(date,TSPL_PR_HEAD_Cancel_Data.Cancel_On ,103) >= convert(date,'" + dtpFromDate.Value + "',103) 
                     And Convert(Date, TSPL_PR_HEAD_Cancel_Data.Cancel_On,103) <= Convert(Date,'" + dtpToDate.Value + "',103) "
                Else
                    qry += "convert(date,TSPL_PR_HEAD_Cancel_Data.PR_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) 
                     And Convert(Date, TSPL_PR_HEAD_Cancel_Data.PR_Date,103) <= Convert(Date,'" + dtpToDate.Value + "',103) "
                End If


                If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                        qry += " and TSPL_PR_HEAD_Cancel_Data.Bill_To_Location  in   (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                    End If
                    If chkUserSelect.IsChecked AndAlso cbgUser.CheckedValue.Count > 0 Then
                        qry += " and TSPL_PR_HEAD_Cancel_Data.Cancel_By IN (" + clsCommon.GetMulcallString(cbgUser.CheckedValue) + ")"
                        'Else
                        '    qry += " and TSPL_PR_HEAD_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(arrSelectedUser) + ")"
                    End If
                    qry += " ORDER BY TSPL_PR_HEAD_Cancel_Data.PR_Date,TSPL_PR_HEAD_Cancel_Data.PR_No "


                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), clsUserMgtCode.mbtnPurchaseRequistion) = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing
                    qry = "Select TSPL_REQUISITION_HEAD_Cancel_Data.Requisition_Id as [Document Id],convert(varchar,TSPL_REQUISITION_HEAD_Cancel_Data.Requisition_Date ,103) as [Document Date]                
                    ,TSPL_REQUISITION_HEAD_Cancel_Data.Location as [Location Code]
                    ,TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_REQUISITION_HEAD_Cancel_Data.Created_By as [Created By]
                    ,convert(varchar,TSPL_REQUISITION_HEAD_Cancel_Data.Created_Date,103) as [Created Date]
                ,Cancel_By as [Cancelled By],
					Cancel_On as [Cancelled Date],'' as Description
from TSPL_REQUISITION_HEAD_Cancel_Data 
                    Left Outer Join TSPL_LOCATION_MASTER  on TSPL_REQUISITION_HEAD_Cancel_Data.Location  =TSPL_LOCATION_MASTER.Location_Code 
                     WHERE "
                If rbtnCancelDate.IsChecked Then
                    qry += "convert(date,TSPL_REQUISITION_HEAD_Cancel_Data.Cancel_On ,103) >= convert(date,'" + dtpFromDate.Value + "',103) 
                     And Convert(Date, TSPL_REQUISITION_HEAD_Cancel_Data.Cancel_On,103) <= Convert(Date,'" + dtpToDate.Value + "',103) "
                Else
                    qry += "convert(date,TSPL_REQUISITION_HEAD_Cancel_Data.Requisition_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) 
                     And Convert(Date, TSPL_REQUISITION_HEAD_Cancel_Data.Requisition_Date,103) <= Convert(Date,'" + dtpToDate.Value + "',103) "
                End If
                If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                    qry += " and TSPL_REQUISITION_HEAD_Cancel_Data.Location  in   (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                End If
                If chkUserSelect.IsChecked AndAlso cbgUser.CheckedValue.Count > 0 Then
                        qry += " and TSPL_REQUISITION_HEAD_Cancel_Data.Cancel_By IN (" + clsCommon.GetMulcallString(cbgUser.CheckedValue) + ")"
                        'Else
                        '    qry += " and TSPL_REQUISITION_HEAD_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(arrSelectedUser) + ")"
                    End If
                    qry += " ORDER BY TSPL_REQUISITION_HEAD_Cancel_Data.Requisition_Date,TSPL_REQUISITION_HEAD_Cancel_Data.Requisition_Id "


                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), clsUserMgtCode.mbtnGatePass) = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing
                qry = "Select TSPL_RGP_HEAD_cancel_data.RGP_No as [Document Id],convert(varchar,TSPL_RGP_HEAD_cancel_data.RGP_Date ,103) as [Document Date]                
                    ,TSPL_RGP_HEAD_cancel_data.Location as [Location Code]
                    ,TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_RGP_HEAD_cancel_data.Created_By as [Created By]
                    ,convert(varchar,TSPL_RGP_HEAD_cancel_data.Created_Date,103) as [Created Date]
                ,Cancel_By as [Cancelled By],
					Cancel_On as [Cancelled Date],'' as Description
from TSPL_RGP_HEAD_cancel_data 
                    Left Outer Join TSPL_LOCATION_MASTER  on TSPL_RGP_HEAD_cancel_data.Location  =TSPL_LOCATION_MASTER.Location_Code 
                     WHERE"
                If rbtnCancelDate.IsChecked Then
                    qry += "convert(date,TSPL_RGP_HEAD_cancel_data.Cancel_On ,103) >= convert(date,'" + dtpFromDate.Value + "',103) 
                     And Convert(Date, TSPL_RGP_HEAD_cancel_data.Cancel_On,103) <= Convert(Date,'" + dtpToDate.Value + "',103) "
                Else
                    qry += "convert(date,TSPL_RGP_HEAD_cancel_data.RGP_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) 
                     And Convert(Date, TSPL_RGP_HEAD_cancel_data.RGP_Date,103) <= Convert(Date,'" + dtpToDate.Value + "',103) "
                End If


                If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                    qry += " and TSPL_RGP_HEAD_cancel_data.Location  in   (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                End If
                If chkUserSelect.IsChecked AndAlso cbgUser.CheckedValue.Count > 0 Then
                    qry += " and TSPL_RGP_HEAD_cancel_data.Cancel_By IN (" + clsCommon.GetMulcallString(cbgUser.CheckedValue) + ")"
                    'Else
                    '    qry += " and TSPL_RGP_HEAD_cancel_data.Created_By IN (" + clsCommon.GetMulcallString(arrSelectedUser) + ")"
                End If
                qry += " ORDER BY TSPL_RGP_HEAD_cancel_data.RGP_Date,TSPL_RGP_HEAD_cancel_data.RGP_No "
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
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
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
        rbtnDocumentDate.IsChecked = True
    End Sub

    Private Sub gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Try
            If clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), clsUserMgtCode.mbtnGRN) = CompairStringResult.Equal Then
                clsGRNHead.funGRNPrint(MyBase.Form_ID, True, clsCommon.myCDate(gv1.Rows(gv1.CurrentCell.RowIndex).Cells("Document Date").Value), clsCommon.myCstr(gv1.Rows(gv1.CurrentCell.RowIndex).Cells("Document ID").Value))
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), clsUserMgtCode.mbtnMRN) = CompairStringResult.Equal Then
                clsMRNHead.funMRNPrint(MyBase.Form_ID, True, clsCommon.myCDate(gv1.Rows(gv1.CurrentCell.RowIndex).Cells("Document Date").Value), clsCommon.myCstr(gv1.Rows(gv1.CurrentCell.RowIndex).Cells("Document ID").Value))
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Purchase Return") = CompairStringResult.Equal Then
                'clsPurchasReturnHead.funPRPrint(MyBase.Form_ID, True, clsCommon.myCDate(gv1.Rows(gv1.CurrentCell.RowIndex).Cells("Document Date").Value), clsCommon.myCstr(gv1.Rows(gv1.CurrentCell.RowIndex).Cells("Document ID").Value), Nothing, Nothing)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), clsUserMgtCode.mbtnPurchaseInvoice) = CompairStringResult.Equal Then

                Dim ItemCode As New List(Of String)
                'ItemCode = clsCommon.myCstr(clsDBFuncationality.("select Item_Code from TSPL_PI_DETAIL_Cancel_Data where PI_No ='" + clsCommon.myCstr(gv1.Rows(gv1.CurrentCell.RowIndex).Cells("Document ID").Value) + "'"))
                'doccodeShip = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_Code from TSPL_SD_SHIPMENT_HEAD_Cancel_Data  where Against_Booking_No ='" + clsCommon.myCstr(gv1.Rows(gv1.CurrentCell.RowIndex).Cells("Document ID").Value) + "'"))
                ItemCode.Add(clsDBFuncationality.getSingleValue("select Item_Code from TSPL_PI_DETAIL_Cancel_Data where PI_No ='" + clsCommon.myCstr(gv1.Rows(gv1.CurrentCell.RowIndex).Cells("Document ID").Value) + "'"))

                clsPurchaseInvoiceHead.funPIPrint(MyBase.Form_ID, True, clsCommon.myCDate(gv1.Rows(gv1.CurrentCell.RowIndex).Cells("Document Date").Value), clsCommon.myCstr(gv1.Rows(gv1.CurrentCell.RowIndex).Cells("Document ID").Value), clsCommon.GetMulcallString(ItemCode), clsCommon.myCstr(gv1.Rows(gv1.CurrentCell.RowIndex).Cells("Location Code").Value), clsCommon.myCstr(gv1.Rows(gv1.CurrentCell.RowIndex).Cells("Ref_No").Value), clsCommon.myCstr(gv1.Rows(gv1.CurrentCell.RowIndex).Cells("Vendor Code").Value), Nothing)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), clsUserMgtCode.mbtnPurchaseReturn) = CompairStringResult.Equal Then
                clsPurchasReturnHead.funPRPrint(MyBase.Form_ID, True, clsCommon.myCDate(gv1.Rows(gv1.CurrentCell.RowIndex).Cells("Document Date").Value), clsCommon.myCstr(gv1.Rows(gv1.CurrentCell.RowIndex).Cells("Document ID").Value), Nothing, Nothing)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), clsUserMgtCode.frmStoreRequistion) = CompairStringResult.Equal Then
                clsPurchasReturnHead.funStoreRequisitionPrint(MyBase.Form_ID, True, clsCommon.myCDate(gv1.Rows(gv1.CurrentCell.RowIndex).Cells("Document Date").Value), clsCommon.myCstr(gv1.Rows(gv1.CurrentCell.RowIndex).Cells("Document ID").Value))
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), clsUserMgtCode.mbtnIssueReturn) = CompairStringResult.Equal Then
                clsPurchasReturnHead.funIssueRetunPrint(MyBase.Form_ID, True, clsCommon.myCDate(gv1.Rows(gv1.CurrentCell.RowIndex).Cells("Document Date").Value), clsCommon.myCstr(gv1.Rows(gv1.CurrentCell.RowIndex).Cells("Document ID").Value))

            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), clsUserMgtCode.mbtnPurchaseRequistion) = CompairStringResult.Equal Then
                clsRequistionHead.funPurchaseReqPrint(MyBase.Form_ID, True, clsCommon.myCDate(gv1.Rows(gv1.CurrentCell.RowIndex).Cells("Document Date").Value), clsCommon.myCstr(gv1.Rows(gv1.CurrentCell.RowIndex).Cells("Document ID").Value), False, False)

            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), clsUserMgtCode.mbtnSRN) = CompairStringResult.Equal Then
                Dim SRNNO As New ArrayList()
                SRNNO.Add(clsDBFuncationality.getSingleValue("select Srn_no from tspl_srn_head_cancel_data where srn_no ='" + clsCommon.myCstr(gv1.Rows(gv1.CurrentCell.RowIndex).Cells("Document ID").Value) + "'"))

                clsSRNHead.funSRNPrint(MyBase.Form_ID, True, clsCommon.myCDate(gv1.Rows(gv1.CurrentCell.RowIndex).Cells("Document Date").Value), Nothing, Nothing, False, (SRNNO), Nothing, Nothing)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), clsUserMgtCode.mbtnGatePass) = CompairStringResult.Equal Then
                clsRGPHead.funRGPPrint(MyBase.Form_ID, True, clsCommon.myCDate(gv1.Rows(gv1.CurrentCell.RowIndex).Cells("Document Date").Value), clsCommon.myCstr(gv1.Rows(gv1.CurrentCell.RowIndex).Cells("Document ID").Value), False, Nothing, Nothing, False)

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class




