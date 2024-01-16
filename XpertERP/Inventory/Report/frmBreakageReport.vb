''''--28/06/2012--[Updation By--Pankaj Kumar]--Added Location Segment Filter And Breakage Type will not be Mendatory, Changed Reports----Req by-Balwinder Sir----
Imports common
Public Class FrmBreakageReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.mbtnBreakageReport)
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

    Private Sub FrmBreakageReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.P Then
            funPrint()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            reset()
        End If
    End Sub
    Private Sub FrmBreakageReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        dtpstart.Value = clsCommon.GETSERVERDATE()
        dtpend.Value = clsCommon.GETSERVERDATE()
        dtpStarttime.Value = DateTime.Now
        dtpStarttime.ShowUpDown = True
        dtpendtime.Value = "12:00PM"
        dtpendtime.ShowUpDown = True
        'lblBreakageType.Visible = False
        'txtBreakageType.Visible = False
        RadioBtnSummary.IsChecked = True
        dtpstart.Focus()
        LoadType()
        chktypeAll.IsChecked = True
        reset()
        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If

    End Sub

    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "BRK-RPT"
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
        dtpstart.Value = clsCommon.GETSERVERDATE()
        dtpend.Value = clsCommon.GETSERVERDATE()
        dtpStarttime.Value = DateTime.Now
        'dtpendtime.Value = "02/08/2013 2:37:53 PM"
        dtpendtime.Value = DateTime.Now
        txtBreakageType.Value = Nothing
        LoadType()
        chktypeAll.IsChecked = True
        LoadLocation()
        chkLocAll.IsChecked = True
    End Sub
    'Addde By Pankaj 0n -- 28/06/2012
    Sub LoadLocation()
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        'cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        'cbgLocation.ValueMember = "Code"
        'cbgLocation.DisplayMember = "Description"
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub

    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = False
    End Sub

    Private Sub chkLocSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocSelect.ToggleStateChanged
        cbgLocation.Enabled = True
    End Sub
    '---_Code Ends Here---------------
    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        funPrint()
    End Sub

    Public Sub funPrint()
        If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select at least one Location", Me.Text)
            Return
        End If

        If chktypeSelect.IsChecked AndAlso cbgtype.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select at least one Type", Me.Text)
            Return
        End If
        Dim IsBreakage As String = ""
        Dim filter As String = ""
        If (dtpstart.Value > dtpend.Value) Then
            common.clsCommon.MyMessageBoxShow(Me, "'Start Date' Cann't be more than 'End date'", Me.Text)
        ElseIf (dtpStarttime.Value > dtpendtime.Value) Then
            common.clsCommon.MyMessageBoxShow(Me, "'Start Time' Cann't be more than 'End Time'", Me.Text)
        ElseIf chktypeSelect.IsChecked = True AndAlso cbgtype.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please Select Atleast Single Type Or Select All", Me.Text)
        ElseIf chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "please Select Atleast Single Location Segment Or Select All", Me.Text)
        Else
            If clsCommon.myLen(txtBreakageType.Value) > 0 Then
                IsBreakage = " AND TSPL_ADJUSTMENT_DETAIL.BreakageType= '" + txtBreakageType.Value + "'"
            End If
            If chktypeSelect.IsChecked AndAlso cbgtype.CheckedValue.Count > 0 Then
                filter += " and TSPL_ADJUSTMENT_DETAIL.Unit_Code in  (" + clsCommon.GetMulcallString(cbgtype.CheckedValue) + ")"
            End If
            If chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0 Then
                filter += " AND TSPL_ADJUSTMENT_HEADER.Loc_Code in  (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") ) "
            End If
            Try
                Dim address As String
                If cbgLocation.CheckedValue.Count = 1 Then
                    address = " (Select MAX(TSPL_LOCATION_MASTER.Add1 + case When TSPL_LOCATION_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.Add2, 103) End + Case When TSPL_LOCATION_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_LOCATION_MASTER.Add3,103) end + case When TSPL_LOCATION_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.City_Code, 103) end+ Case When TSPL_TDS_STATE_MASTER.State_Code ='' Then '' else ', '+Convert(Varchar, TSPL_TDS_STATE_MASTER.State_Name ) end +  Case When TSPL_LOCATION_MASTER.Pin_Code='' Then '' Else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.Pin_Code, 103)  end) as Address From TSPL_LOCATION_MASTER Left Outer Join TSPL_TDS_STATE_MASTER ON TSPL_LOCATION_MASTER.State= TSPL_TDS_STATE_MASTER.State_Code  "
                    address += "  WHERE TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "))"
                Else
                    address = "(TSPL_COMPANY_MASTER.Add1 + case When TSPL_COMPANY_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Add2, 103) End + Case When TSPL_COMPANY_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_COMPANY_MASTER.Add3,103) end + case When TSPL_COMPANY_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.City_Code, 103) end+ Case When TSPL_COMPANY_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_COMPANY_MASTER.State) end +  Case When TSPL_COMPANY_MASTER.Pincode='' Then '' Else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Pincode, 103)  end) "

                End If
                If RadioBtnDetail.IsChecked Then
                    Dim qry As String = "SELECT '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd-MMM-yyyy") + "' as RunDate, TSPL_BREAKAGE_HEAD.Breakage_Type as [Breakage Type], TSPL_BREAKAGE_HEAD.Description as [BrkgTypeDesc],TSPL_COMPANY_MASTER.Comp_Name as [Company Name], " + address + " as [Address], TSPL_COMPANY_MASTER.Logo_Img as [Logo1], TSPL_COMPANY_MASTER.Logo_Img2 as [Logo2], " & _
        "TSPL_ADJUSTMENT_DETAIL.Item_Code as [Item Code], TSPL_ADJUSTMENT_DETAIL.Item_Description as [Description], isnull(TSPL_ADJUSTMENT_DETAIL.LeakageQty,0) as [Leakage],ISNULL( TSPL_ADJUSTMENT_DETAIL.Breakage,0) as [Breakage], " & _
        "TSPL_ADJUSTMENT_HEADER.Created_Date as [Adjustment Date], TSPL_ADJUSTMENT_HEADER.Document_No as [Ref Doc No], TSPL_ADJUSTMENT_HEADER.Created_time as [Time],  TSPL_ADJUSTMENT_DETAIL.Adjustment_No as [Doc No], TSPL_ADJUSTMENT_DETAIL.Item_Quantity as [Quantity],CONVERT(date, '" & dtpstart.Value & "', 103) AS startdate, CONVERT(date, '" & dtpend.Value & "', 103) AS enddate, substring('" & dtpStarttime.Value & "',12,12) as startTime, substring('" & dtpendtime.Value & "',12,12) as endTime, TSPL_ADJUSTMENT_DETAIL.Unit_Code as UOM FROM TSPL_ADJUSTMENT_HEADER " & _
        "LEFT OUTER JOIN TSPL_ADJUSTMENT_DETAIL ON TSPL_ADJUSTMENT_HEADER.Adjustment_No = TSPL_ADJUSTMENT_DETAIL.Adjustment_No LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_ADJUSTMENT_HEADER.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code LEFT OUTER JOIN TSPL_BREAKAGE_HEAD on TSPL_BREAKAGE_HEAD.Breakage_Type=TSPL_ADJUSTMENT_DETAIL.BreakageType where convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) >= convert(date, '" & dtpstart.Value & "',103) AND convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) <= convert(date, '" & dtpend.Value & "',103) "
                    If Not clsCommon.CompairString(dtpStarttime.Value, dtpendtime.Value) = CompairStringResult.Equal Then

                        qry += " and convert(time,TSPL_ADJUSTMENT_HEADER.Created_time,103) > = CONVERT(time,'" & dtpStarttime.Value & "' ,103) and convert(time,TSPL_ADJUSTMENT_HEADER.Created_time,103) < = CONVERT(time,'" & dtpendtime.Value & "' ,103) "
                    End If

                    qry += " " + IsBreakage + " " + filter + " order by TSPL_ADJUSTMENT_HEADER.Created_Date"

                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        common.clsCommon.MyMessageBoxShow("No Record Found")
                    Else
                        dt = clsDBFuncationality.GetDataTable(qry)
                        Dim frmCRV As New frmCrystalReportViewer()
                        frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, "crptBreakageDetail", "Breakage Detail")
                        frmCRV = Nothing
                    End If

                ElseIf RadioBtnSummary.IsChecked = True Then
                    Dim qry As String = "select '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd-MMM-yyyy") + "' as RunDate, Breakage_Date, BreakageType, BreakageDesc, LeakageQty, Breakage, TSPL_COMPANY_MASTER.Comp_Name as [Company Name], " + address + " as Address , TSPL_COMPANY_MASTER.Add3," & _
        " TSPL_COMPANY_MASTER.Logo_Img as logo1,TSPL_COMPANY_MASTER.Logo_Img2 as logo2, startdate , startTime, enddate, endTime, BrkgDescription, UOM  from (select max(TSPL_ADJUSTMENT_HEADER.Adjustment_Date) as Breakage_Date, " & _
        " MAX(TSPL_ADJUSTMENT_HEADER.Created_time) as Ceated_Time,MAX(TSPL_ADJUSTMENT_HEADER.Adjustment_No) as Adjustment_No, MAX(TSPL_ADJUSTMENT_HEADER.Reference) as Reference, " & _
        " MAX(TSPL_ADJUSTMENT_DETAIL.Item_Code) as Item_Code,MAX(TSPL_ADJUSTMENT_DETAIL.Item_Description) as Description, SUM(ISNULL( TSPL_ADJUSTMENT_DETAIL.Breakage,0)) as Breakage, " & _
        " SUM(isnull(TSPL_ADJUSTMENT_DETAIL.LeakageQty,0)) as LeakageQty, TSPL_BREAKAGE_HEAD.Breakage_Type as BreakageType, MAX(TSPL_BREAKAGE_HEAD.Description) as BreakageDesc, " & _
        " MAX(TSPL_ADJUSTMENT_HEADER .Comp_Code) as Comp,  CONVERT(date, '" & dtpstart.Value & "', 103) AS startdate, CONVERT(date, '" & dtpend.Value & "', 103) AS enddate, substring('" & dtpStarttime.Value & "',12,12) as startTime, substring('" & dtpendtime.Value & "',12,12) as endTime   , MAX(TSPL_BREAKAGE_HEAD.Description) as [BrkgDescription], Max(TSPL_ADJUSTMENT_DETAIL.Unit_Code) as UOM from TSPL_ADJUSTMENT_DETAIL " & _
        " left outer join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No Left Outer join TSPL_BREAKAGE_HEAD on TSPL_BREAKAGE_HEAD.Breakage_Type= TSPL_ADJUSTMENT_DETAIL.BreakageType " & _
        " Where convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) >= convert(date, '" & dtpstart.Value & "',103) AND convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) <= convert(date, '" & dtpend.Value & "',103) " + IsBreakage + " " + filter + "   "
                    If Not clsCommon.CompairString(dtpStarttime.Value, dtpendtime.Value) = CompairStringResult.Equal Then

                        qry += " and convert(time,TSPL_ADJUSTMENT_HEADER.Created_time,103) > = CONVERT(time,'" & dtpStarttime.Value & "' ,103) and convert(time,TSPL_ADJUSTMENT_HEADER.Created_time,103) < = CONVERT(time,'" & dtpendtime.Value & "' ,103) "
                    End If
                        qry += " group by TSPL_ADJUSTMENT_HEADER.Adjustment_Date, TSPL_BREAKAGE_HEAD.Breakage_Type, TSPL_ADJUSTMENT_DETAIL.Unit_Code) as xxx LEFT OUTER JOIN TSPL_COMPANY_MASTER ON xxx.Comp = TSPL_COMPANY_MASTER.Comp_Code  order by Breakage_Date"

                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        common.clsCommon.MyMessageBoxShow(Me, "No Record Found", Me.Text)
                    Else
                            dt = clsDBFuncationality.GetDataTable(qry)
                        Dim frmCRV As New frmCrystalReportViewer()
                        frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, "crptBreakageSummary", "Breakage Summary")
                        frmCRV = Nothing
                    End If
                End If
            Catch ex As Exception
                common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        End If
    End Sub

    Private Sub txtBreakageType__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtBreakageType._MYValidating
        Dim qry As String = "select TSPL_BREAKAGE_HEAD.Breakage_Type as [Code], TSPL_BREAKAGE_HEAD.Description as [Description] from TSPL_BREAKAGE_HEAD"
        txtBreakageType.Value = clsCommon.ShowSelectForm("Breakage Type", qry, "Code", "", txtBreakageType.Value, "Code", isButtonClicked)
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub RadioBtnDetail_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles RadioBtnDetail.ToggleStateChanged
        'lblBreakageType.Visible = True
        'txtBreakageType.Visible = True
    End Sub

    Private Sub RadioBtnSummary_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles RadioBtnSummary.ToggleStateChanged
        'lblBreakageType.Visible = False
        'txtBreakageType.Visible = False
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
        dr = dt.NewRow()
        'dr("Code") = "N0S"
        'dt.Rows.Add(dr)
        'dr = dt.NewRow()
        'dr("Code") = "8oz"
        'dt.Rows.Add(dr)
        'dr = dt.NewRow()
        'dr("Code") = "Con"
        'dt.Rows.Add(dr)
        Return dt
    End Function



    Private Sub chktypeAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chktypeAll.ToggleStateChanged
        cbgtype.Enabled = Not chktypeAll.IsChecked
    End Sub

    
End Class
