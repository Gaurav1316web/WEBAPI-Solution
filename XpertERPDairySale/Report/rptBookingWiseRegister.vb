Imports common
Imports System.IO
Imports System.Net
Imports System.Net.Configuration
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Xml
'Imports Outlook = Microsoft.Office.Interop.Outlook
Imports System.Text.RegularExpressions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Public Class rptBookingWiseRegister
    Inherits FrmMainTranScreen

    Dim atchqry As String = ""
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dt As DataTable
    Dim AmountinLacs As Boolean = False
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
    '' new filters

    Dim dtCategory As DataTable
    Dim strPivotForFinalOuterQuery As String
    Dim MIS_Item_Group As String = ""
    Dim arrBack As List(Of String)
    Dim Document_No As String = ""
    Dim Document_No_Old As String = ""
    Dim FORMTYPE As String = Nothing
    Dim IsFormLoad As Boolean = False
#Region "User Defined Functions and Subroutines"
    Public Sub New(ByVal formid As String)
        InitializeComponent()
        FORMTYPE = formid
    End Sub
    Public Sub New()
        InitializeComponent()
    End Sub
#End Region
#Region "Functions"
    Private Sub SetUserMgmtNew()
        '=========Update By Preeti Gupta===============
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptsaleRegisterReport)
        'MyBase.SetUserMgmt(FORMTYPE)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If

        RadSplitButton1.Visible = MyBase.isExport
        'btnQuickExport.Visible = MyBase.isExport
        'radbtnBulkExp.Visible = MyBase.isQuickExportFlag
    End Sub
#End Region

    Sub LoadTypes()
        dt = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Rows.Add("Total Sale")
        dt.Rows.Add("Customer Group Wise")
        dt.Rows.Add("Item Wise")
        dt.Rows.Add("Customer Wise")
        dt.Rows.Add("Document Wise")
        dt.Rows.Add("Document Detail")
        ddlReportType.DataSource = dt
        ddlReportType.ValueMember = "Code"
        ddlReportType.DisplayMember = "Code"
    End Sub



    Private Sub txtUOM__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtUOM._MYValidating
        Dim qry As String = "select Unit_Code as Code,Unit_Desc as Description from TSPL_UNIT_MASTER"
        txtUOM.Value = clsCommon.ShowSelectForm("fndUOMMaster", qry, "Code", "", txtUOM.Value, "Code", isButtonClicked)
    End Sub


    Sub Print(ByVal IsPrint As Exporter)
        Try
            Dim Sql As String = ""
            Dim str_UOM As String = ""
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")

            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If
            If IsPrint = Exporter.Excel Then
                clsCommon.MyExportToExcelGrid(" Booking Wise Register :" + ddlReportType.SelectedValue, Gv1, arrHeader, Me.Text)
                Exit Sub
            ElseIf IsPrint = Exporter.PDF Then
                clsCommon.MyExportToPDF("Booking Wise Register" + ddlReportType.SelectedValue, Gv1, arrHeader, "Booking Wise Register", True)
                Exit Sub
            End If

            clsCommon.ProgressBarShow()
            ddlReportType.Enabled = False
            txtLocation.Enabled = False
            txtItem.Enabled = False
            txtCustomer.Enabled = False
            txtCustGroup.Enabled = False
            TxtRoute.Enabled = False
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()


            Unit_Code = txtUOM.Value
            clsCommon.ProgressBarUpdate("Loading Data.Please Wait...")
            Dim str As String = ""
            Dim dt As DataTable = Nothing

            RadPageViewPage2.Text = ddlReportType.SelectedValue

            Gv1.DataSource = Nothing
            Gv1.Columns.Clear()
            Gv1.Rows.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.EnableFiltering = True

            Gv1.Tag = ddlReportType.SelectedValue
            Dim strqry As String = ReturnQuery()
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(strqry)
            Gv1.DataSource = dt1


            SetGridFormationOFGV1()
            FindAndRestoreGridLayout(Me)
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
    Function ReturnQuery() As String
        Dim str As String = ""
        Dim strqry As String = String.Empty
        str = "Select document_no as [Document No],convert(varchar,Document_date,103) as [Document Date],Delivery_No as [Delivery No],InnrQry.Cust_code as [Customer Code],Customer_name as [Customer Name],InnrQry.Cust_Group_Code as [Customer Group Code],tspl_customer_group_master.Cust_Group_Desc  as [Customer Group Name],Vehicle_code as [Vehicle Code],InnrQry.location_code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Desc],TSPL_Item_MASTER.Item_code as [Item Code],TSPL_Item_MASTER.Item_desc as [Item Name],InnrQry.Unit_code as [Unit],Booking_Qty as Qty,Item_rate as Rate,Amount as [Document Amt],amount_with_tax as [Total Doc Amt],cast((amount_with_tax*TCSRate)/100 as decimal(18,2)) as [TCS Amt] from (select TSPL_BOOKING_MATSER.document_no,TSPL_BOOKING_MATSER.Document_date,TSPL_BOOKING_DETAIL.Cust_code,
