Imports common
Imports System.ComponentModel
Imports System.IO

'by Sanjay - Create new report 
Public Class rptTruckSheetDailySummaryReport
    Inherits FrmMainTranScreen

    Dim StrPermission As String
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExp.Visible = MyBase.isExport
    End Sub

    Private Sub RptInventoryMovement_Load(sender As Object, e As EventArgs) Handles Me.Load
        StrPermission = clsERPFuncationality.UserWiseAvailableLocationCode()
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        Reset()
    End Sub
    Sub Reset()
        EnableDisableControl(True)
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub EnableDisableControl(ByVal val As Boolean)
        txtMCC.Enabled = val
        fromDate.Enabled = val
        ToDate.Enabled = val
        btnGo.Enabled = val
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = clsCommon.myCstr(MyBase.Form_ID)
        Print(False)
    End Sub
    Sub Print(ByVal isPrint As Boolean, Optional ByVal isPrerint As Boolean = False)
        Try

            'Dim strDate As String = clsDBFuncationality.getSingleValue(" Declare @colsScheme As NVARCHAR(MAX),@query  As NVARCHAR(MAX) with dates_cte(Date) as (select convert(date,'" + fromDate.Value + "',103) union all select dateadd(day,1,date) from dates_cte where convert(date,date,103)<convert(date,'" + ToDate.Value + "',103)) select  STUFF((Select distinct ',' + QUOTENAME(convert(varchar,dates_cte.Date,103) ) as Alies_Name FROM dates_cte order by Alies_Name FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') option (maxrecursion 0)")
            'Dim strDateSum As String = clsDBFuncationality.getSingleValue(" Declare @colsScheme As NVARCHAR(MAX),@query  As NVARCHAR(MAX)  with dates_cte(Date) as (select convert(date,'" + fromDate.Value + "',103) union all select dateadd(day,1,date) from dates_cte where convert(date,date,103)<convert(date,'" + ToDate.Value + "',103)) select  STUFF((SELECT distinct ',' +'Sum(isnull(' + QUOTENAME(convert(varchar,dates_cte.Date,103)) +',0))' +' as ' + QUOTENAME(convert(varchar,dates_cte.Date,103)) as Alies_Name FROM dates_cte order by Alies_Name FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') option (maxrecursion 0)")

            'qry = "select [Plant],[Mcc],[Shift]," + strDateSum + " from (SELECT isnull(tspl_location_master.location_desc,'') as [Plant],isnull(TSPL_MCC_MASTER.mcc_name,'') as [Mcc],tspl_location_master.Location_Code,TSPL_MILK_SRN_HEAD.MCC_CODE,convert(varchar,TSPL_MILK_SRN_HEAD.doc_date,103) as doc_date,TSPL_MILK_SRN_HEAD.shift,(case when TSPL_MILK_SRN_HEAD.shift='M' THEN '1'+TSPL_MILK_SRN_HEAD.shift ELSE '2'+TSPL_MILK_SRN_HEAD.shift END) as shift1,cast(TSPL_MILK_SRN_DETAIL.ACC_Qty as decimal(18,2)) AS Quantity from TSPL_MILK_SRN_DETAIL left outer join  TSPL_MILK_SRN_HEAD On  TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.mcc_code=TSPL_MILK_SRN_HEAD.MCC_CODE left outer join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.Plant_code " &
            '    " where CONVERT(date,TSPL_MILK_SRN_HEAD.Doc_Date,103) >= convert(date,'" + fromDate.Value + "',103) AND  CONVERT(date,TSPL_MILK_SRN_HEAD.Doc_Date,103) <= convert(date,'" + ToDate.Value + "',103) "
            'If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
            '    qry += " and TSPL_MILK_SRN_HEAD.MCC_CODE  IN (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") "
            'End If
            'qry += " ) as s pivot (  sum(Quantity) for doc_date in (" + strDate + " ) ) as zpivot group by zpivot.[Plant],zpivot.[Mcc],zpivot.[Shift],zpivot.[Shift1] order by [Plant],[Mcc],[Shift1]"

            Dim dtREJECT As DataTable
            Dim dt1 As New DataTable
            Dim qry As String = Nothing
            Dim FinalQuery As String = Nothing
            Dim strRejection As String = Nothing
            Dim strSRNQuery As String = Nothing
            Dim strRejectionQuery As String = Nothing
            strRejection = ",'' as RejectType,'' as RejectReason,'' as Defaulter"
            Dim ShowVLCUploaderData As Boolean = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowVLCUploaderData, clsFixedParameterCode.ShowVLCUploaderData, Nothing)) = 1
            strSRNQuery = clsMilkRejectHead.GetMCCRegisterWithRejectionColumnQuery(fromDate.Value, ToDate.Value, "M", "E", "", StrPermission, txtMCC.arrValueMember, Nothing, Nothing, "", strRejection, ShowVLCUploaderData)

            strRejectionQuery = clsMilkRejectHead.GetMCCRegisterRejectionQuery(fromDate.Value, ToDate.Value, "M", "E", StrPermission, txtMCC.arrValueMember, Nothing, Nothing, "")

            qry = "Select final.[Milk Receipt Code] ,final.MCC as [MCC Code] ,final.[MCC Name],final.[MCC Type] ,final.[Chilling Center],final.[Plant Code],final.[Plant Name] ,final.Date ,final.[Doc Date] ,final.Shift ," &
                " final.[Route Code],final.[Route Name] ,final.[Vehicle Code] ,final.[VSP Code],final.[VSP Name], final.[Vendor Group Code],final.[Vendor Group Desc] ,final.[Vlc Uploader Code] ,final.[Vlc Code] ,final.[VLC Name] ," &
                " final.[Sample No] ,final.[No Of Cans],final.Item_Code,final.Item_Desc,final.[Milk Weight],final.UOM_Code as [UOM],final.[Milk Weight(KG)]," &
                " final.[Milk Weight(LTR)]  as [Milk Weight(LTR)]," &
                " final.[FAT(%)]  ,final.CLR,final.[SNF(%)] ,final.[FAT(KG)],final.[SNF(KG)] ,final.[Cow Milk Qty (KG)],final.[Cow FAT(%)], Case When final.[FAT(%)] <= 5 Then CLR Else 0 End [Cow CLR],final.[Cow SNF(%)] , Case When final.[FAT(%)] <= 5 Then final.[FAT(KG)] Else 0 End [Cow FAT (KG)], Case When final.[FAT(%)] <= 5 Then final.[SNF(KG)] Else 0 End [Cow SNF (KG)]," &
                " final.[Buffalo Milk Qty (KG)], Case When final.[FAT(%)] > 5 Then CLR Else 0 End [Buffalo CLR],final.[Buffalo SNF(%)],final.[Buffalo FAT(%)], Case When final.[FAT(%)] > 5 Then final.[FAT(KG)] Else 0 End [Buffalo FAT (KG)], Case When final.[FAT(%)] > 5 Then final.[SNF(KG)] Else 0 End [Buffalo SNF (KG)],final.[Milk Type],final.[SRN No],final.[SRN Amount]," &
                " final.[SRN Qty],final.[SRN Rate],final.[Shift Status] ,Invoice_no ,Invoice_Date , IS_MANUAL, MACHINE_NO,IS_MILK_SAMPLE_MANUAL,RejectType,RejectReason,Defaulter, " &
                " final.EMP_Amount,final.TIP_Amount,final.Service_Charge_Amount ,([SRN Amount]+EMP_Amount+TIP_Amount-Service_Charge_Amount) as NetAmount,final.Purchase_Order_No,final.Head_Load_Amount ,final.SNF_Ded_Value,final.SNF_Ded_Rate,final.SNF_Ded_Amount, final.price_code,final.[Transporter Code],final.[Transporter Name],final.Handling_Charges_Amount,final.VSP_Commission_Amount,final.VSP_Deduction_Amount,final.VSP_Day_Wise_Incentive,final.SubStandard,final.vehicle  From ( " & strSRNQuery & " Union All " & strRejectionQuery & ") As final where 2=2 "

            'Dim qry1 As String = "select TSPL_MILK_REJECT_TYPE.code as Reject_Type,TSPL_MILK_REJECT_TYPE.Description as Description,TSPL_MILK_REJECT_TYPE.SNo as SNo from TSPL_MILK_REJECT_TYPE where 1=1 order by SNo"
            'dtREJECT = clsDBFuncationality.GetDataTable(qry1)
            'Dim strRejectQty As String = ""
            'Dim strRejectPer1 As String = ""
            'Dim strRejectsum As String = ""
            'Dim strRejectPer2 As String = ",'' as [FAT - Per],'' as [SNF - Per]"
            'If dtREJECT IsNot Nothing AndAlso dtREJECT.Rows.Count > 0 Then
            '    For Each dr As DataRow In dtREJECT.Rows
            '        strRejectQty += ",case When isnull(RejectType,'')='" + clsCommon.myCstr(dr("Description")) + "' then [Milk Weight] else 0 end as [" + clsCommon.myCstr(dr("Description")) + "]"
            '        strRejectPer1 += ",case When sum([Milk Weight])>0 then cast((sum([" + clsCommon.myCstr(dr("Description")) + "])/sum([Milk Weight]))*100 as decimal(18,2)) else 0 end as [" + clsCommon.myCstr(dr("Description")) + " %]"
            '        strRejectsum += ",sum([" + clsCommon.myCstr(dr("Description")) + "]) as [" + clsCommon.myCstr(dr("Description")) + "]"
            '        strRejectPer2 += ",'' as [" + clsCommon.myCstr(dr("Description")) + " - Per]"
            '    Next
            'End If

            'FinalQuery = "select [MCC Code],[MCC Name],[Route Code],[Route Name]
            ' ,ROW_NUMBER() OVER(Partition by [MCC Name],[Route Name] ORDER BY [MCC Name],[Route Name]) AS SNo
            ',[VSP Code] as [SOCIETY CODE],[VSP Name] as [SOCIETY NAME],[Milk Type]
            ',sum([Milk Weight]) as [Milk Weight],sum([FAT]) as [FAT],sum([SNF]) as [SNF]" + strRejectsum + " ,case when sum([Milk Weight] )=0 then 0 else (sum([FAT] )/sum([Milk Weight] ))*100 end as [FAT(%)]
            ',case when sum([Milk Weight] )=0 then 0 else (sum([SNF] )/sum([Milk Weight] ))*100 end as [SNF(%)] " + strRejectPer1 + " " + strRejectPer2 + " from(select [MCC Code],[MCC Name],[Route Code],[Route Name]
            ',[VSP Code],[VSP Name],[Milk Type]
            ',[Milk Weight],[FAT], [SNF]" + strRejectQty + "
            'from (select pp.[MCC Code]  as [MCC Code],max(pp.[MCC Name] )  as [MCC Name]
            ',pp.[Route Code],max(pp.[Route Name]) as [Route Name]
            ',pp.[VSP Code],max(pp.[VSP Name]) as [VSP Name]
            ',pp.[Milk Type]
            ',sum([Milk Weight(KG)] ) as [Milk Weight]
            ',sum([FAT(KG)] ) as [FAT] ,sum([SNF(KG)] ) as [SNF]
            ',RejectType
            'from (" + Environment.NewLine + qry + Environment.NewLine + " ) as  pp group by pp.[MCC Code],pp.[Route Code],pp.[VSP Code],pp.[Milk Type],pp.RejectType 
            '  ) as aa )a where 1=1 group by [MCC Code],[MCC Name],[Route Code],[Route Name]
            ',[VSP Code],[VSP Name],[Milk Type] order by [MCC Name],[Route Name],[VSP Code]"

            '            ,case when sum([Milk Weight] )=0 then 0 else (sum([FAT] )/sum([Milk Weight] ))*100 end as [Total FAT(%)]
            ',case when sum([Milk Weight] )=0 then 0 else (sum([SNF] )/sum([Milk Weight] ))*100 end as [Total SNF(%)] 

            FinalQuery = "select [Doc Date],[Shift],case when sum([Milk Weight])=0 then 0 else 
