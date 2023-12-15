Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO


Public Class rptSalesLedgerReport
    Inherits FrmMainTranScreen

#Region "Variables"
    Const colGatePassDate As String = "colGatePassDate"
    Const colShiftName As String = "colShiftName"
    Const colItemQty As String = "colItemQty"
    Const colItemAmt As String = "colItemAmt"
    Const colTotalQty As String = "colTotalQty"
    Const colTotalAmt As String = "colTotalAmt"
    Const colDepositAmt As String = "colDepositAmt"
    Const colBalanceAmt As String = "colBalanceAmt"
    Const colDueAmt As String = "colDueAmt"
    Const colItemStrCodeQty As String = "colItemStrCodeQty"
    Const colItemStrCodeAmt As String = "colItemStrCodeAmt"
    Const colTotal As String = "colTotal"
    Const colTotalSum As String = "colTotalSum"

    Const ReportID As String = "SalesLedgerReport"
#End Region
    Private Sub rptSalesLedgerReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        funreset()

    End Sub

    Private Sub txtRoute__My_Click(sender As Object, e As EventArgs) Handles txtRoute._My_Click
        Try
            Dim qry As String = "select distinct TSPL_ROUTE_MASTER.ROUTE_NO as [ROUTE NO] ,TSPL_ROUTE_MASTER.Route_Desc as [ROUTE NAME] from TSPL_ROUTE_MASTER
            left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Route_No = TSPL_ROUTE_MASTER.Route_No "
            If txtZone.arrValueMember.Count > 0 Then
                qry += "where TSPL_CUSTOMER_MASTER.Zone_Code in (" + clsCommon.GetMulcallString(txtZone.arrValueMember) + ") "
            End If

            txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("LedgerRoute", qry, "[ROUTE NO]", "[ROUTE NAME]", txtRoute.arrValueMember, txtRoute.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub LoadBlankGrid()
        Dim qry As String = String.Empty
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.DataSource = Nothing
        gv1.Rows.AddNew()

        Dim repoGatePassDate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoGatePassDate.FormatString = ""
        repoGatePassDate.HeaderText = "Gate Pass Date"
        repoGatePassDate.Name = colGatePassDate
        repoGatePassDate.Width = 30
        repoGatePassDate.IsVisible = True
        repoGatePassDate.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoGatePassDate)

        Dim repoShiftName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoShiftName.FormatString = ""
        repoShiftName.HeaderText = "S"
        repoShiftName.Name = colShiftName
        repoShiftName.Width = 30
        repoShiftName.IsVisible = True
        repoShiftName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoShiftName)

        Dim repoINameQty As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        qry = "SELECT distinct TSPL_SD_SHIPMENT_DETAIL.Structure_Code,TSPL_SD_SHIPMENT_DETAIL.Item_Code, TSPL_ITEM_MASTER.Item_Desc, TSPL_ITEM_MASTER.Short_Description, TSPL_SD_SHIPMENT_DETAIL.Unit_code
             FROM TSPL_SD_SHIPMENT_DETAIL 
             left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code 
             left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE
             left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code
             where Convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) >=Convert(date,'" & txtFromDate.Value & "',103) 
             and Convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <= Convert(date,'" & txtToDate.Value & "',103)  ORDER BY Structure_Code "
        Dim dtQty As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dtQty IsNot Nothing AndAlso dtQty.Rows.Count > 0 Then
            Dim i As Integer = 1
            Dim obj As ItemValueClass = New ItemValueClass()

            For Each dr As DataRow In dtQty.Rows
                repoINameQty = New GridViewTextBoxColumn()
                repoINameQty.FormatString = ""
                repoINameQty.HeaderText = clsCommon.myCstr(dr("Short_Description"))
                obj = New ItemValueClass()
                obj.itemCode = clsCommon.myCstr(dr("Item_Code"))
                obj.itemDesc = clsCommon.myCstr(dr("Item_Desc"))
                obj.Unit_code = clsCommon.myCstr(dr("Unit_code"))
                obj.ShortDesc = clsCommon.myCstr(dr("Short_Description"))
                repoINameQty.Name = colItemQty + clsCommon.myCstr(i)
                repoINameQty.Width = 20
                repoINameQty.IsVisible = True
                repoINameQty.Tag = obj
                i = i + 1
                gv1.MasterTemplate.Columns.Add(repoINameQty)
            Next
        End If
        Dim repoTotalQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalQty.FormatString = ""
        repoTotalQty.HeaderText = "Total Qty"
        repoTotalQty.Name = colTotalQty
        repoTotalQty.Width = 50
        repoTotalQty.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTotalQty)

        Dim repoIAmt As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        qry = "SELECT distinct TSPL_SD_SHIPMENT_DETAIL.Structure_Code,TSPL_SD_SHIPMENT_DETAIL.Item_Code, TSPL_ITEM_MASTER.Item_Desc, TSPL_ITEM_MASTER.Short_Description, TSPL_SD_SHIPMENT_DETAIL.Unit_code
             FROM TSPL_SD_SHIPMENT_DETAIL 
             left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code 
             left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE
             left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code
             where Convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) >=Convert(date,'" & txtFromDate.Value & "',103) 
             and Convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <= Convert(date,'" & txtToDate.Value & "',103) ORDER BY Structure_Code "
        Dim dtAmt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dtAmt IsNot Nothing AndAlso dtAmt.Rows.Count > 0 Then
            Dim i As Integer = 1
            Dim obj As ItemValueClass = New ItemValueClass()
            For Each dr As DataRow In dtAmt.Rows
                repoIAmt = New GridViewTextBoxColumn()
                repoIAmt.FormatString = ""
                repoIAmt.HeaderText = clsCommon.myCstr(dr("Short_Description"))
                obj = New ItemValueClass()
                obj.itemCode = clsCommon.myCstr(dr("Item_Code"))
                obj.itemDesc = clsCommon.myCstr(dr("Item_Desc"))
                obj.Unit_code = clsCommon.myCstr(dr("Unit_code"))
                obj.ShortDesc = clsCommon.myCstr(dr("Short_Description"))
                repoIAmt.Name = colItemAmt + clsCommon.myCstr(i)
                repoIAmt.Width = 20
                repoIAmt.Tag = obj
                repoIAmt.IsVisible = True
                i = i + 1
                gv1.MasterTemplate.Columns.Add(repoIAmt)
            Next
        End If


        Dim repoTotalAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalAmt.FormatString = ""
        repoTotalAmt.HeaderText = "Total Amt"
        repoTotalAmt.Name = colTotalAmt
        repoTotalAmt.Width = 70
        repoTotalAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTotalAmt)

        Dim repoDepositAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDepositAmt.FormatString = ""
        repoDepositAmt.HeaderText = "Deposit Amt"
        repoDepositAmt.Name = colDepositAmt
        repoDepositAmt.Width = 70
        repoDepositAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDepositAmt)

        Dim repoDueAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDueAmt.FormatString = ""
        repoDueAmt.HeaderText = "Due Amt Int.Paid"
        repoDueAmt.Name = colDueAmt
        repoDueAmt.Width = 70
        repoDueAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDueAmt)

        Dim repoBalanceAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBalanceAmt.FormatString = ""
        repoBalanceAmt.HeaderText = "Balance Amount"
        repoBalanceAmt.Name = colBalanceAmt
        repoBalanceAmt.Width = 70
        repoBalanceAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoBalanceAmt)

        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.EnableFiltering = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        RadPageView1.SelectedPage = RadPageViewPage2
        ' View()
    End Sub

    Sub View()
        Try
            If gv1.Rows.Count > 0 Then
                Dim view As New ColumnGroupsViewDefinition()
                view.ColumnGroups.Add(New GridViewColumnGroup(""))
                view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colGatePassDate).Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colShiftName).Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("Quantity"))
                view.ColumnGroups.Add(New GridViewColumnGroup("Qy.Tot."))
                view.ColumnGroups.Add(New GridViewColumnGroup("Rate Amount"))
                view.ColumnGroups.Add(New GridViewColumnGroup("Total"))

                view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
                For col As Integer = 2 To gv1.Columns(colTotalQty).Index - 1
                    view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns(col).Name)
                Next
                view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns(colTotalQty).Name)

                view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())

                For col As Integer = gv1.Columns(colTotalQty).Index + 1 To gv1.Columns(colTotalAmt).Index - 1
                    view.ColumnGroups(3).Rows(1).ColumnNames.Add(gv1.Columns(col).Name)
                Next

                view.ColumnGroups(4).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(4).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv1.Columns(colTotalAmt).Name)
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv1.Columns(colDepositAmt).Name)
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv1.Columns(colDueAmt).Name)
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv1.Columns(colBalanceAmt).Name)

                gv1.ViewDefinition = view

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        Try
            Dim qry As String = "select Cust_Code as Code ,Customer_Name as  Name from TSPL_CUSTOMER_MASTER "

            If txtRoute.arrValueMember.Count > 0 Then
                qry += "where Route_No in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ") "
            End If
            txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("LedgerCustomer", qry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtZone__My_Click(sender As Object, e As EventArgs) Handles txtZone._My_Click
        Try
            Dim qry As String = "select distinct TSPL_ZONE_MASTER.Zone_Code,TSPL_ZONE_MASTER.Description as Name ,TSPL_ZONE_MASTER.City_Code as [City Code],TSPL_CITY_MASTER.City_Name as [City Name]  from TSPL_ZONE_MASTER left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code = TSPL_ZONE_MASTER.City_Code 
            left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Zone_Code = TSPL_ZONE_MASTER.Zone_Code"

            txtZone.arrValueMember = clsCommon.ShowMultipleSelectForm("LedgerZone", qry, "Code", "Name", txtZone.arrValueMember, txtZone.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        funreset()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click

        LoadData()
    End Sub

    Sub funreset()
        EnableDisableControls(True)
        gv1.DataSource = Nothing
        txtRoute.arrValueMember = Nothing
        txtCustomer.arrValueMember = Nothing
        txtZone.arrValueMember = Nothing
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        RadPageView1.SelectedPage = RadPageViewPage1
        rbtnRoute.IsChecked = True
    End Sub

    Private Sub EnableDisableControls(ByVal val As Boolean)
        RadGroupBox1.Enabled = val
    End Sub

    Private Sub LoadData()
        Try
            Dim BaseQry As String = "SELECT TSPL_SD_SHIPMENT_HEAD.Document_Code, case when TSPL_SD_SHIPMENT_HEAD.Shift_Type = 'PM' then 'E' else 'M' end as Shift_Type , TSPL_SD_SHIPMENT_HEAD.Document_Date,
TSPL_SD_SHIPMENT_DETAIL.Structure_Code,TSPL_SD_SHIPMENT_DETAIL.Item_Code, TSPL_ITEM_MASTER.Item_Desc, TSPL_ITEM_MASTER.Short_Description, TSPL_SD_SHIPMENT_DETAIL.Unit_code,
TSPL_SD_SHIPMENT_DETAIL.CRATE,
 TSPL_SD_SHIPMENT_DETAIL.Item_Net_Amt
 FROM TSPL_SD_SHIPMENT_DETAIL 
 left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code 
  left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE
  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code
where Convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) >=Convert(date,'" & txtFromDate.Value & "',103) 
and Convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <= Convert(date,'" & txtToDate.Value & "',103) "
            Dim FinalQuery As String = ""
            If rbtnZone.IsChecked Then
                BaseQry += "and TSPL_CUSTOMER_MASTER.Zone_Code in (" + clsCommon.GetMulcallString(txtZone.arrValueMember) + ")"

            ElseIf rbtnRoute.IsChecked Then
                BaseQry += "and TSPL_SD_SHIPMENT_HEAD.Route_No in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")"
            ElseIf rbtnCustomer.IsChecked Then
                BaseQry += "and TSPL_SD_SHIPMENT_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")"

            ElseIf rbtnDetail.IsChecked Then
                FinalQuery = BaseQry

            End If
            BaseQry += "ORDER BY TSPL_SD_SHIPMENT_HEAD.Document_Date, Shift_Type"
            FinalQuery = BaseQry
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(FinalQuery)

            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterView.Refresh()
            gv1.GroupDescriptors.Clear()
            gv1.EnableFiltering = True
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            If dt.Rows.Count > 0 Then
                'gv1.DataSource = dt
                LoadBlankGrid()
                gv1.BestFitColumns()

                Dim dblrows As Integer = 0
                For Each dr As DataRow In dt.Rows
                    Dim index As Integer = dt.Rows.IndexOf(dr)
                    Dim Document_Code As String
                    If index = 0 Then
                        Document_Code = clsCommon.myCstr(dt.Rows(index)("Document_Code"))
                        gv1.Rows(dblrows).Cells(colGatePassDate).Value = (dr("Document_Date"))
                        gv1.Rows(dblrows).Cells(colShiftName).Value = (dr("Shift_Type"))
                    Else
                        Document_Code = clsCommon.myCstr(dt.Rows(index - 1)("Document_Code"))
                        If clsCommon.CompairString(dr("Document_Code"), Document_Code) = CompairStringResult.Equal Then
                            gv1.Rows(dblrows).Cells(colGatePassDate).Value = clsCommon.myCstr(dt.Rows(index - 1)("Document_Date"))
                            gv1.Rows(dblrows).Cells(colShiftName).Value = clsCommon.myCstr(dt.Rows(index - 1)("Shift_Type"))

                        Else
                            dblrows = dblrows + 1
                            gv1.Rows.AddNew()
                            gv1.Rows(dblrows).Cells(colGatePassDate).Value = (dr("Document_Date"))
                            gv1.Rows(dblrows).Cells(colShiftName).Value = (dr("Shift_Type"))
                        End If
                    End If
                    Dim k As Integer = 1
                    For columns = gv1.Columns(colShiftName).Index + 1 To gv1.Columns(colTotal).Index - 1
                        Dim obj1 As ItemValueClass = TryCast(gv1.Columns(colItemQty + clsCommon.myCstr(k)).Tag, ItemValueClass)
                        k = k + 1
                        If clsCommon.CompairString(dr("Item_Code"), clsCommon.myCstr(obj1.itemCode)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(dr("Unit_code"), clsCommon.myCstr(obj1.Unit_code)) = CompairStringResult.Equal Then
                            gv1.Rows(dblrows).Cells(columns).Value = clsCommon.myCDecimal(dr("CRATE"))
                        End If

                    Next
                Next



                SetGridFormation()
                ReStoreGridLayout()
                gv1.MasterTemplate.AutoExpandGroups = True
                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub

    Sub SetGridFormation()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = True
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
        Next
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer = 0
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
                obj = Nothing
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
End Class

