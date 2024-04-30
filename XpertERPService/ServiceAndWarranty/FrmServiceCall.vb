' ----------------- Created By Anubhooti On 15-Oct-2015 Against -------------------- '
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
'Imports XpertERPCommanServices

Public Class FrmServiceCall
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Dim isInsideLoadData As Boolean = False
    Public errorControl As clsErrorControl = New clsErrorControl()
    Const ReportID As String = "SWServiceCall"
    Private objSol As ClsSolutionKnowledgeBase
    Dim frm As New FrmSolutionKnowledgeBase()
#Region "Solution"
    Public Const ColSNo As String = "SNo"
    Public Const ColCode As String = "Code"
    Public Const ColSolution As String = "Solution"
    Public Const ColSymptom As String = "Symptom"
    Public Const ColUpdatedBy As String = "Updated By"
    Public Const ColUpdatedOn As String = "Updated On"
#End Region
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmServiceCall)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub
    Sub LoadBlankGrid()
        gvSol.Rows.Clear()
        gvSol.Columns.Clear()

        Dim repoSNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSNo.FormatString = ""
        repoSNo.HeaderText = "SNo"
        repoSNo.Name = ColSNo
        repoSNo.Width = 70
        repoSNo.ReadOnly = True
        gvSol.MasterTemplate.Columns.Add(repoSNo)

        Dim repoCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCode.FormatString = ""
        repoCode.HeaderText = "Code"
        repoCode.Name = ColCode
        repoCode.Width = 150
        repoCode.ReadOnly = True
        gvSol.MasterTemplate.Columns.Add(repoCode)

        Dim repoUpdB As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUpdB.FormatString = ""
        repoUpdB.HeaderText = "Updated By"
        repoUpdB.Name = ColUpdatedBy
        repoUpdB.Width = 150
        repoUpdB.ReadOnly = True
        gvSol.MasterTemplate.Columns.Add(repoUpdB)

        Dim repoUpdO As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUpdO.FormatString = ""
        repoUpdO.HeaderText = "Updated On"
        repoUpdO.Name = ColUpdatedOn
        repoUpdO.Width = 150
        repoUpdO.ReadOnly = True
        gvSol.MasterTemplate.Columns.Add(repoUpdO)

        Dim repoSol As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSol.FormatString = ""
        repoSol.HeaderText = "Solution"
        repoSol.Name = ColSolution
        repoSol.Width = 250
        repoSol.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoSol.ReadOnly = True
        gvSol.MasterTemplate.Columns.Add(repoSol)

        Dim repoSym As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSym.FormatString = ""
        repoSym.HeaderText = "Symptom"
        repoSym.Name = ColSymptom
        repoSym.Width = 250
        repoSym.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoSym.ReadOnly = True
        gvSol.MasterTemplate.Columns.Add(repoSym)

        gvSol.AllowAddNewRow = False
        gvSol.ShowGroupPanel = False
        gvSol.AllowColumnReorder = True
        gvSol.AllowRowReorder = False
        gvSol.EnableSorting = False
        gvSol.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvSol.MasterTemplate.ShowRowHeaderColumn = False
        gvSol.TableElement.TableHeaderHeight = 40

        ReStoreGridLayout()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvSol.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvSol.Columns.Count - 1 Step ii + 1
                        gvSol.Columns(ii).IsVisible = False
                        gvSol.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gvSol.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Sub AddNew()
        isNewEntry = True
        txtcode.MyReadOnly = False
        txtcode.Value = Nothing
        txtcode.Focus()
        TxtCustGrp.Value = ""
        LblDealer.Text = ""
        LblCustGrp.Text = ""
        TxtDealer.Value = ""
        LblDealer.Text = ""
        TxtVehicleName.Value = ""
        LblVehicleName.Text = ""
        TxtItemPartNo.Value = ""
        LblItemPartNo.Text = ""
        TxtSubject.Text = ""
        TxtCallType.Value = ""
        LblCallType.Text = ""
        TxtProbType.Value = ""
        LblProbType.Text = ""
        LblStartDate.Text = clsCommon.GETSERVERDATE()
        LblClosedDate.Text = ""
        TxtResolution.Text = ""
        TxtIssueNotice.Value = ""
        LblIssueNotice.Text = ""
        TxtActivityType.Value = ""
        LblActivityType.Text = ""
        LblTeleNo.Text = ""
        LblEmail.Text = ""
        TxtActRemarks.Text = ""
        TxtDocType.Value = ""
        TxtDocNo.Value = ""
        LblAssignedBy.Text = objCommonVar.CurrentUserCode
        MyLabel11.Text = objCommonVar.CurrentUser
        LblAssignedByCode.Text = objCommonVar.CurrentUserCode
        LblAssignedByName.Text = objCommonVar.CurrentUser
        Me.CmbCallStatus.DataSource = ClsServiceCall.GetCallStatus()
        Me.CmbCallStatus.DisplayMember = "Name"
        Me.CmbCallStatus.ValueMember = "Code"

        Me.CmbPriority.DataSource = ClsServiceCall.GetPriority()
        Me.CmbPriority.DisplayMember = "Name"
        Me.CmbPriority.ValueMember = "Code"

        Me.CmbOrigin.DataSource = ClsServiceCall.GetOrigin()
        Me.CmbOrigin.DisplayMember = "Name"
        Me.CmbOrigin.ValueMember = "Code"

        Me.CmbRecurrence.DataSource = ClsServiceCall.GetRecurrence()
        Me.CmbRecurrence.DisplayMember = "Name"
        Me.CmbRecurrence.ValueMember = "Code"

        Me.RadPageView1.SelectedPage = RadPgGen

        dtpDate.Value = clsCommon.GETSERVERDATE()

        '' Blank Grid
        LoadBlankGrid()
        'gvSol.Rows.AddNew()
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = False
    End Sub
    'Sub LoadSolData()
    '    Dim RowCount As Integer = 0
    '    objSol = frm.ObjRtnSol
    '    If objSol IsNot Nothing Then
    '        objSol = New ClsSolutionKnowledgeBase()
    '        For Each grow As GridViewRowInfo In gvSol.Rows
    '            If clsCommon.myLen(clsCommon.myCstr(grow.Cells(ColCode).Value)) > 0 Then
    '                RowCount += 1
    '            End If
    '        Next
    '        gvSol.Rows(RowCount).Cells(ColCode).Value = objSol.Document_Code
    '    End If
    'End Sub
    Sub ViewSolution()
        Try
            '  Dim frm As New FrmSolutionKnowledgeBase()
            frm.Instance = True
            frm.btnsave.Text = "Ok"
            'objSol = frm.ObjRtnSol
            frm.Instance = False
            frm.frm = Me
            frm.ShowDialog()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Function AllowToSave() As Boolean
        Try
            btnsave.Focus()

            If clsCommon.myLen(TxtCustGrp.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please fill customer group")
                TxtCustGrp.Focus()
                errorControl.SetError(TxtCustGrp, "Customer Group must not be blank ")
                Return False
            Else
                errorControl.SetError(TxtCustGrp, "")
            End If

            If clsCommon.myLen(TxtDealer.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please fill dealer")
                TxtDealer.Focus()
                errorControl.SetError(TxtDealer, "Dealer must not be blank ")
                Return False
            Else
                errorControl.SetError(TxtDealer, "")
            End If

            If clsCommon.myLen(TxtVehicleName.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please fill vehicle name")
                TxtVehicleName.Focus()
                errorControl.SetError(TxtVehicleName, "Vehicle name must not be blank ")
                Return False
            Else
                errorControl.SetError(TxtVehicleName, "")
            End If

            If clsCommon.myLen(TxtItemPartNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please fill item part no.")
                TxtItemPartNo.Focus()
                errorControl.SetError(TxtItemPartNo, "Item part no. must not be blank ")
                Return False
            Else
                errorControl.SetError(TxtItemPartNo, "")
            End If

            If clsCommon.myLen(TxtIssueNotice.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please fill issue notice")
                TxtIssueNotice.Focus()
                errorControl.SetError(TxtIssueNotice, "Issue notice must not be blank ")
                Return False
            Else
                errorControl.SetError(TxtIssueNotice, "")
            End If

            If clsCommon.myLen(TxtSubject.Text) <= 0 Then
                clsCommon.MyMessageBoxShow("Please fill Subject")
                TxtSubject.Focus()
                errorControl.SetError(TxtSubject, "Subject must not be blank ")
                Return False
            Else
                errorControl.SetError(TxtSubject, "")
            End If

            If clsCommon.myLen(TxtProbType.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please fill problem type")
                TxtProbType.Focus()
                errorControl.SetError(TxtProbType, "Problem Type must not be blank ")
                Return False
            Else
                errorControl.SetError(TxtProbType, "")
            End If

            If clsCommon.myLen(TxtCallType.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please fill call type")
                TxtCallType.Focus()
                errorControl.SetError(TxtCallType, "Call Type must not be blank ")
                Return False
            Else
                errorControl.SetError(TxtCallType, "")
            End If

            If clsCommon.myLen(TxtActivityType.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please fill activity type")
                TxtActivityType.Focus()
                errorControl.SetError(TxtActivityType, "Activity Type must not be blank ")
                Return False
            Else
                errorControl.SetError(TxtActivityType, "")
            End If

            If clsCommon.myLen(TxtAssignedTo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please fill assigned to")
                TxtAssignedTo.Focus()
                errorControl.SetError(TxtAssignedTo, "Assigned to must not be blank ")
                Return False
            Else
                errorControl.SetError(TxtAssignedTo, "")
            End If

            If clsCommon.CompairString(CmbCallStatus.SelectedValue, "C") = CompairStringResult.Equal AndAlso clsCommon.myLen(TxtResolution.Text) <= 0 Then
                clsCommon.MyMessageBoxShow("Please fill resolution remarks")
                TxtResolution.Focus()
                errorControl.SetError(TxtResolution, "Resolution remarks must not be blank ")
                Return False
            Else
                errorControl.SetError(TxtResolution, "")
            End If

            'Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function
    Public Sub Save()
        Try

            If AllowToSave() Then

                Dim arr As New List(Of ClsServiceCall)
                Dim obj As New ClsServiceCall()
                obj.Service_Call_Code = clsCommon.myCstr(txtcode.Value)
                obj.Cust_Group_Code = clsCommon.myCstr(TxtCustGrp.Value)
                obj.Service_Call_Date = dtpDate.Value
                obj.Dealer_Code = clsCommon.myCstr(TxtDealer.Value)
                obj.Call_Type = TxtCallType.Value
                obj.Call_Status = CmbCallStatus.SelectedValue
                obj.Priority = CmbPriority.SelectedValue
                obj.Assigned_To = TxtAssignedTo.Value
                obj.Assigned_By = LblAssignedBy.Text
                obj.Recurrence = CmbRecurrence.SelectedValue
                obj.Origin = CmbOrigin.SelectedValue
                obj.Start_Date = LblStartDate.Text
                obj.Closed_Date = LblClosedDate.Text
                obj.Problem_Type = TxtProbType.Value
                obj.Activity_Type = TxtActivityType.Value
                If ChkSMS.Checked = True Then
                    obj.SMS = "1"
                Else
                    obj.SMS = "0"
                End If
                If ChkEmail.Checked = True Then
                    obj.Email = "1"
                Else
                    obj.Email = "0"
                End If
                obj.Item_Part_No = clsCommon.myCstr(TxtItemPartNo.Value)
                obj.Subject = clsCommon.myCstr(TxtSubject.Text)
                obj.Issued_Notice = clsCommon.myCstr(TxtIssueNotice.Value)
                obj.Vehicle_Code = clsCommon.myCstr(TxtVehicleName.Value)
                obj.Vehicle_Sr_No = LblSerialNo.Text
                obj.Activity_Remarks = TxtActRemarks.Text
                obj.Document_Type = TxtDocType.Value
                obj.Document_No = TxtDocNo.Value
                obj.Resolution_Remarks = TxtResolution.Text
                '' Solution Tab
                obj.ObjList = New List(Of ClsServiceCallSolution)
                For Each grow As GridViewRowInfo In gvSol.Rows
                    If clsCommon.myLen(grow.Cells(ColCode).Value) <= 0 Then
                        Continue For
                    End If
                    Dim objTr As New ClsServiceCallSolution()
                    objTr.Service_Call_Code = clsCommon.myCstr(Me.txtcode.Value)
                    objTr.Sol_Knowledge_Code = clsCommon.myCstr(grow.Cells(ColCode).Value)
                    obj.ObjList.Add(objTr)
                Next

                arr.Add(obj)
                If (ClsServiceCall.SaveData(arr)) Then
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    LoadData(obj.Service_Call_Code, NavigatorType.Current)
                    btnsave.Text = "Update"
                    btndelete.Enabled = True
                Else
                    btnsave.Text = "Save"
                    btndelete.Enabled = False
                End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            txtcode.MyReadOnly = True
            btnsave.Enabled = True
            btndelete.Enabled = True
            isNewEntry = False

            Dim obj As New ClsServiceCall()
            obj = ClsServiceCall.GetData(strCode, NavTyep)

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Service_Call_Code) > 0) Then
                AddNew()
                isNewEntry = False
                btnsave.Text = "Update"
                btndelete.Enabled = True

                txtcode.Value = obj.Service_Call_Code
                TxtCustGrp.Value = obj.Cust_Group_Code

                If clsCommon.myLen(obj.Cust_Group_Code) > 0 Then
                    LblCustGrp.Text = clsDBFuncationality.getSingleValue("Select isnull(Cust_Group_Desc,'') As Cust_Group_Desc From TSPL_CUSTOMER_GROUP_MASTER Where Cust_Group_Code ='" + obj.Cust_Group_Code + "'")
                Else
                    LblCustGrp.Text = ""
                End If
                dtpDate.Value = obj.Service_Call_Date
                TxtDealer.Value = obj.Dealer_Code

                If clsCommon.myLen(TxtDealer.Value) > 0 Then
                    LblDealer.Text = clsDBFuncationality.getSingleValue("Select isnull(Customer_Name,'') As Customer_Name From TSPL_CUSTOMER_MASTER Where Cust_Code ='" + TxtDealer.Value + "'")
                Else
                    LblDealer.Text = ""
                End If
                TxtVehicleName.Value = obj.Vehicle_Code
                LblSerialNo.Text = obj.Vehicle_Sr_No
                If clsCommon.myLen(TxtVehicleName.Value) > 0 Then
                    LblVehicleName.Text = clsDBFuncationality.getSingleValue("Select ISNULL(Item_Desc,'') As Item_Desc From TSPL_ITEM_MASTER Where Item_Code ='" + TxtVehicleName.Value + "'")
                Else
                    LblVehicleName.Text = ""
                End If
                CmbCallStatus.SelectedValue = obj.Call_Status
                CmbOrigin.SelectedValue = obj.Origin
                CmbPriority.SelectedValue = obj.Priority
                CmbRecurrence.SelectedValue = obj.Recurrence
                LblAssignedBy.Text = obj.Assigned_By
                LblStartDate.Text = obj.Start_Date
                LblClosedDate.Text = obj.Closed_Date
                TxtItemPartNo.Value = obj.Item_Part_No
                TxtIssueNotice.Value = obj.Issued_Notice
                TxtAssignedTo.Value = obj.Assigned_To
                TxtResolution.Text = obj.Resolution_Remarks
                TxtActRemarks.Text = obj.Activity_Remarks
                TxtDocType.Value = obj.Document_Type
                TxtDocNo.Value = obj.Document_No



                If clsCommon.CompairString(obj.SMS, "1") = CompairStringResult.Equal Then
                    ChkSMS.Checked = True
                Else
                    ChkSMS.Checked = False
                End If
                If clsCommon.CompairString(obj.Email, "1") = CompairStringResult.Equal Then
                    ChkEmail.Checked = True
                Else
                    ChkEmail.Checked = False
                End If
                TxtProbType.Value = obj.Problem_Type
                If clsCommon.myLen(TxtProbType.Value) > 0 Then
                    LblProbType.Text = clsDBFuncationality.getSingleValue("Select isnull(Problem_Type_Name,'') As Problem_Type_Name From TSPL_SW_PROBLEM_TYPE_MASTER Where Problem_Type_Code='" + TxtProbType.Value + "'")
                Else
                    LblProbType.Text = ""
                End If
                TxtCallType.Value = obj.Call_Type
                If clsCommon.myLen(TxtCallType.Value) > 0 Then
                    LblCallType.Text = clsDBFuncationality.getSingleValue("Select isnull(Call_Type_Name,'') As Call_Type_Name From TSPL_SW_CALL_TYPE_MASTER Where Call_Type_Code='" + TxtCallType.Value + "'")
                Else
                    LblCallType.Text = ""
                End If
                TxtActivityType.Value = obj.Activity_Type
                If clsCommon.myLen(TxtActivityType.Value) > 0 Then
                    LblActivityType.Text = clsDBFuncationality.getSingleValue("Select isnull(Activity_Type_Name,'') As Activity_Type_Name From tspl_sw_activity_type_master Where Activity_Type_Code='" + TxtActivityType.Value + "'")
                Else
                    LblActivityType.Text = ""
                End If
                If clsCommon.myLen(TxtAssignedTo.Value) > 0 Then
                    LblAssignedTo.Text = clsDBFuncationality.getSingleValue("Select isnull(Emp_Name,'') As Emp_Name From TSPL_EMPLOYEE_MASTER Where EMP_CODE='" + TxtAssignedTo.Value + "'")
                    LblTechCode.Text = TxtAssignedTo.Value
                    LblTeleNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Phone FROM TSPL_EMPLOYEE_MASTER WHERE EMP_CODE='" & TxtAssignedTo.Value & "'"))
                    LblEmail.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT EMail_ID FROM TSPL_EMPLOYEE_MASTER WHERE EMP_CODE='" & TxtAssignedTo.Value & "'"))
                    TechName.Text = LblAssignedTo.Text
                Else
                    LblAssignedTo.Text = ""
                    LblTechCode.Text = ""
                    TechName.Text = ""
                    LblTeleNo.Text = ""
                    LblEmail.Text = ""
                End If
                If clsCommon.myLen(TxtIssueNotice.Value) > 0 Then
                    LblIssueNotice.Text = clsDBFuncationality.getSingleValue("Select isnull(Fault_Master_Name,'') As Fault_Master_Name From TSPL_SW_FAULT_MASTER Where Fault_Master_Code='" + TxtIssueNotice.Value + "'")
                Else
                    LblIssueNotice.Text = ""
                End If
                '' Solution Details

                Dim ii As Int16 = 0
                If obj.ObjList IsNot Nothing AndAlso obj.ObjList.Count > 0 Then
                    LoadBlankGrid()
                    For Each objTr As ClsServiceCallSolution In obj.ObjList
                        gvSol.Rows.AddNew()
                        ii = ii + 1
                        gvSol.Rows(gvSol.Rows.Count - 1).Cells(ColSNo).Value = ii
                        gvSol.Rows(gvSol.Rows.Count - 1).Cells(ColCode).Value = objTr.Sol_Knowledge_Code
                        gvSol.Rows(gvSol.Rows.Count - 1).Cells(ColUpdatedBy).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Updated_By From TSPL_SW_SOLUTION_KNOWLEDGE_BASE Where Document_Code ='" + objTr.Sol_Knowledge_Code + "'"))
                        gvSol.Rows(gvSol.Rows.Count - 1).Cells(ColUpdatedOn).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Updated_On From TSPL_SW_SOLUTION_KNOWLEDGE_BASE Where Document_Code ='" + objTr.Sol_Knowledge_Code + "'"))
                        gvSol.Rows(gvSol.Rows.Count - 1).Cells(ColSolution).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Solution From TSPL_SW_SOLUTION_KNOWLEDGE_BASE Where Document_Code ='" + objTr.Sol_Knowledge_Code + "'"))
                        gvSol.Rows(gvSol.Rows.Count - 1).Cells(ColSymptom).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Symptom From TSPL_SW_SOLUTION_KNOWLEDGE_BASE Where Document_Code ='" + objTr.Sol_Knowledge_Code + "'"))
                    Next
                End If
                txtcode.MyReadOnly = True
            Else
                isNewEntry = True
                AddNew()
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(txtcode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("code not found to delete")
            Exit Sub
        End If

        FunDelete()
    End Sub
    Sub FunDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (ClsServiceCall.DeleteData(txtcode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    AddNew()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Private Sub BtnRecommend_Click(sender As Object, e As EventArgs) Handles BtnRecommend.Click
        ViewSolution()
    End Sub

    Private Sub FrmServiceCall_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnnew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnsave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            AddNew()
        End If
    End Sub

    Private Sub FrmServiceCall_Load(sender As Object, e As EventArgs) Handles Me.Load
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New Trasnaction")
        isNewEntry = True
        AddNew()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click

    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs)
        AddNew()
    End Sub

    Private Sub TxtCustGrp__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtCustGrp._MYValidating
        Try
            TxtCustGrp.Value = clsCustomerGroupMaster.getFinder("", TxtCustGrp.Value, isButtonClicked)
            If clsCommon.myLen(TxtCustGrp.Value) > 0 Then
                LblCustGrp.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Cust_Group_Desc FROM TSPL_CUSTOMER_GROUP_MASTER WHERE Cust_Group_Code='" & TxtCustGrp.Value & "'"))
            Else
                LblCustGrp.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtProbType__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtProbType._MYValidating
        Try
            TxtProbType.Value = ClsProblemType.GetFinder("", TxtProbType.Value, isButtonClicked)
            If clsCommon.myLen(TxtProbType.Value) > 0 Then
                LblProbType.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Problem_Type_Name FROM TSPL_SW_PROBLEM_TYPE_MASTER WHERE Problem_Type_Code='" & TxtProbType.Value & "'"))
            Else
                LblProbType.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtDealer__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtDealer._MYValidating
        Try
            If clsCommon.myLen(TxtCustGrp.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Select Customer Group First..")
                TxtCustGrp.Focus()
                Exit Sub
            End If

            LblDealer.Text = ""
            TxtVehicleName.Value = ""
            LblVehicleName.Text = ""
            TxtItemPartNo.Value = ""
            LblItemPartNo.Text = ""

            TxtDealer.Value = clsCustomerMaster.getFinder(" Cust_Group_Code ='" & TxtCustGrp.Value & "' ", TxtDealer.Value, isButtonClicked)
            If clsCommon.myLen(TxtDealer.Value) > 0 Then
                LblDealer.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Customer_Name FROM TSPL_CUSTOMER_MASTER WHERE Cust_Code='" & TxtDealer.Value & "'"))
            Else
                LblDealer.Text = ""
                TxtVehicleName.Value = ""
                LblVehicleName.Text = ""
                TxtItemPartNo.Value = ""
                LblItemPartNo.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtVehicleName__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtVehicleName._MYValidating
        Try
            If clsCommon.myLen(TxtDealer.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please select dealer first", Me.Text)
                TxtDealer.Focus()
                TxtDealer.Select()
                Return
            End If

            LblVehicleName.Text = ""
            TxtItemPartNo.Value = ""
            LblItemPartNo.Text = ""

            Dim Qry As String = String.Empty
            Qry = " SELECT DISTINCT TSPL_SD_SALE_INVOICE_DETAIL.Item_Code AS [Code],TSPL_ITEM_MASTER.Item_Desc As [Item Desp],TSPL_SERIAL_ITEM.Auto_Sr_No AS [SrNo],TSPL_ITEM_MASTER.Short_Description AS [Short Description],TSPL_ITEM_MASTER.Item_Type AS [Item Type],ISNULL(TSPL_ITEM_MASTER.item_category,'') AS [Item Category],ISNULL(TSPL_MF_BOM_HEAD.REVISION_NO,'') AS [Revision No] FROM TSPL_SD_SALE_INVOICE_HEAD " & _
                  " LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_DETAIL ON TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE =TSPL_SD_SALE_INVOICE_HEAD.Document_Code " & _
                  " LEFT OUTER JOIN TSPL_MF_BOM_HEAD ON TSPL_MF_BOM_HEAD.PROD_ITEM_CODE  = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " & _
                  " LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " & _
                  " LEFT OUTER JOIN TSPL_SERIAL_ITEM ON TSPL_SERIAL_ITEM.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " & _
                  " WHERE TSPL_SD_SALE_INVOICE_HEAD.Trans_Type ='ALL' AND TSPL_MF_BOM_HEAD.POSTED=1 AND TSPL_SD_SALE_INVOICE_HEAD.Status=1 AND TSPL_SD_SALE_INVOICE_HEAD.Customer_Code='" & TxtDealer.Value & "'  AND TSPL_SERIAL_ITEM.Document_Type='SD-IN' "

            Dim dr As DataRow = clsCommon.ShowSelectFormForRow("FVDE", Qry)
            If dr IsNot Nothing Then
                TxtVehicleName.Value = clsCommon.myCstr(dr("Code"))
                LblVehicleName.Text = clsCommon.myCstr(dr("Item Desp"))
                LblSerialNo.Text = clsCommon.myCstr(dr("SrNo"))
            Else
                TxtVehicleName.Value = ""
                LblVehicleName.Text = ""
                TxtItemPartNo.Value = ""
                LblItemPartNo.Text = ""
                LblSerialNo.Text = ""
                LblItemType.Text = ""
            End If

            If clsCommon.myLen(TxtVehicleName.Value) <= 0 Then
                LblVehicleName.Text = ""
                TxtItemPartNo.Value = ""
                LblItemPartNo.Text = ""
                LblSerialNo.Text = ""
                LblItemType.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtItemPartNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtItemPartNo._MYValidating
        Try
            If clsCommon.myLen(TxtVehicleName.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Select Vehicle First..")
                TxtVehicleName.Focus()
                Exit Sub
            End If

            Dim qry As String = ""
            qry = " Select CONSM_ITEM_CODE AS Code,ITEM_DESCRIPTION As [Item Description] From TSPL_MF_BOM_DETAIL LEFT OUTER JOIN TSPL_MF_BOM_HEAD ON TSPL_MF_BOM_HEAD.BOM_CODE =TSPL_MF_BOM_DETAIL.BOM_CODE "
            'qry = " Select TSPL_MF_PRINCIPLE_RECEIPT_DETAIL.Main_Item_Code  AS MCode,TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.Item_Code AS Code,TSPL_ITEM_MASTER.Item_Desc As [Item Description],TSPL_ITEM_MASTER.Item_Type As [Item Type] " & _
            '     " ,TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.Serial_No As [Sr No],TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.Main_Serial_No AS [Main Sr No] " & _
            '    " From TSPL_MF_PRINCIPLE_RECEIPT_DETAIL " & _
            '   " LEFT OUTER JOIN TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL ON TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.Main_Item_Code= TSPL_MF_PRINCIPLE_RECEIPT_DETAIL.Main_Item_Code " & _
            '  " LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code  = TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.Item_Code " & _
            ' " WHERE TSPL_MF_PRINCIPLE_RECEIPT_DETAIL.Main_Item_Code  ='" & TxtVehicleName.Value & "' AND TSPL_MF_PRINCIPLE_RECEIPT_DETAIL.Serial_No='" & LblSerialNo.Text & "' AND TSPL_MF_PRINCIPLE_RECEIPT_SERIAL_DETAIL.Main_Serial_No ='" & LblSerialNo.Text & "' "

            'Dim DR As DataRow = clsCommon.ShowSelectFormForRow("SWCallItem", qry)
            TxtItemPartNo.Value = clsCommon.ShowSelectForm("SWSerEnqIC", qry, "Code", "  TSPL_MF_BOM_HEAD.PROD_ITEM_CODE ='" & TxtVehicleName.Value & "' ", TxtItemPartNo.Value, "Code", isButtonClicked)

            'If DR IsNot Nothing Then
            '    TxtItemPartNo.Value = clsCommon.myCstr(DR("Code"))
            '    LblItemPartNo.Text = clsCommon.myCstr(DR("Item Description"))
            '    LblItemType.Text = clsCommon.myCstr(DR("Item Type"))
            'Else
            '    TxtItemPartNo.Value = ""
            '    LblItemPartNo.Text = ""
            '    LblItemType.Text = ""
            'End If
            If clsCommon.myLen(TxtItemPartNo.Value) > 0 Then
                LblItemPartNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Item_Desc,'') As Item_Desc From TSPL_ITEM_MASTER Where Item_Code ='" + TxtItemPartNo.Value + "'"))
                LblItemType.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT CASE WHEN Item_Type='F' THEN 'Finished Goods' WHEN Item_Type='S' THEN 'Semi Finished Good' WHEN Item_Type ='R' THEN 'Raw Material' WHEN Item_Type ='A' THEN 'Asset' WHEN Item_Type ='T' THEN 'Trading Good' WHEN Item_Type='O' THEN 'Other' END AS [ItemType] FROM TSPL_ITEM_MASTER WHERE Item_Code ='" & TxtItemPartNo.Value & "'"))
            Else
                TxtItemPartNo.Value = ""
                LblItemPartNo.Text = ""
                LblItemType.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtCallType__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtCallType._MYValidating
        Try
            TxtCallType.Value = ClsCallType.GetFinder("", TxtCallType.Value, isButtonClicked)
            If clsCommon.myLen(TxtCallType.Value) > 0 Then
                LblCallType.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Call_Type_Name FROM TSPL_SW_CALL_TYPE_MASTER WHERE Call_Type_Code='" & TxtCallType.Value & "'"))
            Else
                LblCallType.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtActivityType__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtActivityType._MYValidating
        Try
            TxtActivityType.Value = ClsActivityType.GetFinder("", TxtActivityType.Value, isButtonClicked)
            If clsCommon.myLen(TxtActivityType.Value) > 0 Then
                LblActivityType.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Activity_Type_Name FROM TSPL_SW_ACTIVITY_TYPE_MASTER WHERE Activity_Type_Code='" & TxtActivityType.Value & "'"))
            Else
                LblActivityType.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtAssignedTo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtAssignedTo._MYValidating
        Try
            TxtAssignedTo.Value = clsEmployeeMaster.getFinder("", TxtAssignedTo.Value, isButtonClicked)
            If clsCommon.myLen(TxtActivityType.Value) > 0 Then
                LblTechCode.Text = TxtAssignedTo.Value
                LblAssignedTo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Emp_Name FROM TSPL_EMPLOYEE_MASTER WHERE EMP_CODE='" & TxtAssignedTo.Value & "'"))
                TechName.Text = LblAssignedTo.Text
                LblTeleNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Phone FROM TSPL_EMPLOYEE_MASTER WHERE EMP_CODE='" & TxtAssignedTo.Value & "'"))
                LblEmail.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT EMail_ID FROM TSPL_EMPLOYEE_MASTER WHERE EMP_CODE='" & TxtAssignedTo.Value & "'"))
            Else
                LblAssignedTo.Text = ""
                TechName.Text = ""
                LblTechCode.Text = ""
                LblTeleNo.Text = ""
                LblEmail.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtDocType__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)
        Try
            Dim qry As String = ""
            qry = " SELECT CASE WHEN ISNULL(RE_NAME,'') ='' THEN Program_Name ELSE Re_Name END As ScreenName,CASE WHEN Parent_Code='SMSaleNTrans' THEN 'Sale' WHEN Parent_Code='SMPurTrans' THEN 'Purchase' WHEN Parent_Code='SMRecTrans' THEN 'Receivables' WHEN Parent_Code='SMPayTrans' THEN 'Payables' END AS  ModuleName  From TSPL_PROGRAM_MASTER "

            TxtDocType.Value = clsCommon.ShowSelectForm("SWDocType", qry, "ScreenName", " Parent_Code IN ('SMSaleNTrans','SMPurTrans','SMRecTrans','SMPayTrans') ", TxtDocType.Value, "SNo", isButtonClicked)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub TxtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)
        Try
            Dim qry As String = ""
            qry = " SELECT CASE WHEN ISNULL(RE_NAME,'') ='' THEN Program_Name ELSE Re_Name END As ScreenName,CASE WHEN Parent_Code='SMSaleNTrans' THEN 'Sale' WHEN Parent_Code='SMPurTrans' THEN 'Purchase' WHEN Parent_Code='SMRecTrans' THEN 'Receivables' WHEN Parent_Code='SMPayTrans' THEN 'Payables' END AS  ModuleName,SNo  From TSPL_PROGRAM_MASTER  "

            TxtDocType.Value = clsCommon.ShowSelectForm("SWDocType", qry, "ScreenName", " Parent_Code IN ('SMSaleNTrans','SMPurTrans','SMRecTrans','SMPayTrans') ", TxtDocType.Value, "SNo", isButtonClicked)


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtIssueNotice__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtIssueNotice._MYValidating
        Try
            TxtIssueNotice.Value = ClsFaultMaster.GetFinder("", TxtIssueNotice.Value, isButtonClicked)
            If clsCommon.myLen(TxtIssueNotice.Value) > 0 Then
                LblIssueNotice.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Fault_Master_Name FROM TSPL_SW_FAULT_MASTER WHERE Fault_Master_Code='" & TxtIssueNotice.Value & "'"))
            Else
                LblIssueNotice.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Save()
    End Sub

    Private Sub txtcode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtcode._MYNavigator
        Try
            LoadData(txtcode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtcode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtcode._MYValidating
        Dim str As String = "SELECT count(*) FROM TSPL_SW_SERVICE_CALL WHERE Service_Call_Code ='" + txtcode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtcode.MyReadOnly = False
        Else
            txtcode.MyReadOnly = True
        End If

        If txtcode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = ""
            txtcode.Value = ClsServiceCall.GetFinder("", txtcode.Value, isButtonClicked)
            If clsCommon.myLen(txtcode.Value) > 0 Then
                Dim objOT As ClsServiceCall
                objOT = ClsServiceCall.GetData(txtcode.Value, NavigatorType.Current)
                If Not objOT Is Nothing Then
                    LoadData(txtcode.Value, NavigatorType.Current)
                End If
            Else
                AddNew()
            End If
        End If
    End Sub


#Region "Email-Setting"
    'Private Sub SendSMSandEmail(ByVal isSendForApproval As Boolean)
    '    'Try

    '    Dim EmailId As String

    '    Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.FrmServiceCall)

    '    If obj Is Nothing Then
    '        clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
    '        Return
    '    End If
    '    If clsCommon.myLen(obj.mailsubjct) <= 0 Then
    '        clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
    '        Return
    '    End If

    '    Dim strContactPerson As String = ""
    '    Dim strSubject As String = obj.mailsubjct.Replace(clsEmailSMSConstants.Call_No, txtcode.Value)
    '    strSubject = strSubject.Replace(clsEmailSMSConstants.Call_Date, clsCommon.GetPrintDate(dtpDate.Text, "dd/MMM/yyyy"))
    '    Dim strbody As String = obj.mailbody.Replace(clsEmailSMSConstants.App_No, txtcode.Value)
    '    strbody = strbody.Replace(clsEmailSMSConstants.Call_No, txtcode.Value)
    '    strbody = strbody.Replace(clsEmailSMSConstants.Call_Date, clsCommon.GetPrintDate(dtpDate.Text, "dd/MMM/yyyy"))
    '    strbody = strbody.Replace(clsEmailSMSConstants.Problem_Type, TxtProbType.Value)
    '    strbody = strbody.Replace(clsEmailSMSConstants.Subject, TxtSubject.Text)
    '    strbody = strbody.Replace(clsEmailSMSConstants.ItemPartNo, TxtItemPartNo.Value)
    '    strbody = strbody.Replace(clsEmailSMSConstants.IssueNotice, TxtIssueNotice.Value)
    '    strbody = strbody.Replace(clsEmailSMSConstants.AssignedTo, TxtAssignedTo.Value)
    '    strbody = strbody.Replace(clsEmailSMSConstants.AssignedBy, LblAssignedByCode.Text)
    '    strbody = strbody.Replace(clsEmailSMSConstants.Form_Code, MyBase.Form_ID)
    '    If isSendForApproval Then
    '        Dim lstReceiptents As New List(Of String)
    '        If clsCommon.myLen(LblEmail.Text) > 0 Then
    '            EmailId = LblEmail.Text
    '            lstReceiptents.Add(EmailId)
    '            clsMailViaOutlook.SendEmail(strSubject, strbody, lstReceiptents, Nothing, "")
    '        Else
    '            clsCommon.MyMessageBoxShow("Technician Email ID not found", Me.Text)
    '            Return
    '        End If
    '        If ChkSMS.Checked = True Then
    '            SMSSENDONLY(False)
    '        End If
    '    End If
    'End Sub
    Sub MailSend()
        Try
            If clsCommon.myLen(txtcode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Select Call No. First", Me.Text)
                txtcode.Focus()
                txtcode.Select()
                Return
            End If

            If Not (common.clsCommon.MyMessageBoxShow("Send E-Mail/SMS Of Respective Call No. " + txtcode.Value + "" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
                Return
            End If
            Save()
            ' LoadData(txtcode.Value, NavigatorType.Current)
            'SendSMSandEmail(True)
            clsCommon.MyMessageBoxShow("Mail send succussfully")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    'Private Sub SMSSENDONLY(ByVal isPost As Boolean)
    '    Try
    '        Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.FrmServiceCall)
    '        If obj Is Nothing Then
    '            clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
    '            Return
    '        End If


    '        If clsCommon.myLen(obj.smsbody) <= 0 Then
    '            Return
    '        End If

    '        Dim strMes As String = obj.smsbody
    '        If strMes.Contains(clsEmailSMSConstants.Call_No) Then
    '            strMes = strMes.Replace(clsEmailSMSConstants.Call_No, txtcode.Value)
    '        End If
    '        If strMes.Contains(clsEmailSMSConstants.Call_Date) Then
    '            strMes = strMes.Replace(clsEmailSMSConstants.Call_Date, clsCommon.GetPrintDate(dtpDate.Text, "dd/MMM/yyyy"))
    '        End If
    '        If strMes.Contains(clsEmailSMSConstants.Problem_Type) Then
    '            strMes = strMes.Replace(clsEmailSMSConstants.Problem_Type, TxtProbType.Value)
    '        End If
    '        If strMes.Contains(clsEmailSMSConstants.Subject) Then
    '            strMes = strMes.Replace(clsEmailSMSConstants.Subject, TxtSubject.Text)
    '        End If
    '        If strMes.Contains(clsEmailSMSConstants.IssueNotice) Then
    '            strMes = strMes.Replace(clsEmailSMSConstants.IssueNotice, TxtIssueNotice.Value)
    '        End If
    '        If strMes.Contains(clsEmailSMSConstants.AssignedTo) Then
    '            strMes = strMes.Replace(clsEmailSMSConstants.AssignedTo, TxtAssignedTo.Value)
    '        End If
    '        If strMes.Contains(clsEmailSMSConstants.AssignedBy) Then
    '            strMes = strMes.Replace(clsEmailSMSConstants.AssignedBy, LblAssignedByCode.Text)
    '        End If
    '        If strMes.Contains(clsEmailSMSConstants.ItemPartNo) Then
    '            strMes = strMes.Replace(clsEmailSMSConstants.ItemPartNo, TxtItemPartNo.Value)
    '        End If

    '        If clsCommon.myLen(LblTeleNo.Text) > 0 Then
    '            Dim strphone As String = LblTeleNo.Text

    '            If clsSMSSend.SendSMS(clsUserMgtCode.FrmServiceCall, strMes, strphone) Then
    '                If Not isPost Then
    '                    clsCommon.MyMessageBoxShow("SMS Send Successfully", Me.Text)
    '                End If
    '            End If
    '        Else
    '            clsCommon.MyMessageBoxShow("Telephone no. not found to send the SMS", Me.Text)
    '            Return
    '        End If
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Sub
#End Region
    Private Sub rmEmail_Click(sender As Object, e As EventArgs) Handles rmEmail.Click
        Dim frm As New FrmMailSMSSettingNew2()
        frm.FormId = clsUserMgtCode.FrmServiceCall
        frm.ShowDialog()
    End Sub

    Private Sub btnsendmail_Click(sender As Object, e As EventArgs) Handles btnsendmail.Click
        If ChkEmail.Checked = True Then
            MailSend()
        Else
            clsCommon.MyMessageBoxShow("Please check email setting in reminder")
            ChkEmail.Focus()
            Return
        End If

    End Sub

    Private Sub btnnew_Click1(sender As Object, e As EventArgs) Handles btnnew.Click
        AddNew()
    End Sub
End Class