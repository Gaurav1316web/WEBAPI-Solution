Imports common
Imports System.ComponentModel
Imports System.IO

'by Sanjay - Create new report 
Public Class rptMilkBillProcurementSummary
    Inherits FrmMainTranScreen

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExp.Visible = MyBase.isExport
    End Sub
    Dim StrPermission As String
    Dim dtREJECT As DataTable
    Private Sub rptMilkBillProcurementSummary_Load(sender As Object, e As EventArgs) Handles Me.Load
        StrPermission = clsERPFuncationality.UserWiseAvailableLocationCode()
        txtFiscalYear.Value = objCommonVar.CurrFiscalYear
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        Reset()
    End Sub
    Sub Reset()
        dtREJECT = Nothing
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        EnableDisableCtrl(True)
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = clsCommon.myCstr(MyBase.Form_ID)
        Print(False)
    End Sub
    Sub Print(ByVal isPrint As Boolean, Optional ByVal isPrerint As Boolean = False)
        Try

            Dim dt1 As New DataTable
            Dim qry As String = Nothing
            Dim FinalQuery As String = Nothing
            Dim strRejection As String = Nothing
            Dim strSRNQuery As String = Nothing
            Dim strRejectionQuery As String = Nothing
            If clsCommon.myLen(txtFiscalYear.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Plz Select Fiscal Year First.", Me.Text)
                txtFiscalYear.Focus()
                Exit Sub
            End If
            If clsCommon.myLen(txtPaymentCycleFrom.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Plz Select Payment Cycle From First.", Me.Text)
                txtPaymentCycleFrom.Focus()
                Exit Sub
            End If
            If clsCommon.myLen(txtPaymentCycleTo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Plz Select Payment Cycle To First.", Me.Text)
                txtPaymentCycleTo.Focus()
                Exit Sub
            End If

            If clsCommon.myCdbl(txtPaymentCycleFrom.Value) > clsCommon.myCdbl(txtPaymentCycleTo.Value) Then
                common.clsCommon.MyMessageBoxShow(Me, "From Payment Cycle can not be greater then to Payment Cycle")
                txtPaymentCycleFrom.Focus()
                Exit Sub
            End If
            Patment_Cycle_changed()
            strRejection = ",'' as RejectType,'' as RejectReason,'' as Defaulter"
            Dim ShowVLCUploaderData As Boolean = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowVLCUploaderData, clsFixedParameterCode.ShowVLCUploaderData, Nothing)) = 1
            Dim SetCowFatPer As Decimal = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CowFATPer, clsFixedParameterCode.CowFATPer, Nothing))
            strSRNQuery = clsMilkRejectHead.GetMCCRegisterWithRejectionColumnQuery(fromDate.Value, ToDate.Value, "M", "E", "", StrPermission, txtMCC.arrValueMember, Nothing, Nothing, "", strRejection, ShowVLCUploaderData, SetCowFatPer)

            strRejectionQuery = clsMilkRejectHead.GetMCCRegisterRejectionQuery(fromDate.Value, ToDate.Value, "M", "E", StrPermission, txtMCC.arrValueMember, Nothing, Nothing, "", SetCowFatPer)

            qry = "Select final.[Milk Receipt Code] ,final.MCC as [MCC Code] ,final.[MCC Name],final.[MCC Type] ,final.[Chilling Center],final.[Plant Code],final.[Plant Name] ,final.Date ,final.[Doc Date] ,final.Shift ," &
                " final.[Route Code],final.[Route Name] ,final.[Vehicle Code] ,final.[VSP Code],final.[VSP Name], final.[Vendor Group Code],final.[Vendor Group Desc] ,final.[Vlc Uploader Code] ,final.[Vlc Code] ,final.[VLC Name] ," &
                " final.[Sample No] ,final.[No Of Cans],final.Item_Code,final.Item_Desc,final.[Milk Weight],final.UOM_Code as [UOM],final.[Milk Weight(KG)]," &
                " final.[Milk Weight(LTR)]  as [Milk Weight(LTR)]," &
                " final.[FAT(%)]  ,final.CLR,final.[SNF(%)] ,final.[FAT(KG)],final.[SNF(KG)] ,final.[Cow Milk Qty (KG)],final.[Cow FAT(%)], Case When final.[FAT(%)] <= 5 Then CLR Else 0 End [Cow CLR],final.[Cow SNF(%)] , Case When final.[FAT(%)] <= 5 Then final.[FAT(KG)] Else 0 End [Cow FAT (KG)], Case When final.[FAT(%)] <= 5 Then final.[SNF(KG)] Else 0 End [Cow SNF (KG)]," &
                " final.[Buffalo Milk Qty (KG)], Case When final.[FAT(%)] > 5 Then CLR Else 0 End [Buffalo CLR],final.[Buffalo SNF(%)],final.[Buffalo FAT(%)], Case When final.[FAT(%)] > 5 Then final.[FAT(KG)] Else 0 End [Buffalo FAT (KG)], Case When final.[FAT(%)] > 5 Then final.[SNF(KG)] Else 0 End [Buffalo SNF (KG)],final.[Milk Type],final.[SRN No],final.[SRN Amount]," &
                " final.[SRN Qty],final.[SRN Rate],final.[Shift Status] ,Invoice_no ,Invoice_Date , IS_MANUAL, MACHINE_NO,IS_MILK_SAMPLE_MANUAL,RejectType,RejectReason,Defaulter, " &
                " final.EMP_Amount,final.TIP_Amount,final.Service_Charge_Amount ,([SRN Amount]+EMP_Amount+TIP_Amount-Service_Charge_Amount) as NetAmount,final.Purchase_Order_No,final.Head_Load_Amount ,final.SNF_Ded_Value,final.SNF_Ded_Rate,final.SNF_Ded_Amount, final.price_code,final.[Transporter Code],final.[Transporter Name],final.Handling_Charges_Amount,final.VSP_Commission_Amount,final.VSP_Deduction_Amount,final.VSP_Day_Wise_Incentive,final.SubStandard,final.vehicle  From ( " & strSRNQuery & " Union All " & strRejectionQuery & ") As final where 2=2 "

            Dim qry1 As String = "select TSPL_MILK_REJECT_TYPE.code as Reject_Type,TSPL_MILK_REJECT_TYPE.Description as Description,TSPL_MILK_REJECT_TYPE.SNo as SNo from TSPL_MILK_REJECT_TYPE where 1=1 order by SNo"
            dtREJECT = clsDBFuncationality.GetDataTable(qry1)
            Dim strRejectQty As String = ""
            Dim strRejectPer1 As String = ""
            Dim strRejectsum As String = ""
            Dim strRejectPer2 As String = ",'' as [FAT - Per],'' as [SNF - Per]"
            If dtREJECT IsNot Nothing AndAlso dtREJECT.Rows.Count > 0 Then
                For Each dr As DataRow In dtREJECT.Rows
                    strRejectQty += ",case When isnull(RejectType,'')='" + clsCommon.myCstr(dr("Description")) + "' then [Milk Weight] else 0 end as [" + clsCommon.myCstr(dr("Description")) + "]"
                    strRejectPer1 += ",case When sum([Milk Weight])>0 then cast((sum([" + clsCommon.myCstr(dr("Description")) + "])/sum([Milk Weight]))*100 as decimal(18,2)) else 0 end as [" + clsCommon.myCstr(dr("Description")) + " %]"
                    strRejectsum += ",sum([" + clsCommon.myCstr(dr("Description")) + "]) as [" + clsCommon.myCstr(dr("Description")) + "]"
                    strRejectPer2 += ",'' as [" + clsCommon.myCstr(dr("Description")) + " - Per]"
                Next
            End If

            FinalQuery = "select [MCC Code],[MCC Name],[Route Code],[Route Name]
             ,ROW_NUMBER() OVER(Partition by [MCC Name],[Route Name] ORDER BY [MCC Name],[Route Name]) AS SNo
            ,[VSP Code] as [SOCIETY CODE],[VSP Name] as [SOCIETY NAME],[Milk Type]
            ,sum([Milk Weight]) as [Milk Weight],sum([FAT]) as [FAT],sum([SNF]) as [SNF]" + strRejectsum + " ,case when sum([Milk Weight] )=0 then 0 else (sum([FAT] )/sum([Milk Weight] ))*100 end as [FAT(%)]
            ,case when sum([Milk Weight] )=0 then 0 else (sum([SNF] )/sum([Milk Weight] ))*100 end as [SNF(%)] " + strRejectPer1 + " " + strRejectPer2 + " from(select [MCC Code],[MCC Name],[Route Code],[Route Name]
            ,[VSP Code],[VSP Name],[Milk Type]
            ,[Milk Weight],[FAT], [SNF]" + strRejectQty + "
            from (select pp.[MCC Code]  as [MCC Code],max(pp.[MCC Name] )  as [MCC Name]
            ,pp.[Route Code],max(pp.[Route Name]) as [Route Name]
            ,pp.[VSP Code],max(pp.[VSP Name]) as [VSP Name]
            ,pp.[Milk Type]
            ,sum([Milk Weight(KG)] ) as [Milk Weight]
            ,sum([FAT(KG)] ) as [FAT] ,sum([SNF(KG)] ) as [SNF]
            ,RejectType
            from (" + Environment.NewLine + qry + Environment.NewLine + " ) as  pp group by pp.[MCC Code],pp.[Route Code],pp.[VSP Code],pp.[Milk Type],pp.RejectType 
              ) as aa )a where 1=1 group by [MCC Code],[MCC Name],[Route Code],[Route Name]
            ,[VSP Code],[VSP Name],[Milk Type] order by [MCC Name],[Route Name],[VSP Code]"
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
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            Else
                Gv1.DataSource = dt1
                RadPageView1.SelectedPage = RadPageViewPage2
                EnableDisableCtrl(False)
                SetGridFormat()
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub EnableDisableCtrl(ByVal val As Boolean)
        txtFiscalYear.Enabled = val
        txtPaymentCycleFrom.Enabled = val
        txtPaymentCycleTo.Enabled = val
        txtMCC.Enabled = val
    End Sub
    Sub SetGridFormat()
        Gv1.GroupDescriptors.Add(New GridGroupByExpression("[MCC Name] as [MCC Name] format ""{0}: {1}"" Group By [MCC Name]"))
        Gv1.GroupDescriptors.Add(New GridGroupByExpression("[Route Name] as [Route Name] format ""{0}: {1}"" Group By [Route Name]"))
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

        Gv1.Columns("MCC Code").IsVisible = False
        Gv1.Columns("Route Code").IsVisible = False

        If Gv1.Rows.Count > 0 Then
            Dim summaryRowItemB As New GridViewSummaryRowItem()
            Dim summaryRowItemC As New GridViewSummaryRowItem()


            Dim MilkTypeB As New GridViewSummaryItem("Milk Type", "B", GridAggregateFunction.Max)
            summaryRowItemB.Add(MilkTypeB)
            Dim MilkTypeC As New GridViewSummaryItem("Milk Type", "C", GridAggregateFunction.Max)
            summaryRowItemC.Add(MilkTypeC)

            For i As Integer = 8 To 8 + 2 + dtREJECT.Rows.Count
                Dim aa = Gv1.Columns(i).HeaderText()

                Dim summaryItemB As New GridViewSummaryItem()
                summaryItemB.FormatString = "{0:n2}"
                summaryItemB.Name = aa
                summaryItemB.AggregateExpression = "sum(IIF([Milk Type]='B',[" + aa + "],0))"
                summaryRowItemB.Add(summaryItemB)

                Dim summaryItemC As New GridViewSummaryItem()
                summaryItemC.FormatString = "{0:n2}"
                summaryItemC.Name = aa
                summaryItemC.AggregateExpression = "sum(IIF([Milk Type]='C',[" + aa + "],0))"
                summaryRowItemC.Add(summaryItemC)

            Next

            For i As Integer = 9 To 9 + 2 + dtREJECT.Rows.Count - 1
                Dim aa = Gv1.Columns(i).HeaderText()

                Dim summaryItemB As New GridViewSummaryItem()
                summaryItemB.FormatString = "{0:n2}"
                summaryItemB.Name = aa + "(%)"
                summaryItemB.AggregateExpression = "sum(IIF([Milk Type]='B',[" + aa + "],0))*100/sum(IIF([Milk Type]='B',[Milk Weight],0))"
                summaryRowItemB.Add(summaryItemB)

                Dim summaryItemC As New GridViewSummaryItem()
                summaryItemC.FormatString = "{0:n2}"
                summaryItemC.Name = aa + "(%)"
                summaryItemC.AggregateExpression = "sum(IIF([Milk Type]='C',[" + aa + "],0))*100/sum(IIF([Milk Type]='C',[Milk Weight],0))"
                summaryRowItemC.Add(summaryItemC)

            Next


            Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItemB)
            Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItemC)
        End If

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
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim strHeading As String = clsCommon.myCstr("Milk Bill Procurement Summary For Cycle : " + txtPaymentCycleFrom.Value + " To " + txtPaymentCycleTo.Value + ", " + txtFiscalYear.Value + "")
            Dim arrHeader As List(Of String) = New List(Of String)()
            'arrHeader.Add("Name : " & "Milk Bill Procurement Summary For Cycle : " + txtPaymentCycleFrom.Value + " To " + txtPaymentCycleTo.Value + ", " + txtFiscalYear.Value)
            'arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            'arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            If txtMCC.arrDispalyMember IsNot Nothing AndAlso txtMCC.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Mcc : " + clsCommon.GetMulcallStringWithComma(txtMCC.arrDispalyMember))
            End If
            'arrHeader.Add("MCC : " + txtmccode.Value)
            'arrHeader.Add("Fiscal Year : " + txtFiscalYear.Value)
            'arrHeader.Add("Payment Cycle : " + txtPaymentCycleFrom.Value)
            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid(strHeading, Gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF(strHeading, Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        ExportGrid(EnumExportTo.PDF)
    End Sub


    Private Sub TxtMCC__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
        Dim qry As String = "select MCC_Code as [MCC Code],MCC_NAME as [MCC Name],TSPL_MCC_MASTER.plant_code as [Plant Code],tspl_location_master.location_desc as [Plant Name] from TSPL_MCC_MASTER left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.plant_code"
        txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("MBPSMCC", qry, "MCC Code", "MCC Name", txtMCC.arrValueMember, txtMCC.arrDispalyMember)
    End Sub

    'Private Sub Txtmccode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)
    '    Dim whrcls As String = ""

    '    txtmccode.Value = clsMccMaster.getFinder(whrcls, txtmccode.Value, isButtonClicked)
    '    If clsCommon.myLen(txtmccode.Value) > 0 Then
    '        lblmccname.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select mcc_name from tspl_mcc_master where mcc_code='" + txtmccode.Value + "'"))
    '    Else
    '        txtmccode.Value = ""
    '        lblmccname.Text = ""
    '    End If
    'End Sub

    Private Sub TxtFiscalYear__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtFiscalYear._MYValidating
        Try
            Dim qry As String = "select Fiscal_Code,Fiscal_Name,Start_Date,End_Date from TSPL_FISCAL_YEAR_MASTER"
            txtFiscalYear.Value = clsCommon.ShowSelectForm("LRFY", qry, "Fiscal_Code", "", txtFiscalYear.Value, "", isButtonClicked)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub TxtPaymentCycleFrom__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtPaymentCycleFrom._MYValidating
        Try
            If clsCommon.myLen(txtFiscalYear.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Plz Select Fiscal Year First.", Me.Text)
                txtFiscalYear.Focus()
                Exit Sub
            End If
            Dim whrcls As String = " Fiscal_Code='" + txtFiscalYear.Value + "' "
            Dim qry As String = "SELECT distinct convert(int,name) as Code,convert(varchar,From_Date,103) as [From Date],convert(varchar,To_Date,103) as [To Date] FROM TSPL_PAYMENT_CYCLE_GENERATED"
            txtPaymentCycleFrom.Value = clsCommon.ShowSelectForm("LRPCF", qry, "Code", whrcls, txtPaymentCycleFrom.Value, "Code", isButtonClicked)

            If clsCommon.myLen(txtPaymentCycleFrom.Value) > 0 Then
                txtPaymentCycleTo.Value = txtPaymentCycleFrom.Value
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
        End Try

    End Sub

    Private Sub Patment_Cycle_changed()
        Try
            Dim dt As DataTable
            Dim qry As String = "SELECT Name ,From_Date,To_Date FROM TSPL_PAYMENT_CYCLE_GENERATED where Fiscal_Code='" + txtFiscalYear.Value + "' and Name='" + txtPaymentCycleFrom.Value + "' "
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Payment Cycle found for Selected Fiscal Year", Me.Text)
                Exit Sub
            End If

            fromDate.Value = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select From_Date from TSPL_PAYMENT_CYCLE_GENERATED where Fiscal_Code='" + txtFiscalYear.Value + "' and Name='" + txtPaymentCycleFrom.Value + "' "))
            ToDate.Value = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select To_Date from TSPL_PAYMENT_CYCLE_GENERATED where Fiscal_Code='" + txtFiscalYear.Value + "' and Name='" + txtPaymentCycleTo.Value + "' "))
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub TxtPaymentCycleTo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtPaymentCycleTo._MYValidating
        Try
            If clsCommon.myLen(txtFiscalYear.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Plz Select Fiscal Year First.", Me.Text)
                txtFiscalYear.Focus()
                Exit Sub
            End If
            Dim whrcls As String = " Fiscal_Code='" + txtFiscalYear.Value + "' "
            Dim qry As String = "SELECT distinct convert(int,name) as Code,convert(varchar,From_Date,103) as [From Date],convert(varchar,To_Date,103) as [To Date] FROM TSPL_PAYMENT_CYCLE_GENERATED"
            txtPaymentCycleTo.Value = clsCommon.ShowSelectForm("LRPCT", qry, "Code", whrcls, txtPaymentCycleTo.Value, "Code", isButtonClicked)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub RmiExcelGrid_Click(sender As Object, e As EventArgs) Handles rmiExcelGrid.Click
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim strHeading As String = clsCommon.myCstr("Milk Bill Procurement Summary For Cycle : " + txtPaymentCycleFrom.Value + " To " + txtPaymentCycleTo.Value + ", " + txtFiscalYear.Value + "")
            Dim arrHeader As List(Of String) = New List(Of String)()

            If txtMCC.arrDispalyMember IsNot Nothing AndAlso txtMCC.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Mcc : " + clsCommon.GetMulcallStringWithComma(txtMCC.arrDispalyMember))
            End If

            clsCommon.MyExportToExcelGrid(strHeading, Gv1, arrHeader, Me.Text, True)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
        End Try
    End Sub
End Class
