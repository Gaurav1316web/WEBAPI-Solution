Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System.IO
Imports System.Net
Imports XpertERPEngine
Imports System.Text.RegularExpressions

Public Class rptMilkProcurementReport
    Inherits FrmMainTranScreen

    Private Sub rptMilkProcurementReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtFromDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
        txtToDate.Value = New DateTime(txtFromDate.Value.Year, txtFromDate.Value.Month, DateTime.DaysInMonth(txtFromDate.Value.Year, txtFromDate.Value.Month))
        RadPageView1.SelectedPage = RadPageViewPage1
        Reset()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData(False)
    End Sub
    Sub Reset()
        gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        EnableDisableControl(True)
    End Sub

    Private Sub EnableDisableControl(ByVal val As Boolean)
        RadGroupBox1.Enabled = val
    End Sub

    Private Sub LoadData(ByVal isPrint As Boolean)
        Try
            Dim whrcls As String = ""
            txtFromDate.Value = "01/" & DatePart(DateInterval.Month, txtFromDate.Value) & "/" & DatePart(DateInterval.Year, txtFromDate.Value)
            Dim dtDate As New DataTable()
            whrcls = "  and TSPL_MILK_SRN_HEAD.Posted=1  and convert(date,TSPL_MILK_SRN_HEAD.Doc_Date,103) >= Convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103)  and convert(date,TSPL_MILK_SRN_HEAD.Doc_Date,103) <= Convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103)  "

            Dim qry As String = "select "
            If isPrint Then
                qry += "  Comp_Name, Logo_Img,'" & clsCommon.GetPrintDate(txtFromDate.Value, "MMM-yyyy") & "' as Month,'" & objCommonVar.CurrentUser & "' as UserName, "
            End If
            qry += " Source,Zone_Code,Zone_Name,Sweet_Qty,Sweet_FATKG,Sweet_SNFKG,Sour_Qty,Sour_FATKG,Sour_SNFKG,Curd_Qty,Total_Qty,Total_FAT_KG,Total_SNF_KG,(Total_Qty/" & txtToDate.Value.Day & ") as Avg_Qty,convert(decimal(18,2),((Total_FAT_KG*100)/ (Sweet_Qty+Sour_Qty))) as Avg_FAT,convert(decimal(18,2),((Total_SNF_KG*100)/ (Sweet_Qty+Sour_Qty))) as Avg_SNF from ( Select Source,Zone_Code,max(Zone_Name)Zone_Name,sum(case when QBD = 'SWEET' then Qty else 0 end) as Sweet_Qty,sum(case when QBD = 'SWEET' then FAT_KG else 0 end) as Sweet_FATKG,sum(case when QBD = 'SWEET' then SNF_KG else 0 end) as Sweet_SNFKG,sum(case when QBD = 'SOUR' then Qty else 0 end) as Sour_Qty,sum(case when QBD = 'SOUR' then FAT_KG else 0 end) as Sour_FATKG,
