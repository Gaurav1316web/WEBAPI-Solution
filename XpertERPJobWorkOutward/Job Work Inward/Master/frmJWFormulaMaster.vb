Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class frmJWFormulaMaster
    Inherits FrmMainTranScreen
    ' Ticket No : BHA/28/08/18-000493 By Prabhakar Create New screen 
#Region "Variables"
    Private isNewEntry As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
    'Dim blnOperator As Boolean = False
    Const colCode As String = "Code"
    Const colDesc As String = "Description"
    Const colValue As String = "Value"
    Const colType As String = "Type"
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
    'Sub LoadType()
    '    Dim dt As DataTable = New DataTable()
    '    dt.Columns.Add("Code", GetType(String))
    '    dt.Columns.Add("Name", GetType(String))
    '    Dim dr As DataRow = dt.NewRow()

    '    dr("Code") = "Formula"
    '    dr("Name") = "Formula"
    '    dt.Rows.Add(dr)

    '    dr = dt.NewRow()
    '    dr("Code") = "Rate"
    '    dr("Name") = "Rate"
    '    dt.Rows.Add(dr)

    '    cboType.DataSource = dt
    '    cboType.ValueMember = "Code"
    '    cboType.DisplayMember = "Name"
    '    'isInsideLoadData = False
    'End Sub

    Private Sub FrmDepreciationField_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadDepreciationValue()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        'LoadDepreciationFields()
        AddNew()
        SetLength()
    End Sub

    Sub LoadDepreciationValue()
        'Dim dt As New DataTable
        'dt.Columns.Add("SNo", GetType(Integer))
        'dt.Columns.Add("Code", GetType(String))
        'dt.Columns.Add("Name", GetType(String))

        'Dim dr As DataRow = dt.NewRow()
        'dr("SNo") = 1
        'dr("Code") = clsDepreciationParameter.BNV
        'dr("Name") = clsDepreciationParameter.BNVName
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("SNo") = 2
        'dr("Code") = clsDepreciationParameter.BEY
        'dr("Name") = clsDepreciationParameter.BEYName
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("SNo") = 3
        'dr("Code") = clsDepreciationParameter.BSV
        'dr("Name") = clsDepreciationParameter.BSVName
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("SNo") = 4
        'dr("Code") = clsDepreciationParameter.BDT
        'dr("Name") = clsDepreciationParameter.BDTName
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("SNo") = 5
        'dr("Code") = clsDepreciationParameter.BRT
        'dr("Name") = clsDepreciationParameter.BRTName
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("SNo") = 6
        'dr("Code") = clsDepreciationParameter.BSR
        'dr("Name") = clsDepreciationParameter.BSRName
        'dt.Rows.Add(dr)
        Dim qry As String = " select ROW_NUMBER() OVER (Order by code) AS SNo, TSPL_JW_Parameter_MASTER.Code as Code , TSPL_JW_Parameter_MASTER.Description as Name,case when TSPL_JW_Parameter_MASTER .Type = 'N' then 'NONE' when TSPL_JW_Parameter_MASTER .Type = 'F' then 'FAT' else 'SNF' end as Type, 0.0 as  Value  from TSPL_JW_Parameter_MASTER "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            gv1.DataSource = dt
            gv1.Columns("SNo").ReadOnly = True
            gv1.Columns("SNo").Width = "22"

            gv1.Columns("Code").ReadOnly = True
            gv1.Columns("Code").Width = "120"

            gv1.Columns("Name").ReadOnly = True
            gv1.Columns("Name").Width = "140"

            gv1.Columns("Type").ReadOnly = True
            gv1.Columns("Type").Width = "90"

            gv1.Columns("Value").ReadOnly = False
            gv1.Columns("Value").Width = "90"

        End If
    End Sub

    Sub SetLength()
        txtCode.MyMaxLength = 30
        txtDesc.MaxLength = 100
        txtFormula.MaxLength = 500
    End Sub

    Sub LoadDepreciationFields()
        Dim qry As String = "select Code as " + colCode + ",Description as " + colDesc + " from TSPL_JW_Parameter_MASTER"
        gv1.DataSource = clsDBFuncationality.GetDataTable(qry)
        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
    End Sub

    Sub AddNew()
        txtCode.MyReadOnly = False
        BlankAllControls()
        isNewEntry = True
        LoadDepreciationValue()
        btnSave.Text = "Save"
        ' LoadType()
    End Sub

    Sub BlankAllControls()
        txtCode.Value = ""
        txtDesc.Text = ""
        txtFormula.Text = ""
        txtStructurer.Value = ""
        lblStructurer.Text = ""
        LoadDepreciationValue()
    End Sub

    Function AllowToSave() As Boolean
        'If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) Then
        '    If clsCommon.myLen(txtCode.Value) <= 0 Then
        '        common.clsCommon.MyMessageBoxShow("Please Enter Code")
        '        txtCode.Focus()
        '        Return False
        '    End If
        'End If
        If clsCommon.myLen(txtDesc.Text) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please Enter Description", Me.Text)
            txtDesc.Focus()
            Return False
        End If
        If clsCommon.myLen(txtFormula.Text) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please Enter Forumula for calculation", Me.Text)
            txtFormula.Focus()
            Return False
        End If
        If clsCommon.myLen(txtStructurer.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select Structurer Code", Me.Text)
            txtStructurer.Focus()
            Return False
        End If
        Try

            Dim strFormula As String = txtFormula.Text
            '==================================================================
            For Each grow As GridViewRowInfo In gv1.Rows
                strFormula = strFormula.Replace(clsCommon.myCstr(grow.Cells(colCode).Value), "2")
            Next
            '==================================================================

            'strFormula = strFormula.Replace(clsDepreciationParameter.BNV, "2")
            'strFormula = strFormula.Replace(clsDepreciationParameter.BEY, "2")
            'strFormula = strFormula.Replace(clsDepreciationParameter.BSV, "2")
            'strFormula = strFormula.Replace(clsDepreciationParameter.BDT, "2")
            'strFormula = strFormula.Replace(clsDepreciationParameter.BRT, "2")
            'strFormula = strFormula.Replace(clsDepreciationParameter.BSR, "2")
            Dim dblxyz = clsDBFuncationality.getSingleValue("select  " + strFormula + "")
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, "Not a Correct Formula", Me.Text)
            txtFormula.Focus()
            Return False
        End Try
        Return True
    End Function

    Private Sub SaveData()
        Try
            If (AllowToSave()) Then

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmJWFormulaMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
                Dim arr As New List(Of clsJWFormula)

                Dim obj As New clsJWFormula()
                obj.Code = txtCode.Value
                obj.Description = txtDesc.Text
                obj.Formula = txtFormula.Text
                obj.Structure_Code = txtStructurer.Value
                arr.Add(obj)

                Dim Arr2 As New List(Of clsJWFormulaDetails)
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsJWFormulaDetails()
                    objTr.Code = txtCode.Value
                    objTr.Parameter_Code = clsCommon.myCstr(grow.Cells(colCode).Value)
                    objTr.Value = clsCommon.myCstr(grow.Cells(colValue).Value)
                    Arr2.Add(objTr)
                Next

                If obj.SaveData(arr, Arr2, isNewEntry) Then
                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(obj.Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            isNewEntry = False
            btnSave.Text = "Update"
            BlankAllControls()
            txtCode.MyReadOnly = True
            Dim obj As New clsJWFormula()
            obj = clsJWFormula.GetData(strCode, NavTyep, Nothing) ' clsJWFormula.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
                txtCode.Value = obj.Code
                txtDesc.Text = obj.Description
                txtFormula.Text = obj.Formula
                txtStructurer.Value = obj.Structure_Code
                lblStructurer.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Structure_Descq from TSPL_STRUCTURE_MASTER where Structure_Code='" + txtStructurer.Value + "'"))

                '====================================
                Dim count As Boolean = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_JW_FORMULA_DETAILS where Code = '" + obj.Code + "'"))
                If count = True Then
                    For Each objtr As clsJWFormulaDetails In obj.Arr
                        For ii As Integer = 0 To gv1.Rows.Count - 1
                            If clsCommon.myCstr(gv1.Rows(ii).Cells(colCode).Value) = objtr.Parameter_Code Then
                                gv1.Rows(ii).Cells(colValue).Value = objtr.Value
                            End If
                        Next
                    Next
                End If
                '===================================

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        LoadData(txtCode.Value, NavType)
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Try
            Dim qst As String = "select count(*) from TSPL_JW_FORMULA where Code='" + txtCode.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If
            If txtCode.MyReadOnly OrElse isButtonClicked Then

                'Dim qry As String = "select Code,Description from TSPL_DEPRECIATION_METHOD"
                'LoadData(clsCommon.ShowSelectForm("DepreciatioMet1", qry, "Code", "", txtCode.Value, "Code", isButtonClicked), NavigatorType.Current)
                LoadData(clsJWFormula.getFinder("", txtCode.Value, isButtonClicked), NavigatorType.Current)
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
                If (clsJWFormula.DeleteData(txtCode.Value)) Then
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
            'Dim dblFormula As String = (txtFormula.Text)
            'txtFormula.Text = CDbl(dblFormula)
            SaveData()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    Private Sub txtFormula_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFormula.KeyDown
        ' Dim res As String
        'MessageBox.Show("The key pressed was: " & e.KeyChar)

        'res = Regex.Replace(txtFormula.Text, "[^\w\\-]@", "")
        'txtFormula.Text = res
    End Sub

    'Private Sub gv1_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs)
    '    txtFormula.Text = txtFormula.Text.Insert(txtFormula.SelectionStart, " " + clsCommon.myCstr(gv1.CurrentRow.Cells("Code").Value))
    'End Sub
    '' Anubhooti 23-June-2014
    Private Sub rmExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmExport.Click
        Dim strDetail As String

        'strDetail = " Select Code As [Code],Description As [Description],Formula As [Formula],Structure_Code as [Structure Code] From TSPL_JW_FORMULA "
        strDetail = "  select TSPL_JW_FORMULA.Code , TSPL_JW_FORMULA.Description , TSPL_JW_FORMULA.Structure_Code as [Structure Code] , TSPL_JW_FORMULA.Formula, " &
                    "  (Select Parameter_Code From (Select ROW_NUMBER () over (order by Code,Parameter_Code ) As SNo,Code,Parameter_Code  From TSPL_JW_FORMULA_DETAILS where TSPL_JW_FORMULA_DETAILS.Code =TSPL_JW_FORMULA.Code) xxx where xxx.SNo =1 )  AS Parameter1 " &
                    "  ,(Select Value From (Select ROW_NUMBER () over (order by Code,Parameter_Code ) As SNo,Code,Value  From TSPL_JW_FORMULA_DETAILS where TSPL_JW_FORMULA_DETAILS.Code =TSPL_JW_FORMULA.Code) xxx where xxx.SNo =1 )  AS Value1, " &
                    "  (Select Parameter_Code From (Select ROW_NUMBER () over (order by Code,Parameter_Code ) As SNo,Code,Parameter_Code  From TSPL_JW_FORMULA_DETAILS where TSPL_JW_FORMULA_DETAILS.Code =TSPL_JW_FORMULA.Code) xxx where xxx.SNo =2 )  AS Parameter2" &
                    " ,(Select Value From (Select ROW_NUMBER () over (order by Code,Parameter_Code ) As SNo,Code,Value  From TSPL_JW_FORMULA_DETAILS where TSPL_JW_FORMULA_DETAILS.Code =TSPL_JW_FORMULA.Code) xxx where xxx.SNo =2 )  AS Value2, " &
                    "  (Select Parameter_Code From (Select ROW_NUMBER () over (order by Code,Parameter_Code ) As SNo,Code,Parameter_Code  From TSPL_JW_FORMULA_DETAILS where TSPL_JW_FORMULA_DETAILS.Code =TSPL_JW_FORMULA.Code) xxx where xxx.SNo =3 )  AS Parameter3 " &
                    " ,(Select Value From (Select ROW_NUMBER () over (order by Code,Parameter_Code ) As SNo,Code,Value  From TSPL_JW_FORMULA_DETAILS where TSPL_JW_FORMULA_DETAILS.Code =TSPL_JW_FORMULA.Code) xxx where xxx.SNo =3 )  AS Value3, " &
                    "  (Select Parameter_Code From (Select ROW_NUMBER () over (order by Code,Parameter_Code ) As SNo,Code,Parameter_Code  From TSPL_JW_FORMULA_DETAILS where TSPL_JW_FORMULA_DETAILS.Code =TSPL_JW_FORMULA.Code) xxx where xxx.SNo =4 )  AS Parameter4 " &
                    " ,(Select Value From (Select ROW_NUMBER () over (order by Code,Parameter_Code ) As SNo,Code,Value  From TSPL_JW_FORMULA_DETAILS where TSPL_JW_FORMULA_DETAILS.Code =TSPL_JW_FORMULA.Code) xxx where xxx.SNo =4 )  AS Value4 , " &
                    "  (Select Parameter_Code From (Select ROW_NUMBER () over (order by Code,Parameter_Code ) As SNo,Code,Parameter_Code  From TSPL_JW_FORMULA_DETAILS where TSPL_JW_FORMULA_DETAILS.Code =TSPL_JW_FORMULA.Code) xxx where xxx.SNo =5 )  AS Parameter5 " &
                    " ,(Select Value From (Select ROW_NUMBER () over (order by Code,Parameter_Code ) As SNo,Code,Value  From TSPL_JW_FORMULA_DETAILS where TSPL_JW_FORMULA_DETAILS.Code =TSPL_JW_FORMULA.Code) xxx where xxx.SNo =5 )  AS Value5 , " &
                    "  (Select Parameter_Code From (Select ROW_NUMBER () over (order by Code,Parameter_Code ) As SNo,Code,Parameter_Code  From TSPL_JW_FORMULA_DETAILS where TSPL_JW_FORMULA_DETAILS.Code =TSPL_JW_FORMULA.Code) xxx where xxx.SNo =6 )  AS Parameter6 " &
                    " ,(Select Value From (Select ROW_NUMBER () over (order by Code,Parameter_Code ) As SNo,Code,Value  From TSPL_JW_FORMULA_DETAILS where TSPL_JW_FORMULA_DETAILS.Code =TSPL_JW_FORMULA.Code) xxx where xxx.SNo =6 )  AS Value6, " &
                    "  (Select Parameter_Code From (Select ROW_NUMBER () over (order by Code,Parameter_Code ) As SNo,Code,Parameter_Code  From TSPL_JW_FORMULA_DETAILS where TSPL_JW_FORMULA_DETAILS.Code =TSPL_JW_FORMULA.Code) xxx where xxx.SNo =7 )  AS Parameter7 " &
                    " ,(Select Value From (Select ROW_NUMBER () over (order by Code,Parameter_Code ) As SNo,Code,Value  From TSPL_JW_FORMULA_DETAILS where TSPL_JW_FORMULA_DETAILS.Code =TSPL_JW_FORMULA.Code) xxx where xxx.SNo =7 )  AS Value7, " &
                    "  (Select Parameter_Code From (Select ROW_NUMBER () over (order by Code,Parameter_Code ) As SNo,Code,Parameter_Code  From TSPL_JW_FORMULA_DETAILS where TSPL_JW_FORMULA_DETAILS.Code =TSPL_JW_FORMULA.Code) xxx where xxx.SNo =8 )  AS Parameter8 " &
                    " ,(Select Value From (Select ROW_NUMBER () over (order by Code,Parameter_Code ) As SNo,Code,Value  From TSPL_JW_FORMULA_DETAILS where TSPL_JW_FORMULA_DETAILS.Code =TSPL_JW_FORMULA.Code) xxx where xxx.SNo =8 )  AS Value8, " &
                    "  (Select Parameter_Code From (Select ROW_NUMBER () over (order by Code,Parameter_Code ) As SNo,Code,Parameter_Code  From TSPL_JW_FORMULA_DETAILS where TSPL_JW_FORMULA_DETAILS.Code =TSPL_JW_FORMULA.Code) xxx where xxx.SNo =9 )  AS Parameter9 " &
                    " ,(Select Value From (Select ROW_NUMBER () over (order by Code,Parameter_Code ) As SNo,Code,Value  From TSPL_JW_FORMULA_DETAILS where TSPL_JW_FORMULA_DETAILS.Code =TSPL_JW_FORMULA.Code) xxx where xxx.SNo =9 )  AS Value9, " &
                    "  (Select Parameter_Code From (Select ROW_NUMBER () over (order by Code,Parameter_Code ) As SNo,Code,Parameter_Code  From TSPL_JW_FORMULA_DETAILS where TSPL_JW_FORMULA_DETAILS.Code =TSPL_JW_FORMULA.Code) xxx where xxx.SNo =10 )  AS Parameter10 " &
                    " ,(Select Value From (Select ROW_NUMBER () over (order by Code,Parameter_Code ) As SNo,Code,Value  From TSPL_JW_FORMULA_DETAILS where TSPL_JW_FORMULA_DETAILS.Code =TSPL_JW_FORMULA.Code) xxx where xxx.SNo =10 )  AS Value10 " &
                    "  from TSPL_JW_FORMULA  "
        transportSql.ExporttoExcel(strDetail, Me)
    End Sub
    '' Anubhooti 23-June-2014
    Private Sub rmImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim obj As clsJWFormula = Nothing
        Dim currentdate As Date = Date.Today
        Dim trans As SqlTransaction = Nothing
        If transportSql.importExcel(gv, "Code", "Description", "Structure Code", "Formula", "Parameter1", "Value1", "Parameter2", "Value2", "Parameter3", "Value3", "Parameter4", "Value4", "Parameter5", "Value5", "Parameter6", "Value6", "Parameter7", "Value7", "Parameter8", "Value8", "Parameter9", "Value9", "Parameter10", "Value10") Then
            Dim linno As Integer = 0
            Try
                Dim arr As New List(Of clsJWFormula)
                clsCommon.ProgressBarShow()
                trans = clsDBFuncationality.GetTransactin()
                For Each grow As GridViewRowInfo In gv.Rows
                    obj = New clsJWFormula
                    linno += 1
                    Dim strcode As String = clsCommon.myCstr(grow.Cells("Code").Value)
                    If (String.IsNullOrEmpty(strcode)) Or clsCommon.myLen(strcode) > 30 Then
                        Throw New Exception("Length of Code should be max. 30 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Code = strcode

                    Dim strDesp As String = clsCommon.myCstr(grow.Cells("Description").Value)
                    If (String.IsNullOrEmpty(strDesp)) Or clsCommon.myLen(strDesp) > 100 Then
                        Throw New Exception("Length of Description should be max. 100 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Description = strDesp

                    Dim strFormula As String = clsCommon.myCstr(grow.Cells("Formula").Value)
                    If (String.IsNullOrEmpty(strFormula)) Or clsCommon.myLen(strFormula) > 500 Then
                        Throw New Exception("Length of Formula should be max. 500 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Formula = strFormula

                    Dim strType As String = clsCommon.myCstr(grow.Cells("Structure Code").Value)

                    Dim strStructureCode As Boolean = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select count(*) from TSPL_STRUCTURE_MASTER where Structure_Code ='" + strType + "'  ", trans))
                    If strStructureCode = False Then
                        Throw New Exception("Invalid Structure Code. At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Structure_Code = strType

                    If clsCommon.myLen(strFormula) > 0 Then
                        Try
                            Dim ChkFormula As String = strFormula

                            For Each grow1 As GridViewRowInfo In gv1.Rows
                                ChkFormula = ChkFormula.Replace(clsCommon.myCstr(grow1.Cells(colCode).Value), "2")
                            Next
                            'ChkFormula = ChkFormula.Replace(clsDepreciationParameter.BNV, "2")
                            'ChkFormula = ChkFormula.Replace(clsDepreciationParameter.BEY, "2")
                            'ChkFormula = ChkFormula.Replace(clsDepreciationParameter.BSV, "2")
                            'ChkFormula = ChkFormula.Replace(clsDepreciationParameter.BDT, "2")
                            'ChkFormula = ChkFormula.Replace(clsDepreciationParameter.BRT, "2")
                            'ChkFormula = ChkFormula.Replace(clsDepreciationParameter.BSR, "2")
                            Dim dblxyz = clsDBFuncationality.getSingleValue("select  " + ChkFormula + "", trans)
                        Catch ex As Exception
                            Throw New Exception("Not a Correct Formula. At Line No. " + clsCommon.myCstr(linno) + ".")
                            'clsCommon.MyMessageBoxShow(ex.Message)
                            txtFormula.Focus()
                        End Try
                    End If
                    If clsCommon.myLen(strcode) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from TSPL_JW_FORMULA  where Code='" + strcode + "' ", trans) > 0 Then
                        isNewEntry = False
                    Else
                        isNewEntry = True
                    End If
                    arr.Add(obj)
                    '==================================================
                    Dim Arr2 As New List(Of clsJWFormulaDetails)
                    For i As Integer = 1 To 10
                        Dim objTr As New clsJWFormulaDetails()
                        Dim parameterCode As String = "Parameter" + clsCommon.myCstr(i)
                        Dim strValue As String = "Value" + clsCommon.myCstr(i)
                        If clsCommon.myLen(clsCommon.myCstr(grow.Cells(parameterCode).Value)) > 0 Then
                            Dim chkPraCode As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count (*) from TSPL_JW_PARAMETER_MASTER where Code = '" + clsCommon.myCstr(grow.Cells(parameterCode).Value) + "'", trans))
                            If chkPraCode <= 0 Then
                                Throw New Exception("Invalid " + parameterCode + " Code At Line No. " + clsCommon.myCstr(linno) + ".")
                            End If
                            objTr.Code = clsCommon.myCstr(grow.Cells("Code").Value)
                            objTr.Parameter_Code = clsCommon.myCstr(grow.Cells(parameterCode).Value)
                            If chkPraCode > 0 AndAlso String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells(strValue).Value)) = False Then
                                objTr.Value = clsCommon.myCstr(grow.Cells(strValue).Value)
                            Else
                                objTr.Value = 0
                            End If
                            Arr2.Add(objTr)
                        End If

                    Next
                    '==================================================
                    obj.SaveData(arr, Arr2, isNewEntry, trans)
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

    Private Sub txtStructurer__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtStructurer._MYValidating
        Dim qry As String = "select Structure_Code as [Code],Structure_Descq as [Description]  from TSPL_STRUCTURE_MASTER"
        txtStructurer.Value = clsCommon.ShowSelectForm("IMStruCode", qry, "Code", "", txtStructurer.Value, "", isButtonClicked)
        lblStructurer.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Structure_Descq from TSPL_STRUCTURE_MASTER where Structure_Code='" + txtStructurer.Value + "'"))
    End Sub

    Private Sub gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellDoubleClick
        If e.Column Is gv1.Columns("Code") Or e.Column Is gv1.Columns("Name") Then
            txtFormula.Text = txtFormula.Text.Insert(txtFormula.SelectionStart, " " + clsCommon.myCstr(gv1.CurrentRow.Cells("Code").Value))
        End If
    End Sub

    Private Sub gv1_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gv1.CellFormatting
        Try
            If e.Column Is gv1.Columns("Value") Then
                If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Type").Value), "NONE") = CompairStringResult.Equal Then
                    gv1.CurrentRow.Cells("Value").ReadOnly = False
                Else
                    gv1.CurrentRow.Cells("Value").ReadOnly = True
                End If
            End If
        Catch ex As Exception
        End Try

    End Sub
End Class

Public Class clsDepreciationParameter
    Public Const BNV As String = "BNV"
    Public Const BNVName As String = "Book Net Value"
    Public Const BEY As String = "BEY"
    Public Const BEYName As String = "Book Estimated Life"
    Public Const BSV As String = "BSV"
    Public Const BSVName As String = "Book Salvage Value"
    Public Const BSR As String = "BSR"
    Public Const BSRName As String = "Book Salvage Rate"
    Public Const BDT As String = "BDT"
    Public Const BDTName As String = "Book Accumulated Depreciation count"
    Public Const BRT As String = "BRT"
    Public Const BRTName As String = "Book Depreciation Rate"
    Public Const BCLD As String = "BCLD"
    Public Const BCLDays As String = "Book Current Life in Days"
End Class