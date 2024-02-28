'' RICHA BM00000008220
Imports common
Imports System.IO

Public Class frmTankerDispatchReturn
    Inherits FrmMainTranScreen

#Region "Variables"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
#End Region

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.MCCDispatchReturn)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        If MyBase.isReverse Then
            btnReverse.Enabled = True
        Else
            btnReverse.Enabled = False
        End If

    End Sub

    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
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
    End Sub

    Sub SetLength()
        txtCode.MyMaxLength = 30
        txtRmks.MaxLength = 200
    End Sub

    Sub BlankAllControls()
        txtCode.Value = ""
        txtRmks.Text = ""
        txtDate.Value = clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
        txtSRNNo.Value = ""
        blankSRNFields()
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
        End If
        ''End of For Custom Fields
        UcAttachment1.BlankAllControls()
    End Sub

    Sub blankSRNFields()
        lblVehicleNo.Text = ""
        lblTransporterCode.Text = ""
        lblTranporterName.Text = ""
        lblFromMCCCode.Text = ""
        lblFromMCCName.Text = ""
        lblMCCPlantCode.Text = ""
        lblMCCPlantName.Text = ""
        lblNetWeigth.Text = ""
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    Sub AddNew()
        BlankAllControls()
        isNewEntry = True
        btnSave.Enabled = True
        lblTransporterCode.Text = ""
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
            If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
                txtDate.Focus()
                Return False
            End If

            If clsCommon.myLen(txtSRNNo.Value) <= 0 Then
                txtSRNNo.Focus()
                Throw New Exception("Plese enter SRN")
            End If
            If clsCommon.myLen(txtRmks.Text) <= 0 Then
                txtRmks.Focus()
                Throw New Exception("Plese enter remarks for return")
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
        SaveData()
    End Sub

    Sub SaveData()
        Try
            If (AllowToSave()) Then
                Dim obj As New clsMCCDispatchReturn()
                obj.Document_No = txtCode.Value
                obj.Document_Date = txtDate.Value
                obj.Remarks = txtRmks.Text
                obj.Challan_No = txtSRNNo.Value
                ''For Custom Fields
                obj.Form_ID = MyBase.Form_ID
                obj.arrCustomFields = New List(Of clsCustomFieldValues)
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.GetData(obj.arrCustomFields)
                End If
                ''End of For Custom Fields
                If (obj.SaveData(obj, isNewEntry)) Then
                    UcAttachment1.SaveData(obj.Document_No)
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(obj.Document_No, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strDocumentNo As String, ByVal navType As common.NavigatorType)
        Try
            btnSave.Enabled = True
            isInsideLoadData = True
            isNewEntry = False
            BlankAllControls()
            Dim obj As clsMCCDispatchReturn = clsMCCDispatchReturn.GetData(strDocumentNo, navType, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
                btnSave.Enabled = False
                txtCode.Value = obj.Document_No
                txtDate.Value = obj.Document_Date
                txtSRNNo.Value = obj.Challan_No
                txtRmks.Text = obj.Remarks
                LoadSRNData()
                ''For Custom Fields
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.LoadData(obj.Document_No)
                End If
                ''End of For Custom Fields
                UcAttachment1.LoadData(obj.Document_No)
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
                SaveData()
                If (ClsVendorQuotationHead.PostData(txtCode.Value)) Then
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
            Dim qst As String = "select count(*) from TSPL_SRN_RETURN where Document_No='" + txtCode.Value + "'"
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
        Dim qry As String = "select TSPL_MCC_Dispatch_Challan_Return.Document_No as Code,CONVERT(varchar,TSPL_MCC_Dispatch_Challan_Return.Document_Date,103) as DocumentDate,TSPL_MCC_Dispatch_Challan_Return.Challan_No,TSPL_MCC_Dispatch_Challan_Return.Remarks from TSPL_MCC_Dispatch_Challan_Return"
        Dim whrClas As String = ""
        LoadData(clsCommon.ShowSelectForm("TDRetDocNo", qry, "Code", whrClas, txtCode.Value, "Code", isButtonClicked), NavigatorType.Current)
    End Sub

    Private Sub frmPurchaseRequistion_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then

                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SIRC"
                frm.strCode = "SIReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnReverse.Visible = True
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
                'MessageBox.Show("You are not authorized to perform this action.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine +
                                          "========Table Name=========" + Environment.NewLine +
                                          "TSPL_MCC_DISPATCH_CHALLAN_RETURN" + Environment.NewLine +
                                          "TSPL_CUSTOM_FIELD_VALUES" + Environment.NewLine +
                                          "TSPL_INVENTORY_MOVEMENT_new" + Environment.NewLine +
                                          "TSPL_JOURNAL_DETAILS (For Journal Entry)" + Environment.NewLine +
                                          "TSPL_JOURNAL_MASTER (For Journal Entry)" + Environment.NewLine +
                                          "=========Setting Name======" + Environment.NewLine +
                                          "CreateTankerDispatchGL" + Environment.NewLine +
                                          "TransferEntryOnInvCtrlAccount" + Environment.NewLine +
                                          "SkipCogsEntry" + Environment.NewLine +
                                          "GateEntryTankerFromTankerMaster")
            End If
    End Sub

    Private Sub txtVendor__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtSRNNo._MYValidating
        Dim qry As String = "select TSPL_MCC_Dispatch_Challan.Chalan_NO as DocumentNo,convert(varchar, TSPL_MCC_Dispatch_Challan.Dispatch_Date,103) as Dispatch_Date,TSPL_MCC_Dispatch_Challan.MCC_Code,TSPL_MCC_Dispatch_Challan.MCC_Name,TSPL_MCC_Dispatch_Challan.Mcc_Or_Plant_Code,TSPL_MCC_Dispatch_Challan.Tanker_No,TSPL_TANKER_MASTER.Tanker_Transporter_Code as TranporterCode,TSPL_TANKER_MASTER.Description as TransporterName,TSPL_MCC_Dispatch_Challan.Gross_Weight, TSPL_MCC_Dispatch_Challan.Tare_Weight,TSPL_MCC_Dispatch_Challan.Net_Qty,TSPL_MCC_Dispatch_Challan.FAT_R,TSPL_MCC_Dispatch_Challan.SNF_R,TSPL_MCC_Dispatch_Challan.FAT_KG,TSPL_MCC_Dispatch_Challan.SNF_KG,TSPL_MCC_Dispatch_Challan.FAT_RATE,TSPL_MCC_Dispatch_Challan.SNF_RATE,TSPL_MCC_Dispatch_Challan.Amount " + Environment.NewLine + _
        " from TSPL_MCC_Dispatch_Challan left outer join TSPL_TANKER_MASTER on TSPL_TANKER_MASTER.Tanker_No=TSPL_MCC_Dispatch_Challan.Tanker_No" + Environment.NewLine
        Dim whrcls As String = " TSPL_MCC_Dispatch_Challan.isPosted=1 and not exists(select 1 from Tspl_Gate_Entry_Details where Tspl_Gate_Entry_Details.Challan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO ) " & _
                               " and not exists(select 1 from TSPL_MCC_Tanker_Dispatch_Return_Head where TSPL_MCC_Tanker_Dispatch_Return_Head.Chalan_NO = TSPL_MCC_Dispatch_Challan.Chalan_NO ) " & _
                               " and not exists(select 1 from TSPL_MCC_DISPATCH_CHALLAN_RETURN where TSPL_MCC_DISPATCH_CHALLAN_RETURN.Challan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO )"
        txtSRNNo.Value = clsCommon.ShowSelectForm("tankerDisRetDoc", qry, "DocumentNo", whrcls, txtSRNNo.Value, "", isButtonClicked)
        LoadSRNData()
    End Sub

    Private Sub LoadSRNData()
        blankSRNFields()
        Dim qry As String = "select TSPL_MCC_Dispatch_Challan.Chalan_NO as DocumentNo,convert(varchar, TSPL_MCC_Dispatch_Challan.Dispatch_Date,103) as Dispatch_Date,TSPL_MCC_Dispatch_Challan.MCC_Code,TSPL_MCC_Dispatch_Challan.MCC_Name,TSPL_MCC_Dispatch_Challan.Mcc_Or_Plant_Code,TSPL_MCC_Dispatch_Challan.Tanker_No,TSPL_TANKER_MASTER.Tanker_Transporter_Code as TranporterCode,TSPL_TANKER_MASTER.Description as TransporterName,TSPL_MCC_Dispatch_Challan.Gross_Weight, TSPL_MCC_Dispatch_Challan.Tare_Weight,TSPL_MCC_Dispatch_Challan.Net_Qty,TSPL_MCC_Dispatch_Challan.FAT_R,TSPL_MCC_Dispatch_Challan.SNF_R,TSPL_MCC_Dispatch_Challan.FAT_KG,TSPL_MCC_Dispatch_Challan.SNF_KG,TSPL_MCC_Dispatch_Challan.FAT_RATE,TSPL_MCC_Dispatch_Challan.SNF_RATE,TSPL_MCC_Dispatch_Challan.Amount " + Environment.NewLine + _
        " from TSPL_MCC_Dispatch_Challan left outer join TSPL_TANKER_MASTER on TSPL_TANKER_MASTER.Tanker_No=TSPL_MCC_Dispatch_Challan.Tanker_No" + Environment.NewLine + _
        " where TSPL_MCC_Dispatch_Challan.Chalan_NO='" + txtSRNNo.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            lblVehicleNo.Text = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
            lblTransporterCode.Text = clsCommon.myCstr(dt.Rows(0)("TranporterCode"))
            lblTranporterName.Text = clsCommon.myCstr(dt.Rows(0)("TransporterName"))
            lblFromMCCCode.Text = clsCommon.myCstr(dt.Rows(0)("MCC_Code"))
            lblFromMCCName.Text = clsCommon.myCstr(dt.Rows(0)("MCC_Name"))
            lblMCCPlantCode.Text = clsCommon.myCstr(dt.Rows(0)("Mcc_Or_Plant_Code"))
            lblMCCPlantName.Text = clsLocation.GetName(lblMCCPlantCode.Text, Nothing)
            lblNetWeigth.Text = clsCommon.myCstr(dt.Rows(0)("Net_Qty"))
        End If
    End Sub

    Private Sub btnReverse_Click(sender As Object, e As EventArgs) Handles btnReverse.Click
        'If clsCommon.myLen(txtCode.Value) <= 0 Then
        '    clsCommon.MyMessageBoxShow("Select document.")
        'End If
        'clsMCCDispatchReturn.UnpostData(txtCode.Value)
        'LoadData(txtCode.Value, NavigatorType.Current)
        'btnSave.Enabled = True
        'btnReverse.Enabled = False
        'clsCommon.MyMessageBoxShow("Document Reverse successfully")
    End Sub
End Class




