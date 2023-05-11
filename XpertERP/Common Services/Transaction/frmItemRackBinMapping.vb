Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.WinControls.Enumerations
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Text.RegularExpressions
Imports common

Public Class frmItemRackBinMapping
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()

#Region "Variables"
    Dim dt As DataTable
    Dim isInsideLoadData As Boolean = False
    Const colICode As String = "colICode"
    Const colRackCode As String = "colRackCode"
    Const colBinCode As String = "colBinCode"
    Const colBinName As String = "colBinName"
    Const colRackName As String = "colRackName"
    Public strLocation As String = Nothing
    Public strReqQty As Decimal = 0
    Public strBalQty As Decimal = 0
    Public UnitCode As String = Nothing
    Private isNewEntry As Boolean = False

#End Region

    Private Sub SetUserMgmtNew()

    End Sub
    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoACCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoACCode.FormatString = ""
        repoACCode.HeaderText = "Item Code"
        repoACCode.Name = colICode
        repoACCode.Width = 100
        repoACCode.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoACCode)

        Dim repoBinCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBinCode.FormatString = ""
        repoBinCode.HeaderText = "Bin Code"
        repoBinCode.Name = colBinCode
        repoBinCode.Width = 100
        repoBinCode.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoBinCode)

        Dim repoBinName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBinName.FormatString = ""
        repoBinName.HeaderText = "Bin Name"
        repoBinName.Name = colBinName
        repoBinName.Width = 100
        repoBinName.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoBinName)

        Dim repoRack As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRack.FormatString = ""
        repoRack.HeaderText = "Rack Code"
        repoRack.Name = colRackCode
        repoRack.Width = 100
        repoRack.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoRack)

        Dim repoRackName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRackName.FormatString = ""
        repoRackName.HeaderText = "Rack Name"
        repoRackName.Name = colRackName
        repoRackName.Width = 100
        repoRackName.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoRackName)

        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40

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
    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Save()
    End Sub
    Public Function Save() As Boolean

        If AllowToSave() Then
            Dim obj As New clsItemRackbinMapping()
            obj.Arr = New List(Of clsItemRackbinMapping)
            For Each grow As GridViewRowInfo In gv1.Rows
                Dim objTr As New clsItemRackbinMapping()

                objTr.Location = txtDocNo.Value
                objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                objTr.Rack_Code = clsCommon.myCstr(grow.Cells(colRackCode).Value)
                objTr.Bin_Code = clsCommon.myCstr(grow.Cells(colBinCode).Value)

                If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                    obj.Arr.Add(objTr)
                End If

            Next
            If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow("Please Fill at list one Item")
                Return False
            End If
            Dim isSaved As Boolean = True
            isSaved = isSaved AndAlso obj.SaveData(txtDocNo.Value, obj.Arr)

            If isSaved = True Then
                clsCommon.MyMessageBoxShow("Data Saved", Me.Text)
            End If
        End If
    End Function
    Function AllowToSave() As Boolean
        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please fill Location")
            Return False
        End If
        If gv1.RowCount > 0 AndAlso gv1.ColumnCount > 0 Then
            Dim oldCurrentRow As Integer = IIf(gv1.CurrentRow.Index < 0, 0, gv1.CurrentRow.Index)
            Dim oldCurrentColumne As Integer = IIf(gv1.CurrentColumn.Index < 0, 0, gv1.CurrentColumn.Index)
            For ii As Integer = 0 To gv1.RowCount - 1

                Dim strItemCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
                Dim strRackCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colRackCode).Value)
                Dim strBinCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colBinCode).Value)
                If clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) < 0 Then
                    clsCommon.MyMessageBoxShow("Please fill Item Code")
                    Return False
                End If
                If clsCommon.myLen(gv1.Rows(ii).Cells(colBinCode).Value) < 0 Then
                    clsCommon.MyMessageBoxShow("Please fill Bin Code")
                    Return False
                End If
                If clsCommon.myLen(gv1.Rows(ii).Cells(colRackCode).Value) < 0 Then
                    clsCommon.MyMessageBoxShow("Please fill Rack Code")
                    Return False
                End If
            Next
            gv1.CurrentRow = gv1.Rows(oldCurrentRow)
            gv1.CurrentColumn = gv1.Columns(oldCurrentColumne)
        End If
      

        Return True
    End Function
    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Reset()
    End Sub

    Sub Reset()
        gv1.DataSource = Nothing
        txtDocNo.Value = ""
        LoadBlankGrid()
        gv1.Rows.AddNew()

    End Sub

    Private Sub frmItemReorderLevel1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then

        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Private Sub frmItemReorderLevel1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save")
        LoadBlankGrid()
        'Reset()
        gv1.Rows.AddNew()

    End Sub
    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        If e.Column Is gv1.Columns(colICode) Then
            OpenICodeList(False)
        End If
        If e.Column Is gv1.Columns(colBinCode) Then
            OpenBinList(False)
        End If

    End Sub
    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Select Location First", Me.Text)
        End If
        Dim qry As String = "select * from tspl_item_master "
        gv1.CurrentRow.Cells(colICode).Value = clsCommon.ShowSelectForm("ItCode", qry, "Item_Code", "", gv1.CurrentRow.Cells(colICode).Value, "Item_Code", isButtonClick)
      

    End Sub
    Sub OpenBinList(ByVal isButtonClick As Boolean)
        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Select Location First", Me.Text)
        End If
        Dim qry As String = "select Bin_Code as Code,Description as Name,Location from TSPL_Bin_MASTER "
        gv1.CurrentRow.Cells(colBinCode).Value = clsCommon.ShowSelectForm("BinCode", qry, "Code", "Location='" & txtDocNo.Value & "' ", gv1.CurrentRow.Cells(colBinCode).Value, "Code", isButtonClick)
        If clsCommon.myLen(gv1.CurrentRow.Cells(colBinCode).Value) > 0 Then
            gv1.CurrentRow.Cells(colBinName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from tspl_bin_master where Bin_Code='" + gv1.CurrentRow.Cells(colBinCode).Value + "'"))
            gv1.CurrentRow.Cells(colRackCode).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Rack_Code from tspl_bin_master where Bin_Code='" + gv1.CurrentRow.Cells(colBinCode).Value + "'"))
            gv1.CurrentRow.Cells(colRackName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_RACK_MASTER.Description from tspl_bin_master left outer join TSPL_RACK_MASTER on TSPL_RACK_MASTER.Code=TSPL_BIN_MASTER.Rack_Code where TSPL_BIN_MASTER.Bin_Code='" + gv1.CurrentRow.Cells(colBinCode).Value + "'"))
        End If

    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        Reset()
    End Sub
    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocNo._MYValidating
        Try
            Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
            Dim WhrCls As String = String.Empty

            WhrCls = " Location_Type='Physical'  "

            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            txtDocNo.Value = clsCommon.ShowSelectForm("LOC", qry, "Code", WhrCls, txtDocNo.Value, "Code", True)

            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                LoadData(txtDocNo.Value, NavigatorType.Current)
                gv1.Rows.AddNew()
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
   
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        txtDocNo.MyReadOnly = True
        btnSave.Enabled = True

        isNewEntry = False
        Dim obj As New clsItemRackbinMapping()
        obj = clsItemRackbinMapping.GetData(strCode, NavTyep)
        LoadBlankGrid()
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Location) > 0 Then
            For Each objTr As clsItemRackbinMapping In obj.Arr
                gv1.Rows.AddNew()
                txtDocNo.Value = objTr.Location
                gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                gv1.Rows(gv1.Rows.Count - 1).Cells(colBinCode).Value = objTr.Bin_Code
                gv1.Rows(gv1.Rows.Count - 1).Cells(colBinName).Value = clsDBFuncationality.getSingleValue("select Description from TSPL_BIN_MASTER where bin_code='" & objTr.Bin_Code & "' ")
                gv1.Rows(gv1.Rows.Count - 1).Cells(colRackCode).Value = objTr.Rack_Code
                gv1.Rows(gv1.Rows.Count - 1).Cells(colRackName).Value = clsDBFuncationality.getSingleValue("select Description from TSPL_RACK_MASTER where Code='" & objTr.Rack_Code & "' ")


            Next

        End If
    End Sub

    Private Sub rmiExport_Click(sender As Object, e As EventArgs) Handles rmiExport.Click
        Dim str As String
        str = "select Location, Item_Code as [Item Code],Bin_Code as [Bin Code] from TSPL_Item_Rack_Bin_Mapping"
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub rmiImport_Click(sender As Object, e As EventArgs) Handles rmiImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim gvbool As Boolean = False
        gvbool = transportSql.importExcel(gv, "Location", "Item Code", "Bin Code")
        If gvbool Then
            Dim linno As Integer = 1
            Try
                Dim arr As New List(Of clsItemRackbinMapping)
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clsItemRackbinMapping()
                    linno += 1
                    Dim strLocCode As String = clsCommon.myCstr(grow.Cells("Location").Value)
                    If clsCommon.myLen(strLocCode) <= 0 Then
                        Throw New Exception("Location Code should not be left blank ")
                    End If
                    Dim strItemCode As String = clsCommon.myCstr(grow.Cells("Item Code").Value)
                    If clsCommon.myLen(strItemCode) <= 0 Then
                        Throw New Exception("Item Code should not be left blank ")
                    End If
                    Dim strBinCode As String = clsCommon.myCstr(grow.Cells("Bin Code").Value)
                    If clsCommon.myLen(strBinCode) <= 0 Then
                        Throw New Exception("Bin Code should not be left blank ")
                    End If
                    Dim BinLocation As String = clsDBFuncationality.getSingleValue("Select Bin_Code from tspl_bin_master where location='" & strLocCode & "'", trans)
                    If clsCommon.myLen(BinLocation) <= 0 Then
                        Throw New Exception("Bin Code not found for this Location ")
                    End If
                    Dim BinCount As String = clsDBFuncationality.getSingleValue("Select Bin_Code from tspl_bin_master where Bin_Code='" & strBinCode & "'", trans)
                    If clsCommon.myLen(BinCount) <= 0 Then
                        Throw New Exception("Bin Code should not be left blank ")
                    End If
                    Dim strRackCode As String = clsDBFuncationality.getSingleValue("select Rack_Code from tspl_bin_master where Bin_Code='" & strBinCode & "'", trans)
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Location", strLocCode)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", strItemCode)
                    clsCommon.AddColumnsForChange(coll, "Bin_Code", strBinCode)
                    clsCommon.AddColumnsForChange(coll, "Rack_Code", strRackCode)

                    Dim coount As String = clsDBFuncationality.getSingleValue("select Bin_Code from TSPL_ITEM_RACK_BIN_MAPPING where location='" & strLocCode & "' and Item_Code='" & strItemCode & "'", trans)
                    If clsCommon.myLen(coount) > 0 Then
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_RACK_BIN_MAPPING", OMInsertOrUpdate.Update, "TSPL_ITEM_RACK_BIN_MAPPING.Location='" + strLocCode + "' and  TSPL_ITEM_RACK_BIN_MAPPING.Item_Code='" + strItemCode + "'", trans)

                    Else
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_RACK_BIN_MAPPING", OMInsertOrUpdate.Insert, "", trans)
                    End If
                    clsCommon.ProgressBarPercentUpdate(((linno + 1) * 100 / (gv.Rows.Count + 1)), "Saving : " & clsCommon.myCstr(linno + 1) & "/" & clsCommon.myCstr(gv.Rows.Count) & "")
                Next
                trans.Commit()
                clsCommon.ProgressBarPercentHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarPercentHide()
                clsCommon.MyMessageBoxShow(ex.Message & " At Line No : " & linno)
            End Try
        End If

    End Sub
End Class