TSPL_CUSTOMER_MASTER.Customer_name,TSPL_CUSTOMER_MASTER.Cust_Group_Code ,TSPL_BOOKING_MATSER.location_code,
Item_code,Booking_Qty ,Unit_code ,Delivery_No ,item_rate,vehicle_code,Booking_Qty*item_rate as Amount,amount_with_tax,amount_with_tax-(Booking_Qty*item_rate) as TaxAmt,
case when len( isnull (PAN,'')) > 0 OR LEN (ISNULL(Collectorate,'')) >0  then  (select Description from TSPL_FIXED_PARAMETER where Type = 'TCSRateforCustomerWithPanNo' and code = 'TCSRateforCustomerWithPanNo') else (select Description from TSPL_FIXED_PARAMETER where Type = 'TCSRateforCustomerWithoutPanNo' and code = 'TCSRateforCustomerWithoutPanNo')  end TCSRate
from TSPL_BOOKING_DETAIL
left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_code=TSPL_BOOKING_DETAIL.Cust_code
left outer join TSPL_BOOKING_MATSER on TSPL_BOOKING_MATSER.document_no=TSPL_BOOKING_DETAIL.document_no
where TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY = 'Others' and  isnull (TSPL_CUSTOMER_MASTER.IsTCSnotApplicable,0) = 0 
and  convert(date,TSPL_BOOKING_MATSER.Document_date,103)  >='" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and  convert(date,TSPL_BOOKING_MATSER.Document_date,103)  <='" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "' " + Environment.NewLine


        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            str += " and TSPL_BOOKING_MATSER.Location_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
        End If

        If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
            str += "  and TSPL_BOOKING_DETAIL.Cust_code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")"
        End If
        If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
            str += "  and TSPL_BOOKING_DETAIL.Item_code in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ")"
        End If
        If TxtRoute.arrValueMember IsNot Nothing AndAlso TxtRoute.arrValueMember.Count > 0 Then
            str += "  and TSPL_BOOKING_DETAIL.Route_No in (" + clsCommon.GetMulcallString(TxtRoute.arrValueMember) + ")"
        End If

        If txtCustGroup.arrValueMember IsNot Nothing AndAlso txtCustGroup.arrValueMember.Count > 0 Then
            str += "  and TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + clsCommon.GetMulcallString(txtCustGroup.arrValueMember) + ")"
        End If
        If btnPosted.IsChecked = True Then
            str += "  and isnull(TSPL_BOOKING_DETAIL.Booking_Status,0) =4 "
        ElseIf btnUnposted.IsChecked = True Then
            str += "  and isnull(TSPL_BOOKING_DETAIL.Booking_Status,0) <>4 "
        End If


        str += " )InnrQry
left outer join TSPL_Item_MASTER on TSPL_Item_MASTER.Item_code=InnrQry.Item_code
left outer join tspl_customer_group_master on tspl_customer_group_master.Cust_Group_Code=InnrQry.Cust_Group_Code
left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.lOCATION_code=InnrQry.lOCATION_code WHERE 1=1"

        If clsCommon.CompairString(ddlReportType.SelectedValue, "Document Wise") = CompairStringResult.Equal Then
            strqry = "select 
