Imports common
Imports System.IO

Public Class frmLockLoctionUserwise
#Region "Variables"
    Dim IsmanualBatchNomandatory As Double = 0
    Public strLocCode As String = ""
    Public strLocname As String = ""
    Public strModule As String = ""
    Public strTransName As String = Nothing
    Public blnLocationwsie As Boolean = False
    Const ColSNo As String = "COLSNO"
    Const ColSelect As String = "ColSelect"
    Const ColUserCode As String = "ColUserCode"
    Const ColUserName As String = "ColUserName"
    Const colToDate As String = "colToDate"
    Dim intShelfLife As Integer
    Public arr As List(Of clsLockTransactionLocationUserwise) = Nothing
    Public arr1 As List(Of clsLockTransactionLocationSegmentUserwise) = Nothing
    Const ReportID As String = "BatchInvIn"
    Public isCencelButtonClicked As Boolean = False
    Public isInsideLoadData As Boolean = False
    Public isCellValueChangedOpen As Boolean = False

#End Region

    Private Sub FrmSerializeItemIn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'IsmanualBatchNomandatory = clsDBFuncationality.getSingleValue("select Is_ManualBatchNo_Mandatory from TSPL_INV_PARAMETERS ")
        IsmanualBatchNomandatory = 1
        isInsideLoadData = True
        lblLocationCode.Text = strLocCode
        lblILocName.Text = strLocname
        lblModule.Text = strModule
        lblModule.Text = strTransName
        LoadBlankGrid()
        If blnLocationwsie Then
            arr = clsLockTransactionLocationUserwise.GetData(strLocCode, strModule, strTransName)
        Else
            arr1 = clsLockTransactionLocationSegmentUserwise.GetData(strLocCode, strModule, strTransName)
        End If



        If blnLocationwsie Then
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each obj As clsLockTransactionLocationUserwise In arr
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColUserCode).Value = obj.User_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColUserName).Value = obj.User_Name
                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColSelect).Value = IIf(obj.Status = 1, True, False)
                    If obj.ToDate <> "1900-01-01" Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colToDate).Value = obj.ToDate
                    End If
                Next
            End If
        Else
            If arr1 IsNot Nothing AndAlso arr1.Count > 0 Then
                For Each obj As clsLockTransactionLocationSegmentUserwise In arr1
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColUserCode).Value = obj.User_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColUserName).Value = obj.User_Name
                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColSelect).Value = IIf(obj.Status = 1, True, False)
                    If obj.ToDate <> "1900-01-01" Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colToDate).Value = obj.ToDate
                    End If
                Next
            End If

            End If
            'gv1.Rows.AddNew()
            'RefeshSNO()
            If gv1.RowCount > 0 Then
                gv1.CurrentRow = gv1.Rows(0)
                gv1.CurrentColumn = gv1.Columns(ColUserCode)
            End If
            gv1.Focus()

            isInsideLoadData = False
    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim Lock As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        Lock.FormatString = ""
        Lock.HeaderText = "Lock"
        Lock.Name = ColSelect
        Lock.Width = 71
        Lock.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(Lock)

        Dim repoLocationName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLocationName.FormatString = ""
        repoLocationName.HeaderText = "User Code"
        repoLocationName.Name = ColUserCode
        repoLocationName.ReadOnly = False
        repoLocationName.IsVisible = True
        repoLocationName.Width = 150
        gv1.MasterTemplate.Columns.Add(repoLocationName)

        Dim repoManualbatch As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoManualbatch.FormatString = ""
        repoManualbatch.HeaderText = "User Name"
        repoManualbatch.Name = ColUserName
        repoManualbatch.ReadOnly = False
        repoManualbatch.IsVisible = True
        repoManualbatch.Width = 150
        gv1.MasterTemplate.Columns.Add(repoManualbatch)

        Dim repoExpDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoExpDate.Format = DateTimePickerFormat.Custom
        repoExpDate.CustomFormat = "dd/MM/yyyy"
        repoExpDate.FormatString = "{0:dd/MM/yyyy}"
        repoExpDate.HeaderText = "Lock Date"
        repoExpDate.Name = colToDate
        repoExpDate.ReadOnly = False
        repoExpDate.IsVisible = True
        repoExpDate.Width = 80
        gv1.MasterTemplate.Columns.Add(repoExpDate)

        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40

        gv1.AllowDeleteRow = True
        ReStoreGridLayout()

    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        OKPressed()
    End Sub

    Sub OKPressed()
        Try
            If AllowToSave() Then
                If blnLocationwsie Then
                    arr = New List(Of clsLockTransactionLocationUserwise)
                    For ii As Integer = 0 To gv1.RowCount - 1
                        If gv1.Rows(ii).Cells(ColSelect).Value = False Then
                            Dim obj As clsLockTransactionLocationUserwise = New clsLockTransactionLocationUserwise()
                            obj.Status = clsCommon.myCstr(IIf(gv1.Rows(ii).Cells(ColSelect).Value = True, 1, 0))
                            obj.User_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(ColUserCode).Value)
                            obj.User_Name = clsCommon.myCstr(gv1.Rows(ii).Cells(ColUserName).Value)
                            obj.ToDate = clsCommon.myCDate(gv1.Rows(ii).Cells(colToDate).Value)
                            If gv1.Rows(ii).Cells(ColSelect).Value = False Then
                                arr.Add(obj)
                            End If
                        End If
                    Next
                Else
                    arr1 = New List(Of clsLockTransactionLocationSegmentUserwise)
                    For ii As Integer = 0 To gv1.RowCount - 1
                        If gv1.Rows(ii).Cells(ColSelect).Value = False Then
                            Dim obj As clsLockTransactionLocationSegmentUserwise = New clsLockTransactionLocationSegmentUserwise()
                            obj.Status = clsCommon.myCstr(IIf(gv1.Rows(ii).Cells(ColSelect).Value = True, 1, 0))
                            obj.User_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(ColUserCode).Value)
                            obj.User_Name = clsCommon.myCstr(gv1.Rows(ii).Cells(ColUserName).Value)
                            obj.ToDate = clsCommon.myCDate(gv1.Rows(ii).Cells(colToDate).Value)

                            If gv1.Rows(ii).Cells(ColSelect).Value = False Then
                                arr1.Add(obj)
                            End If
                        End If
                    Next
                End If
               
                Me.Close()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Function AllowToSave() As Boolean
        Dim dblTotQty As Double = 0
        For ii As Integer = 0 To gv1.RowCount - 1
            If gv1.Rows(ii).Cells(ColSelect).Value = False Then
                If clsCommon.myLen(gv1.Rows(ii).Cells(colToDate).Value) <= 0 Then
                    Throw New Exception("Please enter To Date at Row No" + clsCommon.myCstr(ii + 1))
                End If
            End If
        Next    
        Return True
    End Function

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        If clsCommon.myLen(ReportID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If

            ''richa agarwal regarding memory leakage
            obj = Nothing
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
    End Sub

    Private Sub FrmSerializeItemIn_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F5 Then
            OKPressed()
        ElseIf e.KeyCode = Keys.Escape Then
            CancelPressed()
        End If
    End Sub

    Sub CancelPressed()
        isCencelButtonClicked = True
        Me.Close()
    End Sub

    Private Sub gv1_UserDeletedRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow
        'RefeshSNO()
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
       
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Sub RefeshSNO()
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(ColSNo).Value = ii
        Next
    End Sub


    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'For ii As Integer = 0 To gv1.RowCount - 1
        '    If ii < dblqty Then
        '        If clsCommon.myLen(gv1.Rows(ii).Cells(colBatchNo).Value) <= 0 Then
        '            gv1.Rows(ii).Cells(colBatchNo).Value = clsItemMaster.GetItemSerialCounter(strItemCode, Nothing)
        '        End If
        '    End If
        'Next
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton2.Click
        CancelPressed()
    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        'If gv1.RowCount > 0 Then
        '    Dim intCurrRow As Integer = gv1.CurrentRow.Index
        '    gv1.CurrentRow.Cells(ColSNo).Value = clsCommon.myCdbl(intCurrRow + 1)
        '    If intCurrRow = gv1.Rows.Count - 1 Then
        '        gv1.Rows.AddNew()
        '        gv1.CurrentRow = gv1.Rows(intCurrRow)
        '    End If
        'End If
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    'If e.Column Is gv1.Columns(colMfgDate) AndAlso intShelfLife > 0 Then
                    '    gv1.CurrentRow.Cells(colExpDate).Value = clsCommon.myCDate(gv1.CurrentRow.Cells(colMfgDate).Value).AddDays(intShelfLife)
                    'End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
End Class
