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
            Dim whr As String = " and 2=2"
            Dim dt As New DataTable

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                whr += " and TSPL_RCDF_STD.Location_Code (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
            End If

            If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                whr += " and TSPL_RCDF_STD_PRODUCE.Item_Code (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ")"
            End If

            qry = "   select TSPL_RCDF_STD.Doc_Code,TSPL_RCDF_STD.Doc_Date,TSPL_RCDF_STD.Location_Code,TSPL_RCDF_STD.Batch_No,TSPL_RCDF_STD.Tot_Produce_Qty,
                      TSPL_RCDF_STD.Tot_Produce_FATKG,TSPL_RCDF_STD.Tot_Produce_SNFKG,TSPL_RCDF_STD.Tot_Issue_Qty,TSPL_RCDF_STD.Tot_Issue_FATKG,TSPL_RCDF_STD.Tot_Issue_SNFKG,
                      TSPL_RCDF_STD.Tot_Difference_Qty,TSPL_RCDF_STD.Tot_Difference_FATKG,TSPL_RCDF_STD.Tot_Difference_SNFKG,TSPL_RCDF_STD.Tot_Added_Qty,TSPL_RCDF_STD.Tot_Added_FATKG,
                      TSPL_RCDF_STD.Tot_Added_SNFKG,TSPL_RCDF_STD.Tot_Removed_Qty,TSPL_RCDF_STD.Tot_Removed_FATKG,TSPL_RCDF_STD.Tot_Removed_SNFKG,TSPL_RCDF_STD.Tot_AddRemove_Qty,TSPL_RCDF_STD.Tot_AddRemove_FATKG,
                      TSPL_RCDF_STD.Tot_AddRemove_SNFKG,TSPL_RCDF_STD.Tot_Net_Qty,TSPL_RCDF_STD.Tot_Net_FATKG,TSPL_RCDF_STD.Tot_Net_SNFKG,TSPL_RCDF_STD_PRODUCE.BOM_Code,TSPL_RCDF_STD_PRODUCE.Item_Code,TSPL_RCDF_STD_PRODUCE.Unit_Code,TSPL_RCDF_STD_PRODUCE.Qty,
                      TSPL_RCDF_STD_PRODUCE.FAT,TSPL_RCDF_STD_PRODUCE.FAT_KG,TSPL_RCDF_STD_PRODUCE.SNF,TSPL_RCDF_STD_PRODUCE.SNF_KG,TSPL_RCDF_STD_PRODUCE.Location_Code,TSPL_RCDF_STD_ISSUE.Item_Code,TSPL_RCDF_STD_ISSUE.Location_Code,TSPL_RCDF_STD_ISSUE.Unit_Code,TSPL_RCDF_STD_ISSUE.Qty,
                      TSPL_RCDF_STD_ISSUE.FAT,TSPL_RCDF_STD_ISSUE.FAT_KG,TSPL_RCDF_STD_ISSUE.SNF,TSPL_RCDF_STD_ISSUE.SNF_KG,TSPL_RCDF_STD_ADD_REMOVE.ADD_REMOVE_TYPE,TSPL_RCDF_STD_ADD_REMOVE.Location_Code,TSPL_RCDF_STD_ADD_REMOVE.Item_Code,TSPL_RCDF_STD_ADD_REMOVE.Unit_Code,TSPL_RCDF_STD_ADD_REMOVE.Qty,
                      TSPL_RCDF_STD_ADD_REMOVE.FAT,TSPL_RCDF_STD_ADD_REMOVE.FAT_KG,TSPL_RCDF_STD_ADD_REMOVE.SNF,TSPL_RCDF_STD_ADD_REMOVE.SNF_KG from TSPL_RCDF_STD
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


        Catch ex As Exception

        End Try
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