Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
'===========Created by Preeti gupta Ticket no [BM00000008018]==================

Public Class RptMPWiseMilkCollection
    Inherits FrmMainTranScreen
    Dim dt As DataTable
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim tmpValLoad As Boolean = True
    Dim arrLoc As String = Nothing
    Dim ItemStructureMandatoryOnWeightConversion As Boolean = False
    Dim qry As String
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
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptMPWiseMilkCollection)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
        btnprint.Visible = MyBase.isPrintFlag
    End Sub
    Sub LoadMCC()
        Dim qry As String = Nothing
        If clsCommon.myLen(arrLoc) > 0 Then
            qry = "select TSPL_LOCATION_MASTER.Location_Code as [Code] ,TSPL_LOCATION_MASTER .Location_Desc as [Name] from TSPL_LOCATION_MASTER inner join tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_LOCATION_MASTER.Location_Code where isnull(CSA_Type,'N') ='N' and (isnull(GIT_Type,'N')='N' or isnull(GIT_Type,'N')='') and isnull(Is_Consumption_Location,0) =0  and isnull(Rejected_type,'N') ='N'  and Location_Code in (" + arrLoc + ") "
        Else
            btnGo.Enabled = False
        End If
        cbgMCC.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgMCC.ValueMember = "Code"
        cbgMCC.DisplayMember = "Name"
    End Sub
    Sub LoadVLC()
        Dim qry As String = Nothing
        qry = "select TSPL_VLC_MASTER_HEAD.VLC_Code as [Code],TSPL_VLC_MASTER_HEAD.VLC_Name as [Name]  from TSPL_VLC_MASTER_HEAD "
        cbgVLC.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVLC.ValueMember = "Code"
        cbgVLC.DisplayMember = "Name"
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
    '===================================================================

    Sub FormatGrid()
        Dim summaryItem As New GridViewSummaryItem()
        gv.TableElement.TableHeaderHeight = 25
        gv.MasterTemplate.ShowRowHeaderColumn = True
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = True
        Next

        gv.Columns("Payee Name").IsVisible = False
        gv.Columns("Payee Name").Width = 100
        gv.Columns("Account Type").IsVisible = False
        gv.Columns("Account Type").Width = 50
        gv.Columns("fromDate").IsVisible = False
        gv.Columns("Todate").IsVisible = False
        gv.Columns("companyADD").IsVisible = False
        gv.Columns("CompName").IsVisible = False
        gv.Columns("CompCode").IsVisible = False
        gv.Columns("HSHIFT").IsVisible = False
        gv.Columns("ShiftMor").IsVisible = False
        gv.Columns("ShiftEve").IsVisible = False
        gv.Columns("Unit").IsVisible = False
        gv.Columns("doc_no").IsVisible = False

        gv.Columns("qty").IsVisible = True
        gv.Columns("qty").Width = 100
        gv.Columns("qty").HeaderText = "Qty"
        gv.Columns("qty").FormatString = "{0:F3}"

        gv.Columns("fat").IsVisible = True
        gv.Columns("fat").Width = 100
        gv.Columns("fat").HeaderText = "fat"
        gv.Columns("fat").FormatString = "{0:F2}"

        gv.Columns("Snf").IsVisible = True
        gv.Columns("Snf").Width = 100
        gv.Columns("Snf").HeaderText = "Snf"
        gv.Columns("Snf").FormatString = "{0:F2}"

        gv.Columns("Kg FAT").IsVisible = True
        gv.Columns("Kg FAT").Width = 100
        gv.Columns("Kg FAT").HeaderText = "TFat"
        gv.Columns("Kg FAT").FormatString = "{0:F3}"

        gv.Columns("Kg SNF").IsVisible = True
        gv.Columns("Kg SNF").Width = 100
        gv.Columns("Kg SNF").HeaderText = "TSnf"
        gv.Columns("Kg SNF").FormatString = "{0:F3}"

        gv.Columns("Amount").IsVisible = True
        gv.Columns("Amount").Width = 100
        gv.Columns("Amount").HeaderText = "Amount"
        gv.Columns("Amount").FormatString = "{0:F3}"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As New GridViewSummaryItem("Qty", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        Dim summaryItem1 As New GridViewSummaryItem()
        summaryItem1.FormatString = "{0:F2}"
        summaryItem1.Name = "fat"
        summaryItem1.AggregateExpression = "sum(Kg FAT)*100/sum(Qty)"
        summaryRowItem.Add(summaryItem1)

        Dim summaryItem2 As New GridViewSummaryItem()
        summaryItem2.FormatString = "{0:F2}"
        summaryItem2.Name = "snf"
        summaryItem2.AggregateExpression = "sum(Kg SNF)*100/sum(Qty)"
        summaryRowItem.Add(summaryItem2)

        Dim item6 As New GridViewSummaryItem("Kg FAT", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)
        Dim item7 As New GridViewSummaryItem("Kg SNF", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)

        Dim item12 As New GridViewSummaryItem("Amount", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item12)
        If chkWithoutGroup.Checked = True Then

        Else
            gv.GroupDescriptors.Add(New GridGroupByExpression("[MCC Uploader] as Item format ""{0}: {1}"" Group By [MCC Uploader]"))
            gv.GroupDescriptors.Add(New GridGroupByExpression("[VLC Uploader] as Item format ""{0}: {1}"" Group By [VLC Uploader]"))
            gv.ShowGroupPanel = False
            gv.MasterTemplate.AutoExpandGroups = True
        End If
        
        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub
    Sub Reset()
        gv.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        If chkMCCAll.IsChecked Then
            cbgMCC.CheckedAll()
        Else
            cbgMCC.UnCheckedAll()
        End If
        chkMCCAll.CheckState = CheckState.Checked
        chkVLCAll.CheckState = CheckState.Checked
        gv.DataSource = Nothing
        cboUnit.Text = "Kg"
        EnableDisableControl(True)
    End Sub

    Private Sub EnableDisableControl(ByVal val As Boolean)
        RadGroupBox1.Enabled = val
    End Sub
    Private Sub LoadData()
        If clsCommon.myCDate(txtFromDate.Value) > clsCommon.myCDate(txtToDate.Value) Then
            txtFromDate.Focus()
            clsCommon.MyMessageBoxShow("From date can not be greater then to Date", Me.Text)
            Exit Sub
        End If
        If chkMCCSelect.IsChecked AndAlso cbgMCC.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast single MCC or select all.", Me.Text)
            Exit Sub
        End If
        Dim companyADD, CompName, CompCode As String
        'Dim qry As String

        qry = ""
        qry += " select   TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_COMPANY_MASTER.City_Code)>0 then ', '+TSPL_COMPANY_MASTER.City_Code else ' ' end + case when len(TSPL_COMPANY_MASTER.State )>0 then TSPL_COMPANY_MASTER.State else '' end  as comp_address from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
        companyADD = dt1.Rows(0).Item("comp_address")

        qry = ""
        qry += " select   TSPL_COMPANY_MASTER.Comp_Name  from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry)
        CompName = dt2.Rows(0).Item("Comp_Name")


        qry = ""
        qry += " select   TSPL_COMPANY_MASTER.comp_code  from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        Dim dt5 As DataTable = clsDBFuncationality.GetDataTable(qry)
        CompCode = dt5.Rows(0).Item("Comp_Code")
        Dim Shh As String = String.Empty
        Dim fromDate As Date = clsCommon.GetPrintDate(txtFromDate.Value)

        Dim Todate As Date = clsCommon.GetPrintDate(txtToDate.Value)

        Dim DiffDate As Integer = clsCommon.myCDate(Todate).Day - clsCommon.myCDate(fromDate).Day
        If DiffDate > 0 Then
            Shh = "Both"
        ElseIf DiffDate = 0 And txtFromShift.SelectedValue <> txtToShift.SelectedValue Then
            Shh = "Both"
        Else
            Shh = IIf(txtFromShift.SelectedValue = "M", "Morning", "Evening")
        End If

        Dim QryUploader As String = " select '" & clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.MCCDefaultMilkItem, clsFixedParameterCode.MilkSetting, Nothing)) & "'  as item_code,TSPL_VLC_DATA_UPLOADER.Doc_No , TSPL_Mp_MASTER.Mcc_Code_VLC_Uploader,TSPL_VLC_DATA_UPLOADER.MCC_Code,TSPL_VLC_DATA_UPLOADER.VLC_CODE as VLC_uploader_CODE,TSPL_Mp_MASTER.MCC_NAME ,TSPL_MP_MASTER.VLC_Code ,TSPL_MP_MASTER.VLC_Name,TSPL_Mp_MASTER.MP_Code ,TSPL_VLC_DATA_UPLOADER.MP_CODE   as MP_Code_uploader ,TSPL_MP_MASTER.MP_Name ,TSPL_MP_MASTER.AccountNO ,TSPL_MP_MASTER.BankBranch ,TSPL_MP_MASTER.BankName,TSPL_MP_MASTER.IFCIcode,TSPL_VLC_DATA_UPLOADER.Doc_Date ,TSPL_VLC_DATA_UPLOADER.shift,TSPL_VLC_DATA_UPLOADER.qty as qty,TSPL_VLC_DATA_UPLOADER.fat, TSPL_VLC_DATA_UPLOADER.snf ,TSPL_VLC_DATA_UPLOADER.Amount ,TSPL_Mp_Master.UOM_Code,TSPL_MP_MASTER.Route_Code,RT.Route_Name,TSPL_MP_MASTER.PayeeName,TSPL_MP_MASTER.Education    from  (select TSPL_Mp_MASTER.MP_Code ,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader ,VLC_Code_VLC_Uploader,MP_Code_VLC_Uploader,Mp_Name,TSPL_MP_MASTER.AccountNO,TSPL_MP_MASTER.BankBranch,TSPL_MP_MASTER.BankName,TSPL_MP_MASTER.IFCIcode ,TSPL_VLC_MASTER_HEAD.MCC, MCC_NAME,UOM_Code,TSPL_VLC_MASTER_HEAD.VLC_Code ,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.Route_Code,TSPL_MP_MASTER.PayeeName,TSPL_MP_MASTER.Education  " & Environment.NewLine & _
        " from TSPL_VLC_MASTER_HEAD   left join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code =TSPL_VLC_MASTER_HEAD.MCC " & Environment.NewLine & _
        " Left join TSPL_Mcc_UOM_DETAIL on TSPL_Mcc_UOM_DETAIL.MCC_CODE =TSPL_MCC_MASTER.MCC_Code and Stocking_Unit ='Y' Left join TSPL_MP_MASTER on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MP_MASTER.VLC_Code  left join TSPL_BANK_BRANCH_MASTER on TSPL_BANK_BRANCH_MASTER.BRANCH_CODE =TSPL_MP_MASTER.BankBranch  left join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE =TSPL_MP_MASTER.BankName) TSPL_MP_MASTER left join TSPL_VLC_DATA_UPLOADER " & Environment.NewLine & _
        " on TSPL_MP_MASTER.MP_Code_VLC_Uploader=TSPL_VLC_DATA_UPLOADER.MP_CODE and TSPL_MP_MASTER.VLC_Code_VLC_Uploader=TSPL_VLC_DATA_UPLOADER.VLC_CODE and TSPL_MP_MASTER.MCC =TSPL_VLC_DATA_UPLOADER.MCC_Code " & Environment.NewLine & _
        " left join TSPL_MCC_ROUTE_MASTER RT on TSPL_MP_MASTER.Route_Code=RT.Route_Code  where 2=2"

        If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal Then
            QryUploader += " and 2=( case when File_Date >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and File_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and shift='M' then 3 else 2 end  )"
        End If
        If clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
            QryUploader += " and 2=( case when File_Date >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and File_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and shift='E' then 3 else 2 end  )"
        End If

        If cbgMCC.CheckedValue.Count > 0 Then
            QryUploader += " and TSPL_VLC_DATA_UPLOADER.MCC_Code  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
        End If
        If cbgVLC.CheckedValue.Count > 0 AndAlso chkVLCselect.IsChecked Then
            QryUploader += " and TSPL_MP_MASTER.VLC_Code  IN (" + clsCommon.GetMulcallString(cbgVLC.CheckedValue) + ") "
        End If
        If Not txtRoute.arrValueMember Is Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
            QryUploader += " and TSPL_MP_MASTER.Route_Code  IN (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ") "
        End If
        QryUploader += "  and convert(date,File_Date,103)>=convert(date,('" + txtFromDate.Value + "'),103) and convert(date,File_Date,103) <=convert(date,('" + txtToDate.Value + "'),103) and qty >0 "

        '' query change by Panch Raj against ticket No:KDI/21/05/18-000323-> Route code must be picked from vlc master
        Dim QryManual As String = "select '" & clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.MCCDefaultMilkItem, clsFixedParameterCode.MilkSetting, Nothing)) & "'  as item_code, VDUM.document_code,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader, VLCM.MCC,VLCM.vlc_code_vlc_uploader AS VLC_UPLODER_CODE," & Environment.NewLine & _
        " tspl_mcc_master.mcc_name,VDUM.VLC_Code,VLCM.vlc_Name as VLC_Name,TSPL_MP_MASTER.mp_code,TSPL_MP_MASTER.MP_Code_VLC_Uploader,TSPL_MP_MASTER.mp_name,TSPL_MP_MASTER.AccountNO," & Environment.NewLine & _
        " TSPL_MP_MASTER.BankBranch,TSPL_MP_MASTER.BankName,TSPL_MP_MASTER.IFCIcode ,VDUM.Document_Date as Document_Date,(case when VDUM.Shift='MORNING' THEN 'M' ELSE 'E' END) AS Shift," & Environment.NewLine & _
        " VDUD.Qty,VDUD.fatper,VDUD.snfper,VDUD.Amount as VLC_Amount,VDUD.Unit_Code,VLCM.Route_Code," & Environment.NewLine & _
        "  tspl_mcc_route_master.route_name,TSPL_MP_MASTER.PayeeName,TSPL_MP_MASTER.Education from TSPL_VLC_DATA_UPLOADER_DETAIL VDUD  inner join TSPL_VLC_DATA_UPLOADER_MASTER VDUM on VDUD.Document_Code=VDUM.Document_Code  " & Environment.NewLine & _
        " left join TSPL_VLC_MASTER_HEAD VLCM on VDUM.VLC_Code=VLCM.VLC_Code  left join tspl_mcc_master on tspl_mcc_master.mcc_code=VLCM.MCC " & Environment.NewLine & _
        " left join tspl_mp_master on tspl_mp_master.mp_code=VDUD.farmer_code left join tspl_mcc_route_master on tspl_mcc_route_master.Route_Code=VLCM.Route_Code " & Environment.NewLine & _
        " where 2 = 2  and  convert(date, VDUM.Document_Date,103) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and  convert(date, VDUM.Document_Date,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'"

        If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal Then
            QryManual += " and 2=( case when convert(date, VDUM.Document_Date,103) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and convert(date, VDUM.Document_Date,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and SHIFT='MORNING' then 3 else 2 end  )"
        End If
        If clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
            QryManual += " and 2=( case when convert(date, VDUM.Document_Date,103) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and convert(date, VDUM.Document_Date,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and SHIFT='EVENING' then 3 else 2 end  )"
        End If
        If cbgMCC.CheckedValue.Count > 0 Then
            QryManual += " and VLCM.MCC  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
        End If
        If cbgVLC.CheckedValue.Count > 0 Then
            QryManual += " and VLCM.VLC_Code IN (" + clsCommon.GetMulcallString(cbgVLC.CheckedValue) + ") "
        End If
        If Not txtRoute.arrValueMember Is Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
            QryManual += " and VLCM.Route_Code  IN (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ") "
        End If


        qry = ""
        qry = "select '" + txtFromShift.Text + "' as ShiftMor,'" + txtToShift.Text + "' as ShiftEve,'" + cboUnit.Text + "' as Unit, '" & Shh & "'  as HShift ,'" & clsCommon.myCDate(fromDate) & "' as fromDate ,'" & clsCommon.myCDate(Todate) & "' as Todate ,'" & companyADD & "'  as companyADD, '" & CompName & "'  as CompName,'" & CompCode & "'  as CompCode," & Environment.NewLine & _
        " convert(varchar,final.Doc_Date,103) as [Doc Date],final.MCC_Code as [MCC Code],final.Mcc_Code_VLC_Uploader as [MCC Uploader] ,final.MCC_NAME as [MCC Name],final.VLC_uploader_CODE as [VLC Uploader],final.VLC_Code as [VLC Code],final.VLC_Name as [VLC Name],final.Route_Code as [Route Code],Final.Route_Name as [Route Name],final.shift  ,Case When Coalesce(final.FAT, 0) <= 0 Then '' When Coalesce(final.FAT, 0) <= 5 Then 'C' Else 'B' End As [CTyp],final.MP_Code_uploader as [MP Uploader],final.MP_Code as [MP Code] ,final.MP_Name as [MP Name],final.BankName,final.BankBranch,final.PayeeName as [Payee Name],final.Education as [Account Type], final.AccountNo,final.IFCIcode as IFSCCode ,Case When final.FAT <= 5 Then  final.NewQty else Case When final.FAT > 5 Then final.NewQty end end as Qty," & Environment.NewLine & _
        " Case When final.FAT <= 5 Then final.FAT else Case When final.FAT > 5 Then final.FAT end end as FAT," & Environment.NewLine & _
        " Case When final.FAT >= 5 Then final.SNF Else Case When final.FAT < 5 Then final.SNF end end SNF," & Environment.NewLine & _
        " Case When final.FAT  <= 5 Then final.NewFAT_KG  Else Case When final.FAT > 5 Then final.NewFAT_KG  end end as [Kg FAT]," & Environment.NewLine & _
        " Case When final.FAT > 5 Then final.NewSNF_KG Else Case When final.FAT <= 5 Then final.NewSNF_KG end end as [Kg SNF]," & Environment.NewLine & _
        " Case When final.FAT <= 5 Then final.NewAmount Else Case When final.FAT > 5 Then final.NewAmount   end end [Amount],doc_no" & Environment.NewLine & _
        " from ( select xx.*,xx.qty*CF as NewQty,xx.Amount*CF as NewAmount,fat_KG *Cf as NewFAT_KG,snf_KG *cf as NewSNF_KG from (" & Environment.NewLine & _
        " select *,(qty *fat) /100 as fat_KG,(qty *snf) /100 as snf_KG from (" & Environment.NewLine & _
        " " & QryUploader & " " & Environment.NewLine & _
        " union all " & Environment.NewLine & _
         " " & QryManual & "" & Environment.NewLine & _
         " ) as pp )xx" & Environment.NewLine & _
          " LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code =xx.Item_Code "

        ''richa agarwal 04 jUN,2019  TEC/28/03/19-000462 add item structure on setting based
        If ItemStructureMandatoryOnWeightConversion = True Then
            qry += " left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF,Structure_code  from TSPL_WEIGHT_CONVERSION where Contained_UOM='KG' UNION All Select Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/ nullif (Contained_Qty,0) as CF,Structure_code from TSPL_WEIGHT_CONVERSION where Container_UOM='LTR' UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF,Structure_code  from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF,Structure_code  from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =xx.UOM_Code   and lower(zzz.TOUOM)='" + cboUnit.Text + "' where 2 = 2 AND TSPL_ITEM_MASTER.Structure_Code =zzz.Structure_code ) "
        Else
            qry += " left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF  " & Environment.NewLine &
                " from TSPL_WEIGHT_CONVERSION where Contained_UOM='KG' UNION All Select Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/Contained_Qty as CF from TSPL_WEIGHT_CONVERSION where Container_UOM='LTR' " & Environment.NewLine &
                " UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION  UNION All Select Container_UOM as FromUOM, " & Environment.NewLine &
                "   Container_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION  )as yyy)zzz on zzz.FromUOM =UOM_Code  and (zzz.TOUOM)='" + cboUnit.Text + "' ) "
        End If

        qry += "    as  final order by convert(date,Doc_Date,103) "

        dt = clsDBFuncationality.GetDataTable(qry)
        gv.DataSource = Nothing
        gv.Rows.Clear()
        gv.Columns.Clear()
        gv.DataSource = dt
        gv.GroupDescriptors.Clear()
        gv.MasterTemplate.SummaryRowsBottom.Clear()
        FormatGrid()
        If btnReferesh = False Then
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dt, clsERPFuncationality.CompanyAddresShowinHeader(), "crptMilkCollectionAsPerDataEntry", "Milk Collection As Per Data Entry", "")
            frmCRV = Nothing
        End If
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
            Exit Sub
        End If
        gv.BestFitColumns()
        RadPageView1.SelectedPage = RadPageViewPage2
        ReStoreGridLayout()
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

    Sub print(ByVal exporter As EnumExportTo)
        Try
            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                Dim CompName As String = clsDBFuncationality.getSingleValue("Select Comp_Name from TSPL_COMPANY_MASTER Where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'")
                arrHeader.Add(CompName)
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
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
                    arrHeader.Add(("MCC Name: " + strMCCName + " "))
                End If
                If chkVLCselect.IsChecked Then
                    Dim strMCCName As String = ""
                    For Each StrName As String In cbgVLC.CheckedDisplayMember
                        If clsCommon.myLen(strMCCName) > 0 Then
                            strMCCName += ", "
                        End If
                        strMCCName += StrName
                    Next
                    Dim strMCCCode As String = ""
                    For Each StrCode As String In cbgVLC.CheckedValue
                        If clsCommon.myLen(strMCCCode) > 0 Then
                            strMCCCode += ", "
                        End If
                        strMCCCode += StrCode
                    Next
                    arrHeader.Add(("VLC Name: " + strMCCName + " "))
                End If
                If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                    arrHeader.Add(("Route : " + clsCommon.GetMulcallStringWithComma(txtRoute.arrDispalyMember) + " "))
                End If

                If exporter = EnumExportTo.Excel Then
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    clsCommon.MyExportToExcelGrid("Milk Collection As Per Data Entry", gv, arrHeader, Me.Text)
                ElseIf exporter = EnumExportTo.PDF Then
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("Milk Collection As Per Data Entry", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                Else
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
                End If
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub RptMPWiseMilkCollection_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ItemStructureMandatoryOnWeightConversion = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ItemStructureMandatoryOnWeightConversion, clsFixedParameterCode.ItemStructureMandatoryOnWeightConversion, Nothing)) = 1, True, False))
        LOCATIONRIGTHS()
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        RadPageView1.SelectedPage = RadPageViewPage1
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        LoadMCC()
        LoadVLC()
        LoadShiftFrom()
        LoadShiftTo()
        Reset()
    End Sub
    Private Sub RptMPWiseMilkCollection_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            LoadData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()
        End If
    End Sub

    Private Sub chkMCCAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkMCCAll.ToggleStateChanged
        cbgMCC.Enabled = Not chkMCCAll.IsChecked
        If chkMCCAll.IsChecked Then
            cbgMCC.CheckedAll()
        Else
            cbgMCC.UnCheckedAll()
        End If
    End Sub

    Private Sub chkVLCAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkVLCAll.ToggleStateChanged
        cbgVLC.Enabled = Not chkVLCAll.IsChecked
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        btnReferesh = True
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        LoadData()
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub btnQuickexport_Click(sender As Object, e As EventArgs)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptMPWiseMilkCollection & "'"))

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

            transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click
        btnReferesh = False
        LoadData()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
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
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub
    Private Sub rmdeleteLayout_Click(sender As Object, e As EventArgs) Handles rmdeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub
    Private Sub txtRoute__My_Click(sender As Object, e As EventArgs) Handles txtRoute._My_Click
        Dim qry As String = " SELECT  DISTINCT RT.Route_Code as Code,RT.Route_Name as Name FROM TSPL_MCC_ROUTE_MASTER RT LEFT JOIN TSPL_VLC_MASTER_HEAD VLC ON RT.Route_Code=VLC.Route_Code where 2=2 "
        If cbgVLC.CheckedValue.Count > 0 AndAlso chkVLCselect.IsChecked Then
            qry += " and VLC.VLC_Code  IN (" + clsCommon.GetMulcallString(cbgVLC.CheckedValue) + ") "
        End If
        txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("RouteMulSel", qry, "Code", "Name", txtRoute.arrValueMember, txtRoute.arrDispalyMember)
    End Sub
    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        print(EnumExportTo.Excel)
    End Sub
    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        print(EnumExportTo.PDF)
    End Sub

    Private Sub RmiQuickExcel_Click(sender As Object, e As EventArgs) Handles rmiQuickExcel.Click
        print(Nothing)
    End Sub

    Private Sub rmiBulkExcel_Click(sender As Object, e As EventArgs) Handles rmiBulkExcel.Click
        Try
            If clsCommon.myLen(qry) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "First Run the report.", Me.Text)
                Exit Sub
            End If
            clsCommon.ProgressBarPercentShow()
            clsCommon.ProgressBarPercentUpdate(0, "Generating query for the report..")
            Dim StrReportName As String = clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptMPWiseMilkCollectionATPoolingPoint & "'")
            qry = "select * from (" & qry & ") PP order by convert(date,Doc_Date,103)"
            clsCommon.ProgressBarPercentUpdate(10, "Query generated..starting bulk export..")
            transportSql.BulkExport(StrReportName, qry, "order by convert(date,Doc_Date,103)", "xls")
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, "Data exported successfully", Me.Text)
        Catch ex As Exception
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            clsCommon.ProgressBarPercentHide()
        End Try
    End Sub
End Class