[Document No], [Document Date],max([Delivery No]) as [Delivery No], [Customer Code] as [Customer Code],[Customer Name] as [Customer Name],max([Vehicle Code]) as [Vehicle Code],[Location Code] as [Location Code],[Location Desc] as [Location Desc],sum([Document Amt]) as [Document Amt],sum([Total Doc Amt]) as [Total Doc Amt],sum([TCS Amt]) as [TCS Amt] from
(" & str & " )DocWise
group by DocWise.[Document No] ,DocWise.[Document Date],[Customer Code],[Customer Name],[Location Code],[Location Desc]
order by DocWise.[Document No] ,DocWise.[Document Date]"
        ElseIf clsCommon.CompairString(ddlReportType.SelectedValue, "Customer Wise") = CompairStringResult.Equal Then
            strqry = "select 
[Customer Code] as [Customer Code],[Customer Name] as [Customer Name],sum([Document Amt]) as [Document Amt],sum([Total Doc Amt]) as [Total Doc Amt],sum([TCS Amt]) as [TCS Amt] from
(" & str & " )CustWise
group by [Customer Code],[Customer Name]
order by [Customer Code],[Customer Name]"
        ElseIf clsCommon.CompairString(ddlReportType.SelectedValue, "Item Wise") = CompairStringResult.Equal Then
            strqry = "select 
[Item Code] as [Item Code], [Item Name] as [Item Name],sum([Document Amt]) as [Document Amt],sum([Total Doc Amt]) as [Total Doc Amt],sum([TCS Amt]) as [TCS Amt] from
(" & str & " )ItemWise
group by  [Item Code],[Item Name]
order by  [Item Code],[Item Name]"
        ElseIf clsCommon.CompairString(ddlReportType.SelectedValue, "Customer Group Wise") = CompairStringResult.Equal Then
            strqry = "select 
[Customer Group Code] as [Customer Group Code],[Customer Group Name] as [Customer Group Name],sum([Document Amt]) as [Document Amt],sum([Total Doc Amt]) as [Total Doc Amt],sum([TCS Amt]) as [TCS Amt] from
(" & str & " )CustGroupWise
group by [Customer Group Code], [Customer Group Name]
order by [Customer Group Code], [Customer Group Name]"
        ElseIf clsCommon.CompairString(ddlReportType.SelectedValue, "Total Sale") = CompairStringResult.Equal Then
            strqry = "select sum([Document Amt]) as [Document Amt],sum([Total Doc Amt]) as [Total Doc Amt],sum([TCS Amt]) as [TCS Amt] from
