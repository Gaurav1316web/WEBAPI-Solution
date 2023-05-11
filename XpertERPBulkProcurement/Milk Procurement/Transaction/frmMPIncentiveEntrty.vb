Imports System.Data.SqlClient
Imports common
Imports System.IO

Public Class frmMPIncentiveEntrty
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
    Dim SettMPIncentiveEntryIncentiveRate As Decimal = 0

    Dim SettApplyPashuAaharAndMineralMixture As Boolean = False
    Dim ApplyZoneWiseVSP As Boolean = False
    Dim SettMCCOneDBTOneDoc As String = ""
    Dim sett
#End Region
    Public Sub New()
        InitializeComponent()
    End Sub
    Private Sub FrmVLCDataUploaderManual_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SettMCCOneDBTOneDoc = clsFixedParameter.GetData(clsFixedParameterType.AndroidAPP, clsFixedParameterCode.OneDBTOneDoc, Nothing)
        SettMPIncentiveEntryApplyMonthly = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MPIncentiveEntryApplyMonthly, clsFixedParameterCode.MPIncentiveEntryApplyMonthly, Nothing)) > 0)
        SettMPIncentiveEntryIncentiveRate = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MPIncentiveEntryIncentiveRate, clsFixedParameterCode.MPIncentiveEntryIncentiveRate, Nothing))
        SettApplyPashuAaharAndMineralMixture = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyPashuAaharAndMineralMixture, clsFixedParameterCode.ApplyPashuAaharAndMineralMixture, Nothing)) > 0)
        ApplyZoneWiseVSP = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyZoneWiseVSP, clsFixedParameterCode.ApplyZoneWiseVSP, Nothing)) > 0)

        txtPashuAahar.Visible = SettApplyPashuAaharAndMineralMixture
        lblPashuAahar.Visible = SettApplyPashuAaharAndMineralMixture
        txtMineralMixture.Visible = SettApplyPashuAaharAndMineralMixture
        lblMineralMixture.Visible = SettApplyPashuAaharAndMineralMixture
        txtSailejRate.Visible = SettApplyPashuAaharAndMineralMixture
        lblSailejRate.Visible = SettApplyPashuAaharAndMineralMixture
        txtRahatKampekatFeedRate.Visible = SettApplyPashuAaharAndMineralMixture
        lblRahatKampekatFeedRate.Visible = SettApplyPashuAaharAndMineralMixture

        MyLabel11.Visible = Not SettApplyPashuAaharAndMineralMixture
        txtIncentiveRate.Visible = Not SettApplyPashuAaharAndMineralMixture

        SetUserMgmtNew()
        Reset()

        MCCLOCATIONFINDER()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Transaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Transaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N New Transaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")

        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        Me.Focus()
        txtdate.Focus()
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
            'PostData()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine +
                      "========Table Name=========" + Environment.NewLine +
                      "TSPL_MP_INCENTIVE_ENTRY_HEAD" + Environment.NewLine +
                      "TSPL_MP_INCENTIVE_ENTRY_DETAIL" + Environment.NewLine)
        End If
    End Sub
    Private Sub CloseForm()
        Me.Close()
        GC.Collect()
    End Sub
    Sub loadBlankGrid()
        Try
            gvItem.DataSource = Nothing
            gvItem.Rows.Clear()
            gvItem.Columns.Clear()
        Catch ex As Exception
        End Try

        Exit Sub

        'gvItem.Rows.Clear()
        'gvItem.Columns.Clear()
        'gvItem.DataSource = Nothing

        'gvItem.MasterTemplate.SummaryRowsBottom.Clear()

        'Dim lineNo As New GridViewTextBoxColumn()
        'lineNo.FormatString = ""
        'lineNo.HeaderText = "PKID."
        'lineNo.Name = clsMPIncetiveEntryColumns.colPKID
        'lineNo.IsVisible = False
        'lineNo.ReadOnly = True
        'lineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gvItem.Columns.Add(lineNo)

        'lineNo = New GridViewTextBoxColumn()
        'lineNo.FormatString = ""
        'lineNo.HeaderText = "SNo."
        'lineNo.Name = clsMPIncetiveEntryColumns.colSlNo
        'lineNo.Width = 60
        'lineNo.ReadOnly = True
        'lineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gvItem.Columns.Add(lineNo)

        'Dim farmercode As New GridViewTextBoxColumn()
        'farmercode.FormatString = ""
        'farmercode.HeaderText = "VLC"
        'farmercode.Name = clsMPIncetiveEntryColumns.colVLCUploaderCode
        'farmercode.ReadOnly = True
        'farmercode.IsVisible = True
        'farmercode.Width = 100
        'farmercode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        'gvItem.Columns.Add(farmercode)

        'farmercode = New GridViewTextBoxColumn()
        'farmercode.FormatString = ""
        'farmercode.HeaderText = "VLC Code"
        'farmercode.Name = clsMPIncetiveEntryColumns.colVLCCode
        'farmercode.ReadOnly = True
        'farmercode.IsVisible = False
        'farmercode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        'gvItem.Columns.Add(farmercode)

        'farmercode = New GridViewTextBoxColumn()
        'farmercode.FormatString = ""
        'farmercode.HeaderText = "VLC Name"
        'farmercode.Name = clsMPIncetiveEntryColumns.colVLCName
        'farmercode.ReadOnly = True
        'farmercode.IsVisible = False
        'farmercode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        'gvItem.Columns.Add(farmercode)


        'Dim farmercode_VlcUploader As New GridViewTextBoxColumn()
        'farmercode_VlcUploader.FormatString = ""
        'farmercode_VlcUploader.HeaderText = "Farmer"
        'farmercode_VlcUploader.Name = clsMPIncetiveEntryColumns.colMPUploaderCode
        'farmercode_VlcUploader.Width = 100
        'farmercode_VlcUploader.ReadOnly = True
        'farmercode_VlcUploader.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        'gvItem.Columns.Add(farmercode_VlcUploader)

        'farmercode = New GridViewTextBoxColumn()
        'farmercode.FormatString = ""
        'farmercode.HeaderText = "Farmer Code"
        'farmercode.Name = clsMPIncetiveEntryColumns.colMPCode
        'farmercode.ReadOnly = True
        'farmercode.IsVisible = True
        'farmercode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        'gvItem.Columns.Add(farmercode)



        'Dim farmername As New GridViewTextBoxColumn()
        'farmername.FormatString = ""
        'farmername.HeaderText = "Farmer Name"
        'farmername.Name = clsMPIncetiveEntryColumns.colMPName
        'farmername.Width = 200
        'farmername.ReadOnly = True
        'farmername.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        'gvItem.Columns.Add(farmername)

        'farmername = New GridViewTextBoxColumn()
        'farmername.FormatString = ""
        'farmername.HeaderText = "Bank"
        'farmername.Name = clsMPIncetiveEntryColumns.colMPBank
        'farmername.Width = 100
        'farmername.ReadOnly = True
        'farmername.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        'gvItem.Columns.Add(farmername)


        'farmername = New GridViewTextBoxColumn()
        'farmername.FormatString = ""
        'farmername.HeaderText = "Account No"
        'farmername.Name = clsMPIncetiveEntryColumns.colMPAccountNo
        'farmername.Width = 150
        'farmername.ReadOnly = True
        'farmername.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        'gvItem.Columns.Add(farmername)

        'farmername = New GridViewTextBoxColumn()
        'farmername.FormatString = ""
        'farmername.HeaderText = "IFSC"
        'farmername.Name = clsMPIncetiveEntryColumns.colMPIFSCCode
        'farmername.Width = 150
        'farmername.ReadOnly = True
        'farmername.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        'gvItem.Columns.Add(farmername)

        'farmername = New GridViewTextBoxColumn()
        'farmername.FormatString = ""
        'farmername.HeaderText = "Phone No"
        'farmername.Name = clsMPIncetiveEntryColumns.colMPPhoneNo
        'farmername.Width = 150
        'farmername.ReadOnly = True
        'farmername.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        'gvItem.Columns.Add(farmername)

        'farmername = New GridViewTextBoxColumn()
        'farmername.FormatString = ""
        'farmername.HeaderText = "Aadhar No"
        'farmername.Name = clsMPIncetiveEntryColumns.colMPAadharNo
        'farmername.Width = 150
        'farmername.ReadOnly = True
        'farmername.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        'gvItem.Columns.Add(farmername)


        'Dim Qty As New GridViewDecimalColumn
        'Qty.FormatString = ""
        'Qty.HeaderText = "Qty"
        'Qty.Name = clsMPIncetiveEntryColumns.colQty
        'Qty.Width = 120
        'Qty.ReadOnly = False
        'Qty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gvItem.Columns.Add(Qty)

        'Dim UOM As New GridViewTextBoxColumn()
        'UOM.FormatString = ""
        'UOM.HeaderText = "UOM"
        'UOM.Name = clsMPIncetiveEntryColumns.colUOM
        'UOM.Width = 80
        'UOM.ReadOnly = True
        'UOM.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        'gvItem.Columns.Add(UOM)

        'Qty = New GridViewDecimalColumn
        'Qty.FormatString = ""
        'Qty.HeaderText = "FAT"
        'Qty.Name = clsMPIncetiveEntryColumns.colFAT
        'Qty.Width = 120
        'Qty.ReadOnly = False
        'Qty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gvItem.Columns.Add(Qty)

        'Qty = New GridViewDecimalColumn
        'Qty.FormatString = ""
        'Qty.HeaderText = "SNF"
        'Qty.Name = clsMPIncetiveEntryColumns.colSNF
        'Qty.Width = 120
        'Qty.ReadOnly = False
        'Qty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gvItem.Columns.Add(Qty)



        'Dim Amount As New GridViewDecimalColumn
        'Amount.FormatString = ""
        'Amount.HeaderText = "Amount"
        'Amount.Name = clsMPIncetiveEntryColumns.colAmount
        'Amount.Width = 100
        'Amount.FormatString = "{0:n2}"
        'Amount.ReadOnly = Not SettApplyPashuAaharAndMineralMixture
        'Amount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gvItem.Columns.Add(Amount)

        'Dim AmountActual As New GridViewDecimalColumn
        'AmountActual.FormatString = ""
        'AmountActual.HeaderText = "Amount Actual"
        'AmountActual.Name = clsMPIncetiveEntryColumns.colAmountActual
        'AmountActual.Width = 100
        'AmountActual.FormatString = "{0:n2}"
        'AmountActual.ReadOnly = True
        'AmountActual.IsVisible = False
        'AmountActual.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gvItem.Columns.Add(AmountActual)


        'Amount = New GridViewDecimalColumn
        'Amount.FormatString = ""
        'Amount.HeaderText = "Pashu Aahar Qty"
        'Amount.Name = clsMPIncetiveEntryColumns.colPashuAaharQty
        'Amount.Width = 100
        'Amount.FormatString = "{0:n2}"
        'Amount.ReadOnly = Not SettApplyPashuAaharAndMineralMixture
        'Amount.IsVisible = SettApplyPashuAaharAndMineralMixture
        'Amount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gvItem.Columns.Add(Amount)

        'Amount = New GridViewDecimalColumn
        'Amount.FormatString = ""
        'Amount.HeaderText = "Pashu Aahar Amount"
        'Amount.Name = clsMPIncetiveEntryColumns.colPashuAaharAmt
        'Amount.Width = 100
        'Amount.FormatString = "{0:n2}"
        'Amount.ReadOnly = True
        'Amount.IsVisible = SettApplyPashuAaharAndMineralMixture
        'Amount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gvItem.Columns.Add(Amount)

        'Amount = New GridViewDecimalColumn
        'Amount.FormatString = ""
        'Amount.HeaderText = "Mineral Mixture Qty"
        'Amount.Name = clsMPIncetiveEntryColumns.colMineralMixtureQty
        'Amount.Width = 100
        'Amount.FormatString = "{0:n2}"
        'Amount.ReadOnly = Not SettApplyPashuAaharAndMineralMixture
        'Amount.IsVisible = SettApplyPashuAaharAndMineralMixture
        'Amount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gvItem.Columns.Add(Amount)

        'Amount = New GridViewDecimalColumn
        'Amount.FormatString = ""
        'Amount.HeaderText = "Mineral Mixture Amount"
        'Amount.Name = clsMPIncetiveEntryColumns.colMineralMixtureAmt
        'Amount.Width = 100
        'Amount.FormatString = "{0:n2}"
        'Amount.ReadOnly = True
        'Amount.IsVisible = SettApplyPashuAaharAndMineralMixture
        'Amount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gvItem.Columns.Add(Amount)

        'Amount = New GridViewDecimalColumn
        'Amount.FormatString = ""
        'Amount.HeaderText = "Sailej Qty"
        'Amount.Name = clsMPIncetiveEntryColumns.colSailejQty
        'Amount.Width = 100
        'Amount.FormatString = "{0:n2}"
        'Amount.ReadOnly = Not SettApplyPashuAaharAndMineralMixture
        'Amount.IsVisible = SettApplyPashuAaharAndMineralMixture
        'Amount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gvItem.Columns.Add(Amount)

        'Amount = New GridViewDecimalColumn
        'Amount.FormatString = ""
        'Amount.HeaderText = "Sailej Amount"
        'Amount.Name = clsMPIncetiveEntryColumns.colSailejAmt
        'Amount.Width = 100
        'Amount.FormatString = "{0:n2}"
        'Amount.ReadOnly = True
        'Amount.IsVisible = SettApplyPashuAaharAndMineralMixture
        'Amount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gvItem.Columns.Add(Amount)


        'Amount = New GridViewDecimalColumn
        'Amount.FormatString = ""
        'Amount.HeaderText = "Rahat Kampekat Feed Qty"
        'Amount.Name = clsMPIncetiveEntryColumns.colRahatKampekatFeedQty
        'Amount.Width = 100
        'Amount.FormatString = "{0:n2}"
        'Amount.ReadOnly = Not SettApplyPashuAaharAndMineralMixture
        'Amount.IsVisible = SettApplyPashuAaharAndMineralMixture
        'Amount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gvItem.Columns.Add(Amount)

        'Amount = New GridViewDecimalColumn
        'Amount.FormatString = ""
        'Amount.HeaderText = "Rahat Kampekat Feed Amount"
        'Amount.Name = clsMPIncetiveEntryColumns.colRahatKampekatFeedAmt
        'Amount.Width = 100
        'Amount.FormatString = "{0:n2}"
        'Amount.ReadOnly = True
        'Amount.IsVisible = SettApplyPashuAaharAndMineralMixture
        'Amount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gvItem.Columns.Add(Amount)




        'Amount = New GridViewDecimalColumn
        'Amount.FormatString = ""
        'Amount.HeaderText = "Total Amount"
        'Amount.Name = clsMPIncetiveEntryColumns.colTotAmount
        'Amount.Width = 100
        'Amount.FormatString = "{0:n2}"
        'Amount.ReadOnly = True
        'Amount.IsVisible = True
        'Amount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gvItem.Columns.Add(Amount)


        'gvItem.AllowAddNewRow = False
        'gvItem.AllowDeleteRow = True
        'gvItem.AllowRowReorder = False
        'gvItem.ShowGroupPanel = False
        'gvItem.EnableFiltering = True
        'gvItem.ShowFilteringRow = True
        'gvItem.EnableSorting = False
        'gvItem.EnableGrouping = False
        'gvItem.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        'gvItem.GridBehavior = New MyBehavior()


        'Dim summaryRowItem As New GridViewSummaryRowItem()
        'summaryRowItem.Add(New GridViewSummaryItem(clsMPIncetiveEntryColumns.colQty, "{0:n2}", GridAggregateFunction.Sum))
        'summaryRowItem.Add(New GridViewSummaryItem(clsMPIncetiveEntryColumns.colAmount, "{0:n2}", GridAggregateFunction.Sum))
        'summaryRowItem.Add(New GridViewSummaryItem(clsMPIncetiveEntryColumns.colAmountActual, "{0:n2}", GridAggregateFunction.Sum))
        'summaryRowItem.Add(New GridViewSummaryItem(clsMPIncetiveEntryColumns.colPashuAaharAmt, "{0:n2}", GridAggregateFunction.Sum))
        'summaryRowItem.Add(New GridViewSummaryItem(clsMPIncetiveEntryColumns.colMineralMixtureQty, "{0:n2}", GridAggregateFunction.Sum))
        'summaryRowItem.Add(New GridViewSummaryItem(clsMPIncetiveEntryColumns.colMineralMixtureAmt, "{0:n2}", GridAggregateFunction.Sum))
        'summaryRowItem.Add(New GridViewSummaryItem(clsMPIncetiveEntryColumns.colSailejQty, "{0:n2}", GridAggregateFunction.Sum))
        'summaryRowItem.Add(New GridViewSummaryItem(clsMPIncetiveEntryColumns.colSailejAmt, "{0:n2}", GridAggregateFunction.Sum))
        'summaryRowItem.Add(New GridViewSummaryItem(clsMPIncetiveEntryColumns.colRahatKampekatFeedQty, "{0:n2}", GridAggregateFunction.Sum))
        'summaryRowItem.Add(New GridViewSummaryItem(clsMPIncetiveEntryColumns.colRahatKampekatFeedAmt, "{0:n2}", GridAggregateFunction.Sum))
        'summaryRowItem.Add(New GridViewSummaryItem(clsMPIncetiveEntryColumns.colTotAmount, "{0:n2}", GridAggregateFunction.Sum))
        'gvItem.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FrmVLCDataUploaderManual)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        btnPost.Visible = MyBase.isPostFlag
    End Sub
    Sub Reset()
        loadBlankGrid()
        Dim dt As Date = clsCommon.GETSERVERDATE()
        txtFromDate.Value = "01/" & DatePart(DateInterval.Month, dt) & "/" & DatePart(DateInterval.Year, dt)
        If SettMPIncentiveEntryApplyMonthly Then
            txtToDate.Value = txtFromDate.Value.AddMonths(1).AddDays(-1)
        Else
            txtToDate.Value = "01/" & DatePart(DateInterval.Month, dt) & "/" & DatePart(DateInterval.Year, dt)
        End If
        txtDocumentNo.Value = ""
        txtMCC.Value = ""
        lblMCCDesc.Text = ""
        txtdate.Value = dt
        txtDocumentNo.MyReadOnly = False
        btnsave.Text = "Save"
        mtxtVLC.arrValueMember = Nothing
        txtIncentiveRate.Value = 0
        btndelete.Enabled = False
        btnsave.Enabled = True
        btnPost.Enabled = False
        txtdate.Focus()
        txtMCC.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        If clsCommon.myLen(SettMCCOneDBTOneDoc) > 0 Then
            txtMCC.Value = SettMCCOneDBTOneDoc
        End If
        If clsCommon.myLen(txtMCC.Value) > 0 Then
            lblMCCDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MCC_NAME from tspl_mcc_master where mcc_Code='" & txtMCC.Value & "' "))
            If clsCommon.myLen(lblMCCDesc.Text) <= 0 Then
                txtMCC.Value = ""
            End If
        End If
        EnableInputDataField()
        isNewEntry = True
        IsinsideLoadData = False
        lblPending.Status = ERPTransactionStatus.Pending
        rbtnFATSNFPer.IsChecked = True
        rbtnFATSNFKG.IsChecked = False
        txtIncentiveRate.Value = SettMPIncentiveEntryIncentiveRate
        mtxtVLC.Enabled = True
        txtMCC.Enabled = True
        btnAddMissing.Enabled = False
    End Sub

    Private Function AllowToSave() As Boolean
        If AllowFutureDateTransaction(txtdate.Value, Nothing) = False Then
            txtdate.Focus()
            Return False
        End If
        If clsCommon.myLen(txtMCC.Value) <= 0 Then
            txtMCC.Focus()
            Throw New Exception("Please select MCC")
        End If
        If clsCommon.GetDateWithStartTime(txtFromDate.Value) = clsCommon.GetDateWithStartTime(txtToDate.Value) Then
            txtFromDate.Focus()
            Throw New Exception("Please select From Date")
        End If
        If SettApplyPashuAaharAndMineralMixture Then
            If clsCommon.myLen(txtPashuAahar.Value) <= 0 Then
                txtIncentiveRate.Focus()
                Throw New Exception("Please enter incentive Rate")
            End If
            If clsCommon.myLen(txtMineralMixture.Value) <= 0 Then
                txtIncentiveRate.Focus()
                Throw New Exception("Please enter Mineral Mixture")
            End If
            If clsCommon.myLen(txtSailejRate.Value) <= 0 Then
                txtIncentiveRate.Focus()
                Throw New Exception("Please enter Sailej Rate")
            End If
            If clsCommon.myLen(txtRahatKampekatFeedRate.Value) <= 0 Then
                txtIncentiveRate.Focus()
                Throw New Exception("Please enter Rahat Kampekat Feed Rate")
            End If
        Else
            If clsCommon.myLen(txtIncentiveRate.Value) <= 0 Then
                txtIncentiveRate.Focus()
                Throw New Exception("Please enter incentive Rate")
            End If
        End If

        If mtxtVLC.arrValueMember Is Nothing AndAlso mtxtVLC.arrValueMember.Count <= 0 Then
            mtxtVLC.Focus()
            Throw New Exception("Please select at least one VLC")
        End If
        UpdateAllRow()
        Return True
    End Function
    Sub SaveData()
        Try
            If AllowToSave() Then
                Dim obj As New clsMPIncentiveEntry()
                obj.Document_Code = txtDocumentNo.Value
                obj.Document_Date = txtdate.Value
                obj.MCC_Code = txtMCC.Value
                obj.From_Date = txtFromDate.Value
                obj.To_Date = txtToDate.Value
                obj.Incetive_Rate = txtIncentiveRate.Value
                If rbtnFATSNFNA.IsChecked Then
                    obj.FATSNFPer = 2
                Else
                    obj.FATSNFPer = rbtnFATSNFPer.IsChecked
                End If
                Dim objTr As New clsMPIncentiveEntryDetail
                obj.arr = New List(Of clsMPIncentiveEntryDetail)
                For Each grow As GridViewRowInfo In gvItem.Rows
                    If clsCommon.myCdbl(grow.Cells(clsMPIncetiveEntryColumns.colQty).Value) > 0 Then
                        objTr = New clsMPIncentiveEntryDetail()
                        objTr.PK_Id = clsCommon.myCdbl(grow.Cells(clsMPIncetiveEntryColumns.colPKID).Value)
                        objTr.SNo = obj.arr.Count + 1
                        objTr.VLC_Code = clsCommon.myCstr(grow.Cells(clsMPIncetiveEntryColumns.colVLCCode).Value)
                        objTr.MP_Code = clsCommon.myCstr(grow.Cells(clsMPIncetiveEntryColumns.colMPCode).Value)
                        objTr.MP_Account_No = clsCommon.myCstr(grow.Cells(clsMPIncetiveEntryColumns.colMPAccountNo).Value)
                        objTr.MP_Bank = clsCommon.myCstr(grow.Cells(clsMPIncetiveEntryColumns.colMPBank).Value)
                        objTr.Qty = clsCommon.myCdbl(grow.Cells(clsMPIncetiveEntryColumns.colQty).Value)
                        objTr.UOM = clsCommon.myCstr(grow.Cells(clsMPIncetiveEntryColumns.colUOM).Value)
                        objTr.Amount = clsCommon.myCdbl(grow.Cells(clsMPIncetiveEntryColumns.colAmount).Value)
                        objTr.Amount_Actual = clsCommon.myCdbl(grow.Cells(clsMPIncetiveEntryColumns.colAmountActual).Value)

                        objTr.MP_Phone_No = clsCommon.myCstr(grow.Cells(clsMPIncetiveEntryColumns.colMPPhoneNo).Value)
                        objTr.MP_Aadhar_No = clsCommon.myCstr(grow.Cells(clsMPIncetiveEntryColumns.colMPAadharNo).Value)
                        objTr.MP_IFSC_No = clsCommon.myCstr(grow.Cells(clsMPIncetiveEntryColumns.colMPIFSCCode).Value)

                        objTr.FAT = clsCommon.myCdbl(grow.Cells(clsMPIncetiveEntryColumns.colFAT).Value)
                        objTr.SNF = clsCommon.myCdbl(grow.Cells(clsMPIncetiveEntryColumns.colSNF).Value)

                        objTr.Pashu_Aahar_Qty = clsCommon.myCdbl(grow.Cells(clsMPIncetiveEntryColumns.colPashuAaharQty).Value)
                        objTr.Pashu_Aahar_Amount = clsCommon.myCdbl(grow.Cells(clsMPIncetiveEntryColumns.colPashuAaharAmt).Value)
                        objTr.Mineral_Mixture_Qty = clsCommon.myCdbl(grow.Cells(clsMPIncetiveEntryColumns.colMineralMixtureQty).Value)
                        objTr.Mineral_Mixture_Amount = clsCommon.myCdbl(grow.Cells(clsMPIncetiveEntryColumns.colMineralMixtureAmt).Value)
                        objTr.Sailej_Qty = clsCommon.myCdbl(grow.Cells(clsMPIncetiveEntryColumns.colSailejQty).Value)
                        objTr.Sailej_Amount = clsCommon.myCdbl(grow.Cells(clsMPIncetiveEntryColumns.colSailejAmt).Value)
                        objTr.Rahat_Kampekat_Feed_Qty = clsCommon.myCdbl(grow.Cells(clsMPIncetiveEntryColumns.colRahatKampekatFeedQty).Value)
                        objTr.Rahat_Kampekat_Feed_Amount = clsCommon.myCdbl(grow.Cells(clsMPIncetiveEntryColumns.colRahatKampekatFeedAmt).Value)
                        objTr.Total_Amount = clsCommon.myCdbl(grow.Cells(clsMPIncetiveEntryColumns.colTotAmount).Value)
                        obj.arr.Add(objTr)
                    End If
                Next
                If obj.arr.Count <= 0 Then
                    Throw New Exception("Please Enter Qty of At lease on MP")
                End If
                If (clsMPIncentiveEntry.SaveData(obj, isNewEntry)) Then
                    clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
                    LoadData(obj.Document_Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            If clsCommon.myLen(ex.Message) > 200 Then
                clsERPFuncationality.OpenNotepadFile(ex.Message, Me.Text)
            Else
                clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
            End If
        End Try
    End Sub
    Private Sub DeleteData()
        Try
            clsLockMPPaymentCycle.LockMPTransaction(txtMCC.Value, txtdate.Value)
            If (deleteConfirm()) Then
                If (clsMPIncentiveEntry.DeleteData(txtDocumentNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ")
                    Reset()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Reset()
        IsinsideLoadData = True
        Dim obj As clsMPIncentiveEntry = clsMPIncentiveEntry.GetData(strCode, NavTyep, Nothing, False)
        If obj IsNot Nothing Then

            isNewEntry = False
            txtDocumentNo.Value = obj.Document_Code
            txtdate.Value = obj.Document_Date
            txtMCC.Value = obj.MCC_Code
            lblMCCDesc.Text = obj.MCC_Name
            txtFromDate.Value = obj.From_Date
            txtToDate.Value = obj.To_Date
            txtIncentiveRate.Value = obj.Incetive_Rate
            lblPending.Status = obj.Status
            If obj.FATSNFPer = 2 Then
                rbtnFATSNFNA.IsChecked = True
            ElseIf obj.FATSNFPer = 1 Then
                rbtnFATSNFPer.IsChecked = True
            Else
                rbtnFATSNFKG.IsChecked = True
            End If

            gvItem.DataSource = Nothing
            Dim Mcc_Uom As String = clsDBFuncationality.getSingleValue("select Uom_Code from TSPL_Mcc_UOM_DETAIL where Stocking_Unit='Y' and  MCC_CODE='" & txtMCC.Value & "'")

            gvItem.DataSource = clsDBFuncationality.GetDataTable(clsMPIncentiveEntryDetail.getQry(obj.Document_Code, Mcc_Uom, Nothing))
            FormatGrid()
            mtxtVLC.Enabled = False
            txtMCC.Enabled = False
            Dim arrVLCUploader As New ArrayList
            Dim arrVLCCode As New ArrayList
            For ii As Integer = 0 To gvItem.Rows.Count - 1
                If Not arrVLCCode.Contains(clsCommon.myCstr(gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colVLCCode).Value)) Then
                    arrVLCCode.Add(clsCommon.myCstr(gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colVLCCode).Value))
                    arrVLCUploader.Add(clsCommon.myCstr(gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colVLCUploaderCode).Value))
                End If
            Next
            mtxtVLC.arrValueMember = arrVLCUploader
            mtxtVLC.Tag = arrVLCCode

            'If obj.arr IsNot Nothing AndAlso obj.arr.Count > 0 Then
            '    For Each objTr As clsMPIncentiveEntryDetail In obj.arr
            '        gvItem.Rows.AddNew()
            '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colPKID).Value = objTr.PK_Id
            '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colSlNo).Value = gvItem.Rows.Count

            '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colVLCCode).Value = objTr.VLC_Code
            '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colVLCUploaderCode).Value = objTr.VLC_Uploader_Code
            '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colVLCName).Value = objTr.VLC_Name


            '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colMPCode).Value = objTr.MP_Code
            '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colMPUploaderCode).Value = objTr.MP_Uploader_Code
            '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colMPName).Value = objTr.MP_Name

            '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colMPBank).Value = objTr.MP_Bank
            '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colMPAccountNo).Value = objTr.MP_Account_No


            '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colQty).Value = objTr.Qty
            '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colUOM).Value = objTr.UOM
            '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colAmount).Value = objTr.Amount
            '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colAmountActual).Value = objTr.Amount_Actual


            '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colMPPhoneNo).Value = objTr.MP_Phone_No
            '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colMPAadharNo).Value = objTr.MP_Aadhar_No
            '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colMPIFSCCode).Value = objTr.MP_IFSC_No

            '        If obj.FATSNFPer Then
            '            gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colFAT).Value = objTr.FAT
            '            gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colSNF).Value = objTr.SNF
            '        Else
            '            gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colFAT).Value = objTr.FAT_Kg
            '            gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colSNF).Value = objTr.SNF_Kg
            '        End If


            '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colPashuAaharQty).Value = objTr.Pashu_Aahar_Qty
            '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colPashuAaharAmt).Value = objTr.Pashu_Aahar_Amount
            '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colMineralMixtureQty).Value = objTr.Mineral_Mixture_Qty
            '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colMineralMixtureAmt).Value = objTr.Mineral_Mixture_Amount
            '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colSailejQty).Value = objTr.Sailej_Qty
            '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colSailejAmt).Value = objTr.Sailej_Amount
            '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colRahatKampekatFeedQty).Value = objTr.Rahat_Kampekat_Feed_Qty
            '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colRahatKampekatFeedAmt).Value = objTr.Rahat_Kampekat_Feed_Amount
            '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colTotAmount).Value = objTr.Total_Amount

            '        If Not arrVLCCode.Contains(objTr.VLC_Code) Then
            '            arrVLCCode.Add(objTr.VLC_Code)
            '            arrVLCUploader.Add(objTr.VLC_Uploader_Code)
            '        End If
            '    Next
            '    mtxtVLC.arrValueMember = arrVLCUploader
            '    mtxtVLC.Tag = arrVLCCode
            'End If
            txtDocumentNo.MyReadOnly = True
            If obj.Status = ERPTransactionStatus.Approved Then
                btnsave.Enabled = False
                btndelete.Enabled = False
                btnPost.Enabled = False
            Else
                btnsave.Text = "Update"
                btnsave.Enabled = True
                btndelete.Enabled = True
                btnPost.Enabled = True
                btnAddMissing.Enabled = True
            End If
            DisableInputDataField()
            mtxtVLC.Focus()
        End If
        IsinsideLoadData = False
    End Sub

    Private Sub FormatGrid()
        Try

            gvItem.Columns(clsMPIncetiveEntryColumns.colPKID).ReadOnly = True
            gvItem.Columns(clsMPIncetiveEntryColumns.colPKID).IsVisible = False
            gvItem.Columns(clsMPIncetiveEntryColumns.colPKID).TextAlignment = System.Drawing.ContentAlignment.MiddleRight


            gvItem.Columns(clsMPIncetiveEntryColumns.colSlNo).FormatString = ""
            gvItem.Columns(clsMPIncetiveEntryColumns.colSlNo).Width = 60
            gvItem.Columns(clsMPIncetiveEntryColumns.colSlNo).ReadOnly = True
            gvItem.Columns(clsMPIncetiveEntryColumns.colSlNo).IsVisible = False
            gvItem.Columns(clsMPIncetiveEntryColumns.colSlNo).TextAlignment = System.Drawing.ContentAlignment.MiddleRight


            gvItem.Columns(clsMPIncetiveEntryColumns.colVLCUploaderCode).FormatString = ""
            gvItem.Columns(clsMPIncetiveEntryColumns.colVLCUploaderCode).ReadOnly = True
            gvItem.Columns(clsMPIncetiveEntryColumns.colVLCUploaderCode).IsVisible = True
            gvItem.Columns(clsMPIncetiveEntryColumns.colVLCUploaderCode).Width = 100
            gvItem.Columns(clsMPIncetiveEntryColumns.colVLCUploaderCode).TextAlignment = System.Drawing.ContentAlignment.MiddleLeft


            gvItem.Columns(clsMPIncetiveEntryColumns.colVLCCode).FormatString = ""
            gvItem.Columns(clsMPIncetiveEntryColumns.colVLCCode).ReadOnly = True
            gvItem.Columns(clsMPIncetiveEntryColumns.colVLCCode).IsVisible = False
            gvItem.Columns(clsMPIncetiveEntryColumns.colVLCCode).TextAlignment = System.Drawing.ContentAlignment.MiddleLeft



            gvItem.Columns(clsMPIncetiveEntryColumns.colVLCName).FormatString = ""
            gvItem.Columns(clsMPIncetiveEntryColumns.colVLCName).ReadOnly = True
            gvItem.Columns(clsMPIncetiveEntryColumns.colVLCName).IsVisible = False
            gvItem.Columns(clsMPIncetiveEntryColumns.colVLCName).TextAlignment = System.Drawing.ContentAlignment.MiddleLeft


            gvItem.Columns(clsMPIncetiveEntryColumns.colMPUploaderCode).FormatString = ""
            gvItem.Columns(clsMPIncetiveEntryColumns.colMPUploaderCode).Width = 100
            gvItem.Columns(clsMPIncetiveEntryColumns.colMPUploaderCode).ReadOnly = True
            gvItem.Columns(clsMPIncetiveEntryColumns.colMPUploaderCode).TextAlignment = System.Drawing.ContentAlignment.MiddleLeft

            gvItem.Columns(clsMPIncetiveEntryColumns.colMPCode).FormatString = ""
            gvItem.Columns(clsMPIncetiveEntryColumns.colMPCode).ReadOnly = True
            gvItem.Columns(clsMPIncetiveEntryColumns.colMPCode).IsVisible = True
            gvItem.Columns(clsMPIncetiveEntryColumns.colMPCode).TextAlignment = System.Drawing.ContentAlignment.MiddleLeft




            gvItem.Columns(clsMPIncetiveEntryColumns.colMPName).FormatString = ""
            gvItem.Columns(clsMPIncetiveEntryColumns.colMPName).Width = 200
            gvItem.Columns(clsMPIncetiveEntryColumns.colMPName).ReadOnly = True
            gvItem.Columns(clsMPIncetiveEntryColumns.colMPName).TextAlignment = System.Drawing.ContentAlignment.MiddleLeft


            gvItem.Columns(clsMPIncetiveEntryColumns.colMPBank).FormatString = ""
            gvItem.Columns(clsMPIncetiveEntryColumns.colMPBank).Width = 100
            gvItem.Columns(clsMPIncetiveEntryColumns.colMPBank).ReadOnly = True
            gvItem.Columns(clsMPIncetiveEntryColumns.colMPBank).TextAlignment = System.Drawing.ContentAlignment.MiddleLeft



            gvItem.Columns(clsMPIncetiveEntryColumns.colMPAccountNo).FormatString = ""
            gvItem.Columns(clsMPIncetiveEntryColumns.colMPAccountNo).Name = clsMPIncetiveEntryColumns.colMPAccountNo
            gvItem.Columns(clsMPIncetiveEntryColumns.colMPAccountNo).Width = 150
            gvItem.Columns(clsMPIncetiveEntryColumns.colMPAccountNo).ReadOnly = True
            gvItem.Columns(clsMPIncetiveEntryColumns.colMPAccountNo).TextAlignment = System.Drawing.ContentAlignment.MiddleLeft


            gvItem.Columns(clsMPIncetiveEntryColumns.colMPIFSCCode).FormatString = ""
            gvItem.Columns(clsMPIncetiveEntryColumns.colMPIFSCCode).Width = 150
            gvItem.Columns(clsMPIncetiveEntryColumns.colMPIFSCCode).ReadOnly = True
            gvItem.Columns(clsMPIncetiveEntryColumns.colMPIFSCCode).TextAlignment = System.Drawing.ContentAlignment.MiddleLeft


            gvItem.Columns(clsMPIncetiveEntryColumns.colMPPhoneNo).FormatString = ""
            gvItem.Columns(clsMPIncetiveEntryColumns.colMPPhoneNo).Width = 150
            gvItem.Columns(clsMPIncetiveEntryColumns.colMPPhoneNo).ReadOnly = True
            gvItem.Columns(clsMPIncetiveEntryColumns.colMPPhoneNo).TextAlignment = System.Drawing.ContentAlignment.MiddleLeft

            gvItem.Columns(clsMPIncetiveEntryColumns.colMPAadharNo).FormatString = ""
            gvItem.Columns(clsMPIncetiveEntryColumns.colMPAadharNo).Width = 150
            gvItem.Columns(clsMPIncetiveEntryColumns.colMPAadharNo).ReadOnly = True
            gvItem.Columns(clsMPIncetiveEntryColumns.colMPAadharNo).TextAlignment = System.Drawing.ContentAlignment.MiddleLeft


            gvItem.Columns(clsMPIncetiveEntryColumns.colQty).FormatString = ""
            gvItem.Columns(clsMPIncetiveEntryColumns.colQty).Width = 120
            gvItem.Columns(clsMPIncetiveEntryColumns.colQty).ReadOnly = False
            gvItem.Columns(clsMPIncetiveEntryColumns.colQty).TextAlignment = System.Drawing.ContentAlignment.MiddleRight

            gvItem.Columns(clsMPIncetiveEntryColumns.colUOM).FormatString = ""
            gvItem.Columns(clsMPIncetiveEntryColumns.colUOM).Width = 80
            gvItem.Columns(clsMPIncetiveEntryColumns.colUOM).ReadOnly = True
            gvItem.Columns(clsMPIncetiveEntryColumns.colUOM).TextAlignment = System.Drawing.ContentAlignment.MiddleLeft



            gvItem.Columns(clsMPIncetiveEntryColumns.colFAT).FormatString = ""
            gvItem.Columns(clsMPIncetiveEntryColumns.colFAT).Width = 120
            gvItem.Columns(clsMPIncetiveEntryColumns.colFAT).ReadOnly = False
            gvItem.Columns(clsMPIncetiveEntryColumns.colFAT).TextAlignment = System.Drawing.ContentAlignment.MiddleRight

            gvItem.Columns(clsMPIncetiveEntryColumns.colSNF).FormatString = ""
            gvItem.Columns(clsMPIncetiveEntryColumns.colSNF).Width = 120
            gvItem.Columns(clsMPIncetiveEntryColumns.colSNF).ReadOnly = False
            gvItem.Columns(clsMPIncetiveEntryColumns.colSNF).TextAlignment = System.Drawing.ContentAlignment.MiddleRight



            gvItem.Columns(clsMPIncetiveEntryColumns.colAmount).Width = 100
            gvItem.Columns(clsMPIncetiveEntryColumns.colAmount).FormatString = "{0:n2}"
            gvItem.Columns(clsMPIncetiveEntryColumns.colAmount).ReadOnly = Not SettApplyPashuAaharAndMineralMixture
            gvItem.Columns(clsMPIncetiveEntryColumns.colAmount).TextAlignment = System.Drawing.ContentAlignment.MiddleRight



            gvItem.Columns(clsMPIncetiveEntryColumns.colAmountActual).Width = 100
            gvItem.Columns(clsMPIncetiveEntryColumns.colAmountActual).FormatString = "{0:n2}"
            gvItem.Columns(clsMPIncetiveEntryColumns.colAmountActual).ReadOnly = True
            gvItem.Columns(clsMPIncetiveEntryColumns.colAmountActual).IsVisible = False
            gvItem.Columns(clsMPIncetiveEntryColumns.colAmountActual).TextAlignment = System.Drawing.ContentAlignment.MiddleRight

            gvItem.Columns(clsMPIncetiveEntryColumns.colPashuAaharQty).Width = 100
            gvItem.Columns(clsMPIncetiveEntryColumns.colPashuAaharQty).FormatString = "{0:n2}"
            gvItem.Columns(clsMPIncetiveEntryColumns.colPashuAaharQty).ReadOnly = Not SettApplyPashuAaharAndMineralMixture
            gvItem.Columns(clsMPIncetiveEntryColumns.colPashuAaharQty).IsVisible = SettApplyPashuAaharAndMineralMixture
            gvItem.Columns(clsMPIncetiveEntryColumns.colPashuAaharQty).TextAlignment = System.Drawing.ContentAlignment.MiddleRight


            gvItem.Columns(clsMPIncetiveEntryColumns.colPashuAaharAmt).Width = 100
            gvItem.Columns(clsMPIncetiveEntryColumns.colPashuAaharAmt).FormatString = "{0:n2}"
            gvItem.Columns(clsMPIncetiveEntryColumns.colPashuAaharAmt).ReadOnly = True
            gvItem.Columns(clsMPIncetiveEntryColumns.colPashuAaharAmt).IsVisible = SettApplyPashuAaharAndMineralMixture
            gvItem.Columns(clsMPIncetiveEntryColumns.colPashuAaharAmt).TextAlignment = System.Drawing.ContentAlignment.MiddleRight

            gvItem.Columns(clsMPIncetiveEntryColumns.colMineralMixtureQty).Width = 100
            gvItem.Columns(clsMPIncetiveEntryColumns.colMineralMixtureQty).FormatString = "{0:n2}"
            gvItem.Columns(clsMPIncetiveEntryColumns.colMineralMixtureQty).ReadOnly = Not SettApplyPashuAaharAndMineralMixture
            gvItem.Columns(clsMPIncetiveEntryColumns.colMineralMixtureQty).IsVisible = SettApplyPashuAaharAndMineralMixture
            gvItem.Columns(clsMPIncetiveEntryColumns.colMineralMixtureQty).TextAlignment = System.Drawing.ContentAlignment.MiddleRight

            gvItem.Columns(clsMPIncetiveEntryColumns.colMineralMixtureAmt).Width = 100
            gvItem.Columns(clsMPIncetiveEntryColumns.colMineralMixtureAmt).FormatString = "{0:n2}"
            gvItem.Columns(clsMPIncetiveEntryColumns.colMineralMixtureAmt).ReadOnly = True
            gvItem.Columns(clsMPIncetiveEntryColumns.colMineralMixtureAmt).IsVisible = SettApplyPashuAaharAndMineralMixture
            gvItem.Columns(clsMPIncetiveEntryColumns.colMineralMixtureAmt).TextAlignment = System.Drawing.ContentAlignment.MiddleRight


            gvItem.Columns(clsMPIncetiveEntryColumns.colSailejQty).Width = 100
            gvItem.Columns(clsMPIncetiveEntryColumns.colSailejQty).FormatString = "{0:n2}"
            gvItem.Columns(clsMPIncetiveEntryColumns.colSailejQty).ReadOnly = Not SettApplyPashuAaharAndMineralMixture
            gvItem.Columns(clsMPIncetiveEntryColumns.colSailejQty).IsVisible = SettApplyPashuAaharAndMineralMixture
            gvItem.Columns(clsMPIncetiveEntryColumns.colSailejQty).TextAlignment = System.Drawing.ContentAlignment.MiddleRight

            gvItem.Columns(clsMPIncetiveEntryColumns.colSailejAmt).Width = 100
            gvItem.Columns(clsMPIncetiveEntryColumns.colSailejAmt).FormatString = "{0:n2}"
            gvItem.Columns(clsMPIncetiveEntryColumns.colSailejAmt).ReadOnly = True
            gvItem.Columns(clsMPIncetiveEntryColumns.colSailejAmt).IsVisible = SettApplyPashuAaharAndMineralMixture
            gvItem.Columns(clsMPIncetiveEntryColumns.colSailejAmt).TextAlignment = System.Drawing.ContentAlignment.MiddleRight

            gvItem.Columns(clsMPIncetiveEntryColumns.colRahatKampekatFeedQty).Width = 100
            gvItem.Columns(clsMPIncetiveEntryColumns.colRahatKampekatFeedQty).FormatString = "{0:n2}"
            gvItem.Columns(clsMPIncetiveEntryColumns.colRahatKampekatFeedQty).ReadOnly = Not SettApplyPashuAaharAndMineralMixture
            gvItem.Columns(clsMPIncetiveEntryColumns.colRahatKampekatFeedQty).IsVisible = SettApplyPashuAaharAndMineralMixture
            gvItem.Columns(clsMPIncetiveEntryColumns.colRahatKampekatFeedQty).TextAlignment = System.Drawing.ContentAlignment.MiddleRight

            gvItem.Columns(clsMPIncetiveEntryColumns.colRahatKampekatFeedAmt).Width = 100
            gvItem.Columns(clsMPIncetiveEntryColumns.colRahatKampekatFeedAmt).FormatString = "{0:n2}"
            gvItem.Columns(clsMPIncetiveEntryColumns.colRahatKampekatFeedAmt).ReadOnly = True
            gvItem.Columns(clsMPIncetiveEntryColumns.colRahatKampekatFeedAmt).IsVisible = SettApplyPashuAaharAndMineralMixture
            gvItem.Columns(clsMPIncetiveEntryColumns.colRahatKampekatFeedAmt).TextAlignment = System.Drawing.ContentAlignment.MiddleRight





            gvItem.Columns(clsMPIncetiveEntryColumns.colTotAmount).Width = 100
            gvItem.Columns(clsMPIncetiveEntryColumns.colTotAmount).FormatString = "{0:n2}"
            gvItem.Columns(clsMPIncetiveEntryColumns.colTotAmount).ReadOnly = True
            gvItem.Columns(clsMPIncetiveEntryColumns.colTotAmount).IsVisible = True
            gvItem.Columns(clsMPIncetiveEntryColumns.colTotAmount).TextAlignment = System.Drawing.ContentAlignment.MiddleRight


            gvItem.Columns(clsMPIncetiveEntryColumns.colDBTProcessed).FormatString = ""
            gvItem.Columns(clsMPIncetiveEntryColumns.colDBTProcessed).Width = 80
            gvItem.Columns(clsMPIncetiveEntryColumns.colDBTProcessed).ReadOnly = True
            gvItem.Columns(clsMPIncetiveEntryColumns.colDBTProcessed).TextAlignment = System.Drawing.ContentAlignment.MiddleLeft

            gvItem.MasterTemplate.SummaryRowsBottom.Clear()
            Dim summaryRowItem As New GridViewSummaryRowItem()
            summaryRowItem.Add(New GridViewSummaryItem(clsMPIncetiveEntryColumns.colQty, "{0:n2}", GridAggregateFunction.Sum))
            summaryRowItem.Add(New GridViewSummaryItem(clsMPIncetiveEntryColumns.colAmount, "{0:n2}", GridAggregateFunction.Sum))
            summaryRowItem.Add(New GridViewSummaryItem(clsMPIncetiveEntryColumns.colAmountActual, "{0:n2}", GridAggregateFunction.Sum))
            summaryRowItem.Add(New GridViewSummaryItem(clsMPIncetiveEntryColumns.colPashuAaharAmt, "{0:n2}", GridAggregateFunction.Sum))
            summaryRowItem.Add(New GridViewSummaryItem(clsMPIncetiveEntryColumns.colMineralMixtureQty, "{0:n2}", GridAggregateFunction.Sum))
            summaryRowItem.Add(New GridViewSummaryItem(clsMPIncetiveEntryColumns.colMineralMixtureAmt, "{0:n2}", GridAggregateFunction.Sum))
            summaryRowItem.Add(New GridViewSummaryItem(clsMPIncetiveEntryColumns.colSailejQty, "{0:n2}", GridAggregateFunction.Sum))
            summaryRowItem.Add(New GridViewSummaryItem(clsMPIncetiveEntryColumns.colSailejAmt, "{0:n2}", GridAggregateFunction.Sum))
            summaryRowItem.Add(New GridViewSummaryItem(clsMPIncetiveEntryColumns.colRahatKampekatFeedQty, "{0:n2}", GridAggregateFunction.Sum))
            summaryRowItem.Add(New GridViewSummaryItem(clsMPIncetiveEntryColumns.colRahatKampekatFeedAmt, "{0:n2}", GridAggregateFunction.Sum))
            summaryRowItem.Add(New GridViewSummaryItem(clsMPIncetiveEntryColumns.colTotAmount, "{0:n2}", GridAggregateFunction.Sum))
            gvItem.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

            gvItem.BestFitColumns()

            ReStoreGridLayout()

            gvItem.AllowAddNewRow = False
            gvItem.AllowDeleteRow = True
            gvItem.AllowRowReorder = False
            gvItem.ShowGroupPanel = False
            gvItem.EnableFiltering = True
            gvItem.ShowFilteringRow = True
            gvItem.EnableSorting = False
            gvItem.EnableGrouping = False
            gvItem.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
            gvItem.GridBehavior = New MyBehavior()


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
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub txtDocumentNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtDocumentNo._MYValidating
        Try
            txtDocumentNo.Value = clsMPIncentiveEntry.getFinder("", txtDocumentNo.Value, isButtonClicked)
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
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub DisableInputDataField()
        txtdate.Enabled = False
        txtMCC.Enabled = False
        txtFromDate.Enabled = False
        txtToDate.Enabled = False
    End Sub
    Sub EnableInputDataField()
        txtdate.Enabled = True
        txtMCC.Enabled = True
        txtFromDate.Enabled = True
        txtToDate.Enabled = True
        If clsCommon.myLen(SettMCCOneDBTOneDoc) > 0 Then
            txtMCC.Enabled = False
        End If
    End Sub
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
            gvItem.CurrentColumn = gvItem.Columns(clsMPIncetiveEntryColumns.colMPUploaderCode)
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
            gvItem.CurrentColumn = gvItem.Columns(clsMPIncetiveEntryColumns.colMPUploaderCode)
            gvItem.CurrentRow = gvItem.Rows(gvItem.Rows.Count - 1)
            gvItem.PerformLayout()
            gvItem.BeginEdit()
        End If
    End Sub
    Private Sub gvItem_KeyDown(sender As Object, e As KeyEventArgs) Handles gvItem.KeyDown
        If e.KeyCode = Keys.Enter Then
            If gvItem.CurrentCell Is Nothing Then
                Exit Sub
            End If
            If (gvItem.CurrentCell.ColumnInfo.Name = clsMPIncetiveEntryColumns.colAmount) Then
                GridFocus(gvItem.CurrentRow)
            ElseIf (gvItem.CurrentCell.ColumnInfo.Name = clsMPIncetiveEntryColumns.colMPUploaderCode) Then
                gvItem.EndEdit()
                gvItem.CurrentColumn = gvItem.Columns(clsMPIncetiveEntryColumns.colQty)
                gvItem.BeginEdit()
            ElseIf (gvItem.CurrentCell.ColumnInfo.Name = clsMPIncetiveEntryColumns.colMPUploaderCode Or gvItem.CurrentCell.ColumnInfo.Name = clsMPIncetiveEntryColumns.colMPName Or gvItem.CurrentCell.ColumnInfo.Name = clsMPIncetiveEntryColumns.colUOM Or gvItem.CurrentCell.ColumnInfo.Name = clsMPIncetiveEntryColumns.colQty) Then
                gvItem.EndEdit()
                gvItem.CurrentColumn = gvItem.Columns(gvItem.CurrentCell.ColumnInfo.Index + 1)
                gvItem.BeginEdit()
            End If
        End If
    End Sub
    Private Sub txtMCC__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtMCC._MYValidating
        Dim strOLDMCC As String = txtMCC.Value
        Dim wrh As String = "TSPL_MCC_MASTER.MCC_CODE in (" + arrLoc + ")"
        If clsCommon.myLen(SettMCCOneDBTOneDoc) > 0 Then
            wrh += " and TSPL_MCC_MASTER.MCC_CODE='" + SettMCCOneDBTOneDoc + "'"
        End If
        txtMCC.Value = clsMccMaster.getFinder(wrh, txtMCC.Value, isButtonClicked)
        lblMCCDesc.Text = clsDBFuncationality.getSingleValue("Select MCC_NAME from TSPL_MCC_MASTER where MCC_CODE ='" + txtMCC.Value + "' ")
        If Not clsCommon.CompairString(strOLDMCC, txtMCC.Value) = CompairStringResult.Equal Then
            mtxtVLC.arrValueMember = Nothing
            loadBlankGrid()
            SetToDate()
        End If
    End Sub
    Private Sub txtFromDate_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtFromDate.Validating
        SetToDate()
    End Sub
    Private Sub txtFromDate_Leave(sender As Object, e As EventArgs) Handles txtFromDate.Leave
        SetToDate()
    End Sub
    Sub SetToDate()
        If Not IsinsideLoadData Then
            Dim PaymentCycleType As String = ""
            Dim PaymentCycleValue As Integer = 0
            ' If Not isLoad Then
            If clsCommon.myLen(txtMCC.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please select the MCC first")
                Exit Sub
            End If
            If SettMPIncentiveEntryApplyMonthly Then
                txtFromDate.Value = New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1)
                txtToDate.Value = txtFromDate.Value.AddMonths(1).AddDays(-1)
            Else
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select TSPL_MCC_MASTER.Payment_Cycle,TSPL_PAYMENT_CYCLE_MASTER.PC_TYPE,TSPL_PAYMENT_CYCLE_MASTER.PC_VALUE  from TSPL_MCC_MASTER left outer join TSPL_PAYMENT_CYCLE_MASTER on TSPL_PAYMENT_CYCLE_MASTER.PC_CODE=TSPL_MCC_MASTER.Payment_Cycle   where TSPL_MCC_MASTER.MCC_Code  in ('" + txtMCC.Value + "') ")
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    clsCommon.MyMessageBoxShow("No Payment Cycle found on current MCC/Location")
                    Exit Sub
                End If
                PaymentCycleType = clsCommon.myCstr(dt.Rows(0)("PC_TYPE"))
                PaymentCycleValue = clsCommon.myCdbl(dt.Rows(0)("PC_VALUE"))
                Dim dtCurr As DateTime = clsCommon.GETSERVERDATE()
                If clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal Then
                    If txtFromDate.Value.Day Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                        clsCommon.MyMessageBoxShow("Date can only be first day of month or at interval of " & PaymentCycleValue & " Day, Because MCC has payment Cycle of " & PaymentCycleValue & " Day ")
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
                        clsCommon.MyMessageBoxShow("Date can only be first day of month, Because MCC has payment Cycle of Month Type")
                        txtFromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                        txtToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                        Exit Sub
                    End If
                    txtToDate.Value = DateAdd(DateInterval.Month, PaymentCycleValue, txtFromDate.Value)
                ElseIf clsCommon.CompairString(PaymentCycleType, "Year") = CompairStringResult.Equal Then
                    If clsCommon.myCdbl(clsCommon.GetPrintDate(txtFromDate.Value, "dd")) <> 1 Then
                        clsCommon.MyMessageBoxShow("Date can only be first day of month, Because MCC has payment Cycle of Year Type")
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
    End Sub
    Private Sub mtxtVLC__My_Click(sender As Object, e As EventArgs) Handles mtxtVLC._My_Click
        AddVLC(False)
    End Sub

    Sub AddVLC(ByVal isForAddOnly As Boolean)
        Try
            If clsCommon.myLen(clsCommon.myCstr(txtMCC.Value)) <= 0 Then
                txtMCC.Focus()
                Throw New Exception("Please Select MCC First.")
            End If

            Dim arrOLDVLC As ArrayList = TryCast(mtxtVLC.Tag, ArrayList)
            Dim arrOLDVLCUploader As ArrayList = mtxtVLC.arrValueMember

            Dim qry As String = "Select VLC.VLC_Code_vlc_Uploader as [Code],VLC.VLC_Code as [VLC Code],VLC.VLC_Name as [VLC Name],VLC.MCC as [MCC Code],VLC.Route_Code as [Route Code],RM.Route_Name " &
                      " from TSPL_VLC_MASTER_HEAD VLC left join TSPL_MCC_ROUTE_MASTER RM on vlc.Route_Code=RM.Route_Code  left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =  VLC.VSP_Code where 2=2 "
            If clsCommon.myLen(SettMCCOneDBTOneDoc) <= 0 Then
                If clsCommon.myLen(txtMCC.Value) > 0 Then
                    qry += " and VLC.MCC='" & clsCommon.myCstr(txtMCC.Value) & "'"
                End If
            End If
            If ApplyZoneWiseVSP = True Then
                If clsCommon.myLen(objCommonVar.strCurrUserZones) > 0 Then
                    qry += " and TSPL_VENDOR_MASTER.Zone_Code in (" + objCommonVar.strCurrUserZones + ")"
                End If
            End If
            Dim arrNewVLC As ArrayList = clsCommon.ShowMultipleSelectForm("MPIncVLC@N", qry, "VLC Code", "VLC Code", IIf(isForAddOnly, Nothing, arrOLDVLC), Nothing)
            mtxtVLC.Tag = arrNewVLC
            Dim arrVLCUploader As ArrayList = Nothing
            If arrNewVLC IsNot Nothing AndAlso arrNewVLC.Count > 0 Then
                arrVLCUploader = New ArrayList()
                qry = qry + " and VLC.VLC_Code in (" + clsCommon.GetMulcallString(arrNewVLC) + ")"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        If Not arrVLCUploader.Contains(clsCommon.myCstr(dt.Rows(0).Item("Code"))) Then
                            arrVLCUploader.Add(clsCommon.myCstr(dt.Rows(0).Item("Code")))
                        End If
                    Next
                End If
            End If
            'If clsCommon.myLen(SettMCCOneDBTOneDoc) > 0 Then
            If arrOLDVLCUploader IsNot Nothing AndAlso arrOLDVLCUploader.Count > 0 Then
                    If arrVLCUploader Is Nothing OrElse arrVLCUploader.Count <= 0 Then
                        arrVLCUploader = New ArrayList()
                    End If
                    For ii As Integer = 0 To arrOLDVLCUploader.Count - 1
                        arrVLCUploader.Add(arrOLDVLCUploader(ii))
                    Next
                End If
                If arrOLDVLC IsNot Nothing AndAlso arrOLDVLC.Count > 0 Then
                    If arrNewVLC Is Nothing OrElse arrVLCUploader.Count <= 0 Then
                        arrNewVLC = New ArrayList()
                    End If
                    For ii As Integer = 0 To arrOLDVLC.Count - 1
                        arrNewVLC.Add(arrOLDVLC(ii))
                    Next
                End If
            'End If

            mtxtVLC.arrValueMember = arrVLCUploader
            fillMPS(arrNewVLC)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub fillMPS(ByVal arrVLC As ArrayList)
        If arrVLC IsNot Nothing AndAlso arrVLC.Count > 0 Then
            Dim arrOLD As New Dictionary(Of String, clsTempFATSNFAmt)
            For ii As Integer = 0 To gvItem.Rows.Count - 1
                If clsCommon.myCdbl(gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colQty).Value) > 0 Then

                    Dim objTemp As New clsTempFATSNFAmt
                    Try
                        objTemp.PKID = clsCommon.myCdbl(gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colPKID).Value)
                    Catch ex As Exception
                    End Try

                    objTemp.Qty = clsCommon.myCdbl(gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colQty).Value)
                    objTemp.FAT = clsCommon.myCdbl(gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colFAT).Value)
                    objTemp.SNF = clsCommon.myCdbl(gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colSNF).Value)
                    objTemp.Amt = clsCommon.myCdbl(gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colAmount).Value)
                    objTemp.PashuAaharQty = clsCommon.myCdbl(gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colPashuAaharQty).Value)
                    objTemp.MineralMixtureQty = clsCommon.myCdbl(gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colMineralMixtureQty).Value)
                    objTemp.SailejQty = clsCommon.myCdbl(gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colSailejQty).Value)
                    objTemp.RahatKampekatFeedQty = clsCommon.myCdbl(gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colRahatKampekatFeedQty).Value)
                    arrOLD.Add(clsCommon.myCstr(gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colMPCode).Value), objTemp)
                End If
            Next
            loadBlankGrid()
            Dim Mcc_Uom As String = clsDBFuncationality.getSingleValue("select Uom_Code from TSPL_Mcc_UOM_DETAIL where Stocking_Unit='Y' and  MCC_CODE='" & txtMCC.Value & "'")
            '            Qry = "select TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name
            ',TSPL_MP_MASTER.MP_Code_VLC_Uploader,TSPL_MP_MASTER.MP_Code,TSPL_MP_MASTER.MP_Name ,TSPL_MP_MASTER.BankName,TSPL_MP_MASTER.AccountNO,TSPL_MP_MASTER.Telphone,TSPL_MP_MASTER.Fax,TSPL_MP_MASTER.IFCICode
            'from TSPL_MP_MASTER 
            'left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MP_MASTER.VLC_Code
            'where TSPL_MP_MASTER.VLC_Code	 in (" + clsCommon.GetMulcallString(arrVLC) + ") "

            Qry = clsMPIncentiveEntryDetail.getQry("", Mcc_Uom, arrVLC)

            Dim dt As DataTable = Nothing
            Try
                dt = clsDBFuncationality.GetDataTable(Qry + " order by TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_MP_MASTER.VLC_Code,cast(TSPL_MP_MASTER.MP_Code_VLC_Uploader as integer)")
            Catch ex As Exception
                dt = clsDBFuncationality.GetDataTable(Qry + " order by TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_MP_MASTER.VLC_Code,TSPL_MP_MASTER.MP_Code_VLC_Uploader ")
            End Try
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gvItem.DataSource = dt
                FormatGrid()
                For ii As Integer = 0 To gvItem.Rows.Count - 1
                    If arrOLD.ContainsKey(clsCommon.myCstr(gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colMPCode).Value)) Then
                        Dim objTemp As clsTempFATSNFAmt = arrOLD(clsCommon.myCstr(gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colMPCode).Value))
                        gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colPKID).Value = objTemp.PKID
                        gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colAmount).Value = objTemp.Amt
                        gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colQty).Value = objTemp.Qty
                        gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colFAT).Value = objTemp.FAT
                        gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colSNF).Value = objTemp.SNF
                        gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colPashuAaharQty).Value = objTemp.PashuAaharQty
                        gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colMineralMixtureQty).Value = objTemp.MineralMixtureQty
                        gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colSailejQty).Value = objTemp.SailejQty
                        gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colRahatKampekatFeedQty).Value = objTemp.RahatKampekatFeedQty
                    End If
                Next
            End If
        End If
    End Sub
    Sub UpdateAllRow()
        If Not IsinsideLoadData Then
            For ii As Integer = 0 To gvItem.Rows.Count - 1
                UpdateRow(ii)
            Next
        End If
    End Sub

    Sub UpdateRow(ByVal ii As Integer)
        gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colAmountActual).Value = (clsCommon.myCdbl(gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colQty).Value) * txtIncentiveRate.Value)
        If Not SettApplyPashuAaharAndMineralMixture Then
            gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colAmount).Value = Math.Ceiling(clsCommon.myCdbl(gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colQty).Value) * txtIncentiveRate.Value)
        End If
        gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colPashuAaharAmt).Value = Math.Ceiling(clsCommon.myCdbl(gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colPashuAaharQty).Value) * txtPashuAahar.Value)
        gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colMineralMixtureAmt).Value = Math.Ceiling(clsCommon.myCdbl(gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colMineralMixtureQty).Value) * txtMineralMixture.Value)
        gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colSailejAmt).Value = Math.Ceiling(clsCommon.myCdbl(gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colSailejQty).Value) * txtSailejRate.Value)
        gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colRahatKampekatFeedAmt).Value = Math.Ceiling(clsCommon.myCdbl(gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colRahatKampekatFeedQty).Value) * txtRahatKampekatFeedRate.Value)
        gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colTotAmount).Value = clsCommon.myCDecimal(gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colAmount).Value) + clsCommon.myCDecimal(gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colPashuAaharAmt).Value) + clsCommon.myCDecimal(gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colMineralMixtureAmt).Value) + clsCommon.myCDecimal(gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colSailejAmt).Value) + clsCommon.myCDecimal(gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colRahatKampekatFeedAmt).Value)
    End Sub
    Private Sub txtIncentiveRate_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtIncentiveRate.Validating, txtPashuAahar.Validating, txtMineralMixture.Validating, txtSailejRate.Validating, txtRahatKampekatFeedRate.Validating
        UpdateAllRow()
    End Sub
    Private Sub rmimport_Click(sender As Object, e As EventArgs) Handles rmimport.Click
        Dim dgv As New RadGridView
        Me.Controls.Add(dgv)
        Try
            Dim flag As Boolean = False
            'If SettApplyPashuAaharAndMineralMixture Then
            '    flag = transportSql.importExcel(dgv, "SNo.", "VLC", "Farmer Code", "Farmer", "Farmer Name", "Bank", "Account No", "IFSC", "Phone No", "Aadhar No", "Qty", "UOM", "FAT", "SNF", "Amount", "Pashu Aahar Qty", "Pashu Aahar Amount", "Mineral Mixture Qty", "Mineral Mixture Amount", "Sailej Qty", "Sailej Amount", "Rahat Kampekat Feed Qty", "Rahat Kampekat Feed Amount", "Total Amount")
            'Else
            '    flag = transportSql.importExcel(dgv, "SNo.", "VLC", "Farmer Code", "Farmer", "Farmer Name", "Bank", "Account No", "IFSC", "Phone No", "Aadhar No", "Qty", "UOM", "FAT", "SNF", "Amount", "Total Amount")
            'End If
            Dim ListColumns As List(Of String) = Nothing
            If gvItem IsNot Nothing AndAlso gvItem.Columns.Count > 0 Then
                ListColumns = New List(Of String)
                For Each GC As GridViewColumn In gvItem.Columns
                    ListColumns.Add(GC.HeaderText)
                Next
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No column Found!", Me.Text)
                Exit Sub
            End If
            flag = transportSql.importExcel(dgv, ListColumns)
            If flag Then
                Dim arrOLD As New Dictionary(Of String, clsTempFATSNFAmt)
                For ii As Integer = 0 To dgv.Rows.Count - 1
                    flag = (clsCommon.myCdbl(dgv.Rows(ii).Cells("Qty").Value) > 0)
                    If SettApplyPashuAaharAndMineralMixture Then
                        flag = flag OrElse (clsCommon.myCdbl(dgv.Rows(ii).Cells("Pashu Aahar Qty").Value) > 0) OrElse (clsCommon.myCdbl(dgv.Rows(ii).Cells("Mineral Mixture Qty").Value) > 0) OrElse (clsCommon.myCdbl(dgv.Rows(ii).Cells("Sailej Qty").Value) > 0) OrElse (clsCommon.myCdbl(dgv.Rows(ii).Cells("Rahat Kampekat Feed Qty").Value) > 0)
                    End If
                    If flag Then
                        If clsCommon.myLen(dgv.Rows(ii).Cells("Farmer Code").Value) <= 0 AndAlso clsCommon.myLen(dgv.Rows(ii).Cells("Farmer").Value) > 0 AndAlso clsCommon.myLen(dgv.Rows(ii).Cells("VLC").Value) > 0 Then
                            Qry = "select TSPL_MP_MASTER.MP_Code from TSPL_MP_MASTER 
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MP_MASTER.VLC_Code
where TSPL_VLC_MASTER_HEAD.MCC='" + txtMCC.Value + "' and TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader='" + clsCommon.myCstr(dgv.Rows(ii).Cells("VLC").Value) + "' and TSPL_MP_MASTER.MP_Code_VLC_Uploader='" + clsCommon.myCstr(dgv.Rows(ii).Cells("Farmer").Value) + "'"
                            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
                            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                                ''Generate new MP  
                                Dim objNewMP = New clsMpMaster()
                                objNewMP.isNewEntry = True
                                Dim dtServer As Date = clsCommon.GETSERVERDATE(Nothing, "dd/MMM/yyyy")
                                objNewMP.MP_Code = clsERPFuncationality.GetNextCode(Nothing, dtServer, clsDocType.MPMaster, "", "")
                                If clsCommon.myLen(objNewMP.MP_Code) <= 0 Then
                                    Throw New Exception("Error In Document Code Genertion")
                                End If
                                objNewMP.MP_CODE_VLC_UPLOADER = clsCommon.myCstr(dgv.Rows(ii).Cells("Farmer").Value)
                                objNewMP.MCC_Code = clsCommon.myCstr(dgv.Rows(ii).Cells("VLC").Value)
                                Qry = "select VLC_Code from tspl_vlc_master_head where VLC_Code_VLC_Uploader='" + objNewMP.MCC_Code + "'"
                                Dim dtVLCCode As DataTable = clsDBFuncationality.GetDataTable(Qry)
                                If dtVLCCode Is Nothing OrElse dtVLCCode.Rows.Count <= 0 Then
                                    Throw New Exception("Invalid VLC uploader Code [" + objNewMP.MCC_Code + "]")
                                End If
                                If dtVLCCode.Rows.Count > 1 Then
                                    Throw New Exception("VLC uploader Code [" + objNewMP.MCC_Code + "] is not unique.")
                                End If
                                objNewMP.MCC_Code = clsCommon.myCstr(dtVLCCode.Rows(0)("VLC_Code"))
                                objNewMP.Modified_By = objCommonVar.CurrentUserCode
                                objNewMP.Modified_Date = clsCommon.GetPrintDate(dtServer, "dd/MMM/yyyy")
                                objNewMP.Comp_Code = objCommonVar.CurrentCompanyCode
                                If objNewMP.isNewEntry Then
                                    objNewMP.Created_By = objCommonVar.CurrentUserCode
                                    objNewMP.Created_Date = clsCommon.GetPrintDate(dtServer, "dd/MMM/yyyy")
                                End If

                                objNewMP.Fax = clsCommon.myCstr(dgv.Rows(ii).Cells("Aadhar No").Value)
                                objNewMP.Telphone = clsCommon.myCstr(dgv.Rows(ii).Cells("Phone No").Value)
                                objNewMP.IFCICode = clsCommon.myCstr(dgv.Rows(ii).Cells("IFSC").Value)


                                objNewMP.BankName = clsCommon.myCstr(dgv.Rows(ii).Cells("Bank").Value)
                                objNewMP.AccountNO = clsCommon.myCstr(dgv.Rows(ii).Cells("Account No").Value)
                                objNewMP.MP_Name = clsCommon.myCstr(dgv.Rows(ii).Cells("Farmer Name").Value)
                                objNewMP.Form_Id = clsUserMgtCode.frmMPMaster
                                clsMpMaster.SaveData(objNewMP, Nothing)
                                dgv.Rows(ii).Cells("Farmer Code").Value = objNewMP.MP_Code
                            End If
                        End If
                        Dim objTemp As New clsTempFATSNFAmt
                        objTemp.Qty = clsCommon.myCdbl(dgv.Rows(ii).Cells("Qty").Value)
                        objTemp.FAT = clsCommon.myCdbl(dgv.Rows(ii).Cells("FAT").Value)
                        objTemp.SNF = clsCommon.myCdbl(dgv.Rows(ii).Cells("SNF").Value)
                        If SettApplyPashuAaharAndMineralMixture Then
                            objTemp.PashuAaharQty = clsCommon.myCDecimal(dgv.Rows(ii).Cells("Pashu Aahar Qty").Value)
                            objTemp.MineralMixtureQty = clsCommon.myCDecimal(dgv.Rows(ii).Cells("Mineral Mixture Qty").Value)
                            objTemp.SailejQty = clsCommon.myCDecimal(dgv.Rows(ii).Cells("Sailej Qty").Value)
                            objTemp.RahatKampekatFeedQty = clsCommon.myCDecimal(dgv.Rows(ii).Cells("Rahat Kampekat Feed Qty").Value)
                            objTemp.Amt = clsCommon.myCDecimal(dgv.Rows(ii).Cells("Amount").Value)
                        End If
                        arrOLD.Add(clsCommon.myCstr(dgv.Rows(ii).Cells("Farmer Code").Value), objTemp)


                    End If
                Next

                Dim arrVLC As ArrayList = TryCast(mtxtVLC.Tag, ArrayList)
                fillMPS(arrVLC)

                For ii As Integer = 0 To gvItem.Rows.Count - 1
                    If arrOLD.ContainsKey(clsCommon.myCstr(gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colMPCode).Value)) Then
                        Dim objTemp As clsTempFATSNFAmt = arrOLD(clsCommon.myCstr(gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colMPCode).Value))
                        gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colQty).Value = objTemp.Qty
                        gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colFAT).Value = objTemp.FAT
                        gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colSNF).Value = objTemp.SNF
                        If SettApplyPashuAaharAndMineralMixture Then
                            gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colAmount).Value = objTemp.Amt
                        End If
                        gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colPashuAaharQty).Value = objTemp.PashuAaharQty
                        gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colMineralMixtureQty).Value = objTemp.MineralMixtureQty
                        gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colSailejQty).Value = objTemp.SailejQty
                        gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colRahatKampekatFeedQty).Value = objTemp.RahatKampekatFeedQty
                    End If
                Next

                UpdateAllRow()
            End If
            common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
        Catch ex As Exception
            myMessages.myExceptions(ex)
        Finally
            Me.Controls.Remove(dgv)
        End Try
    End Sub
    Private Sub rmExport_Click(sender As Object, e As EventArgs) Handles rmExport.Click
        Try
            transportSql.exportdata(gvItem, Me.Text, "")
            'clsCommon.MyExportToExcel("", gvItem, Nothing, Me.Text)
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Try
            Dim qry As String = ""
            Dim msg As String = ""
            Dim dt As DataTable = Nothing

            If (myMessages.postConfirm()) Then
                clsMPIncentiveEntry.PostData(txtDocumentNo.Value)
                clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                LoadData(txtDocumentNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub



    Private Sub gvItem_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvItem.CellValueChanged
        Try
            If (Not IsinsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gvItem.Columns(clsMPIncetiveEntryColumns.colQty) OrElse e.Column Is gvItem.Columns(clsMPIncetiveEntryColumns.colPashuAaharQty) OrElse e.Column Is gvItem.Columns(clsMPIncetiveEntryColumns.colMineralMixtureQty) OrElse e.Column Is gvItem.Columns(clsMPIncetiveEntryColumns.colSailejQty) OrElse e.Column Is gvItem.Columns(clsMPIncetiveEntryColumns.colRahatKampekatFeedQty) Then
                        UpdateRow(gvItem.CurrentRow.Index)
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isCellValueChangedOpen = False
        End Try
    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gvItem.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gvItem.SaveLayout(obj.GridLayout)
            obj.GridColumns = gvItem.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub


    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)

        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvItem.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvItem.Columns.Count - 1 Step ii + 1
                        gvItem.Columns(ii).IsVisible = False
                        gvItem.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gvItem.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub RadMenuItem4_Click(sender As Object, e As EventArgs) Handles RadMenuItem4.Click
        clsMPIncentiveEntry.MultipleDateSingleExport(Me)
    End Sub
    Private Sub RadMenuItem5_Click(sender As Object, e As EventArgs) Handles RadMenuItem5.Click
        clsMPIncentiveEntry.MultipleDateSingleImport(Me)
    End Sub

    Private Sub btnAddMissing_Click(sender As Object, e As EventArgs) Handles btnAddMissing.Click
        AddVLC(True)
    End Sub


End Class

Class MyBehavior
    Inherits BaseGridBehavior
    Public Overrides Function ProcessKeyDown(keys__1 As KeyEventArgs) As Boolean
        If keys__1.KeyData = Keys.Enter AndAlso Me.GridControl.IsInEditMode Then
            Me.GridControl.GridNavigator.SelectNextColumn()
        ElseIf keys__1.KeyData = Keys.Up AndAlso Me.GridControl.IsInEditMode Then
            Me.GridControl.GridNavigator.SelectPreviousRow(1)
        ElseIf keys__1.KeyData = Keys.Down AndAlso Me.GridControl.IsInEditMode Then
            Me.GridControl.GridNavigator.SelectNextRow(1)
        ElseIf keys__1.KeyData = Keys.Right AndAlso Me.GridControl.IsInEditMode Then
            Me.GridControl.GridNavigator.SelectNextColumn()
        ElseIf keys__1.KeyData = Keys.Left AndAlso Me.GridControl.IsInEditMode Then
            Me.GridControl.GridNavigator.SelectPreviousColumn()
        End If

        Return True
    End Function
End Class