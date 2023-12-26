Imports common
Imports System.Data.SqlClient
Public Class FrmProvisionEntry
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub rbtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        clsERPFuncationality.closeForm(Me)
        'clsERPFuncationality.ShowAlert("Closing Done")
    End Sub
    Sub Reset()
        fndLocation.Enabled = True
        fndDocNo.Value = ""
        fndDocNo.MyReadOnly = False
        dtpDate.Value = clsCommon.GETSERVERDATE()
        fndLocation.Value = clsGateEntry.getUsersDefaultLocation()
        txtLocationName.Text = clsCommon.myCstr(clsLocation.GetName(fndLocation.Value, Nothing))
        ddlVendorType.DataSource = clsProvisionEntry.getVendorType()
        ddlVendorType.DisplayMember = "value"
        ddlVendorType.ValueMember = "value"
        ddlVendorType.SelectedIndex = 0
        fndVendor.Value = ""
        txtVendorNAme.Text = ""
        ddlProvType.DataSource = clsProvisionEntry.getProvisionType()
        ddlProvType.DisplayMember = "value"
        ddlProvType.ValueMember = "value"
        ddlProvType.SelectedIndex = 0
        ddlProvMonth.DataSource = clsProvisionEntry.LoadMonthName()
        ddlProvMonth.ValueMember = "monthNumber"
        ddlProvMonth.DisplayMember = "Monthname"
        ddlProvMonth.SelectedIndex = 0
        dtpProvYear.Value = clsCommon.GETSERVERDATE()
        txtProvAmount.Text = ""
        btnSave.Text = "Save"
        btnDelete.Enabled = False
        btnPost.Enabled = False
        btnSave.Enabled = True
        btnClose.Enabled = True
        btnReverse.Visible = False
        GroupBox1.Visible = False
        lblPending.Status = ERPTransactionStatus.Pending
        'LoadBlankProvisionGrid()
        RadPageView1.SelectedPage = RadPageView1.Pages("RadPageViewPage1")
        TxtMultiSelectProvision.arrValueMember = Nothing
        txtRouteCode.Value = ""
        txtRouteName.Text = ""
    End Sub

    'Sub LoadBlankProvisionGrid()
    '    ' gv.Rows.Clear()
    '    'gv.Columns.Clear()
    '    gv.DataSource = Nothing
    'End Sub

    'Sub LoadProvisionGridData(Optional ByVal trans As SqlTransaction = Nothing)
    '    'Dim dt As DataTable = Nothing
    '    'Dim qry As String = String.Empty
    '    'Dim whrCls As String = String.Empty
    '    'If clsCommon.myLen(fndVendor.Value) > 0 Then
    '    '    whrCls = "  where vendor_code='" & fndVendor.Value & "' and prov_month=" & ddlProvMonth.SelectedValue & " and prov_Year=" & Year(dtpProvYear.Value)
    '    'End If
    '    'qry = " select TSPL_PROVISION_ENTRY.Doc_No as [Doc No] ,convert(varchar,TSPL_PROVISION_ENTRY.Doc_Date,103) as [Doc Date] ,TSPL_PROVISION_ENTRY.Vendor_Code as [Vendor Code] ,TSPL_PROVISION_ENTRY.Vendor_Desc as [Vendor Desc] ,TSPL_PROVISION_ENTRY.Vendor_Type as [Vendor Type] ,TSPL_PROVISION_ENTRY.Status as [Status] ,TSPL_PROVISION_ENTRY.Ref_Doc_No as [Ref Doc No] ,TSPL_PROVISION_ENTRY.Prov_type as [Prov Type] ,TSPL_PROVISION_ENTRY.Amount as [Amount] ,TSPL_PROVISION_ENTRY.Prog_Code as [Prog Code] ,TSPL_PROVISION_ENTRY.Prov_Month as [Prov Month] ,TSPL_PROVISION_ENTRY.Prov_Year as [Prov Year] ,TSPL_PROVISION_ENTRY.Comp_Code as [Comp Code] ,TSPL_PROVISION_ENTRY.Created_By as [Created By] ,TSPL_PROVISION_ENTRY.Created_Date as [Created Date] ,TSPL_PROVISION_ENTRY.Modified_By as [Modified By] ,TSPL_PROVISION_ENTRY.Modified_Date as [Modified Date] ,TSPL_PROVISION_ENTRY.isPosted as [Isposted] ,TSPL_PROVISION_ENTRY.Posting_Date as [Posting Date] ,TSPL_PROVISION_ENTRY.Loc_Code as [Loc Code] ,TSPL_PROVISION_ENTRY.Loc_Desc as [Loc Desc] ,TSPL_PROVISION_ENTRY.Status_Update_Doc_No as [Status Update Doc No] ,TSPL_PROVISION_ENTRY.Route_Code as [Route Code]  From TSPL_PROVISION_ENTRY   " & whrCls
    '    'dt = clsDBFuncationality.GetDataTable(qry, trans)
    '    'gv.DataSource = dt
    '    'For i As Integer = 0 To gv.Columns.Count - 1
    '    '    gv.Columns(i).IsVisible = False
    '    'Next
    '    'gv.Columns("Doc No").HeaderText = "Document No"
    '    'gv.Columns("Doc_No").Width = 150
    '    'gv.Columns("Doc No").IsVisible = True

    '    'gv.Columns("Doc Date").HeaderText = "Document Date"
    '    'gv.Columns("Doc Date").Width = 150
    '    'gv.Columns("Doc Date").IsVisible = True

    '    'gv.Columns("Vendor Code").HeaderText = "Vendor Code"
    '    'gv.Columns("Vendor Code").Width = 150
    '    'gv.Columns("Vendor Code").IsVisible = True

    '    'gv.Columns("Vendor Desc").HeaderText = "Vendor Desc"
    '    'gv.Columns("Vendor Desc").Width = 250
    '    'gv.Columns("Vendor Desc").IsVisible = True

    '    'gv.Columns("Vendor type").HeaderText = "Vendor Type"
    '    'gv.Columns("Vendor type").Width = 200
    '    'gv.Columns("Vendor type").IsVisible = True

    '    'gv.Columns("Status").HeaderText = "Status"
    '    'gv.Columns("Status").Width = 100
    '    'gv.Columns("Status").IsVisible = True

    '    'gv.Columns("Ref Doc No").HeaderText = "Ref Doc No"
    '    'gv.Columns("Ref Doc No").Width = 100
    '    'gv.Columns("Ref Doc No").IsVisible = True


    '    'gv.Columns("Prov Type").HeaderText = "Prov Type"
    '    'gv.Columns("Prov Type").Width = 100
    '    'gv.Columns("Prov Type").IsVisible = True

    '    'gv.Columns("Amount").HeaderText = "Amount"
    '    'gv.Columns("Amount").Width = 150
    '    'gv.Columns("Amount").IsVisible = True

    'End Sub

    Private Sub fndLocation__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndLocation._MYValidating
        Dim qry As String = String.Empty
        Dim whrCls As String = ""
        If Not clsMccMaster.isCurrentUserHO(Nothing) Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrCls = " location_code in (" & objCommonVar.strCurrUserLocations & " )"
            End If
        End If
        fndLocation.Value = clsCommon.myCstr(clsLocation.getFinder(whrCls, fndLocation.Value, isButtonClicked))
        txtLocationName.Text = clsCommon.myCstr(clsLocation.GetName(fndLocation.Value, Nothing))
    End Sub

    Private Sub FrmProvisionEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnReset.Enabled Then
            btnReset.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            btnSave.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled AndAlso MyBase.isPostFlag Then
            btnPost.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled AndAlso MyBase.isDeleteFlag Then
            btnDelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            btnClose.PerformClick()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "SIRC"
            frm.strCode = "SIReversAndCreate"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnReverse.Visible = True
                GroupBox1.Visible = True
            End If
        End If

    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmProvisionEntry)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        If MyBase.isReverse Then
            btnReverse.Enabled = True
            GroupBox1.Enabled = True
        Else
            btnReverse.Enabled = False
            GroupBox1.Enabled = False
        End If
    End Sub

    Private Sub FrmProvisionEntry_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        Reset()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D To Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C To Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N For New")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P to Post the Transaction")
        RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Collapsed
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub
    Function AllowToSave() As Boolean
        Try
            '= KUNAL > TICKET : BM00000009575 =====
           
            If AllowFutureDateTransaction(dtpDate.Value, Nothing) = False Then
                dtpDate.Focus()
                Return False

            End If

            If clsCommon.myLen(fndLocation.Value) <= 0 Then
                Throw New Exception("Please select Location")
                Return False
            Else
                Return True
            End If
            If clsCommon.myLen(ddlProvType.Text) = 0 Then
                Throw New Exception("Please Select Provision Type")
                Return False
            Else
                Return True
            End If
            If clsCommon.myLen(ddlVendorType.Text) = 0 Then
                Throw New Exception("Please Select Vendor Type")
                Return False
            Else
                Return True
            End If
            If clsCommon.myLen(ddlProvMonth.Text) = 0 Then
                Throw New Exception("Please Select Provision Month")
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try

    End Function
    Sub deleteData()
        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(fndDocNo.Value) > 0 Then
                If clsCommon.MyMessageBoxShow("Want To Delete The Doc No : " & fndDocNo.Value & " ?", "Confirm", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    If clsProvisionEntry.deleteData(fndDocNo.Value, tran) Then
                        tran.Commit()
                        clsCommon.MyMessageBoxShow(Me, "Deleted successFully", Me.Text)
                        Reset()
                    End If
                End If
            Else
                Throw New Exception("Doc No not Found to delete")
            End If
        Catch ex As Exception
            tran.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        deleteData()
    End Sub

    Private Sub fndVendor__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndVendor._MYValidating
        Try
            Dim whrCls As String = String.Empty
            If clsCommon.CompairString(ddlVendorType.Text, "Secondary Transporter") = CompairStringResult.Equal OrElse clsCommon.CompairString(ddlVendorType.Text, "Transporter For Bulk Sale") = CompairStringResult.Equal Then
                whrCls = "  Form_type='TTM' and Status='N' "
                fndVendor.Value = clsCommon.myCstr(clsVendorMaster.getFinder(whrCls, fndVendor.Value, isButtonClicked))
            ElseIf clsCommon.CompairString(ddlVendorType.Text, "Primary Transporter") = CompairStringResult.Equal OrElse clsCommon.CompairString(ddlVendorType.Text, "Transporter For Bulk Sale") = CompairStringResult.Equal Then
                whrCls = "  Form_type='PTM'  and Status='N' "
                fndVendor.Value = clsCommon.myCstr(clsVendorMaster.getFinder(whrCls, fndVendor.Value, isButtonClicked))
            ElseIf clsCommon.CompairString(ddlVendorType.Text, "Transporter For Fresh Sale") = CompairStringResult.Equal OrElse clsCommon.CompairString(ddlVendorType.Text, "Transporter For Product Sale") = CompairStringResult.Equal Then
                whrCls = "  Vendor_Code in (select Transport_Id  from TSPL_TRANSPORT_MASTER )   and Status='N' "
                fndVendor.Value = clsCommon.myCstr(clsVendorMaster.getFinder(whrCls, fndVendor.Value, isButtonClicked))
            ElseIf clsCommon.CompairString(ddlVendorType.Text, "MCC Lease Vendor") = CompairStringResult.Equal Then
                whrCls = " Vendor_Code in ( select distinct Chilling_Vendor  from TSPL_MCC_MASTER  where MCC_Type='Co. Leased' )   and Status='N' "
                fndVendor.Value = clsCommon.myCstr(clsVendorMaster.getFinder(whrCls, fndVendor.Value, isButtonClicked))
            Else
                whrCls = " Form_type='ALL'  and Status='N' "
                fndVendor.Value = clsCommon.myCstr(clsVendorMaster.getFinder(whrCls, fndVendor.Value, isButtonClicked))
            End If

            txtVendorNAme.Text = clsCommon.myCstr(clsVendorMaster.GetName(fndVendor.Value, Nothing))
            'LoadProvisionGridData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub SaveData(ByVal isPostbtnClick As Boolean)
        Try
            Dim isSaved As Boolean = True
            Dim trans As SqlTransaction = Nothing
            trans = clsDBFuncationality.GetTransactin()
            Dim objProv As clsProvisionEntry = New clsProvisionEntry()
            If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                objProv.isNewEntry = True
            Else
                objProv.isNewEntry = False
                objProv.Doc_No = fndDocNo.Value
            End If
            objProv.Doc_Date = dtpDate.Value
            objProv.Vendor_Code = clsCommon.myCstr(fndVendor.Value)
            objProv.Vendor_Desc = clsCommon.myCstr(txtVendorNAme.Text)
            objProv.Vendor_Type = clsCommon.myCstr(ddlVendorType.Text)
            objProv.Prov_type = clsCommon.myCstr(ddlProvType.Text)
            objProv.Status = "No"
            objProv.Ref_Doc_No = ""
            objProv.Amount = clsCommon.myCdbl(txtProvAmount.Text)
            objProv.Prog_Code = Me.Form_ID
            objProv.Prov_Month = ddlProvMonth.SelectedValue
            objProv.Prov_Year = Year(dtpProvYear.Value)
            objProv.Loc_Code = clsCommon.myCstr(fndLocation.Value)
            objProv.Loc_Desc = clsCommon.myCstr(txtLocationName.Text)
            If clsProvisionEntry.SaveData(objProv, trans) Then
                trans.Commit()
                If Not isPostbtnClick Then
                    If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                        clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    Else
                        clsCommon.MyMessageBoxShow(Me, "Data Updated Successfully", Me.Text)
                    End If
                End If
                LoadData(objProv.Doc_No, NavigatorType.Current)
                btnSave.Text = "Update"
                fndDocNo.MyReadOnly = True
                btnDelete.Enabled = True
                btnPost.Enabled = True
                Exit Sub
            Else
                btnSave.Text = "Save"
                btnDelete.Enabled = False
                btnPost.Enabled = False
                fndDocNo.MyReadOnly = False
                trans.Rollback()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal navType As NavigatorType)
        Try
            Dim mnthNum As Integer = 0
            Dim obj As clsProvisionEntry = clsProvisionEntry.getData(strCode, navType)
            If obj IsNot Nothing Then
                Reset()
                fndLocation.Enabled = False
                fndDocNo.Value = obj.Doc_No
                dtpDate.Value = obj.Doc_Date
                fndLocation.Value = obj.Loc_Code
                txtLocationName.Text = clsCommon.myCstr(clsLocation.GetName(obj.Loc_Code, Nothing))
                fndVendor.Value = obj.Vendor_Code
                txtVendorNAme.Text = clsCommon.myCstr(clsVendorMaster.GetName(obj.Vendor_Code, Nothing))
                ddlVendorType.Text = obj.Vendor_Type
                ddlProvType.Text = obj.Prov_type
                mnthNum = Math.Truncate(clsCommon.myCdbl(obj.Prov_Month))
                ddlProvMonth.SelectedValue = mnthNum
                txtRouteCode.Value = obj.Route_Code
                txtRouteName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Route_Name from TSPL_MCC_ROUTE_MASTER where Route_Code='" + obj.Route_Code + "'"))
                dtpProvYear.Value = clsCommon.myCDate("01/" & "Jan/" & obj.Prov_Year)
                txtProvAmount.Text = obj.Amount

                If obj.isPosted = 0 Then
                    lblPending.Status = ERPTransactionStatus.Pending
                    btnSave.Text = "Update"
                    btnDelete.Enabled = True
                    btnSave.Enabled = True
                    btnPost.Enabled = True
                Else
                    lblPending.Status = ERPTransactionStatus.Approved
                    btnDelete.Enabled = False
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                End If
                fndDocNo.Focus()
            Else
                Reset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndDocNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndDocNo._MYNavigator
        LoadData(fndDocNo.Value, NavType)
    End Sub

    Private Sub fndDocNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndDocNo._MYValidating
        Dim whrCls As String = String.Empty
        If Not clsMccMaster.isCurrentUserHO Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrCls = "  Loc_Code in (" & objCommonVar.strCurrUserLocations & ") "
            End If
        End If
        fndDocNo.Value = clsProvisionEntry.getFinder(whrCls, fndDocNo.Value, isButtonClicked)
        If clsCommon.myLen(fndDocNo.Value) > 0 Then
            LoadData(fndDocNo.Value, NavigatorType.Current)
        End If
    End Sub

    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click
        Try
            Dim qry As String = ""
            Dim msg As String = ""
            Dim dt As DataTable = Nothing
            If (myMessages.postConfirm()) Then
                SaveData(True)
                If (clsProvisionEntry.PostData(fndDocNo.Value, Nothing, False)) Then
                    msg = "Successfully Posted"
                End If
                common.clsCommon.MyMessageBoxShow(msg)
                LoadData(fndDocNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If AllowToSave() Then
            SaveData(False)
        End If
    End Sub

    Private Sub ddlProvMonth_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles ddlProvMonth.SelectedIndexChanged

    End Sub

    'Private Sub dtpProvYear_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpProvYear.Validated
    '    LoadProvisionGridData()
    'End Sub

    'Private Sub ddlProvMonth_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProvMonth.Validated
    '    LoadProvisionGridData()
    'End Sub

    Private Sub fndDocNo_Load(sender As Object, e As EventArgs) Handles fndDocNo.Load

    End Sub

    Private Sub btnReverse_Click(sender As Object, e As EventArgs) Handles btnReverse.Click
        Try
            If clsCommon.myLen(fndDocNo.Value) <= 0 Then
                fndDocNo.Focus()
                Throw New Exception("Doc No not Found to Reverse")
            End If
            If common.clsCommon.MyMessageBoxShow("Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If clsProvisionEntry.ReverseAndUnpost(fndDocNo.Value) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Unpost.", Me.Text)
                    LoadData(fndDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnMultiReverse_Click(sender As Object, e As EventArgs) Handles btnMultiReverse.Click
        Try
            If TxtMultiSelectProvision.arrValueMember Is Nothing OrElse TxtMultiSelectProvision.arrValueMember.Count <= 0 Then
                Throw New Exception("Please select at least one document to Reverse")
            End If
            Dim StrAllException As String = ""
            For ii As Integer = 0 To TxtMultiSelectProvision.arrValueMember.Count - 1
                Try
                    clsProvisionEntry.ReverseAndUnpost(TxtMultiSelectProvision.arrValueMember(ii))
                Catch ex As Exception
                    StrAllException += "Error In Document no:" + TxtMultiSelectProvision.arrValueMember(ii) + Environment.NewLine + ex.Message
                End Try
            Next
            If clsCommon.myLen(StrAllException) > 0 Then
                clsCommon.MyMessageBoxShow(Me, StrAllException, Me.Text)
            Else
                TxtMultiSelectProvision.arrValueMember = Nothing
                clsCommon.MyMessageBoxShow(Me, "Successfully reverse and unposted", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnMultiDelete_Click(sender As Object, e As EventArgs) Handles btnMultiDelete.Click
        Try
            Dim Reason As String = ""
            If TxtMultiSelectProvision.arrValueMember Is Nothing OrElse TxtMultiSelectProvision.arrValueMember.Count <= 0 Then
                Throw New Exception("Please select at least one document to delete")
            End If

            If (clsCommon.MyMessageBoxShow(Me, "Delete " + clsCommon.myCstr(TxtMultiSelectProvision.arrValueMember.Count) + "Documents." + Environment.NewLine + "Are You sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question)) = System.Windows.Forms.DialogResult.Yes Then

                Dim arr As ArrayList = TxtMultiSelectProvision.arrValueMember
                For Each strDocNo As String In arr
                    clsProvisionEntry.deleteData(strDocNo, Nothing)
                Next

                common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                TxtMultiSelectProvision.arrValueMember = Nothing

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtMultiSelectProvision__My_Click(sender As Object, e As EventArgs) Handles TxtMultiSelectProvision._My_Click
        Dim qry As String = " select TSPL_PROVISION_ENTRY.Doc_No as Code From TSPL_PROVISION_ENTRY "
        TxtMultiSelectProvision.arrValueMember = clsCommon.ShowMultipleSelectForm("PROV@Mul1", qry, "Code", "Code", TxtMultiSelectProvision.arrValueMember, TxtMultiSelectProvision.arrDispalyMember)
    End Sub
End Class
