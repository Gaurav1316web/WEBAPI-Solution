'KUNAL > DEVELOPED NEW SCREEEN > DATE : 29 -NOV -2016 

Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common
Imports XpertERPEngine
Imports Telerik.WinControls.UI.Export
Imports System.IO

Public Class FrmRpt_OutStnd_Items_RGP
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub SetUserMgmtNew()
        Try
            'MyBase.SetUserMgmt(clsUserMgtCode.FrmRpt_OutStnd_Items_RGP)
            If Not (MyBase.isReadFlag) Then
                Throw New Exception("Permission Denied")
            End If
            RadSplitButton1.Visible = MyBase.isExport
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub FrmRGP_Register_NRGP_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnreset.Enabled Then
                Reset()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
                ''savedata()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
                ''postdata()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
                ''deletedata()
            ElseIf e.Control AndAlso e.KeyCode = Keys.P AndAlso btnPrint.Enabled Then
                print(0)
            ElseIf e.Alt And e.KeyCode = Keys.C Then
                Me.Close()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub FrmRGP_Register_NRGP_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            SetUserMgmtNew()

            ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
            ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P Print the Report")
            ButtonToolTip.SetToolTip(btnreset, "Press Alt+R Reset the Window")
            Reset()
            txtFromDate.Value = Today.AddMonths(-1)
            RadPageView1.SelectedPage = RadPageViewPage1
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Sub Reset()
        Try
            txtFromDate.Value = Today.AddMonths(-1)
            txtToDate.Value = clsCommon.GETSERVERDATE()

            txtDocNo.arrValueMember = Nothing
            txtItem.arrValueMember = Nothing
            txtVendor.arrValueMember = Nothing
            txtLocation.arrValueMember = Nothing
            gv.DataSource = Nothing
            gv.EnableGrouping = True
            RadPageView1.SelectedPage = RadPageViewPage1
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub LoadData()
        Try
            Dim qry As String = Nothing
            Dim fromdate As String = clsCommon.GetPrintDate(dtpfromdate.Value, "dd/MMM/yyyy")
            Dim Todate As String = clsCommon.GetPrintDate(dtptodate.Value, "dd/MMM/yyyy")

            qry = "SELECT CONVERT(varchar , ROW_NUMBER() OVER ( ORDER BY TBL0.VENDOR_NAME ) )AS SERIAL ,  (TBL0.VENDOR_NAME) AS [VENDOR_NAME], MAX(TBL0.RGP_NO) AS [RGP NO], MAX(CONVERT(DATE,TBL0.RGP_Date,103)) RGP_Date , MAX(TBL0.ITEM_CODE) AS [ITEM], MAX(TBL0.ITEM_DESC) AS [ITEM DETAIL], MAX(TBL0.Unit_code) AS UOM, SUM(TBL0.RGP_QTY) AS [QTY],  MAX('NOT FOUND ON SCREEN') AS [RTRN EXP DT], MAX(TBL0.REASON) AS [PURPOSE], MAX(TBL0.MODIFY_BY) AS [AUTH BY], MAX(ISNULL(TBL0.Dept,'')) AS [AUTH_BY DEPT], MAX(TBL0.STATUS) AS [POST STATUS], MAX('UNKNOW') AS [ACTION PERSON], MAX(TBL0.Remarks) AS [REMARKS], MAX('UNKNOWN') AS [EXPECTED RTRN DATE 1], MAX('UNKNOWN') AS [EXPECTED RTRN DATE 2] , MAX(TBL0.Location) Location , MAX(TBL0.Doc_Type)  Doc_Type FROM (SELECT  RH.Location , RH.Doc_Type , RH.RGP_Date ,  RH.RGP_NO,  RD.ITEM_CODE, RD.ITEM_DESC,  RD.RGP_QTY,  RD.Unit_code, RH.REASON, RH.VENDOR_NAME, RH.VENDOR_CODE, RH.MODIFY_BY,  UM.Dept,  RH.Status,  RH.Remarks FROM TSPL_RGP_HEAD RH LEFT JOIN TSPL_RGP_DETAIL RD ON RH.RGP_NO = RD.RGP_NO AND RH.STATUS = '1' LEFT JOIN TSPL_USER_MASTER UM ON RH.Modify_By = UM.User_Code) AS TBL0  WHERE 1=1"
            If txtDocNo.arrValueMember IsNot Nothing AndAlso txtDocNo.arrValueMember.Count > 0 Then
                qry += " and TBL0.RGP_No In (" + clsCommon.GetMulcallString(txtDocNo.arrValueMember) + ") " + Environment.NewLine
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                qry += " and TBL0.Location  in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") " + Environment.NewLine
            End If
            If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                qry += "  and TBL0.Item_Code in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ") " + Environment.NewLine
            End If
            qry += " and TBL0.Doc_Type = 'RGP' "
            If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                qry += "   and TBL0.VENDOR_NAME in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ") " + Environment.NewLine
            End If
            qry += "and (convert(date,TBL0.RGP_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and convert(date,TBL0.RGP_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value) + "')  GROUP BY TBL0.Vendor_Name ORDER BY TBL0.Vendor_Name "
            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(qry)
            If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                gv.DataSource = dtgv
                gv.MasterTemplate.ShowRowHeaderColumn = False
                gv.GroupDescriptors.Clear()
                gv.MasterTemplate.SummaryRowsBottom.Clear()
                FormatGrid()
                For ii As Integer = 0 To gv.Columns.Count - 1
                    gv.Columns(ii).ReadOnly = True
                Next
                gv.BestFitColumns()
                ReStoreGridLayout()
            End If
            RadPageView1.SelectedPage = RadPageViewPage2
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub FormatGrid()
        Try

            gv.Columns("RGP NO").IsVisible = True
            gv.Columns("RGP NO").Width = 100
            gv.Columns("RGP NO").HeaderText = "RGP No"

            gv.Columns("RGP_Date").IsVisible = True
            gv.Columns("RGP_Date").Width = 100
            gv.Columns("RGP_Date").HeaderText = "Date"

            gv.Columns("ITEM").IsVisible = False
            gv.Columns("ITEM").Width = 100
            gv.Columns("ITEM").HeaderText = "Item"

            gv.Columns("ITEM DETAIL").IsVisible = True
            gv.Columns("ITEM DETAIL").Width = 150
            gv.Columns("ITEM DETAIL").HeaderText = "Item Desc"

            gv.Columns("UOM").IsVisible = True
            gv.Columns("UOM").Width = 100
            gv.Columns("UOM").HeaderText = "UOM"

            gv.Columns("QTY").IsVisible = True
            gv.Columns("QTY").Width = 100
            gv.Columns("QTY").HeaderText = "Quantity"

            gv.Columns("RTRN EXP DT").IsVisible = True
            gv.Columns("RTRN EXP DT").Width = 100
            gv.Columns("RTRN EXP DT").HeaderText = "Expected Date of Return"

            gv.Columns("PURPOSE").IsVisible = True
            gv.Columns("PURPOSE").Width = 100
            gv.Columns("PURPOSE").HeaderText = "Purpose"

            gv.Columns("VENDOR_NAME").IsVisible = True
            gv.Columns("VENDOR_NAME").Width = 150
            gv.Columns("VENDOR_NAME").HeaderText = "Party's Name"

            gv.Columns("AUTH BY").IsVisible = True
            gv.Columns("AUTH BY").Width = 100
            gv.Columns("AUTH BY").HeaderText = "Authorised By"

            gv.Columns("AUTH_BY DEPT").IsVisible = True
            gv.Columns("AUTH_BY DEPT").Width = 100
            gv.Columns("AUTH_BY DEPT").HeaderText = "Auth Deptt"

            gv.Columns("POST STATUS").IsVisible = False
            gv.Columns("POST STATUS").Width = 100
            gv.Columns("POST STATUS").HeaderText = "POST STATUS"

            gv.Columns("ACTION PERSON").IsVisible = True
            gv.Columns("ACTION PERSON").Width = 100
            gv.Columns("ACTION PERSON").HeaderText = "Action Taken By"

            gv.Columns("REMARKS").IsVisible = True
            gv.Columns("REMARKS").Width = 100
            gv.Columns("REMARKS").HeaderText = "Remark"

            gv.Columns("EXPECTED RTRN DATE 1").IsVisible = True
            gv.Columns("EXPECTED RTRN DATE 1").Width = 100
            gv.Columns("EXPECTED RTRN DATE 1").HeaderText = "Exp.Dt.of Receipt"

            gv.Columns("EXPECTED RTRN DATE 2").IsVisible = True
            gv.Columns("EXPECTED RTRN DATE 2").Width = 100
            gv.Columns("EXPECTED RTRN DATE 2").HeaderText = "Exp.Dt.of Receipt"

            gv.Columns("Location").IsVisible = False
            gv.Columns("Location").Width = 100
            gv.Columns("Location").HeaderText = "Location"

            gv.Columns("Doc_Type").IsVisible = False
            gv.Columns("Doc_Type").Width = 100
            gv.Columns("Doc_Type").HeaderText = "Doc_Type"

            gv.GroupDescriptors.Add(New GridGroupByExpression("VENDOR_NAME as Item format ""{0}: {1}"" Group By VENDOR_NAME"))
            gv.ShowGroupPanel = False
            gv.MasterTemplate.AutoExpandGroups = True

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        LoadData()
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arr As List(Of String) = New List(Of String)()

            arr.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmRpt_OutStnd_Items_RGP & "'"))
            arr.Add("Company : " + objCommonVar.CurrentCompanyName)
            arr.Add("From Date:  " + dtpfromdate.Value + "  To Date: " + dtptodate.Value + "")

            If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                arr.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If

            If txtDocNo.arrValueMember IsNot Nothing AndAlso txtDocNo.arrValueMember.Count > 0 Then
                arr.Add("Document : " + clsCommon.GetMulcallStringWithComma(txtDocNo.arrValueMember))
            End If

            If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                arr.Add("Items : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember))
            End If

            If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                arr.Add("Party's Name : " + clsCommon.GetMulcallStringWithComma(txtVendor.arrDispalyMember))
            End If

            If (gv.Rows.Count > 0) Then
                If exporter = EnumExportTo.Excel Then
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    clsCommon.MyExportToExcelGrid("OUT STANDING ITEMS SENT AGAINST RETURNABLE GATE PASS", gv, arr, Me.Text)
                ElseIf exporter = EnumExportTo.PDF Then
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("OUT STANDING ITEMS SENT AGAINST RETURNABLE GATE PASS ", gv, arr, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Public Function ExporttoMyExcel(ByVal sql As String, ByVal frm As RadForm) As Boolean
        Dim sfd As SaveFileDialog = New SaveFileDialog()
        Dim Fullpath As String
        sfd.FileName = frm.Text
        Dim path As String

        sfd.Filter = "Excel Workbooks (*.xls;*.xlsx)|*.xls;*.xlsx"
        If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            path = sfd.FileName
            Fullpath = path
        Else
            Return False
        End If

        If Not path.Equals(String.Empty) Then
            Dim gv As New RadGridView()
            Try
                gv.Name = "OutStanding Items against RGP"
                frm.Controls.Add(gv)
                FillGridView(sql, gv)
                If gv.Rows.Count = 0 Then
                    Throw New Exception("Sorry No Data Found to Load Report")
                End If
                Dim i As Integer = 0
                For i = 0 To gv.ColumnCount - 1
                    Dim grow As GridViewRowInfo = TryCast(gv.Rows(0), GridViewRowInfo)
                    If TypeOf grow.Cells(i).Value Is DateTime Then
                        Dim datecol As GridViewDateTimeColumn = TryCast(gv.Columns(i), GridViewDateTimeColumn)
                        datecol.ExcelExportType = DisplayFormatType.ShortDate
                    End If
                    If TypeOf grow.Cells(i).Value Is Decimal Then
                        Dim datecol As GridViewDecimalColumn = TryCast(gv.Columns(i), GridViewDecimalColumn)
                        datecol.ExcelExportType = DisplayFormatType.Standard
                    End If
                Next i

                Dim exporter As New ExportToExcelML(gv)
                AddHandler exporter.ExcelCellFormatting, AddressOf exporter_ExcelCellFormatting
                exporter.ExportHierarchy = True
                ' exporter.ExportVisualSettings = True
                exporter.SheetMaxRows = ExcelMaxRows._65536
                exporter.SheetName = frm.Text
                exporter.RunExport(Fullpath)

                frm.Controls.Remove(gv)

                Dim xlsApp As Microsoft.Office.Interop.Excel.Application
                Dim xlsWB As Microsoft.Office.Interop.Excel.Workbook
                xlsApp = New Microsoft.Office.Interop.Excel.Application
                xlsApp.Visible = True
                xlsWB = xlsApp.Workbooks.Open(Fullpath)
                'common.clsCommon.MyMessageBoxShow("Excel Report Created!", "Export", MessageBoxButtons.OK)
                Return True
            Catch ex As Exception
                frm.Controls.Remove(gv)
                Throw New Exception(ex.Message)
                Return False
            End Try
        End If
        Return True
    End Function
    Private Sub exporter_ExcelCellFormatting(ByVal sender As Object, ByVal e As ExcelML.ExcelCellFormattingEventArgs)
        Try
            If e.GridRowInfoType Is GetType(GridViewTableHeaderRowInfo) Then
                e.ExcelStyleElement.FontStyle.Bold = False
                e.ExcelStyleElement.FontStyle.Size = 8
            End If
            e.ExcelStyleElement.FontStyle.Bold = False
            e.ExcelStyleElement.FontStyle.Size = 8
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        print(EnumExportTo.Excel)
    End Sub
    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Try
            Dim qry As String = " select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
            txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("OutStndLoc", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub txtVendor__My_Click(sender As Object, e As EventArgs) Handles txtVendor._My_Click
        Try
            Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name from TSPL_VENDOR_MASTER  WHERE  Status='N' "
            txtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("OutStndVnd", qry, "Code", "Name", txtVendor.arrValueMember, txtVendor.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub
    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Try
            Dim qry As String = "select item_Code as Code,Item_Desc as Name from  TSPL_ITEM_MASTER "
            txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("OutStndItem", qry, "Code", "Name", txtItem.arrValueMember, txtItem.arrDispalyMember)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub QuickExport(ByVal exportType As EnumExportTo, ByVal arrayOfParams As List(Of String))
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(dtpfromdate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtptodate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmRpt_OutStnd_Items_RGP & "'"))

            If txtDocNo.arrDispalyMember IsNot Nothing AndAlso txtDocNo.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Document No : " + clsCommon.GetMulcallStringWithComma(txtDocNo.arrDispalyMember))
            End If

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If
            If txtVendor.arrDispalyMember IsNot Nothing AndAlso txtVendor.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Vendor : " + clsCommon.GetMulcallStringWithComma(txtVendor.arrDispalyMember))
            End If

            'Dim sfd As SaveFileDialog = New SaveFileDialog()
            'Dim filePath As String
            'sfd.FileName = Me.Text
            'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"

            If exportType = EnumExportTo.PDF Then
                clsCommon.MyExportToPDF("OUT STANDING ITEMS SENT AGAINST RETURNABLE GATE PASS ", gv, arrHeader, Me.Text, True)
            Else

                'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                '    filePath = sfd.FileName
                transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
                'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)
                '    Else
                '    Exit Sub
                'End If
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub btnQuickExport_Click(sender As Object, e As EventArgs)
        QuickExport(EnumExportTo.Excel, Nothing)
    End Sub
    Private Sub txtDocNo__My_Click(sender As Object, e As EventArgs) Handles txtDocNo._My_Click
        Try
            Dim qry As String = " select  RGP_No Doc , RGP_Date Date , Doc_Type , Vendor_Name   from TSPL_RGP_HEAD "
            txtDocNo.arrValueMember = clsCommon.ShowMultipleSelectForm("OutStndDoc", qry, "Doc", "Date", txtItem.arrValueMember, txtItem.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub btnreset_Click(sender As Object, e As EventArgs) Handles btnreset.Click
        Reset()
    End Sub

    Private Sub btnPDF_Click(sender As Object, e As EventArgs) Handles btnPDF.Click
        print(EnumExportTo.PDF)
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
            common.clsCommon.MyMessageBoxShow(err.Message)
        End Try
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv.ColumnCount
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
End Class
