Imports System.Data.SqlClient
Imports common
Imports System.IO

Public Class frmDBTMonthlyFarmerMilk
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Dim IsinsideLoadData As Boolean = False
#End Region
    Public Sub New()
        InitializeComponent()
    End Sub
    Private Sub FrmVLCDataUploaderManual_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim coll As New Dictionary(Of String, String)()
        coll = New Dictionary(Of String, String)()
        coll.Add("Document_Code", "Varchar(30) NOT NULL primary key")
        coll.Add("Document_Date", "datetime not NULL")
        coll.Add("DBT_Reco_Code", "varchar(30) not NULL references TSPL_DCS_MP_INCENTIVE_RECO_HEAD (Document_Code) ")
        coll.Add("Created_By", "varchar(12) NOT NULL")
        coll.Add("Created_Date", "Datetime NOT NULL")
        coll.Add("Modified_By", "varchar(12) NOT NULL")
        coll.Add("Modified_Date", "Datetime NOT NULL")
        coll.Add("Posted_By", "varchar(12)   NULL")
        coll.Add("Posting_Date", "Datetime   NULL")
        coll.Add("Status", "int Null")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_DBT_MONTHLY_FARMER_MILK", coll, Nothing, False, False, "", "Document_Code", "Document_Date")

        coll = New Dictionary(Of String, String)()
        coll.Add("PK_Id", "integer NOT NULL identity NOT FOR REPLICATION primary key")
        coll.Add("Document_Code", "varchar(30) not NULL unique references TSPL_DBT_MONTHLY_FARMER_MILK (Document_Code) ")
        coll.Add("VLC_Code", "Varchar(30) not null REFERENCES TSPL_VLC_MASTER_HEAD (VLC_Code)")
        coll.Add("MP_Code", "varchar(30) null REFERENCES TSPL_MP_MASTER (MP_Code)")
        coll.Add("Cycle_No", "integer NULL ")
        coll.Add("Cycle_Month", "integer NULL ")
        coll.Add("Cycle_Year", "integer NULL ")
        coll.Add("Qty", "Decimal(18,2) null")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_DBT_MONTHLY_FARMER_MILK_DETAIL", coll, "", True, False, "TSPL_MP_INCENTIVE_ENTRY_HEAD", "Document_Code", "")



        SetUserMgmtNew()
        Reset()


        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Transaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Transaction")
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
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclose.Enabled Then
            CloseForm()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnclose.Enabled Then
            'PostData()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine +
                      "========Table Name=========" + Environment.NewLine +
                      "TSPL_DBT_MONTHLY_FARMER_MILK" + Environment.NewLine +
                      "TSPL_DBT_MONTHLY_FARMER_MILK_DETAIL" + Environment.NewLine)
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
        'If Not (MyBase.isReadFlag) Then
        '    Throw New Exception("Permission Denied")
        'End If
        'btnsave.Visible = MyBase.isModifyFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
        'btnPost.Visible = MyBase.isPostFlag
        'If MyBase.isExport = True Then
        '    rmimport.Enabled = True
        '    rmExport.Enabled = True
        '    RadMenuItem4.Enabled = True
        '    RadMenuItem5.Enabled = True
        'Else
        '    rmimport.Enabled = False
        '    rmExport.Enabled = False
        '    RadMenuItem4.Enabled = False
        '    RadMenuItem5.Enabled = False
        'End If

        'RadMenu1.Visible = MyBase.isExport
    End Sub
    Sub Reset()
        'loadBlankGrid()
        'Dim dt As Date = clsCommon.GETSERVERDATE()
        'txtFromDate.Value = "01/" & DatePart(DateInterval.Month, dt) & "/" & DatePart(DateInterval.Year, dt)
        'If SettMPIncentiveEntryApplyMonthly Then
        '    txtToDate.Value = txtFromDate.Value.AddMonths(1).AddDays(-1)
        'Else
        '    txtToDate.Value = "01/" & DatePart(DateInterval.Month, dt) & "/" & DatePart(DateInterval.Year, dt)
        'End If
        'txtDocumentNo.Value = ""
        'txtDBTReco.Value = ""
        'lblMCCDesc.Text = ""
        'txtdate.Value = dt
        'txtDocumentNo.MyReadOnly = False
        'btnsave.Text = "Save"
        'mtxtVLC.arrValueMember = Nothing
        'txtIncentiveRate.Value = 0
        'btndelete.Enabled = False
        'btnsave.Enabled = True
        'btnPost.Enabled = False
        'txtdate.Focus()
        'txtDBTReco.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        'If clsCommon.myLen(SettMCCOneDBTOneDoc) > 0 Then
        '    txtDBTReco.Value = SettMCCOneDBTOneDoc
        'End If
        'If clsCommon.myLen(txtDBTReco.Value) > 0 Then
        '    lblMCCDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MCC_NAME from tspl_mcc_master where mcc_Code='" & txtDBTReco.Value & "' "))
        '    If clsCommon.myLen(lblMCCDesc.Text) <= 0 Then
        '        txtDBTReco.Value = ""
        '    End If
        'End If
        'EnableInputDataField()
        'isNewEntry = True
        'IsinsideLoadData = False
        'lblPending.Status = ERPTransactionStatus.Pending
        'rbtnFATSNFPer.IsChecked = True
        'rbtnFATSNFKG.IsChecked = False
        'txtIncentiveRate.Value = SettMPIncentiveEntryIncentiveRate
        'mtxtVLC.Enabled = True
        'txtDBTReco.Enabled = True
        'btnAddMissing.Enabled = False
    End Sub

    Private Function AllowToSave() As Boolean
        'Xtra.TransactionValidity(txtdate.Value)
        'If AllowFutureDateTransaction(txtdate.Value, Nothing) = False Then
        '    txtdate.Focus()
        '    Return False
        'End If
        'If clsCommon.myLen(txtDBTReco.Value) <= 0 Then
        '    txtDBTReco.Focus()
        '    Throw New Exception("Please select MCC")
        'End If
        'If clsCommon.GetDateWithStartTime(txtFromDate.Value) = clsCommon.GetDateWithStartTime(txtToDate.Value) Then
        '    txtFromDate.Focus()
        '    Throw New Exception("Please select From Date")
        'End If
        'If SettApplyPashuAaharAndMineralMixture Then
        '    If clsCommon.myLen(txtPashuAahar.Value) <= 0 Then
        '        txtIncentiveRate.Focus()
        '        Throw New Exception("Please enter incentive Rate")
        '    End If
        '    If clsCommon.myLen(txtMineralMixture.Value) <= 0 Then
        '        txtIncentiveRate.Focus()
        '        Throw New Exception("Please enter Mineral Mixture")
        '    End If
        '    If clsCommon.myLen(txtSailejRate.Value) <= 0 Then
        '        txtIncentiveRate.Focus()
        '        Throw New Exception("Please enter Sailej Rate")
        '    End If
        '    If clsCommon.myLen(txtRahatKampekatFeedRate.Value) <= 0 Then
        '        txtIncentiveRate.Focus()
        '        Throw New Exception("Please enter Rahat Kampekat Feed Rate")
        '    End If
        'Else
        '    If clsCommon.myLen(txtIncentiveRate.Value) <= 0 Then
        '        txtIncentiveRate.Focus()
        '        Throw New Exception("Please enter incentive Rate")
        '    End If
        'End If

        'If mtxtVLC.arrValueMember Is Nothing AndAlso mtxtVLC.arrValueMember.Count <= 0 Then
        '    mtxtVLC.Focus()
        '    Throw New Exception("Please select at least one VLC")
        'End If
        UpdateAllRow()
        Return True
    End Function
    Sub SaveData()
        Try
            If AllowToSave() Then
                Dim obj As New clsMPIncentiveEntry()
                obj.Document_Code = txtDocumentNo.Value
                obj.Document_Date = txtdate.Value
                obj.MCC_Code = txtDBTReco.Value
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
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End If
        End Try
    End Sub
    Private Sub DeleteData()
        Try
            clsLockMPPaymentCycle.LockMPTransaction(txtDBTReco.Value, txtdate.Value)
            If (deleteConfirm()) Then
                If (clsMPIncentiveEntry.DeleteData(txtDocumentNo.Value)) Then
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
        Dim obj As clsMPIncentiveEntry = clsMPIncentiveEntry.GetData(strCode, NavTyep, Nothing, False)
        'If obj IsNot Nothing Then

        '    isNewEntry = False
        '    txtDocumentNo.Value = obj.Document_Code
        '    txtdate.Value = obj.Document_Date
        '    txtDBTReco.Value = obj.MCC_Code
        '    lblMCCDesc.Text = obj.MCC_Name
        '    txtFromDate.Value = obj.From_Date
        '    txtToDate.Value = obj.To_Date
        '    txtIncentiveRate.Value = obj.Incetive_Rate
        '    lblPending.Status = obj.Status
        '    If obj.FATSNFPer = 2 Then
        '        rbtnFATSNFNA.IsChecked = True
        '    ElseIf obj.FATSNFPer = 1 Then
        '        rbtnFATSNFPer.IsChecked = True
        '    Else
        '        rbtnFATSNFKG.IsChecked = True
        '    End If

        '    gvItem.DataSource = Nothing
        '    Dim Mcc_Uom As String = clsDBFuncationality.getSingleValue("select Uom_Code from TSPL_Mcc_UOM_DETAIL where Stocking_Unit='Y' and  MCC_CODE='" & txtDBTReco.Value & "'")

        '    gvItem.DataSource = clsDBFuncationality.GetDataTable(clsMPIncentiveEntryDetail.getQry(obj.Document_Code, Mcc_Uom, Nothing))
        '    FormatGrid()
        '    mtxtVLC.Enabled = False
        '    txtDBTReco.Enabled = False
        '    Dim arrVLCUploader As New ArrayList
        '    Dim arrVLCCode As New ArrayList
        '    For ii As Integer = 0 To gvItem.Rows.Count - 1
        '        If Not arrVLCCode.Contains(clsCommon.myCstr(gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colVLCCode).Value)) Then
        '            arrVLCCode.Add(clsCommon.myCstr(gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colVLCCode).Value))
        '            arrVLCUploader.Add(clsCommon.myCstr(gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colVLCUploaderCode).Value))
        '        End If
        '    Next
        '    mtxtVLC.arrValueMember = arrVLCUploader
        '    mtxtVLC.Tag = arrVLCCode

        '    'If obj.arr IsNot Nothing AndAlso obj.arr.Count > 0 Then
        '    '    For Each objTr As clsMPIncentiveEntryDetail In obj.arr
        '    '        gvItem.Rows.AddNew()
        '    '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colPKID).Value = objTr.PK_Id
        '    '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colSlNo).Value = gvItem.Rows.Count

        '    '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colVLCCode).Value = objTr.VLC_Code
        '    '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colVLCUploaderCode).Value = objTr.VLC_Uploader_Code
        '    '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colVLCName).Value = objTr.VLC_Name


        '    '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colMPCode).Value = objTr.MP_Code
        '    '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colMPUploaderCode).Value = objTr.MP_Uploader_Code
        '    '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colMPName).Value = objTr.MP_Name

        '    '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colMPBank).Value = objTr.MP_Bank
        '    '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colMPAccountNo).Value = objTr.MP_Account_No


        '    '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colQty).Value = objTr.Qty
        '    '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colUOM).Value = objTr.UOM
        '    '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colAmount).Value = objTr.Amount
        '    '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colAmountActual).Value = objTr.Amount_Actual


        '    '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colMPPhoneNo).Value = objTr.MP_Phone_No
        '    '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colMPAadharNo).Value = objTr.MP_Aadhar_No
        '    '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colMPIFSCCode).Value = objTr.MP_IFSC_No

        '    '        If obj.FATSNFPer Then
        '    '            gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colFAT).Value = objTr.FAT
        '    '            gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colSNF).Value = objTr.SNF
        '    '        Else
        '    '            gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colFAT).Value = objTr.FAT_Kg
        '    '            gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colSNF).Value = objTr.SNF_Kg
        '    '        End If


        '    '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colPashuAaharQty).Value = objTr.Pashu_Aahar_Qty
        '    '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colPashuAaharAmt).Value = objTr.Pashu_Aahar_Amount
        '    '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colMineralMixtureQty).Value = objTr.Mineral_Mixture_Qty
        '    '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colMineralMixtureAmt).Value = objTr.Mineral_Mixture_Amount
        '    '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colSailejQty).Value = objTr.Sailej_Qty
        '    '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colSailejAmt).Value = objTr.Sailej_Amount
        '    '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colRahatKampekatFeedQty).Value = objTr.Rahat_Kampekat_Feed_Qty
        '    '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colRahatKampekatFeedAmt).Value = objTr.Rahat_Kampekat_Feed_Amount
        '    '        gvItem.Rows(gvItem.Rows.Count - 1).Cells(clsMPIncetiveEntryColumns.colTotAmount).Value = objTr.Total_Amount

        '    '        If Not arrVLCCode.Contains(objTr.VLC_Code) Then
        '    '            arrVLCCode.Add(objTr.VLC_Code)
        '    '            arrVLCUploader.Add(objTr.VLC_Uploader_Code)
        '    '        End If
        '    '    Next
        '    '    mtxtVLC.arrValueMember = arrVLCUploader
        '    '    mtxtVLC.Tag = arrVLCCode
        '    'End If
        '    txtDocumentNo.MyReadOnly = True
        '    If obj.Status = ERPTransactionStatus.Approved Then
        '        btnsave.Enabled = False
        '        btndelete.Enabled = False
        '        btnPost.Enabled = False
        '    Else
        '        btnsave.Text = "Update"
        '        btnsave.Enabled = True
        '        btndelete.Enabled = True
        '        btnPost.Enabled = True
        '        btnAddMissing.Enabled = True
        '    End If
        '    DisableInputDataField()
        '    mtxtVLC.Focus()
        'End If
        IsinsideLoadData = False
    End Sub

    Private Sub FormatGrid()
        Try

            gvItem.Columns("VLC_Code").HeaderText = "DCS Code"
            gvItem.Columns("VLC_Code").ReadOnly = True
            gvItem.Columns("VLC_Code").IsVisible = False
            gvItem.Columns("VLC_Code").TextAlignment = System.Drawing.ContentAlignment.MiddleLeft


            gvItem.Columns("Vlc_Code_VLC_Uploader").FormatString = "DCS"
            gvItem.Columns("Vlc_Code_VLC_Uploader").ReadOnly = True
            gvItem.Columns("Vlc_Code_VLC_Uploader").IsVisible = True
            gvItem.Columns("Vlc_Code_VLC_Uploader").Width = 100
            gvItem.Columns("Vlc_Code_VLC_Uploader").TextAlignment = System.Drawing.ContentAlignment.MiddleLeft

            gvItem.Columns("VLC_Name").FormatString = "DCS Name"
            gvItem.Columns("VLC_Name").ReadOnly = True
            gvItem.Columns("VLC_Name").IsVisible = False
            gvItem.Columns("VLC_Name").TextAlignment = System.Drawing.ContentAlignment.MiddleLeft

            gvItem.Columns("MP_Code").FormatString = "Farmer Code"
            gvItem.Columns("MP_Code").ReadOnly = True
            gvItem.Columns("MP_Code").IsVisible = False
            gvItem.Columns("MP_Code").TextAlignment = System.Drawing.ContentAlignment.MiddleLeft

            gvItem.Columns("MP_Uploader_Code").FormatString = "Farmer"
            gvItem.Columns("MP_Uploader_Code").Width = 100
            gvItem.Columns("MP_Uploader_Code").ReadOnly = True
            gvItem.Columns("MP_Uploader_Code").TextAlignment = System.Drawing.ContentAlignment.MiddleLeft

            gvItem.Columns("MP_Name").FormatString = "Farmer Name"
            gvItem.Columns("MP_Name").Width = 200
            gvItem.Columns("MP_Name").ReadOnly = True
            gvItem.Columns("MP_Name").TextAlignment = System.Drawing.ContentAlignment.MiddleLeft

            gvItem.Columns("Qty").FormatString = "Quantity"
            gvItem.Columns("Qty").Width = 120
            gvItem.Columns("Qty").ReadOnly = False
            gvItem.Columns("Qty").TextAlignment = System.Drawing.ContentAlignment.MiddleRight

            gvItem.MasterTemplate.SummaryRowsBottom.Clear()
            Dim summaryRowItem As New GridViewSummaryRowItem()
            summaryRowItem.Add(New GridViewSummaryItem("Qty", "{0:n2}", GridAggregateFunction.Sum))
            gvItem.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            gvItem.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
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
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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

    Sub DisableInputDataField()
        txtdate.Enabled = False
        txtDBTReco.Enabled = False
        txtFromDate.Enabled = False
        txtToDate.Enabled = False
    End Sub
    Sub EnableInputDataField()
        txtdate.Enabled = True
        txtDBTReco.Enabled = True
        txtFromDate.Enabled = True
        txtToDate.Enabled = True
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
    Private Sub txtMCC__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDBTReco._MYValidating
        txtDBTReco.Value = clsMPDCSInsentiveReco.getFinder("not exists(select 1 from TSPL_DBT_MONTHLY_FARMER_MILK where Document_Code not in ('" + txtDocumentNo.Value + "'))", txtDBTReco.Value, isButtonClicked)
        If clsCommon.myLen(txtDBTReco.Value) > 0 Then
            SetRecoDate()
        End If
        loadBlankGrid()
    End Sub

    Private Sub SetRecoDate()
        Dim dt As DataTable = clsDBFuncationality.getSingleValue("select Reco_Date,Reco_Date_To from TSPL_DCS_MP_INCENTIVE_RECO_HEAD where Document_Code='" + txtDBTReco.Value + "' ")
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            txtFromDate.Value = clsCommon.myCDate(dt.Rows(0)("Reco_Date"))
            txtFromDate.Value = clsCommon.myCDate(dt.Rows(0)("Reco_Date"))
        End If
    End Sub

    Sub fillMPS(ByVal arrVLC As ArrayList)
        If clsCommon.myLen(txtDBTReco.Value) <= 0 Then
            txtDBTReco.Focus()
            Throw New Exception("Please select " + txtDBTReco.MyLinkLable1.Text)
        End If
        loadBlankGrid()
        Dim Mcc_Uom As String = clsDBFuncationality.getSingleValue("select Uom_Code from TSPL_Mcc_UOM_DETAIL where Stocking_Unit='Y' and  MCC_CODE='" & txtDBTReco.Value & "'")
        Dim Qry As String = "Select VLC_Code,max(Vlc_Code_VLC_Uploader) As Vlc_Code_VLC_Uploader,max(VLC_Name) As VLC_Name,MP_Code,max(MP_Uploader_Code) As MP_Uploader_Code,max(MP_Name) As MP_Name,sum(qty) As Qty from (  
 select Doc_No,Convert(Varchar(10),Doc_Date,103)Doc_Date,shift,TSPL_VLC_MASTER_HEAD.VLC_Code, TSPL_VLC_MASTER_HEAD.Vlc_Code_VLC_Uploader,TSPL_VLC_MASTER_HEAD.VLC_Name,
