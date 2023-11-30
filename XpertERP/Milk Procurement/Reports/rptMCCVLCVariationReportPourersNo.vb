Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Public Class rptMCCVLCVariationReportPourersNo
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim tmpValLoad As Boolean = True
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
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptMCCVLCVariationReportPourersNo)
        If Not (MyBase.isReadFlag) Then
            If MDI.blnShowAllMenu = False Then
                Throw New Exception("Permission Denied")
            Else
                Throw New Exception("Can't Access in demo version. " + Environment.NewLine + " For any queries/details, contact tecxpert@tecxpert.in. ")
            End If
        End If
        RadSplitButton1.Visible = MyBase.isExport
        'btnQuickExport.Visible = MyBase.isExport
        'btnPrint.Visible = MyBase.isPrintFlag

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

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        btnReferesh = True
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        Load_Report()
        'If tmpValLoad Then
        '    Load_Report()
        'End If
        tmpValLoad = False
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        btnReferesh = False
        Load_Report()
    End Sub

    Private Sub rmExcel_Click(sender As Object, e As EventArgs) Handles rmExcel.Click
        print(EnumExportTo.Excel)
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                Dim CompName As String = clsDBFuncationality.getSingleValue("Select Comp_Name from TSPL_COMPANY_MASTER Where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'")
                arrHeader.Add(CompName)
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")


                'If rbtnMCCRouteVLCCSelect.IsChecked Then
                Dim arr As List(Of String)
                If cbtMCCRouteVLCC.CheckedText.Count > 0 Then
                    arr = cbtMCCRouteVLCC.CheckedText(1)
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        arrHeader.Add(("MCC : " + clsCommon.GetMulcallStringWithComma(arr) + " "))
                    End If
                End If
                If cbtMCCRouteVLCC.CheckedText.Count > 1 Then
                    arr = cbtMCCRouteVLCC.CheckedText(2)
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        arrHeader.Add(("Route : " + clsCommon.GetMulcallStringWithComma(arr) + " "))
                    End If
                End If
                If cbtMCCRouteVLCC.CheckedText.Count > 2 Then
                    arr = cbtMCCRouteVLCC.CheckedText(3)
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        arrHeader.Add(("VLC : " + clsCommon.GetMulcallStringWithComma(arr) + " "))
                    End If
                End If
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                If exporter = EnumExportTo.Excel Then
                    clsCommon.MyExportToExcelGrid("MCC/VLC Variation Report & Pourers No", gv, arrHeader, Me.Text)
                Else
                    clsCommon.MyExportToPDF("MCC/VLC Variation Report & Pourers No", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        print(EnumExportTo.PDF)
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

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub

    Private Sub rpt1VillageDifferenceREportParas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ItemStructureMandatoryOnWeightConversion = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ItemStructureMandatoryOnWeightConversion, clsFixedParameterCode.ItemStructureMandatoryOnWeightConversion, Nothing)) = 1, True, False))
        LOCATIONRIGTHS()
        SetUserMgmtNew()
        cboUnit.Text = "Kg"
        'rbtnMCCRouteVLCCAll.IsChecked = True

        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P For Print")
        ButtonToolTip.SetToolTip(BtnReset, "Pres%s Alt+N Adding New")
        RadPageView1.SelectedPage = RadPageViewPage1
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()

        LoadMCCRouteVLCTree()

        LoadShiftFrom()
        LoadShiftTo()

        Reset()
    End Sub
    Sub LoadMCCRouteVLCTree()

        Dim qry As String = Nothing
        Dim dt As DataTable = Nothing
        If clsCommon.myLen(arrLoc) > 0 Then
            qry = "select TSPL_VLC_MASTER_HEAD.VLC_Code as Code,TSPL_VLC_MASTER_HEAD.VLC_Name as Name,TSPL_VLC_MASTER_HEAD.Route_Code as ParentCode,3 as Lvl from TSPL_VLC_MASTER_HEAD where len(isnull(TSPL_VLC_MASTER_HEAD.Route_Code,''))>0 union all   select TSPL_MCC_ROUTE_MASTER.Route_Code as Code,TSPL_MCC_ROUTE_MASTER.Route_Name as Name,TSPL_MCC_ROUTE_MASTER.MCC_Code as ParentCode,2 as Lvl from TSPL_MCC_ROUTE_MASTER where len(isnull(TSPL_MCC_ROUTE_MASTER.MCC_Code,''))>0  union all   select TSPL_MCC_MASTER.MCC_Code as Code,TSPL_MCC_MASTER.MCC_NAME as Name,null as ParentCode,1 as Lvl from TSPL_MCC_MASTER   where TSPL_MCC_MASTER.MCC_Code in (" + arrLoc + ") "
            dt = clsDBFuncationality.GetDataTable(qry)
        End If

        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            btnGo.Enabled = False
        Else
            cbtMCCRouteVLCC.DataSource = dt
            cbtMCCRouteVLCC.ValueMember = "Code"
            cbtMCCRouteVLCC.DisplayMember = "Name"
            cbtMCCRouteVLCC.ParentValue = "ParentCode"
        End If

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
        'cbgShift.DisplayMember = "Shift"
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
        'cbgShift.DisplayMember = "Shift"
    End Sub
    Public Sub Load_Report()
        Try
            '==changes by shivani [BM00000007518]
            'Ticket No : BHA/15/02/19-000816 by Prabhakar For   Divide by Zero issue
            Dim sQuery As String

            If txtFromDate.Value > txtToDate.Value Then
                txtFromDate.Focus()
                Throw New Exception("From date can not be greater then to Date")
            End If
            If cbtMCCRouteVLCC.CheckedValue.Count = 0 Then
                Throw New Exception("Please select atleast single MCC or select all.")
            End If
            Dim PeriodDate As String = "'Period :  From  ' + '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") & "' + ' " & txtFromShift.SelectedValue & "'+' To ' + '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") & "' + ' " & txtToShift.SelectedValue & "'"

            Dim QryUploader As String = " select '" & clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.MCCDefaultMilkItem, clsFixedParameterCode.MilkSetting, Nothing)) & "'  as item_code,TSPL_VLC_DATA_UPLOADER.Doc_No,convert(varchar,TSPL_VLC_DATA_UPLOADER.File_Date,103) as " & _
                " File_Date,TSPL_VLC_DATA_UPLOADER.shift,TSPL_VLC_DATA_UPLOADER.MCC_Code,TSPL_VLC_DATA_UPLOADER.Route_No,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader," & _
                " TSPL_VLC_MASTER_HEAD.VLC_CODE, TSPL_VLC_MASTER_HEAD.VSP_Code  as VSPCode,TSPL_VLC_DATA_UPLOADER.Uom_Code ,tspl_mp_master.mp_code,tspl_mp_master.MP_Name,0 as MCC_Qty, 0 as" & _
                " MCC_FAT, 0 as MCC_SNF , 0 as MCC_Amount, isnull(TSPL_VLC_DATA_UPLOADER.qty,0) as VLC_QTY,TSPL_VLC_DATA_UPLOADER.fat as   " & _
                " VLC_Fat ,TSPL_VLC_DATA_UPLOADER.snf as VLC_SNF  , isnull(TSPL_VLC_DATA_UPLOADER.Amount,0) as VLC_Amount from TSPL_VLC_DATA_UPLOADER " & _
                " left join TSPL_MCC_MASTER on TSPL_MCC_MASTER .Mcc_Code_VLC_Uploader =TSPL_VLC_DATA_UPLOADER.MCC_Code left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader=TSPL_VLC_DATA_UPLOADER.VLC_CODE  and TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader =TSPL_VLC_DATA_UPLOADER.MCC_Code  " & _
                " left join TSPL_MP_MASTER on TSPL_MP_MASTER.MP_Code_VLC_Uploader =TSPL_VLC_DATA_UPLOADER.MP_CODE  and TSPL_MP_MASTER. VLC_Code =TSPL_VLC_MASTER_HEAD.VLC_CODE " & _
                " where 2 = 2  and  convert(date, File_Date,103) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and  convert(date, File_Date,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'"

            If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal Then
                QryUploader += " and 2=( case when convert(date, File_Date,103) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and convert(date, File_Date,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and SHIFT='M' then 3 else 2 end  )"
            End If
            If clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
                QryUploader += " and 2=( case when convert(date, File_Date,103) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and convert(date, File_Date,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and SHIFT='E' then 3 else 2 end  )"
            End If

            Dim QryManual As String = " select '" & clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.MCCDefaultMilkItem, clsFixedParameterCode.MilkSetting, Nothing)) & "'  as item_code, VDUD.Document_Code as Doc_No,(case when VDUM.Shift='MORNING' THEN 'M' ELSE 'E' END) AS Shift,  convert(varchar,VDUM.Document_Date,103) as Document_Date, VLCM.MCC as MCC_Code,VDUM.Route_Code,VLCM.VLC_Code_VLC_Uploader,VDUM.VLC_Code,VLCM.VSP_Code,VDUD.Unit_Code,tspl_mp_master.mp_code,tspl_mp_master.MP_Name, 0 as MCC_Qty, 0 as MCC_FAT, 0 as MCC_SNF,0 as MCC_Amount, VDUD.Qty,VDUD.FatPer as VLC_FAT ,VDUD.SNFPer as VLC_SNF,  VDUD.Amount as VLC_Amount " _
               & " from TSPL_VLC_DATA_UPLOADER_DETAIL VDUD  inner join TSPL_VLC_DATA_UPLOADER_MASTER VDUM on VDUD.Document_Code=VDUM.Document_Code " _
               & " left join TSPL_VLC_MASTER_HEAD VLCM on VDUM.VLC_Code=VLCM.VLC_Code " _
               & " left join tspl_mp_master on tspl_mp_master.mp_code=VDUD.farmer_code " _
               & " left join tspl_mcc_route_master on tspl_mcc_route_master.Route_Code=VDUM.Route_Code " _
               & " where 2 = 2  and  convert(date, VDUM.Document_Date,103) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and  convert(date, VDUM.Document_Date,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'"

            If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal Then
                QryManual += " and 2=( case when convert(date, VDUM.Document_Date,103) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and convert(date, VDUM.Document_Date,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and SHIFT='MORNING' then 3 else 2 end  )"
            End If
            If clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
                QryManual += " and 2=( case when convert(date, VDUM.Document_Date,103) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and convert(date, VDUM.Document_Date,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and SHIFT='EVENING' then 3 else 2 end  )"
            End If

            Dim QryMCC As String = " select tspl_milk_srn_detail.item_code,tspl_milk_srn_detail.DOC_CODE ,SHIFT,convert(varchar,DOC_DATE,103) as DOC_DATE ,tspl_milk_srn_Head.MCC_Code,  " & _
                " TSPL_VLC_MASTER_HEAD.ROUTE_CODE as Route_No,VLC_Code_VLC_Uploader,TSPL_VLC_MASTER_HEAD.VLC_Code ,TSPL_VLC_MASTER_HEAD.VSP_Code  as " & _
                " VSPCode,tspl_milk_srn_detail.UOM_Code ,'' as mp_code ,'' as MP_Name,isnull(tspl_milk_srn_detail.Qty,0) as MCC_Qty,tspl_milk_srn_detail.FAT_PER as " & _
                " MCC_FAT,tspl_milk_srn_detail.SNF_PER as MCC_SNF,  isnull(tspl_milk_srn_detail.AMOUNT,0) AS MCC_Amount ,   0 as  VLC_QTY,0 as VLC_FAT,0 as " & _
                " VLC_SNF  ,0 as VLC_Amount from tspl_milk_srn_detail " & _
                " left join tspl_milk_srn_Head on tspl_milk_srn_Head.DOC_CODE =tspl_milk_srn_detail.DOC_CODE 	left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code =tspl_milk_srn_Head.VLC_CODE " & _
                " where 2 = 2  and  convert(date, DOC_DATE,103) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and  convert(date, DOC_DATE,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'"

            If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal Then
                QryMCC += " and 2=( case when convert(date, DOC_DATE,103) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and convert(date, DOC_DATE,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and SHIFT='M' then 3 else 2 end  )"
            End If
            If clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
                QryMCC += " and 2=( case when convert(date, DOC_DATE,103) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and convert(date, DOC_DATE,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and SHIFT='E' then 3 else 2 end  )"
            End If
            Dim arr As List(Of String) = Nothing
            If cbtMCCRouteVLCC.CheckedValue.Count > 0 Then
                arr = cbtMCCRouteVLCC.CheckedValue(1)
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    QryUploader += "and TSPL_VLC_DATA_UPLOADER.MCC_Code  IN (" + clsCommon.GetMulcallString(arr) + ") "
                    QryManual += "and VLCM.MCC  IN (" + clsCommon.GetMulcallString(arr) + ") "
                    QryMCC += "and tspl_milk_srn_Head.MCC_Code  IN (" + clsCommon.GetMulcallString(arr) + ") "
                Else
                    Throw New Exception("Please select at least one MCC")
                End If
            End If

            sQuery = " (select " & PeriodDate & " as PeriodDate,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") & "'  as fromDate ,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") & "'  as Todate , companyADD, '" & objCommonVar.CurrentCompanyName & "'  as CompName,'" & objCommonVar.CurrentCompanyCode & "'  as CompCode,"
            '& " TSPL_COMPANY_MASTER.Logo_Img  as compLogo1 ,TSPL_COMPANY_MASTER.Logo_Img2 as compLogo2" _
            sQuery += " DOC_CODE,DOC_DATE ,  MCC_Code , 'MCCId :'+ MCC_Code +' MCCName: '+ MCC_NAME  as MCC_NAME  , Route_No , Route_name as Route_name,VLC_Code_VLC_Uploader ,VLC_Code  ,VLC_Name,VSPCode,Vendor_Name ,MCC_Qty as MCC_Qty, MCC_TFAT as MCC_TFAT, MCC_TSNF as MCC_TSNF , MCC_Amount as MCC_Amount,mp_code,MP_Name,VLC_QTY as  VLC_QTY, VLC_TFAT as VLC_TFAT, VLC_TSNF as VLC_TSNF,VLC_Amount as VLC_Amount ,FromUOM,TOUOM,(MCC_Qty-VLC_QTY) as Diff_Qty, (MCC_TFAT-VLC_TFAT) as Diff_TFAT, (MCC_TSNF-VLC_TSNF) as Diff_TSNF,(MCC_Amount-VLC_Amount) as Diff_Amt  from " _
               & " (select max(ff.DOC_CODE) as DOC_CODE,ff.SHIFT,max(ff.DOC_DATE) as DOC_DATE,ff.MCC_Code,max(ff.MP_Name) as MP_Name,max(MCC_NAME) as MCC_NAME ,ff.Route_No,max(Route_Name) as Route_Name ,ff.VLC_Code_VLC_Uploader,ff.VLC_Code,max(VLC_Name) as VLC_Name ,ff.VSPCode ,max(TSPL_VENDOR_MASTER.Vendor_Name) as Vendor_Name,max(ff.UOM_Code) as UOM_Code ,ff.mp_code,sum( MCC_Qty) as MCC_Qty,sum( MCC_TFAT) as MCC_TFAT,sum(MCC_TSNF) as MCC_TSNF ,sum(MCC_Amount) as MCC_Amount,sum(VLC_QTY) as VLC_QTY,sum(VLC_TFAT) as VLC_TFAT,sum(VLC_TSNF) as VLC_TSNF,sum(VLC_Amount) as VLC_Amount,max(FromUOM) as FromUOM,max(TOUOM) as TOUOM, max(TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ','+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' 'end + case when LEN(TSPL_COMPANY_MASTER.City_Code)>0 then ', '+TSPL_COMPANY_MASTER.City_Code else ' ' end + ','+case when len(TSPL_STATE_MASTER.STATE_NAME  )>0 then TSPL_STATE_MASTER.STATE_NAME else '' end)  as companyADD  from" _
               & " (select ttt.* from ( select DOC_CODE,SHIFT, DOC_DATE ,MCC_Code,Route_No ,VLC_Code_VLC_Uploader,VLC_Code,VSPCode,UOM_Code,mp_code,MP_Name , (cf * MCC_Qty) as MCC_Qty,(cf *MCC_TFAT) as MCC_TFAT, (cf * MCC_TSNF) as MCC_TSNF , (cf * MCC_Amount) as MCC_Amount, (cf * VLC_QTY) as  VLC_QTY, (cf * VLC_TFAT) as VLC_TFAT , (cf*VLC_TSNF) as VLC_TSNF ,(cf * VLC_Amount) as  VLC_Amount, FromUOM ,TOUOM  from   (  select *,(VLC_QTY  *VLC_Fat ) /100 as VLC_TFAT,(VLC_QTY *VLC_SNF ) /100 as VLC_TSNF, (MCC_Qty   *MCC_Fat  ) /100 as MCC_TFAT,(MCC_Qty *MCC_SNF  ) /100 as MCC_TSNF " _
               & " from " _
               & " ( " & QryMCC & " " _
               & " union all " _
               & " " & QryUploader & "" _
               & " union all " _
               & " " & QryManual & " )pp ) xx " & Environment.NewLine & _
            " LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code =xx.Item_Code "

            ''richa agarwal 24 May,2019  TEC/28/03/19-000462 add item structure on setting based
            If ItemStructureMandatoryOnWeightConversion = True Then
                sQuery += " left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF,Structure_code  from TSPL_WEIGHT_CONVERSION UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF,Structure_code  from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF,Structure_code  from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =xx.UOM_Code   and lower(zzz.TOUOM)='" + cboUnit.Text + "' where 2 = 2 AND TSPL_ITEM_MASTER.Structure_Code =zzz.Structure_code ) "
            Else
                sQuery += " left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF from TSPL_WEIGHT_CONVERSION UNION All Select Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/ nullif (Contained_Qty,0) as CF from TSPL_WEIGHT_CONVERSION UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =xx.UOM_Code and lower(zzz.TOUOM)='" + cboUnit.Text + "' ) "
            End If

            sQuery += "   ttt  )ff left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_name='" & objCommonVar.CurrentCompanyName & "'   left join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE =TSPL_COMPANY_MASTER.State " _
               & " left join  TSPL_mcc_ROUTE_MASTER  on TSPL_mcc_ROUTE_MASTER.Route_code=ff.Route_No left outer join TSPL_MCC_MASTER  on TSPL_MCC_MASTER .MCC_Code =ff .MCC_CODE    left join TSPL_VLC_MASTER_HEAD  on TSPL_VLC_MASTER_HEAD.VLC_Code=ff.VLC_CODE left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = TSPL_VLC_MASTER_HEAD.VSP_Code " _
               & " where 2 = 2     "

            If cbtMCCRouteVLCC.CheckedValue.Count > 0 Then
                arr = cbtMCCRouteVLCC.CheckedValue(1)
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    '  sQuery += "and TSPL_MCC_MASTER.MCC_Code  IN (" + clsCommon.GetMulcallString(arr) + ") "
                Else
                    Throw New Exception("Please select at least one MCC")
                End If
            End If
            If cbtMCCRouteVLCC.CheckedValue.Count > 1 Then
                arr = cbtMCCRouteVLCC.CheckedValue(2)
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    sQuery += " and ff.Route_No in (" + clsCommon.GetMulcallString(arr) + ")  "
                Else
                    Throw New Exception("Please select at least one Route")
                End If
            End If
            If cbtMCCRouteVLCC.CheckedValue.Count > 2 Then
                arr = cbtMCCRouteVLCC.CheckedValue(3)
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    sQuery += " and ff.VLC_Code in (" + clsCommon.GetMulcallString(arr) + ")  "
                Else
                    Throw New Exception("Please select at least one Route")
                End If
            End If
            'End If

            sQuery += " group by ff.MCC_Code ,Route_No,ff.VLC_CODE,ff.VLC_Code_VLC_Uploader ,ff.SHIFT,ff.mp_code,ff.VSPCode) MM )"

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
            'View()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Sub FormatGrid()
        ' Dim strItemCode, head2 As String

        gv.TableElement.TableHeaderHeight = 25
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
            'If chkcatewise.Checked AndAlso ii > 18 Then
            '    gv.Columns(ii).IsVisible = True
            '    gv.Columns(ii).Width = 100
            'End If
        Next

        gv.Columns("DOC_CODE").IsVisible = True
        gv.Columns("DOC_CODE").Width = 100
        gv.Columns("DOC_CODE").HeaderText = "DOC Code"

        gv.Columns("DOC_DATE").IsVisible = True
        gv.Columns("DOC_DATE").Width = 100
        gv.Columns("DOC_DATE").HeaderText = "DOC DATE"

        gv.Columns("MCC_Code").IsVisible = True
        gv.Columns("MCC_Code").Width = 100
        gv.Columns("MCC_Code").HeaderText = "MCC Code"

        gv.Columns("MCC_NAME").IsVisible = True
        gv.Columns("MCC_NAME").Width = 100
        gv.Columns("MCC_NAME").HeaderText = "MCC NAME"

        gv.Columns("Route_No").IsVisible = True
        gv.Columns("Route_No").Width = 100
        gv.Columns("Route_No").HeaderText = "Route Code"

        gv.Columns("Route_name").IsVisible = True
        gv.Columns("Route_name").Width = 100
        gv.Columns("Route_name").HeaderText = "Route Name"

        'VLC_Code_VLC_Uploader

        gv.Columns("VLC_Code_VLC_Uploader").IsVisible = True
        gv.Columns("VLC_Code_VLC_Uploader").Width = 100
        gv.Columns("VLC_Code_VLC_Uploader").HeaderText = "VLC Code VLC_Uploader"

        gv.Columns("VLC_Code").IsVisible = True
        gv.Columns("VLC_Code").Width = 100
        gv.Columns("VLC_Code").HeaderText = "VLC Code"

        gv.Columns("VLC_Name").IsVisible = True
        gv.Columns("VLC_Name").Width = 100
        gv.Columns("VLC_Name").HeaderText = "VLC Name"


        gv.Columns("VSPCode").IsVisible = True
        gv.Columns("VSPCode").Width = 100
        gv.Columns("VSPCode").HeaderText = "Vendor Code"

        gv.Columns("Vendor_Name").IsVisible = True
        gv.Columns("Vendor_Name").Width = 100
        gv.Columns("Vendor_Name").HeaderText = "Vendor Name"


        gv.Columns("mp_code").IsVisible = True
        gv.Columns("mp_code").Width = 100
        gv.Columns("mp_code").HeaderText = "MP Code"

        gv.Columns("MP_Name").IsVisible = True
        gv.Columns("MP_Name").Width = 100
        gv.Columns("MP_Name").HeaderText = "MP Name"


        gv.Columns("MCC_Qty").IsVisible = True
        gv.Columns("MCC_Qty").Width = 100
        gv.Columns("MCC_Qty").HeaderText = "MCC Qty"
        gv.Columns("MCC_Qty").FormatString = "{0:F3}"

        gv.Columns("MCC_TFAT").IsVisible = True
        gv.Columns("MCC_TFAT").Width = 100
        gv.Columns("MCC_TFAT").HeaderText = "MCC TFAT"
        gv.Columns("MCC_TFAT").FormatString = "{0:F3}"

        gv.Columns("MCC_TSNF").IsVisible = True
        gv.Columns("MCC_TSNF").Width = 100
        gv.Columns("MCC_TSNF").HeaderText = "MCC TSNF"
        gv.Columns("MCC_TSNF").FormatString = "{0:F3}"

        gv.Columns("MCC_Amount").IsVisible = True
        gv.Columns("MCC_Amount").Width = 100
        gv.Columns("MCC_Amount").HeaderText = "MCC Amount"
        gv.Columns("MCC_Amount").FormatString = "{0:F3}"


        gv.Columns("VLC_QTY").IsVisible = True
        gv.Columns("VLC_QTY").Width = 80
        gv.Columns("VLC_QTY").HeaderText = "VLC Qty"
        gv.Columns("VLC_QTY").FormatString = "{0:F3}"

        gv.Columns("VLC_TFAT").IsVisible = True
        gv.Columns("VLC_TFAT").Width = 80
        gv.Columns("VLC_TFAT").HeaderText = "VLC TFAT"
        gv.Columns("VLC_TFAT").FormatString = "{0:F3}"

        gv.Columns("VLC_TSNF").IsVisible = True
        gv.Columns("VLC_TSNF").Width = 50
        gv.Columns("VLC_TSNF").HeaderText = "VLC TSNF"
        gv.Columns("VLC_TSNF").FormatString = "{0:F3}"

        gv.Columns("VLC_Amount").IsVisible = True
        gv.Columns("VLC_Amount").Width = 100
        gv.Columns("VLC_Amount").HeaderText = "VLC Amount"
        gv.Columns("VLC_Amount").FormatString = "{0:F3}"


        gv.Columns("Diff_Qty").IsVisible = True
        gv.Columns("Diff_Qty").Width = 100
        gv.Columns("Diff_Qty").HeaderText = "Diff Quantity"
        gv.Columns("Diff_Qty").FormatString = "{0:F3}"

        gv.Columns("Diff_TFAT").IsVisible = True
        gv.Columns("Diff_TFAT").Width = 100
        gv.Columns("Diff_TFAT").HeaderText = "Diff TFAT"
        gv.Columns("Diff_TFAT").FormatString = "{0:F3}"

        gv.Columns("Diff_TSNF").IsVisible = True
        gv.Columns("Diff_TSNF").Width = 100
        gv.Columns("Diff_TSNF").HeaderText = "Diff TSNF"
        gv.Columns("Diff_TSNF").FormatString = "{0:F3}"

        gv.Columns("Diff_Amt").IsVisible = True
        gv.Columns("Diff_Amt").Width = 100
        gv.Columns("Diff_Amt").HeaderText = "Diff Amount"
        gv.Columns("Diff_Amt").FormatString = "{0:F3}"


        'gv.Columns("VLC_Code_VLC_Uploader").IsVisible = False
        'gv.Columns("VLC_Code_VLC_Uploader").Width = 100
        'gv.Columns("VLC_Code_VLC_Uploader").HeaderText = "VLC Code"


        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As New GridViewSummaryItem("VLC_QTY", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("VLC_TFAT", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("VLC_TSNF", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("VLC_Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("MCC_Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item6 As New GridViewSummaryItem("MCC_TFAT", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)
        Dim item7 As New GridViewSummaryItem("MCC_TSNF", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)
        Dim item8 As New GridViewSummaryItem("MCC_Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)
        Dim item9 As New GridViewSummaryItem("Diff_Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item9)
        Dim item10 As New GridViewSummaryItem("Diff_TFAT", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item10)
        Dim item11 As New GridViewSummaryItem("Diff_TSNF", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item11)
        Dim item12 As New GridViewSummaryItem("Diff_Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item12)
        'gv.GroupDescriptors.Add(New GridGroupByExpression("MCC_Code as Item format ""{0}: {1}"" Group By MCC_Code"))
        'gv.GroupDescriptors.Add(New GridGroupByExpression("Route_No as Item format ""{0}: {1}"" Group By Route_No"))
        'gv.GroupDescriptors.Add(New GridGroupByExpression("VLC_Code_VLC_Uploader as Item format ""{0}: {1}"" Group By VLC_Code_VLC_Uploader"))

        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub
    Sub View()
        If gv.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()

            view.ColumnGroups.Add(New GridViewColumnGroup(" "))
            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("VLC_Code_VLC_Uploader").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Route_name").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("VLC_Name").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("MCC_Qty").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("MCC_TFAT").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("MCC_TSNF").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("MCC_Amount").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("VLC_QTY").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("VLC_TFAT").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("VLC_TSNF").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("VLC_Amount").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Diff_Qty").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Diff_TFAT").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Diff_TSNF").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Diff_Amt").Name)

            'view.ColumnGroups.Add(New GridViewColumnGroup("VSP Milk"))
            'view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
            'view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("MCC_Qty"))
            'view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("MCC_TFAT"))
            'view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("MCC_TSNF"))
            'view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("MCC_Amount"))

            'view.ColumnGroups.Add(New GridViewColumnGroup("Producer Milk"))
            'view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
            'view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("VLC_QTY"))
            'view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("VLC_TFAT"))
            'view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("VLC_TSNF"))
            'view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("VLC_Amount"))

            'gv.ViewDefinition = view




            'view.ColumnGroups.Add(New GridViewColumnGroup("Difference"))
            'view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
            'view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv.Columns("Diff_Qty"))
            'view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv.Columns("Diff_TFAT"))
            'view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv.Columns("Diff_TSNF"))
            'view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv.Columns("Diff_Amt"))
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
        'RadGroupBox5.Enabled = val
        RadGroupBox2.Enabled = val
    End Sub

End Class
