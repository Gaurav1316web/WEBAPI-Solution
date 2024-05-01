'BM00000003734-================created by Monika 01/09/2014=====================
Imports common
Imports System.Data.SqlClient

Public Class frmProductionSerializedReport
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ButtonToolTip As New ToolTip()
#End Region

    Private Sub FunReset()
        txtfrmdate.Text = clsCommon.GETSERVERDATE(Nothing)
        txttodate.Text = clsCommon.GETSERVERDATE(Nothing)
        chkitemAll.IsChecked = True
        cbgItem.Enabled = False

        chklocAll.IsChecked = True
        cbgLocation.Enabled = False

        chkProd_item.Checked = False
        chkTreeVew.Checked = False
        gv.Columns.Clear()
        gv.DataSource = Nothing

        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmProductionSerializedReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnexport.Visible = MyBase.isExport
        btnprint.Visible = MyBase.isPrintFlag
    End Sub

    Private Sub frmProductionSerializedReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnprint.Enabled Then
            btnprint.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            FunReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub

    Private Sub frmProductionSerializedReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        FunReset()
        LoadItems()
        LoadLocation()

        ButtonToolTip.SetToolTip(btnprint, "Press Alt+P for print report")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+N for refresh window")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C for close window")
    End Sub

    Private Sub LoadLocation()
        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER where Location_Type='Physical' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        cbgLocation.DataSource = Nothing
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            cbgLocation.DataSource = dt

            cbgLocation.DisplayMember = "Name"
            cbgLocation.ValueMember = "Code"
        End If

    End Sub

    Private Sub LoadItems()
        Try
            Dim qry As String = "select TSPL_MF_PRINCIPLE_RECEIPT_DETAIL.Main_Item_Code as [Code],TSPL_ITEM_MASTER.Item_Desc as [Description] from TSPL_MF_PRINCIPLE_RECEIPT_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_MF_PRINCIPLE_RECEIPT_DETAIL.Main_Item_Code where TSPL_MF_PRINCIPLE_RECEIPT_DETAIL.Doc_No in (select Doc_No from TSPL_MF_PRINCIPLE_RECEIPT_HEAD where (Doc_Date between '" + clsCommon.GetPrintDate(txtfrmdate.Text, "dd/MMM/yyyy") + "' and '" + clsCommon.GetPrintDate(txttodate.Text, "dd/MMM/yyyy") + "'))"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            cbgItem.DataSource = Nothing
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                cbgItem.DataSource = dt
                cbgItem.DisplayMember = "Description"
                cbgItem.ValueMember = "Code"
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        PrintReport()
    End Sub

    Private Sub PrintReport()
        Try
            If chkSelect.IsChecked AndAlso cbgItem.CheckedValue.Count <= 0 Then
                cbgItem.Focus()
                cbgItem.Select()
                Throw New Exception("Select atleast one item.")
            End If

            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                cbgLocation.Focus()
                cbgLocation.Select()
                Throw New Exception("Select atleast one location.")
            End If

            Dim qry As String = "select TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.Main_Item_Code as [Production Item],(TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.Main_Item_Code+' & '+TSPL_ITEM_MASTER.Item_Desc) as ProductionItem,TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.Item_Code as [Item Code],(TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.Item_Code+' '+item.Item_Desc) as Description,TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.Unit_Code as [Unit],TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.Serial_No as [Serial No],(case when TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.Is_principle='1' then 'Principle' else '' end) as [Principle] from TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL "
            qry += "left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.Main_Item_Code left outer join TSPL_ITEM_MASTER as item on item.Item_Code=TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.Item_Code "
            qry += "where 2=2 and TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.doc_no in (select Doc_No from TSPL_MF_PRINCIPLE_RECEIPT_HEAD where (Doc_Date between '" + clsCommon.GetPrintDate(txtfrmdate.Text, "dd/MMM/yyyy") + "' and '" + clsCommon.GetPrintDate(txttodate.Text, "dd/MMM/yyyy") + "'))"
            If chkSelect.IsChecked Then
                qry += " and TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.Main_Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ")"
            End If

            If chkLocSelect.IsChecked Then
                qry += " and TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.location_code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            gv.DataSource = Nothing
            gv.Columns.Clear()
            gv.Rows.Clear()
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 AndAlso chkTreeVew.Checked = False Then
                gv.DataSource = dt

                FormatGrid()
                RadPageView1.SelectedPage = RadPageViewPage2
            ElseIf dt IsNot Nothing AndAlso dt.Rows.Count > 0 AndAlso chkTreeVew.Checked Then
                gv_tree.RightToLeft = System.Windows.Forms.RightToLeft.No
                BindTreeView(dt, True)
                RadPageView1.SelectedPage = RadPageViewPage3
            Else
                Throw New Exception("No Data Found.")
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function Searchnode(ByVal nodetext As String) As RadTreeNode
        For Each node As RadTreeNode In gv_tree.Nodes
            If node.Text = nodetext Then
                Return node
            End If
        Next
        Return Nothing
    End Function

    Private Sub BindTreeView(ByVal dt As DataTable, ByVal expandall As Boolean)
        gv_tree.Nodes.Clear()
        Dim node As RadTreeNode
        Dim subNode As RadTreeNode
        ' Dim subsubnode As RadTreeNode
        For Each row As DataRow In dt.Rows
            'search in the treeview if any country is already present
            node = Searchnode(row.Item(0).ToString())
            If node IsNot Nothing Then
                'Prduction Item is already present
                subNode = New RadTreeNode(row.Item(2).ToString() + " With Serial No.:- " + row.Item(5).ToString())
                'Add Items to country
                node.Nodes.Add(subNode)
            Else
                node = New RadTreeNode(row.Item(0).ToString())
                subNode = New RadTreeNode(row.Item(2).ToString() + " With Serial No.:- " + row.Item(5).ToString())
                'Add Items to country
                node.Nodes.Add(subNode)
                gv_tree.Nodes.Add(node)
            End If
            '===================================
            'subNode = Searchnode(row.Item(1).ToString())
            'If subNode IsNot Nothing Then
            '    'Prduction Item is already present
            '    subsubnode = New RadTreeNode(row.Item(2).ToString())
            '    'Add Items to country
            '    subNode.Nodes.Add(subsubnode)
            'Else
            '    subNode = New RadTreeNode(row.Item(1).ToString())
            '    subsubnode = New RadTreeNode(row.Item(2).ToString())
            '    'Add Items to country
            '    subNode.Nodes.Add(subsubnode)
            '    node.Nodes.Add(subNode)
            '    gv_tree.Nodes.Add(subNode)
            'End If
        Next
        If expandall Then
            ' Expand the TreeView
            gv_tree.ExpandAll()
        End If
    End Sub

    Private Sub FormatGrid()
        'Dim strItemCode, head2 As String

        gv.TableElement.TableHeaderHeight = 40
        gv.MasterTemplate.ShowRowHeaderColumn = False
        gv.AllowAddNewRow = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = True
            gv.Columns(ii).Width = 100
        Next
        
        gv.Columns(1).Width = 250
        gv.Columns(3).Width = 250

        gv.GroupDescriptors.Clear()
        If chkProd_item.Checked Then
            gv.Columns(0).IsVisible = False
            gv.Columns(2).IsVisible = False
            gv.GroupDescriptors.Add(New GridGroupByExpression("ProductionItem as ProductionItem format ""{0}: {1}"" Group By ProductionItem"))
            gv.GroupDescriptors.Add(New GridGroupByExpression("Description as Description format ""{0}: {1}"" Group By Description"))
            gv.MasterTemplate.ExpandAllGroups()
            gv.AutoExpandGroups = True
        End If
    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        FunReset()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnexcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexcel.Click
        If gv.Rows.Count <= 0 Then
            PrintReport()
        End If
        Dim arrHeader As List(Of String) = New List(Of String)()
        arrHeader.Add("Serialized Report")
        clsCommon.MyExportToExcelGrid("Item Serialized Report", gv, arrHeader, "Serialized Report")
    End Sub

    Private Sub btnpdf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpdf.Click
        If gv.Rows.Count <= 0 Then
            PrintReport()
        End If
        Dim arrHeader As List(Of String) = New List(Of String)()
        arrHeader.Add("Serialized Report")
        clsCommon.MyExportToPDF("Item Serialized Report", gv, arrHeader, "Serialized Report")
    End Sub

    Private Sub chkitemAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkitemAll.ToggleStateChanged, chkSelect.ToggleStateChanged
        cbgItem.Enabled = chkSelect.IsChecked
    End Sub

    Private Sub txtfrmdate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtfrmdate.ValueChanged
        Try
            LoadItems()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txttodate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txttodate.ValueChanged
        Try
            LoadItems()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub chklocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chklocAll.ToggleStateChanged, chkLocSelect.ToggleStateChanged
        cbgLocation.Enabled = chkLocSelect.IsChecked
    End Sub

    Private Sub chkTreeVew_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkTreeVew.ToggleStateChanged, chkProd_item.ToggleStateChanged
        If chkTreeVew.Checked = True Then
            chkProd_item.Checked = False
        End If
    End Sub
End Class