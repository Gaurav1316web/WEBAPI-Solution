Imports common
Imports System.Data.SqlClient

Public Class frmPriceChartPlanMasterGK
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim ErrorControl As clsErrorControl = New clsErrorControl()
    Dim isNewEntry As Boolean = True

    Public Const colPer As String = "colPer"
    Public Const colAmt As String = "colAmt"

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
        gvSNFDed.Rows.AddNew()

        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        RadPageView1.SelectedPage = RadPageViewPage1

        cboDockCollectionMilkType.DataSource = clsMilkReceiptMCC.GetDockCollectionMilkType(False, True)
        cboDockCollectionMilkType.ValueMember = "Code"
        cboDockCollectionMilkType.DisplayMember = "Name"
        cboDockCollectionMilkType.SelectedValue = "M"

        cboDockCollectionMilkType.Enabled = objCommonVar.SepratePriceChartForCow
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.MilkPricePlanning)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow(Me, "Permission Denied", Me.Text)
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
        txtMCC.arrValueMember = Nothing
        txtVLC.arrValueMember = Nothing
        UsLock1.Status = ERPTransactionStatus.Pending
        isNewEntry = True
        txtCode.MyReadOnly = False
        btndelete.Enabled = False
        btnPost.Enabled = True
        btnsave.Enabled = True
        cboDockCollectionMilkType.SelectedValue = "M"
        txtMinFATPer.Value = 0
        txtMaxFATPer.Value = 0
        txtMinSNFPer.Value = 0
        txtMaxSNFPer.Value = 0
        chkSNFZeroAfterMax.Checked = False
        chkFATZeroAfterMax.Checked = False
        loadBlankGrid(gvFATDed)
        loadBlankGrid(gvSNFDed)
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
            If clsCommon.CompairString(clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue), "C") = CompairStringResult.Equal Then
                Dim dblLastRange As Decimal = 0
                For ii As Integer = 0 To gvFATDed.RowCount - 1
                    Dim dblRange As Decimal = clsCommon.myCdbl(gvFATDed.Rows(ii).Cells(colPer).Value)
                    If dblRange > 0 Then
                        If dblLastRange > dblRange Then
                            Throw New Exception("Please provide range in increasing order")
                        End If
                        dblLastRange = dblRange
                    End If
                Next
            End If
            Dim ArrFATDed As Dictionary(Of Decimal, Decimal) = GetDedution("FAT", gvFATDed)
            Dim ArrSNFDed As Dictionary(Of Decimal, Decimal) = GetDedution("SNF", gvSNFDed)
            Dim Rate As Decimal = CalculateRate(txtFatPer.Value, txtSnfPer.Value, ArrFATDed, ArrSNFDed)
            If Rate <> txtRate.Value Then
                txtPriceChartCode.Focus()
                txtPriceChartCode.Select()
                ErrorControl.SetError(txtPriceChartCode, "Price Chart Rate:" + clsCommon.myCstr(txtRate.Value) + " And Calulated Rate : " + clsCommon.myCstr(Rate))
                Throw New Exception("Price Chart Rate:" + clsCommon.myCstr(txtRate.Value) + " And Calulated Rate : " + clsCommon.myCstr(Rate))
            Else
                ErrorControl.ResetError(txtPriceChartCode)
            End If
            If txtMCC.arrValueMember Is Nothing OrElse txtMCC.arrValueMember.Count <= 0 Then
                txtMCC.Focus()
                txtMCC.Select()
                ErrorControl.SetError(txtMCC, "Please select MCC")
                Throw New Exception("Please select MCC")
            Else
                ErrorControl.ResetError(txtMCC)
            End If

            If txtVLC.arrValueMember Is Nothing OrElse txtVLC.arrValueMember.Count <= 0 Then
                txtVLC.Focus()
                txtVLC.Select()
                ErrorControl.SetError(txtVLC, "Please select VLC")
                Throw New Exception("Please select VLC")
            Else
                ErrorControl.ResetError(txtVLC)
            End If



            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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

                obj.GK_Min_FAT_Per = txtMinFATPer.Value
                obj.GK_Max_FAT_Per = txtMaxFATPer.Value
                obj.GK_Min_SNF_Per = txtMinSNFPer.Value
                obj.GK_Max_SNF_Per = txtMaxSNFPer.Value
                obj.GK_Is_FAT_Rate_Zero_After_Max = chkFATZeroAfterMax.Checked
                obj.GK_Is_SNF_Rate_Zero_After_Max = chkSNFZeroAfterMax.Checked

                obj.Shift = clsCommon.myCstr(CboShift.SelectedValue)
                obj.Dock_Collection_Milk_Type = clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue)

                obj.arrMCC = New List(Of clsPriceChartPlanningMCC)
                For Each str As String In txtMCC.arrValueMember
                    Dim objMCC As New clsPriceChartPlanningMCC
                    objMCC.MCC_Code = str
                    obj.arrMCC.Add(objMCC)
                Next

                obj.arrVLC = New List(Of clsPriceChartPlanningVLC)
                For Each str As String In txtVLC.arrValueMember
                    Dim objVLC As New clsPriceChartPlanningVLC
                    objVLC.VLC_Code = str
                    obj.arrVLC.Add(objVLC)
                Next

                obj.arrFATDed = New List(Of clsPriceChartPlanningFATDed)
                For ii As Integer = 0 To gvFATDed.RowCount - 1
                    Dim objTS As New clsPriceChartPlanningFATDed
                    objTS.Per = clsCommon.myCdbl(gvFATDed.Rows(ii).Cells(colPer).Value)
                    objTS.Amount = clsCommon.myCdbl(gvFATDed.Rows(ii).Cells(colAmt).Value)
                    If objTS.Amount > 0 AndAlso objTS.Per > 0 Then
                        obj.arrFATDed.Add(objTS)
                    End If
                Next


                obj.arrSNFDed = New List(Of clsPriceChartPlanningSNFDed)
                For ii As Integer = 0 To gvSNFDed.RowCount - 1
                    Dim objTS As New clsPriceChartPlanningSNFDed
                    objTS.Per = clsCommon.myCdbl(gvSNFDed.Rows(ii).Cells(colPer).Value)
                    objTS.Amount = clsCommon.myCdbl(gvSNFDed.Rows(ii).Cells(colAmt).Value)
                    If objTS.Amount > 0 AndAlso objTS.Per > 0 Then
                        obj.arrSNFDed.Add(objTS)
                    End If
                Next

                If clsPriceChartPlanning.SaveData(obj, isNewEntry) Then
                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(obj.Planning_Code, NavigatorType.Current)
                End If
            Else
                txtCode.MyReadOnly = False
                btndelete.Enabled = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
            clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully", Me.Text)
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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

                txtMinFATPer.Value = obj.GK_Min_FAT_Per
                txtMaxFATPer.Value = obj.GK_Max_FAT_Per
                txtMinSNFPer.Value = obj.GK_Min_SNF_Per
                txtMaxSNFPer.Value = obj.GK_Max_SNF_Per
                chkFATZeroAfterMax.Checked = obj.GK_Is_FAT_Rate_Zero_After_Max
                chkSNFZeroAfterMax.Checked = obj.GK_Is_SNF_Rate_Zero_After_Max


                If obj.arrFATDed IsNot Nothing AndAlso obj.arrFATDed.Count > 0 Then
                    For Each objts As clsPriceChartPlanningFATDed In obj.arrFATDed
                        gvFATDed.Rows.AddNew()
                        gvFATDed.Rows(gvFATDed.Rows.Count - 1).Cells(colPer).Value = objts.Per
                        gvFATDed.Rows(gvFATDed.Rows.Count - 1).Cells(colAmt).Value = objts.Amount
                    Next
                End If
                gvFATDed.Rows.AddNew()

                If obj.arrSNFDed IsNot Nothing AndAlso obj.arrSNFDed.Count > 0 Then
                    For Each objts As clsPriceChartPlanningSNFDed In obj.arrSNFDed
                        gvSNFDed.Rows.AddNew()
                        gvSNFDed.Rows(gvSNFDed.Rows.Count - 1).Cells(colPer).Value = objts.Per
                        gvSNFDed.Rows(gvSNFDed.Rows.Count - 1).Cells(colAmt).Value = objts.Amount
                    Next
                End If
                gvSNFDed.Rows.AddNew()

                Dim arrLst As New ArrayList
                For Each objMCC As clsPriceChartPlanningMCC In obj.arrMCC
                    arrLst.Add(objMCC.MCC_Code)
                Next
                txtMCC.arrValueMember = arrLst

                arrLst = New ArrayList
                For Each objVLC As clsPriceChartPlanningVLC In obj.arrVLC
                    arrLst.Add(objVLC.VLC_Code)
                Next
                txtVLC.arrValueMember = arrLst

                btndelete.Enabled = True
                txtCode.MyReadOnly = True
            End If
        Catch ex As Exception
            isNewEntry = True
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
        gvSNFDed.Rows.AddNew()
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
            clsCommon.MyMessageBoxShow(Me, "Fill numeric value only.", Me.Text)
        End Try
        Try
            txtSNFRatio.Text = 100 - clsCommon.myCdbl(txtFatRatio.Text)
            If (clsCommon.myCdbl(txtFatRatio.Text) + clsCommon.myCdbl(txtSNFRatio.Text)) <> 100 AndAlso clsCommon.myCdbl(txtSNFRatio.Text) > 0 Then
                txtFatRatio.Focus()
                Throw New Exception("Please Fill Ratio Of SNF And FAT." + Environment.NewLine + "There Sum Should be Equal To 100")
            End If
        Catch exx As Exception
            clsCommon.MyMessageBoxShow(Me, exx.Message, Me.Text)
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
            clsCommon.MyMessageBoxShow(Me, "Fill numeric value only.", Me.Text)
        End Try
        Try
            txtFatRatio.Text = 100 - clsCommon.myCdbl(txtSNFRatio.Text)
            If (clsCommon.myCdbl(txtFatRatio.Text) + clsCommon.myCdbl(txtSNFRatio.Text)) <> 100 AndAlso clsCommon.myCdbl(txtFatRatio.Text) > 0 Then
                txtSNFRatio.Focus()
                Throw New Exception("Please Fill Ratio Of SNF And FAT." + Environment.NewLine + "There Sum Should be Equal To 100")
            End If
        Catch exx As Exception
            clsCommon.MyMessageBoxShow(Me, exx.Message, Me.Text)
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
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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

    Private Function CalculateRate(ByVal dblFATPer As Decimal, ByVal dblSNFPer As Decimal, ByVal ArrFATDed As Dictionary(Of Decimal, Decimal), ByVal ArrSNFDed As Dictionary(Of Decimal, Decimal)) As Double
        Dim dclReturnMilkValue As Decimal = 0
        Dim dclFatPart As Decimal = 0
        Dim dclSNFPart As Decimal = 0

        If dblFATPer >= txtMinFATPer.Value Then
            Dim dclTempFatPer As Decimal = dblFATPer
            If dblFATPer > txtMaxFATPer.Value Then
                If chkFATZeroAfterMax.Checked Then
                    dclTempFatPer = 0
                Else
                    dclTempFatPer = txtMaxFATPer.Value
                End If
            End If
            dclFatPart = ((txtRate.Value * txtFatRatio.Value / txtFatPer.Value) * dclTempFatPer)
        End If

        If dblSNFPer >= txtMinSNFPer.Value Then
            Dim dclTempSNFPer As Decimal = dblSNFPer
            If dblSNFPer > txtMaxSNFPer.Value Then
                If chkSNFZeroAfterMax.Checked Then
                    dclTempSNFPer = 0
                Else
                    dclTempSNFPer = txtMaxSNFPer.Value
                End If
            End If
            dclSNFPart = ((txtRate.Value * txtSNFRatio.Value / txtSnfPer.Value) * dclTempSNFPer)
        End If
        dclReturnMilkValue = (dclFatPart + dclSNFPart) / 100
        If ArrFATDed.ContainsKey(dblFATPer) Then
            dclReturnMilkValue -= ArrFATDed.Item(dblFATPer)
        End If
        If ArrSNFDed.ContainsKey(dblSNFPer) Then
            dclReturnMilkValue -= ArrSNFDed.Item(dblSNFPer)
        End If

        dclReturnMilkValue = Math.Round(dclReturnMilkValue, 2)
        If dclReturnMilkValue < 0 Then
            dclReturnMilkValue = 0
        End If
        Return dclReturnMilkValue
    End Function

    Private Sub RadButton2_Click(sender As Object, e As EventArgs) Handles RadButton2.Click
        FillGridView()
    End Sub

    Private Function GetDedution(ByVal strFATOrSNF As String, ByVal gridView As common.UserControls.MyRadGridView) As Dictionary(Of Decimal, Decimal)
        Dim dclPer As Decimal
        Dim dclValue As Decimal
        Dim ArrDed As New Dictionary(Of Decimal, Decimal)
        For ii As Integer = 0 To gridView.RowCount - 1
            dclPer = Math.Round(clsCommon.myCdbl(gridView.Rows(ii).Cells(colPer).Value), 1, MidpointRounding.ToEven)
            dclValue = Math.Round(clsCommon.myCdbl(gridView.Rows(ii).Cells(colAmt).Value), 2, MidpointRounding.ToEven)
            If dclPer > 0 AndAlso dclValue > 0 Then
                If ArrDed.ContainsKey(dclPer) Then
                    Throw New Exception("Repeated " + strFATOrSNF + " Percentage -" + clsCommon.myCstr(dclPer) + " At Row no -" + clsCommon.myCstr(ii))
                Else
                    ArrDed.Add(dclPer, dclValue)
                End If
            End If
        Next
        Return ArrDed
    End Function

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

        Dim ArrFATDed As Dictionary(Of Decimal, Decimal) = GetDedution("FAT", gvFATDed)
        Dim ArrSNFDed As Dictionary(Of Decimal, Decimal) = GetDedution("SNF", gvSNFDed)
         
        clsCommon.ProgressBarPercentShow()
        Try
            For Row As Integer = 0 To 150
                clsCommon.ProgressBarPercentUpdate((Row * 100 / 150), "Generating FAT/SNF matrix")
                gv.Rows.AddNew()
                gv.Rows(gv.Rows.Count - 1).Cells("FATSNF").Value = clsCommon.myCstr(Row / 10)
                For Col As Integer = 0 To 150
                    Dim strColName As String = clsCommon.myCstr(Col / 10)
                    gv.Rows(gv.Rows.Count - 1).Cells(strColName).Value = CalculateRate(Row / 10, Col / 10, ArrFATDed, ArrSNFDed)
                Next
            Next
            clsCommon.ProgressBarPercentHide()
        Catch ex As Exception
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
                Dim ArrFATDed As Dictionary(Of Decimal, Decimal) = GetDedution("FAT", gvFATDed)
                Dim ArrSNFDed As Dictionary(Of Decimal, Decimal) = GetDedution("SNF", gvSNFDed)
                clsCommon.MyMessageBoxShow("Rate is - " + clsCommon.myCstr(CalculateRate(txtSearchFAT.Value, txtSearchSNF.Value, ArrFATDed, ArrSNFDed)), Me.Text)
            Else
                arrNext = New Dictionary(Of Integer, clsRowColumnTemp)()
                If txtSearchRate.Value > 0 Then
                    NextCounter = 0
                    For Row As Integer = 0 To 150
                        For Col As Integer = 0 To 150
                            If clsCommon.myCdbl(gv.Rows(Row).Cells(clsCommon.myCstr(Col / 10)).Value) = txtSearchRate.Value Then
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
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
            Dim ArrFATDed As Dictionary(Of Decimal, Decimal) = GetDedution("FAT", gvFATDed)
            Dim ArrSNFDed As Dictionary(Of Decimal, Decimal) = GetDedution("SNF", gvSNFDed)

            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try
                'Dim qry As String = "select max(code) from TSPL_FAT_SNF_UPLOADER_MASTER"
                'Dim Code As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                'If clsCommon.myLen(Code) > 0 Then
                '    Code = clsCommon.myCstr(clsCommon.incval(Code))
                'Else
                '    Code = "PCU000001"
                'End If
                Dim dtCurrent As DateTime = clsCommon.GETSERVERDATE(trans)
                Dim dtEffective As DateTime = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select Effective_Date from TSPL_MILK_PRICE_MASTER where price_code='" + txtPriceChartCode.Value + "'", trans))
                Dim Code As String = clsERPFuncationality.GetNextCode(trans, dtEffective, clsDocType.MatrixPriceChart, "", "")
                If clsCommon.myLen(Code) < 0 Then
                    Throw New Exception("Error in Code Generation")
                End If
                Dim coll As New Hashtable()
                ''---------------------FAT SNF
                For RowFAT As Integer = 0 To 150
                    For ColSNF As Integer = 0 To 150
                        coll = New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "Code", Code)

                        clsCommon.AddColumnsForChange(coll, "date", clsCommon.GetPrintDate(dtEffective, "dd/MMM/yyyy"))
                        clsCommon.AddColumnsForChange(coll, "fat", RowFAT / 10)
                        clsCommon.AddColumnsForChange(coll, "snf", ColSNF / 10)
                        clsCommon.AddColumnsForChange(coll, "rate", CalculateRate(RowFAT / 10, ColSNF / 10, ArrFATDed, ArrSNFDed))
                        clsCommon.AddColumnsForChange(coll, "Price_Code", txtPriceChartCode.Value)
                        clsCommon.AddColumnsForChange(coll, "Price_Code_Shift", clsCommon.myCstr(CboShift.SelectedValue))
                        clsCommon.AddColumnsForChange(coll, "created_by", clsCommon.myCstr(objCommonVar.CurrentUserCode))
                        clsCommon.AddColumnsForChange(coll, "created_date", clsCommon.myCstr(clsCommon.GetPrintDate(dtCurrent, "dd/MM/yyyy")))
                        clsCommon.AddColumnsForChange(coll, "modified_by", clsCommon.myCstr(objCommonVar.CurrentUserCode))
                        clsCommon.AddColumnsForChange(coll, "modified_date", clsCommon.myCstr(clsCommon.GetPrintDate(dtCurrent, "dd/MM/yyyy")))
                        clsCommon.AddColumnsForChange(coll, "Planning_Code", txtCode.Value, True)
                        clsCommon.AddColumnsForChange(coll, "Dock_Collection_Milk_Type", clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue))
                        clsCommon.AddColumnsForChange(coll, "Posted", 1)
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_FAT_SNF_UPLOADER_MASTER", OMInsertOrUpdate.Insert, "", trans)
                    Next
                Next
                ''---------------------End of FAT SNF

                ''---------------------MCC
                For Each strvalue As String In txtMCC.arrValueMember
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "code", Code)
                    clsCommon.AddColumnsForChange(coll, "mcc_code", strvalue, True)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_FAT_SNF_UPLOADER_MCC", OMInsertOrUpdate.Insert, "", trans)
                Next
                ''---------------------Enf of MCC

                ''---------------------VLC MCC
                For Each strvalue As String In txtVLC.arrValueMember
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "code", Code)
                    clsCommon.AddColumnsForChange(coll, "vlc_code", strvalue, True)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_FAT_SNF_UPLOADER_VLC", OMInsertOrUpdate.Insert, "", trans)
                Next
                ''---------------------Enf of VLC

                coll = New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Status", 1)
                clsCommon.AddColumnsForChange(coll, "Posted_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Posted_Date", clsCommon.myCstr(clsCommon.GetPrintDate(dtCurrent, "dd/MMM/yyyy hh:mm:ss tt")))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PRICE_CHART_PLANNING", OMInsertOrUpdate.Update, "Planning_Code='" + txtCode.Value + "'", trans)

                trans.Commit()
                clsCommon.MyMessageBoxShow(Me, "Data Posted Successfully", Me.Text)
                LoadData(txtCode.Value, NavigatorType.Current)
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub txtMCC__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
        Dim qry As String = "select MCC_Code as Code ,MCC_NAME as Name from TSPL_MCC_MASTER"
        txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("PCPMCC", qry, "Code", "", txtMCC.arrValueMember, Nothing)
        txtVLC.arrValueMember = Nothing
    End Sub

    Private Sub txtVLC__My_Click(sender As Object, e As EventArgs) Handles txtVLC._My_Click
        If txtMCC.arrValueMember Is Nothing OrElse txtMCC.arrValueMember.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select MCC", Me.Text)
            Exit Sub
        End If

        Dim qry As String = "select VLC_Code as Code ,VLC_Name as Name from TSPL_VLC_MASTER_HEAD  where mcc in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") and TSPL_VLC_MASTER_HEAD.Active=1"
        txtVLC.arrValueMember = clsCommon.ShowMultipleSelectForm("PCPMCC", qry, "Code", "", txtVLC.arrValueMember, Nothing)
    End Sub

    Private Sub gvTS_UserDeletedRow(sender As Object, e As GridViewRowEventArgs) Handles gvFATDed.UserDeletedRow
    End Sub

    Private Sub gvTS_UserDeletingRow(sender As Object, e As GridViewRowCancelEventArgs) Handles gvFATDed.UserDeletingRow
        If Not myMessages.deleteConfirm() Then
            e.Cancel = True
        End If
    End Sub

    Private Sub gvSNF_UserDeletedRow(sender As Object, e As GridViewRowEventArgs) Handles gvSNFDed.UserDeletedRow
    End Sub

    Private Sub gvSNF_UserDeletingRow(sender As Object, e As GridViewRowCancelEventArgs) Handles gvSNFDed.UserDeletingRow
        If Not myMessages.deleteConfirm() Then
            e.Cancel = True
        End If
    End Sub

    Sub loadBlankGrid(ByVal gv As common.UserControls.MyRadGridView)
        Try
            gv.Rows.Clear()
            gv.Columns.Clear()
            Dim repoDeciCol As GridViewDecimalColumn
            repoDeciCol = New GridViewDecimalColumn()
            repoDeciCol.Name = colPer
            repoDeciCol.Width = 100
            repoDeciCol.DecimalPlaces = 1
            repoDeciCol.Minimum = 0
            repoDeciCol.Maximum = 100
            repoDeciCol.Step = 0
            repoDeciCol.ShowUpDownButtons = False
            repoDeciCol.HeaderText = "Percent"
            gv.MasterTemplate.Columns.Add(repoDeciCol)

            repoDeciCol = New GridViewDecimalColumn()
            repoDeciCol.Name = colAmt
            repoDeciCol.Width = 100
            repoDeciCol.DecimalPlaces = 2
            repoDeciCol.Minimum = 0
            repoDeciCol.Step = 0
            repoDeciCol.ShowUpDownButtons = False
            repoDeciCol.HeaderText = "Deduction Amount"
            gv.MasterTemplate.Columns.Add(repoDeciCol)

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
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
    End Sub

    Private Sub gvSNFDed_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvSNFDed.CurrentColumnChanged
        If gvSNFDed.RowCount > 0 Then
            Dim intCurrRow As Integer = gvSNFDed.CurrentRow.Index
            If intCurrRow = gvSNFDed.Rows.Count - 1 Then
                gvSNFDed.Rows.AddNew()
                gvSNFDed.CurrentRow = gvSNFDed.Rows(intCurrRow)
            End If
        End If
    End Sub
End Class