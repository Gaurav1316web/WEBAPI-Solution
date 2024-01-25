'--29/06/2013--form Add By- Pradeep Sharma ---------
'------------------------BM00000000726------------------ by shipra'
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class frmMapPayHeadsToSalaStructure
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
    Private isCellValueChangedOpen As Boolean = False
    Private ObjList As List(Of clsMapPayHeadsToSalaStructure)
    Private Obj As clsMapPayHeadsToSalaStructure

    Const colLineNo As String = "LineNo"
    Const colPayHeadCode As String = "PayHeadCode"
    Const colPayHeadName As String = "PayHeadName"
    Const colPayHeadType As String = "PayHeadType"
    Const colPayHeadCategory As String = "PayHeadCategory"
    Const colCalculationBasis As String = "CalculationBasis"
    Const colFormula As String = "Formula"
    Const colRate As String = "Rate"
    Const colVilidFrom As String = "VilidFrom"
    Const colVilidTo As String = "VilidTo"
    Const colDescription As String = "Description"
    Const colHiddenComponent As String = "HiddenComponent"
    Const colLocationCode As String = "Location Code"


#End Region

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Save()
    End Sub

    Public Sub Save()
        Try
            If AllowToSave() Then
                Dim obj As clsMapPayHeadsToSalaStructure = Nothing
                ObjList = New List(Of clsMapPayHeadsToSalaStructure)
                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colPayHeadCode).Value)) > 0 Then
                        obj = New clsMapPayHeadsToSalaStructure()

                        obj.SALARY_STRUCTURE_CODE = txtCode.Value
                        obj.Location_Code = fndLocation.Value
                        obj.LINE_NO = Convert.ToInt16(grow.Cells(colLineNo).Value)
                        obj.PAY_HEAD_CODE = clsCommon.myCstr(grow.Cells(colPayHeadCode).Value)
                        obj.HEAD_TYPE = clsCommon.myCstr(grow.Cells(colPayHeadType).Value)
                        obj.SUB_HEAD_TYPE = clsCommon.myCstr(grow.Cells(colPayHeadCategory).Value)
                        obj.CALC_BASIS = clsCommon.myCstr(grow.Cells(colCalculationBasis).Value)
                        obj.RATE_AMOUNT = clsCommon.myCdbl(grow.Cells(colRate).Value)
                        obj.PAYHEAD_FORMULA = clsCommon.myCstr(grow.Cells(colFormula).Value)
                        obj.DESCRIPTION = clsCommon.myCstr(grow.Cells(colDescription).Value)
                        obj.IsHiddenComponent = clsCommon.myCBool(grow.Cells(colHiddenComponent).Value)
                        If clsCommon.myLen(grow.Cells(colVilidFrom).Value) > 0 Then
                            obj.VALID_FROM = clsCommon.GetPrintDate(grow.Cells(colVilidFrom).Value, "dd/MMM/yyyy")
                        Else
                            obj.VALID_FROM = Nothing
                        End If
                        If clsCommon.myLen(grow.Cells(colVilidTo).Value) > 0 Then
                            obj.VALID_TO = clsCommon.GetPrintDate(grow.Cells(colVilidTo).Value, "dd/MMM/yyyy")
                        Else
                            obj.VALID_TO = Nothing
                        End If
                        ObjList.Add(obj)
                    End If
                Next
                If (obj.SaveData(txtCode.Value, ObjList)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                End If
                LoadData(obj.SALARY_STRUCTURE_CODE, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            txtCode.MyReadOnly = True
            btnsave.Enabled = True
            btndelete.Enabled = True
            Obj = clsMapPayHeadsToSalaStructure.GetData(strCode, NavTyep)
            If (Obj IsNot Nothing AndAlso clsCommon.myLen(Obj.SALARY_STRUCTURE_CODE) > 0) Then
                funReset()
                isNewEntry = False
                btnsave.Text = "Update"
                Dim ii As Int16 = 0
                LoadGridColumns()
                txtCode.Value = Obj.SALARY_STRUCTURE_CODE
                If clsCommon.myLen(objCommonVar.CurrentUserCode) > 0 Then
                    fndLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
                    lblLocation.Text = clsLocation.GetName(fndLocation.Value, Nothing)
                Else
                    fndLocation.Value = ""
                    lblLocation.Text = ""
                End If
                txtName.Text = Obj.SALARY_STRUCTURE_NAME
                If (clsMapPayHeadsToSalaStructure.ObjList IsNot Nothing AndAlso clsMapPayHeadsToSalaStructure.ObjList.Count > 0) Then
                    For Each obj As clsMapPayHeadsToSalaStructure In clsMapPayHeadsToSalaStructure.ObjList
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = obj.LINE_NO
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPayHeadCode).Value = obj.PAY_HEAD_CODE
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPayHeadName).Value = obj.PAY_HEAD_NAME
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPayHeadType).Value = obj.HEAD_TYPE
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPayHeadCategory).Value = obj.SUB_HEAD_TYPE
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCalculationBasis).Value = obj.CALC_BASIS
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFormula).Value = obj.PAYHEAD_FORMULA
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = obj.RATE_AMOUNT
                        If obj.VALID_FROM IsNot Nothing Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colVilidFrom).Value = obj.VALID_FROM
                        End If
                        If obj.VALID_TO IsNot Nothing Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colVilidTo).Value = obj.VALID_TO
                        End If
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDescription).Value = obj.DESCRIPTION
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHiddenComponent).Value = obj.IsHiddenComponent
                        If clsCommon.myLen(obj.Location_Code) > 0 Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationCode).Value = obj.Location_Code
                        End If
                    Next
                Else
                    gv1.Rows.AddNew()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Function AllowToSave() As Boolean
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            myMessages.blankValue("Code")
            txtCode.Focus()
            Return False
        End If
        Dim ii As Int16 = 0
        For Each grow As GridViewRowInfo In gv1.Rows
            If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colPayHeadCode).Value)) > 0 Then
                ii += 1
                If clsCommon.myCstr(grow.Cells(colPayHeadType).Value) = "F" And clsCommon.myLen(clsCommon.myCstr(grow.Cells(colFormula).Value)) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Formula can not be blank for Pay Head " & clsCommon.myCstr(grow.Cells(colPayHeadCode).Value) & "")
                    Return False
                End If
                If clsCommon.myLen(grow.Cells(colVilidFrom).Value) > 0 And clsCommon.myLen(grow.Cells(colVilidTo).Value) > 0 Then
                    If clsCommon.myCDate(grow.Cells(colVilidFrom).Value, "dd/MMM/yyyy") >= clsCommon.myCDate(grow.Cells(colVilidTo).Value, "dd/MMM/yyyy") Then
                        clsCommon.MyMessageBoxShow(Me, "From Date :" + clsCommon.GetPrintDate(grow.Cells(colVilidFrom).Value, "dd/MMM/yyyy") + " can NOT be Grater then To Date : " + clsCommon.GetPrintDate(grow.Cells(colVilidTo).Value, "dd/MMM/yyyy") + " in Row No. " + ii.ToString() + " ")
                        Return False
                    End If
                End If

                '' check for duplicate line no(seq no)
                Dim Line_No As Integer = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                If Line_No <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "enter seq no of pay head " & grow.Cells(colLineNo).Value & "")
                    Return False
                End If
                Dim INext As Integer = grow.Index + 1
                For intNext As Integer = INext To gv1.Rows.Count - 1
                    If clsCommon.myCdbl(gv1.Rows(intNext).Cells(colLineNo).Value) = Line_No Then
                        clsCommon.MyMessageBoxShow(Me, "Duplicate sequence no of pay head " & gv1.Rows(intNext).Cells(colPayHeadCode).Value & "")
                        Return False
                    End If
                Next
            End If

        Next
        Dim qry As String = "select top 1 EMP_CODE from TSPL_EMPLOYEE_SALARY where SALARY_STRUCTURE_CODE='" + txtCode.Value + "'  "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            If clsCommon.myLen(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.UpdateMapPayHeadsToSalaStructurePassword, clsFixedParameterCode.UpdateMapPayHeadsToSalaStructurePassword, Nothing))) > 0 Then
                Dim pwd As New FrmPWD(Nothing)
                pwd.strCode = clsFixedParameterCode.UpdateMapPayHeadsToSalaStructurePassword
                pwd.strType = clsFixedParameterType.UpdateMapPayHeadsToSalaStructurePassword
                pwd.ShowDialog()
                If pwd.isPasswordCorrect Then
                    Return True
                Else
                    Return False
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "Salary Structure is in use.", Me.Text)
                Return False
            End If
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
        Dim qry As String = "select top 1 EMP_CODE from TSPL_EMPLOYEE_SALARY where SALARY_STRUCTURE_CODE='" + txtCode.Value + "'  "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            clsCommon.MyMessageBoxShow(Me, "Salary Structure is in use.", Me.Text)
            Exit Sub
        End If
        funDelete()
    End Sub

    Sub funDelete()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If clsCancelLog.CheckForReasonOnDelete() Then
                    '' REASON FOR DELETE 
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Delete"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If
                End If
                If (clsMapPayHeadsToSalaStructure.DeleteData(txtCode.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtCode.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    Private Sub txtCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub frmMapPayHeadsToSalaStructure_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadGridColumns()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ' ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")
        If clsCommon.myLen(objCommonVar.CurrentUserCode) > 0 Then
            fndLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
            lblLocation.Text = clsLocation.GetName(fndLocation.Value, Nothing)
        Else
            fndLocation.Value = ""
            lblLocation.Text = ""
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmMapPayHeadsToSalaStructure)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        funReset()
        gv1.Rows.AddNew()
    End Sub

    Sub funReset()
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        txtName.Text = ""
        LoadGridColumns()
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = True
        txtStructureCodeCopy.Value = Nothing
        If clsCommon.myLen(objCommonVar.CurrentUserCode) > 0 Then
            fndLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
            lblLocation.Text = clsLocation.GetName(fndLocation.Value, Nothing)
        Else
            fndLocation.Value = ""
            lblLocation.Text = ""
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        funClose()

    End Sub

    Sub funClose()
        Me.Close()
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Dim whrcls As String = Nothing
        Dim LocCode As String = Nothing
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            LocCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
            If clsCommon.myLen(LocCode) > 0 Then
                whrcls = " LOCATION_CODE='" + LocCode + "'"
            End If
        End If
        Dim str As String = "select count(*) from TSPL_SALARY_STRUCTURE where SALARY_STRUCTURE_CODE ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = " select SALARY_STRUCTURE_CODE as Code, SALARY_STRUCTURE_NAME as Name, SAL_PRINT_NAME as 'Print Name',Location_Code As 'Location Code' from TSPL_SALARY_STRUCTURE "
            txtCode.Value = clsCommon.ShowSelectForm("SALARY_STRUCTURE", qry, "Code", whrcls, txtCode.Value, "SALARY_STRUCTURE_CODE", isButtonClicked)
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

    Private Sub frmMapPayHeadsToSalaStructure_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnnew.Enabled Then
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
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        gv1.ReadOnly = False

        Dim lineNo As GridViewDecimalColumn
        lineNo = New GridViewDecimalColumn()
        lineNo.FormatString = ""
        lineNo.HeaderText = "Line No"
        lineNo.Name = colLineNo
        lineNo.Width = 50
        lineNo.ReadOnly = False
        lineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(lineNo)

        Dim PayHeadCode As GridViewTextBoxColumn
        PayHeadCode = New GridViewTextBoxColumn()
        PayHeadCode.FormatString = ""
        PayHeadCode.HeaderText = "Pay Head Code"
        PayHeadCode.Name = colPayHeadCode
        PayHeadCode.Width = 80
        PayHeadCode.ReadOnly = False
        PayHeadCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(PayHeadCode)

        Dim PayHeadName As GridViewTextBoxColumn
        PayHeadName = New GridViewTextBoxColumn()
        PayHeadName.FormatString = ""
        PayHeadName.HeaderText = "Pay Head Name"
        PayHeadName.Name = colPayHeadName
        PayHeadName.Width = 100
        PayHeadName.ReadOnly = True
        PayHeadName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(PayHeadName)

        Dim PayHeadType As GridViewTextBoxColumn
        PayHeadType = New GridViewTextBoxColumn()
        PayHeadType.FormatString = ""
        PayHeadType.HeaderText = "Pay Head Type"
        PayHeadType.Name = colPayHeadType
        PayHeadType.Width = 80
        PayHeadType.ReadOnly = True
        PayHeadType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(PayHeadType)

        Dim PayHeadCategory As GridViewTextBoxColumn
        PayHeadCategory = New GridViewTextBoxColumn()
        PayHeadCategory.FormatString = ""
        PayHeadCategory.HeaderText = "Pay Head Category"
        PayHeadCategory.Name = colPayHeadCategory
        PayHeadCategory.Width = 100
        PayHeadCategory.ReadOnly = True
        PayHeadCategory.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(PayHeadCategory)

        Dim CalculationBasis As GridViewTextBoxColumn
        CalculationBasis = New GridViewTextBoxColumn()
        CalculationBasis.FormatString = ""
        CalculationBasis.HeaderText = "Calculation Basis"
        CalculationBasis.Name = colCalculationBasis
        CalculationBasis.Width = 100
        CalculationBasis.ReadOnly = True
        CalculationBasis.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(CalculationBasis)

        Dim Formula As GridViewTextBoxColumn
        Formula = New GridViewTextBoxColumn()
        Formula.FormatString = ""
        Formula.HeaderText = "Formula"
        Formula.Name = colFormula
        Formula.Width = 100
        Formula.ReadOnly = True
        Formula.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(Formula)

        Dim Rate As GridViewDecimalColumn
        Rate = New GridViewDecimalColumn()
        Rate.FormatString = ""
        Rate.HeaderText = "Rate"
        Rate.Name = colRate
        Rate.Width = 80
        Rate.FormatString = "{0:n3}"
        Rate.DecimalPlaces = 3
        Rate.ReadOnly = False
        Rate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(Rate)

        Dim VilidFrom As GridViewDateTimeColumn
        VilidFrom = New GridViewDateTimeColumn()
        VilidFrom.CustomFormat = "dd/MM/yyyy"
        VilidFrom.FormatString = "{0:d}"
        VilidFrom.HeaderText = "Valid From"
        VilidFrom.Name = colVilidFrom
        VilidFrom.Width = 80
        VilidFrom.ReadOnly = False
        VilidFrom.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(VilidFrom)

        Dim VilidTo As GridViewDateTimeColumn
        VilidTo = New GridViewDateTimeColumn()
        VilidTo.CustomFormat = "dd/MM/yyyy"
        VilidTo.FormatString = "{0:d}"
        VilidTo.HeaderText = "Valid To"
        VilidTo.Name = colVilidTo
        VilidTo.Width = 80
        VilidTo.ReadOnly = False
        VilidTo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(VilidTo)

        Dim Description As GridViewTextBoxColumn
        Description = New GridViewTextBoxColumn()
        Description.FormatString = ""
        Description.HeaderText = "Description"
        Description.Name = colDescription
        Description.Width = 150
        Description.ReadOnly = False
        Description.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(Description)

        Dim IsHiddenComponent As GridViewCheckBoxColumn
        IsHiddenComponent = New GridViewCheckBoxColumn()
        IsHiddenComponent.HeaderText = "Is Hidden Component"
        IsHiddenComponent.Name = colHiddenComponent
        IsHiddenComponent.Width = 50
        IsHiddenComponent.ReadOnly = True
        gv1.Columns.Add(IsHiddenComponent)

        Dim LocationCode As GridViewTextBoxColumn
        LocationCode = New GridViewTextBoxColumn()
        LocationCode.HeaderText = "Location Code"
        LocationCode.Name = colLocationCode
        LocationCode.Width = 50
        LocationCode.ReadOnly = True
        gv1.Columns.Add(LocationCode)

    End Sub


    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.Rows.Count > 0 Then 'AndAlso e.CurrentColumn Is gv1.Columns(colDescription) Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            'gv1.CurrentRow.Cells(0).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(1).Value) > 0 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
        'If gv1.Rows.Count > 0 AndAlso e.CurrentColumn Is gv1.Columns(colFormula) Then
        '    Try
        '        Dim ListOp As New List(Of String)
        '        For kk As Int16 = 0 To gv1.CurrentRow.Index - 1
        '            ListOp.Add(clsCommon.myCstr(gv1.Rows(kk).Cells(colPayHeadCode).Value))
        '        Next

        '        Dim FFS As New frmFormulaSelection
        '        FFS.ListOperand = ListOp
        '        FFS.OldFormula = gv1.CurrentRow.Cells(colFormula).Value
        '        FFS.txtFormula.Text = gv1.CurrentRow.Cells(colFormula).Value
        '        FFS.ShowDialog()
        '        gv1.CurrentRow.Cells(colFormula).Value = FFS.txtFormula.Text
        '    Catch ex As Exception
        '    End Try
        'End If
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        If Not isCellValueChangedOpen Then
            isCellValueChangedOpen = True
            If e.Column Is gv1.Columns(colPayHeadCode) Then
                Dim obj As clsPayHeadDefinitions = clsPayHeadDefinitions.FinderForPayHead(clsCommon.myCstr(gv1.CurrentRow.Cells(colPayHeadCode).Value), False)
                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.PAY_HEAD_CODE) > 0 Then
                    gv1.CurrentRow.Cells(colPayHeadCode).Value = obj.PAY_HEAD_CODE
                    gv1.CurrentRow.Cells(colPayHeadName).Value = obj.PAY_HEAD_NAME
                    gv1.CurrentRow.Cells(colPayHeadType).Value = obj.HEAD_TYPE
                    gv1.CurrentRow.Cells(colPayHeadCategory).Value = obj.SUB_HEAD_TYPE
                    gv1.CurrentRow.Cells(colCalculationBasis).Value = obj.CALC_BASIS
                    gv1.CurrentRow.Cells(colHiddenComponent).Value = obj.IsHiddenComponent
                End If
            End If
            isCellValueChangedOpen = False
        End If
    End Sub

    Private Sub gv1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
        If gv1.Rows.Count > 0 AndAlso gv1.CurrentColumn.Name Is gv1.Columns(colFormula).Name Then
            Try
                Dim ListOp As New List(Of String)
                For kk As Int16 = 0 To gv1.CurrentRow.Index - 1
                    ListOp.Add(clsCommon.myCstr(gv1.Rows(kk).Cells(colPayHeadCode).Value))
                Next

                Dim FFS As New frmFormulaSelection
                FFS.ListOperand = ListOp
                FFS.OldFormula = gv1.CurrentRow.Cells(colFormula).Value
                FFS.txtFormula.Text = gv1.CurrentRow.Cells(colFormula).Value
                FFS.ShowDialog()
                gv1.CurrentRow.Cells(colFormula).Value = FFS.txtFormula.Text
            Catch ex As Exception
            End Try
        End If
    End Sub
    ' Ticket No : BHA/30/01/19-000795 By Prabhakar for structue copy finder and ImportExport 
    Private Sub txtStructureCodeCopy__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtStructureCodeCopy._MYValidating
        Dim Code As String = Nothing
        Dim Name As String = Nothing
        If clsCommon.myLen(txtCode.Value) > 0 Then

            Dim isValidStructureCode As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_SALARY_STRUCTURE where SALARY_STRUCTURE_CODE = '" + txtCode.Value + "' "))
            If isValidStructureCode <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Invalid Salary Structure Code.", Me.Text)
                Exit Sub
            End If
            Dim isNewCode As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_SALSTRUCT_PAYHEADS where SALARY_STRUCTURE_CODE = '" + txtCode.Value + "' "))
            Code = txtCode.Value
            Name = txtName.Text
            If isNewCode > 0 Then
                Dim qry As String = " select distinct TSPL_SALARY_STRUCTURE.SALARY_STRUCTURE_CODE as Code, TSPL_SALARY_STRUCTURE.SALARY_STRUCTURE_NAME as Name, TSPL_SALARY_STRUCTURE.SAL_PRINT_NAME as 'Print Name' from TSPL_SALARY_STRUCTURE inner join TSPL_SALSTRUCT_PAYHEADS on TSPL_SALSTRUCT_PAYHEADS.SALARY_STRUCTURE_CODE = TSPL_SALARY_STRUCTURE.SALARY_STRUCTURE_CODE "
                txtStructureCodeCopy.Value = clsCommon.ShowSelectForm("SALARY_STRUCTURE_COPY", qry, "Code", "", txtStructureCodeCopy.Value, "TSPL_SALARY_STRUCTURE.SALARY_STRUCTURE_CODE", isButtonClicked)
                If txtStructureCodeCopy.Value <> "" Then
                    LoadData(txtStructureCodeCopy.Value, NavigatorType.Current)
                    txtStructureCodeCopy.Value = txtCode.Value
                    txtCode.Value = Code
                    txtName.Text = Name
                    btnsave.Text = "Save"
                End If
            End If
        End If
    End Sub

    Private Sub rmExport_Click(sender As Object, e As EventArgs) Handles rmExport.Click
        Dim strDetail As String
        strDetail = "  select TSPL_SALSTRUCT_PAYHEADS.SALARY_STRUCTURE_CODE , TSPL_SALSTRUCT_PAYHEADS.LINE_NO , TSPL_SALSTRUCT_PAYHEADS.PAY_HEAD_CODE ,convert (varchar,TSPL_SALSTRUCT_PAYHEADS.VALID_FROM,103) as VALID_FROM , " &
                    "  Convert (varchar,TSPL_SALSTRUCT_PAYHEADS.VALID_TO,103) as VALID_TO ,TSPL_SALSTRUCT_PAYHEADS.HEAD_TYPE ,TSPL_SALSTRUCT_PAYHEADS.SUB_HEAD_TYPE ,TSPL_SALSTRUCT_PAYHEADS.CALC_BASIS ,TSPL_SALSTRUCT_PAYHEADS.PAYHEAD_FORMULA ,TSPL_SALSTRUCT_PAYHEADS.RATE_AMOUNT  from TSPL_SALSTRUCT_PAYHEADS " &
                    "  " &
                    " "
        transportSql.ExporttoExcel(strDetail, "", "SALARY_STRUCTURE_CODE,LINE_NO", Me)
    End Sub

    Private Sub rmImport_Click(sender As Object, e As EventArgs) Handles rmImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim obj As clsMapPayHeadsToSalaStructure = Nothing
        Dim currentdate As Date = Date.Today
        Dim coll As Hashtable
        Dim trans As SqlTransaction = Nothing
        If transportSql.importExcel(gv, "SALARY_STRUCTURE_CODE", "PAY_HEAD_CODE", "VALID_FROM", "VALID_TO", "RATE_AMOUNT", "PAYHEAD_FORMULA") Then
            Dim linno As Integer = 0
            Try
                trans = clsDBFuncationality.GetTransactin()
                'Dim arr As New List(Of clsMapPayHeadsToSalaStructure)
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    coll = New Hashtable()
                    obj = New clsMapPayHeadsToSalaStructure
                    linno += 1
                    Dim strStructrueCode As String = clsCommon.myCstr(grow.Cells("SALARY_STRUCTURE_CODE").Value)
                    Dim isValidSalaryStructureCode As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select count (*) from TSPL_SALARY_STRUCTURE where SALARY_STRUCTURE_CODE = '" + strStructrueCode + "' ", trans))
                    If isValidSalaryStructureCode = False Then
                        Throw New Exception("Invalid Salary Structure Code At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.SALARY_STRUCTURE_CODE = strStructrueCode

                    Dim qry As String = "select top 1 EMP_CODE from TSPL_EMPLOYEE_SALARY where SALARY_STRUCTURE_CODE='" + strStructrueCode + "'  "
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        Throw New Exception("Salary Structure is in use.")
                    End If

                    Dim strPayHeadCode As String = clsCommon.myCstr(grow.Cells("PAY_HEAD_CODE").Value)
                    dt = clsDBFuncationality.GetDataTable("select PAY_HEAD_CODE,PAY_HEAD_NAME,HEAD_TYPE,SUB_HEAD_TYPE,CALC_BASIS,IsHiddenComponent  from TSPL_PAYHEAD_MASTER where PAY_HEAD_CODE = '" + strPayHeadCode + "'", trans)
                    If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                        obj.PAY_HEAD_CODE = dt.Rows(0)("PAY_HEAD_CODE")
                        obj.PAY_HEAD_NAME = dt.Rows(0)("PAY_HEAD_NAME")
                        obj.HEAD_TYPE = dt.Rows(0)("HEAD_TYPE")
                        obj.SUB_HEAD_TYPE = dt.Rows(0)("SUB_HEAD_TYPE")
                        obj.CALC_BASIS = dt.Rows(0)("CALC_BASIS")
                        obj.IsHiddenComponent = dt.Rows(0)("IsHiddenComponent")
                    Else
                        Throw New Exception("Invalid Pay Head Code At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    Dim strValidFrom As String = clsCommon.myCstr(grow.Cells("VALID_FROM").Value)
                    If (String.IsNullOrEmpty(strValidFrom)) Then
                        Throw New Exception("Invalid [VALID FROM] cannot be blank At Line No. " + clsCommon.myCstr(linno) + ".")
                    Else
                        Dim isValidTime As Boolean = IsDate(strValidFrom)
                        If isValidTime = False Then
                            Throw New Exception("Invalid [VALID FROM] Date At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    obj.VALID_FROM = clsCommon.GetPrintDate(strValidFrom, "dd/MMM/yyyy")

                    Dim strValidTo As String = clsCommon.myCstr(grow.Cells("VALID_TO").Value)
                    If (String.IsNullOrEmpty(strValidTo)) Then
                        Throw New Exception("Invalid [VALID TO] cannot be blank At Line No. " + clsCommon.myCstr(linno) + ".")
                    Else
                        Dim isValidTime As Boolean = IsDate(strValidFrom)
                        If isValidTime = False Then
                            Throw New Exception("Invalid [VALID TO] Date At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    obj.VALID_TO = clsCommon.GetPrintDate(strValidTo, "dd/MMM/yyyy")

                    If clsCommon.myCDate(obj.VALID_FROM, "dd/MMM/yyyy") >= clsCommon.myCDate(obj.VALID_TO, "dd/MMM/yyyy") Then
                        Throw New Exception("From Date :" + clsCommon.GetPrintDate(obj.VALID_FROM, "dd/MMM/yyyy") + " can NOT be Grater then To Date : " + clsCommon.GetPrintDate(obj.VALID_TO, "dd/MMM/yyyy") + "   At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If

                    Dim strRateAmount As String = clsCommon.myCstr(grow.Cells("RATE_AMOUNT").Value)
                    If String.IsNullOrEmpty(strRateAmount) = True Then
                        Throw New Exception("Rate Amount cannot be blank At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    If IsNumeric(strRateAmount) = False Then
                        Throw New Exception("Rate Amount should be Numeric At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.RATE_AMOUNT = clsCommon.myCdbl(strRateAmount)
                    '---------------------------
                    qry = "SELECT Count(*) FROM TSPL_SALSTRUCT_PAYHEADS where SALARY_STRUCTURE_CODE = '" & obj.SALARY_STRUCTURE_CODE & "' and PAY_HEAD_CODE = '" & obj.PAY_HEAD_CODE & "'"
                    Dim checkLineNo As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                    If checkLineNo = 0 Then
                        obj.LINE_NO = clsDBFuncationality.getSingleValue("select Count (*) + 1 from TSPL_SALSTRUCT_PAYHEADS where SALARY_STRUCTURE_CODE = '" + obj.SALARY_STRUCTURE_CODE + "'", trans)
                    Else
                        obj.LINE_NO = clsDBFuncationality.getSingleValue("select LINE_NO  from TSPL_SALSTRUCT_PAYHEADS where SALARY_STRUCTURE_CODE = '" + obj.SALARY_STRUCTURE_CODE + "' and PAY_HEAD_CODE = '" + obj.PAY_HEAD_CODE + "'", trans)
                    End If
                    '---------------------------
                    '*******************************Check Formula ***********************************
                    Dim strFormula As String = clsCommon.myCstr(grow.Cells("PAYHEAD_FORMULA").Value)
                    If clsCommon.myCstr(obj.HEAD_TYPE) = "F" And clsCommon.myLen(strFormula) <= 0 Then
                        Throw New Exception("Formula can not be blank At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If

                    If clsCommon.myLen(strFormula) > 0 Then
                        Try
                            Dim ChkFormula As String = strFormula

                            Dim qry11 As String = "   select  PAY_HEAD_CODE   from TSPL_SALSTRUCT_PAYHEADS where SALARY_STRUCTURE_CODE = '" + obj.SALARY_STRUCTURE_CODE + "' and Line_NO < '" + clsCommon.myCstr(obj.LINE_NO) + "' order by len (PAY_HEAD_CODE) desc"
                            Dim dtFormula As DataTable = clsDBFuncationality.GetDataTable(qry11, trans)
                            For Each row As DataRow In dtFormula.Rows
                                Dim item As String = clsCommon.myCstr(row.Item("PAY_HEAD_CODE"))
                                ChkFormula = ChkFormula.Replace(item, "2")
                                ChkFormula = ChkFormula.Replace("[", "")
                                ChkFormula = ChkFormula.Replace("]", "")
                            Next
                            Dim qryyy As String = " select  " + clsCommon.myCstr(ChkFormula) + " "

                            Dim dblxyz = clsDBFuncationality.getSingleValue(qryyy, trans)
                        Catch ex As Exception
                            Throw New Exception("Not a Correct Formula. At Line No. " + clsCommon.myCstr(linno) + ".")
                        End Try
                    End If
                    '********************************************************************************

                    obj.PAYHEAD_FORMULA = clsCommon.myCstr(grow.Cells("PAYHEAD_FORMULA").Value)
                    '==============================================================================================================================================
                    clsCommon.AddColumnsForChange(coll, "SALARY_STRUCTURE_CODE", obj.SALARY_STRUCTURE_CODE)
                    clsCommon.AddColumnsForChange(coll, "LINE_NO", obj.LINE_NO)
                    clsCommon.AddColumnsForChange(coll, "PAY_HEAD_CODE", obj.PAY_HEAD_CODE)
                    clsCommon.AddColumnsForChange(coll, "HEAD_TYPE", obj.HEAD_TYPE)
                    clsCommon.AddColumnsForChange(coll, "SUB_HEAD_TYPE", obj.SUB_HEAD_TYPE)
                    clsCommon.AddColumnsForChange(coll, "CALC_BASIS", obj.CALC_BASIS)
                    clsCommon.AddColumnsForChange(coll, "PAYHEAD_FORMULA", obj.PAYHEAD_FORMULA)
                    clsCommon.AddColumnsForChange(coll, "RATE_AMOUNT", obj.RATE_AMOUNT)
                    clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.DESCRIPTION)
                    If obj.VALID_FROM IsNot Nothing Then
                        clsCommon.AddColumnsForChange(coll, "VALID_FROM", clsCommon.GetPrintDate(obj.VALID_FROM, "dd/MMM/yyyy"))
                    Else
                        clsCommon.AddColumnsForChange(coll, "VALID_FROM", Nothing)
                    End If
                    If obj.VALID_TO IsNot Nothing Then
                        clsCommon.AddColumnsForChange(coll, "VALID_TO", clsCommon.GetPrintDate(obj.VALID_TO, "dd/MMM/yyyy"))
                    Else
                        clsCommon.AddColumnsForChange(coll, "VALID_TO", Nothing)
                    End If
                    clsCommon.AddColumnsForChange(coll, "IsHiddenComponent", obj.IsHiddenComponent)

                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

                    qry = "SELECT Count(*) FROM TSPL_SALSTRUCT_PAYHEADS where SALARY_STRUCTURE_CODE = '" & obj.SALARY_STRUCTURE_CODE & "' and PAY_HEAD_CODE = '" & obj.PAY_HEAD_CODE & "'"
                    Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                    If check = 0 Then
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SALSTRUCT_PAYHEADS", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SALSTRUCT_PAYHEADS", OMInsertOrUpdate.Update, "SALARY_STRUCTURE_CODE='" + obj.SALARY_STRUCTURE_CODE + "' and PAY_HEAD_CODE = '" + obj.PAY_HEAD_CODE + "'", trans)
                    End If
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub BtnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select Salary Structure Code", Me.Text)
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowHistoryData(txtCode.Value, "SALARY_STRUCTURE_CODE", "TSPL_SALSTRUCT_PAYHEADS")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub fndLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLocation._MYValidating
        Try
            Dim whrcls As String = Nothing
            Dim LocCode As String = Nothing
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                LocCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
                If clsCommon.myLen(LocCode) > 0 Then
                    whrcls = " LOCATION_CODE='" + LocCode + "'"
                End If
            End If
            Dim Qry As String = "select Location_Code As [Location Code],Location_Desc As [Description] from TSPL_LOCATION_MASTER "
            fndLocation.Value = clsLocation.getFinder(whrcls, Me.fndLocation.Value, isButtonClicked)
            ''fndLocation.Value = clsCommon.ShowSelectForm("SalaryLocation", Qry, "Location_Code", whrcls, "", "Location_Code", isButtonClicked)
            lblLocation.Text = clsLocation.GetName(fndLocation.Value, Nothing)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
