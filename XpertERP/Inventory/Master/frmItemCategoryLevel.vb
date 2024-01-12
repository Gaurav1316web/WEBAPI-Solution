'--18/11/2013--form Add By- Panch Raj ---------
'--------------02/june/2014-------Modified by Meenesh against ticket no.BM00000002745---------------------
'---------------------------------------BM00000003305--------------use this form for Vendor/Customer/Location-------------
Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class frmItemCategoryLevel
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim userCode, companyCode, FormType As String
#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
    'Const colLineNo As String = "LineNo"
    Const colCode As String = "Code"
    Const colDescription As String = "Description"
    Const colBinNo As String = "colBinNo"
    Dim Code As GridViewTextBoxColumn
    Dim Description As GridViewTextBoxColumn
    Dim BinNo As GridViewTextBoxColumn
    Dim headimportfirst As Boolean
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()

#End Region

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Save()
    End Sub
    Public Sub New(ByVal user As String, ByVal company As String, ByVal Type As String)
        InitializeComponent()
        userCode = user
        companyCode = company
        FormType = Type
    End Sub
    Public Sub Save()

        If AllowToSave() Then
            Dim obj As New clsItemCategoryLevel()
            obj.ITEM_CATEGORY_CODE = txtCode.Value
            obj.DESCRIPTION = Me.txtDescription.Text
            obj.CATEGORY_LEVEL = txtCategoryLevel.Text
            obj.formtype = FormType
            obj.Master_Packing = IIf(chkMasterPack.Checked = True, 1, 0)
            obj.Bin_Mapping = IIf(chkBinMapping.Checked = True, 1, 0)
            obj.ObjList = New List(Of clsItemCategoryLevelDetails)
            For Each grow As GridViewRowInfo In gvCategoryValues.Rows
                If clsCommon.myLen(grow.Cells(0).Value) <= 0 Then
                    Continue For
                End If
                Dim objTr As New clsItemCategoryLevelDetails()
                objTr.ITEM_CATEGORY_CODE = clsCommon.myCstr(Me.txtCode.Value)
                objTr.CODE = clsCommon.myCstr(grow.Cells(colCode).Value)
                objTr.DESCRIPTION = clsCommon.myCstr(grow.Cells(colDescription).Value)
                If chkBinMapping.Checked Then
                    objTr.BinNo = clsCommon.myCstr(grow.Cells(colBinNo).Value)
                Else
                    objTr.BinNo = ""
                End If

                objTr.formtype_detail = FormType
                obj.ObjList.Add(objTr)
            Next
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            If (obj.SaveData(obj, isNewEntry, trans)) Then
                trans.Commit()
                If clsCommon.CompairString(btnsave.Text, "Save") = CompairStringResult.Equal Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                Else
                    common.clsCommon.MyMessageBoxShow(Me, "Data Updated Successfully", Me.Text)
                End If
                LoadData(obj.ITEM_CATEGORY_CODE, NavigatorType.Current)
            Else
                trans.Rollback()
                'common.clsCommon.MyMessageBoxShow(" '" + obj.strMsg.ToString() + "'")
                common.clsCommon.MyMessageBoxShow("Cannot Change/Delete Exist Value. This is associated with other record...! ", Me.Text)
            End If

        End If
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        txtCode.MyReadOnly = True
        btnsave.Enabled = True
        btndelete.Enabled = True
        Dim obj As New clsItemCategoryLevel()
        obj = clsItemCategoryLevel.GetData(strCode, NavTyep, FormType)
        isNewEntry = False
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.ITEM_CATEGORY_CODE) > 0) Then
            funReset()
            isNewEntry = False
            btnsave.Text = "Update"
            btndelete.Enabled = True
            txtCode.Value = obj.ITEM_CATEGORY_CODE
            txtDescription.Text = obj.DESCRIPTION
            txtCategoryLevel.Text = obj.CATEGORY_LEVEL
            txtCode.MyReadOnly = True

            chkMasterPack.Checked = clsCommon.myCBool(IIf(obj.Master_Packing = 1, True, False))

            Dim ii As Int16 = 0
            If obj.ObjList IsNot Nothing AndAlso obj.ObjList.Count > 0 Then
                LoadGridColumns()
                For Each objTr As clsItemCategoryLevelDetails In obj.ObjList
                    gvCategoryValues.Rows.AddNew()
                    ii = ii + 1
                    gvCategoryValues.Rows(gvCategoryValues.Rows.Count - 1).Cells(colCode).Value = objTr.CODE
                    gvCategoryValues.Rows(gvCategoryValues.Rows.Count - 1).Cells(colDescription).Value = objTr.DESCRIPTION
                    Dim ShowBinMapping As Boolean = False
                    ShowBinMapping = clsCommon.myCBool(IIf(clsFixedParameter.GetData(clsFixedParameterType.ShowBinMapping, clsFixedParameterCode.ShowBinMapping, Nothing) = "1", True, False))
                    If ShowBinMapping = True Then
                        gvCategoryValues.Rows(gvCategoryValues.Rows.Count - 1).Cells(colBinNo).Value = objTr.BinNo
                    End If

                Next
            End If
            chkBinMapping.Checked = clsCommon.myCBool(IIf(obj.Bin_Mapping = 1, True, False))
        End If
        CheckMasterPack()
        ChecKBinMapping()
    End Sub

    Function AllowToSave() As Boolean
        If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) Then
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Fill Category Code", Me.Text)
                txtCode.Focus()
                txtCode.Select()
                Errorcontrol.SetError(txtCode, "Fill Category Code")
                Return False
            Else
                Errorcontrol.ResetError(txtCode)
            End If
        End If
        If clsCommon.myLen(txtDescription.Text) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Fill Description", Me.Text)
            txtDescription.Focus()
            txtDescription.Select()
            Errorcontrol.SetError(txtDescription, "Fill Description")
            Return False
        Else
            Errorcontrol.ResetError(txtDescription)
        End If

        If clsCommon.myLen(txtCategoryLevel.Text) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Fill Category Level", Me.Text)
            txtCategoryLevel.Focus()
            txtCategoryLevel.Select()
            Errorcontrol.SetError(txtCategoryLevel, "Fill Category Level")
            Return False
        Else
            Errorcontrol.ResetError(txtCategoryLevel)
        End If

        If CheckExistingLevel() = False Then
            clsCommon.MyMessageBoxShow("Category Level " & Me.txtCategoryLevel.Value & " already in use !")
            txtCategoryLevel.Focus()
            txtCategoryLevel.Select()
            Errorcontrol.SetError(txtCategoryLevel, "Category Level " & Me.txtCategoryLevel.Value & " already in use !")
            Return False
        Else
            Errorcontrol.ResetError(txtCategoryLevel)
        End If

        For Each grow As GridViewRowInfo In gvCategoryValues.Rows
            If clsCommon.myLen(gvCategoryValues.Rows(0).Cells(0).Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Fill atleast one row", Me.Text)
                gvCategoryValues.Focus()
                gvCategoryValues.Select()
                Return False
            End If
            If clsCommon.myLen(grow.Cells(0).Value) <= 0 Then
                Continue For
            End If
            Dim strq As String

            If isNewEntry = True Then
                strq = "select code FROM TSPL_ITEM_CATEGORY_LEVEL_VALUES where code='" & clsCommon.myCstr(grow.Cells(0).Value) & "' and form_type='" + FormType + "'"
            Else
                strq = "select code FROM TSPL_ITEM_CATEGORY_LEVEL_VALUES where code='" & clsCommon.myCstr(grow.Cells(0).Value) & "' AND ITEM_CATEGORY_CODE!='" & clsCommon.myCstr(Me.txtCode.Value) & "' and form_type='" + FormType + "'"
            End If

            Dim dt As DataTable
            dt = clsDBFuncationality.GetDataTable(strq)
            If dt.Rows.Count > 0 Then
                clsCommon.MyMessageBoxShow("Category Level Value " & clsCommon.myCstr(grow.Cells(0).Value) & " already exists !")
                gvCategoryValues.Focus()
                gvCategoryValues.Select()
                Return False
            End If
            If clsCommon.CompairString(txtCode.Value, grow.Cells(colCode).Value) = CompairStringResult.Equal Then
                clsCommon.MyMessageBoxShow("Item Category Code is not equal to " & grow.Cells(colCode).Value & "", Me.Text)
                Return False
            End If
        Next

        Return True
    End Function

    Function CheckExistingLevel()
        Dim strq As String
        If isNewEntry = True Then
            strq = "select category_level from TSPL_ITEM_CATEGORY_LEVEL where category_level=" & Me.txtCategoryLevel.Value & " and form_type='" + FormType + "'"
        Else
            strq = "select category_level from TSPL_ITEM_CATEGORY_LEVEL where category_level=" & Me.txtCategoryLevel.Value & " and item_category_code !='" & clsCommon.myCstr(Me.txtCode.Value) & "' and form_type='" + FormType + "'"
        End If

        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(strq)
        If dt.Rows.Count > 0 Then
            Return False
        End If
        Return True
    End Function
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "You Cannot Delete Record", Me.Text)
            Exit Sub
        End If
        'Dim discCode As String
        'discCode = clsDBFuncationality.getSingleValue("select Discount_Code  from TSPL_SHIPMENT_DETAILS  where Discount_Code ='" & txtCode.Value & "'")
        'If clsCommon.myLen(discCode) > 0 Then
        '    common.clsCommon.MyMessageBoxShow("This record can't be deleted.It is used in another process")
        '    Exit Sub
        'End If

        funDelete()
    End Sub

    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsItemCategoryLevel.DeleteData(txtCode.Value, FormType)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub txtCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub frmItemCategoryLevel_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadGridColumns()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ' ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")
        funReset()

        lblItemCategoryCode.Text = "Item Category Code"
        chkMasterPack.Visible = True
        If clsCommon.CompairString(FormType, "VENDOR") = CompairStringResult.Equal Then
            lblItemCategoryCode.Text = "Vendor Category Code"
            chkMasterPack.Visible = False
        ElseIf clsCommon.CompairString(FormType, "CUSTOMER") = CompairStringResult.Equal Then
            lblItemCategoryCode.Text = "Customer Category Code"
            chkMasterPack.Visible = False
        ElseIf clsCommon.CompairString(FormType, "LOCATION") = CompairStringResult.Equal Then
            lblItemCategoryCode.Text = "Location Category Code"
            chkMasterPack.Visible = False
        End If
    End Sub

    Private Sub SetUserMgmtNew()
        If clsCommon.CompairString(FormType, "ITEM") = CompairStringResult.Equal Then
            ''MyBase.SetUserMgmt(clsUserMgtCode.frmItemCategoryLevel)
        ElseIf clsCommon.CompairString(FormType, "VENDOR") = CompairStringResult.Equal Then
            ''MyBase.SetUserMgmt(clsUserMgtCode.frmVendorCategoryLevel)
        ElseIf clsCommon.CompairString(FormType, "CUSTOMER") = CompairStringResult.Equal Then
            ''MyBase.SetUserMgmt(clsUserMgtCode.frmCustomerCategoryLevel)
        ElseIf clsCommon.CompairString(FormType, "LOCATION") = CompairStringResult.Equal Then
            ''MyBase.SetUserMgmt(clsUserMgtCode.frmLocationCategoryLevel)
        End If

        If Not (MyBase.isReadFlag) Then
            '--------richa Ticket no. BM00000003121 15/07/2014 
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 02/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnsave.Visible = True Then
            RadMenuItem2.Enabled = True
            RadMenuItem3.Enabled = True
        Else
            RadMenuItem2.Enabled = False
            RadMenuItem3.Enabled = False
        End If
        '--------------------------------------------------
        'btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        funReset()
    End Sub

    Sub funReset()
        isNewEntry = True
        chkBinMapping.Visible = False
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        txtDescription.Text = ""
        txtCategoryLevel.Text = ""
        Me.gvCategoryValues.Rows.Clear()
        Me.gvCategoryValues.Rows.AddNew()
        chkMasterPack.Checked = False

        Dim ShowBinMapping As Boolean = False
        ShowBinMapping = clsCommon.myCBool(IIf(clsFixedParameter.GetData(clsFixedParameterType.ShowBinMapping, clsFixedParameterCode.ShowBinMapping, Nothing) = "1", True, False))
        If ShowBinMapping = True Then
            chkBinMapping.Visible = True
            chkBinMapping.Checked = False
            ChecKBinMapping()
        End If

        CheckMasterPack()

        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = False
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        funClose()

    End Sub

    Sub funClose()
        Me.Close()
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Dim str As String = "select count(*) from TSPL_ITEM_CATEGORY_LEVEL where item_category_code ='" + txtCode.Value + "' and isnull(form_type,'ITEM')='" + FormType + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtCode.MyReadOnly = True
        End If

        If txtCode.MyReadOnly OrElse isButtonClicked Then
            ' Dim qry As String = " select item_category_code as Code,  description from TSPL_ITEM_CATEGORY_LEVEL "
            'txtCode.Value = clsCommon.ShowSelectForm("TSPL_ITEM_CATEGORY_LEVEL", qry, "Code", "", txtCode.Value, "item_category_code", isButtonClicked)
            txtCode.Value = clsItemCategoryLevel.getFinder(" isnull(form_type,'ITEM')='" + FormType + "'", txtCode.Value, isButtonClicked, FormType)
            '---------------Monika 14/04/2014-------when no datafound then send error of object ref.-------------
            If clsCommon.myLen(txtCode.Value) > 0 Then
                txtCode.Text = clsItemCategoryLevel.GetData(txtCode.Value, Nothing, FormType).ITEM_CATEGORY_CODE
            End If
            '--------------------------------------------------------------
            If txtCode.Value <> "" Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                funReset()
            End If
        End If
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub frmItemCategoryLevel_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Sub LoadGridColumns()
        gvCategoryValues.DataSource = Nothing
        gvCategoryValues.Rows.Clear()
        gvCategoryValues.Columns.Clear()

        gvCategoryValues.ReadOnly = False


        Code = New GridViewTextBoxColumn()
        Code.FormatString = ""
        Code.HeaderText = "Code"
        Code.Name = colCode
        Code.Width = 150
        Code.ReadOnly = False
        Code.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvCategoryValues.Columns.Add(Code)

        Description = New GridViewTextBoxColumn()
        Description.FormatString = ""
        Description.HeaderText = "Description"
        Description.Name = colDescription
        Description.Width = 200
        Description.ReadOnly = False
        Description.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvCategoryValues.Columns.Add(Description)

        BinNo = New GridViewTextBoxColumn()
        BinNo.FormatString = ""
        BinNo.HeaderText = "BinNo"
        BinNo.Name = colBinNo
        BinNo.Width = 200
        BinNo.ReadOnly = False
        BinNo.VisibleInColumnChooser = False
        BinNo.IsVisible = False
        BinNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvCategoryValues.Columns.Add(BinNo)

        
    End Sub

    
    

    'Function CheckForExisingRecord() As Boolean
    '    If clsCommon.myLen(txtPayPeriodCode.Value) > 0 And clsCommon.myLen(txtEmpCode.Value) > 0 Then
    '        Dim str As String = "select LVALLOTMENT_CODE from TSPL_LEAVE_ALLOTMENT where PAY_PERIOD_CODE ='" + txtPayPeriodCode.Value + "' and EMP_CODE ='" + txtEmpCode.Value + "' and LVALLOTMENT_CODE <>'" + txtCode.Value + "' "
    '        Dim Code As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(str))
    '        If clsCommon.myLen(Code) > 0 Then
    '            Dim strmessage As String = "Record Exists for Selected Employee for Selected Pay Period on Allotment Code " + Code + ", New Record can not be genrated on same. Do you want to open Previous Saved ? "
    '            If clsCommon.MyMessageBoxShow(strmessage, Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
    '                LoadData(Code, NavigatorType.Current)
    '            End If
    '            Return False
    '        End If
    '    End If
    '    Return True
    'End Function

    Private Sub gvCategoryValues_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvCategoryValues.CurrentColumnChanged
        If gvCategoryValues.RowCount > 0 Then
            Dim intCurrRow As Integer = gvCategoryValues.CurrentRow.Index
            'gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gvCategoryValues.Rows.Count - 1 Then
                If (clsCommon.myLen(txtCode.Value) > 0) Then
                    gvCategoryValues.Rows.AddNew()
                    'gvCategoryValues.Rows(gvCategoryValues.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem
                    gvCategoryValues.CurrentRow = gvCategoryValues.Rows(intCurrRow)
                End If
            End If
        End If
    End Sub

    Private Sub btnexportItemHead_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexportItemHead.Click
        Dim qry As String = "select ITEM_CATEGORY_CODE as [Code],DESCRIPTION as [Description] ,CATEGORY_LEVEL as [Category Level],Form_Type,Bin_Mapping as [Bin Mapping]  from TSPL_ITEM_CATEGORY_LEVEL "
        ListImpExpColumnsMandatory = New List(Of String)({"Code", "Description", "Category Level"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"Code"})
        transportSql.ExporttoExcel(qry, " and isnull(form_type,'ITEM')='" + FormType + "'", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID + "ItemHead")
    End Sub

    Private Sub BtnExportItemDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExportItemDetails.Click
        Dim qry As String = "select TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE as [Item Category Code],TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE as [Code],TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as [Description],TSPL_ITEM_CATEGORY_LEVEL_VALUES.Form_Type,TSPL_ITEM_CATEGORY_LEVEL_VALUES.Bin_No as [Bin No] from TSPL_ITEM_CATEGORY_LEVEL_VALUES left outer join TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL .ITEM_CATEGORY_CODE =TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE and TSPL_ITEM_CATEGORY_LEVEL_VALUES.form_type=TSPL_ITEM_CATEGORY_LEVEL.form_type "
        ListImpExpColumnsMandatory = New List(Of String)({"Item Category Code", "Description", "Code"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"Item Category Code"})
        transportSql.ExporttoExcel(qry, " and isnull(TSPL_ITEM_CATEGORY_LEVEL_VALUES.form_type,'ITEM')='" + FormType + "'", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID + "ItemDetails")
    End Sub

    Private Sub BtnImpItemHead_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnImpItemHead.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Code", "Description", "Category Level", "Form_Type", "Bin Mapping") Then
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try
                Dim Code As String = ""
                Dim Description As String = ""
                Dim Category_Level As Integer = "0"
                Dim Bin_Mapping As Integer = "0"
                clsCommon.ProgressBarShow()
                Dim obj As New clsItemCategoryLevel()


                Dim counter As Integer = 0
                Dim count As Integer = 0
                Dim checkcode As String = ""
                Dim codevalue As String = ""
                Dim qry As String = ""
                For Each grow As GridViewRowInfo In gv.Rows
                    counter += 1
                    Code = clsCommon.myCstr(grow.Cells("Code").Value)
                    If Code IsNot Nothing Then
                        qry = "Select ITEM_CATEGORY_CODE from TSPL_ITEM_CATEGORY_LEVEL where ITEM_CATEGORY_CODE ='" + Code + "' and isnull(form_type,'ITEM')='" + FormType + "'"
                        checkcode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                        'If clsCommon.myLen(checkcode) <= 0 Then
                        '    Throw New Exception("Item Category  Code Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",First Enter the Code Correctly")
                        'End If


                        If clsCommon.myLen(Code) > 20 Then
                            Throw New Exception("Length Of Code Should Not Exceed 20 Characters,Check It At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    Else
                        clsCommon.ProgressBarHide()
                        Me.Controls.Remove(gv)
                        Exit Sub
                    End If

                    Description = clsCommon.myCstr(grow.Cells("Description").Value)

                    If clsCommon.myLen(Description) <= 0 Then
                        Throw New Exception("Description Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",First Enter the Description  ")
                    End If


                    If clsCommon.myLen(Description) > 100 Then
                        Throw New Exception("Length Of Description Should Not Exceed 100 Characters,Check It At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    Category_Level = clsCommon.myCdbl(grow.Cells("Category Level").Value)
                    If (CInt(Category_Level) <= 0) Then
                        Throw New Exception("Fill Category Level At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    count = (clsDBFuncationality.getSingleValue(" select Count(*)  from TSPL_ITEM_CATEGORY_LEVEL  where ITEM_CATEGORY_CODE<>'" + Code + "' and Bin_Mapping =1 and isnull(form_type,'ITEM')='" + FormType + "'", trans))
                    If count > 0 AndAlso clsCommon.myCdbl(grow.Cells("Bin Mapping").Value) > 1 Then
                        Throw New Exception(" Bin Mapping Already Mapped with another category,Check It At Line No. " + clsCommon.myCstr(counter) + "")
                    Else
                        Bin_Mapping = clsCommon.myCdbl(grow.Cells("Bin Mapping").Value)
                    End If

                    If (CInt(Category_Level) <= 0) Then
                        Throw New Exception("Fill Category Level At Line No. " + clsCommon.myCstr(counter) + "")
                    End If


                    count = (clsDBFuncationality.getSingleValue("select  Count(*) from TSPL_ITEM_CATEGORY_LEVEL where CATEGORY_LEVEL ='" + clsCommon.myCstr(Category_Level) + "' and isnull(form_type,'ITEM')='" + FormType + "'", trans))
                    If count > 0 Then
                        Dim newcount As Integer

                        newcount = (clsDBFuncationality.getSingleValue("select  Count(*) from TSPL_ITEM_CATEGORY_LEVEL where CATEGORY_LEVEL ='" + clsCommon.myCstr(Category_Level) + "'and Item_category_code ='" + clsCommon.myCstr(Code) + "' and isnull(form_type,'ITEM')='" + FormType + "'", trans))
                        If newcount > 0 Then
                        Else

                            Throw New Exception(" Category Level Should Not be Same as previous Level value,Check It At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    End If

                    Dim exitqry As String = "select count(*) from TSPL_ITEM_CATEGORY_LEVEL where TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE='" + clsCommon.myCstr(Code) + "'and TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION ='" + Description + "' and isnull(form_type,'ITEM')='" + FormType + "'"
                    Dim check As Integer = clsCommon.myCstr(clsDBFuncationality.getSingleValue(exitqry, trans))


                    Dim isSaved As Boolean = True
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "ITEM_CATEGORY_CODE", Code)
                    clsCommon.AddColumnsForChange(coll, "DESCRIPTION", Description)
                    clsCommon.AddColumnsForChange(coll, "CATEGORY_LEVEL", Category_Level)
                    clsCommon.AddColumnsForChange(coll, "Bin_Mapping", Bin_Mapping)
                    clsCommon.AddColumnsForChange(coll, "form_type", FormType)
                    clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)

                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", (clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy")))

                    If (check) > 0 Then
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_CATEGORY_LEVEL", OMInsertOrUpdate.Update, "ITEM_CATEGORY_CODE='" + Code + "' and isnull(form_type,'ITEM')='" + FormType + "'", trans)

                    Else
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Created_Date", (clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy")))
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_CATEGORY_LEVEL", OMInsertOrUpdate.Insert, "", trans)
                    End If

                Next

                clsCommon.ProgressBarHide()
                trans.Commit()
                headimportfirst = True
                clsCommon.MyMessageBoxShow(Me, "Data Transfer Successfully", Me.Text)

            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

 
    Private Sub BtnImportItemDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnImportItemDetails.Click
        Dim gv As New RadGridView()
        Dim trans As SqlTransaction = Nothing
        Me.Controls.Add(gv)
        Try
            If headimportfirst = True Then
                If transportSql.importExcel(gv, "Item Category Code", "Code", "Description", "Form_Type", "Bin No") Then
                    trans = clsDBFuncationality.GetTransactin()

                    Dim Item_Category_Code As String = ""
                    Dim Description As String = ""
                    Dim Code As String = ""
                    Dim BinNo As String = ""
                    clsCommon.ProgressBarShow()
                    Dim counter As Integer = 0
                    Dim checkcode As String = ""
                    Dim codevalue As String = ""
                    Dim qry As String = ""
                    For Each grow As GridViewRowInfo In gv.Rows
                        counter += 1
                        Item_Category_Code = clsCommon.myCstr(grow.Cells("Item Category Code").Value)

                        qry = "select TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE as [Item Category Code],TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE as [Code] "
                        qry += " ,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as [Description] from TSPL_ITEM_CATEGORY_LEVEL_VALUES left outer join TSPL_ITEM_CATEGORY_LEVEL "
                        qry += " on TSPL_ITEM_CATEGORY_LEVEL .ITEM_CATEGORY_CODE =TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE and TSPL_ITEM_CATEGORY_LEVEL_VALUES.form_type=TSPL_ITEM_CATEGORY_LEVEL.form_type"
                        qry += " where TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE ='" + Item_Category_Code + "' and isnull(TSPL_ITEM_CATEGORY_LEVEL_VALUES.form_type,'ITEM')='" + FormType + "'"
                        checkcode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                        If clsCommon.myLen(checkcode) <= 0 Then
                            'Monika
                            'clsCommon.ProgressBarHide()
                            'Me.Controls.Remove(gv)
                            'Exit Sub
                        End If
                        If clsCommon.myLen(Item_Category_Code) > 20 Then
                            Throw New Exception("Length Of Code Should Not Exceed 20 Characters,Check It At Line No. " + clsCommon.myCstr(counter) + "")
                        End If

                        Description = clsCommon.myCstr(grow.Cells("Description").Value)

                        If clsCommon.myLen(Description) <= 0 Then
                            Throw New Exception("" + FormType + " Description  Code and Description Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",First Enter the Code  and Description Correctly ")
                        End If


                        If clsCommon.myLen(Description) > 100 Then
                            Throw New Exception("Length Of Description Should Not Exceed 100 Characters,Check It At Line No. " + clsCommon.myCstr(counter) + "")
                        End If

                        Code = clsCommon.myCstr(grow.Cells("Code").Value)
                        If clsCommon.myLen(Code) <= 0 Then
                            Throw New Exception("Fill Code At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If clsCommon.myLen(Code) >= 60 Then
                            Throw New Exception("Length Of Code Should Not Exceed ,Check It At Line No. " + clsCommon.myCstr(counter) + "")
                        End If


                       
                        BinNo = clsCommon.myCstr(grow.Cells("Bin No").Value)
                        Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Count(*) from TSPL_ITEM_CATEGORY_LEVEL_VALUES left join  TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE =TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE   where TSPL_ITEM_CATEGORY_LEVEL.Item_Category_Code='" + Item_Category_Code + "' and Bin_Mapping =1 and isnull(TSPL_ITEM_CATEGORY_LEVEL_VALUES.form_type,'ITEM')='" + FormType + "' and isnull(TSPL_ITEM_CATEGORY_LEVEL.form_type,'ITEM')='" + FormType + "'", trans))
                        If count <= 0 AndAlso clsCommon.myLen(BinNo) > 0 Then
                            BinNo = ""
                        Else
                            If clsCommon.myLen(BinNo) >= 50 Then
                                Throw New Exception("Length Of Bin No Should Not Exceed ,Check It At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        End If

                        Dim exitquery As String = "select count(*) from TSPL_ITEM_CATEGORY_LEVEL_VALUES "
                        exitquery += " where TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE ='" + Item_Category_Code + "' and Code='" + Code + "' and Description='" + Description + "' and isnull(form_type,'ITEM')='" + FormType + "'"
                        Dim check As Integer = clsCommon.myCstr(clsDBFuncationality.getSingleValue(exitquery, trans))



                        Dim isSaved As Boolean = True
                        Dim coll As New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "ITEM_CATEGORY_CODE", Item_Category_Code)
                        clsCommon.AddColumnsForChange(coll, "CODE", Code)
                        clsCommon.AddColumnsForChange(coll, "DESCRIPTION", Description)
                        clsCommon.AddColumnsForChange(coll, "form_type", FormType)
                        clsCommon.AddColumnsForChange(coll, "Bin_No", BinNo)
                        If check <= 0 Then

                            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_CATEGORY_LEVEL_VALUES", OMInsertOrUpdate.Insert, "", trans)
                        Else
                            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_CATEGORY_LEVEL_VALUES", OMInsertOrUpdate.Update, "ITEM_CATEGORY_CODE='" + Item_Category_Code + "' and isnull(form_type,'ITEM')='" + FormType + "' and CODE='" + Code + "'", trans)
                        End If

                    Next
                    clsCommon.ProgressBarHide()
                    trans.Commit()
                    headimportfirst = False

                    clsCommon.MyMessageBoxShow(Me, "Data Transfer Successfully", Me.Text)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "Kindly First Import Head Section of Item Category Level", Me.Text)
            End If

        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Me.Controls.Remove(gv)
    End Sub

    Private Sub CheckMasterPack()
        Try
            chkMasterPack.Enabled = True

            Dim qry As String = "select count(*) from TSPL_ITEM_CATEGORY_LEVEL where isnull(Master_Packing,0)=1 and ITEM_CATEGORY_CODE<>'" + txtCode.Value + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

            If check > 0 Then
                chkMasterPack.Enabled = False
            Else
                chkMasterPack.Enabled = True
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub ChecKBinMapping()
        Try
            chkBinMapping.Enabled = True

            Dim qry As String = "select count(*) from TSPL_ITEM_CATEGORY_LEVEL where isnull(Bin_Mapping,0)=1 and ITEM_CATEGORY_CODE<>'" + txtCode.Value + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

            If check > 0 Then
                chkBinMapping.Enabled = False
            Else
                chkBinMapping.Enabled = True
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub chkBinMapping_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkBinMapping.ToggleStateChanged
        Try

            BinNo.VisibleInColumnChooser = chkBinMapping.Checked
            BinNo.IsVisible = chkBinMapping.Checked
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvCategoryValues_UserDeletingRow(sender As Object, e As GridViewRowCancelEventArgs) Handles gvCategoryValues.UserDeletingRow
        Dim strCode As String = gvCategoryValues.CurrentRow.Cells(colCode).Value
        Dim countRecord As Decimal = clsDBFuncationality.getSingleValue("select Count(*) from TSPL_ITEM_MASTER_CATEGORY where Item_Category_Code = '" + txtCode.Value + "' and Item_Cagetory_Values = '" + clsCommon.myCstr(gvCategoryValues.CurrentRow.Cells(colCode).Value) + "'")
        If countRecord > 0 Then
            e.Cancel = True
            clsCommon.MyMessageBoxShow("Category Code : '" + strCode + "'  Used As a Reference . You Can not Delete/Update", Me.Text)
        Else
            e.Cancel = False
        End If
    End Sub

End Class