sum(case when QBD = 'SOUR' then SNF_KG else 0 end) as Sour_SNFKG,
sum(case when QBD = 'CURD' then Qty else 0 end) as Curd_Qty,sum(Qty)as Total_Qty,sum(FAT_KG)as Total_FAT_KG,SUM(SNF_KG)as Total_SNF_KG ,SeqNo from (
select TSPL_VENDOR_MASTER.Zone_Code,TSPL_ZONE_MASTER.Description as Zone_Name,xx.* from (
select Against_Uploader_TR_No,Against_Shift_Uploader_TR_No,TSPL_MILK_SRN_HEAD.DOC_DATE,TSPL_MILK_SRN_HEAD.VLC_Code,(case when TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No is not null then isnull (TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Reject_Type,'SWEET') else (case when TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No is not null then isnull (TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type,'SWEET') end) end) as QBD ,Qty,FAT_KG,SNF_KG, (case when TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No is not null then 'MILK RECEIVED AT MCC' ELSE  (case when TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No is not null then 'MILK RECEIVED THROUGH BMC' end) end) AS  Source,  (case when TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No is not null then 1 ELSE  (case when TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No is not null then 2 end) end)  AS SeqNo
from TSPL_MILK_SRN_HEAD
left join TSPL_MILK_SRN_DETAIL on TSPL_MILK_SRN_DETAIL.DOC_CODE=TSPL_MILK_SRN_HEAD.DOC_CODE
left join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No  
left join TSPL_MILK_SHIFT_UPLOADER_DETAIL on TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Shift_Uploader_TR_No  
where 2=2 " & whrcls & "
-----TOTAL MILK RECEIVED DURING MONTH
union all
select  Against_Uploader_TR_No, Against_Shift_Uploader_TR_No,TSPL_MILK_SRN_HEAD.DOC_DATE,TSPL_MILK_SRN_HEAD.VLC_Code,(case when TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No is not null then isnull (TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Reject_Type,'SWEET') else (case when TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No is not null then isnull (TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type,'SWEET') end) end) as QBD,Qty, FAT_KG, SNF_KG,'TOTAL MILK RECEIVED DURING MONTH' AS  Source, 3 AS SeqNo 
 from TSPL_MILK_SRN_HEAD
left join TSPL_MILK_SRN_DETAIL on TSPL_MILK_SRN_DETAIL.DOC_CODE=TSPL_MILK_SRN_HEAD.DOC_CODE
left join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No  
left join TSPL_MILK_SHIFT_UPLOADER_DETAIL on TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Shift_Uploader_TR_No  
where 2=2 " & whrcls & "
) xx 
left join TSPL_VLC_MASTER_HEAD ON TSPL_VLC_MASTER_HEAD.VLC_CODE = xx.VLC_Code
LEFT JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.VENDOR_CODE = TSPL_VLC_MASTER_HEAD.VSP_CODE 
LEFT JOIN TSPL_ZONE_MASTER ON TSPL_ZONE_MASTER.Zone_Code = TSPL_VENDOR_MASTER.ZONE_CODE
) xxx "
            If txtZone.arrValueMember IsNot Nothing Then
                qry += " where xxx.Zone_Code in (" & clsCommon.GetMulcallString(txtZone.arrValueMember) & ")"
            End If
            qry += " group by Zone_Code,Source,SeqNo  )xxxx LEFT JOIN TSPL_COMPANY_MASTER ON 1=1 order by SeqNo,Zone_Code "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterView.Refresh()
            If dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                gv1.GroupDescriptors.Clear()
                gv1.EnableFiltering = True
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                EnableDisableControl(False)
                View()
                SetGridFormation(isPrint)
                gv1.MasterTemplate.AutoExpandGroups = True
                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.BestFitColumns()
                If isPrint Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(Form_ID, CrystalReportFolder.MilkProcurement, dt, "rptMilkProcurement", "Milk Procurement Report")
                    frmCRV = Nothing
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub
    Sub View()
        Try
            If gv1.Rows.Count > 0 Then
                Dim view As New ColumnGroupsViewDefinition()
                view.ColumnGroups.Add(New GridViewColumnGroup("AREA"))
                view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Zone_Name").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("SWEET"))
                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(gv1.Columns("Sweet_Qty").Name)
                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(gv1.Columns("Sweet_FATKG").Name)
                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(gv1.Columns("Sweet_SNFKG").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("SOUR"))
                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows.Add(New GridViewColumnGroupRow())

                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(gv1.Columns("Sour_Qty").Name)
                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(gv1.Columns("Sour_FATKG").Name)
                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(gv1.Columns("Sour_SNFKG").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("CURD"))
                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(gv1.Columns("Curd_Qty").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("TOTAL"))
                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(gv1.Columns("Total_Qty").Name)
                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(gv1.Columns("Total_FAT_KG").Name)
                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(gv1.Columns("Total_SNF_KG").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("AVG"))
                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(gv1.Columns("Avg_Qty").Name)
                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(gv1.Columns("Avg_FAT").Name)
                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(gv1.Columns("Avg_SNF").Name)

                gv1.ViewDefinition = view

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Sub SetGridFormation(ByVal isPrint As Boolean)
        '  gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = True
        gv1.EnableFiltering = True
        gv1.ShowRowHeaderColumn = True
        gv1.ShowGroupPanel = True
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
            gv1.Columns(ii).FormatString = "{0:n2}"
        Next
        gv1.Columns("Zone_Code").IsVisible = False
        gv1.Columns("Zone_Name").HeaderText = "AREA"
        gv1.Columns("Sweet_Qty").HeaderText = "QTY"
        gv1.Columns("Sweet_FATKG").HeaderText = "KG-FAT"
        gv1.Columns("Sweet_SNFKG").HeaderText = "KG-SNF"
        gv1.Columns("Sour_Qty").HeaderText = "QTY"
        gv1.Columns("Sour_FATKG").HeaderText = "KG-FAT"
        gv1.Columns("Sour_SNFKG").HeaderText = "KG-SNF"
        gv1.Columns("Curd_Qty").HeaderText = "QTY"
        gv1.Columns("Total_Qty").HeaderText = "QTY"
        gv1.Columns("Total_FAT_KG").HeaderText = "KG-FAT"
        gv1.Columns("Total_SNF_KG").HeaderText = "KG-SNF"

        gv1.Columns("Avg_Qty").HeaderText = "AVG"
        gv1.Columns("Avg_FAT").HeaderText = "FAT%"
        gv1.Columns("Avg_SNF").HeaderText = "SNF%"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        For ii As Integer = IIf(isPrint, 7, 3) To gv1.Columns.Count - 1
            summaryRowItem.Add(New GridViewSummaryItem(gv1.Columns(ii).Name, "", GridAggregateFunction.Sum))
        Next
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        Dim Itemdescriptor As New GroupDescriptor()
        Itemdescriptor.GroupNames.Add("Source", System.ComponentModel.ListSortDirection.Ascending)
        gv1.GroupDescriptors.Add(Itemdescriptor)
        gv1.MasterTemplate.AutoExpandGroups = True
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Print Date (" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd-MMM-yyyy hh:mm:ss tt") + ")")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptMilkProcurementReport & "'"))
                arrHeader.Add("Month: " & clsCommon.GetPrintDate(txtFromDate.Value, "MMM-yyyy"))
                If txtZone.arrValueMember IsNot Nothing Then
                    arrHeader.Add("Route : " & clsCommon.GetMulcallString(txtZone.arrValueMember) & "  Route Name :" & clsCommon.GetMulcallString(txtZone.arrDispalyMember) & "")
                End If
                clsCommon.MyExportToExcelGrid(Me.Text, gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export.", Me.Text)

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPDF_Click(sender As Object, e As EventArgs) Handles btnPDF.Click
        Try

            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()

                clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text)
                arrHeader.Add("Month: " & clsCommon.GetPrintDate(txtFromDate.Value, "MMM-yyyy"))
                If txtZone.arrValueMember IsNot Nothing Then
                    arrHeader.Add("Route : " & clsCommon.GetMulcallString(txtZone.arrValueMember) & "  Route Name :" & clsCommon.GetMulcallString(txtZone.arrDispalyMember) & "")
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found To export", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtFromDate_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtFromDate.Validating
        txtToDate.Value = New DateTime(txtFromDate.Value.Year, txtFromDate.Value.Month, DateTime.DaysInMonth(txtFromDate.Value.Year, txtFromDate.Value.Month))
    End Sub
    Private Sub txtZone__My_Click(sender As Object, e As EventArgs) Handles txtZone._My_Click
        Try
            Dim qry As String = "select Zone_Code AS Code,Description as Name from TSPL_ZONE_MASTER "
            txtZone.arrValueMember = clsCommon.ShowMultipleSelectForm("ZoneMUL", qry, "Code", "Name", txtZone.arrValueMember, txtZone.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        LoadData(True)
    End Sub
End Class