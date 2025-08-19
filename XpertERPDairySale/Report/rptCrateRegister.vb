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


            Dim whrclsCust As String = Nothing
            Dim whrclsRoute As String = Nothing

            If MultLocation.arrValueMember IsNot Nothing AndAlso MultLocation.arrValueMember.Count > 0 Then
                whrclsCust += "  and TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code in (" + clsCommon.GetMulcallString(MultLocation.arrValueMember) + ")  "

            End If

            If MultArea.arrValueMember IsNot Nothing AndAlso MultArea.arrValueMember.Count > 0 Then
                whrclsRoute += " and TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Route_code in (" + clsCommon.GetMulcallString(MultArea.arrValueMember) + ")"
            End If
            qry = " WITH full_data AS (
    SELECT 
        CAST(TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Invoice_Date AS DATE) AS Sale_Invoice_Date,
        TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Customer_Code,
        TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Route_code AS Route_No,
        MAX(TSPL_CUSTOMER_MASTER.Customer_Name) AS Customer_Name,
        MAX(TSPL_COMPANY_MASTER.Comp_Name) AS Comp_Name,
        MAX(TSPL_LOCATION_MASTER.Location_Desc) AS Location_Desc,
        MAX(TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Location_Code) AS Location_Code,
        MAX(TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Vehicle_Code) AS Vehicle_Id,
        MAX(TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.VehicleNo) AS Vehicle_Number,
        MAX(TSPL_ROUTE_MASTER.Route_Desc) AS Route_Desc,
        SUM(CASE WHEN TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.ShiftType = 'M' AND TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type = 'O' THEN TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.CrateQtyRecd ELSE 0 END) AS Morning_Supply,
        SUM(CASE WHEN TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.ShiftType = 'M' AND TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type = 'I' THEN TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.CrateQtyRecd ELSE 0 END) AS Morning_Return,
        SUM(CASE WHEN TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.ShiftType = 'E' AND TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type = 'O' THEN TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.CrateQtyRecd ELSE 0 END) AS Evening_Supply,
        SUM(CASE WHEN TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.ShiftType = 'E' AND TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type = 'I' THEN TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.CrateQtyRecd ELSE 0 END) AS Evening_Return
    FROM TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE
    LEFT JOIN TSPL_CRATE_RECEIVED_HEAD_FRESHSALE ON TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Document_No = TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No
    LEFT JOIN TSPL_ROUTE_MASTER ON TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Route_code = TSPL_ROUTE_MASTER.Route_No
    LEFT JOIN TSPL_LOCATION_MASTER ON TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Location_Code = TSPL_LOCATION_MASTER.Location_Code
    LEFT JOIN TSPL_COMPANY_MASTER ON TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code
    LEFT JOIN TSPL_CUSTOMER_MASTER ON TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Customer_Code = TSPL_CUSTOMER_MASTER.Cust_Code
where 2=2 " + whrclsCust + " " + whrclsRoute + " 

    GROUP BY CAST(TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Invoice_Date AS DATE), TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Customer_Code, TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Route_code
),

op_balance AS (
    SELECT 
        Customer_Code,
        Route_No,
        SUM((Morning_Supply + Evening_Supply) - (Morning_Return + Evening_Return)) AS OP
    FROM full_data
	where convert(date,Sale_Invoice_Date,103)>= convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "',103)
    GROUP BY Customer_Code, Route_No
),

current_data AS (
    SELECT 
        *,
        (Morning_Supply + Evening_Supply) AS Supply1,
        (Morning_Return + Evening_Return) AS Return1
    FROM full_data
   WHERE Sale_Invoice_Date BETWEEN '" + clsCommon.GetPrintDate(txtToDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtFromDate.Value) + "'

)

