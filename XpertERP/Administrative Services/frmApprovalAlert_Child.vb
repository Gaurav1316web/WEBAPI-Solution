''created by Monika'15/10/2015 [BM00000008148]
Imports System.IO
Imports common
Public Class FrmApprovalAlert_Child
    Inherits FrmMainTranScreen

#Region "variables"
    Dim ButtonToolTip As New ToolTip()
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChanged As Boolean = False
    Dim Auto_Post As Boolean = False
    Public ScreenCode As String = Nothing
    Public DocumentCode As String = Nothing
#End Region

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmApprovalAlertSumm)
        'If Not (MyBase.isReadFlag) Then
        '    Throw New Exception("Permission Denied")
        'End If

        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
    End Sub

    Private Sub FrmApprovalAlert_Child_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.Alt AndAlso e.KeyCode = Keys.C Then
                Me.Close()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
                btnSave.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled AndAlso MyBase.isPostFlag AndAlso btnPost.Visible Then
                btnPost.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDrillDown.Enabled AndAlso btnDrillDown.Visible Then
                btnDrillDown.PerformClick()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FrmApprovalAlert_Child_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetUserMgmtNew()
        LoadCombobox()
        FunReset()

        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for approve record.")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C for close window.")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for post record.")
        ButtonToolTip.SetToolTip(btnDrillDown, "Press Alt+D for detailed document.")

        btnPost.Visible = True
        LinkLabel2.Visible = False
        LinkLabel1.Visible = True
        btnSave.Enabled = True

        'Show Print button only on DBT NEFT Uploader screen'
        If clsCommon.CompairString(ScreenCode, clsUserMgtCode.DBTNEFTUploader) = CompairStringResult.Equal Then
            btnPrint.Visible = True
            UcAttachment1.Form_ID = ScreenCode
            UcAttachment1.MandatoryPDFFile = True
            UcAttachment1.isDeleteTheAttachment = False
            UcAttachment1.BlankAllControls()
        Else
            RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Collapsed
            btnPrint.Visible = False
        End If
        If clsCommon.myLen(ScreenCode) > 0 AndAlso clsCommon.myLen(DocumentCode) > 0 Then
            Auto_Post = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select distinct Auto_Post from TSPL_APPROVAL_LEVEL_SCREEN where trans_code='" + ScreenCode + "'"))
            If Auto_Post Then
                btnPost.Visible = False
            End If
            LoadData(ScreenCode, DocumentCode)
        End If
    End Sub

    Private Sub LoadCombobox()
        cboApproval.DataSource = Nothing
        Dim qry As String = "select '' as Code,'None' as Name union all select 'Approved' as Code,'Approved' as Name union all select 'Rejected' as Code,'Rejected' as Name "
        cboApproval.DataSource = clsDBFuncationality.GetDataTable(qry)
        cboApproval.ValueMember = "Code"
        cboApproval.DisplayMember = "Name"
    End Sub

    Private Sub LoadData(ByVal ScreenCode As String, ByVal Document_Code As String)
        Dim dt As New DataTable()
        Dim dt1 As New DataTable()
        Try
            Dim qry As String = "select max(TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Created_By) as created_by,max(No_Of_Level) as max_app_lvl,max(TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Document_Code) as doc_code,max(TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Document_Date) as doc_date,case when isnull(max(TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Status),'')='' then 'Pending' else max(TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Status) end as Doc_Status,max(TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Created_Date) as created_date,max(TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Amount) as amt,max(TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Qty) as qty " &
                                ",max(isnull(is_posted,0)) as posted from TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL where TRANS_Code='" + ScreenCode + "' and Document_Code='" + Document_Code + "' and is_reverse=0 and Status<>'Amend' "
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                txtApprovalSendFrom.Text = clsCommon.myCstr(dt.Rows(0)("created_by"))
                txtmax_app_lvl.Text = "Max Approval Level: " + clsCommon.myCstr(dt.Rows(0)("max_app_lvl"))

                txtDoc_Code.Text = clsCommon.myCstr(dt.Rows(0)("doc_code"))
                txtDoc_Date.Text = clsCommon.myCstr(dt.Rows(0)("doc_date"))
                txtDoc_Amt.Text = clsCommon.myCstr(dt.Rows(0)("amt"))
                txtDoc_Qty.Text = clsCommon.myCstr(dt.Rows(0)("qty"))
                txtDoc_Status.Text = clsCommon.myCstr(dt.Rows(0)("Doc_Status"))
                txtDocIssue_Date.Text = clsCommon.myCstr(dt.Rows(0)("created_date"))

                txtAuthIssue_Date.Text = clsCommon.myCstr(dt.Rows(0)("created_date"))

                qry = "select TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.No_Of_Level,case when isnull(TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Status,'')='' then 'Pending' else TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Status end as status,case when (isnull(TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Description,'')='' and isnull(TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Remarks,'')='') then 'Request pending for approval of '+TSPL_PROGRAM_MASTER.Program_Name when (isnull(TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Description,'')='' and isnull(TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Remarks,'')<>'') then TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Remarks when (isnull(TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Description,'')<>'' and isnull(TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Remarks,'')='') then TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Description when (isnull(TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Description,'')<>'' and isnull(TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Remarks,'')<>'') then TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Description+char(10)+'Remarks : '+TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Remarks else 'Request pending for approval of '+TSPL_PROGRAM_MASTER.Program_Name end as rem from TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL left outer join TSPL_PROGRAM_MASTER on TSPL_PROGRAM_MASTER.Program_Code=TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.TRANS_Code where TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.User_Code='" + objCommonVar.CurrentUserCode + "' and TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.TRANS_Code='" + ScreenCode + "' and TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Document_Code='" + Document_Code + "' and is_reverse=0 and Status<>'Amend' "
                dt1 = New DataTable()
                dt1 = clsDBFuncationality.GetDataTable(qry)

                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    txtAuth_Status.Text = clsCommon.myCstr(dt1.Rows(0)("status"))
                    txtAuthRemark.Text = clsCommon.myCstr(dt1.Rows(0)("rem"))
                    txtCurrentLevel.Text = clsCommon.myCstr(dt1.Rows(0)("No_Of_Level"))
                End If

                cboApproval.Text = ""
                txtRmks.Text = Nothing

                If clsCommon.myCdbl(dt.Rows(0)("posted")) > 0 Then
                    cboApproval.Enabled = False
                    txtRmks.Enabled = False
                    btnPost.Enabled = False
                    btnSave.Enabled = False
                Else
                    cboApproval.Enabled = True
                    txtRmks.Enabled = True
                    btnPost.Enabled = True
                    btnSave.Enabled = True
                End If

                If clsCommon.CompairString(ScreenCode, clsUserMgtCode.DBTNEFTUploader) = CompairStringResult.Equal Then
                    UcAttachment1.isDeleteTheAttachment = False
                    UcAttachment1.LoadData(txtDoc_Code.Text)
                End If
            Else
                FunReset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            dt = Nothing
            dt1 = Nothing
        End Try
    End Sub

    Private Sub FunReset()
        btnSave.Enabled = True
        txtApprovalSendFrom.Text = ""
        txtDoc_Code.Text = ""
        txtDoc_Date.Text = ""
        txtDoc_Status.Text = ""
        txtDocIssue_Date.Text = ""
        txtDoc_Amt.Text = ""
        txtDoc_Qty.Text = ""
        txtmax_app_lvl.Text = ""
        txtCurrentLevel.Text = ""

        txtAuth_Status.Text = ""
        txtAuthIssue_Date.Text = ""
        txtAuthRemark.Text = ""

        cboApproval.SelectedValue = ""
        txtRmks.Text = ""

        gv.DataSource = Nothing
        gv.Rows.Clear()
        gv.Columns.Clear()

        btnSave.Enabled = True

        RadPageViewPage1.Item.Visibility = ElementVisibility.Visible
        RadPageViewPage2.Item.Visibility = ElementVisibility.Collapsed
        RadPageView1.SelectedPage = RadPageViewPage1

        txtDoc_Code.Focus()
        txtDoc_Code.Select()
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 AndAlso RadPageViewPage2.Item.Visibility = ElementVisibility.Visible Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv.Columns.Count - 1 Step ii + 1
                        gv.Columns(ii).IsVisible = False
                        gv.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click ''save Layout
        If clsCommon.myLen(MyBase.Form_ID) > 0 AndAlso RadPageViewPage2.Item.Visibility = ElementVisibility.Visible Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click ''delete layout
        If clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode) AndAlso RadPageViewPage2.Item.Visibility = ElementVisibility.Visible Then
            common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
        End If
    End Sub

    Private Sub btnDrillDown_Click(sender As Object, e As EventArgs) Handles btnDrillDown.Click
        Try
            If clsCommon.myLen(txtDoc_Code.Text) > 0 Then
                Dim AllowModificationByApprovalUser As Boolean = False
                AllowModificationByApprovalUser = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowModificationOnApprovalByApprovalUser, clsFixedParameterCode.AllowModificationOnApprovalByApprovalUser, Nothing)) = "1", True, False))
                MDI.ShowForm(ScreenCode, "", False, txtDoc_Code.Text, True, AllowModificationByApprovalUser)

                'If clsCommon.CompairString(clsUserMgtCode.mbtnPurchaseRequistion, ScreenCode) = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseRequistion, txtDoc_Code.Text, False)

                'ElseIf clsCommon.CompairString(clsUserMgtCode.mbtnPurchaseOrder, ScreenCode) = CompairStringResult.Equal Then
                '    clsOpenTransactionFosrm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseOrder, txtDoc_Code.Text, False)

                'ElseIf clsCommon.CompairString(clsUserMgtCode.frmSNSalesOrder, ScreenCode) = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSNSalesOrder, txtDoc_Code.Text, False)

                'ElseIf clsCommon.CompairString(clsUserMgtCode.frmSNShipment, ScreenCode) = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmShipmentProductSale, txtDoc_Code.Text, False)

                'ElseIf clsCommon.CompairString(clsUserMgtCode.frmDeliveryPrderProductSale, ScreenCode) = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSalesOrderProductSale, txtDoc_Code.Text, False)

                'ElseIf clsCommon.CompairString(clsUserMgtCode.frmDeliveryNoteFreshSale, ScreenCode) = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(EnumTransType.FreshDelivery, txtDoc_Code.Text, False)

                'ElseIf clsCommon.CompairString(clsUserMgtCode.frmCSASaleInvoice, ScreenCode) = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSASaleInvoice, txtDoc_Code.Text, False)

                'ElseIf clsCommon.CompairString(clsUserMgtCode.frmVSPItemIssue, ScreenCode) = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmVSPItemIssue, txtDoc_Code.Text, False)

                'ElseIf clsCommon.CompairString(clsUserMgtCode.frmMCCMaterial, ScreenCode) = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCMaterial, txtDoc_Code.Text, False)

                'ElseIf clsCommon.CompairString(clsUserMgtCode.frmShipmentProductSale, ScreenCode) = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmShipmentProductSale, txtDoc_Code.Text, False)

                'ElseIf clsCommon.CompairString(clsUserMgtCode.FrmDispatchBulkSale, ScreenCode) = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmDispatchBulkSale, txtDoc_Code.Text, False)

                'ElseIf clsCommon.CompairString(clsUserMgtCode.frmbookingdairy, ScreenCode) = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(EnumTransType.DairyBooking, txtDoc_Code.Text, False)

                'ElseIf clsCommon.CompairString(clsUserMgtCode.frmCSADeliveryOrder, ScreenCode) = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(EnumTransType.SDCSADO, txtDoc_Code.Text, False)

                'ElseIf clsCommon.CompairString(clsUserMgtCode.FrmLCCreation, ScreenCode) = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmLCCreation, txtDoc_Code.Text, False)

                'ElseIf clsCommon.CompairString(clsUserMgtCode.frmBulkMilkSRN, ScreenCode) = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmBulkMilkSRN, txtDoc_Code.Text, False)
                'ElseIf clsCommon.CompairString(clsUserMgtCode.frmSaleReturnProductSale, ScreenCode) = CompairStringResult.Equal Then
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSaleReturnProductSale, txtDoc_Code.Text, False)

                'End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Try
            If RadPageViewPage2.Item.Visibility = ElementVisibility.Collapsed Then
                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                RadPageViewPage1.Item.Visibility = ElementVisibility.Collapsed
                RadPageViewPage2.Item.Visibility = ElementVisibility.Visible
                RadPageView1.SelectedPage = RadPageViewPage2
                LinkLabel1.Visible = False
                LinkLabel2.Visible = True

                LoadGridData()
            ElseIf RadPageViewPage2.Item.Visibility = ElementVisibility.Visible Then
                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                RadPageViewPage1.Item.Visibility = ElementVisibility.Visible
                RadPageViewPage2.Item.Visibility = ElementVisibility.Collapsed
                RadPageView1.SelectedPage = RadPageViewPage1

                LinkLabel1.Visible = True
                LinkLabel2.Visible = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub LoadGridData()
        Dim dt As New DataTable()
        Try
            Dim qry As String = "select TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.no_of_level as [Level No],tspl_user_master.user_name as [Authorizer],case when isnull(TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Status,'')='' then '' else cast(TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.modified_date as varchar) end as [Authorized Date],TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Status,TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.approval_remark as [Remarks] " &
                                " from TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL left outer join tspl_user_master on tspl_user_master.user_code=TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.user_code " &
                                " where TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.trans_code='" + ScreenCode + "' and document_code='" + DocumentCode + "' and is_reverse=0 "
            gv.DataSource = Nothing
            dt = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv.DataSource = dt

                gv.ReadOnly = True
                gv.Columns("Level No").Width = 60
                gv.Columns("Level No").WrapText = True

                gv.Columns("Authorizer").Width = 180
                gv.Columns("Authorizer").WrapText = True

                gv.Columns("Authorized Date").Width = 100
                gv.Columns("Authorized Date").WrapText = True

                gv.Columns("Status").Width = 120
                gv.Columns("Status").WrapText = True

                gv.Columns("Remarks").Width = 200
                gv.Columns("Remarks").WrapText = True

                ReStoreGridLayout()

                For Each grow As GridViewRowInfo In gv.Rows
                    grow.Cells("Authorizer").Style.DrawFill = True
                    grow.Cells("Authorizer").Style.CustomizeFill = True
                    grow.Cells("Authorizer").Style.BackColor = Nothing

                    If clsCommon.CompairString(objCommonVar.CurrentUserCode, clsCommon.myCstr(grow.Cells("Authorizer").Value)) = CompairStringResult.Equal Then
                        grow.Cells("Authorizer").Style.DrawFill = True
                        grow.Cells("Authorizer").Style.CustomizeFill = True
                        grow.Cells("Authorizer").Style.BackColor = Color.Wheat
                    End If
                Next
            Else
                Throw New Exception("No record found.")
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            dt = Nothing
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Private Sub SaveData()
        Dim obj As New clsApprovalAlert_Child()
        Try
            If clsCommon.CompairString(cboApproval.SelectedValue, "") = CompairStringResult.Equal Then
                cboApproval.Focus()
                cboApproval.Select()
                Throw New Exception("Select approval status.")
            End If

            If (((clsCommon.CompairString(ScreenCode, "PO-REQ") = CompairStringResult.Equal) Or (clsCommon.CompairString(ScreenCode, "PO-ODR") = CompairStringResult.Equal)) AndAlso clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal AndAlso clsCommon.myCstr(cboApproval.SelectedValue) <> "Rejected") OrElse (clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "SPMMD") = CompairStringResult.Equal) Then
            ElseIf (clsCommon.CompairString(ScreenCode, clsUserMgtCode.DBTNEFTUploader) = CompairStringResult.Equal) Then
                UcAttachment1.AllowToSave()
            Else
                If clsCommon.myLen(txtRmks.Text) <= 0 Then
                    txtRmks.Focus()
                    txtRmks.Select()
                    Throw New Exception("Fill remarks.")
                End If
            End If


            obj = New clsApprovalAlert_Child()
            obj.Approval_Remark = clsCommon.myCstr(txtRmks.Text)
            obj.Auto_Post = Auto_Post
            obj.Document_Code = clsCommon.myCstr(txtDoc_Code.Text)
            obj.Document_Date = clsCommon.myCDate(txtDoc_Date.Text)
            If clsCommon.myLen(txtmax_app_lvl.Text) > 0 Then
                Dim index As Integer = txtmax_app_lvl.Text.LastIndexOf(":")
                obj.Max_App_Level = clsCommon.myCdbl(clsCommon.myCstr(txtmax_app_lvl.Text).Substring(index, clsCommon.myLen(txtmax_app_lvl.Text) - index).Replace(":", ""))
            End If

            obj.No_Of_Level = clsCommon.myCdbl(txtCurrentLevel.Text)
            obj.Status = clsCommon.myCstr(cboApproval.SelectedValue)
            obj.Trans_Code = ScreenCode
            obj.User_Code = clsCommon.myCstr(objCommonVar.CurrentUserCode)
            UcAttachment1.SaveData(obj.Document_Code, False, Nothing)
            If clsApprovalAlert_Child.UpdateData(obj) Then

                clsCommon.MyMessageBoxShow(Me, "Data updated successfully.", Me.Text)
                Me.Close()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
        End Try
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Dim obj As New clsApprovalAlert_Child()
        Try
            If clsCommon.myLen(txtDoc_Code.Text) <= 0 Then
                txtDoc_Code.Focus()
                txtDoc_Code.Select()
                Throw New Exception("No document found for post")
            End If

            ''================if status updated, only then post otherwise no posting of document==========
            Dim qry As String = "select status from TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL where TRANS_Code='" + clsCommon.myCstr(ScreenCode) + "' and Document_Code='" + clsCommon.myCstr(txtDoc_Code.Text) + "' and No_Of_Level='" + clsCommon.myCstr(txtCurrentLevel.Text) + "' and User_Code='" + clsCommon.myCstr(objCommonVar.CurrentUserCode) + "' and is_reverse=0"
            Dim stus As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            If clsCommon.myLen(stus) <= 0 Then
                Throw New Exception("Document must be approve before post.")
            End If

            ''==============================================================================================

            If Not myMessages.postConfirm() Then
                Exit Sub
            End If

            obj = New clsApprovalAlert_Child()

            obj.Approval_Remark = clsCommon.myCstr(txtRmks.Text)
            obj.Auto_Post = Auto_Post
            obj.Document_Code = clsCommon.myCstr(txtDoc_Code.Text)
            obj.Document_Date = clsCommon.myCDate(txtDoc_Date.Text)
            If clsCommon.myLen(txtmax_app_lvl.Text) > 0 Then
                Dim index As Integer = txtmax_app_lvl.Text.LastIndexOf(":")
                obj.Max_App_Level = clsCommon.myCdbl(clsCommon.myCstr(txtmax_app_lvl.Text).Substring(index, clsCommon.myLen(txtmax_app_lvl.Text) - index).Replace(":", ""))
            End If

            obj.No_Of_Level = clsCommon.myCdbl(txtCurrentLevel.Text)
            obj.Status = clsCommon.myCstr(cboApproval.SelectedValue)
            obj.Trans_Code = ScreenCode
            obj.User_Code = clsCommon.myCstr(objCommonVar.CurrentUserCode)

            If clsCommon.CompairString(obj.Status, "Rejected") = CompairStringResult.Equal Then
                Throw New Exception("Document should be approved for post.")
            End If
            If clsCommon.myCdbl(obj.Max_App_Level) = clsCommon.myCdbl(txtCurrentLevel.Text) Then
                If clsApprovalAlert_Child.Postdata(obj) Then
                    myMessages.post()
                End If
            Else
                Throw New Exception("You are not authorized.")
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Try
            If RadPageViewPage2.Item.Visibility = ElementVisibility.Collapsed Then
                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                RadPageViewPage1.Item.Visibility = ElementVisibility.Collapsed
                RadPageViewPage2.Item.Visibility = ElementVisibility.Visible
                RadPageView1.SelectedPage = RadPageViewPage2
                LinkLabel1.Visible = False
                LinkLabel2.Visible = True

                LoadGridData()
            ElseIf RadPageViewPage2.Item.Visibility = ElementVisibility.Visible Then
                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                RadPageViewPage1.Item.Visibility = ElementVisibility.Visible
                RadPageViewPage2.Item.Visibility = ElementVisibility.Collapsed
                RadPageView1.SelectedPage = RadPageViewPage1

                LinkLabel1.Visible = True
                LinkLabel2.Visible = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim objP As New clsDBTNEFT()
        objP.funPrintBankLetter(txtDoc_Code.Text, False)
    End Sub
End Class
