 
Imports common
Imports System.IO
Public Class frmRptUnpostedPO
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim arrBack As List(Of String)
    Const colSegCode As String = "SEGCODE"
    Const colSegName As String = "SEGNAME"
    Const colFrom As String = "FROMFILTER"
    Const colFromName As String = "FROMFILTERNAME"
    Const colTo As String = "TOFILTER"
    Const colToName As String = "TOFILTERNAME"
    Const colIsForAC As String = "ISFORAC"
    Dim StrQry As String
    Const colSelect As String = "SELECT"
    Const colCompCode As String = "COMPCODE"
    Const colCompName As String = "COMPNAME"
    Const colDataBaseName As String = "DATABASE"

    Public arrCompany As ArrayList
    Public arrSourceCode As ArrayList
    Public arrEmployee As ArrayList
    Public arrLocationSegment As ArrayList
    Public arrAccount As ArrayList
    Public arrDepartment As ArrayList
    Public arrVISI As ArrayList
    Public arrMachine As ArrayList
    Public arrVehicle As ArrayList

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public isDataLoad As Boolean = False

    Dim dt As DataTable = Nothing
    Dim strERPStartDate As String
    Dim isRunDoubleClick As Boolean = False
#End Region

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.rptUnpostedPO)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub

    Private Sub frmRptTrialBalance_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso MyBase.isModifyFlag Then
            funreset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Control AndAlso e.KeyCode = Keys.P Then
            RefreshData()
            PrintData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Private Sub FrmRptSales_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        RadPageView1.SelectedPage = RadPageViewPage1

        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)

        LoadBlankGrid()
        SetDataBaseGrid()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New Trasnaction")
        'ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P for Print ")
        funreset()
    End Sub

    Function LoadReportType(ByVal isAddNone As Boolean) As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        If isAddNone Then
            dr = dt.NewRow()
            dr("Code") = " "
            dr("Name") = "None"
            dt.Rows.Add(dr)
        End If

        dr = dt.NewRow()
        dr("Code") = "Main Group Wise"
        dr("Name") = "Main Group Wise"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Group Wise"
        dr("Name") = "Group Wise"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Sub Group Wise"
        dr("Name") = "Sub Group Wise"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Main Account Wise"
        dr("Name") = "Main Account Wise"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "GL Account Wise"
        dr("Name") = "GL Account Wise"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Customer/Vedndor Wise"
        dr("Name") = "Customer/Vedndor Wise"
        dt.Rows.Add(dr)

        Return dt
    End Function

    

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

    Private Sub LoadBlankGrid()


    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        RefreshData()
        PrintData()
    End Sub

    Private Sub PrintData()
        'If clsCommon.CompairString(cbgSrcCode.Text, "Trial Balance") = CompairStringResult.Equal Then
        '    If chkRollupWise.Checked Then
        '        frmCrystalReportViewer.funreport(CrystalReportFolder.GeneralLedger, dt, "rptTrialBalanceRP", "Trial Balance")
        '    Else

        '        frmCrystalReportViewer.funreport(CrystalReportFolder.GeneralLedger, dt, "rptTrialBalance", "Trial Balance")
        '    End If
        'ElseIf clsCommon.CompairString(cbgSrcCode.Text, "Subledger Trial Balance") = CompairStringResult.Equal Then
        '    If chkRollupWise.Checked Then
        '        frmCrystalReportViewer.funreport(CrystalReportFolder.GeneralLedger, dt, "rptTrialBalanceSubLdgRP", "Trial Balance")
        '    Else
        '        frmCrystalReportViewer.funreport(CrystalReportFolder.GeneralLedger, dt, "rptTrialBalanceSubLdg", "Trial Balance")
        '    End If

        'ElseIf clsCommon.CompairString(cbgSrcCode.Text, "Period Trial Balance") = CompairStringResult.Equal Then
        '    If chkShowOPBal.Checked Then
        '        If chkRollupWise.Checked Then
        '            frmCrystalReportViewer.funreport(CrystalReportFolder.GeneralLedger, dt, "rptTrialBalancePeriodRP", "Periodical Trial Balance")
        '        Else
        '            frmCrystalReportViewer.funreport(CrystalReportFolder.GeneralLedger, dt, "rptTrialBalancePeriod", "Periodical Trial Balance")
        '        End If
        '    Else
        '        If chkRollupWise.Checked Then
        '            frmCrystalReportViewer.funreport(CrystalReportFolder.GeneralLedger, dt, "rptTrialBalancePeriodOPBalRP", "Periodical Trial Balance")
        '        Else
        '            frmCrystalReportViewer.funreport(CrystalReportFolder.GeneralLedger, dt, "rptTrialBalancePeriodOPBal", "Periodical Trial Balance")
        '        End If
        '    End If
        'ElseIf clsCommon.CompairString(cbgSrcCode.Text, "Basic Trial Balance") = CompairStringResult.Equal Then
        '    frmCrystalReportViewer.funreport(CrystalReportFolder.GeneralLedger, dt, "rptTrialBalanceBasic", "Trial Balance")
        'End If
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        funreset()
    End Sub

    Sub funreset()
        gv1.GroupDescriptors.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        dt = Nothing
        
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        EnableDisableControls(True)


        arrBack = New List(Of String)
    End Sub

    'Private Sub txtFrom__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean)
    '    Dim qry As String = ""
    '    Dim WhrCls As String = ""
    '    Dim OrderCls As String = ""
    '    qry = "select Account_Group_Code as Code,Account_Group_Desc  as [Description] from TSPL_ACCOUNT_GROUPS "
    '    OrderCls = "Code"
    '    txtFrom.Value = clsCommon.ShowSelectForm("ACGroupFinder", qry, "Code", WhrCls, txtFrom.Value, OrderCls, isButtonClicked)
    '    lblFrom.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Account_Group_Desc  from TSPL_ACCOUNT_GROUPS  where Account_Group_Code='" + txtFrom.Value + "'"))
    '    ''End If
    'End Sub

    'Private Sub txtTo__MYValidating_1(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean)
    '    Dim qry As String = ""
    '    Dim WhrCls As String = ""
    '    Dim OrderCls As String = ""
    '    qry = "select Account_Group_Code as Code,Account_Group_Desc  as [Description] from TSPL_ACCOUNT_GROUPS "
    '    OrderCls = "Code"
    '    txtTo.Value = clsCommon.ShowSelectForm("ACGroupFinder", qry, "Code", WhrCls, txtTo.Value, OrderCls, isButtonClicked)
    '    lblTO.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Account_Group_Desc  from TSPL_ACCOUNT_GROUPS  where Account_Group_Code='" + txtTo.Value + "'"))
    'End Sub

    Private Function GetSelectedDatabase() As List(Of String)
        Dim arrDBName As New List(Of String)
        'For ii As Integer = 0 To gvDB.Rows.Count - 1
        '    If txtCompany.arrValueMember Then
        '        arrDBName.Add(clsCommon.myCstr(gvDB.Rows(ii).Cells(colDataBaseName).Value))
        '    End If
        'Next
        arrDBName.Add(objCommonVar.CurrDatabase)
        Return arrDBName
    End Function

    Private Sub cmbtype_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs)
        'chkShowOPBal.Visible = False
        ''chkRollupWise.Visible = True
        'If clsCommon.CompairString(cbgSrcCode.Text, "Period Trial Balance") = CompairStringResult.Equal Then
        '    chkShowOPBal.Visible = True
        '    chkMultipleRollup.Visible = True
        '    lblFromdate.Visible = True
        '    txtFromDate.Visible = True
        '    lblToDate.Text = "To Date"
        'ElseIf clsCommon.CompairString(cbgSrcCode.Text, "Basic Trial Balance") = CompairStringResult.Equal Then
        '    'chkRollupWise.Visible = False
        '    chkMultipleRollup.Visible = False
        '    lblFromdate.Visible = True
        '    txtFromDate.Visible = True
        '    lblToDate.Text = "To Date"
        'ElseIf clsCommon.CompairString(cbgSrcCode.Text, "Trial Balance") = CompairStringResult.Equal Then
        '    'chkRollupWise.Visible = True
        '    chkMultipleRollup.Visible = True
        '    lblFromdate.Visible = False
        '    txtFromDate.Visible = False
        '    lblToDate.Text = "As On Date"
        'ElseIf clsCommon.CompairString(cbgSrcCode.Text, "Subledger Trial Balance") = CompairStringResult.Equal Then
        '    chkMultipleRollup.Visible = True
        '    lblFromdate.Visible = True
        '    txtFromDate.Visible = True
        '    lblToDate.Text = "To Date"
        'ElseIf clsCommon.CompairString(cbgSrcCode.Text, "Location wise") = CompairStringResult.Equal Then

        'Else
        '    chkMultipleRollup.Visible = False
        'End If

    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv1
        RefreshData()
    End Sub

    Public Sub RefreshData()
        Try
            gv1.EnableFiltering = True
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()

            Dim BaseQry As String = " select TSPL_PURCHASE_ORDER_HEAD.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No,convert(varchar, TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103) as PurchaseOrder_Date, TSPL_PURCHASE_ORDER_HEAD.PO_Total_Amt,TSPL_PURCHASE_ORDER_HEAD.Created_By" + Environment.NewLine +
            " ,(TSPL_VENDOR_MASTER.Add1+case when len(isnull( TSPL_VENDOR_MASTER.Add1,''))>=0 then ',' else '' end +TSPL_VENDOR_MASTER.Add2+case when len(isnull( TSPL_VENDOR_MASTER.Add2,''))>=0 then ',' else '' end +TSPL_VENDOR_MASTER.Add3 ) as Address " + Environment.NewLine +
            " from TSPL_PURCHASE_ORDER_HEAD " + Environment.NewLine +
            " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_PURCHASE_ORDER_HEAD.Vendor_Code " + Environment.NewLine +
            " where TSPL_PURCHASE_ORDER_HEAD.Status=0 " + Environment.NewLine +
            " and TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "'  and TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'"
            If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                BaseQry += " and TSPL_PURCHASE_ORDER_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ") "
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                BaseQry += " and TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
            End If
            BaseQry += "  order by TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date"
            dt = clsDBFuncationality.GetDataTable(BaseQry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data found to display")
            End If
            SetGridFormation()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFormation()
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As GridViewSummaryItem
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        gv1.DataSource = dt
        RadPageView1.SelectedPage = RadPageViewPage2
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = False
        Next

        gv1.Columns("Vendor_Code").IsVisible = True
        gv1.Columns("Vendor_Code").HeaderText = "Vendor Code"

        gv1.Columns("Vendor_Name").IsVisible = True
        gv1.Columns("Vendor_Name").HeaderText = "Vendor"

        gv1.Columns("PurchaseOrder_No").IsVisible = True
        gv1.Columns("PurchaseOrder_No").HeaderText = "Order No"

        gv1.Columns("PurchaseOrder_Date").IsVisible = True
        gv1.Columns("PurchaseOrder_Date").HeaderText = "Order Date"

        gv1.Columns("PO_Total_Amt").IsVisible = True
        gv1.Columns("PO_Total_Amt").HeaderText = "Amount"

        gv1.Columns("Created_By").IsVisible = True
        gv1.Columns("Created_By").HeaderText = "Created By"

        gv1.Columns("Address").IsVisible = True
        gv1.Columns("Address").HeaderText = "Address"

         

        item1 = New GridViewSummaryItem("PO_Total_Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        

        gv1.MasterTemplate.ExpandAllGroups()
        gv1.ShowGroupPanel = False
        gv1.MasterTemplate.AutoExpandGroups = True
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        EnableDisableControls(False)
        gv1.BestFitColumns()
        ReStoreGridLayout()
    End Sub

    Private Sub EnableDisableControls(ByVal Val As Boolean)
        txtVendor.Enabled = Val
        txtLocation.Enabled = Val
        txtFromDate.Enabled = Val
        txtToDate.Enabled = Val
    End Sub

    Private Sub gv1_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.ViewCellFormatting
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub



    Public Sub SetDiplayMember(ByVal Fnd As common.UserControls.txtMultiSelectFinder, ByVal Col_Name As String, ByVal tb_name As String, ByVal val_col_Name As String)
        Try
            Dim sQuery As String = "select TSPL_GL_SEGMENT_CODE." & Col_Name & " as Name,xxx." & val_col_Name & " as Code from (select Loc_Segment_Code  from " & tb_name & " where LEN(isnull(Loc_Segment_Code,''))>0 group by Loc_Segment_Code having Loc_Segment_Code in (" + objCommonVar.strCurrUserLocationsSegment + "))xxx left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code=xxx.Loc_Segment_Code and TSPL_GL_SEGMENT_CODE.Seg_No='7' where Loc_Segment_Code in (" & clsCommon.GetMulcallString(Fnd.arrValueMember) & ") order by xxx.Loc_Segment_Code"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(sQuery)
            Dim arrList As New ArrayList
            For Each row As DataRow In dt.Rows
                arrList.Add(row(0))
            Next
            Fnd.arrDispalyMember = arrList
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub


    Private Sub TxtMultiSelectFinder1__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("MulSelLUP", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub

    Private Sub txtMainGroup__My_Click(sender As Object, e As EventArgs) Handles txtVendor._My_Click
        Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name from TSPL_VENDOR_MASTER  WHERE  Status='N'  order by Vendor_Code"
        txtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("MulSelVUP", qry, "Code", "Name", txtVendor.arrValueMember, txtVendor.arrDispalyMember)
    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        Dim ReportID As String = GetReportID()
        If clsCommon.myLen(ReportID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        clsGridLayout.DeleteData(GetReportID(), objCommonVar.CurrentUserCode)
    End Sub

    Private Function GetReportID() As String
        Dim ReportID As String = "UPPO"
        Return ReportID
    End Function

    Private Sub ReStoreGridLayout()
        Try
            Dim ReportID As String = GetReportID()
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            common.clsCommon.MyMessageBoxShow(err.Message)
        End Try
    End Sub


    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        print(EnumExportTo.PDF)
    End Sub

    Private Sub print(ByVal exporter As EnumExportTo)
        Try
            If gv1.Rows.Count > 0 Then
              
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptUnpostedPO & "'"))
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
                If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                    arrHeader.Add(txtVendor.MyLinkLable1.Text + " : " + clsCommon.GetMulcallStringWithComma(txtVendor.arrValueMember))
                End If
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    arrHeader.Add(txtLocation.MyLinkLable1.Text + " : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrValueMember))
                End If

                If exporter = EnumExportTo.Excel Then
                    ' Dim sfd As SaveFileDialog = New SaveFileDialog()
                    'Dim filePath As String
                    'sfd.FileName = Me.Text
                    'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                    'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    '    filePath = sfd.FileName
                    'Else
                    '    Exit Sub
                    'End If
                    transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
                    'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                Else
                    transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                    clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
End Class
