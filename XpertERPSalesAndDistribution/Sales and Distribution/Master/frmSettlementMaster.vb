'Developed By -BibhuPrasad Parida
'Database - TSPLERP
'Table - tspl_SettleMent_Master
'Start Date -
'End Date -
'' Code Added By Abhishek as on 19/10/2012 10:42 am For Check If Exist SettlementCode in Tspl_Quick SettlementDetali,Tspl_RecieptAdjustmentDetails   then Don't Delete
'' Code Added By Abhishek as on 23/10/2012 12:12 am For FinanCial Entry CheckBox Entry
'--preeti gupta-ticket no.[BM00000003133]
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports common
Imports XpertERPEngine


Public Class FrmSettlementMaster
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()


#Region "Variables"
    Dim sql As String
    Dim ds As DataSet
    Dim dr As DataTable
    Dim tableName As String = "tspl_SettleMent_Master"
    Dim userCode, companyCode As String


#End Region
#Region "Constructor"
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
#End Region

    Private Sub FrmSettlementMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        fndSettleMentCode.MyMaxLength = 12
        txtDescription.MaxLength = 50
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ' ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")

        SetUserMgmtNew()
        fndSettleMentCode_textchanged()
        funReset()
        BindSettlementType()
        'fndSettleMentCode.txtValue.CharacterCasing = CharacterCasing.Upper
        'fndSettleMentCode.txtValue.MaxLength = 12
        'AddHandler fndSettleMentCode.ValueChanged, AddressOf fndSettleMentCode_textchanged
        'AddHandler fndSettleMentCode.txtValue.KeyPress, AddressOf fndSettleMentCode_keypress
        'btnDelete.Enabled = False
        txtGLAccountDesc.ReadOnly = True
        'globalFunc.mandatoryText(fndSettleMentCode.txtValue)
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub
    Private Sub BindSettlementType()
        ddlSettlementType.Items.Add("Salesman")
        ddlSettlementType.Items.Add("Discount")
        ddlSettlementType.Items.Add("Credit Sale")
        ddlSettlementType.Items.Add("Cash")
        ddlSettlementType.Items.Add("Cheque")
        ddlSettlementType.Items.Add("Cash Shortage")
        ddlSettlementType.Items.Add("Empty Shortage")
        ddlSettlementType.Items.Add("Deposit")
        ddlSettlementType.Items.Add("Refund")
    End Sub


    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmSettlementMaster)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub fndSettleMentCode_keypress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub
    Private Sub fndSettleMentCode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndSettleMentCode.ConnectionString = connectSql.SqlCon()
        'fndSettleMentCode.Query = "select SettleMentCode as [SettleMent Code],Description from tspl_SettleMent_Master order by SettleMentCode"
        'fndSettleMentCode.ValueToSelect = "SettleMent Code"
        'fndSettleMentCode.ValueToSelect1 = "Description"
        'fndSettleMentCode.Caption = "SettleMent Details"
    End Sub
    Sub fndSettleMentCode_textchanged()
        Dim s As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select * from tspl_SettleMent_Master where SettleMentCode ='" + fndSettleMentCode.Value + "'"))
        If s <> fndSettleMentCode.Value Then
            txtDescription.Text = ""
            btnSave.Text = "Save"
            btnDelete.Enabled = False
        Else
            funFill()
        End If

    End Sub
    Private Sub funFill()
        dr = clsDBFuncationality.GetDataTable("select Description,Calculate,Account_Code,Account_description ,Settlement_Type,Type,Financial_Entry from tspl_SettleMent_Master where SettleMentCode ='" + fndSettleMentCode.Value + "'")
        Dim s As String = ""
        Dim GlAccount As String = ""
        Dim GlAccDesc As String = ""
        Dim desc As String = ""
        Dim SettlementType As String = ""
        Dim Type As Char = ""
        Dim FinancialEntry As Char = ""

        If dr IsNot Nothing AndAlso dr.Rows.Count > 0 Then
            desc = dr.Rows(0)("Description").ToString()
            s = dr.Rows(0)(1).ToString().ToUpper()
            SettlementType = dr.Rows(0)("Settlement_Type").ToString()
            GlAccount = dr.Rows(0)("Account_Code").ToString()
            GlAccDesc = dr.Rows(0)("Account_description").ToString()
            Type = dr.Rows(0)("Type").ToString().ToUpper()
            FinancialEntry = dr.Rows(0)("Financial_Entry").ToString().ToUpper()
        End If

        

        If s = "A" Then
            rdbAdd.IsChecked = True
        ElseIf s = "S" Then
            rdbSubtract.IsChecked = True
        ElseIf s = "N" Then
            rdbDoNothing.IsChecked = True
        End If

        If SettlementType = "SSE" Then
            ddlSettlementType.Text = "Salesman"
        ElseIf SettlementType = "DSC" Then
            ddlSettlementType.Text = "Discount"
        ElseIf SettlementType = "CRS" Then
            ddlSettlementType.Text = "Credit Sale"
        ElseIf SettlementType = "CSH" Then
            ddlSettlementType.Text = "Cash"
        ElseIf SettlementType = "CHQ" Then
            ddlSettlementType.Text = "Cheque"
        ElseIf SettlementType = "CSE" Then
            ddlSettlementType.Text = "Cash Shortage"
        ElseIf SettlementType = "ESE" Then
            ddlSettlementType.Text = "Empty Shortage"
        ElseIf SettlementType = "DEP" Then
            ddlSettlementType.Text = "Deposit"
        ElseIf SettlementType = "REF" Then
            ddlSettlementType.Text = "Refund"
        Else
            ddlSettlementType.Text = "Select"
        End If
       

        txtDescription.Text = desc '' added by Abhishek as on 19/06/2012
        txtGLAccount.Value = GlAccount
        txtGLAccountDesc.Text = GlAccDesc
        If Type = "Q" Then
            cmbType.Text = "Quick Settlement"
        ElseIf Type = "S" Then
            cmbType.Text = "Settlement"
        ElseIf Type = "B" Then
            cmbType.Text = "Both"
        End If
        '' added by Abhishek as on 23/10/2012
        If FinancialEntry = "Y" Then
            ChkFinancial.Checked = True
        ElseIf FinancialEntry = "N" Then
            ChkFinancial.Checked = False
        End If
        ' Code End Here
        btnSave.Text = "Update"
        btnDelete.Enabled = True

        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub
    Sub SaveData()
        If ddlSettlementType.Text = "Select" Then
            myMessages.blankValue("Settlement Type")
            ddlSettlementType.Focus()
            Return
        End If
        If cmbType.Text = "Select" Then
            myMessages.blankValue("Type")
            cmbType.Focus()
            Return
        End If

        If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) Then
            If clsCommon.myLen(fndSettleMentCode.Value) <= 0 Then
                myMessages.blankValue("Code")
                fndSettleMentCode.Focus()
                Return
            End If
        End If
       
        If fndSettleMentCode.Value <> "" Then
            If rdbAdd.IsChecked = False And rdbSubtract.IsChecked = False And rdbDoNothing.IsChecked = False Then
                myMessages.blankValue("Calculate")
                '''' Commented By abhishek kumar 19/06/2012 said by amit sir
                'ElseIf String.IsNullOrEmpty(txtGLAccount.Value) Then
                '    myMessages.blankValue("GL/Account")
            Else
                If btnSave.Text = "Save" Then
                    funSave()
                ElseIf btnSave.Text = "Update" Then
                    funUpdate()
                End If
                btnSave.Text = "Update"
                btnDelete.Enabled = True
            End If
        Else
            myMessages.blankValue("SettleMent Code")
            fndSettleMentCode.Focus()
        End If
    End Sub
    Private Sub funSave()
        Dim strflag As String = ""

        If rdbAdd.IsChecked = True Then
            strflag = "A"
        ElseIf rdbSubtract.IsChecked = True Then
            strflag = "S"
        ElseIf rdbDoNothing.IsChecked = True Then
            strflag = "N"
        End If

        Dim SettlementType As String = Nothing
        If ddlSettlementType.Text = "Salesman" Then
            SettlementType = "SSE"
        ElseIf ddlSettlementType.Text = "Discount" Then
            SettlementType = "DSC"
        ElseIf ddlSettlementType.Text = "Credit Sale" Then
            SettlementType = "CRS"
        ElseIf ddlSettlementType.Text = "Cash" Then
            SettlementType = "CSH"
        ElseIf ddlSettlementType.Text = "Cheque" Then
            SettlementType = "CHQ"
        ElseIf ddlSettlementType.Text = "Cash Shortage" Then
            SettlementType = "CSE"
        ElseIf ddlSettlementType.Text = "Empty Shortage" Then
            SettlementType = "ESE"
        ElseIf ddlSettlementType.Text = "Deposit" Then
            SettlementType = "DEP"
        ElseIf ddlSettlementType.Text = "Refund" Then
            SettlementType = "REF"
        End If

        Dim type As String = ""
        If cmbType.Text = "Quick Settlement" Then
            type = "Q"
        ElseIf cmbType.Text = "Settlement" Then
            type = "S"
        ElseIf cmbType.Text = "Both" Then
            type = "B"
        End If
        '' added by Abhishek as on 23/10/2012
        Dim FinancialEntry As Char
        If ChkFinancial.Checked = True Then
            FinancialEntry = "Y"
        Else
            FinancialEntry = "N"
        End If
        '' Code end Here
        Try
            If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False)) Then
                Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from tspl_SettleMent_Master where SettleMentCode='" & fndSettleMentCode.Value & "'")
                If ChkNewEntry = 0 Then
                    fndSettleMentCode.Value = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"), clsDocType.SettlementMaster, "", "")
                    If clsCommon.myLen(fndSettleMentCode.Value) <= 0 Then
                        Throw New Exception("Error in Code Generation")
                    End If
                End If
            End If
            connectSql.RunSp("sp_tspl_SettleMent_Master_insert", New SqlParameter("@SettleMentCode", fndSettleMentCode.Value), New SqlParameter("@Description", txtDescription.Text), New SqlParameter("@Calculate", strflag), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate()), New SqlParameter("Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate()), New SqlParameter("@Comp_Code", companyCode), New SqlParameter("@GLAccount", txtGLAccount.Value), New SqlParameter("@GLAccDesc", txtGLAccountDesc.Text), New SqlParameter("@type", type), New SqlParameter("@financialEntry  ", FinancialEntry))
            clsDBFuncationality.ExecuteNonQuery("update tspl_SettleMent_Master set SettleMent_Type='" + SettlementType + "' where SettleMentCode='" + fndSettleMentCode.Value + "'")
            myMessages.insert()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Private Sub funUpdate()
        Dim strflag As String = ""

        If rdbAdd.IsChecked = True Then
            strflag = "A"
        ElseIf rdbSubtract.IsChecked = True Then
            strflag = "S"
        ElseIf rdbDoNothing.IsChecked = True Then
            strflag = "N"
        End If

        Dim SettlementType As String = Nothing
        If ddlSettlementType.Text = "Salesman" Then
            SettlementType = "SSE"
        ElseIf ddlSettlementType.Text = "Discount" Then
            SettlementType = "DSC"
        ElseIf ddlSettlementType.Text = "Credit Sale" Then
            SettlementType = "CRS"
        ElseIf ddlSettlementType.Text = "Cash" Then
            SettlementType = "CSH"
        ElseIf ddlSettlementType.Text = "Cheque" Then
            SettlementType = "CHQ"
        ElseIf ddlSettlementType.Text = "Cash Shortage" Then
            SettlementType = "CSE"
        ElseIf ddlSettlementType.Text = "Empty Shortage" Then
            SettlementType = "ESE"
        ElseIf ddlSettlementType.Text = "Deposit" Then
            SettlementType = "DEP"
        ElseIf ddlSettlementType.Text = "Refund" Then
            SettlementType = "REF"
        End If
        Dim type As String = ""
        If cmbType.Text = "Quick Settlement" Then
            type = "Q"
        ElseIf cmbType.Text = "Settlement" Then
            type = "S"
        ElseIf cmbType.Text = "Both" Then
            type = "B"
        End If
        '' added by Abhishek as on 23/10/2012
        Dim FinancialEntry As Char
        If ChkFinancial.Checked = True Then
            FinancialEntry = "Y"
        Else
            FinancialEntry = "N"
        End If
        '' Code End Here
        Try
            connectSql.RunSp("sp_tspl_SettleMent_Master_update", New SqlParameter("@SettleMentCode", fndSettleMentCode.Value), New SqlParameter("@Description", txtDescription.Text), New SqlParameter("@Calculate", strflag), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate()), New SqlParameter("Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate()), New SqlParameter("@Comp_Code", companyCode), New SqlParameter("@GLAccount", txtGLAccount.Value), New SqlParameter("@GLAccDesc", txtGLAccountDesc.Text), New SqlParameter("@type", type), New SqlParameter("@financialEntry  ", FinancialEntry))
            clsDBFuncationality.ExecuteNonQuery("update tspl_SettleMent_Master set SettleMent_Type='" + SettlementType + "' where SettleMentCode='" + fndSettleMentCode.Value + "'")
            myMessages.update()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Private Sub funDelete()

        Try
            connectSql.RunSp("sp_tspl_SettleMent_Master_delete", New SqlParameter("@SettleMentCode", fndSettleMentCode.Value))
            myMessages.delete()
            btnSave.Text = "Save"
            btnDelete.Enabled = False
            rdbAdd.IsChecked = False
            rdbSubtract.IsChecked = False
            rdbDoNothing.IsChecked = False
            'If userCode <> "ADMIN" Then
            '    If funSetUserAccess() = False Then Exit Sub
            'End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try




    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        DeleteData()
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(fndSettleMentCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("You Cannot Delete Record")
            Exit Sub
        End If
        '' Code Added By Abhishek as on 19/10/2012 10:42 am  For Check If Exist SettlementCode in Tspl_Quick SettlementDetali,Tspl_RecieptAdjustmentDetails   then Don't Delete
        Dim SettlementCode As String
        Dim SettlementCode1 As String
        SettlementCode = clsDBFuncationality.getSingleValue("select SettleMent_Code  from tspl_QuickSettleMent_Detail where SettleMent_Code ='" & fndSettleMentCode.Value & "'")
        SettlementCode1 = clsDBFuncationality.getSingleValue("select Discount_Code  from TSPL_Receipt_Adjustment_Detail where Discount_Code ='" & fndSettleMentCode.Value & "'")
        If clsCommon.myLen(SettlementCode) > 0 Or clsCommon.myLen(SettlementCode1) > 0 Then
            common.clsCommon.MyMessageBoxShow("This record can't be deleted.It is used in another process")
            Exit Sub
        End If
        '' Code Ends 
        If myMessages.deleteConfirm() Then
            funDelete()
        End If

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        funReset()
    End Sub
    Private Sub funReset()
        fndSettleMentCode.Value = ""
        fndSettleMentCode.MyReadOnly = False
        txtDescription.Text = ""
        txtGLAccount.Value = ""
        txtGLAccountDesc.Text = ""
        rdbAdd.IsChecked = False
        rdbSubtract.IsChecked = False
        rdbDoNothing.IsChecked = False
        ddlSettlementType.Text = "Select"
        cmbType.Text = "Select"
        btnSave.Text = "Save"
        btnDelete.Enabled = False
        ChkFinancial.Checked = False
    End Sub

    'It Is Used To Give The Authority To User,To Access This Form (Vehicle Master) Or Not.(It Is Bassed On Mapping)
    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "SETTLEMENT-M"
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


    Private Sub FrmSettlementMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            'PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Private Sub txtGLAccount__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtGLAccount._MYValidating
        Dim qry As String = "select Account_Code as [Account],Description from tspl_gl_accounts inner join TSPL_GL_STRUCTURE on TSPL_GL_ACCOUNTS .Str_Code=TSPL_GL_STRUCTURE.Str_Code "
        txtGLAccount.Value = clsCommon.ShowSelectForm("Gl/AccountsFND", qry, "Account", "1<>(ISNULL(Seg_No1,0) +ISNULL(Seg_No2,0) +ISNULL(Seg_No3,0) +ISNULL(Seg_No4,0) +ISNULL(Seg_No5,0) +ISNULL(Seg_No6,0) +ISNULL(Seg_No7,0) +ISNULL(Seg_No8,0) +ISNULL(Seg_No9,0) +ISNULL(Seg_No10,0)) and ControlAccount <>'Y'", txtGLAccount.Value, "Account", isButtonClicked)
        ''lblVendorName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Name from TSPL_VENDOR_MASTER where Vendor_Code='" + txtVendorNo.Value + "'"))
        qry = "select Description from tspl_gl_accounts where Account_Code ='" + txtGLAccount.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            txtGLAccountDesc.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
        Else
            txtGLAccountDesc.Text = ""
        End If
    End Sub
    ' Added By Abhishek kumar as On 30/05/2012
    Public Sub FunImport()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Settlement Code", "Description", "Calculate", "Gl Account", "Account Description", "Type", "Financial Entry") Then
            Dim trans As SqlTransaction = Nothing
            Try
                connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim settlementcode As String = clsCommon.myCstr(grow.Cells(0).Value)
                    Dim desc As String = clsCommon.myCstr(grow.Cells(1).Value)
                    Dim calculate As String = clsCommon.myCstr(grow.Cells(2).Value)
                    Dim GlAccount As String = clsCommon.myCstr(grow.Cells(3).Value)
                    Dim accDesc As String = clsCommon.myCstr(grow.Cells(4).Value)
                    Dim type As String = clsCommon.myCstr(grow.Cells(5).Value).ToUpper()
                    Dim financialEntry As Char = clsCommon.myCstr(grow.Cells(6).Value).ToUpper()

                    If (String.IsNullOrEmpty(settlementcode) Or clsCommon.myLen(settlementcode) > 12) Then
                        Throw New Exception("Settlement Code can not be blank or greater then 12 length")
                    End If

                    If (clsCommon.myLen(desc) > 50) Then
                        Throw New Exception("Settlement Description can not be Blank or greater then 50 lenght ")
                    End If
                    If (String.IsNullOrEmpty(calculate) Or clsCommon.myLen(calculate) > 1) Then
                        Throw New Exception("Calculate  can not be Blank or greater then 1 lenght ")

                    End If
                    If (clsCommon.myLen(GlAccount) > 50) Then
                        Throw New Exception("Gl Account can not be Greater then 50 length")
                    End If
                    If (clsCommon.myLen(accDesc) > 100) Then
                        Throw New Exception("Account Description Can not be Greater then 100 length")
                    End If
                    If (clsCommon.myLen(financialEntry) > 1) Then
                        Throw New Exception("Financial Entry Can not be  Greater then 1 length")
                    End If

                    Dim qry As String = "select Count(*) from tspl_SettleMent_Master where SettleMentCode='" & settlementcode & "'"
                    Dim i As Integer = CInt(connectSql.RunScalar(trans, qry))
                    If (i = 0) Then
                        connectSql.RunSpTransaction(trans, "sp_tspl_SettleMent_Master_insert", New SqlParameter("@SettleMentCode", settlementcode.ToUpper), New SqlParameter("@Description", desc), New SqlParameter("@Calculate", calculate), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", companyCode), New SqlParameter("@GLAccount", GlAccount), New SqlParameter("@GLAccDesc", accDesc), New SqlParameter("@type", type), New SqlParameter("@financialEntry  ", financialEntry))

                    Else
                        connectSql.RunSpTransaction(trans, "sp_tspl_SettleMent_Master_update", New SqlParameter("@SettleMentCode", settlementcode.ToUpper), New SqlParameter("@Description", desc), New SqlParameter("@Calculate", calculate), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", companyCode), New SqlParameter("@GLAccount", GlAccount), New SqlParameter("@GLAccDesc", accDesc), New SqlParameter("@type", type), New SqlParameter("@financialEntry  ", financialEntry))

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
    ' For Import Data 
    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        FunImport()
    End Sub
    ' For Export Data
    Private Sub RadMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem3.Click
        Dim str As String
        str = "select SettleMentCode as [Settlement Code],Description as [Description],Calculate as [Calculate],Account_Code as [Gl Account],Account_Description as [Account Description],type as [Type],Financial_Entry as [Financial Entry] from tspl_SettleMent_Master "
        transportSql.ExporttoExcel(str, Me)
    End Sub
    ' For Close Screen
    Private Sub RadMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem4.Click
        Me.Close()
    End Sub

    Private Sub fndSettleMentCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndSettleMentCode._MYValidating
        Dim str As String = "select count(*) from tspl_SettleMent_Master where SettleMentCode ='" + fndSettleMentCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 Then
            fndSettleMentCode.MyReadOnly = False
        Else
            fndSettleMentCode.MyReadOnly = True
        End If
        If fndSettleMentCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = "select SettleMentCode as [SettleMentCode],Description from tspl_SettleMent_Master   "
            fndSettleMentCode.Value = clsCommon.ShowSelectForm("SettCodeFND", qry, "SettleMentCode", "", fndSettleMentCode.Value, " SettleMentCode", isButtonClicked)
            'txtDescription.Text = clsDBFuncationality.getSingleValue("Select acct_set_desc from tspl_vendor_account_set where acct_set_code='" + fndSettleMentCode.Value + "'")
            If fndSettleMentCode.Value IsNot Nothing Then
                btnDelete.Enabled = True
            Else
                btnDelete.Enabled = False
            End If
        End If
        fndSettleMentCode_textchanged()
    End Sub

    Private Sub fndSettleMentCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavigatorType As common.NavigatorType) Handles fndSettleMentCode._MYNavigator
        Dim qst As String = "select SettleMentCode as [SettleMentCode],Description ,type from tspl_SettleMent_Master  where  2=2 "
        Select Case NavigatorType
            Case NavigatorType.Current
                '  qst += "and assign_to='" + txtassign.Value + "' "
                ' qst += "and job_code in ('" + txtcode1.Value + "')"
            Case NavigatorType.Next
                qst += "and SettleMentCode in (select min(SettleMentCode) from tspl_SettleMent_Master where SettleMentCode>'" + fndSettleMentCode.Value + "'   ) "
            Case NavigatorType.First
                qst += "and SettleMentCode in (select MIN(SettleMentCode) from tspl_SettleMent_Master  )"
            Case NavigatorType.Last
                qst += "and SettleMentCode in (select Max(SettleMentCode) from tspl_SettleMent_Master  )"
            Case NavigatorType.Previous
                qst += "and SettleMentCode in (select max(SettleMentCode) from tspl_SettleMent_Master where SettleMentCode<'" + fndSettleMentCode.Value + "'   )"
        End Select
        ' fun_gridfill()
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            fndSettleMentCode.Value = clsCommon.myCstr(dt.Rows(0)("SettleMentCode"))
            txtDescription.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
        End If
        'TextChanged()
        If fndSettleMentCode.Value IsNot Nothing Then
            btnDelete.Enabled = True
        Else
            btnDelete.Enabled = False

        End If
        fndSettleMentCode_textchanged()
    End Sub

   
   
 


  

  
End Class
