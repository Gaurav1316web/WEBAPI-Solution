Imports common
Imports System.Data
imports System.Data.SqlClient

Public Class FrmProcessMaster1
    Inherits FrmMainTranScreen
    Const colLineNo As String = "colLineNo"
    Const colItem As String = "colItem"
    Const colCapacity As String = "colCapacity"

    Private isCellValueChangedOpen As Boolean = False
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim Qry As String

    Private Sub FrmProcessMaster1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        AddNew()
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt("FrmProcessMaster1")
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnSave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 02/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnSave.Visible = True Then
            RMExport.Enabled = True
            RMImport.Enabled = True
        Else
            RMExport.Enabled = False
            RMImport.Enabled = False
        End If
        '--------------------------------------------------
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoItem As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoItem.FormatString = ""
        repoItem.HeaderText = "Item"
        repoItem.Name = colItem
        repoItem.TextImageRelation = TextImageRelation.TextBeforeImage
        repoItem.Width = 250
        repoItem.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoItem)

        Dim repoCapacity As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCapacity.FormatString = ""
        repoCapacity.HeaderText = "Capacity"
        repoCapacity.Name = colCapacity
        repoCapacity.Width = 250
        repoCapacity.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoCapacity)

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gv1_UserAddedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserAddedRow
        For i As Integer = 0 To gv1.Rows.Count - 1
            gv1.Rows(0).Cells(0).Value = 1
            If i <> 0 Then
                gv1.Rows(i).Cells(colLineNo).Value = i + 1
            End If
        Next
    End Sub
    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SavedData()
    End Sub
    Sub SavedData()
        Try
            If clsCommon.myLen(txtProcessNo.Value) > 12 Then
                Throw New Exception("Lenghth of Process Code should not be greater than 12.")
                Exit Sub
            End If
            If clsCommon.myLen(txtProcessNo.Value) = 0 Then
                Throw New Exception("Please enter Process Code.")
                Exit Sub
            End If
            
            'Dim Arr As List(Of clsProcessMaster) = New List(Of clsProcessMaster)()
            'For Each grow As GridViewRowInfo In gv1.Rows
            '    If clsCommon.myLen(grow.Cells(colItem).Value) > 0 Then
            '        Dim obj As New clsProcessMaster()
            '        obj.Process_Code = txtProcessNo.Value
            '        obj.Process_Desc = txtReference.Text
            '        obj.Item_Code = clsCommon.myCstr(grow.Cells(colItem).Value)
            '        obj.Capacaity = clsCommon.myCstr(grow.Cells(colCapacity).Value)

            '        Arr.Add(obj)
            '    End If
            'Next
            'If Arr.Count = 0 Then
            '    Throw New Exception("Please enter atleast one Item.")
            '    Exit Sub
            'End If
            'Dim isSaved As Boolean = clsProcessMaster.SaveData(Arr, txtProcessNo.Value)
            'If isSaved Then
            '    clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
            '    LoadData(txtProcessNo.Value)
            'End If

            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Dim obj As New clsProcessMaster
            obj.Process_Code = txtProcessNo.Value
            obj.Process_Desc = txtReference.Text

            Dim i As Integer = 0
            Dim objPURDetail As New clsProcessMasterDetail
            obj.arrProcessDetail = New List(Of clsProcessMasterDetail)
            For Each grow As GridViewRowInfo In gv1.Rows
                If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colItem).Value)) > 0 Then
                    objPURDetail = New clsProcessMasterDetail()
                    objPURDetail.Process_Code = txtProcessNo.Value
                    objPURDetail.Item_Code = clsCommon.myCstr(grow.Cells(colItem).Value)
                    objPURDetail.Capacity = clsCommon.myCstr(grow.Cells(colCapacity).Value)
                    obj.arrProcessDetail.Add(objPURDetail)
                End If
            Next
            If obj.arrProcessDetail.Count = 0 Then
                Throw New Exception("Please enter atleast one Item.")
                Exit Sub
            End If
            Dim issaved As Boolean = False
            If btnSave.Text = "Save" Then
                isNewEntry = True
            Else
                isNewEntry = False

            End If
            'If btnDelete.Enabled = True Then
            '    isNewEntry = False
            'Else
            '    isNewEntry = True

            'End If
            issaved = clsProcessMaster.SaveData(obj, trans, isNewEntry)
            If issaved = True Then

                clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                LoadData(txtProcessNo.Value)
                isNewEntry = False

            End If


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub LoadData(ByVal strDoc As String)
        Try
            LoadBlankGrid()
            Dim intCount As Integer = 1
            Dim Arr As List(Of clsProcessMaster) = clsProcessMaster.GetData(strDoc)
            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                isInsideLoadData = True
                For Each objTr As clsProcessMaster In Arr
                    ' gv1.Rows.AddNew()
                    txtProcessNo.Value = objTr.Process_Code
                    txtReference.Text = objTr.Process_Desc
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = intCount
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colItem).Value = objTr.Item_Code
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colCapacity).Value = objTr.Capacaity
                    'intCount += 1
                Next
                isInsideLoadData = False
            End If

            Dim Arr1 As List(Of clsProcessMasterDetail) = clsProcessMasterDetail.LoadData(strDoc)
                    If Arr1 IsNot Nothing AndAlso Arr1.Count > 0 Then
                For Each objDetail As clsProcessMasterDetail In Arr1
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = intCount
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItem).Value = objDetail.Item_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCapacity).Value = objDetail.Capacity
                    intCount += 1
                Next
                btnSave.Text = "Update"
            Else
                btnSave.Text = "Save"

            End If
          
                    gv1.Rows.AddNew()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
                End Try
    End Sub
    Sub BlankAllControls()
        txtProcessNo.Value = ""
        txtReference.Text = ""
    End Sub
    Sub AddNew()
        BlankAllControls()
        LoadBlankGrid()
        isNewEntry = True
        btnSave.Text = "Save"
        btnDelete.Enabled = True
        gv1.Rows.AddNew()
        '  obj(clsProcessMaster())
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    Private Sub txtProcessNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtProcessNo._MYValidating
        Dim ProcessCode As String = clsDBFuncationality.getSingleValue("select distinct Process_Code  from TSPL_process_master where Process_Code='" & txtProcessNo.Value & "'")
        If clsCommon.myLen(ProcessCode) > 0 Or txtProcessNo.Value = "" Then
            Dim qry As String = "select distinct Process_Code as Code,Process_Desc as [Process Desc] from TSPL_process_master"
            txtProcessNo.Value = clsCommon.ShowSelectForm("Process Code", qry, "Code", "", txtProcessNo.Value, "", isButtonClicked)
        End If
        If clsCommon.myLen(ProcessCode) > 0 Or txtProcessNo.Value <> "" Then
            LoadData(txtProcessNo.Value)
            'btnSave.Text = "Update"
            'Else

            '    btnSave.Text = "Save"
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        funDelete()
    End Sub
    Public Sub funDelete()
        If clsCommon.myLen(txtProcessNo.Value) < 1 Then
            clsCommon.MyMessageBoxShow("Please select Code to Delete.")
            Return
        ElseIf myMessages.deleteConfirm Then
            Try
                If clsProcessMaster.DeleteData(txtProcessNo.Value) Then
                    myMessages.delete()
                    AddNew()
                End If
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
    End Sub

   

    ''richa Ticket No BM00000002902 19/06/2014
    Private Sub RMHeadImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RMHeadImport.Click
        Dim gv As New RadGridView()
        'Dim IsNewEntry As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = Nothing

        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Process Code", "Process Desc") Then

            Dim linno As Integer = 0
            Try
                trans = clsDBFuncationality.GetTransactin()
                connectSql.OpenConnection()

                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clsProcessMaster()
                    linno += 1
                    Dim strcode As String = clsCommon.myCstr(grow.Cells("Process Code").Value)
                    If (String.IsNullOrEmpty(strcode)) Or clsCommon.myLen(strcode) > 12 Then
                        Throw New Exception("Length of Process Code should be max. 12 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Process_Code = strcode


                    Dim strdesc As String = clsCommon.myCstr(grow.Cells("Process Desc").Value)
                    If clsCommon.myLen(strdesc) > 500 Then
                        Throw New Exception("Length of Process Desc should be max. 500 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If

                    obj.Process_Desc = strdesc

                    Dim check As Integer = clsDBFuncationality.getSingleValue("select count(*) from TSPL_PROCESS_MASTER WHERE Process_Code ='" + strcode + "'", trans)

                    Dim coll As New Hashtable()
                    Try

                        clsCommon.AddColumnsForChange(coll, "Process_Code", obj.Process_Code)
                        clsCommon.AddColumnsForChange(coll, "Process_Desc", obj.Process_Desc)
                        clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                        clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode, True)

                        If check <= 0 Then
                            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

                            isSaved = isSaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_process_master", OMInsertOrUpdate.Insert, "", trans)
                        Else
                            isSaved = isSaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_process_master", OMInsertOrUpdate.Update, "TSPL_process_master.Process_Code='" + obj.Process_Code + "'", trans)
                        End If
                    Catch ex As Exception

                    End Try
                Next

                trans.Commit()
                ' clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                'clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub RMHeadExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RMHeadExport.Click
        Dim str As String
        str = "Select Process_Code As [Process Code],Process_Desc As [Process Desc] from TSPL_Process_master"
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub RMDetailImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RMDetailImport.Click
        Dim gv As New RadGridView()
        Dim isSaved As Boolean = True
        Dim obj As clsProcessMasterDetail
        Dim StrProcessCode As String = ""
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Process Code", "Item Code", "Capacity") Then
            Dim linno As Integer = 0
            Dim trans As SqlTransaction = Nothing
            Try
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For i As Integer = 0 To gv.Rows.Count - 1
                    clsDBFuncationality.ExecuteNonQuery("DELETE FROM TSPL_PROCESS_DETAIL where Process_Code = '" & clsCommon.myCstr(gv.Rows(i).Cells("Process Code").Value) & "'", trans)
                Next
                For Each grow As GridViewRowInfo In gv.Rows
                    obj = New clsProcessMasterDetail
                    linno += 1
                    StrProcessCode = clsCommon.myCstr(grow.Cells("Process Code").Value)
                    If clsCommon.myLen(StrProcessCode) <= 0 Then
                        Throw New Exception("Please Fill Process Code")
                    End If

                    If clsCommon.myLen(StrProcessCode) > 0 Then
                        Dim qry As String = "select Process_Code from TSPL_PROCESS_MASTER where Process_Code='" + StrProcessCode + "'"
                        Dim process_Code As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                        If clsCommon.myLen(process_Code) <= 0 Then
                            Throw New Exception("Please Fill Process Code For Process Master [" + StrProcessCode + "] Or Make Process Head Entry First")
                        End If
                    End If

                    Dim strcode As String = clsCommon.myCstr(grow.Cells("Process Code").Value)
                    If (String.IsNullOrEmpty(strcode)) Or clsCommon.myLen(strcode) > 12 Then
                        Throw New Exception("Length of Process Code should be max. 12 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Process_Code = strcode

                    Dim stritemcode As String = clsCommon.myCstr(grow.Cells("Item Code").Value)
                    If (String.IsNullOrEmpty(stritemcode)) Or clsCommon.myLen(stritemcode) > 100 Then
                        Throw New Exception("Length of Item Code should be max. 100 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Item_Code = stritemcode

                    Dim strcapacity As String = clsCommon.myCstr(grow.Cells("Capacity").Value)
                    If (String.IsNullOrEmpty(strcapacity)) Or clsCommon.myLen(strcapacity) > 100 Then
                        Throw New Exception("Length of Capacity should be max. 100 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Capacity = strcapacity

                    Dim coll As New Hashtable()
                    Try
                        clsCommon.AddColumnsForChange(coll, "Process_Code", obj.Process_Code)
                        clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                        clsCommon.AddColumnsForChange(coll, "Capacity", obj.Capacity)
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PROCESS_DETAIL", OMInsertOrUpdate.Insert, "", trans)


                    Catch ex As Exception

                    End Try
                Next
                trans.Commit()

                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)

            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub RMDetailExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RMDetailExport.Click
        'Dim strDetail As String

        Qry = "select count(*) from TSPL_PROCESS_MASTER"
        Dim check As Integer = clsDBFuncationality.getSingleValue(Qry)

        If check > 0 Then
            Qry = "Select TSPL_PROCESS_DETAIL.Process_Code As [Process Code],Item_Code As [Item Code],Capacity as Capacity from TSPL_Process_master left Outer Join TSPL_PROCESS_DETAIL on TSPL_Process_master.Process_Code=TSPL_PROCESS_DETAIL.Process_Code"
        End If
        transportSql.ExporttoExcel(Qry, Me)
    End Sub

End Class