(case when sum(SOUR)=0 and sum(CURD)=0 then sum([Milk Weight]) else 0 end) end as [Sweet Qty]
,case when sum([Milk Weight])=0 then 0 else 
(case when sum(SOUR)=0 and sum(CURD)=0 then sum([FAT]) else 0 end) end as [Sweet FAT]			
,case when sum([Milk Weight])=0 then 0 else 
(case when sum(SOUR)=0 and sum(CURD)=0 then sum([SNF]) else 0 end) end as [Sweet SNF] 
,case when sum([Milk Weight])=0 then 0 else 
(case when sum(SOUR)=0 and sum(CURD)=0 then cast( (sum([FAT] )/sum([Milk Weight] ))*100 as decimal(18,2)) else 0 end) end as [Sweet FAT(%)]			
,case when sum([Milk Weight])=0 then 0 else 
(case when sum(SOUR)=0 and sum(CURD)=0 then cast( (sum([SNF])/sum([Milk Weight] ))*100 as decimal(18,2)) else 0 end) end as [Sweet SNF(%)] 	
,sum(SOUR) as [SOUR Qty]
,case when sum(SOUR)>0 then cast((sum([FAT])/sum([Milk Weight] ))*100 as decimal(18,2)) else 0 end as [SOUR FAT(%)] 
,case When sum(SOUR)>0 then cast((sum([SNF])/sum([Milk Weight]))*100 as decimal(18,2)) else 0 end as [SOUR SNF(%)]
,case when sum(SOUR)>0 then sum([FAT])  else 0 end as [SOUR FAT] 
,case When sum(SOUR)>0 then sum([SNF]) else 0 end as [SOUR SNF]
,sum(CURD) as [CURD Qty],sum([Milk Weight]) as [Total Qty]
,case when sum([Milk Weight] )=0 then 0 else (sum([FAT] )) end as [Total FAT Kg]
,case when sum([Milk Weight] )=0 then 0 else (sum([SNF] )) end as [Total SNF Kg] 
,sum(NetAmount) as Amount
from(select [Milk Weight],[FAT], [SNF]
,case When isnull(RejectType,'')='SOUR' then [Milk Weight] else 0 end as [SOUR]
,case When isnull(RejectType,'')='CURD' then [Milk Weight] else 0 end as [CURD]
,NetAmount,[Doc Date],[Shift]
 from (
 select sum([Milk Weight(KG)] ) as [Milk Weight]
,sum([FAT(KG)] ) as [FAT] ,sum([SNF(KG)] ) as [SNF],RejectType,[Doc Date],[Shift],sum(NetAmount) as NetAmount
from (" + qry +
"  ) as  pp group by  [Doc Date],[Shift],pp.RejectType ) as aa )a where 1=1
 group by [Doc Date],[Shift] order by [Doc Date],[Shift]"

            dt1 = Nothing
            dt1 = clsDBFuncationality.GetDataTable(FinalQuery)
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()

            If dt1 Is Nothing OrElse dt1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            Else
                Gv1.DataSource = dt1
                RadPageView1.SelectedPage = RadPageViewPage2
                btnGo.Enabled = False
                SetGridFormat()
                ReStoreGridLayout()
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub SetGridFormat()
        'Gv1.GroupDescriptors.Add(New GridGroupByExpression("Plant as Plant format ""{0}: {1}"" Group By Plant"))
        'Gv1.GroupDescriptors.Add(New GridGroupByExpression("Mcc as Mcc format ""{0}: {1}"" Group By Mcc"))
        Gv1.AutoExpandGroups = True
        Gv1.ShowGroupPanel = False
        Gv1.ShowRowHeaderColumn = False
        Gv1.AllowAddNewRow = False
        Gv1.AllowDeleteRow = False
        Gv1.EnableFiltering = True
        Gv1.ShowFilteringRow = True

        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).BestFit()
        Next

        Gv1.Columns("Sweet FAT").IsVisible = False
        Gv1.Columns("Sweet SNF").IsVisible = False
        Gv1.Columns("SOUR FAT").IsVisible = False
        Gv1.Columns("SOUR SNF").IsVisible = False

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Gv1.Columns("Sweet Qty").FormatString = "{0:n2}"
        Dim item1 As New GridViewSummaryItem("Sweet Qty", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Gv1.Columns("SOUR Qty").FormatString = "{0:n2}"
        Dim item2 As New GridViewSummaryItem("SOUR Qty", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Gv1.Columns("CURD Qty").FormatString = "{0:n2}"
        Dim item3 As New GridViewSummaryItem("CURD Qty", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Gv1.Columns("Total Qty").FormatString = "{0:n2}"
        Dim item4 As New GridViewSummaryItem("Total Qty", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)

        Gv1.Columns("Total FAT Kg").FormatString = "{0:n3}"
        Dim item5 As New GridViewSummaryItem("Total FAT Kg", "{0:n3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Gv1.Columns("Total SNF Kg").FormatString = "{0:n3}"
        Dim item6 As New GridViewSummaryItem("Total SNF Kg", "{0:n3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)
        Gv1.Columns("Amount").FormatString = "{0:n2}"
        Dim item7 As New GridViewSummaryItem("Amount", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)

        Dim summaryItem1 As New GridViewSummaryItem()
        summaryItem1.FormatString = "{0:n2}"
        summaryItem1.Name = "Sweet FAT(%)"
        summaryItem1.AggregateExpression = "sum([Sweet FAT])*100/sum([Sweet Qty])"
        summaryRowItem.Add(summaryItem1)

        Dim summaryItem2 As New GridViewSummaryItem()
        summaryItem2.FormatString = "{0:n2}"
        summaryItem2.Name = "Sweet SNF(%)"
        summaryItem2.AggregateExpression = "sum([Sweet SNF])*100/sum([Sweet Qty])"
        summaryRowItem.Add(summaryItem2)

        Dim summaryItem3 As New GridViewSummaryItem()
        summaryItem3.FormatString = "{0:n2}"
        summaryItem3.Name = "SOUR FAT(%)"
        summaryItem3.AggregateExpression = "sum([SOUR FAT])*100/sum([SOUR Qty])"
        summaryRowItem.Add(summaryItem3)

        Dim summaryItem4 As New GridViewSummaryItem()
        summaryItem4.FormatString = "{0:n2}"
        summaryItem4.Name = "SOUR SNF(%)"
        summaryItem4.AggregateExpression = "sum([SOUR SNF])*100/sum([SOUR Qty])"
        summaryRowItem.Add(summaryItem4)

        'If Gv1.Rows.Count > 0 Then
        '    Dim summaryRowItem As New GridViewSummaryRowItem()
        '    For i As Integer = 3 To Gv1.Columns.Count - 1
        '        Dim aa = Gv1.Columns(i).HeaderText()
        '        Dim item1 As New GridViewSummaryItem(aa, "{0:F2}", GridAggregateFunction.Sum)
        '        summaryRowItem.Add(item1)
        '    Next
        '    Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        'End If
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Gv1.AutoSizeRows = True
        Gv1.BestFitColumns()
        Gv1.MasterTemplate.AutoExpandGroups = True
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim StrReportName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptTruckSheetDailySummaryReport & "'"))
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : " & StrReportName)
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            If txtMCC.arrDispalyMember IsNot Nothing AndAlso txtMCC.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Mcc : " + clsCommon.GetMulcallStringWithComma(txtMCC.arrDispalyMember))
            End If
            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid(StrReportName, Gv1, arrHeader, Me.Text)
            Else
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                ' clsCommon.MyExportToPDF(StrReportName, Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)

                Dim doc As New clsMyPrintDocument()

                doc.Margins.Top = 50
                    doc.Margins.Bottom = 50
                    doc.Margins.Left = 50
                    doc.Margins.Right = 50
                    doc.HeaderHeight = 90
                doc.Landscape = True
                doc.AssociatedObject = Gv1

                doc.DocumentName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptTruckSheetDailySummaryReport & "'"))
                doc.MiddleHeader = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptTruckSheetDailySummaryReport & "'"))
                doc.HeaderFont = New Font("Segoe UI", 10, FontStyle.Bold)

                doc.LeftUpperText = "Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")
                doc.LeftUpperFont = New Font("Segoe UI", 8, FontStyle.Regular)

                If txtMCC.arrDispalyMember IsNot Nothing AndAlso txtMCC.arrDispalyMember.Count > 0 Then
                    doc.LeftMiddleText = "Mcc : " + clsCommon.GetMulcallStringWithComma(txtMCC.arrDispalyMember)
                    doc.LeftLowerFont = New Font("Segoe UI", 8, FontStyle.Regular)
                End If

                doc.AssociatedObject = Gv1
                    'doc.Print()
                    doc.RightFooter = "Page [Page #] of [Total Pages]"

                    Dim dialog As New RadPrintPreviewDialog
                    dialog.Document = doc
                    dialog.ToolMenu.Visible = True
                    dialog.ShowDialog()
                doc = Nothing

            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        ExportGrid(EnumExportTo.PDF)
    End Sub


    Private Sub TxtMCC__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
        Dim qry As String = "select MCC_Code as [MCC Code],MCC_NAME as [MCC Name],TSPL_MCC_MASTER.plant_code as [Plant Code],tspl_location_master.location_desc as [Plant Name] from TSPL_MCC_MASTER left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.plant_code where tspl_mcc_master.mcc_Code in (" & StrPermission & ")"
        txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("@TSDSR1", qry, "MCC Code", "MCC Name", txtMCC.arrValueMember, txtMCC.arrDispalyMember)
    End Sub

    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = Gv1.ColumnCount
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

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
End Class
