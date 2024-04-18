Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO

Public Class rptProductionStandardization
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim strQry As String = ""

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub

    Private Sub rptProductionStandardization_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Sub Reset()
        'ToDate.Value = clsCommon.GETSERVERDATE()
        'fromDate.Value = ToDate.Value.AddMonths(-1)
        txtBatchNo.arrValueMember = Nothing
        txtLocation.arrValueMember = Nothing
        txtItem.arrValueMember = Nothing
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

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

    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = Gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If


            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)


            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If

            If txtBatchNo.arrValueMember IsNot Nothing AndAlso txtBatchNo.arrValueMember.Count > 0 Then
                arrHeader.Add("Batch No : " + clsCommon.GetMulcallStringWithComma(txtBatchNo.arrDispalyMember))
            End If

            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            clsCommon.MyExportToExcelGrid("Production standardization Report", Gv1, arrHeader, Me.Text)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If

            If txtBatchNo.arrValueMember IsNot Nothing AndAlso txtBatchNo.arrValueMember.Count > 0 Then
                arrHeader.Add("Batch No : " + clsCommon.GetMulcallStringWithComma(txtBatchNo.arrDispalyMember))
            End If

            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            clsCommon.MyExportToPDF("Production standardization Report", Gv1, arrHeader, "Issue WIP Consumption Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try

    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click

        Try
            PageSetupReport_ID = MyBase.Form_ID
            TemplateGridview = Gv1
            Dim qry As String = ""
            Dim whr As String = ""
            Dim dt As New DataTable

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                whr += " and TSPL_RCDF_STD.Location_Code In (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
            End If

            If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                whr += " and TSPL_RCDF_STD_PRODUCE.Item_Code In (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ")"
            End If

            qry = "   select TSPL_RCDF_STD.Doc_Code,TSPL_RCDF_STD.Doc_Date,TSPL_RCDF_STD.Location_Code,TSPL_RCDF_STD.Batch_No,TSPL_RCDF_STD.Tot_Produce_Qty,
                      TSPL_RCDF_STD.Tot_Produce_FATKG,TSPL_RCDF_STD.Tot_Produce_SNFKG,TSPL_RCDF_STD.Tot_Issue_Qty,TSPL_RCDF_STD.Tot_Issue_FATKG,TSPL_RCDF_STD.Tot_Issue_SNFKG,
                      TSPL_RCDF_STD.Tot_Difference_Qty,TSPL_RCDF_STD.Tot_Difference_FATKG,TSPL_RCDF_STD.Tot_Difference_SNFKG,TSPL_RCDF_STD.Tot_Added_Qty,TSPL_RCDF_STD.Tot_Added_FATKG,
                      TSPL_RCDF_STD.Tot_Added_SNFKG,TSPL_RCDF_STD.Tot_Removed_Qty,TSPL_RCDF_STD.Tot_Removed_FATKG,TSPL_RCDF_STD.Tot_Removed_SNFKG,TSPL_RCDF_STD.Tot_AddRemove_Qty,TSPL_RCDF_STD.Tot_AddRemove_FATKG,
                      TSPL_RCDF_STD.Tot_AddRemove_SNFKG,TSPL_RCDF_STD.Tot_Net_Qty,TSPL_RCDF_STD.Tot_Net_FATKG,TSPL_RCDF_STD.Tot_Net_SNFKG,TSPL_RCDF_STD_PRODUCE.BOM_Code,TSPL_RCDF_STD_PRODUCE.Item_Code ,TSPL_RCDF_STD_PRODUCE.Unit_Code,TSPL_RCDF_STD_PRODUCE.Qty,
                      TSPL_RCDF_STD_PRODUCE.FAT,TSPL_RCDF_STD_PRODUCE.FAT_KG,TSPL_RCDF_STD_PRODUCE.SNF,TSPL_RCDF_STD_PRODUCE.SNF_KG,TSPL_RCDF_STD_PRODUCE.Location_Code,
                      TSPL_RCDF_STD_ISSUE.Item_Code as Issue_Item_Code,TSPL_RCDF_STD_ISSUE.Location_Code as Issue_Location_Code,TSPL_RCDF_STD_ISSUE.Unit_Code as Issue_Unit_Code,TSPL_RCDF_STD_ISSUE.Qty as Issue_Qty,
                      TSPL_RCDF_STD_ISSUE.FAT as Issue_FAT,TSPL_RCDF_STD_ISSUE.FAT_KG as Issue_FAT_KG,TSPL_RCDF_STD_ISSUE.SNF as Issue_SNF,TSPL_RCDF_STD_ISSUE.SNF_KG as Issue_SNF_KG,
                      TSPL_RCDF_STD_ADD_REMOVE.ADD_REMOVE_TYPE as AR_Type,TSPL_RCDF_STD_ADD_REMOVE.Location_Code as AR_Location_Code,TSPL_RCDF_STD_ADD_REMOVE.Item_Code as AR_Item_Code,TSPL_RCDF_STD_ADD_REMOVE.Unit_Code as AR_Unit_Code,TSPL_RCDF_STD_ADD_REMOVE.Qty as AR_Qty,
                      TSPL_RCDF_STD_ADD_REMOVE.FAT as AR_FAT,TSPL_RCDF_STD_ADD_REMOVE.FAT_KG as AR_FAT_KG,TSPL_RCDF_STD_ADD_REMOVE.SNF as AR_SNF,TSPL_RCDF_STD_ADD_REMOVE.SNF_KG as AR_SNF_KG from TSPL_RCDF_STD
                      LEFT OUTER JOIN TSPL_RCDF_STD_PRODUCE ON TSPL_RCDF_STD_PRODUCE.Doc_Code = TSPL_RCDF_STD.Doc_Code
                      LEFT OUTER JOIN TSPL_RCDF_STD_ISSUE ON TSPL_RCDF_STD_ISSUE.Doc_Code = TSPL_RCDF_STD.Doc_Code
                      LEFT OUTER JOIN TSPL_RCDF_STD_ADD_REMOVE ON TSPL_RCDF_STD_ADD_REMOVE.Doc_Code = TSPL_RCDF_STD.Doc_Code
                      left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_RCDF_STD.Location_Code
                      WHERE convert(date,TSPL_RCDF_STD.Doc_Date ,103)>=convert(date,'" + fromDate.Value + "',103) and convert(date,TSPL_RCDF_STD.Doc_Date ,103) <=convert(date,'" + ToDate.Value + "',103) 
                      " + whr + " "

            dt = clsDBFuncationality.GetDataTable(qry)
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()

            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                Gv1.DataSource = dt
                For ii As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(ii).ReadOnly = True
                Next
                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.BestFitColumns()
                Gv1.EnableFiltering = True
            Else
                clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            End If
            FormatGrid()
            ReStoreGridLayout()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub FormatGrid()
        Gv1.TableElement.TableHeaderHeight = 25
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).Width = 100
            Gv1.Columns(ii).IsVisible = True
        Next

        Gv1.Columns("Doc_Code").IsVisible = True
        Gv1.Columns("Doc_Code").Width = 100
        Gv1.Columns("Doc_Code").HeaderText = "Code"

        Gv1.Columns("Doc_Date").IsVisible = True
        Gv1.Columns("Doc_Date").Width = 100
        Gv1.Columns("Doc_Date").HeaderText = "Date"

        Gv1.Columns("Location_Code").IsVisible = True
        Gv1.Columns("Location_Code").Width = 100
        Gv1.Columns("Location_Code").HeaderText = "Location"

        Gv1.Columns("Batch_No").IsVisible = True
        Gv1.Columns("Batch_No").Width = 100
        Gv1.Columns("Batch_No").HeaderText = "Batch"

        Gv1.Columns("Tot_Produce_Qty").IsVisible = True
        Gv1.Columns("Tot_Produce_Qty").Width = 100
        Gv1.Columns("Tot_Produce_Qty").HeaderText = "Produce Quantity"

        Gv1.Columns("Tot_Produce_FATKG").IsVisible = True
        Gv1.Columns("Tot_Produce_FATKG").Width = 100
        Gv1.Columns("Tot_Produce_FATKG").HeaderText = "Produce FATKG"

        Gv1.Columns("Tot_Produce_SNFKG").IsVisible = True
        Gv1.Columns("Tot_Produce_SNFKG").Width = 100
        Gv1.Columns("Tot_Produce_SNFKG").HeaderText = "Produce SNFKG"

        Gv1.Columns("Tot_Issue_Qty").IsVisible = True
        Gv1.Columns("Tot_Issue_Qty").Width = 100
        Gv1.Columns("Tot_Issue_Qty").HeaderText = "Issue Qunatity"

        Gv1.Columns("Tot_Issue_FATKG").IsVisible = True
        Gv1.Columns("Tot_Issue_FATKG").Width = 100
        Gv1.Columns("Tot_Issue_FATKG").HeaderText = "Issue FATKG"

        Gv1.Columns("Tot_Issue_SNFKG").IsVisible = True
        Gv1.Columns("Tot_Issue_SNFKG").Width = 100
        Gv1.Columns("Tot_Issue_SNFKG").HeaderText = "Issue SNFKG"

        Gv1.Columns("Tot_Difference_Qty").IsVisible = True
        Gv1.Columns("Tot_Difference_Qty").Width = 100
        Gv1.Columns("Tot_Difference_Qty").HeaderText = "Difference Quantity"

        Gv1.Columns("Tot_Difference_FATKG").IsVisible = True
        Gv1.Columns("Tot_Difference_FATKG").Width = 100
        Gv1.Columns("Tot_Difference_FATKG").HeaderText = "Difference FATKG"

        Gv1.Columns("Tot_Difference_SNFKG").IsVisible = True
        Gv1.Columns("Tot_Difference_SNFKG").Width = 100
        Gv1.Columns("Tot_Difference_SNFKG").HeaderText = "Difference SNFKG"

        Gv1.Columns("Tot_Added_Qty").IsVisible = True
        Gv1.Columns("Tot_Added_Qty").Width = 100
        Gv1.Columns("Tot_Added_Qty").HeaderText = "Added Quantity"

        Gv1.Columns("Tot_Added_FATKG").IsVisible = True
        Gv1.Columns("Tot_Added_FATKG").Width = 100
        Gv1.Columns("Tot_Added_FATKG").HeaderText = "Added FATKG"

        Gv1.Columns("Tot_Added_SNFKG").IsVisible = True
        Gv1.Columns("Tot_Added_SNFKG").Width = 100
        Gv1.Columns("Tot_Added_SNFKG").HeaderText = "Added SNFKG"

        Gv1.Columns("Tot_Removed_Qty").IsVisible = True
        Gv1.Columns("Tot_Removed_Qty").Width = 100
        Gv1.Columns("Tot_Removed_Qty").HeaderText = "Removed Quantity"

        Gv1.Columns("Tot_Removed_SNFKG").IsVisible = True
        Gv1.Columns("Tot_Removed_SNFKG").Width = 100
        Gv1.Columns("Tot_Removed_SNFKG").HeaderText = "Removed SNFKG"

        Gv1.Columns("Tot_AddRemove_Qty").IsVisible = True
        Gv1.Columns("Tot_AddRemove_Qty").Width = 100
        Gv1.Columns("Tot_AddRemove_Qty").HeaderText = "AddRemove Quantity"

        Gv1.Columns("Tot_AddRemove_FATKG").IsVisible = True
        Gv1.Columns("Tot_AddRemove_FATKG").Width = 100
        Gv1.Columns("Tot_AddRemove_FATKG").HeaderText = "AddRemove FATKG"

        Gv1.Columns("Tot_AddRemove_SNFKG").IsVisible = True
        Gv1.Columns("Tot_AddRemove_SNFKG").Width = 100
        Gv1.Columns("Tot_AddRemove_SNFKG").HeaderText = "AddRemove SNFKG"

        Gv1.Columns("Tot_Net_Qty").IsVisible = True
        Gv1.Columns("Tot_Net_Qty").Width = 100
        Gv1.Columns("Tot_Net_Qty").HeaderText = "Net Quantity"

        Gv1.Columns("Tot_Net_FATKG").IsVisible = True
        Gv1.Columns("Tot_Net_FATKG").Width = 100
        Gv1.Columns("Tot_Net_FATKG").HeaderText = "Net FATKG"

        Gv1.Columns("Tot_Net_SNFKG").IsVisible = True
        Gv1.Columns("Tot_Net_SNFKG").Width = 100
        Gv1.Columns("Tot_Net_SNFKG").HeaderText = "Net SNFKG"

        Gv1.Columns("BOM_Code").IsVisible = True
        Gv1.Columns("BOM_Code").Width = 100
        Gv1.Columns("BOM_Code").HeaderText = "BOM Code"

        Gv1.Columns("Item_Code").IsVisible = True
        Gv1.Columns("Item_Code").Width = 100
        Gv1.Columns("Item_Code").HeaderText = "Item Code"

        Gv1.Columns("Unit_Code").IsVisible = True
        Gv1.Columns("Unit_Code").Width = 100
        Gv1.Columns("Unit_Code").HeaderText = "Unit Code"

        Gv1.Columns("Qty").IsVisible = True
        Gv1.Columns("Qty").Width = 100
        Gv1.Columns("Qty").HeaderText = "Quantity"

        Gv1.Columns("FAT").IsVisible = True
        Gv1.Columns("FAT").Width = 100
        Gv1.Columns("FAT").HeaderText = "FAT"

        Gv1.Columns("FAT_KG").IsVisible = True
        Gv1.Columns("FAT_KG").Width = 100
        Gv1.Columns("FAT_KG").HeaderText = "FAT KG"

        Gv1.Columns("SNF").IsVisible = True
        Gv1.Columns("SNF").Width = 100
        Gv1.Columns("SNF").HeaderText = "SNF"

        Gv1.Columns("SNF_KG").IsVisible = True
        Gv1.Columns("SNF_KG").Width = 100
        Gv1.Columns("SNF_KG").HeaderText = "SNF KG"

        Gv1.Columns("Location_Code").IsVisible = True
        Gv1.Columns("Location_Code").Width = 100
        Gv1.Columns("Location_Code").HeaderText = "Location"

        Gv1.Columns("Issue_Item_Code").IsVisible = True
        Gv1.Columns("Issue_Item_Code").Width = 100
        Gv1.Columns("Issue_Item_Code").HeaderText = "Item Code"

        Gv1.Columns("Issue_Location_Code").IsVisible = True
        Gv1.Columns("Issue_Location_Code").Width = 100
        Gv1.Columns("Issue_Location_Code").HeaderText = "Location"

        Gv1.Columns("Issue_Unit_Code").IsVisible = True
        Gv1.Columns("Issue_Unit_Code").Width = 100
        Gv1.Columns("Issue_Unit_Code").HeaderText = "Unit Code"

        Gv1.Columns("Issue_Qty").IsVisible = True
        Gv1.Columns("Issue_Qty").Width = 100
        Gv1.Columns("Issue_Qty").HeaderText = "Quantity"

        Gv1.Columns("Issue_FAT").IsVisible = True
        Gv1.Columns("Issue_FAT").Width = 100
        Gv1.Columns("Issue_FAT").HeaderText = "FAT"

        Gv1.Columns("Issue_FAT_KG").IsVisible = True
        Gv1.Columns("Issue_FAT_KG").Width = 100
        Gv1.Columns("Issue_FAT_KG").HeaderText = "FAT KG"

        Gv1.Columns("Issue_SNF").IsVisible = True
        Gv1.Columns("Issue_SNF").Width = 100
        Gv1.Columns("Issue_SNF").HeaderText = "SNF"

        Gv1.Columns("Issue_SNF_KG").IsVisible = True
        Gv1.Columns("Issue_SNF_KG").Width = 100
        Gv1.Columns("Issue_SNF_KG").HeaderText = "SNF KG"

        Gv1.Columns("AR_Type").IsVisible = True
        Gv1.Columns("AR_Type").Width = 100
        Gv1.Columns("AR_Type").HeaderText = "TYPE"

        Gv1.Columns("AR_Location_Code").IsVisible = True
        Gv1.Columns("AR_Location_Code").Width = 100
        Gv1.Columns("AR_Location_Code").HeaderText = "Location"

        Gv1.Columns("AR_Item_Code").IsVisible = True
        Gv1.Columns("AR_Item_Code").Width = 100
        Gv1.Columns("AR_Item_Code").HeaderText = "Item Code"

        Gv1.Columns("AR_Unit_Code").IsVisible = True
        Gv1.Columns("AR_Unit_Code").Width = 100
        Gv1.Columns("AR_Unit_Code").HeaderText = "Unit Code"

        Gv1.Columns("AR_Qty").IsVisible = True
        Gv1.Columns("AR_Qty").Width = 100
        Gv1.Columns("AR_Qty").HeaderText = "Quantity"

        Gv1.Columns("AR_FAT").IsVisible = True
        Gv1.Columns("AR_FAT").Width = 100
        Gv1.Columns("AR_FAT").HeaderText = "FAT"

        Gv1.Columns("AR_FAT_KG").IsVisible = True
        Gv1.Columns("AR_FAT_KG").Width = 100
        Gv1.Columns("AR_FAT_KG").HeaderText = "FAT KG"

        Gv1.Columns("AR_SNF").IsVisible = True
        Gv1.Columns("AR_SNF").Width = 100
        Gv1.Columns("AR_SNF").HeaderText = "SNF"

        Gv1.Columns("AR_SNF_KG").IsVisible = True
        Gv1.Columns("AR_SNF_KG").Width = 100
        Gv1.Columns("AR_SNF_KG").HeaderText = "SNF KG"

        Dim summaryRowItem As New GridViewSummaryRowItem()

        Gv1.ShowGroupPanel = False
        Gv1.MasterTemplate.AutoExpandGroups = True

        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        View()
    End Sub

    Sub View()
        If Gv1.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()
            view.ColumnGroups.Add(New GridViewColumnGroup("Production Items"))
            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Doc_Code").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Doc_Date").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Location_Code").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Batch_No").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Tot_Produce_Qty").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Tot_Produce_FATKG").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Tot_Produce_SNFKG").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Tot_Issue_Qty").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Tot_Issue_FATKG").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Tot_Issue_SNFKG").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Tot_Difference_Qty").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Tot_Difference_FATKG").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Tot_Difference_SNFKG").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Tot_Added_Qty").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Tot_Added_FATKG").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Tot_Added_SNFKG").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Tot_Removed_Qty").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Tot_Removed_FATKG").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Tot_Removed_SNFKG").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Tot_AddRemove_Qty").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Tot_AddRemove_FATKG").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Tot_AddRemove_SNFKG").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Tot_Net_Qty").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Tot_Net_FATKG").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Tot_Net_SNFKG").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("BOM_Code").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Item_Code").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Unit_Code").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Qty").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("FAT").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("FAT_KG").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("SNF").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("SNF_KG").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Issue Items"))
            view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Issue_Item_Code").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Issue_Unit_Code").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Issue_Qty").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Issue_FAT").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Issue_FAT_KG").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Issue_SNF").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Issue_SNF_KG").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("ADD/REMOVE Items"))
            view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("AR_Type").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("AR_Item_Code").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("AR_Unit_Code").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("AR_Qty").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("AR_FAT").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("AR_FAT_KG").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("AR_SNF").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("AR_SNF_KG").Name)

            Gv1.ViewDefinition = view
        End If

    End Sub
    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Dim qry As String
        qry = " select TSPL_ITEM_MASTER.Item_Code as Code , TSPL_ITEM_MASTER.Item_Desc as Description from TSPL_ITEM_MASTER  "
        txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel@ItemcodeForProdStatRPT", qry, "Code", "Description", txtItem.arrValueMember, txtItem.arrDispalyMember)
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER "
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransMulSe@Batch", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub
End Class