Imports common
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports System.Threading.Thread
Imports System.Threading
Imports Telerik.WinControls.UI.Export
Imports Telerik.WinControls.UI.Export.ExcelML
Imports Microsoft.Office.Interop
Imports Excel = Microsoft.Office.Interop.Excel
Public Class TransferRegister
    Inherits FrmMainTranScreen
    Dim ButtonTooltip As New ToolTip()
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.TransferRegister)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub
    Private Sub TransferRegister_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'RadGroupBox2.Visible = False
        ButtonTooltip.SetToolTip(btnClose, "Press Alt+C For Close The Window")
        ButtonTooltip.SetToolTip(btnExportToExcel, "Press Alt+E For Export Data To Excel")
        ButtonTooltip.SetToolTip(btnRefresh, "Press Ctrl+R For Refresh")
        ButtonTooltip.SetToolTip(btnReset, "Press Alt+R For Reset")
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        Loadlocation()
        SetUserMgmtNew()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Sub Loadlocation()
        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER where Location_Type <> 'Logical'  order by Location_Code"
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbgFromLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        ' cbgFromLocation.DataSource = clsLocation.GetLocationSegments()
        cbgFromLocation.ValueMember = "Code"
        cbgFromLocation.DisplayMember = "Name"
        'cbgFromLocation.ValueMember = "Code"
        'cbgFromLocation.DisplayMember = "Description"
        chkLocationAll.IsChecked = True
        'cbgToLocation.DataSource = clsLocation.GetLocationSegments()
        'cbgToLocation.ValueMember = "Code"
        'cbgToLocation.DisplayMember = "Name"
        ChkLocAll.IsChecked = True
    End Sub

    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgFromLocation.Enabled = Not chkLocationAll.IsChecked
    End Sub


    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        RefreshData()
    End Sub
    Sub RefreshData()
        gvReport.EnableFiltering = True
        If fromDate.Value > ToDate.Value Then
            common.clsCommon.MyMessageBoxShow("Invalid Date Condition")
            Return
        End If
        If chkLocationSelect.IsChecked = True AndAlso cbgFromLocation.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Location or select ALL")
            Return
        ElseIf chkLokSelect.IsChecked = True AndAlso cbgToLocation.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Location or select ALL")
            Return
        End If
        gvReport.DataSource = Nothing
        gvReport.Columns.Clear()
        gvReport.Rows.Clear()
        gvReport.GroupDescriptors.Clear()
        gvReport.MasterTemplate.SummaryRowsBottom.Clear()
        '        Dim qry As String = "Select * ,TransferAmount+ Exice +ECess +HECess as TotalAmountNew from (Select From_Location as FromLocation,TSPL_COMPANY_MASTER.Comp_Name as CompanyName ,Transfer_No as DocumentNumber,Transfer_Date as [Document Date] ,To_Location as ToLocation ,ToLoc_Desc as ToLocationName,Item_Amount  as TransferAmount, " & _
        '        "case when TSPL_TAX_MASTER .Type ='E' then TAX1_Amt Else 0 end as Exice," & _
        '"case when TSPL_TAX_MASTER .Type ='E' then TAX2_Amt  Else 0 end as ECess, " & _
        '"case when TSPL_TAX_MASTER .Type ='E' then TAX3_Amt  Else 0 end as HECess,  Total_Item_Amount as TotalAmount    from TSPL_TRANSFER_HEAD " & _
        '"left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_TRANSFER_HEAD.Comp_Code " & _
        '"left outer join TSPL_TAX_MASTER on TSPL_TAX_MASTER.Tax_Code =TSPL_TRANSFER_HEAD.TAX1 ) as Sub "



        ' ''        Dim qry As String = "Select ROW_NUMBER()  OVER (ORDER BY Transfer_No) AS [S.No], From_Location as FromLocation,TSPL_COMPANY_MASTER.Comp_Name as CompanyName ,Transfer_No as DocumentNumber,Transfer_Date as [Document Date] ,To_Location as ToLocation ,ToLoc_Desc as ToLocationName,Item_Amount  as TransferAmount," & _
        ' ''        "case when TSPL_TAX_MASTER .Type ='E' then TAX1_Amt Else 0 end as Exice, " & _
        ' ''        "case when TSPL_TAX_MASTER .Type ='E' then TAX2_Amt  Else 0 end as ECess, " & _
        ' ''        "case when TSPL_TAX_MASTER .Type ='E' then TAX3_Amt  Else 0 end as HECess, " & _
        ' ''        "case when TSPL_TAX_MASTER .Type ='E' then TAX1_Amt+TAX2_Amt+TAX3_Amt+Item_Amount  Else Item_Amount end as TotalAmountNew from TSPL_TRANSFER_HEAD " & _
        ' ''        "  left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_TRANSFER_HEAD.Comp_Code " & _
        ' ''        "left outer join TSPL_TAX_MASTER on TSPL_TAX_MASTER.Tax_Code =TSPL_TRANSFER_HEAD.TAX1 " & _
        ' ''" Left Outer join TSPL_LOCATION_MASTER as FromLocation on FromLocation.Location_Code  =TSPL_TRANSFER_HEAD.From_Location " & _
        ' ''  "  Left Outer join TSPL_LOCATION_MASTER as TOLocation on TOLocation.Location_Code  =TSPL_TRANSFER_HEAD.To_Location  where  convert(date,Transfer_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND " & _
        ' '' "convert(date,Transfer_Date,103) <= convert(date, '" & ToDate.Value & "',103)and TOLocation.Location_Type='Physical'  and TSPL_TRANSFER_HEAD.Transfer_Type='Lo'  and 2=2 "

        Dim qry As String = "Select  From_Location as FromLocation,TSPL_COMPANY_MASTER.Comp_Name as CompanyName ,Transfer_No as DocumentNumber,Transfer_Date as [Document Date] ,TSPL_LOCATION_MASTER.Location_Code as ToLocation ,TSPL_LOCATION_MASTER.Location_Desc as ToLocationName,Item_Amount  as TransferAmount,case when TSPL_TAX_MASTER .Type ='E' then TAX1_Amt Else 0 end as Exice, case when TSPL_TAX_MASTER .Type ='E' then TAX2_Amt  Else 0 end as ECess, case when TSPL_TAX_MASTER .Type ='E' then TAX3_Amt  Else 0 end as HECess, case when TSPL_TAX_MASTER .Type ='E' then TAX1_Amt+TAX2_Amt+TAX3_Amt+Item_Amount  Else Item_Amount end as [Total Amount] from TSPL_TRANSFER_HEAD   " & _
                            " left outer join TSPL_TAX_MASTER on TSPL_TAX_MASTER.Tax_Code =TSPL_TRANSFER_HEAD.TAX1   " & _
                            " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_TRANSFER_HEAD.Comp_Code  " & _
                             " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .GIT_Location =TSPL_TRANSFER_HEAD.To_Location  " & _
                             " where TSPL_TRANSFER_HEAD.Transfer_Type ='LO'  and convert(date,Transfer_Date,103) >= convert(date, '" + fromDate.Value.Date + "',103) AND  convert(date,Transfer_Date,103) <= convert(date, '" + ToDate.Value.Date + "',103)and   LEN(TSPL_TRANSFER_HEAD.Route_No )=0 "

        If chkItemWise.Checked = True Then
            qry = " select TSPL_TRANSFER_HEAD.From_Location  FromLocation,TSPL_COMPANY_MASTER.Comp_Name as CompanyName,TSPL_TRANSFER_HEAD.Transfer_No as DocumentNumber,TSPL_TRANSFER_HEAD.Item_Type as [Transfer Type] ,TSPL_TRANSFER_HEAD.Transfer_Date as [Document Date],TSPL_LOCATION_MASTER.Location_Code  ToLocation,TSPL_LOCATION_MASTER.Location_Desc  ToLocationName, TSPL_TRANSFER_DETAIL.Item_Code as [ItemCode] ,TSPL_TRANSFER_DETAIL.MRP ,TSPL_TRANSFER_DETAIL.Item_Qty as [Item Qty] ,TSPL_TRANSFER_DETAIL.Basic_Price as [Rate] ,TSPL_TRANSFER_DETAIL.Amount as TransferAmount ,case when TSPL_TAX_MASTER .Type ='E' then TSPL_TRANSFER_DETAIL.TAX1_Amt Else 0 end as Exice, case when TSPL_TAX_MASTER .Type ='E' then TSPL_TRANSFER_DETAIL.TAX2_Amt  Else 0 end as ECess, case when TSPL_TAX_MASTER .Type ='E' then TSPL_TRANSFER_DETAIL.TAX3_Amt  Else 0 end as HECess," & _
                  " TSPL_TRANSFER_DETAIL.Amount+case when TSPL_TAX_MASTER .Type ='E' then TSPL_TRANSFER_DETAIL.TAX1_Amt Else 0 end + case when TSPL_TAX_MASTER .Type ='E' then TSPL_TRANSFER_DETAIL.TAX2_Amt  Else 0 end + case when TSPL_TAX_MASTER .Type ='E' then TSPL_TRANSFER_DETAIL.TAX3_Amt  Else 0 end   as [Total Amount] from TSPL_TRANSFER_DETAIL inner join TSPL_TRANSFER_HEAD on TSPL_TRANSFER_DETAIL.Transfer_No =TSPL_TRANSFER_HEAD.Transfer_No " & _
                 "  left outer join TSPL_TAX_MASTER on TSPL_TAX_MASTER.Tax_Code =TSPL_TRANSFER_HEAD.TAX1 " & _
                 " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_TRANSFER_HEAD.Comp_Code  " & _
                 " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .GIT_Location =TSPL_TRANSFER_HEAD.To_Location " & _
                " where   TSPL_TRANSFER_HEAD.Transfer_Type ='LO'  and LEN(TSPL_TRANSFER_HEAD.Route_No )=0 and  convert(date,Transfer_Date,103) >= convert(date, '" + fromDate.Value.Date + "',103) AND convert(date,Transfer_Date,103) <= convert(date, '" + ToDate.Value.Date + "',103) "
            '" ORDER BY TSPL_TRANSFER_HEAD.Transfer_No "
        End If

        If Not chkLocationAll.IsChecked = True Then
            qry += " and TSPL_TRANSFER_HEAD.From_Location in (" + clsCommon.GetMulcallString(cbgFromLocation.CheckedValue) + ") "
        End If


        'Dim strLocAll, StrLocationAll As String
        'If chkLocationAll.IsChecked = True Then
        '    StrLocationAll = "Y"
        'Else
        '    StrLocationAll = "N"
        'End If
        'If ChkLocAll.IsChecked = True Then
        '    strLocAll = "Y"
        'Else
        '    strLocAll = "N"
        'End If
        'If strLocAll = "N" AndAlso StrLocationAll = "N" Then
        ' qry += " and FromLocation.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgFromLocation.CheckedValue) + ") "
        '    ' qry += " and TOLocation.Loc_Segment_Code in  (" + clsCommon.GetMulcallString(cbgToLocation.CheckedValue) + ") "
        'ElseIf StrLocationAll = "N" Then
        '    qry += " and FromLocation.Loc_Segment_Code in  (" + clsCommon.GetMulcallString(cbgFromLocation.CheckedValue) + ") "
        'ElseIf strLocAll = "N" Then
        '    qry += " and TOLocation.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgToLocation.CheckedValue) + ")  "

        'End If

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry + " ORDER BY TSPL_TRANSFER_HEAD.Transfer_Date,TSPL_TRANSFER_HEAD.Transfer_No ")


        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
            Exit Sub
        Else
            gvReport.DataSource = dt
            '   SetGridFormationOFgvReport()
            SetGridOFGridView()
        End If


    End Sub
    Sub SetGridOFGridView()
        
        gvReport.TableElement.TableHeaderHeight = 40
        gvReport.MasterTemplate.ShowRowHeaderColumn = False

        For ii As Integer = 0 To gvReport.Columns.Count - 1
            gvReport.Columns(ii).ReadOnly = True
            ' gvReport.Columns(ii).IsVisible = False
        Next

        'gvReport.Columns("S.No").IsVisible = True
        'gvReport.Columns("S.No").Width = 50
        'gvReport.Columns("S.No").HeaderText = "S.No"
        'Dim intCurrRow As Integer = gvReport.CurrentRow.Index
        'gvReport.CurrentRow.Cells(0).Value = clsCommon.myCdbl(intCurrRow + 1)
        'If intCurrRow = gvReport.Rows.Count - 1 Then
        '    gvReport.Rows.AddNew()
        '    gvReport.CurrentRow = gvReport.Rows(intCurrRow)
        'End If

        gvReport.Columns("FromLocation").IsVisible = True
        gvReport.Columns("FromLocation").Width = 100
        gvReport.Columns("FromLocation").HeaderText = "From Location"
        '  gvReport.Columns("FromLocation").BestFit()

        gvReport.Columns("CompanyName").IsVisible = True
        gvReport.Columns("CompanyName").Width = 100
        gvReport.Columns("CompanyName").HeaderText = "Company Name"
        '  gvReport.Columns("CompanyName").BestFit()

        gvReport.Columns("DocumentNumber").IsVisible = True
        gvReport.Columns("DocumentNumber").Width = 100
        gvReport.Columns("DocumentNumber").HeaderText = "Document Number"
        ' gvReport.Columns("DocumentNumber").BestFit()




        gvReport.Columns("Document Date").IsVisible = True
        gvReport.Columns("Document Date").Width = 130
        gvReport.Columns("Document Date").FormatString = "{0:d}"
        gvReport.Columns("Document Date").HeaderText = "Document Date"
        '   gvReport.Columns("Document Date").BestFit()

        gvReport.Columns("ToLocation").IsVisible = True
        gvReport.Columns("ToLocation").Width = 100
        gvReport.Columns("ToLocation").HeaderText = "To Location"
        '  gvReport.Columns("ToLocation").BestFit()

        gvReport.Columns("ToLocationName").IsVisible = True
        gvReport.Columns("ToLocationName").Width = 150
        gvReport.Columns("ToLocationName").HeaderText = "To Location Name"
        '  gvReport.Columns("ToLocationName").BestFit()

        gvReport.Columns("TransferAmount").IsVisible = True
        gvReport.Columns("TransferAmount").Width = 100
        gvReport.Columns("TransferAmount").HeaderText = "Document Amount"
        '  gvReport.Columns("TransferAmount").BestFit()

        gvReport.Columns("Exice").IsVisible = True
        gvReport.Columns("Exice").Width = 100
        gvReport.Columns("Exice").HeaderText = "Exice"
        ' gvReport.Columns("Exice").BestFit()

        gvReport.Columns("ECess").IsVisible = True
        gvReport.Columns("ECess").Width = 100
        gvReport.Columns("ECess").HeaderText = "ECess"
        '   gvReport.Columns("ECess").BestFit()

        gvReport.Columns("HECess").IsVisible = True
        gvReport.Columns("HECess").Width = 100
        gvReport.Columns("HECess").HeaderText = "HECess"
        '   gvReport.Columns("HECess").BestFit()

        gvReport.Columns("Total Amount").IsVisible = True
        gvReport.Columns("Total Amount").Width = 100
        gvReport.Columns("Total Amount").HeaderText = "Total Amount"
      
        Dim summaryRowItem As New GridViewSummaryRowItem()
     
        Dim item6 As New GridViewSummaryItem("TransferAmount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)
        Dim item7 As New GridViewSummaryItem("Exice", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)
        Dim item8 As New GridViewSummaryItem("ECess", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)
        Dim item9 As New GridViewSummaryItem("HECess", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item9)
        Dim item10 As New GridViewSummaryItem("Total Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item10)

        gvReport.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub
    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        ResetData()
    End Sub
    Sub ResetData()
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        Loadlocation()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnExportToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportToExcel.Click
        ExportToExcel1()
    End Sub
    Sub ExportToExcel1()
        RefreshData()
        If gvReport.Rows.Count <= 0 Then
            Return
        End If
        Dim arrHeader As List(Of String) = New List(Of String)()
        arrHeader.Add("Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy"))
        If chkLocationSelect.IsChecked Then
            Dim strLoca As String = ""
            For Each Str As String In cbgFromLocation.CheckedDisplayMember
                If clsCommon.myLen(strLoca) > 0 Then
                    strLoca += ", "
                End If
                strLoca += Str
            Next
            arrHeader.Add(" From Location : " + strLoca)

        End If
        'If chkLokSelect.IsChecked Then
        '    Dim strLoca As String = ""
        '    For Each Str As String In cbgToLocation.CheckedDisplayMember
        '        If clsCommon.myLen(strLoca) > 0 Then
        '            strLoca += ", "
        '        End If
        '        strLoca += Str
        '    Next
        '    arrHeader.Add(" To Location : " + strLoca)

        'End If
        clsCommon.MyExportToExcelGrid("TransferRegister", gvReport, arrHeader, Me.Text)

    End Sub
  
    Private Sub exporter_ExcelCellFormatting(ByVal sender As Object, ByVal e As ExcelCellFormattingEventArgs)
        If e.GridRowInfoType Is GetType(GridViewDataRowInfo) Then
        End If
    End Sub
 
    
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub ChkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles ChkLocAll.ToggleStateChanged
        cbgToLocation.Enabled = Not ChkLocAll.IsChecked
    End Sub

    Private Sub TransferRegister_KeyDown_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.R AndAlso btnReset.Enabled Then
            ResetData()
        ElseIf e.Control AndAlso e.KeyCode = Keys.R AndAlso btnRefresh.Enabled Then
            RefreshData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.E AndAlso btnExportToExcel.Enabled AndAlso btnExportToExcel.Visible Then
            ExportToExcel1()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub
End Class
