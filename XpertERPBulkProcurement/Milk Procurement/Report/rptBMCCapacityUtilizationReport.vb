
Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System.IO

Public Class rptBMCCapacityUtilizationReport
    Inherits FrmMainTranScreen
    Dim StrPermission As String

    Private Sub rptBMCCapacityUtilizationReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        StrPermission = clsERPFuncationality.UserWiseAvailableLocationCode()
        RadPageView1.SelectedPage = RadPageViewPage1
        txtToDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy")
        txtFromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy")
        Reset()

    End Sub

    Private Sub txtBMC__My_Click(sender As Object, e As EventArgs) Handles txtBMC._My_Click
            Try
            Dim qry As String = "select  TSPL_VLC_MASTER_HEAD.VLC_Code as [VLC Code],TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [VLC Uploader Code] ,TSPL_VLC_MASTER_HEAD.VLC_Name as [VlC Name] from TSPL_VLC_MASTER_HEAD where TSPL_VLC_MASTER_HEAD.isOwnBMC = 1"
            txtBMC.arrValueMember = clsCommon.ShowMultipleSelectForm(False, "BMCCapacityUtilization", qry, "VLC Code", "VLC Name", txtBMC.arrValueMember, Nothing)
        Catch ex As Exception
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        End Sub

        Private Sub txtZone__My_Click(sender As Object, e As EventArgs) Handles txtZone._My_Click
            Try
                Dim qry As String = " select TSPL_ZONE_MASTER.Zone_Code as Code, TSPL_ZONE_MASTER.Description as Name ,TSPL_ZONE_MASTER.City_Code as [City Code],TSPL_CITY_MASTER.City_Name as [City Name]  from TSPL_ZONE_MASTER left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code = TSPL_ZONE_MASTER.City_Code "

            txtZone.arrValueMember = clsCommon.ShowMultipleSelectForm(False, "BMCCapacityUtilization", qry, "Code", "Name", txtZone.arrValueMember, Nothing)

        Catch ex As Exception
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData()
    End Sub
    Sub Reset()
        gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        txtBMC.arrValueMember = Nothing
        txtZone.arrValueMember = Nothing
        EnableDisableControl(True)
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
    End Sub

    Private Sub EnableDisableControl(ByVal val As Boolean)
        RadGroupBox1.Enabled = val

    End Sub

    Private Sub LoadData()
        Try
            Dim dt As DataTable = New DataTable()
            Dim Qry As String = ""
            Dim whrcls As String = ""
            whrcls = "where 1 = 1 and CONVERT(DATE, Apply_Date, 103) >= CONVERT(DATE, '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "', 103) AND CONVERT(DATE, Apply_Date, 103) <= CONVERT(DATE, '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "', 103)"
            If txtZone.arrValueMember IsNot Nothing AndAlso txtZone.arrValueMember.Count > 0 Then
                whrcls += "  and TSPL_VENDOR_MASTER.Zone_Code in (" + clsCommon.GetMulcallString(txtZone.arrValueMember) + ")  "

            End If
            If txtBMC.arrValueMember IsNot Nothing AndAlso txtBMC.arrValueMember.Count > 0 Then
                whrcls += " and TSPL_VLC_MASTER_HEAD.VLC_Code in (" + clsCommon.GetMulcallString(txtBMC.arrValueMember) + ")"
            End If

            Qry = "select ROW_NUMBER() over(order by apply_date)as 'S.NO',  VLC_Name,VLC_Code_VLC_Uploader,Capacity,GSTNO,Total_Qty, Functional_Day ,BMC_Capacity, Payable_Qty,Rate, convert(decimal(18,2),(Rate * Payable_Qty)) as Amount , Zone_Code , convert(decimal(18,2),(Total_Qty/BMC_Capacity)) as Utilization 
            from ( select Apply_Date,VLC_Name,VLC_Code_VLC_Uploader,Capacity,GSTNO,Total_Qty as Total_Qty, Functional_Day, (Functional_Day* Capacity) as BMC_Capacity,case when  Total_Qty < (Functional_Day* Capacity) then Total_Qty else (Functional_Day * Capacity) end as Payable_Qty  , Rate,Zone_Code  from 
            ( SELECT  Apply_Date, VLC_Code_VLC_Uploader , VLC_Name,Capacity , (GSTFinalNo) GSTNO ,(Qty)Total_Qty,Rate, Zone_Code, ( select count(*) from ( SELECT TSPL_MILK_PURCHASE_INVOICE_CHILLING_CHARGES.Apply_Date FROM TSPL_MILK_PURCHASE_INVOICE_CHILLING_CHARGES 
            where 1 = 1 and CONVERT(DATE, Apply_Date, 103) >= CONVERT(DATE, '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "', 103) AND CONVERT(DATE, Apply_Date, 103) <= CONVERT(DATE, '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "', 103)
            GROUP BY TSPL_MILK_PURCHASE_INVOICE_CHILLING_CHARGES.Apply_Date ) xx ) as Functional_Day  FROM  ( select TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name ,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_CHILLING_CHARGES_SLAB.Capacity,TSPL_VENDOR_MASTER.GSTFinalNo,TSPL_MILK_PURCHASE_INVOICE_CHILLING_CHARGES.Qty,
            TSPL_MILK_PURCHASE_INVOICE_CHILLING_CHARGES.Apply_Date,TSPL_MILK_PURCHASE_INVOICE_CHILLING_CHARGES.Rate,TSPL_VENDOR_MASTER.Zone_Code from TSPL_MILK_PURCHASE_INVOICE_CHILLING_CHARGES  left outer join TSPL_MILK_PURCHASE_INVOICE_HEAD on TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_CHILLING_CHARGES.InvoiceNo
            left outer join TSPL_CHILLING_CHARGES_SLAB on TSPL_CHILLING_CHARGES_SLAB.PK_ID = TSPL_MILK_PURCHASE_INVOICE_CHILLING_CHARGES.Against_Chilling_Slab_PK_ID left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE
            left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_VENDOR_MASTER.Vendor_Code " & whrcls & " ) xx  )xxx )xxxx"
            dt = clsDBFuncationality.GetDataTable(Qry)

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
                SetGridFormation()
                gv1.MasterTemplate.AutoExpandGroups = True
                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub

            End If


        Catch ex As Exception

        End Try
    End Sub

    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
    End Sub
    Sub SetGridFormation()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = True
        gv1.EnableFiltering = True
        gv1.ShowRowHeaderColumn = True
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
        Next

        gv1.Columns("VLC_Name").HeaderText = "DCS"
        gv1.Columns("VLC_Code_VLC_Uploader").HeaderText = "DCS Code"
        gv1.Columns("Capacity").HeaderText = "Capacity in Litres"
        gv1.Columns("GSTNO").HeaderText = "G.S.T.NO"
        gv1.Columns("Total_Qty").HeaderText = "Total Milk Qty(K.G.)"
        gv1.Columns("Functional_Day").HeaderText = "Functional day"
        gv1.Columns("BMC_Capacity").HeaderText = "Permisible Qty for 100% Chilling charges (100% of BMC Capacity)"
        gv1.Columns("Payable_Qty").HeaderText = "QTY of PAYABLE@100% CHILLING CHARGE"
        gv1.Columns("Rate").HeaderText = "Rate of chilling charges"
        gv1.Columns("Amount").HeaderText = "AMOUNT"
        gv1.Columns("Zone_Code").HeaderText = "ZONE"
        gv1.Columns("Utilization").HeaderText = "Utilization"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim Qty As New GridViewSummaryItem("Total_Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Qty)
        Dim Amount As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Amount)
        Dim Payable_Qty As New GridViewSummaryItem("Payable_Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Payable_Qty)
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add("Date Range : " & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "  To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))

            If txtBMC.arrValueMember IsNot Nothing AndAlso txtBMC.arrValueMember.Count > 0 Then
                arrHeader.Add("BMC : " & txtBMC.arrDispalyMember(0))
            End If

            If txtZone.arrValueMember IsNot Nothing AndAlso txtZone.arrValueMember.Count > 0 Then
                arrHeader.Add("Zone : " & txtZone.arrDispalyMember(0))
            End If
            If gv1.Rows.Count > 0 Then
                clsCommon.MyExportToPDF("BMC Capacity Utilization Report", gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)

            Else
                clsCommon.MyMessageBoxShow(Me, "No data found To export", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Print Date (" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd-MMM-yyyy hh:mm:ss tt") + ")")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptBMCCapacityUtilizationReport & "'"))
                arrHeader.Add("Date Range : " & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "  To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))

                If txtBMC.arrValueMember IsNot Nothing AndAlso txtBMC.arrValueMember.Count > 0 Then
                    arrHeader.Add("BMC : " & txtBMC.arrDispalyMember(0))
                End If

                If txtZone.arrValueMember IsNot Nothing AndAlso txtZone.arrValueMember.Count > 0 Then
                    arrHeader.Add("Zone : " & txtZone.arrDispalyMember(0))

                End If

                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
            Else
                Throw New Exception("No data found to export.")

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


End Class