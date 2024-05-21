Imports common
Imports System.Data.SqlClient

Public Class frmMCCTankerWeighment
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim Qry As String
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim isFlag As Boolean = False
    Dim arrLoc As String = Nothing
    Public strDocCode As String = ""
#End Region

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub LOCATIONRIGTHS()
        Dim obj As New clsMCCCodes()
        Try
            obj = clsMCCCodes.GetData()
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
            End If
            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
        End Try
    End Sub

    Private Sub FrmWeighmentEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsAutoTankerWeightment, clsFixedParameterCode.IsAutoTankerWeightment, Nothing)) = 1 Then
            Throw New Exception("Work only is Auto tanker weighment is on")
        End If
        SetUserMgmtNew()
        Reset()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Transaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Transaction")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N New Transaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Transaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")

       

        UcWeighing1.form_ID = MyBase.Form_ID
        UcWeighing1.LoadSettingAndStart()

        If clsCommon.myLen(strDocCode) > 0 Then
            LoadData(strDocCode, NavigatorType.Current)
        End If

        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
       
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmMCCWeighment)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        btnPost.Visible = MyBase.isPostFlag
    End Sub

    Sub Reset()
        txtDocumentNo.Value = ""
        ucStatusTare.Status = ERPTransactionStatus.Pending
        ucStatusGros.Status = ERPTransactionStatus.Pending
        txtDocumentDate.Value = clsCommon.GETSERVERDATE()

        lblTransporterCode.Text = ""
        lblTransporterName.Text = ""
        lblGateEntryNo.Text = ""
        txtTankerNo.Value = ""
        lblLocationCode.Text = ""
        LblLocationName.Text = ""
        txtNetWeight.Value = 0
        txtGrossWeight.Value = 0
        TxtTareWeight.Value = 0
        LOCATIONRIGTHS()

        txtDocumentNo.MyReadOnly = False
        txtGrossWeight.Enabled = False
        txtNetWeight.Enabled = False
        btnsave.Text = "Save"
        btndelete.Enabled = False
        btnPost.Enabled = True
        btnsave.Enabled = True
        isNewEntry = True
    End Sub

    Private Sub FrmWeighmentEntry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F2 Then
            Try
                If ucStatusTare.Status = ERPTransactionStatus.Pending AndAlso ucStatusGros.Status = ERPTransactionStatus.Pending Then
                    TxtTareWeight.Value = UcWeighing1.LiveReading
                ElseIf ucStatusTare.Status = ERPTransactionStatus.Approved AndAlso ucStatusGros.Status = ERPTransactionStatus.Pending Then
                    txtGrossWeight.Value = UcWeighing1.LiveReading
                End If
            Catch ex As Exception
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
            
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclose.Enabled Then
            CloseForm()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine + _
                          "========Table Name=========" + Environment.NewLine + _
                          "TSPL_MCC_WEIGHMENT" + Environment.NewLine + _
                          "=========Setting Name======" + Environment.NewLine + _
                          "IsAutoTankerWeightment")
        End If
    End Sub

    Private Sub CloseForm()
        clsERPFuncationality.closeForm(Me)
    End Sub

    Sub SaveData()
        Dim obj As New clsMCCWeighment
        Try
            If AllowToSave() Then
                If ucStatusTare.Status = ERPTransactionStatus.Pending Then
                    obj.Document_No = txtDocumentNo.Value
                    obj.Document_Date = txtDocumentDate.Value
                    obj.Gate_Entry_No = lblGateEntryNo.Text
                    obj.Location_Code = lblLocationCode.Text
                    obj.Tare_Weight = TxtTareWeight.Value
                    obj.Gross_Weight = txtGrossWeight.Value
                    clsMCCWeighment.SaveData(obj, isNewEntry)
                ElseIf ucStatusGros.Status = ERPTransactionStatus.Pending Then
                    clsMCCWeighment.SaveGrossWeightData(txtDocumentNo.Value, txtGrossWeight.Value, txtNetWeight.Value)
                    obj.Document_No = txtDocumentNo.Value
                Else
                    Throw New Exception("Error is saving data")
                End If
                clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
                LoadData(obj.Document_No, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
        End Try
    End Sub

    Private Function AllowToSave() As Boolean

        ' KUNAL > TICKET : BM00000009575 =====
        If AllowFutureDateTransaction(txtDocumentDate.Value, Nothing) = False Then
            txtDocumentDate.Focus()
            Return False
        End If

        If clsCommon.myLen(txtTankerNo.Value) <= 0 Then
            txtTankerNo.Focus()
            Throw New Exception("Tanker No cannot be left blank")
        End If
        If ucStatusTare.Status = ERPTransactionStatus.Pending Then
            If clsCommon.myCdbl(TxtTareWeight.Value) <= 0 Then
                TxtTareWeight.Focus()
                Throw New Exception("Tare Weight cannot be Zero or blank")
            End If
        ElseIf ucStatusGros.Status = ERPTransactionStatus.Pending Then
            If clsCommon.myCdbl(txtGrossWeight.Value) <= 0 Then
                txtGrossWeight.Focus()
                Throw New Exception("Gross Weight cannot be Zero or blank")
            End If
            txtNetWeight.Value = txtGrossWeight.Value - TxtTareWeight.Value
            If txtNetWeight.Value <= 0 Then
                Throw New Exception("New Weight cannot be Zero or blank")
            End If
        End If
        Return True
    End Function

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As clsMCCWeighment
        Try
            Reset()
            obj = clsMCCWeighment.GetData(strCode, arrLoc, NavTyep)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0 Then
                isNewEntry = False
                txtDocumentNo.Value = obj.Document_No
                txtDocumentDate.Value = obj.Document_Date
                txtTankerNo.Value = obj.Tanker_No
                lblTransporterCode.Text = obj.Transporter_Code
                lblTransporterName.Text = obj.Transporter_name
                lblGateEntryNo.Text = obj.Gate_Entry_No
                lblLocationCode.Text = obj.Location_Code
                LblLocationName.Text = obj.Location_Name
                TxtTareWeight.Value = obj.Tare_Weight
                txtGrossWeight.Value = obj.Gross_Weight
                txtNetWeight.Value = obj.Net_Weight
                ucStatusTare.Status = obj.Status_Tare_Weight
                ucStatusGros.Status = obj.Status_Gross_Weight
                If obj.Status_Gross_Weight = ERPTransactionStatus.Approved Then
                    btnPost.Enabled = False
                    btnsave.Enabled = False
                    btndelete.Enabled = False
                Else
                    btnPost.Enabled = True
                    btnsave.Enabled = True
                    btndelete.Enabled = True
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
        End Try
    End Sub

    Sub LoadDataPendingGrossweight(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        'Reset()
        'Dim obj As clsMCCWeighment = clsMCCWeighment.GetDataforpendingGrossweight(strCode, arrLoc, NavTyep)
        'If obj IsNot Nothing Then
        '    isNewEntry = False
        '    fndWeighmentcode.Value = obj.Weighment_No
        '    txtWeighmentdate.Value = obj.Weighment_Date
        '    lblGateEntryNo.Text = obj.GateEntry_Document_No
        '    FndTankerNo.Value = obj.Tanker_No
        '    lblLocationCode.Text = obj.Location_Code
        '    LblLocationName.Text = clsDBFuncationality.getSingleValue("Select Location_Desc from TSPL_LOCATION_MASTER where Location_Code  ='" + lblLocationCode.Text + "' ")
        '    TxtTareWeight.Value = obj.Tare_Weight
        '    txtGrossWeight.Value = obj.Gross_Weight
        '    txtNetWeight.Value = obj.Net_Weight
        '    lblQCStatus.Visible = True
        '    lblQCStatus.Text = clsDBFuncationality.getSingleValue("Select case when Isnull(TSPL_Quality_Check_BulkSale.Posted,0)  =0 then 'QC Pending' else 'QC Done' end as [QC_Status]  from TSPL_WEIGHMENT_DETAIL_BULKSALE Left Outer Join TSPL_TANKER_MASTER  on TSPL_WEIGHMENT_DETAIL_BULKSALE.Tanker_No  =TSPL_TANKER_MASTER.Tanker_No Left Outer Join TSPL_LOCATION_MASTER on TSPL_WEIGHMENT_DETAIL_BULKSALE.Location_Code =TSPL_LOCATION_MASTER.Location_Code Left Outer Join TSPL_Quality_Check_BulkSale on TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No=TSPL_Quality_Check_BulkSale.Weighment_No where TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No='" & obj.Weighment_No & "'")
        '    fndWeighmentcode.MyReadOnly = True
        '    btnsave.Text = "Update"
        '    If clsCommon.CompairString(obj.Posted, "1") = CompairStringResult.Equal Then
        '        btnPost.Enabled = False
        '        btnsave.Enabled = False
        '        btndelete.Enabled = False
        '        UsLock1.Status = ERPTransactionStatus.Approved
        '    Else
        '        btnPost.Enabled = True
        '        btnsave.Enabled = True
        '        btndelete.Enabled = True
        '        UsLock1.Status = ERPTransactionStatus.Pending
        '    End If
        'End If
        'obj = Nothing
    End Sub

    Private Sub DeleteData()
        Try
            If (deleteConfirm()) Then
                If (clsMCCWeighment.DeleteData(txtDocumentNo.Value, arrLoc)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data deleted successfully ", Me.Text)
                    Reset()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        CloseForm()
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        Reset()
    End Sub

    Private Sub fndWeighmentcode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocumentNo._MYNavigator
        Dim qry As String = String.Empty
        Try
            qry = "select count(*) from TSPL_MCC_WEIGHMENT where Weighment_No='" + txtDocumentNo.Value + "' "
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If check > 0 Then
                txtDocumentNo.MyReadOnly = True
            ElseIf check <= 0 Then
                txtDocumentNo.MyReadOnly = False
            End If
            LoadData(txtDocumentNo.Value, NavType)
             
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            qry = Nothing
        End Try
    End Sub

    Private Sub fndWeighmentcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocumentNo._MYValidating
        Dim qry As String = "select TSPL_MCC_WEIGHMENT.Document_No,TSPL_MCC_WEIGHMENT.Document_Date,TSPL_MCC_WEIGHMENT.Gate_Entry_No,TSPL_MCC_WEIGHMENT.Location_Code,TSPL_LOCATION_MASTER.Location_Desc, " + Environment.NewLine +
        " TSPL_MCC_GATE_ENTRY.Transporter_Code,TSPL_VENDOR_MASTER.Vendor_Name as Transporter_Name," + Environment.NewLine +
        " Tare_Weight, Gross_Weight,Net_Weight,(case when Status_Tare_Weight=1 then 'Approved' else 'Pending' end) as Status_Tare_Weight,(case when Status_Gross_Weight=1 then 'Approved' else 'Pending' end) as Status_Gross_Weight   " + Environment.NewLine +
        " from TSPL_MCC_WEIGHMENT" + Environment.NewLine +
        " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_MCC_WEIGHMENT.Location_Code" + Environment.NewLine +
        " left outer join TSPL_MCC_GATE_ENTRY on TSPL_MCC_GATE_ENTRY.Document_No=TSPL_MCC_WEIGHMENT.Gate_Entry_No " + Environment.NewLine +
        " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_MCC_GATE_ENTRY.Transporter_Code"
        Dim strwhrcls As String = " TSPL_MCC_WEIGHMENT.Location_Code in (" + arrLoc + ")"
        txtDocumentNo.Value = clsCommon.ShowSelectForm("MCCWeDocF", qry, "Document_No", strwhrcls, txtDocumentNo.Value, "", isButtonClicked, "TSPL_MCC_WEIGHMENT.Document_Date")
        LoadData(txtDocumentNo.Value, NavigatorType.Current)
        qry = Nothing
        strwhrcls = Nothing
    End Sub

    Private Sub txtGrossWeight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtGrossWeight.TextChanged
        If clsCommon.myCdbl(txtGrossWeight.Value) > 0 Then
            txtNetWeight.Value = txtGrossWeight.Value - TxtTareWeight.Value
        Else
            txtNetWeight.Value = 0
        End If
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            If ucStatusTare.Status = ERPTransactionStatus.Pending OrElse ucStatusGros.Status = ERPTransactionStatus.Pending Then
                If (myMessages.postConfirm()) Then
                    clsMCCWeighment.PostData(txtDocumentNo.Value, arrLoc)
                    clsCommon.MyMessageBoxShow(Me, "Successfully posted", Me.Text)
                    LoadData(txtDocumentNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FndTankerNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtTankerNo._MYValidating
        Dim qry As String = " select TSPL_MCC_GATE_ENTRY.Tanker_No,TSPL_MCC_GATE_ENTRY.Document_No as [Gate Entry No],TSPL_MCC_GATE_ENTRY.Document_Date as [Gate Entry Date],TSPL_MCC_GATE_ENTRY.Location_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_MCC_GATE_ENTRY.Transporter_Code,TSPL_VENDOR_MASTER.Vendor_Name as [Transporter Name],Remarks  " + Environment.NewLine +
        " from TSPL_MCC_GATE_ENTRY" + Environment.NewLine +
        " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_MCC_GATE_ENTRY.Location_Code" + Environment.NewLine +
        " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_MCC_GATE_ENTRY.Transporter_Code" + Environment.NewLine +
        " where TSPL_MCC_GATE_ENTRY.Status = 1" + Environment.NewLine +
        " and not exists(select 1 from TSPL_MCC_WEIGHMENT where TSPL_MCC_WEIGHMENT.Gate_Entry_No=TSPL_MCC_GATE_ENTRY.Document_No and TSPL_MCC_WEIGHMENT.Document_No not in ('" + txtDocumentNo.Value + "')) "
        Dim dr As DataRow = clsCommon.ShowSelectFormForRow("MCCTGEFinder", qry)
        If dr IsNot Nothing AndAlso dr.ItemArray.Count > 0 Then
            txtTankerNo.Value = clsCommon.myCstr(dr("Tanker_No"))
            lblTransporterCode.Text = clsCommon.myCstr(dr("Transporter_Code"))
            lblTransporterName.Text = clsCommon.myCstr(dr("Transporter Name"))
            lblGateEntryNo.Text = clsCommon.myCstr(dr("Gate Entry No"))
            lblLocationCode.Text = clsCommon.myCstr(dr("Location_Code"))
            LblLocationName.Text = clsCommon.myCstr(dr("Location_Desc"))
        End If
    End Sub

    Private Sub frmMilkReceiptMCC_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Try
            UcWeighing1.CloseCOMPORT()
        Catch ex As Exception
        End Try
    End Sub
End Class
