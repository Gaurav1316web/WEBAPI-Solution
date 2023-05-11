'--------------------------------Last modify By - Dipti ------------------------------------
'--------------------------------Last modify date - 31/01/2013-------------------------------------
'---------At the form load only that company will keep checked by whom user get logged in------

Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common

Public Class FrmTransitBreakageReport
    Inherits FrmMainTranScreen
    Const colSelect As String = "SELECT"
    Const colCompCode As String = "COMPCODE"
    Const colCompName As String = "COMPNAME"
    Const colDataBaseName As String = "DATABASE"
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmTransitBreakageReport1)
        'If Not (MyBase.isReadFlag) Then
        '    Throw New Exception("Permission Denied")

        'End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmTransitBreakageReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            printdata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            reset()
        End If
    End Sub
    Private Sub FrmTransitBreakageReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        chkdocAll1.IsChecked = True
        chkLocationAll.IsChecked = True
        fromdate.Value = clsCommon.GETSERVERDATE()
        Todate.Value = clsCommon.GETSERVERDATE()
        Fromtime.Value = DateTime.Now
        Fromtime.ShowUpDown = True
        Totime.Value = DateTime.Now
        Totime.ShowUpDown = True
        'rbtnAllCompany.IsChecked = True
        rbtnSelectCompany.IsChecked = True
      
        If rdobtnSummary.IsChecked = True Then
            lblSrn_Number.Visible = False
            lblToSRNnumber.Visible = False
            txtSRNnumber.Visible = False
            txtToSRNnumber.Visible = False
            drpboxType.Items.Add("Item")
            drpboxType.Items.Add("Vendor")
            drpboxType.SelectedIndex = 0
            RadGroupBox1.Visible = True

        End If
        If drpboxType.Text = "Item" Then
            groupbxItemVendor.Text = "Select Item"
        ElseIf drpboxType.Text = "Vendor" Then
            groupbxItemVendor.Text = "Select Vendor"
        End If
        loadsub()
        LoadLocation()
        SetDataBaseGrid()
        'If Not clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(btnprint, "Press Alt+P for Print ")

        For ii As Integer = 0 To gvDB.Rows.Count - 1
            If clsCommon.CompairString(clsCommon.myCstr(gvDB.Rows(ii).Cells(colCompCode).Value), objCommonVar.CurrentCompanyCode) = CompairStringResult.Equal Then
                gvDB.Rows(ii).Cells(colSelect).Value = 1
            End If
        Next




    End Sub

    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "TR-BKG-RPT"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete & "," & enuUserRights.enuAuthorised
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
    '        funSetUserAccess = True
    '    Catch er As Exception

    '    End Try
    'End Function
    Sub SetDataBaseGrid()
        gvDB.Rows.Clear()
        gvDB.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.FormatString = ""
        repoSelect.HeaderText = "Select"
        repoSelect.Name = colSelect
        repoSelect.Width = 50
        repoSelect.ReadOnly = False
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvDB.MasterTemplate.Columns.Add(repoSelect)

        Dim repoCompCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCompCode.FormatString = ""
        repoCompCode.HeaderText = "Company Code"
        repoCompCode.Name = colCompCode
        repoCompCode.Width = 150
        repoCompCode.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoCompCode)

        Dim repoCompName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCompName.FormatString = ""
        repoCompName.HeaderText = "Company Name"
        repoCompName.Name = colCompName
        repoCompName.Width = 150
        repoCompName.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoCompName)

        Dim repoDB As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDB.FormatString = ""
        repoDB.HeaderText = "Database Name"
        repoDB.Name = colDataBaseName
        repoDB.IsVisible = False
        repoDB.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoDB)

        Dim qry As String = "SELECT Comp_Code,Comp_Name,DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0 "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                gvDB.Rows.AddNew()
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colSelect).Value = False
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colCompCode).Value = clsCommon.myCstr(dr("Comp_Code"))
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colCompName).Value = clsCommon.myCstr(dr("Comp_Name"))
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colDataBaseName).Value = clsCommon.myCstr(dr("DataBase_Name"))
            Next
        End If
    End Sub
    Private Sub rbtnAllCompany_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnSelectCompany.ToggleStateChanged, rbtnAllCompany.ToggleStateChanged
        gvDB.Enabled = Not rbtnAllCompany.IsChecked
    End Sub

    Private Function GetSelectedDatabase() As List(Of String)
        Dim arrDBName As New List(Of String)
        For ii As Integer = 0 To gvDB.Rows.Count - 1
            If ((clsCommon.myCBool(gvDB.Rows(ii).Cells(colSelect).Value)) OrElse rbtnAllCompany.IsChecked) Then
                arrDBName.Add(clsCommon.myCstr(gvDB.Rows(ii).Cells(colDataBaseName).Value))
            End If
        Next
        Return arrDBName
    End Function

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        Me.Close()
    End Sub
    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        reset()
    End Sub
    Public Sub loadsub()
        Dim qry As String
        qry = "select item_code as Code ,item_desc as Name from TSPL_ITEM_MASTER "
        cbgdoc.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgdoc.ValueMember = "Code"

    End Sub

    Public Sub LoadLocation()
        'Dim Qry As String = "select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        'cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        'cbgLocation.ValueMember = "Code"
        'cbgLocation.DisplayMember = "Description"
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub


    Public Sub reset()
        fromdate.Value = clsCommon.GETSERVERDATE()
        Todate.Value = clsCommon.GETSERVERDATE()
        Fromtime.Value = DateTime.Now
        Totime.Value = DateTime.Now
        rdobtnSummary.IsChecked = True
        txtSRNnumber.Visible = False
        txtToSRNnumber.Visible = False

        txtSRNnumber.Value = ""
        txtToSRNnumber.Value = ""
        drpboxType.SelectedIndex = 0
        chkdocAll1.IsChecked = True
        chkLocationAll.IsChecked = True
        rbtnAllCompany.IsChecked = True
    End Sub
    Private Sub rdobtndetails_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdobtndetails.ToggleStateChanged
        If rdobtndetails.IsChecked = True Then

            lblSrn_Number.Visible = True
            lblToSRNnumber.Visible = True
            txtSRNnumber.Visible = True
            txtToSRNnumber.Visible = True
            drpboxType.Items.Clear()
            drpboxType.Items.Add("Date")
            drpboxType.Items.Add("Vendor")
            drpboxType.SelectedIndex = 0
            RadGroupBox1.Visible = True

        ElseIf rdobtnSummary.IsChecked = True Then
            reset()
            lblSrn_Number.Visible = False
            lblToSRNnumber.Visible = False
            txtSRNnumber.Visible = False
            txtToSRNnumber.Visible = False
            drpboxType.Items.Clear()
            drpboxType.Items.Add("Item")
            drpboxType.Items.Add("Vendor")
            drpboxType.SelectedIndex = 0
            RadGroupBox1.Visible = True
            loadsub()


        End If


    End Sub

    Private Sub txtSRNnumber__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtSRNnumber._MYValidating
        Dim qry As String = "select  SRN_No as 'Code',Vendor_Name as 'Vendor Name',convert(varchar(12),SRN_Date,103)as SRN_Date  from TSPL_SRN_HEAD "
        txtSRNnumber.Value = clsCommon.ShowSelectForm("SRNnumberFilter", qry, "Code", "", txtSRNnumber.Value, "Code", isButtonClicked)
    End Sub

    Private Sub txtToSRNnumber__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtToSRNnumber._MYValidating
        Dim qry As String = "select  SRN_No as 'Code',Vendor_Name as 'Vendor Name',convert(varchar(12),SRN_Date,103)as SRN_Date  from TSPL_SRN_HEAD  "
        txtToSRNnumber.Value = clsCommon.ShowSelectForm("SRNnumberFilter", qry, "Code", "", txtToSRNnumber.Value, "Code", isButtonClicked)
    End Sub
    Public Sub printdata()
        Try
            Dim qry As String = ""
            '  Dim qry1 As String
            Dim frmdate As String = fromdate.Value.Date.ToString("yyyy/MM/dd")
            Dim todate1 As String = Todate.Value.Date.ToString("yyyy/MM/dd")
            Dim frmtime As String = Fromtime.Value.ToLongTimeString
            Dim totime1 As String = Totime.Value.ToLongTimeString
            Dim fromdatetime As String = frmdate & " " & frmtime
            Dim todatetime As String = todate1 & " " & totime1
            Dim frmdate123 As String = fromdate.Value.Date.ToShortDateString
            Dim todate123 As String = Todate.Value.Date.ToShortDateString
            Dim ArrDoc As ArrayList = cbgdoc.CheckedValue
            Dim ArrDoc1 As ArrayList = cbgLocation.CheckedValue
            Dim arrSelDB As List(Of String) = GetSelectedDatabase()

            If rdobtnSummary.IsChecked = True And drpboxType.Text = "Item" Then
                'qry = " select '" + frmdate123 + "'as Fromdate,'" + todate123 + "'as Todate,'" + frmtime + "' as fromtime,'" + totime1 + "' as totime  ,ItemCode,Itemdesc,HalfFilled,BurstQty,ShortQty,SrndateTime,(case itemtype when 'F'then'Finished Goods'end) AS Itemtype ,CONVERT(varchar(12),SrndateTime,103) as date,xxx.Comp_Code,TSPL_COMPANY_MASTER.Comp_Name as compname,TSPL_COMPANY_MASTER.Logo_Img as Image1,TSPL_COMPANY_MASTER.Logo_Img2 as Image2,(select max(ADD1 + case when len(add2)> 0 then ',' else '' end + ADD2 +case when len(add3)> 0 then ','else '' end +ADD3+case when len(add4)> 0 then ',' else '' end +ADD4+case when len(City_Code)> 0 then ',' else '' end +City_Code +case when len(STATE)> 0 then ',' else '' end  +STATE) from tspl_location_master where Location_Code = BillAddress )as address from(select TSPL_SRN_DETAIL .Item_Code  as ItemCode,max(TSPL_SRN_DETAIL .Item_Desc) as Itemdesc, sum(TSPL_SRN_DETAIL.Leak_Qty) as HalfFilled,sum(TSPL_SRN_DETAIL.Burst_Qty) as BurstQty,sum(TSPL_SRN_DETAIL.Short_Qty) as ShortQty,max(TSPL_SRN_HEAD.SRN_Date) as SrndateTime,MAX(TSPL_SRN_HEAD .Item_Type )as itemtype,MAX(TSPL_SRN_HEAD .Comp_Code)as Comp_Code,MAX(TSPL_SRN_HEAD .Bill_To_Location )as BillAddress from TSPL_SRN_DETAIL left outer join TSPL_SRN_HEAD on TSPL_SRN_DETAIL .SRN_No =TSPL_SRN_HEAD .SRN_No group by TSPL_SRN_DETAIL.Item_Code ) as xxx left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER .Comp_Code =xxx.Comp_Code where xxx.SrndateTime >='" + fromdatetime + "' and xxx.SrndateTime <='" + todatetime + "'and xxx.itemtype  ='F'"
                qry = " select '" + frmdate123 + "'as Fromdate,'" + todate123 + "'as Todate,'" + frmtime + "' as fromtime,'" + totime1 + "' as totime  ,ItemCode,Itemdesc,HalfFilled,BurstQty,ShortQty,SrndateTime,BillAddress as location,(case itemtype when 'F'then'Finished Goods'end) AS Itemtype ,CONVERT(varchar(12),SrndateTime,103) as date,xxx.Comp_Code," + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name as compname, " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Logo_Img as Image1, " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Logo_Img2 as Image2,(select max(ADD1 + case when len(add2)> 0 then ',' else '' end + ADD2 +case when len(add3)> 0 then ','else '' end +ADD3+case when len(add4)> 0 then ',' else '' end +ADD4+case when len(City_Code)> 0 then ',' else '' end +City_Code +case when len(STATE)> 0 then ',' else '' end  +STATE) from " + clsCommon.ReplicateDBString + "tspl_location_master where Location_Code = BillAddress )as address,(select MAX(location_desc)from " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER where Location_Code =BillAddress )as Loc_Desc from(select  " + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL .Item_Code  as ItemCode,max( " + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL .Item_Desc) as Itemdesc, sum( " + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL.Leak_Qty) as HalfFilled,sum( " + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL.Burst_Qty) as BurstQty,sum( " + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL.Short_Qty) as ShortQty,max( " + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD.SRN_Date) as SrndateTime,MAX( " + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD .Item_Type )as itemtype,MAX( " + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD .Comp_Code)as Comp_Code,MAX( " + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD .Bill_To_Location )as BillAddress from  " + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL left outer join   " + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD on  " + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL .SRN_No = " + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD .SRN_No  where " + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD.SRN_Date >='" + fromdatetime + "' and " + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD.SRN_Date <='" + todatetime + "'"
                If chkDoc_select1.IsChecked = True AndAlso cbgdoc.CheckedValue.Count = 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select atleast one Item")
                    Return

                Else
                    If chkDoc_select1.IsChecked = True Then
                        qry += "and " + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL .Item_Code in (" + clsCommon.GetMulcallString(ArrDoc) + ") "
                    End If

                End If
                If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count = 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select atleast one location")
                    Return
                ElseIf chkLocationSelect.IsChecked = True Then
                    qry += " and " + clsCommon.ReplicateDBString + "TSPL_SRN_head.Bill_To_Location  in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
                End If
                qry += " and " + clsCommon.ReplicateDBString + "TSPL_SRN_head.Item_Type  ='F' group by  " + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL.Item_Code ) as xxx left outer join  " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER on  " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER .Comp_Code =xxx.Comp_Code "

            ElseIf rdobtnSummary.IsChecked = True And drpboxType.Text = "Vendor" Then
                If txtSRNnumber.Value = "" And txtToSRNnumber.Value = "" Then
                    common.clsCommon.MyMessageBoxShow("Please select SRN No Number")
                    Exit Sub
                End If

                qry = " select '" + frmdate123 + "'as Fromdate,'" + todate123 + "'as Todate,'" + frmtime + "' as fromtime,'" + totime1 + "' as totime ,'" + txtSRNnumber.Value + "'as FrmSrn_Num,'" + txtToSRNnumber.Value + "' as To_Srn_num,vendorCode,vendorname,SRN_NO ,ItemCode,Itemdesc,HalfFilled,BurstQty,ShortQty,SrndateTime,BillAddress as location,(case itemtype when 'F'then'Finished Goods'end) AS Itemtype ,CONVERT(varchar(12),SrndateTime,103) as date,xxx.Comp_Code," + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name as compname," + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Logo_Img as Image1," + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Logo_Img2 as Image2,(select max(ADD1 + case when len(add2)> 0 then ',' else '' end + ADD2 +case when len(add3)> 0 then ','else '' end +ADD3+case when len(add4)> 0 then ',' else '' end +ADD4+case when len(City_Code)> 0 then ',' else '' end +City_Code +case when len(STATE)> 0 then ',' else '' end  +STATE) from " + clsCommon.ReplicateDBString + "tspl_location_master where Location_Code = BillAddress )as address,(select MAX(location_desc)from " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER where Location_Code =BillAddress )as Loc_Desc from(select " + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD .Vendor_Code as vendorCode ,max(" + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD .Vendor_Name) as vendorname ,max(" + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD .SRN_No) as SRN_NO,max(" + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL .Item_Code)  as ItemCode,max(" + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL .Item_Desc) as Itemdesc, sum(" + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL.Leak_Qty) as HalfFilled,sum(" + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL.Burst_Qty) as BurstQty,sum(" + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL.Short_Qty) as ShortQty,max(" + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD.SRN_Date) as SrndateTime,MAX(" + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD .Item_Type )as itemtype,MAX(" + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD .Comp_Code)as Comp_Code,MAX(" + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD .Bill_To_Location )as BillAddress from " + clsCommon.ReplicateDBString + "TSPL_SRN_head left outer join " + clsCommon.ReplicateDBString + "TSPL_SRN_detail on " + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL .SRN_No =" + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD .SRN_No where " + clsCommon.ReplicateDBString + "TSPL_SRN_head.SRN_Date >='" + fromdatetime + "' and " + clsCommon.ReplicateDBString + "TSPL_SRN_head.SRN_Date <='" + todatetime + "' and " + clsCommon.ReplicateDBString + "TSPL_SRN_head.SRN_No >='" & txtSRNnumber.Value & "' and  " + clsCommon.ReplicateDBString + "TSPL_SRN_head.SRN_No <= '" & txtToSRNnumber.Value & "'"
                If chkDoc_select1.IsChecked = True AndAlso cbgdoc.CheckedValue.Count = 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select atleast one Vendor")
                    Return

                Else
                    If chkDoc_select1.IsChecked = True Then
                        qry += "and " + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD .Vendor_Code  in (" + clsCommon.GetMulcallString(ArrDoc) + ") "
                    End If

                End If
                If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count = 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select atleast one location")
                    Return
                ElseIf chkLocationSelect.IsChecked = True Then
                    qry += " and " + clsCommon.ReplicateDBString + "TSPL_SRN_head.Bill_To_Location  in (" + clsCommon.GetMulcallString(ArrDoc1) + ")"
                End If
                qry += " and " + clsCommon.ReplicateDBString + "TSPL_SRN_head.Item_Type  ='F' group by " + clsCommon.ReplicateDBString + "TSPL_SRN_head.Vendor_Code,Item_Code)as xxx left outer join " + clsCommon.ReplicateDBString + "tspl_company_master on " + clsCommon.ReplicateDBString + "tspl_company_master.comp_code = xxx.comp_code "


            ElseIf rdobtndetails.IsChecked = True And drpboxType.Text = "Date" Then
                If txtSRNnumber.Value = "" And txtToSRNnumber.Value = "" Then
                    common.clsCommon.MyMessageBoxShow("Please select SRN No Number")
                    Exit Sub
                End If
                qry = "select  '" + frmdate123 + "'as Fromdate,'" + todate123 + "'as Todate,'" + frmtime + "' as fromtime,'" + totime1 + "' as totime,'" + txtSRNnumber.Value + "'as FrmSrn_Num,'" + txtToSRNnumber.Value + "' as To_Srn_num, " + clsCommon.ReplicateDBString + "tspl_srn_head.srn_no as SRN_NO ," + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL .Item_Code  as ItemCode," + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL .Item_Desc as Itemdesc, " + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL.Leak_Qty as HalfFilled," + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL.Burst_Qty  as BurstQty," + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL.Short_Qty as ShortQty," + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD.SRN_Date as SrndateTime,convert(varchar(8)," + clsCommon.ReplicateDBString + "tspl_srn_head.SRN_Date,108)as time1,Convert(varchar(12)," + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD.srn_date,103) as date1 ,(case when " + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD .Item_Type='F'then 'Finished Good'end) as itemtype," + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD .Comp_Code as Comp_Code," + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name as compname," + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Logo_Img as Image1," + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Logo_Img2 as Image2,(select max(ADD1 + case when len(add2)> 0 then ',' else '' end + ADD2 +case when len(add3)> 0 then ','else '' end +ADD3+case when len(add4)> 0 then ',' else '' end +ADD4+case when len(City_Code)> 0 then ',' else '' end +City_Code +case when len(STATE)> 0 then ',' else '' end  +STATE) from " + clsCommon.ReplicateDBString + "tspl_location_master where Location_Code = " + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD .Bill_To_Location  )as address," + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD .Bill_To_Location as Location,(select Location_Desc  from " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER where Location_Code =" + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD .Bill_To_Location   )as Loc_Desc  from " + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL left outer join " + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD on TSPL_SRN_DETAIL .SRN_No =" + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD .SRN_No left outer join " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER on " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER .Comp_Code =" + clsCommon.ReplicateDBString + "tspl_srn_head.Comp_Code where " + clsCommon.ReplicateDBString + "TSPL_SRN_head .Srn_date >='" + fromdatetime + "' and " + clsCommon.ReplicateDBString + "TSPL_SRN_head .Srn_date <='" + todatetime + "' and " + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD .SRN_No >='" & txtSRNnumber.Value & "' and " + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD .SRN_No <= '" & txtToSRNnumber.Value & "' and " + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD .item_type  ='F'"
                If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count = 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select atleast one location")
                    Return
                ElseIf chkLocationSelect.IsChecked = True Then
                    qry += " and " + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD .Bill_To_Location in (" + clsCommon.GetMulcallString(ArrDoc1) + ")"
                End If
            ElseIf rdobtndetails.IsChecked = True And drpboxType.Text = "Vendor" Then
                If txtSRNnumber.Value = "" And txtToSRNnumber.Value = "" Then
                    common.clsCommon.MyMessageBoxShow("Please select SRN No Number")
                    Exit Sub
                End If
                qry = "select '" + frmdate123 + "'as Fromdate,'" + todate123 + "'as Todate,'" + frmtime + "' as fromtime,'" + totime1 + "' as totime,'" + txtSRNnumber.Value + "'as FrmSrn_Num,'" + txtToSRNnumber.Value + "' as To_Srn_num," + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD .Vendor_Code as vendorCode," + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD .Vendor_Name as vendorname, " + clsCommon.ReplicateDBString + "tspl_srn_head.srn_no as SRN_NO ," + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL .Item_Code  as ItemCode," + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL .Item_Desc as Itemdesc, " + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL.Leak_Qty as HalfFilled," + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL.Burst_Qty  as BurstQty," + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL.Short_Qty as ShortQty," + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD.SRN_Date as SrndateTime,convert(varchar(8)," + clsCommon.ReplicateDBString + "tspl_srn_head.SRN_Date,108)as time1,Convert(varchar(12)," + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD.srn_date,103) as date1 ,(case when " + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD .Item_Type='F'then 'Finished Good'end) as itemtype," + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD .Comp_Code as Comp_Code," + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name as compname," + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Logo_Img as Image1," + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Logo_Img2 as Image2,(select max(ADD1 + case when len(add2)> 0 then ',' else '' end + ADD2 +case when len(add3)> 0 then ','else '' end +ADD3+case when len(add4)> 0 then ',' else '' end +ADD4+case when len(City_Code)> 0 then ',' else '' end +City_Code +case when len(STATE)> 0 then ',' else '' end  +STATE) from " + clsCommon.ReplicateDBString + "tspl_location_master where Location_Code = " + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD .Bill_To_Location  )as address ," + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD .Bill_To_Location as Location,(select Location_Desc  from " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER where Location_Code =" + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD .Bill_To_Location   )as Loc_Desc from " + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL left outer join " + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD on " + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL .SRN_No =" + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD .SRN_No left outer join " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER on " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER .Comp_Code =" + clsCommon.ReplicateDBString + "tspl_srn_head.Comp_Code where " + clsCommon.ReplicateDBString + "TSPL_SRN_head .Srn_date >='" + fromdatetime + "' and " + clsCommon.ReplicateDBString + "TSPL_SRN_head .Srn_date <='" + todatetime + "' and " + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD .SRN_No >='" & txtSRNnumber.Value & "' and " + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD .SRN_No <= '" & txtToSRNnumber.Value & "' and " + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD .item_type  ='F'"
                If chkDoc_select1.IsChecked = True AndAlso cbgdoc.CheckedValue.Count = 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select atleast one Vendor")
                    Return

                Else
                    If chkDoc_select1.IsChecked = True Then
                        qry += "and " + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD .Vendor_Code   in (" + clsCommon.GetMulcallString(ArrDoc) + ") "
                    End If

                End If
                If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count = 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select atleast one location")
                    Return
                ElseIf chkLocationSelect.IsChecked = True Then
                    qry += " and " + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD .Bill_To_Location in (" + clsCommon.GetMulcallString(ArrDoc1) + ")"
                End If
            End If
            Dim finalqry As String = clsCommon.GetQueryWithAllSelectedDataBase(qry, arrSelDB, True)
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(finalqry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Record Found")
            Else
                dt = clsDBFuncationality.GetDataTable(finalqry)
                If rdobtnSummary.IsChecked = True And drpboxType.Text = "Item" Then
                    frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dt, "TransitBreakageReport", "Transit Breakage Report")
                ElseIf rdobtnSummary.IsChecked = True And drpboxType.Text = "Vendor" Then
                    frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dt, "TransitBreakageVendorSummary", "Transit Breakage Vendor Summary")
                ElseIf rdobtndetails.IsChecked = True And drpboxType.Text = "Date" Then
                    frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dt, "TransitBreakage_Details", "Transit Breakage Report")
                ElseIf rdobtndetails.IsChecked = True And drpboxType.Text = "Vendor" Then
                    frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dt, "TransitBreakageVendorDetails", "Transit Breakage Vendor Details")
                End If


            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        printdata()
    End Sub

    Private Sub drpboxType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles drpboxType.SelectedIndexChanged
        If rdobtnSummary.IsChecked = True Then
            If drpboxType.Text = "Vendor" Then
                lblSrn_Number.Visible = True
                lblToSRNnumber.Visible = True
                txtSRNnumber.Visible = True
                txtToSRNnumber.Visible = True
                txtSRNnumber.Value = ""
                txtToSRNnumber.Value = ""
                fromdate.Value = clsCommon.GETSERVERDATE()
                Todate.Value = clsCommon.GETSERVERDATE()
                Fromtime.Value = DateTime.Now
                Totime.Value = DateTime.Now
                Dim qry As String = "select Vendor_Code as  Code ,Vendor_Name as  Name  from TSPL_VENDOR_MASTER "
                cbgdoc.DataSource = clsDBFuncationality.GetDataTable(qry)
                cbgdoc.ValueMember = "Code"
                groupbxItemVendor.Enabled = True
                If drpboxType.Text = "Vendor" Then
                    groupbxItemVendor.Text = "Select Vendor"
                End If
            End If
            If drpboxType.Text = "Item" Then
                lblSrn_Number.Visible = False
                lblToSRNnumber.Visible = False
                txtSRNnumber.Visible = False
                txtToSRNnumber.Visible = False
                fromdate.Value = clsCommon.GETSERVERDATE()
                Todate.Value = clsCommon.GETSERVERDATE()
                Fromtime.Value = DateTime.Now
                Totime.Value = DateTime.Now
                cbgdoc.DataSource = Nothing
                Dim qry1 As String = "select item_code as Code ,item_desc as Name from TSPL_ITEM_MASTER "
                cbgdoc.DataSource = clsDBFuncationality.GetDataTable(qry1)
                cbgdoc.ValueMember = "Code"
                groupbxItemVendor.Enabled = True
                If drpboxType.Text = "Item" Then
                    groupbxItemVendor.Text = "Select Item"
                End If

            End If
        End If
        If rdobtndetails.IsChecked = True Then
            If drpboxType.Text = "Date" Then
                lblSrn_Number.Visible = True
                lblToSRNnumber.Visible = True
                txtSRNnumber.Visible = True
                txtToSRNnumber.Visible = True
                txtSRNnumber.Value = ""
                txtToSRNnumber.Value = ""
                fromdate.Value = clsCommon.GETSERVERDATE()
                Todate.Value = clsCommon.GETSERVERDATE()
                Fromtime.Value = DateTime.Now
                Totime.Value = DateTime.Now
                groupbxItemVendor.Enabled = False
            ElseIf drpboxType.Text = "Vendor" Then
                lblSrn_Number.Visible = True
                lblToSRNnumber.Visible = True
                txtSRNnumber.Visible = True
                txtToSRNnumber.Visible = True
                txtSRNnumber.Value = ""
                txtToSRNnumber.Value = ""
                fromdate.Value = clsCommon.GETSERVERDATE()
                Todate.Value = clsCommon.GETSERVERDATE()
                Fromtime.Value = DateTime.Now
                Totime.Value = DateTime.Now
                cbgdoc.DataSource = Nothing
                Dim qry As String = "select Vendor_Code as  Code ,Vendor_Name as  Name from TSPL_VENDOR_MASTER "
                cbgdoc.DataSource = clsDBFuncationality.GetDataTable(qry)
                cbgdoc.ValueMember = "Code"
                groupbxItemVendor.Enabled = True
                If drpboxType.Text = "Vendor" Then
                    groupbxItemVendor.Text = "Select Vendor"
                End If


            End If
        End If



    End Sub

    Private Sub chkdocAll1_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkdocAll1.ToggleStateChanged
        cbgdoc.Enabled = Not chkdocAll1.IsChecked
    End Sub

    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgLocation.Enabled = False
    End Sub

    Private Sub chkLocationSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationSelect.ToggleStateChanged
        cbgLocation.Enabled = True
    End Sub

End Class
