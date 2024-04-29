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
'''' ''''''''''''''''''''''''TicketNo='BM00000001540''''''''''''''''''''''''''''''''''''''''
'''' </summary>
'''' <remarks></remarks>

Public Class frmTimesheet
    Inherits FrmMainTranScreen
    
#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim userCode, companyCode As String

    Const colCode As String = "COLCODE"
    Const colEmpCode As String = "colEmpCode"
    Const colCustCode As String = "colCustCode"
    Const colCustDesc As String = "colCustDesc"
    Const colProjectCode As String = "colProjectCode"
    Const colProjectDesc As String = "COLPROJECTDESC"
    Const colTaskDate As String = "COLTASKDATE"
    Const colFromTime As String = "colFromTime"
    Const colToTime As String = "colToTime"
    Const colWorkHours As String = "colWorkHours"
    Const colWorkDone As String = "colWorkDone"
    Const colJobCode As String = "colJobCode"
    Const colTaskCode As String = "colTaskCode"
    Const colUnitCost As String = "colUnitCost"
    Const colTotalCost As String = "colTotalCost"
    Const colBillingRate As String = "colBillingRate"
    Const colTotalBilling As String = "colTotalBilling"
    Const colisEdited As String = "colisEdited"
    Private isInsideLoadData As Boolean = False
    Private isCellValueChangedOpen As Boolean = False

