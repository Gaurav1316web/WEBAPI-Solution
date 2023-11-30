Imports common
Imports System.ComponentModel
Imports System.IO

' Date- 24-Sep-2020  by Sanjay - Create new report 
Public Class rptMCCPaymentSummary
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
        'GroupBox1.Enabled = False
        Reset()
    End Sub
    Sub Reset()
        'fndMCC.Value = ""
        'lblMCC.Text = ""
        'fndDocument.Value = ""
        fndLoc.Value = ""
        txtLocName.Text = ""
        dtpFromDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
        dtpToDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
        'EnableDisiablecontrol(True)
        btnGo.Enabled = True
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        GvDetail.DataSource = Nothing
        GvDetail.Rows.Clear()
        GvDetail.Columns.Clear()
        GvDetail.MasterTemplate.SummaryRowsBottom.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Print(False)
    End Sub
    Sub Print(ByVal isPrint As Boolean, Optional ByVal isPrerint As Boolean = False)
        Try
            If clsCommon.myLen(fndLoc.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select Location First", Me.Text)
                Return
            End If
            PageSetupReport_ID = MyBase.Form_ID
            TemplateGridview = Gv1
            Dim qry As String = ""
            Dim dt As New DataTable

            'qry = "select ROW_NUMBER () over (order by tspl_mcc_master.mcc_name ) As [S.No] " &
            '    ",tspl_mcc_master.mcc_name AS [Name of the Unit],COUNT(DISTINCT vsp_code) as [No of Collection Centers] " &
            '    ",sum(srn_net_amount) as [Amount],sum(payable_amount) as [Net Payable Amount] " &
            '    " From TSPL_PAYMENT_PROCESS_DETAIL " &
            '    " left join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No =TSPL_PAYMENT_PROCESS_DETAIL.Doc_No " &
            '    " left join tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_PAYMENT_PROCESS_HEAD.MCC_Code_Selected " &
            '    " Where LOC_SEG_CODE ='" & fndLoc.Value & "' " &
            '    " and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dtpToDate.Value + "'),103)   " &
            '    " Group By tspl_mcc_master.mcc_name Order By tspl_mcc_master.mcc_name "

            qry = "select ROW_NUMBER () over (order by MAX(TSPL_GL_SEGMENT_CODE.Description) ) As [S.No],TSPL_GL_SEGMENT_CODE.Segment_code as [Code of the Unit] ,MAX(TSPL_GL_SEGMENT_CODE.Description) AS [Name of the Unit] 
                    ,COUNT(DISTINCT vsp_code) as [No of Collection Centers] 
                    ,sum(srn_net_amount) as [Invoice Amount] 
                    ,sum(TSPL_PAYMENT_PROCESS_DETAIL.Credit_Note_Amount) as [Credit Note Amount]
                    ,sum(TSPL_PAYMENT_PROCESS_DETAIL.Deduction_Amount) as [Debit Note Amount]
                    ,sum(payable_amount) as [Net Payable Amount]
                     From TSPL_PAYMENT_PROCESS_DETAIL  left join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No =TSPL_PAYMENT_PROCESS_DETAIL.Doc_No  
                     left join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.SEGMENT_CODE=TSPL_PAYMENT_PROCESS_HEAD.LOC_SEG_CODE
                    Where 1=1 and TSPL_PAYMENT_PROCESS_HEAD.LOC_SEG_CODE ='" & fndLoc.Value & "'  
                    and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dtpToDate.Value + "'),103) Group By TSPL_GL_SEGMENT_CODE.Segment_code Order By [Name of the Unit] "

            dt = Nothing
            dt = clsDBFuncationality.GetDataTable(qry)
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            Else
                'If dt IsNot Nothing OrElse dt.Rows.Count > 0 Then
                Gv1.DataSource = dt
                For ii As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(ii).ReadOnly = True
                    Gv1.Columns(ii).BestFit()
                Next
                Gv1.Columns("Code of the Unit").IsVisible = False
                'Gv1.Columns("Qty_In_KG").IsVisible = False
                'Gv1.Columns("Fat_KG").IsVisible = False
                'Gv1.Columns("SNF_KG").IsVisible = False

                RadPageView1.SelectedPage = RadPageViewPage2

                Gv1.AutoSizeRows = True
                Gv1.EnableFiltering = True
                'EnableDisiablecontrol(False)
                btnGo.Enabled = False

                Dim summaryRowItem As New GridViewSummaryRowItem()

                Dim Qty1 As New GridViewSummaryItem("No of Collection Centers", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(Qty1)
                Dim Qty2 As New GridViewSummaryItem("Invoice Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(Qty2)
                Dim Qty3 As New GridViewSummaryItem("Net Payable Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(Qty3)
                Dim Qty4 As New GridViewSummaryItem("Credit Note Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(Qty4)
                Dim Qty5 As New GridViewSummaryItem("Debit Note Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(Qty5)

                Gv1.ShowGroupPanel = True
                Gv1.MasterTemplate.AutoExpandGroups = True
                Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                Gv1.BestFitColumns()
                'Else
                '    clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                '    Exit Sub
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



    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        Dim ReportID As String = PageSetupReport_ID
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
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub


    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Location : " & clsCommon.myCstr(txtLocName.Text))
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptMCCPaymentSummary & "'"))

            If clsCommon.CompairString(RadPageView1.SelectedPage.Name, RadPageViewPage2.Name) = CompairStringResult.Equal Then
                If exporter = EnumExportTo.Excel Then
                    transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                    clsCommon.MyExportToExcelGrid("MCC Payment Summary", Gv1, arrHeader, Me.Text)
                Else
                    transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("MCC Payment Summary", Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            ElseIf clsCommon.CompairString(RadPageView1.SelectedPage.Name, RadPageViewPage3.Name) = CompairStringResult.Equal Then
                If exporter = EnumExportTo.Excel Then
                    'transportSql.applyExportTemplate(GvDetail, PageSetupReport_ID)
                    clsCommon.MyExportToExcelGrid("MCC Payment Summary", GvDetail, arrHeader, Me.Text)
                Else
                    'transportSql.applyExportTemplate(GvDetail, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("MCC Payment Summary", GvDetail, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
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

    Private Sub FndLoc__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLoc._MYValidating
        Dim whrCls As String = " 1=1 "
        If Not clsMccMaster.isCurrentUserHO() Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrCls += " and  Location_Code in (" & objCommonVar.strCurrUserLocations & ")  "
            End If
        End If

        whrCls = whrCls & " and   Rejected_Type='N' and type='PLANT'"
        Dim dr As DataRow = clsLocation.getLocSegFinderFullRow(whrCls)
        If dr Is Nothing OrElse dr.ItemArray.Count <= 0 Then
            fndLoc.Value = ""
            'txtMCC.Text = ""
            'lblMCC.Text = ""
            txtLocName.Text = ""
            Exit Sub
        End If

        fndLoc.Value = clsCommon.myCstr(dr("LocationSegmentCode"))
        txtLocName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Description  from TSPL_GL_SEGMENT_CODE WHERE  Segment_code='" & fndLoc.Value & "' "))
        'If SettShowMCCFinder Then
        '    txtMCC.Text = clsCommon.myCstr(dr("Code"))
        '    lblMCC.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MCC_Name from tspl_mcc_master where mcc_Code='" + txtMCC.Text + "'"))
        'End If



        If clsCommon.myLen(fndLoc.Value) > 0 Then
            ' fndLoc.Enabled = False
            ' txtLocName.Enabled = False

            'If Not isLoad Then
            Dim PaymentCycleType As String = ""
            Dim PaymentCycleValue As Integer = 0
            ' If Not isLoad Then
            If clsCommon.myLen(fndLoc.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select the Location first", Me.Text)
                Exit Sub
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select isnull(TSPL_MCC_MASTER.empOnAmountOnly,0) as empOnAmountOnly,TSPL_MCC_MASTER.Payment_Cycle,TSPL_PAYMENT_CYCLE_MASTER.PC_TYPE,TSPL_PAYMENT_CYCLE_MASTER.PC_VALUE  from TSPL_MCC_MASTER left outer join TSPL_PAYMENT_CYCLE_MASTER on TSPL_PAYMENT_CYCLE_MASTER.PC_CODE=TSPL_MCC_MASTER.Payment_Cycle   where TSPL_MCC_MASTER.MCC_Code in (select Location_Code  from TSPL_LOCATION_MASTER where Loc_Segment_Code='" & fndLoc.Value & "' and Location_Category='MCC' and Rejected_Type='N') ")
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Payment Cycle found on current MCC/Location", Me.Text)
                Exit Sub
            End If
            PaymentCycleType = clsCommon.myCstr(dt.Rows(0)("PC_TYPE"))
            PaymentCycleValue = clsCommon.myCdbl(dt.Rows(0)("PC_VALUE"))
            'isEmpOnAmtOnly = IIf(clsCommon.myCdbl(dt.Rows(0)("empOnAmountOnly")) = 0, False, True)
            If clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.GetPrintDate(dtpFromDate.Value, "dd")) Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                    clsCommon.MyMessageBoxShow("Date can only be first day of month or at interval of " & PaymentCycleValue & " Day, Because MCC has payment Cycle of " & PaymentCycleValue & " Day ")
                    dtpFromDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
                    dtpToDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
                    Exit Sub
                End If
                dtpToDate.Value = DateAdd(DateInterval.Day, PaymentCycleValue - 1, dtpFromDate.Value)
                If DatePart(DateInterval.Month, dtpFromDate.Value) <> DatePart(DateInterval.Month, dtpToDate.Value) Then
                    dtpToDate.Value = DateAdd(DateInterval.Month, 1, clsCommon.myCDate("01/" & DatePart(DateInterval.Month, dtpFromDate.Value) & "/" & DatePart(DateInterval.Year, dtpFromDate.Value)))
                    dtpToDate.Value = DateAdd(DateInterval.Day, -1, dtpToDate.Value)
                End If
            ElseIf clsCommon.CompairString(PaymentCycleType, "Month") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.GetPrintDate(dtpFromDate.Value, "dd")) <> 1 Then
                    clsCommon.MyMessageBoxShow("Date can only be first day of month, Because MCC has payment Cycle of Month Type")
                    dtpFromDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
                    dtpToDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
                    Exit Sub
                End If
                dtpToDate.Value = DateAdd(DateInterval.Month, PaymentCycleValue, dtpFromDate.Value)
            ElseIf clsCommon.CompairString(PaymentCycleType, "Year") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.GetPrintDate(dtpFromDate.Value, "dd")) <> 1 Then
                    clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month, Because MCC has payment Cycle of Year Type", Me.Text)
                    dtpFromDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
                    dtpToDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
                    Exit Sub
                End If
                dtpToDate.Value = DateAdd(DateInterval.Year, PaymentCycleValue, dtpFromDate.Value)
            ElseIf clsCommon.CompairString(PaymentCycleType, "Week") = CompairStringResult.Equal Then
                Dim today As Date = dtpFromDate.Value
                Dim dayDiff As Integer = today.DayOfWeek - IIf(PaymentCycleValue = 1, DayOfWeek.Sunday, IIf(PaymentCycleValue = 2, DayOfWeek.Monday, IIf(PaymentCycleValue = 3, DayOfWeek.Tuesday, IIf(PaymentCycleValue = 4, DayOfWeek.Wednesday, IIf(PaymentCycleValue = 5, DayOfWeek.Thursday, IIf(PaymentCycleValue = 6, DayOfWeek.Friday, DayOfWeek.Saturday))))))
                dtpFromDate.Value = today.AddDays(-dayDiff)
                dtpToDate.Value = dtpFromDate.Value.AddDays(6)
            End If
            ' End If
            'End If
        End If
    End Sub

    Sub SetToDate()
        'If Not isLoad Then
        Dim PaymentCycleType As String = ""
        Dim PaymentCycleValue As Integer = 0
        ' If Not isLoad Then
        If clsCommon.myLen(fndLoc.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select the Location first", Me.Text)
            Exit Sub
        End If
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select TSPL_MCC_MASTER.Payment_Cycle,TSPL_PAYMENT_CYCLE_MASTER.PC_TYPE,TSPL_PAYMENT_CYCLE_MASTER.PC_VALUE  from TSPL_MCC_MASTER left outer join TSPL_PAYMENT_CYCLE_MASTER on TSPL_PAYMENT_CYCLE_MASTER.PC_CODE=TSPL_MCC_MASTER.Payment_Cycle   where TSPL_MCC_MASTER.MCC_Code  in (select Location_Code  from TSPL_LOCATION_MASTER where Loc_Segment_Code='" & fndLoc.Value & "' and Location_Category='MCC' and Rejected_Type='N') ")
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "No Payment Cycle found on current MCC/Location", Me.Text)
            Exit Sub
        End If
        PaymentCycleType = clsCommon.myCstr(dt.Rows(0)("PC_TYPE"))
        PaymentCycleValue = clsCommon.myCdbl(dt.Rows(0)("PC_VALUE"))
        Dim dtCurr As DateTime = clsCommon.GETSERVERDATE()
        If clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal Then
            If dtpFromDate.Value.Day Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                clsCommon.MyMessageBoxShow("Date can only be first day of month or at interval of " & PaymentCycleValue & " Day, Because MCC has payment Cycle of " & PaymentCycleValue & " Day ")
                dtpFromDate.Value = New Date(dtCurr.Year, dtCurr.Month, 1)
                dtpToDate.Value = dtpFromDate.Value
                Exit Sub
            End If
            dtpToDate.Value = dtpFromDate.Value.AddDays(PaymentCycleValue - 1)

            If dtpFromDate.Value.Month <> dtpToDate.Value.Month Then
                dtpToDate.Value = New Date(dtpFromDate.Value.Year, dtpFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
            End If
            Dim dtNxtPay As DateTime = dtpToDate.Value.AddDays(Math.Ceiling(PaymentCycleValue / 2.0))
            If dtpFromDate.Value.Month <> dtNxtPay.Month Then
                dtpToDate.Value = New Date(dtpFromDate.Value.Year, dtpFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
            End If
        ElseIf clsCommon.CompairString(PaymentCycleType, "Month") = CompairStringResult.Equal Then
            If clsCommon.myCdbl(clsCommon.GetPrintDate(dtpFromDate.Value, "dd")) <> 1 Then
                clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month, Because MCC has payment Cycle of Month Type", Me.Text)
                dtpFromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                dtpToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                Exit Sub
            End If
            dtpToDate.Value = DateAdd(DateInterval.Month, PaymentCycleValue, dtpFromDate.Value)
        ElseIf clsCommon.CompairString(PaymentCycleType, "Year") = CompairStringResult.Equal Then
            If clsCommon.myCdbl(clsCommon.GetPrintDate(dtpFromDate.Value, "dd")) <> 1 Then
                clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month, Because MCC has payment Cycle of Year Type", Me.Text)
                dtpFromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                dtpToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                Exit Sub
            End If
            dtpToDate.Value = DateAdd(DateInterval.Year, PaymentCycleValue, dtpFromDate.Value)
        ElseIf clsCommon.CompairString(PaymentCycleType, "Week") = CompairStringResult.Equal Then
            Dim today As Date = dtpFromDate.Value
            Dim dayDiff As Integer = today.DayOfWeek - IIf(PaymentCycleValue = 1, DayOfWeek.Sunday, IIf(PaymentCycleValue = 2, DayOfWeek.Monday, IIf(PaymentCycleValue = 3, DayOfWeek.Tuesday, IIf(PaymentCycleValue = 4, DayOfWeek.Wednesday, IIf(PaymentCycleValue = 5, DayOfWeek.Thursday, IIf(PaymentCycleValue = 6, DayOfWeek.Friday, DayOfWeek.Saturday))))))
            dtpFromDate.Value = today.AddDays(-dayDiff)
            dtpToDate.Value = dtpFromDate.Value.AddDays(6)
        End If
        ' End If
        'End If
    End Sub

    Private Sub dtpFromDate_Validating(sender As Object, e As CancelEventArgs) Handles dtpFromDate.Validating
        SetToDate()
    End Sub

    Private Sub dtpFromDate_Leave(sender As Object, e As EventArgs) Handles dtpFromDate.Leave
        SetToDate()
    End Sub

    Private Sub Gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        Try
            If clsCommon.myLen(e.Row.Cells.Item("Code of the Unit").Value) > 0 Then
                ShowDetail(clsCommon.myCstr(e.Row.Cells.Item("Code of the Unit").Value))
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ShowDetail(ByVal StrCode As String)
        Try
            If Gv1.CurrentRow.Index >= 0 Then
                Dim qry As String = ""
                Dim dt As New DataTable

                qry = "Select ROW_NUMBER() over (order by max(tspl_mcc_master.mcc_name) ) As [S.No] ,
                    tspl_mcc_master.mcc_code as [Code of the Unit], max(tspl_mcc_master.mcc_name) AS [Name of the Unit]
                    ,COUNT(DISTINCT vsp_code) as [No of Collection Centers] 
                    ,sum(srn_net_amount) as [Invoice Amount] 
                    ,sum(TSPL_PAYMENT_PROCESS_DETAIL.Credit_Note_Amount) as [Credit Note Amount]
                    ,sum(TSPL_PAYMENT_PROCESS_DETAIL.Deduction_Amount) as [Debit Note Amount]
                    ,sum(payable_amount) as [Net Payable Amount]
                     From TSPL_PAYMENT_PROCESS_DETAIL  left Join TSPL_PAYMENT_PROCESS_HEAD On TSPL_PAYMENT_PROCESS_HEAD.Doc_No =TSPL_PAYMENT_PROCESS_DETAIL.Doc_No  
                     Left Join tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_PAYMENT_PROCESS_HEAD.MCC_Code_Selected
                    Where 1 = 1 And TSPL_PAYMENT_PROCESS_HEAD.LOC_SEG_CODE ='" & clsCommon.myCstr(StrCode) & "' 
                    and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dtpToDate.Value + "'),103)
                     Group By tspl_mcc_master.mcc_code Order By [Name of the Unit] "

                dt = Nothing
                dt = clsDBFuncationality.GetDataTable(qry)
                GvDetail.MasterTemplate.SummaryRowsBottom.Clear()
                GvDetail.DataSource = Nothing
                GvDetail.Rows.Clear()
                GvDetail.Columns.Clear()
                GvDetail.GroupDescriptors.Clear()
                GvDetail.MasterTemplate.SummaryRowsBottom.Clear()
                GvDetail.MasterView.Refresh()



                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                    Exit Sub
                Else
                    GvDetail.DataSource = dt
                    For ii As Integer = 0 To GvDetail.Columns.Count - 1
                        GvDetail.Columns(ii).ReadOnly = True
                        GvDetail.Columns(ii).BestFit()
                    Next
                    GvDetail.Columns("Code of the Unit").IsVisible = False

                    RadPageView1.SelectedPage = RadPageViewPage3

                    GvDetail.AutoSizeRows = True
                    GvDetail.EnableFiltering = True

                    Dim summaryRowItem As New GridViewSummaryRowItem()

                    Dim Qty1 As New GridViewSummaryItem("No of Collection Centers", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(Qty1)
                    Dim Qty2 As New GridViewSummaryItem("Invoice Amount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(Qty2)
                    Dim Qty3 As New GridViewSummaryItem("Net Payable Amount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(Qty3)
                    Dim Qty4 As New GridViewSummaryItem("Credit Note Amount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(Qty4)
                    Dim Qty5 As New GridViewSummaryItem("Debit Note Amount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(Qty5)

                    GvDetail.ShowGroupPanel = True
                    GvDetail.MasterTemplate.AutoExpandGroups = True
                    GvDetail.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                    GvDetail.BestFitColumns()
                End If
                'ReStoreGridLayout()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    'Private Sub fndMCC__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndMCC._MYValidating
    '    Dim whrCls As String = " 1=1 "
    '    If Not clsMccMaster.isCurrentUserHO() Then
    '        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
    '            whrCls += " and  Location_Code in (" & objCommonVar.strCurrUserLocations & ")  "
    '        End If
    '    End If

    '    whrCls = whrCls & " and   Rejected_Type='N' and Location_Category='MCC'"
    '    fndMCC.Value = clsLocation.getLocSegFinder(whrCls, fndMCC.Value, isButtonClicked)
    '    lblMCC.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Description  from TSPL_GL_SEGMENT_CODE WHERE  Segment_code='" & fndMCC.Value & "' "))
    'End Sub
    'Private Sub fndDocument__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndDocument._MYValidating
    '    Dim whre As String = ""
    '    If clsCommon.myLen(fndMCC.Value) > 0 Then
    '        whre = " and Loc_Seg_Code = '" + fndMCC.Value + "'"
    '    End If
    '    'fndDocument.Value = clsPaymentProcessFarmerHead.getFinder("FarmType='PP' " + whre + "", fndDocument.Value, isButtonClicked)
    '    fndDocument.Value = clsPaymentProcessHead.getFinder("FarmType='PP' " + whre + "", fndDocument.Value, isButtonClicked)
    '    fndMCC.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Loc_Seg_Code  from TSPL_PAYMENT_PROCESS_HEAD WHERE  Doc_No='" & fndDocument.Value & "' "))
    '    lblMCC.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Description  from TSPL_GL_SEGMENT_CODE WHERE  Segment_code='" & fndMCC.Value & "' "))
    '    dtpFromDate.Value = clsCommon.myCDate(clsDBFuncationality.getSingleValue(" select From_Date  from TSPL_PAYMENT_PROCESS_HEAD WHERE  Doc_No='" & fndDocument.Value & "' "))
    '    dtpToDate.Value = clsCommon.myCDate(clsDBFuncationality.getSingleValue(" select To_Date  from TSPL_PAYMENT_PROCESS_HEAD WHERE  Doc_No='" & fndDocument.Value & "' "))
    'End Sub
    'Sub EnableDisiablecontrol(ByVal isEnable As Boolean)
    '    fndMCC.Enabled = isEnable
    '    fndDocument.Enabled = isEnable
    '    dtpFromDate.Enabled = isEnable
    '    dtpToDate.Enabled = isEnable
    'End Sub

End Class
