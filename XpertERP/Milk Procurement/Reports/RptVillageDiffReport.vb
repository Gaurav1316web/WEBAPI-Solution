Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
'Ticket no-ERO/18/11/19-001106,vsp code,vsp group code
Public Class RptVillageDiffReport
    Inherits FrmMainTranScreen
     ''check In sanjay 19/06/2020
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim tmpValLoad As Boolean = True
    Dim arrLoc As String = Nothing
    Dim ItemStructureMandatoryOnWeightConversion As Boolean = False
    Dim VillageDiffrenceReportValueWithTwoDecimalPoint As Boolean = False
    '====shivani Tyagi
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
        ''MyBase.SetUserMgmt(clsUserMgtCode.RptVillageDiffReport)
        If Not (MyBase.isReadFlag) Then
            If MDI.blnShowAllMenu = False Then
                Throw New Exception("Permission Denied")
            Else
                Throw New Exception("Can't Access in demo version. " + Environment.NewLine + " For any queries/details, contact tecxpert@tecxpert.in. ")
            End If
        End If
        RadSplitButton1.Visible = MyBase.isExport
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv.Columns.Count - 1 Step ii + 1
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

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub RptVillageDiffReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ItemStructureMandatoryOnWeightConversion = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ItemStructureMandatoryOnWeightConversion, clsFixedParameterCode.ItemStructureMandatoryOnWeightConversion, Nothing)) = 1, True, False))
        VillageDiffrenceReportValueWithTwoDecimalPoint = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.VillageDiffrenceReportValueWithTwoDecimalPoint, clsFixedParameterCode.VillageDiffrenceReportValueWithTwoDecimalPoint, Nothing)) = 1, True, False))

        LOCATIONRIGTHS()
        SetUserMgmtNew()
        cboUnit.Text = "Kg"
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P For Print")
        ButtonToolTip.SetToolTip(BtnReset, "Pres%s Alt+N Adding New")
        RadPageView1.SelectedPage = RadPageViewPage1
        txtToDate.Value = clsCommon.GETSERVERDATE(Nothing, "dd/MM/yyyy")
        txtFromDate.Value = clsCommon.GETSERVERDATE(Nothing, "dd/MM/yyyy")
        LoadShiftFrom()
        LoadShiftTo()
        Reset()
    End Sub


    Sub LoadShiftFrom()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Shift")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "M"
        dr("Shift") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "E"
        dr("Shift") = "Evening"
        dt.Rows.Add(dr)

        txtFromShift.DataSource = dt
        txtFromShift.ValueMember = "Code"
    End Sub
    Sub LoadShiftTo()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Shift")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "M"
        dr("Shift") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "E"
        dr("Shift") = "Evening"
        dt.Rows.Add(dr)

        txtToShift.DataSource = dt
        txtToShift.ValueMember = "Code"
    End Sub
    Public Sub Load_Report()
        Try
            Dim sQuery As String

            If txtFromDate.Value > txtToDate.Value Then
                txtFromDate.Focus()
                Throw New Exception("From date can not be greater then to Date")
            End If

            Dim PeriodDate As String = "'Period :  From  ' + '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") & "' + ' " & txtFromShift.SelectedValue & "'+' To ' + '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") & "' + ' " & txtToShift.SelectedValue & "'"
            Dim QryUploader As String = "select  '" & clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.MCCDefaultMilkItem, clsFixedParameterCode.MilkSetting, Nothing)) & "'  as item_code, convert(varchar,File_Date,103) as DOC_DATE ,shift ,case when SHIFT='M' then 'M'  else 'E' end as  Shift_type , " &
                " TSPL_VLC_MASTER_HEAD.Route_Code as Route_No  ,TSPL_VLC_DATA_UPLOADER.MCC_Code  ,isnull(TSPL_VLC_DATA_UPLOADER.qty,0) as VLC_QTY,TSPL_VLC_DATA_UPLOADER.fat as VLC_Fat ,TSPL_VLC_DATA_UPLOADER.snf as VLC_SNF  ," &
                " isnull(TSPL_VLC_DATA_UPLOADER.Amount,0) as VLC_Amount  ,UOM_Code,VLC_Code_VLC_Uploader,TSPL_VLC_MASTER_HEAD.VLC_CODE,VLC_Code_VLC_Uploader as VLC1,0 as MCC_Qty,0 as MCC_Fat,0 as MCC_SNF," &
                " 0 AS MCC_Amount  from TSPL_VLC_DATA_UPLOADER  " &
                " left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader=TSPL_VLC_DATA_UPLOADER.VLC_CODE " &
                " and TSPL_VLC_MASTER_HEAD.MCC=TSPL_VLC_DATA_UPLOADER.MCC_Code left join TSPL_MP_MASTER on TSPL_MP_MASTER.MP_Code_VLC_Uploader =TSPL_VLC_DATA_UPLOADER.MP_CODE " &
                " and TSPL_MP_MASTER. VLC_Code =TSPL_VLC_MASTER_HEAD.VLC_CODE " &
                " where 2 = 2  and  convert(date, File_Date,103) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and  convert(date, File_Date,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'"

            If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal Then
                QryUploader += " and 2=( case when convert(date, File_Date,103) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and convert(date, File_Date,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and SHIFT='M' then 3 else 2 end  )"
            End If
            If clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
                QryUploader += " and 2=( case when convert(date, File_Date,103) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and convert(date, File_Date,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and SHIFT='E' then 3 else 2 end  )"
            End If

            Dim QryManual As String = " select '" & clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.MCCDefaultMilkItem, clsFixedParameterCode.MilkSetting, Nothing)) & "'  as item_code,convert(varchar,VDUM.Document_Date,103) as Document_Date,(case when VDUM.Shift='MORNING' THEN 'M' ELSE 'E' END) AS Shift,(case when VDUM.Shift='MORNING' THEN 'M' ELSE 'E' END) AS Shift_Type,VLCM.Route_Code,VLCM.MCC,VDUD.Qty,VDUD.FatPer as VLC_FAT ,VDUD.SNFPer as VLC_SNF, " _
               & " VDUD.Amount as VLC_Amount,VDUD.Unit_Code,VLCM.VLC_Code_VLC_Uploader,VDUM.VLC_Code,VLCM.VLC_Code_VLC_Uploader as vlc1, " _
               & " 0 as MCC_Qty,0 as MCC_FAT,0 as MCC_SNF,0 as MCC_Amount from TSPL_VLC_DATA_UPLOADER_DETAIL VDUD " _
               & " inner join TSPL_VLC_DATA_UPLOADER_MASTER VDUM on VDUD.Document_Code=VDUM.Document_Code " _
               & " left join TSPL_VLC_MASTER_HEAD VLCM on VDUM.VLC_Code=VLCM.VLC_Code " _
               & " where 2 = 2  and  convert(date, VDUM.Document_Date,103) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and  convert(date, VDUM.Document_Date,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'"

            If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal Then
                QryManual += " and 2=( case when convert(date, VDUM.Document_Date,103) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and convert(date, VDUM.Document_Date,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and SHIFT='MORNING' then 3 else 2 end  )"
            End If
            If clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
                QryManual += " and 2=( case when convert(date, VDUM.Document_Date,103) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and convert(date, VDUM.Document_Date,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and SHIFT='EVENING' then 3 else 2 end  )"
            End If

            Dim QryMCC As String = "select tspl_milk_srn_detail.item_code, convert(varchar,DOC_DATE,103) as DOC_DATE ,SHIFT ,case when SHIFT='M' then 'M'  else 'E' end as  Shift_type , " & _
                " TSPL_VLC_MASTER_HEAD.ROUTE_CODE as Route_No, tspl_milk_srn_Head.MCC_Code  ,0 as  VLC_QTY,0 as VLC_FAT,0 as VLC_SNF  ,0 as VLC_Amount , " & _
                " UOM_Code,VLC_Code_VLC_Uploader,tspl_milk_srn_Head.VLC_CODE,VLC_Code_VLC_Uploader as vlc1,isnull(tspl_milk_srn_detail.Qty,0) as MCC_Qty,tspl_milk_srn_detail.FAT_PER as MCC_FAT,tspl_milk_srn_detail.SNF_PER as MCC_SNF, " & _
                " isnull(tspl_milk_srn_detail.AMOUNT,0) AS MCC_Amount from tspl_milk_srn_detail " & _
                " left join tspl_milk_srn_Head on tspl_milk_srn_Head.DOC_CODE =tspl_milk_srn_detail.DOC_CODE " & _
                " left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code =tspl_milk_srn_Head.VLC_CODE " & _
                " where 2 = 2  and  convert(date, DOC_DATE,103) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and  convert(date, DOC_DATE,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'"

            If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal Then
                QryMCC += " and 2=( case when convert(date, DOC_DATE,103) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and convert(date, DOC_DATE,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and SHIFT='M' then 3 else 2 end  )"
            End If
            If clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
                QryMCC += " and 2=( case when convert(date, DOC_DATE,103) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and convert(date, DOC_DATE,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and SHIFT='E' then 3 else 2 end  )"
            End If


            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                QryUploader += "and TSPL_VLC_DATA_UPLOADER.MCC_Code  IN (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") "
                QryManual += "and VLCM.MCC  IN (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") "
                QryMCC += "and tspl_milk_srn_Head.MCC_Code  IN (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") "
            End If


            sQuery = " (select  case when Shift_type ='M' then 1 else 2 end Sno,case when VLC_QTY=0 then 0 else convert(decimal(18,2),VLC_Amount/VLC_QTY) end as VLC_Avg_Rate ,case when MCC_Qty=0 then 0 else MCC_Amount/MCC_Qty end as MCC_Avg_Rate," & PeriodDate & " as PeriodDate,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") & "'  as fromDate ,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") & "'  as Todate , companyADD, '" & objCommonVar.CurrentCompanyName & "'  as CompName,'" & objCommonVar.CurrentCompanyCode & "'  as CompCode," & Environment.NewLine &
          "  DOC_DATE  ,Shift_type  , MCC_Code ,  MCC_NAME  as MCC_NAME  , Route_No , Route_name as Route_name,VLC_Code_VLC_Uploader ,VLC_Code ,vlc1 ,VLC_Name ,VLC_QTY as  VLC_QTY,convert(decimal(18,4),(VLC_TFAT *100)/iif(VLC_QTY=0,1,VLC_QTY)) as VLC_FATPer,  convert(decimal(18,4),(VLC_TSNF  *100)/iif(VLC_QTY=0,1,VLC_QTY)) as VLC_SNFPer,MCC_FATPer,MCC_SNFPer, VLC_TFAT as VLC_TFAT, VLC_TSNF as VLC_TSNF,VLC_Amount as VLC_Amount ,MCC_Qty as MCC_Qty, MCC_TFAT as MCC_TFAT, MCC_TSNF as MCC_TSNF , MCC_Amount as MCC_Amount,FromUOM,TOUOM,(MCC_Qty-VLC_QTY) as Diff_Qty,(MCC_FATPER-convert(decimal(18,2),(VLC_TFAT *100)/iif(VLC_QTY=0,1,VLC_QTY))) as Diff_FATPer, (MCC_SNFPer-convert(decimal(18,2),(VLC_TSNF  *100)/iif(VLC_QTY=0,1,VLC_QTY))) as Diff_SNFPer, (MCC_TFAT-VLC_TFAT) as Diff_TFAT, (MCC_TSNF-VLC_TSNF) as Diff_TSNF,(MCC_Amount-VLC_Amount) as Diff_Amt from " _
          & " (select convert(varchar,DOC_DATE,103) as DOC_DATE,Shift_type,ff.MCC_Code,max(MCC_NAME) as MCC_NAME ,ff.Route_No,max(Route_Name) as Route_Name ,sum(VLC_QTY) as VLC_QTY,sum(VLC_TFAT) as VLC_TFAT,sum(VLC_TSNF) as VLC_TSNF,sum(VLC_Amount) as VLC_Amount,ff.VLC_Code_VLC_Uploader,ff.VLC_CODE,ff.VLC_Code_VLC_Uploader as vlc1,max(VLC_Name) as VLC_Name ,sum( MCC_Qty) as MCC_Qty,sum( MCC_TFAT) as MCC_TFAT,sum(MCC_TSNF) as MCC_TSNF ,sum(MCC_Amount) as MCC_Amount, max(UOM_Code) as  UOM_Code,max(FromUOM) as FromUOM,max(TOUOM) as TOUOM, max(TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_COMPANY_MASTER.City_Code)>0 then ', '+TSPL_COMPANY_MASTER.City_Code else ' ' end + ','+case when len(TSPL_STATE_MASTER.STATE_NAME  )>0 then TSPL_STATE_MASTER.STATE_NAME else '' end)  as companyADD ,case when sum(VLC_QTY) =0 then 0 else ((sum(VLC_TFAT) *100 ) /sum(VLC_QTY)) end as VLC_FATPer,case when sum(VLC_QTY) =0 then 0 else ((sum(VLC_TSNF) *100 ) /sum(VLC_QTY)) end as VLC_SNFPer, case when sum( MCC_Qty) =0 then 0 else (sum( MCC_TFAT)*100)/sum(MCC_Qty) end  as MCC_FATPer," _
            & " case when sum( MCC_Qty) =0 then 0 else (sum( MCC_TSNF)*100)/sum(MCC_Qty) end  as MCC_SNFPer  from" _
          & " (select ttt.* from (select  DOC_DATE ,shift , Shift_type ,Route_No  ,MCC_Code  , (cf * VLC_QTY) as  VLC_QTY, (cf * VLC_TFAT) as VLC_TFAT , (cf *VLC_TSNF) as VLC_TSNF    ,(cf * VLC_Amount) as  VLC_Amount,UOM_Code,VLC_Code_VLC_Uploader,VLC_CODE,VLC_Code_VLC_Uploader as vlc1, (cf * MCC_Qty) as MCC_Qty, (cf * MCC_TFAT) as MCC_TFAT, (cf * MCC_TSNF) as MCC_TSNF , (cf * MCC_Amount) as MCC_Amount,FromUOM ,TOUOM,case when (cf * VLC_QTY) =0 then 0 else ((VLC_TFAT *100 ) /(cf * VLC_QTY)) end as VLC_FATPer,case when (cf * VLC_QTY) =0 then 0 else ((VLC_TSNF *100 ) /(cf * VLC_QTY)) end as VLC_SNFPer,case when (cf * MCC_Qty) =0 then 0 else ((MCC_TFAT *100  ) /(cf * MCC_Qty)) end  as MCC_FATPer,case when (cf * MCC_Qty) =0 then 0 else ((MCC_TSNF *100  ) /(cf * MCC_Qty)) end as MCC_SNFPer  from " _
          & " ( select *,(VLC_QTY  *VLC_Fat ) /100 as VLC_TFAT,(VLC_QTY *VLC_SNF ) /100 as VLC_TSNF, (MCC_Qty   *MCC_Fat  ) /100 as MCC_TFAT,(MCC_Qty *MCC_SNF  ) /100 as MCC_TSNF" _
          & " from " _
          & " ( " & QryUploader & " " _
          & " union all " _
          & " " & QryManual & "" _
          & " union all " _
          & " " & QryMCC & " )pp ) xx" & Environment.NewLine &
          " LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code =xx.Item_Code "

            ''richa agarwal 28 May,2019  TEC/28/03/19-000462 add item structure on setting based
            If ItemStructureMandatoryOnWeightConversion = True Then
                sQuery += " left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF,Structure_code  from TSPL_WEIGHT_CONVERSION where Contained_UOM='KG' UNION All Select Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/ nullif (Contained_Qty,0) as CF,Structure_code from TSPL_WEIGHT_CONVERSION where Container_UOM='LTR' UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF,Structure_code  from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF,Structure_code  from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =xx.UOM_Code   and lower(zzz.TOUOM)='" + cboUnit.Text + "' where 2 = 2 AND TSPL_ITEM_MASTER.Structure_Code =zzz.Structure_code ) "
            Else
                sQuery += " left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF from TSPL_WEIGHT_CONVERSION where Contained_UOM='KG' UNION All Select Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/  nullif (Contained_Qty,0) as CF from TSPL_WEIGHT_CONVERSION where Container_UOM='LTR' UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =xx.UOM_Code and lower(zzz.TOUOM)='" + cboUnit.Text + "' ) "
            End If

            sQuery += " ttt  )ff left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_name='KWALITY LIMITED' left join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE =TSPL_COMPANY_MASTER.State " _
               & " left join  TSPL_mcc_ROUTE_MASTER  on TSPL_mcc_ROUTE_MASTER.Route_code=ff.Route_No left outer join TSPL_MCC_MASTER  on TSPL_MCC_MASTER .MCC_Code =ff .MCC_CODE    left join TSPL_VLC_MASTER_HEAD  on TSPL_VLC_MASTER_HEAD.VLC_Code=ff.VLC_CODE  " _
               & " where 2 = 2  and  convert(date, DOC_DATE,103) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and  convert(date, DOC_DATE,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'"
            If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal Then
                sQuery += " and 2=( case when convert(date, DOC_DATE,103) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and convert(date, DOC_DATE,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and SHIFT='M' then 3 else 2 end  )"
            End If
            If clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
                sQuery += " and 2=( case when convert(date, DOC_DATE,103) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and convert(date, DOC_DATE,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and SHIFT='E' then 3 else 2 end  )"
            End If

            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                sQuery += "and TSPL_MCC_MASTER.MCC_Code  IN (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") "
            End If
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                sQuery += " and ff.Route_No in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")  "
            End If
            If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
                sQuery += " and ff.VLC_Code in (" + clsCommon.GetMulcallString(txtVLC.arrValueMember) + ")  "
            End If


            sQuery += " group by ff.MCC_Code ,DOC_DATE,Shift_type,Route_No,ff.VLC_CODE,ff.VLC_Code_VLC_Uploader ) MM )"

            sQuery = " select *,(case when Ltr_Shortage_Amount < 0 and TSAmount < 0 then Ltr_Shortage_Amount + TSAmount when Ltr_Shortage_Amount < TSAmount then Ltr_Shortage_Amount else TSAmount end) As Deduction from ( " &
                     " select sno,Route_No as R_Code,Route_name as R_Name,PeriodDate,fromDate,Todate,companyADD,CompName,CompCode,MCC_Code,MCC_NAME,Route_No,Route_name,fromuom,ToUOM," &
                     " DOC_DATE,Shift_type,final.VLC_Code,vlc_Name,TBL_VENDOR_GROUP.Vendor_Code,TBL_VENDOR_GROUP.Vendor_Group_Code,TBL_VENDOR_GROUP.Vendor_Group_Code_Desc,vlc1,VLC_Code_VLC_Uploader,VLC_QTY,VLC_FATPer,VLC_SNFPer,VLC_FATPer+VLC_SNFPer as VLCTSPer,VLC_TFAT,VLC_TSNF,VLC_Amount," &
                     " VLC_Avg_Rate ,MCC_Qty,MCC_FATPer,MCC_SNFPer,MCC_FATPer+MCC_SNFPer as MCCTSPer,MCC_TFAT,MCC_TSNF,MCC_Amount, MCC_Avg_Rate,Diff_Qty ,Diff_FATPer ,Diff_SNFPer ,Diff_TFAT,Diff_TSNF,Diff_TFAT+Diff_TSNF as DiffTS,Diff_Amt,Diff_Qty*VLC_Avg_Rate as Ltr_Shortage_Amount,case when (VLC_FATPer+VLC_SNFPer)=0 then 0 else (VLC_Avg_Rate/(VLC_FATPer+VLC_SNFPer))*100*(Diff_TFAT+Diff_TSNF) end  as TSAmount,TabPro.FATSNFLossAmt,TabPro.NoteAmt from " &
                     "" & sQuery & "" &
                     " as final   left Outer Join (select TSPL_VLC_MASTER_HEAD. VLC_Code,TSPL_VENDOR_MASTER.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Group_Code, TSPL_VENDOR_MASTER.Vendor_Group_Code_Desc from TSPL_VENDOR_MASTER left outer Join TSPL_VLC_MASTER_HEAD on TSPL_VENDOR_MASTER.Vendor_Code = TSPL_VLC_MASTER_HEAD.VSP_Code where TSPL_VLC_MASTER_HEAD. VLC_Code is not null ) as TBL_VENDOR_GROUP on TBL_VENDOR_GROUP.VLC_Code = final.VLC_Code  " + Environment.NewLine +
            "left outer join (select TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_CODE as MCCCode,TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE as VSPCode,TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.Doc_Date as DocDate,TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.Shift , FATLossAmt+SNFLossAmt as FATSNFLossAmt,NoteAmt from TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS  left outer join TSPL_MILK_PURCHASE_INVOICE_HEAD on TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE=TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.InvoiceNo
            where TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.Doc_Date >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.Doc_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' 
            ) as TabPro on TabPro.MCCCODE=final.MCC_Code and TabPro.VSPCode=TBL_VENDOR_GROUP.Vendor_Code and TabPro.DocDate = convert(date,final.DOC_DATE,103) and TabPro.Shift= final.Shift_type" + Environment.NewLine +
            ")xx order by mcc_code,route_no,vlc1,convert(date,DOC_DATE,103),sno "

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
                If btnReferesh = False Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(CrystalReportFolder.MilkProcurement, dtgv, "CrptVillageDifference", "Village Different Report")
                    frmCRV = Nothing
                End If
                RadPageView1.SelectedPage = RadPageViewPage2
                EnableDisableControl(False)
            Else
                tmpValLoad = False
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If
            ReStoreGridLayout()
            View()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub FormatGrid()
        Dim strDecimal As String = ""
        If VillageDiffrenceReportValueWithTwoDecimalPoint = True Then
            strDecimal = 2
        Else
            strDecimal = 3
        End If
        gv.TableElement.TableHeaderHeight = 25
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next

        If chkGroupWise.Checked = False Then
            gv.Columns("MCC_Code").IsVisible = True
            gv.Columns("MCC_Code").Width = 100
            gv.Columns("MCC_Code").HeaderText = "MCC Code"

            gv.Columns("MCC_NAME").IsVisible = True
            gv.Columns("MCC_NAME").Width = 100
            gv.Columns("MCC_NAME").HeaderText = "MCC Name"

            gv.Columns("Route_No").IsVisible = True
            gv.Columns("Route_No").Width = 100
            gv.Columns("Route_No").HeaderText = "Route No"

            gv.Columns("Route_name").IsVisible = True
            gv.Columns("Route_name").Width = 100
            gv.Columns("Route_name").HeaderText = "Route Name"
        Else
            gv.Columns("R_Code").IsVisible = True
            gv.Columns("R_Code").Width = 100
            gv.Columns("R_Code").HeaderText = "R Code"

            gv.Columns("R_Name").IsVisible = True
            gv.Columns("R_Name").Width = 100
            gv.Columns("R_Name").HeaderText = "R Name"
        End If
        gv.Columns("vlc1").IsVisible = True
        gv.Columns("vlc1").Width = 100
        gv.Columns("vlc1").HeaderText = "VLC Code"

        gv.Columns("VLC_Code").IsVisible = True
        gv.Columns("VLC_Code").Width = 150
        gv.Columns("VLC_Code").HeaderText = " Code"

        gv.Columns("VLC_Name").IsVisible = True
        gv.Columns("VLC_Name").Width = 150
        gv.Columns("VLC_Name").HeaderText = "VLC Name"

        gv.Columns("Vendor_Code").IsVisible = True
        gv.Columns("Vendor_Code").Width = 100
        gv.Columns("Vendor_Code").HeaderText = "Vendor Code"

        gv.Columns("Vendor_Group_Code").IsVisible = True
        gv.Columns("Vendor_Group_Code").Width = 100
        gv.Columns("Vendor_Group_Code").HeaderText = "Vendor Group Code"

        gv.Columns("Vendor_Group_Code_Desc").IsVisible = True
        gv.Columns("Vendor_Group_Code_Desc").Width = 100
        gv.Columns("Vendor_Group_Code_Desc").HeaderText = "Vendor Group Description"

        gv.Columns("DOC_DATE").IsVisible = True
        gv.Columns("DOC_DATE").Width = 100
        gv.Columns("DOC_DATE").HeaderText = " Date"
        gv.Columns("DOC_DATE").FormatString = "{0:d}"

        gv.Columns("Shift_type").IsVisible = True
        gv.Columns("Shift_type").Width = 100
        gv.Columns("Shift_type").HeaderText = "Shift"

        gv.Columns("VLC_QTY").IsVisible = True
        gv.Columns("VLC_QTY").Width = 80
        gv.Columns("VLC_QTY").HeaderText = "VLC Qty"
        gv.Columns("VLC_QTY").FormatString = "{0:F" + strDecimal + "}"

        gv.Columns("VLC_FATPer").IsVisible = True
        gv.Columns("VLC_FATPer").Width = 80
        gv.Columns("VLC_FATPer").HeaderText = "VLC FAT%"
        gv.Columns("VLC_FATPer").FormatString = "{0:F" + strDecimal + "}"

        gv.Columns("VLC_SNFPer").IsVisible = True
        gv.Columns("VLC_SNFPer").Width = 80
        gv.Columns("VLC_SNFPer").HeaderText = "VLC SNF%"
        gv.Columns("VLC_SNFPer").FormatString = "{0:F" + strDecimal + "}"

        gv.Columns("VLC_TFAT").IsVisible = True
        gv.Columns("VLC_TFAT").Width = 80
        gv.Columns("VLC_TFAT").HeaderText = "VLC TFAT"
        gv.Columns("VLC_TFAT").FormatString = "{0:F" + strDecimal + "}"

        gv.Columns("VLC_TSNF").IsVisible = True
        gv.Columns("VLC_TSNF").Width = 50
        gv.Columns("VLC_TSNF").HeaderText = "VLC TSNF"
        gv.Columns("VLC_TSNF").FormatString = "{0:F" + strDecimal + "}"

        gv.Columns("VLC_Amount").IsVisible = True
        gv.Columns("VLC_Amount").Width = 100
        gv.Columns("VLC_Amount").HeaderText = "VLC Amount"
        gv.Columns("VLC_Amount").FormatString = "{0:F" + strDecimal + "}"

        gv.Columns("MCC_Qty").IsVisible = True
        gv.Columns("MCC_Qty").Width = 100
        gv.Columns("MCC_Qty").HeaderText = "MCC Qty"
        gv.Columns("MCC_Qty").FormatString = "{0:F" + strDecimal + "}"

        gv.Columns("MCC_FATPer").IsVisible = True
        gv.Columns("MCC_FATPer").Width = 100
        gv.Columns("MCC_FATPer").HeaderText = "MCC FAT%"
        gv.Columns("MCC_FATPer").FormatString = "{0:F" + strDecimal + "}"

        gv.Columns("MCC_SNFPer").IsVisible = True
        gv.Columns("MCC_SNFPer").Width = 100
        gv.Columns("MCC_SNFPer").HeaderText = "MCC SNF%"
        gv.Columns("MCC_SNFPer").FormatString = "{0:F" + strDecimal + "}"

        gv.Columns("MCC_TFAT").IsVisible = True
        gv.Columns("MCC_TFAT").Width = 100
        gv.Columns("MCC_TFAT").HeaderText = "MCC TFAT"
        gv.Columns("MCC_TFAT").FormatString = "{0:F" + strDecimal + "}"

        gv.Columns("MCC_TSNF").IsVisible = True
        gv.Columns("MCC_TSNF").Width = 100
        gv.Columns("MCC_TSNF").HeaderText = "MCC TSNF"
        gv.Columns("MCC_TSNF").FormatString = "{0:F" + strDecimal + "}"

        gv.Columns("MCC_Amount").IsVisible = True
        gv.Columns("MCC_Amount").Width = 100
        gv.Columns("MCC_Amount").HeaderText = "MCC Amount"
        gv.Columns("MCC_Amount").FormatString = "{0:F" + strDecimal + "}"

        gv.Columns("Diff_Qty").IsVisible = True
        gv.Columns("Diff_Qty").Width = 100
        gv.Columns("Diff_Qty").HeaderText = "Diff Qty"
        gv.Columns("Diff_Qty").FormatString = "{0:F" + strDecimal + "}"

        gv.Columns("Diff_FATPer").IsVisible = True
        gv.Columns("Diff_FATPer").Width = 100
        gv.Columns("Diff_FATPer").HeaderText = "Diff FAT%"
        gv.Columns("Diff_FATPer").FormatString = "{0:F" + strDecimal + "}"

        gv.Columns("Diff_SNFPer").IsVisible = True
        gv.Columns("Diff_SNFPer").Width = 100
        gv.Columns("Diff_SNFPer").HeaderText = "Diff SNF%"
        gv.Columns("Diff_SNFPer").FormatString = "{0:F" + strDecimal + "}"

        gv.Columns("Diff_TFAT").IsVisible = True
        gv.Columns("Diff_TFAT").Width = 100
        gv.Columns("Diff_TFAT").HeaderText = "Diff TFAT"
        gv.Columns("Diff_TFAT").FormatString = "{0:F" + strDecimal + "}"

        gv.Columns("Diff_TSNF").IsVisible = True
        gv.Columns("Diff_TSNF").Width = 100
        gv.Columns("Diff_TSNF").HeaderText = "Diff TSNF"
        gv.Columns("Diff_TSNF").FormatString = "{0:F" + strDecimal + "}"

        gv.Columns("Diff_Amt").IsVisible = True
        gv.Columns("Diff_Amt").Width = 100
        gv.Columns("Diff_Amt").HeaderText = "Diff Amount"
        gv.Columns("Diff_Amt").FormatString = "{0:F" + strDecimal + "}"

        gv.Columns("Route_No").IsVisible = True
        gv.Columns("Route_No").Width = 100
        gv.Columns("Route_No").HeaderText = "Route Code"

        gv.Columns("MCC_Code").IsVisible = True
        gv.Columns("MCC_Code").Width = 100
        gv.Columns("MCC_Code").HeaderText = "MCC Code"

        gv.Columns("VLC_Code_VLC_Uploader").IsVisible = True
        gv.Columns("VLC_Code_VLC_Uploader").Width = 100
        gv.Columns("VLC_Code_VLC_Uploader").HeaderText = "VLC Code"

        gv.Columns("VLC_Avg_Rate").IsVisible = True
        gv.Columns("VLC_Avg_Rate").Width = 100
        gv.Columns("VLC_Avg_Rate").HeaderText = "VLC Avg Rate"
        gv.Columns("VLC_Avg_Rate").FormatString = "{0:F" + strDecimal + "}"

        gv.Columns("MCC_Avg_Rate").IsVisible = True
        gv.Columns("MCC_Avg_Rate").Width = 100
        gv.Columns("MCC_Avg_Rate").HeaderText = "MCC Avg Rate"
        gv.Columns("MCC_Avg_Rate").FormatString = "{0:F" + strDecimal + "}"

        gv.Columns("Ltr_Shortage_Amount").IsVisible = True
        gv.Columns("Ltr_Shortage_Amount").Width = 100
        gv.Columns("Ltr_Shortage_Amount").HeaderText = "Ltr Shortage Amount"
        gv.Columns("Ltr_Shortage_Amount").FormatString = "{0:F" + strDecimal + "}"


        gv.Columns("VLCTSPer").IsVisible = True
        gv.Columns("VLCTSPer").Width = 100
        gv.Columns("VLCTSPer").HeaderText = "VLC TS%"
        gv.Columns("VLCTSPer").FormatString = "{0:F" + strDecimal + "}"

        gv.Columns("MCCTSPer").IsVisible = True
        gv.Columns("MCCTSPer").Width = 100
        gv.Columns("MCCTSPer").HeaderText = "MCC TS%"
        gv.Columns("MCCTSPer").FormatString = "{0:F" + strDecimal + "}"

        gv.Columns("DiffTS").IsVisible = True
        gv.Columns("DiffTS").Width = 100
        gv.Columns("DiffTS").HeaderText = "Diff TS"
        gv.Columns("DiffTS").FormatString = "{0:F" + strDecimal + "}"

        gv.Columns("TSAmount").IsVisible = True
        gv.Columns("TSAmount").Width = 100
        gv.Columns("TSAmount").HeaderText = "TS Amount"
        gv.Columns("TSAmount").FormatString = "{0:F" + strDecimal + "}"

        gv.Columns("Deduction").IsVisible = True
        gv.Columns("Deduction").Width = 100
        gv.Columns("Deduction").HeaderText = "Deduction Amount"
        gv.Columns("Deduction").FormatString = "{0:F" + strDecimal + "}"


        gv.Columns("FATSNFLossAmt").IsVisible = True
        gv.Columns("FATSNFLossAmt").Width = 100
        gv.Columns("FATSNFLossAmt").HeaderText = "Pro FAT/SNF Loss Amount"
        gv.Columns("FATSNFLossAmt").FormatString = "{0:F" + strDecimal + "}"

        gv.Columns("NoteAmt").IsVisible = True
        gv.Columns("NoteAmt").Width = 100
        gv.Columns("NoteAmt").HeaderText = "Pro Debit/Credit Note Amount"
        gv.Columns("NoteAmt").FormatString = "{0:F" + strDecimal + "}"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As New GridViewSummaryItem("VLC_QTY", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("VLC_TFAT", "{0:F" + strDecimal + "}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("VLC_TSNF", "{0:F" + strDecimal + "}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("VLC_Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("MCC_Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item6 As New GridViewSummaryItem("MCC_TFAT", "{0:F" + strDecimal + "}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)
        Dim item7 As New GridViewSummaryItem("MCC_TSNF", "{0:F" + strDecimal + "}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)
        Dim item8 As New GridViewSummaryItem("MCC_Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)
        Dim item9 As New GridViewSummaryItem("Diff_Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item9)
        Dim item10 As New GridViewSummaryItem("Diff_TFAT", "{0:F" + strDecimal + "}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item10)
        Dim item11 As New GridViewSummaryItem("Diff_TSNF", "{0:F" + strDecimal + "}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item11)
        Dim item12 As New GridViewSummaryItem("Diff_Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item12)

        Dim item121 As New GridViewSummaryItem("FATSNFLossAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item121)
        Dim item122 As New GridViewSummaryItem("NoteAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item122)

        Dim item13 As New GridViewSummaryItem()
        item13.FormatString = "{0:F" + strDecimal + "}"
        item13.Name = "VLC_Avg_Rate"
        item13.AggregateExpression = "sum(VLC_Amount)/sum(VLC_QTY)"
        summaryRowItem.Add(item13)


        Dim item14 As New GridViewSummaryItem("MCC_Avg_Rate", "{0:F" + strDecimal + "}", GridAggregateFunction.Avg)
        summaryRowItem.Add(item14)
        Dim item15 As New GridViewSummaryItem("Ltr_Shortage_Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item15)

        Dim item16 As New GridViewSummaryItem("DiffTS", "{0:F" + strDecimal + "}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item16)
        Dim item17 As New GridViewSummaryItem("TSAmount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item17)

        Dim summaryItem5 As New GridViewSummaryItem()
        summaryItem5.FormatString = "{0:F2}"
        summaryItem5.Name = "Deduction"
        summaryItem5.AggregateExpression = " IIF ( sum (Ltr_Shortage_Amount) < 0 and sum (TSAmount) < 0 , sum (Ltr_Shortage_Amount) + sum (TSAmount) , IIf (sum (Ltr_Shortage_Amount) < sum (TSAmount) ,sum(Ltr_Shortage_Amount) ,sum(TSAmount) ) ) "
        summaryRowItem.Add(summaryItem5)

        Dim summaryItem1 As New GridViewSummaryItem()
        summaryItem1.FormatString = "{0:F" + strDecimal + "}"
        summaryItem1.Name = "VLC_FATPer"
        summaryItem1.AggregateExpression = "IIf (sum(VLC_QTY) =0 , '0.00' , sum(VLC_TFAT)*100 /sum(VLC_QTY))" ' "sum(VLC_TFAT)*100/sum(VLC_QTY)"
        summaryRowItem.Add(summaryItem1)

        Dim summaryItem2 As New GridViewSummaryItem()
        summaryItem2.FormatString = "{0:F" + strDecimal + "}"
        summaryItem2.Name = "VLC_SNFPer"
        summaryItem2.AggregateExpression = "IIf (sum(VLC_QTY) =0 , '0.00' , sum(VLC_TSNF)*100 /sum(VLC_QTY))" ' "sum(VLC_TSNF)*100/sum(VLC_QTY)"
        summaryRowItem.Add(summaryItem2)

        Dim summaryItem3 As New GridViewSummaryItem()
        summaryItem3.FormatString = "{0:F" + strDecimal + "}"
        summaryItem3.Name = "MCC_FATPer"
        'summaryItem3.AggregateExpression = "sum(MCC_TFAT)*100/sum(MCC_Qty)"
        summaryItem3.AggregateExpression = "IIf (sum(MCC_Qty) =0 , '0.00' , sum(MCC_TFAT)*100 /sum(MCC_Qty))" '  "sum(MCC_TFAT)*100/sum(MCC_Qty)"
        summaryRowItem.Add(summaryItem3)

        Dim summaryItem4 As New GridViewSummaryItem()
        summaryItem4.FormatString = "{0:F" + strDecimal + "}"
        summaryItem4.Name = "MCC_SNFPer"
        'summaryItem4.AggregateExpression = "sum(MCC_TSNF)*100/sum(MCC_Qty)"
        summaryItem4.AggregateExpression = "IIf (sum(MCC_Qty) =0 , '0.00' , sum(MCC_TSNF)*100 /sum(MCC_Qty))" '"sum(MCC_TSNF)*100/sum(MCC_Qty)"
        summaryRowItem.Add(summaryItem4)

        Dim item22 As New GridViewSummaryItem("VLCTSPer", "{0:F" + strDecimal + "}", GridAggregateFunction.Avg)
        summaryRowItem.Add(item22)
        Dim item23 As New GridViewSummaryItem("MCCTSPer", "{0:F" + strDecimal + "}", GridAggregateFunction.Avg)
        summaryRowItem.Add(item23)
        Dim item24 As New GridViewSummaryItem("Diff_FATPer", "{0:F" + strDecimal + "}", GridAggregateFunction.Avg)
        summaryRowItem.Add(item24)
        Dim item25 As New GridViewSummaryItem("Diff_SNFPer", "{0:F" + strDecimal + "}", GridAggregateFunction.Avg)
        summaryRowItem.Add(item25)

        If chkGroupWise.Checked = True Then
            gv.GroupDescriptors.Add(New GridGroupByExpression("MCC_Code as Item format ""{0}: {1}"" Group By MCC_Code"))
            gv.GroupDescriptors.Add(New GridGroupByExpression("Route_No as Item format ""{0}: {1}"" Group By Route_No"))
            gv.GroupDescriptors.Add(New GridGroupByExpression("VLC_Code_VLC_Uploader as Item format ""{0}: {1}"" Group By VLC_Code_VLC_Uploader"))
        End If
        'gv.MasterTemplate.SummaryRowsBottom.Clear()
        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True
        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        gv.MasterTemplate.ShowTotals = True
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "SPMMD") = CompairStringResult.Equal Then
            gv.MasterTemplate.ShowParentGroupSummaries = False
        Else
            gv.MasterTemplate.ShowParentGroupSummaries = True
        End If
    End Sub
    Sub View()
        '========update by preeti gupta Against ticket no[ERO/12/07/19-000945,ERO/11/07/19-000681]
        If gv.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()

            view.ColumnGroups.Add(New GridViewColumnGroup(" "))
            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            If chkGroupWise.Checked = False Then
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("MCC_Code").Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("MCC_Name").Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Route_No").Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Route_Name").Name)
            Else
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("R_Code").Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("R_Name").Name)
            End If
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("DOC_DATE").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Shift_type").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("VLC_Code").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("vlc_Name").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Vendor_Code").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Vendor_Group_Code").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Vendor_Group_Code_Desc").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("vlc1").Name)


            view.ColumnGroups.Add(New GridViewColumnGroup("As Per Center"))
            view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("VLC_QTY").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("VLC_FATPer").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("VLC_SNFPer").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("VLCTSPer").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("VLC_TFAT").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("VLC_TSNF").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("VLC_Amount").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("VLC_Avg_Rate").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("FATSNFLossAmt").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("NoteAmt").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("As Per Dairy"))
            view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("MCC_Qty").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("MCC_FATPer").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("MCC_SNFPer").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("MCCTSPer").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("MCC_TFAT").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("MCC_TSNF").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("MCC_Amount").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("MCC_Avg_Rate").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Difference"))
            view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv.Columns("Diff_Qty").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv.Columns("Diff_FATPer").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv.Columns("Diff_SNFPer").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv.Columns("Diff_TFAT").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv.Columns("Diff_TSNF").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv.Columns("DiffTS").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv.Columns("Diff_Amt").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv.Columns("Ltr_Shortage_Amount").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv.Columns("TSAmount").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv.Columns("Deduction").Name)
            gv.ViewDefinition = view
        End If
    End Sub
    Sub Reset()
        gv.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        EnableDisableControl(True)
    End Sub
    Private Sub EnableDisableControl(ByVal val As Boolean)
        RadGroupBox1.Enabled = val
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        btnReferesh = True
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        GetReportID()
        Load_Report()
        tmpValLoad = False
    End Sub

    Sub GetReportID()
        Dim VarID As String = ""
        If chkGroupWise.Checked Then
            VarID += "_GW"
        End If
        gv.VarID = VarID
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        btnReferesh = False
        Load_Report()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                Dim CompName As String = clsDBFuncationality.getSingleValue("Select Comp_Name from TSPL_COMPANY_MASTER Where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'")
                arrHeader.Add(CompName)
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")



                If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                    arrHeader.Add(("MCC : " + clsCommon.GetMulcallStringWithComma(txtMCC.arrValueMember) + " "))
                End If
                If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then

                    arrHeader.Add(("Route : " + clsCommon.GetMulcallStringWithComma(txtRoute.arrValueMember) + " "))
                End If
                If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
                    arrHeader.Add(("VLC : " + clsCommon.GetMulcallStringWithComma(txtVLC.arrValueMember) + " "))
                End If



                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                If exporter = EnumExportTo.Excel Then
                    clsCommon.MyExportToExcelGrid(Me.Text, gv, arrHeader, Me.Text)
                Else
                    clsCommon.MyExportToPDF(Me.Text, gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub rmExcel_Click(sender As Object, e As EventArgs) Handles rmExcel.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        print(EnumExportTo.PDF)
    End Sub

    Private Sub RptVillageDiffReport_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Load_Report()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P Then
            Load_Report()
        End If
    End Sub

    Private Sub txtMCC__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
        Dim qry As String = "select MCC_Code,MCC_NAME,TSPL_MCC_MASTER.plant_code as [Plant Code],tspl_location_master.location_desc as [Plant Name] from TSPL_MCC_MASTER left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.plant_code where 2=2 "
        txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("PCUMCC", qry, "MCC_Code", "MCC_NAME", txtMCC.arrValueMember, txtMCC.arrDispalyMember)
        RefreshRoute()
        RefreshVLC()
    End Sub

    Private Sub txtRoute__My_Click(sender As Object, e As EventArgs) Handles txtRoute._My_Click
        Try
            Dim qry As String = "select Route_Code,Route_Name from TSPL_MCC_ROUTE_MASTER where 2=2 "
            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                qry += "  and MCC_Code in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")"
            End If

            txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("PCURoute", qry, "Route_Code", "Route_Name", txtRoute.arrValueMember, txtRoute.arrDispalyMember)
            RefreshVLC()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtVLC__My_Click(sender As Object, e As EventArgs) Handles txtVLC._My_Click
        Try
            Dim qry As String = "select TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.Route_Code,TSPL_MCC_ROUTE_MASTER.Route_Name from TSPL_VLC_MASTER_HEAD left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_VLC_MASTER_HEAD.Route_Code where 2=2 and TSPL_VLC_MASTER_HEAD.Active='1' "
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                qry += " and TSPL_VLC_MASTER_HEAD.Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ") "
            End If
            txtVLC.arrValueMember = clsCommon.ShowMultipleSelectForm("PCUVLC1", qry, "VLC_Code", "VLC_Name", txtVLC.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub RefreshRoute()
        If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
            Dim qry As String = "select Route_Code from TSPL_MCC_ROUTE_MASTER where Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")  and MCC_Code in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            txtRoute.arrValueMember = Nothing
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim arr As New ArrayList
                For Each dr As DataRow In dt.Rows
                    arr.Add(clsCommon.myCstr(dr("Route_Code")))
                Next
                txtRoute.arrValueMember = arr
            End If
        End If
    End Sub

    Sub RefreshVLC()
        If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
            Dim qry As String = "select VLC_Code from TSPL_VLC_MASTER_HEAD where  VLC_Code in (" + clsCommon.GetMulcallString(txtVLC.arrValueMember) + ")  and Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")  "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            txtVLC.arrValueMember = Nothing
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim arr As New ArrayList
                For Each dr As DataRow In dt.Rows
                    arr.Add(clsCommon.myCstr(dr("VLC_Code")))
                Next
                txtVLC.arrValueMember = arr
            End If
        End If
    End Sub

End Class
