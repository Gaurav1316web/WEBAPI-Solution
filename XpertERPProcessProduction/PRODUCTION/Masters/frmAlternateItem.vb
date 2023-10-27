Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports System.Threading
Imports System.Data.Sql
Imports common
''''''''''''''''''''''''''''''''''''''''''Ticket No:BM00000000477''''''''''''''''''''''''''''''''''''''''''''''''Created by Shipra on 13/09/13''''''

Public Class FrmAlternateItem
    Inherits FrmMainTranScreen
    Private isCellValueChangedOpen As Boolean = False
    Dim iStxtTaxGroup_TxtChangedComplete As Boolean = True
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Const colitemno As String = "Item No"
    Const coldesc As String = "Description"
    Const coluom As String = "UOM"
    Const colQty As String = "Qty per"
    Const colprior As String = "Priority"
    Const colComments As String = "Comments"
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()

    'Const colStartDate As String = "StartDate"
    'Const colEndDate As String = "EndDate"
#Region "Functions"

    Sub LoadBlankGrid()

        dgvitem.AddNewRowPosition = SystemRowPosition.Bottom
        dgvitem.Rows.Clear()
        dgvitem.Columns.Clear()

        Dim item_code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        item_code.FormatString = ""
        item_code.HeaderText = "Item No"
        item_code.Name = colitemno
        item_code.Width = 90
        item_code.ReadOnly = False
        item_code.TextImageRelation = TextImageRelation.TextBeforeImage
        item_code.HeaderImage = Global.XpertERPProcessProduction.My.Resources.Resources.search4
        item_code.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        dgvitem.MasterTemplate.Columns.Add(item_code)

        Dim item_desc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        item_desc.FormatString = ""
        item_desc.HeaderText = "Description"
        item_desc.Name = coldesc
        item_desc.Width = 200
        item_desc.ReadOnly = True
        item_desc.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        dgvitem.MasterTemplate.Columns.Add(item_desc)

        Dim Qty As GridViewDecimalColumn = New GridViewDecimalColumn()
        Qty.FormatString = ""
        Qty.HeaderText = "Qty per"
        Qty.Name = colQty
        Qty.Width = 70
        Qty.ReadOnly = False
        dgvitem.MasterTemplate.Columns.Add(Qty)

        Dim uom As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        uom.FormatString = ""
        uom.HeaderText = "UOM"
        uom.Name = coluom
        uom.Width = 70
        uom.ReadOnly = False
        uom.TextImageRelation = TextImageRelation.TextBeforeImage
        uom.HeaderImage = Global.XpertERPProcessProduction.My.Resources.Resources.search4
        uom.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        dgvitem.MasterTemplate.Columns.Add(uom)

        Dim Priority As GridViewDecimalColumn = New GridViewDecimalColumn()
        Priority.FormatString = ""
        Priority.HeaderText = "Priority"
        Priority.Name = colprior
        Priority.Width = 70
        Priority.ReadOnly = False
        dgvitem.MasterTemplate.Columns.Add(Priority)


        Dim Comments As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Comments.FormatString = ""
        Comments.HeaderText = "Comments"
        Comments.Name = colComments
        Comments.Width = 150
        Comments.ReadOnly = False
        dgvitem.MasterTemplate.Columns.Add(Comments)



        dgvitem.AllowDeleteRow = True
        dgvitem.AllowAddNewRow = True
        dgvitem.ShowGroupPanel = False
        dgvitem.AllowColumnReorder = False
        dgvitem.AllowRowReorder = False
        dgvitem.EnableSorting = False
        dgvitem.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        dgvitem.MasterTemplate.ShowRowHeaderColumn = False


    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.ALTER)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 02/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnsave.Visible = True Then
            rmExport.Enabled = True
            rmimport.Enabled = True
        Else
            rmExport.Enabled = False
            rmimport.Enabled = False
        End If
        '--------------------------------------------------
        ' btnPost.Visible = MyBase.isPostFlag

    End Sub

    Sub SaveData()
        Try
            If (AllowToSave()) Then
                Dim obj As New ClsAlternateitemDetail()
                obj.ITEM_CODE = fndvendor.Value
                Dim Arr As New List(Of ClsAlternateitemDetail)
                For Each grow As GridViewRowInfo In dgvitem.Rows
                    Dim objTr As New ClsAlternateitemDetail()
                    objTr.SUBSTITUTE_ITEM_CODE = clsCommon.myCstr(grow.Cells(colitemno).Value)
                    objTr.DESCRIPTION = clsCommon.myCstr(grow.Cells(coldesc).Value)
                    objTr.UNIT_CODE = clsCommon.myCstr(grow.Cells(coluom).Value)
                    objTr.PRIORITY = clsCommon.myCdbl(grow.Cells(colprior).Value)
                    objTr.QUANTITY = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    objTr.COMMENTS = clsCommon.myCstr(grow.Cells(colComments).Value)
                
                    If (clsCommon.myLen(objTr.SUBSTITUTE_ITEM_CODE) > 0) Then
                        Arr.Add(objTr)
                    End If
                Next


                If (Arr Is Nothing OrElse Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow("Please Fill at least one Item")
                    Return
                End If

                'Dim objHist As New ClsAlternateitemDetailHistory()
                'objHist.SaveDataHistory(fndvendor.Value)

                If (obj.SaveData(fndvendor.Value, txtdesc.Text, Arr)) Then
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    btnsave.Text = "Update"
                    'LoadData(obj.vendor_code, NavigatorType.Current)
                End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub


    Function AllowToSave() As Boolean
        Try

            If clsCommon.myLen(fndvendor.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select Item No")
                fndvendor.Focus()
                Return False
            End If

            Dim arrICode As New List(Of String)()
            For ii As Integer = 0 To dgvitem.Rows.Count - 1
                Dim strICode As String = clsCommon.myCstr(dgvitem.Rows(ii).Cells(colitemno).Value)
                If (clsCommon.CompairString(clsCommon.myCstr(dgvitem.Rows(ii).Cells(coluom).Value), Nothing) = CompairStringResult.Equal) Then
                    common.clsCommon.MyMessageBoxShow("Please fill UOM At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + "")
                    Return False
                End If
                If (clsCommon.CompairString(clsCommon.myCstr(dgvitem.Rows(ii).Cells(colprior).Value), Nothing) = CompairStringResult.Equal) Then
                    common.clsCommon.MyMessageBoxShow("Please fill Priority At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + "")
                    Return False
                End If
                If (clsCommon.CompairString(clsCommon.myCstr(dgvitem.Rows(ii).Cells(colQty).Value), Nothing) = CompairStringResult.Equal) Then
                    common.clsCommon.MyMessageBoxShow("Please fill Quantity At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                    Return False
                End If
                For jj As Integer = 0 To dgvitem.Rows.Count - 1
                    If (ii = jj) Then
                        Continue For
                    End If
                    If (clsCommon.CompairString(strICode, clsCommon.myCstr(dgvitem.Rows(jj).Cells(colitemno).Value)) = CompairStringResult.Equal) Then
                        common.clsCommon.MyMessageBoxShow("Already selected Item " + strICode.Trim() + "( " + strICode.Trim() + " ) At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " and  " + clsCommon.myCstr(clsCommon.myCdbl(jj + 1)) + "")
                        Return False
                    End If

                Next
                If Not arrICode.Contains(strICode) Then
                    arrICode.Add(strICode)
                End If
            Next


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
        Return True
    End Function

    Sub LoadData(ByVal vendorcode As String, ByVal Desc As String)
        Try
            btnsave.Enabled = True
            btnsave.Text = "Save"

            isInsideLoadData = True

            'btnsave.Text = "Update"

            'funreset()
            LoadBlankGrid()

            Dim Arr As List(Of ClsAlternateitemDetail) = ClsAlternateitemDetail.GetData(vendorcode)
            'fndvendor.Value = vendorcode
            txtdesc.Text = Desc
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each objTr As ClsAlternateitemDetail In Arr
                    dgvitem.Rows.AddNew()
                    dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(colitemno).Value = objTr.SUBSTITUTE_ITEM_CODE
                    dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(coldesc).Value = objTr.DESCRIPTION
                    dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(coluom).Value = objTr.UNIT_CODE
                    dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(colQty).Value = objTr.QUANTITY
                    dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(colprior).Value = objTr.PRIORITY
                    dgvitem.Rows(dgvitem.Rows.Count - 1).Cells(colComments).Value = objTr.COMMENTS
                    btnsave.Text = "Update"
                Next
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    Sub openuom(ByVal isButtonClick As Boolean)
        dgvitem.CurrentRow.Cells(coluom).Value = clsItemMaster.FinderForuom(clsCommon.myCstr(dgvitem.CurrentRow.Cells(coluom).Value), clsCommon.myCstr(dgvitem.CurrentRow.Cells(colitemno).Value), isButtonClick)
    End Sub

    Public Sub funreset()
        fndvendor.Value = ""
        txtdesc.Text = ""
        LoadBlankGrid()
        btnsave.Text = "Save"
        'btndelete.Enabled = False
    End Sub

    Sub OpenICodeList(ByVal isButtonClick As Boolean)

        Dim obj As clsItemMaster = clsItemMaster.FinderForItem(clsCommon.myCstr(dgvitem.CurrentRow.Cells(colitemno).Value), "", isButtonClick)
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
            dgvitem.CurrentRow.Cells(colitemno).Value = obj.Item_Code
            dgvitem.CurrentRow.Cells(coldesc).Value = obj.Item_Desc
            dgvitem.CurrentRow.Cells(coluom).Value = obj.Unit_Code
        Else
            dgvitem.CurrentRow.Cells(colitemno).Value = ""
            dgvitem.CurrentRow.Cells(coldesc).Value = ""
            dgvitem.CurrentRow.Cells(coldesc).Value = ""
        End If

    End Sub
    Sub BlankAllControls()
        fndvendor.Value = ""
        txtdesc.Text = ""
        LoadBlankGrid()
        btnsave.Text = "Save"

    End Sub
#End Region
#Region "Finders And Navigator"
    Private Sub fndvendor__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndvendor._MYValidating
        Dim qry As String = "Select Item_Code as Code,Item_Desc as Description from TSPL_ITEM_master"
        fndvendor.Value = clsCommon.ShowSelectForm("VendorMasFND", qry, "Code", "", fndvendor.Value, "Code", isButtonClicked)
        txtdesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Item_Desc  from TSPL_ITEM_master where Item_Code='" + fndvendor.Value + "'"))
        LoadData(fndvendor.Value, txtdesc.Text)
    End Sub
#End Region
#Region "EVENTS"
    Private Sub FrmAlternateItem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
       
        LoadBlankGrid()

        btnsave.Enabled = True
        SetUserMgmtNew()
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If

        ButtonToolTip.SetToolTip(btnclear, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P Print the Report")

        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S to Save")
    End Sub

    Private Sub FrmAlternateItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnnew.Enabled Then
            funreset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()

        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclear.Enabled Then
            Me.Close()
            'ElseIf e.Control AndAlso e.KeyCode = Keys.P AndAlso btnPrint.Enabled Then
            '    Print()
        End If
    End Sub


    Private Sub dgvitem_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles dgvitem.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub
    Private Sub rmExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmExport.Click
        Dim str As String
        str = "select ITEM_CODE as Item,SUBSTITUTE_ITEM_CODE as [Substitute Item],DESCRIPTION as Description,QUANTITY as Qty,UNIT_CODE as UOM,PRIORITY as Priority,COMMENTS as Comments from TSPL_MF_SUBSTITUTE_ITEMS"

        Dim check As Integer = clsDBFuncationality.getSingleValue("select count(*) from TSPL_MF_SUBSTITUTE_ITEMS")
        If check <= 0 Then
            clsCommon.MyMessageBoxShow("No Data Found To Transfer,First Save The Data", Me.Text)
            Return
        End If
        transportSql.ExporttoExcel(str, Me)
    End Sub

    'Private Sub rmimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmimport.Click
    '    Dim gv As New RadGridView()
    '    Me.Controls.Add(gv)
    '    Dim currentdate As Date = Date.Today
    '    If transportSql.importExcel(gv, "Item", "Substitute Item", "Description", "Qty", "UOM", "Priority", "Comments") Then
    '        Dim trans As SqlTransaction
    '        Try
    '            trans = clsDBFuncationality.GetTransactin()
    '            clsCommon.ProgressBarShow()
    '            Dim LineNo As String
    '            For Each grow As GridViewRowInfo In gv.Rows
    '                LineNo = clsCommon.myCstr(grow.Index + 2)

    '                Dim ItemCode As String = clsCommon.myCstr(grow.Cells(0).Value)
    '                If ItemCode.Length > 0 Then
    '                    ItemCode = "select Item_code from tspl_Item_master where Item_code='" + ItemCode + "'"
    '                    ItemCode = clsDBFuncationality.getSingleValue(ItemCode, trans)
    '                    If clsCommon.myLen(ItemCode) <= 0 Then
    '                        Throw New Exception("Item Code '" + ItemCode + "' at line " + LineNo + " does not exist .")
    '                    End If
    '                Else
    '                    Throw New Exception("Item Code can not be blank at line " + LineNo + " .")
    '                End If


    '                Dim SubItemCode As String = clsCommon.myCstr(grow.Cells(1).Value)
    '                If SubItemCode.Length > 0 Then
    '                    SubItemCode = "select Item_code from tspl_Item_master where Item_code='" + SubItemCode + "'"
    '                    SubItemCode = clsDBFuncationality.getSingleValue(SubItemCode, trans)
    '                    If clsCommon.myLen(SubItemCode) <= 0 Then
    '                        Throw New Exception("Alternate Item Code '" + SubItemCode + "' at line " + LineNo + " does not exist .")
    '                    End If
    '                Else
    '                    Throw New Exception("Alternate Item Code can not be blank at line " + LineNo + " .")
    '                End If

    '                Dim Itemdesc As String = clsDBFuncationality.getSingleValue("Select Item_Desc FROM tspl_Item_master WHERE Item_code='" + SubItemCode + "'", trans)


    '                Dim Qty As String = clsCommon.myCstr(grow.Cells(2).Value)
    '                If Qty.Length <= 3 And IsNumeric(Qty) Then
    '                Else
    '                    Throw New Exception("Check the value of 'Qty Per'.")
    '                End If

    '                Dim strUnit As String = clsCommon.myCstr(grow.Cells(3).Value)
    '                If strUnit.Length > 0 Then
    '                    strUnit = "select Unit_Code from TSPL_UNIT_MASTER where Unit_Code='" + strUnit + "'"
    '                    strUnit = clsDBFuncationality.getSingleValue(SubItemCode, trans)
    '                    If clsCommon.myLen(strUnit) <= 0 Then
    '                        Throw New Exception("Unit '" + strUnit + "' at line " + LineNo + " does not exist .")
    '                    End If
    '                Else
    '                    Throw New Exception("Unit can not be blank at line " + LineNo + " .")
    '                End If

    '                Dim strPriority As String = clsCommon.myCstr(grow.Cells(4).Value)
    '                If strPriority.Length <= 8 And IsNumeric(strPriority) Then
    '                Else
    '                    Throw New Exception("Check the value of 'Priority'.")
    '                End If
    '                Dim strComments As String = clsCommon.myCstr(grow.Cells(5).Value)
    '                Dim obj As New ClsAlternateitemDetail()
    '                obj.ITEM_CODE = ItemCode
    '                Dim Arr As New List(Of ClsAlternateitemDetail)

    '                Dim objTr As New ClsAlternateitemDetail()
    '                objTr.SUBSTITUTE_ITEM_CODE = clsCommon.myCstr(grow.Cells(colitemno).Value)
    '                objTr.DESCRIPTION = clsCommon.myCstr(grow.Cells(coldesc).Value)
    '                objTr.UNIT_CODE = clsCommon.myCstr(grow.Cells(coluom).Value)
    '                objTr.PRIORITY = clsCommon.myCdbl(grow.Cells(colprior).Value)
    '                objTr.QUANTITY = clsCommon.myCdbl(grow.Cells(colQty).Value)
    '                objTr.COMMENTS = clsCommon.myCstr(grow.Cells(colComments).Value)

    '                If (clsCommon.myLen(objTr.SUBSTITUTE_ITEM_CODE) > 0) Then
    '                    Arr.Add(objTr)
    '                End If
    '            Next


    '            If (Arr Is Nothing OrElse Arr.Count <= 0) Then
    '                common.clsCommon.MyMessageBoxShow("Please Fill at least one Item")
    '                Return
    '            End If
    '            Next
    '            trans.Commit()
    '            clsCommon.ProgressBarHide()
    '            common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
    '        Catch ex As Exception
    '            trans.Rollback()
    '            clsCommon.ProgressBarHide()
    '            myMessages.myExceptions(ex)
    '        Finally
    '            clsCommon.ProgressBarHide()
    '        End Try

    '    End If
    '    Me.Controls.Remove(gv)
    'End Sub
   




    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub


    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        Me.Close()
    End Sub


    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        funreset()
    End Sub

    Private Sub dgvitem_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles dgvitem.CellValueChanged
        Try
            If isInsideLoadData = False Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is dgvitem.Columns(colitemno) Then
                        OpenICodeList(False)
                    ElseIf e.Column Is dgvitem.Columns(coluom) Then
                        openuom(False)
                    End If
                    isCellValueChangedOpen = False
                End If
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally

        End Try
    End Sub
#End Region
End Class
