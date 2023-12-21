Imports common
Imports System.Data.SqlClient

Public Class frmIncetiveEntryBySRN
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim ButtonTooltip As New ToolTip()
    Dim DtIncentive As DataTable
    Dim Formcode As String
    Dim Is_Load As Boolean = False
    Dim AllowDateChanged As Boolean = False

    Dim IsNewEntry As Boolean = False
    Dim isInsideLoadData As Boolean = False

    Const colInvSNo As String = "colInvSNo"
    Const colInvVSPCode As String = "colInvVSPCode"
    Const colInvVSPName As String = "colInvVSPName"
    Const colInvInvoiceNo As String = "colInvInvoiceNo"
    Const colInvAmount As String = "colInvAmount"
    Const colVSPCode As String = "colVSPCode"
    Const colVSPName As String = "colVSPName"
    Const colAccNo As String = "colAccNo"
    Const colVLCCode As String = "colVLCCode"
    Const colVLCName As String = "colVLCName"
    Const colMonthlyRentAmount As String = "colMonthlyRentAmount"
    Const colMilkReceivedDays As String = "colMilkReceivedDays"
    Const colRentAmount As String = "colRentAmount"
    Const colArrear As String = "colArrear"
    Const colDeduction As String = "colDeduction"
    Const colIncentiveCode As String = "colIncentiveCode"
    Const colIncentiveQty As String = "colIncentiveQty"
    Const colIncentiveUOM As String = "colIncentiveUOM"
    Const colIncentiveRate As String = "colIncentiveRate"
    Const colIncentiveAmount As String = "colIncentiveAmount"
    Const colAmount As String = "colAmount"
    ' colVSPBankCode , colVSPBankName, colVSPBranchName, colVSPIfscCode
    Const colVSPBankCode As String = "colVSPBankCode"
    Const colVSPBankName As String = "colVSPBankName"
    Const colVSPBranchName As String = "colVSPBranchName"
    Const colVSPIfscCode As String = "colVSPIfscCode"

    Const colCreditNoteNo As String = "colCreditNoteNo"
    Const colDebitNoteNo As String = "colDebitNoteNo"
    Const colPaymentNo As String = "colPaymentNo"

    Dim settIncentiveProcessPaymentStartDate As String = ""
    Dim settIncetiveApplyArrear As Boolean = True
