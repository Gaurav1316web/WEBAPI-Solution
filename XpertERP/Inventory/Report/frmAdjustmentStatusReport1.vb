Imports common


Public Class FrmAdjustmentStatusReport1
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FrmAdjustmentStatusReport1)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnprint, "Press Ctrl+P Print the Report")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+R Reset the Window")
        btnprint.Visible = MyBase.isPrintFlag
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub FrmAdjustmentStatusReport1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        dtpstart.Focus()
        LoadType()
        LoadLocation()
        chktypeAll.IsChecked = True
        chkLocationAll.IsChecked = True
        reset()
    End Sub
    Sub LoadType()
        Dim dt As DataTable = New DataTable()
        dt = GetItemType()
        ' Dim qry As String = "select CUST_CATEGORY_CODE,CUST_CATEGORY_DESC from TSPL_CUSTOMER_CATEGORY_MASTER order by CUST_CATEGORY_CODE "
        'cbgtype.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgtype.DataSource = dt
        cbgtype.ValueMember = "Code"
        cbgtype.DisplayMember = "Code"
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
    Sub LoadItem()
        Dim qry As String = " select item_code ,item_Desc  from TSPL_ITEM_MASTER order by Item_Code "
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem.ValueMember = "item_code"
        cbgItem.DisplayMember = "item_Desc"
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
    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        reset()
    End Sub
    Public Sub reset()
        dtpstart.Value = clsCommon.GetDateWithStartTime(clsCommon.GETSERVERDATE())
        dtpend.Value = clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE())
        'dtpStarttime.Value = DateTime.Now
        ' dtpendtime.Value = DateTime.Now
        LoadType()
        LoadItem()
        chktypeAll.IsChecked = True
        chkLocationAll.IsChecked = True
        chkIemAll.IsChecked = True

    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        funPrint()
    End Sub
    Private Sub funPrint()
        Dim fromdate As String = clsCommon.GetPrintDate(dtpstart.Value, "dd/MMM/yyyy hh:mm tt")
        Dim enddate As String = clsCommon.GetPrintDate(dtpend.Value, "dd/MMM/yyyy hh:mm tt")
        If chktypeSelect.IsChecked AndAlso cbgtype.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Type")
            Return
        End If

        If chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Location")
            Return
        End If
        If (dtpstart.Value > dtpend.Value) Then
            common.clsCommon.MyMessageBoxShow("'Start Date' Cann't be more than 'End date'")
            'ElseIf (dtpStarttime.Value > dtpendtime.Value) Then
            '    common.clsCommon.MyMessageBoxShow("'Start Time' Cann't be more than 'End Time'")
        Else


            Dim filter As String = ""
            If chktypeSelect.IsChecked AndAlso chktypeSelect.IsChecked Then
                filter = " and TSPL_ADJUSTMENT_DETAIL.Unit_Code in  (" + clsCommon.GetMulcallString(cbgtype.CheckedValue) + ")"
            End If

            If chkItemSelect.IsChecked = True Then
                filter += " and TSPL_ADJUSTMENT_DETAIL.Item_Code IN  (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ")  "
            End If
            Dim strPost As String = "2=2"
            If optPosted.IsChecked Then
                strPost = " TSPL_ADJUSTMENT_HEADER.posted = 'N'"
            End If

            If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count = 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one Location")
                Return
            ElseIf chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0 Then
                filter += " and TSPL_ADJUSTMENT_HEADER.Loc_Code in ( Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "))"
            End If

            Dim LocNcmpAdd As String = ""
            If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count = 1 Then
                LocNcmpAdd = " (Select MAX(TSPL_LOCATION_MASTER.Add1 + case When TSPL_LOCATION_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.Add2, 103) End + Case When TSPL_LOCATION_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_LOCATION_MASTER.Add3,103) end + case When TSPL_LOCATION_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.City_Code, 103) end+ Case When TSPL_LOCATION_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_LOCATION_MASTER.State) end +  Case When TSPL_LOCATION_MASTER.Pin_Code='' Then '' Else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.Pin_Code, 103)  end) as Address from TSPL_LOCATION_MASTER WHERE Loc_Segment_Code  IN (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "))"
            Else
                LocNcmpAdd = " ISNULL(tspl_company_Master.ADD1,'')"
            End If
            Try
                Dim frmCRV As New frmCrystalReportViewer()
                If RadioBtnDetail.IsChecked = True Then
                    Dim qry As String = "SELECT TSPL_ADJUSTMENT_HEADER.Adjustment_No,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,convert(decimal(18,2),(TSPL_ADJUSTMENT_DETAIL.Item_Quantity/TSPL_ITEM_UOM_DETAIL .Conversion_Factor )) as [Adjustment Quantity],CASE when TSPL_ADJUSTMENT_DETAIL.Adjustment_Type='BI' then 'Both Incerease' else CASE when TSPL_ADJUSTMENT_DETAIL.Adjustment_Type='BD' then 'Both Decrease' else CASE when TSPL_ADJUSTMENT_DETAIL.Adjustment_Type='QI' then 'Quantity Increase' else CASE when TSPL_ADJUSTMENT_DETAIL.Adjustment_Type='QD' then 'Quantity Decrease' end end end end as Adjustment_Type, TSPL_ADJUSTMENT_DETAIL.Item_Cost as [Amount],  TSPL_ADJUSTMENT_HEADER.Document_No as [DocumentNo], TSPL_ADJUSTMENT_DETAIL.Item_Code, (case when ( TSPL_ADJUSTMENT_HEADER.Posted='Y') then 'Posted' else 'Pending' end) as Posted  ,TSPL_ADJUSTMENT_HEADER.Customer_CODE , CASE when TSPL_ADJUSTMENT_HEADER.ItemType='FT' then 'Finished Trade' else CASE when TSPL_ADJUSTMENT_HEADER.ItemType='E' then 'Empty' else CASE when TSPL_ADJUSTMENT_HEADER.ItemType='OT' then 'Others' else CASE when TSPL_ADJUSTMENT_HEADER.ItemType='FM' then 'Finished Manufacturing' end end end end  as [ItemType]  , TSPL_COMPANY_MASTER.Comp_Name as compname, TSPL_COMPANY_MASTER.Logo_Img, TSPL_COMPANY_MASTER.Logo_Img2 , " + LocNcmpAdd + " as address, CONVERT(date, '" & dtpstart.Value & "', 103) AS startdate, CONVERT(date, '" & dtpend.Value & "', 103) AS enddate, substring('" & fromdate & "',12,12) as startTime, substring('" & enddate & "',12,12) as endTime,TSPL_ADJUSTMENT_HEADER.Loc_Code as Location   FROM  TSPL_ADJUSTMENT_HEADER LEFT OUTER JOIN TSPL_ADJUSTMENT_DETAIL ON TSPL_ADJUSTMENT_HEADER.Adjustment_No = TSPL_ADJUSTMENT_DETAIL.Adjustment_No LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_ADJUSTMENT_HEADER.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL .Item_Code =TSPL_ADJUSTMENT_DETAIL .Item_Code and TSPL_ITEM_UOM_DETAIL .UOM_Code   =TSPL_ADJUSTMENT_DETAIL .Unit_Code where " + strPost + " " & _
                       " And TSPL_ADJUSTMENT_HEADER.EntryDateTime >= ( '" & fromdate & "') AND TSPL_ADJUSTMENT_HEADER.EntryDateTime <= ( '" & enddate & "')" + filter + " "
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        common.clsCommon.MyMessageBoxShow("No Record Found")
                    Else
                        dt = clsDBFuncationality.GetDataTable(qry)
                        frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, "crptAdjustmentStatusDetailReport", "Adjustment Status Report")
                    End If
                ElseIf RadioBtnSummary.IsChecked = True Then
                    Dim qry As String = "select xxxx.*,TSPL_COMPANY_MASTER.Comp_Code as [CompNamae],TSPL_COMPANY_MASTER.Comp_Name as [compname], " + LocNcmpAdd + " as [address],TSPL_COMPANY_MASTER .Logo_Img ,TSPL_COMPANY_MASTER .Logo_Img2  from ( " & _
                    " select MAX(Adjustment_No) as [Adjustment_No],MAX(Adjustment_Date) as [Adjustment_Date], MAX(DocumentNo) as [DocumentNo] , max(Posted)as [Posted], max(Customer_CODE)as [Customer_CODE],Max(Location) as Location, MAX(ItemType) as [ItemType], max(startdate) as [startdate], max(enddate) as [EndDate], max(startTime) as [StartTime], max(endTime) as [EndTime] from  (SELECT TSPL_ADJUSTMENT_HEADER.Adjustment_No as [Adjustment_No], TSPL_ADJUSTMENT_HEADER.Adjustment_Date as [Adjustment_Date],  " & _
                    " TSPL_ADJUSTMENT_HEADER.Created_time as [Created_time], (case when ( TSPL_ADJUSTMENT_HEADER.Posted='Y') then 'Posted' else 'Pending' end) as Posted ,CASE when TSPL_ADJUSTMENT_HEADER.ItemType='FT' then 'Finished Trade' else CASE when TSPL_ADJUSTMENT_HEADER.ItemType='E' then 'Empty' else CASE when TSPL_ADJUSTMENT_HEADER.ItemType='OT' then 'Others' else CASE when TSPL_ADJUSTMENT_HEADER.ItemType='FM' then 'Finished Manufacturing' end end end end  as [ItemType] ,TSPL_ADJUSTMENT_HEADER.Customer_CODE , " & _
                    " TSPL_ADJUSTMENT_HEADER.Document_No as [DocumentNo], CONVERT(date, '" + dtpstart.Value.Date + "', 103) AS [startdate], CONVERT(date, '" + dtpend.Value.Date + "', 103) AS [enddate],  substring('" + fromdate + "',12,12) as [startTime], substring('" + enddate + "',12,12) as [endTime] , TSPL_ADJUSTMENT_HEADER.Comp_Code  AS [COMPCODE], TSPL_ADJUSTMENT_HEADER.Loc_Code as Location FROM TSPL_ADJUSTMENT_HEADER  " & _
                    " LEFT OUTER JOIN TSPL_ADJUSTMENT_DETAIL ON TSPL_ADJUSTMENT_HEADER.Adjustment_No = TSPL_ADJUSTMENT_DETAIL.Adjustment_No where " + strPost + "  and TSPL_ADJUSTMENT_HEADER.EntryDateTime >= ('" + fromdate + "') AND " & _
                    " TSPL_ADJUSTMENT_HEADER.EntryDateTime <=  ('" + enddate + "') " + filter + " " & _
                    " )xxx group by Adjustment_No " & _
                    " )xxxx " & _
                    " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code ='" + objCommonVar.CurrentCompanyCode + "'" & _
                    " order By Created_Date "
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        common.clsCommon.MyMessageBoxShow("No Record Found")
                    Else
                        dt = clsDBFuncationality.GetDataTable(qry)
                        frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, "crptAdjustmentStatusReport", "Adjustment Status Report")
                    End If
                End If
                frmCRV = Nothing
            Catch ex As Exception
                common.clsCommon.MyMessageBoxShow(ex.Message)
            End Try
        End If
    End Sub

    Private Sub chktypeAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chktypeAll.ToggleStateChanged
        cbgtype.Enabled = Not chktypeAll.IsChecked
    End Sub

    Private Sub chktypeSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chktypeSelect.ToggleStateChanged
        cbgtype.Enabled = True
    End Sub

    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgLocation.Enabled = False
    End Sub

    Private Sub chkLocationSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationSelect.ToggleStateChanged
        cbgLocation.Enabled = True
    End Sub

    Private Sub chkIemAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkIemAll.ToggleStateChanged
        cbgItem.Enabled = Not chkIemAll.IsChecked
    End Sub

    Private Sub chkItemSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkItemSelect.ToggleStateChanged
        cbgItem.Enabled = True
    End Sub

    Private Sub FrmAdjustmentStatusReport1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.P AndAlso MyBase.isModifyFlag Then
            funPrint()

        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()

        ElseIf e.Alt And e.KeyCode = Keys.R Then
            reset()
        End If
    End Sub
End Class
