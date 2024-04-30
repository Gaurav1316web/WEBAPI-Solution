Imports common
Imports System.Data.SqlClient
Imports System

Public Class FrmCapexMaster
    Inherits FrmMainTranScreen

#Region "Variable"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
#End Region

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.capexmaster)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        RadMenuItem1.Enabled = MyBase.isModifyFlag
        RadMenuItem2.Enabled = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmCapexMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isNewEntry = True
        funReset()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        funReset()
    End Sub

    Sub funReset()
        isNewEntry = True
        'txtCode.MyReadOnly = True
        txtCode.Value = Nothing
        txtCode.Focus()
        txtDesc.Text = ""
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = False
        txt_budget.Text = 0
        txt_revisedbudget.Text = 0
        txt_revisionno.Text = 0
        txt_tolerence.Text = 0
        NumIncBudget.Text = 0
        chkProvisional.Checked = False
        txt_budget.ReadOnly = False
        txtdate.Text = DateTime.Now()
        lblcurrentBudget.Text = 0
        EnableDisableSaveButton()
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        'txtCode.MyReadOnly = True
        'btnSave.Enabled = True
        btnDelete.Enabled = True
        isNewEntry = False

        Dim obj As New clsCapexMaster()
        obj = clsCapexMaster.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
            funReset()
            txt_budget.ReadOnly = True
            isNewEntry = False
            btnSave.Text = "Update"
            btnDelete.Enabled = True
            txtCode.Value = obj.Code
            txtDesc.Text = obj.Description
            txt_budget.Text = obj.Budget
            txt_revisedbudget.Text = 0
            txt_revisionno.Text = obj.RevNo
            txt_tolerence.Text = obj.Tolerence
            NumIncBudget.Text = 0
            If clsCommon.myCdbl(obj.RevBudget) <> 0 Then
                lblcurrentBudget.Text = obj.RevBudget
            Else
                lblcurrentBudget.Text = obj.CurrentBudget
            End If
            chkProvisional.Checked = obj.Provisional
            txtdate.Text = obj.DocDate
            EnableDisableSaveButton()

        Else
            funReset()
        End If

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Save()
    End Sub

    Public Sub Save()
        Try
            If AllowToSave() Then
                Dim rbudget As String = Nothing
                Dim revno As String = Nothing

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.capexmaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
                Dim obj As New clsCapexMaster()
                obj.Code = txtCode.Value
                obj.Description = txtDesc.Text
                obj.Budget = clsCommon.myCdbl(txt_budget.Text)
                obj.IncBudget = clsCommon.myCdbl(NumIncBudget.Text)
                obj.RevBudget = clsCommon.myCdbl(txt_revisedbudget.Text)
                obj.Tolerence = clsCommon.myCdbl(txt_tolerence.Text)
                obj.Provisional = chkProvisional.Checked
                obj.RevNo = clsCommon.myCstr(txt_revisionno.Text)

                obj.CurrentBudget = clsCommon.myCdbl(lblcurrentBudget.Text)
                obj.DocDate = clsCommon.GetPrintDate(txtdate.Text)

                If Not isNewEntry Then
                    rbudget = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select tspl_capex_master.Revised_Budget from tspl_capex_master where tspl_capex_master.Code='" + clsCommon.myCstr(obj.Code) + "'", Nothing))
                    revno = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select tspl_capex_master.Revision_No from tspl_capex_master where tspl_capex_master.Code='" + clsCommon.myCstr(obj.Code) + "'", Nothing))
                    If clsCommon.CompairString(clsCommon.myCstr(revno), "") = CompairStringResult.Equal Then
                        revno = 0
                    End If
                    If clsCommon.myCdbl(obj.RevBudget) <> clsCommon.myCdbl(rbudget) Then
                        obj.RevNo = clsCommon.myCstr(clsCommon.myCdbl(revno) + 1)
                        Dim frm As New FrmPWD(Nothing)
                        frm.strType = "Revised Budget"
                        frm.strCode = "Revised Budget"
                        frm.ShowDialog()
                        If frm.isPasswordCorrect Then
                        Else
                            common.clsCommon.MyMessageBoxShow("Budget Not Revised.")
                            Exit Sub
                        End If
                    Else
                        obj.RevNo = clsCommon.myCstr(clsCommon.myCdbl(revno))
                    End If
                End If

                If (clsCapexMaster.SaveData(obj, isNewEntry)) Then
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    LoadData(obj.Code, NavigatorType.Current)
                    btnSave.Text = "Update"
                    btnDelete.Enabled = True
                    txt_budget.ReadOnly = True
                Else
                    btnSave.Text = "Save"
                    btnDelete.Enabled = False
                    txt_budget.ReadOnly = False
                End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        
    End Sub

    Function AllowToSave() As Boolean
        If clsCommon.myLen(txtDesc.Text) <= 0 Then
            myMessages.blankValue(Me, "Description", Me.Text)
            txtDesc.Focus()
            Return False
        End If


        Dim docdate As String = "select Doc_Date  from TSPL_CAPEX_BUDGET_MASTER where Capex_Code ='" & txtCode.Value & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(docdate)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                If clsCommon.GetPrintDate(txtdate.Text, "dd/MMM/yyyy") > clsCommon.GetPrintDate(dr("Doc_Date"), "dd/MMM/yyyy") Then
                    clsCommon.MyMessageBoxShow("Main Capex date cannot be updated more than sub capex date")
                    txtdate.Focus()
                    Return False
                End If
            Next
           
        End If
        
        'Dim Budget As Decimal
        'If clsCommon.myCdbl(txt_revisedbudget.Text) > 0 Then
        '    Budget = clsCommon.myCdbl(txt_revisedbudget.Text)
        'Else
        '    Budget = clsCommon.myCdbl(txt_budget.Text)
        'End If
        'If clsCapexMaster.chkLimitBugetMaster(clsCommon.myCstr(txtCode.Value), clsCommon.myCdbl(Budget), clsCommon.myCdbl(txt_tolerence.Text), "") = False Then
        '    Return False
        'End If
        'If POAmount(clsCommon.myCstr(txtCode.Value), Budget, clsCommon.myCdbl(txt_tolerence.Text)) = False Then
        '    Return False
        'End If
        If clsCapexMaster.ChkAcquisitionEntry(clsCommon.myCstr(txtCode.Value)) = False Then
            clsCommon.MyMessageBoxShow("Project Closed Budget can't be updated")
            Return False
        End If
        If isNewEntry = False Then
            If common.clsCommon.MyMessageBoxShow("Budget amount once saved will not be update.Please re-verify and save", "Receipt Entry", MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.NO Then
                Return False
            End If
        End If

        Return True
    End Function

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        funDelete()
    End Sub

    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsCapexMaster.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub FrmCapexMaster_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            fundelete()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub

    Private Sub RadMenuItem4_Click(sender As Object, e As EventArgs) Handles RadMenuItem4.Click
        Me.Close()
    End Sub

    Private Sub txtCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        'Dim str As String = "select count(*) from TSPL_CAPEX_MASTER where TSPL_CAPEX_MASTER.CODE ='" + txtCode.Value + "' "
        'Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        'If no = 0 AndAlso isButtonClicked = False Then
        '    txtCode.MyReadOnly = True
        'Else
        '    txtCode.MyReadOnly = True
        'End If
        'If txtCode.MyReadOnly OrElse isButtonClicked Then
        txtCode.Value = clsCapexMaster.getFinder("", txtCode.Value, isButtonClicked)
        If txtCode.Value <> "" Then
            LoadData(txtCode.Value, NavigatorType.Current)
        Else
            funReset()
        End If
        'End If
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Dim gv As New RadGridView()
        Dim rbudget As String = Nothing
        Dim revno As String = Nothing
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Code", "Description", "Budget", "Tolerence", "Inc Budget", "Doc Date") Then
            Try
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clsCapexMaster()

                    Dim strCode As String = clsCommon.myCstr(grow.Cells(0).Value)
                    'If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                    '    Throw New Exception("Code can not be blank or incorrect.")
                    'End If
                    obj.Code = strCode

                    Dim strName As String = clsCommon.myCstr(grow.Cells(1).Value)
                    If strName.Length > 100 Or (String.IsNullOrEmpty(strName)) Then
                        Throw New Exception("Description can not be blank or incorrect.")
                    End If
                    obj.Description = strName

                    '=====================Added by preeti gupta==================
                    Dim strBudget As String = clsCommon.myCstr(grow.Cells("Budget").Value)
                    If strBudget.Length > 100 Or (String.IsNullOrEmpty(strBudget)) Then
                        Throw New Exception("Budget can not be blank or incorrect.")
                    End If
                    obj.Budget = clsCommon.myCdbl(strBudget)

                    Dim strIncBudget As String = clsCommon.myCstr(grow.Cells("Inc Budget").Value)
                    obj.IncBudget = clsCommon.myCdbl(strIncBudget)
                    If clsCommon.myCdbl(obj.IncBudget) > 0 Then
                        If clsCommon.myLen(strCode) > 0 Then
                            obj.RevBudget = obj.Budget + obj.IncBudget
                        End If
                    End If
                    'If obj.RevBudget > 0 Then
                    '    obj.Budget = obj.RevBudget
                    'End If
                    Dim strDocDate As String = clsCommon.myCstr(grow.Cells("Doc Date").Value)
                    If clsCommon.myLen(strDocDate) <= 0 Then
                        Throw New Exception("Doc Date can not be blank or incorrect.")
                    End If
                    obj.DocDate = clsCommon.myCDate(strDocDate)
                    'Dim strRevBudget As String = clsCommon.myCstr(grow.Cells("Revised Budget").Value)


                    Dim strTolerence As String = clsCommon.myCstr(grow.Cells("Tolerence").Value)
                    If strTolerence.Length > 100 Or (String.IsNullOrEmpty(strTolerence)) Then
                        Throw New Exception("Tolerence can not be blank or incorrect.")
                    End If
                    obj.Tolerence = clsCommon.myCdbl(strTolerence)

                    If clsCapexMaster.CheckNewEntry(obj.Code) Then
                    Else
                        rbudget = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select tspl_capex_master.Revised_Budget from tspl_capex_master where tspl_capex_master.Code='" + clsCommon.myCstr(obj.Code) + "'", Nothing))
                        revno = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select tspl_capex_master.Revision_No from tspl_capex_master where tspl_capex_master.Code='" + clsCommon.myCstr(obj.Code) + "'", Nothing))
                        If clsCommon.CompairString(clsCommon.myCstr(revno), "") = CompairStringResult.Equal Then
                            revno = 0
                        End If
                        If clsCommon.myCdbl(obj.RevBudget) <> clsCommon.myCdbl(rbudget) Then
                            obj.RevNo = clsCommon.myCstr(clsCommon.myCdbl(revno) + 1)
                            Dim frm As New FrmPWD(Nothing)
                            frm.strType = "Revised Budget"
                            frm.strCode = "Revised Budget"
                            frm.ShowDialog()
                            If frm.isPasswordCorrect Then
                            Else
                                common.clsCommon.MyMessageBoxShow("Budget Not Revised.")
                                Exit Sub
                            End If
                        Else
                            obj.RevNo = clsCommon.myCstr(clsCommon.myCdbl(revno))
                        End If
                    End If

                    '============================================================

                    clsCapexMaster.SaveData(obj, clsCapexMaster.CheckNewEntry(obj.Code))
                Next
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub RadMenuItem3_Click(sender As Object, e As EventArgs) Handles RadMenuItem3.Click
        Dim str As String
        str = "select TSPL_CAPEX_MASTER.CODE AS Code, TSPL_CAPEX_MASTER.DESCRIPTION as [DESCRIPTION],TSPL_CAPEX_MASTER.Budget as [Budget],TSPL_CAPEX_MASTER.Tolerence as [Tolerence],TSPL_CAPEX_MASTER.inc_budget as [Inc Budget],convert(varchar,TSPL_CAPEX_MASTER.Doc_Date,103) as [Doc Date] from TSPL_CAPEX_MASTER"
        transportSql.ExporttoExcel(str, Me)
    End Sub
    Private Sub NumIncBudget_TextChanged(sender As Object, e As EventArgs) Handles NumIncBudget.TextChanged
        If clsCommon.myCdbl(NumIncBudget.Text) <> 0 AndAlso clsCommon.myCdbl(txt_budget.Text) > 0 Then
            If clsCommon.myCdbl(lblcurrentBudget.Text) > 0 Then
                txt_revisedbudget.Text = clsCommon.myCdbl(lblcurrentBudget.Text) + clsCommon.myCdbl(NumIncBudget.Text)
            Else
                txt_revisedbudget.Text = clsCommon.myCdbl(txt_budget.Text) + clsCommon.myCdbl(NumIncBudget.Text)
            End If
            EnableDisableSaveButton()
        Else
            txt_revisedbudget.Text = 0

        End If
    End Sub

    Private Sub txt_budget_TextChanged(sender As Object, e As EventArgs) Handles txt_budget.TextChanged
        If clsCommon.myCdbl(NumIncBudget.Text) = 0 AndAlso clsCommon.myCdbl(txt_budget.Text) > 0 Then

            lblcurrentBudget.Text = txt_budget.Text
        Else
            lblcurrentBudget.Text = 0

        End If
    End Sub
    Sub EnableDisableSaveButton()
        If isNewEntry = False Then
            If clsCommon.myCdbl(txt_revisionno.Text) <> 0 AndAlso clsCommon.myCdbl(NumIncBudget.Text) = 0 Then
                btnSave.Enabled = False
            Else
                btnSave.Enabled = True
            End If
        Else

            btnSave.Enabled = True
        End If
    End Sub
    'Function POAmount(ByVal StrCapexCode As String, ByVal Budget As Decimal, ByVal Tolerence As Decimal) As Boolean
    '    Dim strPoAmount As Decimal = 0
    '    Dim strBudget As Decimal = 0
    '    strPoAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(amt_after_tax ) as PO_AMOUNT from tspl_purchase_order_head where Capex_Code ='" & StrCapexCode & "' "))
    '    strBudget = ((Budget * Tolerence) / 100) + Budget
    '    If strPoAmount > strBudget Then
    '        clsCommon.MyMessageBoxShow(" " & strPoAmount & " Budget already used in PO for the Capex")
    '        Return False
    '    Else
    '        Return True
    '    End If
    '    Return True
    'End Function

    'Function ChkAcquisitionEntry(ByVal StrCapexCode As String) As Boolean
    '    Dim Count As Decimal = 0

    '    Count = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) as net_Amt from TSPL_ACQUISITION_DETAIL" & _
    '                         " left join TSPL_ACQUISITION_head on TSPL_ACQUISITION_DETAIL.Acquisition_Code =TSPL_ACQUISITION_head.Acquisition_Code" & _
    '                         " where TSPL_ACQUISITION_DETAIL.capex_code='" & StrCapexCode & "' and TSPL_ACQUISITION_head.status=1"))

    '    If Count > 0 Then
    '        clsCommon.MyMessageBoxShow("Project Closed Budget can't be updated")
    '        Return False

    '    Else
    '        Return True
    '    End If
    '    Return True
    'End Function

   

End Class
