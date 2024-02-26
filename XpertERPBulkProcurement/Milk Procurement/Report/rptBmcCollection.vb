Imports common
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports common.UserControls

Public Class rptBmcCollection
    Inherits FrmMainTranScreen
    Sub Reset()
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        btnGo.Enabled = True
        ControlEnableDisable(True)
    End Sub
    Sub ControlEnableDisable(ByVal isEnable As Boolean)
        fromDate.Enabled = isEnable
        dtpToDate.Enabled = isEnable
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData
    End Sub


    Public Sub LoadData()

        Try

            Dim dt As New DataTable
            Dim strQry As String = ""
            If chkZone.Checked = True Then

                strQry = "select Row_Number() Over (Order By (SELECT 1) Asc) as [S No],max(TSPL_ZONE_MASTER.Description) as Zone,max(TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader)Mcc_Code_VLC_Uploader,max(TSPL_MCC_MASTER.MCC_NAME)MCC_NAME,max(TSPL_VLC_MASTER.vle_count) as Total_No_Of_Dcs,round (COUNT(1)/2,0) AS Entry_No_Of_Dcs,
                     Convert (decimal(18,2), sum (TSPL_MILK_COLLECTION_BMCDCS_DCS.Qty)/1.03) as Qty_Ltr,sum (TSPL_MILK_COLLECTION_BMCDCS_DCS.Qty) as Qty_Kg,max(TSPL_MILK_COLLECTION_BMCDCS.Created_By) as CreatedBy,max(TSPL_MILK_COLLECTION_BMCDCS.Created_Date) as CreatedDate,Max (TSPL_MILK_COLLECTION_BMCDCS.Modify_By) as ModifyBy,max(TSPL_MILK_COLLECTION_BMCDCS.Modify_Date) as ModifyDate					 
                        from TSPL_MILK_COLLECTION_BMCDCS
                       left join TSPL_MILK_COLLECTION_BMCDCS_DCS on TSPL_MILK_COLLECTION_BMCDCS_DCS.REF_PK_ID=TSPL_MILK_COLLECTION_BMCDCS.PK_ID
                        left join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_COLLECTION_BMCDCS.MCC_Code     	left join (select count(1) as vle_count,mcc from TSPL_VLC_MASTER_HEAD
			group by mcc)TSPL_VLC_MASTER on TSPL_VLC_MASTER.mcc=TSPL_MCC_MASTER.mcc_Code
			left join TSPL_VLC_MASTER_HEAD  on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_COLLECTION_BMCDCS_DCS.VLC_Code	
			left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code 
			and TSPL_VENDOR_MASTER.Form_Type='VSP'
			 left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code = TSPL_VENDOR_MASTER.Zone_Code
                where TSPL_MILK_COLLECTION_BMCDCS.IDate >='" + clsCommon.GetPrintDate(fromDate.Value) + "' and TSPL_MILK_COLLECTION_BMCDCS.IDate<='" + clsCommon.GetPrintDate(dtpToDate.Value) + "'
				group by TSPL_MCC_MASTER.MCC_NAME having round (COUNT(1)/2,0) >0 order by Zone  "
            Else

                strQry = "select Row_Number() Over (Order By (SELECT 1) Asc) as [S No],max(TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader)Mcc_Code_VLC_Uploader,max(TSPL_MCC_MASTER.MCC_NAME)MCC_NAME,max(TSPL_VLC_MASTER.vle_count) as Total_No_Of_Dcs,round (COUNT(1)/2,0) AS Entry_No_Of_Dcs,
                     Convert (decimal(18,2), sum (TSPL_MILK_COLLECTION_BMCDCS_DCS.Qty)/1.03) as Qty_Ltr,sum (TSPL_MILK_COLLECTION_BMCDCS_DCS.Qty) as Qty_Kg,max(TSPL_MILK_COLLECTION_BMCDCS.Created_By) as CreatedBy,max(TSPL_MILK_COLLECTION_BMCDCS.Created_Date) as CreatedDate,Max (TSPL_MILK_COLLECTION_BMCDCS.Modify_By) as ModifyBy,max(TSPL_MILK_COLLECTION_BMCDCS.Modify_Date) as ModifyDate
                        from TSPL_MILK_COLLECTION_BMCDCS
                       left join TSPL_MILK_COLLECTION_BMCDCS_DCS on TSPL_MILK_COLLECTION_BMCDCS_DCS.REF_PK_ID=TSPL_MILK_COLLECTION_BMCDCS.PK_ID
                        left join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_COLLECTION_BMCDCS.MCC_Code
                        
						left join (select count(1) as vle_count,mcc from TSPL_VLC_MASTER_HEAD
			group by mcc)TSPL_VLC_MASTER on TSPL_VLC_MASTER.mcc=TSPL_MCC_MASTER.mcc_Code
                where TSPL_MILK_COLLECTION_BMCDCS.IDate >='" + clsCommon.GetPrintDate(fromDate.Value) + "' and TSPL_MILK_COLLECTION_BMCDCS.IDate<='" + clsCommon.GetPrintDate(dtpToDate.Value) + "'
				group by TSPL_MCC_MASTER.MCC_NAME"
            End If
            If Checkallmcc.Checked = True Then
                strQry = "select Row_Number() Over (Order By (SELECT 1) Asc) as [S No],TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader,TSPL_MCC_MASTER.MCC_NAME
