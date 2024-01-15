'Ticket No- BHA/17/10/18-000629,Add column -cheque amount and clearance date
Imports common
Imports System.Data.SqlClient
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class frmEmpFullAndFinalSettlement
    Inherits FrmMainTranScreen
    '' Salary Struct and Unpaid Amount Tab Grid columns
    Const colSalLineNo As String = "colSalLineNo"
    Const colSalPayHeadCode As String = "colSalPayHeadCode"
    Const colSalPayHeadDesc As String = "colSalPayHeadDesc"
    Const colSalHeadRate As String = "colSalHeadRate"
    Const colSalHeadAmount As String = "colSalHeadAmount"
    Const colSalUnpaidAmt As String = "colSalUnpaidAmt"
    Const colSalRemarks As String = "colSalRemarks"



    '' Other Earning grid columns
    Const colOthrEarngLineNo As String = "colOthrEarngLineNo"
    Const colOthrearngCode As String = "colOthrearngCode"
    Const colOthrEarngDesc As String = "colOthrEarngDesc"
    Const colOthrEarngRate As String = "colOthrEarngRate"
    Const colOthrEarngAmount As String = "colOthrEarngAmount"
    Const colOthrEarngActualAmt As String = "colOthrEarngActualAmt"
    Const colOthrEarngRemarks As String = "colOthrEarngRemarks"


    '' Deduction grid 
    Const colDedLineNo As String = "colDedLineNo"
    Const colDedPayHeadCode As String = "colDedPayHeadCode"
    Const colDedPayHeadDesc As String = "colDedPayHeadDesc"
    Const colDedHeadRate As String = "colDedHeadRate"
    Const colDedHeadAmount As String = "colDedHeadAmount"
    Const colDedActualAmt As String = "colDedActualAmt"
    Const colDedRemarks As String = "colDedRemarks"

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = True
    Private isInsideLoadData As Boolean = False
    Dim obj As New clsFFSettlement
    'Private ObjList As New List(Of clsFFSettlement)
    Private isCellValueChangedOpen As Boolean = False
    Dim OpenFileDialog1 As New OpenFileDialog

    Sub LoadUnpaidSalGrid()

        gvSalStructAndUnpaidSalAmt.Rows.Clear()
        gvSalStructAndUnpaidSalAmt.Columns.Clear()

        Dim SalLineNo As New GridViewTextBoxColumn
        Dim SalPayHeadCode As New GridViewTextBoxColumn
        Dim SalPayHeadDesc As New GridViewTextBoxColumn
        Dim SalHeadRate As New GridViewDecimalColumn
        Dim SalHeadAmount As New GridViewDecimalColumn
        Dim SalUnpaidAmt As New GridViewDecimalColumn
        Dim SalRemarks As New GridViewTextBoxColumn

        SalLineNo.FormatString = ""
        SalLineNo.HeaderText = "Line No"
        SalLineNo.Name = colSalLineNo
        SalLineNo.Width = 70
        SalLineNo.ReadOnly = True
        SalLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSalStructAndUnpaidSalAmt.Columns.Add(SalLineNo)

        SalPayHeadCode.FormatString = ""
        SalPayHeadCode.HeaderText = "Payhead Code"
        SalPayHeadCode.Name = colSalPayHeadCode
        SalPayHeadCode.Width = 100
        SalPayHeadCode.ReadOnly = True
        SalPayHeadCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvSalStructAndUnpaidSalAmt.Columns.Add(SalPayHeadCode)

        SalPayHeadDesc.FormatString = ""
        SalPayHeadDesc.HeaderText = "Description"
        SalPayHeadDesc.Name = colSalPayHeadDesc
        SalPayHeadDesc.Width = 100
        SalPayHeadDesc.ReadOnly = True
        SalPayHeadDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvSalStructAndUnpaidSalAmt.Columns.Add(SalPayHeadDesc)

        SalHeadRate.FormatString = ""
        SalHeadRate.HeaderText = "Rate"
        SalHeadRate.Name = colSalHeadRate
        SalHeadRate.Width = 100
        SalHeadRate.ReadOnly = True
        SalHeadRate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvSalStructAndUnpaidSalAmt.Columns.Add(SalHeadRate)

        SalHeadAmount.FormatString = ""
        SalHeadAmount.HeaderText = "Head Amount"
        SalHeadAmount.Name = colSalHeadAmount
        SalHeadAmount.Width = 120
        SalHeadAmount.ReadOnly = True
        SalHeadAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSalStructAndUnpaidSalAmt.Columns.Add(SalHeadAmount)

        SalUnpaidAmt.FormatString = ""
        SalUnpaidAmt.HeaderText = "Unpaid Amount"
        SalUnpaidAmt.Name = colSalUnpaidAmt
        SalUnpaidAmt.Width = 120
        SalUnpaidAmt.ReadOnly = False
        SalUnpaidAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSalStructAndUnpaidSalAmt.Columns.Add(SalUnpaidAmt)

        SalRemarks.FormatString = ""
        SalRemarks.HeaderText = "Remarks"
        SalRemarks.Name = colSalRemarks
        SalRemarks.Width = 120
        SalRemarks.ReadOnly = False
        SalRemarks.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSalStructAndUnpaidSalAmt.Columns.Add(SalRemarks)

    End Sub
    Sub LoadOthrEarngGrid()

        gvOthers.Rows.Clear()
        gvOthers.Columns.Clear()

        Dim OthrEarngLineNo As New GridViewTextBoxColumn
        Dim OthrEarngPayHeadCode As New GridViewTextBoxColumn
        Dim OthrEarngPayHeadDesc As New GridViewTextBoxColumn
        Dim OthrEarngHeadRate As New GridViewDecimalColumn
        Dim OthrEarngHeadAmount As New GridViewDecimalColumn
        Dim OthrEarngUnpaidAmt As New GridViewDecimalColumn
        Dim OthrEarngRemarks As New GridViewTextBoxColumn

        OthrEarngLineNo.FormatString = ""
        OthrEarngLineNo.HeaderText = "Line No"
        OthrEarngLineNo.Name = colOthrEarngLineNo
        OthrEarngLineNo.Width = 70
        OthrEarngLineNo.ReadOnly = True
        OthrEarngLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvOthers.Columns.Add(OthrEarngLineNo)

        OthrEarngPayHeadCode.FormatString = ""
        OthrEarngPayHeadCode.HeaderText = "Payhead Code"
        OthrEarngPayHeadCode.Name = colOthrearngCode
        OthrEarngPayHeadCode.Width = 100
        OthrEarngPayHeadCode.ReadOnly = True
        OthrEarngPayHeadCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvOthers.Columns.Add(OthrEarngPayHeadCode)

        OthrEarngPayHeadDesc.FormatString = ""
        OthrEarngPayHeadDesc.HeaderText = "Description"
        OthrEarngPayHeadDesc.Name = colOthrEarngDesc
        OthrEarngPayHeadDesc.Width = 100
        OthrEarngPayHeadDesc.ReadOnly = True
        OthrEarngPayHeadDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvOthers.Columns.Add(OthrEarngPayHeadDesc)

        OthrEarngHeadRate.FormatString = ""
        OthrEarngHeadRate.HeaderText = "Rate"
        OthrEarngHeadRate.Name = colOthrEarngRate
        OthrEarngHeadRate.Width = 100
        OthrEarngHeadRate.ReadOnly = True
        OthrEarngHeadRate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvOthers.Columns.Add(OthrEarngHeadRate)

        OthrEarngHeadAmount.FormatString = ""
        OthrEarngHeadAmount.HeaderText = "Head Amount"
        OthrEarngHeadAmount.Name = colOthrEarngAmount
        OthrEarngHeadAmount.Width = 120
        OthrEarngHeadAmount.ReadOnly = True
        OthrEarngHeadAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvOthers.Columns.Add(OthrEarngHeadAmount)

        OthrEarngUnpaidAmt.FormatString = ""
        OthrEarngUnpaidAmt.HeaderText = "Actual Amount"
        OthrEarngUnpaidAmt.Name = colOthrEarngActualAmt
        OthrEarngUnpaidAmt.Width = 120
        OthrEarngUnpaidAmt.ReadOnly = False
        OthrEarngUnpaidAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvOthers.Columns.Add(OthrEarngUnpaidAmt)

        OthrEarngRemarks.FormatString = ""
        OthrEarngRemarks.HeaderText = "Remarks"
        OthrEarngRemarks.Name = colOthrEarngRemarks
        OthrEarngRemarks.Width = 120
        OthrEarngRemarks.ReadOnly = False
        OthrEarngRemarks.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvOthers.Columns.Add(OthrEarngRemarks)





    End Sub
    Sub LoadDeductionGrid()

        gvDeductions.Rows.Clear()
        gvDeductions.Columns.Clear()

        Dim DedLineNo As New GridViewTextBoxColumn
        Dim DedPayHeadCode As New GridViewTextBoxColumn
        Dim DedPayHeadDesc As New GridViewTextBoxColumn
        Dim DedHeadRate As New GridViewDecimalColumn
        Dim DedHeadAmount As New GridViewDecimalColumn
        Dim DedUnpaidAmt As New GridViewDecimalColumn
        Dim DedRemarks As New GridViewTextBoxColumn

        DedLineNo.FormatString = ""
        DedLineNo.HeaderText = "Line No"
        DedLineNo.Name = colDedLineNo
        DedLineNo.Width = 70
        DedLineNo.ReadOnly = True
        DedLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvDeductions.Columns.Add(DedLineNo)

        DedPayHeadCode.FormatString = ""
        DedPayHeadCode.HeaderText = "Payhead Code"
        DedPayHeadCode.Name = colDedPayHeadCode
        DedPayHeadCode.Width = 100
        DedPayHeadCode.ReadOnly = True
        DedPayHeadCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvDeductions.Columns.Add(DedPayHeadCode)

        DedPayHeadDesc.FormatString = ""
        DedPayHeadDesc.HeaderText = "Description"
        DedPayHeadDesc.Name = colDedPayHeadDesc
        DedPayHeadDesc.Width = 100
        DedPayHeadDesc.ReadOnly = True
        DedPayHeadDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvDeductions.Columns.Add(DedPayHeadDesc)

        DedHeadRate.FormatString = ""
        DedHeadRate.HeaderText = "Rate"
        DedHeadRate.Name = colDedHeadRate
        DedHeadRate.Width = 100
        DedHeadRate.ReadOnly = True
        DedHeadRate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvDeductions.Columns.Add(DedHeadRate)

        DedHeadAmount.FormatString = ""
        DedHeadAmount.HeaderText = "Head Amount"
        DedHeadAmount.Name = colDedHeadAmount
        DedHeadAmount.Width = 120
        DedHeadAmount.ReadOnly = True
        DedHeadAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvDeductions.Columns.Add(DedHeadAmount)

        DedUnpaidAmt.FormatString = ""
        DedUnpaidAmt.HeaderText = "Actual Amount"
        DedUnpaidAmt.Name = colDedActualAmt
        DedUnpaidAmt.Width = 120
        DedUnpaidAmt.ReadOnly = False
        DedUnpaidAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvDeductions.Columns.Add(DedUnpaidAmt)

        DedRemarks.FormatString = ""
        DedRemarks.HeaderText = "Remarks"
        DedRemarks.Name = colDedRemarks
        DedRemarks.Width = 120
        DedRemarks.ReadOnly = False
        DedRemarks.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvDeductions.Columns.Add(DedRemarks)

    End Sub

    Private Sub frmEmpFullAndFinalSettlement_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnnew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnsave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled Then
            PostData()
        End If
    End Sub
    Sub funClose()
        Me.Close()
    End Sub

    Private Sub frmEmpFullAndFinalSettlement_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadUnpaidSalGrid()
        LoadOthrEarngGrid()
        LoadDeductionGrid()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")

        funReset()

        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub
    Public Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmFullAndFinalSettlement)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        funClose()
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            funReset()
        Catch ex As Exception
        End Try
    End Sub

    Sub funReset()
        isNewEntry = True
        Dim serverdate As Date = clsCommon.GETSERVERDATE
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        UsLock1.Status = ERPTransactionStatus.Pending
        lblDesignationId.Text = ""
        lblDepartmentId.Text = ""
        dtpDoJ.Value = serverdate
        dtpResignSubmitDate.Value = serverdate
        dtpLastSalUptoDate.Value = serverdate
        dtpActualLastWDay.Value = serverdate
        txtdocdate.Value = serverdate
        dtpLastWDay.Value = serverdate
        dtpDoJ.Enabled = False
        dtpLastWDay.Enabled = False
        dtpActualLastWDay.Enabled = False
        fndLastSalaryPayperiodCode.Enabled = False
        dtpLastSalUptoDate.Enabled = False

        lblNoticePeriod.Text = ""
        lblShortFallDays.Text = ""
        txtReasonForLeaving.Text = ""
        lblServiceRenderedPeriod.Text = ""
        fndLastSalaryPayperiodCode.Value = Nothing
        txtNoofDaysLastSal.Text = ""
        gvSalStructAndUnpaidSalAmt.Rows.Clear()
        gvOthers.Rows.Clear()
        gvDeductions.Rows.Clear()
        Me.txtTotalUnpaidSalary.Text = 0
        Me.txtTotalDuction.Text = 0
        Me.txtNetAmountPayable.Text = 0
        Me.lblTotalEarnings.Text = 0
        ' Add by Prabhakar Ticket Ref : BM00000009911
        lblEmpName.Text = ""
        txtDescription.Text = ""
        txtChequeNo.Text = ""
        dtpChequeDated.Value = serverdate
        txtChequeAmt.Text = ""
        dtpClearDate.Value = serverdate
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType)
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            isInsideLoadData = True
            funReset()

            txtCode.MyReadOnly = True
            btnsave.Enabled = True
            btndelete.Enabled = True
            Me.txtCode.Value = strCode
            obj = clsFFSettlement.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.EMP_CODE) > 0) Then
                isNewEntry = False
                btnsave.Text = "Update"
                txtCode.Value = obj.EMP_CODE
                lblEmpName.Text = obj.EMP_NAME
                If obj.POSTED Then
                    btnsave.Enabled = False
                    btnPost.Enabled = False
                    btndelete.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                Else
                    btnsave.Enabled = True
                    btndelete.Enabled = True
                    btnPost.Enabled = True
                    UsLock1.Status = ERPTransactionStatus.Pending
                End If
                Dim ii As Int16 = 0

                '' general tab
                Me.lblDepartmentId.Text = obj.DEPARTMENT_CODE
                Me.lblDesignationId.Text = obj.DESIGNATION_ID
                Me.dtpDoJ.Value = obj.DOJ
                Me.dtpResignSubmitDate.Value = obj.RESIGN_SUBMIT_DATE
                Me.dtpActualLastWDay.Value = obj.ACTUAL_LAST_WORKING_DAY
                Me.dtpLastSalUptoDate.Value = obj.LAST_SALARY_UPTO_DATE
                Me.dtpLastWDay.Value = obj.LAST_WORKING_DAY
                Me.lblNoticePeriod.Text = obj.NOTICE_PERIOD
                Me.lblShortFallDays.Text = obj.SHORT_FALL_DAYS
                Me.txtReasonForLeaving.Text = obj.LEAVING_REASON
                Me.lblServiceRenderedPeriod.Text = obj.TOTAL_SERVICE_PERIOD
                Me.txtNoofDaysLastSal.Text = obj.WORKING_DAYS_AFTER_LAST_SAL
                Me.txtDescription.Text = obj.DESCRIPTION
                Me.fndLastSalaryPayperiodCode.Value = obj.Last_PAY_PERIOD_CODE
                Me.txtPayPeriod.Value = obj.PAY_PERIOD_CODE
                '' total tab
                Me.txtTotalUnpaidSalary.Text = obj.TOTAL_EARNING_AMOUNT
                Me.lblTotalEarnings.Text = obj.TOTAL_OTHR_EARNING_AMOUNT
                Me.txtTotalDuction.Text = obj.TOTAL_DEDUCTION_AMOUNT
                Me.txtNetAmountPayable.Text = obj.NET_PAYABLE_AMOUNT
                If Not obj.Document_Date Is Nothing Then
                    Me.txtdocdate.Text = obj.Document_Date
                End If
                txtChequeNo.Text = obj.CHEQUE_NO
                If Not obj.CHEQUE_DATED Is Nothing Then
                    dtpChequeDated.Value = obj.CHEQUE_DATED
                End If
                txtChequeAmt.Text = obj.CHEQUE_AMOUNT
                If Not obj.CHEQUE_CLEARANCE_DATE Is Nothing Then
                    dtpClearDate.Value = obj.CHEQUE_CLEARANCE_DATE
                End If
            Else
                Dim obj1 As clsEmployeeMaster
                obj1 = clsEmployeeMaster.GetData(strCode, NavigatorType.Current)
                If obj1 IsNot Nothing Then
                    Me.txtCode.Value = strCode
                    Me.lblEmpName.Text = obj1.Emp_Name
                    Me.lblDepartmentId.Text = obj1.DEPARTMENT_CODE
                    Me.lblDesignationId.Text = obj1.Designation
                    Me.dtpDoJ.Value = obj1.Joining_date
                    Me.dtpResignSubmitDate.Value = obj1.RESINATION_SUBMIT_DATE
                    Me.dtpActualLastWDay.Value = clsCommon.myCDate(obj1.RESINATION_SUBMIT_DATE).AddDays(obj1.NOTICE_IN_DAYS)
                    '' generate salary for the extra days present after last salary date
                    '' get lastDrawnPayPeriod
                    fndLastSalaryPayperiodCode.Value = clsSalaryGeneration.GetLastDrawnSalaryPayPeriod(txtCode.Value, Nothing)
                    If clsCommon.myLen(fndLastSalaryPayperiodCode.Value) > 0 Then
                        Me.dtpLastSalUptoDate.Value = clsPayPeriodMaster.GetToDate(fndLastSalaryPayperiodCode.Value, Nothing)
                        '' get next payperiod
                        txtPayPeriod.Value = clsSalaryGeneration.GetNextPayPeriod(fndLastSalaryPayperiodCode.Value, Nothing)
                    End If
                    'Me.dtpLastSalUptoDate.Value = clsCommon.myCDate("1" & "/" & MonthName(IIf(clsCommon.myCDate(obj1.RESINATION_SUBMIT_DATE).Month < 12, clsCommon.myCDate(obj1.RESINATION_SUBMIT_DATE).Month, 1) + 1) & "/" & clsCommon.myCDate(obj1.RESINATION_SUBMIT_DATE).Year).AddDays(-1)
                    Me.dtpLastWDay.Value = clsCommon.myCDate(obj1.RESINATION_SUBMIT_DATE).AddDays(obj1.NOTICE_IN_DAYS)
                    Me.lblNoticePeriod.Text = obj1.NOTICE_IN_DAYS
                    Me.lblShortFallDays.Text = 0
                    Me.txtReasonForLeaving.Text = obj1.LEAVING_REASON
                    Me.lblServiceRenderedPeriod.Text = DateDiff(DateInterval.Year, clsCommon.myCDate(obj1.Joining_date), Me.dtpLastWDay.Value)
                    Me.txtNoofDaysLastSal.Text = 0
                    Me.txtDescription.Text = obj1.DESCRIPTION

                    '' total tab
                    Me.txtTotalUnpaidSalary.Text = 0
                    Me.lblTotalEarnings.Text = 0
                    Me.txtTotalDuction.Text = 0
                    Me.txtNetAmountPayable.Text = 0
                End If
            End If


            '' display salary
            gvSalStructAndUnpaidSalAmt.Rows.Clear()
            gvDeductions.Rows.Clear()
            gvOthers.Rows.Clear()

            If obj.ObjListSalary IsNot Nothing AndAlso obj.ObjListSalary.Count > 0 Then
                For Each objSAL As clsFFSalary In obj.ObjListSalary
                    If objSAL.IS_OTHR_EARNING Then
                        gvOthers.Rows.AddNew()
                        gvOthers.Rows(gvOthers.Rows.Count - 1).Cells(colOthrEarngLineNo).Value = objSAL.Line_No
                        gvOthers.Rows(gvOthers.Rows.Count - 1).Cells(colOthrearngCode).Value = objSAL.PAY_HEAD_CODE
                        gvOthers.Rows(gvOthers.Rows.Count - 1).Cells(colOthrEarngDesc).Value = objSAL.PAY_HEAD_DESC

                        gvOthers.Rows(gvOthers.Rows.Count - 1).Cells(colOthrearngCode).Tag = objSAL.PAYHEAD_FORMULA
                        gvOthers.Rows(gvOthers.Rows.Count - 1).Cells(colOthrEarngRate).Value = objSAL.RATE_AMOUNT
                        gvOthers.Rows(gvOthers.Rows.Count - 1).Cells(colOthrEarngAmount).Value = objSAL.PAYHEAD_AMOUNT
                        gvOthers.Rows(gvOthers.Rows.Count - 1).Cells(colOthrEarngActualAmt).Value = objSAL.ACTUAL_AMOUNT
                        gvOthers.Rows(gvOthers.Rows.Count - 1).Cells(colOthrEarngRemarks).Value = objSAL.REMARKS

                    ElseIf objSAL.IS_DEDUCTION Then
                        gvDeductions.Rows.AddNew()
                        gvDeductions.Rows(gvDeductions.Rows.Count - 1).Cells(colDedLineNo).Value = objSAL.Line_No
                        gvDeductions.Rows(gvDeductions.Rows.Count - 1).Cells(colDedPayHeadCode).Value = objSAL.PAY_HEAD_CODE
                        gvDeductions.Rows(gvDeductions.Rows.Count - 1).Cells(colDedPayHeadDesc).Value = objSAL.PAY_HEAD_DESC

                        gvDeductions.Rows(gvDeductions.Rows.Count - 1).Cells(colDedPayHeadCode).Tag = objSAL.PAYHEAD_FORMULA
                        gvDeductions.Rows(gvDeductions.Rows.Count - 1).Cells(colDedHeadRate).Value = objSAL.RATE_AMOUNT
                        gvDeductions.Rows(gvDeductions.Rows.Count - 1).Cells(colDedHeadAmount).Value = objSAL.PAYHEAD_AMOUNT
                        gvDeductions.Rows(gvDeductions.Rows.Count - 1).Cells(colDedActualAmt).Value = objSAL.ACTUAL_AMOUNT
                        gvDeductions.Rows(gvDeductions.Rows.Count - 1).Cells(colDedRemarks).Value = objSAL.REMARKS
                    Else
                        gvSalStructAndUnpaidSalAmt.Rows.AddNew()
                        gvSalStructAndUnpaidSalAmt.Rows(gvSalStructAndUnpaidSalAmt.Rows.Count - 1).Cells(colSalLineNo).Value = objSAL.Line_No
                        gvSalStructAndUnpaidSalAmt.Rows(gvSalStructAndUnpaidSalAmt.Rows.Count - 1).Cells(colSalPayHeadCode).Value = objSAL.PAY_HEAD_CODE
                        gvSalStructAndUnpaidSalAmt.Rows(gvSalStructAndUnpaidSalAmt.Rows.Count - 1).Cells(colSalPayHeadDesc).Value = objSAL.PAY_HEAD_DESC

                        gvSalStructAndUnpaidSalAmt.Rows(gvSalStructAndUnpaidSalAmt.Rows.Count - 1).Cells(colSalPayHeadCode).Tag = objSAL.PAYHEAD_FORMULA
                        gvSalStructAndUnpaidSalAmt.Rows(gvSalStructAndUnpaidSalAmt.Rows.Count - 1).Cells(colSalHeadRate).Value = objSAL.RATE_AMOUNT
                        gvSalStructAndUnpaidSalAmt.Rows(gvSalStructAndUnpaidSalAmt.Rows.Count - 1).Cells(colSalHeadAmount).Value = objSAL.PAYHEAD_AMOUNT
                        gvSalStructAndUnpaidSalAmt.Rows(gvSalStructAndUnpaidSalAmt.Rows.Count - 1).Cells(colSalUnpaidAmt).Value = objSAL.ACTUAL_AMOUNT
                        gvSalStructAndUnpaidSalAmt.Rows(gvSalStructAndUnpaidSalAmt.Rows.Count - 1).Cells(colSalRemarks).Value = objSAL.REMARKS

                    End If


                Next
            Else

                Dim empSal As clsEmployeeSalary
                Dim objPayHead As clsPayHeadDefinitions
                ''  get latest salary
                Dim emp_sal_code As String
                emp_sal_code = "select EMP_SAL_CODE from TSPL_EMPLOYEE_SALARY where EMP_CODE='" & txtCode.Value & "' and REVISION_NO=" & _
                               " (SELECT MAX(REVISION_NO) FROM TSPL_EMPLOYEE_SALARY WHERE EMP_CODE='" & txtCode.Value & "' group by EMP_CODE)"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(emp_sal_code)
                If dt.Rows.Count > 0 Then
                    emp_sal_code = Nothing
                    emp_sal_code = dt.Rows(0).Item("EMP_SAL_CODE")
                Else
                    clsCommon.MyMessageBoxShow(Me, "Employee Salary not defined", Me.Text)
                    Exit Sub
                End If
                empSal = clsEmployeeSalary.GetData(emp_sal_code, NavigatorType.Current)
                If empSal IsNot Nothing And clsEmployeeSalary.ObjList.Count > 0 Then
                    For Each objph As clsEmpSalaryPayHeadDetails In clsEmployeeSalary.ObjList
                        objPayHead = New clsPayHeadDefinitions
                        objPayHead = clsPayHeadDefinitions.GetData(objph.PayHeadCode, NavigatorType.Current)
                        If objPayHead.ISEARNING = False AndAlso objPayHead.IsFullnFinal = False Then
                            gvDeductions.Rows.AddNew()
                            gvDeductions.Rows(gvDeductions.Rows.Count - 1).Cells(colDedLineNo).Value = gvDeductions.Rows.Count
                            gvDeductions.Rows(gvDeductions.Rows.Count - 1).Cells(colDedPayHeadCode).Value = objPayHead.PAY_HEAD_CODE
                            gvDeductions.Rows(gvDeductions.Rows.Count - 1).Cells(colDedPayHeadDesc).Value = objPayHead.PAY_HEAD_NAME

                            gvDeductions.Rows(gvDeductions.Rows.Count - 1).Cells(colDedPayHeadCode).Tag = objph.Formula
                            gvDeductions.Rows(gvDeductions.Rows.Count - 1).Cells(colDedHeadRate).Value = objph.Rate_Amount
                            gvDeductions.Rows(gvDeductions.Rows.Count - 1).Cells(colDedHeadAmount).Value = objph.Rate_Amount
                            gvDeductions.Rows(gvDeductions.Rows.Count - 1).Cells(colDedActualAmt).Value = 0
                            gvDeductions.Rows(gvDeductions.Rows.Count - 1).Cells(colDedRemarks).Value = ""
                        ElseIf objPayHead.IsFullnFinal = True Then
                            'ElseIf (clsCommon.CompairString(objPayHead.SUB_HEAD_TYPE, "OTHER") = CompairStringResult.Equal AndAlso objph.Rate_Amount <= 0) Or clsCommon.CompairString(objPayHead.SUB_HEAD_TYPE, "EACASHLEAVE") = CompairStringResult.Equal Or clsCommon.CompairString(objPayHead.SUB_HEAD_TYPE, "LTA") = CompairStringResult.Equal Or clsCommon.CompairString(objPayHead.SUB_HEAD_TYPE, "GRATUITY") = CompairStringResult.Equal Or clsCommon.CompairString(objPayHead.SUB_HEAD_TYPE, "BONUS") = CompairStringResult.Equal Then
                            gvOthers.Rows.AddNew()
                            gvOthers.Rows(gvOthers.Rows.Count - 1).Cells(colOthrEarngLineNo).Value = gvOthers.Rows.Count
                            gvOthers.Rows(gvOthers.Rows.Count - 1).Cells(colOthrearngCode).Value = objPayHead.PAY_HEAD_CODE
                            gvOthers.Rows(gvOthers.Rows.Count - 1).Cells(colOthrEarngDesc).Value = objPayHead.PAY_HEAD_NAME

                            gvOthers.Rows(gvOthers.Rows.Count - 1).Cells(colOthrearngCode).Tag = objph.Formula
                            gvOthers.Rows(gvOthers.Rows.Count - 1).Cells(colOthrEarngRate).Value = objph.Rate_Amount
                            gvOthers.Rows(gvOthers.Rows.Count - 1).Cells(colOthrEarngAmount).Value = objph.Rate_Amount
                            gvOthers.Rows(gvOthers.Rows.Count - 1).Cells(colOthrEarngActualAmt).Value = 0
                            gvOthers.Rows(gvOthers.Rows.Count - 1).Cells(colOthrEarngRemarks).Value = ""
                        Else
                            gvSalStructAndUnpaidSalAmt.Rows.AddNew()
                            gvSalStructAndUnpaidSalAmt.Rows(gvSalStructAndUnpaidSalAmt.Rows.Count - 1).Cells(colSalLineNo).Value = gvSalStructAndUnpaidSalAmt.Rows.Count
                            gvSalStructAndUnpaidSalAmt.Rows(gvSalStructAndUnpaidSalAmt.Rows.Count - 1).Cells(colSalPayHeadCode).Value = objPayHead.PAY_HEAD_CODE
                            gvSalStructAndUnpaidSalAmt.Rows(gvSalStructAndUnpaidSalAmt.Rows.Count - 1).Cells(colSalPayHeadDesc).Value = objPayHead.PAY_HEAD_NAME

                            gvSalStructAndUnpaidSalAmt.Rows(gvSalStructAndUnpaidSalAmt.Rows.Count - 1).Cells(colSalPayHeadCode).Tag = objph.Formula
                            gvSalStructAndUnpaidSalAmt.Rows(gvSalStructAndUnpaidSalAmt.Rows.Count - 1).Cells(colSalHeadRate).Value = objph.Rate_Amount
                            gvSalStructAndUnpaidSalAmt.Rows(gvSalStructAndUnpaidSalAmt.Rows.Count - 1).Cells(colSalHeadAmount).Value = objph.Rate_Amount
                            gvSalStructAndUnpaidSalAmt.Rows(gvSalStructAndUnpaidSalAmt.Rows.Count - 1).Cells(colSalUnpaidAmt).Value = 0
                            gvSalStructAndUnpaidSalAmt.Rows(gvSalStructAndUnpaidSalAmt.Rows.Count - 1).Cells(colSalRemarks).Value = ""
                        End If
                    Next
                    '' generate salary for unpaid amount
                    If clsCommon.myLen(txtPayPeriod.Value) > 0 Then
                        Dim arrEMP As New ArrayList
                        arrEMP.Add(txtCode.Value)
                        clsSalaryGeneration.Generate_Salary(txtPayPeriod.Value, arrEMP, Nothing)
                    End If
                    Dim dtSal As DataTable = clsDBFuncationality.GetDataTable("select Pay_Head_Code,Actual_Amount,PAYABLE_DAYS from TSPL_SALARY_CALCULATION where EMP_CODE='" & txtCode.Value & "'")
                    '' update unpaid amount in fisrt grid
                    For Each grow As GridViewRowInfo In gvSalStructAndUnpaidSalAmt.Rows
                        For Each dr As DataRow In dtSal.Rows
                            If clsCommon.CompairString(grow.Cells(colSalPayHeadCode).Value, clsCommon.myCstr(dr.Item("Pay_Head_Code"))) = CompairStringResult.Equal Then
                                grow.Cells(colSalUnpaidAmt).Value = clsCommon.myCdbl(dr.Item("Actual_Amount"))
                                Exit For
                            End If
                        Next
                    Next
                    '' update unpaid amount in second grid
                    For Each grow As GridViewRowInfo In gvOthers.Rows
                        For Each dr As DataRow In dtSal.Rows
                            If clsCommon.CompairString(grow.Cells(colOthrearngCode).Value, clsCommon.myCstr(dr.Item("Pay_Head_Code"))) = CompairStringResult.Equal Then
                                grow.Cells(colOthrEarngActualAmt).Value = clsCommon.myCdbl(dr.Item("Actual_Amount"))
                                Exit For
                            End If
                        Next
                    Next
                    '' update unpaid amount in third grid
                    For Each grow As GridViewRowInfo In gvDeductions.Rows
                        For Each dr As DataRow In dtSal.Rows
                            If clsCommon.CompairString(grow.Cells(colDedPayHeadCode).Value, clsCommon.myCstr(dr.Item("Pay_Head_Code"))) = CompairStringResult.Equal Then
                                grow.Cells(colDedActualAmt).Value = clsCommon.myCdbl(dr.Item("Actual_Amount"))
                                Exit For
                            End If
                        Next
                    Next
                    If dtSal.Rows.Count > 0 Then
                        txtNoofDaysLastSal.Text = clsCommon.myCdbl(dtSal.Rows(0).Item("PAYABLE_DAYS"))
                    End If
                End If


            End If
            ''===========================do work for showing summary total at bottom=======================
            gvSalStructAndUnpaidSalAmt.Rows.AddNew()
            gvSalStructAndUnpaidSalAmt.Rows(gvSalStructAndUnpaidSalAmt.Rows.Count - 1).Cells(colSalPayHeadCode).Value = "TOTAL"
            setGridTotalSalStructAndUnpaidSalAmt()

            gvOthers.Rows.AddNew()
            gvOthers.Rows(gvOthers.Rows.Count - 1).Cells(colOthrearngCode).Value = "TOTAL"
            setGridTotalotherEarning()

            gvDeductions.Rows.AddNew()
            gvDeductions.Rows(gvDeductions.Rows.Count - 1).Cells(colDedPayHeadCode).Value = "TOTAL"
            setGridTotalDeduction()
            ''=============================================================================
            isInsideLoadData = False
            'FormatGridSalStructAndUnpaidSalAmt()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub setGridTotalSalStructAndUnpaidSalAmt()
        Try
            Dim intCurrRow As Integer? = Nothing
            Dim dblRate As Double = 0
            Dim dblHeadAmount As Double = 0
            Dim dblActualAmount As Double = 0
            For ii As Integer = 0 To gvSalStructAndUnpaidSalAmt.Rows.Count - 1
                If (clsCommon.myLen(gvSalStructAndUnpaidSalAmt.Rows(ii).Cells(colSalLineNo).Value) > 0) AndAlso clsCommon.CompairString(clsCommon.myCstr(gvSalStructAndUnpaidSalAmt.Rows(ii).Cells(colSalPayHeadCode).Value), "TOTAL") <> CompairStringResult.Equal Then
                    dblRate = dblRate + clsCommon.myCdbl(gvSalStructAndUnpaidSalAmt.Rows(ii).Cells(colSalHeadRate).Value)
                    dblHeadAmount = dblHeadAmount + clsCommon.myCdbl(gvSalStructAndUnpaidSalAmt.Rows(ii).Cells(colSalHeadAmount).Value)
                    dblActualAmount = dblActualAmount + clsCommon.myCdbl(gvSalStructAndUnpaidSalAmt.Rows(ii).Cells(colSalUnpaidAmt).Value)
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvSalStructAndUnpaidSalAmt.Rows(ii).Cells(colSalPayHeadCode).Value), "TOTAL") = CompairStringResult.Equal Then
                    intCurrRow = ii
                End If
            Next
            gvSalStructAndUnpaidSalAmt.Rows(intCurrRow).Cells(colSalHeadRate).Value = dblRate
            gvSalStructAndUnpaidSalAmt.Rows(intCurrRow).Cells(colSalHeadAmount).Value = dblHeadAmount
            gvSalStructAndUnpaidSalAmt.Rows(intCurrRow).Cells(colSalUnpaidAmt).Value = dblActualAmount


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub setGridTotalotherEarning()
        Try

            Dim intCurrRow As Integer? = Nothing
            Dim dblRate As Double = 0
            Dim dblHeadAmount As Double = 0
            Dim dblActualAmount As Double = 0
            For ii As Integer = 0 To gvOthers.Rows.Count - 1
                If (clsCommon.myLen(gvOthers.Rows(ii).Cells(colOthrEarngLineNo).Value) > 0) AndAlso clsCommon.CompairString(clsCommon.myCstr(gvOthers.Rows(ii).Cells(colOthrearngCode).Value), "TOTAL") <> CompairStringResult.Equal Then
                    dblRate = dblRate + clsCommon.myCdbl(gvOthers.Rows(ii).Cells(colOthrEarngRate).Value)
                    dblHeadAmount = dblHeadAmount + clsCommon.myCdbl(gvOthers.Rows(ii).Cells(colOthrEarngAmount).Value)
                    dblActualAmount = dblActualAmount + clsCommon.myCdbl(gvOthers.Rows(ii).Cells(colOthrEarngActualAmt).Value)
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvOthers.Rows(ii).Cells(colOthrearngCode).Value), "TOTAL") = CompairStringResult.Equal Then
                    intCurrRow = ii
                End If
            Next
            gvOthers.Rows(intCurrRow).Cells(colOthrEarngRate).Value = dblRate
            gvOthers.Rows(intCurrRow).Cells(colOthrEarngAmount).Value = dblHeadAmount
            gvOthers.Rows(intCurrRow).Cells(colOthrEarngActualAmt).Value = dblActualAmount
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub setGridTotalDeduction()
        Try


            Dim intCurrRow As Integer? = Nothing
            Dim dblRate As Double = 0
            Dim dblHeadAmount As Double = 0
            Dim dblActualAmount As Double = 0
            For ii As Integer = 0 To gvDeductions.Rows.Count - 1
                If (clsCommon.myLen(gvDeductions.Rows(ii).Cells(colDedLineNo).Value) > 0) AndAlso clsCommon.CompairString(clsCommon.myCstr(gvDeductions.Rows(ii).Cells(colDedPayHeadCode).Value), "TOTAL") <> CompairStringResult.Equal Then
                    dblRate = dblRate + clsCommon.myCdbl(gvDeductions.Rows(ii).Cells(colDedHeadRate).Value)
                    dblHeadAmount = dblHeadAmount + clsCommon.myCdbl(gvDeductions.Rows(ii).Cells(colDedHeadAmount).Value)
                    dblActualAmount = dblActualAmount + clsCommon.myCdbl(gvDeductions.Rows(ii).Cells(colDedActualAmt).Value)
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDeductions.Rows(ii).Cells(colDedPayHeadCode).Value), "TOTAL") = CompairStringResult.Equal Then
                    intCurrRow = ii
                End If
            Next
            gvDeductions.Rows(intCurrRow).Cells(colDedHeadRate).Value = dblRate
            gvDeductions.Rows(intCurrRow).Cells(colDedHeadAmount).Value = dblHeadAmount
            gvDeductions.Rows(intCurrRow).Cells(colDedActualAmt).Value = dblActualAmount
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SavingData(False)
    End Sub
    Public Function Save() As Boolean
        If AllowToSave() Then
            'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Dim issaved As Boolean
            Try
                Dim obj As New clsFFSettlement
                Dim TOTAL_EARNING_AMOUNT As Decimal = 0
                Dim TOTAL_OTHR_EARNING_AMOUNT As Decimal = 0
                Dim TOTAL_DEDUCTION_AMOUNT As Decimal = 0
                Dim NET_PAYABLE_AMOUNT As Decimal = 0

                obj.EMP_CODE = Me.txtCode.Value

                obj.DEPARTMENT_CODE = Me.lblDepartmentId.Text
                obj.DESIGNATION_ID = Me.lblDesignationId.Text
                obj.DOJ = Me.dtpDoJ.Value
                obj.RESIGN_SUBMIT_DATE = Me.dtpResignSubmitDate.Value
                obj.ACTUAL_LAST_WORKING_DAY = Me.dtpActualLastWDay.Value
                obj.LAST_SALARY_UPTO_DATE = Me.dtpLastSalUptoDate.Value
                obj.LAST_WORKING_DAY = Me.dtpLastWDay.Value
                obj.NOTICE_PERIOD = clsCommon.myCdbl(Me.lblNoticePeriod.Text)
                obj.SHORT_FALL_DAYS = Me.lblShortFallDays.Text
                obj.LEAVING_REASON = Me.txtReasonForLeaving.Text
                obj.TOTAL_SERVICE_PERIOD = Me.lblServiceRenderedPeriod.Text
                obj.WORKING_DAYS_AFTER_LAST_SAL = Me.txtNoofDaysLastSal.Text
                obj.DESCRIPTION = Me.txtDescription.Text
                obj.Last_PAY_PERIOD_CODE = Me.fndLastSalaryPayperiodCode.Value
                obj.PAY_PERIOD_CODE = Me.txtPayPeriod.Value
                obj.Document_Date = Me.txtdocdate.Value
                obj.CHEQUE_NO = txtChequeNo.Text
                obj.CHEQUE_DATED = dtpChequeDated.Value
                obj.CHEQUE_AMOUNT = clsCommon.myCdbl(txtChequeAmt.Text)
                obj.CHEQUE_CLEARANCE_DATE = dtpClearDate.Value
                '' saving operations
                Dim objListSal As New List(Of clsFFSalary)
                Dim objSal As clsFFSalary
                '' unpaid salary
                For Each row As GridViewRowInfo In gvSalStructAndUnpaidSalAmt.Rows
                    If clsCommon.myLen(row.Cells(colSalLineNo).Value) > 0 Then
                        objSal = New clsFFSalary
                        objSal.EMP_CODE = Me.txtCode.Value
                        objSal.Line_No = row.Cells(colSalLineNo).Value
                        objSal.Line_No = row.Cells(colSalLineNo).Value
                        objSal.PAY_HEAD_CODE = row.Cells(colSalPayHeadCode).Value
                        objSal.PAY_HEAD_DESC = row.Cells(colSalPayHeadDesc).Value

                        objSal.PAYHEAD_FORMULA = row.Cells(colSalPayHeadCode).Tag
                        objSal.RATE_AMOUNT = row.Cells(colSalHeadRate).Value
                        objSal.PAYHEAD_AMOUNT = row.Cells(colSalHeadAmount).Value
                        objSal.ACTUAL_AMOUNT = row.Cells(colSalUnpaidAmt).Value
                        objSal.REMARKS = row.Cells(colSalRemarks).Value
                        objSal.IS_DEDUCTION = 0
                        objSal.IS_OTHR_EARNING = 0
                        TOTAL_EARNING_AMOUNT = TOTAL_EARNING_AMOUNT + objSal.ACTUAL_AMOUNT
                        objListSal.Add(objSal)

                    End If

                Next
                '' other earnings
                For Each row As GridViewRowInfo In gvOthers.Rows
                    If clsCommon.myLen(row.Cells(colOthrEarngLineNo).Value) > 0 Then
                        objSal = New clsFFSalary
                        objSal.EMP_CODE = Me.txtCode.Value
                        objSal.Line_No = row.Cells(colOthrEarngLineNo).Value
                        objSal.PAY_HEAD_CODE = row.Cells(colOthrearngCode).Value
                        objSal.PAY_HEAD_DESC = row.Cells(colOthrEarngDesc).Value

                        objSal.PAYHEAD_FORMULA = row.Cells(colOthrearngCode).Tag
                        objSal.RATE_AMOUNT = row.Cells(colOthrEarngRate).Value
                        objSal.PAYHEAD_AMOUNT = row.Cells(colOthrEarngAmount).Value
                        objSal.ACTUAL_AMOUNT = row.Cells(colOthrEarngActualAmt).Value
                        objSal.REMARKS = row.Cells(colOthrEarngRemarks).Value
                        objSal.IS_DEDUCTION = 0
                        objSal.IS_OTHR_EARNING = 1
                        TOTAL_OTHR_EARNING_AMOUNT = TOTAL_OTHR_EARNING_AMOUNT + objSal.ACTUAL_AMOUNT
                        objListSal.Add(objSal)

                    End If

                Next
                '' deduction
                For Each row As GridViewRowInfo In gvDeductions.Rows
                    If clsCommon.myLen(row.Cells(colDedLineNo).Value) > 0 Then
                        objSal = New clsFFSalary
                        objSal.EMP_CODE = Me.txtCode.Value
                        objSal.Line_No = row.Cells(colDedLineNo).Value
                        objSal.Line_No = row.Cells(colDedLineNo).Value
                        objSal.PAY_HEAD_CODE = row.Cells(colDedPayHeadCode).Value
                        objSal.PAY_HEAD_DESC = row.Cells(colDedPayHeadDesc).Value

                        objSal.PAYHEAD_FORMULA = row.Cells(colDedPayHeadCode).Tag
                        objSal.RATE_AMOUNT = row.Cells(colDedHeadRate).Value
                        objSal.PAYHEAD_AMOUNT = row.Cells(colDedHeadAmount).Value
                        objSal.ACTUAL_AMOUNT = row.Cells(colDedActualAmt).Value
                        objSal.REMARKS = row.Cells(colDedRemarks).Value
                        objSal.IS_DEDUCTION = 1
                        objSal.IS_OTHR_EARNING = 0
                        TOTAL_DEDUCTION_AMOUNT = TOTAL_DEDUCTION_AMOUNT + objSal.ACTUAL_AMOUNT
                        objListSal.Add(objSal)

                    End If

                Next
                '' total tab
                obj.TOTAL_EARNING_AMOUNT = TOTAL_EARNING_AMOUNT
                obj.TOTAL_OTHR_EARNING_AMOUNT = TOTAL_OTHR_EARNING_AMOUNT
                obj.TOTAL_DEDUCTION_AMOUNT = TOTAL_DEDUCTION_AMOUNT
                obj.NET_PAYABLE_AMOUNT = TOTAL_EARNING_AMOUNT + TOTAL_OTHR_EARNING_AMOUNT - TOTAL_DEDUCTION_AMOUNT

                obj.ObjListSalary = objListSal
                issaved = clsFFSettlement.SaveData(obj, isNewEntry, Me.txtCode.Value)
                'trans.Commit()
                If issaved Then
                    LoadData(obj.EMP_CODE, NavigatorType.Current)
                    'clsCommon.MyMessageBoxShow("Document Saved Successfully.")
                    Return issaved
                Else
                    Return issaved
                End If
            Catch ex As Exception
                'If issaved = False Then
                '    trans.Rollback()
                'End If
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                Return False
            End Try
        Else
            Return False
        End If

        Return True
    End Function
    Function AllowToSave() As Boolean
        If btnsave.Text = "Update" Then
            Dim QryStr As String = "select POSTED from TSPL_FF_SETTLEMENT_HEAD where EMP_CODE = '" + txtCode.Value + "' "
            Dim chkpost As String = clsDBFuncationality.getSingleValue(QryStr)
            If chkpost = "1" Then
                clsCommon.MyMessageBoxShow(Me, "Transection already posted", Me.Text)
                Return False
            End If
        End If

        If clsCommon.myLen(txtCode.Value) <= 0 Then
            myMessages.blankValue("Employee Code")
            txtCode.Focus()
            Return False
        ElseIf clsCommon.myLen(fndLastSalaryPayperiodCode.Value) <= 0 Then
            myMessages.blankValue("Last Salary Pay Period Code")
            fndLastSalaryPayperiodCode.Focus()
            Return False
        ElseIf clsCommon.myLen(Me.lblDesignationId.Text) <= 0 Then
            myMessages.blankValue("Please update Designation of employee.")
            Return False
        End If
        If CheckNoDuesAndAssets(txtCode.Value) = False Then
            Return False
        End If
        Return True
    End Function
    Function CheckNoDuesAndAssets(ByVal Emp_Code As String) As Boolean
        Dim qry As String = "select NO_DUES from TSPL_EMPLOYEE_MASTER where emp_code='" & Emp_Code & "'"
        If clsCommon.myCBool(clsDBFuncationality.getSingleValue(qry)) = False Then
            clsCommon.MyMessageBoxShow(Me, "Update No dues in Employee Master.", Me.Text)
            Return False
        End If
        qry = "select sum(case when RETURNED='N' then 1 else 0 end) from TSPL_EMPLOYEE_ASSETS where EMP_CODE='" & Emp_Code & "'"
        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) > 0 Then
            clsCommon.MyMessageBoxShow(Me, "Some assets are not returned.", Me.Text)
            Return False
        End If
        Return True
    End Function


    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "You Cannot Delete Record", Me.Text)
            Exit Sub
        End If
        funDelete()
    End Sub

    Sub funDelete()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If clsCancelLog.CheckForReasonOnDelete() Then
                    '' REASON FOR DELETE 
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Delete"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If
                End If
                If (clsFFSettlement.DeleteData(txtCode.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtCode.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function




    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try

            If (myMessages.postConfirm()) Then
                SavingData(True)
                If (clsFFSettlement.PostData(txtCode.Value, True)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SavingData(ByVal ChekBtnPost As Boolean)
        If (Save()) Then
            If ChekBtnPost = False Then
                common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
            End If
        End If
    End Sub


    Private Sub funPrint()
        'Try
        '    Dim qry As String = " select '" & objCommonVar.CurrentCompanyName & "' as Company_Name, TSPL_FF_SETTLEMENT_HEAD.PROD_ITEM_CODE  as BuildItemCode,CONVERT(VARCHAR,TSPL_FF_SETTLEMENT_HEAD.MO_DATE,103) as BOMDate,CONVERT(VARCHAR,TSPL_FF_SETTLEMENT_HEAD.START_DATE,103) as StartDate,"
        '    qry += " CONVERT(VARCHAR,TSPL_FF_SETTLEMENT_HEAD.END_DATE,103) as EndDate,TSPL_FF_SETTLEMENT_HEAD.STATUS as BomStatus,TSPL_FF_SETTLEMENT_HEAD.PROD_ITEM_UNIT_CODE as BuildUOM,"
        '    qry += " TSPL_FF_SETTLEMENT_HEAD.PROD_QUANTITY as BuildQty, "
        '    qry += " TSPL_FF_SETTLEMENT_HEAD.MIN_BATCH_SIZE as MinBatchSize,TSPL_MF_BOM_DETAIL.LINE_NO as SL_No,TSPL_MF_BOM_DETAIL.CONSM_ITEM_CATEGORY_CODE as ItemCategory,"
        '    qry += " TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE as ItemCode,TSPL_MF_BOM_DETAIL.ITEM_DESCRIPTION as ItemDesc,TSPL_MF_BOM_DETAIL.CONSM_ITEM_UNIT_CODE as UOM,"
        '    qry += " TSPL_MF_BOM_DETAIL.CONSM_QUANTITY as Quantity,TSPL_MF_BOM_DETAIL.SCRAP_PERCENT as Scrap,TSPL_MF_BOM_DETAIL.WASTAGE_PERCENT as Wastage,"
        '    qry += " TSPL_MF_BOM_DETAIL.REMARKS as Remarks from TSPL_FF_SETTLEMENT_HEAD inner join TSPL_MF_BOM_DETAIL on TSPL_FF_SETTLEMENT_HEAD.EMP_CODE=TSPL_MF_BOM_DETAIL.EMP_CODE"
        '    qry += " where 2=2"

        '    If txtCode.Value <> "" Then
        '        qry += " and  TSPL_FF_SETTLEMENT_HEAD.EMP_CODE='" & txtCode.Value & "' "
        '    End If
        '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        '    ProductionReportViewer.funreport(dt, "crptBOMPrint", "Bill Of Material")

        'Catch ex As Exception
        '    common.clsCommon.MyMessageBoxShow(ex.Message)
        'End Try
    End Sub



    Private Sub txtCode__MYNavigator1(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCode._MYValidating

        Dim str As String = "select count(*) from TSPL_EMPLOYEE_MASTER where EMP_CODE ='" + txtCode.Value + "' AND RESIGNATION_SUBMIT_DATE IS NOT NULL"
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then

            'Dim qry As String = "select EMP_CODE as Code, Emp_Name as Name, Designation from TSPL_EMPLOYEE_MASTER"
            'txtCode.Value = clsCommon.ShowSelectForm("EMP_MASTER", qry, "Code", "", txtCode.Value, "EMP_CODE", isButtonClicked)
            'txtCode.Value = clsFinder.ShowEmployeeFinder(, , txtCode.Value)
            txtCode.Value = clsEmployeeMaster.getFinder("RESIGNATION_SUBMIT_DATE IS NOT NULL", txtCode.Value, isButtonClicked)
            If clsCommon.myLen(txtCode.Value) > 0 Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                funReset()
            End If
        End If
    End Sub

    Private Sub fndLastSalaryPayperiodCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndLastSalaryPayperiodCode._MYValidating

        Dim qry As String = "select PAY_PERIOD_CODE as Code , PAY_PERIOD_NAME as Name, DATE_FROM as 'From Date', DATE_TO AS 'To Date', DESCRIPTION as Description  from TSPL_PAYPERIOD_MASTER"
        fndLastSalaryPayperiodCode.Value = clsCommon.ShowSelectForm("PAYPERIOD_Master", qry, "Code", "POSTED=1", txtCode.Value, "PAY_PERIOD_CODE", isButtonClicked)

    End Sub

    Private Sub btnnew_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        funReset()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("Please Select Employee Code For Print")
                txtCode.Focus()
                txtCode.Select()
                Return
            End If

            'Dim qry As String = " select convert(varchar,TSPL_FF_SETTLEMENT_HEAD.Document_Date,103) as Document_Date ,(TSPL_COMPANY_MASTER.Comp_Name + ',' + Location_Desc ) as Name_Of_Unit,(case when TSPL_FF_SALARY.IS_DEDUCTION=0 and TSPL_FF_SALARY.IS_OTHR_EARNING=0 then (TSPL_FF_SALARY.RATE_AMOUNT) else 0 end) as rate_sal,(case when TSPL_FF_SALARY.IS_DEDUCTION=0 and TSPL_FF_SALARY.IS_OTHR_EARNING=0 then (TSPL_FF_SALARY.ACTUAL_AMOUNT) else 0 end) as sal,(case when TSPL_FF_SALARY.IS_DEDUCTION=0 and TSPL_FF_SALARY.IS_OTHR_EARNING=1 then (TSPL_FF_SALARY.ACTUAL_AMOUNT) else 0 end) as other,(case when TSPL_FF_SALARY.IS_DEDUCTION=1 and TSPL_FF_SALARY.IS_OTHR_EARNING=0 then (TSPL_FF_SALARY.ACTUAL_AMOUNT) else 0 end) as deduction,TSPL_COMPANY_MASTER.Comp_Code,TSPL_COMPANY_MASTER.Comp_Name,(TSPL_COMPANY_MASTER.Add1+' '+TSPL_COMPANY_MASTER.Add2+' '+TSPL_COMPANY_MASTER.Add3) as address,TSPL_CITY_MASTER.City_Name,TSPL_STATE_MASTER.state_name as STATE_CODE,TSPL_COMPANY_MASTER.Pincode,TSPL_FF_SETTLEMENT_HEAD.EMP_CODE,TSPL_EMPLOYEE_MASTER.Emp_Name,TSPL_DESIGNATION_MASTER.Designation_Desc,TSPL_DEPARTMENT_MASTER.DEPARTMENT_NAME,convert(varchar,TSPL_EMPLOYEE_MASTER.Joining_date,103) as doj," & _
            '" convert(varchar,TSPL_FF_SETTLEMENT_HEAD.RESIGN_SUBMIT_DATE,103) as RESIGN_SUBMIT_DATE,TSPL_FF_SETTLEMENT_HEAD.NOTICE_PERIOD,convert(varchar,TSPL_FF_SETTLEMENT_HEAD.ACTUAL_LAST_WORKING_DAY,103) as ACTUAL_LAST_WORKING_DAY,convert(varchar,TSPL_FF_SETTLEMENT_HEAD.LAST_WORKING_DAY,103) as LAST_WORKING_DAY,TSPL_FF_SETTLEMENT_HEAD.SHORT_FALL_DAYS,TSPL_FF_SETTLEMENT_HEAD.LEAVING_REASON,TSPL_FF_SETTLEMENT_HEAD.TOTAL_SERVICE_PERIOD,convert(varchar,TSPL_FF_SETTLEMENT_HEAD.LAST_SALARY_UPTO_DATE,103) as LAST_SALARY_UPTO_DATE,TSPL_FF_SETTLEMENT_HEAD.WORKING_DAYS_AFTER_LAST_SAL,TSPL_FF_SALARY.PAY_HEAD_CODE,TSPL_FF_SALARY.PAY_HEAD_DESC,TSPL_FF_SALARY.PAYHEAD_AMOUNT,TSPL_FF_SALARY.RATE_AMOUNT,TSPL_FF_SALARY.ACTUAL_AMOUNT as UnpaidAmount,TSPL_FF_SALARY.IS_DEDUCTION,TSPL_FF_SALARY.IS_OTHR_EARNING from TSPL_FF_SETTLEMENT_HEAD  left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_FF_SETTLEMENT_HEAD.EMP_CODE " & _
            '" left join tspl_location_master on tspl_location_master.Location_Code = TSPL_EMPLOYEE_MASTER.WORKING_LOCATION_CODE  " & _
            '" left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = TSPL_EMPLOYEE_MASTER.Comp_Code  " & _
            '" left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_COMPANY_MASTER.City_Code " & _
            '" left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE=TSPL_COMPANY_MASTER.State " & _
            '" left outer join TSPL_DESIGNATION_MASTER on TSPL_DESIGNATION_MASTER.Designation_id=TSPL_FF_SETTLEMENT_HEAD.DEPARTMENT_CODE " & _
            '" left outer join TSPL_DEPARTMENT_MASTER on TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE=TSPL_FF_SETTLEMENT_HEAD.DEPARTMENT_CODE  left outer join TSPL_FF_SALARY on TSPL_FF_SALARY.EMP_CODE=TSPL_FF_SETTLEMENT_HEAD.EMP_CODE  LEFT JOIN TSPL_PAYHEAD_MASTER ON TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE = TSPL_FF_SALARY.PAY_HEAD_CODE where TSPL_FF_SETTLEMENT_HEAD.EMP_CODE='" + txtCode.Value + "' AND TSPL_PAYHEAD_MASTER.SUB_HEAD_TYPE <> 'EPS' "
            'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)



            Dim qry As String = "select  Document_Date , Name_Of_Unit, rate_sal, sal, other, deduction,address,City_Name, STATE_CODE,Pincode,EMP_CODE,Emp_Name,Designation_Desc,DEPARTMENT_NAME, doj, RESIGN_SUBMIT_DATE,NOTICE_PERIOD, ACTUAL_LAST_WORKING_DAY, LAST_WORKING_DAY,SHORT_FALL_DAYS,LEAVING_REASON,TOTAL_SERVICE_PERIOD,LAST_SALARY_UPTO_DATE,WORKING_DAYS_AFTER_LAST_SAL,PAY_HEAD_CODE,PAY_HEAD_DESC,PAYHEAD_AMOUNT,RATE_AMOUNT, UnpaidAmount,IS_DEDUCTION,IS_OTHR_EARNING from  (select convert(varchar,TSPL_FF_SETTLEMENT_HEAD.Document_Date,103) as Document_Date ,( Location_Desc ) as Name_Of_Unit" & _
 " ,(case when TSPL_FF_SALARY.IS_DEDUCTION=0 and TSPL_FF_SALARY.IS_OTHR_EARNING=0 then (TSPL_FF_SALARY.RATE_AMOUNT) else 0 end) as rate_sal" & _
 " ,(case when TSPL_FF_SALARY.IS_DEDUCTION=0 and TSPL_FF_SALARY.IS_OTHR_EARNING=0 then (TSPL_FF_SALARY.ACTUAL_AMOUNT) else 0 end) as sal" & _
" ,(case when TSPL_FF_SALARY.IS_DEDUCTION=0 and TSPL_FF_SALARY.IS_OTHR_EARNING=1 then (TSPL_FF_SALARY.ACTUAL_AMOUNT) else 0 end) as other" & _
" ,(case when TSPL_FF_SALARY.IS_DEDUCTION=1 and TSPL_FF_SALARY.IS_OTHR_EARNING=0 then (TSPL_FF_SALARY.ACTUAL_AMOUNT) else 0 end) as deduction" & _
" ,TSPL_COMPANY_MASTER.Comp_Code,TSPL_COMPANY_MASTER.Comp_Name,(TSPL_COMPANY_MASTER.Add1+' '+TSPL_COMPANY_MASTER.Add2+' '+TSPL_COMPANY_MASTER.Add3) as address,TSPL_CITY_MASTER.City_Name,TSPL_STATE_MASTER.state_name as STATE_CODE,TSPL_COMPANY_MASTER.Pincode,TSPL_FF_SETTLEMENT_HEAD.EMP_CODE,TSPL_EMPLOYEE_MASTER.Emp_Name,TSPL_DESIGNATION_MASTER.Designation_Desc,TSPL_DEPARTMENT_MASTER.DEPARTMENT_NAME,convert(varchar,TSPL_EMPLOYEE_MASTER.Joining_date,103) as doj, convert(varchar,TSPL_FF_SETTLEMENT_HEAD.RESIGN_SUBMIT_DATE,103) as RESIGN_SUBMIT_DATE,TSPL_FF_SETTLEMENT_HEAD.NOTICE_PERIOD,convert(varchar,TSPL_FF_SETTLEMENT_HEAD.ACTUAL_LAST_WORKING_DAY,103) as ACTUAL_LAST_WORKING_DAY,convert(varchar,TSPL_FF_SETTLEMENT_HEAD.LAST_WORKING_DAY,103) as LAST_WORKING_DAY,TSPL_FF_SETTLEMENT_HEAD.SHORT_FALL_DAYS,TSPL_FF_SETTLEMENT_HEAD.LEAVING_REASON,TSPL_FF_SETTLEMENT_HEAD.TOTAL_SERVICE_PERIOD,convert(varchar,TSPL_FF_SETTLEMENT_HEAD.LAST_SALARY_UPTO_DATE,103) as LAST_SALARY_UPTO_DATE,TSPL_FF_SETTLEMENT_HEAD.WORKING_DAYS_AFTER_LAST_SAL,TSPL_FF_SALARY.PAY_HEAD_CODE,TSPL_FF_SALARY.PAY_HEAD_DESC,TSPL_FF_SALARY.PAYHEAD_AMOUNT,TSPL_FF_SALARY.RATE_AMOUNT,TSPL_FF_SALARY.ACTUAL_AMOUNT as UnpaidAmount,TSPL_FF_SALARY.IS_DEDUCTION,TSPL_FF_SALARY.IS_OTHR_EARNING from TSPL_FF_SETTLEMENT_HEAD  left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_FF_SETTLEMENT_HEAD.EMP_CODE  left join tspl_location_master on tspl_location_master.Location_Code = TSPL_EMPLOYEE_MASTER.WORKING_LOCATION_CODE   left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = TSPL_EMPLOYEE_MASTER.Comp_Code   left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_COMPANY_MASTER.City_Code  left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE=TSPL_COMPANY_MASTER.State  left outer join TSPL_DESIGNATION_MASTER on TSPL_DESIGNATION_MASTER.Designation_id=TSPL_FF_SETTLEMENT_HEAD.DESIGNATION_ID  left outer join TSPL_DEPARTMENT_MASTER on TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE=TSPL_FF_SETTLEMENT_HEAD.DEPARTMENT_CODE  left outer join TSPL_FF_SALARY on TSPL_FF_SALARY.EMP_CODE=TSPL_FF_SETTLEMENT_HEAD.EMP_CODE  LEFT JOIN TSPL_PAYHEAD_MASTER ON TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE = TSPL_FF_SALARY.PAY_HEAD_CODE where TSPL_FF_SETTLEMENT_HEAD.EMP_CODE='" + txtCode.Value + "' and  IS_DEDUCTION = 0 and IS_OTHR_EARNING = 0" & _
 " union all select * from (select convert(varchar,TSPL_FF_SETTLEMENT_HEAD.Document_Date,103) as Document_Date ,(Location_Desc ) as Name_Of_Unit,0 as rate_sal ,0 as sal,(case when TSPL_FF_SALARY.IS_DEDUCTION=0 and TSPL_FF_SALARY.IS_OTHR_EARNING=1 then (TSPL_FF_SALARY.ACTUAL_AMOUNT) else 0 end) as other,0 as deduction" & _
" ,TSPL_COMPANY_MASTER.Comp_Code,TSPL_COMPANY_MASTER.Comp_Name,(TSPL_COMPANY_MASTER.Add1+' '+TSPL_COMPANY_MASTER.Add2+' '+TSPL_COMPANY_MASTER.Add3) as address,TSPL_CITY_MASTER.City_Name,TSPL_STATE_MASTER.state_name as STATE_CODE,TSPL_COMPANY_MASTER.Pincode,TSPL_FF_SETTLEMENT_HEAD.EMP_CODE,TSPL_EMPLOYEE_MASTER.Emp_Name,TSPL_DESIGNATION_MASTER.Designation_Desc,TSPL_DEPARTMENT_MASTER.DEPARTMENT_NAME,convert(varchar,TSPL_EMPLOYEE_MASTER.Joining_date,103) as doj, convert(varchar,TSPL_FF_SETTLEMENT_HEAD.RESIGN_SUBMIT_DATE,103) as RESIGN_SUBMIT_DATE,TSPL_FF_SETTLEMENT_HEAD.NOTICE_PERIOD,convert(varchar,TSPL_FF_SETTLEMENT_HEAD.ACTUAL_LAST_WORKING_DAY,103) as ACTUAL_LAST_WORKING_DAY,convert(varchar,TSPL_FF_SETTLEMENT_HEAD.LAST_WORKING_DAY,103) as LAST_WORKING_DAY,TSPL_FF_SETTLEMENT_HEAD.SHORT_FALL_DAYS,TSPL_FF_SETTLEMENT_HEAD.LEAVING_REASON,TSPL_FF_SETTLEMENT_HEAD.TOTAL_SERVICE_PERIOD,convert(varchar,TSPL_FF_SETTLEMENT_HEAD.LAST_SALARY_UPTO_DATE,103) as LAST_SALARY_UPTO_DATE,TSPL_FF_SETTLEMENT_HEAD.WORKING_DAYS_AFTER_LAST_SAL,TSPL_FF_SALARY.PAY_HEAD_CODE,TSPL_FF_SALARY.PAY_HEAD_DESC,TSPL_FF_SALARY.PAYHEAD_AMOUNT,TSPL_FF_SALARY.RATE_AMOUNT,TSPL_FF_SALARY.ACTUAL_AMOUNT as UnpaidAmount,TSPL_FF_SALARY.IS_DEDUCTION,TSPL_FF_SALARY.IS_OTHR_EARNING from TSPL_FF_SETTLEMENT_HEAD  left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_FF_SETTLEMENT_HEAD.EMP_CODE  left join tspl_location_master on tspl_location_master.Location_Code = TSPL_EMPLOYEE_MASTER.WORKING_LOCATION_CODE   left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = TSPL_EMPLOYEE_MASTER.Comp_Code   left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_COMPANY_MASTER.City_Code  left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE=TSPL_COMPANY_MASTER.State  left outer join TSPL_DESIGNATION_MASTER on TSPL_DESIGNATION_MASTER.Designation_id=TSPL_FF_SETTLEMENT_HEAD.DESIGNATION_ID  left outer join TSPL_DEPARTMENT_MASTER on TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE=TSPL_FF_SETTLEMENT_HEAD.DEPARTMENT_CODE  left outer join TSPL_FF_SALARY on TSPL_FF_SALARY.EMP_CODE=TSPL_FF_SETTLEMENT_HEAD.EMP_CODE  LEFT JOIN TSPL_PAYHEAD_MASTER ON TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE = TSPL_FF_SALARY.PAY_HEAD_CODE where TSPL_FF_SETTLEMENT_HEAD.EMP_CODE='" + txtCode.Value + "') as mm where other > 0" & _
" union all select * from (select convert(varchar,TSPL_FF_SETTLEMENT_HEAD.Document_Date,103) as Document_Date ,(Location_Desc ) as Name_Of_Unit,0 as rate_sal,0 as sal,0 as other,(case when TSPL_FF_SALARY.IS_DEDUCTION=1 and TSPL_FF_SALARY.IS_OTHR_EARNING=0 then (TSPL_FF_SALARY.ACTUAL_AMOUNT) else 0 end) as deduction " & _
" ,TSPL_COMPANY_MASTER.Comp_Code,TSPL_COMPANY_MASTER.Comp_Name,(TSPL_COMPANY_MASTER.Add1+' '+TSPL_COMPANY_MASTER.Add2+' '+TSPL_COMPANY_MASTER.Add3) as address,TSPL_CITY_MASTER.City_Name,TSPL_STATE_MASTER.state_name as STATE_CODE,TSPL_COMPANY_MASTER.Pincode,TSPL_FF_SETTLEMENT_HEAD.EMP_CODE,TSPL_EMPLOYEE_MASTER.Emp_Name,TSPL_DESIGNATION_MASTER.Designation_Desc,TSPL_DEPARTMENT_MASTER.DEPARTMENT_NAME,convert(varchar,TSPL_EMPLOYEE_MASTER.Joining_date,103) as doj, convert(varchar,TSPL_FF_SETTLEMENT_HEAD.RESIGN_SUBMIT_DATE,103) as RESIGN_SUBMIT_DATE,TSPL_FF_SETTLEMENT_HEAD.NOTICE_PERIOD,convert(varchar,TSPL_FF_SETTLEMENT_HEAD.ACTUAL_LAST_WORKING_DAY,103) as ACTUAL_LAST_WORKING_DAY,convert(varchar,TSPL_FF_SETTLEMENT_HEAD.LAST_WORKING_DAY,103) as LAST_WORKING_DAY,TSPL_FF_SETTLEMENT_HEAD.SHORT_FALL_DAYS,TSPL_FF_SETTLEMENT_HEAD.LEAVING_REASON,TSPL_FF_SETTLEMENT_HEAD.TOTAL_SERVICE_PERIOD,convert(varchar,TSPL_FF_SETTLEMENT_HEAD.LAST_SALARY_UPTO_DATE,103) as LAST_SALARY_UPTO_DATE,TSPL_FF_SETTLEMENT_HEAD.WORKING_DAYS_AFTER_LAST_SAL,TSPL_FF_SALARY.PAY_HEAD_CODE,TSPL_FF_SALARY.PAY_HEAD_DESC,TSPL_FF_SALARY.PAYHEAD_AMOUNT,TSPL_FF_SALARY.RATE_AMOUNT,TSPL_FF_SALARY.ACTUAL_AMOUNT as UnpaidAmount,TSPL_FF_SALARY.IS_DEDUCTION,TSPL_FF_SALARY.IS_OTHR_EARNING from TSPL_FF_SETTLEMENT_HEAD  left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_FF_SETTLEMENT_HEAD.EMP_CODE  left join tspl_location_master on tspl_location_master.Location_Code = TSPL_EMPLOYEE_MASTER.WORKING_LOCATION_CODE   left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = TSPL_EMPLOYEE_MASTER.Comp_Code   left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_COMPANY_MASTER.City_Code  left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE=TSPL_COMPANY_MASTER.State  left outer join TSPL_DESIGNATION_MASTER on TSPL_DESIGNATION_MASTER.Designation_id=TSPL_FF_SETTLEMENT_HEAD.DESIGNATION_ID  left outer join TSPL_DEPARTMENT_MASTER on TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE=TSPL_FF_SETTLEMENT_HEAD.DEPARTMENT_CODE  left outer join TSPL_FF_SALARY on TSPL_FF_SALARY.EMP_CODE=TSPL_FF_SETTLEMENT_HEAD.EMP_CODE  LEFT JOIN TSPL_PAYHEAD_MASTER ON TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE = TSPL_FF_SALARY.PAY_HEAD_CODE where TSPL_FF_SETTLEMENT_HEAD.EMP_CODE='" + txtCode.Value + "') as mm where  deduction > 0 ) as final"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim frmcrsytal As New frmCrystalReportViewer
                frmcrsytal.funsubreportWithdt(CrystalReportFolder.HRPayroll, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptFullAndFinalSettlement", "Full And Final Settlement", "rptCompanyAddress.rpt")
                'frmCrystalReportViewer.funreport(CrystalReportFolder.HRPayroll, dt, "crptFullAndFinalSettlement", "Full And Final Settlement")
            Else
                Throw New Exception("No Data Found")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub printResignation()
        If clsCommon.myLen(txtCode.Value) > 0 Then
            Dim strQuery As String = "select Emp_Name,Designation_Desc  from TSPL_FF_SETTLEMENT_HEAD left join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_FF_SETTLEMENT_HEAD.EMP_CODE left join TSPL_DESIGNATION_MASTER on TSPL_DESIGNATION_MASTER.Designation_id =TSPL_EMPLOYEE_MASTER.Designation"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQuery)
            Dim frmcrsytal As New frmCrystalReportViewer
            frmcrsytal.funreport(CrystalReportFolder.HRPayroll, dt, "crptresignationLetter", "Resignation Letter")
        Else
            clsCommon.MyMessageBoxShow(Me, "Please select an Employee Code to print", Me.Text)
        End If
    End Sub
    Private Sub btnPrintResignation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintResignation.Click
        printResignation()
    End Sub
    Sub printFinalDeclaration()
        If clsCommon.myLen(txtCode.Value) > 0 Then
            Dim strQuery As String = "select TSPL_DESIGNATION_MASTER.Designation_Desc ,TSPL_EMPLOYEE_MASTER.add2 as Perma_Add,Working_City_Code,Perma_City_Code.City_Name ,PERMA_CITY_CODE ,PERMA_STATE_CODE,Perma_State_code.STATE_NAME,Working_location.Location_Code as Posted_LOC_Code,Working_location.Location_Desc as Working_Loc_Desc,TSPL_COMPANY_MASTER.Comp_Code, TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end  +case when LEN(isnull(TSPL_COMPANY_MASTER.City_Code ,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.City_Code,'') else ' ' end " & _
            " +case when LEN(isnull(TSPL_COMPANY_MASTER.State  ,''))>0 then ', '+isnull(Comp_State.STATE_NAME  ,'') else ' ' end " & _
            " +case when LEN(isnull(TSPL_COMPANY_MASTER.Pincode   ,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Pincode  ,'') else ' ' end" & _
            " as Comp_Add ,TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end " & _
            " +case when LEN(isnull(TSPL_LOCATION_MASTER.City_Code ,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.City_Code,'') else ' ' end " & _
            " +case when LEN(isnull(TSPL_LOCATION_MASTER.State  ,''))>0 then ', '+isnull(Location_State.STATE_NAME  ,'') else ' ' end" & _
            " as Loc_Add ,TSPL_EMPLOYEE_MASTER.EMail_ID as emp_email_id,TSPL_EMPLOYEE_MASTER.Add1 +case when len(TSPL_EMPLOYEE_MASTER.add2)>0 then ', '+TSPL_EMPLOYEE_MASTER.add2 else '' end as EMP_Add , Emp_Name ,FATHERS_NAME,TSPL_EMPLOYEE_MASTER.Add1 ,NET_PAYABLE_AMOUNT ,convert (varchar,LAST_SALARY_UPTO_DATE,103)as LAST_SALARY_UPTO_DATE ," & _
            " TSPL_EMPLOYEE_MASTER.Designation, TSPL_EMPLOYEE_MASTER.EMP_CODE, TSPL_LOCATION_MASTER.Location_Code, TSPL_LOCATION_MASTER.Location_Desc" & _
            " from TSPL_FF_SETTLEMENT_HEAD" & _
            " left join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_FF_SETTLEMENT_HEAD.EMP_CODE" & _
            " left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_EMPLOYEE_MASTER.Comp_Code " & _
            " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code =TSPL_EMPLOYEE_MASTER.LOCATION_CODE " & _
            " left join TSPL_STATE_MASTER as Comp_State on Comp_State .STATE_CODE =TSPL_COMPANY_MASTER.State" & _
            " left join TSPL_STATE_MASTER as Location_State on Location_State .STATE_CODE =TSPL_LOCATION_MASTER.State  " & _
           " left join TSPL_LOCATION_MASTER Working_location on Working_location.Location_Code =TSPL_EMPLOYEE_MASTER.WORKING_LOCATION_CODE " & _
            " left join TSPL_CITY_MASTER Perma_City_Code on Perma_City_Code.City_Code =TSPL_EMPLOYEE_MASTER.PERMA_CITY_CODE" & _
            " left join TSPL_STATE_MASTER Perma_State_code on Perma_State_code.STATE_CODE =TSPL_EMPLOYEE_MASTER.PERMA_STATE_CODE" & _
          "  left join TSPL_DESIGNATION_MASTER on TSPL_DESIGNATION_MASTER.Designation_id =TSPL_EMPLOYEE_MASTER.Designation " & _
            " where  TSPL_FF_SETTLEMENT_HEAD.EMP_CODE ='" + txtCode.Value + "'"


            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQuery)
            Dim frmcrsytal As New frmCrystalReportViewer
            frmcrsytal.funreport(CrystalReportFolder.HRPayroll, dt, "crptFinalDeclaration", "Final Declaration")
        Else
            clsCommon.MyMessageBoxShow(Me, "Please select an Employee Code to print", Me.Text)
        End If
    End Sub
    Sub printNoDues()
        If clsCommon.myLen(txtCode.Value) > 0 Then
            Dim strQuery As String = "select TSPL_DESIGNATION_MASTER.Designation_Desc,TSPL_EMPLOYEE_MASTER.Designation ,TSPL_EMPLOYEE_MASTER.EMP_CODE  , Emp_Name ,FATHERS_NAME,Add1 ,convert (varchar,LAST_WORKING_DAY,103)as LAST_WORKING_DAY    from TSPL_FF_SETTLEMENT_HEAD left join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_FF_SETTLEMENT_HEAD.EMP_CODE " & _
                                     " left join TSPL_DESIGNATION_MASTER on TSPL_DESIGNATION_MASTER.Designation_id =TSPL_EMPLOYEE_MASTER.Designation " & _
                                     " where 2=2 and TSPL_EMPLOYEE_MASTER.EMP_CODE='" + txtCode.Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQuery)
            Dim frmcrsytal As New frmCrystalReportViewer
            frmcrsytal.funreport(CrystalReportFolder.HRPayroll, dt, "crptNoDues", "No Dues Certificate")
        Else
            clsCommon.MyMessageBoxShow(Me, "Please select an Employee Code to print", Me.Text)
        End If
    End Sub
    Private Sub BtnPrintNoDues_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrintNoDues.Click
        printNoDues()
    End Sub

    Private Sub btnPrintFinalDeclaration_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintFinalDeclaration.Click
        printFinalDeclaration()
    End Sub

    Private Sub gvSalStructAndUnpaidSalAmt_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvSalStructAndUnpaidSalAmt.CellValueChanged


        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gvSalStructAndUnpaidSalAmt.Columns(colSalHeadRate) OrElse e.Column Is gvSalStructAndUnpaidSalAmt.Columns(colSalHeadAmount) OrElse e.Column Is gvSalStructAndUnpaidSalAmt.Columns(colSalUnpaidAmt) Then
                        setGridTotalSalStructAndUnpaidSalAmt()
                    End If


                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gvOthers_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvOthers.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True

                    If e.Column Is gvOthers.Columns(colOthrEarngRate) OrElse e.Column Is gvOthers.Columns(colOthrEarngAmount) OrElse e.Column Is gvOthers.Columns(colOthrEarngActualAmt) Then
                        setGridTotalotherEarning()
                    End If


                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gvDeductions_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvDeductions.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True

                    If e.Column Is gvDeductions.Columns(colDedHeadRate) OrElse e.Column Is gvDeductions.Columns(colDedActualAmt) OrElse e.Column Is gvDeductions.Columns(colDedHeadAmount) Then
                        setGridTotalDeduction()
                    End If

                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class