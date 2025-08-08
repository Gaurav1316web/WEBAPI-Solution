Imports System.IO
Imports common
Public Class rptCrateRegister
    Inherits FrmMainTranScreen
    Private Sub rptCrateRegister_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
    End Sub

    Private Sub MultLocation__My_Click(sender As Object, e As EventArgs) Handles MultLocation._My_Click
        Try
            'Dim qry As String = "select Cust_Code as Code ,Customer_Name as  Name from TSPL_CUSTOMER_MASTER "

            'If txtRoute.arrValueMember IsNot Nothing Then
            '    qry += "where Route_No in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ") "
            'End If
            Dim qry As String = " select CUST_CODE,Customer_Name from TSPL_CUSTOMER_MASTER"
            MultLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "CUST_CODE", "CUST_CODE", MultLocation.arrValueMember, MultLocation.arrDispalyMember)
            ' MultLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("LedgerCustomer", qry, "Code", "Name", MultLocation.arrValueMember, MultLocation.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub MultArea__My_Click(sender As Object, e As EventArgs) Handles MultArea._My_Click
        Dim qry As String = " select Route_No,Route_Desc from tspl_route_master "
        MultArea.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Route_No", "Route_No", MultArea.arrValueMember, MultArea.arrDispalyMember)
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Griddata(False)
    End Sub
    Public Sub Griddata(ByVal print As Boolean)
        Try

            MultLocation.Enabled = False
            MultArea.Enabled = False
            txtFromDate.Enabled = False
            txtToDate.Enabled = False
            Dim FromDate As String = clsCommon.myCstr(txtFromDate.Text)
            Dim ToDate As String = clsCommon.myCstr(txtToDate.Text)
            Dim qry As String = Nothing
            Dim whrcls As String = ""
            Dim whrclsClosing As String = ""
            Dim whrclss As String = ""

            'whrclss = "    Document_Date >= '" & clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(FromDate), "dd/MMM/yyyy hh:mm:ss tt") & "' AND " & "Document_Date <= '" & clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(ToDate), "dd/MMM/yyyy hh:mm:ss tt") & "' "
            whrclss = " convert(date, Document_Date, 108) >= convert(date, '" & clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(FromDate), "dd/MMM/yyyy hh:mm:ss tt") & "', 108) " &
          "AND convert(date, Document_Date, 108) <= convert(date, '" & clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(ToDate), "dd/MMM/yyyy hh:mm:ss tt") & "', 108)"

            'whrcls = "where 2 = 2 and convert(date,Document_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,Document_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103) "
            'whrclsClosing = "where 2 = 2 and convert(date,Document_Date,103)<convert(date,'" + txtFromDate.Value + "',103) "
            whrclsClosing = "  Document_Date <= '" & clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(FromDate), "dd/MMM/yyyy hh:mm:ss tt") & "'"



            If MultLocation.arrValueMember IsNot Nothing AndAlso MultLocation.arrValueMember.Count > 0 Then
                whrcls += "  and TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code in (" + clsCommon.GetMulcallString(MultLocation.arrValueMember) + ")  "

            End If

            If MultArea.arrValueMember IsNot Nothing AndAlso MultArea.arrValueMember.Count > 0 Then
                whrcls += " and tspl_route_master.Route_No in (" + clsCommon.GetMulcallString(MultArea.arrValueMember) + ")"
            End If

            '            qry = " Select max(YY.Document_No)Document_No,max(YY.Document_Date)Document_Date,(DistributorCode)DistributorCode,max(DistributorName)DistributorName,sum(Supply)Supply,sum(ReturnQty)ReturnQty,sum(CloseingBal) CloseingBal
            ',max(Code)Code,max(NAME)NAME,MAX(Comp_Code)Comp_Code,MAX(Comp_Name)Comp_Name,max(FromDate)FromDate,max(ToDate)ToDate
            'from(select max(xxx.Document_Date)Document_Date,max(xxx.Document_No)Document_No,(DistributorCode)DistributorCode,max(DistributorName)DistributorName,
            'sum(Supply)Supply,sum(ReturnQty)ReturnQty,0 as CloseingBal
            ',max(Code)Code,max(NAME)NAME,MAX(Comp_Code)Comp_Code,MAX(Comp_Name)Comp_Name,max(FromDate)FromDate,max(ToDate)ToDate
            'from(
            'SELECT TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No, Document_Date,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.type, Customer_Code as DistributorCode,tspl_customer_master.Customer_Name as DistributorName,
            'CrateQtyRecd as Supply ,OutQty as ReturnQty,TSPL_AREA_MASTER.Code,TSPL_AREA_MASTER.NAME,TSPL_COMPANY_MASTER.Comp_Code,TSPL_COMPANY_MASTER.Comp_Name
            ','" + FromDate + "' AS FromDate, '" + ToDate + " ' as ToDate
            'FROM TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE
            'LEFT OUTER JOIN TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No=TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Document_No
            'left outer join tspl_customer_master on tspl_customer_master.cust_code=TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Customer_Code
            'left outer join tspl_route_master on tspl_route_master.route_no=TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Route_code
            'left outer join TSPL_AREA_MASTER on TSPL_AREA_MASTER.Code=tspl_route_master.Area_Code
            'Left Join TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code1='" & objCommonVar.CurrComp_Code1 & "' 
            '" & whrcls & "
            ') as xxx
            'group by DistributorCode

            'union all
            'select max(Document_Date)Document_Date,max(Document_No)Document_No,(DistributorCode)DistributorCode,max(DistributorName)DistributorName,--max(LocationCode)LocationCode,max(AreaName)AreaName,

            '0 as Supply,0 as ReturnQty,(sum(Supply)-sum(ReturnQty)) as CloseingBal--,(route_no)route_no,max(route_desc)route_desc 
            ',max(Code)Code,max(NAME)NAME,MAX(Comp_Code)Comp_Code,MAX(Comp_Name)Comp_Name,max(FromDate)FromDate,max(ToDate)ToDate
            'from (
            'SELECT TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No, Document_Date,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.type, Customer_Code as DistributorCode,tspl_customer_master.Customer_Name as DistributorName,
            'CrateQtyRecd as Supply ,OutQty as ReturnQty,TSPL_AREA_MASTER.Code,TSPL_AREA_MASTER.NAME,TSPL_COMPANY_MASTER.Comp_Code,TSPL_COMPANY_MASTER.Comp_Name,'" + FromDate + "' AS FromDate, '" + ToDate + " ' as ToDate
            'FROM TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE
            'LEFT OUTER JOIN TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No=TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Document_No
            'left outer join tspl_customer_master on tspl_customer_master.cust_code=TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Customer_Code
            'left outer join tspl_route_master on tspl_route_master.route_no=TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Route_code
            'left outer join TSPL_AREA_MASTER on TSPL_AREA_MASTER.Code=tspl_route_master.Area_Code
            'Left Join TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code1='" & objCommonVar.CurrComp_Code1 & "' 
            '" & whrclsClosing & "
            ') as zxy
            'group by DistributorCode
            ')YY group by DistributorCode

            '"
            qry = "SELECT (DistributorCode)DistributorCode,MAX(DistributorName)DistributorName,(Route_No)Route_No,max(Route_Desc)Route_Desc,MAX(Comp_Code)Comp_Code,MAX(Comp_Name)Comp_Name, sum(closingBalnce) ClosingBalance,SUM(Supply)Supply,SUM(ReturnQty)ReturnQty
