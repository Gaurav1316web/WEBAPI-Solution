'Created By---> priti sharma
'Created Date---> 17 /08/2016
'Added sequence no on 21/10/2016 against BM00000010116
Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Data.OleDb
Imports System.Drawing
Imports System.IO
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Imports System.Text.RegularExpressions
Imports Telerik.WinControls.UI.Export
Imports Telerik.WinControls.UI.Export.ExportToExcelML
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Globalization
Imports common
Imports System.Threading
Public Class frmMilkGradeMaster
    Inherits FrmMainTranScreen
    Private isInsideLoadData As Boolean = False
    Private isCellValueChangedOpen As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim obj As New clsMilkGradeMaster
    Const colCode As String = "colCode"
    Const colSLNo As String = "SL. No."
    Const colDesc As String = "Description"
    Const colLower As String = "Lower_Range"
    Const colUpper As String = "Upper_Range"
    Const colStatus As String = "Status"
    Const colValue1 As String = "Value1"
    Const colValue2 As String = "Value2"
    Const colNature As String = "colNature"
    Const colISelect As String = "colISelect"
    Private Sub frmPrimaryReasonMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso rbtnSave.Enabled Then
                saveData()

            ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso rbtnDelete.Enabled Then
                deleteData()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
                Close()
            ElseIf e.Alt And e.KeyCode = Keys.N Then
                Reset()
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub


    Private Sub frmPrimaryReasonMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            SetUserMgmtNew()
            fndCode.MyMaxLength = 30
            txtDescription.MaxLength = 200

            ButtonToolTip.SetToolTip(rbtnSave, "Press Alt+S for Save/Update ")
            ButtonToolTip.SetToolTip(rbtnDelete, "Press Alt+D  for Delete ")
            ButtonToolTip.SetToolTip(rbtnClose, "Press Alt+C Close the Window")
            LoadMilkType()
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub LoadMilkType()
        cmbMilkType.DataSource = Nothing
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "A"
        dr("Name") = "A"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "A+"
        dr("Name") = "A+"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "A++"
        dr("Name") = "A++"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "B"
        dr("Name") = "B"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "B+"
        dr("Name") = "B+"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "B++"
        dr("Name") = "B++"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "B+++"
        dr("Name") = "B+++"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "C"
        dr("Name") = "C"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "C+"
        dr("Name") = "C+"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "C++"
        dr("Name") = "C++"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Others"
        dr("Name") = "Others"
        dt.Rows.Add(dr)

        cmbMilkType.DataSource = dt
        cmbMilkType.DisplayMember = "Name"
        cmbMilkType.ValueMember = "Code"
    End Sub

    Sub LoadBlankGrid()
        gv.DataSource = Nothing
        gv.Rows.Clear()
        gv.Columns.Clear()

        Dim repoSL As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSL.Name = colSLNo
        repoSL.Width = 60
        repoSL.HeaderText = "SL. No."
        repoSL.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoSL)

        Dim repoISelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoISelect.FormatString = ""
        repoISelect.HeaderText = "Select"
        repoISelect.Name = colISelect
        repoISelect.Width = 100
        repoISelect.IsVisible = False
        repoISelect.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoISelect)


        Dim repocode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repocode.Name = colCode
        repocode.Width = 150
        repocode.HeaderText = "Parameter Code"
        repocode.ReadOnly = True
        repocode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repocode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv.MasterTemplate.Columns.Add(repocode)


        Dim reponame As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reponame.FormatString = ""
        reponame.Name = colDesc
        reponame.Width = 205
        reponame.HeaderText = "Description"
        reponame.ReadOnly = True
        gv.MasterTemplate.Columns.Add(reponame)

        Dim repolower As GridViewDecimalColumn = New GridViewDecimalColumn()
        repolower.Name = colLower
        repolower.Width = 80
        repolower.FormatString = "{0:n3}"
        repolower.HeaderText = "Lower Range"
        repolower.DecimalPlaces = 3
        repolower.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repolower)

        Dim repoupper As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoupper.Name = colUpper
        repoupper.Width = 80
        repoupper.HeaderText = "Upper Range"
        repoupper.FormatString = "{0:n3}"
        repoupper.ReadOnly = False
        repoupper.DecimalPlaces = 3
        gv.MasterTemplate.Columns.Add(repoupper)

        Dim repovalue1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repovalue1.Name = colValue1
        repovalue1.Width = 180
        repovalue1.ReadOnly = True
        repovalue1.HeaderText = "Value"
        gv.MasterTemplate.Columns.Add(repovalue1)
        repovalue1 = Nothing


        Dim repostatus As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repostatus.Name = colStatus
        repostatus.Width = 80
        repostatus.HeaderText = "Status(Yes/No)"
        repostatus.DataSource = LoadCombobox()
        repostatus.ValueMember = "Code"
        repostatus.DisplayMember = "Name"
        gv.MasterTemplate.Columns.Add(repostatus)
        repostatus = Nothing

        gv.AllowDeleteRow = True
        gv.AllowAddNewRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = True
        gv.AllowRowReorder = True
        gv.EnableSorting = True
        gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv.MasterTemplate.ShowRowHeaderColumn = False
        gv.EnableFiltering = True
        'gv.Rows.AddNew()
    End Sub
    Function LoadCombobox() As DataTable
        Dim qry As String = "select * from (select '' as Code,'None' as Name union all select 'YES' as Code,'YES' as Name union all select 'NO' as Code,'NO' as Name)a"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Return dt
    End Function
    Sub OpenParameter()
        'iscellvalue = True
        Dim qry As String = "select Code,Description,Type,nature from tspl_parameter_master "
        Dim dr As DataRow = clsCommon.ShowSelectFormForRow("PMFND", qry, "Code", gv.CurrentRow.Cells(colCode).Value)
        If dr IsNot Nothing Then
            gv.CurrentRow.Cells(colCode).Value = clsCommon.myCstr(dr("code"))
            gv.CurrentRow.Cells(colDesc).Value = clsCommon.myCstr(dr("description"))
        Else
            gv.CurrentRow.Cells(colCode).Value = ""
            gv.CurrentRow.Cells(colDesc).Value = ""
        End If

    End Sub
    Private Sub Reset()
        Try
            fndCode.Value = ""
            txtDescription.Text = ""
            cmbMilkType.SelectedValue = ""
            txtMilktypeCode.Value = ""
            lblMilkDescription.Text = ""
            lblMilkType.Text = ""
            rbtnDelete.Enabled = False
            fndCode.Focus()
            rbtnSave.Text = "Save"
            fndCode.MyReadOnly = False
            LoadBlankGrid()
            LoadGrid()
            'gv.Rows.AddNew()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmMilkGradeMaster)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        rbtnSave.Visible = MyBase.isModifyFlag
        rbtnDelete.Visible = MyBase.isDeleteFlag
    End Sub


    Private Sub rbtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnClose.Click
        Me.Close()
    End Sub

    Private Sub rbtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnSave.Click
        If AllowToSave() Then saveData()
    End Sub
    Public Function AllowToSave() As Boolean
        Try
            If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) AndAlso clsCommon.myLen(fndCode.Value) = 0 Then
                common.clsCommon.MyMessageBoxShow("Please Enter Code", Me.Text)
                Return False
            ElseIf cmbMilkType.SelectedValue = "" Then
                common.clsCommon.MyMessageBoxShow("Please Enter Grade Type", Me.Text)
                Return False
            End If
            If clsCommon.myLen(txtMilktypeCode.Value) = 0 Then
                common.clsCommon.MyMessageBoxShow("Please select Milk Type", Me.Text)
                Return False
            End If
            If rbtnSave.Text = "Save" Then
                Dim intExist As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select count(GRADE_TYPE) from TSPL_MILK_GRADE_MASTER where GRADE_TYPE='" & cmbMilkType.SelectedValue & "' AND MILK_TYPE_CODE='" & clsCommon.myCstr(txtMilktypeCode.Value) & "'"))
                If intExist = 1 Then
                    clsCommon.MyMessageBoxShow("Code already exist for this grade.", Me.Text)
                    Return False
                End If
            End If

            For ii As Integer = 0 To gv.Rows.Count - 1
                If clsCommon.myLen(gv.Rows(ii).Cells(colCode).Value) > 0 Then
                    Dim code As String = clsCommon.myCstr(gv.Rows(ii).Cells(colCode).Value)
                    Dim qry As String = ""
                    qry = "select nature from tspl_parameter_master where code='" + code + "'"
                    Dim nature As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

                    If clsCommon.CompairString(nature, "A") = CompairStringResult.Equal Then
                        If clsCommon.myLen(gv.Rows(ii).Cells(colValue1).Value) = 0 Then
                            clsCommon.MyMessageBoxShow("Please enter Value for parameter " + code, Me.Text)
                            Return False
                        End If
                    End If

                    If clsCommon.CompairString(nature, "R") = CompairStringResult.Equal Then
                        If clsCommon.myCdbl(gv.Rows(ii).Cells(colLower).Value) = 0 AndAlso clsCommon.myCdbl(gv.Rows(ii).Cells(colUpper).Value) = 0 Then
                            clsCommon.MyMessageBoxShow("Please enter Both range for parameter " + code, Me.Text)
                            Return False
                        ElseIf clsCommon.myCdbl(gv.Rows(ii).Cells(colLower).Value) > clsCommon.myCdbl(gv.Rows(ii).Cells(colUpper).Value) Then
                            clsCommon.MyMessageBoxShow("Lower range should be smaller than upper range for parameter " + code, Me.Text)
                            Return False
                        End If
                    End If

                    If clsCommon.CompairString(nature, "B") = CompairStringResult.Equal Then
                        If clsCommon.myLen(gv.Rows(ii).Cells(colStatus).Value) = 0 Then
                            clsCommon.MyMessageBoxShow("Please select status for parameter " + code, Me.Text)
                            Return False
                        End If
                    End If
                End If
            Next
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Function
    Private Function SaveData() As Boolean
        Try

            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmMilkGradeMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return False
                End If
            End If

            obj = New clsMilkGradeMaster()
            obj.MILK_GRADE_CODE = fndCode.Value
            obj.DESCRIPTION = txtDescription.Text
            obj.GRADE_TYPE = cmbMilkType.SelectedValue
            obj.SequenceNo = txtSequenceNo.Value
            obj.MILK_TYPE_CODE = clsCommon.myCstr(txtMilktypeCode.Value)
            obj.Arr = New List(Of clsMilkGradeDetail)
            For Each grow As GridViewRowInfo In gv.Rows
                Dim objTr As New clsMilkGradeDetail()
                ' If clsCommon.myCBool(clsCommon.myCBool(grow.Cells(colISelect).Value)) = True Then
                objTr.Line_No = clsCommon.myCdbl(grow.Cells(colSLNo).Value)
                objTr.Parameter_Code = clsCommon.myCstr(grow.Cells(colCode).Value)
                objTr.Lower_Range = clsCommon.myCdbl(grow.Cells(colLower).Value)
                objTr.Upper_Range = clsCommon.myCdbl(grow.Cells(colUpper).Value)
                objTr.status = clsCommon.myCstr(grow.Cells(colStatus).Value)
                objTr.value1 = clsCommon.myCstr(grow.Cells(colValue1).Value)
                If (clsCommon.myLen(objTr.Parameter_Code) > 0) Then
                    obj.Arr.Add(objTr)
                End If
                'End If             
            Next
            If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow("Please Select at least One parameter")
                Return False
            End If
            Dim isSaved As Boolean = obj.SaveData(obj, IIf(rbtnSave.Text = "Save", True, False))
            If isSaved Then
                common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                loadData(obj.MILK_GRADE_CODE, NavigatorType.Current)
                rbtnSave.Text = "Update"
                rbtnDelete.Enabled = True
            Else
                rbtnSave.Text = "Save"
                rbtnDelete.Enabled = False
                common.clsCommon.MyMessageBoxShow("Data Could Not Saved")
            End If
            Return isSaved
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Function
    Function deleteData() As Boolean
        Try
            If clsCommon.myLen(fndCode.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Milk Type Code found to Delete", Me.Name)
                Return False
            ElseIf Not (common.clsCommon.MyMessageBoxShow("Delete the Milk Type Code " + fndCode.Value + Environment.NewLine + " Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
                Return False
            End If
            If (clsMilkGradeMaster.DeleteData(fndCode.Value)) Then
                common.clsCommon.MyMessageBoxShow("Data Deleted Sucessfully", Me.Name)
                Reset()
                Return True
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Function
    Private Sub LoadGrid()
        Try
            Dim strQuery As String = "select Code,Description,0.000 as Lower_Range,0.000 as Upper_Range from TSPL_PARAMETER_MASTER where IsForMilkGrade=1 "
            Dim dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(strQuery)
            Dim intLineNo As Integer = 0
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    gv.Rows.AddNew()
                    intLineNo += 1
                    gv.Rows(gv.Rows.Count - 1).Cells(colSLNo).Value = intLineNo
                    gv.Rows(gv.Rows.Count - 1).Cells(colISelect).Value = False
                    gv.Rows(gv.Rows.Count - 1).Cells(colCode).Value = clsCommon.myCstr(dr("Code"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colDesc).Value = clsCommon.myCstr(dr("Description"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colLower).Value = clsCommon.myCdbl(dr("Lower_Range"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colUpper).Value = clsCommon.myCdbl(dr("Upper_Range"))
                Next
            Else
                Throw New Exception("Please map parameter master for Milk Grade.")
            End If


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub loadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try

            obj = New clsMilkGradeMaster()
            obj = clsMilkGradeMaster.GetData(strCode, NavTyep)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.MILK_GRADE_CODE) > 0 Then
                Reset()
                LoadBlankGrid()
                isInsideLoadData = True
                rbtnSave.Text = "Update"
                fndCode.MyReadOnly = True
                fndCode.Value = obj.MILK_GRADE_CODE
                txtDescription.Text = obj.DESCRIPTION
                cmbMilkType.SelectedValue = obj.GRADE_TYPE
                txtSequenceNo.Value = obj.SequenceNo
                txtMilktypeCode.Value = obj.MILK_TYPE_CODE
                lblMilkDescription.Text = obj.MILK_TYPE_Description
                lblMilkType.Text = obj.MILK_TYPE_Type
                Dim intLineNo As Integer = 0
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsMilkGradeDetail In obj.Arr
                        intLineNo += 1
                        gv.Rows.AddNew()
                        gv.Rows(gv.Rows.Count - 1).Cells(colSLNo).Value = intLineNo
                        gv.Rows(gv.Rows.Count - 1).Cells(colISelect).Value = IIf(objTr.ParameterStatus = 1, True, False)
                        gv.Rows(gv.Rows.Count - 1).Cells(colCode).Value = objTr.Parameter_Code
                        gv.Rows(gv.Rows.Count - 1).Cells(colDesc).Value = clsDBFuncationality.getSingleValue("select Description from tspl_parameter_master where code= '" & objTr.Parameter_Code & "'")
                        gv.Rows(gv.Rows.Count - 1).Cells(colLower).Value = objTr.Lower_Range
                        gv.Rows(gv.Rows.Count - 1).Cells(colUpper).Value = objTr.Upper_Range
                        gv.Rows(gv.Rows.Count - 1).Cells(colStatus).Value = objTr.status
                        gv.Rows(gv.Rows.Count - 1).Cells(colValue1).Value = objTr.value1
                    Next
                End If
                isInsideLoadData = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndReasonID__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndCode._MYNavigator
        Try
            Dim Qry = "select count(*) from TSPL_MILK_GRADE_MASTER where MILK_GRADE_CODE='" + fndCode.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry))
            If count = 0 Then
                fndCode.MyReadOnly = False
            Else
                fndCode.MyReadOnly = True
            End If
            loadData(fndCode.Value, NavType)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub fndReasonID__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndCode._MYValidating
        Try
            Dim str As String = "select count(*) from TSPL_MILK_GRADE_MASTER where MILK_GRADE_CODE='" + fndCode.Value + "'"
            Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
            If no = 0 Then
                fndCode.MyReadOnly = False
            Else
                fndCode.MyReadOnly = True
            End If
            If fndCode.MyReadOnly OrElse isButtonClicked Then
                fndCode.Value = clsMilkGradeMaster.getFinder("", fndCode.Value, isButtonClicked)
                loadData(fndCode.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub rbtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnDelete.Click
        deleteData()
    End Sub

    Private Sub rbtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnReset.Click
        Reset()
    End Sub

    Private Sub rdmenuexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdmenuexit.Click
        Me.Close()
    End Sub

    Private Sub rdmenuexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdmenuexport.Click

    End Sub

    Private Sub rdmenuimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdmenuimport.Click

    End Sub

    Private Sub RadMenufile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenufile.Click

    End Sub
    Function OpenParameterValueList(ByVal code As String, ByVal strValue As String) As String
        Dim strRetValue As String = String.Empty
        Dim table_name As String = "tspl_parameter_master"
    
        Dim cnt As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select count(*) from TSPL_PARAMEter_value_master where parameter_code='" & code & "' "))
        If cnt <> 0 Then
            Dim frm As FrmCheckBoxGrid = New FrmCheckBoxGrid()
            Dim strValueList() As String = strValue.Split(",")
            frm.qry = " select Value as Value from TSPL_PARAMEter_value_master where parameter_code='" & code & "' "
            frm.qry = frm.qry
            frm.arrValue = New List(Of String)
            For i As Integer = 0 To strValueList.Count - 1
                frm.arrValue.Add(strValueList(i))
            Next
            frm.ShowDialog()

            If frm.arrValue IsNot Nothing AndAlso frm.arrValue.Count > 0 Then
                For i As Integer = 0 To frm.arrValue.Count - 1
                    strRetValue = strRetValue & frm.arrValue(i).ToString
                    If i <> frm.arrValue.Count - 1 Then
                        strRetValue = strRetValue & ","
                    End If
                Next
            End If
        Else

            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from " + table_name + " where code='" & code & "' and nature='A'")) > 0 Then
                clsCommon.MyMessageBoxShow("No record found.")
            End If
        End If
        Return strRetValue
    End Function
    Private Sub gv_CellClick(sender As Object, e As GridViewCellEventArgs) Handles gv.CellClick
        If gv.CurrentColumn Is gv.Columns(colValue1) AndAlso clsCommon.myLen(clsCommon.myCstr(gv.CurrentRow.Cells(colCode).Value)) > 0 Then
            gv.CurrentCell.Value = clsCommon.myCstr(OpenParameterValueList(clsCommon.myCstr(gv.CurrentRow.Cells(colCode).Value), clsCommon.myCstr(gv.CurrentCell.Value)))
        End If
    End Sub

    Private Sub gv_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gv.CellFormatting
        Try
            If e.Column Is gv.Columns(colCode) Then
                gv.CurrentRow.Cells(colLower).ReadOnly = True
                gv.CurrentRow.Cells(colUpper).ReadOnly = True
                gv.CurrentRow.Cells(colStatus).ReadOnly = True
                gv.CurrentRow.Cells(colValue1).ReadOnly = True
              
                If clsCommon.myLen(gv.CurrentRow.Cells(colCode).Value) > 0 Then
                    Dim code As String = clsCommon.myCstr(gv.CurrentRow.Cells(colCode).Value)
                    Dim qry As String = ""
                    qry = "select nature from tspl_parameter_master where code='" + code + "'"
                    Dim nature As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

                    If clsCommon.CompairString(nature, "A") = CompairStringResult.Equal Then
                        gv.CurrentRow.Cells(colValue1).ReadOnly = False
                        gv.CurrentRow.Cells(colLower).Value = Nothing
                        gv.CurrentRow.Cells(colUpper).Value = Nothing
                        gv.CurrentRow.Cells(colStatus).Value = Nothing
                    End If

                    If clsCommon.CompairString(nature, "R") = CompairStringResult.Equal Then
                        gv.CurrentRow.Cells(colLower).ReadOnly = False
                        gv.CurrentRow.Cells(colUpper).ReadOnly = False
                        gv.CurrentRow.Cells(colValue1).Value = Nothing
                        gv.CurrentRow.Cells(colStatus).Value = Nothing
                    End If

                    If clsCommon.CompairString(nature, "B") = CompairStringResult.Equal Then
                        gv.CurrentRow.Cells(colStatus).ReadOnly = False
                        gv.CurrentRow.Cells(colLower).Value = Nothing
                        gv.CurrentRow.Cells(colUpper).Value = Nothing
                        gv.CurrentRow.Cells(colValue1).Value = Nothing
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gv_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv.CellValueChanged
        'Try
        '    If (Not isInsideLoadData) Then
        '        If Not isCellValueChangedOpen Then
        '            isCellValueChangedOpen = True
        '            If e.Column Is gv.Columns(colCode) Then
        '                OpenParameter()
        '            End If
        '            isCellValueChangedOpen = False
        '        End If
        '    End If
        'Catch ex As Exception
        '    common.clsCommon.MyMessageBoxShow(ex.Message)
        'End Try
    End Sub

    Private Sub gv_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gv.CurrentColumnChanged
        'If gv.RowCount > 0 Then
        '    Dim intCurrRow As Integer = gv.CurrentRow.Index
        '    gv.CurrentRow.Cells(colSLNo).Value = clsCommon.myCdbl(intCurrRow + 1)
        '    If intCurrRow = gv.Rows.Count - 1 Then
        '        gv.Rows.AddNew()
        '        gv.CurrentRow = gv.Rows(intCurrRow)
        '    End If
        'End If
    End Sub

    
    Private Sub btnExportHead_Click(sender As Object, e As EventArgs) Handles btnExportHead.Click
        Try
            Dim str As String
            str = "select TSPL_MILK_GRADE_MASTER.MILK_GRADE_CODE as [CODE] ,TSPL_MILK_GRADE_MASTER.Description, TSPL_MILK_GRADE_MASTER.GRADE_TYPE as [GRADE TYPE],TSPL_MILK_GRADE_MASTER.MILK_TYPE_CODE AS [MILK TYPE CODE],TSPL_MILK_GRADE_MASTER.SequenceNo from   TSPL_MILK_GRADE_MASTER "
            transportSql.ExporttoExcel(str, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnExportDetail_Click(sender As Object, e As EventArgs) Handles btnExportDetail.Click
        Try
            Dim qry = "select count(*) from TSPL_MILK_GRADE_MASTER"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
            If check > 0 Then
                qry = "select Price_Code as [Price Code],Milk_Grade_code as [Milk Grade code],Fat_Weightage as [Fat Weightage],Snf_Weightage as [Snf Weightage],Fat_Percentage as [Fat Percentage],Snf_Percentage as [Snf Percentage],Standard_Rate as [Standard Rate],Tolerance from TSPL_MILK_GRADE_MASTER"
            Else
                qry = "select '' as [Price Code] ,'' as [Milk Grade code],0 as [Fat Weightage],0 as [Snf Weightage],0 as [Fat Percentage],0 as [Snf Percentage] ,0 as [Standard Rate],0 as Tolerance from TSPL_MILK_GRADE_MASTER"
            End If
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnImportHead_Click(sender As Object, e As EventArgs) Handles btnImportHead.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "CODE", "Description", "GRADE TYPE", "MILK TYPE CODE", "SequenceNo") Then
            ' Dim trans As SqlTransaction
            Try
                'connectSql.OpenConnection()
                'trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clsMilkGradeMaster()

                    Dim strCode As String = clsCommon.myCstr(grow.Cells("CODE").Value)
                    If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                        Throw New Exception("Code can not be blank or incorrect.")
                    End If
                    obj.MILK_GRADE_CODE = strCode

                    Dim strDec As String = clsCommon.myCstr(grow.Cells("Description").Value)
                    If clsCommon.myLen(strDec) > 100 Then
                        Throw New Exception("Description Length can not be greater than 100.")
                    End If
                    obj.DESCRIPTION = strDec
                    Dim GRADE_TYPE As String = ""
                    GRADE_TYPE = clsCommon.myCstr(grow.Cells("GRADE TYPE").Value)
                    If Not (clsCommon.CompairString(GRADE_TYPE, "A") = CompairStringResult.Equal OrElse clsCommon.CompairString(GRADE_TYPE, "A+") = CompairStringResult.Equal OrElse clsCommon.CompairString(GRADE_TYPE, "A++") = CompairStringResult.Equal OrElse clsCommon.CompairString(GRADE_TYPE, "B") = CompairStringResult.Equal OrElse clsCommon.CompairString(GRADE_TYPE, "B+") = CompairStringResult.Equal OrElse clsCommon.CompairString(GRADE_TYPE, "B++") = CompairStringResult.Equal OrElse clsCommon.CompairString(GRADE_TYPE, "C") = CompairStringResult.Equal OrElse clsCommon.CompairString(GRADE_TYPE, "C+") = CompairStringResult.Equal OrElse clsCommon.CompairString(GRADE_TYPE, "C++") = CompairStringResult.Equal OrElse clsCommon.CompairString(GRADE_TYPE, "Others") = CompairStringResult.Equal) Then
                        Throw New Exception("Milk Type Should be one of these ( A , A+ , A++ , B , B+ , B++ , C , C+ , C++ , others ) ")
                    End If
                    obj.GRADE_TYPE = GRADE_TYPE

                    ''-------------RICHA BM00000009836
                    Dim MILK_TYPECODE As String = String.Empty
                    MILK_TYPECODE = clsCommon.myCstr(grow.Cells("MILK TYPE CODE").Value)
                    If clsCommon.myLen(MILK_TYPECODE) > 30 Then
                        Throw New Exception("MILK TYPE CODE Length can not be greater than 30.")
                    End If
                    If clsCommon.myLen(MILK_TYPECODE) > 0 Then
                        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" Select count(*) from TSPL_MILK_TYPE_MASTER where MILK_TYPE_CODE ='" & clsCommon.myCstr(MILK_TYPECODE) & "'")) <= 0 Then
                            Throw New Exception("MILK TYPE CODE is not exist in MILK TYPE MASTER.")
                        End If
                    End If

                    obj.SequenceNo = clsCommon.myCdbl(grow.Cells("SequenceNo").Value)

                    obj.MILK_TYPE_CODE = MILK_TYPECODE
                    ''-------------------------------

                    If (clsMilkGradeMaster.CheckNewEntry(obj.MILK_GRADE_CODE, obj.GRADE_TYPE, obj.MILK_TYPE_CODE)) = True Then
                        Dim intExist As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select count(GRADE_TYPE) from TSPL_MILK_GRADE_MASTER where GRADE_TYPE='" & clsCommon.myCstr(GRADE_TYPE) & "' AND MILK_TYPE_CODE='" & clsCommon.myCstr(MILK_TYPECODE) & "' and MILK_GRADE_CODE not in ('" & obj.MILK_GRADE_CODE & "')"))
                        If intExist = 1 Then
                            Throw New Exception("Code already exist for Grade " & clsCommon.myCstr(GRADE_TYPE) & " and Milk Type " & clsCommon.myCstr(MILK_TYPECODE) & ".")
                        End If
                        obj.SaveData(obj, True)
                    Else
                        obj.SaveData(obj, False)
                    End If

                Next
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub btnImportdetail_Click(sender As Object, e As EventArgs) Handles btnImportdetail.Click

    End Sub

    Private Sub txtMilktypeCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtMilktypeCode._MYValidating
        Dim whr As String = ""
        txtMilktypeCode.Value = clsMilkTypeMaster.getFinder(whr, txtMilktypeCode.Value, isButtonClicked)
        If clsCommon.myLen(txtMilktypeCode.Value) > 0 Then
            lblMilkDescription.Text = clsMilkTypeMaster.getMilkTypeName(txtMilktypeCode.Value, Nothing)
            lblMilkType.Text = clsMilkTypeMaster.getMilkType(txtMilktypeCode.Value, Nothing)
        End If
    End Sub


End Class
