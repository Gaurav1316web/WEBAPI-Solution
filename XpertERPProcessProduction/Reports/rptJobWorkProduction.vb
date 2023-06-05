Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
'================Created By Parteek-==============
'============= Ticket No: BM00000010016 by Parteek'
Public Class rptJobWorkProduction
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptDairyProductionWreckageReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExport.Visible = MyBase.isExport
    End Sub
    Sub Print(ByVal IsPrint As Exporter)

        Dim arrHeader As List(Of String) = New List(Of String)()
        Dim strTemp As String = ""
        arrHeader.Add("From Date : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + " ")

        arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
      
        If txtItemMult.arrDispalyMember IsNot Nothing AndAlso txtItemMult.arrDispalyMember.Count > 0 Then
            arrHeader.Add(" Item : " + clsCommon.GetMulcallStringWithComma(txtItemMult.arrDispalyMember))
        End If
        Dim qry As String = Nothing

        'Sanjay Ticket no-BHA/22/11/18-000696 add isnull(TSPL_LOCATION_MASTER.Is_Jobwork,0)=1 
        qry = "SELECT ROW_NUMBER() OVER (ORDER BY TSPL_PP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE) as [Sno] ,TSPL_PP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE as [Item Code],TSPL_PP_PRODUCTION_ENTRY_DETAIL.ITEM_DESCRIPTION as [Item Description], " & _
         "TSPL_ITEM_MASTER.HSN_Code as [HSN Code],TSPL_UNIT_MASTER.Unit_Desc as [UOM],sum(isnull(TSPL_PP_PRODUCTION_ENTRY_DETAIL.RECEIPT_QTY,0) ) as Qty " & _
         ",max(isnull(Job_Work_Rate,0))  as Rate" & _
                  ",sum(isnull(TSPL_PP_PRODUCTION_ENTRY_DETAIL.RECEIPT_QTY,0) )* " & _
                   "max(isnull(Job_Work_Rate,0)) as Amount " & _
                 "FROM TSPL_PP_PRODUCTION_ENTRY_DETAIL " & _
                 "left join TSPL_PP_PRODUCTION_ENTRY on TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE= TSPL_PP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE " & _
          "left join TSPL_ITEM_MASTER on TSPL_PP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE=TSPL_ITEM_MASTER.Item_Code  " & _
          "LEFT JOIN TSPL_UNIT_MASTER ON TSPL_PP_PRODUCTION_ENTRY_DETAIL.UNIT_CODE=TSPL_UNIT_MASTER.UNIT_CODE " & _
         "left join TSPL_LOCATION_MASTER ON TSPL_PP_PRODUCTION_ENTRY_DETAIL.LOCATION_CODE=TSPL_LOCATION_MASTER.LOCATION_CODE " & _
         "left join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_PP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE " & _
        "and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_PP_PRODUCTION_ENTRY_DETAIL.UNIT_CODE " & _
         "WHERE 2 = 2 and isnull(TSPL_LOCATION_MASTER.Is_Jobwork,0)=1 "

        qry += " and convert(date,TSPL_PP_PRODUCTION_ENTRY.PROD_DATE ,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_PP_PRODUCTION_ENTRY.PROD_DATE ,103) <=convert(date,'" + txtToDate.Value + "' ,103) "


        If txtItemMult.arrValueMember IsNot Nothing AndAlso txtItemMult.arrValueMember.Count > 0 Then
            qry += " and  TSPL_PP_PRODUCTION_ENTRY_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(txtItemMult.arrValueMember) + ")  "
        End If


        If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
            qry += " and TSPL_PP_PRODUCTION_ENTRY.Location_Code in (" + clsCommon.GetMulcallString(TxtMultiLocation.arrValueMember) + ")  "
        End If

        qry += " group by  TSPL_PP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE,TSPL_PP_PRODUCTION_ENTRY_DETAIL.ITEM_DESCRIPTION " & _
        ", TSPL_ITEM_MASTER.HSN_Code,TSPL_UNIT_MASTER.Unit_Desc "

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
        gv1.BestFitColumns()
        'FindAndRestoreGridLayout(Me)
        ReStoreGridLayout()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
    Sub SetGridFormationOFGV1()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
        Next

        'gv1.Columns("Category").Width = 50
        'gv1.Columns("Category").IsVisible = True
        'gv1.Columns("Category").HeaderText = "Category"

        'gv1.Columns("PROD_ENTRY_CODE").Width = 50
        'gv1.Columns("PROD_ENTRY_CODE").IsVisible = True
        'gv1.Columns("PROD_ENTRY_CODE").HeaderText = "Document Code"

        'gv1.Columns("PROD_DATE").Width = 50
        'gv1.Columns("PROD_DATE").IsVisible = True
        'gv1.Columns("PROD_DATE").HeaderText = "Document Date"

        'gv1.Columns("Type").Width = 50
        'gv1.Columns("Type").IsVisible = False
        'gv1.Columns("Type").HeaderText = "Document Type"

        'gv1.Columns("Batch_Code").Width = 100
        'gv1.Columns("Batch_Code").IsVisible = True
        'gv1.Columns("Batch_Code").HeaderText = "Batch Code"

        'gv1.Columns("BATCH_DATE").Width = 100
        'gv1.Columns("BATCH_DATE").IsVisible = True
        'gv1.Columns("BATCH_DATE").HeaderText = "Batch Date"

        'gv1.Columns("Location_Code").Width = 100
        'gv1.Columns("Location_Code").IsVisible = True
        'gv1.Columns("Location_Code").HeaderText = "Location Code"

        'gv1.Columns("Location_Desc").Width = 100
        'gv1.Columns("Location_Desc").IsVisible = True
        'gv1.Columns("Location_Desc").HeaderText = "Location Name"


        'gv1.Columns("CONSM_LOCATION_CODE").Width = 100
        'gv1.Columns("CONSM_LOCATION_CODE").IsVisible = True
        'gv1.Columns("CONSM_LOCATION_CODE").HeaderText = "Consm Location"

        'gv1.Columns("CONSM_SECTION_CODE").Width = 100
        'gv1.Columns("CONSM_SECTION_CODE").IsVisible = True
        'gv1.Columns("CONSM_SECTION_CODE").HeaderText = "Consm Section"

        'gv1.Columns("Item_Code").Width = 100
        'gv1.Columns("Item_Code").IsVisible = True
        'gv1.Columns("Item_Code").HeaderText = "Item Code"

        'gv1.Columns("Item_Desc").Width = 100
        'gv1.Columns("Item_Desc").IsVisible = True
        'gv1.Columns("Item_Desc").HeaderText = "Item Name"

        'gv1.Columns("Unit_Desc").Width = 100
        'gv1.Columns("Unit_Desc").IsVisible = True
        'gv1.Columns("Unit_Desc").HeaderText = "UOM"


        'gv1.Columns("BACK_QTY").Width = 100
        'gv1.Columns("BACK_QTY").IsVisible = True
        'gv1.Columns("BACK_QTY").HeaderText = "Back Qty"

        'gv1.Columns("WRECKAGE_QTY").Width = 100
        'gv1.Columns("WRECKAGE_QTY").IsVisible = True
        'gv1.Columns("WRECKAGE_QTY").HeaderText = "Wreckage Qty"


        'gv1.Columns("Wreckage_Location").Width = 100
        'gv1.Columns("Wreckage_Location").IsVisible = True
        'gv1.Columns("Wreckage_Location").HeaderText = "Wreckage Location"

        'gv1.Columns("Avail_FAT_Per").Width = 100
        'gv1.Columns("Avail_FAT_Per").IsVisible = True
        'gv1.Columns("Avail_FAT_Per").HeaderText = "[Fat %]"

        'gv1.Columns("Avail_FAT_KG").Width = 100
        'gv1.Columns("Avail_FAT_KG").IsVisible = True
        'gv1.Columns("Avail_FAT_KG").HeaderText = "[Fat KG]"

        'gv1.Columns("Avail_SNF_Per").Width = 100
        'gv1.Columns("Avail_SNF_Per").IsVisible = True
        'gv1.Columns("Avail_SNF_Per").HeaderText = "[SNF %]"

        'gv1.Columns("Avail_FAT_KG").Width = 100
        'gv1.Columns("Avail_FAT_KG").IsVisible = True
        'gv1.Columns("Avail_FAT_KG").HeaderText = "[SNF KG]"

        'gv1.Columns("Remarks").Width = 100
        'gv1.Columns("Remarks").IsVisible = True
        'gv1.Columns("Remarks").HeaderText = "Remarks"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        Dim item1 As New GridViewSummaryItem("Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)

        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub
    Sub Reset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub txtItemMult__My_Click(sender As Object, e As EventArgs) Handles txtItemMult._My_Click
        Dim qry As String = " select TSPL_ITEM_MASTER.Item_Code as Code,TSPL_ITEM_MASTER.Item_Desc as Name from TSPL_ITEM_MASTER "
        txtItemMult.arrValueMember = clsCommon.ShowMultipleSelectForm("txtMultItemNo", qry, "Code", "Name", txtItemMult.arrValueMember, txtItemMult.arrDispalyMember)
    End Sub

   
    Private Sub TxtMultiLocation__My_Click(sender As Object, e As EventArgs) Handles TxtMultiLocation._My_Click
        Dim qry As String = "select Location_Code as [Code] ,Location_Desc as [Name] from TSPL_LOCATION_MASTER where Is_Jobwork=1"
        TxtMultiLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("Pro", qry, "Code", "Name", TxtMultiLocation.arrValueMember, TxtMultiLocation.arrDispalyMember)
    End Sub

    'Private Sub RptWreckageReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    '    SetUserMgmtNew()

    '    ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
    '    ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
    '    ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")

    '    Reset()
    'End Sub

    'Private Sub RptWreckageReport_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
    '    If e.Alt And e.KeyCode = Keys.R Then
    '        Print(Exporter.Refresh)
    '    ElseIf e.Alt And e.KeyCode = Keys.C Then
    '        Me.Close()
    '    ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
    '        Reset()
    '    End If
    'End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv1
        Print(Exporter.Refresh)
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub


    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub rptJobWorkProduction_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            Print(Exporter.Refresh)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub

    Private Sub rptJobWorkProduction_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")

        Reset()
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & "'"))

                If txtItemMult.arrValueMember IsNot Nothing AndAlso txtItemMult.arrValueMember.Count > 0 Then
                    arrHeader.Add(" Item : " + clsCommon.GetMulcallStringWithComma(txtItemMult.arrDispalyMember))
                End If

                If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
                    arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(TxtMultiLocation.arrDispalyMember))
                End If

                'Dim sfd As SaveFileDialog = New SaveFileDialog()
                'Dim filePath As String
                'sfd.FileName = Me.Text
                'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                '    filePath = sfd.FileName
                'Else
                '    Exit Sub
                'End If
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
                'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & "'"))

                If txtItemMult.arrValueMember IsNot Nothing AndAlso txtItemMult.arrValueMember.Count > 0 Then
                    arrHeader.Add(" Item : " + clsCommon.GetMulcallStringWithComma(txtItemMult.arrDispalyMember))
                End If

                If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
                    arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(TxtMultiLocation.arrDispalyMember))
                End If
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Job Work Production Report", gv1, arrHeader, "Job Work Production Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
End Class