TSPl_MP_MAster.MP_CODE,TSPl_MP_MAster.MP_Name, TSPl_MP_MAster.MP_Code_VLC_Uploader As MP_Uploader_Code,TSPL_VLC_DATA_UPLOADER.qty 
from TSPL_VLC_DATA_UPLOADER
Left Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.Vlc_Code_VLC_Uploader=TSPL_VLC_DATA_UPLOADER.VLC_CODE
Left Join TSPl_MP_MAster On TSPl_MP_MAster.MP_Code_VLC_Uploader=TSPL_VLC_DATA_UPLOADER.MP_CODE and TSPl_MP_MAster.VLC_Code=TSPL_VLC_MASTER_HEAD.VLC_Code
 inner join TSPL_DCS_MP_INCENTIVE_RECO_DETAIL on TSPL_DCS_MP_INCENTIVE_RECO_DETAIL.VLC_Code=TSPL_VLC_MASTER_HEAD.VLC_Code
where TSPL_VLC_DATA_UPLOADER.Doc_Date>='" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "' And TSPL_VLC_DATA_UPLOADER.Doc_Date<='" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "'  and TSPL_DCS_MP_INCENTIVE_RECO_DETAIL.Document_Code='" & txtDBTReco.Value & "'
 Union All 
 select  TSPL_VLC_DATA_UPLOADER_MASTER.Document_Code As Doc_No,
