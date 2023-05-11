''''' bug no BM00000000229 , BM00000000548 ,BM00000000540,BM00000000617
'' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
'---Preeti Gupta--Ticket No-BM00000003031

Imports common
Public Class frmRptInventoryMovement
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.mbtnItemMovement)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        '' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
        btnExport.Visible = MyBase.isExport

        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P Print the Report")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+R Reset the Window")
        btnPrint.Visible = MyBase.isPrintFlag
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub FrmRptSales_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        chkLocAll.IsChecked = True
        LoadLocation()
        rdbSummary.IsChecked = True
        chkLocAll.IsChecked = True
        chkIemAll.IsChecked = True
        LoadItem()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        rdbFinish.IsChecked = True
        LoadMrp()
        chkmrpall.IsChecked = True
        'AddHandler txtItem.txtValue.TextChanged, AddressOf FinderToCustomer_txtChanged
        'AddHandler txtLocation.txtValue.TextChanged, AddressOf txtLocation_txtChanged
        'AddHandler txtItem.txtValue.Leave, AddressOf txtSalesman_Leave
        'AddHandler txtLocation.txtValue.Leave, AddressOf txtLocation_Leave
        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub

    'Private Sub txtSalesman_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If (clsCommon.myLen(txtItem.txtValue.Text) > 0) Then
    '        Dim qry As String = "select 1 from TSPL_ITEM_MASTER where item_code='" + txtItem.txtValue.Text + "'"
    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '        If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
    '            common.clsCommon.MyMessageBoxShow("Item Code Not Exist")
    '            txtItem.SelectedValue = ""
    '            txtItem.txtValue.Text = ""
    '        End If
    '    End If
    'End Sub

    Sub LoadLocation()
        Dim qry As String = "select Location_Code as Location,Location_Desc as [Location Description] from TSPL_LOCATION_MASTER where Location_Type='Physical'"
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Location"
        cbgLocation.DisplayMember = "Location Description"
    End Sub

    Sub LoadMrp()
        Dim qry As String = " select distinct  CONVERT(varchar(20),MRP) as MRP ,Item_Code as Code from TSPL_INVENTORY_MOVEMENT where 2=2"
        If chkitemSelect.IsChecked = True AndAlso cbgitem.CheckedValue.Count > 0 Then
            qry += " and Item_Code in (" + clsCommon.GetMulcallString(cbgitem.CheckedValue) + ")"
        End If
        If chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0 Then
            qry += " and Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
        End If
        cbgmrp.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgmrp.ValueMember = "MRP"
        cbgmrp.DisplayMember = "Code"
    End Sub
    'Private Sub txtLocation_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If (clsCommon.myLen(txtLocation.txtValue.Text) > 0) Then
    '        Dim qry As String = "select 1 from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.txtValue.Text + "'"
    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '        If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
    '            common.clsCommon.MyMessageBoxShow("Location Code Not Exist")
    '            txtLocation.SelectedValue = ""
    '            txtLocation.txtValue.Text = ""
    '        End If
    '    End If
    'End Sub

    'Private Sub txtLocation_txtChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.txtValue.Text + "'"))
    'End Sub

    'Private Sub FinderToCustomer_txtChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    lblItem.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_Desc from TSPL_ITEM_MASTER where item_code='" + txtItem.txtValue.Text + "'"))
    'End Sub

    'Private Sub findToCustomer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtItem.Load
    '    txtItem.ConnectionString = connectSql.SqlCon()
    '    txtItem.Query = "select item_code as Code,item_Desc  as Name from TSPL_ITEM_MASTER order by Item_Code"
    '    txtItem.ValueToSelect = "Code"
    '    txtItem.Caption = "Code"
    '    txtItem.ValueToSelect1 = "Name"
    'End Sub

    Sub LoadItem()
        Dim qry As String = " select item_code ,item_Desc  from TSPL_ITEM_MASTER order by Item_Code "
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem.ValueMember = "item_code"
        cbgItem.DisplayMember = "item_Desc"
    End Sub

    'Private Sub txtLocation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    txtLocation.ConnectionString = connectSql.SqlCon()
    '    txtLocation.Query = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER order by Location_Code"
    '    txtLocation.ValueToSelect = "Code"
    '    txtLocation.Caption = "Code"
    '    txtLocation.ValueToSelect1 = "Name"
    'End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        PrintData()
    End Sub
    Sub PrintData()

        Try
            Dim StrQuery As String
            Dim strItemtype As String = ""
            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Select Atleast Single Location")
            ElseIf chkItemSelect.IsChecked AndAlso cbgItem.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Select Atleast Single Item")
            End If

            If rdbFinish.IsChecked = True Then
                strItemtype = " and (Item_Type='F')"
            ElseIf rdbOthers.IsChecked = True Then
                strItemtype = " and (Item_Type='O')"
            ElseIf rdbRaw.IsChecked = True Then
                strItemtype = " and (Item_Type='R')"

            End If


            If rdbSummary.IsChecked Then
                StrQuery = "SELECT     0 as OP,a.Item_Code, " & _
                "a.Item_Desc,cast('ITF Code-'+(tspl_item_master.itf_code)as varchar) as itf_code,  " & _
                "(a.Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as Qty, " & _
                " a.InOut, " & _
                " case when a.trans_type='Load Out' then sale_invoice_no else  a.Source_Doc_No end as Source_Doc_No,  " & _
                "a.Punching_Date, " & _
                "a.Entry_Date, " & _
                "case when a.Trans_Type='Transfer' and InOut='I' then  (a.Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * isnull((select Top 1 Item_Price from TSPL_TRANSFER_DETAIL   where TSPL_TRANSFER_DETAIL.Transfer_No=a.Source_Doc_No and TSPL_TRANSFER_DETAIL.Item_Code =a.Item_Code and TSPL_TRANSFER_DETAIL.Uom =a.UOM  and TSPL_TRANSFER_DETAIL.MRP=a.MRP  ),0)  else Net_Cost end as Net_Cost, " & _
                "a.Location_Code, " & _
                "a.Trans_Type,a.ItemType,case when InOut ='I' then Net_Cost  else 0 end as InCost,case when InOut ='I' then Qty else 0 end as InQty,case when InOut ='O' then Qty else 0 end as OutQty,case when InOut ='O' then Net_Cost  else 0 end as OutCost, " & _
                " '' AS Fdate, " & _
                "'' AS Tdate, " & _
                "b.Location_Desc, " & _
                "a.MRP, " & _
                "0 as CB FROM TSPL_INVENTORY_MOVEMENT AS a INNER JOIN " & _
                " TSPL_LOCATION_MASTER AS b ON a.Location_Code = b.Location_Code INNER JOIN " & _
                " TSPL_ITEM_UOM_DETAIL ON a.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND a.UOM = TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                " inner join TSPL_ITEM_MASTER on a.Item_Code=TSPL_ITEM_MASTER.Item_Code " & _
                "left outer join tspl_sale_invoice_head on a.source_doc_no=tspl_sale_invoice_head.shipment_no " & _
                "WHERE b.Location_Type='Physical' " & strItemtype & " "

            Else
                StrQuery = "SELECT     " & _
                "a.Trans_Type, a.ItemType," & _
                "a.Item_Code, " & _
                "a.Item_Desc,cast('ITF code-'+ (tspl_item_master.itf_code)as varchar)as itf_code,  " & _
                "a.MRP, " & _
                " a.InOut, " & _
                " case when a.trans_type='Load Out' then sale_invoice_no else  a.Source_Doc_No end as Source_Doc_No,  " & _
                "a.Punching_Date, " & _
                "a.Entry_Date, " & _
                "case when a.Trans_Type='Transfer' and InOut='I' then  (a.Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * isnull((select Top 1 Item_Price from TSPL_TRANSFER_DETAIL   where TSPL_TRANSFER_DETAIL.Transfer_No=a.Source_Doc_No and TSPL_TRANSFER_DETAIL.Item_Code =a.Item_Code and TSPL_TRANSFER_DETAIL.Uom =a.UOM  and TSPL_TRANSFER_DETAIL.MRP=a.MRP  ),0)  else Net_Cost end as Net_Cost, " & _
                "a.Location_Code, " & _
                "b.Location_Desc, " & _
                " '' AS Fdate, " & _
                "'' AS Tdate, " & _
                "0 as OP, " & _
                "case when InOut='I' then convert(decimal(18,2),(a.Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor)) else 0 end as INQty, " & _
                "case when InOut='I' then 0 else convert(decimal(18,2),-1 * (a.Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor)) end as OutQty, " & _
                "0 as CB FROM TSPL_INVENTORY_MOVEMENT AS a INNER JOIN " & _
                " TSPL_LOCATION_MASTER AS b ON a.Location_Code = b.Location_Code INNER JOIN " & _
                " TSPL_ITEM_UOM_DETAIL ON a.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND a.UOM = TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                " inner join TSPL_ITEM_MASTER on a.Item_Code=TSPL_ITEM_MASTER.Item_Code " & _
                "left outer join tspl_sale_invoice_head on a.source_doc_no=tspl_sale_invoice_head.shipment_no " & _
                "WHERE b.Location_Type='Physical' " & strItemtype & " "

            End If
            If (chkItemSelect.IsChecked AndAlso cbgItem.CheckedValue.Count > 0) Then
                StrQuery += " and a.Item_Code IN  (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ")  "
            End If

            If (chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0) Then
                StrQuery += " and a.Location_Code IN  ( " + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")  "
            End If

            If (chkmrpselect.IsChecked AndAlso cbgmrp.CheckedValue.Count > 0) Then
                StrQuery += " and a.MRP IN  (" + clsCommon.GetMulcallString(cbgmrp.CheckedValue) + ")  "
            End If

            StrQuery += " and convert(date,a.Punching_Date,103) >=convert(date,'" + txtFromDate.Value + "',103) and convert(date,a.Punching_Date,103)<=convert(date,'" + txtToDate.Value + "',103)"

            If rdbDetail.IsChecked Then
                StrQuery += "  order by a.Item_Code  + a.Location_Code,a.Punching_Date + (case when InOut='I' then 1 else 2 end)   asc"
            ElseIf rdbSummary.IsChecked Then
                StrQuery += " order by CONVERT(date, Entry_Date,103) asc,CONVERT(date, Punching_Date ,103) asc"
            ElseIf rdbItemSummary.IsChecked Then
                StrQuery = "select Item_Code,Item_Desc,itf_code,convert(date,(Punching_Date),103) as Punching_Date,MRP,Location_Code,Location_Desc,sum(OP) as OP, " & _
                "sum(INQty) as INQty,sum(OutQty) as OutQty,sum(CB) as CB from( " & StrQuery & " ) aa  " & _
                "group by Item_Code,Item_Desc,itf_code,Location_Code,Location_Desc,MRP,convert(date,(Punching_Date),103),InOut order by Item_Code  + Location_Code,convert(date,aa.Punching_Date,103) , (case when InOut='I' then 1 else 2 end)   asc"


            End If

            Dim ArrDBName As ArrayList = Nothing
            Dim ArrOPBal As New Dictionary(Of String, Double)
            Dim dt, dt1 As New DataTable
            Dim dtFinal As DataTable

            If rdbDetail.IsChecked OrElse rdbItemSummary.IsChecked OrElse rdbSummary.IsChecked Then


                dt = clsDBFuncationality.GetDataTable(StrQuery)

                Dim strOpening As String = "select TSPL_INVENTORY_MOVEMENT.Location_Code,SUM(case when InOut='I' then Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else  - 1 * Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor end) as OP, " & _
                "TSPL_INVENTORY_MOVEMENT.Item_Code,max(TSPL_ITEM_MASTER .ITF_CODE) as Itf_Code from TSPL_INVENTORY_MOVEMENT left outer join TSPL_LOCATION_MASTER on " & _
                "TSPL_INVENTORY_MOVEMENT.Location_Code=TSPL_LOCATION_MASTER.Location_Code left outer join TSPL_ITEM_MASTER on " & _
                "TSPL_INVENTORY_MOVEMENT.Item_Code=TSPL_ITEM_MASTER.Item_Code left outer join " & _
                "TSPL_ITEM_UOM_DETAIL ON TSPL_INVENTORY_MOVEMENT.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND TSPL_INVENTORY_MOVEMENT.UOM = TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                " WHERE Location_Type='Physical' " & _
                " and convert(date,Punching_Date,103) < convert(date,'" + txtFromDate.Value + "',103)  " & strItemtype & "  group by TSPL_INVENTORY_MOVEMENT.Item_Code,TSPL_INVENTORY_MOVEMENT.Location_Code"

                dt1 = clsDBFuncationality.GetDataTable(strOpening)

                For Each dr As DataRow In dt1.Rows
                    ArrOPBal.Add(clsCommon.myCstr(dr("Item_Code")).ToUpper + clsCommon.myCstr(dr("Location_Code")).ToUpper, clsCommon.myCdbl(dr("OP")))
                Next
                dtFinal = dt.Clone()
                Dim strItemCode As String = ""
                Dim dblOPBal As Double = 0
                ' Dim strSalesname As String

                If dt.Rows.Count <> 0 Then
                    For Each dr As DataRow In dt.Rows
                        Dim drFianl As DataRow = dtFinal.NewRow()
                        If Not clsCommon.CompairString(strItemCode, (clsCommon.myCstr(dr("Item_Code")).ToUpper + clsCommon.myCstr(dr("Location_Code")).ToUpper)) = CompairStringResult.Equal Then
                            dblOPBal = 0
                            strItemCode = clsCommon.myCstr(dr("Item_Code")).ToUpper + clsCommon.myCstr(dr("Location_Code")).ToUpper
                            If ArrOPBal.ContainsKey(strItemCode) Then
                                dblOPBal = ArrOPBal(strItemCode)
                            End If
                        End If
                        If rdbDetail.IsChecked Then
                            drFianl("OP") = dblOPBal
                            drFianl("Item_Code") = dr("Item_Code")
                            drFianl("Item_Desc") = dr("Item_Desc")
                            drFianl("itf_code") = dr("itf_code")
                            drFianl("INQty") = dr("INQty")
                            drFianl("OutQty") = dr("OutQty")
                            drFianl("InOut") = dr("InOut")
                            drFianl("Source_Doc_No") = dr("Source_Doc_No")
                            drFianl("Punching_Date") = dr("Punching_Date")
                            drFianl("Entry_Date") = dr("Entry_Date")
                            drFianl("Net_Cost") = dr("Net_Cost")
                            drFianl("Location_Code") = dr("Location_Code")
                            drFianl("Trans_Type") = dr("Trans_Type")
                            drFianl("ItemType") = dr("ItemType")
                            drFianl("Fdate") = dr("Fdate")
                            drFianl("Tdate") = dr("Tdate")
                            drFianl("Location_Desc") = dr("Location_Desc")
                            drFianl("MRP") = dr("MRP")

                            dblOPBal += clsCommon.myCdbl(dr("InQty")) + clsCommon.myCdbl(dr("OutQty"))
                            drFianl("CB") = dblOPBal
                            dtFinal.Rows.Add(drFianl)
                        ElseIf rdbItemSummary.IsChecked Then

                            drFianl("OP") = dblOPBal
                            drFianl("Item_Code") = dr("Item_Code")
                            drFianl("Item_Desc") = dr("Item_Desc")
                            drFianl("itf_code") = dr("Itf_code")
                            drFianl("Punching_Date") = dr("Punching_Date")
                            drFianl("INQty") = dr("INQty")
                            drFianl("OutQty") = dr("OutQty")
                            drFianl("MRP") = dr("MRP")
                            drFianl("Location_Code") = dr("Location_Code")
                            drFianl("Location_Desc") = dr("Location_Desc")

                            dblOPBal += clsCommon.myCdbl(dr("InQty")) + clsCommon.myCdbl(dr("OutQty"))
                            drFianl("CB") = dblOPBal
                            dtFinal.Rows.Add(drFianl)
                        ElseIf rdbSummary.IsChecked Then



                            drFianl("Item_Code") = dr("Item_Code")
                            drFianl("Item_Desc") = dr("Item_Desc")
                            drFianl("itf_code") = dr("Itf_code")
                            drFianl("MRP") = dr("MRP")
                            drFianl("InQty") = dr("InQty")
                            drFianl("InCost") = dr("InCost")
                            drFianl("OutQty") = dr("OutQty")
                            drFianl("OutCost") = dr("OutCost")
                            drFianl("Source_Doc_No") = dr("Source_Doc_No")
                            drFianl("Punching_Date") = dr("Punching_Date")
                            drFianl("Location_Code") = dr("Location_Code")
                            'drFianl("Fdate") = dr("Fdate")
                            'drFianl("Tdate") = dr("Tdate")
                            drFianl("Location_Desc") = dr("Location_Desc")
                            drFianl("trans_type") = dr("trans_type")
                            drFianl("ItemType") = dr("ItemType")
                            dtFinal.Rows.Add(drFianl)
                        End If





                    Next

                   
                End If
                gv1.DataSource = Nothing
                gv1.Columns.Clear()
                gv1.Rows.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.EnableFiltering = True

                If dtFinal Is Nothing OrElse dtFinal.Rows.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                    Exit Sub
                Else
                    gv1.DataSource = dtFinal
                    SetGridFormationOFGV1()
                End If

                gv1.MasterTemplate.AllowAddNewRow = False
                RadPageView1.SelectedPage = RadPageViewPage2
            End If



            'If rdbSummary.IsChecked Then
            '    Dim frm As New frmInventoryReportViewer
            '    frm.funreport(StrQuery, "crptInventoryMovement", "Invenory Movement Report")
            '    'dtFinal = clsDBFuncationality.GetDataTable(StrQuery)
            '    'gv1.DataSource = Nothing
            '    'gv1.Columns.Clear()
            '    'gv1.Rows.Clear()
            '    'gv1.GroupDescriptors.Clear()
            '    'gv1.MasterTemplate.SummaryRowsBottom.Clear()
            '    'gv1.EnableFiltering = True
            '    'RadPageView1.SelectedPage = RadPageViewPage2
            '    'If dtFinal Is Nothing OrElse dtFinal.Rows.Count <= 0 Then
            '    '    common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
            '    '    Exit Sub
            '    'Else
            '    '    gv1.DataSource = dtFinal
            '    '    'SetGridFormationOFGV1()
            '    'End If
            'Else
            'End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub SetGridFormationOFGV1()
        '  Dim strItemCode, head2 As String

        GV1.TableElement.TableHeaderHeight = 40
        GV1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To GV1.Columns.Count - 1
            GV1.Columns(ii).ReadOnly = True
            GV1.Columns(ii).IsVisible = False
        Next

        If rdbDetail.IsChecked = True Then
            gv1.Columns("OP").IsVisible = True
            gv1.Columns("OP").Width = 50
            gv1.Columns("OP").HeaderText = "Opening Balance"

            gv1.Columns("Item_Code").IsVisible = True
            gv1.Columns("Item_Code").Width = 70
            gv1.Columns("Item_Code").HeaderText = "Item Code"

            gv1.Columns("Item_Desc").IsVisible = True
            gv1.Columns("Item_Desc").Width = 100
            gv1.Columns("Item_Desc").HeaderText = "Item Desc"

            gv1.Columns("itf_code").IsVisible = True
            gv1.Columns("itf_code").Width = 100
            gv1.Columns("itf_code").HeaderText = "ITF Code"

            gv1.Columns("INQty").IsVisible = True
            gv1.Columns("INQty").Width = 100
            gv1.Columns("INQty").HeaderText = "In"

            gv1.Columns("OutQty").IsVisible = True
            gv1.Columns("OutQty").Width = 100
            gv1.Columns("OutQty").HeaderText = "Out"

            gv1.Columns("Source_Doc_No").IsVisible = True
            gv1.Columns("Source_Doc_No").Width = 120
            gv1.Columns("Source_Doc_No").HeaderText = "Doc No"

            gv1.Columns("Trans_Type").IsVisible = True
            gv1.Columns("Trans_Type").Width = 80
            gv1.Columns("Trans_Type").HeaderText = "Trans Type"

            gv1.Columns("Punching_Date").IsVisible = True
            gv1.Columns("Punching_Date").Width = 80
            gv1.Columns("Punching_Date").HeaderText = "Date"

            gv1.Columns("Entry_Date").IsVisible = False
            gv1.Columns("Entry_Date").Width = 80
            gv1.Columns("Entry_Date").HeaderText = "Entry_Date"

            gv1.Columns("Net_Cost").IsVisible = False
            gv1.Columns("Net_Cost").Width = 80
            gv1.Columns("Net_Cost").HeaderText = "Net_Cost"

            gv1.Columns("Location_Code").IsVisible = True
            gv1.Columns("Location_Code").Width = 80
            gv1.Columns("Location_Code").HeaderText = "Location Code"

            gv1.Columns("Location_Desc").IsVisible = True
            gv1.Columns("Location_Desc").Width = 80
            gv1.Columns("Location_Desc").HeaderText = "Location Desc"

            gv1.Columns("MRP").IsVisible = True
            gv1.Columns("MRP").Width = 50
            gv1.Columns("MRP").HeaderText = "MRP"

            gv1.Columns("CB").IsVisible = True
            gv1.Columns("CB").Width = 80
            gv1.Columns("CB").HeaderText = "Closing Balance"

            gv1.Columns("ItemType").IsVisible = False
            gv1.Columns("ItemType").Width = 80
            gv1.Columns("ItemType").HeaderText = "Item Type"
        ElseIf rdbItemSummary.IsChecked Then
            gv1.Columns("OP").IsVisible = True
            gv1.Columns("OP").Width = 50
            gv1.Columns("OP").HeaderText = "Opening Balance"

            gv1.Columns("Item_Code").IsVisible = True
            gv1.Columns("Item_Code").Width = 70
            gv1.Columns("Item_Code").HeaderText = "Item Code"

            gv1.Columns("Item_Desc").IsVisible = True
            gv1.Columns("Item_Desc").Width = 100
            gv1.Columns("Item_Desc").HeaderText = "Item Desc"

            gv1.Columns("itf_code").IsVisible = True
            gv1.Columns("itf_code").Width = 100
            gv1.Columns("itf_code").HeaderText = "ITF Code"

            gv1.Columns("INQty").IsVisible = True
            gv1.Columns("INQty").Width = 100
            gv1.Columns("INQty").HeaderText = "In"

            gv1.Columns("OutQty").IsVisible = True
            gv1.Columns("OutQty").Width = 100
            gv1.Columns("OutQty").HeaderText = "Out"

           
            gv1.Columns("Punching_Date").IsVisible = True
            gv1.Columns("Punching_Date").Width = 80
            gv1.Columns("Punching_Date").HeaderText = "Date"


            gv1.Columns("Location_Code").IsVisible = True
            gv1.Columns("Location_Code").Width = 80
            gv1.Columns("Location_Code").HeaderText = "Location Code"

            gv1.Columns("Location_Desc").IsVisible = True
            gv1.Columns("Location_Desc").Width = 80
            gv1.Columns("Location_Desc").HeaderText = "Location Desc"

            gv1.Columns("MRP").IsVisible = True
            gv1.Columns("MRP").Width = 50
            gv1.Columns("MRP").HeaderText = "MRP"

            gv1.Columns("CB").IsVisible = True
            gv1.Columns("CB").Width = 80
            gv1.Columns("CB").HeaderText = "Closing Balance"


        ElseIf rdbSummary.IsChecked Then
           

            gv1.Columns("Item_Code").IsVisible = True
            gv1.Columns("Item_Code").Width = 70
            gv1.Columns("Item_Code").HeaderText = "Item Code"

            gv1.Columns("Item_Desc").IsVisible = True
            gv1.Columns("Item_Desc").Width = 100
            gv1.Columns("Item_Desc").HeaderText = "Item Desc"

            gv1.Columns("itf_code").IsVisible = True
            gv1.Columns("itf_code").Width = 100
            gv1.Columns("itf_code").HeaderText = "ITF Code"

            gv1.Columns("MRP").IsVisible = True
            gv1.Columns("MRP").Width = 50
            gv1.Columns("MRP").HeaderText = "MRP"

            gv1.Columns("InQty").IsVisible = True
            gv1.Columns("InQty").Width = 50
            gv1.Columns("InQty").HeaderText = "In Qty"

            gv1.Columns("InCost").IsVisible = True
            gv1.Columns("InCost").Width = 50
            gv1.Columns("InCost").HeaderText = "In Cost"

            gv1.Columns("OutQty").IsVisible = True
            gv1.Columns("OutQty").Width = 50
            gv1.Columns("OutQty").HeaderText = "Out Qty"

            gv1.Columns("OutCost").IsVisible = True
            gv1.Columns("OutCost").Width = 50
            gv1.Columns("OutCost").HeaderText = "Out Cost"

            gv1.Columns("Source_Doc_No").IsVisible = True
            gv1.Columns("Source_Doc_No").Width = 120
            gv1.Columns("Source_Doc_No").HeaderText = "Doc No"

            gv1.Columns("Punching_Date").IsVisible = True
            gv1.Columns("Punching_Date").Width = 80
            gv1.Columns("Punching_Date").HeaderText = "Date"


            gv1.Columns("Location_Code").IsVisible = True
            gv1.Columns("Location_Code").Width = 80
            gv1.Columns("Location_Code").HeaderText = "Location Code"

            gv1.Columns("Location_Desc").IsVisible = True
            gv1.Columns("Location_Desc").Width = 80
            gv1.Columns("Location_Desc").HeaderText = "Location Desc"

            gv1.Columns("ItemType").IsVisible = False
            gv1.Columns("ItemType").Width = 80
            gv1.Columns("ItemType").HeaderText = "Item Type"

            gv1.Columns("trans_type").IsVisible = False
            gv1.Columns("trans_type").Width = 80
            gv1.Columns("trans_type").HeaderText = "trans_type"
        End If

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item6 As New GridViewSummaryItem("CB", "{0:F2}", GridAggregateFunction.Last)
        summaryRowItem.Add(item6)
        Dim item7 As New GridViewSummaryItem("OP", "{0:F2}", GridAggregateFunction.First)
        summaryRowItem.Add(item7)
        Dim item8 As New GridViewSummaryItem("INQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)
        Dim item9 As New GridViewSummaryItem("OutQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item9)

        gv1.GroupDescriptors.Add(New GridGroupByExpression("Item_Code as Item format ""{0}: {1}"" Group By Item_Code"))
        gv1.GroupDescriptors.Add(New GridGroupByExpression("Item_Desc as Item format ""{0}: {1}"" Group By Item_Desc"))

        gv1.GroupDescriptors.Add(New GridGroupByExpression("Location_Code as Location format ""{0}: {1}"" Group By Location_Code"))

        gv1.ShowGroupPanel = False
        gv1.MasterTemplate.AutoExpandGroups = True

        GV1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Sub Reset()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        'txtItem.txtValue.Text = ""
        'lblItem.Text = ""
        chkLocAll.IsChecked = True
        chkIemAll.IsChecked = True
        rdbFinish.IsChecked = True
        chkmrpall.IsChecked = True
        rdbSummary.IsChecked = True
        gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

   

    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        cbgLocation.Enabled = False
    End Sub

    Private Sub chkLocSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        cbgLocation.Enabled = True
    End Sub

    Private Sub chkIemAll_ToggleStateChanged_1(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        cbgItem.Enabled = False
    End Sub

    Private Sub chkItemSelect_ToggleStateChanged_1(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        cbgItem.Enabled = True
    End Sub

    Private Sub frmRptInventoryMovement_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Control And e.KeyCode = Keys.P Then
            PrintData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            Reset()
        End If
    End Sub


    Private Sub cbgLocation_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        LoadMrp()
    End Sub

    Private Sub cbgItem_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        LoadMrp()
    End Sub
    Private Sub chkmrpselect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        cbgmrp.Enabled = True
    End Sub
    Private Sub chkmrpall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        cbgmrp.Enabled = False
    End Sub

  
    Private Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click

    End Sub

    Private Sub Export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export.Click
        If gv1.Rows.Count > 0 Then
            ExporttoExcel(EnumExportTo.Excel)
        Else
            RadMessageBox.Show("No Data Found to Display", Me.Text)
        End If
    End Sub

    Private Sub PDF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PDF.Click
        If gv1.Rows.Count > 0 Then
            ExporttoExcel(EnumExportTo.PDF)
        Else
            RadMessageBox.Show("No Data Found to Display", Me.Text)
        End If
    End Sub

    Private Sub ExportToExcel(ByVal exporter As EnumExportTo)
        Try


            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)

            If chkItemSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgItem.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Item : " + strtemp)
            End If

            If chkLocSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgLocation.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Location : " + strtemp)
            End If



            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcel("Inventory Movement Report", gv1, arrHeader, Me.Text)

            Else
                clsCommon.MyExportToPDF("Inventory Movement Report", gv1, arrHeader, "Inventory Movement Report ", True)
            End If


        Catch ex As Exception
            RadMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub chkIemAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkIemAll.ToggleStateChanged
        cbgItem.Enabled = Not chkIemAll.IsChecked
    End Sub

    Private Sub chkmrpall_ToggleStateChanged1(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkmrpall.ToggleStateChanged
        cbgmrp.Enabled = Not chkmrpall.IsChecked

    End Sub

    Private Sub chkLocAll_ToggleStateChanged1(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocAll.IsChecked

    End Sub


    Private Sub gv1_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellDoubleClick
        If rdbSummary.IsChecked Or rdbDetail.IsChecked Then


            If clsCommon.CompairString(gv1.CurrentRow.Cells("trans_type").Value, "IC-AD") = CompairStringResult.Equal AndAlso clsCommon.CompairString(gv1.CurrentRow.Cells("ItemType").Value, "FM") = CompairStringResult.Equal Then
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnProductionEntry, gv1.CurrentRow.Cells("Source_Doc_No").Value)

            ElseIf clsCommon.CompairString(gv1.CurrentRow.Cells("trans_type").Value, "IC-AD") = CompairStringResult.Equal AndAlso clsCommon.CompairString(gv1.CurrentRow.Cells("ItemType").Value, "FT") = CompairStringResult.Equal Then
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnProductionEntry, gv1.CurrentRow.Cells("Source_Doc_No").Value)

            ElseIf clsCommon.CompairString(gv1.CurrentRow.Cells("trans_type").Value, "IC-AD") = CompairStringResult.Equal AndAlso clsCommon.CompairString(gv1.CurrentRow.Cells("ItemType").Value, "") = CompairStringResult.Equal Then
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnStoreAdjustment, gv1.CurrentRow.Cells("Source_Doc_No").Value)

            ElseIf clsCommon.CompairString(gv1.CurrentRow.Cells("trans_type").Value, "SRN") = CompairStringResult.Equal Then
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnSRN, gv1.CurrentRow.Cells("Source_Doc_No").Value)

            ElseIf clsCommon.CompairString(gv1.CurrentRow.Cells("trans_type").Value, "SD-SH") = CompairStringResult.Equal Then
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmShipmentProductSale, gv1.CurrentRow.Cells("Source_Doc_No").Value)

            ElseIf clsCommon.CompairString(gv1.CurrentRow.Cells("trans_type").Value, "Sale Return") = CompairStringResult.Equal Then
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.saleReturn, gv1.CurrentRow.Cells("Source_Doc_No").Value)

            ElseIf clsCommon.CompairString(gv1.CurrentRow.Cells("trans_type").Value, "ExpiredItem") = CompairStringResult.Equal Then
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmExpiryDateEntry, gv1.CurrentRow.Cells("Source_Doc_No").Value)

            ElseIf clsCommon.CompairString(gv1.CurrentRow.Cells("trans_type").Value, "Transfer") = CompairStringResult.Equal Then
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.Transfer, gv1.CurrentRow.Cells("Source_Doc_No").Value)
            End If
        End If
    End Sub
End Class
