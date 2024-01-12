Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI

' Ticket No : BHA/13/11/18-000678 by prabhakar - Create new report 
' Ticket No : BHA/14/11/18-000680 By Prabhakar - jobwork billing Print

Public Class rptJobworkBilling
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim strQry As String = ""
    Dim strCustomerCode As String = Nothing
    Dim strLocationCode As String = Nothing
    Dim strItemCode As String = Nothing
    Dim isBackOn As Boolean = False

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub


    Private Sub RptInventoryMovement_Load(sender As Object, e As EventArgs) Handles Me.Load
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
        Reset()
    End Sub
    Sub Reset()
        LoadReportBy()
        'ToDate.Value = clsCommon.GETSERVERDATE()
        'fromDate.Value = ToDate.Value.AddMonths(-1)
        cboBy.Visible = True
        rdbSummary.Checked = True
        txtCustomer.arrValueMember = Nothing
        txtLocation.arrValueMember = Nothing
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        strCustomerCode = ""
        strItemCode = ""
        strLocationCode = ""
        isBackOn = False
        btnGo.Enabled = True
        RadSplitButton1.Enabled = True
        btnBack.Enabled = False
        
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            PageSetupReport_ID = GetReportId()
            TemplateGridview = Gv1
            Dim BaseQry As String = ""
            Dim qry As String = ""
            Dim dt As New DataTable
            BaseQry = " select TSPL_JOBWORK_BILLING_DETAIL.Document_Code as [Document Code], convert (varchar, TSPL_JOBWORK_BILLING_HEAD.Document_Date,103) as [Document Date] ,TSPL_JOBWORK_BILLING_HEAD.cust_Code as [Customer Code] , " & _
                  " tspl_Customer_Master.Customer_Name as [Customer Name], TSPL_JOBWORK_BILLING_HEAD.Loc_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Desc], TSPL_JOBWORK_BILLING_HEAD.Status ,  " & _
                  " TSPL_JOBWORK_BILLING_DETAIL.Item_Code as [Item Code], TSPL_Item_Master.Item_Desc as [Item Desc], TSPL_JOBWORK_BILLING_DETAIL.Invoice_Qty as [Invoice Qty], TSPL_JOBWORK_BILLING_DETAIL.ConvKG_Qty as [Invoice Qty(KG)], " & _
                  " TSPL_JOBWORK_BILLING_DETAIL.Price as [Rate], TSPL_JOBWORK_BILLING_DETAIL.ItemAmt as [Amount], " & _
                  " TSPL_JOBWORK_BILLING_DETAIL.TAX1 as [Tax1 Code], TSPL_JOBWORK_BILLING_DETAIL.Tax1_Rate as [Tax1 Rate], TSPL_JOBWORK_BILLING_DETAIL.Tax1_Amt as [Tax1 Amount], " & _
                  " TSPL_JOBWORK_BILLING_DETAIL.TAX2 as [Tax2 Code], TSPL_JOBWORK_BILLING_DETAIL.Tax2_Rate as [Tax2 Rate], TSPL_JOBWORK_BILLING_DETAIL.Tax2_Amt as [Tax2 Amount], " & _
                  " TSPL_JOBWORK_BILLING_DETAIL.TAX3 as [Tax3 Code], TSPL_JOBWORK_BILLING_DETAIL.Tax3_Rate as [Tax3 Rate], TSPL_JOBWORK_BILLING_DETAIL.Tax3_Amt as [Tax3 Amount], " & _
                  " TSPL_JOBWORK_BILLING_DETAIL.TotalTaxAmt as [Total Tax Amount], TSPL_JOBWORK_BILLING_DETAIL.TotalAmt as [Total Amount] " & _
                  " from TSPL_JOBWORK_BILLING_DETAIL " & _
                  " left outer join TSPL_JOBWORK_BILLING_HEAD on TSPL_JOBWORK_BILLING_HEAD.Document_Code= TSPL_JOBWORK_BILLING_DETAIL.Document_code " & _
                  " left outer join tspl_Customer_Master on tspl_Customer_Master.cust_Code = TSPL_JOBWORK_BILLING_HEAD.Cust_Code " & _
                  " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_JOBWORK_BILLING_HEAD.Loc_Code " & _
                  " left outer join TSPL_Item_Master on TSPL_Item_Master.Item_Code = TSPL_JOBWORK_BILLING_DETAIL.Item_Code  " & _
                  " where convert(date,TSPL_JOBWORK_BILLING_HEAD.Document_Date ,103)>=convert(date,'" + fromDate.Value + "',103) AND convert(date,TSPL_JOBWORK_BILLING_HEAD.Document_Date,103)<=convert(date,'" + ToDate.Value + "',103)  "
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 And isBackOn = False Then
                BaseQry += " and TSPL_JOBWORK_BILLING_HEAD.cust_Code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")"
            End If
            If clsCommon.myLen(strCustomerCode) > 0 AndAlso clsCommon.myLen(strItemCode) AndAlso isBackOn = True Then
                BaseQry += " and TSPL_JOBWORK_BILLING_HEAD.cust_Code in ('" + strCustomerCode + "') and TSPL_JOBWORK_BILLING_DETAIL.Item_Code in ('" + strItemCode + "')"
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 And isBackOn = False Then
                BaseQry += " and TSPL_JOBWORK_BILLING_HEAD.Loc_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")  "
            End If
            If clsCommon.myLen(strLocationCode) > 0 AndAlso clsCommon.myLen(strItemCode) AndAlso isBackOn = True Then
                BaseQry += " and TSPL_JOBWORK_BILLING_HEAD.Loc_Code in ('" + strLocationCode + "') and TSPL_JOBWORK_BILLING_DETAIL.Item_Code in ('" + strItemCode + "')"
            End If
            If rdbDetails.Checked = True Then
                qry = BaseQry
            ElseIf rdbSummary.Checked Then
                If clsCommon.CompairString(cboBy.SelectedValue, "All") = CompairStringResult.Equal Then
                    qry = " select  XXX.[Document Code],max(XXX.[Document Date]) as [Document Date], max( XXX.[Customer Code] ) as [Customer Code] , max(XXX.[Customer Name] )as [Customer Name],max(XXX.[Location Code]) as [Location Code],max([Location Desc]) as [Location Desc],  XXX.[Item code], max (XXX.[Item Desc]) as [Item Desc] , sum( XXX.[Invoice Qty(KG)]) as  [Invoice Qty(KG)], sum(XXX.[Amount]) as [Amount] , sum(XXX.[Total Tax Amount]) as [Total Tax Amount] ,sum (XXX.[Total Amount]) as [Total Amount] from ( " & _
                         "" + BaseQry + " " & _
                         " ) XXX  group by XXX.[Document Code] , XXX.[Item code] order by XXX.[Document Code] "
                ElseIf clsCommon.CompairString(cboBy.SelectedValue, "Customerwise") = CompairStringResult.Equal Then
                    qry = " select XXX.[Customer Code] , max(XXX.[Customer Name] )as [Customer Name], XXX.[Item code], max (XXX.[Item Desc]) as [Item Desc] , sum( XXX.[Invoice Qty(KG)]) as  [Invoice Qty(KG)], sum(XXX.[Amount]) as [Amount] , sum(XXX.[Total Tax Amount]) as [Total Tax Amount] ,sum (XXX.[Total Amount]) as [Total Amount] from ( " & _
                          "" + BaseQry + " " & _
                          " ) XXX  group by XXX.[Customer Code] , XXX.[Item code] order by XXX.[Customer Code] "
                ElseIf clsCommon.CompairString(cboBy.SelectedValue, "Locationwise") = CompairStringResult.Equal Then
                    qry = " select XXX.[Location Code] , max(XXX.[Location Desc] )as [Location Desc], XXX.[Item code], max (XXX.[Item Desc]) as [Item Desc] , sum( XXX.[Invoice Qty(KG)]) as  [Invoice Qty(KG)], sum(XXX.[Amount]) as [Amount] , sum(XXX.[Total Tax Amount]) as [Total Tax Amount] ,sum (XXX.[Total Amount]) as [Total Amount] from ( " & _
                         "" + BaseQry + " " & _
                         " ) XXX  group by XXX.[Location Code] , XXX.[Item code] order by XXX.[Location Code] "
                ElseIf clsCommon.CompairString(cboBy.SelectedValue, "Datewise") = CompairStringResult.Equal Then
                    qry = " select XXX.[Document Date] , max(XXX.[Customer Name] )as [Customer Name], XXX.[Item code], max (XXX.[Item Desc]) as [Item Desc] , sum( XXX.[Invoice Qty(KG)]) as  [Invoice Qty(KG)], sum(XXX.[Amount]) as [Amount] , sum(XXX.[Total Tax Amount]) as [Total Tax Amount] ,sum (XXX.[Total Amount]) as [Total Amount] from ( " & _
                             "" + BaseQry + " " & _
                             " ) XXX  group by XXX.[Document Date] , XXX.[Item code] order by convert (date, XXX.[Document Date],103) asc "
                End If

            End If


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
                Gv1.BestFitColumns()
                Gv1.EnableFiltering = True
                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim amount As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(amount)
                Dim totalTaxAmount As New GridViewSummaryItem("Total Tax Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(totalTaxAmount)
                Dim totalAmount As New GridViewSummaryItem("Total Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(totalAmount)
                If rdbDetails.Checked = True Then
                    Dim Tax1Rate As New GridViewSummaryItem("Tax1 Rate", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(Tax1Rate)
                    Dim Tax2Rate As New GridViewSummaryItem("Tax2 Rate", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(Tax2Rate)
                    Dim Tax3Rate As New GridViewSummaryItem("Tax3 Rate", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(Tax3Rate)
                End If

                Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
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

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER where Location_Type='Physical'"
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransMulSe@JWBilling", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub
    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        Dim ReportID As String = GetReportId()
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
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If


            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        Dim ReportID As String = GetReportId()
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub

    Sub LoadReportBy()
        Dim dt As New DataTable()
        Dim Qry = " Select 'All' as 'By' union all Select 'Customerwise' as 'By' Union All Select 'Locationwise' as 'By' Union All Select 'Datewise' as 'By' "
        dt = clsDBFuncationality.GetDataTable(Qry)
        cboBy.DataSource = dt
        cboBy.ValueMember = "By"
        cboBy.DisplayMember = "By"
    End Sub

    Private Sub rdbDetails_CheckedChanged(sender As Object, e As EventArgs) Handles rdbDetails.CheckedChanged
        If rdbDetails.Checked = True Then
            cboBy.Visible = False
        Else
            cboBy.Visible = True
        End If
    End Sub

    Private Sub rdbSummary_CheckedChanged(sender As Object, e As EventArgs) Handles rdbSummary.CheckedChanged
        If rdbSummary.Checked = True Then
            cboBy.Visible = True
        Else
            cboBy.Visible = False
        End If
    End Sub

    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        Dim qry As String = " select TSPL_CUSTOMER_MASTER.Cust_Code  as Code, TSPL_CUSTOMER_MASTER.Customer_Name as Name from TSPL_CUSTOMER_MASTER "
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("TransMulSe@customer", qry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
    End Sub

    Private Sub Gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        If e.Column Is Gv1.Columns("Document Code") Then
            Dim strCode As String = Gv1.CurrentRow.Cells("Document Code").Value
            If clsCommon.myLen(strCode) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Item code Found.", Me.Text)
            Else
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmJobWorkBillig, strCode)
            End If
        ElseIf rdbSummary.Checked = True AndAlso e.Column Is Gv1.Columns("Customer Code") Then
            strCustomerCode = Gv1.CurrentRow.Cells("Customer Code").Value
            strItemCode = Gv1.CurrentRow.Cells("Item Code").Value
            isBackOn = True
            rdbDetails.Checked = True
            rdbSummary.Checked = False
            btnGo.PerformClick()
            btnGo.Enabled = False
            RadSplitButton1.Enabled = False
            btnBack.Enabled = True
        ElseIf rdbSummary.Checked = True AndAlso e.Column Is Gv1.Columns("Location Code") Then
            strLocationCode = Gv1.CurrentRow.Cells("Location Code").Value
            strItemCode = Gv1.CurrentRow.Cells("Item Code").Value
            isBackOn = True
            rdbDetails.Checked = True
            rdbSummary.Checked = False
            btnGo.PerformClick()
            btnGo.Enabled = False
            RadSplitButton1.Enabled = False
            btnBack.Enabled = True
        End If
        PageSetupReport_ID = GetReportId()
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        rdbSummary.Checked = True
        isBackOn = False
        btnGo.Enabled = True
        RadSplitButton1.Enabled = True
        btnBack.Enabled = False
        strCustomerCode = ""
        strItemCode = ""
        strLocationCode = ""
        btnGo.PerformClick()
        PageSetupReport_ID = GetReportId()
    End Sub

    Private Function GetReportId()
        Dim ReportId As String = ""
        Try
            If rdbDetails.Checked = True Then
                ReportId = MyBase.Form_ID
            ElseIf rdbSummary.Checked = True Then
                ReportId = clsERPFuncationality.GetReportID(MyBase.Form_ID, cboBy.Text)
            End If

        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
        Return ReportId
    End Function

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            clsCommon.MyExportToExcelGrid("Jobwork Billing Report", Gv1, arrHeader, Me.Text)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                arrHeader.Add("Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
            End If

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If

            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            clsCommon.MyExportToPDF("Jobwork Billing Report", Gv1, arrHeader, "Jobwork Billing Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
