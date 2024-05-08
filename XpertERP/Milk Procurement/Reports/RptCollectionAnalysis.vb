Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
'======================SHIVANI TYAGI AGAINST[BM00000008418]
Public Class RptCollectionAnalysis
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim arrLoc As String = Nothing
    Private Sub LOCATIONRIGTHS()
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData()

            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadMCCRouteVLCTree()

        Dim qry As String = Nothing
        Dim dt As DataTable = Nothing
        If clsCommon.myLen(arrLoc) > 0 Then
            qry = "select TSPL_VLC_MASTER_HEAD.VLC_Code as Code,TSPL_VLC_MASTER_HEAD.VLC_Name as Name,TSPL_VLC_MASTER_HEAD.Route_Code as ParentCode,3 as Lvl from TSPL_VLC_MASTER_HEAD where len(isnull(TSPL_VLC_MASTER_HEAD.Route_Code,''))>0 union all   select TSPL_MCC_ROUTE_MASTER.Route_Code as Code,TSPL_MCC_ROUTE_MASTER.Route_Name as Name,TSPL_MCC_ROUTE_MASTER.MCC_Code as ParentCode,2 as Lvl from TSPL_MCC_ROUTE_MASTER where len(isnull(TSPL_MCC_ROUTE_MASTER.MCC_Code,''))>0  union all   select TSPL_MCC_MASTER.MCC_Code as Code,TSPL_MCC_MASTER.MCC_NAME as Name,null as ParentCode,1 as Lvl from TSPL_MCC_MASTER   where TSPL_MCC_MASTER.MCC_Code in (" + arrLoc + ") "
            dt = clsDBFuncationality.GetDataTable(qry)
        End If

        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            btnGo.Enabled = False
        Else
            cbtMCCRouteVLCC.DataSource = dt
            cbtMCCRouteVLCC.ValueMember = "Code"
            cbtMCCRouteVLCC.DisplayMember = "Name"
            cbtMCCRouteVLCC.ParentValue = "ParentCode"
        End If

    End Sub
    Private Sub TxtMultiVSP__My_Click(sender As Object, e As EventArgs) Handles TxtMultiVSP._My_Click
        Dim qry As String = "select Vendor_Code as [Code] ,Vendor_Name as [Name]  from TSPL_VENDOR_MASTER  where Form_Type ='VSP'"
        TxtMultiVSP.arrValueMember = clsCommon.ShowMultipleSelectForm("Cust", qry, "Code", "Name", TxtMultiVSP.arrValueMember, TxtMultiVSP.arrDispalyMember)
    End Sub
    Sub LoadReport()
        Try

            Dim sQuery As String = ""

            If txtFromDate.Value > txtToDate.Value Then
                txtFromDate.Focus()
                Throw New Exception("From date can not be greater then to Date")
            End If
            If cbtMCCRouteVLCC.CheckedValue.Count = 0 Then
                Throw New Exception("Please select atleast single MCC or select all.")
            End If

            '            sQuery = " SELECT vlc_code,max(VLC_Name) as VLC_Name,MAX(Total_Shift) AS Total_Shift,sum(shift) as shift,SUM(Milk_Collected) AS Milk_Collected,MAX([Total Days]) AS [Total Days],SUM(FULLDAY) AS FULLDAY ,sum(isnull(HalfDAY,0)) as HalfDAY,(SUM(ISNULL(FULLDAY,0)) +sum(isnull(HalfDAY,0)* 0.5)) as Total FROM (SELECT VLC_CODE,VLC_Name  ,DATEDIFF(DAY ,convert(date,'" & txtFromDate.Value & "',103),convert(date,'" & txtToDate.Value & "',103))+ 1 as [Total Days],case when morning > 0 and Evening >0 then 2  else  1  end as  Milk_Collected ,((DATEDIFF(DAY ,convert(date,'" & txtFromDate.Value & "',103),convert(date,'" & txtToDate.Value & "',103))+ 1) * 2) as Total_Shift,CASE WHEN MORNING >0 AND eVENING >0 THEN 1 END AS FULLDAY,CASE WHEN MORNING = 0 AND eVENING >0 or MORNING > 0 AND eVENING = 0 THEN 1 END AS HalfDAY,shift FROM (SELECT VLC_CODE ,MAX(VLC_Name) AS VLC_Name,DOC_DATE   ,SUM(MORNING) AS MORNING,SUM(eVENING) AS eVENING,sum(shift) as shift FROM " & _
            '" (SELECT VLC_CODE ,MAX(VLC_Name) AS VLC_Name ,DOC_CODE,DOC_DATE,SUM(MORNING) AS MORNING,SUM(eVENING) AS eVENING,count(shift) as shift  FROM (select distinct TSPL_MILK_SRN_HEAD.VLC_CODE ,TSPL_VLC_MASTER_HEAD.VLC_Name  ,TSPL_MILK_RECEIPT_HEAD.DOC_CODE   ,(convert(varchar,TSPL_MILK_SRN_HEAD.DOC_DATE,103)) as  DOC_DATE,case when TSPL_MILK_SRN_HEAD.Shift ='M' then 1 end as  Morning   ,case when TSPL_MILK_SRN_HEAD.Shift ='E' then 1 end as  Evening ,TSPL_MILK_SRN_HEAD.Shift " & _
            ' " from TSPL_MILK_RECEIPT_DETAIL left join TSPL_MILK_RECEIPT_HEAD on TSPL_MILK_RECEIPT_HEAD.DOC_CODE =TSPL_MILK_RECEIPT_DETAIL.DOC_CODE  left join tspl_mcc_master on tspl_mcc_master.MCC_Code =TSPL_MILK_RECEIPT_DETAIL.MCC_CODE  left join tspl_vendor_master on tspl_vendor_master.Vendor_Code =TSPL_MILK_SRN_HEAD.VSP_CODE  left join TSPL_MCC_ROUTE_MASTER on  TSPL_MCC_ROUTE_MASTER.Route_Code =TSPL_MILK_SRN_HEAD.ROUTE_CODE  left join TSPL_VLC_MASTER_HEAD  on TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_SRN_HEAD.VLC_CODE " & _
            '         " where 2 = 2  and  convert(date, TSPL_MILK_SRN_HEAD.DOC_DATE,103) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and  convert(date,TSPL_MILK_RECEIPT_HEAD. DOC_DATE,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'"

            sQuery = " SELECT vlc_code,max(VLC_Name) as VLC_Name,MAX(shift) as shift,SUM(Milk_Collected) AS Milk_Collected,MAX([Total Days]) AS [Total Days],SUM(FULLDAY) AS FULLDAY ,sum(isnull(HalfDAY,0)) as HalfDAY,(SUM(ISNULL(FULLDAY,0)) +sum(ISNULL(HalfDAY,0))*0.5) as Total FROM " &
