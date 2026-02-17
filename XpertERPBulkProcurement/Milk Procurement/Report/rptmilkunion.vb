Imports common
Imports System.Text
Imports System.Globalization
Public Class rptmilkunion
    Inherits FrmMainTranScreen
    Dim dt As DataTable
    Dim isSummary As Boolean = False
    Dim status1 As String
    Dim status2 As String
    Dim status3 As String
    Dim status4 As String
    Dim status5 As String
    Dim status6 As String
    Dim status7 As String
    Dim status8 As String
    Dim status9 As String
    Dim status10 As String
    Dim status11 As String
    Dim status12 As String
    Dim status13 As String
    Dim status14 As String
    Dim status15 As String
    Dim status16 As String
    Dim status17 As String
    Dim status18 As String
    Dim status19 As String
    Dim status20 As String
    Dim status21 As String
    Dim status22 As String

    Private Sub rptmilkunion_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Control AndAlso e.Shift AndAlso e.Alt AndAlso e.KeyCode = Keys.F12 Then
            chkRJSBNS.Visible = True
            chkRJSBNS.Checked = True
        Else
            chkRJSBNS.Visible = False
        End If
    End Sub
    Private Sub rptmilkunion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        funreset()
        chkRJSBNS.Visible = False
        chkRJSBNS.Checked = True
    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Griddata(True)
    End Sub
    Sub SetGridFormat1()
        'Gv1.GroupDescriptors.Add(New GridGroupByExpression("Plant as Plant format ""{0}: {1}"" Group By Plant"))
        'Gv1.GroupDescriptors.Add(New GridGroupByExpression("Mcc as Mcc format ""{0}: {1}"" Group By Mcc"))
        gv1.AutoExpandGroups = True
        gv1.ShowGroupPanel = True
        gv1.ShowRowHeaderColumn = False
        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = False
        gv1.EnableFiltering = True
        gv1.ShowFilteringRow = True


        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).BestFit()
        Next

        gv1.Columns("SNo").Name = "SNo"
        gv1.Columns("SNo").IsVisible = False
        gv1.Columns("username").IsVisible = False
        gv1.Columns("Union Name").HeaderText = "Union Name"
        gv1.Columns("Union Name").Width = 500
        gv1.Columns("Union Name").IsVisible = True

        gv1.Columns("Fromdate").HeaderText = "From Date"
        gv1.Columns("Fromdate").Width = 500
        gv1.Columns("Fromdate").IsVisible = False

        gv1.Columns("Todate").HeaderText = "To Date"
        gv1.Columns("Todate").Width = 500
        gv1.Columns("Todate").IsVisible = False

        If clsCommon.CompairString(ddlReportType.SelectedValue, "UWASR") = CompairStringResult.Equal Then
            gv1.Columns("Dis_QtyInLTR").HeaderText = "Dis QTY"
            gv1.Columns("Dis_QtyInLTR").Width = 200
            gv1.Columns("Dis_QtyInLTR").FormatString = "{0:n3}"

            gv1.Columns("Dis Avg").HeaderText = "Dis Avg"
            gv1.Columns("Dis Avg").Width = 200
            gv1.Columns("Dis Avg").FormatString = "{0:n3}"

            gv1.Columns("Prod_QTY").HeaderText = "Prod QTY"
            gv1.Columns("Prod_QTY").IsVisible = True
            gv1.Columns("Prod_QTY").FormatString = ""

            gv1.Columns("Prod Avg").HeaderText = "Prod Avg"
            gv1.Columns("Prod Avg").Width = 200
            gv1.Columns("Prod Avg").FormatString = "{0:n3}"

            gv1.Columns("TotalLtr_ItemWiseDemand").HeaderText = "Demand QTY"
            gv1.Columns("TotalLtr_ItemWiseDemand").IsVisible = True
            gv1.Columns("TotalLtr_ItemWiseDemand").FormatString = ""

            gv1.Columns("Dem Avg").HeaderText = "Dem Avg"
            gv1.Columns("Dem Avg").Width = 200
            gv1.Columns("Dem Avg").FormatString = "{0:n3}"

            gv1.Columns("Milk_WeightProc").HeaderText = "Proc QTY"
            gv1.Columns("Milk_WeightProc").IsVisible = True
            gv1.Columns("Milk_WeightProc").FormatString = "{0:n3}"

            gv1.Columns("Proc Avg").HeaderText = "Proc Avg"
            gv1.Columns("Proc Avg").Width = 200
            gv1.Columns("Proc Avg").FormatString = "{0:n3}"

            gv1.Columns("Purchase_Count").HeaderText = "No of PO"
            gv1.Columns("Purchase_Count").IsVisible = True

            gv1.Columns("SRN_Count").HeaderText = "No of SRN"
            gv1.Columns("SRN_Count").IsVisible = True

            gv1.Columns("GRN_Count").HeaderText = "No of GRN"
            gv1.Columns("GRN_Count").IsVisible = True

            gv1.Columns("E_Invoice_Count").HeaderText = "E Invoice No"
            gv1.Columns("E_Invoice_Count").IsVisible = True

            gv1.Columns("Sale_Voucher").HeaderText = "Sale Voucher"
            gv1.Columns("Sale_Voucher").IsVisible = True
            gv1.Columns("Sale_Voucher").FormatString = "{0:n3}"

            gv1.Columns("Purchase_Voucher").HeaderText = "Purchase Voucher"
            gv1.Columns("Purchase_Voucher").IsVisible = True
            gv1.Columns("Purchase_Voucher").FormatString = "{0:n3}"

            gv1.Columns("Last_Salary").HeaderText = "Last Salary"
            gv1.Columns("Last_Salary").IsVisible = True

            gv1.Columns("Last DBT App. Month").HeaderText = "Last DBT App. Month"
            gv1.Columns("Last DBT App. Month").IsVisible = True

        Else
            gv1.Columns("UnionCode").IsVisible = False
            gv1.Columns("Dis_QtyInLTR").HeaderText = "Dis QTY"
            gv1.Columns("Dis_QtyInLTR").Width = 200
            gv1.Columns("Dis_QtyInLTR").FormatString = "{0:N2}"

            gv1.Columns("Dis_FATKG").HeaderText = "Dis FATKG"
            gv1.Columns("Dis_FATKG").FormatString = "{0:n3}"

            gv1.Columns("Dis_SNFKG").HeaderText = " Dis SNFKG"
            gv1.Columns("Dis_SNFKG").IsVisible = True
            gv1.Columns("Dis_SNFKG").FormatString = "{0:n3}"

            gv1.Columns("Prod_QTY").HeaderText = "Prod QTY"
            gv1.Columns("Prod_QTY").IsVisible = True
            gv1.Columns("Prod_QTY").FormatString = "{0:N2}"

            gv1.Columns("Prod_FATkg").HeaderText = "Prod FATKG"
            gv1.Columns("Prod_FATkg").IsVisible = True
            gv1.Columns("Prod_FATkg").FormatString = "{0:n3}"

            gv1.Columns("Prod_SNFkg").HeaderText = "Prod SNFKG"
            gv1.Columns("Prod_SNFkg").IsVisible = True
            gv1.Columns("Prod_SNFkg").FormatString = "{0:n3}"

            gv1.Columns("TotalLtr_ItemWiseDemand").HeaderText = "Demand QTY"
            gv1.Columns("TotalLtr_ItemWiseDemand").IsVisible = True
            gv1.Columns("TotalLtr_ItemWiseDemand").FormatString = "{0:N2}"

            gv1.Columns("FATKGDemand").HeaderText = "Demand FATKG"
            gv1.Columns("FATKGDemand").IsVisible = True
            gv1.Columns("FATKGDemand").FormatString = "{0:n3}"

            gv1.Columns("SNFKGDemand").HeaderText = "Demand SNFKG"
            gv1.Columns("SNFKGDemand").IsVisible = True
            gv1.Columns("SNFKGDemand").FormatString = "{0:n3}"


            gv1.Columns("Milk_WeightProc").HeaderText = "Proc Qty"
            gv1.Columns("Milk_WeightProc").IsVisible = True
            gv1.Columns("Milk_WeightProc").FormatString = "{0:N2}"

            gv1.Columns("FATKGProc").HeaderText = "Proc FATKG"
            gv1.Columns("FATKGProc").IsVisible = True
            gv1.Columns("FATKGProc").FormatString = "{0:n3}"

            gv1.Columns("SNFKGProc").HeaderText = "Proc SNFKG"
            gv1.Columns("SNFKGProc").IsVisible = True
            gv1.Columns("SNFKGProc").FormatString = "{0:n3}"


            gv1.Columns("TankerQty").HeaderText = "Tanker Qty"
            gv1.Columns("TankerQty").IsVisible = True
            gv1.Columns("TankerQty").FormatString = "{0:N2}"

            gv1.Columns("TankerFATKG").HeaderText = "Tanker FATKG"
            gv1.Columns("TankerFATKG").IsVisible = True
            gv1.Columns("TankerFATKG").FormatString = "{0:n3}"

            gv1.Columns("TankerSNFKG").HeaderText = "Tanker SNFKG"
            gv1.Columns("TankerSNFKG").IsVisible = True
            gv1.Columns("TankerSNFKG").FormatString = "{0:n3}"

            gv1.Columns("LastDBT").HeaderText = "Last DBT"
            gv1.Columns("LastDBT").IsVisible = True
            gv1.Columns("LastDBT").FormatString = "{0:n3}"


            gv1.Columns("Sale_Voucher").HeaderText = "Sale Voucher"
            gv1.Columns("Sale_Voucher").IsVisible = True
            gv1.Columns("Sale_Voucher").FormatString = "{0:N2}"

            gv1.Columns("Purchase_Voucher").HeaderText = "Purchase Voucher"
            gv1.Columns("Purchase_Voucher").IsVisible = True
            gv1.Columns("Purchase_Voucher").FormatString = "{0:N2}"

            gv1.Columns("Last_Salary").HeaderText = "Last Salary"
            gv1.Columns("Last_Salary").IsVisible = True
        End If

        gv1.ShowGroupPanel = True
        gv1.MasterTemplate.AutoExpandGroups = True

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim i As Integer = 0
        If clsCommon.CompairString(ddlReportType.SelectedValue, "UWSR") = CompairStringResult.Equal Then
            i = 2
        Else
            i = 7
        End If
        For ii As Integer = 2 To gv1.Columns.Count - i
            summaryRowItem.Add(New GridViewSummaryItem(gv1.Columns(ii).Name, "{0:N2}", GridAggregateFunction.Sum))
        Next

        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom

        View()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(MyBase.Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Sub View()

        If gv1.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()
            If clsCommon.CompairString(ddlReportType.SelectedValue, "UWSR") = CompairStringResult.Equal Then
                view.ColumnGroups.Add(New GridViewColumnGroup("Union"))
                view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Union Name").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("Milk Procurement (As Per Truck Sheet)"))
                view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("Milk_WeightProc").Name)
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("FATKGProc").Name)
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("SNFKGProc").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("Milk Procurement (As Per Tanker)"))
                view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("TankerQty").Name)
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("TankerFATKG").Name)
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("TankerSNFKG").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("DBT"))
                view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv1.Columns("LastDBT").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("Production"))
                view.ColumnGroups(4).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv1.Columns("Prod_QTY").Name)
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv1.Columns("Prod_FATkg").Name)
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv1.Columns("Prod_SNFkg").Name)


                view.ColumnGroups.Add(New GridViewColumnGroup("Demand"))
                view.ColumnGroups(5).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(5).Rows(0).ColumnNames.Add(gv1.Columns("TotalLtr_ItemWiseDemand").Name)
                view.ColumnGroups(5).Rows(0).ColumnNames.Add(gv1.Columns("FATKGDemand").Name)
                view.ColumnGroups(5).Rows(0).ColumnNames.Add(gv1.Columns("SNFKGDemand").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("Dispatch"))
                view.ColumnGroups(6).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(6).Rows(0).ColumnNames.Add(gv1.Columns("Dis_QtyInLTR").Name)
                view.ColumnGroups(6).Rows(0).ColumnNames.Add(gv1.Columns("Dis_FATKG").Name)
                view.ColumnGroups(6).Rows(0).ColumnNames.Add(gv1.Columns("Dis_SNFKG").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("Accounts"))
                view.ColumnGroups(7).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(7).Rows(0).ColumnNames.Add(gv1.Columns("Sale_Voucher").Name)
                view.ColumnGroups(7).Rows(0).ColumnNames.Add(gv1.Columns("Purchase_Voucher").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("PayRoll"))
                view.ColumnGroups(8).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(8).Rows(0).ColumnNames.Add(gv1.Columns("Last_Salary").Name)

            ElseIf clsCommon.CompairString(ddlReportType.SelectedValue, "UWASR") = CompairStringResult.Equal Then
                view.ColumnGroups.Add(New GridViewColumnGroup("Union"))
                view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Union Name").Name)


                view.ColumnGroups.Add(New GridViewColumnGroup(" Milk Procurement"))
                view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("Milk_WeightProc").Name)
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("Proc Avg").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("Production"))
                view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Prod_QTY").Name)
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Prod Avg").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("Purchase & Stores"))
                view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv1.Columns("Purchase_Count").Name)
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv1.Columns("SRN_Count").Name)
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv1.Columns("GRN_Count").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("Sales(Demand)"))
                view.ColumnGroups(4).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv1.Columns("TotalLtr_ItemWiseDemand").Name)
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv1.Columns("Dem Avg").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("Sales(Dispatch)"))
                view.ColumnGroups(5).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(5).Rows(0).ColumnNames.Add(gv1.Columns("Dis_QtyInLTR").Name)
                view.ColumnGroups(5).Rows(0).ColumnNames.Add(gv1.Columns("Dis Avg").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("Sales (E-Invoice)"))
                view.ColumnGroups(6).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(6).Rows(0).ColumnNames.Add(gv1.Columns("E_Invoice_Count").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("Accounyts / Payroll"))
                view.ColumnGroups(7).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(7).Rows(0).ColumnNames.Add(gv1.Columns("Sale_Voucher").Name)
                view.ColumnGroups(7).Rows(0).ColumnNames.Add(gv1.Columns("Purchase_Voucher").Name)
                view.ColumnGroups(7).Rows(0).ColumnNames.Add(gv1.Columns("Last_Salary").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("Last DBT App. Month"))
                view.ColumnGroups(8).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(8).Rows(0).ColumnNames.Add(gv1.Columns("Last DBT App. Month").Name)
            End If


            gv1.ViewDefinition = view
        End If
    End Sub
    Private Sub btngo_Click(sender As Object, e As EventArgs) Handles btngo.Click
        Griddata(False)
        isSummary = False
    End Sub
    Sub Status()
        If rdbPosted.Checked Then
            status1 = " and  sh.Status=1 "
            status2 = " and pe.POSTED= 1 "
            status3 = " and dbm.Posted= 1 "
            status4 = " and muh.Status= 1 "
            status5 = "and msh.Status= 1"
            status6 = "and TSPL_GENERATE_SALARY.posted= 1 "
            status7 = "and TSPL_SD_SALE_INVOICE_HEAD.Status= 1 "
            status8 = "and TSPL_MILK_PURCHASE_INVOICE_HEAD.Posted= 1 "
            status9 = "and TSPL_Dispatch_BulkSale.Posted= 1 "
            status10 = " and TSPL_PRODUCTION_UPLOADER_HEAD.Status= 1"
            status11 = " and TSPL_PURCHASE_ORDER_HEAD.Status=1 "
            status12 = " and TSPL_SRN_HEAD.Status=1"
            status13 = " and mcs.Status=1 "
            status14 = " and TSPL_DBT_NEFT.Status=1 "
            status15 = " and TSPL_BOOKING_MATSER.Posted=1 "
            status16 = " and TSPL_GRN_HEAD.Status=1"
            status17 = " and tspl_shift_mgmt.Status = 1 "
            status18 = " and TSPL_MILK_COLLECTION_BMCDCS.Status=1"
            status19 = " and TSPL_MILK_COLLECTION_DCS.Status=1"
            status20 = " And TSPL_DCS_SALE_ENTRY.Status=1"
            status21 = " And TSPL_PRODUCT_DEMAND_BOOKING_MASTER.Posted=1"
            status22 = " And TSPL_ADJUSTMENT_HEADER.Posted='Y'"
        ElseIf rdbUnposted.Checked Then
            status1 = " and  sh.Status= 0 "
            status2 = " and pe.POSTED= 0 "
            status3 = " and dbm.Posted= 0 "
            status4 = " and muh.Status= 0 "
            status5 = "and msh.Status= 0 "
            status6 = "and TSPL_GENERATE_SALARY.posted= 0 "
            status7 = "and TSPL_SD_SALE_INVOICE_HEAD.Status= 0 "
            status8 = "and TSPL_MILK_PURCHASE_INVOICE_HEAD.Posted= 0 "
            status9 = "and TSPL_Dispatch_BulkSale.Posted= 0 "
            status10 = "and TSPL_PRODUCTION_UPLOADER_HEAD.Status=0 "
            status11 = " and TSPL_PURCHASE_ORDER_HEAD.Status=0 "
            status12 = " and TSPL_SRN_HEAD.Status=0 "
            status13 = " and mcs.Status=0 "
            status14 = " and TSPL_DBT_NEFT.Status=0 "
            status15 = " and TSPL_BOOKING_MATSER.Posted=0 "
            status16 = " and TSPL_GRN_HEAD.Status= 0 "
            status17 = " and tspl_shift_mgmt.Status = 0 "
            status18 = " and TSPL_MILK_COLLECTION_BMCDCS.Status=0"
            status19 = " and TSPL_MILK_COLLECTION_DCS.Status=0"
            status20 = " And TSPL_DCS_SALE_ENTRY.Status=0"
            status21 = " And TSPL_PRODUCT_DEMAND_BOOKING_MASTER.Posted=0"
            status22 = " And TSPL_ADJUSTMENT_HEADER.Posted='N'"
        Else
            status1 = " "
            status2 = " "
            status3 = " "
            status4 = " "
            status5 = " "
            status6 = " "
            status7 = " "
            status8 = " "
            status9 = " "
            status10 = " "
            status11 = " "
            status12 = " "
            status13 = "  "
            status14 = " "
            status15 = " "
            status16 = " "
            status17 = "  "
            status18 = "  "
            status19 = "  "
            status20 = "  "
            status21 = "  "
            status22 = "  "
        End If
    End Sub
    Public Sub Griddata(ByVal print As Boolean)
        Try
            isSummary = True
            Dim strUnion As New StringBuilder()
            Dim query As String = ""
            Dim qry As String = ""
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("Select name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                Exit Sub
            End If
            Dim docNo As String = ""
            query = " SELECT ROW_NUMBER() Over (Order By (Select 1)) As [SNo], [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE  Union_Report=1 ORDER BY Location_Name"

            '          If objCommonVar.RCDFCFP Then
            '              query = " 
            '  SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE  Union_Report=1 and DataBase_Name not in ('BANSWARA')"
            '              If chkRJSBNS.Checked Then
            '                  query += "union all
            'SELECT 'Banswara' AS Location_Name,'BNS' AS DataBase_Name
            'ORDER BY Location_Name"
            '              End If
            '              dt = clsDBFuncationality.GetDataTable(query)
            '          Else
            '              Dim DatabaseName As New DataTable()
            '              DatabaseName.Rows.Add(objCommonVar.CurrDatabase)
            '              dt = DatabaseName
            '          End If

            dt = clsDBFuncationality.GetDataTable(query)
            query = ""
            Dim dtUnion As DataTable = New DataTable()
            Dim dt2 As DataTable = New DataTable()
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                'If ii > 0 Then
                '    '  query += " UNION ALL "
                'End If
                Status()
                If clsCommon.CompairString(ddlReportType.SelectedValue, "UWSR") = CompairStringResult.Equal OrElse clsCommon.CompairString(ddlReportType.SelectedValue, "UTWSR") = CompairStringResult.Equal Then
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        If ii = dt.Rows.Count - 1 Then
                            strUnion.Append("('" & clsCommon.myCstr(dt.Rows(ii)("Database_Name")) & "'," & clsCommon.myCstr(ii + 1) & ")")
                        Else
                            strUnion.Append("('" & clsCommon.myCstr(dt.Rows(ii)("Database_Name")) & "'," & clsCommon.myCstr(ii + 1) & "),")
                        End If
                    Next

                    query = "DECLARE @FromDate DATE='" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "'
DECLARE @ToDate   DATE='" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "'

DECLARE @SQL NVARCHAR(MAX)=''

---------------------------------------------------------
-- DATABASE LIST
---------------------------------------------------------
DECLARE @DB TABLE(DBName SYSNAME,SNo INT)

INSERT INTO @DB Values " & clsCommon.myCstr(strUnion) & "

---------------------------------------------------------
-- BUILD FINAL QUERY
---------------------------------------------------------
SELECT @SQL =
@SQL +
CASE WHEN @SQL='' THEN '' ELSE CHAR(13)+' UNION ALL '+CHAR(13) END +
'
SELECT
 '''+DBName+''' AS UnionCode,
 '+CAST(SNo AS VARCHAR)+' AS SNo,
 '''+DBName+''' AS [Union Name],
 Convert(Varchar(10),@FromDate,103) AS FromDate,
 Convert(Varchar(10),@ToDate,103) AS ToDate,
 ''Administrator'' AS UserName,

 ISNULL(P.Milk_WeightProc,0) AS Milk_WeightProc,
 ISNULL(P.FATKGProc,0) AS FATKGProc,
 ISNULL(P.SNFKGProc,0) AS SNFKGProc,

 ISNULL(T.TankerQty,0) AS TankerQty,
 ISNULL(T.TankerFATKG,0) AS TankerFATKG,
 ISNULL(T.TankerSNFKG,0) AS TankerSNFKG,

 ISNULL(PR.Prod_QTY,0) AS Prod_QTY,
 ISNULL(PR.Prod_FATkg,0) AS Prod_FATkg,
 ISNULL(PR.Prod_SNFkg,0) AS Prod_SNFkg,

 ISNULL(DM.TotalLtr_ItemWiseDemand,0) AS TotalLtr_ItemWiseDemand,
 ISNULL(DM.FATKGDemand,0) AS FATKGDemand,
 ISNULL(DM.SNFKGDemand,0) AS SNFKGDemand,

 ISNULL(DS.Dis_QtyInLTR,0) AS Dis_QtyInLTR,
 ISNULL(DS.Dis_FATKG,0) AS Dis_FATKG,
 ISNULL(DS.Dis_SNFKG,0) AS Dis_SNFKG,

 ISNULL(S.Sale_Voucher,0) AS Sale_Voucher,
 ISNULL(PC.Purchase_Voucher,0) AS Purchase_Voucher,

 LS.Last_Salary,
 DBT.LastDBT

FROM (SELECT 1 X) A

---------------------------------------------------------
-- PROCUREMENT
---------------------------------------------------------
OUTER APPLY
(
Select SUM(Milk_Weight) AS Milk_WeightProc,SUM(Milk_Weight*FAT/100.0) AS FATKGProc,SUM(Milk_Weight*SNF/100.0) AS SNFKGProc from (
" & ReturnMPUDProcurementQry("QUOTENAME(DBName)", "@FromDate AND @ToDate " & status4) & "
Union All
" & ReturnMilkShiftUploaderProcurementQry("QUOTENAME(DBName)", "@FromDate AND @ToDate " & status5) & "
Union All
" & ReturnMilkCollectionDCSProcurementQry("QUOTENAME(DBName)", "@FromDate AND @ToDate " & status13) & "
Union All
" & ReturnMilkCollectionBMCDCSProcurementQry("QUOTENAME(DBName)", "@FromDate AND @ToDate " & status18) & "
)xyz
) P

---------------------------------------------------------
-- TANKER
---------------------------------------------------------
OUTER APPLY
(
 SELECT SUM(Qty) AS TankerQty,SUM(Fat_KG) AS TankerFATKG,SUM(SNF_KG) AS TankerSNFKG
 FROM (" & ReturnInventoryMovementProcurementTankerQry("QUOTENAME(DBName)", "@FromDate AND @ToDate") & ")xyz
) T

---------------------------------------------------------
-- PRODUCTION (ALIAS FIXED)
---------------------------------------------------------
OUTER APPLY
(
 SELECT
  SUM(ISNULL(Prod_QTY,0)) AS Prod_QTY,
  SUM(ISNULL((Prod_QTY*STD_FatPer/100.0),0)) AS Prod_FATkg,
  SUM(ISNULL((Prod_QTY*STD_SNFPer/100.0),0)) AS Prod_SNFkg
 FROM
 (
  " & ReturnPPProdProductionQry("QUOTENAME(DBName)", "@FromDate AND @ToDate " & status2) & "
Union All 
" & ReturnProdUPProductionQry("QUOTENAME(DBName)", "@FromDate AND @ToDate " & status10) & "
Union All 
" & ReturnShiftGMTProdcutionQry("QUOTENAME(DBName)", "@FromDate AND @ToDate " & status17) & "
Union All 
" & ReturnAdjstProductionQry("QUOTENAME(DBName)", "@FromDate AND @ToDate " & Replace(status22, "'", "''")) & "
 ) X
) PR

---------------------------------------------------------
-- DEMAND (ALIAS FIXED)
---------------------------------------------------------
OUTER APPLY
(
 SELECT SUM(ISNULL(TotalLtr_ItemWiseDemand,0)) AS TotalLtr_ItemWiseDemand,SUM(ISNULL(TotalLtr_ItemWiseDemand,0)*STD_FatPer/100.0) AS FATKGDemand,SUM(ISNULL(TotalLtr_ItemWiseDemand,0)*STD_SNFPer/100.0) AS SNFKGDemand FROM
(
" & ReturnDemandBookingDemandQry("QUOTENAME(DBName)", "@FromDate AND @ToDate " & status3) & "
Union All
" & ReturnDisBulkDemandQry("QUOTENAME(DBName)", "@FromDate AND @ToDate " & status9) & "
Union All
" & ReturnBookingDemandQry("QUOTENAME(DBName)", "@FromDate AND @ToDate " & status15) & "
Union All
" & ReturnDCSSaleEntryDemandQry("QUOTENAME(DBName)", "@FromDate AND @ToDate " & status20) & "
Union All
" & ReturnProdDemandDemandQry("QUOTENAME(DBName)", "@FromDate AND @ToDate " & status21) & "
) xyz
) DM

---------------------------------------------------------
-- DISBURSEMENT (ALIAS FIXED)
---------------------------------------------------------
OUTER APPLY
(
 SELECT
  SUM(ISNULL(Dis_QtyInLTR,0)) AS Dis_QtyInLTR,
  SUM(ISNULL(Dis_QtyInLTR,0)*STD_FatPer/100.0) AS Dis_FATKG,
  SUM(ISNULL(Dis_QtyInLTR,0)*STD_SNFPer/100.0) AS Dis_SNFKG
 FROM (
" & ReturnShipDispatchQry("QUOTENAME(DBName)", "@FromDate AND @ToDate " & status1) & "
Union All
" & ReturnBulkDispatchQry("QUOTENAME(DBName)", "@FromDate AND @ToDate " & status9) & "
)xyz
) DS

---------------------------------------------------------
-- SALE
---------------------------------------------------------
OUTER APPLY
(
 " & ReturnSaleInvoiceQry("QUOTENAME(DBName)", "@FromDate AND @ToDate " & status7) & "
) S

---------------------------------------------------------
-- PURCHASE
---------------------------------------------------------
OUTER APPLY
(
 " & ReturnMilkPurchaseInvoiceQry("QUOTENAME(DBName)", "@FromDate AND @ToDate " & status8) & "
) PC

---------------------------------------------------------
-- LAST SALARY
---------------------------------------------------------
OUTER APPLY
(
 SELECT TOP 1
 LEFT(DATENAME(MONTH,DATE_TO),3)+'' ''+
 CONVERT(VARCHAR(4),YEAR(DATE_TO)) AS Last_Salary
 FROM '+QUOTENAME(DBName)+'.dbo.TSPL_PAYPERIOD_MASTER
 ORDER BY DATE_TO DESC
) LS

---------------------------------------------------------
-- LAST DBT
---------------------------------------------------------
OUTER APPLY
(
 SELECT FORMAT(
 DATEFROMPARTS(
 YEAR(MAX(To_Date)),
 MONTH(MAX(To_Date)),1),
 ''MMM-yyyy'') AS LastDBT
 FROM '+QUOTENAME(DBName)+'.dbo.TSPL_DBT_NEFT where 1=1 " & status14 & "
) DBT
'
FROM @DB

---------------------------------------------------------
-- EXECUTE FINAL QUERY
---------------------------------------------------------
EXEC sp_executesql
@SQL,
N'@FromDate DATE,@ToDate DATE',
@FromDate,
@ToDate
"






                    '                        query = " select * from (select '" & clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) & "' As UnionCode," & clsCommon.myCstr(ii + 1) & " AS SNo,'" & clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) & "' AS [Union Name],'" & clsCommon.GetPrintDate(txtFromDate.Value) & "'as Fromdate,'" & clsCommon.GetPrintDate(txtToDate.Value) & "'as Todate,'" & objCommonVar.CurrentUser & "' as username,
                    '                                     CAST(ROUND(ISNULL(SUM(Dis_Procurement.Milk_WeightProc),0),0) AS BIGINT) AS Milk_WeightProc,
                    'CAST(ROUND(ISNULL(SUM(Dis_Procurement.FATKGProc),0),0) AS BIGINT) AS FATKGProc,
                    'CAST(ROUND(ISNULL(SUM(Dis_Procurement.SNFKGProc),0),0) AS BIGINT) AS SNFKGProc,
                    'CAST(ROUND(ISNULL(SUM(Tanker.TankerQty),0),0) AS BIGINT) AS TankerQty,
                    'CAST(ROUND(ISNULL(SUM(Tanker.TankerFATKG),0),0) AS BIGINT) AS TankerFATKG,
                    'CAST(ROUND(ISNULL(SUM(Tanker.TankerSNFKG),0),0) AS BIGINT) AS TankerSNFKG,
                    'CAST(ROUND(ISNULL(SUM(Dis_Production.Prod_QTY),0),0) AS BIGINT) AS Prod_QTY,
                    'CAST(ROUND(ISNULL(SUM(Dis_Production.Prod_FATkg),0),0) AS BIGINT) AS Prod_FATkg,
                    'CAST(ROUND(ISNULL(SUM(Dis_Production.Prod_SNFkg),0),0) AS BIGINT) AS Prod_SNFkg,
                    'CAST(ROUND(ISNULL(SUM(Dis_Demand.TotalLtr_ItemWiseDemand),0),0) AS BIGINT) AS TotalLtr_ItemWiseDemand,
                    'CAST(ROUND(ISNULL(SUM(Dis_Demand.FATKGDemand),0),0) AS BIGINT) AS FATKGDemand,
                    'CAST(ROUND(ISNULL(SUM(Dis_Demand.SNFKGDemand),0),0) AS BIGINT) AS SNFKGDemand,
                    'CAST(ROUND(ISNULL(SUM(Dis_Disbursement.Dis_QtyInLTR),0),0) AS BIGINT) AS Dis_QtyInLTR,
                    'CAST(ROUND(ISNULL(SUM(Dis_Disbursement.Dis_FATKG),0),0) AS BIGINT) AS Dis_FATKG,
                    'CAST(ROUND(ISNULL(SUM(Dis_Disbursement.Dis_SNFKG),0),0) AS BIGINT) AS Dis_SNFKG,
                    'CAST(ROUND(ISNULL(SUM(Sale_invoice.Sale_Voucher),0),0) AS BIGINT) AS Sale_Voucher,
                    'CAST(ROUND(ISNULL(SUM(Milk_Purchase_invoice.Purchase_Voucher),0),0) AS BIGINT) AS Purchase_Voucher,
                    '                    (SELECT TOP 1  LEFT(DATENAME(MONTH, [" & clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) & "].[dbo].TSPL_PAYPERIOD_MASTER.DATE_TO),3) + ' ' + CONVERT(VARCHAR(4), YEAR([" & clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) & "].[dbo].TSPL_PAYPERIOD_MASTER.DATE_TO)) FROM [" & clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) & "].[dbo].TSPL_GENERATE_SALARY
                    '                    left join [" & clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) & "].[dbo].TSPL_PAYPERIOD_MASTER on [" & clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) & "].[dbo].TSPL_GENERATE_SALARY.PAY_PERIOD_CODE = [" & clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) & "].[dbo].TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE
                    '                    where 2=2" & status6 & " ORDER BY DATE_TO DESC) as Last_Salary,Max(DBT.LastDBT)LastDBT
                    '                    FROM 
                    '                    (SELECT SUM(Dis_QtyInLTR) AS Dis_QtyInLTR,SUM(Dis_FATKG) AS Dis_FATKG,SUM(Dis_SNFKG) AS Dis_SNFKG
                    '                    FROM (( SELECT SUM(Dis_QtyInLTR) AS Dis_QtyInLTR,SUM(Dis_FATKG) AS Dis_FATKG,SUM(Dis_SNFKG) AS Dis_SNFKG from ( select (Dis_QtyInLTR) AS Dis_QtyInLTR,(Dis_QtyInLTR * STD_FatPer) / 100 AS Dis_FATKG,(Dis_QtyInLTR * STD_SNFPer) / 100 AS Dis_SNFKG from ( " & ReturnShipDispatchQry(clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")), status1) & " )xx)xxx
                    '                        union all
                    '                    SELECT cast(  (xxxx.Qty) as decimal(18,2)) AS TotalLtr_ItemWiseDemand,(xxxx.Fat_KG ) AS FATKGDemand,(xxxx.SNF_KG) AS SNFKGDemand
                    '                    from ( " & ReturnBulkDispatchQry(clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")), status9) & " )xxxx ))Disp_BUlksale
                    '                    ) AS Dis_Disbursement,
                    '                    (SELECT SUM(Prod_QTY) AS Prod_QTY,SUM(Prod_FATkg) AS Prod_FATkg,SUM(Prod_SNFkg) AS Prod_SNFkg FROM ((SELECT  SUM(Prod_QTY) AS Prod_QTY,sum(Prod_FATkg)Prod_FATkg,sum(Prod_SNFkg)Prod_SNFkg from (select (Prod_QTY) AS Prod_QTY,(Prod_QTY * STD_FatPer) / 100 AS Prod_FATkg,(Prod_QTY * STD_SNFPer) / 100 AS Prod_SNFkg
                    '						from ( " & ReturnPPProdProductionQry(clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")), status2) & ")XX)xxx
                    '                        union all
                    '                        select sum(Prod_QTY)Prod_QTY,sum(Prod_FATkg)Prod_FATkg,sum(Prod_SNFkg)Prod_SNFkg from (select (Prod_QTY) AS Prod_QTY,(Prod_QTY * STD_FatPer) / 100 AS Prod_FATkg,(Prod_QTY * STD_SNFPer) / 100 AS Prod_SNFkg
                    '                    from ( " & ReturnProdUPProductionQry(clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")), status10) & ") xx)xxx                       
                    '                      UNION ALL
                    '                      select isnull(sum(Prod_QTY),0)Prod_QTY,isnull(sum(Prod_FATkg),0)Prod_FATkg,isnull(sum(Prod_SNFkg),0)Prod_SNFkg 
                    '                    from (select (Prod_QTY) AS Prod_QTY,(Prod_QTY * STD_FatPer) / 100 AS Prod_FATkg,(Prod_QTY * STD_SNFPer) / 100 AS Prod_SNFkg
                    '                    from (" & ReturnShiftGMTProdcutionQry(clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")), status17) & ") xx ) xxx 
                    '                    Union All
                    '					   select isnull(sum(Item_Quantity),0)Prod_QTY,isnull(sum(FAT_KG),0)Prod_FATkg,isnull(sum(SNF_KG),0)Prod_SNFkg from (
                    '					   " & ReturnAdjstProductionQry(clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")), Nothing) & " )Adjstment
                    '                      ))Prod_Uploder ) AS Dis_Production,
                    '                        (SELECT SUM(TotalLtr_ItemWiseDemand) AS TotalLtr_ItemWiseDemand,
                    '                        SUM(FATKGDemand) AS FATKGDemand,SUM(SNFKGDemand) AS SNFKGDemand
                    '                    FROM ((SELECT SUM(TotalLtr_ItemWiseDemand) AS TotalLtr_ItemWiseDemand,sum(FATKGDemand)FATKGDemand,sum(SNFKGDemand)SNFKGDemand
                    '                    from (  select (TotalLtr_ItemWiseDemand) AS TotalLtr_ItemWiseDemand,
                    '                          (TotalLtr_ItemWiseDemand * STD_FatPer) / 100 AS FATKGDemand,
                    '                          (TotalLtr_ItemWiseDemand * STD_SNFPer) / 100 AS SNFKGDemand
                    '                    FROM (" & ReturnDemandBookingDemandQry(clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")), status3) & ")xx)xxx 
                    '                        union all
                    '                        SELECT 
                    '                      cast(  SUM(xxxx.Qty) as decimal(18,2)) AS TotalLtr_ItemWiseDemand,SUM(xxxx.Fat_KG ) AS FATKGDemand,SUM(xxxx.SNF_KG) AS SNFKGDemand
                    '                    from ( " & ReturnDisBulkDemandQry(clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")), status9) & ")xxxx )
                    '                            union all
                    '                           SELECT   SUM(TotalLtr_ItemWiseDemand) AS TotalLtr_ItemWiseDemand,sum(FATKGDemand)FATKGDemand,sum(SNFKGDemand)SNFKGDemand from (
                    '                       select (TotalLtr_ItemWiseDemand) AS TotalLtr_ItemWiseDemand,(TotalLtr_ItemWiseDemand * STD_FatPer) / 100 AS FATKGDemand,(TotalLtr_ItemWiseDemand * STD_SNFPer) / 100 AS SNFKGDemand
                    '                        from (" & ReturnBookingDemandQry(clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")), status15) & ")xx)xxx)Disp_BUlksale
                    '                        Union All
                    '						Select SUM(Qty) As TotalLtr_ItemWiseDemand,SUM(FATKG) As FATKGDemand,SUM(SNFKG) As SNFKGDemand from
                    '						(" & ReturnDCSSaleEntryDemandQry(clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")), status20) & ") As DCSSaleEntry
                    '						Union All
                    '						Select SUM(Qty) As TotalLtr_ItemWiseDemand,SUM(FATKG) As FATKGDemand,SUM(SNFKG) As SNFKGDemand from
                    '						(" & ReturnProdDemandDemandQry(clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")), status21) & ") As ProductDemand
                    '                    ) AS Dis_Demand,
                    '                    (SELECT SUM(milk_weight) AS Milk_WeightProc,SUM(fatkg) AS FATKGProc,SUM(SNFKG) AS SNFKGProc
                    '                    FROM ( " & ReturnMPUDProcurementQry(clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")), status4) & " 
                    '                        UNION ALL
                    '                        " & ReturnMilkShiftUploaderProcurementQry(clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")), status5) & "
                    '                        UNION ALL
                    '                        " & ReturnMilkCollectionDCSProcurementQry(clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")), Nothing) & "
                    '                         UNION ALL
                    '                        " & ReturnMilkCollectionBMCDCSProcurementQry(clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")), status18 & status19) & " 
                    '                        ) AS Procurement
                    '                    ) AS Dis_Procurement,
                    '                        (Select Sum(Qty) As TankerQty,Sum(Fat_KG) As TankerFATKG,Sum(SNF_KG) As TankerSNFKG from (
                    '					" & ReturnInventoryMovementProcurementTankerQry(clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")), Nothing) & ")qwer) As Tanker  ,
                    '                    (" & ReturnSaleInvoiceQry(clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")), status7) & ") AS Sale_invoice,
                    '                    (" & ReturnMilkPurchaseInvoiceQry(clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")), status8) & ") AS Milk_Purchase_invoice,
                    '(SELECT FORMAT(DATEFROMPARTS(YEAR(MAX(To_Date)), MONTH(MAX(To_Date)),1),'MMM-yyyy') AS LastDBT
                    'FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT) As DBT)final"
                    dt2 = clsDBFuncationality.GetDataTable(query)

                ElseIf clsCommon.CompairString(ddlReportType.SelectedValue, "UWASR") = CompairStringResult.Equal Then
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        query = " select  SNo,[Union Name],Fromdate,Todate,username,Milk_WeightProc,(Milk_WeightProc/DaysCount) as [Proc Avg],Prod_QTY,(Prod_QTY/DaysCount) as [Prod Avg],Purchase_Count,SRN_Count,GRN_Count,TotalLtr_ItemWiseDemand,(TotalLtr_ItemWiseDemand/DaysCount) as [Dem Avg],
    Dis_QtyInLTR,(Dis_QtyInLTR/DaysCount) as [Dis Avg],E_Invoice_Count,Sale_Voucher,Purchase_Voucher,Last_Salary,[Last DBT App. Month] from (select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],
                        '" + clsCommon.GetPrintDate(txtFromDate.Value) + "'as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "'as Todate,'" + objCommonVar.CurrentUser + "' as username,
                    ISNULL(SUM(Dis_Disbursement.Dis_QtyInLTR), 0) AS Dis_QtyInLTR,
                    ISNULL(SUM(Dis_Production.Prod_QTY), 0) AS Prod_QTY,
                    ISNULL(SUM(Dis_Demand.TotalLtr_ItemWiseDemand), 0) AS TotalLtr_ItemWiseDemand,
                    ISNULL(SUM(Dis_Procurement.Milk_WeightProc), 0) AS Milk_WeightProc,
                    ISNULL(SUM(Sale_invoice.Sale_Voucher),0) AS Sale_Voucher,
					ISNULL(SUM(Milk_Purchase_invoice.Purchase_Voucher),0) AS Purchase_Voucher,
                    (SELECT TOP 1  LEFT(DATENAME(MONTH, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PAYPERIOD_MASTER.DATE_TO),3) + ' ' + CONVERT(VARCHAR(4), YEAR([" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PAYPERIOD_MASTER.DATE_TO)) FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_GENERATE_SALARY
                left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PAYPERIOD_MASTER on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_GENERATE_SALARY.PAY_PERIOD_CODE = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE
                where 2=2 " + status6 + " ORDER BY DATE_TO DESC) as Last_Salary,(select count(PurchaseOrder_No) from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PURCHASE_ORDER_HEAD where CONVERT(DATE, PurchaseOrder_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "' " + status11 + ")   as Purchase_Count,(select count(SRN_No) from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SRN_HEAD where CONVERT(DATE, SRN_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "' " + status12 + ") as SRN_Count,(select count(GRN_No) from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_GRN_HEAD where CONVERT(DATE, GRN_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "' " + status16 + ") as GRN_Count,(select COUNT(IRN_No) from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SALE_INVOICE_HEAD where CONVERT(DATE, Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "' " + status13 + ") as E_Invoice_Count,(select FORMAT(max(To_Date),'MMM yyyy')
            FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT  where  Status=1) AS [Last DBT App. Month],(SELECT DATEDIFF(day, '" + clsCommon.GetPrintDate(txtFromDate.Value) + "', '" + clsCommon.GetPrintDate(txtToDate.Value) + "')+1) AS DaysCount
                FROM 
            (SELECT SUM(Dis_QtyInLTR) AS Dis_QtyInLTR FROM (( SELECT SUM(Dis_QtyInLTR) AS Dis_QtyInLTR from (
            select CASE when  im.Is_FreshItem =1 then convert(decimal(18,2),(isnull( sd.Qty,0)) * (isnull( [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/(I.[LTR])) ELSE convert(decimal(18,2),(isnull( sd.Qty,0)) * (isnull( [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/(I.[KG])) END  as Dis_QtyInLTR
                    FROM 
                         [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_DETAIL sd
                    LEFT JOIN 
                        [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_item_master im ON im.Item_Code = sd.Item_Code
                    LEFT JOIN 
                        [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_HEAD sh ON sh.Document_Code = sd.DOCUMENT_CODE
                    LEFT JOIN    [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL ON  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Item_Code =   sd.Item_Code and  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.UOM_Code =   sd.Unit_code
                    LEFT JOIN (  SELECT * FROM ( select item_code,uom_code,conversion_factor from    [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL ) I  PIVOT (Max(conversion_factor) FOR uom_code IN ( [LTR],[KG] )) P ) I ON   sd.ITEM_CODE = I.item_code
                    WHERE 
                        CONVERT(DATE, sh.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "' " + status1 + "
                        )xx
                        union all
                        SELECT cast(  SUM(xxxx.Qty) as decimal(18,2)) AS TotalLtr_ItemWiseDemand from (
                        SELECT case when isnull(ConvertDiv.Conversion_Factor,0)=0 then 0 else  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Qty*ConvertMul.Conversion_Factor/ConvertDiv.Conversion_Factor end as Qty ,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Unit_code
                        FROM  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale
                       LEFT JOIN  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale  ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale.Document_No = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Document_No
                     inner join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL as ConvertMul on ConvertMul.Item_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code and ConvertMul.UOM_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Unit_code 
                    inner join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL as ConvertDiv on ConvertDiv .Item_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code and ConvertDiv.UOM_Code='LTR'  

                    WHERE CONVERT(DATE, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale.Document_Date, 103)  BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                    " + status9 + "     
                    )xxxx ))Disp_BUlksale
                    ) AS Dis_Disbursement,
                            (SELECT 
                        SUM(Prod_QTY) AS Prod_QTY
                         FROM (
                            ( SELECT  SUM(Prod_QTY) AS Prod_QTY
						from (
                    select CASE when  im.Is_FreshItem =1 then convert(decimal(18,2),(isnull( ped.RECEIPT_QTY,0)) * (isnull( [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/(I.[LTR])) ELSE convert(decimal(18,2),(isnull( ped.RECEIPT_QTY,0)) * (isnull( [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/(I.[KG])) END  as Prod_QTY
                    FROM 
                        [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PP_PRODUCTION_ENTRY pe
                    LEFT JOIN 
                        [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PP_PRODUCTION_ENTRY_DETAIL ped ON ped.PROD_ENTRY_CODE = pe.PROD_ENTRY_CODE
                    LEFT JOIN 
                        [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_master im ON im.Item_Code = ped.ITEM_CODE
				    LEFT JOIN    [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL ON  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Item_Code =   ped.ITEM_CODE and  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.UOM_Code =   ped.UNIT_CODE
                    LEFT JOIN (  SELECT * FROM ( select item_code,uom_code,conversion_factor from    [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL ) I  PIVOT (Max(conversion_factor) FOR uom_code IN ( [LTR],[KG] )) P ) I ON   ped.ITEM_CODE = I.item_code
                    WHERE  CONVERT(DATE, pe.PROD_DATE, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "' " + status2 + "
						)XX
                        union all
                        select sum(Prod_QTY)Prod_QTY from (
                    select CASE when [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_master.Is_FreshItem =1 then convert(decimal(18,2),(isnull([" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCTION_UPLOADER_DETAIL.Qty,0)) * (isnull([" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/(I.[LTR])) ELSE convert(decimal(18,2),(isnull([" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCTION_UPLOADER_DETAIL.Qty,0)) * (isnull([" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/(I.[KG])) END  as Prod_QTY
                from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCTION_UPLOADER_HEAD 
                left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCTION_UPLOADER_DETAIL on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCTION_UPLOADER_DETAIL.Document_No = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCTION_UPLOADER_HEAD.Document_No
                left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_master on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_master.Item_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCTION_UPLOADER_DETAIL.Item_Code
                LEFT JOIN   [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Item_Code =  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCTION_UPLOADER_DETAIL.Item_Code and  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.UOM_Code =  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCTION_UPLOADER_DETAIL.UOM
                      LEFT JOIN (  SELECT * FROM ( select item_code,uom_code,conversion_factor from   [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL ) I  PIVOT (Max(conversion_factor) FOR uom_code IN ( [LTR],[KG] )) P ) I ON  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCTION_UPLOADER_DETAIL.Item_Code = I.item_code
                    where  CONVERT(DATE, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PRODUCTION_UPLOADER_HEAD.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'  " + status10 + "
                       ) xx
                    	   union all
					   select isnull(sum(Prod_QTY),0) as Prod_QTY	 from (
                    select CASE when [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_master.Is_FreshItem =1 then convert(decimal(18,2),(isnull([" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SHIFT_MGMT_PRODUCTION.Qty_KG,0))) end as Prod_QTY
                from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SHIFT_MGMT_PRODUCTION 

                left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_shift_mgmt on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_shift_mgmt.Document_No = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SHIFT_MGMT_PRODUCTION.Document_No
                left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_master on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_master.Item_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SHIFT_MGMT_PRODUCTION.Item_Code

                    where  CONVERT(DATE, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_shift_mgmt.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'  " + status17 + "
                       ) xx
                    Union All
                    select isnull(sum(Item_Quantity),0) as Prod_QTY	 from (
                       select TSPL_ADJUSTMENT_DETAIL.Item_Quantity,TSPL_ADJUSTMENT_DETAIL.FAT_KG,TSPL_ADJUSTMENT_DETAIL.SNF_KG from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ADJUSTMENT_DETAIL
Left Join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ADJUSTMENT_HEADER On TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No
Left Join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code=TSPL_ADJUSTMENT_DETAIL.Item_Code
Where TSPL_ITEM_MASTER.Item_Type='F' And TSPL_ADJUSTMENT_HEADER.Trans_Type='IN'
And Convert(Date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "')Adjsment
                  ))Prod_Uploder
                    ) AS Dis_Production,
                        (SELECT  SUM(TotalLtr_ItemWiseDemand) AS TotalLtr_ItemWiseDemand FROM (
                    (SELECT SUM(TotalLtr_ItemWiseDemand) AS TotalLtr_ItemWiseDemand
                    FROM (select  CASE when  im.Is_FreshItem =1 then convert(decimal(18,2),(isnull( TotalLtr_ItemWise,0)) ) ELSE convert(decimal(18,2),(isnull( Qty,0)) * (isnull( [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/(I.[KG])) END  as TotalLtr_ItemWiseDemand
					from 
                        [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL dbd
                    LEFT JOIN 
                        [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_master dbm ON dbm.Document_No = dbd.Document_No
                    LEFT JOIN 
                       [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER im ON im.Item_Code = dbd.Item_Code
					LEFT JOIN    [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL ON  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Item_Code =   dbd.Item_Code and   [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.UOM_Code =   dbd.Unit_code
                    LEFT JOIN (  SELECT * FROM ( select item_code,uom_code,conversion_factor from    [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL ) I  PIVOT (Max(conversion_factor) FOR uom_code IN ( [KG] )) P ) I ON   dbd.Item_Code = I.item_code
                    WHERE 
                        CONVERT(DATE, dbm.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'  " + status3 + ")xx
                        union all
                        SELECT 
                      cast(  SUM(xxxx.Qty) as decimal(18,2)) AS TotalLtr_ItemWiseDemand
                    from (
                    SELECT case when isnull(ConvertDiv.Conversion_Factor,0)=0 then 0 else  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Qty*ConvertMul.Conversion_Factor/ConvertDiv.Conversion_Factor end as Qty ,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Unit_code
                    FROM  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale
                    LEFT JOIN  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale  ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale.Document_No = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Document_No
                    inner join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL as ConvertMul on ConvertMul.Item_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code and ConvertMul.UOM_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Unit_code 
                    inner join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL as ConvertDiv on ConvertDiv .Item_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code and ConvertDiv.UOM_Code='LTR'  

                    WHERE CONVERT(DATE, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale.Document_Date, 103)  BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                       " + status9 + "                
                    )xxxx ) 
                 union all
                    SELECT SUM(TotalLtr_ItemWiseDemand) AS TotalLtr_ItemWiseDemand
                        from (
                select CASE when  tspl_item_master.Is_FreshItem =1 then convert(decimal(18,2),(isnull( [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_DETAIL.Booking_Qty,0)) * (isnull( [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/(I.[LTR])) ELSE convert(decimal(18,2),(isnull( [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_DETAIL.Booking_Qty,0)) * (isnull( TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/(I.[KG])) END  as TotalLtr_ItemWiseDemand
                    FROM 
                         [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_DETAIL 
					LEFT JOIN 
                        [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER  ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER.Document_No = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_DETAIL.Document_No
                    LEFT JOIN 
                        [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_item_master  ON tspl_item_master.Item_Code = TSPL_BOOKING_DETAIL.Item_Code
                    LEFT JOIN  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL ON  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Item_Code =   TSPL_BOOKING_DETAIL.Item_Code and  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.UOM_Code =   TSPL_BOOKING_DETAIL.Unit_code
                    LEFT JOIN (  SELECT * FROM ( select item_code,uom_code,conversion_factor from    [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL ) I  PIVOT (Max(conversion_factor) FOR uom_code IN ( [LTR],[KG] )) P ) I ON   TSPL_BOOKING_DETAIL.ITEM_CODE = I.item_code
                    WHERE 
                        CONVERT(DATE, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "' " + status15 + "
                        )xx)Disp_BUlksale
                    ) AS Dis_Demand,
                    (SELECT 
                        SUM(milk_weight) AS Milk_WeightProc
                    FROM (
                        SELECT 
                            SUM(Milk_Weight) AS Milk_Weight
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL mud
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD muh ON muh.Document_No = mud.Document_No
                        WHERE 
                            CONVERT(DATE, muh.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                             " + status4 + "
                        UNION ALL
                        SELECT 
                            SUM(Milk_Weight) AS Milk_Weight
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_DETAIL msd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_HEAD msh ON msh.Document_No = msd.Document_No
                        WHERE 
                            CONVERT(DATE, msh.Shift_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                              " + status5 + "
                        UNION ALL
                        SELECT 
                            ISNULL(SUM(QTY),0) AS QTY
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_DETAIL mcd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS mcs ON mcs.Document_No = mcd.Document_No
                        WHERE 
                           mcs.Status = 0
                           AND
                            CONVERT(DATE, mcs.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            ) AS Procurement
                    ) AS Dis_Procurement,
                    (select sum([" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SALE_INVOICE_DETAIL.Total_Basic_Amt) as Sale_Voucher from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SALE_INVOICE_DETAIL
                    left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SALE_INVOICE_HEAD on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SALE_INVOICE_HEAD.Document_Code
                    where  CONVERT(DATE, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SALE_INVOICE_HEAD.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'  " + status7 + " ) AS Sale_invoice,
                    (select SUM(TOTAL_AMOUNT) AS Purchase_Voucher from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PURCHASE_INVOICE_HEAD where  CONVERT(DATE, DOC_DATE, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "' " + status8 + "  ) AS Milk_Purchase_invoice)final "
                        dtUnion = clsDBFuncationality.GetDataTable(query)
                        dt2.Merge(dtUnion)
                    Next
                End If
                'dtUnion = clsDBFuncationality.GetDataTable(query)
                'dt2.Merge(dtUnion)

            End If
            If dt2 IsNot Nothing OrElse dt2.Rows.Count > 0 Then

                gv1.DataSource = Nothing
                gv1.Rows.Clear()
                gv1.Columns.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.MasterView.Refresh()
                gv1.DataSource = dt2
                For ii As Integer = 0 To gv1.Columns.Count - 1
                    gv1.Columns(ii).ReadOnly = True
                Next
                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.EnableFiltering = True
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                SetGridFormat1()
                gv1.BestFitColumns()
                If print Then
                    If clsCommon.CompairString(ddlReportType.SelectedValue, "UWASR") = CompairStringResult.Equal Then
                        Dim frmCRV As New frmCrystalReportViewer()
                        frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.CommonForUnionAndCattlefeed, dt2, "crptmilkunionAvgreport", "") ''report for both (RCDF And RCDFCF)
                    Else
                        Dim frmCRV As New frmCrystalReportViewer()
                        frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.CommonForUnionAndCattlefeed, dt2, "crptmilkunionreport", "") ''report for both (RCDF And RCDFCF)
                    End If

                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

            ' ReStoreGridLayout()
            isSummary = False
        Catch ex As Exception
            isSummary = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Function ReturnShipDispatchQry(ByVal strUnion As String, ByVal status As String) As String
        Dim Qry As String = " select "
        If Not isSummary Then
            Qry &= " sh.Trans_Type,sh.Screen_Type,sh.DOCUMENT_CODE,Convert(VArchar(10),sh.Document_Date,103) As Document_Date,"
            status = "'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyy") & "' And '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyy") & "' " & status
        Else
            strUnion = "'+" & strUnion & "+'"
        End If
        Qry &= " CASE when im.Is_FreshItem =1 then convert(decimal(18,2),(isnull( sd.Qty,0)) * (isnull( TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/(I.[LTR])) ELSE convert(decimal(18,2),(isnull( sd.Qty,0)) * (isnull( TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/(I.[KG])) END as Dis_QtyInLTR,im.STD_FatPer,im.STD_SNFPer 
FROM " & strUnion & ".[dbo].TSPL_SD_SHIPMENT_DETAIL sd 
LEFT JOIN " & strUnion & ".[dbo].TSPL_SD_SHIPMENT_HEAD sh ON sh.Document_Code = sd.DOCUMENT_CODE 
LEFT JOIN " & strUnion & ".[dbo].tspl_item_master im ON im.Item_Code = sd.Item_Code 
LEFT JOIN " & strUnion & ".[dbo].TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.Item_Code = sd.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code = sd.Unit_code 
LEFT JOIN ( SELECT * FROM ( select item_code,uom_code,conversion_factor from " & strUnion & ".[dbo].TSPL_ITEM_UOM_DETAIL ) I PIVOT (Max(conversion_factor) FOR uom_code IN ( [LTR],[KG] )) P ) I ON sd.ITEM_CODE = I.item_code WHERE CONVERT(DATE, sh.Document_Date, 103) Between   " & status
        Return Qry
    End Function

    Function ReturnBulkDispatchQry(ByVal strUnion As String, ByVal status As String) As String
        Dim Qry As String = "SELECT "
        If Not isSummary Then
            Qry &= " '' As Trans_Type,'' As Screen_Type,TSPL_Dispatch_BulkSale.Document_No As DOCUMENT_CODE,Convert(Varchar(10),TSPL_Dispatch_BulkSale.Document_Date,103)Document_Date,"
            status = "'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyy") & "' And '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyy") & "' " & status
        Else
            strUnion = "'+" & strUnion & "+'"
        End If
        Qry &= " case when isnull(ConvertDiv.Conversion_Factor,0)=0 then 0 else TSPL_Dispatch_Detail_BulkSale.Qty*ConvertMul.Conversion_Factor/ConvertDiv.Conversion_Factor end as Dis_QtyInLTR ,TSPL_Dispatch_Detail_BulkSale.FatPer As STD_FatPer ,TSPL_Dispatch_Detail_BulkSale.SNFPer As STD_SNFPer
FROM " & strUnion & ".[dbo].TSPL_Dispatch_Detail_BulkSale 
LEFT JOIN " & strUnion & ".[dbo].TSPL_Dispatch_BulkSale ON TSPL_Dispatch_BulkSale.Document_No = TSPL_Dispatch_Detail_BulkSale.Document_No 
inner join " & strUnion & ".[dbo].TSPL_ITEM_UOM_DETAIL as ConvertMul on ConvertMul.Item_Code=TSPL_Dispatch_Detail_BulkSale.Item_Code and ConvertMul.UOM_Code=TSPL_Dispatch_Detail_BulkSale.Unit_code 
inner join " & strUnion & ".[dbo].TSPL_ITEM_UOM_DETAIL as ConvertDiv on ConvertDiv.Item_Code=TSPL_Dispatch_Detail_BulkSale.Item_Code "
        If isSummary Then
            Qry += " and ConvertDiv.UOM_Code=''LTR''"
        Else
            Qry += " and ConvertDiv.UOM_Code='LTR'"
        End If
        Qry &= " WHERE CONVERT(DATE,TSPL_Dispatch_BulkSale.Document_Date, 103) BETWEEN   " & status
        Return Qry
    End Function

    Function ReturnPPProdProductionQry(ByVal strUnion As String, ByVal status As String) As String
        Dim Qry As String = "select "
        If Not isSummary Then
            Qry &= " '' As Trans_Type,'' As Screen_Type,pe.PROD_ENTRY_CODE As Document_No,Convert(Varchar(10),pe.PROD_DATE,103)Document_DATE,"
            status = "'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyy") & "' And '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyy") & "' " & status
        Else
            strUnion = "'+" & strUnion & "+'"
        End If
        Qry &= " CASE when im.Is_FreshItem =1 then convert(decimal(18,2),(isnull( ped.RECEIPT_QTY,0)) * (isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/(I.[LTR])) ELSE convert(decimal(18,2),(isnull( ped.RECEIPT_QTY,0)) * (isnull( TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/(I.[KG])) END as Prod_QTY,im.STD_FatPer,im.STD_SNFPer 
FROM " & strUnion & ".[dbo].TSPL_PP_PRODUCTION_ENTRY pe 
LEFT JOIN " & strUnion & ".[dbo].TSPL_PP_PRODUCTION_ENTRY_DETAIL ped ON ped.PROD_ENTRY_CODE = pe.PROD_ENTRY_CODE 
LEFT JOIN " & strUnion & ".[dbo].TSPL_ITEM_master im ON im.Item_Code = ped.ITEM_CODE 
LEFT JOIN " & strUnion & ".[dbo].TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.Item_Code = ped.ITEM_CODE and TSPL_ITEM_UOM_DETAIL.UOM_Code = ped.UNIT_CODE 
LEFT JOIN ( SELECT * FROM ( select item_code,uom_code,conversion_factor from " & strUnion & ".[dbo].TSPL_ITEM_UOM_DETAIL ) I PIVOT (Max(conversion_factor) FOR uom_code IN ( [LTR],[KG] )) P ) I ON ped.ITEM_CODE = I.item_code WHERE CONVERT(DATE, pe.PROD_DATE, 103) BETWEEN   " & status
        Return Qry
    End Function

    Function ReturnProdUPProductionQry(ByVal strUnion As String, ByVal status As String) As String
        Dim Qry As String = "select "
        If Not isSummary Then
            Qry &= "'' As Trans_Type,'' As Screen_Type,TSPL_PRODUCTION_UPLOADER_HEAD.Document_No,Convert(Varchar(10),TSPL_PRODUCTION_UPLOADER_HEAD.Document_Date,103)Document_Date,"
            status = "'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyy") & "' And '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyy") & "' " & status
        Else
            strUnion = "'+" & strUnion & "+'"
        End If
        Qry &= " CASE when TSPL_ITEM_master.Is_FreshItem =1 then convert(decimal(18,2),(isnull(TSPL_PRODUCTION_UPLOADER_DETAIL.Qty,0)) * (isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/(I.[LTR])) ELSE convert(decimal(18,2),(isnull(TSPL_PRODUCTION_UPLOADER_DETAIL.Qty,0)) * (isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/(I.[KG])) END as Prod_QTY,tspl_item_master.STD_FatPer,tspl_item_master.STD_SNFPer 
from " & strUnion & ".[dbo].TSPL_PRODUCTION_UPLOADER_HEAD 
left join " & strUnion & ".[dbo].TSPL_PRODUCTION_UPLOADER_DETAIL on TSPL_PRODUCTION_UPLOADER_DETAIL.Document_No = TSPL_PRODUCTION_UPLOADER_HEAD.Document_No 
left join " & strUnion & ".[dbo].TSPL_ITEM_master on TSPL_ITEM_master.Item_Code = TSPL_PRODUCTION_UPLOADER_DETAIL.Item_Code 
LEFT JOIN " & strUnion & ".[dbo].TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_PRODUCTION_UPLOADER_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code = TSPL_PRODUCTION_UPLOADER_DETAIL.UOM 
LEFT JOIN ( SELECT * FROM ( select item_code,uom_code,conversion_factor from " & strUnion & ".[dbo].TSPL_ITEM_UOM_DETAIL ) I PIVOT (Max(conversion_factor) FOR uom_code IN ( [LTR],[KG] )) P ) I ON TSPL_PRODUCTION_UPLOADER_DETAIL.Item_Code = I.item_code where CONVERT(DATE, TSPL_PRODUCTION_UPLOADER_HEAD.Document_Date, 103) BETWEEN   " & status
        Return Qry
    End Function

    Function ReturnShiftGMTProdcutionQry(ByVal strUnion As String, ByVal status As String) As String
        Dim Qry As String = "select "
        If Not isSummary Then
            Qry &= " '' As Trans_Type,'' As Screen_Type,tspl_shift_mgmt.Document_No,Convert(Varchar(10),tspl_shift_mgmt.Document_Date,103)Document_Date, "
            status = "'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyy") & "' And '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyy") & "' " & status
        Else
            strUnion = "'+" & strUnion & "+'"
        End If
        Qry &= " CASE when TSPL_ITEM_master.Is_FreshItem =1 then convert(decimal(18,2),(isnull(TSPL_SHIFT_MGMT_PRODUCTION.Qty_KG,0))) end as Prod_QTY,tspl_item_master.STD_FatPer,tspl_item_master.STD_SNFPer 
from " & strUnion & ".[dbo].TSPL_SHIFT_MGMT_PRODUCTION 
left join " & strUnion & ".[dbo].tspl_shift_mgmt on tspl_shift_mgmt.Document_No = TSPL_SHIFT_MGMT_PRODUCTION.Document_No 
left join " & strUnion & ".[dbo].TSPL_ITEM_master on TSPL_ITEM_master.Item_Code = TSPL_SHIFT_MGMT_PRODUCTION.Item_Code 
where CONVERT(DATE, tspl_shift_mgmt.Document_Date, 103) BETWEEN   " & status
        Return Qry
    End Function

    Function ReturnAdjstProductionQry(ByVal strUnion As String, ByVal status As String) As String
        Dim Qry As String = " select "
        If Not isSummary Then
            Qry &= " '' As Trans_Type,'' As Screen_Type,TSPL_ADJUSTMENT_HEADER.Adjustment_No As Document_No,Convert(Varchar(10),TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103)Document_Date, "
            status = "'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyy") & "' And '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyy") & "' " & status
        Else
            strUnion = "'+" & strUnion & "+'"
        End If
        Qry &= " TSPL_ADJUSTMENT_DETAIL.Item_Quantity as Prod_QTY,TSPL_ADJUSTMENT_DETAIL.FAT_Pers As STD_FatPer,TSPL_ADJUSTMENT_DETAIL.SNF_Pers As STD_SNFPer 
from " & strUnion & ".[dbo].TSPL_ADJUSTMENT_DETAIL 
Left Join " & strUnion & ".[dbo].TSPL_ADJUSTMENT_HEADER On TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No 
Left Join " & strUnion & ".[dbo].TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code=TSPL_ADJUSTMENT_DETAIL.Item_Code Where  Convert(Date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) BETWEEN   " & status
        If isSummary Then
            Qry &= " And TSPL_ITEM_MASTER.Item_Type=''F'' And TSPL_ADJUSTMENT_HEADER.Trans_Type=''IN''"
        Else
            Qry &= " And TSPL_ITEM_MASTER.Item_Type='F' And TSPL_ADJUSTMENT_HEADER.Trans_Type='IN'"
        End If
        Return Qry
    End Function
    Function ReturnDemandBookingDemandQry(ByVal strUnion As String, ByVal status As String) As String
        Dim Qry As String = "select "
        If Not isSummary Then
            Qry &= " '' As Trans_Type,'' As Screen_Type,dbm.Document_No,Convert(Varchar(10),dbm.Document_Date,103)Document_Date,"
            status = "'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyy") & "' And '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyy") & "' " & status
        Else
            strUnion = "'+" & strUnion & "+'"
        End If
        Qry &= " CASE when im.Is_FreshItem =1 then convert(decimal(18,2),(isnull( TotalLtr_ItemWise,0)) ) ELSE convert(decimal(18,2),(isnull( Qty,0)) * (isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/(I.[KG])) END as TotalLtr_ItemWiseDemand,im.STD_FatPer,im.STD_SNFPer 
from " & strUnion & ".[dbo].TSPL_DEMAND_BOOKING_DETAIL dbd 
LEFT JOIN " & strUnion & ".[dbo].TSPL_DEMAND_BOOKING_master dbm ON dbm.Document_No = dbd.Document_No 
LEFT JOIN " & strUnion & ".[dbo].TSPL_ITEM_MASTER im ON im.Item_Code = dbd.Item_Code 
LEFT JOIN " & strUnion & ".[dbo].TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.Item_Code = dbd.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code = dbd.Unit_code 
LEFT JOIN ( SELECT * FROM ( select item_code,uom_code,conversion_factor from " & strUnion & ".[dbo].TSPL_ITEM_UOM_DETAIL ) I PIVOT (Max(conversion_factor) FOR uom_code IN ( [KG] )) P ) I ON dbd.Item_Code = I.item_code
WHERE CONVERT(DATE, dbm.Document_Date, 103) BETWEEN   " & status
        Return Qry
    End Function

    Function ReturnDisBulkDemandQry(ByVal strUnion As String, ByVal status As String) As String
        Dim Qry As String = "SELECT "
        If Not isSummary Then
            Qry &= " '' As Trans_Type,'' As Screen_Type,TSPL_Dispatch_BulkSale.Document_No,Convert(Varchar(10),TSPL_Dispatch_BulkSale.Document_Date,103)Document_Date,"
            status = "'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyy") & "' And '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyy") & "' " & status
        Else
            strUnion = "'+" & strUnion & "+'"
        End If
        Qry &= " case when isnull(ConvertDiv.Conversion_Factor,0)=0 then 0 else TSPL_Dispatch_Detail_BulkSale.Qty*ConvertMul.Conversion_Factor/ConvertDiv.Conversion_Factor end as TotalLtr_ItemWiseDemand ,TSPL_Dispatch_Detail_BulkSale.FATPer As STD_FatPer ,TSPL_Dispatch_Detail_BulkSale.SNFPer As STD_SNFPer 
FROM " & strUnion & ".[dbo].TSPL_Dispatch_Detail_BulkSale 
LEFT JOIN " & strUnion & ".[dbo].TSPL_Dispatch_BulkSale ON TSPL_Dispatch_BulkSale.Document_No = TSPL_Dispatch_Detail_BulkSale.Document_No 
inner join " & strUnion & ".[dbo].TSPL_ITEM_UOM_DETAIL as ConvertMul on ConvertMul.Item_Code=TSPL_Dispatch_Detail_BulkSale.Item_Code and ConvertMul.UOM_Code=TSPL_Dispatch_Detail_BulkSale.Unit_code 
inner join " & strUnion & ".[dbo].TSPL_ITEM_UOM_DETAIL as ConvertDiv on ConvertDiv .Item_Code=TSPL_Dispatch_Detail_BulkSale.Item_Code "
        If isSummary Then
            Qry &= " and ConvertDiv.UOM_Code=''LTR'' "
        Else
            Qry &= " and ConvertDiv.UOM_Code='LTR' "
        End If
        Qry &= " WHERE CONVERT(DATE, TSPL_Dispatch_BulkSale.Document_Date, 103) BETWEEN   " & status
        Return Qry
    End Function

    Function ReturnBookingDemandQry(ByVal strUnion As String, ByVal status As String) As String
        Dim Qry As String = " select "
        If Not isSummary Then
            Qry &= " '' As Trans_Type,TSPL_BOOKING_MATSER.From_Screen_Code As Screen_Type,TSPL_BOOKING_MATSER.Document_No,Convert(Varchar(10),TSPL_BOOKING_MATSER.Document_Date,103)Document_Date,"
            status = "'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyy") & "' And '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyy") & "' " & status
        Else
            strUnion = "'+" & strUnion & "+'"
        End If
        Qry &= " CASE when tspl_item_master.Is_FreshItem =1 then convert(decimal(18,2),(isnull(TSPL_BOOKING_DETAIL.Booking_Qty,0)) * (isnull( TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/(I.[LTR])) ELSE convert(decimal(18,2),(isnull(TSPL_BOOKING_DETAIL.Booking_Qty,0)) * (isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/(I.[KG])) END as TotalLtr_ItemWiseDemand,tspl_item_master.STD_FatPer,tspl_item_master.STD_SNFPer 
FROM " & strUnion & ".[dbo].TSPL_BOOKING_DETAIL 
LEFT JOIN " & strUnion & ".[dbo].TSPL_BOOKING_MATSER ON TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No 
LEFT JOIN " & strUnion & ".[dbo].tspl_item_master ON tspl_item_master.Item_Code = TSPL_BOOKING_DETAIL.Item_Code 
LEFT JOIN " & strUnion & ".[dbo].TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_BOOKING_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code = TSPL_BOOKING_DETAIL.Unit_code 
LEFT JOIN ( SELECT * FROM ( select item_code,uom_code,conversion_factor from " & strUnion & ".[dbo].TSPL_ITEM_UOM_DETAIL ) I PIVOT (Max(conversion_factor) FOR uom_code IN ( [LTR],[KG] )) P ) I ON TSPL_BOOKING_DETAIL.ITEM_CODE = I.item_code WHERE CONVERT(DATE,TSPL_BOOKING_MATSER.Document_Date, 103) BETWEEN   " & status
        Return Qry
    End Function

    Function ReturnDCSSaleEntryDemandQry(ByVal strUnion As String, ByVal status As String) As String
        Dim Qry As String = "Select "
        If Not isSummary Then
            Qry &= " Trans_Type,'' As Screen_Type,TSPL_DCS_SALE_ENTRY.Document_Code As Document_No,Convert(Varchar(10),TSPL_DCS_SALE_ENTRY.Document_Date,103)Document_Date,"
            status = "'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyy") & "' And '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyy") & "' " & status
        Else
            strUnion = "'+" & strUnion & "+'"
        End If
        Qry &= " TSPL_DCS_SALE_ENTRY_DETAIL.Qty As TotalLtr_ItemWiseDemand,tspl_item_master.STD_FatPer,STD_SNFPer from " & strUnion & ".[dbo].TSPL_DCS_SALE_ENTRY_DETAIL 
Left Join " & strUnion & ".[dbo].TSPL_DCS_SALE_ENTRY On TSPL_DCS_SALE_ENTRY.Document_Code=TSPL_DCS_SALE_ENTRY_DETAIL.DOCUMENT_CODE 
Left Join " & strUnion & ".[dbo].tspl_item_master On tspl_item_master.Item_Code=TSPL_DCS_SALE_ENTRY_DETAIL.Item_Code 
Where CONVERT(DATE,TSPL_DCS_SALE_ENTRY.Document_Date, 103) BETWEEN   " & status
        Return Qry
    End Function

    Function ReturnProdDemandDemandQry(ByVal strUnion As String, ByVal status As String) As String
        Dim Qry As String = "Select "
        If Not isSummary Then
            Qry &= " '' As Trans_Type,'' As Screen_Type,TSPL_PRODUCT_DEMAND_BOOKING_MASTER.Document_No,Convert(Varchar(10),TSPL_PRODUCT_DEMAND_BOOKING_MASTER.Document_Date,103)Document_Date,"
            status = "'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyy") & "' And '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyy") & "' " & status
        Else
            strUnion = "'+" & strUnion & "+'"
        End If
        Qry &= " TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Qty As TotalLtr_ItemWiseDemand,tspl_item_master.STD_FatPer,STD_SNFPer 
from " & strUnion & ".[dbo].TSPL_PRODUCT_DEMAND_BOOKING_DETAIL 
Left Join " & strUnion & ".[dbo].TSPL_PRODUCT_DEMAND_BOOKING_MASTER On TSPL_PRODUCT_DEMAND_BOOKING_MASTER.Document_No=TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Document_No 
Left Join " & strUnion & ".[dbo].tspl_item_master On tspl_item_master.Item_Code=TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Item_Code 
Where CONVERT(DATE,TSPL_PRODUCT_DEMAND_BOOKING_MASTER.Document_Date, 103) BETWEEN   " & status
        Return Qry
    End Function

    Function ReturnMPUDProcurementQry(ByVal strUnion As String, ByVal status As String) As String
        Dim Qry As String = "Select "
        If Not isSummary Then
            Qry &= " '' As Trans_Type,'' As Screen_Type,muh.Document_No,Convert(Varchar(10),(muh.Document_Date),103)Document_Date,"
            status = "'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyy") & "' And '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyy") & "' " & status
        Else
            strUnion = "'+" & strUnion & "+'"
        End If
        Qry &= " (Milk_Weight) AS Milk_Weight, FAT, SNF
FROM " & strUnion & ".[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL mud 
LEFT JOIN " & strUnion & ".[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD muh ON muh.Document_No = mud.Document_No
WHERE CONVERT(DATE, muh.Document_Date, 103)BETWEEN   " & status
        Return Qry
    End Function

    Function ReturnMilkShiftUploaderProcurementQry(ByVal strUnion As String, ByVal status As String) As String
        Dim Qry As String = "SELECT "
        If Not isSummary Then
            Qry &= " '' As Trans_Type,'' As Screen_Type,msh.Document_No,Convert(Varchar(10),(msh.Shift_Date),103)Document_Date,"
            status = "'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyy") & "' And '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyy") & "'" & status
        Else
            strUnion = "'+" & strUnion & "+'"
        End If
        Qry &= " (Milk_Weight) AS Milk_Weight,FAT,SNF 
        FROM " & strUnion & ".[dbo].TSPL_MILK_SHIFT_UPLOADER_DETAIL msd 
LEFT JOIN " & strUnion & ".[dbo].TSPL_MILK_SHIFT_UPLOADER_HEAD msh ON msh.Document_No = msd.Document_No 
WHERE CONVERT(DATE, msh.Shift_Date, 103) BETWEEN   " & status
        Return Qry
    End Function

    Function ReturnMilkCollectionDCSProcurementQry(ByVal strUnion As String, ByVal status As String) As String
        Dim Qry As String = "SELECT"
        If Not isSummary Then
            Qry &= " '' As Trans_Type,'' As Screen_Type,mcs.Document_No,Convert(Varchar(10),(mcs.document_date),103)Document_Date,"
            status = "'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyy") & "' And '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyy") & "'" & status
        Else
            strUnion = "'+" & strUnion & "+'"
        End If
        Qry &= " ISNULL((QTY), 0) As Milk_Weight,FAT,SNF
FROM " & strUnion & ".[dbo].TSPL_MILK_COLLECTION_DCS_DETAIL mcd 
LEFT JOIN " & strUnion & ".[dbo].TSPL_MILK_COLLECTION_DCS mcs ON mcs.Document_No = mcd.Document_No WHERE mcs.Status = 0 AND CONVERT(DATE, mcs.Document_Date, 103) BETWEEN   " & status
        Return Qry
    End Function

    Function ReturnMilkCollectionBMCDCSProcurementQry(ByVal strUnion As String, ByVal status As String) As String
        Dim Qry As String = "Select "
        If Not isSummary Then
            Qry &= " '' As Trans_Type,'' As Screen_Type,'' As Document_No,Convert(Varchar(10),TSPL_MILK_COLLECTION_BMCDCS.IDate,103)Document_Date,"
            status = "'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyy") & "' And '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyy") & "'" & status
        Else
            strUnion = "'+" & strUnion & "+'"
        End If
        Qry &= "IsNull((TSPL_MILK_COLLECTION_BMCDCS_DCS.Qty),0)Milk_Weight,TSPL_MILK_COLLECTION_BMCDCS_DCS.FAT,TSPL_MILK_COLLECTION_BMCDCS_DCS.SNF
from " & strUnion & ".[dbo].TSPL_MILK_COLLECTION_BMCDCS_DCS 
Left Join " & strUnion & ".[dbo].TSPL_MILK_COLLECTION_BMCDCS On TSPL_MILK_COLLECTION_BMCDCS.PK_ID=TSPL_MILK_COLLECTION_BMCDCS_DCS.REF_PK_ID 
Left Join " & strUnion & ".[dbo].TSPL_MILK_COLLECTION_DCS_detail On TSPL_MILK_COLLECTION_DCS_detail.PK_Id=TSPL_MILK_COLLECTION_BMCDCS.PK_ID 
Left Join " & strUnion & ".[dbo].TSPL_MILK_COLLECTION_DCS On TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_detail.document_no 
Where IsNull(TSPL_MILK_COLLECTION_BMCDCS.PK_ID,0) = 0 And CONVERT(DATE, TSPL_MILK_COLLECTION_BMCDCS.IDate, 103) BETWEEN   " & status
        Return Qry
    End Function

    Function ReturnInventoryMovementProcurementTankerQry(ByVal strUnion As String, ByVal status As String) As String
        Dim Qry As String = "select "
        If Not isSummary Then
            Qry &= "TSPL_INVENTORY_MOVEMENT_new.Trans_Type,'' As Screen_Type,TSPL_INVENTORY_MOVEMENT_new.Source_Doc_No As Document_No,Convert(Varchar(10),TSPL_INVENTORY_MOVEMENT_new.Source_Doc_Date,103)Document_Date,"
            status = "'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyy") & "' And '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyy") & "'" & status
        Else
            strUnion = "'+" & strUnion & "+'"
        End If
        Qry &= " Qty,Fat_Per,Fat_KG,SNF_Per,SNF_KG 
from " & strUnion & ".[dbo].TSPL_INVENTORY_MOVEMENT_new 
Where  CONVERT(DATE, TSPL_INVENTORY_MOVEMENT_new.Entry_Date, 103) BETWEEN   " & status
        If isSummary Then
            Qry &= " And Trans_Type In (''MilkTransferIn'',''MCC-MSRN'') "
        Else
            Qry &= " And Trans_Type In ('MilkTransferIn','MCC-MSRN') "
        End If
        Return Qry
    End Function

    Function ReturnSaleInvoiceQry(ByVal strUnion As String, ByVal status As String) As String
        Dim qry As String = "select "
        If Not isSummary Then
            qry &= " Max(TSPL_SD_SALE_INVOICE_HEAD.Trans_Type)Trans_Type,Max(TSPL_SD_SALE_INVOICE_HEAD.Screen_Type)Screen_Type,TSPL_SD_SALE_INVOICE_HEAD.document_code As Document_No,Convert(Varchar(10),Max(TSPL_SD_SALE_INVOICE_HEAD.Document_Date),103)Document_Date,"
            status = "'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyy") & "' And '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyy") & "'" & status
        Else
            strUnion = "'+" & strUnion & "+'"
        End If
        qry &= " sum(TSPL_SD_SALE_INVOICE_DETAIL.Total_Basic_Amt) as Sale_Voucher,0 As  Purchase_Voucher 
from " & strUnion & ".[dbo].TSPL_SD_SALE_INVOICE_DETAIL 
left outer join " & strUnion & ".[dbo].TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE = TSPL_SD_SALE_INVOICE_HEAD.Document_Code 
where CONVERT(DATE,TSPL_SD_SALE_INVOICE_HEAD.Document_Date, 103) BETWEEN   " & status
        If Not isSummary Then
            qry &= " Group By TSPL_SD_SALE_INVOICE_HEAD.document_code"
        End If
        Return qry
    End Function

    Function ReturnMilkPurchaseInvoiceQry(ByVal strUnion As String, ByVal status As String) As String
        Dim Qry As String = "select "
        If Not isSummary Then
            Qry &= " '' As Trans_Type,'' As Screen_Type,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE As Document_No,Convert(Varchar(10),Max(TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE),103)Document_Date,"
            status = "'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyy") & "' And '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyy") & "'" & status
        Else
            strUnion = "'+" & strUnion & "+'"
        End If
        Qry &= " 0 As Sale_Voucher,SUM(TOTAL_AMOUNT) AS Purchase_Voucher from " & strUnion & ".[dbo].TSPL_MILK_PURCHASE_INVOICE_HEAD where CONVERT(DATE, DOC_DATE, 103) BETWEEN   " & status & " "
        If Not isSummary Then
            Qry &= " Group By TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE "
        End If
        Return Qry
    End Function



    '    Function ReturnShipDispatchQry(ByVal strUnion As String, ByVal status As String) As String
    '        Dim Qry As String = " select sh.Trans_Type,sh.Screen_Type,sh.DOCUMENT_CODE,Convert(VArchar(10),sh.Document_Date,103) As Document_Date,CASE when  im.Is_FreshItem =1 then convert(decimal(18,2),(isnull( sd.Qty,0)) * (isnull( TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/(I.[LTR])) ELSE convert(decimal(18,2),(isnull( sd.Qty,0)) * (isnull( TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/(I.[KG])) END  as Dis_QtyInLTR,im.STD_FatPer,im.STD_SNFPer
    '                    FROM [" & strUnion & "].[dbo].TSPL_SD_SHIPMENT_DETAIL sd
    '                    LEFT JOIN [" & strUnion & "].[dbo].tspl_item_master im ON im.Item_Code = sd.Item_Code
    '                    LEFT JOIN [" & strUnion & "].[dbo].TSPL_SD_SHIPMENT_HEAD sh ON sh.Document_Code = sd.DOCUMENT_CODE
    '                    LEFT JOIN [" & strUnion & "].[dbo].TSPL_ITEM_UOM_DETAIL ON  [" & strUnion & "].[dbo].TSPL_ITEM_UOM_DETAIL.Item_Code =   sd.Item_Code and  [" & strUnion & "].[dbo].TSPL_ITEM_UOM_DETAIL.UOM_Code =   sd.Unit_code
    '                    LEFT JOIN (  SELECT * FROM ( select item_code,uom_code,conversion_factor from    [" & strUnion & "].[dbo].TSPL_ITEM_UOM_DETAIL ) I  PIVOT (Max(conversion_factor) FOR uom_code IN ( [LTR],[KG] )) P ) I ON   sd.ITEM_CODE = I.item_code
    '                    WHERE CONVERT(DATE, sh.Document_Date, 103) Between " & status
    '        Return Qry
    '    End Function

    '    Function ReturnBulkDispatchQry(ByVal strUnion As String, ByVal status As String) As String
    '        Dim Qry As String = "SELECT '' As Trans_Type,'' As Screen_Type,TSPL_Dispatch_BulkSale.Document_No As DOCUMENT_CODE,Convert(Varchar(10),TSPL_Dispatch_BulkSale.Document_Date,103)Document_Date,case when isnull(ConvertDiv.Conversion_Factor,0)=0 then 0 else  [" & strUnion & "].[dbo].TSPL_Dispatch_Detail_BulkSale.Qty*ConvertMul.Conversion_Factor/ConvertDiv.Conversion_Factor end as Dis_QtyInLTR ,im.STD_FatPer,im.STD_SNFPer
    '                FROM  [" & strUnion & "].[dbo].TSPL_Dispatch_Detail_BulkSale
    '                LEFT JOIN  [" & strUnion & "].[dbo].TSPL_Dispatch_BulkSale  ON [" & strUnion & "].[dbo].TSPL_Dispatch_BulkSale.Document_No = [" & strUnion & "].[dbo].TSPL_Dispatch_Detail_BulkSale.Document_No
    '                LEFT JOIN [" & strUnion & "].[dbo].tspl_item_master im ON im.Item_Code = TSPL_Dispatch_Detail_BulkSale.Item_Code
    '                inner join [" & strUnion & "].[dbo].TSPL_ITEM_UOM_DETAIL as ConvertMul on ConvertMul.Item_Code=[" & strUnion & "].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code and ConvertMul.UOM_Code=[" & strUnion & "].[dbo].TSPL_Dispatch_Detail_BulkSale.Unit_code 
    '                inner join [" & strUnion & "].[dbo].TSPL_ITEM_UOM_DETAIL as ConvertDiv on ConvertDiv .Item_Code=[" & strUnion & "].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code and ConvertDiv.UOM_Code='LTR'  WHERE CONVERT(DATE, [" & strUnion & "].[dbo].TSPL_Dispatch_BulkSale.Document_Date, 103)  BETWEEN " & status
    '        Return Qry
    '    End Function

    '    Function ReturnPPProdProductionQry(ByVal strUnion As String, ByVal status As String) As String
    '        Dim Qry As String = "select '' As Trans_Type,'' As Screen_Type,pe.PROD_ENTRY_CODE As Document_No,Convert(Varchar(10),pe.PROD_DATE,103)Document_DATE,CASE when  im.Is_FreshItem =1 then convert(decimal(18,2),(isnull( ped.RECEIPT_QTY,0)) * (isnull( [" & strUnion & "].[dbo].TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/(I.[LTR])) ELSE convert(decimal(18,2),(isnull( ped.RECEIPT_QTY,0)) * (isnull( TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/(I.[KG])) END  as Prod_QTY,im.STD_FatPer,im.STD_SNFPer
    '                    FROM [" & strUnion & "].[dbo].TSPL_PP_PRODUCTION_ENTRY pe
    '                    LEFT JOIN [" & strUnion & "].[dbo].TSPL_PP_PRODUCTION_ENTRY_DETAIL ped ON ped.PROD_ENTRY_CODE = pe.PROD_ENTRY_CODE
    '                    LEFT JOIN [" & strUnion & "].[dbo].TSPL_ITEM_master im ON im.Item_Code = ped.ITEM_CODE
    '				    LEFT JOIN [" & strUnion & "].[dbo].TSPL_ITEM_UOM_DETAIL ON  [" & strUnion & "].[dbo].TSPL_ITEM_UOM_DETAIL.Item_Code =   ped.ITEM_CODE and  [" & strUnion & "].[dbo].TSPL_ITEM_UOM_DETAIL.UOM_Code =   ped.UNIT_CODE
    '                    LEFT JOIN (  SELECT * FROM ( select item_code,uom_code,conversion_factor from    [" & strUnion & "].[dbo].TSPL_ITEM_UOM_DETAIL ) I  PIVOT (Max(conversion_factor) FOR uom_code IN ( [LTR],[KG] )) P ) I ON   ped.ITEM_CODE = I.item_code
    '                    WHERE  CONVERT(DATE, pe.PROD_DATE, 103) BETWEEN " & status
    '        Return Qry
    '    End Function

    '    Function ReturnProdUPProductionQry(ByVal strUnion As String, ByVal status As String) As String
    '        Dim Qry As String = "select '' As Trans_Type,'' As Screen_Type,TSPL_PRODUCTION_UPLOADER_HEAD.Document_No,Convert(Varchar(10),TSPL_PRODUCTION_UPLOADER_HEAD.Document_Date,103)Document_Date,CASE when [" & strUnion & "].[dbo].TSPL_ITEM_master.Is_FreshItem =1 then convert(decimal(18,2),(isnull([" & strUnion & "].[dbo].TSPL_PRODUCTION_UPLOADER_DETAIL.Qty,0)) * (isnull([" & strUnion & "].[dbo].TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/(I.[LTR])) ELSE convert(decimal(18,2),(isnull([" & strUnion & "].[dbo].TSPL_PRODUCTION_UPLOADER_DETAIL.Qty,0)) * (isnull([" & strUnion & "].[dbo].TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/(I.[KG])) END  as Prod_QTY,[" & strUnion & "].[dbo].tspl_item_master.STD_FatPer,[" & strUnion & "].[dbo].tspl_item_master.STD_SNFPer
    '                    from [" & strUnion & "].[dbo].TSPL_PRODUCTION_UPLOADER_HEAD 
    '                    left join [" & strUnion & "].[dbo].TSPL_PRODUCTION_UPLOADER_DETAIL on [" & strUnion & "].[dbo].TSPL_PRODUCTION_UPLOADER_DETAIL.Document_No = [" & strUnion & "].[dbo].TSPL_PRODUCTION_UPLOADER_HEAD.Document_No
    '                    left join [" & strUnion & "].[dbo].TSPL_ITEM_master on [" & strUnion & "].[dbo].TSPL_ITEM_master.Item_Code = [" & strUnion & "].[dbo].TSPL_PRODUCTION_UPLOADER_DETAIL.Item_Code
    '                    LEFT JOIN [" & strUnion & "].[dbo].TSPL_ITEM_UOM_DETAIL ON [" & strUnion & "].[dbo].TSPL_ITEM_UOM_DETAIL.Item_Code =  [" & strUnion & "].[dbo].TSPL_PRODUCTION_UPLOADER_DETAIL.Item_Code and  [" & strUnion & "].[dbo].TSPL_ITEM_UOM_DETAIL.UOM_Code =  [" & strUnion & "].[dbo].TSPL_PRODUCTION_UPLOADER_DETAIL.UOM
    '                      LEFT JOIN (  SELECT * FROM ( select item_code,uom_code,conversion_factor from   [" & strUnion & "].[dbo].TSPL_ITEM_UOM_DETAIL ) I  PIVOT (Max(conversion_factor) FOR uom_code IN ( [LTR],[KG] )) P ) I ON  [" & strUnion & "].[dbo].TSPL_PRODUCTION_UPLOADER_DETAIL.Item_Code = I.item_code where  CONVERT(DATE, [" & strUnion & "].[dbo].TSPL_PRODUCTION_UPLOADER_HEAD.Document_Date, 103) BETWEEN " & status
    '        Return Qry
    '    End Function

    '    Function ReturnShiftGMTProdcutionQry(ByVal strUnion As String, ByVal status As String) As String
    '        Dim Qry As String = "select '' As Trans_Type,'' As Screen_Type,tspl_shift_mgmt.Document_No,Convert(Varchar(10),tspl_shift_mgmt.Document_Date,103)Document_Date, CASE when [" & strUnion & "].[dbo].TSPL_ITEM_master.Is_FreshItem =1 then convert(decimal(18,2),(isnull([" & strUnion & "].[dbo].TSPL_SHIFT_MGMT_PRODUCTION.Qty_KG,0))) end as Prod_QTY,[" & strUnion & "].[dbo].tspl_item_master.STD_FatPer,[" & strUnion & "].[dbo].tspl_item_master.STD_SNFPer
    'from  [" & strUnion & "].[dbo].TSPL_SHIFT_MGMT_PRODUCTION                
    'left join [" & strUnion & "].[dbo].tspl_shift_mgmt on [" & strUnion & "].[dbo].tspl_shift_mgmt.Document_No = [" & strUnion & "].[dbo].TSPL_SHIFT_MGMT_PRODUCTION.Document_No
    '                left join [" & strUnion & "].[dbo].TSPL_ITEM_master on [" & strUnion & "].[dbo].TSPL_ITEM_master.Item_Code = [" & strUnion & "].[dbo].TSPL_SHIFT_MGMT_PRODUCTION.Item_Code
    '                    where  CONVERT(DATE, [" & strUnion & "].[dbo].tspl_shift_mgmt.Document_Date, 103) BETWEEN " & status
    '        Return Qry
    '    End Function

    '    Function ReturnAdjstProductionQry(ByVal strUnion As String, ByVal status As String) As String
    '        Dim Qry As String = " select '' As Trans_Type,'' As Screen_Type,TSPL_ADJUSTMENT_HEADER.Adjustment_No As Document_No,Convert(Varchar(10),TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103)Document_Date,TSPL_ADJUSTMENT_DETAIL.Item_Quantity As Prod_QTY,tspl_item_master.STD_FatPer,tspl_item_master.STD_SNFPer
    '					   from [" & strUnion & "].[dbo].TSPL_ADJUSTMENT_DETAIL
    'Left Join [" & strUnion & "].[dbo].TSPL_ADJUSTMENT_HEADER On TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No
    'Left Join [" & strUnion & "].[dbo].TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code=TSPL_ADJUSTMENT_DETAIL.Item_Code
    'Where TSPL_ITEM_MASTER.Item_Type='F' And TSPL_ADJUSTMENT_HEADER.Trans_Type='IN'
    'And Convert(Date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) BETWEEN " & status
    '        Return Qry
    '    End Function

    '    Function ReturnDemandBookingDemandQry(ByVal strUnion As String, ByVal status As String) As String
    '        Dim Qry As String = "select  '' As Trans_Type,'' As Screen_Type,dbm.Document_No,Convert(Varchar(10),dbm.Document_Date,103)Document_Date,CASE when  im.Is_FreshItem =1 then convert(decimal(18,2),(isnull( TotalLtr_ItemWise,0)) ) ELSE convert(decimal(18,2),(isnull( Qty,0)) * (isnull( [" & strUnion & "].[dbo].TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/(I.[KG])) END  as TotalLtr_ItemWiseDemand,im.STD_FatPer,im.STD_SNFPer
    '					from [" & strUnion & "].[dbo].TSPL_DEMAND_BOOKING_DETAIL dbd
    '                    LEFT JOIN [" & strUnion & "].[dbo].TSPL_DEMAND_BOOKING_master dbm ON dbm.Document_No = dbd.Document_No
    '                    LEFT JOIN [" & strUnion & "].[dbo].TSPL_ITEM_MASTER im ON im.Item_Code = dbd.Item_Code
    '					LEFT JOIN [" & strUnion & "].[dbo].TSPL_ITEM_UOM_DETAIL ON  [" & strUnion & "].[dbo].TSPL_ITEM_UOM_DETAIL.Item_Code =   dbd.Item_Code and   [" & strUnion & "].[dbo].TSPL_ITEM_UOM_DETAIL.UOM_Code =   dbd.Unit_code
    '                    LEFT JOIN (  SELECT * FROM ( select item_code,uom_code,conversion_factor from    [" & strUnion & "].[dbo].TSPL_ITEM_UOM_DETAIL ) I  PIVOT (Max(conversion_factor) FOR uom_code IN ( [KG] )) P ) I ON   dbd.Item_Code = I.item_code
    '                    WHERE CONVERT(DATE, dbm.Document_Date, 103) BETWEEN " & status
    '        Return Qry
    '    End Function

    '    Function ReturnDisBulkDemandQry(ByVal strUnion As String, ByVal status As String) As String
    '        Dim Qry As String = "SELECT '' As Trans_Type,'' As Screen_Type,TSPL_Dispatch_BulkSale.Document_No,Convert(Varchar(10),TSPL_Dispatch_BulkSale.Document_Date,103)Document_Date,case when isnull(ConvertDiv.Conversion_Factor,0)=0 then 0 else  [" & strUnion & "].[dbo].TSPL_Dispatch_Detail_BulkSale.Qty*ConvertMul.Conversion_Factor/ConvertDiv.Conversion_Factor end as TotalLtr_ItemWiseDemand ,TSPL_Item_Master.STD_FatPer,TSPL_Item_Master.STD_SNFPer
    '                    FROM  [" & strUnion & "].[dbo].TSPL_Dispatch_Detail_BulkSale
    '                    LEFT JOIN  [" & strUnion & "].[dbo].TSPL_Dispatch_BulkSale  ON [" & strUnion & "].[dbo].TSPL_Dispatch_BulkSale.Document_No = [" & strUnion & "].[dbo].TSPL_Dispatch_Detail_BulkSale.Document_No
    '                    LEFT JOIN [" & strUnion & "].[dbo].tspl_item_master  ON tspl_item_master.Item_Code = TSPL_Dispatch_Detail_BulkSale.Item_Code
    '                    inner join [" & strUnion & "].[dbo].TSPL_ITEM_UOM_DETAIL as ConvertMul on ConvertMul.Item_Code=[" & strUnion & "].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code and ConvertMul.UOM_Code=[" & strUnion & "].[dbo].TSPL_Dispatch_Detail_BulkSale.Unit_code 
    '                    inner join [" & strUnion & "].[dbo].TSPL_ITEM_UOM_DETAIL as ConvertDiv on ConvertDiv .Item_Code=[" & strUnion & "].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code and ConvertDiv.UOM_Code='LTR'  
    '                    WHERE CONVERT(DATE, [" & strUnion & "].[dbo].TSPL_Dispatch_BulkSale.Document_Date, 103)  BETWEEN " & status
    '        Return Qry
    '    End Function

    '    Function ReturnBookingDemandQry(ByVal strUnion As String, ByVal status As String) As String
    '        Dim Qry As String = " select '' As Trans_Type,TSPL_BOOKING_MATSER.From_Screen_Code As Screen_Type,TSPL_BOOKING_MATSER.Document_No,Convert(Varchar(10),TSPL_BOOKING_MATSER.Document_Date,103)Document_Date,CASE when  tspl_item_master.Is_FreshItem =1 then convert(decimal(18,2),(isnull( [" & strUnion & "].[dbo].TSPL_BOOKING_DETAIL.Booking_Qty,0)) * (isnull( [" & strUnion & "].[dbo].TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/(I.[LTR])) ELSE convert(decimal(18,2),(isnull( [" & strUnion & "].[dbo].TSPL_BOOKING_DETAIL.Booking_Qty,0)) * (isnull( [" & strUnion & "].[dbo].TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/(I.[KG])) END  as TotalLtr_ItemWiseDemand,tspl_item_master.STD_FatPer,tspl_item_master.STD_SNFPer
    '                    FROM [" & strUnion & "].[dbo].TSPL_BOOKING_DETAIL 
    '					LEFT JOIN [" & strUnion & "].[dbo].TSPL_BOOKING_MATSER  ON [" & strUnion & "].[dbo].TSPL_BOOKING_MATSER.Document_No = [" & strUnion & "].[dbo].TSPL_BOOKING_DETAIL.Document_No
    '                    LEFT JOIN [" & strUnion & "].[dbo].tspl_item_master  ON tspl_item_master.Item_Code = TSPL_BOOKING_DETAIL.Item_Code
    '                    LEFT JOIN [" & strUnion & "].[dbo].TSPL_ITEM_UOM_DETAIL ON  [" & strUnion & "].[dbo].TSPL_ITEM_UOM_DETAIL.Item_Code =   TSPL_BOOKING_DETAIL.Item_Code and  [" & strUnion & "].[dbo].TSPL_ITEM_UOM_DETAIL.UOM_Code =   TSPL_BOOKING_DETAIL.Unit_code
    '                    LEFT JOIN (  SELECT * FROM ( select item_code,uom_code,conversion_factor from    [" & strUnion & "].[dbo].TSPL_ITEM_UOM_DETAIL ) I  PIVOT (Max(conversion_factor) FOR uom_code IN ( [LTR],[KG] )) P ) I ON   TSPL_BOOKING_DETAIL.ITEM_CODE = I.item_code
    '                    WHERE CONVERT(DATE, [" & strUnion & "].[dbo].TSPL_BOOKING_MATSER.Document_Date, 103) BETWEEN " & status
    '        Return Qry
    '    End Function

    '    Function ReturnDCSSaleEntryDemandQry(ByVal strUnion As String, ByVal status As String) As String
    '        Dim Qry As String = "Select Trans_Type,'' As Screen_Type,TSPL_DCS_SALE_ENTRY.Document_Code As Document_No,Convert(Varchar(10),TSPL_DCS_SALE_ENTRY.Document_Date,103)Document_Date,TSPL_DCS_SALE_ENTRY_DETAIL.Qty As TotalLtr_ItemWiseDemand,tspl_item_master.STD_FatPer,STD_SNFPer from [" & strUnion & "].[dbo].TSPL_DCS_SALE_ENTRY_DETAIL
    'Left Join [" & strUnion & "].[dbo].TSPL_DCS_SALE_ENTRY On TSPL_DCS_SALE_ENTRY.Document_Code=TSPL_DCS_SALE_ENTRY_DETAIL.DOCUMENT_CODE
    'Left Join [" & strUnion & "].[dbo].tspl_item_master On tspl_item_master.Item_Code=TSPL_DCS_SALE_ENTRY_DETAIL.Item_Code
    'Where CONVERT(DATE,TSPL_DCS_SALE_ENTRY.Document_Date, 103) BETWEEN " & status
    '        Return Qry
    '    End Function

    '    Function ReturnProdDemandDemandQry(ByVal strUnion As String, ByVal status As String) As String
    '        Dim Qry As String = "Select '' As Trans_Type,'' As Screen_Type,TSPL_PRODUCT_DEMAND_BOOKING_MASTER.Document_No,Convert(Varchar(10),TSPL_PRODUCT_DEMAND_BOOKING_MASTER.Document_Date,103)Document_Date,TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Qty As TotalLtr_ItemWiseDemand,tspl_item_master.STD_FatPer,STD_SNFPer from [" & strUnion & "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_DETAIL
    'Left Join [" & strUnion & "].[dbo].TSPL_PRODUCT_DEMAND_BOOKING_MASTER On TSPL_PRODUCT_DEMAND_BOOKING_MASTER.Document_No=TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Document_No
    'Left Join [" & strUnion & "].[dbo].tspl_item_master On tspl_item_master.Item_Code=TSPL_PRODUCT_DEMAND_BOOKING_DETAIL.Item_Code
    'Where CONVERT(DATE,TSPL_PRODUCT_DEMAND_BOOKING_MASTER.Document_Date, 103) BETWEEN  " & status
    '        Return Qry
    '    End Function

    '    Function ReturnMPUDProcurementQry(ByVal strUnion As String, ByVal status As String) As String
    '        Dim Qry As String = "Select '' As Trans_Type,'' As Screen_Type,muh.Document_No,Convert(Varchar(10),Max(muh.Document_Date),103)Document_Date,SUM(Milk_Weight) AS Milk_Weight, SUM(CAST(Milk_Weight * FAT / 100 AS DECIMAL(18,3))) AS FATKg,SUM(CAST(Milk_Weight * SNF / 100 AS DECIMAL(18,3))) AS SNFKG FROM [" & strUnion & "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL mud
    'LEFT JOIN [" & strUnion & "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD muh ON muh.Document_No = mud.Document_No
    'WHERE CONVERT(DATE, muh.Document_Date, 103)BETWEEN  " & status & "
    'Group By muh.Document_No"
    '        Return Qry
    '    End Function

    '    Function ReturnMilkShiftUploaderProcurementQry(ByVal strUnion As String, ByVal status As String) As String
    '        Dim Qry As String = "SELECT '' As Trans_Type,'' As Screen_Type,msh.Document_No,Convert(Varchar(10),Max(msh.Shift_Date),103)Document_Date,SUM(Milk_Weight) AS Milk_Weight, SUM(Milk_Weight * FAT / 100) AS FATKG, SUM(Milk_Weight * SNF / 100) AS SNFKG
    'FROM [" & strUnion & "].[dbo].TSPL_MILK_SHIFT_UPLOADER_DETAIL msd
    ' LEFT JOIN [" & strUnion & "].[dbo].TSPL_MILK_SHIFT_UPLOADER_HEAD msh ON msh.Document_No = msd.Document_No
    'WHERE CONVERT(DATE, msh.Shift_Date, 103) BETWEEN  " & status & "
    'Group By msh.Document_No"
    '        Return Qry
    '    End Function

    '    Function ReturnMilkCollectionDCSProcurementQry(ByVal strUnion As String, ByVal status As String) As String
    '        Dim Qry As String = "SELECT '' As Trans_Type,'' As Screen_Type,mcs.Document_No,Convert(Varchar(10),Max(mcs.document_date),103)Document_Date,ISNULL(SUM(QTY),0) AS Milk_Weight,ISNULL(SUM(FATKG),0) AS FATKG,ISNULL(SUM(SNFKG),0) AS SNFKG
    'FROM [" & strUnion & "].[dbo].TSPL_MILK_COLLECTION_DCS_DETAIL mcd
    ' LEFT JOIN [" & strUnion & "].[dbo].TSPL_MILK_COLLECTION_DCS mcs ON mcs.Document_No = mcd.Document_No
    'WHERE  mcs.Status = 0 AND CONVERT(DATE, mcs.Document_Date, 103) BETWEEN  " & status & "
    '		Group By mcs.Document_No"
    '        Return Qry
    '    End Function
    '    Function ReturnMilkCollectionBMCDCSProcurementQry(ByVal strUnion As String, ByVal status As String) As String
    '        Dim Qry As String = "Select '' As Trans_Type,'' As Screen_Type,'' As  Document_No,Convert(Varchar(10),TSPL_MILK_COLLECTION_BMCDCS.IDate,103)Document_Date,IsNull(Sum(TSPL_MILK_COLLECTION_BMCDCS_DCS.Qty),0)Milk_Weight,IsNull(Sum(TSPL_MILK_COLLECTION_BMCDCS_DCS.FATKG),0)FATKG,IsNull(Sum(TSPL_MILK_COLLECTION_BMCDCS_DCS.SNFKG),0)SNFKG 
    '                        from [" & strUnion & "].[dbo].TSPL_MILK_COLLECTION_BMCDCS_DCS
    '                        Left Join [" & strUnion & "].[dbo].TSPL_MILK_COLLECTION_BMCDCS On TSPL_MILK_COLLECTION_BMCDCS.PK_ID=TSPL_MILK_COLLECTION_BMCDCS_DCS.REF_PK_ID
    '                        Left Join [" & strUnion & "].[dbo].TSPL_MILK_COLLECTION_DCS_detail On TSPL_MILK_COLLECTION_DCS_detail.PK_Id=TSPL_MILK_COLLECTION_BMCDCS.PK_ID
    'Left Join [" & strUnion & "].[dbo].TSPL_MILK_COLLECTION_DCS On TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_detail.document_no
    '                        Where  IsNull(TSPL_MILK_COLLECTION_BMCDCS.PK_ID,'') = '' And CONVERT(DATE, TSPL_MILK_COLLECTION_BMCDCS.IDate, 103) BETWEEN  " & status & "
    '						Group By TSPL_MILK_COLLECTION_BMCDCS.IDate"
    '        Return Qry
    '    End Function

    '    Function ReturnInventoryMovementProcurementTankerQry(ByVal strUnion As String, ByVal status As String) As String
    '        Dim Qry As String = "select TSPL_INVENTORY_MOVEMENT_new.Trans_Type,'' As Screen_Type,TSPL_INVENTORY_MOVEMENT_new.Source_Doc_No As  Document_No,Convert(Varchar(10),TSPL_INVENTORY_MOVEMENT_new.Source_Doc_Date,103)Document_Date,(Qty) ,(Fat_KG),(SNF_KG) from [" & strUnion & "].[dbo].TSPL_INVENTORY_MOVEMENT_new 
    '						Where Trans_Type In ('MilkTransferIn','MCC-MSRN') 
    '						 And  CONVERT(DATE, TSPL_INVENTORY_MOVEMENT_new.Entry_Date, 103) 
    '						BETWEEN   " & status
    '        Return Qry
    '    End Function

    '    Function ReturnSaleInvoiceQry(ByVal strUnion As String, ByVal status As String) As String
    '        Dim qry As String = "select Max(TSPL_SD_SALE_INVOICE_HEAD.Trans_Type)Trans_Type,Max(TSPL_SD_SALE_INVOICE_HEAD.Screen_Type)Screen_Type,TSPL_SD_SALE_INVOICE_HEAD.document_code As  Document_No,Convert(Varchar(10),Max(TSPL_SD_SALE_INVOICE_HEAD.Document_Date),103)Document_Date,sum([" & strUnion & "].[dbo].TSPL_SD_SALE_INVOICE_DETAIL.Total_Basic_Amt) as Sale_Voucher from [" & strUnion & "].[dbo].TSPL_SD_SALE_INVOICE_DETAIL
    '                    left outer join [" & strUnion & "].[dbo].TSPL_SD_SALE_INVOICE_HEAD on [" & strUnion & "].[dbo].TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE = [" & strUnion & "].[dbo].TSPL_SD_SALE_INVOICE_HEAD.Document_Code
    '                    where  CONVERT(DATE, [" & strUnion & "].[dbo].TSPL_SD_SALE_INVOICE_HEAD.Document_Date, 103) BETWEEN  " & status & "
    '        Group By TSPL_SD_SALE_INVOICE_HEAD.document_code"
    '        Return qry
    '    End Function

    '    Function ReturnMilkPurchaseInvoiceQry(ByVal strUnion As String, ByVal status As String) As String
    '        Dim Qry As String = "select '' As Trans_Type,'' As Screen_Type,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE As  Document_No,Convert(Varchar(10),Max(TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE),103)Document_Date,SUM(TOTAL_AMOUNT) AS Purchase_Voucher from [" & strUnion & "].[dbo].TSPL_MILK_PURCHASE_INVOICE_HEAD where  CONVERT(DATE, DOC_DATE, 103) BETWEEN  " & status & "
    'Group By TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE"
    '        Return Qry
    '    End Function

    Private Sub btnreset_Click(sender As Object, e As EventArgs) Handles btnreset.Click
        funreset()
    End Sub

    Sub funreset()
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        gvDetails.DataSource = Nothing
        gvDetails.Rows.Clear()
        gvDetails.Columns.Clear()
        gvDetails.MasterTemplate.SummaryRowsBottom.Clear()
        RadPageViewPage3.Text = "Report Details"
        Dim serverDate As DateTime = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        Dim previousDay As DateTime = serverDate.AddDays(-1)
        Dim previousDayString As String = clsCommon.GetPrintDate(previousDay, "dd/MMM/yyyy")
        txtFromDate.Value = previousDayString
        txtToDate.Value = previousDayString
        btngo.Enabled = True
        rdbPosted.Checked = False
        rdbUnposted.Checked = False
        rbdAllTrans.Checked = True
        ReportType()
    End Sub
    Private Sub ReportType()
        dt = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Value", GetType(String))
        dt.Rows.Add("Union Wise Status Report", "UWSR")
        dt.Rows.Add("Union Wise Average Status Report", "UWASR")
        ddlReportType.DataSource = dt
        ddlReportType.DisplayMember = "Code"
        ddlReportType.ValueMember = "Value"
    End Sub
    Sub printview(ByVal print As Boolean)
        Try

            Dim query As String
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                Exit Sub
            End If
            Dim docNo As String = ""
            query = ""
            dt = clsDBFuncationality.GetDataTable("SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','CHT','JMBILL') ORDER BY [TSPL_APP_LOCATION].Location_Name")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For ii As Integer = 0 To dt.Rows.Count - 1
                    If ii > 0 Then
                        query += " UNION ALL "
                    End If
                    Dim status1 As String
                    Dim status2 As String
                    Dim status3 As String
                    Dim status4 As String
                    Dim status5 As String
                    If rdbPosted.Checked Then
                        status1 = " and  sh.Status=1 "
                        status2 = " and pe.POSTED= 1 "
                        status3 = " and dbm.Posted= 1 "
                        status4 = " and muh.Status= 1 "
                        status5 = "and msh.Status= 1"
                    ElseIf rdbUnposted.Checked Then
                        status1 = " and  sh.Status= 0 "
                        status2 = " and pe.POSTED= 0 "
                        status3 = " and dbm.Posted= 0 "
                        status4 = " and muh.Status= 0 "
                        status5 = "and msh.Status= 0 "
                    Else
                        status1 = " "
                        status2 = " "
                        status3 = " "
                        status4 = " "
                        status5 = " "
                    End If
                    query += " select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name]"
                    query += ",'" + clsCommon.GetPrintDate(txtFromDate.Value) + "'as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "'as Todate,'" + objCommonVar.CurrentUser + "' as username,ISNULL(SUM(Dis_Disbursement.Dis_QtyInLTR), 0) AS Dis_QtyInLTR,
                    ISNULL(SUM(Dis_Disbursement.Dis_FATKG), 0) AS Dis_FATKG,
                    ISNULL(SUM(Dis_Disbursement.Dis_SNFKG), 0) AS Dis_SNFKG,
                    ISNULL(SUM(Dis_Demand.TotalLtr_ItemWiseDemand), 0) AS TotalLtr_ItemWiseDemand,
                    ISNULL(SUM(Dis_Demand.FATKGDemand), 0) AS FATKGDemand,
                    ISNULL(SUM(Dis_Demand.SNFKGDemand), 0) AS SNFKGDemand,
                    ISNULL(SUM(Dis_Procurement.Milk_WeightProc), 0) AS Milk_WeightProc,
                    ISNULL(SUM(Dis_Procurement.FATKGProc), 0) AS FATKGProc,
                    ISNULL(SUM(Dis_Procurement.SNFKGProc), 0) AS SNFKGProc
                FROM 
                    (SELECT 
                        SUM(CASE 
                                WHEN sd.Unit_code = 'POUCH' THEN sd.qty * icp.Conversion_Factor / ilt.Conversion_Factor 
                                WHEN sd.Unit_code = 'CRATE' THEN sd.qty * icc.Conversion_Factor / ilt.Conversion_Factor
                                WHEN sd.Unit_code = 'LTR' THEN sd.qty  
                                ELSE 0 
                            END) AS Dis_QtyInLTR,
                        SUM(ISNULL((
                            CASE 
                                WHEN sd.Unit_code = 'POUCH' THEN sd.qty * icp.Conversion_Factor / ilt.Conversion_Factor 
                                WHEN sd.Unit_code = 'CRATE' THEN sd.qty * icc.Conversion_Factor / ilt.Conversion_Factor
                                WHEN sd.Unit_code = 'LTR' THEN sd.qty  
                                ELSE 0 
                            END
                        ) * STD_FatPer / 100,
                        0
                        )) AS Dis_FATKG,
                        SUM(ISNULL((
                            CASE 
                                WHEN sd.Unit_code = 'POUCH' THEN sd.qty * icp.Conversion_Factor / ilt.Conversion_Factor 
                                WHEN sd.Unit_code = 'CRATE' THEN sd.qty * icc.Conversion_Factor / ilt.Conversion_Factor
                                WHEN sd.Unit_code = 'LTR' THEN sd.qty  
                                ELSE 0 
                            END
                        ) * STD_SNFPer / 100,
                        0
                        )) AS Dis_SNFKG
                    FROM 
                         [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_DETAIL sd
                    LEFT JOIN 
                        [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_item_master im ON im.Item_Code = sd.Item_Code
                    LEFT JOIN 
                        [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_HEAD sh ON sh.Document_Code = sd.DOCUMENT_CODE
                    LEFT JOIN 
                        (SELECT Conversion_factor, Item_code FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL WHERE UOM_code = 'Crate') AS icc ON icc.Item_code = sd.Item_Code
                    LEFT JOIN 
                        (SELECT Conversion_factor, Item_code FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL WHERE UOM_code = 'Pouch') AS icp ON icp.Item_code = sd.Item_Code
                    LEFT JOIN 
                        (SELECT Conversion_factor, Item_code FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL WHERE UOM_code = 'LTR') AS ilt ON ilt.Item_code = sd.Item_Code
                    WHERE 
                        CONVERT(DATE, sh.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                        AND im.IsTaxable = 0 
                        AND im.Is_FreshItem = 1  
                        " + status1 + "
                    ) AS Dis_Disbursement,
                    (SELECT 
                        SUM(TotalLtr_ItemWise) AS TotalLtr_ItemWiseDemand,
                        SUM(TotalLtr_ItemWise * im.STD_FatPer / 100) AS FATKGDemand,
                        SUM(TotalLtr_ItemWise * im.STD_SNFPer / 100) AS SNFKGDemand
                    FROM 
                        [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL dbd
                    LEFT JOIN 
                        [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_master dbm ON dbm.Document_No = dbd.Document_No
                    LEFT JOIN 
                        [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER im ON im.Item_Code = dbd.Item_Code
                    WHERE 
                        CONVERT(DATE, dbm.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                        AND im.IsTaxable = 0
                        AND im.Is_FreshItem = 1
                        " + status3 + " 
                    ) AS Dis_Demand,
                    (SELECT 
                        SUM(milk_weight) AS Milk_WeightProc,
                        SUM(fatkg) AS FATKGProc,
                        SUM(SNFKG) AS SNFKGProc
                    FROM (
                        SELECT 
                            SUM(Milk_Weight) AS Milk_Weight,
                            SUM(CAST(Milk_Weight * FAT / 100 AS DECIMAL(18,3))) AS FATKg,
                            SUM(CAST(Milk_Weight * SNF / 100 AS DECIMAL(18,3))) AS SNFKG
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL mud
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD muh ON muh.Document_No = mud.Document_No
                        WHERE 
                            CONVERT(DATE, muh.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            " + status4 + "

                        UNION ALL

                        SELECT 
                            SUM(Milk_Weight) AS Milk_Weight,
                            SUM(Milk_Weight * FAT / 100) AS FATKG,
                            SUM(Milk_Weight * SNF / 100) AS SNFKG
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_DETAIL msd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_HEAD msh ON msh.Document_No = msd.Document_No
                        WHERE 
                            CONVERT(DATE, msh.Shift_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            " + status5 + "

                        UNION ALL

                        SELECT 
                            ISNULL(SUM(QTY),0) AS QTY,
                            ISNULL(SUM(FATKG),0) AS FATKG,
                            ISNULL(SUM(SNFKG),0) AS SNFKG
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_DETAIL mcd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS mcs ON mcs.Document_No = mcd.Document_No
                        WHERE 
                            mcs.Status = 0
                            AND CONVERT(DATE, mcs.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                        ) AS Procurement
                    ) AS Dis_Procurement
                   "
                Next
            End If
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(query)
            If dt2 IsNot Nothing OrElse dt2.Rows.Count > 0 Then
                If print = False Then
                    gv1.DataSource = Nothing
                    gv1.Rows.Clear()
                    gv1.Columns.Clear()
                    gv1.GroupDescriptors.Clear()
                    gv1.MasterTemplate.SummaryRowsBottom.Clear()
                    gv1.MasterView.Refresh()
                    gv1.DataSource = dt2
                    For ii As Integer = 0 To gv1.Columns.Count - 1
                        gv1.Columns(ii).ReadOnly = True
                    Next
                    RadPageView1.SelectedPage = RadPageViewPage2
                    gv1.EnableFiltering = True
                    SetGridFormat1()
                    gv1.BestFitColumns()
                Else
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.UnionReports, dt2, "crptmilkunionreport", "") ''report for both (RCDF And RCDFCF)
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub '  

    Private Sub btnPDF_Click(sender As Object, e As EventArgs) Handles btnPDF.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Date : " & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "  To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy"))
                arrHeader.Add("Report : " + ddlReportType.SelectedItem.Text)
                clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        Try
            If gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim strHeading As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptMilkUnion & "'"))

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Report Name : " + strHeading)
            arrHeader.Add("Date Range from : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Report : " + ddlReportType.SelectedItem.Text)
            transportSql.exportdata(gv1, "", Me.Text, False, arrHeader, False, False, True)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub gv1_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gv1.CellFormatting
        If e.CellElement.Value IsNot Nothing AndAlso IsNumeric(e.CellElement.Value) Then
            e.CellElement.Text = Convert.ToDecimal(e.CellElement.Value).ToString("N2", New CultureInfo("en-IN"))
        End If
    End Sub

    Private Sub gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Try
            If e.Row Is Nothing OrElse e.RowIndex < 0 Then
                Exit Sub
            End If

            Dim columnName As String = e.Column.Name
            Dim view As ColumnGroupsViewDefinition = CType(gv1.ViewDefinition, ColumnGroupsViewDefinition)
            For Each grp As GridViewColumnGroup In view.ColumnGroups
                For Each r As GridViewColumnGroupRow In grp.Rows
                    If r.ColumnNames.Contains(columnName) Then
                        Dim groupName As String = grp.Text
                        For Each colName As String In r.ColumnNames
                            Status()
                            GetDetails(groupName, clsCommon.myCstr(gv1.CurrentRow.Cells(0).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(2).Value))
                            Exit Sub
                        Next
                    End If
                Next
            Next
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub GetDetails(ByVal strGroupName As String, ByVal strUnionCode As String, ByVal strUnionName As String)
        Try
            Dim Qry As String = ""
            If clsCommon.CompairString(strGroupName, "Milk Procurement (As Per Truck Sheet)") = CompairStringResult.Equal Then
                Qry &= "Select '" & strUnionName & "' As [Union],Max(Trans_Type) As [Source],Document_No As [Document Code],Max(Document_Date) As [Document Date],Sum(Milk_Weight) As Qty,Sum((Milk_Weight*FAT)/100) As [FAT KG],Sum((Milk_Weight*SNF)/100) As [SNF KG] from("
                Qry &= ReturnMPUDProcurementQry(strUnionCode, status4)
                Qry &= " Union All "
                Qry &= ReturnMilkShiftUploaderProcurementQry(strUnionCode, status5)
                Qry &= " Union All "
                Qry &= ReturnMilkCollectionDCSProcurementQry(strUnionCode, status13)
                Qry &= " Union All "
                Qry &= ReturnMilkCollectionBMCDCSProcurementQry(strUnionCode, status18)
                Qry &= ")BaseQry Group By Document_No"
            ElseIf clsCommon.CompairString(strGroupName, "Milk Procurement (As Per Tanker)") = CompairStringResult.Equal Then
                Qry &= "Select '" & strUnionName & "' As [Union],Max(Trans_Type) As [Source],Document_No,Max(Document_Date) As [Document Date],Sum(Qty) As Qty,Sum(Fat_KG) As [FAT KG],Sum(SNF_KG) As [SNF KG]  from ("
                Qry &= ReturnInventoryMovementProcurementTankerQry(strUnionCode, Nothing)
                Qry &= ")BaseQry Group By Document_No"
            ElseIf clsCommon.CompairString(strGroupName, "Production") = CompairStringResult.Equal Then
                Qry &= "Select '" & strUnionName & "' As [Union],Max(Trans_Type) As [Source],Document_No As [Document Code],Sum(Prod_QTY) As Qty,Sum((Prod_QTY*STD_FatPer)/100) As [FAT KG],
Sum((Prod_QTY*STD_SNFPer)/100) As [SNF KG] from ("
                Qry &= ReturnPPProdProductionQry(strUnionCode, status2)
                Qry &= " Union All "
                Qry &= ReturnProdUPProductionQry(strUnionCode, status10)
                Qry &= " Union All "
                Qry &= ReturnShiftGMTProdcutionQry(strUnionCode, status17)
                Qry &= " Union All "
                Qry &= ReturnAdjstProductionQry(strUnionCode, status22)
                Qry &= " )BaseQry Group By Document_No"
            ElseIf clsCommon.CompairString(strGroupName, "Demand") = CompairStringResult.Equal Then
                Qry &= "Select '" & strUnionName & "' As [Union],Max(Trans_Type) As [Source],Document_No As [Document Code],Max(Document_Date) As [Document Date],Sum(TotalLtr_ItemWiseDemand) As Qty,Sum(Case When STD_FatPer>0 Then ((TotalLtr_ItemWiseDemand*STD_FatPer)/100) Else 0 End) As [FAT KG],Sum(Case When STD_SNFPer>0 Then ((TotalLtr_ItemWiseDemand*STD_SNFPer)/100) Else 0 end) As [SNF KG] from ("
                Qry &= ReturnDemandBookingDemandQry(strUnionCode, status3)
                Qry &= " Union All "
                Qry &= ReturnDisBulkDemandQry(strUnionCode, status9)
                Qry &= " Union All "
                Qry &= ReturnBookingDemandQry(strUnionCode, status15)
                Qry &= " Union All "
                Qry &= ReturnDCSSaleEntryDemandQry(strUnionCode, status20)
                Qry &= " Union All "
                Qry &= ReturnProdDemandDemandQry(strUnionCode, status21)
                Qry &= " )BaseQry Group By Document_NO"
            ElseIf clsCommon.CompairString(strGroupName, "Dispatch") = CompairStringResult.Equal Then
                Qry &= "Select '" & strUnionName & "' As [Union],Max(Trans_Type) As [Source],DOCUMENT_CODE As [Document Code],Max(Document_Date) As [Document Date],Sum(Dis_QtyInLTR) As Qty,
Sum((Dis_QtyInLTR*STD_FatPer)/100) As [FAT KG],Sum((Dis_QtyInLTR*STD_SNFPer)/100) As [SNF KG] from ("
                Qry &= ReturnShipDispatchQry(strUnionCode, status1)
                Qry &= " Union All "
                Qry &= ReturnBulkDispatchQry(strUnionCode, status9)
                Qry &= " )BaseQry Group By DOCUMENT_CODE"
            ElseIf clsCommon.CompairString(strGroupName, "Accounts") = CompairStringResult.Equal Then
                Qry &= " Select '" & strUnionName & "' As [Union],Max(Trans_Type) As [Source],Document_No As [Document Code],Max(Document_Date) As [Document Date],Sum(Sale_Voucher) As [Sale Voucher],Sum(Purchase_Voucher) As [Purhase Voucher] from ("
                Qry &= ReturnSaleInvoiceQry(strUnionCode, status7)
                Qry &= " Union All "
                Qry &= ReturnMilkPurchaseInvoiceQry(strUnionCode, status8)
                Qry &= " )BaseQry Group By Document_No"
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gvDetails.DataSource = Nothing
                gvDetails.Rows.Clear()
                gvDetails.Columns.Clear()
                gvDetails.GroupDescriptors.Clear()
                gvDetails.MasterTemplate.SummaryRowsBottom.Clear()
                gvDetails.MasterView.Refresh()
                gvDetails.DataSource = dt
                For ii As Integer = 0 To gvDetails.Columns.Count - 1
                    gvDetails.Columns(ii).ReadOnly = True
                Next
                RadPageView1.SelectedPage = RadPageViewPage3
                RadPageViewPage3.Text = strGroupName
                gvDetails.EnableFiltering = True
                'SetGridFormat1()
                gvDetails.BestFitColumns()
            Else
                Throw New Exception("Data Not found !")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub




End Class