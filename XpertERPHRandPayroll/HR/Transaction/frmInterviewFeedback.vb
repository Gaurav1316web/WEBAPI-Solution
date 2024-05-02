' ----------------- Created By Anubhooti On 13-Aug-2014 Against -------------------- '
'Ticket No- BHA/28/09/18-000574  Resolved insert null error
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
Imports XpertERPEngine
Imports System.IO

Public Class FrmInterviewFeedback
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Dim isInsideLoadData As Boolean = False
    Dim isFlag As Boolean = False
    Dim ComboLoad As Boolean = False
    Dim IsFO As Boolean = False
    Dim IsSaving As Boolean = False

#Region "Rating Details"
    Dim ratingColumn As New GridViewRatingColumn()

    Const ColParCode As String = "Parameter Code"
    Const ColParName As String = "Parameter Name"
    Const ColRating As String = "Rating"

    Const ColRoundCode As String = "Round Code"
    Const ColRoundName As String = "Round Name"
    Const ColAction As String = "Action"
    Const ColStartTime As String = "Start Time"
    Const ColEndTime As String = "End Time"
    Const ColIsNR As String = "IsNR"
#End Region
#Region "Functions"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmInterviewFeedback)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        btnpost.Visible = MyBase.isPostFlag
    End Sub
    Sub LoadBlankGrid()
        gvparameter.Rows.Clear()
        gvparameter.Columns.Clear()

        Dim ParCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ParCode = New GridViewTextBoxColumn()
        ParCode.FormatString = ""
        ParCode.HeaderText = "Parameter Code"
        ParCode.Name = ColParCode
        ParCode.Width = 200
        'ParCode.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
        'ParCode.TextImageRelation = TextImageRelation.TextBeforeImage
        ParCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvparameter.MasterTemplate.Columns.Add(ParCode) '0

        Dim ParName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ParName.FormatString = ""
        ParName.HeaderText = "Parameter Name"
        ParName.Name = ColParName
        ParName.Width = 710
        ParName.ReadOnly = True
        ParName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvparameter.MasterTemplate.Columns.Add(ParName) '1

        ratingColumn.HeaderText = "Rating"
        ratingColumn.Name = ColRating
        ratingColumn.Width = 150
        ratingColumn.ReadOnly = False
        ratingColumn.SelectionMode = RatingSelectionMode.FullItem
        ratingColumn.Minimum = 0
        ratingColumn.Maximum = 10
        gvparameter.MasterTemplate.Columns.Add(ratingColumn)

        gvparameter.AllowDeleteRow = False
        gvparameter.AllowAddNewRow = False
        gvparameter.ShowGroupPanel = False
        gvparameter.AllowColumnReorder = False
        gvparameter.AllowRowReorder = False
        gvparameter.EnableSorting = False
        gvparameter.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvparameter.MasterTemplate.ShowRowHeaderColumn = False
        gvparameter.TableElement.TableHeaderHeight = 40

        ParCode = Nothing
        ParName = Nothing
    End Sub

    Sub LoadBlankGridRound()
        gvround.Rows.Clear()
        gvround.Columns.Clear()

        Dim RoundCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        RoundCode = New GridViewTextBoxColumn()
        RoundCode.FormatString = ""
        RoundCode.HeaderText = "Round Code"
        RoundCode.Name = ColRoundCode
        RoundCode.Width = 300
        'RoundCode.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
        'RoundCode.TextImageRelation = TextImageRelation.TextBeforeImage
        RoundCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvround.MasterTemplate.Columns.Add(RoundCode) '0

        Dim RoundName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        RoundName.FormatString = ""
        RoundName.HeaderText = "Round Name"
        RoundName.Name = ColRoundName
        RoundName.Width = 610
        RoundName.ReadOnly = True
        RoundName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvround.MasterTemplate.Columns.Add(RoundName) '1

        Dim Action As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Action.HeaderText = "Action"
        Action.Name = ColAction
        Action.Width = 150
        Action.ReadOnly = True
        Action.IsVisible = True
        Action.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvround.MasterTemplate.Columns.Add(Action)

        Dim ST As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        ST.FormatString = "{0:dd/MM/yyyy hh:mm tt}"
        ST.HeaderText = "StartTime"
        ST.Name = ColStartTime
        ST.Width = 150
        ST.ReadOnly = True
        ST.IsVisible = False
        ST.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvround.MasterTemplate.Columns.Add(ST)

        Dim ET As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        ET.FormatString = "{0:dd/MM/yyyy hh:mm tt}"
        ET.HeaderText = "EndTime"
        ET.Name = ColEndTime
        ET.Width = 150
        ET.ReadOnly = True
        ET.IsVisible = False
        ET.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvround.MasterTemplate.Columns.Add(ET)

        Dim IsNR As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        IsNR.HeaderText = "IsNR"
        IsNR.Name = ColIsNR
        IsNR.Width = 100
        IsNR.ReadOnly = True
        IsNR.IsVisible = False
        IsNR.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvround.MasterTemplate.Columns.Add(IsNR)

        gvround.AllowDeleteRow = False
        gvround.AllowAddNewRow = False
        gvround.ShowGroupPanel = False
        gvround.AllowColumnReorder = False
        gvround.AllowRowReorder = False
        gvparameter.EnableSorting = False
        gvround.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvround.MasterTemplate.ShowRowHeaderColumn = False
        gvround.TableElement.TableHeaderHeight = 40

        RoundCode = Nothing
        RoundName = Nothing
        Action = Nothing
    End Sub
    Function AllowToSave() As Boolean
        Try
            btnsave.Focus()
            If clsCommon.myLen(txtcode.Value) <= 0 Then
                txtcode.Focus()
                Throw New Exception("Applicant code can not be left blank")
            ElseIf clsCommon.myLen(cmbAction.Text) <= 0 Then
                cmbAction.Focus()
                Throw New Exception("Action can not be left blank")
            ElseIf clsCommon.myCstr(cmbAction.SelectedValue = "FO") AndAlso clsCommon.myLen(CmbFinalAction.Text) <= 0 Then
                CmbFinalAction.Focus()
                Throw New Exception("Final action can not be left blank")
            ElseIf clsCommon.myCstr(cmbAction.SelectedValue = "R") AndAlso (dtpStartTime.Value > dtpEndTime.Value) Then
                dtpStartTime.Focus()
                Throw New Exception("Please check ! start date time can not be more than from end date time.")
            ElseIf clsCommon.myLen(txtremark1.Text) <= 0 Then
                txtremark1.Focus()
                Throw New Exception("Remarks can not be left blank")
            End If
            '' Rounds -----------------------------------
            Dim GridRowR As Integer = 0
            For Each grow As GridViewRowInfo In gvround.Rows
                If clsCommon.myLen(grow.Cells(ColRoundCode).Value) > 0 Then
                    If clsCommon.myLen(grow.Cells(ColRoundName).Value) <= 0 Then
                        Throw New Exception("Round name can not be left blank for round (" + clsCommon.myCstr(grow.Cells(ColRoundCode).Value) + ")")
                    End If
                    GridRowR = GridRowR + 1
                End If
            Next

            If GridRowR <= 0 Then
                Throw New Exception("Enter at least one round detail")
            End If
            '' End Rounds -------------------------------
            If clsCommon.CompairString(cmbAction.SelectedValue, "R") <> CompairStringResult.Equal Then
                Dim GridRow As Integer = 0
                For Each grow As GridViewRowInfo In gvparameter.Rows
                    If clsCommon.myLen(grow.Cells(ColParCode).Value) > 0 Then
                        If clsCommon.myLen(grow.Cells(ColRating).Value) <= 0 Then
                            Throw New Exception("Please rate candidate with respect to parameters")
                        End If
                        GridRow = GridRow + 1
                    End If
                Next

                If GridRow <= 0 Then
                    Throw New Exception("Enter at least one parameter detail")
                End If
            End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return True
    End Function
    Sub DeleteData()
        If clsCommon.myLen(txtcode.Value) <= 0 Or clsCommon.myLen(txtroundcode.Text) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "code not found to delete", Me.Text)
            Exit Sub
        End If

        funDelete()
    End Sub
    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (ClsInterviewFeedback.DeleteData(txtFeedbackCode.Text, txtcode.Value, txtroundcode.Text)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Sub funReset()
        isNewEntry = True
        txtcode.MyReadOnly = False
        txtcode.Value = Nothing
        txtFeedbackCode.Text = ""
        txtremark1.Text = ""
        txtcomments.Text = ""
        txtroundcode.Text = ""
        txttotalscore.Value = 0
        txtclearingscore.Value = 0
        txtscore.Value = 0
        txtcode.Focus()

        Me.cmbAction.DisplayMember = "Name"
        Me.cmbAction.ValueMember = "Code"

        Me.cmbAction.DataSource = ClsInterviewFeedback.GetAction()

        Me.CmbFinalAction.DataSource = ClsInterviewFeedback.GetFA()
        Me.CmbFinalAction.DisplayMember = "Name"
        Me.CmbFinalAction.ValueMember = "Code"
        ComboLoad = True
        Me.gvparameter.Rows.Clear()
        Me.gvparameter.Rows.AddNew()

        Me.gvround.Rows.Clear()
        Me.gvround.Rows.AddNew()
        UsLock1.Status = ERPTransactionStatus.Pending
        UcRequisitionDetail1.AppCode = ""
        UcRequisitionDetail1.RefreshData()
        LblDec.Text = "Pending"
        GrpBoxPar.Visible = False
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btnpost.Enabled = False
        btndelete.Enabled = False
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)

        Try
            txtcode.MyReadOnly = True
            btnsave.Enabled = True
            btndelete.Enabled = True
            btnpost.Enabled = True
            isNewEntry = False
            Dim Parameter_Name As String = String.Empty
            Dim obj As New ClsInterviewFeedback()
            obj = ClsInterviewFeedback.GetData(strCode, NavTyep)

            FillRoundList(False)

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Applicant_Code) > 0) Then
                funReset()
                isNewEntry = False
                btnsave.Text = "Update"
                btndelete.Enabled = True
                btnpost.Enabled = True
                If clsCommon.myLen(obj.Final_Action) > 0 Then
                    If clsCommon.CompairString(obj.Final_Action, "H") = CompairStringResult.Equal Then
                        btnpost.Visible = True
                        LblDec.Text = "HIRED"
                    ElseIf clsCommon.CompairString(obj.Final_Action, "OH") = CompairStringResult.Equal Then
                        btnpost.Visible = False
                        LblDec.Text = "ON HOLD"
                    ElseIf clsCommon.CompairString(obj.Final_Action, "R") = CompairStringResult.Equal Then
                        btnpost.Visible = False
                        LblDec.Text = "REJECTED"
                    End If
                Else
                    LblDec.Text = "NOT DECLARED"
                End If
                If obj.Posted = ERPTransactionStatus.Approved Then
                    btnsave.Enabled = False
                    btnpost.Enabled = False
                    btndelete.Enabled = False
                End If
                UsLock1.Status = obj.Posted
                txtFeedbackCode.Text = obj.Feedback_Code
                txtcode.Value = obj.Applicant_Code

                CmbFinalAction.SelectedValue = obj.Final_Action

                UcRequisitionDetail1.AppCode = txtcode.Value
                UcRequisitionDetail1.RefreshData()

                txtcode.MyReadOnly = True
                FillRoundList(False)
                Dim ii As Int16 = 0
                If obj.ObjList IsNot Nothing AndAlso obj.ObjList.Count > 0 Then
                    LoadBlankGrid()
                    GrpBoxPar.Visible = True
                    For Each objTr As ClsInterviewFeedbackDetail In obj.ObjList
                        gvparameter.Rows.AddNew()
                        ii = ii + 1
                        gvparameter.Rows(gvparameter.Rows.Count - 1).Cells(ColParCode).Value = objTr.Parameter_Code
                        gvparameter.Rows(gvparameter.Rows.Count - 1).Cells(ColRating).Value = objTr.Rating

                        Dim ParameterName As String = clsDBFuncationality.getSingleValue("Select Parameter_Name From TSPL_HR_PARAMETER_MASTER Where Parameter_Code ='" + objTr.Parameter_Code + "'")
                        If clsCommon.myLen(ParameterName) > 0 Then
                            gvparameter.Rows(gvparameter.Rows.Count - 1).Cells(ColParName).Value = ParameterName
                        End If
                        txtclearingscore.Value = objTr.Clearing_Score
                        txtscore.Value = objTr.Score
                        txttotalscore.Value = objTr.Total_Score
                        txtpercentage.Value = objTr.Percentage
                        txtremark1.Text = objTr.Remarks
                        txtcomments.Text = objTr.Comments
                    Next
                End If
                FillParameterList(False, clsCommon.myCstr(gvround.CurrentRow.Cells(ColRoundCode).Value), clsCommon.myCstr(gvround.CurrentRow.Cells(ColStartTime).Value), clsCommon.myCstr(gvround.CurrentRow.Cells(ColEndTime).Value), False)
                CalculateTotalScore(False)
                CalculatePercentage(False)
                CalculateClearScore(False, clsCommon.myCstr(gvround.CurrentRow.Cells("Round Code").Value))
            Else
                isNewEntry = True
                'funReset()
                UsLock1.Status = ERPTransactionStatus.Pending
                ' Me.gvparameter.Rows.Clear()
                'Me.gvparameter.Rows.AddNew()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    Sub LoadDataForNav(ByVal strCode As String, ByVal NavTyep As NavigatorType)

        Try
            txtcode.MyReadOnly = True
            btnsave.Enabled = True
            btndelete.Enabled = True
            btnpost.Enabled = True
            isNewEntry = False
            Dim Parameter_Name As String = String.Empty
            Dim obj As New ClsInterviewFeedback()
            obj = ClsInterviewFeedback.GetDataForNav(strCode, NavTyep)

            FillRoundList(False)

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Applicant_Code) > 0) Then
                funReset()
                isNewEntry = False
                btnsave.Text = "Update"
                btndelete.Enabled = True
                btnpost.Enabled = True
                If clsCommon.myLen(obj.Final_Action) > 0 Then
                    If clsCommon.CompairString(obj.Final_Action, "H") = CompairStringResult.Equal Then
                        btnpost.Visible = True
                        LblDec.Text = "HIRED"
                    ElseIf clsCommon.CompairString(obj.Final_Action, "OH") = CompairStringResult.Equal Then
                        btnpost.Visible = False
                        LblDec.Text = "ON HOLD"
                    ElseIf clsCommon.CompairString(obj.Final_Action, "R") = CompairStringResult.Equal Then
                        btnpost.Visible = False
                        LblDec.Text = "REJECTED"
                    End If
                Else
                    LblDec.Text = "NOT DECLARED"
                End If
                If obj.Posted = ERPTransactionStatus.Approved Then
                    btnsave.Enabled = False
                    btnpost.Enabled = False
                    btndelete.Enabled = False
                End If
                UsLock1.Status = obj.Posted
                txtFeedbackCode.Text = obj.Feedback_Code
                txtcode.Value = obj.Applicant_Code

                CmbFinalAction.SelectedValue = obj.Final_Action

                UcRequisitionDetail1.AppCode = txtcode.Value
                UcRequisitionDetail1.RefreshData()

                txtcode.MyReadOnly = True
                FillRoundList(False)
                Dim ii As Int16 = 0
                If obj.ObjList IsNot Nothing AndAlso obj.ObjList.Count > 0 Then
                    LoadBlankGrid()
                    gvparameter.Visible = True
                    For Each objTr As ClsInterviewFeedbackDetail In obj.ObjList
                        gvparameter.Rows.AddNew()
                        ii = ii + 1
                        gvparameter.Rows(gvparameter.Rows.Count - 1).Cells(ColParCode).Value = objTr.Parameter_Code
                        gvparameter.Rows(gvparameter.Rows.Count - 1).Cells(ColRating).Value = objTr.Rating

                        Dim ParameterName As String = clsDBFuncationality.getSingleValue("Select Parameter_Name From TSPL_HR_PARAMETER_MASTER Where Parameter_Code ='" + objTr.Parameter_Code + "'")
                        If clsCommon.myLen(ParameterName) > 0 Then
                            gvparameter.Rows(gvparameter.Rows.Count - 1).Cells(ColParName).Value = ParameterName
                        End If
                        txtclearingscore.Value = objTr.Clearing_Score
                        txtscore.Value = objTr.Score
                        txttotalscore.Value = objTr.Total_Score
                        txtpercentage.Value = objTr.Percentage
                        txtremark1.Text = objTr.Remarks
                        txtcomments.Text = objTr.Comments
                    Next
                End If
                FillParameterList(False, clsCommon.myCstr(gvround.CurrentRow.Cells(ColRoundCode).Value), clsCommon.myCstr(gvround.CurrentRow.Cells(ColStartTime).Value), clsCommon.myCstr(gvround.CurrentRow.Cells(ColEndTime).Value), False)
                CalculateTotalScore(False)
                CalculatePercentage(False)
                CalculateClearScore(False, clsCommon.myCstr(gvround.CurrentRow.Cells("Round Code").Value))
            Else
                isNewEntry = True
                'funReset()
                UsLock1.Status = ERPTransactionStatus.Pending
                ' Me.gvparameter.Rows.Clear()
                'Me.gvparameter.Rows.AddNew()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    '' ------------------------------------- NAV LOADATA() ----------------------------------------------------------------
    Public Sub Save()
        Dim UpdateQry As String = String.Empty
        Dim FOExists As Boolean = False

        If AllowToSave() Then
            Dim arr As New List(Of ClsInterviewFeedback)
            Dim obj As New ClsInterviewFeedback()
            obj.Feedback_Code = clsCommon.myCstr(txtFeedbackCode.Text)
            obj.Applicant_Code = txtcode.Value
            obj.Final_Action = CmbFinalAction.SelectedValue
            obj.Round_Code = txtroundcode.Text

            If cmbAction.SelectedValue = "R" Then
                ' For Reschedule
                UpdateQry = "UPDATE TSPL_HR_INTERVIEW_SCHEDULE_DETAIL SET Start_Time ='" + clsCommon.GetPrintDate(dtpStartTime.Value, "dd/MMM/yyyy hh:mm tt") + "', End_Time = '" + clsCommon.GetPrintDate(dtpEndTime.Value, "dd/MMM/yyyy hh:mm tt") + "'  WHERE APPLICANT_CODE ='" + clsCommon.myCstr(txtcode.Value) + "' AND Round_Code ='" + txtroundcode.Text + "'"
                clsDBFuncationality.ExecuteNonQuery(UpdateQry)
            End If
            'Else
            obj.ObjList = New List(Of ClsInterviewFeedbackDetail)
            For Each grow As GridViewRowInfo In gvparameter.Rows
                Dim objTr As New ClsInterviewFeedbackDetail()
                If clsCommon.CompairString(cmbAction.SelectedValue, "NR") = CompairStringResult.Equal Or clsCommon.CompairString(cmbAction.SelectedValue, "FO") = CompairStringResult.Equal Then
                    If clsCommon.myCdbl(grow.Cells(ColRating).Value) > 0 Then
                        objTr.Feedback_Code = clsCommon.myCstr(Me.txtFeedbackCode.Text)
                        objTr.Applicant_Code = txtcode.Value
                        objTr.Parameter_Code = clsCommon.myCstr(grow.Cells(ColParCode).Value)
                        objTr.Rating = clsCommon.myCdbl(grow.Cells(ColRating).Value)
                        objTr.Round_Code = txtroundcode.Text
                        objTr.Round_Action = cmbAction.SelectedValue
                        '' Next Round 19-Aug
                        If clsCommon.CompairString(objTr.Round_Action, "NR") = CompairStringResult.Equal Then
                            objTr.IsNR = "Y"
                        Else
                            objTr.IsNR = "N"
                        End If
                        'If clsCommon.CompairString(objTr.Round_Action, "FO") = CompairStringResult.Equal Then
                        For Each grow1 As GridViewRowInfo In gvround.Rows
                            If clsCommon.CompairString(clsCommon.myCstr(grow1.Cells(ColAction).Value), "Final Opinion") = CompairStringResult.Equal AndAlso clsCommon.CompairString(objTr.Round_Action, "FO") = CompairStringResult.Equal Then
                                FOExists = True
                                obj.Final_Action = CmbFinalAction.SelectedValue
                            Else
                                obj.Final_Action = ""
                            End If
                        Next

                        objTr.Remarks = txtremark1.Text
                        objTr.Comments = txtcomments.Text
                        objTr.Total_Score = txttotalscore.Value
                        objTr.Score = txtscore.Value
                        objTr.Clearing_Score = txtclearingscore.Value
                        objTr.Percentage = txtpercentage.Value

                        If FOExists = True Then
                            obj.Final_Action = CmbFinalAction.SelectedValue
                        Else
                            obj.Final_Action = ""
                        End If
                        obj.ObjList.Add(objTr)
                    End If
                Else
                    objTr.Feedback_Code = clsCommon.myCstr(Me.txtFeedbackCode.Text)
                    objTr.Applicant_Code = txtcode.Value
                    objTr.Round_Code = txtroundcode.Text
                    objTr.Round_Action = cmbAction.SelectedValue
                    If clsCommon.CompairString(objTr.Round_Action, "NR") = CompairStringResult.Equal Then
                        objTr.IsNR = "Y"
                        obj.Final_Action = ""
                    Else
                        objTr.IsNR = "N"
                        obj.Final_Action = ""
                    End If
                    objTr.Remarks = txtremark1.Text
                    objTr.Comments = txtcomments.Text
                    obj.ObjList.Add(objTr)
                End If
            Next
            'End If
            arr.Add(obj)

            If (ClsInterviewFeedback.SaveData(arr)) Then
                If Not isFlag Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(obj.Applicant_Code, NavigatorType.Current)
                    btnsave.Text = "Update"
                    btndelete.Enabled = True
                    'FillParameterList(False, clsCommon.myCstr(txtroundcode.Text), clsCommon.myCstr(gvround.CurrentRow.Cells(ColStartTime).Value), clsCommon.myCstr(gvround.CurrentRow.Cells(ColEndTime).Value), False)
                    'CalculateTotalScore(False)
                    'CalculatePercentage(False)
                    'CalculateClearScore(False, clsCommon.myCstr(gvround.CurrentRow.Cells("Round Code").Value))
                    gvround.CurrentRow.IsSelected = True

                Else
                    clsCommon.MyMessageBoxShow(Me, "Data posted successfully", Me.Text)
                End If
                IsSaving = True
            Else
                btnsave.Text = "Save"
                btndelete.Enabled = False
                IsSaving = False
            End If
            obj = Nothing
        End If
    End Sub
    Sub PostData()
        Try
            Dim msg As String = String.Empty
            Dim qry As String = String.Empty
            Dim dt As DataTable = Nothing
            Dim Feedback_Code As String = String.Empty
            isFlag = True
            If clsCommon.myLen(txtcode.Value) > 0 Then
                Feedback_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Feedback_Code from TSPL_HR_INTERVIEW_FEEDBACK where Feedback_Code='" + txtFeedbackCode.Text + "'"))
                If clsCommon.myLen(Feedback_Code) > 0 Then
                    If (myMessages.postConfirm()) Then
                        Save()
                        If (ClsInterviewFeedback.PostData(MyBase.Form_ID, txtFeedbackCode.Text)) Then
                            'msg = "Successfully Posted"
                            'common.clsCommon.MyMessageBoxShow(msg)
                            LoadData(txtcode.Value, NavigatorType.Current)
                        End If
                    End If
                Else
                    Throw New Exception("You cannot post this entry before entering feedback code")
                End If

            Else
                Throw New Exception("feedback code not found to Post")
            End If
            'isFlag = False
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isFlag = False
        End Try
    End Sub
    Sub CalculateTotalScore(ByVal isButtonClick As Boolean)
        Dim TotalRow As Integer
        For i As Integer = 0 To gvparameter.Rows.Count - 1
            If clsCommon.myLen(gvparameter.Rows(i).Cells("Parameter Code").Value) > 0 Then
                TotalRow = i + 1
            End If
        Next
        txttotalscore.Value = TotalRow * 10

    End Sub
    Sub CalculateClearScore(ByVal isButtonClick As Boolean, ByVal RoundCode As String)
        Dim Clearing_Score As String = clsDBFuncationality.getSingleValue("Select Clearing_Score From TSPL_HR_ROUND_MASTER Where Round_Code ='" + RoundCode + "'")
        If clsCommon.myLen(Clearing_Score) > 0 Then
            txtclearingscore.Value = (txttotalscore.Value * Clearing_Score / 100)
        End If
    End Sub
    Sub CalculateRatingScore(ByVal isButtonClick As Boolean)
        Dim TotalRating As Integer
        For i As Integer = 0 To gvparameter.Rows.Count - 1
            If clsCommon.myLen(gvparameter.Rows(i).Cells("Rating").Value) > 0 Then
                TotalRating = TotalRating + clsCommon.myCdbl(gvparameter.Rows(i).Cells("Rating").Value)
            End If
        Next
        'txtscore.Value = clsCommon.myCdbl(TotalRating / 10)
        txtscore.Value = clsCommon.myCdbl(TotalRating)
    End Sub
    Sub FillParameterList(ByVal isButtonClick As Boolean, ByVal RoundCode As String, ByVal StartTime As String, ByVal EndTime As String, ByVal RatingReadOnly As Boolean)
        Dim qry As String = "select Round_Code As Code from TSPL_HR_INTERVIEW_SCHEDULE_DETAIL"
        If clsCommon.myLen(txtcode.Value) > 0 Then

            '' Clearing_Score From Round Master 01-Sep-2014 Changes
            'Dim Clearing_Score As String = clsDBFuncationality.getSingleValue("Select Clearing_Score From TSPL_HR_ROUND_MASTER Where Round_Code ='" + RoundCode + "'")
            'If clsCommon.myLen(Clearing_Score) > 0 Then
            '    txtclearingscore.Value = Clearing_Score
            'End If

            GrpBoxPar.Visible = True
            txtroundcode.Text = RoundCode
            Dim Round_Name As String = clsDBFuncationality.getSingleValue("Select Round_Name From TSPL_HR_ROUND_MASTER Where Round_Code ='" + RoundCode + "'")
            If clsCommon.myLen(Round_Name) > 0 Then
                lblround.Text = Round_Name
            End If
            If clsCommon.myLen(StartTime) > 0 Then
                dtpStartTime.Value = StartTime
            End If
            If clsCommon.myLen(EndTime) > 0 Then
                dtpEndTime.Value = EndTime
            End If

            'dtpEndTime.Value = EndTime
            '' Filling Parameters
            Dim ii As Int16 = 0
            Dim ParQry As String
            Dim DBPar As Integer = clsDBFuncationality.getSingleValue("Select Count(Parameter_Code) As Parameter_Code From TSPL_HR_INTERVIEW_FEEDBACK_DETAIL Where Feedback_Code ='" + txtFeedbackCode.Text + "' AND Round_Code ='" + RoundCode + "' AND Applicant_Code ='" + txtcode.Value + "'")
            If DBPar > 0 Then
                ParQry = "Select Parameter_Code From TSPL_HR_INTERVIEW_FEEDBACK_DETAIL Where Feedback_Code ='" + txtFeedbackCode.Text + "' AND Round_Code ='" + RoundCode + "' AND Applicant_Code ='" + txtcode.Value + "'"
                ratingColumn.ReadOnly = RatingReadOnly
            Else
                ParQry = "select * from TSPL_HR_ROUND_DETAIL Where Round_Code='" + RoundCode + "'"
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(ParQry)

            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                gvparameter.Rows.Clear()
                ' funReset()
                For Each dr As DataRow In dt.Rows
                    gvparameter.Rows.AddNew()
                    ii = ii + 1
                    gvparameter.Rows(gvparameter.Rows.Count - 1).Cells(ColParCode).Value = clsCommon.myCstr(dr("Parameter_Code"))
                    Dim ParameterName As String = clsDBFuncationality.getSingleValue("Select Parameter_Name From TSPL_HR_PARAMETER_MASTER Where Parameter_Code ='" + clsCommon.myCstr(dr("Parameter_Code")) + "'")
                    If clsCommon.myLen(ParameterName) > 0 Then
                        gvparameter.Rows(gvparameter.Rows.Count - 1).Cells(ColParName).Value = ParameterName
                    End If

                Next

                Dim j As Int16 = 0
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("Select Round_Action,Rating,TSPL_HR_INTERVIEW_FEEDBACK_DETAIL.Remarks,TSPL_HR_INTERVIEW_FEEDBACK_DETAIL.Comments,TSPL_HR_INTERVIEW_FEEDBACK_DETAIL.Total_Score,TSPL_HR_INTERVIEW_FEEDBACK_DETAIL.Clearing_Score,TSPL_HR_INTERVIEW_FEEDBACK_DETAIL.Score,TSPL_HR_INTERVIEW_FEEDBACK_DETAIL.Percentage,TSPL_HR_INTERVIEW_FEEDBACK.Final_Action FROM TSPL_HR_INTERVIEW_FEEDBACK left outer join TSPL_HR_INTERVIEW_FEEDBACK_DETAIL on TSPL_HR_INTERVIEW_FEEDBACK_DETAIL.APPLICANT_CODE=TSPL_HR_INTERVIEW_FEEDBACK.APPLICANT_CODE Where TSPL_HR_INTERVIEW_FEEDBACK_DETAIL.Round_Code ='" + RoundCode + "' AND TSPL_HR_INTERVIEW_FEEDBACK_DETAIL.Applicant_Code ='" + txtcode.Value + "'")
                If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                    'gvparameter.Rows.Clear()
                    For Each dr1 As DataRow In dt1.Rows
                        'gvparameter.Rows.AddNew()
                        j = j + 1
                        txtremark1.Text = clsCommon.myCstr(dr1("Remarks"))
                        txtcomments.Text = clsCommon.myCstr(dr1("Comments"))
                        cmbAction.SelectedValue = clsCommon.myCstr(dr1("Round_Action"))
                        CmbFinalAction.SelectedValue = clsCommon.myCstr(dr1("Final_Action"))
                        txttotalscore.Text = clsCommon.myCdbl(dr1("Total_Score"))
                        txtclearingscore.Text = clsCommon.myCdbl(dr1("Clearing_Score"))
                        txtscore.Text = clsCommon.myCdbl(dr1("Score"))
                        txtpercentage.Text = clsCommon.myCdbl(dr1("percentage"))
                        gvparameter.Rows(j - 1).Cells(ColRating).Value = clsCommon.myCdbl(dr1("Rating"))
                        If cmbAction.SelectedValue = "R" Then
                            dtpStartTime.Visible = True
                            dtpEndTime.Visible = True
                            LblStartDate.Visible = True
                            LblEndTime.Visible = True
                            gvparameter.Enabled = False
                            LblResch.Visible = True
                            'gvparameter.Columns("Rating").ReadOnly = True
                        ElseIf cmbAction.SelectedValue = "FO" Then
                            LblFinalAction.Visible = True
                            CmbFinalAction.Visible = True
                            gvparameter.Enabled = True
                        Else
                            dtpStartTime.Visible = False
                            dtpEndTime.Visible = False
                            LblStartDate.Visible = False
                            LblFinalAction.Visible = False
                            CmbFinalAction.Visible = False
                            LblEndTime.Visible = False
                            gvparameter.Enabled = True
                            LblResch.Visible = False
                            'gvparameter.Columns("Rating").ReadOnly = False
                        End If
                        'gvparameter.Rows(gvparameter.Rows.Count - 1).Cells(ColRating).Value = clsCommon.myCdbl(dr1("Rating"))

                    Next
                Else
                    dtpStartTime.Visible = False
                    dtpEndTime.Visible = False
                    LblStartDate.Visible = False
                    LblEndTime.Visible = False
                    LblFinalAction.Visible = False
                    CmbFinalAction.Visible = False
                    cmbAction.SelectedValue = "NR"
                    gvparameter.Enabled = True
                    LblResch.Visible = False
                    'gvparameter.Columns("Rating").ReadOnly = False
                End If

                GrpBoxPar.Visible = True
                gvparameter.Columns(ColParCode).ReadOnly = True
                gvparameter.Columns(ColParName).ReadOnly = True
                ' gvparameter.MasterTemplate.Columns("Rating").ReadOnly = RatingReadOnly

            Else
                clsCommon.MyMessageBoxShow(Me, "No Parameters found", Me.Text)
            End If
        End If
    End Sub
    '' Filling Rounds
    Sub FillRoundList(ByVal isButtonClick As Boolean)
        Dim qry As String = "select Round_Code As Code,Start_Time,End_Time from TSPL_HR_INTERVIEW_SCHEDULE_DETAIL"
        If clsCommon.myLen(txtcode.Value) > 0 Then
            '' Filling Rounds
            Dim ParQry As String = String.Empty
            Dim ii As Int16 = 0
            Dim FirstRow As Integer = 0
            If isNewEntry = True Then
                ParQry = "select Round_Code As Code,Start_Time,End_Time from TSPL_HR_INTERVIEW_SCHEDULE_DETAIL Where APPLICANT_CODE='" + txtcode.Value + "' Order By S_No"
            Else
                ParQry = "select Round_Code As Code,Start_Time,End_Time from TSPL_HR_INTERVIEW_SCHEDULE_DETAIL Where APPLICANT_CODE='" + txtcode.Value + "'"
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(ParQry)

            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                gvround.Rows.Clear()
                LoadBlankGridRound()
                For Each dr As DataRow In dt.Rows
                    gvround.Rows.AddNew()
                    FirstRow = gvround.Rows.Count
                    ii = ii + 1

                    gvround.Rows(gvround.Rows.Count - 1).Cells(ColRoundCode).Value = clsCommon.myCstr(dr("Code"))
                    gvround.Rows(gvround.Rows.Count - 1).Cells(ColStartTime).Value = clsCommon.myCstr(dr("Start_Time"))
                    gvround.Rows(gvround.Rows.Count - 1).Cells(ColEndTime).Value = clsCommon.myCstr(dr("End_Time"))
                    Dim Round_Name As String = clsDBFuncationality.getSingleValue("Select Round_Name From TSPL_HR_ROUND_MASTER Where Round_Code ='" + clsCommon.myCstr(dr("Code")) + "'")
                    If clsCommon.myLen(Round_Name) > 0 Then
                        gvround.Rows(gvround.Rows.Count - 1).Cells(ColRoundName).Value = Round_Name
                    End If
                    'Dim Action As String = clsDBFuncationality.getSingleValue("Select Top 1 Action from TSPL_HR_INTERVIEW_FEEDBACK left outer join TSPL_HR_INTERVIEW_FEEDBACK_DETAIL on TSPL_HR_INTERVIEW_FEEDBACK_DETAIL.APPLICANT_CODE=TSPL_HR_INTERVIEW_FEEDBACK.APPLICANT_CODE  Where TSPL_HR_INTERVIEW_FEEDBACK_DETAIL.Round_Code ='" + clsCommon.myCstr(dr("Code")) + "'")

                    Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("Select Top 1 CASE WHEN ISNULL(Round_Action,'') = 'R' THEN 'Reschedule' WHEN ISNULL(Round_Action,'') = 'NR' THEN 'Next Round' WHEN ISNULL(Round_Action,'') = 'FO' THEN 'Final Opinion' END Round_Action,IsNR from TSPL_HR_INTERVIEW_FEEDBACK_DETAIL   Where TSPL_HR_INTERVIEW_FEEDBACK_DETAIL.Round_Code ='" + clsCommon.myCstr(dr("Code")) + "' AND TSPL_HR_INTERVIEW_FEEDBACK_DETAIL.Applicant_Code ='" + txtcode.Value + "'")

                    If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                        'gvround.Rows.Clear()
                        'For Each dr1 As DataRow In dt1.Rows
                        'gvround.Rows.AddNew()
                        ii = ii + 1
                        gvround.Rows(gvround.Rows.Count - 1).Cells(ColAction).Value = clsCommon.myCstr(dt1.Rows(0)("Round_Action"))
                        gvround.Rows(gvround.Rows.Count - 1).Cells(ColIsNR).Value = clsCommon.myCstr(dt1.Rows(0)("IsNR"))

                        If clsCommon.myCstr(dt1.Rows(0)("Round_Action")) = "R" Then
                            dtpStartTime.Visible = True
                            dtpEndTime.Visible = True
                            LblStartDate.Visible = True
                            LblEndTime.Visible = True
                            dtpStartTime.Value = clsCommon.myCstr(dr("Start_Time"))
                            dtpEndTime.Value = clsCommon.myCstr(dr("End_Time"))
                            gvparameter.Enabled = False
                            LblResch.Visible = True
                        ElseIf clsCommon.myCstr(dt1.Rows(0)("Round_Action")) = "FO" Then
                            LblFinalAction.Visible = True
                            CmbFinalAction.Visible = True
                            gvparameter.Enabled = True
                        Else
                            dtpStartTime.Visible = False
                            dtpEndTime.Visible = False
                            LblStartDate.Visible = False
                            LblEndTime.Visible = False
                            LblFinalAction.Visible = False
                            CmbFinalAction.Visible = False
                            dtpStartTime.Value = Nothing
                            dtpEndTime.Value = Nothing
                            gvparameter.ReadOnly = True
                            LblResch.Visible = False
                        End If

                        'Next
                    End If

                    'Dim Action As String = clsDBFuncationality.getSingleValue("Select Top 1 Round_Action from TSPL_HR_INTERVIEW_FEEDBACK_DETAIL   Where TSPL_HR_INTERVIEW_FEEDBACK_DETAIL.Round_Code ='" + clsCommon.myCstr(dr("Code")) + "' AND TSPL_HR_INTERVIEW_FEEDBACK_DETAIL.Applicant_Code ='" + txtcode.Value + "'")
                    'If clsCommon.myLen(Action) > 0 Then

                    '    gvround.Rows(gvround.Rows.Count - 1).Cells(ColAction).Value = Action
                    '    If Action = "R" Then
                    '        dtpStartTime.Visible = True
                    '        dtpEndTime.Visible = True
                    '        LblStartDate.Visible = True
                    '        LblEndTime.Visible = True
                    '        dtpStartTime.Value = clsCommon.myCstr(dr("Start_Time"))
                    '        dtpEndTime.Value = clsCommon.myCstr(dr("End_Time"))
                    '        gvparameter.Enabled = False
                    '        'gvparameter.Columns("Rating").ReadOnly = True
                    '    ElseIf Action = "FO" Then
                    '        LblFinalAction.Visible = True
                    '        CmbFinalAction.Visible = True
                    '        gvparameter.Enabled = True
                    '    Else
                    '        dtpStartTime.Visible = False
                    '        dtpEndTime.Visible = False
                    '        LblStartDate.Visible = False
                    '        LblEndTime.Visible = False
                    '        LblFinalAction.Visible = False
                    '        CmbFinalAction.Visible = False
                    '        dtpStartTime.Value = Nothing
                    '        dtpEndTime.Value = Nothing
                    '        gvparameter.ReadOnly = True
                    '        'gvparameter.Columns("Rating").ReadOnly = False
                    '    End If
                    'End If
                Next


                gvround.Columns("Round Code").ReadOnly = True
                gvround.Columns("Round Name").ReadOnly = True
            End If

            'Else
            '    clsCommon.MyMessageBoxShow("First select applicant code")
        End If
    End Sub
    Sub FinalOpinionRow(ByVal Current As String)
        Dim RoundCode As String = String.Empty
        RoundCode = txtroundcode.Text
        For row As Integer = 0 To gvround.RowCount - 1
            If gvround.Rows(row).Cells(ColAction).Value.ToString().ToUpper().Equals(RoundCode) Then
            End If
        Next
    End Sub
    Sub CalculatePercentage(ByVal isButtonClick As Boolean)
        Dim TotalScore As Integer
        Dim Score As Integer
        Dim Percentage As Integer
        TotalScore = txttotalscore.Value
        Score = txtscore.Value
        If TotalScore > 0 Then
            Percentage = (Score / TotalScore * 100)
            txtpercentage.Value = Percentage
        End If

    End Sub
