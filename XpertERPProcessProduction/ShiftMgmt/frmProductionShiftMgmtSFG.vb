Imports common
Imports System.IO

Public Class frmProductionShiftMgmtSFG
    Inherits FrmMainTranScreen
#Region "Variables"
    Public FilterDate As Date
    Public FilterShift As String
    Public FilterLocation As String


    Dim isNewEntry As Boolean = False
    Const ColProSFGPKID As String = "ColProSFGPKID"
    Const ColProSFGSNo As String = "ColProSFGSNo"
    Const ColProSFGItemCode As String = "ColProSFGItemCode"
    Const ColProSFGItemName As String = "ColProSFGItemName"
    Const ColProSFGBatchNo As String = "ColProSFGBatchNo"
    Const ColProSFGQtyKG As String = "ColProSFGQtyKG"
    Const ColProSFGQtyLTR As String = "ColProSFGQtyLTR"
    Const ColProSFGLocationCode As String = "ColProSFGLocationCode"
    Const ColProSFGLocationName As String = "ColProSFGLocationName"
    Const ColProSFGFAT As String = "ColProSFGFAT"
    Const ColProSFGSNF As String = "ColProSFGSNF"
    Const ColProSFGFATKG As String = "ColProSFGFATKG"
    Const ColProSFGSNFKG As String = "ColProSFGSNFKG"
    Const ColProSFGAdd As String = "ColProSFGAdd"
    Const ColProSFGRemove As String = "ColProSFGRemove"
    Const ColProSFGRemarks As String = "ColProSFGRemarks"
    Const ColProSFGEnteredUOM As String = "ColProSFGEnteredUOM"
    Const ColProSFGBOMCode As String = "ColProSFGBOMCode"


    Const ColProRMPKID As String = "ColProRMPKID"
    Const ColProRMSNo As String = "ColProRMSNo"
    Const ColProRMItemCode As String = "ColProRMItemCode"
    Const ColProRMItemName As String = "ColProRMItemName"
    Const ColProRMQty As String = "ColProRMQty"
    Const ColProRMUOM As String = "ColProRMUOM"
    Const ColProRMFAT As String = "ColProRMFAT"
    Const ColProRMSNF As String = "ColProRMSNF"
    Const ColProRMFATKG As String = "ColProRMFATKG"
    Const ColProRMSNFKG As String = "ColProRMSNFKG"
    Const ColProRMIssue As String = "ColProRMIssue"

    Const ReportID As String = "ShftMgmt"
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Dim arrLoc As String = Nothing
#End Region
    Private Sub frmDairyProductionUploader_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.SetUserMgmt(clsUserMgtCode.ProductionShiftMgmt)
        Dim qry As String = "select Document_No from TSPL_SHIFT_MGMT_SFG where Document_Date='" + clsCommon.GetPrintDate(FilterDate, "dd/MMM/yyyy") + "' and Shift_Code='" + FilterShift + "' and Location_Code='" + FilterLocation + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            LoadData(clsCommon.myCstr(dt.Rows(0)("Document_No")), NavigatorType.Current)
        Else
            isNewEntry = True
            AddNew()
            txtDate.Value = FilterDate
            txtShift.Value = FilterShift
            SetShiftStartEndDateTime()
            txtLocation.Value = FilterLocation
            lblLocationFG.Text = clsLocation.GetName(txtLocation.Value, Nothing)
        End If
        RadPageView3.SelectedPage = RadPageViewPage10
        EnableDisableControl(False)
        LOCATIONRIGTHS()
        btnReverse.Visible = False
    End Sub
    Private Sub LOCATIONRIGTHS()
        Dim obj As New clsMCCCodes()
        Try
            obj = clsMCCCodes.GetData()
            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
        End Try
    End Sub

    Private Sub EnableDisableControl(v As Boolean)
        txtDate.Enabled = v
        txtShiftStart.Enabled = v
        txtShiftEnd.Enabled = v
        txtLocation.Enabled = v
        txtShift.Enabled = v
    End Sub
    Private Sub btnAddNew_Click(sender As Object, e As EventArgs)
        AddNew()
    End Sub
    Sub AddNew()
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        txtDocNo.Text = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtShiftStart.Value = txtDate.Value
        txtShiftEnd.Value = txtDate.Value
        txtLocation.Value = ""
        lblLocationFG.Text = ""
        isNewEntry = True
        UsLock1.Status = ERPTransactionStatus.Pending
        LoadBlankGrid()
        LoadBlankGridProRM()
        gvProSFG.Rows.AddNew()
        isCellValueChangedOpen = False
        txtShift.Value = ""
    End Sub

    Sub LoadBlankGrid()
        LoadBlankGridProSFG()
    End Sub
    Sub LoadBlankGridProSFG()
        gvProSFG.Columns.Clear()
        gvProSFG.DataSource = Nothing
        gvProSFG.Rows.Clear()



        Dim repoTextBox As New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "PK ID"
        repoTextBox.Name = ColProSFGPKID
        repoTextBox.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTextBox.Width = 200
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = False
        gvProSFG.MasterTemplate.Columns.Add(repoTextBox)

        Dim repoNumBox As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "SNo"
        repoNumBox.Name = ColProSFGSNo
        repoNumBox.Width = 40
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvProSFG.MasterTemplate.Columns.Add(repoNumBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Item Code"
        repoTextBox.Name = ColProSFGItemCode
        repoTextBox.HeaderImage = Global.XpertERPProcessProduction.My.Resources.Resources.search4
        repoTextBox.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTextBox.Width = 100
        repoTextBox.IsVisible = True
        gvProSFG.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Item"
        repoTextBox.Name = ColProSFGItemName
        repoTextBox.Width = 150
        repoTextBox.ReadOnly = True
        gvProSFG.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Silo Location Code"
        repoTextBox.Name = ColProSFGLocationCode
        repoTextBox.HeaderImage = Global.XpertERPProcessProduction.My.Resources.Resources.search4
        repoTextBox.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTextBox.Width = 100
        repoTextBox.IsVisible = True
        gvProSFG.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Silo Location"
        repoTextBox.Name = ColProSFGLocationName
        repoTextBox.Width = 150
        repoTextBox.ReadOnly = True
        gvProSFG.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Batch No"
        repoTextBox.Name = ColProSFGBatchNo
        repoTextBox.Width = 100
        repoTextBox.IsVisible = True
        gvProSFG.MasterTemplate.Columns.Add(repoTextBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Qty LTR"
        repoNumBox.Name = ColProSFGQtyLTR
        repoNumBox.Width = 80
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 2
        repoNumBox.ReadOnly = False
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvProSFG.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Qty KG"
        repoNumBox.Name = ColProSFGQtyKG
        repoNumBox.Width = 80
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 2
        repoNumBox.ReadOnly = False
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvProSFG.MasterTemplate.Columns.Add(repoNumBox)



        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "FAT %"
        repoNumBox.Name = ColProSFGFAT
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 0
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvProSFG.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "FAT KG"
        repoNumBox.Name = ColProSFGFATKG
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 3
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvProSFG.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "SNF %"
        repoNumBox.Name = ColProSFGSNF
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 2
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvProSFG.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "SNF KG"
        repoNumBox.Name = ColProSFGSNFKG
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 3
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvProSFG.MasterTemplate.Columns.Add(repoNumBox)

        Dim ShowBtn As New GridViewCommandColumn()
        ShowBtn.FormatString = ""
        ShowBtn.UseDefaultText = True
        ShowBtn.DefaultText = "Add"
        ShowBtn.HeaderText = ""
        ShowBtn.Name = ColProSFGAdd
        ShowBtn.FieldName = ColProSFGAdd
        ShowBtn.Width = 80
        ShowBtn.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvProSFG.MasterTemplate.Columns.Add(ShowBtn)

        ShowBtn = New GridViewCommandColumn()
        ShowBtn.FormatString = ""
        ShowBtn.UseDefaultText = True
        ShowBtn.DefaultText = "Remove"
        ShowBtn.HeaderText = ""
        ShowBtn.Name = ColProSFGRemove
        ShowBtn.FieldName = ColProSFGRemove
        ShowBtn.Width = 80
        ShowBtn.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvProSFG.MasterTemplate.Columns.Add(ShowBtn)



        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Remarks"
        repoTextBox.Name = ColProSFGRemarks
        repoTextBox.Width = 150
        repoTextBox.ReadOnly = False
        gvProSFG.MasterTemplate.Columns.Add(repoTextBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Entered UOM"
        repoNumBox.Name = ColProSFGEnteredUOM
        repoNumBox.Width = 40
        repoNumBox.ReadOnly = True
        repoNumBox.IsVisible = False
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvProSFG.MasterTemplate.Columns.Add(repoNumBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "BOM Code"
        repoTextBox.Name = ColProSFGBOMCode
        repoTextBox.Width = 100
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = False
        gvProSFG.MasterTemplate.Columns.Add(repoTextBox)

        gvProSFG.AllowAddNewRow = False
        gvProSFG.ShowGroupPanel = False
        gvProSFG.AllowColumnReorder = True
        gvProSFG.AllowRowReorder = False
        gvProSFG.EnableSorting = False
        gvProSFG.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvProSFG.MasterTemplate.ShowRowHeaderColumn = False
        gvProSFG.TableElement.TableHeaderHeight = 40

        gvProSFG.AllowDeleteRow = True

        gvProSFG.Rows.AddNew()
        gvProSFG.MasterTemplate.SummaryRowsBottom.Clear()

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As New GridViewSummaryItem(ColProSFGQtyKG, "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        item1 = New GridViewSummaryItem(ColProSFGQtyLTR, "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        item1 = New GridViewSummaryItem(ColProSFGFATKG, "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        item1 = New GridViewSummaryItem(ColProSFGSNFKG, "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        gvProSFG.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gvProSFG.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub
    Sub LoadBlankGridProRM()
        gvProRM.Columns.Clear()
        gvProRM.DataSource = Nothing
        gvProRM.Rows.Clear()

        Dim repoTextBox As New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "PK ID"
        repoTextBox.Name = ColProRMPKID
        repoTextBox.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTextBox.Width = 200
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = False
        gvProRM.MasterTemplate.Columns.Add(repoTextBox)

        Dim repoNumBox As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "SNo"
        repoNumBox.Name = ColProRMSNo
        repoNumBox.Width = 40
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvProRM.MasterTemplate.Columns.Add(repoNumBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Item Code"
        repoTextBox.Name = ColProRMItemCode
        repoTextBox.HeaderImage = Global.XpertERPProcessProduction.My.Resources.Resources.search4
        repoTextBox.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTextBox.Width = 100
        repoTextBox.IsVisible = True
        repoTextBox.ReadOnly = True
        gvProRM.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Item"
        repoTextBox.Name = ColProRMItemName
        repoTextBox.Width = 150
        repoTextBox.ReadOnly = True
        gvProRM.MasterTemplate.Columns.Add(repoTextBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Qty"
        repoNumBox.Name = ColProRMQty
        repoNumBox.Width = 80
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 2
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvProRM.MasterTemplate.Columns.Add(repoNumBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "UOM"
        repoTextBox.Name = ColProRMUOM
        repoTextBox.Width = 100
        repoTextBox.IsVisible = True
        repoTextBox.ReadOnly = True
        gvProRM.MasterTemplate.Columns.Add(repoTextBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "FAT %"
        repoNumBox.Name = ColProRMFAT
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 0
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvProRM.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "FAT KG"
        repoNumBox.Name = ColProRMFATKG
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 3
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvProRM.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "SNF %"
        repoNumBox.Name = ColProRMSNF
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 2
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvProRM.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "SNF KG"
        repoNumBox.Name = ColProRMSNFKG
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 3
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvProRM.MasterTemplate.Columns.Add(repoNumBox)

        Dim ShowBtn As New GridViewCommandColumn()
        ShowBtn.FormatString = ""
        ShowBtn.UseDefaultText = True
        ShowBtn.DefaultText = "Issue Items"
        ShowBtn.HeaderText = ""
        ShowBtn.Name = ColProRMIssue
        ShowBtn.FieldName = ColProRMIssue
        ShowBtn.Width = 80
        ShowBtn.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvProRM.MasterTemplate.Columns.Add(ShowBtn)

        gvProRM.AllowAddNewRow = False
        gvProRM.ShowGroupPanel = False
        gvProRM.AllowColumnReorder = True
        gvProRM.AllowRowReorder = False
        gvProRM.EnableSorting = False
        gvProRM.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvProRM.MasterTemplate.ShowRowHeaderColumn = False
        gvProRM.TableElement.TableHeaderHeight = 40

        gvProRM.AllowDeleteRow = False

        gvProRM.MasterTemplate.SummaryRowsBottom.Clear()

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As New GridViewSummaryItem(ColProRMFATKG, "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        item1 = New GridViewSummaryItem(ColProRMSNFKG, "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        gvProRM.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gvProRM.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub
    Sub CalcuateProuctionRawMilk()
        LoadBlankGridProRM()
        For ii As Integer = 0 To gvProSFG.Rows.Count - 1
            Dim ArrRM As List(Of clsProductionShiftMgmtSFGProductionRM) = TryCast(gvProSFG.Rows(ii).Cells(ColProSFGBOMCode).Tag, List(Of clsProductionShiftMgmtSFGProductionRM))
            If ArrRM IsNot Nothing AndAlso ArrRM.Count > 0 Then
                For Each objtr As clsProductionShiftMgmtSFGProductionRM In ArrRM
                    Dim idx As Integer = -1
                    For jj As Integer = 0 To gvProRM.Rows.Count - 1
                        If clsCommon.CompairString(clsCommon.myCstr(gvProRM.Rows(jj).Cells(ColProRMItemCode).Value), objtr.Item_Code) = CompairStringResult.Equal Then
                            idx = jj
                        End If
                    Next
                    If idx < 0 Then
                        gvProRM.Rows.AddNew()
                        idx = gvProRM.Rows.Count - 1
                        gvProRM.Rows(idx).Cells(ColProRMSNo).Value = idx + 1
                        gvProRM.Rows(idx).Cells(ColProRMItemCode).Value = objtr.Item_Code
                        gvProRM.Rows(idx).Cells(ColProRMItemName).Value = objtr.Item_Name
                        gvProRM.Rows(idx).Cells(ColProRMUOM).Value = objtr.UOM
                        gvProRM.Rows(idx).Cells(ColProRMQty).Value = 0
                        gvProRM.Rows(idx).Cells(ColProRMFAT).Value = 0
                        gvProRM.Rows(idx).Cells(ColProRMSNF).Value = 0
                        gvProRM.Rows(idx).Cells(ColProRMFATKG).Value = 0
                        gvProRM.Rows(idx).Cells(ColProRMSNFKG).Value = 0
                    End If
                    gvProRM.Rows(idx).Cells(ColProRMQty).Value += objtr.Qty
                    gvProRM.Rows(idx).Cells(ColProRMFATKG).Value += objtr.FAT_KG
                    gvProRM.Rows(idx).Cells(ColProRMSNFKG).Value += objtr.SNF_KG

                    Dim dclKGQty As Decimal = clsItemMaster.Convert(clsCommon.myCstr(gvProRM.Rows(idx).Cells(ColProRMItemCode).Value), clsCommon.myCDecimal(gvProRM.Rows(idx).Cells(ColProRMQty).Value), clsCommon.myCstr(gvProRM.Rows(idx).Cells(ColProRMUOM).Value), "KG")
                    If dclKGQty > 0 Then
                        gvProRM.Rows(idx).Cells(ColProRMFAT).Value = clsCommon.myCDivide(clsCommon.myCDecimal(gvProRM.Rows(idx).Cells(ColProRMFATKG).Value) * 100, dclKGQty)
                        gvProRM.Rows(idx).Cells(ColProRMSNF).Value = clsCommon.myCDivide(clsCommon.myCDecimal(gvProRM.Rows(idx).Cells(ColProRMSNFKG).Value) * 100, dclKGQty)
                    End If
                Next
            End If
        Next
    End Sub
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub
    Private Function AllowToSave() As Boolean
        Return True
    End Function
    Private Sub FrmSerializeItemIn_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.Escape Then
            CancelPressed()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then
                Dim frm As New FrmPWD(Nothing)
                frm.strType = clsFixedParameterType.SIRC
                frm.strCode = clsFixedParameterCode.SIReversAndCreate
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnReverse.Visible = True
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
            End If
        End If
    End Sub
    Sub CancelPressed()
        Me.Close()
    End Sub
    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CancelPressed()
    End Sub
    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType)
        Try
            Dim qst As String = "select count(*) from TSPL_SHIFT_MGMT_SFG where Document_No='" + txtDocNo.Text + "'"
            Dim count As Integer = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue(qst))

            LoadData(txtDocNo.Text, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)
        Dim qry As String = "select TSPL_SHIFT_MGMT_SFG.Document_No,convert (varchar,TSPL_SHIFT_MGMT_SFG.Document_Date,103) as Document_Date,TSPL_SHIFT_MGMT_SFG.Remarks,TSPL_SHIFT_MGMT_SFG.Comment
,case when TSPL_SHIFT_MGMT_SFG.Status=1 then 'Posted' else 'Pending' end as Status
,TSPL_SHIFT_MGMT_SFG.Location_Code as [FG Location Code]  ,TSPL_LOCATION_MASTER_FG.Location_Desc as [Location Name]  
from TSPL_SHIFT_MGMT_SFG 
left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_FG on TSPL_LOCATION_MASTER_FG.Location_Code=TSPL_SHIFT_MGMT_SFG.Location_Code"
        LoadData(clsCommon.ShowSelectForm("PUFINDOC", qry, "Document_No", "", txtDocNo.Text, "Document_No", isButtonClicked, "Document_Date"), NavigatorType.Current)
    End Sub
    Public Sub SaveData()
        Try
            If (AllowToSave()) Then
                Dim obj As New clsProductionShiftMgmtSFG()
                obj.Document_No = txtDocNo.Text
                obj.Document_Date = txtDate.Value
                obj.Shift_Code = clsCommon.myCstr(txtShift.Value)
                obj.Shift_Start_Date = txtShiftStart.Value
                obj.Shift_End_Date = txtShiftEnd.Value
                obj.Location_Code = txtLocation.Value
                obj.ArrProSFG = New List(Of clsProductionShiftMgmtSFGProduction)

                For ii As Integer = 0 To gvProSFG.RowCount - 1
                    If clsCommon.myLen(gvProSFG.Rows(ii).Cells(ColProSFGItemCode).Value) > 0 Then
                        Dim objTr As New clsProductionShiftMgmtSFGProduction()
                        objTr.Item_Code = clsCommon.myCstr(gvProSFG.Rows(ii).Cells(ColProSFGItemCode).Value)
                        objTr.Location_code = clsCommon.myCstr(gvProSFG.Rows(ii).Cells(ColProSFGLocationCode).Value)
                        If clsCommon.myLen(objTr.Location_code) <= 0 Then
                            Throw New Exception("Please provide location for SFG item [" + objTr.Item_Code + "]")
                        End If
                        objTr.Batch_No = clsCommon.myCstr(gvProSFG.Rows(ii).Cells(ColProSFGBatchNo).Value)
                        objTr.Qty_KG = clsCommon.myCDecimal(gvProSFG.Rows(ii).Cells(ColProSFGQtyKG).Value)
                        objTr.Qty_LTR = clsCommon.myCDecimal(gvProSFG.Rows(ii).Cells(ColProSFGQtyLTR).Value)
                        objTr.FAT = clsCommon.myCDecimal(gvProSFG.Rows(ii).Cells(ColProSFGFAT).Value)
                        objTr.SNF = clsCommon.myCDecimal(gvProSFG.Rows(ii).Cells(ColProSFGSNF).Value)
                        objTr.FAT_KG = clsCommon.myCDecimal(gvProSFG.Rows(ii).Cells(ColProSFGFATKG).Value)
                        objTr.SNF_KG = clsCommon.myCDecimal(gvProSFG.Rows(ii).Cells(ColProSFGSNFKG).Value)
                        objTr.Remarks = clsCommon.myCstr(gvProSFG.Rows(ii).Cells(ColProSFGRemarks).Value)
                        objTr.BOM_Code = clsCommon.myCstr(gvProSFG.Rows(ii).Cells(ColProSFGBOMCode).Value)
                        objTr.Entered_UOM = clsCommon.myCstr(gvProSFG.Rows(ii).Cells(ColProSFGEnteredUOM).Value)

                        objTr.ArrRM = New List(Of clsProductionShiftMgmtSFGProductionRM)
                        objTr.ArrRM = TryCast(gvProSFG.Rows(ii).Cells(ColProSFGBOMCode).Tag, List(Of clsProductionShiftMgmtSFGProductionRM))
                        objTr.ArrAdd = New List(Of clsProductionShiftMgmtSFGProductionItemAddRemove)
                        objTr.ArrAdd = TryCast(gvProSFG.Rows(ii).Cells(ColProSFGAdd).Tag, List(Of clsProductionShiftMgmtSFGProductionItemAddRemove))
                        objTr.ArrRemove = New List(Of clsProductionShiftMgmtSFGProductionItemAddRemove)
                        objTr.ArrRemove = TryCast(gvProSFG.Rows(ii).Cells(ColProSFGRemove).Tag, List(Of clsProductionShiftMgmtSFGProductionItemAddRemove))
                        obj.ArrProSFG.Add(objTr)
                    End If
                Next

                obj.ArrProRMSummary = New List(Of clsProductionShiftMgmtSFGProductionRMSummary)
                For ii As Integer = 0 To gvProRM.RowCount - 1
                    If clsCommon.myLen(gvProRM.Rows(ii).Cells(ColProRMItemCode).Value) > 0 Then
                        Dim objTr As New clsProductionShiftMgmtSFGProductionRMSummary()
                        objTr.Item_Code = clsCommon.myCstr(gvProRM.Rows(ii).Cells(ColProRMItemCode).Value)
                        objTr.Qty = clsCommon.myCDecimal(gvProRM.Rows(ii).Cells(ColProRMQty).Value)
                        objTr.UOM = clsCommon.myCstr(gvProRM.Rows(ii).Cells(ColProRMUOM).Value)
                        objTr.FAT = clsCommon.myCDecimal(gvProRM.Rows(ii).Cells(ColProRMFAT).Value)
                        objTr.SNF = clsCommon.myCDecimal(gvProRM.Rows(ii).Cells(ColProRMSNF).Value)
                        objTr.FAT_KG = clsCommon.myCDecimal(gvProRM.Rows(ii).Cells(ColProRMFATKG).Value)
                        objTr.SNF_KG = clsCommon.myCDecimal(gvProRM.Rows(ii).Cells(ColProRMSNFKG).Value)

                        objTr.Arr = New List(Of clsProductionShiftMgmtSFGProductionRMIssue)
                        objTr.Arr = TryCast(gvProRM.Rows(ii).Cells(ColProRMIssue).Tag, List(Of clsProductionShiftMgmtSFGProductionRMIssue))
                        obj.ArrProRMSummary.Add(objTr)
                    End If
                Next

                If (obj.ArrProSFG Is Nothing OrElse obj.ArrProSFG.Count <= 0) Then
                    Throw New Exception("Please Fill at list one Produce Item")
                End If
                obj.SaveData(obj, isNewEntry)
                clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
                LoadData(obj.Document_No, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try

    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            isInsideLoadData = True
            LoadBlankGrid()
            LoadBlankGridProRM()
            Dim obj As clsProductionShiftMgmtSFG = clsProductionShiftMgmtSFG.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
                isNewEntry = False
                UsLock1.Status = obj.Status
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                Else
                    btnSave.Enabled = True
                    btnPost.Enabled = True
                    btnDelete.Enabled = True
                End If
                txtDocNo.Text = obj.Document_No
                txtDate.Value = obj.Document_Date
                txtShiftStart.Value = obj.Shift_Start_Date
                txtShiftEnd.Value = obj.Shift_End_Date
                txtShift.Value = obj.Shift_Code
                txtLocation.Value = obj.Location_Code


                If obj.ArrProSFG IsNot Nothing AndAlso obj.ArrProSFG.Count > 0 Then
                    For Each objTr As clsProductionShiftMgmtSFGProduction In obj.ArrProSFG
                        gvProSFG.Rows(gvProSFG.Rows.Count - 1).Cells(ColProSFGPKID).Value = objTr.PK_ID
                        gvProSFG.Rows(gvProSFG.Rows.Count - 1).Cells(ColProSFGSNo).Value = gvProSFG.Rows.Count
                        gvProSFG.Rows(gvProSFG.Rows.Count - 1).Cells(ColProSFGItemCode).Value = objTr.Item_Code
                        gvProSFG.Rows(gvProSFG.Rows.Count - 1).Cells(ColProSFGItemName).Value = objTr.Item_Name
                        gvProSFG.Rows(gvProSFG.Rows.Count - 1).Cells(ColProSFGLocationCode).Value = objTr.Location_code
                        gvProSFG.Rows(gvProSFG.Rows.Count - 1).Cells(ColProSFGLocationName).Value = objTr.Location_Name
                        gvProSFG.Rows(gvProSFG.Rows.Count - 1).Cells(ColProSFGBatchNo).Value = objTr.Batch_No
                        gvProSFG.Rows(gvProSFG.Rows.Count - 1).Cells(ColProSFGQtyKG).Value = objTr.Qty_KG
                        gvProSFG.Rows(gvProSFG.Rows.Count - 1).Cells(ColProSFGQtyLTR).Value = objTr.Qty_LTR
                        gvProSFG.Rows(gvProSFG.Rows.Count - 1).Cells(ColProSFGFAT).Value = objTr.FAT
                        gvProSFG.Rows(gvProSFG.Rows.Count - 1).Cells(ColProSFGSNF).Value = objTr.SNF
                        gvProSFG.Rows(gvProSFG.Rows.Count - 1).Cells(ColProSFGFATKG).Value = objTr.FAT_KG
                        gvProSFG.Rows(gvProSFG.Rows.Count - 1).Cells(ColProSFGSNFKG).Value = objTr.SNF_KG
                        gvProSFG.Rows(gvProSFG.Rows.Count - 1).Cells(ColProSFGRemarks).Value = objTr.Remarks
                        gvProSFG.Rows(gvProSFG.Rows.Count - 1).Cells(ColProSFGBOMCode).Value = objTr.BOM_Code
                        gvProSFG.Rows(gvProSFG.Rows.Count - 1).Cells(ColProSFGEnteredUOM).Value = objTr.Entered_UOM
                        gvProSFG.Rows(gvProSFG.Rows.Count - 1).Cells(ColProSFGBOMCode).Tag = objTr.ArrRM
                        gvProSFG.Rows(gvProSFG.Rows.Count - 1).Cells(ColProSFGAdd).Tag = objTr.ArrAdd
                        gvProSFG.Rows(gvProSFG.Rows.Count - 1).Cells(ColProSFGRemove).Tag = objTr.ArrRemove
                        gvProSFG.Rows.AddNew()
                    Next
                End If
                If obj.ArrProRMSummary IsNot Nothing AndAlso obj.ArrProRMSummary.Count > 0 Then
                    For Each objTr As clsProductionShiftMgmtSFGProductionRMSummary In obj.ArrProRMSummary
                        gvProRM.Rows.AddNew()
                        gvProRM.Rows(gvProRM.Rows.Count - 1).Cells(ColProRMPKID).Value = objTr.PK_ID
                        gvProRM.Rows(gvProRM.Rows.Count - 1).Cells(ColProRMSNo).Value = gvProRM.Rows.Count
                        gvProRM.Rows(gvProRM.Rows.Count - 1).Cells(ColProRMItemCode).Value = objTr.Item_Code
                        gvProRM.Rows(gvProRM.Rows.Count - 1).Cells(ColProRMItemName).Value = objTr.Item_Name
                        gvProRM.Rows(gvProRM.Rows.Count - 1).Cells(ColProRMQty).Value = objTr.Qty
                        gvProRM.Rows(gvProRM.Rows.Count - 1).Cells(ColProRMUOM).Value = objTr.UOM
                        gvProRM.Rows(gvProRM.Rows.Count - 1).Cells(ColProRMFAT).Value = objTr.FAT
                        gvProRM.Rows(gvProRM.Rows.Count - 1).Cells(ColProRMSNF).Value = objTr.SNF
                        gvProRM.Rows(gvProRM.Rows.Count - 1).Cells(ColProRMFATKG).Value = objTr.FAT_KG
                        gvProRM.Rows(gvProRM.Rows.Count - 1).Cells(ColProRMSNFKG).Value = objTr.SNF_KG
                        gvProRM.Rows(gvProRM.Rows.Count - 1).Cells(ColProRMIssue).Tag = objTr.Arr
                    Next
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If clsCommon.myLen(txtDocNo.Text) <= 0 Then
                Throw New Exception("Please select document no to delete")
            End If
            If clsCommon.MyMessageBoxShow("Delete the current document" + Environment.NewLine + "Are you sure ? ", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                clsProductionShiftMgmtSFG.DeleteData(txtDocNo.Text)
                clsCommon.MyMessageBoxShow(Me, "Data delete successfully", Me.Text)
                AddNew()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Try
            If clsCommon.myLen(txtDocNo.Text) <= 0 Then
                Throw New Exception("No document found to post")
            End If
            If clsCommon.MyMessageBoxShow("Post the Current Document [" + txtDocNo.Text + "]" + Environment.NewLine + "Are You Sure.", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then

                clsProductionShiftMgmtSFG.PostData(txtDocNo.Text)

                clsCommon.MyMessageBoxShow("Data posted successfully", Me.Text)
                LoadData(txtDocNo.Text, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtLocationFG__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
        If clsCommon.myLen(arrLoc) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "No location rights.", Me.Text)
            Exit Sub
        End If
        txtLocation.Value = clsLocation.getFinder(" tspl_location_master.IsMainPlant=1 and tspl_location_master.location_code in (" + arrLoc + ") and isnull(csa_type,'N')<>'Y' and isnull(Is_Section,'N')<>'Y'  and Location_Category<>'MCC'", txtLocation.Value, isButtonClicked)
        lblLocationFG.Text = clsLocation.GetName(txtLocation.Value, Nothing)
    End Sub
    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(txtDocNo.Text) <= 0 Then
                clsCommon.MyMessageBoxShow("Select Document No")
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowHistoryData(txtDocNo.Text, "Document_No", "TSPL_SHIFT_MGMT_SFG")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles btnReverse.Click
        Try
            If common.clsCommon.MyMessageBoxShow("Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If clsProductionShiftMgmtSFG.ReverseAndUnpost(txtDocNo.Text) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtDocNo.Text, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnShowInventory_Click(sender As Object, e As EventArgs) Handles btnShowInventory.Click
        clsOpenInventory.ShowInventoryDatails(txtDocNo.Text)
    End Sub
    Private Sub gvProRM_CommandCellClick(sender As Object, e As EventArgs) Handles gvProRM.CommandCellClick
        Try
            If gvProRM.CurrentColumn Is gvProRM.Columns(ColProRMIssue) Then
                If clsCommon.myLen(gvProRM.CurrentRow.Cells(ColProRMItemCode).Value) > 0 Then
                    Dim frm As New frmStockBalance
                    frm.isForSFG = True
                    frm.ArrRMIssue = Nothing
                    frm.ArrRMIssueSFG = TryCast(gvProRM.CurrentRow.Cells(ColProRMIssue).Tag, List(Of clsProductionShiftMgmtSFGProductionRMIssue))
                    frm.FilterItemCode = clsCommon.myCstr(gvProRM.CurrentRow.Cells(ColProRMItemCode).Value)
                    frm.FilterUOM = clsCommon.myCstr(gvProRM.CurrentRow.Cells(ColProRMUOM).Value)
                    frm.FilterReqQty = clsCommon.myCDecimal(gvProRM.CurrentRow.Cells(ColProRMQty).Value)
                    frm.FilterReqFATKg = clsCommon.myCDecimal(gvProRM.CurrentRow.Cells(ColProRMFATKG).Value)
                    frm.FilterReqSNFKg = clsCommon.myCDecimal(gvProRM.CurrentRow.Cells(ColProRMSNFKG).Value)
                    frm.FilterLocationCode = txtLocation.Value
                    frm.FilterDate = txtShiftEnd.Value
                    frm.WindowState = FormWindowState.Normal
                    frm.ShowDialog()
                    If frm.isOKClicked = 1 Then
                        gvProRM.CurrentRow.Cells(ColProRMIssue).Tag = frm.ArrRMIssueSFG
                    ElseIf frm.isOKClicked = 2 Then
                        gvProRM.CurrentRow.Cells(ColProRMIssue).Tag = Nothing
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtDate_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtDate.Validating
        SetShiftStartEndDateTime()
    End Sub
    Sub SetShiftStartEndDateTime()
        Try
            If Not isInsideLoadData Then
                txtShiftStart.Value = txtDate.Value
                txtShiftEnd.Value = clsShiftMaster.GetShiftTime(clsCommon.myCstr(txtShift.Value), txtDate.Value, txtShiftStart.Value)
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub gvProSFG_UserDeletingRow(sender As Object, e As GridViewRowCancelEventArgs) Handles gvProSFG.UserDeletingRow

    End Sub
    Private Sub gvProSFG_UserDeletedRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gvProSFG.UserDeletedRow
        RefeshSNOSFG()
    End Sub
    Sub RefeshSNOSFG()
        For ii As Integer = 1 To gvProSFG.Rows.Count
            gvProSFG.Rows(ii - 1).Cells(ColProSFGSNo).Value = ii
        Next
    End Sub
    Private Sub gvProSFG_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvProSFG.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gvProSFG.Columns(ColProSFGItemCode) Then
                        OpenSFGItem(False)
                    ElseIf e.Column Is gvProSFG.Columns(ColProSFGLocationCode) Then
                        OpenSFGLocation(False)
                    ElseIf e.Column Is gvProSFG.Columns(ColProSFGQtyLTR) Then
                        CalculateQtySFG(True)
                    ElseIf e.Column Is gvProSFG.Columns(ColProSFGQtyKG) Then
                        CalculateQtySFG(False)
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub OpenSFGLocation(ByVal isButtonClicked As Boolean)
        gvProSFG.CurrentRow.Cells(ColProSFGLocationCode).Value = clsLocation.getFinder(" isnull(TSPL_LOCATION_MASTER.csa_type,'N')<>'Y' and isnull(TSPL_LOCATION_MASTER.Is_Section,'N')<>'Y'  and TSPL_LOCATION_MASTER.Location_Category<>'MCC' and  isnull(TSPL_LOCATION_MASTER.Is_Jobwork,0)=0 and isnull(TSPL_LOCATION_MASTER.GIT_Type,'N')='N' ", clsCommon.myCstr(gvProSFG.CurrentRow.Cells(ColProSFGLocationCode).Value), isButtonClicked)
        gvProSFG.CurrentRow.Cells(ColProSFGLocationName).Value = clsLocation.GetName(clsCommon.myCstr(gvProSFG.CurrentRow.Cells(ColProSFGLocationCode).Value), Nothing)
    End Sub
    Sub OpenSFGItem(ByVal isButtonClicked As Boolean)
        gvProSFG.CurrentRow.Cells(ColProSFGItemCode).Value = clsItemMaster.getFinder(" tspl_item_master.item_type in ('S') and tspl_item_master.Active='1' ", clsCommon.myCstr(gvProSFG.CurrentRow.Cells(ColProSFGItemCode).Value), isButtonClicked)
        gvProSFG.CurrentRow.Cells(ColProSFGItemName).Value = clsItemMaster.GetItemName(clsCommon.myCstr(gvProSFG.CurrentRow.Cells(ColProSFGItemCode).Value), Nothing)
    End Sub
    Sub CalculateQtySFG(ByVal FromLTR As Boolean)
        If clsCommon.myLen(gvProSFG.CurrentRow.Cells(ColProSFGItemCode).Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please first select item.")
            Exit Sub
        End If
        Dim strColumn As String = ColProSFGQtyKG
        Dim strUOM As String = "KG"
        Try
            If FromLTR Then
                strColumn = ColProSFGQtyLTR
                strUOM = "LTR"
            End If
            Dim qry As String = "select 1 from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(gvProSFG.CurrentRow.Cells(ColProSFGItemCode).Value) + "' and UOM_Code='" + strUOM + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception(strUOM + " is invalid UOM for Item [" + clsCommon.myCstr(gvProSFG.CurrentRow.Cells(ColProSFGItemCode).Value) + "]")
            End If

            qry = "select top 1 BOM_CODE from TSPL_PP_BOM_HEAD 
where isnull(TSPL_PP_BOM_HEAD.is_osp,0)<>1 and TSPL_PP_BOM_HEAD.prod_item_code='" + clsCommon.myCstr(gvProSFG.CurrentRow.Cells(ColProSFGItemCode).Value) + "'  and '" + clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") + "' between cast(TSPL_PP_BOM_HEAD.Valid_FROM_DATE as date) and cast(TSPL_PP_BOM_HEAD.Valid_UPTO_Date as date) order by BOM_CODE desc"
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("BOM Not Found for Item [" + clsCommon.myCstr(gvProSFG.CurrentRow.Cells(ColProSFGItemCode).Value) + "] and Date [" + clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") + "]")
            End If
            gvProSFG.CurrentRow.Cells(ColProSFGBOMCode).Value = clsCommon.myCstr(dt.Rows(0)("BOM_CODE"))

            qry = "select xxx.ITEM_CODE,xxx.Item_Desc,xxx.Item_Type,TabStockUOM.UOM_Code as UNIT_CODE,xxx.Product_Type,(xxx.Qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as Qty,xxx.fat,xxx.snf,xxx.fat_kg,xxx.snf_kg from (
select xx.ITEM_CODE,xx.Item_Desc,xx.Item_Type,xx.UNIT_CODE,xx.Product_Type,(xx.prod_qty * (xx.quantity/xx.build_qty)) as Qty,xx.fat,xx.snf,(xx.fat_kg*xx.Prod_Qty/xx.build_qty) as fat_kg,(xx.snf_kg*xx.prod_qty/xx.build_qty) as snf_kg from (
select  (" + clsCommon.myCstr(clsCommon.myCDecimal(gvProSFG.CurrentRow.Cells(strColumn).Value)) + " * TabConvFatMul.Conversion_Factor/ TabConvFatDiv.Conversion_Factor) as Prod_Qty,tspl_pp_bom_head.bom_code,tspl_pp_bom_head.prod_item_code,tspl_pp_bom_head.prod_quantity as build_qty,TSPL_PP_BOM_ITEM_DETAIL.deactive,TSPL_PP_BOM_ITEM_DETAIL.effective_date
,TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Item_Type,TSPL_PP_BOM_ITEM_DETAIL.UNIT_CODE,TSPL_ITEM_MASTER.Product_Type
,(TSPL_PP_BOM_ITEM_DETAIL.QUANTITY+TSPL_PP_BOM_ITEM_DETAIL.QUANTITY*coalesce(TSPL_PP_BOM_ITEM_DETAIL.ProcessLossPer,0)/100) as QUANTITY
,(TSPL_PP_BOM_ITEM_DETAIL.FAT) as fat,(TSPL_PP_BOM_ITEM_DETAIL.SNF) as snf
,(TSPL_PP_BOM_ITEM_DETAIL.fat_kg+TSPL_PP_BOM_ITEM_DETAIL.fat_kg*coalesce(TSPL_PP_BOM_ITEM_DETAIL.ProcessLossPer,0)/100) as fat_kg
,(TSPL_PP_BOM_ITEM_DETAIL.snf_kg+TSPL_PP_BOM_ITEM_DETAIL.snf_kg*coalesce(TSPL_PP_BOM_ITEM_DETAIL.ProcessLossPer,0)/100) as snf_kg 
from   TSPL_PP_BOM_HEAD  
left outer join TSPL_PP_BOM_ITEM_DETAIL on TSPL_PP_BOM_ITEM_DETAIL.BOM_CODE=TSPL_PP_BOM_HEAD.BOM_CODE
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE 
left outer join TSPL_ITEM_UOM_DETAIL as  TabConvFatDiv on TabConvFatDiv.Item_Code=TSPL_PP_BOM_HEAD.PROD_ITEM_CODE and TabConvFatDiv.UOM_Code=TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE 
left outer join TSPL_ITEM_UOM_DETAIL as  TabConvFatMul on TabConvFatMul.item_code=TSPL_PP_BOM_HEAD.PROD_ITEM_CODE and TabConvFatMul.UOM_Code='" + strUOM + "'
where  TSPL_PP_BOM_HEAD.BOM_CODE='" + clsCommon.myCstr(gvProSFG.CurrentRow.Cells(ColProSFGBOMCode).Value) + "'
)xx  
)xxx 
left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xxx.ITEM_CODE and TSPL_ITEM_UOM_DETAIL.UOM_Code=xxx.UNIT_CODE 
left outer join TSPL_ITEM_UOM_DETAIL as TabStockUOM on TabStockUOM.item_code=xxx.ITEM_CODE and TabStockUOM.Stocking_Unit='Y'"
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Raw items for BOM [" + clsCommon.myCstr(gvProSFG.CurrentRow.Cells(ColProSFGBOMCode).Value) + "] not found")
            End If
            Dim arrRM As New List(Of clsProductionShiftMgmtSFGProductionRM)
            Dim dblFATKG As Decimal = 0
            Dim dblSNFKG As Decimal = 0
            For Each dr As DataRow In dt.Rows
                Dim obj As New clsProductionShiftMgmtSFGProductionRM
                obj.Item_Code = clsCommon.myCstr(dr("ITEM_CODE"))
                obj.Item_Name = clsCommon.myCstr(dr("Item_Desc"))
                obj.Qty = clsCommon.myCDecimal(dr("Qty"))
                obj.UOM = clsCommon.myCstr(dr("UNIT_CODE"))
                obj.FAT = clsCommon.myCDecimal(dr("fat"))
                obj.SNF = clsCommon.myCDecimal(dr("snf"))
                obj.FAT_KG = clsCommon.myCDecimal(dr("fat_kg"))
                obj.SNF_KG = clsCommon.myCDecimal(dr("snf_kg"))
                arrRM.Add(obj)

                dblFATKG += clsCommon.myCDecimal(dr("fat_kg"))
                dblSNFKG += clsCommon.myCDecimal(dr("snf_kg"))
            Next
            gvProSFG.CurrentRow.Cells(ColProSFGBOMCode).Tag = arrRM
            gvProSFG.CurrentRow.Cells(ColProSFGFATKG).Value = dblFATKG
            gvProSFG.CurrentRow.Cells(ColProSFGSNFKG).Value = dblSNFKG
            If FromLTR Then
                gvProSFG.CurrentRow.Cells(ColProSFGQtyKG).Value = clsItemMaster.Convert(clsCommon.myCstr(gvProSFG.CurrentRow.Cells(ColProSFGItemCode).Value), clsCommon.myCDecimal(gvProSFG.CurrentRow.Cells(ColProSFGQtyLTR).Value), "LTR", "KG")
            Else
                gvProSFG.CurrentRow.Cells(ColProSFGQtyLTR).Value = clsItemMaster.Convert(clsCommon.myCstr(gvProSFG.CurrentRow.Cells(ColProSFGItemCode).Value), clsCommon.myCDecimal(gvProSFG.CurrentRow.Cells(ColProSFGQtyKG).Value), "KG", "LTR")
            End If
            gvProSFG.CurrentRow.Cells(ColProSFGFAT).Value = clsCommon.myCDivide(dblFATKG * 100, clsCommon.myCDecimal(gvProSFG.CurrentRow.Cells(ColProSFGQtyKG).Value))
            gvProSFG.CurrentRow.Cells(ColProSFGSNF).Value = clsCommon.myCDivide(dblSNFKG * 100, clsCommon.myCDecimal(gvProSFG.CurrentRow.Cells(ColProSFGQtyKG).Value))
            If FromLTR Then
                gvProSFG.CurrentRow.Cells(ColProSFGEnteredUOM).Value = 1
            Else
                gvProSFG.CurrentRow.Cells(ColProSFGEnteredUOM).Value = 2
            End If
            CalcuateProuctionRawMilk()
        Catch ex As Exception
            gvProSFG.CurrentRow.Cells(ColProSFGQtyLTR).Value = 0
            gvProSFG.CurrentRow.Cells(ColProSFGQtyKG).Value = 0
            gvProSFG.CurrentRow.Cells(ColProSFGEnteredUOM).Value = 0
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub txtShift__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtShift._MYValidating
        Dim qry As String = " select  SHIFT_CODE as Code,SHIFT_NAME as Name from TSPL_SHIFT_MASTER"
        txtShift.Value = clsCommon.ShowSelectForm("Shift@shiftM", qry, "Code", "InActive=0", txtShift.Value, "Code", isButtonClicked)
        SetShiftStartEndDateTime()
    End Sub

    Private Sub gvProSFG_CommandCellClick(sender As Object, e As EventArgs) Handles gvProSFG.CommandCellClick
        Try
            If clsCommon.myLen(gvProSFG.CurrentRow.Cells(ColProSFGItemCode).Value) > 0 Then
                If gvProSFG.CurrentColumn Is gvProSFG.Columns(ColProSFGAdd) Then
                    Dim frm As New frmProductionShiftMgmtAdd()
                    frm.isSFG = True
                    frm.ArrSFG = TryCast(gvProSFG.CurrentRow.Cells(ColProSFGAdd).Tag, List(Of clsProductionShiftMgmtSFGProductionItemAddRemove))
                    frm.FilterDate = txtShiftEnd.Value
                    frm.FilterLocationCode = txtLocation.Value
                    frm.WindowState = FormWindowState.Normal
                    frm.ShowDialog()
                    If frm.isOKClicked = 1 Then
                        gvProSFG.CurrentRow.Cells(ColProSFGAdd).Tag = frm.ArrSFG
                    ElseIf frm.isOKClicked = 2 Then
                        gvProSFG.CurrentRow.Cells(ColProSFGAdd).Tag = Nothing
                    End If
                ElseIf gvProSFG.CurrentColumn Is gvProSFG.Columns(ColProSFGRemove) Then
                    Dim frm As New frmProductionShiftMgmtRemove()
                    frm.isSFG = True
                    frm.ArrSFG = TryCast(gvProSFG.CurrentRow.Cells(ColProSFGRemove).Tag, List(Of clsProductionShiftMgmtSFGProductionItemAddRemove))
                    frm.FilterDate = txtShiftEnd.Value
                    frm.FilterLocationCode = txtLocation.Value
                    frm.WindowState = FormWindowState.Normal
                    frm.ShowDialog()
                    If frm.isOKClicked = 1 Then
                        gvProSFG.CurrentRow.Cells(ColProSFGRemove).Tag = frm.ArrSFG
                    ElseIf frm.isOKClicked = 2 Then
                        gvProSFG.CurrentRow.Cells(ColProSFGRemove).Tag = Nothing
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
