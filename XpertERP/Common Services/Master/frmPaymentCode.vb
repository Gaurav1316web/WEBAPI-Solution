Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports System.Data.SqlClient
Imports System.Data
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Text.RegularExpressions
Imports exz = Microsoft.Office.Interop.Excel
Imports common

Public Class FrmPaymentCode
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim userCode, companyCode As String
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
#Region "Variables"
    Dim sql As String
    Dim ds As DataSet
    Dim dr As DataTable
    Dim dt As Date = Date.Today

    Const colSelect As String = "SELECT"
    Const colCompCode As String = "COMPCODE"
    Const colCompName As String = "COMPNAME"
    Const colDataBaseName As String = "DATABASE"
#End Region

    Private Sub SetUserMgmtNew()
        '' Anubhooti 30-July-2014 BM00000003131
        'MyBase.SetUserMgmt(clsUserMgtCode.paymentCodes)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If
        btn_save.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 03/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btn_save.Visible = True Then
            RadMenuItem2.Enabled = True
            RadMenuItem3.Enabled = True
        Else
            RadMenuItem2.Enabled = False
            RadMenuItem3.Enabled = False
        End If
        '--------------------------------------------------
        '         btnclose.Visible = MyBase.isDeleteFlag
        btn_delete.Visible = MyBase.isDeleteFlag
    End Sub
    Public Sub SetLength()
        fnd_paymentcode.MyMaxLength = 12
        txt_description.MaxLength = 50
    End Sub

    Private Sub FrmPaymentCode_vb_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetLength()
        SetUserMgmtNew()
        SetDataBaseGrid()
        ButtonToolTip.SetToolTip(btn_save, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btn_delete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btn_close, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btn_reset, "Press Alt+N Adding New Trasnaction")
        ' globalFunc.mandatoryText(fnd_paymentcode.Value)
        globalFunc.mandatoryDropdown(ddl_paymenttype)
        ToolTipcode.SetToolTip(btn_reset, "New")
        fnd_paymentcode.MyMaxLength = 12
        fnd_paymentcode.BackColor = Color.White
        fnd_paymentcode.TabIndex = 0
        txt_description.TabIndex = 1
        ddl_paymenttype.TabIndex = 2
        btn_delete.Enabled = False
        btn_save.Enabled = True
        AddHandler fnd_paymentcode.KeyPress, AddressOf KeyPress1
        AddHandler fnd_paymentcode.TextChanged, AddressOf TextChanged1
        ' fnd_paymentcode.CharacterCasing = CharacterCasing.Upper
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub
    Private Sub KeyPress1(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub TextChanged1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            dr = clsDBFuncationality.GetDataTable("select * from TSPL_PAYMENT_CODE where [Payment_Code] ='" + fnd_paymentcode.Value + "'")
            Dim payment_code As String = ""
            If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                payment_code = dr.Rows(0)(0).ToString()
            End If

            If payment_code <> fnd_paymentcode.Value Then
                txt_description.Text = ""
                btn_save.Enabled = True
                btn_save.Text = "Save"
            Else
                funFill()
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Private Sub funFill()
        Try
            If fnd_paymentcode.Value <> "" Then
                dr = clsDBFuncationality.GetDataTable("select * from TSPL_PAYMENT_CODE where Payment_Code='" + fnd_paymentcode.Value + "'")
                If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                    txt_description.Text = dr.Rows(0)(1).ToString()
                    ddl_paymenttype.Text = dr.Rows(0)(2).ToString()
                End If
                btn_save.Enabled = True
                btn_save.Text = "Update"
                btn_delete.Enabled = True
                txt_description.Enabled = True
            Else
                btn_save.Enabled = True
                btn_save.Text = "Save"
                txt_description.Enabled = True
            End If
            'If userCode <> "ADMIN" Then
            '    If funSetUserAccess() = False Then Exit Sub
            'End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub btn_reset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_reset.Click
        funreset()
    End Sub
    Private Sub funreset()
        Try
            fnd_paymentcode.Value = ""
            fnd_paymentcode.MyReadOnly = False
            txt_description.Text = ""
            ddl_paymenttype.Text = "Cash"
            btn_save.Enabled = True
            btn_delete.Enabled = False
            btn_save.Text = "Save"
            fnd_paymentcode.Focus()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    'Private Sub fnd_paymentcode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fnd_paymentcode.ConnectionString = connectSql.SqlCon()
    '    fnd_paymentcode.Query = "select [Payment_Code] as [Payment Code],[Payment_Desc] as [Description] from TSPL_PAYMENT_CODE"
    '    fnd_paymentcode.ValueToSelect = "Payment Code"
    '    fnd_paymentcode.Caption = "Payment Code"
    '    fnd_paymentcode.ValueToSelect1 = "Payment Code"
    'End Sub

    Private Sub btn_save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Savedata()
    End Sub
    Public Sub Savedata()
        If MyBase.isModifyonPasswordFlag Then
            If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.paymentCodes, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
            Else
                Return
            End If
        End If
        
        'If clsCommon.myLen(fnd_paymentcode.Value) <= 0 Then
        '    common.clsCommon.MyMessageBoxShow(" Payment Code can not be left blank")
        '    fnd_paymentcode.Focus()
        '    Return
        'End If
        Try
            If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) AndAlso clsCommon.myLen(fnd_paymentcode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(" Payment Code cannot be left blank")
                fnd_paymentcode.Focus()
                Return
            ElseIf ddl_paymenttype.Text = "" Then
                myMessages.blankValue("Payment Type")
            ElseIf btn_save.Text = "Save" Then
                funInsert()
            Else
                funUpdate()
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub btn_close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_close.Click
        closeform()
    End Sub
    Public Sub closeform()
        Me.Close()
    End Sub
    Private Sub funInsert()
        Try
            If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False)) Then
                Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from tspl_payment_code where Payment_Code='" & fnd_paymentcode.Value & "'")
                If ChkNewEntry = 0 Then
                    fnd_paymentcode.Value = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"), clsDocType.PaymentCode, "", "")
                    If clsCommon.myLen(fnd_paymentcode.Value) <= 0 Then
                        Throw New Exception("Error in Payment Code Generation")
                    End If
                End If
            End If
            clsDBFuncationality.UpdateInSelectedDatabase(GetReplecateCompaniesDataBase(), "sp_tspl_paymentcode_insert", New SqlParameter("@Payment_Code", fnd_paymentcode.Value), New SqlParameter("@Payment_desc", txt_description.Text), New SqlParameter("@Payment_Type", ddl_paymenttype.Text), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate()), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate()), New SqlParameter("@Comp_Code", companyCode))
            myMessages.insert()
            btn_save.Text = "Update"
            btn_delete.Enabled = True
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Private Sub funUpdate()
        Try
            clsDBFuncationality.UpdateInSelectedDatabase(GetReplecateCompaniesDataBase(), "sp_tspl_paymentcode_update", New SqlParameter("@Payment_Code", fnd_paymentcode.Value), New SqlParameter("@Payment_desc", txt_description.Text), New SqlParameter("@Payment_Type", ddl_paymenttype.Text), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate()), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate()), New SqlParameter("@Comp_Code", companyCode))
            myMessages.update()
            btn_save.Text = "Update"
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub btn_delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_delete.Click
        DeleteData()
    End Sub
    Public Sub DeleteData()

        If clsCommon.myLen(fnd_paymentcode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please Fill the Payment Code")
            fnd_paymentcode.Focus()
            Return
        End If
        Try
            If myMessages.deleteConfirm() Then
                funDelete()
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Private Sub funDelete()
        Try
            clsDBFuncationality.UpdateInSelectedDatabase(GetReplecateCompaniesDataBase(), "sp_tspl_paymentcode_delete", New SqlParameter("@Payment_Code", fnd_paymentcode.Value))
            myMessages.delete()
            btn_save.Text = "Save"
            btn_delete.Enabled = False
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub RadMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem3.Click
        sql = "Select Payment_Code,Payment_Desc,Payment_Type from TSPL_PAYMENT_CODE"
        transportSql.ExporttoExcel(sql, Me)
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click


        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Payment_Code", "Payment_Desc", "Payment_Type") Then
            Dim trans As SqlTransaction = Nothing
            Try
                connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim strpaymentcode As String = grow.Cells(0).Value.ToString().Trim()
                    Dim strpaymentdesc As String = grow.Cells(1).Value.ToString().Trim()
                    Dim strpaymenttype As String = grow.Cells(2).Value.ToString().Trim()
                    If strpaymentcode = String.Empty Then
                        myMessages.blankValue("Payment Code")
                        trans.Rollback()
                        Exit Sub
                    End If
                    If strpaymenttype = String.Empty Then
                        myMessages.blankValue("Payment_Type")
                        trans.Rollback()
                        Exit Sub
                    End If
                    If strpaymentcode.Length > 12 Then
                        common.clsCommon.MyMessageBoxShow("Payment Code length cannot be more than 12")
                        trans.Rollback()
                        Exit Sub
                    End If
                    If strpaymentdesc.Length > 50 Then
                        common.clsCommon.MyMessageBoxShow("Payment Description length cannot be more than 50")
                        trans.Rollback()
                        Exit Sub
                    End If
                    If strpaymenttype.Length > 12 Then
                        common.clsCommon.MyMessageBoxShow("Payment Type length cannot be more than 12")
                        trans.Rollback()
                        Exit Sub
                    End If
                    If strpaymenttype <> "Cash" And strpaymenttype <> "Cheque" And strpaymenttype <> "Petty Cash" And strpaymenttype <> "Other" And strpaymenttype <> "NEFT" And strpaymenttype <> "RTGS" And strpaymenttype <> "Transfer" Then
                        common.clsCommon.MyMessageBoxShow("Payment Type accept only Cash,Cheque,Petty Cash,Transfer,NEFT,RTGS and Other ")
                        trans.Rollback()
                        clsCommon.ProgressBarHide()
                        Exit Sub
                    End If
                    Dim sql1 As String = "select count(*) from TSPL_PAYMENT_CODE where Payment_Code='" + strpaymentcode + "'"
                    Dim i As Integer = CInt(connectSql.RunScalar(trans, sql1))
                    If (i = 0) Then
                        'sql = "insert into TSPL_TERMS_MASTER values('" + str + "','" + str1 + "','" + str2 + "','" + userCode + "','" + connectSql.serverDate() + "','" + userCode + "','" + connectSql.serverDate() + "','" + companyCode + "')"
                        'connectSql.RunSqlTransaction(trans, sql)
                        connectSql.RunSpTransaction(trans, "sp_tspl_paymentcode_insert", New SqlParameter("@Payment_Code", strpaymentcode), New SqlParameter("@Payment_desc", strpaymentdesc), New SqlParameter("@Payment_Type", strpaymenttype), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", companyCode))
                    Else
                        connectSql.RunSpTransaction(trans, "sp_tspl_paymentcode_update", New SqlParameter("@Payment_Code", strpaymentcode), New SqlParameter("@Payment_desc", strpaymentdesc), New SqlParameter("@Payment_Type", strpaymenttype), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", companyCode))
                    End If
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)

            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub
    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        'If funCheckLoginStatus() = False Then Exit Function
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "PAYM-CODE"
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
    '            btn_save.Enabled = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            btn_delete.Enabled = False
    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception

    '    End Try
    'End Function

    Private Function GetReplecateCompaniesDataBase() As List(Of String)
        Dim arrDBName As New List(Of String)
        arrDBName.Add(objCommonVar.CurrDatabase)
        For ii As Integer = 0 To gvDB.Rows.Count - 1
            If (clsCommon.myCBool(gvDB.Rows(ii).Cells(colSelect).Value)) Then
                arrDBName.Add(clsCommon.myCstr(gvDB.Rows(ii).Cells(colDataBaseName).Value))
            End If
        Next
        Return arrDBName
    End Function

    Sub SetDataBaseGrid()
        gvDB.Rows.Clear()
        gvDB.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.FormatString = ""
        repoSelect.HeaderText = "Select"
        repoSelect.Name = colSelect
        repoSelect.Width = 50
        repoSelect.ReadOnly = False
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvDB.MasterTemplate.Columns.Add(repoSelect)

        Dim repoCompCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCompCode.FormatString = ""
        repoCompCode.HeaderText = "Company Code"
        repoCompCode.Name = colCompCode
        repoCompCode.Width = 150
        repoCompCode.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoCompCode)

        Dim repoCompName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCompName.FormatString = ""
        repoCompName.HeaderText = "Company Name"
        repoCompName.Name = colCompName
        repoCompName.Width = 150
        repoCompName.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoCompName)

        Dim repoDB As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDB.FormatString = ""
        repoDB.HeaderText = "Database Name"
        repoDB.Name = colDataBaseName
        repoDB.IsVisible = False
        repoDB.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoDB)

        Dim qry As String = "SELECT Comp_Code,Comp_Name,DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0 and Comp_Code not in ('" + objCommonVar.CurrentCompanyCode + "')"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                gvDB.Rows.AddNew()
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colSelect).Value = False
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colCompCode).Value = clsCommon.myCstr(dr("Comp_Code"))
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colCompName).Value = clsCommon.myCstr(dr("Comp_Name"))
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colDataBaseName).Value = clsCommon.myCstr(dr("DataBase_Name"))
            Next
        End If
    End Sub

    Private Sub FrmPaymentCode_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btn_save.Enabled Then
            Savedata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btn_delete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            closeform()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funreset()
        End If
    End Sub

    Private Sub fnd_paymentcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fnd_paymentcode._MYValidating
        Dim qst As String = "select count(*) from TSPL_PAYMENT_CODE where Payment_Code='" + fnd_paymentcode.Value + "'"
        Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
        If count = 0 Then
            fnd_paymentcode.MyReadOnly = False
        Else
            fnd_paymentcode.MyReadOnly = True
        End If
        If fnd_paymentcode.MyReadOnly OrElse isButtonClicked Then
            'Dim qry As String = "select [Payment_Code] as Code,[Payment_Desc] as [Description] from TSPL_PAYMENT_CODE"
            'fnd_paymentcode.Value = clsCommon.ShowSelectForm("fmPaymentCode", qry, "Code", "", fnd_paymentcode.Value, "Payment_Code", isButtonClicked)

            fnd_paymentcode.Value = clsPaymentCode.getFinder("", fnd_paymentcode.Value, isButtonClicked)
            LoadData()
        End If
    End Sub
    Public Sub LoadData()
        Try
            dr = clsDBFuncationality.GetDataTable("select * from TSPL_PAYMENT_CODE where [Payment_Code] ='" + fnd_paymentcode.Value + "'")
            Dim payment_code As String = ""
            If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                payment_code = dr.Rows(0)(0).ToString()
            End If

            If payment_code <> fnd_paymentcode.Value Then
                txt_description.Text = ""
                btn_save.Enabled = True
                btn_save.Text = "Save"
            Else
                funFill()
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub fnd_paymentcode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fnd_paymentcode._MYNavigator


        Dim qst As String = "select [Payment_Code] as [Payment Code],[Payment_Desc] as [Description] from TSPL_PAYMENT_CODE where 2=2"
        Select Case NavType
            Case NavigatorType.Current
                qst += " and TSPL_PAYMENT_CODE .Payment_Code in ('" + fnd_paymentcode.Value + "')"
            Case NavigatorType.Next
                qst += " and TSPL_PAYMENT_CODE .Payment_Code in (select min(Payment_Code) from TSPL_PAYMENT_CODE where Payment_Code  >'" + fnd_paymentcode.Value + "')"
            Case NavigatorType.First
                qst += " and TSPL_PAYMENT_CODE .Payment_Code in (select MIN(Payment_Code) from TSPL_PAYMENT_CODE)"

            Case NavigatorType.Last
                qst += " and TSPL_PAYMENT_CODE .Payment_Code in (select Max(Payment_Code) from TSPL_PAYMENT_CODE)"
            Case NavigatorType.Previous
                qst += " and TSPL_PAYMENT_CODE .Payment_Code in (select Max(Payment_Code) from TSPL_PAYMENT_CODE where Payment_Code  <'" + fnd_paymentcode.Value + "')"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            fnd_paymentcode.Value = clsCommon.myCstr(dt.Rows(0)("Payment Code"))
            txt_description.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
        End If
        LoadData()
    End Sub

    Private Sub gvDB_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gvDB.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub
End Class
