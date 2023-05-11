'--Created By --[Panch Raj]--Aaagainst Ticket No-[BM00000002087]
'---------Anubhooti 01/07/2014 added location filter and changed customer filter aganist BM00000003006------------------------------
'' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
Imports common
Imports System.Data.SqlClient
Imports System.IO

Public Class frmReceiptChallanReport
    Inherits FrmMainTranScreen
    'Dim Qry As String
    'Dim dt As DataTable
    Dim ButtonToolTip As ToolTip = New ToolTip()
    'Dim ArrDb As New List(Of String)
    Const colInvoiceNo As String = "colInvoiceNo"
    Const colInvoiceDate As String = "colInvoiceDate"
    Const colBillToLocation As String = "colBillToLocation"
    Const colBillToLocationDesc As String = "colBillToLocationDesc"
    Const colInvoiceAmount As String = "colInvoiceAmount"
    Const colCustCode As String = "colCustCode"
    Const colCustName As String = "colCustName"
    Const colSalesmanCode As String = "colSalesmanCode"
    Const colSalesmanName As String = "colSalesmanName"

    Const colVehicleIn As String = "colVehicleIn"
    Const colReceiptIn As String = "colReceiptIn"
    Const colRemarks As String = "colRemarks"
    Const colComments As String = "colComments"
    Const colTransferHO As String = "colTransferHO"
    Dim ReportID As String = "Daily Receipt"
    Dim IsInsideLoadData As Boolean = True
    Dim dt1 As DataTable

    Private Sub frmReceiptChallan_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        'If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
        '    SaveData()
        'Else
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso MyBase.isPostFlag Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            Me.Close()
        End If
    End Sub


    Private Sub frmReceiptChallan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Reset()
        RadPageView1.SelectedPage = RadPageViewPage1
        LoadCustomer()
        'cbgCustomer.CheckedAll()
        LoadLocation()
        'cbgLocation.CheckedAll()
        rbtncustall.IsChecked = True
        rbtnlocall.IsChecked = True
        ' ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+N Adding New Trasnaction")
        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+N Reset Trasnaction")
        btnSave.Visible = False

    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmReceiptChallanReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        '' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
        btnExport.Visible = MyBase.isExport

        'btnSave.Visible = MyBase.isModifyFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub LoadBlankGrid()
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        gv1.DataSource = dt1
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).Width = 70
            gv1.Columns(ii).IsVisible = True
        Next

        gv1.EnableFiltering = True
        gv1.AllowDeleteRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AllowAddNewRow = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
    End Sub

    Private Sub Reset()
        Dim serverdate As Date = clsCommon.GETSERVERDATE
        Me.dtpFrom.Value = serverdate
        Me.dtpTo.Value = serverdate
        Me.fndVehicleCode.Value = Nothing
        ' Me.fndCustCode.Value = Nothing

        'LoadBlankGrid()
    End Sub

    ''Anubhooti 30-June-2014
    Sub LoadCustomer()
        'Dim qry As String = "select Cust_Code as Code,Customer_Name as Name,TSPL_CUSTOMER_MASTER.add1 +case when len(TSPL_CUSTOMER_MASTER.add2)>0 then ', '+TSPL_CUSTOMER_MASTER.add2 else '' end +case when LEN(isnull(TSPL_CUSTOMER_MASTER.Add3,''))>0 then ', '+isnull(TSPL_CUSTOMER_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER.City_Name)>0 then ', '+TSPL_CITY_MASTER.City_Name else ' ' end + case when len(TSPL_CUSTOMER_MASTER.State )>0 then TSPL_CUSTOMER_MASTER.State else '' end  as Address,TSPL_CUSTOMER_MASTER.Terms_Code as [Term Code] , TSPL_TERMS_MASTER.Terms_Desc as [Term Description] ,Tax_Group as [Tax Group],Tax_Group_Desc as [Tax Group Description],Salesman_Code as [Salesman Code],Salesman_Desc as Salesman "
        'qry += " from TSPL_CUSTOMER_MASTER "
        'qry += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_CUSTOMER_MASTER.City_Code"
        'qry += " left outer join TSPL_TDS_STATE_MASTER on TSPL_TDS_STATE_MASTER.State_Code=TSPL_CUSTOMER_MASTER.State"
        'qry += " left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_CUSTOMER_MASTER.Terms_Code"
        'qry += " left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_CUSTOMER_MASTER.Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S'"
        Dim qry As String = "Select Cust_Code As Code,Customer_Name As Name From TSPL_CUSTOMER_MASTER"
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCustomer.ValueMember = "Code"
        cbgCustomer.DisplayMember = "Name"
    End Sub
    Sub LoadLocation()
        Dim qry As String = " Select Location_Code As Code ,Location_Desc as Name From TSPL_Location_Master "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub
    ''

    'Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
    '    Try
    '        If SaveData() Then
    '            RadMessageBox.Show("Data Saved Successfully")
    '            Reset()
    '        End If
    '    Catch ex As Exception
    '        RadMessageBox.Show(ex.Message)
    '    End Try
    'End Sub

    'Private Function SaveData() As Boolean
    '    Try
    '        If AllowToSave() Then
    '            Dim Arr As New List(Of clsReceiptChallan)
    '            For Each grow As GridViewRowInfo In gv1.Rows
    '                Dim objTr As New clsReceiptChallan()
    '                objTr.SALE_INVOICE_NO = clsCommon.myCstr(grow.Cells(colInvoiceNo).Value)
    '                objTr.VEHICLE_IN = IIf(clsCommon.myCBool(grow.Cells(colVehicleIn).Value) = True, "Y", "N")
    '                objTr.RECEIPT_IN = IIf(clsCommon.myCBool(grow.Cells(colReceiptIn).Value) = True, "Y", "N")
    '                objTr.TRANSFER_HO = IIf(clsCommon.myCBool(grow.Cells(colTransferHO).Value) = True, "Y", "N")
    '                objTr.REMARKS = clsCommon.myCstr(grow.Cells(colRemarks).Value)
    '                objTr.COMMENTS = clsCommon.myCstr(grow.Cells(colComments).Value)
    '                Arr.Add(objTr)
    '            Next
    '            If clsReceiptChallan.SaveData(Arr) Then
    '                Return True
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    '    Return True
    'End Function

    'Private Function AllowToSave() As Boolean
    '    Return True
    'End Function


    Private Sub LoadDetails()
        Try
            IsInsideLoadData = True
            'LoadBlankGrid()

            '-----------------------Anubhooti 01/07/2014------------------------------
            If rbtncustslct.IsChecked AndAlso cbgCustomer.CheckedValue.Count = 0 Then
                clsCommon.MyMessageBoxShow("Please Select Atleast One Customer Code", Me.Text)
                Return
            End If

            If rbtnlocslct.IsChecked AndAlso cbgLocation.CheckedValue.Count = 0 Then
                clsCommon.MyMessageBoxShow("Please Select Atleast One Location Code", Me.Text)
                Return
            End If
            '' Anubhooti 30-Sep-2014 (Fetch GRNo,GRDate From TSPL_RECEIPT_CHALLAN)
            Dim qry As String

            qry = "select Document_Code as [Invoice No],convert(date,Document_Date,105) as [Invoice Date],Bill_To_Location as [Bill To Location],TSPL_LOCATION_MASTER.Location_Desc as [Location Description],TSPL_SD_SALE_INVOICE_HEAD.Total_Amt as [Total Amount]," & _
                  " Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],TSPL_SD_SALE_INVOICE_HEAD.Salesman_Code as [Salesman Code], " & _
                  " TSPL_SD_SALE_INVOICE_HEAD.Salesman_Name as [Salesman Name],TSPL_SD_SALE_INVOICE_HEAD.VEHICLE_IN as [Vehicle In], " & _
                  " TSPL_SD_SALE_INVOICE_HEAD.RECEIPT_IN  as [Receipt In],TSPL_SD_SALE_INVOICE_HEAD.TRANSFER_HO as [Transfer To HO],TSPL_RECEIPT_CHALLAN.Remarks,TSPL_RECEIPT_CHALLAN.Comments,ISNULL(TSPL_RECEIPT_CHALLAN.GRNO,'') AS [GR No],TSPL_RECEIPT_CHALLAN.GRDate AS [GR Date] from TSPL_SD_SALE_INVOICE_HEAD inner join TSPL_CUSTOMER_MASTER " & _
                  " on TSPL_SD_SALE_INVOICE_HEAD.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code " & _
                  " left join TSPL_LOCATION_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location=TSPL_LOCATION_MASTER.Location_Code " & _
                  " left outer join TSPL_RECEIPT_CHALLAN on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_RECEIPT_CHALLAN.SALE_INVOICE_NO and TSPL_SD_SALE_INVOICE_HEAD.VEHICLE_IN=TSPL_RECEIPT_CHALLAN.VEHICLE_IN and TSPL_SD_SALE_INVOICE_HEAD.TRANSFER_HO=TSPL_RECEIPT_CHALLAN.TRANSFER_HO and TSPL_RECEIPT_CHALLAN.RECEIPT_IN=TSPL_SD_SALE_INVOICE_HEAD.RECEIPT_IN  " & _
                  " where (TSPL_SD_SALE_INVOICE_HEAD.VEHICLE_IN ='Y' or TSPL_SD_SALE_INVOICE_HEAD.RECEIPT_IN ='Y' or TSPL_SD_SALE_INVOICE_HEAD.TRANSFER_HO='Y') " & _
                  " and TSPL_SD_SALE_INVOICE_HEAD.Document_Date between '" & clsCommon.GetPrintDate(Me.dtpFrom.Value, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(Me.dtpTo.Value.AddDays(1), "dd/MMM/yyyy") & "'"
            If clsCommon.myLen(fndVehicleCode.Value) > 0 Then
                qry = qry & " and TSPL_SD_SALE_INVOICE_HEAD.VehicleNo='" & clsCommon.myCstr(fndVehicleCode.Value) & "'"
            End If

            'If clsCommon.myLen(fndCustCode.Value) > 0 Then
            '    qry = qry & " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code='" & clsCommon.myCstr(fndCustCode.Value) & "'"
            'End If
            '' Anubhooti 30-June-2014
            If rbtncustslct.IsChecked AndAlso cbgCustomer.CheckedValue.Count > 0 Then
                qry = qry & "  and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (" & clsCommon.GetMulcallString(cbgCustomer.CheckedValue) & ")"
            End If
            If rbtnlocslct.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                qry = qry & "  and TSPL_LOCATION_MASTER.Location_Code in (" & clsCommon.GetMulcallString(cbgLocation.CheckedValue) & ")"
            End If

            'If cbgCustomer.CheckedValue.Count > 0 Then
            '    qry = qry & " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (" & clsCommon.GetMulcallString(cbgCustomer.CheckedValue) & ")"
            'End If
            'If cbgLocation.CheckedValue.Count > 0 Then
            '    qry = qry & " and TSPL_LOCATION_MASTER.Location_Code in (" & clsCommon.GetMulcallString(cbgLocation.CheckedValue) & ")"
            'End If
            ''

            dt1 = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = dt1
            RadPageView1.SelectedPage = RadPageViewPage2
            LoadBlankGrid()
            'ReStoreGridLayout()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            IsInsideLoadData = False
        End Try
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        'Reset()
        LoadDetails()

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    'Private Sub fndCustCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean)
    '    Dim qry As String = "select Cust_Code as Code,Customer_Name as Name,TSPL_CUSTOMER_MASTER.add1 +case when len(TSPL_CUSTOMER_MASTER.add2)>0 then ', '+TSPL_CUSTOMER_MASTER.add2 else '' end +case when LEN(isnull(TSPL_CUSTOMER_MASTER.Add3,''))>0 then ', '+isnull(TSPL_CUSTOMER_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER.City_Name)>0 then ', '+TSPL_CITY_MASTER.City_Name else ' ' end + case when len(TSPL_CUSTOMER_MASTER.State )>0 then TSPL_CUSTOMER_MASTER.State else '' end  as Address,TSPL_CUSTOMER_MASTER.Terms_Code as [Term Code] , TSPL_TERMS_MASTER.Terms_Desc as [Term Description] ,Tax_Group as [Tax Group],Tax_Group_Desc as [Tax Group Description],Salesman_Code as [Salesman Code],Salesman_Desc as Salesman "
    '    qry += " from TSPL_CUSTOMER_MASTER "
    '    qry += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_CUSTOMER_MASTER.City_Code"
    '    qry += " left outer join TSPL_TDS_STATE_MASTER on TSPL_TDS_STATE_MASTER.State_Code=TSPL_CUSTOMER_MASTER.State"
    '    qry += " left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_CUSTOMER_MASTER.Terms_Code"
    '    qry += " left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_CUSTOMER_MASTER.Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S'"
    '    fndCustCode.Value = clsCommon.ShowSelectForm("SNSOVendorFndr", qry, "Code", "", fndCustCode.Value, "Code", isButtonClicked)
    '    If clsCommon.myLen(fndCustCode.Value) > 0 Then
    '        lblCustName.Text = clsCustomerMasterNew.GetData(fndCustCode.Value, Nothing).Customer_Name
    '    End If

    'End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        If clsCommon.myLen(ReportID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub RadMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem3.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
    End Sub

    Private Sub Export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export.Click
        If (gv1.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow("No Data To Export")
            Exit Sub
        End If
        ExportToExcel(Exporter.Excel)
    End Sub

    Private Sub PDF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PDF.Click
        If (gv1.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow("No Data To Export")
            Exit Sub
        End If
        ExportToExcel(Exporter.PDF)
    End Sub
    Enum Exporter
        Excel = 0
        PDF = 1
        Print = 2
        Refresh = 3
    End Enum
    Sub ExportToExcel(ByVal IsPrint As Exporter)
        Dim arrHeader As List(Of String) = New List(Of String)()
        Dim strTemp As String = ""
        arrHeader.Add("From Date : " + clsCommon.GetPrintDate(dtpFrom.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpTo.Value, "dd/MM/yyyy") + " ")
        'arrHeader.Add("Customer : " + fndCustCode.Value)


        If IsPrint = Exporter.Excel Then
            clsCommon.MyExportToExcelGrid("Daily Receipt", gv1, arrHeader, Me.Text)
        ElseIf IsPrint = Exporter.PDF Then
            clsCommon.MyExportToPDF("Daily Receipt", gv1, arrHeader, Me.Text, True)
        End If
    End Sub


    Private Sub rbtncustslct_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtncustslct.ToggleStateChanged
        cbgCustomer.Enabled = rbtncustslct.IsChecked
    End Sub
    Private Sub rbtncustall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtncustall.ToggleStateChanged
        cbgCustomer.Enabled = rbtncustslct.IsChecked
    End Sub

    Private Sub rbtnlocall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnlocall.ToggleStateChanged
        cbgLocation.Enabled = rbtnlocslct.IsChecked
    End Sub
    Private Sub rbtnlocslct_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnlocslct.ToggleStateChanged
        cbgLocation.Enabled = rbtncustslct.IsChecked
    End Sub
End Class
