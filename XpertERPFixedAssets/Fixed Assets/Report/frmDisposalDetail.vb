'--Updation By--[Pankaj Kumar Chaudhary] Against Ticket No--[BM00000001522]
'' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''

Imports common
Imports Telerik.WinControls.UI
Imports XpertERPEngine
Imports Telerik.WinControls
Imports System.IO

Public Class FrmDisposalDetail
    Inherits FrmMainTranScreen
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FrmDisposalDetail)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        '' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
        btnExport.Visible = MyBase.isExport
        btnRefresh.Visible = MyBase.isPrintFlag
    End Sub


    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Sub LoadLocation()
        Dim strquery As String = "select Location_Code AS Code ,Location_Desc as Description FROM TSPL_LOCATION_MASTER WHERE 2=2 "
        cbgLoc.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgLoc.ValueMember = "Code"
        cbgLoc.DisplayMember = "Description"
    End Sub

    Sub LoadAssetId()
        Dim strquery As String = "select Asset_Code as Code,Asset_Name as Description FROM TSPL_ACQUISITION_DETAIL WHERE 2=2 "
        cbgAsset.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgAsset.ValueMember = "Code"
        cbgAsset.DisplayMember = "Description"
    End Sub

    Sub LoadVendor()
        Dim strquery As String = "select Vendor_Code as Code,Vendor_Name as Description FROM TSPL_VENDOR_MASTER WHERE 2=2 AND Status='N' "
        cbgVendor.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgVendor.ValueMember = "Code"
        cbgVendor.DisplayMember = "Description"
    End Sub

    Sub LoadGroup()
        Dim strquery As String = "select Group_Code as Code,Description as Description FROM TSPL_ASSET_GROUP WHERE 2=2 "
        cbgGroup.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgGroup.ValueMember = "Code"
        cbgGroup.DisplayMember = "Description"
    End Sub

    Sub LoadCostCenter()
        Dim strquery As String = ""
        If clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ApplyFinancialCostCenter, clsFixedParameterCode.ApplyFinancialCostCenter, Nothing)) = "1", True, False)) = True Then
            strquery = "select Cost_Center_Fin_Code as Code,Cost_Center_Fin_Name as Description FROM TSPL_COST_CENTRE_FINANCIAL WHERE 2=2 "
        Else
            strquery = "select CostCenter_Code as Code,CostCenter_Name as Description FROM TSPL_FA_COST_CENTER_MASTER WHERE 2=2 "
        End If

        cbgCostCenter.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgCostCenter.ValueMember = "Code"
        cbgCostCenter.DisplayMember = "Description"
    End Sub

    Sub LoadCategory()
        Dim strquery As String = "select Category_Code as Code,Description as Description FROM TSPL_ASSET_CATEGORY WHERE 2=2 "
        cbgCategory.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgCategory.ValueMember = "Code"
        cbgCategory.DisplayMember = "Description"
    End Sub


    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgLoc.Enabled = Not chkLocationAll.IsChecked
    End Sub

    Private Sub chkAssetAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkAssetAll.ToggleStateChanged
        cbgAsset.Enabled = Not chkAssetAll.IsChecked
    End Sub

    Private Sub chkCategAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCategAll.ToggleStateChanged
        cbgCategory.Enabled = Not chkCategAll.IsChecked
    End Sub

    Private Sub chkCostCentAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCostCentAll.ToggleStateChanged
        cbgCostCenter.Enabled = Not chkCostCentAll.IsChecked
    End Sub
    Sub reset()
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        chkAssetAll.IsChecked = True
        chkCategAll.IsChecked = True
        chkVendorAll.IsChecked = True
        chkGroupAll.IsChecked = True
        chkLocationAll.IsChecked = True
        chkCostCentAll.IsChecked = True
        LoadAssetId()
        LoadLocation()
        LoadCategory()
        LoadCostCenter()
        LoadGroup()
        LoadVendor()

    End Sub
    Private Sub chkGroupAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkGroupAll.ToggleStateChanged
        cbgGroup.Enabled = Not chkGroupAll.IsChecked
    End Sub

    Private Sub chkVendorAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVendorAll.ToggleStateChanged
        cbgVendor.Enabled = Not chkVendorAll.IsChecked
    End Sub


    Private Sub Print(ByVal exporter As EnumExportTo)
        Try
            'Dim strInvItem, strRetItem, strSeq As String
            If chkLocationSelect.IsChecked = True AndAlso cbgLoc.CheckedValue.Count <= 0 Then
                RadMessageBox.Show("Please select at least one Location or select ALL")
                Return
            ElseIf chkAssetSelect.IsChecked = True AndAlso cbgAsset.CheckedValue.Count <= 0 Then
                RadMessageBox.Show("Please select at least one Asset or select ALL")
                Return
            ElseIf chkCategSelect.IsChecked = True AndAlso cbgCategory.CheckedValue.Count <= 0 Then
                RadMessageBox.Show("Please select at least one Category or select ALL")
                Return
            ElseIf chkCostCentSelect.IsChecked = True AndAlso cbgCostCenter.CheckedValue.Count <= 0 Then
                RadMessageBox.Show("Please select at least one Cost Centre or select ALL")
                Return
            ElseIf chkGroupSelect.IsChecked = True AndAlso cbgGroup.CheckedValue.Count <= 0 Then
                RadMessageBox.Show("Please select at least one Group or select ALL")
                Return
            ElseIf chkVendorSelect.IsChecked = True AndAlso cbgVendor.CheckedValue.Count <= 0 Then
                RadMessageBox.Show("Please select at least one Vendor or select ALL")
                Return

            End If

            GV1.EnableFiltering = True
            Dim dt As DataTable
            Dim strFromDateTime As String = clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy hh:mm tt")
            Dim strToDateTime As String = clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy hh:mm tt")

            Dim strQuery As String = "select TSPL_ACQUISITION_DETAIL.Asset_Code,Asset_Name,Asset_Specification,Acquisition_Date,convert(varchar,TSPL_ASSET_SCRAP_HEAD.Document_Date,103)  as [Disposal Date]"
            strQuery += " ,Location_Desc, CostCenter_Name,TSPL_ASSET_CATEGORY.Description as CatDesc,TSPL_ASSET_GROUP.Description  as GrpDesc,Vendor_Name, max(Book_Source_value) as BookValue,isnull(sum(Amt_After_Discount),0) as [Disposal Value],isnull(sum(Tax_Amt),0) as Tax,isnull((sum(Tax_Amt)+ sum(Amt_After_Discount)),0) as [Total Disposal Value]"
            strQuery += " from TSPL_ACQUISITION_HEAD "
            strQuery += " left outer join TSPL_ACQUISITION_DETAIL on TSPL_ACQUISITION_HEAD.Acquisition_Code=TSPL_ACQUISITION_DETAIL.Acquisition_Code "
            strQuery += "  left outer join TSPL_LOCATION_MASTER on TSPL_ACQUISITION_HEAD.Loc_Code=TSPL_LOCATION_MASTER.Location_Code "
            strQuery += "  left outer join (select TSPL_FA_COST_CENTER_MASTER.CostCenter_Code , CostCenter_Name from TSPL_FA_COST_CENTER_MASTER  union  select TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code as CostCenter_Code , TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name as CostCenter_Name from  TSPL_COST_CENTRE_FINANCIAL ) as  TSPL_FA_COST_CENTER_MASTER on TSPL_ACQUISITION_DETAIL.CostCenter_Code=TSPL_FA_COST_CENTER_MASTER.CostCenter_Code "
            strQuery += "   left outer join TSPL_ASSET_CATEGORY on TSPL_ACQUISITION_DETAIL.Category_code=TSPL_ASSET_CATEGORY.Category_Code "
            strQuery += "    left outer join TSPL_ASSET_GROUP on TSPL_ACQUISITION_DETAIL.Group_Code=TSPL_ASSET_GROUP.Group_Code "
            strQuery += "   left outer join TSPL_VENDOR_MASTER on TSPL_ACQUISITION_HEAD.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code"
            strQuery += "      left outer join TSPL_ASSET_SCRAP_DETAIL on TSPL_ASSET_SCRAP_DETAIL.Asset_Code =TSPL_ACQUISITION_DETAIL.Asset_Code "
            strQuery += "  left outer join TSPL_ASSET_SCRAP_HEAD on TSPL_ASSET_SCRAP_HEAD.Document_No =TSPL_ASSET_SCRAP_DETAIL.Document_No "
            strQuery += "   where Acquisition_Date >=  '" & strFromDateTime & "' AND "
            strQuery += "  Acquisition_Date <=  '" & strToDateTime & "'   "

            If chkLocationSelect.IsChecked = True AndAlso cbgLoc.CheckedValue.Count > 0 Then
                strQuery += " and  TSPL_ACQUISITION_HEAD.Loc_Code in (" + clsCommon.GetMulcallString(cbgLoc.CheckedValue) + ") "
            ElseIf chkAssetSelect.IsChecked = True AndAlso cbgAsset.CheckedValue.Count > 0 Then
                strQuery += " and  TSPL_ACQUISITION_DETAIL.Asset_Code in (" + clsCommon.GetMulcallString(cbgAsset.CheckedValue) + ") "
            ElseIf chkCategSelect.IsChecked = True AndAlso cbgCategory.CheckedValue.Count > 0 Then
                strQuery += " and  TSPL_ACQUISITION_DETAIL.Category_code in (" + clsCommon.GetMulcallString(cbgCategory.CheckedValue) + ") "
            ElseIf chkCostCentSelect.IsChecked = True AndAlso cbgCostCenter.CheckedValue.Count <= 0 Then
                strQuery += " and  TSPL_ACQUISITION_DETAIL.CostCenter_Code in (" + clsCommon.GetMulcallString(cbgCostCenter.CheckedValue) + ") "
            ElseIf chkGroupSelect.IsChecked = True AndAlso cbgGroup.CheckedValue.Count <= 0 Then
                strQuery += " and  TSPL_ACQUISITION_DETAIL.Group_Code in (" + clsCommon.GetMulcallString(cbgGroup.CheckedValue) + ") "
            ElseIf chkVendorSelect.IsChecked = True AndAlso cbgVendor.CheckedValue.Count <= 0 Then
                strQuery += " and  TSPL_ACQUISITION_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ") "
            End If
            strQuery += "group by TSPL_ACQUISITION_DETAIL.Asset_Code,Asset_Name,Asset_Specification, "
            strQuery += "Acquisition_Date, Location_Desc, CostCenter_Name, TSPL_ASSET_CATEGORY.Description, TSPL_ASSET_GROUP.Description, Vendor_Name, TSPL_ACQUISITION_DETAIL.Start_Date ,TSPL_ASSET_SCRAP_HEAD.Document_Date"

            'If rdbSummary.IsChecked Then
            '    strQuery = "select Location_Desc,CostCenter_Name,CatDesc,GrpDesc,Vendor_Name,SUM(BookValue) as BookValue,SUM(YTDDep) as YTDDep, " & _
            '    "SUM(TotalDep) as  TotalDep,SUM(Net) as Net  from (" & strQuery & ") aaa where Net >0 group by  Location_Desc,CostCenter_Name,CatDesc,GrpDesc,Vendor_Name"

            'End If

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
            Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            If chkAssetSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgAsset.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Asset : " + strtemp)
            End If
            If chkCategSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgCategory.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Category : " + strtemp)
            End If

            If chkLocationSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgLoc.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Location  : " + strtemp)
            End If


            'If rbtnCompanySelect.IsChecked Then
            '    strtemp = ""
            '    For Each Str As String In cbgCompany.CheckedDisplayMember
            '        If clsCommon.myLen(strtemp) > 0 Then
            '            strtemp += ", "
            '        End If
            '        strtemp += Str
            '    Next
            '    arrHeader.Add("Company : " + strtemp)
            'End If
            ' clsCommon.MyExportToExcel("Salesman Shortage" + IIf(rdbDetail.IsChecked, " ( Detail ) ", " ( Summary ) "), gv1, arrHeader, Me.Text)

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcel("Disposal Details", GV1, arrHeader, Me.Text)
            ElseIf exporter = EnumExportTo.PDF Then
                clsCommon.MyExportToPDF("Disposal Details", GV1, arrHeader, "Disposal Detail Report", True)
            End If

        Catch ex As Exception
            RadMessageBox.Show(ex.Message, Me.Text)
        End Try
    End Sub
    Sub SetGridFormationOFGV1()
        ' Dim strItemCode, head2 As String

        GV1.TableElement.TableHeaderHeight = 40
        GV1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To GV1.Columns.Count - 1
            GV1.Columns(ii).ReadOnly = True
            GV1.Columns(ii).IsVisible = False
        Next


        GV1.Columns("Asset_Code").IsVisible = True
        GV1.Columns("Asset_Code").Width = 120
        GV1.Columns("Asset_Code").HeaderText = "Asset Code"

        GV1.Columns("Asset_Name").IsVisible = True
        GV1.Columns("Asset_Name").Width = 120
        GV1.Columns("Asset_Name").HeaderText = "Asset Name"

        GV1.Columns("Asset_Specification").IsVisible = True
        GV1.Columns("Asset_Specification").Width = 120
        GV1.Columns("Asset_Specification").HeaderText = "Asset Specs"

        GV1.Columns("Acquisition_Date").IsVisible = True
        GV1.Columns("Acquisition_Date").Width = 120
        GV1.Columns("Acquisition_Date").HeaderText = "Acquisition Date"

        GV1.Columns("Disposal Date").IsVisible = True
        GV1.Columns("Disposal Date").Width = 120
        GV1.Columns("Disposal Date").HeaderText = "Disposal Date"



        GV1.Columns("Location_Desc").IsVisible = True
        GV1.Columns("Location_Desc").Width = 120
        GV1.Columns("Location_Desc").HeaderText = "Location"

        GV1.Columns("CostCenter_Name").IsVisible = True
        GV1.Columns("CostCenter_Name").Width = 120
        GV1.Columns("CostCenter_Name").HeaderText = "CostCenter"


        GV1.Columns("CatDesc").IsVisible = True
        GV1.Columns("CatDesc").Width = 120
        GV1.Columns("CatDesc").HeaderText = "Category"


        GV1.Columns("GrpDesc").IsVisible = True
        GV1.Columns("GrpDesc").Width = 120
        GV1.Columns("GrpDesc").HeaderText = "Group"


        GV1.Columns("Vendor_Name").IsVisible = True
        GV1.Columns("Vendor_Name").Width = 120
        GV1.Columns("Vendor_Name").HeaderText = "Vendor"


        GV1.Columns("BookValue").IsVisible = True
        GV1.Columns("BookValue").Width = 120
        GV1.Columns("BookValue").HeaderText = "Book Value"

        GV1.Columns("Disposal Value").IsVisible = True
        GV1.Columns("Disposal Value").Width = 120
        GV1.Columns("Disposal Value").HeaderText = "Disposal Value"

        GV1.Columns("Tax").IsVisible = True
        GV1.Columns("Tax").Width = 120
        GV1.Columns("Tax").HeaderText = "Tax"

        GV1.Columns("Total Disposal Value").IsVisible = True
        GV1.Columns("Total Disposal Value").Width = 120
        GV1.Columns("Total Disposal Value").HeaderText = "Total Disposal Value"



        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0


        Dim item8 As New GridViewSummaryItem("BookValue", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)

        Dim item9 As New GridViewSummaryItem("Disposal Value", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item9)
        Dim item10 As New GridViewSummaryItem("Tax", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item10)
        Dim item11 As New GridViewSummaryItem("Total Disposal Value", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item11)


        GV1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = GV1
        Print(EnumExportTo.Refresh)
    End Sub

    Public Enum EnumExportTo
        Excel = 0
        PDF = 1
        Refresh = 2
    End Enum

    Private Sub FrmDisposalDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        reset()
        SetUserMgmtNew()
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
    End Sub

    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            Dim strtemp As String = ""
            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmDisposalDetail & "'"))
            'If chkLocationSelect.IsChecked Then
            '    Dim strLocationName As String = ""
            '    For Each StrName As String In cbgLoc.CheckedDisplayMember
            '        If clsCommon.myLen(strLocationName) > 0 Then
            '            strLocationName += ", "
            '        End If
            '        strLocationName += StrName
            '    Next
            '    Dim strLocationCode As String = ""
            '    For Each StrCode As String In cbgLoc.CheckedValue
            '        If clsCommon.myLen(strLocationCode) > 0 Then
            '            strLocationCode += ", "
            '        End If
            '        strLocationCode += StrCode
            '    Next
            '    arrHeader.Add(("Location: " + strLocationName + " "))
            'End If
            If chkAssetSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgAsset.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Asset : " + strtemp)
            End If
            If chkCategSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgCategory.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Category : " + strtemp)
            End If

            If chkLocationSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgLoc.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Location  : " + strtemp)
            End If


            If chkCostCentSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgCostCenter.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Cost Center  : " + strtemp)
            End If

            If chkGroupSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgGroup.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Group  : " + strtemp)
            End If

            If chkVendorSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgVendor.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Vendor  : " + strtemp)
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
                clsCommon.MyExportToPDF("Disposal Details", GV1, arrHeader, "Disposal Detail Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub rmExcel_Click(sender As Object, e As EventArgs) Handles rmExcel.Click
        If (GV1.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow("No Data To Export")
            Exit Sub
        End If
        'Print(EnumExportTo.Excel)
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        If (GV1.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow("No Data To Export")
            Exit Sub
        End If
        'Print(EnumExportTo.PDF)
        ExportGrid(EnumExportTo.PDF)
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
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub
End Class
