'Main form
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports System.IO
Public Class frmEmployeeSavingsMapping
    Inherits FrmMainTranScreen
    ' Ticket No : BHA/31/12/18-000769 By Prabhakar Create New screen 
    'colLineNo, colSavingCode, colSavingDesc, colSectionCode, colSectionDesc , colAmount
#Region "Variables"
    Private isNewEntry As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Const colLineNo As String = "LineNo"
    Const colSavingCode As String = "SavingCode"
    Const colSavingDesc As String = "SavingDesc"
    Const colSectionCode As String = "SectionCode"
    Const colSectionDesc As String = "SectionDesc"
    Const colMaxLimit As String = "MaxLimit"
    Const colAmount As String = "Amount"
    'colLineNoForTotal, colSeactionCodeForTotal , colSeactionDescForTotal , colAmountForTotal  
    Const colLineNoForTotal As String = "LineNoForTotal"
    Const colSeactionCodeForTotal As String = "SeactionCodeForTotal"
    Const colSeactionDescForTotal As String = "SeactionDescForTotal"
    Const colMaxLimitForTotal As String = "MaxLimitForTotal"
    Const colAmountForTotal As String = "AmountForTotal"
    ' for Attachment ================================
    Const colTRCode As String = "TRCode"
    Const ColFileName As String = "FileName"
    Const ColPath As String = "ColPath"
    Const ColView As String = "View"
    Const ColDelete As String = "Delete"
    Const ColSelect As String = "ColSelect"
    Const colAddNewAttachment As String = "colAddNewAttachemnt"
    '=================================================
    Private isInsideLoadData As Boolean = False
    Public isDeleteTheAttachment As Boolean = True
    Dim br As BinaryReader
    Dim SettNoOFSavingCodeForImportExport As Integer