,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader,xx.Zone,isnull(TSPL_VLC_MASTER.vle_count,0) as Total_No_Of_Dcs,
Isnull(xx.Entry_No_Of_Dcs,0)Entry_No_Of_Dcs,xx.Qty_Ltr,xx.Qty_Kg,xx.CreatedBy,xx.CreatedDate,xx.ModifyBy,xx.ModifyDate
from TSPL_MCC_MASTER 
left Outer join (
select (TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader)Mcc_Code_VLC_Uploader
,max(TSPL_ZONE_MASTER.Description) as Zone,
round (COUNT(1)/2,0) AS Entry_No_Of_Dcs,Convert (decimal(18,2), sum (TSPL_MILK_COLLECTION_BMCDCS_DCS.Qty)/1.03) as Qty_Ltr,
sum (TSPL_MILK_COLLECTION_BMCDCS_DCS.Qty) as Qty_Kg,max(TSPL_MILK_COLLECTION_BMCDCS.Created_By) as CreatedBy,
max(TSPL_MILK_COLLECTION_BMCDCS.Created_Date) as CreatedDate,Max (TSPL_MILK_COLLECTION_BMCDCS.Modify_By) as ModifyBy,
max(TSPL_MILK_COLLECTION_BMCDCS.Modify_Date) as ModifyDate				 
from TSPL_MILK_COLLECTION_BMCDCS
left Outer join TSPL_MILK_COLLECTION_BMCDCS_DCS on TSPL_MILK_COLLECTION_BMCDCS_DCS.REF_PK_ID=TSPL_MILK_COLLECTION_BMCDCS.PK_ID
left Outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_COLLECTION_BMCDCS.MCC_Code 
left Outer join TSPL_VLC_MASTER_HEAD  on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_COLLECTION_BMCDCS_DCS.VLC_Code	
left Outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code and TSPL_VENDOR_MASTER.Form_Type='VSP'
left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code = TSPL_VENDOR_MASTER.Zone_Code
                where TSPL_MILK_COLLECTION_BMCDCS.IDate >='" + clsCommon.GetPrintDate(fromDate.Value) + "' and TSPL_MILK_COLLECTION_BMCDCS.IDate<='" + clsCommon.GetPrintDate(dtpToDate.Value) + "'
				group by TSPL_MILK_COLLECTION_BMCDCS.MCC_Code,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader 
) xx on xx.Mcc_Code_VLC_Uploader=TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader
left Outer join (select count(1) as vle_count,mcc from TSPL_VLC_MASTER_HEAD
group by mcc)TSPL_VLC_MASTER on TSPL_VLC_MASTER.mcc=TSPL_MCC_MASTER.mcc_Code where isnull(TSPL_VLC_MASTER.vle_count,0)>0"

            End If
            dt = clsDBFuncationality.GetDataTable(strQry)
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()

            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                Gv1.DataSource = dt

                RadPageView1.SelectedPage = RadPageViewPage2

                Gv1.EnableFiltering = True
                FormatGrid()
                ControlEnableDisable(False)
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            End If

            Gv1.BestFitColumns()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptMobileAppMilkCollection & "'"))
            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            clsCommon.MyExportToExcelGrid(Me.Text, Gv1, arrHeader, Me.Text)
            common.clsCommon.MyMessageBoxShow(Me, "Exported Successfully.")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptMobileAppMilkCollection & "'"))

            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            clsCommon.MyExportToPDF(Me.Text, Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub


    Sub FormatGrid()
        Dim summaryItem As New GridViewSummaryItem()
        Gv1.TableElement.TableHeaderHeight = 25
        Gv1.MasterTemplate.ShowRowHeaderColumn = True
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = True
            Gv1.Columns("S No").HeaderText = "S No"
            Gv1.Columns("Mcc_Code_VLC_Uploader").HeaderText = "Mcc Code VLC Uploader"
            Gv1.Columns("MCC_NAME").HeaderText = "MCC NAME"
            Gv1.Columns("Entry_No_Of_Dcs").HeaderText = "Entry No Of Dcs"
            Gv1.Columns("Total_No_Of_Dcs").HeaderText = "Total No Of Dcs"
            Gv1.Columns("Qty_Ltr").HeaderText = "Qty Ltr"
            Gv1.Columns("Qty_Kg").HeaderText = "Qty Kg"
        Next
        'Gv1.GroupDescriptors.Add(New GridGroupByExpression("Zone as Zone format ""{0}: {1}"" Group By Zone"))
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As New GridViewSummaryItem("Total_No_Of_Dcs", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("Entry_No_Of_Dcs", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("Qty_Ltr", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("Qty_Kg", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub rptBmcCollection_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        fromDate.Value = DateTime.Now()
        dtpToDate.Value = DateTime.Now()
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Reset()
    End Sub
End Class