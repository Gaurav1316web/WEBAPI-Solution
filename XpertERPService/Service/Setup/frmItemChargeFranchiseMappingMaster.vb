'Created By---> Monika
'Created Date--->31/Mar/2014
Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Data.OleDb
Imports System.Drawing
Imports System.IO
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Imports System.Text.RegularExpressions
Imports Telerik.WinControls.UI.Export
Imports Telerik.WinControls.UI.Export.ExportToExcelML
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Globalization
Imports common
Imports System.Threading
Imports XpertERPEngine

Public Class FrmItemChargeFranchiseMappingMaster
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim obj As New clsfrmItemChrgFranMapMaster()
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Const colItemCode As String = "colItemCode"
    Const colcharges As String = "colcharges"
    Const colItemName As String = "colItemName"
    Const colFranchiseCode As String = "colFranchiseCode"
    Const colFranchiseName As String = "colFranchiseName"
    Const colType As String = "colType"
#End Region

    Public Sub Reset()
        txtchrcode.Value = ""
        lblchrdesc.Text = ""
        gv1.Rows.Clear()
        btnsave.Text = "Save"
        btndelete.Enabled = False
        txtchrcode.MyReadOnly = False
        LoadBlankGrid()
        gv1.Rows.AddNew()
    End Sub
    Public Sub LoadData()
        Try
            If clsCommon.myLen(txtchrcode.Value) <= 0 Then
                txtchrcode.Focus()
                clsCommon.MyMessageBoxShow("Please first select Charge Category Code", Me.Text)
                Exit Sub
            End If

            isInsideLoadData = True
            isCellValueChangedOpen = False
            LoadBlankGrid()
            Dim Arr As List(Of clsfrmItemChrgFranMapMaster) = clsfrmItemChrgFranMapMaster.GetDataALL(txtchrcode.Value)
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                    For Each objTr As clsfrmItemChrgFranMapMaster In Arr
                        gv1.Rows.AddNew()
                        txtchrcode.Value = objTr.chrcatcode
                        lblchrdesc.Text = objTr.chrcatdesc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = objTr.itemcode
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemName).Value = objTr.itemname
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFranchiseCode).Value = objTr.vendrcode
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFranchiseName).Value = objTr.vendrname
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colcharges).Value = objTr.chrgs
                    Next
                End If
            End If
            gv1.Rows.AddNew()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(txtchrcode.Value) <= 0 Then
                txtchrcode.Focus()
                Throw New Exception("Please select Charge Category Code")
            End If

            If gv1.Rows.Count <= 0 Then
                gv1.Focus()
                Throw New Exception("Please select at least one item in grid.")
            End If

            Dim RowCount As Integer = 0
            For ii As Integer = 0 To gv1.Rows.Count - 1
                For jj As Integer = 0 To gv1.Rows.Count - 1


                    If clsCommon.myLen(gv1.Rows(ii).Cells(colItemCode).Value) > 0 AndAlso clsCommon.myLen(gv1.Rows(ii).Cells(colFranchiseCode).Value) = 0 Then
                        clsCommon.MyMessageBoxShow("Please Select Franchises At Rows No. " + clsCommon.myCstr(ii + 1) + "", Me.Text)
                        Return False
                    End If

                    If clsCommon.myLen(gv1.Rows(ii).Cells(colItemCode).Value) > 0 AndAlso clsCommon.myLen(gv1.Rows(ii).Cells(colcharges).Value) = 0 Then
                        clsCommon.MyMessageBoxShow("Please Fill Charges At Rows No. " + clsCommon.myCstr(ii + 1) + "", Me.Text)
                        Return False
                    End If

                    If jj = ii Then
                        'Continue For
                    Else
                        If clsCommon.myLen(gv1.Rows(ii).Cells(colItemCode).Value) > 0 AndAlso clsCommon.myLen(gv1.Rows(jj).Cells(colItemCode).Value) > 0 Then
                            If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colItemCode).Value), clsCommon.myCstr(gv1.Rows(jj).Cells(colItemCode).Value)) = CompairStringResult.Equal AndAlso (clsCommon.myCstr(gv1.Rows(ii).Cells(colFranchiseCode).Value) = clsCommon.myCstr(gv1.Rows(jj).Cells(colFranchiseCode).Value)) Then
                                Dim Msg As String = " Same Item Exist at Row No " + clsCommon.myCstr(ii + 1) + " And " + clsCommon.myCstr(jj + 1)
                                Msg = Msg + Environment.NewLine + "Item: " + clsCommon.myCstr(gv1.Rows(ii).Cells(colItemCode).Value)
                                common.clsCommon.MyMessageBoxShow(Msg)
                                Return False
                            End If
                        End If
                    End If
                Next
                If clsCommon.myLen(gv1.Rows(ii).Cells(colItemCode).Value) > 0 Then
                    RowCount += 1
                End If
            Next
            If RowCount <= 0 Then
                gv1.Focus()
                Throw New Exception("Please select at least one item in grid.")
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
        Return True
    End Function
    Public Sub SaveData()
        Try
            If (AllowToSave()) Then
                Dim Arr As New List(Of clsfrmItemChrgFranMapMaster)
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsfrmItemChrgFranMapMaster()
                    objTr.itemcode = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                    objTr.itemname = clsCommon.myCstr(grow.Cells(colItemName).Value)
                    objTr.vendrcode = clsCommon.myCstr(grow.Cells(colFranchiseCode).Value)
                    objTr.vendrname = clsCommon.myCstr(grow.Cells(colFranchiseName).Value)
                    objTr.chrgs = clsCommon.myCdbl(grow.Cells(colcharges).Value)
                    If clsCommon.myLen(objTr.itemcode) > 0 Then
                        Arr.Add(objTr)
                    End If
                Next
                If (clsfrmItemChrgFranMapMaster.SaveData(txtchrcode.Value, lblchrdesc.Text.Replace("'", "`"), Arr)) Then
                    clsCommon.MyMessageBoxShow("Data saved successfully", Me.Text)
                    btnsave.Text = "Update"
                    btndelete.Enabled = True
                    txtchrcode.MyReadOnly = True
                    LoadData()
                Else
                    Reset()
                    btnsave.Text = "Save"
                    txtchrcode.MyReadOnly = False
                    btndelete.Enabled = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(txtchrcode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("No Data Found For Deletion", Me.Text)
            'Return False
        ElseIf Not (common.clsCommon.MyMessageBoxShow("Delete the Item Charge Franchise Mapping Value " + txtchrcode.Value + "" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
            'Return False
        End If

        If clsfrmItemChrgFranMapMaster.Deletdata(txtchrcode.Value) Then
            common.clsCommon.MyMessageBoxShow("Data Deleted Sucessfully", Me.Text)
            Reset()
            'Return True
        End If
    End Sub
    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colItemCode
        repoICode.HeaderImage = My.Resources.search4
        repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICode.Width = 100
        gv1.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Description"
        repoIName.Name = colItemName
        repoIName.Width = 250
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)

        Dim repoFCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoFCode.FormatString = ""
        repoFCode.HeaderText = "Franchise Code"
        repoFCode.Name = colFranchiseCode
        repoFCode.HeaderImage = My.Resources.search4
        repoFCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoFCode.Width = 100
        gv1.MasterTemplate.Columns.Add(repoFCode)

        Dim repoFName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoFName.FormatString = ""
        repoFName.HeaderText = "Franchise Description"
        repoFName.Name = colFranchiseName
        repoFName.Width = 250
        repoFName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoFName)

        Dim repoType As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoType.FormatString = ""
        repoType.HeaderText = "Charges"
        repoType.Name = colcharges
        repoType.IsVisible = True
        repoType.ReadOnly = False
        repoType.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoType)


        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
    End Sub
    Private Sub FrmItemChargeFranchiseMappingMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Reset()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        SetUserMgmtNew()
        LoadBlankGrid()
        gv1.Rows.AddNew()
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmItemChargeFranchiseMappingMaster)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied", Me.Text)
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub rdexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdexport.Click
        Dim str As String
        str = "select TSPL_ITEM_FRANCHISE_MAPPING.charge_cat_code as 'Charge Cat Code',TSPL_Charge_Category.Description,TSPL_ITEM_FRANCHISE_MAPPING.item_code as 'Item Code',TSPL_ITEM_MASTER.item_desc as 'Item Desc',TSPL_ITEM_FRANCHISE_MAPPING.vendor_code as 'Vendor Code',TSPL_VENDOR_MASTER.vendor_name as 'Vendor Name',TSPL_ITEM_FRANCHISE_MAPPING.Charges from TSPL_ITEM_FRANCHISE_MAPPING left outer join TSPL_Charge_Category on TSPL_Charge_Category.charge_cat_code=TSPL_ITEM_FRANCHISE_MAPPING.charge_cat_code left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_ITEM_FRANCHISE_MAPPING.item_code left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.vendor_code=TSPL_ITEM_FRANCHISE_MAPPING.vendor_code"
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub rdimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdimport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Charge Cat Code", "Description", "Item Code", "Item Desc", "Vendor Code", "Vendor Name", "Charges") Then
            ' Dim trans As SqlTransaction
            Try

                clsCommon.ProgressBarShow()
                Dim Arr As New List(Of clsfrmItemChrgFranMapMaster)
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clsfrmItemChrgFranMapMaster()

                    Dim strCode As String = clsCommon.myCstr(grow.Cells(0).Value)
                    If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                        Throw New Exception(" Charge Category Code can not be blank or incorrect.")
                    End If
                    obj.chrcatcode = strCode
                    txtchrcode.Value = strCode

                    Dim strDec As String = clsCommon.myCstr(grow.Cells(1).Value)

                    obj.chrcatdesc = strDec
                    lblchrdesc.Text = strDec

                    Dim strcode2 As String = clsCommon.myCstr(grow.Cells(2).Value)
                    If strcode2.Length > 30 Or (String.IsNullOrEmpty(strcode2)) Then
                        Throw New Exception(" Invalid Item Code ")
                    End If
                    obj.itemcode = strcode2

                    Dim strDec1 As String = clsCommon.myCstr(grow.Cells(3).Value)

                    obj.itemname = strDec1


                    Dim strCode3 As String = clsCommon.myCstr(grow.Cells(4).Value)
                    If strCode3.Length > 30 Or (String.IsNullOrEmpty(strCode3)) Then
                        Throw New Exception(" Charge Vendor Code can not be blank or incorrect.")
                    End If
                    obj.vendrcode = strCode3

                    Dim strDec3 As String = clsCommon.myCstr(grow.Cells(5).Value)
                    obj.vendrname = strDec3

                    Dim strDec4 As String = clsCommon.myCstr(grow.Cells(6).Value)
                    obj.chrgs = strDec4

                    If clsCommon.myLen(obj.itemcode) > 0 Then
                        Arr.Add(obj)
                    End If


                Next

                clsfrmItemChrgFranMapMaster.SaveData(txtchrcode.Value, lblchrdesc.Text, Arr)

                txtchrcode.Value = ""
                lblchrdesc.Text = ""
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try


        End If


        Me.Controls.Remove(gv)
    End Sub

    Private Sub rdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdexit.Click
        Me.Close()
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        Reset()
        btnsave.Text = "Save"
        btndelete.Enabled = False
    End Sub

    Private Sub FrmItemChargeFranchiseMappingMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If Not isInsideLoadData Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(colItemCode) Then
                        OpenItemFinder(False)
                    ElseIf e.Column Is gv1.Columns(colType) Then
                        gv1.CurrentRow.Cells(colType).Value = clsCommon.myCdbl(gv1.CurrentRow.Cells(colType).Value)
                    ElseIf e.Column Is gv1.Columns(colFranchiseCode) Then
                        OpenFranchiseList(False)
                    End If

                End If
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub
    Sub OpenFranchiseList(ByVal isButtonClick As Boolean)

        Dim qry As String = "select Vendor_Code as Code,Vendor_Name as [Vendor],(add1+' '+add2+' '+add3) as Address,city_code_desc as [City],vendor_group_code_desc as [Vendor Group Description] from TSPL_vendor_master "
        Dim whrCls As String = " franchise_yn='Y'"
        gv1.CurrentRow.Cells(colFranchiseCode).Value = clsCommon.ShowSelectForm("cumFMList", qry, "code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colFranchiseCode).Value), "code", isButtonClick)

        If gv1.CurrentRow.Cells(colFranchiseCode).Value = "" Then
            Return
        End If

        Dim obj1 As clsfrmItemChrgFranMapMaster = clsfrmItemChrgFranMapMaster.GetData1(clsCommon.myCstr(gv1.CurrentRow.Cells(colFranchiseCode).Value), NavigatorType.Current, Nothing)
        If obj IsNot Nothing Then
            gv1.CurrentRow.Cells(colFranchiseCode).Value = obj1.vendrcode
            gv1.CurrentRow.Cells(colFranchiseName).Value = obj1.vendrname

        Else
            gv1.CurrentRow.Cells(colFranchiseCode).Value = ""
            gv1.CurrentRow.Cells(colFranchiseName).Value = False
        End If


    End Sub
    Sub OpenItemFinder(ByVal isButtonClick As Boolean)
        Dim qry As String = "select item_Code as Code,item_Desc as Description,structure_desc as [Structure Desc],unit_code as Unit,item_type as [Type Of Items],Rate from TSPL_ITEM_MASTER"
        Dim whrCls As String = ""
        gv1.CurrentRow.Cells(colItemCode).Value = clsCommon.ShowSelectForm("cumIMList", qry, "code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value), "code", isButtonClick)

        If gv1.CurrentRow.Cells(colItemCode).Value = "" Then
            Return
        End If

        Dim obj1 As clsfrmItemChrgFranMapMaster = clsfrmItemChrgFranMapMaster.GetData(clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value), NavigatorType.Current, Nothing)
        If obj IsNot Nothing Then
            gv1.CurrentRow.Cells(colItemCode).Value = obj1.itemcode
            gv1.CurrentRow.Cells(colItemName).Value = obj1.itemname

        Else
            gv1.CurrentRow.Cells(colItemCode).Value = ""
            gv1.CurrentRow.Cells(colItemName).Value = False

        End If
    End Sub
    Private Sub txtchrcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtchrcode._MYValidating
        Try
            If isButtonClicked Then
                Dim qry As String = "select Charge_cat_code as Code,Description,gl_code as [Account Code],gl_desc as [Account Description] from   TSPL_charge_category "
                txtchrcode.Value = clsCommon.ShowSelectForm("CHRIDFND", qry, "code", "", txtchrcode.Value, "", isButtonClicked)
                lblchrdesc.Text = clsDBFuncationality.getSingleValue("Select description from TSPL_charge_category where Charge_cat_code='" + txtchrcode.Value + "'")

                If txtchrcode.Value = "" Then
                    Return
                End If
            Else
                lblchrdesc.Text = clsDBFuncationality.getSingleValue("Select description from TSPL_charge_category where Charge_cat_code='" + txtchrcode.Value + "'")
            End If

            LoadBlankGrid()
            gv1.Rows.AddNew()
            If clsCommon.myLen(txtchrcode.Value) > 0 Then
                Dim qry As String = "select count(*) from TSPL_ITEM_FRANCHISE_MAPPING where charge_cat_code='" + txtchrcode.Value + "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
                If (check > 0) Then
                    LoadData()
                    btnsave.Text = "Update"
                    txtchrcode.MyReadOnly = True
                    btndelete.Enabled = True
                Else
                    btnsave.Text = "Save"
                    txtchrcode.MyReadOnly = False
                    btndelete.Enabled = False
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub


    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If clsCommon.myLen(txtchrcode.Value) > 0 Then
            If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                e.Cancel = True
                Exit Sub
            Else
                Dim qry As String = " Select count(*) from TSPL_ITEM_FRANCHISE_MAPPING where item_code= '" + gv1.CurrentRow.Cells(colItemCode).Value + "' and charge_cat_code='" + txtchrcode.Value + "' "
                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) > 0 Then
                    clsCommon.MyMessageBoxShow("Current File is in Use.")
                    e.Cancel = True
                    Exit Sub
                Else
                    qry = " delete from TSPL_ITEM_FRANCHISE_MAPPING where charge_cat_code='" + txtchrcode.Value + "' and item_code= '" + gv1.CurrentRow.Cells(colItemCode).Value + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry)
                End If
            End If
        End If
    End Sub

    Private Sub RadGroupBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadGroupBox1.Click

    End Sub

    Private Sub btnclose_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
End Class
