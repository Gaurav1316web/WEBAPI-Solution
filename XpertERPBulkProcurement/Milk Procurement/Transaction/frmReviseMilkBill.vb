Imports System.Data.SqlClient
Imports System.IO
Imports common
Imports Telerik

Public Class frmReviseMilkBill
    Inherits FrmMainTranScreen
#Region "Variables"
    Private isInsideLoadData As Boolean = False
    Private isCellValueChangedOpen As Boolean = False
    Public Const colSNO As String = "colSNO"
    Public Const colDate As String = "colDate"
    Public Const colShift As String = "colShift"
    Public Const colQty As String = "colQty"
    Public Const colFATPer As String = "colFATPer"
    Public Const colSNFPer As String = "colSNFPer"
    Public Const colRate As String = "colRate"
    Public Const colPriceCode As String = "colPriceCode"
    Public Const colAmount As String = "colAmount"
    Public Const colDeductionCode As String = "colDeductionCode"
    Public Const colDeductionDesc As String = "colDeductionDesc"
    Public Const colDedAmt As String = "colDedAmt"
    Public Const colAdditionCode As String = "colAdditionCode"
    Public Const colAdditionDesc As String = "colAdditionDesc"
    Public Const colAddAmt As String = "colAddAmt"
    Dim colTextBox As GridViewTextBoxColumn = Nothing
    Dim colDecimal As GridViewDecimalColumn = Nothing
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = False
    Dim isLoad As Boolean = True
    Dim arrStrDedCode As List(Of String) = Nothing
    Dim arrStrAdditionCode As List(Of String) = Nothing
    Dim MaxFATPerLimit As Decimal = 0
    Dim MaxSNFPerLimit As Decimal = 0
    Dim MinFATPerLimit As Decimal = 0
    Dim MinSNFPerLimit As Decimal = 0

