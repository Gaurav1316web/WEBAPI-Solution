Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
'=========added tree and shift by shivani==========='

'created by preeti gupta ticket no[BM000000042194,TEC/03/05/19-000471] 24/9
''changes done by richa agarwal BM00000008811
Public Class RptDailyDifferentRow_vb

    Inherits FrmMainTranScreen
    Dim btnReferesh As Boolean = False
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
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptDailyDifferentReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnPrint.Visible = MyBase.isPrintFlag

    End Sub
    Private Sub rmSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmSaveLayout.Click
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
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
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

    Private Sub rmDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
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
                'Ticket No-TEC/13/03/19-000451 In case of PDF Export,Remove VLC list beacuse it's take more than half page and consume time
                If exporter = EnumExportTo.Excel Then
                    If cbtMCCRouteVLCC.CheckedText.Count > 2 Then
                        arr = cbtMCCRouteVLCC.CheckedText(3)
                        If arr IsNot Nothing AndAlso arr.Count > 0 Then
                            arrHeader.Add(("VLC : " + clsCommon.GetMulcallStringWithComma(arr) + " "))
                        End If
                    End If
                End If
                'End If
                If chkStateSelect.IsChecked Then
                    Dim strStateName As String = ""
                    For Each StrName As String In cbgState.CheckedDisplayMember
                        If clsCommon.myLen(strStateName) > 0 Then
                            strStateName += ", "
                        End If
                        strStateName += StrName
                    Next
                    Dim strStateCode As String = ""
                    For Each StrCode As String In cbgState.CheckedValue
                        If clsCommon.myLen(strStateCode) > 0 Then
                            strStateCode += ", "
                        End If
                        strStateCode += StrCode
                    Next
                    arrHeader.Add(("State Name: " + strStateName + " "))
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
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    transportSql.exportdata(gv, "", Me.Text, , arrHeader, False, False, True) 'frm.Text)
                    'transportSql.exportdata(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader, False, False, True) 'frm.Text)
                    'clsCommon.MyMessageBoxShow("Data Exported Successfully.")
                    'Process.Start(filePath)
                Else
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("Daily Different Report", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Private Sub rmExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmExcel.Click
        print(EnumExportTo.Excel)
    End Sub
    Public Sub Load_Report()
        Try
            '==========changes by shivani against ticket no [BM00000008870][Serial no must be in increasing order. date format must be dd/mm/yyyy.]
            Dim sQuery As String
            Dim companyADD, CompName, CompCode As String
            If txtFromDate.Value > txtToDate.Value Then
                common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater then to Date", Me.Text)
                txtFromDate.Focus()
                Exit Sub
            End If
            If cbtMCCRouteVLCC.CheckedValue.Count = 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select atleast single MCC or select all.", Me.Text)
                Exit Sub
            End If
            'If chkMCCSelect.IsChecked AndAlso cbgMCC.CheckedValue.Count = 0 Then
            '    clsCommon.MyMessageBoxShow("Please select atleast single MCC or select all.")
            '    Exit Sub
            'End If
            'If chkVLCSelect.IsChecked AndAlso cbgVLC.CheckedValue.Count = 0 Then
            '    clsCommon.MyMessageBoxShow("Please select atleast single VLC or select all.")
            '    Exit Sub
            'End If
            If chkStateSelect.IsChecked AndAlso cbgState.CheckedValue.Count = 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select atleast single State or select all.", Me.Text)
                Exit Sub
            End If

            Dim whrcls As String = " where 2=2 "


            sQuery = ""
            sQuery += " select   TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_COMPANY_MASTER.City_Code)>0 then ', '+TSPL_COMPANY_MASTER.City_Code else ' ' end + case when len(TSPL_COMPANY_MASTER.State )>0 then TSPL_COMPANY_MASTER.State else '' end  as comp_address from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(sQuery)
            companyADD = dt1.Rows(0).Item("comp_address")

            sQuery = ""
            sQuery += " select   TSPL_COMPANY_MASTER.Comp_Name  from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(sQuery)
            CompName = dt2.Rows(0).Item("Comp_Name")


            sQuery = ""
            sQuery += " select   TSPL_COMPANY_MASTER.comp_code  from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
            Dim dt5 As DataTable = clsDBFuncationality.GetDataTable(sQuery)
            CompCode = dt5.Rows(0).Item("Comp_Code")
            Dim fromDate As String = txtFromDate.Value

            Dim Todate As String = txtToDate.Value
            sQuery = ""

            ''richa agarwal TEC/28/03/19-000462 add item master table to pick item structure code
            sQuery += "  WITH TEMP AS" & Environment.NewLine &
            " ( select ROW_NUMBER () over (  order by [Mcc Code] desc,VLC_Code desc,convert(date,DOC_DATE,103) asc )  as FSLNO, * from 
            (select ROW_NUMBER () over ( partition by [Mcc Code],VLC_Code order by [Mcc Code],VLC_Code,convert(date,DOC_DATE,103) )  as SLNO,* from" & Environment.NewLine &
            " (select max( [MCC Code]) as [MCC Code] ,max([Mcc Name]) as [MCC Name] ,max(VLC_Code) as VLC_Code ,max(VLC_Name) as VLC_Name,max(VLC_Uploader_Code) as VLC_Uploader_Code ,convert(varchar,DOC_DATE,103) as DOC_DATE,convert(decimal(18,2),SUM(isnull(NewQty,0) )) as QTY,isnull(convert(DECIMAL(18,1),sum(FATQTY)/nullif(sum(NewQty),0)*100),0) as FAT_PER, isnull(convert(DECIMAL(18,1),sum(SNFQTY )/nullif(sum(NewQty),0)*100),0) as SNF_PER from" & Environment.NewLine &
            " (  select [MCC Code] as [MCC Code],[Mcc Name],VLC_Code ,VLC_Name ,VLC_Uploader_Code , convert(date,DOC_DATE,103) as DOC_DATE,UOM_Code,convert(DECIMAL(18,2),FATQTY*CF) as FATQTY,convert(DECIMAL(18,2),SNFQTY*CF) as SNFQTY,Qty*CF as NewQty, Qty,FromUOM,TOUOM,CF 
            from( 
            select TSPL_MCC_MASTER.MCC_Code as [MCC Code] ,TSPL_MCC_MASTER.MCC_NAME as [Mcc Name],TSPL_VLC_MASTER_HEAD.VLC_Code  , TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VLC_Uploader_Code,TSPL_VLC_MASTER_HEAD.VLC_Name, TSPL_MILK_SRN_HEAD.DOC_DATE,TSPL_MILK_SRN_detail.UOM_Code ,TSPL_MILK_SRN_detail.FAT_Per,(TSPL_MILK_SRN_detail.FAT_per*TSPL_MILK_SRN_detail.Qty/100) as FATQTY ,TSPL_MILK_SRN_detail.SNF_per,(TSPL_MILK_SRN_detail.SNF_PER*TSPL_MILK_SRN_detail.Qty /100) as SNFQTY ,TSPL_MILK_SRN_detail.Qty,TSPL_ITEM_MASTER.Structure_Code  from TSPL_MILK_SRN_detail 
             left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_SRN_detail.DOC_CODE
            left outer join TSPL_MCC_MASTER  on TSPL_MCC_MASTER .MCC_Code =TSPL_MILK_SRN_HEAD .MCC_CODE  
            left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.State_Code =TSPL_MCC_MASTER .State_Code 
            left outer join TSPL_CITY_MASTER  on TSPL_CITY_MASTER.City_Code =TSPL_MCC_MASTER.City_code 
            left outer join TSPL_VLC_MASTER_HEAD  on TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_SRN_HEAD .VLC_Code
            left join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER. Route_No =TSPL_MILK_SRN_HEAD.ROUTE_CODE" & Environment.NewLine &
            " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_MILK_SRN_detail.Item_Code " & Environment.NewLine &
            " where 2 = 2 " & Environment.NewLine &
            " and convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103)>=convert(date,('" + txtFromDate.Value + "'),103) and convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) <=convert(date,('" + txtToDate.Value + "'),103) " & Environment.NewLine

            'If rbtnMCCRouteVLCCSelect.IsChecked Then
            Dim arr As List(Of String) = Nothing
            If cbtMCCRouteVLCC.CheckedValue.Count > 0 Then
                arr = cbtMCCRouteVLCC.CheckedValue(1)
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    sQuery += "and TSPL_MCC_MASTER.MCC_Code  IN (" + clsCommon.GetMulcallString(arr) + ") "
                Else
                    Throw New Exception("Please select at least one MCC")
                End If
            End If
            If cbtMCCRouteVLCC.CheckedValue.Count > 1 Then
                arr = cbtMCCRouteVLCC.CheckedValue(2)
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    sQuery += " and TSPL_MILK_SRN_HEAD.ROUTE_CODE in (" + clsCommon.GetMulcallString(arr) + ")  "
                Else
                    Throw New Exception("Please select at least one Route")
                End If
            End If
            If cbtMCCRouteVLCC.CheckedValue.Count > 2 Then
                arr = cbtMCCRouteVLCC.CheckedValue(3)
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    sQuery += " and TSPL_MILK_SRN_HEAD.VLC_CODE in (" + clsCommon.GetMulcallString(arr) + ")  "
                Else
                    Throw New Exception("Please select at least one Route")
                End If
            End If
            'End If

            'If chkMCCSelect.IsChecked And cbgMCC.CheckedValue.Count > 0 Then
            '    sQuery += " and TSPL_MCC_MASTER.MCC_Code  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
            'End If
            'If chkVLCSelect.IsChecked And cbgVLC.CheckedValue.Count > 0 Then
            '    sQuery += " and TSPL_MILK_RECEIPT_DETAIL.VLC_Code in (" + clsCommon.GetMulcallString(cbgVLC.CheckedValue) + ")  "
            'End If
            ' Ticket No : BHA/21/11/18-000686 By Prabhakar - for Devided by Zero error
            If chkStateSelect.IsChecked And cbgState.CheckedValue.Count > 0 Then
                sQuery += " and TSPL_STATE_MASTER.STATE_CODE in (" + clsCommon.GetMulcallString(cbgState.CheckedValue) + ")  "
            End If
            If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal Then
                sQuery += " and 2=( case when TSPL_MILK_SRN_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_SRN_HEAD.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_SRN_HEAD.SHIFT='M' then 3 else 2 end  )"
            End If
            If clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
                sQuery += " and 2=( case when TSPL_MILK_SRN_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_SRN_HEAD.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_SRN_HEAD.SHIFT='E' then 3 else 2 end  )"
            End If
            ''richa agarwal TEC/28/03/19-000462 add item structure on setting based
            If ItemStructureMandatoryOnWeightConversion Then
                sQuery += " ) xx " & Environment.NewLine &
                " left outer join (Select Distinct yyy.* From ( " & Environment.NewLine &
                " Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF,Structure_Code from TSPL_WEIGHT_CONVERSION where Contained_UOM='KG'" & Environment.NewLine &
                " UNION All " & Environment.NewLine &
                " Select Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/ nullif (Contained_Qty,0) as CF,Structure_code from TSPL_WEIGHT_CONVERSION where Container_UOM='LTR' " & Environment.NewLine &
                " UNION All " & Environment.NewLine &
                " Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF,Structure_Code  from TSPL_WEIGHT_CONVERSION " & Environment.NewLine &
                " UNION All" & Environment.NewLine &
                " Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF,Structure_Code from TSPL_WEIGHT_CONVERSION " & Environment.NewLine &
                ") yyy ) zzz on zzz.FromUOM =UOM_Code  and zzz.TOUOM='" + cboUnit.Text + "' where zzz.Structure_Code =xx.Structure_code ) ttt group by [MCC Code],VLC_Code,DOC_DATE )xxx   )yyy)  "

            Else
                sQuery += " ) xx " & Environment.NewLine &
           " left outer join (Select Distinct yyy.* From ( " & Environment.NewLine &
           " Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF from TSPL_WEIGHT_CONVERSION where Contained_UOM='KG' " & Environment.NewLine &
           " UNION All " & Environment.NewLine &
           " Select Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/ nullif (Contained_Qty,0) as CF from TSPL_WEIGHT_CONVERSION where Container_UOM='LTR' " & Environment.NewLine &
           " UNION All " & Environment.NewLine &
           " Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF  from TSPL_WEIGHT_CONVERSION " & Environment.NewLine &
           " UNION All" & Environment.NewLine &
           " Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION " & Environment.NewLine &
           ") yyy ) zzz on zzz.FromUOM =UOM_Code  and zzz.TOUOM='" + cboUnit.Text + "' ) ttt group by [MCC Code],VLC_Code,DOC_DATE )xxx   )yyy)  "

            End If
           

            'Dim strQuery1 As String = " SELECT " & _
            '                          "   [current].fromDate,[current].Todate,[current].companyADD,[current].CompName,[current].CompCode,[current].compLogo1,[current].compLogo2,[current].SLNO,[current].DOC_DATE,[current].[MCC Code],[current].[MCC Name] ,[current].VLC_Code as [VlC Code] ,[current].VLC_Name as [VLC Name] ,[current] .VLC_Uploader_Code as [VLC Uploader Code] ,convert(decimal(18,2),[current].QTY) as QTY ,convert(decimal(18,1),[current].[SNF_PER]) as SNF_PER,convert(decimal(18,1),[current] .[FAT_PER]) as FAT_PER  , convert(decimal(18,2),case when [current].SLNO=1 then 0 else  [current].QTY-  ISNULL([next].QTY , 0)   end) as Diff_Qty,convert(decimal(18,1),case when [current].SLNO=1 then 0 else  [current].[FAT_PER] -  ISNULL([next].FAT_PER  , 0)   end )as Diff_Fat_Per,convert(decimal(18,1),case when [current].SLNO=1 then 0 else  [current].[SNF_PER]  -  ISNULL([next].SNF_PER   , 0)   end) as Diff_SNF_Per" & _
            '                          " FROM "
            'Dim strQuery2 As String = " AS [current] LEFT JOIN "
            'Dim strQuery3 As String = " AS [next] ON [next].SLNO  = (SELECT max(SLNO ) FROM "
            'Dim strQuery4 As String = " xxxxx  WHERE SLNO < [current].SLNO  ) "
            'Dim strQueryfinal As String = strQuery1 & sQuery & strQuery2 & sQuery & strQuery3 & sQuery & strQuery4
            'strQueryfinal += "  order by DOC_DATE ,[MCC Name],[VlC Code]"

            sQuery += " SELECT TableA.*,case when TableA.slno=1 then 0 else (TableA.qty - TableB.qty) end AS Diff_Qty," & Environment.NewLine & _
            " case when TableA.slno=1 then 0 else (TableA.FAT_PER - TableB.FAT_PER) end AS Diff_Fat_Per," & Environment.NewLine & _
            " case when TableA.slno=1 then 0 else (TableA.SNF_PER - TableB.SNF_PER) end AS Diff_SNF_Per" & Environment.NewLine & _
            " FROM " & Environment.NewLine & _
            " temp TableA" & Environment.NewLine & _
            " Left Join" & Environment.NewLine & _
            " temp TableB" & Environment.NewLine & _
            " ON" & Environment.NewLine & _
            " TableB.slno = TableA.slno - 1 and tablea.[Mcc code]=tableb.[MCC Code] and tablea.VLC_code=tableb.VLC_code" & Environment.NewLine & _
            " order by FSLNO,TableA.[MCC Code],tablea.VLC_code,convert(date,tableA.Doc_date,103)" & Environment.NewLine

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
                    frmCRV.funreport(CrystalReportFolder.MilkProcurement, dtgv, "crptDailyDifferentReport", "Daily Different Report")
                    frmCRV = Nothing
                End If
                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If
            ReStoreGridLayout()
            View()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        
    End Sub
    Sub FormatGrid()
        

        gv.MasterTemplate.ShowRowHeaderColumn = False
        'gv.ViewDefinition = view
        For ii As Integer = 0 To gv.Columns.Count - 1
            '  gv.Columns(ii).ReadOnly = True
            'gv.Columns(ii).IsVisible = False
            'If chkcatewise.Checked AndAlso ii > 18 Then
            '    gv.Columns(ii).IsVisible = True
            '    gv.Columns(ii).Width = 100
            'End If
        Next

        gv.Columns("SLNO").IsVisible = False
        gv.Columns("SLNO").Width = 100
        gv.Columns("SLNO").HeaderText = "S No."

        gv.Columns("DOC_DATE").IsVisible = True
        gv.Columns("DOC_DATE").Width = 100
        gv.Columns("DOC_DATE").HeaderText = "Date"
        gv.Columns("DOC_DATE").FormatString = "{0:d}"

        gv.Columns("QTY").IsVisible = True
        gv.Columns("QTY").Width = 100
        gv.Columns("QTY").HeaderText = "QTY"

        gv.Columns("MCC Name").IsVisible = True
        gv.Columns("MCC Name").Width = 100
        gv.Columns("MCC Name").HeaderText = "MCC Name"


        gv.Columns("VLC_Uploader_Code").IsVisible = True
        gv.Columns("VLC_Uploader_Code").Width = 100
        gv.Columns("VLC_Uploader_Code").HeaderText = "VLC Uploader Code"

        gv.Columns("VLC_Name").IsVisible = True
        gv.Columns("VLC_Name").Width = 100
        gv.Columns("VLC_Name").HeaderText = "VLC Name"

        gv.Columns("SNF_PER").IsVisible = True
        gv.Columns("SNF_PER").Width = 100
        gv.Columns("SNF_PER").HeaderText = "SNF %"


        gv.Columns("FAT_PER").IsVisible = True
        gv.Columns("FAT_PER").Width = 100
        gv.Columns("FAT_PER").HeaderText = "FAT %"


        gv.Columns("Diff_Qty").IsVisible = True
        gv.Columns("Diff_Qty").Width = 100
        gv.Columns("Diff_Qty").HeaderText = "Qty"


        gv.Columns("Diff_SNF_Per").IsVisible = True
        gv.Columns("Diff_SNF_Per").Width = 100
        gv.Columns("Diff_SNF_Per").HeaderText = " SNF %"

        gv.Columns("Diff_Fat_Per").IsVisible = True
        gv.Columns("Diff_Fat_Per").Width = 100
        gv.Columns("Diff_Fat_Per").HeaderText = "FAT %"

        gv.Columns("Diff_Fat_Per").IsVisible = True
        gv.Columns("Diff_Fat_Per").Width = 100
        gv.Columns("Diff_Fat_Per").HeaderText = "FAT %"

        
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        'Dim item1 As New GridViewSummaryItem("QTY", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item1)
        'Dim item2 As New GridViewSummaryItem("Diff_Qty", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item2)
        ''Dim item3 As New GridViewSummaryItem("SNF", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item3)
        'Dim item4 As New GridViewSummaryItem("TFat", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item4)
        'Dim item5 As New GridViewSummaryItem("TSNF", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item5)
        'Dim item6 As New GridViewSummaryItem("Rate", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item6)
        'Dim item7 As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item7)
        'gv.GroupDescriptors.Add(New GridGroupByExpression("MCC_CODE as Item format ""{0}: {1}"" Group By MCC_CODE"))
        'gv.GroupDescriptors.Add(New GridGroupByExpression("VSP_Code as Item format ""{0}: {1}"" Group By VSP_Code"))
        'gv.GroupDescriptors.Add(New GridGroupByExpression("ROUTE_CODE as Item format ""{0}: {1}"" Group By ROUTE_CODE"))

      

        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        gv.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom

    End Sub
    Sub View()
        If gv.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()


            view.ColumnGroups.Add(New GridViewColumnGroup("MCC Receipt"))

            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("FSLNO").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("SLNO").Name)

            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("DOC_DATE").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("MCC Code").Name)

            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("MCC Name").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("VLC_Code").Name)

            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("VLC_Name").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("VLC_Uploader_Code").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("QTY").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("SNF_PER").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("FAT_PER").Name)
            'gv.ViewDefinition = view

            view.ColumnGroups.Add(New GridViewColumnGroup("Variation from last day"))
            view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("Diff_Qty").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("Diff_SNF_Per").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("Diff_Fat_Per").Name)
            gv.ViewDefinition = view
        End If

    End Sub
    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        btnReferesh = True
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        Load_Report()
    End Sub
    Sub Reset()
        LOCATIONRIGTHS()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)

        cboUnit.Text = "Kg"
        LoadMCCRouteVLCTree()
        LoadShiftFrom()
        LoadShiftTo()
        'rbtnMCCRouteVLCCAll.IsChecked = True

        chkStateAll.CheckState = CheckState.Checked


        gv.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub BtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub


    Private Sub chkStateAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkStateAll.ToggleStateChanged
        cbgState.Enabled = Not chkStateAll.IsChecked
    End Sub

    Private Sub RptDailyDifferentRow_vb_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ItemStructureMandatoryOnWeightConversion = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ItemStructureMandatoryOnWeightConversion, clsFixedParameterCode.ItemStructureMandatoryOnWeightConversion, Nothing)) = 1, True, False))
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New ")
        RadPageView1.SelectedPage = RadPageViewPage1
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        
        LoadState()
        Reset()
    End Sub
    
   
    Sub LoadState()
        Dim qry As String = "select STATE_CODE as Code ,STATE_NAME  as Name from TSPL_STATE_MASTER   "
        cbgState.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgState.ValueMember = "Code"
        cbgState.DisplayMember = "Name"

    End Sub

    Private Sub RptDailyDifferentRow_vb_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Load_Report()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()
        End If
    End Sub

   
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        btnReferesh = False
        Load_Report()
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

    Sub LoadMCCRouteVLCTree()
        'Dim qry As String = "select TSPL_VLC_MASTER_HEAD.VLC_Code as Code,TSPL_VLC_MASTER_HEAD.VLC_Name as Name,TSPL_VLC_MASTER_HEAD.Route_Code as ParentCode,3 as Lvl from TSPL_VLC_MASTER_HEAD where len(isnull(TSPL_VLC_MASTER_HEAD.Route_Code,''))>0 union all   select TSPL_MCC_ROUTE_MASTER.Route_Code as Code,TSPL_MCC_ROUTE_MASTER.Route_Name as Name,TSPL_MCC_ROUTE_MASTER.MCC_Code as ParentCode,2 as Lvl from TSPL_MCC_ROUTE_MASTER where len(isnull(TSPL_MCC_ROUTE_MASTER.MCC_Code,''))>0  union all   select TSPL_MCC_MASTER.MCC_Code as Code,TSPL_MCC_MASTER.MCC_NAME as Name,null as ParentCode,1 as Lvl from TSPL_MCC_MASTER"
        'cbtMCCRouteVLCC.ValueMember = "Code"
        'cbtMCCRouteVLCC.DisplayMember = "Name"
        'cbtMCCRouteVLCC.ParentValue = "ParentCode"
        'cbtMCCRouteVLCC.DataSource = clsDBFuncationality.GetDataTable(qry)

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
    Private Sub rbtnMCCRouteVLCCAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        'cbtMCCRouteVLCC.Enabled = rbtnMCCRouteVLCCSelect.IsChecked

    End Sub

    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        print(EnumExportTo.PDF)
    End Sub
End Class
