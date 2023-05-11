Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class frmSavingsMaster
    Inherits FrmMainTranScreen
    ' Ticket No : BHA/31/12/18-000768 By Prabhakar Create New screen 
#Region "Variables"
    Private isNewEntry As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
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
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        AddNew()
        SetLength()
    End Sub

    Sub SetLength()
        txtCode.MyMaxLength = 30
        txtDesc.MaxLength = 100

    End Sub

   

    Sub AddNew()
        txtCode.MyReadOnly = False
        BlankAllControls()
        isNewEntry = True
        btnSave.Text = "Save"
    End Sub

    Sub BlankAllControls()
        txtCode.Value = ""
        txtDesc.Text = ""
        txtSectionCode.Value = ""
        lblSectionCode.Text = ""
    End Sub

    Function AllowToSave() As Boolean

        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please Enter Code")
            txtCode.Focus()
            Return False
        End If

        If clsCommon.myLen(txtDesc.Text) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please Enter Description")
            txtDesc.Focus()
            Return False
        End If

        If clsCommon.myLen(txtSectionCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select Section Code")
            txtSectionCode.Focus()
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
                Dim arr As New List(Of clsSavingMaster)

                Dim obj As New clsSavingMaster()
                obj.SAVINGS_CODE = txtCode.Value
                obj.Description = txtDesc.Text
                obj.Section_Code = txtSectionCode.Value
                arr.Add(obj)
                If obj.SaveData(arr, isNewEntry) Then
                    clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    LoadData(obj.SAVINGS_CODE, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            isNewEntry = False

            BlankAllControls()

            Dim obj As New clsSavingMaster()
            obj = clsSavingMaster.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.SAVINGS_CODE) > 0) Then
                txtCode.Value = obj.SAVINGS_CODE
                txtDesc.Text = obj.Description
                txtSectionCode.Value = obj.Section_Code
                lblSectionCode.Text = obj.Section_Desc
                btnSave.Text = "Update"
                txtCode.MyReadOnly = True
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
            Dim qst As String = "select count(*) from TSPL_SAVINGS_MASTER where SAVINGS_CODE='" + txtCode.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If
            If txtCode.MyReadOnly OrElse isButtonClicked Then

               
                LoadData(clsSavingMaster.getFinder("", txtCode.Value, isButtonClicked), NavigatorType.Current)
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
                If (clsSavingMaster.DeleteData(txtCode.Value)) Then
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
            SaveData()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    Private Sub rmExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmExport.Click
        Dim strDetail As String
        strDetail = " select TSPL_SAVINGS_MASTER.SAVINGS_CODE as [Savings Code] ,TSPL_SAVINGS_MASTER.Description as [Description] ,TSPL_SAVINGS_MASTER.Section_Code as [Section Code],TSPL_SECTION_ALLOWANCE_MASTER.Description as [Section Desc]  From TSPL_SAVINGS_MASTER left join TSPL_SECTION_ALLOWANCE_MASTER on TSPL_SECTION_ALLOWANCE_MASTER.Code=TSPL_SAVINGS_MASTER.Section_Code "
        transportSql.ExporttoExcel(strDetail, Me)
    End Sub

    Private Sub rmImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim obj As clsSavingMaster = Nothing
        Dim currentdate As Date = Date.Today
        Dim trans As SqlTransaction = Nothing
        If transportSql.importExcel(gv, "Savings Code", "Description", "Section Code", "Section Desc") Then
            Dim linno As Integer = 0
            Try
                Dim arr As New List(Of clsSavingMaster)
                clsCommon.ProgressBarShow()
                trans = clsDBFuncationality.GetTransactin()
                For Each grow As GridViewRowInfo In gv.Rows
                    obj = New clsSavingMaster
                    linno += 1
                    Dim strcode As String = clsCommon.myCstr(grow.Cells("Savings Code").Value)
                    If (String.IsNullOrEmpty(strcode)) Or clsCommon.myLen(strcode) > 30 Then
                        Throw New Exception("Length of Code should be max. 30 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.SAVINGS_CODE = strcode

                    Dim strDesp As String = clsCommon.myCstr(grow.Cells("Description").Value)
                    If (String.IsNullOrEmpty(strDesp)) Or clsCommon.myLen(strDesp) > 100 Then
                        Throw New Exception("Length of Description should be max. 100 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Description = strDesp

                    

                    Dim strType As String = clsCommon.myCstr(grow.Cells("Section Code").Value)

                    Dim strSectionCode As Boolean = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select count(*) from TSPL_SECTION_ALLOWANCE_MASTER where Code ='" + strType + "'  ", trans))
                    If strSectionCode = False Then
                        Throw New Exception("Invalid Section Code. At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Section_Code = strType

                    
                    If clsCommon.myLen(strcode) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from TSPL_SAVINGS_MASTER  where SAVINGS_CODE='" + strcode + "' ", trans) > 0 Then
                        isNewEntry = False
                    Else
                        isNewEntry = True
                    End If
                    arr.Add(obj)
                    obj.SaveData(arr, isNewEntry, trans)
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

   
    Private Sub txtSectionCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtSectionCode._MYValidating
        Dim qry As String = "select  Code as Code, TSPL_SECTION_ALLOWANCE_MASTER.Description from TSPL_SECTION_ALLOWANCE_MASTER"
        txtSectionCode.Value = clsCommon.ShowSelectForm("Sectionallownce@Finder", qry, "Code", "Type = 'S'", txtSectionCode.Value, "", isButtonClicked)
        lblSectionCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Description from TSPL_SECTION_ALLOWANCE_MASTER where Code='" + txtSectionCode.Value + "'"))
    End Sub
End Class

