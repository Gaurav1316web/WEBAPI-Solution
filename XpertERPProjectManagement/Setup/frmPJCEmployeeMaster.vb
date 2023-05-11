Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports common
'''' <summary>
'''' ''''''''''''''''''''''''TicketNo='BM00000001531''''''''''''''''''''''''''''''''''''''''
'''' </summary>
'''' <remarks></remarks>

Public Class frmPJCEmployeeMaster
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim userCode, companyCode As String

    Const colEmpCode As String = "EmpCode"
    Const colEmpName As String = "EmpName"
    Const colCustCode As String = "CustCode"
    Const colCustName As String = "CustName"
    Const colBillingRate As String = "BillingRate"
    Const colRemarks As String = "Remarks"
    Private isCellValueChangedOpen As Boolean = False

#Region "Functions"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmPJCEmployeeMaster)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As clsEmployeeMaster = clsEmployeeMaster.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            isNewEntry = False
            txtCode.Value = obj.EMP_CODE
            txtEmpName.Text = obj.Emp_Name
            Me.txtEarningCode.Text = obj.EARNING_CODE
            Me.fndUser.Value = obj.USER_CODE
            Me.lblUserName.Text = obj.USER_CODE
            Me.txtBillingRate.Text = obj.BILLING_RATE
            Me.txtUnitCost.Text = obj.UNIT_COST
            Me.txtEmailId.Text = obj.EMail_ID
            Me.RadTextBox1.Text = obj.COMMENTS
            Me.chkApplyToAllCust.Checked = obj.APPLY_ALL_CUST
            If obj.Emp_Status = "Active" Then
                Me.chkInactive.Checked = False
            Else
                Me.chkInactive.Checked = True
            End If
            txtCode.MyReadOnly = True
            ''For Custom Fields
            If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                UcCustomFields1.LoadData(obj.EMP_CODE)
            End If
            ''End of For Custom Fields

            '' fill customer billing rate detaisl
            Dim objList As List(Of clsEmpCustomerBillingRateDetails)
            objList = clsEmpCustomerBillingRateDetails.GetData(obj.EMP_CODE, Nothing)
            LoadGridColumns()

            For Each objRate As clsEmpCustomerBillingRateDetails In objList

                gv1.Rows(gv1.Rows.Count - 1).Cells(colEmpCode).Value = objRate.EMP_CODE
                gv1.Rows(gv1.Rows.Count - 1).Cells(colCustCode).Value = objRate.Cust_Code
                gv1.Rows(gv1.Rows.Count - 1).Cells(colBillingRate).Value = objRate.BILLING_RATE
                gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = objRate.COMMENTS
                gv1.Rows.AddNew()
            Next
        End If
    End Sub
    Sub SaveData()
        Dim trans As SqlTransaction = Nothing
        Try
            If AllowToSave() Then
                trans = clsDBFuncationality.GetTransactin()
                Dim obj As New clsEmployeeMaster()
                obj.EMP_CODE = txtCode.Value
                obj.Emp_Name = txtEmpName.Text
                obj.EARNING_CODE = txtEarningCode.Text
                obj.USER_CODE = clsCommon.myCstr(Me.fndUser.Value)
                obj.BILLING_RATE = clsCommon.myCdbl(Me.txtBillingRate.Text)
                obj.UNIT_COST = clsCommon.myCdbl(Me.txtUnitCost.Text)
                obj.EMail_ID = Me.txtEmailId.Text
                obj.COMMENTS = Me.RadTextBox1.Text
                obj.APPLY_ALL_CUST = Me.chkApplyToAllCust.Checked
                If Me.chkInactive.Checked = True Then
                    obj.Emp_Status = "Inactive"
                Else
                    obj.Emp_Status = "Active"
                End If
                obj.isPJCModule = True
                Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(EMP_CODE) from TSPL_EMPLOYEE_MASTER where EMP_CODE='" + obj.EMP_CODE + "'", trans)
                If (qry = 0) Then
                    isNewEntry = True
                Else
                    isNewEntry = False
                End If

                ''For Custom Fields
                obj.Form_ID = MyBase.Form_ID
                obj.arrCustomFields = New List(Of clsCustomFieldValues)
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.GetData(obj.arrCustomFields)
                End If
                ''End of For Custom Fields

                '' billing rate details
                If Me.chkApplyToAllCust.Checked = False Then
                    Dim objList As clsEmpCustomerBillingRateDetails
                    Dim objBRList As New List(Of clsEmpCustomerBillingRateDetails)
                    For intloop As Integer = 0 To gv1.Rows.Count - 1
                        objList = New clsEmpCustomerBillingRateDetails
                        If clsCommon.myLen(Me.gv1.Rows(intloop).Cells(colCustCode).Value) > 0 Then
                            objList.EMP_CODE = obj.EMP_CODE
                            objList.Cust_Code = Me.gv1.Rows(intloop).Cells(colCustCode).Value
                            objList.BILLING_RATE = Me.gv1.Rows(intloop).Cells(colBillingRate).Value
                            objList.COMMENTS = Me.gv1.Rows(intloop).Cells(colRemarks).Value
                            objBRList.Add(objList)
                        End If

                    Next
                    obj.ObjListBRDetails = objBRList
                End If

                If (obj.SaveData(obj, isNewEntry, trans)) Then
                    trans.Commit()
                    clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                    LoadData(obj.EMP_CODE, NavigatorType.Current)

                End If
            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(clsCommon.myCstr(txtCode.Value)) <= 0 Then
                txtCode.Focus()
                Throw New Exception("Please Fill  Code")
            End If

            If clsCommon.myLen(clsCommon.myCstr(txtEmpName.Text)) <= 0 Then
                txtEmpName.Focus()
                Throw New Exception("Please Fill  Employee Name")
            End If
            If clsCommon.myLen(clsCommon.myCstr(fndUser.Value)) <= 0 Then
                fndUser.Focus()
                Throw New Exception("Please map user with Employee !")
            Else
                Dim EmpCode As String
                EmpCode = clsEmployeeMaster.CheckMappedUser(fndUser.Value.ToString, Me.txtCode.Value.ToString)
                If clsCommon.myLen(EmpCode) > 0 Then
                    Throw New Exception("User '" & clsCommon.myCstr(fndUser.Value) & "' is already mapped to Employee Code'" & EmpCode & "' !")
                End If
            End If
            UcCustomFields1.AllowToSave()
            If Me.chkApplyToAllCust.Checked = False Then
                Dim count As Integer = 0
                For intloop As Integer = 0 To gv1.Rows.Count - 1
                    If clsCommon.myLen(gv1.Rows(intloop).Cells(colCustCode).Value) > 0 Then
                        count = count + 1
                    End If
                Next
                If count = 0 Then
                    Throw New Exception("Customer Billing Rate Detail is empty.")
                End If
            End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Function
    Private Sub DeleteData()
        Dim trans As SqlTransaction = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("  Code not found to delete")

            End If

            If clsCommon.MyMessageBoxShow("Do you want to delete  Code '" + txtCode.Value + "'", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then

                clsEmployeeMaster.DeleteData(Me.txtCode.Value, trans)
                clsEmpCustomerBillingRateDetails.DeleteData(txtCode.Value, trans)

                '' custom fields
                clsCustomFieldValues.DeleteData(Me.Form_ID, txtCode.Value, trans)
                trans.Commit()
                clsCommon.MyMessageBoxShow("Successfully Deleted", Me.Text)
                AddNew()
            End If
        Catch ex As Exception
            If (clsCommon.CompairString(clsCommon.myCstr(ex.Message), "Cost Code not found to delete") <> CompairStringResult.Equal) Then
                clsCommon.MyMessageBoxShow("Current Cost Code is in use")

            Else
                clsCommon.MyMessageBoxShow(ex.Message)
            End If
            trans.Rollback()
        End Try
    End Sub
    Sub AddNew()
        txtCode.Value = ""
        txtCode.MyReadOnly = False
        txtEmpName.Text = ""
        Me.fndUser.Value = Nothing
        Me.lblUserName.Text = ""
        Me.txtBillingRate.Text = 0
        Me.txtUnitCost.Text = 0
        Me.txtEarningCode.Text = ""
        Me.txtEmailId.Text = ""
        Me.txtEmpName.Text = ""
        Me.chkApplyToAllCust.Checked = False
        Me.chkInactive.Checked = False
        Me.RadTextBox1.Text = ""
        LoadGridColumns()
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
        End If
        ''End of For Custom Fields

    End Sub

#End Region
#Region "Events"

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click

        SaveData()
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        AddNew()
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        LoadData(txtCode.Value, NavType)
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Dim str As String = "select count(*) from TSPL_EMPLOYEE_MASTER where EMP_CODE ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then

            'Dim qry As String = "select EMP_CODE as Code,Emp_Name as Name from  TSPL_EMPLOYEE_MASTER"
            'txtCode.Value = clsCommon.ShowSelectForm("TSPL_EMPLOYEE_MASTER", qry, "Code", "", txtCode.Value, "", isButtonClicked)
            txtCode.Value = clsEmployeeMaster.getFinder("", txtCode.Value, isButtonClicked)
            LoadData(txtCode.Value, NavigatorType.Current)
        End If
    End Sub
    Private Sub frmPJCEmployeeMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
        ' globalFunc.mandatoryText(fnddesig.txtValue, txtdes)

        ''For Custom Fields
        RadPageView1.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.Report_ID = MyBase.Form_ID
            UcCustomFields1.LoadCustomControls()
        End If
        ''End of For Custom Fields
        LoadGridColumns()
        AddNew()

    End Sub

    Private Sub frmPJCEmployeeMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            '    PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            AddNew()
        End If
    End Sub
#End Region


    Private Sub fndUser__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndUser._MYValidating
        Dim qry As String = "select USER_CODE as [Code],USER_NAME as [Name] from TSPL_USER_MASTER "
        fndUser.Value = clsCommon.ShowSelectForm("TSPL_USER_MASTER", qry, "Code", "", fndUser.Value.ToString, "USER_CODE", isButtonClicked)
        lblUserName.Text = clsDBFuncationality.getSingleValue("select USER_NAME from TSPL_USER_MASTER where USER_CODE='" + fndUser.Value + "' ")
    End Sub


    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click

        Dim str As String
        str = " select EMP_CODE as 'Emp ID',[Emp_Name] as 'Employee Name',EARNING_CODE as 'Earning Code',UNIT_COST as 'Unit Cost',BILLING_RATE AS [Billing Rate],USER_CODE as 'User Code',EMail_ID as 'Email Id',COMMENTS as 'Comments' "
        str += " From TSPL_EMPLOYEE_MASTER "
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        Dim gv As New RadGridView()
        Dim Counter As Int16 = 0
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        'If transportSql.importExcel(gv, "Emp ID", "Employee Name", "Fathers Name", "Mothers Name", "Date of Birth", "Sex", "Marital Status", "Spouse Name", "Date of Joining", "Salary calculate from", "Date of leaving", "Reason for leaving") Then

        'If transportSql.importExcel(gv, "Emp ID", "Employee Name", "Fathers Name", "Mothers Name", "Date of Birth", "Sex", "Marital Status", "Spouse Name", "Designation", "Occupation", "Department", "Grade", "Branch", "Division", "Bank Account No", "Bank Name", "Sal Structure", "Attendance", "Res No", "Res Name", "Road/Street", "Locality/Area", "City/District", "State", "Pincode", "Res No", "Res Name", "Road/Street", "Locality/Area", "City/District", "State", "Pincode", "E - Mail ID", "STD Code", "Phone", "Mobile", "Date of Joining", "Salary calculate from", "Date of leaving", "Reason for leaving", "ESI Applicable", "ESI No", "ESI Dispensary", "PF Applicable", "PF No", "PF No for Dept File", "Restrict PF", "Zero Pension", "Zero PT", "PAN", "Ward/Circle", "Director") Then
        If transportSql.importExcel(gv, "Emp ID", "Employee Name", "Earning Code", "Unit Cost", "Billing Rate", "User Code", "Email Id", "Comments") Then


            Dim trans As SqlTransaction = Nothing
            Try
                'connectSql.OpenConnection()
                'trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    If clsCommon.myLen(grow.Cells("Emp ID").Value) > 0 AndAlso clsCommon.myLen(grow.Cells("Employee Name").Value) > 0 Then


                        Dim obj As New clsEmployeeMaster()
                        Counter += 1
                        Dim strCode As String = clsCommon.myCstr(grow.Cells("Emp ID").Value)
                        If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                            'Throw New Exception("Employee id can not be blank or incorrect.")
                            Continue For
                        End If
                        obj.EMP_CODE = strCode

                        '' employee name
                        Dim strName As String = ""
                        strName = clsCommon.myCstr(grow.Cells("Employee Name").Value)
                        If strName.Length > 100 Or (String.IsNullOrEmpty(strName)) Then
                            Throw New Exception("Name can not be blank or incorrect for Emp Id : " + clsCommon.myCstr(grow.Cells("Emp ID").Value) + "")
                        End If
                        obj.Emp_Name = strName

                        '' earning code
                        strName = clsCommon.myCstr(grow.Cells("Earning Code").Value)
                        'If strName.Length > 100 Or (String.IsNullOrEmpty(strName)) Then
                        '    Throw New Exception("Fathers Name can not be blank or incorrect for Emp Id : " + clsCommon.myCstr(grow.Cells("Emp ID").Value) + "")
                        'End If
                        obj.EARNING_CODE = strName
                        '' unit cost
                        strName = clsCommon.mycdbl(grow.Cells("Unit Cost").Value)
                        'If strName.Length > 100 Then
                        '    Throw New Exception("Mothers Name can not be blank or incorrect for Emp Id : " + clsCommon.myCstr(grow.Cells("Emp ID").Value) + "")
                        'End If
                        obj.UNIT_COST = strName

                        '' billing rate
                        strName = clsCommon.mycdbl(grow.Cells("Billing Rate").Value)
                        'If strName.Length > 100 Then
                        '    Throw New Exception("Mothers Name can not be blank or incorrect for Emp Id : " + clsCommon.myCstr(grow.Cells("Emp ID").Value) + "")
                        'End If
                        obj.BILLING_RATE = strName

                        '' map user code
                        strName = clsCommon.myCstr(grow.Cells("User Code").Value)
                        If strName.Length > 100 Or (String.IsNullOrEmpty(strName)) Then
                            Throw New Exception("User Code can not be blank or incorrect for Emp Id : " + clsCommon.myCstr(grow.Cells("Emp ID").Value) + "")
                        End If
                        obj.USER_CODE = strName

                        '' email id
                        strName = clsCommon.myCstr(grow.Cells("Email Id").Value)
                        obj.EMail_ID = strName

                        '' comments
                        strName = clsCommon.myCstr(grow.Cells("Comments").Value)
                        obj.COMMENTS = strName

                        '' check for new entry
                        'Dim isNewEntry As Boolean = True
                        'Dim strq As String = ""
                        'Dim dt As DataTable

                        'strq = "select emp_code from tspl_employee_master where emp_code='" & obj.EMP_CODE & "'"
                        'dt = clsDBFuncationality.GetDataTable(strq, trans)
                        'If dt.Rows.Count > 0 Then
                        '    isNewEntry = False
                        'Else
                        '    isNewEntry = True
                        'End If

                        obj.APPLY_ALL_CUST = True
                        'If Me.chkInactive.Checked = True Then
                        '    obj.Emp_Status = "Inactive"
                        'Else
                        '    obj.Emp_Status = "Active"
                        'End If
                        obj.Emp_Status = "Active"
                        obj.isPJCModule = True
                        Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(EMP_CODE) from TSPL_EMPLOYEE_MASTER where EMP_CODE='" + obj.EMP_CODE + "'", trans)
                        If (qry = 0) Then
                            isNewEntry = True
                        Else
                            isNewEntry = False
                        End If

                        obj.SaveData(obj, isNewEntry, trans)
                    End If
                Next
                clsCommon.ProgressBarHide()
                Common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

    Sub LoadGridColumns()
        gv1.Rows.Clear()
        gv1.Columns.Clear()


        Dim EmpCode As New GridViewTextBoxColumn
        Dim EmpName As New GridViewTextBoxColumn
        Dim CustCode As New GridViewTextBoxColumn
        Dim CustName As New GridViewTextBoxColumn
        Dim BillingRate As New GridViewDecimalColumn
        Dim remarks As New GridViewTextBoxColumn

        EmpCode.FormatString = ""
        EmpCode.HeaderText = "Emp Code"
        EmpCode.Name = colEmpCode
        EmpCode.Width = 0
        EmpCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(EmpCode)
        gv1.Columns(colEmpCode).IsVisible = False

        EmpName.FormatString = ""
        EmpName.HeaderText = "Emp Name"
        EmpName.Name = colEmpName
        EmpName.Width = 0
        EmpName.ReadOnly = True
        EmpName.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(EmpName)
        gv1.Columns(colEmpName).IsVisible = False

        CustCode.FormatString = ""
        CustCode.HeaderText = "Customer Code"
        CustCode.Name = colCustCode
        CustCode.Width = 100
        CustCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(CustCode)

        CustName.FormatString = ""
        CustName.HeaderText = "Customer Name"
        CustName.Name = colCustName
        CustName.Width = 100
        CustName.ReadOnly = True
        CustName.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(CustName)

        BillingRate.FormatString = ""
        BillingRate.HeaderText = "Billing Rate"
        BillingRate.Name = colBillingRate
        BillingRate.Width = 100
        BillingRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(BillingRate)

        remarks.FormatString = ""
        remarks.HeaderText = "Remarks"
        remarks.Name = colRemarks
        remarks.Width = 130
        remarks.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(remarks)
        gv1.Rows.AddNew()
    End Sub

    Private Sub chkApplyToAllCust_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkApplyToAllCust.ToggleStateChanged
        If chkApplyToAllCust.Checked = False Then
            Me.gv1.Enabled = True
        Else
            Me.gv1.Enabled = False
        End If
    End Sub

    Private Sub gv1_CellEndEdit(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellEndEdit


        If gv1.CurrentRow Is Nothing Then
            Exit Sub
        End If


        If Not isCellValueChangedOpen Then
            isCellValueChangedOpen = True
            If e.Column Is gv1.Columns(colCustCode) Then
                Dim strq As String = ""
                strq = "select Cust_Code as [Code],Customer_Name as [Name] from TSPL_CUSTOMER_MASTER "
                gv1.CurrentRow.Cells(colCustCode).Value = clsCommon.ShowSelectForm("cust", strq, "Code", "", clsCommon.myCstr(gv1.CurrentRow.Cells(colCustCode).Value))
                If clsCommon.myLen(gv1.CurrentRow.Cells(colCustCode).Value) > 0 Then
                    Dim strName As String = clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where cust_code='" & gv1.CurrentRow.Cells(colCustCode).Value.ToString & "'").ToString
                    gv1.CurrentRow.Cells(colCustName).Value = strName
                End If

            End If

            isCellValueChangedOpen = False
        End If

    End Sub



    Private Sub gv1_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            'gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                If (clsCommon.myLen(txtCode.Value) > 0) Then
                    gv1.Rows.AddNew()
                    'gvCategoryValues.Rows(gvCategoryValues.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem
                    gv1.CurrentRow = gv1.Rows(intCurrRow)
                End If
            End If
        End If
    End Sub

End Class
