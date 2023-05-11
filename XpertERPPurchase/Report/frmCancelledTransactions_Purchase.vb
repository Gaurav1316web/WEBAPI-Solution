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
    End Sub
    Public Sub LoadModuleType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        dr = dt.NewRow()
        dr("Code") = "Purchase"
        dr("Name") = "Purchase"
        dt.Rows.Add(dr)

        cboModule.DataSource = dt
        cboModule.DisplayMember = "Name"
        cboModule.ValueMember = "Code"

    End Sub
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
            clsCommon.MyMessageBoxShow(ex.Message)
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
        Dim dt1 As DataTable = New DataTable()
        dt1.Columns.Add("Code", GetType(String))
        dt1.Columns.Add("Name", GetType(String))

        dr = dt1.NewRow()
        dr("Code") = "Store Requisition"
        dr("Name") = "Store Requisition"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Gate Received Note"
        dr("Name") = "Gate Received Note"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Material Received Note"
        dr("Name") = "Material Received Note"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Store Received Note"
        dr("Name") = "Store Received Note"
        dt1.Rows.Add(dr)


        dr = dt1.NewRow()
        dr("Code") = "Purchase Invoice"
        dr("Name") = "Purchase Invoice"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Purchase Return"
        dr("Name") = "Purchase Return"
        dt1.Rows.Add(dr)


        dr = dt1.NewRow()
        dr("Code") = "Issue/Return/Transfer"
        dr("Name") = "Issue/Return/Transfer"
        dt1.Rows.Add(dr)

        cboTransaction.DataSource = dt1
        cboTransaction.DisplayMember = "Name"
        cboTransaction.ValueMember = "Code"
    End Sub

    Private Sub cboModule_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboModule.SelectedIndexChanged
        LoadTrnsListOfSelectedModeule()
    End Sub

    Public Sub LoadTrnsListOfSelectedModeule()
        If clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Purchase") = CompairStringResult.Equal Then
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
        For i As Integer = 2 To count - 2
            Me.gv1.MasterTemplate.Columns(i).Width = 120
        Next i
        Me.gv1.MasterTemplate.Columns("Description").Width = 200    ''Last Column
        For j As Integer = 1 To count - 1
            Me.gv1.MasterTemplate.Columns(j).ReadOnly = True
        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
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
            Else
                arrSelectedUser = arrUser
            End If

            If clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Purchase") = CompairStringResult.Equal Then
                gv1.DataSource = Nothing
                FillPurchase()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub FillPurchase()
        If clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Gate Received Note") = CompairStringResult.Equal Then
            gv1.DataSource = Nothing
            qry = "Select TSPL_GRN_HEAD_Cancel_Data.GRN_NO as [Document Id],convert(varchar,TSPL_GRN_HEAD_Cancel_Data.GRN_date ,103) as [Document Date]
                    ,TSPL_GRN_HEAD_Cancel_Data.Against_Requisition as [Against Requisition],TSPL_GRN_HEAD_Cancel_Data.Against_PO as [Against PO] 
                    ,TSPL_GRN_HEAD_Cancel_Data.Bill_To_Location as [Location Code]
                    ,TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_GRN_HEAD_Cancel_Data.Created_By as [Created By]
                    ,convert(varchar,TSPL_GRN_HEAD_Cancel_Data.Created_Date,103) as [Created Date],TSPL_GRN_HEAD_Cancel_Data.DESCRIPTION as Description from TSPL_GRN_HEAD_Cancel_Data 
                    Left Outer Join TSPL_LOCATION_MASTER  on TSPL_GRN_HEAD_Cancel_Data.Bill_To_Location  =TSPL_LOCATION_MASTER.Location_Code 
                     WHERE  convert(date,TSPL_GRN_HEAD_Cancel_Data.GRN_date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) 
                     And Convert(Date, TSPL_GRN_HEAD_Cancel_Data.GRN_date,103) <= Convert(Date,'" + dtpToDate.Value + "',103) "

            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                qry += " and TSPL_GRN_HEAD_Cancel_Data.Bill_To_Location  in   (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If
            If chkUserSelect.IsChecked AndAlso cbgUser.CheckedValue.Count > 0 Then
                qry += " and TSPL_GRN_HEAD_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(cbgUser.CheckedValue) + ")"
            Else
                qry += " and TSPL_GRN_HEAD_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(arrSelectedUser) + ")"
            End If
            qry += " ORDER BY TSPL_GRN_HEAD_Cancel_Data.GRN_DATE,TSPL_GRN_HEAD_Cancel_Data.GRN_NO "
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Material Received Note") = CompairStringResult.Equal Then
            gv1.DataSource = Nothing
            qry = "Select TSPL_MRN_HEAD_Cancel_Data.MRN_NO as [Document Id],convert(varchar,TSPL_MRN_HEAD_Cancel_Data.MRN_date ,103) as [Document Date]
                    ,TSPL_MRN_HEAD_Cancel_Data.Against_PO as [Against PO] ,TSPL_MRN_HEAD_Cancel_Data.Against_GRN as [Against GRN]
                    ,TSPL_MRN_HEAD_Cancel_Data.Bill_To_Location as [Location Code]
                    ,TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_MRN_HEAD_Cancel_Data.Created_By as [Created By]
                    ,convert(varchar,TSPL_MRN_HEAD_Cancel_Data.Created_Date,103) as [Created Date],TSPL_MRN_HEAD_Cancel_Data.DESCRIPTION as Description from TSPL_MRN_HEAD_Cancel_Data 
                    Left Outer Join TSPL_LOCATION_MASTER  on TSPL_MRN_HEAD_Cancel_Data.Bill_To_Location  =TSPL_LOCATION_MASTER.Location_Code 
                     WHERE  convert(date,TSPL_MRN_HEAD_Cancel_Data.MRN_date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) 
                     And Convert(Date, TSPL_MRN_HEAD_Cancel_Data.MRN_date,103) <= Convert(Date,'" + dtpToDate.Value + "',103) "

            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                qry += " and TSPL_MRN_HEAD_Cancel_Data.Bill_To_Location  in   (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If
            If chkUserSelect.IsChecked AndAlso cbgUser.CheckedValue.Count > 0 Then
                qry += " and TSPL_MRN_HEAD_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(cbgUser.CheckedValue) + ")"
            Else
                qry += " and TSPL_MRN_HEAD_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(arrSelectedUser) + ")"
            End If
            qry += " ORDER BY TSPL_MRN_HEAD_Cancel_Data.MRN_DATE,TSPL_MRN_HEAD_Cancel_Data.MRN_NO "
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Store Received Note") = CompairStringResult.Equal Then
            gv1.DataSource = Nothing
            qry = "Select TSPL_SRN_HEAD_Cancel_Data.SRN_NO as [Document Id],convert(varchar,TSPL_SRN_HEAD_Cancel_Data.SRN_date ,103) as [Document Date]
                    ,TSPL_SRN_HEAD_Cancel_Data.Against_PO as [Against PO] ,TSPL_SRN_HEAD_Cancel_Data.Against_GRN as [Against GRN]
                    ,TSPL_SRN_HEAD_Cancel_Data.Against_MRN as [Against MRN],TSPL_SRN_HEAD_Cancel_Data.Bill_To_Location as [Location Code]
                    ,TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_SRN_HEAD_Cancel_Data.Created_By as [Created By]
                    ,convert(varchar,TSPL_SRN_HEAD_Cancel_Data.Created_Date,103) as [Created Date],TSPL_SRN_HEAD_Cancel_Data.DESCRIPTION as Description from TSPL_SRN_HEAD_Cancel_Data 
                    Left Outer Join TSPL_LOCATION_MASTER  on TSPL_SRN_HEAD_Cancel_Data.Bill_To_Location  =TSPL_LOCATION_MASTER.Location_Code 
                     WHERE  convert(date,TSPL_SRN_HEAD_Cancel_Data.SRN_date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) 
                     And Convert(Date, TSPL_SRN_HEAD_Cancel_Data.SRN_date,103) <= Convert(Date,'" + dtpToDate.Value + "',103) "

            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                qry += " and TSPL_SRN_HEAD_Cancel_Data.Bill_To_Location  in   (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If
            If chkUserSelect.IsChecked AndAlso cbgUser.CheckedValue.Count > 0 Then
                qry += " and TSPL_SRN_HEAD_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(cbgUser.CheckedValue) + ")"
            Else
                qry += " and TSPL_SRN_HEAD_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(arrSelectedUser) + ")"
            End If
            qry += " ORDER BY TSPL_SRN_HEAD_Cancel_Data.SRN_DATE,TSPL_SRN_HEAD_Cancel_Data.SRN_NO "

        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Store Requisition") = CompairStringResult.Equal Then
            gv1.DataSource = Nothing
            qry = "Select TSPL_REQUISITION_HEAD_Cancel_Data.Requisition_Id as [Document Id],convert(varchar,TSPL_REQUISITION_HEAD_Cancel_Data.Requisition_Date ,103) as [Document Date]
                    ,TSPL_REQUISITION_HEAD_Cancel_Data.Location as [Location Code]
                    ,TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_REQUISITION_HEAD_Cancel_Data.Created_By as [Created By]
                    ,convert(varchar,TSPL_REQUISITION_HEAD_Cancel_Data.Created_Date,103) as [Created Date]
                     ,TSPL_REQUISITION_HEAD_Cancel_Data.Dept as [Department Code]
                     ,TSPL_REQUISITION_HEAD_Cancel_Data.Dept_Desc as [Department Name]
                    ,TSPL_REQUISITION_HEAD_Cancel_Data.Request_By as [Request By]
                    ,TSPL_REQUISITION_HEAD_Cancel_Data.DESCRIPTION as Description from TSPL_REQUISITION_HEAD_Cancel_Data 
                    Left Outer Join TSPL_LOCATION_MASTER  on TSPL_REQUISITION_HEAD_Cancel_Data.Location  =TSPL_LOCATION_MASTER.Location_Code 
                     WHERE  convert(date,TSPL_REQUISITION_HEAD_Cancel_Data.Requisition_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) 
                     And Convert(Date, TSPL_REQUISITION_HEAD_Cancel_Data.Requisition_Date,103) <= Convert(Date,'" + dtpToDate.Value + "',103) "

            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                qry += " and TSPL_REQUISITION_HEAD_Cancel_Data.Location  in   (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If
            If chkUserSelect.IsChecked AndAlso cbgUser.CheckedValue.Count > 0 Then
                qry += " and TSPL_REQUISITION_HEAD_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(cbgUser.CheckedValue) + ")"
            Else
                qry += " and TSPL_REQUISITION_HEAD_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(arrSelectedUser) + ")"
            End If
            qry += " ORDER BY TSPL_REQUISITION_HEAD_Cancel_Data.Requisition_Date,TSPL_REQUISITION_HEAD_Cancel_Data.Requisition_Id "

        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Issue/Return/Transfer") = CompairStringResult.Equal Then
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
                    ,TSPL_IssueReturn_HEAD_Cancel_Data.Remarks as Description from TSPL_IssueReturn_HEAD_Cancel_Data 
                    Left Outer Join TSPL_LOCATION_MASTER  on TSPL_IssueReturn_HEAD_Cancel_Data.From_Location  =TSPL_LOCATION_MASTER.Location_Code 
                     WHERE  convert(date,TSPL_IssueReturn_HEAD_Cancel_Data.Doc_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) 
                     And Convert(Date, TSPL_IssueReturn_HEAD_Cancel_Data.Doc_Date,103) <= Convert(Date,'" + dtpToDate.Value + "',103) "

            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                qry += " and TSPL_IssueReturn_HEAD_Cancel_Data.From_Location  in   (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If
            If chkUserSelect.IsChecked AndAlso cbgUser.CheckedValue.Count > 0 Then
                qry += " and TSPL_IssueReturn_HEAD_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(cbgUser.CheckedValue) + ")"
            Else
                qry += " and TSPL_IssueReturn_HEAD_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(arrSelectedUser) + ")"
            End If
            qry += " ORDER BY TSPL_IssueReturn_HEAD_Cancel_Data.Doc_Date,TSPL_IssueReturn_HEAD_Cancel_Data.Doc_No "

        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Purchase Invoice") = CompairStringResult.Equal Then
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
                    ,TSPL_PI_HEAD_Cancel_Data.Description as Description from TSPL_PI_HEAD_Cancel_Data 
                    Left Outer Join TSPL_LOCATION_MASTER  on TSPL_PI_HEAD_Cancel_Data.Bill_To_Location  =TSPL_LOCATION_MASTER.Location_Code 
                     WHERE  convert(date,TSPL_PI_HEAD_Cancel_Data.PI_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) 
                     And Convert(Date, TSPL_PI_HEAD_Cancel_Data.PI_Date,103) <= Convert(Date,'" + dtpToDate.Value + "',103) "

            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                qry += " and TSPL_PI_HEAD_Cancel_Data.Bill_To_Location  in   (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If
            If chkUserSelect.IsChecked AndAlso cbgUser.CheckedValue.Count > 0 Then
                qry += " and TSPL_PI_HEAD_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(cbgUser.CheckedValue) + ")"
            Else
                qry += " and TSPL_PI_HEAD_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(arrSelectedUser) + ")"
            End If
            qry += " ORDER BY TSPL_PI_HEAD_Cancel_Data.PI_Date,TSPL_PI_HEAD_Cancel_Data.PI_No "

        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Purchase Return") = CompairStringResult.Equal Then
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
                    ,TSPL_PR_HEAD_Cancel_Data.Description as Description from TSPL_PR_HEAD_Cancel_Data 
                    Left Outer Join TSPL_LOCATION_MASTER  on TSPL_PR_HEAD_Cancel_Data.Bill_To_Location  =TSPL_LOCATION_MASTER.Location_Code 
                     WHERE  convert(date,TSPL_PR_HEAD_Cancel_Data.PR_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) 
                     And Convert(Date, TSPL_PR_HEAD_Cancel_Data.PR_Date,103) <= Convert(Date,'" + dtpToDate.Value + "',103) "

            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                qry += " and TSPL_PR_HEAD_Cancel_Data.Bill_To_Location  in   (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If
            If chkUserSelect.IsChecked AndAlso cbgUser.CheckedValue.Count > 0 Then
                qry += " and TSPL_PR_HEAD_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(cbgUser.CheckedValue) + ")"
            Else
                qry += " and TSPL_PR_HEAD_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(arrSelectedUser) + ")"
            End If
            qry += " ORDER BY TSPL_PR_HEAD_Cancel_Data.PR_Date,TSPL_PR_HEAD_Cancel_Data.PR_No "

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
End Class




