Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class frmSectionAllowanceMaster
    Inherits FrmMainTranScreen
    ' Ticket No : BHA/31/12/18-000767 By Prabhakar Create New screen 
#Region "Variables"
    Private isNewEntry As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isLoadData As Boolean = False
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
        isLoadData = True
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        AddNew()
        SetLength()
        isLoadData = False
    End Sub

    Sub SetLength()
        txtCode.MyMaxLength = 30
        txtDesc.MaxLength = 200

    End Sub



    Sub AddNew()
        txtCode.MyReadOnly = False
        BlankAllControls()
        isNewEntry = True
        btnSave.Text = "Save"
        cboType.Enabled = True
        txtPayheadCode.Visible = False
        lblPayheadCode.Visible = False
        MyLabel4.Visible = False
        txtCode.MyReadOnly = True
    End Sub

    Sub BlankAllControls()
        txtCode.Value = ""
        txtDesc.Text = ""
        txtPayheadCode.Value = ""
        lblPayheadCode.Text = ""
        txtMaxLimit.Text = ""
        LoadType()
    End Sub

    Function AllowToSave() As Boolean

        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please Enter Code", Me.Text)
            txtCode.Focus()
            Return False
        End If

        If clsCommon.myLen(txtDesc.Text) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please Enter Description", Me.Text)
            txtDesc.Focus()
            Return False
        End If

        If String.IsNullOrEmpty(txtMaxLimit.Text) = True Then
            common.clsCommon.MyMessageBoxShow(Me, "Please Enter Max Limit Value", Me.Text)
            txtMaxLimit.Focus()
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
                Dim arr As New List(Of clsSectionAllowanceMaster)

                Dim obj As New clsSectionAllowanceMaster()
                obj.CODE = txtCode.Value
                obj.Description = txtDesc.Text
                obj.Type = cboType.SelectedValue
                obj.MAX_LIMIT = txtMaxLimit.Text
                arr.Add(obj)
                If obj.SaveData(arr, isNewEntry) Then
                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(obj.CODE, NavigatorType.Current)
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
            txtPayheadCode.Visible = False
            lblPayheadCode.Visible = False
            MyLabel4.Visible = False
            Dim obj As New clsSectionAllowanceMaster()
            obj = clsSectionAllowanceMaster.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.CODE) > 0) Then
                txtCode.Value = obj.CODE
                txtDesc.Text = obj.Description
                cboType.SelectedValue = obj.Type
                txtMaxLimit.Text = obj.MAX_LIMIT
                cboType.Enabled = False
                txtCode.MyReadOnly = True
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
            Dim qst As String = "select count(*) from TSPL_SECTION_ALLOWANCE_MASTER where CODE='" + txtCode.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If
            If txtCode.MyReadOnly OrElse isButtonClicked Then


                LoadData(clsSectionAllowanceMaster.getFinder("", txtCode.Value, isButtonClicked), NavigatorType.Current)
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
                If (clsSectionAllowanceMaster.DeleteData(txtCode.Value)) Then
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
        Dim strDetail As String
        strDetail = " select TSPL_SECTION_ALLOWANCE_MASTER.CODE as [Code] ,TSPL_SECTION_ALLOWANCE_MASTER.Description as [Description] ,  TSPL_SECTION_ALLOWANCE_MASTER.Type  as [Type],convert (varchar, TSPL_SECTION_ALLOWANCE_MASTER.MAX_LIMIT ) as [MAX Limit] From TSPL_SECTION_ALLOWANCE_MASTER  "
        transportSql.ExporttoExcel(strDetail, Me)
    End Sub

    Private Sub rmImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim obj As clsSectionAllowanceMaster = Nothing
        Dim currentdate As Date = Date.Today
        Dim trans As SqlTransaction = Nothing
        If transportSql.importExcel(gv, "Code", "Description", "Type", "MAX Limit") Then
            Dim linno As Integer = 0
            Try
                Dim arr As New List(Of clsSectionAllowanceMaster)
                clsCommon.ProgressBarShow()
                trans = clsDBFuncationality.GetTransactin()
                For Each grow As GridViewRowInfo In gv.Rows
                    obj = New clsSectionAllowanceMaster
                    linno += 1
                    Dim strcode As String = clsCommon.myCstr(grow.Cells("Code").Value)
                    If (String.IsNullOrEmpty(strcode)) Or clsCommon.myLen(strcode) > 30 Then
                        Throw New Exception("Length of Code should be max. 30 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.CODE = strcode

                    Dim strDesp As String = clsCommon.myCstr(grow.Cells("Description").Value)
                    If (String.IsNullOrEmpty(strDesp)) Or clsCommon.myLen(strDesp) > 100 Then
                        Throw New Exception("Length of Description should be max. 100 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Description = strDesp
                    Dim strType As String = ""
                    Dim isPayHeadcode As String = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select Count (*) from TSPL_PAYHEAD_MASTER  where PAY_HEAD_CODE = '" + obj.CODE + "'", trans))
                    If isPayHeadcode = True Then
                        strType = "A"
                    Else
                        strType = "S"
                    End If
                    obj.Type = strType
                    If String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells("MAX Limit").Value)) = False Then
                        If clsCommon.myCdbl(grow.Cells("MAX Limit").Value) Then
                        Else
                            Throw New Exception("Max Limit Should be Numeric At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If
                    Else
                        Throw New Exception("Max Limit Can not be Blank At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If

                    Dim strMaxLimit As Double = clsCommon.myCdbl(grow.Cells("MAX Limit").Value)
                    obj.MAX_LIMIT = strMaxLimit


                    If clsCommon.myLen(strcode) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from TSPL_SECTION_ALLOWANCE_MASTER  where CODE='" + strcode + "' ", trans) > 0 Then
                        isNewEntry = False
                    Else
                        isNewEntry = True
                    End If
                    arr.Add(obj)
                    obj.SaveData(arr, isNewEntry, trans)
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


   

    Sub LoadType()

        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()

        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "S"
        dr("Name") = "Section"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "A"
        dr("Name") = "Allowances"
        dt.Rows.Add(dr)

        cboType.DataSource = dt
        cboType.ValueMember = "Code"
        cboType.DisplayMember = "Name"
        cboType.SelectedIndex = 0
    End Sub
    
    Private Sub txtPayheadCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtPayheadCode._MYValidating
        Dim qry As String = "select PAY_HEAD_CODE as Code , PAY_HEAD_NAME as Name from TSPL_PAYHEAD_MASTER "
        txtPayheadCode.Value = clsCommon.ShowSelectForm("PayHeadCode@Finder", qry, "Code", " IsTDSExempted = 1 and PAY_HEAD_CODE not in ( Select CODE from TSPL_SECTION_ALLOWANCE_MASTER )", txtPayheadCode.Value, "", isButtonClicked)
        lblPayheadCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  PAY_HEAD_NAME from TSPL_PAYHEAD_MASTER where PAY_HEAD_CODE='" + txtPayheadCode.Value + "'"))
        txtCode.Value = txtPayheadCode.Value
        txtDesc.Text = lblPayheadCode.Text
    End Sub

    Private Sub cboType_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboType.SelectedValueChanged
        If isLoadData = False Then
            If clsCommon.CompairString(cboType.SelectedValue, "A") = CompairStringResult.Equal Then
                If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                    txtPayheadCode.Visible = True
                    lblPayheadCode.Visible = True
                    MyLabel4.Visible = True
                Else
                    txtPayheadCode.Visible = False
                    lblPayheadCode.Visible = False
                    MyLabel4.Visible = False
                End If
                txtCode.MyReadOnly = True
                
            Else
                If clsCommon.CompairString(txtCode.Value, "Select") = CompairStringResult.Equal Then
                    txtCode.MyReadOnly = True
                End If
                txtCode.MyReadOnly = False
                txtPayheadCode.Visible = False
                lblPayheadCode.Visible = False
                MyLabel4.Visible = False
            End If
        End If
    End Sub
End Class

