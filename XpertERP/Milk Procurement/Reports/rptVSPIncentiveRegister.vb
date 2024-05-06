Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO

Public Class rptVSPIncentiveRegister
    Inherits FrmMainTranScreen
    Dim dt As DataTable
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim tmpValLoad As Boolean = True
    Dim arrLoc As String = Nothing

    Private Sub LOCATIONRIGTHS()
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData()

            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptVSPIncentiveRegister)
        If Not (MyBase.isReadFlag) Then
            If MDI.blnShowAllMenu = False Then
                Throw New Exception("Permission Denied")
            Else
                Throw New Exception("Can't Access in demo version. " + Environment.NewLine + " For any queries/details, contact tecxpert@tecxpert.in. ")

            End If
        End If
        radbtnBulkExp.Visible = MyBase.isExport

    End Sub
    Sub LoadMCCRouteVLCTree()
        'Dim qry As String = "select TSPL_VLC_MASTER_HEAD.VLC_Code as Code,TSPL_VLC_MASTER_HEAD.VLC_Name as Name,TSPL_VLC_MASTER_HEAD.Route_Code as ParentCode,3 as Lvl from TSPL_VLC_MASTER_HEAD where len(isnull(TSPL_VLC_MASTER_HEAD.Route_Code,''))>0 union all   select TSPL_MCC_ROUTE_MASTER.Route_Code as Code,TSPL_MCC_ROUTE_MASTER.Route_Name as Name,TSPL_MCC_ROUTE_MASTER.MCC_Code as ParentCode,2 as Lvl from TSPL_MCC_ROUTE_MASTER where len(isnull(TSPL_MCC_ROUTE_MASTER.MCC_Code,''))>0  union all   select TSPL_MCC_MASTER.MCC_Code as Code,TSPL_MCC_MASTER.MCC_NAME as Name,null as ParentCode,1 as Lvl from TSPL_MCC_MASTER"
        'cbtMCCRouteVLCC.ValueMember = "Code"
        'cbtMCCRouteVLCC.DisplayMember = "Name"
        'cbtMCCRouteVLCC.ParentValue = "ParentCode"
        'cbtMCCRouteVLCC.DataSource = clsDBFuncationality.GetDataTable(qry)

        Dim qry As String = Nothing
        Dim dt As DataTable = Nothing
        If clsCommon.myLen(arrLoc) > 0 Then
            qry = "select TSPL_VLC_MASTER_HEAD.VLC_Code as Code,TSPL_VLC_MASTER_HEAD.VLC_Name as Name,TSPL_VLC_MASTER_HEAD.Route_Code as ParentCode,3 as Lvl from TSPL_VLC_MASTER_HEAD where len(isnull(TSPL_VLC_MASTER_HEAD.Route_Code,''))>0 union all   select TSPL_MCC_ROUTE_MASTER.Route_Code as Code,TSPL_MCC_ROUTE_MASTER.Route_Name as Name,TSPL_MCC_ROUTE_MASTER.MCC_Code as ParentCode,2 as Lvl from TSPL_MCC_ROUTE_MASTER where len(isnull(TSPL_MCC_ROUTE_MASTER.MCC_Code,''))>0  union all   select TSPL_MCC_MASTER.MCC_Code as Code,TSPL_MCC_MASTER.MCC_NAME as Name,null as ParentCode,1 as Lvl from TSPL_MCC_MASTER   where TSPL_MCC_MASTER.MCC_Code in (" + arrLoc + ") "
            dt = clsDBFuncationality.GetDataTable(qry)
        End If

        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            btnGo.Enabled = False
        Else
            cbtMCCRouteVLCC.DataSource = dt
            cbtMCCRouteVLCC.ValueMember = "Code"
            cbtMCCRouteVLCC.DisplayMember = "Name"
            cbtMCCRouteVLCC.ParentValue = "ParentCode"
        End If
    End Sub


    Sub LoadShiftFrom()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Shift")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "M"
        dr("Shift") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "E"
        dr("Shift") = "Evening"
        dt.Rows.Add(dr)

        txtFromShift.DataSource = dt
        txtFromShift.ValueMember = "Code"
        'cbgShift.DisplayMember = "Shift"
    End Sub
    Sub LoadShiftTo()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Shift")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "M"
        dr("Shift") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "E"
        dr("Shift") = "Evening"
        dt.Rows.Add(dr)

        txtToShift.DataSource = dt
        txtToShift.ValueMember = "Code"
        'cbgShift.DisplayMember = "Shift"
    End Sub
    '===================================================================

    Sub FormatGrid()
        ' Dim strItemCode, head2 As String
        Dim summaryItem As New GridViewSummaryItem()
        gv.TableElement.TableHeaderHeight = 25
        gv.MasterTemplate.ShowRowHeaderColumn = True
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = True
            gv.Columns(ii).FormatString = "{0:n2}"
            gv.Columns(ii).Width = 100
        Next
        If ChkDetailWise.Checked Then
            gv.Columns("Milk Receipt Code").IsVisible = True
            gv.Columns("Milk Receipt Code").Width = 100
            gv.Columns("Milk Receipt Code").HeaderText = " Milk Receipt Code"

            gv.Columns("MCC Code").IsVisible = True
            gv.Columns("MCC Code").Width = 100
            gv.Columns("MCC Code").HeaderText = "MCC Code"

            gv.Columns("Shift").IsVisible = True

            gv.Columns("MCC Name").IsVisible = True
            gv.Columns("MCC Name").Width = 100
            gv.Columns("MCC Name").HeaderText = "MCC Name"

            gv.Columns("Doc Date").IsVisible = True
            gv.Columns("Doc Date").Width = 100
            gv.Columns("Doc Date").HeaderText = " Doc Date"

            gv.Columns("Route Code").IsVisible = True
            gv.Columns("Route Code").Width = 100
            gv.Columns("Route Code").HeaderText = "Route Code"

            gv.Columns("Route Name").IsVisible = True
            gv.Columns("Route Name").Width = 100
            gv.Columns("Route Name").HeaderText = "Route Name"

            gv.Columns("Vehicle Code").IsVisible = True
            gv.Columns("Vehicle Code").Width = 100
            gv.Columns("Vehicle Code").HeaderText = "Vehicle Code"

            gv.Columns("VSP Code").IsVisible = True
            gv.Columns("VSP Code").Width = 100
            gv.Columns("VSP Code").HeaderText = " VSP Code"

            gv.Columns("VSP Name").IsVisible = True
            gv.Columns("VSP Name").Width = 100
            gv.Columns("VSP Name").HeaderText = "VSP Name"

            gv.Columns("Vlc Uploader Code").IsVisible = True
            gv.Columns("Vlc Uploader Code").Width = 100
            gv.Columns("Vlc Uploader Code").HeaderText = "Vlc Uploader Code"

            gv.Columns("Vlc Code").IsVisible = True
            gv.Columns("Vlc Code").Width = 100
            gv.Columns("Vlc Code").HeaderText = " VSP Code"

            gv.Columns("VLC Name").IsVisible = True
            gv.Columns("VLC Name").Width = 100
            gv.Columns("VLC Name").HeaderText = "VLC Name"

            gv.Columns("Sample No").IsVisible = True
            gv.Columns("Sample No").Width = 100
            gv.Columns("Sample No").HeaderText = "Sample No"

            gv.Columns("No Of Cans").IsVisible = True
            gv.Columns("No Of Cans").Width = 100
            gv.Columns("No Of Cans").HeaderText = "No Of Cans"

            gv.Columns("Milk Weight").IsVisible = True
            gv.Columns("Milk Weight").Width = 100
            gv.Columns("Milk Weight").HeaderText = "Milk Weight"

            gv.Columns("Milk Weight(KG)").IsVisible = True
            gv.Columns("Milk Weight(KG)").Width = 100
            gv.Columns("Milk Weight(KG)").HeaderText = "Milk Weight(KG)"

            gv.Columns("Milk Weight(LTR)").IsVisible = True
            gv.Columns("Milk Weight(LTR)").Width = 100
            gv.Columns("Milk Weight(LTR)").HeaderText = "Milk Weight(LTR)"

            gv.Columns("FAT(%)").IsVisible = True
            gv.Columns("FAT(%)").Width = 100
            gv.Columns("FAT(%)").HeaderText = " FAT(%)"

            gv.Columns("SNF(%)").IsVisible = True
            gv.Columns("SNF(%)").Width = 100
            gv.Columns("SNF(%)").HeaderText = "SNF(%)"

            gv.Columns("FAT(KG)").IsVisible = True
            gv.Columns("FAT(KG)").Width = 100
            gv.Columns("FAT(KG)").HeaderText = " FAT(KG)"

            gv.Columns("SNF(KG)").IsVisible = True
            gv.Columns("SNF(KG)").Width = 100
            gv.Columns("SNF(KG)").HeaderText = "SNF(KG)"

            gv.Columns("Cow Milk Qty (KG)").IsVisible = True
            gv.Columns("Cow Milk Qty (KG)").Width = 100
            gv.Columns("Cow Milk Qty (KG)").HeaderText = "Cow Milk Qty (KG)"

            gv.Columns("Cow FAT(%)").IsVisible = True
            gv.Columns("Cow FAT(%)").Width = 100
            gv.Columns("Cow FAT(%)").HeaderText = "Cow FAT(%)"

            gv.Columns("Cow SNF(%)").IsVisible = True
            gv.Columns("Cow SNF(%)").Width = 100
            gv.Columns("Cow SNF(%)").HeaderText = "Cow SNF(%)"

            gv.Columns("Cow FAT (KG)").IsVisible = True
            gv.Columns("Cow FAT (KG)").Width = 100
            gv.Columns("Cow FAT (KG)").HeaderText = "Cow FAT (KG)"

            gv.Columns("Cow SNF (KG)").IsVisible = True
            gv.Columns("Cow SNF (KG)").Width = 100
            gv.Columns("Cow SNF (KG)").HeaderText = "Cow SNF (KG)"

            gv.Columns("Buffalo Milk Qty (KG)").IsVisible = True
            gv.Columns("Buffalo Milk Qty (KG)").Width = 100
            gv.Columns("Buffalo Milk Qty (KG)").HeaderText = "Buffalo Milk Qty (KG)"

            gv.Columns("Buffalo SNF(%)").IsVisible = True
            gv.Columns("Buffalo SNF(%)").Width = 100
            gv.Columns("Buffalo SNF(%)").HeaderText = "Buffalo SNF(%)"

            gv.Columns("Buffalo FAT(%)").IsVisible = True
            gv.Columns("Buffalo FAT(%)").Width = 100

            gv.Columns("Buffalo FAT (KG)").IsVisible = True
            gv.Columns("Buffalo FAT (KG)").Width = 100
            gv.Columns("Buffalo FAT (KG)").HeaderText = "Buffalo FAT (KG)"

            gv.Columns("Milk Type").IsVisible = True
            gv.Columns("Milk Type").Width = 100
            gv.Columns("Milk Type").HeaderText = " Milk Type"

            gv.Columns("SRN No").IsVisible = True
            gv.Columns("SRN No").Width = 100
            gv.Columns("SRN No").HeaderText = "SRN No"

            gv.Columns("SRN Amount").IsVisible = True
            gv.Columns("SRN Amount").Width = 100
            gv.Columns("SRN Amount").HeaderText = "SRN Amount"

            gv.Columns("SRN No").IsVisible = True
            gv.Columns("SRN No").Width = 100
            gv.Columns("SRN No").HeaderText = "SRN No"

            gv.Columns("SRN Qty").IsVisible = True
            gv.Columns("SRN Qty").Width = 100
            gv.Columns("SRN Qty").HeaderText = "SRN Qty"

            gv.Columns("STD Qty").IsVisible = True
            gv.Columns("STD Qty").Width = 100
            gv.Columns("STD Qty").HeaderText = "STD Qty"

            gv.Columns("SRN Rate").IsVisible = True
            gv.Columns("SRN Rate").Width = 100
            gv.Columns("SRN Rate").HeaderText = "SRN Rate"

            gv.Columns("Shift Status").IsVisible = True
            gv.Columns("Shift Status").Width = 100
            gv.Columns("Shift Status").HeaderText = "Shift Status"

            gv.Columns("Invoice_no").IsVisible = True
            gv.Columns("Invoice_no").Width = 100
            gv.Columns("Invoice_no").HeaderText = "Invoice No"

            gv.Columns("Invoice_Date").IsVisible = True
            gv.Columns("Invoice_Date").Width = 100
            gv.Columns("Invoice_Date").HeaderText = "Invoice Date"


            gv.Columns("Date").IsVisible = False





            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim intCount As Integer = 0

            Dim item1 As New GridViewSummaryItem("Milk Weight", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("Milk Weight(KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            Dim item3 As New GridViewSummaryItem("Milk Weight(LTR)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("FAT(KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)

            Dim item5 As New GridViewSummaryItem("SNF(KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)
            Dim summaryItem1 As New GridViewSummaryItem()
            summaryItem1.FormatString = "{0:F2}"
            summaryItem1.Name = "FAT(%)"
            summaryItem1.AggregateExpression = "sum([FAT(KG)])*100/sum([Milk Weight(KG)])"
            summaryRowItem.Add(summaryItem1)

            Dim summaryItem2 As New GridViewSummaryItem()
            summaryItem2.FormatString = "{0:F2}"
            summaryItem2.Name = "SNF(%)"
            summaryItem2.AggregateExpression = "sum([SNF(KG)])*100/sum([Milk Weight(KG)])"
            summaryRowItem.Add(summaryItem2)

            Dim item6 As New GridViewSummaryItem("Cow Milk Qty (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item6)
            Dim item7 As New GridViewSummaryItem("Cow FAT (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item7)

            Dim item8 As New GridViewSummaryItem("Cow SNF (KG)", "{0:F2}", GridAggregateFunction.Sum)

            summaryRowItem.Add(item8)
            Dim summaryItem3 As New GridViewSummaryItem()
            summaryItem3.FormatString = "{0:F2}"
            summaryItem3.Name = "Cow SNF(%)"
            summaryItem3.AggregateExpression = "sum([Cow SNF (KG)])*100/sum([Cow Milk Qty (KG)])"
            summaryRowItem.Add(summaryItem3)

            Dim summaryItem4 As New GridViewSummaryItem()
            summaryItem4.FormatString = "{0:F2}"
            summaryItem4.Name = "Cow FAT(%)"
            summaryItem4.AggregateExpression = "sum([Cow FAT (KG)])*100/sum([Cow Milk Qty (KG)])"
            summaryRowItem.Add(summaryItem4)

            Dim item9 As New GridViewSummaryItem("Buffalo Milk Qty (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item9)
            Dim item10 As New GridViewSummaryItem("Buffalo FAT (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item10)
            Dim item11 As New GridViewSummaryItem("Buffalo SNF (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item11)
            Dim summaryItem5 As New GridViewSummaryItem()
            summaryItem5.FormatString = "{0:F2}"
            summaryItem5.Name = "Buffalo FAT(%)"
            summaryItem5.AggregateExpression = "sum([Buffalo FAT (KG)])*100/sum([Buffalo Milk Qty (KG)])"
            summaryRowItem.Add(summaryItem5)

            Dim summaryItem6 As New GridViewSummaryItem()
            summaryItem6.FormatString = "{0:F2}"
            summaryItem6.Name = "Buffalo SNF(%)"
            summaryItem6.AggregateExpression = "sum([Buffalo SNF (KG)])*100/sum([Buffalo Milk Qty (KG)])"
            summaryRowItem.Add(summaryItem6)

            Dim item12 As New GridViewSummaryItem("SRN Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item12)
            Dim item13 As New GridViewSummaryItem("SRN Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item13)

            Dim item14 As New GridViewSummaryItem("STD Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item14)

            Dim item15 As New GridViewSummaryItem("Incentive Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item15)
            'Dim item14 As New GridViewSummaryItem("Cow FAT (KG)", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(item14)
            gv.ShowGroupPanel = False
            gv.MasterTemplate.AutoExpandGroups = True

            gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        ElseIf ChkMCCWise.Checked Then
            gv.Columns("MCC Code").IsVisible = True
            gv.Columns("MCC Code").Width = 100
            gv.Columns("MCC Code").HeaderText = "MCC Code"

            gv.Columns("MCC Name").IsVisible = True
            gv.Columns("MCC Name").Width = 100
            gv.Columns("MCC Name").HeaderText = "MCC Name"


            gv.Columns("Milk Weight").IsVisible = True
            gv.Columns("Milk Weight").Width = 100
            gv.Columns("Milk Weight").HeaderText = "Milk Weight"

            gv.Columns("Milk Weight(KG)").IsVisible = True
            gv.Columns("Milk Weight(KG)").Width = 100
            gv.Columns("Milk Weight(KG)").HeaderText = "Milk Weight(KG)"

            gv.Columns("Milk Weight(LTR)").IsVisible = True
            gv.Columns("Milk Weight(LTR)").Width = 100
            gv.Columns("Milk Weight(LTR)").HeaderText = "Milk Weight(LTR)"

            gv.Columns("FAT(%)").IsVisible = True
            gv.Columns("FAT(%)").Width = 100
            gv.Columns("FAT(%)").HeaderText = " FAT(%)"

            gv.Columns("SNF(%)").IsVisible = True
            gv.Columns("SNF(%)").Width = 100
            gv.Columns("SNF(%)").HeaderText = "SNF(%)"

            gv.Columns("FAT(KG)").IsVisible = True
            gv.Columns("FAT(KG)").Width = 100
            gv.Columns("FAT(KG)").HeaderText = " FAT(KG)"

            gv.Columns("SNF(KG)").IsVisible = True
            gv.Columns("SNF(KG)").Width = 100
            gv.Columns("SNF(KG)").HeaderText = "SNF(KG)"

            gv.Columns("Cow Milk Qty (KG)").IsVisible = True
            gv.Columns("Cow Milk Qty (KG)").Width = 100
            gv.Columns("Cow Milk Qty (KG)").HeaderText = "Cow Milk Qty (KG)"

            gv.Columns("Cow FAT(%)").IsVisible = True
            gv.Columns("Cow FAT(%)").Width = 100
            gv.Columns("Cow FAT(%)").HeaderText = "Cow FAT(%)"

            gv.Columns("Cow SNF(%)").IsVisible = True
            gv.Columns("Cow SNF(%)").Width = 100
            gv.Columns("Cow SNF(%)").HeaderText = "Cow SNF(%)"

            gv.Columns("Cow FAT (KG)").IsVisible = True
            gv.Columns("Cow FAT (KG)").Width = 100
            gv.Columns("Cow FAT (KG)").HeaderText = "Cow FAT (KG)"

            gv.Columns("Cow SNF (KG)").IsVisible = True
            gv.Columns("Cow SNF (KG)").Width = 100
            gv.Columns("Cow SNF (KG)").HeaderText = "Cow SNF (KG)"

            gv.Columns("Buffalo Milk Qty (KG)").IsVisible = True
            gv.Columns("Buffalo Milk Qty (KG)").Width = 100
            gv.Columns("Buffalo Milk Qty (KG)").HeaderText = "Buffalo Milk Qty (KG)"

            gv.Columns("Buffalo SNF(%)").IsVisible = True
            gv.Columns("Buffalo SNF(%)").Width = 100
            gv.Columns("Buffalo SNF(%)").HeaderText = "Buffalo SNF(%)"

            gv.Columns("Buffalo FAT(%)").IsVisible = True
            gv.Columns("Buffalo FAT(%)").Width = 100

            gv.Columns("Buffalo FAT (KG)").IsVisible = True
            gv.Columns("Buffalo FAT (KG)").Width = 100
            gv.Columns("Buffalo FAT (KG)").HeaderText = "Buffalo FAT (KG)"



            gv.Columns("SRN Qty").IsVisible = True
            gv.Columns("SRN Qty").Width = 100
            gv.Columns("SRN Qty").HeaderText = "SRN Qty"

            gv.Columns("STD Qty").IsVisible = True
            gv.Columns("STD Qty").Width = 100
            gv.Columns("STD Qty").HeaderText = "STD Qty"


            gv.Columns("SRN Amount").IsVisible = True
            gv.Columns("SRN Amount").Width = 100
            gv.Columns("SRN Amount").HeaderText = "SRN Amount"


            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim intCount As Integer = 0

            Dim item1 As New GridViewSummaryItem("Milk Weight", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("Milk Weight(KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            Dim item3 As New GridViewSummaryItem("Milk Weight(LTR)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("FAT(KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)

            Dim item5 As New GridViewSummaryItem("SNF(KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)
            Dim summaryItem1 As New GridViewSummaryItem()
            summaryItem1.FormatString = "{0:F2}"
            summaryItem1.Name = "FAT(%)"
            summaryItem1.AggregateExpression = "sum([FAT(KG)])*100/sum([Milk Weight(KG)])"
            summaryRowItem.Add(summaryItem1)

            Dim summaryItem2 As New GridViewSummaryItem()
            summaryItem2.FormatString = "{0:F2}"
            summaryItem2.Name = "SNF(%)"
            summaryItem2.AggregateExpression = "sum([SNF(KG)])*100/sum([Milk Weight(KG)])"
            summaryRowItem.Add(summaryItem2)

            Dim item6 As New GridViewSummaryItem("Cow Milk Qty (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item6)
            Dim item7 As New GridViewSummaryItem("Cow FAT (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item7)

            Dim item8 As New GridViewSummaryItem("Cow SNF (KG)", "{0:F2}", GridAggregateFunction.Sum)

            summaryRowItem.Add(item8)
            Dim summaryItem3 As New GridViewSummaryItem()
            summaryItem3.FormatString = "{0:F2}"
            summaryItem3.Name = "Cow SNF(%)"
            summaryItem3.AggregateExpression = "sum([Cow SNF (KG)])*100/sum([Cow Milk Qty (KG)])"
            summaryRowItem.Add(summaryItem3)

            Dim summaryItem4 As New GridViewSummaryItem()
            summaryItem4.FormatString = "{0:F2}"
            summaryItem4.Name = "Cow FAT(%)"
            summaryItem4.AggregateExpression = "sum([Cow FAT (KG)])*100/sum([Cow Milk Qty (KG)])"
            summaryRowItem.Add(summaryItem4)

            Dim item9 As New GridViewSummaryItem("Buffalo Milk Qty (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item9)
            Dim item10 As New GridViewSummaryItem("Buffalo FAT (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item10)
            Dim item11 As New GridViewSummaryItem("Buffalo SNF (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item11)
            Dim summaryItem5 As New GridViewSummaryItem()
            summaryItem5.FormatString = "{0:F2}"
            summaryItem5.Name = "Buffalo FAT(%)"
            summaryItem5.AggregateExpression = "sum([Buffalo FAT (KG)])*100/sum([Buffalo Milk Qty (KG)])"
            summaryRowItem.Add(summaryItem5)

            Dim summaryItem6 As New GridViewSummaryItem()
            summaryItem6.FormatString = "{0:F2}"
            summaryItem6.Name = "Buffalo SNF(%)"
            summaryItem6.AggregateExpression = "sum([Buffalo SNF (KG)])*100/sum([Buffalo Milk Qty (KG)])"
            summaryRowItem.Add(summaryItem6)
            Dim item13 As New GridViewSummaryItem("SRN Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item13)

            Dim item14 As New GridViewSummaryItem("STD Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item14)

            Dim item15 As New GridViewSummaryItem("Incentive Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item15)

            Dim item12 As New GridViewSummaryItem("SRN Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item12)

            gv.ShowGroupPanel = False
            gv.MasterTemplate.AutoExpandGroups = True

            gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        ElseIf chkRoutewise.Checked Then
            gv.Columns("MCC Code").IsVisible = True
            gv.Columns("MCC Code").Width = 100
            gv.Columns("MCC Code").HeaderText = "MCC Code"

            gv.Columns("MCC Name").IsVisible = True
            gv.Columns("MCC Name").Width = 100
            gv.Columns("MCC Name").HeaderText = "MCC Name"

            gv.Columns("Route Code").IsVisible = True
            gv.Columns("Route Code").Width = 100
            gv.Columns("Route Code").HeaderText = "Route Code"

            gv.Columns("Route Name").IsVisible = True
            gv.Columns("Route Name").Width = 100
            gv.Columns("Route Name").HeaderText = "Route Name"


            gv.Columns("Milk Weight").IsVisible = True
            gv.Columns("Milk Weight").Width = 100
            gv.Columns("Milk Weight").HeaderText = "Milk Weight"

            gv.Columns("Milk Weight(KG)").IsVisible = True
            gv.Columns("Milk Weight(KG)").Width = 100
            gv.Columns("Milk Weight(KG)").HeaderText = "Milk Weight(KG)"

            gv.Columns("Milk Weight(LTR)").IsVisible = True
            gv.Columns("Milk Weight(LTR)").Width = 100
            gv.Columns("Milk Weight(LTR)").HeaderText = "Milk Weight(LTR)"

            gv.Columns("FAT(%)").IsVisible = True
            gv.Columns("FAT(%)").Width = 100
            gv.Columns("FAT(%)").HeaderText = " FAT(%)"

            gv.Columns("SNF(%)").IsVisible = True
            gv.Columns("SNF(%)").Width = 100
            gv.Columns("SNF(%)").HeaderText = "SNF(%)"

            gv.Columns("FAT(KG)").IsVisible = True
            gv.Columns("FAT(KG)").Width = 100
            gv.Columns("FAT(KG)").HeaderText = " FAT(KG)"

            gv.Columns("SNF(KG)").IsVisible = True
            gv.Columns("SNF(KG)").Width = 100
            gv.Columns("SNF(KG)").HeaderText = "SNF(KG)"

            gv.Columns("Cow Milk Qty (KG)").IsVisible = True
            gv.Columns("Cow Milk Qty (KG)").Width = 100
            gv.Columns("Cow Milk Qty (KG)").HeaderText = "Cow Milk Qty (KG)"

            gv.Columns("Cow FAT(%)").IsVisible = True
            gv.Columns("Cow FAT(%)").Width = 100
            gv.Columns("Cow FAT(%)").HeaderText = "Cow FAT(%)"

            gv.Columns("Cow SNF(%)").IsVisible = True
            gv.Columns("Cow SNF(%)").Width = 100
            gv.Columns("Cow SNF(%)").HeaderText = "Cow SNF(%)"

            gv.Columns("Cow FAT (KG)").IsVisible = True
            gv.Columns("Cow FAT (KG)").Width = 100
            gv.Columns("Cow FAT (KG)").HeaderText = "Cow FAT (KG)"

            gv.Columns("Cow SNF (KG)").IsVisible = True
            gv.Columns("Cow SNF (KG)").Width = 100
            gv.Columns("Cow SNF (KG)").HeaderText = "Cow SNF (KG)"

            gv.Columns("Buffalo Milk Qty (KG)").IsVisible = True
            gv.Columns("Buffalo Milk Qty (KG)").Width = 100
            gv.Columns("Buffalo Milk Qty (KG)").HeaderText = "Buffalo Milk Qty (KG)"

            gv.Columns("Buffalo SNF(%)").IsVisible = True
            gv.Columns("Buffalo SNF(%)").Width = 100
            gv.Columns("Buffalo SNF(%)").HeaderText = "Buffalo SNF(%)"

            gv.Columns("Buffalo FAT(%)").IsVisible = True
            gv.Columns("Buffalo FAT(%)").Width = 100

            gv.Columns("Buffalo FAT (KG)").IsVisible = True
            gv.Columns("Buffalo FAT (KG)").Width = 100
            gv.Columns("Buffalo FAT (KG)").HeaderText = "Buffalo FAT (KG)"



            gv.Columns("SRN Qty").IsVisible = True
            gv.Columns("SRN Qty").Width = 100
            gv.Columns("SRN Qty").HeaderText = "SRN Qty"

            gv.Columns("STD Qty").IsVisible = True
            gv.Columns("STD Qty").Width = 100
            gv.Columns("STD Qty").HeaderText = "STD Qty"


            gv.Columns("SRN Amount").IsVisible = True
            gv.Columns("SRN Amount").Width = 100
            gv.Columns("SRN Amount").HeaderText = "SRN Amount"


            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim intCount As Integer = 0

            Dim item1 As New GridViewSummaryItem("Milk Weight", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("Milk Weight(KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            Dim item3 As New GridViewSummaryItem("Milk Weight(LTR)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("FAT(KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)

            Dim item5 As New GridViewSummaryItem("SNF(KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)
            Dim summaryItem1 As New GridViewSummaryItem()
            summaryItem1.FormatString = "{0:F2}"
            summaryItem1.Name = "FAT(%)"
            summaryItem1.AggregateExpression = "sum([FAT(KG)])*100/sum([Milk Weight(KG)])"
            summaryRowItem.Add(summaryItem1)

            Dim summaryItem2 As New GridViewSummaryItem()
            summaryItem2.FormatString = "{0:F2}"
            summaryItem2.Name = "SNF(%)"
            summaryItem2.AggregateExpression = "sum([SNF(KG)])*100/sum([Milk Weight(KG)])"
            summaryRowItem.Add(summaryItem2)

            Dim item6 As New GridViewSummaryItem("Cow Milk Qty (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item6)
            Dim item7 As New GridViewSummaryItem("Cow FAT (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item7)

            Dim item8 As New GridViewSummaryItem("Cow SNF (KG)", "{0:F2}", GridAggregateFunction.Sum)

            summaryRowItem.Add(item8)
            Dim summaryItem3 As New GridViewSummaryItem()
            summaryItem3.FormatString = "{0:F2}"
            summaryItem3.Name = "Cow SNF(%)"
            summaryItem3.AggregateExpression = "sum([Cow SNF (KG)])*100/sum([Cow Milk Qty (KG)])"
            summaryRowItem.Add(summaryItem3)

            Dim summaryItem4 As New GridViewSummaryItem()
            summaryItem4.FormatString = "{0:F2}"
            summaryItem4.Name = "Cow FAT(%)"
            summaryItem4.AggregateExpression = "sum([Cow FAT (KG)])*100/sum([Cow Milk Qty (KG)])"
            summaryRowItem.Add(summaryItem4)

            Dim item9 As New GridViewSummaryItem("Buffalo Milk Qty (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item9)
            Dim item10 As New GridViewSummaryItem("Buffalo FAT (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item10)
            Dim item11 As New GridViewSummaryItem("Buffalo SNF (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item11)
            Dim summaryItem5 As New GridViewSummaryItem()
            summaryItem5.FormatString = "{0:F2}"
            summaryItem5.Name = "Buffalo FAT(%)"
            summaryItem5.AggregateExpression = "sum([Buffalo FAT (KG)])*100/sum([Buffalo Milk Qty (KG)])"
            summaryRowItem.Add(summaryItem5)

            Dim summaryItem6 As New GridViewSummaryItem()
            summaryItem6.FormatString = "{0:F2}"
            summaryItem6.Name = "Buffalo SNF(%)"
            summaryItem6.AggregateExpression = "sum([Buffalo SNF (KG)])*100/sum([Buffalo Milk Qty (KG)])"
            summaryRowItem.Add(summaryItem6)
            Dim item13 As New GridViewSummaryItem("SRN Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item13)

            Dim item14 As New GridViewSummaryItem("STD Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item14)

            Dim item15 As New GridViewSummaryItem("Incentive Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item15)

            Dim item12 As New GridViewSummaryItem("SRN Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item12)

            gv.ShowGroupPanel = False
            gv.MasterTemplate.AutoExpandGroups = True

            gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        ElseIf rbtnVLCWise.Checked Then
            gv.Columns("MCC Code").IsVisible = True
            gv.Columns("MCC Code").Width = 100
            gv.Columns("MCC Code").HeaderText = "MCC Code"

            gv.Columns("MCC Name").IsVisible = True
            gv.Columns("MCC Name").Width = 100
            gv.Columns("MCC Name").HeaderText = "MCC Name"

            gv.Columns("Route Code").IsVisible = True
            gv.Columns("Route Code").Width = 100
            gv.Columns("Route Code").HeaderText = "Route Code"

            gv.Columns("Route Name").IsVisible = True
            gv.Columns("Route Name").Width = 100
            gv.Columns("Route Name").HeaderText = "Route Name"

            gv.Columns("Vlc Code").IsVisible = True
            gv.Columns("Vlc Code").Width = 100
            gv.Columns("Vlc Code").HeaderText = "VLC Code"

            gv.Columns("VLC Name").IsVisible = True
            gv.Columns("VLC Name").Width = 100
            gv.Columns("VLC Name").HeaderText = "VLC Name"


            gv.Columns("Milk Weight").IsVisible = True
            gv.Columns("Milk Weight").Width = 100
            gv.Columns("Milk Weight").HeaderText = "Milk Weight"

            gv.Columns("Milk Weight(KG)").IsVisible = True
            gv.Columns("Milk Weight(KG)").Width = 100
            gv.Columns("Milk Weight(KG)").HeaderText = "Milk Weight(KG)"

            gv.Columns("Milk Weight(LTR)").IsVisible = True
            gv.Columns("Milk Weight(LTR)").Width = 100
            gv.Columns("Milk Weight(LTR)").HeaderText = "Milk Weight(LTR)"

            gv.Columns("FAT(%)").IsVisible = True
            gv.Columns("FAT(%)").Width = 100
            gv.Columns("FAT(%)").HeaderText = " FAT(%)"

            gv.Columns("SNF(%)").IsVisible = True
            gv.Columns("SNF(%)").Width = 100
            gv.Columns("SNF(%)").HeaderText = "SNF(%)"

            gv.Columns("FAT(KG)").IsVisible = True
            gv.Columns("FAT(KG)").Width = 100
            gv.Columns("FAT(KG)").HeaderText = " FAT(KG)"

            gv.Columns("SNF(KG)").IsVisible = True
            gv.Columns("SNF(KG)").Width = 100
            gv.Columns("SNF(KG)").HeaderText = "SNF(KG)"

            gv.Columns("Cow Milk Qty (KG)").IsVisible = True
            gv.Columns("Cow Milk Qty (KG)").Width = 100
            gv.Columns("Cow Milk Qty (KG)").HeaderText = "Cow Milk Qty (KG)"

            gv.Columns("Cow FAT(%)").IsVisible = True
            gv.Columns("Cow FAT(%)").Width = 100
            gv.Columns("Cow FAT(%)").HeaderText = "Cow FAT(%)"

            gv.Columns("Cow SNF(%)").IsVisible = True
            gv.Columns("Cow SNF(%)").Width = 100
            gv.Columns("Cow SNF(%)").HeaderText = "Cow SNF(%)"

            gv.Columns("Cow FAT (KG)").IsVisible = True
            gv.Columns("Cow FAT (KG)").Width = 100
            gv.Columns("Cow FAT (KG)").HeaderText = "Cow FAT (KG)"

            gv.Columns("Cow SNF (KG)").IsVisible = True
            gv.Columns("Cow SNF (KG)").Width = 100
            gv.Columns("Cow SNF (KG)").HeaderText = "Cow SNF (KG)"

            gv.Columns("Buffalo Milk Qty (KG)").IsVisible = True
            gv.Columns("Buffalo Milk Qty (KG)").Width = 100
            gv.Columns("Buffalo Milk Qty (KG)").HeaderText = "Buffalo Milk Qty (KG)"

            gv.Columns("Buffalo SNF(%)").IsVisible = True
            gv.Columns("Buffalo SNF(%)").Width = 100
            gv.Columns("Buffalo SNF(%)").HeaderText = "Buffalo SNF(%)"

            gv.Columns("Buffalo FAT(%)").IsVisible = True
            gv.Columns("Buffalo FAT(%)").Width = 100

            gv.Columns("Buffalo FAT (KG)").IsVisible = True
            gv.Columns("Buffalo FAT (KG)").Width = 100
            gv.Columns("Buffalo FAT (KG)").HeaderText = "Buffalo FAT (KG)"



            gv.Columns("SRN Qty").IsVisible = True
            gv.Columns("SRN Qty").Width = 100
            gv.Columns("SRN Qty").HeaderText = "SRN Qty"


            gv.Columns("SRN Amount").IsVisible = True
            gv.Columns("SRN Amount").Width = 100
            gv.Columns("SRN Amount").HeaderText = "SRN Amount"


            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim intCount As Integer = 0

            Dim item1 As New GridViewSummaryItem("Milk Weight", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("Milk Weight(KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            Dim item3 As New GridViewSummaryItem("Milk Weight(LTR)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("FAT(KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)

            Dim item5 As New GridViewSummaryItem("SNF(KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)
            Dim summaryItem1 As New GridViewSummaryItem()
            summaryItem1.FormatString = "{0:F2}"
            summaryItem1.Name = "FAT(%)"
            summaryItem1.AggregateExpression = "sum([FAT(KG)])*100/sum([Milk Weight(KG)])"
            summaryRowItem.Add(summaryItem1)

            Dim summaryItem2 As New GridViewSummaryItem()
            summaryItem2.FormatString = "{0:F2}"
            summaryItem2.Name = "SNF(%)"
            summaryItem2.AggregateExpression = "sum([SNF(KG)])*100/sum([Milk Weight(KG)])"
            summaryRowItem.Add(summaryItem2)

            Dim item6 As New GridViewSummaryItem("Cow Milk Qty (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item6)
            Dim item7 As New GridViewSummaryItem("Cow FAT (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item7)

            Dim item8 As New GridViewSummaryItem("Cow SNF (KG)", "{0:F2}", GridAggregateFunction.Sum)

            summaryRowItem.Add(item8)
            Dim summaryItem3 As New GridViewSummaryItem()
            summaryItem3.FormatString = "{0:F2}"
            summaryItem3.Name = "Cow SNF(%)"
            summaryItem3.AggregateExpression = "sum([Cow SNF (KG)])*100/sum([Cow Milk Qty (KG)])"
            summaryRowItem.Add(summaryItem3)

            Dim summaryItem4 As New GridViewSummaryItem()
            summaryItem4.FormatString = "{0:F2}"
            summaryItem4.Name = "Cow FAT(%)"
            summaryItem4.AggregateExpression = "sum([Cow FAT (KG)])*100/sum([Cow Milk Qty (KG)])"
            summaryRowItem.Add(summaryItem4)

            Dim item9 As New GridViewSummaryItem("Buffalo Milk Qty (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item9)
            Dim item10 As New GridViewSummaryItem("Buffalo FAT (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item10)
            Dim item11 As New GridViewSummaryItem("Buffalo SNF (KG)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item11)
            Dim summaryItem5 As New GridViewSummaryItem()
            summaryItem5.FormatString = "{0:F2}"
            summaryItem5.Name = "Buffalo FAT(%)"
            summaryItem5.AggregateExpression = "sum([Buffalo FAT (KG)])*100/sum([Buffalo Milk Qty (KG)])"
            summaryRowItem.Add(summaryItem5)

            Dim summaryItem6 As New GridViewSummaryItem()
            summaryItem6.FormatString = "{0:F2}"
            summaryItem6.Name = "Buffalo SNF(%)"
            summaryItem6.AggregateExpression = "sum([Buffalo SNF (KG)])*100/sum([Buffalo Milk Qty (KG)])"
            summaryRowItem.Add(summaryItem6)
            Dim item13 As New GridViewSummaryItem("SRN Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item13)
            Dim item12 As New GridViewSummaryItem("SRN Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item12)

            Dim item14 As New GridViewSummaryItem("STD Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item14)

            Dim item15 As New GridViewSummaryItem("Incentive Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item15)

            gv.ShowGroupPanel = False
            gv.MasterTemplate.AutoExpandGroups = True

            gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        End If
       



    End Sub
    Sub Reset()
        gv.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        'cbtMCCRouteVLCC.Enabled = rbtnMCCRouteVLCCSelect.IsChecked
        EnableDisableControl(True)
    End Sub

    Private Sub EnableDisableControl(ByVal val As Boolean)
        RadGroupBox1.Enabled = val

        RadGroupBox2.Enabled = val
    End Sub



    Private Sub LoadData(Optional ByVal BulkExport As Integer = 0)
        Try

            If txtFromDate.Value > txtToDate.Value Then
                txtFromDate.Focus()
                Throw New Exception("From date can not be greater then to Date")
            End If

            If cbtMCCRouteVLCC.CheckedValue.Count = 0 Then

                clsCommon.MyMessageBoxShow("Please select atleast single MCC or select all.", Me.Text)
                Exit Sub
            End If
            ' KUNAL > TICKET : BM00000009937 > DATE : 05-0CT-2016 > TASK ADDED COLS : IS_MANUAL, MACHINE_NO
            Dim FinalQuery As String = Nothing
            '===Case when final.[Milk Weight(KG)]=final.[Milk Weight(LTR)] then final.[Milk Weight(LTR)]/1.03 else [Milk Weight(LTR)]
            Dim qry As String = "Select final.[Milk Receipt Code] ,final.MCC as [MCC Code] ,final.[MCC Name] ,final.Date ,final.[Doc Date] ,final.Shift ," &
            "final.[Route Code],final.[Route Name] ,final.[Vehicle Code] ,final.[VSP Code],final.[VSP Name] ,final.[Vlc Uploader Code] ,final.[Vlc Code] ,final.[VLC Name] ," &
             " final.[Sample No] ,final.[No Of Cans] ,final.[Milk Weight],final.[Milk Weight(KG)]," &
                " final.[Milk Weight(LTR)]  as [Milk Weight(LTR)]," &
             " final.[FAT(%)] ,final.[SNF(%)] ,final.[FAT(KG)],final.[SNF(KG)] ,final.[Cow Milk Qty (KG)],final.[Cow FAT(%)],final.[Cow SNF(%)] , Case When final.[FAT(%)] <= 5 Then final.[FAT(KG)] Else 0 End [Cow FAT (KG)], Case When final.[FAT(%)] <= 5 Then final.[SNF(KG)] Else 0 End [Cow SNF (KG)]," &
             " final.[Buffalo Milk Qty (KG)],final.[Buffalo SNF(%)],final.[Buffalo FAT(%)], Case When final.[FAT(%)] > 5 Then final.[FAT(KG)] Else 0 End [Buffalo FAT (KG)], Case When final.[FAT(%)] > 5 Then final.[SNF(KG)] Else 0 End [Buffalo SNF (KG)],final.[Milk Type],final.[SRN No],final.[SRN Amount]," &
             " final.[SRN Qty],final.[STD Qty],final.[SRN Rate],final.[Shift Status] ,Invoice_no ,Invoice_Date , IS_MANUAL, MACHINE_NO,final.Incentive_Code as [Incentive Code],final.Incentive_Amount as [Incentive Amount],final.[Incentive Description] From (Select Case When TSPL_MILK_SAMPLE_DETAIL.FAT < 5 Then TSPL_MILK_SAMPLE_DETAIL.FAT Else 0 End [Cow FAT(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT < 5 Then TSPL_MILK_SAMPLE_DETAIL.SNF Else 0 End [Cow SNF(%)]," &
           " Case When TSPL_MILK_SAMPLE_DETAIL.FAT > 5 Then TSPL_MILK_SAMPLE_DETAIL.FAT Else 0 End [Buffalo FAT(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT > 5 Then TSPL_MILK_SAMPLE_DETAIL.SNF Else 0 End [Buffalo SNF(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT <= 5 Then TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT Else 0 End [Cow Milk Qty (KG)]," &
           " Case When TSPL_MILK_SAMPLE_DETAIL.FAT > 5 Then TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT Else 0 End [Buffalo Milk Qty (KG)], Case When Coalesce(TSPL_MILK_SAMPLE_DETAIL.FAT, 0) <= 0 Then '' When Coalesce(TSPL_MILK_SAMPLE_DETAIL.FAT, 0) <= 5 Then 'C' Else 'B' End As [Milk Type], TSPL_MILK_RECEIPT_HEAD.DOC_CODE As [Milk Receipt Code]," &
            " TSPL_MILK_RECEIPT_HEAD.MCC_CODE As MCC, TSPL_MCC_MASTER.MCC_NAME As [MCC Name], Convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) As Date, " &
        " Convert(varchar,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) As [Doc Date], Case When TSPL_MILK_RECEIPT_DETAIL.SHIFT = 'M' Then 'Morning' Else 'Evening' End As Shift, " &
        " TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE As [Route Code], TSPL_MCC_ROUTE_MASTER.Route_Name As [Route Name], TSPL_MILK_RECEIPT_DETAIL.VEHICLE_CODE As [Vehicle Code]," &
       " TSPL_MILK_SRN_HEAD.VSP_CODE As [VSP Code], TSPL_VENDOR_MASTER.Vendor_Name As [VSP Name], TSPL_VLC_MASTER_HEAD.VLC_Code As [Vlc Code]," &
       " TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name], TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO As [Sample No], " &
       " TSPL_MILK_RECEIPT_DETAIL.NO_OF_CANS As [No Of Cans], TSPL_MILK_RECEIPT_DETAIL.MILK_WEIGHT As [Milk Weight], TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT As [Milk Weight(KG)]," &
       " TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT_LTR As [Milk Weight(LTR)], TSPL_MILK_SAMPLE_DETAIL.FAT As [FAT(%)], TSPL_MILK_SAMPLE_DETAIL.SNF As [SNF(%)], Convert(decimal(18,2)," &
           " TSPL_MILK_SAMPLE_DETAIL.FAT * TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT / 100) As [FAT(KG)], Convert(decimal(18,2),TSPL_MILK_SAMPLE_DETAIL.SNF * TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT / 100) As [SNF(KG)], Case When TSPL_MILK_SAMPLE_DETAIL.IS_MANUAL = '' Then 'Auto' Else TSPL_MILK_SAMPLE_DETAIL.IS_MANUAL End As [Sample Status]," &
        " TSPL_MILK_SRN_HEAD.DOC_CODE As [SRN No], Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.AMOUNT) As [SRN Amount], TSPL_MILK_SRN_DETAIL.RATE As [SRN Rate], TSPL_MILK_SRN_DETAIL.Qty As [SRN Qty],round(((TSPL_MILK_SRN_detail.FAT_KG/Price_Chart.FAT_Pers) * (Price_Chart.Fat_ratio/100.00)+ " &
        " (TSPL_MILK_SRN_detail.SNF_KG/Price_Chart.SNF_Pers) * (Price_Chart.SNF_Ratio/100.00))*100,3) as [Std Qty], Case When TSPL_MILK_Shift_End_HEAD.DOC_CODE Is Null Then 'Open' Else 'Close' End [Shift Status],TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as Invoice_no," &
    " convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as Invoice_Date , tspl_milk_receipt_detail.IS_MANUAL , tspl_milk_receipt_detail.MACHINE_NO ,TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL.Incentive_Code ,isnull(TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL.Incentive_Amount,0) as Incentive_Amount,TSPL_INCENTIVE_MASTER_HEAD.DESCRIPTION AS [Incentive Description] From TSPL_MILK_RECEIPT_DETAIL Left Outer Join TSPL_MILK_RECEIPT_HEAD On TSPL_MILK_RECEIPT_HEAD.DOC_CODE = TSPL_MILK_RECEIPT_DETAIL.DOC_CODE Left Outer Join TSPL_MILK_SAMPLE_HEAD On TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE = TSPL_MILK_RECEIPT_HEAD.DOC_CODE" &
    " Left Outer Join TSPL_MILK_SAMPLE_DETAIL On TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO = TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO And TSPL_MILK_SAMPLE_DETAIL.DOC_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE " &
    " Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE And TSPL_MILK_SRN_HEAD.SAMPLE_NO = TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO Left Outer Join TSPL_MILK_SRN_DETAIL On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE" &
    " Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE " &
    " Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_RECEIPT_HEAD.MCC_CODE Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_CODE" &
     " Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_RECEIPT_DETAIL.VSP_CODE" &
     " Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE" &
    " Left Outer Join TSPL_Primary_Vehicle_Master On TSPL_Primary_Vehicle_Master.Vehicle_Code = TSPL_MCC_ROUTE_MASTER.Vehicle_Code " &
    " Left Outer Join TSPL_MILK_Shift_End_HEAD On TSPL_MILK_Shift_End_HEAD.MCC_CODE = TSPL_MILK_RECEIPT_HEAD.MCC_CODE " &
    " And convert(date,TSPL_MILK_Shift_End_HEAD.DOC_DATE,103) = convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) " &
    " And TSPL_MILK_Shift_End_HEAD.SHIFT = TSPL_MILK_RECEIPT_HEAD.SHIFT " &
    " Left Outer Join TSPL_MILK_Shift_End_Route_DETAIL On TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE = TSPL_MILK_Shift_End_HEAD.DOC_CODE " &
    " And TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE = TSPL_MCC_ROUTE_MASTER.Route_Code" &
    " Left Outer Join TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL on TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL.MILK_DOC_Code = TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE and  " &
    " TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL.MILK_SRN_Code=TSPL_MILK_SRN_HEAD.DOC_CODE and TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL.MILK_Item_Code=TSPL_MILK_PURCHASE_INVOICE_DETAIL.Item_Code " &
    " Left Outer Join TSPL_INCENTIVE_MASTER_HEAD on TSPL_INCENTIVE_MASTER_HEAD.INCENTIVE_CODE=TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL.Incentive_Code" &
    " LEFT JOIN (select distinct FAT_Pers,SNF_Pers,Ratio as Fat_ratio,SNF_Ratio, Milk_Rate,TSPL_MILK_PRICE_MASTER.Price_Code,TSPL_FAT_SNF_UPLOADER_MASTER.code " &
    " from TSPL_FAT_SNF_UPLOADER_MASTER inner join  TSPL_MILK_PRICE_MASTER  on TSPL_MILK_PRICE_MASTER.Price_Code=TSPL_FAT_SNF_UPLOADER_MASTER.Price_Code) as  Price_Chart " &
    " on TSPL_MILK_SRN_DETAIL.Price_Code=Price_Chart.Code " &
    " where 2 = 2 "
            qry += " and TSPL_MILK_RECEIPT_HEAD.DOC_DATE >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_RECEIPT_HEAD.DOC_DATE <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'"
            If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal Then
                qry += " and 2=( case when TSPL_MILK_RECEIPT_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_RECEIPT_HEAD.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_RECEIPT_DETAIL.SHIFT='M' then 3 else 2 end  )"
            End If
            If clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
                qry += " and 2=( case when TSPL_MILK_RECEIPT_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_RECEIPT_HEAD.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_RECEIPT_DETAIL.SHIFT='E' then 3 else 2 end  )"
            End If

            'If rbtnMCCRouteVLCCSelect.IsChecked Then'===update by preeti gupta Against Ticket No [BM00000008695]
            Dim arr As List(Of String) = Nothing
            If cbtMCCRouteVLCC.CheckedValue.Count > 0 Then
                arr = cbtMCCRouteVLCC.CheckedValue(1)
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    qry += "and TSPL_MILK_RECEIPT_HEAD.MCC_Code  IN (" + clsCommon.GetMulcallString(arr) + ") "
                Else
                    Throw New Exception("Please select at least one MCC")
                End If
            End If
            If cbtMCCRouteVLCC.CheckedValue.Count > 1 Then
                arr = cbtMCCRouteVLCC.CheckedValue(2)
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    qry += " and TSPL_MILK_RECEIPT_DETAIL .Route_Code in (" + clsCommon.GetMulcallString(arr) + ")  "
                Else
                    Throw New Exception("Please select at least one Route")
                End If
            End If
            '===============Update by Preeti Gupta Against Ticket No[BM00000008365]
            If cbtMCCRouteVLCC.CheckedValue.Count > 1 Then
                arr = cbtMCCRouteVLCC.CheckedValue(3)
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    qry += " and TSPL_MILK_RECEIPT_DETAIL.VLC_CODE in (" + clsCommon.GetMulcallString(arr) + ")  "
                Else
                    Throw New Exception("Please select at least one VLC Code")
                End If
            End If
            qry += " ) As final where 2=2 "

            'qry += "order by final.[Date]  "


            If ChkDetailWise.Checked Then
                FinalQuery = "" & qry & ""
            ElseIf rbtnVLCWise.Checked Then
                FinalQuery = "select aa.[MCC Code] ,aa.[MCC Name] ,aa.[Route Code] ,aa.[Route Name],aa.[Vlc Code] ,aa.[VLC Name],aa.[Milk Weight] ,aa.[Milk Weight(KG)]	,aa.[Milk Weight(LTR)] ,aa.[FAT(%)] ,aa.[SNF(%)] ,aa.[FAT(KG)] ,aa.[SNF(KG)] ,aa.[Cow Milk Qty (KG)] ,aa.[Cow FAT(%)] ,aa.[Cow SNF(%)] ,aa.[Cow FAT (KG)] ,aa.[Cow SNF (KG)] ,aa.[Buffalo Milk Qty (KG)] ,aa.[Buffalo FAT(%)] ,aa.[Buffalo SNF(%)] ,aa.[Buffalo FAT (KG)] ,aa.[Buffalo SNF (KG)] ,aa.[SRN Qty],aa.[STD Qty],aa.[SRN Amount] ,aa.[Incentive Code],aa.[Incentive Amount],aa.[Incentive Description]  from ( "
                FinalQuery += " select xxx.* ,"
                FinalQuery += "  case when [Cow Milk Qty (KG)] =0 then 0 else [Cow FAT (KG)]/[Cow Milk Qty (KG)] *100 end as [Cow FAT(%)],"
                FinalQuery += " case when [Cow Milk Qty (KG)] =0 then 0 else [Cow Snf (KG)]/[Cow Milk Qty (KG)] *100 end as [Cow SNF(%)],"
                FinalQuery += "  case when  [Buffalo Milk Qty (KG)] =0 then 0 else [Buffalo FAT (KG)]/[Buffalo Milk Qty (KG)] *100 end as [Buffalo FAT(%)],"
                FinalQuery += " case when  [Buffalo Milk Qty (KG)] =0 then 0 else [Buffalo SNF (KG)]/[Buffalo Milk Qty (KG)] *100 end as [Buffalo SNF(%)]"
                FinalQuery += " from ("
                FinalQuery += " select xx.*"
                FinalQuery += " from ( "
                FinalQuery += "select pp.[MCC Code]  as [MCC Code],max(pp.[MCC Name] )  as [MCC Name],pp.[Route Code] as [Route Code],max(pp.[Route Name] ) as [Route Name],pp.[Vlc Code],max([VLC Name]) as [VLC Name],sum([Milk Weight] ) as [Milk Weight],sum([Milk Weight(KG)] ) as [Milk Weight(KG)],sum([Milk Weight(LTR)] ) as [Milk Weight(LTR)],"
                FinalQuery += " case when sum([Milk Weight(KG)] )=0 then 0 else (sum([FAT(KG)] )/sum([Milk Weight(KG)] ))*100 end as [FAT(%)],"
                FinalQuery += " case when sum([Milk Weight(KG)] )=0 then 0 else (sum([SNF(KG)] )/sum([Milk Weight(KG)] ))*100 end as [SNF(%)]"
                FinalQuery += " ,sum([FAT(KG)] ) as [FAT(KG)] ,sum([SNF(KG)] ) as [SNF(KG)],"
                FinalQuery += " sum(pp.[Cow Milk Qty (KG)]) as [Cow Milk Qty (KG)],"
                FinalQuery += " sum([Buffalo Milk Qty (KG)]) as [Buffalo Milk Qty (KG)],"
                FinalQuery += " sum([SRN Qty]) as [SRN Qty],sum([STD Qty]) as [STD Qty] ,sum([Cow FAT (KG)]) as [Cow FAT (KG)], sum ([Cow SNF (KG)]) as [Cow SNF (KG)], sum([Buffalo FAT (KG)]) as [Buffalo FAT (KG)], sum( [Buffalo SNF (KG)]) as [Buffalo SNF (KG)],sum([SRN Amount]) as [SRN Amount],pp.[Incentive Code],sum(pp.[Incentive Amount]) as [Incentive Amount],max(pp.[Incentive Description]) as [Incentive Description] from ("
                FinalQuery += "" & qry & ""
                FinalQuery += " ) as  pp group by pp.[MCC Code],pp.[Route Code],pp.[Vlc Code],pp.[Incentive Code] "
                FinalQuery += " )as xx"
                FinalQuery += " ) as xxx"
                FinalQuery += " ) as aa"
            ElseIf chkRoutewise.Checked Then
                FinalQuery = "select aa.[MCC Code] ,aa.[MCC Name] ,aa.[Route Code] ,aa.[Route Name] ,aa.[Milk Weight] ,aa.[Milk Weight(KG)]	,aa.[Milk Weight(LTR)] ,aa.[FAT(%)] ,aa.[SNF(%)] ,aa.[FAT(KG)] ,aa.[SNF(KG)] ,aa.[Cow Milk Qty (KG)] ,aa.[Cow FAT(%)] ,aa.[Cow SNF(%)] ,aa.[Cow FAT (KG)] ,aa.[Cow SNF (KG)] ,aa.[Buffalo Milk Qty (KG)] ,aa.[Buffalo FAT(%)] ,aa.[Buffalo SNF(%)] ,aa.[Buffalo FAT (KG)] ,aa.[Buffalo SNF (KG)] ,aa.[SRN Qty],aa.[STD Qty],aa.[SRN Amount],aa.[Incentive Code],aa.[Incentive Amount],aa.[Incentive Description]  from ( "
                FinalQuery += " select xxx.* ,"
                FinalQuery += "  case when [Cow Milk Qty (KG)] =0 then 0 else [Cow FAT (KG)]/[Cow Milk Qty (KG)] *100 end as [Cow FAT(%)],"
                FinalQuery += " case when [Cow Milk Qty (KG)] =0 then 0 else [Cow Snf (KG)]/[Cow Milk Qty (KG)] *100 end as [Cow SNF(%)],"
                FinalQuery += "  case when  [Buffalo Milk Qty (KG)] =0 then 0 else [Buffalo FAT (KG)]/[Buffalo Milk Qty (KG)] *100 end as [Buffalo FAT(%)],"
                FinalQuery += " case when  [Buffalo Milk Qty (KG)] =0 then 0 else [Buffalo SNF (KG)]/[Buffalo Milk Qty (KG)] *100 end as [Buffalo SNF(%)]"
                FinalQuery += " from ("
                FinalQuery += " select xx.*"
                FinalQuery += " from ( "
                FinalQuery += "select pp.[MCC Code]  as [MCC Code],max(pp.[MCC Name] )  as [MCC Name],pp.[Route Code] as [Route Code],max(pp.[Route Name] ) as [Route Name],sum([Milk Weight] ) as [Milk Weight],sum([Milk Weight(KG)] ) as [Milk Weight(KG)],sum([Milk Weight(LTR)] ) as [Milk Weight(LTR)],"
                FinalQuery += " case when sum([Milk Weight(KG)] )=0 then 0 else (sum([FAT(KG)] )/sum([Milk Weight(KG)] ))*100 end as [FAT(%)],"
                FinalQuery += " case when sum([Milk Weight(KG)] )=0 then 0 else (sum([SNF(KG)] )/sum([Milk Weight(KG)] ))*100 end as [SNF(%)]"
                FinalQuery += " ,sum([FAT(KG)] ) as [FAT(KG)] ,sum([SNF(KG)] ) as [SNF(KG)],"
                FinalQuery += " sum(pp.[Cow Milk Qty (KG)]) as [Cow Milk Qty (KG)],"
                FinalQuery += " sum([Buffalo Milk Qty (KG)]) as [Buffalo Milk Qty (KG)],"
                FinalQuery += " sum([SRN Qty]) as [SRN Qty],sum([STD Qty]) as [STD Qty] ,sum([Cow FAT (KG)]) as [Cow FAT (KG)], sum ([Cow SNF (KG)]) as [Cow SNF (KG)], sum([Buffalo FAT (KG)]) as [Buffalo FAT (KG)], sum( [Buffalo SNF (KG)]) as [Buffalo SNF (KG)],sum([SRN Amount]) as [SRN Amount],pp.[Incentive Code],sum(pp.[Incentive Amount]) as [Incentive Amount],max(pp.[Incentive Description]) as [Incentive Description] from ("
                FinalQuery += "" & qry & ""
                FinalQuery += " ) as  pp group by pp.[MCC Code],pp.[Route Code],pp.[Incentive Code]"
                FinalQuery += " )as xx"
                FinalQuery += " ) as xxx"
                FinalQuery += " ) as aa"
            ElseIf ChkMCCWise.Checked Then
                FinalQuery = "select aa.[MCC Code] ,aa.[MCC Name] ,aa.[Milk Weight] ,aa.[Milk Weight(KG)]	,aa.[Milk Weight(LTR)] ,aa.[FAT(%)] ,aa.[SNF(%)] ,aa.[FAT(KG)] ,aa.[SNF(KG)] ,aa.[Cow Milk Qty (KG)] ,aa.[Cow FAT(%)] ,aa.[Cow SNF(%)] ,aa.[Cow FAT (KG)] ,aa.[Cow SNF (KG)] ,aa.[Buffalo Milk Qty (KG)] ,aa.[Buffalo FAT(%)] ,aa.[Buffalo SNF(%)] ,aa.[Buffalo FAT (KG)] ,aa.[Buffalo SNF (KG)] ,aa.[SRN Qty],aa.[STD Qty],aa.[SRN Amount],aa.[Incentive Code],aa.[Incentive Amount],aa.[Incentive Description]  from ( "
                FinalQuery += " select xxx.* ,"
                FinalQuery += "  case when [Cow Milk Qty (KG)] =0 then 0 else [Cow FAT (KG)]/[Cow Milk Qty (KG)] *100 end as [Cow FAT(%)],"
                FinalQuery += " case when [Cow Milk Qty (KG)] =0 then 0 else [Cow Snf (KG)]/[Cow Milk Qty (KG)] *100 end as [Cow SNF(%)],"
                FinalQuery += "  case when  [Buffalo Milk Qty (KG)] =0 then 0 else [Buffalo FAT (KG)]/[Buffalo Milk Qty (KG)] *100 end as [Buffalo FAT(%)],"
                FinalQuery += " case when  [Buffalo Milk Qty (KG)] =0 then 0 else [Buffalo SNF (KG)]/[Buffalo Milk Qty (KG)] *100 end as [Buffalo SNF(%)]"
                FinalQuery += " from ("
                FinalQuery += " select xx.*"
                FinalQuery += " from ( "
                FinalQuery += "select pp.[MCC Code]  as [MCC Code],max(pp.[MCC Name] )  as [MCC Name] ,sum([Milk Weight] ) as [Milk Weight],sum([Milk Weight(KG)] ) as [Milk Weight(KG)],sum([Milk Weight(LTR)] ) as [Milk Weight(LTR)],"
                FinalQuery += " case when sum([Milk Weight(KG)] )=0 then 0 else (sum([FAT(KG)] )/sum([Milk Weight(KG)] ))*100 end as [FAT(%)],"
                FinalQuery += " case when sum([Milk Weight(KG)] )=0 then 0 else (sum([SNF(KG)] )/sum([Milk Weight(KG)] ))*100 end as [SNF(%)]"
                FinalQuery += " ,sum([FAT(KG)] ) as [FAT(KG)] ,sum([SNF(KG)] ) as [SNF(KG)],"
                FinalQuery += " sum(pp.[Cow Milk Qty (KG)]) as [Cow Milk Qty (KG)],"
                FinalQuery += " sum([Buffalo Milk Qty (KG)]) as [Buffalo Milk Qty (KG)],"
                FinalQuery += " sum([SRN Qty]) as [SRN Qty],sum([STD Qty]) as [STD Qty] ,sum([Cow FAT (KG)]) as [Cow FAT (KG)], sum ([Cow SNF (KG)]) as [Cow SNF (KG)], sum([Buffalo FAT (KG)]) as [Buffalo FAT (KG)], sum( [Buffalo SNF (KG)]) as [Buffalo SNF (KG)],sum([SRN Amount]) as [SRN Amount],pp.[Incentive Code],sum(pp.[Incentive Amount]) as [Incentive Amount],max(pp.[Incentive Description]) as [Incentive Description] from ("
                FinalQuery += "" & qry & ""
                FinalQuery += " ) as  pp group by pp.[MCC Code],pp.[Incentive Code] "
                FinalQuery += " )as xx"
                FinalQuery += " ) as xxx"
                FinalQuery += " ) as aa"
            End If
            '' bulk export
            If BulkExport = 1 Then
                transportSql.BulkExport("VSP_Incentive_Register", FinalQuery, "", "csv")
                Exit Sub
            ElseIf BulkExport = 2 Then
                transportSql.BulkExport("VSP_Incentive_Register", FinalQuery, "", "xls")
                Exit Sub
            End If

            dt = clsDBFuncationality.GetDataTable(FinalQuery)
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dt
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            FormatGrid()

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            End If

            RadPageView1.SelectedPage = RadPageViewPage2
            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub FrmMCCMilkRegister_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LOCATIONRIGTHS()
        SetUserMgmtNew()
        ChkDetailWise.Checked = True
        'rbtnMCCRouteVLCCAll.IsChecked = True

        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")

        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        RadPageView1.SelectedPage = RadPageViewPage1
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()

        LoadMCCRouteVLCTree()

        LoadShiftFrom()
        LoadShiftTo()

        Reset()

    End Sub
    Private Function GetReportID() As String
        Dim ReportID As String = "MCCMR"
        If ChkMCCWise.Checked Then
            ReportID += "M"
        ElseIf chkRoutewise.Checked Then
            ReportID += "R"
        ElseIf rbtnVLCWise.Checked Then
            ReportID += "V"
        ElseIf chkRoutewise.Checked Then
            ReportID += "D"
        End If
        Return ReportID
    End Function
    Private Sub ReStoreGridLayout()
        Try
            Dim ReportID As String = GetReportID()
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv.Columns.Count - 1 Step ii + 1
                        gv.Columns(ii).IsVisible = False
                        gv.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub FrmMCCMilkRegister_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            LoadData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()

        End If
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = GetReportID()
        TemplateGridview = gv
        LoadData()
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        Dim ReportID As String = GetReportID()
        If clsCommon.myLen(ReportID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub rbtnMCCRouteVLCCAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs)
        'cbtMCCRouteVLCC.Enabled = rbtnMCCRouteVLCCSelect.IsChecked
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                Dim CompName As String = clsDBFuncationality.getSingleValue("Select Comp_Name from TSPL_COMPANY_MASTER Where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'")
                arrHeader.Add(CompName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptVSPIncentiveRegister & "'"))
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")


                'If rbtnMCCRouteVLCCSelect.IsChecked Then
                Dim arr As List(Of String)
                If cbtMCCRouteVLCC.CheckedText.Count > 0 Then
                    arr = cbtMCCRouteVLCC.CheckedText(1)
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        arrHeader.Add(("MCC : " + clsCommon.GetMulcallStringWithComma(arr) + " "))
                    End If
                End If
                If cbtMCCRouteVLCC.CheckedText.Count > 1 Then
                    arr = cbtMCCRouteVLCC.CheckedText(2)
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        arrHeader.Add(("Route : " + clsCommon.GetMulcallStringWithComma(arr) + " "))
                    End If
                End If
                If cbtMCCRouteVLCC.CheckedText.Count > 2 Then
                    arr = cbtMCCRouteVLCC.CheckedText(3)
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        arrHeader.Add(("VLC : " + clsCommon.GetMulcallStringWithComma(arr) + " "))
                    End If
                End If
                'End If

                If exporter = EnumExportTo.Excel Then
                    'Dim sfd As SaveFileDialog = New SaveFileDialog()
                    'Dim filePath As String
                    'sfd.FileName = Me.Text
                    'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                    'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    '    filePath = sfd.FileName
                    'Else
                    '    Exit Sub
                    'End If
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
                    'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                Else
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("VSP Milk Incentive", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub BulkExportCSV_Click(sender As Object, e As EventArgs) Handles BulkExportCSV.Click
        LoadData(1)
    End Sub

    Private Sub BulkExportXls_Click(sender As Object, e As EventArgs) Handles BulkExportXls.Click
        LoadData(2)
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        print(EnumExportTo.PDF)
    End Sub
End Class
