Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
'preeti gupta ticket no.[BM00000005034]
'' work to be done agaist ticket no. BHA/21/11/18-000691
Public Class RptMilkStockLegderSummary
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim arrLoc As String = Nothing
    Dim ItemStructureMandatoryOnWeightConversion As Boolean = False
    Private Sub LOCATIONRIGTHS()
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData()

            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptMilkStockLedgerSummary)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub
    Sub LoadMCC()
        Dim qry As String = Nothing
        If clsCommon.myLen(arrLoc) > 0 Then

            qry = "select TSPL_LOCATION_MASTER.Location_Code as [Code] ,TSPL_LOCATION_MASTER .Location_Desc as [Name] from TSPL_LOCATION_MASTER where isnull(CSA_Type,'N') ='N' and (isnull(GIT_Type,'N')='N' or isnull(GIT_Type,'N')='') and isnull(Is_Consumption_Location,0) =0  and isnull(Rejected_type,'N') ='N'  and isnull(Is_Sub_Location,'')<>'Y' and Location_Code in (" + arrLoc + ") "

        Else
            btnGo.Enabled = False
        End If
        cbgMCC.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgMCC.ValueMember = "Code"
        cbgMCC.DisplayMember = "Name"

    End Sub
    Sub Reset()
        LOCATIONRIGTHS()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        LoadMCC()
        cboUnit.Text = "Kg"
        chkMCCAll.CheckState = CheckState.Checked
        'chkAllSubLocation.CheckState = CheckState.Checked
        If chkMCCAll.IsChecked Then
            cbgMCC.CheckedAll()
        Else
            cbgMCC.UnCheckedAll()
        End If
        gv.DataSource = Nothing
        gv2.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub RptMilkStockLegderSummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ItemStructureMandatoryOnWeightConversion = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ItemStructureMandatoryOnWeightConversion, clsFixedParameterCode.ItemStructureMandatoryOnWeightConversion, Nothing)) = 1, True, False))
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+N Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+R Adding New")
        RadPageView1.SelectedPage = RadPageViewPage1
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        Reset()
    End Sub
    Sub printSummary(ByVal exporter As EnumExportTo)
        Try
            'Ticket No-TEC/13/03/19-000451 ,sanjay
            If gv.Rows.Count <= 0 Then
                Throw New Exception("No data in Summary Grid for Export.")
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            If chkMCCSelect.IsChecked Then
                Dim strMCCName As String = ""
                For Each StrName As String In cbgMCC.CheckedDisplayMember
                    If clsCommon.myLen(strMCCName) > 0 Then
                        strMCCName += ", "
                    End If
                    strMCCName += StrName
                Next
                Dim strMCCCode As String = ""
                For Each StrCode As String In cbgMCC.CheckedValue
                    If clsCommon.myLen(strMCCCode) > 0 Then
                        strMCCCode += ", "
                    End If
                    strMCCCode += StrCode
                Next
                arrHeader.Add((" MCC Name: " + strMCCName + " "))
            End If
            transportSql.applyExportTemplate(gv, MyBase.Form_ID + gv.Name)
            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Milk Stock Ledger(Summary)", gv, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Milk Stock Ledger(Summary))", gv, arrHeader, Me.Text, MyBase.Form_ID + gv.Name, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Sub printDetails(ByVal exporter As EnumExportTo)
        Try
            If gv2.Rows.Count <= 0 Then
                Throw New Exception("No data in Detail Grid for Export.")
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            If chkMCCSelect.IsChecked Then
                Dim strMCCName As String = ""
                For Each StrName As String In cbgMCC.CheckedDisplayMember
                    If clsCommon.myLen(strMCCName) > 0 Then
                        strMCCName += ", "
                    End If
                    strMCCName += StrName
                Next
                Dim strMCCCode As String = ""
                For Each StrCode As String In cbgMCC.CheckedValue
                    If clsCommon.myLen(strMCCCode) > 0 Then
                        strMCCCode += ", "
                    End If
                    strMCCCode += StrCode
                Next
                arrHeader.Add((" MCC Name: " + strMCCName + " "))
            End If
            transportSql.applyExportTemplate(gv2, MyBase.Form_ID)
            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Milk Stock Ledger(Details)", gv2, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Milk Stock Ledger(Details))", gv2, arrHeader, Me.Text, MyBase.Form_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Public Sub Load_Report()
        If txtFromDate.Value > txtToDate.Value Then
            common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater then to Date", Me.Text)
            txtFromDate.Focus()
            Exit Sub
        End If
        If chkMCCSelect.IsChecked AndAlso cbgMCC.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select atleast single MCC or select all.", Me.Text)
            Exit Sub
        End If

        Dim whrCls As String = ""
        If cbgMCC.CheckedValue.Count > 0 Then
            whrCls = " and  (TSPL_INVENTORY_MOVEMENT_NEW.Location_Code   IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ")  or TSPL_INVENTORY_MOVEMENT_NEW.main_location   IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") )  "
        Else
            whrCls = " and  (TSPL_INVENTORY_MOVEMENT_NEW.Location_Code   IN (" + clsCommon.GetMulcallString(cbgMCC.AllValue) + ")  or TSPL_INVENTORY_MOVEMENT_NEW.main_location   IN (" + clsCommon.GetMulcallString(cbgMCC.AllValue) + ") )  "
        End If
        Dim whrCls1 As String = ""

        Dim FinalWhrCls As String = whrCls & whrCls1

        Dim sQuery As String = "select finaly .* ,case when isnull(finaly.Closing_Qty,0)= 0 then 0 else  convert(decimal(18,2),finaly.Closing_Value /finaly .Closing_Qty)  end  as Closing_Rate    from (select  coalesce(final .OpenLocationCode," & Environment.NewLine & _
        " coalesce(final .inLocationCode," & Environment.NewLine & _
        " final .outLocationCode)) as Loc_code ," & Environment.NewLine & _
        " coalesce(final .OpenLocationDesc," & Environment.NewLine & _
        " coalesce(final .InLocationDesc," & Environment.NewLine & _
        " final .OutLocationDesc)) as Loc_desc  ,final.OpenDate ,isnull(convert(Decimal(18,2),final .New_Open_Qty ),0) as New_Open_Qty,isnull(convert(decimal(18,2),final.open_Rate),0) as open_Rate,isnull(final .Open_FAT_Kg,0) as Open_FAT_Kg ,isnull(final.Open_SNF_Kg,0) as Open_SNF_Kg ,isnull(convert(decimal(18,2),final.Opening_Value ),0) as Opening_Value,isnull(convert(decimal(18,2),final.New_In_Qty),0 ) as New_In_Qty,isnull(Convert(decimal(18,2),final.In_Rate ),0) as In_Rate,isnull(final .In_FAT_Kg,0)  as In_FAT_Kg,isnull(final .In_SNF_Kg ,0)  as In_SNF_Kg" & Environment.NewLine & _
        " ,isnull(convert(decimal(18,2),final.In_Value ),0) as In_Value,isnull(convert(decimal(18,2),final.New_Out_Qty ),0) as New_Out_Qty,isnull(convert(decimal(18,2),final .out_open_Rate),0)  as out_open_Rate,isnull(final.Out_Fat_Kg ,0) as Out_Fat_Kg,isnull(final.Out_SNF_Kg  ,0) as Out_SNF_Kg" & Environment.NewLine & _
        " ,isnull(convert(decimal(18,2),final.OutValue),0) as OutValue,isnull(convert(Decimal(18,2),final .New_Open_Qty ),0)+isnull(convert(decimal(18,2),final.New_In_Qty),0 )-isnull(convert(decimal(18,2),final.New_Out_Qty ),0) as Closing_Qty,isnull(convert(decimal(18,2),final.Opening_Value ),0)+isnull(convert(decimal(18,2),final.In_Value ),0)-isnull(convert(decimal(18,2),final.OutValue ),0) as Closing_Value  ,isnull(convert(decimal(18,2),final.Open_FAT_Kg  ),0)+isnull(convert(decimal(18,2),final.In_FAT_Kg  ),0)-isnull(convert(decimal(18,2),final.Out_Fat_Kg  ),0) as Closing_Out_Fat_Kg" & Environment.NewLine & _
        " ,isnull(convert(decimal(18,2),final.Open_SNF_Kg   ),0)+isnull(convert(decimal(18,2),final.In_SNF_Kg   ),0)-isnull(convert(decimal(18,2),final.Out_SNF_Kg   ),0) as Closing_Out_SNF_Kg" & Environment.NewLine & _
        " from (select Opn .*,Opn.New_Open_Qty *Opn.open_Rate as Opening_Value,Inopn .*,outopn .* from (select opening .OpenLocationCode ,max(opening .OpenLocationDesc )as OpenLocationDesc,max(opening .OpenDate ) as OpenDate,sum(opening .New_Open_Qty ) as New_Open_Qty, case when SUM(opening .Open_Qty)=0 then 0 else  SUM(opening .Avg_Cost )/SUM(opening .Open_Qty) end as open_Rate,convert(decimal(18,2),SUM(opening .FAT_Kg )) as Open_FAT_Kg,convert(decimal(18,2),SUM(opening .SNF_Kg  )) as Open_SNF_Kg from (select TSPL_INVENTORY_MOVEMENT_NEW.Item_Code , TSPL_LOCATION_MASTER.Location_Code as OpenLocationCode ,TSPL_LOCATION_MASTER.Location_Desc as OpenLocationDesc ,convert(date,TSPL_INVENTORY_MOVEMENT_NEW.Posting_Date,103) as OpenDate,case when ISNULL (InOut,'')='I' then Qty*CF else (Qty*CF)*-1 end as New_Open_Qty," & Environment.NewLine & _
        " case when ISNULL (InOut,'')='I' then Qty else Qty*-1 end as Open_Qty," & Environment.NewLine & _
        " case when TSPL_INVENTORY_MOVEMENT_NEW.InOut  ='O' then  (TSPL_INVENTORY_MOVEMENT_NEW.Fat_KG  )*(-1)  else (TSPL_INVENTORY_MOVEMENT_NEW.Fat_KG ) end As FAT_Kg," & Environment.NewLine & _
        " case when TSPL_INVENTORY_MOVEMENT_NEW.InOut  ='O' then  (TSPL_INVENTORY_MOVEMENT_NEW.SNF_KG   )*(-1)  else (TSPL_INVENTORY_MOVEMENT_NEW.SNF_KG ) end As SNF_Kg," & Environment.NewLine & _
        " TSPL_INVENTORY_MOVEMENT_NEW.Avg_Cost  as Avg_Cost from TSPL_INVENTORY_MOVEMENT_NEW " & Environment.NewLine & _
        " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_INVENTORY_MOVEMENT_NEW.Location_Code  " & Environment.NewLine & _
        " left outer join tspl_item_master on tspl_item_master.item_code= TSPL_INVENTORY_MOVEMENT_NEW.Item_Code "

        ''richa agarwal 24 May,2019  TEC/28/03/19-000462 add item structure on setting based
        If ItemStructureMandatoryOnWeightConversion = True Then
            sQuery += " left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF,Structure_code  from TSPL_WEIGHT_CONVERSION UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF,Structure_code  from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF,Structure_code  from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =uom   and lower(zzz.TOUOM)='" + cboUnit.Text + "' where 2 = 2 AND TSPL_ITEM_MASTER.Structure_Code =zzz.Structure_code and convert(date,TSPL_INVENTORY_MOVEMENT_NEW.Posting_Date,103) < convert(date,'" + txtFromDate.Value + "' ,103) "
        Else
            sQuery += " left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF from TSPL_WEIGHT_CONVERSION UNION All Select Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/ nullif (Contained_Qty,0) as CF from TSPL_WEIGHT_CONVERSION UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =uom and lower(zzz.TOUOM)='" + cboUnit.Text + "' where 2 = 2 and convert(date,TSPL_INVENTORY_MOVEMENT_NEW.Posting_Date,103) < convert(date,'" + txtFromDate.Value + "' ,103) "
        End If

        sQuery += FinalWhrCls
        sQuery += " ) as opening" & Environment.NewLine & _
        " group by opening  .OpenLocationCode )Opn " & Environment.NewLine & _
        " full Join" & Environment.NewLine & _
        " (select Inopn .*,Inopn.New_In_Qty *Inopn .In_Rate as In_Value from (select Inopening .InLocationCode ,max(Inopening .InLocationDesc )as InLocationDesc,max(Inopening .InPostingDate ) as InPostingDate,sum(Inopening .New_In_Qty ) as New_In_Qty,  case when SUM(Inopening .In_Qty)=0 then 0 else (  SUM(Inopening .Avg_Cost )/SUM(Inopening .In_Qty)) end as In_Rate,convert(decimal(18,2),SUM(Inopening.Fat_KG )) as In_FAT_Kg,convert(decimal(18,2),SUM(Inopening.SNF_KG  )) as In_SNF_Kg from (select TSPL_INVENTORY_MOVEMENT_NEW.Item_Code ,TSPL_LOCATION_MASTER.Location_Code as InLocationCode ,TSPL_LOCATION_MASTER.Location_Desc  as InLocationDesc,convert(date,TSPL_INVENTORY_MOVEMENT_NEW.Posting_Date,103) as InPostingDate,TSPL_INVENTORY_MOVEMENT_NEW.Qty *cf As New_In_Qty," & Environment.NewLine & _
        " TSPL_INVENTORY_MOVEMENT_NEW.Qty  As In_Qty" & Environment.NewLine & _
        " ,TSPL_INVENTORY_MOVEMENT_NEW.Avg_Cost  as Avg_Cost,TSPL_INVENTORY_MOVEMENT_NEW.Fat_KG,TSPL_INVENTORY_MOVEMENT_NEW.SNF_KG from TSPL_INVENTORY_MOVEMENT_NEW " & Environment.NewLine & _
        " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_INVENTORY_MOVEMENT_NEW.Location_Code  " & Environment.NewLine & _
        " left outer join tspl_item_master on tspl_item_master.item_code= TSPL_INVENTORY_MOVEMENT_NEW.Item_Code "

        ''richa agarwal 24 May,2019  TEC/28/03/19-000462 add item structure on setting based
        If ItemStructureMandatoryOnWeightConversion = True Then
            sQuery += " left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF,Structure_code  from TSPL_WEIGHT_CONVERSION UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF,Structure_code  from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF,Structure_code  from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =uom   and lower(zzz.TOUOM)='" + cboUnit.Text + "' where 2 = 2 AND TSPL_ITEM_MASTER.Structure_Code =zzz.Structure_code and TSPL_INVENTORY_MOVEMENT_NEW.InOut ='I' and "
        Else
            sQuery += " left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF from TSPL_WEIGHT_CONVERSION UNION All Select Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/ nullif (Contained_Qty,0) as CF from TSPL_WEIGHT_CONVERSION UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =uom and lower(zzz.TOUOM)='" + cboUnit.Text + "' where 2 = 2 and TSPL_INVENTORY_MOVEMENT_NEW.InOut ='I' and "
        End If

        sQuery += " convert(date,TSPL_INVENTORY_MOVEMENT_NEW.Posting_Date,103) BETWEEN convert(date,'" + txtFromDate.Value + "' ,103) AND convert(date,'" + txtToDate.Value + "' ,103) " & FinalWhrCls & " " & Environment.NewLine & _
        " ) as Inopening " & Environment.NewLine & _
        " group by Inopening  .InLocationCode )Inopn) Inopn on Inopn.InLocationCode=Opn.OpenLocationCode " & Environment.NewLine & _
        " full Join " & Environment.NewLine & _
        " (select outopn .*,outopn .New_Out_Qty *outopn .out_open_Rate  as OutValue from (select Outopening .OutLocationCode ,max(Outopening .OutLocationDesc )as OutLocationDesc,max(Outopening .OutPostingDate  ) as OutPostingDate,sum(Outopening .New_Out_Qty  ) as New_Out_Qty,convert(decimal(18,2),SUM(Outopening.Fat_KG )) as Out_Fat_Kg ,convert(decimal(18,2),SUM(Outopening.SNF_KG  )) as Out_SNF_Kg, case when SUM(Outopening .Out_Qty)=0 then 0 else(SUM(Outopening .Avg_Cost )/SUM(Outopening .Out_Qty)) end as out_open_Rate from (select TSPL_INVENTORY_MOVEMENT_NEW.Item_Code ,TSPL_LOCATION_MASTER.Location_Code as OutLocationCode ,TSPL_LOCATION_MASTER.Location_Desc  as OutLocationDesc,convert(date,TSPL_INVENTORY_MOVEMENT_NEW.Posting_Date,103) as OutPostingDate,TSPL_INVENTORY_MOVEMENT_NEW.Qty *cf As New_Out_Qty," & Environment.NewLine & _
        " TSPL_INVENTORY_MOVEMENT_NEW.Qty  As Out_Qty " & Environment.NewLine & _
        " ,TSPL_INVENTORY_MOVEMENT_NEW.Avg_Cost  as Avg_Cost,TSPL_INVENTORY_MOVEMENT_NEW.Fat_KG ,TSPL_INVENTORY_MOVEMENT_NEW.SNF_KG from TSPL_INVENTORY_MOVEMENT_NEW " & Environment.NewLine & _
        " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_INVENTORY_MOVEMENT_NEW.Location_Code  " & Environment.NewLine & _
        " left outer join tspl_item_master on tspl_item_master.item_code= TSPL_INVENTORY_MOVEMENT_NEW.Item_Code "

        ''richa agarwal 24 May,2019  TEC/28/03/19-000462 add item structure on setting based
        If ItemStructureMandatoryOnWeightConversion = True Then
            sQuery += " left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF,Structure_code  from TSPL_WEIGHT_CONVERSION UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF,Structure_code  from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF,Structure_code  from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =uom   and lower(zzz.TOUOM)='" + cboUnit.Text + "' where 2 = 2 AND TSPL_ITEM_MASTER.Structure_Code =zzz.Structure_code and TSPL_INVENTORY_MOVEMENT_NEW.InOut ='O' and  convert(date,TSPL_INVENTORY_MOVEMENT_NEW.Posting_Date,103) BETWEEN convert(date,'" + txtFromDate.Value + "' ,103) AND convert(date,'" + txtToDate.Value + "' ,103) "
        Else
            sQuery += " left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF from TSPL_WEIGHT_CONVERSION UNION All Select Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/ nullif (Contained_Qty,0) as CF from TSPL_WEIGHT_CONVERSION UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =uom and lower(zzz.TOUOM)='" + cboUnit.Text + "' where 2 = 2 and TSPL_INVENTORY_MOVEMENT_NEW.InOut ='O' and  convert(date,TSPL_INVENTORY_MOVEMENT_NEW.Posting_Date,103) BETWEEN convert(date,'" + txtFromDate.Value + "' ,103) AND convert(date,'" + txtToDate.Value + "' ,103)   "
        End If

        sQuery += " " & FinalWhrCls & " ) as Outopening " & Environment.NewLine & _
        " group by Outopening  .OutLocationCode  " & Environment.NewLine & _
        " )outopn)outopn on outopn.OutLocationCode =Inopn.InLocationCode)final) finaly"

        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(sQuery)
        If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dtgv
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            FormatGrid()

            RadPageView1.SelectedPage = RadPageViewPage2
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If
        ReStoreGridLayout()
    End Sub
    Sub FormatGrid()
        gv.TableElement.TableHeaderHeight = 25
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next

        gv.Columns("Loc_code").IsVisible = True
        gv.Columns("Loc_code").Width = 100
        gv.Columns("Loc_code").HeaderText = " Location Code"

        gv.Columns("Loc_desc").IsVisible = True
        gv.Columns("Loc_desc").Width = 100
        gv.Columns("Loc_desc").HeaderText = " Location Description"

        gv.Columns("OpenDate").IsVisible = False
        gv.Columns("OpenDate").Width = 100
        gv.Columns("OpenDate").HeaderText = " Date"
        gv.Columns("OpenDate").FormatString = "{0:d}"

        gv.Columns("New_Open_Qty").IsVisible = True
        gv.Columns("New_Open_Qty").Width = 100
        gv.Columns("New_Open_Qty").HeaderText = " Opening Qty"

        gv.Columns("open_Rate").IsVisible = True
        gv.Columns("open_Rate").Width = 100
        gv.Columns("open_Rate").HeaderText = "Rate"

        gv.Columns("Open_FAT_Kg").IsVisible = True
        gv.Columns("Open_FAT_Kg").Width = 100
        gv.Columns("Open_FAT_Kg").HeaderText = "FAT Kg"

        gv.Columns("Open_SNF_Kg").IsVisible = True
        gv.Columns("Open_SNF_Kg").Width = 100
        gv.Columns("Open_SNF_Kg").HeaderText = "SNF Kg"

        gv.Columns("Opening_value").IsVisible = True
        gv.Columns("Opening_value").Width = 80
        gv.Columns("Opening_value").HeaderText = "Opening value"

        gv.Columns("New_In_Qty").IsVisible = True
        gv.Columns("New_In_Qty").Width = 80
        gv.Columns("New_In_Qty").HeaderText = "Qty(In)"

        gv.Columns("In_Rate").IsVisible = True
        gv.Columns("In_Rate").Width = 50
        gv.Columns("In_Rate").HeaderText = "Rate"

        gv.Columns("In_FAT_Kg").IsVisible = True
        gv.Columns("In_FAT_Kg").Width = 100
        gv.Columns("In_FAT_Kg").HeaderText = "FAT Kg"

        gv.Columns("In_SNF_Kg").IsVisible = True
        gv.Columns("In_SNF_Kg").Width = 100
        gv.Columns("In_SNF_Kg").HeaderText = "SNF Kg"

        gv.Columns("In_Value").IsVisible = True
        gv.Columns("In_Value").Width = 100
        gv.Columns("In_Value").HeaderText = "Value"

        gv.Columns("New_Out_Qty").IsVisible = True
        gv.Columns("New_Out_Qty").Width = 100
        gv.Columns("New_Out_Qty").HeaderText = "Qty(Out)"

        gv.Columns("out_open_Rate").IsVisible = True
        gv.Columns("out_open_Rate").Width = 100
        gv.Columns("out_open_Rate").HeaderText = "Rate"

        gv.Columns("Out_Fat_Kg").IsVisible = True
        gv.Columns("Out_Fat_Kg").Width = 100
        gv.Columns("Out_Fat_Kg").HeaderText = "FAT KG"

        gv.Columns("Out_SNF_Kg").IsVisible = True
        gv.Columns("Out_SNF_Kg").Width = 100
        gv.Columns("Out_SNF_Kg").HeaderText = "SNF Kg"

        gv.Columns("OutValue").IsVisible = True
        gv.Columns("OutValue").Width = 100
        gv.Columns("OutValue").HeaderText = "Value"

        gv.Columns("Closing_Qty").IsVisible = True
        gv.Columns("Closing_Qty").Width = 100
        gv.Columns("Closing_Qty").HeaderText = "Closing Qty"

        gv.Columns("Closing_Out_Fat_Kg").IsVisible = True
        gv.Columns("Closing_Out_Fat_Kg").Width = 100
        gv.Columns("Closing_Out_Fat_Kg").HeaderText = "FAT Kg"

        gv.Columns("Closing_Out_SNF_Kg").IsVisible = True
        gv.Columns("Closing_Out_SNF_Kg").Width = 100
        gv.Columns("Closing_Out_SNF_Kg").HeaderText = "SNF Kg"

        gv.Columns("Closing_Rate").IsVisible = True
        gv.Columns("Closing_Rate").Width = 100
        gv.Columns("Closing_Rate").HeaderText = "Rate"

        gv.Columns("Closing_Value").IsVisible = True
        gv.Columns("Closing_Value").Width = 100
        gv.Columns("Closing_Value").HeaderText = "Closing Value"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True
        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub
    Private Sub RptMilkStockLegderSummary_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Load_Report()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()
        End If
    End Sub

    Private Sub chkMCCAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkMCCAll.ToggleStateChanged
        cbgMCC.Enabled = Not chkMCCAll.IsChecked
        If chkMCCAll.IsChecked Then
            cbgMCC.CheckedAll()
        Else
            cbgMCC.UnCheckedAll()
        End If
    End Sub

    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID + gv.Name
        TemplateGridview = gv
        Load_Report()
    End Sub

    Private Sub BtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub btnExporttoExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        print(EnumExportTo.Excel)
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID & gv.Name, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv.Columns.Count - 1
                        gv.Columns(ii).IsVisible = False
                        gv.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub rmSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmSaveLayout.Click
        Dim success As Boolean = True
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then

            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID & gv.Name
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                success = True
            End If

            gv2.MasterTemplate.FilterDescriptors.Clear()
            obj = New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv2.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv2.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            success = success And obj.SaveData()
            If success Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID & gv.Name, objCommonVar.CurrentUserCode)
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub

    Private Sub gv_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellDoubleClick

        Try
            Dim query As String
            Dim tdate As Date = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")
            Dim fdate As Date = clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy")

            gv2.DataSource = Nothing
            Dim summaryRowItem As New GridViewSummaryRowItem()

            Dim LocCode As String = clsCommon.myCstr(gv.CurrentRow.Cells("Loc_code").Value)
            query = "select tspl_location_master.Location_Code,tspl_location_master.Location_Desc,TSPL_ITEM_MASTER.Item_Code ,TSPL_ITEM_MASTER.Item_Desc,TSPL_INVENTORY_MOVEMENT_NEW.Qty,TSPL_INVENTORY_MOVEMENT_NEW.Fat_KG ,TSPL_INVENTORY_MOVEMENT_NEW.SNF_KG , convert(decimal(18,2),TSPL_INVENTORY_MOVEMENT_NEW.Avg_Cost/TSPL_INVENTORY_MOVEMENT_NEW.Qty*cf) as Rate,convert(decimal(18,2),TSPL_INVENTORY_MOVEMENT_NEW.Avg_Cost/(TSPL_INVENTORY_MOVEMENT_NEW.Qty*CF)*(TSPL_INVENTORY_MOVEMENT_NEW.Qty*CF )) as Amount, TSPL_INVENTORY_MOVEMENT_NEW.InOut ,TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type ,TSPL_INVENTORY_MOVEMENT_NEW.Source_Doc_No ,TSPL_INVENTORY_MOVEMENT_NEW.Source_Doc_Date ,TSPL_INVENTORY_MOVEMENT_NEW.UOM ,TSPL_INVENTORY_MOVEMENT_NEW.Basic_Cost ,TSPL_INVENTORY_MOVEMENT_NEW.Avg_Cost,TSPL_INVENTORY_MOVEMENT_NEW.UOM   from TSPL_INVENTORY_MOVEMENT_NEW "
            query += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code =TSPL_INVENTORY_MOVEMENT_NEW.Item_Code "
            query += " left outer join tspl_location_master on tspl_location_master.Location_Code =TSPL_INVENTORY_MOVEMENT_NEW.Location_Code"
            query += "  left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF from TSPL_WEIGHT_CONVERSION UNION All Select Contained_UOM as FromUOM, Container_UOM as TOUOM, case when Contained_Qty=0 then 0 else Container_Qty/Contained_Qty end as CF from TSPL_WEIGHT_CONVERSION UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION  ) yyy)zzz on zzz.FromUOM =uom  and (zzz.TOUOM)='Kg'"
            query += " where 2=2 and  TSPL_INVENTORY_MOVEMENT_NEW.Location_Code ='" + LocCode + "' and convert(date,TSPL_INVENTORY_MOVEMENT_NEW.Posting_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_INVENTORY_MOVEMENT_NEW.Posting_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103)"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(query)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                gv2.Visible = True
                gv2.DataSource = dt
                FormatGridDetails()
                gv2.ReadOnly = True
                RadPageView1.Visible = True
                ReStoreGridLayoutDetails()
                RadPageView1.SelectedPage = RadPageViewPage3
                PageSetupReport_ID = MyBase.Form_ID
                TemplateGridview = gv2
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub ReStoreGridLayoutDetails()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then

                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv2.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv2.Columns.Count - 1
                        gv2.Columns(ii).IsVisible = False
                        gv2.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv2.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Sub FormatGridDetails()
        gv2.TableElement.TableHeaderHeight = 25
        gv2.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv2.Columns.Count - 1
            gv2.Columns(ii).ReadOnly = True
            gv2.Columns(ii).IsVisible = False
        Next

        gv2.Columns("InOut").IsVisible = True
        gv2.Columns("InOut").Width = 100
        gv2.Columns("InOut").HeaderText = " In/Out"

        gv2.Columns("Trans_Type").IsVisible = True
        gv2.Columns("Trans_Type").Width = 100
        gv2.Columns("Trans_Type").HeaderText = "Trans Type "

        gv2.Columns("Location_Desc").IsVisible = False
        gv2.Columns("Location_Desc").Width = 100
        gv2.Columns("Location_Desc").HeaderText = " MCC "

        gv2.Columns("Source_Doc_No").IsVisible = True
        gv2.Columns("Source_Doc_No").Width = 100
        gv2.Columns("Source_Doc_No").HeaderText = " Doc No"

        gv2.Columns("Item_Code").IsVisible = True
        gv2.Columns("Item_Code").Width = 100
        gv2.Columns("Item_Code").HeaderText = "Item Code"

        gv2.Columns("Item_Desc").IsVisible = True
        gv2.Columns("Item_Desc").Width = 80
        gv2.Columns("Item_Desc").HeaderText = "Item Desc"

        gv2.Columns("Qty").IsVisible = True
        gv2.Columns("Qty").Width = 80
        gv2.Columns("Qty").HeaderText = "Qty(In)"

        gv2.Columns("Fat_KG").IsVisible = True
        gv2.Columns("Fat_KG").Width = 80
        gv2.Columns("Fat_KG").HeaderText = "FAT Kg"

        gv2.Columns("SNF_KG").IsVisible = True
        gv2.Columns("SNF_KG").Width = 80
        gv2.Columns("SNF_KG").HeaderText = "SNF Kg"

        gv2.Columns("Rate").IsVisible = True
        gv2.Columns("Rate").Width = 80
        gv2.Columns("Rate").HeaderText = "Rate"

        gv2.Columns("Amount").IsVisible = True
        gv2.Columns("Amount").Width = 80
        gv2.Columns("Amount").HeaderText = "Amount"

        gv2.Columns("UOM").IsVisible = True
        gv2.Columns("UOM").Width = 50
        gv2.Columns("UOM").HeaderText = "UOM"

        gv2.Columns("Basic_Cost").IsVisible = True
        gv2.Columns("Basic_Cost").Width = 100
        gv2.Columns("Basic_Cost").HeaderText = "Basic Cost"

        gv2.Columns("Avg_Cost").IsVisible = True
        gv2.Columns("Avg_Cost").Width = 100
        gv2.Columns("Avg_Cost").HeaderText = "Cost"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        gv2.GroupDescriptors.Add(New GridGroupByExpression("Location_Desc as Item format ""{0}: {1}"" Group By Location_Desc"))
        gv2.ShowGroupPanel = False
        gv2.MasterTemplate.AutoExpandGroups = True
        gv2.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub

    Private Sub gv2_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv2.CellDoubleClick
        If clsCommon.CompairString(gv2.CurrentRow.Cells("Trans_Type").Value, "SRN") = CompairStringResult.Equal Then
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkSRN, gv2.CurrentRow.Cells("Source_Doc_No").Value)
        ElseIf clsCommon.CompairString(gv2.CurrentRow.Cells("Trans_Type").Value, "DispChallan") = CompairStringResult.Equal Then
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCDispatch, gv2.CurrentRow.Cells("Source_Doc_No").Value)
        ElseIf clsCommon.CompairString(gv2.CurrentRow.Cells("Trans_Type").Value, "MilkTransferIn") = CompairStringResult.Equal Then
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkTransferIn, gv2.CurrentRow.Cells("Source_Doc_No").Value)
        End If
    End Sub

    Private Sub rmSummaryExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmSummaryExport.Click
        printSummary(EnumExportTo.Excel)
    End Sub
    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        printDetails(EnumExportTo.Excel)
    End Sub
    Private Sub rmPDFSummary_Click(sender As Object, e As EventArgs) Handles rmPDFSummary.Click
        printSummary(EnumExportTo.PDF)
    End Sub
    Private Sub rmPDFDetail_Click(sender As Object, e As EventArgs) Handles rmPDFDetail.Click
        printDetails(EnumExportTo.PDF)
    End Sub
    Private Sub RadPageView1_SelectedPageChanged(sender As Object, e As EventArgs) Handles RadPageView1.SelectedPageChanged
        If clsCommon.CompairString(RadPageView1.SelectedPage.Name, RadPageViewPage2.Name) = CompairStringResult.Equal Then
            PageSetupReport_ID = MyBase.Form_ID + gv.Name
            TemplateGridview = gv
        ElseIf clsCommon.CompairString(RadPageView1.SelectedPage.Name, RadPageViewPage3.Name) = CompairStringResult.Equal Then
            PageSetupReport_ID = MyBase.Form_ID
            TemplateGridview = gv2
        End If
    End Sub
End Class