,    max(ToDate)ToDate,max(FromDate)FromDate
FROM (
select '" + FromDate + "' AS FromDate, '" + ToDate + " ' as ToDate ,Document_No,Document_Date,DistributorCode,DistributorName,Route_No,Route_Desc,Comp_Code,Comp_Name,TYPE,
	 CASE  WHEN " & whrclss & "
        THEN Supply ELSE 0 END AS Supply,
	CASE  WHEN " & whrclss & "
        THEN ReturnQty  ELSE 0 
    END AS ReturnQty,
	case when " & whrclsClosing & "
	then (Supply-ReturnQty) else 0 end as closingBalnce
from (
SELECT TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No, Document_Date,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.type, Customer_Code as DistributorCode,tspl_customer_master.Customer_Name as DistributorName,
CrateQtyRecd as Supply ,OutQty as ReturnQty,tspl_route_master.Route_No,tspl_route_master.Route_Desc,TSPL_COMPANY_MASTER.Comp_Code,TSPL_COMPANY_MASTER.Comp_Name
FROM TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE
LEFT OUTER JOIN TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No=TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Document_No
left outer join tspl_customer_master on tspl_customer_master.cust_code=TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Customer_Code
left outer join tspl_route_master on tspl_route_master.route_no=TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Route_code
Left Join TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code1='" & objCommonVar.CurrComp_Code1 & "' 
where 2 = 2   " & whrcls & "
)as new
) AS XXX where closingBalnce <> 0
GROUP BY XXX.DistributorCode,xxx.Route_No




