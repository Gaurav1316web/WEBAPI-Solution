Imports common
Imports Telerik.WinControls.UI
Imports System.IO
Imports XpertERPEngine

Public Class frmAssetsIssueReturn
    Inherits FrmMainTranScreen

    Const colLineNo As String = "LineNo"
    Const colIssueCode As String = "IssueCode"
    Const colAssetCode As String = "AssetCode"
    Const colAssetDescription As String = "AssetDescription"
    Const colAssetSno As String = "colAssetSno"
    Const colQty As String = "colqty"
    Const colReturnedQty As String = "colReturnedQty"
    Const colSelect As String = "colSelect"
    Const colStatus As String = "colStatus"
    Const colAssetTypeCode As String = "AssetTypeCode"
    Const colAssetTypeDesc As String = "AssetTypeDesc"

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = True

    Dim obj As New clsAssetsIssueReturn
    Private ObjList As New List(Of clsAssetsIssueReturnDetail)
    Private isCellValueChangedOpen As Boolean = False
    Private isCboActivated As Boolean = False

    Sub LoadGridColumns()
        Dim LineNo As New GridViewTextBoxColumn
        Dim AssetCode As New GridViewTextBoxColumn
        Dim AssetDescription As New GridViewTextBoxColumn
        Dim AssetSno As New GridViewTextBoxColumn

        Dim Qty As New GridViewDecimalColumn
        Dim Status As New GridViewTextBoxColumn
        Dim AssetTypeCode As New GridViewTextBoxColumn
        Dim AssetTypeDesc As New GridViewTextBoxColumn

        Dim ReturnedQty As New GridViewDecimalColumn

        Dim SelectAsset As New GridViewCheckBoxColumn

        gvIncrement.Rows.Clear()
        gvIncrement.Columns.Clear()

        LineNo.FormatString = ""
        LineNo.HeaderText = "Line No"
        LineNo.Name = colLineNo
        LineNo.Width = 50
        LineNo.ReadOnly = True
        LineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvIncrement.Columns.Add(LineNo)

        AssetCode.FormatString = ""
        AssetCode.HeaderText = "Asset Code"
        AssetCode.Name = colAssetCode
        AssetCode.Width = 150
        AssetCode.ReadOnly = False
        AssetCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvIncrement.Columns.Add(AssetCode)

        AssetDescription.FormatString = ""
        AssetDescription.HeaderText = "Asset Description"
        AssetDescription.Name = colAssetDescription
        AssetDescription.Width = 150
        AssetDescription.ReadOnly = True
        AssetDescription.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvIncrement.Columns.Add(AssetDescription)

        'AssetSno.FormatString = ""
        'AssetSno.HeaderText = "Asset Sno"
        'AssetSno.Name = colAssetSno
        'AssetSno.Width = 100
        'AssetSno.ReadOnly = True
        'AssetSno.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gvIncrement.Columns.Add(AssetSno)

        AssetTypeCode.FormatString = ""
        AssetTypeCode.HeaderText = "Asset Type Code"
        AssetTypeCode.Name = colAssetTypeCode
        AssetTypeCode.Width = 150
        AssetTypeCode.ReadOnly = True
        AssetTypeCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvIncrement.Columns.Add(AssetTypeCode)

        'AssetTypeDesc.FormatString = ""
        'AssetTypeDesc.HeaderText = "Asset Type"
        'AssetTypeDesc.Name = colAssetTypeDesc
        'AssetTypeDesc.Width = 100
        'AssetTypeDesc.ReadOnly = True
        'AssetTypeDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gvIncrement.Columns.Add(AssetTypeDesc)

        Qty.FormatString = ""
        Qty.HeaderText = "Qty."
        Qty.Name = colQty
        Qty.Width = 90
        Qty.ReadOnly = False
        Qty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvIncrement.Columns.Add(Qty)

        ReturnedQty.FormatString = ""
        ReturnedQty.HeaderText = "Returned Qty."
        ReturnedQty.Name = colReturnedQty
        ReturnedQty.Width = 100
        ReturnedQty.ReadOnly = False
        ReturnedQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvIncrement.Columns.Add(ReturnedQty)

        Status.FormatString = ""
        Status.HeaderText = "Status"
        Status.Name = colStatus
        Status.Width = 150
        Status.ReadOnly = True
        Status.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvIncrement.Columns.Add(Status)
        

        SelectAsset.FormatString = ""
        SelectAsset.HeaderText = "Select"
        SelectAsset.Name = colSelect
        SelectAsset.Width = 100
        SelectAsset.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvIncrement.Columns.Add(SelectAsset)

    End Sub
    'Sub LoadGridColumns1()

    '    'Dim LineNo As New GridViewTextBoxColumn
    '    'Dim AssetCode As New GridViewTextBoxColumn
    '    'Dim AssetDescription As New GridViewTextBoxColumn
    '    'Dim AssetSno As New GridViewTextBoxColumn

    '    'Dim Qty As New GridViewDecimalColumn
    '    Dim ReturnedQty As New GridViewDecimalColumn
    '    Dim Status As New GridViewTextBoxColumn
    '    Dim SelectAsset As New GridViewCheckBoxColumn

    '    'Dim AssetTypeCode As New GridViewTextBoxColumn
    '    'Dim AssetTypeDesc As New GridViewTextBoxColumn

    '    'gvIncrement.Rows.Clear()
    '    'gvIncrement.Columns.Clear()
    '    'gvIncrement.Rows.AddNew()
    '    'LineNo.FormatString = ""
    '    'LineNo.HeaderText = "Line No"
    '    'LineNo.Name = colLineNo
    '    'LineNo.Width = 100
    '    'LineNo.ReadOnly = True
    '    'LineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '    'gvIncrement.Columns.Add(LineNo)

    '    'AssetCode.FormatString = ""
    '    'AssetCode.HeaderText = "Asset Code"
    '    'AssetCode.Name = colAssetCode
    '    'AssetCode.Width = 100
    '    'AssetCode.ReadOnly = False
    '    'AssetCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '    'gvIncrement.Columns.Add(AssetCode)

    '    'AssetDescription.FormatString = ""
    '    'AssetDescription.HeaderText = "Asset Description"
    '    'AssetDescription.Name = colAssetDescription
    '    'AssetDescription.Width = 100
    '    'AssetDescription.ReadOnly = True
    '    'AssetDescription.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '    'gvIncrement.Columns.Add(AssetDescription)

    '    'AssetSno.FormatString = ""
    '    'AssetSno.HeaderText = "Asset Sno"
    '    'AssetSno.Name = colAssetSno
    '    'AssetSno.Width = 100
    '    'AssetSno.ReadOnly = True
    '    'AssetSno.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '    'gvIncrement.Columns.Add(AssetSno)

    '    'Qty.FormatString = ""
    '    'Qty.HeaderText = "Qty."
    '    'Qty.Name = colQty
    '    'Qty.Width = 100
    '    'Qty.ReadOnly = False
    '    'Qty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '    'gvIncrement.Columns.Add(Qty)

    '    ReturnedQty.FormatString = ""
    '    ReturnedQty.HeaderText = "Returned Qty."
    '    ReturnedQty.Name = colReturnedQty
    '    ReturnedQty.Width = 100
    '    ReturnedQty.ReadOnly = False
    '    ReturnedQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '    gvIncrement.Columns.Add(ReturnedQty)

    '    Status.FormatString = ""
    '    Status.HeaderText = "Status"
    '    Status.Name = colStatus
    '    Status.Width = 100
    '    Status.ReadOnly = True
    '    Status.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '    gvIncrement.Columns.Add(Status)

    '    SelectAsset.FormatString = ""
    '    SelectAsset.HeaderText = "Select"
    '    SelectAsset.Name = colSelect
    '    SelectAsset.Width = 100
    '    SelectAsset.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '    gvIncrement.Columns.Add(SelectAsset)



    '    'AssetTypeCode.FormatString = ""
    '    'AssetTypeCode.HeaderText = "Asset Type Code"
    '    'AssetTypeCode.Name = colAssetTypeCode
    '    'AssetTypeCode.Width = 100
    '    'AssetTypeCode.ReadOnly = True
    '    'AssetTypeCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '    'gvIncrement.Columns.Add(AssetTypeCode)

    '    'AssetTypeDesc.FormatString = ""
    '    'AssetTypeDesc.HeaderText = "Asset Type"
    '    'AssetTypeDesc.Name = colAssetTypeDesc
    '    'AssetTypeDesc.Width = 100
    '    'AssetTypeDesc.ReadOnly = True
    '    'AssetTypeDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '    'gvIncrement.Columns.Add(AssetTypeDesc)

    'End Sub

    Private Sub frmAdjustmentVoucher_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnsave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Private Sub frmMonthlyAttendance_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        funReset()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Public Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmAssetsIssueReturn)
        'If Not (MyBase.isReadFlag) Then
        '    common.clsCommon.MyMessageBoxShow("Permission Denied")
        '    Me.Close()
        '    Exit Function
        'End If
        'btnsave.Visible = MyBase.isModifyFlag
        'btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        funClose()
    End Sub

    Sub funClose()
        Me.Close()
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            funReset()
        Catch ex As Exception

        End Try
    End Sub

    Sub funReset()

        isNewEntry = True
        lblReturnNo.Visible = False
        FndIssueReturn.Visible = False
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        FndIssueReturn.Value = Nothing
        UsLock1.Status = ERPTransactionStatus.Pending
        txtCode.Focus()
        txtIssueTo.Value = Nothing
        lblIssueToName.Text = ""
        txtDescription.Text = ""
        dtpDate.Value = clsCommon.GETSERVERDATE()
        isCboActivated = False
        lblCode.Text = "Issue No."
        lblIssueTo.Text = "Issue To"
        lblDate.Text = "Issue Date"
        Dim dtType As DataTable = GetVochType()
        cboType.SelectedIndex = -1
        cboType.DataSource = dtType
        cboType.ValueMember = "Code"
        cboType.DisplayMember = "Value"
        isCboActivated = True
        cboType_SelectedValueChanged(Nothing, Nothing)
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = True

        BtnPost.Enabled = True

        LoadGridColumns()
        gvIncrement.Rows.Clear()
        gvIncrement.Rows.AddNew()
        gvIncrement.Columns(colReturnedQty).IsVisible = False
        gvIncrement.Columns(colSelect).IsVisible = False

    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadIssueData(ByVal strCode As String, ByVal NavTyep As NavigatorType)

        obj = obj.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.IssueCode) > 0) Then
            'funReset()
            isNewEntry = True
            btnsave.Text = "Save"
            Dim ii As Int16 = 0
            LoadGridColumns()
            'If clsCommon.myLen(FndIssueReturn.Value) <= 0 Then
            '    txtCode.Value = obj.IssueCode
            '    cboType.SelectedValue = obj.Type
            'End If

            txtIssueTo.Value = obj.IssueTo
            txtDescription.Text = obj.Remark
            lblIssueToName.Text = obj.IssueToName
            dtpDate.Value = obj.IssueDate
            txtCode.MyReadOnly = True

            'cboType_SelectedValueChanged(Nothing, Nothing)

            'If obj.Type = "Issue" Then
            '    gvIncrement.Columns(colSelect).IsVisible = False
            'End If
            'If obj.POSTED = ERPTransactionStatus.Approved Then
            btnsave.Enabled = True
            BtnPost.Enabled = True
            btndelete.Enabled = True
            UsLock1.Status = ERPTransactionStatus.Pending
            'End If

            'If obj.Type = "Issue" AndAlso obj.s Then
            If (obj.ObjList IsNot Nothing AndAlso obj.ObjList.Count > 0) Then
                Dim Cnt As Integer = 0
                For Each objtr As clsAssetsIssueReturnDetail In obj.ObjList
                    gvIncrement.Rows.AddNew()
                    Cnt = Cnt + 1
                    ' If objtr.Status = "Returned" Then
                    'lblReturnNo.Visible = True
                    'FndIssueREturn.Visible = True
                    'FndIssueREturn.Text = objtr.ReturnCode
                    gvIncrement.Columns(colReturnedQty).IsVisible = True
                    'gvIncrement.Columns(colSelect).IsVisible = True
                    gvIncrement.Rows(gvIncrement.Rows.Count - 1).Cells(colReturnedQty).Value = objtr.ReturnedQty
                    'Else
                    '    gvIncrement.Columns(colReturnedQty).IsVisible = False
                    '    gvIncrement.Columns(colSelect).IsVisible = False

                    'End If
                    gvIncrement.Rows(gvIncrement.Rows.Count - 1).Cells(colLineNo).Value = Cnt
                    gvIncrement.Rows(gvIncrement.Rows.Count - 1).Cells(colAssetCode).Value = objtr.Asset_Code
                    gvIncrement.Rows(gvIncrement.Rows.Count - 1).Cells(colAssetDescription).Value = objtr.Asset_Description
                    'gvIncrement.Rows(gvIncrement.Rows.Count - 1).Cells(colAssetSno).Value = objtr.Asset_Sno
                    gvIncrement.Rows(gvIncrement.Rows.Count - 1).Cells(colQty).Value = objtr.Qty
                    gvIncrement.Rows(gvIncrement.Rows.Count - 1).Cells(colStatus).Value = "Returned"
                    gvIncrement.Rows(gvIncrement.Rows.Count - 1).Cells(colAssetTypeCode).Value = objtr.Asset_Type_Code
                    'gvIncrement.Rows(gvIncrement.Rows.Count - 1).Cells(colAssetTypeDesc).Value = objtr.Asset_Type_desc
                    'lblReturnNo.Visible = False
                    'FndIssueReturn.Visible = False

                Next
            Else
                gvIncrement.Rows.AddNew()
            End If
            'Else
            '    If (obj.ObjList IsNot Nothing AndAlso obj.ObjList.Count > 0) Then
            '        Dim Cnt As Integer = 0
            '        For Each objtr As clsAssetsIssueReturnDetail In obj.ObjList
            '            gvIncrement.Rows.AddNew()
            '            Cnt = Cnt + 1
            '            FndIssueREturn.Text = objtr.ReturnCode
            '            gvIncrement.Rows(gvIncrement.Rows.Count - 1).Cells(colLineNo).Value = Cnt
            '            gvIncrement.Rows(gvIncrement.Rows.Count - 1).Cells(colAssetCode).Value = objtr.Asset_Code
            '            gvIncrement.Rows(gvIncrement.Rows.Count - 1).Cells(colAssetDescription).Value = objtr.Asset_Description
            '            'gvIncrement.Rows(gvIncrement.Rows.Count - 1).Cells(colAssetSno).Value = objtr.Asset_Sno
            '            gvIncrement.Rows(gvIncrement.Rows.Count - 1).Cells(colQty).Value = objtr.Qty
            '            gvIncrement.Rows(gvIncrement.Rows.Count - 1).Cells(colAssetTypeCode).Value = objtr.Asset_Type_Code
            '            'gvIncrement.Rows(gvIncrement.Rows.Count - 1).Cells(colAssetTypeDesc).Value = objtr.Asset_Type_desc
            '            gvIncrement.Rows(gvIncrement.Rows.Count - 1).Cells(colStatus).Value = objtr.Status
            '            gvIncrement.Rows(gvIncrement.Rows.Count - 1).Cells(colReturnedQty).Value = objtr.ReturnedQty
            '            gvIncrement.Rows(gvIncrement.Rows.Count - 1).Cells(colReturnedQty).ReadOnly = True
            '        Next
            '    Else
            '        gvIncrement.Rows.AddNew()
            '    End If
            'End If
        End If

    End Sub


    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)

        obj = obj.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.IssueCode) > 0) Then
            'funReset()
            isNewEntry = False
            btnsave.Text = "Update"
            Dim ii As Int16 = 0
            LoadGridColumns()
            If clsCommon.myLen(FndIssueReturn.Value) <= 0 Then
                txtCode.Value = obj.IssueCode
                cboType.SelectedValue = obj.Type
            End If

            txtIssueTo.Value = obj.IssueTo
            txtDescription.Text = obj.Remark
            lblIssueToName.Text = obj.IssueToName
            dtpDate.Value = obj.IssueDate
            txtCode.MyReadOnly = True
            FndIssueReturn.Value = obj.Against_Issue_Code
            'cboType_SelectedValueChanged(Nothing, Nothing)

            If obj.Type = "Issue" Then
                gvIncrement.Columns(colSelect).IsVisible = False
            End If
            If obj.POSTED = ERPTransactionStatus.Approved Then
                btnsave.Enabled = False
                BtnPost.Enabled = False
                btndelete.Enabled = False
                UsLock1.Status = ERPTransactionStatus.Approved
            End If

            'If obj.Type = "Issue" AndAlso obj.s Then
            If (obj.ObjList IsNot Nothing AndAlso obj.ObjList.Count > 0) Then
                Dim Cnt As Integer = 0
                For Each objtr As clsAssetsIssueReturnDetail In obj.ObjList
                    gvIncrement.Rows.AddNew()
                    Cnt = Cnt + 1
                    If objtr.Status = "Returned" Then
                        'lblReturnNo.Visible = True
                        'FndIssueREturn.Visible = True
                        'FndIssueREturn.Text = objtr.ReturnCode
                        gvIncrement.Columns(colReturnedQty).IsVisible = True
                        'gvIncrement.Columns(colSelect).IsVisible = True
                        gvIncrement.Rows(gvIncrement.Rows.Count - 1).Cells(colReturnedQty).Value = objtr.ReturnedQty
                    Else
                        gvIncrement.Columns(colReturnedQty).IsVisible = False
                        gvIncrement.Columns(colSelect).IsVisible = False

                    End If
                    gvIncrement.Rows(gvIncrement.Rows.Count - 1).Cells(colLineNo).Value = Cnt
                    gvIncrement.Rows(gvIncrement.Rows.Count - 1).Cells(colAssetCode).Value = objtr.Asset_Code
                    gvIncrement.Rows(gvIncrement.Rows.Count - 1).Cells(colAssetDescription).Value = objtr.Asset_Description
                    'gvIncrement.Rows(gvIncrement.Rows.Count - 1).Cells(colAssetSno).Value = objtr.Asset_Sno
                    gvIncrement.Rows(gvIncrement.Rows.Count - 1).Cells(colQty).Value = objtr.Qty
                    gvIncrement.Rows(gvIncrement.Rows.Count - 1).Cells(colStatus).Value = objtr.Status
                    gvIncrement.Rows(gvIncrement.Rows.Count - 1).Cells(colAssetTypeCode).Value = objtr.Asset_Type_Code
                    'gvIncrement.Rows(gvIncrement.Rows.Count - 1).Cells(colAssetTypeDesc).Value = objtr.Asset_Type_desc
                    If objtr.Status = "Returned" Then
                        lblReturnNo.Visible = True
                        FndIssueReturn.Visible = True
                    Else
                        lblReturnNo.Visible = False
                        FndIssueReturn.Visible = False
                    End If
                Next
            Else
                gvIncrement.Rows.AddNew()
            End If
            'Else
            '    If (obj.ObjList IsNot Nothing AndAlso obj.ObjList.Count > 0) Then
            '        Dim Cnt As Integer = 0
            '        For Each objtr As clsAssetsIssueReturnDetail In obj.ObjList
            '            gvIncrement.Rows.AddNew()
            '            Cnt = Cnt + 1
            '            FndIssueREturn.Text = objtr.ReturnCode
            '            gvIncrement.Rows(gvIncrement.Rows.Count - 1).Cells(colLineNo).Value = Cnt
            '            gvIncrement.Rows(gvIncrement.Rows.Count - 1).Cells(colAssetCode).Value = objtr.Asset_Code
            '            gvIncrement.Rows(gvIncrement.Rows.Count - 1).Cells(colAssetDescription).Value = objtr.Asset_Description
            '            'gvIncrement.Rows(gvIncrement.Rows.Count - 1).Cells(colAssetSno).Value = objtr.Asset_Sno
            '            gvIncrement.Rows(gvIncrement.Rows.Count - 1).Cells(colQty).Value = objtr.Qty
            '            gvIncrement.Rows(gvIncrement.Rows.Count - 1).Cells(colAssetTypeCode).Value = objtr.Asset_Type_Code
            '            'gvIncrement.Rows(gvIncrement.Rows.Count - 1).Cells(colAssetTypeDesc).Value = objtr.Asset_Type_desc
            '            gvIncrement.Rows(gvIncrement.Rows.Count - 1).Cells(colStatus).Value = objtr.Status
            '            gvIncrement.Rows(gvIncrement.Rows.Count - 1).Cells(colReturnedQty).Value = objtr.ReturnedQty
            '            gvIncrement.Rows(gvIncrement.Rows.Count - 1).Cells(colReturnedQty).ReadOnly = True
            '        Next
            '    Else
            '        gvIncrement.Rows.AddNew()
            '    End If
            'End If
        End If

    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCode._MYValidating
        Dim str As String = "select count(*) from TSPL_AssetIssueReturn where IssueCode ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = ""
            If clsCommon.CompairString(cboType.Text, "Issue") = CompairStringResult.Equal Then
                qry = "select IssueCode as Code, IssueTo, Remark, IssueDate from TSPL_AssetIssueReturn "
            Else
                qry = "select IssueCode as Code, IssueTo 'Return From', Remark, IssueDate 'Return Date' from TSPL_AssetIssueReturn "
            End If
            Dim whrcls As String = "Type='" + cboType.Text + "' "
            txtCode.Value = clsCommon.ShowSelectForm("fndIssRet", qry, "Code", whrcls, txtCode.Value, "IssueCode", isButtonClicked)
            If txtCode.Value <> "" Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                funReset()
            End If
        End If
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SavingData(False)
    End Sub
    Public Function Save() As Boolean
        Try
            Dim Counter As Integer = 0
            For Each grow1 As GridViewRowInfo In gvIncrement.Rows
                If grow1.Cells(colSelect).Value = True OrElse cboType.SelectedValue = "Issue" Then
                    Counter += 1
                    If AllowToSave() Then
                        Dim obj As New clsAssetsIssueReturn
                        ObjList = New List(Of clsAssetsIssueReturnDetail)
                        obj.IssueCode = Me.txtCode.Value
                        obj.IssueTo = Me.txtIssueTo.Value
                        obj.IssueDate = Me.dtpDate.Value
                        obj.Remark = Me.txtDescription.Text
                        obj.Type = cboType.SelectedValue
                        obj.Against_Issue_Code = FndIssueReturn.Value
                        Dim obj1 As clsAssetsIssueReturnDetail
                        For Each grow As GridViewRowInfo In gvIncrement.Rows
                            If grow.Cells(colSelect).Value = True OrElse cboType.SelectedValue = "Issue" Then
                                If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colAssetCode).Value)) > 0 Then
                                    obj1 = New clsAssetsIssueReturnDetail()
                                    obj1.Asset_Code = clsCommon.myCstr(grow.Cells(colAssetCode).Value)
                                    obj1.Asset_Description = clsCommon.myCstr(grow.Cells(colAssetDescription).Value)
                                    obj1.Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                                    obj1.Asset_Type_Code = clsCommon.myCstr(grow.Cells(colAssetTypeCode).Value)
                                    obj1.ReturnCode = clsCommon.myCstr(FndIssueReturn.Value)
                                    obj1.ReturnedQty = clsCommon.myCdbl(grow.Cells(colReturnedQty).Value)
                                    ObjList.Add(obj1)
                                End If
                            End If
                        Next
                        obj.ObjList = ObjList
                        If (obj.SaveData(obj, isNewEntry, clsCommon.myCstr(txtCode.Value))) Then
                            LoadData(obj.IssueCode, NavigatorType.Current)
                            If cboType.SelectedValue = "Issue" AndAlso gvIncrement.CurrentRow.Cells(colStatus).Value = "Returned" Then
                                lblReturnNo.Visible = True
                                FndIssueReturn.Visible = True
                                FndIssueReturn.Text = obj.ReturnCode
                            End If
                            'common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                            Return True
                        End If
                    End If
                End If
            Next
            If Counter <= 0 Then
                Throw New Exception("Please select atleast One Asset to Return")
            End If
            Return False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return False
    End Function
    Function AllowToSave() As Boolean
        If clsCommon.myLen(txtIssueTo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please select " + lblIssueTo.Text)
            txtIssueTo.Focus()
            Return False

        End If
        'For Each grow As GridViewRowInfo In gvIncrement.Rows
        '    If cboType.SelectedValue = "Return" AndAlso grow.Cells(colStatus).Value = "Returned" AndAlso grow.Cells(colSelect).Value = True Then
        '        Throw New Exception("This Asset is Already Returned")
        '    End If

        'Next



        'If cboType.Text = "" Then
        '    clsCommon.MyMessageBoxShow("Increment Type")
        '    cboType.Focus()
        '    Return False
        'End If

        Dim Counter As Integer = 0
        For ii As Integer = 0 To gvIncrement.Rows.Count - 1
            If clsCommon.myLen(clsCommon.myCstr(gvIncrement.Rows(ii).Cells(colAssetCode).Value)) > 0 Then
                For jj As Integer = ii + 1 To gvIncrement.Rows.Count - 1
                    If (clsCommon.CompairString(clsCommon.myCstr(gvIncrement.Rows(ii).Cells(colAssetCode).Value), clsCommon.myCstr(gvIncrement.Rows(jj).Cells(colAssetCode).Value)) = CompairStringResult.Equal) Then
                        Throw New Exception("Duplicate Asset exist at row '" + clsCommon.myCstr(ii + 1) + "' And '" + clsCommon.myCstr(jj + 1) + "'")
                    End If

                    Dim qty As Double = clsCommon.myCdbl(gvIncrement.Rows(ii).Cells(colQty).Value)
                    Dim Returnqty As Double = clsCommon.myCdbl(gvIncrement.Rows(ii).Cells(colReturnedQty).Value)
                    If Returnqty > qty Then
                        Throw New Exception("Return Qty cannot be greater than Issue Qty")
                    End If

                    If cboType.SelectedValue = "Issue" Then
                        Dim query As String = "Select Count(1)AssetCode from TSPL_AssetIssueReturnDetail  " & _
                                    "left outer join TSPL_AssetIssueReturn on TSPL_AssetIssueReturn.IssueCode = TSPL_AssetIssueReturnDetail.IssueCode " & _
                                    " where TSPL_AssetIssueReturn.Type='Issue' and ISRETURN='0' and TSPL_AssetIssueReturnDetail.Asset_Code='" + gvIncrement.Rows(ii).Cells(colAssetCode).Value + "' "
                        Dim no As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(query))

                        If no >= 1 Then
                            Throw New Exception("Asset Code: " + gvIncrement.Rows(ii).Cells(colAssetCode).Value + " already Issued ")
                        End If
                    End If

                Next
                If clsCommon.myLen(gvIncrement.Rows(ii).Cells(colAssetCode).Value) > 0 Then
                    Counter += 1
                End If
            End If
        Next
        If Counter <= 0 Then
            Throw New Exception("Please select atleast single Asset to Save.")
        End If
        Return True
    End Function

    Private Sub txtIssueTo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtIssueTo._MYValidating
        Dim obj As clsEmployeeMaster = clsEmployeeMaster.FinderForEmployee("", True)
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.EMP_CODE) > 0 Then
            txtIssueTo.Value = obj.EMP_CODE
            lblIssueToName.Text = obj.Emp_Name
        Else
            txtIssueTo.Value = ""
            lblIssueToName.Text = ""
        End If
        'Dim qry As String = "select user_code as [Code],USER_NAME as [User Name] from Tspl_USER_MASTER  "
        'txtIssueTo.Value = clsCommon.ShowSelectForm("User_Fnd", qry, "Code", " Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' ", txtIssueTo.Value, "", isButtonClicked)
        'lblIssueToName.Text = clsUserMaster.GetName(txtIssueTo.Value, Nothing)
    End Sub

    Private Sub gvIncrement_CellEndEdit(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvIncrement.CellEndEdit
        If Not isCellValueChangedOpen Then
            isCellValueChangedOpen = True

            If e.Column Is gvIncrement.Columns(colAssetCode) Then
                Dim qry As String = " select Asset_Code as [Code], Asset_Description as [Description],serial_no as [Serial No,],Asset_Specification as [Specification],TSPL_Asset_Type_Master.Asset_Type_Description as [Asset Type] from TSPL_Asset_Details " & _
                "left outer join TSPL_Asset_Type_Master on TSPL_Asset_Type_Master.Asset_Type_Code = TSPL_Asset_Details.Asset_Type_Code"
                Dim whrclas As String = "  TSPL_Asset_Details.Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
                gvIncrement.CurrentRow.Cells(colAssetCode).Value = clsCommon.myCstr(clsCommon.ShowSelectForm("AssetFnd", qry, "Code", whrclas, clsCommon.myCstr(gvIncrement.CurrentRow.Cells(colAssetCode).Value), "Code", False))
                qry = " select Asset_Code,Asset_Description,serial_no,TSPL_Asset_Details.Asset_Type_Code, TSPL_Asset_Type_Master.Asset_Type_Description from TSPL_Asset_Details " & _
                      " left outer join TSPL_Asset_Type_Master on TSPL_Asset_Type_Master.Asset_Type_Code = TSPL_Asset_Details.Asset_Type_Code where Asset_Code = '" + clsCommon.myCstr(gvIncrement.CurrentRow.Cells(colAssetCode).Value) + "' "
                Dim dttemp As DataTable = clsDBFuncationality.GetDataTable(qry)

                If dttemp IsNot Nothing AndAlso dttemp.Rows.Count > 0 Then
                    gvIncrement.CurrentRow.Cells(colAssetCode).Value = clsCommon.myCstr(dttemp.Rows(0)("Asset_Code"))
                    gvIncrement.CurrentRow.Cells(colAssetDescription).Value = clsCommon.myCstr(dttemp.Rows(0)("Asset_Description"))
                    'gvIncrement.CurrentRow.Cells(colAssetSno).Value = clsCommon.myCstr(dttemp.Rows(0)("serial_no"))
                    gvIncrement.CurrentRow.Cells(colAssetTypeCode).Value = clsCommon.myCstr(dttemp.Rows(0)("Asset_Type_Code"))
                    'gvIncrement.CurrentRow.Cells(colAssetTypeDesc).Value = clsCommon.myCstr(dttemp.Rows(0)("Asset_Type_Description"))
                Else
                    gvIncrement.CurrentRow.Cells(colAssetCode).Value = ""
                    gvIncrement.CurrentRow.Cells(colAssetDescription).Value = ""
                    'gvIncrement.CurrentRow.Cells(colAssetSno).Value = ""
                    'gvIncrement.CurrentRow.Cells(colAssetTypeDesc).Value = ""
                    gvIncrement.CurrentRow.Cells(colAssetTypeCode).Value = ""
                End If
            End If
            isCellValueChangedOpen = False
        End If
    End Sub

    Private Sub gvIncrement_UserDeletedRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gvIncrement.UserDeletedRow
        For ii As Integer = 1 To gvIncrement.Rows.Count
            gvIncrement.Rows(ii - 1).Cells(colLineNo).Value = ii
        Next
    End Sub

    Private Sub gvIncrement_UserAddedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gvIncrement.UserAddedRow
        For i As Integer = 0 To gvIncrement.Rows.Count - 1
            gvIncrement.Rows(0).Cells(0).Value = 1
            If i <> 0 Then
                gvIncrement.Rows(i).Cells(colLineNo).Value = i + 1
            End If
        Next
    End Sub

    Private Sub gvIncrement_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvIncrement.CurrentColumnChanged
        If gvIncrement.RowCount > 0 Then
            Dim intCurrRow As Integer = gvIncrement.CurrentRow.Index
            gvIncrement.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gvIncrement.Rows.Count - 1 Then
                gvIncrement.Rows.AddNew()
                gvIncrement.CurrentRow = gvIncrement.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "You Cannot Delete Record", Me.Text)
            Exit Sub
        End If
        funDelete()
    End Sub

    Sub funDelete()
        Try
            If (clsCommon.MyMessageBoxShow("Do you want to Delete this record ?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes) Then
                If (clsAssetsIssueReturn.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    funReset()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Sub SavingData(ByVal ChekBtnPost As Boolean)
        If (Save()) Then
            If ChekBtnPost = False Then
                common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
            End If
        End If

    End Sub

    Private Function GetVochType() As DataTable

        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Value", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Issue"
        dr("Value") = "Issue"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Return"
        dr("Value") = "Return"
        dt.Rows.Add(dr)

        dt.AcceptChanges()

        Return dt
    End Function

    Private Sub cboType_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboType.SelectedValueChanged
        If clsCommon.myLen(cboType.SelectedValue) > 0 AndAlso isCboActivated Then
            If clsCommon.CompairString(cboType.SelectedValue, "Return") = CompairStringResult.Equal Then
                lblCode.Text = "Issue No."
                lblIssueTo.Text = "Return From"
                lblDate.Text = "Return Date"
                lblReturnNo.Visible = True
                FndIssueReturn.Visible = True
                FndIssueReturn.Text = ""
                If cboType.SelectedValue = "Return" Then
                    gvIncrement.Columns(colReturnedQty).IsVisible = True
                    gvIncrement.Columns(colSelect).IsVisible = True
                End If
            Else
                lblCode.Text = "Issue No."
                lblIssueTo.Text = "Issue To"
                lblDate.Text = "Issue Date"
                lblReturnNo.Visible = False
                FndIssueReturn.Visible = False
                LoadGridColumns()
                gvIncrement.Columns(colReturnedQty).IsVisible = False
                gvIncrement.Columns(colReturnedQty).ReadOnly = True
                gvIncrement.Columns(colSelect).IsVisible = False

            End If
        End If

    End Sub

    Private Sub FndIssueReturn__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles FndIssueReturn._MYValidating
        Dim qry As String = ""
        qry = "select issuecode as [Code],IssueDate as [Issue Date],issueto as [Issue To],emp_name as [Issue To Name],Remark from TSPL_AssetIssueReturn left join " _
            & " tspl_employee_master on tspl_employee_master.emp_code= TSPL_AssetIssueReturn.issueto "

        FndIssueReturn.Value = clsCommon.ShowSelectForm("VSPISSUENo", qry, "Code", "type='Issue'  and Posted=1 and convert(date,Issuedate,103)<=convert(date,'" & dtpDate.Value & "',103) ", FndIssueReturn.Value, "Code", isButtonClicked)

        If clsCommon.myLen(FndIssueReturn.Value) > 0 Then
            txtIssueTo.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT  ISNULL(Issue_To,'' ) As [Issue To] FROM TSPL_VSPAsset_HEAD Where Doc_No='" + clsCommon.myCstr(FndIssueReturn.Value) + "'"))
            If clsCommon.myLen(txtIssueTo.Value) > 0 Then
                lblIssueToName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ISNULL( emp_Name,'') As emp_Name  from TSPL_Employee_MASTER where Emp_Code ='" + clsCommon.myCstr(txtIssueTo.Value) + "'"))
            End If
            txtDescription.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT  isnull(Remark,'' ) As Remark  FROM TSPL_AssetIssueReturn Where Issuecode='" + clsCommon.myCstr(FndIssueReturn.Value) + "'"))
            LoadIssueData(clsCommon.myCstr(FndIssueReturn.Value), NavigatorType.Current)
        Else
            txtIssueTo.Value = ""
            lblIssueToName.Text = ""
            txtDescription.Text = ""
            gvIncrement.Rows.Clear()
        End If
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles BtnPost.Click
        Try
            Dim sQuery As String = "Update TSPL_AssetIssueReturn set posted=1,posting_date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd-MMM-yyyy hh:mm:ss tt") & "' where Issuecode='" & clsCommon.myCstr(txtCode.Value) & "'"
            clsDBFuncationality.ExecuteNonQuery(sQuery)

            clsCommon.MyMessageBoxShow(Me, "Posted Successfully.", Me.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

    Private Sub gvIncrement_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvIncrement.CellValueChanged
        Try
            If isCellValueChangedOpen = False Then
                isCellValueChangedOpen = True
                If e.Column Is gvIncrement.Columns(colReturnedQty) Then
                    If clsCommon.myCdbl(gvIncrement.CurrentRow.Cells(colReturnedQty).Value) > clsCommon.myCdbl(gvIncrement.CurrentRow.Cells(colQty).Value) Then
                        clsCommon.MyMessageBoxShow(Me, "Return Qty can not be Greater then Issue Qty", Me.Text)
                        gvIncrement.CurrentRow.Cells(colReturnedQty).Value = 0
                    End If
                    isCellValueChangedOpen = False
                ElseIf e.Column Is gvIncrement.Columns(colQty) Then
                    isCellValueChangedOpen = True
                    Dim str As String = "select sum(Qty) from TSPL_Inventory_Movement where Item_Code='" + gvIncrement.CurrentRow.Cells(colAssetCode).Value + "'  "
                    Dim stockqty As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
                    If clsCommon.myLen(clsCommon.myCstr(gvIncrement.CurrentRow.Cells(colAssetCode).Value)) > 0 Then
                        If stockqty < clsCommon.myCdbl(gvIncrement.CurrentRow.Cells(colQty).Value) Then
                            clsCommon.MyMessageBoxShow(Me, "Stock not Avilable of This Item.Its Stock is [" & stockqty & "]", Me.Text)
                            gvIncrement.CurrentRow.Cells(colQty).Value = 0
                        End If
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class