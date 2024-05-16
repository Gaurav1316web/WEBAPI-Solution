'for bug no BM00000000885
'' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
Imports common
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports System.IO

Public Class FrmAssetRegister
    Inherits FrmMainTranScreen
    Dim ReadOnlyTemplateFieldsOnAcqusition As Boolean = False
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FrmAssetRegister)
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

    Private Sub FrmAssetRegister_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '' if below setting is on then 
        '' 1. Rename FA Template Master to Asset Category
        '' 2. Rename Asset Category Master to Asset Group
        '' 3. Rename Asset Group Master to Sub Group Master
        ReadOnlyTemplateFieldsOnAcqusition = If(clsFixedParameter.GetData(clsFixedParameterCode.ReadOnlyTemplateFieldsOnAcqusition, clsFixedParameterType.ReadOnlyTemplateFieldsOnAcqusition, Nothing) = 1, True, False)
        If ReadOnlyTemplateFieldsOnAcqusition Then
            lblCategory.Text = "Asset Group"
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
        rdbBook.IsChecked = True
        rdbSummary.IsChecked = True
    End Sub
    Private Sub Print_OLD_BACKUP(ByVal exporter As EnumExportTo)
        Try
            GV1.EnableFiltering = True
            Dim dt As DataTable
            Dim strFromDateTime As String = clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm tt")
            Dim strToDateTime As String = clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm tt")
            Dim qry As String = clsAcquisitionDetail.GetAssetQuery()
            qry = qry & " and Convert (date, ACQ.Acquisition_Date) >= Convert (date, '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") & "',103) AND Convert(date, ACQ.Acquisition_Date,103) <= Convert(date, '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") & "' ,103)  "
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

            If rdbSummary.IsChecked Then
                If rdbBook.CheckState = CheckState.Checked Then
                    qry = "select Book.Asset_Code,Book.Asset_Name,Book.Asset_Specification,Book.Loc_Code,Book.Vendor_Code,Book.Vendor_Name,Book.Location_Desc,Book.Acquisition_Date,Book.Acquisition_Code,Book.Templete_Code,Book.Category_code,Book.CatDesc, " &
                            " Book.Group_Code,Book.GrpDesc ,Book.AcSet_Code,Book.CostCenter_Code,Book.CostCenter_Name,Book.Dep_Method_Code,Book.Dep_Period_Code,Book.Start_Date,Book.Dep_Rate," &
                            " Book.Dep_Tax_Rate,Book.Book_Estimated_Life,Book.BookValue,Book.OriginalBookValue,Book.SRN_No,(isnull((select sum(Dep_Amount)  from TSPL_ASSET_DEPRECIATION as inTable where inTable.asset_code=Book.Asset_Code and document_date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'),0)) as Dep_Amount,Book.Final_Dep_rate as DepRate,Book.Addition_Amount as Additional_Amount,Book.Asset_Value-isnull((select sum(Dep_Amount)  from TSPL_ASSET_DEPRECIATION as inTable where inTable.asset_code=Book.Asset_Code and document_date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'),0)  as Asset_Value " &
                            " from (" & qry & ") as Book order by convert (date,Book.Acquisition_Date,103), Book.Asset_Code"
                ElseIf rdbTax.CheckState = CheckState.Checked Then
                    qry = "select Book.Asset_Code,Book.Asset_Name,Book.Asset_Specification,Book.Loc_Code,Book.Vendor_Code,Book.Vendor_Name,Book.Location_Desc,Book.Acquisition_Date,Book.Acquisition_Code,Book.Templete_Code,Book.Category_code,Book.CatDesc, " &
                           " Book.Group_Code,Book.GrpDesc ,Book.AcSet_Code,Book.CostCenter_Code,Book.CostCenter_Name,Book.Dep_Method_Code,Book.Dep_Period_Code,Book.Start_Date,Book.Dep_Rate," &
                           " Book.Dep_Tax_Rate,Book.Book_Estimated_Life,Book.BookValue,Book.OriginalBookValue,Book.SRN_No,isnull((select sum(Dep_Amount)  from TSPL_ASSET_DEPRECIATION as inTable where inTable.asset_code=Book.Asset_Code and document_date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'),0) as Dep_Amount,Book.Final_Dep_rate as DepRate,Book.Addition_Amount as Additional_Amount,Book.Asset_Value_Tax-isnull((select sum(Dep_Amount)  from TSPL_ASSET_DEPRECIATION as inTable where inTable.asset_code=Book.Asset_Code and document_date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'),0)  as Asset_Value as Asset_Value " &
                           " from (" & qry & ") as Book order by convert (date,Book.Acquisition_Date,103),Book.Asset_Code"
                End If
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
            ElseIf rdbDetail.IsChecked Then
                If rdbBook.CheckState = CheckState.Checked Then
                    qry = "  select XXXfinal.Asset_Code,XXXfinal.Asset_Name, XXXfinal.Asset_Specification, XXXfinal.Loc_code, XXXfinal.Vendor_Code, XXXfinal.Location_Desc,XXXfinal.Acquisition_Date, XXXfinal.Acquisition_Code, XXXfinal.Templete_Code,XXXfinal.Category_Code,XXXfinal.CatDesc,XXXfinal.Group_Code,XXXfinal.GrpDesc ,XXXfinal.Vendor_Name,XXXfinal.AcSet_Code,XXXfinal.CostCenter_Code,XXXfinal.CostCenter_Name,XXXfinal.Dep_Method_Code,XXXfinal.Dep_period_Code,XXXfinal.Start_Date,XXXfinal.Dep_Rate,XXXfinal.Dep_Tax_Rate, XXXfinal.Book_Estimated_Life,XXXfinal.OriginalBookValue,XXXfinal.SRN_No,XXXfinal.[Document Date], XXXfinal.BookValue,XXXfinal.[Opening Depreciation],case when XXXfinal.SNO = 1 then XXXfinal.[Opening_Value(Book Vlue -Opening Depreciation)] else TBL_Asset_Value_Cal.[asset value new] end  as Opening_Value, XXXfinal.Additional_Amount, XXXfinal.Dep_Amount, XXXfinal.[total des new] as [Total Depreciation],XXXfinal.DepRate, case when XXXfinal.SNO = 1 then XXXfinal.[asset value new] else TBL_Asset_Value_Cal.[asset value new] - XXXfinal.dep_amount   end as 'Asset_Value' from ( " &
                          "  Select ROW_NUMBER() OVER(PARTITION BY XFinal.Asset_Code ORDER BY XFinal.Asset_Code,DepBook.Document_Date  ASC) as SNO, XFinal.Asset_Code,XFinal.Asset_Name,XFinal.Asset_Specification, XFinal.Loc_Code, XFinal.Vendor_Code,XFinal. Location_Desc, XFinal.Acquisition_Date,XFinal. Acquisition_Code, XFinal.Templete_Code, XFinal.Category_code, XFinal.CatDesc," &
                          "  XFinal.Group_Code,XFinal.GrpDesc ,XFinal.Vendor_Name,XFinal.AcSet_Code,XFinal.CostCenter_Code,XFinal.CostCenter_Name,XFinal.Dep_Method_Code,XFinal.Dep_Period_Code,XFinal.Start_Date,XFinal.Dep_Rate, XFinal.Dep_Tax_Rate,XFinal.Book_Estimated_Life,XFinal.Asset_Value as BookValue,XFinal.OriginalBookValue,XFinal.SRN_No,DepBook.Document_Date as [Document Date],XFinal.Asset_Value - XFinal.[Opening Depreciation] as 'Opening_Value(Book Vlue -Opening Depreciation)' ,DepBook.Additional_Amount,case when   ROW_NUMBER() OVER(PARTITION BY XFinal.Asset_Code ORDER BY XFinal.Asset_Code,DepBook.Document_Date ASC)  = 1 then  XFinal.[Opening Depreciation]  else 0 end [Opening Depreciation] " &
                          " ,DepBook.Dep_Amount,depbook.dep_amount+XFinal.[Opening Depreciation] as [Total Depreciation],case when ROW_NUMBER() OVER(PARTITION BY XFinal.Asset_Code ORDER BY XFinal.Asset_Code,DepBook.Document_Date  ASC)  = 1 then  XFinal.[Opening Depreciation]  else 0 end+ dep_amount as [total des new],isnull(DepBook.DepRate,0) as DepRate,XFinal.Asset_Value- (depbook.dep_amount+XFinal.[Opening Depreciation]) as Asset_Value ,XFinal.Asset_Value - XFinal.[Opening Depreciation] - dep_amount as 'asset value new' " &
                          "  from ( " &
                          " " + qry + " " &
                          "  ) XFinal " &
                          "  left outer Join " &
                          "  (select Document_Code,Document_Date,Asset_Code,Opening_Value,Work_Expense as Additional_Amount,Dep_Amount,deprate,Asset_value from ( select Document_Code,Document_Date,Asset_Code,Opening_Value,Work_Expense,Dep_Amount,deprate,Asset_value, Opening_Value_Tax,Dep_Amount_Tax,Asset_value_Tax,Is_Permanent,Is_Reverse_Dep from TSPL_ASSET_DEPRECIATION  where TSPL_ASSET_DEPRECIATION.Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' ) DepBook) DepBook on XFinal.Asset_Code = DepBook.Asset_Code " &
                          "  )XXXFinal " &
                          "   left outer join ( " &
                          "  Select ROW_NUMBER() OVER(PARTITION BY XFinal.Asset_Code ORDER BY XFinal.Asset_Code,DepBook.Document_Date  ASC)  +1 as SNO, XFinal.Asset_Code,XFinal.Asset_Name,XFinal.Asset_Specification, XFinal.Loc_Code, XFinal.Vendor_Code,XFinal. Location_Desc, XFinal.Acquisition_Date,XFinal. Acquisition_Code, XFinal.Templete_Code, XFinal.Category_code, XFinal.CatDesc," &
                          "  XFinal.Group_Code,XFinal.GrpDesc ,XFinal.Vendor_Name,XFinal.AcSet_Code,XFinal.CostCenter_Code,XFinal.CostCenter_Name,XFinal.Dep_Method_Code,XFinal.Dep_Period_Code,XFinal.Start_Date,XFinal.Dep_Rate, XFinal.Dep_Tax_Rate,XFinal.Book_Estimated_Life,XFinal.Asset_Value as BookValue,XFinal.OriginalBookValue,XFinal.SRN_No,DepBook.Document_Date as [Document Date],XFinal.Asset_Value - XFinal.[Opening Depreciation] as 'Opening_Value(Book Vlue -Opening Depreciation)' ,DepBook.Additional_Amount,case when   ROW_NUMBER() OVER(PARTITION BY XFinal.Asset_Code ORDER BY XFinal.Asset_Code,DepBook.Document_Date ASC)  = 1 then  XFinal.[Opening Depreciation]  else 0 end [Opening Depreciation] " &
                          " ,DepBook.Dep_Amount,depbook.dep_amount+XFinal.[Opening Depreciation] as [Total Depreciation],case when ROW_NUMBER() OVER(PARTITION BY XFinal.Asset_Code ORDER BY XFinal.Asset_Code,DepBook.Document_Date  ASC)  = 1 then  XFinal.[Opening Depreciation]  else 0 end+ dep_amount as [total des new],isnull(DepBook.DepRate,0) as DepRate,XFinal.Asset_Value- (depbook.dep_amount+XFinal.[Opening Depreciation]) as Asset_Value ,XFinal.Asset_Value - XFinal.[Opening Depreciation] - dep_amount as 'asset value new' " &
                          "  from ( " &
                          " " + qry + " " &
                          "  ) XFinal " &
                          "  left outer Join " &
                          "  (select Document_Code,Document_Date,Asset_Code,Opening_Value,Work_Expense as Additional_Amount,Dep_Amount,deprate,Asset_value from ( select Document_Code,Document_Date,Asset_Code,Opening_Value,Work_Expense,Dep_Amount,deprate,Asset_value, Opening_Value_Tax,Dep_Amount_Tax,Asset_value_Tax,Is_Permanent,Is_Reverse_Dep from TSPL_ASSET_DEPRECIATION  where TSPL_ASSET_DEPRECIATION.Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' ) DepBook) DepBook on XFinal.Asset_Code = DepBook.Asset_Code " &
                          " ) as TBL_Asset_Value_Cal on TBL_Asset_Value_Cal.Asset_Code =  XXXFinal.Asset_Code and TBL_Asset_Value_Cal.SNO = XXXFinal.SNO "
                ElseIf rdbTax.CheckState = CheckState.Checked Then
                    qry = "  select XFinal.Asset_Code,XFinal.Asset_Name,XFinal.Asset_Specification,XFinal.Loc_Code,XFinal.Vendor_Code,XFinal.Vendor_Name,XFinal.Location_Desc,XFinal.Acquisition_Date,XFinal.Acquisition_Code,XFinal.Templete_Code,XFinal.Category_code,XFinal.CatDesc,  XFinal.Group_Code,XFinal.GrpDesc,XFinal.Vendor_Name ,XFinal.AcSet_Code,XFinal.CostCenter_Code,XFinal.CostCenter_Name,XFinal.Dep_Method_Code,XFinal.Dep_Period_Code,XFinal.Start_Date,XFinal.Dep_Rate, XFinal.Dep_Tax_Rate,XFinal.Book_Estimated_Life,XFinal.BookValue,XFinal.OriginalBookValue,XFinal.SRN_No,DepBook.Document_Date as [Document Date],DepBook.Opening_Value,DepBook.Additional_Amount,XFinal.[Opening Depreciation],DepBook.Dep_Amount,depbook.dep_amount+XFinal.[Opening Depreciation] as [Total Depreciation],isnull(DepBook.DepRate,0) as DepRate,XFinal.Asset_Value-isnull((select sum(Dep_Amount)  from TSPL_ASSET_DEPRECIATION as inTable where inTable.asset_code=XFinal.Asset_Code and document_date<=DepBook.Document_Date),0)  as Asset_Value   " &
                          "  from (  " &
                          "  " + qry + "" &
                          " ) XFinal " &
                          " left Outer Join  " &
                          "  (select Document_Code,Document_Date,Asset_Code,Opening_Value_Tax as Opening_Value,Work_Expense as Additional_Amount,Dep_Amount_Tax as Dep_Amount,Asset_value_Tax as Asset_value,deprate from ( select Document_Code,Document_Date,Asset_Code,Opening_Value,Work_Expense,Dep_Amount,deprate,Asset_value, Opening_Value_Tax,Dep_Amount_Tax,Asset_value_Tax,Is_Permanent,Is_Reverse_Dep from TSPL_ASSET_DEPRECIATION   ) DepTax)  DepBook on DepBook.Asset_Code = XFinal.Asset_Code  order by XFinal.Asset_Code,DepBook.Document_Date"
                End If
                dt = clsDBFuncationality.GetDataTable(qry)
                GV1.DataSource = Nothing
                GV1.GroupDescriptors.Clear()
                GV1.MasterTemplate.SummaryRowsBottom.Clear()
                GV1.BestFitColumns()
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    RadMessageBox.Show("No Data Found to Display", Me.Text)
                    Exit Sub
                Else
                    GV1.DataSource = dt
                    SetGridFormationOFGV1()
                    ReStoreGridLayout()
                    RadPageView1.SelectedPage = RadPageViewPage2
                End If
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

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcel("Asset Register" + IIf(rdbDetail.IsChecked, " ( Detail ) ", " ( Summary ) "), GV1, arrHeader, Me.Text)
            ElseIf exporter = EnumExportTo.PDF Then
                clsCommon.MyExportToPDF("Asset Register" + IIf(rdbDetail.IsChecked, " ( Detail ) ", " ( Summary ) "), GV1, arrHeader, "Asset Register Report", True)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try


    End Sub
    Private Sub PrintOld(ByVal exporter As EnumExportTo)
        Try
            GV1.EnableFiltering = True
            Dim dt As DataTable

            Dim strFromDateTime As String = clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm tt")
            Dim strToDateTime As String = clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm tt")

            Dim strQuery As String = "select TSPL_ACQUISITION_DETAIL.Asset_Code,Asset_Name,Asset_Specification,Acquisition_Date,Location_Desc, " & _
            "CostCenter_Name,TSPL_ASSET_CATEGORY.Description as CatDesc,TSPL_ASSET_GROUP.Description  as GrpDesc,Vendor_Name,TSPL_ACQUISITION_DETAIL.Start_Date, " & _
            "max(Book_Source_value) as BookValue, "
            If rdbBook.IsChecked Then
                strQuery += "(select sum(Dep_Amount) from TSPL_ASSET_DEPRECIATION where Asset_Code=TSPL_ACQUISITION_DETAIL.Asset_Code and YEAR(TSPL_ASSET_DEPRECIATION.Document_Date)='" & Year(fromDate.Value) & "'  ) as YTDDep, "
                strQuery += "(select SUM(Dep_Amount) from TSPL_ASSET_DEPRECIATION where Asset_Code=TSPL_ACQUISITION_DETAIL.Asset_Code) as TotalDep, "
                strQuery += "isnull( (select MIN(Asset_value) from TSPL_ASSET_DEPRECIATION  "
            ElseIf rdbTax.IsChecked Then
                strQuery += "     (select sum(Dep_Amount_Tax ) from TSPL_ASSET_DEPRECIATION where Asset_Code=TSPL_ACQUISITION_DETAIL.Asset_Code and YEAR(TSPL_ASSET_DEPRECIATION.Document_Date)='" & Year(fromDate.Value) & "'  ) as YTDDep, (select SUM(Dep_Amount_Tax ) from TSPL_ASSET_DEPRECIATION where Asset_Code=TSPL_ACQUISITION_DETAIL.Asset_Code) as TotalDep, isnull( (select MIN(Asset_value_Tax ) from TSPL_ASSET_DEPRECIATION "
            End If
            strQuery += "  where Asset_Code=TSPL_ACQUISITION_DETAIL.Asset_Code and Document_Date <= '" + strToDateTime + "'),0)  as Net " & _
            "from TSPL_ACQUISITION_HEAD left outer join TSPL_ACQUISITION_DETAIL on " & _
            "TSPL_ACQUISITION_HEAD.Acquisition_Code=TSPL_ACQUISITION_DETAIL.Acquisition_Code  " & _
            "left outer join TSPL_LOCATION_MASTER on TSPL_ACQUISITION_HEAD.Loc_Code=TSPL_LOCATION_MASTER.Location_Code  " & _
            "left outer join TSPL_FA_COST_CENTER_MASTER on TSPL_ACQUISITION_DETAIL.CostCenter_Code=TSPL_FA_COST_CENTER_MASTER.CostCenter_Code  " & _
            "left outer join TSPL_ASSET_CATEGORY on TSPL_ACQUISITION_DETAIL.Category_code=TSPL_ASSET_CATEGORY.Category_Code " & _
            "left outer join TSPL_ASSET_GROUP on TSPL_ACQUISITION_DETAIL.Group_Code=TSPL_ASSET_GROUP.Group_Code " & _
            "left outer join TSPL_VENDOR_MASTER on TSPL_ACQUISITION_HEAD.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code " & _
            "where TSPL_ACQUISITION_HEAD.Status=1 and Acquisition_Date >=  '" & strFromDateTime & "' AND " & _
            "Acquisition_Date <=  '" & strToDateTime & "'   "

            If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
                strQuery += " and TSPL_ACQUISITION_HEAD.Loc_Code in (" + clsCommon.GetMulcallString(TxtMultiLocation.arrValueMember) + ") " + Environment.NewLine
            ElseIf TxtMultiAssetId.arrValueMember IsNot Nothing AndAlso TxtMultiAssetId.arrValueMember.Count > 0 Then
                strQuery += " and TSPL_ACQUISITION_DETAIL.Asset_Code in (" + clsCommon.GetMulcallString(TxtMultiAssetId.arrValueMember) + ") " + Environment.NewLine
            ElseIf TxtMultiCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCategory.arrValueMember.Count > 0 Then
                strQuery += " and TSPL_ACQUISITION_DETAIL.Category_code in (" + clsCommon.GetMulcallString(TxtMultiCategory.arrValueMember) + ") " + Environment.NewLine
            ElseIf TxtMultiCostCenter.arrValueMember IsNot Nothing AndAlso TxtMultiCostCenter.arrValueMember.Count > 0 Then
                strQuery += " and TSPL_ACQUISITION_DETAIL.CostCenter_Code in (" + clsCommon.GetMulcallString(TxtMultiCostCenter.arrValueMember) + ") " + Environment.NewLine
            ElseIf TxtMultiGroup.arrValueMember IsNot Nothing AndAlso TxtMultiGroup.arrValueMember.Count > 0 Then
                strQuery += " and TSPL_ACQUISITION_DETAIL.Group_Code in (" + clsCommon.GetMulcallString(TxtMultiGroup.arrValueMember) + ") " + Environment.NewLine
            ElseIf TxtMultiVendor.arrValueMember IsNot Nothing AndAlso TxtMultiVendor.arrValueMember.Count > 0 Then
                strQuery += " and TSPL_ACQUISITION_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(TxtMultiVendor.arrValueMember) + ") " + Environment.NewLine
            End If
            strQuery += "group by TSPL_ACQUISITION_DETAIL.Asset_Code,Asset_Name,Asset_Specification, "
            strQuery += "Acquisition_Date, Location_Desc, CostCenter_Name, TSPL_ASSET_CATEGORY.Description, TSPL_ASSET_GROUP.Description, Vendor_Name, TSPL_ACQUISITION_DETAIL.Start_Date "

            If rdbSummary.IsChecked Then
                strQuery = "select Location_Desc,CostCenter_Name,CatDesc,GrpDesc,Vendor_Name,SUM(BookValue) as BookValue,SUM(YTDDep) as YTDDep, " & _
                "SUM(TotalDep) as  TotalDep,SUM(Net) as Net  from (" & strQuery & ") aaa  group by  Location_Desc,CostCenter_Name,CatDesc,GrpDesc,Vendor_Name"
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
                End If
            ElseIf rdbDetail.IsChecked Then
                Dim str As String = " select TSPL_ASSET_DEPRECIATION.Document_Code as [Document Code],CONVERT(varchar, TSPL_ASSET_DEPRECIATION.Document_Date,103) as [Document Date],TSPL_ASSET_DEPRECIATION.Asset_Code as [Asset Code], TSPL_ACQUISITION_DETAIL.Asset_Name as [Asset Name],Location_Desc as [Location],CostCenter_Name as [Cost Center Name],TSPL_ASSET_CATEGORY.Description as [Category Name],TSPL_ASSET_GROUP.Description  as [Group Name],Vendor_Name as [Vendor Name],TSPL_ACQUISITION_DETAIL.Asset_Specification as [Specification],TSPL_ACQUISITION_DETAIL.Dep_Method_Code as [Depreciation Method Code],TSPL_DEPRECIATION_METHOD.Description as [Depreciation Method Name]"
                If rdbBook.IsChecked Then
                    str += ",TSPL_ASSET_DEPRECIATION.Value_Before_Depreciation as [Before Depreciation],TSPL_ASSET_DEPRECIATION.Dep_Amount as [Depreciation Amount],TSPL_ASSET_DEPRECIATION.Asset_value as [Net Asset Value]"

                ElseIf rdbTax.IsChecked Then
                    str += "  , TSPL_ACQUISITION_DETAIL.Dep_Method_Tax_Code,DepMethodTax.Description as Dep_method_Tax_Name,TSPL_ASSET_DEPRECIATION.Value_Before_Depreciation_Tax as [Before Depreciation],TSPL_ASSET_DEPRECIATION.Dep_Amount_Tax as [Depreciation Amount],TSPL_ASSET_DEPRECIATION.Asset_value_Tax as [Net Asset Value] "
                End If
                str += " from TSPL_ASSET_DEPRECIATION left outer join TSPL_ACQUISITION_DETAIL on TSPL_ACQUISITION_DETAIL.Asset_Code=TSPL_ASSET_DEPRECIATION.Asset_Code " & _
            " left outer join TSPL_ACQUISITION_HEAD on TSPL_ACQUISITION_HEAD.Acquisition_Code=TSPL_ACQUISITION_DETAIL.Acquisition_Code  " & _
            " left outer join TSPL_LOCATION_MASTER on TSPL_ACQUISITION_HEAD.Loc_Code=TSPL_LOCATION_MASTER.Location_Code " & _
            " left outer join TSPL_FA_COST_CENTER_MASTER on TSPL_ACQUISITION_DETAIL.CostCenter_Code=TSPL_FA_COST_CENTER_MASTER.CostCenter_Code  " & _
            " left outer join TSPL_ASSET_CATEGORY on TSPL_ACQUISITION_DETAIL.Category_code=TSPL_ASSET_CATEGORY.Category_Code " & _
            " left outer join TSPL_ASSET_GROUP on TSPL_ACQUISITION_DETAIL.Group_Code=TSPL_ASSET_GROUP.Group_Code " & _
            " left outer join TSPL_VENDOR_MASTER on TSPL_ACQUISITION_HEAD.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code " & _
            " left outer join TSPL_DEPRECIATION_METHOD on TSPL_DEPRECIATION_METHOD.Code=TSPL_ASSET_DEPRECIATION.Dep_Method_Code " & _
            " left outer join TSPL_DEPRECIATION_METHOD as DepMethodTax on DepMethodTax.Code=TSPL_ASSET_DEPRECIATION.Dep_Method_Tax_Code " & _
            " where TSPL_ACQUISITION_HEAD.Status=1 and Acquisition_Date >=  '" & strFromDateTime & "' AND Acquisition_Date <=  '" & strToDateTime & "' "
                If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
                    str += " and TSPL_ACQUISITION_HEAD.Loc_Code in (" + clsCommon.GetMulcallString(TxtMultiLocation.arrValueMember) + ") " + Environment.NewLine
                ElseIf TxtMultiAssetId.arrValueMember IsNot Nothing AndAlso TxtMultiAssetId.arrValueMember.Count > 0 Then
                    str += " and TSPL_ACQUISITION_DETAIL.Asset_Code in (" + clsCommon.GetMulcallString(TxtMultiAssetId.arrValueMember) + ") " + Environment.NewLine
                ElseIf TxtMultiCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCategory.arrValueMember.Count > 0 Then
                    str += " and TSPL_ACQUISITION_DETAIL.Category_code in (" + clsCommon.GetMulcallString(TxtMultiCategory.arrValueMember) + ") " + Environment.NewLine
                ElseIf TxtMultiCostCenter.arrValueMember IsNot Nothing AndAlso TxtMultiCostCenter.arrValueMember.Count > 0 Then
                    str += " and TSPL_ACQUISITION_DETAIL.CostCenter_Code in (" + clsCommon.GetMulcallString(TxtMultiCostCenter.arrValueMember) + ") " + Environment.NewLine
                ElseIf TxtMultiGroup.arrValueMember IsNot Nothing AndAlso TxtMultiGroup.arrValueMember.Count > 0 Then
                    str += " and TSPL_ACQUISITION_DETAIL.Group_Code in (" + clsCommon.GetMulcallString(TxtMultiGroup.arrValueMember) + ") " + Environment.NewLine
                ElseIf TxtMultiVendor.arrValueMember IsNot Nothing AndAlso TxtMultiVendor.arrValueMember.Count > 0 Then
                    str += " and TSPL_ACQUISITION_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(TxtMultiVendor.arrValueMember) + ") " + Environment.NewLine
                End If


                dt = clsDBFuncationality.GetDataTable(str)
                GV1.DataSource = Nothing
                GV1.GroupDescriptors.Clear()
                GV1.MasterTemplate.SummaryRowsBottom.Clear()
                GV1.BestFitColumns()
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    RadMessageBox.Show("No Data Found to Display", Me.Text)
                    Exit Sub
                Else
                    GV1.DataSource = dt

                    RadPageView1.SelectedPage = RadPageViewPage2
                End If
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

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcel("Asset Register" + IIf(rdbDetail.IsChecked, " ( Detail ) ", " ( Summary ) "), GV1, arrHeader, Me.Text)
            ElseIf exporter = EnumExportTo.PDF Then
                clsCommon.MyExportToPDF("Asset Register" + IIf(rdbDetail.IsChecked, " ( Detail ) ", " ( Summary ) "), GV1, arrHeader, "Asset Register Report", True)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
        'GV1.Columns("Acquisition_Date").FormatString = "{0:  dd/MM/yyyy}"

        GV1.Columns("Start_Date").IsVisible = True
        GV1.Columns("Start_Date").Width = 120
        GV1.Columns("Start_Date").HeaderText = "Life Start Date"
        'GV1.Columns("Start_Date").FormatString = "{0:  dd/MM/yyyy}"


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

        GV1.Columns("Dep_Amount").IsVisible = True
        GV1.Columns("Dep_Amount").Width = 120
        GV1.Columns("Dep_Amount").HeaderText = "Depreciation Amount"

       
        If rdbDetail.IsChecked Then 'added by preeti gupta [UDL/25/09/18-000224]

            GV1.Columns("Opening Depreciation").IsVisible = True
            GV1.Columns("Opening Depreciation").Width = 120
            GV1.Columns("Opening Depreciation").HeaderText = "Opening Depreciation"

            GV1.Columns("Total Depreciation").IsVisible = True
            GV1.Columns("Total Depreciation").Width = 120
            GV1.Columns("Total Depreciation").HeaderText = "Total Depreciation"

            GV1.Columns("Document_Code").IsVisible = True
            GV1.Columns("Document_Code").Width = 120
            GV1.Columns("Document_Code").HeaderText = "Depreciation Code"

            GV1.Columns("Document Date").IsVisible = True
            GV1.Columns("Document Date").Width = 120
            GV1.Columns("Document Date").HeaderText = "Depreciation Date"

        End If


        GV1.Columns("DepValueFinal").IsVisible = True
        GV1.Columns("DepValueFinal").Width = 120
        GV1.Columns("DepValueFinal").HeaderText = "Accumulated Depreciation"


        GV1.Columns("DepRate").IsVisible = True
        GV1.Columns("DepRate").Width = 120
        GV1.Columns("DepRate").HeaderText = "Depreciation Rate"

        GV1.Columns("Addition_amount").IsVisible = True
        GV1.Columns("Addition_amount").Width = 120
        GV1.Columns("Addition_amount").HeaderText = "Additional Amount"

        GV1.Columns("Asset_Value").IsVisible = True
        GV1.Columns("Asset_Value").Width = 120
        GV1.Columns("Asset_Value").HeaderText = "Asset Value"

        GV1.Columns("Dep_Period_Code").IsVisible = True
        GV1.Columns("Dep_Period_Code").Width = 120
        GV1.Columns("Dep_Period_Code").HeaderText = "Dep Period Code"


        If rdbDetail.CheckState = CheckState.Checked Then
            GV1.Columns("Document Date").IsVisible = True
            GV1.Columns("Document Date").Width = 120
            GV1.Columns("Document Date").HeaderText = "Depreciation Date"
            'GV1.Columns("Document Date").FormatString = "{0:  dd/MM/yyyy}"
            GV1.Columns("Opening_Value").IsVisible = True
            GV1.Columns("Opening_Value").Width = 120
            GV1.Columns("Opening_Value").HeaderText = "Opening Value"
        End If

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        'If rdbDetail.CheckState = CheckState.Checked Then
        '    Dim item0 As New GridViewSummaryItem("Opening_Value", "{0:F2}", GridAggregateFunction.Sum)
        '    summaryRowItem.Add(item0)
        'End If
        'Dim item1 As New GridViewSummaryItem("BookValue", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item1)

        'Dim item2 As New GridViewSummaryItem("Dep_Amount", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item2)

        'Dim item3 As New GridViewSummaryItem("Additional_Amount", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item3)

        'Dim item4 As New GridViewSummaryItem("Asset_Value", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item4)

        'Dim item5 As New GridViewSummaryItem("DepRate", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item5)
        'Dim item6 As New GridViewSummaryItem("Opening Depreciation", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item6)
        Dim item7 As New GridViewSummaryItem("Total Depreciation", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)

        'End If



        GV1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        GV1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        'If clsCommon.myLen(txtAssetCodeFinder.Value) <= 0 Then
        '    clsCommon.MyMessageBoxShow("Please Select Asset Code First.", Me.Text)
        '    Exit Sub
        'End If
        PageSetupReport_ID = getreport_id()
        TemplateGridview = GV1
        Print(EnumExportTo.Refresh)
    End Sub

    Public Enum EnumExportTo
        Excel = 0
        PDF = 1
        Refresh = 2
    End Enum
    Private Sub export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles export.Click
        'Print(EnumExportTo.Excel)
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub PDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PDF.Click
        'Print(EnumExportTo.PDF)
        ExportGrid(EnumExportTo.PDF)
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
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
        rdbBook.IsChecked = True
        rdbDetail.IsChecked = True
        txtAssetCodeFinder.Value = ""
        lblAssetCodeFinder.Text = ""
    End Sub
    '===shivani[BM00000008016]
    Private Sub TxtMultiLocation__My_Click(sender As Object, e As EventArgs) Handles TxtMultiLocation._My_Click
        Dim frm As New FrmPendingRequisitionQty()
        Dim qry As String = " select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        TxtMultiLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("MulLoc", qry, "Code", "Name", TxtMultiLocation.arrValueMember, TxtMultiLocation.arrDispalyMember)
        frm.SetDiplayMember(TxtMultiLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub

    Private Sub TxtMultiCostCenter__My_Click(sender As Object, e As EventArgs) Handles TxtMultiCostCenter._My_Click
        Dim qry As String = "select CostCenter_Code as Code,CostCenter_Name as Name FROM TSPL_FA_COST_CENTER_MASTER"
        TxtMultiCostCenter.arrValueMember = clsCommon.ShowMultipleSelectForm("MulCost", qry, "Code", "Name", TxtMultiCostCenter.arrValueMember, TxtMultiCostCenter.arrDispalyMember)
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
        Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name FROM TSPL_VENDOR_MASTER Where Status='N' "
        TxtMultiVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("MulVendor", qry, "Code", "Name", TxtMultiVendor.arrValueMember, TxtMultiVendor.arrDispalyMember)
    End Sub

    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmAssetRegister & "'"))
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

            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(GV1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(GV1, "", Me.Text, , arrHeader)
            Else
                transportSql.applyExportTemplate(GV1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Asset Register" + IIf(rdbDetail.IsChecked, " ( Detail ) ", " ( Summary ) "), GV1, arrHeader, "Asset Register Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
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

    Private Function getreport_id()
        Dim report_id As String = Me.Form_ID
        If rdbBook.IsChecked = True Then
            report_id = report_id + "B"
        Else
            report_id = report_id + "T"
        End If
        If rdbSummary.IsChecked = True Then
            report_id = report_id + "S"
        Else
            report_id = report_id + "D"
        End If
        Return report_id
    End Function

    Private Sub Print(ByVal exporter As EnumExportTo)
        Try
            GV1.EnableFiltering = True
            Dim dt As DataTable
            Dim strFromDateTime As String = clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm tt")
            Dim strToDateTime As String = clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm tt")
            Dim qry As String = ""
            Dim qryBase As String = ""
            Dim whrACQUISITION As String = ""
            Dim whrDEPRECIATION As String = ""
            Dim whrWORK_HEAD As String = ""
            If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
                whrACQUISITION += " and  TSPL_ACQUISITION_HEAD.Loc_Code in (" + clsCommon.GetMulcallString(TxtMultiLocation.arrValueMember) + ") " + Environment.NewLine
                whrDEPRECIATION += " and  TSPL_ASSET_DEPRECIATION.Location_Code in (" + clsCommon.GetMulcallString(TxtMultiLocation.arrValueMember) + ") " + Environment.NewLine
                whrWORK_HEAD += " and  TSPL_ASSET_WORK_HEAD.Location_Code in (" + clsCommon.GetMulcallString(TxtMultiLocation.arrValueMember) + ") " + Environment.NewLine
            End If
            If TxtMultiAssetId.arrValueMember IsNot Nothing AndAlso TxtMultiAssetId.arrValueMember.Count > 0 Then
                whrACQUISITION += "   and TSPL_ACQUISITION_DETAIL.Asset_Code in  (" + clsCommon.GetMulcallString(TxtMultiAssetId.arrValueMember) + ") " + Environment.NewLine
                whrDEPRECIATION += "   and TSPL_ASSET_DEPRECIATION.Asset_Code in  (" + clsCommon.GetMulcallString(TxtMultiAssetId.arrValueMember) + ") " + Environment.NewLine
                whrWORK_HEAD += "   and TSPL_ASSET_WORK_HEAD.Asset_Code in  (" + clsCommon.GetMulcallString(TxtMultiAssetId.arrValueMember) + ") " + Environment.NewLine
            End If
            If TxtMultiCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCategory.arrValueMember.Count > 0 Then
                whrACQUISITION += " and  TSPL_ACQUISITION_DETAIL.Category_code in (" + clsCommon.GetMulcallString(TxtMultiCategory.arrValueMember) + ") " + Environment.NewLine
                whrDEPRECIATION += " and  TSPL_ASSET_DEPRECIATION.Asset_Category_Code in (" + clsCommon.GetMulcallString(TxtMultiCategory.arrValueMember) + ") "
                whrWORK_HEAD += " "
            End If
            If TxtMultiCostCenter.arrValueMember IsNot Nothing AndAlso TxtMultiCostCenter.arrValueMember.Count > 0 Then
                whrACQUISITION += " and  TSPL_ACQUISITION_DETAIL.CostCenter_Code in (" + clsCommon.GetMulcallString(TxtMultiCostCenter.arrValueMember) + ") " + Environment.NewLine
                whrDEPRECIATION += ""
                whrWORK_HEAD += " "
            End If
            If TxtMultiGroup.arrValueMember IsNot Nothing AndAlso TxtMultiGroup.arrValueMember.Count > 0 Then
                whrACQUISITION += " and  TSPL_ACQUISITION_DETAIL.Group_Code in (" + clsCommon.GetMulcallString(TxtMultiGroup.arrValueMember) + ") " + Environment.NewLine
                whrDEPRECIATION += ""
                whrWORK_HEAD += " "
            End If
            If TxtMultiVendor.arrValueMember IsNot Nothing AndAlso TxtMultiVendor.arrValueMember.Count > 0 Then
                whrACQUISITION += " and  TSPL_ACQUISITION_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(TxtMultiVendor.arrValueMember) + ") " + Environment.NewLine
                whrDEPRECIATION += ""
                whrWORK_HEAD += " and  TSPL_ASSET_WORK_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(TxtMultiVendor.arrValueMember) + ") " + Environment.NewLine
            End If

            Dim costCenterJoin As String = "  left outer join TSPL_FA_COST_CENTER_MASTER on TSPL_ACQUISITION_DETAIL.CostCenter_Code=TSPL_FA_COST_CENTER_MASTER.CostCenter_Code  "
            If clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ApplyFinancialCostCenter, clsFixedParameterCode.ApplyFinancialCostCenter, Nothing)) = "1", True, False)) = True Then
                costCenterJoin = "  left outer join (select TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code as CostCenter_Code , TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name as CostCenter_Name from  TSPL_COST_CENTRE_FINANCIAL) as TSPL_FA_COST_CENTER_MASTER on TSPL_FA_COST_CENTER_MASTER .CostCenter_Code = TSPL_ACQUISITION_DETAIL.CostCenter_Code "
            End If



            qryBase = "with cte as (  " &
                    " select XXXFinal.SNO,  XXXFinal.Asset_Code ,XXXFinal.Asset_Name , XXXFinal.Asset_Specification, XXXFinal.Loc_Code,XXXFinal.Location_Desc,XXXFinal.Vendor_Code,XXXFinal.Vendor_Name ,  " &
                    " XXXfinal.Acquisition_Date, XXXfinal.Acquisition_Code, XXXfinal.Templete_Code,XXXfinal.Category_Code,XXXfinal.CatDesc,XXXfinal.Group_Code,XXXfinal.GrpDesc,  " &
                    " XXXfinal.AcSet_Code,XXXfinal.CostCenter_Code,XXXfinal.CostCenter_Name,XXXfinal.Dep_Method_Code,XXXfinal.Dep_period_Code,XXXfinal.Start_Date,XXXfinal.Dep_Rate,XXXfinal.Dep_Tax_Rate,  " &
                    " XXXfinal.Book_Estimated_Life,XXXfinal.OriginalBookValue,XXXfinal.SRN_No,XXXFinal.Document_Code,XXXFinal.Document_Date,XXXFinal.BookValue, case when XXXFinal.SNO = 1 then  XXXFinal.[Opening Depreciation]  Else 0 End [Opening Depreciation],XXXFinal.BookValue- XXXFinal.[Opening Depreciation]+XXXFinal.Addition_amount As [Opening_Value] , XXXFinal.Addition_amount  ,XXXFinal. Dep_Amount , XXXFinal. Dep_Amount + XXXFinal.[Opening Depreciation] As [Total Depreciation], " &
                    " XXXfinal.DepRate,XXXFinal.Asset_Value - XXXFinal.[Opening Depreciation] - XXXFinal.dep_amount As 'Asset_Value',  " &
                    " case when XXXFinal.SNO = 1 then XXXFinal.Asset_Value - XXXFinal.[Opening Depreciation] - XXXFinal.dep_amount else -1*XXXFinal.dep_amount end as AssetValueFinal  " &
                    ",case when XXXFinal.SNO = 1 then  XXXFinal.[Opening Depreciation]+XXXFinal.Dep_Amount  Else XXXFinal. Dep_Amount End as DepValueFinal,XXXFinal.Book_Salvage_Value,XXXFinal.Book_Salvage_Rate,XXXFinal.InvoiceNo,XXXFinal.InvoiceDate,XXXFinal.Asset_Disposal_Code,XXXFinal.AdjustAmount,XXXFinal.AdjustAmountTax  " &
                    " from ( " &
                    " select ROW_NUMBER() OVER(PARTITION BY TSPL_ACQUISITION_DETAIL.Asset_Code ORDER BY TSPL_ACQUISITION_DETAIL.Asset_Code,DepBook.Document_Date  ASC) as SNO,  " &
                    " TSPL_ACQUISITION_DETAIL.Asset_Code,TSPL_ACQUISITION_DETAIL.Asset_Name,TSPL_ACQUISITION_DETAIL.Asset_Specification,TSPL_ACQUISITION_HEAD.Loc_Code,TSPL_ACQUISITION_HEAD.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_LOCATION_MASTER.Location_Desc, DepBook.Document_Code,DepBook.Document_Date,TSPL_ACQUISITION_HEAD.Acquisition_Date,TSPL_ACQUISITION_HEAD.Acquisition_Code,TSPL_ACQUISITION_DETAIL.Templete_Code,TSPL_ACQUISITION_DETAIL.Category_code,TSPL_ASSET_CATEGORY.Description as CatDesc,  TSPL_ACQUISITION_DETAIL.Group_Code,TSPL_ASSET_GROUP.Description as GrpDesc ,TSPL_ACQUISITION_DETAIL.AcSet_Code,TSPL_ACQUISITION_DETAIL.CostCenter_Code,TSPL_FA_COST_CENTER_MASTER.CostCenter_Name,TSPL_ACQUISITION_DETAIL.Dep_Method_Code,TSPL_ACQUISITION_DETAIL.Dep_Period_Code,TSPL_ACQUISITION_DETAIL.Start_Date,TSPL_ACQUISITION_DETAIL.Dep_Rate, TSPL_ACQUISITION_DETAIL.Dep_Tax_Rate,TSPL_ACQUISITION_DETAIL.Book_Estimated_Life,TSPL_ACQUISITION_DETAIL.Book_Source_value as BookValue,TSPL_ACQUISITION_DETAIL.Book_Source_Original_value as OriginalBookValue,TSPL_ACQUISITION_DETAIL.SRN_No,isnull(TSPL_ACQUISITION_DETAIL.depreciated_value,0) as [Opening Depreciation],0 as Final_Dep_Amount,0+TSPL_ACQUISITION_DETAIL.Depreciated_Value as [Total Final Depreeciated],  0 as Final_Dep_Rate,0 as Final_Dep_Rate_tax,0 as Final_Dep_Amount_Tax,coalesce(AW.Addition_Amount,0) as Addition_Amount,(TSPL_ACQUISITION_DETAIL.Book_Source_value+coalesce(AW.Addition_Amount,0)) as Asset_Value,(TSPL_ACQUISITION_DETAIL.Book_Source_value+coalesce(AW.Addition_Amount,0)-0) as Asset_Value_Tax,  TSPL_ACQUISITION_DETAIL.Total_Tax_Amt,TSPL_ACQUISITION_DETAIL.Book_Salvage_Rate,TSPL_ACQUISITION_DETAIL.Book_Salvage_Value,TSPL_ACQUISITION_DETAIL.Is_Assembled,TSPL_ACQUISITION_HEAD.Acquisition_Type ,isnull(DepBook.Additional_Amount,0) as Additional_Amount,isnull( DepBook.Dep_Amount,0) as Dep_Amount,   isnull(DepBook.DepRate,0) as  DepRate,case when len(isnull(TSPL_ACQUISITION_HEAD.PI_No,''))>0 then TSPL_PI_HEAD.Vendor_Invoice_No else (case when len(isnull(TSPL_ACQUISITION_HEAD.SRN_No,''))>0 then TSPL_SRN_HEAD.Challan_No else null end) end as InvoiceNo,case when len(isnull(TSPL_ACQUISITION_HEAD.PI_No,''))>0 then TSPL_PI_HEAD.InvoiceDate else (case when len(isnull(TSPL_ACQUISITION_HEAD.SRN_No,''))>0 then TSPL_SRN_HEAD.Challan_Date else null end) end as InvoiceDate,DepBook.Asset_Disposal_Code,DepBook.AdjustAmount,  DepBook.AdjustAmountTax  " &
                    " from TSPL_ACQUISITION_DETAIL  " &
                    " inner join TSPL_ACQUISITION_HEAD  on TSPL_ACQUISITION_DETAIL.Acquisition_Code=TSPL_ACQUISITION_HEAD.Acquisition_Code  " &
                    " left outer join TSPL_LOCATION_MASTER on TSPL_ACQUISITION_HEAD.Loc_Code=TSPL_LOCATION_MASTER.Location_Code     " &
                    " " + costCenterJoin + "     " &
                    " left outer join TSPL_ASSET_CATEGORY on TSPL_ACQUISITION_DETAIL.Category_code=TSPL_ASSET_CATEGORY.Category_Code    " &
                    " left outer join TSPL_ASSET_GROUP on TSPL_ACQUISITION_DETAIL.Group_Code=TSPL_ASSET_GROUP.Group_Code   " &
                    " left outer join TSPL_VENDOR_MASTER on TSPL_ACQUISITION_HEAD.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code  " &
                    " left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_No=TSPL_ACQUISITION_HEAD.PI_No" &
                    " left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_ACQUISITION_HEAD.SRN_No " &
                    " left outer Join ( (select '--Opening--' as Document_Code,'" + clsCommon.GetPrintDate(fromDate.Value.AddDays(-1), "dd/MMM/yyyy") + "' as  Document_Date,Asset_Code,max(Opening_Value) as Opening_Value,sum(Work_Expense) as Additional_Amount,sum(Dep_Amount) as Dep_Amount,max(deprate) as deprate,max(Opening_Value)-sum(Dep_Amount) Asset_value, max(Asset_Disposal_Code) as Asset_Disposal_Code, 0 as AdjustAmount,0 as AdjustAmountTax from ( select Document_Code,Document_Date,Asset_Code,Opening_Value,Work_Expense,Dep_Amount,deprate,Asset_value, Opening_Value_Tax,Dep_Amount_Tax,Asset_value_Tax,Is_Permanent,Is_Reverse_Dep,TSPL_ASSET_DEPRECIATION.Asset_Disposal_Code from TSPL_ASSET_DEPRECIATION  where 
	 Convert(date, TSPL_ASSET_DEPRECIATION.Document_Date,103)<'" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "'  " + whrDEPRECIATION + " 
     union all
     select cast( TSPL_ASSET_DEPRECIATION_ADJUSTMENT.Adjustment_ID as Varchar) as Document_Code,(TSPL_ASSET_DEPRECIATION_ADJUSTMENT.Adjustment_Date) as Document_Date,TSPL_ASSET_DEPRECIATION_ADJUSTMENT.Asset_Code,0 as Opening_Value,0 as Work_Expense,-1*TSPL_ASSET_DEPRECIATION_ADJUSTMENT.Dep_Amount as Dep_Amount,0 as deprate,0 as Asset_value, 0 as Opening_Value_Tax,-1*TSPL_ASSET_DEPRECIATION_ADJUSTMENT.Dep_Amount_Tax as Dep_Amount_Tax,0 as Asset_value_Tax,null as Is_Permanent,0 as Is_Reverse_Dep,null as Asset_Disposal_Code 
