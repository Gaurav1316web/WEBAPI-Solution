Imports common
Imports System.Data.SqlClient

Public Class FrmUserPerformanceDetail
    Inherits FrmMainTranScreen
    Dim dt As DataTable

    Private Sub FrmUserPerformanceDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        lblModule.Visible = False
        cboModule.Visible = False
        LoadCompany()
        LoadModule()
        LoadUser()
        LoadLocation()
        Reset()
        btnCashMemoStatus.Visible = True
    End Sub
    Sub LoadCompany()
        Dim qry As String = "SELECT Comp_Code as [Company Code],Comp_Name as [Company Name],DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0"
        Dim ArrHideColumn As New List(Of String)
        ArrHideColumn.Add("DataBase_Name")
        cbgCompany.ArrHideColumns = ArrHideColumn
        cbgCompany.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCompany.ValueMember = "DataBase_Name"
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmUserPerformanceDetail)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
        End If
    End Sub

    Private Sub Reset()
        txtFromDate.Value = clsCommon.GetDateWithStartTime(clsCommon.GETSERVERDATE)
        txtToDate.Value = clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE)
        chkUserAll.IsChecked = True
        chkLocAll.IsChecked = True
        gvReport.GroupDescriptors.Clear()
        gvReport.MasterTemplate.SummaryRowsBottom.Clear()
        dt = Nothing
        gvReport.DataSource = Nothing
        gvReport.Columns.Clear()
        gvReport.Rows.Clear()
    End Sub

    ''
    ''Loads The Item In Combo Box (Module)
    ''

    Private Sub LoadModule()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Sales & Distribution"
        dr("Name") = "Sales & Distribution"
        dt.Rows.Add(dr)

        cboModule.DataSource = dt
        cboModule.DisplayMember = "Name"
        cboModule.ValueMember = "Code"

    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnExportToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportToExcel.Click
        ExportToExcel()
    End Sub

    Private Sub ExportToExcel()
        Try
            RefreshData()
            If gvReport.DataSource Is Nothing OrElse gvReport.Rows.Count <= 0 Then
                Throw New Exception("No Data found to Export")
            End If
            ExportToExcelGV()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub RefreshData()
        Try
            Dim DateFilter As String = "AND CONVERT(Date,Created_Date, 103)>=COnvert(Date,'" + txtFromDate.Value + "', 103) AND CONVERT(Date,Created_Date, 103)<=Convert(Date,'" + txtToDate.Value + "', 103) "

            If cbgCompany.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one Company or select ALL")
                Return
            End If

            If chkUserSelect.IsChecked AndAlso cbgUser.CheckedValue.Count <= 0 Then
                Throw New Exception("Please Select at least Single User or Select All")
                Return
            End If

            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                Throw New Exception("Please Select at least Single location or Select All")
                Return
            End If

            Dim qry As String = ""
            If clsCommon.CompairString(cboModule.SelectedValue, "Sales & Distribution") = CompairStringResult.Equal Then
                Dim location1 As String = ""
                Dim Location1_1 As String = ""
                Dim Location2 As String = ""
                Dim Location3 As String = ""
                Dim DateFilterQckSet As String = "AND CONVERT(Date," + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Created_Date, 103)>=CONVERT(DATE,'" + txtFromDate.Value.Date + "', 103) AND CONVERT(DATE," + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Created_Date, 103)<=CONVERT(DATE, '" + txtToDate.Value.Date + "', 103) "
                Dim DateFilterShPMNT As String = "AND Created_Date >= '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy hh:mm tt") + "' AND Created_Date <= '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy hh:mm tt") + "' "
                If cbgLocation.CheckedValue.Count > 0 Then
                    location1 = " AND From_Location in (" + (clsCommon.GetMulcallString(cbgLocation.CheckedValue)) + ")"
                    Location1_1 = " AND To_Location in (" + (clsCommon.GetMulcallString(cbgLocation.CheckedValue)) + ")"
                    Location2 = " AND Location in (" + (clsCommon.GetMulcallString(cbgLocation.CheckedValue)) + ")"
                    Location3 = " AND Loc_Code in (" + (clsCommon.GetMulcallString(cbgLocation.CheckedValue)) + ")"
                End If

                qry = " Select * from ( SELECT TSPL_USER_MASTER.User_Code, TSPL_USER_MASTER.User_Name, " & _
                        " (SELECT COUNT(*) FROM " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD WHERE " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Type='LO' AND TSPL_TRANSFER_HEAD.Created_By=" + clsCommon.ReplicateDBString + "TSPL_USER_MASTER.User_Code " + location1 + " " + DateFilter + " ) AS [LoadOut], " & _
                        " (SELECT COUNT(*) FROM " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD WHERE " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Type='LI' AND TSPL_TRANSFER_HEAD.Created_By=" + clsCommon.ReplicateDBString + "TSPL_USER_MASTER.User_Code " + Location1_1 + " " + DateFilter + ") AS [LoadIn], " & _
                        " (SELECT COUNT(*) FROM " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER WHERE Shipment_Type='Sale' AND " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Created_By=" + clsCommon.ReplicateDBString + "TSPL_USER_MASTER.User_Code " + Location2 + " " + DateFilterShPMNT + ") AS [Tax/RetalInvoice], " & _
                        " (SELECT COUNT(*) FROM " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER WHERE ItemType='E' AND Reference_Document='Load Out/Transfer' AND " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Created_By=" + clsCommon.ReplicateDBString + "TSPL_USER_MASTER.User_Code " + Location3 + " " + DateFilter + ") AS [EmptyAdjustment], " & _
                        " (SELECT COUNT(*) FROM " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD ON " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Number=" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No WHERE " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Created_By=" + clsCommon.ReplicateDBString + "TSPL_USER_MASTER.User_Code " + location1 + " " + DateFilterQckSet + ") AS [QuickSettlement], " & _
                        " (SELECT COUNT(*) FROM " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER WHERE Shipment_Type='Transfer' AND " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Created_By=" + clsCommon.ReplicateDBString + "TSPL_USER_MASTER.User_Code " + Location2 + " " + DateFilterShPMNT + " and Is_Post ='N') AS [UnPostedCashMemo]," & _
                        " (SELECT COUNT(*) FROM " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER WHERE Shipment_Type='Transfer' AND " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Created_By=" + clsCommon.ReplicateDBString + "TSPL_USER_MASTER.User_Code " + Location2 + " " + DateFilterShPMNT + " and Is_Post ='Y') AS [PostedCashMemo] FROM " + clsCommon.ReplicateDBString + "TSPL_USER_MASTER WHERE 1=1 "
            End If

            If chkUserSelect.IsChecked = True Then
                qry += " and  " + clsCommon.ReplicateDBString + "TSPL_USER_MASTER.User_Code in (" + (clsCommon.GetMulcallString(cbgUser.CheckedValue)) + ") "
            End If

            qry += " ) XXX WHERE LoadIn>0 OR LoadIn>0 OR [Tax/RetalInvoice]>0 OR EmptyAdjustment>0 OR QuickSettlement>0 OR  XXX.UnPostedCashMemo>0 or XXX.PostedCashMemo>0  "

            Dim ArrDBName As ArrayList = cbgCompany.CheckedValue

            qry = clsCommon.GetQueryWithAllSelectedDataBase(qry, ArrDBName, False)
            '--------------------------------------------------------------------------

            dt = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data found ")
            Else
                gvReport.DataSource = dt
                FormatGrid()
            End If
            RadPageView1.SelectedPage = RadPageViewPage2


        Catch ex As Exception
            'common.clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub ExportToExcelGV()
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + " ")

            If chkUserSelect.IsChecked Then
                strTemp = ""
                For Each Str As String In cbgUser.CheckedValue
                    If clsCommon.myLen(strTemp) > 0 Then
                        strTemp += ", "
                    End If
                    strTemp += Str
                Next
                arrHeader.Add("User : " + strTemp)
            End If
            If chkLocSelect.IsChecked Then
                strTemp = ""
                For Each Str As String In cbgLocation.CheckedValue
                    If clsCommon.myLen(strTemp) > 0 Then
                        strTemp += ", "
                    End If
                    strTemp += Str
                Next
                arrHeader.Add("Location : " + strTemp)
            End If
            arrHeader.Add("Run Date : " + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy") + " ")
            clsCommon.MyExportToExcel("User Performance Detail ", gvReport, arrHeader, Me.Text)
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            clsCommon.ProgressBarHide()
        End Try
    End Sub

    Private Sub FormatGrid()
        gvReport.AllowAddNewRow = False
        gvReport.TableElement.TableHeaderHeight = 40
        gvReport.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gvReport.Columns.Count - 1
            gvReport.Columns(ii).ReadOnly = True
            gvReport.Columns(ii).IsVisible = False
        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()

        gvReport.Columns("User_Code").IsVisible = True
        gvReport.Columns("User_Code").Width = 101
        gvReport.Columns("User_Code").HeaderText = "User Code"

        gvReport.Columns("User_Name").IsVisible = True
        gvReport.Columns("User_Name").Width = 201
        gvReport.Columns("User_Name").HeaderText = "User Name"

        gvReport.Columns("LoadOut").IsVisible = True
        gvReport.Columns("LoadOut").Width = 71
        gvReport.Columns("LoadOut").HeaderText = "Load Out"

        gvReport.Columns("LoadIn").IsVisible = True
        gvReport.Columns("LoadIn").Width = 71
        gvReport.Columns("LoadIn").HeaderText = "Load In"

        gvReport.Columns("Tax/RetalInvoice").IsVisible = True
        gvReport.Columns("Tax/RetalInvoice").Width = 101
        gvReport.Columns("Tax/RetalInvoice").HeaderText = "Tax/Retal Invoice"

        gvReport.Columns("EmptyAdjustment").IsVisible = True
        gvReport.Columns("EmptyAdjustment").Width = 101
        gvReport.Columns("EmptyAdjustment").HeaderText = "Empty Adjustment"

        gvReport.Columns("QuickSettlement").Width = 101
        gvReport.Columns("QuickSettlement").HeaderText = "Quick Settlement"
        gvReport.Columns("QuickSettlement").IsVisible = True

        gvReport.Columns("UnPostedCashMemo").Width = 81
        gvReport.Columns("UnPostedCashMemo").HeaderText = "Un-Posted Cash Memo"
        gvReport.Columns("UnPostedCashMemo").IsVisible = True


        gvReport.Columns("PostedCashMemo").Width = 81
        gvReport.Columns("PostedCashMemo").HeaderText = "Posted Cash Memo"
        gvReport.Columns("PostedCashMemo").IsVisible = True

    End Sub

    Sub LoadUser()
        Dim strquery As String = "SELECT User_Code AS [Code], User_Name AS [Name] FROM TSPL_USER_MASTER"
        cbgUser.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgUser.ValueMember = "Code"
        cbgUser.DisplayMember = "Name"
    End Sub

    Private Sub chkUserAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkUserAll.ToggleStateChanged
        cbgUser.Enabled = False
    End Sub

    Private Sub chkUserSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkUserSelect.ToggleStateChanged
        cbgUser.Enabled = True
    End Sub

    Sub LoadLocation()
        Dim strquery As String = "SELECT Location_Code AS [Code], Location_Desc AS [Description] FROM TSPL_LOCATION_MASTER where Location_Type='Physical' "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub

    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = False
    End Sub

    Private Sub chkLocSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocSelect.ToggleStateChanged
        cbgLocation.Enabled = True
    End Sub



    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnCashMemoStatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCashMemoStatus.Click

        If cbgCompany.CheckedValue.Count <= 0 Then
            Throw New Exception("Please select at least one Company or select ALL")
            Return
        End If
        Dim Qry As String = ""
        Qry += " select Sale_Invoice_No ,CASE WHEN TSPL_SALE_INVOICE_HEAD.Is_Post = 'Y' THEN 1 ELSE 0 END AS Posted, CASE WHEN TSPL_SALE_INVOICE_HEAD.Is_Post = 'N' THEN 1 ELSE 0 END AS UnPosted, TSPL_LOCATION_MASTER.Location_Desc as [Location],(select SUM(xx.qty) from (select TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No  , case when Unit_code ='FC' then Invoice_Qty  else   Invoice_Qty/(select Conversion_Factor  from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.Item_Code =TSPL_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code ='FB') end as qty  from " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL   where Unit_code <>'SH') as xx where xx.Sale_Invoice_No =TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No " & _
               " ) as qty ,TSPL_TRANSFER_HEAD.Total_Transfer_QtyInCase ,TSPL_TRANSFER_HEAD.Transfer_No   from " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD INNER JOIN " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Shipment_No = " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Shipment_No INNER JOIN " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Transfer_No = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No " & _
               " inner join " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.location=" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code WHERE     TSPL_SALE_INVOICE_HEAD.Shipment_Type = 'Transfer'  " & _
               " and CONVERT(date, TSPL_TRANSFER_HEAD.Transfer_Date,103) >=convert(date,'" + txtFromDate.Value + "',103) and CONVERT(date, TSPL_TRANSFER_HEAD.Transfer_Date,103)<=convert(date,'" + txtToDate.Value + "',103)"

        Dim ArrDBName As ArrayList = cbgCompany.CheckedValue
        Qry = clsCommon.GetQueryWithAllSelectedDataBase(Qry, ArrDBName, False)
        Dim Mainqry As String = "select final.location  as [Location], SUM(final.NoOfCashMamo  ) as [CashMemo],SUM(Posted ) as [Posted],SUM(UnPosted ) as [UnPosted] from  (select COUNT(xxxx.Sale_Invoice_No ) as [NoOfCashMamo],SUM(Posted ) as [Posted],SUM(UnPosted ) as[UnPosted],MAX(Location ) as location,SUM(qty ) as[CashMemoQty],xxxx.Transfer_No as loadout ,case when AVG(xxxx.Total_Transfer_QtyInCase )-SUM(xxxx.qty )=0 then 1*COUNT(xxxx.Sale_Invoice_No ) else 0 end as [Reconciled],case when AVG(xxxx.Total_Transfer_QtyInCase )-SUM(xxxx.qty )=0 then 0 else 1*count(xxxx.Sale_Invoice_No ) end as [To be Reconcile]  from (" + Qry + ""
        Mainqry += " )xxxx group by xxxx.Transfer_No) as final group by location"

        transportSql.ExporttoExcel(Mainqry, Me)

    End Sub

    Private Sub btnRecoStatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRecoStatus.Click

        If cbgCompany.CheckedValue.Count <= 0 Then
            Throw New Exception("Please select at least one Company or select ALL")
            Return
        End If
        Dim Qry As String = ""
        Qry += " select Sale_Invoice_No , TSPL_SALE_INVOICE_HEAD.Location as [Location],(select SUM(xx.qty) from (select TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No  , case when Unit_code ='FC' then Invoice_Qty  else   Invoice_Qty/(select Conversion_Factor  from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.Item_Code =TSPL_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code ='FB') end as qty  from " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL   where Unit_code <>'SH') as xx where xx.Sale_Invoice_No =TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No " & _
               " ) as qty ,TSPL_TRANSFER_HEAD.Total_Transfer_QtyInCase ,TSPL_TRANSFER_HEAD.Transfer_No   from " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD INNER JOIN " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Shipment_No = " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Shipment_No INNER JOIN " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Transfer_No = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No " & _
               " WHERE  TSPL_SALE_INVOICE_HEAD.Is_Post ='N' and    TSPL_SALE_INVOICE_HEAD.Shipment_Type = 'Transfer'  " & _
               " and CONVERT(date, TSPL_TRANSFER_HEAD.Transfer_Date,103) >=convert(date,'" + txtFromDate.Value.Date + "',103) and CONVERT(date, TSPL_TRANSFER_HEAD.Transfer_Date,103)<=convert(date,'" + txtToDate.Value.Date + "',103)"

        Dim ArrDBName As ArrayList = cbgCompany.CheckedValue
        Qry = clsCommon.GetQueryWithAllSelectedDataBase(Qry, ArrDBName, False)
        Dim Mainqry As String = "select final.location  as [Location], SUM(final.NoOfCashMamo  ) as [CashMemo] ,SUM(final.Reconciled ) as [Reconciled] ,sum(final.[To be Reconcile] )as [To be Reconcile] from  (select COUNT(xxxx.Sale_Invoice_No ) as [NoOfCashMamo],MAX(Location ) as location,SUM(qty ) as[CashMemoQty],xxxx.Transfer_No as loadout ,case when convert(decimal,AVG(xxxx.Total_Transfer_QtyInCase )-(avg(TSPL_TRANSFER_HEAD.Total_Transfer_QtyInCase)+SUM(xxxx.qty )),2)=0 then 1*COUNT(xxxx.Sale_Invoice_No ) else 0 end as [Reconciled],case when convert(decimal,AVG(xxxx.Total_Transfer_QtyInCase )-(avg(TSPL_TRANSFER_HEAD.Total_Transfer_QtyInCase)+SUM(xxxx.qty )),2)=0 then 0 else 1*count(xxxx.Sale_Invoice_No ) end as [To be Reconcile]  from (" + Qry + ""
        Mainqry += " )xxxx inner join TSPL_TRANSFER_HEAD on xxxx.Transfer_No =TSPL_TRANSFER_HEAD.Load_Out_No  group by xxxx.Transfer_No) as final group by location"

        transportSql.ExporttoExcel(Mainqry, Me)

    End Sub
End Class
