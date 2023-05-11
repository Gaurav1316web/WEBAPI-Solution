Imports common
Imports System.Data.SqlClient
Imports common.UserControls

Public Class FrmCustomFieldMaster
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Const colValue As String = "colValue"
    Const colDescription As String = "colDescription"
    Dim isInsideLoadData As Boolean = False
#End Region
    WithEvents Evaluator1 As Evaluator
    Dim TextCol As GridViewTextBoxColumn = Nothing
    Dim ComboCol As GridViewComboBoxColumn = Nothing
    Const colLogicalOperator As String = "colLogicalOperator"
    Const colConditionType As String = "colConditionType"
    Const colConditionValue As String = "colConditionValue"
    Const colSlno As String = "colSlno"
    Private Sub FrmCustomFieldMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        LoadType()
        AddNew()
        Evaluator1 = New Evaluator
        '        AddHandler btnSave.Click, AddressOf test
    End Sub

    Private Sub SetUserMgmtNew()
        '' Anubhooti 31-July-2014 BM00000003131
        'MyBase.SetUserMgmt(clsUserMgtCode.CustomFieldMaster)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If
        btnSave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 03/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnSave.Visible = True Then
            MenuItemImport.Enabled = True
            MenuItemExport.Enabled = True
        Else
            MenuItemImport.Enabled = False
            MenuItemExport.Enabled = False
        End If
        '--------------------------------------------------
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub LoadBlankGridCondition(type As EnumConditionType)
        gv2.Rows.Clear()
        gv2.Columns.Clear()
        If cboType.SelectedValue = EnumCustomFieldType.TextType OrElse cboType.SelectedValue = EnumCustomFieldType.FinderType OrElse cboType.SelectedValue = EnumCustomFieldType.MultilineTextType OrElse cboType.SelectedValue = EnumCustomFieldType.ComboListBoxType OrElse cboType.SelectedValue = EnumCustomFieldType.NumberType OrElse cboType.SelectedValue = EnumCustomFieldType.DateType Then

            TextCol = New GridViewTextBoxColumn()
            TextCol.FormatString = ""
            TextCol.HeaderText = "SL. NO."
            TextCol.Name = colSlno
            TextCol.ReadOnly = True
            TextCol.IsVisible = True
            TextCol.Width = 50
            gv2.MasterTemplate.Columns.Add(TextCol)


            ComboCol = New GridViewComboBoxColumn()
            ComboCol.FormatString = ""
            ComboCol.HeaderText = "Logical" & Environment.NewLine & "Operator"
            ComboCol.Name = colLogicalOperator
            ComboCol.ReadOnly = False
            ComboCol.IsVisible = True
            ComboCol.Width = 100
            ComboCol.DataSource = LoadLogicalOperatorType()
            ComboCol.ValueMember = "Code"
            ComboCol.DisplayMember = "Name"
            gv2.MasterTemplate.Columns.Add(ComboCol)


            ComboCol = New GridViewComboBoxColumn()
            ComboCol.FormatString = ""
            ComboCol.HeaderText = "Conditional Operator"
            ComboCol.Name = colConditionType
            ComboCol.ReadOnly = False
            ComboCol.IsVisible = True
            ComboCol.Width = 180

            ComboCol.DataSource = LoadConditionType()
            ComboCol.ValueMember = "Code"
            ComboCol.DisplayMember = "Name"
            gv2.MasterTemplate.Columns.Add(ComboCol)

            If type = EnumCustomFieldType.TextType OrElse type = EnumCustomFieldType.FinderType OrElse type = EnumCustomFieldType.MultilineTextType OrElse type = EnumCustomFieldType.ComboListBoxType Then
                TextCol = New GridViewTextBoxColumn()
                TextCol.FormatString = ""
                TextCol.HeaderText = "Value"
                TextCol.Name = colConditionValue
                TextCol.ReadOnly = False
                TextCol.IsVisible = True
                TextCol.Width = 300
                gv2.MasterTemplate.Columns.Add(TextCol)
            ElseIf type = EnumCustomFieldType.NumberType Then
                Dim repoDecimal As GridViewDecimalColumn = New GridViewDecimalColumn()
                repoDecimal = New GridViewDecimalColumn()
                repoDecimal.FormatString = ""
                repoDecimal.HeaderText = "Value"
                repoDecimal.Name = colConditionValue
                repoDecimal.Width = 200
                repoDecimal.ReadOnly = False
                repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
                gv2.MasterTemplate.Columns.Add(repoDecimal)
            ElseIf type = EnumCustomFieldType.DateType Then
                Dim repoManDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
                repoManDate.Format = DateTimePickerFormat.Custom
                repoManDate.CustomFormat = "dd-MM-yyyy"
                repoManDate.HeaderText = "Value"
                repoManDate.WrapText = True
                repoManDate.FormatString = "{0:d}"
                repoManDate.Name = colConditionValue
                repoManDate.ReadOnly = False
                repoManDate.Width = 200
                gv2.MasterTemplate.Columns.Add(repoManDate)

            End If
        End If
    End Sub


    Function LoadConditionType() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(Integer))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = Nothing
        If cboType.SelectedValue = EnumCustomFieldType.TextType OrElse cboType.SelectedValue = EnumCustomFieldType.FinderType OrElse cboType.SelectedValue = EnumCustomFieldType.MultilineTextType OrElse cboType.SelectedValue = EnumCustomFieldType.ComboListBoxType Then

            dr = dt.NewRow()
            dr("Code") = EnumConditionType.Contains
            dr("Name") = "Contains"
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Code") = EnumConditionType.DoesNotContains
            dr("Name") = "Does Not Contains"
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Code") = EnumConditionType.StartsWith
            dr("Name") = "Starts With"
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Code") = EnumConditionType.DoesNotStartsWith
            dr("Name") = "Does Not Starts With"
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Code") = EnumConditionType.EndsWith
            dr("Name") = "Ends With"
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Code") = EnumConditionType.DoesNotEndsWith
            dr("Name") = "Does Not Ends With"
            dt.Rows.Add(dr)
        ElseIf cboType.SelectedValue = EnumCustomFieldType.NumberType OrElse cboType.SelectedValue = EnumCustomFieldType.DateType Then
            'dr = dt.NewRow()
            'dr("Code") = EnumConditionType.Between
            'dr("Name") = "Between"
            'dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Code") = EnumConditionType.GreaterThan
            dr("Name") = "Greater Than"
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Code") = EnumConditionType.GreaterThanOrEquals
            dr("Name") = "Greater Than Or Equals To"
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Code") = EnumConditionType.LessThan
            dr("Name") = "Less Than"
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Code") = EnumConditionType.LessThanOrEquals
            dr("Name") = "Less Than Or Equals To"
            dt.Rows.Add(dr)
        End If

        dr = dt.NewRow()
        dr("Code") = EnumConditionType.EqualsTo
        dr("Name") = "Equals To"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = EnumConditionType.DoesNotEqualsTo
        dr("Name") = "Does Not Equals To"
        dt.Rows.Add(dr)





        Return dt

    End Function

    Function LoadLogicalOperatorType() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))


        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "AND"
        dr("Name") = "AND"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "OR"
        dr("Name") = "OR"
        dt.Rows.Add(dr)



        Return dt

    End Function


    Sub LoadType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(Integer))
        dt.Columns.Add("Name", GetType(String))


        Dim dr As DataRow = dt.NewRow()
        dr("Code") = EnumCustomFieldType.TextType
        dr("Name") = "Text"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = EnumCustomFieldType.FinderType
        dr("Name") = "Finder"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = EnumCustomFieldType.MultilineTextType
        dr("Name") = "Multiline Text"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = EnumCustomFieldType.ComboListBoxType
        dr("Name") = "ComboList Box"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = EnumCustomFieldType.NumberType
        dr("Name") = "Number"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = EnumCustomFieldType.DateType
        dr("Name") = "Date"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = EnumCustomFieldType.CheckType
        dr("Name") = "CheckBox"
        dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = EnumCustomFieldType.PictureType
        'dr("Name") = "Picture"
        'dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = EnumCustomFieldType.Buttons
        dr("Name") = "Buttons"
        dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = EnumCustomFieldType.RadioButtonType
        'dr("Name") = "Radio Buttons"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = EnumCustomFieldType.GridType
        'dr("Name") = "Grid"
        'dt.Rows.Add(dr)


        cboType.DataSource = dt
        cboType.ValueMember = "Code"
        cboType.DisplayMember = "Name"
    End Sub



    Sub AddNew()
        BlankAllControls()
        LoadBlankGrid()
        gv1.Rows.AddNew()
        isNewEntry = True
        btnSave.Text = "Save"
        RadPageView1.Enabled = False
        chkManualList.Checked = False
        chkFromTable.Checked = False
        fndReferenceTable.Enabled = False
        fndFieldName.Enabled = False
        LoadBlankGridValueList()
        chkIsUnique.Checked = False
    End Sub

    Sub LoadBlankGridValueList()
        gv3.Rows.Clear()
        gv3.Columns.Clear()
        If chkValidate.Checked Then
            TextCol = New GridViewTextBoxColumn()
            TextCol.FormatString = ""
            TextCol.HeaderText = "SL. NO."
            TextCol.Name = colSlno
            TextCol.ReadOnly = True
            TextCol.IsVisible = True
            TextCol.Width = 50
            gv3.MasterTemplate.Columns.Add(TextCol)
            If cboType.SelectedValue = EnumCustomFieldType.TextType Then
                Dim repoVendroItemNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
                repoVendroItemNo = New GridViewTextBoxColumn()
                repoVendroItemNo.FormatString = ""
                repoVendroItemNo.HeaderText = "Value"
                repoVendroItemNo.Name = colValue
                repoVendroItemNo.Width = 200
                gv3.MasterTemplate.Columns.Add(repoVendroItemNo)
            ElseIf cboType.SelectedValue = EnumCustomFieldType.NumberType Then
                Dim repoDecimal As GridViewDecimalColumn = New GridViewDecimalColumn()
                repoDecimal = New GridViewDecimalColumn()
                repoDecimal.FormatString = ""
                repoDecimal.HeaderText = "Value"
                repoDecimal.Name = colValue
                repoDecimal.Width = 200
                repoDecimal.ReadOnly = False
                repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
                gv3.MasterTemplate.Columns.Add(repoDecimal)
            ElseIf cboType.SelectedValue = EnumCustomFieldType.DateType Then
                Dim repoManDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
                repoManDate.Format = DateTimePickerFormat.Custom
                repoManDate.CustomFormat = "dd-MM-yyyy"
                repoManDate.HeaderText = "Value"
                repoManDate.WrapText = True
                repoManDate.FormatString = "{0:d}"
                repoManDate.Name = colValue
                repoManDate.ReadOnly = False
                repoManDate.Width = 200
                gv3.MasterTemplate.Columns.Add(repoManDate)
            ElseIf cboType.SelectedValue = EnumCustomFieldType.CheckType Then

            End If

            Dim repoDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoDesc = New GridViewTextBoxColumn()
            repoDesc.FormatString = ""
            repoDesc.HeaderText = "Description"
            repoDesc.Name = colDescription
            repoDesc.Width = 500
            gv3.MasterTemplate.Columns.Add(repoDesc)
        End If
        gv3.AllowDeleteRow = True
        gv3.AllowAddNewRow = False
        gv3.ShowGroupPanel = False
        gv3.AllowColumnReorder = False
        gv3.AllowRowReorder = False
        gv3.EnableSorting = False
        gv3.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv3.MasterTemplate.ShowRowHeaderColumn = False
        gv3.Rows.AddNew()
    End Sub
    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        If chkValidate.Checked Then
            If cboType.SelectedValue = EnumCustomFieldType.TextType Then
                Dim repoVendroItemNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
                repoVendroItemNo = New GridViewTextBoxColumn()
                repoVendroItemNo.FormatString = ""
                repoVendroItemNo.HeaderText = "Value"
                repoVendroItemNo.Name = colValue
                repoVendroItemNo.Width = 200
                gv1.MasterTemplate.Columns.Add(repoVendroItemNo)
            ElseIf cboType.SelectedValue = EnumCustomFieldType.NumberType Then
                Dim repoDecimal As GridViewDecimalColumn = New GridViewDecimalColumn()
                repoDecimal = New GridViewDecimalColumn()
                repoDecimal.FormatString = ""
                repoDecimal.HeaderText = "Value"
                repoDecimal.Name = colValue
                repoDecimal.Width = 200
                repoDecimal.ReadOnly = False
                repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
                gv1.MasterTemplate.Columns.Add(repoDecimal)
            ElseIf cboType.SelectedValue = EnumCustomFieldType.DateType Then
                Dim repoManDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
                repoManDate.Format = DateTimePickerFormat.Custom
                repoManDate.CustomFormat = "dd-MM-yyyy"
                repoManDate.HeaderText = "Value"
                repoManDate.WrapText = True
                repoManDate.FormatString = "{0:d}"
                repoManDate.Name = colValue
                repoManDate.ReadOnly = False
                repoManDate.Width = 200
                gv1.MasterTemplate.Columns.Add(repoManDate)
            ElseIf cboType.SelectedValue = EnumCustomFieldType.CheckType Then

            End If

            Dim repoDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoDesc = New GridViewTextBoxColumn()
            repoDesc.FormatString = ""
            repoDesc.HeaderText = "Description"
            repoDesc.Name = colDescription
            repoDesc.Width = 500
            gv1.MasterTemplate.Columns.Add(repoDesc)
        End If
        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
    End Sub

    Sub BlankAllControls()
        txtCode.Value = ""
        txtName.Text = ""
        chkValidate.Checked = False
        cboType.SelectedIndex = 0
        txtMaxLength.Text = ""
        txtFieldName.Text = ""
        isMandatory.Checked = False
        fndReferenceTable.Value = ""
        fndFieldName.Value = ""
        chkIsUnique.Checked = False
    End Sub

    Private Sub chkValidate_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkValidate.ToggleStateChanged
        'LoadBlankGrid()
        'gv1.Rows.AddNew()
        RadPageView1.Enabled = chkValidate.Checked
        If (Not RadPageView1.Enabled) OrElse clsCommon.myLen(txtFieldName.Text) <= 0 OrElse clsCommon.myLen(txtName.Text) <= 0 Then
            txtMaxLength.Text = ""
            isMandatory.Checked = False
            fndReferenceTable.Value = ""
            fndFieldName.Value = ""
            LoadBlankGridCondition(cboType.SelectedValue)
        End If
    End Sub
    Sub test()
        clsCommon.MyMessageBoxShow("Hi")
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Sub SaveData()
        Try
            
            If (AllowToSave()) Then
                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.CustomFieldMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
                Dim obj As New clsCustomFieldHead()
                obj.Code = txtCode.Value
                obj.Name = txtName.Text
                obj.FieldName = txtFieldName.Text
                obj.Type = clsCommon.myCdbl(cboType.SelectedValue)
                obj.Is_Validate = chkValidate.Checked
                If chkValidate.Checked Then
                    obj.MaxLength = clsCommon.myCdbl(txtMaxLength.Text)
                    obj.Is_Mandatory = IIf(isMandatory.Checked, 1, 0)
                    obj.IsUnique = IIf(chkIsUnique.Checked, 1, 0)
                    obj.ArrCondition = New List(Of clsCustomFieldConditions)
                    For Each grow As GridViewRowInfo In gv2.Rows
                        Dim objTr As New clsCustomFieldConditions()
                        objTr.SNo = clsCommon.myCstr(grow.Cells(colSlno).Value)
                        objTr.LogicalOperator = clsCommon.myCstr(grow.Cells(colLogicalOperator).Value)
                        'Dim comboBoxColumn As GridViewComboBoxColumn = TryCast(Me.gv2.Columns(colConditionType), GridViewComboBoxColumn)
                        'objTr.ConditionalOperator = clsCommon.myCdbl(DirectCast(comboBoxColumn.GetLookupValue(grow.Cells(colConditionType).Value), String))
                        objTr.ConditionalOperator = grow.Cells(colConditionType).Value
                        objTr.ConditionValue = clsCommon.myCstr(grow.Cells(colConditionValue).Value)
                        If clsCommon.myLen(objTr.LogicalOperator) Then
                            obj.ArrCondition.Add(objTr)
                        End If
                    Next

                    If chkFromTable.Checked Then
                        obj.IsSourceFromValueList = 0
                        obj.IsSourceFromTable = 1
                        obj.ReferenceTableName = clsCommon.myCstr(fndReferenceTable.Value)
                        obj.ReferenceFieldName = clsCommon.myCstr(fndFieldName.Value)
                    ElseIf chkManualList.Checked Then
                        obj.IsSourceFromValueList = 1
                        obj.IsSourceFromTable = 0
                        obj.ReferenceTableName = ""
                        obj.ReferenceFieldName = ""
                        obj.Arr = New List(Of clsCustomFieldDetail)
                        For Each grow As GridViewRowInfo In gv3.Rows
                            If clsCommon.myLen(grow.Cells(colSlno).Value) > 0 Then
                                Dim objTr As New clsCustomFieldDetail()
                                objTr.SNo = clsCommon.myCstr(grow.Cells(colSlno).Value)
                                objTr.Value = clsCommon.myCstr(grow.Cells(colValue).Value)
                                objTr.Description = clsCommon.myCstr(grow.Cells(colDescription).Value)
                                If clsCommon.myLen(objTr.Value) Then
                                    obj.Arr.Add(objTr)
                                End If
                            End If
                        Next
                    Else
                        obj.ReferenceTableName = ""
                        obj.Arr = Nothing
                        obj.ReferenceFieldName = ""
                    End If
                End If
                If (obj.SaveData(obj, isNewEntry)) Then
                    clsCommon.MyMessageBoxShow("Data saved successfully", Me.Text)
                    LoadData(obj.Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub LoadData(ByVal strDocumentNo As String, ByVal navType As common.NavigatorType)
        Try
            isInsideLoadData = True
            BlankAllControls()
            LoadBlankGrid()

            LoadBlankGridValueList()
            Dim obj As New clsCustomFieldHead()
            obj = clsCustomFieldHead.GetData(strDocumentNo, navType, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
                isNewEntry = False
                txtCode.Value = obj.Code
                txtName.Text = obj.Name
                txtFieldName.Text = obj.FieldName
                cboType.SelectedValue = obj.Type
                chkValidate.Checked = obj.Is_Validate
                txtMaxLength.Text = obj.MaxLength
                isMandatory.Checked = IIf(obj.Is_Mandatory = 1, True, False)
                chkIsUnique.Checked = IIf(obj.IsUnique = 1, True, False)
                LoadBlankGridCondition(cboType.SelectedValue)
                chkFromTable.Checked = IIf(obj.IsSourceFromTable = 1, True, False)
                chkManualList.Checked = IIf(obj.IsSourceFromValueList = 1, True, False)
                fndReferenceTable.Value = clsCommon.myCstr(obj.ReferenceTableName)
                fndFieldName.Value = clsCommon.myCstr(obj.ReferenceFieldName)
                If obj.ArrCondition IsNot Nothing AndAlso obj.ArrCondition.Count > 0 Then
                    For Each objTr As clsCustomFieldConditions In obj.ArrCondition
                        gv2.Rows.AddNew()
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colSlno).Value = objTr.SNo
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colLogicalOperator).Value = objTr.LogicalOperator
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colConditionType).Value = objTr.ConditionalOperator
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colConditionValue).Value = objTr.ConditionValue

                    Next
                End If
                LoadBlankGridValueList()
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsCustomFieldDetail In obj.Arr
                        gv3.Rows(gv3.Rows.Count - 1).Cells(colSlno).Value = objTr.SNo
                        gv3.Rows(gv3.Rows.Count - 1).Cells(colValue).Value = objTr.Value
                        gv3.Rows(gv3.Rows.Count - 1).Cells(colDescription).Value = objTr.Description
                        gv3.Rows.AddNew()
                    Next
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    Function checkDuplicateRows(gv As MyRadGridView) As Boolean
        Try
            For x As Integer = 0 To gv.Rows.Count - 1
                For i As Integer = x + 1 To gv.Rows.Count - 1
                    For j As Integer = 1 To gv.Columns.Count - 1
                        If clsCommon.CompairString(gv.Rows(x).Cells(j).Value, gv.Rows(i).Cells(j).Value) = CompairStringResult.Equal Then
                            Throw New Exception("Duplicate Rows Found at Rows Number " & (x + 1) & "  and " & (i + 1) & "  For Column " & gv.Columns(j).HeaderText)
                        End If
                    Next
                Next
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function


    Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(txtName.Text) <= 0 Then
                txtName.Focus()
                Throw New Exception("Please Enter Caption Text of the field")
            End If

            If clsCommon.myLen(txtFieldName.Text) <= 0 Then
                txtFieldName.Focus()
                Throw New Exception("Please Enter Identifiable Name For the field")
            End If


            If txtFieldName.Text.Contains(" ") Then
                Throw New Exception("Field Name must not have any blank space")
            End If

            If txtFieldName.Text.Contains(".") OrElse txtFieldName.Text.Contains(",") OrElse txtFieldName.Text.Contains(":") OrElse txtFieldName.Text.Contains(";") OrElse txtFieldName.Text.Contains("'") OrElse txtFieldName.Text.Contains("""") OrElse txtFieldName.Text.Contains("]") OrElse txtFieldName.Text.Contains("[") OrElse txtFieldName.Text.Contains("]") OrElse txtFieldName.Text.Contains("{") OrElse txtFieldName.Text.Contains("}") OrElse txtFieldName.Text.Contains("(") OrElse txtFieldName.Text.Contains(")") OrElse txtFieldName.Text.Contains("-") OrElse txtFieldName.Text.Contains("+") OrElse txtFieldName.Text.Contains("=") OrElse txtFieldName.Text.Contains(">") OrElse txtFieldName.Text.Contains("<") OrElse txtFieldName.Text.Contains("/") OrElse txtFieldName.Text.Contains("\") OrElse txtFieldName.Text.Contains("|") OrElse txtFieldName.Text.Contains("?") OrElse txtFieldName.Text.Contains("*") OrElse txtFieldName.Text.Contains("&") OrElse txtFieldName.Text.Contains("^") OrElse txtFieldName.Text.Contains("%") OrElse txtFieldName.Text.Contains("$") OrElse txtFieldName.Text.Contains("#") OrElse txtFieldName.Text.Contains("@") OrElse txtFieldName.Text.Contains("!") OrElse txtFieldName.Text.Contains("~") OrElse txtFieldName.Text.Contains("`") Then
                Throw New Exception("Field Name must not have following Symbols , . : ; ' "" ) ( { } [ ] | \ / ? > < * & ^ % $ # @ ! ~ ` = + -")
            End If

            Dim cnt As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from tspl_custom_field_head where code<>'" & txtCode.Value & "' and FieldName='" & txtFieldName.Text & "'"))
            If cnt > 0 Then
                Throw New Exception(" Field Name :" & txtFieldName.Text & "  Found Duplicate [Also been specified for other custom field]")
            End If
            If chkValidate.Checked Then
                If clsCommon.myLen(txtMaxLength.Text) <= 0 AndAlso isMandatory.Checked = False AndAlso gv2.Rows.Count <= 0 Then
                    If chkFromTable.Checked Then

                    ElseIf chkManualList.Checked Then

                    Else
                        Throw New Exception(" You Must Specify any validation rule, Either length, mandatory, Compare Conditions or value List if validate check is ON")
                    End If
                End If
            End If


            If chkFromTable.Checked Then
                If clsCommon.myLen(fndReferenceTable.Value) <= 0 Then
                    Throw New Exception("You must select a Reference Table Name")
                ElseIf clsCommon.myLen(fndFieldName.Value) <= 0 Then
                    Throw New Exception(" You must Select Field Name of Reference Table")
                End If
            End If

            If chkManualList.Checked Then
                If gv3.Rows.Count <= 0 Then
                    Throw New Exception(" You must specify atleast one value for manual value List")
                ElseIf clsCommon.myLen(gv3.Rows(0).Cells(colValue).Value) <= 0 OrElse clsCommon.myLen(gv3.Rows(0).Cells(colDescription).Value) <= 0 Then
                    Throw New Exception(" Please Specify Manual Value List at Row No 1")
                Else
                    For i As Integer = 0 To gv3.Rows.Count - 1
                        If clsCommon.myLen(gv3.Rows(i).Cells(colValue).Value) <= 0 AndAlso clsCommon.myLen(gv3.Rows(i).Cells(colDescription).Value) > 0 Then
                            Throw New Exception(" Please Specify Value in  Manual Value List at Row No " & (i + 1))
                        ElseIf clsCommon.myLen(gv3.Rows(i).Cells(colValue).Value) > 0 AndAlso clsCommon.myLen(gv3.Rows(i).Cells(colDescription).Value) <= 0 Then
                            Throw New Exception(" Please SpecifyDescription in Manual Value List at Row No " & (i + 1))
                        End If
                    Next
                End If
            End If

            If gv2 IsNot Nothing AndAlso gv2.Rows.Count > 0 Then
                For i As Integer = 0 To gv2.Rows.Count - 1
                    If clsCommon.myLen(gv2.Rows(i).Cells(colLogicalOperator).Value) > 0 AndAlso clsCommon.myLen(gv2.Rows(i).Cells(colConditionType).Value) > 0 AndAlso clsCommon.myLen(gv2.Rows(i).Cells(colConditionValue).Value) <= 0 Then
                        Throw New Exception("Please Fill Condition Value at Row No. " & (i + 1))
                    End If
                    If clsCommon.myLen(gv2.Rows(i).Cells(colLogicalOperator).Value) > 0 AndAlso clsCommon.myLen(gv2.Rows(i).Cells(colConditionType).Value) <= 0 AndAlso clsCommon.myLen(gv2.Rows(i).Cells(colConditionValue).Value) > 0 Then
                        Throw New Exception("Please Fill Conditional Operator at Row No. " & (i + 1))
                    End If

                    If clsCommon.myLen(gv2.Rows(i).Cells(colLogicalOperator).Value) <= 0 AndAlso clsCommon.myLen(gv2.Rows(i).Cells(colConditionType).Value) > 0 AndAlso clsCommon.myLen(gv2.Rows(i).Cells(colConditionValue).Value) > 0 Then
                        Throw New Exception("Please Fill Logical Operator at Row No. " & (i + 1))
                    End If

                Next
            End If

            checkDuplicateRows(gv2)
            checkDuplicateRows(gv3)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
        Return True
    End Function

    Private Sub FrmCustomFieldMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        End If
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Dim qry As String = "select code,Name from TSPL_CUSTOM_FIELD_HEAD"
        LoadData(clsCommon.ShowSelectForm("custFieMF", qry, "Code", "", txtCode.Value, "Code", isButtonClicked), NavigatorType.Current)
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub

    Sub CloseForm()
        Me.Close()
    End Sub

    Private Sub cboType_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboType.SelectedValueChanged
        Try
            '    chkValidate.Checked = False
            If cboType.SelectedValue = EnumCustomFieldType.Buttons Then
                chkValidate.Enabled = False
            Else
                chkValidate.Enabled = True
                LoadBlankGrid()
                gv1.Rows.AddNew()
                'If cboType.SelectedValue = EnumCustomFieldType.TextType OrElse cboType.SelectedValue = EnumCustomFieldType.FinderType OrElse cboType.SelectedValue = EnumCustomFieldType.MultilineTextType OrElse cboType.SelectedValue = EnumCustomFieldType.ComboListBoxType OrElse cboType.SelectedValue = EnumCustomFieldType.NumberType OrElse cboType.SelectedValue = EnumCustomFieldType.DateType Then
                LoadBlankGridCondition(cboType.SelectedValue)
            End If
            
            'End If

        Catch ex As Exception

        End Try
    End Sub



    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        If Not isInsideLoadData Then
            If gv1.RowCount > 0 Then
                Dim intCurrRow As Integer = gv1.CurrentRow.Index
                If intCurrRow = gv1.Rows.Count - 1 Then
                    gv1.Rows.AddNew()
                    gv1.CurrentRow = gv1.Rows(intCurrRow)
                End If
            End If
        End If
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    Private Sub MenuItemImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemImport.Click

        clsCommon.MyMessageBoxShow("Under Development")
        'Dim gv As New RadGridView()
        'Me.Controls.Add(gv)
        'Dim currentdate As Date = Date.Today
        'If transportSql.importExcel(gv, "Code", "Description", "Type", "Validate", "Value_1", "Description_1", "Value_2", "Description_2", "Value_3", "Description_3", "Value_4", "Description_4", "Value_5", "Description_5", "Value_6", "Description_6", "Value_7", "Description_7", "Value_8", "Description_8", "Value_9", "Description_9", "Value_10", "Description_10", "Value_11", "Description_11", "Value_12", "Description_12", "Value_13", "Description_13", "Value_14", "Description_14", "Value_15", "Description_15", "Value_16", "Description_16", "Value_17", "Description_17", "Value_18", "Description_18", "Value_19", "Description_19", "Value_20", "Description_20") Then
        '    Try
        '        clsCommon.ProgressBarShow()
        '        Dim obj As New clsCustomFieldHead()

        '        Dim strCode As String = ""
        '        Dim strName As String = ""
        '        Dim strDes As String = ""
        '        Dim strValidate As String = ""

        '        Dim Counter As Integer = 4

        '        For Each grow As GridViewRowInfo In gv.Rows
        '            obj = New clsCustomFieldHead()

        '            strCode = clsCommon.myCstr(grow.Cells(0).Value)
        '            If strCode.Length > 30 Then
        '                Throw New Exception("Code can not be blank or incorrect in row No " + clsCommon.myCstr(grow.Index) + ".")
        '            End If
        '            obj.Code = strCode

        '            strName = clsCommon.myCstr(grow.Cells(1).Value)
        '            If strName.Length > 50 Or (String.IsNullOrEmpty(strName)) Then
        '                Throw New Exception("Description can not be blank or incorrect in row No " + clsCommon.myCstr(grow.Index) + ".")
        '            End If
        '            obj.Name = strName

        '            strDes = clsCommon.myCstr(grow.Cells(2).Value)
        '            If strDes.Length > 10 Or (String.IsNullOrEmpty(strName)) Then
        '                Throw New Exception("Type can not be blank or incorrect in row No " + clsCommon.myCstr(grow.Index) + ".")
        '            End If
        '            If clsCommon.CompairString(strDes, "Text") = CompairStringResult.Equal Then
        '                obj.Type = EnumCustomFieldType.TextType
        '            ElseIf clsCommon.CompairString(strDes, "Number") = CompairStringResult.Equal Then
        '                obj.Type = EnumCustomFieldType.NumberType
        '            ElseIf clsCommon.CompairString(strDes, "Date") = CompairStringResult.Equal Then
        '                obj.Type = EnumCustomFieldType.DateType
        '            ElseIf clsCommon.CompairString(strDes, "Check Box") = CompairStringResult.Equal Then
        '                obj.Type = EnumCustomFieldType.CheckType
        '            Else
        '                Throw New Exception("Type is incorrect in row No " + clsCommon.myCstr(grow.Index) + ".")
        '            End If

        '            strValidate = clsCommon.myCstr(grow.Cells(3).Value).ToUpper()
        '            If clsCommon.CompairString(strValidate, "YES") = CompairStringResult.Equal Then
        '                obj.Is_Validate = True
        '            ElseIf clsCommon.CompairString(strValidate, "NO") = CompairStringResult.Equal Then
        '                obj.Is_Validate = False
        '            Else
        '                Throw New Exception("Validate is incorrect in row No " + clsCommon.myCstr(grow.Index) + ".")
        '            End If

        '            Counter = 4
        '            If obj.Is_Validate Then
        '                obj.Arr = New List(Of clsCustomFieldDetail)
        '                For ii As Integer = 1 To 20
        '                    Dim objTr As New clsCustomFieldDetail()
        '                    If clsCommon.myLen(grow.Cells(Counter).Value) > 0 Then
        '                        objTr.Value = clsCommon.myCstr(grow.Cells(Counter).Value)
        '                        objTr.Description = clsCommon.myCstr(grow.Cells(Counter + 1).Value)
        '                        obj.Arr.Add(objTr)
        '                        Counter = Counter + 2
        '                    End If
        '                Next
        '            End If

        '            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_CUSTOM_FIELD_HEAD where Code='" + obj.Code + "'")) > 0 Then
        '                obj.SaveData(obj, False)
        '            Else
        '                obj.SaveData(obj, True)
        '            End If
        '        Next
        '        clsCommon.ProgressBarHide()
        '        common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
        '    Catch ex As Exception
        '        clsCommon.ProgressBarHide()
        '        myMessages.myExceptions(ex)
        '    End Try

        'End If
        'Me.Controls.Remove(gv)
    End Sub

    Private Sub MenuItemExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemExport.Click
        'Dim str As String
        'str = "select Code, Name as Description, ( case when Type = 0 then 'Text' when Type = 1 then 'Number' when Type = 2 then 'Date' when Type = 3 then 'Check Box' else '' end) as 'Type', ( case when Is_Validate = 1 then 'YES' ELSE 'NO' end) as 'Validate' "

        'For ii As Integer = 1 To 20
        '    str += ", ( select Value from TSPL_CUSTOM_FIELD_DETAIL where TSPL_CUSTOM_FIELD_DETAIL.Custom_Field_Code =TSPL_CUSTOM_FIELD_HEAD.Code and SNo='" + ii.ToString() + "') as Value_" + ii.ToString() + ","
        '    str += " ( select Description from TSPL_CUSTOM_FIELD_DETAIL where TSPL_CUSTOM_FIELD_DETAIL.Custom_Field_Code =TSPL_CUSTOM_FIELD_HEAD.Code and SNo='" + ii.ToString() + "')  as Description_" + ii.ToString() + " "
        'Next


        'str += " from TSPL_CUSTOM_FIELD_HEAD "
        'transportSql.ExporttoExcel(str, Me)


        clsCommon.MyMessageBoxShow("Under Development")
    End Sub

    Private Sub MenuItemClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemClose.Click
        Me.Close()
    End Sub


    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCustomFieldHead.DeleteData(txtCode.Value, trans) Then
                trans.Commit()
                clsCommon.MyMessageBoxShow("Data deleted successfully.")
                AddNew()
            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndReferenceTable__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndReferenceTable._MYValidating
        Dim qry As String = "SELECT  UPPER (TABLE_NAME ) as TABLE_NAME FROM INFORMATION_SCHEMA.TABLES  "
        fndReferenceTable.Value = clsCommon.ShowSelectForm("TableList", qry, "TABLE_NAME", "TABLE_TYPE='BASE TABLE'", fndReferenceTable.Value, "", isButtonClicked)
    End Sub

    Private Sub fndFieldName__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndFieldName._MYValidating
        If clsCommon.myLen(fndReferenceTable.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please Select Table First")
            Exit Sub
        End If
        Dim qry As String = "select  upper(sys.columns.Name) as FieldName from sys.columns inner join sys.tables on sys.tables.object_id=sys.columns.object_id   "
        fndFieldName.Value = clsCommon.ShowSelectForm("FiledList", qry, "FieldName", "sys.tables.name='" & fndReferenceTable.Value & "'", fndFieldName.Value, "", isButtonClicked)
    End Sub

    Sub ApplyRowNumbering()
        isInsideLoadData = True
        If gv2.Rows.Count > 0 Then
            For i As Integer = 0 To gv2.Rows.Count - 1
                gv2.Rows(i).Cells(colSlno).Value = (i + 1)
            Next
        End If
        isInsideLoadData = False
    End Sub

    Sub ApplyRowNumberingGv3()
        isInsideLoadData = True
        If gv3.Rows.Count > 0 Then
            For i As Integer = 0 To gv3.Rows.Count - 1
                gv3.Rows(i).Cells(colSlno).Value = (i + 1)
            Next
        End If
        isInsideLoadData = False
    End Sub

    Private Sub gv3_UserAddedRow(sender As Object, e As GridViewRowEventArgs) Handles gv3.UserAddedRow
        ApplyRowNumberingGv3()
    End Sub

    Private Sub gv3_UserDeletedRow(sender As Object, e As GridViewRowEventArgs) Handles gv3.UserDeletedRow
        ApplyRowNumberingGv3()
    End Sub

    Private Sub gv3_UserDeletingRow(sender As Object, e As GridViewRowCancelEventArgs) Handles gv3.UserDeletingRow
        If clsCommon.MyMessageBoxShow("Sure to Delete Row ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = vbNo Then
            e.Cancel = True
        End If
    End Sub



    Private Sub gv2_UserAddedRow(sender As Object, e As GridViewRowEventArgs) Handles gv2.UserAddedRow
        ApplyRowNumbering()
    End Sub

    Private Sub gv2_UserDeletedRow(sender As Object, e As GridViewRowEventArgs) Handles gv2.UserDeletedRow
        ApplyRowNumbering()
    End Sub

    Private Sub gv2_UserDeletingRow(sender As Object, e As GridViewRowCancelEventArgs) Handles gv2.UserDeletingRow
        If clsCommon.MyMessageBoxShow("Sure to Delete Row ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = vbNo Then
            e.Cancel = True
        End If
    End Sub

    Private Sub chkManualList_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkManualList.ToggleStateChanged
        If chkManualList.Checked Then
            chkFromTable.Checked = False
            gv3.Enabled = True
            LoadBlankGridValueList()
            gv3.Rows.AddNew()
        Else
            gv3.Enabled = False
            gv3.Rows.Clear()
        End If
    End Sub

    Private Sub chkFromTable_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkFromTable.ToggleStateChanged
        If chkFromTable.Checked Then
            chkManualList.Checked = False
            fndReferenceTable.Enabled = True
            fndFieldName.Enabled = True
            fndFieldName.Value = ""
            fndReferenceTable.Value = ""
        Else
            fndReferenceTable.Enabled = False
            fndFieldName.Enabled = False
            fndFieldName.Value = ""
            fndReferenceTable.Value = ""
        End If
    End Sub

    Private Sub gv3_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv3.CellValueChanged
        If Not isInsideLoadData Then
            If gv3.RowCount > 0 Then
                Dim intCurrRow As Integer = gv3.CurrentRow.Index
                If intCurrRow = gv3.Rows.Count - 1 Then
                    gv3.Rows.AddNew()
                    ApplyRowNumberingGv3()
                    gv3.CurrentRow = gv3.Rows(intCurrRow)
                End If
            End If
        End If
    End Sub
    Dim isControlFound As Boolean = False
    Dim rValue As Control = Nothing
    Function getControl(ctrlParent As Control, findControl As String) As Control
        Dim ctrl As Control
        If rValue Is Nothing Then
            For Each ctrl In ctrlParent.Controls
                If clsCommon.CompairString(ctrl.Name, findControl) = CompairStringResult.Equal Then
                    rValue = ctrl
                    isControlFound = True
                    Return rValue
                    Exit Function
                End If
                If ctrl.HasChildren AndAlso (Not isControlFound) Then
                    getControl(ctrl, findControl)
                End If
            Next
        End If
        Return rValue
    End Function
    Private Sub Evaluator1_GetVariable(ByVal name As String, ByRef value As Object) Handles Evaluator1.GetVariable
        'Select Case name
        '    Case "anumber"
        '        value = 5.0
        '    Case "adate"
        '        value = #1/1/2005#
        '    Case "theForm"
        '        value = Me
        'End Select

        Try
            Dim sValue() As String = name.Split(".")

            isControlFound = False
            rValue = Nothing
            Dim ctrl As Control = getControl(Me, sValue(0))
            'Dim pInfo As System.Reflection.PropertyInfo = ctrl.[GetType]().GetProperty(sValue(1))
            'If pInfo IsNot Nothing Then
            '    Dim tc As System.ComponentModel.TypeConverter = System.ComponentModel.TypeDescriptor.GetConverter(pInfo.PropertyType)
            '    'If tc.CanConvertFrom(Type.[GetType](propertyValue.[GetType]().ToString())) Then
            '    '    'valToSet = tc.ConvertFromString(propertyValue)
            '    pInfo.SetValue(ctrl, "Hello", Nothing)
            '    '    pinfo.getvalue(
            '    'End If

            'End If
            value = clsCommon.myCdbl(ctrl.Text)
            'Dim prnt As Control = Me
            'While ctrl Is Nothing
            '    ctrl = prnt.Controls(sValue(0))
            '    If ctrl Is Nothing Then
            '        For i As Integer = 0 To prnt.Controls.Count - 1
            '            Dim ctr As Control = prnt.Controls(i)
            '            If ctr.HasChildren Then

            '            End If
            '        Next
            '    End If
            'End While


            'If sValue.Length = 3 Then
            '    Dim str As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select value from TSPL_CUSTOM_FIELD_VALUES where program_code='" & sValue(0) & "' and Transaction_Code='" & sValue(1) & "' and Custom_Field_Code='" & sValue(2) & "' "))
            '    Dim type As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Type  from TSPL_CUSTOM_FIELD_HEAD where code='" & sValue(2) & "'"))
            '    If type = EnumCustomFieldType.NumberType Then
            '        value = clsCommon.myCdbl(str)
            '    Else
            '        value = str
            '    End If


            'Else
            '    Throw New Exception("Parameter Missing , ProgramCode.DocumentCode.FieldCode")
            'End If
        Catch ex As Exception
            value = "Error: " & ex.Message
        End Try
    End Sub

    Private Sub txtFieldName_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtFieldName.Validating
        'Try
        '    Dim res As String = Evaluator1.Eval(txtFieldName.Text).ToString
        '    'txtFieldName.Text = res
        '    clsCommon.MyMessageBoxShow(res)
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(" Error :" & ex.Message)
        'End Try
    End Sub
End Class
