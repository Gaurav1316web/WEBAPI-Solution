'''''''''''''''''''''''''''''''''''''created by shipra on 25-06-2012''''''''''''''''''''''''''''''''''
' by vipin for pdf on 08/02/2013
'' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''

Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common
Public Class FrmRGP_Register_NRGP
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim qryExportToExcel As String = Nothing
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmRGP_Register_NRGP)
        'If Not (MyBase.isReadFlag) Then
        '    Throw New Exception("Permission Denied")
        'End If
        ' '' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
        'RadSplitButton1.Visible = MyBase.isExport

        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub


    Private Sub FrmRGP_Register_NRGP_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnreset.Enabled Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'savedata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            'postdata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            'deletedata()
        ElseIf e.Control AndAlso e.KeyCode = Keys.P AndAlso btnPrint.Enabled Then
            print(0)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub
    Private Sub FrmRGP_Register_NRGP_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ItemLoad()
        LoadVendor()
        LoadPo()
        LoadLocation()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P Print the Report")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+R Reset the Window")
        Reset()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click, RadButton6.Click
        Reset()
    End Sub
    Sub Reset()
        dtpfromdate.Value = clsCommon.GETSERVERDATE()
        dtptodate.Value = clsCommon.GETSERVERDATE()
        'chkCategoryAll.IsChecked = True
        chkItemAll.IsChecked = True
        chkLocAll.IsChecked = True
        'chkSubCategoryAll.IsChecked = True
        chkPoInvoiceAll.IsChecked = True
        chkVendorall.IsChecked = True
        ItemLoad()
        'CategoryLoad()
        'SubCategoryLoad()
        LoadLocation()
        LoadPo()
        LoadVendor()

        txtDocNo.arrValueMember = Nothing
        txtItem.arrValueMember = Nothing
        txtVendor.arrValueMember = Nothing
        txtLocation.arrValueMember = Nothing
        gv.DataSource = Nothing
        cmbtype.SelectedText = "NRGP"
        chkRgpStmt.Checked = False
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub chkVendorall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVendorall.ToggleStateChanged, chkVendorSelect.ToggleStateChanged
        cbgVendor.Enabled = Not chkVendorall.IsChecked
    End Sub

    Private Sub chkItemAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkItemAll.ToggleStateChanged, chkItemselect.ToggleStateChanged
        cbgItem.Enabled = Not chkItemAll.IsChecked
    End Sub

    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocAll.IsChecked
    End Sub

    'Private Sub chkPoInvoiceAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
    '    cbgPoInvoice.Enabled = True
    'End Sub

    Private Sub dtpfromdate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpfromdate.ValueChanged

        LoadPo()

    End Sub

    Private Sub dtptodate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtptodate.ValueChanged

        LoadPo()

    End Sub

    Private Sub LoadReportData(ByVal companyCode As String)
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
            Dim qry As String = Nothing
            Dim fromdate As String = clsCommon.GetPrintDate(dtpfromdate.Value, "dd/MMM/yyyy")
            Dim Todate As String = clsCommon.GetPrintDate(dtptodate.Value, "dd/MMM/yyyy")
            Dim ItemArr As ArrayList = cbgItem.CheckedValue
            Dim locationArr As ArrayList = cbgLocation.CheckedValue
            Dim VendorArr As ArrayList = cbgVendor.CheckedValue
            Dim Po As ArrayList = cbgPoInvoice.CheckedValue

            Try
                qry = "SELECT " & _
                        " (fnlQry.[RGP No])   AS [RGP No], " & _
                  " MAX(fnlQry.[Doc Type]) AS [Doc Type], " & _
                  " MAX(fnlQry.[RGP Date]) AS [RGP Date], " & _
                  " MAX(FNLQRY.[Loc Code]) AS [Loc Code], " & _
                  " MAX(FNLQRY.[Loc Desc]) AS [Location], " & _
                  "    (fnlQry.[Item Code]) AS [Item Code], " & _
                  " MAX(fnlQry.[Item Desc]) AS [Item Desc], " & _
                  " (fnlQry.[Delivered By]) AS [Delivered By], " & _
                  " MAX(fnlQry.[Delivered By Desc]) AS [Delivered By Desc], " & _
                  " MAX(fnlQry.[UOM]) AS [UOM], " & _
                  " SUM(fnlQry.[Qty]) AS [Qty], " & _
                  " MAX(fnlQry.[Expected Date of Return]) AS [Expected Date of Return], " & _
                  " MAX(fnlQry.[Purpose]) AS [Purpose], " & _
                  " MAX(fnlQry.[Party Code]) AS [Party Code], " & _
                  " MAX(fnlQry.[Party Name]) AS [Party Name], " & _
                  " MAX(fnlQry.[Authorised By]) AS [Authorised By], " & _
                  " MAX(fnlQry.[Authorised Person Dept.]) AS [Authorised Person Dept.], " & _
                  " MAX(fnlQry.[Action To Be Taken By]) AS [Action To Be Taken By], " & _
                  " MAX(fnlQry.[Remarks]) AS [Remarks] " & _
                  " FROM (SELECT " & _
                  " COALESCE(rgpHead.RGP_No, '') AS [RGP No], " & _
                  " COALESCE(rgpHead.Doc_Type, '') AS [Doc Type], " & _
                  " FORMAT(cast(coalesce(rgpHead.RGP_Date,'') as date),'dd/MMM/yyyy') AS [RGP Date] , " & _
                  " COALESCE(rgpHead.Location, '') AS [Loc Code], " & _
                  " COALESCE(TSPL_LOCATION_MASTER.Location_Desc, '') AS [Loc Desc], " & _
                  " COALESCE(rgpDtl.Item_Code, '') AS [Item Code], " & _
                  " COALESCE(rgpDtl.Item_Desc, '') AS [Item Desc], " & _
                  " COALESCE(rgpHead.Delivered_By, '') AS [Delivered By],  " & _
                  " COALESCE(TSPL_EMPLOYEE_MASTER.emp_name, '') AS [Delivered By Desc],  " & _
                  " COALESCE(rgpDtl.Unit_code, '') AS [UOM], " & _
                  " COALESCE(rgpDtl.RGP_Qty, 0) AS [Qty], " & _
                  " COALESCE('', '') AS [Expected Date of Return], " & _
                  " COALESCE(rgpHead.Reason, '') AS [Purpose], " & _
                  " COALESCE(rgpHead.Vendor_Code, '') AS [Party Code], " & _
                  " COALESCE(rgpHead.Vendor_Name, '') AS [Party Name], " & _
                  " COALESCE(rgpHead.Modify_By, '') AS [Authorised By], " & _
                  " COALESCE(dptTbl.DESCRIPTION, '') AS [Authorised Person Dept.], " & _
                  " COALESCE(rgpHead.Modify_By, '') AS [Action To Be Taken By], " & _
                  " COALESCE(rgpHead.Remarks, '') AS [Remarks] " & _
                  "  FROM TSPL_RGP_HEAD AS rgpHead " & _
                  " LEFT JOIN TSPL_RGP_DETAIL rgpDtl " & _
                  "    ON rgpHead.RGP_No = rgpDtl.RGP_No " & _
                  " LEFT JOIN TSPL_DEPARTMENT_MASTER AS dptTbl " & _
                  "    ON dptTbl.DEPARTMENT_CODE = rgpHead.Department " & _
                  " LEFT JOIN TSPL_LOCATION_MASTER " & _
                  "    ON TSPL_LOCATION_MASTER.Location_Code = rgpHead.Location " & _
                  " LEFT JOIN  TSPL_EMPLOYEE_MASTER " & _
                  "    ON TSPL_EMPLOYEE_MASTER.EMP_CODE = rgpHead.Delivered_By ) AS fnlQry " & _
                " WHERE 1 = 1 "
                If txtDocNo.arrValueMember IsNot Nothing AndAlso txtDocNo.arrValueMember.Count > 0 Then
                    qry += " AND fnlQry.[RGP No] IN (" + clsCommon.GetMulcallString(txtDocNo.arrValueMember) + ") " + Environment.NewLine
                End If
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    qry += " AND fnlQry.[Loc Code] IN (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") " + Environment.NewLine
                End If
                If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                    qry += "  and fnlQry.[Item Code] IN (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ") " + Environment.NewLine
                End If
                qry += "AND fnlQry.[Doc Type] IN ( '" + cmbtype.Text + "') "
                If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                    qry += " AND fnlQry.[Party Code] IN (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ") " + Environment.NewLine
                End If
                If clsCommon.myLen(fromdate) > 0 AndAlso clsCommon.myLen(Todate) > 0 Then
                    qry += " AND fnlQry.[RGP Date] BETWEEN (CONVERT(date, '" + fromdate + "', 103)) AND (CONVERT(date, '" + Todate + "', 103)) "
                End If
                qry += " GROUP BY fnlQry.[RGP No] , " & _
                                "fnlQry.[Item Code] , [Delivered By] " & _
                       " ORDER BY fnlQry.[RGP No] , fnlQry.[Item Code] "
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    gv.DataSource = Nothing
                    gv.Rows.Clear()
                    gv.Columns.Clear()
                    gv.DataSource = dt
                    gv.MasterTemplate.ShowRowHeaderColumn = False
                    For Each col As GridViewColumn In gv.Columns
                        col.ReadOnly = True
                        col.Width = 150
                        If col.Name = "Delivered By" Then
                            col.IsVisible = False
                        End If
                        If col.Name = "Loc Code" Then
                            col.IsVisible = False
                        End If
                        If col.Name = "Location" Then
                            col.IsVisible = False
                        End If
                        If col.Name = "Party Code" Then
                            col.IsVisible = False
                        End If
                    Next
                    RadPageView1.SelectedPage = RadPageViewPage2
                    gv.BestFitColumns()
                    If qry IsNot Nothing AndAlso clsCommon.myLen(qry) > 0 Then
                        qryExportToExcel = qry
                    End If
                Else
                    clsCommon.MyMessageBoxShow("No Data Found", Me.Text)
                End If

            Catch ex As Exception
                common.clsCommon.MyMessageBoxShow(ex.Message)
            End Try
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click

        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal AndAlso chkRgpStmt.Checked Then
            LoadReportData(objCommonVar.CurrentCompanyCode.ToString().Trim())
        Else
            Try

                Dim qry As String
                Dim fromdate As String = clsCommon.GetPrintDate(dtpfromdate.Value, "dd/MMM/yyyy")
                Dim Todate As String = clsCommon.GetPrintDate(dtptodate.Value, "dd/MMM/yyyy")
                Dim ItemArr As ArrayList = cbgItem.CheckedValue
                Dim locationArr As ArrayList = cbgLocation.CheckedValue
                Dim VendorArr As ArrayList = cbgVendor.CheckedValue
                Dim Po As ArrayList = cbgPoInvoice.CheckedValue


                'Dim location As String

                'Dim Vendor As String
                'Dim StrItem As String
                'Dim Strlocation As String
                'Dim StrDocNo As String
                'Dim StrVendor As String


                qry = "select convert(varchar,TSPL_RGP_HEAD.RGP_Date,103) as 'Vr.Date',TSPL_RGP_HEAD.RGP_No as SlipNo,TSPL_RGP_HEAD.Vendor_Code as Party,TSPL_RGP_HEAD.Vendor_Name as 'Name Of Party',TSPL_RGP_HEAD.Remarks ,TSPL_RGP_DETAIL.Item_Code as Item ,TSPL_RGP_DETAIL.Item_Desc as 'Name Of Item',TSPL_RGP_HEAD.Reason as Description,TSPL_RGP_DETAIL.Unit_code as UOM ,TSPL_RGP_DETAIL.RGP_Qty as Quantity,TSPL_RGP_DETAIL.Item_Cost as Rate,TSPL_RGP_DETAIL.Amount as Value from TSPL_RGP_HEAD left outer join TSPL_RGP_DETAIL  on TSPL_RGP_HEAD .RGP_No =TSPL_RGP_DETAIL.RGP_No  where 1=1 "


                '====added by shivani
                If txtDocNo.arrValueMember IsNot Nothing AndAlso txtDocNo.arrValueMember.Count > 0 Then
                    qry += " and TSPL_RGP_HEAD.RGP_No In (" + clsCommon.GetMulcallString(txtDocNo.arrValueMember) + ") " + Environment.NewLine
                End If
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    qry += " and TSPL_RGP_HEAD.Location  in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") " + Environment.NewLine
                End If
                If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                    qry += "  and TSPL_RGP_DETAIL .Item_Code in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ") " + Environment.NewLine
                End If
                '=================
                qry += " and TSPL_RGP_HEAD.Doc_Type = '" + cmbtype.Text + "' "  '--Added By Pankaj Kymar--on--24/09/2012-----



                If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                    qry += "   and TSPL_RGP_HEAD .Vendor_Code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ") " + Environment.NewLine
                End If
                qry += "and (convert(date,TSPL_RGP_HEAD.RGP_Date,103)>='" + fromdate + "' and convert(date,TSPL_RGP_HEAD.RGP_Date,103)<='" + Todate + "') order by RGP_Date"

                Dim dtgv As New DataTable
                dtgv = clsDBFuncationality.GetDataTable(qry)
                If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then

                    gv.DataSource = Nothing
                    gv.Rows.Clear()
                    gv.Columns.Clear()
                    gv.DataSource = dtgv


                    gv.MasterTemplate.ShowRowHeaderColumn = False
                    For ii As Integer = 0 To gv.Columns.Count - 1
                        gv.Columns(ii).ReadOnly = True
                    Next
                    gv.BestFitColumns()
                    RadPageView1.SelectedPage = RadPageViewPage2
                Else
                    clsCommon.MyMessageBoxShow("No Data Found", Me.Text)

                End If



            Catch ex As Exception
                common.clsCommon.MyMessageBoxShow(ex.Message)
            End Try
        End If
    End Sub
    Sub print(ByVal companyCode As String, ByVal exporter As EnumExportTo)
        Try
            Dim fromdate As String = clsCommon.GetPrintDate(dtpfromdate.Value, "dd/MMM/yyyy")
            Dim Todate As String = clsCommon.GetPrintDate(dtptodate.Value, "dd/MMM/yyyy")
            Dim Item As String = ""
            Dim location As String = ""
            Dim DocNo As String = ""
            Dim Vendor As String = ""
            Dim StrItem As String = ""
            Dim Strlocation As String = ""
            Dim StrDocNo As String = ""
            Dim StrVendor As String = ""
            Dim arr As New List(Of String)()
            '-----------------------------------------------------------------------------------------------------
            arr.Add("  From Date:  " + fromdate + "  To Date: " + Todate + "")
            arr.Add("Company : " + objCommonVar.CurrentCompanyName)
            If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                arr.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If
            If txtDocNo.arrDispalyMember IsNot Nothing AndAlso txtDocNo.arrDispalyMember.Count > 0 Then
                arr.Add("Document No : " + clsCommon.GetMulcallStringWithComma(txtDocNo.arrDispalyMember))
            End If
            '-----------------------------------------------------------------------------------------------------
            If txtVendor.arrDispalyMember IsNot Nothing AndAlso txtVendor.arrDispalyMember.Count > 0 Then
                arr.Add(" Vendor : " + clsCommon.GetMulcallStringWithComma(txtVendor.arrDispalyMember))
            End If
            '-----------------------------------------------------------------------------------------------------
            If gv.Rows.Count > 0 Then
                If exporter = EnumExportTo.Excel Then
                    clsCommon.MyExportToExcelGrid("RGP NRGP Report Statements", gv, arr, Me.Text)
                ElseIf exporter = EnumExportTo.PDF Then
                    clsCommon.MyExportToPDF("RGP NRGP Report Statements", gv, arr, Me.Text, True)
                End If
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try

            Dim qry As String
            Dim fromdate As String = clsCommon.GetPrintDate(dtpfromdate.Value, "dd/MMM/yyyy")
            Dim Todate As String = clsCommon.GetPrintDate(dtptodate.Value, "dd/MMM/yyyy")
            'Dim ItemArr As ArrayList = cbgItem.CheckedValue
            ''Dim locationArr As ArrayList = cbgLocation.CheckedValue
            'Dim VendorArr As ArrayList = cbgVendor.CheckedValue
            'Dim Po As ArrayList = cbgPoInvoice.CheckedValue

            Dim Item As String = ""
            Dim location As String = ""
            Dim DocNo As String = ""
            Dim Vendor As String = ""
            Dim StrItem As String = ""
            Dim Strlocation As String = ""
            Dim StrDocNo As String = ""
            Dim StrVendor As String = ""

            qry = "select convert(varchar,TSPL_RGP_HEAD.RGP_Date,103) as 'Vr.Date',TSPL_RGP_HEAD.RGP_No as SlipNo,TSPL_RGP_HEAD.Vendor_Code as Party,TSPL_RGP_HEAD.Vendor_Name as 'Name Of Party',TSPL_RGP_HEAD.Remarks ,TSPL_RGP_DETAIL.Item_Code as Item ,TSPL_RGP_DETAIL.Item_Desc as 'Name Of Item',TSPL_RGP_HEAD.Reason as Description,TSPL_RGP_DETAIL.Unit_code as UOM ,TSPL_RGP_DETAIL.RGP_Qty as Quantity,TSPL_RGP_DETAIL.Item_Cost as Rate,TSPL_RGP_DETAIL.Amount as Value from TSPL_RGP_HEAD left outer join TSPL_RGP_DETAIL  on TSPL_RGP_HEAD .RGP_No =TSPL_RGP_DETAIL.RGP_No  where 1=1 "

            '====added by shivani
            If txtDocNo.arrValueMember IsNot Nothing AndAlso txtDocNo.arrValueMember.Count > 0 Then
                qry += " and TSPL_RGP_HEAD.RGP_No In (" + clsCommon.GetMulcallString(txtDocNo.arrValueMember) + ") " + Environment.NewLine
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                qry += " and TSPL_RGP_HEAD.Location  in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") " + Environment.NewLine
            End If
            If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                qry += "  and TSPL_RGP_DETAIL .Item_Code in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ") " + Environment.NewLine
            End If
            '=================

            qry += " and TSPL_RGP_HEAD.Doc_Type = '" + cmbtype.Text + "' "  '--Added By Pankaj Kymar--on--24/09/2012-----

            If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                qry += "   and TSPL_RGP_HEAD .Vendor_Code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ") " + Environment.NewLine
            End If

            qry += "and (convert(date,TSPL_RGP_HEAD.RGP_Date,103)>='" + fromdate + "' and convert(date,TSPL_RGP_HEAD.RGP_Date,103)<='" + Todate + "') order by RGP_Date"

            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(qry)
            If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then

                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                gv.DataSource = dtgv

            End If

            Dim arr As New List(Of String)()
            arr.Add("  From Date:  " + fromdate + "  To Date: " + Todate + "")
            arr.Add("Company : " + objCommonVar.CurrentCompanyName)
            If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                arr.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If
            If txtDocNo.arrDispalyMember IsNot Nothing AndAlso txtDocNo.arrDispalyMember.Count > 0 Then
                arr.Add("Document No : " + clsCommon.GetMulcallStringWithComma(txtDocNo.arrDispalyMember))
            End If

            If txtVendor.arrDispalyMember IsNot Nothing AndAlso txtVendor.arrDispalyMember.Count > 0 Then
                arr.Add(" Vendor : " + clsCommon.GetMulcallStringWithComma(txtVendor.arrDispalyMember))
            End If
            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("RGP NRGP Report", gv, arr, Me.Text)
            ElseIf exporter = EnumExportTo.PDF Then
                clsCommon.MyExportToPDF("RGP NRGP Report", gv, arr, Me.Text, True)
            End If



            'ExporttoMyExcel(qry, Me)
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
                ''''' Dim exporter As New RadGridViewExcelExporter()
                gv.Name = "RegisterNRGP"
                frm.Controls.Add(gv)
                FillGridView(sql, gv)
                If gv.Rows.Count = 0 Then
                    Throw New Exception("There is no data for Show Excel Report.")
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
                '    exporter.Export(gv, path, frm.Text)

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
    End Function
    Private Sub exporter_ExcelCellFormatting(ByVal sender As Object, ByVal e As ExcelML.ExcelCellFormattingEventArgs)
        If e.GridRowInfoType Is GetType(GridViewTableHeaderRowInfo) Then
            e.ExcelStyleElement.FontStyle.Bold = False
            e.ExcelStyleElement.FontStyle.Size = 8
        End If

        e.ExcelStyleElement.FontStyle.Bold = False
        e.ExcelStyleElement.FontStyle.Size = 8

    End Sub


    Private Sub chkPoInvoiceAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkPoInvoiceAll.ToggleStateChanged, chkPoInvoiceSelect.ToggleStateChanged
        cbgPoInvoice.Enabled = Not chkPoInvoiceAll.IsChecked
    End Sub


    Private Sub cmbtype_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cmbtype.SelectedIndexChanged
        LoadPo()
    End Sub
    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Try
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal AndAlso chkRgpStmt.Checked Then
                print(objCommonVar.CurrentCompanyCode, EnumExportTo.Excel)
            Else
                print(EnumExportTo.Excel)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        Try
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal AndAlso chkRgpStmt.Checked Then
                print(objCommonVar.CurrentCompanyCode, EnumExportTo.PDF)
            Else
                print(EnumExportTo.PDF)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    ' KUNAL > TICKET : BM00000009573 > DATE : 03-NOV-106 
    Private Sub gv_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellDoubleClick
        Try
            If chkRgpStmt.Checked = False Then
                If gv.Rows.Count > 0 Then
                    Dim strDoc As String = Nothing
                    strDoc = gv.CurrentRow.Cells("SlipNo").Value
                    If cmbtype.Text = "RGP" Then
                        clsOpenTransactionForm.OpenTransacionForm(EnumTransType.RGP, strDoc)
                    ElseIf cmbtype.Text = "NRGP" Then
                        clsOpenTransactionForm.OpenTransacionForm(EnumTransType.NRGP, strDoc)
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Public Sub ItemLoad()
        Dim qry = "select item_Code as Code,Item_Desc as Name from  TSPL_ITEM_MASTER  "
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem.ValueMember = "Code"
    End Sub
    Public Sub LoadLocation()
        Dim Qry As String = "select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(Qry)
        cbgLocation.ValueMember = "Code"
    End Sub
    Public Sub LoadPo()
        Dim Qry As String = "select RGP_no as 'DocNo' ,RGP_Date  from  TSPL_RGP_HEAD where Doc_type='" + cmbtype.Text + "' and Convert(Date,RGP_Date,103) >=convert(date,'" + dtpfromdate.Value + "',103) and Convert(Date,RGP_Date,103) <=Convert(Date,'" + dtptodate.Value + "',103)"
        cbgPoInvoice.DataSource = clsDBFuncationality.GetDataTable(Qry)
        cbgPoInvoice.ValueMember = "DocNo"
    End Sub

    Public Sub LoadVendor()
        Dim Qry As String = "select Vendor_Code as Code,Vendor_Name as Description from TSPL_VENDOR_MASTER   WHERE  Status='N' "
        cbgVendor.DataSource = clsDBFuncationality.GetDataTable(Qry)
        cbgVendor.ValueMember = "Code"
    End Sub
    'Private Sub txtDocNo__My_Click(sender As Object, e As EventArgs)
    '    Dim qry As String = "select RGP_no as Code ,RGP_Date as Date from  TSPL_RGP_HEAD where Doc_type='" + cmbtype.Text + "' and Convert(Date,RGP_Date,103) >=convert(date,'" + dtpfromdate.Value + "',103) and Convert(Date,RGP_Date,103) <=Convert(Date,'" + dtptodate.Value + "',103)"
    '    txtDocNo.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Date", txtDocNo.arrValueMember, txtDocNo.arrDispalyMember)
    'End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        FrmPendingRequisitionQty.SetDiplayMember(txtLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub

    Private Sub txtVendor__My_Click(sender As Object, e As EventArgs) Handles txtVendor._My_Click
        Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name from TSPL_VENDOR_MASTER  WHERE  Status='N' "
        txtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtVendor.arrValueMember, txtVendor.arrDispalyMember)
    End Sub

    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Dim qry As String = "select item_Code as Code,Item_Desc as Name from  TSPL_ITEM_MASTER "
        txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtItem.arrValueMember, txtItem.arrDispalyMember)
    End Sub

    Private Sub btnQuickExport_Click(sender As Object, e As EventArgs) Handles btnQuickExport.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(dtpfromdate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtptodate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmRGP_Register_NRGP & "'"))
            If txtDocNo.arrDispalyMember IsNot Nothing AndAlso txtDocNo.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Document No : " + clsCommon.GetMulcallStringWithComma(txtDocNo.arrDispalyMember))
            End If

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If
            If txtVendor.arrDispalyMember IsNot Nothing AndAlso txtVendor.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Vendor : " + clsCommon.GetMulcallStringWithComma(txtVendor.arrDispalyMember))
            End If

            Dim sfd As SaveFileDialog = New SaveFileDialog()
            Dim filePath As String
            sfd.FileName = Me.Text
            If sfd.FileName.Contains("/") Then
                sfd.FileName = sfd.FileName.Replace("/", " ")
            End If
            sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                filePath = sfd.FileName
            Else
                Exit Sub
            End If
            transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            common.clsCommon.MyMessageBoxShow("Exported Successfully.", Me.Text)
            Process.Start(filePath)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtDocNo__My_Click_1(sender As Object, e As EventArgs) Handles txtDocNo._My_Click
        Dim qry As String = "select RGP_no as Code ,RGP_Date as Name from  TSPL_RGP_HEAD where Doc_type='" + cmbtype.Text + "' and Convert(Date,RGP_Date,103) >=convert(date,'" + dtpfromdate.Value + "',103) and Convert(Date,RGP_Date,103) <=Convert(Date,'" + dtptodate.Value + "',103)"
        txtDocNo.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtDocNo.arrValueMember, txtDocNo.arrDispalyMember)


    End Sub

End Class