SELECT 
    '01/08/2025' AS FromDate,
    '01/08/2025' AS ToDate,
    ISNULL(current_data.Comp_Name, '') AS Comp_Name,
    ISNULL(current_data.Location_Code, '') AS Location_Code,
    ISNULL(current_data.Location_Desc, '') AS Location_Desc,
    ISNULL(current_data.Vehicle_Id, '') AS Vehicle_Id,
    ISNULL(current_data.Vehicle_Number, '') AS Vehicle_Number,
    ISNULL(current_data.Route_No, op_balance.Route_No) AS Route_No,
    ISNULL(current_data.Route_Desc, '') AS Route_Desc,
    ISNULL(current_data.Customer_Code, op_balance.Customer_Code) AS Customer_Code,
    ISNULL(current_data.Customer_Name, '') AS Customer_Name,
    ISNULL(current_data.Sale_Invoice_Date, '2025-08-01') AS Sale_Invoice_Date,
    ISNULL(current_data.Morning_Supply, 0) AS Morning_Supply,
    ISNULL(current_data.Morning_Return, 0) AS Morning_Return,
    ISNULL(current_data.Evening_Supply, 0) AS Evening_Supply,
    ISNULL(current_data.Evening_Return, 0) AS Evening_Return,
    ISNULL(op_balance.OP, 0) AS OP,
    ISNULL(current_data.Supply1, 0) AS Supply1,
    ISNULL(current_data.Return1, 0) AS Return1,
    ISNULL(op_balance.OP, 0) + ISNULL(current_data.Supply1, 0) - ISNULL(current_data.Return1, 0) AS CL
FROM op_balance
LEFT JOIN current_data ON 
    op_balance.Customer_Code = current_data.Customer_Code AND 
    op_balance.Route_No = current_data.Route_No