#End Region

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        If btnSave.Visible = True Then
            rmExport.Enabled = True
            rmImport.Enabled = True
        Else
            rmExport.Enabled = False
            rmImport.Enabled = False
        End If
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmDepreciationField_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        isInsideLoadData = True

        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        AddNew()
        SetLength()
        SettNoOFSavingCodeForImportExport = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.NoOFSavingCodeForImportExport, clsFixedParameterCode.NoOFSavingCodeForImportExport, Nothing))
        isInsideLoadData = False
    End Sub

    Sub SetLength()
        txtCode.MyMaxLength = 30
    End Sub
    Sub AddNew()
        txtCode.MyReadOnly = False
        BlankAllControls()
        isNewEntry = True
        btnSave.Text = "Save"
    End Sub

    Sub BlankAllControls()
        txtCode.Value = ""
        txtEmployee.Value = ""
        lblEmployeeCode.Text = ""
        txtFinancialYear.Value = objCommonVar.CurrFiscalYear
        LoadBlankDetailsGrid()
        LoadBlankTotalGrid()
        isInsideLoadData = False
    End Sub

    Sub LoadBlankDetailsGrid()
        gv.Rows.Clear()
        gv.Columns.Clear()

        gv.Columns.Add(colLineNo, "SNo")
        gv.Columns(colLineNo).Width = 50
        gv.Columns(colLineNo).ReadOnly = True

        Dim repoTRCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTRCode.FormatString = ""
        repoTRCode.HeaderText = "TR Code"
        repoTRCode.Name = colTRCode
        repoTRCode.Width = 50
        repoTRCode.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoTRCode)

        Dim repoSavingCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSavingCode.FormatString = ""
        repoSavingCode.HeaderText = "Saving Code"
        repoSavingCode.Name = colSavingCode
        repoSavingCode.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4 ' Global.ERP.My.Resources.Resources.search4
        repoSavingCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoSavingCode.Width = 100
        gv.MasterTemplate.Columns.Add(repoSavingCode)

        Dim repoSavingDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSavingDesc.FormatString = ""
        repoSavingDesc.HeaderText = "Saving Description"
        repoSavingDesc.Name = colSavingDesc
        repoSavingDesc.Width = 150
        repoSavingDesc.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoSavingDesc)

        Dim repoSectionCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSectionCode.FormatString = ""
        repoSectionCode.HeaderText = "Section Code"
        repoSectionCode.Name = colSectionCode
        repoSectionCode.Width = 150
        repoSectionCode.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoSectionCode)

        Dim repoSectionDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSectionDesc.FormatString = ""
        repoSectionDesc.HeaderText = "Section Description"
        repoSectionDesc.Name = colSectionDesc
        repoSectionDesc.Width = 150
        repoSectionDesc.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoSectionDesc)

        Dim repMaxLimit As GridViewDecimalColumn = New GridViewDecimalColumn()
        repMaxLimit.FormatString = "{0:n2}"
        repMaxLimit.HeaderText = "Max Limit"
        repMaxLimit.Name = colMaxLimit
        repMaxLimit.Width = 80
        repMaxLimit.Minimum = 0
        repMaxLimit.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repMaxLimit.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repMaxLimit)

        Dim repAmount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repAmount.FormatString = "{0:n2}"
        repAmount.HeaderText = "Amount"
        repAmount.Name = colAmount
        repAmount.Width = 80
        repAmount.Minimum = 0
        repAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repAmount)

        '========================================================================
        Dim rptoUpload As New GridViewCommandColumn()
        rptoUpload.FormatString = ""
        rptoUpload.UseDefaultText = True
        rptoUpload.DefaultText = "Upload"
        rptoUpload.HeaderText = "Attachment"
        rptoUpload.Name = colAddNewAttachment
        rptoUpload.FieldName = colAddNewAttachment
        rptoUpload.Width = 70
        rptoUpload.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv.Columns.Add(rptoUpload)


        Dim repoGroupCod As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoGroupCod.FormatString = ""
        repoGroupCod.HeaderText = "File Name"
        repoGroupCod.Width = 200
        repoGroupCod.Name = ColFileName
        repoGroupCod.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoGroupCod)

        Dim repoPath As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPath.FormatString = ""
        repoPath.HeaderText = "Path"
        repoPath.Width = 100
        repoPath.Name = ColPath
        repoPath.ReadOnly = True
        repoPath.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoPath)

        Dim ShowBtn As New GridViewCommandColumn()
        ShowBtn.FormatString = ""
        ShowBtn.UseDefaultText = True
        ShowBtn.DefaultText = "Show"
        ShowBtn.HeaderText = "Show"
        ShowBtn.Name = ColView
        ShowBtn.FieldName = colLineNo
        ShowBtn.Width = 70
        ShowBtn.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv.Columns.Add(ShowBtn)

        Dim repoSelect As GridViewCommandColumn = New GridViewCommandColumn()
        repoSelect.FormatString = ""
        repoSelect.UseDefaultText = True
        repoSelect.DefaultText = "ADD"
        repoSelect.HeaderText = "ADD"
        repoSelect.Width = 70
        repoSelect.Name = ColSelect
        repoSelect.IsVisible = False
        repoSelect.FieldName = colLineNo
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv.MasterTemplate.Columns.Add(repoSelect)

        Dim repoType As GridViewCommandColumn = New GridViewCommandColumn()
        repoType.FormatString = ""
        repoType.UseDefaultText = True
        repoType.DefaultText = "Delete"
        repoType.HeaderText = "Delete"
        repoType.Width = 100
        repoType.Name = ColDelete
        repoType.FieldName = colLineNo
        repoType.IsVisible = isDeleteTheAttachment
        repoType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv.MasterTemplate.Columns.Add(repoType)

        '========================================================================

        gv.AllowAddNewRow = False
        gv.AllowDeleteRow = True
        gv.AllowRowReorder = False
        gv.ShowGroupPanel = False
        gv.EnableFiltering = False
        gv.EnableSorting = False
        gv.EnableGrouping = False
        gv.AllowColumnChooser = True
        gv.AllowColumnReorder = True
        gv.Rows.AddNew()
    End Sub
    'colLineNoForTotal, colSeactionCodeForTotal , colSeactionDescForTotal , colAmountForTotal
    Sub LoadBlankTotalGrid()
        gvTotal.DataSource = Nothing
        'gvTotal.Rows.Clear()
        'gvTotal.Columns.Clear()

        'gvTotal.Columns.Add(colLineNoForTotal, "SNo")
        'gvTotal.Columns(colLineNoForTotal).Width = 50
        'gvTotal.Columns(colLineNoForTotal).ReadOnly = True

        'Dim repoSectionCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'repoSectionCode.FormatString = ""
        'repoSectionCode.HeaderText = "Section Code"
        'repoSectionCode.Name = colSeactionCodeForTotal
        'repoSectionCode.Width = 150
        'repoSectionCode.ReadOnly = False
        'gvTotal.MasterTemplate.Columns.Add(repoSectionCode)

        'Dim repoSectionDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'repoSectionDesc.FormatString = ""
        'repoSectionDesc.HeaderText = "Section Description"
        'repoSectionDesc.Name = colSeactionDescForTotal
        'repoSectionDesc.Width = 150
        'repoSectionDesc.ReadOnly = False
        'gvTotal.MasterTemplate.Columns.Add(repoSectionDesc)

        'Dim repMaxLimit As GridViewDecimalColumn = New GridViewDecimalColumn()
        'repMaxLimit.FormatString = "{0:n2}"
        'repMaxLimit.HeaderText = "Max Limit"
        'repMaxLimit.Name = colMaxLimitForTotal
        'repMaxLimit.Width = 80
        'repMaxLimit.Minimum = 0
        'repMaxLimit.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'repMaxLimit.ReadOnly = True
        'gvTotal.MasterTemplate.Columns.Add(repMaxLimit)

        'Dim repAmount As GridViewDecimalColumn = New GridViewDecimalColumn()
        'repAmount.FormatString = "{0:n2}"
        'repAmount.HeaderText = "Amount"
        'repAmount.Name = colAmountForTotal
        'repAmount.Width = 80
        'repAmount.Minimum = 0
        'repAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gvTotal.MasterTemplate.Columns.Add(repAmount)

        gvTotal.AllowAddNewRow = False
        gvTotal.AllowDeleteRow = False
        gvTotal.AllowRowReorder = False
        gvTotal.ShowGroupPanel = False
        gvTotal.EnableFiltering = False
        gvTotal.EnableSorting = False
        gvTotal.EnableGrouping = False
        'gvTotal.AllowColumnChooser = True
        'gvTotal.AllowColumnReorder = True
        'gvTotal.Rows.AddNew()
    End Sub


    Function AllowToSave() As Boolean

        If clsCommon.myLen(txtFinancialYear.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select Financial Year.", Me.Text)
            txtFinancialYear.Focus()
            Return False
        End If

        If clsCommon.myLen(txtEmployee.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Employee", Me.Text)
            txtEmployee.Focus()
            Return False
        End If

        Dim ExistDocumentCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_Code from TSPL_EMPLOYEE_SAVINGS_MAPPING_MASTER where Emp_code = '" + txtEmployee.Value + "' and Fiscal_Code = '" + txtFinancialYear.Value + "'"))
        If clsCommon.myLen(ExistDocumentCode) > 0 AndAlso (clsCommon.CompairString(ExistDocumentCode, txtCode.Value) <> CompairStringResult.Equal) Then
            clsCommon.MyMessageBoxShow(Me, "Document Code (" + ExistDocumentCode + ") already exist for Employee (" + txtEmployee.Value + ") in Financial Year (" + txtFinancialYear.Value + ").")
            Return False
        End If
        Dim count As Integer = 0
        If gv.Rows.Count > 0 Then
            For ii As Integer = 0 To gv.RowCount - 1
                If clsCommon.myLen(clsCommon.myCstr(gv.Rows(ii).Cells(colSavingCode).Value)) > 0 Then
                    count += 1
                    If String.IsNullOrEmpty(clsCommon.myCstr(gv.Rows(ii).Cells(colAmount).Value)) = True Then
                        clsCommon.MyMessageBoxShow(Me, "Please Enter amount . At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ", Me.Text)
                        Return False
                    End If

                End If
            Next
        End If
        If count = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Enter Saving Code.", Me.Text)
            Return False
        End If

        Return True
    End Function

    Private Sub SaveData()
        Try
            If (AllowToSave()) Then

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmSavingsMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
                Dim arr As New List(Of clsEmployeeSavingsMappingHead)

                Dim obj As New clsEmployeeSavingsMappingHead()
                obj.DOCUMENT_CODE = txtCode.Value
                obj.EMP_CODE = txtEmployee.Value
                obj.Fiscal_Code = txtFinancialYear.Value
                arr.Add(obj)
                Dim Arr2 As New List(Of clsEmployeeSavingsMappingDetail)
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim objTr As New clsEmployeeSavingsMappingDetail()
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colSavingCode).Value)) > 0 Then
                        objTr.Path = clsCommon.myCstr(grow.Cells(colTRCode).Value) ' TR Code pass in Path Variable
                        objTr.SNO = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                        objTr.SAVINGS_CODE = clsCommon.myCstr(grow.Cells(colSavingCode).Value)
                        objTr.Section_Code = clsCommon.myCstr(grow.Cells(colSectionCode).Value)
                        objTr.MaxLimit = clsCommon.myCstr(grow.Cells(colMaxLimit).Value)
                        objTr.AMOUNT = clsCommon.myCstr(grow.Cells(colAmount).Value)
                        If String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells(ColPath).Value)) = False AndAlso clsCommon.myLen(clsCommon.myCstr(grow.Cells(colTRCode).Value)) <= 0 Then
                            objTr.FileName = clsCommon.myCstr(grow.Cells(ColFileName).Value)
                            br = New BinaryReader(System.IO.File.OpenRead(clsCommon.myCstr(grow.Cells(ColPath).Value)))
                            objTr.FileData = br.ReadBytes(br.BaseStream.Length)
                        End If
                        Arr2.Add(objTr)
                    End If
                Next
                'If Arr2.Count <= 0 Then
                '    clsCommon.MyMessageBoxShow("Please Enter atlest one row in Saving Code in grid.")
                '    Return
                'End If
                If obj.SaveData(arr, Arr2, isNewEntry) Then
                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(obj.DOCUMENT_CODE, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            isInsideLoadData = True
            isNewEntry = False
            'btnSave.Text = "Update"
            BlankAllControls()
            txtCode.MyReadOnly = True
            Dim obj As New clsEmployeeSavingsMappingHead()
            obj = clsEmployeeSavingsMappingHead.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.DOCUMENT_CODE) > 0) Then
                txtCode.Value = obj.DOCUMENT_CODE
                txtFinancialYear.Value = obj.Fiscal_Code
                txtEmployee.Value = obj.EMP_CODE
                lblEmployeeCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Emp_Name from TSPL_EMPLOYEE_MASTER where EMP_Code='" + obj.EMP_CODE + "'"))

                '====================================
                Dim count As Boolean = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_EMPLOYEE_SAVINGS_MAPPING_DETAIL where DOCUMENT_CODE = '" + obj.DOCUMENT_CODE + "'"))
                If count = True Then
                    For Each objtr As clsEmployeeSavingsMappingDetail In obj.ArrEmployeeSavingsMappingDetails
                        gv.CurrentRow.Cells(colLineNo).Value = objtr.SNO
                        gv.CurrentRow.Cells(colTRCode).Value = objtr.TR_CODE
                        gv.CurrentRow.Cells(colSavingCode).Value = objtr.SAVINGS_CODE
                        gv.CurrentRow.Cells(colSavingDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Description from TSPL_SAVINGS_MASTER where SAVINGS_CODE='" + objtr.SAVINGS_CODE + "'"))
                        gv.CurrentRow.Cells(colSectionCode).Value = objtr.Section_Code
                        gv.CurrentRow.Cells(colSectionDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Description from TSPL_SECTION_ALLOWANCE_MASTER where Code='" + objtr.Section_Code + "'"))
                        gv.CurrentRow.Cells(colMaxLimit).Value = objtr.MaxLimit
                        gv.CurrentRow.Cells(colAmount).Value = objtr.AMOUNT
                        gv.CurrentRow.Cells(ColFileName).Value = objtr.FileName
                        gv.Rows.AddNew()
                    Next

                    Dim qry As String = " select Row_Number () over (Order by TSPL_EMPLOYEE_SAVINGS_MAPPING_DETAIL.Section_Code asc) as[SNO], TSPL_EMPLOYEE_SAVINGS_MAPPING_DETAIL.Section_Code as [Section Code] ,max(TSPL_SECTION_ALLOWANCE_MASTER.Description) as [Section Description], max (TSPL_EMPLOYEE_SAVINGS_MAPPING_DETAIL.MaxLimit) as [Max Limit],sum( TSPL_EMPLOYEE_SAVINGS_MAPPING_DETAIL.Amount) as [Amount]   from TSPL_EMPLOYEE_SAVINGS_MAPPING_DETAIL left outer join TSPL_SECTION_ALLOWANCE_MASTER on TSPL_SECTION_ALLOWANCE_MASTER.code =TSPL_EMPLOYEE_SAVINGS_MAPPING_DETAIL.Section_Code   where TSPL_EMPLOYEE_SAVINGS_MAPPING_DETAIL.Document_Code = '" + obj.DOCUMENT_CODE + "' group by TSPL_EMPLOYEE_SAVINGS_MAPPING_DETAIL.Section_code "
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                        gvTotal.DataSource = dt
                        gvTotal.BestFitColumns()
                    End If

                End If
                '===================================
                isInsideLoadData = False
                btnSave.Text = "Update"
            End If
        Catch ex As Exception
            isInsideLoadData = False
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        LoadData(txtCode.Value, NavType)
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Try
            Dim qst As String = "select count(*) from TSPL_EMPLOYEE_SAVINGS_MAPPING_MASTER where DOCUMENT_CODE='" + txtCode.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If
            If txtCode.MyReadOnly OrElse isButtonClicked Then


                LoadData(clsEmployeeSavingsMappingHead.getFinder("", txtCode.Value, isButtonClicked), NavigatorType.Current)
                LoadData(txtCode.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FrmDepreciationField_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsEmployeeSavingsMappingHead.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub

    Sub CloseForm()
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            SaveData()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    Private Sub rmExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmExport.Click
        'Dim strDetail As String
        'strDetail = " select TSPL_SAVINGS_MASTER.SAVINGS_CODE as [Savings Code] ,TSPL_SAVINGS_MASTER.Description as [Description] ,TSPL_SAVINGS_MASTER.Section_Code as [Section Code],tspl_Section_master.Description as [Section Desc]  From TSPL_SAVINGS_MASTER left join tspl_Section_master on TSPL_SECTION_ALLOWANCE_MASTER.Code=TSPL_SAVINGS_MASTER.Section_Code "
        'transportSql.ExporttoExcel(strDetail, Me)
        Dim str As String
        str = "select '' as [Financial Year],'' as [Employee Code]"
        For ii As Integer = 1 To SettNoOFSavingCodeForImportExport
            str += ",'' as [Saving Code " + clsCommon.myCstr(ii) + "]"
            str += ",'' as [Amount " + clsCommon.myCstr(ii) + "]"
        Next
        transportSql.ExporttoExcelWithoutFilter(str, "", "", Me)
    End Sub

    Private Sub rmImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmImport.Click
        Dim qry As String = ""
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim trans As SqlTransaction = Nothing
        Try
            Dim currentdate As Date = Date.Today
            Dim linno As Integer = 0
            Dim Strs As List(Of String) = New List(Of String)
            Strs.Add("Financial Year")
            Strs.Add("Employee Code")
            For ii As Integer = 1 To SettNoOFSavingCodeForImportExport
                Strs.Add("Saving Code " + clsCommon.myCstr(ii))
                Strs.Add("Amount " + clsCommon.myCstr(ii))
            Next

            If transportSql.importExcel(gv, Strs.ToArray()) Then
                ' Dim trans As SqlTransaction = Nothing
                Try
                    trans = clsDBFuncationality.GetTransactin()
                    clsCommon.ProgressBarShow()
                    For Each grow As GridViewRowInfo In gv.Rows
                        Dim obj As New clsEmployeeSavingsMappingHead()
                        Dim arr As New List(Of clsEmployeeSavingsMappingHead)
                        linno += 1
                        obj.Fiscal_Code = clsCommon.myCstr(grow.Cells("Financial Year").Value)
                        If clsCommon.myLen(obj.Fiscal_Code) > 0 Then
                            qry = "Select count (*) from TSPL_Fiscal_Year_Master where Fiscal_Code = '" + obj.Fiscal_Code + "'"
                            Dim isFinancialYear As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue(qry, trans))
                            If isFinancialYear = False Then
                                Throw New Exception("Invalid Financial Year at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                            obj.EMP_CODE = clsCommon.myCstr(grow.Cells("Employee Code").Value)
                            If clsCommon.myLen(obj.EMP_CODE) > 0 Then
                                qry = "Select count(*) from TSPL_EMPLOYEE_MASTER where EMP_CODE = '" + obj.EMP_CODE + "'"
                                Dim isValidEmpCode As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue(qry, trans))
                                If isValidEmpCode = False Then
                                    Throw New Exception("Invalid Employee Code at line no. " + clsCommon.myCstr(linno) + ".")
                                End If
                                Dim ExistDocumentCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_Code from TSPL_EMPLOYEE_SAVINGS_MAPPING_MASTER where Emp_code = '" + obj.EMP_CODE + "' and Fiscal_Code = '" + obj.Fiscal_Code + "'", trans))
                                If clsCommon.myLen(ExistDocumentCode) > 0 Then
                                    Throw New Exception("Document Code (" + ExistDocumentCode + ") already exist for Employee (" + obj.EMP_CODE + ") in Financial Year (" + obj.Fiscal_Code + ")  at line no. " + clsCommon.myCstr(linno) + ".")
                                End If
                            Else
                                Throw New Exception("Employee Code Can't be blank at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                            arr.Add(obj)
                            '==================================================
                            Dim Arr2 As New List(Of clsEmployeeSavingsMappingDetail)
                            Dim strSNO As Integer = 1
                            For i As Integer = 1 To SettNoOFSavingCodeForImportExport
                                Dim objTr As New clsEmployeeSavingsMappingDetail()
                                Dim strSavingCode As String = "Saving Code " + clsCommon.myCstr(i)
                                Dim strAmount As String = "Amount " + clsCommon.myCstr(i)
                                If clsCommon.myLen(clsCommon.myCstr(grow.Cells(strSavingCode).Value)) > 0 Then
                                    Dim chkSavingCode As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count (*) from TSPL_SAVINGS_MASTER where SAVINGS_CODE = '" + clsCommon.myCstr(grow.Cells(strSavingCode).Value) + "'", trans))
                                    If chkSavingCode <= 0 Then
                                        Throw New Exception("Invalid Saving Code " + strSavingCode + " Code At Line No. " + clsCommon.myCstr(linno) + ".")
                                    End If
                                    objTr.SAVINGS_CODE = clsCommon.myCstr(grow.Cells(strSavingCode).Value)

                                    If chkSavingCode > 0 AndAlso String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells(strAmount).Value)) = False Then
                                        If clsCommon.myCdbl(grow.Cells(strAmount).Value) Then
                                            objTr.AMOUNT = clsCommon.myCdbl(grow.Cells(strAmount).Value) 'clsCommon.myCdbl(grow.Cells("Amount").Value)
                                        Else
                                            Throw New Exception("Amount Should be Numeric At Line No. " + clsCommon.myCstr(linno) + ".")
                                        End If
                                    Else
                                        Throw New Exception("Invalid Amount At Line No. " + clsCommon.myCstr(linno) + ".")
                                    End If
                                    If chkSavingCode > 0 Then


                                        '*******************************************

                                        qry = "select  Section_Code , TSPL_SECTION_ALLOWANCE_MASTER.MAX_LIMIT from TSPL_SAVINGS_MASTER  left outer join TSPL_SECTION_ALLOWANCE_MASTER on TSPL_SECTION_ALLOWANCE_MASTER.CODE = TSPL_SAVINGS_MASTER.Section_Code where TSPL_SAVINGS_MASTER.Savings_Code = '" + objTr.SAVINGS_CODE + "'"
                                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                            For Each dr As DataRow In dt.Rows
                                                objTr.SNO = strSNO
                                                objTr.Section_Code = clsCommon.myCstr(dr("Section_Code"))
                                                objTr.MaxLimit = clsCommon.myCstr(dr("MAX_LIMIT"))
                                            Next
                                        End If
                                        '*******************************************
                                        Arr2.Add(objTr)
                                        strSNO += 1
                                    End If
                                End If

                            Next
                            '==================================================
                            obj.SaveData(arr, Arr2, isNewEntry, trans)
                            
                        End If
                    Next
                    trans.Commit()
                    clsCommon.ProgressBarHide()
                    clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    ' trans.Rollback()
                    clsCommon.ProgressBarHide()
                    Throw New Exception("Error at Line No" + clsCommon.myCstr(linno) + Environment.NewLine + ex.Message)
                End Try
            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            Me.Controls.Remove(gv)
        End Try

        'Dim gv As New RadGridView()
        'Me.Controls.Add(gv)
        'Dim obj As clsSavingMaster = Nothing
        'Dim currentdate As Date = Date.Today
        'Dim trans As SqlTransaction = Nothing
        'If transportSql.importExcel(gv, "Savings Code", "Description", "Section Code", "Section Desc") Then
        '    Dim linno As Integer = 0
        '    Try
        '        Dim arr As New List(Of clsSavingMaster)
        '        clsCommon.ProgressBarShow()
        '        trans = clsDBFuncationality.GetTransactin()
        '        For Each grow As GridViewRowInfo In gv.Rows
        '            obj = New clsSavingMaster
        '            linno += 1
        '            Dim strcode As String = clsCommon.myCstr(grow.Cells("Savings Code").Value)
        '            If (String.IsNullOrEmpty(strcode)) Or clsCommon.myLen(strcode) > 30 Then
        '                Throw New Exception("Length of Code should be max. 30 character At Line No. " + clsCommon.myCstr(linno) + ".")
        '            End If
        '            obj.SAVINGS_CODE = strcode

        '            Dim strDesp As String = clsCommon.myCstr(grow.Cells("Description").Value)
        '            If (String.IsNullOrEmpty(strDesp)) Or clsCommon.myLen(strDesp) > 100 Then
        '                Throw New Exception("Length of Description should be max. 100 character At Line No. " + clsCommon.myCstr(linno) + ".")
        '            End If
        '            obj.Description = strDesp



        '            Dim strType As String = clsCommon.myCstr(grow.Cells("Section Code").Value)

        '            Dim strSectionCode As Boolean = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select count(*) from TSPL_SECTION_ALLOWANCE_MASTER where Code ='" + strType + "'  ", trans))
        '            If strSectionCode = False Then
        '                Throw New Exception("Invalid Section Code. At Line No. " + clsCommon.myCstr(linno) + ".")
        '            End If
        '            obj.Section_Code = strType


        '            If clsCommon.myLen(strcode) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from TSPL_SAVINGS_MASTER  where SAVINGS_CODE='" + strcode + "' ", trans) > 0 Then
        '                isNewEntry = False
        '            Else
        '                isNewEntry = True
        '            End If
        '            arr.Add(obj)
        '            obj.SaveData(arr, isNewEntry, trans)
        '        Next
        '        trans.Commit()
        '        clsCommon.ProgressBarHide()
        '        common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
        '    Catch ex As Exception
        '        trans.Rollback()
        '        clsCommon.ProgressBarHide()
        '        myMessages.myExceptions(ex)
        '    End Try
        'End If
        'Me.Controls.Remove(gv)
    End Sub
    Private Sub txtEmployee__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtEmployee._MYValidating
        Dim qry As String = "select EMP_Code as Code , Emp_Name as Name from TSPL_EMPLOYEE_MASTER"
        txtEmployee.Value = clsCommon.ShowSelectForm("Employee@Finder", qry, "Code", "", txtEmployee.Value, "", isButtonClicked)
        lblEmployeeCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Emp_Name from TSPL_EMPLOYEE_MASTER where EMP_Code='" + txtEmployee.Value + "'"))
        Dim ExistDocumentCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_Code from TSPL_EMPLOYEE_SAVINGS_MAPPING_MASTER where Emp_code = '" + txtEmployee.Value + "' and Fiscal_Code = '" + txtFinancialYear.Value + "'"))
        If clsCommon.myLen(ExistDocumentCode) > 0 Then
            LoadData(ExistDocumentCode, NavigatorType.Current)
        End If
    End Sub

    Private Sub txtFinancialYear__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtFinancialYear._MYValidating
        Dim qry As String = " select Fiscal_Code as [Code],convert (varchar, Start_Date,103) as [Start_Date] ,convert (varchar, End_Date,103) as [End Date]  from TSPL_Fiscal_Year_Master "
        txtFinancialYear.Value = clsCommon.ShowSelectForm("FinancialYear@Finder", qry, "Code", "", txtFinancialYear.Value, "", isButtonClicked)
    End Sub

    Private Sub gv_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv.CellValueChanged
        Try
            If (Not isInsideLoadData) Then ' If isInsideLoadData = False Then
                If e.Column Is gv.Columns(colSavingCode) Then
                    OpenSavingCode(False)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub OpenSavingCode(ByVal isButtonClick As Boolean)
        'Dim strSavingCode As String = clsCommon.myCstr(gv.CurrentRow.Cells(colSavingCode).Value)
        'If clsCommon.myLen(strICode) > 0 Then
        Dim qry As String = " select TSPL_SAVINGS_MASTER.Savings_Code as [Code] , TSPL_SAVINGS_MASTER.Description as [Description],TSPL_SAVINGS_MASTER.Section_Code as [Section Code],TSPL_SECTION_ALLOWANCE_MASTER.Description as [Section Desc] , TSPL_SECTION_ALLOWANCE_MASTER.Max_Limit as [Max Limit] from TSPL_SAVINGS_MASTER left outer join TSPL_SECTION_ALLOWANCE_MASTER on TSPL_SECTION_ALLOWANCE_MASTER.Code = TSPL_SAVINGS_MASTER.Section_Code "
        Dim whrCls As String = ""
        gv.CurrentRow.Cells(colSavingCode).Value = clsCommon.ShowSelectForm("Saving@fndnder", qry, "Code", whrCls, clsCommon.myCstr(gv.CurrentRow.Cells(colSavingCode).Value), "Code", isButtonClick)
        qry = " select TSPL_SAVINGS_MASTER.Savings_Code as [Code] , TSPL_SAVINGS_MASTER.Description as [Description],TSPL_SAVINGS_MASTER.Section_Code as [Section Code],TSPL_SECTION_ALLOWANCE_MASTER.Description as [Section Desc] , TSPL_SECTION_ALLOWANCE_MASTER.Max_Limit as [Max Limit],0.0 as Amount from TSPL_SAVINGS_MASTER left outer join TSPL_SECTION_ALLOWANCE_MASTER on TSPL_SECTION_ALLOWANCE_MASTER.Code = TSPL_SAVINGS_MASTER.Section_Code where TSPL_SAVINGS_MASTER.Savings_Code = '" + clsCommon.myCstr(gv.CurrentRow.Cells(colSavingCode).Value) + "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                gv.CurrentRow.Cells(colSavingDesc).Value = clsCommon.myCstr(dr("Description"))
                gv.CurrentRow.Cells(colSectionCode).Value = clsCommon.myCstr(dr("Section Code"))
                gv.CurrentRow.Cells(colSectionDesc).Value = clsCommon.myCstr(dr("Section Desc"))
                gv.CurrentRow.Cells(colMaxLimit).Value = clsCommon.myCstr(dr("Max Limit"))
                gv.CurrentRow.Cells(colAmount).Value = clsCommon.myCstr(dr("Amount"))

                ' For total==========================================================
                'Dim strSectioncode As String = Nothing
                'Dim strAmount As Double = 0.0
                'For Each grow As GridViewRowInfo In gvTotal.Rows
                '    strSectioncode = clsCommon.myCstr(grow.Cells(colSeactionCodeForTotal).Value)
                '    strAmount = clsCommon.myCstr(grow.Cells(colAmountForTotal).Value)
                '    If clsCommon.CompairString(clsCommon.myCstr(dr("Section Code")), strSectioncode) = CompairStringResult.Equal Then
                '        gvTotal.CurrentRow.Cells(colAmountForTotal).Value = clsCommon.myCstr(dr("Amount"))
                '    End If
                'Next
                'gvTotal.CurrentRow.Cells(colSeactionCodeForTotal).Value = clsCommon.myCstr(dr("Section Code"))
                'gvTotal.CurrentRow.Cells(colSeactionDescForTotal).Value = clsCommon.myCstr(dr("Section Desc"))
                'gvTotal.CurrentRow.Cells(colMaxLimitForTotal).Value = clsCommon.myCstr(dr("Max Limit"))
                'gvTotal.CurrentRow.Cells(colAmountForTotal).Value = clsCommon.myCstr(dr("Amount"))
                '====================================================================
            Next
        End If
        ' gv.CurrentRow.Cells(colLineNo).Value = 1


    End Sub


    Private Sub gv_CurrentRowChanged(sender As Object, e As CurrentRowChangedEventArgs) Handles gv.CurrentRowChanged
        If gv.RowCount > 0 Then
            Dim intCurrRow As Integer = gv.CurrentRow.Index
            gv.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv.Rows.Count - 1 Then
                gv.CurrentRow = gv.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gv_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gv.CurrentColumnChanged
        If gv.RowCount > 0 Then
            Dim intCurrRow As Integer = gv.CurrentRow.Index
            gv.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv.Rows.Count - 1 Then
                gv.Rows.AddNew()
                gv.CurrentRow = gv.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gv_UserDeletingRow(sender As Object, e As GridViewRowCancelEventArgs) Handles gv.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
            Dim qry As String = "delete TSPL_EMPLOYEE_SAVINGS_MAPPING_Uttachment from DOCUMENT_CODE = '" + txtCode.Value + "' and TR_CODE = '" + gv.CurrentRow.Cells(colTRCode).Value + "' "
            clsDBFuncationality.ExecuteNonQuery(qry)
        End If
    End Sub

    Private Sub gv_UserDeletedRow(sender As Object, e As GridViewRowEventArgs) Handles gv.UserDeletedRow
        For ii As Integer = 1 To gv.Rows.Count
            gv.Rows(ii - 1).Cells(colLineNo).Value = ii
        Next
    End Sub

    Private Sub gv_CommandCellClick(sender As Object, e As EventArgs) Handles gv.CommandCellClick
        Try
            If (Not isInsideLoadData) Then
                If gv.CurrentColumn Is gv.Columns(ColDelete) AndAlso isDeleteTheAttachment Then
                    If clsCommon.MyMessageBoxShow(" Do you want to Delete This Document.", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                        Dim qry As String = "delete from TSPL_EMPLOYEE_SAVINGS_MAPPING_Uttachment where DOCUMENT_CODE = '" + txtCode.Value + "' and TR_CODE = '" + gv.CurrentRow.Cells(colTRCode).Value + "' "
                        clsDBFuncationality.ExecuteNonQuery(qry)
                        gv.CurrentRow.Cells(ColPath).Value = ""
                        gv.CurrentRow.Cells(ColFileName).Value = ""
                        gv.CurrentRow.Cells(colTRCode).Value = ""
                       
                    End If
                ElseIf gv.CurrentColumn Is gv.Columns(ColView) Then
                    ' ByVal strDocumentCode As String, ByVal strTRCode As String, ByVal strSNO As String, ByVal strSectionCode As String, ByVal strSavingCode As String
                    FunShow(txtCode.Value, gv.CurrentRow.Cells(colTRCode).Value, gv.CurrentRow.Cells(colLineNo).Value, gv.CurrentRow.Cells(colSectionCode).Value, gv.CurrentRow.Cells(colSavingCode).Value)
                ElseIf gv.CurrentColumn Is gv.Columns(ColSelect) Then
                    OpenFileDialog1.ShowDialog()
                    gv.CurrentRow.Cells(ColPath).Value = OpenFileDialog1.FileName
                    gv.CurrentRow.Cells(ColFileName).Value = OpenFileDialog1.SafeFileName
                ElseIf gv.CurrentColumn Is gv.Columns(colAddNewAttachment) Then
                    If clsCommon.myLen(gv.CurrentRow.Cells(ColFileName).Value) > 0 Then
                        clsCommon.MyMessageBoxShow(Me, "First Delete Attached File.SNo = [" + gv.CurrentRow.Cells(colLineNo).Value + "]")
                        Return
                    End If
                    If OpenFileDialog1.ShowDialog = DialogResult.OK Then
                        AddAttachment(OpenFileDialog1.FileName, OpenFileDialog1.SafeFileName)
                    End If

                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Function AddAttachment(ByVal FileName As String, ByVal SafeFileName As String) As Boolean
        Try
            gv.CurrentRow.Cells(ColPath).Value = FileName
            gv.CurrentRow.Cells(ColFileName).Value = SafeFileName
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, "Atttachment", MessageBoxButtons.OK)
        End Try
        Return True
    End Function

    Public Sub FunShow(ByVal strDocumentCode As String, ByVal strTRCode As String, ByVal strSNO As String, ByVal strSectionCode As String, ByVal strSavingCode As String)
        If (clsCommon.myLen(strTRCode) <= 0) Then
            clsCommon.MyMessageBoxShow(Me, "Document not found to View.", Me.Text)
            Return
        End If

        Dim ds_attachment As DataTable
        Dim filename As String = ""
        Dim file_path As String = ""
        Dim file_extn As String = ""
        Try

            ds_attachment = New DataTable
            ds_attachment = clsEmployeeSavingsMappingHead.GetDocumentByte(strDocumentCode, strTRCode)
            filename = clsCommon.myCstr(ds_attachment.Rows(0)("FileName"))
            Dim blob As Byte() = ds_attachment.Rows(0)("FileData")
            file_path = "C:\ERPTempFolder"
            Dim dir As DirectoryInfo = New DirectoryInfo(file_path)
            If dir.Exists = False Then
                dir.Create()
            End If
            Dim index As Int16 = filename.LastIndexOf(".")
            file_extn = filename.Substring(index)
            filename = filename.Remove(index)
            filename += (clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MM/yy hh:mm:ss")).ToString()
            filename = filename.Replace(" ", "")
            filename = filename.Replace("/", "_")
            filename = filename.Replace(":", "_")
            Dim fs As FileStream = File.Create(file_path + "\\" + filename + file_extn)
            fs.Write(blob, 0, blob.Length)
            fs.Close()
            System.Diagnostics.Process.Start(file_path + "\\" + filename + file_extn)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, "Error in Downloading Documnet.", Me.Text)
        End Try
    End Sub
    '====================Ticket No : BHA/17/01/19-000782 By Prabhakar============================================================
    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select Code", Me.Text)
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowTransHistoryData(txtCode.Value, "DOCUMENT_CODE", "TSPL_EMPLOYEE_SAVINGS_MAPPING_MASTER", "TSPL_EMPLOYEE_SAVINGS_MAPPING_DETAIL")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    '=========================================================================================
End Class

