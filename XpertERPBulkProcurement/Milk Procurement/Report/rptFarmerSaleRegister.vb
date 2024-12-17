Imports common
Imports System.IO
Imports System.Net
Imports System.Net.Configuration
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Text.RegularExpressions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared

Public Class rptFarmerSaleRegister
    Inherits FrmMainTranScreen
    Private Sub rptFarmerSaleRegister_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub Reset()
        ToDate.Enabled = True
        fromDate.Enabled = True
        RadGroupBox1.Enabled = True
        txtBMC.Enabled = True
        TxtDCS.Enabled = True
        Gv11.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        Gv11.MasterTemplate.SummaryRowsBottom.Clear()
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFormationOFGv11()
        Gv11.TableElement.TableHeaderHeight = 40
        Gv11.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv11.Columns.Count - 1
            Gv11.Columns(ii).ReadOnly = True
            Gv11.Columns(ii).IsVisible = False
        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()

        Gv11.Columns("Discount").IsVisible = True
        Gv11.Columns("Discount").Width = 100
        Gv11.Columns("Discount").HeaderText = "Discount"

        Gv11.Columns("Amount").IsVisible = True
        Gv11.Columns("Amount").Width = 100
        Gv11.Columns("Amount").HeaderText = "Amount"

        If rdbDetail.IsChecked Then
            Gv11.Columns("Doc_No").IsVisible = True
            Gv11.Columns("Doc_No").Width = 100
            Gv11.Columns("Doc_No").HeaderText = "Document No"

            Gv11.Columns("DocDate").IsVisible = True
            Gv11.Columns("DocDate").Width = 100
            Gv11.Columns("DocDate").HeaderText = "Document Date"

            Gv11.Columns("MPUploaderCode").IsVisible = True
            Gv11.Columns("MPUploaderCode").Width = 100
            Gv11.Columns("MPUploaderCode").HeaderText = "MP Uploader Code"

            Gv11.Columns("VLC_Name").IsVisible = True
            Gv11.Columns("VLC_Name").Width = 100
            Gv11.Columns("VLC_Name").HeaderText = "VLC Name"

            Gv11.Columns("VLC_Code_VLC_Uploader").IsVisible = True
            Gv11.Columns("VLC_Code_VLC_Uploader").Width = 100
            Gv11.Columns("VLC_Code_VLC_Uploader").HeaderText = "VLC Uploader"

            Gv11.Columns("MCC").IsVisible = False
            Gv11.Columns("MCC").Width = 100
            Gv11.Columns("MCC").HeaderText = "MCC Code"

            Gv11.Columns("MCC_NAME").IsVisible = True
            Gv11.Columns("MCC_NAME").Width = 100
            Gv11.Columns("MCC_NAME").HeaderText = "MCC NAME"

            Gv11.Columns("Mcc_Code_VLC_Uploader").IsVisible = True
            Gv11.Columns("Mcc_Code_VLC_Uploader").Width = 100
            Gv11.Columns("Mcc_Code_VLC_Uploader").HeaderText = "MCC Uploader"

        End If

        If rdbDCSWise.IsChecked Then
            Gv11.Columns("VLC_Name").IsVisible = True
            Gv11.Columns("VLC_Name").Width = 100
            Gv11.Columns("VLC_Name").HeaderText = "VLC Name"

            Gv11.Columns("VLC_Code").IsVisible = True
            Gv11.Columns("VLC_Code").Width = 100
            Gv11.Columns("VLC_Code").HeaderText = "VLC Code"

            Gv11.Columns("VLC_Code_VLC_Uploader").IsVisible = True
            Gv11.Columns("VLC_Code_VLC_Uploader").Width = 100
            Gv11.Columns("VLC_Code_VLC_Uploader").HeaderText = "VLC Uploader"

            Gv11.Columns("MCC").IsVisible = False
            Gv11.Columns("MCC").Width = 100
            Gv11.Columns("MCC").HeaderText = "MCC Code"

            Gv11.Columns("MCC_NAME").IsVisible = True
            Gv11.Columns("MCC_NAME").Width = 100
            Gv11.Columns("MCC_NAME").HeaderText = "MCC NAME"

            Gv11.Columns("Mcc_Code_VLC_Uploader").IsVisible = True
            Gv11.Columns("Mcc_Code_VLC_Uploader").Width = 100
            Gv11.Columns("Mcc_Code_VLC_Uploader").HeaderText = "MCC Uploader"
        End If

        If rdbBMCWise.IsChecked Then

            Gv11.Columns("Mcc_Code_VLC_Uploader").IsVisible = True
            Gv11.Columns("Mcc_Code_VLC_Uploader").Width = 100
            Gv11.Columns("Mcc_Code_VLC_Uploader").HeaderText = "MCC Uploader"

            Gv11.Columns("MCC_NAME").IsVisible = True
            Gv11.Columns("MCC_NAME").Width = 100
            Gv11.Columns("MCC_NAME").HeaderText = "MCC NAME"

            Gv11.Columns("MCC").IsVisible = True
            Gv11.Columns("MCC").Width = 100
            Gv11.Columns("MCC").HeaderText = "MCC Code"

            Gv11.Columns("Discount").IsVisible = True
            Gv11.Columns("Discount").Width = 100
            Gv11.Columns("Discount").HeaderText = "Discount"

            Gv11.Columns("Amount").IsVisible = True
            Gv11.Columns("Amount").Width = 100
            Gv11.Columns("Amount").HeaderText = "Amount"
        End If

        If rdbItemWise.IsChecked OrElse rdbDetail.IsChecked Then
            Gv11.Columns("ItemCode").IsVisible = True
            Gv11.Columns("ItemCode").Width = 100
            Gv11.Columns("ItemCode").HeaderText = "Item Code"

            Gv11.Columns("ItemName").IsVisible = True
            Gv11.Columns("ItemName").Width = 150
            Gv11.Columns("ItemName").HeaderText = "Item Name"

            Gv11.Columns("Qty").IsVisible = True
            Gv11.Columns("Qty").Width = 100
            Gv11.Columns("Qty").HeaderText = "Qty"

            Gv11.Columns("UOM").IsVisible = True
            Gv11.Columns("UOM").Width = 100
            Gv11.Columns("UOM").HeaderText = "UOM"

            Gv11.Columns("Rate").IsVisible = True
            Gv11.Columns("Rate").Width = 150
            Gv11.Columns("Rate").HeaderText = "Rate"
        End If

        Dim item1 As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("Discount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)

        RadPageView1.SelectedPage = RadPageViewPage2
        Gv11.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Gv11.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        Gv11.MasterTemplate.AutoExpandGroups = True
        Gv11.AllowAddNewRow = False
        Gv11.ShowGroupPanel = False
        Gv11.BestFitColumns()
    End Sub

    Private Sub btnGO_Click(sender As Object, e As EventArgs) Handles btnGO.Click
        Try
            PageSetupReport_ID = GetReportID()
            LoadData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function GetReportID() As String
        Dim VarID As String = MyBase.Form_ID
        If rdbDetail.IsChecked Then
            VarID += "_DE"
        ElseIf rdbDCSWise.IsChecked Then
            VarID += "_DW"
        ElseIf rdbBMCWise.IsChecked Then
            VarID += "_BW"
        ElseIf rdbItemWise.IsChecked Then
            VarID += "_IW"
        End If
        Return VarID
    End Function

    Public Sub LoadData()
        Try
            Dim whrcls As String = ""
            Dim dt As New DataTable
            Dim BaseQry  As String = Nothing
            whrcls = " "

            If txtBMC.arrValueMember IsNot Nothing AndAlso txtBMC.arrValueMember.Count > 0 Then
                whrcls += " and TSPL_MCC_MASTER.MCC_Code in (" + clsCommon.GetMulcallString(txtBMC.arrValueMember) + ")"
            End If

            If TxtDCS.arrValueMember IsNot Nothing AndAlso TxtDCS.arrValueMember.Count > 0 Then
                whrcls += " and TSPL_VLC_MASTER_HEAD.VLC_Code in (" + clsCommon.GetMulcallString(TxtDCS.arrValueMember) + ")"
            End If

            BaseQry = "SELECT TSPL_REIL_FARMER_SALE.Doc_No as Doc_No, TSPL_REIL_FARMER_SALE.DocDate, TSPL_VLC_MASTER_HEAD.MCC,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader, TSPL_MCC_MASTER.MCC_NAME, TSPL_REIL_FARMER_SALE.VLC_Code, TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader, TSPL_VLC_MASTER_HEAD.VLC_Name, TSPL_REIL_FARMER_SALE.MPUploaderCode,
                            TSPL_REIL_FARMER_SALE.ItemCode,TSPL_REIL_FARMER_SALE.ItemName, TSPL_REIL_FARMER_SALE.Qty, TSPL_REIL_FARMER_SALE.UOM, 
                            TSPL_REIL_FARMER_SALE.Rate, TSPL_REIL_FARMER_SALE.Discount, TSPL_REIL_FARMER_SALE.Amount FROM TSPL_REIL_FARMER_SALE 
							left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_REIL_FARMER_SALE.VLC_Code
							left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
                            where convert(date,TSPL_REIL_FARMER_SALE.DocDate,103) >='" + clsCommon.GetPrintDate(fromDate.Value) + "' and convert(date,TSPL_REIL_FARMER_SALE.DocDate,103)<='" + clsCommon.GetPrintDate(ToDate.Value) + "' " + whrcls + ""

            Dim FinalQuery As String = ""
            If rdbDetail.IsChecked = True Then
                FinalQuery = BaseQry

            ElseIf rdbBMCWise.IsChecked = True Then
                FinalQuery = "Select final.MCC,Max(final.Mcc_Code_VLC_Uploader)Mcc_Code_VLC_Uploader, Max(MCC_NAME)MCC_NAME, Sum(final.Discount)Discount, Sum(final.Amount)Amount 
                            from (" + Environment.NewLine + BaseQry + Environment.NewLine + " ) as final Group By final.MCC"

            ElseIf rdbDCSWise.IsChecked = True Then
                FinalQuery = "Select final.VLC_Code, Max(final.VLC_Code_VLC_Uploader)VLC_Code_VLC_Uploader, Max(final.VLC_Name)VLC_Name, max(final.MCC)MCC, Max(MCC_NAME)MCC_NAME,Max(final.Mcc_Code_VLC_Uploader)Mcc_Code_VLC_Uploader, Sum(final.Discount)Discount, Sum(final.Amount)Amount 
                            from (" + Environment.NewLine + BaseQry + Environment.NewLine + " ) as final Group By final.VLC_Code"

            ElseIf rdbItemWise.IsChecked = True Then
                FinalQuery = "Select final.ItemCode, max(final.ItemName)ItemName, Sum(final.Qty)Qty, max(final.UOM)UOM, max(final.Rate)Rate, sum(final.Discount)Discount, sum(final.Amount)Amount
                            from  (" + Environment.NewLine + BaseQry + Environment.NewLine + " ) as final Group By final.ItemCode"
            End If
            dt = clsDBFuncationality.GetDataTable(FinalQuery)
            Gv11.DataSource = Nothing
            Gv11.Rows.Clear()
            Gv11.Columns.Clear()
            Gv11.GroupDescriptors.Clear()
            Gv11.MasterTemplate.SummaryRowsBottom.Clear()
            Gv11.MasterView.Refresh()
            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                Gv11.DataSource = dt
                RadPageView1.SelectedPage = RadPageViewPage2
                Gv11.EnableFiltering = True
                SetGridFormationOFGv11()
                ControlEnableDisable(False)
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            End If
            Gv11.BestFitColumns()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Sub ControlEnableDisable(ByVal isEnable As Boolean)
        fromDate.Enabled = isEnable
        ToDate.Enabled = isEnable
        RadGroupBox1.Enabled = isEnable
        txtBMC.Enabled = isEnable
        TxtDCS.Enabled = isEnable
    End Sub

    Private Sub rmExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmExcel.Click
        Try
            If Gv11.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Print Date (" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd-MMM-yyyy hh:mm:ss tt") + ")")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptFarmerSaleRegister & "'"))
                arrHeader.Add("Date Range : " & clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + "  To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy"))
                transportSql.QuickExportToExcel(Gv11, "", Me.Text, , arrHeader)
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export.", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmPDF.Click
        Try
            If Gv11.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                clsCommon.MyExportToPDF(Me.Text, Gv11, arrHeader, Me.Text)
                arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy"))
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found To export", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub txtBMC__My_Click(sender As Object, e As EventArgs) Handles txtBMC._My_Click
        Try
            Dim qry As String = "select MCC_Code,MCC_NAME from TSPL_MCC_MASTER where 2=2 "
            txtBMC.arrValueMember = clsCommon.ShowMultipleSelectForm("MCCCode", qry, "MCC_Code", "MCC_NAME", txtBMC.arrValueMember, txtBMC.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtDCS__My_Click(sender As Object, e As EventArgs) Handles TxtDCS._My_Click
        Try
            Dim qry As String = "select VLC_Code,VLC_Name from TSPL_VLC_MASTER_HEAD where 2=2"
            TxtDCS.arrValueMember = clsCommon.ShowMultipleSelectForm("VLCCode", qry, "VLC_Code", "VLC_Code", TxtDCS.arrValueMember, TxtDCS.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class