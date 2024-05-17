Imports common
Public Class RptZoneWiseFreshSaleReport
    Inherits FrmMainTranScreen
    Dim strQry As String = ""
    Dim btnReferesh As Boolean = False





    Private Sub txtCustomerGroup__My_Click(sender As Object, e As EventArgs) Handles txtCustomerGroup._My_Click
        strQry = "Select Cust_Group_Code as Code, Cust_Group_Desc as Name from TSPL_CUSTOMER_GROUP_MASTER"
        txtCustomerGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtCustomerGroup.arrValueMember, txtCustomerGroup.arrDispalyMember)
    End Sub

    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        strQry = " select Cust_Code as [code],Customer_Name as [Name] from TSPL_CUSTOMER_MASTER"
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
    End Sub

    Private Sub txtItemCode__My_Click(sender As Object, e As EventArgs) Handles txtItemCode._My_Click
        strQry = " select distinct TSPL_ITEM_MASTER.Item_Code As Code,  TSPL_ITEM_MASTER.Item_Desc as Name From TSPL_SD_SALE_INVOICE_DETAIL Left Outer Join TSPL_SD_SALE_INVOICE_HEAD On TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_SD_SALE_INVOICE_DETAIL.Location Where TSPL_SD_SALE_INVOICE_HEAD.Trans_Type = 'FS' "
        'strQry = "Select TSPL_ITEM_MASTER.Item_Code As Code,  TSPL_ITEM_MASTER.Item_Desc As Name From TSPL_ITEM_MASTER "
        txtItemCode.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtItemCode.arrValueMember, txtItemCode.arrDispalyMember)
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        strQry = "select Location_Code  as [Code],Location_Desc as [Name] from TSPL_LOCATION_MASTER"
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        FrmPendingRequisitionQty.SetDiplayMember(txtLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub

    Private Sub txtZone__My_Click(sender As Object, e As EventArgs) Handles txtZone._My_Click
        strQry = "Select TSPL_ZONE_MASTER.Zone_Code As Code,  TSPL_ZONE_MASTER.Description As Name From TSPL_ZONE_MASTER "
        txtZone.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtZone.arrValueMember, txtZone.arrDispalyMember)
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        Print(Exporter.Refresh)
    End Sub
    Private Sub Print(ByVal IsPrint As Exporter)
        Try
            If fromDate.Value > ToDate.Value Then
                common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater then to Date", Me.Text)
                fromDate.Focus()
                Exit Sub
            End If

            Gv1.MasterTemplate.SummaryRowsBottom.Clear()

            Dim MainQuery As String = String.Empty
            Dim strWhrClause As String = String.Empty
            Dim itemCode As String = String.Empty
            itemCode = " and 2=2 "

            strWhrClause = " ''FS'' and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= ''" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "'' and  convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= ''" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'' "
            If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtCustomerGroup.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + sss + ")  "
            End If

            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtCustomer.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (" + sss + ")  "

            End If

            If txtItemCode.arrValueMember IsNot Nothing AndAlso txtItemCode.arrValueMember.Count > 0 Then

                itemCode = itemCode + " and TSPL_ITEM_MASTER.Item_Code in (" + clsCommon.GetMulcallString(txtItemCode.arrValueMember) + ")  "
            End If

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtLocation.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_SD_SALE_INVOICE_DETAIL.Location_Code in (" + sss + ")  "
            End If

            If txtZone.arrValueMember IsNot Nothing AndAlso txtZone.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtZone.arrValueMember)
                Dim sss As String = ss.Replace("'", "''")
                strWhrClause += " and TSPL_CUSTOMER_MASTER.Zone_Code in (" + sss + ")  "
            End If



            MainQuery = "DECLARE @cols AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT @cols = STUFF((SELECT distinct ',' + QUOTENAME(Short_Description) FROM TSPL_ITEM_MASTER where (Short_Description !='' or Short_Description is null)  " + itemCode + "  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') SELECT  'select * from (select  zzz.Zone,zzz.[Group],zzz.[Cust Group Desc],zzz.Item,isnull(zzz.Qty,0) as Qty  from (Select Max(TSPL_ITEM_MASTER.Sku_Seq) As Sequence, Max(TSPL_SD_SALE_INVOICE_HEAD.Document_Date) As Document_Date, Max(TSPL_SD_SALE_INVOICE_HEAD.Document_Date) As Date, Max(TSPL_CUSTOMER_MASTER.Zone_Code) As Zone,Max(TSPL_CUSTOMER_MASTER.Cust_Group_Code) as [Group],max(TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc) as [Cust Group Desc],Max(TSPL_ITEM_MASTER.Short_Description) As Item, Sum(TSPL_SD_SALE_INVOICE_DETAIL.Qty) As Qty, TSPL_SD_SALE_INVOICE_HEAD.Customer_Code As Customer, Max(TSPL_CUSTOMER_MASTER.Customer_Name) As [Customer Name], TSPL_SD_SALE_INVOICE_DETAIL.Item_Code As [Item Code], Max(TSPL_ITEM_MASTER.Item_Desc) As [Item Name], Max(TSPL_SD_SALE_INVOICE_DETAIL.Location) As Location, Max(TSPL_LOCATION_MASTER.Location_Desc) As [Location Name] From TSPL_SD_SALE_INVOICE_DETAIL Left Outer Join TSPL_SD_SALE_INVOICE_HEAD On TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_SD_SALE_INVOICE_DETAIL.Location left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code  Where TSPL_SD_SALE_INVOICE_HEAD.Trans_Type =  " + strWhrClause + " Group By TSPL_SD_SALE_INVOICE_HEAD.Customer_Code, TSPL_SD_SALE_INVOICE_DETAIL.Item_Code)zzz ) as s pivot (   sum(Qty) for Item in (' + @cols + ') ) as zpivot"
            MainQuery = MainQuery + "'"
            Dim query As String = ""
            query = clsDBFuncationality.getSingleValue(MainQuery)
            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(query)
            Gv1.DataSource = Nothing

            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.DataSource = dtgv
            Gv1.BestFitColumns()

            If dtgv Is Nothing OrElse dtgv.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

            RadPageView1.SelectedPage = RadPageViewPage2
            'Add new Column  for Grand total'
            Dim colGrandtotal As GridViewDecimalColumn = New GridViewDecimalColumn()
            colGrandtotal = New GridViewDecimalColumn()
            colGrandtotal.FormatString = ""
            colGrandtotal.HeaderText = "Grand Total"
            colGrandtotal.Name = "GrandTotal"
            colGrandtotal.Width = 100

            Gv1.MasterTemplate.Columns.Add(colGrandtotal) '129



            'For i As Integer = 0 To Gv1.Rows.Count - 1
            '    Dim grandTotal As Decimal = 0
            '    For j As Integer = 1 To Gv1.Columns.Count - 3
            '        grandTotal = grandTotal + clsCommon.myCdbl(Gv1.Rows(i).Cells(j).Value)
            '    Next
            '    Gv1.Rows(i).Cells("GrandTotal").Value = grandTotal
            'Next

            For i As Integer = 0 To Gv1.Rows.Count - 1
                Dim grandTotal As Decimal = 0
                For j As Integer = 3 To Gv1.Columns.Count - 3
                    Dim cellValue As Object = String.Empty
                    cellValue = Gv1.Rows(i).Cells(j).Value
                    If (Not IsDBNull(Gv1.Rows(i).Cells(j).Value) AndAlso cellValue IsNot Nothing) Then
                        grandTotal = grandTotal + clsCommon.myCdbl(Gv1.Rows(i).Cells(j).Value)
                    End If
                Next
                Gv1.Rows(i).Cells("GrandTotal").Value = grandTotal
            Next


            If Gv1.Rows.Count > 0 Then
                Dim summaryRowItem As New GridViewSummaryRowItem()
                For i As Integer = 2 To Gv1.Columns.Count - 1
                    Dim aa = Gv1.Columns(i).HeaderText()
                    Dim item1 As New GridViewSummaryItem(aa, "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)


                Next

                Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
            End If


            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            ' Hide Column when footer grand total <= 0
            For i As Integer = 3 To Gv1.Columns.Count - 2
                Dim grandTotal As Decimal = 0
                For j As Integer = 0 To Gv1.Rows.Count - 1
                    Dim columnValue As Object = String.Empty
                    columnValue = Gv1.Rows(j).Cells(i).Value
                    If (Not IsDBNull(Gv1.Rows(j).Cells(i).Value) AndAlso columnValue IsNot Nothing) Then
                        grandTotal = grandTotal + clsCommon.myCdbl(Gv1.Rows(j).Cells(i).Value)
                    End If
                Next
                If (clsCommon.myCdbl(grandTotal) > 0) Then
                    Gv1.Columns(i).IsVisible = True
                Else
                    Gv1.Columns(i).IsVisible = False
                End If
            Next

            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            'Pinned column 
            Gv1.Columns(0).IsPinned = True
            Gv1.Columns(1).IsPinned = True
            Gv1.Columns(2).IsPinned = True
            Gv1.Columns(Gv1.Columns.Count - 1).IsPinned = True
            Gv1.Columns(Gv1.Columns.Count - 1).PinPosition = PinnedColumnPosition.Right

            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''







        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
       
    End Sub

    Sub HideColumn()

        For j As Integer = 1 To Gv1.Columns.Count - 1
            If (clsCommon.myCdbl(Gv1.Rows(Gv1.Rows.Count - 1).Cells(j).Value) = Nothing AndAlso clsCommon.myCdbl(Gv1.Rows(Gv1.Rows.Count - 1).Cells(j).Value) <= 0) And Gv1.Columns(Gv1.Columns.Count - 1).HeaderText <> "GrandTotal" Then
                Gv1.Columns(j).IsVisible = False

            End If
            Gv1.Columns("GrandTotal").IsVisible = True
        Next
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub RptZoneWiseFreshSaleReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        txtCustomer.arrValueMember = Nothing
        txtCustomerGroup.arrValueMember = Nothing
        txtItemCode.arrValueMember = Nothing
        txtLocation.arrValueMember = Nothing
        txtZone.arrValueMember = Nothing
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
       
        Try
            If Gv1.Rows.Count > 0 Then


                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("From Date: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy"))
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptZoneWiseFreshSaleReport & "'"))

                If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                    arrHeader.Add("Customer Group : " + clsCommon.GetMulcallStringWithComma(txtCustomerGroup.arrDispalyMember))
                End If
                If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                    arrHeader.Add("Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
                End If
                If txtItemCode.arrValueMember IsNot Nothing AndAlso txtItemCode.arrValueMember.Count > 0 Then
                    arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtItemCode.arrDispalyMember))
                End If
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
                End If
                If txtZone.arrValueMember IsNot Nothing AndAlso txtZone.arrValueMember.Count > 0 Then
                    arrHeader.Add("Zone : " + clsCommon.GetMulcallStringWithComma(txtZone.arrDispalyMember))
                End If

                'Dim sfd As SaveFileDialog = New SaveFileDialog()
                'Dim filePath As String
                'sfd.FileName = Me.Text
                'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                '    filePath = sfd.FileName
                'Else
                '    Exit Sub
                'End If
                'transportSql.exportdataChilRows(Gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)
                transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    'Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs)
    '    Try
    '        If Gv1.Rows.Count > 0 Then
    '            Dim arrHeader As List(Of String) = New List(Of String)()
    '            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
    '            arrHeader.Add("Zone Wise Fresh Sale Report")
    '            clsCommon.MyExportToPDF("Zone Wise Fresh Sale Report", Gv1, arrHeader, "Zone Wise Fresh Sale Report")
    '        End If
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try

    'End Sub

    
    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Try
            If Gv1.Rows.Count > 0 Then


                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy"))
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptZoneWiseFreshSaleReport & "'"))

                If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                    arrHeader.Add("Customer Group : " + clsCommon.GetMulcallStringWithComma(txtCustomerGroup.arrDispalyMember))
                End If
                If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                    arrHeader.Add("Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
                End If
                If txtItemCode.arrValueMember IsNot Nothing AndAlso txtItemCode.arrValueMember.Count > 0 Then
                    arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtItemCode.arrDispalyMember))
                End If
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
                End If
                If txtZone.arrValueMember IsNot Nothing AndAlso txtZone.arrValueMember.Count > 0 Then
                    arrHeader.Add("Zone : " + clsCommon.GetMulcallStringWithComma(txtZone.arrDispalyMember))
                End If

                clsCommon.MyExportToPDF("Zone Wise Fresh Sale Report", Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
