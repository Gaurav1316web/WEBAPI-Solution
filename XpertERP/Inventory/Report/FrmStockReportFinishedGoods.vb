Imports Telerik.WinControls
Imports System.Text.RegularExpressions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports common

Public Class FrmStockReportFinishedGoods
    Inherits FrmMainTranScreen
    Const colSelect As String = "SELECT"
    Const colCompCode As String = "COMPCODE"
    Const colCompName As String = "COMPNAME"
    Const colDataBaseName As String = "DATABASE"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dt As DataTable


    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.StockReportForFinishedGoods)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Sub SetDataBaseGrid()
        gvDB.Rows.Clear()
        gvDB.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.FormatString = ""
        repoSelect.HeaderText = "Select"
        repoSelect.Name = colSelect
        repoSelect.Width = 50
        repoSelect.ReadOnly = False
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvDB.MasterTemplate.Columns.Add(repoSelect)

        Dim repoCompCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCompCode.FormatString = ""
        repoCompCode.HeaderText = "Company Code"
        repoCompCode.Name = colCompCode
        repoCompCode.Width = 150
        repoCompCode.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoCompCode)

        Dim repoCompName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCompName.FormatString = ""
        repoCompName.HeaderText = "Company Name"
        repoCompName.Name = colCompName
        repoCompName.Width = 150
        repoCompName.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoCompName)

        Dim repoDB As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDB.FormatString = ""
        repoDB.HeaderText = "Database Name"
        repoDB.Name = colDataBaseName
        repoDB.IsVisible = False
        repoDB.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoDB)

        Dim qry As String = "SELECT Comp_Code,Comp_Name,DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0 "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                gvDB.Rows.AddNew()
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colSelect).Value = False
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colCompCode).Value = clsCommon.myCstr(dr("Comp_Code"))
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colCompName).Value = clsCommon.myCstr(dr("Comp_Name"))
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colDataBaseName).Value = clsCommon.myCstr(dr("DataBase_Name"))
            Next
        End If
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    End Sub


    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        PrintData()
    End Sub
    Sub PrintData()
        Try
            RefreshData()
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, "StockReportFinishGoods", " Stock Report For Finish Goods")
            frmCRV = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try

    End Sub

    Private Function GetSelectedDatabase() As List(Of String)
        Dim arrDBName As New List(Of String)
        For ii As Integer = 0 To gvDB.Rows.Count - 1
            If ((clsCommon.myCBool(gvDB.Rows(ii).Cells(colSelect).Value)) OrElse rbtnAllCompany.IsChecked) Then
                arrDBName.Add(clsCommon.myCstr(gvDB.Rows(ii).Cells(colDataBaseName).Value))
            End If
        Next
        Return arrDBName
    End Function


    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        rdoFull.IsChecked = True
        reset()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Sub LoadLocation()
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        'cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        'cbgLocation.ValueMember = "Code"
        'cbgLocation.DisplayMember = "Description"
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub

    Sub LoadItem()
        Dim qry As String = " select Item_Code as [Item Code],Item_Desc as [Description] from TSPL_ITEM_MASTER where Item_Type='f' "
        cgvitems.DataSource = clsDBFuncationality.GetDataTable(qry)
        cgvitems.ValueMember = "Item Code"
        cgvitems.DisplayMember = "Description"
    End Sub

    Private Sub FrmStockReportFinishedGoods_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnprint, "Press Ctrl+P Print the Report")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+R Reset the Window")
        reset()
        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub

    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "STK-FIN-RPT"
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

    Public Sub reset()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        'dtptodate.Value = clsCommon.GETSERVERDATE()
        SetDataBaseGrid()
        LoadLocation()
        LoadItem()
        chkLocAll.IsChecked = True
        chkitemall.IsChecked = True
        rbtnAllCompany.IsChecked = True
        gvReport.GroupDescriptors.Clear()
        gvReport.MasterTemplate.SummaryRowsBottom.Clear()
        dt = Nothing
        gvReport.DataSource = Nothing
        gvReport.Columns.Clear()
        gvReport.Rows.Clear()
    End Sub

    Private Sub chkitemall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkitemall.ToggleStateChanged
        cgvitems.Enabled = Not chkitemall.IsChecked
    End Sub

    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocAll.IsChecked
    End Sub

    Private Sub rbtnAllCompany_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnAllCompany.ToggleStateChanged
        gvDB.Enabled = Not rbtnAllCompany.IsChecked
    End Sub

    Private Sub FrmStockReportFinishedGoods_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.Control And e.KeyCode = Keys.P Then
            PrintData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            reset()
        End If
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            gvReport.EnableFiltering = True
            reset()
            RefreshData()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub RefreshData()
        Try
           
            If chkselect.IsChecked AndAlso cgvitems.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select at least one Item", Me.Text)
                Return
            End If
            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select at least one Location", Me.Text)
                Return
            End If

            '-----------------------------New -----------------------------------------

            Dim qry As String = ""
            Dim group1 As String = ""
            Dim sku As String = ""
            Dim ec As String = ""
            Dim value As String = ""
            If rdoFull.IsChecked = True Then
                value = " and  " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.UOM in('fc','fb')"
                ec = "Full"
            ElseIf rdoEmpty.IsChecked = True Then
                value = " and  " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.UOM in('ec','eb')"
                ec = "Empty"
            End If

            If ddltype.Text = "SKU" Then
                group1 = " 'SKU wise' as heading,Item_Code as grp ,Item_Code ,Class_Desc,Class_Desc1 , "
                sku = " " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Sku_Seq as Sku_Seq"

            ElseIf ddltype.Text = "Flavour" Then
                group1 = " 'Flavour wise' as heading, Class_Desc as grp ,Item_Code ,Class_Desc,Class_Desc1 ,"
                sku = "" + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Flavour_Seq as Sku_Seq "


            ElseIf ddltype.Text = "Pack" Then
                group1 = " 'Pack wise' as heading,  Class_Desc1 as grp ,Item_Code ,Class_Desc,Class_Desc1 ,"
                sku = "" + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Pack_Seq  as Sku_Seq"
            End If
            qry = " select  ('" + txtFromDate.Value + "') as Date," + group1 + "sum(Item_qty) as Item_qty ,('" + ec + "') as value, Location_Code,max(Location_Desc) as Location_Desc ,Comp_Code,max(Comp_Name) as Comp_Name,max(Add1) as Add1,Sku_Seq from (select * from ( " & _
                  " select " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Type, " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Trans_Type," + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Source_Doc_No," + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Item_Code," + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Desc," + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Desc,TSPL_ITEM_DETAILS_1.Class_Desc as Class_Desc1 , " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.UOM," + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Shipment_Type as Type, " & _
                  "  isnull((case when " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.UOM in ('FC','EC')  then (isnull(" + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Qty,0))* (isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) else  case when  isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)=0 then 0 else  (isnull(" + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Qty,0) / isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) end  end  ),0)    * (case when  " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.InOut='I' then 1 else -1 end ) as item_Qty , " & _
                  "    " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code, " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name, " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Add1, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Punching_Date  ," + sku + " " & _
                  "         from " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT " & _
                  "    left outer join " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER on " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER .Item_Code=" + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Item_Code  " & _
                  "   LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS  ON " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code " & _
                  " Left Outer Join " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 on " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code  " & _
                  "   left Outer Join " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER on " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code=" + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Comp_Code   " & _
                  "  left Outer Join " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code=" + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Location_Code  " & _
                  " left outer join " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code= " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Item_Code " & _
                  "  and " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code=" + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Uom " & _
                  " left outer join " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Shipment_No=" + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Source_Doc_No " & _
                  "  where(2 = 2) and  " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Name = 'flavour' and TSPL_ITEM_DETAILS_1.Class_Name = 'size'  and  CONVERT(date, " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Punching_Date, 103) <= CONVERT(date,'" + txtFromDate.Value + "', 103) and  " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Type='F' " + value + " "
            If chkLocSelect.IsChecked = True Then

                qry += " and  " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.Location_code in( Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + (clsCommon.GetMulcallString(cbgLocation.CheckedValue)) + ")) "

            End If
            If chkselect.IsChecked = True Then

                qry += " and  " + clsCommon.ReplicateDBString + "TSPL_INVENTORY_MOVEMENT.item_code in(" + (clsCommon.GetMulcallString(cgvitems.CheckedValue)) + ") "

            End If

            qry += " ) abc  where Type='sale' or  Type is null AND Location_Type='Physical') final group by Item_Code ,Class_Desc,Class_Desc1,Location_Code,Comp_Code,Sku_Seq"

            qry = clsCommon.GetQueryWithAllSelectedDataBase(qry, GetSelectedDatabase(), False)

            '--------------------------------------------------------------------------



            dt = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data found ")
            Else
                gvReport.DataSource = dt
                FormatGrid()
            End If
            RadPageView1.SelectedPage = RadPageViewPage2


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub

    Private Sub FormatGrid()
        'gvReport.AllowAddNewRow = False
        gvReport.TableElement.TableHeaderHeight = 40
        gvReport.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gvReport.Columns.Count - 1
            gvReport.Columns(ii).ReadOnly = True
            gvReport.Columns(ii).IsVisible = False
        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()


        gvReport.Columns("Date").IsVisible = True
        gvReport.Columns("Date").Width = 101
        gvReport.Columns("Date").HeaderText = "Date"

        gvReport.Columns("Heading").IsVisible = True
        gvReport.Columns("Heading").Width = 201
        gvReport.Columns("Heading").HeaderText = "Heading"

        gvReport.Columns("grp").IsVisible = True
        gvReport.Columns("grp").Width = 51
        gvReport.Columns("grp").HeaderText = "GRP"

        gvReport.Columns("Item_Code").IsVisible = True
        gvReport.Columns("Item_Code").Width = 101
        gvReport.Columns("Item_Code").HeaderText = "Item Code"

        gvReport.Columns("Class_Desc").IsVisible = True
        gvReport.Columns("Class_Desc").Width = 51
        gvReport.Columns("Class_Desc").HeaderText = "Class "

        gvReport.Columns("Class_Desc1").IsVisible = True
        gvReport.Columns("Class_Desc1").Width = 151
        gvReport.Columns("Class_Desc1").HeaderText = "Class Description "

        gvReport.Columns("Item_Qty").Width = 71
        gvReport.Columns("Item_Qty").HeaderText = "Quantity"
        gvReport.Columns("Item_Qty").IsVisible = True

        gvReport.Columns("Value").Width = 71
        gvReport.Columns("Value").HeaderText = "Value"
        gvReport.Columns("Value").IsVisible = True

        gvReport.Columns("Location_Code").IsVisible = True
        gvReport.Columns("Location_Code").Width = 51
        gvReport.Columns("Location_Code").HeaderText = "Location"
        
        gvReport.Columns("Location_Desc").IsVisible = True
        gvReport.Columns("Location_Desc").Width = 151
        gvReport.Columns("Location_Desc").HeaderText = "Location Desc"


        gvReport.Columns("Sku_Seq").IsVisible = True
        gvReport.Columns("Sku_Seq").Width = 101
        If ddltype.Text = "SKU" Then
            gvReport.Columns("Sku_Seq").HeaderText = "SKU Sequence"
        ElseIf ddltype.Text = "Flavour" Then
            gvReport.Columns("Sku_Seq").HeaderText = "Flavour Sequence"
        ElseIf ddltype.Text = "Pack" Then
            gvReport.Columns("Sku_Seq").HeaderText = "Pack Sequence"
        End If


        Dim item2 As New GridViewSummaryItem("Item_Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)

        gvReport.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gvReport.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom

    End Sub

    Private Sub btnExportToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportToExcel.Click
        Try
            RefreshData()
            If gvReport.DataSource Is Nothing OrElse gvReport.Rows.Count <= 0 Then
                Throw New Exception("No Data found to Export")
            End If
            ExportToExcelGV()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub ExportToExcelGV()
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("As On Date : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " ")
            If rbtnSelectCompany.IsChecked Then
                strTemp = ""
                For ii As Integer = 0 To gvDB.Rows.Count - 1
                    If clsCommon.myCBool(gvDB.Rows(ii).Cells(colSelect).Value) Then
                        Dim Str As String = clsCommon.myCstr(gvDB.Rows(ii).Cells(colCompName).Value)
                        If clsCommon.myLen(strTemp) > 0 Then
                            strTemp += ", "
                        End If
                        strTemp += Str
                    End If
                Next
                arrHeader.Add("Company : " + strTemp)
            End If
            
            If chkLocSelect.IsChecked Then
                strTemp = ""
                For Each Str As String In cbgLocation.CheckedValue
                    If clsCommon.myLen(strTemp) > 0 Then
                        strTemp += ", "
                    End If
                    strTemp += Str
                Next
                arrHeader.Add("Location Segment : " + strTemp)
            End If

            If chkselect.IsChecked Then
                strTemp = ""
                For Each Str As String In cgvitems.CheckedValue
                    If clsCommon.myLen(strTemp) > 0 Then
                        strTemp += ", "
                    End If
                    strTemp += Str
                Next
                arrHeader.Add("Items : " + strTemp)
            End If
            
            
            Dim ReportType As String = ddltype.Text
            
            clsCommon.MyExportToExcel("Stock Report For FInished Goods ( " + ReportType + ")", gvReport, arrHeader, Me.Text)
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            clsCommon.ProgressBarHide()
        End Try
    End Sub

End Class
