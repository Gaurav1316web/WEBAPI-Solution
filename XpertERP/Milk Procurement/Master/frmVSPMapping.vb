Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Public Class frmVSPMapping
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String

#End Region
    Private Sub frmJWPriceCodeMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update")
        ButtonToolTip.SetToolTip(rdbtndelete, "Press Alt+D  for Delete")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P  for Post ")
        ButtonToolTip.SetToolTip(rdbtnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(rdbtnreset, "Press Alt+N Adding New")
        funReset()
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Save()
    End Sub
    Public Sub Save()
        Try
            If (AllowToSave()) Then
                Dim obj As New clsVSPMapping()
                obj.Code = txtCode.Value
                obj.Start_Date = dtStartDate.Value
                If dtpEndDate.Checked Then
                    obj.End_Date = dtpEndDate.Value
                End If
                obj.Description = txtDescription.Text
                obj.Commission_Code = txtCommission.Value
                obj.Deduction_Code = txtDeduction.Value
                obj.Day_Wise_Incentive_Code = txtDayWiseIncentive.Value
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
            Dim obj As clsVSPMapping = clsVSPMapping.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
                btnsave.Enabled = True
                btnPost.Enabled = True
                isNewEntry = False
                txtCode.Value = obj.Code
                txtDescription.Text = obj.Description
                dtStartDate.Value = obj.Start_Date
                If obj.End_Date IsNot Nothing Then
                    dtpEndDate.Checked = True
                    dtpEndDate.Value = obj.End_Date
                Else
                    dtpEndDate.Checked = False
                End If
                txtCommission.Value = obj.Commission_Code
                txtDeduction.Value = obj.Deduction_Code
                txtDayWiseIncentive.Value = obj.Day_Wise_Incentive_Code
                lblDayWiseIncentive.Text = clsVSSDayWiseIncentive.GetName(txtDayWiseIncentive.Value)
                lblDeduction.Text = clsVSPDeduction.GetName(txtDeduction.Value)
                lblCommission.Text = clsVSPCommission.GetName(txtCommission.Value)
                txtMCC.arrValueMember = obj.ArrMCC
                txtRoute.arrValueMember = obj.ArrRoute
                txtVSP.arrValueMember = obj.ArrVSP
                FillVSPDetails()
                UsLock1.Status = obj.Posted
                chkInactive.Checked = obj.Inactive
                If obj.Posted = ERPTransactionStatus.Approved Then
                    btnsave.Enabled = False
                    btnPost.Enabled = False
                    rdbtndelete.Enabled = False
                    chkInactive.Enabled = Not obj.Inactive
                    If chkInactive.Enabled Then
                        chkInactive.Enabled = MyBase.isPostFlag
                    End If
                    btnUpdates.Enabled = False
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
        'If clsCommon.myLen(txtCode.Value) <= 0 Then
        '    myMessages.blankValue("Code")
        '    txtCode.Focus()
        '    Return False
        'End If
        If clsCommon.myLen(txtDescription.Text) <= 0 Then
            myMessages.blankValue(Me, "Description", Me.Text)
            txtDescription.Focus()
            Return False
        End If
        If clsCommon.myLen(txtCommission.Value) <= 0 AndAlso clsCommon.myLen(txtDeduction.Value) <= 0 AndAlso clsCommon.myLen(txtDayWiseIncentive.Value) <= 0 Then
            Throw New Exception("Please select commission or Deduction or day wise incentive")
        End If
        Return True
    End Function
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtndelete.Click
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
                If (clsVSPMapping.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating

        Dim str As String = "select count(*) from TSPL_VSP_MAPPING where Code ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            txtCode.Value = clsVSPMapping.getFinder("", txtCode.Value, isButtonClicked)
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
        rdbtndelete.Enabled = False
        btnPost.Enabled = False
        btnUpdates.Enabled = False
        chkInactive.Enabled = False
        chkInactive.Checked = False
        UsLock1.Status = ERPTransactionStatus.Pending
        txtCode.Value = Nothing
        txtDescription.Text = Nothing
        txtCommission.Value = Nothing
        lblCommission.Text = Nothing
        txtDeduction.Value = Nothing
        lblDeduction.Text = Nothing
        txtDayWiseIncentive.Value = Nothing
        lblDayWiseIncentive.Text = Nothing
        txtMCC.arrValueMember = Nothing
        txtRoute.arrValueMember = Nothing
        txtVSP.arrValueMember = Nothing
        FillVSPDetails()
        dtpEndDate.Value = clsCommon.GETSERVERDATE()
        dtStartDate.Value = dtpEndDate.Value
    End Sub
    Private Sub frmHSNMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso rdbtnreset.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso rdbtndelete.Enabled Then
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
        btnUpdates.Visible = MyBase.isPostFlag
        btnPost.Visible = MyBase.isPostFlag
        rdbtndelete.Visible = MyBase.isDeleteFlag
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
                        If clsCommon.MyMessageBoxShow("Current code [" + txtCode.Value + "] will be inactive" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, Telerik.WinControls.RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                            If (clsVSPMapping.InactiveData(txtCode.Value)) Then
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
        txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("PCUMCC", qry, "MCC_Code", "MCC_NAME", txtMCC.arrValueMember, Nothing)
        RefreshRoute()
        RefreshVSP()
    End Sub
    Private Sub txtRoute__My_Click(sender As Object, e As EventArgs) Handles txtRoute._My_Click
        Try
            If txtMCC.arrValueMember Is Nothing OrElse txtMCC.arrValueMember.Count <= 0 Then
                txtMCC.Focus()
                Throw New Exception("Please select MCC")
            End If
            Dim qry As String = "select Route_Code,Route_Name,TSPL_MCC_ROUTE_MASTER.MCC_Code,TSPL_MCC_MASTER.MCC_NAME from TSPL_MCC_ROUTE_MASTER left join TSPL_MCC_MASTER on TSPL_MCC_MASTER.mcc_code=TSPL_MCC_ROUTE_MASTER.MCC_Code " &
             " where TSPL_MCC_ROUTE_MASTER.MCC_Code in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")"
            txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("PCURoute", qry, "Route_Code", "Route_Name", txtRoute.arrValueMember, Nothing)
            RefreshVSP()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtVSP__My_Click(sender As Object, e As EventArgs) Handles txtVSP._My_Click
        Try
            If txtRoute.arrValueMember Is Nothing OrElse txtRoute.arrValueMember.Count <= 0 Then
                txtRoute.Focus()
                Throw New Exception("Please select at least route")
            End If
            Dim qry As String = VSPQuery("TSPL_VLC_MASTER_HEAD.Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ") and TSPL_VLC_MASTER_HEAD.Active='1' ")
            txtVSP.arrValueMember = clsCommon.ShowMultipleSelectForm("PCUVLC", qry, "VSP Code", "VSP Name", txtVSP.arrValueMember, Nothing)
            FillVSPDetails()
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


    Sub FillVSPDetails()
        Dim whrcls As String = ""
        If txtVSP.arrValueMember IsNot Nothing AndAlso txtVSP.arrValueMember.Count > 0 Then
            whrcls += "TSPL_VLC_MASTER_HEAD.VSP_Code in (" + clsCommon.GetMulcallString(txtVSP.arrValueMember) + ")"
        Else
            whrcls += " 2=3 "
        End If
        Dim qry As String = VSPQuery(whrcls)
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        gv1.DataSource = dt
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
        Next
        gv1.ShowFilteringRow = True
        gv1.EnableFiltering = True
        gv1.AllowDeleteRow = False
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = true
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.BestFitColumns()

    End Sub
    Sub RefreshRoute()
        If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
            Dim qry As String = "select Route_Code from TSPL_MCC_ROUTE_MASTER where Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")  and MCC_Code in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            txtRoute.arrValueMember = Nothing
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim arr As New ArrayList
                For Each dr As DataRow In dt.Rows
                    arr.Add(clsCommon.myCstr(dr("Route_Code")))
                Next
                txtRoute.arrValueMember = arr
            End If
        End If
    End Sub
    Sub RefreshVSP()
        If txtVSP.arrValueMember IsNot Nothing AndAlso txtVSP.arrValueMember.Count > 0 Then
            Dim qry As String = "select VSP_Code from TSPL_VLC_MASTER_HEAD where VSP_Code in (" + clsCommon.GetMulcallString(txtVSP.arrValueMember) + ")  and Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")  "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            txtVSP.arrValueMember = Nothing
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim arr As New ArrayList
                For Each dr As DataRow In dt.Rows
                    arr.Add(clsCommon.myCstr(dr("VSP_Code")))
                Next
                txtVSP.arrValueMember = arr
            End If
            FillVSPDetails()
        End If
    End Sub
    Sub PostData()
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("Please select document no to post")
            End If
            If (myMessages.postConfirm()) Then
                If (clsVSPMapping.PostData(txtCode.Value)) Then
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
    Private Sub btnUpdates_Click(sender As Object, e As EventArgs) Handles btnUpdates.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("Please select code")
            End If
            If txtMCC.arrValueMember Is Nothing OrElse txtMCC.arrValueMember.Count <= 0 Then
                Throw New Exception("Please select at least one MCC")
            End If
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "PWD"
            frm.strCode = "UserPWD"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                Dim qry As String = "select TSPL_VLC_MASTER_HEAD.VSP_Code,TSPL_VENDOR_MASTER.Vendor_Name as [VSP Name], TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name,VLC_Code_VLC_Uploader as [Uploader Code],TSPL_VLC_MASTER_HEAD.Route_Code,TSPL_MCC_ROUTE_MASTER.Route_Name " + Environment.NewLine +
            " from TSPL_VLC_MASTER_HEAD " + Environment.NewLine +
            " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code " + Environment.NewLine +
            " left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_VLC_MASTER_HEAD.Route_Code " + Environment.NewLine +
            " where TSPL_VLC_MASTER_HEAD.MCC in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") and TSPL_VLC_MASTER_HEAD.Active='1' and len( isnull(TSPL_VLC_MASTER_HEAD.Route_Code,''))>0 " + Environment.NewLine +
            " and not exists (select 1 from  TSPL_VSP_MAPPING_VSP where Code='" + txtCode.Value + "' and TSPL_VSP_MAPPING_VSP.VSP_Code=TSPL_VLC_MASTER_HEAD.VSP_Code)"
                Dim arr As ArrayList = clsCommon.ShowMultipleSelectForm("AddVLCvspm", qry, "VSP_Code", "", Nothing, Nothing)
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    If clsCommon.MyMessageBoxShow("Add " + clsCommon.myCstr(arr.Count) + " VSP in current Mapping." + Environment.NewLine + " Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                        Try
                            clsVSPMappingVSP.SaveData(txtCode.Value, arr, trans)
                            trans.Commit()
                            clsCommon.MyMessageBoxShow(Me, "VSP Added Surressfully", Me.Text)
                            LoadData(txtCode.Value, NavigatorType.Current)
                        Catch ex As Exception
                            trans.Rollback()
                            Throw New Exception(ex.Message)
                        End Try
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCommission__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCommission._MYValidating
        txtCommission.Value = clsVSPCommission.getFinder("", txtCommission.Value, isButtonClicked)
        lblCommission.Text = clsVSPCommission.GetName(txtCommission.Value)
    End Sub

    Private Sub txtDeduction__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDeduction._MYValidating
        txtDeduction.Value = clsVSPDeduction.getFinder("", txtDeduction.Value, isButtonClicked)
        lblDeduction.Text = clsVSPDeduction.GetName(txtDeduction.Value)
    End Sub

    Private Sub txtDayWiseIncentive__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDayWiseIncentive._MYValidating
        txtDayWiseIncentive.Value = clsVSSDayWiseIncentive.getFinder("", txtDayWiseIncentive.Value, isButtonClicked)
        lblDayWiseIncentive.Text = clsVSSDayWiseIncentive.GetName(txtDayWiseIncentive.Value)
    End Sub
End Class