" (SELECT VLC_CODE,VLC_Name  ,DATEDIFF(DAY ,convert(date,'" & txtFromDate.Value & "',103),convert(date,'" & txtToDate.Value & "',103))+ 1 as [Total Days],case when morning > 0 and Evening >0 then 2  else  1  end as  Milk_Collected ,((DATEDIFF(DAY ,convert(date,'" & txtFromDate.Value & "',103),convert(date,'" & txtToDate.Value & "',103))+ 1) * 2) as Total_Shift,CASE WHEN MORNING >0 AND eVENING >0 THEN 1 END AS FULLDAY,CASE WHEN ISNULL(MORNING,0) = 0 AND ISNULL(eVENING,0) > 0 or ISNULL(MORNING,0) > 0 AND ISNULL(eVENING,0) = 0 THEN 1 END AS HalfDAY,shift FROM " &
" (SELECT VLC_CODE ,MAX(VLC_Name) AS VLC_Name,DOC_DATE   ,SUM(MORNING) AS MORNING,SUM(eVENING) AS eVENING,MAX(shift) as shift FROM " &
 " ( SELECT VLC_CODE ,MAX(VLC_Name) AS VLC_Name ,DOC_CODE,DOC_DATE,SUM(MORNING) AS MORNING,SUM(eVENING) AS eVENING,MAX(shift) as shift  FROM " &
