Imports common
Imports System.Data.SqlClient

Public Class frmPriceChartPlanMasterJPR
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim ErrorControl As clsErrorControl = New clsErrorControl()
    Dim isNewEntry As Boolean = True

    Public Const colSNo As String = "colSNo"
    Public Const colFATFrom As String = "colFATFrom"
    Public Const colFATTo As String = "colFATTo"
    Public Const colApplyFAT As String = "colApplyFAT"
    Public Const colSNFFrom As String = "colSNFFrom"
    Public Const colSNFTo As String = "colSNFTo"
    Public Const colApplySNF As String = "colApplySNF"

    Public Const colRatePer As String = "colRatePer"
    Public Const colFixedRate As String = "colFixedRate"
    Public Const colBelowSNFRate As String = "colBelowSNFRate"
    Public Const colDeductionPer As String = "colDeductionPer"

    Public Const colFillDeduction As String = "colFillDeduction"

    Dim arrNext As Dictionary(Of Integer, clsRowColumnTemp)
    Dim NextCounter As Integer = 0
    Dim isCalculate As Boolean = False
#End Region

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub FrmPriceChartMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadShift()
        Reset()
        gvFATDed.Rows.AddNew()
        RefreshSNo()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        RadPageView1.SelectedPage = RadPageViewPage1
        If objCommonVar.DisplayTypeInMilkReceipt Then
            cboDockCollectionMilkType.DataSource = clsMilkReceiptMCC.GetMilkType()
        Else
            cboDockCollectionMilkType.DataSource = clsMilkReceiptMCC.GetDockCollectionMilkType(False, True)
        End If
        cboDockCollectionMilkType.ValueMember = "Code"
        cboDockCollectionMilkType.DisplayMember = "Name"
        cboDockCollectionMilkType.SelectedValue = "M"
        cboDockCollectionMilkType.Enabled = (objCommonVar.SepratePriceChartForCow OrElse objCommonVar.DisplayTypeInMilkReceipt)

    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.MilkPricePlanning)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub Reset()
        txtCode.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtDesc.Text = ""
        txtPriceChartCode.Value = ""
        txtFatRatio.Value = 0
        txtSNFRatio.Value = 0
        txtFatPer.Value = 0
        txtSnfPer.Value = 0
        txtRate.Value = 0
        txtStdFATRate.Value = 0
        UsLock1.Status = ERPTransactionStatus.Pending
        isNewEntry = True
        txtCode.MyReadOnly = False
        btndelete.Enabled = False
        btnPost.Enabled = True
        btnsave.Enabled = True
        cboDockCollectionMilkType.SelectedValue = "M"

        'chkSNFZeroAfterMax.Checked = False
        'chkFATZeroAfterMax.Checked = False
        loadBlankGrid(gvFATDed)
        'loadBlankGrid(gvSNFDed)
    End Sub

    Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(txtPriceChartCode.Value) <= 0 Then
                txtPriceChartCode.Focus()
                txtPriceChartCode.Select()
                ErrorControl.SetError(txtPriceChartCode, "Please select Price Chart Code")
                Throw New Exception("Please select Price Chart Code")
            Else
                ErrorControl.ResetError(txtPriceChartCode)
            End If
            Dim Rate As Decimal = CalculateRate(txtFatPer.Value, txtSnfPer.Value)
            If Math.Abs(Rate - txtRate.Value) > 0.1 Then
                txtPriceChartCode.Focus()
                txtPriceChartCode.Select()
                ErrorControl.SetError(txtPriceChartCode, "Price Chart Rate:" + clsCommon.myCstr(txtRate.Value) + " And Calulated Rate : " + clsCommon.myCstr(Rate))
                Throw New Exception("Price Chart Rate:" + clsCommon.myCstr(txtRate.Value) + " And Calulated Rate : " + clsCommon.myCstr(Rate))
            Else
                ErrorControl.ResetError(txtPriceChartCode)
            End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Function

    Sub SaveData()
        Try
            If AllowToSave() Then

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.MilkPricePlanning, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If

                Dim obj As New clsPriceChartPlanning()
                obj.Planning_Code = txtCode.Value
                obj.Planning_Date = txtDate.Value
                obj.Planning_Description = txtDesc.Text
                obj.Price_Chart_Code = txtPriceChartCode.Value
                obj.Price_Chart_FAT_Ratio = txtFatRatio.Value
                obj.Price_Chart_SNF_Ratio = txtSNFRatio.Value
                obj.Price_Chart_FAT_Per = txtFatPer.Value
                obj.Price_Chart_SNF_Per = txtSnfPer.Value
                obj.Price_Chart_Rate = txtRate.Value


                obj.Shift = clsCommon.myCstr(CboShift.SelectedValue)
                obj.Dock_Collection_Milk_Type = clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue)

                obj.TSDDCS_Rate = txtStdFATRate.Value

                obj.arrTSDDCS = New List(Of clsPriceChartPlanningTSDDCF)
                For ii As Integer = 0 To gvFATDed.RowCount - 1
                    Dim objTSDDCF As New clsPriceChartPlanningTSDDCF
                    objTSDDCF.FAT_From = clsCommon.myCDecimal(gvFATDed.Rows(ii).Cells(colFATFrom).Value)
                    objTSDDCF.FAT_To = clsCommon.myCDecimal(gvFATDed.Rows(ii).Cells(colFATTo).Value)
                    objTSDDCF.Apply_FAT = clsCommon.myCDecimal(gvFATDed.Rows(ii).Cells(colApplyFAT).Value)
                    objTSDDCF.SNF_From = clsCommon.myCDecimal(gvFATDed.Rows(ii).Cells(colSNFFrom).Value)
                    objTSDDCF.SNF_To = clsCommon.myCDecimal(gvFATDed.Rows(ii).Cells(colSNFTo).Value)
                    objTSDDCF.Apply_SNF = clsCommon.myCDecimal(gvFATDed.Rows(ii).Cells(colApplySNF).Value)
                    objTSDDCF.Rate_Per = clsCommon.myCDecimal(gvFATDed.Rows(ii).Cells(colRatePer).Value)
                    objTSDDCF.Fixed_Rate = clsCommon.myCDecimal(gvFATDed.Rows(ii).Cells(colFixedRate).Value)
                    objTSDDCF.Below_SNF_Rate = clsCommon.myCDecimal(gvFATDed.Rows(ii).Cells(colBelowSNFRate).Value)
                    objTSDDCF.Deduction_Per = clsCommon.myCDecimal(gvFATDed.Rows(ii).Cells(colDeductionPer).Value)
                    objTSDDCF.arr = TryCast(gvFATDed.Rows(ii).Cells(colSNo).Tag, Dictionary(Of Decimal, Decimal))
                    If ((objTSDDCF.FAT_From > 0 OrElse objTSDDCF.FAT_To > 0) AndAlso (objTSDDCF.SNF_From > 0 OrElse objTSDDCF.SNF_To > 0) OrElse (objTSDDCF.Rate_Per > 0 OrElse objTSDDCF.Fixed_Rate > 0 OrElse objTSDDCF.Below_SNF_Rate > 0)) Then
                        obj.arrTSDDCS.Add(objTSDDCF)
                    End If
                Next
                If clsPriceChartPlanning.SaveData(obj, isNewEntry) Then
                    clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                    LoadData(obj.Planning_Code, NavigatorType.Current)
                End If
            Else
                txtCode.MyReadOnly = False
                btndelete.Enabled = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub FrmPriceChartMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Control AndAlso e.Alt AndAlso e.Shift AndAlso e.KeyCode = Keys.F11 Then
            isCalculate = Not isCalculate
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            btnnew.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            btnsave.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            btndelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub

    Private Sub LoadShift()
        CboShift.DataSource = ClsOpenMCCShift.GetShift
        CboShift.DisplayMember = "Name"
        CboShift.ValueMember = "Code"
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        Try
            clsPriceChartPlanning.DeleteData(txtCode.Value)
            clsCommon.MyMessageBoxShow("Data Deleted Successfully", Me.Text)
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try
            'Dim FromValue As Decimal
            Reset()
            Dim obj As clsPriceChartPlanning = clsPriceChartPlanning.GetData(strCode, NavType)
            isNewEntry = True
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Planning_Code) > 0 Then
                isNewEntry = False
                If obj.Status = ERPTransactionStatus.Approved Then
                    btndelete.Enabled = False
                    btnPost.Enabled = False
                    btnsave.Enabled = False
                End If

                UsLock1.Status = obj.Status
                txtCode.Value = obj.Planning_Code
                txtDate.Value = obj.Planning_Date
                txtDesc.Text = obj.Planning_Description
                txtPriceChartCode.Value = obj.Price_Chart_Code
                txtFatRatio.Value = obj.Price_Chart_FAT_Ratio
                txtSNFRatio.Value = obj.Price_Chart_SNF_Ratio
                txtFatPer.Value = obj.Price_Chart_FAT_Per
                txtSnfPer.Value = obj.Price_Chart_SNF_Per
                txtRate.Value = obj.Price_Chart_Rate
                CboShift.SelectedValue = obj.Shift
                cboDockCollectionMilkType.SelectedValue = obj.Dock_Collection_Milk_Type
                txtStdFATRate.Value = obj.TSDDCS_Rate
                If obj.arrTSDDCS IsNot Nothing AndAlso obj.arrTSDDCS.Count > 0 Then
                    For Each objTSDDCS As clsPriceChartPlanningTSDDCF In obj.arrTSDDCS
                        gvFATDed.Rows.AddNew()
                        gvFATDed.Rows(gvFATDed.Rows.Count - 1).Cells(colSNo).Value = objTSDDCS.SNo
                        gvFATDed.Rows(gvFATDed.Rows.Count - 1).Cells(colFATFrom).Value = objTSDDCS.FAT_From
                        gvFATDed.Rows(gvFATDed.Rows.Count - 1).Cells(colFATTo).Value = objTSDDCS.FAT_To
                        gvFATDed.Rows(gvFATDed.Rows.Count - 1).Cells(colApplyFAT).Value = objTSDDCS.Apply_FAT
                        gvFATDed.Rows(gvFATDed.Rows.Count - 1).Cells(colSNFFrom).Value = objTSDDCS.SNF_From
                        gvFATDed.Rows(gvFATDed.Rows.Count - 1).Cells(colSNFTo).Value = objTSDDCS.SNF_To
                        gvFATDed.Rows(gvFATDed.Rows.Count - 1).Cells(colApplySNF).Value = objTSDDCS.Apply_SNF
                        gvFATDed.Rows(gvFATDed.Rows.Count - 1).Cells(colRatePer).Value = objTSDDCS.Rate_Per
                        gvFATDed.Rows(gvFATDed.Rows.Count - 1).Cells(colFixedRate).Value = objTSDDCS.Fixed_Rate
                        gvFATDed.Rows(gvFATDed.Rows.Count - 1).Cells(colBelowSNFRate).Value = objTSDDCS.Below_SNF_Rate
                        gvFATDed.Rows(gvFATDed.Rows.Count - 1).Cells(colDeductionPer).Value = objTSDDCS.Deduction_Per
                        gvFATDed.Rows(gvFATDed.Rows.Count - 1).Cells(colSNo).Tag = objTSDDCS.arr
                    Next
                End If

                btndelete.Enabled = True
                txtCode.MyReadOnly = True
            End If
        Catch ex As Exception
            isNewEntry = True
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndcode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        LoadData(txtCode.Value, NavType)
    End Sub

    Private Sub fndcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Dim qry As String = "select Planning_Code as Code,Planning_Description as Description , Planning_Date as [Date],Price_Chart_Code from TSPL_PRICE_CHART_PLANNING"

        Dim whrcls As String = ""
        txtCode.Value = clsCommon.ShowSelectForm("PCPLN", qry, "Code", whrcls, txtCode.Value, "Code", isButtonClicked)

        If clsCommon.myLen(txtCode.Value) > 0 Then
            LoadData(txtCode.Value, NavigatorType.Current)
        Else
            Reset()
        End If
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        Reset()
        gvFATDed.Rows.AddNew()
        RefreshSNo()
    End Sub

    Private Sub txtratio_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Try
            Convert.ToDecimal(txtFatRatio.Text)
            ErrorControl.ResetError(txtFatRatio)
        Catch ex As Exception
            ErrorControl.SetError(txtFatRatio, "Fill numeric value only.")
            txtFatRatio.Focus()
            txtFatRatio.Select()
            txtFatRatio.Text = "0"
            clsCommon.MyMessageBoxShow("Fill numeric value only.", Me.Text)
        End Try
        Try
            txtSNFRatio.Text = 100 - clsCommon.myCDecimal(txtFatRatio.Text)
            If (clsCommon.myCDecimal(txtFatRatio.Text) + clsCommon.myCDecimal(txtSNFRatio.Text)) <> 100 AndAlso clsCommon.myCDecimal(txtSNFRatio.Text) > 0 Then
                txtFatRatio.Focus()
                Throw New Exception("Please Fill Ratio Of SNF And FAT." + Environment.NewLine + "There Sum Should be Equal To 100")
            End If
        Catch exx As Exception
            clsCommon.MyMessageBoxShow(exx.Message)
        End Try
    End Sub

    Private Sub txtsnf_ratio_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Try
            Convert.ToDecimal(txtSNFRatio.Text)
            ErrorControl.ResetError(txtSNFRatio)
        Catch ex As Exception
            ErrorControl.SetError(txtSNFRatio, "Fill numeric value only.")
            txtSNFRatio.Focus()
            txtSNFRatio.Select()
            txtSNFRatio.Text = "0"
            clsCommon.MyMessageBoxShow("Fill numeric value only.", Me.Text)
        End Try
        Try
            txtFatRatio.Text = 100 - clsCommon.myCDecimal(txtSNFRatio.Text)
            If (clsCommon.myCDecimal(txtFatRatio.Text) + clsCommon.myCDecimal(txtSNFRatio.Text)) <> 100 AndAlso clsCommon.myCDecimal(txtFatRatio.Text) > 0 Then
                txtSNFRatio.Focus()
                Throw New Exception("Please Fill Ratio Of SNF And FAT." + Environment.NewLine + "There Sum Should be Equal To 100")
            End If
        Catch exx As Exception
            clsCommon.MyMessageBoxShow(exx.Message)
        End Try
    End Sub

    Private Sub fndInventoryControl__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtPriceChartCode._MYValidating
        Dim qry As String = ""
        qry = "select price_code as Code,Description,Effective_Date as [Effective Date],Inactive_Date as [Inactive Date],Ratio as [FAT Ratio],snf_ratio as [SNF Ratio],FAT_Pers as [FAT %],SNF_Pers as [SNF %],Milk_Rate as [Milk Rate],Created_By as [Created By],Created_Date as [Created Date],Modified_By as [Modified By],Modified_Date as [Modified Date] from TSPL_MILK_PRICE_MASTER"
        Dim whrcls As String = " Price_Type='MCC'"

        txtPriceChartCode.Value = clsCommon.ShowSelectForm("PRCFND1", qry, "Code", whrcls, txtPriceChartCode.Value, "Code", isButtonClicked)
        Try
            Dim obj As clsfrmPriceChartMaster = clsfrmPriceChartMaster.GetData(txtPriceChartCode.Value, NavigatorType.Current, "MCC")
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.code) > 0 Then
                txtPriceChartCode.Value = obj.code
                txtFatRatio.Text = obj.ratio
                txtFatPer.Text = obj.fat_pers
                txtSnfPer.Text = obj.snf_pers
                txtRate.Text = obj.declrd_rate
                txtSNFRatio.Text = obj.snf_ratio
            Else
                txtFatRatio.Text = 0
                txtFatPer.Text = 0
                txtSnfPer.Text = 0
                txtRate.Text = 0
                txtSNFRatio.Text = 0
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtSAFatPer_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs)
        'txtSingleAxisSnfDedFatPer.Value = txtSingleAxisFatPer.Value
        'txtDoubleAxisFatPer.Value = txtSingleAxisFatPer.Value
    End Sub

    Private Sub txtDoubleAxisCreamBaseSnfPer_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs)
        'txtSingleAxisSnfDedSnfPer.Value = txtDoubleAxisCreamBaseSnfPer.Value
        'txtDoubleAxisSnfPer.Value = txtDoubleAxisCreamBaseSnfPer.Value
    End Sub

    Private Function CalculateRate(ByVal dblFATPer As Decimal, ByVal dblSNFPer As Decimal) As Double
        Dim dclReturnMilkValue As Decimal = 0
        Dim dclRate As Decimal = 0
        If dblFATPer = 7.6 AndAlso dblSNFPer = 8.9 Then
            Dim x As Integer = 0
        End If
        Dim dclDedPer As Decimal = 0
        For ii As Integer = 0 To gvFATDed.Rows.Count - 1
            If dblFATPer >= clsCommon.myCDecimal(gvFATDed.Rows(ii).Cells(colFATFrom).Value) AndAlso dblFATPer <= clsCommon.myCDecimal(gvFATDed.Rows(ii).Cells(colFATTo).Value) Then
                If txtStdFATRate.Value > 0 Then
                    If Not (dblSNFPer >= clsCommon.myCDecimal(gvFATDed.Rows(ii).Cells(colSNFFrom).Value) AndAlso dblSNFPer <= clsCommon.myCDecimal(gvFATDed.Rows(ii).Cells(colSNFTo).Value)) Then
                        Continue For
                    End If
                End If

                If clsCommon.myCDecimal(gvFATDed.Rows(ii).Cells(colApplyFAT).Value) > 0 Then
                    If dblFATPer > clsCommon.myCDecimal(gvFATDed.Rows(ii).Cells(colApplyFAT).Value) Then
                        dblFATPer = clsCommon.myCDecimal(gvFATDed.Rows(ii).Cells(colApplyFAT).Value)
                    End If
                End If
                If clsCommon.myCDecimal(gvFATDed.Rows(ii).Cells(colApplySNF).Value) > 0 Then
                    If dblSNFPer > clsCommon.myCDecimal(gvFATDed.Rows(ii).Cells(colApplySNF).Value) Then
                        dblSNFPer = clsCommon.myCDecimal(gvFATDed.Rows(ii).Cells(colApplySNF).Value)
                    End If
                End If
                If dblSNFPer < clsCommon.myCDecimal(gvFATDed.Rows(ii).Cells(colSNFFrom).Value) Then
                    dclRate = clsCommon.myCDecimal(gvFATDed.Rows(ii).Cells(colBelowSNFRate).Value)
                Else
                    dclRate = clsCommon.myCDecimal(gvFATDed.Rows(ii).Cells(colRatePer).Value)
                End If

                dclReturnMilkValue = ((dclRate * dblFATPer) / 100)
                dclReturnMilkValue += clsCommon.myCDecimal(gvFATDed.Rows(ii).Cells(colFixedRate).Value)

                Dim arr As Dictionary(Of Decimal, Decimal) = TryCast(gvFATDed.Rows(ii).Cells(colSNo).Tag, Dictionary(Of Decimal, Decimal))
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    If arr.ContainsKey(dblSNFPer) Then
                        dclReturnMilkValue += arr.Item(dblSNFPer)
                    End If
                End If
                dclDedPer = clsCommon.myCDecimal(gvFATDed.Rows(ii).Cells(colDeductionPer).Value)
                Exit For
            End If
        Next
        dclReturnMilkValue = clsCommon.myRoundOFF(dclReturnMilkValue, 2, 4)
        dclReturnMilkValue = (dclReturnMilkValue * ((100 - dclDedPer) / 100))
        If dclReturnMilkValue < 0 Then
            dclReturnMilkValue = 0
        End If

        Return dclReturnMilkValue
    End Function





    Private Sub RadButton2_Click(sender As Object, e As EventArgs) Handles RadButton2.Click
        FillGridView()
    End Sub



    Sub FillGridView()
        RadPageView1.SelectedPage = RadPageViewPage2
        gv.Rows.Clear()
        gv.Columns.Clear()

        Dim repoComplete As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoComplete.FormatString = ""
        repoComplete.HeaderText = "FAT \ SNF"
        repoComplete.Name = "FATSNF"
        repoComplete.ReadOnly = True
        repoComplete.IsVisible = True
        repoComplete.Width = 80
        repoComplete.IsPinned = True
        gv.MasterTemplate.Columns.Add(repoComplete)

        For ii As Integer = 0 To 150
            Dim strColName As String = clsCommon.myCstr(ii / 10)
            Dim repoOrderQty As GridViewDecimalColumn = New GridViewDecimalColumn()
            repoOrderQty.FormatString = ""
            repoOrderQty.HeaderText = strColName
            repoOrderQty.Name = strColName
            repoOrderQty.Width = 50
            repoOrderQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            repoOrderQty.ReadOnly = True
            repoOrderQty.Minimum = 0
            repoOrderQty.ShowUpDownButtons = False

            gv.MasterTemplate.Columns.Add(repoOrderQty)
        Next


        gv.AllowAddNewRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = True
        gv.AllowRowReorder = False
        gv.EnableSorting = False
        gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv.MasterTemplate.ShowRowHeaderColumn = False
        gv.TableElement.TableHeaderHeight = 30
        gv.EnableSorting = False

        clsCommon.ProgressBarPercentShow()
        Try
            For Row As Integer = 0 To 150
                clsCommon.ProgressBarPercentUpdate((Row * 100 / 150), "Generating FAT/SNF matrix")
                gv.Rows.AddNew()
                gv.Rows(gv.Rows.Count - 1).Cells("FATSNF").Value = clsCommon.myCstr(Row / 10)
                For Col As Integer = 0 To 150
                    Dim strColName As String = clsCommon.myCstr(Col / 10)
                    gv.Rows(gv.Rows.Count - 1).Cells(strColName).Value = CalculateRate(Row / 10, Col / 10)
                Next
            Next
            clsCommon.ProgressBarPercentHide()
        Catch ex As Exception
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetNextSerach()
        Try
            If arrNext IsNot Nothing AndAlso arrNext.Count > 0 Then
                If arrNext.Count <= NextCounter Then
                    NextCounter = 0
                End If
                Dim obj As clsRowColumnTemp = arrNext(NextCounter)
                gv.CurrentRow = gv.Rows(obj.Row)
                gv.CurrentColumn = gv.Columns(obj.col)
                NextCounter += 1
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub RadButton3_Click(sender As Object, e As EventArgs) Handles RadButton3.Click
        Try
            If isCalculate Then
                'clsCommon.MyMessageBoxShow("Rate is - " + clsCommon.myCstr(CalculateRate(txtSearchFAT.Value, txtSearchSNF.Value, ArrFATDed, ArrSNFDed)), Me.Text)
            Else
                arrNext = New Dictionary(Of Integer, clsRowColumnTemp)()
                If txtSearchRate.Value > 0 Then
                    NextCounter = 0
                    For Row As Integer = 0 To 150
                        For Col As Integer = 0 To 150
                            If clsCommon.myCDecimal(gv.Rows(Row).Cells(clsCommon.myCstr(Col / 10)).Value) = txtSearchRate.Value Then
                                Dim obj As New clsRowColumnTemp
                                obj.Row = Row
                                obj.col = Col + 1
                                arrNext.Add(arrNext.Count, obj)
                            End If
                        Next
                    Next
                    SetNextSerach()
                ElseIf txtSearchFAT.Value > 0 And txtSearchSNF.Value > 0 Then
                    Try
                        gv.CurrentRow = gv.Rows(txtSearchFAT.Value * 10)
                        gv.CurrentColumn = gv.Columns(clsCommon.myCstr(txtSearchSNF.Value))
                    Catch ex As Exception
                    End Try
                End If
            End If
        Catch ex As Exception
            Throw New Exception("No data found")
        End Try
    End Sub

    Private Sub RadButton4_Click(sender As Object, e As EventArgs) Handles RadButton4.Click
        Try
            'Dim PriceCode As String = ""
            'Dim Rate As Decimal = clsEkoPro.GetRateCalculatedJPR(PriceCode, DateTime.Now, "M", "", "M", 2, txtSearchFAT.Value, txtSearchSNF.Value, Nothing)
            'clsCommon.MyMessageBoxShow(clsCommon.myCstr(Rate))
            If gv Is Nothing OrElse gv.Rows.Count <= 0 Then
                Throw New Exception("No data found to export")
            End If

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("FAT/SNF Matrix")
            'Dim sfd As SaveFileDialog = New SaveFileDialog()
            'Dim filePath As String
            'sfd.FileName = Me.Text
            'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    filePath = sfd.FileName
            'Else
            '    Exit Sub
            'End If
            'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
            transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub RadButton5_Click(sender As Object, e As EventArgs) Handles RadButton5.Click
        SetNextSerach()
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("Code not found to post")
            End If
            LoadData(txtCode.Value, NavigatorType.Current)
            If UsLock1.Status = ERPTransactionStatus.Approved Then
                Throw New Exception("Already posted")
            End If
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try
                ''Dim qry As String = "select max(code) from TSPL_FAT_SNF_UPLOADER_MASTER"
                ''Dim Code As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                ''If clsCommon.myLen(Code) > 0 Then
                ''    Code = clsCommon.myCstr(clsCommon.incval(Code))
                ''Else
                ''    Code = "PCU000001"
                ''End If
                Dim dtCurrent As DateTime = clsCommon.GETSERVERDATE(trans)
                ''Dim dtEffective As DateTime = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select Effective_Date from TSPL_MILK_PRICE_MASTER where price_code='" + txtPriceChartCode.Value + "'", trans))
                'Dim Code As String = clsERPFuncationality.GetNextCode(trans, txtDate.Value, clsDocType.MatrixPriceChart, "", "")
                'If clsCommon.myLen(Code) < 0 Then
                '    Throw New Exception("Error in Code Generation")
                'End If
                'Dim coll As New Hashtable()
                '''---------------------FAT SNF
                'For RowFAT As Integer = 0 To 150
                '    For ColSNF As Integer = 0 To 150
                '        coll = New Hashtable()
                '        clsCommon.AddColumnsForChange(coll, "Code", Code)

                '        'clsCommon.AddColumnsForChange(coll, "date", clsCommon.GetPrintDate(dtEffective, "dd/MMM/yyyy"))
                '        clsCommon.AddColumnsForChange(coll, "date", clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"))
                '        clsCommon.AddColumnsForChange(coll, "fat", RowFAT / 10)
                '        clsCommon.AddColumnsForChange(coll, "snf", ColSNF / 10)
                '        clsCommon.AddColumnsForChange(coll, "rate", CalculateRate(RowFAT / 10, ColSNF / 10))
                '        clsCommon.AddColumnsForChange(coll, "Price_Code", txtPriceChartCode.Value)
                '        clsCommon.AddColumnsForChange(coll, "Price_Code_Shift", clsCommon.myCstr(CboShift.SelectedValue))
                '        clsCommon.AddColumnsForChange(coll, "created_by", clsCommon.myCstr(objCommonVar.CurrentUserCode))
                '        clsCommon.AddColumnsForChange(coll, "created_date", clsCommon.myCstr(clsCommon.GetPrintDate(dtCurrent, "dd/MM/yyyy")))
                '        clsCommon.AddColumnsForChange(coll, "modified_by", clsCommon.myCstr(objCommonVar.CurrentUserCode))
                '        clsCommon.AddColumnsForChange(coll, "modified_date", clsCommon.myCstr(clsCommon.GetPrintDate(dtCurrent, "dd/MM/yyyy")))
                '        clsCommon.AddColumnsForChange(coll, "Planning_Code", txtCode.Value, True)
                '        clsCommon.AddColumnsForChange(coll, "Dock_Collection_Milk_Type", clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue))
                '        clsCommon.AddColumnsForChange(coll, "Posted", 1)
                '        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_FAT_SNF_UPLOADER_MASTER", OMInsertOrUpdate.Insert, "", trans)
                '    Next
                'Next
                '''---------------------End of FAT SNF



                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Status", 1)
                clsCommon.AddColumnsForChange(coll, "Posted_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Posted_Date", clsCommon.myCstr(clsCommon.GetPrintDate(dtCurrent, "dd/MMM/yyyy hh:mm:ss tt")))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PRICE_CHART_PLANNING", OMInsertOrUpdate.Update, "Planning_Code='" + txtCode.Value + "'", trans)

                trans.Commit()
                clsCommon.MyMessageBoxShow("Data Posted Successfully")
                LoadData(txtCode.Value, NavigatorType.Current)
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try

    End Sub





    Private Sub gvTS_UserDeletedRow(sender As Object, e As GridViewRowEventArgs) Handles gvFATDed.UserDeletedRow
    End Sub

    Private Sub gvTS_UserDeletingRow(sender As Object, e As GridViewRowCancelEventArgs) Handles gvFATDed.UserDeletingRow
        If Not myMessages.deleteConfirm() Then
            e.Cancel = True
        End If
        RefreshSNo()
    End Sub

    Sub loadBlankGrid(ByVal gv As common.UserControls.MyRadGridView)
        Try
            gv.Rows.Clear()
            gv.Columns.Clear()
            Dim repoDeciCol As GridViewDecimalColumn
            repoDeciCol = New GridViewDecimalColumn()
            repoDeciCol.Name = colSNo
            repoDeciCol.Width = 30
            repoDeciCol.DecimalPlaces = 0
            'repoDeciCol.Minimum = 0
            'repoDeciCol.Maximum = 100
            repoDeciCol.Step = 0
            repoDeciCol.ShowUpDownButtons = False
            repoDeciCol.HeaderText = "SNo"
            repoDeciCol.ReadOnly = True
            gv.MasterTemplate.Columns.Add(repoDeciCol)

            repoDeciCol = New GridViewDecimalColumn()
            repoDeciCol.Name = colFATFrom
            repoDeciCol.Width = 100
            repoDeciCol.DecimalPlaces = 1
            repoDeciCol.Minimum = 0
            repoDeciCol.Maximum = 15
            repoDeciCol.Step = 0
            repoDeciCol.ShowUpDownButtons = False
            repoDeciCol.HeaderText = "From FAT %"
            gv.MasterTemplate.Columns.Add(repoDeciCol)

            repoDeciCol = New GridViewDecimalColumn()
            repoDeciCol.Name = colFATTo
            repoDeciCol.Width = 100
            repoDeciCol.DecimalPlaces = 1
            repoDeciCol.Minimum = 0
            repoDeciCol.Maximum = 15
            repoDeciCol.Step = 0
            repoDeciCol.ShowUpDownButtons = False
            repoDeciCol.HeaderText = "TO FAT %"
            gv.MasterTemplate.Columns.Add(repoDeciCol)

            repoDeciCol = New GridViewDecimalColumn()
            repoDeciCol.Name = colApplyFAT
            repoDeciCol.Width = 100
            repoDeciCol.DecimalPlaces = 1
            repoDeciCol.Minimum = 0
            'repoDeciCol.Maximum = 15
            repoDeciCol.Step = 0
            repoDeciCol.ShowUpDownButtons = False
            repoDeciCol.HeaderText = "Apply FAT%"
            gv.MasterTemplate.Columns.Add(repoDeciCol)

            repoDeciCol = New GridViewDecimalColumn()
            repoDeciCol.Name = colSNFFrom
            repoDeciCol.Width = 100
            repoDeciCol.DecimalPlaces = 1
            repoDeciCol.Minimum = 0
            repoDeciCol.Maximum = 15
            repoDeciCol.Step = 0
            repoDeciCol.ShowUpDownButtons = False
            repoDeciCol.HeaderText = "From SNF %"
            gv.MasterTemplate.Columns.Add(repoDeciCol)

            repoDeciCol = New GridViewDecimalColumn()
            repoDeciCol.Name = colSNFTo
            repoDeciCol.Width = 100
            repoDeciCol.DecimalPlaces = 1
            repoDeciCol.Minimum = 0
            repoDeciCol.Maximum = 15
            repoDeciCol.Step = 0
            repoDeciCol.ShowUpDownButtons = False
            repoDeciCol.HeaderText = "TO SNF %"
            gv.MasterTemplate.Columns.Add(repoDeciCol)

            repoDeciCol = New GridViewDecimalColumn()
            repoDeciCol.Name = colApplySNF
            repoDeciCol.Width = 100
            repoDeciCol.DecimalPlaces = 1
            repoDeciCol.Minimum = 0
            'repoDeciCol.Maximum = 15
            repoDeciCol.Step = 0
            repoDeciCol.ShowUpDownButtons = False
            repoDeciCol.HeaderText = "Apply SNF%"
            gv.MasterTemplate.Columns.Add(repoDeciCol)

            repoDeciCol = New GridViewDecimalColumn()
            repoDeciCol.Name = colRatePer
            repoDeciCol.Width = 100
            repoDeciCol.DecimalPlaces = 2
            repoDeciCol.Minimum = 0
            'repoDeciCol.Maximum = 100
            repoDeciCol.Step = 0
            repoDeciCol.ShowUpDownButtons = False
            repoDeciCol.HeaderText = "FAT Rate"
            gv.MasterTemplate.Columns.Add(repoDeciCol)

            repoDeciCol = New GridViewDecimalColumn()
            repoDeciCol.Name = colFixedRate
            repoDeciCol.Width = 100
            repoDeciCol.DecimalPlaces = 2
            repoDeciCol.Minimum = 0
            repoDeciCol.Step = 0
            repoDeciCol.ShowUpDownButtons = False
            repoDeciCol.HeaderText = "Fixed Rate"
            gv.MasterTemplate.Columns.Add(repoDeciCol)

            repoDeciCol = New GridViewDecimalColumn()
            repoDeciCol.Name = colBelowSNFRate
            repoDeciCol.Width = 100
            repoDeciCol.DecimalPlaces = 2
            repoDeciCol.Minimum = 0
            repoDeciCol.Step = 0
            repoDeciCol.ShowUpDownButtons = False
            repoDeciCol.HeaderText = "Below SNF Rate"
            gv.MasterTemplate.Columns.Add(repoDeciCol)

            repoDeciCol = New GridViewDecimalColumn()
            repoDeciCol.Name = colDeductionPer
            repoDeciCol.Width = 100
            repoDeciCol.DecimalPlaces = 2
            repoDeciCol.Minimum = 0
            repoDeciCol.Step = 0
            repoDeciCol.ShowUpDownButtons = False
            repoDeciCol.HeaderText = "Deduction %"
            gv.MasterTemplate.Columns.Add(repoDeciCol)

            Dim ShowBtn As New GridViewCommandColumn()
            ShowBtn.FormatString = ""
            ShowBtn.UseDefaultText = True
            ShowBtn.DefaultText = "SNF Deduction"
            ShowBtn.HeaderText = ""
            ShowBtn.Name = colFillDeduction
            ShowBtn.FieldName = colFillDeduction
            ShowBtn.Width = 100
            ShowBtn.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            gv.MasterTemplate.Columns.Add(ShowBtn)

            gv.AllowDeleteRow = True
            gv.AllowAddNewRow = False
            gv.ShowGroupPanel = False
            gv.AllowColumnReorder = False
            gv.AllowRowReorder = False
            gv.EnableSorting = False
            gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
            gv.MasterTemplate.ShowRowHeaderColumn = False
            gv.TableElement.TableHeaderHeight = 40
            gv.AutoSizeRows = False
            gv.AllowRowReorder = True

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gvTS_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvFATDed.CurrentColumnChanged
        If gvFATDed.RowCount > 0 Then
            Dim intCurrRow As Integer = gvFATDed.CurrentRow.Index
            If intCurrRow = gvFATDed.Rows.Count - 1 Then
                gvFATDed.Rows.AddNew()
                gvFATDed.CurrentRow = gvFATDed.Rows(intCurrRow)
            End If
        End If
        RefreshSNo()
    End Sub

    Sub RefreshSNo()
        For ii As Integer = 0 To gvFATDed.Rows.Count - 1
            gvFATDed.Rows(ii).Cells(colSNo).Value = ii + 1
        Next
    End Sub



    Private Sub gvFATDed_CommandCellClick(sender As Object, e As EventArgs) Handles gvFATDed.CommandCellClick
        Try
            If gvFATDed.CurrentColumn Is gvFATDed.Columns(colFillDeduction) Then
                Dim frm As New frmPriceChartPlanMasterTSDDCFDeduction()
                frm.ArrDed = gvFATDed.CurrentRow.Cells(colSNo).Tag
                frm.SNFFrom = gvFATDed.CurrentRow.Cells(colSNFFrom).Value
                frm.SNFTo = gvFATDed.CurrentRow.Cells(colSNFTo).Value
                frm.ShowDialog()
                If frm.isOK Then
                    gvFATDed.CurrentRow.Cells(colSNo).Tag = frm.ArrDed
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub RadButton1_Click_1(sender As Object, e As EventArgs) Handles RadButton1.Click
        Try
            Dim qry As String = "select Planning_Code as Code,Planning_Description as Description , Planning_Date as [Date],Price_Chart_Code from TSPL_PRICE_CHART_PLANNING"
            Dim whrcls As String = ""
            txtCode.Value = clsCommon.ShowSelectForm("PCP1LN", qry, "Code", whrcls, txtCode.Value, "Code", True)

            If clsCommon.myLen(txtCode.Value) > 0 Then
                LoadData(txtCode.Value, NavigatorType.Current)
                isNewEntry = True
                txtCode.Value = ""
                btnDelete.Enabled = False
                btnPost.Enabled = False
                btnsave.Enabled = True
            Else
                Reset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
End Class