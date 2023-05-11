'--------------------------------Last modify By - Dipti ------------------------------------
'--------------------------------Last modify date - 31/01/2013-------------------------------------
'---------At the form load only that company will keep checked by whom user get logged in------

Imports System.Data.SqlClient
Imports common
Imports XpertERPEngine
Imports Telerik.WinControls
Imports Telerik.WinControls.UI

Public Class FrmMismatchCashMemo
    Inherits FrmMainTranScreen
    Dim dt As DataTable

    Private Sub FrmMismatchCashMemo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadCompany()
        LoadLocation()
        Reset()
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmMismatchCashMemo)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Private Sub Reset()
        txtFirst.Text = ""
        txtSecond.Text = ""
        txtFromDate.Value = clsCommon.GETSERVERDATE
        txtToDate.Value = clsCommon.GETSERVERDATE
        chkLocAll.IsChecked = True
        rdbBoth.IsChecked = True
        rdbmatchAll.IsChecked = True
        gvReport.GroupDescriptors.Clear()
        gvReport.MasterTemplate.SummaryRowsBottom.Clear()
        dt = Nothing
        gvReport.DataSource = Nothing
        gvReport.Columns.Clear()
        gvReport.Rows.Clear()
        chkFromShipment.Checked = False
        'rbtnCompanySelect.IsChecked = True
        Dim arrDBName As New ArrayList()
        arrDBName.Add(objCommonVar.CurrDatabase)
        cbgCompany.CheckedValue = arrDBName


    End Sub


    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnExportToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportToExcel.Click
        ExportToExcel()
    End Sub

    Private Sub ExportToExcel()
        'Try
        '    RefreshData()
        '    If gvReport.DataSource Is Nothing OrElse gvReport.Rows.Count <= 0 Then
        '        Throw New Exception("No Data found to Export")
        '    End If
        '    ExportToExcelGV()
        'Catch ex As Exception
        '    common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
        'End Try
    End Sub

    Private Sub RefreshData()
        'Try

        '    If cbgCompany.CheckedValue.Count <= 0 Then
        '        Throw New Exception("Please select at least one Company or select ALL")
        '        Return
        '    End If

        '    If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
        '        Throw New Exception("Please Select at least Single location or Select All")
        '        Return
        '    End If

        '    If clsCommon.myCdbl(txtFirst.Text) > clsCommon.myCdbl(txtSecond.Text) And clsCommon.myLen(txtSecond.Text) > 0 Then
        '        txtFirst.Focus()
        '        Throw New Exception("Value Of First Text Can Not be Greater Than Text Second")
        '    End If

        '    Dim qry As String = ""

        '    qry = " SELECT " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Cust_Name ," + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Transfer_No AS LoadOutNo, CONVERT(VARCHAR, " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Transfer_Date, 103) AS LoadOutDate, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No," & _
        '        " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_Desc, " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS Salesman, " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Customer_Invoice_No AS MannualMemoNo, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No, " & _
        '        " CONVERT(DECIMAL(18, 2), ISNULL(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Mannual_Invoice_Amt, 0)) as Mannual_Invoice_Amt, Convert(DECIMAL(18,2)," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Total_Invoice_Amt) as Total_Invoice_Amt, " & _
        '        " (CONVERT(DECIMAL(18,2),ISNULL(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Total_Invoice_Amt, 0))-CONVERT(DECIMAL(18,2),ISNULL(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Mannual_Invoice_Amt, 0))) AS Difference, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Remarks AS [Remarks], Case WHEN " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Is_Post='Y' Then 'Posted' Else 'Pending' End as Status " & _
        '        " FROM " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD " & _
        '        " LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Shipment_No=" + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Shipment_No " & _
        '        " LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code=" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE WHERE " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Shipment_Type = 'Transfer' " & _
        '        " AND CONVERT(DATE, TSPL_SHIPMENT_MASTER.Transfer_Date,103)>=CONVERT(DATE, '" + txtFromDate.Value.Date + "', 103) AND CONVERT(DATE, TSPL_SHIPMENT_MASTER.Transfer_Date,103)<=CONVERT(DATE, '" + txtToDate.Value.Date + "', 103) "

        '    If chkLocSelect.IsChecked Then
        '        qry += " AND " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location IN (" + (clsCommon.GetMulcallString(cbgLocation.CheckedValue)) + ") "
        '    End If

        '    Dim ArrDBName As ArrayList = cbgCompany.CheckedValue

        '    qry = clsCommon.GetQueryWithAllSelectedDataBase(qry, ArrDBName, False)
        '    '--------------------------------------------------------------------------
        '    qry += " WHERE 1=1"
        '    If rdbZero.IsChecked = True Then
        '        qry += " and ISNULL(Difference, 0)=0"
        '    ElseIf rdbNonZero.IsChecked = True Then
        '        qry += " and ISNULL(Difference, 0)<>0"
        '    Else

        '    End If

        '    ' qry += " WHERE ISNULL(Difference, 0)>0"
        '    Dim T1 As String = clsCommon.myCstr(clsCommon.myCdbl(txtFirst.Text))
        '    Dim T2 As String = clsCommon.myCstr(clsCommon.myCdbl(txtSecond.Text))
        '    If clsCommon.myCdbl(txtFirst.Text) > 0 AndAlso clsCommon.myCdbl(txtSecond.Text) = 0 Then
        '        qry += " AND ISNULL(Difference, 0)>= " + T1 + ""
        '    ElseIf clsCommon.myCdbl(txtSecond.Text) > 0 AndAlso clsCommon.myCdbl(txtFirst.Text) = 0 Then
        '        qry += " AND ISNULL(Difference, 0)<= " + T2 + ""
        '    ElseIf clsCommon.myCdbl(txtFirst.Text) > 0 AndAlso clsCommon.myCdbl(txtSecond.Text) > 0 Then
        '        qry += "AND ISNULL(Difference, 0)>= " + T1 + " AND ISNULL(Difference, 0)<= " + T2 + ""
        '    End If

        '    If rdbPending.IsChecked = True Then
        '        qry += " and xxx.Status ='Pending'"
        '    End If
        '    If rdbPosted.IsChecked = True Then
        '        qry += " and xxx.Status ='Posted'"
        '    End If



        '    dt = clsDBFuncationality.GetDataTable(qry)
        '    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
        '        Throw New Exception("No Data found ")
        '    Else
        '        gvReport.DataSource = dt
        '        FormatGrid()
        '    End If
        '    RadPageView1.SelectedPage = RadPageViewPage2


        'Catch ex As Exception
        '    'common.clsCommon.MyMessageBoxShow(ex.Message)
        '    Throw New Exception(ex.Message)
        'End Try
        Try

            If cbgCompany.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one Company or select ALL")
                Return
            End If

            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                Throw New Exception("Please Select at least Single location or Select All")
                Return
            End If

            If clsCommon.myCdbl(txtFirst.Text) > clsCommon.myCdbl(txtSecond.Text) And clsCommon.myLen(txtSecond.Text) > 0 Then
                txtFirst.Focus()
                Throw New Exception("Value Of First Text Can Not be Greater Than Text Second")
            End If

            Dim qry As String = ""
            If chkFromShipment.Checked = True Then
                qry = "SELECT " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Cust_Name ," + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Transfer_No AS LoadOutNo, CONVERT(VARCHAR, " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Transfer_Date, 103) AS LoadOutDate, "
                qry += " " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Route_No, " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Route_Desc, " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS Salesman, "
                qry += " " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Customer_Invoice_No AS MannualMemoNo, " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Customer_Invoice_No  as Sale_Invoice_No,  "
                qry += " CONVERT(DECIMAL(18, 2), ISNULL(" + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Mannual_Invoice_Amt, 0)) as Mannual_Invoice_Amt, "
                qry += " Convert(DECIMAL(18,2)," + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Total_Order_Amt) as Total_Invoice_Amt,  "
                qry += " (CONVERT(DECIMAL(18,2),ISNULL(" + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Total_Order_Amt , 0))-CONVERT(DECIMAL(18,2),ISNULL(" + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Mannual_Invoice_Amt, 0))) AS Difference, "
                qry += " " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Remarks AS [Remarks], "
                qry += " Case WHEN " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Is_Post='Y' Then 'Posted' Else 'Pending' End as Status  "
                qry += " FROM  " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER  "
                qry += " LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Salesman_Code=" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE WHERE " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Shipment_Type = 'Transfer'  AND CONVERT(DATE, " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Transfer_Date,103)>=CONVERT(DATE, '" + txtFromDate.Value.Date + "', 103) AND CONVERT(DATE, " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Transfer_Date,103)<=CONVERT(DATE, '" + txtToDate.Value.Date + "', 103)"
                If chkLocSelect.IsChecked Then
                    qry += " AND " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Location IN (" + (clsCommon.GetMulcallString(cbgLocation.CheckedValue)) + ") "
                End If

            Else

                qry = " SELECT " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Cust_Name ," + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Transfer_No AS LoadOutNo, CONVERT(VARCHAR, " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Transfer_Date, 103) AS LoadOutDate, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No," & _
                    " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_Desc, " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS Salesman, " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Customer_Invoice_No AS MannualMemoNo, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No, " & _
                    " CONVERT(DECIMAL(18, 2), ISNULL(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Mannual_Invoice_Amt, 0)) as Mannual_Invoice_Amt, Convert(DECIMAL(18,2)," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Total_Invoice_Amt) as Total_Invoice_Amt, " & _
                    " (CONVERT(DECIMAL(18,2),ISNULL(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Total_Invoice_Amt, 0))-CONVERT(DECIMAL(18,2),ISNULL(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Mannual_Invoice_Amt, 0))) AS Difference, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Remarks AS [Remarks], Case WHEN " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Is_Post='Y' Then 'Posted' Else 'Pending' End as Status " & _
                    " FROM " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD " & _
                    " LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Shipment_No=" + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Shipment_No " & _
                    " LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code=" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE WHERE " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Shipment_Type = 'Transfer' " & _
                    " AND CONVERT(DATE, TSPL_SHIPMENT_MASTER.Transfer_Date,103)>=CONVERT(DATE, '" + txtFromDate.Value.Date + "', 103) AND CONVERT(DATE, TSPL_SHIPMENT_MASTER.Transfer_Date,103)<=CONVERT(DATE, '" + txtToDate.Value.Date + "', 103) "

                If chkLocSelect.IsChecked Then
                    qry += " AND " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location IN (" + (clsCommon.GetMulcallString(cbgLocation.CheckedValue)) + ") "
                End If

            End If

            Dim ArrDBName As ArrayList = cbgCompany.CheckedValue

            qry = clsCommon.GetQueryWithAllSelectedDataBase(qry, ArrDBName, False)
            '--------------------------------------------------------------------------
            qry += " WHERE 1=1"
            If rdbZero.IsChecked = True Then
                qry += " and ISNULL(Difference, 0)=0"
            ElseIf rdbNonZero.IsChecked = True Then
                qry += " and ISNULL(Difference, 0)<>0"
            Else

            End If

            ' qry += " WHERE ISNULL(Difference, 0)>0"
            Dim T1 As String = clsCommon.myCstr(clsCommon.myCdbl(txtFirst.Text))
            Dim T2 As String = clsCommon.myCstr(clsCommon.myCdbl(txtSecond.Text))
            If clsCommon.myCdbl(txtFirst.Text) > 0 AndAlso clsCommon.myCdbl(txtSecond.Text) = 0 Then
                qry += " AND ISNULL(Difference, 0)>= " + T1 + ""
            ElseIf clsCommon.myCdbl(txtSecond.Text) > 0 AndAlso clsCommon.myCdbl(txtFirst.Text) = 0 Then
                qry += " AND ISNULL(Difference, 0)<= " + T2 + ""
            ElseIf clsCommon.myCdbl(txtFirst.Text) > 0 AndAlso clsCommon.myCdbl(txtSecond.Text) > 0 Then
                qry += "AND ISNULL(Difference, 0)>= " + T1 + " AND ISNULL(Difference, 0)<= " + T2 + ""
            End If

            If rdbPending.IsChecked = True Then
                qry += " and xxx.Status ='Pending'"
            End If
            If rdbPosted.IsChecked = True Then
                qry += " and xxx.Status ='Posted'"
            End If



            dt = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data found ")
            Else
                gvReport.DataSource = dt
                FormatGrid()
            End If
            RadPageView1.SelectedPage = RadPageViewPage2


        Catch ex As Exception
            'common.clsCommon.MyMessageBoxShow(ex.Message)
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub ExportToExcelGV(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + " ")

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
            'clsCommon.MyExportToExcel("Mismatched Cash Memo ", gvReport, arrHeader, Me.Text)

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Mismatched Cash Memo ", gvReport, arrHeader, Me.Text)

            Else
                clsCommon.MyExportToPDF("Mismatched Cash Memo ", gvReport, arrHeader, "Mismatched Cash Memo", True)
            End If

        Catch ex As Exception
            clsCommon.ProgressBarHide()
            common.clsCommon.MyMessageBoxShow(ex.Message)
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

        gvReport.Columns("Cust_Name").IsVisible = True
        gvReport.Columns("Cust_Name").Width = 181
        gvReport.Columns("Cust_Name").HeaderText = "Customer"

        gvReport.Columns("LoadOutNo").IsVisible = True
        gvReport.Columns("LoadOutNo").Width = 101
        gvReport.Columns("LoadOutNo").HeaderText = "LoadOut No"

        gvReport.Columns("LoadOutDate").IsVisible = True
        gvReport.Columns("LoadOutDate").Width = 101
        gvReport.Columns("LoadOutDate").FormatString = "{0:d}"
        gvReport.Columns("LoadOutDate").HeaderText = "LoadOut Date"

        gvReport.Columns("Route_No").IsVisible = True
        gvReport.Columns("Route_No").Width = 71
        gvReport.Columns("Route_No").HeaderText = "Route No"

        gvReport.Columns("Route_Desc").IsVisible = True
        gvReport.Columns("Route_Desc").Width = 201
        gvReport.Columns("Route_Desc").HeaderText = "Route Name"

        gvReport.Columns("Salesman").IsVisible = True
        gvReport.Columns("Salesman").Width = 201
        gvReport.Columns("Salesman").HeaderText = "Salesman"

        gvReport.Columns("MannualMemoNo").IsVisible = True
        gvReport.Columns("MannualMemoNo").Width = 101
        gvReport.Columns("MannualMemoNo").HeaderText = "Mannual Memo No"

        gvReport.Columns("Sale_Invoice_No").Width = 101
        gvReport.Columns("Sale_Invoice_No").HeaderText = "Memo No"
        gvReport.Columns("Sale_Invoice_No").IsVisible = True

        gvReport.Columns("Mannual_Invoice_Amt").Width = 151
        gvReport.Columns("Mannual_Invoice_Amt").HeaderText = "Mannual Cash Memo Amt"
        gvReport.Columns("Mannual_Invoice_Amt").IsVisible = True

        gvReport.Columns("Total_Invoice_Amt").Width = 151
        gvReport.Columns("Total_Invoice_Amt").HeaderText = "Cash Memo Amt"
        gvReport.Columns("Total_Invoice_Amt").IsVisible = True

        gvReport.Columns("Difference").Width = 151
        gvReport.Columns("Difference").HeaderText = "Difference"
        gvReport.Columns("Difference").IsVisible = True

        gvReport.Columns("Remarks").Width = 201
        gvReport.Columns("Remarks").HeaderText = "Remarks"
        gvReport.Columns("Remarks").IsVisible = True

        gvReport.Columns("Status").Width = 71
        gvReport.Columns("Status").IsVisible = True

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

    Sub LoadCompany()
        Dim qry As String = "SELECT Comp_Code as [Company Code],Comp_Name as [Company Name],DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0"
        Dim ArrHideColumn As New List(Of String)
        ArrHideColumn.Add("DataBase_Name")
        cbgCompany.ArrHideColumns = ArrHideColumn
        cbgCompany.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCompany.ValueMember = "DataBase_Name"
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Try
            RefreshData()
            If gvReport.DataSource Is Nothing OrElse gvReport.Rows.Count <= 0 Then
                Throw New Exception("No Data found to Export")
            End If
            ExportToExcelGV(EnumExportTo.Excel)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub btnPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        Try
            RefreshData()
            If gvReport.DataSource Is Nothing OrElse gvReport.Rows.Count <= 0 Then
                Throw New Exception("No Data found to Export")
            End If
            ExportToExcelGV(EnumExportTo.PDF)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
End Class