#End Region

    Private Sub FrmProvisionEntry_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        MaxFATPerLimit = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MaxFATPerLimit, clsFixedParameterCode.MaxFATPerLimit, Nothing))
        MaxSNFPerLimit = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MaxSNFPerLimit, clsFixedParameterCode.MaxSNFPerLimit, Nothing))
        MinFATPerLimit = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MinFATPerLimit, clsFixedParameterCode.MinFATPerLimit, Nothing))
        MinSNFPerLimit = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MinSNFPerLimit, clsFixedParameterCode.MinSNFPerLimit, Nothing))

        RadPageView1.SelectedPage = RadPageViewPage1
        Reset()
        RefreshSNo()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D To Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C To Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N For New")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P to Post the Transaction")
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub
    Sub LoadBlankGridgv()
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        gv1.DataSource = Nothing
        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "SNO"
        colTextBox.Name = colSNO
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(colTextBox)

        Dim repoDate As GridViewDateTimeColumn = New GridViewDateTimeColumn
        repoDate.HeaderText = "Date"
        repoDate.Name = colDate
        repoDate.Format = DateTimePickerFormat.Custom
        repoDate.CustomFormat = "dd/MM/yyyy"
        repoDate.FormatString = "{0:dd/MM/yyyy}"
        repoDate.Width = 150
        repoDate.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoDate)

        Dim repoShift As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoShift.FormatString = ""
        repoShift.HeaderText = "Shift"
        repoShift.Name = colShift
        repoShift.Width = 100
        repoShift.ReadOnly = False
        repoShift.IsVisible = True
        repoShift.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoShift.DataSource = LoadShift()
        repoShift.ValueMember = "Code"
        repoShift.DisplayMember = "Name"
        gv1.MasterTemplate.Columns.Add(repoShift)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Qty"
        colDecimal.Name = colQty
        colDecimal.Width = 100
        colDecimal.ReadOnly = False
        colDecimal.ShowUpDownButtons = False
        gv1.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "FAT %"
        colDecimal.Name = colFATPer
        colDecimal.Width = 100
        colDecimal.ReadOnly = False
        colDecimal.IsVisible = True
        colDecimal.ShowUpDownButtons = False
        gv1.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "SNF %"
        colDecimal.Name = colSNFPer
        colDecimal.Width = 100
        colDecimal.ShowUpDownButtons = False
        colDecimal.ReadOnly = False
        colDecimal.IsVisible = True
        gv1.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Rate"
        colDecimal.Name = colRate
        colDecimal.Width = 100
        colDecimal.ReadOnly = True
        colDecimal.ShowUpDownButtons = False
        colDecimal.IsVisible = True
        gv1.MasterTemplate.Columns.Add(colDecimal)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Price Code"
        colTextBox.Name = colPriceCode
        colTextBox.Width = 70
        colTextBox.ReadOnly = True
        colTextBox.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(colTextBox)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Amount"
        colDecimal.Name = colAmount
        colDecimal.Width = 150
        colDecimal.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(colDecimal)

        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = True
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = True
        gv1.EnableFiltering = True
        gv1.TableElement.TableHeaderHeight = 40
        '   gv1.BestFitColumns()
        gv1.MasterTemplate.AllowEditRow = True
    End Sub
    Public Shared Function LoadShift() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "M"
        dr("Name") = "Morning"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "E"
        dr("Name") = "Evening"
        dt.Rows.Add(dr)
        Return dt
    End Function
    Sub LoadBlankGridDeduction()
        gvDeduction.Columns.Clear()
        gvDeduction.Rows.Clear()
        gvDeduction.DataSource = Nothing
        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "SNO"
        colTextBox.Name = colSNO
        colTextBox.Width = 100
        colTextBox.ReadOnly = True
        gvDeduction.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Deduction"
        colTextBox.Name = colDeductionCode
        colTextBox.Width = 200
        colTextBox.ReadOnly = False
        colTextBox.HeaderImage = Global.XpertERPBulkProcurement.My.Resources.Resources.search4
        colTextBox.TextImageRelation = TextImageRelation.TextBeforeImage
        gvDeduction.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Deduction Description"
        colTextBox.Name = colDeductionDesc
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvDeduction.MasterTemplate.Columns.Add(colTextBox)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Amount"
        colDecimal.Name = colDedAmt
        colDecimal.Width = 150
        colDecimal.ReadOnly = False
        colDecimal.ShowUpDownButtons = False
        colDecimal.IsVisible = True
        gvDeduction.MasterTemplate.Columns.Add(colDecimal)

        gvDeduction.ShowGroupPanel = False
        gvDeduction.AllowAddNewRow = False
        gvDeduction.AllowColumnReorder = True
        gvDeduction.AllowRowReorder = False
        gvDeduction.EnableSorting = True
        gvDeduction.EnableFiltering = True
        gvDeduction.TableElement.TableHeaderHeight = 40
        'gvDeduction.BestFitColumns(BestFitColumnMode.AllCells)
    End Sub
    Sub LoadBlankGridAddition()
        gvAddition.Columns.Clear()
        gvAddition.Rows.Clear()
        gvAddition.DataSource = Nothing

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "SNO"
        colTextBox.Name = colSNO
        colTextBox.Width = 100
        colTextBox.ReadOnly = True
        gvAddition.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Addition"
        colTextBox.Name = colAdditionCode
        colTextBox.Width = 200
        colTextBox.ReadOnly = False
        colTextBox.HeaderImage = Global.XpertERPBulkProcurement.My.Resources.Resources.search4
        colTextBox.TextImageRelation = TextImageRelation.TextBeforeImage
        gvAddition.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Additions Description"
        colTextBox.Name = colAdditionDesc
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvAddition.MasterTemplate.Columns.Add(colTextBox)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Amount"
        colDecimal.Name = colAddAmt
        colDecimal.Width = 150
        colDecimal.ReadOnly = False
        colDecimal.ShowUpDownButtons = False
        gvAddition.MasterTemplate.Columns.Add(colDecimal)

        gvAddition.ShowGroupPanel = False
        gvAddition.AllowColumnReorder = True
        gvAddition.AllowAddNewRow = False
        gvAddition.AllowRowReorder = False
        gvAddition.EnableSorting = True
        gvAddition.EnableFiltering = True
        gvAddition.TableElement.TableHeaderHeight = 40
        '  gvAddition.BestFitColumns(BestFitColumnMode.AllCells)
    End Sub
    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        clsERPFuncationality.closeForm(Me)
    End Sub

    Sub Reset()
        isLoad = True
        isNewEntry = True
        arrStrAdditionCode = New List(Of String)
        arrStrDedCode = New List(Of String)
        txtDCS.Enabled = True
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
        txtToDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
        txtDocumentNo.Value = ""
        txtDocumentNo.MyReadOnly = False
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = False
        btnDelete.Enabled = False
        btnSendBillToDCS.Enabled = False
        lblPending.Status = ERPTransactionStatus.Pending
        txtDCS.Value = ""
        lblDCSName.Text = ""
        txtRemarks.Text = ""
        txtComment.Text = ""
        lblMCCCode.Text = ""
        lblMCCName.Text = ""
        LoadBlankGridDeduction()
        LoadBlankGridAddition()
        LoadBlankGridgv()
        gv1.Rows.AddNew()
        gvAddition.Rows.AddNew()
        gvDeduction.Rows.AddNew()
        RefreshSNo()
        txtToDate.Enabled = True
        txtToDate.ReadOnly = True
        txtFromDate.Enabled = True
        isLoad = False
    End Sub
    Private Sub SetUserMgmtNew()
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPost.Visible = MyBase.isPostFlag
        btnPrint.Visible = MyBase.isPrintFlag
        If MyBase.isPrintFlag = True Then
            btnPrint.Enabled = True
        Else
            btnPrint.Enabled = False
        End If
    End Sub

    Function AllowToSave() As Boolean
        Try
            RefreshSNo()
            If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
                txtDate.Focus()
                Return False
            End If
            If txtFromDate.Value > txtToDate.Value Then
                Throw New Exception("'From Date' can't be larger than 'To Date'")
            End If
            If clsCommon.GetDateWithEndTime(txtDate.Value) < clsCommon.GetDateWithStartTime(txtToDate.Value) Then
                Throw New Exception("Document date can't be less than to date")
            End If

            If clsCommon.myLen(txtDCS.Value) <= 0 Then
                Throw New Exception("Please select DCS")
                txtDCS.Focus()
            End If
            For ii As Integer = 0 To gv1.RowCount - 1
                If clsCommon.myLen(clsCommon.myCstr(gv1.Rows(ii).Cells(colDate).Value)) > 0 Then
                    If clsCommon.myCDate(gv1.Rows(ii).Cells(colDate).Value) < clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") OrElse clsCommon.myCDate(gv1.Rows(ii).Cells(colDate).Value) > clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") Then
                        Throw New Exception("Shift Date should be between From Date and To Date at line No " + clsCommon.myCstr(ii) + "")
                    End If
                    If clsCommon.myCdbl(gv1.Rows(ii).Cells(colFATPer).Value) < MinFATPerLimit OrElse clsCommon.myCdbl(gv1.Rows(ii).Cells(colFATPer).Value) > MaxFATPerLimit Then
                        Throw New Exception(" FAT Should be in Range [" + clsCommon.myCstr(MinFATPerLimit) + " - " + clsCommon.myCstr(MaxFATPerLimit) + "] at Line No " + clsCommon.myCstr(gv1.CurrentRow.Index) + "")
                    ElseIf clsCommon.myCdbl(gv1.Rows(ii).Cells(colSNFPer).Value) < MinSNFPerLimit OrElse clsCommon.myCdbl(gv1.Rows(ii).Cells(colSNFPer).Value) > MaxSNFPerLimit Then
                        Throw New Exception(" SNF Should be in Range [" + clsCommon.myCstr(MinSNFPerLimit) + " - " + clsCommon.myCstr(MaxSNFPerLimit) + "] at Line No " + clsCommon.myCstr(gv1.CurrentRow.Index) + "")
                    End If
                End If
                UpdateCurrentRow(ii)
            Next

            arrStrAdditionCode = New List(Of String)
            For ii As Integer = 0 To gvAddition.RowCount - 1
                If clsCommon.myLen(clsCommon.myCstr(gvAddition.Rows(ii).Cells(colAdditionCode).Value)) <= 0 AndAlso clsCommon.myLen(clsCommon.myCstr(gvAddition.Rows(ii).Cells(colAddAmt).Value)) > 0 Then
                    Throw New Exception("Addition Code couldn't be Blank at Line No " + clsCommon.myCstr(ii) + "")
                End If
                If clsCommon.myLen(clsCommon.myCstr(gvAddition.Rows(ii).Cells(colAdditionCode).Value)) > 0 Then
                    If arrStrAdditionCode.Count > 0 AndAlso arrStrAdditionCode IsNot Nothing Then
                        If arrStrAdditionCode.Contains(clsCommon.myCstr(gvAddition.Rows(ii).Cells(colAdditionCode).Value)) Then
                            Throw New Exception("Addition Code [" + clsCommon.myCstr(gvAddition.Rows(ii).Cells(colAdditionCode).Value) + "] is already exist ")
                        End If
                    End If
                    arrStrAdditionCode.Add(clsCommon.myCstr(gvAddition.Rows(ii).Cells(colAdditionCode).Value))
                End If
            Next

            arrStrDedCode = New List(Of String)
            For ii As Integer = 0 To gvDeduction.RowCount - 1
                If clsCommon.myLen(clsCommon.myCstr(gvDeduction.Rows(ii).Cells(colDeductionCode).Value)) <= 0 AndAlso clsCommon.myLen(clsCommon.myCstr(gvDeduction.Rows(ii).Cells(colDedAmt).Value)) > 0 Then
                    Throw New Exception("Deduction Code couldn't be Blank at Line No " + clsCommon.myCstr(ii) + "")
                End If
                If clsCommon.myLen(clsCommon.myCstr(gvDeduction.Rows(ii).Cells(colDeductionCode).Value)) > 0 Then
                    If arrStrDedCode.Count > 0 AndAlso arrStrDedCode IsNot Nothing Then
                        If arrStrDedCode.Contains(clsCommon.myCstr(gvDeduction.Rows(ii).Cells(colDeductionCode).Value)) Then
                            Throw New Exception("Deduction Code [" + clsCommon.myCstr(gvDeduction.Rows(ii).Cells(colDeductionCode).Value) + "] is already exist ")
                        End If
                    End If
                    arrStrDedCode.Add(clsCommon.myCstr(gvDeduction.Rows(ii).Cells(colDeductionCode).Value))
                End If
            Next
            UpdateAllTotals()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function

    Sub deleteData()
        Try
            If clsCommon.myLen(txtDocumentNo.Value) > 0 Then
                If (myMessages.deleteConfirm()) Then
                    If clsReviseMilkBill.deleteData(txtDocumentNo.Value) Then
                        clsCommon.MyMessageBoxShow(Me, "Deleted successFully", Me.Text)
                        Reset()
                    End If
                End If
            Else
                Throw New Exception("Doc No not Found to delete")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        deleteData()
    End Sub


    Function SaveData(ByVal ChekPostBtn As Boolean) As Boolean
        Dim i As Integer = 0
        Try
            If AllowToSave() Then
                Dim obj As clsReviseMilkBill = New clsReviseMilkBill()
                obj.Document_No = txtDocumentNo.Value
                obj.Document_Date = clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy")
                obj.VLC_Code = clsCommon.myCstr(txtDCS.Tag)
                obj.MCC_Code = lblMCCCode.Text
                obj.From_Date = clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy")
                obj.To_Date = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")
                obj.Remarks = txtRemarks.Text
                obj.Comments = txtComment.Text
                obj.Total_Milk_Amount = lblTotalMilkAmt.Text
                obj.Total_Addition_Amount = lblTotalAddAmt.Text
                obj.Total_Deduction_Amount = lblTotalDedAmt.Text
                obj.Payable_Amount = lblPayableAmt.Text

                If gvDeduction IsNot Nothing AndAlso gvDeduction.Rows.Count > 0 Then
                    obj.arrClsReviseMilkBillDeductions = New List(Of clsReviseMilkBillDeduction)
                    Dim objRevMilkBillDeduction As clsReviseMilkBillDeduction = Nothing
                    For i = 0 To gvDeduction.Rows.Count - 1
                        If clsCommon.myLen(clsCommon.myCstr(gvDeduction.Rows(i).Cells(colDeductionCode).Value)) > 0 Then
                            objRevMilkBillDeduction = New clsReviseMilkBillDeduction
                            objRevMilkBillDeduction.SNo = clsCommon.myCstr(gvDeduction.Rows(i).Cells(colSNO).Value)
                            objRevMilkBillDeduction.Ded_Code = clsCommon.myCstr(gvDeduction.Rows(i).Cells(colDeductionCode).Value)
                            objRevMilkBillDeduction.Ded_Desc = clsCommon.myCstr(gvDeduction.Rows(i).Cells(colDeductionDesc).Value)
                            objRevMilkBillDeduction.Amount = clsCommon.myCdbl(gvDeduction.Rows(i).Cells(colDedAmt).Value)
                            obj.arrClsReviseMilkBillDeductions.Add(objRevMilkBillDeduction)
                        End If
                    Next
                End If

                If gvAddition IsNot Nothing AndAlso gvAddition.Rows.Count > 0 Then
                    obj.arrClsReviseMilkBillAddition = New List(Of clsReviseMilkBillAddition)
                    Dim objRevMilkBillAddition As clsReviseMilkBillAddition = Nothing
                    For i = 0 To gvAddition.Rows.Count - 1
                        If clsCommon.myLen(clsCommon.myCstr(gvAddition.Rows(i).Cells(colAdditionCode).Value)) > 0 Then
                            objRevMilkBillAddition = New clsReviseMilkBillAddition
                            objRevMilkBillAddition.SNo = clsCommon.myCstr(gvAddition.Rows(i).Cells(colSNO).Value)
                            objRevMilkBillAddition.Addition_Code = clsCommon.myCstr(gvAddition.Rows(i).Cells(colAdditionCode).Value)
                            objRevMilkBillAddition.Addition_Desc = clsCommon.myCstr(gvAddition.Rows(i).Cells(colAdditionDesc).Value)
                            objRevMilkBillAddition.Amount = clsCommon.myCDecimal(gvAddition.Rows(i).Cells(colAddAmt).Value)
                            obj.arrClsReviseMilkBillAddition.Add(objRevMilkBillAddition)
                        End If
                    Next
                End If

                If gv1 IsNot Nothing AndAlso gv1.Rows.Count > 0 Then
                    obj.ArrReviseMilkBillDetail = New List(Of clsReviseMilkBillDetail)
                    Dim objReviseMilkBillDetail As clsReviseMilkBillDetail = Nothing
                    For i = 0 To gv1.Rows.Count - 1
                        If clsCommon.myLen(clsCommon.myCstr(gv1.Rows(i).Cells(colShift).Value)) > 0 Then
                            objReviseMilkBillDetail = New clsReviseMilkBillDetail()
                            objReviseMilkBillDetail.SNo = clsCommon.myCdbl(gv1.Rows(i).Cells(colSNO).Value)
                            objReviseMilkBillDetail.Shift_Date = clsCommon.myCDate(gv1.Rows(i).Cells(colDate).Value)
                            objReviseMilkBillDetail.Shift = clsCommon.myCstr(gv1.Rows(i).Cells(colShift).Value)
                            objReviseMilkBillDetail.Qty = clsCommon.myCDecimal(gv1.Rows(i).Cells(colQty).Value)
                            objReviseMilkBillDetail.FAT = clsCommon.myCDecimal(gv1.Rows(i).Cells(colFATPer).Value)
                            objReviseMilkBillDetail.SNF = clsCommon.myCDecimal(gv1.Rows(i).Cells(colSNFPer).Value)
                            objReviseMilkBillDetail.Rate = clsCommon.myCDecimal(gv1.Rows(i).Cells(colRate).Value)
                            objReviseMilkBillDetail.Price_Code = clsCommon.myCstr(gv1.Rows(i).Cells(colPriceCode).Value)
                            objReviseMilkBillDetail.Amount = clsCommon.myCDecimal(gv1.Rows(i).Cells(colAmount).Value)
                            obj.ArrReviseMilkBillDetail.Add(objReviseMilkBillDetail)
                        End If
                    Next
                End If

                If clsReviseMilkBill.SaveData(obj, isNewEntry) Then
                    If ChekPostBtn = False Then
                        clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    End If
                    LoadData(obj.Document_No, NavigatorType.Current)
                    Return True
                Else
                    Return False
                End If
            End If
        Catch ex As Exception
            If ChekPostBtn = False Then
                common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Else
                Throw New Exception(ex.Message)
            End If
        End Try
    End Function

    Sub LoadData(ByVal strCode As String, ByVal navType As NavigatorType)
        Try
            Dim obj As clsReviseMilkBill = clsReviseMilkBill.getData(strCode, navType, Nothing)
            If obj IsNot Nothing Then
                isInsideLoadData = True
                Reset()
                txtDCS.Enabled = False
                isNewEntry = False
                btnSave.Text = "Update"
                btnDelete.Enabled = True
                btnPost.Enabled = True
                txtDocumentNo.MyReadOnly = True
                txtDocumentNo.Value = obj.Document_No
                txtDate.Value = clsCommon.myCDate(obj.Document_Date)
                txtDCS.Value = obj.VLC_Code_VLC_Uploader
                txtDCS.Tag = obj.VLC_Code
                lblMCCCode.Text = obj.MCC_Code
                lblMCCName.Text = obj.MCC_NAME
                lblDCSName.Text = obj.VLC_Name
                txtFromDate.Value = obj.From_Date
                txtToDate.Value = obj.To_Date
                txtRemarks.Text = obj.Remarks
                txtComment.Text = obj.Comments
                lblTotalMilkAmt.Text = obj.Total_Milk_Amount
                lblTotalAddAmt.Text = obj.Total_Addition_Amount
                lblTotalDedAmt.Text = obj.Total_Deduction_Amount
                lblPayableAmt.Text = obj.Payable_Amount

                If obj.Status = 0 Then
                    btnSendBillToDCS.Enabled = False
                    lblPending.Status = ERPTransactionStatus.Pending
                Else
                    btnSendBillToDCS.Enabled = True
                    lblPending.Status = ERPTransactionStatus.Approved
                End If
                isLoad = True
                Dim i As Integer = 0


                If obj.ArrReviseMilkBillDetail IsNot Nothing AndAlso obj.ArrReviseMilkBillDetail.Count > 0 Then
                    gv1.Rows.Clear()
                    For Each objReviseMilkBillDetail As clsReviseMilkBillDetail In obj.ArrReviseMilkBillDetail
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSNO).Value = objReviseMilkBillDetail.SNo
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDate).Value = objReviseMilkBillDetail.Shift_Date
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colShift).Value = objReviseMilkBillDetail.Shift
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objReviseMilkBillDetail.Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFATPer).Value = objReviseMilkBillDetail.FAT
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFPer).Value = objReviseMilkBillDetail.SNF
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objReviseMilkBillDetail.Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceCode).Value = objReviseMilkBillDetail.Price_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmount).Value = objReviseMilkBillDetail.Amount
                    Next
                Else
                    gv1.Rows.AddNew()
                End If

                If obj.arrClsReviseMilkBillDeductions IsNot Nothing AndAlso obj.arrClsReviseMilkBillDeductions.Count > 0 Then
                    gvDeduction.Rows.Clear()
                    For Each objReviseMilkBillDeduction As clsReviseMilkBillDeduction In obj.arrClsReviseMilkBillDeductions
                        gvDeduction.Rows.AddNew()
                        gvDeduction.Rows(gvDeduction.Rows.Count - 1).Cells(colSNO).Value = objReviseMilkBillDeduction.SNo
                        gvDeduction.Rows(gvDeduction.Rows.Count - 1).Cells(colDeductionCode).Value = objReviseMilkBillDeduction.Ded_Code
                        gvDeduction.Rows(gvDeduction.Rows.Count - 1).Cells(colDeductionDesc).Value = objReviseMilkBillDeduction.Ded_Desc
                        gvDeduction.Rows(gvDeduction.Rows.Count - 1).Cells(colDedAmt).Value = objReviseMilkBillDeduction.Amount
                    Next
                Else
                    gvDeduction.Rows.AddNew()
                End If

                If obj.arrClsReviseMilkBillAddition IsNot Nothing AndAlso obj.arrClsReviseMilkBillAddition.Count > 0 Then
                    gvAddition.Rows.Clear()
                    For Each objReviseMilkBillAddition As clsReviseMilkBillAddition In obj.arrClsReviseMilkBillAddition
                        gvAddition.Rows.AddNew()
                        gvAddition.Rows(gvAddition.Rows.Count - 1).Cells(colSNO).Value = objReviseMilkBillAddition.SNo
                        gvAddition.Rows(gvAddition.Rows.Count - 1).Cells(colAdditionCode).Value = objReviseMilkBillAddition.Addition_Code
                        gvAddition.Rows(gvAddition.Rows.Count - 1).Cells(colAdditionDesc).Value = objReviseMilkBillAddition.Addition_Desc
                        gvAddition.Rows(gvAddition.Rows.Count - 1).Cells(colAddAmt).Value = objReviseMilkBillAddition.Amount
                    Next
                Else
                    gvAddition.Rows.AddNew()
                End If
                btnPost.Visible = MyBase.isPostFlag
                If Not clsApply_Approval.Visibility_PostButtonForApproval(txtDCS.Value, "", MyBase.Form_ID, clsCommon.myCstr(txtDocumentNo.Value), 0, 0, "") Then
                    btnPost.Visible = True
                End If

                isInsideLoadData = False
            Else
                Reset()
            End If
        Catch ex As Exception
            isInsideLoadData = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Sub AddSummary()
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        gv1.SummaryRowsBottom.Clear()

        For iii As Integer = 0 To gv1.Columns.Count - 1
            If TypeOf (gv1.Columns(iii)) Is GridViewDecimalColumn Then
                summaryRowItem.Add(New GridViewSummaryItem(gv1.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
            End If
        Next

        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        Dim summaryRowItemInvoice As New GridViewSummaryRowItem()
        Dim summaryRowDeduction As New GridViewSummaryRowItem()
        gvDeduction.SummaryRowsBottom.Clear()
        For iii As Integer = 0 To gvDeduction.Columns.Count - 1
            If TypeOf (gvDeduction.Columns(iii)) Is GridViewDecimalColumn Then
                summaryRowDeduction.Add(New GridViewSummaryItem(gvDeduction.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
            End If
        Next
        gvDeduction.MasterTemplate.SummaryRowsBottom.Add(summaryRowDeduction)
        gvDeduction.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        Dim summaryRowCreditNote As New GridViewSummaryRowItem()
        gvAddition.SummaryRowsBottom.Clear()
        For iii As Integer = 0 To gvAddition.Columns.Count - 1
            If TypeOf (gvAddition.Columns(iii)) Is GridViewDecimalColumn Then
                summaryRowCreditNote.Add(New GridViewSummaryItem(gvAddition.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
            End If
        Next
        gvAddition.MasterTemplate.SummaryRowsBottom.Add(summaryRowCreditNote)
        gvAddition.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub

    Private Sub txtDocumentNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocumentNo._MYNavigator
        LoadData(txtDocumentNo.Value, NavType)
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData(False)
    End Sub
    Function isItemFoundInList(ByVal arrStr As List(Of String), ByVal str As String) As Boolean

        If arrStr Is Nothing OrElse arrStr.Count <= 0 Then
            Return False
        Else
            For i As Integer = 0 To arrStr.Count - 1
                If clsCommon.CompairString(arrStr.Item(i), str) = CompairStringResult.Equal Then
                    Return True
                End If
            Next
        End If

        Return False
    End Function

    Function isDedFoundInList(ByVal arrStr As List(Of String), ByVal str As String) As Boolean

        If arrStr Is Nothing OrElse arrStr.Count <= 0 Then
            Return False
        Else
            For i As Integer = 0 To arrStr.Count - 1
                If clsCommon.CompairString(arrStr.Item(i), str) = CompairStringResult.Equal Then
                    Return True
                End If
            Next
        End If

        Return False
    End Function
    Private Sub txtDCS__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDCS._MYValidating
        Try
            Dim qry As String = " select  VLC_Code_VLC_Uploader as DCSUploaderCode ,VLC_Code as [DCS Code],VLC_Name as [DCS Name],TSPL_VLC_MASTER_HEAD.MCC AS [MCC Code],TSPL_MCC_MASTER.MCC_NAME as [MCC Name] from TSPL_VLC_MASTER_HEAD LEFT OUTER JOIN TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code = TSPL_VLC_MASTER_HEAD.MCC  "
            Dim whrCls As String = " 1=1 AND TSPL_VLC_MASTER_HEAD.Active=1"
            txtDCS.Value = clsCommon.ShowSelectForm(MyBase.Form_ID, qry, "DCSUploaderCode", whrCls, txtDCS.Value, "DCSUploaderCode", isButtonClicked)
            If clsCommon.myLen(txtDCS.Value) > 0 Then
                qry += " where VLC_Code_VLC_Uploader = '" + txtDCS.Value + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    lblDCSName.Text = clsCommon.myCstr(dt.Rows(0)("DCS Name"))
                    txtDCS.Tag = clsCommon.myCstr(dt.Rows(0)("DCS Code"))
                    lblMCCCode.Text = clsCommon.myCstr(dt.Rows(0)("MCC Code"))
                    lblMCCName.Text = clsCommon.myCstr(dt.Rows(0)("MCC Name"))
                    If clsCommon.myLen(GetDocumentNo()) > 0 Then
                        LoadData(GetDocumentNo(), NavigatorType.Current)
                    End If
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Function GetDocumentNo() As String
        Dim whrcls As String = ""
        If clsCommon.myLen(txtDCS.Value) > 0 Then
            whrcls = " and VLC_Code='" + txtDCS.Tag + "'"
        End If
        Dim DocumentNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_No from TSPL_MILK_BILL_REVISE where 2=2 and convert(date,From_Date,103)= '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and convert(date,To_Date,103)=  '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' " & whrcls & ""))
        Return DocumentNo
    End Function
    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Sub PostData()
        Try
            Dim msg As String = ""
            Dim qry As String = ""
            Dim dt As DataTable = Nothing
            If (myMessages.postConfirm()) Then
                If SaveData(True) Then
                    If (clsReviseMilkBill.PostData(MyBase.Form_ID, txtDocumentNo.Value)) Then
                        clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                        LoadData(txtDocumentNo.Value, NavigatorType.Current)
                    End If

                    'If (common.clsCommon.MyMessageBoxShow("Do you want to print", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes) Then
                    '    clsMCCMaterialSale.funPrint(MyBase.Form_ID, False, txtDate.Value, txtDocumentNo.Value)
                    '    'funPrint()
                    'End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocumentNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtDocumentNo._MYValidating
        txtDocumentNo.Value = clsReviseMilkBill.getFinder("", txtDocumentNo.Value, isButtonClicked)
        If clsCommon.myLen(txtDocumentNo.Value) > 0 Then
            LoadData(txtDocumentNo.Value, NavigatorType.Current)
        End If
    End Sub

    Private Sub txtFromDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFromDate.ValueChanged
    End Sub
    Private Sub gvDeduction_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvDeduction.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gvDeduction.Columns(colDeductionCode) Then
                        OpenDeductionCode(False)
                    End If
                    UpdateAllTotals()
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            isCellValueChangedOpen = False
        End Try
    End Sub
    Sub OpenDeductionCode(ByVal isButtonClick As Boolean)
        Dim qry As String
        Dim whrcls As String = ""
        If arrStrDedCode.Count > 0 AndAlso arrStrDedCode IsNot Nothing Then
            whrcls = "  xx.Code not in (" + clsCommon.GetMulcallString(arrStrDedCode) + ")"
        End If
        qry = "select distinct Code,Description from ( select code,Description from TSPL_DCS_ADDITION_DEDUCTION  Where 2=2 and Nature_Type=1 " & Environment.NewLine & " union all " & Environment.NewLine & "
        select Code,Description from TSPL_DEDUCTION_MASTER where Ded_Grp_Code = 'DEDUCTION' )xx"
        gvDeduction.CurrentRow.Cells(colDeductionCode).Value = clsCommon.ShowSelectForm("DedCodeFnd", qry, "Code", whrcls, clsCommon.myCstr(gvDeduction.CurrentRow.Cells(colDeductionCode).Value), "Code", isButtonClick)
        If clsCommon.myLen(clsCommon.myCstr(gvDeduction.CurrentRow.Cells(colDeductionCode).Value)) > 0 Then
            qry += " where xx.Code='" & clsCommon.myCstr(gvDeduction.CurrentRow.Cells(colDeductionCode).Value) & "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                arrStrDedCode.Add(clsCommon.myCstr(dt.Rows(0)("Code")))
                gvDeduction.CurrentRow.Cells(colDeductionDesc).Value = clsCommon.myCstr(dt.Rows(0)("Description"))
            End If
        End If
    End Sub
    Private Sub gvAddition_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvAddition.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gvAddition.Columns(colAdditionCode) Then
                        OpenAdditionCode(False)
                    End If
                    UpdateAllTotals()
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            isCellValueChangedOpen = False
        End Try
    End Sub
    Sub OpenAdditionCode(ByVal isButtonClick As Boolean)
        Dim qry As String
        Dim whrcls As String = ""
        If arrStrAdditionCode.Count > 0 AndAlso arrStrAdditionCode IsNot Nothing Then
            whrcls = "  xx.Code not in (" + clsCommon.GetMulcallString(arrStrAdditionCode) + ")"
        End If
        qry = " select distinct Code,Description from ( select code,Description from TSPL_DCS_ADDITION_DEDUCTION  Where 2=2 and Nature_Type=0  " & Environment.NewLine & " union all " & Environment.NewLine & "
        select Code,Description from TSPL_DEDUCTION_MASTER where Ded_Grp_Code = 'ADDITION' )xx "
        gvAddition.CurrentRow.Cells(colAdditionCode).Value = clsCommon.ShowSelectForm("AddCodeFnd", qry, "Code", whrcls, clsCommon.myCstr(gvAddition.CurrentRow.Cells(colAdditionCode).Value), "Code", isButtonClick)
        If clsCommon.myLen(clsCommon.myCstr(gvAddition.CurrentRow.Cells(colAdditionCode).Value)) > 0 Then
            qry += " where xx.Code='" & clsCommon.myCstr(gvAddition.CurrentRow.Cells(colAdditionCode).Value) & "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                arrStrAdditionCode.Add(clsCommon.myCstr(dt.Rows(0)("Code")))
                gvAddition.CurrentRow.Cells(colAdditionDesc).Value = clsCommon.myCstr(dt.Rows(0)("Description"))
            End If
        End If
    End Sub
    Private Sub gv1_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If (e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colFATPer) OrElse e.Column Is gv1.Columns(colSNFPer)) AndAlso gv1 IsNot Nothing AndAlso gv1.Rows.Count > 0 Then
                        If e.Column Is gv1.Columns(colFATPer) Then
                            If MaxFATPerLimit > 0 Then
                                If clsCommon.myCdbl(gv1.CurrentRow.Cells(colFATPer).Value) > MaxFATPerLimit Then
                                    clsCommon.MyMessageBoxShow(Me, "FAT % Can't be more than " + clsCommon.myCstr(MaxFATPerLimit) + " at Line No " + clsCommon.myCstr(gv1.CurrentRow.Index) + "", Me.Text)
                                    gv1.CurrentRow.Cells(colFATPer).Value = MaxSNFPerLimit
                                End If
                            End If
                        End If
                        If e.Column Is gv1.Columns(colSNFPer) Then
                            If MaxSNFPerLimit > 0 Then
                                If clsCommon.myCdbl(gv1.CurrentRow.Cells(colSNFPer).Value) > MaxSNFPerLimit Then
                                    clsCommon.MyMessageBoxShow(Me, "SNF % Can't be more than " + clsCommon.myCstr(MaxSNFPerLimit) + " at Line No " + clsCommon.myCstr(gv1.CurrentRow.Index) + "", Me.Text)
                                    gv1.CurrentRow.Cells(colSNFPer).Value = MaxSNFPerLimit
                                End If
                            End If
                        End If
                        For ii As Integer = 0 To gv1.Rows.Count - 1
                            UpdateCurrentRow(ii)
                        Next
                        UpdateAllTotals()
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            isCellValueChangedOpen = False
        End Try
    End Sub

    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)
        Try
            If clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value) > 0 Then
                Dim dblQty As Decimal = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value)
                Dim dblFATPer As Decimal = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colFATPer).Value)
                Dim dblSNFPer As Decimal = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colSNFPer).Value)
                Dim dblRate As Decimal = clsEkoPro.getRateAndPriceCodeFromUploaderShiftWise(dblQty, gv1.Rows(IntRowNo).Cells(colPriceCode).Value, dblFATPer, dblSNFPer, lblMCCCode.Text, txtDCS.Tag, clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colShift).Value), clsCommon.myCDate(gv1.Rows(IntRowNo).Cells(colDate).Value), Nothing, "")
                gv1.Rows(IntRowNo).Cells(colRate).Value = dblRate
                Dim dblAmt As Decimal = (dblQty * dblRate)
                Dim dblTotalMilkAmt As Decimal = 0
                gv1.Rows(IntRowNo).Cells(colAmount).Value = dblAmt
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub UpdateAllTotals()
        Dim dblTotalMilkAmt As Decimal = 0
        Dim dblTotalDedAmt As Decimal = 0
        Dim dblTotalAddAmt As Decimal = 0
        Dim dblPayableAmt As Decimal = 0
        For ii As Integer = 0 To gv1.Rows.Count - 1
            If (clsCommon.myLen(gv1.Rows(ii).Cells(colShift).Value) > 0) Then
                dblTotalMilkAmt = dblTotalMilkAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmount).Value)
            End If
        Next
        For ii As Integer = 0 To gvDeduction.Rows.Count - 1
            If (clsCommon.myLen(gvDeduction.Rows(ii).Cells(colDeductionCode).Value) > 0) Then
                dblTotalDedAmt = dblTotalDedAmt + clsCommon.myCdbl(gvDeduction.Rows(ii).Cells(colDedAmt).Value)
            End If
        Next

        For ii As Integer = 0 To gvAddition.Rows.Count - 1
            If (clsCommon.myLen(gvAddition.Rows(ii).Cells(colAdditionCode).Value) > 0) Then
                dblTotalAddAmt = dblTotalAddAmt + clsCommon.myCdbl(gvAddition.Rows(ii).Cells(colAddAmt).Value)
            End If
        Next
        dblPayableAmt = dblTotalMilkAmt + dblTotalAddAmt - dblTotalDedAmt
        lblTotalMilkAmt.Text = clsCommon.myFormat(dblTotalMilkAmt)
        lblTotalAddAmt.Text = clsCommon.myFormat(dblTotalAddAmt)
        lblTotalDedAmt.Text = clsCommon.myFormat(dblTotalDedAmt)
        lblPayableAmt.Text = clsCommon.myFormat(dblPayableAmt)

    End Sub

    Private Sub gv1_KeyDown(sender As Object, e As KeyEventArgs) Handles gv1.KeyDown
        If e.KeyCode = Keys.Enter Then
            gv1.BeginEdit()
        End If
        If e.Control AndAlso e.KeyCode = Keys.V Then
            e.Handled = True
        End If
    End Sub
    Private Sub gv1_KeyUp(sender As Object, e As KeyEventArgs) Handles gv1.KeyUp
        If e.KeyCode = Keys.Home Then
            If gv1.Rows.Count = gv1.CurrentRow.Index + 1 Then
                gv1.Rows.AddNew()
            End If
            gv1.CurrentRow = gv1.Rows(gv1.CurrentRow.Index + 1)
            gv1.CurrentColumn = gv1.Columns(colDate)
        End If
    End Sub

    Private Sub gvAddition_KeyDown(sender As Object, e As KeyEventArgs) Handles gvAddition.KeyDown
        If e.KeyCode = Keys.Enter Then
            gvAddition.BeginEdit()
        End If
        If e.Control AndAlso e.KeyCode = Keys.V Then
            e.Handled = True
        End If
    End Sub
    Private Sub gvAddition_KeyUp(sender As Object, e As KeyEventArgs) Handles gvAddition.KeyUp
        If e.KeyCode = Keys.Home Then
            If gvAddition.Rows.Count = gvAddition.CurrentRow.Index + 1 Then
                gvAddition.Rows.AddNew()
            End If
            gvAddition.CurrentRow = gvAddition.Rows(gvAddition.CurrentRow.Index + 1)
            gvAddition.CurrentColumn = gvAddition.Columns(colAdditionCode)
        End If
    End Sub

    Private Sub gvDeduction_KeyDown(sender As Object, e As KeyEventArgs) Handles gvDeduction.KeyDown
        If e.KeyCode = Keys.Enter Then
            gvDeduction.BeginEdit()
        End If
        If e.Control AndAlso e.KeyCode = Keys.V Then
            e.Handled = True
        End If
    End Sub
    Private Sub gvDeduction_KeyUp(sender As Object, e As KeyEventArgs) Handles gvDeduction.KeyUp
        If e.KeyCode = Keys.Home Then
            If gvDeduction.Rows.Count = gvDeduction.CurrentRow.Index + 1 Then
                gvDeduction.Rows.AddNew()
            End If
            gvDeduction.CurrentRow = gvDeduction.Rows(gvDeduction.CurrentRow.Index + 1)
            gvDeduction.CurrentColumn = gvDeduction.Columns(colDate)
        End If
    End Sub
    Sub SetToDate()
        If Not isLoad Then
            Dim PaymentCycleType As String = ""
            Dim PaymentCycleValue As Integer = 0
            If clsCommon.myLen(txtDCS.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select the DCS first", Me.Text)
                Exit Sub
            End If

            Dim strDCScode = ""
            If clsCommon.myLen(txtDCS.Value) > 0 Then
                strDCScode = " VLC_Code_VLC_Uploader = '" + txtDCS.Value + "'"
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select TSPL_MCC_MASTER.Payment_Cycle,TSPL_PAYMENT_CYCLE_MASTER.PC_TYPE,TSPL_PAYMENT_CYCLE_MASTER.PC_VALUE  from TSPL_MCC_MASTER left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.MCC = TSPL_MCC_MASTER.MCC_Code  left outer join TSPL_PAYMENT_CYCLE_MASTER on TSPL_PAYMENT_CYCLE_MASTER.PC_CODE=TSPL_MCC_MASTER.Payment_Cycle   where TSPL_MCC_MASTER.MCC_Code  in (select Location_Code  from TSPL_LOCATION_MASTER where " + strDCScode + " and Location_Category='MCC' and Rejected_Type='N') ")
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Payment Cycle found on current DCS", Me.Text)
                Exit Sub
            End If
            PaymentCycleType = clsCommon.myCstr(dt.Rows(0)("PC_TYPE"))
            PaymentCycleValue = clsCommon.myCdbl(dt.Rows(0)("PC_VALUE"))
            Dim dtCurr As DateTime = clsCommon.GETSERVERDATE()
            If clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal Then
                If txtFromDate.Value.Day Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                    clsCommon.MyMessageBoxShow("Date can only be first day of month or at interval of " & PaymentCycleValue & " Day, Because DCS has payment Cycle of " & PaymentCycleValue & " Day ")
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
                    clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month, Because DCS has payment Cycle of Month Type")
                    txtFromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    txtToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    Exit Sub
                End If
                txtToDate.Value = DateAdd(DateInterval.Month, PaymentCycleValue, txtFromDate.Value)
            ElseIf clsCommon.CompairString(PaymentCycleType, "Year") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.GetPrintDate(txtFromDate.Value, "dd")) <> 1 Then
                    clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month, Because DCS has payment Cycle of Year Type")
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
    End Sub

    Private Sub txtFromDate_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtFromDate.Validating
        SetToDate()
        If clsCommon.myLen(GetDocumentNo()) > 0 Then
            LoadData(GetDocumentNo(), NavigatorType.Current)
        End If
    End Sub

    Private Sub txtFromDate_Leave(sender As Object, e As EventArgs) Handles txtFromDate.Leave
        SetToDate()
        If clsCommon.myLen(GetDocumentNo()) > 0 Then
            LoadData(GetDocumentNo(), NavigatorType.Current)
        End If
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            funPrint(False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Function funPrint(GetPDFPath As Boolean)
        Dim PDFPath As String = Nothing
        Try
            If clsCommon.myLen(txtDocumentNo.Value) <= 0 Then
                Throw New Exception("Please select Document No")
                Exit Function
            End If
            Dim whrcls As String = " and TSPL_MILK_BILL_REVISE.Document_No='" + txtDocumentNo.Value + "'"
            Dim qry As String = "select TSPL_MILK_BILL_REVISE.Payable_Amount, 'SWEET' as QBD,  replace(replace(TSPL_COMPANY_MASTER.Phone1 ,'(+__)__________',''),'(+91)','') as Comp_Phone1,replace(replace(TSPL_COMPANY_MASTER.Phone2 ,'(+__)__________',''),'(+91)','') as Comp_Phone2,TSPL_COMPANY_MASTER.Regn_No,TSPL_MILK_BILL_REVISE_DETAIL.Amount,round(TSPL_MILK_BILL_REVISE_DETAIL.FAT*TSPL_MILK_BILL_REVISE_DETAIL.Qty/100,3,1 ) as FATQTY,round(TSPL_MILK_BILL_REVISE_DETAIL.SNF *TSPL_MILK_BILL_REVISE_DETAIL.Qty/100,3,1 ) as SNFQTY ,
            TSPL_MILK_BILL_REVISE_DETAIL.Rate,TSPL_MILK_BILL_REVISE_DETAIL.Amount AS Net_AMOUNT,TSPL_MILK_BILL_REVISE_DETAIL.QTY,TSPL_COMPANY_MASTER.Comp_Name AS CompName,TSPL_COMPANY_MASTER.Logo_Img   as compLogo1,convert(varchar, TSPL_MILK_BILL_REVISE_DETAIL.Shift_Date,103) as DOC_DATE  ,TSPL_MILK_BILL_REVISE_DETAIL.Shift,TSPL_MILK_BILL_REVISE_DETAIL.FAT as FAT_PER,TSPL_MILK_BILL_REVISE_DETAIL.SNF as SNF_PER,TSPL_MILK_BILL_REVISE.VLC_Code,VSP_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,
            TSPL_VENDOR_MASTER.Vendor_Name,convert(varchar,TSPL_MILK_BILL_REVISE.From_Date,103) FromDate ,convert(varchar,TSPL_MILK_BILL_REVISE.To_Date,103) as ToDate,TSPL_VENDOR_MASTER.PAN,Tab_TSPL_PRICE_CHART_PLANNING_TSDDCF.Rate_Per as Planing_Rate_Per,Tab_TSPL_PRICE_CHART_PLANNING_TSDDCF.Fixed_Rate as Planing_Fixed_Rate,TSPL_VLC_MASTER_HEAD.Route_Code,TSPL_BULK_ROUTE_MASTER.ROUTE_NAME,TSPL_MILK_BILL_REVISE_DETAIL.Document_No from TSPL_MILK_BILL_REVISE_DETAIL
            left outer join TSPL_MILK_BILL_REVISE on TSPL_MILK_BILL_REVISE.Document_No= TSPL_MILK_BILL_REVISE_DETAIL.Document_No left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code= TSPL_MILK_BILL_REVISE.VLC_Code left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code= TSPL_VLC_MASTER_HEAD.VSP_Code left outer join TSPL_BULK_ROUTE_MASTER on TSPL_BULK_ROUTE_MASTER.ROUTE_NO= TSPL_VLC_MASTER_HEAD.Route_Code 
            left outer join  (select TSPL_PRICE_CHART_PLANNING_TSDDCF.Planning_Code, TSPL_PRICE_CHART_PLANNING_TSDDCF.Rate_Per,TSPL_PRICE_CHART_PLANNING_TSDDCF.Fixed_Rate from (select Planning_Code,max(SNo) as SNo from TSPL_PRICE_CHART_PLANNING_TSDDCF group by Planning_Code) TabMaxPlanning inner join TSPL_PRICE_CHART_PLANNING_TSDDCF on TSPL_PRICE_CHART_PLANNING_TSDDCF.Planning_Code=TabMaxPlanning.Planning_Code and TSPL_PRICE_CHART_PLANNING_TSDDCF.SNo=TabMaxPlanning.SNo) as Tab_TSPL_PRICE_CHART_PLANNING_TSDDCF on 
            Tab_TSPL_PRICE_CHART_PLANNING_TSDDCF.Planning_Code=TSPL_MILK_BILL_REVISE_DETAIL.Price_Code left join TSPL_COMPANY_MASTER on 1=1 where TSPL_MILK_BILL_REVISE.Status = 1 " + whrcls + " "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            qry = "	select Final.VSP_Uploader_Code,final.VSP_Code, Final.VLC_Code, Final.Addition_Code AS Addition , sum(Amount) as [Amount] from ( select TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code, TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VSP_Code,  TSPL_MILK_BILL_REVISE_ADDITION.Addition_Code , TSPL_MILK_BILL_REVISE_ADDITION.Amount from TSPL_MILK_BILL_REVISE_ADDITION inner join TSPL_MILK_BILL_REVISE on  TSPL_MILK_BILL_REVISE.Document_No = TSPL_MILK_BILL_REVISE_ADDITION.Document_No
            left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_BILL_REVISE.VLC_Code where TSPL_MILK_BILL_REVISE.Status = 1 " + whrcls + "  ) Final group by  final.VSP_Uploader_Code,final.VSP_Code, Final.VLC_Code, Final.Addition_Code "
            Dim dtSubAddition As DataTable = clsDBFuncationality.GetDataTable(qry)
            qry = "	select Final.VSP_Uploader_Code,final.VSP_Code, Final.VLC_Code, Final.Ded_Code  , sum(Amount) as [Amount] from ( select TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code, TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VSP_Code,  TSPL_MILK_BILL_REVISE_Deduction.Ded_Code , TSPL_MILK_BILL_REVISE_Deduction.Amount from TSPL_MILK_BILL_REVISE_Deduction inner join TSPL_MILK_BILL_REVISE on  TSPL_MILK_BILL_REVISE.Document_No = TSPL_MILK_BILL_REVISE_Deduction.Document_No
             left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_BILL_REVISE.VLC_Code where  TSPL_MILK_BILL_REVISE.Status = 1 " + whrcls + "  ) Final group by  final.VSP_Uploader_Code,final.VSP_Code, Final.VLC_Code, Final.Ded_Code "
            Dim dtSubDeduction As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                If GetPDFPath Then
                    PDFPath = frmCRV.funsubreportWithdt(MyBase.Form_ID, True, CrystalReportFolder.MilkProcurement, dt, dtSubAddition, "crptReviseMilkBil", "Revise Milk Bill", Nothing, "subAddition", "subDeduction", dtSubDeduction)
                Else
                    frmCRV.funsubreportWithdt(MyBase.Form_ID, CrystalReportFolder.MilkProcurement, dt, dtSubAddition, "crptReviseMilkBil", "Revise Milk Bill", "subAddition", "subDeduction", dtSubDeduction)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return PDFPath
    End Function
    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID + "M"
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New System.IO.MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub
    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        clsGridLayout.DeleteData(MyBase.Form_ID + "M", objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID + "M", "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub frmReviseMilkBill_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            btnAddNew.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            btnSave.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled AndAlso MyBase.isDeleteFlag Then
            btnDelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            btnClose.PerformClick()
        End If
        If e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then
                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SIRC"
                frm.strCode = "SIReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    lblPending.Visible = True
                    btnReverseAndUnpost.Visible = True
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
            End If
        End If
    End Sub
    Sub RefreshSNo()
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(colSNO).Value = ii
        Next
        For ii As Integer = 1 To gvAddition.Rows.Count
            gvAddition.Rows(ii - 1).Cells(colSNO).Value = ii
        Next

        For ii As Integer = 1 To gvDeduction.Rows.Count
            gvDeduction.Rows(ii - 1).Cells(colSNO).Value = ii
        Next
    End Sub

    Private Sub gv1_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow
        UpdateAllTotals()
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(colSNO).Value = ii
        Next
        RefreshSNo()
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub


    Private Sub gvAddition_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gvAddition.UserDeletedRow
        UpdateAllTotals()
        For ii As Integer = 1 To gvAddition.Rows.Count
            gvAddition.Rows(ii - 1).Cells(colSNO).Value = ii
        Next
        RefreshSNo()
    End Sub

    Private Sub gvAddition_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gvAddition.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub
    Private Sub gvDeduction_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gvDeduction.UserDeletedRow
        UpdateAllTotals()
        For ii As Integer = 1 To gvDeduction.Rows.Count
            gvDeduction.Rows(ii - 1).Cells(colSNO).Value = ii
        Next
        RefreshSNo()
    End Sub

    Private Sub gvDeduction_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gvDeduction.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colSNO).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub
    Private Sub gvDeduction_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvDeduction.CurrentColumnChanged
        If gvDeduction.RowCount > 0 Then
            Dim intCurrRow As Integer = gvDeduction.CurrentRow.Index
            gvDeduction.CurrentRow.Cells(colSNO).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gvDeduction.Rows.Count - 1 Then
                gvDeduction.Rows.AddNew()
                gvDeduction.CurrentRow = gvDeduction.Rows(intCurrRow)
            End If
        End If
    End Sub
    Private Sub gvAddition_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvAddition.CurrentColumnChanged
        If gvAddition.RowCount > 0 Then
            Dim intCurrRow As Integer = gvAddition.CurrentRow.Index
            gvAddition.CurrentRow.Cells(colSNO).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gvAddition.Rows.Count - 1 Then
                gvAddition.Rows.AddNew()
                gvAddition.CurrentRow = gvAddition.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gv1_CellValidated(sender As Object, e As CellValidatedEventArgs) Handles gv1.CellValidated
        Try
            SetGridFocusgv1()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFocusgv1()
        If gv1.CurrentCell IsNot Nothing Then
            Dim setNxtRow As Boolean = False
            If gv1.CurrentCell.ColumnInfo.Name = colDate Then
                gv1.CurrentColumn = gv1.Columns(colShift)
            ElseIf gv1.CurrentCell.ColumnInfo.Name = colShift Then
                gv1.CurrentColumn = gv1.Columns(colQty)
            ElseIf gv1.CurrentCell.ColumnInfo.Name = colQty Then
                gv1.CurrentColumn = gv1.Columns(colFATPer)
            ElseIf gv1.CurrentCell.ColumnInfo.Name = colFATPer Then
                gv1.CurrentColumn = gv1.Columns(colSNFPer)
            ElseIf gv1.CurrentCell.ColumnInfo.Name = colSNFPer Then
                setNxtRow = True
            End If
            If setNxtRow Then
                Dim str As Date = clsCommon.myCDate(gv1.CurrentRow.Cells(colDate).Value)
                If gv1.Rows.Count > gv1.CurrentRow.Index + 1 Then
                    gv1.CurrentRow = gv1.Rows(gv1.CurrentRow.Index + 1)
                End If
                gv1.CurrentColumn = gv1.Columns(colDate)
            End If
        End If
    End Sub
    Private Sub gvAddition_CellValidated(sender As Object, e As CellValidatedEventArgs) Handles gvAddition.CellValidated
        Try
            SetGridFocusgvAddition()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFocusgvAddition()
        If gvAddition.CurrentCell IsNot Nothing Then
            Dim setNxtRow As Boolean = False
            If gvAddition.CurrentCell.ColumnInfo.Name = colAdditionCode Then
                gvAddition.CurrentColumn = gvAddition.Columns(colAddAmt)
            ElseIf (gvAddition.CurrentCell.ColumnInfo.Name = colAddAmt) Then
                setNxtRow = True
            End If
            If setNxtRow Then
                If gvAddition.Rows.Count > gvAddition.CurrentRow.Index + 1 Then
                    gvAddition.CurrentRow = gvAddition.Rows(gvAddition.CurrentRow.Index + 1)
                    gvAddition.CurrentColumn = gvAddition.Columns(colAdditionCode)
                End If
            End If
        End If
    End Sub
    Private Sub gvDeduction_CellValidated(sender As Object, e As CellValidatedEventArgs) Handles gvDeduction.CellValidated
        Try
            SetGridFocusgvDeduction()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub SetGridFocusgvDeduction()
        If gvDeduction.CurrentCell IsNot Nothing Then
            Dim setNxtRow As Boolean = False
            If gvDeduction.CurrentCell.ColumnInfo.Name = colDeductionCode Then
                gvDeduction.CurrentColumn = gvDeduction.Columns(colDedAmt)
            ElseIf (gvDeduction.CurrentCell.ColumnInfo.Name = colDedAmt) Then
                setNxtRow = True
            End If
            If setNxtRow Then
                If gvDeduction.Rows.Count > gvDeduction.CurrentRow.Index + 1 Then
                    gvDeduction.CurrentRow = gvDeduction.Rows(gvDeduction.CurrentRow.Index + 1)
                    gvDeduction.CurrentColumn = gvDeduction.Columns(colDeductionCode)
                End If
            End If
        End If
    End Sub

    Private Sub btnReverseAndUnpost_Click(sender As Object, e As EventArgs) Handles btnReverseAndUnpost.Click
        Try
            If common.clsCommon.MyMessageBoxShow("Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If clsReviseMilkBill.ReverseAndUnpost(txtDocumentNo.Value) Then
                    common.clsCommon.MyMessageBoxShow("Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtDocumentNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnSendBillToDCS_Click(sender As Object, e As EventArgs) Handles btnSendBillToDCS.Click
        Try
            Dim qry As String = "select TSPL_MILK_BILL_REVISE_DETAIL.Document_No,TSPL_MILK_BILL_REVISE.From_Date,TSPL_MILK_BILL_REVISE.To_Date,TSPL_MILK_BILL_REVISE.VLC_CODE from TSPL_MILK_BILL_REVISE_DETAIL  left outer join TSPL_MILK_BILL_REVISE on TSPL_MILK_BILL_REVISE.Document_No=TSPL_MILK_BILL_REVISE_DETAIL.Document_No
            where TSPL_MILK_BILL_REVISE_DETAIL.Document_No='" + txtDocumentNo.Value + "' and TSPL_MILK_BILL_REVISE.FILE_INFO is null"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim frmCRV As New frmCrystalReportViewer()
                    Dim PDFPath As String = funPrint(True)
                    Dim FileNo As Integer = clsAttachDocument.UploadWithHttpRequest(PDFPath, Path.GetFileName(PDFPath), clsUserMgtCode.frmReviseMilkBill, txtDocumentNo.Value)
                    If FileNo > 0 Then
                        qry = " UPDATE TSPL_MILK_BILL_REVISE set FILE_INFO=" + clsCommon.myCstr(FileNo) + " where Document_No='" + txtDocumentNo.Value + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry)
                    End If
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExportBlank.Click
        Try
            Dim Str As String = "select '' as [Date],'' AS Shift,0 AS Qty,0 AS [FAT %], 0 AS [SNF %], 0 as Rate, '' as [Price Code], 0 as Amount"
            transportSql.ExporttoExcel(Str, Me)
            Str = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnExportData_Click(sender As Object, e As EventArgs) Handles btnExportData.Click
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmReviseMilkBill & "'"))
            transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
            clsCommon.MyExportToExcelGrid(Me.Text, gv1, arrHeader, Me.Text)
            common.clsCommon.MyMessageBoxShow(Me, "Exported Successfully.", Me.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub
End Class
