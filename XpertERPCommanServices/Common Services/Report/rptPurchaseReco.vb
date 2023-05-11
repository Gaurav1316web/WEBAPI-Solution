Imports common
Imports System.IO
Imports System.Net
Imports System.Net.Configuration
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Xml
Imports Outlook = Microsoft.Office.Interop.Outlook
Imports System.Text.RegularExpressions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared

Public Class rptPurchaseReco

#Region "Varibales"
    Dim atchqry As String = ""
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dt As DataTable
    '' new varables 
    Public isDataLoad As Boolean = False
    Public dtFrom As Date
    Public dtTo As Date
    Public strType As String
    Public arrItem As ArrayList
    Public arrTransaction As ArrayList
    Public arrCat As Dictionary(Of String, Object) = Nothing
    Public Unit_Code As String = Nothing
    Public arrLocation As ArrayList
    Public arrCustomer As ArrayList
    Public arrCustGroup As ArrayList
    Public arrItemGroup As ArrayList
    Public Stocking_Uom As Boolean = False
    '' new filters
    Dim dtCategory As DataTable
    Dim strPivotForFinalOuterQuery As String
    Dim strPivotForAddChargeFinalOutersumQuery As String
    Dim MIS_Item_Group As String = ""
    Dim arrBack As List(Of String)
    Dim Document_No As String = ""
    Dim Document_No_Old As String = ""