Convert(Varchar(10),TSPL_VLC_DATA_UPLOADER_MASTER.Document_Date,103) As Doc_Date, TSPL_VLC_DATA_UPLOADER_MASTER.Shift,TSPL_VLC_DATA_UPLOADER_MASTER.VLC_CODE,
TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPl_MP_MAster.MP_CODE,TSPl_MP_MAster.MP_Name,
TSPl_MP_MAster.MP_Code_VLC_Uploader As MP_Uploader_Code,TSPL_VLC_DATA_UPLOADER_DETAIL.qty 
from TSPL_VLC_DATA_UPLOADER_DETAIL
Left Outer Join TSPL_VLC_DATA_UPLOADER_MASTER On TSPL_VLC_DATA_UPLOADER_MASTER.Document_Code=TSPL_VLC_DATA_UPLOADER_DETAIL.Document_Code
Left Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.Vlc_Code_VLC_Uploader=TSPL_VLC_DATA_UPLOADER_MASTER.VLC_CODE
Left Join TSPl_MP_MAster On TSPl_MP_MAster.MP_Code_VLC_Uploader=TSPL_VLC_DATA_UPLOADER_DETAIL.Farmer_Code and TSPl_MP_MAster.VLC_Code=TSPL_VLC_MASTER_HEAD.VLC_Code
 inner join TSPL_DCS_MP_INCENTIVE_RECO_DETAIL on TSPL_DCS_MP_INCENTIVE_RECO_DETAIL.VLC_Code=TSPL_VLC_DATA_UPLOADER_MASTER.VLC_CODE
 where  convert(date,TSPL_VLC_DATA_UPLOADER_MASTER.Document_Date,103) >='" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "' And convert(date,TSPL_VLC_DATA_UPLOADER_MASTER.Document_Date,103)<= '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "'  and TSPL_DCS_MP_INCENTIVE_RECO_DETAIL.Document_Code='" & txtDBTReco.Value & "' 
) xx where MP_CODE is not null group by VLC_Code,MP_CODE  order by Vlc_Code_VLC_Uploader,MP_Uploader_Code"
        Dim dt As DataTable = Nothing
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            gvItem.DataSource = dt
            FormatGrid()
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
        'gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colAmountActual).Value = (clsCommon.myCdbl(gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colQty).Value) * txtIncentiveRate.Value)
        'gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colAmount).Value = (clsCommon.myCdbl(gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colQty).Value) * txtIncentiveRate.Value)

        ''If Not SettApplyPashuAaharAndMineralMixture Then
        ''    gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colAmount).Value = Math.Ceiling(clsCommon.myCdbl(gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colQty).Value) * txtIncentiveRate.Value)
        ''End If
        'gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colPashuAaharAmt).Value = Math.Ceiling(clsCommon.myCdbl(gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colPashuAaharQty).Value) * txtPashuAahar.Value)
        'gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colMineralMixtureAmt).Value = Math.Ceiling(clsCommon.myCdbl(gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colMineralMixtureQty).Value) * txtMineralMixture.Value)
        'gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colSailejAmt).Value = Math.Ceiling(clsCommon.myCdbl(gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colSailejQty).Value) * txtSailejRate.Value)
        'gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colRahatKampekatFeedAmt).Value = Math.Ceiling(clsCommon.myCdbl(gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colRahatKampekatFeedQty).Value) * txtRahatKampekatFeedRate.Value)
        'gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colTotAmount).Value = clsCommon.myCDecimal(gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colAmount).Value) + clsCommon.myCDecimal(gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colPashuAaharAmt).Value) + clsCommon.myCDecimal(gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colMineralMixtureAmt).Value) + clsCommon.myCDecimal(gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colSailejAmt).Value) + clsCommon.myCDecimal(gvItem.Rows(ii).Cells(clsMPIncetiveEntryColumns.colRahatKampekatFeedAmt).Value)
    End Sub
    Private Sub txtIncentiveRate_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs)
        UpdateAllRow()
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
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub


    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)

        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
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






End Class

