Imports System.IO
Imports common


Public Class rptPaymentProcessReportBMCSocietyWise
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim strQry As String = ""
    Dim strDocumentCodeDetails As String = Nothing
    Dim strVSPCodeDetails As String = Nothing
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Function setFormId() As String
        Dim fromId As String = ""
        If rdbSummary.IsChecked = True Then
            fromId = MyBase.Form_ID + "_S"
        ElseIf rdbDetails.IsChecked = True Then
            fromId = MyBase.Form_ID + "_D"
            'Else
            '    fromId = MyBase.Form_ID + "_N"
        End If
        Return fromId
    End Function

    Private Sub RptInventoryMovement_Load(sender As Object, e As EventArgs) Handles Me.Load
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
        Reset()
    End Sub
    Sub Reset()
        ' ToDate.Value = clsCommon.GETSERVERDATE()
        ' fromDate.Value = ToDate.Value.AddMonths(-1)
        strDocumentCodeDetails = ""
        strVSPCodeDetails = ""
        'btnBack.Visible = False
        txtVSP.arrValueMember = Nothing
        txtLocation.arrValueMember = Nothing
        txtDocumentNo.arrValueMember = Nothing

        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        GetReportID()
        LoadData()
    End Sub

    Sub GetReportID()
        Dim VarID As String = ""
        If rdbSummary.IsChecked Then
            VarID += "_S"
        ElseIf rdbDetails.ISChecked Then
            VarID += "_D"
        End If
        Gv1.VarID = VarID
    End Sub

    Public Sub LoadData(Optional ByVal strDocumentNoForDetails As String = "", Optional ByVal strVSPCodeForDetails As String = "")
        Try
            PageSetupReport_ID = setFormId()
            TemplateGridview = Gv1
            Dim qry As String = ""
            Dim whr As String = " where 2=2 and TSPL_PAYMENT_PROCESS_HEAD.FarmType='PP' "
            Dim dt As New DataTable

            If rbFromDate.IsChecked = True Then
                whr += " and convert (date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) > = convert (date, '" + fromDate.Value + "',103)  and  convert (date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) <= convert (date, '" + ToDate.Value + "',103) "
            ElseIf rbToDate.IsChecked = True Then
                whr += " and convert (date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) > = convert (date, '" + fromDate.Value + "',103)  and  convert (date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <= convert (date, '" + ToDate.Value + "',103) "
            ElseIf rbBothFromToDate.IsChecked = True Then
                whr += " and convert (date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) > = convert (date, '" + fromDate.Value + "',103)   and convert (date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <= convert (date, '" + ToDate.Value + "',103) "
            End If



            'VSP filter
            If txtDocumentNo.arrValueMember IsNot Nothing AndAlso txtDocumentNo.arrValueMember.Count > 0 Then
                whr += " and TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader in (" + clsCommon.GetMulcallString(txtDocumentNo.arrValueMember) + ")"
            End If

            ' location filter
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                whr += " and TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
            End If

            ' route filter 
            If txtVSP.arrValueMember IsNot Nothing AndAlso txtVSP.arrValueMember.Count > 0 Then
                whr += " and TBL_Route.Route_Code in (" + clsCommon.GetMulcallString(txtVSP.arrValueMember) + ")"
            End If



            If ChkPosted.IsChecked = True Then
                whr += " and TSPL_PAYMENT_PROCESS_HEAD.isPosted = 1 "
            ElseIf ChkUnPosted.IsChecked = True Then
                whr += " and TSPL_PAYMENT_PROCESS_HEAD.isPosted = 0 "
            End If

            qry = "  Select TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code as [BMC],TSPL_GL_SEGMENT_CODE.Description as [BMC Name],TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE as [VSP Code],TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [Society Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Society Name], isnull(TSPL_PAYMENT_PROCESS_DETAIL.Total_Invoice_Amount,0) as [Milk Amount] ,isnull(TSPL_PAYMENT_PROCESS_DETAIL.MCC_Sale_Amount,0) as [MCC Sale Amount]  , isnull(TBL_Sale.Reduce_Deduc_Amt,0) as [Adjusted Amount], isnull(TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount,0) as [Payable Amount]
                     from TSPL_PAYMENT_PROCESS_DETAIL
                     left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
                     left outer join TSPL_VLC_MASTER_HEAD on  TSPL_VLC_MASTER_HEAD.VSP_Code =  TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE
                     left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE 
                     left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code = TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code
                     left outer join ( select  VSP_Code , TSPL_VLC_MASTER_HEAD.Route_Code  from TSPL_VLC_MASTER_HEAD  where len(isnull (TSPL_VLC_MASTER_HEAD.Route_Code,''))>0 group by  VSP_Code, TSPL_VLC_MASTER_HEAD.Route_Code ) TBL_Route on TBL_Route.VSP_Code = TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE
                     left outer join ( select Doc_No, Customer_CODE ,sum(  isnull(Amount,0) - isnull(Reduce_Deduc_Amt,0)  ) as Reduce_Deduc_Amt  from TSPL_PAYMENT_PROCESS_MCC_SALE   group by Doc_No,Customer_CODE ) TBL_Sale on TBL_Sale.Doc_No = TSPL_PAYMENT_PROCESS_HEAD.Doc_No and TBL_Sale.Customer_CODE = TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE
                     " + whr + "  "
            'BMC Wise 
            If rdbSummary.IsChecked = True Then
                qry = " select [BMC],max([BMC Name]) as [BMC Name] ,sum([Milk Amount]) as [Milk Amount],sum([MCC Sale Amount]) as [MCC Sale Amount] ,sum([Adjusted Amount]) as [Adjusted Amount],sum([Payable Amount]) as [Payable Amount] from ( 
                        " + qry + " 
                        ) XXXFinal	group by XXXFinal.[BMC] order by BMC asc  "
            ElseIf rdbDetails.IsChecked = True Then
                qry = " select [BMC],max([BMC Name]) as [BMC Name] ,[Society Code],max([Society Name]) as [Society Name],sum([Milk Amount]) as [Milk Amount],sum([MCC Sale Amount]) as [MCC Sale Amount] ,sum([Adjusted Amount]) as [Adjusted Amount],sum([Payable Amount]) as [Payable Amount] from (
                      " + qry + "
                      ) XXXFinal	group by XXXFinal.[BMC],[Society Code] order by BMC, [Society Code] asc   "
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
                'If rdbDetails.IsChecked = True OrElse clsCommon.myLen(strDocumentNoForDetails) > 0 Then
                '    Gv1.Columns("DrPE").IsVisible = False
                '    Gv1.Columns("doc_No").HeaderText = "Payment Process" + Environment.NewLine + "Code"
                '    Gv1.Columns("doc_date").HeaderText = "Payment Process" + Environment.NewLine + "Date"
                'End If
                'Gv1.Columns("From Date").HeaderText = "Payment Cycle" + Environment.NewLine + "From Date"
                'Gv1.Columns("To Date").HeaderText = "Payment Cycle" + Environment.NewLine + "To Date"
                ' Gv1.Columns("Trans Type").IsVisible = False

                'Gv1.Columns("Document No").IsPinned = True
                'Gv1.Columns("Document No").PinPosition = PinnedColumnPosition.Right

                'Gv1.Columns("Dr").IsPinned = True
                'Gv1.Columns("Dr").PinPosition = PinnedColumnPosition.Right

                'Gv1.Columns("Cr").IsPinned = True
                'Gv1.Columns("Cr").PinPosition = PinnedColumnPosition.Right

                'If rdbSummary.IsChecked = True Then
                '    Gv1.Columns("Payable Amt").IsPinned = True
                '    Gv1.Columns("Payable Amt").PinPosition = PinnedColumnPosition.Right
                'End If

                RadPageView1.SelectedPage = RadPageViewPage2

                Gv1.EnableFiltering = True
                'Gv1.Columns("Qty in Stocking UOM").FormatString = "{0:n2}"

                '================================
                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim summaryDr As New GridViewSummaryItem()
                'If rdbDetails.IsChecked = True OrElse clsCommon.myLen(strDocumentNoForDetails) > 0 Then
                '    summaryDr.Name = "Dr"
                '    summaryDr.AggregateExpression = "sum(Dr) - sum(DrPE)"
                '    summaryRowItem.Add(summaryDr)
                'Else
                '    Dim itemMilkAmt As New GridViewSummaryItem("Milk Amount", "{0:F2}", GridAggregateFunction.Sum)
                '    summaryRowItem.Add(itemMilkAmt)
                'End If
                Dim itemMilkAmt As New GridViewSummaryItem("Milk Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(itemMilkAmt)

                Dim itemSaleAmt As New GridViewSummaryItem("MCC Sale Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(itemSaleAmt)

                Dim itemAdjAmt As New GridViewSummaryItem("Adjusted Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(itemAdjAmt)

                Dim itemPayableAmount As New GridViewSummaryItem("Payable Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(itemPayableAmount)

                'If rdbSummary.IsChecked = True Then
                '    Dim itemPayableAmt As New GridViewSummaryItem("Payable Amt", "{0:F2}", GridAggregateFunction.Sum)
                '    summaryRowItem.Add(itemPayableAmt)
                'End If
                '================================

                Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
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
            If clsCommon.myLen(setFormId()) > 0 Then
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
    Private Sub txtTransaction__My_Click(sender As Object, e As EventArgs) Handles txtDocumentNo._My_Click
        Dim qry As String
        qry = "  select VLC_Code as Code , VLC_Name as Name, VLC_Code_VLC_Uploader as VLCUploderCode from TSPL_VLC_MASTER_HEAD "
        txtDocumentNo.arrValueMember = clsCommon.ShowMultipleSelectForm("CatteleReport2222@VSP", qry, "VLCUploderCode", "Name", txtDocumentNo.arrValueMember, txtDocumentNo.arrDispalyMember)
    End Sub
    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtVSP._My_Click
        Dim qry As String
        qry = "   select   TSPL_VLC_MASTER_HEAD.Route_Code as Code, max(TSPL_MCC_ROUTE_MASTER.Route_Name) as Name from TSPL_VLC_MASTER_HEAD  inner join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_VLC_MASTER_HEAD.Route_Code where len(isnull (TSPL_VLC_MASTER_HEAD.Route_Code,''))>0 group by   TSPL_VLC_MASTER_HEAD.Route_Code  "
        txtVSP.arrValueMember = clsCommon.ShowMultipleSelectForm("PaymentProcessReport2222@VSP", qry, "Code", "Name", txtVSP.arrValueMember, txtVSP.arrDispalyMember)
    End Sub
    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " select Location_Code as [Code],Location_Desc as [Name],Loc_Segment_Code as [LocationSegmentCode] from TSPL_Location_MASTER where    Rejected_Type='N' and Location_Category='MCC' "
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("PaymentProcessReport222@LocFinder", qry, "LocationSegmentCode", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub
    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        Dim ReportID As String = setFormId()
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
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", Me.Text)
            End If


            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(setFormId(), objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub


    Private Sub Export(ByVal exporter As EnumExportTo)
        'Try
        '    If Gv1.Rows.Count <= 0 Then
        '        clsCommon.MyMessageBoxShow("No Data Found to Export", Me.Text)
        '        Exit Sub
        '    End If
        '    Dim arrHeader As List(Of String) = New List(Of String)()
        '    'arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptBatchItemReport1 & "'"))
        '    'arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
        '    'arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

        '    If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
        '        arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
        '    End If
        '    If txtVSP.arrDispalyMember IsNot Nothing AndAlso txtVSP.arrDispalyMember.Count > 0 Then
        '        arrHeader.Add("VSP : " + clsCommon.GetMulcallStringWithComma(txtVSP.arrDispalyMember))
        '    End If
        '    If txtDocumentNo.arrDispalyMember IsNot Nothing AndAlso txtDocumentNo.arrDispalyMember.Count > 0 Then
        '        arrHeader.Add("Document : " + clsCommon.GetMulcallStringWithComma(txtDocumentNo.arrDispalyMember))
        '    End If
        '    If exporter = EnumExportTo.Excel Then
        '        transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
        '        clsCommon.MyExportToExcelGrid("Payment Process Report", Gv1, arrHeader, Me.Text)
        '    Else
        '        transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
        '        clsCommon.MyExportToPDF("Payment Process Report", Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
        '    End If

        'Catch ex As Exception
        '    common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        'End Try

        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            'arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptBatchItemReport1 & "'"))
            'arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            'arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            If rdbSummary.IsChecked = True Then
                arrHeader.Add("Report Type : BMC Wise Report")
            Else
                arrHeader.Add("Report Type : Society Wise Report")
            End If

            If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                arrHeader.Add("BMC : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If
            If txtVSP.arrDispalyMember IsNot Nothing AndAlso txtVSP.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Route : " + clsCommon.GetMulcallStringWithComma(txtVSP.arrDispalyMember))
            End If
            If txtDocumentNo.arrDispalyMember IsNot Nothing AndAlso txtDocumentNo.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Document : " + clsCommon.GetMulcallStringWithComma(txtDocumentNo.arrDispalyMember))
            End If
            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("Cattle Feed Adjustment Report", Gv1, arrHeader, Me.Text)
            Else
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Cattle Feed Adjustment Report", Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
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

    Private Sub Gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        Try
            If rdbSummary.IsChecked = True AndAlso clsCommon.myLen(strDocumentCodeDetails) <= 0 Then
                strDocumentCodeDetails = clsCommon.myCstr(Gv1.Rows(e.RowIndex).Cells.Item("Document No").Value)
                strVSPCodeDetails = clsCommon.myCstr(Gv1.Rows(e.RowIndex).Cells.Item("VSP Code").Value)
                LoadData(strDocumentCodeDetails, strVSPCodeDetails)
                'btnBack.Visible = True
                btnGo.Enabled = False
                Enabledisablecontrol(False)
                'strDocumentCodeDetails = ""
                'strVSPCodeDetails = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs)
        'Dim strDocumentCodeDetails As String = ""
        'Dim strVSPCodeDetails As String = ""
        strDocumentCodeDetails = ""
        strVSPCodeDetails = ""
        LoadData()
        'btnBack.Visible = False
        btnGo.Enabled = True
        Enabledisablecontrol(True)
    End Sub

    Private Sub rbFromDate_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbFromDate.ToggleStateChanged
        Try
            ControlEmpty()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString(), Me.Text)
        End Try
    End Sub

    Sub ControlEmpty()

        strDocumentCodeDetails = ""
        strVSPCodeDetails = ""
        'btnBack.Visible = False
        'txtVSP.arrValueMember = Nothing
        'txtVSP.arrDispalyMember = Nothing
        'txtLocation.arrValueMember = Nothing
        'txtLocation.arrDispalyMember = Nothing
        txtDocumentNo.arrValueMember = Nothing
        txtDocumentNo.arrDispalyMember = Nothing
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub rbToDate_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbToDate.ToggleStateChanged
        Try
            ControlEmpty()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub rbBothFromToDate_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbBothFromToDate.ToggleStateChanged
        Try
            ControlEmpty()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub rbNone_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbNone.ToggleStateChanged
        Try
            ControlEmpty()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub fromDate_ValueChanged(sender As Object, e As EventArgs) Handles fromDate.ValueChanged
        Try
            ControlEmpty()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub ToDate_ValueChanged(sender As Object, e As EventArgs) Handles ToDate.ValueChanged
        Try
            ControlEmpty()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString(), Me.Text)
        End Try
    End Sub

    Public Sub Enabledisablecontrol(ByVal isEnable As Boolean)
        gbDateRangeApply.Enabled = isEnable
        RadGroupBox3.Enabled = isEnable
        RadGroupBox2.Enabled = isEnable
        RadGroupBox1.Enabled = isEnable
        txtLocation.Enabled = isEnable
        txtVSP.Enabled = isEnable
        txtDocumentNo.Enabled = isEnable
    End Sub

    Private Sub rdbSummary_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rdbSummary.ToggleStateChanged
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
    End Sub

    Private Sub rdbDetails_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rdbDetails.ToggleStateChanged
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
    End Sub

    Private Sub chkBoth_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkBoth.ToggleStateChanged
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
    End Sub

    Private Sub ChkPosted_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles ChkPosted.ToggleStateChanged
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
    End Sub

    Private Sub ChkUnPosted_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles ChkUnPosted.ToggleStateChanged
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
    End Sub
End Class
