'--preeti gupta-ticket no.[BM00000003133]
Imports common

Public Class frmClaimMaster
    Inherits FrmMainTranScreen

    Const colLineNo As String = "LineNo"
    Const colClaimCode As String = "ClaimCode"
    Const colReceivingDate As String = "ReceivingDate"
    Const colClaimDate As String = "ClaimDate"
    Const colTargetCode As String = "TARGETCODE"
    Const colTargetIsOPening As String = "TargetIsOPening"
    Const colClaimAmount As String = "ClaimAmount"
    Const colApprovedAmount As String = "ApprovedAmount"
    Const colSelect As String = "Select"
    Const colApprovedDate As String = "ApprovedDate"

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = True
    'Private Obj As clsmo
    Dim APPROVE As Boolean = False
    Dim obj As New clsClaimDetails
    Private ObjList As New List(Of clsClaimDetails)
    Private isCellValueChangedOpen As Boolean = False

    Sub LoadGridColumns()
        gvDeduction.DataSource = Nothing
        gvDeduction.Rows.Clear()
        gvDeduction.Columns.Clear()

        Dim repoLineNo As New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvDeduction.MasterTemplate.Columns.Add(repoLineNo)

        Dim ClaimCode As New GridViewTextBoxColumn()
        ClaimCode.FormatString = ""
        ClaimCode.HeaderText = "Claim Code"
        ClaimCode.Name = colClaimCode
        ClaimCode.Width = 100
        ClaimCode.ReadOnly = True
        ClaimCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvDeduction.Columns.Add(ClaimCode)

        Dim ReceivingDate As New GridViewDateTimeColumn
        ReceivingDate.CustomFormat = "dd/MMM/yyyy"
        ReceivingDate.FormatString = "{0:d}"
        ReceivingDate.HeaderText = "Receiving Date"
        ReceivingDate.Name = colReceivingDate
        ReceivingDate.Width = 120
        ReceivingDate.ReadOnly = False
        ReceivingDate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvDeduction.Columns.Add(ReceivingDate)

        Dim ClaimDate As New GridViewDateTimeColumn
        ClaimDate.CustomFormat = "dd/MMM/yyyy"
        ClaimDate.FormatString = "{0:d}"
        ClaimDate.HeaderText = "Claim Date"
        ClaimDate.Name = colClaimDate
        ClaimDate.Width = 120
        ClaimDate.ReadOnly = False
        ClaimDate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvDeduction.Columns.Add(ClaimDate)

        Dim TargetCode As New GridViewTextBoxColumn
        TargetCode.FormatString = ""
        TargetCode.HeaderText = "Target Code"
        TargetCode.Name = colTargetCode
        TargetCode.Width = 100
        TargetCode.ReadOnly = False
        TargetCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvDeduction.Columns.Add(TargetCode)

        Dim TargetIsOpening As New GridViewCheckBoxColumn
        TargetIsOpening.FormatString = ""
        TargetIsOpening.HeaderText = "Is Opening"
        TargetIsOpening.Name = colTargetIsOPening
        TargetIsOpening.Width = 100
        TargetIsOpening.ReadOnly = True
        TargetIsOpening.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvDeduction.Columns.Add(TargetIsOpening)

        Dim ClaimeAmount As New GridViewDecimalColumn
        ClaimeAmount.FormatString = ""
        ClaimeAmount.HeaderText = "Claim Amount"
        ClaimeAmount.Name = colClaimAmount
        ClaimeAmount.Width = 100
        ClaimeAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvDeduction.Columns.Add(ClaimeAmount)

        Dim ApprovedAmount As New GridViewDecimalColumn
        ApprovedAmount.FormatString = ""
        ApprovedAmount.HeaderText = "Approved Amount"
        ApprovedAmount.Name = colApprovedAmount
        ApprovedAmount.Width = 100
        ApprovedAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvDeduction.Columns.Add(ApprovedAmount)

        Dim IsSelect As New GridViewCheckBoxColumn()
        IsSelect.HeaderText = "Status"
        IsSelect.Name = colSelect
        IsSelect.Width = 100
        IsSelect.ReadOnly = False
        IsSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvDeduction.Columns.Add(IsSelect)

        Dim repoRecoDate As New GridViewDateTimeColumn
        repoRecoDate.CustomFormat = "dd/MMM/yyyy"
        repoRecoDate.FormatString = "{0:d}"
        repoRecoDate.HeaderText = "Approved Date"
        repoRecoDate.Name = colApprovedDate
        repoRecoDate.Width = 120
        repoRecoDate.ReadOnly = False
        repoRecoDate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvDeduction.Columns.Add(repoRecoDate)

    End Sub

    Private Sub frmReimbursementDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub
    Sub funClose()
        Me.Close()
    End Sub

    Private Sub frmMonthlyAttendance_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadGridColumns()
        gvDeduction.Rows.AddNew()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")

    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmClaimMaster)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
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
        txtCustCode.Value = Nothing
        btnsave.Text = "Save"
        UsLock1.Status = ERPTransactionStatus.Pending
        btnsave.Enabled = True
        btndelete.Enabled = True
        LoadGridColumns()
        Me.gvDeduction.Rows.AddNew()
    End Sub

    Sub LoadData(ByVal strCode As String)
        funReset()
        txtCustCode.Value = strCode
        ObjList = clsClaimDetails.GetData(strCode, Nothing)
        If (ObjList IsNot Nothing AndAlso ObjList.Count > 0) Then
            isNewEntry = False
            btnsave.Text = "Update"
            Dim ii As Int16 = 0
            LoadGridColumns()
            For Each objtr As clsClaimDetails In ObjList
                txtCustCode.Value = objtr.Cust_Code
                ii += 1
                gvDeduction.Rows.AddNew()
                gvDeduction.Rows(gvDeduction.Rows.Count - 1).Cells(colLineNo).Value = ii
                gvDeduction.Rows(gvDeduction.Rows.Count - 1).Cells(colClaimCode).Value = objtr.Claim_Code
                If objtr.Receiving_Date.Year > 1900 Then
                    gvDeduction.Rows(gvDeduction.Rows.Count - 1).Cells(colReceivingDate).Value = objtr.Receiving_Date
                Else
                    gvDeduction.Rows(gvDeduction.Rows.Count - 1).Cells(colReceivingDate).Value = Nothing
                End If
                gvDeduction.Rows(gvDeduction.Rows.Count - 1).Cells(colClaimDate).Value = objtr.Claim_Date
                gvDeduction.Rows(gvDeduction.Rows.Count - 1).Cells(colTargetCode).Value = objtr.Target_Code
                gvDeduction.Rows(gvDeduction.Rows.Count - 1).Cells(colTargetIsOPening).Value = objtr.Isopening
                gvDeduction.Rows(gvDeduction.Rows.Count - 1).Cells(colClaimAmount).Value = objtr.Claim_Amount
                gvDeduction.Rows(gvDeduction.Rows.Count - 1).Cells(colSelect).Value = objtr.Status
                If clsCommon.myLen(objtr.Approved_Date) > 0 AndAlso clsCommon.myCBool(objtr.Status) Then
                    gvDeduction.Rows(gvDeduction.Rows.Count - 1).Cells(colApprovedDate).Value = objtr.Approved_Date
                    gvDeduction.Rows(gvDeduction.Rows.Count - 1).Cells(colApprovedAmount).Value = objtr.Approved_Amount
                Else
                    gvDeduction.Rows(gvDeduction.Rows.Count - 1).Cells(colApprovedDate).Value = Nothing
                    gvDeduction.Rows(gvDeduction.Rows.Count - 1).Cells(colApprovedAmount).Value = Nothing
                End If
            Next
            gvDeduction.Rows.AddNew()
        End If

    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SavingData(False)
    End Sub
    Public Function Save() As Boolean
        Try

            If AllowToSave() Then
                Dim obj As New clsClaimDetails

                ObjList = New List(Of clsClaimDetails)
                Dim objtr As clsClaimDetails
                For Each grow As GridViewRowInfo In gvDeduction.Rows
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colTargetCode).Value)) > 0 Then
                        objtr = New clsClaimDetails()
                        objtr.Claim_Code = clsCommon.myCstr(grow.Cells(colClaimCode).Value)
                        If clsCommon.myLen(grow.Cells(colReceivingDate).Value) > 0 Then
                            objtr.Receiving_Date = clsCommon.myCstr(grow.Cells(colReceivingDate).Value)
                        Else
                            objtr.Receiving_Date = Nothing
                        End If
                        objtr.Claim_Date = clsCommon.myCstr(grow.Cells(colClaimDate).Value)
                        objtr.Cust_Code = txtCustCode.Value
                        objtr.Target_Code = clsCommon.myCstr(grow.Cells(colTargetCode).Value)
                        objtr.Claim_Amount = clsCommon.myCdbl(grow.Cells(colClaimAmount).Value)
                        If clsCommon.myCBool(grow.Cells(colSelect).Value) Then
                            objtr.Approved_Amount = clsCommon.myCdbl(grow.Cells(colApprovedAmount).Value)
                            objtr.Approved_Date = clsCommon.myCDate(grow.Cells(colApprovedDate).Value)
                            objtr.Status = clsCommon.myCBool(grow.Cells(colSelect).Value)
                        Else
                            objtr.Approved_Amount = 0
                            objtr.Approved_Date = Nothing
                            objtr.Status = False
                        End If
                        ObjList.Add(objtr)
                    End If
                Next
                If (obj.SaveData(txtCustCode.Value, ObjList)) Then
                    LoadData(txtCustCode.Value)
                    'common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    Return True
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
        Return False
    End Function
    Function AllowToSave() As Boolean
        If clsCommon.myLen(txtCustCode.Value) <= 0 Then
            myMessages.blankValue("Customer Code")
            txtCustCode.Focus()
            Return False
        End If

        Dim ii As Int16 = 0
        For Each grow As GridViewRowInfo In gvDeduction.Rows
            If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colClaimDate).Value)) > 0 Or clsCommon.myLen(clsCommon.myCstr(grow.Cells(colTargetCode).Value)) > 0 Then
                ii += 1
                If (ii <> 1) AndAlso clsCommon.myCdbl(grow.Cells(colClaimAmount).Value) < 1 Then
                    clsCommon.MyMessageBoxShow("Enter Claim Amount at Row No : " + ii.ToString())
                    Return False
                End If
                If clsCommon.myLen(grow.Cells(colClaimDate).Value) < 1 Then
                    clsCommon.MyMessageBoxShow("Enter Claim Date at Row No : " + ii.ToString())
                    Return False
                End If
                If clsCommon.myLen(grow.Cells(colTargetCode).Value) < 1 Then
                    clsCommon.MyMessageBoxShow("Enter Target Code at Row No : " + ii.ToString())
                    Return False
                End If
                If (ii = 1) AndAlso (Not clsCommon.myCBool(grow.Cells(colTargetIsOPening).Value)) Then
                    clsCommon.MyMessageBoxShow("Please select a Opening Target Code in First row.")
                    Return False
                End If

                If clsCommon.myCBool(grow.Cells(colSelect).Value) Then
                    If clsCommon.myLen(grow.Cells(colApprovedDate).Value) < 1 Then
                        clsCommon.MyMessageBoxShow("Please Enter Approve Date in Row no :" + clsCommon.myCstr(ii))
                        Exit Function
                    End If
                    If clsCommon.myCdbl(grow.Cells(colApprovedAmount).Value) > clsCommon.myCdbl(grow.Cells(colClaimAmount).Value) Then
                        clsCommon.MyMessageBoxShow("Approve Amount can not be grater then Claim Amount in Row no :" + clsCommon.myCstr(ii))
                        Exit Function
                    End If
                End If
            End If
        Next

        Return True
    End Function

    Private Sub gvMonthlyAttendance_CellEndEdit(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvDeduction.CellEndEdit
        If Not isCellValueChangedOpen Then
            isCellValueChangedOpen = True
            Dim IsOpeningRow As Boolean = False
            If e.Column Is gvDeduction.Columns(colTargetCode) Then
                Dim qry As String = "select Code,Description,IsOpening from TSPL_Discount_Master"
                'gvDeduction.CurrentRow.Cells(colTargetCode).Value = clsCommon.ShowSelectForm("DiscontMasterFND", qry, "Code", "", gvDeduction.CurrentRow.Cells(colTargetCode).Value, "Code", False)
                gvDeduction.CurrentRow.Cells(colTargetCode).Value = clsCommon.ShowSelectForm("DiscontMasterFND", qry, "Code", "", gvDeduction.CurrentRow.Cells(colTargetCode).Value, "Code", False)
                IsOpeningRow = clsDiscountMaster.Check_IsOpening(gvDeduction.CurrentRow.Cells(colTargetCode).Value, Nothing)
                gvDeduction.CurrentRow.Cells(colTargetIsOPening).Value = IsOpeningRow

                If clsCommon.myLen(gvDeduction.CurrentRow.Cells(colTargetCode).Value) > 0 AndAlso (IsOpeningRow = False) AndAlso (gvDeduction.CurrentRow.Index = 0) Then
                    clsCommon.MyMessageBoxShow(" Please select a Opening Target Code in First row.")
                    gvDeduction.CurrentRow.Cells(colTargetCode).Value = ""
                    gvDeduction.CurrentRow.Cells(colTargetIsOPening).Value = False
                End If

                'gvDeduction.Rows.AddNew()
            End If
            If e.Column Is gvDeduction.Columns(colApprovedDate) Then

            End If
            isCellValueChangedOpen = False
        End If
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(txtCustCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("You Cannot Delete Record")
            Exit Sub
        End If
        funDelete()
    End Sub

    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsClaimDetails.DeleteData(txtCustCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            Dim IsChecked As Int16 = 0
            For Each dr As GridViewRowInfo In gvDeduction.Rows
                If clsCommon.myCBool(dr.Cells(colSelect).Value) Then
                    IsChecked += 1
                    If clsCommon.myLen(dr.Cells(colApprovedDate).Value) < 1 Then
                        clsCommon.MyMessageBoxShow("Please Enter Approve Date in Row no :" + clsCommon.myCstr(IsChecked))
                        Exit Sub
                    End If
                    If clsCommon.myCdbl(dr.Cells(colApprovedAmount).Value) > clsCommon.myCdbl(dr.Cells(colClaimAmount).Value) Then
                        clsCommon.MyMessageBoxShow("Approve Amount can not be grater then Claim Amount in Row no :" + clsCommon.myCstr(IsChecked))
                        Exit Sub
                    End If
                    APPROVE = True
                End If
            Next
            If IsChecked < 1 Then
                clsCommon.MyMessageBoxShow("Please Select row to approv.")
                Exit Sub
            End If
            Save()
            clsCommon.MyMessageBoxShow("Data Approved.")
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub SavingData(ByVal ChekBtnPost As Boolean)
        If (Save()) Then
            If ChekBtnPost = False Then
                common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
            End If
        End If
    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvDeduction.CurrentColumnChanged
        If gvDeduction.RowCount > 0 Then
            Dim intCurrRow As Integer = gvDeduction.CurrentRow.Index
            gvDeduction.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gvDeduction.Rows.Count - 1 Then
                gvDeduction.Rows.AddNew()
                gvDeduction.CurrentRow = gvDeduction.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gv1_UserAddedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gvDeduction.UserAddedRow
        For i As Integer = 0 To gvDeduction.Rows.Count - 1
            gvDeduction.Rows(0).Cells(0).Value = 1
            If i <> 0 Then
                gvDeduction.Rows(i).Cells(colLineNo).Value = i + 1
            End If
        Next
    End Sub

    Private Sub txtCustCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCustCode._MYNavigator
        Try
            Dim qry As String = "select Cust_Code from TSPL_CUSTOMER_MASTER where 2=2"
            Select Case NavType
                Case NavigatorType.First
                    qry += " and Cust_Code = (select MIN(Cust_Code) from TSPL_CUSTOMER_MASTER)"
                Case NavigatorType.Last
                    qry += " and Cust_Code = (select Max(Cust_Code) from TSPL_CUSTOMER_MASTER)"
                Case NavigatorType.Next
                    qry += " and Cust_Code = (select Min(Cust_Code) from TSPL_CUSTOMER_MASTER where  Cust_Code >'" + txtCustCode.Value + "')"
                Case NavigatorType.Previous
                    qry += " and Cust_Code = (select Max(Cust_Code) from TSPL_CUSTOMER_MASTER where Cust_Code <'" + txtCustCode.Value + "')"
                Case NavigatorType.Current
                    qry += " and Cust_Code = '" + txtCustCode.Value + "'"
            End Select
            Dim StrCode As String = clsDBFuncationality.getSingleValue(qry)
            If clsCommon.myLen(StrCode) > 0 Then
                txtCustCode.Value = StrCode
                LoadData(txtCustCode.Value)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtCustCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCustCode._MYValidating
        Dim str As String = "select count(*) from TSPL_CUSTOMER_MASTER where Cust_Code = '" + txtCustCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCustCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtCustCode.MyReadOnly = True
        End If
        If txtCustCode.MyReadOnly OrElse isButtonClicked Then

            Dim qry As String = "select Cust_Code as Code, Customer_Name as Description from TSPL_CUSTOMER_MASTER"
            txtCustCode.Value = clsCommon.ShowSelectForm("Code", qry, "Code", "", txtCustCode.Value, "Cust_Code", isButtonClicked)
            If txtCustCode.Value <> "" Then
                LoadData(txtCustCode.Value)
            Else
                funReset()
            End If
        End If

    End Sub

    Private Sub rdbtnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnreset.Click
        funReset()
    End Sub

    Private Sub gvDeduction_UserDeletedRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gvDeduction.UserDeletedRow
        For ii As Integer = 1 To gvDeduction.Rows.Count
            gvDeduction.Rows(ii - 1).Cells(colLineNo).Value = ii
        Next
    End Sub
End Class