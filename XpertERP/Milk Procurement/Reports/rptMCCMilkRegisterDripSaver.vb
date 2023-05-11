Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
'==================created by preeti gupta===Against Ticket No[BM00000007934]
Public Class RptMCCMilkRegisterDripSaver
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
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptMCCMilkRegisterDripSaver)
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

            clsCommon.MyMessageBoxShow("Please select atleast single MCC or select all.")
            Exit Sub
        End If

        Dim qry As String = "select [Date],[Milk Receipt Code],MCC as [MCC Code],[MCC Name] ,[Doc Date] ,Shift,[Route Code] ,[Route Name] ,[Vehicle Code] ,[VSP Code],[VSP Name] ,[Vlc Uploader Code],[Vlc Code],[VLC Name] ,[Sample No] ,[No Of Cans],[Milk Weight],[Milk Weight(KG)],[Milk Weight(LTR)],[FAT(%)],[SNF(%)] ,[FAT(KG)],[SNF(KG)],[Cow Milk Qty (KG)],[Cow FAT(%)],[Cow SNF(%)],[Cow FAT (KG)],[Cow SNF (KG)],[Buffalo Milk Qty (KG)],[Buffalo FAT(%)],[Buffalo SNF(%)],[Buffalo FAT (KG)] ,[Buffalo SNF (KG)] ,[Milk Type],[SRN No],[SRN Qty],[SRN Rate] ,[SRN Amount] ,[Shift Status]  from (Select final.*, Case When final.[FAT(%)] <= 5 Then final.[FAT(KG)] Else 0    End [Cow FAT (KG)], Case When final.[FAT(%)] <= 5 Then final.[SNF(KG)] Else 0    End [Cow SNF (KG)], Case When final.[FAT(%)] > 5 Then final.[FAT(KG)] Else 0    End [Buffalo FAT (KG)], Case When final.[FAT(%)] > 5 Then final.[SNF(KG)]      Else 0 End [Buffalo SNF (KG)] From (Select Case        When TSPL_MILK_SAMPLE_DETAIL.FAT < 5 Then TSPL_MILK_SAMPLE_DETAIL.FAT        Else 0 End [Cow FAT(%)], Case        When TSPL_MILK_SAMPLE_DETAIL.FAT < 5 Then TSPL_MILK_SAMPLE_DETAIL.SNF        Else 0 End [Cow SNF(%)], Case        When TSPL_MILK_SAMPLE_DETAIL.FAT > 5 Then TSPL_MILK_SAMPLE_DETAIL.FAT        Else 0 End [Buffalo FAT(%)], Case        When TSPL_MILK_SAMPLE_DETAIL.FAT > 5 Then TSPL_MILK_SAMPLE_DETAIL.SNF        Else 0 End [Buffalo SNF(%)], Case        When TSPL_MILK_SAMPLE_DETAIL.FAT <=        5 Then TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT Else 0      End [Cow Milk Qty (KG)], Case        When TSPL_MILK_SAMPLE_DETAIL.FAT >        5 Then TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT Else 0      End [Buffalo Milk Qty (KG)], Case        When Coalesce(TSPL_MILK_SAMPLE_DETAIL.FAT, 0) <= 0 Then ''        When Coalesce(TSPL_MILK_SAMPLE_DETAIL.FAT, 0) <= 5 Then 'C' Else 'B'      End As [Milk Type], TSPL_MILK_RECEIPT_HEAD.DOC_CODE As [Milk Receipt Code],      TSPL_MILK_RECEIPT_HEAD.MCC_CODE As MCC, TSPL_MCC_MASTER.MCC_NAME As      [MCC Name], Convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) As Date,      Convert(varchar,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) As [Doc Date], Case        When TSPL_MILK_RECEIPT_DETAIL.SHIFT = 'M' Then 'Morning' Else 'Evening'      End As Shift, TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE As [Route Code],      TSPL_MCC_ROUTE_MASTER.Route_Name As [Route Name],      TSPL_MILK_RECEIPT_DETAIL.VEHICLE_CODE As [Vehicle Code],      TSPL_MILK_RECEIPT_DETAIL.VSP_CODE As [VSP Code],      TSPL_VENDOR_MASTER.Vendor_Name As [VSP Name], TSPL_VLC_MASTER_HEAD.VLC_Code      As [Vlc Code], TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As      [Vlc Uploader Code], TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name],      TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO As [Sample No],      TSPL_MILK_RECEIPT_DETAIL.NO_OF_CANS As [No Of Cans],      TSPL_MILK_RECEIPT_DETAIL.MILK_WEIGHT As [Milk Weight],      TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT As [Milk Weight(KG)],      TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT_LTR As [Milk Weight(LTR)],      TSPL_MILK_SAMPLE_DETAIL.FAT As [FAT(%)], TSPL_MILK_SAMPLE_DETAIL.SNF As      [SNF(%)], Convert(decimal(18,2),TSPL_MILK_SAMPLE_DETAIL.FAT *      TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT / 100) As [FAT(KG)],      Convert(decimal(18,2),TSPL_MILK_SAMPLE_DETAIL.SNF *      TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT / 100) As [SNF(KG)],      TSPL_MILK_SRN_HEAD.DOC_CODE As [SRN No],     Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.AMOUNT) As [SRN Amount],      TSPL_MILK_SRN_DETAIL.RATE As [SRN Rate], TSPL_MILK_SRN_DETAIL.Qty As      [SRN Qty], Case When TSPL_MILK_Shift_End_HEAD.DOC_CODE Is Null Then 'Open'        Else 'Close' End [Shift Status]    From TSPL_MILK_RECEIPT_DETAIL Left Outer Join TSPL_MILK_RECEIPT_HEAD        On TSPL_MILK_RECEIPT_HEAD.DOC_CODE = TSPL_MILK_RECEIPT_DETAIL.DOC_CODE      Left Outer Join TSPL_MILK_SAMPLE_HEAD        On TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE =        TSPL_MILK_RECEIPT_HEAD.DOC_CODE Left Outer Join TSPL_MILK_SAMPLE_DETAIL        On TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO = TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO        And TSPL_MILK_SAMPLE_DETAIL.DOC_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE      Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE =        TSPL_MILK_SAMPLE_HEAD.DOC_CODE And TSPL_MILK_SRN_HEAD.SAMPLE_NO =        TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO Left Outer Join TSPL_MILK_SRN_DETAIL        On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE      Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL        On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE =        TSPL_MILK_SRN_HEAD.DOC_CODE Left Outer Join      TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE        = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE Left Outer Join      TSPL_MCC_MASTER        On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_RECEIPT_HEAD.MCC_CODE      Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code =        TSPL_MILK_RECEIPT_DETAIL.VLC_CODE Left Outer Join TSPL_VENDOR_MASTER        On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_RECEIPT_DETAIL.VSP_CODE      Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code =        TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE Left Outer Join      TSPL_Primary_Vehicle_Master On TSPL_Primary_Vehicle_Master.Vehicle_Code =        TSPL_MCC_ROUTE_MASTER.Vehicle_Code Left Outer Join      TSPL_MILK_Shift_End_HEAD On TSPL_MILK_Shift_End_HEAD.MCC_CODE =        TSPL_MILK_RECEIPT_HEAD.MCC_CODE And TSPL_MILK_Shift_End_HEAD.DOC_DATE =        TSPL_MILK_RECEIPT_HEAD.DOC_DATE And TSPL_MILK_Shift_End_HEAD.SHIFT =        TSPL_MILK_RECEIPT_HEAD.SHIFT Left Outer Join      TSPL_MILK_Shift_End_Route_DETAIL        On TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE =        TSPL_MILK_Shift_End_HEAD.DOC_CODE And TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE = TSPL_MCC_ROUTE_MASTER.Route_Code where (coalesce(is_drip_saver,'')='Y')) As final ) as pp"
        qry += " where 2=2 "
        qry += " and convert(date,pp.[Date],103 )>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,pp.[Date],103) <=convert(date,'" + txtToDate.Value + "',103)"
        If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal Then
            qry += " and 2=( case when  convert(date,pp.[Date],103 ) >= convert(date,'" + txtFromDate.Value + "',103) and  convert(date,pp.[Date],103) <= convert(date,'" + txtFromDate.Value + "',103) and pp.shift='Morning' then 3 else 2 end  )"
        End If
        If clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
            qry += " and 2=( case when  convert(date,pp.[Date],103 ) >= convert(date,'" + txtToDate.Value + "',103) and convert(date,pp.[Date],103 ) <= convert(date,'" + txtToDate.Value + "',103) and pp.shift='Evening' then 3 else 2 end  )"
        End If

        'If rbtnMCCRouteVLCCSelect.IsChecked Then
        Dim arr As List(Of String) = Nothing
        If cbtMCCRouteVLCC.CheckedValue.Count > 0 Then
            arr = cbtMCCRouteVLCC.CheckedValue(1)
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                qry += "and pp.MCC  IN (" + clsCommon.GetMulcallString(arr) + ") "
            Else
                Throw New Exception("Please select at least one MCC")
            End If
        End If
        If cbtMCCRouteVLCC.CheckedValue.Count > 1 Then
            arr = cbtMCCRouteVLCC.CheckedValue(2)
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                qry += " and pp.[Route Code] in (" + clsCommon.GetMulcallString(arr) + ")  "
            Else
                Throw New Exception("Please select at least one Route")
            End If
        End If
        If cbtMCCRouteVLCC.CheckedValue.Count > 1 Then
            arr = cbtMCCRouteVLCC.CheckedValue(3)
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                qry += " and pp.[Vlc Code] in (" + clsCommon.GetMulcallString(arr) + ")  "
            Else
                Throw New Exception("Please select at least one VLC")
            End If
        End If

        'End If

        qry += "order by convert(date,pp.[Date],103)  "


        dt = clsDBFuncationality.GetDataTable(qry)
        gv.DataSource = Nothing
        gv.Rows.Clear()
        gv.Columns.Clear()
        gv.DataSource = dt
        gv.GroupDescriptors.Clear()
        gv.MasterTemplate.SummaryRowsBottom.Clear()
        'FormatGrid()
        gv.Columns("Date").IsVisible = False
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow("No Data Found to Display")
            Exit Sub
        End If

        RadPageView1.SelectedPage = RadPageViewPage2
        ReStoreGridLayout()
    End Sub
    Private Sub RptMCCMilkRegisterDripSaver_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        Private Sub rmDeleteLayout_Click_1(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Sub print(ByVal exporter As EnumExportTo)
        Try
            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                Dim CompName As String = clsDBFuncationality.getSingleValue("Select Comp_Name from TSPL_COMPANY_MASTER Where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'")
                arrHeader.Add(CompName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptMCCMilkRegisterDripSaver & "'"))
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
                    clsCommon.MyExportToPDF("MCC Milk Register (Drip Saver)", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub RptMCCMilkRegisterDripSaver_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            LoadData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()

        End If
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        LoadData()
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
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
