Imports common
Imports System.Data.SqlClient
Imports common.UserControls


Public Class frmCustomFieldEditor
    Inherits FrmMainTranScreen

    Public formId As String = String.Empty
    Public isNewEntry As Boolean = False
    Public CustomFieldCodeToLoad As String = String.Empty
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Const colValue As String = "colValue"
    Const colDescription As String = "colDescription"
    Dim isInsideLoadData As Boolean = False
    Dim TextCol As GridViewTextBoxColumn = Nothing
    Dim ComboCol As GridViewComboBoxColumn = Nothing
    Const colLogicalOperator As String = "colLogicalOperator"
    Const colConditionType As String = "colConditionType"
    Const colConditionValue As String = "colConditionValue"
    Const colSlno As String = "colSlno"
    Dim cboType As common.Controls.MyComboBox = Nothing
    Dim chkValidate As common.Controls.MyCheckBox = Nothing

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


    Sub SaveData()
        Try
            Dim obj As New clsCustomFieldMapping
            obj.Program_Code = formId
            obj.Custom_Field_Code = fndCustomFieldName.Value
            obj.Is_Mandatory = isMandatory.Checked
            obj.Default_Value = ""  '' Have To Set By Puting a Textbox of Default Value
            obj.SNo = 1
            obj.Is_For_Detail_Level = chkIsForDetailLevel.Checked '' Have to Add it Also
            obj.Is_For_Print = chkIsForPrint.Checked '' Have to be added
            obj.Is_CalCulated_Column = IIf(chkCalculatedField.Checked, 1, 0)
            If obj.Is_CalCulated_Column = 1 Then
                obj.CalculationExpression = txtExpression.Text
            Else
                obj.CalculationExpression = ""
            End If
            obj.MaxLength = clsCommon.myCdbl(txtMaxLength.Text)
            obj.ReferenceTableName = clsCommon.myCstr(fndReferenceTable.Value)
            obj.ReferenceFieldName = clsCommon.myCstr(fndFieldName.Value)
            obj.MethodCode = clsCommon.myCstr(fndMethodCode.Value)
            Dim Arg As String = String.Empty
            If gv4 IsNot Nothing AndAlso gv4.Rows.Count > 0 Then
                For i As Integer = 0 To gv4.Rows.Count - 1
                    Arg = Arg & IIf(clsCommon.myLen(gv4.Rows(i).Cells(colDescription).Value) <= 0, "#", gv4.Rows(i).Cells(colDescription).Value)
                    If i <> gv4.Rows.Count - 1 Then
                        Arg = Arg & ","
                    End If
                Next
            End If
            obj.MethodArg = Arg
            obj.IsSourceFromTable = IIf(chkFromTable.Checked, 1, 0)
            obj.IsSourceFromValueList = IIf(chkManualList.Checked, 1, 0)
            obj.IsUnique = IIf(chkIsUnique.Checked, 1, 0)


            If gv2 IsNot Nothing AndAlso gv2.Rows.Count > 0 Then
                obj.arrConditions = New List(Of clsCustomFieldMappingConditions)
                Dim objConditions As clsCustomFieldMappingConditions = Nothing
                For i As Integer = 0 To gv2.Rows.Count - 1
                    If clsCommon.myLen(gv2.Rows(i).Cells(colLogicalOperator).Value) > 0 Then
                        objConditions = New clsCustomFieldMappingConditions
                        objConditions.Program_Code = obj.Program_Code
                        objConditions.Custom_Field_Code = obj.Custom_Field_Code
                        objConditions.SNo = (i + 1)
                        objConditions.LogicalOperator = gv2.Rows(i).Cells(colLogicalOperator).Value
                        objConditions.ConditionalOperator = gv2.Rows(i).Cells(colConditionType).Value
                        objConditions.ConditionValue = gv2.Rows(i).Cells(colConditionValue).Value
                        obj.arrConditions.Add(objConditions)
                    End If
                Next
            End If

            If gv3 IsNot Nothing AndAlso gv3.Rows.Count > 0 AndAlso obj.IsSourceFromValueList = 1 Then
                obj.arrValueList = New List(Of clsCustomFieldMappingValueList)
                Dim objValueList As clsCustomFieldMappingValueList = Nothing
                For i As Integer = 0 To gv3.Rows.Count - 1
                    If clsCommon.myLen(gv3.Rows(i).Cells(colValue).Value) > 0 Then
                        objValueList = New clsCustomFieldMappingValueList
                        objValueList.Program_Code = obj.Program_Code
                        objValueList.Custom_Field_Code = obj.Custom_Field_Code
                        objValueList.SNo = (i + 1)
                        objValueList.Value = gv2.Rows(i).Cells(colValue).Value
                        objValueList.Description = gv2.Rows(i).Cells(colDescription).Value
                        obj.arrValueList.Add(objValueList)
                    End If
                Next
            End If
            If clsCustomFieldMapping.SaveData(obj) Then
                clsCommon.MyMessageBoxShow(Me, "Saved Successfully", Me.Text)
                LoadData(obj.Custom_Field_Code, obj.Program_Code)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub


    Sub DeleteData()
        Dim trans As SqlTransaction = Nothing
        Try
            Dim qry As String = "select count(*) from TSPL_CUSTOM_FIELD_VALUES where Custom_Field_Code='" & fndCustomFieldName.Value & "' and Program_Code ='" & formId & "'"
            Dim cnt As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If cnt <= 0 Then
                If clsCommon.MyMessageBoxShow(Me, "Sure to Delete ?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                    trans = clsDBFuncationality.GetTransactin()
                    If clsCustomFieldMapping.DeleteData(fndCustomFieldName.Value, formId, trans) Then
                        clsCommon.MyMessageBoxShow(Me, "Record Deleted Successfully.", Me.Text)
                        trans.Commit()
                    End If
                End If
            Else
                Throw New Exception("Can not delete this field, As it is being used at " & cnt & " Record(s)")
            End If
        Catch ex As Exception
            Try
                trans.Rollback()
            Catch ex1 As Exception
            End Try
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub LoadCustomField(isButtonClicked As Boolean)
        Try
            Dim qry As String = "select code,Name from TSPL_CUSTOM_FIELD_HEAD"
            Dim prevValue As String = fndCustomFieldName.Value
            fndCustomFieldName.Value = clsCommon.ShowSelectForm("custFieldName", qry, "Code", "", fndCustomFieldName.Value, "Code", isButtonClicked)
            If clsCommon.myLen(fndCustomFieldName.Value) <= 0 Then
                fndCustomFieldName.Value = prevValue
            End If
            If clsCommon.myLen(fndCustomFieldName.Value) <= 0 Then
                Reset()
            Else
                Dim cnt As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from  TSPL_CUSTOM_FIELD_MAPPING where custom_field_code='" & fndCustomFieldName.Value & "' and program_code='" & formId & "' "))
                If cnt > 0 Then
                    LoadData(fndCustomFieldName.Value, formId)
                Else
                    Dim Fieldtype As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select type from TSPL_CUSTOM_FIELD_HEAD where code='" & fndCustomFieldName.Value & "'"))
                    cboType.SelectedValue = Fieldtype
                    Dim Is_Validated As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select is_Validate from TSPL_CUSTOM_FIELD_HEAD where code='" & fndCustomFieldName.Value & "'"))
                    chkValidate.Checked = IIf(Is_Validated = 1, True, False)
                    LoadSelectedFieldData(fndCustomFieldName.Value)
                End If

            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Sub LoadSelectedFieldData(FieldCode As String)
        Try
            isInsideLoadData = True
            If chkCalculatedField.Checked Then
                Reset()
            ElseIf chkManualEntryField.Checked Then
                Reset()
                Dim obj As New clsCustomFieldHead()
                obj = clsCustomFieldHead.GetData(FieldCode, NavigatorType.Current, Nothing)
                If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
                    isNewEntry = True
                    fndCustomFieldName.Value = obj.Code
                    lblFieldDescription.Text = obj.Name
                    lblFieldName.Text = obj.FieldName
                    cboType.SelectedValue = obj.Type
                    chkValidate.Checked = obj.Is_Validate
                    txtMaxLength.Text = obj.MaxLength
                    isMandatory.Checked = IIf(obj.Is_Mandatory = 1, True, False)
                    chkIsUnique.Checked = IIf(obj.IsUnique = 1, True, False)
                    txtFieldHeight.Text = ""
                    LoadBlankGridCondition(cboType.SelectedValue)
                    chkFromTable.Checked = IIf(obj.IsSourceFromTable = 1, True, False)
                    chkManualList.Checked = IIf(obj.IsSourceFromValueList = 1, True, False)
                    fndReferenceTable.Value = clsCommon.myCstr(obj.ReferenceTableName)
                    fndFieldName.Value = clsCommon.myCstr(obj.ReferenceFieldName)
                    fndMethodCode.Value = ""
                    lblMethodDesc.Text = ""
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
            End If


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
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
        If clsCommon.MyMessageBoxShow(Me, "Sure to Delete Row ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = vbNo Then
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
        If clsCommon.MyMessageBoxShow(Me, "Sure to Delete Row ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = vbNo Then
            e.Cancel = True
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
    Sub LoadBlankGv4(arg As String)
        gv4.Rows.Clear()
        gv4.Columns.Clear()

        If clsCommon.myLen(arg) > 0 Then
            TextCol = New GridViewTextBoxColumn()
            TextCol.FormatString = ""
            TextCol.HeaderText = "SL. NO."
            TextCol.Name = colSlno
            TextCol.ReadOnly = True
            TextCol.IsVisible = True
            TextCol.Width = 50
            gv4.MasterTemplate.Columns.Add(TextCol)

            Dim repoVendroItemNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoVendroItemNo = New GridViewTextBoxColumn()
            repoVendroItemNo.FormatString = ""
            repoVendroItemNo.HeaderText = "Argument Name"
            repoVendroItemNo.Name = colValue
            repoVendroItemNo.Width = 200
            gv4.MasterTemplate.Columns.Add(repoVendroItemNo)

            Dim repoDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoDesc = New GridViewTextBoxColumn()
            repoDesc.FormatString = ""
            repoDesc.HeaderText = "Argument Value"
            repoDesc.Name = colDescription
            repoDesc.Width = 500
            gv4.MasterTemplate.Columns.Add(repoDesc)

            gv4.AllowDeleteRow = False
            gv4.AllowAddNewRow = False
            gv4.ShowGroupPanel = False
            gv4.AllowColumnReorder = False
            gv4.AllowRowReorder = False
            gv4.EnableSorting = False
            gv4.MasterTemplate.ShowRowHeaderColumn = False
            Dim argArray() As String = arg.Split(",")
            If argArray.Length > 0 Then
                isInsideLoadData = True
                For i As Integer = 0 To argArray.Length - 1
                    gv4.Rows.Add((i + 1), argArray(i), "")
                Next
                isInsideLoadData = False
            End If
        End If
    End Sub
    Sub LoadData(customFieldCode As String, ProgId As String)
        Try
            Dim obj As clsCustomFieldMapping = clsCustomFieldMapping.GetData(customFieldCode, ProgId, Nothing)

            If obj IsNot Nothing Then
                isNewEntry = False
                fndCustomFieldName.Value = obj.Custom_Field_Code
                lblFieldDescription.Text = obj.Custom_Field_Name
                lblFieldName.Text = obj.Custom_Field_Field_Name
                cboType.SelectedValue = obj.Type
                chkValidate.Checked = obj.Is_Validate
                chkIsForDetailLevel.Checked = obj.Is_For_Detail_Level
                chkIsForPrint.Checked = obj.Is_For_Print
                txtMaxLength.Text = obj.MaxLength
                isMandatory.Checked = IIf(obj.Is_Mandatory = 1, True, False)
                chkIsUnique.Checked = IIf(obj.IsUnique = 1, True, False)
                txtFieldHeight.Text = ""
                LoadBlankGridCondition(cboType.SelectedValue)
                chkFromTable.Checked = IIf(obj.IsSourceFromTable = 1, True, False)
                chkManualList.Checked = IIf(obj.IsSourceFromValueList = 1, True, False)
                fndReferenceTable.Value = clsCommon.myCstr(obj.ReferenceTableName)
                fndFieldName.Value = clsCommon.myCstr(obj.ReferenceFieldName)
                fndMethodCode.Value = clsCommon.myCstr(obj.MethodCode)
                lblMethodDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MethodName from tspl_Standard_Method where MethodCode='" & fndMethodCode.Value & "'"))
                Dim ArgCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MethodArg from tspl_Standard_Method where MethodCode='" & fndMethodCode.Value & "'"))
                Dim arg As String = obj.MethodArg 'clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MethodArg from TSPL_CUSTOM_FIELD_MAPPING where MethodCode='" & fndMethodCode.Value & "'"))
                If clsCommon.myLen(arg) > 0 Then
                    LoadBlankGv4(ArgCode)
                    Dim argArray() As String = arg.Split(",")
                    isInsideLoadData = True
                    For i As Integer = 0 To argArray.Length - 1
                        gv4.Rows(i).Cells(colDescription).Value = IIf(clsCommon.CompairString(argArray(i), "#") = CompairStringResult.Equal, "", argArray(i))
                    Next
                    isInsideLoadData = False
                End If
                If obj.arrConditions IsNot Nothing AndAlso obj.arrConditions.Count > 0 Then
                    For Each objTr As clsCustomFieldMappingConditions In obj.arrConditions
                        gv2.Rows.AddNew()
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colSlno).Value = objTr.SNo
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colLogicalOperator).Value = objTr.LogicalOperator
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colConditionType).Value = objTr.ConditionalOperator
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colConditionValue).Value = objTr.ConditionValue

                    Next
                End If
                LoadBlankGridValueList()
                If obj.arrValueList IsNot Nothing AndAlso obj.arrValueList.Count > 0 Then
                    For Each objTr As clsCustomFieldMappingValueList In obj.arrValueList
                        gv3.Rows(gv3.Rows.Count - 1).Cells(colSlno).Value = objTr.SNo
                        gv3.Rows(gv3.Rows.Count - 1).Cells(colValue).Value = objTr.Value
                        gv3.Rows(gv3.Rows.Count - 1).Cells(colDescription).Value = objTr.Description
                        gv3.Rows.AddNew()
                    Next
                End If
            End If
            If obj.Is_CalCulated_Column = 1 Then
                chkCalculatedField.Checked = True
                txtExpression.Text = obj.CalculationExpression
                RadPageViewPage1.Item.Visibility = ElementVisibility.Visible
                RadPageViewPage2.Item.Visibility = ElementVisibility.Visible
                RadPageViewPage3.Item.Visibility = ElementVisibility.Visible
                RadPageViewPage4.Item.Visibility = ElementVisibility.Collapsed
                chkManualEntryField.Checked = False
                chkCalculatedField.Checked = True
                chkButtons.Checked = False
                RadPageView1.Visible = True
                RadPageView1.SelectedPage = RadPageView1.Pages("RadPageViewPage3")
            ElseIf clsCommon.myLen(fndMethodCode.Value) > 0 Then
                RadPageViewPage1.Item.Visibility = ElementVisibility.Collapsed
                RadPageViewPage2.Item.Visibility = ElementVisibility.Collapsed
                RadPageViewPage3.Item.Visibility = ElementVisibility.Collapsed
                RadPageViewPage4.Item.Visibility = ElementVisibility.Visible

                chkManualEntryField.Checked = False
                chkCalculatedField.Checked = False
                chkButtons.Checked = True
                RadPageView1.Visible = True
                RadPageView1.SelectedPage = RadPageView1.Pages("RadPageViewPage4")
            Else
                RadPageViewPage1.Item.Visibility = ElementVisibility.Visible
                RadPageViewPage2.Item.Visibility = ElementVisibility.Visible
                RadPageViewPage3.Item.Visibility = ElementVisibility.Collapsed
                RadPageViewPage4.Item.Visibility = ElementVisibility.Collapsed
                chkManualEntryField.Checked = True
                chkCalculatedField.Checked = False
                chkButtons.Checked = False
                RadPageView1.Visible = True
                RadPageView1.SelectedPage = RadPageView1.Pages("RadPageViewPage1")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(fndCustomFieldName.Value) <= 0 Then
                Throw New Exception("Please select a custom field")
            End If

            'If chkManualEntryField.Checked = False AndAlso chkCalculatedField.Checked = False Then
            '    Throw New Exception("Please Check Either Manual entry Field or Calculated Field")
            'End If
            checkDuplicateRows(gv2)
            checkDuplicateRows(gv3)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function
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

    Sub Reset()
        Try
            txtExpression.ReadOnly = True
            LoadType()
            chkManualEntryField.Checked = False
            chkCalculatedField.Checked = False
            fndCustomFieldName.Value = ""
            lblFieldName.Text = ""
            lblFieldDescription.Text = ""
            LoadBlankGridCondition(0)
            txtMaxLength.Text = ""
            isMandatory.Checked = False
            chkIsUnique.Checked = False
            txtFieldHeight.Text = ""
            chkFromTable.Checked = False
            chkManualList.Checked = False
            fndReferenceTable.Value = ""
            fndFieldName.Value = ""
            chkValidate.Checked = False
            LoadBlankGridValueList()
            txtExpression.Text = ""
            chkManualEntryField.Checked = True
            chkCalculatedField.Checked = False
            fndMethodCode.Value = ""
            lblMethodDesc.Text = ""
            chkButtons.Checked = False
            LoadBlankGv4("")
            RadPageViewPage3.Item.Visibility = ElementVisibility.Collapsed
            RadPageView1.SelectedPage = RadPageView1.Pages("RadPageViewPage1")
            'RadPageView1.Pages("RadPageViewPage3")
            'RadPageView1.Visible = False
            isNewEntry = True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
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
    Sub LoadType()
        cboType = New common.Controls.MyComboBox
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

        dr = dt.NewRow()
        dr("Code") = EnumCustomFieldType.PictureType
        dr("Name") = "Picture"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = EnumCustomFieldType.Buttons
        dr("Name") = "Buttons"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = EnumCustomFieldType.RadioButtonType
        dr("Name") = "Radio Buttons"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = EnumCustomFieldType.GridType
        dr("Name") = "Grid"
        dt.Rows.Add(dr)


        cboType.DataSource = dt
        cboType.ValueMember = "Code"
        cboType.DisplayMember = "Name"
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Me.Close()
            GC.Collect()
            GC.WaitForPendingFinalizers()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            DeleteData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If AllowToSave() Then
                SaveData()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub frmCustomFieldEditor_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            btnSave.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            btnClose.PerformClick()
        End If
    End Sub

    Private Sub frmCustomFieldEditor_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
            ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
            ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
            chkValidate = New common.Controls.MyCheckBox
            Reset()

            If clsCommon.myLen(CustomFieldCodeToLoad) > 0 AndAlso clsCommon.myLen(formId) > 0 Then
                LoadData(CustomFieldCodeToLoad, formId)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub chkCalculatedField_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkCalculatedField.ToggleStateChanged
        Try
            If chkCalculatedField.Checked Then
                RadPageViewPage1.Item.Visibility = ElementVisibility.Visible
                RadPageViewPage2.Item.Visibility = ElementVisibility.Visible
                RadPageViewPage3.Item.Visibility = ElementVisibility.Visible
                RadPageViewPage4.Item.Visibility = ElementVisibility.Collapsed
                chkManualEntryField.Checked = False
                chkCalculatedField.Checked = True
                chkButtons.Checked = False
                RadPageView1.Visible = True
                RadPageView1.SelectedPage = RadPageView1.Pages("RadPageViewPage3")
            ElseIf chkButtons.Checked Then
                RadPageViewPage1.Item.Visibility = ElementVisibility.Collapsed
                RadPageViewPage2.Item.Visibility = ElementVisibility.Collapsed
                RadPageViewPage3.Item.Visibility = ElementVisibility.Collapsed
                RadPageViewPage4.Item.Visibility = ElementVisibility.Visible
                chkManualEntryField.Checked = False
                chkCalculatedField.Checked = False
                chkButtons.Checked = True
                RadPageView1.Visible = True
                RadPageView1.SelectedPage = RadPageView1.Pages("RadPageViewPage4")
            ElseIf chkManualEntryField.Checked Then
                RadPageViewPage1.Item.Visibility = ElementVisibility.Visible
                RadPageViewPage2.Item.Visibility = ElementVisibility.Visible
                RadPageViewPage3.Item.Visibility = ElementVisibility.Collapsed
                RadPageViewPage4.Item.Visibility = ElementVisibility.Collapsed
                chkManualEntryField.Checked = True
                chkCalculatedField.Checked = False
                chkButtons.Checked = False
                RadPageView1.Visible = True
                RadPageView1.SelectedPage = RadPageView1.Pages("RadPageViewPage1")
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub chkFromTable_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkFromTable.ToggleStateChanged
        Try
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
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub chkManualEntryField_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkManualEntryField.ToggleStateChanged
        Try
            If chkCalculatedField.Checked Then
                RadPageViewPage1.Item.Visibility = ElementVisibility.Visible
                RadPageViewPage2.Item.Visibility = ElementVisibility.Visible
                RadPageViewPage3.Item.Visibility = ElementVisibility.Visible
                RadPageViewPage4.Item.Visibility = ElementVisibility.Collapsed
                chkManualEntryField.Checked = False
                chkCalculatedField.Checked = True
                chkButtons.Checked = False
                RadPageView1.Visible = True
                RadPageView1.SelectedPage = RadPageView1.Pages("RadPageViewPage3")
            ElseIf chkButtons.Checked Then
                RadPageViewPage1.Item.Visibility = ElementVisibility.Collapsed
                RadPageViewPage2.Item.Visibility = ElementVisibility.Collapsed
                RadPageViewPage3.Item.Visibility = ElementVisibility.Collapsed
                RadPageViewPage4.Item.Visibility = ElementVisibility.Visible
                chkManualEntryField.Checked = False
                chkCalculatedField.Checked = False
                chkButtons.Checked = True
                RadPageView1.Visible = True
                RadPageView1.SelectedPage = RadPageView1.Pages("RadPageViewPage4")
            ElseIf chkManualEntryField.Checked Then
                RadPageViewPage1.Item.Visibility = ElementVisibility.Visible
                RadPageViewPage2.Item.Visibility = ElementVisibility.Visible
                RadPageViewPage3.Item.Visibility = ElementVisibility.Collapsed
                RadPageViewPage4.Item.Visibility = ElementVisibility.Collapsed
                chkManualEntryField.Checked = True
                chkCalculatedField.Checked = False
                chkButtons.Checked = False
                RadPageView1.Visible = True
                RadPageView1.SelectedPage = RadPageView1.Pages("RadPageViewPage1")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub chkManualList_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkManualList.ToggleStateChanged
        Try
            If chkManualList.Checked Then
                chkFromTable.Checked = False
                gv3.Enabled = True
                LoadBlankGridValueList()
                gv3.Rows.AddNew()
            Else
                gv3.Enabled = False
                gv3.Rows.Clear()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnEditExpression_Click(sender As Object, e As EventArgs) Handles btnEditExpression.Click
        Try
            Dim frm As New frmExpressionEditor
            frm.ProgCode = formId
            frm.expression = txtExpression.Text
            If chkIsForDetailLevel.Checked Then
                frm.Tag = 1
            Else
                frm.Tag = 0
            End If
            frm.ShowDialog()
            txtExpression.Text = frm.expression
            frm = Nothing
            GC.Collect()
            GC.WaitForPendingFinalizers()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndReferenceTable__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndReferenceTable._MYValidating
        Try
            Dim qry As String = "SELECT  UPPER (TABLE_NAME ) as TABLE_NAME FROM INFORMATION_SCHEMA.TABLES  "
            fndReferenceTable.Value = clsCommon.ShowSelectForm("TableList", qry, "TABLE_NAME", "TABLE_TYPE='BASE TABLE'", fndReferenceTable.Value, "", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndCustomFieldName__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCustomFieldName._MYValidating
        Try

            If chkManualEntryField.Checked OrElse chkCalculatedField.Checked Then
                LoadCustomField(isButtonClicked)
            Else
                Throw New Exception("Please select Either Manual Entry Field Or Calculated Field")
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndFieldName__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndFieldName._MYValidating
        Try
            If clsCommon.myLen(fndReferenceTable.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select Table First", Me.Text)
                Exit Sub
            End If
            Dim qry As String = "select  upper(sys.columns.Name) as FieldName from sys.columns inner join sys.tables on sys.tables.object_id=sys.columns.object_id   "
            fndFieldName.Value = clsCommon.ShowSelectForm("FiledList", qry, "FieldName", "sys.tables.name='" & fndReferenceTable.Value & "'", fndFieldName.Value, "", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        Try
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
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

    Sub LoadBlankGridCondition(type As EnumConditionType)
        gv2.Rows.Clear()
        gv2.Columns.Clear()
        If type = EnumCustomFieldType.TextType OrElse type = EnumCustomFieldType.FinderType OrElse type = EnumCustomFieldType.MultilineTextType OrElse type = EnumCustomFieldType.ComboListBoxType OrElse type = EnumCustomFieldType.NumberType OrElse type = EnumCustomFieldType.DateType Then

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

    Private Sub txtExpression_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtExpression.Validating

    End Sub

    Private Sub fndCustomFieldName_Load(sender As Object, e As EventArgs) Handles fndCustomFieldName.Load

    End Sub

    Private Sub fndMethodCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndMethodCode._MYValidating
        Dim qry As String = "select * from tspl_Standard_Method"
        fndMethodCode.Value = clsCommon.ShowSelectForm("fndStandardMethod", qry, "MethodCode", "", fndMethodCode.Value, "", isButtonClicked)
        lblMethodDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MethodName from tspl_Standard_Method where MethodCode='" & fndMethodCode.Value & "'"))
        Dim Arg As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MethodArg from tspl_Standard_Method where MethodCode='" & fndMethodCode.Value & "'"))
        If clsCommon.myLen(Arg) > 0 Then
            LoadBlankGv4(Arg)
        End If
    End Sub

    Private Sub fndMethodCode_Load(sender As Object, e As EventArgs) Handles fndMethodCode.Load

    End Sub

    Private Sub chkButtons_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkButtons.ToggleStateChanged
        Try
            If chkCalculatedField.Checked Then
                RadPageViewPage1.Item.Visibility = ElementVisibility.Visible
                RadPageViewPage2.Item.Visibility = ElementVisibility.Visible
                RadPageViewPage3.Item.Visibility = ElementVisibility.Visible
                RadPageViewPage4.Item.Visibility = ElementVisibility.Collapsed
                chkManualEntryField.Checked = False
                chkCalculatedField.Checked = True
                chkButtons.Checked = False
                RadPageView1.Visible = True
                RadPageView1.SelectedPage = RadPageView1.Pages("RadPageViewPage3")
            ElseIf chkButtons.Checked Then
                RadPageViewPage1.Item.Visibility = ElementVisibility.Collapsed
                RadPageViewPage2.Item.Visibility = ElementVisibility.Collapsed
                RadPageViewPage3.Item.Visibility = ElementVisibility.Collapsed
                RadPageViewPage4.Item.Visibility = ElementVisibility.Visible
                chkManualEntryField.Checked = False
                chkCalculatedField.Checked = False
                chkButtons.Checked = True
                RadPageView1.Visible = True
                RadPageView1.SelectedPage = RadPageView1.Pages("RadPageViewPage4")
            ElseIf chkManualEntryField.Checked Then
                RadPageViewPage1.Item.Visibility = ElementVisibility.Visible
                RadPageViewPage2.Item.Visibility = ElementVisibility.Visible
                RadPageViewPage3.Item.Visibility = ElementVisibility.Collapsed
                RadPageViewPage4.Item.Visibility = ElementVisibility.Collapsed
                chkManualEntryField.Checked = True
                chkCalculatedField.Checked = False
                chkButtons.Checked = False
                RadPageView1.Visible = True
                RadPageView1.SelectedPage = RadPageView1.Pages("RadPageViewPage1")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv4_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv4.CellValueChanged
        If Not isInsideLoadData Then
            isInsideLoadData = True
            If e.Column Is gv4.Columns(colDescription) Then
                Dim progcode As String = formId
                Dim qry As String = "select * from (select isnull(TSPL_CUSTOM_FIELD_HEAD.FieldName,'') as FieldName   from TSPL_CUSTOM_FIELD_MAPPING  left outer join TSPL_CUSTOM_FIELD_HEAD on TSPL_CUSTOM_FIELD_HEAD.Code=TSPL_CUSTOM_FIELD_MAPPING.Custom_Field_Code where  Program_code='" & progcode & "' and isnull(TSPL_CUSTOM_FIELD_HEAD.FieldName,'')<>'' and isnull(TSPL_CUSTOM_FIELD_HEAD.type,0)=1 union all select description from TSPL_SCREEN_CONTROL_MASTER where ProgramCode='" & progcode & "' and isnull(Description,'')<>'' )xx"
                gv4.Rows(e.RowIndex).Cells(colDescription).Value = clsCommon.myCstr(clsCommon.ShowSelectForm("rptArgFieldFinder", qry, "FieldName", "", "", "", True))
            End If
            isInsideLoadData = False
        End If
    End Sub
End Class