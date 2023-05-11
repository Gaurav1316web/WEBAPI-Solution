Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class frmDepreciationMethod
    Inherits FrmMainTranScreen

#Region "Variables"
    Private isNewEntry As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
    'Dim blnOperator As Boolean = False
    Const colCode As String = "Code"
    Const colDesc As String = "Description"
#End Region

    Private Sub SetUserMgmtNew()
        '' Anubhooti 31-July-2014 BM00000003131
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmDepreciationMethod)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If
        btnSave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnSave.Visible = True Then
            rmExport.Enabled = True
            rmImport.Enabled = True
        Else
            rmExport.Enabled = False
            rmImport.Enabled = False
        End If
        btnDelete.Visible = MyBase.isDeleteFlag
        '--------------------------------------------------
    End Sub
    '=======================Added by preeti gupta[13/04/2017]====================
    Sub LoadType()
        'isInsideLoadData = True
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()

        dr("Code") = "Formula"
        dr("Name") = "Formula"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Rate"
        dr("Name") = "Rate"
        dt.Rows.Add(dr)

        cboType.DataSource = dt
        cboType.ValueMember = "Code"
        cboType.DisplayMember = "Name"
        'isInsideLoadData = False
    End Sub
    '==============================================================================
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
        Dim dt As New DataTable
        dt.Columns.Add("SNo", GetType(Integer))
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("SNo") = 1
        dr("Code") = clsDepreciationParameter.BNV
        dr("Name") = clsDepreciationParameter.BNVName
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("SNo") = 2
        dr("Code") = clsDepreciationParameter.BEY
        dr("Name") = clsDepreciationParameter.BEYName
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("SNo") = 3
        dr("Code") = clsDepreciationParameter.BSV
        dr("Name") = clsDepreciationParameter.BSVName
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("SNo") = 4
        dr("Code") = clsDepreciationParameter.BDT
        dr("Name") = clsDepreciationParameter.BDTName
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("SNo") = 5
        dr("Code") = clsDepreciationParameter.BRT
        dr("Name") = clsDepreciationParameter.BRTName
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("SNo") = 6
        dr("Code") = clsDepreciationParameter.BSR
        dr("Name") = clsDepreciationParameter.BSRName
        dt.Rows.Add(dr)

        gv1.DataSource = dt
        gv1.Columns("SNo").ReadOnly = True
        gv1.Columns("SNo").Width = "30"

        gv1.Columns("Code").ReadOnly = True
        gv1.Columns("Code").Width = "80"

        gv1.Columns("Name").ReadOnly = True
        gv1.Columns("Name").Width = "200"

    End Sub

    Sub SetLength()
        txtCode.MyMaxLength = 30
        txtDesc.MaxLength = 100
        txtFormula.MaxLength = 500
    End Sub

    Sub LoadDepreciationFields()
        Dim qry As String = "select Code as " + colCode + ",Description as " + colDesc + " from TSPL_DEPRECIATION_FIELD"
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
        btnSave.Text = "Save"
        LoadType()
    End Sub

    Sub BlankAllControls()
        txtCode.Value = ""
        txtDesc.Text = ""
        txtFormula.Text = ""
    End Sub

    Function AllowToSave() As Boolean
        If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) Then
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Enter Code")
                txtCode.Focus()
                Return False
            End If
        End If
        If clsCommon.myLen(txtDesc.Text) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please Enter Description")
            txtDesc.Focus()
            Return False
        End If
        If clsCommon.myLen(txtFormula.Text) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please Enter Forumula for calculation")
            txtFormula.Focus()
            Return False
        End If
        If clsCommon.myLen(cboType.Text) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please Enter Type")
            cboType.Focus()
            Return False
        End If
        Try

            Dim strFormula As String = txtFormula.Text
            strFormula = strFormula.Replace(clsDepreciationParameter.BNV, "2")
            strFormula = strFormula.Replace(clsDepreciationParameter.BEY, "2")
            strFormula = strFormula.Replace(clsDepreciationParameter.BSV, "2")
            strFormula = strFormula.Replace(clsDepreciationParameter.BDT, "2")
            strFormula = strFormula.Replace(clsDepreciationParameter.BRT, "2")
            strFormula = strFormula.Replace(clsDepreciationParameter.BSR, "2")
            Dim dblxyz = clsDBFuncationality.getSingleValue("select  " + strFormula + "")
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow("Not a Correct Formula")
            txtFormula.Focus()
            Return False
        End Try
        Return True
    End Function

    Private Sub SaveData()
        Try
            If (AllowToSave()) Then

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmDepreciationMethod, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
                Dim arr As New List(Of clsDepreciationMethod)
                Dim obj As New clsDepreciationMethod()
                obj.Code = txtCode.Value
                obj.Description = txtDesc.Text
                obj.Formula = txtFormula.Text
                obj.Type = cboType.Text
                arr.Add(obj)
                If obj.SaveData(arr, isNewEntry) Then
                    clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    LoadData(obj.Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            isNewEntry = False
            btnSave.Text = "Update"
            BlankAllControls()
            txtCode.MyReadOnly = True
            Dim obj As New clsDepreciationMethod()
            obj = clsDepreciationMethod.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
                txtCode.Value = obj.Code
                txtDesc.Text = obj.Description
                txtFormula.Text = obj.Formula
                cboType.Text = obj.Type
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        LoadData(txtCode.Value, NavType)
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Try
            Dim qst As String = "select count(*) from TSPL_DEPRECIATION_METHOD where Code='" + txtCode.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If
            If txtCode.MyReadOnly OrElse isButtonClicked Then

                'Dim qry As String = "select Code,Description from TSPL_DEPRECIATION_METHOD"
                'LoadData(clsCommon.ShowSelectForm("DepreciatioMet1", qry, "Code", "", txtCode.Value, "Code", isButtonClicked), NavigatorType.Current)
                LoadData(clsDepreciationMethod.getFinder("", txtCode.Value, isButtonClicked), NavigatorType.Current)
                LoadData(txtCode.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
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
                If (clsDepreciationMethod.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully")
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

    Private Sub gv1_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellDoubleClick
        txtFormula.Text = txtFormula.Text.Insert(txtFormula.SelectionStart, " " + clsCommon.myCstr(gv1.CurrentRow.Cells("Code").Value))
    End Sub
    '' Anubhooti 23-June-2014
    Private Sub rmExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmExport.Click
        Dim strDetail As String

        strDetail = " Select Code As [Code],Description As [Description],Formula As [Formula],Type From TSPL_DEPRECIATION_METHOD "
        transportSql.ExporttoExcel(strDetail, Me)
    End Sub
    '' Anubhooti 23-June-2014
    Private Sub rmImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim obj As clsDepreciationMethod = Nothing
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Code", "Description", "Formula", "Type") Then
            Dim linno As Integer = 0
            Try
                Dim arr As New List(Of clsDepreciationMethod)
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    obj = New clsDepreciationMethod
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

                    Dim strType As String = clsCommon.myCstr(grow.Cells("Type").Value)

                    If clsCommon.CompairString(strType, "Rate") <> CompairStringResult.Equal And clsCommon.CompairString(strType, "Formula") <> CompairStringResult.Equal Then
                        Throw New Exception("Current Type should be amoung 'Rate','Formula' ")
                    End If
                    ' obj.Type = strType

                    If clsCommon.myLen(strFormula) > 0 Then
                        Try
                            Dim ChkFormula As String = strFormula
                            ChkFormula = ChkFormula.Replace(clsDepreciationParameter.BNV, "2")
                            ChkFormula = ChkFormula.Replace(clsDepreciationParameter.BEY, "2")
                            ChkFormula = ChkFormula.Replace(clsDepreciationParameter.BSV, "2")
                            ChkFormula = ChkFormula.Replace(clsDepreciationParameter.BDT, "2")
                            ChkFormula = ChkFormula.Replace(clsDepreciationParameter.BRT, "2")
                            ChkFormula = ChkFormula.Replace(clsDepreciationParameter.BSR, "2")
                            Dim dblxyz = clsDBFuncationality.getSingleValue("select  " + ChkFormula + "")
                        Catch ex As Exception
                            Throw New Exception("Not a Correct Formula")
                            'clsCommon.MyMessageBoxShow(ex.Message)
                            txtFormula.Focus()
                        End Try
                    End If
                    If clsCommon.myLen(strcode) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from TSPL_DEPRECIATION_METHOD where Code='" + strcode + "' ") > 0 Then
                        isNewEntry = False
                    Else
                        isNewEntry = True
                    End If
                    arr.Add(obj)
                Next
                obj.SaveData(arr, isNewEntry)
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
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