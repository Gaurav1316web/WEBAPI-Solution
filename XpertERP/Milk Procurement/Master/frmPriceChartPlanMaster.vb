'--------Created By Monika 29/05/2014
'-----------------BM00000003159----------Monika
Imports common
Imports System.Data.SqlClient
Public Class frmPriceChartPlanMaster
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim ErrorControl As clsErrorControl = New clsErrorControl()
    Dim isNewEntry As Boolean = True
#End Region

    Public Sub New()
        InitializeComponent()
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
        txtMCC.arrValueMember = Nothing
        txtVLC.arrValueMember = Nothing
        txtSingleAxisFatPer.Value = 0
        txtSingleAxisSnfPer.Value = 0
        txtSingleAxisSnfDedFatPer.Value = 0
        txtSingleAxisSnfDedSnfPer.Value = 0
        txtDoubleAxisFatPer.Value = 0
        txtDoubleAxisSnfPer.Value = 0
        txtDoubleAxisCreamBaseFatPer.Value = 0
        txtDoubleAxisCreamBaseSnfPer.Value = 0
        txtDoubleAxisCreamBaseDeductionPer.Value = 0
        UsLock1.Status = ERPTransactionStatus.Pending
        isNewEntry = True
        txtCode.MyReadOnly = False
        btndelete.Enabled = False
        btnPost.Enabled = True
        btnsave.Enabled = True
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

            If clsCommon.myLen(txtSingleAxisFatPer.Value) <= 0 Then
                txtSingleAxisFatPer.Focus()
                txtSingleAxisFatPer.Select()
                ErrorControl.SetError(txtSingleAxisFatPer, "Please Enter Single Axis FAT %")
                Throw New Exception("Please Enter Single Axis FAT %")
            Else
                ErrorControl.ResetError(txtSingleAxisFatPer)
            End If

            If clsCommon.myLen(txtSingleAxisSnfPer.Value) <= 0 Then
                txtSingleAxisSnfPer.Focus()
                txtSingleAxisSnfPer.Select()
                ErrorControl.SetError(txtSingleAxisSnfPer, "Please Enter Single Axis SNF %")
                Throw New Exception("Please Enter Single Axis SNF %")
            Else
                ErrorControl.ResetError(txtSingleAxisSnfPer)
            End If

            If clsCommon.myLen(txtDoubleAxisCreamBaseFatPer.Value) <= 0 Then
                txtDoubleAxisCreamBaseFatPer.Focus()
                txtDoubleAxisCreamBaseFatPer.Select()
                ErrorControl.SetError(txtDoubleAxisCreamBaseFatPer, "Please Enter FAT % of Double Axis cream Base Deduction")
                Throw New Exception("Please Enter FAT % of Double Axis cream Base Deduction")
            Else
                ErrorControl.ResetError(txtDoubleAxisCreamBaseFatPer)
            End If

            If clsCommon.myLen(txtDoubleAxisCreamBaseSnfPer.Value) <= 0 Then
                txtDoubleAxisCreamBaseSnfPer.Focus()
                txtDoubleAxisCreamBaseSnfPer.Select()
                ErrorControl.SetError(txtDoubleAxisCreamBaseSnfPer, "Please Enter SNF % of Double Axis cream Base Deduction")
                Throw New Exception("Please Enter SNF % of Double Axis cream Base Deduction")
            Else
                ErrorControl.ResetError(txtDoubleAxisCreamBaseSnfPer)
            End If

            If clsCommon.myLen(txtDoubleAxisCreamBaseDeductionPer.Value) <= 0 Then
                txtDoubleAxisCreamBaseDeductionPer.Focus()
                txtDoubleAxisCreamBaseDeductionPer.Select()
                ErrorControl.SetError(txtDoubleAxisCreamBaseDeductionPer, "Please Enter Deduction % of Double Axis cream Base Deduction")
                Throw New Exception("Please Enter Deduction % of Double Axis cream Base Deduction")
            Else
                ErrorControl.ResetError(txtDoubleAxisCreamBaseDeductionPer)
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
                obj.Single_Axis_FAT_Per = txtSingleAxisFatPer.Value
                obj.Single_Axis_SNF_Per = txtSingleAxisSnfPer.Value
                obj.Single_Axis_SNFDed_FAT_Per = txtSingleAxisSnfDedFatPer.Value
                obj.Single_Axis_SNFDed_SNF_Per = txtSingleAxisSnfDedSnfPer.Value
                obj.Double_Axis_FAT_Per = txtDoubleAxisFatPer.Value
                obj.Double_Axis_SNF_Per = txtDoubleAxisSnfPer.Value
                obj.Double_Axis_Ded_FAT_Per = txtDoubleAxisCreamBaseFatPer.Value
                obj.Double_Axis_Ded_SNF_Per = txtDoubleAxisCreamBaseSnfPer.Value
                obj.Double_Axis_Ded_Per = txtDoubleAxisCreamBaseDeductionPer.Value
                obj.Shift = clsCommon.myCstr(CboShift.SelectedValue)

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
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            btnnew.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            btnsave.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            btndelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub

    Private Sub FrmPriceChartMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadShift()
        Reset()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        RadPageView1.SelectedPage = RadPageViewPage1
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
                txtSingleAxisFatPer.Value = obj.Single_Axis_FAT_Per
                txtSingleAxisSnfPer.Value = obj.Single_Axis_SNF_Per
                txtSingleAxisSnfDedFatPer.Value = obj.Single_Axis_SNFDed_FAT_Per
                txtSingleAxisSnfDedSnfPer.Value = obj.Single_Axis_SNFDed_SNF_Per
                txtDoubleAxisFatPer.Value = obj.Double_Axis_FAT_Per
                txtDoubleAxisSnfPer.Value = obj.Double_Axis_SNF_Per
                txtDoubleAxisCreamBaseFatPer.Value = obj.Double_Axis_Ded_FAT_Per
                txtDoubleAxisCreamBaseSnfPer.Value = obj.Double_Axis_Ded_SNF_Per
                txtDoubleAxisCreamBaseDeductionPer.Value = obj.Double_Axis_Ded_Per
                CboShift.SelectedValue = obj.Shift

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
            txtSNFRatio.Text = 100 - clsCommon.myCdbl(txtFatRatio.Text)
            If (clsCommon.myCdbl(txtFatRatio.Text) + clsCommon.myCdbl(txtSNFRatio.Text)) <> 100 AndAlso clsCommon.myCdbl(txtSNFRatio.Text) > 0 Then
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
            txtFatRatio.Text = 100 - clsCommon.myCdbl(txtSNFRatio.Text)
            If (clsCommon.myCdbl(txtFatRatio.Text) + clsCommon.myCdbl(txtSNFRatio.Text)) <> 100 AndAlso clsCommon.myCdbl(txtFatRatio.Text) > 0 Then
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

    Private Sub txtSAFatPer_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtSingleAxisFatPer.Validating
        txtSingleAxisSnfDedFatPer.Value = txtSingleAxisFatPer.Value
        txtDoubleAxisFatPer.Value = txtSingleAxisFatPer.Value
    End Sub

    Private Sub txtDoubleAxisCreamBaseSnfPer_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtDoubleAxisCreamBaseSnfPer.Validating
        txtSingleAxisSnfDedSnfPer.Value = txtDoubleAxisCreamBaseSnfPer.Value
        txtDoubleAxisSnfPer.Value = txtDoubleAxisCreamBaseSnfPer.Value
    End Sub

    Private Function CalculateRate(ByVal dblFATPer, ByVal dblSNFPer) As Double
        Dim dblStdFATKg As Decimal = 100 / txtFatPer.Value * txtRate.Value
        Dim dblStdFATRate As Decimal = txtFatRatio.Value / txtFatPer.Value * txtRate.Value
        Dim dblStdSNFRate As Decimal = txtSNFRatio.Value / txtSnfPer.Value * txtRate.Value

        Dim dblQty As Decimal = 20
        Dim dblCalFatKg As Decimal = dblQty * dblFATPer / 100
        Dim dblCalSNFKg As Decimal = dblQty * dblSNFPer / 100

        Dim dclReturnMilkValue As Decimal = 0
        If dblFATPer < txtDoubleAxisCreamBaseFatPer.Value OrElse dblSNFPer < txtDoubleAxisCreamBaseSnfPer.Value Then
            'clsCommon.MyMessageBoxShow("Double Axis with Cream Base")
            Dim dblCreaBaseAmt As Decimal = (((dblStdFATRate * dblCalFatKg) + (dblStdSNFRate * dblCalSNFKg)) * txtDoubleAxisCreamBaseDeductionPer.Value / 100)
            dclReturnMilkValue = ((dblStdFATRate * dblCalFatKg) + (dblStdSNFRate * dblCalSNFKg)) - dblCreaBaseAmt
        ElseIf (dblFATPer < txtDoubleAxisFatPer.Value AndAlso dblFATPer >= txtDoubleAxisCreamBaseFatPer.Value) OrElse dblSNFPer < txtDoubleAxisSnfPer.Value Then
            'clsCommon.MyMessageBoxShow("Double Axis ")
            dclReturnMilkValue = ((dblStdFATRate * dblCalFatKg) + (dblStdSNFRate * dblCalSNFKg))
        ElseIf dblFATPer >= txtSingleAxisSnfDedFatPer.Value AndAlso dblSNFPer >= txtSingleAxisSnfDedSnfPer.Value AndAlso dblSNFPer < txtSingleAxisSnfPer.Value Then
            'clsCommon.MyMessageBoxShow("Single Axis SNF Deduction")
            Dim dblSNFDeduction As Decimal = (((txtSnfPer.Value - dblSNFPer) * dblQty / 100) * dblStdSNFRate)
            dclReturnMilkValue = dblCalFatKg * dblStdFATKg - dblSNFDeduction
        ElseIf dblFATPer >= txtSingleAxisSnfDedFatPer.Value AndAlso dblSNFPer >= txtSingleAxisSnfPer.Value Then
            'clsCommon.MyMessageBoxShow("Single Axis")
            dclReturnMilkValue = dblCalFatKg * dblStdFATKg
        Else
            clsCommon.MyMessageBoxShow("Method not exists")
        End If
        If dclReturnMilkValue < 0 Then
            dclReturnMilkValue = 0
        End If
        dclReturnMilkValue = Math.Round(dclReturnMilkValue / dblQty, 2, MidpointRounding.ToEven)
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
    Dim arrNext As Dictionary(Of Integer, clsRowColumnTemp)
    Dim NextCounter As Integer = 0

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
                        clsCommon.AddColumnsForChange(coll, "rate", CalculateRate(RowFAT / 10, ColSNF / 10))
                        clsCommon.AddColumnsForChange(coll, "Price_Code", txtPriceChartCode.Value)
                        clsCommon.AddColumnsForChange(coll, "Price_Code_Shift", clsCommon.myCstr(CboShift.SelectedValue))
                        clsCommon.AddColumnsForChange(coll, "created_by", clsCommon.myCstr(objCommonVar.CurrentUserCode))
                        clsCommon.AddColumnsForChange(coll, "created_date", clsCommon.myCstr(clsCommon.GetPrintDate(dtCurrent, "dd/MM/yyyy")))
                        clsCommon.AddColumnsForChange(coll, "modified_by", clsCommon.myCstr(objCommonVar.CurrentUserCode))
                        clsCommon.AddColumnsForChange(coll, "modified_date", clsCommon.myCstr(clsCommon.GetPrintDate(dtCurrent, "dd/MM/yyyy")))
                        clsCommon.AddColumnsForChange(coll, "Planning_Code", txtCode.Value, True)
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

    Private Sub txtMCC__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
        Dim qry As String = "select MCC_Code as Code ,MCC_NAME as Name from TSPL_MCC_MASTER"
        txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("PCPMCC", qry, "Code", "", txtMCC.arrValueMember, Nothing)
        txtVLC.arrValueMember = Nothing
    End Sub

    Private Sub txtVLC__My_Click(sender As Object, e As EventArgs) Handles txtVLC._My_Click
        If txtMCC.arrValueMember Is Nothing OrElse txtMCC.arrValueMember.Count <= 0 Then
            clsCommon.MyMessageBoxShow("Please select MCC", Me.Text)
            Exit Sub
        End If

        Dim qry As String = "select VLC_Code as Code ,VLC_Name as Name from TSPL_VLC_MASTER_HEAD  where mcc in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") and TSPL_VLC_MASTER_HEAD.Active=1"
        txtVLC.arrValueMember = clsCommon.ShowMultipleSelectForm("PCPMCC", qry, "Code", "", txtVLC.arrValueMember, Nothing)
    End Sub
End Class


