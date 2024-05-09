Imports common
Imports System.IO
Imports System.Net
Imports System.Net.Configuration
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Xml
Imports System.Text.RegularExpressions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports Excel = Microsoft.Office.Interop.Excel


Public Class RptPurchaseRegisterReport

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
    Dim StrGLAccount As String = ""
#End Region

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
      
        btnQuickExpNew.Visible = MyBase.isExport
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
        'dt.Rows.Add("Document Type Info")'Commented by preeti gupta Against ticket no[TEC/05/06/19-000513]
        'dt.Rows.Add("GL Account Wise")

        ddlReportType.DataSource = dt
        ddlReportType.ValueMember = "Code"
        ddlReportType.DisplayMember = "Code"
    End Sub

    Private Sub txtUOM__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtUOM._MYValidating
        Dim qry As String = "select Unit_Code as Code,Unit_Desc as Description from TSPL_UNIT_MASTER"
        txtUOM.Value = clsCommon.ShowSelectForm("fndUOMMaster", qry, "Code", "", txtUOM.Value, "Code", isButtonClicked)
        If clsCommon.myLen(txtUOM.Value) > 0 Then
            chkStockingUOM.Enabled = False
        Else
            chkStockingUOM.Enabled = True
        End If

    End Sub

    Sub Print(ByVal IsPrint As Exporter, Optional ByVal BulkExport As Integer = 0)
        Try
            'KDI/09/07/18-000392 by balwinder check vendor name should come from vendor master.
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            If clsCommon.myLen(txtUOM.Value) > 0 Then
                arrHeader.Add("UOM : " + txtUOM.Value)
            End If
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

            Unit_Code = txtUOM.Value
            If clsCommon.myLen(Unit_Code) <= 0 AndAlso chkStockingUOM.Checked = True Then
                Stocking_Uom = True
            Else
                Stocking_Uom = False
            End If

            clsCommon.ProgressBarUpdate("Loading Data.Please Wait...")
            Dim str As String = ""
            Dim dt As DataTable = Nothing
            Dim strRunQuery As String = ""
            Dim strMainForDocumentInfo As String = Nothing
            Dim strMain As String = Nothing
            If ddlReportType.SelectedValue = "Document Type Info" Then
                strMainForDocumentInfo = ReturnQueryFordocumentinfodetail()
            Else
                strMain = ReturnQuery()
            End If




            If ddlReportType.SelectedValue = "Total Purchase" Then
                strRunQuery = "select sum(COALESCE([FAT KG],0)) as [Total FAT KG],sum(COALESCE([SNF KG],0)) as [Total SNF KG],sum([Purchase Amount]) as [Amount Before Tax],sum([Additional Amount]) as [Total Additional Amount],sum([Total Tax Amount]) as [Total Tax Amount],sum([Total Amount] ) as [Total Purchase Amount] from (" & strMain & ") as Final"
            ElseIf ddlReportType.SelectedValue = "Location Wise" Then
                strRunQuery = "select [Location Code],[Location Name],max(Location_GSTIN) as Location_GSTIN,[Location Address],sum(COALESCE([FAT KG],0)) as [Total FAT KG],sum(COALESCE([SNF KG],0)) as [Total SNF KG],sum([Purchase Amount]) as [Amount Before Tax],sum([Additional Amount]) as [Total Additional Amount],sum([Total Tax Amount]) as [Total Tax Amount],sum([Total Amount] ) as [Total Purchase Amount] from (" & strMain & ") as Final group by [Location Code],[Location Name],[Location Address] "
            ElseIf ddlReportType.SelectedValue = "Vendor Group Wise" Then
                strRunQuery = "select [Location Code],[Location Name],max(Location_GSTIN) as Location_GSTIN,[Location Address],[Vendor Group Code],[Vendor Group Description],sum(COALESCE([FAT KG],0)) as [Total FAT KG],sum(COALESCE([SNF KG],0)) as [Total SNF KG],sum([Purchase Amount]) as [Amount Before Tax],sum([Additional Amount]) as [Total Additional Amount],sum([Total Tax Amount]) as [Total Tax Amount],sum([Total Amount] ) as [Total Purchase Amount] from (" & strMain & ") as Final group by [Location Code],[Location Address],[Location Name],[Vendor Group Code],[Vendor Group Description]"
            ElseIf ddlReportType.SelectedValue = "Item Wise" Then
                strRunQuery = "select [Location Code],[Location Name],max(Location_GSTIN) as Location_GSTIN,[Location Address],[Vendor Group Code],[Vendor Group Description],[Item Code],[Item Name],sum(COALESCE([FAT KG],0)) as [Total FAT KG],sum(COALESCE([SNF KG],0)) as [Total SNF KG],sum([Purchase Amount]) as [Amount Before Tax],sum([Additional Amount]) as [Total Additional Amount],sum([Total Tax Amount]) as [Total Tax Amount],sum([Total Amount] ) as [Total Purchase Amount],max(HSN_Code) as HSN_Code from (" & strMain & ") as Final group by [Location Code],[Location Address],[Location Name],[Vendor Group Code],[Vendor Group Description],[Item Code],[Item Name]"
            ElseIf ddlReportType.SelectedValue = "Vendor Wise" Then
                strRunQuery = "select [Location Code],[Location Name],max(Location_GSTIN) as Location_GSTIN,[Location Address],[Vendor Group Code],[Vendor Group Description],[Vendor Code],[Vendor Name],[Vendor Address],[Item Code],[Item Name],sum(COALESCE([FAT KG],0)) as [Total FAT KG],sum(COALESCE([SNF KG],0)) as [Total SNF KG],sum([Purchase Amount]) as [Amount Before Tax],sum([Additional Amount]) as [Total Additional Amount],sum([Total Tax Amount]) as [Total Tax Amount],sum([Total Amount] ) as [Total Purchase Amount],max(HSN_Code) as HSN_Code,max(GSTFinalNo) as GSTFinalNo,max(GST_Composition_scheme) as GST_Composition_scheme,max(GSTRegistered) as GSTRegistered,max(VendorCityCode) as VendorCityCode,max(VendorCityName) as VendorCityName,max (Vendor_GST_STATE_Code) as Vendor_GST_STATE_Code,max(Veindor_STATE_Name) as Veindor_STATE_Name from (" & strMain & ") as Final group by [Location Code],[Location Address],[Location Name],[Vendor Group Code],[Vendor Group Description],[Item Code],[Item Name],[Vendor Code],[Vendor Name],[Vendor Address]"
            ElseIf ddlReportType.SelectedValue = "Document Wise" Then
                'If chkQuickLoad.Checked Then
                '    strRunQuery = "select [Document No],[Document Date],[GR No],[LR No],[Way Bill No],[Trans Type],[Location Code],[Location Name],max([Location State GST]) as [Location State GST],max([Location State Code]) as [Location State Code],max([Location State Name]) as [Location State Name],max(VendorCityCode) as VendorCityCode,max(VendorCityName) as VendorCityName,max(Location_GSTIN) as Location_GSTIN,[Location Address],[Vendor Group Code],[Vendor Group Description],[Vendor Code],[Vendor Name],[Vendor Address],[Vendor TIN No],max(GSTFinalNo) as GSTFinalNo,max(GST_Composition_scheme) as GST_Composition_scheme,max(GSTRegistered) as GSTRegistered,max (Vendor_GST_STATE_Code) as Vendor_GST_STATE_Code,max(Veindor_STATE_Name) as Veindor_STATE_Name,sum(COALESCE([FAT KG],0)) as [Total FAT KG],sum(COALESCE([SNF KG],0)) as [Total SNF KG],sum([Amount]) as [Amount Before Discount],Sum([Discount AMount]) as [Discount Amount],sum([Purchase Amount]) as [Amount Before Tax],sum([Additional Amount]) as [Total Additional Amount],sum([Total Tax Amount]) as [Total Tax Amount],sum([Total Amount]-[Total Tax Amount]) as [Total Purchase Amount],sum([Total Amount] ) as [Total Amount],max([Vendor Invoice No]) as [Vendor Invoice No],Max([AP Document No]) as [AP Document No],Max([Against Invoice No]) as [Against Invoice No],Max([AP Total Tax]) as [AP Total Tax],Sum([AP Total Add Charge]) as [AP Total Add Charge],Max([AP Landed Amt]) as [AP Landed Amt],Max([AP Document Total]) as [AP Document Total],max(Purchase_Tax_Invoice) as Purchase_Tax_Invoice,sum([Exempted Amount]) as [Exempted Amount],sum([NILL Rate Amount]) as [NILL Rate Amount],sum([Composition Amount]) as [Composition Amount],sum([NILL Rate Amount]) as [NILL Rate Amount],sum([Exempted Amount]) as [Exempted Amount],sum([Non-GST Amount]) as [Non-GST Amount],max([Import Type]) as [Import Type],max([Import Bill of Entry No]) as [Import Bill of Entry No],max([Import Bill of Entry Date]) as [Import Bill of Entry Date],sum([Import Bill of Entry Amount]) as [Import Bill of Entry Amount],max([Original Invoice No]) as [Original Invoice No],max([Original Invoice Date]) as [Original Invoice Date],max([Reason For Revision]) as [Reason For Revision],max([ITC Eligible]) as [ITC Eligible],max([ITC Status]) as [ITC Status],max([ITC Details]) as [ITC Details] from (" & strMain & ") as Final group by [Document No],[Location Code],[Location Name],[Location Address],[Vendor Group Code],[Vendor Group Description],[Vendor Code],[Vendor Name],[Vendor Address],[Document Date],[Trans Type],[Vendor TIN No],[GR No],[LR No],[Way Bill No]   "
                'Else
                '    strRunQuery = "select [Document No],[Document Date],[GR No],[LR No],[Way Bill No],[Trans Type],[Location Code],[Location Name],max([Location State GST]) as [Location State GST],max([Location State Code]) as [Location State Code],max([Location State Name]) as [Location State Name],max(VendorCityCode) as VendorCityCode,max(VendorCityName) as VendorCityName,max(Location_GSTIN) as Location_GSTIN,[Location Address],[Vendor Group Code],[Vendor Group Description],[Vendor Code],[Vendor Name],[Vendor Address],[Vendor TIN No],max(GSTFinalNo) as GSTFinalNo,max(GST_Composition_scheme) as GST_Composition_scheme,max(GSTRegistered) as GSTRegistered,max (Vendor_GST_STATE_Code) as Vendor_GST_STATE_Code,max(Veindor_STATE_Name) as Veindor_STATE_Name,sum(COALESCE([FAT KG],0)) as [Total FAT KG],sum(COALESCE([SNF KG],0)) as [Total SNF KG],sum([Amount]) as [Amount Before Discount],Sum([Discount AMount]) as [Discount Amount],sum([Purchase Amount]) as [Amount Before Tax],sum([Additional Amount]) as [Total Additional Amount],sum([Total Tax Amount]) as [Total Tax Amount],sum([Total Amount]-[Total Tax Amount]) as [Total Purchase Amount],sum([Total Amount] ) as [Total Amount],max([Vendor Invoice No]) as [Vendor Invoice No],Max([AP Document No]) as [AP Document No],Max([Against Invoice No]) as [Against Invoice No],Max([AP Total Tax]) as [AP Total Tax],Sum([AP Total Add Charge]) as [AP Total Add Charge],Max([AP Landed Amt]) as [AP Landed Amt],Max([AP Document Total]) as [AP Document Total],max(Purchase_Tax_Invoice) as Purchase_Tax_Invoice,sum([Exempted Amount]) as [Exempted Amount],sum([NILL Rate Amount]) as [NILL Rate Amount],sum([Composition Amount]) as [Composition Amount],sum([NILL Rate Amount]) as [NILL Rate Amount],sum([Exempted Amount]) as [Exempted Amount],sum([Non-GST Amount]) as [Non-GST Amount],max([Import Type]) as [Import Type],max([Import Bill of Entry No]) as [Import Bill of Entry No],max([Import Bill of Entry Date]) as [Import Bill of Entry Date],sum([Import Bill of Entry Amount]) as [Import Bill of Entry Amount],max([Original Invoice No]) as [Original Invoice No],max([Original Invoice Date]) as [Original Invoice Date],max([Reason For Revision]) as [Reason For Revision],max([ITC Eligible]) as [ITC Eligible],max([ITC Status]) as [ITC Status],max([ITC Details]) as [ITC Details],max(DocDateView) as DocDateView from (" & strMain & ") as Final group by [Document No],[Location Code],[Location Name],[Location Address],[Vendor Group Code],[Vendor Group Description],[Vendor Code],[Vendor Name],[Vendor Address],[Document Date],[Trans Type],[Vendor TIN No],[GR No],[LR No],[Way Bill No]  order by DocDateView,[Document No] "
                'End If
                ''richa KDI/22/10/18-000439
                'If chkQuickLoad.Checked Then
                '    strRunQuery = "select [Document No],[Document Date],[GR No],[LR No],[Way Bill No],[Trans Type],[Location Code],[Location Name],max([Location State GST]) as [Location State GST],max([Location State Code]) as [Location State Code],max([Location State Name]) as [Location State Name],max(VendorCityCode) as VendorCityCode,max(VendorCityName) as VendorCityName,max(Location_GSTIN) as Location_GSTIN,[Location Address],[Vendor Group Code],[Vendor Group Description],[Vendor Code],[Vendor Name],[Vendor Address],[Vendor TIN No],max(GSTFinalNo) as GSTFinalNo,max(GST_Composition_scheme) as GST_Composition_scheme,max(GSTRegistered) as GSTRegistered,max (Vendor_GST_STATE_Code) as Vendor_GST_STATE_Code,max(Veindor_STATE_Name) as Veindor_STATE_Name,sum(COALESCE([FAT KG],0)) as [Total FAT KG],sum(COALESCE([SNF KG],0)) as [Total SNF KG],sum([Amount]) as [Amount Before Discount],Sum([Discount AMount]) as [Discount Amount],sum([Purchase Amount]) as [Amount Before Tax],sum([Additional Amount]) as [Total Additional Amount],case when [Trans Type] in ('Transfer Return','Transfer') then sum([Total Amount]) else  sum([Total Amount]-[Total Tax Amount]) end  as [Total Purchase Amount],sum([Total Tax Amount]) as [Total Tax Amount],case when [Trans Type] in ('Transfer Return','Transfer') then sum([Total Amount]+[Total Tax Amount]) else sum([Total Amount] ) end as [Total Amount],max([Vendor Invoice No]) as [Vendor Invoice No],Max([AP Document No]) as [AP Document No],Max([Against Invoice No]) as [Against Invoice No],Max([AP Total Tax]) as [AP Total Tax],Sum([AP Total Add Charge]) as [AP Total Add Charge],Max([AP Landed Amt]) as [AP Landed Amt],Max([AP Document Total]) as [AP Document Total],max(Purchase_Tax_Invoice) as Purchase_Tax_Invoice,sum([Exempted Amount]) as [Exempted Amount],sum([NILL Rate Amount]) as [NILL Rate Amount],sum([Composition Amount]) as [Composition Amount],sum([NILL Rate Amount]) as [NILL Rate Amount],sum([Exempted Amount]) as [Exempted Amount],sum([Non-GST Amount]) as [Non-GST Amount],max([Import Type]) as [Import Type],max([Import Bill of Entry No]) as [Import Bill of Entry No],max([Import Bill of Entry Date]) as [Import Bill of Entry Date],sum([Import Bill of Entry Amount]) as [Import Bill of Entry Amount],max([Original Invoice No]) as [Original Invoice No],max([Original Invoice Date]) as [Original Invoice Date],max([Reason For Revision]) as [Reason For Revision],max([ITC Eligible]) as [ITC Eligible],max([ITC Status]) as [ITC Status],max([ITC Details]) as [ITC Details] from (" & strMain & ") as Final group by [Document No],[Location Code],[Location Name],[Location Address],[Vendor Group Code],[Vendor Group Description],[Vendor Code],[Vendor Name],[Vendor Address],[Document Date],[Trans Type],[Vendor TIN No],[GR No],[LR No],[Way Bill No]   "
                'Else
                '    strRunQuery = "select [Document No],[Document Date],[GR No],[LR No],[Way Bill No],[Trans Type],[Location Code],[Location Name],max([Location State GST]) as [Location State GST],max([Location State Code]) as [Location State Code],max([Location State Name]) as [Location State Name],max(VendorCityCode) as VendorCityCode,max(VendorCityName) as VendorCityName,max(Location_GSTIN) as Location_GSTIN,[Location Address],[Vendor Group Code],[Vendor Group Description],[Vendor Code],[Vendor Name],[Vendor Address],[Vendor TIN No],max(GSTFinalNo) as GSTFinalNo,max(GST_Composition_scheme) as GST_Composition_scheme,max(GSTRegistered) as GSTRegistered,max (Vendor_GST_STATE_Code) as Vendor_GST_STATE_Code,max(Veindor_STATE_Name) as Veindor_STATE_Name,sum(COALESCE([FAT KG],0)) as [Total FAT KG],sum(COALESCE([SNF KG],0)) as [Total SNF KG],sum([Amount]) as [Amount Before Discount],Sum([Discount AMount]) as [Discount Amount],sum([Purchase Amount]) as [Amount Before Tax],sum([Additional Amount]) as [Total Additional Amount],case when [Trans Type] in ('Transfer Return','Transfer') then sum([Total Amount]) else sum([Total Amount]-[Total Tax Amount]) end  as [Total Purchase Amount],sum([Total Tax Amount]) as [Total Tax Amount], case when [Trans Type] in ('Transfer Return','Transfer') then sum([Total Amount]+[Total Tax Amount]) else sum([Total Amount] ) end  as [Total Amount],max([Vendor Invoice No]) as [Vendor Invoice No],Max([AP Document No]) as [AP Document No],Max([Against Invoice No]) as [Against Invoice No],Max([AP Total Tax]) as [AP Total Tax],Sum([AP Total Add Charge]) as [AP Total Add Charge],Max([AP Landed Amt]) as [AP Landed Amt],Max([AP Document Total]) as [AP Document Total],max(Purchase_Tax_Invoice) as Purchase_Tax_Invoice,sum([Exempted Amount]) as [Exempted Amount],sum([NILL Rate Amount]) as [NILL Rate Amount],sum([Composition Amount]) as [Composition Amount],sum([NILL Rate Amount]) as [NILL Rate Amount],sum([Exempted Amount]) as [Exempted Amount],sum([Non-GST Amount]) as [Non-GST Amount],max([Import Type]) as [Import Type],max([Import Bill of Entry No]) as [Import Bill of Entry No],max([Import Bill of Entry Date]) as [Import Bill of Entry Date],sum([Import Bill of Entry Amount]) as [Import Bill of Entry Amount],max([Original Invoice No]) as [Original Invoice No],max([Original Invoice Date]) as [Original Invoice Date],max([Reason For Revision]) as [Reason For Revision],max([ITC Eligible]) as [ITC Eligible],max([ITC Status]) as [ITC Status],max([ITC Details]) as [ITC Details],max(DocDateView) as DocDateView from (" & strMain & ") as Final group by [Document No],[Location Code],[Location Name],[Location Address],[Vendor Group Code],[Vendor Group Description],[Vendor Code],[Vendor Name],[Vendor Address],[Document Date],[Trans Type],[Vendor TIN No],[GR No],[LR No],[Way Bill No]  order by DocDateView,[Document No] "
                'End If

                If chkQuickLoad.Checked Then
                    strRunQuery = "select [Document No],[Document Date],[GR No],[LR No],[Way Bill No],[Trans Type],[Location Code],[Location Name],max([Location State GST]) as [Location State GST],max([Location State Code]) as [Location State Code],max([Location State Name]) as [Location State Name],max(VendorCityCode) as VendorCityCode,max(VendorCityName) as VendorCityName,max(Location_GSTIN) as Location_GSTIN,[Location Address],[Vendor Group Code],[Vendor Group Description],[Vendor Code],[Vendor Name],[Vendor Address],[Vendor TIN No],max(GSTFinalNo) as GSTFinalNo,max(GST_Composition_scheme) as GST_Composition_scheme,max(GSTRegistered) as GSTRegistered,max (Vendor_GST_STATE_Code) as Vendor_GST_STATE_Code,max(Veindor_STATE_Name) as Veindor_STATE_Name,sum(COALESCE([FAT KG],0)) as [Total FAT KG],sum(COALESCE([SNF KG],0)) as [Total SNF KG],sum([Amount]) as [Amount Before Discount],Sum([Discount AMount]) as [Discount Amount],sum([Purchase Amount]) as [Amount Before Tax],sum([Additional Amount]) as [Total Additional Amount], sum([Total Amount]-[Total Tax Amount]) as [Total Purchase Amount],sum([Total Tax Amount]) as [Total Tax Amount], sum([Total Amount] ) as [Total Amount],max([Vendor Invoice No]) as [Vendor Invoice No],Max([AP Document No]) as [AP Document No],Max([Against Invoice No]) as [Against Invoice No],Max([AP Total Tax]) as [AP Total Tax],Sum([AP Total Add Charge]) as [AP Total Add Charge],Max([AP Landed Amt]) as [AP Landed Amt],Max([AP Document Total]) as [AP Document Total],max(Purchase_Tax_Invoice) as Purchase_Tax_Invoice,sum([Exempted Amount]) as [Exempted Amount],sum([NILL Rate Amount]) as [NILL Rate Amount],sum([Composition Amount]) as [Composition Amount],sum([NILL Rate Amount]) as [NILL Rate Amount],sum([Exempted Amount]) as [Exempted Amount],sum([Non-GST Amount]) as [Non-GST Amount],max([Import Type]) as [Import Type],max([Import Bill of Entry No]) as [Import Bill of Entry No],max([Import Bill of Entry Date]) as [Import Bill of Entry Date],sum([Import Bill of Entry Amount]) as [Import Bill of Entry Amount],max([Original Invoice No]) as [Original Invoice No],max([Original Invoice Date]) as [Original Invoice Date],max([Reason For Revision]) as [Reason For Revision],max([ITC Eligible]) as [ITC Eligible],max([ITC Status]) as [ITC Status],max([ITC Details]) as [ITC Details] from (" & strMain & ") as Final group by [Document No],[Location Code],[Location Name],[Location Address],[Vendor Group Code],[Vendor Group Description],[Vendor Code],[Vendor Name],[Vendor Address],[Document Date],[Trans Type],[Vendor TIN No],[GR No],[LR No],[Way Bill No]   "
                Else
                    strRunQuery = "select [Document No],[Document Date],[GR No],[LR No],[Way Bill No],[Trans Type],[Location Code],[Location Name],max([Location State GST]) as [Location State GST],max([Location State Code]) as [Location State Code],max([Location State Name]) as [Location State Name],max(VendorCityCode) as VendorCityCode,max(VendorCityName) as VendorCityName,max(Location_GSTIN) as Location_GSTIN,[Location Address],[Vendor Group Code],[Vendor Group Description],[Vendor Code],[Vendor Name],[Vendor Address],[Vendor TIN No],max(GSTFinalNo) as GSTFinalNo,max(GST_Composition_scheme) as GST_Composition_scheme,max(GSTRegistered) as GSTRegistered,max (Vendor_GST_STATE_Code) as Vendor_GST_STATE_Code,max(Veindor_STATE_Name) as Veindor_STATE_Name,sum(COALESCE([FAT KG],0)) as [Total FAT KG],sum(COALESCE([SNF KG],0)) as [Total SNF KG],sum([Amount]) as [Amount Before Discount],Sum([Discount AMount]) as [Discount Amount],sum([Purchase Amount]) as [Amount Before Tax],sum([Additional Amount]) as [Total Additional Amount], sum([Total Amount]-[Total Tax Amount]) as [Total Purchase Amount],sum([Total Tax Amount]) as [Total Tax Amount], sum([Total Amount] ) as [Total Amount],max([Vendor Invoice No]) as [Vendor Invoice No],Max([AP Document No]) as [AP Document No],Max([Against Invoice No]) as [Against Invoice No],Max([AP Total Tax]) as [AP Total Tax],Sum([AP Total Add Charge]) as [AP Total Add Charge],Max([AP Landed Amt]) as [AP Landed Amt],Max([AP Document Total]) as [AP Document Total],max(Purchase_Tax_Invoice) as Purchase_Tax_Invoice,sum([Exempted Amount]) as [Exempted Amount],sum([NILL Rate Amount]) as [NILL Rate Amount],sum([Composition Amount]) as [Composition Amount],sum([NILL Rate Amount]) as [NILL Rate Amount],sum([Exempted Amount]) as [Exempted Amount],sum([Non-GST Amount]) as [Non-GST Amount],max([Import Type]) as [Import Type],max([Import Bill of Entry No]) as [Import Bill of Entry No],max([Import Bill of Entry Date]) as [Import Bill of Entry Date],sum([Import Bill of Entry Amount]) as [Import Bill of Entry Amount],max([Original Invoice No]) as [Original Invoice No],max([Original Invoice Date]) as [Original Invoice Date],max([Reason For Revision]) as [Reason For Revision],max([ITC Eligible]) as [ITC Eligible],max([ITC Status]) as [ITC Status],max([ITC Details]) as [ITC Details],max(DocDateView) as DocDateView from (" & strMain & ") as Final group by [Document No],[Location Code],[Location Name],[Location Address],[Vendor Group Code],[Vendor Group Description],[Vendor Code],[Vendor Name],[Vendor Address],[Document Date],[Trans Type],[Vendor TIN No],[GR No],[LR No],[Way Bill No]  order by DocDateView,[Document No] "
                End If
            ElseIf ddlReportType.SelectedValue = "Document Detail" Then
                If clsCommon.myLen(StrGLAccount) > 0 Then
                    strMain += " and TabPurchaseGLCode.Account_Code='" + StrGLAccount + "'"
                End If

                If chkQuickLoad.Checked Then
                    strRunQuery = strMain & "  " ''order by  convert(date,[Document Date],103),[Document No]
                Else
                    strRunQuery = strMain & " order by  DocDateView,[Document No] " ''order by  convert(date,[Document Date],103),[Document No]
                End If

            ElseIf ddlReportType.SelectedValue = "Document Type Info" Then
                strRunQuery = strMainForDocumentInfo & " order by  convert(date,[Document Date],103), [Document No] "
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "GL Account Wise") = CompairStringResult.Equal Then
                strRunQuery = "select [Inventory Account Code],max([Inventory Account Name]) as [Inventory Account Name],sum(Landed_Cost_Amount) as Landed_Cost_Amount    from ( (" & strMain & ") )xxx group by xxx.[Inventory Account Code] having len(isnull( [Inventory Account Code],''))>0 "
            End If
            '' bulk export
            If BulkExport = 1 Then
                If ddlReportType.SelectedValue = "Total Purchase" Then
                    strRunQuery = "select * from (" & strRunQuery & ") PP order by [Total FAT KG]"
                    transportSql.BulkExport("Purchase_Register_" & ddlReportType.SelectedValue, strRunQuery, "order by [Total FAT KG] ", "csv")
                    Exit Sub

                ElseIf ddlReportType.SelectedValue = "Location Wise" Then
                    strRunQuery = "select * from (" & strRunQuery & ") PP order by [Location Code],[Location Name],[Location Address]]"
                    transportSql.BulkExport("Purchase_Register_" & ddlReportType.SelectedValue, strRunQuery, "order by  [Location Code],[Location Name],[Location Address] ", "csv")
                    Exit Sub
                ElseIf ddlReportType.SelectedValue = "Vendor Group Wise" Then
                    strRunQuery = "select * from (" & strRunQuery & ") PP order by [Location Code],[Location Address],[Location Name],[Vendor Group Code],[Vendor Group Description]"
                    transportSql.BulkExport("Purchase_Register_" & ddlReportType.SelectedValue, strRunQuery, "order by  [Location Code],[Location Address],[Location Name],[Vendor Group Code],[Vendor Group Description] ", "csv")
                    Exit Sub
                ElseIf ddlReportType.SelectedValue = "Item Wise" Then
                    strRunQuery = "select * from (" & strRunQuery & ") PP order by [Location Code],[Location Address],[Location Name],[Vendor Group Code],[Vendor Group Description],[Item Code],[Item Name]"
                    transportSql.BulkExport("Purchase_Register_" & ddlReportType.SelectedValue, strRunQuery, "order by  [Location Code],[Location Address],[Location Name],[Vendor Group Code],[Vendor Group Description],[Item Code],[Item Name] ", "csv")
                    Exit Sub
                ElseIf ddlReportType.SelectedValue = "Vendor Wise" Then
                    strRunQuery = "select * from (" & strRunQuery & ") PP order by [Location Code],[Location Address],[Location Name],[Vendor Group Code],[Vendor Group Description],[Item Code],[Item Name],[Vendor Code],[Vendor Name],[Vendor Address]"
                    transportSql.BulkExport("Purchase_Register_" & ddlReportType.SelectedValue, strRunQuery, "order by  [Location Code],[Location Address],[Location Name],[Vendor Group Code],[Vendor Group Description],[Item Code],[Item Name],[Vendor Code],[Vendor Name],[Vendor Address] ", "csv")
                    Exit Sub
                ElseIf ddlReportType.SelectedValue = "Document Wise" Then

                    transportSql.BulkExport("Purchase_Register_" & ddlReportType.SelectedValue, strRunQuery, "order by  convert(date,[Document Date],103),[Document No] ", "csv")
                    Exit Sub
                ElseIf ddlReportType.SelectedValue = "Document Detail" Then

                    transportSql.BulkExport("Purchase_Register_" & ddlReportType.SelectedValue, strRunQuery, "order by  convert(date,[Document Date],103),[Document No] ", "csv")
                    Exit Sub
                ElseIf ddlReportType.SelectedValue = "Document Type Info" Then
                    transportSql.BulkExport("Purchase_Register_" & ddlReportType.SelectedValue, strRunQuery, "order by  convert(date,[Document Date],103), [Document No] ", "csv")
                    Exit Sub
                End If


            ElseIf BulkExport = 2 Then
                If ddlReportType.SelectedValue = "Total Purchase" Then
                    strRunQuery = "select * from (" & strRunQuery & ") PP order by [Total FAT KG]"
                    transportSql.BulkExport("Purchase_Register_" & ddlReportType.SelectedValue, strRunQuery, "order by [Total FAT KG] ", "xls")
                    Exit Sub

                ElseIf ddlReportType.SelectedValue = "Location Wise" Then
                    strRunQuery = "select * from (" & strRunQuery & ") PP order by [Location Code],[Location Name],[Location Address]]"
                    transportSql.BulkExport("Purchase_Register_" & ddlReportType.SelectedValue, strRunQuery, "order by  [Location Code],[Location Name],[Location Address] ", "xls")
                    Exit Sub
                ElseIf ddlReportType.SelectedValue = "Vendor Group Wise" Then
                    strRunQuery = "select * from (" & strRunQuery & ") PP order by [Location Code],[Location Address],[Location Name],[Vendor Group Code],[Vendor Group Description]"
                    transportSql.BulkExport("Purchase_Register_" & ddlReportType.SelectedValue, strRunQuery, "order by  [Location Code],[Location Address],[Location Name],[Vendor Group Code],[Vendor Group Description] ", "xls")
                    Exit Sub
                ElseIf ddlReportType.SelectedValue = "Item Wise" Then
                    strRunQuery = "select * from (" & strRunQuery & ") PP order by [Location Code],[Location Address],[Location Name],[Vendor Group Code],[Vendor Group Description],[Item Code],[Item Name]"
                    transportSql.BulkExport("Purchase_Register_" & ddlReportType.SelectedValue, strRunQuery, "order by  [Location Code],[Location Address],[Location Name],[Vendor Group Code],[Vendor Group Description],[Item Code],[Item Name] ", "xls")
                    Exit Sub
                ElseIf ddlReportType.SelectedValue = "Vendor Wise" Then
                    strRunQuery = "select * from (" & strRunQuery & ") PP order by [Location Code],[Location Address],[Location Name],[Vendor Group Code],[Vendor Group Description],[Item Code],[Item Name],[Vendor Code],[Vendor Name],[Vendor Address]"
                    transportSql.BulkExport("Purchase_Register_" & ddlReportType.SelectedValue, strRunQuery, "order by  [Location Code],[Location Address],[Location Name],[Vendor Group Code],[Vendor Group Description],[Item Code],[Item Name],[Vendor Code],[Vendor Name],[Vendor Address] ", "xls")
                    Exit Sub
                ElseIf ddlReportType.SelectedValue = "Document Wise" Then
                    transportSql.BulkExport("Purchase_Register_" & ddlReportType.SelectedValue, strRunQuery, "order by convert(date,[Document Date],103), [Document No] ", "xls")
                    Exit Sub
                ElseIf ddlReportType.SelectedValue = "Document Detail" Then
                    transportSql.BulkExport("Purchase_Register_" & ddlReportType.SelectedValue, strRunQuery, "order by  convert(date,[Document Date],103),[Document No] ", "xls")
                    Exit Sub
                ElseIf ddlReportType.SelectedValue = "Document Type Info" Then
                    transportSql.BulkExport("Purchase_Register_" & ddlReportType.SelectedValue, strRunQuery, "order by  convert(date,[Document Date],103), [Document No] ", "xls")
                    Exit Sub
                End If
            End If

            RadPageViewPage2.Text = ddlReportType.SelectedValue
            dt = clsDBFuncationality.GetDataTable(strRunQuery)
            Gv1.DataSource = Nothing
            Gv1.Columns.Clear()
            Gv1.Rows.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.EnableFiltering = True
            Gv1.Tag = ddlReportType.SelectedValue
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            Else
                EnableDisableAllControl(False)
                Gv1.DataSource = dt
                SetGridFormationOFGV1()
            End If
            'FindAndRestoreGridLayout(Me)
            PageSetupReport_ID = clsERPFuncationality.GetReportID(MyBase.Form_ID, ddlReportType.Text)
            ReStoreGridLayout()
            Gv1.MasterTemplate.AllowAddNewRow = False
            RadPageView1.SelectedPage = RadPageViewPage2
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            clsCommon.ProgressBarHide()
        End Try
    End Sub

    Sub EnableDisableAllControl(ByVal val As Boolean)
        txtUOM.Enabled = val
        ddlReportType.Enabled = val
        txtTransaction.Enabled = val
        txtState.Enabled = val
        txtLocation.Enabled = val
        txtItem.Enabled = val
        txtCustGroup.Enabled = val
        txtCustomer.Enabled = val
        RadGroupBox6.Enabled = val
        chkSerializeInv.Enabled = val
        RadGroupBox3.Enabled = val
        RadGroupBox7.Enabled = val
        chkStockingUOM.Enabled = val
    End Sub

    Sub SetGridFormationOFGV1()
        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = False
            If ddlReportType.SelectedValue = "Document Detail" AndAlso Gv1.Columns(ii).Name.Contains("TCS%") = True Then
                Gv1.Columns(ii).FormatString = "{0:n3}"
            ElseIf Not clsCommon.CompairString(Gv1.Columns(ii).Name, "DocDateView") = CompairStringResult.Equal Then
                Gv1.Columns(ii).FormatString = "{0:n2}"
            End If
        Next

        If ddlReportType.SelectedValue = "Document Detail" Then
            Gv1.Columns("Trans Type").IsVisible = True
            Gv1.Columns("Trans Type").Width = 70
            Gv1.Columns("Trans Type").HeaderText = "Trans Type"

            Gv1.Columns("Landed_Cost_Amount").IsVisible = True
            Gv1.Columns("Landed_Cost_Amount").Width = 70
            Gv1.Columns("Landed_Cost_Amount").HeaderText = "Landed Amount"

            Gv1.Columns("HSN_Code").HeaderText = "HSN Code"
            Gv1.Columns("GST_Composition_scheme").HeaderText = "GST Composition Vendor"
            Gv1.Columns("GSTRegistered").HeaderText = "GST Registered Vendor"
            Gv1.Columns("GSTFinalNo").HeaderText = "GSTIN"
            Gv1.Columns("Vendor_GST_STATE_Code").HeaderText = "GST State Code"
            Gv1.Columns("Veindor_STATE_Name").HeaderText = "State Name"
            Gv1.Columns("Location_GSTIN").HeaderText = "Location GSTIN"
            Gv1.Columns("VendorCityCode").HeaderText = "Place of Supply Code"
            Gv1.Columns("VendorCityName").HeaderText = "Place of Supply"
            Gv1.Columns("Purchase_Tax_Invoice").HeaderText = "Purchase Tax Invoice"
            For i As Integer = 0 To Gv1.Columns.Count - 1
                Gv1.Columns(i).BestFit()
                Gv1.Columns(i).IsVisible = True
                If Gv1.Columns(i).Name.Contains("Code") = True And Not (Gv1.Columns(i).Name.Contains("Item Code") = False Or Gv1.Columns(i).Name.Contains("Vendor Code") = False Or Gv1.Columns(i).Name.Contains("Location Code") = False) Then
                    Gv1.Columns(i).IsVisible = False
                ElseIf Gv1.Columns(i).Name.Contains("AP ") = True Or Gv1.Columns(i).Name.Contains("Against Invoice No") = True Then
                    Gv1.Columns(i).IsVisible = False
                Else
                    Gv1.Columns(i).IsVisible = True
                End If
            Next
        ElseIf ddlReportType.SelectedValue = "Document Type Info" Then
            Gv1.Columns("Trans Type").IsVisible = True
            Gv1.Columns("Trans Type").Width = 70
            Gv1.Columns("Trans Type").HeaderText = "Trans Type"

            Gv1.Columns("HSN_Code").HeaderText = "HSN Code"
            Gv1.Columns("GST_Composition_scheme").HeaderText = "GST Composition Vendor"
            Gv1.Columns("GSTRegistered").HeaderText = "GST Registered Vendor"
            Gv1.Columns("GSTFinalNo").HeaderText = "GSTIN"
            Gv1.Columns("Vendor_GST_STATE_Code").HeaderText = "GST State Code"
            Gv1.Columns("Veindor_STATE_Name").HeaderText = "State Name"
            Gv1.Columns("Location_GSTIN").HeaderText = "Location GSTIN"
            Gv1.Columns("VendorCityCode").HeaderText = "Place of Supply Code"
            Gv1.Columns("VendorCityName").HeaderText = "Place of Supply"
            Gv1.Columns("Purchase_Tax_Invoice").HeaderText = "Purchase Tax Invoice"
            For i As Integer = 0 To Gv1.Columns.Count - 1
                Gv1.Columns(i).BestFit()
                Gv1.Columns(i).IsVisible = True
                If Gv1.Columns(i).Name.Contains("Code") = True And Not (Gv1.Columns(i).Name.Contains("Item Code") = False Or Gv1.Columns(i).Name.Contains("Vendor Code") = False Or Gv1.Columns(i).Name.Contains("Location Code") = False) Then
                    Gv1.Columns(i).IsVisible = False
                ElseIf Gv1.Columns(i).Name.Contains("AP ") = True Or Gv1.Columns(i).Name.Contains("Against Invoice No") = True Then
                    Gv1.Columns(i).IsVisible = False
                Else
                    Gv1.Columns(i).IsVisible = True
                End If
            Next
        ElseIf ddlReportType.SelectedValue = "Document Wise" Then
            For i As Integer = 0 To Gv1.Columns.Count - 1
                Gv1.Columns(i).BestFit()
                Gv1.Columns(i).IsVisible = True
                If Gv1.Columns(i).Name.Contains("Code") = True And Not (Gv1.Columns(i).Name.Contains("Item Code") = False Or Gv1.Columns(i).Name.Contains("Vendor Code") = False Or Gv1.Columns(i).Name.Contains("Location Code") = False) Then
                    Gv1.Columns(i).IsVisible = False
                ElseIf Gv1.Columns(i).Name.Contains("AP ") = True Or Gv1.Columns(i).Name.Contains("Against Invoice No") = True Then
                    Gv1.Columns(i).IsVisible = False
                Else
                    Gv1.Columns(i).IsVisible = True
                End If
            Next

            Gv1.Columns("GSTFinalNo").HeaderText = "GSTIN"
            Gv1.Columns("Vendor_GST_STATE_Code").HeaderText = "GST State Code"
            Gv1.Columns("Veindor_STATE_Name").HeaderText = "State Name"
            Gv1.Columns("Location_GSTIN").HeaderText = "Location GSTIN"
            Gv1.Columns("GST_Composition_scheme").HeaderText = "GST Composition Vendor"
            Gv1.Columns("GSTRegistered").HeaderText = "GST Registered Vendor"
            Gv1.Columns("VendorCityCode").HeaderText = "Place of Supply Code"
            Gv1.Columns("VendorCityName").HeaderText = "Place of Supply"
            Gv1.Columns("Purchase_Tax_Invoice").HeaderText = "Purchase Tax Invoice"
            If chkQuickLoad.Checked = False Then
                Gv1.Columns("DocDateView").IsVisible = False
            End If

        ElseIf ddlReportType.SelectedValue = "Total Purchase" Then
            Gv1.Columns("Total FAT KG").IsVisible = True
            Gv1.Columns("Total SNF KG").IsVisible = True
            Gv1.Columns("Total Purchase Amount").IsVisible = True
            For i As Integer = 0 To Gv1.Columns.Count - 1
                Gv1.Columns(i).BestFit()
                Gv1.Columns(i).IsVisible = True
                If Gv1.Columns(i).Name.Contains("Code") = True And Not (Gv1.Columns(i).Name.Contains("Item Code") = False Or Gv1.Columns(i).Name.Contains("Vendor Code") = False Or Gv1.Columns(i).Name.Contains("Location Code") = False) Then
                    Gv1.Columns(i).IsVisible = False
                End If
            Next
        ElseIf ddlReportType.SelectedValue = "Location Wise" Then
            Gv1.Columns("Total FAT KG").IsVisible = True
            Gv1.Columns("Total SNF KG").IsVisible = True
            Gv1.Columns("Total Purchase Amount").IsVisible = True
            Gv1.Columns("Location Code").IsVisible = True
            Gv1.Columns("Location Name").IsVisible = True
            Gv1.Columns("Location_GSTIN").IsVisible = True
            For i As Integer = 0 To Gv1.Columns.Count - 1
                Gv1.Columns(i).BestFit()
                Gv1.Columns(i).IsVisible = True
                If Gv1.Columns(i).Name.Contains("Code") = True And Not (Gv1.Columns(i).Name.Contains("Item Code") = False Or Gv1.Columns(i).Name.Contains("Vendor Code") = False Or Gv1.Columns(i).Name.Contains("Location Code") = False) Then
                    Gv1.Columns(i).IsVisible = False
                End If
            Next
            Gv1.Columns("Location_GSTIN").HeaderText = "Location GSTIN"
        ElseIf ddlReportType.SelectedValue = "Vendor Group Wise" Then
            Gv1.Columns("Total FAT KG").IsVisible = True
            Gv1.Columns("Total SNF KG").IsVisible = True
            Gv1.Columns("Total Purchase Amount").IsVisible = True
            Gv1.Columns("Location Code").IsVisible = True
            Gv1.Columns("Location Name").IsVisible = True
            Gv1.Columns("Location_GSTIN").IsVisible = True
            Gv1.Columns("Vendor Group Code").IsVisible = True
            Gv1.Columns("Vendor Group Description").IsVisible = True
            For i As Integer = 0 To Gv1.Columns.Count - 1
                Gv1.Columns(i).BestFit()
                Gv1.Columns(i).IsVisible = True
                If Gv1.Columns(i).Name.Contains("Code") = True And Not (Gv1.Columns(i).Name.Contains("Item Code") = False Or Gv1.Columns(i).Name.Contains("Vendor Code") = False Or Gv1.Columns(i).Name.Contains("Location Code") = False) Then
                    Gv1.Columns(i).IsVisible = False
                End If
            Next
            Gv1.Columns("Location_GSTIN").HeaderText = "Location GSTIN"
        ElseIf ddlReportType.SelectedValue = "Item Wise" Then
            Gv1.Columns("Total FAT KG").IsVisible = True
            Gv1.Columns("Total SNF KG").IsVisible = True
            Gv1.Columns("Total Purchase Amount").IsVisible = True
            Gv1.Columns("Location Code").IsVisible = True
            Gv1.Columns("Location Name").IsVisible = True
            Gv1.Columns("Location_GSTIN").IsVisible = True
            Gv1.Columns("Vendor Group Code").IsVisible = True
            Gv1.Columns("Vendor Group Description").IsVisible = True
            Gv1.Columns("Item Code").IsVisible = True
            Gv1.Columns("Item Name").IsVisible = True
            For i As Integer = 0 To Gv1.Columns.Count - 1
                Gv1.Columns(i).BestFit()
                Gv1.Columns(i).IsVisible = True
                If Gv1.Columns(i).Name.Contains("Code") = True And Not (Gv1.Columns(i).Name.Contains("Item Code") = False Or Gv1.Columns(i).Name.Contains("Vendor Code") = False Or Gv1.Columns(i).Name.Contains("Location Code") = False) Then
                    Gv1.Columns(i).IsVisible = False
                End If
            Next

            Gv1.Columns("HSN_Code").HeaderText = "HSN Code"
            Gv1.Columns("Location_GSTIN").HeaderText = "Location GSTIN"


        ElseIf ddlReportType.SelectedValue = "Vendor Wise" Then
            Gv1.Columns("Total FAT KG").IsVisible = True
            Gv1.Columns("Total SNF KG").IsVisible = True
            Gv1.Columns("Total Purchase Amount").IsVisible = True
            Gv1.Columns("Location Code").IsVisible = True
            Gv1.Columns("Location Name").IsVisible = True
            Gv1.Columns("Location_GSTIN").IsVisible = True
            Gv1.Columns("Vendor Group Code").IsVisible = True
            Gv1.Columns("Vendor Group Description").IsVisible = True
            Gv1.Columns("Item Code").IsVisible = True
            Gv1.Columns("Item Name").IsVisible = True
            Gv1.Columns("Vendor Code").IsVisible = True
            Gv1.Columns("Vendor Name").IsVisible = True


            Gv1.Columns("GSTFinalNo").HeaderText = "GSTIN"
            Gv1.Columns("Vendor_GST_STATE_Code").HeaderText = "GST State Code"
            Gv1.Columns("Veindor_STATE_Name").HeaderText = "State Name"
            Gv1.Columns("Location_GSTIN").HeaderText = "Location GSTIN"
            Gv1.Columns("HSN_Code").HeaderText = "HSN Code"
            Gv1.Columns("GST_Composition_scheme").HeaderText = "GST Composition Vendor"
            Gv1.Columns("GSTRegistered").HeaderText = "GST Registered Vendor"
            Gv1.Columns("VendorCityCode").HeaderText = "Place of Supply Code"
            Gv1.Columns("VendorCityName").HeaderText = "Place of Supply"

            For i As Integer = 0 To Gv1.Columns.Count - 1
                Gv1.Columns(i).BestFit()
                Gv1.Columns(i).IsVisible = True

                If Gv1.Columns(i).Name.Contains("Code") = True And Not (Gv1.Columns(i).Name.Contains("Item Code") = False Or Gv1.Columns(i).Name.Contains("Vendor Code") = False Or Gv1.Columns(i).Name.Contains("Location Code") = False) Then
                    Gv1.Columns(i).IsVisible = False
                End If
            Next
        ElseIf ddlReportType.SelectedValue = "GL Account Wise" Then
            Gv1.Columns("Inventory Account Code").IsVisible = True
            Gv1.Columns("Inventory Account Name").IsVisible = True
            Gv1.Columns("Landed_Cost_Amount").IsVisible = True
            Gv1.Columns("Landed_Cost_Amount").HeaderText = "Landed Amount"

            For i As Integer = 0 To Gv1.Columns.Count - 1
                Gv1.Columns(i).BestFit()
            Next

        End If

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        For Each col As GridViewColumn In Gv1.Columns
            If clsCommon.CompairString(col.Name, "Total FAT KG") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "Total SNF KG") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "FAT KG") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "SNF KG") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "Quantity") = CompairStringResult.Equal Then
                Dim item1 As New GridViewSummaryItem(col.Name, "{0:F3}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)

            ElseIf col.Name.Contains("Landed_Cost_Amount") = True Or col.Name.Contains("Amount") = True Or col.Name.Contains("Amt") = True Or col.Name.Contains("Total") = True Or strPivotForFinalOuterQuery.Contains(col.Name) = True Or strPivotForAddChargeFinalOutersumQuery.Contains(col.Name) Then
                Dim item As New GridViewSummaryItem(col.Name, "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item)
            End If
        Next
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        RadPageView1.SelectedPage = RadPageViewPage2
        Gv1.AllowAddNewRow = False
        Gv1.BestFitColumns()
        Gv1.ShowGroupPanel = False
        Try
            Gv1.Columns("DocDateView").IsVisible = False
        Catch ex As Exception
        End Try
    End Sub

    Sub Reset()
        StrGLAccount = ""
        EnableDisableAllControl(True)
        Gv1.DataSource = Nothing
    End Sub

    Enum Exporter
        Excel = 0
        PDF = 1
        Print = 2
        Refresh = 3
    End Enum

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            Dim obj As New clsGridLayout()
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            obj = New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = Gv1.ColumnCount
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        End If
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = clsERPFuncationality.GetReportID(MyBase.Form_ID, ddlReportType.Text)
        TemplateGridview = Gv1
        Print(Exporter.Refresh)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub


    Private Sub rmSetting_Click(sender As Object, e As EventArgs) Handles rmSetting.Click
        Dim frm As New FrmMailSMSSettingNew2()
        frm.FormId = clsUserMgtCode.RptFreshSaleRegister1
        frm.ShowDialog()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub RptPurchaseRegisterReport_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            Print(Exporter.Refresh)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub

    Private Sub RptPurchaseRegisterReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Visible = False
        arrBack = New List(Of String)
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New ")
        GetMIS_ITem_GroupColumn()
        'If clsCommon.myLen(MIS_Item_Group) <= 0 Then
        '    clsCommon.MyMessageBoxShow("MIS Item Group Custom field is not create in Item Structure.")
        'End If
        chkQuickLoad.Checked = False
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        txtUOM.Value = ""
        LoadTypes()
        Document_No = ""
        ddlReportType.SelectedValue = "Total Purchase"
        LoadCategory()
        txtState.arrValueMember = Nothing
        txtLocation.arrValueMember = Nothing
        txtTransaction.arrValueMember = Nothing
        txtItem.arrValueMember = Nothing
        txtCustomer.arrValueMember = Nothing
        txtCustGroup.arrValueMember = Nothing
        rbtnCategoryAll.IsChecked = True
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
        chkStockingUOM.Enabled = True
        chkStockingUOM.Checked = False

        RadPageView1.SelectedPage = RadPageViewPage1
        Gv1.EnableGrouping = True
        Gv1.ShowGroupPanel = True
        If isDataLoad Then
            fromDate.Value = dtFrom
            ToDate.Value = dtTo
            txtUOM.Value = Unit_Code
            txtLocation.arrValueMember = arrLocation
            txtItem.arrValueMember = arrItem
            txtCustomer.arrValueMember = arrCustomer
            txtTransaction.arrValueMember = arrTransaction
            If arrCat IsNot Nothing AndAlso arrCat.Count > 0 Then
                rbtnCategorySelect.IsChecked = True
                For Each str As String In arrCat.Keys
                    For ii As Integer = 0 To gvCategory.RowCount - 1
                        If clsCommon.CompairString(clsCommon.myCstr(gvCategory.Rows(ii).Cells("CODE").Value), str) = CompairStringResult.Equal Then
                            gvCategory.Rows(ii).Cells("SEL").Value = True
                            gvCategory.Rows(ii).Tag = arrCat(str)
                        End If
                    Next
                Next
            End If
            ddlReportType.SelectedValue = strType
            Print(True)
            Me.Visible = True
        End If
    End Sub

    Private Sub chkAllType_ToggleStateChanged(sender As Object, args As StateChangedEventArgs)
        'cbgType.Enabled = chkselecttype.IsChecked
    End Sub

    Private Sub rbtnCategoryAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnCategoryAll.ToggleStateChanged
        gvCategory.Enabled = rbtnCategorySelect.IsChecked
    End Sub

    Private Sub chkAllProductType_ToggleStateChanged(sender As Object, args As StateChangedEventArgs)
        'cbgProductType.Enabled = chkSelectProductType.IsChecked
    End Sub

    Private Sub Gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        DrillDown()
    End Sub

    Sub DrillDown()
        Try
            If clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Total Purchase") = CompairStringResult.Equal Then
                If Not arrBack.Contains("Total Purchase") Then
                    arrBack.Add("Total Purchase")
                End If
                ddlReportType.SelectedValue = "Location Wise"
                Document_No = ""
                Print(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Location Wise") = CompairStringResult.Equal Then
                If Not arrBack.Contains("Location Wise") Then
                    arrBack.Add("Location Wise")
                End If
                ddlReportType.SelectedValue = "Vendor Group Wise"
                arrLocation = New ArrayList()
                arrLocation = txtLocation.arrValueMember
                Dim tmp As New ArrayList()
                tmp.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Location Code").Value))
                txtLocation.arrValueMember = tmp
                Document_No = ""
                Print(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Vendor Group Wise") = CompairStringResult.Equal Then
                If Not arrBack.Contains("Vendor Group Wise") Then
                    arrBack.Add("Vendor Group Wise")
                End If
                ddlReportType.SelectedValue = "Item Wise"
                arrCustGroup = New ArrayList()
                arrCustGroup = txtCustGroup.arrValueMember
                Dim tmp As New ArrayList()
                tmp.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Vendor Group Code").Value))
                txtCustGroup.arrValueMember = tmp
                Document_No = ""
                Print(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Item Wise") = CompairStringResult.Equal Then
                If Not arrBack.Contains("Item Wise") Then
                    arrBack.Add("Item Wise")
                End If
                ddlReportType.SelectedValue = "Vendor Wise"
                arrItem = New ArrayList()
                arrItem = txtItem.arrValueMember
                Dim tmp As New ArrayList()
                tmp.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Item Code").Value))
                txtItem.arrValueMember = tmp
                Document_No = ""
                Print(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Vendor Wise") = CompairStringResult.Equal Then
                If Not arrBack.Contains("Vendor Wise") Then
                    arrBack.Add("Vendor Wise")
                End If
                ddlReportType.SelectedValue = "Document Wise"
                arrCustomer = New ArrayList()
                arrCustomer = txtCustomer.arrValueMember
                Dim tmp As New ArrayList()
                tmp.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Vendor Code").Value))
                txtCustomer.arrValueMember = tmp
                Document_No = ""
                Print(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Document Wise") = CompairStringResult.Equal Then
                If Not arrBack.Contains("Document Wise") Then
                    arrBack.Add("Document Wise")
                End If
                ddlReportType.SelectedValue = "Document Detail"
                Document_No_Old = Document_No
                Document_No = clsCommon.myCstr(Gv1.CurrentRow.Cells("Document No").Value)
                Print(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Document Detail") = CompairStringResult.Equal Then
                Dim strTransType As String = clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value)
                Dim strTransCode As String = clsCommon.myCstr(Gv1.CurrentRow.Cells("Document No").Value)
                If clsCommon.myLen(strTransType) > 0 AndAlso clsCommon.myLen(strTransCode) > 0 Then
                    Select Case strTransType
                        Case "Purchase Invoice"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseInvoice, strTransCode)
                        Case "Milk Receipt"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkPurchaseInvoice, strTransCode)
                        Case "Bulk Purchase"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmBulkMilkPurchaseInvoice, strTransCode)
                        Case "Bulk Purchase Return"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmMilkPurchaseReturn, strTransCode)
                        Case "MCC Transfer"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkTransferIn, strTransCode)

                        Case "Transfer"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.Transfer, strTransCode)
                        Case "Purchase Return"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseReturn, strTransCode)
                    End Select

                End If

                'KUNAL > TICKET : BM00000009878 > DATE : 01 - OCT- 2016 
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Document Type Info") = CompairStringResult.Equal Then
                Dim strTransType As String = clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans Type").Value)
                Dim strTransCode As String = clsCommon.myCstr(Gv1.CurrentRow.Cells("Document No").Value)
                If clsCommon.myLen(strTransType) > 0 AndAlso clsCommon.myLen(strTransCode) > 0 Then
                    Select Case strTransType
                        Case "Purchase Invoice"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseInvoice, strTransCode)
                        Case "Milk Receipt"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkPurchaseInvoice, strTransCode)
                        Case "Bulk Purchase"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmBulkMilkPurchaseInvoice, strTransCode)
                        Case "Bulk Purchase Return"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmMilkPurchaseReturn, strTransCode)
                        Case "MCC Transfer"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkTransferIn, strTransCode)

                        Case "Transfer"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.Transfer, strTransCode)
                        Case "Purchase Return"
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseReturn, strTransCode)
                    End Select

                End If
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "GL Account Wise") = CompairStringResult.Equal Then
                If Not arrBack.Contains("GL Account Wise") Then
                    arrBack.Add("GL Account Wise")
                End If
                ddlReportType.SelectedValue = "Document Detail"
                StrGLAccount = clsCommon.myCstr(Gv1.CurrentRow.Cells("Inventory Account Code").Value)
                Print(Exporter.Refresh)
            End If
            PageSetupReport_ID = clsERPFuncationality.GetReportID(MyBase.Form_ID, ddlReportType.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        Dim qry As String = " select Vendor_Code as Code,Vendor_name as Name from TSPL_VENDOR_master  WHERE  Status='N'  "
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("VenMulSel", qry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
        FrmPendingRequisitionQty.SetDiplayMember(txtCustomer, "Vendor_name", "TSPL_VENDOR_master", "Vendor_Code")
    End Sub

    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Dim qry As String = " select Item_Code,Item_Desc from TSPL_ITEM_MASTER order by Item_Code "
        txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Item_Code", "Item_Code", txtItem.arrValueMember, txtItem.arrDispalyMember)
        FrmPendingRequisitionQty.SetDiplayMember(txtItem, "Item_Desc", "TSPL_ITEM_MASTER", "Item_Code")
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim stateCond As String = ""
        If txtState.arrValueMember IsNot Nothing AndAlso txtState.arrValueMember.Count > 0 Then
            stateCond = " and state in  (" + clsCommon.GetMulcallString(txtState.arrValueMember) + ") "
        End If
        If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            stateCond += "  and TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        Dim qry As String = " select Location_Code as Code,Location_Desc as [Name] from TSPL_LOCATION_MASTER  where location_type IN  ('Physical','Virtual') " & stateCond & "  "
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        FrmPendingRequisitionQty.SetDiplayMember(txtLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub

    Private Sub txtTransaction__My_Click(sender As Object, e As EventArgs) Handles txtTransaction._My_Click
        Dim qry As String = " Select xxx.Code,  xxx.Name From (" &
                                 " Select distinct 'PI' As Code,    'Purchase Invoice' As Name from TSPL_PI_HEAD " &
                                 " Union  Select distinct 'MCC' As Code,    'Milk Receipt' As Name from TSPL_MILK_SRN_HEAD " &
                                 " Union  Select distinct 'Bulk' As Code,    'Bulk Purchase' As Name from tspl_Bulk_milk_purchase_Invoice_head " &
                                  " Union  Select distinct 'Bulk Purchase Return' As Code,    'Bulk Purchase Return' As Name from TSPL_BULK_MILK_PURCHASE_RETURN_HEAD " &
                                 " Union  Select distinct 'MCC Transfer' As Code,    'MCC Transfer' As Name from TSPL_MILK_TRANSFER_IN " &
                                 " Union  Select distinct 'Transfer' As Code,    'Transfer' As Name from TSPL_TRANSFER_ORDER_HEAD " &
                                  " Union  Select distinct 'Transfer Return' As Code,    'Transfer Return' As Name from TSPL_TRANSFER_RETURN " &
                                 " Union  Select distinct 'Return' As Code,    'Purchase Return' As Name from TSPL_PR_HEAD " &
                                 " union Select distinct 'MT' As Code,    'Merchant Trade' As Name from TSPL_PI_HEAD " &
                                 " ) xxx"
        txtTransaction.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulPur", qry, "Name", "Name", txtTransaction.arrValueMember, txtTransaction.arrDispalyMember)
    End Sub

    Sub LoadCategory()
        dtCategory = clsDBFuncationality.GetDataTable("select ITEM_CATEGORY_CODE AS CodeColumn,ITEM_CATEGORY_CODE+' Description' as CodeDescColumn,DESCRIPTION as DescColumn  from TSPL_ITEM_CATEGORY_LEVEL order by CATEGORY_LEVEL")

        gvCategory.DataSource = Nothing
        Dim qry As String = "select cast( 0 as bit) as SEL,ITEM_CATEGORY_CODE AS Code,DESCRIPTION as NAME from TSPL_ITEM_CATEGORY_LEVEL ORDER BY CATEGORY_LEVEL"
        gvCategory.DataSource = clsDBFuncationality.GetDataTable(qry)

        gvCategory.Columns("SEL").ReadOnly = False
        gvCategory.Columns("SEL").Width = 30
        gvCategory.Columns("SEL").HeaderText = " "

        gvCategory.Columns("CODE").ReadOnly = True
        gvCategory.Columns("CODE").Width = 100
        gvCategory.Columns("CODE").HeaderText = "Code"

        gvCategory.Columns("NAME").ReadOnly = True
        gvCategory.Columns("NAME").Width = 200
        gvCategory.Columns("NAME").HeaderText = "Description"

        gvCategory.ShowGroupPanel = False
        gvCategory.AllowAddNewRow = False
        gvCategory.AllowColumnReorder = False
        gvCategory.AllowRowReorder = False
        gvCategory.EnableSorting = False
        gvCategory.ShowFilteringRow = True
        gvCategory.EnableFiltering = True
        gvCategory.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvCategory.MasterTemplate.ShowRowHeaderColumn = True

    End Sub

    Private Sub gvCategory_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gvCategory.CellDoubleClick
        If clsCommon.myCBool(gvCategory.CurrentRow.Cells("SEL").Value) Then
            Dim frm As New FrmCategorySelect()
            frm.lvl = 2
            frm.strCode = clsCommon.myCstr(gvCategory.CurrentRow.Cells("CODE").Value)
            frm.arrIn = gvCategory.CurrentRow.Tag
            frm.ShowDialog()
            If Not frm.isCancel Then
                gvCategory.CurrentRow.Tag = frm.arrOut
            End If
        End If
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
                Print(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Document Detail") = CompairStringResult.Equal AndAlso arrBack.Contains("GL Account Wise") Then
                arrBack.Remove("GL Account Wise")
                ddlReportType.SelectedValue = "GL Account Wise"
                StrGLAccount = ""
                Print(Exporter.Refresh)

            End If
            PageSetupReport_ID = clsERPFuncationality.GetReportID(MyBase.Form_ID, ddlReportType.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCustGroup__My_Click(sender As Object, e As EventArgs) Handles txtCustGroup._My_Click
        Dim qry As String = " select Ven_Group_Code as Code,Group_desc as Name from TSPL_VENDOR_group"
        txtCustGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("VenGroupMulSel", qry, "Code", "Name", txtCustGroup.arrValueMember, txtCustGroup.arrDispalyMember)
        FrmPendingRequisitionQty.SetDiplayMember(txtCustGroup, "Group_desc", "TSPL_VENDOR_group", "Ven_Group_Code")
    End Sub

    Private Sub btnQuickExport_Click(sender As Object, e As EventArgs)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptPurchaseRegisterReport & "'"))
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")
            If clsCommon.myLen(txtUOM.Value) > 0 Then
                arrHeader.Add("UOM : " + txtUOM.Value)
            End If
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
            'Dim sfd As SaveFileDialog = New SaveFileDialog()
            'Dim filePath As String
            'sfd.FileName = Me.Text
            'sfd.Filter = "Excel 2007 (*.xlsx) |*.xlsx;|Excel 97-2003 (*.xls)|*.xlsx;|CSV Files (*.csv) |*.csv"
            'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    filePath = sfd.FileName
            'Else
            '    Exit Sub
            'End If
            'transportSql.exportdataChilRows(Gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader) 'frm.Text)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
            transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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

    Public Shared Function ReturnQuery(ByVal From_Date As Date, ByVal To_Date As Date, ByVal Unit_Code As String, ByVal StockingUom As Boolean) As ArrayList
        '' query change by Panch raj against Ticket No:BM00000008426
        Dim strMainQuery As String = ""
        Dim QryLst As New ArrayList
        Dim strCodeColumn As String = ""
        Dim strCodeColumn1 As String = ""
        Dim strCodeColumnMax As String = ""
        Dim strCodeDescColumn As String = ""
        Dim strCodeDescColumnMax As String = ""
        Dim strPivotForFinalOuterQuery As String = ""
        Dim strPivotForAddChargeFinalOuterQuery As String = ""
        Dim strPivotForAddChargeFinalOuterSumQuery As String = ""
        Dim strCategoryTable As String = ""
        Dim dtCategory As DataTable
        dtCategory = clsDBFuncationality.GetDataTable("select ITEM_CATEGORY_CODE AS CodeColumn,ITEM_CATEGORY_CODE+' Description' as CodeDescColumn,DESCRIPTION as DescColumn  from TSPL_ITEM_CATEGORY_LEVEL order by CATEGORY_LEVEL")
        If dtCategory IsNot Nothing AndAlso dtCategory.Rows.Count > 0 Then
            For ii As Integer = 0 To dtCategory.Rows.Count - 1
                If ii <> 0 Then
                    strCodeColumn += ","
                    strCodeColumn1 += ","
                    strCodeColumnMax += ","
                    strCodeDescColumn += ","
                    strCodeDescColumnMax += ","
                End If
                strCodeColumn += "[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                strCodeColumn1 += "VirtualCategoryTabel.[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                strCodeColumnMax += "max([" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]) as [" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                strCodeDescColumn += "[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")) + "]"
                strCodeDescColumnMax += "max([" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")).Trim() + "]) as [" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")).Trim() + "]"
            Next
            strCategoryTable = "select Item_Code," + strCodeColumnMax + "," + strCodeDescColumnMax + "  from (" + Environment.NewLine & _
            " select * from ( " + Environment.NewLine & _
            " select TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code " + Environment.NewLine & _
            " ,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code+' Description' as Item_Category_CodeDesc " + Environment.NewLine & _
            " ,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values " + Environment.NewLine & _
            " ,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as Category_Value_Desc " + Environment.NewLine & _
            " from  TSPL_ITEM_MASTER  " + Environment.NewLine & _
            " left outer join TSPL_ITEM_MASTER_CATEGORY on  TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code " + Environment.NewLine & _
            " left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values" + Environment.NewLine & _
            " where 2=2 " + Environment.NewLine & _
            " )xx" + Environment.NewLine

            If clsCommon.myLen(strCodeColumn) > 0 Then
                strCategoryTable = strCategoryTable + " Pivot " + Environment.NewLine & _
            " ( max(Item_Cagetory_Values) for Item_Category_Code   in ( " + strCodeColumn + ")" + Environment.NewLine & _
            " ) Pivt" + Environment.NewLine
            End If

            If clsCommon.myLen(strCodeDescColumn) > 0 Then
                strCategoryTable = strCategoryTable + " Pivot " + Environment.NewLine & _
           " (" + Environment.NewLine & _
           " max(Category_Value_Desc) for Item_Category_CodeDesc in (" + strCodeDescColumn + ")" + Environment.NewLine & _
           " ) Pivt1 " + Environment.NewLine
            End If

            strCategoryTable = strCategoryTable + " ) xxx group by Item_Code "
        End If
        Dim qryTaxQuery As String = ""
        Dim qryAddChargeQuery As String = ""
        Dim qryAddChargeForZeroQuery As String = ""

        Dim strPivotForOuter As String
        Dim strPivotForOuterOnlyForDocumentInfo As String
        Dim strPivotForAddChargeOuter As String
        Dim lstTables As New List(Of String)
        '========added By preeti gupta===========
        Dim lstTablesAddCharge As New List(Of String)
        '================ENd==========
        lstTables.Add("TSPL_PI_DETAIL")
        qryTaxQuery = GetTaxQuery(lstTables)
        '===============Added By preeti gupta============
        lstTablesAddCharge.Add("TSPL_PI_HEAD")
        qryAddChargeQuery = GetAddChargeQuery(lstTablesAddCharge)
        qryAddChargeForZeroQuery = GetAddChargeZeroQuery(lstTablesAddCharge)

        '==========================END=================

        'strPivotForOuter = " select distinct (select Distinct ',sum(isnull(final.'+tax1+',0)) as '+TAX1 from ( " & qryTaxQuery
        strPivotForOuter = "select distinct (select Distinct ',sum(isnull(final.['+tax1+'],0)) as ['+TAX1+']' from ( " & qryTaxQuery
        strPivotForOuter += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"

        Dim strPivotForOuterQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForOuter))
        '============================
        '=====================Added by preeti Gupta========================================
        strPivotForOuterOnlyForDocumentInfo = "select distinct (select Distinct ',(isnull(final.['+tax1+'],0)) as ['+TAX1+']' from ( " & qryTaxQuery
        strPivotForOuterOnlyForDocumentInfo += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"

        Dim strPivotForOuterQueryOnlyForDocumentInfo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForOuterOnlyForDocumentInfo))
        '=====================================================================================
        '==================Added By Preeti Gupta=============================
        strPivotForAddChargeOuter = " select REPLACE(xp,'&amp;','&') from (select distinct (select Distinct ',sum(isnull(final.['+Add_Charge_Code1+'],0)) as ['+Add_Charge_Code1+']' from ( " & qryAddChargeQuery
        strPivotForAddChargeOuter += " )aa where len(isnull(Add_Charge_Code1,''))>0 for xml path('') ) as xp)fin"

        Dim strPivotForAddChargeOuterQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForAddChargeOuter))
        '===============================END==================================
        Dim strPivotForFinalOuter As String
        strPivotForFinalOuter = ""
        strPivotForFinalOuter = " select distinct (select Distinct ',xx.['+tax1+']' from ( " & qryTaxQuery
        strPivotForFinalOuter += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"
        strPivotForFinalOuterQuery = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForFinalOuter))
        '=====================================Added By Preeti Gupta============================================
        Dim strPivotForAddChargeFinalOuter As String
        strPivotForAddChargeFinalOuter = ""
        strPivotForAddChargeFinalOuter = " select REPLACE(xp,'&amp;','&') from (select distinct (select Distinct ',xx.['+Add_Charge_Code1+']' from ( " & qryAddChargeQuery
        strPivotForAddChargeFinalOuter += " )aa where len(isnull(Add_Charge_Code1,''))>0 for xml path('') ) as xp)fin"
        strPivotForAddChargeFinalOuterQuery = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForAddChargeFinalOuter))

        Dim strPivotForAddChargeFinalSum As String
        strPivotForAddChargeFinalSum = ""
        strPivotForAddChargeFinalSum = " select REPLACE(xp,'&amp;','&') from ( select distinct (select Distinct ',xx.['+'AC_'+Add_Charge_Code1+']' from ( " & qryAddChargeQuery
        strPivotForAddChargeFinalSum += " )aa where len(isnull(Add_Charge_Code1,''))>0 for xml path('') )as xp)fin"
        strPivotForAddChargeFinalOuterSumQuery = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForAddChargeFinalSum))

        '=============================================END======================================================

        Dim strPivotForFinalOuterPercent As String
        strPivotForFinalOuterPercent = " select distinct (select  Distinct ',xx.['+tax1+'%'+']' from ( " & qryTaxQuery
        strPivotForFinalOuterPercent += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"
        Dim strPivotForFinalOuterPercentQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForFinalOuterPercent))

        Dim strPivotForTransfer_In As String
        strPivotForFinalOuter = ""
        strPivotForFinalOuter = " select distinct (select Distinct ',0 as ['+tax1+']' from ( " & qryTaxQuery
        strPivotForFinalOuter += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"
        strPivotForTransfer_In = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForFinalOuter))
        Dim strTransferTaxColumns As String = ""
        Dim strTransferTaxPerColumns As String = ""
        Dim dtTempDT As DataTable = clsDBFuncationality.GetDataTable("select * from (" + qryTaxQuery + ")xx where len(isnull(TAX1,''))>0  order by  TAX1")
        If dtTempDT IsNot Nothing AndAlso dtTempDT.Rows.Count > 0 Then
            For Each dr As DataRow In dtTempDT.Rows
                Dim strTax As String = clsCommon.myCstr(dr(0))
                strTransferTaxColumns += ", (case when TSPL_TRANSFER_ORDER_DETAIL.TAX1='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX1_Amt else case when TSPL_TRANSFER_ORDER_DETAIL.TAX2='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX2_Amt else case when TSPL_TRANSFER_ORDER_DETAIL.TAX3='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX3_Amt else case when TSPL_TRANSFER_ORDER_DETAIL.TAX4='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX4_Amt else case when TSPL_TRANSFER_ORDER_DETAIL.TAX5='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX5_Amt else case when TSPL_TRANSFER_ORDER_DETAIL.TAX6='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX6_Amt else case when TSPL_TRANSFER_ORDER_DETAIL.TAX7='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX7_Amt else case when TSPL_TRANSFER_ORDER_DETAIL.TAX8='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX8_Amt else case when TSPL_TRANSFER_ORDER_DETAIL.TAX9='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX9_Amt else case when TSPL_TRANSFER_ORDER_DETAIL.TAX10='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX10_Amt else 0 end end end end end end end end end end ) as [" + strTax + "]"
                strTransferTaxPerColumns += ",(case when TSPL_TRANSFER_ORDER_DETAIL.TAX1='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX1_Rate else case when TSPL_TRANSFER_ORDER_DETAIL.TAX2='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX2_Rate else case when TSPL_TRANSFER_ORDER_DETAIL.TAX3='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX3_Rate else case when TSPL_TRANSFER_ORDER_DETAIL.TAX4='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX4_Rate else case when TSPL_TRANSFER_ORDER_DETAIL.TAX5='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX5_Rate else case when TSPL_TRANSFER_ORDER_DETAIL.TAX6='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX6_Rate else case when TSPL_TRANSFER_ORDER_DETAIL.TAX7='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX7_Rate else case when TSPL_TRANSFER_ORDER_DETAIL.TAX8='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX8_Rate else case when TSPL_TRANSFER_ORDER_DETAIL.TAX9='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX9_Rate else case when TSPL_TRANSFER_ORDER_DETAIL.TAX10='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX10_Rate else 0 end end end end end end end end end end ) as [" + strTax + "%]"
            Next
        End If

        '=============================Added By Preeti Gupta=================================================

        Dim strPivotForAddChargeFinalOuterPercent As String
        strPivotForAddChargeFinalOuterPercent = " select REPLACE(xp,'&amp;','&') from (select distinct (select  Distinct ',xx.['+Add_Charge_Code1+']' from ( " & qryAddChargeQuery
        strPivotForAddChargeFinalOuterPercent += " )aa where len(isnull(Add_Charge_Code1,''))>0 for xml path('') )as xp)fin"
        Dim strPivotForFinalAddChargeOuterPercentQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForAddChargeFinalOuterPercent))

        Dim strPivotForAddChargeZeroFinalOuterPercent As String
        strPivotForAddChargeZeroFinalOuterPercent = " select REPLACE(xp,'&amp;','&') from ( select distinct (select  Distinct ',xx.['+Add_Charge_Code1+']' from ( " & qryAddChargeForZeroQuery
        strPivotForAddChargeZeroFinalOuterPercent += " )aa where len(isnull('AC_'+Add_Charge_Code1,''))>0 for xml path('') )as xp)fin"
        Dim strPivotForFinalAddChargeZeroOuterPercentQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForAddChargeZeroFinalOuterPercent))

        Dim strPivotForAddChargeTransfer_In As String
        strPivotForAddChargeFinalOuter = ""
        strPivotForAddChargeFinalOuter = " select REPLACE(xp,'&amp;','&') from (select distinct (select Distinct ',0 as ['+Add_Charge_Code1+']' from ( " & qryAddChargeQuery
        strPivotForAddChargeFinalOuter += " )aa where len(isnull(Add_Charge_Code1,''))>0 for xml path('') )as xp)fin"
        strPivotForAddChargeTransfer_In = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForAddChargeFinalOuter))

        Dim strPivotForAddChargeForZeroTransfer_In As String
        strPivotForAddChargeFinalOuter = ""
        strPivotForAddChargeFinalOuter = " select REPLACE(xp,'&amp;','&') from (select distinct (select Distinct ',0 as ['+Add_Charge_Code1+']' from ( " & qryAddChargeForZeroQuery
        strPivotForAddChargeFinalOuter += " )aa where len(isnull('AC_'+Add_Charge_Code1,''))>0 for xml path('') )as xp)fin"
        strPivotForAddChargeForZeroTransfer_In = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForAddChargeFinalOuter))

        '=========================================END=======================================================

        Dim strPivotFortRANSFER_INPercent As String
        strPivotFortRANSFER_INPercent = " select distinct (select  Distinct ',0 as ['+tax1+'%'+']' from ( " & qryTaxQuery
        strPivotFortRANSFER_INPercent += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"
        Dim strPivotFortRANSFER_INPercentQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotFortRANSFER_INPercent))
        '===========

        Dim strPivotForGroupOuter As String
        strPivotForGroupOuter = " select REPLACE(abc,'&amp;','&') from ( select SUBSTRING(ax,2,len(Ax)) as abc from ("
        'strPivotForGroupOuter += " select distinct (select Distinct ',['+tax1+'%'+']' from ( " & qryTaxQuery
        strPivotForGroupOuter += " select distinct (select Distinct ',max(isnull(final.['+tax1+'%'+'],0)) as ['+TAX1+'%'+']' from ( " & qryTaxQuery

        strPivotForGroupOuter += " )a where len(isnull(TAX1,''))>0 for xml path('') )ax)Axx)XXX"
        Dim strPivotFoGrouprOuterQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForGroupOuter))

        ''done below code so that when variable placed in query,no need for comma(,),in case of blank variable its gives error.(06/12/2016)
        If clsCommon.myLen(strPivotFoGrouprOuterQuery) > 0 Then
            strPivotFoGrouprOuterQuery = "," + strPivotFoGrouprOuterQuery
        End If




        '======================================Added by Preeti Gupta
        '================================Added by preeti Gupta===============================
        Dim strPivotForGroupOuterOnlyForDocumnetInfo As String
        strPivotForGroupOuterOnlyForDocumnetInfo = " select REPLACE(abc,'&amp;','&') from ( select SUBSTRING(ax,2,len(Ax)) as abc from ("
        'strPivotForGroupOuter += " select distinct (select Distinct ',['+tax1+'%'+']' from ( " & qryTaxQuery
        strPivotForGroupOuterOnlyForDocumnetInfo += " select distinct (select Distinct ',(isnull(final.['+tax1+'%'+'],0)) as ['+TAX1+'%'+']' from ( " & qryTaxQuery

        strPivotForGroupOuterOnlyForDocumnetInfo += " )a where len(isnull(TAX1,''))>0 for xml path('') )ax)Axx)XXX"
        Dim strPivotFoGrouprOuterQueryonlyForDocumnetInfo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForGroupOuterOnlyForDocumnetInfo))
        '=========================================================================================

        Dim strPivotForADDChargeGroupOuter As String
        strPivotForADDChargeGroupOuter = " select REPLACE(abc,'&amp;','&') from (select SUBSTRING(ax,2,len(Ax)) as abc from ("
        'strPivotForGroupOuter += " select distinct (select Distinct ',['+tax1+'%'+']' from ( " & qryTaxQuery
        strPivotForADDChargeGroupOuter += " select distinct (select Distinct ',max(isnull(final.['+Add_Charge_Code1+'],0)) as ['+Add_Charge_Code1+']' from ( " & qryAddChargeQuery

        strPivotForADDChargeGroupOuter += " )a where len(isnull(Add_Charge_Code1,''))>0 for xml path('') )ax)Axx)XXX"
        Dim strPivotFoAddChargeGrouprOuterQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForADDChargeGroupOuter))

        Dim strPivotForADDChargeZeroGroupOuter As String
        strPivotForADDChargeZeroGroupOuter = "select REPLACE(abc,'&amp;','&') from (select SUBSTRING(ax,2,len(Ax)) as abc from ("
        'strPivotForGroupOuter += " select distinct (select Distinct ',['+tax1+'%'+']' from ( " & qryTaxQuery
        strPivotForADDChargeZeroGroupOuter += " select distinct (select Distinct ',sum(isnull(final.['+Add_Charge_Code1+'],0)) as ['+Add_Charge_Code1+']' from ( " & qryAddChargeForZeroQuery

        strPivotForADDChargeZeroGroupOuter += " )a where len(isnull('AC_'+Add_Charge_Code1,''))>0 for xml path('') )ax)Axx)XXX"
        Dim strPivotFoAddChargeZeroGrouprOuterQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForADDChargeZeroGroupOuter))

        ''done below code so that when variable placed in query,no need for comma(,),in case of blank variable its gives error.(06/12/2016)
        If clsCommon.myLen(strPivotFoAddChargeZeroGrouprOuterQuery) > 0 Then
            strPivotFoAddChargeZeroGrouprOuterQuery = "," + strPivotFoAddChargeZeroGrouprOuterQuery
        End If




        '================================================END================================================
        '==================================Added by preeti Gupta====================================
        Dim strPivotForADDChargeZeroGroupOuterOnlyForDocumentInfo As String
        strPivotForADDChargeZeroGroupOuterOnlyForDocumentInfo = "select REPLACE(abc,'&amp;','&') from (select SUBSTRING(ax,2,len(Ax)) as abc from ("
        'strPivotForGroupOuter += " select distinct (select Distinct ',['+tax1+'%'+']' from ( " & qryTaxQuery
        strPivotForADDChargeZeroGroupOuterOnlyForDocumentInfo += " select distinct (select Distinct ',(isnull(final.['+Add_Charge_Code1+'],0)) as ['+Add_Charge_Code1+']' from ( " & qryAddChargeForZeroQuery

        strPivotForADDChargeZeroGroupOuterOnlyForDocumentInfo += " )a where len(isnull('AC_'+Add_Charge_Code1,''))>0 for xml path('') )ax)Axx)XXX"
        Dim strPivotFoAddChargeZeroGrouprOuterQueryonlyForDocumentInfo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForADDChargeZeroGroupOuterOnlyForDocumentInfo))
        '===============================================================================================


        Dim strPivotForOuterForBulk As String
        strPivotForOuterForBulk = " select distinct (select Distinct ',0 as ['+TAX1+']' from ( " & qryTaxQuery

        strPivotForOuterForBulk += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"

        Dim strPivotForOuterQueryforBulk As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForOuterForBulk))

        Dim strDoublePivotForOuterForBulk As String

        strDoublePivotForOuterForBulk = " select distinct (select Distinct ',0 as ['+tax1+'%'+']' from ( " & qryTaxQuery


        strDoublePivotForOuterForBulk += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"

        Dim strDoublePivotForOuterQueryforBulk As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strDoublePivotForOuterForBulk))

        '=====================================Added by Preeti Gupta===================================================================
        Dim strPivotForAddChargeOuterForBulk As String
        strPivotForAddChargeOuterForBulk = " select REPLACE(xp,'&amp;','&') from (select distinct (select Distinct ',0 as ['+Add_Charge_Code1+']' from ( " & qryAddChargeQuery

        strPivotForAddChargeOuterForBulk += " )aa where len(isnull(Add_Charge_Code1,''))>0 for xml path('') )as xp)fin"

        Dim strPivotForAddChargeOuterQueryforBulk As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForAddChargeOuterForBulk))

        'Dim strDoublePivotForAddChrageOuterForBulk As String

        'strDoublePivotForAddChrageOuterForBulk = " select distinct (select Distinct ',0 as ['+Add_Charge_Code1+'%'+']' from ( " & qryAddChargeQuery


        'strDoublePivotForAddChrageOuterForBulk += " )aa where len(isnull(Add_Charge_Code1,''))>0 for xml path('') )"

        'Dim strDoublePivotForAddChargeOuterQueryforBulk As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strDoublePivotForAddChrageOuterForBulk))

        '============================================END===============================================================================


        Dim strPivotForInner As String
        strPivotForInner = "select SUBSTRING(ax,2,len(Ax)) from ("
        strPivotForInner += " select distinct (select Distinct ',['+tax1+']' from ( " & qryTaxQuery

        strPivotForInner += " )a where len(isnull(TAX1,''))>0 for xml path('') )ax)Axx"

        Dim strPivotForInnerQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForInner))

        '============================================Added By Preeti Gupta ticket no [BM00000009024]===========================================
        Dim strPivotForAddChargeInner As String
        strPivotForAddChargeInner = "select REPLACE(abc,'&amp;','&') from (select SUBSTRING(ax,2,len(Ax)) as abc from ("
        strPivotForAddChargeInner += " select distinct (select Distinct ',['+Add_Charge_Code1+']' from ( " & qryAddChargeQuery

        strPivotForAddChargeInner += " )a where len(isnull(Add_Charge_Code1,''))>0 for xml path('') ) as ax)Axx)XXX"

        Dim strPivotForAddChargeInnerQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForAddChargeInner))

        Dim strPivotForAddChargeInnerOuter As String
        strPivotForAddChargeInnerOuter = " select REPLACE(abc,'&amp;','&') from (select ','+SUBSTRING(ax,2,len(Ax)) as abc from ("
        strPivotForAddChargeInnerOuter += " select distinct (select Distinct ',['+Add_Charge_Code1+']  as ['+'AC_'+Add_Charge_Code1+']' from ( " & qryAddChargeQuery

        strPivotForAddChargeInnerOuter += " )a where len(isnull(Add_Charge_Code1,''))>0 for xml path('') ) as ax)Axx)XXX"
        Dim strPivotForAddChargeInnerQueryOuter As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForAddChargeInnerOuter))

        '=======================================================END==================================================



        Dim strDoublePivotForInner As String
        strDoublePivotForInner = "select SUBSTRING(ax,2,len(Ax)) from ("
        strDoublePivotForInner += " select distinct (select Distinct ',['+tax1+'%'+']' from ( " & qryTaxQuery

        strDoublePivotForInner += " )a where len(isnull(TAX1,''))>0 for xml path('') )ax)Axx"

        Dim strDoublePivotForInnerQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strDoublePivotForInner))
        Dim qryQC As String = ""
        qryQC = " select Item_Code,MAX(Fat_Per) as Fat_Per,MAX(SNF_Per) as SNF_Per from (" & _
                " select Item_QCP.Item_Code,Item_QCP.Code as Parameter_Code,(case when QCP.Type='FAT' then Item_QCP.Actual_Range end) as Fat_Per," & _
                " (case when QCP.Type='SNF' then Item_QCP.Actual_Range  end) as SNF_Per from TSPL_ITEM_QC_PARAMETER_MASTER Item_QCP " & _
                " left join TSPL_PARAMETER_MASTER QCP  on Item_QCP.Code=QCP.Code) as QC group by Item_Code"

        Dim qryKG As String = ""
        qryKG = "select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='KG'"
        Dim qryStock As String = ""
        qryStock = "select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL "

        '' query for transaction  UOM conversion
        Dim qryTransStock As String = ""
        If clsCommon.myLen(Unit_Code) <= 0 AndAlso StockingUom = False Then
            qryTransStock = "select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL "
        Else
            If StockingUom Then
                qryTransStock = "select Item_Code,max(UOM_Code) as UOM_Code,max(Conversion_Factor) as Conversion_Factor from TSPL_ITEM_UOM_DETAIL where  Stocking_Unit='Y'  group by Item_Code"
            Else
                qryTransStock = "select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='" & Unit_Code & "'"
            End If

            'qryTransStock = "select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='" & Unit_Code & "'"
        End If

        '' end query for transaction  UOM conversion

        '' query for structure and item group custom field
        Dim strSDCommonQuery As String = ""
        Dim strTaxColumns As String = ""
        Dim strAddChargeColumns As String = ""
        Dim strTaxNonRecoverableAmt As String = ""
        Dim strSDEndQry As String = ",TSPL_PI_DETAIL.TAX1+'%' as Tax1_Rate"
        strSDCommonQuery = " select Distinct  TSPL_PI_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,'PI' as Trans_Type,TSPL_PI_DETAIL.Line_No,TSPL_PI_HEAD .ConvRate,TSPL_PI_DETAIL.srn_Id,TSPL_PI_HEAD.Status ,TSPL_PI_HEAD.Bill_To_Location, " & _
                           " TSPL_PI_HEAD.Vendor_Code as Customer_Code,TSPL_STATE_MASTER.STATE_CODE as [State Code],TSPL_STATE_MASTER.STATE_NAME as [State Name],'PI' as Invoice_Type,TSPL_PI_HEAD.PI_NO , " & _
                           " convert(varchar,TSPL_PI_HEAD.PI_Date,103 ) as PI_Date,'' as Way_BillNo,TSPL_PI_HEAD.GRNO,TSPL_PI_HEAD.LR_NO,TSPL_PI_HEAD.Vendor_Invoice_No ,convert(varchar,TSPL_PI_Head.InvoiceDate,103) as Vendor_Invoice_Date,TSPL_PI_HEAD.Transporter as [Transporter],TSPL_PI_HEAD.TransporterDesc as [Transporter Name], TSPL_PI_DETAIL.Item_Code , " & _
                           " TSPL_PI_DETAIL.PI_Qty as Qty ,TSPL_PI_DETAIL.Unit_code ,TSPL_PI_DETAIL.Item_Cost*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Item_Cost , " & _
                           " TSPL_PI_DETAIL.Amount*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amount ,TSPL_PI_DETAIL.Disc_Per ,TSPL_PI_DETAIL.Disc_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Disc_Amt , " & _
                           " TSPL_PI_DETAIL.Amt_Less_Discount *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amt_Less_Discount, " & _
                           " (TSPL_PI_DETAIL.Total_Tax_Amt) *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Tax_Amt,TSPL_PI_DETAIL.Item_Net_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Amt,TSPL_PI_HEAD.vehicledesc,TSPL_VEHICLE_MASTER.Number as Vehicle_No,(TSPL_PI_Detail.Total_ItemAdd_Charge*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end)) as Additional_Charge,0 as NonRecoverable_Tax, "
        strSDEndQry = ",TSPL_vendor_Invoice_Head.Document_No as [AP Document No],TSPL_vendor_Invoice_Head.Document_Total*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as [AP Document Amt],TSPL_vendor_Invoice_Head.Discount_Amount as [AP Document Discount Amt],TSPL_vendor_Invoice_Head.amount_less_Discount*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as [AP Amount Before Tax],TSPL_PI_DETAIL.SRN_Id as against_PoInvoice_No,TSPL_vendor_Invoice_Head.Against_PurchaseREturn_No,TSPL_vendor_Invoice_Head.total_tax*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as [AP Total Tax],TSPL_vendor_Invoice_Head.total_Add_Charge*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as [AP Total Add Charge],TSPL_vendor_Invoice_Head.Total_landed_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as [AP Landed Amt],TSPL_vendor_Invoice_Head.Against_MillkpurchaseInvoice_No,TSPL_vendor_Invoice_Head.Against_BulkMillkpurchaseInvoice_No,TSPL_vendor_Invoice_Head.Document_total*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as [AP Document Total],TSPL_PI_DETAIL.Po_Id,TSPL_PI_DETAIL.MRP,TSPL_PI_HEAD.Purchase_Tax_Invoice,TSPL_PURCHASE_ORDER_HEAD.Created_By,TSPL_PURCHASE_ORDER_HEAD.Modify_By,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No as Doc_No,TSPL_PURCHASE_ORDER_head.PurchaseOrder_Date as Doc_Purchase_Date " + _
        ",case when TSPL_PI_Head.PI_Type='I' then 'Yes' else 'No' end as [Import Type],case when TSPL_PI_Head.PI_Type='I' then TSPL_PI_Head.Port  else null end as [Port],case when TSPL_PI_Head.PI_Type='I' then TSPL_PI_Head.Import_Entry_No else null end as [Import Bill of Entry No],case when TSPL_PI_Head.PI_Type='I' then convert(varchar, TSPL_PI_Head.Import_Entry_Date,103) else null end as [Import Bill of Entry Date]" + Environment.NewLine + _
        ",'' as [Original Invoice No],'' as [Original Invoice Date],'' as [Reason For Revision],case when TSPL_PI_HEAD.ITC_Elibible=1 then 'Yes' else 'No' end as [ITC Eligible],case when TSPL_PI_HEAD.ITC_Elibible=1 then case when TSPL_PI_HEAD.ITC_Type=1 then 'Yes' else 'No' end else '' end as [ITC Status],case when TSPL_PI_HEAD.ITC_Elibible=1 then TSPL_PI_HEAD.ITC_Type_Category else '' end as [ITC Details] " + Environment.NewLine + _
                        " from TSPL_PI_DETAIL " & _
                           " left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_NO =TSPL_PI_DETAIL.PI_NO " & _
                           " left join TSPL_VEHICLE_MASTER on TSPL_PI_HEAD.vehicledesc=TSPL_VEHICLE_MASTER.Vehicle_Id left join TSPL_vendor_Invoice_Head on TSPL_vendor_Invoice_Head.against_PoInvoice_No=TSPL_PI_HEAD.PI_NO  left join TSPL_vendor_master on   TSPL_vendor_master.Vendor_Code=tspl_Pi_Head.Vendor_Code left join TSPL_STATE_MASTER on tspl_State_Master.STATE_CODE=TSPL_VENDOR_MASTER.State_Code " & _
                           "    "
        strMainQuery = "  select [Trans Type],[Location Code],[Location Name],[Location Address],TSPL_STATE_MASTER.GST_STATE_Code as [Location State GST], TSPL_STATE_MASTER.STATE_CODE as [Location State Code], TSPL_STATE_MASTER.STATE_NAME as [Location State Name],TSPL_LOCATION_MASTER_For_GSTIN.City_Code as VendorCityCode, TSPL_LOCATION_MASTER_For_GSTIN.City_Code as VendorCityName,TSPL_LOCATION_MASTER_For_GSTIN.GSTNO as Location_GSTIN,[Invoice Type],[Document No],[Document Date],[Way Bill No],[GR No],[LR No],Vendor_Invoice_no as [Vendor Invoice No],case when Coalesce(Vendor_Invoice_no,'')<>'' then convert(varchar,Vendor_Invoice_Date,103) else null end as [Vendor Invoice Date],vehicledesc as [Vehicle Code],Vehicle_No as [Vehicle No],cast(Additional_Charge as numeric(18,3)) as [Additional Amount],[Vendor Code],[Vendor Name],[Vendor Address],[State Code] as [Vendor State Code],[State Name] as [Vendor State Desc.],case when cust.GST_Composition_scheme=1 then 'Yes' else 'No' end as GST_Composition_scheme,case when [Trans Type] in ('MCC Transfer','Transfer') then case when TSPL_LOCATION_MASTER_AS_Transfer.Registered=1 then 'Yes' else 'No' end else case when cust.GSTRegistered=1 then 'Yes' else 'No' end end GSTRegistered,case when [Trans Type] in ('MCC Transfer','Transfer') then TSPL_LOCATION_MASTER_AS_Transfer.GSTNO else cust.GSTFinalNo end GSTFinalNo ,case when [Trans Type] in ('MCC Transfer','Transfer') then TSPL_LOCATION_MASTER_AS_Transfer.GST_STATE_Code else cust.Vendor_GST_STATE_Code end as Vendor_GST_STATE_Code,case when [Trans Type] in ('MCC Transfer','Transfer') then TSPL_LOCATION_MASTER_AS_Transfer.STATE_NAME else cust.Veindor_STATE_Name end as Veindor_STATE_Name,[Vendor TIN No],xx.[Transporter],[Transporter Name],Cust.Vendor_Group_Code as [Vendor Group Code],Cust_Group.Group_Desc as [Vendor Group Description], [Parent Vendor No],[Parent Vendor Code], [Parent Vendor Name]"
        If clsCommon.myLen(strCategoryTable) > 0 Then
            strMainQuery += "," + strCodeColumn1 + "," + strCodeDescColumn
        End If
        strMainQuery += " , [Item Code],[Item Name],cast(([Quantity]*Stock_SU.Conversion_Factor)/(coalesce(TransStock.Conversion_Factor,1)) as Numeric(18,3)) as [Quantity]," & IIf(clsCommon.myLen(Unit_Code) <= 0, "xx.[UOM]", "'" & Unit_Code & "'") & " as [UOM],[Item Cost] as [Item Rate],[Fat Per] as [FAT %],[SNF Per] as [SNF %],cast(([Quantity]*[Fat Per]*Stock_SU.Conversion_Factor)/(100*coalesce(StockKG.Conversion_Factor,1)) as numeric(18,3)) as [FAT KG],cast(([Quantity]*[SNF Per]*Stock_SU.Conversion_Factor)/(100*coalesce(StockKG.Conversion_Factor,1)) as Numeric(18,3)) as [SNF KG],Amount,[Discount Per] as [Discount %], [Discount Amount],cast(Round([EMP],2) as decimal(18,2)) as [EMP],Round([Incentive],2) as [Incentive],Round([IncentiveEMP],0) as [Incentive EMP] ,Round([Amount Less Discount],2) as [Amount Less Discount] " + strPivotForFinalOuterQuery + " " + strPivotForFinalOuterPercentQuery + "" + strPivotForFinalAddChargeZeroOuterPercentQuery + ",[Tax Type] as [Form Type],round(([Total Amount]+cast(Additional_Charge as numeric(18,3))-[Total Tax Amount]),2) as [Purchase Amount],Round([Total Tax Amount],2) as [Total Tax Amount], Round((cast(Additional_Charge as numeric(18,3))+[Total Amount]),2) as [Total Amount],(SUBSTRING(tps.Inv_Control_Account,0,10) + [Location Code]) as [Inventory Account Code],TSPL_GL_ACCOUNTS.Description as [Inventory Account Name],[AP Document No] ,coalesce(against_PoInvoice_No, coalesce(Against_PurchaseREturn_No,coalesce(Against_MillkpurchaseInvoice_No,Against_BulkMillkpurchaseInvoice_No))) as [Against Invoice No],[AP Total Tax],[AP Total Add Charge],[AP Landed Amt],[AP Document Total],MRP, Item.HSN_Code,Purchase_Tax_Invoice " + _
        " ,case when cust.GST_Composition_scheme=1 then Round((cast(Additional_Charge as numeric(18,3))+[Total Amount]),2)  else 0 end as [Composition Amount] ,case when [Total Tax Amount]=0 then Round((cast(Additional_Charge as numeric(18,3))+[Total Amount]),2)  else 0 end as [NILL Rate Amount],case when TSPL_TAX_GROUP_MASTER.Is_Tax_Exempted=1 then Round((cast(Additional_Charge as numeric(18,3))+[Total Amount]),2)  else 0 end as [Exempted Amount] " + Environment.NewLine + Environment.NewLine + _
        " ,case when item.Skip_GST=1 then Round((cast(Additional_Charge as numeric(18,3))+[Total Amount]),2)  else 0 end as [Non-GST Amount],[Import Type],[Import Bill of Entry No],[Import Bill of Entry Date],case when [Import Type]='Yes' then Round((cast(Additional_Charge as numeric(18,3))+[Total Amount]),2)  else 0 end as [Import Bill of Entry Amount],[Original Invoice No],case when len(isnull([Original Invoice No],''))>0 then convert(varchar, [Original Invoice Date],103) else null end as [Original Invoice Date],[Reason For Revision],[ITC Eligible],[ITC Status],[ITC Details] " + Environment.NewLine

        strMainQuery += " from ( "
        strMainQuery += Environment.NewLine + Environment.NewLine + "  "
        strMainQuery += "  select max(Head_Tax_Group) as Head_Tax_Group,max(Head_Tax_Group_Type) as Head_Tax_Group_Type, case when Trans_Type ='PI' then 'Purchase Invoice' when Trans_Type ='Transfer' then 'Transfer' when Trans_Type='MCC' then 'Milk Receipt' when Trans_Type='Bulk' then 'Bulk Purchase'  when Trans_Type='Bulk Purchase Return' then 'Bulk Purchase Return' when Trans_Type ='MT' then 'Merchant Trade' end  as [Trans Type],max(final.Line_No) as Line_No,max(final.ConvRate) as ConvRate,max(TSPL_LOCATION_MASTER .location_Code) as [Location Code],max(TSPL_LOCATION_MASTER.Add1) + ' ' + max(TSPL_LOCATION_MASTER.Add2) + ' ' + max(TSPL_LOCATION_MASTER.Add3) As [Location Address],max(TSPL_LOCATION_MASTER.State) as [Location State],final.Status  ,max(TSPL_LOCATION_MASTER .Location_Desc) as [Location Name] ,(final.Invoice_Type) as [Invoice Type],final.PI_NO as [Document No],final.SRN_Id as [SRN No],final.PI_Date as [Document Date],final.Way_BillNo as [Way Bill No],Final.GRNO as [GR No],final.LR_NO as [LR No] ,max(VENDOR_INVOICE_no)as VENDOR_INVOICE_no,max(VENDOR_INVOICE_Date)as VENDOR_INVOICE_Date,vehicledesc,Vehicle_No,final.Additional_Charge+Case when coalesce(final.Additional_Charge,0)>0 then coalesce(max(PACKING),0) else 0 end as Additional_Charge,final.Customer_Code as [Vendor Code] ,max(TSPL_vendor_MASTER .vendor_Name) as [Vendor Name],max(TSPL_VENDOR_MASTER.Add1) + ' ' + max(TSPL_VENDOR_MASTER.Add2) + ' ' + max(TSPL_VENDOR_MASTER.Add3) As [Vendor Address],Max([State Code]) as [State Code],Max([State Name]) as [State Name],max(TSPL_vendor_MASTER .Tin_No) as [Vendor TIN No] ,max(TSPL_vendor_MASTER .Parent_vendor_Code) as [Parent Vendor No] ,max(Parent_Master.Vendor_Code) as [Parent Vendor Code],max(Parent_Master.Vendor_Name) as [Parent Vendor Name],Max(final.[Transporter]) as [Transporter],Max([Transporter Name]) as [Transporter Name], final.Item_Code as [Item Code],max(tspl_item_master.Item_Desc) as [Item Name],final.Qty as [Quantity],final.Unit_code as [UOM],final.Item_Cost as [Item Cost], QC.FAT_Per as [Fat Per],QC.SNF_Per as [SNF Per],0 as [Fat Kg],0 as [SNF KG],final.Amount,final.Disc_Per as [Discount Per],final.Disc_Amt as [Discount Amount],final.Amt_Less_Discount  as [Amount Less Discount],0 as [EMP],0 as [Incentive],0 as [IncentiveEMP]  " + strPivotForOuterQuery + " " + strPivotFoGrouprOuterQuery + " " + strPivotFoAddChargeZeroGrouprOuterQuery + ",max(_Type) as [Tax Type] ,(final.Total_Tax_Amt-coalesce(sum(final.NonRecoverable_Tax),0)) as [Total Tax Amount],final.Total_Amt as [Total Amount],Max([AP Document No]) as [AP Document No],Max(coalesce([AP Document Amt],0)) as [AP Document Amt],Max(coalesce([AP Document Discount Amt],0)) as [AP Document Discount Amt],Max(coalesce([AP Amount Before Tax],0)) as [AP Amount Before Tax],Max(against_PoInvoice_No) as against_PoInvoice_No,Max(Against_PurchaseREturn_No) as Against_PurchaseREturn_No,Max(coalesce([AP Total Tax],0)) as [AP Total Tax],max(coalesce([AP Total Add Charge],0)) as [AP Total Add Charge],(Max(coalesce([AP Landed Amt],0))-coalesce(sum(final.NonRecoverable_Tax),0)) as [AP Landed Amt],max(Against_MillkpurchaseInvoice_No) as Against_MillkpurchaseInvoice_No,Max(Against_BulkMillkpurchaseInvoice_No) as Against_BulkMillkpurchaseInvoice_No,Max(coalesce([AP Document Total],0)) as [AP Document Total],max(final.MRP) as MRP,coalesce(sum(final.NonRecoverable_Tax),0) as NonRecoverable_Tax,max(final.Purchase_Tax_Invoice) as Purchase_Tax_Invoice " + Environment.NewLine + _
        ",max([Import Type]) as [Import Type],max([Port]) as [Port], max([Import Bill of Entry No]) as [Import Bill of Entry No],max([Import Bill of Entry Date]) as [Import Bill of Entry Date]" + Environment.NewLine + _
        ",max([Original Invoice No]) as [Original Invoice No],max([Original Invoice Date]) as [Original Invoice Date],max([Reason For Revision]) as [Reason For Revision],max([ITC Eligible]) as [ITC Eligible],max([ITC Status]) as [ITC Status],max([ITC Details]) as [ITC Details]"
        strMainQuery += " from (" + Environment.NewLine
        strTaxColumns = " TSPL_PI_DETAIL.TAX1 ,TSPL_PI_DETAIL.TAX1_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as TAX1_Amt, TSPL_PI_DETAIL.TAX1_Rate,TSPL_PI_DETAIL.TAX1+'%' as Tax1Rate,'' as _Type,'N' as Tax_Recoverable "
        strAddChargeColumns = " ,TSPL_PI_Detail.ItemAdd_Charge_Code1 as Add_Charge_Code1 ,TSPL_PI_Detail.ItemAdd_Calc_Charge_Amt1*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Add_Charge_Amt1 "
        'strTaxNonRecoverableAmt = " tm.Tax_Recoverable "
        '' query for no tax applied
        strMainQuery += " select Head_Tax_Group,Head_Tax_Group_Type,Trans_Type,Line_No,ConvRate ,SRN_Id ,Status ,Bill_To_Location ,Customer_Code ,[State Code] ,[State Name] ,Invoice_Type ,PI_No ,PI_Date ,Way_BillNo ,GRNo ,LR_No ,Vendor_Invoice_No ,Vendor_Invoice_Date ,Transporter ,[Transporter Name] ,Item_Code ,Qty ,Unit_code ,Item_Cost ,Amount ,Disc_Per,Disc_Amt ,Amt_Less_Discount ,Total_Tax_Amt ,Total_Amt ,Vehicle_No ,VehicleDesc ,Additional_Charge ,NonRecoverable_Tax ,_Type ,Tax_Recoverable ,[AP Document No] ,[AP Document Amt],[AP Document Discount Amt] ,[AP Amount Before Tax],Against_POInvoice_No,Against_PurchaseReturn_No,[AP Total Tax],[AP Total Add Charge],[AP Landed Amt],Against_MillkPurchaseInvoice_No,Against_BulkMillkPurchaseInvoice_No,[AP Document Total],PO_ID,MRP,final1.Purchase_Tax_invoice,[Import Type],[Port], [Import Bill of Entry No],[Import Bill of Entry Date] " & _
            ",[Original Invoice No],[Original Invoice Date],[Reason For Revision],[ITC Eligible],[ITC Status],[ITC Details] " + Environment.NewLine + _
            " " + IIf(clsCommon.myLen(strPivotForInnerQuery) > 0, "," + strPivotForInnerQuery, "") + " " + IIf(clsCommon.myLen(strDoublePivotForInnerQuery) > 0, "," + strDoublePivotForInnerQuery, "") + " " + strPivotForAddChargeInnerQueryOuter + "  " & _
            " from (select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PI_DETAIL.PO_ID where 2=2 and (coalesce(TSPL_PI_DETAIL.tax1,'')='' and coalesce(TSPL_PI_DETAIL.tax2,'')='' " & _
                          " and coalesce(TSPL_PI_DETAIL.tax3,'')='' and coalesce(TSPL_PI_DETAIL.tax4,'')='' and " & _
                          " coalesce(TSPL_PI_DETAIL.tax5,'')='' and coalesce(TSPL_PI_DETAIL.tax6,'')='' and " & _
                          " coalesce(TSPL_PI_DETAIL.tax7,'')='' and coalesce(TSPL_PI_DETAIL.tax8,'')='' and " & _
                          " coalesce(TSPL_PI_DETAIL.tax9,'')='' and coalesce(TSPL_PI_DETAIL.tax10,'')='') and  convert(date,TSPL_PI_HEAD.PI_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_PI_HEAD.PI_Date ,103) <= convert(date,('" + To_Date + "'),103) )s  "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (min(tax1_rate) for Tax1Rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (sum(Add_Charge_Amt1) for Add_Charge_Code1 in (" + strPivotForAddChargeInnerQuery + "))t"
        End If


        strSDCommonQuery = " select Distinct TSPL_PI_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type, case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Trans_Type,TSPL_PI_DETAIL.Line_No,TSPL_PI_HEAD .ConvRate,TSPL_PI_DETAIL.srn_Id,TSPL_PI_HEAD.Status ,TSPL_PI_HEAD.Bill_To_Location, " & _
                          " TSPL_PI_HEAD.Vendor_Code as Customer_Code,TSPL_STATE_MASTER.STATE_CODE as [State Code],TSPL_STATE_MASTER.STATE_NAME as [State Name],case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Invoice_Type,TSPL_PI_HEAD.PI_NO , " & _
                          " convert(varchar,TSPL_PI_HEAD.PI_Date,103 ) as PI_Date,'' as Way_BillNo,TSPL_PI_HEAD.GRNO,TSPL_PI_HEAD.LR_NO,TSPL_PI_HEAD.Vendor_Invoice_No ,convert(varchar,TSPL_PI_Head.InvoiceDate,103) as Vendor_Invoice_Date,TSPL_PI_HEAD.Transporter as [Transporter],TSPL_PI_HEAD.TransporterDesc as [Transporter Name], TSPL_PI_DETAIL.Item_Code , " & _
                          " TSPL_PI_DETAIL.PI_Qty as Qty ,TSPL_PI_DETAIL.Unit_code ,TSPL_PI_DETAIL.Item_Cost*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Item_Cost , " & _
                          " TSPL_PI_DETAIL.Amount*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amount ,TSPL_PI_DETAIL.Disc_Per ,TSPL_PI_DETAIL.Disc_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Disc_Amt , " & _
                          " TSPL_PI_DETAIL.Amt_Less_Discount *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amt_Less_Discount, " & _
                          " (TSPL_PI_DETAIL.Total_Tax_Amt) *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Tax_Amt,TSPL_PI_DETAIL.Item_Net_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Amt,TSPL_PI_HEAD.vehicledesc,TSPL_VEHICLE_MASTER.Number as Vehicle_No,(TSPL_PI_Detail.Total_ItemAdd_Charge*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end)) as Additional_Charge,(case when Tax_Recoverable='N' then TSPL_PI_DETAIL.TAX1_Amt else 0 end) as NonRecoverable_Tax, "
        strTaxColumns = " TSPL_PI_DETAIL.TAX1 ,TSPL_PI_DETAIL.TAX1_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as TAX1_Amt, TSPL_PI_DETAIL.TAX1_Rate,TSPL_PI_DETAIL.TAX1+'%' as Tax1Rate,ttr._Type as _Type,tm.Tax_Recoverable "
        strAddChargeColumns = " , TSPL_PI_Detail.ItemAdd_Charge_Code1 as Add_Charge_Code1,TSPL_PI_Detail.ItemAdd_Calc_Charge_Amt1*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Add_Charge_Amt1 "

        strMainQuery += Environment.NewLine + " union all " + Environment.NewLine
        '' query for tax1 applied============BM00000008364 ''richa add date filter
        strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_PI_DETAIL.tax1 and ttr.tax_Rate=TSPL_PI_DETAIL.TAX1_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PI_DETAIL.tax1=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_PI_HEAD .Add_Charge_Code1 =AdCh .Code left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PI_DETAIL.PO_ID where 2=2    and  convert(date,TSPL_PI_HEAD.PI_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_PI_HEAD.PI_Date ,103) <= convert(date,('" + To_Date + "'),103)  )s "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += "pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (min(tax1_rate) for Tax1Rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (sum(Add_Charge_Amt1) for Add_Charge_Code1 in (" + strPivotForAddChargeInnerQuery + "))t"
        End If


        strMainQuery += Environment.NewLine + " union all " + Environment.NewLine

        strSDCommonQuery = " select Distinct TSPL_PI_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Trans_Type,TSPL_PI_DETAIL.Line_No,TSPL_PI_HEAD .ConvRate,TSPL_PI_DETAIL.srn_Id,TSPL_PI_HEAD.Status ,TSPL_PI_HEAD.Bill_To_Location, " & _
                          " TSPL_PI_HEAD.Vendor_Code as Customer_Code,TSPL_STATE_MASTER.STATE_CODE as [State Code],TSPL_STATE_MASTER.STATE_NAME as [State Name],case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Invoice_Type,TSPL_PI_HEAD.PI_NO , " & _
                          " convert(varchar,TSPL_PI_HEAD.PI_Date,103 ) as PI_Date,'' as Way_BillNo,TSPL_PI_HEAD.GRNO,TSPL_PI_HEAD.LR_NO,TSPL_PI_HEAD.Vendor_Invoice_No ,convert(varchar,TSPL_PI_Head.InvoiceDate,103) as Vendor_Invoice_Date,TSPL_PI_HEAD.Transporter as [Transporter],TSPL_PI_HEAD.TransporterDesc as [Transporter Name], TSPL_PI_DETAIL.Item_Code , " & _
                          " TSPL_PI_DETAIL.PI_Qty as Qty ,TSPL_PI_DETAIL.Unit_code ,TSPL_PI_DETAIL.Item_Cost*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Item_Cost , " & _
                          " TSPL_PI_DETAIL.Amount*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amount ,TSPL_PI_DETAIL.Disc_Per ,TSPL_PI_DETAIL.Disc_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Disc_Amt , " & _
                          " TSPL_PI_DETAIL.Amt_Less_Discount *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amt_Less_Discount, " & _
                          " (TSPL_PI_DETAIL.Total_Tax_Amt) *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Tax_Amt,TSPL_PI_DETAIL.Item_Net_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Amt,TSPL_PI_HEAD.vehicledesc,TSPL_VEHICLE_MASTER.Number as Vehicle_No,(TSPL_PI_Detail.Total_ItemAdd_Charge*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end)) as Additional_Charge,(case when Tax_Recoverable='N' then TSPL_PI_DETAIL.TAX2_Amt else 0 end) as NonRecoverable_Tax, "

        strTaxColumns = " TSPL_PI_DETAIL.TAX2 ,TSPL_PI_DETAIL.TAX2_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as TAX2_Amt,TSPL_PI_DETAIL.TAX2_Rate, TSPL_PI_DETAIL.TAX2+'%' as Tax2Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_PI_Detail.ItemAdd_Charge_Code2 as Add_Charge_Code2 ,TSPL_PI_Detail.ItemAdd_Calc_Charge_Amt2*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Add_Charge_Amt2 "

        ''add date filter richa
        strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PI_DETAIL.tax2 and ttr.tax_Rate=TSPL_PI_DETAIL.TAX2_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PI_DETAIL.tax2=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_PI_HEAD .Add_Charge_Code2 =AdCh .Code left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PI_DETAIL.PO_ID where 2=2  and  convert(date,TSPL_PI_HEAD.PI_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_PI_HEAD.PI_Date ,103) <= convert(date,('" + To_Date + "'),103)  )s "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += "pivot (sum(tax2_amt) for tax2 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (min(tax2_rate) for tax2rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (sum(Add_Charge_Amt2) for Add_Charge_Code2 in (" + strPivotForAddChargeInnerQuery + "))t "
        End If


        strMainQuery += Environment.NewLine + " union all " + Environment.NewLine

        strSDCommonQuery = " select Distinct TSPL_PI_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Trans_Type,TSPL_PI_DETAIL.Line_No,TSPL_PI_HEAD .ConvRate,TSPL_PI_DETAIL.srn_Id,TSPL_PI_HEAD.Status ,TSPL_PI_HEAD.Bill_To_Location, " & _
                         " TSPL_PI_HEAD.Vendor_Code as Customer_Code,TSPL_STATE_MASTER.STATE_CODE as [State Code],TSPL_STATE_MASTER.STATE_NAME as [State Name],case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Invoice_Type,TSPL_PI_HEAD.PI_NO , " & _
                         " convert(varchar,TSPL_PI_HEAD.PI_Date,103 ) as PI_Date,'' as Way_BillNo,TSPL_PI_HEAD.GRNO,TSPL_PI_HEAD.LR_NO,TSPL_PI_HEAD.Vendor_Invoice_No ,convert(varchar,TSPL_PI_Head.InvoiceDate,103) as Vendor_Invoice_Date,TSPL_PI_HEAD.Transporter as [Transporter],TSPL_PI_HEAD.TransporterDesc as [Transporter Name], TSPL_PI_DETAIL.Item_Code , " & _
                         " TSPL_PI_DETAIL.PI_Qty as Qty ,TSPL_PI_DETAIL.Unit_code ,TSPL_PI_DETAIL.Item_Cost*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Item_Cost , " & _
                         " TSPL_PI_DETAIL.Amount*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amount ,TSPL_PI_DETAIL.Disc_Per ,TSPL_PI_DETAIL.Disc_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Disc_Amt , " & _
                         " TSPL_PI_DETAIL.Amt_Less_Discount *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amt_Less_Discount, " & _
                         " (TSPL_PI_DETAIL.Total_Tax_Amt) *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Tax_Amt,TSPL_PI_DETAIL.Item_Net_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Amt,TSPL_PI_HEAD.vehicledesc,TSPL_VEHICLE_MASTER.Number as Vehicle_No,(TSPL_PI_Detail.Total_ItemAdd_Charge*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end)) as Additional_Charge,(case when Tax_Recoverable='N' then TSPL_PI_DETAIL.TAX3_Amt else 0 end) as NonRecoverable_Tax, "
        strTaxColumns = " TSPL_PI_DETAIL.TAX3 ,TSPL_PI_DETAIL.TAX3_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end)  as TAX3_Amt, TSPL_PI_DETAIL.TAX3_Rate, TSPL_PI_DETAIL.TAX3+'%' as Tax3Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_PI_Detail.ItemAdd_Charge_Code3 as Add_Charge_Code3 ,TSPL_PI_Detail.ItemAdd_Calc_Charge_Amt3*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Add_Charge_Amt3 "
        ''add date filter richa
        strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PI_DETAIL.tax3 and ttr.tax_Rate=TSPL_PI_DETAIL.TAX3_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PI_DETAIL.tax3=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_PI_HEAD .Add_Charge_Code3 =AdCh .Code  left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PI_DETAIL.PO_ID where 2=2  and  convert(date,TSPL_PI_HEAD.PI_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_PI_HEAD.PI_Date ,103) <= convert(date,('" + To_Date + "'),103) )s "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += "pivot (sum(tax3_amt) for tax3 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (min(tax3_rate) for tax3rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (sum(Add_Charge_Amt3) for Add_Charge_Code3 in (" + strPivotForAddChargeInnerQuery + "))t"
        End If


        strMainQuery += Environment.NewLine + " union all " + Environment.NewLine

        strSDCommonQuery = " select Distinct TSPL_PI_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Trans_Type,TSPL_PI_DETAIL.Line_No,TSPL_PI_HEAD .ConvRate,TSPL_PI_DETAIL.srn_Id,TSPL_PI_HEAD.Status ,TSPL_PI_HEAD.Bill_To_Location, " & _
                         " TSPL_PI_HEAD.Vendor_Code as Customer_Code,TSPL_STATE_MASTER.STATE_CODE as [State Code],TSPL_STATE_MASTER.STATE_NAME as [State Name],case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Invoice_Type,TSPL_PI_HEAD.PI_NO , " & _
                         " convert(varchar,TSPL_PI_HEAD.PI_Date,103 ) as PI_Date,'' as Way_BillNo,TSPL_PI_HEAD.GRNO,TSPL_PI_HEAD.LR_NO,TSPL_PI_HEAD.Vendor_Invoice_No ,convert(varchar,TSPL_PI_Head.InvoiceDate,103) as Vendor_Invoice_Date,TSPL_PI_HEAD.Transporter as [Transporter],TSPL_PI_HEAD.TransporterDesc as [Transporter Name], TSPL_PI_DETAIL.Item_Code , " & _
                         " TSPL_PI_DETAIL.PI_Qty as Qty ,TSPL_PI_DETAIL.Unit_code ,TSPL_PI_DETAIL.Item_Cost*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Item_Cost , " & _
                         " TSPL_PI_DETAIL.Amount*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amount ,TSPL_PI_DETAIL.Disc_Per ,TSPL_PI_DETAIL.Disc_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Disc_Amt , " & _
                         " TSPL_PI_DETAIL.Amt_Less_Discount *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amt_Less_Discount, " & _
                         " (TSPL_PI_DETAIL.Total_Tax_Amt) *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Tax_Amt,TSPL_PI_DETAIL.Item_Net_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Amt,TSPL_PI_HEAD.vehicledesc,TSPL_VEHICLE_MASTER.Number as Vehicle_No,(TSPL_PI_Detail.Total_ItemAdd_Charge*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end)) as Additional_Charge,(case when Tax_Recoverable='N' then TSPL_PI_DETAIL.TAX4_Amt else 0 end) as NonRecoverable_Tax, "

        strTaxColumns = " TSPL_PI_DETAIL.TAX4 ,TSPL_PI_DETAIL.TAX4_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as TAX4_Amt,TSPL_PI_DETAIL.TAX4_Rate, TSPL_PI_DETAIL.TAX4+'%' as Tax4Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_PI_Detail.ItemAdd_Charge_Code4 as Add_Charge_Code4 ,TSPL_PI_Detail.ItemAdd_Calc_Charge_Amt4*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Add_Charge_Amt4 "
        ''richa add date filter
        strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PI_DETAIL.tax4 and ttr.tax_Rate=TSPL_PI_DETAIL.TAX4_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PI_DETAIL.tax4=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_PI_HEAD .Add_Charge_Code4 =AdCh .Code left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PI_DETAIL.PO_ID where 2=2  and  convert(date,TSPL_PI_HEAD.PI_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_PI_HEAD.PI_Date ,103) <= convert(date,('" + To_Date + "'),103) )s "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += "pivot (sum(tax4_amt) for tax4 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (min(tax4_rate) for tax4rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (sum(Add_Charge_Amt4) for Add_Charge_Code4 in (" + strPivotForAddChargeInnerQuery + "))t"
        End If


        strMainQuery += Environment.NewLine + " union all " + Environment.NewLine
        strSDCommonQuery = " select Distinct TSPL_PI_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Trans_Type,TSPL_PI_DETAIL.Line_No,TSPL_PI_HEAD .ConvRate,TSPL_PI_DETAIL.srn_Id,TSPL_PI_HEAD.Status ,TSPL_PI_HEAD.Bill_To_Location, " & _
                         " TSPL_PI_HEAD.Vendor_Code as Customer_Code,TSPL_STATE_MASTER.STATE_CODE as [State Code],TSPL_STATE_MASTER.STATE_NAME as [State Name],case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Invoice_Type,TSPL_PI_HEAD.PI_NO , " & _
                         " convert(varchar,TSPL_PI_HEAD.PI_Date,103 ) as PI_Date,'' as Way_BillNo,TSPL_PI_HEAD.GRNO,TSPL_PI_HEAD.LR_NO,TSPL_PI_HEAD.Vendor_Invoice_No ,convert(varchar,TSPL_PI_Head.InvoiceDate,103) as Vendor_Invoice_Date,TSPL_PI_HEAD.Transporter as [Transporter],TSPL_PI_HEAD.TransporterDesc as [Transporter Name], TSPL_PI_DETAIL.Item_Code , " & _
                         " TSPL_PI_DETAIL.PI_Qty as Qty ,TSPL_PI_DETAIL.Unit_code ,TSPL_PI_DETAIL.Item_Cost*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Item_Cost , " & _
                         " TSPL_PI_DETAIL.Amount*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amount ,TSPL_PI_DETAIL.Disc_Per ,TSPL_PI_DETAIL.Disc_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Disc_Amt , " & _
                         " TSPL_PI_DETAIL.Amt_Less_Discount *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amt_Less_Discount, " & _
                         " (TSPL_PI_DETAIL.Total_Tax_Amt) *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Tax_Amt,TSPL_PI_DETAIL.Item_Net_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Amt,TSPL_PI_HEAD.vehicledesc,TSPL_VEHICLE_MASTER.Number as Vehicle_No,(TSPL_PI_Detail.Total_ItemAdd_Charge*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end)) as Additional_Charge,(case when Tax_Recoverable='N' then TSPL_PI_DETAIL.TAX5_Amt else 0 end) as NonRecoverable_Tax, "

        strTaxColumns = " TSPL_PI_DETAIL.TAX5 ,TSPL_PI_DETAIL.TAX5_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as TAX5_Amt,TSPL_PI_DETAIL.TAX5_Rate, TSPL_PI_DETAIL.TAX5+'%' as Tax5Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = ", TSPL_PI_Detail.ItemAdd_Charge_Code5 as Add_Charge_Code5 ,TSPL_PI_Detail.ItemAdd_Calc_Charge_Amt5*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Add_Charge_Amt5 "
        ''richa add date filter
        strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PI_DETAIL.tax5 and ttr.tax_Rate=TSPL_PI_DETAIL.TAX5_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PI_DETAIL.tax5=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_PI_HEAD .Add_Charge_Code5 =AdCh .Code left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PI_DETAIL.PO_ID where 2=2 and  convert(date,TSPL_PI_HEAD.PI_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_PI_HEAD.PI_Date ,103) <= convert(date,('" + To_Date + "'),103) )s "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += "pivot (sum(tax5_amt) for tax5 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (min(tax5_rate) for tax5rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (sum(Add_Charge_Amt5) for Add_Charge_Code5 in (" + strPivotForAddChargeInnerQuery + "))t "
        End If


        strMainQuery += Environment.NewLine + " union all " + Environment.NewLine

        strSDCommonQuery = " select Distinct TSPL_PI_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Trans_Type,TSPL_PI_DETAIL.Line_No,TSPL_PI_HEAD .ConvRate,TSPL_PI_DETAIL.srn_Id,TSPL_PI_HEAD.Status ,TSPL_PI_HEAD.Bill_To_Location, " & _
                         " TSPL_PI_HEAD.Vendor_Code as Customer_Code,TSPL_STATE_MASTER.STATE_CODE as [State Code],TSPL_STATE_MASTER.STATE_NAME as [State Name],case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Invoice_Type,TSPL_PI_HEAD.PI_NO , " & _
                         " convert(varchar,TSPL_PI_HEAD.PI_Date,103 ) as PI_Date,'' as Way_BillNo,TSPL_PI_HEAD.GRNO,TSPL_PI_HEAD.LR_NO,TSPL_PI_HEAD.Vendor_Invoice_No ,convert(varchar,TSPL_PI_Head.InvoiceDate,103) as Vendor_Invoice_Date,TSPL_PI_HEAD.Transporter as [Transporter],TSPL_PI_HEAD.TransporterDesc as [Transporter Name], TSPL_PI_DETAIL.Item_Code , " & _
                         " TSPL_PI_DETAIL.PI_Qty as Qty ,TSPL_PI_DETAIL.Unit_code ,TSPL_PI_DETAIL.Item_Cost*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Item_Cost , " & _
                         " TSPL_PI_DETAIL.Amount*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amount ,TSPL_PI_DETAIL.Disc_Per ,TSPL_PI_DETAIL.Disc_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Disc_Amt , " & _
                         " TSPL_PI_DETAIL.Amt_Less_Discount *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amt_Less_Discount, " & _
                         " (TSPL_PI_DETAIL.Total_Tax_Amt) *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Tax_Amt,TSPL_PI_DETAIL.Item_Net_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Amt,TSPL_PI_HEAD.vehicledesc,TSPL_VEHICLE_MASTER.Number as Vehicle_No,(TSPL_PI_Detail.Total_ItemAdd_Charge*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end)) as Additional_Charge,(case when Tax_Recoverable='N' then TSPL_PI_DETAIL.TAX6_Amt else 0 end) as NonRecoverable_Tax, "
        strTaxColumns = " TSPL_PI_DETAIL.TAX6 ,TSPL_PI_DETAIL.TAX6_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as TAX6_Amt,TSPL_PI_DETAIL.TAX6_Rate, TSPL_PI_DETAIL.TAX6+'%' as Tax6Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_PI_Detail.ItemAdd_Charge_Code6 as Add_Charge_Code6 ,TSPL_PI_Detail.ItemAdd_Calc_Charge_Amt6*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Add_Charge_Amt6 "
        ''richa add date filter 
        strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PI_DETAIL.tax6 and ttr.tax_Rate=TSPL_PI_DETAIL.TAX6_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PI_DETAIL.tax6=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_PI_HEAD .Add_Charge_Code6 =AdCh .Code left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PI_DETAIL.PO_ID where 2=2  and  convert(date,TSPL_PI_HEAD.PI_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_PI_HEAD.PI_Date ,103) <= convert(date,('" + To_Date + "'),103) )s "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += "pivot (sum(tax6_amt) for tax6 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (min(tax6_rate) for tax6rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (sum(Add_Charge_Amt6) for Add_Charge_Code6 in (" + strPivotForAddChargeInnerQuery + "))t"
        End If


        strMainQuery += Environment.NewLine + " union all " + Environment.NewLine

        strSDCommonQuery = " select Distinct TSPL_PI_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Trans_Type,TSPL_PI_DETAIL.Line_No,TSPL_PI_HEAD .ConvRate,TSPL_PI_DETAIL.srn_Id,TSPL_PI_HEAD.Status ,TSPL_PI_HEAD.Bill_To_Location, " & _
                         " TSPL_PI_HEAD.Vendor_Code as Customer_Code,TSPL_STATE_MASTER.STATE_CODE as [State Code],TSPL_STATE_MASTER.STATE_NAME as [State Name],case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Invoice_Type,TSPL_PI_HEAD.PI_NO , " & _
                         " convert(varchar,TSPL_PI_HEAD.PI_Date,103 ) as PI_Date,'' as Way_BillNo,TSPL_PI_HEAD.GRNO,TSPL_PI_HEAD.LR_NO,TSPL_PI_HEAD.Vendor_Invoice_No ,convert(varchar,TSPL_PI_Head.InvoiceDate,103) as Vendor_Invoice_Date,TSPL_PI_HEAD.Transporter as [Transporter],TSPL_PI_HEAD.TransporterDesc as [Transporter Name], TSPL_PI_DETAIL.Item_Code , " & _
                         " TSPL_PI_DETAIL.PI_Qty as Qty ,TSPL_PI_DETAIL.Unit_code ,TSPL_PI_DETAIL.Item_Cost*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Item_Cost , " & _
                         " TSPL_PI_DETAIL.Amount*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amount ,TSPL_PI_DETAIL.Disc_Per ,TSPL_PI_DETAIL.Disc_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Disc_Amt , " & _
                         " TSPL_PI_DETAIL.Amt_Less_Discount *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amt_Less_Discount, " & _
                         " (TSPL_PI_DETAIL.Total_Tax_Amt) *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Tax_Amt,TSPL_PI_DETAIL.Item_Net_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Amt,TSPL_PI_HEAD.vehicledesc,TSPL_VEHICLE_MASTER.Number as Vehicle_No,(TSPL_PI_Detail.Total_ItemAdd_Charge*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end)) as Additional_Charge,(case when Tax_Recoverable='N' then TSPL_PI_DETAIL.TAX7_Amt else 0 end) as NonRecoverable_Tax, "
        strTaxColumns = " TSPL_PI_DETAIL.TAX7 ,TSPL_PI_DETAIL.TAX7_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as TAX7_Amt,TSPL_PI_DETAIL.TAX7_Rate, TSPL_PI_DETAIL.TAX7+'%' as Tax7Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_PI_Detail.ItemAdd_Charge_Code7 as Add_Charge_Code7 ,TSPL_PI_Detail.ItemAdd_Calc_Charge_Amt7*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Add_Charge_Amt7 "
        ''richa add date filter
        strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PI_DETAIL.tax7 and ttr.tax_Rate=TSPL_PI_DETAIL.TAX7_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PI_DETAIL.tax7=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_PI_HEAD .Add_Charge_Code7 =AdCh .Code left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PI_DETAIL.PO_ID where 2=2  and  convert(date,TSPL_PI_HEAD.PI_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_PI_HEAD.PI_Date ,103) <= convert(date,('" + To_Date + "'),103) )s "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += "pivot (sum(tax7_amt) for tax7 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (min(tax7_rate) for tax7rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (sum(Add_Charge_Amt7) for Add_Charge_Code7 in (" + strPivotForAddChargeInnerQuery + "))t "
        End If



        strMainQuery += Environment.NewLine + " union all " + Environment.NewLine

        strSDCommonQuery = " select Distinct TSPL_PI_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Trans_Type,TSPL_PI_DETAIL.Line_No,TSPL_PI_HEAD .ConvRate,TSPL_PI_DETAIL.srn_Id,TSPL_PI_HEAD.Status ,TSPL_PI_HEAD.Bill_To_Location, " & _
                         " TSPL_PI_HEAD.Vendor_Code as Customer_Code,TSPL_STATE_MASTER.STATE_CODE as [State Code],TSPL_STATE_MASTER.STATE_NAME as [State Name],case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Invoice_Type,TSPL_PI_HEAD.PI_NO , " & _
                         " convert(varchar,TSPL_PI_HEAD.PI_Date,103 ) as PI_Date,'' as Way_BillNo,TSPL_PI_HEAD.GRNO,TSPL_PI_HEAD.LR_NO,TSPL_PI_HEAD.Vendor_Invoice_No ,convert(varchar,TSPL_PI_Head.InvoiceDate,103) as Vendor_Invoice_Date,TSPL_PI_HEAD.Transporter as [Transporter],TSPL_PI_HEAD.TransporterDesc as [Transporter Name], TSPL_PI_DETAIL.Item_Code , " & _
                         " TSPL_PI_DETAIL.PI_Qty as Qty ,TSPL_PI_DETAIL.Unit_code ,TSPL_PI_DETAIL.Item_Cost*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Item_Cost , " & _
                         " TSPL_PI_DETAIL.Amount*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amount ,TSPL_PI_DETAIL.Disc_Per ,TSPL_PI_DETAIL.Disc_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Disc_Amt , " & _
                         " TSPL_PI_DETAIL.Amt_Less_Discount *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amt_Less_Discount, " & _
                         " (TSPL_PI_DETAIL.Total_Tax_Amt) *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Tax_Amt,TSPL_PI_DETAIL.Item_Net_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Amt,TSPL_PI_HEAD.vehicledesc,TSPL_VEHICLE_MASTER.Number as Vehicle_No,(TSPL_PI_Detail.Total_ItemAdd_Charge*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end)) as Additional_Charge,(case when Tax_Recoverable='N' then TSPL_PI_DETAIL.TAX8_Amt else 0 end) as NonRecoverable_Tax, "
        strTaxColumns = " TSPL_PI_DETAIL.TAX8 ,TSPL_PI_DETAIL.TAX8_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as TAX8_Amt,TSPL_PI_DETAIL.TAX8_Rate, TSPL_PI_DETAIL.TAX8+'%' as Tax8Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_PI_Detail.ItemAdd_Charge_Code8 as Add_Charge_Code8 ,TSPL_PI_Detail.ItemAdd_Calc_Charge_Amt8*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Add_Charge_Amt8 "
        ''richa add date filter
        strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PI_DETAIL.tax8 and ttr.tax_Rate=TSPL_PI_DETAIL.TAX8_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PI_DETAIL.tax8=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_PI_HEAD .Add_Charge_Code8 =AdCh .Code left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PI_DETAIL.PO_ID where 2=2  and  convert(date,TSPL_PI_HEAD.PI_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_PI_HEAD.PI_Date ,103) <= convert(date,('" + To_Date + "'),103) )s "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += "pivot (sum(tax8_amt) for tax8 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (min(tax8_rate) for tax8rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (sum(Add_Charge_Amt8) for Add_Charge_Code8 in (" + strPivotForAddChargeInnerQuery + "))t "
        End If



        strMainQuery += Environment.NewLine + " union all " + Environment.NewLine

        strSDCommonQuery = " select Distinct TSPL_PI_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Trans_Type,TSPL_PI_DETAIL.Line_No,TSPL_PI_HEAD .ConvRate,TSPL_PI_DETAIL.srn_Id,TSPL_PI_HEAD.Status ,TSPL_PI_HEAD.Bill_To_Location, " & _
                         " TSPL_PI_HEAD.Vendor_Code as Customer_Code,TSPL_STATE_MASTER.STATE_CODE as [State Code],TSPL_STATE_MASTER.STATE_NAME as [State Name],case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Invoice_Type,TSPL_PI_HEAD.PI_NO , " & _
                         " convert(varchar,TSPL_PI_HEAD.PI_Date,103 ) as PI_Date,'' as Way_BillNo,TSPL_PI_HEAD.GRNO,TSPL_PI_HEAD.LR_NO,TSPL_PI_HEAD.Vendor_Invoice_No ,convert(varchar,TSPL_PI_Head.InvoiceDate,103) as Vendor_Invoice_Date,TSPL_PI_HEAD.Transporter as [Transporter],TSPL_PI_HEAD.TransporterDesc as [Transporter Name], TSPL_PI_DETAIL.Item_Code , " & _
                         " TSPL_PI_DETAIL.PI_Qty as Qty ,TSPL_PI_DETAIL.Unit_code ,TSPL_PI_DETAIL.Item_Cost*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Item_Cost , " & _
                         " TSPL_PI_DETAIL.Amount*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amount ,TSPL_PI_DETAIL.Disc_Per ,TSPL_PI_DETAIL.Disc_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Disc_Amt , " & _
                         " TSPL_PI_DETAIL.Amt_Less_Discount *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amt_Less_Discount, " & _
                         " (TSPL_PI_DETAIL.Total_Tax_Amt) *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Tax_Amt,TSPL_PI_DETAIL.Item_Net_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Amt,TSPL_PI_HEAD.vehicledesc,TSPL_VEHICLE_MASTER.Number as Vehicle_No,(TSPL_PI_Detail.Total_ItemAdd_Charge*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end)) as Additional_Charge,(case when Tax_Recoverable='N' then TSPL_PI_DETAIL.TAX9_Amt else 0 end) as NonRecoverable_Tax, "

        strTaxColumns = " TSPL_PI_DETAIL.TAX9 ,TSPL_PI_DETAIL.TAX9_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as TAX9_Amt,TSPL_PI_DETAIL.TAX9_Rate, TSPL_PI_DETAIL.TAX9+'%' as Tax9Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_PI_Detail.ItemAdd_Charge_Code9 as Add_Charge_Code9 ,TSPL_PI_Detail.ItemAdd_Calc_Charge_Amt9*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Add_Charge_Amt9 "
        ''richa add date filter
        strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PI_DETAIL.tax9 and ttr.tax_Rate=TSPL_PI_DETAIL.TAX9_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PI_DETAIL.tax9=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_PI_HEAD .Add_Charge_Code9 =AdCh .Code left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PI_DETAIL.PO_ID where 2=2  and  convert(date,TSPL_PI_HEAD.PI_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_PI_HEAD.PI_Date ,103) <= convert(date,('" + To_Date + "'),103) )s "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += "pivot (sum(tax9_amt) for tax9 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (min(tax9_rate) for tax9rate in (" + strDoublePivotForInnerQuery + "))t  "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += "  pivot (sum(Add_Charge_Amt9) for Add_Charge_Code9 in (" + strPivotForAddChargeInnerQuery + "))t "
        End If


        strMainQuery += Environment.NewLine + " union all " + Environment.NewLine

        strSDCommonQuery = " select Distinct TSPL_PI_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Trans_Type,TSPL_PI_DETAIL.Line_No,TSPL_PI_HEAD .ConvRate,TSPL_PI_DETAIL.srn_Id,TSPL_PI_HEAD.Status ,TSPL_PI_HEAD.Bill_To_Location, " & _
                         " TSPL_PI_HEAD.Vendor_Code as Customer_Code,TSPL_STATE_MASTER.STATE_CODE as [State Code],TSPL_STATE_MASTER.STATE_NAME as [State Name],case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Invoice_Type,TSPL_PI_HEAD.PI_NO , " & _
                         " convert(varchar,TSPL_PI_HEAD.PI_Date,103 ) as PI_Date,'' as Way_BillNo,TSPL_PI_HEAD.GRNO,TSPL_PI_HEAD.LR_NO,TSPL_PI_HEAD.Vendor_Invoice_No ,convert(varchar,TSPL_PI_Head.InvoiceDate,103) as Vendor_Invoice_Date,TSPL_PI_HEAD.Transporter as [Transporter],TSPL_PI_HEAD.TransporterDesc as [Transporter Name], TSPL_PI_DETAIL.Item_Code , " & _
                         " TSPL_PI_DETAIL.PI_Qty as Qty ,TSPL_PI_DETAIL.Unit_code ,TSPL_PI_DETAIL.Item_Cost*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Item_Cost , " & _
                         " TSPL_PI_DETAIL.Amount*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amount ,TSPL_PI_DETAIL.Disc_Per ,TSPL_PI_DETAIL.Disc_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Disc_Amt , " & _
                         " TSPL_PI_DETAIL.Amt_Less_Discount *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amt_Less_Discount, " & _
                         " (TSPL_PI_DETAIL.Total_Tax_Amt) *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Tax_Amt,TSPL_PI_DETAIL.Item_Net_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Amt,TSPL_PI_HEAD.vehicledesc,TSPL_VEHICLE_MASTER.Number as Vehicle_No,(TSPL_PI_Detail.Total_ItemAdd_Charge*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end)) as Additional_Charge,(case when Tax_Recoverable='N' then TSPL_PI_DETAIL.TAX10_Amt else 0 end) as NonRecoverable_Tax, "

        strTaxColumns = " TSPL_PI_DETAIL.TAX10 ,TSPL_PI_DETAIL.TAX10_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as TAX10_Amt,TSPL_PI_DETAIL.TAX10_Rate,TSPL_PI_DETAIL.TAX10+'%' as Tax10Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_PI_Detail.ItemAdd_Charge_Code10 as Add_Charge_Code10 ,TSPL_PI_Detail.ItemAdd_Calc_Charge_Amt10*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Add_Charge_Amt10 "
        ''richa add date filter
        strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & "  left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PI_DETAIL.tax10 and ttr.tax_Rate=TSPL_PI_DETAIL.TAX10_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PI_DETAIL.tax10=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_PI_HEAD .Add_Charge_Code10 =AdCh .Code left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PI_DETAIL.PO_ID where 2=2  and  convert(date,TSPL_PI_HEAD.PI_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_PI_HEAD.PI_Date ,103) <= convert(date,('" + To_Date + "'),103) )s "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (sum(tax10_amt) for tax10 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (min(tax10_rate) for tax10rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when column found for pivot only then pivot query run,otherwise not run.(05/12/2016)
            strMainQuery += " pivot (sum(Add_Charge_Amt10) for Add_Charge_Code10 in (" + strPivotForAddChargeInnerQuery + "))t "
        End If


        strMainQuery += " )final1)final"
        strMainQuery += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =final.Bill_To_Location "
        strMainQuery += " left outer join tspl_item_master on tspl_item_master.Item_Code =final.Item_Code "
        strMainQuery += " left outer join TSPL_vendor_MASTER on TSPL_vendor_MASTER .Vendor_Code =final.Customer_Code "
        strMainQuery += " LEFT OUTER JOIN TSPL_vendor_MASTER as Parent_Master ON Parent_Master.Vendor_Code=TSPL_vendor_MASTER.Parent_Vendor_Code "
        strMainQuery += " left outer join " & "(" & qryQC & ") as QC" & " on QC.Item_Code =final.Item_Code  Left join (select * from (select PI_No,Item_Code,Item_Net_AMt from " _
            & " tspl_Pi_Detail where  item_Code='PACKING') Pivoting pivot(SUm(Item_Net_AMt) for item_Code in ([PACKING]) ) pivoted) as pvt on pvt.PI_No=final.PI_No"
        strMainQuery += " group by  final.Trans_Type,final .Status  ,final.PI_NO,final.SRN_Id ,final.Item_Code ,final.Bill_To_Location ,final.Customer_Code ,final.Qty ,final.Total_Tax_Amt ,final.Invoice_Type ,final.PI_Date,final.Way_BillNo,Final.GRNO,final.LR_NO ,final.Unit_code ,final.Item_Cost ,final.Amount ,final.Disc_Per ,final.Disc_Amt ,final.Amt_Less_Discount ,final.Total_Amt,QC.FAT_Per,QC.SNF_Per,vehicledesc,Vehicle_No,final.Additional_Charge " ', " + strPivotFoGrouprOuterQuery + "

        strMainQuery += Environment.NewLine + Environment.NewLine + "  union all" + Environment.NewLine + Environment.NewLine


        'strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX9 ,0 as TAX9_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX9_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX9+'%' as tax9rate  "
        strMainQuery += " select * from (Select '' as Head_Tax_Group,'' as Head_Tax_Group_Type,'MCC Transfer' as Trans_Type,0 as Line_No,0 as ConvRate,recv.location_Code as  Bill_To_Location,recv.Add1 + ' ' + recv.Add2 + ' ' + recv.Add3 As Location_ADD,recv.State as [Location State],TSPL_MILK_TRANSFER_IN.isPosted as Status," _
            & " recv.location_desc,'MCC Transfer' as Invoice_Type,TSPL_MILK_TRANSFER_IN.Receipt_Challan_No as PI_NO,'' as SRN_Id  ,  convert(varchar,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,103 ) as PI_Date,'' as Way_BillNo,'' as [GRNO],'' as LR_NO,TSPL_MILK_TRANSFER_IN.Dispatch_Challan_No as Vendor_Invoice_No,convert(varchar,TSPL_MCC_Dispatch_Challan.Dispatch_Date,103) as Vendor_Invoice_Date," _
            & " '' as vehicledesc,tm.tanker_NO as Vehicle_No,0  as Additional_Charge ,  TSPL_MCC_Dispatch_Challan.mcc_code as Customer_Code,  TSPL_LOCATION_MASTER.Location_Desc  as Customer_Name,Tspl_Location_Master.Add1 + ' ' + Tspl_Location_Master.Add2 + ' ' + Tspl_Location_Master.Add3 As Vendor_ADD,tspl_State_Master.state_COde as [State Code],tspl_State_Master.state_Name as [State Name]" _
            & ",tspl_LocaTION_mASTER.tin_No as [TIN No] ,'' as [Parent Vendor No],'' as [Parent Vendor Code],'' as [Parent Vendor Name],Tanker_Transporter_Code,tm.description, TSPL_MCC_Dispatch_Challan.Item_Code," _
            & " TSPL_MCC_Dispatch_Challan.Item_Desc , t_Qty_Recd.Net_Weight  as Qty ,t_Qty_Recd.UOM as  Unit_code ,round(((TSPL_MCC_Dispatch_Challan.FAT_RATE *(coalesce(cast(t_FAT_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)) +(TSPL_MCC_Dispatch_Challan.SNF_RATE  *(coalesce(cast(t_SNF_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)))/coalesce(t_qty_recd.net_Weight,1) ,2) as  Item_Cost ,  t_FAT_Recd.Param_Field_Value as [FAT Per],  t_SNF_Recd.Param_Field_Value as [SNF Per],(coalesce(cast(t_FAT_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100) as [FAT KG],(coalesce(cast(t_Snf_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100) as [SNF KG],Round(Round(((TSPL_MCC_Dispatch_Challan.FAT_RATE *(coalesce(cast(t_FAT_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)) +(TSPL_MCC_Dispatch_Challan.SNF_RATE  *(coalesce(cast(t_SNF_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)))*100,2)/100,2) as  Amount ,0 as Disc_Per " _
            & " ,0 as Disc_Amt ,  Round(Round(((TSPL_MCC_Dispatch_Challan.FAT_RATE *(coalesce(cast(t_FAT_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)) +(TSPL_MCC_Dispatch_Challan.SNF_RATE  *(coalesce(cast(t_SNF_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)))*100,2)/100,2) as  Amt_Less_Discount,0 as [EMP],0 as [Incentive],0 as [IncentiveEMP]   " & strPivotForTransfer_In & "" & strPivotFortRANSFER_INPercentQuery & " " & strPivotForAddChargeForZeroTransfer_In & ",'' as [Tax Type],  0 as Total_Tax_Amt ,Round(Round(((TSPL_MCC_Dispatch_Challan.FAT_RATE *(coalesce(cast(t_FAT_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)) +(TSPL_MCC_Dispatch_Challan.SNF_RATE  *(coalesce(cast(t_SNF_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)))*100,2)/100,2) as   Total_Amt,TSPL_vendor_Invoice_Head.Document_No as [AP Document No],TSPL_vendor_Invoice_Head.Document_Total [AP Document Amt],TSPL_vendor_Invoice_Head.Discount_Amount as [AP Document Discount Amt],TSPL_vendor_Invoice_Head.amount_less_Discount as [AP Amount Before Tax],TSPL_MILK_TRANSFER_IN.Dispatch_Challan_No as against_PoInvoice_No,TSPL_vendor_Invoice_Head.Against_PurchaseREturn_No,TSPL_vendor_Invoice_Head.total_tax as [AP Total Tax],TSPL_vendor_Invoice_Head.total_Add_Charge as [AP Total Add Charge],TSPL_vendor_Invoice_Head.Total_landed_Amt as [AP Landed Amt],TSPL_vendor_Invoice_Head.Against_MillkpurchaseInvoice_No,TSPL_vendor_Invoice_Head.Against_BulkMillkpurchaseInvoice_No,TSPL_vendor_Invoice_Head.Document_total as [AP Document Total],0 as MRP,0 as NonRecoverable_Tax,'' as Purchase_Tax_Invoice" + Environment.NewLine + _
            ",null as [Import Type],null  as [Port], null as [Import Bill of Entry No],null as [Import Bill of Entry Date] " + Environment.NewLine + _
            ",'' as [Original Invoice No],'' as [Original Invoice Date],'' as [Reason For Revision],'' as [ITC Eligible],'' as [ITC Status],'' as [ITC Details]" + Environment.NewLine + _
            " from TSPL_MCC_Dispatch_Challan  left outer " _
            & " join TSPL_MILK_TRANSFER_IN on TSPL_MILK_TRANSFER_IN.Dispatch_Challan_No =TSPL_MCC_Dispatch_Challan.Chalan_NO  LEFT JOIN tspl_Mcc_Master ON tspl_Mcc_Master.MCC_Code=TSPL_MCC_Dispatch_Challan.MCC_CODE  left join tspl_Location_master on tspl_Location_master.location_code=TSPL_MCC_Dispatch_Challan.mcc_Code left join tspl_Location_master recv on recv.location_code=TSPL_MILK_TRANSFER_IN.Location_Code left join TSPL_vendor_Invoice_Head on  TSPL_vendor_Invoice_Head.vendor_Invoice_No=TSPL_MILK_TRANSFER_IN.Receipt_Challan_No left join tspl_tanker_Master tm on tm.tanker_no=TSPL_MCC_Dispatch_Challan.tanker_No Left Outer Join (Select TSPL_QC_Parameter_Detail.* From TSPL_MILK_TRANSFER_IN Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No  = TSPL_MILK_TRANSFER_IN.QC_No  where TSPL_QC_Parameter_Detail.Param_Type = 'SNF') t_SNF_Recd On t_SNF_Recd.QC_No   = TSPL_MILK_TRANSFER_IN.QC_No " _
            & " Left Outer Join (Select TSPL_QC_Parameter_Detail.* From TSPL_MILK_TRANSFER_IN Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No  = TSPL_MILK_TRANSFER_IN.QC_No  where TSPL_QC_Parameter_Detail.Param_Type = 'FAT' ) t_FAT_Recd On t_FAT_Recd.QC_No   = TSPL_MILK_TRANSFER_IN.QC_No " _
            & " Left Outer Join (Select TSPL_Weighment_Detail.* From TSPL_MILK_TRANSFER_IN Left Outer Join TSPL_Weighment_Detail On TSPL_Weighment_Detail.Weighment_No  = TSPL_MILK_TRANSFER_IN.Weighment_No ) t_Qty_Recd On t_Qty_Recd.Weighment_No   = TSPL_MILK_TRANSFER_IN.Weighment_No  left join TSPL_STATE_MASTER on tspl_State_Master.STATE_CODE=Tspl_Mcc_Master.State_Code  where  convert(date,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date ,103) <= convert(date,('" + To_Date + "'),103) )t"


        strMainQuery += Environment.NewLine + Environment.NewLine + "  union all" + Environment.NewLine + Environment.NewLine

        strMainQuery += " select * from (Select TSPL_TRANSFER_ORDER_HEAD.Tax_Group as Head_Tax_Group,'T' as Head_Tax_Group_Type,'Transfer' as Trans_Type,0 as Line_No,0 as ConvRate ,recv.location_Code as  Bill_To_Location,recv.Add1 + ' ' + recv.Add2 + ' ' + recv.Add3 As Location_ADD,recv.State as [Location State],TSPL_TRANSFER_ORDER_HEAD.Status as Status, recv.location_desc " _
        & "  ,'Transfer' as Invoice_Type,TSPL_TRANSFER_ORDER_HEAD.Document_No as PI_NO,'' as SRN_Id ,  convert(varchar,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103 ) as PI_Date,TSPL_TRANSFER_ORDER_HEAD.waybill_No as Way_BillNo,TSPL_TRANSFER_ORDER_HEAD.GR_No as [GRNO],'' as LR_NO," _
        & " TSPL_TRANSFER_ORDER_HEAD.transferoutno  as Vendor_Invoice_No,convert(varchar,Out.DOcument_Date,103) as Vendor_Invoice_Date, '' as vehicledesc,TSPL_TRANSFER_ORDER_HEAD.Vehicle_No as Vehicle_No,0  as Additional_Charge ,  Tspl_Location_Master.Location_Code as Customer_Code," _
        & " Tspl_Location_Master.Location_Desc  as Customer_Name,Tspl_Location_Master.Add1 + ' ' + Tspl_Location_Master.Add2 + ' ' + Tspl_Location_Master.Add3 As Vendor_ADD,Tspl_State_Master.state_Code as [State Code],Tspl_State_Master.state_name as [State Name],Tspl_Location_Master.tin_No as [TIN No],'' as [Parent Vendor No],'' as [Parent Vendor Code],'' as [Parent Vendor Name], " _
        & " tspl_transport_Master.Transport_id as [Transport Code],Tspl_Transport_Master.Transporter_Name as [Transporter Name],TSPL_TRANSFER_ORDER_DETAIL.Item_Code, TSPL_TRANSFER_ORDER_DETAIL.Item_Desc ,  TSPL_TRANSFER_ORDER_DETAIL.In_Qty  as Qty ,TSPL_TRANSFER_ORDER_DETAIL.Unit_code " _
        & " as  Unit_code ,coalesce(TSPL_TRANSFER_ORDER_DETAIL.Price,TSPL_TRANSFER_ORDER_DETAIL.Item_Cost)  as  Item_Cost ,   QC.FAT_Per as [FAT Per],   QC.SNF_Per as [SNF Per],cast((TSPL_TRANSFER_ORDER_DETAIL.In_Qty*Qc.SNF_Per*Stock_SU.Conversion_Factor)/(100*coalesce(StockKG.Conversion_Factor,1)) as numeric(18,3)) as [FAT KG],cast((TSPL_TRANSFER_ORDER_DETAIL.In_Qty*Qc.SNF_Per*Stock_SU.Conversion_Factor)/(100*coalesce(StockKG.Conversion_Factor,1)) as Numeric(18,3)) as [SNF KG],TSPL_TRANSFER_ORDER_DETAIL.Amount " _
        & " ,TSPL_TRANSFER_ORDER_DETAIL.Disc_Per as Disc_Per  ,TSPL_TRANSFER_ORDER_DETAIL.Disc_Amt as Disc_Amt ,  TSPL_TRANSFER_ORDER_DETAIL.Amount as  Amt_Less_Discount,0 as [EMP],0 as [Incentive],0 as [IncentiveEMP]   " _
        & " " & strTransferTaxColumns & "" & strTransferTaxPerColumns & "  " & strPivotForAddChargeForZeroTransfer_In & ",case when coalesce(out.is_AgainstformF,0)=1 then 'F' else '' end as [Tax Type],  TSPL_TRANSFER_ORDER_DETAIL.Total_Tax_Amt ," _
        & " TSPL_TRANSFER_ORDER_DETAIL.Item_Net_Amt as Total_Amt,'' as [AP Document No],0 as  [AP Document Amt],0 as [AP Document Discount Amt],0 as [AP Amount Before Tax]," _
        & " TSPL_TRANSFER_ORDER_HEAD.transferoutno as against_PoInvoice_No,'' as Against_PurchaseREturn_No,0 as [AP Total Tax],0 as [AP Total Add Charge],0 as [AP Landed Amt]," _
        & " '' as Against_MillkpurchaseInvoice_No,'' as Against_BulkMillkpurchaseInvoice_No,0 as  [AP Document Total],TSPL_TRANSFER_ORDER_DETAIL.MRP,0 as NonRecoverable_Tax,'' as Purchase_Tax_Invoice " + Environment.NewLine + _
        " ,null as [Import Type],null  as [Port], null as [Import Bill of Entry No],null as [Import Bill of Entry Date] " + Environment.NewLine + _
        ",'' as [Original Invoice No],'' as [Original Invoice Date],'' as [Reason For Revision],'' as [ITC Eligible],'' as [ITC Status],'' as [ITC Details]" + Environment.NewLine + _
        " from TSPL_TRANSFER_ORDER_HEAD " _
        & " left outer  join TSPL_TRANSFER_ORDER_DETAIL on TSPL_TRANSFER_ORDER_HEAD.Document_No =TSPL_TRANSFER_ORDER_DETAIL.Document_NO  " _
        & "  left join TSPL_TRANSFER_ORDER_HEAD out on out.Document_No=TSPL_TRANSFER_ORDER_HEAD.TransferOutNo  left join tspl_Location_master on tspl_Location_master.LOcation_Code=out.From_Location " _
        & " left join tspl_Location_master recv on recv.location_code=TSPL_TRANSFER_ORDER_Head.To_Location left join TSPL_TRANSPORT_MASTER on " _
        & " TSPL_TRANSPORT_MASTER.transport_Id=tspl_Transfer_Order_Head.Transport_Id  left outer join " & "(" & qryQC & ") as QC" & " on QC.Item_Code =" _
        & " TSPL_TRANSFER_ORDER_DETAIL.Item_Code  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on " _
        & " TSPL_TRANSFER_ORDER_DETAIL.Item_Code=Stock_SU.Item_Code and TSPL_TRANSFER_ORDER_DETAIL.Unit_code=Stock_SU.UOM_Code  " _
        & " left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='KG') as StockKG on " _
        & " TSPL_TRANSFER_ORDER_DETAIL.Item_Code=StockKG.Item_Code  " _
        & " left join TSPL_STATE_MASTER on tspl_State_Master.STATE_CODE=Tspl_Location_Master.State where TSPL_TRANSFER_ORDER_Head.Transfer_Type='I' and convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date ,103) <= convert(date,('" + To_Date + "'),103) )t"

        ''richa agarwal

        strMainQuery += Environment.NewLine + Environment.NewLine + "  union all" + Environment.NewLine + Environment.NewLine

        strMainQuery += " select * from (Select TSPL_TRANSFER_ORDER_HEAD.Tax_Group as Head_Tax_Group,'T' as Head_Tax_Group_Type,'Transfer Return' as Trans_Type,0 as Line_No,0 as ConvRate ,recv.location_Code as  Bill_To_Location, recv.Add1 + ' ' + recv.Add2 + ' ' + recv.Add3 As Location_ADD,recv.State as [Location State],TSPL_TRANSFER_ORDER_HEAD.Status as Status, recv.location_desc , " _
        & "  'Transfer Return' as Invoice_Type,TSPL_TRANSFER_RETURN.Document_No as PI_NO,'' as SRN_Id ,  convert(varchar,TSPL_TRANSFER_RETURN.Document_Date,103 ) as PI_Date,TSPL_TRANSFER_ORDER_HEAD.waybill_No as Way_BillNo,TSPL_TRANSFER_ORDER_HEAD.GR_No as [GRNO],'' as LR_NO," _
        & " TSPL_TRANSFER_ORDER_HEAD.Document_No  as Vendor_Invoice_No,convert(varchar,TSPL_TRANSFER_ORDER_HEAD.DOcument_Date,103) as Vendor_Invoice_Date, '' as vehicledesc,TSPL_TRANSFER_ORDER_HEAD.Vehicle_No as Vehicle_No,0  as Additional_Charge , Tspl_Location_Master.Location_Code as Customer_Code, Tspl_Location_Master.Location_Desc  as Customer_Name,Tspl_Location_Master.Add1 + ' ' + Tspl_Location_Master.Add2 + ' ' + Tspl_Location_Master.Add3 As Vendor_ADD,Tspl_State_Master.state_Code as [State Code],Tspl_State_Master.state_name as [State Name],Tspl_Location_Master.TIN_No as [TIN No]," _
        & " '' as [Parent Vendor No],'' as [Parent Vendor Code],'' as [Parent Vendor Name], " _
        & " tspl_transport_Master.Transport_id as [Transport Code],Tspl_Transport_Master.Transporter_Name as [Transporter Name],TSPL_TRANSFER_ORDER_DETAIL.Item_Code, TSPL_TRANSFER_ORDER_DETAIL.Item_Desc ,  -TSPL_TRANSFER_ORDER_DETAIL.In_Qty  as Qty ,TSPL_TRANSFER_ORDER_DETAIL.Unit_code " _
        & " as  Unit_code ,-coalesce(TSPL_TRANSFER_ORDER_DETAIL.Price,TSPL_TRANSFER_ORDER_DETAIL.Item_Cost)  as  Item_Cost ,   QC.FAT_Per as [FAT Per],   QC.SNF_Per as [SNF Per],cast((TSPL_TRANSFER_ORDER_DETAIL.In_Qty*Qc.SNF_Per*Stock_SU.Conversion_Factor)/(100*coalesce(StockKG.Conversion_Factor,1)) as numeric(18,3)) as [FAT KG],cast((TSPL_TRANSFER_ORDER_DETAIL.In_Qty*Qc.SNF_Per*Stock_SU.Conversion_Factor)/(100*coalesce(StockKG.Conversion_Factor,1)) as Numeric(18,3)) as [SNF KG],-TSPL_TRANSFER_ORDER_DETAIL.Amount AS Amount " _
        & " ,TSPL_TRANSFER_ORDER_DETAIL.Disc_Per as Disc_Per  ,TSPL_TRANSFER_ORDER_DETAIL.Disc_Amt as Disc_Amt , - TSPL_TRANSFER_ORDER_DETAIL.Amount as  Amt_Less_Discount,0 as [EMP],0 as [Incentive],0 as [IncentiveEMP]   " _
        & " " & strPivotForTransfer_In & "" & strPivotFortRANSFER_INPercentQuery & "  " & strPivotForAddChargeForZeroTransfer_In & ",case when coalesce(out.is_AgainstformF,0)=1 then 'F' else '' end as [Tax Type],  0 as Total_Tax_Amt ," _
        & " -TSPL_TRANSFER_ORDER_DETAIL.Amount as   Total_Amt,'' as [AP Document No],0 as  [AP Document Amt],0 as [AP Document Discount Amt],0 as [AP Amount Before Tax]," _
        & " TSPL_TRANSFER_ORDER_HEAD.Document_No as against_PoInvoice_No,'' as Against_PurchaseREturn_No,0 as [AP Total Tax],0 as [AP Total Add Charge],0 as [AP Landed Amt]," _
        & " '' as Against_MillkpurchaseInvoice_No,'' as Against_BulkMillkpurchaseInvoice_No,0 as  [AP Document Total],TSPL_TRANSFER_ORDER_DETAIL.MRP,0 as NonRecoverable_Tax,'' as Purchase_Tax_Invoice " + Environment.NewLine + _
        " ,null as [Import Type],null  as [Port], null as [Import Bill of Entry No],null as [Import Bill of Entry Date] " + Environment.NewLine + _
        ",TSPL_TRANSFER_RETURN.Transfer_No as [Original Invoice No],convert(varchar,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103) as [Original Invoice Date],'' as [Reason For Revision],'' as [ITC Eligible],'' as [ITC Status],'' as [ITC Details]" + Environment.NewLine + _
        " from TSPL_TRANSFER_ORDER_HEAD " _
        & " left outer  join TSPL_TRANSFER_ORDER_DETAIL on TSPL_TRANSFER_ORDER_HEAD.Document_No =TSPL_TRANSFER_ORDER_DETAIL.Document_NO  " _
        & "  left join TSPL_TRANSFER_ORDER_HEAD out on out.Document_No=TSPL_TRANSFER_ORDER_HEAD.TransferOutNo " _
        & "  left join TSPL_TRANSFER_RETURN on TSPL_TRANSFER_ORDER_HEAD.Document_No=TSPL_TRANSFER_RETURN.Transfer_No " _
        & " left join tspl_Location_master on tspl_Location_master.LOcation_Code=out.From_Location " _
        & " left join tspl_Location_master recv on recv.location_code=TSPL_TRANSFER_ORDER_Head.To_Location left join TSPL_TRANSPORT_MASTER on " _
        & " TSPL_TRANSPORT_MASTER.transport_Id=tspl_Transfer_Order_Head.Transport_Id  left outer join " & "(" & qryQC & ") as QC" & " on QC.Item_Code =" _
        & " TSPL_TRANSFER_ORDER_DETAIL.Item_Code  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on " _
        & " TSPL_TRANSFER_ORDER_DETAIL.Item_Code=Stock_SU.Item_Code and TSPL_TRANSFER_ORDER_DETAIL.Unit_code=Stock_SU.UOM_Code  " _
        & " left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='KG') as StockKG on " _
        & " TSPL_TRANSFER_ORDER_DETAIL.Item_Code=StockKG.Item_Code  " _
        & " left join TSPL_STATE_MASTER on tspl_State_Master.STATE_CODE=Tspl_Location_Master.State where TSPL_TRANSFER_ORDER_Head.Transfer_Type='I'  and isnull(TSPL_TRANSFER_RETURN.Document_No,'')<>'' and convert(date,TSPL_TRANSFER_RETURN.Document_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_TRANSFER_RETURN.Document_Date ,103) <= convert(date,('" + To_Date + "'),103) )t"

        ''------------



        strMainQuery += Environment.NewLine + Environment.NewLine + "  union all" + Environment.NewLine + Environment.NewLine


        strSDCommonQuery = " select Distinct Tspl_PR_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,'Purchase Return' as Trans_Type,Tspl_PR_Head.ConvRate ,TSPL_PR_DETAIL.Line_No,Tspl_PR_Head.Status ,Tspl_PR_Head.Bill_To_Location, " & _
                        " Tspl_PR_Head.Vendor_Code as Customer_Code,Tspl_STate_Master.state_Code AS [State Code],tspl_State_Master.state_name as [State Name],'Purchase Return' as Invoice_Type,Tspl_PR_Head.PR_NO ,'' as SRN_Id, " & _
                        " convert(varchar,Tspl_PR_Head.PR_Date,103 ) as PR_Date,'' as Way_BillNo,Tspl_PR_Head.GRNO,'' as LR_NO,Tspl_PR_Head.Vendor_Invoice_No ,convert(varchar,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) as Vendor_Invoice_Date,'' as [Transporter],'' as [Transporter Name], Tspl_PR_detail.Item_Code , " & _
                        "  case when Document_Type='C' then  TSPL_PR_DETAIL.PR_Qty else -1* TSPL_PR_DETAIL.PR_Qty end as Qty ,Tspl_PR_detail.Unit_code ,Tspl_PR_detail.Item_Cost , " & _
                        " case when Document_Type='C' then TSPL_PR_DETAIL.Amount else -1 * TSPL_PR_DETAIL.Amount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Amount ,Tspl_PR_detail.Disc_Per ,case when Document_Type='C' then TSPL_PR_DETAIL.Disc_Amt else -1 * TSPL_PR_DETAIL.Disc_AMt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Disc_Amt, " & _
                        " case when Document_Type='C' then TSPL_PR_DETAIL.Amt_Less_Discount else -1 * TSPL_PR_DETAIL.Amt_Less_Discount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) Amt_Less_Discount , " & _
                        " case when Document_Type='C' then Tspl_PR_detail.Total_Tax_Amt else -1 * Tspl_PR_detail.Total_Tax_Amt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Tax_Amt ,case when Document_Type='C' then Tspl_PR_detail.Item_Net_Amt else -1 * Tspl_PR_detail.Item_Net_Amt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Amt,Vehicle_No as vehicledesc,Vehicle_No as Vehicle_No,(case when Document_Type='C' then Tspl_PR_Detail.Total_ItemAdd_Charge else -1 * Tspl_PR_Detail.Total_ItemAdd_Charge end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Additional_Charge,0 as NonRecoverable_Tax, "
        strSDEndQry = ",TSPL_vendor_Invoice_Head.Document_No as [AP Document No], (case when Document_Type='C' then TSPL_vendor_Invoice_Head.Document_Total else -1 * TSPL_vendor_Invoice_Head.Document_Total end) *(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as  [AP Document Amt],(case when Document_Type='C' then TSPL_vendor_Invoice_Head.Discount_Amount else -1 * TSPL_vendor_Invoice_Head.Discount_Amount end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as [AP Document Discount Amt],(case when Document_Type='C' then TSPL_vendor_Invoice_Head.amount_less_Discount else -1 * TSPL_vendor_Invoice_Head.amount_less_Discount end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as [AP Amount Before Tax],TSPL_PR_DETAIL.PI_Id as against_PoInvoice_No,TSPL_vendor_Invoice_Head.Against_PurchaseREturn_No,(case when Document_Type='C' then TSPL_vendor_Invoice_Head.total_tax else -1 * TSPL_vendor_Invoice_Head.total_tax end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as [AP Total Tax],(case when Document_Type='C' then TSPL_vendor_Invoice_Head.total_Add_Charge else -1 * TSPL_vendor_Invoice_Head.total_Add_Charge end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as [AP Total Add Charge],(case when Document_Type='C' then TSPL_vendor_Invoice_Head.Total_landed_Amt else -1 * TSPL_vendor_Invoice_Head.Total_landed_Amt end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as [AP Landed Amt],TSPL_vendor_Invoice_Head.Against_MillkpurchaseInvoice_No,TSPL_vendor_Invoice_Head.Against_BulkMillkpurchaseInvoice_No,(case when Document_Type='C' then TSPL_vendor_Invoice_Head.Document_total else -1 * TSPL_vendor_Invoice_Head.Document_total end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as [AP Document Total] ,PI_Id,Tspl_PR_detail.MRP,TSPL_PR_DETAIL.PI_Id as [Original Invoice No],PIHeadTable.PI_Date as [Original Invoice Date],isnull(TSPL_PR_HEAD.Description,'')+' '+isnull(TSPL_PR_HEAD.Remarks,'')+' '+isnull(TSPL_PR_HEAD.Comments,'') as [Reason For Revision] from Tspl_PR_detail " & _
                           " left outer join Tspl_PR_Head on Tspl_PR_Head.PR_NO =Tspl_PR_detail.PR_NO " & Environment.NewLine + _
                           " left outer join (select PI_Date,PI_No,Description,Remarks,Comments from TSPL_PI_HEAD) as PIHeadTable on PIHeadTable.PI_No =Tspl_PR_detail.PI_Id " & Environment.NewLine + _
                           " left join TSPL_VEHICLE_MASTER on Vehicle_No=TSPL_VEHICLE_MASTER.Vehicle_Id left join TSPL_vendor_Invoice_Head on TSPL_vendor_Invoice_Head.Against_PurchaseReturn_No=Tspl_PR_Head.PR_NO  left join TSPL_vendor_master on   TSPL_vendor_master.Vendor_Code=tspl_PR_Head.Vendor_Code left join TSPL_STATE_MASTER on tspl_State_Master.STATE_CODE=TSPL_VENDOR_MASTER.State_Code " & _
                           "    "
        strMainQuery += " select  max(Head_Tax_Group) as Head_Tax_Group,max(Head_Tax_Group_Type) as Head_Tax_Group_Type,'Purchase Return'  as [Trans Type],max(Line_No)as Line_No ,max(ConvRate) as ConvRate,max(TSPL_LOCATION_MASTER .location_Code) as [Location Code],	 max(TSPL_LOCATION_MASTER.Add1) + ' ' + max(TSPL_LOCATION_MASTER.Add2) + ' ' + max(TSPL_LOCATION_MASTER.Add3) As [Location Address] ,max(TSPL_LOCATION_MASTER.State) as [Location State],final.Status  ,max(TSPL_LOCATION_MASTER .Location_Desc) as [Location Name] ,(final.Invoice_Type) as [Invoice Type],final.PR_NO as [Document No],'' as SRN_Id,final.PR_Date as [Document Date],final.Way_BillNo as [Way Bill No],Final.GRNO as [GR No],final.LR_NO as [LR No] ,max(VENDOR_INVOICE_no)as VENDOR_INVOICE_no,max(VENDOR_INVOICE_Date)as VENDOR_INVOICE_Date,vehicledesc,Vehicle_No,final.Additional_Charge,final.Customer_Code as [Vendor Code] ,max(TSPL_vendor_MASTER .vendor_Name) as [Vendor Name], max(TSPL_VENDOR_MASTER.Add1) + ' ' + max(TSPL_VENDOR_MASTER.Add2) + ' ' + max(TSPL_VENDOR_MASTER.Add3) As [Vendor Address],Max([State Code]) as [State Code],Max([State Name]) as [State Name],max(TSPL_vendor_MASTER .Tin_No) as [Vendor TIN No] ,max(TSPL_vendor_MASTER .Parent_vendor_Code) as [Parent Vendor No] ,max(Parent_Master.Vendor_Code) as [Parent Vendor Code],max(Parent_Master.Vendor_Name) as [Parent Vendor Name],Max(final.[Transporter]) as [Transporter],Max([Transporter Name]) as [Transporter Name], final.Item_Code as [Item Code],max(tspl_item_master.Item_Desc) as [Item Name],final.Qty as [Quantity],final.Unit_code as [UOM],final.Item_Cost as [Item Cost], QC.FAT_Per as [Fat Per],QC.SNF_Per as [SNF Per],0 as [Fat Kg],0 as [SNF KG],final.Amount,final.Disc_Per as [Discount Per],final.Disc_Amt as [Discount Amount],final.Amt_Less_Discount  as [Amount Less Discount],0 as [EMP],0 as [Incentive],0 as [IncentiveEMP]  " + strPivotForOuterQuery + " " + strPivotFoGrouprOuterQuery + " " + strPivotFoAddChargeZeroGrouprOuterQuery + ",max(_Type) as [Tax Type] ,(final.Total_Tax_Amt-coalesce(sum(final.NonRecoverable_Tax),0)) as [Total Tax Amount],final.Total_Amt as [Total Amount],Max([AP Document No]) as [AP Document No],Max(coalesce([AP Document Amt],0)) as [AP Document Amt],Max(coalesce([AP Document Discount Amt],0)) as [AP Document Discount Amt],Max(coalesce([AP Amount Before Tax],0)) as [AP Amount Before Tax],Max(against_PoInvoice_No) as against_PoInvoice_No,Max(Against_PurchaseREturn_No) as Against_PurchaseREturn_No,(Max(coalesce([AP Total Tax],0))-coalesce(sum(final.NonRecoverable_Tax),0)) as [AP Total Tax],max(coalesce([AP Total Add Charge],0)) as [AP Total Add Charge],(Max(coalesce([AP Landed Amt],0))-coalesce(-sum(final.NonRecoverable_Tax),0)) as [AP Landed Amt],Against_MillkpurchaseInvoice_No, Against_BulkMillkpurchaseInvoice_No,Max(coalesce([AP Document Total],0)) as [AP Document Total],max(MRP) as MRP,coalesce(sum(final.NonRecoverable_Tax),0) as NonRecoverable_Tax ,'' as Purchase_Tax_Invoice   " + Environment.NewLine + _
        "  ,null as [Import Type],null  as [Port], null as [Import Bill of Entry No],null as [Import Bill of Entry Date] " + Environment.NewLine + _
        " ,max([Original Invoice No]) as [Original Invoice No],max([Original Invoice Date]) as [Original Invoice Date],max([Reason For Revision]) as [Reason For Revision],null as [ITC Eligible],null as [ITC Status],null as [ITC Details]" + Environment.NewLine
        strMainQuery += " from ("
        strTaxColumns = " TSPL_PR_DETAIL.TAX1 ,(case when Document_Type='C' then TSPL_PR_DETAIL.TAX1_Amt else -1 * TSPL_PR_DETAIL.TAX1_Amt end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as TAX1_Amt, TSPL_PR_DETAIL.TAX1_Rate,TSPL_PR_DETAIL.TAX1+'%' as Tax1Rate,'' as _Type,'N' as Tax_Recoverable"
        strAddChargeColumns = " , TSPL_Pr_Detail.ItemAdd_Charge_Code1 as Add_Charge_Code1 ,(case when Document_Type='C' then TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt1 else -1 * TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt1 end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Add_Charge_Amt1"
        '' query for no tax applied
        strMainQuery += "  select Head_Tax_Group,Head_Tax_Group_Type,Trans_Type, Line_No,ConvRate ,Status  ,Bill_To_Location ,Customer_Code ,[State Code] ,[State Name] ,Invoice_Type ,PR_No  ,SRN_Id ,PR_Date ,Way_BillNo ,GRNo ,LR_NO ,Vendor_Invoice_No ,Vendor_Invoice_Date ,Transporter ,[Transporter Name] ,Item_Code ,Qty ,Unit_code ,Item_Cost ,Amount ,Disc_Per ,Disc_Amt ,Amt_Less_Discount ,Total_Tax_Amt ,Total_Amt ,vehicledesc ,Vehicle_No ,Additional_Charge ,NonRecoverable_Tax ,_Type,Tax_Recoverable,[AP Document No],[AP Document Amt],[AP Document Discount Amt],[AP Amount Before Tax],Against_POInvoice_No,Against_PurchaseReturn_No ,[AP Total Tax],[AP Total Add Charge],[AP Landed Amt],Against_MillkPurchaseInvoice_No,Against_BulkMillkPurchaseInvoice_No,[AP Document Total],PI_Id,MRP,[Original Invoice No],[Original Invoice Date],[Reason For Revision] " & _
            " " + IIf(clsCommon.myLen(strPivotForInnerQuery) > 0, "," + strPivotForInnerQuery, "") + " " + IIf(clsCommon.myLen(strDoublePivotForInnerQuery) > 0, "," + strDoublePivotForInnerQuery, "") + " " + strPivotForAddChargeInnerQueryOuter + " " & _
            " from( select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " where 2=2 and (coalesce(TSPL_PR_DETAIL.tax1,'')='' and coalesce(TSPL_PR_DETAIL.tax2,'')='' " & _
                          " and coalesce(TSPL_PR_DETAIL.tax3,'')='' and coalesce(TSPL_PR_DETAIL.tax4,'')='' and " & _
                          " coalesce(TSPL_PR_DETAIL.tax5,'')='' and coalesce(TSPL_PR_DETAIL.tax6,'')='' and " & _
                          " coalesce(TSPL_PR_DETAIL.tax7,'')='' and coalesce(TSPL_PR_DETAIL.tax8,'')='' and " & _
                          " coalesce(TSPL_PR_DETAIL.tax9,'')='' and coalesce(TSPL_PR_DETAIL.tax10,'')='') and convert(date,Tspl_PR_Head.PR_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,Tspl_PR_Head.PR_Date ,103) <= convert(date,('" + To_Date + "'),103) )s  "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
            strMainQuery += " pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
            strMainQuery += " pivot (min(tax1_rate) for Tax1Rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
            strMainQuery += " pivot (sum(Add_Charge_Amt1) for Add_Charge_Code1 in (" + strPivotForAddChargeInnerQuery + "))t "
        End If


        strMainQuery += Environment.NewLine + " union all " + Environment.NewLine
        '' query for tax1 applied
        strSDCommonQuery = " select Distinct Tspl_PR_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,'Purchase Return' as Trans_Type,Tspl_PR_Head.ConvRate ,TSPL_PR_DETAIL.Line_No,Tspl_PR_Head.Status ,Tspl_PR_Head.Bill_To_Location, " & _
                        " Tspl_PR_Head.Vendor_Code as Customer_Code,Tspl_STate_Master.state_Code AS [State Code],tspl_State_Master.state_name as [State Name],'Purchase Return' as Invoice_Type,Tspl_PR_Head.PR_NO ,'' as SRN_Id, " & _
                        " convert(varchar,Tspl_PR_Head.PR_Date,103 ) as PR_Date,'' as Way_BillNo,Tspl_PR_Head.GRNO,'' as LR_NO,Tspl_PR_Head.Vendor_Invoice_No ,convert(varchar,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) as Vendor_Invoice_Date,'' as [Transporter],'' as [Transporter Name], Tspl_PR_detail.Item_Code , " & _
                        " case when Document_Type='C' then  TSPL_PR_DETAIL.PR_Qty else -1* TSPL_PR_DETAIL.PR_Qty end as Qty ,Tspl_PR_detail.Unit_code ,Tspl_PR_detail.Item_Cost , " & _
                        " case when Document_Type='C' then TSPL_PR_DETAIL.Amount else -1 * TSPL_PR_DETAIL.Amount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Amount ,Tspl_PR_detail.Disc_Per ,case when Document_Type='C' then TSPL_PR_DETAIL.Disc_Amt else -1 * TSPL_PR_DETAIL.Disc_AMt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Disc_Amt, " & _
                        " case when Document_Type='C' then TSPL_PR_DETAIL.Amt_Less_Discount else -1 * TSPL_PR_DETAIL.Amt_Less_Discount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) Amt_Less_Discount , " & _
                        " case when Document_Type='C' then (Tspl_PR_detail.Total_Tax_Amt) else -1 * (Tspl_PR_detail.Total_Tax_Amt) end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Tax_Amt ,case when Document_Type='C' then Tspl_PR_detail.Item_Net_Amt else -1 * Tspl_PR_detail.Item_Net_Amt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Amt,Vehicle_No as vehicledesc,Vehicle_No as Vehicle_No,(case when Document_Type='C' then Tspl_PR_Detail.Total_ItemAdd_Charge else -1 * Tspl_PR_Detail.Total_ItemAdd_Charge end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Additional_Charge,-(case when Tax_Recoverable='N' then Tspl_PR_detail.TAX1_Amt else 0 end) as NonRecoverable_Tax, "
        strTaxColumns = " TSPL_PR_DETAIL.TAX1 ,(case when Document_Type='C' then TSPL_PR_DETAIL.TAX1_Amt else -1 * TSPL_PR_DETAIL.TAX1_Amt end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as TAX1_Amt,TSPL_PR_DETAIL.TAX1_Rate, TSPL_PR_DETAIL.TAX1+'%' as Tax1Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_Pr_Detail.ItemAdd_Charge_Code1 as Add_Charge_Code1 ,(case when Document_Type='C' then TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt1 else -1 * TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt1 end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Add_Charge_Amt1"
        ''richa add filter date
        strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PR_DETAIL.tax1 and ttr.tax_Rate=TSPL_PR_DETAIL.TAX1_Rate and ttr._type<>'OH'  left join tspl_tax_master tm on TSPL_PR_DETAIL.tax1=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_Pr_Head .Add_Charge_Code1 =AdCh .Code where 2=2  and (TSPL_PR_DETAIL.tax1<>'' or TSPL_PR_HEAD.Add_Charge_Code1<>'' ) and convert(date,Tspl_PR_Head.PR_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,Tspl_PR_Head.PR_Date ,103) <= convert(date,('" + To_Date + "'),103) )s "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
            strMainQuery += " pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t"
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
            strMainQuery += " pivot (min(tax1_rate) for Tax1Rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
            strMainQuery += " pivot (sum(Add_Charge_Amt1) for Add_Charge_Code1 in (" + strPivotForAddChargeInnerQuery + "))t "
        End If


        strMainQuery += Environment.NewLine + " union all " + Environment.NewLine

        strSDCommonQuery = " select Distinct Tspl_PR_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,'Purchase Return' as Trans_Type,Tspl_PR_Head.ConvRate ,TSPL_PR_DETAIL.Line_No,Tspl_PR_Head.Status ,Tspl_PR_Head.Bill_To_Location, " & _
                        " Tspl_PR_Head.Vendor_Code as Customer_Code,Tspl_STate_Master.state_Code AS [State Code],tspl_State_Master.state_name as [State Name],'Purchase Return' as Invoice_Type,Tspl_PR_Head.PR_NO ,'' as SRN_Id, " & _
                        " convert(varchar,Tspl_PR_Head.PR_Date,103 ) as PR_Date,'' as Way_BillNo,Tspl_PR_Head.GRNO,'' as LR_NO,Tspl_PR_Head.Vendor_Invoice_No ,convert(varchar,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) as Vendor_Invoice_Date,'' as [Transporter],'' as [Transporter Name], Tspl_PR_detail.Item_Code , " & _
                        " case when Document_Type='C' then  TSPL_PR_DETAIL.PR_Qty else -1* TSPL_PR_DETAIL.PR_Qty end as Qty ,Tspl_PR_detail.Unit_code ,Tspl_PR_detail.Item_Cost , " & _
                        " case when Document_Type='C' then TSPL_PR_DETAIL.Amount else -1 * TSPL_PR_DETAIL.Amount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Amount ,Tspl_PR_detail.Disc_Per ,case when Document_Type='C' then TSPL_PR_DETAIL.Disc_Amt else -1 * TSPL_PR_DETAIL.Disc_AMt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Disc_Amt, " & _
                        " case when Document_Type='C' then TSPL_PR_DETAIL.Amt_Less_Discount else -1 * TSPL_PR_DETAIL.Amt_Less_Discount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) Amt_Less_Discount , " & _
                        " case when Document_Type='C' then (Tspl_PR_detail.Total_Tax_Amt) else -1 * (Tspl_PR_detail.Total_Tax_Amt) end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Tax_Amt ,case when Document_Type='C' then Tspl_PR_detail.Item_Net_Amt else -1 * Tspl_PR_detail.Item_Net_Amt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Amt,Vehicle_No as vehicledesc,Vehicle_No as Vehicle_No,(case when Document_Type='C' then Tspl_PR_Detail.Total_ItemAdd_Charge else -1 * Tspl_PR_Detail.Total_ItemAdd_Charge end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Additional_Charge,-(case when Tax_Recoverable='N' then Tspl_PR_detail.TAX2_Amt else 0 end) as NonRecoverable_Tax, "

        strTaxColumns = " TSPL_PR_DETAIL.TAX2 ,(case when Document_Type='C' then TSPL_PR_DETAIL.TAX2_Amt else -1 * TSPL_PR_DETAIL.TAX2_Amt end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as TAX2_Amt,TSPL_PR_DETAIL.TAX2_Rate, TSPL_PR_DETAIL.TAX2+'%' as Tax2Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_Pr_Detail.ItemAdd_Charge_Code2 as Add_Charge_Code2 ,(case when Document_Type='C' then TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt2 else -1 * TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt2 end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Add_Charge_Amt2"
        '' add filter date richa
        strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PR_DETAIL.tax2 and ttr.tax_Rate=TSPL_PR_DETAIL.TAX2_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PR_DETAIL.tax2=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_Pr_Head .Add_Charge_Code2 =AdCh .Code where 2=2 and (TSPL_PR_DETAIL.tax2<>'' or TSPL_PR_HEAD.Add_Charge_Code2<>'' ) and convert(date,Tspl_PR_Head.PR_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,Tspl_PR_Head.PR_Date ,103) <= convert(date,('" + To_Date + "'),103) )s "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
            strMainQuery += " pivot (sum(tax2_amt) for tax2 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
            strMainQuery += " pivot (min(tax2_rate) for tax2rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
            strMainQuery += " pivot (sum(Add_Charge_Amt2) for Add_Charge_Code2 in (" + strPivotForAddChargeInnerQuery + "))t "
        End If


        strMainQuery += Environment.NewLine + " union all " + Environment.NewLine

        strSDCommonQuery = " select Distinct Tspl_PR_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,'Purchase Return' as Trans_Type,Tspl_PR_Head.ConvRate ,TSPL_PR_DETAIL.Line_No,Tspl_PR_Head.Status ,Tspl_PR_Head.Bill_To_Location, " & _
                        " Tspl_PR_Head.Vendor_Code as Customer_Code,Tspl_STate_Master.state_Code AS [State Code],tspl_State_Master.state_name as [State Name],'Purchase Return' as Invoice_Type,Tspl_PR_Head.PR_NO ,'' as SRN_Id, " & _
                        " convert(varchar,Tspl_PR_Head.PR_Date,103 ) as PR_Date,'' as Way_BillNo,Tspl_PR_Head.GRNO,'' as LR_NO,Tspl_PR_Head.Vendor_Invoice_No ,convert(varchar,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) as Vendor_Invoice_Date,'' as [Transporter],'' as [Transporter Name], Tspl_PR_detail.Item_Code , " & _
                        " case when Document_Type='C' then  TSPL_PR_DETAIL.PR_Qty else -1* TSPL_PR_DETAIL.PR_Qty end as Qty ,Tspl_PR_detail.Unit_code ,Tspl_PR_detail.Item_Cost , " & _
                        " case when Document_Type='C' then TSPL_PR_DETAIL.Amount else -1 * TSPL_PR_DETAIL.Amount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Amount ,Tspl_PR_detail.Disc_Per ,case when Document_Type='C' then TSPL_PR_DETAIL.Disc_Amt else -1 * TSPL_PR_DETAIL.Disc_AMt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Disc_Amt, " & _
                        " case when Document_Type='C' then TSPL_PR_DETAIL.Amt_Less_Discount else -1 * TSPL_PR_DETAIL.Amt_Less_Discount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) Amt_Less_Discount , " & _
                        " case when Document_Type='C' then (Tspl_PR_detail.Total_Tax_Amt) else -1 * (Tspl_PR_detail.Total_Tax_Amt) end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Tax_Amt ,case when Document_Type='C' then Tspl_PR_detail.Item_Net_Amt else -1 * Tspl_PR_detail.Item_Net_Amt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Amt,Vehicle_No as vehicledesc,Vehicle_No as Vehicle_No,(case when Document_Type='C' then Tspl_PR_Detail.Total_ItemAdd_Charge else -1 * Tspl_PR_Detail.Total_ItemAdd_Charge end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Additional_Charge,-(case when Tax_Recoverable='N' then Tspl_PR_detail.TAX3_Amt else 0 end) as NonRecoverable_Tax, "

        strTaxColumns = " TSPL_PR_DETAIL.TAX3 ,(case when Document_Type='C' then TSPL_PR_DETAIL.TAX3_Amt else -1 * TSPL_PR_DETAIL.TAX3_Amt end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as TAX3_Amt, TSPL_PR_DETAIL.TAX3_Rate, TSPL_PR_DETAIL.TAX3+'%' as Tax3Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_Pr_Detail.ItemAdd_Charge_Code3 as Add_Charge_Code3 ,(case when Document_Type='C' then TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt3 else -1 * TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt3 end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Add_Charge_Amt3"
        ''add filter date richa
        strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PR_DETAIL.tax3 and ttr.tax_Rate=TSPL_PR_DETAIL.TAX3_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PR_DETAIL.tax3=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_Pr_Head .Add_Charge_Code3 =AdCh .Code where 2=2 and (TSPL_PR_DETAIL.tax3<>'' or TSPL_PR_HEAD.Add_Charge_Code3<>'' ) and convert(date,Tspl_PR_Head.PR_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,Tspl_PR_Head.PR_Date ,103) <= convert(date,('" + To_Date + "'),103) )s "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
            strMainQuery += " pivot (sum(tax3_amt) for tax3 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
            strMainQuery += " pivot (min(tax3_rate) for tax3rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
            strMainQuery += " pivot (sum(Add_Charge_Amt3) for Add_Charge_Code3 in (" + strPivotForAddChargeInnerQuery + "))t "
        End If


        strMainQuery += Environment.NewLine + " union all " + Environment.NewLine

        strSDCommonQuery = " select Distinct Tspl_PR_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,'Purchase Return' as Trans_Type,Tspl_PR_Head.ConvRate ,TSPL_PR_DETAIL.Line_No,Tspl_PR_Head.Status ,Tspl_PR_Head.Bill_To_Location, " & _
                        " Tspl_PR_Head.Vendor_Code as Customer_Code,Tspl_STate_Master.state_Code AS [State Code],tspl_State_Master.state_name as [State Name],'Purchase Return' as Invoice_Type,Tspl_PR_Head.PR_NO ,'' as SRN_Id, " & _
                        " convert(varchar,Tspl_PR_Head.PR_Date,103 ) as PR_Date,'' as Way_BillNo,Tspl_PR_Head.GRNO,'' as LR_NO,Tspl_PR_Head.Vendor_Invoice_No ,convert(varchar,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) as Vendor_Invoice_Date,'' as [Transporter],'' as [Transporter Name], Tspl_PR_detail.Item_Code , " & _
                        " case when Document_Type='C' then  TSPL_PR_DETAIL.PR_Qty else -1* TSPL_PR_DETAIL.PR_Qty end as Qty ,Tspl_PR_detail.Unit_code ,Tspl_PR_detail.Item_Cost , " & _
                        " case when Document_Type='C' then TSPL_PR_DETAIL.Amount else -1 * TSPL_PR_DETAIL.Amount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Amount ,Tspl_PR_detail.Disc_Per ,case when Document_Type='C' then TSPL_PR_DETAIL.Disc_Amt else -1 * TSPL_PR_DETAIL.Disc_AMt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Disc_Amt, " & _
                        " case when Document_Type='C' then TSPL_PR_DETAIL.Amt_Less_Discount else -1 * TSPL_PR_DETAIL.Amt_Less_Discount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) Amt_Less_Discount , " & _
                        " case when Document_Type='C' then (Tspl_PR_detail.Total_Tax_Amt) else -1 * (Tspl_PR_detail.Total_Tax_Amt) end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Tax_Amt ,case when Document_Type='C' then Tspl_PR_detail.Item_Net_Amt else -1 * Tspl_PR_detail.Item_Net_Amt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Amt,Vehicle_No as vehicledesc,Vehicle_No as Vehicle_No,(case when Document_Type='C' then Tspl_PR_Detail.Total_ItemAdd_Charge else -1 * Tspl_PR_Detail.Total_ItemAdd_Charge end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Additional_Charge,-(case when Tax_Recoverable='N' then Tspl_PR_detail.TAX4_Amt else 0 end) as NonRecoverable_Tax, "

        strTaxColumns = " TSPL_PR_DETAIL.TAX4 ,(case when Document_Type='C' then TSPL_PR_DETAIL.TAX4_Amt else -1 * TSPL_PR_DETAIL.TAX4_Amt end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as TAX4_Amt,TSPL_PR_DETAIL.TAX4_Rate, TSPL_PR_DETAIL.TAX4+'%' as Tax4Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_PR_Detail.ItemAdd_Charge_Code4 as Add_Charge_Code4 ,(case when Document_Type='C' then TSPL_PR_Detail.ItemAdd_Calc_Charge_Amt4 else -1 * TSPL_PR_Detail.ItemAdd_Calc_Charge_Amt4 end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Add_Charge_Amt4"
        ''add filter date richa
        strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PR_DETAIL.tax4 and ttr.tax_Rate=TSPL_PR_DETAIL.TAX4_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PR_DETAIL.tax4=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_Pr_Head .Add_Charge_Code4 =AdCh .Code where 2=2 and (TSPL_PR_DETAIL.tax4<>''  or TSPL_PR_HEAD.Add_Charge_Code4 <>'') and convert(date,Tspl_PR_Head.PR_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,Tspl_PR_Head.PR_Date ,103) <= convert(date,('" + To_Date + "'),103) )s "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
            strMainQuery += " pivot (sum(tax4_amt) for tax4 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
            strMainQuery += " pivot (min(tax4_rate) for tax4rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
            strMainQuery += " pivot (sum(Add_Charge_Amt4) for Add_Charge_Code4 in (" + strPivotForAddChargeInnerQuery + "))t "
        End If



        strMainQuery += Environment.NewLine + " union all " + Environment.NewLine

        strSDCommonQuery = " select Distinct Tspl_PR_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,'Purchase Return' as Trans_Type,Tspl_PR_Head.ConvRate ,TSPL_PR_DETAIL.Line_No,Tspl_PR_Head.Status ,Tspl_PR_Head.Bill_To_Location, " & _
                        " Tspl_PR_Head.Vendor_Code as Customer_Code,Tspl_STate_Master.state_Code AS [State Code],tspl_State_Master.state_name as [State Name],'Purchase Return' as Invoice_Type,Tspl_PR_Head.PR_NO ,'' as SRN_Id, " & _
                        " convert(varchar,Tspl_PR_Head.PR_Date,103 ) as PR_Date,'' as Way_BillNo,Tspl_PR_Head.GRNO,'' as LR_NO,Tspl_PR_Head.Vendor_Invoice_No ,convert(varchar,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) as Vendor_Invoice_Date,'' as [Transporter],'' as [Transporter Name], Tspl_PR_detail.Item_Code , " & _
                        " case when Document_Type='C' then  TSPL_PR_DETAIL.PR_Qty else -1* TSPL_PR_DETAIL.PR_Qty end as Qty ,Tspl_PR_detail.Unit_code ,Tspl_PR_detail.Item_Cost , " & _
                        " case when Document_Type='C' then TSPL_PR_DETAIL.Amount else -1 * TSPL_PR_DETAIL.Amount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Amount ,Tspl_PR_detail.Disc_Per ,case when Document_Type='C' then TSPL_PR_DETAIL.Disc_Amt else -1 * TSPL_PR_DETAIL.Disc_AMt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Disc_Amt, " & _
                        " case when Document_Type='C' then TSPL_PR_DETAIL.Amt_Less_Discount else -1 * TSPL_PR_DETAIL.Amt_Less_Discount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) Amt_Less_Discount , " & _
                        " case when Document_Type='C' then (Tspl_PR_detail.Total_Tax_Amt) else -1 * (Tspl_PR_detail.Total_Tax_Amt) end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Tax_Amt ,case when Document_Type='C' then Tspl_PR_detail.Item_Net_Amt else -1 * Tspl_PR_detail.Item_Net_Amt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Amt,Vehicle_No as vehicledesc,Vehicle_No as Vehicle_No,(case when Document_Type='C' then Tspl_PR_Detail.Total_ItemAdd_Charge else -1 * Tspl_PR_Detail.Total_ItemAdd_Charge end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Additional_Charge,-(case when Tax_Recoverable='N' then Tspl_PR_detail.TAX5_Amt else 0 end) as NonRecoverable_Tax, "

        strTaxColumns = " TSPL_PR_DETAIL.TAX5 ,(case when Document_Type='C' then TSPL_PR_DETAIL.TAX5_Amt else -1 * TSPL_PR_DETAIL.TAX5_Amt end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as TAX5_Amt,TSPL_PR_DETAIL.TAX5_Rate, TSPL_PR_DETAIL.TAX5+'%' as Tax5Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_Pr_Detail.ItemAdd_Charge_Code5 as Add_Charge_Code5 ,(case when Document_Type='C' then TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt5 else -1 * TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt5 end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Add_Charge_Amt5"
        ''richa add date filter
        strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PR_DETAIL.tax5 and ttr.tax_Rate=TSPL_PR_DETAIL.TAX5_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PR_DETAIL.tax5=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_Pr_Head .Add_Charge_Code5 =AdCh .Code where 2=2 and (TSPL_PR_DETAIL.tax5<>'' or TSPL_PR_HEAD.Add_Charge_Code5<>'' ) and convert(date,Tspl_PR_Head.PR_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,Tspl_PR_Head.PR_Date ,103) <= convert(date,('" + To_Date + "'),103) )s "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
            strMainQuery += " pivot (sum(tax5_amt) for tax5 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
            strMainQuery += " pivot (min(tax5_rate) for tax5rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
            strMainQuery += " pivot (sum(Add_Charge_Amt5) for Add_Charge_Code5 in (" + strPivotForAddChargeInnerQuery + "))t "
        End If


        strMainQuery += Environment.NewLine + " union all " + Environment.NewLine

        strSDCommonQuery = " select Distinct Tspl_PR_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,'Purchase Return' as Trans_Type,Tspl_PR_Head.ConvRate ,TSPL_PR_DETAIL.Line_No,Tspl_PR_Head.Status ,Tspl_PR_Head.Bill_To_Location, " & _
                        " Tspl_PR_Head.Vendor_Code as Customer_Code,Tspl_STate_Master.state_Code AS [State Code],tspl_State_Master.state_name as [State Name],'Purchase Return' as Invoice_Type,Tspl_PR_Head.PR_NO ,'' as SRN_Id, " & _
                        " convert(varchar,Tspl_PR_Head.PR_Date,103 ) as PR_Date,'' as Way_BillNo,Tspl_PR_Head.GRNO,'' as LR_NO,Tspl_PR_Head.Vendor_Invoice_No ,convert(varchar,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) as Vendor_Invoice_Date,'' as [Transporter],'' as [Transporter Name], Tspl_PR_detail.Item_Code , " & _
                        " case when Document_Type='C' then  TSPL_PR_DETAIL.PR_Qty else -1* TSPL_PR_DETAIL.PR_Qty end as Qty ,Tspl_PR_detail.Unit_code ,Tspl_PR_detail.Item_Cost , " & _
                        " case when Document_Type='C' then TSPL_PR_DETAIL.Amount else -1 * TSPL_PR_DETAIL.Amount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Amount ,Tspl_PR_detail.Disc_Per ,case when Document_Type='C' then TSPL_PR_DETAIL.Disc_Amt else -1 * TSPL_PR_DETAIL.Disc_AMt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Disc_Amt, " & _
                        " case when Document_Type='C' then TSPL_PR_DETAIL.Amt_Less_Discount else -1 * TSPL_PR_DETAIL.Amt_Less_Discount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) Amt_Less_Discount , " & _
                        " case when Document_Type='C' then (Tspl_PR_detail.Total_Tax_Amt) else -1 * (Tspl_PR_detail.Total_Tax_Amt) end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Tax_Amt ,case when Document_Type='C' then Tspl_PR_detail.Item_Net_Amt else -1 * Tspl_PR_detail.Item_Net_Amt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Amt,Vehicle_No as vehicledesc,Vehicle_No as Vehicle_No,(case when Document_Type='C' then Tspl_PR_Detail.Total_ItemAdd_Charge else -1 * Tspl_PR_Detail.Total_ItemAdd_Charge end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Additional_Charge,-(case when Tax_Recoverable='N' then Tspl_PR_detail.TAX6_Amt else 0 end) as NonRecoverable_Tax, "

        strTaxColumns = " TSPL_PR_DETAIL.TAX6 ,(case when Document_Type='C' then TSPL_PR_DETAIL.TAX6_Amt else -1 * TSPL_PR_DETAIL.TAX6_Amt end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as TAX6_Amt,TSPL_PR_DETAIL.TAX6_Rate, TSPL_PR_DETAIL.TAX6+'%' as Tax6Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_Pr_Detail.ItemAdd_Charge_Code6 as Add_Charge_Code6 ,(case when Document_Type='C' then TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt6 else -1 * TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt6 end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Add_Charge_Amt6"
        ''richa add date filter
        strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PR_DETAIL.tax6 and ttr.tax_Rate=TSPL_PR_DETAIL.TAX6_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PR_DETAIL.tax6=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_Pr_Head .Add_Charge_Code6 =AdCh .Code where 2=2 and (TSPL_PR_DETAIL.tax6<>'' or TSPL_PR_HEAD.Add_Charge_Code6<>'' ) and convert(date,Tspl_PR_Head.PR_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,Tspl_PR_Head.PR_Date ,103) <= convert(date,('" + To_Date + "'),103) )s "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
            strMainQuery += " pivot (sum(tax6_amt) for tax6 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
            strMainQuery += " pivot (min(tax6_rate) for tax6rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
            strMainQuery += " pivot (sum(Add_Charge_Amt6) for Add_Charge_Code6 in (" + strPivotForAddChargeInnerQuery + "))t"
        End If


        strMainQuery += Environment.NewLine + " union all " + Environment.NewLine

        strSDCommonQuery = " select Distinct Tspl_PR_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,'Purchase Return' as Trans_Type,Tspl_PR_Head.ConvRate ,TSPL_PR_DETAIL.Line_No,Tspl_PR_Head.Status ,Tspl_PR_Head.Bill_To_Location, " & _
                        " Tspl_PR_Head.Vendor_Code as Customer_Code,Tspl_STate_Master.state_Code AS [State Code],tspl_State_Master.state_name as [State Name],'Purchase Return' as Invoice_Type,Tspl_PR_Head.PR_NO ,'' as SRN_Id, " & _
                        " convert(varchar,Tspl_PR_Head.PR_Date,103 ) as PR_Date,'' as Way_BillNo,Tspl_PR_Head.GRNO,'' as LR_NO,Tspl_PR_Head.Vendor_Invoice_No ,convert(varchar,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) as Vendor_Invoice_Date,'' as [Transporter],'' as [Transporter Name], Tspl_PR_detail.Item_Code , " & _
                        " case when Document_Type='C' then  TSPL_PR_DETAIL.PR_Qty else -1* TSPL_PR_DETAIL.PR_Qty end as Qty ,Tspl_PR_detail.Unit_code ,Tspl_PR_detail.Item_Cost , " & _
                        " case when Document_Type='C' then TSPL_PR_DETAIL.Amount else -1 * TSPL_PR_DETAIL.Amount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Amount ,Tspl_PR_detail.Disc_Per ,case when Document_Type='C' then TSPL_PR_DETAIL.Disc_Amt else -1 * TSPL_PR_DETAIL.Disc_AMt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Disc_Amt, " & _
                        " case when Document_Type='C' then TSPL_PR_DETAIL.Amt_Less_Discount else -1 * TSPL_PR_DETAIL.Amt_Less_Discount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) Amt_Less_Discount , " & _
                        " case when Document_Type='C' then (Tspl_PR_detail.Total_Tax_Amt) else -1 * (Tspl_PR_detail.Total_Tax_Amt) end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Tax_Amt ,case when Document_Type='C' then Tspl_PR_detail.Item_Net_Amt else -1 * Tspl_PR_detail.Item_Net_Amt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Amt,Vehicle_No as vehicledesc,Vehicle_No as Vehicle_No,(case when Document_Type='C' then Tspl_PR_Detail.Total_ItemAdd_Charge else -1 * Tspl_PR_Detail.Total_ItemAdd_Charge end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Additional_Charge,-(case when Tax_Recoverable='N' then Tspl_PR_detail.TAX7_Amt else 0 end) as NonRecoverable_Tax, "

        strTaxColumns = " TSPL_PR_DETAIL.TAX7 ,(case when Document_Type='C' then TSPL_PR_DETAIL.TAX7_Amt else -1 * TSPL_PR_DETAIL.TAX7_Amt end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as TAX7_Amt,TSPL_PR_DETAIL.TAX7_Rate, TSPL_PR_DETAIL.TAX7+'%' as Tax7Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_Pr_Detail.ItemAdd_Charge_Code7 as Add_Charge_Code7 ,(case when Document_Type='C' then TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt7 else -1 * TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt7 end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Add_Charge_Amt7"
        ''richa add date filter
        strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PR_DETAIL.tax7 and ttr.tax_Rate=TSPL_PR_DETAIL.TAX7_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PR_DETAIL.tax7=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_Pr_Head .Add_Charge_Code7 =AdCh .Code  where 2=2 and (TSPL_PR_DETAIL.tax7<>''or TSPL_PR_HEAD.Add_Charge_Code7 <>'') and convert(date,Tspl_PR_Head.PR_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,Tspl_PR_Head.PR_Date ,103) <= convert(date,('" + To_Date + "'),103) )s "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
            strMainQuery += " pivot (sum(tax7_amt) for tax7 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
            strMainQuery += " pivot (min(tax7_rate) for tax7rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
            strMainQuery += " pivot (sum(Add_Charge_Amt7) for Add_Charge_Code7 in (" + strPivotForAddChargeInnerQuery + "))t"
        End If


        strMainQuery += Environment.NewLine + " union all " + Environment.NewLine

        strSDCommonQuery = " select Distinct Tspl_PR_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,'Purchase Return' as Trans_Type,Tspl_PR_Head.ConvRate ,TSPL_PR_DETAIL.Line_No,Tspl_PR_Head.Status ,Tspl_PR_Head.Bill_To_Location, " & _
                        " Tspl_PR_Head.Vendor_Code as Customer_Code,Tspl_STate_Master.state_Code AS [State Code],tspl_State_Master.state_name as [State Name],'Purchase Return' as Invoice_Type,Tspl_PR_Head.PR_NO ,'' as SRN_Id, " & _
                        " convert(varchar,Tspl_PR_Head.PR_Date,103 ) as PR_Date,'' as Way_BillNo,Tspl_PR_Head.GRNO,'' as LR_NO,Tspl_PR_Head.Vendor_Invoice_No ,convert(varchar,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) as Vendor_Invoice_Date,'' as [Transporter],'' as [Transporter Name], Tspl_PR_detail.Item_Code , " & _
                        " case when Document_Type='C' then  TSPL_PR_DETAIL.PR_Qty else -1* TSPL_PR_DETAIL.PR_Qty end as Qty ,Tspl_PR_detail.Unit_code ,Tspl_PR_detail.Item_Cost , " & _
                        " case when Document_Type='C' then TSPL_PR_DETAIL.Amount else -1 * TSPL_PR_DETAIL.Amount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Amount ,Tspl_PR_detail.Disc_Per ,case when Document_Type='C' then TSPL_PR_DETAIL.Disc_Amt else -1 * TSPL_PR_DETAIL.Disc_AMt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Disc_Amt, " & _
                        " case when Document_Type='C' then TSPL_PR_DETAIL.Amt_Less_Discount else -1 * TSPL_PR_DETAIL.Amt_Less_Discount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) Amt_Less_Discount , " & _
                        " case when Document_Type='C' then (Tspl_PR_detail.Total_Tax_Amt) else -1 * (Tspl_PR_detail.Total_Tax_Amt) end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Tax_Amt ,case when Document_Type='C' then Tspl_PR_detail.Item_Net_Amt else -1 * Tspl_PR_detail.Item_Net_Amt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Amt,Vehicle_No as vehicledesc,Vehicle_No as Vehicle_No,(case when Document_Type='C' then Tspl_PR_Detail.Total_ItemAdd_Charge else -1 * Tspl_PR_Detail.Total_ItemAdd_Charge end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Additional_Charge,-(case when Tax_Recoverable='N' then Tspl_PR_detail.TAX8_Amt else 0 end) as NonRecoverable_Tax, "

        strTaxColumns = " TSPL_PR_DETAIL.TAX8 ,(case when Document_Type='C' then TSPL_PR_DETAIL.TAX8_Amt else -1 * TSPL_PR_DETAIL.TAX8_Amt end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as TAX8_Amt,TSPL_PR_DETAIL.TAX8_Rate, TSPL_PR_DETAIL.TAX8+'%' as Tax8Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_PR_Detail.ItemAdd_Charge_Code8 as Add_Charge_Code8 ,(case when Document_Type='C' then TSPL_PR_Detail.ItemAdd_Calc_Charge_Amt8 else -1 * TSPL_PR_Detail.ItemAdd_Calc_Charge_Amt8 end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Add_Charge_Amt8"
        ''richa add date filter
        strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PR_DETAIL.tax8 and ttr.tax_Rate=TSPL_PR_DETAIL.TAX8_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PR_DETAIL.tax8=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_Pr_Head .Add_Charge_Code8 =AdCh .Code where 2=2 and (TSPL_PR_DETAIL.tax8<>''or TSPL_PR_HEAD.Add_Charge_Code8<>'' ) and convert(date,Tspl_PR_Head.PR_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,Tspl_PR_Head.PR_Date ,103) <= convert(date,('" + To_Date + "'),103) )s "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
            strMainQuery += " pivot (sum(tax8_amt) for tax8 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
            strMainQuery += " pivot (min(tax8_rate) for tax8rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
            strMainQuery += " pivot (sum(Add_Charge_Amt8) for Add_Charge_Code8 in (" + strPivotForAddChargeInnerQuery + "))t"
        End If



        strMainQuery += Environment.NewLine + " union all " + Environment.NewLine

        strSDCommonQuery = " select Distinct Tspl_PR_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,'Purchase Return' as Trans_Type,Tspl_PR_Head.ConvRate ,TSPL_PR_DETAIL.Line_No,Tspl_PR_Head.Status ,Tspl_PR_Head.Bill_To_Location, " & _
                        " Tspl_PR_Head.Vendor_Code as Customer_Code,Tspl_STate_Master.state_Code AS [State Code],tspl_State_Master.state_name as [State Name],'Purchase Return' as Invoice_Type,Tspl_PR_Head.PR_NO ,'' as SRN_Id, " & _
                        " convert(varchar,Tspl_PR_Head.PR_Date,103 ) as PR_Date,'' as Way_BillNo,Tspl_PR_Head.GRNO,'' as LR_NO,Tspl_PR_Head.Vendor_Invoice_No ,convert(varchar,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) as Vendor_Invoice_Date,'' as [Transporter],'' as [Transporter Name], Tspl_PR_detail.Item_Code , " & _
                        " case when Document_Type='C' then  TSPL_PR_DETAIL.PR_Qty else -1* TSPL_PR_DETAIL.PR_Qty end as Qty ,Tspl_PR_detail.Unit_code ,Tspl_PR_detail.Item_Cost , " & _
                        " case when Document_Type='C' then TSPL_PR_DETAIL.Amount else -1 * TSPL_PR_DETAIL.Amount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Amount ,Tspl_PR_detail.Disc_Per ,case when Document_Type='C' then TSPL_PR_DETAIL.Disc_Amt else -1 * TSPL_PR_DETAIL.Disc_AMt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Disc_Amt, " & _
                        " case when Document_Type='C' then TSPL_PR_DETAIL.Amt_Less_Discount else -1 * TSPL_PR_DETAIL.Amt_Less_Discount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) Amt_Less_Discount , " & _
                        " case when Document_Type='C' then (Tspl_PR_detail.Total_Tax_Amt) else -1 * (Tspl_PR_detail.Total_Tax_Amt) end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Tax_Amt ,case when Document_Type='C' then Tspl_PR_detail.Item_Net_Amt else -1 * Tspl_PR_detail.Item_Net_Amt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Amt,Vehicle_No as vehicledesc,Vehicle_No as Vehicle_No,(case when Document_Type='C' then Tspl_PR_Detail.Total_ItemAdd_Charge else -1 * Tspl_PR_Detail.Total_ItemAdd_Charge end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Additional_Charge,-(case when Tax_Recoverable='N' then Tspl_PR_detail.TAX9_Amt else 0 end) as NonRecoverable_Tax, "

        strTaxColumns = " TSPL_PR_DETAIL.TAX9 ,(case when Document_Type='C' then TSPL_PR_DETAIL.TAX9_Amt else -1 * TSPL_PR_DETAIL.TAX9_Amt end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as TAX9_Amt,TSPL_PR_DETAIL.TAX9_Rate, TSPL_PR_DETAIL.TAX9+'%' as Tax9Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_Pr_Detail.ItemAdd_Charge_Code9 as Add_Charge_Code9 ,(case when Document_Type='C' then TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt9 else -1 * TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt9 end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Add_Charge_Amt9"
        ''richa add date filter
        strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PR_DETAIL.tax9 and ttr.tax_Rate=TSPL_PR_DETAIL.TAX9_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PR_DETAIL.tax9=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_Pr_Head .Add_Charge_Code9 =AdCh .Code where 2=2 and (TSPL_PR_DETAIL.tax9<>'' or TSPL_PR_HEAD.Add_Charge_Code9 <>'')  and convert(date,Tspl_PR_Head.PR_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,Tspl_PR_Head.PR_Date ,103) <= convert(date,('" + To_Date + "'),103) )s "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
            strMainQuery += " pivot (sum(tax9_amt) for tax9 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
            strMainQuery += " pivot (min(tax9_rate) for tax9rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
            strMainQuery += " pivot (sum(Add_Charge_Amt9) for Add_Charge_Code9 in (" + strPivotForAddChargeInnerQuery + "))t"
        End If


        strMainQuery += Environment.NewLine + " union all " + Environment.NewLine

        strSDCommonQuery = " select Distinct Tspl_PR_Head.Tax_Group as Head_Tax_Group,'P' as Head_Tax_Group_Type,'Purchase Return' as Trans_Type,Tspl_PR_Head.ConvRate ,TSPL_PR_DETAIL.Line_No,Tspl_PR_Head.Status ,Tspl_PR_Head.Bill_To_Location, " & _
                        " Tspl_PR_Head.Vendor_Code as Customer_Code,Tspl_STate_Master.state_Code AS [State Code],tspl_State_Master.state_name as [State Name],'Purchase Return' as Invoice_Type,Tspl_PR_Head.PR_NO ,'' as SRN_Id, " & _
                        " convert(varchar,Tspl_PR_Head.PR_Date,103 ) as PR_Date,'' as Way_BillNo,Tspl_PR_Head.GRNO,'' as LR_NO,Tspl_PR_Head.Vendor_Invoice_No ,convert(varchar,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) as Vendor_Invoice_Date,'' as [Transporter],'' as [Transporter Name], Tspl_PR_detail.Item_Code , " & _
                        " case when Document_Type='C' then  TSPL_PR_DETAIL.PR_Qty else -1* TSPL_PR_DETAIL.PR_Qty end as Qty ,Tspl_PR_detail.Unit_code ,Tspl_PR_detail.Item_Cost , " & _
                        " case when Document_Type='C' then TSPL_PR_DETAIL.Amount else -1 * TSPL_PR_DETAIL.Amount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Amount ,Tspl_PR_detail.Disc_Per ,case when Document_Type='C' then TSPL_PR_DETAIL.Disc_Amt else -1 * TSPL_PR_DETAIL.Disc_AMt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Disc_Amt, " & _
                        " case when Document_Type='C' then TSPL_PR_DETAIL.Amt_Less_Discount else -1 * TSPL_PR_DETAIL.Amt_Less_Discount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) Amt_Less_Discount , " & _
                        " case when Document_Type='C' then (Tspl_PR_detail.Total_Tax_Amt) else -1 * (Tspl_PR_detail.Total_Tax_Amt) end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Tax_Amt ,case when Document_Type='C' then Tspl_PR_detail.Item_Net_Amt else -1 * Tspl_PR_detail.Item_Net_Amt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Amt,Vehicle_No as vehicledesc,Vehicle_No as Vehicle_No,(case when Document_Type='C' then Tspl_PR_Detail.Total_ItemAdd_Charge else -1 * Tspl_PR_Detail.Total_ItemAdd_Charge end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Additional_Charge,-(case when Tax_Recoverable='N' then Tspl_PR_detail.TAX10_Amt else 0 end) as NonRecoverable_Tax, "

        strTaxColumns = " TSPL_PR_DETAIL.TAX10 ,(case when Document_Type='C' then TSPL_PR_DETAIL.TAX10_Amt else -1 * TSPL_PR_DETAIL.TAX10_Amt end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as TAX10_Amt,TSPL_PR_DETAIL.TAX10_Rate,TSPL_PR_DETAIL.TAX10+'%' as Tax10Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_Pr_Detail.ItemAdd_Charge_Code10 as Add_Charge_Code10 ,(case when Document_Type='C' then TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt10 else -1 * TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt10 end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Add_Charge_Amt10"
        ''richa add date filter
        strMainQuery += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & "  left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PR_DETAIL.tax10 and ttr.tax_Rate=TSPL_PR_DETAIL.TAX10_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PR_DETAIL.tax10=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_Pr_Head .Add_Charge_Code10 =AdCh .Code where 2=2 and (TSPL_PR_DETAIL.tax10<>'' or TSPL_PR_HEAD.Add_Charge_Code10<>'' )  and convert(date,Tspl_PR_Head.PR_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,Tspl_PR_Head.PR_Date ,103) <= convert(date,('" + To_Date + "'),103) )s "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
            strMainQuery += "pivot (sum(tax10_amt) for tax10 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
            strMainQuery += " pivot (min(tax10_rate) for tax10rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot values found,only then pivot query run(05/12/2016)
            strMainQuery += " pivot (sum(Add_Charge_Amt10) for Add_Charge_Code10 in (" + strPivotForAddChargeInnerQuery + "))t"
        End If


        strMainQuery += " )final1)final"
        strMainQuery += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =final.Bill_To_Location "
        strMainQuery += " left outer join tspl_item_master on tspl_item_master.Item_Code =final.Item_Code "
        strMainQuery += " left outer join TSPL_vendor_MASTER on TSPL_vendor_MASTER .Vendor_Code =final.Customer_Code "
        strMainQuery += " LEFT OUTER JOIN TSPL_vendor_MASTER as Parent_Master ON Parent_Master.Vendor_Code=TSPL_vendor_MASTER.Parent_Vendor_Code "
        strMainQuery += " left outer join " & "(" & qryQC & ") as QC" & " on QC.Item_Code =final.Item_Code "
        strMainQuery += " group by  final.Trans_Type,final .Status  ,final.PR_NO ,final.Item_Code ,final.Bill_To_Location ,final.Customer_Code ,final.Qty ,final.Total_Tax_Amt ,final.Invoice_Type ,final.PR_Date,final.Way_BillNo,Final.GRNO,final.LR_NO ,final.Unit_code ,final.Item_Cost ,final.Amount ,final.Disc_Per ,final.Disc_Amt ,final.Amt_Less_Discount ,final.Total_Amt,QC.FAT_Per,QC.SNF_Per,vehicledesc,Vehicle_No,final.Additional_Charge ,final.Against_BulkMillkPurchaseInvoice_No ,final.Against_MillkPurchaseInvoice_No ,final.PI_Id  " ', " + strPivotFoGrouprOuterQuery + "


        strMainQuery += Environment.NewLine + Environment.NewLine + "  union all" + Environment.NewLine + Environment.NewLine

        'strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX9 ,0 as TAX9_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX9_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX9+'%' as tax9rate  "
        strMainQuery += " select * from (Select '' as Head_Tax_Group,'' as Head_Tax_Group_Type,'Milk Receipt' as Trans_Type,0 as Line_No,0 as ConvRate ,TSPL_LOCATION_MASTER .location_Code  as  Bill_To_Location, " _
         & " (TSPL_LOCATION_MASTER.Add1) + ' ' + (TSPL_LOCATION_MASTER.Add2) + ' ' + (TSPL_LOCATION_MASTER.Add3) As [Location Address],TSPL_LOCATION_MASTER.State as [Location State],TSPL_MILK_PURCHASE_INVOICE_Head.Posted as Status, " _
            & " tspl_mcc_Master.mcc_name,'Milk Receipt' as Invoice_Type,TSPL_MILK_PURCHASE_INVOICE_Head.DOC_CODE as PI_NO,'' as SRN_Id ,  convert(varchar,TSPL_MILK_PURCHASE_INVOICE_Head.DOC_DATE,103 ) as PI_Date,'' as Way_BillNo,'' as [GRNO],'' as LR_NO,'' as Vendor_Invoice_No,'' as Vendor_Invoice_Date," _
            & " TSPL_Primary_Vehicle_Master.Vehicle_Code as Vehicle_No, TSPL_Primary_Vehicle_Master.Description as vehicledesc,0  as Additional_Charge ,  " _
            & " TSPL_MILK_SRN_HEAD.Vsp_CODE as Customer_Code,  tspl_vendor_Master.Vendor_Name as Customer_Name,(TSPL_VENDOR_MASTER.Add1) + ' ' + (TSPL_VENDOR_MASTER.Add2) + ' ' + (TSPL_VENDOR_MASTER.Add3) As [Vendor Address],tspl_state_Master.state_Code as [State Code],tspl_State_Master.state_Name as [State Name],tspl_vendor_Master.Tin_No as [TIN No],Parent_V.vendor_Code as [Parent Vendor No],Parent_V.vendor_Code as [Parent Vendor Code],Parent_V.vendor_Name as [Parent Vendor Name],pm.vendor_Code as [Transporter],pm.Vendor_Name as [Transporter Name]," _
            & " TSPL_MILK_SRN_detail.Item_Code, tspl_Item_Master.Item_Desc ,  MILK_WEIGHT  as Qty ,TSPL_MILK_SRN_detail.UOM_Code as  Unit_code ,TSPL_MILK_SRN_DETAIL.RATE as  Item_Cost " _
            & " ,  TSPL_MILK_SRN_DETAIL.FAT_Per as [FAT Per],  TSPL_MILK_SRN_DETAIL.SNF_PER as [SNF Per],TSPL_MILK_SRN_DETAIL.FAT_KG as [FAT KG],TSPL_MILK_SRN_DETAIL.SNF_KG " _
            & " as [SNF KG],TSPL_MILK_SRN_DETAIL.Amount ,0 as Disc_Per  ,0 as Disc_Amt ,  TSPL_MILK_PURCHASE_INVOICE_DETAIL.NET_AMOUNT as  Amt_Less_Discount ," _
            & " round(coalesce(TSPL_MILK_PURCHASE_INVOICE_DETAIL.AMOUNT*TSPL_MILK_PURCHASE_INVOICE_DETAIL.PAYMENT_COMMISSION/100,0),2) as EMP," _
            & " round(coalesce(TSPL_MILK_PURCHASE_INVOICE_DETAIL.Incentive,0),2) as Incentive_Head,round(coalesce(IncentiveEMP,0),2) as IncentiveEMP " _
            & " " & strPivotForTransfer_In & "" & strPivotFortRANSFER_INPercentQuery & " " & strPivotForAddChargeForZeroTransfer_In & ",'' as [Tax Type],  0 as Total_Tax_Amt ,TSPL_MILK_PURCHASE_INVOICE_DETAIL.NET_AMOUNT as   Total_Amt," _
            & " TSPL_vendor_Invoice_Head.Document_No as [AP Document No],TSPL_vendor_Invoice_Head.Document_Total [AP Document Amt],TSPL_vendor_Invoice_Head.Discount_Amount " _
            & " as [AP Document Discount Amt],TSPL_vendor_Invoice_Head.amount_less_Discount as [AP Amount Before Tax],stuff((select ',' + isnull(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE  ,'')  FROM TSPL_MILK_PURCHASE_INVOICE_DETAIL WHERE TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  =TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE  for xml path ('')),1,1,'' )as against_PoInvoice_No," _
            & " TSPL_vendor_Invoice_Head.Against_PurchaseREturn_No,TSPL_vendor_Invoice_Head.total_tax as [AP Total Tax],TSPL_vendor_Invoice_Head.total_Add_Charge as " _
            & " [AP Total Add Charge],TSPL_vendor_Invoice_Head.Total_landed_Amt as [AP Landed Amt],TSPL_vendor_Invoice_Head.Against_MillkpurchaseInvoice_No," _
            & " TSPL_vendor_Invoice_Head.Against_BulkMillkpurchaseInvoice_No,TSPL_vendor_Invoice_Head.Document_total as [AP Document Total],0 as MRP,0 as NonRecoverable_Tax, TSPL_MILK_PURCHASE_INVOICE_HEAD.Purchase_Tax_Invoice " _
            & " ,null as [Import Type],null  as [Port], null as [Import Bill of Entry No],null as [Import Bill of Entry Date] " _
            & " ,'' as [Original Invoice No],'' as [Original Invoice Date],'' as [Reason For Revision],'' as [ITC Eligible],'' as [ITC Status],'' as [ITC Details] " _
            & " from TSPL_MILK_SRN_detail Left Outer Join TSPL_MILK_SRN_HEAD        On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_detail.DOC_CODE  " _
            & " Left Outer Join      TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE        = " _
            & " TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE Left Outer Join      TSPL_MCC_MASTER        On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_SRN_HEAD.MCC_CODE   " _
            & " Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code =        TSPL_MILK_SRN_HEAD.VLC_CODE Left Outer Join TSPL_VENDOR_MASTER  " _
            & " On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_SRN_HEAD.VSP_CODE      Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code =  " _
            & " TSPL_MILK_SRN_HEAD.ROUTE_CODE Left Outer Join      TSPL_Primary_Vehicle_Master On TSPL_Primary_Vehicle_Master.Vehicle_Code =    " _
            & " TSPL_MCC_ROUTE_MASTER.Vehicle_Code left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_Code=TSPL_MILK_SRN_detail.Item_Code " _
            & " Left join tspl_Vendor_Master Parent_v on Parent_v.vendor_Code=tspl_Vendor_Master.Parent_Vendor_Code" _
            & " Left join tspl_Vendor_Master Pm on pm.vendor_Code=TSPL_Primary_Vehicle_Master.Vendor_Code" _
            & " left outer join TSPL_FAT_SNF_UPLOADER_MASTER on TSPL_FAT_SNF_UPLOADER_MASTER.Code=TSPL_MILK_SRN_DETAIL.Price_Code and TSPL_FAT_SNF_UPLOADER_MASTER.FAT=TSPL_MILK_SRN_DETAIL.FAT_PER and TSPL_FAT_SNF_UPLOADER_MASTER.SNF=TSPL_MILK_SRN_DETAIL.SNF_PER  " _
            & " left outer join TSPL_MILK_PRICE_MASTER on TSPL_MILK_PRICE_MASTER.Price_Code=TSPL_FAT_SNF_UPLOADER_MASTER.Price_Code " _
            & " left join tspl_Location_master on tspl_Location_master.location_code=TSPL_MILK_SRN_HEAD.mcc_Code left join TSPL_vendor_Invoice_Head on  TSPL_vendor_Invoice_Head.Against_MillkpurchaseInvoice_No=TSPL_MILK_PURCHASE_INVOICE_Head.DOC_CODE  left join TSPL_STATE_MASTER on tspl_State_Master.STATE_CODE=TSPL_Mcc_MASTER.State_Code where coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.doc_Code,'')<>''  AND TSPL_vendor_Invoice_Head.DOCUMENT_TYPE='I' and convert(date,TSPL_MILK_PURCHASE_INVOICE_Head.DOC_DATE ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_MILK_PURCHASE_INVOICE_Head.DOC_DATE ,103) <= convert(date,('" + To_Date + "'),103))t"

        strMainQuery += Environment.NewLine + Environment.NewLine + "  union all" + Environment.NewLine + Environment.NewLine


        strMainQuery += "  select * from (Select '' as Head_Tax_Group,'' as Head_Tax_Group_Type,'Bulk Purchase' as Trans_Type,0 as Line_No,0 as ConvRate ,TSPL_LOCATION_MASTER .location_Code  as  Bill_To_Location, (TSPL_LOCATION_MASTER.Add1) + ' ' + (TSPL_LOCATION_MASTER.Add2) + ' ' + (TSPL_LOCATION_MASTER.Add3) As [Location Address],TSPL_LOCATION_MASTER.State as [Location State],tspl_Bulk_milk_purchase_Invoice_head.isPosted " _
            & " as Status, TSPL_LOCATION_MASTER.Location_Desc as [Location Desc],'Bulk Purchase' as Invoice_Type,tspl_Bulk_milk_purchase_Invoice_head.DOC_NO as PI_NO,'' as SRN_Id , " _
            & " convert(varchar,tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE,103 ) as PI_Date,'' as Way_BillNo,'' as [GRNO],'' as LR_NO,tspl_Bulk_milk_purchase_Invoice_head.Vendor_Invoice_No as Vendor_Invoice_No,convert(varchar,tspl_Bulk_milk_purchase_Invoice_head.Doc_Date,103) as Vendor_Invoice_Date,Case when coalesce(TSPL_Bulk_MILK_SRN.Tanker_No,'')='' then TSPL_Dispatch_BulkSale_Trade.tanker_No else coalesce(TSPL_Bulk_MILK_SRN.Tanker_No,'') end as Vehicle_No, Case when coalesce(TSPL_Bulk_MILK_SRN.Tanker_No,'')='' then TSPL_Dispatch_BulkSale_Trade.tanker_No else coalesce(TSPL_Bulk_MILK_SRN.Tanker_No,'') end  " _
            & " as vehicledesc,Case when row_Number()over(partition by tspl_Bulk_milk_purchase_Invoice_head.DOC_NO Order by tspl_Bulk_milk_purchase_Invoice_Detail.Item_Code)=1 then tspl_Bulk_milk_purchase_Invoice_head.RoundoffAMount else 0 end  as Additional_Charge ,  tspl_Bulk_milk_purchase_Invoice_head.vendor_code as Customer_Code, .tspl_Vendor_Master.Vendor_Name as Customer_Name,(TSPL_VENDOR_MASTER.Add1) + ' ' + (TSPL_VENDOR_MASTER.Add2) + ' ' + (TSPL_VENDOR_MASTER.Add3) As [Vendor Address],tspl_State_Master.state_COde as [State Code],Tspl_State_Master.state_Name as [State Name], tspl_vendor_Master.Tin_No as [TIN No],Parent_v.vendor_Code as " _
            & " [Parent Vendor No],Parent_v.vendor_Code as [Parent Vendor Code],Parent_v.vendor_Name as [Parent Vendor Name],'' as Tanker_Transporter_Code,'' as [Transporter Name], tspl_Bulk_milk_purchase_Invoice_Detail.Item_Code, " _
            & " tspl_Bulk_milk_purchase_Invoice_Detail.Item_Desc ,  tspl_Bulk_milk_purchase_Invoice_Detail.Net_Weight  as Qty ,tspl_Bulk_milk_purchase_Invoice_Detail.UOM " _
            & " as  Unit_code ,Case when   TSPL_Bulk_MILK_SRN.Approved_Rate<=0 then   tspl_Bulk_milk_purchase_Invoice_Detail.NetRate else   TSPL_Bulk_MILK_SRN.Approved_Rate end as  Item_Cost ,  tspl_Bulk_milk_purchase_Invoice_Detail.FAT_Per as [FAT Per],  tspl_Bulk_milk_purchase_Invoice_Detail.SNF_PER as [SNF Per]," _
            & " tspl_Bulk_milk_purchase_Invoice_Detail.FAT_KG as [FAT KG],tspl_Bulk_milk_purchase_Invoice_Detail.SNF_KG as [SNF KG],tspl_Bulk_milk_purchase_Invoice_Detail.Amount ,0 as Disc_Per  ,0 as Disc_Amt , " _
            & " tspl_Bulk_milk_purchase_Invoice_Detail.Actual_Amount as  Amt_Less_Discount,0 as [EMP],0 as [Incentive],0 as [IncentiveEMP]   " & strPivotForTransfer_In & "" & strPivotFortRANSFER_INPercentQuery & " " & strPivotForAddChargeForZeroTransfer_In & ",'' as [Tax Type], " _
            & " 0 as Total_Tax_Amt ,tspl_Bulk_milk_purchase_Invoice_DETAIL.actual_amount as   Total_Amt,TSPL_vendor_Invoice_Head.Document_No as [AP Document No],TSPL_vendor_Invoice_Head.Document_Total [AP Document Amt],TSPL_vendor_Invoice_Head.Discount_Amount as [AP Document Discount Amt],TSPL_vendor_Invoice_Head.amount_less_Discount as [AP Amount Before Tax],stuff((select ',' + isnull(tspl_Bulk_milk_purchase_Invoice_DETAIL.SRN_NO ,'')  FROM tspl_Bulk_milk_purchase_Invoice_DETAIL WHERE tspl_Bulk_milk_purchase_Invoice_DETAIL.DOC_NO =tspl_Bulk_milk_purchase_Invoice_head.DOC_NO  for xml path ('')),1,1,'' )as against_PoInvoice_No,TSPL_vendor_Invoice_Head.Against_PurchaseREturn_No,TSPL_vendor_Invoice_Head.total_tax as [AP Total Tax],TSPL_vendor_Invoice_Head.total_Add_Charge as [AP Total Add Charge],TSPL_vendor_Invoice_Head.Total_landed_Amt as [AP Landed Amt],TSPL_vendor_Invoice_Head.Against_MillkpurchaseInvoice_No,TSPL_vendor_Invoice_Head.Against_BulkMillkpurchaseInvoice_No,TSPL_vendor_Invoice_Head.Document_total as [AP Document Total],0 as MRP,0 as NonRecoverable_Tax, tspl_Bulk_milk_purchase_Invoice_head.Purchase_Tax_Invoice " _
            & " ,null as [Import Type],null  as [Port], null as [Import Bill of Entry No],null as [Import Bill of Entry Date] " _
            & " ,'' as [Original Invoice No],'' as [Original Invoice Date],'' as [Reason For Revision],'' as [ITC Eligible],'' as [ITC Status],'' as [ITC Details] " _
            & " from tspl_Bulk_milk_purchase_Invoice_head inner join" _
            & " tspl_Bulk_milk_purchase_Invoice_Detail on tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO=tspl_Bulk_milk_purchase_Invoice_Head.DOC_NO left join " _
            & " TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=tspl_Bulk_milk_purchase_Invoice_head.Loc_Code left join TSPL_VENDOR_MASTER on " _
            & " TSPL_VENDOR_MASTER.Vendor_Code=tspl_Bulk_milk_purchase_Invoice_head.VENDOR_CODE left join TSPL_Bulk_MILK_SRN on TSPL_Bulk_MILK_SRN.SRN_NO=" _
            & " tspl_Bulk_milk_purchase_Invoice_DETAIL.SRN_NO   Left join tspl_Vendor_Master Parent_v on Parent_v.vendor_Code=tspl_Vendor_Master.Parent_Vendor_Code  left join TSPL_Dispatch_BulkSale_Trade on TSPL_Dispatch_BulkSale_Trade.Against_SRN_No=TSPL_Bulk_MILK_SRN.SRN_NO  left join TSPL_vendor_Invoice_Head on  TSPL_vendor_Invoice_Head.Against_bulkmillkPurchaseInvoice_No=tspl_Bulk_milk_purchase_Invoice_head.DOC_NO  left join TSPL_STATE_MASTER on tspl_State_Master.STATE_CODE=TSPL_VENDOR_MASTER.State_Code where convert(date,tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE,103) <= convert(date,('" + To_Date + "'),103))t "

        strMainQuery += Environment.NewLine + Environment.NewLine + "  union all" + Environment.NewLine + Environment.NewLine

        strMainQuery += " select * from (Select '' as Head_Tax_Group,'' as Head_Tax_Group_Type,'Bulk Purchase Return' as Trans_Type,0 as Line_No,0 as ConvRate,TSPL_LOCATION_MASTER .location_Code  as  Bill_To_Location,(TSPL_LOCATION_MASTER.Add1) + ' ' + (TSPL_LOCATION_MASTER.Add2) + ' ' + (TSPL_LOCATION_MASTER.Add3) As [Location Address], " & _
                            " TSPL_LOCATION_MASTER.State as [Location State],TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.isPosted  as Status,TSPL_LOCATION_MASTER.Location_Desc as [Location Desc],'Bulk Purchase Return' as Invoice_Type,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_No as PI_NO,'' as SRN_Id , " & _
                            " convert(varchar,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_Date,103 ) as PI_Date,'' as Way_BillNo,'' as [GRNO],'' as LR_NO,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Vendor_Invoice_No as Vendor_Invoice_No,convert(varchar,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_Date,103) as Vendor_Invoice_Date, " & _
                            " Case when coalesce(TSPL_Bulk_MILK_SRN.Tanker_No,'')='' then TSPL_Dispatch_BulkSale_Trade.tanker_No else coalesce(TSPL_Bulk_MILK_SRN.Tanker_No,'') end as Vehicle_No, Case when coalesce(TSPL_Bulk_MILK_SRN.Tanker_No,'')='' then TSPL_Dispatch_BulkSale_Trade.tanker_No else coalesce(TSPL_Bulk_MILK_SRN.Tanker_No,'') end   as vehicledesc," & _
                            " Case when row_Number()over(partition by TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_No Order by TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Item_Code)=1 then TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.RoundoffAMount else 0 end  as Additional_Charge ,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.vendor_code as Customer_Code " & _
                            " , .tspl_Vendor_Master.Vendor_Name as Customer_Name,(TSPL_VENDOR_MASTER.Add1) + ' ' + (TSPL_VENDOR_MASTER.Add2) + ' ' + (TSPL_VENDOR_MASTER.Add3) As [Vendor Address],tspl_State_Master.state_COde as [State Code],Tspl_State_Master.state_Name as [State Name], tspl_vendor_Master.Tin_No as [TIN No],Parent_v.vendor_Code as  [Parent Vendor No], " & _
                            " Parent_v.vendor_Code as [Parent Vendor Code],Parent_v.vendor_Name as [Parent Vendor Name],'' as Tanker_Transporter_Code,'' as [Transporter Name], TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Item_Code,  TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Item_Desc ,  -1*TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Net_Weight  as Qty ,TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.UOM  as  Unit_code " & _
                            " ,Case when   TSPL_Bulk_MILK_SRN.Approved_Rate<=0 then   TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.NetRate else   TSPL_Bulk_MILK_SRN.Approved_Rate end as  Item_Cost ,  TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.FAT_Per as [FAT Per],  TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.SNF_PER as [SNF Per], -1*TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.FAT_KG as [FAT KG],-1*TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.SNF_KG as [SNF KG],-1*TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Amount as Amount " & _
                            " ,0 as Disc_Per  ,0 as Disc_Amt ,  -1*TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Actual_Amount as  Amt_Less_Discount,0 as [EMP],0 as [Incentive],0 as [IncentiveEMP]  " & strPivotForTransfer_In & "" & strPivotFortRANSFER_INPercentQuery & " " & strPivotForAddChargeForZeroTransfer_In & " ,'' as [Tax Type],  0 as Total_Tax_Amt ,-1*TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.actual_amount as   Total_Amt,TSPL_vendor_Invoice_Head.Document_No as [AP Document No],-1*TSPL_vendor_Invoice_Head.Document_Total [AP Document Amt],-1*TSPL_vendor_Invoice_Head.Discount_Amount as [AP Document Discount Amt], " & _
                            " TSPL_vendor_Invoice_Head.amount_less_Discount as [AP Amount Before Tax],stuff((select ',' + isnull(TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.SRN_NO ,'')  FROM TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL WHERE TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Pur_Return_No =TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_No  for xml path ('')),1,1,'' )as against_PoInvoice_No, " & _
                            " TSPL_vendor_Invoice_Head.Against_PurchaseREturn_No,TSPL_vendor_Invoice_Head.total_tax as [AP Total Tax],TSPL_vendor_Invoice_Head.total_Add_Charge as [AP Total Add Charge],TSPL_vendor_Invoice_Head.Total_landed_Amt as [AP Landed Amt],TSPL_vendor_Invoice_Head.Against_MillkpurchaseInvoice_No,TSPL_vendor_Invoice_Head.Against_BulkMillkpurchaseInvoice_No, " & _
                            " TSPL_vendor_Invoice_Head.Document_total as [AP Document Total],0 as MRP,0 as NonRecoverable_Tax,'' as Purchase_Tax_Invoice " & _
                            " ,null as [Import Type],null  as [Port], null as [Import Bill of Entry No],null as [Import Bill of Entry Date] " & _
                            " ,'' as [Original Invoice No],'' as [Original Invoice Date],'' as [Reason For Revision],'' as [ITC Eligible],'' as [ITC Status],'' as [ITC Details]" & _
                            " from TSPL_BULK_MILK_PURCHASE_RETURN_HEAD inner join TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL " & _
                            " on TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Pur_Return_No=TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_No left join  TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Loc_Code left join TSPL_VENDOR_MASTER on  TSPL_VENDOR_MASTER.Vendor_Code=TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.VENDOR_CODE " & _
                            " left join TSPL_Bulk_MILK_SRN on TSPL_Bulk_MILK_SRN.SRN_NO= TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.SRN_NO   Left join tspl_Vendor_Master Parent_v on Parent_v.vendor_Code=tspl_Vendor_Master.Parent_Vendor_Code  left join TSPL_Dispatch_BulkSale_Trade on TSPL_Dispatch_BulkSale_Trade.Against_SRN_No=TSPL_Bulk_MILK_SRN.SRN_NO  left join TSPL_vendor_Invoice_Head on  TSPL_vendor_Invoice_Head.Against_bulkmillkPurchaseInvoice_No=TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_No  " & _
                            " left join TSPL_STATE_MASTER on tspl_State_Master.STATE_CODE=TSPL_VENDOR_MASTER.State_Code where convert(date,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_Date,103) <= convert(date,('" + To_Date + "'),103))t"

        strMainQuery += ") xx"
        strMainQuery += Environment.NewLine + Environment.NewLine
        strMainQuery += " left outer join tspl_item_master Item on Item.Item_Code =xx.[Item Code] " + Environment.NewLine
        strMainQuery += " left join (" & qryTransStock & ") as  TransStock on xx.[Item Code]=TransStock.Item_Code and TransStock.UOM_Code=" & IIf(clsCommon.myLen(Unit_Code) <= 0, "xx.[UOM]", "'" & Unit_Code & "'") & " " + Environment.NewLine
        strMainQuery += " left join (" & qryStock & ") as Stock_SU on xx.[Item Code]=Stock_SU.Item_Code and xx.[UOM]=Stock_SU.UOM_Code " + Environment.NewLine
        strMainQuery += " left join (" & qryKG & ") as StockKG on xx.[Item Code]=StockKG.Item_Code  " + Environment.NewLine
        strMainQuery += " left join (select Vendor_Code,Vendor_Group_Code,'' as Zone_Code,'' as Struct_Code,GST_Composition_scheme,GSTRegistered,GSTFinalNo,TSPL_VENDOR_MASTER.City_Code as VendorCityCode,TSPL_CITY_MASTER.City_Name as VendorCityName ,tspl_state_master.GST_STATE_Code as Vendor_GST_STATE_Code ,tspl_state_master.STATE_NAME as Veindor_STATE_Name from TSPL_vendor_MASTER left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_VENDOR_MASTER.City_Code left outer join tspl_state_master on tspl_state_master.state_code =TSPL_vendor_MASTER.state_code ) as Cust on xx.[vENDOR Code]=Cust.Vendor_Code " + Environment.NewLine
        strMainQuery += " left outer join (select TSPL_LOCATION_MASTER.Registered,TSPL_LOCATION_MASTER.Location_Code,TSPL_LOCATION_MASTER.GSTNO,TSPL_LOCATION_MASTER.City_Code,TSPL_LOCATION_MASTER.City_Code as City_Name,tspl_state_master.GST_STATE_Code,tspl_state_master.STATE_NAME from TSPL_LOCATION_MASTER  left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_LOCATION_MASTER.City_Code     left outer join tspl_state_master on tspl_state_master.state_code =TSPL_LOCATION_MASTER.State  ) as TSPL_LOCATION_MASTER_AS_Transfer on TSPL_LOCATION_MASTER_AS_Transfer.Location_Code = xx.[vENDOR Code] and xx.[Trans Type] in ('MCC Transfer','Transfer')" + Environment.NewLine
        strMainQuery += " left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_For_GSTIN on TSPL_LOCATION_MASTER_For_GSTIN.Location_Code = xx.[Location Code] " + Environment.NewLine + _
        " left outer join TSPL_STATE_MASTER on  TSPL_STATE_MASTER.STATE_CODE=TSPL_LOCATION_MASTER_For_GSTIN.State " + Environment.NewLine
        strMainQuery += " left join (select Ven_Group_Code,Group_Desc from TSPL_Vendor_GROUP) as Cust_Group on Cust.Vendor_Group_Code=Cust_Group.ven_Group_Code " + Environment.NewLine
        strMainQuery += " left join (select Zone_Code,Description from TSPL_ZONE_MASTER) as Zone on Cust.Zone_Code=Zone.Zone_Code " + Environment.NewLine
        strMainQuery += " left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=xx.Head_Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type=xx.Head_Tax_Group_Type" + Environment.NewLine
        If clsCommon.myLen(strCategoryTable) > 0 Then
            strMainQuery += " left outer join (" + strCategoryTable + ") as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=xx.[Item Code]" + Environment.NewLine
        End If
        strMainQuery += " left join tspl_item_master itmp on itmp.Item_Code=xx.[Item Code] " + Environment.NewLine + " left join TSPL_PURCHASE_ACCOUNTS tps on tps.Purchase_Class_Code=itmp.Purchase_Class_Code " + Environment.NewLine + " left join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code= " _
                   & " concat(SUBSTRING(tps.Inv_Control_Account,0,10), [Location Code]) " + Environment.NewLine + " where 2 = 2  and  convert(date,xx.[Document Date],103) >= convert(date,('" + From_Date + "'),103) and convert(date,xx.[Document Date],103) <= convert(date,('" + To_Date + "'),103) " ' + clsCommon.myCstr(IIf(clsCommon.myLen(txtUOM.Value) > 0, "and xx.[UOM]='" + txtUOM.Value + "' ", ""))
        QryLst.Add(strMainQuery)
        QryLst.Add(strPivotForFinalOuterQuery)
        QryLst.Add(strPivotForAddChargeFinalOuterSumQuery)
        Return QryLst
    End Function

    Function ReturnQuery() As String

        Dim qryList As ArrayList
        'qryList = ReturnQuery(clsCommon.GetDateWithStartTime(fromDate.Value), clsCommon.GetDateWithEndTime(ToDate.Value), Unit_Code, Stocking_Uom)
        qryList = clsPurchaseInvoiceHead.ReturnQuery(True, clsCommon.GetDateWithStartTime(fromDate.Value), clsCommon.GetDateWithEndTime(ToDate.Value), Unit_Code, Stocking_Uom)
        Dim strMCCMaterial As String = qryList(0)
        strPivotForFinalOuterQuery = qryList(1)
        strPivotForAddChargeFinalOutersumQuery = qryList(2)

        If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
            strMCCMaterial += " and xx.[Item Code] in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ") "
        End If
        If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
            strMCCMaterial += " and xx.[Trans Type] in (" + clsCommon.GetMulcallString(txtTransaction.arrValueMember) + ") "
        End If
        If txtState.arrValueMember IsNot Nothing AndAlso txtState.arrValueMember.Count > 0 Then
            strMCCMaterial += " and xx.[Location State] in (" + clsCommon.GetMulcallString(txtState.arrValueMember) + ") "
        End If

        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            strMCCMaterial += " and xx.[Location Code] in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
        Else
            If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                strMCCMaterial += " and xx.[Location Code] in (" + objCommonVar.strCurrUserLocations + ")"
            End If
        End If

        If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
            strMCCMaterial += " and xx.[Vendor Code] in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") "
        End If
        If arrCustGroup IsNot Nothing AndAlso arrCustGroup.Count > 0 Then
            strMCCMaterial += " and coalesce(Cust.Vendor_Group_Code,'') in (" + clsCommon.GetMulcallString(arrCustGroup) + ") "
        End If

        If IsNothing(arrCustGroup) And txtCustGroup.arrValueMember IsNot Nothing AndAlso txtCustGroup.arrValueMember.Count > 0 Then
            strMCCMaterial += "  and coalesce(Cust.Vendor_Group_Code,'') in (" + clsCommon.GetMulcallString(txtCustGroup.arrValueMember) + ") "
        End If
        If clsCommon.myLen(Document_No) > 0 Then
            strMCCMaterial += " and coalesce(xx.[Document No],'') ='" & clsCommon.myCstr(Document_No) & "' "
        End If
        Dim strWhrCatg As String = ""
        strWhrCatg = ""
        If rbtnCategorySelect.IsChecked Then
            Dim IsApplicable As Boolean = False
            For ii As Integer = 0 To gvCategory.RowCount - 1
                If clsCommon.myCBool(gvCategory.Rows(ii).Cells("SEL").Value) Then
                    If IsApplicable Then
                        strWhrCatg += " and "
                    End If
                    IsApplicable = True
                    strWhrCatg += "("
                    Dim arr As Dictionary(Of String, Object) = gvCategory.Rows(ii).Tag
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        strWhrCatg += " VirtualCategoryTabel.[" + clsCommon.myCstr(gvCategory.Rows(ii).Cells("CODE").Value) + "] in (" ''ERO/14/04/18-000083 added [VirtualCategoryTabel] by balwinder on 04/05/2018 
                        Dim isFirstTime As Boolean = True
                        For Each strInn As String In arr.Keys
                            If Not isFirstTime Then
                                strWhrCatg += ","
                            End If
                            strWhrCatg += "'" + strInn + "'"
                            isFirstTime = False
                        Next
                        strWhrCatg += ")"
                    Else
                        strWhrCatg += " 2=2  "
                    End If
                    strWhrCatg += ")"
                End If
            Next
            If Not IsApplicable Then
                Throw New Exception("Please select at least one category")
            End If
            strMCCMaterial += " and (" + strWhrCatg + ")"
        End If
        If btnPosted.IsChecked Then
            strMCCMaterial += " and xx.Status=1  "

        ElseIf btnUnposted.IsChecked Then
            strMCCMaterial += " and xx.Status=0  "

        End If
        Return strMCCMaterial
    End Function

    Function ReturnQueryFordocumentinfodetail() As String

        Dim qryList As ArrayList
        qryList = ReturnQueryForDocumentInfo(fromDate.Value, ToDate.Value, Unit_Code)
        Dim strMCCMaterial As String = qryList(0)
        strPivotForFinalOuterQuery = qryList(1)
        strPivotForAddChargeFinalOutersumQuery = qryList(2)

        If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
            strMCCMaterial += " and xx.[Item Code] in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ") "
        End If
        If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
            strMCCMaterial += " and xx.[Trans Type] in (" + clsCommon.GetMulcallString(txtTransaction.arrValueMember) + ") "
        End If
        If txtState.arrValueMember IsNot Nothing AndAlso txtState.arrValueMember.Count > 0 Then
            strMCCMaterial += " and xx.[Location State] in (" + clsCommon.GetMulcallString(txtState.arrValueMember) + ") "
        End If

        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            strMCCMaterial += " and xx.[Location Code] in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
        Else
            If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                strMCCMaterial += " and xx.[Location Code] in (" + objCommonVar.strCurrUserLocations + ")"
            End If
        End If

        If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
            strMCCMaterial += " and xx.[Vendor Code] in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") "
        End If
        'If arrCustGroup IsNot Nothing AndAlso arrCustGroup.Count > 0 Then
        '    strMCCMaterial += " and coalesce(Cust.ven_Group_Code,'') in (" + clsCommon.GetMulcallString(arrCustGroup) + ") "
        'End If
        ''shivani  against [BM00000008190]
        If arrCustGroup IsNot Nothing AndAlso arrCustGroup.Count > 0 Then
            strMCCMaterial += " and coalesce(Cust.Vendor_Group_Code,'') in (" + clsCommon.GetMulcallString(arrCustGroup) + ") "
        End If

        If IsNothing(arrCustGroup) And txtCustGroup.arrValueMember IsNot Nothing AndAlso txtCustGroup.arrValueMember.Count > 0 Then
            strMCCMaterial += "  and coalesce(Cust.Vendor_Group_Code,'') in (" + clsCommon.GetMulcallString(txtCustGroup.arrValueMember) + ") "
        End If


        If clsCommon.myLen(Document_No) > 0 Then
            strMCCMaterial += " and coalesce(xx.[Document No],'') ='" & clsCommon.myCstr(Document_No) & "' "
        End If
        Dim strWhrCatg As String = ""
        strWhrCatg = ""
        If rbtnCategorySelect.IsChecked Then
            Dim IsApplicable As Boolean = False
            For ii As Integer = 0 To gvCategory.RowCount - 1
                If clsCommon.myCBool(gvCategory.Rows(ii).Cells("SEL").Value) Then
                    If IsApplicable Then
                        strWhrCatg += " and "
                    End If
                    IsApplicable = True
                    strWhrCatg += "("
                    Dim arr As Dictionary(Of String, Object) = gvCategory.Rows(ii).Tag
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        strWhrCatg += " [" + clsCommon.myCstr(gvCategory.Rows(ii).Cells("CODE").Value) + "] in ("
                        Dim isFirstTime As Boolean = True
                        For Each strInn As String In arr.Keys
                            If Not isFirstTime Then
                                strWhrCatg += ","
                            End If
                            strWhrCatg += "'" + strInn + "'"
                            isFirstTime = False
                        Next
                        strWhrCatg += ")"
                    Else
                        strWhrCatg += " 2=2  "
                    End If
                    strWhrCatg += ")"
                End If
            Next
            If Not IsApplicable Then
                Throw New Exception("Please select at least one category")
            End If
            strMCCMaterial += " and (" + strWhrCatg + ")"
        End If
        If btnPosted.IsChecked Then
            strMCCMaterial += " and xx.Status=1  "

        ElseIf btnUnposted.IsChecked Then
            strMCCMaterial += " and xx.Status=0  "

        End If
        Return strMCCMaterial
    End Function

    Public Shared Function ReturnQueryForDocumentInfo(ByVal From_Date As Date, ByVal To_Date As Date, ByVal Unit_Code As String) As ArrayList
        Dim QryLst As New ArrayList
        Dim strCodeColumn As String = ""
        Dim strCodeColumnMax As String = ""
        Dim strCodeDescColumn As String = ""
        Dim strCodeDescColumnMax As String = ""
        Dim strPivotForFinalOuterQuery As String = ""
        Dim strPivotForAddChargeFinalOuterQuery As String = ""
        Dim strPivotForAddChargeFinalOuterSumQuery As String = ""
        Dim strCategoryTable As String = ""
        Dim dtCategory As DataTable
        dtCategory = clsDBFuncationality.GetDataTable("select ITEM_CATEGORY_CODE AS CodeColumn,ITEM_CATEGORY_CODE+' Description' as CodeDescColumn,DESCRIPTION as DescColumn  from TSPL_ITEM_CATEGORY_LEVEL order by CATEGORY_LEVEL")
        If dtCategory IsNot Nothing AndAlso dtCategory.Rows.Count > 0 Then
            For ii As Integer = 0 To dtCategory.Rows.Count - 1
                If ii <> 0 Then
                    strCodeColumn += ","
                    strCodeColumnMax += ","
                    strCodeDescColumn += ","
                    strCodeDescColumnMax += ","
                End If
                strCodeColumn += "[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                strCodeColumnMax += "max([" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]) as [" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                strCodeDescColumn += "[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")) + "]"
                strCodeDescColumnMax += "max([" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")).Trim() + "]) as [" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")).Trim() + "]"
            Next
            strCategoryTable = "select Item_Code," + strCodeColumnMax + "," + strCodeDescColumnMax + "  from (" + Environment.NewLine & _
            " select * from ( " + Environment.NewLine & _
            " select TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code " + Environment.NewLine & _
            " ,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code+' Description' as Item_Category_CodeDesc " + Environment.NewLine & _
            " ,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values " + Environment.NewLine & _
            " ,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as Category_Value_Desc " + Environment.NewLine & _
            " from  TSPL_ITEM_MASTER  " + Environment.NewLine & _
            " left outer join TSPL_ITEM_MASTER_CATEGORY on  TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code " + Environment.NewLine & _
            " left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values" + Environment.NewLine & _
            " where 2=2 " + Environment.NewLine & _
            " )xx" + Environment.NewLine

            If clsCommon.myLen(strCodeColumn) > 0 Then
                strCategoryTable += " Pivot " + Environment.NewLine & _
           " ( max(Item_Cagetory_Values) for Item_Category_Code   in ( " + strCodeColumn + ")" + Environment.NewLine & _
           " ) Pivt" + Environment.NewLine
            End If

            If clsCommon.myLen(strCodeDescColumn) > 0 Then
                strCategoryTable += " Pivot " + Environment.NewLine & _
            " (" + Environment.NewLine & _
            " max(Category_Value_Desc) for Item_Category_CodeDesc in (" + strCodeDescColumn + ")" + Environment.NewLine & _
            " ) Pivt1 " + Environment.NewLine
            End If

            strCategoryTable += " ) xxx group by Item_Code "
            ''End of Category Table start now.
        End If
        ''Virtual Category Table start now.


        Dim strMCCMaterial As String = ""
        Dim qryTaxQuery As String = ""
        Dim qryAddChargeQuery As String = ""
        Dim qryAddChargeForZeroQuery As String = ""

        Dim strPivotForOuter As String
        Dim strPivotForOuteronlyDocumentInfo As String
        Dim strPivotForAddChargeOuter As String
        Dim lstTables As New List(Of String)
        '========added By preeti gupta===========
        Dim lstTablesAddCharge As New List(Of String)
        '================ENd==========
        lstTables.Add("TSPL_PI_DETAIL")
        qryTaxQuery = GetTaxQuery(lstTables)
        '===============Added By preeti gupta============
        lstTablesAddCharge.Add("TSPL_PI_HEAD")
        qryAddChargeQuery = GetAddChargeQuery(lstTablesAddCharge)
        qryAddChargeForZeroQuery = GetAddChargeZeroQuery(lstTablesAddCharge)

        '==========================END=================

        'strPivotForOuter = " select distinct (select Distinct ',sum(isnull(final.'+tax1+',0)) as '+TAX1 from ( " & qryTaxQuery
        strPivotForOuter = "select distinct (select Distinct ',sum(isnull(final.['+tax1+'],0)) as ['+TAX1+']' from ( " & qryTaxQuery
        strPivotForOuter += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"

        Dim strPivotForOuterQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForOuter))
        '============================
        '===========================added by Preeti gupta 30/08/2016===========================
        strPivotForOuteronlyDocumentInfo = "select distinct (select Distinct ',max(isnull(final.['+tax1+'],0)) as ['+TAX1+']' from ( " & qryTaxQuery
        strPivotForOuteronlyDocumentInfo += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"

        Dim strPivotForOuterQueryonlyDocumentInfo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForOuteronlyDocumentInfo))
        '============================
        '============================================================================
        '==================Added By Preeti Gupta=============================
        strPivotForAddChargeOuter = " select REPLACE(xp,'&amp;','&') from (select distinct (select Distinct ',sum(isnull(final.['+Add_Charge_Code1+'],0)) as ['+Add_Charge_Code1+']' from ( " & qryAddChargeQuery
        strPivotForAddChargeOuter += " )aa where len(isnull(Add_Charge_Code1,''))>0 for xml path('') ) as xp)fin"

        Dim strPivotForAddChargeOuterQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForAddChargeOuter))
        '===============================END==================================
        Dim strPivotForFinalOuter As String
        strPivotForFinalOuter = ""
        strPivotForFinalOuter = " select distinct (select Distinct ',xx.['+tax1+']' from ( " & qryTaxQuery
        strPivotForFinalOuter += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"
        strPivotForFinalOuterQuery = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForFinalOuter))
        '=====================================Added By Preeti Gupta============================================
        Dim strPivotForAddChargeFinalOuter As String
        strPivotForAddChargeFinalOuter = ""
        strPivotForAddChargeFinalOuter = " select REPLACE(xp,'&amp;','&') from (select distinct (select Distinct ',xx.['+Add_Charge_Code1+']' from ( " & qryAddChargeQuery
        strPivotForAddChargeFinalOuter += " )aa where len(isnull(Add_Charge_Code1,''))>0 for xml path('') ) as xp)fin"
        strPivotForAddChargeFinalOuterQuery = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForAddChargeFinalOuter))

        Dim strPivotForAddChargeFinalSum As String
        strPivotForAddChargeFinalSum = ""
        strPivotForAddChargeFinalSum = " select REPLACE(xp,'&amp;','&') from ( select distinct (select Distinct ',xx.['+'AC_'+Add_Charge_Code1+']' from ( " & qryAddChargeQuery
        strPivotForAddChargeFinalSum += " )aa where len(isnull(Add_Charge_Code1,''))>0 for xml path('') )as xp)fin"
        strPivotForAddChargeFinalOuterSumQuery = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForAddChargeFinalSum))

        '=============================================END======================================================

        Dim strPivotForFinalOuterPercent As String
        strPivotForFinalOuterPercent = " select distinct (select  Distinct ',xx.['+tax1+'%'+']' from ( " & qryTaxQuery
        strPivotForFinalOuterPercent += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"
        Dim strPivotForFinalOuterPercentQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForFinalOuterPercent))

        Dim strPivotForTransfer_In As String
        strPivotForFinalOuter = ""
        strPivotForFinalOuter = " select distinct (select Distinct ',0 as ['+tax1+']' from ( " & qryTaxQuery
        strPivotForFinalOuter += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"
        strPivotForTransfer_In = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForFinalOuter))

        '=============================Added By Preeti Gupta=================================================

        Dim strPivotForAddChargeFinalOuterPercent As String
        strPivotForAddChargeFinalOuterPercent = " select REPLACE(xp,'&amp;','&') from (select distinct (select  Distinct ',xx.['+Add_Charge_Code1+']' from ( " & qryAddChargeQuery
        strPivotForAddChargeFinalOuterPercent += " )aa where len(isnull(Add_Charge_Code1,''))>0 for xml path('') )as xp)fin"
        Dim strPivotForFinalAddChargeOuterPercentQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForAddChargeFinalOuterPercent))

        Dim strPivotForAddChargeZeroFinalOuterPercent As String
        strPivotForAddChargeZeroFinalOuterPercent = " select REPLACE(xp,'&amp;','&') from ( select distinct (select  Distinct ',xx.['+Add_Charge_Code1+']' from ( " & qryAddChargeForZeroQuery
        strPivotForAddChargeZeroFinalOuterPercent += " )aa where len(isnull('AC_'+Add_Charge_Code1,''))>0 for xml path('') )as xp)fin"
        Dim strPivotForFinalAddChargeZeroOuterPercentQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForAddChargeZeroFinalOuterPercent))

        Dim strPivotForAddChargeTransfer_In As String
        strPivotForAddChargeFinalOuter = ""
        strPivotForAddChargeFinalOuter = " select REPLACE(xp,'&amp;','&') from (select distinct (select Distinct ',0 as ['+Add_Charge_Code1+']' from ( " & qryAddChargeQuery
        strPivotForAddChargeFinalOuter += " )aa where len(isnull(Add_Charge_Code1,''))>0 for xml path('') )as xp)fin"
        strPivotForAddChargeTransfer_In = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForAddChargeFinalOuter))

        Dim strPivotForAddChargeForZeroTransfer_In As String
        strPivotForAddChargeFinalOuter = ""
        strPivotForAddChargeFinalOuter = " select REPLACE(xp,'&amp;','&') from (select distinct (select Distinct ',0 as ['+Add_Charge_Code1+']' from ( " & qryAddChargeForZeroQuery
        strPivotForAddChargeFinalOuter += " )aa where len(isnull('AC_'+Add_Charge_Code1,''))>0 for xml path('') )as xp)fin"
        strPivotForAddChargeForZeroTransfer_In = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForAddChargeFinalOuter))

        '=========================================END=======================================================

        Dim strPivotFortRANSFER_INPercent As String
        strPivotFortRANSFER_INPercent = " select distinct (select  Distinct ',0 as ['+tax1+'%'+']' from ( " & qryTaxQuery
        strPivotFortRANSFER_INPercent += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"
        Dim strPivotFortRANSFER_INPercentQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotFortRANSFER_INPercent))

        Dim strTransferTaxColumns As String = ""
        Dim strTransferTaxPerColumns As String = ""
        Dim dtTempDT As DataTable = clsDBFuncationality.GetDataTable("select * from (" + qryTaxQuery + ")xx where len(isnull(TAX1,''))>0  order by  TAX1")
        If dtTempDT IsNot Nothing AndAlso dtTempDT.Rows.Count > 0 Then
            For Each dr As DataRow In dtTempDT.Rows
                Dim strTax As String = clsCommon.myCstr(dr(0))
                strTransferTaxColumns += ", (case when TSPL_TRANSFER_ORDER_DETAIL.TAX1='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX1_Amt else case when TSPL_TRANSFER_ORDER_DETAIL.TAX2='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX2_Amt else case when TSPL_TRANSFER_ORDER_DETAIL.TAX3='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX3_Amt else case when TSPL_TRANSFER_ORDER_DETAIL.TAX4='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX4_Amt else case when TSPL_TRANSFER_ORDER_DETAIL.TAX5='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX5_Amt else case when TSPL_TRANSFER_ORDER_DETAIL.TAX6='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX6_Amt else case when TSPL_TRANSFER_ORDER_DETAIL.TAX7='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX7_Amt else case when TSPL_TRANSFER_ORDER_DETAIL.TAX8='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX8_Amt else case when TSPL_TRANSFER_ORDER_DETAIL.TAX9='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX9_Amt else case when TSPL_TRANSFER_ORDER_DETAIL.TAX10='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX10_Amt else 0 end end end end end end end end end end ) as [" + strTax + "]"
                strTransferTaxPerColumns += ",(case when TSPL_TRANSFER_ORDER_DETAIL.TAX1='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX1_Rate else case when TSPL_TRANSFER_ORDER_DETAIL.TAX2='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX2_Rate else case when TSPL_TRANSFER_ORDER_DETAIL.TAX3='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX3_Rate else case when TSPL_TRANSFER_ORDER_DETAIL.TAX4='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX4_Rate else case when TSPL_TRANSFER_ORDER_DETAIL.TAX5='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX5_Rate else case when TSPL_TRANSFER_ORDER_DETAIL.TAX6='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX6_Rate else case when TSPL_TRANSFER_ORDER_DETAIL.TAX7='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX7_Rate else case when TSPL_TRANSFER_ORDER_DETAIL.TAX8='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX8_Rate else case when TSPL_TRANSFER_ORDER_DETAIL.TAX9='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX9_Rate else case when TSPL_TRANSFER_ORDER_DETAIL.TAX10='" + strTax + "' then TSPL_TRANSFER_ORDER_DETAIL.TAX10_Rate else 0 end end end end end end end end end end ) as [" + strTax + "%]"
            Next
        End If
        '===========

        Dim strPivotForGroupOuter As String
        strPivotForGroupOuter = " select REPLACE(abc,'&amp;','&') from ( select SUBSTRING(ax,2,len(Ax)) as abc from ("
        'strPivotForGroupOuter += " select distinct (select Distinct ',['+tax1+'%'+']' from ( " & qryTaxQuery
        strPivotForGroupOuter += " select distinct (select Distinct ',max(isnull(final.['+tax1+'%'+'],0)) as ['+TAX1+'%'+']' from ( " & qryTaxQuery

        strPivotForGroupOuter += " )a where len(isnull(TAX1,''))>0 for xml path('') )ax)Axx)XXX"
        Dim strPivotFoGrouprOuterQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForGroupOuter))

        ''done below code so that when variable placed in query,no need for comma(,),in case of blank variable its gives error.(06/12/2016)
        If clsCommon.myLen(strPivotFoGrouprOuterQuery) > 0 Then
            strPivotFoGrouprOuterQuery = "," + strPivotFoGrouprOuterQuery
        End If


        '======================================Added by Preeti Gupta


        '====================================================================

        Dim strPivotForADDChargeGroupOuter As String
        strPivotForADDChargeGroupOuter = " select REPLACE(abc,'&amp;','&') from (select SUBSTRING(ax,2,len(Ax)) as abc from ("
        'strPivotForGroupOuter += " select distinct (select Distinct ',['+tax1+'%'+']' from ( " & qryTaxQuery
        strPivotForADDChargeGroupOuter += " select distinct (select Distinct ',sum(isnull(final.['+Add_Charge_Code1+'],0)) as ['+Add_Charge_Code1+']' from ( " & qryAddChargeQuery

        strPivotForADDChargeGroupOuter += " )a where len(isnull(Add_Charge_Code1,''))>0 for xml path('') )ax)Axx)XXX"
        Dim strPivotFoAddChargeGrouprOuterQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForADDChargeGroupOuter))

        Dim strPivotForADDChargeZeroGroupOuter As String
        strPivotForADDChargeZeroGroupOuter = "select REPLACE(abc,'&amp;','&') from (select SUBSTRING(ax,2,len(Ax)) as abc from ("
        'strPivotForGroupOuter += " select distinct (select Distinct ',['+tax1+'%'+']' from ( " & qryTaxQuery
        strPivotForADDChargeZeroGroupOuter += " select distinct (select Distinct ',sum(isnull(final.['+Add_Charge_Code1+'],0)) as ['+Add_Charge_Code1+']' from ( " & qryAddChargeForZeroQuery

        strPivotForADDChargeZeroGroupOuter += " )a where len(isnull('AC_'+Add_Charge_Code1,''))>0 for xml path('') )ax)Axx)XXX"
        Dim strPivotFoAddChargeZeroGrouprOuterQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForADDChargeZeroGroupOuter))

        If clsCommon.myLen(strPivotFoAddChargeZeroGrouprOuterQuery) > 0 Then
            strPivotFoAddChargeZeroGrouprOuterQuery = "," + strPivotFoAddChargeZeroGrouprOuterQuery
        End If
        Dim strPivotForOuterForBulk As String
        strPivotForOuterForBulk = " select distinct (select Distinct ',0 as ['+TAX1+']' from ( " & qryTaxQuery

        strPivotForOuterForBulk += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"

        Dim strPivotForOuterQueryforBulk As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForOuterForBulk))

        Dim strDoublePivotForOuterForBulk As String

        strDoublePivotForOuterForBulk = " select distinct (select Distinct ',0 as ['+tax1+'%'+']' from ( " & qryTaxQuery


        strDoublePivotForOuterForBulk += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"

        Dim strDoublePivotForOuterQueryforBulk As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strDoublePivotForOuterForBulk))

        '=====================================Added by Preeti Gupta===================================================================
        Dim strPivotForAddChargeOuterForBulk As String
        strPivotForAddChargeOuterForBulk = " select REPLACE(xp,'&amp;','&') from (select distinct (select Distinct ',0 as ['+Add_Charge_Code1+']' from ( " & qryAddChargeQuery

        strPivotForAddChargeOuterForBulk += " )aa where len(isnull(Add_Charge_Code1,''))>0 for xml path('') )as xp)fin"

        Dim strPivotForAddChargeOuterQueryforBulk As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForAddChargeOuterForBulk))
        Dim strPivotForInner As String
        strPivotForInner = "select SUBSTRING(ax,2,len(Ax)) from ("
        strPivotForInner += " select distinct (select Distinct ',['+tax1+']' from ( " & qryTaxQuery

        strPivotForInner += " )a where len(isnull(TAX1,''))>0 for xml path('') )ax)Axx"

        Dim strPivotForInnerQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForInner))

        '============================================Added By Preeti Gupta ticket no [BM00000009024]===========================================
        Dim strPivotForAddChargeInner As String
        strPivotForAddChargeInner = "select REPLACE(abc,'&amp;','&') from (select SUBSTRING(ax,2,len(Ax)) as abc from ("
        strPivotForAddChargeInner += " select distinct (select Distinct ',['+Add_Charge_Code1+']' from ( " & qryAddChargeQuery

        strPivotForAddChargeInner += " )a where len(isnull(Add_Charge_Code1,''))>0 for xml path('') ) as ax)Axx)XXX"

        Dim strPivotForAddChargeInnerQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForAddChargeInner))

        Dim strPivotForAddChargeInnerOuter As String
        strPivotForAddChargeInnerOuter = " select REPLACE(abc,'&amp;','&') from (select ','+SUBSTRING(ax,2,len(Ax)) as abc from ("
        strPivotForAddChargeInnerOuter += " select distinct (select Distinct ',['+Add_Charge_Code1+'] as ['+'AC_'+Add_Charge_Code1+']' from ( " & qryAddChargeQuery

        strPivotForAddChargeInnerOuter += " )a where len(isnull(Add_Charge_Code1,''))>0 for xml path('') ) as ax)Axx)XXX"
        Dim strPivotForAddChargeInnerQueryOuter As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForAddChargeInnerOuter))

        '=======================================================END==================================================
        Dim strDoublePivotForInner As String
        strDoublePivotForInner = "select SUBSTRING(ax,2,len(Ax)) from ("
        strDoublePivotForInner += " select distinct (select Distinct ',['+tax1+'%'+']' from ( " & qryTaxQuery

        strDoublePivotForInner += " )a where len(isnull(TAX1,''))>0 for xml path('') )ax)Axx"

        Dim strDoublePivotForInnerQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strDoublePivotForInner))
        Dim qryQC As String = ""
        qryQC = " select Item_Code,MAX(Fat_Per) as Fat_Per,MAX(SNF_Per) as SNF_Per from (" & _
                " select Item_QCP.Item_Code,Item_QCP.Code as Parameter_Code,(case when QCP.Type='FAT' then Item_QCP.Actual_Range end) as Fat_Per," & _
                " (case when QCP.Type='SNF' then Item_QCP.Actual_Range  end) as SNF_Per from TSPL_ITEM_QC_PARAMETER_MASTER Item_QCP " & _
                " left join TSPL_PARAMETER_MASTER QCP  on Item_QCP.Code=QCP.Code) as QC group by Item_Code"

        Dim qryKG As String = ""
        qryKG = "select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='KG'"
        Dim qryStock As String = ""
        qryStock = "select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL "

        '' query for transaction  UOM conversion
        Dim qryTransStock As String = ""
        If clsCommon.myLen(Unit_Code) <= 0 Then
            qryTransStock = "select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL "
        Else
            qryTransStock = "select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='" & Unit_Code & "'"
        End If

        '' end query for transaction  UOM conversion

        '' query for structure and item group custom field
        Dim strSDCommonQuery As String = ""
        Dim strTaxColumns As String = ""
        Dim strAddChargeColumns As String = ""
        Dim strTaxNonRecoverableAmt As String = ""
        Dim strSDEndQry As String = ",TSPL_PI_DETAIL.TAX1+'%' as Tax1_Rate"
        strSDCommonQuery = " select Distinct 'PI' as Trans_Type,TSPL_PI_DETAIL.Line_No,TSPL_PI_HEAD .ConvRate,TSPL_PI_DETAIL.srn_Id,TSPL_PI_DETAIL.MRN_Id,TSPL_PI_DETAIL.GRN_ID,TSPL_PI_HEAD.Status ,TSPL_PI_HEAD.Bill_To_Location, " & _
                           " TSPL_PI_HEAD.Vendor_Code as Customer_Code,TSPL_STATE_MASTER.STATE_CODE as [State Code],TSPL_STATE_MASTER.STATE_NAME as [State Name],'PI'  as Invoice_Type,TSPL_PI_HEAD.PI_NO , " & _
                           " convert(varchar,TSPL_PI_HEAD.PI_Date,103 ) as PI_Date,'' as Way_BillNo,TSPL_PI_HEAD.GRNO,TSPL_PI_HEAD.LR_NO,TSPL_PI_HEAD.Vendor_Invoice_No ,convert(varchar,TSPL_PI_Head.InvoiceDate,103) as Vendor_Invoice_Date,TSPL_PI_HEAD.Transporter as [Transporter],TSPL_PI_HEAD.TransporterDesc as [Transporter Name], TSPL_PI_DETAIL.Item_Code , " & _
                           " TSPL_PI_DETAIL.PI_Qty as Qty ,TSPL_PI_DETAIL.Unit_code ,TSPL_PI_DETAIL.Item_Cost*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Item_Cost , " & _
                           " TSPL_PI_DETAIL.Amount*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amount ,TSPL_PI_DETAIL.Disc_Per ,TSPL_PI_DETAIL.Disc_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Disc_Amt , " & _
                           " TSPL_PI_DETAIL.Amt_Less_Discount *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amt_Less_Discount, " & _
                           " (TSPL_PI_DETAIL.Total_Tax_Amt) *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Tax_Amt,TSPL_PI_DETAIL.Item_Net_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Amt,TSPL_PI_HEAD.vehicledesc,TSPL_VEHICLE_MASTER.Number as Vehicle_No,(TSPL_PI_Detail.Total_ItemAdd_Charge*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end)) as Additional_Charge,0 as NonRecoverable_Tax, "
        strSDEndQry = ",TSPL_vendor_Invoice_Head.Document_No as [AP Document No],TSPL_vendor_Invoice_Head.Document_Total*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as [AP Document Amt],TSPL_vendor_Invoice_Head.Discount_Amount as [AP Document Discount Amt],TSPL_vendor_Invoice_Head.amount_less_Discount*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as [AP Amount Before Tax],TSPL_PI_DETAIL.SRN_Id as against_PoInvoice_No,TSPL_vendor_Invoice_Head.Against_PurchaseREturn_No,TSPL_vendor_Invoice_Head.total_tax*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as [AP Total Tax],TSPL_vendor_Invoice_Head.total_Add_Charge*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as [AP Total Add Charge],TSPL_vendor_Invoice_Head.Total_landed_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as [AP Landed Amt],TSPL_vendor_Invoice_Head.Against_MillkpurchaseInvoice_No,TSPL_vendor_Invoice_Head.Against_BulkMillkpurchaseInvoice_No,TSPL_vendor_Invoice_Head.Document_total*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as [AP Document Total],TSPL_PI_DETAIL.Po_Id,TSPL_PI_DETAIL.MRP,TSPL_PI_HEAD.Purchase_Tax_Invoice  ,TSPL_PURCHASE_ORDER_HEAD.Created_By,TSPL_PURCHASE_ORDER_HEAD.Modify_By,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No as Doc_No,TSPL_PURCHASE_ORDER_head.PurchaseOrder_Date as Doc_Purchase_Date from TSPL_PI_DETAIL " & _
                           " left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_NO =TSPL_PI_DETAIL.PI_NO " & _
                           " left join TSPL_VEHICLE_MASTER on TSPL_PI_HEAD.vehicledesc=TSPL_VEHICLE_MASTER.Vehicle_Id left join TSPL_vendor_Invoice_Head on TSPL_vendor_Invoice_Head.against_PoInvoice_No=TSPL_PI_HEAD.PI_NO  left join TSPL_vendor_master on   TSPL_vendor_master.Vendor_Code=tspl_Pi_Head.Vendor_Code left join TSPL_STATE_MASTER on tspl_State_Master.STATE_CODE=TSPL_VENDOR_MASTER.State_Code " & _
                           "    "
        strMCCMaterial = " select [Trans Type],[Location Code],[Location Name],[Location Address],[Location State], [Invoice Type],[Document No],[Document Date],[Way Bill No],[GR No],[LR No],Vendor_Invoice_no as [Vendor Invoice No],case when Coalesce(Vendor_Invoice_no,'')<>'' then convert(varchar,Vendor_Invoice_Date,103) else null end as [Vendor Invoice Date],vehicledesc as [Vehicle Code],Vehicle_No as [Vehicle No],cast(Additional_Charge as numeric(18,3)) as [Additional Amount],[Vendor Code],[Vendor Name],[Vendor Address],[State Code] as [Vendor State Code],[State Name] as [Vendor State Desc.],[Vendor TIN No],xx.[Transporter],[Transporter Name],Cust.Vendor_Group_Code as [Vendor Group Code],Cust_Group.Group_Desc as [Vendor Group Description], [Parent Vendor No],[Parent Vendor Code], [Parent Vendor Name] "
        If clsCommon.myLen(strCategoryTable) > 0 Then
            strMCCMaterial += "," + strCodeColumn + "," + strCodeDescColumn
        End If
        strMCCMaterial += " , [Item Code],[Item Name],cast(([Quantity]*Stock_SU.Conversion_Factor)/(coalesce(TransStock.Conversion_Factor,1)) as Numeric(18,3)) as [Quantity]," & IIf(clsCommon.myLen(Unit_Code) <= 0, "xx.[UOM]", "'" & Unit_Code & "'") & " as [UOM],[Item Cost] as [Item Rate],[Fat Per] as [FAT %],[SNF Per] as [SNF %],cast(([Quantity]*[Fat Per]*Stock_SU.Conversion_Factor)/(100*coalesce(StockKG.Conversion_Factor,1)) as numeric(18,3)) as [FAT KG],cast(([Quantity]*[SNF Per]*Stock_SU.Conversion_Factor)/(100*coalesce(StockKG.Conversion_Factor,1)) as Numeric(18,3)) as [SNF KG],Amount,[Discount Per] as [Discount %], [Discount Amount],cast(Round(xx.[EMP],2) as decimal(18,2)) as [EMP],Round(xx.[Incentive],2) as [Incentive],Round(xx.[IncentiveEMP],0) as [Incentive EMP] ,Round([Amount Less Discount],2) as [Amount Less Discount] " + strPivotForFinalOuterQuery + " " + strPivotForFinalOuterPercentQuery + "" + strPivotForFinalAddChargeZeroOuterPercentQuery + ",[Tax Type] as [Form Type],round(([Total Amount]+cast(Additional_Charge as numeric(18,3))-[Total Tax Amount]),2) as [Purchase Amount],Round([Total Tax Amount],2) as [Total Tax Amount], Round((cast(Additional_Charge as numeric(18,3))+[Total Amount]),2) as [Total Amount],(SUBSTRING(tps.Inv_Control_Account,0,10) + [Location Code]) as [Inventory Account Code],TSPL_GL_ACCOUNTS.Description as [Inventory Account Name],[AP Document No] ,coalesce(against_PoInvoice_No, coalesce(Against_PurchaseREturn_No,coalesce(Against_MillkpurchaseInvoice_No,Against_BulkMillkpurchaseInvoice_No))) as [Against Invoice No],[AP Total Tax],[AP Total Add Charge],[AP Landed Amt],[AP Document Total],MRP,Ref_Doc_Type,Doc_No as [Ref Doc No],Doc_Purchase_Date as [Ref Doc Date],Doc_Created_By as [Ref Created By],Doc_Modify_By as [Ref Posted By], Item.HSN_Code,case when cust.GST_Composition_scheme=1 then 'Yes' else 'No' end as GST_Composition_scheme,case when cust.GSTRegistered=1 then 'Yes' else 'No' end as GSTRegistered ,cust.GSTFinalNo,TSPL_LOCATION_MASTER_For_GSTIN.GSTNO as Location_GSTIN,cust.VendorCityCode,cust.VendorCityName,cust.Vendor_GST_STATE_Code,Veindor_STATE_Name,Purchase_Tax_invoice "
        strMCCMaterial += " from (select case when Trans_Type ='PI' then 'Purchase Invoice' when Trans_Type ='Transfer' then 'Transfer' when Trans_Type='MCC' then 'Milk Receipt' when Trans_Type='Bulk' then 'Bulk Purchase' when Trans_Type='MT' then 'Merchant Trade' end  as [Trans Type],max(final.Line_No) as Line_No,max(final.ConvRate) as ConvRate,max(TSPL_LOCATION_MASTER .location_Code) as [Location Code],max(TSPL_LOCATION_MASTER.Add1) + ' ' + max(TSPL_LOCATION_MASTER.Add2) + ' ' + max(TSPL_LOCATION_MASTER.Add3) As [Location Address],max(TSPL_LOCATION_MASTER.State) as [Location State],final.Status  ,max(TSPL_LOCATION_MASTER .Location_Desc) as [Location Name] ,(final.Invoice_Type) as [Invoice Type],final.PI_NO as [Document No],final.SRN_Id as [SRN No],final.MRN_ID ,final.GRN_ID ,final.PI_Date as [Document Date],final.Way_BillNo as [Way Bill No],Final.GRNO as [GR No],final.LR_NO as [LR No] ,max(VENDOR_INVOICE_no)as VENDOR_INVOICE_no,max(VENDOR_INVOICE_Date)as VENDOR_INVOICE_Date,vehicledesc,Vehicle_No,final.Additional_Charge+Case when coalesce(final.Additional_Charge,0)>0 then coalesce(max(PACKING),0) else 0 end as Additional_Charge,final.Customer_Code as [Vendor Code] ,max(TSPL_vendor_MASTER .vendor_Name) as [Vendor Name],max(TSPL_VENDOR_MASTER.Add1) + ' ' + max(TSPL_VENDOR_MASTER.Add2) + ' ' + max(TSPL_VENDOR_MASTER.Add3) As [Vendor Address],Max([State Code]) as [State Code],Max([State Name]) as [State Name],max(TSPL_vendor_MASTER .Tin_No) as [Vendor TIN No] ,max(TSPL_vendor_MASTER .Parent_vendor_Code) as [Parent Vendor No] ,max(Parent_Master.Vendor_Code) as [Parent Vendor Code],max(Parent_Master.Vendor_Name) as [Parent Vendor Name],Max(final.[Transporter]) as [Transporter],Max([Transporter Name]) as [Transporter Name], final.Item_Code as [Item Code],max(tspl_item_master.Item_Desc) as [Item Name],final.Qty as [Quantity],final.Unit_code as [UOM],final.Item_Cost as [Item Cost], QC.FAT_Per as [Fat Per],QC.SNF_Per as [SNF Per],0 as [Fat Kg],0 as [SNF KG],final.Amount,final.Disc_Per as [Discount Per],final.Disc_Amt as [Discount Amount],final.Amt_Less_Discount  as [Amount Less Discount],0 as [EMP],0 as [Incentive],0 as [IncentiveEMP]  " + strPivotForOuterQueryonlyDocumentInfo + " " + strPivotFoGrouprOuterQuery + " " + strPivotFoAddChargeZeroGrouprOuterQuery + ",max(_Type) as [Tax Type] ,(final.Total_Tax_Amt-coalesce(sum(final.NonRecoverable_Tax),0)) as [Total Tax Amount],final.Total_Amt as [Total Amount],Max([AP Document No]) as [AP Document No],Max(coalesce([AP Document Amt],0)) as [AP Document Amt],Max(coalesce([AP Document Discount Amt],0)) as [AP Document Discount Amt],Max(coalesce([AP Amount Before Tax],0)) as [AP Amount Before Tax],Max(against_PoInvoice_No) as against_PoInvoice_No,Max(Against_PurchaseREturn_No) as Against_PurchaseREturn_No,Max(coalesce([AP Total Tax],0)) as [AP Total Tax],max(coalesce([AP Total Add Charge],0)) as [AP Total Add Charge],(Max(coalesce([AP Landed Amt],0))-coalesce(sum(final.NonRecoverable_Tax),0)) as [AP Landed Amt],max(Against_MillkpurchaseInvoice_No) as Against_MillkpurchaseInvoice_No,Max(Against_BulkMillkpurchaseInvoice_No) as Against_BulkMillkpurchaseInvoice_No,Max(coalesce([AP Document Total],0)) as [AP Document Total],max(final.MRP) as MRP,coalesce(sum(final.NonRecoverable_Tax),0) as NonRecoverable_Tax,max(Purchase_Tax_invoice) as Purchase_Tax_invoice,'PO' as Ref_Doc_Type ,max(final.Doc_No) as Doc_No,convert(varchar,max(final.Doc_Purchase_Date),103) as Doc_Purchase_Date ,max(final.Doc_Created_By) as Doc_Created_By,max(final.Doc_Modify_By) as Doc_Modify_By  "
        strMCCMaterial += " from ("
        strTaxColumns = " TSPL_PI_DETAIL.TAX1 ,TSPL_PI_DETAIL.TAX1_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as TAX1_Amt, TSPL_PI_DETAIL.TAX1_Rate,TSPL_PI_DETAIL.TAX1+'%' as Tax1Rate,'' as _Type,'N' as Tax_Recoverable "
        strAddChargeColumns = " ,TSPL_PI_Detail.ItemAdd_Charge_Code1 as Add_Charge_Code1 ,TSPL_PI_Detail.ItemAdd_Calc_Charge_Amt1*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Add_Charge_Amt1 "
        'strTaxNonRecoverableAmt = " tm.Tax_Recoverable "
        '' query for no tax applied
        strMCCMaterial += " select Trans_Type,Line_No,ConvRate ,SRN_Id ,MRN_Id,GRN_ID,Status ,Bill_To_Location ,Customer_Code ,[State Code] ,[State Name] ,Invoice_Type ,PI_No ,PI_Date ,Way_BillNo ,GRNo ,LR_No ,Vendor_Invoice_No ,Vendor_Invoice_Date ,Transporter ,[Transporter Name] ,Item_Code ,Qty ,Unit_code ,Item_Cost ,Amount ,Disc_Per,Disc_Amt ,Amt_Less_Discount ,Total_Tax_Amt ,Total_Amt ,Vehicle_No ,VehicleDesc ,Additional_Charge ,NonRecoverable_Tax ,_Type ,Tax_Recoverable ,[AP Document No] ,[AP Document Amt],[AP Document Discount Amt] ,[AP Amount Before Tax],Against_POInvoice_No,Against_PurchaseReturn_No,[AP Total Tax],[AP Total Add Charge],[AP Landed Amt],Against_MillkPurchaseInvoice_No,Against_BulkMillkPurchaseInvoice_No,[AP Document Total],PO_ID,MRP,Purchase_Tax_invoice,Doc_No,Created_By as Doc_Created_By,Modify_By  as Doc_Modify_By,Doc_Purchase_Date as Doc_Purchase_Date " & _
            " " + IIf(clsCommon.myLen(strPivotForInnerQuery) > 0, "," + strPivotForInnerQuery, "") + " " + IIf(clsCommon.myLen(strDoublePivotForInnerQuery) > 0, "," + strDoublePivotForInnerQuery, "") + " " + strPivotForAddChargeInnerQueryOuter + " " & _
            " from (select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PI_DETAIL.PO_ID where 2=2 and (coalesce(TSPL_PI_DETAIL.tax1,'')='' and coalesce(TSPL_PI_DETAIL.tax2,'')='' " & _
                          " and coalesce(TSPL_PI_DETAIL.tax3,'')='' and coalesce(TSPL_PI_DETAIL.tax4,'')='' and " & _
                          " coalesce(TSPL_PI_DETAIL.tax5,'')='' and coalesce(TSPL_PI_DETAIL.tax6,'')='' and " & _
                          " coalesce(TSPL_PI_DETAIL.tax7,'')='' and coalesce(TSPL_PI_DETAIL.tax8,'')='' and " & _
                          " coalesce(TSPL_PI_DETAIL.tax9,'')='' and coalesce(TSPL_PI_DETAIL.tax10,'')='') and  convert(date,TSPL_PI_HEAD.PI_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_PI_HEAD.PI_Date ,103) <= convert(date,('" + To_Date + "'),103) )s  "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot columns exist only then query run(06/12/2016)
            strMCCMaterial += " pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot columns exist only then query run(06/12/2016)
            strMCCMaterial += " pivot (min(tax1_rate) for Tax1Rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot columns exist only then query run(06/12/2016)
            strMCCMaterial += " pivot (sum(Add_Charge_Amt1) for Add_Charge_Code1 in (" + strPivotForAddChargeInnerQuery + "))t"
        End If


        strSDCommonQuery = " select Distinct case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Trans_Type,TSPL_PI_DETAIL.Line_No,TSPL_PI_HEAD .ConvRate,TSPL_PI_DETAIL.srn_Id,TSPL_PI_DETAIL.MRN_Id,TSPL_PI_DETAIL.GRN_ID,TSPL_PI_HEAD.Status ,TSPL_PI_HEAD.Bill_To_Location, " & _
                          " TSPL_PI_HEAD.Vendor_Code as Customer_Code,TSPL_STATE_MASTER.STATE_CODE as [State Code],TSPL_STATE_MASTER.STATE_NAME as [State Name],case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Invoice_Type,TSPL_PI_HEAD.PI_NO , " & _
                          " convert(varchar,TSPL_PI_HEAD.PI_Date,103 ) as PI_Date,'' as Way_BillNo,TSPL_PI_HEAD.GRNO,TSPL_PI_HEAD.LR_NO,TSPL_PI_HEAD.Vendor_Invoice_No ,convert(varchar,TSPL_PI_Head.InvoiceDate,103) as Vendor_Invoice_Date,TSPL_PI_HEAD.Transporter as [Transporter],TSPL_PI_HEAD.TransporterDesc as [Transporter Name], TSPL_PI_DETAIL.Item_Code , " & _
                          " TSPL_PI_DETAIL.PI_Qty as Qty ,TSPL_PI_DETAIL.Unit_code ,TSPL_PI_DETAIL.Item_Cost*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Item_Cost , " & _
                          " TSPL_PI_DETAIL.Amount*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amount ,TSPL_PI_DETAIL.Disc_Per ,TSPL_PI_DETAIL.Disc_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Disc_Amt , " & _
                          " TSPL_PI_DETAIL.Amt_Less_Discount *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amt_Less_Discount, " & _
                          " (TSPL_PI_DETAIL.Total_Tax_Amt) *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Tax_Amt,TSPL_PI_DETAIL.Item_Net_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Amt,TSPL_PI_HEAD.vehicledesc,TSPL_VEHICLE_MASTER.Number as Vehicle_No,(TSPL_PI_Detail.Total_ItemAdd_Charge*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end)) as Additional_Charge,(case when Tax_Recoverable='N' then TSPL_PI_DETAIL.TAX1_Amt else 0 end) as NonRecoverable_Tax, "
        strTaxColumns = " TSPL_PI_DETAIL.TAX1 ,TSPL_PI_DETAIL.TAX1_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as TAX1_Amt, TSPL_PI_DETAIL.TAX1_Rate,TSPL_PI_DETAIL.TAX1+'%' as Tax1Rate,'' as _Type,tm.Tax_Recoverable "
        strAddChargeColumns = " , TSPL_PI_Detail.ItemAdd_Charge_Code1 as Add_Charge_Code1 ,TSPL_PI_Detail.ItemAdd_Calc_Charge_Amt1*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Add_Charge_Amt1 "

        strMCCMaterial += "   union all"
        '' query for tax1 applied============BM00000008364 ''richa add date filter
        strMCCMaterial += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_PI_DETAIL.tax1 and ttr.tax_Rate=TSPL_PI_DETAIL.TAX1_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PI_DETAIL.tax1=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_PI_HEAD .Add_Charge_Code1 =AdCh .Code left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PI_DETAIL.PO_ID where 2=2  and  convert(date,TSPL_PI_HEAD.PI_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_PI_HEAD.PI_Date ,103) <= convert(date,('" + To_Date + "'),103)  )s "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot columns exist only then query run(06/12/2016)
            strMCCMaterial += " pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot columns exist only then query run(06/12/2016)
            strMCCMaterial += " pivot (min(tax1_rate) for Tax1Rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot columns exist only then query run(06/12/2016)
            strMCCMaterial += " pivot (sum(Add_Charge_Amt1) for Add_Charge_Code1 in (" + strPivotForAddChargeInnerQuery + "))t"
        End If


        strMCCMaterial += "   union all"

        strSDCommonQuery = " select Distinct case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Trans_Type,TSPL_PI_DETAIL.Line_No,TSPL_PI_HEAD .ConvRate,TSPL_PI_DETAIL.srn_Id,TSPL_PI_DETAIL.MRN_Id,TSPL_PI_DETAIL.GRN_ID,TSPL_PI_HEAD.Status ,TSPL_PI_HEAD.Bill_To_Location, " & _
                          " TSPL_PI_HEAD.Vendor_Code as Customer_Code,TSPL_STATE_MASTER.STATE_CODE as [State Code],TSPL_STATE_MASTER.STATE_NAME as [State Name],case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Invoice_Type,TSPL_PI_HEAD.PI_NO , " & _
                          " convert(varchar,TSPL_PI_HEAD.PI_Date,103 ) as PI_Date,'' as Way_BillNo,TSPL_PI_HEAD.GRNO,TSPL_PI_HEAD.LR_NO,TSPL_PI_HEAD.Vendor_Invoice_No ,convert(varchar,TSPL_PI_Head.InvoiceDate,103) as Vendor_Invoice_Date,TSPL_PI_HEAD.Transporter as [Transporter],TSPL_PI_HEAD.TransporterDesc as [Transporter Name], TSPL_PI_DETAIL.Item_Code , " & _
                          " TSPL_PI_DETAIL.PI_Qty as Qty ,TSPL_PI_DETAIL.Unit_code ,TSPL_PI_DETAIL.Item_Cost*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Item_Cost , " & _
                          " TSPL_PI_DETAIL.Amount*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amount ,TSPL_PI_DETAIL.Disc_Per ,TSPL_PI_DETAIL.Disc_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Disc_Amt , " & _
                          " TSPL_PI_DETAIL.Amt_Less_Discount *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amt_Less_Discount, " & _
                          " (TSPL_PI_DETAIL.Total_Tax_Amt) *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Tax_Amt,TSPL_PI_DETAIL.Item_Net_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Amt,TSPL_PI_HEAD.vehicledesc,TSPL_VEHICLE_MASTER.Number as Vehicle_No,(TSPL_PI_Detail.Total_ItemAdd_Charge*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end)) as Additional_Charge,(case when Tax_Recoverable='N' then TSPL_PI_DETAIL.TAX2_Amt else 0 end) as NonRecoverable_Tax, "

        strTaxColumns = " TSPL_PI_DETAIL.TAX2 ,TSPL_PI_DETAIL.TAX2_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as TAX2_Amt,TSPL_PI_DETAIL.TAX2_Rate, TSPL_PI_DETAIL.TAX2+'%' as Tax2Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_PI_Detail.ItemAdd_Charge_Code2 as Add_Charge_Code2 ,TSPL_PI_Detail.ItemAdd_Calc_Charge_Amt2*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Add_Charge_Amt2 "

        ''add date filter richa
        strMCCMaterial += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PI_DETAIL.tax2 and ttr.tax_Rate=TSPL_PI_DETAIL.TAX2_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PI_DETAIL.tax2=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_PI_HEAD .Add_Charge_Code2 =AdCh .Code left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PI_DETAIL.PO_ID where 2=2  and  convert(date,TSPL_PI_HEAD.PI_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_PI_HEAD.PI_Date ,103) <= convert(date,('" + To_Date + "'),103)  )s "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot columns exist only then query run(06/12/2016)
            strMCCMaterial += " pivot (sum(tax2_amt) for tax2 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot columns exist only then query run(06/12/2016)
            strMCCMaterial += " pivot (min(tax2_rate) for tax2rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot columns exist only then query run(06/12/2016)
            strMCCMaterial += " pivot (sum(Add_Charge_Amt2) for Add_Charge_Code2 in (" + strPivotForAddChargeInnerQuery + "))t"
        End If


        strMCCMaterial += "  union all"

        strSDCommonQuery = " select Distinct case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Trans_Type,TSPL_PI_DETAIL.Line_No,TSPL_PI_HEAD .ConvRate,TSPL_PI_DETAIL.srn_Id,TSPL_PI_DETAIL.MRN_Id,TSPL_PI_DETAIL.GRN_ID,TSPL_PI_HEAD.Status ,TSPL_PI_HEAD.Bill_To_Location, " & _
                         " TSPL_PI_HEAD.Vendor_Code as Customer_Code,TSPL_STATE_MASTER.STATE_CODE as [State Code],TSPL_STATE_MASTER.STATE_NAME as [State Name],case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Invoice_Type,TSPL_PI_HEAD.PI_NO , " & _
                         " convert(varchar,TSPL_PI_HEAD.PI_Date,103 ) as PI_Date,'' as Way_BillNo,TSPL_PI_HEAD.GRNO,TSPL_PI_HEAD.LR_NO,TSPL_PI_HEAD.Vendor_Invoice_No ,convert(varchar,TSPL_PI_Head.InvoiceDate,103) as Vendor_Invoice_Date,TSPL_PI_HEAD.Transporter as [Transporter],TSPL_PI_HEAD.TransporterDesc as [Transporter Name], TSPL_PI_DETAIL.Item_Code , " & _
                         " TSPL_PI_DETAIL.PI_Qty as Qty ,TSPL_PI_DETAIL.Unit_code ,TSPL_PI_DETAIL.Item_Cost*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Item_Cost , " & _
                         " TSPL_PI_DETAIL.Amount*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amount ,TSPL_PI_DETAIL.Disc_Per ,TSPL_PI_DETAIL.Disc_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Disc_Amt , " & _
                         " TSPL_PI_DETAIL.Amt_Less_Discount *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amt_Less_Discount, " & _
                         " (TSPL_PI_DETAIL.Total_Tax_Amt) *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Tax_Amt,TSPL_PI_DETAIL.Item_Net_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Amt,TSPL_PI_HEAD.vehicledesc,TSPL_VEHICLE_MASTER.Number as Vehicle_No,(TSPL_PI_Detail.Total_ItemAdd_Charge*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end)) as Additional_Charge,(case when Tax_Recoverable='N' then TSPL_PI_DETAIL.TAX3_Amt else 0 end) as NonRecoverable_Tax, "
        strTaxColumns = " TSPL_PI_DETAIL.TAX3 ,TSPL_PI_DETAIL.TAX3_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end)  as TAX3_Amt, TSPL_PI_DETAIL.TAX3_Rate, TSPL_PI_DETAIL.TAX3+'%' as Tax3Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_PI_Detail.ItemAdd_Charge_Code3 as Add_Charge_Code3 ,TSPL_PI_Detail.ItemAdd_Calc_Charge_Amt3*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Add_Charge_Amt3 "
        ''add date filter richa
        strMCCMaterial += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PI_DETAIL.tax3 and ttr.tax_Rate=TSPL_PI_DETAIL.TAX3_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PI_DETAIL.tax3=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_PI_HEAD .Add_Charge_Code3 =AdCh .Code left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PI_DETAIL.PO_ID  where 2=2  and  convert(date,TSPL_PI_HEAD.PI_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_PI_HEAD.PI_Date ,103) <= convert(date,('" + To_Date + "'),103) )s "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot columns exist only then query run(06/12/2016)
            strMCCMaterial += " pivot (sum(tax3_amt) for tax3 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot columns exist only then query run(06/12/2016)
            strMCCMaterial += " pivot (min(tax3_rate) for tax3rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot columns exist only then query run(06/12/2016)
            strMCCMaterial += " pivot (sum(Add_Charge_Amt3) for Add_Charge_Code3 in (" + strPivotForAddChargeInnerQuery + "))t"
        End If


        strMCCMaterial += "   union all"

        strSDCommonQuery = " select Distinct case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Trans_Type,TSPL_PI_DETAIL.Line_No,TSPL_PI_HEAD .ConvRate,TSPL_PI_DETAIL.srn_Id,TSPL_PI_DETAIL.MRN_Id,TSPL_PI_DETAIL.GRN_ID,TSPL_PI_HEAD.Status ,TSPL_PI_HEAD.Bill_To_Location, " & _
                         " TSPL_PI_HEAD.Vendor_Code as Customer_Code,TSPL_STATE_MASTER.STATE_CODE as [State Code],TSPL_STATE_MASTER.STATE_NAME as [State Name],case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Invoice_Type,TSPL_PI_HEAD.PI_NO , " & _
                         " convert(varchar,TSPL_PI_HEAD.PI_Date,103 ) as PI_Date,'' as Way_BillNo,TSPL_PI_HEAD.GRNO,TSPL_PI_HEAD.LR_NO,TSPL_PI_HEAD.Vendor_Invoice_No ,convert(varchar,TSPL_PI_Head.InvoiceDate,103) as Vendor_Invoice_Date,TSPL_PI_HEAD.Transporter as [Transporter],TSPL_PI_HEAD.TransporterDesc as [Transporter Name], TSPL_PI_DETAIL.Item_Code , " & _
                         " TSPL_PI_DETAIL.PI_Qty as Qty ,TSPL_PI_DETAIL.Unit_code ,TSPL_PI_DETAIL.Item_Cost*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Item_Cost , " & _
                         " TSPL_PI_DETAIL.Amount*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amount ,TSPL_PI_DETAIL.Disc_Per ,TSPL_PI_DETAIL.Disc_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Disc_Amt , " & _
                         " TSPL_PI_DETAIL.Amt_Less_Discount *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amt_Less_Discount, " & _
                         " (TSPL_PI_DETAIL.Total_Tax_Amt) *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Tax_Amt,TSPL_PI_DETAIL.Item_Net_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Amt,TSPL_PI_HEAD.vehicledesc,TSPL_VEHICLE_MASTER.Number as Vehicle_No,(TSPL_PI_Detail.Total_ItemAdd_Charge*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end)) as Additional_Charge,(case when Tax_Recoverable='N' then TSPL_PI_DETAIL.TAX4_Amt else 0 end) as NonRecoverable_Tax, "

        strTaxColumns = " TSPL_PI_DETAIL.TAX4 ,TSPL_PI_DETAIL.TAX4_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as TAX4_Amt,TSPL_PI_DETAIL.TAX4_Rate, TSPL_PI_DETAIL.TAX4+'%' as Tax4Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_PI_Detail.ItemAdd_Charge_Code4 as Add_Charge_Code4 ,TSPL_PI_Detail.ItemAdd_Calc_Charge_Amt4*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Add_Charge_Amt4 "
        ''richa add date filter
        strMCCMaterial += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PI_DETAIL.tax4 and ttr.tax_Rate=TSPL_PI_DETAIL.TAX4_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PI_DETAIL.tax4=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_PI_HEAD .Add_Charge_Code4 =AdCh .Code left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PI_DETAIL.PO_ID where 2=2  and  convert(date,TSPL_PI_HEAD.PI_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_PI_HEAD.PI_Date ,103) <= convert(date,('" + To_Date + "'),103) )s "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot columns exist only then query run(06/12/2016)
            strMCCMaterial += " pivot (sum(tax4_amt) for tax4 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot columns exist only then query run(06/12/2016)
            strMCCMaterial += " pivot (min(tax4_rate) for tax4rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot columns exist only then query run(06/12/2016)
            strMCCMaterial += " pivot (sum(Add_Charge_Amt4) for Add_Charge_Code4 in (" + strPivotForAddChargeInnerQuery + "))t"
        End If


        strMCCMaterial += "  union all"
        strSDCommonQuery = " select Distinct case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Trans_Type,TSPL_PI_DETAIL.Line_No,TSPL_PI_HEAD .ConvRate,TSPL_PI_DETAIL.srn_Id,TSPL_PI_DETAIL.MRN_Id,TSPL_PI_DETAIL.GRN_ID,TSPL_PI_HEAD.Status ,TSPL_PI_HEAD.Bill_To_Location, " & _
                         " TSPL_PI_HEAD.Vendor_Code as Customer_Code,TSPL_STATE_MASTER.STATE_CODE as [State Code],TSPL_STATE_MASTER.STATE_NAME as [State Name],case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Invoice_Type,TSPL_PI_HEAD.PI_NO , " & _
                         " convert(varchar,TSPL_PI_HEAD.PI_Date,103 ) as PI_Date,'' as Way_BillNo,TSPL_PI_HEAD.GRNO,TSPL_PI_HEAD.LR_NO,TSPL_PI_HEAD.Vendor_Invoice_No ,convert(varchar,TSPL_PI_Head.InvoiceDate,103) as Vendor_Invoice_Date,TSPL_PI_HEAD.Transporter as [Transporter],TSPL_PI_HEAD.TransporterDesc as [Transporter Name], TSPL_PI_DETAIL.Item_Code , " & _
                         " TSPL_PI_DETAIL.PI_Qty as Qty ,TSPL_PI_DETAIL.Unit_code ,TSPL_PI_DETAIL.Item_Cost*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Item_Cost , " & _
                         " TSPL_PI_DETAIL.Amount*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amount ,TSPL_PI_DETAIL.Disc_Per ,TSPL_PI_DETAIL.Disc_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Disc_Amt , " & _
                         " TSPL_PI_DETAIL.Amt_Less_Discount *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amt_Less_Discount, " & _
                         " (TSPL_PI_DETAIL.Total_Tax_Amt) *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Tax_Amt,TSPL_PI_DETAIL.Item_Net_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Amt,TSPL_PI_HEAD.vehicledesc,TSPL_VEHICLE_MASTER.Number as Vehicle_No,(TSPL_PI_Detail.Total_ItemAdd_Charge*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end)) as Additional_Charge,(case when Tax_Recoverable='N' then TSPL_PI_DETAIL.TAX5_Amt else 0 end) as NonRecoverable_Tax, "

        strTaxColumns = " TSPL_PI_DETAIL.TAX5 ,TSPL_PI_DETAIL.TAX5_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as TAX5_Amt,TSPL_PI_DETAIL.TAX5_Rate, TSPL_PI_DETAIL.TAX5+'%' as Tax5Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = ", TSPL_PI_Detail.ItemAdd_Charge_Code5 as Add_Charge_Code5 ,TSPL_PI_Detail.ItemAdd_Calc_Charge_Amt5*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Add_Charge_Amt5 "
        ''richa add date filter
        strMCCMaterial += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PI_DETAIL.tax5 and ttr.tax_Rate=TSPL_PI_DETAIL.TAX5_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PI_DETAIL.tax5=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_PI_HEAD .Add_Charge_Code5 =AdCh .Code left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PI_DETAIL.PO_ID where 2=2 and  convert(date,TSPL_PI_HEAD.PI_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_PI_HEAD.PI_Date ,103) <= convert(date,('" + To_Date + "'),103) )s "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot columns exist only then query run(06/12/2016)
            strMCCMaterial += " pivot (sum(tax5_amt) for tax5 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot columns exist only then query run(06/12/2016)
            strMCCMaterial += " pivot (min(tax5_rate) for tax5rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot columns exist only then query run(06/12/2016)
            strMCCMaterial += " pivot (sum(Add_Charge_Amt5) for Add_Charge_Code5 in (" + strPivotForAddChargeInnerQuery + "))t"
        End If


        strMCCMaterial += "  union all"

        strSDCommonQuery = " select Distinct case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Trans_Type,TSPL_PI_DETAIL.Line_No,TSPL_PI_HEAD .ConvRate,TSPL_PI_DETAIL.srn_Id,TSPL_PI_DETAIL.MRN_Id,TSPL_PI_DETAIL.GRN_ID,TSPL_PI_HEAD.Status ,TSPL_PI_HEAD.Bill_To_Location, " & _
                         " TSPL_PI_HEAD.Vendor_Code as Customer_Code,TSPL_STATE_MASTER.STATE_CODE as [State Code],TSPL_STATE_MASTER.STATE_NAME as [State Name],case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Invoice_Type,TSPL_PI_HEAD.PI_NO , " & _
                         " convert(varchar,TSPL_PI_HEAD.PI_Date,103 ) as PI_Date,'' as Way_BillNo,TSPL_PI_HEAD.GRNO,TSPL_PI_HEAD.LR_NO,TSPL_PI_HEAD.Vendor_Invoice_No ,convert(varchar,TSPL_PI_Head.InvoiceDate,103) as Vendor_Invoice_Date,TSPL_PI_HEAD.Transporter as [Transporter],TSPL_PI_HEAD.TransporterDesc as [Transporter Name], TSPL_PI_DETAIL.Item_Code , " & _
                         " TSPL_PI_DETAIL.PI_Qty as Qty ,TSPL_PI_DETAIL.Unit_code ,TSPL_PI_DETAIL.Item_Cost*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Item_Cost , " & _
                         " TSPL_PI_DETAIL.Amount*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amount ,TSPL_PI_DETAIL.Disc_Per ,TSPL_PI_DETAIL.Disc_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Disc_Amt , " & _
                         " TSPL_PI_DETAIL.Amt_Less_Discount *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amt_Less_Discount, " & _
                         " (TSPL_PI_DETAIL.Total_Tax_Amt) *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Tax_Amt,TSPL_PI_DETAIL.Item_Net_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Amt,TSPL_PI_HEAD.vehicledesc,TSPL_VEHICLE_MASTER.Number as Vehicle_No,(TSPL_PI_Detail.Total_ItemAdd_Charge*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end)) as Additional_Charge,(case when Tax_Recoverable='N' then TSPL_PI_DETAIL.TAX6_Amt else 0 end) as NonRecoverable_Tax, "
        strTaxColumns = " TSPL_PI_DETAIL.TAX6 ,TSPL_PI_DETAIL.TAX6_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as TAX6_Amt,TSPL_PI_DETAIL.TAX6_Rate, TSPL_PI_DETAIL.TAX6+'%' as Tax6Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_PI_Detail.ItemAdd_Charge_Code6 as Add_Charge_Code6 ,TSPL_PI_Detail.ItemAdd_Calc_Charge_Amt6*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Add_Charge_Amt6 "
        ''richa add date filter 
        strMCCMaterial += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PI_DETAIL.tax6 and ttr.tax_Rate=TSPL_PI_DETAIL.TAX6_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PI_DETAIL.tax6=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_PI_HEAD .Add_Charge_Code6 =AdCh .Code left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PI_DETAIL.PO_ID where 2=2  and  convert(date,TSPL_PI_HEAD.PI_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_PI_HEAD.PI_Date ,103) <= convert(date,('" + To_Date + "'),103) )s "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot columns exist only then query run(06/12/2016)
            strMCCMaterial += " pivot (sum(tax6_amt) for tax6 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot columns exist only then query run(06/12/2016)
            strMCCMaterial += " pivot (min(tax6_rate) for tax6rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot columns exist only then query run(06/12/2016)
            strMCCMaterial += " pivot (sum(Add_Charge_Amt6) for Add_Charge_Code6 in (" + strPivotForAddChargeInnerQuery + "))t"
        End If


        strMCCMaterial += "  union all"

        strSDCommonQuery = " select Distinct case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Trans_Type,TSPL_PI_DETAIL.Line_No,TSPL_PI_HEAD .ConvRate,TSPL_PI_DETAIL.srn_Id,TSPL_PI_DETAIL.MRN_Id,TSPL_PI_DETAIL.GRN_ID,TSPL_PI_HEAD.Status ,TSPL_PI_HEAD.Bill_To_Location, " & _
                         " TSPL_PI_HEAD.Vendor_Code as Customer_Code,TSPL_STATE_MASTER.STATE_CODE as [State Code],TSPL_STATE_MASTER.STATE_NAME as [State Name],case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Invoice_Type,TSPL_PI_HEAD.PI_NO , " & _
                         " convert(varchar,TSPL_PI_HEAD.PI_Date,103 ) as PI_Date,'' as Way_BillNo,TSPL_PI_HEAD.GRNO,TSPL_PI_HEAD.LR_NO,TSPL_PI_HEAD.Vendor_Invoice_No ,convert(varchar,TSPL_PI_Head.InvoiceDate,103) as Vendor_Invoice_Date,TSPL_PI_HEAD.Transporter as [Transporter],TSPL_PI_HEAD.TransporterDesc as [Transporter Name], TSPL_PI_DETAIL.Item_Code , " & _
                         " TSPL_PI_DETAIL.PI_Qty as Qty ,TSPL_PI_DETAIL.Unit_code ,TSPL_PI_DETAIL.Item_Cost*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Item_Cost , " & _
                         " TSPL_PI_DETAIL.Amount*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amount ,TSPL_PI_DETAIL.Disc_Per ,TSPL_PI_DETAIL.Disc_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Disc_Amt , " & _
                         " TSPL_PI_DETAIL.Amt_Less_Discount *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amt_Less_Discount, " & _
                         " (TSPL_PI_DETAIL.Total_Tax_Amt) *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Tax_Amt,TSPL_PI_DETAIL.Item_Net_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Amt,TSPL_PI_HEAD.vehicledesc,TSPL_VEHICLE_MASTER.Number as Vehicle_No,(TSPL_PI_Detail.Total_ItemAdd_Charge*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end)) as Additional_Charge,(case when Tax_Recoverable='N' then TSPL_PI_DETAIL.TAX7_Amt else 0 end) as NonRecoverable_Tax, "
        strTaxColumns = " TSPL_PI_DETAIL.TAX7 ,TSPL_PI_DETAIL.TAX7_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as TAX7_Amt,TSPL_PI_DETAIL.TAX7_Rate, TSPL_PI_DETAIL.TAX7+'%' as Tax7Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_PI_Detail.ItemAdd_Charge_Code7 as Add_Charge_Code7 ,TSPL_PI_Detail.ItemAdd_Calc_Charge_Amt7*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Add_Charge_Amt7 "
        ''richa add date filter
        strMCCMaterial += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PI_DETAIL.tax7 and ttr.tax_Rate=TSPL_PI_DETAIL.TAX7_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PI_DETAIL.tax7=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_PI_HEAD .Add_Charge_Code7 =AdCh .Code left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PI_DETAIL.PO_ID where 2=2  and  convert(date,TSPL_PI_HEAD.PI_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_PI_HEAD.PI_Date ,103) <= convert(date,('" + To_Date + "'),103) )s "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot columns exist only then query run(06/12/2016)
            strMCCMaterial += " pivot (sum(tax7_amt) for tax7 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot columns exist only then query run(06/12/2016)
            strMCCMaterial += " pivot (min(tax7_rate) for tax7rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot columns exist only then query run(06/12/2016)
            strMCCMaterial += " pivot (sum(Add_Charge_Amt7) for Add_Charge_Code7 in (" + strPivotForAddChargeInnerQuery + "))t"
        End If


        strMCCMaterial += "  union all"

        strSDCommonQuery = " select Distinct case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Trans_Type,TSPL_PI_DETAIL.Line_No,TSPL_PI_HEAD .ConvRate,TSPL_PI_DETAIL.srn_Id,TSPL_PI_DETAIL.MRN_Id,TSPL_PI_DETAIL.GRN_ID,TSPL_PI_HEAD.Status ,TSPL_PI_HEAD.Bill_To_Location, " & _
                         " TSPL_PI_HEAD.Vendor_Code as Customer_Code,TSPL_STATE_MASTER.STATE_CODE as [State Code],TSPL_STATE_MASTER.STATE_NAME as [State Name],case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Invoice_Type,TSPL_PI_HEAD.PI_NO , " & _
                         " convert(varchar,TSPL_PI_HEAD.PI_Date,103 ) as PI_Date,'' as Way_BillNo,TSPL_PI_HEAD.GRNO,TSPL_PI_HEAD.LR_NO,TSPL_PI_HEAD.Vendor_Invoice_No ,convert(varchar,TSPL_PI_Head.InvoiceDate,103) as Vendor_Invoice_Date,TSPL_PI_HEAD.Transporter as [Transporter],TSPL_PI_HEAD.TransporterDesc as [Transporter Name], TSPL_PI_DETAIL.Item_Code , " & _
                         " TSPL_PI_DETAIL.PI_Qty as Qty ,TSPL_PI_DETAIL.Unit_code ,TSPL_PI_DETAIL.Item_Cost*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Item_Cost , " & _
                         " TSPL_PI_DETAIL.Amount*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amount ,TSPL_PI_DETAIL.Disc_Per ,TSPL_PI_DETAIL.Disc_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Disc_Amt , " & _
                         " TSPL_PI_DETAIL.Amt_Less_Discount *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amt_Less_Discount, " & _
                         " (TSPL_PI_DETAIL.Total_Tax_Amt) *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Tax_Amt,TSPL_PI_DETAIL.Item_Net_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Amt,TSPL_PI_HEAD.vehicledesc,TSPL_VEHICLE_MASTER.Number as Vehicle_No,(TSPL_PI_Detail.Total_ItemAdd_Charge*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end)) as Additional_Charge,(case when Tax_Recoverable='N' then TSPL_PI_DETAIL.TAX8_Amt else 0 end) as NonRecoverable_Tax, "
        strTaxColumns = " TSPL_PI_DETAIL.TAX8 ,TSPL_PI_DETAIL.TAX8_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as TAX8_Amt,TSPL_PI_DETAIL.TAX8_Rate, TSPL_PI_DETAIL.TAX8+'%' as Tax8Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_PI_Detail.ItemAdd_Charge_Code8 as Add_Charge_Code8 ,TSPL_PI_Detail.ItemAdd_Calc_Charge_Amt8*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Add_Charge_Amt8 "
        ''richa add date filter
        strMCCMaterial += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PI_DETAIL.tax8 and ttr.tax_Rate=TSPL_PI_DETAIL.TAX8_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PI_DETAIL.tax8=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_PI_HEAD .Add_Charge_Code8 =AdCh .Code left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PI_DETAIL.PO_ID where 2=2 and  convert(date,TSPL_PI_HEAD.PI_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_PI_HEAD.PI_Date ,103) <= convert(date,('" + To_Date + "'),103) )s "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot columns exist only then query run(06/12/2016)
            strMCCMaterial += " pivot (sum(tax8_amt) for tax8 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot columns exist only then query run(06/12/2016)
            strMCCMaterial += " pivot (min(tax8_rate) for tax8rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot columns exist only then query run(06/12/2016)
            strMCCMaterial += " pivot (sum(Add_Charge_Amt8) for Add_Charge_Code8 in (" + strPivotForAddChargeInnerQuery + "))t"
        End If


        strMCCMaterial += "  union all"

        strSDCommonQuery = " select Distinct case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Trans_Type,TSPL_PI_DETAIL.Line_No,TSPL_PI_HEAD .ConvRate,TSPL_PI_DETAIL.srn_Id,TSPL_PI_DETAIL.MRN_Id,TSPL_PI_DETAIL.GRN_ID,TSPL_PI_HEAD.Status ,TSPL_PI_HEAD.Bill_To_Location, " & _
                         " TSPL_PI_HEAD.Vendor_Code as Customer_Code,TSPL_STATE_MASTER.STATE_CODE as [State Code],TSPL_STATE_MASTER.STATE_NAME as [State Name],case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Invoice_Type,TSPL_PI_HEAD.PI_NO , " & _
                         " convert(varchar,TSPL_PI_HEAD.PI_Date,103 ) as PI_Date,'' as Way_BillNo,TSPL_PI_HEAD.GRNO,TSPL_PI_HEAD.LR_NO,TSPL_PI_HEAD.Vendor_Invoice_No ,convert(varchar,TSPL_PI_Head.InvoiceDate,103) as Vendor_Invoice_Date,TSPL_PI_HEAD.Transporter as [Transporter],TSPL_PI_HEAD.TransporterDesc as [Transporter Name], TSPL_PI_DETAIL.Item_Code , " & _
                         " TSPL_PI_DETAIL.PI_Qty as Qty ,TSPL_PI_DETAIL.Unit_code ,TSPL_PI_DETAIL.Item_Cost*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Item_Cost , " & _
                         " TSPL_PI_DETAIL.Amount*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amount ,TSPL_PI_DETAIL.Disc_Per ,TSPL_PI_DETAIL.Disc_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Disc_Amt , " & _
                         " TSPL_PI_DETAIL.Amt_Less_Discount *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amt_Less_Discount, " & _
                         " (TSPL_PI_DETAIL.Total_Tax_Amt) *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Tax_Amt,TSPL_PI_DETAIL.Item_Net_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Amt,TSPL_PI_HEAD.vehicledesc,TSPL_VEHICLE_MASTER.Number as Vehicle_No,(TSPL_PI_Detail.Total_ItemAdd_Charge*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end)) as Additional_Charge,(case when Tax_Recoverable='N' then TSPL_PI_DETAIL.TAX9_Amt else 0 end) as NonRecoverable_Tax, "

        strTaxColumns = " TSPL_PI_DETAIL.TAX9 ,TSPL_PI_DETAIL.TAX9_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as TAX9_Amt,TSPL_PI_DETAIL.TAX9_Rate, TSPL_PI_DETAIL.TAX9+'%' as Tax9Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_PI_Detail.ItemAdd_Charge_Code9 as Add_Charge_Code9 ,TSPL_PI_Detail.ItemAdd_Calc_Charge_Amt9*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Add_Charge_Amt9 "
        ''richa add date filter
        strMCCMaterial += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PI_DETAIL.tax9 and ttr.tax_Rate=TSPL_PI_DETAIL.TAX9_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PI_DETAIL.tax9=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_PI_HEAD .Add_Charge_Code9 =AdCh .Code left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PI_DETAIL.PO_ID where 2=2  and  convert(date,TSPL_PI_HEAD.PI_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_PI_HEAD.PI_Date ,103) <= convert(date,('" + To_Date + "'),103) )s "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot columns exist only then query run(06/12/2016)
            strMCCMaterial += " pivot (sum(tax9_amt) for tax9 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot columns exist only then query run(06/12/2016)
            strMCCMaterial += " pivot (min(tax9_rate) for tax9rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot columns exist only then query run(06/12/2016)
            strMCCMaterial += " pivot (sum(Add_Charge_Amt9) for Add_Charge_Code9 in (" + strPivotForAddChargeInnerQuery + "))t"
        End If


        strMCCMaterial += "  union all"

        strSDCommonQuery = " select Distinct case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Trans_Type,TSPL_PI_DETAIL.Line_No,TSPL_PI_HEAD .ConvRate,TSPL_PI_DETAIL.srn_Id,TSPL_PI_DETAIL.MRN_Id,TSPL_PI_DETAIL.GRN_ID,TSPL_PI_HEAD.Status ,TSPL_PI_HEAD.Bill_To_Location, " & _
                         " TSPL_PI_HEAD.Vendor_Code as Customer_Code,TSPL_STATE_MASTER.STATE_CODE as [State Code],TSPL_STATE_MASTER.STATE_NAME as [State Name],case when TSPL_PI_HEAD.Document_Type= 'PI' then 'PI' else 'MT' end as Invoice_Type,TSPL_PI_HEAD.PI_NO , " & _
                         " convert(varchar,TSPL_PI_HEAD.PI_Date,103 ) as PI_Date,'' as Way_BillNo,TSPL_PI_HEAD.GRNO,TSPL_PI_HEAD.LR_NO,TSPL_PI_HEAD.Vendor_Invoice_No ,convert(varchar,TSPL_PI_Head.InvoiceDate,103) as Vendor_Invoice_Date,TSPL_PI_HEAD.Transporter as [Transporter],TSPL_PI_HEAD.TransporterDesc as [Transporter Name], TSPL_PI_DETAIL.Item_Code , " & _
                         " TSPL_PI_DETAIL.PI_Qty as Qty ,TSPL_PI_DETAIL.Unit_code ,TSPL_PI_DETAIL.Item_Cost*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Item_Cost , " & _
                         " TSPL_PI_DETAIL.Amount*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amount ,TSPL_PI_DETAIL.Disc_Per ,TSPL_PI_DETAIL.Disc_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Disc_Amt , " & _
                         " TSPL_PI_DETAIL.Amt_Less_Discount *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Amt_Less_Discount, " & _
                         " (TSPL_PI_DETAIL.Total_Tax_Amt) *(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Tax_Amt,TSPL_PI_DETAIL.Item_Net_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Total_Amt,TSPL_PI_HEAD.vehicledesc,TSPL_VEHICLE_MASTER.Number as Vehicle_No,(TSPL_PI_Detail.Total_ItemAdd_Charge*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end)) as Additional_Charge,(case when Tax_Recoverable='N' then TSPL_PI_DETAIL.TAX10_Amt else 0 end) as NonRecoverable_Tax, "

        strTaxColumns = " TSPL_PI_DETAIL.TAX10 ,TSPL_PI_DETAIL.TAX10_Amt*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as TAX10_Amt,TSPL_PI_DETAIL.TAX10_Rate,TSPL_PI_DETAIL.TAX10+'%' as Tax10Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_PI_Detail.ItemAdd_Charge_Code10 as Add_Charge_Code10 ,TSPL_PI_Detail.ItemAdd_Calc_Charge_Amt10*(case when coalesce(TSPL_PI_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PI_Head.convrate,0) end) as Add_Charge_Amt10 "
        ''richa add date filter
        strMCCMaterial += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & "  left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PI_DETAIL.tax10 and ttr.tax_Rate=TSPL_PI_DETAIL.TAX10_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PI_DETAIL.tax10=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_PI_HEAD .Add_Charge_Code10 =AdCh .Code left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PI_DETAIL.PO_ID  where 2=2  and  convert(date,TSPL_PI_HEAD.PI_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_PI_HEAD.PI_Date ,103) <= convert(date,('" + To_Date + "'),103) )s "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot columns exist only then query run(06/12/2016)
            strMCCMaterial += " pivot (sum(tax10_amt) for tax10 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot columns exist only then query run(06/12/2016)
            strMCCMaterial += " pivot (min(tax10_rate) for tax10rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot columns exist only then query run(06/12/2016)
            strMCCMaterial += " pivot (sum(Add_Charge_Amt10) for Add_Charge_Code10 in (" + strPivotForAddChargeInnerQuery + "))t"
        End If


        strMCCMaterial += " )final1)final"
        strMCCMaterial += "  left join tspl_srn_detail on tspl_srn_detail.srn_no=final.SRN_Id and tspl_srn_detail.Item_Code = final.Item_Code"
        strMCCMaterial += " and isnull(tspl_srn_detail.MRN_Id,'') =isnull(final.MRN_ID,'' ) "
        strMCCMaterial += " and isnull(tspl_srn_detail.GRN_ID,'') =isnull(final.GRN_ID,'') "
        strMCCMaterial += " and isnull(tspl_srn_detail.PO_ID ,'')=isnull(final.PO_ID ,'')"
        strMCCMaterial += " left join TSPL_MRN_DETAIL on TSPL_MRN_DETAIL.MRN_No = tspl_srn_detail.MRN_Id "
        strMCCMaterial += " and TSPL_MRN_DETAIL.Item_Code =tspl_srn_detail.Item_Code "
        strMCCMaterial += " and isnull(tspl_srn_detail.GRN_ID,'') =isnull(TSPL_MRN_DETAIL.GRN_ID,'') "
        strMCCMaterial += " and isnull(tspl_srn_detail.PO_ID ,'')=isnull(TSPL_MRN_DETAIL.PO_ID ,'')"
        strMCCMaterial += " left join TSPL_MRN_HEAD on TSPL_MRN_HEAD.MRN_No =TSPL_MRN_DETAIL.MRN_No  "
        strMCCMaterial += "left join TSPL_GRN_DETAIL on isnull(TSPL_GRN_DETAIL.GRN_No,'') =isnull(TSPL_MRN_DETAIL.GRN_Id,'') and TSPL_GRN_DETAIL.Item_Code =TSPL_MRN_DETAIL.Item_Code and isnull(TSPL_GRN_DETAIL.PO_Id,'') =isnull(TSPL_MRN_DETAIL.PO_ID,'')  "
        strMCCMaterial += " left join TSPL_GRN_HEAD on isnull(TSPL_GRN_HEAD.GRN_No,'') =isnull(TSPL_GRN_DETAIL.GRN_No,'')  "
        strMCCMaterial += "left join TSPL_PURCHASE_ORDER_DETAIL on isnull(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No,'')  =isnull(TSPL_GRN_DETAIL.PO_Id,'') and TSPL_GRN_DETAIL.Item_Code =TSPL_PURCHASE_ORDER_DETAIL.Item_Code  and isnull(TSPL_MRN_DETAIL.PO_ID,'') =isnull(TSPL_GRN_DETAIL.PO_Id,'')  left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No =TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No  "
        strMCCMaterial += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =final.Bill_To_Location "
        strMCCMaterial += " left outer join tspl_item_master on tspl_item_master.Item_Code =final.Item_Code "
        strMCCMaterial += " left outer join TSPL_vendor_MASTER on TSPL_vendor_MASTER .Vendor_Code =final.Customer_Code "
        strMCCMaterial += " LEFT OUTER JOIN TSPL_vendor_MASTER as Parent_Master ON Parent_Master.Vendor_Code=TSPL_vendor_MASTER.Parent_Vendor_Code "
        strMCCMaterial += " left outer join " & "(" & qryQC & ") as QC" & " on QC.Item_Code =final.Item_Code  Left join (select * from (select PI_No,Item_Code,Item_Net_AMt from " _
            & " tspl_Pi_Detail where  item_Code='PACKING') Pivoting pivot(SUm(Item_Net_AMt) for item_Code in ([PACKING]) ) pivoted) as pvt on pvt.PI_No=final.PI_No "
        'where TSPL_MRN_HEAD.IsCancel=0 and TSPL_GRN_HEAD.IsCancel=0"
        strMCCMaterial += " group by  final.Trans_Type,final .Status  ,final.PI_NO,final.SRN_Id ,final.Item_Code ,final.Bill_To_Location ,final.Customer_Code ,final.Qty ,final.Total_Tax_Amt ,final.Invoice_Type ,final.PI_Date,final.Way_BillNo,Final.GRNO,final.LR_NO ,final.Unit_code ,final.Item_Cost ,final.Amount ,final.Disc_Per ,final.Disc_Amt ,final.Amt_Less_Discount ,final.Total_Amt,QC.FAT_Per,QC.SNF_Per,vehicledesc,Vehicle_No,final.Additional_Charge,final.Po_Id,final.MRN_Id,final.GRN_ID  " ', " + strPivotFoGrouprOuterQuery + "

        strMCCMaterial += "  union all"


        'strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX9 ,0 as TAX9_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX9_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX9+'%' as tax9rate  "
        strMCCMaterial += " select * from (Select 'MCC Transfer' as Trans_Type,0 as Line_No,0 as ConvRate,recv.location_Code as  Bill_To_Location,recv.Add1 + ' ' + recv.Add2 + ' ' + recv.Add3 As Location_ADD,recv.State as [Location State],TSPL_MILK_TRANSFER_IN.isPosted as Status," _
            & " recv.location_desc,'MCC Transfer' as Invoice_Type,TSPL_MILK_TRANSFER_IN.Receipt_Challan_No as PI_NO,'' as SRN_Id  , '' as MRN_Id,'' as GRN_ID, convert(varchar,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,103 ) as PI_Date,'' as Way_BillNo,'' as [GRNO],'' as LR_NO,TSPL_MILK_TRANSFER_IN.Dispatch_Challan_No as Vendor_Invoice_No,convert(varchar,TSPL_MCC_Dispatch_Challan.Dispatch_Date,103) as Vendor_Invoice_Date," _
            & " '' as vehicledesc,tm.tanker_NO as Vehicle_No,0  as Additional_Charge ,  TSPL_MCC_Dispatch_Challan.mcc_code as Customer_Code,  TSPL_LOCATION_MASTER.Location_Desc  as Customer_Name,Tspl_Location_Master.Add1 + ' ' + Tspl_Location_Master.Add2 + ' ' + Tspl_Location_Master.Add3 As Vendor_ADD,tspl_State_Master.state_COde as [State Code],tspl_State_Master.state_Name as [State Name]" _
            & ",tspl_LocaTION_mASTER.tin_No as [TIN No] ,'' as [Parent Vendor No],'' as [Parent Vendor Code],'' as [Parent Vendor Name],Tanker_Transporter_Code,tm.description, TSPL_MCC_Dispatch_Challan.Item_Code," _
            & " TSPL_MCC_Dispatch_Challan.Item_Desc , t_Qty_Recd.Net_Weight  as Qty ,t_Qty_Recd.UOM as  Unit_code ,round(((TSPL_MCC_Dispatch_Challan.FAT_RATE *(coalesce(cast(t_FAT_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)) +(TSPL_MCC_Dispatch_Challan.SNF_RATE  *(coalesce(cast(t_SNF_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)))/coalesce(t_qty_recd.net_Weight,1) ,2) as  Item_Cost ,  t_FAT_Recd.Param_Field_Value as [FAT Per],  t_SNF_Recd.Param_Field_Value as [SNF Per],(coalesce(cast(t_FAT_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100) as [FAT KG],(coalesce(cast(t_Snf_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100) as [SNF KG],Round(Round(((TSPL_MCC_Dispatch_Challan.FAT_RATE *(coalesce(cast(t_FAT_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)) +(TSPL_MCC_Dispatch_Challan.SNF_RATE  *(coalesce(cast(t_SNF_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)))*100,2)/100,2) as  Amount ,0 as Disc_Per " _
            & " ,0 as Disc_Amt ,  Round(Round(((TSPL_MCC_Dispatch_Challan.FAT_RATE *(coalesce(cast(t_FAT_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)) +(TSPL_MCC_Dispatch_Challan.SNF_RATE  *(coalesce(cast(t_SNF_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)))*100,2)/100,2) as  Amt_Less_Discount,0 as [EMP],0 as [Incentive],0 as [IncentiveEMP]   " & strPivotForTransfer_In & "" & strPivotFortRANSFER_INPercentQuery & " " & strPivotForAddChargeForZeroTransfer_In & ",'' as [Tax Type],  0 as Total_Tax_Amt ,Round(Round(((TSPL_MCC_Dispatch_Challan.FAT_RATE *(coalesce(cast(t_FAT_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)) +(TSPL_MCC_Dispatch_Challan.SNF_RATE  *(coalesce(cast(t_SNF_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)))*100,2)/100,2) as   Total_Amt,TSPL_vendor_Invoice_Head.Document_No as [AP Document No],TSPL_vendor_Invoice_Head.Document_Total [AP Document Amt],TSPL_vendor_Invoice_Head.Discount_Amount as [AP Document Discount Amt],TSPL_vendor_Invoice_Head.amount_less_Discount as [AP Amount Before Tax],TSPL_MILK_TRANSFER_IN.Dispatch_Challan_No as against_PoInvoice_No,TSPL_vendor_Invoice_Head.Against_PurchaseREturn_No,TSPL_vendor_Invoice_Head.total_tax as [AP Total Tax],TSPL_vendor_Invoice_Head.total_Add_Charge as [AP Total Add Charge],TSPL_vendor_Invoice_Head.Total_landed_Amt as [AP Landed Amt],TSPL_vendor_Invoice_Head.Against_MillkpurchaseInvoice_No,TSPL_vendor_Invoice_Head.Against_BulkMillkpurchaseInvoice_No,TSPL_vendor_Invoice_Head.Document_total as [AP Document Total],0 as MRP,0 as NonRecoverable_Tax,'' as Purchase_Tax_Invoice ,'' as Def_DOC_Type,'' as PO,Null as PO_Date,'' as created_by,'' as Modify_By from TSPL_MCC_Dispatch_Challan  left outer " _
            & " join TSPL_MILK_TRANSFER_IN on TSPL_MILK_TRANSFER_IN.Dispatch_Challan_No =TSPL_MCC_Dispatch_Challan.Chalan_NO  LEFT JOIN tspl_Mcc_Master ON tspl_Mcc_Master.MCC_Code=TSPL_MCC_Dispatch_Challan.MCC_CODE  left join tspl_Location_master on tspl_Location_master.location_code=TSPL_MCC_Dispatch_Challan.mcc_Code left join tspl_Location_master recv on recv.location_code=TSPL_MILK_TRANSFER_IN.Location_Code left join TSPL_vendor_Invoice_Head on  TSPL_vendor_Invoice_Head.vendor_Invoice_No=TSPL_MILK_TRANSFER_IN.Receipt_Challan_No left join tspl_tanker_Master tm on tm.tanker_no=TSPL_MCC_Dispatch_Challan.tanker_No Left Outer Join (Select TSPL_QC_Parameter_Detail.* From TSPL_MILK_TRANSFER_IN Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No  = TSPL_MILK_TRANSFER_IN.QC_No  where TSPL_QC_Parameter_Detail.Param_Type = 'SNF') t_SNF_Recd On t_SNF_Recd.QC_No   = TSPL_MILK_TRANSFER_IN.QC_No " _
            & " Left Outer Join (Select TSPL_QC_Parameter_Detail.* From TSPL_MILK_TRANSFER_IN Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No  = TSPL_MILK_TRANSFER_IN.QC_No  where TSPL_QC_Parameter_Detail.Param_Type = 'FAT' ) t_FAT_Recd On t_FAT_Recd.QC_No   = TSPL_MILK_TRANSFER_IN.QC_No " _
            & " Left Outer Join (Select TSPL_Weighment_Detail.* From TSPL_MILK_TRANSFER_IN Left Outer Join TSPL_Weighment_Detail On TSPL_Weighment_Detail.Weighment_No  = TSPL_MILK_TRANSFER_IN.Weighment_No ) t_Qty_Recd On t_Qty_Recd.Weighment_No   = TSPL_MILK_TRANSFER_IN.Weighment_No  left join TSPL_STATE_MASTER on tspl_State_Master.STATE_CODE=Tspl_Mcc_Master.State_Code  where  convert(date,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date ,103) <= convert(date,('" + To_Date + "'),103) )t"
        '========================Added by preeti gupta [Milk Transfer In Return][]KDI/04/07/18-000386===================
        strMCCMaterial += "  union all"


        'strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX9 ,0 as TAX9_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX9_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX9+'%' as tax9rate  "
        strMCCMaterial += " select * from (Select 'MCC Transfer' as Trans_Type,0 as Line_No,0 as ConvRate,recv.location_Code as  Bill_To_Location,recv.Add1 + ' ' + recv.Add2 + ' ' + recv.Add3 As Location_ADD,recv.State as [Location State],TSPL_MILK_TRANSFER_IN_RETURN.isPosted as Status," _
            & " recv.location_desc,'MCC Transfer' as Invoice_Type,TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_No as PI_NO,'' as SRN_Id  , '' as MRN_Id,'' as GRN_ID, convert(varchar,TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_Date,103 ) as PI_Date,'' as Way_BillNo,'' as [GRNO],'' as LR_NO,TSPL_MILK_TRANSFER_IN_RETURN.Dispatch_Challan_No as Vendor_Invoice_No,convert(varchar,TSPL_MCC_Dispatch_Challan.Dispatch_Date,103) as Vendor_Invoice_Date," _
            & " '' as vehicledesc,tm.tanker_NO as Vehicle_No,0  as Additional_Charge ,  TSPL_MCC_Dispatch_Challan.mcc_code as Customer_Code,  TSPL_LOCATION_MASTER.Location_Desc  as Customer_Name,Tspl_Location_Master.Add1 + ' ' + Tspl_Location_Master.Add2 + ' ' + Tspl_Location_Master.Add3 As Vendor_ADD,tspl_State_Master.state_COde as [State Code],tspl_State_Master.state_Name as [State Name]" _
            & ",tspl_LocaTION_mASTER.tin_No as [TIN No] ,'' as [Parent Vendor No],'' as [Parent Vendor Code],'' as [Parent Vendor Name],Tanker_Transporter_Code,tm.description, TSPL_MCC_Dispatch_Challan.Item_Code," _
            & " TSPL_MCC_Dispatch_Challan.Item_Desc , t_Qty_Recd.Net_Weight*(-1)  as Qty ,t_Qty_Recd.UOM as  Unit_code ,(round(((TSPL_MCC_Dispatch_Challan.FAT_RATE *(coalesce(cast(t_FAT_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)) +(TSPL_MCC_Dispatch_Challan.SNF_RATE  *(coalesce(cast(t_SNF_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)))/coalesce(t_qty_recd.net_Weight,1) ,2))*(-1) as  Item_Cost ,  t_FAT_Recd.Param_Field_Value as [FAT Per],  t_SNF_Recd.Param_Field_Value as [SNF Per],(coalesce(cast(t_FAT_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)*(-1) as [FAT KG],(coalesce(cast(t_Snf_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)*(-1) as [SNF KG],(Round(Round(((TSPL_MCC_Dispatch_Challan.FAT_RATE *(coalesce(cast(t_FAT_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)) +(TSPL_MCC_Dispatch_Challan.SNF_RATE  *(coalesce(cast(t_SNF_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)))*100,2)/100,2))*(-1) as  Amount ,0 as Disc_Per " _
            & " ,0 as Disc_Amt ,  (Round(Round(((TSPL_MCC_Dispatch_Challan.FAT_RATE *(coalesce(cast(t_FAT_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)) +(TSPL_MCC_Dispatch_Challan.SNF_RATE  *(coalesce(cast(t_SNF_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)))*100,2)/100,2))*(-1) as  Amt_Less_Discount,0 as [EMP],0 as [Incentive],0 as [IncentiveEMP]   " & strPivotForTransfer_In & "" & strPivotFortRANSFER_INPercentQuery & " " & strPivotForAddChargeForZeroTransfer_In & ",'' as [Tax Type],  0 as Total_Tax_Amt ,(Round(Round(((TSPL_MCC_Dispatch_Challan.FAT_RATE *(coalesce(cast(t_FAT_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)) +(TSPL_MCC_Dispatch_Challan.SNF_RATE  *(coalesce(cast(t_SNF_Recd.Param_Field_Value as float),0) * coalesce(t_QTy_recd.net_Weight,0)/100)))*100,2)/100,2))*(-1) as   Total_Amt,TSPL_vendor_Invoice_Head.Document_No as [AP Document No],TSPL_vendor_Invoice_Head.Document_Total*(-1) [AP Document Amt],TSPL_vendor_Invoice_Head.Discount_Amount*(-1) as [AP Document Discount Amt],TSPL_vendor_Invoice_Head.amount_less_Discount*(-1) as [AP Amount Before Tax],TSPL_MILK_TRANSFER_IN_RETURN.Dispatch_Challan_No as against_PoInvoice_No,TSPL_vendor_Invoice_Head.Against_PurchaseREturn_No,TSPL_vendor_Invoice_Head.total_tax*(-1) as [AP Total Tax],TSPL_vendor_Invoice_Head.total_Add_Charge as [AP Total Add Charge],TSPL_vendor_Invoice_Head.Total_landed_Amt*(-1) as [AP Landed Amt],TSPL_vendor_Invoice_Head.Against_MillkpurchaseInvoice_No,TSPL_vendor_Invoice_Head.Against_BulkMillkpurchaseInvoice_No,TSPL_vendor_Invoice_Head.Document_total*(-1) as [AP Document Total],0 as MRP,0 as NonRecoverable_Tax,'' as Purchase_Tax_Invoice ,'' as Def_DOC_Type,'' as PO,Null as PO_Date,'' as created_by,'' as Modify_By from TSPL_MILK_TRANSFER_IN_RETURN  left outer " _
            & " join TSPL_MCC_Dispatch_Challan on TSPL_MILK_TRANSFER_IN_RETURN.Dispatch_Challan_No =TSPL_MCC_Dispatch_Challan.Chalan_NO  LEFT JOIN tspl_Mcc_Master ON tspl_Mcc_Master.MCC_Code=TSPL_MCC_Dispatch_Challan.MCC_CODE  left join tspl_Location_master on tspl_Location_master.location_code=TSPL_MCC_Dispatch_Challan.mcc_Code left join tspl_Location_master recv on recv.location_code=TSPL_MILK_TRANSFER_IN_RETURN.Location_Code left join TSPL_vendor_Invoice_Head on  TSPL_vendor_Invoice_Head.vendor_Invoice_No=TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_No left join tspl_tanker_Master tm on tm.tanker_no=TSPL_MCC_Dispatch_Challan.tanker_No Left Outer Join (Select TSPL_QC_Parameter_Detail.* From TSPL_MILK_TRANSFER_IN_RETURN Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No  = TSPL_MILK_TRANSFER_IN_RETURN.QC_No  where TSPL_QC_Parameter_Detail.Param_Type = 'SNF') t_SNF_Recd On t_SNF_Recd.QC_No   = TSPL_MILK_TRANSFER_IN_RETURN.QC_No " _
            & " Left Outer Join (Select TSPL_QC_Parameter_Detail.* From TSPL_MILK_TRANSFER_IN_RETURN Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No  = TSPL_MILK_TRANSFER_IN_RETURN.QC_No  where TSPL_QC_Parameter_Detail.Param_Type = 'FAT' ) t_FAT_Recd On t_FAT_Recd.QC_No   = TSPL_MILK_TRANSFER_IN_RETURN.QC_No " _
            & " Left Outer Join (Select TSPL_Weighment_Detail.* From TSPL_MILK_TRANSFER_IN_RETURN Left Outer Join TSPL_Weighment_Detail On TSPL_Weighment_Detail.Weighment_No  = TSPL_MILK_TRANSFER_IN_RETURN.Weighment_No ) t_Qty_Recd On t_Qty_Recd.Weighment_No   = TSPL_MILK_TRANSFER_IN_RETURN.Weighment_No  left join TSPL_STATE_MASTER on tspl_State_Master.STATE_CODE=Tspl_Mcc_Master.State_Code  where  convert(date,TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_Date ,103) <= convert(date,('" + To_Date + "'),103) )t"
        '============================================================================================

        strMCCMaterial += "  union all"

        strMCCMaterial += " select * from (Select 'Transfer' as Trans_Type,0 as Line_No,0 as ConvRate ,recv.location_Code as  Bill_To_Location,recv.Add1 + ' ' + recv.Add2 + ' ' + recv.Add3 As Location_ADD,recv.State as [Location State],TSPL_TRANSFER_ORDER_HEAD.Status as Status, recv.location_desc " _
        & "  ,'Transfer' as Invoice_Type,TSPL_TRANSFER_ORDER_HEAD.Document_No as PI_NO,'' as SRN_Id ,'' as MRN_Id,'' as GRN_ID,  convert(varchar,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103 ) as PI_Date,TSPL_TRANSFER_ORDER_HEAD.waybill_No as Way_BillNo,TSPL_TRANSFER_ORDER_HEAD.GR_No as [GRNO],'' as LR_NO," _
        & " TSPL_TRANSFER_ORDER_HEAD.transferoutno  as Vendor_Invoice_No,convert(varchar,Out.DOcument_Date,103) as Vendor_Invoice_Date, '' as vehicledesc,TSPL_TRANSFER_ORDER_HEAD.Vehicle_No as Vehicle_No,0  as Additional_Charge ,  Tspl_Location_Master.Location_Code as Customer_Code," _
        & " Tspl_Location_Master.Location_Desc  as Customer_Name,Tspl_Location_Master.Add1 + ' ' + Tspl_Location_Master.Add2 + ' ' + Tspl_Location_Master.Add3 As Vendor_ADD,Tspl_State_Master.state_Code as [State Code],Tspl_State_Master.state_name as [State Name],Tspl_Location_Master.tin_No as [TIN No],'' as [Parent Vendor No],'' as [Parent Vendor Code],'' as [Parent Vendor Name], " _
        & " tspl_transport_Master.Transport_id as [Transport Code],Tspl_Transport_Master.Transporter_Name as [Transporter Name],TSPL_TRANSFER_ORDER_DETAIL.Item_Code, TSPL_TRANSFER_ORDER_DETAIL.Item_Desc ,  TSPL_TRANSFER_ORDER_DETAIL.In_Qty  as Qty ,TSPL_TRANSFER_ORDER_DETAIL.Unit_code " _
        & " as  Unit_code ,coalesce(TSPL_TRANSFER_ORDER_DETAIL.Price,TSPL_TRANSFER_ORDER_DETAIL.Item_Cost)  as  Item_Cost ,   QC.FAT_Per as [FAT Per],   QC.SNF_Per as [SNF Per],cast((TSPL_TRANSFER_ORDER_DETAIL.In_Qty*Qc.SNF_Per*Stock_SU.Conversion_Factor)/(100*coalesce(StockKG.Conversion_Factor,1)) as numeric(18,3)) as [FAT KG],cast((TSPL_TRANSFER_ORDER_DETAIL.In_Qty*Qc.SNF_Per*Stock_SU.Conversion_Factor)/(100*coalesce(StockKG.Conversion_Factor,1)) as Numeric(18,3)) as [SNF KG],TSPL_TRANSFER_ORDER_DETAIL.Amount " _
        & " ,TSPL_TRANSFER_ORDER_DETAIL.Disc_Per as Disc_Per  ,TSPL_TRANSFER_ORDER_DETAIL.Disc_Amt as Disc_Amt ,  TSPL_TRANSFER_ORDER_DETAIL.Amount as  Amt_Less_Discount,0 as [EMP],0 as [Incentive],0 as [IncentiveEMP]   " _
        & " " & strTransferTaxColumns & "" & strTransferTaxPerColumns & "  " & strPivotForAddChargeForZeroTransfer_In & ",case when coalesce(out.is_AgainstformF,0)=1 then 'F' else '' end as [Tax Type],  TSPL_TRANSFER_ORDER_DETAIL.Total_Tax_Amt ," _
        & " TSPL_TRANSFER_ORDER_DETAIL.Item_Net_Amt as   Total_Amt,'' as [AP Document No],0 as  [AP Document Amt],0 as [AP Document Discount Amt],0 as [AP Amount Before Tax]," _
        & " TSPL_TRANSFER_ORDER_HEAD.transferoutno as against_PoInvoice_No,'' as Against_PurchaseREturn_No,0 as [AP Total Tax],0 as [AP Total Add Charge],0 as [AP Landed Amt]," _
        & " '' as Against_MillkpurchaseInvoice_No,'' as Against_BulkMillkpurchaseInvoice_No,0 as  [AP Document Total],TSPL_TRANSFER_ORDER_DETAIL.MRP,0 as NonRecoverable_Tax,'' as Purchase_Tax_Invoice,'' as Def_DOC_Type,'' as PO,Null as PO_Date,'' as created_by,'' as Modify_By from TSPL_TRANSFER_ORDER_HEAD " _
        & " left outer  join TSPL_TRANSFER_ORDER_DETAIL on TSPL_TRANSFER_ORDER_HEAD.Document_No =TSPL_TRANSFER_ORDER_DETAIL.Document_NO  " _
        & "  left join TSPL_TRANSFER_ORDER_HEAD out on out.Document_No=TSPL_TRANSFER_ORDER_HEAD.TransferOutNo  left join tspl_Location_master on tspl_Location_master.LOcation_Code=out.From_Location " _
        & " left join tspl_Location_master recv on recv.location_code=TSPL_TRANSFER_ORDER_Head.To_Location left join TSPL_TRANSPORT_MASTER on " _
        & " TSPL_TRANSPORT_MASTER.transport_Id=tspl_Transfer_Order_Head.Transport_Id  left outer join " & "(" & qryQC & ") as QC" & " on QC.Item_Code =" _
        & " TSPL_TRANSFER_ORDER_DETAIL.Item_Code  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on " _
        & " TSPL_TRANSFER_ORDER_DETAIL.Item_Code=Stock_SU.Item_Code and TSPL_TRANSFER_ORDER_DETAIL.Unit_code=Stock_SU.UOM_Code  " _
        & " left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='KG') as StockKG on " _
        & " TSPL_TRANSFER_ORDER_DETAIL.Item_Code=StockKG.Item_Code  " _
        & " left join TSPL_STATE_MASTER on tspl_State_Master.STATE_CODE=Tspl_Location_Master.State where TSPL_TRANSFER_ORDER_Head.Transfer_Type='I' and convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date ,103) <= convert(date,('" + To_Date + "'),103) )t"

        ''richa agarwal

        strMCCMaterial += "  union all"

        strMCCMaterial += " select * from (Select 'Transfer Return' as Trans_Type,0 as Line_No,0 as ConvRate ,recv.location_Code as  Bill_To_Location, recv.Add1 + ' ' + recv.Add2 + ' ' + recv.Add3 As Location_ADD,recv.State as [Location State],TSPL_TRANSFER_ORDER_HEAD.Status as Status, recv.location_desc , " _
        & "  'Transfer Return' as Invoice_Type,TSPL_TRANSFER_RETURN.Document_No as PI_NO,'' as SRN_Id , '' as MRN_Id,'' as GRN_ID, convert(varchar,TSPL_TRANSFER_RETURN.Document_Date,103 ) as PI_Date,TSPL_TRANSFER_ORDER_HEAD.waybill_No as Way_BillNo,TSPL_TRANSFER_ORDER_HEAD.GR_No as [GRNO],'' as LR_NO," _
        & " TSPL_TRANSFER_ORDER_HEAD.Document_No  as Vendor_Invoice_No,convert(varchar,TSPL_TRANSFER_ORDER_HEAD.DOcument_Date,103) as Vendor_Invoice_Date, '' as vehicledesc,TSPL_TRANSFER_ORDER_HEAD.Vehicle_No as Vehicle_No,0  as Additional_Charge , Tspl_Location_Master.Location_Code as Customer_Code, Tspl_Location_Master.Location_Desc  as Customer_Name,Tspl_Location_Master.Add1 + ' ' + Tspl_Location_Master.Add2 + ' ' + Tspl_Location_Master.Add3 As Vendor_ADD,Tspl_State_Master.state_Code as [State Code],Tspl_State_Master.state_name as [State Name],Tspl_Location_Master.TIN_No as [TIN No]," _
        & " '' as [Parent Vendor No],'' as [Parent Vendor Code],'' as [Parent Vendor Name], " _
        & " tspl_transport_Master.Transport_id as [Transport Code],Tspl_Transport_Master.Transporter_Name as [Transporter Name],TSPL_TRANSFER_ORDER_DETAIL.Item_Code, TSPL_TRANSFER_ORDER_DETAIL.Item_Desc ,  -TSPL_TRANSFER_ORDER_DETAIL.In_Qty  as Qty ,TSPL_TRANSFER_ORDER_DETAIL.Unit_code " _
        & " as  Unit_code ,-coalesce(TSPL_TRANSFER_ORDER_DETAIL.Price,TSPL_TRANSFER_ORDER_DETAIL.Item_Cost)  as  Item_Cost ,   QC.FAT_Per as [FAT Per],   QC.SNF_Per as [SNF Per],cast((TSPL_TRANSFER_ORDER_DETAIL.In_Qty*Qc.SNF_Per*Stock_SU.Conversion_Factor)/(100*coalesce(StockKG.Conversion_Factor,1)) as numeric(18,3)) as [FAT KG],cast((TSPL_TRANSFER_ORDER_DETAIL.In_Qty*Qc.SNF_Per*Stock_SU.Conversion_Factor)/(100*coalesce(StockKG.Conversion_Factor,1)) as Numeric(18,3)) as [SNF KG],-TSPL_TRANSFER_ORDER_DETAIL.Amount AS Amount " _
        & " ,TSPL_TRANSFER_ORDER_DETAIL.Disc_Per as Disc_Per  ,TSPL_TRANSFER_ORDER_DETAIL.Disc_Amt as Disc_Amt , - TSPL_TRANSFER_ORDER_DETAIL.Amount as  Amt_Less_Discount,0 as [EMP],0 as [Incentive],0 as [IncentiveEMP]   " _
        & " " & strPivotForTransfer_In & "" & strPivotFortRANSFER_INPercentQuery & "  " & strPivotForAddChargeForZeroTransfer_In & ",case when coalesce(out.is_AgainstformF,0)=1 then 'F' else '' end as [Tax Type],  0 as Total_Tax_Amt ," _
        & " -TSPL_TRANSFER_ORDER_DETAIL.Amount as   Total_Amt,'' as [AP Document No],0 as  [AP Document Amt],0 as [AP Document Discount Amt],0 as [AP Amount Before Tax]," _
        & " TSPL_TRANSFER_ORDER_HEAD.Document_No as against_PoInvoice_No,'' as Against_PurchaseREturn_No,0 as [AP Total Tax],0 as [AP Total Add Charge],0 as [AP Landed Amt]," _
        & " '' as Against_MillkpurchaseInvoice_No,'' as Against_BulkMillkpurchaseInvoice_No,0 as  [AP Document Total],TSPL_TRANSFER_ORDER_DETAIL.MRP,0 as NonRecoverable_Tax,'' as Purchase_Tax_Invoice,'' as Def_DOC_Type,'' as PO,Null as PO_Date,'' as created_by,'' as Modify_By from TSPL_TRANSFER_ORDER_HEAD " _
        & " left outer  join TSPL_TRANSFER_ORDER_DETAIL on TSPL_TRANSFER_ORDER_HEAD.Document_No =TSPL_TRANSFER_ORDER_DETAIL.Document_NO  " _
        & "  left join TSPL_TRANSFER_ORDER_HEAD out on out.Document_No=TSPL_TRANSFER_ORDER_HEAD.TransferOutNo " _
        & "  left join TSPL_TRANSFER_RETURN on TSPL_TRANSFER_ORDER_HEAD.Document_No=TSPL_TRANSFER_RETURN.Transfer_No " _
        & " left join tspl_Location_master on tspl_Location_master.LOcation_Code=out.From_Location " _
        & " left join tspl_Location_master recv on recv.location_code=TSPL_TRANSFER_ORDER_Head.To_Location left join TSPL_TRANSPORT_MASTER on " _
        & " TSPL_TRANSPORT_MASTER.transport_Id=tspl_Transfer_Order_Head.Transport_Id  left outer join " & "(" & qryQC & ") as QC" & " on QC.Item_Code =" _
        & " TSPL_TRANSFER_ORDER_DETAIL.Item_Code  left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL ) as Stock_SU on " _
        & " TSPL_TRANSFER_ORDER_DETAIL.Item_Code=Stock_SU.Item_Code and TSPL_TRANSFER_ORDER_DETAIL.Unit_code=Stock_SU.UOM_Code  " _
        & " left join (select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='KG') as StockKG on " _
        & " TSPL_TRANSFER_ORDER_DETAIL.Item_Code=StockKG.Item_Code  " _
        & " left join TSPL_STATE_MASTER on tspl_State_Master.STATE_CODE=Tspl_Location_Master.State where TSPL_TRANSFER_ORDER_Head.Transfer_Type='I'  and isnull(TSPL_TRANSFER_RETURN.Document_No,'')<>'' and convert(date,TSPL_TRANSFER_RETURN.Document_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_TRANSFER_RETURN.Document_Date ,103) <= convert(date,('" + To_Date + "'),103) )t"

        ''------------



        strMCCMaterial += "  union all"
        strSDCommonQuery = " select Distinct 'Purchase Return' as Trans_Type,Tspl_PR_Head.ConvRate ,TSPL_PR_DETAIL.Line_No,Tspl_PR_Head.Status ,Tspl_PR_Head.Bill_To_Location, " & _
                        " Tspl_PR_Head.Vendor_Code as Customer_Code,Tspl_STate_Master.state_Code AS [State Code],tspl_State_Master.state_name as [State Name],'Purchase Return' as Invoice_Type,Tspl_PR_Head.PR_NO ,'' as SRN_Id, " & _
                        " convert(varchar,Tspl_PR_Head.PR_Date,103 ) as PR_Date,'' as Way_BillNo,Tspl_PR_Head.GRNO,'' as LR_NO,Tspl_PR_Head.Vendor_Invoice_No ,convert(varchar,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) as Vendor_Invoice_Date,'' as [Transporter],'' as [Transporter Name], Tspl_PR_detail.Item_Code , " & _
                        " case when transaction_type='R' then case when Document_Type='C' then  TSPL_PR_DETAIL.PR_Qty else -1* TSPL_PR_DETAIL.PR_Qty end else 0 end as Qty ,Tspl_PR_detail.Unit_code ,Tspl_PR_detail.Item_Cost , " & _
                        " case when Document_Type='C' then TSPL_PR_DETAIL.Amount else -1 * TSPL_PR_DETAIL.Amount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Amount ,Tspl_PR_detail.Disc_Per ,case when Document_Type='C' then TSPL_PR_DETAIL.Disc_Amt else -1 * TSPL_PR_DETAIL.Disc_AMt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Disc_Amt, " & _
                        " case when Document_Type='C' then TSPL_PR_DETAIL.Amt_Less_Discount else -1 * TSPL_PR_DETAIL.Amt_Less_Discount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) Amt_Less_Discount , " & _
                        " case when Document_Type='C' then Tspl_PR_detail.Total_Tax_Amt else -1 * Tspl_PR_detail.Total_Tax_Amt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Tax_Amt ,case when Document_Type='C' then Tspl_PR_detail.Item_Net_Amt else -1 * Tspl_PR_detail.Item_Net_Amt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Amt,Vehicle_No as vehicledesc,Vehicle_No as Vehicle_No,(case when Document_Type='C' then Tspl_PR_Detail.Total_ItemAdd_Charge else -1 * Tspl_PR_Detail.Total_ItemAdd_Charge end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Additional_Charge,0 as NonRecoverable_Tax, "
        strSDEndQry = ",TSPL_vendor_Invoice_Head.Document_No as [AP Document No], (case when Document_Type='C' then TSPL_vendor_Invoice_Head.Document_Total else -1 * TSPL_vendor_Invoice_Head.Document_Total end) *(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as  [AP Document Amt],(case when Document_Type='C' then TSPL_vendor_Invoice_Head.Discount_Amount else -1 * TSPL_vendor_Invoice_Head.Discount_Amount end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as [AP Document Discount Amt],(case when Document_Type='C' then TSPL_vendor_Invoice_Head.amount_less_Discount else -1 * TSPL_vendor_Invoice_Head.amount_less_Discount end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as [AP Amount Before Tax],TSPL_PR_DETAIL.PI_Id as against_PoInvoice_No,TSPL_vendor_Invoice_Head.Against_PurchaseREturn_No,(case when Document_Type='C' then TSPL_vendor_Invoice_Head.total_tax else -1 * TSPL_vendor_Invoice_Head.total_tax end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as [AP Total Tax],(case when Document_Type='C' then TSPL_vendor_Invoice_Head.total_Add_Charge else -1 * TSPL_vendor_Invoice_Head.total_Add_Charge end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as [AP Total Add Charge],(case when Document_Type='C' then TSPL_vendor_Invoice_Head.Total_landed_Amt else -1 * TSPL_vendor_Invoice_Head.Total_landed_Amt end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as [AP Landed Amt],TSPL_vendor_Invoice_Head.Against_MillkpurchaseInvoice_No,TSPL_vendor_Invoice_Head.Against_BulkMillkpurchaseInvoice_No,(case when Document_Type='C' then TSPL_vendor_Invoice_Head.Document_total else -1 * TSPL_vendor_Invoice_Head.Document_total end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as [AP Document Total] ,PI_Id,Tspl_PR_detail.MRP from Tspl_PR_detail " & _
                           " left outer join Tspl_PR_Head on Tspl_PR_Head.PR_NO =Tspl_PR_detail.PR_NO " & _
                           " left join TSPL_VEHICLE_MASTER on Vehicle_No=TSPL_VEHICLE_MASTER.Vehicle_Id left join TSPL_vendor_Invoice_Head on TSPL_vendor_Invoice_Head.Against_PurchaseReturn_No=Tspl_PR_Head.PR_NO  left join TSPL_vendor_master on   TSPL_vendor_master.Vendor_Code=tspl_PR_Head.Vendor_Code left join TSPL_STATE_MASTER on tspl_State_Master.STATE_CODE=TSPL_VENDOR_MASTER.State_Code "

        strMCCMaterial += " select 'Purchase Return'  as [Trans Type],max(Line_No)as Line_No ,max(ConvRate) as ConvRate,max(TSPL_LOCATION_MASTER .location_Code) as [Location Code],	 max(TSPL_LOCATION_MASTER.Add1) + ' ' + max(TSPL_LOCATION_MASTER.Add2) + ' ' + max(TSPL_LOCATION_MASTER.Add3) As [Location Address] ,max(TSPL_LOCATION_MASTER.State) as [Location State],final.Status  ,max(TSPL_LOCATION_MASTER .Location_Desc) as [Location Name] ,(final.Invoice_Type) as [Invoice Type],final.PR_NO as [Document No],'' as SRN_Id,'' as MRN_Id,'' as GRN_ID,final.PR_Date as [Document Date],final.Way_BillNo as [Way Bill No],Final.GRNO as [GR No],final.LR_NO as [LR No] ,max(VENDOR_INVOICE_no)as VENDOR_INVOICE_no,max(VENDOR_INVOICE_Date)as VENDOR_INVOICE_Date,vehicledesc,Vehicle_No,final.Additional_Charge,final.Customer_Code as [Vendor Code] ,max(TSPL_vendor_MASTER .vendor_Name) as [Vendor Name], max(TSPL_VENDOR_MASTER.Add1) + ' ' + max(TSPL_VENDOR_MASTER.Add2) + ' ' + max(TSPL_VENDOR_MASTER.Add3) As [Vendor Address],Max([State Code]) as [State Code],Max([State Name]) as [State Name],max(TSPL_vendor_MASTER .Tin_No) as [Vendor TIN No] ,max(TSPL_vendor_MASTER .Parent_vendor_Code) as [Parent Vendor No] ,max(Parent_Master.Vendor_Code) as [Parent Vendor Code],max(Parent_Master.Vendor_Name) as [Parent Vendor Name],Max(final.[Transporter]) as [Transporter],Max([Transporter Name]) as [Transporter Name], final.Item_Code as [Item Code],max(tspl_item_master.Item_Desc) as [Item Name],final.Qty as [Quantity],final.Unit_code as [UOM],final.Item_Cost as [Item Cost], QC.FAT_Per as [Fat Per],QC.SNF_Per as [SNF Per],0 as [Fat Kg],0 as [SNF KG],final.Amount,final.Disc_Per as [Discount Per],final.Disc_Amt as [Discount Amount],final.Amt_Less_Discount  as [Amount Less Discount],0 as [EMP],0 as [Incentive],0 as [IncentiveEMP]  " + strPivotForOuterQuery + " " + strPivotFoGrouprOuterQuery + " " + strPivotFoAddChargeZeroGrouprOuterQuery + ",max(_Type) as [Tax Type] ,(final.Total_Tax_Amt-coalesce(sum(final.NonRecoverable_Tax),0)) as [Total Tax Amount],final.Total_Amt as [Total Amount],Max([AP Document No]) as [AP Document No],Max(coalesce([AP Document Amt],0)) as [AP Document Amt],Max(coalesce([AP Document Discount Amt],0)) as [AP Document Discount Amt],Max(coalesce([AP Amount Before Tax],0)) as [AP Amount Before Tax],Max(against_PoInvoice_No) as against_PoInvoice_No,Max(Against_PurchaseREturn_No) as Against_PurchaseREturn_No,(Max(coalesce([AP Total Tax],0))-coalesce(sum(final.NonRecoverable_Tax),0)) as [AP Total Tax],max(coalesce([AP Total Add Charge],0)) as [AP Total Add Charge],(Max(coalesce([AP Landed Amt],0))-coalesce(-sum(final.NonRecoverable_Tax),0)) as [AP Landed Amt],Against_MillkpurchaseInvoice_No, Against_BulkMillkpurchaseInvoice_No,Max(coalesce([AP Document Total],0)) as [AP Document Total],max(MRP) as MRP,coalesce(sum(final.NonRecoverable_Tax),0) as NonRecoverable_Tax ,'' as Purchase_Tax_Invoice ,'' as Ref_Doc_Type,'' as [Ref Doc No],'' as [Ref Doc Date],'' as [Ref Created By],'' as [Ref Posted By] "
        strMCCMaterial += " from ("
        strTaxColumns = " TSPL_PR_DETAIL.TAX1 ,(case when Document_Type='C' then TSPL_PR_DETAIL.TAX1_Amt else -1 * TSPL_PR_DETAIL.TAX1_Amt end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as TAX1_Amt, TSPL_PR_DETAIL.TAX1_Rate,TSPL_PR_DETAIL.TAX1+'%' as Tax1Rate,'' as _Type,'N' as Tax_Recoverable"
        strAddChargeColumns = " , TSPL_Pr_Detail.ItemAdd_Charge_Code1 as Add_Charge_Code1 ,(case when Document_Type='C' then TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt1 else -1 * TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt1 end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Add_Charge_Amt1"
        '' query for no tax applied
        strMCCMaterial += "  select Trans_Type, Line_No,ConvRate ,Status  ,Bill_To_Location ,Customer_Code ,[State Code] ,[State Name] ,Invoice_Type ,PR_No  ,SRN_Id ,PR_Date ,Way_BillNo ,GRNo ,LR_NO ,Vendor_Invoice_No ,Vendor_Invoice_Date ,Transporter ,[Transporter Name] ,Item_Code ,Qty ,Unit_code ,Item_Cost ,Amount ,Disc_Per ,Disc_Amt ,Amt_Less_Discount ,Total_Tax_Amt ,Total_Amt ,vehicledesc ,Vehicle_No ,Additional_Charge ,NonRecoverable_Tax ,_Type,Tax_Recoverable,[AP Document No],[AP Document Amt],[AP Document Discount Amt],[AP Amount Before Tax],Against_POInvoice_No,Against_PurchaseReturn_No ,[AP Total Tax],[AP Total Add Charge],[AP Landed Amt],Against_MillkPurchaseInvoice_No,Against_BulkMillkPurchaseInvoice_No,[AP Document Total],PI_Id,MRP " & _
            " " + IIf(clsCommon.myLen(strPivotForInnerQuery) > 0, "," + strPivotForInnerQuery, "") + " " + IIf(clsCommon.myLen(strDoublePivotForInnerQuery) > 0, "," + strDoublePivotForInnerQuery, "") + " " + strPivotForAddChargeInnerQueryOuter + " " & _
            " from( select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " where 2=2 and (coalesce(TSPL_PR_DETAIL.tax1,'')='' and coalesce(TSPL_PR_DETAIL.tax2,'')='' " & _
                          " and coalesce(TSPL_PR_DETAIL.tax3,'')='' and coalesce(TSPL_PR_DETAIL.tax4,'')='' and " & _
                          " coalesce(TSPL_PR_DETAIL.tax5,'')='' and coalesce(TSPL_PR_DETAIL.tax6,'')='' and " & _
                          " coalesce(TSPL_PR_DETAIL.tax7,'')='' and coalesce(TSPL_PR_DETAIL.tax8,'')='' and " & _
                          " coalesce(TSPL_PR_DETAIL.tax9,'')='' and coalesce(TSPL_PR_DETAIL.tax10,'')='') and convert(date,Tspl_PR_Head.PR_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,Tspl_PR_Head.PR_Date ,103) <= convert(date,('" + To_Date + "'),103) )s  "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot columns exist then pivot query run(06/12/2016)
            strMCCMaterial += " pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot columns exist then pivot query run(06/12/2016)
            strMCCMaterial += " pivot (min(tax1_rate) for Tax1Rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot columns exist then pivot query run(06/12/2016)
            strMCCMaterial += " pivot (sum(Add_Charge_Amt1) for Add_Charge_Code1 in (" + strPivotForAddChargeInnerQuery + "))t"
        End If


        strMCCMaterial += "   union all"
        '' query for tax1 applied
        strSDCommonQuery = " select Distinct 'Purchase Return' as Trans_Type,Tspl_PR_Head.ConvRate ,TSPL_PR_DETAIL.Line_No,Tspl_PR_Head.Status ,Tspl_PR_Head.Bill_To_Location, " & _
                        " Tspl_PR_Head.Vendor_Code as Customer_Code,Tspl_STate_Master.state_Code AS [State Code],tspl_State_Master.state_name as [State Name],'Purchase Return' as Invoice_Type,Tspl_PR_Head.PR_NO ,'' as SRN_Id, " & _
                        " convert(varchar,Tspl_PR_Head.PR_Date,103 ) as PR_Date,'' as Way_BillNo,Tspl_PR_Head.GRNO,'' as LR_NO,Tspl_PR_Head.Vendor_Invoice_No ,convert(varchar,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) as Vendor_Invoice_Date,'' as [Transporter],'' as [Transporter Name], Tspl_PR_detail.Item_Code , " & _
                        " case when Document_Type='C' then  TSPL_PR_DETAIL.PR_Qty else -1* TSPL_PR_DETAIL.PR_Qty end as Qty ,Tspl_PR_detail.Unit_code ,Tspl_PR_detail.Item_Cost , " & _
                        " case when Document_Type='C' then TSPL_PR_DETAIL.Amount else -1 * TSPL_PR_DETAIL.Amount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Amount ,Tspl_PR_detail.Disc_Per ,case when Document_Type='C' then TSPL_PR_DETAIL.Disc_Amt else -1 * TSPL_PR_DETAIL.Disc_AMt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Disc_Amt, " & _
                        " case when Document_Type='C' then TSPL_PR_DETAIL.Amt_Less_Discount else -1 * TSPL_PR_DETAIL.Amt_Less_Discount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) Amt_Less_Discount , " & _
                        " case when Document_Type='C' then (Tspl_PR_detail.Total_Tax_Amt) else -1 * (Tspl_PR_detail.Total_Tax_Amt) end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Tax_Amt ,case when Document_Type='C' then Tspl_PR_detail.Item_Net_Amt else -1 * Tspl_PR_detail.Item_Net_Amt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Amt,Vehicle_No as vehicledesc,Vehicle_No as Vehicle_No,(case when Document_Type='C' then Tspl_PR_Detail.Total_ItemAdd_Charge else -1 * Tspl_PR_Detail.Total_ItemAdd_Charge end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Additional_Charge,-(case when Tax_Recoverable='N' then Tspl_PR_detail.TAX1_Amt else 0 end) as NonRecoverable_Tax, "
        strTaxColumns = " TSPL_PR_DETAIL.TAX1 ,(case when Document_Type='C' then TSPL_PR_DETAIL.TAX1_Amt else -1 * TSPL_PR_DETAIL.TAX1_Amt end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as TAX1_Amt,TSPL_PR_DETAIL.TAX1_Rate, TSPL_PR_DETAIL.TAX1+'%' as Tax1Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_Pr_Detail.ItemAdd_Charge_Code1 as Add_Charge_Code1 ,(case when Document_Type='C' then TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt1 else -1 * TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt1 end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Add_Charge_Amt1"
        ''richa add filter date
        strMCCMaterial += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PR_DETAIL.tax1 and ttr.tax_Rate=TSPL_PR_DETAIL.TAX1_Rate and ttr._type<>'OH'  left join tspl_tax_master tm on TSPL_PR_DETAIL.tax1=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_Pr_Head .Add_Charge_Code1 =AdCh .Code where 2=2  and (TSPL_PR_DETAIL.tax1<>'' or TSPL_PR_HEAD.Add_Charge_Code1<>'' ) and convert(date,Tspl_PR_Head.PR_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,Tspl_PR_Head.PR_Date ,103) <= convert(date,('" + To_Date + "'),103) )s "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot columns exist then pivot query run(06/12/2016)
            strMCCMaterial += " pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot columns exist then pivot query run(06/12/2016)
            strMCCMaterial += " pivot (min(tax1_rate) for Tax1Rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot columns exist then pivot query run(06/12/2016)
            strMCCMaterial += " pivot (sum(Add_Charge_Amt1) for Add_Charge_Code1 in (" + strPivotForAddChargeInnerQuery + "))t"
        End If


        strMCCMaterial += "   union all"

        strSDCommonQuery = " select Distinct 'Purchase Return' as Trans_Type,Tspl_PR_Head.ConvRate ,TSPL_PR_DETAIL.Line_No,Tspl_PR_Head.Status ,Tspl_PR_Head.Bill_To_Location, " & _
                        " Tspl_PR_Head.Vendor_Code as Customer_Code,Tspl_STate_Master.state_Code AS [State Code],tspl_State_Master.state_name as [State Name],'Purchase Return' as Invoice_Type,Tspl_PR_Head.PR_NO ,'' as SRN_Id, " & _
                        " convert(varchar,Tspl_PR_Head.PR_Date,103 ) as PR_Date,'' as Way_BillNo,Tspl_PR_Head.GRNO,'' as LR_NO,Tspl_PR_Head.Vendor_Invoice_No ,convert(varchar,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) as Vendor_Invoice_Date,'' as [Transporter],'' as [Transporter Name], Tspl_PR_detail.Item_Code , " & _
                        " case when Document_Type='C' then  TSPL_PR_DETAIL.PR_Qty else -1* TSPL_PR_DETAIL.PR_Qty end as Qty ,Tspl_PR_detail.Unit_code ,Tspl_PR_detail.Item_Cost , " & _
                        " case when Document_Type='C' then TSPL_PR_DETAIL.Amount else -1 * TSPL_PR_DETAIL.Amount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Amount ,Tspl_PR_detail.Disc_Per ,case when Document_Type='C' then TSPL_PR_DETAIL.Disc_Amt else -1 * TSPL_PR_DETAIL.Disc_AMt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Disc_Amt, " & _
                        " case when Document_Type='C' then TSPL_PR_DETAIL.Amt_Less_Discount else -1 * TSPL_PR_DETAIL.Amt_Less_Discount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) Amt_Less_Discount , " & _
                        " case when Document_Type='C' then (Tspl_PR_detail.Total_Tax_Amt) else -1 * (Tspl_PR_detail.Total_Tax_Amt) end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Tax_Amt ,case when Document_Type='C' then Tspl_PR_detail.Item_Net_Amt else -1 * Tspl_PR_detail.Item_Net_Amt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Amt,Vehicle_No as vehicledesc,Vehicle_No as Vehicle_No,(case when Document_Type='C' then Tspl_PR_Detail.Total_ItemAdd_Charge else -1 * Tspl_PR_Detail.Total_ItemAdd_Charge end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Additional_Charge,-(case when Tax_Recoverable='N' then Tspl_PR_detail.TAX2_Amt else 0 end) as NonRecoverable_Tax, "

        strTaxColumns = " TSPL_PR_DETAIL.TAX2 ,(case when Document_Type='C' then TSPL_PR_DETAIL.TAX2_Amt else -1 * TSPL_PR_DETAIL.TAX2_Amt end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as TAX2_Amt,TSPL_PR_DETAIL.TAX2_Rate, TSPL_PR_DETAIL.TAX2+'%' as Tax2Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_Pr_Detail.ItemAdd_Charge_Code2 as Add_Charge_Code2 ,(case when Document_Type='C' then TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt2 else -1 * TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt2 end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Add_Charge_Amt2"
        '' add filter date richa
        strMCCMaterial += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PR_DETAIL.tax2 and ttr.tax_Rate=TSPL_PR_DETAIL.TAX2_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PR_DETAIL.tax2=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_Pr_Head .Add_Charge_Code2 =AdCh .Code where 2=2 and (TSPL_PR_DETAIL.tax2<>'' or TSPL_PR_HEAD.Add_Charge_Code2<>'' ) and convert(date,Tspl_PR_Head.PR_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,Tspl_PR_Head.PR_Date ,103) <= convert(date,('" + To_Date + "'),103) )s "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot columns exist then pivot query run(06/12/2016)
            strMCCMaterial += " pivot (sum(tax2_amt) for tax2 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot columns exist then pivot query run(06/12/2016)
            strMCCMaterial += " pivot (min(tax2_rate) for tax2rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot columns exist then pivot query run(06/12/2016)
            strMCCMaterial += " pivot (sum(Add_Charge_Amt2) for Add_Charge_Code2 in (" + strPivotForAddChargeInnerQuery + "))t"
        End If


        strMCCMaterial += "  union all"

        strSDCommonQuery = " select Distinct 'Purchase Return' as Trans_Type,Tspl_PR_Head.ConvRate ,TSPL_PR_DETAIL.Line_No,Tspl_PR_Head.Status ,Tspl_PR_Head.Bill_To_Location, " & _
                        " Tspl_PR_Head.Vendor_Code as Customer_Code,Tspl_STate_Master.state_Code AS [State Code],tspl_State_Master.state_name as [State Name],'Purchase Return' as Invoice_Type,Tspl_PR_Head.PR_NO ,'' as SRN_Id, " & _
                        " convert(varchar,Tspl_PR_Head.PR_Date,103 ) as PR_Date,'' as Way_BillNo,Tspl_PR_Head.GRNO,'' as LR_NO,Tspl_PR_Head.Vendor_Invoice_No ,convert(varchar,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) as Vendor_Invoice_Date,'' as [Transporter],'' as [Transporter Name], Tspl_PR_detail.Item_Code , " & _
                        " case when Document_Type='C' then  TSPL_PR_DETAIL.PR_Qty else -1* TSPL_PR_DETAIL.PR_Qty end as Qty ,Tspl_PR_detail.Unit_code ,Tspl_PR_detail.Item_Cost , " & _
                        " case when Document_Type='C' then TSPL_PR_DETAIL.Amount else -1 * TSPL_PR_DETAIL.Amount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Amount ,Tspl_PR_detail.Disc_Per ,case when Document_Type='C' then TSPL_PR_DETAIL.Disc_Amt else -1 * TSPL_PR_DETAIL.Disc_AMt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Disc_Amt, " & _
                        " case when Document_Type='C' then TSPL_PR_DETAIL.Amt_Less_Discount else -1 * TSPL_PR_DETAIL.Amt_Less_Discount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) Amt_Less_Discount , " & _
                        " case when Document_Type='C' then (Tspl_PR_detail.Total_Tax_Amt) else -1 * (Tspl_PR_detail.Total_Tax_Amt) end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Tax_Amt ,case when Document_Type='C' then Tspl_PR_detail.Item_Net_Amt else -1 * Tspl_PR_detail.Item_Net_Amt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Amt,Vehicle_No as vehicledesc,Vehicle_No as Vehicle_No,(case when Document_Type='C' then Tspl_PR_Detail.Total_ItemAdd_Charge else -1 * Tspl_PR_Detail.Total_ItemAdd_Charge end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Additional_Charge,-(case when Tax_Recoverable='N' then Tspl_PR_detail.TAX3_Amt else 0 end) as NonRecoverable_Tax, "

        strTaxColumns = " TSPL_PR_DETAIL.TAX3 ,(case when Document_Type='C' then TSPL_PR_DETAIL.TAX3_Amt else -1 * TSPL_PR_DETAIL.TAX3_Amt end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as TAX3_Amt, TSPL_PR_DETAIL.TAX3_Rate, TSPL_PR_DETAIL.TAX3+'%' as Tax3Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_Pr_Detail.ItemAdd_Charge_Code3 as Add_Charge_Code3 ,(case when Document_Type='C' then TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt3 else -1 * TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt3 end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Add_Charge_Amt3"
        ''add filter date richa
        strMCCMaterial += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PR_DETAIL.tax3 and ttr.tax_Rate=TSPL_PR_DETAIL.TAX3_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PR_DETAIL.tax3=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_Pr_Head .Add_Charge_Code3 =AdCh .Code where 2=2 and (TSPL_PR_DETAIL.tax3<>'' or TSPL_PR_HEAD.Add_Charge_Code3<>'' ) and convert(date,Tspl_PR_Head.PR_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,Tspl_PR_Head.PR_Date ,103) <= convert(date,('" + To_Date + "'),103) )s "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot columns exist then pivot query run(06/12/2016)
            strMCCMaterial += " pivot (sum(tax3_amt) for tax3 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot columns exist then pivot query run(06/12/2016)
            strMCCMaterial += " pivot (min(tax3_rate) for tax3rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot columns exist then pivot query run(06/12/2016)
            strMCCMaterial += " pivot (sum(Add_Charge_Amt3) for Add_Charge_Code3 in (" + strPivotForAddChargeInnerQuery + "))t"
        End If


        strMCCMaterial += "   union all"

        strSDCommonQuery = " select Distinct 'Purchase Return' as Trans_Type,Tspl_PR_Head.ConvRate ,TSPL_PR_DETAIL.Line_No,Tspl_PR_Head.Status ,Tspl_PR_Head.Bill_To_Location, " & _
                        " Tspl_PR_Head.Vendor_Code as Customer_Code,Tspl_STate_Master.state_Code AS [State Code],tspl_State_Master.state_name as [State Name],'Purchase Return' as Invoice_Type,Tspl_PR_Head.PR_NO ,'' as SRN_Id, " & _
                        " convert(varchar,Tspl_PR_Head.PR_Date,103 ) as PR_Date,'' as Way_BillNo,Tspl_PR_Head.GRNO,'' as LR_NO,Tspl_PR_Head.Vendor_Invoice_No ,convert(varchar,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) as Vendor_Invoice_Date,'' as [Transporter],'' as [Transporter Name], Tspl_PR_detail.Item_Code , " & _
                        " case when Document_Type='C' then  TSPL_PR_DETAIL.PR_Qty else -1* TSPL_PR_DETAIL.PR_Qty end as Qty ,Tspl_PR_detail.Unit_code ,Tspl_PR_detail.Item_Cost , " & _
                        " case when Document_Type='C' then TSPL_PR_DETAIL.Amount else -1 * TSPL_PR_DETAIL.Amount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Amount ,Tspl_PR_detail.Disc_Per ,case when Document_Type='C' then TSPL_PR_DETAIL.Disc_Amt else -1 * TSPL_PR_DETAIL.Disc_AMt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Disc_Amt, " & _
                        " case when Document_Type='C' then TSPL_PR_DETAIL.Amt_Less_Discount else -1 * TSPL_PR_DETAIL.Amt_Less_Discount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) Amt_Less_Discount , " & _
                        " case when Document_Type='C' then (Tspl_PR_detail.Total_Tax_Amt) else -1 * (Tspl_PR_detail.Total_Tax_Amt) end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Tax_Amt ,case when Document_Type='C' then Tspl_PR_detail.Item_Net_Amt else -1 * Tspl_PR_detail.Item_Net_Amt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Amt,Vehicle_No as vehicledesc,Vehicle_No as Vehicle_No,(case when Document_Type='C' then Tspl_PR_Detail.Total_ItemAdd_Charge else -1 * Tspl_PR_Detail.Total_ItemAdd_Charge end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Additional_Charge,-(case when Tax_Recoverable='N' then Tspl_PR_detail.TAX4_Amt else 0 end) as NonRecoverable_Tax, "

        strTaxColumns = " TSPL_PR_DETAIL.TAX4 ,(case when Document_Type='C' then TSPL_PR_DETAIL.TAX4_Amt else -1 * TSPL_PR_DETAIL.TAX4_Amt end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as TAX4_Amt,TSPL_PR_DETAIL.TAX4_Rate, TSPL_PR_DETAIL.TAX4+'%' as Tax4Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_PR_Detail.ItemAdd_Charge_Code4 as Add_Charge_Code4 ,(case when Document_Type='C' then TSPL_PR_Detail.ItemAdd_Calc_Charge_Amt4 else -1 * TSPL_PR_Detail.ItemAdd_Calc_Charge_Amt4 end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Add_Charge_Amt4"
        ''add filter date richa
        strMCCMaterial += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PR_DETAIL.tax4 and ttr.tax_Rate=TSPL_PR_DETAIL.TAX4_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PR_DETAIL.tax4=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_Pr_Head .Add_Charge_Code4 =AdCh .Code where 2=2 and (TSPL_PR_DETAIL.tax4<>''  or TSPL_PR_HEAD.Add_Charge_Code4 <>'') and convert(date,Tspl_PR_Head.PR_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,Tspl_PR_Head.PR_Date ,103) <= convert(date,('" + To_Date + "'),103) )s "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot columns exist then pivot query run(06/12/2016)
            strMCCMaterial += " pivot (sum(tax4_amt) for tax4 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot columns exist then pivot query run(06/12/2016)
            strMCCMaterial += " pivot (min(tax4_rate) for tax4rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot columns exist then pivot query run(06/12/2016)
            strMCCMaterial += " pivot (sum(Add_Charge_Amt4) for Add_Charge_Code4 in (" + strPivotForAddChargeInnerQuery + "))t"
        End If


        strMCCMaterial += "  union all"

        strSDCommonQuery = " select Distinct 'Purchase Return' as Trans_Type,Tspl_PR_Head.ConvRate ,TSPL_PR_DETAIL.Line_No,Tspl_PR_Head.Status ,Tspl_PR_Head.Bill_To_Location, " & _
                        " Tspl_PR_Head.Vendor_Code as Customer_Code,Tspl_STate_Master.state_Code AS [State Code],tspl_State_Master.state_name as [State Name],'Purchase Return' as Invoice_Type,Tspl_PR_Head.PR_NO ,'' as SRN_Id, " & _
                        " convert(varchar,Tspl_PR_Head.PR_Date,103 ) as PR_Date,'' as Way_BillNo,Tspl_PR_Head.GRNO,'' as LR_NO,Tspl_PR_Head.Vendor_Invoice_No ,convert(varchar,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) as Vendor_Invoice_Date,'' as [Transporter],'' as [Transporter Name], Tspl_PR_detail.Item_Code , " & _
                        " case when Document_Type='C' then  TSPL_PR_DETAIL.PR_Qty else -1* TSPL_PR_DETAIL.PR_Qty end as Qty ,Tspl_PR_detail.Unit_code ,Tspl_PR_detail.Item_Cost , " & _
                        " case when Document_Type='C' then TSPL_PR_DETAIL.Amount else -1 * TSPL_PR_DETAIL.Amount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Amount ,Tspl_PR_detail.Disc_Per ,case when Document_Type='C' then TSPL_PR_DETAIL.Disc_Amt else -1 * TSPL_PR_DETAIL.Disc_AMt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Disc_Amt, " & _
                        " case when Document_Type='C' then TSPL_PR_DETAIL.Amt_Less_Discount else -1 * TSPL_PR_DETAIL.Amt_Less_Discount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) Amt_Less_Discount , " & _
                        " case when Document_Type='C' then (Tspl_PR_detail.Total_Tax_Amt) else -1 * (Tspl_PR_detail.Total_Tax_Amt) end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Tax_Amt ,case when Document_Type='C' then Tspl_PR_detail.Item_Net_Amt else -1 * Tspl_PR_detail.Item_Net_Amt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Amt,Vehicle_No as vehicledesc,Vehicle_No as Vehicle_No,(case when Document_Type='C' then Tspl_PR_Detail.Total_ItemAdd_Charge else -1 * Tspl_PR_Detail.Total_ItemAdd_Charge end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Additional_Charge,-(case when Tax_Recoverable='N' then Tspl_PR_detail.TAX5_Amt else 0 end) as NonRecoverable_Tax, "

        strTaxColumns = " TSPL_PR_DETAIL.TAX5 ,(case when Document_Type='C' then TSPL_PR_DETAIL.TAX5_Amt else -1 * TSPL_PR_DETAIL.TAX5_Amt end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as TAX5_Amt,TSPL_PR_DETAIL.TAX5_Rate, TSPL_PR_DETAIL.TAX5+'%' as Tax5Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_Pr_Detail.ItemAdd_Charge_Code5 as Add_Charge_Code5 ,(case when Document_Type='C' then TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt5 else -1 * TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt5 end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Add_Charge_Amt5"
        ''richa add date filter
        strMCCMaterial += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PR_DETAIL.tax5 and ttr.tax_Rate=TSPL_PR_DETAIL.TAX5_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PR_DETAIL.tax5=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_Pr_Head .Add_Charge_Code5 =AdCh .Code where 2=2 and (TSPL_PR_DETAIL.tax5<>'' or TSPL_PR_HEAD.Add_Charge_Code5<>'' ) and convert(date,Tspl_PR_Head.PR_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,Tspl_PR_Head.PR_Date ,103) <= convert(date,('" + To_Date + "'),103) )s "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot columns exist then pivot query run(06/12/2016)
            strMCCMaterial += " pivot (sum(tax5_amt) for tax5 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot columns exist then pivot query run(06/12/2016)
            strMCCMaterial += " pivot (min(tax5_rate) for tax5rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot columns exist then pivot query run(06/12/2016)
            strMCCMaterial += " pivot (sum(Add_Charge_Amt5) for Add_Charge_Code5 in (" + strPivotForAddChargeInnerQuery + "))t"
        End If


        strMCCMaterial += "  union all"

        strSDCommonQuery = " select Distinct 'Purchase Return' as Trans_Type,Tspl_PR_Head.ConvRate ,TSPL_PR_DETAIL.Line_No,Tspl_PR_Head.Status ,Tspl_PR_Head.Bill_To_Location, " & _
                        " Tspl_PR_Head.Vendor_Code as Customer_Code,Tspl_STate_Master.state_Code AS [State Code],tspl_State_Master.state_name as [State Name],'Purchase Return' as Invoice_Type,Tspl_PR_Head.PR_NO ,'' as SRN_Id, " & _
                        " convert(varchar,Tspl_PR_Head.PR_Date,103 ) as PR_Date,'' as Way_BillNo,Tspl_PR_Head.GRNO,'' as LR_NO,Tspl_PR_Head.Vendor_Invoice_No ,convert(varchar,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) as Vendor_Invoice_Date,'' as [Transporter],'' as [Transporter Name], Tspl_PR_detail.Item_Code , " & _
                        " case when Document_Type='C' then  TSPL_PR_DETAIL.PR_Qty else -1* TSPL_PR_DETAIL.PR_Qty end as Qty ,Tspl_PR_detail.Unit_code ,Tspl_PR_detail.Item_Cost , " & _
                        " case when Document_Type='C' then TSPL_PR_DETAIL.Amount else -1 * TSPL_PR_DETAIL.Amount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Amount ,Tspl_PR_detail.Disc_Per ,case when Document_Type='C' then TSPL_PR_DETAIL.Disc_Amt else -1 * TSPL_PR_DETAIL.Disc_AMt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Disc_Amt, " & _
                        " case when Document_Type='C' then TSPL_PR_DETAIL.Amt_Less_Discount else -1 * TSPL_PR_DETAIL.Amt_Less_Discount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) Amt_Less_Discount , " & _
                        " case when Document_Type='C' then (Tspl_PR_detail.Total_Tax_Amt) else -1 * (Tspl_PR_detail.Total_Tax_Amt) end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Tax_Amt ,case when Document_Type='C' then Tspl_PR_detail.Item_Net_Amt else -1 * Tspl_PR_detail.Item_Net_Amt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Amt,Vehicle_No as vehicledesc,Vehicle_No as Vehicle_No,(case when Document_Type='C' then Tspl_PR_Detail.Total_ItemAdd_Charge else -1 * Tspl_PR_Detail.Total_ItemAdd_Charge end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Additional_Charge,-(case when Tax_Recoverable='N' then Tspl_PR_detail.TAX6_Amt else 0 end) as NonRecoverable_Tax, "

        strTaxColumns = " TSPL_PR_DETAIL.TAX6 ,(case when Document_Type='C' then TSPL_PR_DETAIL.TAX6_Amt else -1 * TSPL_PR_DETAIL.TAX6_Amt end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as TAX6_Amt,TSPL_PR_DETAIL.TAX6_Rate, TSPL_PR_DETAIL.TAX6+'%' as Tax6Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_Pr_Detail.ItemAdd_Charge_Code6 as Add_Charge_Code6 ,(case when Document_Type='C' then TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt6 else -1 * TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt6 end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Add_Charge_Amt6"
        ''richa add date filter
        strMCCMaterial += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PR_DETAIL.tax6 and ttr.tax_Rate=TSPL_PR_DETAIL.TAX6_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PR_DETAIL.tax6=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_Pr_Head .Add_Charge_Code6 =AdCh .Code where 2=2 and (TSPL_PR_DETAIL.tax6<>'' or TSPL_PR_HEAD.Add_Charge_Code6<>'' ) and convert(date,Tspl_PR_Head.PR_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,Tspl_PR_Head.PR_Date ,103) <= convert(date,('" + To_Date + "'),103) )s "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot columns exist then pivot query run(06/12/2016)
            strMCCMaterial += " pivot (sum(tax6_amt) for tax6 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot columns exist then pivot query run(06/12/2016)
            strMCCMaterial += " pivot (min(tax6_rate) for tax6rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot columns exist then pivot query run(06/12/2016)
            strMCCMaterial += " pivot (sum(Add_Charge_Amt6) for Add_Charge_Code6 in (" + strPivotForAddChargeInnerQuery + "))t"
        End If


        strMCCMaterial += "  union all"

        strSDCommonQuery = " select Distinct 'Purchase Return' as Trans_Type,Tspl_PR_Head.ConvRate ,TSPL_PR_DETAIL.Line_No,Tspl_PR_Head.Status ,Tspl_PR_Head.Bill_To_Location, " & _
                        " Tspl_PR_Head.Vendor_Code as Customer_Code,Tspl_STate_Master.state_Code AS [State Code],tspl_State_Master.state_name as [State Name],'Purchase Return' as Invoice_Type,Tspl_PR_Head.PR_NO ,'' as SRN_Id, " & _
                        " convert(varchar,Tspl_PR_Head.PR_Date,103 ) as PR_Date,'' as Way_BillNo,Tspl_PR_Head.GRNO,'' as LR_NO,Tspl_PR_Head.Vendor_Invoice_No ,convert(varchar,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) as Vendor_Invoice_Date,'' as [Transporter],'' as [Transporter Name], Tspl_PR_detail.Item_Code , " & _
                        " case when Document_Type='C' then  TSPL_PR_DETAIL.PR_Qty else -1* TSPL_PR_DETAIL.PR_Qty end as Qty ,Tspl_PR_detail.Unit_code ,Tspl_PR_detail.Item_Cost , " & _
                        " case when Document_Type='C' then TSPL_PR_DETAIL.Amount else -1 * TSPL_PR_DETAIL.Amount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Amount ,Tspl_PR_detail.Disc_Per ,case when Document_Type='C' then TSPL_PR_DETAIL.Disc_Amt else -1 * TSPL_PR_DETAIL.Disc_AMt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Disc_Amt, " & _
                        " case when Document_Type='C' then TSPL_PR_DETAIL.Amt_Less_Discount else -1 * TSPL_PR_DETAIL.Amt_Less_Discount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) Amt_Less_Discount , " & _
                        " case when Document_Type='C' then (Tspl_PR_detail.Total_Tax_Amt) else -1 * (Tspl_PR_detail.Total_Tax_Amt) end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Tax_Amt ,case when Document_Type='C' then Tspl_PR_detail.Item_Net_Amt else -1 * Tspl_PR_detail.Item_Net_Amt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Amt,Vehicle_No as vehicledesc,Vehicle_No as Vehicle_No,(case when Document_Type='C' then Tspl_PR_Detail.Total_ItemAdd_Charge else -1 * Tspl_PR_Detail.Total_ItemAdd_Charge end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Additional_Charge,-(case when Tax_Recoverable='N' then Tspl_PR_detail.TAX7_Amt else 0 end) as NonRecoverable_Tax, "

        strTaxColumns = " TSPL_PR_DETAIL.TAX7 ,(case when Document_Type='C' then TSPL_PR_DETAIL.TAX7_Amt else -1 * TSPL_PR_DETAIL.TAX7_Amt end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as TAX7_Amt,TSPL_PR_DETAIL.TAX7_Rate, TSPL_PR_DETAIL.TAX7+'%' as Tax7Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_Pr_Detail.ItemAdd_Charge_Code7 as Add_Charge_Code7 ,(case when Document_Type='C' then TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt7 else -1 * TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt7 end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Add_Charge_Amt7"
        ''richa add date filter
        strMCCMaterial += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PR_DETAIL.tax7 and ttr.tax_Rate=TSPL_PR_DETAIL.TAX7_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PR_DETAIL.tax7=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_Pr_Head .Add_Charge_Code7 =AdCh .Code  where 2=2 and (TSPL_PR_DETAIL.tax7<>''or TSPL_PR_HEAD.Add_Charge_Code7 <>'') and convert(date,Tspl_PR_Head.PR_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,Tspl_PR_Head.PR_Date ,103) <= convert(date,('" + To_Date + "'),103) )s "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot columns exist then pivot query run(06/12/2016)
            strMCCMaterial += " pivot (sum(tax7_amt) for tax7 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot columns exist then pivot query run(06/12/2016)
            strMCCMaterial += " pivot (min(tax7_rate) for tax7rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot columns exist then pivot query run(06/12/2016)
            strMCCMaterial += " pivot (sum(Add_Charge_Amt7) for Add_Charge_Code7 in (" + strPivotForAddChargeInnerQuery + "))t"
        End If


        strMCCMaterial += "  union all"

        strSDCommonQuery = " select Distinct 'Purchase Return' as Trans_Type,Tspl_PR_Head.ConvRate ,TSPL_PR_DETAIL.Line_No,Tspl_PR_Head.Status ,Tspl_PR_Head.Bill_To_Location, " & _
                        " Tspl_PR_Head.Vendor_Code as Customer_Code,Tspl_STate_Master.state_Code AS [State Code],tspl_State_Master.state_name as [State Name],'Purchase Return' as Invoice_Type,Tspl_PR_Head.PR_NO ,'' as SRN_Id, " & _
                        " convert(varchar,Tspl_PR_Head.PR_Date,103 ) as PR_Date,'' as Way_BillNo,Tspl_PR_Head.GRNO,'' as LR_NO,Tspl_PR_Head.Vendor_Invoice_No ,convert(varchar,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) as Vendor_Invoice_Date,'' as [Transporter],'' as [Transporter Name], Tspl_PR_detail.Item_Code , " & _
                        " case when Document_Type='C' then  TSPL_PR_DETAIL.PR_Qty else -1* TSPL_PR_DETAIL.PR_Qty end as Qty ,Tspl_PR_detail.Unit_code ,Tspl_PR_detail.Item_Cost , " & _
                        " case when Document_Type='C' then TSPL_PR_DETAIL.Amount else -1 * TSPL_PR_DETAIL.Amount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Amount ,Tspl_PR_detail.Disc_Per ,case when Document_Type='C' then TSPL_PR_DETAIL.Disc_Amt else -1 * TSPL_PR_DETAIL.Disc_AMt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Disc_Amt, " & _
                        " case when Document_Type='C' then TSPL_PR_DETAIL.Amt_Less_Discount else -1 * TSPL_PR_DETAIL.Amt_Less_Discount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) Amt_Less_Discount , " & _
                        " case when Document_Type='C' then (Tspl_PR_detail.Total_Tax_Amt) else -1 * (Tspl_PR_detail.Total_Tax_Amt) end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Tax_Amt ,case when Document_Type='C' then Tspl_PR_detail.Item_Net_Amt else -1 * Tspl_PR_detail.Item_Net_Amt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Amt,Vehicle_No as vehicledesc,Vehicle_No as Vehicle_No,(case when Document_Type='C' then Tspl_PR_Detail.Total_ItemAdd_Charge else -1 * Tspl_PR_Detail.Total_ItemAdd_Charge end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Additional_Charge,-(case when Tax_Recoverable='N' then Tspl_PR_detail.TAX8_Amt else 0 end) as NonRecoverable_Tax, "

        strTaxColumns = " TSPL_PR_DETAIL.TAX8 ,(case when Document_Type='C' then TSPL_PR_DETAIL.TAX8_Amt else -1 * TSPL_PR_DETAIL.TAX8_Amt end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as TAX8_Amt,TSPL_PR_DETAIL.TAX8_Rate, TSPL_PR_DETAIL.TAX8+'%' as Tax8Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_PR_Detail.ItemAdd_Charge_Code8 as Add_Charge_Code8 ,(case when Document_Type='C' then TSPL_PR_Detail.ItemAdd_Calc_Charge_Amt8 else -1 * TSPL_PR_Detail.ItemAdd_Calc_Charge_Amt8 end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Add_Charge_Amt8"
        ''richa add date filter
        strMCCMaterial += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PR_DETAIL.tax8 and ttr.tax_Rate=TSPL_PR_DETAIL.TAX8_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PR_DETAIL.tax8=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_Pr_Head .Add_Charge_Code8 =AdCh .Code where 2=2 and (TSPL_PR_DETAIL.tax8<>''or TSPL_PR_HEAD.Add_Charge_Code8<>'' ) and convert(date,Tspl_PR_Head.PR_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,Tspl_PR_Head.PR_Date ,103) <= convert(date,('" + To_Date + "'),103) )s "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot columns exist then pivot query run(06/12/2016)
            strMCCMaterial += " pivot (sum(tax8_amt) for tax8 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot columns exist then pivot query run(06/12/2016)
            strMCCMaterial += " pivot (min(tax8_rate) for tax8rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot columns exist then pivot query run(06/12/2016)
            strMCCMaterial += " pivot (sum(Add_Charge_Amt8) for Add_Charge_Code8 in (" + strPivotForAddChargeInnerQuery + "))t"
        End If


        strMCCMaterial += "  union all"

        strSDCommonQuery = " select Distinct 'Purchase Return' as Trans_Type,Tspl_PR_Head.ConvRate ,TSPL_PR_DETAIL.Line_No,Tspl_PR_Head.Status ,Tspl_PR_Head.Bill_To_Location, " & _
                        " Tspl_PR_Head.Vendor_Code as Customer_Code,Tspl_STate_Master.state_Code AS [State Code],tspl_State_Master.state_name as [State Name],'Purchase Return' as Invoice_Type,Tspl_PR_Head.PR_NO ,'' as SRN_Id, " & _
                        " convert(varchar,Tspl_PR_Head.PR_Date,103 ) as PR_Date,'' as Way_BillNo,Tspl_PR_Head.GRNO,'' as LR_NO,Tspl_PR_Head.Vendor_Invoice_No ,convert(varchar,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) as Vendor_Invoice_Date,'' as [Transporter],'' as [Transporter Name], Tspl_PR_detail.Item_Code , " & _
                        " case when Document_Type='C' then  TSPL_PR_DETAIL.PR_Qty else -1* TSPL_PR_DETAIL.PR_Qty end as Qty ,Tspl_PR_detail.Unit_code ,Tspl_PR_detail.Item_Cost , " & _
                        " case when Document_Type='C' then TSPL_PR_DETAIL.Amount else -1 * TSPL_PR_DETAIL.Amount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Amount ,Tspl_PR_detail.Disc_Per ,case when Document_Type='C' then TSPL_PR_DETAIL.Disc_Amt else -1 * TSPL_PR_DETAIL.Disc_AMt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Disc_Amt, " & _
                        " case when Document_Type='C' then TSPL_PR_DETAIL.Amt_Less_Discount else -1 * TSPL_PR_DETAIL.Amt_Less_Discount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) Amt_Less_Discount , " & _
                        " case when Document_Type='C' then (Tspl_PR_detail.Total_Tax_Amt) else -1 * (Tspl_PR_detail.Total_Tax_Amt) end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Tax_Amt ,case when Document_Type='C' then Tspl_PR_detail.Item_Net_Amt else -1 * Tspl_PR_detail.Item_Net_Amt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Amt,Vehicle_No as vehicledesc,Vehicle_No as Vehicle_No,(case when Document_Type='C' then Tspl_PR_Detail.Total_ItemAdd_Charge else -1 * Tspl_PR_Detail.Total_ItemAdd_Charge end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Additional_Charge,-(case when Tax_Recoverable='N' then Tspl_PR_detail.TAX9_Amt else 0 end) as NonRecoverable_Tax, "

        strTaxColumns = " TSPL_PR_DETAIL.TAX9 ,(case when Document_Type='C' then TSPL_PR_DETAIL.TAX9_Amt else -1 * TSPL_PR_DETAIL.TAX9_Amt end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as TAX9_Amt,TSPL_PR_DETAIL.TAX9_Rate, TSPL_PR_DETAIL.TAX9+'%' as Tax9Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_Pr_Detail.ItemAdd_Charge_Code9 as Add_Charge_Code9 ,(case when Document_Type='C' then TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt9 else -1 * TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt9 end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Add_Charge_Amt9"
        ''richa add date filter
        strMCCMaterial += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & " left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PR_DETAIL.tax9 and ttr.tax_Rate=TSPL_PR_DETAIL.TAX9_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PR_DETAIL.tax9=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_Pr_Head .Add_Charge_Code9 =AdCh .Code where 2=2 and (TSPL_PR_DETAIL.tax9<>'' or TSPL_PR_HEAD.Add_Charge_Code9 <>'')  and convert(date,Tspl_PR_Head.PR_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,Tspl_PR_Head.PR_Date ,103) <= convert(date,('" + To_Date + "'),103) )s "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot columns exist then pivot query run(06/12/2016)
            strMCCMaterial += " pivot (sum(tax9_amt) for tax9 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot columns exist then pivot query run(06/12/2016)
            strMCCMaterial += " pivot (min(tax9_rate) for tax9rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot columns exist then pivot query run(06/12/2016)
            strMCCMaterial += " pivot (sum(Add_Charge_Amt9) for Add_Charge_Code9 in (" + strPivotForAddChargeInnerQuery + "))t"
        End If


        strMCCMaterial += "  union all"

        strSDCommonQuery = " select Distinct 'Purchase Return' as Trans_Type,Tspl_PR_Head.ConvRate ,TSPL_PR_DETAIL.Line_No,Tspl_PR_Head.Status ,Tspl_PR_Head.Bill_To_Location, " & _
                        " Tspl_PR_Head.Vendor_Code as Customer_Code,Tspl_STate_Master.state_Code AS [State Code],tspl_State_Master.state_name as [State Name],'Purchase Return' as Invoice_Type,Tspl_PR_Head.PR_NO ,'' as SRN_Id, " & _
                        " convert(varchar,Tspl_PR_Head.PR_Date,103 ) as PR_Date,'' as Way_BillNo,Tspl_PR_Head.GRNO,'' as LR_NO,Tspl_PR_Head.Vendor_Invoice_No ,convert(varchar,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) as Vendor_Invoice_Date,'' as [Transporter],'' as [Transporter Name], Tspl_PR_detail.Item_Code , " & _
                        " case when Document_Type='C' then  TSPL_PR_DETAIL.PR_Qty else -1* TSPL_PR_DETAIL.PR_Qty end as Qty ,Tspl_PR_detail.Unit_code ,Tspl_PR_detail.Item_Cost , " & _
                        " case when Document_Type='C' then TSPL_PR_DETAIL.Amount else -1 * TSPL_PR_DETAIL.Amount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Amount ,Tspl_PR_detail.Disc_Per ,case when Document_Type='C' then TSPL_PR_DETAIL.Disc_Amt else -1 * TSPL_PR_DETAIL.Disc_AMt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Disc_Amt, " & _
                        " case when Document_Type='C' then TSPL_PR_DETAIL.Amt_Less_Discount else -1 * TSPL_PR_DETAIL.Amt_Less_Discount end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) Amt_Less_Discount , " & _
                        " case when Document_Type='C' then (Tspl_PR_detail.Total_Tax_Amt) else -1 * (Tspl_PR_detail.Total_Tax_Amt) end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Tax_Amt ,case when Document_Type='C' then Tspl_PR_detail.Item_Net_Amt else -1 * Tspl_PR_detail.Item_Net_Amt end*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Total_Amt,Vehicle_No as vehicledesc,Vehicle_No as Vehicle_No,(case when Document_Type='C' then Tspl_PR_Detail.Total_ItemAdd_Charge else -1 * Tspl_PR_Detail.Total_ItemAdd_Charge end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Additional_Charge,-(case when Tax_Recoverable='N' then Tspl_PR_detail.TAX10_Amt else 0 end) as NonRecoverable_Tax, "

        strTaxColumns = " TSPL_PR_DETAIL.TAX10 ,(case when Document_Type='C' then TSPL_PR_DETAIL.TAX10_Amt else -1 * TSPL_PR_DETAIL.TAX10_Amt end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as TAX10_Amt,TSPL_PR_DETAIL.TAX10_Rate,TSPL_PR_DETAIL.TAX10+'%' as Tax10Rate,ttr._Type,tm.Tax_Recoverable"
        strAddChargeColumns = " , TSPL_Pr_Detail.ItemAdd_Charge_Code10 as Add_Charge_Code10 ,(case when Document_Type='C' then TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt10 else -1 * TSPL_Pr_Detail.ItemAdd_Calc_Charge_Amt10 end)*(case when coalesce(TSPL_PR_Head.convrate,0)<=0  then 1 else coalesce(TSPL_PR_Head.convrate,0) end) as Add_Charge_Amt10"
        ''richa add date filter
        strMCCMaterial += " select * from (" & strSDCommonQuery & strTaxColumns & strAddChargeColumns & strSDEndQry & "  left join TSPL_TAX_RATES ttr on ttr.tax_Code=TSPL_PR_DETAIL.tax10 and ttr.tax_Rate=TSPL_PR_DETAIL.TAX10_Rate and ttr._type<>'OH' left join tspl_tax_master tm on TSPL_PR_DETAIL.tax10=tm.Tax_Code left join TSPL_Additional_Charges AdCh on TSPL_Pr_Head .Add_Charge_Code10 =AdCh .Code where 2=2 and (TSPL_PR_DETAIL.tax10<>'' or TSPL_PR_HEAD.Add_Charge_Code10<>'' )  and convert(date,Tspl_PR_Head.PR_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,Tspl_PR_Head.PR_Date ,103) <= convert(date,('" + To_Date + "'),103) )s "

        If clsCommon.myLen(strPivotForInnerQuery) > 0 Then ''when pivot columns exist then pivot query run(06/12/2016)
            strMCCMaterial += " pivot (sum(tax10_amt) for tax10 in (" + strPivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strDoublePivotForInnerQuery) > 0 Then ''when pivot columns exist then pivot query run(06/12/2016)
            strMCCMaterial += " pivot (min(tax10_rate) for tax10rate in (" + strDoublePivotForInnerQuery + "))t "
        End If
        If clsCommon.myLen(strPivotForAddChargeInnerQuery) > 0 Then ''when pivot columns exist then pivot query run(06/12/2016)
            strMCCMaterial += " pivot (sum(Add_Charge_Amt10) for Add_Charge_Code10 in (" + strPivotForAddChargeInnerQuery + "))t"
        End If


        strMCCMaterial += " )final1)final"
        strMCCMaterial += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =final.Bill_To_Location "
        strMCCMaterial += " left outer join tspl_item_master on tspl_item_master.Item_Code =final.Item_Code "
        strMCCMaterial += " left outer join TSPL_vendor_MASTER on TSPL_vendor_MASTER .Vendor_Code =final.Customer_Code "
        strMCCMaterial += " LEFT OUTER JOIN TSPL_vendor_MASTER as Parent_Master ON Parent_Master.Vendor_Code=TSPL_vendor_MASTER.Parent_Vendor_Code "
        strMCCMaterial += " left outer join " & "(" & qryQC & ") as QC" & " on QC.Item_Code =final.Item_Code "
        strMCCMaterial += " group by  final.Trans_Type,final .Status  ,final.PR_NO ,final.Item_Code ,final.Bill_To_Location ,final.Customer_Code ,final.Qty ,final.Total_Tax_Amt ,final.Invoice_Type ,final.PR_Date,final.Way_BillNo,Final.GRNO,final.LR_NO ,final.Unit_code ,final.Item_Cost ,final.Amount ,final.Disc_Per ,final.Disc_Amt ,final.Amt_Less_Discount ,final.Total_Amt,QC.FAT_Per,QC.SNF_Per,vehicledesc,Vehicle_No,final.Additional_Charge ,final.Against_BulkMillkPurchaseInvoice_No ,final.Against_MillkPurchaseInvoice_No ,final.PI_Id  " ', " + strPivotFoGrouprOuterQuery + "

        strMCCMaterial += "  union all"

        'strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX9 ,0 as TAX9_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX9_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX9+'%' as tax9rate  "
        strMCCMaterial += " select * from (Select 'Milk Receipt' as Trans_Type,0 as Line_No,0 as ConvRate ,TSPL_LOCATION_MASTER .location_Code  as  Bill_To_Location, " _
         & " (TSPL_LOCATION_MASTER.Add1) + ' ' + (TSPL_LOCATION_MASTER.Add2) + ' ' + (TSPL_LOCATION_MASTER.Add3) As [Location Address],TSPL_LOCATION_MASTER.State as [Location State],TSPL_MILK_PURCHASE_INVOICE_Head.Posted as Status, " _
            & " tspl_mcc_Master.mcc_name,'Milk Receipt' as Invoice_Type,TSPL_MILK_PURCHASE_INVOICE_Head.DOC_CODE as PI_NO,'' as SRN_Id ,'' as MRN_Id,'' as GRN_ID,  convert(varchar,TSPL_MILK_PURCHASE_INVOICE_Head.DOC_DATE,103 ) as PI_Date,'' as Way_BillNo,'' as [GRNO],'' as LR_NO,'' as Vendor_Invoice_No,'' as Vendor_Invoice_Date," _
            & " TSPL_Primary_Vehicle_Master.Vehicle_Code as Vehicle_No, TSPL_Primary_Vehicle_Master.Description as vehicledesc,0  as Additional_Charge ,  " _
            & " TSPL_MILK_SRN_HEAD.Vsp_CODE as Customer_Code,  tspl_vendor_Master.Vendor_Name as Customer_Name,(TSPL_VENDOR_MASTER.Add1) + ' ' + (TSPL_VENDOR_MASTER.Add2) + ' ' + (TSPL_VENDOR_MASTER.Add3) As [Vendor Address],tspl_state_Master.state_Code as [State Code],tspl_State_Master.state_Name as [State Name],tspl_vendor_Master.Tin_No as [TIN No],Parent_V.vendor_Code as [Parent Vendor No],Parent_V.vendor_Code as [Parent Vendor Code],Parent_V.vendor_Name as [Parent Vendor Name],pm.vendor_Code as [Transporter],pm.Vendor_Name as [Transporter Name]," _
            & " TSPL_MILK_SRN_detail.Item_Code, tspl_Item_Master.Item_Desc ,  MILK_WEIGHT  as Qty ,TSPL_MILK_SRN_detail.UOM_Code as  Unit_code ,TSPL_MILK_SRN_DETAIL.RATE as  Item_Cost " _
            & " ,  TSPL_MILK_SRN_DETAIL.FAT_Per as [FAT Per],  TSPL_MILK_SRN_DETAIL.SNF_PER as [SNF Per],TSPL_MILK_SRN_DETAIL.FAT_KG as [FAT KG],TSPL_MILK_SRN_DETAIL.SNF_KG " _
            & " as [SNF KG],TSPL_MILK_SRN_DETAIL.Amount ,0 as Disc_Per  ,0 as Disc_Amt ,  TSPL_MILK_PURCHASE_INVOICE_DETAIL.NET_AMOUNT as  Amt_Less_Discount ," _
            & " round(coalesce(TSPL_MILK_PURCHASE_INVOICE_DETAIL.AMOUNT*TSPL_MILK_PURCHASE_INVOICE_DETAIL.PAYMENT_COMMISSION/100,0),2) as EMP," _
            & " round(coalesce(TSPL_MILK_PURCHASE_INVOICE_DETAIL.Incentive,0),2) as Incentive_Head,round(coalesce(IncentiveEMP,0),2) as IncentiveEMP " _
            & " " & strPivotForTransfer_In & "" & strPivotFortRANSFER_INPercentQuery & " " & strPivotForAddChargeForZeroTransfer_In & ",'' as [Tax Type],  0 as Total_Tax_Amt ,TSPL_MILK_PURCHASE_INVOICE_DETAIL.NET_AMOUNT as   Total_Amt," _
            & " TSPL_vendor_Invoice_Head.Document_No as [AP Document No],TSPL_vendor_Invoice_Head.Document_Total [AP Document Amt],TSPL_vendor_Invoice_Head.Discount_Amount " _
            & " as [AP Document Discount Amt],TSPL_vendor_Invoice_Head.amount_less_Discount as [AP Amount Before Tax],stuff((select ',' + isnull(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE  ,'')  FROM TSPL_MILK_PURCHASE_INVOICE_DETAIL WHERE TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  =TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE  for xml path ('')),1,1,'' )as against_PoInvoice_No," _
            & " TSPL_vendor_Invoice_Head.Against_PurchaseREturn_No,TSPL_vendor_Invoice_Head.total_tax as [AP Total Tax],TSPL_vendor_Invoice_Head.total_Add_Charge as " _
            & " [AP Total Add Charge],TSPL_vendor_Invoice_Head.Total_landed_Amt as [AP Landed Amt],TSPL_vendor_Invoice_Head.Against_MillkpurchaseInvoice_No," _
            & " TSPL_vendor_Invoice_Head.Against_BulkMillkpurchaseInvoice_No,TSPL_vendor_Invoice_Head.Document_total as [AP Document Total],0 as MRP,0 as NonRecoverable_Tax, TSPL_MILK_PURCHASE_INVOICE_HEAD.Purchase_Tax_Invoice,'SRN' as Ref_Doc_Type ,TSPL_MILK_SRN_HEAD.DOC_CODE ,convert(varchar,TSPL_MILK_SRN_HEAD.DOC_DATE,103) as DOC_DATE ,TSPL_MILK_PRICE_MASTER.Created_By ,TSPL_MILK_PRICE_MASTER.Modified_By " _
            & " from TSPL_MILK_SRN_detail Left Outer Join TSPL_MILK_SRN_HEAD        On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_detail.DOC_CODE  " _
            & " Left Outer Join      TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE        = " _
            & " TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE Left Outer Join      TSPL_MCC_MASTER        On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_SRN_HEAD.MCC_CODE   " _
            & " Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code =        TSPL_MILK_SRN_HEAD.VLC_CODE Left Outer Join TSPL_VENDOR_MASTER  " _
            & " On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_SRN_HEAD.VSP_CODE      Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code =  " _
            & " TSPL_MILK_SRN_HEAD.ROUTE_CODE Left Outer Join      TSPL_Primary_Vehicle_Master On TSPL_Primary_Vehicle_Master.Vehicle_Code =    " _
            & " TSPL_MCC_ROUTE_MASTER.Vehicle_Code left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_Code=TSPL_MILK_SRN_detail.Item_Code " _
            & " Left join tspl_Vendor_Master Parent_v on Parent_v.vendor_Code=tspl_Vendor_Master.Parent_Vendor_Code" _
            & " Left join tspl_Vendor_Master Pm on pm.vendor_Code=TSPL_Primary_Vehicle_Master.Vendor_Code" _
            & " left outer join TSPL_FAT_SNF_UPLOADER_MASTER on TSPL_FAT_SNF_UPLOADER_MASTER.Code=TSPL_MILK_SRN_DETAIL.Price_Code and TSPL_FAT_SNF_UPLOADER_MASTER.FAT=TSPL_MILK_SRN_DETAIL.FAT_PER and TSPL_FAT_SNF_UPLOADER_MASTER.SNF=TSPL_MILK_SRN_DETAIL.SNF_PER  " _
            & " left outer join TSPL_MILK_PRICE_MASTER on TSPL_MILK_PRICE_MASTER.Price_Code=TSPL_FAT_SNF_UPLOADER_MASTER.Price_Code " _
            & " left join tspl_Location_master on tspl_Location_master.location_code=TSPL_MILK_SRN_HEAD.mcc_Code left join TSPL_vendor_Invoice_Head on  TSPL_vendor_Invoice_Head.Against_MillkpurchaseInvoice_No=TSPL_MILK_PURCHASE_INVOICE_Head.DOC_CODE  left join TSPL_STATE_MASTER on tspl_State_Master.STATE_CODE=TSPL_Mcc_MASTER.State_Code where coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.doc_Code,'')<>''  AND TSPL_vendor_Invoice_Head.DOCUMENT_TYPE='I' and convert(date,TSPL_MILK_PURCHASE_INVOICE_Head.DOC_DATE ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_MILK_PURCHASE_INVOICE_Head.DOC_DATE ,103) <= convert(date,('" + To_Date + "'),103))t"
        'strMCCMaterial += "  union all"
        strMCCMaterial += "  union all"

        'strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX9 ,0 as TAX9_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX9_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX9+'%' as tax9rate  "
        strMCCMaterial += "  select * from (Select 'Bulk Purchase' as Trans_Type,0 as Line_No,0 as ConvRate ,TSPL_LOCATION_MASTER .location_Code  as  Bill_To_Location, (TSPL_LOCATION_MASTER.Add1) + ' ' + (TSPL_LOCATION_MASTER.Add2) + ' ' + (TSPL_LOCATION_MASTER.Add3) As [Location Address],TSPL_LOCATION_MASTER.State as [Location State],tspl_Bulk_milk_purchase_Invoice_head.isPosted " _
            & " as Status, TSPL_LOCATION_MASTER.Location_Desc as [Location Desc],'Bulk Purchase' as Invoice_Type,tspl_Bulk_milk_purchase_Invoice_head.DOC_NO as PI_NO,'' as SRN_Id ,'' as MRN_Id,'' as GRN_ID, " _
            & " convert(varchar,tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE,103 ) as PI_Date,'' as Way_BillNo,'' as [GRNO],'' as LR_NO,tspl_Bulk_milk_purchase_Invoice_head.Vendor_Invoice_No as Vendor_Invoice_No,convert(varchar,tspl_Bulk_milk_purchase_Invoice_head.Doc_Date,103) as Vendor_Invoice_Date,Case when coalesce(TSPL_Bulk_MILK_SRN.Tanker_No,'')='' then TSPL_Dispatch_BulkSale_Trade.tanker_No else coalesce(TSPL_Bulk_MILK_SRN.Tanker_No,'') end as Vehicle_No, Case when coalesce(TSPL_Bulk_MILK_SRN.Tanker_No,'')='' then TSPL_Dispatch_BulkSale_Trade.tanker_No else coalesce(TSPL_Bulk_MILK_SRN.Tanker_No,'') end  " _
            & " as vehicledesc,Case when row_Number()over(partition by tspl_Bulk_milk_purchase_Invoice_head.DOC_NO Order by tspl_Bulk_milk_purchase_Invoice_Detail.Item_Code)=1 then tspl_Bulk_milk_purchase_Invoice_head.RoundoffAMount else 0 end  as Additional_Charge ,  tspl_Bulk_milk_purchase_Invoice_head.vendor_code as Customer_Code, .tspl_Vendor_Master.Vendor_Name as Customer_Name,(TSPL_VENDOR_MASTER.Add1) + ' ' + (TSPL_VENDOR_MASTER.Add2) + ' ' + (TSPL_VENDOR_MASTER.Add3) As [Vendor Address],tspl_State_Master.state_COde as [State Code],Tspl_State_Master.state_Name as [State Name], tspl_vendor_Master.Tin_No as [TIN No],Parent_v.vendor_Code as " _
            & " [Parent Vendor No],Parent_v.vendor_Code as [Parent Vendor Code],Parent_v.vendor_Name as [Parent Vendor Name],'' as Tanker_Transporter_Code,'' as [Transporter Name], tspl_Bulk_milk_purchase_Invoice_Detail.Item_Code, " _
            & " tspl_Bulk_milk_purchase_Invoice_Detail.Item_Desc ,  tspl_Bulk_milk_purchase_Invoice_Detail.Net_Weight  as Qty ,tspl_Bulk_milk_purchase_Invoice_Detail.UOM " _
            & " as  Unit_code ,Case when   TSPL_Bulk_MILK_SRN.Approved_Rate<=0 then   tspl_Bulk_milk_purchase_Invoice_Detail.NetRate else   TSPL_Bulk_MILK_SRN.Approved_Rate end as  Item_Cost ,  TSPL_Bulk_MILK_SRN.FAT_Per as [FAT Per],  TSPL_Bulk_MILK_SRN.SNF_PER as [SNF Per]," _
            & " TSPL_Bulk_MILK_SRN.FAT_KG as [FAT KG],TSPL_Bulk_MILK_SRN.SNF_KG as [SNF KG],tspl_Bulk_milk_purchase_Invoice_Detail.Amount ,0 as Disc_Per  ,0 as Disc_Amt , " _
            & " tspl_Bulk_milk_purchase_Invoice_Detail.Actual_Amount as  Amt_Less_Discount,0 as [EMP],0 as [Incentive],0 as [IncentiveEMP]   " & strPivotForTransfer_In & "" & strPivotFortRANSFER_INPercentQuery & " " & strPivotForAddChargeForZeroTransfer_In & ",'' as [Tax Type], " _
            & " 0 as Total_Tax_Amt ,tspl_Bulk_milk_purchase_Invoice_DETAIL.actual_amount as   Total_Amt,TSPL_vendor_Invoice_Head.Document_No as [AP Document No],TSPL_vendor_Invoice_Head.Document_Total [AP Document Amt],TSPL_vendor_Invoice_Head.Discount_Amount as [AP Document Discount Amt],TSPL_vendor_Invoice_Head.amount_less_Discount as [AP Amount Before Tax],stuff((select ',' + isnull(tspl_Bulk_milk_purchase_Invoice_DETAIL.SRN_NO ,'')  FROM tspl_Bulk_milk_purchase_Invoice_DETAIL WHERE tspl_Bulk_milk_purchase_Invoice_DETAIL.DOC_NO =tspl_Bulk_milk_purchase_Invoice_head.DOC_NO  for xml path ('')),1,1,'' )as against_PoInvoice_No,TSPL_vendor_Invoice_Head.Against_PurchaseREturn_No,TSPL_vendor_Invoice_Head.total_tax as [AP Total Tax],TSPL_vendor_Invoice_Head.total_Add_Charge as [AP Total Add Charge],TSPL_vendor_Invoice_Head.Total_landed_Amt as [AP Landed Amt],TSPL_vendor_Invoice_Head.Against_MillkpurchaseInvoice_No,TSPL_vendor_Invoice_Head.Against_BulkMillkpurchaseInvoice_No,TSPL_vendor_Invoice_Head.Document_total as [AP Document Total],0 as MRP,0 as NonRecoverable_Tax, tspl_Bulk_milk_purchase_Invoice_head.Purchase_Tax_Invoice,'SRN' as Ref_Doc_Type,TSPL_Bulk_MILK_SRN.SRN_NO ,convert(varchar,TSPL_Bulk_MILK_SRN.SRN_Date,103) as SRN_Date ,TSPL_Bulk_MILK_SRN.Created_By ,TSPL_Bulk_MILK_SRN.Modify_By from tspl_Bulk_milk_purchase_Invoice_head inner join" _
            & " tspl_Bulk_milk_purchase_Invoice_Detail on tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO=tspl_Bulk_milk_purchase_Invoice_Head.DOC_NO left join " _
            & " TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=tspl_Bulk_milk_purchase_Invoice_head.Loc_Code left join TSPL_VENDOR_MASTER on " _
            & " TSPL_VENDOR_MASTER.Vendor_Code=tspl_Bulk_milk_purchase_Invoice_head.VENDOR_CODE left join TSPL_Bulk_MILK_SRN on TSPL_Bulk_MILK_SRN.SRN_NO=" _
            & " tspl_Bulk_milk_purchase_Invoice_DETAIL.SRN_NO   Left join tspl_Vendor_Master Parent_v on Parent_v.vendor_Code=tspl_Vendor_Master.Parent_Vendor_Code  left join TSPL_Dispatch_BulkSale_Trade on TSPL_Dispatch_BulkSale_Trade.Against_SRN_No=TSPL_Bulk_MILK_SRN.SRN_NO  left join TSPL_vendor_Invoice_Head on  TSPL_vendor_Invoice_Head.Against_bulkmillkPurchaseInvoice_No=tspl_Bulk_milk_purchase_Invoice_head.DOC_NO  left join TSPL_STATE_MASTER on tspl_State_Master.STATE_CODE=TSPL_VENDOR_MASTER.State_Code where convert(date,tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE,103) <= convert(date,('" + To_Date + "'),103))t "

        'done by stuti against ticket No -BM00000007375 on 27/10/2016
        strMCCMaterial += " union all"

        strMCCMaterial += " select * from (Select 'Bulk Purchase Return' as Trans_Type,0 as Line_No,0 as ConvRate ,TSPL_LOCATION_MASTER .location_Code  as  Bill_To_Location, (TSPL_LOCATION_MASTER.Add1) + ' ' + (TSPL_LOCATION_MASTER.Add2) + ' ' + (TSPL_LOCATION_MASTER.Add3) As [Location Address],TSPL_LOCATION_MASTER.State as [Location State],TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.isPosted " _
            & " as Status, TSPL_LOCATION_MASTER.Location_Desc as [Location Desc],'Bulk Purchase Return' as Invoice_Type,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_No as PI_NO,'' as SRN_Id ,'' as MRN_Id,'' as GRN_ID, " _
            & " convert(varchar,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_Date,103 ) as PI_Date,'' as Way_BillNo,'' as [GRNO],'' as LR_NO,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Vendor_Invoice_No as Vendor_Invoice_No,convert(varchar,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_Date,103) as Vendor_Invoice_Date,Case when coalesce(TSPL_Bulk_MILK_SRN.Tanker_No,'')='' then TSPL_Dispatch_BulkSale_Trade.tanker_No else coalesce(TSPL_Bulk_MILK_SRN.Tanker_No,'') end as Vehicle_No, Case when coalesce(TSPL_Bulk_MILK_SRN.Tanker_No,'')='' then TSPL_Dispatch_BulkSale_Trade.tanker_No else coalesce(TSPL_Bulk_MILK_SRN.Tanker_No,'') end  " _
            & " as vehicledesc,Case when row_Number()over(partition by TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_No Order by TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Item_Code)=1 then (TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.RoundoffAMount*(-1)) else 0 end  as Additional_Charge ,  TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.vendor_code as Customer_Code, .tspl_Vendor_Master.Vendor_Name as Customer_Name,(TSPL_VENDOR_MASTER.Add1) + ' ' + (TSPL_VENDOR_MASTER.Add2) + ' ' + (TSPL_VENDOR_MASTER.Add3) As [Vendor Address],tspl_State_Master.state_COde as [State Code],Tspl_State_Master.state_Name as [State Name], tspl_vendor_Master.Tin_No as [TIN No],Parent_v.vendor_Code as " _
            & " [Parent Vendor No],Parent_v.vendor_Code as [Parent Vendor Code],Parent_v.vendor_Name as [Parent Vendor Name],'' as Tanker_Transporter_Code,'' as [Transporter Name], TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Item_Code, " _
            & " TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Item_Desc ,  (TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Net_Weight * (-1))  as Qty ,TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.UOM " _
            & " as  Unit_code ,Case when   TSPL_Bulk_MILK_SRN.Approved_Rate<=0 then   TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.NetRate else   TSPL_Bulk_MILK_SRN.Approved_Rate end as  Item_Cost ,  TSPL_Bulk_MILK_SRN.FAT_Per as [FAT Per],  TSPL_Bulk_MILK_SRN.SNF_PER as [SNF Per]," _
            & " TSPL_Bulk_MILK_SRN.fat_KG  as [FAT KG],TSPL_Bulk_MILK_SRN.SNF_KG  as [SNF KG],(TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Amount * (-1)) as Amount ,0 as Disc_Per  ,0 as Disc_Amt ," _
            & " (TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Actual_Amount * (-1)) as  Amt_Less_Discount,0 as [EMP],0 as [Incentive],0 as [IncentiveEMP]   " & strPivotForTransfer_In & "" & strPivotFortRANSFER_INPercentQuery & " " & strPivotForAddChargeForZeroTransfer_In & ",'' as [Tax Type], " _
            & " 0 as Total_Tax_Amt ,(TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.actual_amount * (-1)) as   Total_Amt,TSPL_vendor_Invoice_Head.Document_No as [AP Document No],TSPL_vendor_Invoice_Head.Document_Total [AP Document Amt],TSPL_vendor_Invoice_Head.Discount_Amount as [AP Document Discount Amt],TSPL_vendor_Invoice_Head.amount_less_Discount as [AP Amount Before Tax],stuff((select ',' + isnull(TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.SRN_NO ,'')  FROM TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL WHERE TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Pur_Return_No =TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_No  for xml path ('')),1,1,'' )as against_PoInvoice_No,TSPL_vendor_Invoice_Head.Against_PurchaseREturn_No,TSPL_vendor_Invoice_Head.total_tax as [AP Total Tax],TSPL_vendor_Invoice_Head.total_Add_Charge as [AP Total Add Charge],TSPL_vendor_Invoice_Head.Total_landed_Amt as [AP Landed Amt],TSPL_vendor_Invoice_Head.Against_MillkpurchaseInvoice_No,TSPL_vendor_Invoice_Head.Against_BulkMillkpurchaseInvoice_No,TSPL_vendor_Invoice_Head.Document_total as [AP Document Total],0 as MRP,0 as NonRecoverable_Tax,'' as Purchase_Tax_Invoice ,'SRN' as Ref_Doc_Type,TSPL_Bulk_MILK_SRN.SRN_NO ,convert(varchar,TSPL_Bulk_MILK_SRN.SRN_Date,103) as SRN_Date ,TSPL_Bulk_MILK_SRN.Created_By ,TSPL_Bulk_MILK_SRN.Modify_By from TSPL_BULK_MILK_PURCHASE_RETURN_HEAD inner join " _
            & " TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL on TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Pur_Return_No=TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_No left join " _
            & " TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Loc_Code left join TSPL_VENDOR_MASTER on " _
            & " TSPL_VENDOR_MASTER.Vendor_Code=TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.VENDOR_CODE left join TSPL_Bulk_MILK_SRN on TSPL_Bulk_MILK_SRN.SRN_NO= " _
            & " TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.SRN_NO   Left join tspl_Vendor_Master Parent_v on Parent_v.vendor_Code=tspl_Vendor_Master.Parent_Vendor_Code  left join TSPL_Dispatch_BulkSale_Trade on TSPL_Dispatch_BulkSale_Trade.Against_SRN_No=TSPL_Bulk_MILK_SRN.SRN_NO  left join TSPL_vendor_Invoice_Head on  TSPL_vendor_Invoice_Head.Against_bulkmillkPurchaseInvoice_No=TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_No  left join TSPL_STATE_MASTER on tspl_State_Master.STATE_CODE=TSPL_VENDOR_MASTER.State_Code where convert(date,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_Date ,103) >= convert(date,('" + From_Date + "'),103) and convert(date,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_Date,103) <= convert(date,('" + To_Date + "'),103))t"

        strMCCMaterial += ") xx"

        strMCCMaterial += " left outer join tspl_item_master Item on Item.Item_Code =xx.[Item Code] "
        '' transaction unit conversion
        strMCCMaterial += " inner join (" & qryTransStock & ") as  TransStock on xx.[Item Code]=TransStock.Item_Code and TransStock.UOM_Code=" & IIf(clsCommon.myLen(Unit_Code) <= 0, "xx.[UOM]", "'" & Unit_Code & "'") & " "
        ''end transaction unit conversion
        'strMCCMaterial += " left outer join TSPL_LOCATION_MASTER as TSPL_vendor_MASTER on TSPL_vendor_MASTER .Location_Code =final.To_location "
        strMCCMaterial += " left join (" & qryStock & ") as Stock_SU on xx.[Item Code]=Stock_SU.Item_Code and xx.[UOM]=Stock_SU.UOM_Code "
        strMCCMaterial += " left join (" & qryKG & ") as StockKG on xx.[Item Code]=StockKG.Item_Code  "
        strMCCMaterial += " left join (select Vendor_Code,Vendor_Group_Code,'' as Zone_Code,'' as Struct_Code,GST_Composition_scheme,GSTRegistered,GSTFinalNo,TSPL_VENDOR_MASTER.City_Code as VendorCityCode,TSPL_CITY_MASTER.City_Name as VendorCityName ,tspl_state_master.GST_STATE_Code as Vendor_GST_STATE_Code ,tspl_state_master.STATE_NAME as Veindor_STATE_Name from TSPL_vendor_MASTER left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_VENDOR_MASTER.City_Code left outer join tspl_state_master on tspl_state_master.state_code =TSPL_vendor_MASTER.state_code ) as Cust on xx.[vENDOR Code]=Cust.Vendor_Code  left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_For_GSTIN on TSPL_LOCATION_MASTER_For_GSTIN.Location_Code = xx.[Location Code] "
        strMCCMaterial += " left join (select Ven_Group_Code,Group_Desc from TSPL_Vendor_GROUP) as Cust_Group on Cust.Vendor_Group_Code=Cust_Group.ven_Group_Code "
        strMCCMaterial += " left join (select Zone_Code,Description from TSPL_ZONE_MASTER) as Zone on Cust.Zone_Code=Zone.Zone_Code "
        If clsCommon.myLen(strCategoryTable) > 0 Then
            strMCCMaterial += " left outer join (" + strCategoryTable + ") as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=xx.[Item Code]"
        End If
        strMCCMaterial += " left join tspl_item_master itmp on itmp.Item_Code=xx.[Item Code] left join TSPL_PURCHASE_ACCOUNTS tps on tps.Purchase_Class_Code=itmp.Purchase_Class_Code left join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code= " _
                   & " concat(SUBSTRING(tps.Inv_Control_Account,0,10), [Location Code])  where 2 = 2  and  convert(date,xx.[Document Date],103) >= convert(date,('" + From_Date + "'),103) and convert(date,xx.[Document Date],103) <= convert(date,('" + To_Date + "'),103) " ' + clsCommon.myCstr(IIf(clsCommon.myLen(txtUOM.Value) > 0, "and xx.[UOM]='" + txtUOM.Value + "' ", ""))
        QryLst.Add(strMCCMaterial)
        QryLst.Add(strPivotForFinalOuterQuery)
        QryLst.Add(strPivotForAddChargeFinalOuterSumQuery)
        Return QryLst
    End Function

    Private Sub txtState__My_Click(sender As Object, e As EventArgs) Handles txtState._My_Click
        Dim qry As String = " select STATE_CODE as Code,STATE_NAME as Name from TSPL_STATE_MASTER"
        txtState.arrValueMember = clsCommon.ShowMultipleSelectForm("StateMulSel", qry, "Code", "Name", txtState.arrValueMember, txtState.arrDispalyMember)
        FrmPendingRequisitionQty.SetDiplayMember(txtState, "STATE_NAME", "TSPL_sTATE_MASTER", "STATE_CODE")
    End Sub

    Private Sub chkdefaultUOM_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkStockingUOM.ToggleStateChanged
        If chkStockingUOM.Checked Then
            txtUOM.Enabled = False
        Else
            txtUOM.Enabled = True
        End If

    End Sub

    Private Sub btnCopyExcel_Click(sender As Object, e As EventArgs)

        If Gv1 Is Nothing OrElse Gv1.RowCount <= 0 Then
            MsgBox("I think the the Datatable is empty!!!")
            Exit Sub
        End If
        ExportCSV(Gv1, True)

        'Dim xlApp As New Excel.Application
        'Dim xlWorkBook As Excel.Workbook
        'Dim misValue As Object = System.Reflection.Missing.Value
        'xlWorkBook = xlApp.Workbooks.Add(misValue)
        'Dim grid As New DataGridView
        'grid.DataSource = Gv1.DataSource
        'Try
        '    xlApp.Visible = True
        '    Dim xlWorkSheet = xlWorkBook.ActiveSheet

        '    'Data transfer from grid to Excel. 
        '    With xlWorkSheet
        '        .Range("A1", misValue).EntireRow.Font.Bold = True

        '        'Set Clipboard Copy Mode
        '        'grid.SelectionMode = GridViewSelectionMode.FullRowSelect
        '        grid.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
        '        grid.SelectAll()

        '        'Get the content from Grid for Clipboard
        '        Dim str As String = TryCast(grid.GetClipboardContent().GetData(DataFormats.UnicodeText), String)

        '        'Set the content to Clipboard
        '        Clipboard.SetText(str, TextDataFormat.UnicodeText)

        '        'Identifiy and select the range of cells in excel to paste the clipboard data.
        '        .Range("A1:" & ConvertToLetter(grid.ColumnCount) & grid.RowCount, misValue).Select()

        '        'Paste the clipboard data
        '        .Paste()
        '        Clipboard.Clear()
        '    End With
        '    releaseObject(xlWorkSheet)
        '    grid.DataSource = Nothing
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'Finally
        '    releaseObject(xlWorkBook)
        '    releaseObject(xlApp)
        'End Try
    End Sub
    Public Sub ExportCSV(ByVal sender As RadGridView, Optional ByVal AddHeader As Boolean = False)
        '', ByVal FileName As String, 
        Try

            Dim sfd As SaveFileDialog = New SaveFileDialog()
            Dim filePath As String
            sfd.FileName = Me.Text
            sfd.Filter = "CSV Files (*.csv) |*.csv"
            If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                filePath = sfd.FileName
            Else
                Exit Sub
            End If
            '' applyExportTemplate
            ''By- Panch Raj on 04-05-2018 against tickt: KDI/02/05/18-000288 ---------
            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            Dim filecount As Integer = ExportCSVMultipleFile(Gv1, filePath, True)
            If filecount <= 1 Then
                clsCommon.MyMessageBoxShow(Me, "Data Exported successfully", Me.Text)
                Process.Start(filePath)
            Else
                clsCommon.MyMessageBoxShow("Data Exported in directory -" & System.IO.Path.GetDirectoryName(filePath) & "\" & System.IO.Path.GetFileName(filePath) & " in " & filecount & " files")
            End If
            'Dim OpenInExcel As Boolean = True
            'If Gv1.Rows.Count * Gv1.Columns.Count > 22000000 Then
            '    OpenInExcel = False
            'Else
            '    OpenInExcel = True
            'End If
            'clsCommon.ProgressBarShow()
            'IO.File.WriteAllLines(filePath, transportSql.ExportCSV(sender, AddHeader))
            'clsCommon.ProgressBarHide()

            'If OpenInExcel Then
            '    clsCommon.MyMessageBoxShow("Data Exported successfully")
            '    Process.Start(filePath)
            'Else
            '    clsCommon.MyMessageBoxShow("Data Exported successfully but can not open through excel, use other utility to open the file.")
            'End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        
    End Sub
    'Private Sub releaseObject(ByVal obj As Object)
    '    Try
    '        System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
    '        obj = Nothing
    '    Catch ex As Exception
    '        obj = Nothing
    '    Finally
    '        GC.Collect()
    '    End Try
    'End Sub

    'Private Function ConvertToLetter(ByVal num As Integer) As String
    '    num = num - 1
    '    If num < 0 Or num >= 27 * 26 Then
    '        ConvertToLetter = "-"
    '    Else
    '        If num < 26 Then
    '            ConvertToLetter = Chr(num + 65)
    '        Else
    '            ConvertToLetter = Chr(num \ 26 + 64) + Chr(num Mod 26 + 65)
    '        End If
    '    End If
    'End Function
    
    Private Sub QExpExcel_Click(sender As Object, e As EventArgs) Handles QExpExcel.Click
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptPurchaseRegisterReport & "'"))
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")
            If clsCommon.myLen(txtUOM.Value) > 0 Then
                arrHeader.Add("UOM : " + txtUOM.Value)
            End If
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
            'Dim sfd As SaveFileDialog = New SaveFileDialog()
            'Dim filePath As String
            'sfd.FileName = Me.Text
            'sfd.Filter = "Excel 2007 (*.xlsx) |*.xlsx;|Excel 97-2003 (*.xls)|*.xlsx;|CSV Files (*.csv) |*.csv"
            'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    filePath = sfd.FileName
            'Else
            '    Exit Sub
            'End If
            '' applyExportTemplate
            ''By- Panch Raj on 04-05-2018 against tickt: KDI/02/05/18-000288 ---------
            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
            'Dim fileCount As Integer = transportSql.exportdataChilRows(Gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader) 'frm.Text)
            'If fileCount > 1 Then
            '    common.clsCommon.MyMessageBoxShow("Data Exported in directory -" & System.IO.Path.GetDirectoryName(filePath) & "\" & System.IO.Path.GetFileName(filePath) & " in " & fileCount & " files.")
            'Else
            '    common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            '    Process.Start(filePath)
            'End If
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub QExpCSV_Click(sender As Object, e As EventArgs) Handles QExpCSV.Click
        Try
            If Gv1 Is Nothing OrElse Gv1.RowCount <= 0 Then
                MsgBox("Grid is empty!!!")
                Exit Sub
            End If
            ExportCSV(Gv1, True)            
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
       
    End Sub

    Private Sub BulkExcel_Click(sender As Object, e As EventArgs) Handles BulkExcel.Click
        Print(Exporter.Refresh, 2)
    End Sub

    Private Sub BulkCSV_Click(sender As Object, e As EventArgs) Handles BulkCSV.Click
        Print(Exporter.Refresh, 1)
    End Sub

    Private Sub PDF_Click(sender As Object, e As EventArgs) Handles PDF.Click
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptPurchaseRegisterReport & "'"))
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")
            If clsCommon.myLen(txtUOM.Value) > 0 Then
                arrHeader.Add("UOM : " + txtUOM.Value)
            End If
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
           
            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            clsCommon.MyExportToPDF("Purchase Register", Gv1, arrHeader, "Purchase Register", PageSetupReport_ID, objCommonVar.CurrentUserCode)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class


