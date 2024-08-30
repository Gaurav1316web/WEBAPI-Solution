'' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''


Imports common
Imports System.Threading
Imports Telerik.WinControls.UI.Export
Imports Telerik.WinControls.UI.Export.ExcelML
Imports System.IO

Public Class FrmItemSerialTrackingReport
    Inherits FrmMainTranScreen
#Region "Varaibels"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isDataLoad As Boolean = False
    Public dtFrom As Date
    Public dtTo As Date
    Public strType As String
    Public arrLocation As ArrayList
    Public arrItem As ArrayList
    Public arrCategory As ArrayList
    Public arrSubCategory As ArrayList

#End Region
    Sub LoadLocation()
        Dim qry As String = " select Location_Code,Location_Desc from TSPL_LOCATION_MASTER where Location_Type='Physical' order by Location_Code"
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Location_Code"
        cbgLocation.DisplayMember = "Location_Desc"
    End Sub

    Sub LoadType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Available"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Not Available"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Both"
        dt.Rows.Add(dr)

        cboType.DataSource = dt
        cboType.ValueMember = "Code"
        cboType.DisplayMember = "Code"
    End Sub



    Sub LoadSerialNo()
        Try
            Dim qry As String = "select distinct Auto_Sr_No as [Auto Sr No],TSPL_SERIAL_ITEM.Item_Code as Item,Item_Desc as [Item Desc] from TSPL_SERIAL_ITEM left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SERIAL_ITEM.Item_Code"
            cbgSerialNo.DataSource = clsDBFuncationality.GetDataTable(qry)
            cbgSerialNo.ValueMember = "Auto Sr No"
            cbgSerialNo.DisplayMember = "Item"

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadItem()
        Try
            Dim qry As String = " select Item_Code,Item_Desc from TSPL_ITEM_MASTER where Is_Serial_Item=1 order by Item_Code "
            cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry)
            cbgItem.ValueMember = "Item_Code"
            cbgItem.DisplayMember = "Item_Desc"
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FrmItemSerialTrackingReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        '' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
        btnExport.Visible = MyBase.isExport

        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmItemSerialTrackingReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If

    End Sub
    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton2.Click
        Me.Close()
    End Sub

    Sub GetReportGridID()
        Dim VarID As String = ""
        If rcbShowSerialNo.Checked = True Then
            VarID += "_SS"
        End If
        If rdbSummary.IsChecked = True Then
            VarID += "_SU"
        ElseIf rdbDetail.IsChecked = True Then
            VarID += "_DE"

        End If
        gv1.VarID = VarID
    End Sub
    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        Try
            GetReportGridID()
            gv1.EnableFiltering = True
            PageSetupReport_ID = MyBase.Form_ID
            LoadData()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub CommentedCode()
        ' Dim QrySerialDetials As String = " select aaa.Item_Code [Item Code],max( aaa.Item_Desc) as [Item Desc],aaa.Location_Code as [Location Code] ,aaa.Auto_Sr_No as [Auto Serial No], max(aaa.Qty) as Qty  from  (select a.Item_Code,TSPL_ITEM_MASTER.Item_Desc as Item_Desc,a.Location_Code,a.Auto_Sr_No ,'1' as 'Qty'  from TSPL_SERIAL_ITEM as a   left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=a.Item_Code  where a.Document_Date < '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "'  and In_Out_Type='I' and  code  not in (   select Auto_Sr_No from TSPL_SERIAL_ITEM as a    where a.Document_Date < '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "'   and In_Out_Type='O') union all select TSPL_SERIAL_ITEM.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_SERIAL_ITEM.Location_Code,TSPL_SERIAL_ITEM.Auto_Sr_No , '1' as 'Qty' from TSPL_SERIAL_ITEM  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SERIAL_ITEM.Item_Code   where  Document_Date >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and Document_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'  and In_Out_Type='I'  )aaa  where aaa.Auto_Sr_No not in (select Auto_Sr_No from TSPL_SERIAL_ITEM where  Document_Date >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and Document_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'  and In_Out_Type='O'  ) Group by aaa.Auto_Sr_No,  aaa.Item_Code ,aaa.Location_Code "

        '================================================================================================================================================================================================================ '================================================================================================================= '================================================================================================================= '================================================================================================================= '================================================================================================================= '================================================================================================================= '================================================================================================================= '==================================================================================================================================================================================================================================
        'QrySerialDetials += " create table #Closing (Auto_Sr_No varchar(100),Auto_Count int, Doc_Type varchar(50) ,  Item_Code varchar(100),Location_Code  varchar(100)) "



        '================================================================================================================================================================================================================ '================================================================================================================= '================================================================================================================= '================================================================================================================= '================================================================================================================= '================================================================================================================= '================================================================================================================= '==================================================================================================================================================================================================================================
        'Old Query
        'Dim qry As String = "select case when max(In_Out_Type) ='I' then 'Avaliable' else 'Not Available' end as Staus , " + Environment.NewLine
        'qry += "MAX(Document_Code) as [Document Code],MAX(convert(varchar,Document_Date,103)) as [Document Date],  " + Environment.NewLine
        'qry += "case when max(In_Out_Type)='I' then 'In' else 'Out' end as [Type],MAX(Auto_Sr_No) as [Auto Sr No]," + Environment.NewLine
        'qry += "MAX(Item_Code) as Item," + Environment.NewLine
        'qry += "MAX(Item_Desc) as [Item Desc],MAX(Location_Code) as Location from  (" + Environment.NewLine
        'qry += "select Code,Document_Date,In_Out_Type,Document_Code,Auto_Sr_No,TSPL_SERIAL_ITEM.Item_Code, " + Environment.NewLine
        'qry += " Item_Desc,Location_Code from TSPL_SERIAL_ITEM  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SERIAL_ITEM.Item_Code " + Environment.NewLine
        'qry += " where TSPL_ITEM_MASTER.Is_Serial_Item=1 and Document_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' " + Environment.NewLine
        'qry += ") aa where Code=(select MAX(code) from TSPL_SERIAL_ITEM  where Item_Code=aa.Item_Code  and Auto_Sr_No=aa.Auto_Sr_No ) " + Environment.NewLine

        'If rbtnItemSelet.IsChecked AndAlso cbgItem.CheckedValue.Count > 0 Then
        '    qry += " and aa.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") " + Environment.NewLine
        'End If
        'If rbtnLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
        '    qry += " and aa.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") " + Environment.NewLine
        'End If
        'If chkSerialselect.IsChecked AndAlso cbgSerialNo.CheckedValue.Count > 0 Then
        '    qry += " and aa.Auto_Sr_No in (" + clsCommon.GetMulcallString(cbgSerialNo.CheckedValue) + ") " + Environment.NewLine
        'End If


        'If clsCommon.CompairString(cboType.Text, "Available") = CompairStringResult.Equal Then
        '    qry += " and aa.In_Out_Type='I' "
        'ElseIf clsCommon.CompairString(cboType.Text, "Not Available") = CompairStringResult.Equal Then
        '    qry += " and aa.In_Out_Type='O' "
        'End If

        'qry += " group by Item_Code ,Auto_Sr_No"
        'Old Query End

        'New Query Added By Nazia
        'Dim qry As String = "select MAX(Document_Code) as [Document Code]," & _
        '    " MAX(convert(varchar,Document_Date,103)) as [Document Date], case when max(In_Out_Type)='I' then 'In' else 'Out' end as [Type],MAX(Auto_Sr_No) as [Auto Sr No]," & _
        '    " MAX(TSPL_SERIAL_ITEM.Item_Code) as Item,MAX(TSPL_ITEM_MASTER.Item_Desc) as [Item Desc],MAX(Location_Code) as Location" & _
        '    " ,case In_Out_Type when 'I' Then 1 else 0 end 'RecQty',case In_Out_Type when 'O' Then 1 else 0 end 'IssueQty'," & _
        '    " (select (sum(case In_Out_Type When 'I' then 1 else 0 end  )-sum(case In_Out_Type When 'O' then 1  else 0 end  ))  from TSPL_SERIAL_ITEM as a " & _
        '    " where a.Document_Date<TSPL_SERIAL_ITEM.Document_Date  and a.Location_Code=TSPL_SERIAL_ITEM.Location_Code and a.Item_Code=TSPL_SERIAL_ITEM.Item_Code ) as 'Opening'" & _
        '    " from  TSPL_SERIAL_ITEM  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SERIAL_ITEM.Item_Code " & _
        '     "  where TSPL_ITEM_MASTER.Is_Serial_Item = 1 and TSPL_SERIAL_ITEM.Document_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_SERIAL_ITEM.Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' "

        'If rbtnItemSelet.IsChecked AndAlso cbgItem.CheckedValue.Count > 0 Then
        '    qry += " and TSPL_SERIAL_ITEM.item_code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") " + Environment.NewLine
        'End If
        'If rbtnLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
        '    qry += " and TSPL_SERIAL_ITEM.location_code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") " + Environment.NewLine
        'End If
        'If chkSerialselect.IsChecked AndAlso cbgSerialNo.CheckedValue.Count > 0 Then
        '    qry += " and TSPL_SERIAL_ITEM.auto_sr_no in (" + clsCommon.GetMulcallString(cbgSerialNo.CheckedValue) + ") " + Environment.NewLine
        'End If


        'If clsCommon.CompairString(cboType.Text, "available") = CompairStringResult.Equal Then
        '    qry += " and TSPL_SERIAL_ITEM.in_out_type='i' "
        'ElseIf clsCommon.CompairString(cboType.Text, "not available") = CompairStringResult.Equal Then
        '    qry += " and TSPL_SERIAL_ITEM.in_out_type='o' "
        'End If
        'qry += " group by TSPL_SERIAL_ITEM.Item_Code ,TSPL_SERIAL_ITEM.Auto_Sr_No,In_Out_Type,Document_Date,Document_Code,Location_Code "
        'MainQry = "Select final.[Document Date],final.Type,final.[Auto Sr No],final.Item,final.[Item Desc],final.[Location],final.[RecQty] as 'Received Qty',final.[IssueQty] as 'Issue Qty',isnull(final.Opening,0) as Opening," & _
        '          " isnull(Opening,0)+RecQty-IssueQty as Closing from (" & qry & ")  as final order by [Document Code], [Document Date]"


        ''Old Query 
        'MainQry = "select  dd.Item_Code as [Item Code] ,Item_Desc as [Item desc],Location_Code as [Location Code] ,Location_Desc as [Location Desc],(isnull(ClQty,0)+isnull(IssQty,0)) as OP,IssQty as [Issue Qty],ClQty as [Closing]   from(select SUM( Stock_Qty * (case when InOut ='I' then 1 else -1 end)) as ClQty,sum(case when InOut ='I' then Stock_Qty  else 0 end) as RecvQty,sum(case when inout='O' then 1*Stock_Qty else 0 end) as IssQty,mm.Item_Code ,max(mm.Item_Desc) as Item_Desc,Location_Code ,max(Location_Desc) as  Location_Desc   from(" & _
        '        " select InOut ,TSPL_INVENTORY_MOVEMENT.Item_Code ,TSPL_INVENTORY_MOVEMENT.Item_Desc,'o' as type ,Source_Doc_No,Source_Doc_Date,Stock_Qty,TSPL_INVENTORY_MOVEMENT.Location_Code ,Location_Desc        from TSPL_INVENTORY_MOVEMENT " & _
        '        " left join tspl_location_master on tspl_location_master.Location_Code = TSPL_INVENTORY_MOVEMENT.Location_Code " & _
        '        " where TSPL_INVENTORY_MOVEMENT.Punching_Date <=CONVERT(Date,'" + txtFromDate.Value + "',103)"
        'If rbtnItemSelet.IsChecked AndAlso cbgItem.CheckedValue.Count > 0 Then
        '    MainQry += " and TSPL_INVENTORY_MOVEMENT.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") " + Environment.NewLine
        'End If
        'If rbtnLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
        '    MainQry += " and TSPL_INVENTORY_MOVEMENT.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") " + Environment.NewLine
        'End If
        'MainQry += " UNION ALL " & _
        '            " select InOut ,TSPL_INVENTORY_MOVEMENT_NEW.Item_Code ,TSPL_INVENTORY_MOVEMENT_NEW.Item_Desc,'o' as type ,Source_Doc_No,Source_Doc_Date,Stock_Qty,TSPL_INVENTORY_MOVEMENT_NEW.Location_Code ,Location_Desc      from TSPL_INVENTORY_MOVEMENT_NEW " & _
        '            " left join tspl_location_master on tspl_location_master.Location_Code = TSPL_INVENTORY_MOVEMENT_NEW.Location_Code " & _
        '             " where TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date <=CONVERT(Date,'" + txtFromDate.Value + "',103)"
        'If rbtnItemSelet.IsChecked AndAlso cbgItem.CheckedValue.Count > 0 Then
        '    MainQry += " and TSPL_INVENTORY_MOVEMENT_NEW.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") " + Environment.NewLine
        'End If
        'If rbtnLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
        '    MainQry += " and TSPL_INVENTORY_MOVEMENT_NEW.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") " + Environment.NewLine
        'End If
        'MainQry += " union all " & _
        '        " select InOut ,TSPL_INVENTORY_MOVEMENT.Item_Code ,TSPL_INVENTORY_MOVEMENT.Item_Desc,'R' as type,Source_Doc_No,Source_Doc_Date,Stock_Qty,TSPL_INVENTORY_MOVEMENT.Location_Code,Location_Desc    from TSPL_INVENTORY_MOVEMENT   " & _
        '        " left join tspl_location_master on tspl_location_master.Location_Code = TSPL_INVENTORY_MOVEMENT.Location_Code " & _
        '        " where TSPL_INVENTORY_MOVEMENT.Punching_Date >=CONVERT(Date,'" + txtFromDate.Value + "',103) and TSPL_INVENTORY_MOVEMENT.Punching_Date<=CONVERT(Date,'" + txtToDate.Value + "',103) "
        'If rbtnItemSelet.IsChecked AndAlso cbgItem.CheckedValue.Count > 0 Then
        '    MainQry += " and TSPL_INVENTORY_MOVEMENT.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") " + Environment.NewLine
        'End If
        'If rbtnLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
        '    MainQry += " and TSPL_INVENTORY_MOVEMENT.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") " + Environment.NewLine
        'End If
        'MainQry += " UNION ALL " & _
        '        " select InOut ,TSPL_INVENTORY_MOVEMENT_NEW.Item_Code ,TSPL_INVENTORY_MOVEMENT_NEW.Item_Desc,'R' as type,Source_Doc_No,Source_Doc_Date,Stock_Qty,TSPL_INVENTORY_MOVEMENT_NEW.Location_Code,Location_Desc    from TSPL_INVENTORY_MOVEMENT_NEW  " & _
        '        " left join tspl_location_master on tspl_location_master.Location_Code = TSPL_INVENTORY_MOVEMENT_NEW.Location_Code " & _
        '         " where TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date >=CONVERT(Date,'" + txtFromDate.Value + "',103) and TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date<=CONVERT(Date,'" + txtToDate.Value + "',103) "
        'If rbtnItemSelet.IsChecked AndAlso cbgItem.CheckedValue.Count > 0 Then
        '    MainQry += " and TSPL_INVENTORY_MOVEMENT_NEW.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") " + Environment.NewLine
        'End If
        'If rbtnLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
        '    MainQry += " and TSPL_INVENTORY_MOVEMENT_NEW.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") " + Environment.NewLine
        'End If
        'MainQry += " ) as mm left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=mm.Item_Code where TSPL_ITEM_MASTER.Is_Serial_Item = 1 group by mm.Item_Code,Location_Code )as dd"
        ''Old Query End

        '========================================================================================================================================================================================================================================== '================================================================================================================= '================================================================================================================= '================================================================================================================= '================================================================================================================= '================================================================================================================= '================================================================================================================= '==================================================================================================================================================================================================================================

        '================================================================================================================================================================================================================ '================================================================================================================= '================================================================================================================= '================================================================================================================= '================================================================================================================= '================================================================================================================= '================================================================================================================= '==================================================================================================================================================================================================================================
        ' old prabkar 
        '========================================================================================================================================================================================================================================== '================================================================================================================= '================================================================================================================= '================================================================================================================= '================================================================================================================= '================================================================================================================= '================================================================================================================= '==================================================================================================================================================================================================================================
        '=================================================================================================================
        '  QrySerialDetials += " create table #Closing (Auto_Sr_No varchar(100),Auto_Count int, Doc_Type varchar(50) COLLATE DATABASE_DEFAULT NULL ,  Item_Code varchar(100) COLLATE DATABASE_DEFAULT NULL,Location_Code  varchar(100) COLLATE DATABASE_DEFAULT NULL)  "

        ' QrySerialDetials += " INSERT INTO  #Closing ( Auto_Sr_No,Auto_Count,Doc_Type,Item_Code,Location_Code)   select Auto_Sr_No as Auto_Sr_No, count (Auto_Sr_No) as Auto_Count,'OQ'  as Doc_Type, Item_Code,Location_Code from TSPL_SERIAL_ITEM group by Auto_Sr_No ,Document_Date,Item_Code, Location_Code,In_Out_Type having  Document_Date <'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "'  and  In_Out_Type='I' and In_Out_Type <> 'O'  " + qry + " "

        ' QrySerialDetials += " INSERT INTO  #Closing ( Auto_Sr_No,Auto_Count,Doc_Type,Item_Code,Location_Code)   select Auto_Sr_No as Auto_Sr_No, count (Auto_Sr_No) as Auto_Count, 'RQ' as Doc_Type, Item_Code,Location_Code from TSPL_SERIAL_ITEM group by Auto_Sr_No ,Document_Date,Item_Code, Location_Code,In_Out_Type having   Document_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'    and  In_Out_Type ='I' and  In_Out_Type <> 'O'  " + qry + " "

        '  QrySerialDetials += " INSERT INTO  #Closing ( Auto_Sr_No,Auto_Count,Doc_Type,Item_Code,Location_Code)   select Auto_Sr_No as Auto_Sr_No, count (Auto_Sr_No) as Auto_Count ,'IQ' as Doc_Type, Item_Code,Location_Code from TSPL_SERIAL_ITEM group by Auto_Sr_No ,Document_Date,Item_Code, Location_Code,In_Out_Type having   Document_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'     and   In_Out_Type ='O'  " + qry + " "

        '''''QrySerialDetials += " select bbb.Item_Code ,bbb.Location_Code ,bbb.Auto_Sr_No,sum (bbb .Counts) as Qty from (select aaa.Item_Code,aaa.Location_Code, aaa.Auto_Sr_No,sum(ISNULL( aaa.OQ,0) + isnull (aaa.RQ,0)) - (ISNULL ( aaa.IQ,0)) as Counts from ( select  * from #Closing pivot ( Sum(Auto_Count) for Doc_Type in ([OQ],[RQ],[IQ])) as rrr ) aaa where(ISNULL( aaa.OQ,0) + isnull (aaa.RQ,0)) - (ISNULL ( aaa.IQ,0)) >0  group by aaa.Item_Code, aaa.Auto_Sr_No ,aaa.Location_Code ,aaa.OQ,aaa.RQ ,aaa.IQ) bbb  group by bbb.Auto_Sr_No,bbb.Item_Code,bbb.Location_Code "

        ' QrySerialDetials += " select xxx.Item_Code ,max(TSPL_ITEM_MASTER.Item_Desc ) as Item_Desc,xxx.Location_Code, max (TSPL_LOCATION_MASTER.Location_Desc) as Location_Desc ,xxx.Auto_Sr_No, max(xxx.Qty ) as Qty from (  select bbb.Item_Code ,bbb.Location_Code ,bbb.Auto_Sr_No,sum (bbb .Counts) as Qty from (select aaa.Item_Code,aaa.Location_Code, aaa.Auto_Sr_No,sum(ISNULL( aaa.OQ,0) + isnull (aaa.RQ,0)) - (ISNULL ( aaa.IQ,0)) as Counts from ( select  * from #Closing pivot ( Sum(Auto_Count) for Doc_Type in ([OQ],[RQ],[IQ])) as rrr ) aaa where(ISNULL( aaa.OQ,0) + isnull (aaa.RQ,0)) - (ISNULL ( aaa.IQ,0)) >0  group by aaa.Item_Code, aaa.Auto_Sr_No ,aaa.Location_Code ,aaa.OQ,aaa.RQ ,aaa.IQ) bbb group by bbb.Auto_Sr_No,bbb.Item_Code,bbb.Location_Code)xxx left outer join TSPL_ITEM_MASTER on  TSPL_ITEM_MASTER.Item_Code =xxx.Item_Code left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=xxx.Location_Code  group by xxx.Auto_Sr_No,xxx.Item_Code,xxx.Location_Code  "
        '=================================================================================================================

        ' WITH GIT LOCATION QERY
        'QrySerialDetials += " create table #Closing (Auto_Sr_No varchar(100),Auto_Count int, Doc_Type varchar(50) ,  Item_Code varchar(100),Location_Code  varchar(100) COLLATE DATABASE_DEFAULT NULL) "
        'QrySerialDetials += " INSERT INTO  #Closing ( Auto_Sr_No,Auto_Count,Doc_Type,Item_Code,Location_Code)   select Auto_Sr_No as Auto_Sr_No, count (Auto_Sr_No) as Auto_Count,'OQ'  as Doc_Type, Item_Code,case when  TSPL_LOCATION_MASTER.Location_Code is null then  TSPL_SERIAL_ITEM.Location_Code else TSPL_LOCATION_MASTER.Location_Code end as Location_Code from TSPL_SERIAL_ITEM  left outer join TSPL_LOCATION_MASTER on TSPL_SERIAL_ITEM.Location_Code=TSPL_LOCATION_MASTER.GIT_Location group by Auto_Sr_No ,Document_Date,Item_Code, TSPL_LOCATION_MASTER.Location_Code,In_Out_Type,TSPL_SERIAL_ITEM.Location_Code having  Document_Date <'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy") + "'  and  In_Out_Type='I' and In_Out_Type <> 'O'  " + qry + " "
        'QrySerialDetials += " INSERT INTO  #Closing ( Auto_Sr_No,Auto_Count,Doc_Type,Item_Code,Location_Code)   select Auto_Sr_No as Auto_Sr_No, count (Auto_Sr_No) as Auto_Count, 'RQ' as Doc_Type, Item_Code, case when  TSPL_LOCATION_MASTER.Location_Code is null then  TSPL_SERIAL_ITEM.Location_Code else TSPL_LOCATION_MASTER.Location_Code end as Location_Code from TSPL_SERIAL_ITEM left outer join TSPL_LOCATION_MASTER on TSPL_SERIAL_ITEM.Location_Code=TSPL_LOCATION_MASTER.GIT_Location group by Auto_Sr_No ,Document_Date,Item_Code, TSPL_LOCATION_MASTER.Location_Code,In_Out_Type,TSPL_SERIAL_ITEM.Location_Code having   Document_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy") + "' and Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy") + "'    and  In_Out_Type ='I' and  In_Out_Type <> 'O'  " + qry + " "
        'QrySerialDetials += " INSERT INTO  #Closing ( Auto_Sr_No,Auto_Count,Doc_Type,Item_Code,Location_Code)   select Auto_Sr_No as Auto_Sr_No, count (Auto_Sr_No) as Auto_Count ,'IQ' as Doc_Type, Item_Code,case when  TSPL_LOCATION_MASTER.Location_Code is null then  TSPL_SERIAL_ITEM.Location_Code else TSPL_LOCATION_MASTER.Location_Code end as Location_Code from TSPL_SERIAL_ITEM left outer join TSPL_LOCATION_MASTER on TSPL_SERIAL_ITEM.Location_Code=TSPL_LOCATION_MASTER.GIT_Location group by Auto_Sr_No ,Document_Date,Item_Code, TSPL_LOCATION_MASTER.Location_Code,In_Out_Type,TSPL_SERIAL_ITEM.Location_Code having   Document_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy") + "' and Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy") + "'     and   In_Out_Type ='O'  " + qry + " "
        'QrySerialDetials += " select bbb.Item_Code ,bbb.Location_Code ,bbb.Auto_Sr_No,sum (bbb .Counts) as Qty from (select aaa.Item_Code,aaa.Location_Code, aaa.Auto_Sr_No,sum(ISNULL( aaa.OQ,0) + isnull (aaa.RQ,0)) - (ISNULL ( aaa.IQ,0)) as Counts from ( select  * from #Closing pivot ( Sum(Auto_Count) for Doc_Type in ([OQ],[RQ],[IQ])) as rrr ) aaa where(ISNULL( aaa.OQ,0) + isnull (aaa.RQ,0)) - (ISNULL ( aaa.IQ,0)) >0  group by aaa.Item_Code, aaa.Auto_Sr_No ,aaa.Location_Code ,aaa.OQ,aaa.RQ ,aaa.IQ) bbb  group by bbb.Auto_Sr_No,bbb.Item_Code,bbb.Location_Code "

        '========================================================================================================================================================================================================================================== '================================================================================================================= '================================================================================================================= '================================================================================================================= '================================================================================================================= '================================================================================================================= '================================================================================================================= '==================================================================================================================================================================================================================================
        '========================================================================================================================================================================================================================================== '================================================================================================================= '================================================================================================================= '================================================================================================================= '================================================================================================================= '================================================================================================================= '================================================================================================================= '==================================================================================================================================================================================================================================

    End Sub
    Private Sub LoadData()
        Try
            If rbtnItemSelet.IsChecked AndAlso cbgItem.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one Item")
            End If
            If rbtnLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one Location")
            End If
            If chkSerialselect.IsChecked AndAlso cbgSerialNo.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least Serial No")
            End If
            Dim MainQry As String = ""

            '================================================================================================================================================================================================================ '================================================================================================================= '================================================================================================================= '================================================================================================================= '================================================================================================================= '================================================================================================================= '================================================================================================================= '==================================================================================================================================================================================================================================
            If rdbDetail.IsChecked = True Then

                Dim qry As String = "select (Document_Code) as [Document Code],(convert(varchar,Document_Date,103)) as [Document Date],  " & _
                                    " case when (In_Out_Type)='I' then 'In' else 'Out' end as [Type],(Auto_Sr_No) as [Auto Sr No]," & _
                                    " (TSPL_SERIAL_ITEM.Item_Code) as Item,(TSPL_ITEM_MASTER.Item_Desc) as [Item Desc],(TSPL_SERIAL_ITEM.Location_Code) as 'Location Code',tspl_Location_Master.Location_Desc as 'Location Name',case when (In_Out_Type)='I' then 1 else -1 end  AS 'Qty'" & _
                                    " from  TSPL_SERIAL_ITEM  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SERIAL_ITEM.Item_Code left Outer Join tspl_Location_Master on tspl_Location_Master.Location_Code= TSPL_SERIAL_ITEM.Location_Code left Outer Join TSPL_INVENTORY_MOVEMENT on TSPL_INVENTORY_MOVEMENT.Trans_Id=TSPL_SERIAL_ITEM.Against_Inv_Movement_Trans_Id" & _
                                    "  where TSPL_ITEM_MASTER.Is_Serial_Item = 1 and TSPL_SERIAL_ITEM.Document_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_SERIAL_ITEM.Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' "

                If rbtnItemSelet.IsChecked AndAlso cbgItem.CheckedValue.Count > 0 Then
                    qry += " and TSPL_SERIAL_ITEM.item_code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") " + Environment.NewLine
                End If
                If rbtnLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                    qry += " and TSPL_SERIAL_ITEM.location_code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") " + Environment.NewLine
                End If
                If chkSerialselect.IsChecked AndAlso cbgSerialNo.CheckedValue.Count > 0 Then
                    qry += " and TSPL_SERIAL_ITEM.auto_sr_no in (" + clsCommon.GetMulcallString(cbgSerialNo.CheckedValue) + ") " + Environment.NewLine
                End If

                If clsCommon.CompairString(cboType.Text, "available") = CompairStringResult.Equal Then
                    qry += " and TSPL_SERIAL_ITEM.in_out_type='I' "
                ElseIf clsCommon.CompairString(cboType.Text, "not available") = CompairStringResult.Equal Then
                    qry += " and TSPL_SERIAL_ITEM.in_out_type='O' "
                End If
                MainQry = " (" & qry & ") order by  TSPL_SERIAL_ITEM.Document_Date,[Document Code]"

                '================================================================================================================================================================================================================ '================================================================================================================= '================================================================================================================= '================================================================================================================= '================================================================================================================= '================================================================================================================= '================================================================================================================= '==================================================================================================================================================================================================================================
            ElseIf rdbSummary.IsChecked = True Then
                ''New Query Added By Nazia
                '========================================================================================================================================================================================================================================== '================================================================================================================= '================================================================================================================= '================================================================================================================= '================================================================================================================= '================================================================================================================= '================================================================================================================= '==================================================================================================================================================================================================================================
                '    Dim QrySummary As String = "select TSPL_SERIAL_ITEM.Item_Code as 'Item',MAX(TSPL_ITEM_MASTER.Item_Desc) as 'Item Desc',TSPL_SERIAL_ITEM.Location_Code as 'Location_Code'," & _
                '                        " Max(tspl_Location_Master.Location_Desc) as 'Location_Desc' ,sum(case In_Out_Type when 'I' Then 1 else 0 end) 'RecQty'," & _
                '                        " sum(case In_Out_Type when 'O' Then 1 else 0 end )'IssueQty',"

                '    '                    " (select (sum(case In_Out_Type When 'I' then 1 else 0 end  )-sum(case In_Out_Type When 'O' then 1  else 0 end  )) " & _
                '    '                    " from TSPL_SERIAL_ITEM as a  where a.Document_Date<'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "'  and a.Location_Code=TSPL_SERIAL_ITEM.Location_Code and		a.Item_Code=TSPL_SERIAL_ITEM.Item_Code "
                '    'If chkSerialselect.IsChecked AndAlso cbgSerialNo.CheckedValue.Count > 0 Then
                '    '    QrySummary += "  and a.auto_sr_no in (" + clsCommon.GetMulcallString(cbgSerialNo.CheckedValue) + ") " + Environment.NewLine
                '    'End If
                '    QrySummary += "(Select sum(Opening) as Opening from (select (sum(case In_Out_Type When 'I' then 1 else 0 end  )-sum(case In_Out_Type When 'O' then 1  else 0 end  )) as Opening from TSPL_SERIAL_ITEM as a  where a.Document_Date<'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "'   and a.Location_Code=TSPL_SERIAL_ITEM.Location_Code and a.Item_Code=TSPL_SERIAL_ITEM.Item_Code"
                '    '    If chkSerialselect.IsChecked AndAlso cbgSerialNo.CheckedValue.Count > 0 Then
                '    '    QrySummary += "  and a.auto_sr_no in (" + clsCommon.GetMulcallString(cbgSerialNo.CheckedValue) + ") " + Environment.NewLine
                '    'End If
                '    QrySummary += " group by a.Auto_Sr_No ,a.Item_Code ,a.Location_Code having (sum(case In_Out_Type When 'I' then 1 else 0 end  )-sum(case In_Out_Type When 'O' then 1  else 0 end  )) >0) finalOpening" & _
                '                " ) as 'Opening' from  TSPL_SERIAL_ITEM  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SERIAL_ITEM.Item_Code" & _
                '                    " left Outer Join tspl_Location_Master on tspl_Location_Master.Location_Code= TSPL_SERIAL_ITEM.Location_Code left Outer Join TSPL_INVENTORY_MOVEMENT on TSPL_INVENTORY_MOVEMENT.Trans_Id=TSPL_SERIAL_ITEM.Against_Inv_Movement_Trans_Id" & _
                '                    "  where TSPL_ITEM_MASTER.Is_Serial_Item = 1 and TSPL_SERIAL_ITEM.Document_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_SERIAL_ITEM.Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' "

                'If rbtnItemSelet.IsChecked AndAlso cbgItem.CheckedValue.Count > 0 Then
                '    QrySummary += " and TSPL_SERIAL_ITEM.item_code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") " + Environment.NewLine
                'End If
                'If rbtnLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                '    QrySummary += " and TSPL_SERIAL_ITEM.location_code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") " + Environment.NewLine
                'End If
                '    'If chkSerialselect.IsChecked AndAlso cbgSerialNo.CheckedValue.Count > 0 Then
                '    '    QrySummary += " and TSPL_SERIAL_ITEM.auto_sr_no in (" + clsCommon.GetMulcallString(cbgSerialNo.CheckedValue) + ") " + Environment.NewLine
                '    'End If
                'QrySummary += "group by TSPL_SERIAL_ITEM.Item_Code ,TSPL_SERIAL_ITEM.Location_Code "
                'MainQry = " Select final.Item,final.[Item Desc],final.[Location_Code] as 'Location Code',final.[Location_Desc] as 'Location Name',isnull(final.Opening,0) as 'Opening',final.[RecQty] as 'Received Qty',final.[IssueQty] as 'Issue Qty', isnull(Opening,0)+RecQty-IssueQty as Closing from (" & QrySummary & ")  as final order by final.Location_Code,final.[Item] "

                MainQry = "Select Item,max(FinalQry.[Item Desc]) as [Item Desc],[Location Code] as 'Location Code',max(FinalQry.[Location Name]) as 'Location Name',sum(Opening) as Opening, " & _
                " sum([Received Qty] ) as [Received Qty], sum([Issue Qty] ) as [Issue Qty],sum(Opening)+sum([Received Qty] )- sum([Issue Qty] )  as Closing " & _
                " from ( " & _
                " Select final.Item,final.[Item Desc],final.[Location_Code] as 'Location Code',final.[Location_Desc] as 'Location Name',isnull(Opening,0)+RecQty-IssueQty as 'Opening',0 as 'Received Qty',0 as 'Issue Qty', 0 as Closing from (select TSPL_SERIAL_ITEM.Item_Code as 'Item',MAX(TSPL_ITEM_MASTER.Item_Desc) as 'Item Desc',TSPL_SERIAL_ITEM.Location_Code as 'Location_Code', Max(tspl_Location_Master.Location_Desc) as 'Location_Desc' ,sum(case In_Out_Type when 'I' Then 1 else 0 end) 'RecQty', sum(case In_Out_Type when 'O' Then 1 else 0 end )'IssueQty',0 as 'Opening' from  TSPL_SERIAL_ITEM  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SERIAL_ITEM.Item_Code left Outer Join tspl_Location_Master on tspl_Location_Master.Location_Code= TSPL_SERIAL_ITEM.Location_Code left Outer Join TSPL_INVENTORY_MOVEMENT on TSPL_INVENTORY_MOVEMENT.Trans_Id=TSPL_SERIAL_ITEM.Against_Inv_Movement_Trans_Id " & _
                " where TSPL_ITEM_MASTER.Is_Serial_Item = 1 and TSPL_SERIAL_ITEM.Document_Date<'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' "

                If rbtnItemSelet.IsChecked AndAlso cbgItem.CheckedValue.Count > 0 Then
                    MainQry += " and TSPL_SERIAL_ITEM.item_code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") " + Environment.NewLine
                End If
                If rbtnLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                    MainQry += " and TSPL_SERIAL_ITEM.location_code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") " + Environment.NewLine
                End If
                If chkSerialselect.IsChecked AndAlso cbgSerialNo.CheckedValue.Count > 0 Then
                    MainQry += " and TSPL_SERIAL_ITEM.auto_sr_no in (" + clsCommon.GetMulcallString(cbgSerialNo.CheckedValue) + ") " + Environment.NewLine
                End If

                MainQry += " group by TSPL_SERIAL_ITEM.Item_Code ,TSPL_SERIAL_ITEM.Location_Code )  as final " + Environment.NewLine & _
                " union all " + Environment.NewLine & _
                " Select final.Item,final.[Item Desc],final.[Location_Code] as 'Location Code',final.[Location_Desc] as 'Location Name',isnull(final.Opening,0) as 'Opening',final.[RecQty] as 'Received Qty',final.[IssueQty] as 'Issue Qty', isnull(Opening,0)+RecQty-IssueQty as Closing from (select TSPL_SERIAL_ITEM.Item_Code as 'Item',MAX(TSPL_ITEM_MASTER.Item_Desc) as 'Item Desc',TSPL_SERIAL_ITEM.Location_Code as 'Location_Code', Max(tspl_Location_Master.Location_Desc) as 'Location_Desc' ,sum(case In_Out_Type when 'I' Then 1 else 0 end) 'RecQty', sum(case In_Out_Type when 'O' Then 1 else 0 end )'IssueQty',0 as 'Opening' from  TSPL_SERIAL_ITEM  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SERIAL_ITEM.Item_Code left Outer Join tspl_Location_Master on tspl_Location_Master.Location_Code= TSPL_SERIAL_ITEM.Location_Code left Outer Join TSPL_INVENTORY_MOVEMENT on TSPL_INVENTORY_MOVEMENT.Trans_Id=TSPL_SERIAL_ITEM.Against_Inv_Movement_Trans_Id " & _
                " where TSPL_ITEM_MASTER.Is_Serial_Item = 1 and TSPL_SERIAL_ITEM.Document_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_SERIAL_ITEM.Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' " + Environment.NewLine

                If rbtnItemSelet.IsChecked AndAlso cbgItem.CheckedValue.Count > 0 Then
                    MainQry += " and TSPL_SERIAL_ITEM.item_code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") " + Environment.NewLine
                End If
                If rbtnLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                    MainQry += " and TSPL_SERIAL_ITEM.location_code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") " + Environment.NewLine
                End If
                If chkSerialselect.IsChecked AndAlso cbgSerialNo.CheckedValue.Count > 0 Then
                    MainQry += " and TSPL_SERIAL_ITEM.auto_sr_no in (" + clsCommon.GetMulcallString(cbgSerialNo.CheckedValue) + ") " + Environment.NewLine
                End If
                MainQry += " group by TSPL_SERIAL_ITEM.Item_Code ,TSPL_SERIAL_ITEM.Location_Code )  as final) as FinalQry " & _
               " group by Item ,[Location Code] order by [Location Code],Item "

            End If

            If MainQry IsNot Nothing AndAlso MainQry.Length > 0 Then

                Dim dt As DataTable = clsDBFuncationality.GetDataTable(MainQry)
                gv1.DataSource = Nothing
                gv1.Columns.Clear()
                gv1.Rows.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.ShowFilteringRow = True
                gv1.ShowGroupPanel = False
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    'Throw New Exception("No Data Found to Display")
                    clsCommon.MyMessageBoxShow(Me, "No Data found to display", Me.Text)
                Else
                    gv1.DataSource = dt
                    SetGridFormationOFGV1()
                    RadPageView1.SelectedPage = RadPageViewPage2
                    EnableDisableCtrl(False)
                End If
            End If

            '========================================================================================================================================================================================================================================== '================================================================================================================= '================================================================================================================= '================================================================================================================= '================================================================================================================= '================================================================================================================= '================================================================================================================= '==================================================================================================================================================================================================================================
            'CLOSING DETIALS TAB LOGIC 
            '================================================================================================================================================================================================================ '================================================================================================================= '================================================================================================================= '================================================================================================================= '================================================================================================================= '================================================================================================================= '================================================================================================================= '==================================================================================================================================================================================================================================
            Dim QrySerialDetials As String = Nothing
            Dim rptGridItems As List(Of String) = New List(Of String)
            Dim rptGridItemsLoc As List(Of String) = New List(Of String)

            If rcbShowSerialNo.Checked = True Then

                'Dim qry As String = String.Empty
                'If rbtnItemSelet.IsChecked AndAlso cbgItem.CheckedValue.Count > 0 Then
                '    qry += " and TSPL_SERIAL_ITEM.item_code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") " + Environment.NewLine
                'End If
                ''If rbtnLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                ''    qry += " and TSPL_SERIAL_ITEM.location_code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") " + Environment.NewLine
                ''End If
                'If chkSerialselect.IsChecked AndAlso cbgSerialNo.CheckedValue.Count > 0 Then
                '    qry += " and TSPL_SERIAL_ITEM.auto_sr_no in (" + clsCommon.GetMulcallString(cbgSerialNo.CheckedValue) + ") " + Environment.NewLine
                'End If
                ''========================================================================================================================================================================================================================================== '================================================================================================================= '================================================================================================================= '================================================================================================================= '================================================================================================================= '================================================================================================================= '================================================================================================================= '==================================================================================================================================================================================================================================
                ''KUNAL > CLIENT : KDIL > TICKET : UNKNOWN (DONE ON VERBAL DISCUSSION) > BUG : DATA IN CLOSING DETAIL WAS NOT COMING ON SAME START AND END DATE RANGE > STATUS : FIXED
                ''========================================================================================================================================================================================================================================== '================================================================================================================= '================================================================================================================= '================================================================================================================= '================================================================================================================= '================================================================================================================= '================================================================================================================= '==================================================================================================================================================================================================================================
                'QrySerialDetials += " create table #Closing (Auto_Sr_No varchar(100),Auto_Count int, Doc_Type varchar(50) COLLATE DATABASE_DEFAULT NULL ,  Item_Code varchar(100) COLLATE DATABASE_DEFAULT NULL,Location_Code  varchar(100) COLLATE DATABASE_DEFAULT NULL)  "
                'QrySerialDetials += " INSERT INTO  #Closing (Auto_Sr_No, Auto_Count, Doc_Type, Item_Code, Location_Code)  SELECT Auto_Sr_No AS Auto_Sr_No, SUM(case when In_Out_Type = 'I' THEN 1 ELSE -1 END )  AS Auto_Count,  'OQ' AS Doc_Type,  Item_Code,  Location_Code  FROM TSPL_SERIAL_ITEM  where Document_Date < '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' GROUP BY Auto_Sr_No ,Item_Code,Location_Code  having SUM(case when In_Out_Type = 'I' THEN 1 ELSE -1 END ) > 0 " + qry + " "
                'If rbtnLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                '    QrySerialDetials += " and TSPL_SERIAL_ITEM.location_code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") " + Environment.NewLine
                'End If
                'QrySerialDetials += " INSERT INTO  #Closing (Auto_Sr_No,Auto_Count,Doc_Type,Item_Code,Location_Code)     select  Auto_Sr_No as Auto_Sr_No, count (Auto_Sr_No) as Auto_Count, 'RQ' as Doc_Type, Item_Code,Location_Code from TSPL_SERIAL_ITEM group by Auto_Sr_No ,Document_Date,Item_Code, Location_Code,In_Out_Type having   Document_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'    and  In_Out_Type ='I' and  In_Out_Type <> 'O'  " + qry + " "
                'If rbtnLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                '    QrySerialDetials += " and TSPL_SERIAL_ITEM.location_code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") " + Environment.NewLine
                'End If
                'QrySerialDetials += " INSERT INTO  #Closing (Auto_Sr_No,Auto_Count,Doc_Type,Item_Code,Location_Code)     select  Auto_Sr_No as Auto_Sr_No, count (Auto_Sr_No) as Auto_Count, 'IQ' as Doc_Type, Item_Code,Location_Code from TSPL_SERIAL_ITEM group by Auto_Sr_No ,Document_Date,Item_Code, Location_Code,In_Out_Type having   Document_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'     and   In_Out_Type ='O'  " + qry + " "
                'If rbtnLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                '    QrySerialDetials += " and TSPL_SERIAL_ITEM.location_code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") " + Environment.NewLine
                'End If

                'If gv1.Rows.Count > 0 Then
                '    For Each rowItem As GridViewRowInfo In gv1.Rows
                '        rptGridItems.Add(rowItem.Cells("Item").Value)
                '        rptGridItemsLoc.Add(rowItem.Cells("Location Code").Value)

                '    Next
                '    qry += " and xxx.item_code in (" + clsCommon.GetMulcallString(rptGridItems.Distinct().ToList) + ") " + Environment.NewLine
                '    qry += " and xxx.location_code in (" + clsCommon.GetMulcallString(rptGridItemsLoc.Distinct().ToList) + ") " + Environment.NewLine
                'End If
                'If qry IsNot Nothing AndAlso qry.Length > 0 Then
                '    QrySerialDetials += " select xxx.Item_Code ,max(TSPL_ITEM_MASTER.Item_Desc ) as Item_Desc,xxx.Location_Code, max (TSPL_LOCATION_MASTER.Location_Desc) as Location_Desc ,xxx.Auto_Sr_No, max(xxx.Qty ) as Qty from (  select bbb.Item_Code ,bbb.Location_Code ,bbb.Auto_Sr_No,sum (bbb .Counts) as Qty from (select aaa.Item_Code,aaa.Location_Code, aaa.Auto_Sr_No,sum(ISNULL( aaa.OQ,0) + isnull (aaa.RQ,0)) - (ISNULL ( aaa.IQ,0)) as Counts from ( select  * from #Closing pivot ( Sum(Auto_Count) for Doc_Type in ([OQ],[RQ],[IQ])) as rrr ) aaa where(ISNULL( aaa.OQ,0) + isnull (aaa.RQ,0)) - (ISNULL ( aaa.IQ,0)) >0  group by aaa.Item_Code, aaa.Auto_Sr_No ,aaa.Location_Code ,aaa.OQ,aaa.RQ ,aaa.IQ) bbb group by bbb.Auto_Sr_No,bbb.Item_Code,bbb.Location_Code)xxx left outer join TSPL_ITEM_MASTER on  TSPL_ITEM_MASTER.Item_Code =xxx.Item_Code left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=xxx.Location_Code  WHERE 1=1  " + qry + ""

                '    QrySerialDetials += " group by xxx.Auto_Sr_No,xxx.Item_Code,xxx.Location_Code  order by xxx.Item_Code ,  xxx.Location_Code"
                'End If

                ''richa agarwal 04 Aug, 2017

                'Dim qry As String = "  Select final.Item as Item_Code,final.[Item Desc] as Item_Desc,final.[Location Code] as Location_Code,final.[Location Name] as Location_Desc,final.[Auto Sr No] as Auto_Sr_No,final.Qty from ( " & _
                Dim qry As String = " Select Item as Item_Code,max([Item Desc] ) as Item_Desc,[Location Code] as Location_Code,max([Location Name]) as Location_Desc,[Auto Sr No] as Auto_Sr_No,sum(qty) as Qty from (" & _
                                "select (Document_Code) as [Document Code],(convert(varchar,Document_Date,103)) as [Document Date],  " & _
                                  " case when (In_Out_Type)='I' then 'In' else 'Out' end as [Type],(Auto_Sr_No) as [Auto Sr No]," & _
                                  " (TSPL_SERIAL_ITEM.Item_Code) as Item,(TSPL_ITEM_MASTER.Item_Desc) as [Item Desc],(TSPL_SERIAL_ITEM.Location_Code) as 'Location Code',tspl_Location_Master.Location_Desc as 'Location Name',case when (In_Out_Type)='I' then 1 else -1 end  AS 'Qty'" & _
                                  " from  TSPL_SERIAL_ITEM  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SERIAL_ITEM.Item_Code left Outer Join tspl_Location_Master on tspl_Location_Master.Location_Code= TSPL_SERIAL_ITEM.Location_Code left Outer Join TSPL_INVENTORY_MOVEMENT on TSPL_INVENTORY_MOVEMENT.Trans_Id=TSPL_SERIAL_ITEM.Against_Inv_Movement_Trans_Id" & _
                                  "  where TSPL_ITEM_MASTER.Is_Serial_Item = 1 and TSPL_SERIAL_ITEM.Document_Date<'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' "

                If rbtnItemSelet.IsChecked AndAlso cbgItem.CheckedValue.Count > 0 Then
                    qry += " and TSPL_SERIAL_ITEM.item_code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") " + Environment.NewLine
                End If
                If rbtnLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                    qry += " and TSPL_SERIAL_ITEM.location_code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") " + Environment.NewLine
                End If
                If chkSerialselect.IsChecked AndAlso cbgSerialNo.CheckedValue.Count > 0 Then
                    qry += " and TSPL_SERIAL_ITEM.auto_sr_no in (" + clsCommon.GetMulcallString(cbgSerialNo.CheckedValue) + ") " + Environment.NewLine
                End If

                If clsCommon.CompairString(cboType.Text, "available") = CompairStringResult.Equal Then
                    qry += " and TSPL_SERIAL_ITEM.in_out_type='I' "
                ElseIf clsCommon.CompairString(cboType.Text, "not available") = CompairStringResult.Equal Then
                    qry += " and TSPL_SERIAL_ITEM.in_out_type='O' "
                End If

                qry += Environment.NewLine + " Union All " + Environment.NewLine

                qry += "select (Document_Code) as [Document Code],(convert(varchar,Document_Date,103)) as [Document Date],  " & _
                                 " case when (In_Out_Type)='I' then 'In' else 'Out' end as [Type],(Auto_Sr_No) as [Auto Sr No]," & _
                                 " (TSPL_SERIAL_ITEM.Item_Code) as Item,(TSPL_ITEM_MASTER.Item_Desc) as [Item Desc],(TSPL_SERIAL_ITEM.Location_Code) as 'Location Code',tspl_Location_Master.Location_Desc as 'Location Name',case when (In_Out_Type)='I' then 1 else -1 end  AS 'Qty'" & _
                                 " from  TSPL_SERIAL_ITEM  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SERIAL_ITEM.Item_Code left Outer Join tspl_Location_Master on tspl_Location_Master.Location_Code= TSPL_SERIAL_ITEM.Location_Code left Outer Join TSPL_INVENTORY_MOVEMENT on TSPL_INVENTORY_MOVEMENT.Trans_Id=TSPL_SERIAL_ITEM.Against_Inv_Movement_Trans_Id" & _
                                 "  where TSPL_ITEM_MASTER.Is_Serial_Item = 1 and TSPL_SERIAL_ITEM.Document_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_SERIAL_ITEM.Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' "

                If rbtnItemSelet.IsChecked AndAlso cbgItem.CheckedValue.Count > 0 Then
                    qry += " and TSPL_SERIAL_ITEM.item_code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") " + Environment.NewLine
                End If
                If rbtnLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                    qry += " and TSPL_SERIAL_ITEM.location_code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") " + Environment.NewLine
                End If
                If chkSerialselect.IsChecked AndAlso cbgSerialNo.CheckedValue.Count > 0 Then
                    qry += " and TSPL_SERIAL_ITEM.auto_sr_no in (" + clsCommon.GetMulcallString(cbgSerialNo.CheckedValue) + ") " + Environment.NewLine
                End If

                If clsCommon.CompairString(cboType.Text, "available") = CompairStringResult.Equal Then
                    qry += " and TSPL_SERIAL_ITEM.in_out_type='I' "
                ElseIf clsCommon.CompairString(cboType.Text, "not available") = CompairStringResult.Equal Then
                    qry += " and TSPL_SERIAL_ITEM.in_out_type='O' "
                End If

                qry += " ) final group by [Auto Sr No] ,Item ,[Location Code] having sum(qty)<>0 "

                Dim dtSerial As DataTable = clsDBFuncationality.GetDataTable(qry)
                'Dim dtSerial As DataTable = clsDBFuncationality.GetDataTable(QrySerialDetials)
                gvSerial.DataSource = Nothing
                gvSerial.Columns.Clear()
                gvSerial.Rows.Clear()
                If dtSerial Is Nothing OrElse dtSerial.Rows.Count <= 0 Then
                    Return
                End If

                gvSerial.DataSource = dtSerial
                gvSerial.BestFitColumns()
                SetGridFormationOfGVSummary()

            End If
            '========================================================================================================================================================================================================================================== '================================================================================================================= '================================================================================================================= '================================================================================================================= '================================================================================================================= '================================================================================================================= '================================================================================================================= '==================================================================================================================================================================================================================================


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub SetGridFormationOfGVSummary()
        Try
            gvSerial.MasterTemplate.SummaryRowsBottom.Clear()
            Dim gvSerialItemCountAtFoot As New GridViewSummaryRowItem()
            If rcbShowSerialNo.CheckState = CheckState.Checked Then
                If gvSerial.Rows.Count > 0 Then
                    Dim itms As New GridViewSummaryItem("Qty", "{0:F2}", GridAggregateFunction.Sum)
                    gvSerialItemCountAtFoot.Add(itms)
                    gvSerial.MasterTemplate.SummaryRowsBottom.Add(gvSerialItemCountAtFoot)
                    gvSerial.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
                End If
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
            gv1.Columns(ii).IsVisible = True
            gv1.Columns(ii).Width = 125
        Next

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        If rdbDetail.IsChecked Then
            Dim item1 As New GridViewSummaryItem("Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        Else
            Dim item1 As New GridViewSummaryItem("Opening", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("Received Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            Dim item3 As New GridViewSummaryItem("Issue Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("Closing", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        End If




    End Sub



    Private Sub gv1_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.ViewCellFormatting
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub

    Sub print(ByVal exporter As EnumExportTo)
        Try
            LoadData()
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmItemSerialTrackingReport & "'"))

                If rbtnItemSelet.IsChecked Then
                    Dim strLoca As String = ""
                    For Each Str As String In cbgItem.CheckedDisplayMember
                        If clsCommon.myLen(strLoca) > 0 Then
                            strLoca += ", "
                        End If
                        strLoca += Str
                    Next
                    arrHeader.Add("Item : " + strLoca)
                End If
                If rbtnLocationSelect.IsChecked Then
                    Dim strLoca As String = ""
                    For Each Str As String In cbgLocation.CheckedDisplayMember
                        If clsCommon.myLen(strLoca) > 0 Then
                            strLoca += ", "
                        End If
                        strLoca += Str
                    Next
                    arrHeader.Add("Location : " + strLoca)
                End If
                If chkSerialselect.IsChecked Then
                    Dim strLoca As String = ""
                    For Each Str As String In cbgSerialNo.CheckedDisplayMember
                        If clsCommon.myLen(strLoca) > 0 Then
                            strLoca += ", "
                        End If
                        strLoca += Str
                    Next
                    arrHeader.Add("Srial No : " + strLoca)
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
                    'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                    transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
                    'clsCommon.MyExportToExcelGrid("Item Serial Tracking Report (" + cboType.Text + ")", gv1, arrHeader, Me.Text)
                Else
                    clsCommon.MyExportToPDF("Item Serial Tracking Report (" + cboType.Text + ")", gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        print(EnumExportTo.Excel)

    End Sub

    Private Sub btnPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        print(EnumExportTo.PDF)
    End Sub

    Private Sub rbtnSalesmanAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnItemAll.ToggleStateChanged, rbtnItemSelet.ToggleStateChanged
        cbgItem.Enabled = rbtnItemSelet.IsChecked
    End Sub
    Private Sub Reset()
        Try

            EnableDisableCtrl(True)
            gv1.DataSource = Nothing
            gvSerial.DataSource = Nothing

            gv1.Columns.Clear()
            gv1.Rows.Clear()
            gv1.GroupDescriptors.Clear()

            gvSerial.Columns.Clear()
            gvSerial.Rows.Clear()
            gvSerial.GroupDescriptors.Clear()

            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gvSerial.MasterTemplate.SummaryRowsBottom.Clear()

            rcbShowSerialNo.Checked = False
            RadPageView1.SelectedPage = RadPageViewPage1

            rbtnItemAll.IsChecked = True
            chkSerialAll.IsChecked = True
            rbtnLocationAll.IsChecked = True

            txtFromDate.Value = Today.AddMonths(-1)
            txtToDate.Value = clsCommon.GETSERVERDATE()

            rdbDetail.IsChecked = False
            rdbSummary.IsChecked = True
            rcbShowSerialNo.CheckState = CheckState.Unchecked
            cboType.SelectedValue = "Both"

            If rcbShowSerialNo.CheckState = CheckState.Checked Then
                RadPageView1.Pages("RadPageViewPage3").Item.Visibility = ElementVisibility.Visible
            Else
                RadPageView1.Pages("RadPageViewPage3").Item.Visibility = ElementVisibility.Collapsed
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub RadButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton3.Click
        Reset()
    End Sub

    Sub EnableDisableCtrl(ByVal val As Boolean)
        txtFromDate.Enabled = val
        txtToDate.Enabled = val
        RadGroupBox1.Enabled = val
        RadGroupBox2.Enabled = val
        RadGroupBox4.Enabled = val
        cboType.Enabled = val

    End Sub


    Private Sub rbtnLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnLocationAll.ToggleStateChanged, rbtnLocationSelect.ToggleStateChanged
        cbgLocation.Enabled = rbtnLocationSelect.IsChecked
    End Sub

    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
        Try
            If rdbDetail.IsChecked Then
                Dim strLocation, strItem, strSerialNo As String

                strSerialNo = gv1.CurrentRow.Cells("Auto Sr No").Value
                strLocation = gv1.CurrentRow.Cells("Location Code").Value
                strItem = gv1.CurrentRow.Cells("Item").Value

                Dim strQuery = "select Item_Code as Item,Auto_Sr_No as [Auto Sr No], " & _
                "Document_Code as Document,Document_Date as Date,Document_Type as Type, " & _
                "Location_Code as Location from TSPL_SERIAL_ITEM where Auto_Sr_No='" & strSerialNo & "' and Item_Code='" & strItem & "' and Location_Code='" & strLocation & "'"
                Dim frmStock As New FrmStockDetail
                frmStock.LoadSerialData(strQuery)
                frmStock.Show()

            End If


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub chkSerialAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkSerialAll.ToggleStateChanged
        cbgSerialNo.Enabled = chkSerialselect.IsChecked
    End Sub

    Private Sub FrmItemSerialTrackingReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            SetUserMgmtNew()
            LoadSerialNo()
            LoadItem()
            LoadLocation()
            LoadType()
            ButtonToolTip.SetToolTip(RadButton2, "Press Alt+C Close the Window")
            Reset()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rdbSummary_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rdbSummary.ToggleStateChanged
        If rdbSummary.IsChecked = True Then
            cboType.Enabled = False
            rcbShowSerialNo.Enabled = True
            RadGroupBox4.Enabled = False
        Else
            cboType.Enabled = True
            rcbShowSerialNo.Enabled = False
            RadGroupBox4.Enabled = True
        End If
    End Sub

    Private Sub rdbDetail_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rdbDetail.ToggleStateChanged
        Try

            If rdbDetail.IsChecked = True Then

                rcbShowSerialNo.Enabled = False
                RadGroupBox4.Enabled = True
                rcbShowSerialNo.Checked = False ' prabhakar 18/11/2016

                cboType.Enabled = True
                gvSerial.DataSource = Nothing
                gvSerial.Columns.Clear()
                gvSerial.Rows.Clear()
                gvSerial.GroupDescriptors.Clear()
                gvSerial.MasterTemplate.SummaryRowsBottom.Clear()

                If rcbShowSerialNo.CheckState = CheckState.Checked Then
                    RadPageView1.Pages("RadPageViewPage3").Item.Visibility = ElementVisibility.Visible
                Else
                    RadPageView1.Pages("RadPageViewPage3").Item.Visibility = ElementVisibility.Collapsed
                End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub



    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        If gvSerial.Rows.Count > 0 Then
            printSerialDetails(EnumExportTo.Excel)
        Else
            common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
        End If

    End Sub
    Sub printSerialDetails(ByVal exporter As EnumExportTo)
        Try
            LoadData()
            If gvSerial.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmItemSerialTrackingReport & "'"))
                If rbtnItemSelet.IsChecked Then
                    Dim strLoca As String = ""
                    For Each Str As String In cbgItem.CheckedDisplayMember
                        If clsCommon.myLen(strLoca) > 0 Then
                            strLoca += ", "
                        End If
                        strLoca += Str
                    Next
                    arrHeader.Add("Item : " + strLoca)
                End If
                If rbtnLocationSelect.IsChecked Then
                    Dim strLoca As String = ""
                    For Each Str As String In cbgLocation.CheckedDisplayMember
                        If clsCommon.myLen(strLoca) > 0 Then
                            strLoca += ", "
                        End If
                        strLoca += Str
                    Next
                    arrHeader.Add("Location : " + strLoca)
                End If
                If chkSerialselect.IsChecked Then
                    Dim strLoca As String = ""
                    For Each Str As String In cbgSerialNo.CheckedDisplayMember
                        If clsCommon.myLen(strLoca) > 0 Then
                            strLoca += ", "
                        End If
                        strLoca += Str
                    Next
                    arrHeader.Add("Srial No : " + strLoca)
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
                    'transportSql.exportdataChilRows(gvSerial, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                    transportSql.QuickExportToExcel(gvSerial, "", Me.Text, , arrHeader)
                    'clsCommon.MyExportToExcelGrid("Item Serial Details Tracking Report", gvSerial, arrHeader, Me.Text)

                Else
                    clsCommon.MyExportToPDF("Item Serial Details Tracking Report", gvSerial, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        If gvSerial.Rows.Count > 0 Then
            printSerialDetails(EnumExportTo.PDF)
        Else
            common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
        End If

    End Sub


    Private Sub rcbShowSerialNo_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rcbShowSerialNo.ToggleStateChanged
        Try
            If rcbShowSerialNo.CheckState = CheckState.Checked Then
                RadPageView1.Pages("RadPageViewPage3").Item.Visibility = ElementVisibility.Visible
            Else
                RadPageView1.Pages("RadPageViewPage3").Item.Visibility = ElementVisibility.Collapsed
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
