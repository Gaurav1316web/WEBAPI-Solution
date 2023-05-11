Imports common
Imports System.ComponentModel
Imports System.IO

'by Sanjay - Create new report 
Public Class rptMilkCostReport
    Inherits FrmMainTranScreen

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExp.Visible = MyBase.isExport
    End Sub
    Dim StrPermission As String
    Private Sub rptMilkCostReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        StrPermission = clsERPFuncationality.UserWiseAvailableLocationCode()
        txtFiscalYear.Value = objCommonVar.CurrFiscalYear
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        Reset()
    End Sub
    Sub Reset()
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        EnableDisableCtrl(True)
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = clsCommon.myCstr(MyBase.Form_ID)
        TemplateGridview = Gv1
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
                clsCommon.MyMessageBoxShow("Plz Select Fiscal Year First.", Me.Text)
                txtFiscalYear.Focus()
                Exit Sub
            End If
            If clsCommon.myLen(txtPaymentCycleFrom.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Plz Select Payment Cycle From First.", Me.Text)
                txtPaymentCycleFrom.Focus()
                Exit Sub
            End If
            If clsCommon.myLen(txtPaymentCycleTo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Plz Select Payment Cycle To First.", Me.Text)
                txtPaymentCycleTo.Focus()
                Exit Sub
            End If

            If clsCommon.myCdbl(txtPaymentCycleFrom.Value) > clsCommon.myCdbl(txtPaymentCycleTo.Value) Then
                common.clsCommon.MyMessageBoxShow("From Payment Cycle can not be greater then to Payment Cycle")
                txtPaymentCycleFrom.Focus()
                Exit Sub
            End If

            Patment_Cycle_changed()
            strRejection = ",'' as RejectType,'' as RejectReason,'' as Defaulter"
            Dim ShowVLCUploaderData As Boolean = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowVLCUploaderData, clsFixedParameterCode.ShowVLCUploaderData, Nothing)) = 1
            strSRNQuery = clsMilkRejectHead.GetMCCRegisterWithRejectionColumnQuery(fromDate.Value, ToDate.Value, "M", "E", "", StrPermission, txtMCC.arrValueMember, txtRoute.arrValueMember, txtVLC.arrValueMember, "", strRejection, ShowVLCUploaderData)

            strRejectionQuery = clsMilkRejectHead.GetMCCRegisterRejectionQuery(fromDate.Value, ToDate.Value, "M", "E", StrPermission, txtMCC.arrValueMember, txtRoute.arrValueMember, txtVLC.arrValueMember, "")

            qry = "Select final.MCC as [MCC Code] ,final.[MCC Name]
                ,final.[Doc Date] ,final.Shift , final.[Route Code],final.[Route Name] ,final.[VSP Code],final.[Vlc Uploader Code] as [Society Code]
                ,final.[VSP Name] as [Society Name],final.[Vlc Code] ,final.[VLC Name],final.[Milk Weight],final.[FAT(%)],final.[SNF(%)] ,final.[FAT(KG)],final.[SNF(KG)] 
                ,cast((case when RejectType='SOUR' and Defaulter='VSP' then ((TSPL_MILK_PRICE_MASTER.Milk_Rate*TSPL_MILK_PRICE_MASTER.Ratio/TSPL_MILK_PRICE_MASTER.Fat_Pers)*70)/100
                 when RejectType='CURD' and Defaulter='VSP' then ((TSPL_MILK_PRICE_MASTER.Milk_Rate*TSPL_MILK_PRICE_MASTER.Ratio/TSPL_MILK_PRICE_MASTER.Fat_Pers)*35)/100
                else (TSPL_MILK_PRICE_MASTER.Milk_Rate*TSPL_MILK_PRICE_MASTER.Ratio/TSPL_MILK_PRICE_MASTER.Fat_Pers) end) as decimal(18,2)) as [FAT Rate]
                ,cast((case when ((RejectType='SOUR' or RejectType='CURD') and Defaulter='VSP') then 0
                else (TSPL_MILK_PRICE_MASTER.Milk_Rate*TSPL_MILK_PRICE_MASTER.SNF_Ratio/TSPL_MILK_PRICE_MASTER.SNF_Pers) end) as decimal(18,2)) as [SNF Rate]
                ,RejectType,Defaulter,final.[SRN Amount] AS [Inventory Value] From ( " & strSRNQuery & " Union All " & strRejectionQuery & ") As final left join TSPL_MILK_PRICE_MASTER on TSPL_MILK_PRICE_MASTER.price_code=final.price_code where 2=2 "


            FinalQuery = "SELECT  ROW_NUMBER() OVER(Partition by 1 ORDER BY [MCC Code]) AS SNo
                ,XX.[MCC Code] ,XX.[MCC Name] , XX.[Route Code],XX.[Route Name] ,XX.[VSP Code],XX.[Society Code],XX.[Society Name],XX.[Vlc Code] ,XX.[VLC Name],XX.[Doc Date] ,XX.Shift
                ,XX.[Milk Weight],XX.[FAT(%)],XX.[SNF(%)] ,XX.[FAT(KG)],XX.[SNF(KG)] ,XX.[FAT Rate],XX.[SNF Rate],CAST(XX.[FAT(KG)]*XX.[FAT Rate] as decimal(18,2)) AS [FAT Value]
                ,CAST(XX.[SNF(KG)]*XX.[SNF Rate] as decimal(18,2)) AS [SNF Value],CAST(case when XX.[SNF(%)]<8 then (XX.[FAT(KG)]*XX.[FAT Rate]) + (XX.[SNF(KG)]*XX.[SNF Rate]) - (((XX.[FAT(KG)]*XX.[FAT Rate]) + (XX.[SNF(KG)]*XX.[SNF Rate]))*10/100) 
                  else (XX.[FAT(KG)]*XX.[FAT Rate]) + (XX.[SNF(KG)]*XX.[SNF Rate]) end as decimal(18,2)) AS [Total Value]
                ,XX.RejectType,XX.Defaulter,XX.[Inventory Value] FROM
                (" + Environment.NewLine + qry + Environment.NewLine + ")XX WHERE 1=1  order by [MCC Name],[Route Name],[VSP Code]"
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
                EnableDisableCtrl(False)
                SetGridFormat()
            End If
            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub EnableDisableCtrl(ByVal val As Boolean)
        txtFiscalYear.Enabled = val
        txtPaymentCycleFrom.Enabled = val
        txtPaymentCycleTo.Enabled = val
        txtMCC.Enabled = val
        txtRoute.Enabled = val
        txtVLC.Enabled = val
    End Sub
    Sub SetGridFormat()
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
        Gv1.Columns("VSP Code").IsVisible = False
        Gv1.Columns("VLC Code").IsVisible = False
        Gv1.Columns("VLC Name").IsVisible = False

        If Gv1.Rows.Count > 0 Then
            Dim summaryRowItem As New GridViewSummaryRowItem()

            Dim summaryItemFAT As New GridViewSummaryItem()
            summaryItemFAT.FormatString = "{0:n2}"
            summaryItemFAT.Name = "FAT(%)"
            summaryItemFAT.AggregateExpression = "sum([FAT(KG)])*100/sum([Milk Weight])"
            summaryRowItem.Add(summaryItemFAT)

            Dim summaryItemSNF As New GridViewSummaryItem()
            summaryItemSNF.FormatString = "{0:n2}"
            summaryItemSNF.Name = "SNF(%)"
            summaryItemSNF.AggregateExpression = "sum([SNF(KG)])*100/sum([Milk Weight])"
            summaryRowItem.Add(summaryItemSNF)

            Dim item1 As New GridViewSummaryItem("Milk Weight", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("FAT(KG)", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            Dim item3 As New GridViewSummaryItem("SNF(KG)", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("FAT Value", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)
            Dim item5 As New GridViewSummaryItem("SNF Value", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)
            Dim item6 As New GridViewSummaryItem("Total Value", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item6)
            Dim item7 As New GridViewSummaryItem("Inventory Value", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item7)

            Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

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
                clsCommon.MyMessageBoxShow("No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim strHeading As String = clsCommon.myCstr("Milk Cost Report For Cycle : " + txtPaymentCycleFrom.Value + " To " + txtPaymentCycleTo.Value + ", " + txtFiscalYear.Value + "")
            Dim arrHeader As List(Of String) = New List(Of String)()
            'arrHeader.Add("Name : " & "Milk Bill Procurement Summary For Cycle : " + txtPaymentCycleFrom.Value + " To " + txtPaymentCycleTo.Value + ", " + txtFiscalYear.Value)
            'arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            'arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                arrHeader.Add(("MCC : " + clsCommon.GetMulcallStringWithComma(txtMCC.arrDispalyMember) + " "))
            End If
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                arrHeader.Add(("Route : " + clsCommon.GetMulcallStringWithComma(txtRoute.arrDispalyMember) + " "))
            End If
            If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
                arrHeader.Add(("VLC : " + clsCommon.GetMulcallStringWithComma(txtVLC.arrDispalyMember) + " "))
            End If
            'arrHeader.Add("MCC : " + txtmccode.Value)
            'arrHeader.Add("Fiscal Year : " + txtFiscalYear.Value)
            'arrHeader.Add("Payment Cycle : " + txtPaymentCycleFrom.Value)
            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid(strHeading, Gv1, arrHeader, Me.Text)
            Else
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF(strHeading, Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
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
        txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("MCRMCC", qry, "MCC Code", "MCC Name", txtMCC.arrValueMember, txtMCC.arrDispalyMember)
        RefreshRoute()
        RefreshVLC()
    End Sub

    Private Sub txtRoute__My_Click(sender As Object, e As EventArgs) Handles txtRoute._My_Click
        Try
            'If txtMCC.arrValueMember Is Nothing OrElse txtMCC.arrValueMember.Count <= 0 Then
            '    txtMCC.Focus()
            '    Throw New Exception("Please select MCC")
            'End If
            Dim qry As String = "select Route_Code,Route_Name from TSPL_MCC_ROUTE_MASTER where 2=2 "
            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                qry += "  and MCC_Code in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")"
            End If

            txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("MCRRoute", qry, "Route_Code", "Route_Name", txtRoute.arrValueMember, txtRoute.arrDispalyMember)
            RefreshVLC()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtVLC__My_Click(sender As Object, e As EventArgs) Handles txtVLC._My_Click
        Try
            'If txtRoute.arrValueMember Is Nothing OrElse txtRoute.arrValueMember.Count <= 0 Then
            '    txtRoute.Focus()
            '    Throw New Exception("Please select at least route")
            'End If
            Dim qry As String = "select TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.Route_Code,TSPL_MCC_ROUTE_MASTER.Route_Name from TSPL_VLC_MASTER_HEAD left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_VLC_MASTER_HEAD.Route_Code where 2=2 and TSPL_VLC_MASTER_HEAD.Active='1' "
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                qry += " and TSPL_VLC_MASTER_HEAD.Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ") "
            End If

            txtVLC.arrValueMember = clsCommon.ShowMultipleSelectForm("MCRVLC", qry, "VLC_Code", "VLC_Name", txtVLC.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Sub RefreshRoute()
        If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
            Dim qry As String = "select Route_Code from TSPL_MCC_ROUTE_MASTER where Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")  and MCC_Code in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            txtRoute.arrValueMember = Nothing
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim arr As New ArrayList
                For Each dr As DataRow In dt.Rows
                    arr.Add(clsCommon.myCstr(dr("Route_Code")))
                Next
                txtRoute.arrValueMember = arr
            End If
        End If
    End Sub

    Sub RefreshVLC()
        If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
            Dim qry As String = "select VLC_Code from TSPL_VLC_MASTER_HEAD where  VLC_Code in (" + clsCommon.GetMulcallString(txtVLC.arrValueMember) + ")  and Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")  "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            txtVLC.arrValueMember = Nothing
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim arr As New ArrayList
                For Each dr As DataRow In dt.Rows
                    arr.Add(clsCommon.myCstr(dr("VLC_Code")))
                Next
                txtVLC.arrValueMember = arr
            End If
        End If
    End Sub


    Private Sub TxtFiscalYear__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtFiscalYear._MYValidating
        Try
            Dim qry As String = "select Fiscal_Code,Fiscal_Name,Start_Date,End_Date from TSPL_FISCAL_YEAR_MASTER"
            txtFiscalYear.Value = clsCommon.ShowSelectForm("LRFY", qry, "Fiscal_Code", "", txtFiscalYear.Value, "", isButtonClicked)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub TxtPaymentCycleFrom__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtPaymentCycleFrom._MYValidating
        Try
            If clsCommon.myLen(txtFiscalYear.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Plz Select Fiscal Year First.", Me.Text)
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
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK)
        End Try

    End Sub

    Private Sub Patment_Cycle_changed()
        Try
            Dim dt As DataTable
            Dim qry As String = "SELECT Name ,From_Date,To_Date FROM TSPL_PAYMENT_CYCLE_GENERATED where Fiscal_Code='" + txtFiscalYear.Value + "' and Name='" + txtPaymentCycleFrom.Value + "' "
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Payment Cycle found for Selected Fiscal Year")
                Exit Sub
            End If

            fromDate.Value = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select From_Date from TSPL_PAYMENT_CYCLE_GENERATED where Fiscal_Code='" + txtFiscalYear.Value + "' and Name='" + txtPaymentCycleFrom.Value + "' "))
            ToDate.Value = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select To_Date from TSPL_PAYMENT_CYCLE_GENERATED where Fiscal_Code='" + txtFiscalYear.Value + "' and Name='" + txtPaymentCycleTo.Value + "' "))
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub TxtPaymentCycleTo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtPaymentCycleTo._MYValidating
        Try
            If clsCommon.myLen(txtFiscalYear.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Plz Select Fiscal Year First.", Me.Text)
                txtFiscalYear.Focus()
                Exit Sub
            End If
            Dim whrcls As String = " Fiscal_Code='" + txtFiscalYear.Value + "' "
            Dim qry As String = "SELECT distinct convert(int,name) as Code,convert(varchar,From_Date,103) as [From Date],convert(varchar,To_Date,103) as [To Date] FROM TSPL_PAYMENT_CYCLE_GENERATED"
            txtPaymentCycleTo.Value = clsCommon.ShowSelectForm("LRPCT", qry, "Code", whrcls, txtPaymentCycleTo.Value, "Code", isButtonClicked)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub RmiExcelGrid_Click(sender As Object, e As EventArgs) Handles rmiExcelGrid.Click
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim strHeading As String = clsCommon.myCstr("Milk Cost Report For Cycle : " + txtPaymentCycleFrom.Value + " To " + txtPaymentCycleTo.Value + ", " + txtFiscalYear.Value + "")
            Dim arrHeader As List(Of String) = New List(Of String)()

            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                arrHeader.Add(("MCC : " + clsCommon.GetMulcallStringWithComma(txtMCC.arrDispalyMember) + " "))
            End If
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                arrHeader.Add(("Route : " + clsCommon.GetMulcallStringWithComma(txtRoute.arrDispalyMember) + " "))
            End If
            If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
                arrHeader.Add(("VLC : " + clsCommon.GetMulcallStringWithComma(txtVLC.arrDispalyMember) + " "))
            End If
            clsCommon.MyExportToExcelGrid(strHeading, Gv1, arrHeader, Me.Text, True)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
        End Try
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

    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        Try
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
                ''---------------
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        Try
            clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
            common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

End Class
