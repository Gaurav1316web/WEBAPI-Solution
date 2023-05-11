Imports common
Imports System.IO
Imports Telerik.WinControls.UI
Imports XpertERPEngine
Imports Telerik.WinControls
Imports Telerik.WinControls.Enumerations
Imports System.Data.SqlClient
'Create By Sanjay

Public Class frmOutputEntry
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim i As Integer

    Private IsFormLoad As Boolean = False
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()


#End Region

    Public Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FAAcquisitionEntry)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        If MyBase.isReverse Then
            btnReverse.Enabled = True
        Else
            btnReverse.Enabled = False
        End If
    End Sub

    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")

        RadPageView1.SelectedPage = RadPageViewPage1

        LoadOutputType()
        setFromAndToDate()
        IsFormLoad = True
        AddNew()
        IsFormLoad = False
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub


    Sub BlankAllControls()

        txtDocNo.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        fndLoc.Value = ""
        txtLocName.Text = ""
        txtMCC.Value = ""
        txtMccName.Text = ""
        cboOutPutType.SelectedValue = "Select"
        txtFatPer.Value = 0
        txtSNFPer.Value = 0
        txtFatKG.Value = 0
        txtSnfKG.Value = 0
        txtQtyKG.Value = 0
        txtQtyLTR.Value = 0
        UsLock1.Status = ERPTransactionStatus.Pending

    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()

    End Sub

    Sub AddNew()

        BlankAllControls()


        isNewEntry = True
        btnSave.Text = "Save"

        btnSave.Enabled = True
        btnPost.Enabled = True

        btnDelete.Enabled = True
        txtDate.Focus()

        RadPageView1.SelectedPage = RadPageViewPage1

    End Sub

    Private Function AllowToSave() As Boolean
        If (clsCommon.myLen(fndLoc.Value)) <= 0 Then
            clsCommon.MyMessageBoxShow("Select Plant first.")
            fndLoc.Focus()
            Return False
        End If
        If (clsCommon.myLen(txtMCC.Value)) <= 0 Then
            clsCommon.MyMessageBoxShow("Select MCC/Plant first.")
            txtMCC.Focus()
            Return False
        End If
        If clsCommon.CompairString(cboOutPutType.SelectedValue, "Select") = CompairStringResult.Equal Then
            clsCommon.MyMessageBoxShow("Select Output Type first.")
            cboOutPutType.Focus()
            Return False
        End If

        If (clsCommon.myCdbl(txtFatKG.Value)) <= 0 Then
            clsCommon.MyMessageBoxShow("Enter Fat Kg")
            txtFatKG.Focus()
            Return False
        End If
        If (clsCommon.myCdbl(txtSnfKG.Value)) <= 0 Then
            clsCommon.MyMessageBoxShow("Select Snf Kg")
            txtSnfKG.Focus()
            Return False
        End If
        If (clsCommon.myCdbl(txtQtyKG.Value)) <= 0 Then
            clsCommon.MyMessageBoxShow("Enter Quantity Kg")
            txtQtyKG.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SavingData(False)
    End Sub

    Sub SavingData(ByVal ChekBtnPost As Boolean)
        If (SaveData(False, ChekBtnPost)) Then
            If ChekBtnPost = False Then
                common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
            End If
        End If
    End Sub

    Private Function SaveData(ByVal isDoAbandomentNo As Boolean, ByVal ChekBtnPost As Boolean) As Boolean
        Try
            If (AllowToSave()) Then
                Dim obj As New clsOutputEntry()

                obj.Doc_Code = txtDocNo.Value
                obj.Doc_Date = txtDate.Value
                obj.Plant_Code = clsCommon.myCstr(fndLoc.Value)
                obj.Mcc_Code = clsCommon.myCstr(txtMCC.Value)
                obj.FromDate = txtFromDate.Value
                obj.ToDate = txtToDate.Value
                obj.Output_Type = clsCommon.myCstr(cboOutPutType.SelectedValue)
                obj.FatPer = clsCommon.myCdbl(txtFatPer.Value)
                obj.SNFPer = clsCommon.myCdbl(txtSNFPer.Value)
                obj.FatKG = clsCommon.myCdbl(txtFatKG.Value)
                obj.SNFKG = clsCommon.myCdbl(txtSnfKG.Value)
                obj.QtyKG = clsCommon.myCdbl(txtQtyKG.Value)
                obj.QtyLTR = clsCommon.myCdbl(txtQtyLTR.Value)

                Dim isSaved As Boolean = obj.SaveData(obj, isNewEntry)

                LoadData(obj.Doc_Code, NavigatorType.Current)
                Return isSaved
            Else
                Return False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return False
    End Function

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            btnSave.Enabled = True
            btnPost.Enabled = True
            btnDelete.Enabled = True
            isInsideLoadData = True

            BlankAllControls()

            Dim obj As New clsOutputEntry()
            obj = clsOutputEntry.GetData(strCode, NavTyep)


            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Doc_Code) > 0) Then

                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                End If
                isNewEntry = False
                btnSave.Text = "Update"
                UsLock1.Status = obj.Status

                txtDocNo.Value = obj.Doc_Code
                txtDate.Value = obj.Doc_Date
                fndLoc.Value = clsCommon.myCstr(obj.Plant_Code)
                txtLocName.Text = clsLocation.GetName(fndLoc.Value, Nothing)
                txtMCC.Value = clsCommon.myCstr(obj.Mcc_Code)
                txtMccName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MCC_Name from tspl_mcc_master where mcc_Code='" + txtMCC.Value + "'"))
                If clsCommon.myLen(txtMccName.Text) <= 0 Then
                    txtMccName.Text = clsLocation.GetName(txtMCC.Value, Nothing)
                End If
                txtFromDate.Value = obj.FromDate
                txtToDate.Value = obj.ToDate
                cboOutPutType.SelectedValue = clsCommon.myCstr(obj.Output_Type)
                txtFatPer.Value = clsCommon.myCdbl(obj.FatPer)
                txtSNFPer.Value = clsCommon.myCdbl(obj.SNFPer)
                txtFatKG.Value = clsCommon.myCdbl(obj.FatKG)
                txtSnfKG.Value = clsCommon.myCdbl(obj.SNFKG)
                txtQtyKG.Value = clsCommon.myCdbl(obj.QtyKG)
                txtQtyLTR.Value = clsCommon.myCdbl(obj.QtyLTR)

            End If


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub

    Sub CloseForm()
        Me.Close()
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                SavingData(True)
                If (clsOutputEntry.PostData(txtDocNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Successfully Posted")
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        Try
            'Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                'If clsCancelLog.CheckForReasonOnDelete() Then
                '    '' REASON FOR DELETE 
                '    Dim frm As New FrmFreeTxtBox1
                '    frm.Text = "Remarks for Delete"
                '    frm.ShowDialog()
                '    If clsCommon.myLen(frm.strRmks) <= 0 Then
                '        Exit Sub
                '    Else
                '        Reason = frm.strRmks
                '    End If
                'End If
                If (clsOutputEntry.DeleteData(txtDocNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    AddNew()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub


    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_OUTPUT_ENTRY where Doc_Code='" + txtDocNo.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If
            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = "select Doc_Code as Code,convert (varchar(10), Doc_Date,103) as Date,TSPL_OUTPUT_ENTRY.Plant_Code as [Plant],isnull(tspl_Plant_Code.Location_Desc ,'') as [Plant Name],case when Status='0' then 'Pending' else 'Approved' end as [Status]  from TSPL_OUTPUT_ENTRY left outer join TSPL_LOCATION_MASTER as  tspl_Plant_Code on tspl_Plant_Code.Location_Code  = TSPL_OUTPUT_ENTRY.plant_code"
        LoadData(clsCommon.ShowSelectForm("OURPUFndd1", qry, "Code", "", txtDocNo.Value, "TSPL_OUTPUT_ENTRY.Doc_Date desc", isButtonClicked), NavigatorType.Current)
    End Sub

    Private Sub FrmAPInvoiceEntry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            SavingData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
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



    Private Sub btnReverse_Click(sender As Object, e As EventArgs) Handles btnReverse.Click
        Dim trans As SqlTransaction = Nothing
        Try
            If common.clsCommon.MyMessageBoxShow("Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                '' REASON FOR Reverse 
                'Dim Reason As String = ""
                'Dim frm As New FrmFreeTxtBox1
                'frm.Text = "Remarks for Reverse"
                'frm.ShowDialog()
                'If clsCommon.myLen(frm.strRmks) <= 0 Then
                '    Exit Sub
                'Else
                '    Reason = frm.strRmks
                'End If
                'trans = clsDBFuncationality.GetTransactin()
                If clsOutputEntry.ReverseAndUnpost(txtDocNo.Value) Then
                    'trans.Commit()
                    common.clsCommon.MyMessageBoxShow("Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            'trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub FndLoc__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLoc._MYValidating
        Dim whrCls As String = "TSPL_LOCATION_MASTER.Type = 'PLANT'"

        fndLoc.Value = clsLocation.getFinder(whrCls, fndLoc.Value, isButtonClicked)
        txtLocName.Text = clsLocation.GetName(fndLoc.Value, Nothing)
    End Sub

    Private Sub TxtMCC__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtMCC._MYValidating
        If clsCommon.myLen(fndLoc.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please select the Plant first")
            fndLoc.Focus()
            Exit Sub
        End If
        Dim qry As String = ""
        ''Dim StrWhere As String = " Plant_Code='" & fndLoc.Value & "'"
        qry = " select Code,Name from (select Mcc_Code as [Code],MCC_Name as [Name] from tspl_mcc_master where Plant_Code='" & fndLoc.Value & "'
         union all select TSPL_LOCATION_MASTER.Location_Code as [Code],TSPL_LOCATION_MASTER.Location_Desc as [Name] from TSPL_LOCATION_MASTER where TSPL_LOCATION_MASTER.Location_Code = '" & fndLoc.Value & "')xx"

        txtMCC.Value = clsCommon.ShowSelectForm("OUTMCCFn1", qry, "Code", "", txtMCC.Value, "", isButtonClicked)
        qry = "select MCC_Name from tspl_mcc_master where mcc_Code='" + txtMCC.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            txtMccName.Text = clsCommon.myCstr(dt.Rows(0)("MCC_Name"))
        Else
            If clsCommon.myLen(clsLocation.GetName(txtMCC.Value, Nothing)) > 0 Then
                txtMccName.Text = clsLocation.GetName(txtMCC.Value, Nothing)
            Else
                txtMccName.Text = ""
            End If
        End If
    End Sub

    Private Sub TxtFromDate_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtFromDate.Validating
        SetToDate()
    End Sub

    Sub setFromAndToDate()

        txtToDate.Enabled = False
        Dim dtCurr As DateTime = clsCommon.GETSERVERDATE()
        txtFromDate.Value = New Date(dtCurr.Year, dtCurr.Month, 1) 'New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1)
        SetToDate()
    End Sub

    Sub SetToDate()

        Dim PaymentCycleType As String = ""
        Dim PaymentCycleValue As Integer = 0

        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select TOP 1 TSPL_MCC_MASTER.Payment_Cycle,TSPL_PAYMENT_CYCLE_MASTER.PC_TYPE,TSPL_PAYMENT_CYCLE_MASTER.PC_VALUE  from TSPL_MCC_MASTER left outer join TSPL_PAYMENT_CYCLE_MASTER on TSPL_PAYMENT_CYCLE_MASTER.PC_CODE=TSPL_MCC_MASTER.Payment_Cycle   order by TSPL_PAYMENT_CYCLE_MASTER.PC_VALUE  desc ") ' where TSPL_MCC_MASTER.MCC_Code ='" & txtMCC.Value & "'")
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow("No Payment Cycle found")
            Exit Sub
        End If
        PaymentCycleType = clsCommon.myCstr(dt.Rows(0)("PC_TYPE"))
        PaymentCycleValue = clsCommon.myCdbl(dt.Rows(0)("PC_VALUE"))
        Dim dtCurr As DateTime = clsCommon.GETSERVERDATE()
        If clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal Then
            If txtFromDate.Value.Day Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                clsCommon.MyMessageBoxShow("Date can only be first day of month or at interval of " & PaymentCycleValue & " Day, Because MCC has payment Cycle of " & PaymentCycleValue & " Day ")
                txtFromDate.Value = New Date(dtCurr.Year, dtCurr.Month, 1)
                'txtToDate.Value = txtFromDate.Value
                txtToDate.Value = txtFromDate.Value.AddDays(PaymentCycleValue - 1)
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
                clsCommon.MyMessageBoxShow("Date can only be first day of month, Because MCC has payment Cycle of Month Type")
                txtFromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                txtToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                Exit Sub
            End If
            txtToDate.Value = DateAdd(DateInterval.Month, PaymentCycleValue, txtFromDate.Value)
        ElseIf clsCommon.CompairString(PaymentCycleType, "Year") = CompairStringResult.Equal Then
            If clsCommon.myCdbl(clsCommon.GetPrintDate(txtFromDate.Value, "dd")) <> 1 Then
                clsCommon.MyMessageBoxShow("Date can only be first day of month, Because MCC has payment Cycle of Year Type")
                txtFromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                txtToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                Exit Sub
            End If
            txtToDate.Value = DateAdd(DateInterval.Year, PaymentCycleValue, txtFromDate.Value)
        End If

    End Sub

    Sub LoadOutputType()
        'isInsideLoadData = True
        Dim dt As DataTable = clsDB.GetOPType()
        Dim dr As DataRow = dt.NewRow
        dr("Code") = "Select"
        dt.Rows.InsertAt(dr, 0)

        cboOutPutType.DataSource = dt
        cboOutPutType.ValueMember = "Code"
        cboOutPutType.DisplayMember = "Code"

        'isInsideLoadData = False
    End Sub

    Private Sub txtFatKG_TextChanged(sender As Object, e As EventArgs) Handles txtFatKG.TextChanged
        Try
            If isInsideLoadData = False Then
                If clsCommon.myCdbl(txtQtyKG.Value) > 0 Then
                    If clsCommon.myCdbl(txtFatKG.Value) > 0 Then
                        txtFatPer.Value = System.Math.Round(((txtFatKG.Value * 100) / txtQtyKG.Value), 2)
                    Else
                        txtFatPer.Value = 0
                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtSnfKG_TextChanged(sender As Object, e As EventArgs) Handles txtSnfKG.TextChanged
        Try
            If isInsideLoadData = False Then
                If clsCommon.myCdbl(txtQtyKG.Value) > 0 Then
                    If clsCommon.myCdbl(txtSnfKG.Value) > 0 Then
                        txtSNFPer.Value = System.Math.Round(((txtSnfKG.Value * 100) / txtQtyKG.Value), 2)
                    Else
                        txtSNFPer.Value = 0
                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtQtyKG_TextChanged(sender As Object, e As EventArgs) Handles txtQtyKG.TextChanged
        Try
            If isInsideLoadData = False Then
                If clsCommon.myCdbl(txtQtyKG.Value) > 0 Then
                    If clsCommon.myCdbl(txtFatKG.Value) > 0 Then
                        txtFatPer.Value = System.Math.Round(((txtFatKG.Value * 100) / txtQtyKG.Value), 2)
                    End If
                    If clsCommon.myCdbl(txtSnfKG.Value) > 0 Then
                        txtSNFPer.Value = System.Math.Round(((txtSnfKG.Value * 100) / txtQtyKG.Value), 2)
                    End If
                Else
                    txtFatPer.Value = 0
                    txtSNFPer.Value = 0
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
End Class
