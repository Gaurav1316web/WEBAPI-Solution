Imports System.Data.SqlClient
Imports common
Imports System.IO

Public Class frmDBTNEFTUploader
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Dim IsinsideLoadData As Boolean = False
    Dim Qry As String
    Dim isFlag As Boolean = False
    Dim arrLoc As String = Nothing
    Dim SettMPIncentiveEntryApplyMonthly As Boolean = False
    Dim SettMPIncentiveEntryCycleWiseButNEFTMonthly As Boolean = False
    Dim SettMPIncentiveEntryIncentiveRate As Decimal = 0
    Dim SettDCSMPIncetiveReco As Boolean = False
    Dim SettApplyZoneOnDBT As Boolean = False
    Dim dtPerforma As DataTable
    Dim settMaxRowExport As Integer = 0
    Dim SettMCCOneDBTOneDoc As String = ""
#End Region
    Public Sub New()
        InitializeComponent()
    End Sub
    Private Sub FrmVLCDataUploaderManual_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Qry = "select TSPL_BANK_MASTER_NEFT_PERFORMA.* from TSPL_BANK_MASTER_NEFT_PERFORMA 
left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_BANK_MASTER_NEFT_PERFORMA.BANK_CODE
where TSPL_BANK_MASTER.NEFT_DBT_Default=1 order by TRCode"
        dtPerforma = clsDBFuncationality.GetDataTable(Qry)
        If dtPerforma Is Nothing OrElse dtPerforma.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please First Create DBT NEFT Performa With Default DBT NEFT Bank", Me.Text)
            Me.Close()
        Else
            Dim dtDefault As DataTable = clsDBTNEFTPerforma.GetDefault()
            For Each dr As DataRow In dtDefault.Rows
                Dim flag As Boolean = False
                For Each drPer As DataRow In dtPerforma.Rows
                    If clsCommon.CompairString(clsCommon.myCstr(dr("Code")), clsCommon.myCstr(drPer("NEFT_Col_Code"))) = CompairStringResult.Equal Then
                        flag = True
                        Exit For
                    End If
                Next
                If Not flag Then
                    clsCommon.MyMessageBoxShow(Me, "Please Set [" + clsCommon.myCstr(dr("Code")) + "] in Default DBT NEFT Performa", Me.Text)
                    Me.Close()
                    Exit Sub
                End If
            Next
        End If

        SplitContainer3.Panel2Collapsed = True
        SettApplyZoneOnDBT = (clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.ApplyZoneInDBT, clsFixedParameterCode.ApplyZoneInDBT, Nothing)) > 0)
        txtZone.MendatroryField = SettApplyZoneOnDBT
        SettMCCOneDBTOneDoc = clsFixedParameter.GetData(clsFixedParameterType.AndroidAPP, clsFixedParameterCode.OneDBTOneDoc, Nothing)
        SettMPIncentiveEntryApplyMonthly = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MPIncentiveEntryApplyMonthly, clsFixedParameterCode.MPIncentiveEntryApplyMonthly, Nothing))
        SettMPIncentiveEntryCycleWiseButNEFTMonthly = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.MPIncentiveEntryCycleWiseButNEFTMonthly, clsFixedParameterCode.MPIncentiveEntryCycleWiseButNEFTMonthly, Nothing))
        SettMPIncentiveEntryIncentiveRate = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MPIncentiveEntryIncentiveRate, clsFixedParameterCode.MPIncentiveEntryIncentiveRate, Nothing))
        SettDCSMPIncetiveReco = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DCSRecoCondition, clsFixedParameterCode.MandatoryDCSMPIncetiveReco, Nothing)) = 1)
        settMaxRowExport = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.MaxRowsExcelDBTNEFTUploader, clsFixedParameterCode.MaxRowsExcelDBTNEFTUploader, Nothing))

        UcAttachment1.Form_ID = "A" + MyBase.Form_ID
        UcAttachment1.isDeleteTheAttachment = False
        UcAttachment1.settAutoAttachment = True
        'RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Collapsed

        UcAttachment2.Form_ID = MyBase.Form_ID
        UcAttachment2.MandatoryPDFFile = True
        SetUserMgmtNew()
        Reset()

        MCCLOCATIONFINDER()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Transaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Transaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N New Transaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        If SettMPIncentiveEntryCycleWiseButNEFTMonthly Then
            RadPageView1.SelectedPage = RadPageViewPage3
        Else
            RadPageView1.Pages("RadPageViewPage3").Item.Visibility = ElementVisibility.Collapsed
            RadPageView1.SelectedPage = RadPageViewPage1
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        Me.Focus()
        txtdate.Focus()
        btnPrint.Visible = (clsCommon.CompairString(objCommonVar.CurrComp_Code1, "GNG") = CompairStringResult.Equal)
        'btnPrint.Visible = True
    End Sub
    Private Sub FrmVLCDataUploaderManual_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclose.Enabled Then
            CloseForm()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnclose.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            Dim frm As New frmPWDHighSecrity(Nothing)
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnReverse.Visible = True
                SplitContainer3.Panel2Collapsed = False
            End If
        End If
    End Sub
    Private Sub CloseForm()
        Me.Close()
        GC.Collect()
    End Sub



    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        btnNEFTUploader.Visible = MyBase.isPrintFlag
        If lblPending.Status = ERPTransactionStatus.Pending Then
            btnNEFTUploader.Visible = False
        Else
            btnNEFTUploader.Visible = True
        End If
        btnPost.Visible = MyBase.isPostFlag
        btnReverse.Enabled = MyBase.isReverse
    End Sub
    Sub Reset()
        'loadBlankGrid()
        gvItem.DataSource = Nothing
        gvInvalid.DataSource = Nothing
        gvFarmer.DataSource = Nothing

        Dim dt As Date = clsCommon.GETSERVERDATE()
        txtFromDate.Value = "01/" & DatePart(DateInterval.Month, dt) & "/" & DatePart(DateInterval.Year, dt)
        If SettMPIncentiveEntryApplyMonthly OrElse SettMPIncentiveEntryCycleWiseButNEFTMonthly Then
            txtToDate.Value = txtFromDate.Value.AddMonths(1).AddDays(-1)
        Else
            txtToDate.Value = "01/" & DatePart(DateInterval.Month, dt) & "/" & DatePart(DateInterval.Year, dt)
        End If
        txtDocumentNo.Value = ""
        txtdate.Value = dt
        txtDocumentNo.MyReadOnly = False
        btnsave.Text = "Save"
        txtMCC.arrValueMember = Nothing
        txtVLC.arrValueMember = Nothing
        btndelete.Enabled = False
        btnsave.Enabled = True
        btnPost.Enabled = False
        txtdate.Focus()
        EnableInputDataField()
        isNewEntry = True
        IsinsideLoadData = False
        lblPending.Status = ERPTransactionStatus.Pending
        txtRemarks.Text = ""
        txtZone.Value = ""
        lblZone.Text = ""
        If clsCommon.myLen(SettMCCOneDBTOneDoc) > 0 Then
            Dim arr As New ArrayList()
            arr.Add(SettMCCOneDBTOneDoc)
            txtMCC.arrValueMember = arr
        End If
        gvItem.MasterTemplate.SummaryRowsBottom.Clear()
        UcAttachment1.BlankAllControls()
        UcAttachment2.BlankAllControls()
        UcAttachment2.isDeleteTheAttachment = True
        BtnBank.Enabled = False
    End Sub

    Private Function AllowToSave() As Boolean
        clsApply_Approval.CheckUpdate_Doc_Valid(MyBase.Form_ID, clsCommon.myCstr(txtDocumentNo.Value))
        Return True
    End Function
    Sub SaveData()
        Try
            If AllowToSave() Then
                Dim obj As New clsDBTNEFT()
                obj.Document_Code = txtDocumentNo.Value
                obj.Document_Date = txtdate.Value
                obj.From_Date = txtFromDate.Value
                obj.To_Date = txtToDate.Value
                obj.Remarks = txtRemarks.Text
                obj.Zone_Code = txtZone.Value
                obj.Bank_Letter_Date = txtBankLetterDate.Value
                If gvItem.Rows.Count > 0 Then
                    obj.arr = New List(Of clsDBTNEFTDetail)
                    For Each grow As GridViewRowInfo In gvItem.Rows
                        Dim objTr As New clsDBTNEFTDetail
                        objTr.SNo = obj.arr.Count + 1
                        objTr.Against_MP_Incentive_TR = clsCommon.myCstr(grow.Cells(clsDBTNEFTPerforma.colAgainstMPIncetive).Value)

                        objTr.VLC_Uploader_Code = clsCommon.myCstr(grow.Cells(clsDBTNEFTPerforma.colSociety).Value)
                        objTr.MP_Uploader_Code = clsCommon.myCstr(grow.Cells(clsDBTNEFTPerforma.colMPUploaderCode).Value)
                        objTr.Amount = clsCommon.myCdbl(grow.Cells(clsDBTNEFTPerforma.colAmount).Value)
                        objTr.MP_IFSC_No = clsCommon.myCstr(grow.Cells(clsDBTNEFTPerforma.colMPIFSCCode).Value)
                        objTr.MP_Account_No = clsCommon.myCstr(grow.Cells(clsDBTNEFTPerforma.colMPAccountNo).Value)
                        objTr.MP_Bank = clsCommon.myCstr(grow.Cells(clsDBTNEFTPerforma.colMPBank).Value)
                        objTr.MP_Mobile_No = clsCommon.myCstr(grow.Cells(clsDBTNEFTPerforma.colMPMobileNo).Value)
                        objTr.MP_Name = clsCommon.myCstr(grow.Cells(clsDBTNEFTPerforma.colMPName).Value)
                        'objTr.Transaction = clsCommon.myCstr(grow.Cells(colTransaction).Value)
                        obj.arr.Add(objTr)
                    Next
                End If
                If gvInvalid.Rows.Count > 0 Then
                    obj.arrInvalid = New List(Of clsDBTNEFTDetailInvalid)
                    For Each grow As GridViewRowInfo In gvInvalid.Rows
                        Dim objTr As New clsDBTNEFTDetailInvalid
                        objTr.SNo = obj.arrInvalid.Count + 1
                        objTr.Against_MP_Incentive_TR = clsCommon.myCstr(grow.Cells(clsDBTNEFTPerforma.colAgainstMPIncetive).Value)
                        objTr.VLC_Uploader_Code = clsCommon.myCstr(grow.Cells(clsDBTNEFTPerforma.colSociety).Value)
                        objTr.MP_Uploader_Code = clsCommon.myCstr(grow.Cells(clsDBTNEFTPerforma.colMPUploaderCode).Value)
                        objTr.Amount = clsCommon.myCdbl(grow.Cells(clsDBTNEFTPerforma.colAmount).Value)
                        objTr.MP_IFSC_No = clsCommon.myCstr(grow.Cells(clsDBTNEFTPerforma.colMPIFSCCode).Value)
                        objTr.MP_Account_No = clsCommon.myCstr(grow.Cells(clsDBTNEFTPerforma.colMPAccountNo).Value)
                        objTr.MP_Bank = clsCommon.myCstr(grow.Cells(clsDBTNEFTPerforma.colMPBank).Value)
                        objTr.MP_Mobile_No = clsCommon.myCstr(grow.Cells(clsDBTNEFTPerforma.colMPMobileNo).Value)
                        objTr.MP_Name = clsCommon.myCstr(grow.Cells(clsDBTNEFTPerforma.colMPName).Value)
                        obj.arrInvalid.Add(objTr)
                    Next
                End If
                Dim objApproval As New clsApply_Approval()
                If (clsDBTNEFT.SaveData(obj, isNewEntry)) Then
                    Dim qry As String = "select sum(Amount) from TSPL_DBT_NEFT_DETAIL where Document_Code='" + txtDocumentNo.Value + "' "
                    Dim TotAmt As Decimal = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue(qry))
                    clsApply_Approval.CheckApprovalRequired(MyBase.Form_ID, obj.Document_Code, txtdate.Text, "", clsCommon.myCstr(txtRemarks.Text), clsCommon.myCdbl(TotAmt), 0, "", objApproval)
                    qry = "select 1 from TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL where TRANS_Code='" + MyBase.Form_ID + "' and Document_Code='" + obj.Document_Code + "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        UcAttachment1.BlankAllControls()
                        Dim objP As New clsDBTNEFT()
                        Dim Filename As String = objP.funPrintBankLetter(obj.Document_Code, True)
                        Dim SafeFileName As String = "BankLetter.pdf"
                        UcAttachment1.AddAttachment(Filename, SafeFileName)
                        Filename = clsCommon.MyExportToExcelGridPath("NEFT Uploader", gvItem, Nothing, Me.Text, False, "", "")
                        SafeFileName = "NEFTDetail.xls"
                        UcAttachment1.AddAttachment(Filename, SafeFileName)

                        UcAttachment1.SaveData(obj.Document_Code, True, Nothing)
                    End If

                    clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
                    LoadData(obj.Document_Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            If clsCommon.myLen(ex.Message) > 200 Then
                clsERPFuncationality.OpenNotepadFile(ex.Message, Me.Text)
            Else
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End If
        End Try
    End Sub
    Private Sub DeleteData()
        Try
            clsApply_Approval.CheckUpdate_Doc_Valid(MyBase.Form_ID, clsCommon.myCstr(txtDocumentNo.Value))
            If (deleteConfirm()) Then
                If (clsDBTNEFT.DeleteData(txtDocumentNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    Reset()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Reset()
        IsinsideLoadData = True
        Dim obj As clsDBTNEFT = clsDBTNEFT.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            DisableInputDataField()
            'BtnBank.Enabled = True
            isNewEntry = False
            txtDocumentNo.Value = obj.Document_Code
            txtdate.Value = obj.Document_Date
            txtFromDate.Value = obj.From_Date
            txtToDate.Value = obj.To_Date
            lblPending.Status = obj.Status
            txtRemarks.Text = obj.Remarks
            txtZone.Value = obj.Zone_Code
            lblZone.Text = ClsZoneMaster.GetName(obj.Zone_Code)
            txtMCC.arrValueMember = obj.arrMCC
            txtVLC.arrValueMember = obj.arrVLC
            txtBankLetterDate.Value = obj.Bank_Letter_Date
            If obj.arr IsNot Nothing AndAlso obj.arr.Count > 0 Then
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(GetQry("TSPL_DBT_NEFT_DETAIL", False))
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    gvItem.DataSource = dt
                    FormatGrid(gvItem)
                End If
                If SettMPIncentiveEntryCycleWiseButNEFTMonthly Then
                    Dim dtFarmer As DataTable = clsDBFuncationality.GetDataTable(GetQry("TSPL_DBT_NEFT_DETAIL", True))
                    If dtFarmer IsNot Nothing AndAlso dtFarmer.Rows.Count > 0 Then
                        gvFarmer.DataSource = dtFarmer
                        FormatGrid(gvFarmer)
                    End If
                End If
            End If

            If obj.arrInvalid IsNot Nothing AndAlso obj.arrInvalid.Count > 0 Then
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(GetQry("TSPL_DBT_NEFT_DETAIL_INVALID", False))
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    gvInvalid.DataSource = dt
                    FormatGrid(gvInvalid)
                End If
            End If

            txtDocumentNo.MyReadOnly = True
            If obj.Status = ERPTransactionStatus.Approved Then
                btnsave.Enabled = False
                btndelete.Enabled = False
                btnPost.Enabled = False
                btnNEFTUploader.Visible = True
                UcAttachment2.isDeleteTheAttachment = False
                BtnBank.Enabled = True
            Else
                btnsave.Text = "Update"
                btnsave.Enabled = True
                btndelete.Enabled = True
                btnPost.Enabled = True
                BtnBank.Enabled = False
                UcAttachment2.isDeleteTheAttachment = True
            End If
            UcAttachment1.LoadData(obj.Document_Code)
            UcAttachment2.LoadData(obj.Document_Code)
        End If
        IsinsideLoadData = False
    End Sub


    Private Sub FormatGrid(ByVal gv As RadGridView)
        Try
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            For ii As Integer = 0 To gv.Columns.Count - 1
                gv.Columns(ii).ReadOnly = True
                gv.Columns(ii).FormatString = ""
                gv.Columns(ii).BestFit()
            Next
            For ii As Integer = 0 To dtPerforma.Rows.Count - 1
                If clsCommon.CompairString(gv.Name, "gvFarmer") = CompairStringResult.Equal Then
                    If clsCommon.CompairString(clsCommon.myCstr(dtPerforma.Rows(ii)("NEFT_Col_Name")), clsDBTNEFTPerforma.colAgainstMPIncetive) = CompairStringResult.Equal Then
                        Continue For
                    End If
                End If
                gv.Columns(clsCommon.myCstr(dtPerforma.Rows(ii)("NEFT_Col_Code"))).HeaderText = clsCommon.myCstr(dtPerforma.Rows(ii)("NEFT_Col_Name"))
                gv.Columns(clsCommon.myCstr(dtPerforma.Rows(ii)("NEFT_Col_Code"))).IsVisible = Not clsCommon.myCBool(dtPerforma.Rows(ii)("NEFT_Col_Hide"))
            Next

            gv.Columns(clsDBTNEFTPerforma.colAmount).FormatString = "{0:n2}"
            gv.Columns(clsDBTNEFTPerforma.colAmount).TextAlignment = System.Drawing.ContentAlignment.MiddleRight

            gv.BestFitColumns()
            gv.AllowAddNewRow = False
            gv.AllowDeleteRow = True
            gv.AllowRowReorder = False
            gv.ShowGroupPanel = False
            gv.EnableFiltering = True
            gv.ShowFilteringRow = True
            gv.EnableSorting = False
            gv.EnableGrouping = False
            gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
            gv.GridBehavior = New MyBehavior()

            'Add Summary row for Amount and S.No'
            Dim summaryRowItem As New GridViewSummaryRowItem()
            gv.Columns("SNo").FormatString = ""
            Dim item1 As New GridViewSummaryItem("SNo", "", GridAggregateFunction.Count)
            summaryRowItem.Add(item1)

            gv.Columns("AMOUNT").FormatString = "{0:n2}"
            Dim item11 As New GridViewSummaryItem("AMOUNT", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item11)

            gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtDocumentNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocumentNo._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_MP_INCENTIVE_ENTRY_HEAD where Document_Code='" + txtDocumentNo.Value + "' "
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If check > 0 Then
                txtDocumentNo.MyReadOnly = True
            ElseIf check <= 0 Then
                txtDocumentNo.MyReadOnly = False
            End If

            LoadData(txtDocumentNo.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtDocumentNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtDocumentNo._MYValidating
        Try
            txtDocumentNo.Value = clsDBTNEFT.getFinder("", txtDocumentNo.Value, isButtonClicked)
            LoadData(txtDocumentNo.Value, NavigatorType.Current)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub MCCLOCATIONFINDER()
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData(True)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                arrLoc = obj.arrLocCodes
            Else
                'fndMCCCode.Enabled = False
                'Throw New Exception("Please Set Default Location Of LogIn User")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub DisableInputDataField()
        txtFromDate.Enabled = False
        txtToDate.Enabled = False
    End Sub
    Sub EnableInputDataField()
        txtFromDate.Enabled = True
        txtToDate.Enabled = True
    End Sub

    'Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
    '    CloseForm()
    'End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        CloseForm()
    End Sub
    Private Sub btndelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Private Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Sub GridFocus(Optional ByVal gvRow As GridViewRowInfo = Nothing)
        If gvItem.Rows.Count > 0 Then
            gvItem.Focus()
            gvItem.CurrentColumn = gvItem.Columns(clsDBTNEFTPerforma.colMPUploaderCode)
            If Not gvRow Is Nothing Then
                gvItem.CurrentRow = gvItem.Rows(gvRow.Index + 1)
            Else
                If gvItem.CurrentRow Is Nothing Then
                    gvItem.CurrentRow = gvItem.Rows(gvItem.Rows.Count - 1)
                End If
            End If


            gvItem.PerformLayout()
            gvItem.BeginEdit()
            gvItem.EndEdit()
        Else
            gvItem.Rows.AddNew()
            gvItem.CurrentColumn = gvItem.Columns(clsDBTNEFTPerforma.colMPUploaderCode)
            gvItem.CurrentRow = gvItem.Rows(gvItem.Rows.Count - 1)
            gvItem.PerformLayout()
            gvItem.BeginEdit()
        End If
    End Sub


    Private Sub txtFromDate_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtFromDate.Validating
        SetToDate()
    End Sub
    Private Sub txtFromDate_Leave(sender As Object, e As EventArgs) Handles txtFromDate.Leave
        SetToDate()
    End Sub
    Sub SetToDate()
        Try
            If Not IsinsideLoadData Then
                Dim PaymentCycleType As String = ""
                Dim PaymentCycleValue As Integer = 0
                If txtMCC.arrValueMember Is Nothing OrElse txtMCC.arrValueMember.Count <= 0 Then
                    Throw New Exception("Please select the MCC first")
                End If
                If SettMPIncentiveEntryApplyMonthly OrElse SettMPIncentiveEntryCycleWiseButNEFTMonthly Then
                    txtFromDate.Value = New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1)
                    txtToDate.Value = txtFromDate.Value.AddMonths(1).AddDays(-1)
                Else
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select Payment_Cycle,PC_TYPE,PC_VALUE from ( select TSPL_MCC_MASTER.Payment_Cycle,TSPL_PAYMENT_CYCLE_MASTER.PC_TYPE,TSPL_PAYMENT_CYCLE_MASTER.PC_VALUE  from TSPL_MCC_MASTER left outer join TSPL_PAYMENT_CYCLE_MASTER on TSPL_PAYMENT_CYCLE_MASTER.PC_CODE=TSPL_MCC_MASTER.Payment_Cycle   where TSPL_MCC_MASTER.MCC_Code  in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") ) xx group by Payment_Cycle,PC_TYPE,PC_VALUE")
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        Throw New Exception("No Payment Cycle found on current MCC/Location")
                    End If
                    If dt.Rows.Count > 1 Then
                        Throw New Exception("Selected MCC's Payment Cycle Should be Same")
                    End If
                    PaymentCycleType = clsCommon.myCstr(dt.Rows(0)("PC_TYPE"))
                    PaymentCycleValue = clsCommon.myCdbl(dt.Rows(0)("PC_VALUE"))
                    Dim dtCurr As DateTime = clsCommon.GETSERVERDATE()
                    If clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal Then
                        If txtFromDate.Value.Day Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                            clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month or at interval of " & PaymentCycleValue & " Day, Because MCC has payment Cycle of " & PaymentCycleValue & " Day ", Me.Text)
                            txtFromDate.Value = New Date(dtCurr.Year, dtCurr.Month, 1)
                            txtToDate.Value = txtFromDate.Value
                            Exit Sub
                        End If
                        txtToDate.Value = txtFromDate.Value.AddDays(PaymentCycleValue - 1)

                        If txtFromDate.Value.Month <> txtToDate.Value.Month Then
                            txtToDate.Value = New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                        End If
                        Dim dtNxtPay As DateTime = txtToDate.Value.AddDays(Math.Ceiling(PaymentCycleValue / 2.0))
                        If txtFromDate.Value.Month <> dtNxtPay.Month Then
                            txtToDate.Value = New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                        End If
                    ElseIf clsCommon.CompairString(PaymentCycleType, "Month") = CompairStringResult.Equal Then
                        If clsCommon.myCdbl(clsCommon.GetPrintDate(txtFromDate.Value, "dd")) <> 1 Then
                            clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month, Because MCC has payment Cycle of Month Type", Me.Text)
                            txtFromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                            txtToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                            Exit Sub
                        End If
                        txtToDate.Value = DateAdd(DateInterval.Month, PaymentCycleValue, txtFromDate.Value)
                    ElseIf clsCommon.CompairString(PaymentCycleType, "Year") = CompairStringResult.Equal Then
                        If clsCommon.myCdbl(clsCommon.GetPrintDate(txtFromDate.Value, "dd")) <> 1 Then
                            clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month, Because MCC has payment Cycle of Year Type", Me.Text)
                            txtFromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                            txtToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                            Exit Sub
                        End If
                        txtToDate.Value = DateAdd(DateInterval.Year, PaymentCycleValue, txtFromDate.Value)
                    ElseIf clsCommon.CompairString(PaymentCycleType, "Week") = CompairStringResult.Equal Then
                        Dim today As Date = txtFromDate.Value
                        Dim dayDiff As Integer = today.DayOfWeek - IIf(PaymentCycleValue = 1, DayOfWeek.Sunday, IIf(PaymentCycleValue = 2, DayOfWeek.Monday, IIf(PaymentCycleValue = 3, DayOfWeek.Tuesday, IIf(PaymentCycleValue = 4, DayOfWeek.Wednesday, IIf(PaymentCycleValue = 5, DayOfWeek.Thursday, IIf(PaymentCycleValue = 6, DayOfWeek.Friday, DayOfWeek.Saturday))))))
                        txtFromDate.Value = today.AddDays(-dayDiff)
                        txtToDate.Value = txtFromDate.Value.AddDays(6)
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Private Sub mtxtVLC__My_Click(sender As Object, e As EventArgs) Handles txtVLC._My_Click
        If txtMCC.arrValueMember Is Nothing OrElse txtMCC.arrValueMember.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Select MCC First.", Me.Text)
            txtMCC.Focus()
            Exit Sub
        End If
        If SettApplyZoneOnDBT Then
            If clsCommon.myLen(txtZone.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select " + txtZone.MyLinkLable2.Text, Me.Text)
                txtZone.Focus()
                Exit Sub
            End If
        End If
        Dim qry As String = "Select VLC.VLC_Code_vlc_Uploader as [Code],VLC.VLC_Code as [VLC Code],VLC.VLC_Name as [VLC Name],VLC.MCC as [MCC Code],VLC.Route_Code as [Route Code],RM.Route_Name ,TSPL_VENDOR_MASTER.Zone_Code as ZoneCode, TSPL_ZONE_MASTER.Description as ZoneName
from TSPL_VLC_MASTER_HEAD    VLC left join TSPL_MCC_ROUTE_MASTER RM on vlc.Route_Code=RM.Route_Code 
left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=VLC.VSP_Code
left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code=TSPL_VENDOR_MASTER.Zone_Code
where 2=2 "
        If clsCommon.myLen(SettMCCOneDBTOneDoc) <= 0 Then
            qry += " and VLC.MCC in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")"
        End If
        If SettApplyZoneOnDBT Then
            qry += "and TSPL_VENDOR_MASTER.Zone_Code='" + txtZone.Value + "' "
        End If
        txtVLC.arrValueMember = clsCommon.ShowMultipleSelectForm("dbtneftv", qry, "VLC Code", "VLC Name", txtVLC.arrValueMember, txtVLC.arrDispalyMember)

        fillMPS()
    End Sub
    Sub fillMPS()
        Try
            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                'loadBlankGrid()
                gvItem.DataSource = Nothing
                Dim isDataFound As Boolean = False
                Dim dtValid As DataTable = clsDBFuncationality.GetDataTable(GetMpQry(True, False))
                If dtValid IsNot Nothing AndAlso dtValid.Rows.Count > 0 Then
                    isDataFound = True
                    gvItem.DataSource = dtValid
                    FormatGrid(gvItem)
                End If
                If SettMPIncentiveEntryCycleWiseButNEFTMonthly Then
                    Dim dtFarmerWiseValid As DataTable = clsDBFuncationality.GetDataTable(GetMpQry(True, True))
                    If dtFarmerWiseValid IsNot Nothing AndAlso dtFarmerWiseValid.Rows.Count > 0 Then
                        isDataFound = True
                        gvFarmer.DataSource = dtFarmerWiseValid
                        FormatGrid(gvFarmer)
                    End If
                End If

                Dim dtInValid As DataTable = clsDBFuncationality.GetDataTable(GetMpQry(False, False))
                If dtInValid IsNot Nothing AndAlso dtInValid.Rows.Count > 0 Then
                    isDataFound = True
                    gvInvalid.DataSource = dtInValid
                    FormatGrid(gvInvalid)
                End If

                If Not isDataFound Then
                    clsCommon.MyMessageBoxShow(Me, "No Data found", Me.Text)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Function GetQry(ByVal TableName As String, ByVal GrpByFarmer As Boolean) As String
        Dim qry As String = "Select " + TableName + ".SNo as [" + clsDBTNEFTPerforma.colSlNo + "]," + TableName + ".Against_MP_Incentive_TR AS [" + clsDBTNEFTPerforma.colAgainstMPIncetive + "]," + TableName + ".VLC_Uploader_Code AS [" + clsDBTNEFTPerforma.colSociety + "]
                ," + TableName + ".MP_Uploader_Code AS [" + clsDBTNEFTPerforma.colMPUploaderCode + "]"

        If clsCommon.CompairString(objCommonVar.CurrDatabase, "BKN") = CompairStringResult.Equal Then
            qry += " ,CASE  WHEN (TSPL_DBT_NEFT_DETAIL.Amount) % 1 >= 0.5    THEN CEILING(TSPL_DBT_NEFT_DETAIL.Amount)  Else FLOOR(TSPL_DBT_NEFT_DETAIL.Amount) End As Amount "
        Else
            qry += " ," + TableName + ".Amount AS [" + clsDBTNEFTPerforma.colAmount + "]"
        End If

        qry += "," + TableName + ".MP_IFSC_No AS [" + clsDBTNEFTPerforma.colMPIFSCCode + "]
," + TableName + ".MP_Bank AS [" + clsDBTNEFTPerforma.colMPBank + "]," + TableName + ".MP_Mobile_No AS [" + clsDBTNEFTPerforma.colMPMobileNo + "]
," + TableName + ".MP_Account_No AS [" + clsDBTNEFTPerforma.colMPAccountNo + "]," + TableName + ".MP_Name AS [" + clsDBTNEFTPerforma.colMPName + "]
,TSPL_VLC_MASTER_HEAD.VLC_Name as [" + clsDBTNEFTPerforma.colSocietyName + "],TSPL_ZONE_MASTER.Description as [" + clsDBTNEFTPerforma.colZoneName + "],TSPL_MP_INCENTIVE_ENTRY_DETAIL.MP_Code as [" + clsDBTNEFTPerforma.colFarmerCode + "]
from " + TableName + " 
Left Outer Join TSPL_MP_INCENTIVE_ENTRY_DETAIL On TSPL_MP_INCENTIVE_ENTRY_DETAIL.PK_Id=" + TableName + ".Against_MP_Incentive_TR   
left outer join TSPL_MP_INCENTIVE_ENTRY_HEAD on TSPL_MP_INCENTIVE_ENTRY_HEAD.Document_Code=TSPL_MP_INCENTIVE_ENTRY_DETAIL.Document_Code
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MP_INCENTIVE_ENTRY_DETAIL.VLC_Code
left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.vendor_code=TSPL_VLC_MASTER_HEAD.VSP_Code
left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code=TSPL_VENDOR_MASTER.Zone_Code
where " + TableName + ".Document_Code='" & txtDocumentNo.Value & "'"
        If GrpByFarmer Then
            qry = "select ROW_NUMBER() OVER (ORDER BY [" + clsDBTNEFTPerforma.colFarmerCode + "]) AS [" + clsDBTNEFTPerforma.colSlNo + "],[" + clsDBTNEFTPerforma.colFarmerCode + "],max([" + clsDBTNEFTPerforma.colSociety + "]) as [" + clsDBTNEFTPerforma.colSociety + "],max([" + clsDBTNEFTPerforma.colMPUploaderCode + "]) as [" + clsDBTNEFTPerforma.colMPUploaderCode + "],sum([" + clsDBTNEFTPerforma.colAmount + "]) as [" + clsDBTNEFTPerforma.colAmount + "],max([" + clsDBTNEFTPerforma.colMPIFSCCode + "]) as [" + clsDBTNEFTPerforma.colMPIFSCCode + "],max([" + clsDBTNEFTPerforma.colMPAccountNo + "]) as [" + clsDBTNEFTPerforma.colMPAccountNo + "],max([" + clsDBTNEFTPerforma.colMPBank + "]) as [" + clsDBTNEFTPerforma.colMPBank + "],max([" + clsDBTNEFTPerforma.colMPMobileNo + "]) as [" + clsDBTNEFTPerforma.colMPMobileNo + "],max([" + clsDBTNEFTPerforma.colMPName + "]) as [" + clsDBTNEFTPerforma.colMPName + "],max([" + clsDBTNEFTPerforma.colSocietyName + "]) as [" + clsDBTNEFTPerforma.colSocietyName + "],max([" + clsDBTNEFTPerforma.colZoneName + "]) as [" + clsDBTNEFTPerforma.colZoneName + "] from (" + qry + ")xx group by  [" + clsDBTNEFTPerforma.colFarmerCode + "]"
        End If
        Dim strMain As String = "Select "
        For ii As Integer = 0 To dtPerforma.Rows.Count - 1
            If GrpByFarmer Then
                If clsCommon.CompairString(clsCommon.myCstr(dtPerforma.Rows(ii)("NEFT_Col_Code")), clsDBTNEFTPerforma.colAgainstMPIncetive) = CompairStringResult.Equal Then
                    Continue For
                End If
            End If
            If clsCommon.myCstr(dtPerforma.Rows(ii)("NEFT_Col_Code")).Contains("CR") Then
                strMain += "'" + clsCommon.myCstr(dtPerforma.Rows(ii)("NEFT_Col_Value")) + "' as [" + clsCommon.myCstr(dtPerforma.Rows(ii)("NEFT_Col_Code")) + "]"
                If ii <> dtPerforma.Rows.Count - 1 Then
                    strMain += ","
                End If
            Else
                strMain += "[" + clsCommon.myCstr(dtPerforma.Rows(ii)("NEFT_Col_Code")) + "] "
                If ii <> dtPerforma.Rows.Count - 1 Then
                    strMain += ","
                End If
            End If
        Next
        strMain += " from (" + qry + ")xxx order by [" + clsDBTNEFTPerforma.colSlNo + "]"

        Return strMain
    End Function

    Private Function GetMpQry(ByVal IsPickValid As Boolean, ByVal GrpByFarmer As Boolean) As String

        Dim BaseQry As String = "select TSPL_MP_INCENTIVE_ENTRY_DETAIL.PK_Id,TSPL_MP_INCENTIVE_ENTRY_HEAD.Document_Code as Doc_No,convert(varchar, TSPL_MP_INCENTIVE_ENTRY_HEAD.From_Date,103) +' To '+ convert(varchar,TSPL_MP_INCENTIVE_ENTRY_HEAD.To_Date,103) as Date_Range,TSPL_MP_INCENTIVE_ENTRY_HEAD.MCC_Code,tspl_MCC_Master.MCC_Name,TSPL_MP_INCENTIVE_ENTRY_DETAIL.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_MP_MASTER.MP_Code,TSPL_MP_MASTER.MP_Code_VLC_Uploader as VLC_CODE_Uploader,TSPL_MP_MASTER.PayeeName as Payee_Joint_Name,TSPL_MP_MASTER.BankName as Bank_Code,TSPL_MP_MASTER.BankName as Bank_Code_Desc,TSPL_MP_MASTER.Telphone,TSPL_MP_MASTER.AccountNO as Payee_Joint_Account_No,TSPL_MP_MASTER.IFCICode as Payee_Joint_IFSC_Code,TSPL_MP_INCENTIVE_ENTRY_DETAIL.Qty,TSPL_MP_INCENTIVE_ENTRY_DETAIL.Amount_Actual as Payable_Amount 
,TSPL_ZONE_MASTER.Description  as ZoneName   
from TSPL_MP_INCENTIVE_ENTRY_DETAIL "
        If SettDCSMPIncetiveReco Then
            BaseQry += " left outer join TSPL_DCS_MP_INCENTIVE_RECO_DETAIL on TSPL_DCS_MP_INCENTIVE_RECO_DETAIL.Cycle_Year=TSPL_MP_INCENTIVE_ENTRY_DETAIL.Cycle_Year and TSPL_DCS_MP_INCENTIVE_RECO_DETAIL.Cycle_Month=TSPL_MP_INCENTIVE_ENTRY_DETAIL.Cycle_Month and TSPL_DCS_MP_INCENTIVE_RECO_DETAIL.Cycle_No=TSPL_MP_INCENTIVE_ENTRY_DETAIL.Cycle_No and TSPL_DCS_MP_INCENTIVE_RECO_DETAIL.VLC_Code=TSPL_MP_INCENTIVE_ENTRY_DETAIL.VLC_Code 
left outer join TSPL_DCS_MP_INCENTIVE_RECO_HEAD on TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Document_Code =TSPL_DCS_MP_INCENTIVE_RECO_DETAIL.Document_Code"
        End If

        BaseQry += " left outer join TSPL_MP_INCENTIVE_ENTRY_HEAD on TSPL_MP_INCENTIVE_ENTRY_HEAD.Document_Code=TSPL_MP_INCENTIVE_ENTRY_DETAIL.Document_Code
    left outer join TSPL_MP_MASTER on TSPL_MP_MASTER.MP_Code=TSPL_MP_INCENTIVE_ENTRY_DETAIL.MP_Code
    left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MP_INCENTIVE_ENTRY_DETAIL.VLC_Code
    left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.vendor_code=TSPL_VLC_MASTER_HEAD.VSP_Code
    left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code=TSPL_VENDOR_MASTER.Zone_Code
    left outer join tspl_MCC_Master on tspl_MCC_Master.MCC_Code=TSPL_MP_INCENTIVE_ENTRY_HEAD.MCC_Code
    where 2=2 "
        If SettDCSMPIncetiveReco Then
            BaseQry += " and TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Status=1 and TSPL_DCS_MP_INCENTIVE_RECO_DETAIL.PK_Id is not null "
        End If
        If IsPickValid Then
            BaseQry += " and (len(isnull(TSPL_MP_MASTER.AccountNO,''))>0 and len(isnull(TSPL_MP_MASTER.IFCICode,''))=11 )"
        Else
            BaseQry += " and (len(isnull(TSPL_MP_MASTER.AccountNO,''))=0 or len(isnull(TSPL_MP_MASTER.IFCICode,''))<>11 )"
        End If
        BaseQry += " and TSPL_MP_INCENTIVE_ENTRY_HEAD.MCC_Code in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") and  TSPL_MP_INCENTIVE_ENTRY_HEAD.From_Date >='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and TSPL_MP_INCENTIVE_ENTRY_DETAIL.VLC_Code in (" + clsCommon.GetMulcallString(txtVLC.arrValueMember) + ") and  TSPL_MP_INCENTIVE_ENTRY_HEAD.To_Date <='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' 
    and not exists(select 1 from TSPL_DBT_NEFT_DETAIL where TSPL_DBT_NEFT_DETAIL.Document_Code not in ('" + txtDocumentNo.Value + "') and TSPL_DBT_NEFT_DETAIL.Against_MP_Incentive_TR=TSPL_MP_INCENTIVE_ENTRY_DETAIL.PK_Id
    and not exists(select 1 from TSPL_DBT_NEFT_REJECT_DETAIL left outer join TSPL_DBT_NEFT_REJECT on TSPL_DBT_NEFT_REJECT.Document_Code=TSPL_DBT_NEFT_REJECT_DETAIL.Document_Code where TSPL_DBT_NEFT_REJECT_DETAIL.Against_DBT_NEFT_TR=TSPL_DBT_NEFT_DETAIL.PK_Id and TSPL_DBT_NEFT_REJECT.Status=1))"


        Qry = "select   ROW_NUMBER() OVER (ORDER BY Bank_Code,MCC_Code,VLC_Code_VLC_Uploader) AS [" + clsDBTNEFTPerforma.colSlNo + "],MP_Code as [" + clsDBTNEFTPerforma.colFarmerCode + "],PK_Id as [" + clsDBTNEFTPerforma.colAgainstMPIncetive + "],VLC_Code_VLC_Uploader as [" + clsDBTNEFTPerforma.colSociety + "],VLC_CODE_Uploader as [" + clsDBTNEFTPerforma.colMPUploaderCode + "],Payable_Amount as [" + clsDBTNEFTPerforma.colAmount + "],Payee_Joint_IFSC_Code as [" + clsDBTNEFTPerforma.colMPIFSCCode + "],Payee_Joint_Account_No as [" + clsDBTNEFTPerforma.colMPAccountNo + "],Bank_Code as [" + clsDBTNEFTPerforma.colMPBank + "],Telphone as [" + clsDBTNEFTPerforma.colMPMobileNo + "],Payee_Joint_Name as [" + clsDBTNEFTPerforma.colMPName + "],Bank_Code,MCC_Code,VLC_Name as [" + clsDBTNEFTPerforma.colSocietyName + "],ZoneName as [" + clsDBTNEFTPerforma.colZoneName + "] from (" + BaseQry + ")xxx "

        If GrpByFarmer Then
            Qry = "select ROW_NUMBER() OVER (ORDER BY max(Bank_Code),max(MCC_Code),max([" + clsDBTNEFTPerforma.colMPUploaderCode + "])) AS [" + clsDBTNEFTPerforma.colSlNo + "],[" + clsDBTNEFTPerforma.colFarmerCode + "],max([" + clsDBTNEFTPerforma.colSociety + "]) as [" + clsDBTNEFTPerforma.colSociety + "],max([" + clsDBTNEFTPerforma.colMPUploaderCode + "]) as [" + clsDBTNEFTPerforma.colMPUploaderCode + "],sum([" + clsDBTNEFTPerforma.colAmount + "]) as [" + clsDBTNEFTPerforma.colAmount + "],max([" + clsDBTNEFTPerforma.colMPIFSCCode + "]) as [" + clsDBTNEFTPerforma.colMPIFSCCode + "],max([" + clsDBTNEFTPerforma.colMPAccountNo + "]) as [" + clsDBTNEFTPerforma.colMPAccountNo + "],max([" + clsDBTNEFTPerforma.colMPBank + "]) as [" + clsDBTNEFTPerforma.colMPBank + "],max([" + clsDBTNEFTPerforma.colMPMobileNo + "]) as [" + clsDBTNEFTPerforma.colMPMobileNo + "],max([" + clsDBTNEFTPerforma.colMPName + "]) as [" + clsDBTNEFTPerforma.colMPName + "],max(Bank_Code) as Bank_Code,max(MCC_Code) as MCC_Code,max([" + clsDBTNEFTPerforma.colSocietyName + "]) as [" + clsDBTNEFTPerforma.colSocietyName + "],max([" + clsDBTNEFTPerforma.colZoneName + "]) as [" + clsDBTNEFTPerforma.colZoneName + "] from  (select   MP_Code as [" + clsDBTNEFTPerforma.colFarmerCode + "],VLC_Code_VLC_Uploader as [" + clsDBTNEFTPerforma.colSociety + "],VLC_CODE_Uploader as [" + clsDBTNEFTPerforma.colMPUploaderCode + "],Payable_Amount as [" + clsDBTNEFTPerforma.colAmount + "],Payee_Joint_IFSC_Code as [" + clsDBTNEFTPerforma.colMPIFSCCode + "],Payee_Joint_Account_No as [" + clsDBTNEFTPerforma.colMPAccountNo + "],BankName as [" + clsDBTNEFTPerforma.colMPBank + "],Telphone as [" + clsDBTNEFTPerforma.colMPMobileNo + "],Payee_Joint_Name as [" + clsDBTNEFTPerforma.colMPName + "],Bank_Code,MCC_Code,VLC_Name as [" + clsDBTNEFTPerforma.colSocietyName + "],ZoneName as [" + clsDBTNEFTPerforma.colZoneName + "] from (" + BaseQry + ")xx )xxx  group by  [" + clsDBTNEFTPerforma.colFarmerCode + "]"
        End If
        Dim strMain As String = "Select "
        For ii As Integer = 0 To dtPerforma.Rows.Count - 1
            If GrpByFarmer Then
                If clsCommon.CompairString(clsCommon.myCstr(dtPerforma.Rows(ii)("NEFT_Col_Code")), clsDBTNEFTPerforma.colAgainstMPIncetive) = CompairStringResult.Equal Then
                    Continue For
                End If
            End If
            If clsCommon.myCstr(dtPerforma.Rows(ii)("NEFT_Col_Code")).Contains("CR") Then
                strMain += "'" + clsCommon.myCstr(dtPerforma.Rows(ii)("NEFT_Col_Value")) + "' as [" + clsCommon.myCstr(dtPerforma.Rows(ii)("NEFT_Col_Code")) + "]"
                If ii <> dtPerforma.Rows.Count - 1 Then
                    strMain += ","
                End If
            Else
                strMain += "[" + clsCommon.myCstr(dtPerforma.Rows(ii)("NEFT_Col_Code")) + "] "
                If ii <> dtPerforma.Rows.Count - 1 Then
                    strMain += ","
                End If
            End If
        Next
        strMain += " from (" + Qry + ")xxx " + Environment.NewLine
        strMain += "order by Bank_Code,MCC_Code,[" + clsDBTNEFTPerforma.colSociety + "],[" + clsDBTNEFTPerforma.colMPUploaderCode + "]"
        Return strMain
    End Function

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Private Sub PostData()
        Try
            Dim qry As String = ""
            Dim msg As String = ""
            Dim dt As DataTable = Nothing

            clsApply_Approval.CheckUpdate_Doc_Valid(MyBase.Form_ID, clsCommon.myCstr(txtDocumentNo.Value))
            If (myMessages.postConfirm()) Then
                UcAttachment2.AllowToSave()
                UcAttachment2.SaveData(txtDocumentNo.Value)

                Dim ExcellPath As String = ExportGridPath()
                clsDBTNEFT.PostData(txtDocumentNo.Value, ExcellPath)
                clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                LoadData(txtDocumentNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If clsCommon.myLen(txtDocumentNo.Value) <= 0 Then
                txtDocumentNo.Focus()
                Throw New Exception("Please select Document No")
            End If
            Dim strHeading As String = clsCommon.myCstr("NEFT Uploader")
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(" ")
            If exporter = EnumExportTo.Excel Then
                If SettMPIncentiveEntryCycleWiseButNEFTMonthly Then
                    transportSql.exportdata(settMaxRowExport, gvFarmer, "", "", 0, 0)
                Else
                    transportSql.exportdata(settMaxRowExport, gvItem, "", "", 0, 0)
                End If
            Else
                PageSetupReport_ID = clsCommon.myCstr(MyBase.Form_ID)
                If SettMPIncentiveEntryCycleWiseButNEFTMonthly Then
                    clsCommon.MyExportToPDF(strHeading, gvFarmer, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                Else
                    clsCommon.MyExportToPDF(strHeading, gvItem, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Function ExportGridPath() As String
        Dim excelPath As String = ""
        Try
            Dim dtContent As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.DBTNEFTUploader + "'")
            If dtContent IsNot Nothing AndAlso dtContent.Rows.Count > 0 Then
                Dim qry As String = "select Email from TSPL_BANK_MASTER where NEFT_DBT_Default = 1 "
                Dim bankEmail As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                If clsCommon.myLen(bankEmail) > 0 Then
                    Dim strHeading As String = clsCommon.myCstr("NEFT Uploader")
                    Dim arrHeader As List(Of String) = New List(Of String)()
                    arrHeader.Add(" ")
                    excelPath = clsCommon.MyExportToExcelGridPath(strHeading, gvItem, Nothing, Me.Text, False, "", "")
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return excelPath
    End Function


    Private Sub txtMCC__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
        Dim qry As String = "select MCC_Code as [MCC Code],MCC_NAME as [MCC Name],TSPL_MCC_MASTER.plant_code as [Plant Code],tspl_location_master.location_desc as [Plant Name] from TSPL_MCC_MASTER left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.plant_code where  2=2 "
        If clsCommon.myLen(SettMCCOneDBTOneDoc) > 0 Then
            qry += " and MCC_Code='" + SettMCCOneDBTOneDoc + "'"
        End If
        txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("MBPSMCCr3", qry, "MCC Code", "MCC Name", txtMCC.arrValueMember, txtMCC.arrDispalyMember)
        txtVLC.arrValueMember = Nothing
        'loadBlankGrid()
        gvItem.DataSource = Nothing
        SetToDate()

    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        ExportGrid(EnumExportTo.PDF)
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Try
            If clsCommon.myLen(txtDocumentNo.Value) <= 0 Then
                txtDocumentNo.Focus()
                Throw New Exception("Please select Document No")
            End If
            Dim strHeading As String = clsCommon.myCstr("Invalid Data")
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(" ")
            clsCommon.MyExportToExcelGrid(strHeading, gvInvalid, Nothing, Me.Text)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub txtZone__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtZone._MYValidating
        Dim whr As String = "2=3"
        If SettApplyZoneOnDBT Then
            whr = "2=2"
        End If
        txtZone.Value = ClsZoneMaster.getFinder(whr, txtZone.Value, isButtonClicked)
        lblZone.Text = ClsZoneMaster.GetName(txtZone.Value)
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If clsCommon.myLen(txtDocumentNo.Value) <= 0 Then
            myMessages.blankValue("Doument not found to Print")
        Else
            funPrint(txtDocumentNo.Value)
        End If
    End Sub
    Public Sub funPrint(ByVal strDocNo As String)
        Try
            Try
                If clsCommon.myLen(txtDocumentNo.Value) <= 0 Then
                    txtDocumentNo.Focus()
                    Throw New Exception("Please select Document No")
                End If
                Dim qry As String = "Select TSPL_COMPANY_MASTER.Comp_Name,TSPL_DBT_NEFT_DETAIL.MP_Bank as Bank_Code,TSPL_DBT_NEFT_DETAIL.MP_Bank as Bank_Code_Desc,TSPL_DBT_NEFT_DETAIL.MP_Mobile_No,CONVERT(varchar, TSPL_DBT_NEFT.From_Date,103)+' - '+CONVERT(varchar, TSPL_DBT_NEFT.To_Date,103)  as Date_Range,TSPL_MP_INCENTIVE_ENTRY_HEAD.MCC_Code as MCC_Code,TSPL_MCC_MASTER.MCC_NAME,
ROW_NUMBER() OVER(PArtition by TSPL_MP_INCENTIVE_ENTRY_DETAIL.MP_Bank ORDER BY (TSPL_MP_INCENTIVE_ENTRY_DETAIL.MP_Bank )) As SNo
,TSPL_DBT_NEFT_DETAIL.MP_IFSC_No as Payee_Joint_IFSC_Code ,TSPL_DBT_NEFT_DETAIL.MP_Account_No as Payee_Joint_Account_No,TSPL_DBT_NEFT_DETAIL.MP_Name as Payee_Joint_Name,TSPL_DBT_NEFT_DETAIL.Amount as Payable_Amount
from TSPL_DBT_NEFT_DETAIL 
left outer join TSPL_DBT_NEFT on TSPL_DBT_NEFT.Document_Code=TSPL_DBT_NEFT_DETAIL.Document_Code
Left Outer Join TSPL_MP_INCENTIVE_ENTRY_DETAIL On TSPL_MP_INCENTIVE_ENTRY_DETAIL.PK_Id=TSPL_DBT_NEFT_DETAIL.Against_MP_Incentive_TR   
left outer join TSPL_MP_INCENTIVE_ENTRY_HEAD on TSPL_MP_INCENTIVE_ENTRY_HEAD.Document_Code=TSPL_MP_INCENTIVE_ENTRY_DETAIL.Document_Code
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MP_INCENTIVE_ENTRY_DETAIL.VLC_Code
left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.vendor_code=TSPL_VLC_MASTER_HEAD.VSP_Code
left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MP_INCENTIVE_ENTRY_HEAD.MCC_Code
left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code='" + objCommonVar.CurrentCompanyCode + "'
where TSPL_DBT_NEFT_DETAIL.Document_Code='" + txtDocumentNo.Value + "' order by TSPL_MP_INCENTIVE_ENTRY_DETAIL.MP_Bank "

                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "crptDBTNEFTUploader", "Bank Wise NEFT Uploader")
                frmCRV = Nothing
            Catch ex As Exception
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnPrintBankLetter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintBankLetter.Click
        Dim objP As New clsDBTNEFT()
        objP.funPrintBankLetter(txtDocumentNo.Value, False)
    End Sub

    Private Sub btnReverse_Click(sender As Object, e As EventArgs) Handles btnReverse.Click
        Try
            If clsCommon.myLen(txtDocumentNo.Value) > 0 Then
                If clsCommon.MyMessageBoxShow(Me, "Unpost the current transaction" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    clsDBTNEFT.ReverseAndUnpost(txtDocumentNo.Value)
                    clsCommon.MyMessageBoxShow(Me, "Tansaction unposted succesffuly", Me.Text)
                    LoadData(txtDocumentNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub BtnBank_Click(sender As Object, e As EventArgs) Handles BtnBank.Click
        Try
            Dim obj As New clsDBTNEFT()
            Dim RCDF_Status As Decimal
            obj.Bank_Letter_Date = txtBankLetterDate.Value
            obj.Document_Code = txtDocumentNo.Value

            RCDF_Status = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue(" select isnull(RCDF_Status,0) FROM TSPL_DBT_NEFT WHERE Document_Code='" + txtDocumentNo.Value + "'"))
            If RCDF_Status = 1 Then
                clsCommon.MyMessageBoxShow(Me, "DBT NEFT updated by RCDF.", Me.Text)
            Else

                If obj.Bank_Letter_Date > clsCommon.GETSERVERDATE Then
                    txtBankLetterDate.Focus()

                    Throw New Exception("Bank Letter Date should be less than Server Date")
                End If

                If (clsDBTNEFT.SaveBankLetter(obj)) Then
                    clsCommon.MyMessageBoxShow(Me, "Bank Letter Date Save Successfully.", Me.Text)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

End Class

