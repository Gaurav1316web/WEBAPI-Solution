' ----------------- Created By Anubhooti On 04-May-2015 BM00000006297 -------------------- '
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
Imports System.IO
Imports XpertERPEngine

Public Class FrmHRTravelReimbursementExpense
    Inherits FrmMainTranScreen

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Dim isInsideLoadData As Boolean = False

    Const ColSNo As String = "S No."
    Const colStatus As String = "Status"
    Const colEmpCode As String = "Emp Code"
    Const colEmpName As String = "Emp Name"
    Const colClaimCode As String = "Claim Code"
    Const colClaimName As String = "Claim Name"
    Const colClaimType As String = "Claim Type"
    Const colAppliedDate As String = "Applied Date"
    Const colLastActionDate As String = "Last Action Date"
    Const colTravelCategory As String = "Travel Category"
    Const colTotalAppliedAmt As String = "Total Applied Amount"
    Const colAppliedAmount As String = "Applied Amount"
    Const colApprovedAmount As String = "Approved Date"
    Const colRemarks As String = "Remarks"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmHRTravelReimbursementExpense)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
    End Sub
    Public Shared Function GetTT() As DataTable
        Dim DT_TT As DataTable = New DataTable
        DT_TT.Columns.Add("Code", GetType(String))
        DT_TT.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT_TT.NewRow()
        DR("Name") = ""
        DR("Code") = ""
        DT_TT.Rows.Add(DR)

        DR = DT_TT.NewRow()
        DR("Name") = "Domestic"
        DR("Code") = "D"
        DT_TT.Rows.Add(DR)

        DR = DT_TT.NewRow()
        DR("Name") = "International"
        DR("Code") = "I"
        DT_TT.Rows.Add(DR)

        DT_TT.AcceptChanges()

        Return DT_TT
    End Function
    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim SNoCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        SNoCode.FormatString = ""
        SNoCode.HeaderText = "S No."
        SNoCode.Name = ColSNo
        SNoCode.Width = 40
        SNoCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(SNoCode)

        Dim IntrCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        IntrCode.FormatString = ""
        IntrCode.HeaderText = "Employee Code"
        IntrCode.Name = colEmpCode
        IntrCode.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
        IntrCode.TextImageRelation = TextImageRelation.TextBeforeImage
        IntrCode.Width = 150
        IntrCode.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(IntrCode)

        Dim Name As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Name.FormatString = ""
        Name.HeaderText = "Employee Name"
        Name.Name = colEmpName
        Name.Width = 125
        Name.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(Name)

        Dim Email As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Email.FormatString = ""
        Email.HeaderText = "Claim Code"
        Email.Name = colClaimCode
        Email.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
        Email.TextImageRelation = TextImageRelation.TextBeforeImage
        Email.Width = 125
        gv1.MasterTemplate.Columns.Add(Email)

        Dim CName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        CName.FormatString = ""
        CName.HeaderText = "Claim Name"
        CName.Name = colClaimName
        CName.Width = 125
        CName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(CName)

        Dim ClaimType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        ClaimType.FormatString = ""
        ClaimType.HeaderText = "Claim Type"
        ClaimType.Name = colClaimType
        ClaimType.Width = 100
        ClaimType.ReadOnly = False
        ClaimType.DataSource = GetTT()
        ClaimType.ValueMember = "Code"
        ClaimType.DisplayMember = "Name"
        ClaimType.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(ClaimType)

     
        Dim StartTime As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        'StartTime.FormatString = "{0:dd/MM/yyyy hh:mm tt}"
        StartTime.Format = DateTimePickerFormat.Custom
        StartTime.FormatString = "{0:dd/MMM/yyyy}"
        StartTime.CustomFormat = "dd/MMM/yyyy"
        StartTime.HeaderText = "Applied Date"
        StartTime.Name = colAppliedDate
        StartTime.Width = 120
        gv1.MasterTemplate.Columns.Add(StartTime)

        Dim EndTime As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        'EndTime.FormatString = "{0:dd/MM/yyyy hh:mm tt}"
        EndTime.Format = DateTimePickerFormat.Custom
        EndTime.FormatString = "{0:dd/MMM/yyyy}"
        EndTime.CustomFormat = "dd/MMM/yyyy"
        EndTime.HeaderText = "Last Action Date"
        EndTime.Name = colLastActionDate
        EndTime.Width = 110
        gv1.MasterTemplate.Columns.Add(EndTime)

        Dim Amount As GridViewDecimalColumn = New GridViewDecimalColumn()
        Amount.FormatString = ""
        Amount.HeaderText = "Applied Amount"
        Amount.Name = colAppliedAmount
        Amount.Width = 100
        Amount.DecimalPlaces = 2
        gv1.MasterTemplate.Columns.Add(Amount)

        Dim AppAmount As GridViewDecimalColumn = New GridViewDecimalColumn()
        AppAmount.FormatString = ""
        AppAmount.HeaderText = "Approved Amount"
        AppAmount.Name = colApprovedAmount
        AppAmount.Width = 100
        AppAmount.DecimalPlaces = 2
        gv1.MasterTemplate.Columns.Add(AppAmount)

        Dim Remarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Remarks.FormatString = ""
        Remarks.HeaderText = "Remarks"
        Remarks.Name = colRemarks
        Remarks.Width = 150
        Remarks.MaxLength = 200
        gv1.MasterTemplate.Columns.Add(Remarks)

        clsCustomFieldGrid.LoadBlankGrid(gv1, MyBase.ArrDetailFields)

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
    End Sub
    Function AllowToSave() As Boolean
        Try
          
            Dim GridRow As Integer = 0
            Dim LineNo As Integer = 0

            If clsCommon.myLen(txtcode.Value) <= 0 Then
                Throw New Exception("please fill code")
            End If
            For Each grow As GridViewRowInfo In gv1.Rows
        
                LineNo += 1
                If clsCommon.myLen(grow.Cells(colEmpCode).Value) > 0 Then
                    If clsCommon.myLen(grow.Cells(colClaimCode).Value) <= 0 Then
                        Throw New Exception("please fill claim code at line no." + clsCommon.myCstr(LineNo) + "")
                    End If
                 
                    If clsCommon.myLen(grow.Cells(colClaimType).Value) <= 0 Then
                        Throw New Exception("please fill claim type at line no." + clsCommon.myCstr(LineNo) + "")
                    End If

                    If clsCommon.myLen(grow.Cells(colAppliedDate).Value) <= 0 Then
                        Throw New Exception("please fill applied date at line no." + clsCommon.myCstr(LineNo) + "")
                    End If
                    If clsCommon.myLen(grow.Cells(colLastActionDate).Value) <= 0 Then
                        Throw New Exception("please fill last action date at line no." + clsCommon.myCstr(LineNo) + "")
                    End If
                    
                    If clsCommon.myCdbl(grow.Cells(colAppliedAmount).Value) <= 0 Then
                        Throw New Exception("Please fill applied amount at line no." + clsCommon.myCstr(LineNo) + "")
                    End If

                    GridRow = GridRow + 1
                End If
            Next
            For i As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myLen(gv1.Rows(i).Cells(colEmpCode).Value) > 0 Then
                    Dim Emp As String = clsCommon.myCstr(gv1.Rows(i).Cells(colEmpCode).Value)
                    Dim ClaimC As String = clsCommon.myCstr(gv1.Rows(i).Cells("Claim Code").Value)
                    For j As Integer = i + 1 To gv1.Rows.Count - 1
                        Dim SecondEmp As String = clsCommon.myCstr(gv1.Rows(j).Cells(colEmpCode).Value)
                        Dim SecClaimC As String = clsCommon.myCstr(gv1.Rows(j).Cells("Claim Code").Value)
                        If clsCommon.CompairString(Emp, SecondEmp) = CompairStringResult.Equal AndAlso clsCommon.CompairString(ClaimC, SecClaimC) = CompairStringResult.Equal Then
                            Throw New Exception("Please check ! Duplicate emp (" + Emp + ") mapped with claim (" + ClaimC + ") in grid")
                        End If
                    Next
                End If
            Next
            If GridRow <= 0 Then
                Throw New Exception("Enter at least one reimbursement summary")
            End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
        Return True
    End Function
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)

        Try
            txtcode.MyReadOnly = True
            isNewEntry = False

            Dim obj As New ClsHRTravelReimbursementExpense()
            obj = ClsHRTravelReimbursementExpense.GetData(strCode, NavTyep)

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Expense_Code) > 0) Then

                isNewEntry = False
                btnSave.Text = "Update"
                'btnDelete.Enabled = True

                txtcode.Value = obj.Expense_Code
                dtpDate.Value = obj.Document_Date
                txtcode.MyReadOnly = True
                Dim ii As Int16 = 0
                If obj.ObjList IsNot Nothing AndAlso obj.ObjList.Count > 0 Then
                    LoadBlankGrid()
                    For Each objTr As ClsHRTravelReimbursementExpenseDetail In obj.ObjList
                        gv1.Rows.AddNew()
                        ii = ii + 1
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColSNo).Value = objTr.S_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colEmpCode).Value = objTr.Emp_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colClaimCode).Value = objTr.Claim_Code
                        If clsCommon.myLen(objTr.Claim_Code) > 0 Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colClaimName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Description FROM TSPL_HR_REIMBURSEMENT_TYPE_MASTER WHERE Reimbursement_Code ='" + clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colClaimCode).Value) + "'"))
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colClaimName).Value = ""
                        End If
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colClaimType).Value = objTr.Claim_Type
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAppliedAmount).Value = objTr.Applied_Amount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colApprovedAmount).Value = objTr.Approved_Amount
                        If clsCommon.myLen(objTr.Emp_Code) > 0 Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colEmpName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Emp_Name FROM TSPL_EMPLOYEE_MASTER WHERE EMP_CODE ='" + clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colEmpCode).Value) + "'"))
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colEmpName).Value = ""
                        End If
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAppliedDate).Value = objTr.Applied_Date
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLastActionDate).Value = objTr.Last_Action_Date
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = objTr.Remarks
                    Next
                End If
            Else
                isNewEntry = True
                'Me.gv1.Rows.Clear()
                'Me.gv1.Rows.AddNew()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    Public Sub Save()

        If AllowToSave() Then
            Dim arr As New List(Of ClsHRTravelReimbursementExpense)
            Dim obj As New ClsHRTravelReimbursementExpense()
            obj.Expense_Code = txtcode.Value
            obj.Document_Date = dtpDate.Value
            obj.ObjList = New List(Of ClsHRTravelReimbursementExpenseDetail)
            For Each grow As GridViewRowInfo In gv1.Rows  
                Dim objTr As New ClsHRTravelReimbursementExpenseDetail()
                If clsCommon.myLen(grow.Cells(colEmpCode).Value) > 0 Then
                    objTr.S_No = clsCommon.myCstr(grow.Cells(ColSNo).Value)
                    objTr.Emp_Code = clsCommon.myCstr(grow.Cells(colEmpCode).Value)
                    objTr.Claim_Code = clsCommon.myCstr(grow.Cells(colClaimCode).Value)
                    objTr.Claim_Type = clsCommon.myCstr(grow.Cells(colClaimType).Value)
                    objTr.Approved_Amount = clsCommon.myCdbl(grow.Cells(colApprovedAmount).Value)
                    objTr.Applied_Amount = clsCommon.myCdbl(grow.Cells(colAppliedAmount).Value)
                    objTr.Applied_Date = clsCommon.myCDate(grow.Cells(colAppliedDate).Value)
                    objTr.Last_Action_Date = clsCommon.myCstr(grow.Cells(colLastActionDate).Value)
                    objTr.Remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value)
                    obj.ObjList.Add(objTr)
                End If
            Next
            arr.Add(obj)

            If (ClsHRTravelReimbursementExpense.SaveData(arr)) Then
                common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                LoadData(obj.Expense_Code, NavigatorType.Current)
                btnSave.Text = "Update"
                ' btnDelete.Enabled = True
            Else
                btnSave.Text = "Save"
                '  btnDelete.Enabled = False
            End If

        End If
    End Sub
    Sub OpenEmpCodeList(ByVal isButtonClick As Boolean)
        Dim qry As String = ""
        gv1.CurrentRow.Cells(colEmpCode).Value = clsEmployeeMaster.getFinder("", clsCommon.myCstr(gv1.CurrentRow.Cells(colEmpCode).Value), isButtonClick)
        If clsCommon.myLen(gv1.CurrentRow.Cells(colEmpCode).Value) > 0 Then
            qry = "select isnull(EMP_CODE,'') As [Code],isnull(Emp_Name,'') As [Name],isnull(EMail_ID,'') As Email,isnull(DEPARTMENT_CODE,'') As [DEPARTMENT CODE],isnull(Designation,'') As [Designation Code] from TSPL_EMPLOYEE_MASTER  WHERE Emp_Code ='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colEmpCode).Value) + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv1.CurrentRow.Cells(colEmpName).Value = clsCommon.myCstr(dt.Rows(0)("Name"))
            End If
        End If
    End Sub
    Sub OpenReimburseCodeList(ByVal isButtonClick As Boolean)
        Dim qry As String = ""
        gv1.CurrentRow.Cells(colClaimCode).Value = ClsReimbursementTypeMaster.getFinder("", clsCommon.myCstr(gv1.CurrentRow.Cells(colClaimCode).Value), isButtonClick)
        If clsCommon.myLen(gv1.CurrentRow.Cells(colClaimCode).Value) > 0 Then
            qry = "select isnull(Reimbursement_Code,'') As [Code],isnull(Description,'') As [Description],CASE WHEN Reimbursement_Type ='T' THEN 'Travel' WHEN Reimbursement_Type ='F' THEN 'Food' WHEN Reimbursement_Type ='C' THEN 'Conveyance' WHEN Reimbursement_Type ='O' THEN 'Others' WHEN Reimbursement_Type ='M' THEN 'Miscellaneous'  END AS [Reimbursement Type],Travel_Type  AS [Travel Type] from TSPL_HR_REIMBURSEMENT_TYPE_MASTER  WHERE Reimbursement_Code ='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colClaimCode).Value) + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv1.CurrentRow.Cells(colClaimName).Value = clsCommon.myCstr(dt.Rows(0)("Description"))
                gv1.CurrentRow.Cells(colClaimType).Value = clsCommon.myCstr(dt.Rows(0)("Travel Type"))
            End If
        End If
    End Sub
    Sub funReset()
        isNewEntry = True
        txtcode.Value = Nothing
        txtcode.MyReadOnly = False
        dtpDate.Value = clsCommon.GETSERVERDATE()
        txtcode.Focus()
        Me.gv1.Rows.Clear()
        Me.gv1.Rows.AddNew()
        btnsave.Text = "Save"
        btnsave.Enabled = True
     
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(txtcode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("code not found to delete")
            Exit Sub
        End If

        funDelete()
    End Sub
    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (ClsHRTravelReimbursementExpense.DeleteData(txtcode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If AllowToSave() Then
            Save()
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
        GC.Collect()
    End Sub

    Private Sub txtcode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtcode._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE where Expense_Code='" + txtcode.Value + "'"
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If check > 0 Then
                txtCode.MyReadOnly = True
            ElseIf check <= 0 Then
                txtCode.MyReadOnly = False
            End If
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtcode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtcode._MYValidating
        Try
            Dim str As String = "select count(*) from TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE where Expense_Code ='" + txtcode.Value + "' "
            Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
            If no = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If
            If txtCode.MyReadOnly OrElse isButtonClicked Then

                txtcode.Value = ClsHRTravelReimbursementExpense.GetFinder("", txtcode.Value, isButtonClicked)
                If clsCommon.myLen(txtCode.Value) > 0 Then
                    ' btnDelete.Enabled = True
                    btnSave.Text = "Update"
                    txtCode.MyReadOnly = True
                Else
                    btnSave.Text = "Save"
                    '  btnDelete.Enabled = False
                    txtCode.MyReadOnly = False
                End If
            End If
            LoadData(txtCode.Value, NavigatorType.Current)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FrmHRTravelReimbursementExpense_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            If AllowToSave() Then
                Save()
            End If
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso BtnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            Me.Close()
            GC.Collect()
        End If
    End Sub

    Private Sub FrmHRTravelReimbursementExpense_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetUserMgmtNew()
        LoadBlankGrid()
        funReset()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
        ButtonToolTip.SetToolTip(BtnDelete, "Press Alt+D for Delete")
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If isInsideLoadData = False Then
                If e.Column Is gv1.Columns(colEmpCode) Then
                    OpenEmpCodeList(False)
                ElseIf e.Column Is gv1.Columns(colClaimCode) Then
                    OpenReimburseCodeList(False)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gv1_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(ColSNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gv1_UserAddedRow(sender As Object, e As GridViewRowEventArgs) Handles gv1.UserAddedRow
        For i As Integer = 0 To gv1.Rows.Count - 1
            gv1.Rows(0).Cells(0).Value = 1
            If i <> 0 Then
                gv1.Rows(i).Cells(ColSNo).Value = i + 1
            End If
        Next
    End Sub

    Private Sub gv1_UserDeletingRow(sender As Object, e As GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        DeleteData()
    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        funReset()
    End Sub
End Class
