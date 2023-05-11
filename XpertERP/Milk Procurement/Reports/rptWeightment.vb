'=============BM00000007876===============================
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
'created by preeti gupta ticket no.[BM00000004443]
Public Class RptWeightment
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.RptWeighment)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton.Visible = MyBase.isExport
    End Sub
    Private Sub chkTankerAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkTankerAll.ToggleStateChanged
        cbgTanker.Enabled = Not chkTankerAll.IsChecked
    End Sub

    Private Sub chkVendorAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVendorAll.ToggleStateChanged
        cbgVendor.Enabled = Not chkVendorAll.IsChecked
    End Sub

    Private Sub RptWeightment_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.R Then
            Load_Report()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub

    Private Sub btnSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveLayout.Click

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

    Private Sub btnDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
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
    Sub LoadTanker()
        Dim qry As String = " select Tanker_No as Code ,Tanker_Name as Name from TSPL_TANKER_MASTER   "
        'Dim qry As String = "  select Vendor_Code  as Code ,Vendor_Name  as Name from TSPL_VENDOR_MASTER    "
        cbgTanker.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgTanker.ValueMember = "Code"
        cbgTanker.DisplayMember = "Name"
    End Sub
    Sub Loadvendor()
        Dim qry As String = "  select Vendor_Code  as Code ,Vendor_Name  as Name from TSPL_VENDOR_MASTER  WHERE Status='N'  "
        'Dim qry As String = " select Tanker_No as Code ,Tanker_Name as Name from TSPL_TANKER_MASTER   "
        cbgVendor.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVendor.ValueMember = "Code"
        cbgVendor.DisplayMember = "Name"
    End Sub

    'KUNAL > TICKET : BM00000009581 > DATE 27-SEP-2016 
    Private Sub LoadLocations()
        Try
            Dim qry As String = "select Location_Code Code , Location_Desc Name from TSPL_LOCATION_MASTER"
            cbgLocations.DataSource = clsDBFuncationality.GetDataTable(qry)
            cbgLocations.DisplayMember = "Name"
            cbgLocations.ValueMember = "Code"

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub Reset()
        Try
            txtToDate.Value = clsCommon.GETSERVERDATE()
            txtFromDate.Value = txtToDate.Value.AddMonths(-1)
            chkTankerAll.CheckState = CheckState.Checked
            chkVendorAll.CheckState = CheckState.Checked
            gv.DataSource = Nothing
            LoadTanker()
            Loadvendor()
            'KUNAL > TICKET : BM00000009581 > DATE 26-SEP-2016 > ADDED LOCATION FILTER
            LoadLocations()
            cbgLocations.UnCheckedAll()
            rbLocationsAll.IsChecked = True
            RadPageView1.SelectedPage = RadPageViewPage1
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub RptWeightment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            SetUserMgmtNew()
            ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
            ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
            ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New ")
            RadPageView1.SelectedPage = RadPageViewPage1
            txtToDate.Value = clsCommon.GETSERVERDATE()
            txtFromDate.Value = txtToDate.Value.AddMonths(-1)
            LoadTanker()
            Loadvendor()
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptWeighment & "'"))
                If chkTankerSelect.IsChecked Then
                    Dim strTankerName As String = ""
                    For Each StrName As String In cbgTanker.CheckedDisplayMember
                        If clsCommon.myLen(strTankerName) > 0 Then
                            strTankerName += ", "
                        End If
                        strTankerName += StrName
                    Next
                    Dim strTankerCode As String = ""
                    For Each StrCode As String In cbgTanker.CheckedValue
                        If clsCommon.myLen(strTankerCode) > 0 Then
                            strTankerCode += ", "
                        End If
                        strTankerCode += StrCode
                    Next

                    arrHeader.Add(("Tanker Code : " + strTankerCode + "  Tanker Name: " + strTankerName + " "))

                End If
                If chkVendorSelect.IsChecked Then
                    Dim strvendorName As String = ""
                    For Each StrName As String In cbgVendor.CheckedDisplayMember
                        If clsCommon.myLen(strvendorName) > 0 Then
                            strvendorName += ", "
                        End If
                        strvendorName += StrName
                    Next
                    Dim strVendorCode As String = ""
                    For Each StrCode As String In cbgVendor.CheckedValue
                        If clsCommon.myLen(strVendorCode) > 0 Then
                            strVendorCode += ", "
                        End If
                        strVendorCode += StrCode
                    Next

                    arrHeader.Add(("Vendor Name: " + strvendorName + " "))

                End If

                If rbLocationsSelect.IsChecked Then
                    Dim strvendorName As String = ""
                    For Each StrName As String In cbgLocations.CheckedDisplayMember
                        If clsCommon.myLen(strvendorName) > 0 Then
                            strvendorName += ", "
                        End If
                        strvendorName += StrName
                    Next
                    Dim strVendorCode As String = ""

                    arrHeader.Add(("Location Name: " + strvendorName + " "))

                End If

                If exporter = EnumExportTo.Excel Then
                    clsCommon.MyExportToExcelGrid("Weigment Report", gv, arrHeader, Me.Text)
                Else
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("Weigment Report", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Private Sub BtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub RmExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RmExport.Click
        Try
            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptWeighment & "'"))

                If chkTankerSelect.IsChecked Then
                    Dim strTankerName As String = ""
                    For Each StrName As String In cbgTanker.CheckedDisplayMember
                        If clsCommon.myLen(strTankerName) > 0 Then
                            strTankerName += ", "
                        End If
                        strTankerName += StrName
                    Next
                    Dim strTankerCode As String = ""
                    For Each StrCode As String In cbgTanker.CheckedValue
                        If clsCommon.myLen(strTankerCode) > 0 Then
                            strTankerCode += ", "
                        End If
                        strTankerCode += StrCode
                    Next

                    arrHeader.Add((" Tanker Code : " + strTankerCode + "  Tanker Name: " + strTankerName + " "))

                End If
                If chkVendorSelect.IsChecked Then
                    Dim strvendorName As String = ""
                    For Each StrName As String In cbgVendor.CheckedDisplayMember
                        If clsCommon.myLen(strvendorName) > 0 Then
                            strvendorName += ", "
                        End If
                        strvendorName += StrName
                    Next
                    Dim strVendorCode As String = ""
                    For Each StrCode As String In cbgVendor.CheckedValue
                        If clsCommon.myLen(strVendorCode) > 0 Then
                            strVendorCode += ", "
                        End If
                        strVendorCode += StrCode
                    Next

                    arrHeader.Add(("   Vendor Name: " + strvendorName + " "))

                End If

                If rbLocationsSelect.IsChecked Then
                    Dim strvendorName As String = ""
                    For Each StrName As String In cbgLocations.CheckedDisplayMember
                        If clsCommon.myLen(strvendorName) > 0 Then
                            strvendorName += ", "
                        End If
                        strvendorName += StrName
                    Next
                    Dim strVendorCode As String = ""

                    arrHeader.Add(("Location Name: " + strvendorName + " "))

                End If

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
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Public Sub Load_Report()
        If txtFromDate.Value > txtToDate.Value Then
            common.clsCommon.MyMessageBoxShow("From date can not be greater then to Date")
            txtFromDate.Focus()
            Exit Sub
        End If
        If chkVendorSelect.IsChecked AndAlso cbgVendor.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast single Vendor or select all.")
            Exit Sub
        End If
        If chkTankerSelect.IsChecked AndAlso cbgTanker.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast single Tanker or select all.")
            Exit Sub
        End If

        Dim whrcls As String = " where 2=2 "

        Dim sQuery As String = "select '' as SNo,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_Weighment_Detail.Weighment_date  ,TSPL_Weighment_Detail.Tanker_No ,    TSPL_Weighment_Detail.Vendor_Code ,TSPL_VENDOR_MASTER.Vendor_Name , TSPL_Weighment_Detail.location_Code [Location Code], TSPL_Weighment_Detail.Location_Desc [Location Desc]  ,TSPL_Weighment_Detail.Gross_Weight ,TSPL_Weighment_Detail.Tare_Weight ,TSPL_Weighment_Detail.Net_Weight   from TSPL_Weighment_Detail  left outer join TSPL_TANKER_MASTER  on TSPL_TANKER_MASTER .Tanker_No =TSPL_Weighment_Detail.Tanker_No"
        sQuery += " left outer join TSPL_COMPANY_MASTER  on TSPL_COMPANY_MASTER.Comp_Code =TSPL_Weighment_Detail.comp_code left outer join TSPL_VENDOR_MASTER  on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_Weighment_Detail.Vendor_Code  "
        sQuery += " where 2=2"
        sQuery += " and TSPL_Weighment_Detail.Doc_Type ='BulkProc' and convert(date,TSPL_Weighment_Detail.Weighment_date,103)>=convert(date,('" + txtFromDate.Value + "'),103) and convert(date,TSPL_Weighment_Detail.Weighment_date,103) <=convert(date,('" + txtToDate.Value + "'),103) "
        If chkTankerSelect.IsChecked And cbgTanker.CheckedValue.Count > 0 Then
            sQuery += " and TSPL_TANKER_MASTER.Tanker_No  IN (" + clsCommon.GetMulcallString(cbgTanker.CheckedValue) + ") "
        End If
        If chkVendorSelect.IsChecked And cbgVendor.CheckedValue.Count > 0 Then
            sQuery += " and TSPL_VENDOR_MASTER.Vendor_Code in (" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ")  "
        End If
        'KUNAL > TICKET : BM00000009581 > DATE 27-SEP-2016 > ADDED LOCATION FILTER
        If rbLocationsSelect.IsChecked And cbgLocations.CheckedValue.Count > 0 Then
            sQuery += " AND TSPL_Weighment_Detail.location_Code IN (" + clsCommon.GetMulcallString(cbgLocations.CheckedValue) + ")"
        End If

        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(sQuery)
        If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dtgv
            For i As Integer = 0 To gv.Rows.Count - 1
                gv.Rows(i).Cells(0).Value = i + 1
            Next
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            RadPageView1.SelectedPage = RadPageViewPage2
            FormatGrid()
        Else
            clsCommon.MyMessageBoxShow("No Data Found")
        End If
        ReStoreGridLayout()
    End Sub
    Sub FormatGrid()
        Dim strItemCode As String = ""
        gv.TableElement.TableHeaderHeight = 20
        gv.MasterTemplate.ShowRowHeaderColumn = False

        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
           
        Next

        gv.Columns("SNo").IsVisible = True
        gv.Columns("SNo").Width = 30
        gv.Columns("SNo").HeaderText = " S No."


        gv.Columns("Tanker_No").IsVisible = True
        gv.Columns("Tanker_No").Width = 100
        gv.Columns("Tanker_No").HeaderText = "Tanker No"

        gv.Columns("Vendor_Name").IsVisible = True
        gv.Columns("Vendor_Name").Width = 200
        gv.Columns("Vendor_Name").HeaderText = "Supplier's Name"

        'KUNAL > TICKET : BM00000009581 > DATE 26-SEP-2016 > ADDED LOCATION FILTER
        gv.Columns("Location Code").IsVisible = True
        gv.Columns("Location Code").Width = 100
        gv.Columns("Location Code").HeaderText = "Location Code"

        'KUNAL > TICKET : BM00000009581 > DATE 26-SEP-2016 > ADDED LOCATION FILTER
        gv.Columns("Location Desc").IsVisible = True
        gv.Columns("Location Desc").Width = 200
        gv.Columns("Location Desc").HeaderText = "Location Desc"


        gv.Columns("Gross_Weight").IsVisible = True
        gv.Columns("Gross_Weight").Width = 100
        gv.Columns("Gross_Weight").HeaderText = "Gross Weight"


        gv.Columns("Tare_Weight").IsVisible = True
        gv.Columns("Tare_Weight").Width = 100
        gv.Columns("Tare_Weight").HeaderText = "Tare Weight"


        gv.Columns("Net_Weight").IsVisible = True
        gv.Columns("Net_Weight").Width = 100
        gv.Columns("Net_Weight").HeaderText = "Net Weight"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        'gv.GroupDescriptors.Add(New GridGroupByExpression("ROUTE_CODE as Item format ""{0}: {1}"" Group By ROUTE_CODE"))


        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub

    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        Try
            PageSetupReport_ID = MyBase.Form_ID
            TemplateGridview = gv
            Load_Report()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    'KUNAL > TICKET : BM00000009581 > DATE 26-SEP-2016 > ADDED LOCATION FILTER
    Private Sub rbLocationsAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbLocationsAll.ToggleStateChanged, rbLocationsSelect.ToggleStateChanged
        Try
            cbgLocations.Enabled = rbLocationsSelect.IsChecked
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub RmPDF_Click(sender As Object, e As EventArgs) Handles RmPDF.Click
        print(EnumExportTo.PDF)
    End Sub
End Class
