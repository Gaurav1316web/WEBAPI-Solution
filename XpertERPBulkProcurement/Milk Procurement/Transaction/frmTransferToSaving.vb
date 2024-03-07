Imports System.IO
Imports common
Public Class frmTransferToSaving
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim GSTStatus As Boolean = False
    Private isCellValueChangedOpen As Boolean = False
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False

    Const colVendorCode As String = "colVendorCode"
    Const colSNo As String = "colSNo"
    Const colVlcUploderCode As String = "colVlcUploderCode"
    Const colVendorName As String = "colVendorName"
    Const colAmt As String = "colAmt"

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim SettShowMCCFinder As Boolean = False
#End Region

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        'RadMenu1.Visible = MyBase.isExport
        btnsetting.Visible = MyBase.isExport
        btnReverse.Visible = False

        'If MyBase.isReverse Then
        '    btnReverse.Enabled = True
        'Else
        '    btnReverse.Enabled = False
        'End If
        If MyBase.isExport = True Then
            RadMenuItem2.Enabled = True
            RadMenuItem3.Enabled = True
        Else
            RadMenuItem2.Enabled = False
            RadMenuItem3.Enabled = False
        End If
    End Sub

    Private Sub FrmMultipleProcDeduction_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        SettShowMCCFinder = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowMCCFinderInPaymentProcess, clsFixedParameterCode.ShowMCCFinderInPaymentProcess, Nothing)) = 1)

        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")

        RadPageView1.SelectedPage = RadPageViewPage1
        LoadBlankGrid()
        AddNew()
        txtlocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code in (select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' ) "))
        If clsCommon.myLen(clsCommon.myCstr(txtlocation.Value)) > 0 Then
            LblLocDesp.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Description,'') As Description FROM TSPL_GL_SEGMENT_CODE WHERE Segment_code ='" & clsCommon.myCstr(txtlocation.Value) & "'"))
        Else
            LblLocDesp.Text = ""
        End If
        ReStoreGridLayout()
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Sub BlankAllControls()
        UsLock1.Status = ERPTransactionStatus.Pending
        txtDocNo.Value = ""
        txtDesc.Text = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtDesc.MaxLength = 250
        UcAttachment1.BlankAllControls()
        lblTotalDocAmt.Text = ""
    End Sub

    Sub LoadBlankGrid()
        gv1.Columns.Clear()
        gv1.DataSource = Nothing
        Dim repoAmtAfterTax = New GridViewTextBoxColumn()
        Dim repoDedcutionCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Dim repoDedcutionDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colSNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoVendorCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVendorCode.FormatString = ""
        repoVendorCode.HeaderText = "Vendor Code"
        repoVendorCode.Name = colVendorCode
        repoVendorCode.Width = 100
        repoVendorCode.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoVendorCode)

        Dim repoVlcUploderCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVlcUploderCode.FormatString = ""
        repoVlcUploderCode.HeaderText = "Vlc Uploder Code"
        repoVlcUploderCode.Name = colVlcUploderCode
        repoVlcUploderCode.Width = 150
        repoVlcUploderCode.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoVlcUploderCode)

        Dim repoVendorNme As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVendorNme.FormatString = ""
        repoVendorNme.HeaderText = "Vendor Name"
        repoVendorNme.Name = colVendorName
        repoVendorNme.Width = 150
        repoVendorNme.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoVendorNme)

        Dim repoAmt As GridViewCalculatorColumn = New GridViewCalculatorColumn()
        repoAmt.FormatString = ""
        repoAmt.HeaderText = "Amount"
        repoAmt.Name = colAmt
        repoAmt.Width = 80
        repoAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAmt)

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try

            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column.FieldName.StartsWith("_CFLD_") Then
                        clsCustomFieldGrid.getFinderForCustomFieldGrid(gv1, e.Column.Name.ToString, MyBase.Form_ID)
                    End If
                    If (clsCommon.CompairString(e.Column.Name, colVlcUploderCode) = CompairStringResult.Equal) Then
                        If (clsCommon.CompairString(e.Column.Name, colVlcUploderCode) = CompairStringResult.Equal) Then
                            OpenVlcUploder(False)
                        End If
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            isInsideLoadData = False
            isCellValueChangedOpen = False
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
        End Try
    End Sub




    Private Sub OpenVlcUploder(ByVal isButtonClick As Boolean)
        If clsCommon.myLen(txtlocation.Value) <= 0 Then
            Throw New Exception("Please select location first.")
        End If
        Dim Qry As String = " select TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader as [Code], M.Vendor_Code AS [Vendor Code], m.Vendor_Name as [Vendor Name],ISNULL(m.alies_name,'') As [Alies Name], TSPL_VLC_MASTER_HEAD.MCC as [MCC Code],TSPL_MCC_MASTER.MCC_Name as [MCC Name],TSPL_MCC_MASTER.Plant_Code as [Plant Code],TSPL_LOCATION_MASTER.Location_Desc as [Plant Name],(m.Add1+(case when m.Add2='' then '' else ',' end)+m.Add2) as [Address],m.Vendor_Group_Code as [Vendor Group Code],m.Vendor_Group_Code_Desc as [Vendor Group Desc],s.Acct_Set_Code as [Vendor Account Set],s.Acct_Set_Desc as [Vendor Account Set Desc] from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code " &
                               " left outer Join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = M.Vendor_Code " &
                               " Left Outer Join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_VLC_MASTER_HEAD.MCC " &
                               " Left Outer Join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_MCC_MASTER.Plant_Code "
        Dim whr As String = " m.Status='N' and len( isnull (TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader,'')) > 0 "
        If clsCommon.myLen(txtMCC.Text) > 0 Then
            whr += " and TSPL_VLC_MASTER_HEAD.MCC = '" + txtMCC.Text + "' "
        End If
        gv1.CurrentRow.Cells(colVlcUploderCode).Value = clsCommon.ShowSelectForm("TtS@vlc", Qry, "Code", whr, clsCommon.myCstr(gv1.CurrentRow.Cells(colVlcUploderCode).Value), "Code", isButtonClick)
        isInsideLoadData = True
        gv1.CurrentRow.Cells(colVendorCode).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_VLC_MASTER_HEAD.VSP_Code from TSPL_VLC_MASTER_HEAD where   VLC_CODE_VLC_Uploader = '" + gv1.CurrentRow.Cells(colVlcUploderCode).Value + "'"))
        gv1.CurrentRow.Cells(colVendorName).Value = clsVendorMaster.GetName(clsCommon.myCstr(gv1.CurrentRow.Cells(colVendorCode).Value), Nothing)
        isInsideLoadData = False
    End Sub


    Private Sub gv1_UserAddedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserAddedRow
        For i As Integer = 0 To gv1.Rows.Count - 1
            gv1.Rows(0).Cells(0).Value = 1
            If i <> 0 Then
                gv1.Rows(i).Cells(colSNo).Value = i + 1
            End If
        Next
    End Sub
    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    Sub AddNew()
        txtPaymentCycleNo.Text = ""
        txtFiscalYear.Text = ""
        txtlocation.Value = ""
        LblLocDesp.Text = ""
        txtlocation.Enabled = True
        BlankAllControls()
        LoadBlankGrid()
        btnSave.Text = "Save"
        txtDocNo.MyReadOnly = False
        txtDate.Enabled = True
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        UcAttachment1.BlankAllControls()
        gv1.Rows.AddNew()
        isNewEntry = True
        txtMCC.Text = ""
        lblMCC.Text = ""
        ReStoreGridLayout()
    End Sub

    Function AllowToSave() As Boolean
        Try
            If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
                txtDate.Focus()
                Return False
            End If
            btnSave.Focus()

            If clsCommon.myLen(txtlocation.Value) <= 0 Then
                txtlocation.Focus()
                Throw New Exception("Please first select Location")
            End If

            Dim k As Integer
            Dim vendorcount As Integer = 0
            For k = 0 To gv1.Rows.Count - 1
                Dim strOuterVendorCode As String = clsCommon.myCstr(gv1.Rows(k).Cells(colVendorCode).Value)
                If clsCommon.myLen(strOuterVendorCode) > 0 Then
                    If clsCommon.myCdbl(gv1.Rows(k).Cells(colAmt).Value) <= 0 Then
                        Throw New Exception(" Please enter Amount at Row no  " & clsCommon.myCstr(k + 1))
                    End If
                    vendorcount = vendorcount + 1
                End If
            Next
            If vendorcount <= 0 Then
                Throw New Exception(" Please enter vendor atleast in one row")
            End If

            UcAttachment1.AllowToSave()
            Return True
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
            'Throw New Exception(ex.Message)
        End Try
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData(False)
    End Sub


    Sub SaveData(ByVal isPost As Boolean)
        Try
            If (AllowToSave()) Then
                Dim obj As New clsTransferToSaving()
                obj.Document_No = txtDocNo.Value
                obj.Document_Date = txtDate.Value
                obj.Loc_Seg_Code = txtlocation.Value
                obj.MCC_Code = txtMCC.Text
                obj.MCC_Name = lblMCC.Text
                obj.Remarks = txtDesc.Text
                obj.Arr = New List(Of clsTransferToSavingDetail)
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsTransferToSavingDetail()
                    objTr.VLCUploderCode = clsCommon.myCstr(grow.Cells(colVlcUploderCode).Value)
                    objTr.Vendor_Name = clsCommon.myCstr(grow.Cells(colVendorName).Value)
                    objTr.Vendor_Code = clsCommon.myCstr(grow.Cells(colVendorCode).Value)
                    objTr.Amount = clsCommon.myCdbl(grow.Cells(colAmt).Value)
                    If ((clsCommon.myLen(objTr.Vendor_Code) > 0) And objTr.Amount > 0) Then
                        obj.Arr.Add(objTr)
                    End If
                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please select at lease one DCS", Me.Text)
                    Return
                End If
                If (obj.SaveData(obj, isNewEntry)) Then
                    UcAttachment1.SaveData(obj.Document_No)
                    txtDocNo.Value = obj.Document_No
                    If Not isPost Then
                        common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    End If
                    LoadData(obj.Document_No, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strDocumentNo As String, NavType As common.NavigatorType)
        Try
            isNewEntry = False
            txtlocation.Enabled = False
            btnSave.Enabled = True
            btnPost.Enabled = True
            btnDelete.Enabled = True
            isInsideLoadData = True
            btnSave.Text = "Update"
            txtDocNo.MyReadOnly = True
            BlankAllControls()
            LoadBlankGrid()

            Dim obj As New clsTransferToSaving()
            obj = clsTransferToSaving.GetData(strDocumentNo, NavType, True, Nothing)

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                End If
                UsLock1.Status = obj.Status
                txtDocNo.Value = obj.Document_No
                txtDate.Value = obj.Document_Date
                txtlocation.Value = obj.Loc_Seg_Code
                LblLocDesp.Text = obj.Loc_Seg_Name
                txtMCC.Text = obj.MCC_Code
                lblMCC.Text = obj.MCC_Name
                txtDesc.Text = obj.Remarks

                gv1.DataSource = Nothing
                gv1.AutoGenerateColumns = False
                gv1.DataSource = obj.ArrDT
                gv1.Columns(colSNo).FieldName = "SNo"
                gv1.Columns(colVlcUploderCode).FieldName = "VLC_Code_VLC_Uploader"
                gv1.Columns(colVendorCode).FieldName = "Vendor_Code"
                gv1.Columns(colVendorName).FieldName = "VLC_Name"
                gv1.Columns(colAmt).FieldName = "Amount"

                If obj.Status = ERPTransactionStatus.Pending Then
                    gv1.Rows.AddNew()
                    txtDate.Enabled = True
                Else
                    txtDate.Enabled = False
                End If
                btnPost.Visible = MyBase.isPostFlag
                UcAttachment1.LoadData(obj.Document_No)

                txtPaymentCycleNo.Text = clsGenratePaymentCycles.GetPaymentCycleNo(txtlocation.Value, txtDate.Value)
                txtFiscalYear.Text = clsGenratePaymentCycles.GetPaymentFiscalCode(txtlocation.Value, txtDate.Value)
            End If
            ReStoreGridLayout()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
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

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            Dim msg As String = ""
            Dim qry As String = ""
            Dim dt As DataTable = Nothing
            If (myMessages.postConfirm()) Then
                If Not AllowToSave() Then
                    Exit Sub
                End If
                SaveData(True)
                If (clsTransferToSaving.PostData(txtDocNo.Value)) Then
                    msg = "Successfully Posted"
                End If
                common.clsCommon.MyMessageBoxShow(msg)
                LoadData(txtDocNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If clsCancelLog.CheckForReasonOnDelete() Then
                    '' REASON FOR DELETE 
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Delete"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If
                End If
                If (clsTransferToSaving.DeleteData(txtDocNo.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtDocNo.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function


    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try

            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = "select TSPL_TRANSFER_TO_SAVING.Document_No ,convert(varchar,TSPL_TRANSFER_TO_SAVING.Document_date,103) as Document_date,TSPL_TRANSFER_TO_SAVING.Loc_seg_Code as [Location Code],TSPL_TRANSFER_TO_SAVING.MCC_CODE as [MCC Code],TSPL_MCC_MASTER.MCC_Name as [MCC Name],case when TSPL_TRANSFER_TO_SAVING.status =1  then 'Approved' else 'Pending' end as Status   
from TSPL_TRANSFER_TO_SAVING 
left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_TRANSFER_TO_SAVING.MCC_CODE"
        LoadData(clsCommon.ShowSelectForm("TrsToSav@F", qry, "Document_No", "", txtDocNo.Value, "Document_No", isButtonClicked, ""), NavigatorType.Current)

    End Sub

    Private Sub txtDocNo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDocNo.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub FrmAPInvoiceEntry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then

                Dim frm As New FrmPWD(Nothing)
                frm.strType = "MulProcDedReversAndCreate"
                frm.strCode = "MulProcDedReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnReverse.Visible = True
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
                'MessageBox.Show("You are not authorized to perform this action.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine +
                              " TSPL_VENDOR_INVOICE_HEAD   " + Environment.NewLine +
                              " TSPL_VENDOR_INVOICE_DETAIL  " + Environment.NewLine +
                              " TSPL_AP_INVOICE_SECONDARY_TRANSPORTER_DEDUTION_DETAIL (For AP Secondary Tranporter Deduction Detail) " + Environment.NewLine +
                              " TSPL_REMITTANCE (For Remittance) " + Environment.NewLine +
                              " TSPL_CUSTOM_FIELD_VALUES " + Environment.NewLine +
                              " TSPL_AP_Invoice_Asset_EMI_Details " + Environment.NewLine +
                              " TSPL_AP_Invoice_Advance_Interest " + Environment.NewLine +
                              " TSPL_APPROVAL_LEVEL_SCREEN " + Environment.NewLine +
                              " TSPL_APPROVAL_LEVEL_SCREEN_HISTORY " + Environment.NewLine +
                              " TSPL_PROVISION_ENTRY_KNOCKOFF " + Environment.NewLine +
                              " TSPL_Bulk_MILK_PURCHASE_INVOICE_HEAD (update during Journal Entry) " + Environment.NewLine +
                              " TSPL_MILK_PURCHASE_INVOICE_HEAD (update during Journal Entry) " + Environment.NewLine +
                              " TSPL_PI_HEAD (update during Journal Entry) " + Environment.NewLine +
                              " TSPL_PI_HEAD (update during Journal Entry) " + Environment.NewLine +
                              " TSPL_ADJUSTMENT_HEADER  " + Environment.NewLine +
                              " TSPL_ADJUSTMENT_DETAIL " + Environment.NewLine +
                              " TSPL_SALE_INVOICE_HEAD " + Environment.NewLine +
                              " TSPL_INVENTORY_MOVEMENT (For Store Adjustment) " + Environment.NewLine +
                              " TSPL_BATCH_ITEM (During Inventory Movement save) ")

            End If
    End Sub

    Private Sub gv1_CellBeginEdit(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellCancelEventArgs) Handles gv1.CellBeginEdit
        If TypeOf Me.gv1.CurrentColumn Is GridViewTextBoxColumn Then
            Dim editor As RadTextBoxEditor = DirectCast(Me.gv1.ActiveEditor, RadTextBoxEditor)
            Dim editorElement As RadTextBoxElement = DirectCast(editor.EditorElement, RadTextBoxElement)

        End If
    End Sub



    Private Sub gv1_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow
        UpdateAllTotals()
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(colSNo).Value = ii
        Next
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub UpdateAllTotals()
        Dim dblTotalDocAmt As Decimal = 0
        For i As Int16 = 0 To gv1.Rows.Count - 1
            dblTotalDocAmt = dblTotalDocAmt + clsCommon.myCdbl(gv1.Rows(i).Cells(colAmt).Value)
        Next
        lblTotalDocAmt.Text = Math.Round(clsCommon.myCdbl(dblTotalDocAmt), 2)
    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colSNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()

                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub txtlocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtlocation._MYValidating
        Try
            If SettShowMCCFinder Then
                Dim whrCls As String = " 1=1 "
                If Not clsMccMaster.isCurrentUserHO() Then
                    If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                        whrCls += " and  Location_Code in (" & objCommonVar.strCurrUserLocations & ")  "
                    End If
                End If
                whrCls = whrCls & " and  isnull(Loc_Segment_Code ,'')<>''  AND isnull(GIT_Type,'') <>'Y' and Location_Type ='Physical' and isnull(Is_Section,'')<>'Y' and isnull(is_sub_location,'')<>'Y' "
                Dim dr As DataRow = clsLocation.getLocSegFinderFullRow(whrCls)
                If dr Is Nothing OrElse dr.ItemArray.Count <= 0 Then
                    txtlocation.Value = ""
                    txtMCC.Text = ""
                    lblMCC.Text = ""
                    LblLocDesp.Text = ""
                    Exit Sub
                End If
                txtlocation.Value = clsCommon.myCstr(dr("LocationSegmentCode"))
                LblLocDesp.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Description  from TSPL_GL_SEGMENT_CODE WHERE  Segment_code='" & txtlocation.Value & "' "))
                txtMCC.Text = clsCommon.myCstr(dr("Code"))
                lblMCC.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtMCC.Text + "'"))
            Else
                Dim qry As String = "select distinct(Segment_code) as Code ,Description  from TSPL_GL_SEGMENT_CODE left outer join TSPL_LOCATION_MASTER on TSPL_GL_SEGMENT_CODE .Segment_code =TSPL_LOCATION_MASTER .Loc_Segment_Code "
                Dim WhrCls As String = "Seg_No = '7' AND GIT='N'"
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    WhrCls += "  and  TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
                End If
                txtlocation.Value = clsCommon.ShowSelectForm("TaTs@Loc", qry, "Code", WhrCls, txtlocation.Value, "Code", isButtonClicked)
                If clsCommon.myLen(clsCommon.myCstr(txtlocation.Value)) > 0 Then
                    LblLocDesp.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Description,'') As Description FROM TSPL_GL_SEGMENT_CODE WHERE Segment_code ='" & clsCommon.myCstr(txtlocation.Value) & "'"))
                Else
                    LblLocDesp.Text = ""
                End If
            End If

            txtPaymentCycleNo.Text = clsGenratePaymentCycles.GetPaymentCycleNo(txtlocation.Value, txtDate.Value)
            txtFiscalYear.Text = clsGenratePaymentCycles.GetPaymentFiscalCode(txtlocation.Value, txtDate.Value)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub btnReverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverse.Click
        Try
            If clsCommon.MyMessageBoxShow("Do you want to Reverse and unpost the current Document" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                '' REASON FOR DELETE 
                Dim Reason As String = ""
                Dim frm As New FrmFreeTxtBox1
                frm.Text = "Remarks for Reverse"
                frm.ShowDialog()
                If clsCommon.myLen(frm.strRmks) <= 0 Then
                    Exit Sub
                Else
                    Reason = frm.strRmks
                End If

                clsTransferToSaving.ReverseAndUnpost(txtDocNo.Value)
                clsCommon.MyMessageBoxShow(Me, "Task done Successfully", Me.Text)
                LoadData(txtDocNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub



    Private Sub txtDate_ValueChanged(sender As Object, e As EventArgs) Handles txtDate.ValueChanged
        Try
            txtPaymentCycleNo.Text = clsGenratePaymentCycles.GetPaymentCycleNo(txtlocation.Value, txtDate.Value)
            txtFiscalYear.Text = clsGenratePaymentCycles.GetPaymentFiscalCode(txtlocation.Value, txtDate.Value)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadMenuItem4_Click(sender As Object, e As EventArgs) Handles RadMenuItem4.Click
        Try
            'Dim Sql As String = " select ''  as [Vlc Uploder Code], '' as [Deduction Code], 0.00 as Amount"
            Dim Sql As String = " select ''  as [Vlc Uploder Code], '' as [Vendor Name], 0.00 as Amount"
            transportSql.ExporttoExcel(Sql, Me)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub



    Private Sub RadMenuItem6_Click(sender As Object, e As EventArgs) Handles RadMenuItem6.Click
        Try
            If clsCommon.myLen(txtlocation.Value) <= 0 Then
                Throw New Exception("Please select location first.")
            End If

            Dim gv As New RadGridView()
            Me.Controls.Add(gv)

            Dim dtError As New DataTable
            dtError.Columns.Add("Vlc Uploder Code", GetType(String))
            'dtError.Columns.Add("Deduction Code", GetType(String))
            dtError.Columns.Add("Vendor Name", GetType(String))
            dtError.Columns.Add("Amount", GetType(String))
            dtError.Columns.Add("Error", GetType(String))

            Dim qry As String = ""
            'If transportSql.importExcel(gv, "Vlc Uploder Code", "Deduction Code", "Amount") Then
            If transportSql.importExcel(gv, "Vlc Uploder Code", "Vendor Name", "Amount") Then
                Dim arr As New List(Of clsTransferToSavingDetail)

                Try
                    clsCommon.ProgressBarPercentShow()
                    Dim count As Integer = 0
                    For Each grow As GridViewRowInfo In gv.Rows
                        count = count + 1
                        clsCommon.ProgressBarPercentUpdate(((count) * 100 / (gv.Rows.Count)), "Validating Data..." & clsCommon.myCstr(count) & "/" & clsCommon.myCstr(gv.Rows.Count) & "")
                        Try
                            Dim objTr As New clsTransferToSavingDetail()
                            If clsCommon.myLen(grow.Cells("Vlc Uploder Code").Value) <= 0 Then
                                Throw New Exception("Vlc Uploder Code cannot be blank at line no " + clsCommon.myCstr(count) + " ")
                                'ElseIf clsCommon.myLen(grow.Cells("Deduction Code").Value) <= 0 Then
                                'ElseIf clsCommon.myLen(grow.Cells("Vendor Name").Value) <= 0 Then
                                'Throw New Exception("Vendor Name cannot be blank at line no " + clsCommon.myCstr(count) + "")
                            ElseIf clsCommon.myCdbl(grow.Cells("Amount").Value) <= 0 Then
                                Throw New Exception("Amount cannot be blank at line no " + clsCommon.myCstr(count) + "")
                            End If
                            objTr.VLCUploderCode = clsCommon.myCstr(grow.Cells("Vlc Uploder Code").Value)
                            qry = "select TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader, TSPL_VLC_MASTER_HEAD.VSP_Code,TSPL_VENDOR_MASTER.Vendor_Name 
                                    from TSPL_VLC_MASTER_HEAD 
                                    inner join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code where TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader='" + objTr.VLCUploderCode + "'"
                            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
                            If dt1 Is Nothing OrElse dt1.Rows.Count <= 0 Then
                                Throw New Exception("Invalid Vlc Uploder Code at line no " + clsCommon.myCstr(count) + "")
                            End If
                            objTr.VLCUploderCode = clsCommon.myCstr(dt1.Rows(0)("VLC_Code_VLC_Uploader"))
                            objTr.Vendor_Code = clsCommon.myCstr(dt1.Rows(0)("VSP_Code"))
                            objTr.Vendor_Name = clsCommon.myCstr(dt1.Rows(0)("Vendor_Name"))


                            objTr.Amount = clsCommon.myCdbl(grow.Cells("Amount").Value)
                            arr.Add(objTr)
                        Catch ex As Exception
                            Dim dr As DataRow = dtError.NewRow()
                            dr("Vlc Uploder Code") = clsCommon.myCstr(grow.Cells("Vlc Uploder Code").Value)
                            'dr("Deduction Code") = clsCommon.myCstr(grow.Cells("Deduction Code").Value)
                            dr("Vendor Name") = clsCommon.myCstr(grow.Cells("Vendor Name").Value)
                            dr("Amount") = clsCommon.myCstr(grow.Cells("Amount").Value)
                            dr("Error") = "Error At Row No [" + clsCommon.myCstr(count) + "] " + ex.Message
                            dtError.Rows.Add(dr)
                        End Try
                    Next
                    clsCommon.ProgressBarPercentHide()

                Catch ex As Exception
                    clsCommon.ProgressBarPercentHide()
                    clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                End Try

                Try
                    If dtError.Rows.Count > 0 Then
                        Dim ff As New FrmFreeGrid
                        ff.ReportID = "MULPROD"
                        ff.Text = "Transfer To Saving Fill Errors"
                        ff.dt = dtError
                        ff.ShowDialog()
                    ElseIf arr IsNot Nothing AndAlso arr.Count > 0 Then
                        qry = "Valid Row [" + clsCommon.myCstr(arr.Count) + "]" + Environment.NewLine + " " + Environment.NewLine + " Do You want to Proceed"
                        If clsCommon.MyMessageBoxShow(Me, qry, Me.Text, MessageBoxButtons.YesNo) = DialogResult.Yes Then
                            clsCommon.ProgressBarPercentShow()
                            Dim ii = 0
                            Try
                                LoadBlankGrid()
                                isInsideLoadData = True
                                For Each objtr As clsTransferToSavingDetail In arr
                                    ii += 1
                                    clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (arr.Count)), "Filling Rows..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(arr.Count) & "")
                                    gv1.Rows.AddNew()
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSNo).Value = ii
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colVendorCode).Value = objtr.Vendor_Code
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colVendorName).Value = objtr.Vendor_Name
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colVlcUploderCode).Value = objtr.VLCUploderCode
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = objtr.Amount

                                Next
                                clsCommon.ProgressBarPercentHide()
                            Catch ex As Exception
                                clsCommon.ProgressBarPercentHide()
                                Throw New Exception(ex.Message)
                            Finally
                                isInsideLoadData = False
                            End Try
                            UpdateAllTotals()
                            clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                        End If
                    Else
                        Throw New Exception("No Valid Rows Found to Save")
                    End If
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem5_Click(sender As Object, e As EventArgs) Handles RadMenuItem5.Click
        Try
            Dim Sql As String = "select replace(convert(varchar, GetDate(),106),' ','/') as Date,'' as Location,'' as [Deduction/Addition], ''  as [Vlc Uploder Code], '' as [Deduction Code], 0.00 as Amount"
            transportSql.ExporttoExcel(Sql, Me)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub RadMenuItem7_Click(sender As Object, e As EventArgs) Handles RadMenuItem7.Click
        '        Try
        '            Dim gv As New RadGridView()
        '            Me.Controls.Add(gv)

        '            Dim dtError As New DataTable
        '            dtError.Columns.Add("Date", GetType(String))
        '            dtError.Columns.Add("Location", GetType(String))
        '            dtError.Columns.Add("Deduction/Addition", GetType(String))
        '            dtError.Columns.Add("Vlc Uploder Code", GetType(String))
        '            dtError.Columns.Add("Deduction Code", GetType(String))
        '            dtError.Columns.Add("Amount", GetType(String))
        '            dtError.Columns.Add("Error", GetType(String))
        '            Dim indxSuccess As Integer = 0
        '            Dim qry As String = ""
        '            If transportSql.importExcel(gv, "Date", "Location", "Vlc Uploder Code", "Deduction/Addition", "Deduction Code", "Amount") Then
        '                Dim arr As New Dictionary(Of String, clsTransferToSaving)
        '                Try
        '                    clsCommon.ProgressBarPercentShow()
        '                    Dim count As Integer = 0
        '                    For Each grow As GridViewRowInfo In gv.Rows
        '                        count = count + 1
        '                        clsCommon.ProgressBarPercentUpdate(((count) * 100 / (gv.Rows.Count)), "Validating Data..." & clsCommon.myCstr(count) & "/" & clsCommon.myCstr(gv.Rows.Count) & "")
        '                        Try
        '                            If clsCommon.myLen(clsCommon.myCstr(grow.Cells("Date").Value)) > 0 AndAlso clsCommon.myLen(clsCommon.myCstr(grow.Cells("Location").Value)) > 0 AndAlso
        '                                 clsCommon.myLen(clsCommon.myCstr(grow.Cells("Vlc Uploder Code").Value)) > 0 AndAlso clsCommon.myLen(clsCommon.myCstr(grow.Cells("Deduction/Addition").Value)) > 0 AndAlso
        '                                 clsCommon.myLen(clsCommon.myCstr(grow.Cells("Deduction Code").Value)) > 0 AndAlso clsCommon.myCDecimal(grow.Cells("Amount").Value) > 0 Then

        '                                Dim objImport As New clsTransferToSaving()
        '                                Try
        '                                    objImport.Document_Date = clsCommon.myCDate(grow.Cells("Date").Value)
        '                                Catch ex As Exception
        '                                    Throw New Exception("Improper Date [" + clsCommon.myCstr(grow.Cells("Date").Value) + "] It Shoule be in [dd/MMM/yyyy] format ")
        '                                End Try

        '                                objImport.loc_code = clsCommon.myCstr(grow.Cells("Location").Value)
        '                                qry = "select distinct(Segment_code) as Code ,Description  from TSPL_GL_SEGMENT_CODE left outer join TSPL_LOCATION_MASTER on TSPL_GL_SEGMENT_CODE .Segment_code =TSPL_LOCATION_MASTER .Loc_Segment_Code 
        'where  Seg_No = '7' AND GIT='N' and Segment_code='" + objImport.loc_code + "' "
        '                                objImport.loc_code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        '                                objImport.Trans_Type = clsCommon.myCstr(grow.Cells("Deduction/Addition").Value)
        '                                If clsCommon.CompairString(objImport.Trans_Type, "Deduction") = CompairStringResult.Equal Then
        '                                    objImport.Trans_Type = "Deduction"
        '                                ElseIf clsCommon.CompairString(objImport.Trans_Type, "Addition") = CompairStringResult.Equal Then
        '                                    objImport.Trans_Type = "Addition"
        '                                Else
        '                                    Throw New Exception("Improper Deduction/Addition [" + clsCommon.myCstr(grow.Cells("Deduction/Addition").Value) + "] It Shoule be in [Deduction] or [Addition] ")
        '                                End If

        '                                Dim objImportTR As New clsTransferToSavingDetail()
        '                                If clsCommon.myLen(grow.Cells("Vlc Uploder Code").Value) <= 0 Then
        '                                    Throw New Exception("Vlc Uploder Code cannot be blank at line no " + clsCommon.myCstr(count) + " ")
        '                                ElseIf clsCommon.myLen(grow.Cells("Deduction Code").Value) <= 0 Then
        '                                    Throw New Exception("Deduction Code cannot be blank at line no " + clsCommon.myCstr(count) + "")
        '                                ElseIf clsCommon.myCdbl(grow.Cells("Amount").Value) <= 0 Then
        '                                    Throw New Exception("Deduction Code cannot be blank at line no " + clsCommon.myCstr(count) + "")
        '                                End If
        '                                objImportTR.VLCUploderCode = clsCommon.myCstr(grow.Cells("Vlc Uploder Code").Value)
        '                                qry = "select TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader, TSPL_VLC_MASTER_HEAD.VSP_Code,TSPL_VENDOR_MASTER.Vendor_Name 
        'from TSPL_VLC_MASTER_HEAD 
        'inner join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code where TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader='" + objImportTR.VLCUploderCode + "'"
        '                                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
        '                                If dt1 Is Nothing OrElse dt1.Rows.Count <= 0 Then
        '                                    Throw New Exception("Invalid Vlc Uploder Code at line no " + clsCommon.myCstr(count) + "")
        '                                End If
        '                                objImportTR.VLCUploderCode = clsCommon.myCstr(dt1.Rows(0)("VLC_Code_VLC_Uploader"))
        '                                objImportTR.Vendor_Code = clsCommon.myCstr(dt1.Rows(0)("VSP_Code"))
        '                                objImportTR.Vendor_Name = clsCommon.myCstr(dt1.Rows(0)("Vendor_Name"))

        '                                objImportTR.DeductionCode = clsCommon.myCstr(grow.Cells("Deduction Code").Value)
        '                                qry = "select TSPL_DEDUCTION_MASTER.Code,TSPL_DEDUCTION_MASTER.Description,TSPL_DEDUCTION_MASTER.GL_Account_Code,TSPL_GL_ACCOUNTS.Description as GL_Account_Name 
        'from TSPL_DEDUCTION_MASTER 
        'left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_DEDUCTION_MASTER.GL_Account_Code
        'where TSPL_DEDUCTION_MASTER.Code='" + objImportTR.DeductionCode + "'"
        '                                dt1 = clsDBFuncationality.GetDataTable(qry)
        '                                If dt1 Is Nothing OrElse dt1.Rows.Count <= 0 Then
        '                                    Throw New Exception("Invalid Deduction Code at line no " + clsCommon.myCstr(count) + "")
        '                                End If
        '                                objImportTR.DeductionCode = clsCommon.myCstr(dt1.Rows(0)("Code"))
        '                                objImportTR.Deduction_Desc = clsCommon.myCstr(dt1.Rows(0)("Description"))
        '                                objImportTR.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(dt1.Rows(0)("GL_Account_Code")), txtlocation.Value, True, Nothing)
        '                                objImportTR.GL_Account_Desc = clsCommon.myCstr(dt1.Rows(0)("GL_Account_Name"))

        '                                qry = "Select TSPL_VENDOR_MASTER.Vendor_Account,TSPL_VENDOR_MASTER.Terms_Code ,TSPL_TERMS_MASTER.Terms_Desc,TSPL_TERMS_MASTER.No_Days   from TSPL_VENDOR_MASTER left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code =TSPL_VENDOR_MASTER.Terms_Code where TSPL_VENDOR_MASTER.Vendor_Code ='" & objImportTR.Vendor_Code & "'"
        '                                dt1 = clsDBFuncationality.GetDataTable(qry)
        '                                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
        '                                    objImportTR.Terms_Code = clsCommon.myCstr(dt1.Rows(0)("Terms_Code"))
        '                                    objImportTR.Terms_Description = clsCommon.myCstr(dt1.Rows(0)("Terms_Desc"))
        '                                    objImportTR.Due_Date = clsCommon.myCDate(txtDate.Value).AddDays(clsCommon.myCdbl(dt1.Rows(0)("No_Days")))
        '                                    objImportTR.Account_Set = clsCommon.myCstr(dt1.Rows(0)("Vendor_Account"))
        '                                    objImportTR.Vendor_Control_AC = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Payable_Account  from TSPL_VENDOR_ACCOUNT_SET where Acct_Set_Code='" + clsCommon.myCstr(dt1.Rows(0)("Vendor_Account")) + "'"))
        '                                Else
        '                                    Throw New Exception("Please enter Terms Code for Vendor " & objImportTR.Vendor_Code & " in Vendor Master")
        '                                End If
        '                                objImportTR.Amount = clsCommon.myCdbl(grow.Cells("Amount").Value)


        '                                Dim UniqueCombination As String = objImport.loc_code + clsCommon.GetPrintDate(objImport.Document_Date, "dd/MM/yyyy") + objImport.Trans_Type
        '                                If Not arr.ContainsKey(UniqueCombination) Then
        '                                    objImport.Arr = New List(Of clsTransferToSavingDetail)
        '                                    arr.Add(UniqueCombination, objImport)
        '                                End If
        '                                objImportTR.Line_No = arr(UniqueCombination).Arr.Count + 1
        '                                arr(UniqueCombination).Arr.Add(objImportTR)
        '                                indxSuccess += 1
        '                            End If
        '                        Catch ex As Exception
        '                            Dim dr As DataRow = dtError.NewRow()
        '                            dr("Date") = clsCommon.myCstr(grow.Cells("Date").Value)
        '                            dr("Location") = clsCommon.myCstr(grow.Cells("Location").Value)
        '                            dr("Deduction/Addition") = clsCommon.myCstr(grow.Cells("Deduction/Addition").Value)
        '                            dr("Vlc Uploder Code") = clsCommon.myCstr(grow.Cells("Vlc Uploder Code").Value)
        '                            dr("Deduction Code") = clsCommon.myCstr(grow.Cells("Deduction Code").Value)
        '                            dr("Amount") = clsCommon.myCstr(grow.Cells("Amount").Value)
        '                            dr("Error") = "Error At Row No [" + clsCommon.myCstr(count) + "] " + ex.Message
        '                            dtError.Rows.Add(dr)
        '                        End Try
        '                    Next
        '                    clsCommon.ProgressBarPercentHide()

        '                Catch ex As Exception
        '                    clsCommon.ProgressBarPercentHide()
        '                    clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        '                End Try

        '                Try
        '                    If dtError.Rows.Count > 0 Then
        '                        Dim ff As New FrmFreeGrid
        '                        ff.ReportID = "MULMDED"
        '                        ff.Text = "Multiple Deduction Fill Errors"
        '                        ff.dt = dtError
        '                        ff.ShowDialog()
        '                    ElseIf arr IsNot Nothing AndAlso arr.Count > 0 Then
        '                        qry = "Valid Row [" + clsCommon.myCstr(indxSuccess) + "]" + Environment.NewLine + "Total Documents To be Generate [" + clsCommon.myCstr(arr.Count) + "] " + Environment.NewLine + " Do You want to Proceed"
        '                        If clsCommon.MyMessageBoxShow(Me, qry, Me.Text, MessageBoxButtons.YesNo) = DialogResult.Yes Then
        '                            clsCommon.ProgressBarPercentShow()
        '                            Dim ii As Integer = 0
        '                            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        '                            Try
        '                                For Each key As String In arr.Keys
        '                                    ii += 1
        '                                    clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (arr.Count)), "Saving Document..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(arr.Count) & "")
        '                                    Dim obj As clsTransferToSaving = arr.Item(key)
        '                                    obj.SaveData(obj, True, trans)
        '                                Next
        '                                trans.Commit()
        '                            Catch ex As Exception
        '                                trans.Rollback()
        '                                Throw New Exception(ex.Message)
        '                            Finally
        '                                clsCommon.ProgressBarPercentHide()
        '                            End Try
        '                            clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
        '                        End If
        '                    Else
        '                        Throw New Exception("No Valid Rows Found to Save")
        '                    End If
        '                Catch ex As Exception
        '                    Throw New Exception(ex.Message)
        '                End Try
        '            End If
        '        Catch ex As Exception
        '            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        '        End Try
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        'Try
        '    Dim arrHeader As New List(Of String)
        '    arrHeader.Add("Print Date(" + clsCommon.GetPrintDate(txtDate.Value, "dd-MMM-yyyy hh:mm:ss tt") + ")")
        '    arrHeader.Add("Type(" + ddlType.SelectedItem.Text + ")")

        '    If gv1.Rows.Count > 0 Then
        '        'transportSql.applyExportTemplate(gv1, MyBase.Form_ID)
        '        transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
        '    Else
        '        Throw New Exception("No data found to export.")

        '    End If
        'Catch ex As Exception
        '    common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        'End Try
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(MyBase.Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub btnSaveLayout_Click(sender As Object, e As EventArgs) Handles btnSaveLayout.Click
        Dim ReportID As String = MyBase.Form_ID
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub btnDeleteLayout_Click(sender As Object, e As EventArgs) Handles btnDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub
End Class
