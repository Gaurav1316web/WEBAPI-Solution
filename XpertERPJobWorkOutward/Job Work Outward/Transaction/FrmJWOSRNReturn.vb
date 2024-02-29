Imports common
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls

Public Class FrmJWOSRNReturn
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
        lblSRNType.Text = ""
        lblSRNDate.Text = ""
        lblLocationCode.Text = ""
        lblLocationName.Text = ""
        lblJobLocationCode.Text = ""
        lblJobLocationName.Text = ""
        lblVendorCode.Text = ""
        lblVendorName.Text = ""
        lblTankerNo.Text = ""
        lblUnloadingNo.Text = ""
        lblGENo.Text = ""
        lblGEDate.Text = ""
        lblChallanNo.Text = ""
        lblChallanDate.Text = ""
        lblJobAmount.Text = ""
        lblDocAmt.Text = ""
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
                Throw New Exception("Plese enter SRN")
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
                Dim obj As New clsJWOSRNReturn()
                obj.Document_No = txtCode.Value
                obj.Document_Date = txtDate.Value
                obj.Remarks = txtRmks.Text
                obj.JWO_SRN_No = txtSRNNo.Value
                obj.JWO_SRN_Location_Code = lblLocationCode.Text
                obj.JWO_SRN_Document_Type = lblSRNType.Text
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
            Dim obj As clsJWOSRNReturn = clsJWOSRNReturn.GetData(strDocumentNo, navType, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
                btnSave.Enabled = False
                isNewEntry = False
                txtCode.Value = obj.Document_No
                txtDate.Value = obj.Document_Date
                txtSRNNo.Value = obj.JWO_SRN_No
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
            Dim qst As String = "select count(*) from TSPL_JWO_SRN_RETURN where Document_No='" + txtCode.Value + "'"
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
        Dim qry As String = "select TSPL_JWO_SRN_RETURN.Document_No as Code,CONVERT(varchar,TSPL_JWO_SRN_RETURN.Document_Date,103) as DocumentDate,TSPL_JWO_SRN_RETURN.JWO_SRN_No,TSPL_JWO_SRN_RETURN.Remarks from TSPL_JWO_SRN_RETURN "
        Dim whrClas As String = ""
        LoadData(clsCommon.ShowSelectForm("SRNRetDocNo", qry, "Code", whrClas, txtCode.Value, "Code", isButtonClicked), NavigatorType.Current)
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
        Dim qry As String = "select TSPL_JWO_SRN_HEAD.Document_No as DocNo,CONVERT(varchar, TSPL_JWO_SRN_HEAD.Document_Date,103) as DocDate,TSPL_JWO_SRN_HEAD.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_JWO_SRN_HEAD.Loc_Code ,TSPL_LOCATION_MASTER.Location_Desc,TSPL_JWO_SRN_HEAD.Job_Loc_Code as JobLocationCode ,JobLocationMaster.Location_Desc as JobLocationDesc from TSPL_JWO_SRN_HEAD left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_JWO_SRN_HEAD.Loc_Code left outer join TSPL_LOCATION_MASTER as JobLocationMaster on JobLocationMaster.Location_Code=TSPL_JWO_SRN_HEAD.Job_Loc_Code left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_JWO_SRN_HEAD.Vendor_Code"
        Dim whrcls As String = " TSPL_JWO_SRN_HEAD.Posted=1 and not exists(select 1 from TSPL_JWO_SRN_RETURN where TSPL_JWO_SRN_RETURN.JWO_SRN_No=TSPL_JWO_SRN_HEAD.Document_No) "
        txtSRNNo.Value = clsCommon.ShowSelectForm("JWOSRetSFnd", qry, "DocNo", whrcls, txtSRNNo.Value, "", isButtonClicked)
        LoadSRNData()
    End Sub

    Private Sub LoadSRNData()
        blankSRNFields()
        Dim obj As clsJWOSRNHead = clsJWOSRNHead.GetData(txtSRNNo.Value, NavigatorType.Current)
        If obj IsNot Nothing Then
            lblSRNType.Text = obj.Document_Type
            lblSRNDate.Text = clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy")
            lblLocationCode.Text = obj.Loc_Code
            lblLocationName.Text = obj.Loc_Name
            lblJobLocationCode.Text = obj.Job_Loc_Code
            lblJobLocationName.Text = obj.Job_Loc_Name
            lblVendorCode.Text = obj.Vendor_Code
            lblVendorName.Text = obj.Vendor_Name
            lblTankerNo.Text = obj.Tanker_No

            lblUnloadingNo.Text = obj.Unloading_No
            lblGENo.Text = obj.Gate_Entry_No
            lblGEDate.Text = IIf(obj.Gate_Entry_Date Is Nothing, "", clsCommon.GetPrintDate(obj.Gate_Entry_Date, "dd/MM/yyyy"))
            lblChallanNo.Text = obj.Challan_No
            If clsCommon.myLen(obj.Challan_No) > 0 Then
                lblChallanDate.Text = clsCommon.GetPrintDate(obj.Challan_Date, "dd/MM/yyyy")
            Else
                lblChallanDate.Text = ""
            End If
            lblJobAmount.Text = clsCommon.myFormat(obj.Total_Job_Amt)
            lblDocAmt.Text = clsCommon.myFormat(obj.Document_Amt)
        End If
    End Sub

    Private Sub btnReverse_Click(sender As Object, e As EventArgs) Handles btnReverse.Click
        'If clsCommon.myLen(txtCode.Value) <= 0 Then
        '    clsCommon.MyMessageBoxShow("Select document.")
        'End If
        'clsJWOSRNReturn.UnpostData(txtCode.Value)
        'LoadData(txtCode.Value, NavigatorType.Current)
        'btnSave.Enabled = True
        'btnReverse.Enabled = False
        'clsCommon.MyMessageBoxShow("Document Reverse successfully")
    End Sub

End Class
