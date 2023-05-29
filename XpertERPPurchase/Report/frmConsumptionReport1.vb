Imports common
Imports Telerik.WinControls.UI.Export
' Update BY abhishek as on 29 oct 2012 4:35 pm For Excel
' Update BY abhishek as on 30 oct 2012 4:45 pm For Rate
Public Class FrmConsumptionReport1
    Inherits FrmMainTranScreen
    Dim qry As String
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmConsumptionReport1)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If

    End Sub
    Private Sub FrmConsumptionReport1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        dtpfromdate.Value = clsCommon.GETSERVERDATE()
        dtptodate.Value = clsCommon.GETSERVERDATE()
        chkItemAll.IsChecked = True
        chkLocationAll.IsChecked = True
        chkCategoryAll.IsChecked = True
        chkSubCategoryAll.IsChecked = True
        ItemLoad()
        LoadLocation()
        CategoryLoad()
        SubCategoryLoad()
        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P Print the Report")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+R Reset the Window")

    End Sub

    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "CONSM-RPT"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
    '        strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '        strTemp = Split(strRights, ",")
    '        If strTemp(0) = "0" Then
    '            MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    '            funSetUserAccess = False
    '            blnRead = False
    '            Me.Close()
    '            Exit Function
    '        Else
    '            blnRead = True
    '        End If
    '        If strTemp(1) = "0" Then 'Grant modify access

    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access

    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception
    '        myMessages.myExceptions(er)
    '    End Try
    'End Function


    Public Sub ItemLoad()
        qry = "select item_Code as Code,Item_Desc as Name from  TSPL_ITEM_MASTER  where  TSPL_ITEM_MASTER .Item_Type not in('F') "
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem.ValueMember = "Code"
    End Sub

    Public Sub LoadLocation()
        'Commment remove by abhishek kumar as on 19/06/2012
        Dim Qry As String = "select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(Qry)
        cbgLocation.ValueMember = "Code"
    End Sub
    Public Sub CategoryLoad()
        qry = "select Category_Code as Code,Category_Name  as Name from TSPL_Item_Category  "
        cbgCategory.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCategory.ValueMember = "Code"
    End Sub
    Public Sub SubCategoryLoad()
        qry = "select sub_Category_Code as Code,Description as Name  from TSPL_ITEM_SUB_CATEGORY  "
        cbgSubCategroy.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgSubCategroy.ValueMember = "Code"
    End Sub
    Public Sub PrintData()
        Try
            Dim qry As String
            Dim fromdate As String = clsCommon.myCDate(dtpfromdate.Value, "dd/MM/yyyy")
            Dim Todate As String = clsCommon.myCDate(dtptodate.Value, "dd/MM/yyyy")
            Dim ItemArr As ArrayList = cbgItem.CheckedValue
            Dim CategoryArr As ArrayList = cbgCategory.CheckedValue
            Dim SubCategoryArr As ArrayList = cbgSubCategroy.CheckedValue
            Dim locationArr As ArrayList = cbgLocation.CheckedValue
            Dim Address As String

            If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count = 1 Then
                Address = "(select max(ADD1 + case when len(add2)> 0 then ',' else '' end + ADD2 +case when len(add3)> 0 then ','else '' end +ADD3+case when len(add4)> 0 then ',' else '' end +ADD4+case when len(City_Code)> 0 then ',' else '' end +City_Code +case when len(TSPL_TDS_STATE_MASTER .State_Name)> 0 then ',' else '' end  +(TSPL_TDS_STATE_MASTER .State_Name )) from tspl_location_master LEFT OUTER JOIN TSPL_TDS_STATE_MASTER ON TSPL_LOCATION_MASTER .State =TSPL_TDS_STATE_MASTER .State_Code  where Location_Code = Final .LocationCode)"
            Else
                Address = "(TSPL_COMPANY_MASTER.Add1 + case When TSPL_COMPANY_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Add2, 103) End + Case When TSPL_COMPANY_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_COMPANY_MASTER.Add3,103) end + case When TSPL_COMPANY_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.City_Code, 103) end+ Case When TSPL_COMPANY_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_COMPANY_MASTER.State) end +  Case When TSPL_COMPANY_MASTER.Pincode='' Then '' Else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Pincode, 103)  end) "


            End If

            qry = "select '" + fromdate + "' as FromDate ,'" + Todate + "' as ToDate,category ,CategoryName ,SubCat ,SubCateName , Item_Code ,Item_Desc ,UOM,'' as LocationCode,'' as locdesc,OpBal ,OpBalRate ,convert(decimal(18,2),(isnull(opbal,0)*isnull(opbalrate,0))) as OPValue ,ReceiveQty,(RecvdRate/nullif(ReceiveQty,0))as RecvdRate  ,convert(decimal(18,2),(isnull(ReceiveQty,0)*isnull((RecvdRate/nullif(ReceiveQty,0)),0))) as ReceiveValue ,IssueQty ,(IssueRate/nullif(IssueQty,0))as IssueRate ,convert(decimal(18,2),(isnull(IssueQty,0)*isnull(IssueRate/nullif(IssueQty,0),0))) as IssueValue ,ClosingBalance,(OpBalRate+(RecvdRate/nullif(ReceiveQty,0))-(IssueRate/nullif(IssueQty,0)))as ClosingRate,convert(decimal(18,2),(isnull(ClosingBalance,0)* isnull(OpBalRate+(RecvdRate/nullif(ReceiveQty,0))-(IssueRate/nullif(IssueQty,0)),0))) as ClosingValue    ,CompCode,LocationCode,TSPL_COMPANY_MASTER.Comp_Name as compname, TSPL_COMPANY_MASTER.Logo_Img as Image1, TSPL_COMPANY_MASTER.Logo_Img2 as Image2," + Address + "as address " &
                   "   from (select category,MAX(CategoryName )as CategoryName,SubCat ,MAX(SubCateName )as SubCateName, Item_Code,MAX(Item_Desc) as Item_Desc,UOM,SUM(OpBal)  as OpBal,SUM(OpBalRate)as OpBalRate ,SUM(ReceiveQty) as ReceiveQty,SUM(RecvdRate ) as RecvdRate ,SUM(IssueQty) as IssueQty,SUM(IssueRate )as IssueRate ,SUM(OpBal+ReceiveQty-IssueQty) as ClosingBalance  ,Sum(OpBalRate +RecvdRate -IssueRate )as ClosingRate,MAX(CompCode)as CompCode,MAX(LocationCode )as LocationCode  " &
                   " from(select TSPL_ITEM_MASTER .item_category as category,TSPL_Item_Category .Category_Name as CategoryName,TSPL_ITEM_MASTER.Sub_item_category  as SubCat,TSPL_ITEM_SUB_CATEGORY .Description as SubCateName, TSPL_INVENTORY_MOVEMENT .Item_Code,TSPL_INVENTORY_MOVEMENT .Item_Desc,TSPL_INVENTORY_MOVEMENT.UOM,0 as ReceiveQty,0 as RecvdRate ,0 as IssueQty,0 as IssueRate,(Qty*case when InOut='I' then 1 else  case when InOut='O' then -1 else 0 end end) as OpBal,(nullif(Net_Cost,0)/nullIf(Qty,0)*case when InOut='I' then 1 else  case when InOut='O' then -1 else 0 end end) as OpBalRate,TSPL_INVENTORY_MOVEMENT .Comp_Code as CompCode,TSPL_INVENTORY_MOVEMENT .Location_Code as LocationCode from TSPL_INVENTORY_MOVEMENT left outer join TSPL_ITEM_MASTER on TSPL_INVENTORY_MOVEMENT .Item_Code =TSPL_ITEM_MASTER .Item_Code and TSPL_INVENTORY_MOVEMENT .UOM =TSPL_ITEM_MASTER .Unit_Code left outer join TSPL_ITEM_SUB_CATEGORY on TSPL_ITEM_SUB_CATEGORY.Category_Code =TSPL_ITEM_MASTER .item_category and TSPL_ITEM_SUB_CATEGORY .Sub_Category_Code =tspl_item_master.Sub_item_category left outer join TSPL_Item_Category  on TSPL_Item_Category.Category_Code = TSPL_ITEM_MASTER .item_category  where   TSPL_ITEM_MASTER .Item_Type not in('F') and    Convert(Date,TSPL_INVENTORY_MOVEMENT.Source_Doc_Date,103)<Convert(Date,'" + dtpfromdate.Value + "',103) "

            If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one Location")
                Return
            ElseIf chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0 Then
                '' Removed location_segmentCode by abhishek as on 19june 2012 said by amit sir
                qry += " and TSPL_INVENTORY_MOVEMENT .Location_Code   in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"

            End If

            If chkCategorySelect.IsChecked = True AndAlso cbgCategory.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one Category")
                Return
            ElseIf chkCategorySelect.IsChecked = True AndAlso cbgCategory.CheckedValue.Count > 0 Then
                qry += " and TSPL_ITEM_MASTER .Item_Category  in (" + clsCommon.GetMulcallString(CategoryArr) + ") "

            End If
            If chkSubCategroySelect.IsChecked = True AndAlso cbgSubCategroy.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one Sub Category")
                Return
            ElseIf chkSubCategroySelect.IsChecked = True AndAlso cbgSubCategroy.CheckedValue.Count > 0 Then
                qry += " and TSPL_ITEM_MASTER .Sub_item_category in (" + clsCommon.GetMulcallString(SubCategoryArr) + ") "

            End If


            If chkItemSelect.IsChecked = True AndAlso cbgItem.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one Item")
                Return
            ElseIf chkItemSelect.IsChecked = True AndAlso cbgItem.CheckedValue.Count > 0 Then
                qry += " and TSPL_INVENTORY_MOVEMENT.Item_Code  in (" + clsCommon.GetMulcallString(ItemArr) + ")"
            End If

            qry += "Union all"

            qry += "   select TSPL_ITEM_MASTER .item_category as category,TSPL_Item_Category .Category_Name as CategoryName,TSPL_ITEM_MASTER.Sub_item_category   as SubCat,TSPL_ITEM_SUB_CATEGORY .Description as SubCateName," &
                    " TSPL_INVENTORY_MOVEMENT.Item_Code,TSPL_INVENTORY_MOVEMENT.Item_Desc,TSPL_INVENTORY_MOVEMENT.UOM,case when InOut='I' then Qty else 0 end as ReceiveQty,case when InOut='I' then   Net_Cost  else 0 end as RecvdRate," &
                    "  case when InOut='O' then Qty else 0 end as IssueQty,case when InOut='O' then  Net_Cost  else 0 end as IssueRate,0 as OpBal,0 as OpBalRate,TSPL_INVENTORY_MOVEMENT .Comp_Code as CompCode," &
                    "  TSPL_INVENTORY_MOVEMENT .Location_Code as LocationCode  from TSPL_INVENTORY_MOVEMENT left outer join TSPL_ITEM_MASTER on TSPL_INVENTORY_MOVEMENT .Item_Code =TSPL_ITEM_MASTER .Item_Code and TSPL_INVENTORY_MOVEMENT .UOM =TSPL_ITEM_MASTER .Unit_Code left outer join TSPL_ITEM_SUB_CATEGORY on TSPL_ITEM_SUB_CATEGORY.Category_Code =TSPL_ITEM_MASTER .item_category and TSPL_ITEM_SUB_CATEGORY .Sub_Category_Code =tspl_item_master.Sub_item_category left outer join TSPL_Item_Category  on TSPL_Item_Category.Category_Code = TSPL_ITEM_MASTER .item_category  where  TSPL_ITEM_MASTER .Item_Type not in('F') and  Convert(Date,TSPL_INVENTORY_MOVEMENT.Source_Doc_Date,103)>=Convert(Date,'" + dtpfromdate.Value + "',103) and Convert(Date,TSPL_INVENTORY_MOVEMENT.Source_Doc_Date,103)<=convert(Date,'" + dtptodate.Value + "',103) "

            If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one Location")
                Return
            ElseIf chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0 Then
                qry += " and TSPL_INVENTORY_MOVEMENT .Location_Code   in (" + clsCommon.GetMulcallString(locationArr) + ")"
            End If

            If chkCategorySelect.IsChecked = True AndAlso cbgCategory.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one Category")
                Return
            ElseIf chkCategorySelect.IsChecked = True AndAlso cbgCategory.CheckedValue.Count > 0 Then
                qry += " and TSPL_ITEM_MASTER .Item_Category  in (" + clsCommon.GetMulcallString(CategoryArr) + ") "

            End If
            If chkSubCategroySelect.IsChecked = True AndAlso cbgSubCategroy.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one Sub Category")
                Return
            ElseIf chkSubCategroySelect.IsChecked = True AndAlso cbgSubCategroy.CheckedValue.Count > 0 Then
                qry += " and TSPL_ITEM_MASTER .Sub_item_category in (" + clsCommon.GetMulcallString(SubCategoryArr) + ") "

            End If


            If chkItemSelect.IsChecked = True AndAlso cbgItem.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one Item")
                Return
            ElseIf chkItemSelect.IsChecked = True AndAlso cbgItem.CheckedValue.Count > 0 Then
                qry += " and TSPL_INVENTORY_MOVEMENT.Item_Code  in (" + clsCommon.GetMulcallString(ItemArr) + ")"
            End If


            qry += "  )xxx group by category ,SubCat ,Item_Code,UOM) as Final left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER .Comp_Code =final.CompCode  "


            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Record Found")
            Else

                'dt = clsDBFuncationality.GetDataTable(qry)
                'PurchaseOrderViewer.funreport(dt, "ConsumptionReport", "Consumption Report")
                ExporttoMyExcel(qry, Me)

            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Public Sub ExporttoMyExcel(ByVal sql As String, ByVal frm As RadForm)
        Dim sfd As SaveFileDialog = New SaveFileDialog()
        Dim Fullpath As String
        sfd.FileName = frm.Text
        Dim path As String

        sfd.Filter = "Excel Workbooks (*.xls;*.xlsx)|*.xls;*.xlsx"
        If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            path = sfd.FileName
            Fullpath = path
        Else
            Exit Sub
        End If
        If Not path.Equals(String.Empty) Then
            Dim gv As New RadGridView()
            Try
                ''''' Dim exporter As New RadGridViewExcelExporter()
                gv.Name = "Consumption Report"
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
                '' Added By Abhishek For Show Excel Without save.
                Dim xlsApp As Microsoft.Office.Interop.Excel.Application
                Dim xlsWB As Microsoft.Office.Interop.Excel.Workbook
                xlsApp = New Microsoft.Office.Interop.Excel.Application
                xlsApp.Visible = True
                xlsWB = xlsApp.Workbooks.Open(Fullpath)
                'common.clsCommon.MyMessageBoxShow("Excel Report Created!", "Export", MessageBoxButtons.OK)
            Catch ex As Exception
                frm.Controls.Remove(gv)
                Throw New Exception(ex.Message)
            End Try
        End If
    End Sub
    Private Sub exporter_ExcelCellFormatting(ByVal sender As Object, ByVal e As ExcelML.ExcelCellFormattingEventArgs)
        If e.GridRowInfoType Is GetType(GridViewTableHeaderRowInfo) Then
            e.ExcelStyleElement.FontStyle.Bold = False
            e.ExcelStyleElement.FontStyle.Size = 8
        End If

        e.ExcelStyleElement.FontStyle.Bold = False
        e.ExcelStyleElement.FontStyle.Size = 8

    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        Reset()
    End Sub
    Sub Reset()
        dtpfromdate.Value = clsCommon.GETSERVERDATE()
        dtptodate.Value = clsCommon.GETSERVERDATE()
        chkItemAll.IsChecked = True
        chkLocationAll.IsChecked = True
        chkCategoryAll.IsChecked = True
        chkSubCategoryAll.IsChecked = True
        ItemLoad()
        LoadLocation()
        CategoryLoad()
        SubCategoryLoad()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        PrintData()
    End Sub
    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocationAll.IsChecked
    End Sub
    Private Sub chkItemAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkItemAll.ToggleStateChanged
        cbgItem.Enabled = Not chkItemAll.IsChecked
    End Sub
    Private Sub chkCategoryAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCategoryAll.ToggleStateChanged
        cbgCategory.Enabled = Not chkCategoryAll.IsChecked
    End Sub

    Private Sub chkSubCategoryAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkSubCategoryAll.ToggleStateChanged
        cbgSubCategroy.Enabled = Not chkSubCategoryAll.IsChecked
    End Sub

    Private Sub FrmConsumptionReport1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.P Then
            PrintData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            Reset()
        End If
    End Sub
End Class
