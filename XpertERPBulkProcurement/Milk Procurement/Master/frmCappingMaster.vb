Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Public Class frmCappingMaster
    Inherits FrmMainTranScreen

#Region "Variable"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
#End Region

    Private Sub frmJWPriceCodeMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadShift()
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P  for Post ")
        ButtonToolTip.SetToolTip(rdbtnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(rdbtnreset, "Press Alt+N Adding New")
        funReset()
    End Sub

    Sub LoadShift()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select..."
        dt.Rows.Add(dr)


        dr = dt.NewRow()
        dr("Code") = "M"
        dr("Name") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "E"
        dr("Name") = "Evening"
        dt.Rows.Add(dr)

        isInsideLoadData = True

        cboStartShift.DataSource = dt.Copy()
        cboStartShift.ValueMember = "Code"
        cboStartShift.DisplayMember = "Name"

        cboEndShift.DataSource = dt.Copy()
        cboEndShift.ValueMember = "Code"
        cboEndShift.DisplayMember = "Name"

        isInsideLoadData = False
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Save()
    End Sub
    Public Sub Save()
        Try
            If (AllowToSave()) Then
                Dim obj As New clsCapping()
                obj.Code = txtCode.Value
                obj.Start_Date = dtStartDate.Value
                obj.Start_Shift = cboStartShift.SelectedValue
                If dtpEndDate.Checked Then
                    obj.End_Date = dtpEndDate.Value
                    obj.End_Shift = cboEndShift.SelectedValue
                End If
                obj.FAT = txtFat.Value
                obj.SNF = txtSNF.Value
                obj.Description = txtDescription.Text
                obj.ArrMCC = txtMCC.arrValueMember
                If (obj.ArrMCC Is Nothing OrElse obj.ArrMCC.Count <= 0) Then
                    Throw New Exception("Please Fill at least one MCC")
                End If
                obj.ArrVSP = txtVSP.arrValueMember
                If (obj.ArrVSP Is Nothing OrElse obj.ArrVSP.Count <= 0) Then
                    Throw New Exception("Please Fill at least one VSP")
                End If
                If obj.SaveData(obj, isNewEntry) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(obj.Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            isInsideLoadData = True
            funReset()
            Dim obj As clsCapping = clsCapping.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
                btnsave.Enabled = True
                btnPost.Enabled = True
                isNewEntry = False
                txtCode.Value = obj.Code
                txtDescription.Text = obj.Description
                dtStartDate.Value = obj.Start_Date
                cboStartShift.SelectedValue = obj.Start_Shift
                If obj.End_Date IsNot Nothing Then
                    dtpEndDate.Checked = True
                    dtpEndDate.Value = obj.End_Date
                    cboEndShift.SelectedValue = obj.End_Shift
                Else
                    dtpEndDate.Checked = False
                End If
                txtFat.Value = obj.FAT
                txtSNF.Value = obj.SNF
                txtMCC.arrValueMember = obj.ArrMCC
                txtVSP.arrValueMember = obj.ArrVSP
                UsLock1.Status = obj.Posted
                chkInactive.Checked = obj.Inactive
                If obj.Posted = ERPTransactionStatus.Approved Then
                    btnsave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    chkInactive.Enabled = Not obj.Inactive
                    If chkInactive.Enabled Then
                        chkInactive.Enabled = MyBase.isPostFlag
                    End If

                    btnEndDate.Enabled = Not dtpEndDate.Checked
                    If btnEndDate.Enabled Then
                        btnEndDate.Enabled = MyBase.isPostFlag
                    End If
                End If
                btnsave.Text = "Update"
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    Function AllowToSave() As Boolean
        If txtFat.Value <= 0 Then
            myMessages.blankValue("FAT")
            txtFat.Focus()
            Return False
        End If
        If txtSNF.Value <= 0 Then
            myMessages.blankValue("SNF")
            txtSNF.Focus()
            Return False
        End If
        If clsCommon.myLen(txtDescription.Text) <= 0 Then
            myMessages.blankValue("Description")
            txtDescription.Focus()
            Return False
        End If
        If clsCommon.myLen(cboStartShift.SelectedValue) <= 0 Then
            myMessages.blankValue("Start Shift")
            cboStartShift.Focus()
            Return False
        End If
        If dtpEndDate.Checked Then
            If clsCommon.myLen(cboEndShift.SelectedValue) <= 0 Then
                myMessages.blankValue("End Shift")
                cboEndShift.Focus()
                Return False
            End If
        End If
        Return True
    End Function
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "You Cannot Delete Record", Me.Text)
            Exit Sub
        End If
        funDelete()
    End Sub
    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsCapping.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating

        Dim str As String = "select count(*) from TSPL_CAPPING where Code ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            txtCode.Value = clsCapping.getFinder("", txtCode.Value, isButtonClicked)
            If txtCode.Value <> "" Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                funReset()
            End If
        End If
    End Sub
    Private Sub txtCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub funReset()
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Focus()
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btnDelete.Enabled = True
        btnPost.Enabled = False
        chkInactive.Enabled = False
        chkInactive.Checked = False
        btnEndDate.Enabled = False
        cboStartShift.SelectedValue = ""
        cboEndShift.SelectedValue = ""
        txtFat.Text = ""
        txtSNF.Text = ""
        txtCode.Value = Nothing
        txtDescription.Text = Nothing
        txtMCC.arrValueMember = Nothing
        txtVSP.arrValueMember = Nothing
        dtpEndDate.Value = clsCommon.GETSERVERDATE()
        dtStartDate.Value = dtpEndDate.Value
        UsLock1.Status = ERPTransactionStatus.Pending
    End Sub
    Private Sub frmHSNMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso rdbtnreset.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        End If
    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub rdbtnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnclose.Click
        Me.Close()
    End Sub
    Private Sub rdbtnreset_Click(sender As Object, e As EventArgs) Handles rdbtnreset.Click
        funReset()
    End Sub
    Private Sub chkInactive_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkInactive.ToggleStateChanged
        Try
            If Not isInsideLoadData Then
                If chkInactive.Checked Then
                    If clsCommon.myLen(txtCode.Value) > 0 Then
                        If clsCommon.MyMessageBoxShow(Me, "Current code [" + txtCode.Value + "] will be inactive" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, Telerik.WinControls.RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                            If (clsCapping.InactiveData(txtCode.Value)) Then
                                clsCommon.MyMessageBoxShow(Me, "Successfully Inactivated", Me.Text)
                            End If
                        End If
                    End If
                End If
                LoadData(txtCode.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtMCC__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
        Dim qry As String = "select MCC_Code,MCC_NAME from TSPL_MCC_MASTER"
        txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("CAPMCC", qry, "MCC_Code", "MCC_NAME", txtMCC.arrValueMember, Nothing)
        RefreshVSP()
    End Sub

    Sub PostData()
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("Please select document no to post")
            End If
            If (myMessages.postConfirm()) Then
                If (clsCapping.PostData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Private Sub btnEndDate_Click(sender As Object, e As EventArgs) Handles btnEndDate.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("Document No not found to update")
            End If
            If dtpEndDate.Checked Then
                If clsCommon.myLen(cboEndShift.SelectedValue) <= 0 Then
                    cboEndShift.Focus()
                    Throw New Exception("Please select End Shift")
                End If
            End If
            Dim obj As New clsCapping()
            obj.Code = txtCode.Value
            obj.End_Date = dtpEndDate.Value
            obj.End_Shift = clsCommon.myCstr(cboEndShift.SelectedValue)
            obj.SaveEndDateData(obj)
            clsCommon.MyMessageBoxShow(Me, "Successfully Updated", Me.Text)
            LoadData(txtCode.Value, NavigatorType.Current)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Try
            txtCode.Value = clsCapping.getFinder("", txtCode.Value, True)
            If clsCommon.myLen(txtCode.Value) > 0 Then
                LoadData(txtCode.Value, NavigatorType.Current)
                UsLock1.Status = ERPTransactionStatus.Pending
                isNewEntry = True
                txtCode.Value = ""
                btnDelete.Enabled = False
                btnPost.Enabled = False
                btnsave.Enabled = True
                btnsave.Text = "Save"
            Else
                Reset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtVSP__My_Click(sender As Object, e As EventArgs) Handles txtVSP._My_Click
        Try
            Dim qry As String = VSPQuery(" TSPL_VLC_MASTER_HEAD.Active='1' ")
            txtVSP.arrValueMember = clsCommon.ShowMultipleSelectForm("PCUVLC@c", qry, "VSP Code", "VSP Name", txtVSP.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Function VSPQuery(ByVal Whr As String) As String
        Dim qry As String = "select TSPL_VLC_MASTER_HEAD.VSP_Code as [VSP Code],TSPL_VENDOR_MASTER.Vendor_Name as [VSP Name],TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [Uploader Code],TSPL_VLC_MASTER_HEAD.VLC_Code as [VLC Code],TSPL_VLC_MASTER_HEAD.VLC_Name as [VLC],TSPL_VENDOR_MASTER.Vendor_Group_Code as [Group Code],TSPL_VENDOR_GROUP.Group_Desc as [Group],TSPL_VLC_MASTER_HEAD.Route_Code as [Route Code],TSPL_MCC_ROUTE_MASTER.Route_Name as [Route],TSPL_VLC_MASTER_HEAD.MCC as [MCC Code],TSPL_MCC_MASTER.MCC_NAME as [MCC] from TSPL_VLC_MASTER_HEAD left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_VLC_MASTER_HEAD.Route_Code left join TSPL_MCC_MASTER on TSPL_MCC_MASTER.mcc_code=TSPL_VLC_MASTER_HEAD.MCC left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code left outer join TSPL_VENDOR_GROUP on TSPL_VENDOR_GROUP.Ven_Group_Code=TSPL_VENDOR_MASTER.Vendor_Group_Code where 2=2"
        If clsCommon.myLen(Whr) Then
            qry += " and  " + Whr
        End If
        Return qry
    End Function
    Sub RefreshVSP()
        If txtVSP.arrValueMember IsNot Nothing AndAlso txtVSP.arrValueMember.Count > 0 Then
            Dim qry As String = "select VSP_Code from TSPL_VLC_MASTER_HEAD where VSP_Code in (" + clsCommon.GetMulcallString(txtVSP.arrValueMember) + ") "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            txtVSP.arrValueMember = Nothing
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim arr As New ArrayList
                For Each dr As DataRow In dt.Rows
                    arr.Add(clsCommon.myCstr(dr("VSP_Code")))
                Next
                txtVSP.arrValueMember = arr
            End If
        End If
    End Sub
End Class