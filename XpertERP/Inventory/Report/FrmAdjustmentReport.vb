Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common

Public Class FrmAdjustmentReport
    Inherits FrmMainTranScreen
    Dim dt As New DataTable
    Dim qry As String
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FrmAdjustmentReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        ButtonToolTip.SetToolTip(btnClose1, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnprint1, "Press Ctrl+P Print the Report")
        ButtonToolTip.SetToolTip(btnreset1, "Press Alt+R Reset the Window")
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
        btnprint1.Visible = MyBase.isPrintFlag
    End Sub

    Private Sub FrmAdjustmentReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.P Then
            FunPrint()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            Reset()
        End If
    End Sub
    Private Sub FrmAdjustmentReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtpFromdate1.Value = clsCommon.GETSERVERDATE()
        dtpToDate1.Value = clsCommon.GETSERVERDATE()
        LoadLocation()
        chkall.IsChecked = True
        chk_Location_All.IsChecked = True
        rdbtnAdjustment.IsChecked = True
        SetUserMgmtNew()
        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If

    End Sub

    Public Sub LoadDocumentReceipt()
        cbgDoc.DataSource.Clear()
        Dim qry As String = "select TSPL_ADJUSTMENT_HEADER.Adjustment_No AS [Code],TSPL_ADJUSTMENT_HEADER.Adjustment_Date as [Date]  from TSPL_ADJUSTMENT_DETAIL left outer join TSPL_ADJUSTMENT_HEADER   on TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No where TSPL_ADJUSTMENT_HEADER .ItemType ='E' and TSPL_ADJUSTMENT_DETAIL .Adjustment_Line_No =1 and TSPL_ADJUSTMENT_DETAIL .Adjustment_Type ='BI'  "
        cbgDoc.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgDoc.ValueMember = "Code"
        cbgDoc.DisplayMember = "Date"
    End Sub
    Public Sub LoadDocIssue()
        cbgDoc.DataSource.Clear()
        Dim qry As String = "select TSPL_ADJUSTMENT_HEADER.Adjustment_No AS [Code],TSPL_ADJUSTMENT_HEADER.Adjustment_Date as [Date]  from TSPL_ADJUSTMENT_DETAIL left outer join TSPL_ADJUSTMENT_HEADER   on TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No where TSPL_ADJUSTMENT_HEADER .ItemType ='E' and TSPL_ADJUSTMENT_DETAIL .Adjustment_Line_No =1 and TSPL_ADJUSTMENT_DETAIL .Adjustment_Type ='BD' "
        cbgDoc.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgDoc.ValueMember = "Code"
        cbgDoc.DisplayMember = "Date"
    End Sub
    Public Sub LoadDocAdjustmen()
        Dim qry As String = " select TSPL_ADJUSTMENT_HEADER.Adjustment_No AS [Code],TSPL_ADJUSTMENT_HEADER.Adjustment_Date as [Date]  from TSPL_ADJUSTMENT_DETAIL left outer join TSPL_ADJUSTMENT_HEADER   on TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No where TSPL_ADJUSTMENT_HEADER .ItemType <>'E' and TSPL_ADJUSTMENT_DETAIL .Adjustment_Line_No =1  "
        cbgDoc.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgDoc.ValueMember = "Code"
        cbgDoc.DisplayMember = "Date"
    End Sub
    Public Sub LoadLocation()
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        'cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        'cbgLocation.ValueMember = "Code"
        'cbgLocation.DisplayMember = "Description"
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub
    Private Sub rdbtnAdjustment_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdbtnAdjustment.ToggleStateChanged, rdbtnReceipt.ToggleStateChanged, rdbtnIssue.ToggleStateChanged
        Dim qry As String = ""
        If rdbtnAdjustment.IsChecked = True Then
            LoadDocAdjustmen()
        ElseIf rdbtnIssue.IsChecked = True Then
            LoadDocIssue()
        ElseIf rdbtnReceipt.IsChecked = True Then
            LoadDocumentReceipt()
        End If
    End Sub
    Private Sub btnprint1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint1.Click
        FunPrint()
    End Sub
    Public Sub FunPrint()

        Dim ArrDoc As ArrayList = cbgDoc.CheckedValue
        Dim ArrLoc As ArrayList = cbgLocation.CheckedValue
        Try
            Dim frmCRV As New frmCrystalReportViewer()
            If rdbtnAdjustment.IsChecked = True Then
                qry = "select Head.Adjustment_No as [Adjustment No], Head.Adjustment_Date as [Adjustment Date],Head.Description as [Description], Head.Reference_Document AS [Reference Document], Head.Document_No as [Document No],detail.Item_Code as [Item Code], detail.Item_Description as [Item Description],head.Loc_Code as [Locaton Code], Location.Location_Desc as [Location], CASE when detail.Adjustment_Type='BI' then 'Both Increase' else CASE when detail.Adjustment_Type='BD' then 'Both Decrease' else CASE when detail.Adjustment_Type='QI' then 'Quality Increase' else CASE when detail.Adjustment_Type='QD' then 'Quality Decrease' else CASE when detail.Adjustment_Type='CI' then 'Cost Increase' else CASE when detail.Adjustment_Type='CD' then 'Cost Decrease' end end end end end end  as [Adjustment Type],detail.Item_Quantity as [Quantity], detail.Item_Cost as [Cost Adjustment], detail.Breakage as [Breakage Quantity],detail.Breakage_Cost as [Breakage Cost], detail.mrp as [MRP], detail.Unit_Code as [UOM], detail.MFG_Date as [MFG Date],detail.Batch_No as [Batch No], detail.Expiry_Date  as [Exp. Date],Location.Location_Desc as [Location], TSPL_COMPANY_MASTER.Comp_Name as compname,TSPL_COMPANY_MASTER.Logo_Img, TSPL_COMPANY_MASTER.Logo_Img2, (Location.Add1+(case when len(Location.Add2)>0 then ', 'else '' end )+Location.Add2+(case when len(Location.Add3)>0 then ', 'else '' end )+Location.Add3+(case when len(Location.Add4)>0 then ', 'else '' end )+Location.Add4+(case when len(Location.City_Code )>0 then ', 'else '' end ) + '' +TSPL_TDS_STATE_MASTER.State_Name ) as [Add1] from TSPL_ADJUSTMENT_HEADER as Head left outer join TSPL_ADJUSTMENT_DETAIL as detail on head.Adjustment_No = detail.Adjustment_No Left Outer JOIN TSPL_COMPANY_MASTER ON Head.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code left Outer join TSPL_LOCATION_MASTER as Location on detail.Location_Code=Location.Location_Code Left Outer Join TSPL_TDS_STATE_MASTER on Location .State=TSPL_TDS_STATE_MASTER.State_Code  where Convert(Date,head.Adjustment_Date,103) >=Convert(Date,'" + dtpFromdate1.Value + "',103) and Convert(Date,head.Adjustment_Date,103) <=Convert(Date,'" + dtpToDate1.Value + "',103)and Head  .ItemType <>'E'"
                If chk_Doc_Select.IsChecked AndAlso cbgDoc.CheckedValue.Count = 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select atleast one Documnet Number")
                    Return
                ElseIf cbgDoc.CheckedValue.Count > 0 Then
                    qry += " and Head.Adjustment_No in (" + clsCommon.GetMulcallString(ArrDoc) + ")  "

                End If
                If chk_Location_Select.IsChecked AndAlso cbgLocation.CheckedValue.Count = 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select atleast one Location Number")
                    Return
                ElseIf cbgLocation.CheckedValue.Count > 0 Then
                    qry += " and head.Loc_Code in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(ArrLoc) + ")) "
                End If

                qry += " order by detail.Adjustment_Line_No "
                dt = clsDBFuncationality.GetDataTable(qry)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("No Record Found")
                Else
                    frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, "crptAdjustmentThorughReportScreen", "Adjustment Detail")
                End If
            ElseIf rdbtnIssue.IsChecked = True Then
                qry = "select TSPL_ADJUSTMENT_HEADER.Adjustment_No,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,TSPL_ADJUSTMENT_HEADER.Customer_CODE,TSPL_ADJUSTMENT_HEADER.Customer_NAME,TSPL_CUSTOMER_MASTER.Lst_No,TSPL_ADJUSTMENT_DETAIL.Item_Code,TSPL_ADJUSTMENT_DETAIL.Item_Description,TSPL_ADJUSTMENT_DETAIL.Item_Quantity,TSPL_ADJUSTMENT_DETAIL.mrp,TSPL_ADJUSTMENT_DETAIL.Item_Cost,TSPL_ADJUSTMENT_HEADER.Vehicle_No,TSPL_ADJUSTMENT_HEADER .Loc_Code  " & _
                    " from TSPL_ADJUSTMENT_DETAIL" & _
                    " left outer join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No" & _
                    " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_ADJUSTMENT_HEADER.Customer_CODE" & _
                    " where  Convert(Date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) >=Convert(Date,'" + dtpFromdate1.Value + "',103) and Convert(Date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) <=Convert(Date,'" + dtpToDate1.Value + "',103)and TSPL_ADJUSTMENT_DETAIL.Adjustment_Type ='BD' "
                If chk_Doc_Select.IsChecked AndAlso cbgDoc.CheckedValue.Count = 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select atleast one Documnet Number")
                    Return
                ElseIf cbgDoc.CheckedValue.Count > 0 Then
                    qry += " and TSPL_ADJUSTMENT_HEADER.Adjustment_No in (" + clsCommon.GetMulcallString(ArrDoc) + ")  "

                End If
                If chk_Location_Select.IsChecked AndAlso cbgLocation.CheckedValue.Count = 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select atleast one Location Number")
                    Return
                ElseIf cbgLocation.CheckedValue.Count > 0 Then
                    qry += " and TSPL_ADJUSTMENT_HEADER .Loc_Code in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(ArrLoc) + ")) "
                End If

                qry += "and TSPL_ADJUSTMENT_DETAIL.Adjustment_Type ='BD'and TSPL_ADJUSTMENT_HEADER.ItemType  ='E' "
                qry += " ORDER by TSPL_ADJUSTMENT_DETAIL.Adjustment_Line_No"
                dt = clsDBFuncationality.GetDataTable(qry)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("No Record Found")
                Else
                    frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, "crptAdjustmentCustomIssueThorughReportScreen", "Adjustment Detail")
                End If
            ElseIf rdbtnReceipt.IsChecked = True Then
                Dim strReportName As String = "EMPTY RECEIPT CHALLAN"
                Dim strACaption As String = "From"
                Dim strIssueCaption As String = "Empty Receipt"
                Dim strDateCaption As String = "Receipt Date"
                qry = "select Adjustment_No,MAX(Adjustment_Date) as Adjustment_Date,MAX(Customer_NAME) as Customer_NAME,MAX(Description) as Description,Item_Code,MAX(Item_Description) as Item_Desc, SUM(ISNULL( FCS,0)) as FCS, SUM(isnull(FBS,0))as FBS, SUM(ISNULL( FSH,0)) as FSH, SUM(ISNULL( ECS,0)) as ECS, SUM(ISNULL( EBS,0)) as EBS, SUM(Leak_Qty) as HF,SUM(Breakage) as Burst,SUM(Short_Qty) as Short,'" + strReportName + "' as ReportName,'" + strACaption + "' as ACaption,'" + strIssueCaption + "' as EmptyCaption,'" + strDateCaption + "' as DateCaption,max(SalesManName) as SalesManName,max(Challan_No) as Challan_No,max(Challan_date) as Challan_date,max(Vehicle_No) as Vehicle_No,max(Loc_Code)as Loc_Code from(" & _
                "select TSPL_ADJUSTMENT_HEADER.Adjustment_No,TSPL_ADJUSTMENT_HEADER.Adjustment_Date ,TSPL_ADJUSTMENT_HEADER.Customer_NAME,TSPL_ADJUSTMENT_HEADER.Description ,TSPL_ADJUSTMENT_DETAIL.Item_Code,TSPL_ADJUSTMENT_DETAIL.Item_Description,TSPL_ADJUSTMENT_DETAIL.Unit_Code,case when TSPL_ADJUSTMENT_DETAIL.Unit_Code='FC' then Item_Quantity end as FCS, case when TSPL_ADJUSTMENT_DETAIL.Unit_Code='FB' then Item_Quantity end as FBS, case when TSPL_ADJUSTMENT_DETAIL.Unit_Code='SH' then Item_Quantity end as FSH, case when TSPL_ADJUSTMENT_DETAIL.Unit_Code='EC' then Item_Quantity end as ECS,case when TSPL_ADJUSTMENT_DETAIL.Unit_Code='EB' then Item_Quantity end as EBS, 0 as Leak_Qty,TSPL_ADJUSTMENT_DETAIL.Breakage,0 As Short_Qty,TSPL_ADJUSTMENT_HEADER.EMP_NAME as SalesManName,TSPL_ADJUSTMENT_HEADER.Challan_No,TSPL_ADJUSTMENT_HEADER.Challan_date,TSPL_ADJUSTMENT_HEADER.Vehicle_No,TSPL_ADJUSTMENT_HEADER .Loc_Code from TSPL_ADJUSTMENT_DETAIL left outer join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Adjustment_No= TSPL_ADJUSTMENT_DETAIL.Adjustment_No where  Convert(Date,TSPL_ADJUSTMENT_HEADER .Adjustment_Date,103) >=Convert(Date,'" + dtpFromdate1.Value + "',103) and Convert(Date,TSPL_ADJUSTMENT_HEADER .Adjustment_Date,103) <=Convert(Date,'" + dtpToDate1.Value + "',103)"

                If chk_Doc_Select.IsChecked AndAlso cbgDoc.CheckedValue.Count = 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select atleast one Documnet Number")
                    Return
                ElseIf cbgDoc.CheckedValue.Count > 0 Then
                    qry += " and TSPL_ADJUSTMENT_HEADER.Adjustment_No in (" + clsCommon.GetMulcallString(ArrDoc) + ")  "

                End If
                If chk_Location_Select.IsChecked AndAlso cbgLocation.CheckedValue.Count = 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select atleast one Location Number")
                    Return
                ElseIf cbgLocation.CheckedValue.Count > 0 Then
                    qry += " and TSPL_ADJUSTMENT_HEADER .Loc_Code in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(ArrLoc) + ")) "
                End If
                qry += " and TSPL_ADJUSTMENT_DETAIL.Adjustment_Type ='BI'and TSPL_ADJUSTMENT_HEADER.ItemType  ='E' "
                qry += ")xxx group by Adjustment_No,Item_Code order by Item_Desc"
                dt = clsDBFuncationality.GetDataTable(qry)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("No Record Found")
                Else
                    frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, "crptAdjustmentCustomReceiptThorughReportScreen", "Adjustment Detail")
                End If
            End If
            frmCRV = Nothing
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Private Sub btnClose1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose1.Click
        Me.Close()
    End Sub

    Private Sub chkall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkall.ToggleStateChanged
        cbgDoc.Enabled = Not chkall.Enabled = True
    End Sub

    Private Sub chk_Location_All_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chk_Location_All.ToggleStateChanged
        cbgLocation.Enabled = Not chk_Location_All.Enabled = True
    End Sub

    Private Sub chk_Doc_Select_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chk_Doc_Select.ToggleStateChanged
        cbgDoc.Enabled = True AndAlso chk_Doc_Select.Enabled = True
    End Sub

    Private Sub chk_Location_Select_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chk_Location_Select.ToggleStateChanged
        cbgLocation.Enabled = True AndAlso chk_Location_Select.Enabled = True
    End Sub
    Private Sub btnreset1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset1.Click
        Reset()
    End Sub
    Sub Reset()

        dtpFromdate1.Value = clsCommon.GETSERVERDATE()
        dtpToDate1.Value = clsCommon.GETSERVERDATE()
        LoadLocation()
        chkall.IsChecked = True
        chk_Location_All.IsChecked = True
        rdbtnAdjustment.IsChecked = True

    End Sub



    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "ADJ-RPT"
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


End Class
