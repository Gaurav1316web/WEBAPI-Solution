Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
'==================created by preeti gupta===Against Ticket No[]
Public Class FrmMCCVLCVarationReport
    Inherits FrmMainTranScreen
    Dim dt As DataTable
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim tmpValLoad As Boolean = True
    Dim arrLoc As String = Nothing
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
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptMCCVLCVarationReport)
        If Not (MyBase.isReadFlag) Then
            If MDI.blnShowAllMenu = False Then
                Throw New Exception("Permission Denied")
            Else
                Throw New Exception("Can't Access in demo version. " + Environment.NewLine + " For any queries/details, contact tecxpert@tecxpert.in. ")

            End If
        End If
        RadSplitButton1.Visible = MyBase.isExport
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
    Sub Reset()
        gv.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        EnableDisableControl(True)
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


    Private Sub EnableDisableControl(ByVal val As Boolean)
        RadGroupBox1.Enabled = val
        RadGroupBox2.Enabled = val
    End Sub
    Private Sub LoadData()
        If txtFromDate.Value > txtToDate.Value Then
            txtFromDate.Focus()
            Throw New Exception("From date can not be greater then to Date")
        End If

        If cbtMCCRouteVLCC.CheckedValue.Count = 0 Then

            clsCommon.MyMessageBoxShow(Me, "Please select atleast single MCC or select all.", Me.Text)
            Exit Sub
        End If

        Dim qry As String = "Select *, IsNull(final.[Qty(MCC)] - final.[Qty(VLC)], 0) As [Varation MCC/VLC Qty], IsNull(final.[FAT(Mcc)] - final.[FAT(VLC)], 0) As [Varation MCC/VLC FAT], IsNull(final.[SNF(Mcc)] - final.[SNF(VLC)], 0) As [Varation MCC/VLC SNF], IsNull(final.[FAT %(MCC)] - final.[FAT %(VLC)], 0) As [Varation MCC/VLC FAT %], IsNull(final.[SNF %(MCC)] - final.[SNF % (VLC)], 0) As [Varation MCC/VLC SNF %], IsNull(final.[Qty(MCC)] - final.[Qty(Transporter)], 0) As [Varation MCC/Transporter Qty], IsNull(final.[FAT(Mcc)] - final.[FAT(Transporter)], 0) As [Varation MCC/Transporter FAT], IsNull(final.[SNF(Mcc)] - final.[SNF(Transporter)], 0) As [Varation MCC/Transporter SNF], IsNull(final.[FAT %(MCC)] - final.[FAT %(Transporter)], 0) As [Varation MCC/Transporter FAT %], IsNull(final.[SNF %(MCC)] - final.[SNF %(Transporter)], 0) As [Varation MCC/Transporter SNF %] From (Select *    From (Select rh.DOC_CODE As [Doc Code], Convert(date,rh.DOC_DATE,103) As [Doc Date], Convert(Varchar,rh.DOC_DATE,103) As Date,rd.SHIFT, rh.MCC_CODE As [Mcc Code], TSPL_MCC_MASTER.MCC_NAME As [Mcc Name], rd.ROUTE_CODE As [Route Code], rm.Route_Name As [Route Name], rd.VLC_CODE As [Vlc Code], TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], TSPL_VLC_MASTER_HEAD.VLC_Name As [Vlc Name], rd.VSP_CODE As [VSP Code], TSPL_VENDOR_MASTER.Vendor_Name As [VSP Name], Sum(rd.MILK_WEIGHT) As [Qty(MCC)], Sum(sample.FAT_KG) As [FAT(Mcc)], Sum(sample.SNF_KG) As [SNF(Mcc)], Case When Sum(sample.Qty) = 0 Then 0 Else Convert(decimal(18,2),(Sum(sample.FAT_KG) * 100 / Sum(IsNull(sample.Qty, 0)))) End As [FAT %(MCC)], Case When Sum(sample.Qty) = 0 Then 0 Else Convert(decimal(18,2),(Sum(sample.SNF_KG) * 100 / Sum(sample.Qty))) End As [SNF %(MCC)], IsNull(Sum(truck_Sheet.Mcc_Qty), 0) As [Qty(Transporter)], IsNull(Convert(decimal(18,3),Sum(truck_Sheet.FatMCC)), 0) As [FAT(Transporter)], IsNull(Convert(decimal(18,3),Sum(truck_Sheet.SNFMCC)), 0) As [SNF(Transporter)], Case When Sum(truck_Sheet.Mcc_Qty) = 0 Then 0 Else Convert(decimal(18,2),(Sum(truck_Sheet.FatMCC) * 100 / IsNull(Sum(truck_Sheet.Mcc_Qty), 0))) End As [FAT %(Transporter)], Case When Sum(truck_Sheet.Mcc_Qty) = 0 Then 0 Else Convert(decimal(18,2),(Sum(truck_Sheet.SNFMCC) * 100 / IsNull(Sum(truck_Sheet.Mcc_Qty), 0))) End As [SNF %(Transporter)], Convert(decimal(18,3),IsNull(Max(Uploader.Qty), 0)) As [Qty(VLC)], IsNull(Max(Uploader.FAT), 0) As [FAT(VLC)], IsNull(Max(Uploader.SNF), 0) As [SNF(VLC)], Case When Max(Uploader.Qty) = 0 Then 0 Else Convert(decimal(18,2),(Max(Uploader.FAT) * 100 / Max(Uploader.Qty))) End As [FAT %(VLC)], Case When Max(Uploader.Qty) = 0 Then 0 Else Convert(decimal(18,2),(Max(Uploader.SNF) * 100 / Max(Uploader.Qty))) End As [SNF % (VLC)]      From TSPL_MILK_RECEIPT_HEAD rh Inner Join TSPL_MILK_RECEIPT_DETAIL rd On rh.DOC_CODE = rd.DOC_CODE Left Join (Select sh.MILK_RECEIPT_CODE, sd.SAMPLE_NO, sd.VLC_DOC_CODE, sd.FAT_KG, sd.SNF_KG, sd.Qty        From TSPL_MILK_SAMPLE_HEAD sh Inner Join TSPL_MILK_SAMPLE_DETAIL sd On sh.DOC_CODE = sd.DOC_CODE) sample On sample.MILK_RECEIPT_CODE = rh.DOC_CODE And sample.SAMPLE_NO = rd.SAMPLE_NO Left Join (Select Th.Milk_Receipt_Code, Td.SAMPLE_NO, Td.VLC_DOC_CODE, Sum(IsNull(Td.Mcc_Qty, 0)) As Mcc_Qty, Sum(Td.Mcc_FAT * Td.Mcc_Qty / 100) As FatMCC, Sum(Td.Mcc_SNF * Td.Mcc_Qty / 100) As SNFMCC        From Tspl_Milk_Truck_Sheet_Head Th Inner Join Tspl_Milk_Truck_Sheet_Detail Td On Th.DOC_CODE = Td.DOC_CODE        Group By Th.Milk_Receipt_Code, Td.SAMPLE_NO, Td.VLC_DOC_CODE) truck_Sheet On truck_Sheet.Milk_Receipt_Code = rh.DOC_CODE And truck_Sheet.SAMPLE_NO = rd.SAMPLE_NO Left Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = rd.VLC_CODE Left Join (Select TSPL_VLC_DATA_UPLOADER.MCC_Code, TSPL_VLC_DATA_UPLOADER.Doc_Date As File_Date, TSPL_VLC_DATA_UPLOADER.shift, TSPL_VLC_DATA_UPLOADER.VLC_CODE, Sum(IsNull(TSPL_VLC_DATA_UPLOADER.qty, 0)) As Qty, Sum(TSPL_VLC_DATA_UPLOADER.fat * TSPL_VLC_DATA_UPLOADER.qty / 100) As FAT, Sum(TSPL_VLC_DATA_UPLOADER.snf * TSPL_VLC_DATA_UPLOADER.qty / 100) As SNF        From TSPL_VLC_DATA_UPLOADER        Group By TSPL_VLC_DATA_UPLOADER.MCC_Code, TSPL_VLC_DATA_UPLOADER.Doc_Date, TSPL_VLC_DATA_UPLOADER.shift, TSPL_VLC_DATA_UPLOADER.VLC_CODE) Uploader On Uploader.MCC_Code = rh.MCC_CODE And Convert(date,Uploader.File_Date,103) = Convert(date,rh.DOC_DATE,103) And Uploader.shift = rd.SHIFT And Uploader.VLC_CODE = TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader Left Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = rh.MCC_CODE Left Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = rd.VSP_CODE Left Join TSPL_MCC_ROUTE_MASTER rm On rm.Route_Code = rd.ROUTE_CODE      Group By rh.DOC_CODE, Convert(date,rh.DOC_DATE,103), Convert(Varchar,rh.DOC_DATE,103), rh.MCC_CODE, TSPL_MCC_MASTER.MCC_NAME, rd.ROUTE_CODE, rm.Route_Name, rd.VLC_CODE, TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader, TSPL_VLC_MASTER_HEAD.VLC_Name, rd.VSP_CODE, TSPL_VENDOR_MASTER.Vendor_Name,rd.SHIFT) Doc) As final"
        qry += " where 2=2 "
        qry += " and convert(date,final.[Doc Date],103 )>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,final.[Doc Date],103) <=convert(date,'" + txtToDate.Value + "',103)"
        If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal Then
            qry += " and 2=( case when  convert(date,final.[Doc Date],103 ) >= convert(date,'" + txtFromDate.Value + "',103) and  convert(date,final.[Doc Date],103) <= convert(date,'" + txtFromDate.Value + "',103) and final.shift='M' then 3 else 2 end  )"
        End If
        If clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
            qry += " and 2=( case when  convert(date,final.[Doc Date],103 ) >= convert(date,'" + txtToDate.Value + "',103) and convert(date,final.[Date],103 ) <= convert(date,'" + txtToDate.Value + "',103) and final.shift='E' then 3 else 2 end  )"
        End If

        'If rbtnMCCRouteVLCCSelect.IsChecked Then
        Dim arr As List(Of String) = Nothing
        If cbtMCCRouteVLCC.CheckedValue.Count > 0 Then
            arr = cbtMCCRouteVLCC.CheckedValue(1)
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                qry += "and final.[Mcc code]  IN (" + clsCommon.GetMulcallString(arr) + ") "
            Else
                Throw New Exception("Please select at least one MCC")
            End If
        End If
        If cbtMCCRouteVLCC.CheckedValue.Count > 1 Then
            arr = cbtMCCRouteVLCC.CheckedValue(2)
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                qry += " and final.[Route Code] in (" + clsCommon.GetMulcallString(arr) + ")  "
            Else
                Throw New Exception("Please select at least one Route")
            End If
        End If
        If cbtMCCRouteVLCC.CheckedValue.Count > 1 Then
            arr = cbtMCCRouteVLCC.CheckedValue(3)
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                qry += " and final.[Vlc Code] in (" + clsCommon.GetMulcallString(arr) + ")  "
            Else
                Throw New Exception("Please select at least one VLC")
            End If
        End If

        'End If

        qry += "order by convert(date,final.[Doc Date],103)  "


        dt = clsDBFuncationality.GetDataTable(qry)
        gv.DataSource = Nothing
        gv.Rows.Clear()
        gv.Columns.Clear()
        gv.DataSource = dt
        gv.GroupDescriptors.Clear()
        gv.MasterTemplate.SummaryRowsBottom.Clear()
        'FormatGrid()
        gv.BestFitColumns()
        gv.Columns("Doc Date").IsVisible = False
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            Exit Sub
        End If

        RadPageView1.SelectedPage = RadPageViewPage2
        ReStoreGridLayout()
    End Sub

    Private Sub FrmMCCVLCVarationReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LOCATIONRIGTHS()
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        RadPageView1.SelectedPage = RadPageViewPage1
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        LoadMCCRouteVLCTree()
        LoadShiftFrom()
        LoadShiftTo()
        Reset()
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim CompName As String = clsDBFuncationality.getSingleValue("Select Comp_Name from TSPL_COMPANY_MASTER Where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'")
            arrHeader.Add(CompName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptMCCVLCVarationReport & "'"))
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
            'End If

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
                transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
                'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)
            Else
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                clsCommon.MyExportToPDF(Me.Text, gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub FrmMCCVLCVarationReport_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            LoadData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()

        End If
    End Sub

    Private Sub btnSaveLayout_Click(sender As Object, e As EventArgs) Handles btnSaveLayout.Click
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

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        LoadData()
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub


    Private Sub btnExportToExcel_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        print(EnumExportTo.PDF)
    End Sub
End Class
