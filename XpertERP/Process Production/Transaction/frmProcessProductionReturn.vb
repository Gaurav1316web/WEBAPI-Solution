
Imports common
Imports System.IO

Public Class frmProcessProductionReturn
    Inherits FrmMainTranScreen

#Region "Variables"
    Private isNewEntry As Boolean = True
    Private isInsideLoadData As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim arrLoc As String = Nothing
#End Region

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.TransferReturn)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPost.Visible = MyBase.isPostFlag
    End Sub

    Private Sub LOCATIONRIGTHS()
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData()

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                'txtlocationcode.Value = obj.Default_LocCode
                'txtlocationname.Text = obj.Default_LocName
            End If
            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public STIRDocOpenTrans As String = Nothing
    Private Sub OpenFormByEnumreation()
        Try
            If STIRDocOpenTrans IsNot Nothing AndAlso STIRDocOpenTrans.Length > 0 Then
                LoadData(STIRDocOpenTrans, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub frmProcessProductionReturn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            SetUserMgmtNew()

            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
            ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D for Save/Update Trasnaction")
            ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
            ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
            RadPageView1.SelectedPage = RadPageViewPage1

            AddNew()
            SetLength()

            ''For Custom Fields
            RadPageView1.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
            If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                UcCustomFields1.Report_ID = MyBase.Form_ID
                UcCustomFields1.LoadCustomControls()
            End If
            ''End of For Custom Fields

            ''For Attachment
            GetDocumentType()
            cmbDocumentType.SelectedValue = "PE"
            cmbDocumentType.Enabled = False
            LOCATIONRIGTHS()
            If objCommonVar.IsDemoERP Then
                UcAttachment1.Form_ID = MyBase.Form_ID
                RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Visible
            Else
                RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Collapsed
            End If
            ''End of For Attachment
            If clsCommon.myLen(Me.Tag) > 0 Then
                LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
            End If

            OpenFormByEnumreation()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub GetDocumentType()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Name")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "STD"
        dr("Name") = "Production Standardization"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "PE"
        dr("Name") = "Production Entry"
        dt.Rows.Add(dr)

        cmbDocumentType.DataSource = dt
        cmbDocumentType.DisplayMember = "Name"
        cmbDocumentType.ValueMember = "Code"
    End Sub

    Sub SetLength()
        txtCode.MyMaxLength = 30
        txtRmks.MaxLength = 200
    End Sub

    Sub BlankAllControls()
        fndProdNo.Value = Nothing
        txtCode.Value = Nothing
        txtRmks.Text = ""
        lblLineNo.Text = ""
        LblCostCenterCode.Text = ""
        lblCostCenterName.Text = ""
        lblProfitCenterCode.Text = ""
        lblProfitCenterName.Text = ""
        TxtManualBatchNo.Text = ""
        txtDate.Value = clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
        cmbDocumentType.SelectedValue = "PE"
        blankProdFields()

        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
        End If
        ''End of For Custom Fields
        UcAttachment1.BlankAllControls()
    End Sub

    Sub blankProdFields()
        txtBatchNo.Value = Nothing
        dtpBatchDate.Value = Today
        txtLocation.Value = Nothing
        lblLocation.Text = ""
        lblConsmSectionLocCode.Text = ""
        lblConsmSectionCode.Text = ""
        txtDesc.Text = ""

    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()

    End Sub

    Sub AddNew()
        BlankAllControls()
        UsLock1.Status = ERPTransactionStatus.Pending
        isNewEntry = True
        btnSave.Enabled = True
        btnDelete.Enabled = True
        btnPost.Enabled = True
        btnSave.Text = "Save"
        txtDate.Focus()
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.SetDefaultValues()
        End If
        ''End of For Custom Fields
        UcAttachment1.BlankAllControls()
    End Sub

    Function AllowToSave() As Boolean
        Try
            '=============================Added by preeti Gupta=====================
            If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
                txtDate.Focus()
                Return False
            End If
            '=======================================================================
            If clsCommon.myLen(fndProdNo.Value) <= 0 Then
                fndProdNo.Focus()
                Throw New Exception("Please select Doc No")
            End If
            If clsCommon.myLen(txtRmks.Text) <= 0 Then
                txtRmks.Focus()
                Throw New Exception("Please enter Remarks for return")
            End If
            If clsCommon.CompairString(cmbDocumentType.SelectedValue, "PE") = CompairStringResult.Equal Then
                If clsCommon.myCDate(clsDBFuncationality.getSingleValue("Select CONVERT(date, PROD_DATE,103) from TSPL_PP_PRODUCTION_ENTRY WHERE PROD_ENTRY_CODE='" & fndProdNo.Value & "' ")) > clsCommon.myCDate(txtDate.Value) Then
                    txtDate.Focus()
                    Throw New Exception("Date cannot be less than from Production Date")
                End If
            Else
                If clsCommon.myCDate(clsDBFuncationality.getSingleValue("Select CONVERT(date, Standardization_Date,103) from TSPL_PP_STANDARDIZATION_HEAD WHERE Standardization_Code='" & fndProdNo.Value & "' ")) > clsCommon.myCDate(txtDate.Value) Then
                    txtDate.Focus()
                    Throw New Exception("Date cannot be less than from Standardization Date")
                End If
            End If


            UcCustomFields1.AllowToSave()
            UcAttachment1.AllowToSave()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        'If clsCommon.MyMessageBoxShow("Do you want to save this record.", "Save", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
        SaveData(False)
        'End If
    End Sub

    Sub SaveData(ByVal isPost As Boolean)
        Try
            If (AllowToSave()) Then
                Dim obj As New clsProcessProductionReturn()
                obj.PROD_RETURN_CODE = txtCode.Value
                obj.Batch_Code = txtBatchNo.Value
                obj.BATCH_DATE = dtpBatchDate.Value
                obj.Prod_Date = dtpProdDate.Value
                obj.COMMENTS = txtDesc.Text
                obj.CONSM_LOCATION_CODE = lblConsmSectionLocCode.Text
                obj.CONSM_SECTION_CODE = lblConsmSectionCode.Text
                obj.DESCRIPTION = txtRmks.Text
                obj.LOCATION_CODE = txtLocation.Value
                obj.PROD_ENTRY_CODE = fndProdNo.Value
                obj.RETURN_DATE = txtDate.Value
                obj.Transaction_Type = cmbDocumentType.SelectedValue
                ''richa agarwal againt ticket no BHA/02/07/18-000120 11 July,2018
               obj.ManualBatchNo = clsCommon.myCstr(TxtManualBatchNo.Text)
                obj.LINE_NO = clsCommon.myCstr(lblLineNo.Text)
                obj.CostCenterCode = clsCommon.myCstr(LblCostCenterCode.Text)
                obj.ProfitCenterCode = clsCommon.myCstr(lblProfitCenterCode.Text)
                ''----------------------
                'If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                '    UcCustomFields1.GetData(obj.arrCustomFields)
                'End If
                ''End of For Custom Fields

                If (obj.SaveData(obj, isNewEntry)) Then
                    UcAttachment1.SaveData(obj.PROD_RETURN_CODE)
                    If isPost = False Then
                        common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    End If

                    LoadData(obj.PROD_RETURN_CODE, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strDocumentNo As String, ByVal navType As common.NavigatorType)
        Try
            btnSave.Enabled = True
            btnDelete.Enabled = True
            btnPost.Enabled = True
            isInsideLoadData = True

            BlankAllControls()
            Dim obj As clsProcessProductionReturn = clsProcessProductionReturn.GetData(strDocumentNo, navType, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.PROD_RETURN_CODE) > 0) Then
                btnSave.Enabled = True
                isNewEntry = False
                btnSave.Text = "Update"
                txtCode.Value = obj.PROD_RETURN_CODE
                txtBatchNo.Value = obj.Batch_Code
                dtpBatchDate.Value = obj.BATCH_DATE
                dtpProdDate.Value = obj.Prod_Date
                txtDesc.Text = obj.COMMENTS
                lblConsmSectionLocCode.Text = obj.CONSM_LOCATION_CODE
                lblConsmSectionCode.Text = obj.CONSM_SECTION_CODE
                txtRmks.Text = obj.DESCRIPTION
                txtLocation.Value = obj.LOCATION_CODE
                lblLocation.Text = clsLocation.GetName(obj.LOCATION_CODE, Nothing)
                fndProdNo.Value = obj.PROD_ENTRY_CODE
                txtDate.Value = obj.RETURN_DATE
                cmbDocumentType.SelectedValue = obj.Transaction_Type
                ''richa agarwal againt ticket no BHA/02/07/18-000120 11 July,2018
                TxtManualBatchNo.Text = obj.ManualBatchNo
                lblLineNo.Text = obj.LINE_NO
                LblCostCenterCode.Text = obj.CostCenterCode
                lblCostCenterName.Text = obj.CostCenterName
                lblProfitCenterCode.Text = obj.ProfitCenterCode
                lblProfitCenterName.Text = obj.ProfitCenterName
                ''----------------------
                If obj.POSTED Then
                    UsLock1.Status = ERPTransactionStatus.Approved
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                Else
                    UsLock1.Status = ERPTransactionStatus.Pending
                    btnSave.Enabled = True
                    btnPost.Enabled = True
                    btnDelete.Enabled = True
                End If
                ''For Custom Fields
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.LoadData(obj.PROD_RETURN_CODE)
                End If
                ''End of For Custom Fields
                UcAttachment1.LoadData(obj.PROD_RETURN_CODE)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub

    Sub CloseForm()
        Me.Close()
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        PostData()
    End Sub

    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                SaveData(True)
                If clsProcessProductionReturn.PostData(txtCode.Value) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Posted Successfully ", Me.Text)
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_PP_PRODUCTION_RETURN where PROD_RETURN_CODE='" + txtCode.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Dim sQuery As String = String.Empty
        sQuery = "select PROD_RETURN_CODE as [Code],convert(date,RETURN_DATE,103) as [Document Date],PROD_ENTRY_CODE as [Production No],DESCRIPTION,cast(POSTED as varchar(2)) as Posted from TSPL_PP_PRODUCTION_RETURN"
        LoadData(clsCommon.ShowSelectForm("ProdReturn", sQuery, "Code", "", txtCode.Value, "TSPL_PP_PRODUCTION_RETURN.RETURN_DATE", isButtonClicked, "TSPL_PP_PRODUCTION_RETURN.RETURN_DATE"), NavigatorType.Current)
    End Sub

    Private Sub frmPurchaseRequistion_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.Alt AndAlso e.KeyCode = Keys.N Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            SaveData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnDelete.Enabled AndAlso MyBase.isDeleteFlag Then
            delete()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine + _
                                              "TSPL_PP_PRODUCTION_RETURN " + Environment.NewLine + _
                                              "Press Alt+P for Post Trasnaction " + Environment.NewLine + _
                                              "TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL " + Environment.NewLine + _
                                              "TSPL_INVENTORY_MOVEMENT " + Environment.NewLine + _
                                              "TSPL_SERIAL_ITEM " + Environment.NewLine + _
                                              "TSPL_BATCH_ITEM " + Environment.NewLine + _
                                              "TSPL_INVENTORY_MOVEMENT_new ")
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If txtCode.Value = "" Then
            myMessages.blankValue("Requisition Number")

        Else
            funPrint()
        End If

    End Sub

    Private Sub funPrint()

    End Sub

    Private Sub txtSRNNo__MYOpenMasterForm(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndProdNo._MYOpenMasterForm
        Try
            Dim frm As New frmProductionEntry

            frm.Tag = fndProdNo.Value

            'frm.WindowState = FormWindowState.Maximized
            frm.Show()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

    Private Sub txtVendor__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndProdNo._MYValidating
        If clsCommon.CompairString(cmbDocumentType.SelectedValue, "PE") = CompairStringResult.Equal Then
            fndProdNo.Value = clsProcessProductionReturn.GetProductionFinder(" TSPL_PP_PRODUCTION_ENTRY.POSTED=1 AND TSPL_PP_PRODUCTION_ENTRY.location_code in (" + arrLoc + ") and TSPL_PP_PRODUCTION_RETURN.PROD_RETURN_CODE is null ", fndProdNo.Value, isButtonClicked)
        Else
            fndProdNo.Value = clsProcessProductionStandardization.GetFinder(" TSPL_PP_STANDARDIZATION_HEAD.POSTED=1 AND TSPL_PP_STANDARDIZATION_HEAD.Loaction_Code in (" + arrLoc + ") and TSPL_PP_STANDARDIZATION_HEAD.Standardization_Code not in (select PROD_ENTRY_CODE from TSPL_PP_PRODUCTION_RETURN)", fndProdNo.Value, isButtonClicked)
        End If
        LoadProdData()
    End Sub

    Private Sub LoadProdData()
        blankProdFields()
        If clsCommon.myLen(fndProdNo.Value) > 0 Then
            If clsCommon.CompairString(cmbDocumentType.SelectedValue, "PE") = CompairStringResult.Equal Then
                Dim obj As clsProductionEntry = clsProductionEntry.GetData(fndProdNo.Value, Nothing, NavigatorType.Current)
                If Not obj Is Nothing Then
                    dtpProdDate.Value = obj.PROD_DATE
                    txtBatchNo.Value = obj.Batch_Code
                    dtpBatchDate.Value = obj.BATCH_DATE
                    txtLocation.Value = obj.LOCATION_CODE
                    lblLocation.Text = obj.LOCATION_NAME
                    lblConsmSectionLocCode.Text = obj.CONSM_LOCATION_CODE
                    lblConsmSectionCode.Text = obj.CONSM_SECTION_CODE
                    txtDesc.Text = obj.DESCRIPTION
                    ''richa agarwal againt ticket no BHA/02/07/18-000120 11 July,2018
                    TxtManualBatchNo.Text = obj.ManualBatchNo
                    lblLineNo.Text = obj.LINE_NO
                    LblCostCenterCode.Text = obj.CostCenterCode
                    lblCostCenterName.Text = obj.CostCenterName
                    lblProfitCenterCode.Text = obj.ProfitCenterCode
                    lblProfitCenterName.Text = obj.ProfitCenterName
                    ''----------------------
                End If
            Else
                Dim obj As clsProcessProductionStandardization = clsProcessProductionStandardization.GetData(fndProdNo.Value, Nothing, NavigatorType.Current, Nothing)
                If Not obj Is Nothing Then
                    dtpProdDate.Value = obj.Standardization_Date
                    txtBatchNo.Value = obj.Child_Batch_Code
                    dtpBatchDate.Value = clsProcessBatchOrder.GetDate(obj.Child_Batch_Code, Nothing)
                    txtLocation.Value = obj.Loaction_Code
                    lblLocation.Text = obj.Loaction_Desc
                    lblConsmSectionLocCode.Text = obj.CONSM_LOCATION_CODE
                    lblConsmSectionCode.Text = obj.CONSM_SECTION_CODE
                    txtDesc.Text = ""
                    ''richa agarwal againt ticket no BHA/02/07/18-000120 11 July,2018
                    TxtManualBatchNo.Text = obj.ManualBatchNo
                    lblLineNo.Text = obj.LINE_NO
                    LblCostCenterCode.Text = obj.CostCenterCode
                    lblCostCenterName.Text = obj.CostCenterName
                    lblProfitCenterCode.Text = obj.ProfitCenterCode
                    lblProfitCenterName.Text = obj.ProfitCenterName
                    ''-------------------------
                End If
            End If
        End If

    End Sub

    Private Sub btnPost_Click_1(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If myMessages.deleteConfirm() Then
                If clsProcessProductionReturn.DeleteData(txtCode.Value) Then
                    myMessages.delete()
                    AddNew()
                End If
            End If            
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    ' Ticket No : TEC/29/10/18-000347 By Prabhakar
    Private Sub btnShowInventory_Click(sender As Object, e As EventArgs) Handles btnShowInventory.Click
        clsOpenInventory.ShowInventoryDatails(txtCode.Value)
    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select Return No", Me.Text)
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowHistoryData(txtCode.Value, "PROD_RETURN_CODE", "TSPL_PP_PRODUCTION_RETURN")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class
