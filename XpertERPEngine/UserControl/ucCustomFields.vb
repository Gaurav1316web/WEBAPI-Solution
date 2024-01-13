Imports common
Imports Telerik.WinControls.UI
Imports System.Windows.Forms
Public Class ucCustomFields
#Region "Variables"
    Public Report_ID As String = ""
    Dim ArrMapping As List(Of clsCustomFieldMapping)

#End Region

    Public Sub LoadCustomControls()
        Dim Counter As Integer = 0
        Evaluator1 = New Evaluator
        ArrMapping = clsCustomFieldMapping.GetData(Report_ID, True, Nothing)
        Dim ControlsInvolvedinCalculation As List(Of String) = ucCustomFields.getControlInvolvedInCalculation(Report_ID)

        If ArrMapping IsNot Nothing AndAlso ArrMapping.Count > 0 Then
            If ArrMapping.Count > 25 Then
                Throw New Exception("Support maximum 25 custom fields")
            End If
            For Each obj As clsCustomFieldMapping In ArrMapping
                ''Adding Lables

                Dim RadLabel As common.Controls.MyLabel = Nothing

                If obj.Type = EnumCustomFieldType.Buttons Then
                    Dim RadButton1 As New RadButton
                    CType(RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
                    RadButton1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                    RadButton1.Location = New System.Drawing.Point(176, 10 + (Counter * 20))
                    RadButton1.Name = obj.Custom_Field_Code
                    RadButton1.Size = New System.Drawing.Size(274, 18)
                    RadButton1.TabIndex = Counter
                    RadButton1.Text = obj.Custom_Field_Name
                    RadButton1.Tag = obj.MethodCode
                    RadButton1.Visible = True
                    Controls.Add(RadButton1)
                    AddHandler RadButton1.Click, AddressOf btn_Click
                    CType(RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
                End If


                If obj.Type = EnumCustomFieldType.CheckType Then
                    Dim chkBox As New common.Controls.MyCheckBox()
                    CType(chkBox, System.ComponentModel.ISupportInitialize).BeginInit()
                    chkBox.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                    chkBox.Location = New System.Drawing.Point(176, 10 + (Counter * 20))
                    chkBox.MyLinkLable1 = RadLabel
                    chkBox.MyLinkLable2 = Nothing
                    chkBox.Name = obj.Custom_Field_Code
                    chkBox.Size = New System.Drawing.Size(274, 18)
                    chkBox.TabIndex = Counter
                    'chkBox.Checked = clsCommon.myCBool(obj.Default_Value)
                    chkBox.Tag = obj.Custom_Field_Name
                    Controls.Add(chkBox)
                    CType(chkBox, System.ComponentModel.ISupportInitialize).EndInit()
                End If

                If obj.Type = EnumCustomFieldType.NumberType OrElse obj.Type = EnumCustomFieldType.FinderType OrElse obj.Type = EnumCustomFieldType.TextType OrElse obj.Type = EnumCustomFieldType.MultilineTextType OrElse obj.Type = EnumCustomFieldType.ComboListBoxType Then
                    RadLabel = New common.Controls.MyLabel()
                    CType(RadLabel, System.ComponentModel.ISupportInitialize).BeginInit()
                    RadLabel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                    RadLabel.Location = New System.Drawing.Point(5, 10 + (Counter * 20))
                    RadLabel.Name = "Lbl" + obj.Custom_Field_Field_Name
                    RadLabel.Size = New System.Drawing.Size(164, 16)
                    RadLabel.AutoSize = False
                    RadLabel.TabIndex = 20
                    'RadLabel.Width = 170
                    RadLabel.TextWrap = False
                    RadLabel.Text = obj.Custom_Field_Name
                    Controls.Add(RadLabel)
                    CType(RadLabel, System.ComponentModel.ISupportInitialize).EndInit()
                    ''End of Adding Lables
                End If
                TabIndex += 1
                If obj.Type = EnumCustomFieldType.NumberType Then
                    Dim txtNumBox As New common.MyNumBox()
                    CType(txtNumBox, System.ComponentModel.ISupportInitialize).BeginInit()
                    txtNumBox.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                    txtNumBox.Location = New System.Drawing.Point(176, 10 + (Counter * 20))
                    txtNumBox.MaxLength = 200
                    txtNumBox.MendatroryField = obj.Is_Mandatory
                    txtNumBox.MyLinkLable1 = RadLabel
                    txtNumBox.MyLinkLable2 = Nothing
                    txtNumBox.Name = obj.Custom_Field_Code
                    txtNumBox.Size = New System.Drawing.Size(274, 18)
                    txtNumBox.TabIndex = TabIndex
                    txtNumBox.DecimalPlaces = 2
                    'txtNumBox.Value = clsCommon.myCdbl(obj.Default_Value)
                    txtNumBox.Tag = obj.Custom_Field_Field_Name

                    txtNumBox.IsUnique = IIf(obj.IsUnique = 1, True, False)
                    txtNumBox.IsSourceFromTable = IIf(obj.IsSourceFromTable = 1, True, False)
                    txtNumBox.isCalculatedField = IIf(obj.Is_CalCulated_Column = 1, True, False)
                    txtNumBox.CalculationExpression = clsCommon.myCstr(obj.CalculationExpression)
                    txtNumBox.IsSourceFromValueList = IIf(obj.IsSourceFromValueList = 1, True, False)
                    txtNumBox.ReferenceFieldName = clsCommon.myCstr(obj.ReferenceFieldName)
                    txtNumBox.ReferenceTableName = clsCommon.myCstr(obj.ReferenceTableName)
                    txtNumBox.FieldMaxLength = obj.MaxLength

                    Controls.Add(txtNumBox)
                    '' Add Delegates Here 
                    'If obj.Is_Validate Then
                    '    AddHandler txtNumBox.Validating, AddressOf txtNumBox_Validating
                    'End If
                    'If ControlsInvolvedinCalculation IsNot Nothing AndAlso ControlsInvolvedinCalculation.Count > 0 Then
                    '    If FindStringInList(ControlsInvolvedinCalculation, obj.Custom_Field_Field_Name) Then
                    '        AddHandler txtNumBox.Validating, AddressOf txtNumBox_Validating
                    '    End If
                    'End If
                    '' End of Delegates
                    CType(txtNumBox, System.ComponentModel.ISupportInitialize).EndInit()
                ElseIf obj.Type = EnumCustomFieldType.TextType Then
                    Dim txtBox As New common.Controls.MyTextBox()
                    CType(txtBox, System.ComponentModel.ISupportInitialize).BeginInit()
                    txtBox.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                    txtBox.Location = New System.Drawing.Point(176, 10 + (Counter * 20))
                    txtBox.MaxLength = 200
                    txtBox.MendatroryField = obj.Is_Mandatory
                    txtBox.MyLinkLable1 = RadLabel
                    txtBox.MyLinkLable2 = Nothing
                    txtBox.Name = obj.Custom_Field_Code
                    txtBox.Size = New System.Drawing.Size(274, 18)
                    txtBox.TabIndex = Counter
                    'txtBox.Text = obj.Default_Value
                    txtBox.Tag = obj.Custom_Field_Name

                    txtBox.IsUnique = IIf(obj.IsUnique = 1, True, False)
                    txtBox.IsSourceFromTable = IIf(obj.IsSourceFromTable = 1, True, False)
                    txtBox.isCalculatedField = IIf(obj.Is_CalCulated_Column = 1, True, False)
                    txtBox.CalculationExpression = clsCommon.myCstr(obj.CalculationExpression)
                    txtBox.IsSourceFromValueList = IIf(obj.IsSourceFromValueList = 1, True, False)
                    txtBox.ReferenceFieldName = clsCommon.myCstr(obj.ReferenceFieldName)
                    txtBox.ReferenceTableName = clsCommon.myCstr(obj.ReferenceTableName)
                    txtBox.FieldMaxLength = obj.MaxLength

                    Controls.Add(txtBox)
                    '' Add Delegates Here 
                    If obj.Is_Validate Then
                        '                        AddHandler txtBox.Validating, AddressOf textBox_Validating
                    End If
                    '' End of Delegates
                    CType(txtBox, System.ComponentModel.ISupportInitialize).EndInit()
                ElseIf obj.Type = EnumCustomFieldType.FinderType Then
                    Dim RadLabel2 As common.Controls.MyLabel = New common.Controls.MyLabel()
                    CType(RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
                    RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                    RadLabel2.Location = New System.Drawing.Point(455, 10 + (Counter * 20))
                    RadLabel2.Name = "Lablx" + obj.Custom_Field_Field_Name
                    RadLabel2.Size = New System.Drawing.Size(164, 16)
                    RadLabel.AutoSize = False
                    RadLabel2.TabIndex = 20
                    RadLabel2.BorderVisible = True
                    RadLabel2.TextWrap = False
                    RadLabel2.Text = ""
                    RadLabel2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
                    Controls.Add(RadLabel2)
                    CType(RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()

                    Dim txtFinder As New common.UserControls.txtFinder()
                    txtFinder.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                    txtFinder.Location = New System.Drawing.Point(176, 10 + (Counter * 20))
                    txtFinder.MendatroryField = obj.Is_Mandatory
                    txtFinder.MyLinkLable1 = RadLabel
                    txtFinder.MyLinkLable2 = RadLabel2
                    txtFinder.Name = obj.Custom_Field_Code
                    txtFinder.Size = New System.Drawing.Size(274, 18)
                    txtFinder.TabIndex = Counter

                    txtFinder.IsUnique = IIf(obj.IsUnique = 1, True, False)
                    txtFinder.IsSourceFromTable = IIf(obj.IsSourceFromTable = 1, True, False)
                    txtFinder.isCalculatedField = IIf(obj.Is_CalCulated_Column = 1, True, False)
                    txtFinder.CalculationExpression = clsCommon.myCstr(obj.CalculationExpression)
                    txtFinder.IsSourceFromValueList = IIf(obj.IsSourceFromValueList = 1, True, False)
                    txtFinder.ReferenceFieldName = clsCommon.myCstr(obj.ReferenceFieldName)
                    txtFinder.ReferenceTableName = clsCommon.myCstr(obj.ReferenceTableName)
                    txtFinder.FieldMaxLength = obj.MaxLength

                    Controls.Add(txtFinder)
                    txtFinder.Tag = obj.Custom_Field_Name
                    If obj.Is_Validate Then
                        AddHandler txtFinder._MYValidating, AddressOf Finder_Validate
                    End If
                ElseIf obj.Type = EnumCustomFieldType.DateType Then
                    Dim txtDateTimePicker As New common.Controls.MyDateTimePicker()
                    CType(txtDateTimePicker, System.ComponentModel.ISupportInitialize).BeginInit()
                    txtDateTimePicker.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                    txtDateTimePicker.Location = New System.Drawing.Point(176, 10 + (Counter * 20))
                    txtDateTimePicker.MendatroryField = obj.Is_Mandatory
                    txtDateTimePicker.MyLinkLable1 = RadLabel
                    txtDateTimePicker.MyLinkLable2 = Nothing
                    txtDateTimePicker.Name = obj.Custom_Field_Code
                    txtDateTimePicker.Size = New System.Drawing.Size(274, 18)
                    txtDateTimePicker.TabIndex = Counter
                    txtDateTimePicker.Tag = obj.Custom_Field_Name
                    txtDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short
                    txtDateTimePicker.CustomFormat = "dd/MM/yyyy"
                    txtDateTimePicker.IsUnique = IIf(obj.IsUnique = 1, True, False)
                    txtDateTimePicker.IsSourceFromTable = IIf(obj.IsSourceFromTable = 1, True, False)
                    txtDateTimePicker.isCalculatedField = IIf(obj.Is_CalCulated_Column = 1, True, False)
                    txtDateTimePicker.CalculationExpression = clsCommon.myCstr(obj.CalculationExpression)
                    txtDateTimePicker.IsSourceFromValueList = IIf(obj.IsSourceFromValueList = 1, True, False)
                    txtDateTimePicker.ReferenceFieldName = clsCommon.myCstr(obj.ReferenceFieldName)
                    txtDateTimePicker.ReferenceTableName = clsCommon.myCstr(obj.ReferenceTableName)
                    txtDateTimePicker.FieldMaxLength = obj.MaxLength

                    Controls.Add(txtDateTimePicker)
                    CType(txtDateTimePicker, System.ComponentModel.ISupportInitialize).EndInit()

                End If



                'If obj.Is_Validate Then
                '    Dim RadLabel2 As common.Controls.MyLabel = New common.Controls.MyLabel()
                '    CType(RadLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
                '    RadLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                '    RadLabel2.Location = New System.Drawing.Point(455, 10 + (Counter * 20))
                '    RadLabel2.Name = "Lablx" + clsCommon.myCstr(Counter)
                '    RadLabel2.Size = New System.Drawing.Size(164, 16)
                '    RadLabel.AutoSize = False
                '    RadLabel2.TabIndex = 20
                '    RadLabel2.BorderVisible = True
                '    RadLabel2.TextWrap = False
                '    RadLabel2.Text = ""
                '    RadLabel2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
                '    Controls.Add(RadLabel2)
                '    CType(RadLabel2, System.ComponentModel.ISupportInitialize).EndInit()

                '    Dim txtFinder As New common.UserControls.txtFinder()
                '    txtFinder.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                '    txtFinder.Location = New System.Drawing.Point(176, 10 + (Counter * 20))
                '    txtFinder.MendatroryField = obj.Is_Mandatory
                '    txtFinder.MyLinkLable1 = RadLabel
                '    txtFinder.MyLinkLable2 = RadLabel2
                '    txtFinder.Name = obj.Custom_Field_Code
                '    txtFinder.Size = New System.Drawing.Size(274, 18)
                '    txtFinder.TabIndex = Counter
                '    Controls.Add(txtFinder)
                '    txtFinder.Tag = obj.Custom_Field_Name
                '    AddHandler txtFinder._MYValidating, AddressOf Finder_Validate

                'ElseIf obj.Type = EnumCustomFieldType.TextType Then
                '    Dim txtBox As New common.Controls.MyTextBox()
                '    CType(txtBox, System.ComponentModel.ISupportInitialize).BeginInit()
                '    txtBox.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                '    txtBox.Location = New System.Drawing.Point(176, 10 + (Counter * 20))
                '    txtBox.MaxLength = 200
                '    txtBox.MendatroryField = obj.Is_Mandatory
                '    txtBox.MyLinkLable1 = RadLabel
                '    txtBox.MyLinkLable2 = Nothing
                '    txtBox.Name = obj.Custom_Field_Code
                '    txtBox.Size = New System.Drawing.Size(274, 18)
                '    txtBox.TabIndex = Counter
                '    'txtBox.Text = obj.Default_Value
                '    txtBox.Tag = obj.Custom_Field_Name
                '    Controls.Add(txtBox)

                '    CType(txtBox, System.ComponentModel.ISupportInitialize).EndInit()
                'ElseIf obj.Type = EnumCustomFieldType.NumberType Then
                '    Dim txtNumBox As New common.MyNumBox()
                '    CType(txtNumBox, System.ComponentModel.ISupportInitialize).BeginInit()
                '    txtNumBox.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                '    txtNumBox.Location = New System.Drawing.Point(176, 10 + (Counter * 20))
                '    txtNumBox.MaxLength = 200
                '    txtNumBox.MendatroryField = obj.Is_Mandatory
                '    txtNumBox.MyLinkLable1 = RadLabel
                '    txtNumBox.MyLinkLable2 = Nothing
                '    txtNumBox.Name = obj.Custom_Field_Code
                '    txtNumBox.Size = New System.Drawing.Size(274, 18)
                '    txtNumBox.TabIndex = Counter
                '    txtNumBox.DecimalPlaces = 2
                '    'txtNumBox.Value = clsCommon.myCdbl(obj.Default_Value)
                '    txtNumBox.Tag = obj.Custom_Field_Name
                '    Controls.Add(txtNumBox)
                '    CType(txtNumBox, System.ComponentModel.ISupportInitialize).EndInit()
                'ElseIf obj.Type = EnumCustomFieldType.DateType Then
                '    Dim txtDateTimePicker As New common.Controls.MyDateTimePicker()
                '    CType(txtDateTimePicker, System.ComponentModel.ISupportInitialize).BeginInit()
                '    txtDateTimePicker.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                '    txtDateTimePicker.Location = New System.Drawing.Point(176, 10 + (Counter * 20))
                '    txtDateTimePicker.MendatroryField = obj.Is_Mandatory
                '    txtDateTimePicker.MyLinkLable1 = RadLabel
                '    txtDateTimePicker.MyLinkLable2 = Nothing
                '    txtDateTimePicker.Name = obj.Custom_Field_Code
                '    txtDateTimePicker.Size = New System.Drawing.Size(274, 18)
                '    txtDateTimePicker.TabIndex = Counter

                '    'If clsCommon.myLen(obj.Default_Value) > 0 Then
                '    '    txtDateTimePicker.Value = clsCommon.myCDate(obj.Default_Value)
                '    'End If
                '    txtDateTimePicker.Tag = obj.Custom_Field_Name
                '    txtDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short
                '    txtDateTimePicker.CustomFormat = "dd/MM/yyyy"
                '    Controls.Add(txtDateTimePicker)
                '    CType(txtDateTimePicker, System.ComponentModel.ISupportInitialize).EndInit()
                'ElseIf obj.Type = EnumCustomFieldType.CheckType Then
                '    Dim chkBox As New common.Controls.MyCheckBox()
                '    CType(chkBox, System.ComponentModel.ISupportInitialize).BeginInit()
                '    chkBox.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                '    chkBox.Location = New System.Drawing.Point(176, 10 + (Counter * 20))
                '    chkBox.MyLinkLable1 = RadLabel
                '    chkBox.MyLinkLable2 = Nothing
                '    chkBox.Name = obj.Custom_Field_Code
                '    chkBox.Size = New System.Drawing.Size(274, 18)
                '    chkBox.TabIndex = Counter
                '    'chkBox.Checked = clsCommon.myCBool(obj.Default_Value)
                '    chkBox.Tag = obj.Custom_Field_Name
                '    Controls.Add(chkBox)
                '    CType(chkBox, System.ComponentModel.ISupportInitialize).EndInit()
                'End If
                Counter += 1
            Next
            For i As Integer = 0 To Controls.Count - 1
                If ControlsInvolvedinCalculation IsNot Nothing AndAlso ControlsInvolvedinCalculation.Count > 0 Then
                    ' 
                    If FindStringInList(ControlsInvolvedinCalculation, Controls(i).Tag) Then
                        Dim txtNumbox As common.MyNumBox = Controls(i)
                        AddHandler txtNumbox.Validating, AddressOf txtNumBox_Validating
                    End If
                End If
            Next
            Dim parent As Object = Nothing
            'clsCommon.MyMessageBoxShow("Loading Screen Controls")
            Dim control As System.Windows.Forms.Control = Me
            While True
                parent = control.parent
                If (TypeOf parent Is RadForm OrElse TypeOf parent Is System.Windows.Forms.Form OrElse TypeOf parent Is FrmMainTranScreen) Then
                    Exit While
                End If
                control = parent
            End While
            FindAnyCntrolByFieldName(parent, ControlsInvolvedinCalculation, Nothing)
        End If
    End Sub
    Public Sub FindAnyCntrolByFieldName(ByRef formname As XpertERPEngine.FrmMainTranScreen, str As List(Of String), Optional ByVal contrl As Control = Nothing)

        If IsNothing(contrl) Then
            For Each ctrl As Control In formname.Controls
                If ctrl.HasChildren = True AndAlso Not (TypeOf ctrl Is common.UserControls.txtFinder OrElse TypeOf ctrl Is common.UserControls.txtNavigator) Then
                    FindAnyCntrolByFieldName(formname, str, ctrl)
                End If
                If TypeOf ctrl Is MyNumBox Then
                    If FindStringInList(str, TryCast(ctrl, MyNumBox).FieldName) Then
                        Dim txtNumbox As common.MyNumBox = ctrl
                        If txtNumbox.ReadOnly OrElse (Not txtNumbox.Enabled) Then
                            AddHandler txtNumbox.TextChanged, AddressOf txtNumBox_Validating
                        Else
                            AddHandler txtNumbox.Validating, AddressOf txtNumBox_Validating
                        End If

                    End If
                End If
                If TypeOf ctrl Is common.Controls.MyTextBox Then
                    If FindStringInList(str, TryCast(ctrl, common.Controls.MyTextBox).FieldName) Then
                        Dim txtNumbox As common.Controls.MyTextBox = ctrl
                        If txtNumbox.ReadOnly OrElse (Not txtNumbox.Enabled) Then
                            AddHandler txtNumbox.TextChanged, AddressOf txtNumBox_Validating
                        Else
                            AddHandler txtNumbox.Validating, AddressOf txtNumBox_Validating
                        End If
                    End If
                End If
            Next
        Else
            For Each ctrl As Control In contrl.Controls
                If ctrl.HasChildren = True AndAlso Not (TypeOf ctrl Is common.UserControls.txtFinder OrElse TypeOf ctrl Is common.UserControls.txtNavigator) Then
                    FindAnyCntrolByFieldName(formname, str, ctrl)
                End If
                If TypeOf ctrl Is MyNumBox Then
                    If FindStringInList(str, TryCast(ctrl, MyNumBox).FieldName) Then
                        Dim txtNumbox As common.MyNumBox = ctrl
                        If txtNumbox.ReadOnly OrElse (Not txtNumbox.Enabled) Then
                            AddHandler txtNumbox.TextChanged, AddressOf txtNumBox_Validating
                        Else
                            AddHandler txtNumbox.Validating, AddressOf txtNumBox_Validating
                        End If
                    End If
                End If
                If TypeOf ctrl Is common.Controls.MyTextBox Then
                    If FindStringInList(str, TryCast(ctrl, common.Controls.MyTextBox).FieldName) Then
                        Dim txtNumbox As common.Controls.MyTextBox = ctrl
                        If txtNumbox.ReadOnly OrElse (Not txtNumbox.Enabled) Then
                            AddHandler txtNumbox.TextChanged, AddressOf txtNumBox_Validating
                        Else
                            AddHandler txtNumbox.Validating, AddressOf txtNumBox_Validating
                        End If
                    End If
                End If
            Next
        End If
    End Sub
    Public Sub getControlValue(controlName As String, ByRef value As Object)
        Try
            For i As Integer = 0 To Controls.Count - 1
                If clsCommon.CompairString(Controls(i).Tag, controlName) = CompairStringResult.Equal Then
                    value = CType(Controls(i), common.MyNumBox).Value
                    Exit For
                End If
            Next
            If value Is Nothing OrElse clsCommon.myLen(value) <= 0 Then
                Dim parent As Object = Nothing
                Dim control As System.Windows.Forms.Control = Me
                While True
                    parent = control.Parent
                    If (TypeOf parent Is RadForm OrElse TypeOf parent Is System.Windows.Forms.Form OrElse TypeOf parent Is FrmMainTranScreen) Then
                        Exit While
                    End If
                    control = parent
                End While
                Dim ctr As New Control
                clsCreateAllTables.FindAnyCntrolByFieldName(CType(parent, FrmMainTranScreen), ctr, controlName, Nothing)
                If ctr IsNot Nothing Then
                    If TypeOf ctr Is MyNumBox Then
                        value = TryCast(ctr, MyNumBox).Value
                    End If
                    If TypeOf ctr Is Common.Controls.MyTextBox Then
                        value = TryCast(ctr, common.Controls.MyTextBox).Text
                    End If
                    If TypeOf ctr Is common.UserControls.txtFinder Then
                        value = TryCast(ctr, common.UserControls.txtFinder).Value
                    End If
                    If TypeOf ctr Is common.UserControls.txtNavigator Then
                        value = TryCast(ctr, common.UserControls.txtNavigator).Value
                    End If
                    If TypeOf ctr Is common.Controls.MyDateTimePicker Then
                        value = TryCast(ctr, common.Controls.MyDateTimePicker).Value
                    End If
                    If TypeOf ctr Is common.Controls.MyComboBox Then
                        value = TryCast(ctr, common.Controls.MyComboBox).Text
                    End If
                    If TypeOf ctr Is common.Controls.MyLabel Then
                        value = TryCast(ctr, common.Controls.MyLabel).Text
                    End If
                End If

            End If

        Catch ex As Exception
            value = "Error: " & ex.Message
        End Try
    End Sub
    Private Sub btn_Click(sender As Object, e As EventArgs)
        Try
            Dim MethodCode As String = TryCast(sender, RadButton).Tag
            Dim methodName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MethodName from tspl_Standard_Method where MethodCode='" & MethodCode & "'"))
            Dim ClassName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ClassName from tspl_Standard_Method where MethodCode='" & MethodCode & "'"))
            Dim arg As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MethodArg from TSPL_CUSTOM_FIELD_MAPPING where custom_field_Code='" & sender.name & "'"))
            If clsCommon.myLen(methodName) <= 0 OrElse clsCommon.myLen(ClassName) <= 0 Then
                Throw New Exception("Method Detail Not Found")
            End If
            'clsCommon.MyMessageBoxShow("Custom Button Click")
            If clsCommon.myLen(arg) > 0 Then
                Dim argArray() As String = arg.Split(",")
                Dim obj(0 To argArray.Length - 1) As Object
                For i As Integer = 0 To argArray.Length - 1
                    If clsCommon.CompairString(argArray(i), "#") = CompairStringResult.Equal Then
                        obj(i) = ""
                    Else
                        getControlValue(argArray(i), obj(i))
                    End If
                    ' obj(0) = IIf(clsCommon.CompairString(argArray(i), "#") = CompairStringResult.Equal, "", argArray(i))
                Next
                clsCreateAllTables.InvokeMethodFromCurrentAssembly(ClassName, methodName, obj)
            Else
                clsCreateAllTables.InvokeMethodFromCurrentAssembly(ClassName, methodName, Nothing)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub Finder_Validate(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean)
        Try
            Dim SenderObject As common.UserControls.txtFinder
            SenderObject = CType(sender, common.UserControls.txtFinder)

            'Dim qry As String = "select Value,Description from TSPL_CUSTOM_FIELD_DETAIL "
            'Dim whrclas As String = " Custom_Field_Code='" + SenderObject.Name + "' "
            'SenderObject.Value = clsCommon.ShowSelectForm("CF" + SenderObject.Name, qry, "Value", whrclas, SenderObject.Value, "", True)
            'SenderObject.MyLinkLable2.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_CUSTOM_FIELD_DETAIL where Value='" + SenderObject.Value + "'"))

            Dim qry As String = " select distinct  " & SenderObject.ReferenceFieldName & " as Value  from " & SenderObject.ReferenceTableName
            Dim whrclas As String = ""
            SenderObject.Value = clsCommon.ShowSelectForm("CF" + SenderObject.Name, qry, "Value", whrclas, SenderObject.Value, "", True)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Private Sub textBox_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs)
        'clsCommon.MyMessageBoxShow("TextBox Changed")
        Try
            Dim SenderObject As common.Controls.MyTextBox
            SenderObject = CType(sender, common.Controls.MyTextBox)
            clsCommon.MyMessageBoxShow(SenderObject.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    WithEvents Evaluator1 As Evaluator
    Private Sub txtNumBox_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs)
        'clsCommon.MyMessageBoxShow("TextBox Changed")
        Try
            'clsCommon.MyMessageBoxShow("Hi")
            Dim SenderObject As common.MyNumBox
            SenderObject = CType(sender, common.MyNumBox)
            Dim TargetObject As common.MyNumBox = Nothing
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select calculationExpression,Custom_Field_Code from TSPL_CUSTOM_FIELD_MAPPING where Program_Code='" & Report_ID & "' and isnull(calculationExpression,'')<>''")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    If clsCommon.myCstr(dt.Rows(i)("calculationExpression")).Contains("#" & SenderObject.Tag & "#") Then
                        For j As Integer = 0 To Controls.Count - 1
                            If clsCommon.CompairString(Controls(j).Name, clsCommon.myCstr(dt.Rows(i)("Custom_Field_Code"))) = CompairStringResult.Equal Then
                                TargetObject = CType(Controls(j), common.MyNumBox)
                                TargetObject.Value = clsCommon.myCdbl(Evaluator1.Eval(dt.Rows(i)("calculationExpression")))
                            End If

                        Next
                    End If
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub Evaluator1_GetVariable(ByVal name As String, ByRef value As Object) Handles Evaluator1.GetVariable
        Try
            For i As Integer = 0 To Controls.Count - 1
                If clsCommon.CompairString(Controls(i).Tag, name) = CompairStringResult.Equal Then
                    value = clsCommon.myCdbl(CType(Controls(i), common.MyNumBox).Value)
                    Exit For
                End If
            Next
            If value Is Nothing OrElse clsCommon.myLen(value) <= 0 Then
                Dim parent As Object = Nothing
                Dim control As System.Windows.Forms.Control = Me
                While True
                    parent = control.parent
                    If (TypeOf parent Is RadForm OrElse TypeOf parent Is System.Windows.Forms.Form OrElse TypeOf parent Is FrmMainTranScreen) Then
                        Exit While
                    End If
                    control = parent
                End While
                Dim ctr As New MyNumBox
                clsCreateAllTables.FindAnyCntrolByFieldName(CType(parent, FrmMainTranScreen), ctr, name, Nothing)
                If ctr IsNot Nothing Then
                    value = clsCommon.myCdbl(ctr.Value)
                End If
            End If
        Catch ex As Exception
            value = "Error: " & ex.Message
        End Try
    End Sub
    Public Function AllowToSave() As Boolean
        For Each ctr As System.Windows.Forms.Control In Me.Controls
            If ctr.GetType Is GetType(common.Controls.MyTextBox) Then
                Dim myControl As common.Controls.MyTextBox = CType(ctr, common.Controls.MyTextBox)
                If myControl.MendatroryField AndAlso clsCommon.myLen(myControl.Text) <= 0 Then
                    myControl.Focus()
                    Throw New Exception("Please enter the value of " + clsCommon.myCstr(myControl.Tag))
                End If
            ElseIf ctr.GetType Is GetType(common.MyNumBox) Then
                Dim myControl As common.MyNumBox = CType(ctr, common.MyNumBox)
                If myControl.MendatroryField AndAlso clsCommon.myLen(myControl.Text) <= 0 Then
                    myControl.Focus()
                    Throw New Exception("Please enter the value of " + clsCommon.myCstr(myControl.Tag))
                End If
            ElseIf ctr.GetType Is GetType(common.UserControls.txtFinder) Then
                Dim myControl As common.UserControls.txtFinder = CType(ctr, common.UserControls.txtFinder)
                If myControl.MendatroryField AndAlso clsCommon.myLen(myControl.Value) <= 0 Then
                    myControl.Focus()
                    Throw New Exception("Please select the value of " + clsCommon.myCstr(myControl.Tag))
                End If
            ElseIf ctr.GetType Is GetType(common.Controls.MyDateTimePicker) Then
                Dim myControl As common.Controls.MyDateTimePicker = CType(ctr, common.Controls.MyDateTimePicker)
                If myControl.MendatroryField AndAlso clsCommon.myLen(myControl.Value) <= 0 Then
                    myControl.Focus()
                    Throw New Exception("Please select the value of " + clsCommon.myCstr(myControl.Tag))
                End If
            End If
        Next
        Return True
    End Function

    Public Function GetData(ByRef arr As List(Of clsCustomFieldValues))
        For Each ctr As System.Windows.Forms.Control In Me.Controls
            Dim obj As clsCustomFieldValues = New clsCustomFieldValues()
            If ctr.GetType Is GetType(common.Controls.MyTextBox) Then
                Dim myControl As common.Controls.MyTextBox = CType(ctr, common.Controls.MyTextBox)
                obj.Custom_Field_Code = myControl.Name
                obj.Value = myControl.Text
            ElseIf ctr.GetType Is GetType(common.MyNumBox) Then
                Dim myControl As common.MyNumBox = CType(ctr, common.MyNumBox)
                obj.Custom_Field_Code = myControl.Name
                obj.Value = clsCommon.myFormat(myControl.Value)
            ElseIf ctr.GetType Is GetType(common.UserControls.txtFinder) Then
                Dim myControl As common.UserControls.txtFinder = CType(ctr, common.UserControls.txtFinder)
                obj.Custom_Field_Code = myControl.Name
                obj.Value = myControl.Value
            ElseIf ctr.GetType Is GetType(common.Controls.MyDateTimePicker) Then
                Dim myControl As common.Controls.MyDateTimePicker = CType(ctr, common.Controls.MyDateTimePicker)
                obj.Custom_Field_Code = myControl.Name
                If clsCommon.myLen(myControl.Value) > 0 Then
                    obj.Value = clsCommon.GetPrintDate(myControl.Value, "dd/MMM/yyyy")
                End If
            ElseIf ctr.GetType Is GetType(common.Controls.MyCheckBox) Then
                Dim myControl As common.Controls.MyCheckBox = CType(ctr, common.Controls.MyCheckBox)
                obj.Custom_Field_Code = myControl.Name
                obj.Value = IIf(myControl.Checked, "1", "0")
            End If
            If clsCommon.myLen(obj.Value) > 0 Then
                obj.Program_Code = Report_ID
                arr.Add(obj)
            End If
        Next
        Return arr
    End Function

    Public Sub LoadData(ByVal Transaction_Code As String)
        Try
            Dim Arr As List(Of clsCustomFieldValues) = clsCustomFieldValues.GetData(Me.Report_ID, Transaction_Code, False)
            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                For Each obj As clsCustomFieldValues In Arr
                    For Each ctr As System.Windows.Forms.Control In Me.Controls
                        If ctr.GetType Is GetType(common.Controls.MyTextBox) Then
                            Dim myControl As common.Controls.MyTextBox = CType(ctr, common.Controls.MyTextBox)
                            If clsCommon.CompairString(obj.Custom_Field_Code, myControl.Name) = CompairStringResult.Equal Then
                                myControl.Text = obj.Value
                            End If
                        ElseIf ctr.GetType Is GetType(common.MyNumBox) Then
                            Dim myControl As common.MyNumBox = CType(ctr, common.MyNumBox)
                            If clsCommon.CompairString(obj.Custom_Field_Code, myControl.Name) = CompairStringResult.Equal Then
                                myControl.Value = clsCommon.myCdbl(obj.Value)
                            End If
                        ElseIf ctr.GetType Is GetType(common.UserControls.txtFinder) Then
                            Dim myControl As common.UserControls.txtFinder = CType(ctr, common.UserControls.txtFinder)
                            If clsCommon.CompairString(obj.Custom_Field_Code, myControl.Name) = CompairStringResult.Equal Then
                                myControl.Value = obj.Value
                                myControl.MyLinkLable2.Text = obj.ValueDescription
                            End If
                        ElseIf ctr.GetType Is GetType(common.Controls.MyDateTimePicker) Then
                            Dim myControl As common.Controls.MyDateTimePicker = CType(ctr, common.Controls.MyDateTimePicker)
                            If clsCommon.CompairString(obj.Custom_Field_Code, myControl.Name) = CompairStringResult.Equal Then
                                If clsCommon.myLen(obj.Value) > 0 Then
                                    myControl.Value = clsCommon.myCDate(obj.Value)
                                End If
                            End If
                        ElseIf ctr.GetType Is GetType(common.Controls.MyCheckBox) Then
                            Dim myControl As common.Controls.MyCheckBox = CType(ctr, common.Controls.MyCheckBox)
                            If clsCommon.CompairString(obj.Custom_Field_Code, myControl.Name) = CompairStringResult.Equal Then
                                myControl.Checked = IIf(obj.Value = 1, True, False)
                            End If
                        End If
                    Next
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Sub BlankAllControls()
        For Each ctr As System.Windows.Forms.Control In Me.Controls
            If ctr.GetType Is GetType(common.Controls.MyTextBox) Then
                Dim myControl As common.Controls.MyTextBox = CType(ctr, common.Controls.MyTextBox)
                myControl.Text = ""
            ElseIf ctr.GetType Is GetType(common.MyNumBox) Then
                Dim myControl As common.MyNumBox = CType(ctr, common.MyNumBox)
                myControl.Value = Nothing
            ElseIf ctr.GetType Is GetType(common.UserControls.txtFinder) Then
                Dim myControl As common.UserControls.txtFinder = CType(ctr, common.UserControls.txtFinder)
                myControl.Value = ""
                myControl.MyLinkLable2.Text = ""
            ElseIf ctr.GetType Is GetType(common.Controls.MyDateTimePicker) Then
                Dim myControl As common.Controls.MyDateTimePicker = CType(ctr, common.Controls.MyDateTimePicker)
                Dim str As String = GetDefaultValue(myControl.Name)
                myControl.Value = clsCommon.GETSERVERDATE()
            ElseIf ctr.GetType Is GetType(common.Controls.MyCheckBox) Then
                Dim myControl As common.Controls.MyCheckBox = CType(ctr, common.Controls.MyCheckBox)
                myControl.Checked = False
            End If
        Next
    End Sub

    Public Sub SetDefaultValues()
        For Each ctr As System.Windows.Forms.Control In Me.Controls
            If ctr.GetType Is GetType(common.Controls.MyTextBox) Then
                Dim myControl As common.Controls.MyTextBox = CType(ctr, common.Controls.MyTextBox)
                myControl.Text = GetDefaultValue(myControl.Name)
            ElseIf ctr.GetType Is GetType(common.MyNumBox) Then
                Dim myControl As common.MyNumBox = CType(ctr, common.MyNumBox)
                myControl.Value = clsCommon.myCdbl(GetDefaultValue(myControl.Name))
            ElseIf ctr.GetType Is GetType(common.UserControls.txtFinder) Then
                Dim myControl As common.UserControls.txtFinder = CType(ctr, common.UserControls.txtFinder)
                myControl.Value = GetDefaultValue(myControl.Name)
                If clsCommon.myLen(myControl.Value) <= 0 Then
                    myControl.MyLinkLable2.Text = ""
                Else
                    myControl.MyLinkLable2.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Description from TSPL_CUSTOM_FIELD_DETAIL where TSPL_CUSTOM_FIELD_DETAIL.Custom_Field_Code='" + myControl.Name + "' and TSPL_CUSTOM_FIELD_DETAIL.Value='" + myControl.Value + "'"))
                End If
            ElseIf ctr.GetType Is GetType(common.Controls.MyDateTimePicker) Then
                Dim myControl As common.Controls.MyDateTimePicker = CType(ctr, common.Controls.MyDateTimePicker)
                Dim str As String = GetDefaultValue(myControl.Name)
                If clsCommon.myLen(str) > 0 Then
                    myControl.Value = clsCommon.myCDate(str)
                Else
                    myControl.Value = clsCommon.GETSERVERDATE()
                End If
            ElseIf ctr.GetType Is GetType(common.Controls.MyCheckBox) Then
                Dim myControl As common.Controls.MyCheckBox = CType(ctr, common.Controls.MyCheckBox)
                myControl.Checked = False
            End If
        Next
    End Sub

    Private Function GetDefaultValue(ByVal strCustomFieldCode As String) As String
        If ArrMapping IsNot Nothing AndAlso ArrMapping.Count > 0 Then
            For Each obj As clsCustomFieldMapping In ArrMapping
                If clsCommon.CompairString(obj.Custom_Field_Code, strCustomFieldCode) = CompairStringResult.Equal Then
                    Return obj.Default_Value
                End If
            Next
        End If
        Return ""
    End Function
    Public Shared Function getControlInvolvedInCalculation(programCode As String) As List(Of String)
        Dim rValue As New List(Of String)
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select calculationExpression from TSPL_CUSTOM_FIELD_MAPPING where Program_Code='" & programCode & "' and isnull(calculationExpression,'')<>''")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim expr As String = clsCommon.myCstr(dt.Rows(i)("calculationExpression"))
                    If clsCommon.myLen(expr) > 0 AndAlso expr.Contains("#") Then
                        Dim isFieldRead As Boolean = True
                        Dim temp As String = String.Empty
                        Dim temp2 As String = String.Empty
                        For j As Integer = 1 To expr.Length
                            If Microsoft.VisualBasic.Mid(expr, j, 1) = "#" AndAlso isFieldRead Then
                                temp = String.Empty
                                j = j + 1
                                If j <= expr.Length Then
                                    Dim k As Integer = j
                                    While Microsoft.VisualBasic.Mid(expr, k, 1) <> "#"
                                        temp = temp & Microsoft.VisualBasic.Mid(expr, k, 1)
                                        k = k + 1
                                    End While
                                    j = k + 1
                                    If clsCommon.myLen(temp) > 0 Then
                                        rValue.Add(temp)
                                    End If
                                End If
                            End If
                        Next
                    End If
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue


    End Function

    Public Shared Function FindStringInList(arr As List(Of String), value As String) As Boolean
        Dim rValue As Boolean = False
        Try
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For i As Integer = 0 To arr.Count - 1
                    If clsCommon.CompairString(arr.Item(i), value) = CompairStringResult.Equal Then
                        rValue = True
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function
    Public Sub AttachDelegatesForExpNum(txtNumBox As common.MyNumBox, fieldName As String)
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select calculationExpression from TSPL_CUSTOM_FIELD_MAPPING where Program_Code='" & Report_ID & "' and isnull(calculationExpression,'')<>''")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    If clsCommon.myCstr(dt.Rows(i)("calculationExpression")).Contains(fieldName) Then
                        For j As Integer = 0 To Controls.Count - 1

                        Next
                    End If
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class