(" & str & " )TotalSale"
        Else
            strqry = str + " order by document_no,convert(varchar,Document_date,103)"

        End If

        Return strqry
    End Function



    Sub SetGridFormationOFGV1()
        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = True
            If ddlReportType.SelectedValue = "Document Detail" AndAlso Gv1.Columns(ii).Name.Contains("TCS%") = True Then
                Gv1.Columns(ii).FormatString = "{0:n3}"
            Else
                Gv1.Columns(ii).FormatString = "{0:n2}"
            End If

        Next


        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        For Each col As GridViewColumn In Gv1.Columns
            If col.Name.Contains("Amount") = True Or col.Name.Contains("Amt") = True Or col.Name.Contains("Total") = True Then
                Dim item As New GridViewSummaryItem(col.Name, "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item)
            ElseIf col.Name.Contains("Rate") = True Or col.Name.Contains("%") = True Then
                Dim item As New GridViewSummaryItem(col.Name, "{0:F2}", GridAggregateFunction.Avg)
                summaryRowItem.Add(item)
            End If
        Next
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom

        RadPageView1.SelectedPage = RadPageViewPage2
        Gv1.AllowAddNewRow = False
        Gv1.ShowGroupPanel = True
        Gv1.BestFitColumns()
    End Sub
    Sub Reset()
        txtUOM.Enabled = True
        Document_No = ""
        Document_No_Old = ""
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        txtUOM.Value = ""
        LoadTypes()
        ddlReportType.SelectedValue = "Total Sale"
        txtLocation.arrValueMember = Nothing
        txtItem.arrValueMember = Nothing
        txtCustomer.arrValueMember = Nothing
        txtCustGroup.arrValueMember = Nothing
        TxtRoute.arrValueMember = Nothing
        ddlReportType.Enabled = True
        txtLocation.Enabled = True
        txtItem.Enabled = True
        txtCustomer.Enabled = True
        txtCustGroup.Enabled = True
        TxtRoute.Enabled = True



        ddlReportType.SelectedIndex = 0
        btnPosted.IsChecked = True
        Gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        RadPageViewPage2.Text = ddlReportType.SelectedValue

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
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
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
        GetReportGridID()
        PageSetupReport_ID = clsERPFuncationality.GetReportID(MyBase.Form_ID, ddlReportType.Text)
        TemplateGridview = Gv1
        Print(Exporter.Refresh)
    End Sub
    Sub GetReportGridID()
        Dim VarID As String = ""
        If clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Total Sale") = CompairStringResult.Equal Then
            VarID += "_TS"
        ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Item Wise") = CompairStringResult.Equal Then
            VarID += "_IW"
        ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Customer Wise") = CompairStringResult.Equal Then
            VarID += "_CW"
        ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Document Detail") = CompairStringResult.Equal Then
            VarID += "_DD"
        End If
        Gv1.VarID = VarID
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        'Reset()
        txtUOM.Enabled = True
        Document_No = ""
        Document_No_Old = ""
        ddlReportType.Enabled = True
        txtLocation.Enabled = True
        txtItem.Enabled = True
        txtCustomer.Enabled = True
        txtCustGroup.Enabled = True
        TxtRoute.Enabled = True
        Gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        RadPageViewPage2.Text = ddlReportType.SelectedValue

    End Sub


    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub rptSaleRegisterDetail_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            Print(Exporter.Refresh)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub

    Private Sub rptSaleRegisterDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Visible = False
        arrBack = New List(Of String)
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New ")
        Reset()
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

            ddlReportType.SelectedValue = strType
            Print(True)
            Me.Visible = True
        End If


        IsFormLoad = True
    End Sub

    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        Dim qry As String = " select cust_code as [Code], Customer_Name as [Name] from tspl_customer_master "
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSel", qry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
    End Sub

    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Dim qry As String = " select Item_Code,Item_Desc from TSPL_ITEM_MASTER order by Item_Code "
        txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Item_Code", "Item_Code", txtItem.arrValueMember, txtItem.arrDispalyMember)
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim stateCond As String = ""

        Dim qry As String = " select Location_Code as Code,Location_Desc as [Name] from TSPL_LOCATION_MASTER where location_type IN ('Physical','Virtual') " & stateCond & " "
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        'FrmPendingRequisitionQty.SetDiplayMember(txtLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub


    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            If clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Total Sale") = CompairStringResult.Equal Then


            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Item Wise") = CompairStringResult.Equal AndAlso arrBack.Contains("Customer Group Wise") Then
                arrBack.Remove("Customer Group Wise")
                ddlReportType.SelectedValue = "Customer Group Wise"
                txtCustGroup.arrValueMember = arrCustGroup
                Print(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Customer Wise") = CompairStringResult.Equal AndAlso arrBack.Contains("Item Wise") Then
                arrBack.Remove("Item Wise")
                ddlReportType.SelectedValue = "Item Wise"
                txtItem.arrValueMember = arrItem
                Print(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Document Wise") = CompairStringResult.Equal AndAlso arrBack.Contains("Customer Wise") Then
                arrBack.Remove("Customer Wise")
                ddlReportType.SelectedValue = "Customer Wise"
                txtCustomer.arrValueMember = arrCustomer
                Print(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlReportType.SelectedValue), "Document Detail") = CompairStringResult.Equal AndAlso arrBack.Contains("Document Wise") Then
                arrBack.Remove("Document Wise")
                ddlReportType.SelectedValue = "Document Wise"
                Document_No = Document_No_Old
                Print(Exporter.Refresh)
            End If
            PageSetupReport_ID = clsERPFuncationality.GetReportID(MyBase.Form_ID, ddlReportType.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCustGroup__My_Click(sender As Object, e As EventArgs) Handles txtCustGroup._My_Click
        Dim qry As String = " select Cust_Group_Code as Code,Cust_Group_Desc as Name from TSPL_CUSTOMER_GROUP_MASTER"
        txtCustGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("CustGroupMulSel", qry, "Code", "Name", txtCustGroup.arrValueMember, txtCustGroup.arrDispalyMember)
    End Sub


    Sub BulkExport(ByVal FormatType As String)
        Try
            clsCommon.ProgressBarPercentShow()
            clsCommon.ProgressBarPercentUpdate(0, "Generating query for the report..")
            Dim qry As String = ReturnQuery()

            clsCommon.ProgressBarPercentUpdate(10, "Query generated..starting bulk export..")
            If ddlReportType.SelectedValue = "Total Sale" Then
                transportSql.BulkExport("Booking Wise Register", qry, "", FormatType)
                Exit Sub

            ElseIf ddlReportType.SelectedValue = "Customer Group Wise" Then
                transportSql.BulkExport("Booking Wise Register", qry, "order by [Customer Group Code], [Customer Group Name]", FormatType)
                Exit Sub
            ElseIf ddlReportType.SelectedValue = "Item Wise" Then
                transportSql.BulkExport("Booking Wise Register", qry, " order by [Item Code],[Item Name]", FormatType)
                Exit Sub
            ElseIf ddlReportType.SelectedValue = "Customer Wise" Then
                transportSql.BulkExport("Booking Wise Register", qry, "order by [Customer Code],[Customer Name]", FormatType)
                Exit Sub
            ElseIf ddlReportType.SelectedValue = "Document Wise" Then

                transportSql.BulkExport("Booking Wise Register", qry, "order by document_no,convert(varchar,Document_date,103)", FormatType)
                Exit Sub
            ElseIf ddlReportType.SelectedValue = "Document Detail" Then
                transportSql.BulkExport("Booking Wise Register", qry, "order by document_no,convert(varchar,Document_date,103)", FormatType)
                Exit Sub

            End If


            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, "Data exported successfully", Me.Text)
        Catch ex As Exception
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            clsCommon.ProgressBarPercentHide()
        End Try
    End Sub



    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptBookingWiseRegister & "'"))
            If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If


            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            If Gv1.Groups.Count > 0 Then
                clsCommon.MyExportToExcelGrid(Me.Text, Gv1, arrHeader, Me.Text, True)
            Else
                transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtRoute__My_Click(sender As Object, e As EventArgs) Handles TxtRoute._My_Click
        Dim qry As String = "Select TSPL_ROUTE_MASTER.Route_No AS Code,TSPL_ROUTE_MASTER.Route_Desc as Name from TSPL_ROUTE_MASTER  where 1=1 "
        TxtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("RouteMulSel", qry, "Code", "Name", TxtRoute.arrValueMember, TxtRoute.arrDispalyMember)
    End Sub

    Private Sub PDF_Click(sender As Object, e As EventArgs) Handles PDF.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptBookingWiseRegister & "'"))
            If clsCommon.myLen(txtUOM.Value) > 0 Then
                arrHeader.Add("UOM : " + txtUOM.Value)
            End If
            arrHeader.Add("Report Type : " + ddlReportType.Text)

            If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If


            If txtItem.arrDispalyMember IsNot Nothing AndAlso txtItem.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember))
            End If
            If txtCustGroup.arrDispalyMember IsNot Nothing AndAlso txtCustGroup.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Customer Group : " + clsCommon.GetMulcallStringWithComma(txtCustGroup.arrDispalyMember))
            End If
            If txtCustomer.arrDispalyMember IsNot Nothing AndAlso txtCustomer.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
            End If

            If TxtRoute.arrDispalyMember IsNot Nothing AndAlso TxtRoute.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Route : " + clsCommon.GetMulcallStringWithComma(TxtRoute.arrDispalyMember))
            End If

            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)

            clsCommon.MyExportToPDF("Booking Wise Register", Gv1, arrHeader, "Booking Wise Register", PageSetupReport_ID, objCommonVar.CurrentUserCode)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub BulkExcel_Click(sender As Object, e As EventArgs) Handles BulkExcel.Click
        BulkExport("xls")
    End Sub

    Private Sub BulkCSV_Click(sender As Object, e As EventArgs) Handles BulkCSV.Click
        BulkExport("csv")
    End Sub




End Class
