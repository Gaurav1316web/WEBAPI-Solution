Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class frmCustomerComplaintMaster
    Inherits FrmMainTranScreen
    ' Ticket No :  By Prabhakar 
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
        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)()
        coll.Add("Code", "varchar(30) not null primary Key")
        coll.Add("Description", "varchar(200) null")
        coll.Add("Type", "varchar(30) Not null")
        coll.Add("Created_By", "varchar(12) null")
        coll.Add("Created_Date", "datetime null")
        coll.Add("Modify_By", "varchar(12) null")
        coll.Add("Modify_Date", "datetime null")
        clsCommonFunctionality.CreateOrAlterTable("TSPL_CUSTOMER_COMPLAINT_MASTER", coll)

        coll = New Dictionary(Of String, String)()
        coll.Add("Price_with_Tax", "decimal(18,2) NULL Default 0")
        coll.Add("Amount_with_Tax", "decimal(18,2) NULL Default 0")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_BOOKING_DETAIL", coll, "", False, False)

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
        txtDesc.MaxLength = 200

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
        LoadType()
    End Sub

    Function AllowToSave() As Boolean

        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please Enter Code", Me.Text)
            txtCode.Focus()
            Return False
        End If
        'End If
        If clsCommon.myLen(txtDesc.Text) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please Enter Description", Me.Text)
            txtDesc.Focus()
            Return False
        End If

        If clsCommon.CompairString(cboType.Text, "Select") = CompairStringResult.Equal Then
            common.clsCommon.MyMessageBoxShow(Me, "Please Select Type", Me.Text)
            cboType.Focus()
            Return False
        End If

        If clsCommon.myLen(txtDesc.Text) > 0 Then
            Dim chkDesc As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_CUSTOMER_COMPLAINT_MASTER where Description = '" + txtDesc.Text.Trim() + "' and code <> '" + txtCode.Value + "'"))
            If chkDesc > 0 Then
                common.clsCommon.MyMessageBoxShow("Description Already used another Document.Description should be unique.")
                txtDesc.Text = ""
                txtDesc.Focus()
                Return False
            End If

        End If


        Return True
    End Function

    Private Sub SaveData()
        Try
            If (AllowToSave()) Then

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmCustomerComplaintMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
                Dim arr As New List(Of clsCustomerComplaintMaster)
                Dim obj As New clsCustomerComplaintMaster()
                obj.Code = txtCode.Value
                obj.Description = txtDesc.Text
                obj.Type = cboType.SelectedValue
                arr.Add(obj)
                If obj.SaveData(arr, isNewEntry) Then
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
            Dim obj As New clsCustomerComplaintMaster()
            obj = clsCustomerComplaintMaster.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
                txtCode.Value = obj.Code
                txtDesc.Text = obj.Description
                cboType.SelectedValue = obj.Type
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
            Dim qst As String = "select count(*) from TSPL_CUSTOMER_COMPLAINT_MASTER where Code='" + txtCode.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If
            If txtCode.MyReadOnly OrElse isButtonClicked Then
                LoadData(clsCustomerComplaintMaster.getFinder("", txtCode.Value, isButtonClicked), NavigatorType.Current)
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
                If (clsCustomerComplaintMaster.DeleteData(txtCode.Value)) Then
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

        strDetail = " Select Code As [Code],Description As [Description], Type  From TSPL_CUSTOMER_COMPLAINT_MASTER "
        transportSql.ExporttoExcel(strDetail, Me)
    End Sub
    '' Anubhooti 23-June-2014
    Private Sub rmImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim obj As clsCustomerComplaintMaster = Nothing
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Code", "Description", "Type") Then
            Dim linno As Integer = 0
            Try
                Dim arr As New List(Of clsCustomerComplaintMaster)
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    obj = New clsCustomerComplaintMaster
                    linno += 1
                    Dim strcode As String = clsCommon.myCstr(grow.Cells("Code").Value)
                    If (String.IsNullOrEmpty(strcode)) Or clsCommon.myLen(strcode) > 30 Then
                        Throw New Exception("Length of Code should be max. 30 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Code = strcode

                    Dim strDesp As String = clsCommon.myCstr(grow.Cells("Description").Value)
                    If (String.IsNullOrEmpty(strDesp)) Or clsCommon.myLen(strDesp) > 200 Then
                        Throw New Exception("Length of Description should be max. 200 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    If clsCommon.myLen(strDesp) > 0 Then
                        Dim chkDesc As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_CUSTOMER_COMPLAINT_MASTER where Description = '" + strDesp + "' and code <> '" + txtCode.Value + "' "))
                        If chkDesc > 0 Then
                            Throw New Exception("Description Already used another slot.Description should be unique." + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    obj.Description = strDesp

                    Dim strType As String = clsCommon.myCstr(grow.Cells("Type").Value)
                    If (String.IsNullOrEmpty(strType)) Then
                        Throw New Exception("Type Can not be Blank. At Line No. " + clsCommon.myCstr(linno) + ".")
                    ElseIf clsCommon.CompairString("Quailty", strType) = CompairStringResult.Equal Then
                    ElseIf clsCommon.CompairString("Service", strType) = CompairStringResult.Equal Then
                    Else
                        Throw New Exception("Type Should be Quailty or Service . At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If

                    If clsCommon.myLen(strcode) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from TSPL_CUSTOMER_COMPLAINT_MASTER  where Code='" + strcode + "' ") > 0 Then
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

    Sub LoadType()

        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()

        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Quailty"
        dr("Name") = "Quailty"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Service"
        dr("Name") = "Service "
        dt.Rows.Add(dr)

        cboType.DataSource = dt
        cboType.ValueMember = "Code"
        cboType.DisplayMember = "Name"
        cboType.SelectedIndex = 0
    End Sub


End Class

