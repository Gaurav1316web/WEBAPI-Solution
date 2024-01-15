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
Imports System.Data.SqlClient
Public Class rptMassBalanceReport
#Region "Variables"
    Dim FORMTYPE As String = Nothing
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim screenCode As String = Nothing
    Dim documentCode As String = Nothing
    Dim listOfUsers As New ArrayList()

    Public FilterON As Boolean = False
    Public FilterfromDate As Date
    Public FilterToDate As Date
    Public FilterisFG As Boolean = False
    Public FilterAlpha As String
#End Region

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExcel.Visible = MyBase.isExport
    End Sub

    Private Sub RptMassBalanceReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = fromDate.Value
        LoadReportType()
        cboType.SelectedValue = "Summary"
        RadPageView1.SelectedPage = RadPageViewPage1
        LoadLocation()
        rbtnLocationAll.IsChecked = True

        If FilterON Then
            fromDate.Value = FilterfromDate
            ToDate.Value = FilterToDate
            cboType.SelectedValue = "Detail"
            If FilterisFG Then
                rbtnFG.IsChecked = True
                rbtnSFG.IsChecked = False
            Else
                rbtnFG.IsChecked = False
                rbtnSFG.IsChecked = True
            End If
            Load_Data()
            If clsCommon.myLen(FilterAlpha) > 0 Then
                Gv1.ShowFilteringRow = True
                Gv1.EnableFiltering = True
                Dim filter As New FilterDescriptor()
                filter.PropertyName = "Alpha"
                filter.[Operator] = FilterOperator.IsEqualTo
                filter.Value = FilterAlpha
                filter.IsFilterEditor = True
                Gv1.FilterDescriptors.Add(filter)
            End If
        End If

    End Sub

    Private Sub gvLocation_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gvLocation.CellDoubleClick

        If clsCommon.myCBool(gvLocation.CurrentRow.Cells("SEL").Value) Then
            Dim frm As New FrmCategorySelect()
            frm.lvl = If(Form_ID = clsUserMgtCode.stockRecoNewJR, 4, 3)
            frm.strCode = clsCommon.myCstr(gvLocation.CurrentRow.Cells("CODE").Value)
            frm.arrIn = gvLocation.CurrentRow.Tag
            frm.ShowDialog()
            If Not frm.isCancel Then
                gvLocation.CurrentRow.Tag = frm.arrOut
            End If
        End If
    End Sub

    Sub LoadLocation()
        gvLocation.DataSource = Nothing
        Dim qry As String = " select cast( 0 as bit) as SEL,Location_Code as CODE,Location_Desc as NAME,case when Is_Jobwork=1 then 'Yes' else 'No' end as [Job Location] from TSPL_LOCATION_MASTER where 1=1 and ((Is_Section='N' and Is_Sub_Location='N' and Location_Type IN ('Physical','Logical','Virtual') ) or (CSA_Type='Y') ) "
        If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.stockRecoNewJR) = CompairStringResult.Equal Then
            qry += " and TSPL_LOCATION_MASTER.Location_Code in (select distinct coalesce(Main_Location_Code,'') as Main_Location from tspl_location_master where len(coalesce(Main_Location_Code,''))>0 and len(coalesce(Jobwork_Vendor,''))>0) "
        End If
        qry += " order by Location_Code"
        gvLocation.DataSource = clsDBFuncationality.GetDataTable(qry)

        gvLocation.Columns("SEL").ReadOnly = False
        gvLocation.Columns("SEL").Width = 30
        gvLocation.Columns("SEL").HeaderText = " "

        gvLocation.Columns("CODE").ReadOnly = True
        gvLocation.Columns("CODE").Width = 100
        gvLocation.Columns("CODE").HeaderText = "Code"

        gvLocation.Columns("NAME").ReadOnly = True
        gvLocation.Columns("NAME").Width = 200
        gvLocation.Columns("NAME").HeaderText = "Description"

        gvLocation.ShowGroupPanel = False
        gvLocation.AllowAddNewRow = False
        gvLocation.AllowColumnReorder = False
        gvLocation.AllowRowReorder = False
        gvLocation.EnableSorting = False
        gvLocation.ShowFilteringRow = True
        gvLocation.EnableFiltering = True
        gvLocation.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvLocation.MasterTemplate.ShowRowHeaderColumn = True
    End Sub
    Sub LoadReportType()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Shift")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "Summary"
        dr("Shift") = "Summary"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "Detail"
        dr("Shift") = "Detail"
        dt.Rows.Add(dr)

        cboType.DataSource = dt
        cboType.ValueMember = "Code"
    End Sub

    Public Sub Load_Data()
        Try
            If clsCommon.GetDateWithStartTime(fromDate.Value) > clsCommon.GetDateWithEndTime(ToDate.Value) Then
                common.clsCommon.MyMessageBoxShow(Me, "From-Date cannot be Greater than To-Date", Me.Text)
                fromDate.Focus()
                Exit Sub
            End If
            Dim strTotalInput As String = ""
            Dim strTotalOutput As String = ""
            Dim qry As String
            qry = clsDB.GetQueryMassBalance(GetLocation(), fromDate.Value, ToDate.Value, IIf(rbtnSFG.IsChecked, 1, IIf(rbtnFG.IsChecked, 2, IIf(rbtnBoth.IsChecked, 3, 4))), "", "", False, strTotalInput, strTotalOutput)
            Dim strColPraticularName As String = "ParticularName"
            If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Summary") = CompairStringResult.Equal Then
                qry = "select Alpha,case when max(Trans)='' then max(ParticularName) else max(Trans) end as Trans,sum(QtyKg) as QtyKg,sum(QtyLtr) as QtyLtr,case when sum(QtyKg)=0 then 0 else cast( sum(Fat_KG)*100/sum(QtyKg)as decimal(18,2)) end as Fat_Per, case when sum(QtyKg)=0 then 0 else CAST(sum(SNF_KG)*100/sum(QtyKg)as decimal(18,2)) end as SNF_Per ,sum(Fat_KG) as Fat_KG, sum(SNF_KG) as SNF_KG, sum(Avg_Cost)  as Avg_Cost,max(Product_Type) as Product_Type   from (" + Environment.NewLine + qry + Environment.NewLine + ")xxx group by Alpha"
                strColPraticularName = "Trans"
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            dt.Columns.Add("IsBOLD", GetType(Integer))
            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                AddTotalRows(dt, strColPraticularName)

                qry = strTotalInput + Environment.NewLine + " Union all " + Environment.NewLine + strTotalOutput
                Dim dtTotal As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dtTotal IsNot Nothing AndAlso dtTotal.Rows.Count > 0 Then
                    Dim drKg As DataRow = dt.NewRow()
                    drKg("Alpha") = "X"
                    drKg(strColPraticularName) = "Kg FAT & Kg SNF (-Loss)/Gain"
                    Dim dblDrFatKg As Double = Math.Round(clsCommon.myCdbl(dtTotal.Rows(1)("Fat_KG")) - clsCommon.myCdbl(dtTotal.Rows(0)("Fat_KG")), 2, MidpointRounding.ToEven)
                    Dim dblDrSnfKg As Double = Math.Round(clsCommon.myCdbl(dtTotal.Rows(1)("SNF_KG")) - clsCommon.myCdbl(dtTotal.Rows(0)("SNF_KG")), 2, MidpointRounding.ToEven)
                    Dim dblDrAvgCost As Double = Math.Round(clsCommon.myCdbl(dtTotal.Rows(1)("Avg_Cost")) - clsCommon.myCdbl(dtTotal.Rows(0)("Avg_Cost")), 2, MidpointRounding.ToEven)

                    drKg("Fat_KG") = Math.Round(clsCommon.myCdbl(dtTotal.Rows(1)("Fat_KG")) - clsCommon.myCdbl(dtTotal.Rows(0)("Fat_KG")), 2, MidpointRounding.ToEven) ''IIf(Math.Round(clsCommon.myCdbl(dtTotal.Rows(1)("Fat_KG")) - clsCommon.myCdbl(dtTotal.Rows(0)("Fat_KG")), 2, MidpointRounding.ToEven) < 0, "(" + clsCommon.myCstr(Math.Round(clsCommon.myCdbl(dtTotal.Rows(1)("Fat_KG")) - clsCommon.myCdbl(dtTotal.Rows(0)("Fat_KG")), 2, MidpointRounding.ToEven)) + ")", Math.Round(clsCommon.myCdbl(dtTotal.Rows(1)("Fat_KG")) - clsCommon.myCdbl(dtTotal.Rows(0)("Fat_KG")), 2, MidpointRounding.ToEven))
                    drKg("SNF_KG") = Math.Round(clsCommon.myCdbl(dtTotal.Rows(1)("SNF_KG")) - clsCommon.myCdbl(dtTotal.Rows(0)("SNF_KG")), 2, MidpointRounding.ToEven) ''IIf(Math.Round(clsCommon.myCdbl(dtTotal.Rows(1)("SNF_KG")) - clsCommon.myCdbl(dtTotal.Rows(0)("SNF_KG")), 2, MidpointRounding.ToEven) < 0, "(" + clsCommon.myCstr(Math.Round(clsCommon.myCdbl(dtTotal.Rows(1)("SNF_KG")) - clsCommon.myCdbl(dtTotal.Rows(0)("SNF_KG")), 2, MidpointRounding.ToEven)) + ")", Math.Round(clsCommon.myCdbl(dtTotal.Rows(1)("SNF_KG")) - clsCommon.myCdbl(dtTotal.Rows(0)("SNF_KG")), 2, MidpointRounding.ToEven))
                    drKg("Avg_Cost") = Math.Round(clsCommon.myCdbl(dtTotal.Rows(1)("Avg_Cost")) - clsCommon.myCdbl(dtTotal.Rows(0)("Avg_Cost")), 2, MidpointRounding.ToEven) '' IIf(Math.Round(clsCommon.myCdbl(dtTotal.Rows(1)("Avg_Cost")) - clsCommon.myCdbl(dtTotal.Rows(0)("Avg_Cost")), 2, MidpointRounding.ToEven) < 0, "(" + clsCommon.myCstr(Math.Round(clsCommon.myCdbl(dtTotal.Rows(1)("Avg_Cost")) - clsCommon.myCdbl(dtTotal.Rows(0)("Avg_Cost")), 2, MidpointRounding.ToEven)) + ")", Math.Round(clsCommon.myCdbl(dtTotal.Rows(1)("Avg_Cost")) - clsCommon.myCdbl(dtTotal.Rows(0)("Avg_Cost")), 2, MidpointRounding.ToEven))

                    Dim drPer As DataRow = dt.NewRow()
                    drPer("Alpha") = "Y"
                    drPer(strColPraticularName) = "Kg FAT & Kg SNF (-Loss)/Gain %"

                    If clsCommon.myCdbl(dtTotal.Rows(0)("Fat_KG")) <> 0 Then
                        drPer("Fat_KG") = Math.Round((clsCommon.myCdbl(dblDrFatKg) * 100) / clsCommon.myCdbl(dtTotal.Rows(0)("Fat_KG")), 2, MidpointRounding.ToEven) ''IIf(Math.Round((clsCommon.myCdbl(dblDrFatKg) * 100) / clsCommon.myCdbl(dtTotal.Rows(0)("Fat_KG")), 2, MidpointRounding.ToEven) < 0, "(" + clsCommon.myCstr(Math.Round((clsCommon.myCdbl(dblDrFatKg) * 100) / clsCommon.myCdbl(dtTotal.Rows(0)("Fat_KG")), 2, MidpointRounding.ToEven)) + ")", Math.Round((clsCommon.myCdbl(dblDrFatKg) * 100) / clsCommon.myCdbl(dtTotal.Rows(0)("Fat_KG")), 2, MidpointRounding.ToEven))
                    End If
                    Dim ddd As String = clsCommon.myCdbl(dtTotal.Rows(0)("Fat_KG"))
                    If clsCommon.myCdbl(dtTotal.Rows(0)("SNF_KG")) <> 0 Then
                        drPer("SNF_KG") = Math.Round((clsCommon.myCdbl(dblDrSnfKg) * 100) / clsCommon.myCdbl(dtTotal.Rows(0)("SNF_KG")), 2, MidpointRounding.ToEven) ''IIf(Math.Round((clsCommon.myCdbl(dblDrSnfKg) * 100) / clsCommon.myCdbl(dtTotal.Rows(0)("SNF_KG")), 2, MidpointRounding.ToEven) < 0, "(" + clsCommon.myCstr(Math.Round((clsCommon.myCdbl(dblDrSnfKg) * 100) / clsCommon.myCdbl(dtTotal.Rows(0)("SNF_KG")), 2, MidpointRounding.ToEven)) + ")", Math.Round((clsCommon.myCdbl(dblDrSnfKg) * 100) / clsCommon.myCdbl(dtTotal.Rows(0)("SNF_KG")), 2, MidpointRounding.ToEven))
                    End If
                    If clsCommon.myCdbl(dtTotal.Rows(0)("Avg_Cost")) <> 0 Then
                        drPer("Avg_Cost") = Math.Round((clsCommon.myCdbl(dblDrAvgCost) * 100) / clsCommon.myCdbl(dtTotal.Rows(0)("Avg_Cost")), 2, MidpointRounding.ToEven) '' IIf(Math.Round((clsCommon.myCdbl(dblDrAvgCost) * 100) / clsCommon.myCdbl(dtTotal.Rows(0)("Avg_Cost")), 2, MidpointRounding.ToEven) < 0, "(" + clsCommon.myCstr(Math.Round((clsCommon.myCdbl(dblDrAvgCost) * 100) / clsCommon.myCdbl(dtTotal.Rows(0)("Avg_Cost")), 2, MidpointRounding.ToEven)) + ")", Math.Round((clsCommon.myCdbl(dblDrAvgCost) * 100) / clsCommon.myCdbl(dtTotal.Rows(0)("Avg_Cost")), 2, MidpointRounding.ToEven))
                    End If

                    Dim drTS As DataRow = dt.NewRow()
                    drTS("Alpha") = "Z"
                    drTS(strColPraticularName) = "Total TS (-Loss)/Gain %"
                    drTS("Fat_KG") = Math.Round(clsCommon.myCDivide(((clsCommon.myCdbl(dblDrFatKg) + clsCommon.myCdbl(dblDrSnfKg)) * 100), (clsCommon.myCdbl(dtTotal.Rows(0)("Fat_KG")) + clsCommon.myCdbl(dtTotal.Rows(0)("SNF_KG")))), 2, MidpointRounding.ToEven) ''IIf(Math.Round(clsCommon.myCDivide(((clsCommon.myCdbl(dblDrFatKg) + clsCommon.myCdbl(dblDrSnfKg)) * 100), (clsCommon.myCdbl(dtTotal.Rows(0)("Fat_KG")) + clsCommon.myCdbl(dtTotal.Rows(0)("SNF_KG")))), 2, MidpointRounding.ToEven) < 0, "(" + clsCommon.myCstr(Math.Round(clsCommon.myCDivide(((clsCommon.myCdbl(dblDrFatKg) + clsCommon.myCdbl(dblDrSnfKg)) * 100), (clsCommon.myCdbl(dtTotal.Rows(0)("Fat_KG")) + clsCommon.myCdbl(dtTotal.Rows(0)("SNF_KG")))), 2, MidpointRounding.ToEven)) + ")", Math.Round(clsCommon.myCDivide(((clsCommon.myCdbl(dblDrFatKg) + clsCommon.myCdbl(dblDrSnfKg)) * 100), (clsCommon.myCdbl(dtTotal.Rows(0)("Fat_KG")) + clsCommon.myCdbl(dtTotal.Rows(0)("SNF_KG")))), 2, MidpointRounding.ToEven))

                    dt.Rows.Add(drKg)
                    dt.Rows.Add(drPer)
                    dt.Rows.Add(drTS)
                End If

                Gv1.DataSource = Nothing
                Gv1.Rows.Clear()
                Gv1.Columns.Clear()
                Gv1.DataSource = dt
                Gv1.GroupDescriptors.Clear()
                Gv1.ShowGroupPanel = False
                Gv1.MasterTemplate.SummaryRowsBottom.Clear()
                Gv1.EnableFiltering = True
                Gv1.ShowFilteringRow = True
                RadPageView1.SelectedPage = RadPageViewPage2
                For ii As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(ii).ReadOnly = True
                    Gv1.Columns(ii).IsVisible = True
                Next
                Gv1.BestFitColumns()
                Gv1.Columns("Product_Type").IsVisible = False
                Gv1.Columns("IsBOLD").IsVisible = False
                Gv1.Columns("Alpha").HeaderText = "Alpha"
                If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Detail") = CompairStringResult.Equal Then
                    Gv1.Columns("ParticularCode").HeaderText = "Particular Code"
                    Gv1.Columns("ParticularName").HeaderText = "Particular"
                End If
                Gv1.Columns("QtyKg").HeaderText = "Qty KG"
                Gv1.Columns("QtyLtr").HeaderText = "Qty Ltr"
                Gv1.Columns("Fat_Per").HeaderText = "FAT %"
                Gv1.Columns("SNF_Per").HeaderText = "SNF %"
                Gv1.Columns("Fat_KG").HeaderText = "FAT Kg"
                Gv1.Columns("SNF_KG").HeaderText = "SNF Kg"
                Gv1.Columns("Avg_Cost").HeaderText = "Amount"

                EnableDisableAllcontrol(False)
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to display", Me.Text)
            End If
            Gv1.BestFitColumns()
            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub AddTotalRows(ByRef dt As DataTable, ByVal strColPraticularName As String)
        If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Detail") = CompairStringResult.Equal Then
            Dim arr As New Dictionary(Of Integer, DataRow)
            Dim TotStockQty As Decimal = 0
            Dim TotQtyKg As Decimal = 0
            Dim TotQtyLtr As Decimal = 0
            Dim TotFATKg As Decimal = 0
            Dim TotSNFKg As Decimal = 0
            Dim TotAvgCost As Decimal = 0
            Dim strPreviousAlpha As String = clsCommon.myCstr(dt.Rows(0)("Alpha"))
            Dim strPreviousTrans As String = clsCommon.myCstr(dt.Rows(0)("Trans"))
            For ii As Integer = 0 To dt.Rows.Count - 1
                Dim flag As Boolean = False
                If clsCommon.CompairString(strPreviousAlpha, clsCommon.myCstr(dt.Rows(ii)("Alpha"))) = CompairStringResult.Equal Then
                    TotStockQty += clsCommon.myCdbl(dt.Rows(ii)("Stock_Qty"))
                    TotQtyKg += clsCommon.myCdbl(dt.Rows(ii)("QtyKg"))
                    TotQtyLtr += clsCommon.myCdbl(dt.Rows(ii)("QtyLtr"))
                    TotFATKg += clsCommon.myCdbl(dt.Rows(ii)("Fat_KG"))
                    TotSNFKg += clsCommon.myCdbl(dt.Rows(ii)("SNF_KG"))
                    TotAvgCost += clsCommon.myCdbl(dt.Rows(ii)("Avg_Cost"))
                Else
                    Dim drTS As DataRow = dt.NewRow()
                    drTS("Alpha") = strPreviousAlpha
                    drTS(strColPraticularName) = "Total " + strPreviousTrans
                    drTS("Stock_Qty") = TotStockQty
                    drTS("QtyKg") = TotQtyKg
                    drTS("QtyLtr") = TotQtyLtr
                    drTS("Fat_KG") = TotFATKg
                    drTS("SNF_KG") = TotSNFKg
                    drTS("Fat_Per") = Math.Round(clsCommon.myCDivide(TotFATKg * 100, TotQtyKg), 2)
                    drTS("SNF_Per") = Math.Round(clsCommon.myCDivide(TotSNFKg * 100, TotQtyKg), 2)
                    drTS("Avg_Cost") = TotAvgCost
                    drTS("IsBOLD") = 1

                    TotStockQty = clsCommon.myCdbl(dt.Rows(ii)("Stock_Qty"))
                    TotQtyKg = clsCommon.myCdbl(dt.Rows(ii)("QtyKg"))
                    TotQtyLtr = clsCommon.myCdbl(dt.Rows(ii)("QtyLtr"))
                    TotFATKg = clsCommon.myCdbl(dt.Rows(ii)("Fat_KG"))
                    TotSNFKg = clsCommon.myCdbl(dt.Rows(ii)("SNF_KG"))
                    TotAvgCost = clsCommon.myCdbl(dt.Rows(ii)("Avg_Cost"))

                    strPreviousAlpha = clsCommon.myCstr(dt.Rows(ii)("Alpha"))
                    strPreviousTrans = clsCommon.myCstr(dt.Rows(ii)("Trans"))
                    arr.Add(ii, drTS)

                End If

                If dt.Rows.Count - 1 = ii Then
                    Dim drTS As DataRow = dt.NewRow()
                    drTS("Alpha") = strPreviousAlpha
                    drTS(strColPraticularName) = "Total " + strPreviousTrans
                    drTS("Stock_Qty") = TotStockQty
                    drTS("QtyKg") = TotQtyKg
                    drTS("QtyLtr") = TotQtyLtr
                    drTS("Fat_KG") = TotFATKg
                    drTS("SNF_KG") = TotSNFKg
                    drTS("Fat_Per") = Math.Round(clsCommon.myCDivide(TotFATKg * 100, TotQtyKg), 2)
                    drTS("SNF_Per") = Math.Round(clsCommon.myCDivide(TotSNFKg * 100, TotQtyKg), 2)
                    drTS("Avg_Cost") = TotAvgCost
                    drTS("IsBOLD") = 1
                    strPreviousAlpha = clsCommon.myCstr(dt.Rows(ii)("Alpha"))
                    strPreviousTrans = clsCommon.myCstr(dt.Rows(ii)("Trans"))
                    arr.Add(ii + 1, drTS)
                End If

            Next
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For ii As Integer = arr.Count - 1 To 0 Step -1
                    Dim Key As Integer = arr.Keys(ii)
                    If Not clsCommon.CompairString(clsCommon.myCstr(arr(Key)(strColPraticularName)), "Total") = CompairStringResult.Equal Then
                        dt.Rows.InsertAt(arr(Key), Key)
                    End If
                Next
            End If
        End If
    End Sub

    Function GetLocation() As ArrayList
        Dim strWhrCatg As String = ""
        Dim qry As String
        Dim arrLocation As ArrayList = Nothing
        If rbtnLocationSelect.IsChecked Then
            Dim IsApplicable As Boolean = False
            For ii As Integer = 0 To gvLocation.RowCount - 1
                If clsCommon.myCBool(gvLocation.Rows(ii).Cells("SEL").Value) Then
                    If IsApplicable Then
                        strWhrCatg += " Or "
                    End If
                    strWhrCatg += " ((case when Is_Section='N' and Is_Sub_Location='N' then Location_Code else Main_Location_Code end) = '" + clsCommon.myCstr(gvLocation.Rows(ii).Cells("CODE").Value) + "') "
                    IsApplicable = True
                    Dim arr As Dictionary(Of String, Object) = gvLocation.Rows(ii).Tag
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        strWhrCatg += " and Location_Code in ("
                        Dim isFirstTime As Boolean = True
                        For Each strInn As String In arr.Keys
                            If Not isFirstTime Then
                                strWhrCatg += ","
                            End If
                            strWhrCatg += "'" + strInn + "'"
                            isFirstTime = False
                        Next
                        strWhrCatg += ")"
                    End If
                End If
            Next
            If Not IsApplicable Then
                Throw New Exception("Please select at least one location")
            End If
            qry = "select Location_Code from TSPL_LOCATION_MASTER where 2=2 and (" + strWhrCatg + ")"
            Dim dtLoc As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dtLoc IsNot Nothing AndAlso dtLoc.Rows.Count > 0 Then
                arrLocation = New ArrayList
                For Each dr As DataRow In dtLoc.Rows
                    arrLocation.Add(dr("Location_Code"))
                Next
            End If
        End If
        Return arrLocation
    End Function

    Sub Reset()
        Gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        EnableDisableAllcontrol(True)
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            PageSetupReport_ID = GetReportID()
            Load_Data()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub EnableDisableAllcontrol(ByVal val As Boolean)
        RadGroupBox1.Enabled = val
        RadGroupBox3.Enabled = val
        cboType.Enabled = val
        RadGroupBox2.Enabled = val
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub btnSaveLayout_Click(sender As Object, e As EventArgs) Handles btnSaveLayout.Click
        If clsCommon.myLen(GetReportID()) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = GetReportID()
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = Gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", Me.Text)
            End If
            ' stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub
    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(GetReportID(), objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(GetReportID()) > 0 Then
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

    Function GetReportID() As String
        Dim str As String = MyBase.Form_ID
        If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Summary") = CompairStringResult.Equal Then
            str += "S"
        End If
        Return str
    End Function

    Private Sub ExpExcel_Click(sender As Object, e As EventArgs) Handles ExpExcel.Click
        Try
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub Gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        Try
            If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Detail") = CompairStringResult.Equal Then
                If Not (clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans").Value), "Opening Balance") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans").Value), "Closing Balance") = CompairStringResult.Equal) AndAlso clsCommon.myLen(Gv1.CurrentRow.Cells("Trans").Value) > 0 Then
                    Dim qry As String = clsDB.GetQueryMassBalance(GetLocation(), fromDate.Value, ToDate.Value, IIf(rbtnSFG.IsChecked, 1, IIf(rbtnFG.IsChecked, 2, IIf(rbtnBoth.IsChecked, 3, 4))), clsCommon.myCstr(Gv1.CurrentRow.Cells("Alpha").Value), clsCommon.myCstr(Gv1.CurrentRow.Cells("ParticularCode").Value), True, "", "")
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If dt IsNot Nothing And dt.Rows.Count > 0 Then
                        gvDetail.DataSource = Nothing
                        gvDetail.Rows.Clear()
                        gvDetail.Columns.Clear()
                        gvDetail.DataSource = dt
                        gvDetail.GroupDescriptors.Clear()
                        gvDetail.ShowGroupPanel = False
                        gvDetail.MasterTemplate.SummaryRowsBottom.Clear()
                        gvDetail.EnableFiltering = False
                        RadPageView1.SelectedPage = RadPageViewPage3
                        For ii As Integer = 0 To gvDetail.Columns.Count - 1
                            gvDetail.Columns(ii).ReadOnly = True
                            gvDetail.Columns(ii).IsVisible = True
                        Next
                        gvDetail.BestFitColumns()

                        gvDetail.Columns("Source_Doc_No").HeaderText = "Document No"
                        gvDetail.Columns("Punching_Date").HeaderText = "Document Date"
                        gvDetail.Columns("Vendor_Code").HeaderText = "Vendor"
                        gvDetail.Columns("Cust_Code").HeaderText = "Customer"
                        gvDetail.Columns("Trans_Type").HeaderText = "Transaction"
                        gvDetail.Columns("Item_Code").HeaderText = "Item"
                        gvDetail.Columns("Location_Code").HeaderText = "Locatioin"
                        gvDetail.Columns("Other_Location_Code").HeaderText = "Other Location"
                        gvDetail.Columns("QtyKg").HeaderText = "Qty KG"
                        gvDetail.Columns("QtyLtr").HeaderText = "Qty Ltr"
                        gvDetail.Columns("Fat_Per").HeaderText = "FAT %"
                        gvDetail.Columns("SNF_Per").HeaderText = "SNF %"
                        gvDetail.Columns("Fat_KG").HeaderText = "FAT Kg"
                        gvDetail.Columns("Fat_KG").FormatString = "{0:N2}"
                        gvDetail.Columns("SNF_KG").HeaderText = "SNF Kg"
                        gvDetail.Columns("SNF_KG").FormatString = "{0:N2}"
                        gvDetail.Columns("Avg_Cost").HeaderText = "Amount"

                        Dim summaryRowItem As New GridViewSummaryRowItem()
                        Dim Smitem As New GridViewSummaryItem("QtyLtr", "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(Smitem)

                        Smitem = New GridViewSummaryItem("QtyKg", "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(Smitem)

                        Smitem = New GridViewSummaryItem("Fat_KG", "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(Smitem)
                        Smitem = New GridViewSummaryItem("SNF_KG", "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(Smitem)

                        Smitem = New GridViewSummaryItem("Avg_Cost", "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(Smitem)

                        gvDetail.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                    Else
                        clsCommon.MyMessageBoxShow(Me, "No data found to display", Me.Text)
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub GvDetail_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gvDetail.CellDoubleClick
        Try
            Dim strTransType As String = clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value)
            Dim strTransCode As String = clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value)
            If clsCommon.myLen(strTransType) > 0 AndAlso clsCommon.myLen(strTransCode) > 0 Then
                Select Case strTransType
                    Case "IC-AD"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnStoreAdjustment, strTransCode)
                    Case "ISSTRAN"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnIssueReturn, strTransCode)
                    Case "SRN"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnSRN, strTransCode)
                    Case "SD-IN"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmShipmentProductSale, strTransCode)
                    Case "Sale Return"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSNSaleReturn, strTransCode)
                    Case "SD-CSATRANS"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSATransfer, strTransCode)
                    Case "SD-SH"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmShipmentProductSale, strTransCode)
                    Case "CSA-SALE"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSASaleInvoice, strTransCode)
                    Case "RICE-MIX"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmRiceMixingEntry, strTransCode)
                    Case "RICE-PROC"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmRiceProcessingEntry, strTransCode)
                    Case "PP_ISSUE"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProcessProductionIssueEntry, strTransCode)
                    Case "PP_STDN"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProcessProductionStandardization, strTransCode)
                            'Case "BulkSRNTrade"
                            '    clsOpenTransactionForm.OpenTransacionForm(EnumTransType.BulkSRNTrade, strTransCode)
                    'Case "DispatchBSTrade"
                    '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmDispatchBulkSaleTrade, strTransCode)
                    'Case "DispatchBSTrdReturn"
                    '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmDispatchBulkSaleTradeReturn, strTransCode)
                    Case "DispChallan"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCDispatch, strTransCode)
                    Case "MilkTransferIn"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkTransferIn, strTransCode)
                    Case "IC-AD"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnStoreAdjustment, strTransCode)
                    Case "DispatchBS"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmDispatchBulkSale, strTransCode)
                    Case "PRD_STG_PROC"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProcessProductionStageProcess, strTransCode)
                    Case "RGP"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnGatePass, strTransCode)
                    Case "NRGP"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnGatePass, strTransCode)
                    Case "PROD_ENTRY"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProductionEntry, strTransCode)
                    'Case "MCC-IISSUE"
                    '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmVSPItemIssue, strTransCode)
                    Case "FS-SH"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmDispatchMultipleFreshSale, strTransCode)
                    Case "PS-SH"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmShipmentProductSale, strTransCode)
                    Case "Transfer"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.Transfer, strTransCode)
                    Case "ITransfer"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.Transfer, strTransCode)
                    Case "BulkSRN"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmBulkMilkSRN, strTransCode)
                    Case "MCC-MSRN"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkSRN, strTransCode)
                    Case "SD-CSATRANS-RETURN"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSATransferReturn, strTransCode)
                    Case "JWO-SRN"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.JWO_SRN, strTransCode)
                    Case "JWO-SRN-RET"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.JWO_SRN_Return, strTransCode)
                    Case "MilkTransferInReturn"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkTransferInReturn, strTransCode)
                    Case "JW-TO"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkJobWorkTransferOther, strTransCode)
                    Case "JWO-Transfer-RET"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkJobWorkTransferOtherReturn, strTransCode)
                    Case "MilkTransferJobWork"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkJobWorkTransfer, strTransCode)
                    Case "MilkTransJWOReturn"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkJobWorkTransferReturn, strTransCode)
                    Case "RGPR"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnGatePass, strTransCode)
                    Case "BulkSRNRet"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmBulkMilkSRNReturn, strTransCode)
                    'Case "BulkSRNTrade"
                    '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmDispatchBulkSaleTradeReturn, strTransCode)
                    Case "Purchase Return"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseReturn, strTransCode)
                    Case "SRN-RET"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.SRNReturn, strTransCode)
                End Select
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub



    Private Sub rbtnLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnLocationAll.ToggleStateChanged, rbtnLocationSelect.ToggleStateChanged
        gvLocation.Enabled = rbtnLocationSelect.IsChecked
        RadButton4.Enabled = rbtnLocationSelect.IsChecked
        RadButton5.Enabled = rbtnLocationSelect.IsChecked
    End Sub

    Private Sub PDF_Click(sender As Object, e As EventArgs) Handles PDF.Click
        Try

            If Gv1.Rows.Count > 0 Then
                    Dim arrHeader As List(Of String) = New List(Of String)()
                    arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
                    transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                    clsCommon.MyExportToPDF(Me.Text, Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub Gv1_ViewCellFormatting(sender As Object, e As CellFormattingEventArgs) Handles Gv1.ViewCellFormatting
        Try
            If clsCommon.myCdbl(Gv1.Rows(e.RowIndex).Cells("IsBOLD").Value) = 1 Then
                e.CellElement.Font = New Font(e.CellElement.Font, FontStyle.Bold)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadButton4_Click(sender As Object, e As EventArgs) Handles RadButton4.Click
        CheckedAll(gvLocation)
    End Sub

    Private Sub RadButton5_Click(sender As Object, e As EventArgs) Handles RadButton5.Click
        UnCheckedAll(gvLocation)
    End Sub

    Private Sub CheckedAll(ByVal gv As RadGridView)
        For ii As Integer = 0 To gv.RowCount - 1
            gv.Rows(ii).Cells("SEL").Value = False
        Next
        For ii As Integer = 0 To gv.ChildRows.Count - 1
            gv.ChildRows(ii).Cells("SEL").Value = True
        Next
    End Sub
    Private Sub UnCheckedAll(ByVal gv As RadGridView)
        For ii As Integer = 0 To gv.RowCount - 1
            gv.Rows(ii).Cells("SEL").Value = False
        Next
    End Sub
End Class