#End Region

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        RadButton3.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmMilkVSPPayment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        settIncetiveApplyArrear = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IncetiveEntryApplyArrear, clsFixedParameterCode.IncetiveEntryApplyArrear, Nothing)) > 0)
        settIncentiveProcessPaymentStartDate = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.IncentiveProcessPaymentStartDate, clsFixedParameterCode.IncentiveProcessPaymentStartDate, Nothing))
        Is_Load = True
        ButtonTooltip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction")
        ButtonTooltip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonTooltip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
        ButtonTooltip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonTooltip.SetToolTip(btnnew, "Press Alt+N Adding New Trasnaction")

        SetUserMgmtNew()
        txtMonth.Value = clsCommon.GETSERVERDATE()
        Is_Load = False
        AllowDateChanged = True
        AddNew()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub FrmMilkVSPPayment_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            DeleteData()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "SIRC"
            frm.strCode = "SIReversAndCreate"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnReverse.Visible = True
            End If
        End If
    End Sub

    Private Sub txtMCC__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtMCC._MYValidating
        Dim qry As String = ""
        Dim arrLoc As String = ""
        Dim obj As New clsMCCCodes()
        obj = clsMCCCodes.GetData(True)
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
            arrLoc = obj.arrLocCodes
        End If

        qry = "select * from ( select Mcc_Code as [Code],MCC_Name as [Name] from tspl_mcc_master inner join tspl_location_master on location_Code= mcc_Code " _
        & " and (loc_segment_Code in (" & arrLoc & ") or mcc_Code in (" & arrLoc & ")))xx "

        txtMCC.Value = clsCommon.ShowSelectForm("VSPPMCCInEn", qry, "Code", "", txtMCC.Value, "", isButtonClicked)
        lblMCC.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MCC_Name from tspl_mcc_master where mcc_Code='" + txtMCC.Value + "'"))
        gv1.DataSource = Nothing
    End Sub

    Private Sub txtVSP__My_Click(sender As Object, e As EventArgs) Handles txtVSP._My_Click
        Try
            If clsCommon.myLen(txtMCC.Value) <= 0 Then
                txtMCC.Focus()
                Throw New Exception("Please select MCC")
            End If

            Dim qry As String = "select xx.VSP_CODE as Code,TSPL_VENDOR_MASTER.Vendor_Name as Name from (" + Environment.NewLine +
            " select VSP_CODE from TSPL_MILK_SRN_HEAD where  MCC_CODE= '" + txtMCC.Value + "'" + Environment.NewLine + _
            " and datepart(YEAR,TSPL_MILK_SRN_HEAD.DOC_DATE)='" + clsCommon.myCstr(txtMonth.Value.Year) + "' and datepart(MONTH,TSPL_MILK_SRN_HEAD.DOC_DATE)='" + clsCommon.myCstr(txtMonth.Value.Month) + "' " + Environment.NewLine + _
            " group by VSP_CODE " + Environment.NewLine + _
            " )xx " + Environment.NewLine +
            " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=xx.VSP_CODE " + Environment.NewLine +
            " order by xx.VSP_CODE"
            txtVSP.arrValueMember = clsCommon.ShowMultipleSelectForm(False, "PPfPVisrn", qry, "Code", "", txtVSP.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub RadButton2_Click(sender As Object, e As EventArgs) Handles RadButton2.Click
        LoadBlankGrid()
        EnableDisableFilter(True)
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        FillData()
    End Sub

    Sub EnableDisableFilter(ByVal isEnable As Boolean)
        txtMCC.Enabled = isEnable
        txtMonth.Enabled = isEnable
        txtVSP.Enabled = isEnable
    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoString As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "VSP Code"
        repoString.WrapText = True
        repoString.Name = colVSPCode
        repoString.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoString.TextImageRelation = TextImageRelation.TextBeforeImage
        repoString.Width = 100
        repoString.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoString)



        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "VLC Code"
        repoString.WrapText = True
        repoString.Name = colVLCCode
        repoString.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoString.TextImageRelation = TextImageRelation.TextBeforeImage
        repoString.Width = 130
        repoString.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "VLC Name"
        repoString.WrapText = True
        repoString.Name = colVLCName
        repoString.Width = 150
        repoString.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoString)

        Dim repoDecimal As GridViewDecimalColumn
        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "No of Days"
        repoDecimal.Name = colMilkReceivedDays
        repoDecimal.WrapText = True
        repoDecimal.Minimum = 0
        repoDecimal.Width = 100
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDecimal.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDecimal)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Incentive Code"
        repoString.WrapText = True
        repoString.Name = colIncentiveCode
        repoString.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoString.TextImageRelation = TextImageRelation.TextBeforeImage
        repoString.IsVisible = False
        repoString.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoString)


        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Incentive Qty"
        repoDecimal.WrapText = True
        repoDecimal.Name = colIncentiveQty
        repoDecimal.Minimum = 0
        repoDecimal.Width = 100
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDecimal.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDecimal)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Incentive UOM"
        repoString.WrapText = True
        repoString.Name = colIncentiveUOM
        repoString.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoString.TextImageRelation = TextImageRelation.TextBeforeImage
        repoString.IsVisible = False
        repoString.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoString)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Incentive Rate"
        repoDecimal.WrapText = True
        repoDecimal.Name = colIncentiveRate
        repoDecimal.Minimum = 0
        repoDecimal.Width = 100
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDecimal.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDecimal)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Incentive Amount"
        repoDecimal.WrapText = True
        repoDecimal.Name = colIncentiveAmount
        repoDecimal.Minimum = 0
        repoDecimal.Width = 100
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDecimal.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDecimal)


        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Monthly Rent Amount"
        repoDecimal.Name = colMonthlyRentAmount
        repoDecimal.WrapText = True
        repoDecimal.Minimum = 0
        repoDecimal.Width = 100
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDecimal.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDecimal)



        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Rent Amount"
        repoDecimal.Name = colRentAmount
        repoDecimal.WrapText = True
        repoDecimal.Minimum = 0
        repoDecimal.Width = 100
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDecimal.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDecimal)

        If settIncetiveApplyArrear Then
            repoDecimal = New GridViewDecimalColumn()
            repoDecimal.FormatString = ""
            repoDecimal.HeaderText = "Arrear"
            repoDecimal.Name = colArrear
            repoDecimal.WrapText = True
            repoDecimal.Minimum = 0
            repoDecimal.Width = 100
            repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            repoDecimal.ReadOnly = False
            gv1.MasterTemplate.Columns.Add(repoDecimal)

            repoDecimal = New GridViewDecimalColumn()
            repoDecimal.FormatString = ""
            repoDecimal.HeaderText = "Deduction"
            repoDecimal.Name = colDeduction
            repoDecimal.WrapText = True
            repoDecimal.Minimum = 0
            repoDecimal.Width = 100
            repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            repoDecimal.ReadOnly = False
            gv1.MasterTemplate.Columns.Add(repoDecimal)
        End If



        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Amount"
        repoDecimal.Name = colAmount
        repoDecimal.WrapText = True
        repoDecimal.Minimum = 0
        repoDecimal.Width = 100
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDecimal.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDecimal)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "VSP Name"
        repoString.WrapText = True
        repoString.Name = colVSPName
        repoString.Width = 150
        repoString.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "VSP Account No"
        repoString.WrapText = True
        repoString.Name = colAccNo
        repoString.Width = 150
        repoString.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "VSP IFSC Code"
        repoString.WrapText = True
        repoString.Name = colVSPIfscCode
        repoString.Width = 150
        repoString.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "VSP Bank Code"
        repoString.WrapText = True
        repoString.Name = colVSPBankCode
        repoString.Width = 150
        repoString.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoString)
        ' colVSPBankCode , colVSPBankName, colVSPBranchName, colVSPIfscCode
        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "VSP Bank Name"
        repoString.WrapText = True
        repoString.Name = colVSPBankName
        repoString.Width = 150
        repoString.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "VSP Branch Name"
        repoString.WrapText = True
        repoString.Name = colVSPBranchName
        repoString.Width = 150
        repoString.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoString)


        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Credit Note No"
        repoString.WrapText = True
        repoString.Name = colCreditNoteNo
        repoString.Width = 150
        repoString.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Debit Note No"
        repoString.WrapText = True
        repoString.Name = colDebitNoteNo
        repoString.Width = 150
        repoString.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Payment No"
        repoString.WrapText = True
        repoString.Name = colPaymentNo
        repoString.Width = 150
        repoString.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoString)



        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim Qty1 As New GridViewSummaryItem(colIncentiveAmount, "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Qty1)
        Dim Qty2 As New GridViewSummaryItem(colMonthlyRentAmount, "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Qty2)
        Dim Qty3 As New GridViewSummaryItem(colRentAmount, "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Qty3)

        If settIncetiveApplyArrear Then
            Dim Qty31 As New GridViewSummaryItem(colArrear, "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Qty31)

            Dim Qty32 As New GridViewSummaryItem(colDeduction, "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Qty32)
        End If

        Dim Qty4 As New GridViewSummaryItem(colAmount, "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Qty4)
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.EnableFiltering = True
        gv1.AllowDeleteRow = False
    End Sub

    Sub FillData()
        Try
            If clsCommon.myLen(txtMCC.Value) <= 0 Then
                txtMCC.Focus()
                Throw New Exception("Please select MCC")
            End If
            If txtVSP.arrValueMember Is Nothing OrElse txtVSP.arrValueMember.Count <= 0 Then
                txtVSP.Focus()
                Throw New Exception("Please select VSP")
            End If
            LoadBlankGrid()
            LoadDetailData(clsIncentiveEntryBySRNDetail.GetCalculateIncentive(txtCode.Value, txtMCC.Value, txtMonth.Value, txtVSP.arrValueMember))
            EnableDisableFilter(False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        AddNew()
    End Sub

    Sub AddNew()
        btnsave.Enabled = True
        btnPost.Enabled = True
        RadButton3.Enabled = False
        btndelete.Enabled = True

        IsNewEntry = True
        txtCode.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtMCC.Value = ""
        lblMCC.Text = ""
        txtMonth.Value = txtDate.Value
        txtVSP.arrValueMember = Nothing
        txtVSP_Print.arrValueMember = Nothing
        gb_PrintVSPWiseIncentive.Enabled = False
        UsLock1.Status = ERPTransactionStatus.Pending
        LoadBlankGrid()
        EnableDisableFilter(True)
        txtDate.Enabled = True
    End Sub

    Sub SaveData()
        Try
            For ii As Integer = 0 To gv1.Rows.Count - 1
                UpdateCurrentRow(ii)
            Next

            Dim obj As clsIncentiveEntryBySRNHead = New clsIncentiveEntryBySRNHead
            obj.Doc_Code = txtCode.Value
            obj.Doc_Date = txtDate.Value
            obj.MCC_Code = txtMCC.Value
            obj.Filter_Month = txtMonth.Value

            obj.arr = New List(Of clsIncentiveEntryBySRNDetail)
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim objtr As New clsIncentiveEntryBySRNDetail()
                objtr.Vsp_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colVSPCode).Value)
                objtr.Monthly_Rent_Amount = clsCommon.myCdbl(gv1.Rows(ii).Cells(colMonthlyRentAmount).Value)
                objtr.Milk_Received_Days = clsCommon.myCdbl(gv1.Rows(ii).Cells(colMilkReceivedDays).Value)
                objtr.Rent_Amount = clsCommon.myCdbl(gv1.Rows(ii).Cells(colRentAmount).Value)
                If settIncetiveApplyArrear Then
                    objtr.Arrear = clsCommon.myCdbl(gv1.Rows(ii).Cells(colArrear).Value)
                    objtr.Deduction = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDeduction).Value)
                End If
                objtr.Incentive_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colIncentiveCode).Value)

                objtr.Incentive_Qty = clsCommon.myCstr(gv1.Rows(ii).Cells(colIncentiveQty).Value)
                objtr.Incentive_UOM = clsCommon.myCstr(gv1.Rows(ii).Cells(colIncentiveUOM).Value)
                objtr.Incentive_Rate = clsCommon.myCstr(gv1.Rows(ii).Cells(colIncentiveRate).Value)

                objtr.Incentive_Amount = clsCommon.myCdbl(gv1.Rows(ii).Cells(colIncentiveAmount).Value)
                objtr.Amount = clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmount).Value)
                obj.arr.Add(objtr)
            Next
            obj.SaveData(obj, IsNewEntry)
            clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
            LoadData(obj.Doc_Code, NavigatorType.Current)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            AddNew()
            Dim obj As New clsIncentiveEntryBySRNHead()
            obj = clsIncentiveEntryBySRNHead.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Doc_Code) > 0) Then
                If obj.Is_Post = ERPTransactionStatus.Posted Then
                    btnsave.Enabled = False
                    btnPost.Enabled = False
                    RadButton3.Enabled = True
                    btndelete.Enabled = False
                    txtDate.Enabled = False
                End If
                gb_PrintVSPWiseIncentive.Enabled = True
                IsNewEntry = False
                txtCode.Value = obj.Doc_Code
                txtDate.Value = obj.Doc_Date
                txtMCC.Value = obj.MCC_Code
                lblMCC.Text = obj.MCC_Name
                UsLock1.Status = obj.Is_Post
                txtMonth.Value = obj.Filter_Month
                txtVSP.arrValueMember = obj.arrVSP
                txtVSP_Print.arrValueMember = obj.arrVSP
                LoadDetailData(obj.arr)
                EnableDisableFilter(False)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Sub LoadDetailData(ByVal Arr As List(Of clsIncentiveEntryBySRNDetail))
        If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
            For Each objTr As clsIncentiveEntryBySRNDetail In Arr
                gv1.Rows.AddNew()
                gv1.Rows(gv1.Rows.Count - 1).Cells(colVSPCode).Value = objTr.Vsp_Code
                gv1.Rows(gv1.Rows.Count - 1).Cells(colVSPName).Value = objTr.Vsp_Name
                gv1.Rows(gv1.Rows.Count - 1).Cells(colAccNo).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_VENDOR_MASTER.Account_No,'') as AccNo from TSPL_VENDOR_MASTER where vendor_code='" & clsCommon.myCstr(objTr.Vsp_Code) & "'"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colVSPBankCode).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_VENDOR_MASTER.Bank_Code,'') as Bank_Code from TSPL_VENDOR_MASTER where vendor_code='" & clsCommon.myCstr(objTr.Vsp_Code) & "'"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colVSPBankName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_VENDOR_MASTER.Bank_Code_Desc,'') as Bank_Code_Desc from TSPL_VENDOR_MASTER where vendor_code='" & clsCommon.myCstr(objTr.Vsp_Code) & "'"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colVSPBranchName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_VENDOR_MASTER.Branch_Name,'') as Branch_Name from TSPL_VENDOR_MASTER where vendor_code='" & clsCommon.myCstr(objTr.Vsp_Code) & "'"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colVSPIfscCode).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_VENDOR_MASTER.IFSC_CODE,'') as IFCI_Code from TSPL_VENDOR_MASTER where vendor_code='" & clsCommon.myCstr(objTr.Vsp_Code) & "'"))

                gv1.Rows(gv1.Rows.Count - 1).Cells(colVLCCode).Value = objTr.VLC_Code
                gv1.Rows(gv1.Rows.Count - 1).Cells(colVLCName).Value = objTr.VLC_Name
                gv1.Rows(gv1.Rows.Count - 1).Cells(colMonthlyRentAmount).Value = objTr.Monthly_Rent_Amount
                gv1.Rows(gv1.Rows.Count - 1).Cells(colMilkReceivedDays).Value = objTr.Milk_Received_Days
                gv1.Rows(gv1.Rows.Count - 1).Cells(colRentAmount).Value = objTr.Rent_Amount
                If settIncetiveApplyArrear Then
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colArrear).Value = objTr.Arrear
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDeduction).Value = objTr.Deduction
                End If
                gv1.Rows(gv1.Rows.Count - 1).Cells(colIncentiveCode).Value = objTr.Incentive_Code
                gv1.Rows(gv1.Rows.Count - 1).Cells(colIncentiveQty).Value = objTr.Incentive_Qty
                gv1.Rows(gv1.Rows.Count - 1).Cells(colIncentiveUOM).Value = objTr.Incentive_UOM
                gv1.Rows(gv1.Rows.Count - 1).Cells(colIncentiveRate).Value = objTr.Incentive_Rate
                gv1.Rows(gv1.Rows.Count - 1).Cells(colIncentiveAmount).Value = objTr.Incentive_Amount
                gv1.Rows(gv1.Rows.Count - 1).Cells(colAmount).Value = objTr.Amount
                gv1.Rows(gv1.Rows.Count - 1).Cells(colCreditNoteNo).Value = objTr.CreditNoteNo
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDebitNoteNo).Value = objTr.DebitNoteNo
                gv1.Rows(gv1.Rows.Count - 1).Cells(colPaymentNo).Value = objTr.PaymentNo
            Next
        End If
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("No Document found to delete")
            End If

            If (myMessages.deleteConfirm()) Then
                If clsIncentiveEntryBySRNHead.DeleteData(txtCode.Value) Then
                    clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            If (clsCommon.myLen(txtCode.Value) > 0 AndAlso myMessages.postConfirm()) Then
                If (clsIncentiveEntryBySRNHead.PostData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtCode._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD where Doc_Code='" + txtCode.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        Dim qry As String = "select TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.Doc_Code as Document,TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.Doc_Date as [Document Date],TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.MCC_Code as [MCC Code],TSPL_MCC_MASTER.MCC_NAME as [MCC Name],case when TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.Is_Post=1 then 'Approved' else 'Pending' end as Status " + Environment.NewLine + _
        "from TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD" + Environment.NewLine + _
        "left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.MCC_Code"

        Dim whrClas As String = ""
        LoadData(clsCommon.ShowSelectForm("incenentmf", qry, "Document", whrClas, txtCode.Value, "", isButtonClicked), NavigatorType.Current)
    End Sub
    ' Ticket No : ERO/07/06/19-000639 By prabhakar
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Document No found.", Me.Text)
                Return
            End If
            Dim frmCRV As New frmCrystalReportViewer()
            Dim qry As String = " Select * from ( " &
                                " select '1' as CopyType,  row_number() over(partition by TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Vsp_Code order by TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Vsp_Code ) as SNo, TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.Doc_Code as Sale_Invoice_No, case when TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.Is_Post = 1 then 'Posted' else 'Pending' end Document_Status , " &
                                " TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2, TSPL_COMPANY_MASTER.Comp_code ,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 as Comp_Add1,TSPL_COMPANY_MASTER.Add2 as Comp_Add2, TSPL_COMPANY_MASTER.Add3 as Comp_Add3,TSPL_COMPANY_MASTER.City_Code as Comp_City_Code,TSPL_COMPANY_MASTER.Email as Comp_Email,TSPL_COMPANY_MASTER.Pincode as Comp_Pincode,TSPL_COMPANY_MASTER.Pan_No as Comp_Pan_No,TSPL_COMPANY_MASTER.Access_Officer as Comp_Access_Officer,TSPL_COMPANY_MASTER.Phone1 as Comp_Phone1,TSPL_COMPANY_MASTER.Phone2 as Comp_Phone2,TSPL_COMPANY_MASTER.CINNo as Comp_CINNo,GSTReg_No as Comp_GSTReg_No,TSPL_COMPANY_MASTER.Bank_Name as Comp_Bank_Name,TSPL_COMPANY_MASTER.BankAccountNo as Comp_BankAccountNo,TSPL_COMPANY_MASTER.BankIFSCCode as Comp_BankIFSCCode ,TSPL_COMPANY_MASTER.BankBranchAddress as Comp_BankBranchAddress,TSPL_COMPANY_MASTER.State  as Comp_State,TSPL_STATE_MASTER_For_Company.STATE_NAME as Comp_State_Name, TSPL_STATE_MASTER_For_Company.GST_STATE_Code as Comp_GST_STATE_Code,  " &
                                " TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.MCC_Code,TSPL_MCC_MASTER.MCC_NAME,TSPL_MCC_MASTER.Add1 as MCC_Add1, TSPL_MCC_MASTER.Add2 as MCC_Add2 ,TSPL_MCC_MASTER.City_code as MCC_City_Code, TSPL_MCC_MASTER.State_Code as MCC_STATE_CODE, TSPL_STATE_MASTER_ForMCC.STATE_NAME as MCC_STATE_Name,TSPL_STATE_MASTER_ForMCC.GST_STATE_Code as MCC_GST_STATE_Code ,TSPL_MCC_MASTER.Pin_code as MCC_Pin_Code, " &
                                " TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Vsp_Code,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Rent_Amount,TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Incentive_Code,TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Incentive_Qty,TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Incentive_UOM,TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Incentive_Rate, TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Incentive_Amount,TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Amount,Convert (varchar,TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.Doc_Date,103) as Doc_Date, DATENAME (Month, TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.Filter_Month) as Filter_Month,TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name, ISNULL ( TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Arrear,0) as Arrear, isnull (TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Deduction,0) as Deduction " &
                                " from TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL Left Outer Join TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD on TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.Doc_Code = TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Doc_Code " &
                                " left Outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Vsp_Code " &
                                " left Outer Join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_CODE = TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.MCC_Code " &
                                " left Outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = TSPL_MCC_MASTER.Comp_Code " &
                                " left Outer Join TSPL_STATE_MASTER as TSPL_STATE_MASTER_For_Company on TSPL_STATE_MASTER_For_Company.STATE_CODE = TSPL_COMPANY_MASTER.State  " &
                                " left Outer Join TSPL_STATE_MASTER as TSPL_STATE_MASTER_ForMCC on TSPL_STATE_MASTER_ForMCC.State_Code = TSPL_MCC_MASTER.State_Code " &
                                " left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_MASTER.Vendor_Code " &
                                " where TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.Doc_Code = '" + txtCode.Value + "' " &
                                " ) XXX LEFT OUTER JOIN (Select '1' as COL1, 1 as COL2,  'ORIGINAL' as CopyType1  ) YYY ON YYY.COL1=XXX.CopyType ORDER BY YYY.COL2  " &
                                "  "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptIncentiveEntryBySRN", "Incentive")
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        
    End Sub

    Private Sub btnReverse_Click_1(sender As Object, e As EventArgs) Handles btnReverse.Click
        Try
            If common.clsCommon.MyMessageBoxShow("Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If clsIncentiveEntryBySRNHead.ReverseAndUnpost(txtCode.Value) Then
                    common.clsCommon.MyMessageBoxShow("Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub BtnExportExcel_Click(sender As Object, e As EventArgs) Handles btnExportExcel.Click
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(" Document Code : " + txtCode.Value)
            arrHeader.Add(" Date : " + clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy") + "")
            arrHeader.Add(" MCC : " + lblMCC.Text)
            If txtVSP.arrValueMember IsNot Nothing AndAlso txtVSP.arrValueMember.Count > 0 Then
                arrHeader.Add(" VSP : " + clsCommon.GetMulcallStringWithComma(txtVSP.arrValueMember))
            End If
            arrHeader.Add(" Month : " + txtMonth.Text)
            clsCommon.MyExportToExcelGrid("Incentive Entry", gv1, arrHeader, Me.Text)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub RadButton3_Click(sender As Object, e As EventArgs) Handles RadButton3.Click
        Try
            If clsCommon.myLen(txtCode.Value) > 0 Then
                If clsCommon.myLen(settIncentiveProcessPaymentStartDate) <= 0 Then
                    Throw New Exception("Please set Incentive Process Payment Start Date")
                End If
                Dim dtIncentiveProcessPaymentStartDate As Date
                Try
                    dtIncentiveProcessPaymentStartDate = clsCommon.myCDate(settIncentiveProcessPaymentStartDate)
                Catch ex As Exception
                    Throw New Exception("Please Enter Proper Incentive Process Payment Start Date in Format [dd/MMM/yyyy] ")
                End Try
                If txtDate.Value < dtIncentiveProcessPaymentStartDate Then
                    Throw New Exception("This Facility is not available For current document")
                End If
                If UsLock1.Status = ERPTransactionStatus.Posted Then
                    Dim frm As FrmPaymentDetail = New FrmPaymentDetail()
                    frm.StartPosition = FormStartPosition.CenterScreen
                    frm.desc = "Against Incentive No " + txtCode.Value
                    frm.PaymentDate = txtDate.Value
                    frm.ShowDialog()
                    If frm.btnOkClicked Then
                        Dim qry As String = "select TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Vsp_Code,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Incentive_Amount,TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Rent_Amount, TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Amount,TSPL_VENDOR_INVOICE_HEAD.Document_No as CreditNoteAmt,TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.TR_Code" + Environment.NewLine +
                    "from TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL " + Environment.NewLine +
                    "left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Vsp_Code" + Environment.NewLine +
                    "left outer join TSPL_VENDOR_INVOICE_HEAD  on TSPL_VENDOR_INVOICE_HEAD.RefDocNo=TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.TR_Code and TSPL_VENDOR_INVOICE_HEAD.RefDocType = 'COM-INC' " + Environment.NewLine +
                    "left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Against_Incentive_Detail_No=TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.TR_Code " + Environment.NewLine +
                    "where  len(isnull( TSPL_VENDOR_INVOICE_HEAD.Document_No,''))>0 and len(isnull(TSPL_PAYMENT_HEADER.Payment_No,''))<=0 and TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Doc_Code='" + txtCode.Value + "'"
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                            Throw New Exception("There is no Pending credit Note whose payment entry is not created of document [" + txtCode.Value + "]")
                        End If
                        Dim arr As New ArrayList
                        For Each dr As DataRow In dt.Rows
                            arr.Add(clsCommon.myCstr(dr("TR_Code")))
                        Next
                        arr = clsCommon.ShowMultipleSelectForm(False, "PPInEnPayD", qry, "TR_Code", "", arr, Nothing)
                        If arr IsNot Nothing AndAlso arr.Count > 0 Then
                            If clsCommon.MyMessageBoxShow(Me, "Do you want to create Payment Entry for " + clsCommon.myCstr(arr.Count) + " VSP", Me.Text, MessageBoxButtons.YesNo) = DialogResult.Yes Then
                                clsIncentiveEntryBySRNHead.CreatePaymentEntry(arr, frm.bankCode, frm.paymentMode, frm.PaymentDate)
                                clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
                            End If
                        End If
                        'LoadData(txtCode.Value, NavigatorType.Current)
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private isCellValueChangedOpen As Boolean = False
    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(colArrear) OrElse e.Column Is gv1.Columns(colDeduction) Then
                        UpdateCurrentRow(gv1.CurrentRow.Index)
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
            Dim dblIncetiveAmt As Decimal = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colIncentiveAmount).Value)
            Dim dblRentAmt As Decimal = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colRentAmount).Value)
            Dim dblArrear As Decimal = 0
            Dim dblDeduction As Decimal = 0
            If settIncetiveApplyArrear Then
                dblArrear = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colArrear).Value)
                dblDeduction = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colDeduction).Value)
            End If
            Dim dblAmt As Decimal = dblIncetiveAmt + dblRentAmt + dblArrear - dblDeduction
            gv1.Rows(IntRowNo).Cells(colAmount).Value = dblAmt
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrint_VSP_Click(sender As Object, e As EventArgs) Handles btnPrint_VSP.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Document No found.", Me.Text)
                Return
            End If
            Dim frmCRV As New frmCrystalReportViewer()
            Dim strVSP As String = ""
            If txtVSP_Print.arrValueMember IsNot Nothing AndAlso txtVSP_Print.arrValueMember.Count > 0 Then
                strVSP = " and TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Vsp_Code in (" + clsCommon.GetMulcallString(txtVSP_Print.arrValueMember) + ")   "
            End If
            Dim qry As String = " Select * from ( " &
                                " select '1' as CopyType,  row_number() over(partition by TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Vsp_Code order by TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Vsp_Code ) as SNo, TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.Doc_Code as Sale_Invoice_No, case when TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.Is_Post = 1 then 'Posted' else 'Pending' end Document_Status , " &
                                " TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2, TSPL_COMPANY_MASTER.Comp_code ,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 as Comp_Add1,TSPL_COMPANY_MASTER.Add2 as Comp_Add2, TSPL_COMPANY_MASTER.Add3 as Comp_Add3,TSPL_COMPANY_MASTER.City_Code as Comp_City_Code,TSPL_COMPANY_MASTER.Email as Comp_Email,TSPL_COMPANY_MASTER.Pincode as Comp_Pincode,TSPL_COMPANY_MASTER.Pan_No as Comp_Pan_No,TSPL_COMPANY_MASTER.Access_Officer as Comp_Access_Officer,TSPL_COMPANY_MASTER.Phone1 as Comp_Phone1,TSPL_COMPANY_MASTER.Phone2 as Comp_Phone2,TSPL_COMPANY_MASTER.CINNo as Comp_CINNo,GSTReg_No as Comp_GSTReg_No,TSPL_COMPANY_MASTER.Bank_Name as Comp_Bank_Name,TSPL_COMPANY_MASTER.BankAccountNo as Comp_BankAccountNo,TSPL_COMPANY_MASTER.BankIFSCCode as Comp_BankIFSCCode ,TSPL_COMPANY_MASTER.BankBranchAddress as Comp_BankBranchAddress,TSPL_COMPANY_MASTER.State  as Comp_State,TSPL_STATE_MASTER_For_Company.STATE_NAME as Comp_State_Name, TSPL_STATE_MASTER_For_Company.GST_STATE_Code as Comp_GST_STATE_Code,  " &
                                " TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.MCC_Code,TSPL_MCC_MASTER.MCC_NAME,TSPL_MCC_MASTER.Add1 as MCC_Add1, TSPL_MCC_MASTER.Add2 as MCC_Add2 ,TSPL_MCC_MASTER.City_code as MCC_City_Code, TSPL_MCC_MASTER.State_Code as MCC_STATE_CODE, TSPL_STATE_MASTER_ForMCC.STATE_NAME as MCC_STATE_Name,TSPL_STATE_MASTER_ForMCC.GST_STATE_Code as MCC_GST_STATE_Code ,TSPL_MCC_MASTER.Pin_code as MCC_Pin_Code, " &
                                " TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Vsp_Code,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Rent_Amount,TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Incentive_Code,TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Incentive_Qty,TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Incentive_UOM,TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Incentive_Rate, TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Incentive_Amount,TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Amount,Convert (varchar,TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.Doc_Date,103) as Doc_Date, DATENAME (Month, TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.Filter_Month) as Filter_Month,TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name,   TSPL_VLC_MASTER_HEAD.Route_Code , TSPL_MCC_ROUTE_MASTER.Route_Name , DATENAME (YEAR, TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.Filter_Month) as Filter_Year , TSPL_VENDOR_MASTER.Bank_Code, TSPL_VENDOR_MASTER.Bank_Name, TSPL_VENDOR_MASTER.branch_code, TSPL_VENDOR_MASTER.Branch_Name, TSPL_VENDOR_MASTER.IFCI_Code, TSPL_VENDOR_MASTER.Account_No, case when TSPL_VENDOR_MASTER.Account_Type = 'Cur' then 'Current' when TSPL_VENDOR_MASTER.Account_Type = 'Sav' then 'Saving' when TSPL_VENDOR_MASTER.Account_Type = 'Cas' then 'Cash' when TSPL_VENDOR_MASTER.Account_Type = 'Cre' then 'Credit' when TSPL_VENDOR_MASTER.Account_Type = 'Loa' then 'Loan' when TSPL_VENDOR_MASTER.Account_Type = 'Oth' then 'Others' end  as Account_Type , TSPL_VENDOR_MASTER.VSP_Payee_Name ,ISNULL ( TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Arrear,0) as Arrear, isnull (TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Deduction,0) as Deduction " &
                                " from TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL Left Outer Join TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD on TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.Doc_Code = TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Doc_Code " &
                                " left Outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Vsp_Code " &
                                " left Outer Join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_CODE = TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.MCC_Code " &
                                " left Outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = TSPL_MCC_MASTER.Comp_Code " &
                                " left Outer Join TSPL_STATE_MASTER as TSPL_STATE_MASTER_For_Company on TSPL_STATE_MASTER_For_Company.STATE_CODE = TSPL_COMPANY_MASTER.State  " &
                                " left Outer Join TSPL_STATE_MASTER as TSPL_STATE_MASTER_ForMCC on TSPL_STATE_MASTER_ForMCC.State_Code = TSPL_MCC_MASTER.State_Code " &
                                " left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_MASTER.Vendor_Code " &
                                " left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code =  TSPL_VLC_MASTER_HEAD.Route_Code " &
                                " where TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.Doc_Code = '" + txtCode.Value + "' " + strVSP + " " &
                                " ) XXX LEFT OUTER JOIN (Select '1' as COL1, 1 as COL2,  'ORIGINAL' as CopyType1  ) YYY ON YYY.COL1=XXX.CopyType ORDER BY YYY.COL2  " &
                                "  "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptIncentiveEntryBySRNVSPWise", "Incentive")
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtVSP_Print__My_Click(sender As Object, e As EventArgs) Handles txtVSP_Print._My_Click
        Try
            Dim qry As String = " select TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Vsp_Code as Code ,TSPL_VENDOR_MASTER.Vendor_Name as Name  from TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL 
                                  left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Vsp_Code
                                  where TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Doc_Code = '" + txtCode.Value + "'
                                  order by TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Vsp_Code"
            txtVSP_Print.arrValueMember = clsCommon.ShowMultipleSelectForm(False, "PPfPVisrn@@PrintVSP", qry, "Code", "", txtVSP_Print.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
