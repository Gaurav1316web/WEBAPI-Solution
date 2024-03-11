'-Created by --[Pankaj Kumar Chaudhary]--Against Ticket no--[BM00000001563]
'' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''

Imports common
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls
Imports System.IO

Public Class FrmAsset_Issue_Return_Report
    Inherits FrmMainTranScreen
    Dim IsFormLoad As Boolean = False

    Private Sub FrmAsset_Issue_Return_Report_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        IsFormLoad = True
        reset()
        LoadType()
        IsFormLoad = False
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmAsset_Issue_Return_Report)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        '' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
        btnExport.Visible = MyBase.isExport
        btnRefresh.Visible = MyBase.isPrintFlag
    End Sub

    Sub reset()
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        LoadAsset()
        chkAssetAll.IsChecked = True
        LoadLocation()
        chkLocationAll.IsChecked = True
        LoadCostCenter()
        chkCCAll.IsChecked = True
        chkDetail.IsChecked = True
    End Sub

    Sub LoadType()
        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Rows.Add("All")
        dt.Rows.Add("Issued")
        dt.Rows.Add("Available")
        cboTransType.DataSource = dt
        cboTransType.ValueMember = "Code"
        cboTransType.DisplayMember = "Code"
    End Sub


    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Sub LoadAsset()
        Dim strquery As String = "Select TSPL_ACQUISITION_DETAIL.Asset_Code as Code, TSPL_ACQUISITION_DETAIL.Asset_Name as Name, TSPL_ACQUISITION_DETAIL.Asset_Specification as Specification from TSPL_ACQUISITION_DETAIL LEFT OUTER JOIN TSPL_ACQUISITION_HEAD on TSPL_ACQUISITION_DETAIL.Acquisition_Code= TSPL_ACQUISITION_HEAD.Acquisition_Code WHERE TSPL_ACQUISITION_HEAD.Status=1"
        cbgAsset.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgAsset.ValueMember = "Code"
        cbgAsset.DisplayMember = "Name"
    End Sub

    Sub LoadLocation()
        Dim strquery As String = "select Location_Code AS Code ,Location_Desc as Description FROM TSPL_LOCATION_MASTER WHERE 2=2 "
        cbgLoc.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgLoc.ValueMember = "Code"
        cbgLoc.DisplayMember = "Description"
    End Sub

    Sub LoadCostCenter()
        Dim strquery As String = "Select Cost_Code as Code, Cost_name as Name from TSPL_CostCenter_MASTER"
        cbgCostCenter.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgCostCenter.ValueMember = "Code"
        cbgCostCenter.DisplayMember = "Name"
    End Sub

    Private Sub Print(ByVal exporter As EnumExportTo)
        Try
            If chkAssetSelect.IsChecked = True AndAlso cbgAsset.CheckedValue.Count <= 0 Then
                RadMessageBox.Show("Please select at least one Asset or select ALL")
                Return
            ElseIf chkLocationSelect.IsChecked = True AndAlso cbgLoc.CheckedValue.Count <= 0 Then
                RadMessageBox.Show("Please select at least one Location or select ALL")
                Return
            ElseIf chkCCSelect.IsChecked = True AndAlso cbgCostCenter.CheckedValue.Count <= 0 Then
                RadMessageBox.Show("Please select at least one Vendor or select ALL")
                Return
            End If

            GV1.EnableFiltering = True
            Dim dt As DataTable
            Dim strFromDate As String = clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy")
            Dim strToDate As String = clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy")
            Dim strQuery As String

            strQuery = "Select TSPL_ACQUISITION_DETAIL.Asset_Code, TSPL_ACQUISITION_DETAIL.Asset_Name, TSPL_ACQUISITION_DETAIL.Asset_Specification, "
            strQuery += " TSPL_ACQUISITION_HEAD.Loc_Code+ ' - ' + TSPL_LOCATION_MASTER.Location_Desc as Location, TSPL_ASSET_ISSUE_RETURN.Trans_Type, TSPL_ASSET_ISSUE_RETURN.Trans_Date, "
            strQuery += " TSPL_ASSET_ISSUE_RETURN.From_Entity, Case When TSPL_ASSET_ISSUE_RETURN.Trans_Type='Issue' Then L1.Location_Desc Else CC1.Emp_name End AS FromEntityDesc, "
            strQuery += " TSPL_ASSET_ISSUE_RETURN.To_Entity, Case When TSPL_ASSET_ISSUE_RETURN.Trans_Type='Issue' Then CC2.Emp_name Else L2.Location_Desc End AS ToEntityDesc, "
            strQuery += " ROW_NUMBER() OVER (PARTITION By Asset_Code Order By Trans_Date Desc) as RowNo from TSPL_ACQUISITION_DETAIL "
            strQuery += " LEFT OUTER JOIN TSPL_ACQUISITION_HEAD ON TSPL_ACQUISITION_HEAD.Acquisition_Code=TSPL_ACQUISITION_DETAIL.Acquisition_Code"
            strQuery += " LEFT OUTER JOIN TSPL_ASSET_ISSUE_RETURN ON TSPL_ACQUISITION_DETAIL.Asset_Code=TSPL_ASSET_ISSUE_RETURN.Asset_Id"
            strQuery += " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=TSPL_ACQUISITION_HEAD.Loc_Code"
            strQuery += " LEFT OUTER JOIN TSPL_LOCATION_MASTER L1 on L1.Location_Code= TSPL_ASSET_ISSUE_RETURN.From_Entity"
            strQuery += " LEFT OUTER JOIN TSPL_LOCATION_MASTER L2 on L2.Location_Code= TSPL_ASSET_ISSUE_RETURN.To_Entity"
            strQuery += " LEFT OUTER JOIN TSPL_EMPLOYEE_MASTER CC1 ON CC1.EMP_CODE=TSPL_ASSET_ISSUE_RETURN.From_Entity"
            strQuery += " LEFT OUTER JOIN TSPL_EMPLOYEE_MASTER CC2 ON CC2.EMP_CODE=TSPL_ASSET_ISSUE_RETURN.To_Entity WHERE TSPL_ACQUISITION_HEAD.Status=1"

            If clsCommon.CompairString(cboTransType.SelectedValue, "Issued") = CompairStringResult.Equal Then
                strQuery += " AND TSPL_ACQUISITION_DETAIL.Is_Issued='Y'"
            ElseIf clsCommon.CompairString(cboTransType.SelectedValue, "Available") = CompairStringResult.Equal Then
                strQuery += " AND TSPL_ACQUISITION_DETAIL.Is_Issued='N'"
            End If

            If chkAssetSelect.IsChecked And cbgAsset.CheckedValue.Count > 0 Then
                strQuery += " AND TSPL_ACQUISITION_DETAIL.Asset_Code In (" + clsCommon.GetMulcallString(cbgAsset.CheckedValue) + ")"
            End If

            If chkCCSelect.IsChecked And cbgCostCenter.CheckedValue.Count > 0 Then
                strQuery += " AND TSPL_ASSET_ISSUE_RETURN.To_Entity In (" + clsCommon.GetMulcallString(cbgCostCenter.CheckedValue) + ")"
            End If

            If chkLocationSelect.IsChecked And cbgLoc.CheckedValue.Count > 0 Then
                strQuery += " AND TSPL_ACQUISITION_HEAD.Loc_Code In (" + clsCommon.GetMulcallString(cbgLoc.CheckedValue) + ")"
            End If

            If chkSummary.IsChecked Then
                strQuery = "Select * from ( " + strQuery + " ) XXX WHERE RowNo=1"
            End If

            strQuery += " ORDER BY ASSET_CODE"

            dt = clsDBFuncationality.GetDataTable(strQuery)
            GV1.DataSource = Nothing
            GV1.GroupDescriptors.Clear()
            GV1.MasterTemplate.SummaryRowsBottom.Clear()

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                RadMessageBox.Show("No Data Found to Display", Me.Text)
                Exit Sub
            Else
                GV1.DataSource = dt
                SetGridFormationOFGV1()
                ReStoreGridLayout()
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strtemp As String = "Date Range : " + strFromDate + " To " + strToDate
            arrHeader.Add(strtemp)
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            If chkLocationSelect.IsChecked Then
                Dim strLocationName As String = ""
                For Each StrName As String In cbgLoc.CheckedDisplayMember
                    If clsCommon.myLen(strLocationName) > 0 Then
                        strLocationName += ", "
                    End If
                    strLocationName += StrName
                Next
                Dim strLocationCode As String = ""
                For Each StrCode As String In cbgLoc.CheckedValue
                    If clsCommon.myLen(strLocationCode) > 0 Then
                        strLocationCode += ", "
                    End If
                    strLocationCode += StrCode
                Next
                arrHeader.Add(("Location: " + strLocationName + " "))
            End If




            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Asst Issue/Return", GV1, arrHeader, "Asset_Issue_Return")
            ElseIf exporter = EnumExportTo.PDF Then
                clsCommon.MyExportToPDF("Asset Issue/Return", GV1, arrHeader, "Asset_Issue_Return", True)
            End If

        Catch ex As Exception
            RadMessageBox.Show(ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFormationOFGV1()
        GV1.TableElement.TableHeaderHeight = 40
        GV1.MasterTemplate.ShowRowHeaderColumn = False
        'For ii As Integer = 0 To GV1.Columns.Count - 1
        '    GV1.Columns(ii).ReadOnly = True
        '    GV1.Columns(ii).IsVisible = False
        'Next

        GV1.Columns("Asset_Code").Width = 100
        GV1.Columns("Asset_Code").HeaderText = "Asset_Id"

        GV1.Columns("Asset_Name").Width = 200
        GV1.Columns("Asset_Name").HeaderText = "Asset Name"

        GV1.Columns("Asset_Specification").Width = 300
        GV1.Columns("Asset_Specification").HeaderText = "Asset Specification"

        GV1.Columns("Location").Width = 200
        GV1.Columns("Location").HeaderText = "Location"

        GV1.Columns("Trans_Type").Width = 80
        GV1.Columns("Trans_Type").HeaderText = "Type"

        GV1.Columns("Trans_Date").Width = 140
        GV1.Columns("Trans_Date").HeaderText = "Date"

        GV1.Columns("From_Entity").Width = 80
        GV1.Columns("From_Entity").HeaderText = "From"

        GV1.Columns("FromEntityDesc").Width = 200
        GV1.Columns("FromEntityDesc").HeaderText = "Description"

        GV1.Columns("To_Entity").Width = 80
        GV1.Columns("To_Entity").HeaderText = "To"

        GV1.Columns("ToEntityDesc").Width = 200
        GV1.Columns("ToEntityDesc").HeaderText = "Description"

        GV1.Columns("RowNo").IsVisible = False
        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub

    Public Enum EnumExportTo
        Excel = 0
        PDF = 1
        Refresh = 2
    End Enum

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        reset()
    End Sub

    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgLoc.Enabled = Not chkLocationAll.IsChecked
    End Sub

    Private Sub chkVendorAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCCAll.ToggleStateChanged
        cbgCostCenter.Enabled = Not chkCCAll.IsChecked
    End Sub

    Private Sub chkVisiAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkAssetAll.ToggleStateChanged
        cbgAsset.Enabled = Not chkAssetAll.IsChecked
    End Sub


    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        PageSetupReport_ID = MyBase.Form_ID + IIf(chkSummary.IsChecked = True, "S", "D")
        TemplateGridview = GV1
        Print(EnumExportTo.Refresh)
    End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        'Print(EnumExportTo.Excel)
        Export(EnumExportTo.Excel)
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        'Print(EnumExportTo.PDF)
        Export(EnumExportTo.PDF)
    End Sub

    Private Sub cboTransType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboTransType.SelectedIndexChanged
        If Not IsFormLoad Then
            If clsCommon.CompairString(cboTransType.SelectedValue, "Issued") = CompairStringResult.Equal Then
                chkDetail.Enabled = False
                chkSummary.IsChecked = True
            ElseIf clsCommon.CompairString(cboTransType.SelectedValue, "Available") = CompairStringResult.Equal Then
                chkDetail.Enabled = False
                chkSummary.IsChecked = True
            Else
                chkDetail.Enabled = True
            End If
        End If
    End Sub

    Private Sub chkSummary_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkSummary.ToggleStateChanged
        chkCCAll.IsChecked = True
        gbCC.Enabled = Not chkSummary.IsChecked
    End Sub

    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            If GV1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()

                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmAsset_Issue_Return_Report & "'"))
                If chkLocationSelect.IsChecked Then
                    Dim strLocationName As String = ""
                    For Each StrName As String In cbgLoc.CheckedDisplayMember
                        If clsCommon.myLen(strLocationName) > 0 Then
                            strLocationName += ", "
                        End If
                        strLocationName += StrName
                    Next
                    Dim strLocationCode As String = ""
                    For Each StrCode As String In cbgLoc.CheckedValue
                        If clsCommon.myLen(strLocationCode) > 0 Then
                            strLocationCode += ", "
                        End If
                        strLocationCode += StrCode
                    Next
                    arrHeader.Add(("Location: " + strLocationName + " "))
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
                    transportSql.applyExportTemplate(GV1, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(GV1, "", Me.Text, , arrHeader)
                    'transportSql.exportdataChilRows(GV1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                Else
                    transportSql.applyExportTemplate(GV1, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("Asset Issue/Return", GV1, arrHeader, "Asset_Issue_Return", PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
  Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            GV1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            GV1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = GV1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
            MessageBox.Show(err.Message)
        End Try
    End Sub
End Class
