Imports common
Imports XpertERPEngine
Imports System.Data.SqlClient

Public Class rptRejectionReport
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim dt As DataTable
    Dim isLoad As Boolean = False
#End Region

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData()
    End Sub

    Private Sub LoadData()
        Try
            Dim qry As String = ""
            If rdbSummary.Checked Then
                qry = " Select *,([Challan Weight]/[Challan Weight]*100) as[TRQ of challan],([Accepted Qty]/[Challan Weight]*100) as[TRQ of Accepted],
                       ([Rejected Qty]/[Challan Weight]*100) as[TRQ of Rejected] ,([Total Partial Qty]/[Challan Weight]*100) as[TRQ of Total Partial],
                       ([Partial Accepted Qty]/[Challan Weight]*100) as[TRQ of Partial Accepted],([Partial Rejected Qty]/[Challan Weight]*100) as[TRQ of Partial Rejected] 
                       from (Select sum([Challan Weight])[Challan Weight],sum([Accepted Qty])[Accepted Qty],sum([Rejected Qty])[Rejected Qty],Sum([Partial Accepted Qty]+[Partial Rejected Qty])[Total Partial Qty],
                        SUM([Partial Accepted Qty])[Partial Accepted Qty],sum([Partial Rejected Qty])[Partial Rejected Qty] from"
            Else
                qry = " Select * from  "
            End If
            qry += "   ( SELECT 
                         xx.RAL,xx.Bill_To_Location,xx.GRN_No,CONVERT(VARCHAR(10), MAX(xx.GRN_Date), 103) AS GRN_Date,convert(varchar(10),max(xx.MRN_Date),103) as MRN_Date,max(xx.MRN_No)MRN_No,max(xx.Item_Code)Item_Code,max(xx.Item_Desc)Item_Desc,
                         max(xx.Vendor_Code)Vendor_Code,max(xx.Vendor_Name)Vendor_Name,max(xx.VehicleNo)VehicleNo,xx.[Challan Weight],max(xx.Unit_code)Unit_code,max(xx.[QC Status])[QC Status],
	                      CAST(CASE WHEN xx.[QC Status] = 'Ok' THEN xx.[Challan Weight] ELSE 0 END AS DECIMAL(18,3)) AS [Accepted Qty],
	                      CAST(CASE WHEN xx.[QC Status] = 'Not Ok' THEN xx.[Challan Weight] ELSE 0 END AS DECIMAL(18,3)) AS [Rejected Qty],
	                      CAST(CASE WHEN xx.[QC Status] = 'Partial Ok' THEN xx.SRN_Qty ELSE 0 END AS DECIMAL(18,3)) AS [Partial Accepted Qty],
	                      CAST(CASE WHEN xx.[QC Status] = 'Partial Ok' THEN xx.[Challan Weight] - xx.SRN_Qty ELSE 0 END AS DECIMAL(18,3)) AS [Partial Rejected Qty], 
	                    sum(InputDataDeductionPer)InputDataDeductionPer,
                        STRING_AGG(CONCAT(ISNULL(xx.Param_Desc, ''),CASE WHEN xx.Param_Desc IS NOT NULL AND xx.InputData IS NOT NULL THEN ', ' ELSE '' END,
                                ISNULL(xx.InputData, '')), ' , ') AS [Param_Desc_InputData]
		                    FROM ( SELECT TSPL_GRN_HEAD.Bill_To_Location,TSPL_GRN_HEAD.Ref_No AS RAL,TSPL_GRN_HEAD.GRN_No,TSPL_GRN_HEAD.GRN_Date,TSPL_MRN_HEAD.MRN_Date,TSPL_MRN_HEAD.MRN_No,TSPL_GRN_DETAIL.Item_Code,
                            TSPL_GRN_DETAIL.Item_Desc,TSPL_GRN_HEAD.Vendor_Code,TSPL_GRN_HEAD.Vendor_Name,TSPL_GRN_HEAD.VehicleNo,CAST(TSPL_GRN_DETAIL.GRN_Qty AS DECIMAL(18,2)) AS [Challan Weight],TSPL_GRN_DETAIL.Unit_code,
                            CASE WHEN TSPL_GRN_HEAD.VisualQCStatus = 1 THEN 'Ok' WHEN TSPL_GRN_HEAD.VisualQCStatus = 2 THEN 'Not Ok' WHEN TSPL_GRN_HEAD.VisualQCStatus = 3 THEN 'Partial Ok' WHEN TSPL_GRN_HEAD.VisualQCStatus = 4 THEN 'On Hold' ELSE 'Pending' END AS [QC Status],
                            TSPL_QC_CHECK_SRN_DETAIL.InputDataDeductionPer,TSPL_SRN_DETAIL.SRN_Qty,tspl_qc_log_sheet_master.Description AS Param_Desc,
                            TSPL_QC_CHECK_SRN_DETAIL.InputData
                        FROM TSPL_GRN_DETAIL
                        LEFT JOIN TSPL_GRN_HEAD ON TSPL_GRN_HEAD.GRN_No = TSPL_GRN_DETAIL.GRN_No
                        LEFT JOIN TSPL_MRN_HEAD ON TSPL_MRN_HEAD.Against_GRN = TSPL_GRN_HEAD.GRN_No
                        LEFT JOIN TSPL_SRN_DETAIL ON TSPL_SRN_DETAIL.GRN_ID = TSPL_GRN_HEAD.GRN_No
                        LEFT JOIN TSPL_QC_CHECK_HEAD ON TSPL_QC_CHECK_HEAD.Gate_Entry_No = TSPL_GRN_HEAD.GRN_No
                        LEFT JOIN TSPL_QC_CHECK_SRN_DETAIL ON TSPL_QC_CHECK_SRN_DETAIL.MRN_No = TSPL_MRN_HEAD.MRN_No 
                           AND TSPL_QC_CHECK_SRN_DETAIL.InputDataDeductionPer > 0
                        LEFT JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code = TSPL_GRN_DETAIL.Item_Code
                        LEFT JOIN tspl_qc_log_sheet_master ON tspl_qc_log_sheet_master.code = TSPL_QC_CHECK_SRN_DETAIL.qc_param_code 
                           AND tspl_qc_log_sheet_master.trans_id = 'standard'
                        WHERE TSPL_ITEM_MASTER.Structure_Code = 'RM' ) xx WHERE 2=2 
                        Group BY xx.Bill_To_Location,xx.RAL,xx.GRN_No,xx.[Challan Weight],xx.[QC Status],xx.SRN_Qty )
                        YY where 2=2 and convert(date,YY.GRN_Date,103)>= convert(date,'" & txtFromDate.Value & "',103) and convert(date,YY.GRN_Date,103)<= convert(date,'" & txtToDate.Value & "',103)"
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                qry += " and yy.Bill_To_Location In (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
            End If
            If TxtRAL.arrValueMember IsNot Nothing AndAlso TxtRAL.arrValueMember.Count > 0 Then
                qry += " and yy.RAL In (" + clsCommon.GetMulcallString(TxtRAL.arrValueMember) + ") "
            End If
            If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                qry += " and yy.RAL In (" + clsCommon.GetMulcallString(TxtRAL.arrValueMember) + ") "
            End If
            If TxtVendor.arrValueMember IsNot Nothing AndAlso TxtVendor.arrValueMember.Count > 0 Then
                qry += " and yy.RAL In (" + clsCommon.GetMulcallString(TxtRAL.arrValueMember) + ") "
            End If
            '' dt.Rows.Add("Full Accepted", "FA")
            'dt.Rows.Add("Partial Accepted", "PA")
            'dt.Rows.Add("Full Rejected", "FR")
            'dt.Rows.Add("Partial Rejected", "PR")
            If clsCommon.CompairString(txtQCStatus.SelectedValue, "FA") = CompairStringResult.Equal Then
                qry += " and yy.[Accepted Qty]>0 "
            ElseIf clsCommon.CompairString(txtQCStatus.SelectedValue, "PA") = CompairStringResult.Equal Then
                qry += " and yy.[Partial Accepted]>0  "
            ElseIf clsCommon.CompairString(txtQCStatus.SelectedValue, "FR") = CompairStringResult.Equal Then
                qry += " and yy.[Rejected Qty]>0 "
            ElseIf clsCommon.CompairString(txtQCStatus.SelectedValue, "PR") = CompairStringResult.Equal Then
                qry += " and yy.[Partial Rejected]>0 "
            End If
            If rdbSummary.Checked Then
                qry += "  )YYZ "
            End If


            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim DTNEW As New DataTable
            If rdbSummary.Checked Then
                Dim DtSummary As DataTable = clsDBFuncationality.GetDataTable(qry)


                DTNEW.Columns.Add("Label", GetType(String))
                DTNEW.Columns.Add("Quantity", GetType(String))
                DTNEW.Columns.Add("[% Of TRQ]", GetType(String))

                Dim labels As String() = {"ChallanWeight", "AcceptedQty", "RejectedQty", "TotalPartialQty", "PartialAcceptedQty", "PartialRejectedQty"}

                If dt.Rows.Count > 0 Then
                    Dim srcRow As DataRow = dt.Rows(0)

                    For i As Integer = 0 To 5
                        Dim newRow As DataRow = DTNEW.NewRow()

                        newRow("Label") = labels(i)                    ' fixed label in first column
                        newRow("Quantity") = srcRow(i).ToString()        ' value from col 1–7
                        newRow("[% Of TRQ]") = srcRow(i + 6).ToString()    ' value from col 8–14

                        DTNEW.Rows.Add(newRow)
                    Next
                End If
            End If
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterView.Refresh()
            gv1.GroupDescriptors.Clear()
            gv1.EnableFiltering = True
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            If dt.Rows.Count > 0 Then
                If rdbSummary.Checked Then
                    gv1.DataSource = DTNEW
                Else
                    gv1.DataSource = dt
                End If

                gv1.BestFitColumns()
                SetGridFormation()
                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.BestFitColumns()
                EnableDisableControls(False)
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFormation()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = True
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
            gv1.Columns(ii).VisibleInColumnChooser = False
        Next

        If rdbSummary.Checked Then
        Else
            gv1.Columns("Bill_To_Location").HeaderText = "Location"
            gv1.Columns("Bill_To_Location").IsVisible = False
            gv1.Columns("MRN_No").HeaderText = "MRN No"
            gv1.Columns("GRN_Date").HeaderText = "GRN Date"
            gv1.Columns("MRN_Date").HeaderText = "MRN Date"

            gv1.Columns("GRN_No").HeaderText = "GRN_No"
            gv1.Columns("GRN_No").IsVisible = False
            gv1.Columns("GRN_No").VisibleInColumnChooser = True

            gv1.Columns("Vendor_Code").HeaderText = "Vendor_Code"
            gv1.Columns("Vendor_Code").IsVisible = False
            gv1.Columns("Vendor_Code").VisibleInColumnChooser = True

            gv1.Columns("Item_Code").HeaderText = "Item_Code"
            gv1.Columns("Item_Code").IsVisible = False
            gv1.Columns("Item_Code").VisibleInColumnChooser = True

            gv1.Columns("Vendor_Name").HeaderText = "Vendor Name"
            gv1.Columns("Item_Desc").HeaderText = "Item Description"
            gv1.Columns("VehicleNo").HeaderText = "Vehicle No"
            gv1.Columns("Challan Weight").HeaderText = "Challan Weight"
            gv1.Columns("Unit_code").HeaderText = "Unit"
            gv1.Columns("QC Status").HeaderText = "QC Status"
            gv1.Columns("InputDataDeductionPer").HeaderText = "Deduction %"
            gv1.Columns("Param_Desc_InputData").HeaderText = "Deviation/Reject Parameter"


            Dim summaryRowItemB As New GridViewSummaryRowItem()

            Dim Bill_Amt As New GridViewSummaryItem("Accepted Qty", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItemB.Add(Bill_Amt)
            Dim SavingAmt As New GridViewSummaryItem("Rejected Qty", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItemB.Add(SavingAmt)
            Dim CurrentAmt As New GridViewSummaryItem("Partial Accepted", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItemB.Add(CurrentAmt)
            Dim CurrentAmt1 As New GridViewSummaryItem("Partial Rejected", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItemB.Add(CurrentAmt1)
            Dim CurrentAmt7 As New GridViewSummaryItem("Challan Weight", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItemB.Add(CurrentAmt7)
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItemB)
            gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        End If

    End Sub
    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Try
            Dim qry As String = " select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER Where Location_Type='Physical' "
            If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                qry += " and TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtVendor__My_Click(sender As Object, e As EventArgs) Handles TxtVendor._My_Click
        Try
            Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name from TSPL_VENDOR_MASTER  WHERE  Status='N'  order by Vendor_Code"
            TxtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", TxtVendor.arrValueMember, TxtVendor.arrDispalyMember)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtRAL__My_Click(sender As Object, e As EventArgs) Handles TxtRAL._My_Click
        Try
            Dim qry As String = " select DocumentCode as TenderNo,DocumentDate as TenderDate from TSPL_TENDER_HEADER "
            TxtRAL.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "TenderNo", "TenderDate", TxtRAL.arrValueMember, TxtRAL.arrDispalyMember)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Try
            Dim qry As String = " Select Item_Code as Code,Item_Desc as Name,Short_Description,Structure_Code from TSPL_ITEM_MASTER "
            txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtItem.arrValueMember, txtItem.arrDispalyMember)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            funreset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub funreset()
        EnableDisableControls(True)
        gv1.DataSource = Nothing
        rdbSummary.Checked = False
        'txtDCS.arrValueMember = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        'If AreaWiseBilling Then
        '    rdbArea.Visible = True
        'Else
        '    rdbArea.Visible = False
        'End If
    End Sub

    Private Sub EnableDisableControls(ByVal val As Boolean)
        RadGroupBox2.Enabled = val
        RadGroupBox3.Enabled = val
        RadGroupBox1.Enabled = val
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                'arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add(objCommonVar.CurrentCompanyName)
                clsCommon.MyExportToExcelGrid("", gv1, arrHeader, Me.Text)
                'transportSql.exportdata(gv1, "", Me.Text, False, arrHeader, False, False, True)
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                'arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add(objCommonVar.CurrentCompanyName)
                clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rptRejectionReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            txtFromDate.Value = clsCommon.GETSERVERDATE
            txtToDate.Value = clsCommon.GETSERVERDATE
            ReportType()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ReportType()
        dt = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Value", GetType(String))
        'dt.Rows.Add("Select", "SL")
        dt.Rows.Add("ALL", "ALL")
        dt.Rows.Add("Full Accepted", "FA")
        dt.Rows.Add("Partial Accepted", "PA")
        dt.Rows.Add("Full Rejected", "FR")
        dt.Rows.Add("Partial Rejected", "PR")
        txtQCStatus.DataSource = dt
        txtQCStatus.DisplayMember = "Code"
        txtQCStatus.ValueMember = "Value"

    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Me.Close()
    End Sub
End Class