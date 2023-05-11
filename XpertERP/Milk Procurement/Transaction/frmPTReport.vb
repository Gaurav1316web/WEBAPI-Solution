Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Public Class FrmPTReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
   
    Dim btnReferesh As Boolean = False
    Dim tmpValLoad As Boolean = True
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptPTReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub
   
    Sub LoadState()
        Dim qry As String = "select STATE_CODE as [Code] ,STATE_NAME as [Name]  from TSPL_STATE_MASTER "
        cbgState.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgState.ValueMember = "Code"
        cbgState.DisplayMember = "Name"

    End Sub
  
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv.Columns.Count - 1 Step ii + 1
                        gv.Columns(ii).IsVisible = False
                        gv.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Public Sub Load_Report()
        'Dim dt As DataTable
        Dim sQuery As String
        Dim companyADD, CompName, CompCode As String
        If clsCommon.myCDate(txtFromDate.Value) > txtToDate.Value Then
            common.clsCommon.MyMessageBoxShow("From date can not be greater then to Date")
            txtFromDate.Focus()
            Exit Sub
        End If
        'If chkMCCSelect.IsChecked AndAlso cbgMCC.CheckedValue.Count = 0 Then
        '    clsCommon.MyMessageBoxShow("Please select atleast single MCC or select all.")
        '    Exit Sub
        'End If
        If chkStateSelect.IsChecked AndAlso cbgState.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast single State or select all.")
            Exit Sub
        End If
        'If chkRouteSelect.IsChecked AndAlso cbgRoute.CheckedValue.Count = 0 Then
        '    clsCommon.MyMessageBoxShow("Please select atleast single Route or select all.")
        '    Exit Sub
        'End If



        sQuery = ""
        sQuery += " select   TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_COMPANY_MASTER.City_Code)>0 then ', '+TSPL_COMPANY_MASTER.City_Code else ' ' end + case when len(TSPL_COMPANY_MASTER.State )>0 then TSPL_COMPANY_MASTER.State else '' end  as comp_address from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(sQuery)
        companyADD = dt1.Rows(0).Item("comp_address")

        sQuery = ""
        sQuery += " select   TSPL_COMPANY_MASTER.Comp_Name  from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(sQuery)
        CompName = dt2.Rows(0).Item("Comp_Name")


        sQuery = ""
        sQuery += " select   TSPL_COMPANY_MASTER.comp_code  from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        Dim dt5 As DataTable = clsDBFuncationality.GetDataTable(sQuery)
        CompCode = dt5.Rows(0).Item("Comp_Code")

        Dim fromDate As String = txtFromDate.Value
        Dim Todate As String = txtToDate.Value

        sQuery = ""

        ' Ticket No : BHA/15/02/19-000816 By Prabhakar For Divide by Zero
        ' Sanjay Ticket NO-TEC/03/05/19-000474 add Fat and SNF percent in joining TSPL_FAT_SNF_UPLOADER_MASTER & TSPL_MILK_SAMPLE_DETAIL,TSPL_FAT_SNF_UPLOADER_MASTER & TSPL_MILK_PRICE_MASTER
        sQuery += "select final.*,case when final. [Transport Charges / Month]=0 then 0 else(convert (decimal(18,2),final. [Transport Charges / Month]/ nullif (final.[Total Milk Qty in Kg],0))) end as [Per Kg Transport Cost],case when final. Period_days=0 then 0 else(convert (decimal(18,2),final.[Total Milk Qty in Kg] /final. Period_days )) end as [Avg Qty Per Day] ,case when final. [Total VLC]=0 then 0 else(convert (decimal(18,2),final.[Total Milk Qty in Kg] / nullif( final. [Total VLC] ,0) )) end as [Avg Qty per Vlc Day] from (Select xx.*,  TSPL_MILK_Shift_End_Route_DETAIL.Actual_KM As [Total KM], (TSPL_MILK_Shift_End_Route_DETAIL.Amount) As [Transport Charges / Month] ,DateDiff(day,convert(date,'" + txtFromDate.Value + "',103),convert(date,'" + txtToDate.Value + "',103))+1  as Period_days From "
        sQuery += " (Select   convert(varchar,TSPL_MILK_Shift_End_HEAD.DOC_DATE,103) As DOC_DATE ,TSPL_MILK_Shift_End_HEAD.DOC_CODE  as DOC_CODE ,    Max(TSPL_STATE_MASTER.STATE_CODE) As State,    Max(TSPL_STATE_MASTER.STATE_NAME) As [State Name] ,  (TSPL_MCC_MASTER.MCC_Code) As MCC,    Max(TSPL_MCC_MASTER.MCC_NAME) As [MCC NAME],    TSPL_MILK_receipt_DETAIL.Route_CODE As Route,    Max(TSPL_MCC_ROUTE_MASTER.Route_Name) As [Route Name]"
        sQuery += ",      Count(distinct(TSPL_Milk_receipt_detail.VLC_code)) As [Total VLC], max(TSPL_Milk_receipt_detail.VLC_code) as [VLC Code],    Sum(TSPL_milk_receipt_detail.Acc_WEIGHT) As [Total Milk Qty in Kg],max(TSPL_MILK_Shift_End_HEAD.SHIFT)as SHIFT ,  max(tbl1.[Fat %]) [Fat %],  max(tbl1.[Snf %]) [Snf %] , max(tbl1.[Standard Qty]) [Standard Qty]   From TSPL_MILK_Shift_End_HEAD "
        sQuery += "	inner join TSPL_MILK_Shift_End_Route_DETAIL on TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE =TSPL_MILK_Shift_End_HEAD.DOC_CODE "

        sQuery += "inner join TSPL_milk_receipt_Head on TSPL_milk_receipt_Head.Mcc_Code =TSPL_MILK_Shift_End_Head.Mcc_CODE and convert(date, TSPL_milk_receipt_Head.Doc_Date,103)=convert(date,	TSPL_MILK_Shift_End_Head.Doc_Date,103) and TSPL_milk_receipt_Head.Shift=TSPL_MILK_Shift_End_Head.Shift"

        sQuery += " inner join TSPL_milk_receipt_detail on TSPL_milk_receipt_detail.doc_code=TSPL_milk_receipt_Head.doc_code  and TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE =TSPL_milk_receipt_detail.Route_Code"

        sQuery += " left join TSPL_MILK_Shift_End_DETAIL on TSPL_MILK_Shift_End_DETAIL.DOC_CODE =TSPL_MILK_Shift_End_HEAD.DOC_CODE and TSPL_milk_receipt_detail.VLC_DOc_Code =TSPL_MILK_Shift_End_DETAIL.VLC_doc_CODE "

        sQuery += "  Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code =      TSPL_milk_receipt_Head.MCC_CODE  "

        sQuery += " Left Outer Join TSPL_STATE_MASTER On TSPL_STATE_MASTER.STATE_CODE =      TSPL_MCC_MASTER.State_Code  "

        sQuery += " Left Outer Join TSPL_SHIFT_MASTER On TSPL_SHIFT_MASTER.SHIFT_CODE =      TSPL_MILK_Shift_End_DETAIL.SHIFT  "
        sQuery += " Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code =      TSPL_MILK_Shift_End_DETAIL.VLC_CODE  "

        sQuery += " Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code =      TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE "
        sQuery += " left join ( select  tbl0.SHIFT, tbl0.ROUTE_CODE , tbl0.FAT_PERC [Fat %] , tbl0.SNF_PERC [Snf %], convert(decimal(18,2),(tbl0.FAT_KG * [Fat Wtg]/tbl0.[Fat Ratio] + tbl0.SNF_KG *tbl0.[SNF Wtg]/tbl0.[SNF Ratio] )) as [Standard Qty] , tbl0.Price_Code , tbl0.[sample price code] from  ( select TSPL_MILK_RECEIPT_DETAIL.SHIFT ,TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE,   TSPL_MILK_RECEIPT_DETAIL.DOC_CODE,   CONVERT(decimal(18, 2), SUM(TSPL_MILK_SAMPLE_DETAIL.FAT_KG)) FAT_KG,   CONVERT(decimal(18, 2), SUM(TSPL_MILK_SAMPLE_DETAIL.SNF_KG)) SNF_KG,  CONVERT(decimal(18, 2), ((SUM(TSPL_MILK_SAMPLE_DETAIL.FAT_KG) / SUM(TSPL_MILK_SAMPLE_DETAIL.Qty)) * 100)) AS FAT_PERC,  CONVERT(decimal(18, 2), ((SUM(TSPL_MILK_SAMPLE_DETAIL.SNF_KG) / SUM(TSPL_MILK_SAMPLE_DETAIL.Qty)) * 100)) SNF_PERC,  MAX(TSPL_MILK_PRICE_MASTER.Ratio) [Fat Wtg],  MAX(TSPL_MILK_PRICE_MASTER.SNF_Ratio) [SNF Wtg],  SUM(TSPL_MILK_PRICE_MASTER.FAT_Pers) [Fat Ratio],  SUM(TSPL_MILK_PRICE_MASTER.SNF_Pers) [SNF Ratio],  MAX(TSPL_MILK_PRICE_MASTER.Price_Code) [sample price code],  MAX(TSPL_MILK_SAMPLE_DETAIL.Price_Code) Price_Code FROM TSPL_MILK_RECEIPT_HEAD LEFT JOIN TSPL_MILK_RECEIPT_DETAIL on TSPL_MILK_RECEIPT_HEAD.DOC_CODE=TSPL_MILK_RECEIPT_DETAIL.DOC_CODE LEFT JOIN TSPL_MILK_SAMPLE_HEAD  on TSPL_MILK_SAMPLE_HEAD.DOC_CODE =TSPL_MILK_RECEIPT_HEAD.DOC_CODE LEFT JOIN TSPL_MILK_SAMPLE_DETAIL on TSPL_MILK_SAMPLE_HEAD.DOC_CODE=TSPL_MILK_SAMPLE_DETAIL.DOC_CODE  LEFT JOIN TSPL_FAT_SNF_UPLOADER_MASTER on TSPL_MILK_SAMPLE_DETAIL.Price_Code=TSPL_FAT_SNF_UPLOADER_MASTER.Code "
        sQuery += " and TSPL_FAT_SNF_UPLOADER_MASTER.fat=TSPL_MILK_SAMPLE_DETAIL.fat and TSPL_FAT_SNF_UPLOADER_MASTER.snf=TSPL_MILK_SAMPLE_DETAIL.snf "
        sQuery += " LEFT JOIN TSPL_MILK_PRICE_MASTER on TSPL_MILK_PRICE_MASTER.Price_Code=TSPL_FAT_SNF_UPLOADER_MASTER.Price_Code "
        sQuery += " and  TSPL_MILK_PRICE_MASTER.FAT_Pers=TSPL_FAT_SNF_UPLOADER_MASTER.FAT and TSPL_MILK_PRICE_MASTER.SNF_Pers= TSPL_FAT_SNF_UPLOADER_MASTER.SNF "
        sQuery += "  where  convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) <=convert(date,'" + txtToDate.Value + "' ,103)"
        sQuery += " GROUP BY TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE, TSPL_MILK_RECEIPT_DETAIL.DOC_CODE,TSPL_MILK_RECEIPT_DETAIL.SHIFT ) as tbl0 ) as tbl1 on tbl1.ROUTE_CODE = TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE AND tbl1.SHIFT=TSPL_MILK_Shift_End_DETAIL.SHIFT where  CONVERT(date,TSPL_MILK_Shift_End_HEAD.DOC_DATE, 103) >= convert(date,'" + txtFromDate.Value + "',103) and CONVERT(date,TSPL_MILK_Shift_End_HEAD.DOC_DATE, 103) <= convert(date,'" + txtToDate.Value + "' ,103)  "
        sQuery += " Group By TSPL_MCC_MASTER.MCC_Code,TSPL_milk_receipt_detail.Route_CODE,TSPL_MILK_Shift_End_HEAD.DOC_DATE,TSPL_MILK_Shift_End_HEAD.shift,TSPL_MILK_Shift_End_HEAD.DOC_CODE)"
        sQuery += " As xx  Left Outer Join TSPL_MILK_Shift_End_Route_DETAIL    On xx.Route = TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE   and xx.DOC_CODE =TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE ) as Final "
        sQuery += "   where 2 = 2 and convert(date,Final.DOC_DATE,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,Final.DOC_DATE,103) <=convert(date,'" + txtToDate.Value + "' ,103)"
        If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal Then
            sQuery += " and 2=( case when Final.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and Final.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and Final.SHIFT='M' then 3 else 2 end  )"
        End If
        If clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
            sQuery += " and 2=( case when Final.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and Final.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and Final.SHIFT='E' then 3 else 2 end  )"
        End If
        If chkStateSelect.IsChecked And cbgState.CheckedValue.Count > 0 Then
            sQuery += " and final.STATE in (" + clsCommon.GetMulcallString(cbgState.CheckedValue) + ")  "
        End If
        If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal Then
            sQuery += " and 2=( case when Final.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and Final.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and Final.SHIFT='M' then 3 else 2 end  )"
        End If
        If clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
            sQuery += " and 2=( case when Final.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and Final.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and Final.SHIFT='E' then 3 else 2 end  )"
        End If
        If chkStateSelect.IsChecked And cbgState.CheckedValue.Count > 0 Then
            sQuery += " and final.STATE in (" + clsCommon.GetMulcallString(cbgState.CheckedValue) + ")  "
        End If
        If rbtnMCCRouteVLCCSelect.IsChecked Then
            Dim arr As List(Of String) = Nothing
            If cbtMCCRouteVLCC.CheckedValue.Count > 0 Then
                arr = cbtMCCRouteVLCC.CheckedValue(1)
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    sQuery += " and Final.MCC  IN (" + clsCommon.GetMulcallString(arr) + ") "
                Else
                    Throw New Exception("Please select at least one MCC")
                End If
            End If
            If cbtMCCRouteVLCC.CheckedValue.Count > 1 Then
                arr = cbtMCCRouteVLCC.CheckedValue(2)
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    sQuery += " and Final.ROUTE in (" + clsCommon.GetMulcallString(arr) + ")  "
                Else
                    Throw New Exception("Please select at least one Route")
                End If
            End If
            If cbtMCCRouteVLCC.CheckedValue.Count > 2 Then
                arr = cbtMCCRouteVLCC.CheckedValue(3)
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    sQuery += " and final.[VLC Code] in (" + clsCommon.GetMulcallString(arr) + ")  "
                Else
                    Throw New Exception("Please select at least one Route")
                End If
            End If

        End If
        sQuery += " order by convert(date,final.Doc_date,103)"


        Dim dtgv As DataTable = Nothing
        dtgv = New DataTable
        dtgv = clsDBFuncationality.GetDataTable(sQuery)
        If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dtgv
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            FormatGrid()

            RadPageView1.SelectedPage = RadPageViewPage2
        Else
            tmpValLoad = False
            clsCommon.MyMessageBoxShow("No Data Found")
        End If
        ReStoreGridLayout()
    End Sub
    Sub FormatGrid()
        gv.TableElement.TableHeaderHeight = 25
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next

        gv.Columns("DOC_DATE").IsVisible = True
        gv.Columns("DOC_DATE").Width = 100
        gv.Columns("DOC_DATE").HeaderText = "Doc Date"
        gv.Columns("DOC_DATE").FormatString = "{0:d}"

        gv.Columns("DOC_CODE").IsVisible = True
        gv.Columns("DOC_CODE").Width = 130
        gv.Columns("DOC_CODE").HeaderText = "Doc Code"


        gv.Columns("MCC").IsVisible = False
        gv.Columns("MCC").Width = 100
        gv.Columns("MCC").HeaderText = " Mcc Code"

        gv.Columns("MCC NAME").IsVisible = True
        gv.Columns("MCC NAME").Width = 100
        gv.Columns("MCC NAME").HeaderText = " MCC Name"

        gv.Columns("Route").IsVisible = False
        gv.Columns("Route").Width = 100
        gv.Columns("Route").HeaderText = "Route Code"


        gv.Columns("Route Name").IsVisible = True
        gv.Columns("Route Name").Width = 100
        gv.Columns("Route Name").HeaderText = " Route Name"


        gv.Columns("SHIFT").IsVisible = True
        gv.Columns("SHIFT").Width = 100
        gv.Columns("SHIFT").HeaderText = "Shift"

        'kunal

        gv.Columns("Fat %").IsVisible = True
        gv.Columns("Fat %").Width = 100
        gv.Columns("Fat %").HeaderText = "Fat %"

        gv.Columns("Snf %").IsVisible = True
        gv.Columns("Snf %").Width = 100
        gv.Columns("Snf %").HeaderText = "Snf %"

        gv.Columns("Standard Qty").IsVisible = True
        gv.Columns("Standard Qty").Width = 100
        gv.Columns("Standard Qty").HeaderText = "Standard Qty"




        gv.Columns("Total VLC").IsVisible = True
        gv.Columns("Total VLC").Width = 80
        gv.Columns("Total VLC").HeaderText = "Total VLC"

        gv.Columns("Total Milk Qty in Kg").IsVisible = True
        gv.Columns("Total Milk Qty in Kg").Width = 80
        gv.Columns("Total Milk Qty in Kg").HeaderText = "Total Milk Qty in Kg"

        gv.Columns("Total KM").IsVisible = True
        gv.Columns("Total KM").Width = 50
        gv.Columns("Total KM").HeaderText = "Total KM"

        gv.Columns("Transport Charges / Month").IsVisible = True
        gv.Columns("Transport Charges / Month").Width = 100
        gv.Columns("Transport Charges / Month").HeaderText = "Transport Charges / Month"

        gv.Columns("Per Kg Transport Cost").IsVisible = True
        gv.Columns("Per Kg Transport Cost").Width = 100
        gv.Columns("Per Kg Transport Cost").HeaderText = "Per Kg Transport Cost"

        gv.Columns("Avg Qty Per Day").IsVisible = True
        gv.Columns("Avg Qty Per Day").Width = 100
        gv.Columns("Avg Qty Per Day").HeaderText = "Avg Qty Per Day"

        gv.Columns("Avg Qty per Vlc Day").IsVisible = True
        gv.Columns("Avg Qty per Vlc Day").Width = 100
        gv.Columns("Avg Qty per Vlc Day").HeaderText = "Avg Qty per Vlc Day"

       
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As GridViewSummaryItem

        item1 = New GridViewSummaryItem("Standard Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        item1 = New GridViewSummaryItem("Transport Charges / Month", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        item1 = New GridViewSummaryItem("Total Milk Qty in Kg", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        item1 = New GridViewSummaryItem("Total KM", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub

    Private Sub FrmPTReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.R Then
            Load_Report()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Reset()
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.P Then
            '    Load_Report()
        End If
    End Sub
    Sub Reset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)

        LoadState()
        LoadMCCRouteVLCTree()
        LoadShiftFrom()
        LoadShiftTo()
        rbtnMCCRouteVLCCAll.IsChecked = True

        chkStateAll.CheckState = CheckState.Checked

        gv.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                Dim CompName As String = clsDBFuncationality.getSingleValue("Select Comp_Name from TSPL_COMPANY_MASTER Where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'")
                arrHeader.Add(CompName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptPTReport & "'"))
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
                If rbtnMCCRouteVLCCSelect.IsChecked Then
                    Dim arr As List(Of String)
                    If cbtMCCRouteVLCC.CheckedText.Count > 0 Then
                        arr = cbtMCCRouteVLCC.CheckedText(1)
                        If arr IsNot Nothing AndAlso arr.Count > 0 Then
                            arrHeader.Add(("MCC : " + clsCommon.GetMulcallStringWithComma(arr) + " "))
                        End If
                    End If
                    If cbtMCCRouteVLCC.CheckedText.Count > 1 Then
                        arr = cbtMCCRouteVLCC.CheckedText(2)
                        If arr IsNot Nothing AndAlso arr.Count > 0 Then
                            arrHeader.Add(("Route : " + clsCommon.GetMulcallStringWithComma(arr) + " "))
                        End If
                    End If
                    If cbtMCCRouteVLCC.CheckedText.Count > 2 Then
                        arr = cbtMCCRouteVLCC.CheckedText(3)
                        If arr IsNot Nothing AndAlso arr.Count > 0 Then
                            arrHeader.Add(("VLC : " + clsCommon.GetMulcallStringWithComma(arr) + " "))
                        End If
                    End If
                End If
                If chkStateSelect.IsChecked Then
                    Dim strStateName As String = ""
                    For Each StrName As String In cbgState.CheckedDisplayMember
                        If clsCommon.myLen(strStateName) > 0 Then
                            strStateName += ", "
                        End If
                        strStateName += StrName
                    Next
                    Dim strStateCode As String = ""
                    For Each StrCode As String In cbgState.CheckedValue
                        If clsCommon.myLen(strStateCode) > 0 Then
                            strStateCode += ", "
                        End If
                        strStateCode += StrCode
                    Next
                    arrHeader.Add(("State Name: " + strStateName + " "))
                End If

                If exporter = EnumExportTo.Excel Then
                    'Dim sfd As SaveFileDialog = New SaveFileDialog()
                    'Dim filePath As String
                    'sfd.FileName = Me.Text
                    'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                    'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    '    filePath = sfd.FileName
                    'Else
                    '    Exit Sub
                    'End If
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
                    'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                Else
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    clsCommon.MyExportToPDF(" PT Report", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub


    Private Sub chkStateAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkStateAll.ToggleStateChanged
        cbgState.Enabled = Not chkStateAll.IsChecked
    End Sub

   

    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        Load_Report()
    End Sub

    Private Sub BtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub rmSaveLayput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmSaveLayput.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub FrmPTReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        'ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P For Print")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        RadPageView1.SelectedPage = RadPageViewPage1
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)

        LoadState()

        Reset()
    End Sub
    Sub LoadShiftFrom()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Shift")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "M"
        dr("Shift") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "E"
        dr("Shift") = "Evening"
        dt.Rows.Add(dr)

        txtFromShift.DataSource = dt
        txtFromShift.ValueMember = "Code"
        'cbgShift.DisplayMember = "Shift"
    End Sub
    Sub LoadShiftTo()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Shift")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "M"
        dr("Shift") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "E"
        dr("Shift") = "Evening"
        dt.Rows.Add(dr)

        txtToShift.DataSource = dt
        txtToShift.ValueMember = "Code"
        'cbgShift.DisplayMember = "Shift"
    End Sub
    Sub LoadMCCRouteVLCTree()
        Dim qry As String = "select TSPL_VLC_MASTER_HEAD.VLC_Code as Code,TSPL_VLC_MASTER_HEAD.VLC_Name as Name,TSPL_VLC_MASTER_HEAD.Route_Code as ParentCode,3 as Lvl from TSPL_VLC_MASTER_HEAD where len(isnull(TSPL_VLC_MASTER_HEAD.Route_Code,''))>0 union all   select TSPL_MCC_ROUTE_MASTER.Route_Code as Code,TSPL_MCC_ROUTE_MASTER.Route_Name as Name,TSPL_MCC_ROUTE_MASTER.MCC_Code as ParentCode,2 as Lvl from TSPL_MCC_ROUTE_MASTER where len(isnull(TSPL_MCC_ROUTE_MASTER.MCC_Code,''))>0  union all   select TSPL_MCC_MASTER.MCC_Code as Code,TSPL_MCC_MASTER.MCC_NAME as Name,null as ParentCode,1 as Lvl from TSPL_MCC_MASTER"
        cbtMCCRouteVLCC.ValueMember = "Code"
        cbtMCCRouteVLCC.DisplayMember = "Name"
        cbtMCCRouteVLCC.ParentValue = "ParentCode"
        cbtMCCRouteVLCC.DataSource = clsDBFuncationality.GetDataTable(qry)
    End Sub
    Private Sub rbtnMCCRouteVLCCAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnMCCRouteVLCCAll.ToggleStateChanged
        cbtMCCRouteVLCC.Enabled = rbtnMCCRouteVLCCSelect.IsChecked
    End Sub


    Private Sub mniExcel_Click(sender As Object, e As EventArgs) Handles mniExcel.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub mniPDF_Click(sender As Object, e As EventArgs) Handles mniPDF.Click
        print(EnumExportTo.PDF)
    End Sub
End Class