from TSPL_ASSET_DEPRECIATION_ADJUSTMENT  
where  Convert(Date, TSPL_ASSET_DEPRECIATION_ADJUSTMENT.Adjustment_Date,103)<'" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "'  "

            qryBase += ") DepBook group by DepBook.Asset_Code) " &
            " Union all  " &
            " (select Document_Code,Document_Date,Asset_Code,Opening_Value,Work_Expense as Additional_Amount,Dep_Amount,deprate,Asset_value,Asset_Disposal_Code,AdjustAmount,AdjustAmountTax from ( 
            select Document_Code,Document_Date,Asset_Code,Opening_Value,Work_Expense,Dep_Amount,deprate,Asset_value, Opening_Value_Tax,Dep_Amount_Tax,Asset_value_Tax,Is_Permanent,Is_Reverse_Dep,TSPL_ASSET_DEPRECIATION.Asset_Disposal_Code,0 as AdjustAmount,0 as AdjustAmountTax from TSPL_ASSET_DEPRECIATION  
where TSPL_ASSET_DEPRECIATION.Document_Date>='" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and  TSPL_ASSET_DEPRECIATION.Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' " + whrDEPRECIATION + "  
union all
Select cast(TSPL_ASSET_DEPRECIATION_ADJUSTMENT.Adjustment_ID As Varchar) As Document_Code,(TSPL_ASSET_DEPRECIATION_ADJUSTMENT.Adjustment_Date) As Document_Date,TSPL_ASSET_DEPRECIATION_ADJUSTMENT.Asset_Code,0 As Opening_Value,0 As Work_Expense,0 as Dep_Amount,0 As deprate,0 As Asset_value, 0 As Opening_Value_Tax,0 as Dep_Amount_Tax,0 As Asset_value_Tax,null As Is_Permanent,0 As Is_Reverse_Dep,null As Asset_Disposal_Code,-1*TSPL_ASSET_DEPRECIATION_ADJUSTMENT.Dep_Amount as Dep_Amount,-1*TSPL_ASSET_DEPRECIATION_ADJUSTMENT.Dep_Amount_Tax as AdjustAmountTax
From TSPL_ASSET_DEPRECIATION_ADJUSTMENT
Where Convert(Date, TSPL_ASSET_DEPRECIATION_ADJUSTMENT.Adjustment_Date,103) >='" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and Convert(date, TSPL_ASSET_DEPRECIATION_ADJUSTMENT.Adjustment_Date,103)<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'"
            qryBase += " ) DepBook) )DepBook on TSPL_ACQUISITION_DETAIL.Asset_Code = DepBook.Asset_Code   " &
                    "  " &
                    " left join (select Asset_Code,sum(Net_Amt) as Addition_Amount from  TSPL_ASSET_WORK_HEAD where Status=1  and Convert(date, TSPL_ASSET_WORK_HEAD.Document_date,103) <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy") + "' " + whrWORK_HEAD + "  " &
                    " group by Asset_Code) as AW on TSPL_ACQUISITION_DETAIL.Asset_Code=AW.Asset_Code  " &
                    "  " &
                    " where 2=2   " + whrACQUISITION + "  AND Convert(date, TSPL_ACQUISITION_HEAD.Acquisition_Date,103) <= Convert(date, '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") & "' ,103)  " &
                    " )  XXXFinal  " &
                    " )  " &
                    "  
