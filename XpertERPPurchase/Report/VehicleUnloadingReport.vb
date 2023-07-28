Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class VehicleUnloadingReport
    Inherits FrmMainTranScreen


#Region "Variables"

    Dim arrBack As New List(Of String)
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public arrBatchNo As ArrayList
    Dim arrLoc As String = Nothing
    Dim FORMTYPE As String = Nothing


    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptItemConsumptionReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        'rmExportToExcel.Visible = MyBase.isExport
        btnSplitExport.Visible = MyBase.isExport
    End Sub
#End Region

    Sub Reset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        txtBillToLocation.Value = Nothing
        lblBillToLocation.Text = ""
        'TxtMultiLocation.arrValueMember = Nothing
        Gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        lblUnloadinghrs.Text = " 48 "
        'CmbUnloadinghrs.ValueMember = " 48 "
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click

        Load_Vehicle_Unloading()
    End Sub

    Private Sub Load_Vehicle_Unloading()

        Dim qry As String = ""
        Dim dt As New DataTable()

        Try
            Dim whr As String = " "
            Dim whrs As String = " "
            If txtBillToLocation.Value IsNot Nothing AndAlso txtBillToLocation.Value.Count > 0 Then
                whr += " and TSPL_GRN_HEAD.Bill_To_Location In  ('" + clsCommon.myCstr(txtBillToLocation.Value) + "') "
            Else
                'whr += " and TSPL_LOCATION_MASTER.Location_Type='Physical'  "
                'If clsCommon.myLen(arrLoc) > 0 Then
                'whr += "  and  LOCATION_CODE IN (" + clsCommon.GetMulcallStringWithComma(TxtMultiLocation.arrValueMember) + ") "
                'End If
            End If
            whrs += " and DATEDIFF(HOUR, TSPL_GRN_HEAD.GRN_Date, TSPL_PO_WEIGHTMENT_DETAIL.Unload_Date)> ('" + lblUnloadinghrs.Text + "') "

            qry = "SELECT ('" + lblUnloadinghrs.Text + "') as Diff_Hours,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "' as From_Date,'" + clsCommon.GetPrintDate(txtToDate.Value) + "' as To_Date,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2, 'RAJASTHAN CO-OPERATIVE DAIRY FEDERATION LIMITED' as HeadName, TSPL_LOCATION_MASTER.Location_Desc,TSPL_LOCATION_MASTER.Add1,TSPL_LOCATION_MASTER.Add4,TSPL_GRN_HEAD.Bill_To_Location as 'CF Plant',Ref_No as 'RAL',TSPL_GRN_HEAD.GRN_Date as 'Gate IN Date & Time',right(TSPL_GRN_HEAD.GRN_No,6) as 'Gate In No.',Vendor_Name,TSPL_GRN_DETAIL.Item_Code,TSPL_GRN_DETAIL.Item_Desc,right(TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code,6) as 'Weighment No ',TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date as 'Weighment Date & Time',TSPL_PO_WEIGHTMENT_DETAIL.Unload_Date as 'Gate Out Date & Time',TSPL_GRN_HEAD.VehicleNo,
                    CAST(TSPL_PO_WEIGHTMENT_DETAIL.Net_Weight AS DECIMAL(18, 2)) AS Net_Weight,DATEDIFF(HOUR, TSPL_GRN_HEAD.GRN_Date, TSPL_PO_WEIGHTMENT_DETAIL.Unload_Date) as 'Diff. Hour between Gate In & Gate Out'
                    ,Case when TSPL_GRN_HEAD.Status=0 then 'Unposted' else 'Posted' end as 'Status',
					case when TSPL_GRN_HEAD.IsCancel=1 then 'Cancelled' else 'OK' end as 'Cancel Status'
                    from TSPL_GRN_HEAD 
                    left outer join TSPL_GRN_DETAIL on TSPL_GRN_DETAIL.GRN_No=TSPL_GRN_HEAD.GRN_No
                    left outer join TSPL_PO_WEIGHTMENT_HEAD on TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No=TSPL_GRN_HEAD.GRN_No
                    left outer join TSPL_PO_WEIGHTMENT_DETAIL on TSPL_PO_WEIGHTMENT_DETAIL.Weighment_Code=TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code
                    Left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_GRN_HEAD.Bill_To_Location
                    left outer join TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code = TSPL_GRN_HEAD.Comp_Code
                    where convert(date,TSPL_GRN_HEAD.GRN_Date,103)>= '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and convert(date,TSPL_GRN_HEAD.GRN_Date,103)<= '" + clsCommon.GetPrintDate(txtToDate.Value) + "' " + whr + "  " + whrs + "
                    order by TSPL_GRN_HEAD.Bill_To_Location,Ref_No,TSPL_GRN_HEAD.GRN_Date"

            If clsCommon.myLen(qry) > 0 Then
                dt = clsDBFuncationality.GetDataTable(qry)
            End If

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Gv1.DataSource = Nothing
                Gv1.GroupDescriptors.Clear()
                Gv1.SummaryRowsBottom.Clear()
                Gv1.DataSource = dt
                'gv1.Columns("TransType").IsVisible = False
                'gv1.Columns("PROD_ENTRY_CODE").IsVisible = False
                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.BestFitColumns()
                FormatGrid()
                'ReStoreGridLayout()
            Else
                clsCommon.MyMessageBoxShow("No data found to display.", "Vehicle Unloading Report")
            End If


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try

    End Sub

    Sub FormatGrid()

        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = False
        Next

        Gv1.Columns("HeadName").IsVisible = False
        Gv1.Columns("HeadName").Width = 120
        Gv1.Columns("HeadName").HeaderText = "HeadName"

        Gv1.Columns("Location_Desc").IsVisible = False
        Gv1.Columns("Location_Desc").Width = 120
        Gv1.Columns("Location_Desc").HeaderText = "Location Description"

        Gv1.Columns("Add1").IsVisible = False
        Gv1.Columns("Add1").Width = 120
        Gv1.Columns("Add1").HeaderText = "Add1"

        Gv1.Columns("Add4").IsVisible = False
        Gv1.Columns("Add4").Width = 120
        Gv1.Columns("Add4").HeaderText = "Add4"

        Gv1.Columns("CF Plant").IsVisible = True
        Gv1.Columns("CF Plant").Width = 120
        Gv1.Columns("CF Plant").HeaderText = "Location"

        Gv1.Columns("RAL").IsVisible = True
        Gv1.Columns("RAL").Width = 180
        Gv1.Columns("RAL").HeaderText = "RAL"

        Gv1.Columns("Gate IN Date & Time").IsVisible = True
        Gv1.Columns("Gate IN Date & Time").Width = 150
        Gv1.Columns("Gate IN Date & Time").HeaderText = "Gate In(Date/Time)"

        Gv1.Columns("Gate In No.").IsVisible = True
        Gv1.Columns("Gate In No.").Width = 150
        Gv1.Columns("Gate In No.").HeaderText = "Gate In Number"

        Gv1.Columns("Vendor_Name").IsVisible = True
        Gv1.Columns("Vendor_Name").Width = 150
        Gv1.Columns("Vendor_Name").HeaderText = "Vendor Name"

        Gv1.Columns("Item_Code").IsVisible = True
        Gv1.Columns("Item_Code").Width = 120
        Gv1.Columns("Item_Code").HeaderText = "Item Code"

        Gv1.Columns("Item_Desc").IsVisible = True
        Gv1.Columns("Item_Desc").Width = 120
        Gv1.Columns("Item_Desc").HeaderText = "Item Description"

        Gv1.Columns("Weighment No ").IsVisible = True
        Gv1.Columns("Weighment No ").Width = 120
        Gv1.Columns("Weighment No ").HeaderText = "Weighment Number"

        Gv1.Columns("Weighment Date & Time").IsVisible = True
        Gv1.Columns("Weighment Date & Time").Width = 180
        Gv1.Columns("Weighment Date & Time").HeaderText = "Weighment(Date/Time)"

        Gv1.Columns("Gate Out Date & Time").IsVisible = True
        Gv1.Columns("Gate Out Date & Time").Width = 180
        Gv1.Columns("Gate Out Date & Time").HeaderText = "Gate Out(Date/Time)"

        Gv1.Columns("VehicleNo").IsVisible = True
        Gv1.Columns("VehicleNo").Width = 120
        Gv1.Columns("VehicleNo").HeaderText = "Vehicle Number"

        Gv1.Columns("Net_Weight").IsVisible = True
        Gv1.Columns("Net_Weight").Width = 120
        Gv1.Columns("Net_Weight").HeaderText = "Net Weight"

        Gv1.Columns("Diff. Hour between Gate In & Gate Out").IsVisible = True
        Gv1.Columns("Diff. Hour between Gate In & Gate Out").Width = 120
        Gv1.Columns("Diff. Hour between Gate In & Gate Out").HeaderText = "Hours Difference"

        Gv1.Columns("Logo_Img").IsVisible = False
        Gv1.Columns("Logo_Img").Width = 120
        Gv1.Columns("Logo_Img").HeaderText = "Logo_Img"

        Gv1.Columns("Logo_Img2").IsVisible = False
        Gv1.Columns("Logo_Img2").Width = 120
        Gv1.Columns("Logo_Img2").HeaderText = "Logo_Img2"

        Gv1.Columns("Status").IsVisible = True
        Gv1.Columns("Status").Width = 120
        Gv1.Columns("Status").HeaderText = "Status"

        Gv1.Columns("Cancel Status").IsVisible = True
        Gv1.Columns("Cancel Status").Width = 120
        Gv1.Columns("Cancel Status").HeaderText = "Cancel Status"


        Dim summaryRowItem As New GridViewSummaryRowItem()

        Dim item1 As New GridViewSummaryItem("Net_Weight", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub

    Private Sub txtBillToLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtBillToLocation._MYValidating

        Try
            Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
            Dim WhrCls As String
            If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.FrmSRNMT) = CompairStringResult.Equal Then
                WhrCls = " Location_Type='Virtual' "
            Else
                WhrCls = " (Location_Type='Physical' or Location_Type='WorkOrder')  "
            End If

            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If

            txtBillToLocation.Value = clsCommon.ShowSelectForm("VendorMasteidfndr", qry, "Code", WhrCls, txtBillToLocation.Value, "Code", isButtonClicked)

            If clsCommon.myLen(txtBillToLocation.Value) > 0 Then
                lblBillToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtBillToLocation.Value + "'"))
            Else
                lblBillToLocation.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try


    End Sub

    Private Sub VehicleUnloadingReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim WhrCls As String = String.Empty
        If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.FrmSRNMT) = CompairStringResult.Equal Then
            WhrCls = " and Location_Type='Virtual' "
        Else
            WhrCls = " and Location_Type='Physical' or Location_Type='WorkOrder'  "
        End If
        '  If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.mbtnSRN) = CompairStringResult.Equal Then
        ' txtBillToLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        txtBillToLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' " & WhrCls & " "))
        If clsCommon.myLen(txtBillToLocation.Value) > 0 Then
            lblBillToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_Location_Master where Location_Code='" + txtBillToLocation.Value + "' "))
        End If



    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click

        Try
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "Admin") = CompairStringResult.Equal Then
                    arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

                Else
                    'Dim strLocDesc As String = clsDBFuncationality.getSingleValue("select Location_Desc from tspl_location_master where Location_Code in (" + objCommonVar.strCurrUserLocations + ")")
                    'arrHeader.Add("Location : " + strLocDesc)
                End If

                If txtBillToLocation.Value IsNot Nothing AndAlso txtBillToLocation.Value.Count > 0 Then
                    arrHeader.Add(" Location : " + clsCommon.myCstr(lblBillToLocation.Text))
                End If
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("Vehicle Unloading Report", Gv1, arrHeader, Me.Text)
                ' transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                common.clsCommon.MyMessageBoxShow("Exported Successfully.", Me.Text)
                'Process.Start(filePath)

            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click

        Try

            If Gv1.Rows.Count > 0 Then

                Dim arrHeader As List(Of String) = New List(Of String)()
                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "Admin") = CompairStringResult.Equal Then
                    arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                Else
                    'Dim strLocDesc As String = clsDBFuncationality.getSingleValue("select Location_Desc from tspl_location_master where Location_Code in (" + objCommonVar.strCurrUserLocations + ")")
                    'arrHeader.Add("Location : " + strLocDesc)
                End If
                '
                If txtBillToLocation.Value IsNot Nothing AndAlso txtBillToLocation.Value.Count > 0 Then
                    arrHeader.Add("Location : " + clsCommon.myCstr(lblBillToLocation.Text))
                End If
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Vehicle Unloading Report", Gv1, arrHeader, "Vehicle Unloading Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub CmbUnloadinghrs_Click(sender As Object, e As EventArgs) Handles CmbUnloadinghrs.Click

        If CmbUnloadinghrs.SelectedIndex >= 0 Then
            Dim selectedValue As String = CmbUnloadinghrs.SelectedItem.ToString()

            lblUnloadinghrs.Text = selectedValue

        End If
    End Sub

    Private Sub CmbUnloadinghrs_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles CmbUnloadinghrs.SelectedIndexChanged
        Dim selectedValue As String = CmbUnloadinghrs.SelectedItem.ToString()
        lblUnloadinghrs.Text = selectedValue

    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try

            Dim whr As String = " "
            Dim whrs As String = " "
            If txtBillToLocation.Value IsNot Nothing AndAlso txtBillToLocation.Value.Count > 0 Then
                whr += " and TSPL_GRN_HEAD.Bill_To_Location In  ('" + clsCommon.myCstr(txtBillToLocation.Value) + "') "
            Else
                'whr += " and TSPL_LOCATION_MASTER.Location_Type='Physical'  "
                'If clsCommon.myLen(arrLoc) > 0 Then
                'whr += "  and  LOCATION_CODE IN (" + clsCommon.GetMulcallStringWithComma(TxtMultiLocation.arrValueMember) + ") "
                'End If
            End If
            whrs += " and DATEDIFF(HOUR, TSPL_GRN_HEAD.GRN_Date, TSPL_PO_WEIGHTMENT_DETAIL.Unload_Date)> ('" + lblUnloadinghrs.Text + "') "

            Dim qry As String = " SELECT ('" + lblUnloadinghrs.Text + "') as Diff_Hours,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "' as From_Date,'" + clsCommon.GetPrintDate(txtToDate.Value) + "' as To_Date, TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2, 'RAJASTHAN CO-OPERATIVE DAIRY FEDERATION LIMITED' as HeadName, TSPL_LOCATION_MASTER.Location_Desc,TSPL_LOCATION_MASTER.Add1,TSPL_LOCATION_MASTER.Add4,TSPL_GRN_HEAD.Bill_To_Location as 'CF Plant',Ref_No as 'RAL',TSPL_GRN_HEAD.GRN_Date as 'Gate IN Date & Time',right(TSPL_GRN_HEAD.GRN_No,6) as 'Gate In No.',Vendor_Name,TSPL_GRN_DETAIL.Item_Code,TSPL_GRN_DETAIL.Item_Desc,right(TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code,6) as 'Weighment No ',TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date as 'Weighment Date & Time',TSPL_PO_WEIGHTMENT_DETAIL.Unload_Date as 'Gate Out Date & Time',TSPL_GRN_HEAD.VehicleNo,
                    CAST(TSPL_PO_WEIGHTMENT_DETAIL.Net_Weight AS DECIMAL(18, 2)) AS Net_Weight,DATEDIFF(HOUR, TSPL_GRN_HEAD.GRN_Date, TSPL_PO_WEIGHTMENT_DETAIL.Unload_Date) as 'Diff. Hour between Gate In & Gate Out'
                    ,Case when TSPL_GRN_HEAD.Status=0 then 'Unposted' else 'Posted' end as 'Status',
					case when TSPL_GRN_HEAD.IsCancel=1 then 'Cancelled' else 'OK' end as 'Cancel Status'
                    from TSPL_GRN_HEAD 
                    left outer join TSPL_GRN_DETAIL on TSPL_GRN_DETAIL.GRN_No=TSPL_GRN_HEAD.GRN_No
                    left outer join TSPL_PO_WEIGHTMENT_HEAD on TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No=TSPL_GRN_HEAD.GRN_No
                    left outer join TSPL_PO_WEIGHTMENT_DETAIL on TSPL_PO_WEIGHTMENT_DETAIL.Weighment_Code=TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code
                    Left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_GRN_HEAD.Bill_To_Location
                    left outer join TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code = TSPL_GRN_HEAD.Comp_Code
                    where convert(date,TSPL_GRN_HEAD.GRN_Date,103)>= '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and convert(date,TSPL_GRN_HEAD.GRN_Date,103)<= '" + clsCommon.GetPrintDate(txtToDate.Value) + "' " + whr + "  " + whrs + "
                    order by TSPL_GRN_HEAD.Bill_To_Location,Ref_No,TSPL_GRN_HEAD.GRN_Date "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.Purchase, dt, "VehicleUnloadingReport", "")
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow("No Data Found")
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class