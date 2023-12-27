Imports System.Data.SqlClient
Imports common
Imports System.IO

Public Class frmDCSFinancialEntry
    Inherits FrmMainTranScreen
#Region "Variables"
    Public Const colSNo1 As String = "colSNo1"
    Public Const colHeadCode1 As String = "colHeadCode1"
    Public Const colHeadName1 As String = "colHeadName1"
    Public Const colSubAmount1 As String = "colSubAmount1"
    Public Const colAmount1 As String = "colAmount1"

    Public Const colSNo2 As String = "colSNo2"
    Public Const colHeadCode2 As String = "colHeadCode2"
    Public Const colHeadName2 As String = "colHeadName2"
    Public Const colSubAmount2 As String = "colSubAmount2"
    Public Const colAmount2 As String = "colAmount2"

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Dim IsinsideLoadData As Boolean = False
    Dim Qry As String



#End Region
    Public Sub New()
        InitializeComponent()
    End Sub
    Private Sub FrmVLCDataUploaderManual_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        Reset()


        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Transaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Transaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N New Transaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        LoadType()
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        Me.Focus()
        txtdate.Focus()
    End Sub
    Sub LoadType()
        IsinsideLoadData = True

        cboType.DataSource = clsDCSFinancialHead.GetHeadType()
        cboType.ValueMember = "Code"
        cboType.DisplayMember = "Name"

        IsinsideLoadData = False
    End Sub
    Private Sub FrmVLCDataUploaderManual_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclose.Enabled Then
            CloseForm()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnclose.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine +
                      "========Table Name=========" + Environment.NewLine +
                      "TSPL_DCS_FINANCIAL_ENTRY" + Environment.NewLine +
                      "TSPL_DCS_FINANCIAL_ENTRY_DETAIL" + Environment.NewLine)
        End If
    End Sub
    Private Sub CloseForm()
        Me.Close()
        GC.Collect()
    End Sub
    Sub loadBlankGrid()
        gvItem.Rows.Clear()
        gvItem.Columns.Clear()
        gvItem.DataSource = Nothing
        gvItem.MasterTemplate.SummaryRowsBottom.Clear()

        Dim recoTxt As New GridViewTextBoxColumn()
        Dim repoNum As New GridViewDecimalColumn

        recoTxt = New GridViewTextBoxColumn()
        recoTxt.FormatString = ""
        recoTxt.HeaderText = "SNo 1"
        recoTxt.Name = colSNo1
        recoTxt.Width = 60
        recoTxt.ReadOnly = True
        recoTxt.IsVisible = False
        recoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.Columns.Add(recoTxt)

        recoTxt = New GridViewTextBoxColumn()
        recoTxt.FormatString = ""
        recoTxt.HeaderText = "Head Code 1"
        recoTxt.Name = colHeadCode1
        recoTxt.ReadOnly = True
        recoTxt.IsVisible = False
        recoTxt.Width = 100
        recoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvItem.Columns.Add(recoTxt)

        recoTxt = New GridViewTextBoxColumn()
        recoTxt.FormatString = ""
        recoTxt.HeaderText = "Head Name 1"
        recoTxt.Name = colHeadName1
        recoTxt.ReadOnly = True
        recoTxt.IsVisible = True
        recoTxt.Width = 100
        recoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvItem.Columns.Add(recoTxt)


        repoNum = New GridViewDecimalColumn()
        repoNum.FormatString = ""
        repoNum.HeaderText = "Sub Amount"
        repoNum.Name = colSubAmount1
        repoNum.Width = 100
        repoNum.FormatString = "{0:n2}"
        repoNum.ReadOnly = False
        repoNum.IsVisible = True
        repoNum.ShowUpDownButtons = False
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.Columns.Add(repoNum)

        repoNum = New GridViewDecimalColumn
        repoNum.FormatString = ""
        repoNum.HeaderText = "Amount"
        repoNum.Name = colAmount1
        repoNum.Width = 100
        repoNum.FormatString = "{0:n2}"
        repoNum.ReadOnly = False
        repoNum.IsVisible = True
        repoNum.ShowUpDownButtons = False
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.Columns.Add(repoNum)





        recoTxt = New GridViewTextBoxColumn()
        recoTxt.FormatString = ""
        recoTxt.HeaderText = "SNo 2"
        recoTxt.Name = colSNo2
        recoTxt.Width = 60
        recoTxt.ReadOnly = True
        recoTxt.IsVisible = False
        recoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.Columns.Add(recoTxt)

        recoTxt = New GridViewTextBoxColumn()
        recoTxt.FormatString = ""
        recoTxt.HeaderText = "Head Code 2"
        recoTxt.Name = colHeadCode2
        recoTxt.ReadOnly = True
        recoTxt.IsVisible = False
        recoTxt.Width = 200
        recoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvItem.Columns.Add(recoTxt)

        recoTxt = New GridViewTextBoxColumn()
        recoTxt.FormatString = ""
        recoTxt.HeaderText = "Head Name 2"
        recoTxt.Name = colHeadName2
        recoTxt.ReadOnly = True
        recoTxt.IsVisible = True
        recoTxt.Width = 200
        recoTxt.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvItem.Columns.Add(recoTxt)


        repoNum = New GridViewDecimalColumn()
        repoNum.FormatString = ""
        repoNum.HeaderText = "Sub Amount"
        repoNum.Name = colSubAmount2
        repoNum.Width = 100
        repoNum.FormatString = "{0:n2}"
        repoNum.ReadOnly = False
        repoNum.IsVisible = True
        repoNum.ShowUpDownButtons = False
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.Columns.Add(repoNum)

        repoNum = New GridViewDecimalColumn
        repoNum.FormatString = ""
        repoNum.HeaderText = "Amount"
        repoNum.Name = colAmount2
        repoNum.Width = 100
        repoNum.FormatString = "{0:n2}"
        repoNum.ShowUpDownButtons = False
        repoNum.ReadOnly = False
        repoNum.IsVisible = True
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.Columns.Add(repoNum)


        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As New GridViewSummaryItem(colAmount1, "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        Dim item2 As New GridViewSummaryItem(colAmount2, "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)

        gvItem.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)


        gvItem.AllowAddNewRow = False
        gvItem.AllowDeleteRow = True
        gvItem.AllowRowReorder = False
        gvItem.ShowGroupPanel = False
        gvItem.EnableFiltering = True
        gvItem.ShowFilteringRow = True
        gvItem.EnableSorting = False
        gvItem.EnableGrouping = False
        gvItem.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        'gvItem.GridBehavior = New MyBehavior()
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FrmVLCDataUploaderManual)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        btnPost.Visible = MyBase.isPostFlag
    End Sub
    Sub Reset()
        loadBlankGrid()
        Dim dt As Date = clsCommon.GETSERVERDATE()

        txtDocumentNo.Value = ""
        txtDCS.Value = ""
        lblDCSName.Text = ""
        txtFiscalYear.Value = ""
        lblFiscalYear.Text = ""
        cboType.SelectedValue = ""
        txtRemarks.Text = ""
        txtdate.Value = dt
        txtDocumentNo.MyReadOnly = False
        btnsave.Text = "Save"

        btndelete.Enabled = False
        btnsave.Enabled = True
        btnPost.Enabled = False
        txtdate.Focus()


        isNewEntry = True
        IsinsideLoadData = False
        lblPending.Status = ERPTransactionStatus.Pending

    End Sub
    Private Function AllowToSave() As Boolean
        If AllowFutureDateTransaction(txtdate.Value, Nothing) = False Then
            txtdate.Focus()
            Return False
        End If
        If clsCommon.myLen(txtDCS.Value) <= 0 Then
            txtDCS.Focus()
            Throw New Exception("Please select DCS")
        End If

        If clsCommon.myLen(txtFiscalYear.Value) <= 0 Then
            txtFiscalYear.Focus()
            Throw New Exception("Please select Fiscal Year")
        End If
        If clsCommon.myLen(cboType.SelectedValue) <= 0 Then
            cboType.Focus()
            Throw New Exception("Please select Type")
        End If

        UpdateAllRow()
        Return True
    End Function
    Sub SaveData()
        Try
            If AllowToSave() Then
                Dim obj As New clsDCSFinancialEntry()
                obj.Document_Code = txtDocumentNo.Value
                obj.Document_Date = txtdate.Value
                obj.DCS_Code = txtDCS.Value
                obj.Fiscal_Code = txtFiscalYear.Value
                obj.Type = clsCommon.myCstr(cboType.SelectedValue)
                obj.Remarks = txtRemarks.Text
                Dim objTr As New clsDCSFinancialEntryDetail
                obj.arr = New List(Of clsDCSFinancialEntryDetail)
                For Each grow As GridViewRowInfo In gvItem.Rows
                    If clsCommon.myLen(grow.Cells(colHeadCode1).Value) > 0 OrElse clsCommon.myLen(grow.Cells(colHeadCode2).Value) > 0 Then
                        objTr = New clsDCSFinancialEntryDetail()
                        objTr.SNo1 = clsCommon.myCDecimal(grow.Cells(colSNo1).Value)
                        objTr.Head_Code1 = clsCommon.myCstr(grow.Cells(colHeadCode1).Value)
                        objTr.Amount_Type1 = clsCommon.myCDecimal(grow.Cells(colSNo1).Tag)
                        objTr.Head_Sub_Amount1 = clsCommon.myCDecimal(grow.Cells(colSubAmount1).Value)
                        objTr.Head_Amount1 = clsCommon.myCDecimal(grow.Cells(colAmount1).Value)

                        objTr.SNo2 = clsCommon.myCDecimal(grow.Cells(colSNo2).Value)
                        objTr.Head_Code2 = clsCommon.myCstr(grow.Cells(colHeadCode2).Value)
                        objTr.Amount_Type2 = clsCommon.myCDecimal(grow.Cells(colSNo2).Tag)
                        objTr.Head_Sub_Amount2 = clsCommon.myCDecimal(grow.Cells(colSubAmount2).Value)
                        objTr.Head_Amount2 = clsCommon.myCDecimal(grow.Cells(colAmount2).Value)

                        obj.arr.Add(objTr)
                    End If
                Next
                If obj.arr.Count <= 0 Then
                    Throw New Exception("Please configure DCS Financial Heads")
                End If
                If (clsDCSFinancialEntry.SaveData(obj, isNewEntry)) Then
                    clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
                    LoadData(obj.Document_Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            If clsCommon.myLen(ex.Message) > 200 Then
                clsERPFuncationality.OpenNotepadFile(ex.Message, Me.Text)
            Else
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End If
        End Try
    End Sub
    Private Sub DeleteData()
        Try
            clsLockMPPaymentCycle.LockMPTransaction(txtDCS.Value, txtdate.Value)
            If (deleteConfirm()) Then
                If (clsDCSFinancialEntry.DeleteData(txtDocumentNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ")
                    Reset()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Reset()
        IsinsideLoadData = True
        Dim obj As clsDCSFinancialEntry = clsDCSFinancialEntry.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            DisableInputDataField()
            isNewEntry = False
            txtDocumentNo.Value = obj.Document_Code
            txtdate.Value = obj.Document_Date
            txtDCS.Value = obj.DCS_Code
            lblDCSName.Text = obj.DCS_Name
            txtFiscalYear.Value = obj.Fiscal_Code
            lblFiscalYear.Text = obj.Fiscal_Name
            txtRemarks.Text = obj.Remarks
            lblPending.Status = obj.Status
            cboType.SelectedValue = obj.Type

            Dim arrVLCUploader As New ArrayList
            Dim arrVLCCode As New ArrayList
            If obj.arr IsNot Nothing AndAlso obj.arr.Count > 0 Then
                For Each objTr As clsDCSFinancialEntryDetail In obj.arr
                    gvItem.Rows.AddNew()
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSNo1).Value = objTr.SNo1
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSNo1).Tag = objTr.Amount_Type1
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colHeadCode1).Value = objTr.Head_Code1
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colHeadName1).Value = objTr.Head_Name1
                    If objTr.Head_Sub_Amount1 <> 0 Then
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSubAmount1).Value = objTr.Head_Sub_Amount1
                    End If
                    If objTr.Head_Amount1 <> 0 Then
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colAmount1).Value = objTr.Head_Amount1
                    End If


                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSNo2).Value = objTr.SNo2
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSNo2).Tag = objTr.Amount_Type2
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colHeadCode2).Value = objTr.Head_Code2
                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colHeadName2).Value = objTr.Head_Name2
                    If objTr.Head_Sub_Amount2 <> 0 Then
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSubAmount2).Value = objTr.Head_Sub_Amount2
                    End If
                    If objTr.Head_Amount2 <> 0 Then
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colAmount2).Value = objTr.Head_Amount2
                    End If
                Next
            End If
            txtDocumentNo.MyReadOnly = True
            If obj.Status = ERPTransactionStatus.Approved Then
                btnsave.Enabled = False
                btndelete.Enabled = False
                btnPost.Enabled = False
            Else
                btnsave.Text = "Update"
                btnsave.Enabled = True
                btndelete.Enabled = True
                btnPost.Enabled = True
            End If

            Dim dt As DataTable = clsDCSFinancialHead.GetHeadSubType(clsCommon.myCstr(cboType.SelectedValue))
            If dt IsNot Nothing AndAlso dt.Rows.Count > 1 Then
                gvItem.Columns(colHeadCode1).HeaderText = dt.Rows(1)("Code")
                gvItem.Columns(colHeadName1).HeaderText = dt.Rows(1)("Name")
                gvItem.Columns(colHeadCode2).HeaderText = dt.Rows(2)("Code")
                gvItem.Columns(colHeadName2).HeaderText = dt.Rows(2)("Name")
            End If
        End If
        IsinsideLoadData = False
    End Sub
    Private Sub txtDocumentNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocumentNo._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_DCS_FINANCIAL_ENTRY where Document_Code='" + txtDocumentNo.Value + "' "
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If check > 0 Then
                txtDocumentNo.MyReadOnly = True
            ElseIf check <= 0 Then
                txtDocumentNo.MyReadOnly = False
            End If

            LoadData(txtDocumentNo.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtDocumentNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtDocumentNo._MYValidating
        Try
            txtDocumentNo.Value = clsDCSFinancialEntry.getFinder("", txtDocumentNo.Value, isButtonClicked)
            LoadData(txtDocumentNo.Value, NavigatorType.Current)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub DisableInputDataField()
        'txtdate.Enabled = False
        'txtDCS.Enabled = False
        'txtFromDate.Enabled = False
        'txtToDate.Enabled = False
    End Sub
    Sub EnableInputDataField()
        'txtdate.Enabled = True
        'txtDCS.Enabled = True
        'txtFromDate.Enabled = True
        'txtToDate.Enabled = True
    End Sub
    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        CloseForm()
    End Sub
    Private Sub btndelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Private Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub



    Sub UpdateAllRow()
        Dim Dict1 As New Dictionary(Of Integer, List(Of Integer))
        Dim arrIndx1 As New List(Of Integer)
        Dim Dict2 As New Dictionary(Of Integer, List(Of Integer))
        Dim arrIndx2 As New List(Of Integer)

        If gvItem.Rows.Count > 0 Then
            For ii As Integer = 0 To gvItem.Rows.Count - 1
                Dim CurrSNo1 As Integer = clsCommon.myCDecimal(gvItem.Rows(ii).Cells(colSNo1).Value)
                If CurrSNo1 > 0 Then
                    Dim NxtSNo1 As Integer = -1
                    If Not (ii = (gvItem.Rows.Count - 1)) Then
                        NxtSNo1 = clsCommon.myCDecimal(gvItem.Rows(ii + 1).Cells(colSNo1).Value)
                    End If
                    If CurrSNo1 = NxtSNo1 Then
                        arrIndx1.Add(ii)
                    Else
                        If arrIndx1.Count > 0 Then
                            arrIndx1.Add(ii)
                            Dict1.Add(ii, arrIndx1)
                        End If
                        arrIndx1 = New List(Of Integer)
                    End If
                End If

                Dim CurrSNo2 As Integer = clsCommon.myCDecimal(gvItem.Rows(ii).Cells(colSNo2).Value)
                If CurrSNo2 > 0 Then
                    Dim NxtSNo2 As Integer = -1
                    If Not (ii = (gvItem.Rows.Count - 1)) Then
                        NxtSNo2 = clsCommon.myCDecimal(gvItem.Rows(ii + 1).Cells(colSNo2).Value)
                    End If
                    If CurrSNo2 = NxtSNo2 Then
                        arrIndx2.Add(ii)
                    Else
                        If arrIndx2.Count > 0 Then
                            arrIndx2.Add(ii)
                            Dict2.Add(ii, arrIndx2)
                        End If
                        arrIndx2 = New List(Of Integer)
                    End If
                End If
            Next
        End If


        If Dict1 IsNot Nothing AndAlso Dict1.Count > 0 Then
            For Each key As Integer In Dict1.Keys
                arrIndx1 = Dict1(key)
                Dim Total As Decimal = 0
                For ii As Integer = 0 To arrIndx1.Count - 1
                    Total += clsCommon.myCDecimal(gvItem.Rows(arrIndx1(ii)).Cells(colSubAmount1).Value)
                Next
                gvItem.Rows(key).Cells(colAmount1).Value = Total
            Next
        End If

        If Dict2 IsNot Nothing AndAlso Dict2.Count > 0 Then
            For Each key As Integer In Dict2.Keys
                arrIndx2 = Dict2(key)
                Dim Total As Decimal = 0
                For ii As Integer = 0 To arrIndx2.Count - 1
                    Total += clsCommon.myCDecimal(gvItem.Rows(arrIndx2(ii)).Cells(colSubAmount2).Value)
                Next
                gvItem.Rows(key).Cells(colAmount2).Value = Total
            Next
        End If

        Dim x As Integer = 0
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Private Sub PostData()
        Try
            Dim qry As String = ""
            Dim msg As String = ""
            Dim dt As DataTable = Nothing
            If (myMessages.postConfirm()) Then
                clsDCSFinancialEntry.PostData(txtDocumentNo.Value)
                clsCommon.MyMessageBoxShow(Me, "Posted Successfully", Me.Text)
                LoadData(txtDocumentNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub gvItem_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvItem.CellValueChanged
        Try
            If (Not IsinsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gvItem.Columns(colSubAmount1) OrElse e.Column Is gvItem.Columns(colSubAmount2) Then
                        UpdateAllRow()
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isCellValueChangedOpen = False
        End Try
    End Sub
    Private Sub cboType_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cboType.Validating
        Try
            loadBlankGrid()
            Dim dt As DataTable = clsDCSFinancialHead.GetHeadSubType(clsCommon.myCstr(cboType.SelectedValue))
            If dt IsNot Nothing AndAlso dt.Rows.Count > 1 Then
                gvItem.Columns(colHeadCode1).HeaderText = dt.Rows(1)("Code")
                gvItem.Columns(colHeadName1).HeaderText = dt.Rows(1)("Name")
                gvItem.Columns(colHeadCode2).HeaderText = dt.Rows(2)("Code")
                gvItem.Columns(colHeadName2).HeaderText = dt.Rows(2)("Name")

                Dim indx As Integer = 0
                Qry = "select Code,Description,Sub_Type,cast(SNo as integer) as SNo,Parent_Head from TSPL_DCS_FINANCIAL_HEAD where Type='" + clsCommon.myCstr(cboType.SelectedValue) + "' and Sub_Type='" + clsCommon.myCstr(dt.Rows(1)("Code")) + "' order by TSPL_DCS_FINANCIAL_HEAD.SNo"
                Dim dtFH As DataTable = clsDBFuncationality.GetDataTable(Qry)
                If dtFH IsNot Nothing AndAlso dtFH.Rows.Count > 0 Then
                    For jj As Integer = 0 To dtFH.Rows.Count - 1
                        If indx >= gvItem.Rows.Count - 1 Then
                            gvItem.Rows.AddNew()
                            indx = gvItem.Rows.Count - 1
                        End If
                        gvItem.Rows(indx).Cells(colSNo1).Value = clsCommon.myCDecimal(dtFH.Rows(jj)("SNo"))
                        gvItem.Rows(indx).Cells(colHeadCode1).Value = clsCommon.myCstr(dtFH.Rows(jj)("Code"))
                        gvItem.Rows(indx).Cells(colHeadName1).Value = clsCommon.myCstr(dtFH.Rows(jj)("Description"))

                        gvItem.Rows(indx).Cells(colSNo1).Tag = 2
                        If clsCommon.myLen(dtFH.Rows(jj)("Parent_Head")) > 0 Then
                            gvItem.Rows(indx).Cells(colSNo1).Tag = 1
                        Else
                            If jj < dtFH.Rows.Count - 1 Then
                                If clsCommon.CompairString(clsCommon.myCstr(dtFH.Rows(jj)("Code")), clsCommon.myCstr(dtFH.Rows(jj + 1)("Parent_Head"))) = CompairStringResult.Equal Then
                                    gvItem.Rows(indx).Cells(colSNo1).Tag = 1
                                End If
                            End If
                        End If
                        indx += 1
                    Next
                End If


                indx = -1
                Qry = "select Code,Description,Sub_Type,cast(SNo as integer) as SNo,Parent_Head from TSPL_DCS_FINANCIAL_HEAD where Type='" + clsCommon.myCstr(cboType.SelectedValue) + "' and Sub_Type='" + clsCommon.myCstr(dt.Rows(2)("Code")) + "' order by TSPL_DCS_FINANCIAL_HEAD.SNo"
                dtFH = clsDBFuncationality.GetDataTable(Qry)
                If dtFH IsNot Nothing AndAlso dtFH.Rows.Count > 0 Then
                    For jj As Integer = 0 To dtFH.Rows.Count - 1
                        If indx >= gvItem.Rows.Count - 1 Then
                            gvItem.Rows.AddNew()
                            indx = gvItem.Rows.Count - 1
                        Else
                            indx += 1
                        End If
                        gvItem.Rows(indx).Cells(colSNo2).Value = clsCommon.myCDecimal(dtFH.Rows(jj)("SNo"))
                        gvItem.Rows(indx).Cells(colHeadCode2).Value = clsCommon.myCstr(dtFH.Rows(jj)("Code"))
                        gvItem.Rows(indx).Cells(colHeadName2).Value = clsCommon.myCstr(dtFH.Rows(jj)("Description"))

                        gvItem.Rows(indx).Cells(colSNo2).Tag = 2
                        If clsCommon.myLen(dtFH.Rows(jj)("Parent_Head")) > 0 Then
                            gvItem.Rows(indx).Cells(colSNo2).Tag = 1
                        Else
                            If jj < dtFH.Rows.Count - 1 Then
                                If clsCommon.CompairString(clsCommon.myCstr(dtFH.Rows(jj)("Code")), clsCommon.myCstr(dtFH.Rows(jj + 1)("Parent_Head"))) = CompairStringResult.Equal Then
                                    gvItem.Rows(indx).Cells(colSNo2).Tag = 1
                                End If
                            End If
                        End If
                    Next
                End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtMCC__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDCS._MYValidating
        Dim qry As String = "Select TSPL_VLC_MASTER_HEAD.VLC_Code as DCSCode,TSPL_VLC_MASTER_HEAD.VLC_Code_vlc_Uploader as [Uploader Code],TSPL_VLC_MASTER_HEAD.VLC_Name as [DCS Name],TSPL_VLC_MASTER_HEAD.MCC as [MCC Code],TSPL_VLC_MASTER_HEAD.Route_Code as [Route Code]   from TSPL_VLC_MASTER_HEAD "
        txtDCS.Value = clsCommon.ShowSelectForm("DCSFERFY#F", qry, "DCSCode", "", txtDCS.Value, "", isButtonClicked)
        lblDCSName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select VLC_Name from TSPL_VLC_MASTER_HEAD where VLC_Code ='" + txtDCS.Value + "' "))
    End Sub
    Private Sub txtFiscalYear__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtFiscalYear._MYValidating
        Try
            Dim qry As String = "select Fiscal_Code,Fiscal_Name,Start_Date,End_Date from TSPL_FISCAL_YEAR_MASTER"
            txtFiscalYear.Value = clsCommon.ShowSelectForm("DCSFERFY#F", qry, "Fiscal_Code", "", txtFiscalYear.Value, "", isButtonClicked)
            lblFiscalYear.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Fiscal_Name from TSPL_FISCAL_YEAR_MASTER where Fiscal_Code ='" + txtFiscalYear.Value + "' "))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub
    Private Sub gvItem_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gvItem.CellFormatting
        Try

            If e.Column.Index >= 0 Then
                If (e.Column Is gvItem.Columns(colSubAmount1)) Then
                    gvItem.CurrentRow.Cells(colSubAmount1).ReadOnly = Not (clsCommon.myCdbl(gvItem.CurrentRow.Cells(colSNo1).Tag) = 1)
                ElseIf (e.Column Is gvItem.Columns(colAmount1)) Then
                    gvItem.CurrentRow.Cells(colAmount1).ReadOnly = Not (clsCommon.myCdbl(gvItem.CurrentRow.Cells(colSNo1).Tag) = 2)
                ElseIf (e.Column Is gvItem.Columns(colSubAmount2)) Then
                    gvItem.CurrentRow.Cells(colSubAmount2).ReadOnly = Not (clsCommon.myCdbl(gvItem.CurrentRow.Cells(colSNo2).Tag) = 1)
                ElseIf (e.Column Is gvItem.Columns(colAmount2)) Then
                    gvItem.CurrentRow.Cells(colAmount2).ReadOnly = Not (clsCommon.myCdbl(gvItem.CurrentRow.Cells(colSNo2).Tag) = 2)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gvItem_CellValidated(sender As Object, e As CellValidatedEventArgs) Handles gvItem.CellValidated
        Try
            SetGridFocus()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFocus()
        If gvItem.CurrentCell IsNot Nothing Then
            If gvItem.CurrentCell.ColumnInfo.Name = colSubAmount1 Then
                If clsCommon.myCDecimal(gvItem.CurrentRow.Cells(colSNo2).Tag) = 1 Then
                    gvItem.CurrentColumn = gvItem.Columns(colSubAmount2)
                ElseIf clsCommon.myCDecimal(gvItem.CurrentRow.Cells(colSNo2).Tag) = 2 Then
                    gvItem.CurrentColumn = gvItem.Columns(colAmount2)
                End If
            ElseIf gvItem.CurrentCell.ColumnInfo.Name = colAmount1 Then
                If clsCommon.myCDecimal(gvItem.CurrentRow.Cells(colSNo2).Tag) = 1 Then
                    gvItem.CurrentColumn = gvItem.Columns(colSubAmount2)
                ElseIf clsCommon.myCDecimal(gvItem.CurrentRow.Cells(colSNo2).Tag) = 2 Then
                    gvItem.CurrentColumn = gvItem.Columns(colAmount2)
                End If
            ElseIf (gvItem.CurrentCell.ColumnInfo.Name = colSubAmount2) Then
                If gvItem.Rows.Count > gvItem.CurrentRow.Index + 1 Then
                    gvItem.CurrentRow = gvItem.Rows(gvItem.CurrentRow.Index + 1)
                End If
                If clsCommon.myCDecimal(gvItem.CurrentRow.Cells(colSNo1).Tag) = 1 Then
                    gvItem.CurrentColumn = gvItem.Columns(colSubAmount1)
                ElseIf clsCommon.myCDecimal(gvItem.CurrentRow.Cells(colSNo1).Tag) = 2 Then
                    gvItem.CurrentColumn = gvItem.Columns(colAmount1)
                ElseIf clsCommon.myCDecimal(gvItem.CurrentRow.Cells(colSNo2).Tag) = 1 Then
                    gvItem.CurrentColumn = gvItem.Columns(colSubAmount2)
                ElseIf clsCommon.myCDecimal(gvItem.CurrentRow.Cells(colSNo2).Tag) = 2 Then
                    gvItem.CurrentColumn = gvItem.Columns(colAmount2)
                End If

            ElseIf (gvItem.CurrentCell.ColumnInfo.Name = colAmount2) Then
                If gvItem.Rows.Count > gvItem.CurrentRow.Index + 1 Then
                    gvItem.CurrentRow = gvItem.Rows(gvItem.CurrentRow.Index + 1)
                End If
                If clsCommon.myCDecimal(gvItem.CurrentRow.Cells(colSNo1).Tag) = 1 Then
                    gvItem.CurrentColumn = gvItem.Columns(colSubAmount1)
                ElseIf clsCommon.myCDecimal(gvItem.CurrentRow.Cells(colSNo1).Tag) = 2 Then
                    gvItem.CurrentColumn = gvItem.Columns(colAmount1)
                ElseIf clsCommon.myCDecimal(gvItem.CurrentRow.Cells(colSNo2).Tag) = 1 Then
                    gvItem.CurrentColumn = gvItem.Columns(colSubAmount2)
                ElseIf clsCommon.myCDecimal(gvItem.CurrentRow.Cells(colSNo2).Tag) = 2 Then
                    gvItem.CurrentColumn = gvItem.Columns(colAmount2)
                End If
            End If
        End If
    End Sub
End Class



