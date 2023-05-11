Imports common
Imports System.Data.SqlClient



Public Class FrmPhysicalStock
    Inherits FrmMainTranScreen

#Region "Variables"
    Const colSNo As String = "colSNo"
    Const colICode As String = "colICode"
    Const colIName As String = "colIName"
    Const colIsBatchItem As String = "colIsBatchItem"
    Const colGLAccountInvControl As String = "colGLAccountInvControl"
    Const colGLAccountNameInvControl As String = "colGLAccountNameInvControl"
    Const colGLAccount As String = "colGLAccount"
    Const colGLAccountName As String = "colGLAccountName"
    Const colStockUnit As String = "colStockUnit"

    Const colCurrQty As String = "colCurrQty"
    Const colCurrFATPers As String = "colCurrFATPers"
    Const colCurrFATKG As String = "colCurrFATKG"
    Const colCurrFATAmount As String = "colCurrFATAmount"
    Const colCurrSNFPers As String = "colCurrSNFPers"
    Const colCurrSNFKG As String = "colCurrSNFKG"
    Const colCurrSNFAmt As String = "colCurrSNFAmt"
    Const colCurrAmt As String = "colCurrAmt"

    Const colPhyNillBalance As String = "colPhyNillBalance"
    Const colPhyQty As String = "colPhyQty"
    Const colPhyFATPers As String = "colPhyFATPers"
    Const colPhyFATKG As String = "colPhyFATKG"
    Const colPhyFATAmt As String = "colPhyFATAmt"
    Const colPhySNFPers As String = "colPhySNFPers"
    Const colPhySNFKG As String = "colPhySNFKG"
    Const colPhySNFAmt As String = "colPhySNFAmt"
    Const colPhyAmt As String = "colPhyAmt"

    Const colDiffQty As String = "colDiffQty"
    Const colDiffFATPer As String = "colDiffFATPer"
    Const colDiffFATKg As String = "colDiffFATKg"
    Const colDiffFATAmt As String = "colDiffFATAmt"
    Const colDiffSNFPer As String = "colDiffSNFPer"
    Const colDiffSNFKg As String = "colDiffSNFKg"
    Const colDiffSNFAmt As String = "colDiffSNFAmt"
    Const colDiffAmt As String = "colDiffAmt"

    Const colIsSerialseItem As String = "Isserialized"
    Const colIsPickAutoSrNo As String = "PickAutoSerialNo"



    Private isNewEntry As Boolean = True
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isformLoad As Boolean = False
    Private isCellvaluechanged As Boolean = False
    Dim Errorcontrol As New clsErrorControl()
    Dim isGoClick As Boolean = False
    Dim RunBatchFifowise As Boolean = False
    Dim CheckStockOfItemTillTransactionDateOnly As Boolean = False