#End Region
#Region "Events"



#End Region

    Private Sub FrmInterviewFeedback_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnnew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnsave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnpost.Enabled Then
            PostData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub

    Private Sub FrmInterviewFeedback_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        LoadBlankGrid()
        LoadBlankGridRound()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
        ButtonToolTip.SetToolTip(btnpost, "Press Alt+P Post Transaction")
        funReset()
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Private Sub txtcode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtcode._MYNavigator
        'Try
        '    LoadData(txtcode.Value, NavType)
        'Catch ex As Exception
        '    common.clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
        'End Try
        ' Dim IntrAppCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select APPLICANT_CODE As Code from TSPL_HR_INTERVIEW_SCHEDULE"))
        Dim obj As New ClsInterviewSchedule()

        Try
            Dim qst As String = "select count(*) from TSPL_HR_APPLICANT_ENTRY where APPLICANT_CODE='" + txtcode.Value + "' AND Posted =1"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtcode.MyReadOnly = False
            Else
                txtcode.MyReadOnly = True
            End If
            Dim ISAppCode As Integer
            Dim AppCode As String = clsCommon.myCstr(txtcode.Value)
            obj = ClsInterviewSchedule.GetPostedData(AppCode, NavType)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Applicant_Code) > 0) Then
                ISAppCode = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select COUNT(*) From TSPL_HR_INTERVIEW_FEEDBACK WHERE APPLICANT_CODE='" + obj.Applicant_Code + "'"))
                'If ISAppCode = 0 Then
                '    txtcode.Value = clsCommon.myCstr(obj.Applicant_Code)
                '    UcRequisitionDetail1.AppCode = obj.Applicant_Code
                '    UcRequisitionDetail1.RefreshData()
                '    FillRoundList(False)
                'End If
                txtcode.Value = clsCommon.myCstr(obj.Applicant_Code)
                UcRequisitionDetail1.AppCode = obj.Applicant_Code
                UcRequisitionDetail1.RefreshData()
                If ISAppCode > 0 Then
                    LoadDataForNav(txtcode.Value, NavType)
                Else
                    isNewEntry = True

                    Me.gvround.Rows.Clear()
                    Me.gvround.Rows.AddNew()
                    Me.gvparameter.Rows.Clear()
                    Me.GrpBoxPar.Visible = False
                    'Me.gvparameter.Rows.AddNew()
                    txtroundcode.Text = ""
                    lblround.Text = ""
                    txtremark1.Text = ""
                    txttotalscore.Value = 0
                    txtclearingscore.Value = 0
                    txtscore.Value = 0
                    txtpercentage.Value = 0
                    btnsave.Text = "Save"
                    txtFeedbackCode.Text = ""
                    UsLock1.Status = ERPTransactionStatus.Pending
                    FillRoundList(False)
                    LoadData(txtcode.Value, NavigatorType.Current)
                End If

            End If
            'LoadData(txtcode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing

        End Try
    End Sub

    Private Sub txtcode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtcode._MYValidating
        'Dim str As String = "select count(*) from TSPL_HR_INTERVIEW_FEEDBACK where Feedback_Code ='" + txtcode.Value + "' "
        'Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        'If no = 0 AndAlso isButtonClicked = False Then
        '    txtcode.MyReadOnly = False
        'Else
        '    txtcode.MyReadOnly = True
        'End If
        'If txtcode.MyReadOnly OrElse isButtonClicked Then
        '    Dim qry As String = ""
        '    qry = "Select Feedback_Code As [Code],Applicant_Code As [Applicant Code],Round_Code As [Round Code] from TSPL_HR_INTERVIEW_FEEDBACK"
        '    txtcode.Value = clsCommon.ShowSelectForm("TSPL_HR_INTERVIEW_FEEDBACK", qry, "Code", "", txtcode.Value, "TSPL_HR_INTERVIEW_FEEDBACK.Feedback_Code", isButtonClicked)
        '    If clsCommon.myLen(txtcode.Value) > 0 Then
        '        Dim objOT As ClsInterviewFeedback
        '        objOT = ClsInterviewFeedback.GetData(txtcode.Value, NavigatorType.Current)
        '        If Not objOT Is Nothing Then
        '            LoadData(txtcode.Value, NavigatorType.Current)
        '        End If
        '    Else
        '        funReset()
        '    End If
        'End If

        '' App Code
        Dim IntrAppCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select APPLICANT_CODE As Code from TSPL_HR_INTERVIEW_SCHEDULE"))
        ' Dim qry As String = "select APPLICANT_CODE As Code from TSPL_HR_INTERVIEW_SCHEDULE"
        ' txtcode.Value = clsCommon.ShowSelectForm("AppCode", qry, "Code", "Posted = 1", txtcode.Value, "CODE", isButtonClicked)

        txtcode.Value = ClsInterviewSchedule.GetFinder(" ", txtcode.Value, isButtonClicked)
        If clsCommon.myLen(txtcode.Value) > 0 Then
            UcRequisitionDetail1.AppCode = txtcode.Value
            UcRequisitionDetail1.RefreshData()
            FillRoundList(False)
            LoadData(txtcode.Value, NavigatorType.Current)
        Else
            UcRequisitionDetail1.AppCode = ""
            UcRequisitionDetail1.RefreshData()
            funReset()
        End If
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
        clsERPFuncationality.closeForm(Me)
    End Sub

    Private Sub btndelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnnew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnnew.Click
        funReset()
    End Sub

    Private Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Save()
    End Sub

    Private Sub cmbAction_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAction.SelectedValueChanged
        Dim RSCounter As Integer
        If ComboLoad = True Then


            If clsCommon.CompairString(cmbAction.SelectedValue, "R") = CompairStringResult.Equal Then
                LblStartDate.Visible = True
                dtpStartTime.Visible = True
                LblEndTime.Visible = True
                dtpEndTime.Visible = True
                gvparameter.Enabled = False
                LblResch.Visible = True
                txttotalscore.Value = 0
                txtscore.Value = 0
                txtpercentage.Value = 0
                'gvparameter.Columns("Rating").ReadOnly = True
                txtscore.Value = 0
                txttotalscore.Value = 0
                txtclearingscore.Value = 0
                txtpercentage.Value = 0
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("Select DISTINCT COUNT(*) OVER () AS TotalRecords,(Select Round_name From TSPL_HR_ROUND_MASTER Where Round_Code =TSPL_HR_INTERVIEW_FEEDBACK_DETAIL.Round_Code ) AS RoundName  FRom TSPL_HR_INTERVIEW_FEEDBACK_DETAIL  Group By APPLICANT_CODE,Feedback_Code,Round_Code,Round_Action having Feedback_Code ='" + clsCommon.myCstr(txtFeedbackCode.Text) + "' and APPLICANT_CODE ='" + clsCommon.myCstr(txtcode.Value) + "' and Round_Action ='R'")

                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    RSCounter = clsCommon.myCdbl(dt.Rows(0)("TotalRecords"))
                    If RSCounter > 0 Then
                        LblResch.Text = "Round (" + clsCommon.myCstr(dt.Rows(0)("RoundName")) + ") rescheduled " + clsCommon.myCstr(RSCounter) + " times."
                    Else
                        LblResch.Text = ""
                    End If
                End If

            Else
                LblStartDate.Visible = False
                dtpStartTime.Visible = False
                LblEndTime.Visible = False
                dtpEndTime.Visible = False
                gvparameter.Enabled = True
                LblResch.Visible = False
                'gvparameter.Columns("Rating").ReadOnly = False
                FillParameterList(False, clsCommon.myCstr(gvround.CurrentRow.Cells("Round Code").Value), clsCommon.myCstr(gvround.CurrentRow.Cells(ColStartTime).Value), clsCommon.myCstr(gvround.CurrentRow.Cells(ColEndTime).Value), False)
                CalculateTotalScore(False)
                CalculatePercentage(False)
                CalculateClearScore(False, clsCommon.myCstr(gvround.CurrentRow.Cells("Round Code").Value))
            End If
            If clsCommon.CompairString(cmbAction.SelectedValue, "FO") = CompairStringResult.Equal Then
                CmbFinalAction.Visible = True
                LblFinalAction.Visible = True
                IsFO = True
            Else
                CmbFinalAction.Visible = False
                LblFinalAction.Visible = False
                gvround.Enabled = True
                IsFO = False
            End If
        End If
    End Sub

    Private Sub gvparameter_CellClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvparameter.CellClick

    End Sub

    Private Sub gvparameter_CellDoubleClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvparameter.CellDoubleClick
        Dim TotalRating As Integer
        'For i As Integer = 0 To gvparameter.Rows.Count - 1
        '    If clsCommon.myLen(gvparameter.Rows(i).Cells("Rating").Value) > 0 Then
        '        TotalRating = TotalRating + clsCommon.myCdbl(gvparameter.Rows(i).Cells("Rating").Value)
        '    End If
        'Next
        If clsCommon.myLen(gvparameter.CurrentRow.Cells("Rating").Value) > 0 Then
            TotalRating = TotalRating + clsCommon.myCdbl(gvparameter.CurrentRow.Cells("Rating").Value)
        End If
        If TotalRating > 0 AndAlso TotalRating <= 2 Then
            gvparameter.CurrentRow.Cells("Rating").Value = 0
        End If
    End Sub



    'Private Sub txtroundcode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtroundcode._MYValidating
    '    Dim qry As String = "select Round_Code As Code from TSPL_HR_INTERVIEW_SCHEDULE_DETAIL"
    '    If clsCommon.myLen(txtappcode.Value) > 0 Then
    '        If isNewEntry = True Then
    '            txtroundcode.Value = clsCommon.ShowSelectForm("Round_Code", qry, "Code", "APPLICANT_CODE = '" + txtappcode.Value + "'", txtroundcode.Value, "Code", isButtonClicked)
    '            If clsCommon.myLen(txtroundcode.Value) > 0 Then
    '                LblAppName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Round_Name as Name FROM TSPL_HR_ROUND_MASTER Where Round_Code='" + txtroundcode.Value + "'"))
    '            Else
    '                LblAppName.Text = ""
    '            End If
    '            '' Filling Parameters
    '            Dim ii As Int16 = 0
    '            Dim ParQry As String = "select * from TSPL_HR_ROUND_DETAIL Where Round_Code='" + txtroundcode.Value + "'"
    '            Dim dt As DataTable = clsDBFuncationality.GetDataTable(ParQry)

    '            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
    '                gvparameter.Rows.Clear()
    '                ' gvparameter.RowCount = dt.Rows.Count
    '                'For i As Integer = 0 To gvparameter.Rows.Count - 1
    '                For Each dr As DataRow In dt.Rows
    '                    gvparameter.Rows.AddNew()
    '                    ii = ii + 1
    '                    gvparameter.Rows(gvparameter.Rows.Count - 1).Cells(ColParCode).Value = clsCommon.myCstr(dr("Parameter_Code"))
    '                    Dim ParameterName As String = clsDBFuncationality.getSingleValue("Select Parameter_Name From TSPL_HR_PARAMETER_MASTER Where Parameter_Code ='" + clsCommon.myCstr(dr("Parameter_Code")) + "'")
    '                    If clsCommon.myLen(ParameterName) > 0 Then
    '                        gvparameter.Rows(gvparameter.Rows.Count - 1).Cells(ColParName).Value = ParameterName
    '                    End If
    '                Next
    '                'Next
    '                gvparameter.Columns("Parameter Code").ReadOnly = True
    '                gvparameter.Columns("Parameter Name").ReadOnly = True
    '            End If
    '        End If
    '    Else
    '        clsCommon.MyMessageBoxShow("First select applicant code")
    '    End If
    'End Sub

    Private Sub gvparameter_CellValueChanged1(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvparameter.CellValueChanged
        Try
            If isInsideLoadData = False Then
                If e.Column Is gvparameter.Columns(ColParCode) Then
                    CalculateTotalScore(False)
                    CalculatePercentage(False)
                    CalculateClearScore(False, clsCommon.myCstr(gvround.CurrentRow.Cells("Round Code").Value))
                ElseIf e.Column Is gvparameter.Columns(ColRating) Then
                    CalculateRatingScore(False)
                    CalculatePercentage(False)
                    CalculateClearScore(False, clsCommon.myCstr(gvround.CurrentRow.Cells("Round Code").Value))
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gvparameter_CurrentColumnChanged1(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvparameter.CurrentColumnChanged
        'If gvparameter.RowCount > 0 Then
        '    Dim intCurrRow As Integer = gvparameter.CurrentRow.Index
        '    If intCurrRow = gvparameter.Rows.Count - 1 Then
        '        gvparameter.Rows.AddNew()
        '        gvparameter.CurrentRow = gvparameter.Rows(intCurrRow)
        '    End If
        'End If
    End Sub

    Private Sub gvround_CellClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvround.CellClick
        'Dim DBRound As String
        'For Each grow As GridViewRowInfo In gvround.Rows
        '    If grow.Cells(ColAction).Value = "NR" Then
        '        grow.Cells(ColRoundCode).ReadOnly = False
        '    End If
        'Next

    End Sub

    Private Sub gvround_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvround.CurrentColumnChanged

    End Sub

    Private Sub gvround_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvround.DoubleClick
        Dim IsRow As Integer = 0
        Dim IsRound As String = String.Empty
        Dim DBRound As String = String.Empty
        Dim CurrentRound As String = String.Empty
        Dim Index As Integer = 0

        'Index = gvround.CurrentRow.Index
        'Dim TotalRowIndex As Integer = gvround.Rows.Count

        'If Index < TotalRowIndex - 1 Then
        '    If gvround.Rows(Index + 1).Cells(ColIsNR).Value = "" Then
        '        Dim LastEntry As String = clsDBFuncationality.getSingleValue("Select Round_Code From TSPL_HR_INTERVIEW_FEEDBACK_DETAIL Where TSPL_HR_INTERVIEW_FEEDBACK_DETAIL.Round_Code ='" + clsCommon.myCstr(gvround.CurrentRow.Cells("Round Code").Value) + "' AND TSPL_HR_INTERVIEW_FEEDBACK_DETAIL.Applicant_Code ='" + txtcode.Value + "'")
        '        If LastEntry = clsCommon.myCstr(gvround.CurrentRow.Cells("Round Code").Value) Then
        '            FillParameterList(False, clsCommon.myCstr(gvround.CurrentRow.Cells("Round Code").Value), clsCommon.myCstr(gvround.CurrentRow.Cells(ColStartTime).Value), clsCommon.myCstr(gvround.CurrentRow.Cells(ColEndTime).Value), True)
        '            CalculateTotalScore(False)
        '            CalculatePercentage(False)
        '        End If
        '    End If
        'End If

        'If gvround.Rows(Index).Cells(ColIsNR).Value = "Y" Then
        '    FillParameterList(False, clsCommon.myCstr(gvround.CurrentRow.Cells("Round Code").Value), clsCommon.myCstr(gvround.CurrentRow.Cells(ColStartTime).Value), clsCommon.myCstr(gvround.CurrentRow.Cells(ColEndTime).Value), True)
        '    CalculateTotalScore(False)
        '    CalculatePercentage(False)
        'End If
        'If Index > 0 Then
        '    If gvround.Rows(Index - 1).Cells(ColIsNR).Value = "Y" Then
        IsFO = False
        For i As Integer = 0 To gvround.Rows.Count - 1
            If clsCommon.myLen(gvround.Rows(i).Cells(ColRoundCode).Value) > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(gvround.Rows(i).Cells(ColAction).Value), "Final Opinion") = CompairStringResult.Equal Then
                    IsFO = True
                End If
            End If
        Next
        If IsFO = False OrElse clsCommon.CompairString(clsCommon.myCstr(gvround.CurrentRow.Cells(ColAction).Value), "Final Opinion") = CompairStringResult.Equal Then
            FillParameterList(False, clsCommon.myCstr(gvround.CurrentRow.Cells("Round Code").Value), clsCommon.myCstr(gvround.CurrentRow.Cells(ColStartTime).Value), clsCommon.myCstr(gvround.CurrentRow.Cells(ColEndTime).Value), False)
            CalculateTotalScore(False)
            CalculatePercentage(False)
            CalculateClearScore(False, clsCommon.myCstr(gvround.CurrentRow.Cells("Round Code").Value))
        End If
        '    End If
        'End If
        'Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("Select IsNR from TSPL_HR_INTERVIEW_FEEDBACK_DETAIL   Where TSPL_HR_INTERVIEW_FEEDBACK_DETAIL.Round_Code ='" + clsCommon.myCstr(gvround.CurrentRow.Cells("Round Code").Value) + "' AND TSPL_HR_INTERVIEW_FEEDBACK_DETAIL.Applicant_Code ='" + txtcode.Value + "'")

        'If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then

        'Else
        '    CurrentRound = clsCommon.myCstr(gvround.CurrentRow.Cells("Round Code").Value)
        '    If CurrentRound = clsCommon.myCstr(gvround.Rows(0).Cells("Round Code").Value) Then
        '        FillParameterList(False, clsCommon.myCstr(gvround.CurrentRow.Cells("Round Code").Value), clsCommon.myCstr(gvround.CurrentRow.Cells(ColStartTime).Value), clsCommon.myCstr(gvround.CurrentRow.Cells(ColEndTime).Value), False)
        '        CalculateTotalScore(False)
        '        CalculatePercentage(False)
        '    End If
        'End If
    End Sub


    Private Sub gvround_RowFormatting(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.RowFormattingEventArgs) Handles gvround.RowFormatting

        'If IsFO = True Then
        '    If clsCommon.CompairString(cmbAction.SelectedValue, "FO") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(e.RowElement.RowInfo.Cells(ColRoundCode).Value), txtroundcode.Text) = CompairStringResult.Equal Then
        '        e.RowElement.DrawFill = True
        '        e.RowElement.GradientStyle = GradientStyles.Solid
        '        e.RowElement.BackColor = Color.Aquamarine
        '    End If
        'End If
        If e.RowElement.RowInfo.Cells(ColAction).Value = "Next Round" Then
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = Color.LightPink
        ElseIf e.RowElement.RowInfo.Cells(ColAction).Value = "Reschedule" Then
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = Color.Thistle
        ElseIf e.RowElement.RowInfo.Cells(ColAction).Value = "Final Opinion" Then
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = Color.Aquamarine
        End If

    End Sub


    Private Sub btnpost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnpost.Click
        PostData()
    End Sub

    Private Sub CmbFinalAction_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles CmbFinalAction.SelectedIndexChanged
        If clsCommon.CompairString(cmbAction.SelectedValue, "FO") = CompairStringResult.Equal AndAlso clsCommon.CompairString(CmbFinalAction.SelectedValue, "H") = CompairStringResult.Equal Then
            btnpost.Visible = True
        Else
            btnpost.Visible = False
        End If
    End Sub
End Class
