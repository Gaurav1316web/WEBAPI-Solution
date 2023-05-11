Imports common
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls

Public Class frmJWOTransferOtherReturn
    Inherits FrmMainTranScreen

#Region "Variables"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
#End Region

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.SRNReturn)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
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
        lblSRNDate.Text = ""
        lblLocationCode.Text = ""
        lblLocationName.Text = ""
        lblJobLocationCode.Text = ""
        lblJobLocationName.Text = ""
        lblVendorCode.Text = ""
        lblVendorName.Text = ""
        lblTankerNo.Text = ""
        lblVehicleName.Text = ""
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()

    End Sub

    Sub AddNew()
        BlankAllControls()
        isNewEntry = True
        btnSave.Enabled = True
        lblVendorCode.Text = ""
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
            '= KUNAL > TICKET : BM00000009580 =====
            If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
                txtDate.Focus()
                Return False
            End If

            If clsCommon.myLen(txtSRNNo.Value) <= 0 Then
                txtSRNNo.Focus()
                Throw New Exception("Plese enter " + txtSRNNo.MyLinkLable1.Text)
            End If
            If clsCommon.myLen(txtRmks.Text) <= 0 Then
                txtRmks.Focus()
                Throw New Exception("Plese enter remarks for return")
            End If
            UcCustomFields1.AllowToSave()
            UcAttachment1.AllowToSave()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
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
                Dim obj As New clsJWOTransferOtherReturn()
                obj.Document_No = txtCode.Value
                obj.Document_Date = txtDate.Value
                obj.Remarks = txtRmks.Text
                obj.JWO_Transfer_No = txtSRNNo.Value
                obj.JWO_SRN_From_Location_Code = lblLocationCode.Text
                ''For Custom Fields
                obj.Form_ID = MyBase.Form_ID
                obj.arrCustomFields = New List(Of clsCustomFieldValues)
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.GetData(obj.arrCustomFields)
                End If
                ''End of For Custom Fields

                If (obj.SaveData(obj, isNewEntry)) Then
                    UcAttachment1.SaveData(obj.Document_No)
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    LoadData(obj.Document_No, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub LoadData(ByVal strDocumentNo As String, ByVal navType As common.NavigatorType)
        Try
            btnSave.Enabled = True
            isInsideLoadData = True
            BlankAllControls()
            Dim obj As clsJWOTransferOtherReturn = clsJWOTransferOtherReturn.GetData(strDocumentNo, navType, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
                btnSave.Enabled = False
                isNewEntry = False
                txtCode.Value = obj.Document_No
                txtDate.Value = obj.Document_Date
                txtSRNNo.Value = obj.JWO_Transfer_No
                txtRmks.Text = obj.Remarks
                LoadTransferData()
                ''For Custom Fields
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.LoadData(obj.Document_No)
                End If
                ''End of For Custom Fields
                UcAttachment1.LoadData(obj.Document_No)
            End If
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

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        PostData()
    End Sub

    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                SaveData()
                If (ClsVendorQuotationHead.PostData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Posted Successfully ")
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_JOB_WORK_OUTWARD_TRANSFER_RETURN where Document_No='" + txtCode.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Dim qry As String = "select TSPL_JOB_WORK_OUTWARD_TRANSFER_RETURN.Document_No as Code,CONVERT(varchar,TSPL_JOB_WORK_OUTWARD_TRANSFER_RETURN.Document_Date,103) as DocumentDate,TSPL_JOB_WORK_OUTWARD_TRANSFER_RETURN.JWO_TRANSFER_NO,TSPL_JOB_WORK_OUTWARD_TRANSFER_RETURN.Remarks from TSPL_JOB_WORK_OUTWARD_TRANSFER_RETURN "
        Dim whrClas As String = ""
        LoadData(clsCommon.ShowSelectForm("JTRRetDocNo", qry, "Code", whrClas, txtCode.Value, "Code", isButtonClicked), NavigatorType.Current)
    End Sub

    Private Sub frmPurchaseRequistion_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "SIRC"
            frm.strCode = "SIReversAndCreate"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnReverse.Visible = True
            End If
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

    Private Sub txtVendor__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtSRNNo._MYValidating
        Dim qry As String = " select TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TRANSFER_NO as DocNo,CONVERT(varchar, TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TRANSFER_DATE,103) as DocDate,TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.From_Locaction ,TSPL_LOCATION_MASTER.Location_Desc,TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.To_Locaction as ToLocationCode ,ToLocationMaster.Location_Desc as ToLocationName " + Environment.NewLine + _
        " from TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD" + Environment.NewLine + _
        " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.From_Locaction" + Environment.NewLine + _
        " left outer join TSPL_LOCATION_MASTER as ToLocationMaster on ToLocationMaster.Location_Code=TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.To_Locaction " + Environment.NewLine + _
        " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.Vendor_Code"
        Dim whrcls As String = " TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.Status=1  and not exists(select 1 from TSPL_JOB_WORK_OUTWARD_TRANSFER_RETURN where TSPL_JOB_WORK_OUTWARD_TRANSFER_RETURN.JWO_TRANSFER_NO=TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TRANSFER_NO)"
        txtSRNNo.Value = clsCommon.ShowSelectForm("JWOTRetSFnd", qry, "DocNo", whrcls, txtSRNNo.Value, "", isButtonClicked)
        LoadTransferData()
    End Sub

    Private Sub LoadTransferData()
        blankSRNFields()
        Dim obj As clsJWOTransferOtherHead = clsJWOTransferOtherHead.GetData(txtSRNNo.Value, NavigatorType.Current, Nothing)
        If obj IsNot Nothing Then
            lblSRNDate.Text = clsCommon.GetPrintDate(obj.TRANSFER_DATE, "dd/MM/yyyy")
            lblLocationCode.Text = obj.From_Locaction
            lblLocationName.Text = clsLocation.GetName(obj.From_Locaction, Nothing)
            lblJobLocationCode.Text = obj.To_Locaction
            lblJobLocationName.Text = clsLocation.GetName(obj.To_Locaction, Nothing)
            lblVendorCode.Text = obj.Vendor_Code
            lblVendorName.Text = clsVendorMaster.GetName(obj.Vendor_Code, Nothing)
            lblTankerNo.Text = obj.Vehicle_Code
            lblVehicleName.Text = obj.Vehicle_No
        End If
    End Sub

    Private Sub btnReverse_Click(sender As Object, e As EventArgs) Handles btnReverse.Click
        'If clsCommon.myLen(txtCode.Value) <= 0 Then
        '    clsCommon.MyMessageBoxShow("Select document.")
        'End If
        'clsJWOTransferOtherReturn.UnpostData(txtCode.Value)
        'LoadData(txtCode.Value, NavigatorType.Current)
        'btnSave.Enabled = True
        'btnReverse.Enabled = False
        'clsCommon.MyMessageBoxShow("Document Reverse successfully")
    End Sub

End Class
