Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
'=============Created By Preeti Gupta==============
Public Class RptSectionWiseStockReport

    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptSectionWiseStockReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub
    Sub LoadItem()
        Dim qry As String = "select Item_Code as [Code] ,Item_Desc as [Name]  from TSPL_ITEM_MASTER  where Item_Type ='S' or Item_Type= 'F'"
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem.ValueMember = "Code"
        cbgItem.DisplayMember = "Name"
    End Sub
    Sub LoadSection()
        Dim qry As String = "select Section_Code as [Code],Description as [Description] from TSPL_SECTION_MASTER "
        cbgSection.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgSection.ValueMember = "Code"
        cbgSection.DisplayMember = "Name"
    End Sub
    Sub reset()

        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        gv1.GroupDescriptors.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1

        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtfromDate.Value = txtToDate.Value

        LoadItem()

        LoadSection()
        ChkItemAll.CheckState = CheckState.Checked
        ChkSectionAll.CheckState = CheckState.Checked


    End Sub
    Private Sub LoadData()
        If ChkItemSelect.IsChecked AndAlso cbgItem.CheckedValue.Count <= 0 Then
            Throw New Exception("Please select atleast one Item")
        End If
        
        If chkSectionSelect.IsChecked AndAlso cbgSection.CheckedValue.Count <= 0 Then
            Throw New Exception("Please select atleast one Location")
        End If
        'If rbtnCategorySelect.IsChecked AndAlso tvCategory.CheckedValue.Count <= 0 Then
        '    Throw New Exception("Please select atleast one Location")
        'End If
        Dim qry As String = "select final.Source_Doc_No,final.Source_Doc_Date,final.Location_Code,TSPL_LOCATION_MASTER.Section_Code,TSPL_SECTION_MASTER.Description as Section_Desc,final.Item_Code,final.op_qty,sum(final.rec_qty) as rec_qty,sum(final.out_qty) as out_qty,op_qty+sum(final.rec_qty)-sum(final.out_qty) as bal_qty from ("
        qry += " select TSPL_INVENTORY_MOVEMENT.Source_Doc_No,TSPL_INVENTORY_MOVEMENT.Source_Doc_Date,TSPL_INVENTORY_MOVEMENT.Location_Code,TSPL_INVENTORY_MOVEMENT.Item_Code,(select sum(aa.qty) from (select inv.Source_Doc_Date,inv.Location_Code,inv.Item_Code,(case when inv.InOut='I' then Inv.qty else 0-Inv.qty end) as qty from TSPL_INVENTORY_MOVEMENT as Inv)aa where aa.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code and aa.Location_Code=TSPL_INVENTORY_MOVEMENT.Location_Code and aa.Source_Doc_Date<'" + txtfromDate.Value + "') as op_qty,(case when TSPL_INVENTORY_MOVEMENT.InOut='I' then TSPL_INVENTORY_MOVEMENT.Qty else 0 end) as rec_qty,(case when TSPL_INVENTORY_MOVEMENT.InOut='O' then TSPL_INVENTORY_MOVEMENT.Qty else 0 end) as out_qty"
        qry += " from TSPL_INVENTORY_MOVEMENT where Trans_Type='PP_ISSUE')final left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=final.Location_Code left outer join TSPL_SECTION_MASTER on TSPL_SECTION_MASTER.Section_Code=TSPL_LOCATION_MASTER.Section_Code "
        qry += " where 2=2 and convert(date,Final.Source_Doc_Date,103)>=convert(date,'" + txtfromDate.Value + "',103) and convert(date,Final.Source_Doc_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103)"
        If ChkItemSelect.IsChecked And cbgItem.CheckedValue.Count > 0 Then
            qry += "and final.Item_Code  IN (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
        End If

        If chkSectionSelect.IsChecked And cbgSection.CheckedValue.Count > 0 Then
            qry += " and TSPL_LOCATION_MASTER.Section_Code in (" + clsCommon.GetMulcallString(cbgSection.CheckedValue) + ")  "
        End If

        qry += " group by final.Source_Doc_No,final.Source_Doc_Date,final.Item_Code,final.op_qty,final.Location_Code,TSPL_LOCATION_MASTER.Section_Code,TSPL_SECTION_MASTER.Description"
       

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        If dt IsNot Nothing And dt.Rows.Count > 0 Then
            gv1.DataSource = Nothing
            gv1.Columns.Clear()
            gv1.Rows.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.ShowGroupPanel = False

            gv1.EnableFiltering = True

            RadPageView1.SelectedPage = RadPageViewPage2
        Else
            clsCommon.MyMessageBoxShow("No Data Found")
        End If

        gv1.DataSource = dt
        SetGridFormationOFGV1()


    End Sub
    Sub SetGridFormationOFGV1()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = False
        Next

        gv1.Columns("Source_Doc_No").IsVisible = True
        gv1.Columns("Source_Doc_No").Width = 150
        gv1.Columns("Source_Doc_No").HeaderText = "Doc Code"

        gv1.Columns("Source_Doc_Date").Width = 100
        gv1.Columns("Source_Doc_Date").IsVisible = True
        gv1.Columns("Source_Doc_Date").HeaderText = "Date"
       

        gv1.Columns("Location_Code").IsVisible = False
        gv1.Columns("Location_Code").Width = 100
        gv1.Columns("Location_Code").HeaderText = "Location Code"

        gv1.Columns("Section_Code").IsVisible = True
        gv1.Columns("Section_Code").Width = 100
        gv1.Columns("Section_Code").HeaderText = "Section Code"

        gv1.Columns("Section_Desc").IsVisible = True
        gv1.Columns("Section_Desc").Width = 100
        gv1.Columns("Section_Desc").HeaderText = "Section Name"


        gv1.Columns("Item_Code").IsVisible = True
        gv1.Columns("Item_Code").Width = 100
        gv1.Columns("Item_Code").HeaderText = "Item Code"

        gv1.Columns("op_qty").IsVisible = True
        gv1.Columns("op_qty").Width = 100
        gv1.Columns("op_qty").HeaderText = "Opening Qty"

        gv1.Columns("rec_qty").IsVisible = True
        gv1.Columns("rec_qty").Width = 100
        gv1.Columns("rec_qty").HeaderText = "In Qty"


        gv1.Columns("out_qty").IsVisible = True
        gv1.Columns("out_qty").Width = 100
        gv1.Columns("out_qty").HeaderText = "Out Qty"

        gv1.Columns("bal_qty").IsVisible = True
        gv1.Columns("bal_qty").Width = 100
        gv1.Columns("bal_qty").HeaderText = "Balance Qty"

        
        Dim summaryRowItem As New GridViewSummaryRowItem()
        'Dim item1 As New GridViewSummaryItem("Stock_Qty", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item1)
        'Dim item2 As New GridViewSummaryItem("Cost", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item2)
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom

        ReStoreGridLayout()
    End Sub

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
    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            If ChkItemSelect.IsChecked Then
                Dim strItemName As String = ""
                For Each StrName As String In cbgItem.CheckedDisplayMember
                    If clsCommon.myLen(strItemName) > 0 Then
                        strItemName += ", "
                    End If
                    strItemName += StrName
                Next
                Dim strItemCode As String = ""
                For Each StrCode As String In cbgItem.CheckedValue
                    If clsCommon.myLen(strItemCode) > 0 Then
                        strItemCode += ", "
                    End If
                    strItemCode += StrCode
                Next
                arrHeader.Add((" Item Name: " + strItemName + " "))
            End If
                        If chkSectionSelect.IsChecked Then
                Dim strSelectName As String = ""
                For Each StrName As String In cbgSection.CheckedDisplayMember
                    If clsCommon.myLen(strSelectName) > 0 Then
                        strSelectName += ", "
                    End If
                    strSelectName += StrName
                Next

                arrHeader.Add(("Section Name: " + strSelectName + " "))
            End If

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Section Wise Stock Report", gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Section Wise Stock Report", gv1, arrHeader, Me.Text, True)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
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

    Private Sub ChkItemAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles ChkItemAll.ToggleStateChanged
        cbgItem.Enabled = ChkItemSelect.IsChecked
    End Sub

    Private Sub ChkSectionAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles ChkSectionAll.ToggleStateChanged
        cbgSection.Enabled = chkSectionSelect.IsChecked
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        reset()
    End Sub

    Private Sub rmExcel_Click(sender As Object, e As EventArgs) Handles rmExcel.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        print(EnumExportTo.PDF)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub RptSectionWiseStockReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        'ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P For Print")
        ButtonToolTip.SetToolTip(btnReset, "Pres%s Alt+N Adding New")
        reset()
        
    End Sub

    Private Sub RptSectionWiseStockReport_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            LoadData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            reset()
        
        End If
    End Sub
End Class
