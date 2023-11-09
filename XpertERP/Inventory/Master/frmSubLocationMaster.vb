'==========BM00000002920,Created by Rohit Guupta ,June 26,2014 on 2:17 PM.===============================
Imports common
Imports System.Data.SqlClient

Public Class frmSubLocationMaster
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim Qry As String
    Dim dt As DataTable
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim UserCode, CompanyCode As String
#End Region

    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
#Region "Events"
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.SublocationMaster)
        If Not (MyBase.isReadFlag) Then
            '--------richa Ticket no. BM00000003121 15/07/2014 
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 02/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnsave.Visible = True Then
            desimport.Enabled = True
            desexport.Enabled = True
        Else
            desimport.Enabled = False
            desexport.Enabled = False
        End If
        '--------------------------------------------------
        'btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub frmSubLocationMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        'If Not clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        'txtLocationCode.Value.CharacterCasing = CharacterCasing.Upper
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddnew, "Press Alt+N Adding New Trasnaction")
        fndSubLocid.MyReadOnly = True
        fndSubLocid.MyMaxLength = 30     ''''Validates The Length Of Finder(fndSubLocid)
        txtSubLoc.MaxLength = 200           ''''Validates The Length Of TextBox(txtSubLoc)
        txtLocation.MyReadOnly = True
        lblLoc.BorderVisible = True
        AddNew()
    End Sub

    Private Sub btnAddNew_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddnew.Click
        AddNew()
    End Sub

    Private Sub btnSave_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub btnDelete_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnClose_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        CloseForm()
    End Sub

    Private Sub fndSubLocid__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndSubLocid._MYNavigator
        Try
            LoadData(fndSubLocid.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub

    ''''Finder(fndSubLocid)
    Private Sub fndSubLocid_MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndSubLocid._MYValidating
        Dim str As String = "select count(*) from TSPL_SUB_LOCATION_MASTER where Sub_Location_code ='" + fndSubLocid.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 Then
            fndSubLocid.MyReadOnly = False
        Else
            fndSubLocid.MyReadOnly = True
        End If
        If fndSubLocid.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = "select Sub_Location_code as [SubLocationcode],Location_Code as [LocationCode], Description as [SubLocationName]  from TSPL_SUB_LOCATION_MASTER "
            fndSubLocid.Value = clsCommon.ShowSelectForm("SubLocSelector", qry, "SubLocationcode", "", fndSubLocid.Value, "SubLocationcode", isButtonClicked)
            'FillData()
            LoadData(fndSubLocid.Value, NavigatorType.Current)
        End If
    End Sub

    Public Sub FillData()
        txtLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Code from TSPL_SUB_LOCATION_MASTER where Sub_Location_code='" + fndSubLocid.Value + "'"))
        txtSubLoc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_SUB_LOCATION_MASTER where Sub_Location_code='" + fndSubLocid.Value + "'"))
        lblLoc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_desc from tspl_Item_Category Where TSPL_ITEM_CATEGORY.Location_Code='" + txtLocation.Value + "'"))
        lblLoc.Visible = True
        btnsave.Text = "Update"
        fndSubLocid.MyReadOnly = True
    End Sub

    ''''Finder(txtLocation) 
    Private Sub txtLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtLocation._MYValidating
        Dim qry As String = "select Location_Code as [Code],Location_Desc as [Description] from TSPL_LOCATION_MASTER"
        txtLocation.Value = clsCommon.ShowSelectForm("Location_Code", qry, "Code", "", txtLocation.Value, "Code", isButtonClicked)
        lblLoc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
        lblLoc.Visible = True
    End Sub

    ''''Validates The Finder(Sub Category Code)
    Private Sub fndSubLocid_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles fndSubLocid.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If

    End Sub

    'Private Sub RadMenuItemImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemImport.Click
    '    Import()
    'End Sub

    'Private Sub RadMenuItemExport1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemExport1.Click
    '    Export()
    'End Sub

    Private Sub frmSubLocationMaster1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddnew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            CloseForm()
        End If
    End Sub

#End Region

#Region "Methods"

    Sub BlankAllControls()
        fndSubLocid.Value = Nothing
        txtSubLoc.Text = ""
        txtLocation.Value = ""
        lblLoc.Text = ""
        lblLoc.Visible = False
    End Sub

    Sub AddNew()
        BlankAllControls()
        fndSubLocid.Focus()
        ' fndSubLocid.MyReadOnly = False
        isNewEntry = True
        btnsave.Text = "Save"
        btnsave.Enabled = True
        'btnPost.Enabled = True
        btndelete.Enabled = True
        txtSubLoc.Focus()
    End Sub

    Function AllowToSave() As Boolean

        'If clsCommon.myLen(fndSubLocid.Value) <= 0 Then
        '    common.clsCommon.MyMessageBoxShow("Please Enter Sub Location Code")
        '    fndSubLocid.Focus()
        '    Return False
        'End If

        If clsCommon.myLen(txtLocation.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please Select Location Code")
            txtLocation.Focus()
            Return False
        End If

        If clsCommon.myLen(txtSubLoc.Text) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please Select Sub Location Name")
            txtLocation.Focus()
            Return False
        End If

        Dim dt As DataTable = clsDBFuncationality.GetDataTable("Select * From TSPL_SUB_LOCATION_MASTER Where Sub_Location_code='" + fndSubLocid.Value + "'")
        If dt.Rows.Count > 0 And isNewEntry Then
            common.clsCommon.MyMessageBoxShow(Me, "This Code has been already added")
            fndSubLocid.Focus()
            Return False
        End If

        Return True
    End Function

    Sub SaveData()
        Try
            If (AllowToSave()) Then
                'If MyBase.isModifyonPasswordFlag Then
                '    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.SublocationMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                '    Else
                '        Return
                '    End If
                'End If
                Dim obj As New clsSubLocation()
                obj.Sub_Location_code = (fndSubLocid.Value).ToString
                obj.Description = txtSubLoc.Text
                obj.Location_Code = txtLocation.Value
                If (obj.SaveData(obj, isNewEntry)) And btnsave.Text = "Save" Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully")
                    LoadData(obj.Sub_Location_code, NavigatorType.Current)
                Else
                    common.clsCommon.MyMessageBoxShow(Me, "Record Updated Successfully")
                    LoadData(obj.Sub_Location_code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub

    Sub DeleteData()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsSubLocation.DeleteData(fndSubLocid.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ")
                    AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub

    Sub LoadData(ByVal Sub_Location_code As String, ByVal navType As common.NavigatorType)
        Try
            btnsave.Enabled = True
            'btnPost.Enabled = True
            btndelete.Enabled = True
            isInsideLoadData = True
            isNewEntry = False
            btnsave.Text = "Update"
            BlankAllControls()
            fndSubLocid.MyReadOnly = True

            Dim obj As New clsSubLocation()
            obj = clsSubLocation.GetData(Sub_Location_code, navType)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Location_Code) > 0) Then
                fndSubLocid.Value = obj.Sub_Location_code
                txtSubLoc.Text = obj.Description
                txtLocation.Value = obj.Location_Code
                lblLoc.Text = obj.Location_Name
                lblLoc.Visible = True
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Sub CloseForm()
        Me.Close()
    End Sub

#End Region

#Region "Import/Export"
    Public Sub Import()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Sub Category Code", "Category Code", "Description") Then
            Dim trans As SqlTransaction = Nothing
            Try
                'connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim LineNo As String = clsCommon.myCstr(grow.Index + 2)
                    Dim strSubLocationcode As String = clsCommon.myCstr(grow.Cells(0).Value)
                    Dim strLocationCode As String = clsCommon.myCstr(grow.Cells(1).Value)
                    Dim strDescription As String = clsCommon.myCstr(grow.Cells(2).Value)

                    If (String.IsNullOrEmpty(strDescription)) Or clsCommon.myLen(strSubLocationcode) > 30 Then
                        Throw New Exception("Sub Category Code can not be blank or incorrect at Line No '" + LineNo + "'")
                    End If

                    Dim LocationCode As String
                    If clsCommon.myLen(strLocationCode) > 0 Then
                        LocationCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Code from TSPL_Item_Category Where Location_Code='" + strLocationCode + "'", trans))
                        If Not clsCommon.CompairString(strLocationCode, LocationCode) = CompairStringResult.Equal Then
                            Throw New Exception("Category Code '" + strLocationCode + "' at Line No '" + LineNo + "' Does Not Exist In Master")
                        End If
                    Else
                        Throw New Exception(" Category Code can not be blank at Line No '" + LineNo + "'")
                    End If

                    If (String.IsNullOrEmpty(strDescription)) Or clsCommon.myLen(strDescription) > 200 Then
                        Throw New Exception(" Description can not be blank or incorrect At Line No '" + LineNo + "'")
                    End If

                    Dim sql1 As String = "select count(*) from TSPL_SUB_LOCATION_MASTER where Sub_Location_code='" + strSubLocationcode + "'"
                    Dim i As Integer = CInt(clsDBFuncationality.getSingleValue(sql1, trans))
                    If (i = 0) Then
                        Qry = "INSERT Into TSPL_SUB_LOCATION_MASTER values('" + strSubLocationcode + "','" + strLocationCode + "', '" + strDescription + "','" + UserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy") + "','" + UserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy") + "','" + CompanyCode + "')"
                        clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                        'connectSql.RunSqlTransaction(trans, Qry)
                        'connectSql.RunSpTransaction(trans, "sp_DesignationMaster_insert", New SqlParameter("@Category Code", strCtgryCode), New SqlParameter("@Category Name", strCtgryName), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode))
                    Else
                        Qry = "UPDATE TSPL_SUB_LOCATION_MASTER set Sub_Location_code='" + strSubLocationcode + "', Location_Code='" + strLocationCode + "', Description='" + strDescription + "', Modify_By='" + UserCode + "', Modify_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy") + "', Comp_Code='" + CompanyCode + "' WHERE Sub_Location_code='" + strSubLocationcode + "'"
                        clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                        'connectSql.RunSqlTransaction(trans, Qry)
                        'connectSql.RunSpTransaction(trans, "sp_DesignationMaster_update", New SqlParameter("@CAtegory Code", strCtgryCode), New SqlParameter("@Category Name", strCtgryName), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode))

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


    Public Sub Export()
        Dim str As String
        str = "select TSPL_SUB_LOCATION_MASTER.Sub_Location_code As [Sub Category Code],TSPL_SUB_LOCATION_MASTER.Location_Code as [Category Code], TSPL_SUB_LOCATION_MASTER.Description as [Description] from TSPL_SUB_LOCATION_MASTER"
        transportSql.ExporttoExcel(str, Me)
    End Sub
#End Region


    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        btnSave.Visible = True
    '        btnDelete.Visible = True
    '        'btnPost.Visible = True

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "ITM-SUB-CAT"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete & "," & enuUserRights.enuAuthorised
    '        strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '        strTemp = Split(strRights, ",")
    '        If strTemp(0) = "0" Then
    '            MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    '            funSetUserAccess = False
    '            blnRead = False
    '            Me.Close()
    '            Exit Function
    '        Else
    '            blnRead = True
    '        End If
    '        If strTemp(1) = "0" Then 'Grant modify access
    '            btnSave.Visible = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            btnDelete.Visible = False
    '        End If
    '        If strTemp(3) = "0" Then 'Grant Authorize access
    '            'btnPost.Visible = False
    '        End If
    '        funSetUserAccess = True
    '    Catch er As Exception

    '    End Try
    'End Function

    Private Sub RadMenuItemExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        CloseForm()
    End Sub

End Class