select * into #CTETempTable from (select * from cte) x ;
CREATE INDEX IX_#CTETempTable ON #CTETempTable(SNO,Asset_Code);
"



            qry = " select  Asset_Code ,Asset_Name , Asset_Specification, Loc_Code,Location_Desc,Vendor_Code,Vendor_Name ,convert(varchar,Acquisition_Date,103) as Acquisition_Date, Acquisition_Code, Templete_Code,Category_Code,CatDesc,Group_Code,GrpDesc,AcSet_Code,CostCenter_Code,CostCenter_Name,Dep_Method_Code,Dep_period_Code,convert(varchar,[Start_Date],103) as [Start_Date],Dep_Rate,Dep_Tax_Rate, Book_Estimated_Life,OriginalBookValue,SRN_No,Document_Code as  Document_Code,case when len (isnull(Document_Code,'')) > 0 then  convert (varchar,Document_Date,103) else null end as [Document Date],BookValue,  [Opening Depreciation],case when SNO = 1 then  Opening_Value else Asset_Value + Dep_Amount end Opening_Value , Addition_amount ,Dep_Amount , case when SNO = 1 then [Total Depreciation] else Dep_Amount end as [Total Depreciation] ,DepValueFinal ,DepRate, Asset_Value,Book_Salvage_Value,Book_Salvage_Rate,InvoiceNo,InvoiceDate,Asset_Disposal_Code,AdjustAmount,AdjustAmountTax  " &
                        " from (  " &
                        " SELECT a.SNO, a.Asset_Code ,a.Asset_Name , a.Asset_Specification, a.Loc_Code,a.Location_Desc,a.Vendor_Code,a.Vendor_Name ,  " &
                        " a.Acquisition_Date, a.Acquisition_Code, a.Templete_Code,a.Category_Code,a.CatDesc,a.Group_Code,a.GrpDesc,  " &
                        " a.AcSet_Code,a.CostCenter_Code,a.CostCenter_Name,a.Dep_Method_Code,a.Dep_period_Code,a.[Start_Date],a.Dep_Rate,a.Dep_Tax_Rate, a.Book_Estimated_Life,a.OriginalBookValue,a.SRN_No,  " &
                        " a.Document_Code,a.Document_Date,a.BookValue,  a.[Opening Depreciation], " &
                        " a.BookValue + a.Addition_amount - a.[Opening Depreciation] as Opening_Value , a.Addition_amount ,a.Dep_Amount , a.[Total Depreciation],a.DepRate,  " &
                        " (SELECT SUM(AssetValueFinal-AdjustAmount)   FROM #CTETempTable b WHERE b.SNO <= a.SNO and b.Asset_code = a.Asset_Code) AS Asset_Value  " &
                        " ,(SELECT SUM(DepValueFinal) FROM #CTETempTable b WHERE b.SNO <= a.SNO and b.Asset_code = a.Asset_Code) AS DepValueFinal,a.Book_Salvage_Value,a.Book_Salvage_Rate,a.InvoiceNo,a.InvoiceDate,a.Asset_Disposal_Code,a.AdjustAmount,a.AdjustAmountTax" &
                        " FROM #CTETempTable a  " &
                        "  " &
                        " ) Final  "



            If rdbSummary.IsChecked Then
                qry = qryBase + " select XFinal.Asset_Code ,max( XFinal.Asset_Name) as Asset_Name , max(XFinal.Asset_Specification) as Asset_Specification, max(XFinal.Loc_Code) as Loc_Code,max(XFinal.Location_Desc) as Location_Desc,max(XFinal.Vendor_Code) as Vendor_Code,max(XFinal.Vendor_Name) as Vendor_Name ,max(XFinal.Acquisition_Date) as Acquisition_Date, max( XFinal.Acquisition_Code) as Acquisition_Code, max(XFinal.Templete_Code) as Templete_Code,max(XFinal.Category_Code) as Category_Code ,max(XFinal.CatDesc) as CatDesc,max(XFinal.Group_Code) as Group_Code,max(XFinal.GrpDesc) as GrpDesc,max(XFinal.AcSet_Code) as AcSet_Code,max(XFinal.CostCenter_Code) as CostCenter_Code,max(XFinal.CostCenter_Name) as CostCenter_Name,max(XFinal.Dep_Method_Code) as Dep_Method_Code ,max(XFinal.Dep_period_Code) as Dep_period_Code ,max(XFinal.[Start_Date]) as [Start_Date], max(Dep_Rate) as Dep_Rate ,max(Dep_Tax_Rate) as Dep_Tax_Rate, max(Book_Estimated_Life) as Book_Estimated_Life ,max (OriginalBookValue) as OriginalBookValue ,max(SRN_No) as SRN_No ,max( [Document Date]) as [Document Date] ,max(BookValue) as BookValue , max( [Opening Depreciation]) as [Opening Depreciation], max(BookValue)+max(Addition_amount) -max( isnull([Opening Depreciation],0) ) as Opening_Value  , max(Addition_amount) as Addition_amount,sum(Dep_Amount) as Dep_Amount , sum( [Total Depreciation]) as [Total Depreciation],max(DepValueFinal) as DepValueFinal ,max(DepRate) as DepRate,min ( Asset_Value) as Asset_Value ,max(Book_Salvage_Value) as Book_Salvage_Value,sum(AdjustAmount) as AdjustAmount,sum(AdjustAmountTax) as AdjustAmountTax from ( " + qry + " )XFinal group by XFinal.Asset_Code  order by XFinal.Asset_Code "
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
            ElseIf rdbDetail.IsChecked Then
                qry = qryBase + qry + " order by Asset_Code, Document_Date asc "
                dt = clsDBFuncationality.GetDataTable(qry)
                GV1.DataSource = Nothing
                GV1.GroupDescriptors.Clear()
                GV1.MasterTemplate.SummaryRowsBottom.Clear()
                GV1.BestFitColumns()
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    RadMessageBox.Show("No Data Found to Display", Me.Text)
                    Exit Sub
                Else
                    GV1.DataSource = dt
                    SetGridFormationOFGV1()
                    ReStoreGridLayout()
                    RadPageView1.SelectedPage = RadPageViewPage2
                End If
            End If

            If GV1.Columns.Contains("Book_Salvage_Value") Then
                GV1.Columns("Book_Salvage_Value").HeaderText = "Book Salvage Value"
                GV1.Columns("Book_Salvage_Value").IsVisible = True
            End If

            If GV1.Columns.Contains("Book_Salvage_Rate") Then
                GV1.Columns("Book_Salvage_Rate").HeaderText = "Book Salvage Rate"
                GV1.Columns("Book_Salvage_Rate").IsVisible = True
            End If

            If GV1.Columns.Contains("InvoiceNo") Then
                GV1.Columns("InvoiceNo").HeaderText = "Vendor Invoice No"
                GV1.Columns("InvoiceNo").IsVisible = True
            End If

            If GV1.Columns.Contains("InvoiceDate") Then
                GV1.Columns("InvoiceDate").HeaderText = "Vendor Invoice Date"
                GV1.Columns("InvoiceDate").IsVisible = True
            End If

            If GV1.Columns.Contains("Asset_Disposal_Code") Then
                GV1.Columns("Asset_Disposal_Code").HeaderText = "Disposal Code"
                GV1.Columns("Asset_Disposal_Code").IsVisible = True
            End If


            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)


            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcel("Asset Register" + IIf(rdbDetail.IsChecked, " ( Detail ) ", " ( Summary ) "), GV1, arrHeader, Me.Text)
            ElseIf exporter = EnumExportTo.PDF Then
                clsCommon.MyExportToPDF("Asset Register" + IIf(rdbDetail.IsChecked, " ( Detail ) ", " ( Summary ) "), GV1, arrHeader, "Asset Register Report", True)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try


    End Sub

    Private Sub txtAssetCodeFinder__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtAssetCodeFinder._MYValidating
        Dim qry As String = "select Asset_Code as Code,Asset_Name as Name FROM TSPL_ACQUISITION_DETAIL"
        Dim whrcls As String = " "
        txtAssetCodeFinder.Value = clsCommon.ShowSelectForm("assetRegister@find", qry, "Code", whrcls, txtAssetCodeFinder.Value, "Code", isButtonClicked)
        lblAssetCodeFinder.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Asset_Name as Name from TSPL_ACQUISITION_DETAIL where Asset_Code ='" + txtAssetCodeFinder.Value + "' "))
    End Sub
End Class