#End Region

    Private Sub SetUserMgmtNew()
        MyBase.SetUserMgmt(clsUserMgtCode.rptPurReco)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
        radbtnBulkExp.Visible = MyBase.isQuickExportFlag
    End Sub

    Sub LoadTypes()
        dt = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Rows.Add("Total Purchase")
        dt.Rows.Add("Location Wise")
        dt.Rows.Add("Vendor Group Wise")
        dt.Rows.Add("Item Wise")
        dt.Rows.Add("Vendor Wise")
        dt.Rows.Add("Document Wise")
        dt.Rows.Add("Document Detail")
        dt.Rows.Add("Document Type Info")

        ddlReportType.DataSource = dt
        ddlReportType.ValueMember = "Code"
        ddlReportType.DisplayMember = "Code"
    End Sub

    'Private Sub txtUOM__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)
    '    Dim qry As String = "select Unit_Code as Code,Unit_Desc as Description from TSPL_UNIT_MASTER"
    '    txtUOM.Value = clsCommon.ShowSelectForm("fndUOMMaster", qry, "Code", "", txtUOM.Value, "Code", isButtonClicked)
    '    If clsCommon.myLen(txtUOM.Value) > 0 Then
    '        chkStockingUOM.Enabled = False
    '    Else
    '        chkStockingUOM.Enabled = True
    '    End If

    'End Sub

    Sub Print(ByVal IsPrint As Exporter, Optional ByVal BulkExport As Integer = 0)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            'If clsCommon.myLen(txtUOM.Value) > 0 Then
            '    arrHeader.Add("UOM : " + txtUOM.Value)
            'End If
            If clsCommon.myLen(ddlReportType.Text) > 0 Then
                arrHeader.Add("Report Type : " + ddlReportType.Text)
            End If
            'If Not IsNothing(txtState.arrValueMember) Then
            '    arrHeader.Add("State : " + clsCommon.GetMulcallStringWithComma(txtState.arrDispalyMember))
            'End If
            If Not IsNothing(txtLocation.arrValueMember) Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If
            If Not IsNothing(txtItem.arrValueMember) Then
                arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember))
            End If
            'If Not IsNothing(txtCustGroup.arrValueMember) Then
            '    arrHeader.Add("Vendor Group : " + clsCommon.GetMulcallStringWithComma(txtCustGroup.arrDispalyMember))
            'End If
            If Not IsNothing(txtCustomer.arrValueMember) Then
                arrHeader.Add("Vendor : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
            End If
            If btnAll.CheckState = True Or btnPosted.CheckState = True Or btnUnposted.CheckState = True Then
                arrHeader.Add("Status : " + IIf(btnAll.IsChecked, "ALL", IIf(btnPosted.IsChecked, "Posted", "Unposted")))
            End If
            If IsPrint = Exporter.Excel Then
                clsCommon.MyExportToExcelGrid(" Purchase Register:" + ddlReportType.SelectedValue, Gv1, arrHeader, Me.Text)
                Exit Sub
            ElseIf IsPrint = Exporter.PDF Then
                clsCommon.MyExportToPDF("Purchase Register" + ddlReportType.SelectedValue, Gv1, arrHeader, "Purchase Register", True)
                Exit Sub
            End If
            clsCommon.ProgressBarShow()
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()

            'Unit_Code = txtUOM.Value
            'If clsCommon.myLen(Unit_Code) <= 0 AndAlso chkStockingUOM.Checked = True Then
            '    Stocking_Uom = True
            'Else
            '    Stocking_Uom = False
            'End If

            clsCommon.ProgressBarUpdate("Loading Data.Please Wait...")
            Dim str As String = ""
            Dim dt As DataTable = Nothing
            Dim strRunQuery As String = ""            
            Dim strMain As String = Nothing
            Dim obj As New clsPurchaseReco
            If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
                obj.Transaction = txtTransaction.arrValueMember
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                obj.Location = txtLocation.arrValueMember
            End If
            If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                obj.Item_Code = txtItem.arrValueMember
            End If
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                obj.Item_Code = txtCustomer.arrValueMember
            End If
            If txtMultAccountNo.arrValueMember IsNot Nothing AndAlso txtMultAccountNo.arrValueMember.Count > 0 Then
                obj.Acc_Code = txtMultAccountNo.arrValueMember
            End If
            If txtMultDoc.arrValueMember IsNot Nothing AndAlso txtMultDoc.arrValueMember.Count > 0 Then
                obj.Doc_No = txtMultDoc.arrValueMember
            End If
            obj.From_Date = fromDate.Value
            obj.To_Date = ToDate.Value
            strRunQuery = clsPurchaseInvoiceHead.GetPurchaseRecoQry(obj)

            'If ddlReportType.SelectedValue = "Document Type Info" Then
            '    strMainForDocumentInfo = ReturnQueryFordocumentinfodetail()
            'Else
            '    strMain = ReturnQuery()
            'End If




            'If ddlReportType.SelectedValue = "Total Purchase" Then
            '    strRunQuery = "select sum(COALESCE([FAT KG],0)) as [Total FAT KG],sum(COALESCE([SNF KG],0)) as [Total SNF KG],sum([Purchase Amount]) as [Amount Before Tax],sum([Additional Amount]) as [Total Additional Amount],sum([Total Tax Amount]) as [Total Tax Amount],sum([Total Amount] ) as [Total Purchase Amount] from (" & strMain & ") as Final"
            'ElseIf ddlReportType.SelectedValue = "Location Wise" Then
            '    strRunQuery = "select [Location Code],[Location Name],[Location Address],sum(COALESCE([FAT KG],0)) as [Total FAT KG],sum(COALESCE([SNF KG],0)) as [Total SNF KG],sum([Purchase Amount]) as [Amount Before Tax],sum([Additional Amount]) as [Total Additional Amount],sum([Total Tax Amount]) as [Total Tax Amount],sum([Total Amount] ) as [Total Purchase Amount] from (" & strMain & ") as Final group by [Location Code],[Location Name],[Location Address] "
            'ElseIf ddlReportType.SelectedValue = "Vendor Group Wise" Then
            '    strRunQuery = "select [Location Code],[Location Name],[Location Address],[Vendor Group Code],[Vendor Group Description],sum(COALESCE([FAT KG],0)) as [Total FAT KG],sum(COALESCE([SNF KG],0)) as [Total SNF KG],sum([Purchase Amount]) as [Amount Before Tax],sum([Additional Amount]) as [Total Additional Amount],sum([Total Tax Amount]) as [Total Tax Amount],sum([Total Amount] ) as [Total Purchase Amount] from (" & strMain & ") as Final group by [Location Code],[Location Address],[Location Name],[Vendor Group Code],[Vendor Group Description]"
            'ElseIf ddlReportType.SelectedValue = "Item Wise" Then
            '    strRunQuery = "select [Location Code],[Location Name],[Location Address],[Vendor Group Code],[Vendor Group Description],[Item Code],[Item Name],sum(COALESCE([FAT KG],0)) as [Total FAT KG],sum(COALESCE([SNF KG],0)) as [Total SNF KG],sum([Purchase Amount]) as [Amount Before Tax],sum([Additional Amount]) as [Total Additional Amount],sum([Total Tax Amount]) as [Total Tax Amount],sum([Total Amount] ) as [Total Purchase Amount],max(HSN_Code) as HSN_Code from (" & strMain & ") as Final group by [Location Code],[Location Address],[Location Name],[Vendor Group Code],[Vendor Group Description],[Item Code],[Item Name]"
            'ElseIf ddlReportType.SelectedValue = "Vendor Wise" Then
            '    strRunQuery = "select [Location Code],[Location Name],[Location Address],[Vendor Group Code],[Vendor Group Description],[Vendor Code],[Vendor Name],[Vendor Address],[Item Code],[Item Name],sum(COALESCE([FAT KG],0)) as [Total FAT KG],sum(COALESCE([SNF KG],0)) as [Total SNF KG],sum([Purchase Amount]) as [Amount Before Tax],sum([Additional Amount]) as [Total Additional Amount],sum([Total Tax Amount]) as [Total Tax Amount],sum([Total Amount] ) as [Total Purchase Amount],max(HSN_Code) as HSN_Code,max(GSTFinalNo) as GSTFinalNo,max(GST_Composition_scheme) as GST_Composition_scheme,max(GSTRegistered) as GSTRegistered,max(VendorCityCode) as VendorCityCode,max(VendorCityName) as VendorCityName from (" & strMain & ") as Final group by [Location Code],[Location Address],[Location Name],[Vendor Group Code],[Vendor Group Description],[Item Code],[Item Name],[Vendor Code],[Vendor Name],[Vendor Address]"
            'ElseIf ddlReportType.SelectedValue = "Document Wise" Then
            '    strRunQuery = "select [Document No],[Document Date],[GR No],[LR No],[Way Bill No],[Trans Type],[Location Code],[Location Name],[Location Address],[Vendor Group Code],[Vendor Group Description],[Vendor Code],[Vendor Name],[Vendor Address],[Vendor TIN No],sum(COALESCE([FAT KG],0)) as [Total FAT KG],sum(COALESCE([SNF KG],0)) as [Total SNF KG],sum([Amount]) as [Amount Before Discount],Sum([Discount AMount]) as [Discount Amount],sum([Purchase Amount]) as [Amount Before Tax],sum([Additional Amount]) as [Total Additional Amount],sum([Total Tax Amount]) as [Total Tax Amount],sum([Total Amount]-[Total Tax Amount]) as [Total Purchase Amount],sum([Total Amount] ) as [Total Amount],max([Vendor Invoice No]) as [Vendor Invoice No],Max([AP Document No]) as [AP Document No],Max([Against Invoice No]) as [Against Invoice No],Max([AP Total Tax]) as [AP Total Tax],Sum([AP Total Add Charge]) as [AP Total Add Charge],Max([AP Landed Amt]) as [AP Landed Amt],Max([AP Document Total]) as [AP Document Total],max(GSTFinalNo) as GSTFinalNo,max(GST_Composition_scheme) as GST_Composition_scheme,max(GSTRegistered) as GSTRegistered,max(VendorCityCode) as VendorCityCode,max(VendorCityName) as VendorCityName,max(Purchase_Tax_Invoice) as Purchase_Tax_Invoice from (" & strMain & ") as Final group by [Document No],[Location Code],[Location Name],[Location Address],[Vendor Group Code],[Vendor Group Description],[Vendor Code],[Vendor Name],[Vendor Address],[Document Date],[Trans Type],[Vendor TIN No],[GR No],[LR No],[Way Bill No]  order by convert(date,[Document Date],103),[Document No] "
            'ElseIf ddlReportType.SelectedValue = "Document Detail" Then
            '    strRunQuery = strMain & " order by  convert(date,[Document Date],103),[Document No] "
            'ElseIf ddlReportType.SelectedValue = "Document Type Info" Then
            '    strRunQuery = strMainForDocumentInfo & " order by  convert(date,[Document Date],103), [Document No] "
            'End If
            ' '' bulk export
            'If BulkExport = 1 Then
            '    If ddlReportType.SelectedValue = "Total Purchase" Then
            '        strRunQuery = "select * from (" & strRunQuery & ") PP order by [Total FAT KG]"
            '        transportSql.BulkExport("Purchase_Register_" & ddlReportType.SelectedValue, strRunQuery, "order by [Total FAT KG] ", "csv")
            '        Exit Sub

            '    ElseIf ddlReportType.SelectedValue = "Location Wise" Then
            '        strRunQuery = "select * from (" & strRunQuery & ") PP order by [Location Code],[Location Name],[Location Address]]"
            '        transportSql.BulkExport("Purchase_Register_" & ddlReportType.SelectedValue, strRunQuery, "order by  [Location Code],[Location Name],[Location Address] ", "csv")
            '        Exit Sub
            '    ElseIf ddlReportType.SelectedValue = "Vendor Group Wise" Then
            '        strRunQuery = "select * from (" & strRunQuery & ") PP order by [Location Code],[Location Address],[Location Name],[Vendor Group Code],[Vendor Group Description]"
            '        transportSql.BulkExport("Purchase_Register_" & ddlReportType.SelectedValue, strRunQuery, "order by  [Location Code],[Location Address],[Location Name],[Vendor Group Code],[Vendor Group Description] ", "csv")
            '        Exit Sub
            '    ElseIf ddlReportType.SelectedValue = "Item Wise" Then
            '        strRunQuery = "select * from (" & strRunQuery & ") PP order by [Location Code],[Location Address],[Location Name],[Vendor Group Code],[Vendor Group Description],[Item Code],[Item Name]"
            '        transportSql.BulkExport("Purchase_Register_" & ddlReportType.SelectedValue, strRunQuery, "order by  [Location Code],[Location Address],[Location Name],[Vendor Group Code],[Vendor Group Description],[Item Code],[Item Name] ", "csv")
            '        Exit Sub
            '    ElseIf ddlReportType.SelectedValue = "Vendor Wise" Then
            '        strRunQuery = "select * from (" & strRunQuery & ") PP order by [Location Code],[Location Address],[Location Name],[Vendor Group Code],[Vendor Group Description],[Item Code],[Item Name],[Vendor Code],[Vendor Name],[Vendor Address]"
            '        transportSql.BulkExport("Purchase_Register_" & ddlReportType.SelectedValue, strRunQuery, "order by  [Location Code],[Location Address],[Location Name],[Vendor Group Code],[Vendor Group Description],[Item Code],[Item Name],[Vendor Code],[Vendor Name],[Vendor Address] ", "csv")
            '        Exit Sub
            '    ElseIf ddlReportType.SelectedValue = "Document Wise" Then

            '        transportSql.BulkExport("Purchase_Register_" & ddlReportType.SelectedValue, strRunQuery, "order by  convert(date,[Document Date],103),[Document No] ", "csv")
            '        Exit Sub
            '    ElseIf ddlReportType.SelectedValue = "Document Detail" Then

            '        transportSql.BulkExport("Purchase_Register_" & ddlReportType.SelectedValue, strRunQuery, "order by  convert(date,[Document Date],103),[Document No] ", "csv")
            '        Exit Sub
            '    ElseIf ddlReportType.SelectedValue = "Document Type Info" Then
            '        transportSql.BulkExport("Purchase_Register_" & ddlReportType.SelectedValue, strRunQuery, "order by  convert(date,[Document Date],103), [Document No] ", "csv")
            '        Exit Sub
            '    End If


            'ElseIf BulkExport = 2 Then
            '    If ddlReportType.SelectedValue = "Total Purchase" Then
            '        strRunQuery = "select * from (" & strRunQuery & ") PP order by [Total FAT KG]"
            '        transportSql.BulkExport("Purchase_Register_" & ddlReportType.SelectedValue, strRunQuery, "order by [Total FAT KG] ", "xls")
            '        Exit Sub

            '    ElseIf ddlReportType.SelectedValue = "Location Wise" Then
            '        strRunQuery = "select * from (" & strRunQuery & ") PP order by [Location Code],[Location Name],[Location Address]]"
            '        transportSql.BulkExport("Purchase_Register_" & ddlReportType.SelectedValue, strRunQuery, "order by  [Location Code],[Location Name],[Location Address] ", "xls")
            '        Exit Sub
            '    ElseIf ddlReportType.SelectedValue = "Vendor Group Wise" Then
            '        strRunQuery = "select * from (" & strRunQuery & ") PP order by [Location Code],[Location Address],[Location Name],[Vendor Group Code],[Vendor Group Description]"
            '        transportSql.BulkExport("Purchase_Register_" & ddlReportType.SelectedValue, strRunQuery, "order by  [Location Code],[Location Address],[Location Name],[Vendor Group Code],[Vendor Group Description] ", "xls")
            '        Exit Sub
            '    ElseIf ddlReportType.SelectedValue = "Item Wise" Then
            '        strRunQuery = "select * from (" & strRunQuery & ") PP order by [Location Code],[Location Address],[Location Name],[Vendor Group Code],[Vendor Group Description],[Item Code],[Item Name]"
            '        transportSql.BulkExport("Purchase_Register_" & ddlReportType.SelectedValue, strRunQuery, "order by  [Location Code],[Location Address],[Location Name],[Vendor Group Code],[Vendor Group Description],[Item Code],[Item Name] ", "xls")
            '        Exit Sub
            '    ElseIf ddlReportType.SelectedValue = "Vendor Wise" Then
            '        strRunQuery = "select * from (" & strRunQuery & ") PP order by [Location Code],[Location Address],[Location Name],[Vendor Group Code],[Vendor Group Description],[Item Code],[Item Name],[Vendor Code],[Vendor Name],[Vendor Address]"
            '        transportSql.BulkExport("Purchase_Register_" & ddlReportType.SelectedValue, strRunQuery, "order by  [Location Code],[Location Address],[Location Name],[Vendor Group Code],[Vendor Group Description],[Item Code],[Item Name],[Vendor Code],[Vendor Name],[Vendor Address] ", "xls")
            '        Exit Sub
            '    ElseIf ddlReportType.SelectedValue = "Document Wise" Then
            '        transportSql.BulkExport("Purchase_Register_" & ddlReportType.SelectedValue, strRunQuery, "order by convert(date,[Document Date],103), [Document No] ", "xls")
            '        Exit Sub
            '    ElseIf ddlReportType.SelectedValue = "Document Detail" Then
            '        transportSql.BulkExport("Purchase_Register_" & ddlReportType.SelectedValue, strRunQuery, "order by  convert(date,[Document Date],103),[Document No] ", "xls")
            '        Exit Sub
            '    ElseIf ddlReportType.SelectedValue = "Document Type Info" Then
            '        transportSql.BulkExport("Purchase_Register_" & ddlReportType.SelectedValue, strRunQuery, "order by  convert(date,[Document Date],103), [Document No] ", "xls")
            '        Exit Sub
            '    End If
            'End If

            RadPageViewPage2.Text = "Purchase Reco"
            dt = clsDBFuncationality.GetDataTable(strRunQuery)
            Gv1.DataSource = Nothing
            Gv1.Columns.Clear()
            Gv1.Rows.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.EnableFiltering = True
            Gv1.Tag = ddlReportType.SelectedValue
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            Else
                EnableDisableAllControl(False)
                Gv1.DataSource = dt
                SetGridFormationOFGV1()
            End If
            FindAndRestoreGridLayout(Me)
            Gv1.MasterTemplate.AllowAddNewRow = False
            RadPageView1.SelectedPage = RadPageViewPage2
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        Finally
            clsCommon.ProgressBarHide()
        End Try
    End Sub

    Sub EnableDisableAllControl(ByVal val As Boolean)
        'txtUOM.Enabled = val
        ddlReportType.Enabled = val
        txtTransaction.Enabled = val
        txtState.Enabled = val
        txtLocation.Enabled = val
        txtItem.Enabled = val
        txtCustGroup.Enabled = val
        txtCustomer.Enabled = val
        txtMultAccountNo.Enabled = val
        txtMultDoc.Enabled = val
        RadGroupBox6.Enabled = val
        chkSerializeInv.Enabled = val
        RadGroupBox3.Enabled = val
        'RadGroupBox7.Enabled = val
        'chkStockingUOM.Enabled = val
    End Sub

    Sub SetGridFormationOFGV1()
        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = False
            'Gv1.Columns(ii).FormatString = "{0:n2}"
        Next
        Gv1.Columns("Document Code").IsVisible = True
        Gv1.Columns("Document Code").Width = 120
        Gv1.Columns("Document Code").HeaderText = "Document No"

        Gv1.Columns("Document Date").IsVisible = True
        Gv1.Columns("Document Date").Width = 120
        Gv1.Columns("Document Date").HeaderText = "Document Date"

        Gv1.Columns("Trans_Type").IsVisible = True
        Gv1.Columns("Trans_Type").Width = 120
        Gv1.Columns("Trans_Type").HeaderText = "Transaction Type"

        Gv1.Columns("AP_Doc_No").IsVisible = True
        Gv1.Columns("AP_Doc_No").Width = 120
        Gv1.Columns("AP_Doc_No").HeaderText = "AP Doc No"

        Gv1.Columns("AP_Account_Code").IsVisible = True
        Gv1.Columns("AP_Account_Code").Width = 120
        Gv1.Columns("AP_Account_Code").HeaderText = "AP Account No"

        Gv1.Columns("AP_Account_Desc").IsVisible = True
        Gv1.Columns("AP_Account_Desc").Width = 120
        Gv1.Columns("AP_Account_Desc").HeaderText = "AP Account Desc"

        Gv1.Columns("AP_Account_Amount").IsVisible = True
        Gv1.Columns("AP_Account_Amount").Width = 120
        Gv1.Columns("AP_Account_Amount").HeaderText = "AP Account Amount"

        Gv1.Columns("GL_VoucherNo").IsVisible = True
        Gv1.Columns("GL_VoucherNo").Width = 120
        Gv1.Columns("GL_VoucherNo").HeaderText = "Voucher No"

        Gv1.Columns("GL_Source_Doc_No").IsVisible = True
        Gv1.Columns("GL_Source_Doc_No").Width = 120
        Gv1.Columns("GL_Source_Doc_No").HeaderText = "GL Source Doc No"

        Gv1.Columns("GL_Account_No").IsVisible = True
        Gv1.Columns("GL_Account_No").Width = 120
        Gv1.Columns("GL_Account_No").HeaderText = "GL Account No"

        Gv1.Columns("GL_Account_Desc").IsVisible = True
        Gv1.Columns("GL_Account_Desc").Width = 120
        Gv1.Columns("GL_Account_Desc").HeaderText = "GL Account Desc"

        Gv1.Columns("GL_Account_Amount").IsVisible = True
        Gv1.Columns("GL_Account_Amount").Width = 120
        Gv1.Columns("GL_Account_Amount").HeaderText = "GL Account Amount"


        For i As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(i).BestFit()
        Next

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        For Each col As GridViewColumn In Gv1.Columns
            If clsCommon.CompairString(col.Name, "Total FAT KG") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "Total SNF KG") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "FAT KG") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "SNF KG") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "Quantity") = CompairStringResult.Equal Then
                Dim item1 As New GridViewSummaryItem(col.Name, "{0:F3}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)

            ElseIf col.Name.Contains("Amount") = True Or col.Name.Contains("Amt") = True Or col.Name.Contains("Total") = True Then
                Dim item As New GridViewSummaryItem(col.Name, "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item)
            ElseIf col.Name.Contains("Rate") = True Or col.Name.Contains("%") = True Then
                Dim item As New GridViewSummaryItem(col.Name, "{0:F2}", GridAggregateFunction.Avg)
                summaryRowItem.Add(item)
            End If
        Next
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        'End If




        RadPageView1.SelectedPage = RadPageViewPage2
        Gv1.AllowAddNewRow = False
        Gv1.ShowGroupPanel = True
        Gv1.BestFitColumns()
    End Sub

    Sub Reset()
        EnableDisableAllControl(True)
    End Sub

    Enum Exporter
        Excel = 0
        PDF = 1
        Print = 2
        Refresh = 3
    End Enum

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= Gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To Gv1.Columns.Count - 1 Step ii + 1
                        Gv1.Columns(ii).IsVisible = False
                        Gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    Gv1.LoadLayout(obj.GridLayout)

                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click

    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click

    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Print(Exporter.Refresh)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub rmExport_Click(sender As Object, e As EventArgs) Handles rmExport.Click
        If (Gv1.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow("No Data To Export")
            Exit Sub
        End If
        Print(Exporter.Excel)
    End Sub

    Private Sub rmSetting_Click(sender As Object, e As EventArgs) Handles rmSetting.Click
        Dim frm As New FrmMailSMSSettingNew2()
        frm.FormId = clsUserMgtCode.RptFreshSaleRegister1
        frm.ShowDialog()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub rptPurchaseReco_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            Print(Exporter.Refresh)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub

    Private Sub rptPurchaseReco_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Visible = False
        arrBack = New List(Of String)
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New ")
        GetMIS_ITem_GroupColumn()
        If clsCommon.myLen(MIS_Item_Group) <= 0 Then
            clsCommon.MyMessageBoxShow("MIS Item Group Custom field is not create in Item Structure.")
        End If

        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        'txtUOM.Value = ""
        LoadTypes()
        Document_No = ""
        ddlReportType.SelectedValue = "Total Purchase"
        'LoadCategory()
        txtState.arrValueMember = Nothing
        txtLocation.arrValueMember = Nothing
        txtTransaction.arrValueMember = Nothing
        txtItem.arrValueMember = Nothing
        txtCustomer.arrValueMember = Nothing
        txtCustGroup.arrValueMember = Nothing
        'rbtnCategoryAll.IsChecked = True
        ddlReportType.Enabled = True
        txtLocation.Enabled = True
        txtState.Enabled = True
        txtTransaction.Enabled = True
        txtItem.Enabled = True
        txtCustomer.Enabled = True
        txtCustGroup.Enabled = True
        ddlReportType.SelectedIndex = 0
        btnPosted.IsChecked = True
        Gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        RadPageViewPage2.Text = ddlReportType.SelectedValue
        'chkStockingUOM.Enabled = True
        'chkStockingUOM.Checked = False

        RadPageView1.SelectedPage = RadPageViewPage1
        Gv1.EnableGrouping = True
        Gv1.ShowGroupPanel = True
        If isDataLoad Then
            fromDate.Value = dtFrom
            ToDate.Value = dtTo
            'txtUOM.Value = Unit_Code
            txtLocation.arrValueMember = arrLocation
            txtItem.arrValueMember = arrItem
            txtCustomer.arrValueMember = arrCustomer
            txtTransaction.arrValueMember = arrTransaction
            
            ddlReportType.SelectedValue = strType
            Print(True)
            Me.Visible = True
        End If
    End Sub

    Private Sub Gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        DrillDown()
    End Sub

    Sub DrillDown()
        Try
            'If clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Total Purchase") = CompairStringResult.Equal Then
            '    If Not arrBack.Contains("Total Purchase") Then
            '        arrBack.Add("Total Purchase")
            '    End If
            '    ddlReportType.SelectedValue = "Location Wise"
            '    Document_No = ""
            '    Print(Exporter.Refresh)
            'ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Location Wise") = CompairStringResult.Equal Then
            '    If Not arrBack.Contains("Location Wise") Then
            '        arrBack.Add("Location Wise")
            '    End If
            '    ddlReportType.SelectedValue = "Vendor Group Wise"
            '    arrLocation = New ArrayList()
            '    arrLocation = txtLocation.arrValueMember
            '    Dim tmp As New ArrayList()
            '    tmp.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Location Code").Value))
            '    txtLocation.arrValueMember = tmp
            '    Document_No = ""
            '    Print(Exporter.Refresh)
            'ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Vendor Group Wise") = CompairStringResult.Equal Then
            '    If Not arrBack.Contains("Vendor Group Wise") Then
            '        arrBack.Add("Vendor Group Wise")
            '    End If
            '    ddlReportType.SelectedValue = "Item Wise"
            '    arrCustGroup = New ArrayList()
            '    arrCustGroup = txtCustGroup.arrValueMember
            '    Dim tmp As New ArrayList()
            '    tmp.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Vendor Group Code").Value))
            '    txtCustGroup.arrValueMember = tmp
            '    Document_No = ""
            '    Print(Exporter.Refresh)
            'ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Item Wise") = CompairStringResult.Equal Then
            '    If Not arrBack.Contains("Item Wise") Then
            '        arrBack.Add("Item Wise")
            '    End If
            '    ddlReportType.SelectedValue = "Vendor Wise"
            '    arrItem = New ArrayList()
            '    arrItem = txtItem.arrValueMember
            '    Dim tmp As New ArrayList()
            '    tmp.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Item Code").Value))
            '    txtItem.arrValueMember = tmp
            '    Document_No = ""
            '    Print(Exporter.Refresh)
            'ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Vendor Wise") = CompairStringResult.Equal Then
            '    If Not arrBack.Contains("Vendor Wise") Then
            '        arrBack.Add("Vendor Wise")
            '    End If
            '    ddlReportType.SelectedValue = "Document Wise"
            '    arrCustomer = New ArrayList()
            '    arrCustomer = txtCustomer.arrValueMember
            '    Dim tmp As New ArrayList()
            '    tmp.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Vendor Code").Value))
            '    txtCustomer.arrValueMember = tmp
            '    Document_No = ""
            '    Print(Exporter.Refresh)
            'ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Document Wise") = CompairStringResult.Equal Then
            '    If Not arrBack.Contains("Document Wise") Then
            '        arrBack.Add("Document Wise")
            '    End If
            '    ddlReportType.SelectedValue = "Document Detail"
            '    Document_No_Old = Document_No
            '    Document_No = clsCommon.myCstr(Gv1.CurrentRow.Cells("Document No").Value)
            '    Print(Exporter.Refresh)
            'ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Document Detail") = CompairStringResult.Equal Then
            Dim strTransType As String = clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans_Type").Value)
            Dim strTransCode As String = clsCommon.myCstr(Gv1.CurrentRow.Cells("Document Code").Value)
            If clsCommon.myLen(strTransType) > 0 AndAlso clsCommon.myLen(strTransCode) > 0 Then
                Select Case strTransType
                    Case "Purchase Invoice"
                        clsOpenTransactionForm.OpenTransacionForm(EnumTransType.PurchaseInvoice, strTransCode)
                    Case "Milk Receipt"
                        clsOpenTransactionForm.OpenTransacionForm(EnumTransType.MilkPI, strTransCode)
                    Case "Bulk Purchase"
                        clsOpenTransactionForm.OpenTransacionForm(EnumTransType.Bulk_Purchase, strTransCode)
                    Case "Bulk Purchase Return"
                        clsOpenTransactionForm.OpenTransacionForm(EnumTransType.Bulk_Purchase_Return, strTransCode)
                    Case "MCC Transfer"
                        clsOpenTransactionForm.OpenTransacionForm(EnumTransType.Mcc_transfer, strTransCode)
                    Case "Transfer"
                        clsOpenTransactionForm.OpenTransacionForm(EnumTransType.transfer, strTransCode)
                    Case "Purchase Return"
                        clsOpenTransactionForm.OpenTransacionForm(EnumTransType.PurchaseReturn, strTransCode)
                End Select

            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        Dim qry As String = " select Vendor_Code as Code,Vendor_name as Name from TSPL_VENDOR_master  WHERE  Status='N'  "
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("VenMulSel", qry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
        Dim FrmR As New FrmPendingRequisitionQty
        FrmR.SetDiplayMember(txtCustomer, "Vendor_name", "TSPL_VENDOR_master", "Vendor_Code")
    End Sub

    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Dim qry As String = " select Item_Code,Item_Desc from TSPL_ITEM_MASTER order by Item_Code "
        txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Item_Code", "Item_Code", txtItem.arrValueMember, txtItem.arrDispalyMember)
        Dim FrmR As New FrmPendingRequisitionQty
        FrmR.SetDiplayMember(txtItem, "Item_Desc", "TSPL_ITEM_MASTER", "Item_Code")
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim stateCond As String = ""
        If txtState.arrValueMember IsNot Nothing AndAlso txtState.arrValueMember.Count > 0 Then
            stateCond = " and state in  (" + clsCommon.GetMulcallString(txtState.arrValueMember) + ") "
        End If
        Dim qry As String = " select Location_Code as Code,Location_Desc as [Name] from TSPL_LOCATION_MASTER  where location_type IN  ('Physical','Virtual') " & stateCond & "  "
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        Dim FrmR As New FrmPendingRequisitionQty
        FrmR.SetDiplayMember(txtLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub

    Private Sub txtTransaction__My_Click(sender As Object, e As EventArgs) Handles txtTransaction._My_Click
        Dim qry As String = " Select xxx.Code,  xxx.Name From (" & _
                                 " Select distinct 'PI' As Code,    'Purchase Invoice' As Name from TSPL_PI_HEAD " & _
                                 " Union  Select distinct 'MCC' As Code,    'Milk Receipt' As Name from TSPL_MILK_RECEIPT_HEAD " & _
                                 " Union  Select distinct 'Bulk' As Code,    'Bulk Purchase' As Name from tspl_Bulk_milk_purchase_Invoice_head " & _
                                  " Union  Select distinct 'Bulk Purchase Return' As Code,    'Bulk Purchase Return' As Name from TSPL_BULK_MILK_PURCHASE_RETURN_HEAD " & _
                                 " Union  Select distinct 'MCC Transfer' As Code,    'MCC Transfer' As Name from TSPL_MILK_TRANSFER_IN " & _
                                 " Union  Select distinct 'Transfer' As Code,    'Transfer' As Name from TSPL_TRANSFER_ORDER_HEAD " & _
                                  " Union  Select distinct 'Transfer Return' As Code,    'Transfer Return' As Name from TSPL_TRANSFER_RETURN " & _
                                 " Union  Select distinct 'Return' As Code,    'Purchase Return' As Name from TSPL_PR_HEAD " & _
                                 " union Select distinct 'MT' As Code,    'Merchant Trade' As Name from TSPL_PI_HEAD " & _
                                 " ) xxx"
        txtTransaction.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulPur", qry, "Name", "Name", txtTransaction.arrValueMember, txtTransaction.arrDispalyMember)
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

    Function GetMIS_ITem_GroupColumn() As String
        Dim qry As String = ""
        qry = " select MAP.Custom_Field_Code from TSPL_CUSTOM_FIELD_MAPPING MAP " & _
            " left join TSPL_CUSTOM_FIELD_HEAD CF on MAP.Custom_Field_Code=CF.Code " & _
            " where CF.Name='MIS Item Group' and MAP.PROGRAM_CODE='" & clsUserMgtCode.itemStructure & "'"
        MIS_Item_Group = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        Return MIS_Item_Group
    End Function

    Private Sub Gv1_KeyDown(sender As Object, e As KeyEventArgs) Handles Gv1.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            DrillDown()
        End If
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            If clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Total Purchase") = CompairStringResult.Equal Then

            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Location Wise") = CompairStringResult.Equal AndAlso arrBack.Contains("Total Purchase") Then
                arrBack.Remove("Total Purchase")
                ddlReportType.SelectedValue = "Total Purchase"
                Print(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Vendor Group Wise") = CompairStringResult.Equal AndAlso arrBack.Contains("Location Wise") Then
                arrBack.Remove("Location Wise")
                ddlReportType.SelectedValue = "Location Wise"
                txtLocation.arrValueMember = arrLocation
                Print(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Item Wise") = CompairStringResult.Equal AndAlso arrBack.Contains("Vendor Group Wise") Then
                arrBack.Remove("Vendor Group Wise")
                ddlReportType.SelectedValue = "Vendor Group Wise"
                txtCustGroup.arrValueMember = arrCustGroup
                Print(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Vendor Wise") = CompairStringResult.Equal AndAlso arrBack.Contains("Item Wise") Then
                arrBack.Remove("Item Wise")
                ddlReportType.SelectedValue = "Item Wise"
                txtItem.arrValueMember = arrItem
                Print(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Document Wise") = CompairStringResult.Equal AndAlso arrBack.Contains("Vendor Wise") Then
                arrBack.Remove("Vendor Wise")
                ddlReportType.SelectedValue = "Vendor Wise"
                txtCustomer.arrValueMember = arrCustomer
                Print(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Document Detail") = CompairStringResult.Equal AndAlso arrBack.Contains("Document Wise") Then
                arrBack.Remove("Document Wise")
                ddlReportType.SelectedValue = "Document Wise"
                Document_No = Document_No_Old
                'txtCustomer.arrValueMember = arrCustomer
                Print(Exporter.Refresh)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCustGroup__My_Click(sender As Object, e As EventArgs) Handles txtCustGroup._My_Click
        Dim qry As String = " select Ven_Group_Code as Code,Group_desc as Name from TSPL_VENDOR_group"
        txtCustGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("VenGroupMulSel", qry, "Code", "Name", txtCustGroup.arrValueMember, txtCustGroup.arrDispalyMember)
        Dim FrmR As New FrmPendingRequisitionQty
        FrmR.SetDiplayMember(txtCustGroup, "Group_desc", "TSPL_VENDOR_group", "Ven_Group_Code")
    End Sub

    Private Sub btnQuickExport_Click(sender As Object, e As EventArgs) Handles btnQuickExport.Click
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : Purchase Reco")
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")
            'If clsCommon.myLen(txtUOM.Value) > 0 Then
            '    arrHeader.Add("UOM : " + txtUOM.Value)
            'End If
            If clsCommon.myLen(ddlReportType.Text) > 0 Then
                arrHeader.Add("Report Type : " + ddlReportType.Text)
            End If
            If Not IsNothing(txtState.arrValueMember) Then
                arrHeader.Add("State : " + clsCommon.GetMulcallStringWithComma(txtState.arrDispalyMember))
            End If
            If Not IsNothing(txtLocation.arrValueMember) Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If
            If Not IsNothing(txtItem.arrValueMember) Then
                arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember))
            End If
            If Not IsNothing(txtCustGroup.arrValueMember) Then
                arrHeader.Add("Vendor Group : " + clsCommon.GetMulcallStringWithComma(txtCustGroup.arrDispalyMember))
            End If
            If Not IsNothing(txtCustomer.arrValueMember) Then
                arrHeader.Add("Vendor : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
            End If
            If btnAll.CheckState = True Or btnPosted.CheckState = True Or btnUnposted.CheckState = True Then
                arrHeader.Add("Status : " + IIf(btnAll.IsChecked, "ALL", IIf(btnPosted.IsChecked, "Posted", "Unposted")))
            End If
            Dim sfd As SaveFileDialog = New SaveFileDialog()
            Dim filePath As String
            sfd.FileName = Me.Text
            sfd.Filter = "Excel 2007 (*.xlsx) |*.xlsx;|Excel 97-2003 (*.xls)|*.xlsx;|CSV Files (*.csv) |*.csv"
            If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                filePath = sfd.FileName
            Else
                Exit Sub
            End If
            transportSql.exportdataChilRows(Gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader) 'frm.Text)
            common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            Process.Start(filePath)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Public Shared Function GetTaxQuery(ByVal lstTables As List(Of String)) As String
        Dim qry As String = String.Empty
        If Not lstTables Is Nothing AndAlso lstTables.Count > 0 Then
            For Each TableName As String In lstTables
                For intloop As Integer = 1 To 10
                    If clsCommon.myLen(qry) <= 0 Then
                        qry = "select TAX" & intloop & " from " & TableName & ""
                    Else
                        qry = qry & " Union  " & "select TAX" & intloop & " from " & TableName & ""
                    End If
                Next
            Next
        Else
            Return qry
        End If
        Return qry
    End Function

    Public Shared Function GetAddChargeQuery(ByVal lstTables As List(Of String)) As String
        Dim qry As String = String.Empty
        If Not lstTables Is Nothing AndAlso lstTables.Count > 0 Then
            For Each TableName As String In lstTables
                For intloop As Integer = 1 To 10
                    If clsCommon.myLen(qry) <= 0 Then
                        qry = "select Add_Charge_Code" & intloop & "  from " & TableName & ""
                    Else
                        qry = qry & " Union  " & "select Add_Charge_Code" & intloop & " from " & TableName & ""
                    End If
                Next
            Next
        Else
            Return qry
        End If
        Return qry
    End Function

    Public Shared Function GetAddChargeZeroQuery(ByVal lstTables As List(Of String)) As String
        Dim qry As String = String.Empty
        If Not lstTables Is Nothing AndAlso lstTables.Count > 0 Then
            For Each TableName As String In lstTables
                For intloop As Integer = 1 To 10
                    If clsCommon.myLen(qry) <= 0 Then
                        qry = "select 'AC_'+Add_Charge_Code" & intloop & " as Add_Charge_Code" & intloop & "  from " & TableName & ""
                    Else
                        qry = qry & " Union  " & "select 'AC_'+Add_Charge_Code" & intloop & " as Add_Charge_Code" & intloop & " from " & TableName & ""
                    End If
                Next
            Next
        Else
            Return qry
        End If
        If clsCommon.myLen(qry) > 0 Then
            qry = "select * from( " & qry & ") as t1 where Add_Charge_Code1 not in ('AC_')"
        End If

        Return qry
    End Function

    Private Sub txtState__My_Click(sender As Object, e As EventArgs) Handles txtState._My_Click
        Dim qry As String = " select STATE_CODE as Code,STATE_NAME as Name from TSPL_STATE_MASTER"
        txtState.arrValueMember = clsCommon.ShowMultipleSelectForm("StateMulSel", qry, "Code", "Name", txtState.arrValueMember, txtState.arrDispalyMember)
        Dim FrmR As New FrmPendingRequisitionQty
        FrmR.SetDiplayMember(txtState, "STATE_NAME", "TSPL_sTATE_MASTER", "STATE_CODE")
    End Sub

    Private Sub BulkExportCSV_Click(sender As Object, e As EventArgs) Handles BulkExportCSV.Click
        Print(Exporter.Refresh, 1)
    End Sub

    Private Sub BulkExportXls_Click(sender As Object, e As EventArgs) Handles BulkExportXls.Click
        Print(Exporter.Refresh, 2)
    End Sub

    Private Sub txtMultAccountNo__My_Click(sender As Object, e As EventArgs) Handles txtMultAccountNo._My_Click
        Dim qry As String = " select  Account_Code AS Code,Description as [Name] from TSPL_GL_ACCOUNTS "
        txtMultAccountNo.arrValueMember = clsCommon.ShowMultipleSelectForm("GLMulSel", qry, "Code", "Name", txtMultAccountNo.arrValueMember, txtMultAccountNo.arrDispalyMember)
    End Sub

    Private Sub txtMultDoc__My_Click(sender As Object, e As EventArgs) Handles txtMultDoc._My_Click
        Dim qry As String = " Select xxx.Code,  xxx.Name,[Document Code],[Document Date] From (" & _
                                 " Select distinct 'PI' As Code,'Purchase Invoice' As Name,PI_No AS [Document Code],PI_Date as [Document Date] from TSPL_PI_HEAD " & _
                                 " Union  Select distinct 'MCC' As Code,    'Milk Receipt' As Name,DOC_CODE,DOC_DATE from TSPL_MILK_RECEIPT_HEAD " & _
                                 " Union  Select distinct 'Bulk' As Code,    'Bulk Purchase' As Name,DOC_NO,DOC_DATE from tspl_Bulk_milk_purchase_Invoice_head " & _
                                  " Union  Select distinct 'Bulk Purchase Return' As Code,    'Bulk Purchase Return' As Name,Pur_Return_No,Pur_Return_Date from TSPL_BULK_MILK_PURCHASE_RETURN_HEAD " & _
                                 " Union  Select distinct 'MCC Transfer' As Code,    'MCC Transfer' As Name,Receipt_Challan_No,Receipt_Challan_Date from TSPL_MILK_TRANSFER_IN " & _
                                 " Union  Select distinct 'Transfer' As Code,    'Transfer' As Name,Document_No,Document_Date from TSPL_TRANSFER_ORDER_HEAD " & _
                                  " Union  Select distinct 'Transfer Return' As Code,    'Transfer Return' As Name,Document_No,Document_Date from TSPL_TRANSFER_RETURN " & _
                                 " Union  Select distinct 'Return' As Code,    'Purchase Return' As Name,PR_No,PR_Date from TSPL_PR_HEAD " & _
                                 " union Select distinct 'MT' As Code,    'Merchant Trade' As Name,PI_No,PI_Date from TSPL_PI_HEAD " & _
                                 " ) xxx"
        txtMultDoc.arrValueMember = clsCommon.ShowMultipleSelectForm("DocMulSel", qry, "Document Code", "Document Date", txtMultDoc.arrValueMember, txtMultDoc.arrDispalyMember)
    End Sub
End Class