"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing OrElse dt.Rows.Count > 0 Then
                If print = False Then
                    gv2.DataSource = Nothing
                    gv2.Rows.Clear()
                    gv2.Columns.Clear()
                    gv2.GroupDescriptors.Clear()
                    gv2.MasterTemplate.SummaryRowsBottom.Clear()
                    gv2.MasterView.Refresh()
                    gv2.DataSource = dt
                    For ii As Integer = 0 To gv2.Columns.Count - 1
                        gv2.Columns(ii).ReadOnly = True
                    Next
                    RadPageView1.SelectedPage = RadPageViewPage2
                    gv2.EnableFiltering = True
                    SetGridFormat1()
                    gv2.BestFitColumns()
                Else
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.KwalitySalesReport, dt, "rptCrateRegister", "")

                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

            'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            '    gv2.DataSource = Nothing
            '    gv2.Rows.Clear()
            '    gv2.Columns.Clear()
            '    gv2.GroupDescriptors.Clear()
            '    gv2.MasterTemplate.SummaryRowsBottom.Clear()
            '    gv2.MasterView.Refresh()
            '    gv2.DataSource = dt
            '    For ii As Integer = 0 To gv2.Columns.Count - 1
            '        gv2.Columns(ii).ReadOnly = True
            '    Next
            '    RadPageView1.SelectedPage = RadPageViewPage2
            '    gv2.EnableFiltering = True
            '    SetGridFormat1()
            '    gv2.BestFitColumns()
            'Else
            '    clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            '    Exit Sub
            'End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub SetGridFormat1()
        gv2.TableElement.TableHeaderHeight = 40
        gv2.MasterTemplate.ShowRowHeaderColumn = False
        Dim summaryRowItem As New GridViewSummaryRowItem()
        For ii As Integer = 0 To gv2.Columns.Count - 1
            gv2.Columns(ii).ReadOnly = True
            gv2.Columns(ii).IsVisible = True
            'gv1.Columns("Document_No").HeaderText = "Document No."

            'gv2.Columns("Document_No").IsVisible = False
            'gv2.Columns("Document_No").HeaderText = "Document No"
            'gv2.Columns("Document_Date").IsVisible = False
            'gv2.Columns("Document_Date").HeaderText = "Document Date"
            'gv2.Columns("DistributorCode").IsVisible = True
            gv2.Columns("DistributorCode").HeaderText = "Distributor Code"
            gv2.Columns("DistributorName").IsVisible = True
            gv2.Columns("DistributorName").HeaderText = "Distributor Name"
            gv2.Columns("Route_No").IsVisible = True
            gv2.Columns("Route_No").HeaderText = "Area Code"
            gv2.Columns("Route_Desc").IsVisible = True
            gv2.Columns("Route_Desc").HeaderText = "Area Name"
            gv2.Columns("Supply").IsVisible = True
            gv2.Columns("Supply").HeaderText = "Supply"
            gv2.Columns("ReturnQty").IsVisible = True
            gv2.Columns("ReturnQty").HeaderText = "Return Qty"
            gv2.Columns("ClosingBalance").IsVisible = True
            gv2.Columns("ClosingBalance").HeaderText = "Closing Balance"

            gv2.Columns("Comp_Code").IsVisible = False
            gv2.Columns("Comp_Code").HeaderText = "Comp_Code"
            gv2.Columns("Comp_Name").IsVisible = False
            gv2.Columns("Comp_Name").HeaderText = "Comp_Name"
            gv2.Columns("FromDate").IsVisible = False
            gv2.Columns("FromDate").HeaderText = "FromDate"
            gv2.Columns("ToDate").IsVisible = False
            gv2.Columns("ToDate").HeaderText = "ToDate"
        Next
        Dim summaryRowItemB As New GridViewSummaryRowItem()

        'Dim MilkTypeB As New GridViewSummaryItem("Payable_Amount", "{0:n0}", GridAggregateFunction.Sum)

        Dim Supply As New GridViewSummaryItem("Supply", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(Supply)
        Dim ReturnQty As New GridViewSummaryItem("ReturnQty", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(ReturnQty)
        Dim CloseingBal As New GridViewSummaryItem("CloseingBal", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(CloseingBal)
        gv2.MasterTemplate.SummaryRowsBottom.Add(summaryRowItemB)
        gv2.AutoSizeRows = True
        gv2.BestFitColumns()
        gv2.MasterTemplate.AutoExpandGroups = True
    End Sub
    Private Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click
        Export(EnumExportTo.Excel)
    End Sub
    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            If gv2.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Print Date (" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd-MMM-yyyy hh:mm:ss tt") + ")")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptCrateRegister & "'"))
                arrHeader.Add("Date Range : " & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "  To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))

                If MultLocation.arrValueMember IsNot Nothing AndAlso MultLocation.arrValueMember.Count > 0 Then
                    arrHeader.Add("Distrutor Code : " & MultLocation.arrDispalyMember(0))
                End If

                If MultArea.arrValueMember IsNot Nothing AndAlso MultArea.arrValueMember.Count > 0 Then
                    arrHeader.Add("Area : " & MultArea.arrDispalyMember(0))

                End If
                If exporter = EnumExportTo.Excel Then
                    'transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                    transportSql.exportdata(gv2, "", Me.Text, , arrHeader, False, False, True)
                End If
                'transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
            Else
                Throw New Exception("No data found to export.")

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Griddata(True)
    End Sub
    Private Sub Reset()
        'txtMultBmc.Enabled=True
        MultLocation.arrValueMember = Nothing
        MultArea.arrValueMember = Nothing
        txtFromDate.Enabled = True
        txtToDate.Enabled = True
        gv2.DataSource = Nothing
        gv2.Rows.Clear()
        gv2.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        btnGo.Enabled = True
        MultLocation.Enabled = True
        MultArea.Enabled = True
    End Sub
    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub
End Class