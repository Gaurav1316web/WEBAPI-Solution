Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO


Public Class rptMccDeductionReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim strQry As String = ""

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub


    Private Sub RptInventoryMovement_Load(sender As Object, e As EventArgs) Handles Me.Load
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
    End Sub
    Sub Reset()

        txtVLC.arrValueMember = Nothing
        txtLocation.arrValueMember = Nothing
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            PageSetupReport_ID = MyBase.Form_ID
            TemplateGridview = Gv1
            Dim qry As String = ""
            Dim dt As New DataTable
            Dim strDeductionNameForPivot As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select STUFF((Select ',['+Description+']'  from (select TSPL_DEDUCTION_MASTER.Description from TSPL_DEDUCTION_MASTER   ) XXX For XML Path('')),1,1,'') "))
            If clsCommon.myLen(strDeductionNameForPivot) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
            Dim strDeductionNameWithIsNull As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select STUFF((Select ',isnull( ['+Description+'],0) as ' + '['+Description+']' from (select TSPL_DEDUCTION_MASTER.Description from TSPL_DEDUCTION_MASTER   ) XXX For XML Path('')),1,1,'') "))
            Dim strDeductionNameWithSum As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select STUFF((Select ',sum(isnull( ['+Description+'],0)) as ' + '['+Description+']' from (select TSPL_DEDUCTION_MASTER.Description from TSPL_DEDUCTION_MASTER   ) XXX For XML Path('')),1,1,'') "))
            Dim strDeductionNameForTotal As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select STUFF((Select '+ isnull( ['+Description+'],0)' from (select TSPL_DEDUCTION_MASTER.Description from TSPL_DEDUCTION_MASTER   ) XXX For XML Path('')),1,1,'') "))

            Dim whrForMulDed As String = " and 2= 2 "
            Dim whrForSingleDed As String = " and 2= 2 "

            If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
                whrForMulDed += " and TSPL_VLC_MASTER_HEAD.VLC_Code  in (" + clsCommon.GetMulcallString(txtVLC.arrValueMember) + ") "
                whrForSingleDed += " and TSPL_VLC_MASTER_HEAD.VLC_Code  in (" + clsCommon.GetMulcallString(txtVLC.arrValueMember) + ") "
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                whrForMulDed += " and TSPL_MULTIPLE_DEDUCTION_HEAD.MCC_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
                whrForSingleDed += " and TSPL_VLC_MASTER_HEAD.MCC in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
            End If

            If ChkPosted.IsChecked = True Then
                whrForMulDed += " and TSPL_MULTIPLE_DEDUCTION_HEAD.IsPosted = 1 "
                whrForSingleDed += " and  len (TSPL_VENDOR_INVOICE_HEAD.Posting_Date) > 0  "
            ElseIf ChkUnPosted.IsChecked = True Then
                whrForMulDed += " and TSPL_MULTIPLE_DEDUCTION_HEAD.IsPosted = 0 "
                whrForSingleDed += " and  len (TSPL_VENDOR_INVOICE_HEAD.Posting_Date) <= 0  "
            End If

            'select TSPL_MULTIPLE_DEDUCTION_HEAD.Loc_Code as [Plant Code],(TSPL_GL_SEGMENT_CODE.Description) as [Plant Name] , TSPL_MULTIPLE_DEDUCTION_HEAD.MCC_Code as [MCC Code],(TSPL_LOCATION_MASTER.Location_Desc) as [MCC Name] ,convert (varchar, TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,103) as Document_Date, TSPL_MULTIPLE_DEDUCTION_DETAIL.Vendor_Code , (TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader) as [VLC Uploader Code],TSPL_MULTIPLE_DEDUCTION_DETAIL.DeductionCode  , (TSPL_DEDUCTION_MASTER.Description) as DeductionName, (TSPL_MULTIPLE_DEDUCTION_DETAIL.Amount) as Amount from TSPL_MULTIPLE_DEDUCTION_DETAIL left outer join TSPL_MULTIPLE_DEDUCTION_HEAD on TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No = TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_MULTIPLE_DEDUCTION_DETAIL.Vendor_Code  and TSPL_VLC_MASTER_HEAD.MCC = TSPL_MULTIPLE_DEDUCTION_HEAD.MCC_Code left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code = TSPL_MULTIPLE_DEDUCTION_DETAIL.DeductionCode left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code = TSPL_MULTIPLE_DEDUCTION_HEAD.Loc_Code left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_MULTIPLE_DEDUCTION_HEAD.MCC_Code 
            '        where convert(date,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date ,103)>=convert(date,'" + fromDate.Value + "',103) AND convert(date,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,103)<=convert(date,'" + ToDate.Value + "',103)  " + whrForMulDed + " 
            '        Union all
            qry = " select [Plant Code],MAX([Plant Name]) as [Plant Name] , [MCC Code], max([MCC Name]) as [MCC Name],[Document Date],[Vendor Code],[VLC Uploader Code], " + strDeductionNameWithSum + " , sum ([Total]) as [Total] from (
                    select  [Plant Code] , [Plant Name] , [MCC Code],[MCC Name],Document_Date as [Document Date],Vendor_Code as [Vendor Code],[VLC Uploader Code], " + strDeductionNameWithIsNull + " , " + strDeductionNameForTotal + " as [Total]
                    from (
                    select Final.[Plant Code], max(Final. [Plant Name]) as [Plant Name] , Final. [MCC Code],max(Final.[MCC Name]) as  [MCC Name],Final.Document_Date , Final.Vendor_Code , max(Final.[VLC Uploader Code]) as [VLC Uploader Code],Final.DeductionCode  , max(Final.DeductionName) as DeductionName, sum(Final. Amount)as Amount from (
                    
                    select TSPL_VENDOR_INVOICE_HEAD.Loc_Code  as [Plant Code], TSPL_GL_SEGMENT_CODE.Description as [Plant Name], TSPL_VLC_MASTER_HEAD.MCC as [MCC Code], TSPL_LOCATION_MASTER.Location_Desc as [MCC Name], convert (varchar,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) as Document_Date, TSPL_VENDOR_INVOICE_HEAD.Vendor_Code , (TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader) as [VLC Uploader Code],TSPL_VENDOR_INVOICE_DETAIL.DeductionCode  , (TSPL_DEDUCTION_MASTER.Description) as DeductionName, (TSPL_VENDOR_INVOICE_DETAIL.Total_Amount) as Amount   from TSPL_VENDOR_INVOICE_DETAIL  left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_DETAIL.Document_No = TSPL_VENDOR_INVOICE_HEAD.Document_No  left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code = TSPL_VENDOR_INVOICE_HEAD.Loc_Code  left outer Join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_VENDOR_INVOICE_HEAD.Vendor_Code left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_VLC_MASTER_HEAD.MCC  left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code = TSPL_VENDOR_INVOICE_DETAIL.DeductionCode where TSPL_VENDOR_INVOICE_HEAD.isDeduction = 1
                     and convert(date,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date ,103)>=convert(date,'" + fromDate.Value + "',103) AND convert(date,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103)<=convert(date,'" + ToDate.Value + "',103)  " + whrForSingleDed + "
                    ) Final group by Final.[Plant Code], Final.[MCC Code], Final.Document_Date, Final.Vendor_Code,Final.DeductionCode
                    )  XFinal 
                    pivot
                    (
                    sum(XFinal.Amount)
                    for XFinal.DeductionName in (" + strDeductionNameForPivot + ")
                    ) piv  )Final group by [Plant Code] , [MCC Code], [Document Date],[Vendor Code],[VLC Uploader Code] "

            dt = clsDBFuncationality.GetDataTable(qry)
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()

            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                Gv1.DataSource = dt
                For ii As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(ii).ReadOnly = True
                Next
                ' Gv1.Columns("Trans Type").IsVisible = False
                RadPageView1.SelectedPage = RadPageViewPage2

                'Gv1.Columns("Amount").FormatString = "{0:n2}"
                'Dim summaryRowItem As New GridViewSummaryRowItem()
                'Dim itemQty As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(itemQty)
                'Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

                If Gv1.Rows.Count > 0 Then
                    Dim summaryRowItem As New GridViewSummaryRowItem()
                    For i As Integer = 7 To Gv1.Columns.Count - 1
                        Dim aa = Gv1.Columns(i).HeaderText()
                        Dim item1 As New GridViewSummaryItem(aa, "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item1)
                    Next
                    Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                    Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
                End If
                Gv1.EnableFiltering = True
                Gv1.BestFitColumns()

            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= Gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To Gv1.Columns.Count - 1 Step ii + 1
                        Gv1.Columns(ii).IsVisible = False
                        Gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    Gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    'Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtVLC._My_Click
    '    Dim qry As String
    '    qry = " select Item_Code,Item_Desc from TSPL_ITEM_MASTER  order by Item_Code "
    '    txtVLC.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel@Batch", qry, "Item_Code", "Item_Desc", txtVLC.arrValueMember, txtVLC.arrDispalyMember)
    'End Sub
    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " select Location_Code as [Code],Location_Desc as [Name],Loc_Segment_Code as [LocationSegmentCode],Add1,Add2,Add3,Add4,City_Code as [City Code],State,Pin_Code as [Pin Code],Country,Hoadd1,hoadd2,Telphone,Email,Location_Type as [Location Type],Loc_Status as [Location Status],Status_Date as [Status Date],Excisable,Type,Purchase_Tax_Group as [Purchase Tax Group],Sales_Tax_Group as [Sales Tax Group],Ecc_Number as [ECC Number],Registration_Number as [Registration Number],Commissionerate as [Commission Rate],Range_Code as [Range Code],Range_Name as [Range Name],Range_Address as [Range Address],Division_Code as [Division Code],Division_Name as [Division Name],Division_Address as [Division Address],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_code as [Company Code],TIN_No as [TIN No],TAN_No as [TAN No],TCAN_No as [TCAN No],Service_Tax_Reg_No as [Service Tax Registration No],DutyPaid as [Duty Paid],Purchase_Tax_GroupIS as [Purchase Tax Group Inter State],Sales_Tax_GroupIS as [Sales Tax Group Inter State],Stock_Transfer_Filled_Ac as [Stock Transfer Filled Account],Stock_Transfer_Empty_Ac as [Stock Transfer Empty Account],GIT_Location as [GIT Location],GIT_Type as [GIT Type],Rejected_Type as [Rejected Type],Rejected_Location as [Rejected Location],CSA_Type as [CSA Type],Cust_Code as [Cust Code],CST_No as [CST No],Phone1,Phone2,stock_transfer_ac as [Stock Tranfer A/C],Loss_Ac as [Loss A/C]  from TSPL_Location_MASTER where isnull(Loc_Segment_Code ,'')<>''  AND isnull(GIT_Type,'') <>'Y' and Location_Type='Physical' and IsMainPlant='1' and Loc_Segment_Code in (" + objCommonVar.strCurrUserLocationsSegment + ")"
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("Location@MulpledeductionReport", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub
    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        Dim ReportID As String = MyBase.Form_ID
        If clsCommon.myLen(ReportID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = Gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If


            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub


    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptMccDeductionReport & "'"))
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If
            'If txtVLC.arrDispalyMember IsNot Nothing AndAlso txtVLC.arrDispalyMember.Count > 0 Then
            '    arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtVLC.arrDispalyMember))
            'End If

            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("MCC Deduction Report", Gv1, arrHeader, Me.Text)
            Else
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("MCC Deduction Report", Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Export(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Export(EnumExportTo.PDF)
    End Sub

    Private Sub txtVLC__My_Click(sender As Object, e As EventArgs) Handles txtVLC._My_Click
        Dim qry As String = " select VLC_Code as Code, VLC_Name as Name , VSP_Code as [Secretary Code],TSPL_VENDOR_MASTER.Vendor_Name as [Secretary Name], VLC_Code_VLC_Uploader as [DCS Uploader Code] from TSPL_VLC_MASTER_HEAD left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = TSPL_VLC_MASTER_HEAD.VSP_Code "
        txtVLC.arrValueMember = clsCommon.ShowMultipleSelectForm("VSP@MulpledeductionReport", qry, "Code", "Name", txtVLC.arrValueMember, txtVLC.arrDispalyMember)
    End Sub
End Class
