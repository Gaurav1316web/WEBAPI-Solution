Imports common
Imports System
Imports Telerik.WinControls.UI
Imports System.Net.Mail
Imports System.Net
Imports Telerik.WinControls
Imports System.IO
Imports System.Xml
Imports System.Data.SqlClient

Public Class frmDeductionMapping
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim ErrorControl As clsErrorControl = New clsErrorControl()
    Dim isNewEntry As Boolean = True

    Const colTRCode As String = "colTRCode"
    Const colSNo As String = "colSNo"
    Const codDedCode As String = "codDedCode"
    Const codDedName As String = "codDedName"
    Const colType As String = "colType"
    Const colPer As String = "colPer"

    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
#End Region

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub FrmPriceChartMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Reset()
        gv1.Rows.AddNew()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        gv1.Enabled = True
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.DeductionMapping)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow(Me, "Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub Reset()
        txtCode.Value = ""
        txtStartDate.Value = clsCommon.GETSERVERDATE()
        txtEndDate.Value = txtStartDate.Value.AddMonths(1)
        txtDate.Value = txtStartDate.Value
        UsLock1.Status = ERPTransactionStatus.Pending
        txtDesc.Text = ""
        txtMCC.arrValueMember = Nothing
        txtVLC.arrValueMember = Nothing
        chkApplyOnVLC.Checked = False
        SetVLCEnables()
        chkRoundDownAmount.Checked = True
        txtCode.MyReadOnly = False
        btndelete.Enabled = False
        btnPost.Enabled = False
        btnsave.Enabled = True
        loadBlankGrid()
        isNewEntry = True
    End Sub

    Private Function GetItemType() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Qty"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Amt"
        dt.Rows.Add(dr)

        Return dt
    End Function

    Sub loadBlankGrid()
        Try
            gv1.Rows.Clear()
            gv1.Columns.Clear()

            Dim repoTextBox As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoTextBox.HeaderText = "TR Code"
            repoTextBox.Name = colTRCode
            repoTextBox.Width = 100
            repoTextBox.ReadOnly = True
            repoTextBox.IsVisible = False
            gv1.MasterTemplate.Columns.Add(repoTextBox)

            Dim repoNumBox As GridViewDecimalColumn = New GridViewDecimalColumn()
            repoNumBox.Name = colSNo
            repoNumBox.Width = 50
            repoNumBox.DecimalPlaces = 0
            repoNumBox.Minimum = 0
            repoNumBox.Step = 0
            repoNumBox.ShowUpDownButtons = False
            repoNumBox.ReadOnly = True
            repoNumBox.HeaderText = "SNO"
            gv1.MasterTemplate.Columns.Add(repoNumBox)

            repoTextBox = New GridViewTextBoxColumn()
            repoTextBox.HeaderImage = Global.ERP.My.Resources.Resources.search4
            repoTextBox.TextImageRelation = TextImageRelation.TextBeforeImage
            repoTextBox.HeaderText = "Deduction Code"
            repoTextBox.Name = codDedCode
            repoTextBox.Width = 100
            repoTextBox.ReadOnly = False
            gv1.MasterTemplate.Columns.Add(repoTextBox)

            repoTextBox = New GridViewTextBoxColumn()
            repoTextBox.HeaderText = "Deduction Name"
            repoTextBox.Name = codDedName
            repoTextBox.Width = 200
            repoTextBox.ReadOnly = True
            gv1.MasterTemplate.Columns.Add(repoTextBox)

            Dim repoRowType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
            repoRowType.FormatString = ""
            repoRowType.HeaderText = "Apply On"
            repoRowType.Name = colType
            repoRowType.Width = 100
            repoRowType.ReadOnly = False
            repoRowType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            repoRowType.DataSource = GetItemType()
            repoRowType.ValueMember = "Code"
            repoRowType.DisplayMember = "Code"
            gv1.MasterTemplate.Columns.Add(repoRowType)

            repoNumBox = New GridViewDecimalColumn()
            repoNumBox.Name = colPer
            repoNumBox.Width = 100
            repoNumBox.DecimalPlaces = 2
            repoNumBox.Minimum = 0
            repoNumBox.Maximum = 100
            repoNumBox.Step = 0
            repoNumBox.ShowUpDownButtons = False
            repoNumBox.ReadOnly = False
            repoNumBox.HeaderText = "Percent"
            gv1.MasterTemplate.Columns.Add(repoNumBox)

            gv1.AllowDeleteRow = True
            gv1.AllowAddNewRow = False
            gv1.ShowGroupPanel = False
            gv1.AllowColumnReorder = False
            gv1.AllowRowReorder = False
            gv1.EnableSorting = False
            gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
            gv1.MasterTemplate.ShowRowHeaderColumn = False
            gv1.TableElement.TableHeaderHeight = 40
            gv1.AutoSizeRows = False
            gv1.AllowRowReorder = True
            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Function AllowToSave() As Boolean
        Try
            If txtMCC.arrValueMember Is Nothing OrElse txtMCC.arrValueMember.Count <= 0 Then
                txtMCC.Focus()
                txtMCC.Select()
                ErrorControl.SetError(txtMCC, "Please select MCC")
                Throw New Exception("Please select Price MCC")
            Else
                ErrorControl.ResetError(txtMCC)
            End If

            For ii As Integer = 0 To gv1.RowCount - 1
                If clsCommon.myLen(gv1.Rows(ii).Cells(codDedCode).Value) > 0 Then
                    For jj As Integer = 0 To gv1.RowCount - 1
                        If clsCommon.myLen(gv1.Rows(ii).Cells(codDedCode).Value) > 0 Then
                            If ii = jj Then
                                Continue For
                            End If
                            If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(codDedCode).Value), clsCommon.myCstr(gv1.Rows(jj).Cells(codDedCode).Value)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colType).Value), clsCommon.myCstr(gv1.Rows(jj).Cells(colType).Value)) = CompairStringResult.Equal Then
                                Throw New Exception("Repeated Deduction Code-" + clsCommon.myCstr(gv1.Rows(ii).Cells(codDedCode).Value) + " and Type" + clsCommon.myCstr(gv1.Rows(ii).Cells(colType).Value) + Environment.NewLine + "At Row No" + clsCommon.myCstr(ii + 1) + " And " + clsCommon.myCstr(jj + 1))
                            End If
                        End If
                    Next
                End If
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Sub SaveData()
        Try
            If AllowToSave() Then
                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(MyBase.Form_ID, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
                Dim obj As New clsDeductionMappingHead()
                obj.Doc_Code = txtCode.Value
                obj.Doc_Date = txtDate.Value
                obj.Description = txtDesc.Text
                obj.Start_Date = txtStartDate.Value
                If txtEndDate.Checked Then
                    obj.End_Date = txtEndDate.Value
                End If
                obj.Is_Round_Down = chkRoundDownAmount.Checked
                If chkApplyOnVLC.Checked Then
                    obj.ArrVLC = txtVLC.arrValueMember
                End If
                obj.ArrMCC = txtMCC.arrValueMember
                
                obj.Arr = New List(Of clsDeductionMappingDetail)
                For ii As Integer = 0 To gv1.RowCount - 1
                    If clsCommon.myLen(gv1.Rows(ii).Cells(codDedCode).Value) > 0 Then
                        Dim objtr As New clsDeductionMappingDetail
                        objtr.SNo = ii + 1
                        objtr.Deduction_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(codDedCode).Value)
                        objtr.Type = clsCommon.myCstr(gv1.Rows(ii).Cells(colType).Value)
                        objtr.Per = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPer).Value)
                        obj.Arr.Add(objtr)
                    End If
                Next
                If obj.Arr Is Nothing OrElse obj.Arr.Count <= 0 Then
                    Throw New Exception("Please enter deduction")
                End If
                If obj.SaveData(obj, isNewEntry) Then
                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(obj.Doc_Code, NavigatorType.Current)
                End If
            Else
                txtCode.MyReadOnly = False
                btndelete.Enabled = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub FrmPriceChartMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            btnnew.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            btnsave.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            btndelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            btnPost.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If btnReverse.Visible Then
                btnReverse.Visible = False
            Else
                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SIRC"
                frm.strCode = "SIReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnReverse.Visible = True
                End If
            End If
        End If
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("Code not found to delete")
            End If
            If clsCommon.MyMessageBoxShow(Me, "Delete the current mapping" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                clsDeductionMappingHead.DeleteData(txtCode.Value)
                clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully", Me.Text)
                Reset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try
            isInsideLoadData = True
            Reset()
            Dim obj As clsDeductionMappingHead = clsDeductionMappingHead.GetData(strCode, NavType, Nothing)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Doc_Code) > 0 Then
                isNewEntry = False
                If obj.Post_Status = ERPTransactionStatus.Approved Then
                    btndelete.Enabled = False
                    btnPost.Enabled = False
                    btnsave.Enabled = False
                Else
                    btndelete.Enabled = True
                    btnPost.Enabled = True
                    btnsave.Enabled = True
                End If
                UsLock1.Status = obj.Post_Status
                txtCode.Value = obj.Doc_Code
                txtDate.Value = obj.Doc_Date
                txtStartDate.Value = obj.Start_Date
                If obj.End_Date IsNot Nothing Then
                    txtEndDate.Checked = True
                    txtEndDate.Value = obj.End_Date
                Else
                    txtEndDate.Checked = False
                End If
                txtMCC.arrValueMember = obj.ArrMCC
                txtVLC.arrValueMember = obj.ArrVLC
                txtDesc.Text = obj.Description
                chkRoundDownAmount.Checked = obj.Is_Round_Down

                chkApplyOnVLC.Checked = IIf(obj.ArrVLC IsNot Nothing AndAlso obj.ArrVLC.Count > 0, True, False)
                SetVLCEnables()
                LoadDetailData(obj.Arr)
                gv1.Rows.AddNew()
                txtCode.MyReadOnly = True
            End If
        Catch ex As Exception
            isNewEntry = True
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Sub LoadDetailData(ByVal Arr As List(Of clsDeductionMappingDetail))
        If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
            For Each objtr As clsDeductionMappingDetail In Arr
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(gv1.Rows.Count - 1)
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTRCode).Value = objtr.TR_Code
                gv1.Rows(gv1.Rows.Count - 1).Cells(colSNo).Value = gv1.Rows.Count
                gv1.Rows(gv1.Rows.Count - 1).Cells(codDedCode).Value = objtr.Deduction_Code
                gv1.Rows(gv1.Rows.Count - 1).Cells(codDedName).Value = ClsDeductionMaster.GetName(objtr.Deduction_Code, Nothing)
                gv1.Rows(gv1.Rows.Count - 1).Cells(colType).Value = objtr.Type
                gv1.Rows(gv1.Rows.Count - 1).Cells(colPer).Value = objtr.Per
            Next
        End If
    End Sub

    Private Sub fndcode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        LoadData(txtCode.Value, NavType)
    End Sub

    Private Sub fndcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Dim qry As String = "select  Doc_Code as DocCode,Doc_Date as DocDate,Start_Date as StartDate,End_Date as EndDate,case when Post_Status=1 then 'Approved' else 'Pending' end as Status from TSPL_DEDUCTION_MAPPING_HEAD "
        Dim whrcls As String = ""
        txtCode.Value = clsCommon.ShowSelectForm("dedMapMainfd", qry, "DocCode", whrcls, txtCode.Value, "", isButtonClicked, "TSPL_DEDUCTION_MAPPING_HEAD.Doc_Date")
        If clsCommon.myLen(txtCode.Value) > 0 Then
            LoadData(txtCode.Value, NavigatorType.Current)
        Else
            Reset()
        End If
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        Reset()
        gv1.Rows.AddNew()
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("Code not found to post")
            End If
            If clsCommon.MyMessageBoxShow("Post the current plan" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                clsDeductionMappingHead.PostData(txtCode.Value)
                clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                LoadData(txtCode.Value, NavigatorType.Current)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gvTS_UserDeletingRow(sender As Object, e As GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow

        If Not myMessages.deleteConfirm() Then
            e.Cancel = True
        End If
    End Sub

    Private Sub gvTS_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow.Cells(colSNo).Value = intCurrRow + 1
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(codDedCode) Then
                        OpenDeductionCodeList(False)
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            isCellValueChangedOpen = False
        End Try
    End Sub

    Sub OpenDeductionCodeList(ByVal isButtonClick As Boolean)
        Try
            Dim qry As String = "select Code,Description from TSPL_DEDUCTION_MASTER"
            gv1.CurrentRow.Cells(codDedCode).Value = clsCommon.ShowSelectForm("DedFnder@dedMap", qry, "Code", "", clsCommon.myCstr(gv1.CurrentRow.Cells(codDedCode).Value), "", isButtonClick)
            gv1.CurrentRow.Cells(codDedName).Value = ClsDeductionMaster.GetName(clsCommon.myCstr(gv1.CurrentRow.Cells(codDedCode).Value), Nothing)

        Catch ex As Exception
            gv1.CurrentRow.Cells(codDedCode).Value = ""
            gv1.CurrentRow.Cells(codDedName).Value = ""
        End Try
    End Sub

    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Try
            Dim qry As String = "Select Unit_Code as Code, Unit_Desc as Description from TSPL_UNIT_MASTER LEFT OUTER JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_UNIT_MASTER.Unit_Code"
            gv1.CurrentRow.Cells(colType).Value = clsCommon.ShowSelectForm("UOMFND@PriceMaster", qry, "Code", "TSPL_ITEM_UOM_DETAIL.Item_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(codDedCode).Value) + "'", clsCommon.myCstr(gv1.CurrentRow.Cells(colType).Value), "Code", isButtonClick)
        Catch ex As Exception
            gv1.CurrentRow.Cells(colType).Value = ""
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Dim dgv As New RadGridView
        Me.Controls.Add(dgv)
        Try
            If Not isNewEntry Then
                Throw New Exception("Please first select New Button")
            End If
            loadBlankGrid()
            If transportSql.importExcel(dgv, gv1.Columns(colSNo).HeaderText, gv1.Columns(codDedCode).HeaderText, gv1.Columns(codDedName).HeaderText, gv1.Columns(colType).HeaderText, gv1.Columns(colPer).HeaderText) Then
                Dim LineNo As Integer = 0
                Try
                    Dim arr As New List(Of clsDeductionMappingDetail)
                    clsCommon.ProgressBarPercentShow()
                    For Each dgrv As GridViewRowInfo In dgv.Rows
                        clsCommon.ProgressBarPercentUpdate((dgrv.Index + 1) * 100 / (dgv.Rows.Count + 1), "Validating  : " & (dgrv.Index + 1) & "/" & dgv.Rows.Count & "")
                        LineNo += 1
                        Dim obj As New clsDeductionMappingDetail
                        obj.SNo = LineNo
                        obj.Deduction_Code = clsCommon.myCstr(dgrv.Cells(gv1.Columns(codDedCode).HeaderText).Value)
                        If clsCommon.myLen(obj.Deduction_Code) <= 0 Then
                            Continue For
                        End If
                        Dim qry As String = "select Code from TSPL_DEDUCTION_MASTER where Code='" + obj.Deduction_Code + "'"
                        obj.Deduction_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                        If clsCommon.myLen(obj.Deduction_Code) <= 0 Then
                            Throw New Exception("No a valid deduction code")
                        End If

                        obj.Type = clsCommon.myCstr(dgrv.Cells(gv1.Columns(colType).HeaderText).Value)
                        If clsCommon.myLen(obj.Type) <= 0 Then
                            Throw New Exception("Please enter UOM")
                        End If
                        If clsCommon.CompairString(obj.Type, "Qty") = CompairStringResult.Equal Then
                            obj.Type = "Qty"
                        ElseIf clsCommon.CompairString(obj.Type, "Amt") = CompairStringResult.Equal Then
                            obj.Type = "Amt"
                        Else
                            Throw New Exception("Not a valid Apply On (Qty/Amt)")
                        End If
                        obj.Per = clsCommon.myCdbl(dgrv.Cells(gv1.Columns(colPer).HeaderText).Value)
                        If obj.Per < 0 OrElse obj.Per > 100 Then
                            Throw New Exception("Percent should be 0-100")
                        End If
                        arr.Add(obj)
                    Next
                    Try
                        isInsideLoadData = True
                        LoadDetailData(arr)
                    Catch ex As Exception
                        Throw New Exception(ex.Message)
                    Finally
                        isInsideLoadData = True
                    End Try
                    clsCommon.ProgressBarPercentHide()

                Catch ex As Exception
                    clsCommon.ProgressBarPercentHide()
                    Throw New Exception("Error at Line no" + clsCommon.myCstr(LineNo) + Environment.NewLine + ex.Message)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            Me.Controls.Remove(dgv)
        End Try
    End Sub

    Private Sub RadMenuItem3_Click(sender As Object, e As EventArgs) Handles RadMenuItem3.Click
        Try
            'Dim arrHeader As List(Of String) = New List(Of String)()
            'arrHeader.Add("Price Plan Code : " + txtCode.Value)
            Dim sfd As SaveFileDialog = New SaveFileDialog()
            Dim filePath As String
            sfd.FileName = Me.Text
            sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                filePath = sfd.FileName
            Else
                Exit Sub
            End If
            'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), False)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
            transportSql.QuickExportToExcel(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), False, Nothing, True)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem4_Click(sender As Object, e As EventArgs) Handles RadMenuItem4.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(MyBase.Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
            MessageBox.Show(Me, err.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem5_Click(sender As Object, e As EventArgs) Handles RadMenuItem5.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information")
    End Sub

    Private Sub txtMCC__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
        Dim qry As String = "select MCC_Code as Code ,MCC_NAME as Name from TSPL_MCC_MASTER"
        txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm(False, "PCPDMCC", qry, "Code", "", txtMCC.arrValueMember, Nothing)
        txtVLC.arrValueMember = Nothing
    End Sub

    Private Sub txtVLC__My_Click(sender As Object, e As EventArgs) Handles txtVLC._My_Click
        If txtMCC.arrValueMember Is Nothing OrElse txtMCC.arrValueMember.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select MCC", Me.Text)
            Exit Sub
        End If

        Dim qry As String = "select VLC_Code as Code ,VLC_Name as Name from TSPL_VLC_MASTER_HEAD  where mcc in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") and TSPL_VLC_MASTER_HEAD.Active=1"
        txtVLC.arrValueMember = clsCommon.ShowMultipleSelectForm("PCPDMCC", qry, "Code", "", txtVLC.arrValueMember, Nothing)
    End Sub

    Private Sub chkBackCalculation_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkApplyOnVLC.ToggleStateChanged
        SetVLCEnables()
    End Sub

    Sub SetVLCEnables()
        txtVLC.Enabled = chkApplyOnVLC.Checked
        If Not chkApplyOnVLC.Checked Then
            txtVLC.arrValueMember = Nothing
        End If
    End Sub

    Private Sub btnReverse_Click(sender As Object, e As EventArgs) Handles btnReverse.Click
        Try
            If clsCommon.myLen(txtCode.Value) > 0 Then
                If Not UsLock1.Status = ERPTransactionStatus.Approved Then
                    Throw New Exception("Mapping code should be approved")
                End If
                Dim obj As New clsDeductionMappingHead
                obj.ReverseAndUnpost(txtCode.Value)
                LoadData(txtCode.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class