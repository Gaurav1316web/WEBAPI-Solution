Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class frmLockMPCollectionPC
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
    Dim is_Load As Boolean = True
    Dim AllowDateChanged As Boolean = False
    Public isEmpOnAmtOnly As Boolean = False
    Dim isLoad As Boolean = True
#End Region

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SavingData(False)
    End Sub

    Public Function Save() As Boolean
        If AllowToSave() Then

            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmLockMPCollectionPC, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return False
                End If
            End If

            Dim obj As New clsLockMPPaymentCycle()
            obj.LOCK_CODE = txtCode.Value
            obj.MCC_Code = fndLoc.Value
            obj.Description = txtDescription.Text
            obj.FROM_DATE = dtpFromDate.Value
            obj.TO_DATE = dtpToDate.Value
            obj.POSTED = "N"
            If (obj.SaveData(obj, isNewEntry)) Then
                'common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                LoadData(obj.LOCK_CODE, NavigatorType.Current)
                Return True
                'Else
                '    common.clsCommon.MyMessageBoxShow("This '" & obj.LOCK_CODE & "' already exist ")
            End If
        End If
        Return False
    End Function

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        txtCode.MyReadOnly = True
        isNewEntry = False
        Dim obj As New clsLockMPPaymentCycle()
        obj = clsLockMPPaymentCycle.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.LOCK_CODE) > 0) Then
            funReset()
            isNewEntry = False
            btnSave.Text = "Update"

            If obj.POSTED = "Y" Then
                btnSave.Enabled = False
                btnPost.Enabled = False
                btnDelete.Enabled = False
                UsLock1.Status = ERPTransactionStatus.Approved
            Else
                btnSave.Enabled = True
                btnDelete.Enabled = True
                btnPost.Enabled = True
                UsLock1.Status = ERPTransactionStatus.Pending
            End If
            txtCode.Value = obj.LOCK_CODE
            txtCode.MyReadOnly = True
            fndLoc.Value = obj.MCC_Code
            txtLocName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Location_Desc from tspl_location_master where tspl_location_master.Location_Code='" + obj.MCC_Code + "' "))
            txtDescription.Text = obj.Description
            dtpFromDate.Value = obj.FROM_DATE
            dtpToDate.Value = obj.TO_DATE
        End If

    End Sub

    Function AllowToSave() As Boolean

        If btnSave.Text = "Update" Then
            Dim QryStr As String = "select POSTED from TSPL_LOCK_MP_PC where LOCK_CODE = '" + txtCode.Value + "' "
            Dim chkpost As String = clsDBFuncationality.getSingleValue(QryStr)
            If chkpost = "1" Then
                clsCommon.MyMessageBoxShow(Me, "Transection already posted", Me.Text)
                Return False
            End If
        End If

        If clsCommon.myLen(fndLoc.Value) <= 0 Then
            myMessages.blankValue("MCC")
            fndLoc.Focus()
            Return False

        ElseIf clsCommon.myLen(dtpToDate.Value.ToLongDateString()) <= 0 Then
            myMessages.blankValue("To Date")
            dtpToDate.Focus()
            Return False
        ElseIf clsCommon.myLen(dtpFromDate.Value.ToLongDateString()) <= 0 Then
            myMessages.blankValue("From Date")
            dtpFromDate.Focus()
            Return False
        End If
        Dim strchk As String = "select LOCK_CODE from TSPL_LOCK_MP_PC where ( FROM_DATE  between '" + clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy") + "' and '" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") + "' or TO_DATE  between '" + clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy") + "' and '" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") + "' ) and LOCK_CODE <> '" + txtCode.Value + "' and MCC_Code='" & fndLoc.Value & "' "
        Dim chkPayPeriod As String = clsDBFuncationality.getSingleValue(strchk)
        If clsCommon.myLen(chkPayPeriod) > 0 Then
            clsCommon.MyMessageBoxShow(Me, "From or To date overlapped with Lock Code " + chkPayPeriod + " . Overlapping Lock periods can not be created.")
            Return False
        End If
        'Dim strCode As String = clsLockMPPaymentCycle.CheckNameExistness(fndLoc.Value, txtCode.Value, Nothing)
        'If clsCommon.myLen(strCode) > 0 Then
        '    clsCommon.MyMessageBoxShow("Name Allready Exist in Lock Code : " + strCode + ". Please Choose another  Name.")
        '    Return False
        'End If
        Return True
    End Function

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        Dim dt As DataTable = getdata()
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            RadPageView1.SelectedPage = RadPageViewPage1
            RadPageViewPage2.Enabled = False
            PostData()
        Else
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dt
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            RadPageView1.SelectedPage = RadPageViewPage2
            RadPageViewPage2.Enabled = True
            gv.BestFitColumns()
            For ii As Integer = 0 To gv.Columns.Count - 1
                gv.Columns(ii).ReadOnly = True
                gv.Columns(ii).IsVisible = True

            Next
        End If
    End Sub

    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                SavingData(True)


                If (clsLockMPPaymentCycle.PostData(txtCode.Value, True)) Then
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

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "You Cannot Delete Record", Me.Text)
            Exit Sub
        End If
        'Dim discCode As String
        'discCode = clsDBFuncationality.getSingleValue("select LOCK_CODE  from TSPL_SHIPMENT_DETAILS  where LOCK_CODE ='" & txtCode.Value & "'")
        'If clsCommon.myLen(discCode) > 0 Then
        '    common.clsCommon.MyMessageBoxShow("This record can't be deleted.It is used in another process")
        '    Exit Sub
        'End If
        '' Code Ends 
        funDelete()
    End Sub

    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsLockMPPaymentCycle.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub frmLockMPCollectionPC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        clsDBFuncationality.ExecuteNonQuery("update TSPL_LOCK_MP_PC set  LOCK_CODE=REPLACE(LOCK_CODE,'NCC0004',MCC_Code) where LOCK_CODE<>REPLACE(LOCK_CODE,'NCC0004',MCC_Code)")
        SetUserMgmtNew()
        isNewEntry = True

        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")
       
        is_Load = False
        funReset()
        AllowDateChanged = True
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmLockMPCollectionPC)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If
        btnSave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 02/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnSave.Visible = True Then
            MenuItemImport.Enabled = True
            MenuItemExport.Enabled = True
        Else
            MenuItemImport.Enabled = False
            MenuItemExport.Enabled = False
        End If
        '--------------------------------------------------
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        funReset()
    End Sub

    Sub funReset()
        isLoad = True
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        UsLock1.Status = ERPTransactionStatus.Pending
        fndLoc.Value = Nothing
        txtDescription.Text = ""
        btnSave.Text = "Save"
        dtpFromDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        btnSave.Enabled = True
        btnDelete.Enabled = True
        btnPost.Enabled = True
        txtLocName.Text = ""
        'txtMonth.Value = clsCommon.GETSERVERDATE()
        isLoad = False

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        funClose()

    End Sub

    Sub funClose()
        Me.Close()
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating

        Dim str As String = "select count(*) from TSPL_LOCK_MP_PC where LOCK_CODE ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then


            'Dim qry As String = "select LOCK_CODE as Code , PAY_PERIOD_NAME as Name, FROM_DATE as 'From Date', TO_DATE AS 'To Date', DESCRIPTION as Description  from TSPL_LOCK_MP_PC"
            'txtCode.Value = clsCommon.ShowSelectForm("PAYPERIOD_Master", qry, "Code", "", txtCode.Value, "LOCK_CODE", isButtonClicked)
            txtCode.Value = clsLockMPPaymentCycle.getFinder("", txtCode.Value, isButtonClicked)
            If txtCode.Value <> "" Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                funReset()
            End If
        End If


    End Sub

    Private Sub txtCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub frmLockMPCollectionPC_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SavingData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "SIRC"
            frm.strCode = "SIReversAndCreate"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnDeleteVSPBill.Visible = True
            End If
        End If
    End Sub

    Private Sub MenuItemClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemClose.Click
        funClose()

    End Sub

    Private Sub fndLoc__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLoc._MYValidating
        Dim qry As String = ""
        Dim arrLoc As String = ""
        Dim obj As New clsMCCCodes()
        obj = clsMCCCodes.GetData(True)
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
            arrLoc = obj.arrLocCodes
        End If

        qry = "select * from ( select Mcc_Code as [Code],MCC_Name as [Name] from tspl_mcc_master inner join tspl_location_master on location_Code= mcc_Code " _
        & " and (loc_segment_Code in (" & arrLoc & ") or mcc_Code in (" & arrLoc & ")))xx "

        fndLoc.Value = clsCommon.ShowSelectForm("VSPPMCCFn", qry, "Code", "", fndLoc.Value, "", isButtonClicked)
        txtLocName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MCC_Name from tspl_mcc_master where mcc_Code='" + fndLoc.Value + "'"))
        '=========================Added by preeti gupta=========================
        SetToDate()
    End Sub

    Public Function GetpaymentCycle(ByVal Mcc As String)
        Dim sQuery As String = "select case when Pc_Type='Day' then PC_VALUE when PC_Type='Month' then PC_Value * " & Date.DaysInMonth(dtpFromDate.Value.Year, dtpFromDate.Value.Month) & " end " _
               & " as Pc_Value from tspl_Mcc_master inner join TSPL_PAYMENT_CYCLE_MASTER  on tspl_Mcc_master.payment_cycle=TSPL_PAYMENT_CYCLE_MASTER.PC_CODE where Mcc_code='" & Mcc & "'"
        Dim py_code As String = clsDBFuncationality.getSingleValue(sQuery)
        Return py_code
    End Function

    Sub SetToDate()
        
        If Not isLoad Then
            Dim PaymentCycleType As String = ""
            Dim PaymentCycleValue As Integer = 0
            ' If Not isLoad Then
            If clsCommon.myLen(fndLoc.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select the Location first", Me.Text)
                Exit Sub
            End If
            Dim sQuery As String = "select Pc_Type as Type,PC_VALUE as Value, case when Pc_Type='Day' then PC_VALUE when PC_Type='Month' then PC_Value * " & Date.DaysInMonth(dtpFromDate.Value.Year, dtpFromDate.Value.Month) & " end " _
              & " as Pc_Value from tspl_Mcc_master inner join TSPL_PAYMENT_CYCLE_MASTER  on tspl_Mcc_master.payment_cycle=TSPL_PAYMENT_CYCLE_MASTER.PC_CODE where Mcc_code='" & fndLoc.Value & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(sQuery)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Please set payment cycle in Mcc master")
            End If
            PaymentCycleType = clsCommon.myCstr(dt.Rows(0)("Type"))
            PaymentCycleValue = clsCommon.myCdbl(dt.Rows(0)("Value"))
            If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Type")), "Week") = CompairStringResult.Equal Then
                dtpFromDate.MinDate = New Date(2000, 1, 1)
                dtpFromDate.MaxDate = New Date(3000, 1, 1).AddDays(-1)
                Dim today As Date = dtpFromDate.Value
                Dim dayDiff As Integer = today.DayOfWeek - IIf(PaymentCycleValue = 1, DayOfWeek.Sunday, IIf(PaymentCycleValue = 2, DayOfWeek.Monday, IIf(PaymentCycleValue = 3, DayOfWeek.Tuesday, IIf(PaymentCycleValue = 4, DayOfWeek.Wednesday, IIf(PaymentCycleValue = 5, DayOfWeek.Thursday, IIf(PaymentCycleValue = 6, DayOfWeek.Friday, DayOfWeek.Saturday))))))
                dtpFromDate.Value = today.AddDays(-dayDiff)
                dtpToDate.Value = dtpFromDate.Value.AddDays(6)
            Else
                Dim innPaymentCycleValue As Integer = dt.Rows(0)("Pc_Value")
                If dtpFromDate.Value.Day Mod innPaymentCycleValue <> 1 And (Not innPaymentCycleValue = 1) Then
                    AllowDateChanged = False
                    clsCommon.MyMessageBoxShow(Me, "Invalid date.Date should be multiple of " & clsCommon.myCstr(innPaymentCycleValue) & " + 1 ")
                    dtpFromDate.Value = dtpFromDate.MinDate
                    dtpFromDate.Text = dtpFromDate.MinDate
                    AllowDateChanged = True
                End If
                dtpToDate.Value = dtpFromDate.Value.AddDays(innPaymentCycleValue - 1)
                If dtpFromDate.Value.Month <> dtpToDate.Value.Month Then
                    dtpToDate.Value = New Date(dtpFromDate.Value.Year, dtpFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                End If
                Dim dtNxtPay As DateTime = dtpToDate.Value.AddDays(Math.Ceiling(innPaymentCycleValue / 2.0))
                If dtpFromDate.Value.Month <> dtNxtPay.Month Then
                    dtpToDate.Value = New Date(dtpFromDate.Value.Year, dtpFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                End If
            End If


            'Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select TSPL_MCC_MASTER.Payment_Cycle,TSPL_PAYMENT_CYCLE_MASTER.PC_TYPE,TSPL_PAYMENT_CYCLE_MASTER.PC_VALUE  from TSPL_MCC_MASTER left outer join TSPL_PAYMENT_CYCLE_MASTER on TSPL_PAYMENT_CYCLE_MASTER.PC_CODE=TSPL_MCC_MASTER.Payment_Cycle   where TSPL_MCC_MASTER.MCC_Code =(select Location_Code  from TSPL_LOCATION_MASTER where location_code='" & fndLoc.Value & "' and Location_Category='MCC' and Rejected_Type='N') ")
            'If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            '    clsCommon.MyMessageBoxShow("No Payment Cycle found on current MCC/Location")
            '    Exit Sub
            'End If
            'PaymentCycleType = clsCommon.myCstr(dt.Rows(0)("PC_TYPE"))
            'PaymentCycleValue = clsCommon.myCdbl(dt.Rows(0)("PC_VALUE"))
            'If clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal Then

            '    If clsCommon.myCdbl(clsCommon.GetPrintDate(dtpFromDate.Value, "dd")) Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
            '        clsCommon.MyMessageBoxShow("Date can only be first day of month or at interval of " & PaymentCycleValue & " Day, Because MCC has payment Cycle of " & PaymentCycleValue & " Day ")
            '        dtpFromDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
            '        dtpToDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
            '        Exit Sub
            '    End If
            '    dtpToDate.Value = DateAdd(DateInterval.Day, PaymentCycleValue - 1, dtpFromDate.Value)
            '    If DatePart(DateInterval.Month, dtpFromDate.Value) <> DatePart(DateInterval.Month, dtpToDate.Value) Then
            '        dtpToDate.Value = DateAdd(DateInterval.Month, 1, clsCommon.myCDate("01/" & DatePart(DateInterval.Month, dtpFromDate.Value) & "/" & DatePart(DateInterval.Year, dtpFromDate.Value)))
            '        dtpToDate.Value = DateAdd(DateInterval.Day, -1, dtpToDate.Value)

            '    End If
            '    If clsCommon.myCdbl(clsCommon.GetPrintDate(dtpToDate.Value, "dd")) = 30 Then
            '        dtpToDate.Value = DateAdd(DateInterval.Month, 1, clsCommon.myCDate("01/" & DatePart(DateInterval.Month, dtpFromDate.Value) & "/" & DatePart(DateInterval.Year, dtpFromDate.Value)))
            '        dtpToDate.Value = DateAdd(DateInterval.Day, -1, dtpToDate.Value)

            '    End If
            'ElseIf clsCommon.CompairString(PaymentCycleType, "Month") = CompairStringResult.Equal Then
            '    If clsCommon.myCdbl(clsCommon.GetPrintDate(dtpFromDate.Value, "dd")) <> 1 Then
            '        clsCommon.MyMessageBoxShow("Date can only be first day of month, Because MCC has payment Cycle of Month Type")
            '        dtpFromDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
            '        dtpToDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
            '        Exit Sub
            '    End If
            '    dtpToDate.Value = DateAdd(DateInterval.Month, PaymentCycleValue, dtpFromDate.Value)
            'ElseIf clsCommon.CompairString(PaymentCycleType, "Year") = CompairStringResult.Equal Then
            '    If clsCommon.myCdbl(clsCommon.GetPrintDate(dtpFromDate.Value, "dd")) <> 1 Then
            '        clsCommon.MyMessageBoxShow("Date can only be first day of month, Because MCC has payment Cycle of Year Type")
            '        dtpFromDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
            '        dtpToDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
            '        Exit Sub
            '    End If
            '    dtpToDate.Value = DateAdd(DateInterval.Year, PaymentCycleValue, dtpFromDate.Value)
            'ElseIf clsCommon.CompairString(PaymentCycleType, "Week") = CompairStringResult.Equal Then ''ERO/20/06/19-000650 by balwinder on 01/07/2019
            '    dtpFromDate.MinDate = New Date(2000, 1, 1)
            '    dtpFromDate.MaxDate = New Date(3000, 1, 1).AddDays(-1)
            '    Dim today As Date = dtpFromDate.Value
            '    Dim dayDiff As Integer = today.DayOfWeek - IIf(PaymentCycleValue = 1, DayOfWeek.Sunday, IIf(PaymentCycleValue = 2, DayOfWeek.Monday, IIf(PaymentCycleValue = 3, DayOfWeek.Tuesday, IIf(PaymentCycleValue = 4, DayOfWeek.Wednesday, IIf(PaymentCycleValue = 5, DayOfWeek.Thursday, IIf(PaymentCycleValue = 6, DayOfWeek.Friday, DayOfWeek.Saturday))))))
            '    dtpFromDate.Value = today.AddDays(-dayDiff)
            '    dtpToDate.Value = dtpFromDate.Value.AddDays(6)
            'End If
        End If
    End Sub

    Private Sub dtpFromDate_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles dtpFromDate.Validating
        SetToDate()
    End Sub

    Public Function getdata() As DataTable
        Dim qry As String = "  select Trans_Type as [Trans Type],document_code as [Doc Code],convert(varchar,Document_date,103) as [Doc Date],Bill_To_Location as [Loc Code]  ,TSPL_LOCATION_MASTER.Location_Desc as [Loc Name] from (select 'MCC Sale Farmer' as Trans_Type,document_code,Document_date,Bill_To_Location  from TSPL_MCC_Sale_Farmer_Head " & _
                            " where TSPL_MCC_Sale_Farmer_Head.Bill_To_Location='" + fndLoc.Value + "' and convert(date,TSPL_MCC_Sale_Farmer_Head.Document_Date ,103) >= convert(date,'" & dtpFromDate.Value & "',103) and convert(date,TSPL_MCC_Sale_Farmer_Head.Document_Date,103) <= convert(date,'" & dtpToDate.Value & "',103) and  TSPL_MCC_Sale_Farmer_Head.Status =0" & _
                            "  union all" & _
                            " select'MCC Sale Farmer Return' as Trans_Type,document_code,Document_date,Bill_To_Location  from TSPL_MCC_SALE_RETURN_HEAD_FARMER" & _
                            "  where TSPL_MCC_SALE_RETURN_HEAD_FARMER.Bill_To_Location='" + fndLoc.Value + "' and convert(date,TSPL_MCC_SALE_RETURN_HEAD_FARMER.Document_Date ,103) >= convert(date,'" & dtpFromDate.Value & "',103) and convert(date,TSPL_MCC_SALE_RETURN_HEAD_FARMER.Document_Date,103) <= convert(date,'" & dtpToDate.Value & "',103) and  TSPL_MCC_SALE_RETURN_HEAD_FARMER.Status =0" & _
                            "  union all " & _
                            " select 'MCC farmer Adjustment' as Trans_Type,Adjustment_No ,Adjustment_Date,MCC_Code   from TSPL_MP_Pay_Adj_Head " & _
                            "  where TSPL_MP_Pay_Adj_Head.MCC_Code ='" + fndLoc.Value + "' and convert(date,TSPL_MP_Pay_Adj_Head.Adjustment_Date  ,103) >= convert(date,'" & dtpFromDate.Value & "',103) and convert(date,TSPL_MP_Pay_Adj_Head.Adjustment_Date,103) <= convert(date,'" & dtpToDate.Value & "',103) and isnull(TSPL_MP_Pay_Adj_Head.Is_Post,'') ='') as final" & _
                            " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code =final.Bill_To_Location "

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Return dt
    End Function

    Public Sub DrillDown_ShowDialog(ByVal strScreenCode As String, ByVal strDocumentCode As String)
        If clsCommon.myLen(strScreenCode) > 0 AndAlso clsCommon.myLen(strDocumentCode) > 0 Then
            Select Case strScreenCode
                Case "MCC Sale Farmer"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCMaterialFarmer, strDocumentCode)
                Case "MCC Sale Farmer Return"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCMaterialSaleReturnFarmer, strDocumentCode)
                Case "MCC farmer Adjustment"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmFarmerPaymentAdjustment, strDocumentCode)

            End Select
        End If
    End Sub

    Private Sub gv_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv.CellDoubleClick
        DrillDown_ShowDialog(clsCommon.myCstr(gv.CurrentRow.Cells("Trans Type").Value), clsCommon.myCstr(gv.CurrentRow.Cells("Doc Code").Value))
    End Sub

   
    Private Sub btnDeleteVSPBill_Click(sender As Object, e As EventArgs) Handles btnDeleteVSPBill.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                txtCode.Focus()
                Throw New Exception("Document No not found to Unpost")
            End If
            If clsCommon.MyMessageBoxShow(Me, "Reverse and unpost current Document " + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, Telerik.WinControls.RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                clsLockMPPaymentCycle.ReverseAndUnpostData(txtCode.Value)
                clsCommon.MyMessageBoxShow(Me, "Reverse and unposted successfully", Me.Text)
                LoadData(txtCode.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
