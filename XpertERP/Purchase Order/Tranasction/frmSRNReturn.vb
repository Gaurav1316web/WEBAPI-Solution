'' RICHA BM00000008220
Imports common
Imports System.IO

Public Class frmSRNReturn
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
        'If Not clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If

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
        lblVendorCode.Text = ""
        lblVendorName.Text = ""
        lblBillToLocationCode.Text = ""
        lblBillToLocationName.Text = ""
        lblBaseAmt.Text = ""
        lblDiscountAmt.Text = ""
        lblAmtAfterDiscount.Text = ""
        lblTaxAmt.Text = ""
        lblAddCharges.Text = ""
        lblDocAmt.Text = ""
        lblAcceptedAmt.Text = ""
        lblRejectedAmt.Text = ""
        lblShortageAmt.Text = ""
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
                Dim obj As New clsSRNReturn()
                obj.Document_No = txtCode.Value
                obj.Document_Date = txtDate.Value
                obj.Remarks = txtRmks.Text
                obj.SRN_No = txtSRNNo.Value
                obj.Bill_To_Location = lblBillToLocationCode.Text
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
            'isNewEntry = False
            AddNew()
            Dim obj As clsSRNReturn = clsSRNReturn.GetData(strDocumentNo, navType, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
                btnSave.Enabled = False

                txtCode.Value = obj.Document_No
                txtDate.Value = obj.Document_Date
                txtSRNNo.Value = obj.SRN_No
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
        Dim qry As String = "select TSPL_SRN_RETURN.Document_No as Code,CONVERT(varchar,TSPL_SRN_RETURN.Document_Date,103) as DocumentDate,TSPL_SRN_RETURN.SRN_No,TSPL_SRN_RETURN.Remarks from TSPL_SRN_RETURN" & _
        " left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD .SRN_No=TSPL_SRN_RETURN.SRN_No"
        Dim whrClas As String = ""
        LoadData(clsCommon.ShowSelectForm("SRNRetDocNo", qry, "Code", whrClas, txtCode.Value, "Code", isButtonClicked, "Document_Date"), NavigatorType.Current)
    End Sub

    Private Sub frmPurchaseRequistion_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.Alt AndAlso e.KeyCode = Keys.N Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            'Add Tool tip Task No- TEC/22/05/18-000245
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine + _
                                                 "TSPL_SRN_RETURN " + Environment.NewLine + _
                                                 "TSPL_JOB_WORK_OUTWARD_TRANSFER_RETURN " + Environment.NewLine + _
                                                 "TSPL_SERIAL_ITEM " + Environment.NewLine + _
                                                 "TSPL_INVENTORY_MOVEMENT " + Environment.NewLine + _
                                                 "TSPL_JOURNAL_MASTER " + Environment.NewLine + _
                                                 "TSPL_JOURNAL_DETAILS ")
            'Add Tool tip Task No- TEC/22/05/18-000245
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "SIRC"
            frm.strCode = "SIReversAndCreate"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnDelete.Visible = True
            End If
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If txtCode.Value = "" Then
            myMessages.blankValue(Me, "Requisition Number", Me.Text)

        Else
            funPrint()
        End If

    End Sub

    Private Sub funPrint()

    End Sub

    Private Sub txtVendor__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtSRNNo._MYValidating
        Dim qry As String = "select TSPL_SRN_HEAD.SRN_NO as DocNo,CONVERT(varchar, TSPL_SRN_HEAD.SRN_Date,103) as DocDate,TSPL_SRN_HEAD.Vendor_Code,TSPL_SRN_HEAD.Vendor_Name,TSPL_SRN_HEAD.Bill_To_Location ,TSPL_LOCATION_MASTER.Location_Desc from TSPL_SRN_HEAD left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SRN_HEAD.Bill_To_Location"
        Dim whrcls As String = "TSPL_SRN_HEAD.Status=1  and not exists(select 1 from TSPL_PI_DETAIL where TSPL_PI_DETAIL.SRN_Id=TSPL_SRN_HEAD.SRN_NO) and not exists(select 1 from TSPL_SRN_RETURN where TSPL_SRN_RETURN.SRN_No=TSPL_SRN_HEAD.SRN_NO) and TSPL_SRN_HEAD.IsCancel=0"
        txtSRNNo.Value = clsCommon.ShowSelectForm("SRNRetSRNFnd", qry, "DocNo", whrcls, txtSRNNo.Value, "", isButtonClicked)
        LoadSRNData()
    End Sub

    Private Sub LoadSRNData()
        blankSRNFields()
        Dim obj As clsSRNHead = clsSRNHead.GetData(txtSRNNo.Value, NavigatorType.Current)
        If obj IsNot Nothing Then
            lblVendorCode.Text = obj.Vendor_Code
            lblVendorName.Text = obj.Vendor_Name
            lblBillToLocationCode.Text = obj.Bill_To_Location
            lblBillToLocationName.Text = obj.BillToLocationName
            lblBaseAmt.Text = clsCommon.myFormat(obj.Discount_Base)
            lblDiscountAmt.Text = clsCommon.myFormat(obj.Discount_Amt)
            lblAmtAfterDiscount.Text = clsCommon.myFormat(obj.Amount_Less_Discount)
            lblTaxAmt.Text = clsCommon.myFormat(obj.Total_Tax_Amt)
            lblAddCharges.Text = clsCommon.myFormat(obj.Total_Add_Charge)
            lblDocAmt.Text = clsCommon.myFormat(obj.SRN_Total_Amt)
            lblAcceptedAmt.Text = clsCommon.myFormat(obj.Total_Accepted_Amount)
            lblRejectedAmt.Text = clsCommon.myFormat(obj.Total_Rejected_Amount)
            lblShortageAmt.Text = clsCommon.myFormat(obj.Total_Shortage_Amount)
        End If
    End Sub

    Private Sub btnReverse_Click(sender As Object, e As EventArgs) Handles btnReverse.Click
        'If clsCommon.myLen(txtCode.Value) <= 0 Then
        '    clsCommon.MyMessageBoxShow("Select document.")
        'End If
        'clsSRNReturn.UnpostData(txtCode.Value)
        'LoadData(txtCode.Value, NavigatorType.Current)
        'btnSave.Enabled = True
        'btnReverse.Enabled = False
        'clsCommon.MyMessageBoxShow("Document Reverse successfully")
    End Sub

    Private Sub txtSRNNo__MYOpenMasterForm(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtSRNNo._MYOpenMasterForm
        'BHA/27/06/18-000099 by balwinder on 28/04/2018
        If clsCommon.myLen(txtSRNNo.Value) > 0 Then
            MDI.ShowForm(clsUserMgtCode.mbtnSRN, "", True, txtSRNNo.Value)
        End If
    End Sub
    ' Ticket No : TEC/29/10/18-000354 By Prabhakar
    Private Sub btnShowInventory_Click(sender As Object, e As EventArgs) Handles btnShowInventory.Click
        clsOpenInventory.ShowInventoryDatails(txtCode.Value)
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Select document.", Me.Text)
            Exit Sub
        End If
        If clsSRNReturn.DeleteData(txtCode.Value) Then
            AddNew()
            clsCommon.MyMessageBoxShow(Me, "Document Delete successfully", Me.Text)
        End If
    End Sub

    Private Sub btnNewHistory_Click(sender As Object, e As EventArgs) Handles btnNewHistory.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select Document Code", Me.Text)
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowHistoryData(txtCode.Value, "Document_No", "TSPL_SRN_RETURN")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class