ORDER BY Sale_Invoice_Date;
"



            '            qry = "WITH my_cte AS (
            '    SELECT 
            '        ROW_NUMBER() OVER (PARTITION BY 1 ORDER BY Sale_Invoice_Date) AS SNO,
            '        *
            '    FROM (
            '        SELECT  
            '            MAX(Customer_Name) AS Customer_Name,
            '            MAX(Comp_Name) AS Comp_Name,
            '            MAX(Location_Desc) AS Location_Desc,
            '            MAX(Location_Code) AS Location_Code, 
            '            MAX(Vehicle_Id) AS Vehicle_Id,
            '            MAX(Vehicle_Number) AS Vehicle_Number,
            '            Route_No,
            '            MAX(Route_Desc) AS Route_Desc,
            '            Customer_Code,
            '            Sale_Invoice_Date,

            '            SUM(Qty * 
            '                CASE WHEN RI = 1 AND ShiftType = 'M' AND Type = 'O' THEN 1 ELSE 0 END
            '            ) AS Morning_Supply,

            '            SUM(Qty * 
            '                CASE WHEN RI = 1 AND ShiftType = 'M' AND Type = 'I' THEN 1 ELSE 0 END
            '            ) AS Morning_Return,

            '            SUM(Qty * 
            '                CASE WHEN RI = 1 AND ShiftType = 'E' AND Type = 'O' THEN 1 ELSE 0 END
            '            ) AS Evening_Supply,

            '            SUM(Qty * 
            '                CASE WHEN RI = 1 AND ShiftType = 'E' AND Type = 'I' THEN 1 ELSE 0 END
            '            ) AS Evening_Return

            '        FROM (
            '            SELECT 
            '                TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Type,
            '                TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date,
            '                TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No, 
            '                TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Vehicle_Code AS Vehicle_Id,
            '                TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.VehicleNo AS Vehicle_Number, 
            '                TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Route_code AS Route_No,
            '                TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.ShiftType,
            '                TSPL_ROUTE_MASTER.Route_Desc,
            '                TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Customer_Code,
            '                TSPL_CUSTOMER_MASTER.Customer_Name, 
            '                CAST(TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Invoice_Date AS DATE) AS Sale_Invoice_Date,
            '                TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.CrateQtyRecd AS Qty,
            '                1 AS RI,
            '                TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Location_Code,
            '                TSPL_LOCATION_MASTER.Location_Desc,
            '                TSPL_COMPANY_MASTER.Comp_Name 
            '            FROM TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE
            '            LEFT JOIN TSPL_CRATE_RECEIVED_HEAD_FRESHSALE 
            '                ON TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No = TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Document_No
            '            LEFT JOIN TSPL_ROUTE_MASTER 
            '                ON TSPL_ROUTE_MASTER.Route_No = TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Route_code
            '            LEFT JOIN TSPL_LOCATION_MASTER 
            '                ON TSPL_LOCATION_MASTER.Location_Code = TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Location_Code
            '            LEFT JOIN TSPL_COMPANY_MASTER 
            '                ON TSPL_COMPANY_MASTER.Comp_Code = TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Comp_Code 
            '            LEFT JOIN TSPL_CUSTOMER_MASTER 
            '                ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Customer_Code
            '   where 2=2 " + whrclsRoute + " " + whrclsCust + "

            '        ) AS inner_data
            '        GROUP BY Sale_Invoice_Date, Customer_Code, Route_No
            '    ) AS grouped_data
            '),

            'cte_with_op AS (
            '    SELECT 
            '        *,
            '        (
            '            SELECT ISNULL(SUM((Morning_Supply + Evening_Supply) - (Morning_Return + Evening_Return)), 0)
            '            FROM my_cte 
            '            WHERE 
            '                my_cte.Sale_Invoice_Date < curr.Sale_Invoice_Date
            '                AND my_cte.Customer_Code = curr.Customer_Code
            '                AND my_cte.Route_No = curr.Route_No
            '        ) AS OP
            '    FROM my_cte AS curr
            ' where convert(date,Sale_Invoice_Date,103)>= convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "',103)  
            ' and convert(date,Sale_Invoice_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(txtToDate.Value) + "',103)
            ')

            'SELECT 
            ' '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "' as fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + "' as ToDate,
            '    MAX(Comp_Name) AS Comp_Name,
            '    MAX(Location_Code) AS Location_Code,
            '    MAX(Location_Desc) AS Location_Desc,
            '    MAX(Vehicle_Id) AS Vehicle_Id,
            '    MAX(Vehicle_Number) AS Vehicle_Number,
            '    MAX(Route_No) AS Route_No,
            '    MAX(Route_Desc) AS Route_Desc,
            '    MAX(Customer_Code) AS Customer_Code,
            '    MAX(Customer_Name) AS Customer_Name,
            '    SUM(Morning_Supply) AS Morning_Supply,
            '    SUM(Morning_Return) AS Morning_Return,
            '    SUM(Evening_Supply) AS Evening_Supply,
            '    SUM(Evening_Return) AS Evening_Return,
            '    SUM(OP) AS OP,
            '    SUM(Morning_Supply + Evening_Supply) AS Supply1,
            '    SUM(Morning_Return + Evening_Return) AS Return1,
            '    SUM(OP + ((Morning_Supply + Evening_Supply) - (Morning_Return + Evening_Return))) AS CL

            'FROM cte_with_op
            'GROUP BY Customer_Code, Route_No;
            '"


            ''OLD DATA

            '            qry = " WITH my_cte AS (
            '                      select ROW_NUMBER() over (Partition by 1 order by Sale_Invoice_Date) as SNO , * from (
            '                      select max(Customer_Name)Customer_Name,max(Comp_Name)Comp_Name,max(Location_Desc)Location_Desc,max(Location_Code)Location_Code, max(Vehicle_Id)Vehicle_Id,
            '                      max(Vehicle_Number)Vehicle_Number,(Route_No)Route_No,max(Route_Desc)Route_Desc,
            '                      (Customer_Code)Customer_Code,Sale_Invoice_Date ,
            '					  					  sum(Qty * case when RI=1 THEN 1 else 0 end * case when ShiftType='M' then 1 else 0 end * case when Type='O' then 1 else 0 end ) as  Morning_Supply,
            '                      sum(Qty * case when RI=1 THEN 1 else 0 end * case when ShiftType='M' then 1 else 0 end * case when Type='I' then 1 else 0 end ) as  Morning_Return,
            '                      sum(Qty * case when RI=1 THEN 1 else 0 end * case when ShiftType='E' then 1 else 0 end * case when Type='O' then 1 else 0 end ) as  Evening_Supply,
            '                      sum(Qty * case when RI=1 THEN 1 else 0 end * case when ShiftType='E' then 1 else 0 end * case when Type='I' then 1 else 0 end ) as  Evening_Return
            '					                        from (
            '                      select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.type, TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date, TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No, TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Vehicle_Code AS Vehicle_Id,TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.VehicleNo AS Vehicle_Number, TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Route_code as Route_No,  TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.ShiftType,TSPL_route_master.Route_Desc,
            '                      TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name, 
            '                      CAST(TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Invoice_Date AS DATE) AS Sale_Invoice_Date,
            '                      TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.CrateQtyRecd as Qty ,1 as RI,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Location_Code,
            '                      TSPL_LOCATION_MASTER.Location_Desc,TSPL_COMPANY_MASTER.Comp_Name 
            '                      From TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE
            '                      left outer join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No=TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Document_No
            '                      left outer join tspl_route_master on tspl_route_master.route_No=TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.route_Code 
            '					  left outer join TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Location_Code
            '					  LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code=TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Comp_Code 
            '                      left outer join tspl_customer_master on tspl_customer_master.Cust_Code = TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Customer_Code
            '                  where 2=2 " + whrclsRoute + " " + whrclsCust + "

            '                      )xx where 2=2
            '                      group by Sale_Invoice_Date,Customer_Code,Route_No
            '                      )xxx )
            '                         select '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "' as fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + "' as ToDate,
            '                      Comp_Name,Location_Code,Location_Desc,Vehicle_Id,Vehicle_Number,Route_No,Route_Desc,Customer_Code,Customer_Name,Sale_Invoice_Date,Morning_Supply,Morning_Return,Evening_Supply,Evening_Return,
            '                              (OP+((Morning_Supply+Evening_Supply)-(Morning_Return+Evening_Return))) as CL,
            '                         Evening_Supply+Morning_Supply as Supply1,
            '						Evening_Return+Morning_Return as Return1,
            '                         OP


            '                         from  (

            '                    		  select  
            '    (select isnull(sum((Morning_Supply + Evening_Supply) - (Morning_Return + Evening_Return)), 0)  
            '     from my_cte as InnCTE 
            '     where 
            '         InnCTE.Sale_Invoice_Date < my_cte.Sale_Invoice_Date
            '         AND InnCTE.Customer_Code = my_cte.Customer_Code
            '         AND InnCTE.Route_No = my_cte.Route_No
            '    ) as OP, * 
            'from my_cte
            '                      where convert(date,Sale_Invoice_Date,103)>= convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "',103)  and convert(date,Sale_Invoice_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(txtToDate.Value) + "',103)) xx
            '                      order by xx.Sale_Invoice_Date asc"



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
            gv2.Columns("Comp_Name").HeaderText = "Comp_Name"
            gv2.Columns("Comp_Name").IsVisible = False
            gv2.Columns("Location_Code").HeaderText = "Location_Code"
            gv2.Columns("Location_Code").IsVisible = False
            gv2.Columns("Location_Desc").HeaderText = "Location_Desc"
            gv2.Columns("Location_Desc").IsVisible = False
            gv2.Columns("Vehicle_Id").HeaderText = "Vehicle_Id"
            gv2.Columns("Vehicle_Id").IsVisible = False
            gv2.Columns("Vehicle_Number").HeaderText = "Vehicle_Number"
            gv2.Columns("Vehicle_Number").IsVisible = False

            gv2.Columns("Customer_Code").HeaderText = "Distributor Code"
            gv2.Columns("Customer_Code").IsVisible = True
            gv2.Columns("Customer_Name").HeaderText = "Distributor Name"
            gv2.Columns("Route_No").IsVisible = True
            gv2.Columns("Route_No").HeaderText = "Area Code"
            gv2.Columns("Route_Desc").IsVisible = False
            gv2.Columns("Route_Desc").HeaderText = "Area Name"
            gv2.Columns("Supply1").IsVisible = True
            gv2.Columns("Supply1").HeaderText = "Supply"
            gv2.Columns("Return1").IsVisible = True
            gv2.Columns("Return1").HeaderText = "Return"


            'gv2.Columns("Comp_Code").IsVisible = False
            'gv2.Columns("Comp_Code").HeaderText = "Comp_Code"
            gv2.Columns("Comp_Name").IsVisible = False
            gv2.Columns("Comp_Name").HeaderText = "Comp_Name"
            gv2.Columns("FromDate").IsVisible = False
            gv2.Columns("FromDate").HeaderText = "FromDate"
            gv2.Columns("ToDate").IsVisible = False
            gv2.Columns("ToDate").HeaderText = "ToDate"
            'gv2.Columns("Sale_Invoice_Date").IsVisible = False
            'gv2.Columns("Sale_Invoice_Date").HeaderText = "Sale Invoice Date"


            gv2.Columns("Morning_Supply").IsVisible = False
            gv2.Columns("Morning_Supply").HeaderText = "Morning_Supply"
            gv2.Columns("Morning_Return").IsVisible = False
            gv2.Columns("Morning_Return").HeaderText = "Morning_Return"
            gv2.Columns("Evening_Supply").IsVisible = False
            gv2.Columns("Evening_Supply").HeaderText = "Evening_Supply"
            gv2.Columns("Evening_Return").IsVisible = False
            gv2.Columns("Evening_Return").HeaderText = "Evening_Return"
            gv2.Columns("OP").IsVisible = True
            gv2.Columns("OP").HeaderText = "Privious Balance"
            gv2.Columns("CL").IsVisible = True
            gv2.Columns("CL").HeaderText = "Closing Balance"
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