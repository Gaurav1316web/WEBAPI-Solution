Imports common
Imports System.Data.SqlClient

Public Class FrmAbateMentMaster
    Inherits FrmMainTranScreen
    Dim PageMode As String
    Dim change As Boolean = True
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim userCode, companyCode As String
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
    Public Sub SetLength()
        fndAbatement.MyMaxLength = 12
        txtDesc.MaxLength = 50
    End Sub
    Private Sub FrmAbateMentMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetLength()
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New Trasnaction")
        PageMode = "New"
        Me.Text = "Abatement"
        AddHandler fndAbatement.TextChanged, AddressOf fndAbatement_TextChanged
        AddHandler fndAbatement.KeyPress, AddressOf fndAbatement_KeyPress
        ResetScreen()
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If

    End Sub
    Private Sub SetUserMgmtNew()
        '' Anubhooti 31-July-2014 BM00000003131
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmAbateMentMaster)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If
        btnSave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 03/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnSave.Visible = True Then
            rmImport.Enabled = True
            rmExport.Enabled = True
        Else
            rmImport.Enabled = False
            rmExport.Enabled = False
        End If
        '--------------------------------------------------
        'btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub ResetScreen()

        dtpStart.Format = DateTimePickerFormat.Short
        dtpEnd.Format = DateTimePickerFormat.Short
        dtpEnd.Value = connectSql.serverDate()
        dtpStart.Value = connectSql.serverDate()
        fndAbatement.Value = ""
        fndAbatement.MyReadOnly = False
        txtDesc.Text = ""
        txtRate.Text = ""
        PageMode = "New"
    End Sub
    Private Sub fndAbatement_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If change = True Then
            If fndAbatement.Value.Trim <> "" Then
                FillAbtMaster(fndAbatement.Value)
                PageMode = "Edit"
            End If
        End If
        change = True


    End Sub
    Private Sub FillAbtMaster(ByVal Code As String)
        Dim dtMaster As DataTable
        dtMaster = (New BAL.BALPriceComponant).GetAbtMaster(Code)
        txtDesc.Text = dtMaster.Rows(0)("Desc").ToString()
        txtRate.Text = dtMaster.Rows(0)("Rate").ToString()
        dtpStart.Value = Convert.ToDateTime(dtMaster.Rows(0)("Start"))
        dtpEnd.Value = Convert.ToDateTime(dtMaster.Rows(0)("End"))
    End Sub
    Private Sub fndAbatement_KeyPress(ByVal sender As System.Object, ByVal e As KeyPressEventArgs)


        change = False

    End Sub

    'Private Sub fndAbatement_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fndAbatement.ConnectionString = connectSql.SqlCon()
    '    fndAbatement.Query = "SELECT [Abatement_Code] as [Abatement Code] ,[Abatement_Desc] as [Description] FROM [dbo].[TSPL_ABATEMENT_MASTER]"
    '    fndAbatement.ValueToSelect = "Abatement Code"
    '    fndAbatement.ValueToSelect1 = "Abatement Code"
    '    fndAbatement.Caption = "Abatement"
    'End Sub


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()

    End Sub
    Sub SaveData()
        If MyBase.isModifyonPasswordFlag Then
            If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.FrmAbateMentMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
            Else
                Return
            End If
        End If
        If ValidateSave() Then
            If PageMode = "New" Then
                Dim Ins As New BAL.BALPriceComponant
                Ins.InsertAbtMaster(fndAbatement.Value, txtDesc.Text, dtpStart.Value.Date, dtpEnd.Value.Date, Convert.ToDecimal(txtRate.Text), userCode, companyCode)

                ShowMsg("Record Saved Successfully")
                ResetScreen()
            Else
                Dim Ins As New BAL.BALPriceComponant
                Ins.UpdateAbtMaster(fndAbatement.Value, txtDesc.Text, dtpStart.Value.Date, dtpEnd.Value.Date, Convert.ToDecimal(txtRate.Text), userCode, companyCode)
                ShowMsg("Record Updated Successfully")
                ResetScreen()
            End If

        End If

    End Sub
    Private Function ValidateSave() As Boolean
        If fndAbatement.Value.Trim() = "" Then
            ShowMsg("Abatement code can not be blank")
            fndAbatement.Focus()
            Return False
        ElseIf dtpStart.Value > dtpEnd.Value Then
            ShowMsg("To date should be greater than or equal to from date")
            dtpStart.Focus()
            Return False
        ElseIf (New BAL.BALPriceComponant).GetAbtMaster(fndAbatement.Value).Rows.Count > 0 And PageMode = "New" Then
            ShowMsg("This Abatement Code is already exists")
            fndAbatement.Focus()
            Return False
        ElseIf (New BAL.BALPriceComponant).NotValidate(dtpStart.Value, dtpEnd.Value, fndAbatement.Value) Then
            ShowMsg("This Abatement  is already exists between from date and to date")
            dtpStart.Focus()
            Return False
        ElseIf txtRate.Text = "" Then
            ShowMsg("This rate can not be blank")
            txtRate.Focus()
            Return False
        ElseIf Convert.ToDecimal(txtRate.Text) <= 0 Then
            ShowMsg("This rate should be greater than 0")
            txtRate.Focus()
            Return False
        Else
            Return True
        End If
        Return True

    End Function
    Private Sub txtRate_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim KeyAscii As Short = Asc(e.KeyChar)
        If Not ((KeyAscii >= System.Windows.Forms.Keys.D0 And KeyAscii <= System.Windows.Forms.Keys.D9) Or (KeyAscii = System.Windows.Forms.Keys.Back) Or Chr(KeyAscii) = "." Or (Chr(KeyAscii) Like "[ ]")) Then
            KeyAscii = 0
            txtRate.Focus()
        End If
        If KeyAscii = 0 Then
            e.Handled = True
        End If

        If txtRate.Text.IndexOf(".") >= 0 And e.KeyChar = "." Then
            e.Handled = True
        End If

        If txtRate.Text.IndexOf(".") > 0 Then
            If txtRate.SelectionStart > txtRate.Text.IndexOf(".") Then
                If txtRate.Text.Length - txtRate.Text.IndexOf(".") = 3 Then
                    e.Handled = True
                End If
            End If
        End If

    End Sub
    Private Sub ShowMsg(ByVal Msg As String)
        common.clsCommon.MyMessageBoxShow(Msg, "Abatement", MessageBoxButtons.OK)
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If fndAbatement.Value <> "" And PageMode = "Edit" Then


            Dim Del As New BAL.BALPriceComponant
            Del.DeleteAbtMaster(fndAbatement.Value)
            ShowMsg("Record Deleted Sucessfully")
            ResetScreen()
        End If
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        ResetScreen()
    End Sub


    'priti added on 01-06-2011 --- To implement the access control
    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        'If funCheckLoginStatus() = False Then Exit Function
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "ABATMENT"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
    '        strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '        strTemp = Split(strRights, ",")
    '        If strTemp(0) = "0" Then
    '            MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    '            funSetUserAccess = False
    '            blnRead = False
    '            Me.Close()
    '            Exit Function
    '        Else
    '            blnRead = True
    '        End If
    '        If strTemp(1) = "0" Then 'Grant modify access
    '            btnSave.Enabled = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            btnDelete.Enabled = False
    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception

    '    End Try
    'End Function




    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub fndAbatement__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndAbatement._MYValidating
        Dim qst As String = "select count(*) from [dbo].[TSPL_ABATEMENT_MASTER] where Abatement_Code='" + fndAbatement.Value + "'"
        Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
        If count = 0 Then
            fndAbatement.MyReadOnly = False
        Else
            fndAbatement.MyReadOnly = True
        End If
        If fndAbatement.MyReadOnly OrElse isButtonClicked Then

            Dim qry As String '= "SELECT [Abatement_Code] as Code ,[Abatement_Desc] as [Description] FROM [dbo].[TSPL_ABATEMENT_MASTER]"
            'fndAbatement.Value = clsCommon.ShowSelectForm("AbatemnMastrfnd", qry, "Code", "", fndAbatement.Value, "Abatement_Code", isButtonClicked)
            fndAbatement.Value = clsAbatementMaster.getFinder("", fndAbatement.Value, isButtonClicked)
            qry = "select Abatement_Desc from [dbo].[TSPL_ABATEMENT_MASTER] where Abatement_Code='" + fndAbatement.Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                txtDesc.Text = clsCommon.myCstr(dt.Rows(0)("Abatement_Desc"))
            Else
                txtDesc.Text = ""
            End If

            LoadData()
        End If
    End Sub

    Private Sub fndAbatement__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndAbatement._MYNavigator
        Dim qst As String = "SELECT [Abatement_Code] as [Abatement Code] ,[Abatement_Desc] as [Description] FROM [dbo].[TSPL_ABATEMENT_MASTER] where 2=2"
        Select Case NavType
            Case NavigatorType.Current
                qst += " and [dbo].[TSPL_ABATEMENT_MASTER] .Abatement_Code in ('" + fndAbatement.Value + "')"
            Case NavigatorType.Next
                qst += " and [dbo].[TSPL_ABATEMENT_MASTER] .Abatement_Code in (select min(Abatement_Code ) from [dbo].[TSPL_ABATEMENT_MASTER] where Abatement_Code  >'" + fndAbatement.Value + "')"
            Case NavigatorType.First
                qst += " and [dbo].[TSPL_ABATEMENT_MASTER] .Abatement_Code in (select MIN(Abatement_Code ) from [dbo].[TSPL_ABATEMENT_MASTER])"

            Case NavigatorType.Last
                qst += " and [dbo].[TSPL_ABATEMENT_MASTER] .Abatement_Code in (select Max(Abatement_Code ) from [dbo].[TSPL_ABATEMENT_MASTER])"
            Case NavigatorType.Previous
                qst += " and [dbo].[TSPL_ABATEMENT_MASTER] .Abatement_Code in (select Max(Abatement_Code ) from [dbo].[TSPL_ABATEMENT_MASTER] where Abatement_Code  <'" + fndAbatement.Value + "')"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            fndAbatement.Value = clsCommon.myCstr(dt.Rows(0)("Abatement Code"))
            txtDesc.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
        End If
        LoadData()
    End Sub
    Public Sub LoadData()
        If change = True Then
            If fndAbatement.Value.Trim <> "" Then
                FillAbtMaster(fndAbatement.Value)
                PageMode = "Edit"
            End If
        End If
        change = True

    End Sub

    Private Sub FrmAbateMentMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            'PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            ResetScreen()
        End If
    End Sub

    Private Sub rmExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmExport.Click
        Dim str As String
        str = "Select Abatement_Code As [Abatement Code],Abatement_Desc As [Abatement Desc],Start_Date As [Start Date],End_Date As [End Date],Abatement_Percent As [Abatement Percent] from TSPL_ABATEMENT_MASTER "
        transportSql.ExporttoExcel(str, Me)
    End Sub
    ''richa Ticket No BM00000002902 19/06/2014
    Private Sub rmImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmImport.Click
        Dim gv As New RadGridView()
        Dim IsNewEntry As Boolean
        Dim trans As SqlTransaction = Nothing
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Abatement Code", "Abatement Desc", "Start Date", "End Date", "Abatement Percent") Then

            Dim linno As Integer = 0
            Try
                trans = clsDBFuncationality.GetTransactin()
                connectSql.OpenConnection()


                'clsCommon.ProgressBarShow()
                For Each grow1 As GridViewRowInfo In gv.Rows
                    Dim strStartDate As Date = clsCommon.myCDate(grow1.Cells("Start Date").Value)
                    Dim strEndDate As Date = clsCommon.myCDate(grow1.Cells("End Date").Value)
                    If (String.IsNullOrEmpty(strStartDate)) Or clsCommon.myLen(strStartDate) > 0 AndAlso (String.IsNullOrEmpty(strEndDate)) Or clsCommon.myLen(strEndDate) > 0 Then
                        If strStartDate > strEndDate Then
                            ShowMsg("To date should be greater than or equal to from date")
                            Exit Sub
                        End If
                    End If

                Next

                For Each grow As GridViewRowInfo In gv.Rows

                    Dim obj As New clsAbatementMaster()

                    linno += 1
                    Dim strcode As String = clsCommon.myCstr(grow.Cells("Abatement Code").Value)
                    If (String.IsNullOrEmpty(strcode)) Or clsCommon.myLen(strcode) > 12 Then
                        Throw New Exception("Length of Abatement Code should be max. 12 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Abatement_Code = strcode

                    Dim strdesc As String = clsCommon.myCstr(grow.Cells("Abatement Desc").Value)
                    If (String.IsNullOrEmpty(strdesc)) Or clsCommon.myLen(strdesc) > 50 Then
                        Throw New Exception("Length of Abatement Desc should be max. 50 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Abatement_Desc = strdesc

                    Dim strstartdate As Date = clsCommon.myCDate(grow.Cells("Start Date").Value)
                    If (String.IsNullOrEmpty(strstartdate)) Or clsCommon.myLen(strstartdate) < 0 Then
                        Throw New Exception("Start Date should not be left blank At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Start_Date = strstartdate

                    Dim strendate As Date = clsCommon.myCDate(grow.Cells("End Date").Value)
                    If (String.IsNullOrEmpty(strendate)) Or clsCommon.myLen(strendate) < 0 Then
                        Throw New Exception("End Date should not be left blank At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.End_Date = strendate

                    Dim strpercent As String = clsCommon.myCstr(grow.Cells("Abatement Percent").Value)
                    If (String.IsNullOrEmpty(strpercent)) Or clsCommon.myLen(strpercent) < 0 Then
                        Throw New Exception("Length of Abatement Percent should be greater than  0 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Abatement_Percent = strpercent

                    If clsCommon.myLen(strcode) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from TSPL_ABATEMENT_MASTER where Abatement_Code='" + strcode + "' ", trans) > 0 Then
                        IsNewEntry = False
                    Else
                        IsNewEntry = True

                    End If


                    obj.SaveData(obj, IsNewEntry, trans)
                Next
                trans.Commit()
                'clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                'clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub
    '--------------------------------------------------------------------------------------'
End Class
