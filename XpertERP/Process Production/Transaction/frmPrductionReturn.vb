'==========BM00000007888,BM00000007949,BM00000007972============================
Imports common
Imports System.IO

Public Class frmPrductionReturn
    Inherits FrmMainTranScreen

#Region "Variables"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim arrLoc As String = Nothing
    Dim AllowGateReturn As Integer = 0
#End Region

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.TransferReturn)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
    End Sub

    Private Sub MCCLOCATIONFINDER()
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData()

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                arrLoc = obj.arrLocCodes
            Else
                'fndMCCCode.Enabled = False
                'Throw New Exception("Please Set Default Location Of LogIn User")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Public STIRDocOpenTrans As String = Nothing
    Private Sub OpenFormByEnumreation()
        Try
            If STIRDocOpenTrans IsNot Nothing AndAlso STIRDocOpenTrans.Length > 0 Then
                LoadData(STIRDocOpenTrans, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub frmPrductionReturn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try           
            SetUserMgmtNew()          
            If AllowGateReturn = 0 Then
                pnlGateReturn.Visible = False
            Else
                pnlGateReturn.Visible = True
            End If
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
            GetDocumentType()
            MCCLOCATIONFINDER()
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
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub GetDocumentType()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Name")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "Production Standardization"
        dr("Name") = "STD"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "Production Entry"
        dr("Name") = "PE"
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
        fndGateReturnNo.Value = ""
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
            '=============================Added by preeti Gupta=====================
            If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
                txtDate.Focus()
                Return False
            End If
            '=======================================================================
            If clsCommon.myLen(txtSRNNo.Value) <= 0 Then
                txtSRNNo.Focus()
                Throw New Exception("Plese enter Transfer No")
            End If
            If clsCommon.myLen(txtRmks.Text) <= 0 Then
                txtRmks.Focus()
                Throw New Exception("Plese enter remarks for return")
            End If

            If clsCommon.myCDate(clsDBFuncationality.getSingleValue("Select CONVERT(date, DOCUMENT_DATE,103) from TSPL_TRANSFER_ORDER_HEAD WHERE DOCUMENT_NO='" & txtSRNNo.Value & "' ")) > clsCommon.myCDate(txtDate.Value) Then
                txtDate.Focus()
                Throw New Exception("Date cannot be less than from Transfer No Date")
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
        If clsCommon.MyMessageBoxShow("Do you want to save this record.", "Save", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
            SaveData()
        End If
    End Sub

    Sub SaveData()
        Try
            If (AllowToSave()) Then
                Dim obj As New clsTransferReturn()
                obj.Gate_ReturnNo = fndGateReturnNo.Value
                obj.Document_No = txtCode.Value
                obj.Document_Date = txtDate.Value
                obj.Remarks = txtRmks.Text
                obj.Transfer_No = txtSRNNo.Value
                obj.Document_Type = cmbDocumentType.SelectedValue
                obj.Bill_To_Location = lblBillToLocationCode.Text
                obj.Ship_To_Location = lblVendorCode.Text ' prabhakar
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
            isNewEntry = False
            BlankAllControls()
            Dim obj As clsTransferReturn = clsTransferReturn.GetData(strDocumentNo, navType, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
                btnSave.Enabled = False
                fndGateReturnNo.Value = obj.Gate_ReturnNo
                txtCode.Value = obj.Document_No
                txtDate.Value = obj.Document_Date
                txtSRNNo.Value = obj.Transfer_No
                cmbDocumentType.SelectedValue = obj.Document_Type
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
            Dim qst As String = "select count(*) from TSPL_SRN_RETURN where Document_No='" + txtCode.Value + "'"
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
        Dim sQuery As String = String.Empty
        sQuery = "select Document_No as [Code],convert(date,Document_Date,103) as [Document Date],Transfer_No as [Transfer No],Remarks from TSPL_TRANSFER_RETURN"
        LoadData(clsCommon.ShowSelectForm("SRNRetDocNoKK", sQuery, "Code", "", txtCode.Value, "TSPL_TRANSFER_RETURN.Document_Date", isButtonClicked, "TSPL_TRANSFER_RETURN.Document_Date"), NavigatorType.Current)
    End Sub

    Private Sub frmPurchaseRequistion_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.Alt AndAlso e.KeyCode = Keys.N Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
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

    Private Sub txtSRNNo__MYOpenMasterForm(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtSRNNo._MYOpenMasterForm
        Try
            Dim frm As New FrmTransferKDIL
            frm.Tag = txtSRNNo.Value
            frm.WindowState = FormWindowState.Maximized
            frm.Show()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub
    Private Sub fndGateReturnNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndGateReturnNo._MYValidating
        fndGateReturnNo.Value = clsGateEntryReturnTransfer.getFinder("Posted=1", fndGateReturnNo.Value, isButtonClicked)
        If clsCommon.myLen(fndGateReturnNo.Value) Then
            cmbDocumentType.SelectedValue = "O"
            txtSRNNo.Value = clsDBFuncationality.getSingleValue("Select REF_DOC_No from TSPL_GATE_ENTRY_RETURN_TRANSFER where GE_CODE='" & fndGateReturnNo.Value & "'")
            If clsCommon.myLen(txtSRNNo.Value) > 0 Then
                LoadSRNData()
            End If
        End If

    End Sub
    Private Sub txtVendor__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtSRNNo._MYValidating

        Dim sQuery As String = String.Empty
        Dim whrcls As String = ""
        If clsCommon.CompairString(cmbDocumentType.SelectedValue.ToString, "O") = CompairStringResult.Equal Then
            sQuery = "select TSPL_TRANSFER_ORDER_HEAD.Document_NO as DocNo,CONVERT(varchar, TSPL_TRANSFER_ORDER_HEAD.Document_Date,103) as DocDate,From_Location as " _
                       & " [From Location Code],frm.Location_Desc as [From Location],fto.Location_Code as [To Location Code],fto.Location_Desc as [To Location],DOC_Total_Amt as [Amount]  " _
                       & " from TSPL_TRANSFER_ORDER_HEAD left join tspl_Location_Master frm on frm.location_Code=TSPL_TRANSFER_ORDER_HEAD.From_Location left join " _
                       & " tspl_Location_Master fto on fto.GIT_Location=TSPL_TRANSFER_ORDER_HEAD.to_Location left join TSPL_TRANSFER_RETURN on transfer_No=TSPL_TRANSFER_ORDER_HEAD.Document_NO"
            If clsCommon.myLen(arrLoc) > 0 Then
                whrcls = "  TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='O' and TSPL_TRANSFER_ORDER_HEAD.Status =1 and  TSPL_TRANSFER_ORDER_HEAD.Document_No not  in (select coalesce(TransferOutNo,'') from TSPL_TRANSFER_ORDER_HEAD where Document_No not in (select Transfer_No from TSPL_TRANSFER_RETURN)) and TSPL_TRANSFER_RETURN.transfer_No is Null and frm.location_Code in (" + arrLoc + ")"
            End If
        ElseIf clsCommon.CompairString(cmbDocumentType.SelectedValue.ToString, "I") = CompairStringResult.Equal Then
            sQuery = "select TSPL_TRANSFER_ORDER_HEAD.Document_NO as DocNo,CONVERT(varchar, TSPL_TRANSFER_ORDER_HEAD.Document_Date,103) as DocDate,ot.From_Location as" _
                       & " [From Location Code],frm.Location_Desc as [From Location],fto.Location_Code as [To Location Code],fto.Location_Desc as [To Location]," _
                       & " TSPL_TRANSFER_ORDER_HEAD.DOC_Total_Amt as [Amount]  from TSPL_TRANSFER_ORDER_HEAD left join TSPL_TRANSFER_ORDER_HEAD ot on ot.Document_No=" _
                       & " TSPL_TRANSFER_ORDER_HEAD.TransferOutNo left join tspl_Location_Master frm on frm.Location_Code=ot.From_Location left join tspl_Location_Master fto on " _
                       & " fto.Location_Code=TSPL_TRANSFER_ORDER_HEAD.to_Location  left join TSPL_TRANSFER_RETURN on transfer_No=TSPL_TRANSFER_ORDER_HEAD.Document_NO"
            If clsCommon.myLen(arrLoc) > 0 Then
                whrcls = "  TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' and TSPL_TRANSFER_RETURN.transfer_No is Null and TSPL_TRANSFER_ORDER_HEAD.Status =1  and frm.Location_Code in (" + arrLoc + ")"
            End If
        End If

        txtSRNNo.Value = clsCommon.ShowSelectForm("SRNRetSRNFndOI", sQuery, "DocNo", whrcls, txtSRNNo.Value, "TSPL_TRANSFER_ORDER_HEAD.Document_Date", isButtonClicked, "TSPL_TRANSFER_ORDER_HEAD.Document_Date")
        LoadSRNData()
    End Sub

    Private Sub LoadSRNData()
        blankSRNFields()
        Dim obj As clsTransferDCC = clsTransferDCC.GetData(txtSRNNo.Value, NavigatorType.Current)
        If obj IsNot Nothing Then
            lblVendorCode.Text = obj.From_Location
            lblVendorName.Text = obj.From_LocationName

            lblBillToLocationCode.Text = obj.To_Location
            lblBillToLocationName.Text = obj.To_LocationName
            Dim dt As New DataTable
            If clsCommon.CompairString(clsCommon.myCstr(cmbDocumentType.SelectedValue), "I") = CompairStringResult.Equal Then
                dt = clsDBFuncationality.GetDataTable("Select out.From_Location as Location_Code,Location_Desc from tspl_Transfer_Order_Head Left join tspl_Transfer_Order_Head out on " _
               & " out.document_No=tspl_Transfer_Order_Head.TransferOutNo left join tspl_Location_Master on out.from_Location=tspl_Location_Master.location_Code where tspl_Transfer_Order_Head.document_No='" & obj.Document_No & "'")
                If dt.Rows.Count > 0 Then
                    lblVendorCode.Text = clsCommon.myCstr(dt.Rows(0)(0))
                    lblVendorName.Text = clsCommon.myCstr(dt.Rows(0)(1))
                End If
            Else
                dt = clsDBFuncationality.GetDataTable("Select Location_Code,Location_Desc from tspl_Location_Master where GIT_Location='" & obj.To_Location & "'")
                If dt.Rows.Count > 0 Then
                    lblBillToLocationCode.Text = clsCommon.myCstr(dt.Rows(0)(0))
                    lblBillToLocationName.Text = clsCommon.myCstr(dt.Rows(0)(1))
                End If
            End If
            dt = Nothing
            lblBaseAmt.Text = clsCommon.myFormat(obj.Discount_Base)
            lblDiscountAmt.Text = clsCommon.myFormat(obj.Discount_Amt)
            lblAmtAfterDiscount.Text = clsCommon.myFormat(obj.Amount_Less_Discount)
            lblTaxAmt.Text = clsCommon.myFormat(obj.Total_Tax_Amt)
            lblDocAmt.Text = clsCommon.myFormat(obj.DOC_Total_Amt)
        End If
    End Sub


End Class
