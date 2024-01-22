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
            strQry = "select Row_Number() Over (Order By (SELECT 1) Asc) as [S No],max(TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader)Mcc_Code_VLC_Uploader,max(TSPL_MCC_MASTER.MCC_NAME)MCC_NAME,COUNT(1) as Entry_No_Of_Dcs,max(TSPL_VLC_MASTER.vle_count) as Total_No_Of_Dcs,
                     Convert (decimal(18,2), sum (TSPL_MILK_COLLECTION_BMCDCS_DCS.Qty)/1.03) as Qty_Ltr,sum (TSPL_MILK_COLLECTION_BMCDCS_DCS.Qty) as Qty_Kg
                        from TSPL_MILK_COLLECTION_BMCDCS
                       left join TSPL_MILK_COLLECTION_BMCDCS_DCS on TSPL_MILK_COLLECTION_BMCDCS_DCS.REF_PK_ID=TSPL_MILK_COLLECTION_BMCDCS.PK_ID
                        left join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_COLLECTION_BMCDCS.MCC_Code
                        
						left join (select count(1) as vle_count,mcc from TSPL_VLC_MASTER_HEAD
			group by mcc)TSPL_VLC_MASTER on TSPL_VLC_MASTER.mcc=TSPL_MCC_MASTER.mcc_Code
                where TSPL_MILK_COLLECTION_BMCDCS.IDate >='" + clsCommon.GetPrintDate(fromDate.Value) + "' and TSPL_MILK_COLLECTION_BMCDCS.IDate<='" + clsCommon.GetPrintDate(dtpToDate.Value) + "'
				group by TSPL_MCC_MASTER.MCC_NAME"

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

        'Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub rptBmcCollection_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        fromDate.Value = DateTime.Now()
        dtpToDate.Value = DateTime.Now()
    End Sub
End Class