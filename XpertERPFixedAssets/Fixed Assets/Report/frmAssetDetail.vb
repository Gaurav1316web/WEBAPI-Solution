'-Updation By --[Pankaj Kumar Chaudhary]--Against Ticket No-[BM00000001521]
'' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''

Imports common
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports System.IO

Public Class FrmAssetDetail
    Inherits FrmMainTranScreen
    Dim ReadOnlyTemplateFieldsOnAcqusition As Boolean = False
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FrmAssetDetail)
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
        Dim strquery As String = "select CostCenter_Code as Code,CostCenter_Name as Description FROM TSPL_FA_COST_CENTER_MASTER WHERE 2=2 "
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


    'Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
    '    cbgLoc.Enabled = Not chkLocationAll.IsChecked
    'End Sub

    'Private Sub chkAssetAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkAssetAll.ToggleStateChanged
    '    cbgAsset.Enabled = Not chkAssetAll.IsChecked
    'End Sub

    'Private Sub chkCategAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCategAll.ToggleStateChanged
    '    cbgCategory.Enabled = Not chkCategAll.IsChecked
    'End Sub

    'Private Sub chkCostCentAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCostCentAll.ToggleStateChanged
    '    cbgCostCenter.Enabled = Not chkCostCentAll.IsChecked
    'End Sub

    'Private Sub chkGroupAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkGroupAll.ToggleStateChanged
    '    cbgGroup.Enabled = Not chkGroupAll.IsChecked
    'End Sub

    'Private Sub chkVendorAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVendorAll.ToggleStateChanged
    '    cbgVendor.Enabled = Not chkVendorAll.IsChecked
    'End Sub


    Private Sub Print(ByVal exporter As EnumExportTo)
        Try
            ' Dim strInvItem, strRetItem, strSeq As String
            'If chkLocationSelect.IsChecked = True AndAlso cbgLoc.CheckedValue.Count <= 0 Then
            '    RadMessageBox.Show("Please select at least one Location or select ALL")
            '    Return
            'ElseIf chkAssetSelect.IsChecked = True AndAlso cbgAsset.CheckedValue.Count <= 0 Then
            '    RadMessageBox.Show("Please select at least one Asset or select ALL")
            '    Return
            'ElseIf chkCategSelect.IsChecked = True AndAlso cbgCategory.CheckedValue.Count <= 0 Then
            '    RadMessageBox.Show("Please select at least one Category or select ALL")
            '    Return
            'ElseIf chkCostCentSelect.IsChecked = True AndAlso cbgCostCenter.CheckedValue.Count <= 0 Then
            '    RadMessageBox.Show("Please select at least one Cost Centre or select ALL")
            '    Return
            'ElseIf chkGroupSelect.IsChecked = True AndAlso cbgGroup.CheckedValue.Count <= 0 Then
            '    RadMessageBox.Show("Please select at least one Group or select ALL")
            '    Return
            'ElseIf chkVendorSelect.IsChecked = True AndAlso cbgVendor.CheckedValue.Count <= 0 Then
            '    RadMessageBox.Show("Please select at least one Vendor or select ALL")
            '    Return

            'End If

            GV1.EnableFiltering = True
            Dim dt As DataTable
            Dim strFromDateTime As String = clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy")
            Dim strToDateTime As String = clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy")

            'Dim strQuery As String = "select TSPL_ACQUISITION_DETAIL.Asset_Code,Asset_Name,Asset_Specification,Acquisition_Date,Location_Desc, " & _
            '"CostCenter_Name,TSPL_ASSET_CATEGORY.Description as CatDesc,TSPL_ASSET_GROUP.Description  as GrpDesc,Vendor_Name,convert(varchar,TSPL_ACQUISITION_DETAIL.Start_Date,103) as Start_Date, " & _
            '"max(Book_Source_value) as BookValue " & _
            '        "from TSPL_ACQUISITION_HEAD left outer join TSPL_ACQUISITION_DETAIL on " & _
            '"TSPL_ACQUISITION_HEAD.Acquisition_Code=TSPL_ACQUISITION_DETAIL.Acquisition_Code  " & _
            '"left outer join TSPL_LOCATION_MASTER on TSPL_ACQUISITION_HEAD.Loc_Code=TSPL_LOCATION_MASTER.Location_Code  " & _
            '"left outer join TSPL_FA_COST_CENTER_MASTER on TSPL_ACQUISITION_DETAIL.CostCenter_Code=TSPL_FA_COST_CENTER_MASTER.CostCenter_Code  " & _
            '"left outer join TSPL_ASSET_CATEGORY on TSPL_ACQUISITION_DETAIL.Category_code=TSPL_ASSET_CATEGORY.Category_Code " & _
            '"left outer join TSPL_ASSET_GROUP on TSPL_ACQUISITION_DETAIL.Group_Code=TSPL_ASSET_GROUP.Group_Code " & _
            '"left outer join TSPL_VENDOR_MASTER on TSPL_ACQUISITION_HEAD.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code " & _
            '"where Acquisition_Date >=  '" & strFromDateTime & "' AND " & _
            '"Acquisition_Date <=  '" & strToDateTime & "'   "
            'Dim qry As String = clsAcquisitionHead.GetAssetQuery
            'If rdbBook.IsChecked Then
            '    strQuery += " and isnull( (select MIN(Asset_value) from TSPL_ASSET_DEPRECIATION    where Asset_Code=TSPL_ACQUISITION_DETAIL.Asset_Code and Document_Date <= '" + strToDateTime + "'),0)>0"
            'ElseIf rdbTax.IsChecked Then
            '    strQuery += " and  isnull( (select MIN(Asset_value_Tax) from TSPL_ASSET_DEPRECIATION    where Asset_Code=TSPL_ACQUISITION_DETAIL.Asset_Code and Document_Date <= '" + strToDateTime + "'),0)>0"

            'End If
            Dim qry As String = clsAcquisitionDetail.GetAssetQuery()
            qry = qry & " and convert(date,ACQ.Acquisition_Date,103) >=  convert(date,'" & fromDate.Value & "',103) AND convert(date,ACQ.Acquisition_Date,103) <=  convert(date,'" & ToDate.Value & "',103)   "

            If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
                qry += " and  ACQ.Loc_Code in (" + clsCommon.GetMulcallString(TxtMultiLocation.arrValueMember) + ") " + Environment.NewLine
            End If
            If TxtMultiAssetId.arrValueMember IsNot Nothing AndAlso TxtMultiAssetId.arrValueMember.Count > 0 Then
                qry += " and  ACQD.Asset_Code in  (" + clsCommon.GetMulcallString(TxtMultiAssetId.arrValueMember) + ") " + Environment.NewLine
            End If
            If TxtMultiCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCategory.arrValueMember.Count > 0 Then
                qry += " and  ACQD.Category_code in (" + clsCommon.GetMulcallString(TxtMultiCategory.arrValueMember) + ") " + Environment.NewLine
            End If
            If TxtMultiCostCenter.arrValueMember IsNot Nothing AndAlso TxtMultiCostCenter.arrValueMember.Count > 0 Then
                qry += " and  ACQD.CostCenter_Code in (" + clsCommon.GetMulcallString(TxtMultiCostCenter.arrValueMember) + ") " + Environment.NewLine
            End If
            If TxtMultiGroup.arrValueMember IsNot Nothing AndAlso TxtMultiGroup.arrValueMember.Count > 0 Then
                qry += " and  ACQD.Group_Code in (" + clsCommon.GetMulcallString(TxtMultiGroup.arrValueMember) + ") " + Environment.NewLine
            End If
            If TxtMultiVendor.arrValueMember IsNot Nothing AndAlso TxtMultiVendor.arrValueMember.Count > 0 Then
                qry += " and  ACQ.Vendor_Code in (" + clsCommon.GetMulcallString(TxtMultiVendor.arrValueMember) + ") " + Environment.NewLine
            End If
            'qry += "group by TSPL_ACQUISITION_DETAIL.Asset_Code,Asset_Name,Asset_Specification, "
            'strQuery += "Acquisition_Date, Location_Desc, CostCenter_Name, TSPL_ASSET_CATEGORY.Description, TSPL_ASSET_GROUP.Description, Vendor_Name,TSPL_ACQUISITION_DETAIL. Start_Date "

            'If rdbSummary.IsChecked Then
            '    strQuery = "select Location_Desc,CostCenter_Name,CatDesc,GrpDesc,Vendor_Name,SUM(BookValue) as BookValue,SUM(YTDDep) as YTDDep, " & _
            '    "SUM(TotalDep) as  TotalDep,SUM(Net) as Net  from (" & strQuery & ") aaa where Net >0 group by  Location_Desc,CostCenter_Name,CatDesc,GrpDesc,Vendor_Name"

            'End If

            dt = clsDBFuncationality.GetDataTable(qry)
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
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            If TxtMultiLocation.arrDispalyMember IsNot Nothing AndAlso TxtMultiLocation.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(TxtMultiLocation.arrDispalyMember))
            End If
            If TxtMultiAssetId.arrDispalyMember IsNot Nothing AndAlso TxtMultiAssetId.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Asset Id: " + clsCommon.GetMulcallStringWithComma(TxtMultiAssetId.arrDispalyMember))
            End If
            If TxtMultiCategory.arrDispalyMember IsNot Nothing AndAlso TxtMultiCategory.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Category : " + clsCommon.GetMulcallStringWithComma(TxtMultiCategory.arrDispalyMember))
            End If
            If TxtMultiCostCenter.arrDispalyMember IsNot Nothing AndAlso TxtMultiCostCenter.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Cost Center: " + clsCommon.GetMulcallStringWithComma(TxtMultiCostCenter.arrDispalyMember))
            End If
            If TxtMultiGroup.arrDispalyMember IsNot Nothing AndAlso TxtMultiGroup.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Group : " + clsCommon.GetMulcallStringWithComma(TxtMultiGroup.arrDispalyMember))
            End If
            If TxtMultiVendor.arrDispalyMember IsNot Nothing AndAlso TxtMultiVendor.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Vendor : " + clsCommon.GetMulcallStringWithComma(TxtMultiVendor.arrDispalyMember))
            End If

            'If chkAssetSelect.IsChecked Then
            '    strtemp = ""
            '    For Each Str As String In cbgAsset.CheckedDisplayMember
            '        If clsCommon.myLen(strtemp) > 0 Then
            '            strtemp += ", "
            '        End If
            '        strtemp += Str
            '    Next
            '    arrHeader.Add("Asset : " + strtemp)
            'End If
            'If chkCategSelect.IsChecked Then
            '    strtemp = ""
            '    For Each Str As String In cbgCategory.CheckedDisplayMember
            '        If clsCommon.myLen(strtemp) > 0 Then
            '            strtemp += ", "
            '        End If
            '        strtemp += Str
            '    Next
            '    arrHeader.Add("Category : " + strtemp)
            'End If

            'If chkLocationSelect.IsChecked Then
            '    strtemp = ""
            '    For Each Str As String In cbgLoc.CheckedDisplayMember
            '        If clsCommon.myLen(strtemp) > 0 Then
            '            strtemp += ", "
            '        End If
            '        strtemp += Str
            '    Next
            '    arrHeader.Add("Location  : " + strtemp)
            'End If


            ''If rbtnCompanySelect.IsChecked Then
            ''    strtemp = ""
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
                clsCommon.MyExportToExcel("Asset Details", GV1, arrHeader, Me.Text)
            ElseIf exporter = EnumExportTo.PDF Then
                clsCommon.MyExportToPDF("Asset Details", GV1, arrHeader, "Asset Details Report", True)
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

        GV1.Columns("Start_Date").IsVisible = True
        GV1.Columns("Start_Date").Width = 120
        GV1.Columns("Start_Date").HeaderText = "Life Start Date"



        GV1.Columns("Location_Desc").IsVisible = True
        GV1.Columns("Location_Desc").Width = 120
        GV1.Columns("Location_Desc").HeaderText = "Location"

        GV1.Columns("CostCenter_Name").IsVisible = True
        GV1.Columns("CostCenter_Name").Width = 120
        GV1.Columns("CostCenter_Name").HeaderText = "CostCenter"


        GV1.Columns("CatDesc").IsVisible = True
        GV1.Columns("CatDesc").Width = 120
        GV1.Columns("CatDesc").HeaderText = If(ReadOnlyTemplateFieldsOnAcqusition = True, "Asset Group Name", "Category")


        GV1.Columns("GrpDesc").IsVisible = True
        GV1.Columns("GrpDesc").Width = 120
        GV1.Columns("GrpDesc").HeaderText = If(ReadOnlyTemplateFieldsOnAcqusition = True, "Sub Group Name", "Group")


        GV1.Columns("Vendor_Name").IsVisible = True
        GV1.Columns("Vendor_Name").Width = 120
        GV1.Columns("Vendor_Name").HeaderText = "Vendor"


        GV1.Columns("BookValue").IsVisible = True
        GV1.Columns("BookValue").Width = 120
        GV1.Columns("BookValue").HeaderText = "Book Value"

        GV1.Columns("Opening Depreciation").IsVisible = True
        GV1.Columns("Opening Depreciation").Width = 120
        GV1.Columns("Opening Depreciation").HeaderText = "Opening Depreciation"

        GV1.Columns("Final_Dep_Amount").IsVisible = True
        GV1.Columns("Final_Dep_Amount").Width = 120
        GV1.Columns("Final_Dep_Amount").HeaderText = "Depreciation Amount"

        GV1.Columns("Total Final Depreeciated").IsVisible = True
        GV1.Columns("Total Final Depreeciated").Width = 120
        GV1.Columns("Total Final Depreeciated").HeaderText = "Total Depreciation"

        GV1.Columns("Addition_Amount").IsVisible = True
        GV1.Columns("Addition_Amount").Width = 120
        GV1.Columns("Addition_Amount").HeaderText = "Additional Amount"

        GV1.Columns("Asset_Value").IsVisible = True
        GV1.Columns("Asset_Value").Width = 120
        GV1.Columns("Asset_Value").HeaderText = "Asset Value"



        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0


        Dim item1 As New GridViewSummaryItem("BookValue", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        Dim item2 As New GridViewSummaryItem("Final_Dep_Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)

        Dim item3 As New GridViewSummaryItem("Addition_Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)

        Dim item4 As New GridViewSummaryItem("Asset_Value", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)

        Dim item5 As New GridViewSummaryItem("Opening Depreciation", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)

        Dim item6 As New GridViewSummaryItem("Total Final Depreeciated", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)

        'Dim item9 As New GridViewSummaryItem("YTDDep", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item9)
        'Dim item10 As New GridViewSummaryItem("TotalDep", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item10)
        'Dim item11 As New GridViewSummaryItem("Net", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item11)


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




    Private Sub FrmAssetDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        reset()
    End Sub
    Sub reset()
        '' if below setting is on then 
        '' 1. Rename FA Template Master to Asset Category
        '' 2. Rename Asset Category Master to Asset Group
        '' 3. Rename Asset Group Master to Sub Group Master
        ReadOnlyTemplateFieldsOnAcqusition = If(clsFixedParameter.GetData(clsFixedParameterCode.ReadOnlyTemplateFieldsOnAcqusition, clsFixedParameterType.ReadOnlyTemplateFieldsOnAcqusition, Nothing) = 1, True, False)
        If ReadOnlyTemplateFieldsOnAcqusition Then
            lblcategory.Text = "Asset Group"
            lblGroup.Text = "Asset Sub Group"
        End If
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        'chkAssetAll.IsChecked = True
        'chkCategAll.IsChecked = True
        'chkVendorAll.IsChecked = True
        'chkGroupAll.IsChecked = True
        'chkLocationAll.IsChecked = True
        'chkCostCentAll.IsChecked = True
        'LoadAssetId()
        'LoadLocation()
        'LoadCategory()
        'LoadCostCenter()
        'LoadGroup()
        'LoadVendor()
        SetUserMgmtNew()
        'rdbBook.IsChecked = True
        'rdbDetail.IsChecked = True
    End Sub
    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        'Print(EnumExportTo.Excel)
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        'Print(EnumExportTo.PDF)
        ExportGrid(EnumExportTo.PDF)
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
    End Sub

    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmAssetDetail & "'"))
            If TxtMultiLocation.arrDispalyMember IsNot Nothing AndAlso TxtMultiLocation.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(TxtMultiLocation.arrDispalyMember))
            End If
            If TxtMultiAssetId.arrDispalyMember IsNot Nothing AndAlso TxtMultiAssetId.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Asset Id: " + clsCommon.GetMulcallStringWithComma(TxtMultiAssetId.arrDispalyMember))
            End If
            If TxtMultiCategory.arrDispalyMember IsNot Nothing AndAlso TxtMultiCategory.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Category : " + clsCommon.GetMulcallStringWithComma(TxtMultiCategory.arrDispalyMember))
            End If
            If TxtMultiCostCenter.arrDispalyMember IsNot Nothing AndAlso TxtMultiCostCenter.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Cost Center: " + clsCommon.GetMulcallStringWithComma(TxtMultiCostCenter.arrDispalyMember))
            End If
            If TxtMultiGroup.arrDispalyMember IsNot Nothing AndAlso TxtMultiGroup.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Group : " + clsCommon.GetMulcallStringWithComma(TxtMultiGroup.arrDispalyMember))
            End If
            If TxtMultiVendor.arrDispalyMember IsNot Nothing AndAlso TxtMultiVendor.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Vendor : " + clsCommon.GetMulcallStringWithComma(TxtMultiVendor.arrDispalyMember))
            End If

            'If chkAssetSelect.IsChecked Then
            '    Dim strLocationName As String = ""
            '    For Each StrName As String In cbgAsset.CheckedDisplayMember
            '        If clsCommon.myLen(strLocationName) > 0 Then
            '            strLocationName += ", "
            '        End If
            '        strLocationName += StrName
            '    Next
            '    Dim strLocationCode As String = ""
            '    For Each StrCode As String In cbgAsset.CheckedValue
            '        If clsCommon.myLen(strLocationCode) > 0 Then
            '            strLocationCode += ", "
            '        End If
            '        strLocationCode += StrCode
            '    Next
            '    arrHeader.Add(("Asset: " + strLocationName + " "))
            'End If
            'If chkCategSelect.IsChecked Then
            '    Dim strLocationName As String = ""
            '    For Each StrName As String In cbgCategory.CheckedDisplayMember
            '        If clsCommon.myLen(strLocationName) > 0 Then
            '            strLocationName += ", "
            '        End If
            '        strLocationName += StrName
            '    Next
            '    Dim strLocationCode As String = ""
            '    For Each StrCode As String In cbgCategory.CheckedValue
            '        If clsCommon.myLen(strLocationCode) > 0 Then
            '            strLocationCode += ", "
            '        End If
            '        strLocationCode += StrCode
            '    Next
            '    arrHeader.Add(("Category: " + strLocationName + " "))
            'End If

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
            ElseIf exporter = EnumExportTo.PDF Then
                transportSql.applyExportTemplate(GV1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Asset Details", GV1, arrHeader, "Asset Details Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtMultiLocation__My_Click(sender As Object, e As EventArgs) Handles TxtMultiLocation._My_Click
        Dim frm As New FrmPendingRequisitionQty()
        Dim qry As String = " select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        TxtMultiLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("MulLoc1", qry, "Code", "Name", TxtMultiLocation.arrValueMember, TxtMultiLocation.arrDispalyMember)
        frm.SetDiplayMember(TxtMultiLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub

    Private Sub TxtMultiCostCenter__My_Click(sender As Object, e As EventArgs) Handles TxtMultiCostCenter._My_Click
        Dim qry As String = ""
        If clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ApplyFinancialCostCenter, clsFixedParameterCode.ApplyFinancialCostCenter, Nothing)) = "1", True, False)) = True Then
            qry = "select Cost_Center_Fin_Code as Code,Cost_Center_Fin_Name as Name FROM TSPL_COST_CENTRE_FINANCIAL"
        Else
            qry = "select CostCenter_Code as Code,CostCenter_Name as Name FROM TSPL_FA_COST_CENTER_MASTER"
        End If

        TxtMultiCostCenter.arrValueMember = clsCommon.ShowMultipleSelectForm("MulCost1", qry, "Code", "Name", TxtMultiCostCenter.arrValueMember, TxtMultiCostCenter.arrDispalyMember)
    End Sub

    Private Sub TxtMultiAssetId__My_Click(sender As Object, e As EventArgs) Handles TxtMultiAssetId._My_Click
        Dim qry As String = "select Asset_Code as Code,Asset_Name as Name FROM TSPL_ACQUISITION_DETAIL"
        TxtMultiAssetId.arrValueMember = clsCommon.ShowMultipleSelectForm("MulAsset", qry, "Code", "Name", TxtMultiAssetId.arrValueMember, TxtMultiAssetId.arrDispalyMember)
    End Sub

    Private Sub TxtMultiGroup__My_Click(sender As Object, e As EventArgs) Handles TxtMultiGroup._My_Click
        Dim qry As String = "select Group_Code as Code,Description as Name FROM TSPL_ASSET_GROUP"
        TxtMultiGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("MulGroup", qry, "Code", "Name", TxtMultiGroup.arrValueMember, TxtMultiGroup.arrDispalyMember)
    End Sub

    Private Sub TxtMultiCategory__My_Click(sender As Object, e As EventArgs) Handles TxtMultiCategory._My_Click
        Dim qry As String = "select Category_Code as Code,Description as Name FROM TSPL_ASSET_CATEGORY"
        TxtMultiCategory.arrValueMember = clsCommon.ShowMultipleSelectForm("MulCategory", qry, "Code", "Name", TxtMultiCategory.arrValueMember, TxtMultiCategory.arrDispalyMember)
    End Sub

    Private Sub TxtMultiVendor__My_Click(sender As Object, e As EventArgs) Handles TxtMultiVendor._My_Click
        Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name FROM TSPL_VENDOR_MASTER WHERE Status='N' "
        TxtMultiVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("MulVendor", qry, "Code", "Name", TxtMultiVendor.arrValueMember, TxtMultiVendor.arrDispalyMember)
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
End Class
