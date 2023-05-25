'' Work to be done agaist ticket no. UDL/01/08/18-000210 by Parteek
Imports common
Imports System.Data.SqlClient
Imports System

Public Class FrmCapexBudget
    Inherits FrmMainTranScreen

#Region "Variable"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
#End Region



    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.capexbudget)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        RadMenuItem1.Enabled = MyBase.isModifyFlag
        RadMenuItem2.Enabled = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub FrmCapexBudget_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            funDelete()
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
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        'Dim str As String = "select count(*) from TSPL_CAPEX_BUDGET_MASTER where TSPL_CAPEX_BUDGET_MASTER.CODE ='" + txtCode.Value + "' "
        'Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        'If no = 0 AndAlso isButtonClicked = False Then
        '    txtCode.MyReadOnly = True
        'Else
        '    txtCode.MyReadOnly = True
        'End If
        'If txtCode.MyReadOnly OrElse isButtonClicked Then
        txtCode.Value = clsCapexBudget.getFinder("", txtCode.Value, isButtonClicked)
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
        If transportSql.importExcel(gv, "Code", "Description", "Capex Code", "Budget", "Tolerence", "Date", "Inc Budget") Then
            Try
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clsCapexBudget()
                    rbudget = 0
                    revno = 0
                    Dim strCode As String = clsCommon.myCstr(grow.Cells("Code").Value)
                    'If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                    '    Throw New Exception("Code can not be blank or incorrect.")
                    'End If
                    obj.Code = strCode

                    Dim strDesc As String = clsCommon.myCstr(grow.Cells("Description").Value)
                    obj.Description = strDesc

                    Dim strCapex As String = clsCommon.myCstr(grow.Cells("Capex Code").Value)
                    If clsCommon.CompairString(clsCommon.myCstr(strCapex), "") <> CompairStringResult.Equal Then
                        Dim dt As DataTable
                        dt = clsDBFuncationality.GetDataTable("select * from TSPL_CAPEX_MASTER where Code='" + clsCommon.myCstr(strCapex) + "'")
                        If dt.Rows.Count <= 0 Then
                            Throw New Exception("Capex Code not exists in capex master.")
                        End If
                    End If
                    obj.CapexCode = strCapex

                    Dim strBudget As String = clsCommon.myCstr(grow.Cells("Budget").Value)
                    If strBudget.Length > 100 Or (String.IsNullOrEmpty(strBudget)) Then
                        Throw New Exception("Budget can not be blank or incorrect.")
                    End If
                    obj.Budget = clsCommon.myCdbl(strBudget)

                    obj.CurrentBudget = clsCommon.myCdbl(lblcurrentBudget.Text)
                    Dim strIncBudget As String = clsCommon.myCstr(grow.Cells("Inc Budget").Value)
                    obj.IncBudget = clsCommon.myCdbl(strIncBudget)
                    If clsCommon.myCdbl(obj.IncBudget) > 0 Then
                        If clsCommon.myLen(strCode) > 0 Then
                            obj.RevBudget = obj.Budget + obj.IncBudget
                        End If
                    End If


                    'Dim strRevBudget As String = clsCommon.myCstr(grow.Cells("Revised Budget").Value)
                    'obj.RevBudget = clsCommon.myCdbl(strRevBudget)

                    Dim strTolerence As String = clsCommon.myCstr(grow.Cells("Tolerence").Value)
                    If strTolerence.Length > 100 Or (String.IsNullOrEmpty(strTolerence)) Then
                        Throw New Exception("Tolerence can not be blank or incorrect.")
                    End If
                    obj.Tolerence = clsCommon.myCdbl(strTolerence)

                    Dim strDate As String = clsCommon.myCstr(grow.Cells("Date").Value)
                    If strDate.Length > 100 Or (String.IsNullOrEmpty(strDate)) Then
                        Throw New Exception("Document Date can not be blank or incorrect.")
                    End If
                    obj.DocDate = clsCommon.GetPrintDate(strDate, "dd/MMM/yyyy hh:mm tt")

                    If clsCapexBudget.CheckNewEntry(obj.Code) Then
                    Else
                        rbudget = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select TSPL_CAPEX_BUDGET_MASTER.Revised_Budget from TSPL_CAPEX_BUDGET_MASTER where TSPL_CAPEX_BUDGET_MASTER.Code='" + clsCommon.myCstr(obj.Code) + "'", Nothing))
                        revno = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select TSPL_CAPEX_BUDGET_MASTER.Revision_No from TSPL_CAPEX_BUDGET_MASTER where TSPL_CAPEX_BUDGET_MASTER.Code='" + clsCommon.myCstr(obj.Code) + "'", Nothing))
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
                    clsCapexBudget.SaveData(obj, clsCapexBudget.CheckNewEntry(obj.Code))
                Next
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)
        funReset()
    End Sub

    Private Sub RadMenuItem3_Click(sender As Object, e As EventArgs) Handles RadMenuItem3.Click
        Dim str As String
        str = " select TSPL_CAPEX_BUDGET_MASTER.CODE as [Code], TSPL_CAPEX_BUDGET_MASTER.DESCRIPTION as [Description],TSPL_CAPEX_BUDGET_MASTER.Capex_Code as [Capex Code],TSPL_CAPEX_BUDGET_MASTER.Budget as [Budget],TSPL_CAPEX_BUDGET_MASTER.Tolerence as [Tolerence],convert(varchar,TSPL_CAPEX_BUDGET_MASTER.Doc_Date,103) as [Date],TSPL_CAPEX_BUDGET_MASTER.inc_budget as [Inc Budget] from TSPL_CAPEX_BUDGET_MASTER"
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub txtCapexCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCapexCode._MYValidating
        Try
            Me.txtCapexCode.Value = clsCapexMaster.getFinder("", txtCapexCode.Value, isButtonClicked)
            If clsCommon.myLen(Me.txtCapexCode.Value) > 0 Then
                txtCapexName.Text = clsCapexMaster.GetName(Me.txtCapexCode.Value, Nothing)
                txtCapexBalance.Text = clsCapexMaster.chkSubBudget(Me.txtCapexCode.Value, Nothing)
                txtCapexAmount.Text = clsCapexMaster.chkMainBuget(Me.txtCapexCode.Value, Nothing)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub FrmCapexBudget_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        lblcurrentBudget.Text = 0
        isNewEntry = True
        'txtCode.MyReadOnly = True
        txtCode.Value = Nothing
        txtCode.Focus()
        txtDesc.Text = ""
        txtCapexCode.Value = ""
        txtCapexName.Text = ""
        txt_budget.Text = 0
        txt_revisedbudget.Text = 0
        txt_tolerence.Text = 0
        txt_revisionno.Text = 0
        NumIncBudget.Text = 0
        txtdate.Text = DateTime.Now()
        txt_budget.ReadOnly = False
        txt_revisedbudget.Enabled = False
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = False
        txtCapexBalance.Text = 0
        txtCapexAmount.Text = 0
        chkProvisional.Checked = False
        EnableDisableSaveButton()
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        'txtCode.MyReadOnly = True
        'btnSave.Enabled = True
        btnDelete.Enabled = True
        isNewEntry = False
        Dim obj As New clsCapexBudget()
        obj = clsCapexBudget.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
            funReset()
            isNewEntry = False

            txt_budget.ReadOnly = True
            txt_revisedbudget.Enabled = True

            btnSave.Text = "Update"
            btnDelete.Enabled = True
            txtCode.Value = obj.Code
            txtDesc.Text = obj.Description
            txtCapexCode.Value = obj.CapexCode
            txtCapexName.Text = clsCapexMaster.GetName(clsCommon.myCstr(obj.CapexCode), Nothing)
            txt_budget.Text = obj.Budget
            txt_revisedbudget.Text = 0
            txt_revisionno.Text = obj.RevNo
            txtdate.Text = obj.DocDate
            txt_tolerence.Text = obj.Tolerence
            NumIncBudget.Text = 0
            If clsCommon.myCdbl(obj.RevBudget) <> 0 Then
                lblcurrentBudget.Text = obj.RevBudget
            Else
                lblcurrentBudget.Text = obj.CurrentBudget
            End If
            chkProvisional.Checked = obj.Provisional
            txtCapexBalance.Text = clsCapexMaster.chkSubBudget(Me.txtCapexCode.Value, Nothing)
            txtCapexAmount.Text = clsCapexMaster.chkMainBuget(Me.txtCapexCode.Value, Nothing)
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

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.capexbudget, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If

                Dim rbudget As String = Nothing
                Dim revno As String = Nothing

                Dim obj As New clsCapexBudget()
                obj.Code = clsCommon.myCstr(txtCode.Value)
                obj.Description = clsCommon.myCstr(txtDesc.Text)
                obj.Budget = clsCommon.myCdbl(txt_budget.Text)
                obj.CapexCode = clsCommon.myCstr(txtCapexCode.Value)
                obj.DocDate = clsCommon.GetPrintDate(txtdate.Text)
                obj.IncBudget = clsCommon.myCdbl(NumIncBudget.Text)
                obj.RevBudget = clsCommon.myCdbl(txt_revisedbudget.Text)
                obj.Tolerence = clsCommon.myCdbl(txt_tolerence.Text)
                obj.CurrentBudget = clsCommon.myCdbl(lblcurrentBudget.Text)
                obj.RevNo = clsCommon.myCstr(txt_revisionno.Text)
                obj.Provisional = chkProvisional.Checked

                If Not isNewEntry Then
                    rbudget = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select TSPL_CAPEX_BUDGET_MASTER.Revised_Budget from TSPL_CAPEX_BUDGET_MASTER where TSPL_CAPEX_BUDGET_MASTER.Code='" + clsCommon.myCstr(obj.Code) + "'", Nothing))
                    revno = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select TSPL_CAPEX_BUDGET_MASTER.Revision_No from TSPL_CAPEX_BUDGET_MASTER where TSPL_CAPEX_BUDGET_MASTER.Code='" + clsCommon.myCstr(obj.Code) + "'", Nothing))
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

                If (clsCapexBudget.SaveData(obj, isNewEntry)) Then
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    LoadData(obj.Code, NavigatorType.Current)
                    btnSave.Text = "Update"
                    txt_budget.ReadOnly = True
                    txt_revisedbudget.Enabled = True
                    btnDelete.Enabled = True
                Else
                    btnSave.Text = "Save"
                    txt_budget.ReadOnly = False
                    txt_revisedbudget.Enabled = False
                    btnDelete.Enabled = False
                End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString())
        End Try
        
    End Sub

    Function AllowToSave() As Boolean
        If clsCommon.myLen(txt_budget.Text) <= 0 OrElse clsCommon.myCdbl(txt_budget.Text) <= 0 Then
            myMessages.blankValue("Please enter budget.")
            txt_budget.Focus()
            Return False
        End If

        If clsCommon.myLen(txtCapexCode.Value) <= 0 Then
            myMessages.blankValue("Please enter capex code.")
            txtCapexCode.Focus()
            Return False
        End If
        Dim docdate As Date = Nothing
        Dim CurrentDate As Date = Nothing
        Dim ChkRevision As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select TSPL_CAPEX_MASTER.Revision_No from tspl_capex_master where code='" & txtCapexCode.Value & "'"))
        If ChkRevision > 0 Then
            docdate = clsDBFuncationality.getSingleValue("select top 1 doc_date from TSPL_CAPEX_MASTER_Hist_Data where code='" & txtCapexCode.Value & "' and Revision_NO=0 order by Doc_Date")
        Else
            docdate = clsDBFuncationality.getSingleValue("select convert(varchar,Created_Date,103) as Doc_Date from tspl_capex_master where code='" & txtCapexCode.Value & "'")
        End If
        CurrentDate = txtdate.Text

        If clsCommon.myCDate(CurrentDate) < clsCommon.myCDate(docdate) Then
            clsCommon.MyMessageBoxShow("Sub Capex date should  not be  less than Main Capex")
            txtdate.Focus()
            Return False
        End If


        'If clsCommon.GetPrintDate(clsCommon.myCDate(txtdate.Value), "dd/MMM/yyyy") < clsCommon.GetPrintDate(clsCommon.myCDate(docdate), "dd/MMM/yyyy") Then
        '    clsCommon.MyMessageBoxShow("Sub Capex date should  not be  less than Main Capex")
        '    txtdate.Focus()
        '    Return False
        'End If
        'If clsCommon.myLen(txt_tolerence.Text) <= 0 OrElse clsCommon.myCdbl(txt_tolerence.Text) <= 0 Then
        '    myMessages.blankValue("Please enter tolerence")
        '    txt_tolerence.Focus()
        '    Return False
        'End If
        'Dim Buget As Decimal
        'If clsCommon.myCdbl(txt_revisedbudget.Text) > 0 Then
        '    Buget = clsCommon.myCdbl(txt_revisedbudget.Text)
        'Else
        '    Buget = clsCommon.myCdbl(txt_budget.Text)
        'End If

        'If clsCapexMaster.chkLimitBugetMaster(clsCommon.myCstr(txtCapexCode.Value), clsCommon.myCdbl(Buget), clsCommon.myCdbl(txt_tolerence.Text), clsCommon.myCstr(txtCode.Value)) = False Then
        '    Return False
        'End If

        'If POAmount(clsCommon.myCstr(txtCode.Value), Buget, clsCommon.myCdbl(txt_tolerence.Text)) = False Then
        '    Return False
        'End If
        If clsCapexBudget.ChkAcquisitionEntry(clsCommon.myCstr(txtCode.Value)) = False Then
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
                If (clsCapexBudget.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub txt_tolerence_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_tolerence.KeyDown
        If clsCommon.myCdbl(txt_tolerence.Text) > 100 Then
            clsCommon.MyMessageBoxShow("Tolerence can not be greater than 100.")
            txt_tolerence.Text = 100
        End If
    End Sub

    Private Sub BtnHistory_Click(sender As Object, e As EventArgs) Handles BtnHistory.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select Capex Code")
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowHistoryData(txtCode.Value, "Code", "TSPL_CAPEX_BUDGET_MASTER")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub NumIncBudget_TextChanged(sender As Object, e As EventArgs) Handles NumIncBudget.TextChanged


        If clsCommon.myCdbl(NumIncBudget.Text) <> 0 AndAlso clsCommon.myCdbl(txt_budget.Text) >= 0 Then

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
    'Function chkLimitBugetMaster(ByVal CapexCode As String, ByVal Buget As Decimal, ByVal Tolerence As Decimal, ByVal SubCapexCode As String) As Boolean
    '    Dim strLimitBuget As Decimal = 0
    '    Dim strLimitBugetTol As Decimal = 0
    '    Dim strLimitSubBuget As Decimal = 0
    '    Dim strLimitSubBugetTol As Decimal = 0
    '    Dim strLimitBugetwithTol As Decimal = 0
    '    Dim strLimitSubBugetWithtol As Decimal = 0
    '    Dim revno As Decimal = 0
    '    Dim Strcurrentbugetwithtol As Decimal = 0

    '    If clsCommon.myLen(CapexCode) > 0 Then
    '        strLimitBuget = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select case when sum(isnull(Revised_Budget,0))=0 then sum(isnull(Budget,0)) else sum(isnull(Revised_Budget,0)) end as Budget  from tspl_capex_master where code='" & CapexCode & "'"))
    '        strLimitBugetTol = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select (isnull(Tolerence,0)) as Tolerence   from tspl_capex_master where code='" & CapexCode & "'"))
    '        If clsCommon.myLen(SubCapexCode) > 0 Then
    '            strLimitSubBuget = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT SUM(Budget) AS Budget FROM (SELECT ((Budget*Tolerence) /100)+Budget AS Budget FROM (select case when (isnull(Revised_Budget,0))=0 then (isnull(Budget,0)) else (isnull(Revised_Budget,0)) end as Budget,Tolerence from TSPL_CAPEX_BUDGET_MASTER where Capex_Code ='" & CapexCode & "' and code not in ('" & SubCapexCode & "')) AS XX)AS XXX"))
    '        Else
    '            strLimitSubBuget = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT SUM(Budget) AS Budget FROM (SELECT ((Budget*Tolerence) /100)+Budget AS Budget FROM (select case when (isnull(Revised_Budget,0))=0 then (isnull(Budget,0)) else (isnull(Revised_Budget,0)) end as Budget,Tolerence from TSPL_CAPEX_BUDGET_MASTER where Capex_Code ='" & CapexCode & "' )AS XX)AS XXX"))
    '        End If


    '        'If clsCommon.myCdbl(strLimitBugetTol) > 0 Then
    '        strLimitBugetwithTol = (strLimitBuget / 100) * strLimitBugetTol
    '        strLimitBuget = strLimitBuget + strLimitBugetwithTol

    '        Strcurrentbugetwithtol = Buget + ((Buget * Tolerence) / 100)
    '        strLimitSubBugetWithtol = strLimitSubBuget + Strcurrentbugetwithtol
    '        strLimitSubBuget = strLimitSubBugetWithtol




    '        'End If

    '        If clsCommon.myCdbl(strLimitSubBuget) <= clsCommon.myCdbl(strLimitBuget) Then
    '            Return True
    '        Else
    '            'clsCommon.MyMessageBoxShow("Capex Sub Buget with tolerence is " & strLimitSubBuget & " is less then " & strLimitBuget & "  ")
    '            clsCommon.MyMessageBoxShow("Total Sub Capex Budget " & strLimitSubBuget & " cannot exceed Main Capex Budget  " & strLimitBuget & "  ")
    '            Return False
    '        End If

    '    End If

    '    Return True
    'End Function

    Private Sub txt_budget_TextChanged(sender As Object, e As EventArgs) Handles txt_budget.TextChanged
        If clsCommon.myCdbl(NumIncBudget.Text) = 0 AndAlso clsCommon.myCdbl(txt_budget.Text) > 0 Then

            lblcurrentBudget.Text = txt_budget.Text
        Else
            lblcurrentBudget.Text = 0

        End If
    End Sub
    Function POAmount(ByVal StrCapexCode As String, ByVal Budget As Decimal, ByVal Tolerence As Decimal) As Boolean
        Dim strPoAmount As Decimal = 0
        Dim strBudget As Decimal = 0
        strPoAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(amt_after_tax ) as PO_AMOUNT from tspl_purchase_order_head where Capex_SubCode ='" & StrCapexCode & "' "))
        strBudget = ((Budget * Tolerence) / 100) + Budget
        If strPoAmount > strBudget Then
            clsCommon.MyMessageBoxShow(" " & strPoAmount & " Budget already used in PO for the Capex")
            Return False
        Else
            Return True
        End If
        Return True
    End Function

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
End Class