#End Region
#Region "Functions"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmTimeSheet)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub
    
    Sub SaveData()
        Dim trans As SqlTransaction = Nothing
        Try
            If (AllowToSave()) Then
                trans = clsDBFuncationality.GetTransactin()
                Dim Arr As List(Of clsTimeSheet) = New List(Of clsTimeSheet)()
                Dim obj As New clsTimeSheet()
                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myCBool(grow.Cells(colisEdited).Value) Then
                        'If clsCommon.myLen(grow.Cells(colTaskCode).Value) > 0 Then
                        'If Not clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colStatus).Value), "Open") = CompairStringResult.Equal Then
                        obj = New clsTimeSheet
                        obj.CODE = clsCommon.myCstr(grow.Cells(colCode).Value)
                        obj.TASK_DATE = clsCommon.myCstr(grow.Cells(colTaskDate).Value)
                        obj.EMP_CODE = clsCommon.myCstr(grow.Cells(colEmpCode).Value)
                        obj.CUST_CODE = clsCommon.myCstr(grow.Cells(colCustCode).Value)
                        obj.PROJECT_CODE = clsCommon.myCstr(grow.Cells(colProjectCode).Value)

                        obj.FROM_TIME = clsCommon.myCDate(grow.Cells(colFromTime).Value)
                        obj.TO_TIME = clsCommon.myCDate(grow.Cells(colToTime).Value)
                        obj.WORK_TIME_HOURS = clsCommon.myCdbl(grow.Cells(colWorkHours).Value)
                        obj.WORK_TIME_MINS = clsCommon.myCdbl(grow.Cells(colWorkHours).Tag)
                        obj.WORK_DONE = clsCommon.myCstr(grow.Cells(colWorkDone).Value)
                        obj.JOB_CODE = clsCommon.myCstr(grow.Cells(colJobCode).Value)
                        obj.TASK_CODE = clsCommon.myCstr(grow.Cells(colTaskCode).Value)

                        obj.UNIT_COST = clsCommon.myCdbl(grow.Cells(colUnitCost).Value)
                        obj.TOTAL_COST = clsCommon.myCdbl(grow.Cells(colTotalCost).Value)
                        obj.BILLING_RATE = clsCommon.myCdbl(grow.Cells(colBillingRate).Value)
                        obj.TOTAL_BILLING = clsCommon.myCdbl(grow.Cells(colTotalBilling).Value)
                        Arr.Add(obj)
                        'End If
                        'End If
                    End If
                Next
                ' ''For Custom Fields
                ''Dim obj As New clsTimeSheet()
                'obj = New clsTimeSheet
                'obj.Form_ID = MyBase.Form_ID
                'obj.arrCustomFields = New List(Of clsCustomFieldValues)
                'If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                '    UcCustomFields1.GetData(obj.arrCustomFields)
                'End If
                'If MyBase.ArrDetailFields IsNot Nothing AndAlso MyBase.ArrDetailFields.Count > 0 Then
                '    clsCustomFieldGrid.GetData(obj.arrCustomFields, gv1, MyBase.ArrDetailFields, colCode)
                'End If
                ' ''End of For Custom Fields

                If Arr Is Nothing OrElse Arr.Count <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "No Data found to save", Me.Text)
                ElseIf clsTimeSheet.SaveData(Arr, colCode, gv1, trans) Then
                    
                    trans.Commit()
                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData("", "")
                End If
            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Function AllowToSave() As Boolean
        Try
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myCBool(gv1.Rows(ii).Cells(colisEdited).Value) Then
                    Dim strTaskCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colTaskCode).Value)
                    'If clsCommon.myLen(strTaskCode) > 0 Then
                    'If Not clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colStatus).Value), "Open") = CompairStringResult.Equal Then
                    If clsCommon.myLen(gv1.Rows(ii).Cells(colTaskDate).Value) <= 0 Then
                        clsCommon.MyMessageBoxShow("Please Enter Date at Row No" + clsCommon.myCstr(ii + 1), Me.Text)
                        Return False
                    End If
                    If clsCommon.myLen(gv1.Rows(ii).Cells(colFromTime).Value) <= 0 Then
                        clsCommon.MyMessageBoxShow("Please Enter From Time at Row No" + clsCommon.myCstr(ii + 1), Me.Text)
                        Return False
                    End If
                    If clsCommon.myLen(gv1.Rows(ii).Cells(colToTime).Value) <= 0 Then
                        clsCommon.MyMessageBoxShow("Please Enter To Time at Row No" + clsCommon.myCstr(ii + 1), Me.Text)
                        Return False
                    End If
                    If clsCommon.myCdbl(gv1.Rows(ii).Cells(colWorkHours).Value) <= 0 Then
                        clsCommon.MyMessageBoxShow("Zero or Negative time not allowed at Row No" + clsCommon.myCstr(ii + 1), Me.Text)
                        Return False
                    End If
                    If clsCommon.myLen(gv1.Rows(ii).Cells(colWorkDone).Value) <= 0 Then
                        clsCommon.MyMessageBoxShow("Please Enter Work Done at Row No" + clsCommon.myCstr(ii + 1), Me.Text)
                        Return False
                    End If

                    'End If
                    'End If
                End If
            Next
            UcCustomFields1.AllowToSave()
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
    End Function
    Private Sub DeleteData()
        Dim trans As SqlTransaction = Nothing
        Try
            'trans = clsDBFuncationality.GetTransactin
            'If clsCommon.myLen(txtCode.Value) <= 0 Then
            '    Throw New Exception("  Code not found to delete")

            'End If

            'If clsCommon.MyMessageBoxShow("Do you want to delete  Code '" + txtCode.Value + "'", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
            '    Dim qry As String = "delete from TSPL_PJC_TASK where TASK_CODE='" + txtCode.Value + "'"
            '    clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '    '' custom fields
            '    clsCustomFieldValues.DeleteData(Me.Form_ID, txtCode.Value, trans)
            '    trans.Commit()
            '    clsCommon.MyMessageBoxShow("Successfully Deleted", Me.Text)
            '    AddNew()
            'End If
        Catch ex As Exception
            If (clsCommon.CompairString(clsCommon.myCstr(ex.Message), "Cost Code not found to delete") <> CompairStringResult.Equal) Then
                clsCommon.MyMessageBoxShow("Current Cost Code is in use")

            Else
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End If
            trans.Rollback()
        End Try
    End Sub
    Sub AddNew()
        'txtCode.Value = ""
        'txtCode.MyReadOnly = False
        'txtDesc.Text = ""
        'Me.fnduom.Value = Nothing
        'Me.lblUomName.Text = ""
        'Me.txtBillingRate.Text = 0
        'Me.txtUnitCost.Text = 0
        'Me.fndCostType.Value = Nothing
        'txtCostTypeDesc.Text = ""

        'Me.cboTaskType.DataSource = clsTaskMaster.GetTaskTypeTable
        'Me.cboTaskType.DisplayMember = "Name"
        'Me.cboTaskType.ValueMember = "Code"
        'cboTaskType.SelectedIndex = -1
        'Me.txtJobCode.Text = ""

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

    
   
   
    Private Sub frmTimesheet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        'ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
        ' globalFunc.mandatoryText(fnddesig.txtValue, txtdes)

        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        cboTaskType.DataSource = GetTasType()
        cboTaskType.DisplayMember = "value"
        cboTaskType.ValueMember = "code"
        cboTaskType.SelectedValue = "Non Filled"

        ''For Custom Fields
        RadPageView1.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.Report_ID = MyBase.Form_ID
            UcCustomFields1.LoadCustomControls()
        End If
        ''End of For Custom Fields

        LoadData("", "")
    End Sub

    Public Function GetTasType() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("value", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("code") = "ALL"
        dr("value") = "ALL"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("code") = "Filled"
        dr("value") = "Filled"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("code") = "Non Filled"
        dr("value") = "Non Filled"
        dt.Rows.Add(dr)

        dt.AcceptChanges()

        Return dt
    End Function

    Private Sub frmTimesheet_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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


    
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click

        LoadData("", "")
    End Sub
    Public Sub LoadData(ByVal strTaskType As String, ByRef strDoc As String)
        Try
            LoadBlankGrid()
            Dim emp_code As String
            emp_code = clsTimeSheet.findEmpCode(objCommonVar.CurrentUserCode)
            If clsCommon.myLen(emp_code) = 0 Then
                clsCommon.MyMessageBoxShow("User not mapped to employee !")
                Exit Sub
            End If
            If clsCommon.myLen(strDoc) > 0 Then
                cboTaskType.SelectedValue = strTaskType
            End If
            Dim Arr As List(Of clsTimeSheet) = clsTimeSheet.GetData(emp_code, txtFromDate.Value, txtToDate.Value, cboTaskType.SelectedValue, strDoc)
            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                isInsideLoadData = True
                For Each objTr As clsTimeSheet In Arr
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCode).Value = objTr.CODE
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colEmpCode).Value = objTr.EMP_CODE
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCustCode).Value = objTr.CUST_CODE
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCustDesc).Value = objTr.CUST_DESC
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colProjectCode).Value = objTr.PROJECT_CODE
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colProjectDesc).Value = objTr.PROJECT_DESC

                    If objTr.TASK_DATE.HasValue Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaskDate).Value = objTr.TASK_DATE
                    End If

                    If objTr.FROM_TIME.HasValue Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFromTime).Value = objTr.FROM_TIME
                    End If
                    If objTr.TO_TIME.HasValue Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colToTime).Value = objTr.TO_TIME
                    End If

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colWorkHours).Value = objTr.WORK_TIME_HOURS
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colWorkHours).Tag = objTr.WORK_TIME_MINS

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colWorkDone).Value = objTr.WORK_DONE
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colJobCode).Value = objTr.JOB_CODE
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaskCode).Value = objTr.TASK_CODE

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitCost).Value = objTr.UNIT_COST
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalCost).Value = objTr.TOTAL_COST
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colBillingRate).Value = objTr.BILLING_RATE
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalBilling).Value = objTr.TOTAL_BILLING
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colisEdited).Value = False

                    ' ''For Custom Fields
                    'If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    '    UcCustomFields1.LoadData(objTr.CODE)
                    'End If
                    'clsCustomFieldGrid.FillDataInGrid(objTr.CODE, MyBase.Form_ID, gv1)
                    ' ''End of For Custom Fields
                Next
                isInsideLoadData = False
                gv1.Rows(0).IsSelected = True
                gv1.Rows(0).IsCurrent = True
                gv1.Rows(0).EnsureVisible()
            End If


            'gv1.Rows.AddNew()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCode.FormatString = ""
        repoCode.HeaderText = "Code"
        repoCode.Name = colCode
        repoCode.Width = 0
        repoCode.IsVisible = False
        repoCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCode)

        Dim repoEmpCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoEmpCode.FormatString = ""
        repoEmpCode.HeaderText = "Emp Code"
        repoEmpCode.Name = colEmpCode
        repoEmpCode.Width = 0
        repoEmpCode.IsVisible = False
        repoEmpCode.ReadOnly = True
        repoEmpCode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repoEmpCode)


        Dim repoProjectCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoProjectCode.FormatString = ""
        repoProjectCode.HeaderText = "Project Code"
        repoProjectCode.Name = colProjectCode
        repoProjectCode.Width = 100
        repoProjectCode.ReadOnly = True
        repoProjectCode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repoProjectCode)

        Dim repoProjectDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoProjectDesc.FormatString = ""
        repoProjectDesc.HeaderText = "Project Description"
        repoProjectDesc.Name = colProjectDesc
        repoProjectDesc.Width = 120
        repoProjectDesc.ReadOnly = True
        repoProjectDesc.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repoProjectDesc)


        Dim repoCustCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCustCode.FormatString = ""
        repoCustCode.HeaderText = "Customer Code"
        repoCustCode.Name = colCustCode
        repoCustCode.Width = 100
        repoCustCode.ReadOnly = True
        repoCustCode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repoCustCode)

        Dim repoCustDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCustDesc.FormatString = ""
        repoCustDesc.HeaderText = "Customer Description"
        repoCustDesc.Name = colCustDesc
        repoCustDesc.Width = 120
        repoCustDesc.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCustDesc)

        Dim repoTaskDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoTaskDate.Format = DateTimePickerFormat.Custom
        repoTaskDate.CustomFormat = "dd-MM-yyyy"
        repoTaskDate.HeaderText = "Task Date"
        repoTaskDate.FormatString = "{0:d}"
        repoTaskDate.Name = colTaskDate
        repoTaskDate.WrapText = True
        repoTaskDate.ReadOnly = False
        repoTaskDate.Width = 100
        gv1.MasterTemplate.Columns.Add(repoTaskDate)



        Dim repoFromTime As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoFromTime.Format = DateTimePickerFormat.Custom
        repoFromTime.CustomFormat = "hh:mm tt"
        repoFromTime.HeaderText = "From Time"
        repoFromTime.FormatString = "{0:hh:mm tt}"
        repoFromTime.Name = colFromTime
        repoFromTime.WrapText = True
        repoFromTime.ReadOnly = False
        repoFromTime.Width = 100
        gv1.MasterTemplate.Columns.Add(repoFromTime)

        Dim repoToTime As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoToTime.Format = DateTimePickerFormat.Custom
        repoToTime.CustomFormat = "hh:mm tt"
        repoToTime.HeaderText = "To Time"
        repoToTime.FormatString = "{0:hh:mm tt}"
        repoToTime.Name = colToTime
        repoToTime.WrapText = True
        repoToTime.ReadOnly = False
        repoToTime.Width = 100
        gv1.MasterTemplate.Columns.Add(repoToTime)


        Dim repoWorkHours As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoWorkHours = New GridViewDecimalColumn()
        repoWorkHours.FormatString = ""
        repoWorkHours.HeaderText = "Work Hours"
        repoWorkHours.Name = colWorkHours
        repoWorkHours.IsVisible = True
        repoWorkHours.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoWorkHours.ReadOnly = True
        repoWorkHours.WrapText = True
        repoWorkHours.Width = 70
        gv1.MasterTemplate.Columns.Add(repoWorkHours)

        Dim repoWorkDone As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoWorkDone.FormatString = ""
        repoWorkDone.HeaderText = "Work Done"
        repoWorkDone.Name = colWorkDone
        repoWorkDone.Width = 200
        repoWorkDone.IsVisible = True
        repoWorkDone.WrapText = True
        repoWorkDone.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoWorkDone)

        Dim repoJobCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoJobCode.FormatString = ""
        repoJobCode.HeaderText = "Job code"
        repoJobCode.Name = colJobCode
        repoJobCode.Width = 100
        repoJobCode.IsVisible = True
        repoJobCode.WrapText = True
        repoJobCode.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoJobCode)

        Dim repoTaskCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTaskCode.FormatString = ""
        repoTaskCode.HeaderText = "Task code"
        repoTaskCode.Name = colTaskCode
        repoTaskCode.Width = 100
        repoTaskCode.IsVisible = True
        repoTaskCode.WrapText = True
        repoTaskCode.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoTaskCode)


        Dim repoUnitCost As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoUnitCost.FormatString = ""
        repoUnitCost.HeaderText = "Unit Cost"
        repoUnitCost.Name = colUnitCost
        repoUnitCost.IsVisible = False
        repoUnitCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoUnitCost.ReadOnly = True
        repoUnitCost.WrapText = True
        repoUnitCost.Width = 0
        gv1.MasterTemplate.Columns.Add(repoUnitCost)

        Dim repoTotalCost As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalCost.FormatString = ""
        repoTotalCost.HeaderText = "Total Cost"
        repoTotalCost.Name = colTotalCost
        repoTotalCost.IsVisible = False
        repoTotalCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTotalCost.ReadOnly = True
        repoTotalCost.WrapText = True
        repoTotalCost.Width = 0
        gv1.MasterTemplate.Columns.Add(repoTotalCost)

        Dim repoBillingRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBillingRate.FormatString = ""
        repoBillingRate.HeaderText = "Billing Rate"
        repoBillingRate.Name = colBillingRate
        repoBillingRate.IsVisible = False
        repoBillingRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoBillingRate.ReadOnly = True
        repoBillingRate.WrapText = True
        repoBillingRate.Width = 0
        gv1.MasterTemplate.Columns.Add(repoBillingRate)

        Dim repoTotalBilling As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalBilling.FormatString = ""
        repoTotalBilling.HeaderText = "Total Billing"
        repoTotalBilling.Name = colTotalBilling
        repoTotalBilling.IsVisible = False
        repoTotalBilling.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTotalBilling.ReadOnly = True
        repoTotalBilling.WrapText = True
        repoTotalBilling.Width = 0
        gv1.MasterTemplate.Columns.Add(repoTotalBilling)

        Dim repoisEdit As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoisEdit.FormatString = ""
        repoisEdit.HeaderText = "Is Edit"
        repoisEdit.Name = colisEdited
        repoisEdit.IsVisible = False
        repoisEdit.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        repoisEdit.ReadOnly = True
        repoisEdit.WrapText = True
        repoisEdit.Width = 50
        gv1.MasterTemplate.Columns.Add(repoisEdit)

        clsCustomFieldGrid.LoadBlankGrid(gv1, MyBase.ArrDetailFields)

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = True
        gv1.EnableFiltering = True
        gv1.EnableAlternatingRowColor = True
        gv1.AutoSizeRows = False
        gv1.AllowRowResize = True
        gv1.VerticalScrollState = ScrollState.AlwaysShow
        gv1.HorizontalScrollState = ScrollState.AlwaysShow
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.ShowFilteringRow = True
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            lblTotalRows.Text = "Total Rows: " + clsCommon.myCstr(gv1.MasterView.Rows.Count())
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column.FieldName.StartsWith("_CFLD_") Then
                        clsCustomFieldGrid.getFinderForCustomFieldGrid(gv1, e.Column.Name.ToString, MyBase.Form_ID)
                    End If
                    If gv1.CurrentRow.Index >= 0 Then
                        If (e.Column Is gv1.Columns(colTaskDate) OrElse e.Column Is gv1.Columns(colFromTime) OrElse e.Column Is gv1.Columns(colToTime)) Then
                            If clsCommon.myLen(gv1.CurrentRow.Cells(colTaskDate).Value) <= 0 AndAlso e.Column Is gv1.Columns(colFromTime) Then
                                clsCommon.MyMessageBoxShow("Please select Task Date", Me.Text)
                                gv1.CurrentColumn = gv1.Columns(colTaskDate)
                            End If
                            
                            Try
                                Dim dtTaskDate As DateTime = clsCommon.myCDate(gv1.CurrentRow.Cells(colTaskDate).Value)
                                Dim dtFromTime As DateTime = clsCommon.myCDate(gv1.CurrentRow.Cells(colFromTime).Value)
                                Dim dtToTime As DateTime = clsCommon.myCDate(gv1.CurrentRow.Cells(colToTime).Value)
                                Dim dtFrom As DateTime = New DateTime(dtTaskDate.Year, dtTaskDate.Month, dtTaskDate.Day, dtFromTime.Hour, dtFromTime.Minute, 0)
                                Dim dtTo As DateTime = New DateTime(dtTaskDate.Year, dtTaskDate.Month, dtTaskDate.Day, dtToTime.Hour, dtToTime.Minute, 0)
                                Dim ts As TimeSpan = dtTo.Subtract(dtFrom)
                                gv1.CurrentRow.Cells(colWorkHours).Value = clsCommon.myCdbl(clsCommon.myCstr(ts.Hours & "." + IIf(clsCommon.myLen(ts.Minutes) = 1, "0", "") & clsCommon.myCstr(ts.Minutes)))
                                gv1.CurrentRow.Cells(colWorkHours).Tag = ts.Hours * 60 + ts.Minutes
                                gv1.CurrentRow.Cells(colBillingRate).Value = clsTimeSheet.GetBillingRate(gv1.CurrentRow.Cells(colEmpCode).Value, gv1.CurrentRow.Cells(colCustCode).Value)
                                gv1.CurrentRow.Cells(colTotalBilling).Value = gv1.CurrentRow.Cells(colBillingRate).Value * (gv1.CurrentRow.Cells(colWorkHours).Tag / 60)
                                gv1.CurrentRow.Cells(colUnitCost).Value = clsTimeSheet.GetUnitCost(gv1.CurrentRow.Cells(colEmpCode).Value)
                                gv1.CurrentRow.Cells(colTotalCost).Value = gv1.CurrentRow.Cells(colUnitCost).Value * (gv1.CurrentRow.Cells(colWorkHours).Tag / 60)


                            Catch ex As Exception
                                gv1.CurrentRow.Cells(colWorkHours).Value = 0
                                gv1.CurrentRow.Cells(colWorkHours).Tag = 0
                            End Try
                        ElseIf e.Column Is gv1.Columns(colJobCode) Then
                            OpenJobCodeList(False)
                        ElseIf e.Column Is gv1.Columns(colTaskCode) Then
                            OpenTaskCodeList(False)
                        End If
                        gv1.CurrentRow.Cells(colisEdited).Value = True
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub OpenJobCodeList(ByVal isButtonClick As Boolean)
        Dim qry As String = "select JOB_CODE as Code,DESCRIPTION as Description from TSPL_PJC_JOB"
        gv1.CurrentRow.Cells(colJobCode).Value = clsCommon.myCstr(clsCommon.ShowSelectForm("TSPL_PJC_JOB", qry, "Code", "", clsCommon.myCstr(gv1.CurrentRow.Cells(colJobCode).Value), "Code", isButtonClick))

        'qry = "select  Decription from TSPL_PJC_JOB where JOB_CODE='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colJobCode).Value) + "'"
        'gv1.CurrentRow.Cells(colDelayDescription).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
    End Sub
    Sub OpenTaskCodeList(ByVal isButtonClick As Boolean)
        Dim qry As String = "select TASK_CODE as Code,DESCRIPTION as Description from TSPL_PJC_TASK"
        gv1.CurrentRow.Cells(colTaskCode).Value = clsCommon.myCstr(clsCommon.ShowSelectForm("TSPL_PJC_TASK", qry, "Code", "", clsCommon.myCstr(gv1.CurrentRow.Cells(colTaskCode).Value), "Code", isButtonClick))

        'qry = "select  Decription from TSPL_PJC_JOB where JOB_CODE='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colJobCode).Value) + "'"
        'gv1.CurrentRow.Cells(colDelayDescription).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
    End Sub
End Class
