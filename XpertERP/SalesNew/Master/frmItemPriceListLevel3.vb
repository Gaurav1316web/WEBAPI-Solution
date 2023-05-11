Imports common
Imports System.Data.SqlClient

Public Class FrmItemPriceListLevel3
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isCellValueChangedOpen As Boolean = False
    Private isInsideLoadData As Boolean = False
    Const colitemno As String = "Item No"
    Const coldesc As String = "Description"
    Const colMRP As String = "MRP"
    Const colDiscPercentage As String = "Discount"
    Const colNetAmt As String = "Net Amount"
    Const colBuffer As String = "Buffer"
#End Region

    Private Sub frmVendorItemDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadBlankGrid()
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclear, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D to Delete")
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S to Save")
        LoadData()
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Sub SaveData()
        Try
            If (AllowToSave()) Then
                Dim Arr As New List(Of clsItemPriceListLevel3)
                For Each grow As GridViewRowInfo In dgvitem.Rows
                    Dim objTr As New clsItemPriceListLevel3()
                    objTr.item_code = clsCommon.myCstr(grow.Cells(colitemno).Value)
                    objTr.MRP = clsCommon.myCdbl(grow.Cells(colMRP).Value)
                    objTr.Discount_Per = clsCommon.myCdbl(grow.Cells(colDiscPercentage).Value)
                    objTr.Net_Amount = objTr.MRP * ((100 - objTr.Discount_Per) / 100) ''clsCommon.myCdbl(grow.Cells(colMinRate).Value)
                    objTr.Buffer_Amt = clsCommon.myCdbl(grow.Cells(colBuffer).Value)
                    If (clsCommon.myLen(objTr.item_code) > 0) Then
                        Arr.Add(objTr)
                    End If
                Next

                If (Arr Is Nothing OrElse Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow("Please Fill at least one Item")
                    Return
                End If
                If clsItemPriceListLevel3.SaveData(Arr) Then
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    LoadData()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Function AllowToSave() As Boolean
        Try
            For ii As Integer = 0 To dgvitem.Rows.Count - 1
                Dim strICode As String = clsCommon.myCstr(dgvitem.Rows(ii).Cells(colitemno).Value)
                Dim strIName As String = clsCommon.myCstr(dgvitem.Rows(ii).Cells(coldesc).Value)

                For jj As Integer = 0 To dgvitem.Rows.Count - 1
                    If (ii = jj) Then
                        Continue For
                    End If
                    If (clsCommon.CompairString(strICode, clsCommon.myCstr(dgvitem.Rows(jj).Cells(colitemno).Value)) = CompairStringResult.Equal) And clsCommon.myLen(strICode) > 0 Then
                        common.clsCommon.MyMessageBoxShow("Already selected Item " + strICode.Trim() + "( " + strIName.Trim() + " ) At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " and  " + clsCommon.myCstr(clsCommon.myCdbl(jj + 1)) + "")
                        Return False
                    End If
                Next
            Next
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
        Return True
    End Function

    Sub LoadData()
        LoadBlankGrid()
        Try
            Dim Arr As List(Of clsItemPriceListLevel3) = clsItemPriceListLevel3.GetData()
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each objTr As clsItemPriceListLevel3 In Arr
                    dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(colitemno).Value = objTr.item_code
                    dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(colMRP).Value = objTr.MRP
                    dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(colDiscPercentage).Value = objTr.Discount_Per
                    dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(colNetAmt).Value = objTr.Net_Amount
                    dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(colBuffer).Value = objTr.Buffer_Amt
                    dgvitem.Rows.AddNew()
                Next
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isInsideLoadData = False
        End Try
    End Sub


    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        delete()
    End Sub

    Public Sub delete()
        Try
            If myMessages.deleteConfirm() Then
                If clsItemPriceListLevel3.DeleteData() Then
                    clsCommon.MyMessageBoxShow("Records deleted successfully.")
                End If
                LoadData()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub


    Private Sub dgvitem_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles dgvitem.CellValueChanged
        Try
            If isInsideLoadData = False Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is dgvitem.Columns(colitemno) Then
                        OpenICodeList(False)
                    End If
                    If e.Column Is dgvitem.Columns(colMRP) Or e.Column Is dgvitem.Columns(colDiscPercentage) Then
                        If clsCommon.myCdbl(dgvitem.CurrentRow.Cells(colMRP)) >= 0 Then
                            dgvitem.CurrentRow.Cells(colNetAmt).Value = clsCommon.myCdbl(dgvitem.CurrentRow.Cells(colMRP).Value) * (100 - clsCommon.myCdbl(dgvitem.CurrentRow.Cells(colDiscPercentage).Value)) / 100
                        Else
                            dgvitem.CurrentRow.Cells(colNetAmt).Value = 0
                        End If
                    End If
                    If e.Column Is dgvitem.Columns(colDiscPercentage) Then
                        If clsCommon.myCdbl(dgvitem.CurrentRow.Cells(colDiscPercentage).Value) > 0 Then

                        End If
                    End If
                    isCellValueChangedOpen = False
                End If
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally

        End Try
    End Sub


    Sub OpenICodeList(ByVal isButtonClick As Boolean)

        Dim obj As clsItemMaster = clsItemMaster.FinderForItem(clsCommon.myCstr(dgvitem.CurrentRow.Cells(colitemno).Value), "O", isButtonClick)
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
            dgvitem.CurrentRow.Cells(colitemno).Value = obj.Item_Code
            dgvitem.CurrentRow.Cells(coldesc).Value = obj.Item_Desc
        Else
            dgvitem.CurrentRow.Cells(colitemno).Value = ""
            dgvitem.CurrentRow.Cells(coldesc).Value = ""
            dgvitem.CurrentRow.Cells(colMRP).Value = Nothing
            dgvitem.CurrentRow.Cells(colDiscPercentage).Value = Nothing
            dgvitem.CurrentRow.Cells(colDiscPercentage).Value = Nothing
        End If

    End Sub

    Sub LoadBlankGrid()

        dgvitem.AddNewRowPosition = SystemRowPosition.Bottom
        dgvitem.Rows.Clear()
        dgvitem.Columns.Clear()

        Dim item_code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        item_code.FormatString = ""
        item_code.HeaderText = "Item No"
        item_code.Name = colitemno
        item_code.Width = 150
        item_code.ReadOnly = False
        item_code.TextImageRelation = TextImageRelation.TextBeforeImage
        item_code.HeaderImage = Global.ERP.My.Resources.Resources.search4
        item_code.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        dgvitem.MasterTemplate.Columns.Add(item_code)

        Dim item_desc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        item_desc.FormatString = ""
        item_desc.HeaderText = "Description"
        item_desc.Name = coldesc
        item_desc.Width = 300
        item_desc.ReadOnly = True
        item_desc.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        dgvitem.MasterTemplate.Columns.Add(item_desc)

        Dim mrp As GridViewDecimalColumn = New GridViewDecimalColumn()
        mrp.FormatString = ""
        mrp.HeaderText = "MRP"
        mrp.Name = colMRP
        mrp.Width = 120
        mrp.ReadOnly = False
        dgvitem.MasterTemplate.Columns.Add(mrp)

        Dim repoDiscountPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDiscountPer.FormatString = ""
        repoDiscountPer.HeaderText = "Discount %"
        repoDiscountPer.Name = colDiscPercentage
        repoDiscountPer.Width = 100
        repoDiscountPer.ReadOnly = False
        repoDiscountPer.Step = 0
        dgvitem.MasterTemplate.Columns.Add(repoDiscountPer)

        Dim NetAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        NetAmt.FormatString = ""
        NetAmt.HeaderText = "Net Amount"
        NetAmt.Name = colNetAmt
        NetAmt.Width = 150
        NetAmt.ReadOnly = True
        NetAmt.ShowUpDownButtons = False
        dgvitem.MasterTemplate.Columns.Add(NetAmt)

        Dim Buffer As GridViewDecimalColumn = New GridViewDecimalColumn()
        Buffer.FormatString = ""
        Buffer.HeaderText = "Buffer"
        Buffer.Name = colBuffer
        Buffer.Width = 150
        Buffer.ReadOnly = True
        Buffer.ShowUpDownButtons = False
        dgvitem.MasterTemplate.Columns.Add(Buffer)

        dgvitem.AllowDeleteRow = True
        dgvitem.AllowAddNewRow = False
        dgvitem.ShowGroupPanel = False
        dgvitem.AllowColumnReorder = False
        dgvitem.AllowRowReorder = False
        dgvitem.EnableSorting = False
        dgvitem.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        dgvitem.MasterTemplate.ShowRowHeaderColumn = False

        dgvitem.Rows.AddNew()
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.VendorItemDetails)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
        End If
        btnsave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 03/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnsave.Visible = True Then
            rmiImport.Enabled = True
            rmiExport.Enabled = True
        Else
            rmiImport.Enabled = False
            rmiExport.Enabled = False
        End If
        '--------------------------------------------------
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        Me.Close()
    End Sub

    Private Sub rmiExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiExport.Click
        Dim str As String
        str = "select vendor_code as [Vendor Code],vendor_desc as [Vendor Description] ,item_no as [Item No],item_desc as [Item Description],uom as [UOM],MRP as [MRP],item_rate as [Rate],vendor_item_no as [Vendor Item No], REPLACE( Convert(varchar(11) ,Start_Date,102),'.','-') as [Start Date], REPLACE( Convert(varchar(11) ,End_Date,102),'.','-') as [End Date] from TSPL_VENDOR_ITEM_DETAIL  "
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub rmiImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Vendor Code", "Vendor Description", "Item No", "Item Description", "UOM", "MRP", "Rate", "Vendor Item No", "Start Date", "End Date") Then
            Dim trans As SqlTransaction = Nothing
            Try
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim vencode As String = clsCommon.myCstr(grow.Cells(0).Value)
                    If vencode.Length > 50 Then
                        Throw New Exception("Check the length of 'Vendor Code'.")
                        trans.Rollback()
                        Exit Sub
                    End If

                    Dim acc1 As String = clsDBFuncationality.getSingleValue("select count(*) from TSPL_VENDOR_MASTER where vendor_code='" + vencode + "'", trans)
                    If acc1 Is Nothing OrElse clsCommon.myLen(acc1) = 0 Then
                        Throw New Exception("This  '" + vencode + "'  Vendor does not exist")
                        trans.Rollback()
                        Exit Sub
                    End If


                    Dim vendesc As String = clsCommon.myCstr(grow.Cells(1).Value)
                    Dim itemno As String = clsCommon.myCstr(grow.Cells(2).Value)
                    If itemno.Length > 50 Then
                        Throw New Exception("Check the length of 'Item Code'.")
                        trans.Rollback()
                        Exit Sub
                    End If


                    Dim acc2 As String = clsDBFuncationality.getSingleValue("select count(*) from TSPL_ITEM_MASTER where item_code='" + itemno + "'", trans)
                    If acc2 Is Nothing OrElse clsCommon.myLen(acc2) = 0 Then
                        Throw New Exception("This  '" + itemno + "'  Item does not exist")
                        trans.Rollback()
                        Exit Sub
                    End If

                    Dim qryDesc As String = "select Item_Desc from TSPL_ITEM_MASTER where Item_Code='" + itemno + "' "
                    Dim itemdesc As String = clsDBFuncationality.getSingleValue(qryDesc, trans)
                    'Dim itemdesc As String = clsCommon.myCstr(grow.Cells(3).Value)
                    If itemdesc.Length > 100 Then
                        Throw New Exception("Check the length of 'Item Description' In Item Master.")
                        trans.Rollback()
                        Exit Sub
                    End If
                    Dim uom As String = clsCommon.myCstr(grow.Cells(4).Value)
                    Dim mrp As String = clsCommon.myCstr(grow.Cells(5).Value)
                    If mrp.Length < 18 And IsNumeric(mrp) Then
                    Else
                        Throw New Exception("Check the value of 'Item MRP'.")
                        trans.Rollback()
                        Exit Sub
                    End If
                    Dim rate As String = clsCommon.myCstr(grow.Cells(6).Value)
                    If rate.Length < 18 And IsNumeric(rate) Then
                    Else
                        Throw New Exception("Check the value of 'Item Rate'.")
                        trans.Rollback()
                        Exit Sub
                    End If
                    Dim venitemno As String = clsCommon.myCstr(grow.Cells(7).Value)
                    If venitemno.Length > 50 Then
                        Throw New Exception("Check the length of 'Vendor Item No'.")
                        trans.Rollback()
                        Exit Sub
                    End If

                    Dim StrstartDate As String = Nothing
                    If (grow.Cells(8).Value IsNot DBNull.Value AndAlso clsCommon.myLen(grow.Cells(8).Value) > 0 And clsCommon.myLen(grow.Cells(8).Value) < 11) Then
                        StrstartDate = clsCommon.GetPrintDate((grow.Cells(8).Value), "dd-MM-yyyy")
                        ''Else
                        ''    Throw New Exception("Please insert Date in Format- i.e. (yyyy-MM-dd)")
                    End If

                    Dim StrEndDate As String = Nothing
                    If (grow.Cells(9).Value IsNot DBNull.Value AndAlso clsCommon.myLen(grow.Cells(9).Value) > 0 And clsCommon.myLen(grow.Cells(9).Value) < 11) Then
                        StrEndDate = clsCommon.GetPrintDate((grow.Cells(9).Value), "dd-MM-yyyy")
                        ''Else
                        ''    Throw New Exception("Please insert Date in Format- i.e. (yyyy-mm-dd)")
                    End If

                    Dim sql1 As String = "select count(*) from TSPL_VENDOR_ITEM_DETAIL where vendor_code='" + vencode + "' and item_no='" + itemno + "' "
                    Dim i As Integer = CInt(clsDBFuncationality.getSingleValue(sql1, trans))

                    If (i = 0) Then
                        Dim qry As String = "insert into TSPL_VENDOR_ITEM_DETAIL( vendor_code ,vendor_desc  ,item_no ,item_desc ,uom ,MRP ,item_rate ,vendor_item_no ,comp_code, Start_Date, End_Date ) values('" + Convert.ToString(vencode) + "','" + Convert.ToString(vendesc) + "','" + Convert.ToString(itemno) + "','" + Convert.ToString(itemdesc) + "','" + Convert.ToString(uom) + "','" + Convert.ToString(mrp) + "','" + Convert.ToString(rate) + "','" + Convert.ToString(venitemno) + "','" + Convert.ToString(objCommonVar.CurrentCompanyCode) + "'," + IIf(clsCommon.myLen(StrstartDate) > 0, "Convert(Date, '" + StrstartDate + "', 103)", "Null") + " ," + IIf(clsCommon.myLen(StrEndDate) > 0, "Convert(Date, '" + StrEndDate + "', 103)", "Null") + " )"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    Else
                        Dim qry As String = "update TSPL_VENDOR_ITEM_DETAIL set vendor_desc= '" + Convert.ToString(vendesc) + "'  ,item_desc= '" + Convert.ToString(itemdesc) + "',uom= '" + Convert.ToString(uom) + "',MRP= '" + Convert.ToString(mrp) + "',item_rate='" + Convert.ToString(rate) + "' ,vendor_item_no='" + Convert.ToString(venitemno) + "' ,comp_code='" + Convert.ToString(objCommonVar.CurrentCompanyCode) + "', Start_Date=" + IIf(clsCommon.myLen(StrstartDate) > 0, "Convert(Date, '" + StrstartDate + "', 103)", "Null") + ", End_Date=" + IIf(clsCommon.myLen(StrEndDate) > 0, "Convert(Date, '" + StrEndDate + "', 103)", "Null") + " where vendor_code= '" + Convert.ToString(vencode) + "' and item_no='" + Convert.ToString(itemno) + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            Finally
                clsCommon.ProgressBarHide()
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub frmVendorItemDetails_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnsave.Enabled AndAlso MyBase.isModifyFlag Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btndelete.Enabled AndAlso MyBase.isDeleteFlag Then
            delete()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclear.Enabled Then
            Me.Close()
        End If
    End Sub

    Private Sub dgvitem_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles dgvitem.CurrentColumnChanged
        If dgvitem.RowCount > 0 Then
            Dim intCurrRow As Integer = dgvitem.CurrentRow.Index
            If intCurrRow = dgvitem.Rows.Count - 1 Then
                dgvitem.Rows.AddNew()
                dgvitem.CurrentRow = dgvitem.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub dgvitem_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles dgvitem.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

End Class