"(SELECT D.VLC_CODE ,D.VLC_Name ,DOC_CODE ,DOC_DATE,Morning,Evening,MCC_NAME,D.MCC_Code,M.SHIFT FROM( (select   TSPL_MILK_SRN_HEAD.VLC_CODE ,TSPL_VLC_MASTER_HEAD.VLC_Name  ,TSPL_MILK_SRN_HEAD.DOC_CODE   ,(convert(varchar,TSPL_MILK_SRN_HEAD.DOC_DATE,103)) as  DOC_DATE,case when TSPL_MILK_SRN_HEAD.Shift ='M' then 1 end as  Morning   ,case when TSPL_MILK_SRN_HEAD.Shift ='E' then 1 end as  Evening  ,tspl_mcc_master.MCC_NAME,tspl_mcc_master.MCC_Code   from TSPL_MILK_SRN_DETAIL left join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE =TSPL_MILK_SRN_DETAIL.DOC_CODE  left join tspl_mcc_master on tspl_mcc_master.MCC_Code =TSPL_MILK_SRN_HEAD.MCC_CODE  left join tspl_vendor_master on tspl_vendor_master.Vendor_Code =TSPL_MILK_SRN_HEAD.VSP_CODE  left join TSPL_MCC_ROUTE_MASTER on  TSPL_MCC_ROUTE_MASTER.Route_Code =TSPL_MILK_SRN_HEAD.ROUTE_CODE  left join TSPL_VLC_MASTER_HEAD  on TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_SRN_HEAD.VLC_CODE " &
                    " where 2 = 2  and  convert(date, TSPL_MILK_SRN_HEAD.DOC_DATE,103) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and  convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' "
            'If rbtnMCCRouteVLCCSelect.IsChecked Then
            Dim arr As List(Of String) = Nothing
            If cbtMCCRouteVLCC.CheckedValue.Count > 0 Then
                arr = cbtMCCRouteVLCC.CheckedValue(1)
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    sQuery += "and TSPL_MILK_SRN_HEAD.MCC_CODE IN (" + clsCommon.GetMulcallString(arr) + ") "
                Else
                    Throw New Exception("Please select at least one MCC")
                End If
            End If
            If cbtMCCRouteVLCC.CheckedValue.Count > 1 Then
                arr = cbtMCCRouteVLCC.CheckedValue(2)
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    sQuery += " and TSPL_MILK_SRN_HEAD.ROUTE_CODE in (" + clsCommon.GetMulcallString(arr) + ")  "
                Else
                    Throw New Exception("Please select at least one Route")
                End If
            End If
            If cbtMCCRouteVLCC.CheckedValue.Count > 2 Then
                arr = cbtMCCRouteVLCC.CheckedValue(3)
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    sQuery += "and TSPL_MILK_SRN_HEAD.VLC_CODE in (" + clsCommon.GetMulcallString(arr) + ")  "
                Else
                    Throw New Exception("Please select at least one VLC")
                End If
            End If
            'End If
            If TxtMultiVSP.arrValueMember IsNot Nothing AndAlso TxtMultiVSP.arrValueMember.Count > 0 Then
                sQuery += " and TSPL_MILK_SRN_HEAD.VSP_CODE in (" + clsCommon.GetMulcallString(TxtMultiVSP.arrValueMember) + ") "
            End If
            sQuery += " )AS D Left Join"
            sQuery += "( SELECT COUNT(SHIFT) AS SHIFT,MCC_CODE  FROM TSPL_MILK_SRN_HEAD WHERE    convert(date, TSPL_MILK_SRN_HEAD.DOC_DATE,103) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and  convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' GROUP BY MCC_CODE)AS M ON M.MCC_CODE=D.MCC_Code  )"
            sQuery += " )aS M GROUP BY DOC_CODE,DOC_DATE,vlc_code) AS TT GROUP BY VLC_CODE,DOC_DATE) AS GG) AS KK group by vlc_code "
            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(sQuery)
            If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                gv.DataSource = dtgv
                gv.GroupDescriptors.Clear()
                gv.MasterTemplate.SummaryRowsBottom.Clear()
                FormatGrid()
                gv.BestFitColumns()
                RadPageView1.SelectedPage = RadPageViewPage2

            Else

                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If
            ReStoreGridLayout()

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

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
    Sub FormatGrid()
        ' Dim strItemCode, head2 As String

        gv.TableElement.TableHeaderHeight = 25
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
            'If chkcatewise.Checked AndAlso ii > 18 Then
            '    gv.Columns(ii).IsVisible = True
            '    gv.Columns(ii).Width = 100
            'End If
        Next

        gv.Columns("vlc_code").IsVisible = True
        gv.Columns("vlc_code").Width = 100
        gv.Columns("vlc_code").HeaderText = "VLC Code"

        

        gv.Columns("VLC_Name").IsVisible = True
        gv.Columns("VLC_Name").Width = 100
        gv.Columns("VLC_Name").HeaderText = "VLC Name"

        'gv.Columns("Total_Shift").IsVisible = False
        'gv.Columns("Total_Shift").Width = 80
        'gv.Columns("Total_Shift").HeaderText = "Total Shift"

        gv.Columns("shift").IsVisible = True
        gv.Columns("shift").Width = 80
        gv.Columns("shift").HeaderText = "Total Shift"

        gv.Columns("Milk_Collected").IsVisible = True
        gv.Columns("Milk_Collected").Width = 80
        gv.Columns("Milk_Collected").HeaderText = "Milk Collected"

        gv.Columns("Total Days").IsVisible = True
        gv.Columns("Total Days").Width = 50
        gv.Columns("Total Days").HeaderText = "Total Days"

        gv.Columns("FULLDAY").IsVisible = True
        gv.Columns("FULLDAY").Width = 100
        gv.Columns("FULLDAY").HeaderText = "Full Day Collection"

        gv.Columns("HalfDAY").IsVisible = True
        gv.Columns("HalfDAY").Width = 100
        gv.Columns("HalfDAY").HeaderText = "Half Day Collection"

        gv.Columns("Total").IsVisible = True
        gv.Columns("Total").Width = 100
        gv.Columns("Total").HeaderText = "Total Day Collection"

        
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As New GridViewSummaryItem("Milk_Collected", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        
        'gv.GroupDescriptors.Add(New GridGroupByExpression("vlc_code as Item format ""{0}: {1}"" Group By vlc_code"))
        

        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        LoadReport()
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim CompName As String = clsDBFuncationality.getSingleValue("Select Comp_Name from TSPL_COMPANY_MASTER Where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'")
            arrHeader.Add(CompName)
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")


            'If rbtnMCCRouteVLCCSelect.IsChecked Then
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

            If TxtMultiVSP.arrValueMember IsNot Nothing AndAlso TxtMultiVSP.arrValueMember.Count > 0 Then
                arrHeader.Add("VSP : " + clsCommon.GetMulcallStringWithComma(TxtMultiVSP.arrDispalyMember))
            End If

            transportSql.applyExportTemplate(gv, PageSetupReport_ID)
            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Collection Analysis Report", gv, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Collection Analysis Report", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error, Me.Text)
        End Try
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub RptCollectionAnalysis_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LOCATIONRIGTHS()

        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")

        ButtonToolTip.SetToolTip(BtnReset, "Pres%s Alt+N Adding New")
        RadPageView1.SelectedPage = RadPageViewPage1
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()

        LoadMCCRouteVLCTree()

        

        Reset()
    End Sub
    Sub Reset()


        gv.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        EnableDisableControl(True)
    End Sub
    Private Sub EnableDisableControl(ByVal val As Boolean)
        RadGroupBox1.Enabled = val
        'RadGroupBox5.Enabled = val
        RadGroupBox2.Enabled = val
    End Sub
    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
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
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If

    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        print(EnumExportTo.PDF)
    End Sub
End Class
