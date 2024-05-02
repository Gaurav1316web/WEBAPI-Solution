Imports common
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports Telerik.WinControls

Public Class rptTankerDispatchGainLossReco
    Inherits FrmMainTranScreen
#Region "Varibales"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dt As DataTable

#End Region

    Private Sub rptTankerDispatchGainLossReco_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New ")
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub SetUserMgmtNew()
        MyBase.SetUserMgmt(clsUserMgtCode.rptTankerDispatchGainLossReco)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub

    Sub Print()
        Try
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()

            Dim strQry As String = "select Chalan_NO as [Challan No],max(Dispatch_Date) as [Dispatch Date]
            ,sum(Stock_Qty * case when RI=1 then 1 else 0 end) as [Dispatch Stock Qty]
            ,cast(sum(Fat_KG * case when RI=1 then 1 else 0 end) *100/sum(Stock_Qty * case when RI=1 then 1 else 0 end) as decimal(18,2)) as [Dispatch Fat]
            ,sum(Fat_KG * case when RI=1 then 1 else 0 end) as [Dispatch FAT KG]  
            ,cast(sum(Fat_Amt * case when RI=1 then 1 else 0 end)/sum(Fat_KG * case when RI=1 then 1 else 0 end) as decimal(18,2)) as [Dispatch Fat Rate]  
            ,sum(Fat_Amt * case when RI=1 then 1 else 0 end) as [Dispatch FAT Amt]  
            ,cast(sum(SNF_KG * case when RI=1 then 1 else 0 end) *100/sum(Stock_Qty * case when RI=1 then 1 else 0 end) as decimal(18,2)) as [Dispatch SNF]
            ,sum(SNF_KG * case when RI=1 then 1 else 0 end) as [Dispatch SNF KG] 
            ,cast(sum(SNF_Amt * case when RI=1 then 1 else 0 end)/sum(SNF_KG * case when RI=1 then 1 else 0 end) as decimal(18,2)) as [Dispatch SNF Rate]  
            ,sum(SNF_Amt * case when RI=1 then 1 else 0 end) as [Dispatch SNF Amt] 
            ,sum(Avg_Cost * case when RI=1 then 1 else 0 end) as [Dispatch Avg Cost]  
            ,max(Receipt_Challan_No) as [Milk Transfer In]
            ,(case when sum(Stock_Qty * case when RI=2 then 1 else 0 end)=0 then 0 else
			cast(sum(Fat_KG * case when RI=2 then 1 else 0 end) *100/sum(Stock_Qty * case when RI=2 then 1 else 0 end) as decimal(18,2)) end) as [Milk Transfer In FAT]
            ,sum(Fat_KG * case when RI=2 then 1 else 0 end) as [Milk Transfer In FAT KG]  
            ,(case when sum(Fat_KG * case when RI=2 then 1 else 0 end)=0 then 0 else
			cast(sum(Fat_Amt * case when RI=2 then 1 else 0 end)/sum(Fat_KG * case when RI=2 then 1 else 0 end) as decimal(18,2)) end) as [Milk Transfer In FAT Rate]  
            ,sum(Fat_Amt * case when RI=2 then 1 else 0 end) as [Milk Transfer In FAT Amt]  
            ,(case when sum(Stock_Qty * case when RI=2 then 1 else 0 end)=0 then 0 else 
			cast(sum(SNF_KG * case when RI=2 then 1 else 0 end) *100/sum(Stock_Qty * case when RI=2 then 1 else 0 end) as decimal(18,2)) end) as [Milk Transfer In SNF]
            ,sum(SNF_KG * case when RI=2 then 1 else 0 end) as [Milk Transfer In SNF KG] 
            ,(case when sum(SNF_KG * case when RI=2 then 1 else 0 end)=0 then 0 else
			cast(sum(SNF_Amt * case when RI=2 then 1 else 0 end)/sum(SNF_KG * case when RI=2 then 1 else 0 end)as decimal(18,2)) end) as [Milk Transfer In SNF Rate]  
            ,sum(SNF_Amt * case when RI=2 then 1 else 0 end) as [Milk Transfer In SNF Amt] 
            ,sum(Avg_Cost * case when RI=2 then 1 else 0 end) as [Milk Transfer In Avg Cost]  
            from (
            select TSPL_MCC_Dispatch_Challan.Chalan_NO,'' as Receipt_Challan_No, convert(varchar,TSPL_MCC_Dispatch_Challan.Dispatch_Date,103) as Dispatch_Date,1 as RI,TSPL_INVENTORY_MOVEMENT_New.Fat_KG,TSPL_INVENTORY_MOVEMENT_New.Fat_Rate,TSPL_INVENTORY_MOVEMENT_New.Fat_Amt,TSPL_INVENTORY_MOVEMENT_New.SNF_KG,TSPL_INVENTORY_MOVEMENT_New.SNF_Rate,TSPL_INVENTORY_MOVEMENT_New.SNF_Amt,TSPL_INVENTORY_MOVEMENT_New.Avg_Cost,TSPL_INVENTORY_MOVEMENT_New.Stock_Qty from TSPL_MCC_Dispatch_Challan 
            left outer join TSPL_INVENTORY_MOVEMENT_New on TSPL_INVENTORY_MOVEMENT_New.source_doc_no=TSPL_MCC_Dispatch_Challan.Chalan_NO and TSPL_INVENTORY_MOVEMENT_New.Trans_Type='DispChallan'
            where Convert(Date, TSPL_MCC_Dispatch_Challan.Dispatch_Date,103) >='" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "' and convert(date, TSPL_MCC_Dispatch_Challan.Dispatch_Date,103)<='" + clsCommon.GetPrintDate(ToDate.Value, "dd-MMM-yyyy") + "'"

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                strQry += " and tspl_mcc_dispatch_challan.Mcc_Or_Plant_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
            End If

            strQry += " union all
            select TSPL_MCC_Dispatch_Challan.Chalan_NO,TSPL_MILK_TRANSFER_IN.Receipt_Challan_No, convert(varchar,TSPL_MCC_Dispatch_Challan.Dispatch_Date,103) as Dispatch_Date,2 as RI,TSPL_INVENTORY_MOVEMENT_New.Fat_KG,TSPL_INVENTORY_MOVEMENT_New.Fat_Rate,TSPL_INVENTORY_MOVEMENT_New.Fat_Amt,TSPL_INVENTORY_MOVEMENT_New.SNF_KG,TSPL_INVENTORY_MOVEMENT_New.SNF_Rate,TSPL_INVENTORY_MOVEMENT_New.SNF_Amt,TSPL_INVENTORY_MOVEMENT_New.Avg_Cost,TSPL_INVENTORY_MOVEMENT_New.Stock_Qty from TSPL_MILK_TRANSFER_IN
            inner join TSPL_MCC_Dispatch_Challan on TSPL_MCC_Dispatch_Challan.Chalan_NO =TSPL_MILK_TRANSFER_IN.Dispatch_Challan_No
            left outer join TSPL_INVENTORY_MOVEMENT_New on TSPL_INVENTORY_MOVEMENT_New.source_doc_no=TSPL_MILK_TRANSFER_IN.Receipt_Challan_No and TSPL_INVENTORY_MOVEMENT_New.Trans_Type='MilkTransferIn'
            where Convert(Date, TSPL_MCC_Dispatch_Challan.Dispatch_Date,103) >='" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "' and convert(date, TSPL_MCC_Dispatch_Challan.Dispatch_Date,103)<='" + clsCommon.GetPrintDate(ToDate.Value, "dd-MMM-yyyy") + "'"

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                strQry += " and tspl_mcc_dispatch_challan.Mcc_Or_Plant_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
            End If

            strQry += " )xx group by Chalan_NO"

            Dim dt As DataTable = Nothing

            dt = clsDBFuncationality.GetDataTable(strQry)

            Gv1.DataSource = Nothing
            Gv1.Columns.Clear()
            Gv1.Rows.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.EnableFiltering = True
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            Else
                EnableDisableAllControl(False)
                Gv1.DataSource = dt
                SetGridFormationOFGV1()
            End If

            ReStoreGridLayout()
            Gv1.MasterTemplate.AllowAddNewRow = False
            RadPageView1.SelectedPage = RadPageViewPage2
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Sub EnableDisableAllControl(ByVal val As Boolean)
        fromDate.Enabled = val
        ToDate.Enabled = val
        txtLocation.Enabled = val
    End Sub

    Sub SetGridFormationOFGV1()
        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = True
            Gv1.Columns(ii).BestFit()
        Next

        RadPageView1.SelectedPage = RadPageViewPage2
        Gv1.AllowAddNewRow = False
        Gv1.ShowGroupPanel = True
        Gv1.BestFitColumns()
    End Sub

    Sub Reset()
        Gv1.DataSource = Nothing
        Gv1.Columns.Clear()
        Gv1.Rows.Clear()
        Gv1.GroupDescriptors.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        EnableDisableAllControl(True)
    End Sub

    Enum Exporter
        Excel = 0
        PDF = 1
        Print = 2
        Refresh = 3
    End Enum

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

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            Dim obj As New clsGridLayout()
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            obj = New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = Gv1.ColumnCount
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        End If
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = Gv1
        Print()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub rmExport_Click(sender As Object, e As EventArgs) Handles rmExport.Click
        If (Gv1.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow("No Data To Export")
            Exit Sub
        End If
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : Tanker Dispatch Gain-Loss Reco")
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")
            If Not IsNothing(txtLocation.arrValueMember) Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If
            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub rptTankerDispatchGainLossReco_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            Print()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub


    Private Sub PDF_Click(sender As Object, e As EventArgs) Handles PDF.Click
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : Tanker Dispatch Gain-Loss Reco")
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")
            If Not IsNothing(txtLocation.arrValueMember) Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If
            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            clsCommon.MyExportToPDF("Tanker Dispatch Gain Loss Reco", Gv1, arrHeader, "Tanker Dispatch Gain Loss Reco", PageSetupReport_ID, objCommonVar.CurrentUserCode)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " select Location_Code as Code,Location_Desc as [Name] from TSPL_LOCATION_MASTER  WHERE Type='PLANT' OR Location_Category='MCC'  "
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TDGLR", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub
End Class