#End Region

    Private Sub FrmPhysicalStock_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.Alt AndAlso e.KeyCode = Keys.N Then
                reset()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
                btnsave.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
                btnpost.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
                btnDelete.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
                Me.Close()
            ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SIRC"
                frm.strCode = "SIReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnReverse.Visible = True
                End If
            End If

            If e.KeyData = Keys.F2 AndAlso gv1.CurrentColumn IsNot Nothing AndAlso gv1.Columns Is gv1.Columns(colPhyQty) Then
                isCellvaluechanged = True
                If Not clsCommon.myCBool(gv1.CurrentRow.Cells(colIsPickAutoSrNo).Value) AndAlso clsCommon.myCdbl(gv1.CurrentRow.Cells(colPhyQty).Value) <> 0 Then
                    OpenSerialItem()
                End If
                isCellvaluechanged = False
            End If
        Catch ex As Exception
            isCellvaluechanged = False
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub FrmPhysicalStock_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim qry As String = "alter table TSPL_PHYSICAL_STOCK alter column Physical_Qty decimal(18,3) not null"
        clsDBFuncationality.ExecuteNonQuery(qry)

        qry = "alter table TSPL_PHYSICAL_STOCK alter column Existing_Qty decimal(18,3) not null"
        clsDBFuncationality.ExecuteNonQuery(qry)

        qry = "alter table TSPL_PHYSICAL_STOCK alter column  Existing_FAT_Amt float"
        clsDBFuncationality.ExecuteNonQuery(qry)

        qry = "alter table TSPL_PHYSICAL_STOCK alter column  Existing_SNF_Amt float "
        clsDBFuncationality.ExecuteNonQuery(qry)

        qry = "alter table TSPL_PHYSICAL_STOCK alter column  Existing_Amount float "
        clsDBFuncationality.ExecuteNonQuery(qry)

        qry = "alter table TSPL_PHYSICAL_STOCK alter column  FAT_Amt float "
        clsDBFuncationality.ExecuteNonQuery(qry)

        qry = "alter table TSPL_PHYSICAL_STOCK alter column  SNF_Amt float "
        clsDBFuncationality.ExecuteNonQuery(qry)

        SetUserMgmtNew()
        RunBatchFifowise = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RunBatchFifowise, clsFixedParameterCode.RunBatchFifowise, Nothing)) = 1)
        CheckStockOfItemTillTransactionDateOnly = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.checkStockOfItemTillTransactionDateOnly, clsFixedParameterCode.checkStockOfItemTillTransactionDateOnly, Nothing)) = 1)
        Panel2.Enabled = CheckStockOfItemTillTransactionDateOnly
        reset()
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Reset the Window")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S to Save")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D to Delete")
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), "", "", NavigatorType.Current)
        End If
        'dtpdate.Value = clsCommon.GETSERVERDATE(Nothing)
    End Sub

    Sub reset()
        UsLock1.Status = ERPTransactionStatus.Pending
        LoadBlankGrid()
        txtCode.Value = ""
        txtdesc.Text = ""
        If isGoClick = False Then
            dtpdate.Value = clsCommon.GETSERVERDATE(Nothing)
        End If
        chkMilk.Checked = False
        CheckUnCheckMilkType()
        txtLocation.Value = ""
        txtLocName.Text = ""
        txtsubLoc.Value = ""
        txtsubLocName.Text = ""
        txtsubLoc.Enabled = False
        txtCode.MyReadOnly = False
        isNewEntry = True
        txtdesc.Focus()
        txtdesc.Select()
        isImport = False
        txtManualBatchNo.Text = ""
        txtSelfLifeDays.Text = ""
        txtManualBatchNo.Enabled = True
        txtSelfLifeDays.Enabled = True
        btnsave.Enabled = True
        btnDelete.Enabled = True
        btnpost.Enabled = True
        txtInventoryAccount.arrValueMember = Nothing
        txtItemType.arrValueMember = Nothing
    End Sub

    Private Sub SetUserMgmtNew()
        '--preeti gupta--ticket no-[BM00000003176]
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmPhysicalStock)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnpost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub LoadBlankGrid()
        gv1.Tag = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoLineno As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLineno.FormatString = ""
        repoLineno.HeaderText = "SNo"
        repoLineno.Name = colSNo
        repoLineno.ReadOnly = True
        repoLineno.Width = 50
        gv1.MasterTemplate.Columns.Add(repoLineno)

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colICode
        repoICode.ReadOnly = True
        repoICode.Width = 100
        gv1.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Description"
        repoIName.Name = colIName
        repoIName.Width = 150
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)

        Dim repoCheckBox As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoCheckBox.HeaderText = "Is Batch Item"
        repoCheckBox.Name = colIsBatchItem
        repoCheckBox.ReadOnly = True
        repoCheckBox.IsVisible = False
        repoCheckBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoCheckBox)

        repoICode = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Inventory Account Code"
        repoICode.Name = colGLAccountInvControl
        repoICode.ReadOnly = True
        repoICode.Width = 100
        gv1.MasterTemplate.Columns.Add(repoICode)

        repoIName = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Inventory Account"
        repoIName.Name = colGLAccountNameInvControl
        repoIName.Width = 150
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)

        repoICode = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "GL Account Code"
        repoICode.Name = colGLAccount
        repoICode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICode.ReadOnly = False
        repoICode.Width = 100
        gv1.MasterTemplate.Columns.Add(repoICode)

        repoIName = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "GL Account"
        repoIName.Name = colGLAccountName
        repoIName.Width = 150
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)

        Dim repostkunit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repostkunit.FormatString = ""
        repostkunit.HeaderText = "Stock Unit"
        repostkunit.Name = colStockUnit
        repostkunit.Width = 80
        repostkunit.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repostkunit)

        '----Current
        Dim repoCurrent As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCurrent.FormatString = "{0:n3}"
        repoCurrent.HeaderText = "Quantity"
        repoCurrent.Name = colCurrQty
        repoCurrent.ShowUpDownButtons = False
        repoCurrent.Width = 100
        repoCurrent.ReadOnly = True
        repoCurrent.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoCurrent)

        Dim repoCurrentFATPers As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCurrentFATPers.FormatString = "{0:n2}"
        repoCurrentFATPers.HeaderText = "FAT%"
        repoCurrentFATPers.WrapText = True
        repoCurrentFATPers.Name = colCurrFATPers
        repoCurrentFATPers.ShowUpDownButtons = False
        repoCurrentFATPers.Width = 50
        repoCurrentFATPers.ReadOnly = True
        repoCurrentFATPers.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoCurrentFATPers)

        Dim repoCurrentFATKG As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCurrentFATKG.FormatString = "{0:n3}"
        repoCurrentFATKG.HeaderText = "FAT Kg"
        repoCurrentFATKG.Name = colCurrFATKG
        repoCurrentFATKG.WrapText = True
        repoCurrentFATKG.ShowUpDownButtons = False
        repoCurrentFATKG.Width = 60
        repoCurrentFATKG.ReadOnly = True
        repoCurrentFATKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoCurrentFATKG)

        Dim repoCurrentSNFPers As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCurrentSNFPers.FormatString = "{0:n2}"
        repoCurrentSNFPers.HeaderText = "Fat Amt"
        repoCurrentSNFPers.WrapText = True
        repoCurrentSNFPers.Name = colCurrFATAmount
        repoCurrentSNFPers.ShowUpDownButtons = False
        repoCurrentSNFPers.Width = 60
        repoCurrentSNFPers.ReadOnly = True
        repoCurrentSNFPers.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoCurrentSNFPers)

        repoCurrentSNFPers = New GridViewDecimalColumn()
        repoCurrentSNFPers.FormatString = "{0:n2}"
        repoCurrentSNFPers.HeaderText = "SNF%"
        repoCurrentSNFPers.WrapText = True
        repoCurrentSNFPers.Name = colCurrSNFPers
        repoCurrentSNFPers.ShowUpDownButtons = False
        repoCurrentSNFPers.Width = 50
        repoCurrentSNFPers.ReadOnly = True
        repoCurrentSNFPers.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoCurrentSNFPers)

        Dim repoCurrentSNFKG As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCurrentSNFKG.FormatString = "{0:n3}"
        repoCurrentSNFKG.HeaderText = "SNF Kg"
        repoCurrentSNFKG.WrapText = True
        repoCurrentSNFKG.Name = colCurrSNFKG
        repoCurrentSNFKG.ShowUpDownButtons = False
        repoCurrentSNFKG.Width = 60
        repoCurrentSNFKG.ReadOnly = True
        repoCurrentSNFKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoCurrentSNFKG)

        repoCurrentSNFKG = New GridViewDecimalColumn()
        repoCurrentSNFKG.FormatString = "{0:n2}"
        repoCurrentSNFKG.HeaderText = "SNF Amt"
        repoCurrentSNFKG.WrapText = True
        repoCurrentSNFKG.Name = colCurrSNFAmt
        repoCurrentSNFKG.ShowUpDownButtons = False
        repoCurrentSNFKG.Width = 60
        repoCurrentSNFKG.ReadOnly = True
        repoCurrentSNFKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoCurrentSNFKG)

        repoCurrent = New GridViewDecimalColumn()
        repoCurrent.FormatString = "{0:n2}"
        repoCurrent.HeaderText = "Amount"
        repoCurrent.Name = colCurrAmt
        repoCurrent.ShowUpDownButtons = False
        repoCurrent.Width = 100
        repoCurrent.ReadOnly = True
        repoCurrent.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoCurrent)
        '----End of Current

        '----Physical

        repoCheckBox = New GridViewCheckBoxColumn()
        repoCheckBox.HeaderText = "Nill Balance"
        repoCheckBox.Name = colPhyNillBalance
        repoCheckBox.ReadOnly = False
        repoCheckBox.IsVisible = True
        repoCheckBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoCheckBox)

        Dim repoPhysicalStock As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoPhysicalStock.FormatString = "{0:n3}"
        repoPhysicalStock.HeaderText = "Quantity"
        repoPhysicalStock.Name = colPhyQty
        repoPhysicalStock.ShowUpDownButtons = False
        repoPhysicalStock.Width = 100
        repoPhysicalStock.ReadOnly = False
        repoPhysicalStock.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoPhysicalStock)

        Dim repoFATPers As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFATPers.FormatString = "{0:n2}"
        repoFATPers.HeaderText = "FAT%"
        repoFATPers.Name = colPhyFATPers
        repoFATPers.ShowUpDownButtons = False
        repoFATPers.Width = 50
        repoFATPers.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoFATPers)

        Dim repoFATKG As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFATKG.FormatString = "{0:n3}"
        repoFATKG.HeaderText = "FAT Kg"
        repoFATKG.Name = colPhyFATKG
        repoFATKG.ShowUpDownButtons = False
        repoFATKG.Width = 60
        repoFATKG.ReadOnly = True
        repoFATKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoFATKG)

        repoFATKG = New GridViewDecimalColumn()
        repoFATKG.FormatString = "{0:n2}"
        repoFATKG.HeaderText = "Fat Amt"
        repoFATKG.Name = colPhyFATAmt
        repoFATKG.ShowUpDownButtons = False
        repoFATKG.Width = 60
        repoFATKG.ReadOnly = False
        repoFATKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoFATKG)

        Dim repoSNFPers As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSNFPers.FormatString = "{0:n2}"
        repoSNFPers.HeaderText = "SNF%"
        repoSNFPers.Name = colPhySNFPers
        repoSNFPers.ShowUpDownButtons = False
        repoSNFPers.Width = 50
        repoSNFPers.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoSNFPers)

        Dim repoSNFKG As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSNFKG.FormatString = "{0:n3}"
        repoSNFKG.HeaderText = "SNF Kg"
        repoSNFKG.Name = colPhySNFKG
        repoSNFKG.ShowUpDownButtons = False
        repoSNFKG.Width = 60
        repoSNFKG.ReadOnly = True
        repoSNFKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoSNFKG)

        repoSNFKG = New GridViewDecimalColumn()
        repoSNFKG.FormatString = "{0:n2}"
        repoSNFKG.HeaderText = "SNF Amt"
        repoSNFKG.Name = colPhySNFAmt
        repoSNFKG.ShowUpDownButtons = False
        repoSNFKG.Width = 60
        repoSNFKG.ReadOnly = False
        repoSNFKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoSNFKG)

        repoPhysicalStock = New GridViewDecimalColumn()
        repoPhysicalStock.FormatString = "{0:n2}"
        repoPhysicalStock.HeaderText = "Amount"
        repoPhysicalStock.Name = colPhyAmt
        repoPhysicalStock.ShowUpDownButtons = False
        repoPhysicalStock.Width = 100
        repoPhysicalStock.ReadOnly = False
        repoPhysicalStock.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoPhysicalStock)
        '----End of Physical

        '----Difference
        Dim repopfreight As GridViewDecimalColumn = New GridViewDecimalColumn()
        repopfreight.FormatString = "{0:n3}"
        repopfreight.HeaderText = "Qty"
        repopfreight.Name = colDiffQty
        repopfreight.ShowUpDownButtons = False
        repopfreight.Width = 100
        repopfreight.ReadOnly = True
        repopfreight.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repopfreight)

        Dim repopFatPerDiff As GridViewDecimalColumn = New GridViewDecimalColumn()
        repopFatPerDiff.FormatString = "{0:n2}"
        repopFatPerDiff.HeaderText = "Fat %"
        repopFatPerDiff.Name = colDiffFATPer
        repopFatPerDiff.ShowUpDownButtons = False
        repopFatPerDiff.Width = 100
        repopFatPerDiff.ReadOnly = True
        repopFatPerDiff.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repopFatPerDiff)

        Dim repopFatkgDiff As GridViewDecimalColumn = New GridViewDecimalColumn()
        repopFatkgDiff.FormatString = "{0:n3}"
        repopFatkgDiff.HeaderText = "Fat Kg"
        repopFatkgDiff.Name = colDiffFATKg
        repopFatkgDiff.ShowUpDownButtons = False
        repopFatkgDiff.Width = 100
        repopFatkgDiff.ReadOnly = True
        repopFatkgDiff.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repopFatkgDiff)

        repopFatkgDiff = New GridViewDecimalColumn()
        repopFatkgDiff.FormatString = "{0:n2}"
        repopFatkgDiff.HeaderText = "Fat Amt"
        repopFatkgDiff.Name = colDiffFATAmt
        repopFatkgDiff.ShowUpDownButtons = False
        repopFatkgDiff.Width = 100
        repopFatkgDiff.ReadOnly = True
        repopFatkgDiff.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repopFatkgDiff)

        Dim repopSnfPerDiff As GridViewDecimalColumn = New GridViewDecimalColumn()
        repopSnfPerDiff.FormatString = "{0:n2}"
        repopSnfPerDiff.HeaderText = "SNF %"
        repopSnfPerDiff.Name = colDiffSNFPer
        repopSnfPerDiff.ShowUpDownButtons = False
        repopSnfPerDiff.Width = 100
        repopSnfPerDiff.ReadOnly = True
        repopSnfPerDiff.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repopSnfPerDiff)

        Dim repopSnfkgDiff As GridViewDecimalColumn = New GridViewDecimalColumn()
        repopSnfkgDiff.FormatString = "{0:n3}"
        repopSnfkgDiff.HeaderText = "SNF Kg"
        repopSnfkgDiff.Name = colDiffSNFKg
        repopSnfkgDiff.ShowUpDownButtons = False
        repopSnfkgDiff.Width = 100
        repopSnfkgDiff.ReadOnly = True
        repopSnfkgDiff.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repopSnfkgDiff)

        repopSnfkgDiff = New GridViewDecimalColumn()
        repopSnfkgDiff.FormatString = "{0:n2}"
        repopSnfkgDiff.HeaderText = "SNF Amt"
        repopSnfkgDiff.Name = colDiffSNFAmt
        repopSnfkgDiff.ShowUpDownButtons = False
        repopSnfkgDiff.Width = 100
        repopSnfkgDiff.ReadOnly = True
        repopSnfkgDiff.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repopSnfkgDiff)

        repopfreight = New GridViewDecimalColumn()
        repopfreight.FormatString = "{0:n2}"
        repopfreight.HeaderText = "Amount"
        repopfreight.Name = colDiffAmt
        repopfreight.ShowUpDownButtons = False
        repopfreight.Width = 100
        repopfreight.ReadOnly = True
        repopfreight.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repopfreight)
        '----End of Difference

        Dim repoIsSerItem As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSerItem.HeaderText = "Is Serialize Item"
        repoIsSerItem.Name = colIsSerialseItem
        repoIsSerItem.ReadOnly = True
        repoIsSerItem.IsVisible = False
        repoIsSerItem.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSerItem)

        Dim repoIsPickAutoSerNo As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsPickAutoSerNo.HeaderText = "Is Pick Auto Serial"
        repoIsPickAutoSerNo.Name = colIsPickAutoSrNo
        repoIsPickAutoSerNo.ReadOnly = True
        repoIsPickAutoSerNo.IsVisible = False
        repoIsPickAutoSerNo.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsPickAutoSerNo)


        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.FilterDescriptors.Clear()
        gv1.EnableFiltering = True
        SetGridGroup()

        For Each column As GridViewColumn In gv1.Columns
            If TypeOf column Is GridViewDecimalColumn Then
                Dim filterUnitsInStock As New FilterDescriptor()
                filterUnitsInStock.PropertyName = column.Name
                filterUnitsInStock.[Operator] = FilterOperator.None
                filterUnitsInStock.IsFilterEditor = True
                gv1.FilterDescriptors.Add(filterUnitsInStock)
            End If
        Next
    End Sub

    Sub SetGridGroup()
        If gv1.Columns.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()

            view.ColumnGroups.Add(New GridViewColumnGroup(""))
            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colSNo).Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colICode).Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colIName).Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colGLAccountInvControl).Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colGLAccountNameInvControl).Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colGLAccount).Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colGLAccountName).Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colStockUnit).Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colIsSerialseItem).Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colIsPickAutoSrNo).Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colIsBatchItem).Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Current"))
            view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns(colCurrQty).Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns(colCurrFATPers).Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns(colCurrFATKG).Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns(colCurrFATAmount).Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns(colCurrSNFPers).Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns(colCurrSNFKG).Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns(colCurrSNFAmt).Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns(colCurrAmt).Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Physical"))
            view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns(colPhyNillBalance).Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns(colPhyQty).Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns(colPhyFATPers).Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns(colPhyFATKG).Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns(colPhyFATAmt).Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns(colPhySNFPers).Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns(colPhySNFKG).Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns(colPhySNFAmt).Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns(colPhyAmt).Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Difference"))
            view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv1.Columns(colDiffQty).Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv1.Columns(colDiffFATPer).Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv1.Columns(colDiffFATKg).Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv1.Columns(colDiffFATAmt).Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv1.Columns(colDiffSNFPer).Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv1.Columns(colDiffSNFKg).Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv1.Columns(colDiffSNFAmt).Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv1.Columns(colDiffAmt).Name)

            gv1.ViewDefinition = view
        End If

    End Sub

    Private Sub setGridFocus()
        'If gv1.RowCount > 0 Then
        '    Dim intCurrRow As Integer = gv1.CurrentRow.Index
        '    If intCurrRow = gv1.Rows.Count - 1 Then
        '        gv1.Rows.AddNew()
        '        gv1.CurrentRow = gv1.Rows(intCurrRow)
        '    End If
        'End If
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Public Function AllowToSave(Optional ByVal isposted As Boolean = False) As Boolean
        Try
            If AllowFutureDateTransaction(dtpdate.Value, Nothing) = False Then
                dtpdate.Select()
                Return False
            End If
            If clsCommon.myLen(txtLocation.Value) <= 0 Then
                txtLocation.Focus()
                txtLocation.Select()
                Errorcontrol.SetError(txtLocName, "select location.")
                Throw New Exception("Select Location")
            Else
                Errorcontrol.ResetError(txtLocName)
            End If

            If chkMilk.Checked AndAlso clsCommon.myLen(txtsubLoc.Value) <= 0 AndAlso clsCommon.myLen(txtLocation.Value) <= 0 Then
                txtsubLoc.Focus()
                txtsubLoc.Select()
                Errorcontrol.SetError(txtsubLocName, "select location/Sub-location.")
                Throw New Exception("Select Location/Sub-Location")
            Else
                Errorcontrol.ResetError(txtsubLocName)
            End If

            If isposted Then
                For ii As Integer = 0 To gv1.Rows.Count - 1
                    If clsCommon.myCdbl(gv1.Rows(ii).Cells(colPhyQty).Value) <> 0 OrElse clsCommon.myCBool(gv1.Rows(ii).Cells(colPhyNillBalance).Value) Then
                        gv1.CurrentRow = gv1.Rows(ii)
                        UpdateCurrentRow()
                        If clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0 Then
                            If clsCommon.myLen(gv1.Rows(ii).Cells(colGLAccountInvControl).Value) <= 0 Then
                                Throw New Exception("Inventory control account not found for item " + clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value))
                            End If
                            If clsCommon.myLen(gv1.Rows(ii).Cells(colGLAccount).Value) <= 0 Then
                                Throw New Exception("GL account not found for item " + clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value))
                            End If

                            If clsCommon.myCBool(gv1.Rows(ii).Cells(colIsSerialseItem).Value) Then
                                Dim arrSerailNo As List(Of clsSerializeInvenotry) = TryCast(gv1.Rows(ii).Tag, List(Of clsSerializeInvenotry))
                                If clsCommon.myCdbl(gv1.Rows(ii).Cells(colPhyQty).Value) > 0 Then 'in case
                                    If clsCommon.myCBool(gv1.Rows(ii).Cells(colIsPickAutoSrNo).Value) Then
                                        Dim arrOut As List(Of clsSerializeInvenotry) = New List(Of clsSerializeInvenotry)
                                        If arrSerailNo Is Nothing OrElse arrSerailNo.Count <= 0 Then
                                            For kk As Integer = 1 To clsCommon.myCdbl(gv1.Rows(ii).Cells(colPhyQty).Value)
                                                Dim obj As clsSerializeInvenotry = New clsSerializeInvenotry()
                                                obj.Auto_Sr_No = clsItemMaster.GetItemSerialCounter(clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value), Nothing)
                                                arrOut.Add(obj)
                                            Next
                                        Else
                                            For kk As Integer = 0 To arrSerailNo.Count - 1
                                                If kk > clsCommon.myCdbl(gv1.Rows(ii).Cells(colPhyQty).Value) - 1 Then
                                                    Exit For
                                                Else
                                                    Dim obj As clsSerializeInvenotry = New clsSerializeInvenotry()
                                                    If clsCommon.myLen(arrSerailNo(kk).Auto_Sr_No) > 0 Then
                                                        obj.Auto_Sr_No = arrSerailNo(kk).Auto_Sr_No
                                                    Else
                                                        obj.Auto_Sr_No = clsItemMaster.GetItemSerialCounter(clsCommon.myCdbl(gv1.Rows(ii).Cells(colPhyQty).Value), Nothing)
                                                    End If
                                                    arrOut.Add(obj)
                                                End If
                                            Next
                                            If arrOut.Count < clsCommon.myCdbl(gv1.Rows(ii).Cells(colPhyQty).Value) Then
                                                For kk As Integer = arrOut.Count + 1 To clsCommon.myCdbl(gv1.Rows(ii).Cells(colPhyQty).Value)
                                                    Dim obj As clsSerializeInvenotry = New clsSerializeInvenotry()
                                                    obj.Auto_Sr_No = clsItemMaster.GetItemSerialCounter(clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value), Nothing)
                                                    arrOut.Add(obj)
                                                Next
                                            End If
                                        End If
                                        gv1.Rows(ii).Tag = arrOut
                                    End If
                                End If
                                arrSerailNo = TryCast(gv1.Rows(ii).Tag, List(Of clsSerializeInvenotry))
                                Dim dblSerialqty As Decimal = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPhyQty).Value)
                                If dblSerialqty < 0 Then
                                    dblSerialqty = clsCommon.myCdbl(clsCommon.myCstr(dblSerialqty).Substring(1, clsCommon.myLen(dblSerialqty) - 1))
                                End If
                                If arrSerailNo Is Nothing OrElse dblSerialqty <> arrSerailNo.Count Then
                                    Throw New Exception("Please provide serial no for item : " + clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value) + " . At Line No" + clsCommon.myCstr(ii + 1))
                                End If
                            End If
                        End If
                    End If
                Next
            Else
                For ii As Integer = 0 To gv1.Rows.Count - 1
                    If clsCommon.myCdbl(gv1.Rows(ii).Cells(colPhyQty).Value) <> 0 OrElse (clsCommon.myCBool(gv1.Rows(ii).Cells(colPhyNillBalance).Value)) Then ' AndAlso CheckStockOfItemTillTransactionDateOnly = False
                        gv1.CurrentRow = gv1.Rows(ii)
                        UpdateCurrentRow()
                        OpenBatchItem()
                        If clsCommon.myCBool(gv1.Rows(ii).Cells(colIsBatchItem).Value) Then
                            Dim arrBatchNo As List(Of clsBatchInventory) = TryCast(gv1.Rows(ii).Cells(colICode).Tag, List(Of clsBatchInventory))
                            If arrBatchNo Is Nothing Then
                                If clsCommon.myCBool(gv1.Rows(ii).Cells(colPhyNillBalance).Value) = True AndAlso clsCommon.myCdbl(gv1.Rows(ii).Cells(colPhyQty).Value) = 0 AndAlso clsCommon.myCdbl(gv1.Rows(ii).Cells(colCurrQty).Value) = 0 Then
                                Else
                                    Throw New Exception("Please provide Batch no for item : " + clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value) + " . At Line No" + clsCommon.myCstr(ii + 1))
                                End If

                            Else
                                Dim tQty As Decimal = 0
                                For Each objBatch As clsBatchInventory In arrBatchNo
                                    tQty += objBatch.Qty
                                Next
                                If tQty <> Math.Abs(clsCommon.myCdbl(gv1.Rows(ii).Cells(colDiffQty).Value)) Then
                                    Throw New Exception("Item : " + clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value) + " Entered Qty " + clsCommon.myCstr(clsCommon.myCdbl(gv1.Rows(ii).Cells(colDiffQty).Value)) + Environment.NewLine + "And Batchwise Qty " + clsCommon.myCstr(tQty) + " . At Line No" + clsCommon.myCstr(ii + 1))
                                End If
                            End If
                        End If
                        If clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0 Then
                            If clsCommon.myLen(gv1.Rows(ii).Cells(colGLAccountInvControl).Value) <= 0 Then
                                Throw New Exception("Inventory control account not found for item " + clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value))
                            End If
                            If clsCommon.myLen(gv1.Rows(ii).Cells(colGLAccount).Value) <= 0 Then
                                Throw New Exception("GL account not found for item " + clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value))
                            End If
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Try
            If SaveData() Then
                clsCommon.MyMessageBoxShow("Data saved successfully.")
                LoadData(txtCode.Value, txtLocation.Value, txtsubLoc.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Public Function SaveData(Optional ByVal isposted As Boolean = False) As Boolean
        Try
            If (AllowToSave(isposted)) Then

                Dim obj1 As New clsPhysicalstock()
                obj1.Arr = New List(Of clsPhysicalstock)
                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myCdbl(grow.Cells(colPhyQty).Value) <> 0 OrElse clsCommon.myCBool(grow.Cells(colPhyNillBalance).Value) Then
                        Dim obj As New clsPhysicalstock()
                        If isposted Then
                            obj.Is_Posted = CInt(clsCommon.myCdbl("1"))
                        End If

                        obj.Is_Milk = CInt(clsCommon.myCdbl(IIf(chkMilk.Checked = True, 1, 0)))
                        obj.Physical_No = clsCommon.myCstr(txtCode.Value)
                        obj.Description = clsCommon.myCstr(txtdesc.Text).Replace("'", "`")
                        obj.Stock_Date = clsCommon.myCDate(dtpdate.Value)
                        obj.Main_Location = clsCommon.myCstr(txtLocation.Value)
                        obj.Location_Code = clsCommon.myCstr(txtsubLoc.Value)

                        obj.Line_No = clsCommon.myCstr(grow.Cells(colSNo).Value)
                        obj.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                        obj.Item_Desc = clsCommon.myCstr(grow.Cells(colIName).Value)
                        'obj.MRP = clsCommon.myCdbl(grow.Cells(colMRP).Value)
                        obj.Stock_Unit = clsCommon.myCstr(grow.Cells(colStockUnit).Value)
                        'obj.Batch_No = clsCommon.myCstr(grow.Cells(colBatchNo).Value)
                        obj.Existing_Qty = clsCommon.myCdbl(grow.Cells(colCurrQty).Value)

                        obj.Existing_FAT_Kg = clsCommon.myCdbl(grow.Cells(colCurrFATKG).Value)
                        obj.Existing_FAT_Pers = clsCommon.myCdbl(grow.Cells(colCurrFATPers).Value)
                        obj.Existing_FAT_Amt = clsCommon.myCdbl(grow.Cells(colCurrFATAmount).Value)
                        obj.Existing_SNF_Kg = clsCommon.myCdbl(grow.Cells(colCurrSNFKG).Value)
                        obj.Existing_SNF_Pers = clsCommon.myCdbl(grow.Cells(colCurrSNFPers).Value)
                        obj.Existing_SNF_Amt = clsCommon.myCdbl(grow.Cells(colCurrSNFAmt).Value)
                        obj.Existing_Amount = clsCommon.myCdbl(grow.Cells(colCurrAmt).Value)

                        obj.Nill_Balance = clsCommon.myCBool(grow.Cells(colPhyNillBalance).Value)
                        obj.Physical_Qty = clsCommon.myCdbl(grow.Cells(colPhyQty).Value)
                        obj.FAT_Pers = clsCommon.myCdbl(grow.Cells(colPhyFATPers).Value)
                        obj.FAT_Kg = clsCommon.myCdbl(grow.Cells(colPhyFATKG).Value)
                        obj.FAT_Amt = clsCommon.myCdbl(grow.Cells(colPhyFATAmt).Value)
                        obj.SNF_Kg = clsCommon.myCdbl(grow.Cells(colPhySNFKG).Value)
                        obj.SNF_Pers = clsCommon.myCdbl(grow.Cells(colPhySNFPers).Value)
                        obj.SNF_Amt = clsCommon.myCdbl(grow.Cells(colPhySNFAmt).Value)
                        obj.Amt = clsCommon.myCdbl(grow.Cells(colPhyAmt).Value)
                        obj.arrSrItem = TryCast(grow.Tag, List(Of clsSerializeInvenotry))
                        obj.arrBatchItem = TryCast(grow.Cells(colICode).Tag, List(Of clsBatchInventory))

                        obj.GL_Account_Inventroy_Ctrl = clsCommon.myCstr(grow.Cells(colGLAccountInvControl).Value)
                        obj.GL_Account = clsCommon.myCstr(grow.Cells(colGLAccount).Value)
                        obj1.Arr.Add(obj)
                    End If
                Next
                clsPhysicalstock.SaveData(obj1, txtCode.Value, isNewEntry)
                txtCode.Value = obj1.Physical_No
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Private Sub gv1_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gv1.CellFormatting
        Try
            If Not isformLoad AndAlso gv1.CurrentRow.Index >= 0 Then
                If e.Column Is gv1.Columns(colCurrAmt) Then
                    gv1.CurrentRow.Cells(colCurrAmt).ReadOnly = chkMilk.Checked
                ElseIf e.Column Is gv1.Columns(colPhyAmt) Then
                    gv1.CurrentRow.Cells(colPhyAmt).ReadOnly = chkMilk.Checked
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        setGridFocus()
    End Sub

    Sub UpdateCurrentRow()
        If clsCommon.myCBool(gv1.CurrentRow.Cells(colPhyNillBalance).Value) Then
            gv1.CurrentRow.Cells(colPhyQty).Value = 0
            gv1.CurrentRow.Cells(colPhyAmt).Value = 0
            gv1.CurrentRow.Cells(colPhyFATPers).Value = 0
            gv1.CurrentRow.Cells(colPhySNFPers).Value = 0
        End If

        gv1.CurrentRow.Cells(colPhyFATKG).Value = System.Math.Round((clsCommon.myCdbl(gv1.CurrentRow.Cells(colPhyQty).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colPhyFATPers).Value)) / 100, 2)
        gv1.CurrentRow.Cells(colPhySNFKG).Value = System.Math.Round((clsCommon.myCdbl(gv1.CurrentRow.Cells(colPhyQty).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colPhySNFPers).Value)) / 100, 2)
        If chkMilk.Checked Then
            gv1.CurrentRow.Cells(colPhyAmt).Value = clsCommon.myCdbl(gv1.CurrentRow.Cells(colPhyFATAmt).Value) + clsCommon.myCdbl(gv1.CurrentRow.Cells(colPhySNFAmt).Value)
        End If
        gv1.CurrentRow.Cells(colDiffQty).Value = clsCommon.myCdbl(gv1.CurrentRow.Cells(colCurrQty).Value) - clsCommon.myCdbl(gv1.CurrentRow.Cells(colPhyQty).Value)
        gv1.CurrentRow.Cells(colDiffAmt).Value = clsCommon.myCdbl(gv1.CurrentRow.Cells(colCurrAmt).Value) - clsCommon.myCdbl(gv1.CurrentRow.Cells(colPhyAmt).Value)
        gv1.CurrentRow.Cells(colDiffFATKg).Value = clsCommon.myCdbl(clsCommon.myCdbl(gv1.CurrentRow.Cells(colCurrFATKG).Value) - clsCommon.myCdbl(gv1.CurrentRow.Cells(colPhyFATKG).Value))
        gv1.CurrentRow.Cells(colDiffFATPer).Value = System.Math.Round(clsCommon.myCdbl((clsCommon.myCdbl(gv1.CurrentRow.Cells(colDiffFATKg).Value) * 100) / clsCommon.myCdbl(gv1.CurrentRow.Cells(colDiffQty).Value)), 2)
        gv1.CurrentRow.Cells(colDiffFATAmt).Value = clsCommon.myCdbl(clsCommon.myCdbl(gv1.CurrentRow.Cells(colCurrFATAmount).Value) - clsCommon.myCdbl(gv1.CurrentRow.Cells(colPhyFATAmt).Value))
        gv1.CurrentRow.Cells(colDiffSNFKg).Value = clsCommon.myCdbl(clsCommon.myCdbl(gv1.CurrentRow.Cells(colCurrSNFKG).Value) - clsCommon.myCdbl(gv1.CurrentRow.Cells(colPhySNFKG).Value))
        gv1.CurrentRow.Cells(colDiffSNFPer).Value = System.Math.Round(clsCommon.myCdbl((clsCommon.myCdbl(gv1.CurrentRow.Cells(colDiffSNFKg).Value) * 100) / clsCommon.myCdbl(gv1.CurrentRow.Cells(colDiffQty).Value)), 2)
        gv1.CurrentRow.Cells(colDiffSNFAmt).Value = clsCommon.myCdbl(clsCommon.myCdbl(gv1.CurrentRow.Cells(colCurrSNFAmt).Value) - clsCommon.myCdbl(gv1.CurrentRow.Cells(colPhySNFAmt).Value))
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If Not isformLoad AndAlso gv1.CurrentRow.Index >= 0 Then
                If Not isCellvaluechanged Then
                    isCellvaluechanged = True
                    If (e.Column Is gv1.Columns(colGLAccount)) Then
                        OpenGLAccount(False)
                    ElseIf (e.Column Is gv1.Columns(colPhyQty)) Then
                        If Not clsCommon.myCBool(gv1.CurrentRow.Cells(colIsPickAutoSrNo).Value) AndAlso (clsCommon.myCdbl(gv1.CurrentRow.Cells(colPhyQty).Value) <> 0 OrElse (clsCommon.myCBool(gv1.CurrentRow.Cells(colPhyNillBalance).Value))) Then ' AndAlso CheckStockOfItemTillTransactionDateOnly = False
                            OpenSerialItem()
                        End If
                    End If
                    UpdateCurrentRow()

                    If (e.Column Is gv1.Columns(colPhyQty)) Then
                        OpenBatchItem()
                    End If
                    isCellvaluechanged = False
                End If
            End If
        Catch ex As Exception
            isCellvaluechanged = False
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub OpenBatchItem() ''BHA/03/10/18-000586 by balwinder on 04/10/2018
        Dim blnBatchqty As Boolean = False
        If clsCommon.myCBool(gv1.CurrentRow.Cells(colIsBatchItem).Value) AndAlso Not isImport Then
            If clsCommon.myCdbl(gv1.CurrentRow.Cells(colPhyQty).Value) > 0 OrElse clsCommon.myCBool(gv1.CurrentRow.Cells(colPhyNillBalance).Value) Then
                Dim diffQty As Decimal = clsCommon.myCdbl(gv1.CurrentRow.Cells(colDiffQty).Value)
                If diffQty < 0 Then
                    diffQty = Math.Abs(diffQty)
                    Dim frm As frmBatchItemIn = New frmBatchItemIn()
                    frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                    frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
                    frm.dblqty = diffQty
                    frm.strUOM = clsCommon.myCstr(gv1.CurrentRow.Cells(colStockUnit).Value)
                    'frm.dblMRP = clsCommon.myCdbl(gv1.CurrentRow.Cells(colMRP).Value)
                    frm.arr = TryCast(gv1.CurrentRow.Cells(colICode).Tag, List(Of clsBatchInventory))
                    If CheckStockOfItemTillTransactionDateOnly = True AndAlso chkMilk.Checked = False AndAlso clsCommon.myLen(txtManualBatchNo.Text) > 0 Then
                        frm.arr = New List(Of clsBatchInventory)
                        Dim obj As clsBatchInventory = New clsBatchInventory()
                        obj.Batch_No = clsCommon.myCstr(txtManualBatchNo.Text)
                        obj.Manufacture_Date = clsCommon.myCDate(dtpdate.Value)
                        Dim datex As DateTime = dtpdate.Value
                        Dim silfNoDays As Integer = clsCommon.myCdbl(txtSelfLifeDays.Text)
                        obj.Expiry_Date = clsCommon.myCDate(datex.AddDays(silfNoDays))
                        obj.Qty = clsCommon.myCdbl(diffQty)
                        obj.Manual_BatchNo = clsCommon.myCstr(txtManualBatchNo.Text)
                        If clsCommon.myLen(obj.Batch_No) > 0 AndAlso obj.Qty <> 0 Then
                            frm.arr.Add(obj)
                        End If
                        gv1.CurrentRow.Cells(colICode).Tag = frm.arr
                        txtManualBatchNo.Enabled = False
                        txtSelfLifeDays.Enabled = False
                    Else
                        frm.ShowDialog()
                        If Not frm.isCencelButtonClicked Then
                            gv1.CurrentRow.Cells(colICode).Tag = frm.arr
                        End If
                    End If

                ElseIf diffQty > 0 Then
                    If RunBatchFifowise Then
                        If clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) > 0 Then
                            Dim frm As frmBatchItemOut = New frmBatchItemOut()
                            frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                            frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
                            frm.strLocationCode = txtLocation.Value
                            frm.strCurrDocNo = txtCode.Value
                            frm.strCurrentDocDate = clsCommon.myCstr(clsCommon.GetPrintDate(dtpdate.Value, "dd/MMM/yyyy"))
                            frm.strCurrDocType = "PH-ST"
                            frm.strUOM = clsCommon.myCstr(gv1.CurrentRow.Cells(colStockUnit).Value)
                            frm.dblqty = diffQty
                            frm.arr = TryCast(gv1.CurrentRow.Cells(colICode).Tag, List(Of clsBatchInventory))
                            If frm.OpenSerialList(0, "", "") Then
                                gv1.CurrentRow.Cells(colICode).Tag = frm.arr
                                blnBatchqty = True
                            Else
                                Dim batchQty As Double = 0
                                If frm.arr IsNot Nothing AndAlso frm.arr.Count > 0 Then
                                    For Each obj As clsBatchInventory In frm.arr
                                        batchQty += obj.Qty
                                    Next
                                End If
                                clsCommon.MyMessageBoxShow("Please increase stock Item Code - " & frm.strItemCode & " , Entered Qty - " & clsCommon.myCstr(frm.dblqty) & " Batch Qty - " & clsCommon.myCstr(batchQty), Me.Text)
                                blnBatchqty = False
                                Exit Sub
                            End If
                        End If
                    Else
                        Dim frm As frmBatchItemOut = New frmBatchItemOut()
                        frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                        frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
                        frm.strLocationCode = txtLocation.Value
                        frm.strCurrDocNo = txtCode.Value
                        frm.strCurrDocType = "PH-ST"
                        frm.strUOM = clsCommon.myCstr(gv1.CurrentRow.Cells(colStockUnit).Value)
                        'frm.dblMRP = clsCommon.myCdbl(gv1.CurrentRow.Cells(colMRP).Value)
                        frm.dblqty = diffQty
                        frm.arr = TryCast(gv1.CurrentRow.Cells(colICode).Tag, List(Of clsBatchInventory))
                        frm.ShowDialog()
                        If Not frm.isCencelButtonClicked Then
                            gv1.CurrentRow.Cells(colICode).Tag = frm.arr
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub OpenGLAccount(ByVal isButtonClick As Boolean)
        Dim qry As String
        Dim whrcls As String
        Dim arr As New ArrayList()
        If txtlocation.Value = "" Then
            common.clsCommon.MyMessageBoxShow("Please first select Location")
            Return
        End If
        Dim logSeg As String = clsLocation.GetSegmentCode(txtLocation.Value, Nothing)
        arr = clsERPFuncationality.glaccountquery(objCommonVar.CurrentUserCode)
        qry = arr.Item(0) + " inner join TSPL_GL_STRUCTURE on TSPL_GL_ACCOUNTS .Str_Code=TSPL_GL_STRUCTURE.Str_Code "
        whrcls = arr.Item(1)
        If whrcls = "" Then
        Else
            whrcls = "(" + whrcls + ")"
        End If
        If whrcls Is Nothing OrElse whrcls = "" Then
            whrcls = " 1<>(Seg_No1 +Seg_No2 +Seg_No3 +Seg_No4 +Seg_No5 +Seg_No6 +Seg_No7 +Seg_No8 +Seg_No9 +Seg_No10 )"
        Else
            whrcls = whrcls + " and 1<>(Seg_No1 +Seg_No2 +Seg_No3 +Seg_No4 +Seg_No5 +Seg_No6 +Seg_No7 +Seg_No8 +Seg_No9 +Seg_No10 )"
        End If
        whrcls += "   and TSPL_GL_ACCOUNTS.Account_Seg_Code7='" + logSeg + "'  and TSPL_GL_ACCOUNTS.ControlAccount='N'  "

        gv1.CurrentRow.Cells(colGLAccount).Value = clsCommon.ShowSelectForm("PhystLocSeg", qry, "Account_Code", whrcls, clsCommon.myCstr(gv1.CurrentRow.Cells(colGLAccount).Value), "Account_Code", isButtonClick)
        gv1.CurrentRow.Cells(colGLAccountName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colGLAccount).Value) + "'"))
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub txtLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtLocation._MYValidating
        Try
            'Dim qry As String = "select Location_Code ,Location_Desc from TSPL_LOCATION_MASTER "
            Dim whrclas As String = "Location_Type='Physical'"
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrclas += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            txtLocation.Value = clsLocation.getFinder(whrclas, txtLocation.Value, isButtonClicked) ' clsCommon.ShowSelectForm("Location@PhysicalStock", qry, "Location_Code", whrclas, txtLocation.Value, "", isButtonClicked)
            txtLocName.Text = clsLocation.GetName(txtLocation.Value, Nothing)

            'If Not chkMilk.Checked Then
            '    LoadData(txtCode.Value, txtLocation.Value, txtsubLoc.Value, NavigatorType.Current)
            'Else
            '    txtsubLoc.Value = ""
            '    txtsubLocName.Text = ""
            '    gv1.Rows.Clear()
            'End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub LoadData(ByVal strcode As String, ByVal strLocation As String, ByVal SubLocCode As String, ByVal NavType As NavigatorType)
        Try
            LoadBlankGrid()
            isNewEntry = True
            Dim arr As New List(Of clsPhysicalstock)
            arr = clsPhysicalstock.GetData(txtCode.Value, txtLocation.Value, txtsubLoc.Value, chkMilk.Checked, NavType, dtpdate.Value.ToString())
            isformLoad = True
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each obj As clsPhysicalstock In arr
                    If clsCommon.myCdbl(obj.Is_Posted) = 1 Then
                        btnsave.Enabled = False
                        btnpost.Enabled = False
                        btnDelete.Enabled = False
                        UsLock1.Status = ERPTransactionStatus.Approved
                    Else
                        btnsave.Enabled = True
                        btnpost.Enabled = True
                        btnDelete.Enabled = True
                        UsLock1.Status = ERPTransactionStatus.Pending
                    End If
                    chkMilk.Checked = clsCommon.myCBool(IIf(obj.Is_Milk = 1, True, False))
                    txtCode.Value = obj.Physical_No
                    txtdesc.Text = obj.Description
                    If isGoClick = False Then
                        dtpdate.Text = obj.Stock_Date
                    End If
                    If obj.Is_Milk Then
                        If clsCommon.myLen(obj.Main_Location) > 0 Then
                            txtLocation.Value = obj.Main_Location
                            txtLocName.Text = clsLocation.GetName(txtLocation.Value, Nothing)
                            txtsubLoc.Value = obj.Location_Code
                            txtsubLocName.Text = clsLocation.GetName(txtsubLoc.Value, Nothing)
                        Else
                            txtLocation.Value = obj.Location_Code
                            txtLocName.Text = clsLocation.GetName(txtsubLoc.Value, Nothing)
                            txtsubLoc.Value = ""
                            txtsubLocName.Text = ""
                        End If

                    Else
                        txtLocation.Value = obj.Location_Code
                        txtLocName.Text = clsLocation.GetName(txtLocation.Value, Nothing)
                        txtsubLoc.Value = ""
                        txtsubLocName.Text = ""
                    End If
                    gv1.Rows.AddNew()

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSNo).Value = obj.Line_No
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = obj.Item_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = obj.Item_Desc
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIsBatchItem).Value = clsItemMaster.IsBatchItem(obj.Item_Code)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Tag = obj.arrBatchItem
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colStockUnit).Value = obj.Stock_Unit
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCurrQty).Value = obj.Existing_Qty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCurrFATKG).Value = obj.Existing_FAT_Kg
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCurrFATPers).Value = obj.Existing_FAT_Pers
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCurrFATAmount).Value = obj.Existing_FAT_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCurrSNFKG).Value = obj.Existing_SNF_Kg
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCurrSNFPers).Value = obj.Existing_SNF_Pers
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCurrSNFAmt).Value = obj.Existing_SNF_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCurrAmt).Value = obj.Existing_Amount

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPhyNillBalance).Value = obj.Nill_Balance
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPhyQty).Value = obj.Physical_Qty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPhyFATKG).Value = obj.FAT_Kg
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPhyFATPers).Value = obj.FAT_Pers
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPhyFATAmt).Value = obj.FAT_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPhySNFKG).Value = obj.SNF_Kg
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPhySNFPers).Value = obj.SNF_Pers
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPhySNFAmt).Value = obj.SNF_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPhyAmt).Value = obj.Amt

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDiffQty).Value = obj.Difference
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDiffFatPer).Value = obj.FatPerDifference
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDiffFATKg).Value = obj.FatKgDifference
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDiffFATAmt).Value = obj.FatAmtDifference
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDiffSNFPer).Value = obj.SNFPerDifference
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDiffSNFKg).Value = obj.SNFKgDifference
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDiffSNFAmt).Value = obj.SNFAmtDifference
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDiffAmt).Value = obj.DifferenceAmt

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIsSerialseItem).Value = clsCommon.myCBool(clsItemMaster.IsSerializeItem(obj.Item_Code))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIsPickAutoSrNo).Value = clsCommon.myCBool(clsItemMaster.IsPickAutoSerializeItem(obj.Item_Code))
                    gv1.Rows(gv1.Rows.Count - 1).Tag = obj.arrSrItem

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colGLAccountInvControl).Value = obj.GL_Account_Inventroy_Ctrl
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colGLAccountNameInvControl).Value = obj.GL_Account_Inventroy_CtrlName
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colGLAccount).Value = obj.GL_Account
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colGLAccountName).Value = obj.GL_AccountName

                Next
                If gv1.Rows.Count > 0 Then
                    gv1.CurrentRow = gv1.Rows(0)
                End If
                txtsubLoc.Enabled = chkMilk.Checked
                txtCode.MyReadOnly = True
            Else
                reset()
            End If
            CheckUnCheckMilkType()
            If clsCommon.myLen(txtCode.Value) > 0 Then
                isNewEntry = False
            Else
                isNewEntry = True
            End If
            isformLoad = False
        Catch ex As Exception
            isformLoad = False
            isNewEntry = True
            Throw New Exception(ex.Message)
        End Try
    End Sub
    ''richa UDL/29/08/19-000314
    Private Sub LoadDataForFirstTime(ByVal strcode As String, ByVal strLocation As String, ByVal SubLocCode As String, ByVal NavType As NavigatorType)
        Try
            LoadBlankGrid()
            isNewEntry = True
            Dim dt As DataTable = Nothing
            Dim strItemType As String = ""
            Dim strInventoryAccount As String = ""
            If txtItemType.arrValueMember IsNot Nothing AndAlso txtItemType.arrValueMember.Count > 0 Then
                strItemType = clsCommon.GetMulcallString(txtItemType.arrValueMember)
            End If
            If txtInventoryAccount.arrValueMember IsNot Nothing AndAlso txtInventoryAccount.arrValueMember.Count > 0 Then
                strInventoryAccount = clsCommon.GetMulcallString(txtInventoryAccount.arrValueMember)
            End If
            dt = clsPhysicalstock.GetDataForFirstTime(txtCode.Value, txtLocation.Value, txtsubLoc.Value, chkMilk.Checked, NavType, Nothing, dtpdate.Value.ToString(), strItemType, strInventoryAccount)
            isformLoad = True
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    gv1.Rows.AddNew()

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSNo).Value = clsCommon.myCstr(dr("line_no"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = clsCommon.myCstr(dr("item_code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = clsCommon.myCstr(dr("Item_Desc"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIsBatchItem).Value = IIf(clsCommon.myCdbl(clsCommon.myCstr(dr("Is_Batch_Item"))) = 1, True, False)
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Tag = clsBatchInventory.GetData("PH-ST", clsCommon.myCstr(dr("physical_no")), clsCommon.myCstr(dr("item_code")), clsCommon.myCstr(dr("line_no")), Nothing)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colStockUnit).Value = clsCommon.myCstr(dr("stock_unit"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCurrQty).Value = clsCommon.myCdbl(dr("Existing_Qty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCurrFATKG).Value = clsCommon.myCdbl(dr("Existing_FAT_Kg"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCurrFATAmount).Value = clsCommon.myCdbl(dr("Existing_FAT_Amt"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCurrSNFKG).Value = clsCommon.myCdbl(dr("Existing_SNF_Kg"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCurrSNFAmt).Value = clsCommon.myCdbl(dr("Existing_SNF_Amt"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCurrAmt).Value = clsCommon.myCdbl(dr("Existing_Amount"))

                    If clsCommon.myCdbl(dr("Existing_Qty")) <> 0 Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCurrFATPers).Value = System.Math.Round((clsCommon.myCdbl(dr("Existing_FAT_Kg")) * 100) / clsCommon.myCdbl(dr("Existing_Qty")), 2)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCurrSNFPers).Value = System.Math.Round((clsCommon.myCdbl(dr("Existing_SNF_Kg")) * 100) / clsCommon.myCdbl(dr("Existing_Qty")), 2)
                    Else
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCurrFATPers).Value = 0
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCurrSNFPers).Value = 0
                    End If

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPhyNillBalance).Value = (clsCommon.myCdbl(dr("Nill_Balance")) = 1)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPhyQty).Value = clsCommon.myCdbl(dr("Physical_Qty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPhyFATKG).Value = clsCommon.myCdbl(dr("FAT_Kg"))
                    If clsCommon.myCdbl(dr("Physical_Qty")) <> 0 Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPhyFATPers).Value = System.Math.Round((clsCommon.myCdbl(dr("FAT_Kg")) * 100) / clsCommon.myCdbl(dr("Physical_Qty")), 2)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPhySNFPers).Value = System.Math.Round((clsCommon.myCdbl(dr("SNF_Kg")) * 100) / clsCommon.myCdbl(dr("Physical_Qty")), 2)
                    Else
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPhyFATPers).Value = 0
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPhySNFPers).Value = 0
                    End If

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPhyFATAmt).Value = clsCommon.myCdbl(dr("FAT_Amt"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPhySNFKG).Value = clsCommon.myCdbl(dr("SNF_Kg"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPhySNFAmt).Value = clsCommon.myCdbl(dr("SNF_Amt"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPhyAmt).Value = clsCommon.myCdbl(dr("Amt"))

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDiffQty).Value = clsCommon.myCdbl(clsCommon.myCdbl(dr("Existing_Qty")) - clsCommon.myCdbl(dr("Physical_Qty")))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDiffFATKg).Value = clsCommon.myCdbl(clsCommon.myCdbl(dr("Existing_FAT_Kg")) - clsCommon.myCdbl(dr("FAT_Kg")))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDiffFATAmt).Value = clsCommon.myCdbl(clsCommon.myCdbl(dr("Existing_FAT_Amt")) - clsCommon.myCdbl(dr("FAT_Amt")))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDiffSNFKg).Value = clsCommon.myCdbl(clsCommon.myCdbl(dr("Existing_SNF_Kg")) - clsCommon.myCdbl(dr("SNF_Kg")))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDiffSNFAmt).Value = clsCommon.myCdbl(clsCommon.myCdbl(dr("Existing_SNF_Amt")) - clsCommon.myCdbl(dr("SNF_Amt")))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDiffAmt).Value = clsCommon.myCdbl(clsCommon.myCdbl(dr("Existing_Amount")) - clsCommon.myCdbl(dr("Amt")))


                    If clsCommon.myCdbl(clsCommon.myCdbl(dr("Existing_Qty")) - clsCommon.myCdbl(dr("Physical_Qty"))) <> 0 Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDiffFATPer).Value = System.Math.Round((clsCommon.myCdbl(clsCommon.myCdbl(dr("Existing_FAT_Kg")) - clsCommon.myCdbl(dr("FAT_Kg"))) * 100) / clsCommon.myCdbl(clsCommon.myCdbl(dr("Existing_Qty")) - clsCommon.myCdbl(dr("Physical_Qty"))), 2)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDiffSNFPer).Value = System.Math.Round((clsCommon.myCdbl(clsCommon.myCdbl(dr("Existing_SNF_Kg")) - clsCommon.myCdbl(dr("SNF_Kg"))) * 100) / clsCommon.myCdbl(clsCommon.myCdbl(dr("Existing_Qty")) - clsCommon.myCdbl(dr("Physical_Qty"))), 2)
                    Else
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDiffFATPer).Value = 0
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDiffSNFPer).Value = 0
                    End If

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIsSerialseItem).Value = IIf(clsCommon.myCdbl(clsCommon.myCstr(dr("Is_Serial_Item"))) = 1, True, False)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIsPickAutoSrNo).Value = IIf(clsCommon.myCdbl(clsCommon.myCstr(dr("Is_Pick_Auto_SrNo"))) = 1, True, False)
                    'gv1.Rows(gv1.Rows.Count - 1).Tag = obj.arrSrItem

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colGLAccountInvControl).Value = clsCommon.myCstr(dr("GL_Account_Inventroy_Ctrl"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colGLAccountNameInvControl).Value = clsCommon.myCstr(dr("GL_Account_Inventroy_CtrlName"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colGLAccount).Value = clsCommon.myCstr(dr("GL_Account"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colGLAccountName).Value = clsCommon.myCstr(dr("GL_AccountName"))

                Next
                If gv1.Rows.Count > 0 Then
                    gv1.CurrentRow = gv1.Rows(0)
                End If
                txtsubLoc.Enabled = chkMilk.Checked
                txtCode.MyReadOnly = True
            Else
                reset()
            End If
            CheckUnCheckMilkType()
            If clsCommon.myLen(txtCode.Value) > 0 Then
                isNewEntry = False
            Else
                isNewEntry = True
            End If
            isformLoad = False
        Catch ex As Exception
            isformLoad = False
            isNewEntry = True
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnReset_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
    End Sub

    Private Sub txtsubLoc__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtsubLoc._MYValidating
        Dim whrcls As String = " (isnull(is_sub_location,'N')='Y' or isnull(Is_Section,'N')='Y') and Loc_Segment_Code='" + clsLocation.GetSegmentCode(txtLocation.Value, Nothing) + "'"
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrcls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtsubLoc.Value = clsLocation.getFinder(whrcls, txtsubLoc.Value, isButtonClicked)
        txtsubLocName.Text = clsLocation.GetName(txtsubLoc.Value, Nothing)

    End Sub

    Private Sub chkMilk_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkMilk.ToggleStateChanged
        CheckUnCheckMilkType()
        If Not chkMilk.Checked Then
            txtsubLoc.Value = ""
            txtsubLocName.Text = ""
            gv1.Rows.Clear()
        End If
    End Sub

    Sub CheckUnCheckMilkType()
        txtsubLoc.Enabled = chkMilk.Checked
        If gv1.Columns.Count >= 0 Then
            gv1.Columns(colPhyFATPers).IsVisible = chkMilk.Checked
            gv1.Columns(colPhyFATKG).IsVisible = chkMilk.Checked
            gv1.Columns(colPhyFATAmt).IsVisible = chkMilk.Checked
            gv1.Columns(colPhySNFPers).IsVisible = chkMilk.Checked
            gv1.Columns(colPhySNFKG).IsVisible = chkMilk.Checked
            gv1.Columns(colPhySNFAmt).IsVisible = chkMilk.Checked

            gv1.Columns(colCurrFATPers).IsVisible = chkMilk.Checked
            gv1.Columns(colCurrFATKG).IsVisible = chkMilk.Checked
            gv1.Columns(colCurrFATAmount).IsVisible = chkMilk.Checked
            gv1.Columns(colCurrSNFPers).IsVisible = chkMilk.Checked
            gv1.Columns(colCurrSNFKG).IsVisible = chkMilk.Checked
            gv1.Columns(colCurrSNFAmt).IsVisible = chkMilk.Checked

            gv1.Columns(colDiffFATPer).IsVisible = chkMilk.Checked
            gv1.Columns(colDiffFATKg).IsVisible = chkMilk.Checked
            gv1.Columns(colDiffFATAmt).IsVisible = chkMilk.Checked
            gv1.Columns(colDiffSNFPer).IsVisible = chkMilk.Checked
            gv1.Columns(colDiffSNFKg).IsVisible = chkMilk.Checked
            gv1.Columns(colDiffSNFAmt).IsVisible = chkMilk.Checked
        End If

    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        LoadData(txtCode.Value, txtLocation.Value, txtsubLoc.Value, NavType)
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCode._MYValidating
        Dim qry As String = "select count(*) from TSPL_PHYSICAL_STOCK where physical_no='" + txtCode.Value + "' and isnull(Multiple_Location,0)=0 "
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
        Dim whrclas As String = "  isnull(Multiple_Location,0)=0 "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrclas += "  and  [Location Code] in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        If check > 0 Then
            txtCode.MyReadOnly = True
        Else
            txtCode.MyReadOnly = False
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            txtCode.Value = clsPhysicalstock.GetFinder(whrclas, txtCode.Value, isButtonClicked)
            LoadData(txtCode.Value, txtLocation.Value, txtsubLoc.Value, NavigatorType.Current)
        End If

    End Sub

    Private Sub gv1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv1.KeyDown
        If e.KeyCode = Keys.F4 Then
            OpenSerialItem()
        ElseIf e.KeyCode = Keys.F5 Then
            '======update by preeti gupta 17/10/2018
            If RunBatchFifowise = 0 Then
                OpenBatchItem()
            Else
                OpenBatchItemIfFIFIOSettingON()
            End If
        End If
    End Sub
    '============created by preeti gupta===============
    Public Sub OpenBatchItemIfFIFIOSettingON()
        Dim arr As List(Of clsBatchInventory) = Nothing
        Dim strBatchunion As String = ""
        If clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) > 0 Then
            arr = TryCast(gv1.CurrentRow.Cells(colICode).Tag, List(Of clsBatchInventory))
        End If
        If Not arr Is Nothing Then
            If arr.Count > 0 Then
                For Each obj As clsBatchInventory In arr
                    strBatchunion += " Batch No - " & clsCommon.myCstr(obj.Batch_No) & "         Qty - " & clsCommon.myCstr(obj.Qty) + Environment.NewLine
                Next
                clsCommon.MyMessageBoxShow(strBatchunion, Me.Text)
            End If
        End If
    End Sub
    Sub OpenSerialItem()
        If clsCommon.myCBool(gv1.CurrentRow.Cells(colIsSerialseItem).Value) Then
            Dim Item_type As String = clsDBFuncationality.getSingleValue("select Item_Type from TSPL_ITEM_MASTER where Item_Code='" + gv1.CurrentRow.Cells(colICode).Value + "'")
            If clsCommon.myCdbl(gv1.CurrentRow.Cells(colPhyQty).Value) > 0 Then 'in
                Dim frm As FrmSerializeItemIn = New FrmSerializeItemIn()
                frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
                frm.dblqty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colPhyQty).Value)
                frm.strItemType = Item_type
                frm.arr = TryCast(gv1.CurrentRow.Tag, List(Of clsSerializeInvenotry))
                frm.ShowDialog()
                If Not frm.isCencelButtonClicked Then
                    gv1.CurrentRow.Tag = frm.arr
                End If
            Else
                Dim frm As frmSerializeItemOut = New frmSerializeItemOut()
                frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
                frm.strLocationCode = txtLocation.Value
                frm.strCurrDocNo = txtCode.Value
                frm.strCurrDocType = "PH-ST"
                Dim dblqty As Decimal = clsCommon.myCdbl(gv1.CurrentRow.Cells(colPhyQty).Value)
                If dblqty < 0 Then
                    dblqty = clsCommon.myCdbl(clsCommon.myCstr(dblqty).Substring(1, clsCommon.myLen(dblqty) - 1))
                End If
                frm.dblqty = dblqty
                frm.strItemType = Item_type
                frm.arr = TryCast(gv1.CurrentRow.Tag, List(Of clsSerializeInvenotry))
                frm.ShowDialog()
                If Not frm.isCencelButtonClicked Then
                    gv1.CurrentRow.Tag = frm.arr
                End If
            End If
        End If
    End Sub

    Private Sub btnpost_Click(sender As Object, e As EventArgs) Handles btnpost.Click
        Try
            If (myMessages.postConfirm()) Then
                If SaveData(True) Then
                    clsPhysicalstock.PostData(txtCode.Value, True, txtLocation.Value, txtsubLoc.Value, chkMilk.Checked)
                    clsCommon.MyMessageBoxShow("Data posted successfully.")
                    LoadData(txtCode.Value, txtLocation.Value, txtsubLoc.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        '' apply validation
        isGoClick = True
        If chkMilk.Checked Then
            If clsCommon.myLen(txtLocation.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("select Location!!")
                Exit Sub
            End If
            If clsCommon.myLen(txtsubLoc.Value) <= 0 Then
                If clsCommon.MyMessageBoxShow("Proceed without sub location ?", "Validate Sub Location/Section", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
            End If

        End If
        'LoadData(txtCode.Value, txtLocation.Value, txtsubLoc.Value, NavigatorType.Current)
        LoadDataForFirstTime(txtCode.Value, txtLocation.Value, txtsubLoc.Value, NavigatorType.Current)
        isGoClick = False
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If clsPhysicalstock.DeleteData(txtCode.Value) Then
                    clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    reset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub btnReverse_Click(sender As Object, e As EventArgs) Handles btnReverse.Click
        Try
            If clsCommon.MyMessageBoxShow("Do you want to Reverse and unpost the current Document" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                If clsPhysicalstock.ReverseAndUnpost(txtCode.Value) Then
                    clsCommon.MyMessageBoxShow("Task done Successfully", Me.Text)
                    LoadData(txtCode.Value, txtLocation.Value, txtsubLoc.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnexport_Click(sender As Object, e As EventArgs) Handles btnexport.Click
        Try
            Dim str As String = ""
            If gv1.Rows.Count > 0 Then
                For ii As Integer = 0 To gv1.Rows.Count - 1
                    If ii <> 0 Then
                        str += " Union all "
                    End If
                    If chkMilk.Checked Then
                        str += "select '" + clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value) + "' as Item,'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colIName).Value).Replace("'", "''") + "' as ItemName,'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colGLAccountInvControl).Value).Replace("'", "''") + "' as InventoryAccount,'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colGLAccount).Value) + "' as GLAccount,'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colCurrQty).Value) + "' as CurrentQty,'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colCurrFATPers).Value) + "' as CurrentFATPer,'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colCurrFATKG).Value) + "' as CurrentFATKg,'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colCurrFATAmount).Value) + "' as CurrentFATAmt,'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colCurrSNFPers).Value) + "' as CurrentSNFPer,'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colCurrSNFKG).Value) + "' as CurrentSNFKg,'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colCurrSNFAmt).Value) + "' as CurrentSNFAmt,'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colCurrAmt).Value) + "' as CurrentAmt ,'" + IIf(clsCommon.myCBool(gv1.Rows(ii).Cells(colPhyNillBalance).Value), "Y", "N") + "' as [NillBalance(Y/N)],0 as Qty,0 as FAT,0 as FATAmount,0 as SNF,0 as SNFAmount"
                    Else
                        str += "select '" + clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value) + "' as Item,'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colIName).Value).Replace("'", "''") + "' as ItemName,'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colGLAccountInvControl).Value).Replace("'", "''") + "' as InventoryAccount,'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colGLAccount).Value) + "' as GLAccount,'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colCurrQty).Value) + "' as CurrentQty,'" + clsCommon.myCstr(gv1.Rows(ii).Cells(colCurrAmt).Value) + "' as CurrentAmt,'" + IIf(clsCommon.myCBool(gv1.Rows(ii).Cells(colPhyNillBalance).Value), "Y", "N") + "' as [NillBalance(Y/N)],0 as Qty,0 as Amount "
                    End If
                Next
            Else
                If chkMilk.Checked Then
                    str = "select '' as Item,'' as ItemName,'' as InventoryAccount,'' as GLAccount,0 as CurrentQty,0 as CurrentFATPer,0 as as CurrentFATKg,0 as CurrentFATAmt,0 as CurrentSNFPer,0 as CurrentSNFKg,0 as CurrentSNFAmt,0 as CurrentAmt ,'N' as [NillBalance(Y/N)],0 as Qty,0 as FAT,0 as FATAmount,0 as SNF,0 as SNFAmount"
                Else
                    str = "select '' as Item,'' as ItemName,'' as InventoryAccount,'' as GLAccount,0 as CurrentQty,0 as CurrentAmt,'N' as [NillBalance(Y/N)],0 as Qty,0 as Amount "
                End If
            End If
            transportSql.ExporttoExcel(str, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Dim isImport As Boolean = False
    Private Sub btnimport_Click(sender As Object, e As EventArgs) Handles btnimport.Click
        ''GKD/24/09/18-000165 by balwinder on 26/09/2018
        Dim gvImport As New RadGridView()
        Me.Controls.Add(gvImport)
        isImport = True
        Try
            If chkMilk.Checked Then
                transportSql.importExcel(gvImport, "Item", "ItemName", "GLAccount", "CurrentQty", "CurrentFATPer", "CurrentFATKg", "CurrentFATAmt", "CurrentSNFPer", "CurrentSNFKg", "CurrentSNFAmt", "CurrentAmt", "NillBalance(Y/N)", "Qty", "FAT", "FATAmount", "SNF", "SNFAmount")
            Else
                transportSql.importExcel(gvImport, "Item", "ItemName", "GLAccount", "CurrentQty", "CurrentAmt", "NillBalance(Y/N)", "Qty", "Amount")
            End If
            For ii As Integer = 0 To gvImport.RowCount - 1
                Dim strICode As String = clsCommon.myCstr(gvImport.Rows(ii).Cells("Item").Value)
                For jj As Integer = 0 To gv1.RowCount - 1
                    Dim strICodeGrid As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colICode).Value)
                    If clsCommon.CompairString(strICode, strICodeGrid) = CompairStringResult.Equal Then
                        gv1.CurrentRow = gv1.Rows(jj)
                        gv1.Rows(jj).Cells(colPhyQty).Value = clsCommon.myCdbl(gvImport.Rows(ii).Cells("Qty").Value)
                        If chkMilk.Checked Then
                            gv1.Rows(jj).Cells(colPhyFATPers).Value = clsCommon.myCdbl(gvImport.Rows(ii).Cells("FAT").Value)
                            gv1.Rows(jj).Cells(colPhyFATAmt).Value = clsCommon.myCdbl(gvImport.Rows(ii).Cells("FATAmount").Value)
                            gv1.Rows(jj).Cells(colPhySNFPers).Value = clsCommon.myCdbl(gvImport.Rows(ii).Cells("SNF").Value)
                            gv1.Rows(jj).Cells(colPhySNFAmt).Value = clsCommon.myCdbl(gvImport.Rows(ii).Cells("SNFAmount").Value)
                        Else
                            gv1.Rows(jj).Cells(colPhyAmt).Value = clsCommon.myCdbl(gvImport.Rows(ii).Cells("Amount").Value)
                        End If
                        gv1.Rows(jj).Cells(colGLAccount).Value = clsCommon.myCstr(gvImport.Rows(ii).Cells("GLAccount").Value)
                        gv1.Rows(jj).Cells(colPhyNillBalance).Value = (clsCommon.CompairString(clsCommon.myCstr(gvImport.Rows(ii).Cells("NillBalance(Y/N)").Value), "Y") = CompairStringResult.Equal)
                        UpdateCurrentRow()
                        Exit For
                    End If
                Next
            Next
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        Finally
            Me.Controls.Remove(gvImport)
            isImport = False
        End Try
    End Sub

    Private Sub txtInventoryAccount__My_Click(sender As Object, e As EventArgs) Handles txtInventoryAccount._My_Click
        Dim qry As String = " select distinct TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account as Code, TSPL_GL_ACCOUNTS.Description as Name from TSPL_PURCHASE_ACCOUNTS " & _
                            " left outer join TSPL_GL_ACCOUNTS  on TSPL_GL_ACCOUNTS.Account_Code=TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account "
        txtInventoryAccount.arrValueMember = clsCommon.ShowMultipleSelectForm("InventoryAccount@PhsicalStock", qry, "Code", "Name", txtInventoryAccount.arrValueMember, txtInventoryAccount.arrDispalyMember)
    End Sub

    Private Sub txtItemType__My_Click(sender As Object, e As EventArgs) Handles txtItemType._My_Click
        txtItemType.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemTypeForPhysicalStock", FrmItemMasterRMOther.LoadItemTypeQuery(), "Code", "Name", txtItemType.arrValueMember, txtItemType.arrDispalyMember)
    End Sub
End Class
