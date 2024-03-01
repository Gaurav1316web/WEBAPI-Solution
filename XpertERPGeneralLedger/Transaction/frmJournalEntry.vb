Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Collections
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Imports System.Text.RegularExpressions
Imports common
'GKD/28/05/18-000137 by balwinder on 28/05/2018
Public Class frmJournalEntry
    Inherits FrmMainTranScreen
#Region "Variable"

    Public strVoucherNo As String = ""
    Private isInsideLoadData As Boolean = True
    Private isCellValueChangedOpen As Boolean = False

    Dim sql As String
    Dim ds As DataSet
    Dim dr As SqlDataReader
    Dim myDataTable As DataTable
    Dim mycommand As New SqlCommand
    Dim myreader As DataTable '' added By abhishek as on 12/10/2012
    Dim strcon = connectSql.SqlCon()
    Dim myconn = New SqlConnection(strcon)
    Dim userCode, companyCode, Program_Code As String
    Dim i As Integer
    Dim btntooltip As ToolTip = New ToolTip()

    'Tally
    Dim sCompany As String = "Tecxpert"
    Dim TallyIP As String = "alpine-PC"
    Dim TallyPort As String = "9000"
    Dim TM As common.Tally
    Dim SettingCostCenter As Boolean = False
    Dim ERPStartDate As Date
    Dim settLockDate As String
    Dim AllowJEofDifferentLocationOnJournalEntry As Boolean = False
    Dim CostCenterAndHirerachyCodeUpdateAfterPost As Boolean = False
#End Region


    Private Sub GLTrans_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SettingCostCenter = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowHierarchyAndCostCenterInAPInvoiceEntry, clsFixedParameterCode.ShowHierarchyAndCostCenterInAPInvoiceEntry, Nothing)) = 1)
        settLockDate = clsFixedParameter.GetData(clsFixedParameterType.LockDate, clsFixedParameterCode.LockDate, Nothing)
        AllowJEofDifferentLocationOnJournalEntry = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowJEofDifferentLocationOnJournalEntry, clsFixedParameterCode.AllowJEofDifferentLocationOnJournalEntry, Nothing)) = 1, True, False)
        CostCenterAndHirerachyCodeUpdateAfterPost = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CostCenterAndHirerachyCodeUpdateAfterPost, clsFixedParameterCode.CostCenterAndHirerachyCodeUpdateAfterPost, Nothing)) = 1, True, False)

        Try
            ERPStartDate = clsCommon.myCDate(objCommonVar.ERPStartDate)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, "Invalid ERP Start Date", Me.Text)
            Me.Close()
        End Try


        SetUserMgmtNew()
        UcAttachment1.Form_ID = MyBase.Form_ID
        fndVoucher.MyReadOnly = False
        Dim gdColDr As GridViewDecimalColumn = TryCast(gdAcc1.Columns(3), GridViewDecimalColumn)
        gdColDr.DataSourceNullValue = 0
        Dim gdColCr As GridViewDecimalColumn = TryCast(gdAcc1.Columns(4), GridViewDecimalColumn)
        gdColCr.DataSourceNullValue = 0
        Dim gdDate As GridViewDateTimeColumn = TryCast(gdAcc1.Columns(7), GridViewDateTimeColumn)
        gdDate.DataSourceNullValue = connectSql.serverDate()
        fndCode.Enabled = False
        txtCodeDesc.Enabled = False
        rdbOther.IsChecked = True
        dtRevese.Enabled = False
        txtSrcDoc.Enabled = False
        txtDocDesc.Enabled = False
        dtSrc.Enabled = False
        fndVoucher.MyReadOnly = True
        txtSrcDesc.ReadOnly = True
        dtVoucher.Value = clsCommon.GETSERVERDATE()
        dtSrc.Value = clsCommon.GETSERVERDATE()
        dtRevese.Value = clsCommon.GETSERVERDATE()
        chkReverse.Checked = False
        btnSendToTally.Enabled = False
        btnAuth.Enabled = False
        btnProAuth.Enabled = False
        txtCr.ReadOnly = True
        txtDr.ReadOnly = True
        btntooltip.SetToolTip(btnSave, "Press Alt+S for save/update function")
        btntooltip.SetToolTip(btnDelete, "Press Alt+D for delete the data")
        btntooltip.SetToolTip(btnAuth, "Press Alt+P for authorise")
        btntooltip.SetToolTip(btnClose, "Press Esc for close screen")
        btntooltip.SetToolTip(btnNew, "Press Alt+N for new transaction")
        btntooltip.SetToolTip(btnPrint, "Press Ctrl+P for Print ")
        funNew()
        funSrcLoad()
        txtVoucherDesc.Focus()
        If clsCommon.myLen(strVoucherNo) > 0 Then
            fndVoucher.Value = strVoucherNo
            funFill()
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            fndVoucher.Value = clsCommon.myCstr(Me.Tag)
            funFill()
        End If
        SetLength()
        isInsideLoadData = False
        RadGroupBox2.Enabled = False
        btnSendToTally.Visible = False
        'By balwinder against ticket no-UDL/22/05/18-000173
        'If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then 
        'chkIndAS.Visible = False
        'End If

        gdAcc1.Columns(8).IsVisible = SettingCostCenter
        gdAcc1.Columns(9).IsVisible = SettingCostCenter
        If clsCommon.CompairString(Program_Code, clsUserMgtCode.journalEntry) = CompairStringResult.Equal Then
            txtReverseVoucher.Visible = True
            MyLabel5.Visible = True
        Else
            txtReverseVoucher.Visible = False
            MyLabel5.Visible = False
        End If


    End Sub

    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub


    Public Sub New(ByVal user As String, ByVal company As String, ByVal strcode As String)
        InitializeComponent()
        userCode = user
        companyCode = company
        strVoucherNo = strcode
    End Sub
    Public Sub New(ByVal user As String, ByVal company As String, ByVal strcode As String, ByVal strProgram_Code As String)
        InitializeComponent()
        userCode = user
        companyCode = company
        strVoucherNo = strcode
        Program_Code = strProgram_Code
    End Sub



#Region "ButtonEvents"
    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeform()
    End Sub
    Public Sub closeform()
        'Try
        '    Dim qry As String = "select Program_code,replace(Form_Name,' ','') as Form_Name,Mapped_Table_Name,Mapped_Column_Name,replace(Display_Label_Text,' ','') as Display_Label_Text from TSPL_APP_METADATA order by Program_Code"
        '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        '    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
        '        Dim strOldProgramCode As String = clsCommon.myCstr(dt.Rows(0)("Program_code"))
        '        Dim strOldViewName As String = clsCommon.myCstr(dt.Rows(0)("Form_Name"))
        '        Dim strOldTableName As String = clsCommon.myCstr(dt.Rows(0)("Mapped_Table_Name"))
        '        qry = ""
        '        For Each dr As DataRow In dt.Rows
        '            Dim strCurrProgramCode As String = clsCommon.myCstr(dr("Program_code"))
        '            If Not clsCommon.CompairString(strOldProgramCode, strCurrProgramCode) = CompairStringResult.Equal Then
        '                If clsCommon.myLen(qry) > 0 Then
        '                    CreateView(strOldViewName, qry, strOldTableName)

        '                    strOldProgramCode = clsCommon.myCstr(dr("Program_code"))
        '                    strOldViewName = clsCommon.myCstr(dr("Form_Name"))
        '                    strOldTableName = clsCommon.myCstr(dr("Mapped_Table_Name"))
        '                    qry = ""
        '                End If
        '            End If
        '            If clsCommon.myLen(qry) > 0 Then
        '                qry += ","
        '            End If
        '            qry += clsCommon.myCstr(dr("Mapped_Table_Name")) + "." + clsCommon.myCstr(dr("Mapped_Column_Name")) + " as " + clsCommon.myCstr(dr("Display_Label_Text"))
        '        Next
        '        CreateView(strOldViewName, qry, strOldTableName)
        '    End If
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        'End Try



        Me.Close()
    End Sub

    'Function CreateView(ByVal strOldViewName As String, ByVal qry As String, ByVal strOldTableName As String) As Boolean
    '    Try
    '        clsDBFuncationality.ExecuteNonQuery("drop view " + strOldViewName)
    '    Catch ex As Exception
    '    End Try
    '    clsDBFuncationality.ExecuteNonQuery("create view " + strOldViewName + " as (select " + qry + " from " + strOldTableName + " )")
    '    Return True
    'End Function
    '=================Added by preeti Gupta [02/02/2017]
    Function AllowToSave1() As Boolean
        Try
            If AllowFutureDateTransaction(dtVoucher.Value, Nothing) = False Then
                dtVoucher.Focus()
                Return False
            End If

            Dim arrAccountCode As New List(Of String)
            For ii As Integer = 0 To gdAcc1.Rows.Count - 1
                Dim strACode As String = clsCommon.myCstr(gdAcc1.Rows(ii).Cells(1).Value)
                If clsCommon.myLen(strACode) > 0 Then
                    If Not arrAccountCode.Contains(strACode) Then
                        arrAccountCode.Add(strACode)
                    End If
                End If
            Next
            If arrAccountCode IsNot Nothing AndAlso arrAccountCode.Count > 0 Then
                If clsCommon.GetDateWithStartTime(dtVoucher.Value) >= clsCommon.GetDateWithStartTime(ERPStartDate) Then
                    'richa 17 SEp,2019 TEC/03/07/19-000927
                    Dim qry As String = "select Account_Code from TSPL_GL_ACCOUNTS where Account_Code in (" + clsCommon.GetMulcallString(arrAccountCode) + ") and ControlAccount<>'N' AND TSPL_GL_ACCOUNTS.Account_Code NOT IN  (select TSPL_CONTROL_ACC_MAPPING.Account_Code  from TSPL_CONTROL_ACC_MAPPING where IsForJE  =1)"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        Throw New Exception("Can not select control Account -" + clsCommon.myCstr(dt.Rows(0)("Account_Code")))
                    End If
                End If
                If clsCommon.myLen(settLockDate) > 0 Then
                    If clsCommon.GetDateWithStartTime(dtVoucher.Value) < clsCommon.GetDateWithStartTime(clsCommon.myCDate(settLockDate)) Then
                        Throw New Exception("Can not create Financial transaction before Lock Date [" + settLockDate + "]")
                    End If
                End If



            End If

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
    End Function
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If AllowToSave1() Then
            savedata(False)
        End If

    End Sub


    Public Sub savedata(ByVal ChekPostBtn As Boolean)
        Dim AmtCr As Decimal = Convert.ToDecimal(txtCr.Text)
        Dim AmtDr As Decimal = Convert.ToDecimal(txtDr.Text)

        '-----------for post check on update--------
        If btnSave.Text = "Update" And clsCommon.myLen(fndVoucher.Value) > 0 Then
            Dim strchk As String = "select Authorized from TSPL_JOURNAL_MASTER where Voucher_No='" + fndVoucher.Value + "'"
            Dim chkpost As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strchk))
            If chkpost = "A" Then
                clsCommon.MyMessageBoxShow(Me, "Transection already posted", Me.Text)
                Exit Sub
            End If
        End If
        If clsCommon.myLen(txtLocation.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select location", Me.Text)
            txtLocation.Focus()
            Exit Sub
        End If
        Try
            UcAttachment1.AllowToSave()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Exit Sub
        End Try

        '-------------------------------------------

        If AmtCr <> AmtDr Then
            If ChekPostBtn = True Then
                If fndSrcCode.Value <> String.Empty Then
                    If gdAcc1.Rows.Count > 0 Then
                        If btnSave.Text = "Save" Then
                            If CheckLocForLock() Then
                                If ChekPostBtn = True Then    '' This Change Has Done by Abhishek as On 9/1/2012 For Save Data Before Posting
                                    funInsert(True)
                                Else
                                    funInsert(False)

                                End If

                            End If

                        ElseIf btnSave.Text = "Update" Then

                            ''-----------for post check on update--------
                            'Dim strchk As String = "select Authorized from TSPL_JOURNAL_MASTER where Voucher_No='" + fndVoucher.Value + "'"
                            'Dim chkpost As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strchk))
                            'If chkpost = "A" Then
                            '    clsCommon.MyMessageBoxShow("Transection already posted")
                            '    Exit Sub
                            'End If
                            ''----------------------------------------------




                            If CheckLocForLock() Then
                                funUpdate()
                                If ChekPostBtn = True Then   '' This Change Has Done by Abhishek as On 9/1/2012 For Save Data Before Posting
                                Else
                                    myMessages.update()
                                End If

                            End If

                            If funUpdate() = True Then
                                'myMessages.update()
                            End If
                        End If
                    Else
                        Return
                    End If
                Else
                    common.clsCommon.MyMessageBoxShow(Me, "Select Source Code", "Journal-Entry", MessageBoxButtons.OK)
                    fndSrcCode.Focus()
                End If
            Else
                If common.clsCommon.MyMessageBoxShow(Me, "Out Of Balance Amount,Do you want to Save ?", "Journal-Entry", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                    Exit Sub
                Else
                    If fndSrcCode.Value <> String.Empty Then
                        If gdAcc1.Rows.Count > 0 Then
                            If btnSave.Text = "Save" Then
                                If CheckLocForLock() Then
                                    If ChekPostBtn = True Then    '' This Change Has Done by Abhishek as On 9/1/2012 For Save Data Before Posting
                                        funInsert(True)
                                    Else
                                        funInsert(False)

                                    End If

                                End If

                            ElseIf btnSave.Text = "Update" Then
                                If CheckLocForLock() Then
                                    funUpdate()
                                    If ChekPostBtn = True Then   '' This Change Has Done by Abhishek as On 9/1/2012 For Save Data Before Posting
                                    Else
                                        myMessages.update()
                                    End If

                                End If

                                If funUpdate() = True Then
                                    'myMessages.update()
                                End If
                            End If
                        Else
                            Return
                        End If
                    Else
                        common.clsCommon.MyMessageBoxShow("Select Source Code", "Journal-Entry", MessageBoxButtons.OK)
                        fndSrcCode.Focus()
                    End If
                End If
            End If
        ElseIf AmtCr = AmtDr Then
            If fndSrcCode.Value <> String.Empty Then
                If gdAcc1.Rows.Count > 0 Then
                    If btnSave.Text = "Save" Then
                        If CheckLocForLock() Then
                            If ChekPostBtn = True Then   '' This Change Has Done by Abhishek as On 9/1/2012 For Save Data Before Posting
                                funInsert(True)
                            Else
                                funInsert(False)
                            End If

                        End If

                    ElseIf btnSave.Text = "Update" Then
                        If CheckLocForLock() Then
                            funUpdate()
                            If ChekPostBtn = True Then   '' This Change Has Done by Abhishek as On 9/1/2012 For Save Data Before Posting
                            Else
                                myMessages.update()
                            End If

                        End If
                        If funUpdate() = True Then

                        End If
                    End If
                Else
                    Return
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "Select Source Code", "Journal-Entry", MessageBoxButtons.OK)
                fndSrcCode.Focus()
            End If
        End If
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        deletedata()
    End Sub
    Public Sub deletedata()
        If fndVoucher.Value <> String.Empty Then
            Dim Reason As String = ""
            If myMessages.deleteConfirm() Then
                If clsCancelLog.CheckForReasonOnDelete() Then
                    '' REASON FOR DELETE 
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Delete"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If
                End If
                funDelete()
                saveCancelLog(Reason, "Delete", Nothing)
                funNew()
            Else
                Exit Sub
            End If
        Else
            Return
        End If
    End Sub
    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.fndVoucher.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        funNew()
    End Sub
    Private Sub btnAuth_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAuth.Click
        '' This Change Has Done by Abhishek as On 9/1/2012 For Save Data Before Posting
        If myMessages.postConfirm = True Then
            savedata(True)
            btnPostClick(True)
            'Else
            '    btnPostClick(False)
        End If
        '' Code End Here
    End Sub
    Private Sub btnPostClick(ByVal CheckPostBtn As Boolean)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            clsGLAccount.CheckYearEndAccountFilledInSegment(fndVoucher.Value, trans)
            If CheckPostBtn = True Then
                Dim AmtCr As Decimal = Convert.ToDecimal(txtCr.Text)
                Dim AmtDr As Decimal = Convert.ToDecimal(txtDr.Text)
                If AmtCr = AmtDr Then
                    'funUpdate(trans)
                    clsGLEntry.funGLPOST(fndVoucher.Value, dtVoucher.Value.Date, trans)
                    trans.Commit()
                    authorisedata()
                Else
                    Throw New Exception("Out Of Balance Amount .")
                End If
            Else
                '' Commented full else part as discussed with balwinder sir ,no need to ask post msg again
                'If myMessages.postConfirm = True Then
                '    Dim AmtCr As Decimal = Convert.ToDecimal(txtCr.Text)
                '    Dim AmtDr As Decimal = Convert.ToDecimal(txtDr.Text)
                '    If AmtCr = AmtDr Then
                '        'funUpdate(trans)
                '        clsGLEntry.funGLPOST(fndVoucher.Value, dtVoucher.Value.Date, trans)

                '        ''End Fiscal Year Entry
                '        Dim EntryDate As String = clsCommon.GetPrintDate(dtVoucher.Value, "dd/MMM/yyyy")
                '        Dim qry As String = "select is_End_Year_Proceed from TSPL_Fiscal_Year_Master where convert(date, '" + EntryDate + "',103)>= convert(date, Start_Date,103) and convert(date, '" + EntryDate + "',103)<=CONVERT(date, End_Date,103)"
                '        Dim dtable As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                '        If dtable Is Nothing OrElse dtable.Rows.Count <= 0 Then
                '            Throw New Exception("Please create financial year which contains " + EntryDate)
                '        End If

                '        If dtVoucher.Value < objCommonVar.CurrFiscalStartDate OrElse dtVoucher.Value > objCommonVar.CurrFiscalEndDate Then
                '            Throw New Exception("Transaction date " + EntryDate + " is not exists in the current financial year")
                '        End If
                '        If clsCommon.myCdbl(dtable.Rows(0)("is_End_Year_Proceed")) = 1 Then
                '            transportSql.CreateJEForEndYear(fndVoucher.Value, trans)
                '        End If
                '        ''End of End Fiscal Year Entry

                '        Dim objSendToTally As New clsSendToTally()
                '        objSendToTally.SendToTally_JournalEntry(fndVoucher.Value, trans)
                '        trans.Commit()
                '        authorisedata()
                '    Else
                '        Throw New Exception("Out Of Balance Amount .")
                '    End If
                'End If
            End If
            '' 31-July-2015
            'Dim ChkRev As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Auto_Reverse FROM TSPL_JOURNAL_MASTER WHERE Voucher_No ='" & fndVoucher.Value & "'", trans))
            'If clsCommon.CompairString(ChkRev, "R") <> CompairStringResult.Equal AndAlso chkReverse.Checked = True Then
            '    clsJournalEntryHeader.AutoReverse(clsCommon.myCstr(fndVoucher.Value), dtRevese.Value, trans)
            '    clsDBFuncationality.ExecuteNonQuery("update TSPL_JOURNAL_MASTER SET Authorized= 'A' WHERE Voucher_No='" + Me.fndVoucher.Value + "' ", trans)
            '    trans.Commit()
            'End If
            ''
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub authorisedata()
        'sql = "update TSPL_JOURNAL_MASTER SET Authorized= 'A' WHERE Voucher_No='" + Me.fndVoucher.value + "' "
        'connectSql.RunSql(sql)
        'funUpdate()
        funFill()
        If common.clsCommon.MyMessageBoxShow(Me, "Voucher No. " + fndVoucher.Value.Trim + " Posted Successfully. Do You Want To Print Voucher ?", "Journal-Entry", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        Else
            'Dim strQuery As String = " SELECT TSPL_JOURNAL_MASTER.Voucher_No, TSPL_JOURNAL_MASTER.Voucher_Date, TSPL_JOURNAL_MASTER.Voucher_Desc, " & _
            '      "   TSPL_JOURNAL_MASTER.Source_Code, TSPL_JOURNAL_MASTER.Source_doc_No,TSPL_JOURNAL_MASTER.Posting_Date, TSPL_JOURNAL_MASTER.Total_Debit_Amt, " & _
            '      "   TSPL_JOURNAL_MASTER.Total_Credit_Amt, TSPL_JOURNAL_DETAILS.Account_code, TSPL_JOURNAL_DETAILS.Account_Desc,  " & _
            '      "   TSPL_JOURNAL_DETAILS.Amount,case when Amount>=0 then  Amount else 0 end as DrAmt,case when Amount<0 then -1 * Amount else 0 end as CrAmt, TSPL_JOURNAL_DETAILS.Description, TSPL_JOURNAL_DETAILS.Reference,  " & _
            '      "   TSPL_JOURNAL_DETAILS.Posting_Date AS [Dtline PostDt], TSPL_JOURNAL_DETAILS.Detail_Line_No, " & _
            '      "  TSPL_JOURNAL_MASTER.Created_By,TSPL_JOURNAL_MASTER.Modify_By,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2 ,(SELECT     CASE WHEN TSPL_JOURNAL_MASTER.Authorized = 'N' THEN 'OPEN' WHEN TSPL_JOURNAL_MASTER.Authorized = 'A' THEN 'Authorize' END" & _
            '      "   AS Expr1) AS Status, TSPL_JOURNAL_MASTER.Remarks" & _
            '      "   FROM         TSPL_JOURNAL_MASTER INNER JOIN TSPL_JOURNAL_DETAILS ON TSPL_JOURNAL_MASTER.Journal_No = TSPL_JOURNAL_DETAILS.Journal_No  " & _
            '      "  AND  TSPL_JOURNAL_MASTER.Voucher_No = TSPL_JOURNAL_DETAILS.Voucher_No  left outer join  TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code  =TSPL_JOURNAL_MASTER.Comp_Code where TSPL_JOURNAL_MASTER.Voucher_No = '" + fndVoucher.Value + "'  "

            Dim strQuery As String = "  SELECT TSPL_JOURNAL_MASTER.Source_Narration, TSPL_JOURNAL_MASTER.Source_Type, TSPL_JOURNAL_MASTER.CustVend_Code,TSPL_JOURNAL_MASTER.CustVend_Name, TSPL_JOURNAL_MASTER.Voucher_No, TSPL_JOURNAL_MASTER.Voucher_Date,     case when  TSPL_JOURNAL_MASTER.CustVend_Code='' then   TSPL_JOURNAL_MASTER.Voucher_Desc else TSPL_JOURNAL_MASTER.Voucher_Desc+'  for ' +TSPL_JOURNAL_MASTER.CustVend_Code+' - '+TSPL_JOURNAL_MASTER.CustVend_Name end as Voucher_Desc, " &
                  "   TSPL_JOURNAL_MASTER.Source_Code,TSPL_JOURNAL_MASTER.Source_Desc, TSPL_JOURNAL_MASTER.Source_doc_No,convert(varchar(15),TSPL_JOURNAL_MASTER.Source_Doc_Date,103) as Source_Doc_Date,TSPL_JOURNAL_MASTER.Posting_Date, TSPL_JOURNAL_MASTER.Total_Debit_Amt, " &
                  "   TSPL_JOURNAL_MASTER.Total_Credit_Amt, TSPL_JOURNAL_DETAILS.Account_code, TSPL_JOURNAL_DETAILS.Account_Desc,  " &
                  "   TSPL_JOURNAL_DETAILS.Amount ,case when Amount>=0 then  Amount else 0 end as DrAmt,case when Amount<0 then -1 * Amount else 0 end as CrAmt, TSPL_JOURNAL_DETAILS.Description, TSPL_JOURNAL_DETAILS.Reference,  " &
                  "   TSPL_JOURNAL_DETAILS.Posting_Date AS [Dtline PostDt], TSPL_JOURNAL_DETAILS.Detail_Line_No, " &
                  " TSPL_JOURNAL_MASTER.Created_By, Case When Authorized='A' Then TSPL_JOURNAL_MASTER.Modify_By else '' End as Modify_By, TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2 ,(SELECT     CASE WHEN TSPL_JOURNAL_MASTER.Authorized = 'N' THEN 'OPEN' WHEN TSPL_JOURNAL_MASTER.Authorized = 'A' THEN 'Authorize' END" &
                   "  AS Expr1) AS Status, TSPL_JOURNAL_MASTER.Remarks ,TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code ,TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name, TSPL_HIRERACHY_LEVEL_MASTER.Hirerachy_Code,TSPL_HIRERACHY_LEVEL_MASTER.Description as HIRERACHY_LEVEL " &
                  "   FROM         TSPL_JOURNAL_MASTER INNER JOIN TSPL_JOURNAL_DETAILS ON TSPL_JOURNAL_MASTER.Journal_No = TSPL_JOURNAL_DETAILS.Journal_No  " &
                  "  AND  TSPL_JOURNAL_MASTER.Voucher_No = TSPL_JOURNAL_DETAILS.Voucher_No  left outer join  TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code  =TSPL_JOURNAL_MASTER.Comp_Code   left outer join TSPL_COST_CENTRE_FINANCIAL on TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code=TSPL_JOURNAL_DETAILS.Cost_Centre_Code " &
                  " left outer join TSPL_HIRERACHY_LEVEL_MASTER on TSPL_HIRERACHY_LEVEL_MASTER.Hirerachy_Code=TSPL_JOURNAL_DETAILS.Hirerachy_Code where TSPL_JOURNAL_MASTER.Voucher_No = '" + fndVoucher.Value + "'  order by Detail_Line_No "
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.GeneralLedger, clsDBFuncationality.GetDataTable(strQuery), "crptGLVoucher", "Journal Voucher Report")
            frmCRV = Nothing
        End If
    End Sub
    Public Sub funSrcLoad()
        Dim strQ As String = "select SourceCode as [Source Code] from TSPL_GL_SOURCECODE where SourceLedger  ='GL' and SourceType ='JE' "
        Dim SrcType As String = connectSql.RunScalar(strQ)
        If SrcType = "" Then
            common.clsCommon.MyMessageBoxShow(Me, "Create GL-JE Soruce Code !", "Journal-Entry", MessageBoxButtons.OK, RadMessageIcon.Info)
        Else
            fndSrcCode.Value = SrcType
        End If
    End Sub

    Private Sub btnProAuth_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProAuth.Click
        proauthorisedata()
    End Sub
    Public Sub proauthorisedata()
        sql = "update TSPL_JOURNAL_MASTER SET Provisional_Post= 'P' WHERE Voucher_No='" + Me.fndVoucher.Value + "' "
        connectSql.RunSql(sql)
        funFill()
        common.clsCommon.MyMessageBoxShow(Me, "Voucher No. " + fndVoucher.Value.Trim + " Provisionly Authorised.", "Journal-Entry", MessageBoxButtons.OK)
    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            If clsCommon.myLen(fndVoucher.Value) > 0 Then
                PrintData(fndVoucher.Value, "")
            Else
                clsCommon.MyMessageBoxShow("")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub PrintData(ByVal StrCode As String, ByVal FormID As String)
        If btnSave.Text = "Save" AndAlso clsCommon.myLen(FormID) <= 0 Then
            Return
        Else
            ''Dim strQuery As String = " SELECT TSPL_JOURNAL_MASTER.Voucher_No, TSPL_JOURNAL_MASTER.Voucher_Date, TSPL_JOURNAL_MASTER.Voucher_Desc, " & _
            ''        "   TSPL_JOURNAL_MASTER.Source_Code, TSPL_JOURNAL_MASTER.Source_doc_No,TSPL_JOURNAL_MASTER.Posting_Date, TSPL_JOURNAL_MASTER.Total_Debit_Amt, " & _
            ''        "   TSPL_JOURNAL_MASTER.Total_Credit_Amt, TSPL_JOURNAL_DETAILS.Account_code, TSPL_JOURNAL_DETAILS.Account_Desc,  " & _
            ''        "   TSPL_JOURNAL_DETAILS.Amount, TSPL_JOURNAL_DETAILS.Description, TSPL_JOURNAL_DETAILS.Reference,  " & _
            ''        "   TSPL_JOURNAL_DETAILS.Posting_Date AS [Dtline PostDt], TSPL_JOURNAL_DETAILS.Detail_Line_No, " & _
            ''        " TSPL_JOURNAL_MASTER.Created_By,TSPL_JOURNAL_MASTER.Modify_By,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2 ,(SELECT     CASE WHEN TSPL_JOURNAL_MASTER.Authorized = 'N' THEN 'OPEN' WHEN TSPL_JOURNAL_MASTER.Authorized = 'A' THEN 'Authorize' END" & _
            ''         "  AS Expr1) AS Status, TSPL_JOURNAL_MASTER.Remarks" & _
            ''        "   FROM         TSPL_JOURNAL_MASTER INNER JOIN TSPL_JOURNAL_DETAILS ON TSPL_JOURNAL_MASTER.Journal_No = TSPL_JOURNAL_DETAILS.Journal_No  " & _
            ''        "  AND  TSPL_JOURNAL_MASTER.Voucher_No = TSPL_JOURNAL_DETAILS.Voucher_No  left outer join  TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code  =TSPL_JOURNAL_MASTER.Comp_Code where TSPL_JOURNAL_MASTER.Voucher_No = '" + fndVoucher.Value + "'  order by Detail_Line_No "


            ''RICHA KDI/11/07/18-000401 PICK CUSTOMER OR VENDOR NAME FROM THEIR MASTER TABLE
            Dim strQuery As String = "  SELECT TSPL_JOURNAL_MASTER.Source_Narration, TSPL_JOURNAL_MASTER.Source_Type, TSPL_JOURNAL_MASTER.CustVend_Code,CASE WHEN ISNULL(TSPL_JOURNAL_MASTER.Source_Type,'')='C' THEN TSPL_CUSTOMER_MASTER.CUSTOMER_NAME  WHEN ISNULL(TSPL_JOURNAL_MASTER.Source_Type,'')='V' THEN TSPL_VENDOR_MASTER.VENDOR_NAME ELSE TSPL_JOURNAL_MASTER.CustVend_Name end AS CustVend_Name, TSPL_JOURNAL_MASTER.Voucher_No,  convert(varchar(15),TSPL_JOURNAL_MASTER.Voucher_Date,103) as Voucher_Date,     case when  TSPL_JOURNAL_MASTER.CustVend_Code='' then   TSPL_JOURNAL_MASTER.Voucher_Desc else TSPL_JOURNAL_MASTER.Voucher_Desc+'  for ' +TSPL_JOURNAL_MASTER.CustVend_Code+' - '+  CASE WHEN ISNULL(TSPL_JOURNAL_MASTER.Source_Type,'')='C' THEN TSPL_CUSTOMER_MASTER.CUSTOMER_NAME  WHEN ISNULL(TSPL_JOURNAL_MASTER.Source_Type,'')='V' THEN TSPL_VENDOR_MASTER.VENDOR_NAME  end end as Voucher_Desc, " &
            "   TSPL_JOURNAL_MASTER.Source_Code,TSPL_JOURNAL_MASTER.Source_Desc, TSPL_JOURNAL_MASTER.Source_doc_No,convert(varchar(15),TSPL_JOURNAL_MASTER.Source_Doc_Date,103) as Source_Doc_Date,TSPL_JOURNAL_MASTER.Posting_Date, TSPL_JOURNAL_MASTER.Total_Debit_Amt, " &
            "   TSPL_JOURNAL_MASTER.Total_Credit_Amt, TSPL_JOURNAL_DETAILS.Account_code, TSPL_JOURNAL_DETAILS.Account_Desc,  " &
            "   TSPL_JOURNAL_DETAILS.Amount ,case when Amount>=0 then  Amount else 0 end as DrAmt,case when Amount<0 then -1 * Amount else 0 end as CrAmt, TSPL_JOURNAL_DETAILS.Description, TSPL_JOURNAL_DETAILS.Reference,  " &
            "   TSPL_JOURNAL_DETAILS.Posting_Date AS [Dtline PostDt], TSPL_JOURNAL_DETAILS.Detail_Line_No, " &
            " TSPL_JOURNAL_MASTER.Created_By, Case When Authorized='A' Then TSPL_JOURNAL_MASTER.Modify_By else '' End as Modify_By, TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2 ,(SELECT     CASE WHEN TSPL_JOURNAL_MASTER.Authorized = 'N' THEN 'OPEN' WHEN TSPL_JOURNAL_MASTER.Authorized = 'A' THEN 'Authorize' END" &
            "  AS Expr1) AS Status, TSPL_JOURNAL_MASTER.Remarks ,TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code ,TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name, TSPL_HIRERACHY_LEVEL_MASTER.Hirerachy_Code,TSPL_HIRERACHY_LEVEL_MASTER.Description as HIRERACHY_LEVEL " &
            "  ,TSPL_JOURNAL_MASTER.TapalNo,TSPL_JOURNAL_MASTER.DateAndTime FROM         TSPL_JOURNAL_MASTER INNER JOIN TSPL_JOURNAL_DETAILS ON TSPL_JOURNAL_MASTER.Journal_No = TSPL_JOURNAL_DETAILS.Journal_No  " &
            "  AND  TSPL_JOURNAL_MASTER.Voucher_No = TSPL_JOURNAL_DETAILS.Voucher_No  left outer join  TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code  =TSPL_JOURNAL_MASTER.Comp_Code   left outer join TSPL_COST_CENTRE_FINANCIAL on TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code=TSPL_JOURNAL_DETAILS.Cost_Centre_Code " &
            " left outer join TSPL_HIRERACHY_LEVEL_MASTER on TSPL_HIRERACHY_LEVEL_MASTER.Hirerachy_Code=TSPL_JOURNAL_DETAILS.Hirerachy_Code " &
            "  left outer join  tspl_customer_master on tspl_customer_master.cust_code  =TSPL_JOURNAL_MASTER.CustVend_Code" &
            " left outer join  TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.VENDOR_CODE  =TSPL_JOURNAL_MASTER.CustVend_Code " &
            " where TSPL_JOURNAL_MASTER.Voucher_No = '" + StrCode + "'  order by Detail_Line_No "



            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.GeneralLedger, clsDBFuncationality.GetDataTable(strQuery), "crptGLVoucher", "Journal Voucher Report")
            frmCRV = Nothing
        End If
    End Sub
    Public Shared Sub PrintDataAll(ByVal StrCode As String, ByVal StrSourceDocCode As String)
        Dim _Cond As String = ""
        If clsCommon.myLen(StrCode) > 0 Then
            _Cond = " TSPL_JOURNAL_MASTER.Voucher_No = '" + StrCode + "'"
        ElseIf clsCommon.myLen(StrSourceDocCode) > 0 Then
            _Cond = " TSPL_JOURNAL_MASTER.Source_Doc_No = '" + StrSourceDocCode + "'"
        Else
            clsCommon.MyMessageBoxShow("Voucher No and Source Doc No are blank")
            Exit Sub
        End If
        Dim strQuery As String = "  SELECT TSPL_HIRERACHY_LEVEL_MASTER.Description as HIRERACHY_LEVEL,VI.Remarks as Vendor_Detail_Remarks,TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name , TSPL_JOURNAL_MASTER.Source_Type ,TSPL_JOURNAL_MASTER.CustVend_Code,TSPL_JOURNAL_MASTER.CustVend_Name, TSPL_JOURNAL_MASTER.Voucher_No, TSPL_JOURNAL_MASTER.Voucher_Date,     case when  TSPL_JOURNAL_MASTER.CustVend_Code='' then   TSPL_JOURNAL_MASTER.Voucher_Desc else TSPL_JOURNAL_MASTER.Voucher_Desc+'  for ' +TSPL_JOURNAL_MASTER.CustVend_Code+' - '+TSPL_JOURNAL_MASTER.CustVend_Name end as Voucher_Desc, " &
                "   TSPL_JOURNAL_MASTER.Source_Code, TSPL_JOURNAL_MASTER.Source_doc_No,TSPL_JOURNAL_MASTER.Posting_Date, TSPL_JOURNAL_MASTER.Total_Debit_Amt, " &
                "   TSPL_JOURNAL_MASTER.Total_Credit_Amt, TSPL_JOURNAL_DETAILS.Account_code, TSPL_JOURNAL_DETAILS.Account_Desc,  " &
                "   TSPL_JOURNAL_DETAILS.Amount ,case when TSPL_JOURNAL_DETAILS.Amount>=0 then  TSPL_JOURNAL_DETAILS.Amount else 0 end as DrAmt,case when TSPL_JOURNAL_DETAILS.Amount<0 then -1 * TSPL_JOURNAL_DETAILS.Amount else 0 end as CrAmt, TSPL_JOURNAL_DETAILS.Description, TSPL_JOURNAL_DETAILS.Reference,  " &
                "   TSPL_JOURNAL_DETAILS.Posting_Date AS [Dtline PostDt], TSPL_JOURNAL_DETAILS.Detail_Line_No, " &
                " TSPL_JOURNAL_MASTER.Created_By, Case When Authorized='A' Then TSPL_JOURNAL_MASTER.Modify_By else '' End as Modify_By, TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2 ,(SELECT     CASE WHEN TSPL_JOURNAL_MASTER.Authorized = 'N' THEN 'OPEN' WHEN TSPL_JOURNAL_MASTER.Authorized = 'A' THEN 'Authorize' END" &
                 "  AS Expr1) AS Status, TSPL_JOURNAL_MASTER.Remarks" &
                "   FROM         TSPL_JOURNAL_MASTER INNER JOIN TSPL_JOURNAL_DETAILS ON TSPL_JOURNAL_MASTER.Journal_No = TSPL_JOURNAL_DETAILS.Journal_No  " &
                "  AND  TSPL_JOURNAL_MASTER.Voucher_No = TSPL_JOURNAL_DETAILS.Voucher_No  left outer join  TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code  =TSPL_JOURNAL_MASTER.Comp_Code  "
        'left join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No =TSPL_JOURNAL_MASTER.Source_Doc_No and TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code =TSPL_JOURNAL_DETAILS.Account_code  " & _

        strQuery = strQuery + " left join (select TSPL_VENDOR_INVOICE_DETAIL.Document_No,TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code, TSPL_VENDOR_INVOICE_DETAIL.Hirerachy_Code,TSPL_VENDOR_INVOICE_DETAIL.Remarks " &
                  " from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No = TSPL_VENDOR_INVOICE_HEAD.Document_No  " &
                  " GROUP BY TSPL_VENDOR_INVOICE_DETAIL.Document_No,TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code, TSPL_VENDOR_INVOICE_DETAIL.Hirerachy_Code,TSPL_VENDOR_INVOICE_DETAIL.Remarks) VI  on VI.Document_No  =TSPL_JOURNAL_MASTER.Source_Doc_No and VI.GL_Account_Code =TSPL_JOURNAL_DETAILS.Account_code " &
                  " left join TSPL_COST_CENTRE_FINANCIAL on TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code =TSPL_JOURNAL_DETAILS.Cost_Centre_Code  left join TSPL_HIRERACHY_LEVEL_MASTER on TSPL_HIRERACHY_LEVEL_MASTER.Hirerachy_Code =VI.Hirerachy_Code  where " & _Cond & "  order by Detail_Line_No "
        Dim frmCRV As New frmCrystalReportViewer()
        frmCRV.funreport(CrystalReportFolder.GeneralLedger, clsDBFuncationality.GetDataTable(strQuery), "crptGLVoucher", "Journal Voucher Report")
        frmCRV = Nothing
    End Sub
    Public Shared Sub PrintDataAll(ByVal StrCode As String, ByVal StrSourceDocCode As String, ByVal isHierarchyLevel As Boolean)
        Dim _Cond As String = ""
        If clsCommon.myLen(StrCode) > 0 Then
            _Cond = " TSPL_JOURNAL_MASTER.Voucher_No = '" + StrCode + "'"
        ElseIf clsCommon.myLen(StrSourceDocCode) > 0 Then
            _Cond = " TSPL_JOURNAL_MASTER.Source_Doc_No = '" + StrSourceDocCode + "'"
        Else
            clsCommon.MyMessageBoxShow("Voucher No and Source Doc No are blank")
            Exit Sub
        End If
        Dim strQuery As String = "  SELECT TSPL_HIRERACHY_LEVEL_MASTER.Description as HIRERACHY_LEVEL,VI.Remarks as Vendor_Detail_Remarks,TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name , TSPL_JOURNAL_MASTER.Source_Type ,TSPL_JOURNAL_MASTER.CustVend_Code,TSPL_JOURNAL_MASTER.CustVend_Name, TSPL_JOURNAL_MASTER.Voucher_No, TSPL_JOURNAL_MASTER.Voucher_Date,     case when  TSPL_JOURNAL_MASTER.CustVend_Code='' then   TSPL_JOURNAL_MASTER.Voucher_Desc else TSPL_JOURNAL_MASTER.Voucher_Desc+'  for ' +TSPL_JOURNAL_MASTER.CustVend_Code+' - '+TSPL_JOURNAL_MASTER.CustVend_Name end as Voucher_Desc, " &
                "   TSPL_JOURNAL_MASTER.Source_Code, TSPL_JOURNAL_MASTER.Source_doc_No,TSPL_JOURNAL_MASTER.Posting_Date, TSPL_JOURNAL_MASTER.Total_Debit_Amt, " &
                "   TSPL_JOURNAL_MASTER.Total_Credit_Amt, TSPL_JOURNAL_DETAILS.Account_code, TSPL_JOURNAL_DETAILS.Account_Desc,  " &
                "   TSPL_JOURNAL_DETAILS.Amount ,case when TSPL_JOURNAL_DETAILS.Amount>=0 then  TSPL_JOURNAL_DETAILS.Amount else 0 end as DrAmt,case when TSPL_JOURNAL_DETAILS.Amount<0 then -1 * TSPL_JOURNAL_DETAILS.Amount else 0 end as CrAmt, TSPL_JOURNAL_DETAILS.Description, TSPL_JOURNAL_DETAILS.Reference,  " &
                "   TSPL_JOURNAL_DETAILS.Posting_Date AS [Dtline PostDt], TSPL_JOURNAL_DETAILS.Detail_Line_No, " &
                " TSPL_JOURNAL_MASTER.Created_By, Case When Authorized='A' Then TSPL_JOURNAL_MASTER.Modify_By else '' End as Modify_By, TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2 ,(SELECT     CASE WHEN TSPL_JOURNAL_MASTER.Authorized = 'N' THEN 'OPEN' WHEN TSPL_JOURNAL_MASTER.Authorized = 'A' THEN 'Authorize' END" &
                 "  AS Expr1) AS Status, TSPL_JOURNAL_MASTER.Remarks,TSPL_JOURNAL_DETAILS.Hirerachy_Code3,TSPL_JOURNAL_DETAILS.Hirerachy_Code4,VI.Hirerachy_Code " &
                "   FROM         TSPL_JOURNAL_MASTER INNER JOIN TSPL_JOURNAL_DETAILS ON TSPL_JOURNAL_MASTER.Journal_No = TSPL_JOURNAL_DETAILS.Journal_No  " &
                "  AND  TSPL_JOURNAL_MASTER.Voucher_No = TSPL_JOURNAL_DETAILS.Voucher_No  left outer join  TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code  =TSPL_JOURNAL_MASTER.Comp_Code  "
        'left join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No =TSPL_JOURNAL_MASTER.Source_Doc_No and TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code =TSPL_JOURNAL_DETAILS.Account_code  " & _

        strQuery = strQuery + " left join (select TSPL_VENDOR_INVOICE_DETAIL.Document_No,TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code, TSPL_VENDOR_INVOICE_DETAIL.Hirerachy_Code,TSPL_VENDOR_INVOICE_DETAIL.Remarks " &
                  " from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No = TSPL_VENDOR_INVOICE_HEAD.Document_No  " &
                  " GROUP BY TSPL_VENDOR_INVOICE_DETAIL.Document_No,TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code, TSPL_VENDOR_INVOICE_DETAIL.Hirerachy_Code,TSPL_VENDOR_INVOICE_DETAIL.Remarks) VI  on VI.Document_No  =TSPL_JOURNAL_MASTER.Source_Doc_No and VI.GL_Account_Code =TSPL_JOURNAL_DETAILS.Account_code " &
                  " left join TSPL_COST_CENTRE_FINANCIAL on TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code =TSPL_JOURNAL_DETAILS.Cost_Centre_Code  left join TSPL_HIRERACHY_LEVEL_MASTER on TSPL_HIRERACHY_LEVEL_MASTER.Hirerachy_Code =VI.Hirerachy_Code  where " & _Cond & "  order by Detail_Line_No "
        Dim frmCRV As New frmCrystalReportViewer()
        frmCRV.funreport(CrystalReportFolder.GeneralLedger, clsDBFuncationality.GetDataTable(strQuery), "crptGLVoucher_Hierarchy", "Journal Voucher Report")
        frmCRV = Nothing
    End Sub
    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDrillDown.Click
        If fndSrcCode.Value = "AR-PY" Or fndSrcCode.Value = "AR-PI" Or fndSrcCode.Value = "AR-MI" Or fndSrcCode.Value = "AR-UC" Or fndSrcCode.Value = "AR-OA" Or fndSrcCode.Value = "AR-DC" Then
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ReceiptEntry, txtSrcDoc.Text)
        ElseIf fndSrcCode.Value = "AP-PY" Or fndSrcCode.Value = "AP-MI" Then
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.PaymentEntryNew, txtSrcDoc.Text)
        ElseIf fndSrcCode.Value = "AP-IN" OrElse fndSrcCode.Value = "AP-DN" OrElse fndSrcCode.Value = "AP-CN" OrElse fndSrcCode.Value = "MI-PI" OrElse fndSrcCode.Value = "MI-CO" Then
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmVendorService, txtSrcDoc.Text)
        ElseIf fndSrcCode.Value = "SD-IN" Then
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSNSaleInvoice, txtSrcDoc.Text)
        ElseIf fndSrcCode.Value = "BK-TF" Then
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.bankTransfer, txtSrcDoc.Text)
        ElseIf fndSrcCode.Value = "IC-AD" Then
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnStoreAdjustment, txtSrcDoc.Text)
        ElseIf fndSrcCode.Value = "PO-RC" Then
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnSRN, txtSrcDoc.Text)
        ElseIf fndSrcCode.Value = "SD-LO" Then
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.LoadOut, txtSrcDoc.Text)
        ElseIf fndSrcCode.Value = "MM-TF" Then
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.Transfer, txtSrcDoc.Text)
        ElseIf fndSrcCode.Value = "RV-TA" Then
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.reverseTransaction, txtSrcDoc.Text)
        ElseIf fndSrcCode.Value = "AR-AD" Then
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ReceiptAdjustmentEntry, txtSrcDoc.Text)
        ElseIf fndSrcCode.Value = "PO-RT" Then
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseReturn, txtSrcDoc.Text)
        ElseIf fndSrcCode.Value = "SD-SR" Then
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.saleReturn, txtSrcDoc.Text)
        ElseIf fndSrcCode.Value = "AR-IN" OrElse fndSrcCode.Value = "AR-DN" OrElse fndSrcCode.Value = "AR-CN" Then
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnARInvoiceEntry, txtSrcDoc.Text)
        ElseIf fndSrcCode.Value = "AR-SI" Then
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ScrapSale, txtSrcDoc.Text)

        ElseIf fndSrcCode.Value = "EX-AD" Then
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmExpiryDateEntry, txtSrcDoc.Text)
        ElseIf fndSrcCode.Value = "MI-SR" Then
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkSRN, txtSrcDoc.Text)
        Else
            Return
        End If
    End Sub
#End Region
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.journalEntry)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag
        ' btnAuth.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnAuth.Visible = MyBase.isPostFlag
        btnPrint.Visible = MyBase.isPrintFlag
        'RadMenu2.Visible = MyBase.isExport
        If MyBase.isReverse Then
            btnUnpostTransaction.Enabled = True
        Else
            btnUnpostTransaction.Enabled = False
        End If
        If btnSave.Visible = True Then
            mItmExport.Enabled = True
            mItmImport.Enabled = True
        Else
            mItmExport.Enabled = False
            mItmImport.Enabled = False
        End If
        If MyBase.isExport = True Then
            btnExportblank.Enabled = True
            mItmImport.Enabled = True
            RadMenuItem4.Enabled = True
        Else
            btnExportblank.Enabled = False
            mItmImport.Enabled = False
            RadMenuItem4.Enabled = False
        End If

    End Sub
#Region "Page Load"
    Public Sub SetLength()
        fndVoucher.MyMaxLength = 30
        txtVoucherDesc.MaxLength = 250
        txtRemarks.MaxLength = 250
        txtComments.MaxLength = 200
    End Sub
#End Region

#Region "FINDER"
    Private Sub fndVoucher__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndVoucher._MYValidating
        '''' Addded BY Abhishek as on 26 Nov 2012

        'Dim Qry1 As String = " Select Count(*) From TSPL_JOURNAL_MASTER where Voucher_No='" & fndVoucher.Value & "'"
        'Dim Count As String = clsDBFuncationality.getSingleValue(Qry1)
        'If Count = 0 Then
        '    fndVoucher.MyReadOnly = False
        'Else
        '    fndVoucher.MyReadOnly = True
        'End If
        '''' Code end
        'If isButtonClicked Then
        'Dim qry As String = "SELECT  distinct   TSPL_JOURNAL_MASTER.Voucher_No AS VoucherNo, TSPL_JOURNAL_MASTER.Voucher_Desc AS Description, " & _
        '                     " TSPL_JOURNAL_MASTER.Source_Code AS [Source Type], convert(varchar(11),TSPL_JOURNAL_MASTER.Voucher_Date,103) AS [Voucher Date],  " & _
        '                     " TSPL_JOURNAL_MASTER.Source_Doc_No AS [Document No], convert(varchar(11),TSPL_JOURNAL_MASTER.Source_Doc_Date,103) AS [Document Date], " & _
        '                     " case when Source_Code= 'AR-IN' then (select Against_Sale_No from TSPL_Customer_Invoice_Head where Document_No=TSPL_JOURNAL_MASTER.Source_Doc_No) " & _
        '                     " when Source_Code= 'AR-CR' then (select Against_Sale_Return_No from TSPL_Customer_Invoice_Head where Document_No=TSPL_JOURNAL_MASTER.Source_Doc_No) " & _
        '                     " when Source_Code= 'AP-IN' then (select Against_POInvoice_No from TSPL_VENDOR_INVOICE_HEAD where Document_No=TSPL_JOURNAL_MASTER.Source_Doc_No) " & _
        '                     " when Source_Code= 'AP-CN' then (select Against_PurchaseReturn_No from TSPL_VENDOR_INVOICE_HEAD where Document_No=TSPL_JOURNAL_MASTER.Source_Doc_No)  else '' end as RefDocNo, " & _
        '                     " (SELECT     CASE WHEN Authorized = 'A' THEN 'Posted' ELSE 'Open' END AS Expr1) AS Status,TSPL_JOURNAL_MASTER.Remarks,Auto_Reverse As [Auto Reverse] " & _
        '                     "  FROM         TSPL_JOURNAL_MASTER LEFT OUTER JOIN TSPL_JOURNAL_DETAILS ON TSPL_JOURNAL_MASTER.Journal_No = TSPL_JOURNAL_DETAILS.Journal_No"

        Dim qry As String = "SELECT  distinct   TSPL_JOURNAL_MASTER.Voucher_No AS VoucherNo, TSPL_JOURNAL_MASTER.Voucher_Desc AS Description, " &
                            " TSPL_JOURNAL_MASTER.Source_Code AS [Source Type], convert(varchar(11),TSPL_JOURNAL_MASTER.Voucher_Date,103) AS [Voucher Date],  " &
                            " TSPL_JOURNAL_MASTER.Source_Doc_No AS [Document No], convert(varchar(11),TSPL_JOURNAL_MASTER.Source_Doc_Date,103) AS [Document Date], " &
                            " case when TSPL_JOURNAL_MASTER.Source_Code= 'AR-IN' then  TSPL_Customer_Invoice_Head.Against_Sale_No  " &
                            " when TSPL_JOURNAL_MASTER.Source_Code= 'AR-CR' then TSPL_Customer_Invoice_Head.Against_Sale_Return_No " &
                            " when TSPL_JOURNAL_MASTER.Source_Code= 'AP-IN' then  TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No " &
                            " when TSPL_JOURNAL_MASTER.Source_Code= 'AP-CN' then  TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  else '' end as RefDocNo, " &
                            " CASE WHEN TSPL_JOURNAL_MASTER.Authorized = 'A' THEN 'Posted' ELSE 'Open' END AS Status,TSPL_JOURNAL_MASTER.Remarks,Auto_Reverse As [Auto Reverse] " &
                            "  FROM TSPL_JOURNAL_MASTER LEFT OUTER JOIN TSPL_JOURNAL_DETAILS ON TSPL_JOURNAL_MASTER.Journal_No = TSPL_JOURNAL_DETAILS.Journal_No" &
                            " left outer join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Document_No=TSPL_JOURNAL_MASTER.Source_Doc_No " &
                            " left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_JOURNAL_MASTER.Source_Doc_No "

        Dim WhrCls As String = ""
        If clsCommon.CompairString(Program_Code, clsUserMgtCode.journalEntry) = CompairStringResult.Equal Then
            WhrCls = " (TSPL_JOURNAL_MASTER.ProgramCode is null or TSPL_JOURNAL_MASTER.ProgramCode='" & Program_Code & "')"
        ElseIf clsCommon.CompairString(Program_Code, clsUserMgtCode.ReversejournalEntry) = CompairStringResult.Equal Then
            WhrCls = " (TSPL_JOURNAL_MASTER.ProgramCode='" & Program_Code & "')"
        End If
        If clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal Then
            WhrCls = WhrCls
        Else
            If clsCommon.myLen(WhrCls) > 0 Then
                WhrCls = WhrCls & " and TSPL_JOURNAL_DETAILS.Account_code in (" + objCommonVar.strCurrUserGLAccount + ")"
            Else
                WhrCls = " TSPL_JOURNAL_DETAILS.Account_code in (" + objCommonVar.strCurrUserGLAccount + ")"
            End If

        End If



        fndVoucher.Value = clsCommon.ShowSelectForm("Voucher Selector", qry, "VoucherNo", WhrCls, fndVoucher.Value, "", isButtonClicked, "TSPL_JOURNAL_MASTER.[Voucher_Date]")
        If clsCommon.myLen(fndVoucher.Value) <= 0 Then
            funNew()
        Else
            funFill()
        End If
        'End If
    End Sub

    Private Sub fndVoucher__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndVoucher._MYNavigator
        Dim WhrCls As String = ""
        If clsCommon.CompairString(Program_Code, clsUserMgtCode.journalEntry) = CompairStringResult.Equal Then
            WhrCls = " (ProgramCode is null or ProgramCode='" & Program_Code & "')"
        ElseIf clsCommon.CompairString(Program_Code, clsUserMgtCode.ReversejournalEntry) = CompairStringResult.Equal Then
            WhrCls = " (ProgramCode='" & Program_Code & "')"
        End If
        Dim qry As String = "select Voucher_No  from TSPL_JOURNAL_MASTER Where 2=2 "
        If clsCommon.myLen(WhrCls) > 0 Then
            qry = qry & " and " & WhrCls
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_JOURNAL_MASTER.Voucher_No=(select MIN(Voucher_No) from TSPL_JOURNAL_MASTER)"
            Case NavigatorType.Last
                qry += " and TSPL_JOURNAL_MASTER.Voucher_No=(select MAX(Voucher_No) from TSPL_JOURNAL_MASTER)"
            Case NavigatorType.Next
                qry += " and TSPL_JOURNAL_MASTER.Voucher_No=(select Min(Voucher_No) from TSPL_JOURNAL_MASTER where Voucher_No > '" + fndVoucher.Value + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_JOURNAL_MASTER.Voucher_No=(select Max(Voucher_No) from TSPL_JOURNAL_MASTER where Voucher_No < '" + fndVoucher.Value + "')"
            Case NavigatorType.Current
                qry += " and TSPL_ITEM_CATEGORY.Category_Code='" + fndVoucher.Value + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            fndVoucher.Value = clsCommon.myCstr(dt.Rows(0)("Voucher_No"))
            funFill()
        End If
    End Sub

    'Private Sub fndVoucher_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fndVoucher.Query = "select Voucher_No  as [Voucher No] ,Voucher_Desc as [Description] , Source_Code as [Source Type],Voucher_Date as [Voucher Date],Source_Doc_No as [Document No],Source_Doc_Date as [Document Date],(select case when Authorized ='A' then 'Posted' else 'UnPosted' end) as [Status] from TSPL_JOURNAL_MASTER "
    '    fndVoucher.ConnectionString = connectSql.SqlCon()
    '    fndVoucher.Caption = "Journal Entry"
    '    fndVoucher.ValueToSelect = "Voucher No"
    '    fndVoucher.ValueToSelect1 = "Description"
    'End Sub

    Private Sub fndSrcCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndSrcCode._MYValidating
        Dim qry As String = "select SourceCode as [Code],SourceDescription as [Description]  from TSPL_GL_SOURCECODE   "
        fndSrcCode.Value = clsCommon.ShowSelectForm("SourceCode Selector", qry, "Code", "SourceLedger  ='GL'", fndSrcCode.Value, "Code", isButtonClicked)
        Me.txtSrcDesc.Text = fnSrcType(fndSrcCode.Value)
        Me.txtBoxSrcType.Text = fndSrcCode.Value
    End Sub
    'Private Sub fndSrcType_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fndSrcType.Query = "select SourceCode as [Source Code],SourceDescription as [Source Description]  from TSPL_GL_SOURCECODE where SourceLedger  ='GL' "
    '    fndSrcType.ConnectionString = connectSql.SqlCon()
    '    fndSrcType.Caption = "Source Type"
    '    fndSrcType.ValueToSelect = "Source Code"
    '    fndSrcType.ValueToSelect1 = "Source Description"
    'End Sub
    Private Sub Src_txtEvent(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Me.txtSrcDesc.Text = fnSrcType(fndSrcCode.Value)
        'Me.txtBoxSrcType.Text = fndSrcCode.Value
    End Sub

    Private Sub fndCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndCode._MYValidating
        Dim qry As String = Nothing
        If rdbVendor.IsChecked = True Then
            qry = "select Vendor_Code  as [Code],Vendor_Name  as [Name],Vendor_Group_Code  as [Customer Group],(select case when Status='N' then 'Active' else 'In.Active' end ) as [Status] from TSPL_VENDOR_MASTER "
            fndCode.Value = clsCommon.ShowSelectForm("Vendor_Selector", qry, "Code", "", fndCode.Value, "Code", isButtonClicked)
        ElseIf rdbCustomer.IsChecked = True Then
            qry = "select Cust_Code as [Code],Customer_Name as [Name],Cust_Group_Code as [Customer Group],(select case when Status='N' then 'Active' else 'In.Active' end ) as [Status] from TSPL_CUSTOMER_MASTER "
            fndCode.Value = clsCommon.ShowSelectForm("Customer_Selector", qry, "Code", "", fndCode.Value, "Code", isButtonClicked)
        End If
    End Sub

    Private Sub FormFill_Event(ByVal sender As System.Object, ByVal e As System.EventArgs)
        funFill()
    End Sub


    Private Sub fndVoucher_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles fndVoucher.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub fndSrcCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles fndSrcCode.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub fndCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles fndCode.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub


    Private Function fnSrcType(ByVal strSrcId As String) As String
        Dim strSrcDesc As String
        strSrcDesc = ""
        Dim strcmd As String
        strcmd = "select SourceCode as [Source Code],SourceDescription as [Source Description]  from TSPL_GL_SOURCECODE where SourceCode =  '" + strSrcId + "'  "
        Try
            myreader = clsDBFuncationality.GetDataTable(strcmd)
            If (myreader IsNot Nothing AndAlso myreader.Rows.Count > 0) Then
                For Each dr As DataRow In myreader.Rows
                    strSrcDesc = Convert.ToString(dr(1).ToString().Trim())
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Journal Entry")
        End Try
        Return strSrcDesc
    End Function

#End Region

#Region "AutoNumber"
    'Public Function fnAutoGenerateNo() As String
    '    Return fnAutoGenerateNo(Nothing)
    'End Function
    'Public Function fnAutoGenerateNo(ByVal trans As SqlTransaction, ByVal TranDate As Date) As String
    '    Return fnAutoGenerateNo(trans, TranDate, False, "")
    'End Function
    Public Function fnAutoGenerateNo(ByVal trans As SqlTransaction, ByVal TranDate As Date, ByVal isGLJE As Boolean, ByVal strLocationCode As String, ByVal isLocationCodeisSegment As Boolean, Optional ByVal isMilkSRN As Boolean = False) As String
        Dim strJournalNo As String = Nothing
        If clsCommon.myLen(strLocationCode) <= 0 Then
            Throw New Exception("First Account Should have location Segment")
        End If

        If isGLJE Then
            strJournalNo = clsERPFuncationality.GetNextCode(trans, TranDate, clsDocType.JournalEntry, clsDocTransactionType.JournalEntryJLJE, strLocationCode, isLocationCodeisSegment)
        ElseIf isMilkSRN Then
            strJournalNo = clsERPFuncationality.GetNextCode(trans, TranDate, clsDocType.JournalEntry, clsDocTransactionType.JournalEntryMilkSRN, strLocationCode, isLocationCodeisSegment, True, False, False, objCommonVar.ShowMCCFinderInPaymentProcess)
        Else
            strJournalNo = clsERPFuncationality.GetNextCode(trans, TranDate, clsDocType.JournalEntry, clsDocTransactionType.JournalEntryOther, strLocationCode, isLocationCodeisSegment)
        End If


        Return strJournalNo
    End Function
    'Public Function fnAutoGenerateNo(ByVal trans As SqlTransaction) As String
    '    Dim DefaltDate As Date = clsCommon.myCDate(connectSql.serverDate(trans), "dd/MM/yyyy")
    '    Return fnAutoGenerateNo(trans, DefaltDate)



    '    ''Modified by : suraj & balwinder
    '    ''reason = timeout (for transaction )
    '    'sql = "SELECT MAX(Voucher_No) as MaxValue from TSPL_JOURNAL_MASTER  where Voucher_No like '%JRNL%' "
    '    'Dim Maxvlu As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(sql, trans))

    '    'If clsCommon.myLen(Maxvlu) > 0 Then
    '    '    Maxvlu = clsCommon.incval(Maxvlu)
    '    'Else
    '    '    Maxvlu = "JRNL0001"
    '    'End If
    '    'Return Maxvlu



    '    'Dim strJournalNo As String = Nothing
    '    'strJournalNo = clsERPFuncationality.GetNextCode(trans, TranDate, clsDocType.JournalEntry, "", "", True)
    '    'Return strJournalNo


    '    'Dim Maxvlu As String
    '    'Dim NxtMaxNo As Int32
    '    'sql = "SELECT MAX(Voucher_No) as MaxValue from TSPL_JOURNAL_MASTER  where Voucher_No like '%JRNL%' "
    '    'ds = connectSql.RunSQLReturnDS(sql)
    '    ''ds = clsDBFuncationality.getSingleValue(sql, trans)
    '    'If ds.Tables(0).Rows.Count > 0 Then
    '    '    If ds.Tables(0).Rows(0)(0).ToString <> "" Then
    '    '        Maxvlu = ds.Tables(0).Rows(0)(0).ToString()
    '    '        Maxvlu = Maxvlu.Remove(0, 4)
    '    '        NxtMaxNo = Convert.ToInt32(Maxvlu.ToString())
    '    '        NxtMaxNo = NxtMaxNo + 1
    '    '        Dim strCount As String
    '    '        strCount = NxtMaxNo.ToString()
    '    '        If strCount.Length = 1 Then
    '    '            Maxvlu = "JRNL" & "000" & NxtMaxNo.ToString()
    '    '        ElseIf strCount.Length = 2 Then
    '    '            Maxvlu = "JRNL" & "00" & NxtMaxNo.ToString()
    '    '        ElseIf strCount.Length = 3 Then
    '    '            Maxvlu = "JRNL" & "0" & NxtMaxNo.ToString()
    '    '        ElseIf strCount.Length = 4 Then
    '    '            Maxvlu = "JRNL" & NxtMaxNo.ToString()
    '    '        End If
    '    '        Return Maxvlu
    '    '    Else
    '    '        Maxvlu = "JRNL0001"
    '    '        Return Maxvlu
    '    '    End If
    '    'Else
    '    '    Maxvlu = "JRNL0001"
    '    '    Return Maxvlu
    '    'End If
    '    'Return Maxvlu

    'End Function
#End Region

#Region "Function"

    Private Sub OpenGLAccount(ByVal isButtonClick As Boolean)
        Try
            Dim qry As String
            Dim whrcls As String
            Dim arr As New ArrayList()
            arr = clsERPFuncationality.glaccountquery(objCommonVar.CurrentUserCode)
            qry = arr.Item(0)
            ''----richa agarwal against ticket no BM00000006848
            whrcls = arr.Item(1)
            If clsCommon.myLen(txtLocation.Value) > 0 Then
                Dim finalWhr As String = ""
                If clsCommon.myLen(whrcls) > 0 Then
                    finalWhr = " ( " + whrcls + " ) "
                End If
                If AllowJEofDifferentLocationOnJournalEntry = False Then
                    finalWhr += "  and ((Account_Code like '%" + txtLocation.Value + "' and ControlAccount ='N' ) or (Account_Code in (select Account_Code from TSPL_CONTROL_ACC_MAPPING where Account_Code like '%" + txtLocation.Value + "')))"
                End If

                whrcls = finalWhr
            Else
                whrcls = "(" & whrcls & ")" & " or Account_Code in (select Account_Code from TSPL_CONTROL_ACC_MAPPING) "
            End If

            If clsCommon.GetDateWithStartTime(dtVoucher.Value) < clsCommon.GetDateWithStartTime(ERPStartDate) Then
                whrcls = clsCommon.ReplaceString(whrcls, "TSPL_GL_ACCOUNTS.ControlAccount='N'", " 2=2 ")
                whrcls = clsCommon.ReplaceString(whrcls, "ControlAccount ='N'", " 2=2 ")
                whrcls = clsCommon.ReplaceString(whrcls, "( ControlAccount ='N')", " ")

                whrcls = clsCommon.ReplaceString(whrcls, "TSPL_GL_ACCOUNTS.ControlAccount<>'Y'", " 2=2 ")
                whrcls = clsCommon.ReplaceString(whrcls, "ControlAccount<>'Y'", " 2=2 ")
                whrcls = clsCommon.ReplaceString(whrcls, "( ControlAccount<>'Y')", " ")
            End If

            'richa 17 SEp,2019 TEC/03/07/19-000927

            Dim strqry As String = " Select Account_Code,Description from (" & qry & " where " & whrcls & Environment.NewLine & _
              " UNION All " & Environment.NewLine & _
              " select Account_Code , Description  from TSPL_GL_ACCOUNTS " & Environment.NewLine & _
    " left outer join (select TSPL_GL_SEGMENT_CODE.Account_Code as AccCode from TSPL_GL_SEGMENT_CODE where TSPL_GL_SEGMENT_CODE.Seg_No='7' " & Environment.NewLine & _
    " and len(isnull(TSPL_GL_SEGMENT_CODE.Account_Code,''))>0 ) as segTable  on segTable.AccCode=TSPL_GL_ACCOUNTS.Account_Code " & Environment.NewLine & _
      " inner join TSPL_GL_STRUCTURE on TSPL_GL_ACCOUNTS .Str_Code=TSPL_GL_STRUCTURE.Str_Code where ( 2=2  and TSPL_GL_ACCOUNTS.Status='Y' and ( segTable.AccCode is null  ))" & Environment.NewLine & _
      " and 1<>(isnull(Seg_No1,0) +isnull(Seg_No2,0) +isnull(Seg_No3,0) +isnull(Seg_No4,0) +isnull(Seg_No5,0) +isnull(Seg_No6,0) +isnull(Seg_No7,0) +isnull(Seg_No8,0) +isnull(Seg_No9,0) +isnull(Seg_No10,0) ) " & Environment.NewLine & _
      " and TSPL_GL_ACCOUNTS.Account_Code in (select TSPL_CONTROL_ACC_MAPPING.Account_Code  from TSPL_CONTROL_ACC_MAPPING where IsForJE =1) and  Account_Code like '%" + txtLocation.Value + "' ) Final "

            ''-------------------------------
            gdAcc1.CurrentRow.Cells(1).Value = clsCommon.myCstr(clsCommon.ShowSelectForm("GLACJournalEntry", strqry, "Account_Code", "", clsCommon.myCstr(gdAcc1.CurrentRow.Cells(1).Value), "Account_Code", isButtonClick))
            'gdAcc1.CurrentRow.Cells(1).Value = clsCommon.myCstr(clsCommon.ShowSelectForm("GLACJournalEntry", qry, "Account_Code", whrcls, clsCommon.myCstr(gdAcc1.CurrentRow.Cells(1).Value), "Account_Code", isButtonClick))
            gdAcc1.CurrentRow.Cells(2).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + clsCommon.myCstr(gdAcc1.CurrentRow.Cells(1).Value) + "'"))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub funInsert(ByVal ChekPostBtn As Boolean)
        Dim i As Integer = 0
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            'Dim strFirstAccCode As String = clsCommon.myCstr(gdAcc1.Rows(i).Cells(1).Value)
            'strFirstAccCode = strFirstAccCode.Substring(clsCommon.myLen(strFirstAccCode) - 3, 3)
            Dim StrVoucher As String = fnAutoGenerateNo(trans, clsCommon.myCDate(dtVoucher.Value, "dd/MM/yyyy"), True, txtLocation.Value, True)

            Dim strSrcType As String = ""
            If rdbCustomer.IsChecked = True Then
                strSrcType = "C"                  '***** for : Customers
            ElseIf rdbVendor.IsChecked = True Then
                strSrcType = "V"                  '***** for: Vendors
            ElseIf rdbOther.IsChecked = True Then
                strSrcType = "O"                 '****** for: Others
            End If

            Dim strEntryType As String = ""
            If cmbType.SelectedIndex = 0 Then
                strEntryType = "N"               '****** for: Normal Entry
            ElseIf cmbType.SelectedIndex = 1 Then
                strEntryType = "A"               '****** for: Adjustment Entry 
            ElseIf cmbType.SelectedIndex = 2 Then
                strEntryType = "X"               '****** for:Closing Entry 
            ElseIf cmbType.SelectedIndex = 3 Then
                strEntryType = "O"               '****** for:Closing Entry 
            End If

            Dim strRvrs As String
            If chkReverse.Checked = True Then
                strRvrs = "Y"                    '****** For: Auto-Reverse is True  
            Else
                strRvrs = "N"                    '****** For: Auto-Reverse is False
            End If

            Dim strJrnl As String = "select (case when max(journal_no) is not null then max(journal_no) else 0 end) from TSPL_JOURNAL_MASTER "
            Dim Jrnl As String = CInt(connectSql.RunScalar(trans, strJrnl)) + 1

            Dim ReverseDate As String = ""
            If chkReverse.Checked = True Then
                ReverseDate = clsCommon.GetPrintDate(dtRevese.Value, "dd/MM/yyyy")
            End If

            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_MASTER_INSERT", New SqlParameter("@Journal_No", Jrnl),
                                        New SqlParameter("@Voucher_No", StrVoucher),
                                        New SqlParameter("@Voucher_Date", clsCommon.GetPrintDate(dtVoucher.Value, "dd/MMM/yyyy")),
                                        New SqlParameter("@Source_Code", Me.fndSrcCode.Value), New SqlParameter("@Source_Desc", Me.txtSrcDesc.Text),
                                        New SqlParameter("@Source_Doc_No", Me.txtSrcDoc.Text), New SqlParameter("@Source_Doc_Date", clsCommon.GetPrintDate(Me.dtSrc.Value.Date, "dd/MMM/yyyy")),
                                        New SqlParameter("@Posting_Date", clsCommon.GetPrintDate(Me.dtVoucher.Value.Date, "dd/MMM/yyyy")),
                                        New SqlParameter("@Voucher_Desc", Me.txtVoucherDesc.Text), New SqlParameter("@Source_Narration", Me.txtSrcDesc.Text),
                                        New SqlParameter("@Remarks", Me.txtRemarks.Text), New SqlParameter("@Comments", Me.txtComments.Text),
                                        New SqlParameter("@Auto_Reverse", strRvrs), New SqlParameter("@Reverse_Date", ReverseDate),
                                        New SqlParameter("@Source_Type", strSrcType), New SqlParameter("@CustVend_Code", Me.fndCode.Value),
                                        New SqlParameter("@CustVend_Name", Me.txtCodeDesc.Text), New SqlParameter("@Transaction_Type", strEntryType),
                                        New SqlParameter("@Total_Debit_Amt", txtDr.Text), New SqlParameter("@Total_Credit_Amt", txtCr.Text),
                                        New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)),
                                        New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)),
                                        New SqlParameter("@Comp_Code", companyCode))
            Dim qry As String = "Update TSPL_JOURNAL_MASTER set Segment_Code='" + txtLocation.Value + "',MonthlyReverse='" & IIf(chkMonthly.Checked, 1, 0) & "',ProgramCode='" & Program_Code & "',IND_AS='" & IIf(chkIndAS.Checked, 1, 0) & "', AgainstVoucherNoReverseEntry = '" + txtReverseVoucher.Value + "',TapalNo='" & clsCommon.myCstr(txtTapalNo.Text) & "' " & IIf(txtDataAndTimeSelection.Checked, ",DateAndTime='" & clsCommon.GetPrintDate(txtDataAndTimeSelection.Value, "dd/MMM/yyyy hh:mm tt") & "'  ", " ,DateAndTime=null ") & " where Voucher_No='" + StrVoucher + "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strJrnl1 As String = "select journal_no from TSPL_JOURNAL_MASTER where Voucher_No='" + StrVoucher + "'"
            Dim Jrnl1 As String
            Jrnl1 = connectSql.RunScalar(trans, strJrnl1)


            For i = 0 To gdAcc1.Rows.Count - 1
                Dim strAccCode As String = clsCommon.myCstr(gdAcc1.Rows(i).Cells(1).Value)
                If clsCommon.myLen(strAccCode) > 0 Then
                    Dim LineNo As Integer = gdAcc1.Rows(i).Cells(0).Value
                    Dim strAccDesc As String = gdAcc1.Rows(i).Cells(2).Value

                    Dim Amt As Decimal
                    Dim strAmtDr As Decimal = gdAcc1.Rows(i).Cells(3).Value
                    Dim strAmtCr As Decimal = gdAcc1.Rows(i).Cells(4).Value
                    Dim strHierarchy As String = clsCommon.myCstr(gdAcc1.Rows(i).Cells(8).Value)
                    Dim strcostcentre As String = clsCommon.myCstr(gdAcc1.Rows(i).Cells(9).Value)
                    If strAmtDr = 0 Then
                        Amt = strAmtCr * -1
                    ElseIf strAmtCr = 0 Then
                        Amt = strAmtDr
                    End If
                    If SettingCostCenter Then
                        Dim grouptype As String = ""
                        grouptype = clsPaymentHeader.CheckGLAccountType(clsCommon.myCstr(strAccCode), trans)
                        If clsCommon.CompairString(grouptype, "Balance Sheet") = CompairStringResult.Equal Then
                        Else
                            If clsCommon.myLen(strHierarchy) <= 0 Then
                                Throw New Exception("Please provide the Hierarchy Level " + clsCommon.myCstr(LineNo))
                            ElseIf clsCommon.myLen(strcostcentre) <= 0 Then
                                Throw New Exception("Please provide the Cost Center " + clsCommon.myCstr(LineNo))
                            End If
                        End If
                    End If
                    Dim strDesc As String = Convert.ToString(gdAcc1.Rows(i).Cells(5).Value)

                    Dim strRef As String = Convert.ToString(gdAcc1.Rows(i).Cells(6).Value)

                    Dim PostDate As String = gdAcc1.Rows(i).Cells(7).Value
                    If PostDate = "" Then
                        PostDate = Format(dtVoucher.Value, "dd/MM/yyyy")
                    Else
                        PostDate = gdAcc1.Rows(i).Cells(7).Value
                    End If

                    Dim strQ1 As String = " SELECT     Account_Type, Account_Group_Code, Account_Seg_Code1, Account_Seg_Desc1, Account_Seg_Code2, Account_Seg_Desc2, Account_Seg_Code3, " & _
                         " Account_Seg_Desc3, Account_Seg_Code4, Account_Seg_Desc4, Account_Seg_Code5, Account_Seg_Desc5, Account_Seg_Code6, Account_Seg_Desc6," & _
                         " Account_Seg_Code7, Account_Seg_Desc7, Account_Seg_Code8, Account_Seg_Desc8, Account_Seg_Code9, Account_Seg_Desc9, " & _
                         " Account_Seg_Code10, Account_Seg_Desc10 FROM  TSPL_GL_ACCOUNTS where Account_Code='" + strAccCode + "'"
                    myreader = clsDBFuncationality.GetDataTable(strQ1, trans)

                    If (myreader IsNot Nothing AndAlso myreader.Rows.Count > 0) Then

                        For Each dr As DataRow In myreader.Rows



                            Dim AccType As String = dr(0).ToString()
                            Dim AccGrp As String = dr(1).ToString()

                            Dim SegC1 As String = dr(2).ToString()
                            Dim SegDesc1 As String = dr(3).ToString()

                            Dim SegC2 As String = dr(4).ToString()
                            Dim SegDesc2 As String = dr(5).ToString()

                            Dim SegC3 As String = dr(6).ToString()
                            Dim SegDesc3 As String = dr(7).ToString()

                            Dim SegC4 As String = dr(8).ToString()
                            Dim SegDesc4 As String = dr(9).ToString()

                            Dim SegC5 As String = dr(10).ToString()
                            Dim SegDesc5 As String = dr(11).ToString()

                            Dim SegC6 As String = dr(12).ToString()
                            Dim SegDesc6 As String = dr(13).ToString()

                            Dim SegC7 As String = dr(14).ToString()
                            Dim SegDesc7 As String = dr(15).ToString()

                            Dim SegC8 As String = dr(16).ToString()
                            Dim SegDesc8 As String = dr(17).ToString()

                            Dim SegC9 As String = dr(18).ToString()
                            Dim SegDesc9 As String = dr(19).ToString()

                            Dim SegC10 As String = dr(20).ToString()
                            Dim SegDesc10 As String = dr(21).ToString()

                            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl1), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", clsCommon.GetPrintDate(dtVoucher.Value.Date, "dd/MMM/yyyy")), New SqlParameter("@Detail_Line_No", LineNo), New SqlParameter("@Account_code", strAccCode), New SqlParameter("@Account_Desc", strAccDesc), New SqlParameter("@Amount", Amt), New SqlParameter("@Description", strDesc), New SqlParameter("@Reference", strRef), New SqlParameter("@Posting_Date", clsCommon.GetPrintDate(PostDate, "dd/MMM/yyyy")), New SqlParameter("@Account_Type", AccType), New SqlParameter("@Account_Group_Code", AccGrp), New SqlParameter("@Account_Seg_Code1", SegC1), New SqlParameter("@Account_Seg_Desc1", SegDesc1), New SqlParameter("@Account_Seg_Code2", SegC2), New SqlParameter("@Account_Seg_Desc2", SegDesc2), New SqlParameter("@Account_Seg_Code3", SegC3), New SqlParameter("@Account_Seg_Desc3", SegDesc3), New SqlParameter("@Account_Seg_Code4", SegC4), New SqlParameter("@Account_Seg_Desc4", SegDesc4), New SqlParameter("@Account_Seg_Code5", SegC5), New SqlParameter("@Account_Seg_Desc5", SegDesc5), New SqlParameter("@Account_Seg_Code6", SegC6), New SqlParameter("@Account_Seg_Desc6", SegDesc6), New SqlParameter("@Account_Seg_Code7", SegC7), New SqlParameter("@Account_Seg_Desc7", SegDesc7), New SqlParameter("@Account_Seg_Code8", SegC8), New SqlParameter("@Account_Seg_Desc8", SegDesc8), New SqlParameter("@Account_Seg_Code9", SegC9), New SqlParameter("@Account_Seg_Desc9", SegDesc9), New SqlParameter("@Account_Seg_Code10", SegC10), New SqlParameter("@Account_Seg_Desc10", SegDesc10))

                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "Hirerachy_Code", strHierarchy, True)
                            clsCommon.AddColumnsForChange(coll, "Cost_Centre_Code", strcostcentre, True)
                            clsCommonFunctionality.UpdateDataTable(coll, "tspl_journal_details", OMInsertOrUpdate.Update, "tspl_journal_details.Journal_No='" + clsCommon.myCstr(Jrnl) + "' and tspl_journal_details.Voucher_No='" + clsCommon.myCstr(StrVoucher) + "' and tspl_journal_details.Account_Code='" + clsCommon.myCstr(strAccCode) + "' and Detail_Line_No='" + clsCommon.myCstr(LineNo) + "'", trans)

                        Next
                    End If
                End If
            Next
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from tspl_journal_master where voucher_no='" + StrVoucher + "'", trans)
            trans.Commit()
            UcAttachment1.SaveData(StrVoucher)
            funNew()
            fndVoucher.Value = StrVoucher
            txtBoxVoucher.Text = StrVoucher
            funFill()
            If ChekPostBtn = True Then
            Else
                myMessages.insert()

            End If

        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Journal-Entry", MessageBoxButtons.OK)
        End Try
    End Sub



    Public Sub funDelete()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_DELETE", New SqlParameter("@Voucher_No", fndVoucher.Value))
            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_MASTER_DELETE", New SqlParameter("@Voucher_No", fndVoucher.Value))
            trans.Commit()
            myMessages.delete()
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Journal-Entry", MessageBoxButtons.OK)
        End Try
    End Sub

    Public Function funUpdate(Optional ByVal transNew As SqlTransaction = Nothing) As String
        Dim trans As SqlTransaction = Nothing
        If transNew Is Nothing Then
            trans = clsDBFuncationality.GetTransactin()
        Else
            trans = transNew
        End If

        Try
            Dim strSrcType As String = ""
            If rdbCustomer.IsChecked = True Then
                strSrcType = "C"                  '***** for : Customers
            ElseIf rdbVendor.IsChecked = True Then
                strSrcType = "V"                  '***** for: Vendors
            ElseIf rdbOther.IsChecked = True Then
                strSrcType = "O"                 '****** for: Others
            End If

            Dim strEntryType As String = ""
            If cmbType.SelectedIndex = 0 Then
                strEntryType = "N"               '****** for: Normal Entry
            ElseIf cmbType.SelectedIndex = 1 Then
                strEntryType = "A"               '****** for: Adjustment Entry 
            ElseIf cmbType.SelectedIndex = 2 Then
                strEntryType = "X"               '****** for:Closing Entry 
            ElseIf cmbType.SelectedIndex = 3 Then
                strEntryType = "O" '****** for:Opening
            End If

            Dim strRvrs As String
            Dim ReverseDate As String = ""
            If chkReverse.Checked = True Then
                strRvrs = "Y"                    '****** For: Auto-Reverse is True  
                ReverseDate = clsCommon.GetPrintDate(dtRevese.Value, "dd/MM/yyyy")
            Else
                strRvrs = "N"                    '****** For: Auto-Reverse is False
            End If

            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_MASTER_UPDATE", New SqlParameter("@Voucher_No", fndVoucher.Value), New SqlParameter("@Voucher_Date", clsCommon.GetPrintDate(dtVoucher.Value.Date, "dd/MMM/yyyy")), New SqlParameter("@Source_Code", Me.fndSrcCode.Value), New SqlParameter("@Source_Desc", Me.txtSrcDesc.Text), New SqlParameter("@Posting_Date", clsCommon.GetPrintDate(Me.dtVoucher.Value.Date, "dd/MMM/yyyy")), New SqlParameter("@Voucher_Desc", Me.txtVoucherDesc.Text), New SqlParameter("@Remarks", Me.txtRemarks.Text), New SqlParameter("@Comments", Me.txtComments.Text), New SqlParameter("@Auto_Reverse", strRvrs), New SqlParameter("@Reverse_Date", ReverseDate), New SqlParameter("@Source_Type", strSrcType), New SqlParameter("@CustVend_Code", fndCode.Value), New SqlParameter("@CustVend_Name", Me.txtCodeDesc.Text), New SqlParameter("@Transaction_Type", strEntryType), New SqlParameter("@Total_Debit_Amt", Me.txtDr.Text), New SqlParameter("@Total_Credit_Amt", Me.txtCr.Text), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", companyCode))

            Dim qry As String = "Update TSPL_JOURNAL_MASTER set Segment_Code='" & txtLocation.Value & "',IND_AS='" & IIf(chkIndAS.Checked, 1, 0) & "',MonthlyReverse='" & IIf(chkMonthly.Checked, 1, 0) & "' , AgainstVoucherNoReverseEntry = '" + txtReverseVoucher.Value + "',TapalNo='" & clsCommon.myCstr(txtTapalNo.Text) & "' " & IIf(txtDataAndTimeSelection.Checked, ",DateAndTime='" & clsCommon.GetPrintDate(txtDataAndTimeSelection.Value, "dd/MMM/yyyy hh:mm tt") & "'  ", ",DateAndTime=null ") & " where Voucher_No='" & fndVoucher.Value & "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_DELETE", New SqlParameter("@Voucher_No", fndVoucher.Value))

            For i = 0 To gdAcc1.Rows.Count - 2
                Dim strAccCode As String = clsCommon.myCstr(gdAcc1.Rows(i).Cells(1).Value)
                If clsCommon.myLen(strAccCode) > 0 Then



                    Dim LineNo As Integer = gdAcc1.Rows(i).Cells(0).Value

                    Dim strAccDesc As String = gdAcc1.Rows(i).Cells(2).Value

                    Dim strHierarchy As String = clsCommon.myCstr(gdAcc1.Rows(i).Cells(8).Value)
                    Dim strcostcentre As String = clsCommon.myCstr(gdAcc1.Rows(i).Cells(9).Value)

                    Dim Amt As Decimal
                    Dim strAmtDr As Decimal = clsCommon.myCdbl(gdAcc1.Rows(i).Cells(3).Value)
                    Dim strAmtCr As Decimal = clsCommon.myCdbl(gdAcc1.Rows(i).Cells(4).Value)
                    If strAmtDr = 0 Then
                        Amt = strAmtCr * -1
                    ElseIf strAmtCr = 0 Then
                        Amt = strAmtDr
                    End If
                    If SettingCostCenter Then
                        Dim grouptype As String = ""
                        grouptype = clsPaymentHeader.CheckGLAccountType(clsCommon.myCstr(strAccCode), trans)
                        If clsCommon.CompairString(grouptype, "Balance Sheet") = CompairStringResult.Equal Then
                        Else
                            If clsCommon.myLen(strHierarchy) <= 0 Then
                                Throw New Exception("Please provide the Hierarchy Level " + clsCommon.myCstr(LineNo))
                            ElseIf clsCommon.myLen(strcostcentre) <= 0 Then
                                Throw New Exception("Please provide the Cost Center " + clsCommon.myCstr(LineNo))
                            End If
                        End If
                    End If
                    Dim StrVoucher As String = fndVoucher.Value
                    Dim strJrnl As String = "select journal_no from TSPL_JOURNAL_MASTER where Voucher_No='" + StrVoucher + "'"
                    Dim Jrnl As String
                    Jrnl = connectSql.RunScalar(trans, strJrnl)

                    Dim strDesc As String = Convert.ToString(gdAcc1.Rows(i).Cells(5).Value)
                    Dim strRef As String = Convert.ToString(gdAcc1.Rows(i).Cells(6).Value)

                    Dim PostDate As String = clsCommon.myCstr(gdAcc1.Rows(i).Cells(7).Value)
                    If PostDate = "" Then
                        PostDate = clsCommon.GetPrintDate(dtVoucher.Value, "dd/MMM/yyyy")
                    Else
                        PostDate = clsCommon.GetPrintDate(clsCommon.myCstr(gdAcc1.Rows(i).Cells(7).Value), "dd/MMM/yyyy")
                    End If


                    Dim strQ1 As String = " SELECT     Account_Type, Account_Group_Code, Account_Seg_Code1, Account_Seg_Desc1, Account_Seg_Code2, Account_Seg_Desc2, Account_Seg_Code3, " & _
                        " Account_Seg_Desc3, Account_Seg_Code4, Account_Seg_Desc4, Account_Seg_Code5, Account_Seg_Desc5, Account_Seg_Code6, Account_Seg_Desc6," & _
                        " Account_Seg_Code7, Account_Seg_Desc7, Account_Seg_Code8, Account_Seg_Desc8, Account_Seg_Code9, Account_Seg_Desc9, " & _
                        " Account_Seg_Code10, Account_Seg_Desc10 FROM  TSPL_GL_ACCOUNTS where Account_Code='" + strAccCode + "'"
                    '' Added By abhishek as on 12/10/2012
                    myreader = clsDBFuncationality.GetDataTable(strQ1, trans)

                    If (myreader IsNot Nothing AndAlso myreader.Rows.Count > 0) Then

                        For Each dr As DataRow In myreader.Rows
                            Dim AccType As String = dr(0).ToString()
                            Dim AccGrp As String = dr(1).ToString()

                            Dim SegC1 As String = dr(2).ToString()
                            Dim SegDesc1 As String = dr(3).ToString()

                            Dim SegC2 As String = dr(4).ToString()
                            Dim SegDesc2 As String = dr(5).ToString()

                            Dim SegC3 As String = dr(6).ToString()
                            Dim SegDesc3 As String = dr(7).ToString()

                            Dim SegC4 As String = dr(8).ToString()
                            Dim SegDesc4 As String = dr(9).ToString()

                            Dim SegC5 As String = dr(10).ToString()
                            Dim SegDesc5 As String = dr(11).ToString()

                            Dim SegC6 As String = dr(12).ToString()
                            Dim SegDesc6 As String = dr(13).ToString()

                            Dim SegC7 As String = dr(14).ToString()
                            Dim SegDesc7 As String = dr(15).ToString()

                            Dim SegC8 As String = dr(16).ToString()
                            Dim SegDesc8 As String = dr(17).ToString()

                            Dim SegC9 As String = dr(18).ToString()
                            Dim SegDesc9 As String = dr(19).ToString()

                            Dim SegC10 As String = dr(20).ToString()
                            Dim SegDesc10 As String = dr(21).ToString()
                            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", clsCommon.GetPrintDate(dtVoucher.Value.Date, "dd/MMM/yyyy")), New SqlParameter("@Detail_Line_No", LineNo), New SqlParameter("@Account_code", strAccCode), New SqlParameter("@Account_Desc", strAccDesc), New SqlParameter("@Amount", Amt), New SqlParameter("@Description", strDesc), New SqlParameter("@Reference", strRef), New SqlParameter("@Posting_Date", PostDate), New SqlParameter("@Account_Type", AccType), New SqlParameter("@Account_Group_Code", AccGrp), New SqlParameter("@Account_Seg_Code1", SegC1), New SqlParameter("@Account_Seg_Desc1", SegDesc1), New SqlParameter("@Account_Seg_Code2", SegC2), New SqlParameter("@Account_Seg_Desc2", SegDesc2), New SqlParameter("@Account_Seg_Code3", SegC3), New SqlParameter("@Account_Seg_Desc3", SegDesc3), New SqlParameter("@Account_Seg_Code4", SegC4), New SqlParameter("@Account_Seg_Desc4", SegDesc4), New SqlParameter("@Account_Seg_Code5", SegC5), New SqlParameter("@Account_Seg_Desc5", SegDesc5), New SqlParameter("@Account_Seg_Code6", SegC6), New SqlParameter("@Account_Seg_Desc6", SegDesc6), New SqlParameter("@Account_Seg_Code7", SegC7), New SqlParameter("@Account_Seg_Desc7", SegDesc7), New SqlParameter("@Account_Seg_Code8", SegC8), New SqlParameter("@Account_Seg_Desc8", SegDesc8), New SqlParameter("@Account_Seg_Code9", SegC9), New SqlParameter("@Account_Seg_Desc9", SegDesc9), New SqlParameter("@Account_Seg_Code10", SegC10), New SqlParameter("@Account_Seg_Desc10", SegDesc10))
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "Hirerachy_Code", strHierarchy, True)
                            clsCommon.AddColumnsForChange(coll, "Cost_Centre_Code", strcostcentre, True)
                            clsCommonFunctionality.UpdateDataTable(coll, "tspl_journal_details", OMInsertOrUpdate.Update, "tspl_journal_details.Journal_No='" + clsCommon.myCstr(Jrnl) + "' and tspl_journal_details.Voucher_No='" + clsCommon.myCstr(StrVoucher) + "' and tspl_journal_details.Account_Code='" + clsCommon.myCstr(strAccCode) + "' and Detail_Line_No='" + clsCommon.myCstr(LineNo) + "'", trans)
                        Next
                    End If
                End If
            Next
            If transNew Is Nothing Then
                trans.Commit()
            End If
            UcAttachment1.SaveData(fndVoucher.Value)
            Return True
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Journal-Entry", MessageBoxButtons.OK)
            Return False
        End Try
    End Function

    Public Sub funFill()
        isInsideLoadData = True
        Try
            ''RICHA KDI/11/07/18-000401 PICK CUSTOMER OR VENDOR NAME FROM THEIR MASTER TABLE
            sql = " SELECT TSPL_JOURNAL_MASTER.Voucher_No, TSPL_JOURNAL_MASTER.Voucher_Date, TSPL_JOURNAL_MASTER.Source_Code, " & _
                         "  TSPL_JOURNAL_MASTER.Source_Desc, TSPL_JOURNAL_MASTER.Source_Doc_No, TSPL_JOURNAL_MASTER.Source_Doc_Date, " & _
                         "  TSPL_JOURNAL_MASTER.Posting_Date, TSPL_JOURNAL_MASTER.Voucher_Desc, TSPL_JOURNAL_MASTER.Source_Narration,  " & _
                         "  TSPL_JOURNAL_MASTER.Remarks, TSPL_JOURNAL_MASTER.Comments, TSPL_JOURNAL_MASTER.Auto_Reverse,ISNULL(TSPL_JOURNAL_MASTER.Reverse_Date,'') AS Reverse_Date,  " & _
                         "  TSPL_JOURNAL_MASTER.Source_Type, TSPL_JOURNAL_MASTER.CustVend_Code, CASE WHEN ISNULL(TSPL_JOURNAL_MASTER.Source_Type,'')='C' THEN TSPL_CUSTOMER_MASTER.CUSTOMER_NAME  WHEN ISNULL(TSPL_JOURNAL_MASTER.Source_Type,'')='V' THEN TSPL_VENDOR_MASTER.VENDOR_NAME ELSE TSPL_JOURNAL_MASTER.CustVend_Name end AS CustVend_Name,  " & _
                         "  TSPL_JOURNAL_MASTER.Transaction_Type,  " & _
                         "  TSPL_JOURNAL_MASTER.Provisional_Post, TSPL_JOURNAL_MASTER.Authorized, TSPL_JOURNAL_MASTER.Total_Debit_Amt,  " & _
                         "  TSPL_JOURNAL_MASTER.Total_Credit_Amt, TSPL_JOURNAL_DETAILS.Detail_Line_No as [Line No], TSPL_JOURNAL_DETAILS.Account_code as [Acc Code],  " & _
                         "  TSPL_JOURNAL_DETAILS.Account_Desc as [Acc Desc], case  when TSPL_JOURNAL_DETAILS.Amount >=0 then TSPL_JOURNAL_DETAILS.Amount else 0 end as DrAmt , case  when TSPL_JOURNAL_DETAILS.Amount <0 then TSPL_JOURNAL_DETAILS.Amount*-1 else 0 end as CrAmt, TSPL_JOURNAL_DETAILS.Description as [Desc],  " & _
                         "  TSPL_JOURNAL_DETAILS.Reference as [Ref], convert(varchar(11),TSPL_JOURNAL_DETAILS.Posting_Date,103) AS [Date], " & _
                         "  TSPL_JOURNAL_MASTER.SendToTally,TSPL_JOURNAL_MASTER.Segment_code,TSPL_JOURNAL_MASTER.MonthlyReverse,TSPL_JOURNAL_DETAILS.Hirerachy_code as [Hierarchy Code], " & _
                         " TSPL_JOURNAL_DETAILS.Cost_Centre_Code as [Cost Centre],TSPL_JOURNAL_MASTER.Ind_As , TSPL_JOURNAL_MASTER.AgainstVoucherNoReverseEntry,TSPL_JOURNAL_MASTER.TapalNo,TSPL_JOURNAL_MASTER.DateAndTime " & _
                         "  FROM TSPL_JOURNAL_MASTER INNER JOIN " & _
                         "  TSPL_JOURNAL_DETAILS ON   " & _
                         "  TSPL_JOURNAL_MASTER.Voucher_No = TSPL_JOURNAL_DETAILS.Voucher_No " & _
                                     "  left outer join  tspl_customer_master on tspl_customer_master.cust_code  =TSPL_JOURNAL_MASTER.CustVend_Code" & _
            " left outer join  TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.VENDOR_CODE  =TSPL_JOURNAL_MASTER.CustVend_Code " & _
            "  WHERE      TSPL_JOURNAL_MASTER.Voucher_No= '" + fndVoucher.Value + "' order by [Line No] "
            ds = connectSql.RunSQLReturnDS(sql)
            ' Dim dr As DataRow

            If ds.Tables(0).Rows.Count > 0 Then
                Me.txtBoxVoucher.Text = fndVoucher.Value
                Me.dtVoucher.Value = clsCommon.GetPrintDate(ds.Tables(0).Rows(0).Item(1).ToString(), "dd/MMM/yyyy")
                Me.fndSrcCode.Value = ds.Tables(0).Rows(0).Item(2).ToString()
                Me.txtSrcDesc.Text = ds.Tables(0).Rows(0).Item(3).ToString()
                Me.txtSrcDoc.Text = ds.Tables(0).Rows(0).Item(4).ToString()
                Me.dtSrc.Value = clsCommon.GetPrintDate(ds.Tables(0).Rows(0).Item(5).ToString(), "dd/MMM/yyyy")
                Me.txtDocDesc.Text = ds.Tables(0).Rows(0).Item(8).ToString()
                Me.txtVoucherDesc.Text = ds.Tables(0).Rows(0).Item(7).ToString()
                Me.txtRemarks.Text = ds.Tables(0).Rows(0).Item(9).ToString()
                Me.txtComments.Text = ds.Tables(0).Rows(0).Item(10).ToString()
                txtLocation.Enabled = False
                txtLocation.Value = clsCommon.myCstr(ds.Tables(0).Rows(0).Item("Segment_code"))
                lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_SEGMENT_CODE where Segment_code='" + txtLocation.Value + "'"))
                Dim strRvsrFlag As String = ds.Tables(0).Rows(0).Item(11).ToString()
                If strRvsrFlag = "Y" OrElse clsCommon.CompairString(strRvsrFlag, "R") = CompairStringResult.Equal Then '' 31-July-2015 (Reverse 'R' means entry is reversed)
                    chkReverse.Checked = True
                    Me.dtRevese.Value = clsCommon.GetPrintDate(clsCommon.myCstr(ds.Tables(0).Rows(0).Item("Reverse_Date")), "dd/MM/yyyy")
                Else
                    chkReverse.Checked = False
                    Me.dtRevese.Value = clsCommon.GETSERVERDATE()
                End If
                If clsCommon.CompairString(clsCommon.myCstr(ds.Tables(0).Rows(0).Item("MonthlyReverse")), "1") = CompairStringResult.Equal Then
                    chkMonthly.Checked = True
                Else
                    chkMonthly.Checked = False
                End If

                chkIndAS.Checked = IIf(clsCommon.myCdbl(ds.Tables(0).Rows(0).Item("IND_AS")) = 1, True, False)
                '' 31-July-2015
                If clsCommon.myLen(txtSrcDoc.Text) > 0 Then
                    LblManAuto.Text = "GENERATED"
                Else
                    LblManAuto.Text = "ENTERED"
                End If
                '' 
                Dim strSrcFlag As String = ds.Tables(0).Rows(0).Item(13).ToString()
                If strSrcFlag = "O" Then
                    rdbOther.IsChecked = True
                ElseIf strSrcFlag = "V" Then
                    rdbVendor.IsChecked = True
                ElseIf strSrcFlag = "C" Then
                    rdbCustomer.IsChecked = True
                End If

                Me.fndCode.Value = ds.Tables(0).Rows(0).Item(14).ToString()
                Me.txtCodeDesc.Text = ds.Tables(0).Rows(0).Item(15).ToString()



                Dim strETypeFlag As String = ds.Tables(0).Rows(0).Item(16).ToString()
                If strETypeFlag = "N" Then
                    cmbType.SelectedIndex = 0
                ElseIf strETypeFlag = "A" Then
                    cmbType.SelectedIndex = 1
                ElseIf strETypeFlag = "X" Then
                    cmbType.SelectedIndex = 2
                ElseIf strETypeFlag = "O" Then
                    cmbType.SelectedIndex = 3
                End If

                Dim strProvFlag As String = ds.Tables(0).Rows(0).Item(17).ToString()

                Dim strAuthFlag As String = ds.Tables(0).Rows(0).Item(18).ToString()
                Dim SendToTally As Boolean = clsCommon.myCBool(ds.Tables(0).Rows(0).Item("SendToTally"))
                txtReverseVoucher.Value = clsCommon.myCstr(ds.Tables(0).Rows(0).Item("AgainstVoucherNoReverseEntry"))
                txtReverseVoucher.Enabled = False
                If IsDBNull(ds.Tables(0).Rows(0).Item("DateAndTime")) = True Then
                    txtDataAndTimeSelection.Checked = False
                Else
                    txtDataAndTimeSelection.Checked = True
                    txtDataAndTimeSelection.Value = clsCommon.myCstr(ds.Tables(0).Rows(0).Item("DateAndTime"))
                End If
                txtTapalNo.Text = clsCommon.myCstr(ds.Tables(0).Rows(0).Item("TapalNo"))
                If clsCommon.myLen(txtReverseVoucher.Value) > 0 Then
                    funControlEnableDisable(False)
                Else
                    funControlEnableDisable(True)
                End If

                If strAuthFlag = "A" Then
                    btnSave.Text = "Update"
                    UsLock1.Status = ERPTransactionStatus.Approved
                    btnSave.Enabled = False
                    btnDelete.Enabled = False
                    btnAuth.Enabled = False
                    btnSendToTally.Enabled = True
                    btnProAuth.Enabled = False
                    gdAcc1.ReadOnly = True

                    If CostCenterAndHirerachyCodeUpdateAfterPost = True And clsCommon.myLen(txtSrcDoc.Text) <= 0 Then
                        butCostCenterAndHirerachy_Update_AfterPost.Visible = True
                    End If

                Else
                    btnSave.Text = "Update"
                    btnAuth.Enabled = True
                    btnSendToTally.Enabled = False
                    btnProAuth.Enabled = True
                    UsLock1.Status = ERPTransactionStatus.Pending
                End If
                If clsCommon.myLen(txtSrcDoc.Text) > 0 Then
                    btnSave.Enabled = False
                    btnAuth.Enabled = False
                    btnDelete.Enabled = False
                End If

                If SendToTally Then
                    btnSendToTally.Enabled = False
                End If

                gdAcc1.AutoGenerateColumns = False
                Dim bs As New BindingSource()
                bs.DataSource = ds.Tables(0)
                gdAcc1.DataSource = bs
                gdAcc1.Columns(0).FieldName = "Line No"
                gdAcc1.Columns(1).FieldName = "Acc Code"
                gdAcc1.Columns(2).FieldName = "Acc Desc"
                gdAcc1.Columns(3).FieldName = "DrAmt"
                gdAcc1.Columns(4).FieldName = "CrAmt"
                gdAcc1.Columns(5).FieldName = "Desc"
                gdAcc1.Columns(6).FieldName = "Ref"
                gdAcc1.Columns(7).FieldName = "Date"
                gdAcc1.Columns(8).FieldName = "Hierarchy Code"
                gdAcc1.Columns(9).FieldName = "Cost Centre"
                UcAttachment1.LoadData(fndVoucher.Value)
            Else
                Return
                fndVoucher.MyReadOnly = False '' Added By abhsihek as on 26 Nov 2012
            End If


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Journal-Entry", MessageBoxButtons.OK)

        End Try
        funShowAmt()

        isInsideLoadData = False
        gdAcc1.Rows.AddNew()
    End Sub

    Public Sub funShowAmt()
        Dim IncrmentCr As Decimal = 0
        Dim IncrmentDr As Decimal = 0
        For i As Integer = 0 To gdAcc1.Rows.Count - 1
            Dim DrAmt As Decimal = clsCommon.myCdbl(gdAcc1.Rows(i).Cells(3).Value)
            Dim CrAmt As Decimal = clsCommon.myCdbl(gdAcc1.Rows(i).Cells(4).Value)
            IncrmentDr = DrAmt + IncrmentDr
            IncrmentCr = CrAmt + IncrmentCr
        Next
        txtDr.Text = IncrmentDr
        txtBoxDr.Text = IncrmentDr
        txtCr.Text = IncrmentCr
        txtBoxCr.Text = IncrmentCr
        txtUnbalAmt.Text = IncrmentDr - IncrmentCr
    End Sub

    Public Sub funNew()
        funControlEnableDisable(True)
        butCostCenterAndHirerachy_Update_AfterPost.Visible = False
        txtReverseVoucher.Value = ""
        chkIndAS.Checked = False
        btnSave.Text = "Save"
        fndVoucher.Value = ""
        fndVoucher.MyReadOnly = False ' Added By abhishek as on 26 Nov 2012
        txtBoxVoucher.Text = String.Empty
        txtVoucherDesc.Text = ""
        dtVoucher.Value = clsCommon.GETSERVERDATE()
        fndSrcCode.Value = ""
        txtSrcDesc.Text = ""
        txtSrcDoc.Text = ""
        dtRevese.Value = clsCommon.GETSERVERDATE()
        dtRevese.Value = clsCommon.GETSERVERDATE()
        txtComments.Text = ""
        txtRemarks.Text = ""
        gdAcc1.DataSource = Nothing
        gdAcc1.Rows.Clear()
        txtCr.Text = "0.00"
        txtDr.Text = "0.00"
        txtBoxCr.Text = "0.00"
        txtBoxDr.Text = "0.00"
        fndCode.Enabled = False
        fndCode.Value = ""
        txtCodeDesc.Text = ""
        txtCodeDesc.Enabled = False
        rdbOther.IsChecked = True

        ''fillAccount()
        chkReverse.Checked = False
        dtRevese.Enabled = False
        txtSrcDoc.Enabled = False
        txtDocDesc.Enabled = False
        dtSrc.Enabled = False
        dtSrc.Value = clsCommon.GETSERVERDATE()
        'fndVoucher.MyReadOnly = True
        dtVoucher.Value = clsCommon.GETSERVERDATE()
        btnSave.Enabled = True
        btnDelete.Enabled = True
        btnProAuth.Enabled = False
        btnAuth.Enabled = False
        btnSendToTally.Enabled = False
        gdAcc1.ReadOnly = False
        txtUnbalAmt.Text = 0
        LblManAuto.Text = "ENTERED"
        funSrcLoad()
        isInsideLoadData = False

        gdAcc1.AllowAddNewRow = False
        gdAcc1.ShowGroupPanel = False
        gdAcc1.AllowColumnReorder = False
        gdAcc1.AllowRowReorder = False
        gdAcc1.EnableSorting = False
        gdAcc1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gdAcc1.MasterTemplate.ShowRowHeaderColumn = False
        gdAcc1.Rows.AddNew()
        UsLock1.Status = ERPTransactionStatus.Pending
        txtLocation.Enabled = True
        txtLocation.Value = ""
        lblLocation.Text = ""
        txtDataAndTimeSelection.Value = clsCommon.GETSERVERDATE()
        txtTapalNo.Text = ""
        txtDataAndTimeSelection.Checked = False
        If clsCommon.CompairString(Program_Code, clsUserMgtCode.journalEntry) = CompairStringResult.Equal Then
            chkReverse.Enabled = False
            chkMonthly.Enabled = False
            dtRevese.Enabled = False
            txtReverseVoucher.Enabled = True
        ElseIf clsCommon.CompairString(Program_Code, clsUserMgtCode.ReversejournalEntry) = CompairStringResult.Equal Then
            chkReverse.Checked = True
            chkReverse.Enabled = False
            chkMonthly.Enabled = True
            dtRevese.Enabled = True
        End If
        UcAttachment1.BlankAllControls()
    End Sub

#End Region

#Region "Event"

    Private Sub chkReverse_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkReverse.ToggleStateChanged
        If chkReverse.Checked = True Then
            dtRevese.Enabled = True
            chkMonthly.Enabled = True
        Else
            dtRevese.Enabled = False
            chkMonthly.Enabled = False
        End If
    End Sub

    Private Sub gdAcc1_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gdAcc1.CellFormatting
        If gdAcc1.Rows.Count > 0 AndAlso gdAcc1.CurrentRow IsNot Nothing Then
            If clsCommon.myLen(gdAcc1.CurrentRow.Cells("gdcolAcc").Value) > 0 Then
                If SettingCostCenter Then
                    Dim grouptype As String = ""
                    grouptype = clsPaymentHeader.CheckGLAccountType(clsCommon.myCstr(gdAcc1.CurrentRow.Cells("gdcolAcc").Value), Nothing)
                    If clsCommon.CompairString(grouptype, "Balance Sheet") <> CompairStringResult.Equal Then
                        gdAcc1.CurrentRow.Cells("Hierarchy Code").ReadOnly = False
                        gdAcc1.CurrentRow.Cells("Cost Centre").ReadOnly = False
                    Else
                        gdAcc1.CurrentRow.Cells("Hierarchy Code").ReadOnly = True
                        gdAcc1.CurrentRow.Cells("Cost Centre").ReadOnly = True
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub gdAcc1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gdAcc1.CellValueChanged
        funCellValueChange()
    End Sub

    Private Sub funCellValueChange()
        If (Not isInsideLoadData) Then
            If (Not isCellValueChangedOpen) Then
                isCellValueChangedOpen = True
                If gdAcc1.CurrentColumn Is gdAcc1.Columns("gdcolAcc") Then
                    If clsCommon.myLen(txtLocation.Value) <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "First Select Location.", Me.Text)
                        gdAcc1.CurrentRow.Cells(1).Value = ""
                        txtLocation.Focus()
                        isCellValueChangedOpen = False
                        Exit Sub
                    End If
                    OpenGLAccount(False)
                    If clsCommon.myLen(gdAcc1.CurrentRow.Cells("gdcolAcc").Value) <= 0 Then
                        gdAcc1.CurrentRow.Cells("gdcolDr").Value = 0
                        gdAcc1.CurrentRow.Cells("gdcolCr").Value = 0
                        gdAcc1.CurrentRow.Cells("gdcolDr").ReadOnly = True
                        gdAcc1.CurrentRow.Cells("gdcolCr").ReadOnly = True
                    Else
                        gdAcc1.CurrentRow.Cells("gdcolDr").ReadOnly = False
                        gdAcc1.CurrentRow.Cells("gdcolCr").ReadOnly = False
                    End If
                End If
                If SettingCostCenter Then
                    If (clsCommon.CompairString(gdAcc1.CurrentColumn.Name, "Hierarchy Code") = CompairStringResult.Equal) Then
                        Dim grouptype As String = ""
                        grouptype = clsPaymentHeader.CheckGLAccountType(clsCommon.myCstr(gdAcc1.CurrentRow.Cells("gdcolAcc").Value), Nothing)
                        If clsCommon.CompairString(grouptype, "Balance Sheet") <> CompairStringResult.Equal Then
                            OpenHierarchyCode(False)
                        End If
                    End If
                    If (clsCommon.CompairString(gdAcc1.CurrentColumn.Name, "Cost Centre") = CompairStringResult.Equal) Then
                        Dim grouptype As String = ""
                        grouptype = clsPaymentHeader.CheckGLAccountType(clsCommon.myCstr(gdAcc1.CurrentRow.Cells("gdcolAcc").Value), Nothing)
                        If clsCommon.CompairString(grouptype, "Balance Sheet") <> CompairStringResult.Equal Then
                            OpenCostCenterCode(False)
                        End If
                    End If
                End If

                If gdAcc1.CurrentColumn Is gdAcc1.Columns("gdcolDr") Then
                    If clsCommon.myCdbl(gdAcc1.CurrentRow.Cells("gdcolDr").Value) > 0 Then
                        gdAcc1.CurrentRow.Cells("gdcolCr").Value = 0
                    End If
                End If

                If gdAcc1.CurrentColumn Is gdAcc1.Columns("gdcolCr") Then
                    If clsCommon.myCdbl(gdAcc1.CurrentRow.Cells("gdcolCr").Value) > 0 Then
                        gdAcc1.CurrentRow.Cells("gdcolDr").Value = 0
                    End If
                End If
                Dim gdDate As GridViewDateTimeColumn = TryCast(gdAcc1.Columns(7), GridViewDateTimeColumn)
                gdDate.DataSourceNullValue = connectSql.serverDate()

                isCellValueChangedOpen = False
            End If
        End If
        funShowAmt()
    End Sub

    Private Sub OpenHierarchyCode(ByVal isButtonClick As Boolean)
        Dim qry As String = " select TSPL_HIRERACHY_LEVEL_MASTER.HIRERACHY_CODE as [Code] ,TSPL_HIRERACHY_LEVEL_MASTER.Description as [Description],TSPL_HIRERACHY_LEVEL_MASTER.Level as [Level] ,TSPL_HIRERACHY_LEVEL_MASTER.Created_By as [Created By] ,CONVERT(VARCHAR,TSPL_HIRERACHY_LEVEL_MASTER.Created_Date,103) as [Created Date] ,TSPL_HIRERACHY_LEVEL_MASTER.Modified_By as [Modified By] ,CONVERT(VARCHAR,TSPL_HIRERACHY_LEVEL_MASTER.Modified_Date,103) as [Modified Date]  From TSPL_HIRERACHY_LEVEL_MASTER  "
        gdAcc1.CurrentRow.Cells("Hierarchy Code").Value = clsCommon.ShowSelectForm("HierarchyPN", qry, "Code", "", clsCommon.myCstr(gdAcc1.CurrentRow.Cells("Hierarchy Code").Value), "Code", isButtonClick)
        'gdAcc1.CurrentRow.Cells(colHirerachyName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_HIRERACHY_LEVEL_MASTER where HIRERACHY_CODE='" + clsCommon.myCstr(gdAcc1.CurrentRow.Cells(colHirerachyCenter).Value) + "'"))
    End Sub

    Private Sub OpenCostCenterCode(ByVal isButtonClick As Boolean)
        If clsCommon.myLen(clsCommon.myCstr(gdAcc1.CurrentRow.Cells("Hierarchy Code").Value)) > 0 Then
            Dim DBLevel As String = String.Empty
            DBLevel = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Level,'') AS Level from TSPL_HIRERACHY_LEVEL_MASTER Where HIRERACHY_CODE='" + clsCommon.myCstr(gdAcc1.CurrentRow.Cells("Hierarchy Code").Value) + "' "))
            Dim qry As String = "select TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code as [Code] ,TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name,ISNULL(TSPL_COST_CENTRE_FINANCIAL.Hirerachy_Level_Code,'') AS [Hirerachy Level Code],ISNULL(TSPL_COST_CENTRE_FINANCIAL.Cost_Centre_Fin_Level_Code,'') AS [Cost Centre Fin Level Code],ISNULL(TSPL_COST_CENTRE_FINANCIAL.Hirerachy_Level,'') AS [Hirerachy Level] ,TSPL_COST_CENTRE_FINANCIAL.Created_By as [Created By] ,Convert(varchar,TSPL_COST_CENTRE_FINANCIAL.Created_Date,103) as [Created Date] ,TSPL_COST_CENTRE_FINANCIAL.Modified_By as [Modified By] ,Convert(varchar,TSPL_COST_CENTRE_FINANCIAL.Modified_Date,103) as [Modified Date]  From TSPL_COST_CENTRE_FINANCIAL "
            gdAcc1.CurrentRow.Cells("Cost Centre").Value = clsCommon.ShowSelectForm("HierarchyPNCc", qry, "Code", " Hirerachy_Level = '" + DBLevel + "'", clsCommon.myCstr(gdAcc1.CurrentRow.Cells("Cost Centre").Value), "Code", isButtonClick)
            'gdAcc1.CurrentRow.Cells(colCostCenterName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cost_Center_Fin_Name from TSPL_COST_CENTRE_FINANCIAL where Cost_Center_Fin_Code='" + clsCommon.myCstr(gdAcc1.CurrentRow.Cells(colCostCenter).Value) + "'"))
        Else
            clsCommon.MyMessageBoxShow(Me, "Please select hirerachy level first.", Me.Text)
        End If
    End Sub

    Private Sub btnNew_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.MouseHover
        ToolTip1.Show("New", btnNew)
    End Sub
    Private Sub frmJournalEntry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F2 Then
            If gdAcc1.CurrentCell.ColumnInfo.Name = "gdcolAcc" Then
                OpenGLAccount(True)
            End If
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            If clsCommon.myCdbl(txtCr.Text) = 0 And clsCommon.myCdbl(txtDr.Text) = 0 Then
                Return
            Else
                funCellValueChange()
                savedata(False)
            End If
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            deletedata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
            funNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            btnPostClick(False)
        ElseIf e.Control AndAlso e.KeyCode = Keys.P Then
            PrintData(fndVoucher.Value, "")
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            closeform()

        ElseIf e.Alt AndAlso e.Control AndAlso e.Shift AndAlso e.KeyCode = Keys.F12 Then

            If clsCommon.myLen(fndVoucher.Value) > 0 Then
                If MyBase.isReverse Then
                    Dim frm As New FrmPWD(Nothing)
                    frm.strType = "SIRC"
                    frm.strCode = "SIReversAndCreate"
                    frm.ShowDialog()
                    If frm.isPasswordCorrect Then
                        btnUnpostTransaction.Visible = True
                    End If
                Else
                    clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
                    'MessageBox.Show("You are not authorized to perform this action.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            End If
            ElseIf e.Alt AndAlso e.Control AndAlso e.Shift AndAlso e.KeyCode = Keys.F11 Then
            Panel1.Visible = Not Panel1.Visible
        End If
        If gdAcc1.CurrentColumn.Index = 0 And e.KeyCode = Keys.Delete Then
            If myMessages.deleteConfirm = True Then
                gdAcc1.CurrentRow.Delete()
            End If
        End If
    End Sub

#End Region

#Region "RadioButton"
    Private Sub rdbCustomer_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdbCustomer.ToggleStateChanged
        If rdbCustomer.IsChecked = True Then
            fndCode.Enabled = True
            ''''fndCode.Query = "select Cust_Code as [Customer Code],Customer_Name as [Name],Cust_Group_Code as [Customer Group],(select case when Status='N' then 'Active' else 'In.Active' end ) as [Status] from TSPL_CUSTOMER_MASTER  "
            ''''fndCode.ConnectionString = connectSql.SqlCon()
            ''''fndCode.Caption = "Source Type"
            ''''fndCode.ValueToSelect = "Customer Code"
            ''''fndCode.ValueToSelect1 = "Name"
        End If
    End Sub
    Private Sub rdbVendor_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdbVendor.ToggleStateChanged
        If rdbVendor.IsChecked = True Then
            fndCode.Enabled = True
            ''''fndCode.Query = "select Cust_Code as [Customer Code],Customer_Name as [Name],Cust_Group_Code as [Customer Group],(select case when Status='N' then 'Active' else 'In.Active' end ) as [Status] from TSPL_CUSTOMER_MASTER  "
            ''''fndCode.ConnectionString = connectSql.SqlCon()
            ''''fndCode.Caption = "Source Type"
            ''''fndCode.ValueToSelect = "Customer Code"
            ''''fndCode.ValueToSelect1 = "Name"
        End If
    End Sub
    Private Sub rdbOther_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdbOther.ToggleStateChanged
        If rdbOther.IsChecked = True Then
            fndCode.Enabled = False
            fndCode.Value = String.Empty
        End If
    End Sub
    Private Function fnSrc(ByVal strSrcId As String) As String
        Dim strSrcDesc As String
        strSrcDesc = ""
        Dim strcmd As String
        strcmd = "select Cust_Code as [Customer Code],Customer_Name as [Name],Cust_Group_Code as [Customer Group],(select case when Status='N' then 'Active' else 'In.Active' end ) as [Status] from TSPL_CUSTOMER_MASTER where Cust_Code='" + strSrcId + "'  "
        Try
            '' Added By abhishek as on 12/10/2012
            myreader = clsDBFuncationality.GetDataTable(strcmd)
            If (myreader IsNot Nothing AndAlso myreader.Rows.Count > 0) Then
                For Each dr As DataRow In myreader.Rows
                    strSrcDesc = Convert.ToString(dr(1).ToString().Trim())
                Next
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Journal-Entry", MessageBoxButtons.OK)

        End Try
        Return strSrcDesc
    End Function
    Private Sub SrcCode_txtEvent(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.txtCodeDesc.Text = fnSrc(fndCode.Value)
    End Sub
#End Region

    Private Sub RadButton1_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDrillDown.MouseHover
        ToolTip1.Show("Drill Down", btnDrillDown)
    End Sub

    Private Sub mItmExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mItmExit.Click
        Me.Close()
    End Sub



    Private Sub btnUnpostTransaction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnpostTransaction.Click
        If clsCommon.myLen(fndVoucher.Value) > 0 Then
            Dim qry As String = "select 1 from TSPL_JOURNAL_MASTER where Voucher_No='" + fndVoucher.Value + "' and Authorized='A' and isnull(Source_Doc_No,'') ='' and Source_Code ='GL-JE' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If common.clsCommon.MyMessageBoxShow("Unpost the current transaction " + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                    qry = "update TSPL_JOURNAL_MASTER set Authorized='N' where Voucher_No='" + fndVoucher.Value + "'  "
                    clsDBFuncationality.ExecuteNonQuery(qry)
                    common.clsCommon.MyMessageBoxShow(Me, "Transaction Unposted Successfully", Me.Text)
                    funFill()
                    btnSave.Enabled = True
                    btnDelete.Enabled = True
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "Transaction Should be Posted / Source Code should be GL-JE.", Me.Text)
            End If
        End If
    End Sub

    Public Function CheckLocForLock() As Boolean
        Try

            If gdAcc1.Rows.Count > 0 Then
                For i = 0 To gdAcc1.Rows.Count - 1
                    If clsCommon.myLen(gdAcc1.Rows(i).Cells(1).Value) > 0 Then


                        Dim LocSeg As String = gdAcc1.Rows(i).Cells(1).Value.ToString()
                        Dim qry As String = " select Description  from TSPL_GL_SEGMENT_CODE  where Seg_No ='7' and  Segment_code ='" + LocSeg.Trim.Substring(clsCommon.myLen(LocSeg) - 3) + "'"
                        Dim LocSegName As String = clsDBFuncationality.getSingleValue(qry)
                        If clsCommon.myLen(LocSegName) > 0 Then

                        Else
                            LocSegName = LocSeg.Trim.Substring(clsCommon.myLen(LocSeg) - 3)
                        End If

                        clsERPFuncationality.ValidateLocationSegment(objCommonVar.CurrentCompanyCode, "General Ledger", "Journal Entry", LocSeg.Trim.Substring(clsCommon.myLen(LocSeg) - 3), dtVoucher.Value, Nothing)

                    End If
                Next
            End If

            Return True
        Catch ex As Exception

            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try


    End Function

    Private Sub gdAcc1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gdAcc1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow(Me, "Do you want to delete current row?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub gdAcc1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gdAcc1.CurrentColumnChanged
        If Not isInsideLoadData Then
            isCellValueChangedOpen = True
            If gdAcc1.RowCount > 0 Then
                Dim intCurrRow As Integer = gdAcc1.CurrentRow.Index
                gdAcc1.CurrentRow.Cells(0).Value = clsCommon.myCdbl(intCurrRow + 1)
                If intCurrRow = gdAcc1.Rows.Count - 1 Then
                    gdAcc1.Rows.AddNew()
                    gdAcc1.CurrentRow = gdAcc1.Rows(intCurrRow)
                End If
            End If
            isCellValueChangedOpen = False
        End If
    End Sub

    Private Sub SendToTally()

        Dim strQry As String = " "
        Dim IsSend As Boolean = True

        strQry = " SELECT TSPL_JOURNAL_MASTER.Voucher_No, TSPL_JOURNAL_MASTER.Voucher_Date, TSPL_JOURNAL_MASTER.Voucher_Desc, TSPL_JOURNAL_DETAILS.Detail_Line_No as [Line No], TSPL_JOURNAL_DETAILS.Account_code as [Acc Code],    TSPL_JOURNAL_DETAILS.Account_Desc as [Acc Desc], case  when TSPL_JOURNAL_DETAILS.Amount >=0 then TSPL_JOURNAL_DETAILS.Amount else 0 end as DrAmt , case  when TSPL_JOURNAL_DETAILS.Amount <0 then TSPL_JOURNAL_DETAILS.Amount*-1 else 0 end as CrAmt, TSPL_JOURNAL_MASTER.Authorized,TSPL_JOURNAL_MASTER.SendToTally " & _
                " FROM TSPL_JOURNAL_MASTER" & _
                " INNER JOIN TSPL_JOURNAL_DETAILS ON TSPL_JOURNAL_MASTER.Voucher_No = TSPL_JOURNAL_DETAILS.Voucher_No " & _
                " WHERE TSPL_JOURNAL_MASTER.Voucher_No= '" + fndVoucher.Value + "' order by [Line No] "
        Dim DT As DataTable = clsDBFuncationality.GetDataTable(strQry)

        If DT IsNot Nothing AndAlso DT.Rows.Count > 0 Then

            Dim DrBase As DataRow = DT.Rows(0)
            If clsCommon.myCBool(DrBase("SendToTally")) Then
                clsCommon.MyMessageBoxShow(Me, "Voucher already Exported to Tally.", Me.Text)
            End If

            If clsCommon.CompairString(DrBase("Authorized"), "A") = CompairStringResult.Equal Then

                Dim ErrorLog As New IO.StreamWriter(My.Application.Info.DirectoryPath & "\Error.log", True)
                ''Dim R As Voucher
                ''Dim J As Voucher
                ''Dim DebitLedger(0) As String
                ''Dim CreditLedger(0) As String
                ''Dim Amt(0) As String

                ''R = New Voucher
                ''J = New Voucher
                ''R.ObjTally = TM
                ''J.ObjTally = TM
                'R.VchType = VoucherType.Receipt
                'DebitLedger(0) = Row("bankname")
                'Amt(0) = Row("receivedamount") '- Row("actualinterest")
                'CreditLedger(0) = Row("ledgername")
                'R.VoucherDate = Row("receivingdate")
                'R.DebitLedger.Add(New Debit(Row("bankname"), Row("receivedamount")))
                'R.CreditLedger.Add(New Credit(Row("ledgername"), Row("receivedamount")))
                'R.Create()
                'If R.GetResult = TallyResult.Errors Or R.GetResult = 0 Then
                '    ErrorLog.Write("-------" & Now & " - Receipt" & vbCrLf & R.GetErrorDetail & vbCrLf)
                'Else
                '    'sQuery = "UPDATE receiptvoucher SET issendtotally=TRUE ,vchid=" & R.Id & "  WHERE receiptvoucherid=" & Row("receiptvoucherid")
                '    'ExecuteQuery(sQuery, Me.conn.cn)
                'End If

                'Next
                TM = New common.Tally(sCompany, TallyIP & ":" & TallyPort)


                TM.CompanyName = sCompany
                TM.Tally_Destination = TallyIP & ":" & TallyPort
                Dim vch As New common.Voucher()
                ' Dim vchid As Integer
                With vch
                    .Action = common.TallyAction.Create
                    .ObjTally = TM
                    .VoucherNumber = clsCommon.myCstr(DrBase("Voucher_No"))
                    .VoucherDate = clsCommon.GetPrintDate(DrBase("Voucher_Date"), "dd/MMM/yyyy")
                    .VchType = common.VoucherType.Journal
                    .Narration = clsCommon.myCstr(DrBase("Voucher_Desc"))
                    For Each Row As DataRow In DT.Rows
                        If clsCommon.myLen(Row("Acc Code")) > 0 Then
                            createLedger(clsCommon.myCstr(Row("Acc Code")), clsCommon.myCstr(Row("Acc Desc")))
                            If clsCommon.myCdbl(Row("DrAmt")) > 0 Then '= "Debit" 
                                .DebitLedger.Add(New common.Debit(clsCommon.myCstr(Row("Acc Code")), clsCommon.myCdbl(Row("DrAmt"))))
                            End If

                            If clsCommon.myCdbl(Row("CrAmt")) > 0 Then '= "Credit" 
                                .CreditLedger.Add(New common.Credit(clsCommon.myCstr(Row("Acc Code")), clsCommon.myCdbl(Row("CrAmt"))))
                            End If
                        End If
                    Next

                    'If Row.Item("modificationmode") = "Credit" Then
                    '    .CreditLedger.Add(New common.Credit(Row.Item("ledgername").ToString, Row.Item("modifiedamount")))
                    'End If


                    .Create()
                    If .GetResult = common.TallyResult.Errors Or .GetResult = 0 Then
                        IsSend = False
                        MsgBox(vch.GetErrorDetail & vbCrLf & vch.GetLastResponse)
                        ErrorLog.Write("-------" & Now & " - Journal Entry " & vbCrLf & vch.GetErrorDetail & vbCrLf)
                    End If
                End With
                ErrorLog.Close()
                If IsSend Then
                    strQry = " Update TSPL_JOURNAL_MASTER set SendToTally = 1 " & _
                             " WHERE Voucher_No= '" + fndVoucher.Value + "' "
                    clsDBFuncationality.ExecuteNonQuery(strQry)
                    btnSendToTally.Enabled = False
                    clsCommon.MyMessageBoxShow(Me, "Data Send To Tally Successfully.", Me.Text)
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "Only Posted Entry can Sent to Tally.", Me.Text)
            End If

        End If
    End Sub

    Sub createLedger(ByVal LedgerName As String, ByVal LedgerAlias As String)

        Dim AlisList As New List(Of String)
        AlisList.Add(LedgerAlias)

        Dim sAppLedger As String
        Dim L1 As New common.Ledger()
        Dim sAppGroup As String = "Current Assets"
        L1.ObjTally.CompanyName = sCompany
        L1.ObjTally.Tally_Destination = TallyIP & ":" & TallyPort
        sAppLedger = LedgerName
        L1.LedgerName = sAppLedger
        'L1.AdditionalName = sAppLedger
        L1.AliasList = AlisList

        L1.Parent = sAppGroup
        L1.Create()

        If L1.GetResult = TallyResult.Errors Or L1.GetResult = 0 Then
            MsgBox(L1.GetErrorDetail & vbCrLf & L1.GetLastResponse)
            Exit Sub
        Else
            'MsgBox("Ledger Created Successfully")
        End If


        ' '' Creating advance ledger
        ''L1.LedgerName = "Advance For " & sAppLedger
        ''L1.Parent = "Indirect Incomes"
        ''L1.Create()
        ''If L1.GetResult = TallyResult.Errors Or L1.GetResult = 0 Then
        ''    MsgBox(L1.GetLastResponse)
        ''    Exit Sub
        ''Else
        ''    'sQuery = "UPDATE applicantdetails SET ledgername='" & sAppLedger & "'," _
        ''    '& " advledger='" & "Advance For " & sAppLedger & "' WHERE Applicantid = " & .Item(iRow, 1).CellValue
        ''    'ExecuteQuery(sQuery, conn.cn)
        ''    MsgBox("Parent Ledger Created Successfully")
        ''End If

        'MsgBox("Successfully created")
    End Sub

    Private Sub btnSendToTally_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendToTally.Click
        If clsCommon.myLen(fndVoucher.Value) > 0 Then
            'SendToTally()
        Else
            clsCommon.MyMessageBoxShow(Me, "Please select a voucher for Sent to Tally.", Me.Text)
        End If
    End Sub


    Private Sub txtLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtLocation._MYValidating
        Try
            Dim qry As String = "select Segment_code as Code,Description from TSPL_GL_SEGMENT_CODE "
            Dim WhrCls As String = "Seg_No='7'"

            '=============================
            Dim chkGridEmpty As Integer = 0
            If gdAcc1.Rows.Count > 0 Then
                For i = 0 To gdAcc1.Rows.Count - 1
                    Dim strAccCode As String = gdAcc1.Rows(i).Cells(1).Value
                    If clsCommon.myLen(strAccCode) > 0 Then
                        chkGridEmpty += 1
                    End If
                Next
            End If
            If chkGridEmpty > 1 Then
                If myMessages.GLAccountRefreshConfirm() Then
                    gdAcc1.DataSource = Nothing
                    gdAcc1.Rows.Clear()
                    gdAcc1.Rows.AddNew()
                    txtLocation.Value = clsCommon.ShowSelectForm("fndSeegmentMain", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
                    lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_SEGMENT_CODE where Segment_code='" + txtLocation.Value + "'"))
                End If
            Else
                txtLocation.Value = clsCommon.ShowSelectForm("fndSeegmentMain", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
                lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_SEGMENT_CODE where Segment_code='" + txtLocation.Value + "'"))
            End If

            '=============================


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    '= KUNAL > TICKET : BM00000009586 ===
    Private Sub dtVoucher_ValueChanged(sender As Object, e As EventArgs) Handles dtVoucher.ValueChanged
        Try
            dtSrc.Value = dtVoucher.Value
            dtRevese.Value = dtVoucher.Value

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub chkMonthly_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkMonthly.ToggleStateChanged
        If chkMonthly.Checked = True Then
            dtRevese.Value = clsCommon.GETSERVERDATE.AddDays(1)
        Else
            dtRevese.Value = clsCommon.GETSERVERDATE()
        End If
    End Sub

    Private Sub RadButton1_Click_1(sender As Object, e As EventArgs) Handles RadButton1.Click
        Try
            Try
                clsDBFuncationality.ExecuteNonQuery("drop table TSPL_JOURNAL_DETAILS_WIN")
            Catch ex As Exception
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
            Try
                clsDBFuncationality.ExecuteNonQuery("drop table TSPL_JOURNAL_DETAILS_DL")
            Catch ex As Exception
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
            Try
                clsDBFuncationality.ExecuteNonQuery("drop table TSPL_JOURNAL_DETAILS_DL_SC")
            Catch ex As Exception
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try

            Dim coll As Dictionary(Of String, String)
            coll = New Dictionary(Of String, String)
            coll.Add("CustVend_Code", "varchar(12) NULL")
            coll.Add("CustVend_Name", "varchar(200) NULL")
            coll.Add("PK_Id", "integer NOT NULL")
            coll.Add("Journal_No", "int  NOT NULL")
            coll.Add("Voucher_No", "varchar(30)  NOT NULL")
            coll.Add("Voucher_Date", "datetime NULL")
            coll.Add("Detail_Line_No", "int  NOT NULL")
            coll.Add("Account_code", "varchar(50) NULL")
            coll.Add("Account_Desc", "varchar(100) NULL")
            coll.Add("Amount", "decimal (18,2) NULL")
            coll.Add("Description", "varchar(200) NULL")
            coll.Add("Reference", "varchar(200) NULL")
            coll.Add("Posting_Date", "datetime NULL")
            coll.Add("Account_Type", "varchar(20) NULL")
            coll.Add("Account_Group_Code", "varchar(12) NULL")
            coll.Add("Account_Seg_Code1", "varchar(12) NULL")
            coll.Add("Account_Seg_Desc1", "varchar(100) NULL")
            coll.Add("Account_Seg_Code2", "varchar(12) NULL")
            coll.Add("Account_Seg_Desc2", "varchar(50) NULL")
            coll.Add("Account_Seg_Code3", "varchar(12) NULL")
            coll.Add("Account_Seg_Desc3", "varchar(50) NULL")
            coll.Add("Account_Seg_Code4", "varchar(12) NULL")
            coll.Add("Account_Seg_Desc4", "varchar(50) NULL")
            coll.Add("Account_Seg_Code5", "varchar(12) NULL")
            coll.Add("Account_Seg_Desc5", "varchar(50) NULL")
            coll.Add("Account_Seg_Code6", "varchar(12) NULL")
            coll.Add("Account_Seg_Desc6", "varchar(50) NULL")
            coll.Add("Account_Seg_Code7", "varchar(12) NULL")
            coll.Add("Account_Seg_Desc7", "varchar(50) NULL")
            coll.Add("Account_Seg_Code8", "varchar(12) NULL")
            coll.Add("Account_Seg_Desc8", "varchar(50) NULL")
            coll.Add("Account_Seg_Code9", "varchar(12) NULL")
            coll.Add("Account_Seg_Desc9", "varchar(50) NULL")
            coll.Add("Account_Seg_Code10", "varchar(12) NULL")
            coll.Add("Account_Seg_Desc10", "varchar(50) NULL")
            coll.Add("Hirerachy_Code", "Varchar(30) null")
            coll.Add("Cost_Centre_Code", "Varchar(30) null")
            coll.Add("OP_TYPE", "varchar(1) NOT NULL") '' INSERT-I,DELETE-D 
            coll.Add("Post_To_Unpost", "Varchar(1) NOT NULL default 'N'") '' INSERT-I,DELETE-D 
            clsCommonFunctionality.CreateOrAlterTable("TSPL_JOURNAL_DETAILS_WIN", coll)


            coll = New Dictionary(Of String, String)
            coll.Add("B_Date", "date not NULL")
            coll.Add("Account_code", "varchar(50) not NULL")
            coll.Add("Dr_Amount", "decimal (18,2) not NULL default 0")
            coll.Add("Cr_Amount", "decimal (18,2) not NULL default 0")
            coll.Add("CL_Amount", "decimal (18,2) not NULL default 0")
            coll.Add("Dr_Adjustment", "decimal (18,2) not NULL default 0")
            coll.Add("Cr_Adjustment", "decimal (18,2) not NULL default 0")
            coll.Add("CL_Adjustment", "decimal (18,2) not NULL default 0")
            coll.Add("Dr_Closing", "decimal (18,2) not NULL default 0")
            coll.Add("Cr_Closing", "decimal (18,2) not NULL default 0")
            coll.Add("CL_Closing", "decimal (18,2) not NULL default 0")
            coll.Add("Dr_IND", "decimal (18,2) not NULL default 0")
            coll.Add("Cr_IND", "decimal (18,2) not NULL default 0")
            coll.Add("CL_IND", "decimal (18,2) not NULL default 0")
            clsCommonFunctionality.CreateOrAlterTable("TSPL_JOURNAL_DETAILS_DL", coll, "Primary Key (B_Date,Account_code)")

            coll = New Dictionary(Of String, String)
            coll.Add("B_Date", "date not NULL")
            coll.Add("Account_code", "varchar(50) not NULL")
            coll.Add("Source_Code", "varchar(5) not NULL")
            coll.Add("Dr_Amount", "decimal (18,2) not NULL default 0")
            coll.Add("Cr_Amount", "decimal (18,2) not NULL default 0")
            coll.Add("CL_Amount", "decimal (18,2) not NULL default 0")
            coll.Add("Dr_Adjustment", "decimal (18,2) not NULL default 0")
            coll.Add("Cr_Adjustment", "decimal (18,2) not NULL default 0")
            coll.Add("CL_Adjustment", "decimal (18,2) not NULL default 0")
            coll.Add("Dr_Closing", "decimal (18,2) not NULL default 0")
            coll.Add("Cr_Closing", "decimal (18,2) not NULL default 0")
            coll.Add("CL_Closing", "decimal (18,2) not NULL default 0")
            coll.Add("Dr_IND", "decimal (18,2) not NULL default 0")
            coll.Add("Cr_IND", "decimal (18,2) not NULL default 0")
            coll.Add("CL_IND", "decimal (18,2) not NULL default 0")
            clsCommonFunctionality.CreateOrAlterTable("TSPL_JOURNAL_DETAILS_DL_SC", coll, "Primary Key (B_Date,Account_code,Source_Code)")





            Dim qry As String = " AS " + Environment.NewLine + _
                " BEGIN " + Environment.NewLine + _
                " SET NOCOUNT ON;" + Environment.NewLine + _
                "DECLARE @trancount int;" + Environment.NewLine + _
                "SET @trancount = @@trancount;" + Environment.NewLine + _
                "BEGIN TRY" + Environment.NewLine + _
                "  IF @trancount = 0" + Environment.NewLine + _
                "BEGIN TRANSACTION" + Environment.NewLine + _
                "Else" + Environment.NewLine + _
                " SAVE TRANSACTION SP_TSPL_JOURNAL_DETAILS_DL;" + Environment.NewLine + _
                " -- Do the actual work here" + Environment.NewLine + _
     "--------------Step 1-------------" + Environment.NewLine + _
    "MERGE INTO TSPL_JOURNAL_DETAILS_DL " + Environment.NewLine + _
" using( " + Environment.NewLine + _
"select voucher_Date,Account_code" + Environment.NewLine + _
",sum(DrAmount * case when OP_TYPE='D' then -1 else 1 end) as DrAmount" + Environment.NewLine + _
",sum(CrAmount * case when OP_TYPE='D' then -1 else 1 end) as CrAmount" + Environment.NewLine + _
",sum(DrAdjustment * case when OP_TYPE='D' then -1 else 1 end) DrAdjustment" + Environment.NewLine + _
",sum(CrAdjustment * case when OP_TYPE='D' then -1 else 1 end) as CrAdjustment" + Environment.NewLine + _
",sum(DrClosing * case when OP_TYPE='D' then -1 else 1 end) as DrClosing" + Environment.NewLine + _
",sum(CrClosing * case when OP_TYPE='D' then -1 else 1 end) as CrClosing" + Environment.NewLine + _
",sum(DrInd * case when OP_TYPE='D' then -1 else 1 end) as DrInd" + Environment.NewLine + _
",sum(CrInd * case when OP_TYPE='D' then -1 else 1 end) as CrInd" + Environment.NewLine + _
"from (" + Environment.NewLine + _
"select TSPL_JOURNAL_DETAILS_WIN.Voucher_No,CONVERT(date,TSPL_JOURNAL_DETAILS_WIN.Voucher_Date,103) as Voucher_Date,Account_code" + Environment.NewLine + _
",case when TSPL_JOURNAL_DETAILS_WIN.Amount>0 then TSPL_JOURNAL_DETAILS_WIN.Amount else 0 end as DrAmount" + Environment.NewLine + _
",case when TSPL_JOURNAL_DETAILS_WIN.Amount<0 then -1*TSPL_JOURNAL_DETAILS_WIN.Amount else 0 end as CrAmount" + Environment.NewLine + _
",case when TSPL_JOURNAL_MASTER.Transaction_Type='A' and TSPL_JOURNAL_DETAILS_WIN.Amount>0 then TSPL_JOURNAL_DETAILS_WIN.Amount else 0 end as DrAdjustment" + Environment.NewLine + _
",case when TSPL_JOURNAL_MASTER.Transaction_Type='A' and TSPL_JOURNAL_DETAILS_WIN.Amount<0 then -1*TSPL_JOURNAL_DETAILS_WIN.Amount else 0 end as CrAdjustment" + Environment.NewLine + _
",case when TSPL_JOURNAL_MASTER.Transaction_Type='X' and TSPL_JOURNAL_DETAILS_WIN.Amount>0 then TSPL_JOURNAL_DETAILS_WIN.Amount else 0 end as DrClosing" + Environment.NewLine + _
",case when TSPL_JOURNAL_MASTER.Transaction_Type='X' and TSPL_JOURNAL_DETAILS_WIN.Amount<0 then -1*TSPL_JOURNAL_DETAILS_WIN.Amount else 0 end as CrClosing" + Environment.NewLine + _
",case when TSPL_JOURNAL_MASTER.ind_as = 1 and TSPL_JOURNAL_DETAILS_WIN.Amount>0 then TSPL_JOURNAL_DETAILS_WIN.Amount else 0 end as DrInd" + Environment.NewLine + _
",case when TSPL_JOURNAL_MASTER.ind_as = 1 and TSPL_JOURNAL_DETAILS_WIN.Amount<0 then -1*TSPL_JOURNAL_DETAILS_WIN.Amount else 0 end as CrInd" + Environment.NewLine + _
",Amount,OP_TYPE" + Environment.NewLine + _
"from TSPL_JOURNAL_DETAILS_WIN" + Environment.NewLine + _
"left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS_WIN.Voucher_No" + Environment.NewLine + _
"where TSPL_JOURNAL_MASTER.Authorized='A' or TSPL_JOURNAL_DETAILS_WIN.Post_To_Unpost='Y'" + Environment.NewLine + _
")xx group by Account_code,voucher_Date" + Environment.NewLine + _
")XXX on XXX.voucher_Date=TSPL_JOURNAL_DETAILS_DL.B_Date and XXX.Account_code=TSPL_JOURNAL_DETAILS_DL.Account_Code" + Environment.NewLine + _
"when Not MATCHED THEN" + Environment.NewLine + _
"insert(B_Date, Account_code, Dr_Amount, Cr_Amount, Dr_Adjustment, Cr_Adjustment, Dr_Closing, Cr_Closing, Dr_IND, Cr_IND)" + Environment.NewLine + _
"values(xxx.voucher_Date, xxx.Account_code, xxx.DrAmount, xxx.CrAmount, xxx.DrAdjustment, xxx.CrAdjustment, xxx.DrClosing, xxx.CrClosing, xxx.DrIND, xxx.CrIND)" + Environment.NewLine + _
"when matched then" + Environment.NewLine + _
"update set Dr_Amount=Dr_Amount+xxx.DrAmount,Cr_Amount=Cr_Amount+xxx.CrAmount,Dr_Adjustment=Dr_Adjustment+xxx.DrAdjustment,Cr_Adjustment=Cr_Adjustment+xxx.CrAdjustment,Dr_Closing=Dr_Closing+xxx.DrClosing,Cr_Closing=Cr_Closing+xxx.CrClosing,Dr_IND=Dr_IND+xxx.DrIND,Cr_IND=Cr_IND+xxx.CrIND ;" + Environment.NewLine + _
    "--------------Step 2-------------" + Environment.NewLine + _
" MERGE INTO TSPL_JOURNAL_DETAILS_DL_SC " + Environment.NewLine + _
"using( " + Environment.NewLine + _
"select voucher_Date,Account_code,Source_Code" + Environment.NewLine + _
",sum(DrAmount * case when OP_TYPE='D' then -1 else 1 end) as DrAmount" + Environment.NewLine + _
",sum(CrAmount * case when OP_TYPE='D' then -1 else 1 end) as CrAmount" + Environment.NewLine + _
",sum(DrAdjustment * case when OP_TYPE='D' then -1 else 1 end) DrAdjustment" + Environment.NewLine + _
",sum(CrAdjustment * case when OP_TYPE='D' then -1 else 1 end) as CrAdjustment" + Environment.NewLine + _
",sum(DrClosing * case when OP_TYPE='D' then -1 else 1 end) as DrClosing" + Environment.NewLine + _
",sum(CrClosing * case when OP_TYPE='D' then -1 else 1 end) as CrClosing" + Environment.NewLine + _
",sum(DrInd * case when OP_TYPE='D' then -1 else 1 end) as DrInd" + Environment.NewLine + _
",sum(CrInd * case when OP_TYPE='D' then -1 else 1 end) as CrInd " + Environment.NewLine + _
"from (" + Environment.NewLine + _
"select TSPL_JOURNAL_DETAILS_WIN.Voucher_No,CONVERT(date,TSPL_JOURNAL_DETAILS_WIN.Voucher_Date,103) as Voucher_Date,Account_code" + Environment.NewLine + _
",case when TSPL_JOURNAL_DETAILS_WIN.Amount>0 then TSPL_JOURNAL_DETAILS_WIN.Amount else 0 end as DrAmount" + Environment.NewLine + _
",case when TSPL_JOURNAL_DETAILS_WIN.Amount<0 then -1*TSPL_JOURNAL_DETAILS_WIN.Amount else 0 end as CrAmount" + Environment.NewLine + _
",case when TSPL_JOURNAL_MASTER.Transaction_Type='A' and TSPL_JOURNAL_DETAILS_WIN.Amount>0 then TSPL_JOURNAL_DETAILS_WIN.Amount else 0 end as DrAdjustment" + Environment.NewLine + _
",case when TSPL_JOURNAL_MASTER.Transaction_Type='A' and TSPL_JOURNAL_DETAILS_WIN.Amount<0 then -1*TSPL_JOURNAL_DETAILS_WIN.Amount else 0 end as CrAdjustment" + Environment.NewLine + _
",case when TSPL_JOURNAL_MASTER.Transaction_Type='X' and TSPL_JOURNAL_DETAILS_WIN.Amount>0 then TSPL_JOURNAL_DETAILS_WIN.Amount else 0 end as DrClosing" + Environment.NewLine + _
",case when TSPL_JOURNAL_MASTER.Transaction_Type='X' and TSPL_JOURNAL_DETAILS_WIN.Amount<0 then -1*TSPL_JOURNAL_DETAILS_WIN.Amount else 0 end as CrClosing" + Environment.NewLine + _
",case when TSPL_JOURNAL_MASTER.ind_as = 1 and TSPL_JOURNAL_DETAILS_WIN.Amount>0 then TSPL_JOURNAL_DETAILS_WIN.Amount else 0 end as DrInd" + Environment.NewLine + _
",case when TSPL_JOURNAL_MASTER.ind_as = 1 and TSPL_JOURNAL_DETAILS_WIN.Amount<0 then -1*TSPL_JOURNAL_DETAILS_WIN.Amount else 0 end as CrInd" + Environment.NewLine + _
",Amount,OP_TYPE,TSPL_JOURNAL_MASTER.Source_Code" + Environment.NewLine + _
"from TSPL_JOURNAL_DETAILS_WIN" + Environment.NewLine + _
"left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS_WIN.Voucher_No" + Environment.NewLine + _
"where TSPL_JOURNAL_MASTER.Authorized='A' or TSPL_JOURNAL_DETAILS_WIN.Post_To_Unpost='Y'" + Environment.NewLine + _
")xx group by Account_code,Source_Code,voucher_Date" + Environment.NewLine + _
")XXX on XXX.voucher_Date=TSPL_JOURNAL_DETAILS_DL_SC.B_Date and XXX.Account_code=TSPL_JOURNAL_DETAILS_DL_SC.Account_Code and XXX.Source_Code=TSPL_JOURNAL_DETAILS_DL_SC.Source_Code" + Environment.NewLine + _
"when Not MATCHED THEN" + Environment.NewLine + _
"insert(B_Date, Account_code, Source_Code, Dr_Amount, Cr_Amount, Dr_Adjustment, Cr_Adjustment, Dr_Closing, Cr_Closing, Dr_IND, Cr_IND)" + Environment.NewLine + _
"values(xxx.voucher_Date, xxx.Account_code, xxx.Source_Code, xxx.DrAmount, xxx.CrAmount, xxx.DrAdjustment, xxx.CrAdjustment, xxx.DrClosing, xxx.CrClosing, xxx.DrIND, xxx.CrIND)" + Environment.NewLine + _
"when matched then" + Environment.NewLine + _
"update set Dr_Amount=Dr_Amount+xxx.DrAmount,Cr_Amount=Cr_Amount+xxx.CrAmount,Dr_Adjustment=Dr_Adjustment+xxx.DrAdjustment,Cr_Adjustment=Cr_Adjustment+xxx.CrAdjustment,Dr_Closing=Dr_Closing+xxx.DrClosing,Cr_Closing=Cr_Closing+xxx.CrClosing,Dr_IND=Dr_IND+xxx.DrIND,Cr_IND=Cr_IND+xxx.CrIND ;" + Environment.NewLine + _
"--------------Step 3-------------" + Environment.NewLine + _
"MERGE INTO TSPL_JOURNAL_DETAILS_DL " + Environment.NewLine + _
"using( " + Environment.NewLine + _
"select TSPL_JOURNAL_DETAILS_DL.B_Date,TSPL_JOURNAL_DETAILS_DL.Account_code,TSPL_JOURNAL_DETAILS_DL.Dr_Amount,TSPL_JOURNAL_DETAILS_DL.Cr_Amount," + Environment.NewLine + _
"sum(TSPL_JOURNAL_DETAILS_DL.Dr_Amount-TSPL_JOURNAL_DETAILS_DL.Cr_Amount) over (partition by TSPL_JOURNAL_DETAILS_DL.Account_code order by TSPL_JOURNAL_DETAILS_DL.Account_code,TSPL_JOURNAL_DETAILS_DL.B_Date) as CAL_CL_Amount " + Environment.NewLine + _
",TSPL_JOURNAL_DETAILS_DL.Dr_Adjustment,TSPL_JOURNAL_DETAILS_DL.Cr_Adjustment," + Environment.NewLine + _
"sum(TSPL_JOURNAL_DETAILS_DL.Dr_Adjustment-TSPL_JOURNAL_DETAILS_DL.Cr_Adjustment) over (partition by TSPL_JOURNAL_DETAILS_DL.Account_code order by TSPL_JOURNAL_DETAILS_DL.Account_code,TSPL_JOURNAL_DETAILS_DL.B_Date) as CAL_CL_Adjustment " + Environment.NewLine + _
",TSPL_JOURNAL_DETAILS_DL.Dr_Closing,TSPL_JOURNAL_DETAILS_DL.Cr_Closing," + Environment.NewLine + _
"sum(TSPL_JOURNAL_DETAILS_DL.Dr_Closing-TSPL_JOURNAL_DETAILS_DL.Cr_Closing) over (partition by TSPL_JOURNAL_DETAILS_DL.Account_code order by TSPL_JOURNAL_DETAILS_DL.Account_code,TSPL_JOURNAL_DETAILS_DL.B_Date) as CAL_CL_Closing " + Environment.NewLine + _
",TSPL_JOURNAL_DETAILS_DL.Dr_IND,TSPL_JOURNAL_DETAILS_DL.Cr_IND," + Environment.NewLine + _
"sum(TSPL_JOURNAL_DETAILS_DL.Dr_IND-TSPL_JOURNAL_DETAILS_DL.Cr_IND) over (partition by TSPL_JOURNAL_DETAILS_DL.Account_code order by TSPL_JOURNAL_DETAILS_DL.Account_code,TSPL_JOURNAL_DETAILS_DL.B_Date) as CAL_CL_Ind" + Environment.NewLine + _
"from TSPL_JOURNAL_DETAILS_DL" + Environment.NewLine + _
"inner join  ( select distinct account_code from TSPL_JOURNAL_DETAILS_WIN ) xx on xx.Account_code=TSPL_JOURNAL_DETAILS_DL.Account_code" + Environment.NewLine + _
")XXX " + Environment.NewLine + _
"on XXX.B_Date=TSPL_JOURNAL_DETAILS_DL.B_Date and XXX.Account_code=TSPL_JOURNAL_DETAILS_DL.Account_Code" + Environment.NewLine + _
"when matched then" + Environment.NewLine + _
"update set CL_Amount=xxx.CAL_CL_Amount,CL_Adjustment=xxx.CAL_CL_Adjustment,CL_Closing=xxx.CAL_CL_Closing,CL_IND=xxx.CAL_CL_Ind;" + Environment.NewLine + _
"--------------Step 4-------------" + Environment.NewLine + _
"MERGE INTO TSPL_JOURNAL_DETAILS_DL_SC " + Environment.NewLine + _
"using( " + Environment.NewLine + _
"select TSPL_JOURNAL_DETAILS_DL_SC.B_Date,TSPL_JOURNAL_DETAILS_DL_SC.Account_code,TSPL_JOURNAL_DETAILS_DL_SC.Source_Code,TSPL_JOURNAL_DETAILS_DL_SC.Dr_Amount,TSPL_JOURNAL_DETAILS_DL_SC.Cr_Amount," + Environment.NewLine + _
"sum(TSPL_JOURNAL_DETAILS_DL_SC.Dr_Amount-TSPL_JOURNAL_DETAILS_DL_SC.Cr_Amount) over (partition by TSPL_JOURNAL_DETAILS_DL_SC.Account_code,Source_Code order by TSPL_JOURNAL_DETAILS_DL_SC.Account_code,TSPL_JOURNAL_DETAILS_DL_SC.B_Date) as CAL_CL_Amount " + Environment.NewLine + _
",TSPL_JOURNAL_DETAILS_DL_SC.Dr_Adjustment,TSPL_JOURNAL_DETAILS_DL_SC.Cr_Adjustment," + Environment.NewLine + _
"sum(TSPL_JOURNAL_DETAILS_DL_SC.Dr_Adjustment-TSPL_JOURNAL_DETAILS_DL_SC.Cr_Adjustment) over (partition by TSPL_JOURNAL_DETAILS_DL_SC.Account_code,Source_Code order by TSPL_JOURNAL_DETAILS_DL_SC.Account_code,TSPL_JOURNAL_DETAILS_DL_SC.B_Date) as CAL_CL_Adjustment " + Environment.NewLine + _
",TSPL_JOURNAL_DETAILS_DL_SC.Dr_Closing,TSPL_JOURNAL_DETAILS_DL_SC.Cr_Closing," + Environment.NewLine + _
"sum(TSPL_JOURNAL_DETAILS_DL_SC.Dr_Closing-TSPL_JOURNAL_DETAILS_DL_SC.Cr_Closing) over (partition by TSPL_JOURNAL_DETAILS_DL_SC.Account_code,Source_Code order by TSPL_JOURNAL_DETAILS_DL_SC.Account_code,TSPL_JOURNAL_DETAILS_DL_SC.B_Date) as CAL_CL_Closing " + Environment.NewLine + _
",TSPL_JOURNAL_DETAILS_DL_SC.Dr_IND,TSPL_JOURNAL_DETAILS_DL_SC.Cr_IND," + Environment.NewLine + _
"sum(TSPL_JOURNAL_DETAILS_DL_SC.Dr_IND-TSPL_JOURNAL_DETAILS_DL_SC.Cr_IND) over (partition by TSPL_JOURNAL_DETAILS_DL_SC.Account_code,Source_Code order by TSPL_JOURNAL_DETAILS_DL_SC.Account_code,TSPL_JOURNAL_DETAILS_DL_SC.B_Date) as CAL_CL_Ind" + Environment.NewLine + _
"from TSPL_JOURNAL_DETAILS_DL_SC" + Environment.NewLine + _
"inner join  ( select distinct account_code from TSPL_JOURNAL_DETAILS_WIN ) xx on xx.Account_code=TSPL_JOURNAL_DETAILS_DL_SC.Account_code" + Environment.NewLine + _
")XXX " + Environment.NewLine + _
"on XXX.B_Date=TSPL_JOURNAL_DETAILS_DL_SC.B_Date and XXX.Account_code=TSPL_JOURNAL_DETAILS_DL_SC.Account_Code and XXX.Source_Code=TSPL_JOURNAL_DETAILS_DL_SC.Source_Code" + Environment.NewLine + _
"when matched then" + Environment.NewLine + _
"update set CL_Amount=xxx.CAL_CL_Amount,CL_Adjustment=xxx.CAL_CL_Adjustment,CL_Closing=xxx.CAL_CL_Closing,CL_IND=xxx.CAL_CL_Ind;" + Environment.NewLine + _
"-------Step 5----------------" + Environment.NewLine + _
"delete from TSPL_JOURNAL_DETAILS_WIN where TSPL_JOURNAL_DETAILS_WIN.Post_To_Unpost='Y' or exists (select 1 from TSPL_JOURNAL_MASTER where TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS_WIN.Voucher_No and TSPL_JOURNAL_MASTER.Authorized='A') " + Environment.NewLine + _
"lbexit:" + Environment.NewLine + _
                 "IF @trancount = 0" + Environment.NewLine + _
                 "COMMIT;" + Environment.NewLine + _
                 "END TRY" + Environment.NewLine + _
                 "BEGIN CATCH" + Environment.NewLine + _
                 "  DECLARE @error int," + Environment.NewLine + _
                 "          @message varchar(4000)," + Environment.NewLine + _
                 "          @xstate int;" + Environment.NewLine + _
                 "    SELECT" + Environment.NewLine + _
                 "      @error = ERROR_NUMBER()," + Environment.NewLine + _
                 "      @message = ERROR_MESSAGE()," + Environment.NewLine + _
                 "      @xstate = XACT_STATE();" + Environment.NewLine + _
                 "    IF @xstate = -1" + Environment.NewLine + _
                 "      ROLLBACK;" + Environment.NewLine + _
                 "IF @xstate = 1 AND @trancount = 0" + Environment.NewLine + _
                 "        ROLLBACK;" + Environment.NewLine + _
                 "IF @xstate = 1 AND @trancount > 0" + Environment.NewLine + _
                 "  ROLLBACK TRANSACTION SP_TSPL_JOURNAL_DETAILS_DL;" + Environment.NewLine + _
                 "RAISERROR ('SP_TSPL_JOURNAL_DETAILS_DL: %d: %s', 16, 1, @error, @message);" + Environment.NewLine + _
                 "END CATCH" + Environment.NewLine + _
                 "End"
            clsCommonFunctionality.CreateStoreProcedure("SP_TSPL_JOURNAL_DETAILS_DL", qry)

            'Create Trigger for Journal detail table
            Dim strColumnName As String = clsERPFuncationality.GetTableColumnNameForQry("TSPL_JOURNAL_DETAILS", Nothing)
            qry = ""
            If clsPostCreateTable.CheckTriggerExits("TRG_TSPL_JOURNAL_DETAILS", Nothing) = 0 Then
                qry = "Create "
            Else
                qry = "Alter "
            End If
            qry = "" & qry & " trigger [dbo].[TRG_TSPL_JOURNAL_DETAILS] on [dbo].[TSPL_JOURNAL_DETAILS] AFTER INSERT,UPDATE, DELETE as  " + Environment.NewLine + _
                " declare @Sett varchar(1);" + Environment.NewLine + _
                " select @Sett=Description from TSPL_FIXED_PARAMETER where Type='" + clsFixedParameterType.TriggerOfGLEntryForWinTable + "' and Code ='" + clsFixedParameterCode.TriggerOfGLEntryForWinTable + "';  if coalesce(@Sett,'')<>'1'  return;" + Environment.NewLine + _
                " INSERT into TSPL_JOURNAL_DETAILS_WIN( " + strColumnName + ",OP_TYPE,Post_To_Unpost) " + Environment.NewLine + _
                " select " + strColumnName + ",'D' as OP_TYPE,'N' as Post_To_Unpost from deleted " + Environment.NewLine + _
                " INSERT into TSPL_JOURNAL_DETAILS_WIN(" + strColumnName + ",OP_TYPE,Post_To_Unpost) " + Environment.NewLine + _
                " select " + strColumnName + ",'I' as OP_TYPE,'N' as Post_To_Unpost from inserted;"
            clsDBFuncationality.ExecuteNonQuery(qry)

            'Create Trigger for Journal master table
            qry = ""
            If clsPostCreateTable.CheckTriggerExits("TRG_TSPL_JOURNAL_MASTER", Nothing) = 0 Then
                qry = "Create "
            Else
                qry = "Alter "
            End If
            qry += " trigger [dbo].[TRG_TSPL_JOURNAL_MASTER] on [dbo].[TSPL_JOURNAL_MASTER] AFTER INSERT,UPDATE, DELETE as   " + Environment.NewLine + _
            "declare @Sett varchar(1);  " + Environment.NewLine + _
            "select @Sett=Description from TSPL_FIXED_PARAMETER where Type='Trigger Of GL Entry For Win Table' and Code ='Trigger Of GL Entry For Win Table';  if coalesce(@Sett,'')<>'1'  return;  " + Environment.NewLine + _
            "INSERT into TSPL_JOURNAL_DETAILS_WIN( Journal_No,Voucher_No,Voucher_Date,Detail_Line_No,Account_code,Account_Desc,Amount,Description,Reference,Posting_Date,Account_Type,Account_Group_Code,Account_Seg_Code1,Account_Seg_Desc1,Account_Seg_Code2,Account_Seg_Desc2,Account_Seg_Code3,Account_Seg_Desc3,Account_Seg_Code4,Account_Seg_Desc4,Account_Seg_Code5,Account_Seg_Desc5,Account_Seg_Code6,Account_Seg_Desc6,Account_Seg_Code7,Account_Seg_Desc7,Account_Seg_Code8,Account_Seg_Desc8,Account_Seg_Code9,Account_Seg_Desc9,Account_Seg_Code10,Account_Seg_Desc10,CustVend_Code,CustVend_Name,PK_Id,Hirerachy_Code,Cost_Centre_Code,OP_TYPE,Post_To_Unpost)  " + Environment.NewLine + _
            "select xxxx.* from (  " + Environment.NewLine + _
            "select Journal_No ,Voucher_No,Voucher_Date,Detail_Line_No,Account_code,Account_Desc,Amount,Description,Reference,Posting_Date,Account_Type,Account_Group_Code,Account_Seg_Code1,Account_Seg_Desc1,Account_Seg_Code2,Account_Seg_Desc2,Account_Seg_Code3,Account_Seg_Desc3,Account_Seg_Code4,Account_Seg_Desc4,Account_Seg_Code5,Account_Seg_Desc5,Account_Seg_Code6,Account_Seg_Desc6,Account_Seg_Code7,Account_Seg_Desc7,Account_Seg_Code8,Account_Seg_Desc8,Account_Seg_Code9,Account_Seg_Desc9,Account_Seg_Code10,Account_Seg_Desc10,CustVend_Code,CustVend_Name,PK_Id,Hirerachy_Code,Cost_Centre_Code,'D' as OP_TYPE ,'Y' as Post_To_Unpost from TSPL_JOURNAL_DETAILS  " + Environment.NewLine + _
            "union all " + Environment.NewLine + _
            "select Journal_No ,Voucher_No,Voucher_Date,Detail_Line_No,Account_code,Account_Desc,Amount,Description,Reference,Posting_Date,Account_Type,Account_Group_Code,Account_Seg_Code1,Account_Seg_Desc1,Account_Seg_Code2,Account_Seg_Desc2,Account_Seg_Code3,Account_Seg_Desc3,Account_Seg_Code4,Account_Seg_Desc4,Account_Seg_Code5,Account_Seg_Desc5,Account_Seg_Code6,Account_Seg_Desc6,Account_Seg_Code7,Account_Seg_Desc7,Account_Seg_Code8,Account_Seg_Desc8,Account_Seg_Code9,Account_Seg_Desc9,Account_Seg_Code10,Account_Seg_Desc10,CustVend_Code,CustVend_Name,PK_Id,Hirerachy_Code,Cost_Centre_Code,'I' as OP_TYPE ,'N' as Post_To_Unpost " + Environment.NewLine + _
            "from TSPL_JOURNAL_DETAILS  " + Environment.NewLine + _
            ")xxxx " + Environment.NewLine + _
            "inner join (   " + Environment.NewLine + _
            "select Voucher_No as InnerVoucher_No from ( " + Environment.NewLine + _
            "select Voucher_No,max(case when Operation_Type='D' then Authorized else null end) as DeleteAuthorized,max(case when Operation_Type='I' then Authorized else null end) as InsertAuthorized from ( " + Environment.NewLine + _
            "select Voucher_No,Authorized,'D' as Operation_Type  from deleted " + Environment.NewLine + _
            "union all " + Environment.NewLine + _
            "select Voucher_No,Authorized,'I' as Operation_Type from inserted " + Environment.NewLine + _
            ")x group by Voucher_No " + Environment.NewLine + _
            ")xx where DeleteAuthorized='A' and InsertAuthorized<>'A' " + Environment.NewLine + _
            ")xxx on  xxxx.Voucher_No =xxx.InnerVoucher_No"
            clsDBFuncationality.ExecuteNonQuery(qry)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtVSP__My_Click(sender As Object, e As EventArgs) Handles txtGLAC._My_Click
        Dim qry As String = "select Account_Code,Description  from TSPL_GL_ACCOUNTS"
        txtGLAC.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemUtil", qry, "Account_Code", "Description", txtGLAC.arrValueMember, Nothing)
    End Sub

    Private Sub btnInvSummaryUpdate_Click(sender As Object, e As EventArgs) Handles btnInvSummaryUpdate.Click
        clsCommon.ProgressBarShow()
        Dim strColumnName As String = clsERPFuncationality.GetTableColumnNameForQry("TSPL_JOURNAL_DETAILS", Nothing)
        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If txtGLAC.arrValueMember IsNot Nothing AndAlso txtGLAC.arrValueMember.Count > 0 Then
                Dim qry As String = "delete from TSPL_JOURNAL_DETAILS_WIN where Account_code in (" + clsCommon.GetMulcallString(txtGLAC.arrValueMember) + ")"
                clsDBFuncationality.ExecuteNonQuery(qry, tran)

                qry = "delete from TSPL_JOURNAL_DETAILS_DL where Account_code in (" + clsCommon.GetMulcallString(txtGLAC.arrValueMember) + ")"
                clsDBFuncationality.ExecuteNonQuery(qry, tran)

                qry = "delete from TSPL_JOURNAL_DETAILS_DL_SC where Account_code in (" + clsCommon.GetMulcallString(txtGLAC.arrValueMember) + ")"
                clsDBFuncationality.ExecuteNonQuery(qry, tran)

                qry = " INSERT into TSPL_JOURNAL_DETAILS_WIN(" + strColumnName + ",OP_TYPE)  select  " + strColumnName + ",'I' as OP_TYPE from TSPL_JOURNAL_DETAILS where Account_code in (" + clsCommon.GetMulcallString(txtGLAC.arrValueMember) + ")"
                clsDBFuncationality.ExecuteNonQuery(qry, tran)

                qry = "execute SP_TSPL_JOURNAL_DETAILS_DL"
                clsDBFuncationality.ExecuteNonQuery(qry, tran)

                tran.Commit()
            End If
            clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(Me, "Task Completed", Me.Text)
        Catch ex As Exception
            tran.Rollback()
            clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub



    Private Sub mItmExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mItmExport.Click
        funExport()
    End Sub

    Public Sub funExport()
        Try
            Dim strCmd As String = "select  M.Voucher_No as [Voucher No.],Detail_Line_No [Detail Line No] , M.Voucher_Desc as [Voucher Description] , M.Voucher_Date as [Voucher Date],L.Account_code as [Account Code],L.Account_Desc as [Account Name],case when L.Amount>=0 then L.Amount else 0 end as [Debit],case when L.Amount<0 then L.Amount*-1 else 0 end as [Credit],L.Description as [Description],L.Hirerachy_Code as [Hirerachy Code] ,L.Cost_Centre_Code as [Cost Center]  from TSPL_JOURNAL_MASTER M inner join TSPL_JOURNAL_DETAILS L on M.Voucher_No =L.Voucher_No "
            Dim Whrcls As String = " [Voucher No.] ,[Detail Line No] "
            transportSql.ExporttoExcel(strCmd, "", Whrcls, Me)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Journal Entry")
        End Try
    End Sub

    Private Sub btnExportblank_Click(sender As Object, e As EventArgs) Handles btnExportblank.Click
        Try
            Dim strCmd As String = "select '' as [Voucher No], '' as [Voucher Description] , '' as [Voucher Date],'' as [Account Code],0 as [Debit],0 as [Credit],'' as [Description] "
            If SettingCostCenter Then
                strCmd += ",'' as [Hirerachy Code],'' as [Cost Center] "
            End If

            Dim Whrcls As String = ""
            transportSql.ExporttoExcel(strCmd, "", Whrcls, Me)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Journal Entry")
        End Try
    End Sub

    Private Sub RadMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mItmImport.Click
        FunImport(True)
    End Sub

    Private Sub RadMenuItem4_Click_1(sender As Object, e As EventArgs) Handles RadMenuItem4.Click
        FunImport(False)
    End Sub

    Public Sub FunImportOLD(ByVal IsClosingEntry As Boolean)
        Dim EntryDesc As String = ""
        Dim EntryDate As Date = Nothing
        Dim AccOuntCode, Accountname As String
        Dim DrAmt, TotDrAmt, Amt As Decimal
        Dim CrAmt, TotCrAmt As Decimal
        Dim DetailDesc As String = ""
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        Dim LineNumber As Integer = 1
        Dim AccCode As String = String.Empty
        Dim AccLocCode As String = String.Empty
        Dim STRACCOUNT As String = String.Empty
        Dim HirerachyCode As String = ""
        Dim CostCenter As String = ""
        Dim flag As Boolean = False
        If SettingCostCenter Then
            flag = transportSql.importExcel(gv, "Voucher Description", "Voucher Date", "Account Code", "Debit", "Credit", "Description", "Hirerachy Code", "Cost Center")
        Else
            flag = transportSql.importExcel(gv, "Voucher Description", "Voucher Date", "Account Code", "Debit", "Credit", "Description")
        End If

        If flag Then
            Dim trans As SqlTransaction = Nothing
            Try
                'connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    EntryDesc = clsCommon.myCstr(grow.Cells("Voucher Description").Value)
                    EntryDate = clsCommon.GetPrintDate(grow.Cells("Voucher Date").Value, "dd/MMM/yyyy")
                    TotDrAmt = TotDrAmt + clsCommon.myCdbl(grow.Cells("Debit").Value)
                    TotCrAmt = TotDrAmt + clsCommon.myCdbl(grow.Cells("Credit").Value)
                    AccCode = clsCommon.myCstr(grow.Cells("Account Code").Value)
                    Dim qry As String = "select TSPL_GL_ACCOUNTS.Account_Code from TSPL_GL_ACCOUNTS WHERE TSPL_GL_ACCOUNTS.Account_Code='" & AccCode & "' "
                    STRACCOUNT = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                    If clsCommon.myLen(STRACCOUNT) <= 0 Then
                        Throw New Exception("Invalid Account code.")
                    End If
                    If clsCommon.GetDateWithStartTime(clsCommon.myCDate(EntryDate)) >= clsCommon.GetDateWithStartTime(ERPStartDate) Then
                        'richa 17 SEp,2019 TEC/03/07/19-000927
                        qry = "select TSPL_GL_ACCOUNTS.Account_Code from TSPL_GL_ACCOUNTS WHERE TSPL_GL_ACCOUNTS.Account_Code='" & AccCode & "'  and ( ControlAccount='N' or TSPL_GL_ACCOUNTS.Account_Code IN  (select TSPL_CONTROL_ACC_MAPPING.Account_Code  from TSPL_CONTROL_ACC_MAPPING where IsForJE  =1)) "
                        STRACCOUNT = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                        If clsCommon.myLen(STRACCOUNT) <= 0 Then
                            Throw New Exception("Account should be type of non Control Account.")
                        End If
                    End If
                    If SettingCostCenter Then
                        HirerachyCode = clsCommon.myCstr(grow.Cells("Hirerachy Code").Value)
                        CostCenter = clsCommon.myCstr(grow.Cells("Cost Center").Value)
                        Dim grouptype As String = ""
                        grouptype = clsPaymentHeader.CheckGLAccountType(clsCommon.myCstr(STRACCOUNT), trans)
                        If Not clsCommon.CompairString(grouptype, "Balance Sheet") = CompairStringResult.Equal Then
                            If clsCommon.myLen(HirerachyCode) <= 0 Then
                                Throw New Exception("Please provide the Hirerachy Level " + clsCommon.myCstr(LineNumber))
                            ElseIf clsCommon.myLen(HirerachyCode) > 0 Then
                                Dim STRHirerachyCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_HIRERACHY_LEVEL_MASTER.HIRERACHY_CODE From TSPL_HIRERACHY_LEVEL_MASTER   where HIRERACHY_CODE='" & HirerachyCode & "'", trans))
                                If clsCommon.myLen(STRHirerachyCode) <= 0 Then
                                    Throw New Exception("Hirerachy code not exist in master.")
                                End If
                            End If
                            If clsCommon.myLen(CostCenter) <= 0 Then
                                Throw New Exception("Please provide the Cost Center " + clsCommon.myCstr(LineNumber))
                            ElseIf clsCommon.myLen(CostCenter) > 0 Then
                                Dim STRCostCenter = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cost_Center_Fin_Code from TSPL_COST_CENTRE_FINANCIAL WHERE TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code='" & CostCenter & "'", trans))
                                If clsCommon.myLen(STRCostCenter) <= 0 Then
                                    Throw New Exception("Cost Center code not exist in master.")
                                End If
                                Dim DBLevel = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ISNULL(Level,'') AS Level from TSPL_HIRERACHY_LEVEL_MASTER WHERE HIRERACHY_CODE='" & HirerachyCode & "'", trans))
                                STRCostCenter = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cost_Center_Fin_Code from TSPL_COST_CENTRE_FINANCIAL WHERE TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code='" & CostCenter & "' and Hirerachy_Level='" & DBLevel & "'", trans))
                                If clsCommon.myLen(STRCostCenter) <= 0 Then
                                    Throw New Exception("Cost Center code not mapped with Hirerachy.")
                                End If
                            End If
                        End If
                    End If
                    LineNumber += 1
                Next
                If clsCommon.myLen(AccCode) > 0 Then
                    AccLocCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Account_Seg_Code7 ,'') AS Account_Seg_Code7 FROM TSPL_GL_ACCOUNTS WHERE Account_Code ='" & AccCode & "'", trans))
                Else
                    AccLocCode = ""
                End If

                Dim StrVoucher As String = fnAutoGenerateNo(trans, EntryDate, True, AccLocCode, True) ''Location pending
                Dim strJrnl As String = "select (case when max(journal_no) is not null then max(journal_no) else 0 end) from TSPL_JOURNAL_MASTER "
                Dim Jrnl As String = CInt(connectSql.RunScalar(trans, strJrnl)) + 1

                connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_MASTER_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", EntryDate), New SqlParameter("@Source_Code", "GL-JE"), New SqlParameter("@Source_Desc", "Journal Entry"), New SqlParameter("@Source_Doc_No", ""), New SqlParameter("@Source_Doc_Date", EntryDate), New SqlParameter("@Posting_Date", EntryDate), New SqlParameter("@Voucher_Desc", EntryDesc), New SqlParameter("@Source_Narration", ""), New SqlParameter("@Remarks", ""), New SqlParameter("@Comments", ""), New SqlParameter("@Auto_Reverse", "N"), New SqlParameter("@Reverse_Date", EntryDate), New SqlParameter("@Source_Type", "O"), New SqlParameter("@CustVend_Code", ""), New SqlParameter("@CustVend_Name", ""), New SqlParameter("@Transaction_Type", IIf(IsClosingEntry, "X", "")), New SqlParameter("@Total_Debit_Amt", TotDrAmt), New SqlParameter("@Total_Credit_Amt", TotCrAmt), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", companyCode))



                Dim strJrnl1 As String = "select journal_no from TSPL_JOURNAL_MASTER where Voucher_No='" + StrVoucher + "'"
                Dim Jrnl1 As String
                Jrnl1 = connectSql.RunScalar(trans, strJrnl1)
                Dim LogSeg As String = ""

                Dim i As Integer = 1
                For Each grow As GridViewRowInfo In gv.Rows
                    AccOuntCode = clsCommon.myCstr(grow.Cells("Account Code").Value)
                    Dim strAccChk As String = connectSql.RunScalar(trans, "select Account_Code from TSPL_GL_ACCOUNTS where Account_Code ='" + AccOuntCode + "'")
                    If clsCommon.myLen(strAccChk) > 0 Then
                        If clsCommon.myLen(LogSeg) <= 0 Then
                            LogSeg = clsDBFuncationality.getSingleValue("select Account_Seg_Code7  from TSPL_GL_ACCOUNTS where Account_Code='" + AccOuntCode + "'", trans)
                        End If
                        If SettingCostCenter Then
                            HirerachyCode = clsCommon.myCstr(grow.Cells("Hirerachy Code").Value)
                            CostCenter = clsCommon.myCstr(grow.Cells("Cost Center").Value)
                        End If
                        
                        Dim LineNo As Integer = i
                        Accountname = connectSql.RunScalar(trans, "select Description from TSPL_GL_ACCOUNTS where Account_Code ='" + AccOuntCode + "'")
                        DrAmt = clsCommon.myCdbl(grow.Cells("Debit").Value)
                        CrAmt = clsCommon.myCdbl(grow.Cells("Credit").Value)
                        If DrAmt = 0 Then
                            Amt = CrAmt * -1
                        ElseIf CrAmt = 0 Then
                            Amt = DrAmt
                        End If
                        DetailDesc = clsCommon.myCstr(grow.Cells("Description").Value)

                        Dim strQ1 As String = " SELECT     Account_Type, Account_Group_Code, Account_Seg_Code1, Account_Seg_Desc1, Account_Seg_Code2, Account_Seg_Desc2, Account_Seg_Code3, " & _
                             " Account_Seg_Desc3, Account_Seg_Code4, Account_Seg_Desc4, Account_Seg_Code5, Account_Seg_Desc5, Account_Seg_Code6, Account_Seg_Desc6," & _
                             " Account_Seg_Code7, Account_Seg_Desc7, Account_Seg_Code8, Account_Seg_Desc8, Account_Seg_Code9, Account_Seg_Desc9, " & _
                             " Account_Seg_Code10, Account_Seg_Desc10 FROM  TSPL_GL_ACCOUNTS where Account_Code='" + AccOuntCode + "'"
                        '' Added By abhishek as on 12/10/2012
                        myreader = clsDBFuncationality.GetDataTable(strQ1, trans)
                        If (myreader IsNot Nothing AndAlso myreader.Rows.Count > 0) Then

                            For Each dr As DataRow In myreader.Rows


                                Dim AccType As String = dr(0).ToString()
                                Dim AccGrp As String = dr(1).ToString()

                                Dim SegC1 As String = dr(2).ToString()
                                Dim SegDesc1 As String = dr(3).ToString()

                                Dim SegC2 As String = dr(4).ToString()
                                Dim SegDesc2 As String = dr(5).ToString()

                                Dim SegC3 As String = dr(6).ToString()
                                Dim SegDesc3 As String = dr(7).ToString()

                                Dim SegC4 As String = dr(8).ToString()
                                Dim SegDesc4 As String = dr(9).ToString()

                                Dim SegC5 As String = dr(10).ToString()
                                Dim SegDesc5 As String = dr(11).ToString()

                                Dim SegC6 As String = dr(12).ToString()
                                Dim SegDesc6 As String = dr(13).ToString()

                                Dim SegC7 As String = dr(14).ToString()
                                Dim SegDesc7 As String = dr(15).ToString()

                                Dim SegC8 As String = dr(16).ToString()
                                Dim SegDesc8 As String = dr(17).ToString()

                                Dim SegC9 As String = dr(18).ToString()
                                Dim SegDesc9 As String = dr(19).ToString()

                                Dim SegC10 As String = dr(20).ToString()
                                Dim SegDesc10 As String = dr(21).ToString()


                                connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl1), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", EntryDate), New SqlParameter("@Detail_Line_No", LineNo), New SqlParameter("@Account_code", AccOuntCode), New SqlParameter("@Account_Desc", Accountname), New SqlParameter("@Amount", Amt), New SqlParameter("@Description", DetailDesc), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", EntryDate), New SqlParameter("@Account_Type", AccType), New SqlParameter("@Account_Group_Code", AccGrp), New SqlParameter("@Account_Seg_Code1", SegC1), New SqlParameter("@Account_Seg_Desc1", SegDesc1), New SqlParameter("@Account_Seg_Code2", SegC2), New SqlParameter("@Account_Seg_Desc2", SegDesc2), New SqlParameter("@Account_Seg_Code3", SegC3), New SqlParameter("@Account_Seg_Desc3", SegDesc3), New SqlParameter("@Account_Seg_Code4", SegC4), New SqlParameter("@Account_Seg_Desc4", SegDesc4), New SqlParameter("@Account_Seg_Code5", SegC5), New SqlParameter("@Account_Seg_Desc5", SegDesc5), New SqlParameter("@Account_Seg_Code6", SegC6), New SqlParameter("@Account_Seg_Desc6", SegDesc6), New SqlParameter("@Account_Seg_Code7", SegC7), New SqlParameter("@Account_Seg_Desc7", SegDesc7), New SqlParameter("@Account_Seg_Code8", SegC8), New SqlParameter("@Account_Seg_Desc8", SegDesc8), New SqlParameter("@Account_Seg_Code9", SegC9), New SqlParameter("@Account_Seg_Desc9", SegDesc9), New SqlParameter("@Account_Seg_Code10", SegC10), New SqlParameter("@Account_Seg_Desc10", SegDesc10))
                                Dim coll As New Hashtable()
                                clsCommon.AddColumnsForChange(coll, "Hirerachy_Code", HirerachyCode, True)
                                clsCommon.AddColumnsForChange(coll, "Cost_Centre_Code", CostCenter, True)
                                clsCommonFunctionality.UpdateDataTable(coll, "tspl_journal_details", OMInsertOrUpdate.Update, "tspl_journal_details.Journal_No='" + clsCommon.myCstr(Jrnl) + "' and tspl_journal_details.Voucher_No='" + clsCommon.myCstr(StrVoucher) + "' and account_code='" + clsCommon.myCstr(AccOuntCode) + "'", trans)

                            Next
                        End If
                    Else
                        common.clsCommon.MyMessageBoxShow(Me, "Account Code '" + AccOuntCode + "' Not Found!", Me.Text, MessageBoxButtons.OK, RadMessageIcon.Info)
                        trans.Rollback()
                        clsCommon.ProgressBarHide()
                        'connectSql.CloseConnection(connectSql.Connection())
                        Me.Controls.Remove(gv)
                        Exit Sub
                    End If
                    i = +1
                Next
                clsDBFuncationality.ExecuteNonQuery("update TSPL_JOURNAL_MASTER set Segment_code='" + LogSeg + "' where Voucher_No ='" + StrVoucher + "'", trans)

                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                ' End If
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                'connectSql.CloseConnection(connectSql.Connection())
                clsCommon.MyMessageBoxShow(Me, "Line No " + clsCommon.myCstr(LineNumber) + " : " + ex.Message)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub
    ''GKD/04/06/18-000143 by balwinder 
    Public Sub FunImport(ByVal IsClosingEntry As Boolean)
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim flag As Boolean = False
        If SettingCostCenter Then
            flag = transportSql.importExcel(gv, "Voucher No", "Voucher Description", "Voucher Date", "Account Code", "Debit", "Credit", "Description", "Hirerachy Code", "Cost Center")
        Else
            flag = transportSql.importExcel(gv, "Voucher No", "Voucher Description", "Voucher Date", "Account Code", "Debit", "Credit", "Description")
        End If

        If flag Then
            Dim trans As SqlTransaction = Nothing
            Dim arrDate As New List(Of String)
            Dim arrLocSegment As New List(Of String)
            For Each grow As GridViewRowInfo In gv.Rows
                If clsCommon.myLen(grow.Cells("Voucher Date").Value) > 0 Then
                    Dim EntryDateAndVNo As String = clsCommon.GetPrintDate(grow.Cells("Voucher Date").Value, "dd/MMM/yyyy") + "#" + clsCommon.myCstr(grow.Cells("Voucher No").Value)
                    If Not arrDate.Contains(EntryDateAndVNo) Then
                        arrDate.Add(EntryDateAndVNo)
                    End If
                End If
            Next
            For Each grow As GridViewRowInfo In gv.Rows
                Dim StrLocationSegment As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Account_Seg_Code7 ,'') AS Account_Seg_Code7 FROM TSPL_GL_ACCOUNTS WHERE Account_Code ='" & clsCommon.myCstr(grow.Cells("Account Code").Value) & "'"))
                If Not arrLocSegment.Contains(StrLocationSegment) Then
                    arrLocSegment.Add(StrLocationSegment)
                End If
            Next
            Dim LineNumber As Integer = 1
            Try
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For ii As Integer = 0 To arrDate.Count - 1
                    For jj As Integer = 0 To arrLocSegment.Count - 1
                        Dim arrLocSeg As String = arrLocSegment(jj)
                        Dim EntryLocSeg As String = ""

                        Dim EntryDateAndVNo As String() = arrDate(ii).Split("#")
                        Dim arrDatePartDate As Date = clsCommon.myCDate(EntryDateAndVNo(0))
                        Dim arrDatePartVoucherNo As String = clsCommon.myCstr(EntryDateAndVNo(1))
                        Dim EntryDesc As String = ""
                        Dim EntryDate As Date = Nothing
                        Dim AccOuntCode, Accountname As String
                        Dim DrAmt, CrAmt, TotCrAmt, TotDrAmt, Amt As Decimal
                        TotCrAmt = 0
                        TotDrAmt = 0
                        Dim DetailDesc As String = ""
                        Dim AccCode As String = String.Empty
                        Dim AccLocCode As String = String.Empty
                        Dim STRACCOUNT As String = String.Empty
                        Dim HirerachyCode As String = ""
                        Dim CostCenter As String = ""
                        For Each grow As GridViewRowInfo In gv.Rows
                            EntryDate = clsCommon.GetPrintDate(grow.Cells("Voucher Date").Value, "dd/MMM/yyyy")
                            EntryLocSeg = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Account_Seg_Code7 ,'') AS Account_Seg_Code7 FROM TSPL_GL_ACCOUNTS WHERE Account_Code ='" & clsCommon.myCstr(grow.Cells("Account Code").Value) & "'", trans))
                            If arrLocSeg = EntryLocSeg AndAlso arrDatePartDate = EntryDate AndAlso clsCommon.CompairString(arrDatePartVoucherNo, clsCommon.myCstr(grow.Cells("Voucher No").Value)) = CompairStringResult.Equal Then
                                EntryDesc = clsCommon.myCstr(grow.Cells("Voucher Description").Value)
                                TotDrAmt = TotDrAmt + clsCommon.myCdbl(grow.Cells("Debit").Value)
                                TotCrAmt = TotCrAmt + clsCommon.myCdbl(grow.Cells("Credit").Value)
                                AccCode = clsCommon.myCstr(grow.Cells("Account Code").Value)

                                Dim qry As String = "select TSPL_GL_ACCOUNTS.Account_Code from TSPL_GL_ACCOUNTS WHERE TSPL_GL_ACCOUNTS.Account_Code='" & AccCode & "' "
                                STRACCOUNT = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                                If clsCommon.myLen(STRACCOUNT) <= 0 Then
                                    Throw New Exception("Invalid Account code.")
                                End If
                                If clsCommon.GetDateWithStartTime(clsCommon.myCDate(EntryDate)) >= clsCommon.GetDateWithStartTime(ERPStartDate) Then
                                    'richa 17 SEp,2019 TEC/03/07/19-000927
                                    qry = "select TSPL_GL_ACCOUNTS.Account_Code from TSPL_GL_ACCOUNTS WHERE TSPL_GL_ACCOUNTS.Account_Code='" & AccCode & "'  and ( ControlAccount='N' or TSPL_GL_ACCOUNTS.Account_Code IN (select TSPL_CONTROL_ACC_MAPPING.Account_Code  from TSPL_CONTROL_ACC_MAPPING where IsForJE  =1)) "
                                    STRACCOUNT = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                                    If clsCommon.myLen(STRACCOUNT) <= 0 Then
                                        Throw New Exception("Account should be type of non Control Account.")
                                    End If
                                End If
                                ' Ticket No : ERO/29/04/19-000575 By Prabhakar
                                If SettingCostCenter AndAlso IsClosingEntry = False Then
                                    HirerachyCode = clsCommon.myCstr(grow.Cells("Hirerachy Code").Value)
                                    CostCenter = clsCommon.myCstr(grow.Cells("Cost Center").Value)
                                    Dim grouptype As String = ""
                                    grouptype = clsPaymentHeader.CheckGLAccountType(clsCommon.myCstr(STRACCOUNT), trans)
                                    If Not clsCommon.CompairString(grouptype, "Balance Sheet") = CompairStringResult.Equal Then
                                        If clsCommon.myLen(HirerachyCode) <= 0 Then
                                            Throw New Exception("Please provide the Hirerachy Level " + clsCommon.myCstr(LineNumber))
                                        ElseIf clsCommon.myLen(HirerachyCode) > 0 Then
                                            Dim STRHirerachyCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_HIRERACHY_LEVEL_MASTER.HIRERACHY_CODE From TSPL_HIRERACHY_LEVEL_MASTER   where HIRERACHY_CODE='" & HirerachyCode & "'", trans))
                                            If clsCommon.myLen(STRHirerachyCode) <= 0 Then
                                                Throw New Exception("Hirerachy code not exist in master.")
                                            End If
                                        End If
                                        If clsCommon.myLen(CostCenter) <= 0 Then
                                            Throw New Exception("Please provide the Cost Center " + clsCommon.myCstr(LineNumber))
                                        ElseIf clsCommon.myLen(CostCenter) > 0 Then
                                            Dim STRCostCenter = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cost_Center_Fin_Code from TSPL_COST_CENTRE_FINANCIAL WHERE TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code='" & CostCenter & "'", trans))
                                            If clsCommon.myLen(STRCostCenter) <= 0 Then
                                                Throw New Exception("Cost Center code not exist in master.")
                                            End If
                                            Dim DBLevel = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ISNULL(Level,'') AS Level from TSPL_HIRERACHY_LEVEL_MASTER WHERE HIRERACHY_CODE='" & HirerachyCode & "'", trans))
                                            STRCostCenter = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cost_Center_Fin_Code from TSPL_COST_CENTRE_FINANCIAL WHERE TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code='" & CostCenter & "' and Hirerachy_Level='" & DBLevel & "'", trans))
                                            If clsCommon.myLen(STRCostCenter) <= 0 Then
                                                Throw New Exception("Cost Center code not mapped with Hirerachy.")
                                            End If
                                        End If
                                    End If
                                End If
                                LineNumber += 1
                            End If
                        Next
                        If clsCommon.myLen(AccCode) > 0 Then
                            AccLocCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Account_Seg_Code7 ,'') AS Account_Seg_Code7 FROM TSPL_GL_ACCOUNTS WHERE Account_Code ='" & AccCode & "'", trans))
                        Else
                            AccLocCode = ""
                        End If

                        Dim StrVoucher As String = fnAutoGenerateNo(trans, arrDatePartDate, True, AccLocCode, True) ''Location pending
                        Dim strJrnl As String = "select (case when max(journal_no) is not null then max(journal_no) else 0 end) from TSPL_JOURNAL_MASTER "
                        Dim Jrnl As String = CInt(connectSql.RunScalar(trans, strJrnl)) + 1

                        connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_MASTER_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", arrDatePartDate), New SqlParameter("@Source_Code", "GL-JE"), New SqlParameter("@Source_Desc", "Journal Entry"), New SqlParameter("@Source_Doc_No", ""), New SqlParameter("@Source_Doc_Date", arrDatePartDate), New SqlParameter("@Posting_Date", arrDatePartDate), New SqlParameter("@Voucher_Desc", EntryDesc), New SqlParameter("@Source_Narration", ""), New SqlParameter("@Remarks", ""), New SqlParameter("@Comments", ""), New SqlParameter("@Auto_Reverse", "N"), New SqlParameter("@Reverse_Date", clsCommon.GetPrintDate(arrDatePartDate, "dd/MM/yyyy)")), New SqlParameter("@Source_Type", "O"), New SqlParameter("@CustVend_Code", ""), New SqlParameter("@CustVend_Name", ""), New SqlParameter("@Transaction_Type", IIf(IsClosingEntry, "X", "")), New SqlParameter("@Total_Debit_Amt", TotDrAmt), New SqlParameter("@Total_Credit_Amt", TotCrAmt), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", companyCode))



                        Dim strJrnl1 As String = "select journal_no from TSPL_JOURNAL_MASTER where Voucher_No='" + StrVoucher + "'"
                        Dim Jrnl1 As String
                        Jrnl1 = connectSql.RunScalar(trans, strJrnl1)
                        Dim LogSeg As String = ""

                        Dim i As Integer = 1
                        For Each grow As GridViewRowInfo In gv.Rows
                            EntryDate = clsCommon.GetPrintDate(grow.Cells("Voucher Date").Value, "dd/MMM/yyyy")
                            EntryLocSeg = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Account_Seg_Code7 ,'') AS Account_Seg_Code7 FROM TSPL_GL_ACCOUNTS WHERE Account_Code ='" & clsCommon.myCstr(grow.Cells("Account Code").Value) & "'", trans))
                            If arrLocSeg = EntryLocSeg AndAlso arrDatePartDate = EntryDate AndAlso clsCommon.CompairString(arrDatePartVoucherNo, clsCommon.myCstr(grow.Cells("Voucher No").Value)) = CompairStringResult.Equal Then
                                AccOuntCode = clsCommon.myCstr(grow.Cells("Account Code").Value)
                                Dim strAccChk As String = connectSql.RunScalar(trans, "select Account_Code from TSPL_GL_ACCOUNTS where Account_Code ='" + AccOuntCode + "'")
                                If clsCommon.myLen(strAccChk) > 0 Then
                                    If clsCommon.myLen(LogSeg) <= 0 Then
                                        LogSeg = clsDBFuncationality.getSingleValue("select Account_Seg_Code7  from TSPL_GL_ACCOUNTS where Account_Code='" + AccOuntCode + "'", trans)
                                    End If
                                    If SettingCostCenter Then
                                        HirerachyCode = clsCommon.myCstr(grow.Cells("Hirerachy Code").Value)
                                        CostCenter = clsCommon.myCstr(grow.Cells("Cost Center").Value)
                                    End If
                                    Dim LineNo As Integer = i
                                    Accountname = connectSql.RunScalar(trans, "select Description from TSPL_GL_ACCOUNTS where Account_Code ='" + AccOuntCode + "'")
                                    DrAmt = clsCommon.myCdbl(grow.Cells("Debit").Value)
                                    CrAmt = clsCommon.myCdbl(grow.Cells("Credit").Value)
                                    If DrAmt = 0 Then
                                        Amt = CrAmt * -1
                                    ElseIf CrAmt = 0 Then
                                        Amt = DrAmt
                                    End If
                                    DetailDesc = clsCommon.myCstr(grow.Cells("Description").Value)

                                    Dim strQ1 As String = " SELECT     Account_Type, Account_Group_Code, Account_Seg_Code1, Account_Seg_Desc1, Account_Seg_Code2, Account_Seg_Desc2, Account_Seg_Code3, " &
                                         " Account_Seg_Desc3, Account_Seg_Code4, Account_Seg_Desc4, Account_Seg_Code5, Account_Seg_Desc5, Account_Seg_Code6, Account_Seg_Desc6," &
                                         " Account_Seg_Code7, Account_Seg_Desc7, Account_Seg_Code8, Account_Seg_Desc8, Account_Seg_Code9, Account_Seg_Desc9, " &
                                         " Account_Seg_Code10, Account_Seg_Desc10 FROM  TSPL_GL_ACCOUNTS where Account_Code='" + AccOuntCode + "'"

                                    myreader = clsDBFuncationality.GetDataTable(strQ1, trans)
                                    If (myreader IsNot Nothing AndAlso myreader.Rows.Count > 0) Then

                                        For Each dr As DataRow In myreader.Rows


                                            Dim AccType As String = dr(0).ToString()
                                            Dim AccGrp As String = dr(1).ToString()

                                            Dim SegC1 As String = dr(2).ToString()
                                            Dim SegDesc1 As String = dr(3).ToString()

                                            Dim SegC2 As String = dr(4).ToString()
                                            Dim SegDesc2 As String = dr(5).ToString()

                                            Dim SegC3 As String = dr(6).ToString()
                                            Dim SegDesc3 As String = dr(7).ToString()

                                            Dim SegC4 As String = dr(8).ToString()
                                            Dim SegDesc4 As String = dr(9).ToString()

                                            Dim SegC5 As String = dr(10).ToString()
                                            Dim SegDesc5 As String = dr(11).ToString()

                                            Dim SegC6 As String = dr(12).ToString()
                                            Dim SegDesc6 As String = dr(13).ToString()

                                            Dim SegC7 As String = dr(14).ToString()
                                            Dim SegDesc7 As String = dr(15).ToString()

                                            Dim SegC8 As String = dr(16).ToString()
                                            Dim SegDesc8 As String = dr(17).ToString()

                                            Dim SegC9 As String = dr(18).ToString()
                                            Dim SegDesc9 As String = dr(19).ToString()

                                            Dim SegC10 As String = dr(20).ToString()
                                            Dim SegDesc10 As String = dr(21).ToString()

                                            ''richa UDL/09/10/18-000228
                                            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl1), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", arrDatePartDate), New SqlParameter("@Detail_Line_No", LineNo), New SqlParameter("@Account_code", AccOuntCode), New SqlParameter("@Account_Desc", Accountname), New SqlParameter("@Amount", Amt), New SqlParameter("@Description", DetailDesc), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", arrDatePartDate), New SqlParameter("@Account_Type", AccType), New SqlParameter("@Account_Group_Code", AccGrp), New SqlParameter("@Account_Seg_Code1", SegC1), New SqlParameter("@Account_Seg_Desc1", SegDesc1), New SqlParameter("@Account_Seg_Code2", SegC2), New SqlParameter("@Account_Seg_Desc2", SegDesc2), New SqlParameter("@Account_Seg_Code3", SegC3), New SqlParameter("@Account_Seg_Desc3", SegDesc3), New SqlParameter("@Account_Seg_Code4", SegC4), New SqlParameter("@Account_Seg_Desc4", SegDesc4), New SqlParameter("@Account_Seg_Code5", SegC5), New SqlParameter("@Account_Seg_Desc5", SegDesc5), New SqlParameter("@Account_Seg_Code6", SegC6), New SqlParameter("@Account_Seg_Desc6", SegDesc6), New SqlParameter("@Account_Seg_Code7", SegC7), New SqlParameter("@Account_Seg_Desc7", SegDesc7), New SqlParameter("@Account_Seg_Code8", SegC8), New SqlParameter("@Account_Seg_Desc8", SegDesc8), New SqlParameter("@Account_Seg_Code9", SegC9), New SqlParameter("@Account_Seg_Desc9", SegDesc9), New SqlParameter("@Account_Seg_Code10", SegC10), New SqlParameter("@Account_Seg_Desc10", SegDesc10))
                                            Dim coll As New Hashtable()
                                            clsCommon.AddColumnsForChange(coll, "Hirerachy_Code", HirerachyCode, True)
                                            clsCommon.AddColumnsForChange(coll, "Cost_Centre_Code", CostCenter, True)
                                            clsCommonFunctionality.UpdateDataTable(coll, "tspl_journal_details", OMInsertOrUpdate.Update, "tspl_journal_details.Journal_No='" + clsCommon.myCstr(Jrnl) + "' and tspl_journal_details.Voucher_No='" + clsCommon.myCstr(StrVoucher) + "' and account_code='" + clsCommon.myCstr(AccOuntCode) + "' and Detail_Line_No=" & LineNo & "", trans)

                                        Next
                                    End If
                                Else
                                    Throw New Exception("Account Code '" + AccOuntCode + "' Not Found!")
                                End If
                                i = i + 1
                            End If
                        Next
                        clsDBFuncationality.ExecuteNonQuery("update TSPL_JOURNAL_MASTER set Segment_code='" + LogSeg + "' where Voucher_No ='" + StrVoucher + "'", trans)
                    Next
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, "Line No " + clsCommon.myCstr(LineNumber) + " : " + ex.Message)
            Finally
                Me.Controls.Remove(gv)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub RadMenu2_Click(sender As Object, e As EventArgs) Handles RadMenu2.Click

    End Sub
    ' Ticket No : BHA/25/10/18-000639  By Prabhakar - For Reverse Voucher finder and save 
    Private Sub txtReverseVoucher__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtReverseVoucher._MYValidating
        'Dim qry As String = "select SourceCode as [Code],SourceDescription as [Description]  from TSPL_JOURNAL_MASTER   "
        'txtReverseVoucher.Value = clsCommon.ShowSelectForm("Voucher@SelectionForReverse", qry, "Code", "SourceLedger  ='GL'", txtReverseVoucher.Value, "Code", isButtonClicked)
        Dim qry As String = "SELECT  distinct   TSPL_JOURNAL_MASTER.Voucher_No AS VoucherNo, TSPL_JOURNAL_MASTER.Voucher_Desc AS Description, " & _
                           " TSPL_JOURNAL_MASTER.Source_Code AS [Source Type], convert(varchar(11),TSPL_JOURNAL_MASTER.Voucher_Date,103) AS [Voucher Date],  " & _
                           " TSPL_JOURNAL_MASTER.Source_Doc_No AS [Document No], convert(varchar(11),TSPL_JOURNAL_MASTER.Source_Doc_Date,103) AS [Document Date], " & _
                           " case when TSPL_JOURNAL_MASTER.Source_Code= 'AR-IN' then  TSPL_Customer_Invoice_Head.Against_Sale_No  " & _
                           " when TSPL_JOURNAL_MASTER.Source_Code= 'AR-CR' then TSPL_Customer_Invoice_Head.Against_Sale_Return_No " & _
                           " when TSPL_JOURNAL_MASTER.Source_Code= 'AP-IN' then  TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No " & _
                           " when TSPL_JOURNAL_MASTER.Source_Code= 'AP-CN' then  TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No  else '' end as RefDocNo, " & _
                           " CASE WHEN TSPL_JOURNAL_MASTER.Authorized = 'A' THEN 'Posted' ELSE 'Open' END AS Status,TSPL_JOURNAL_MASTER.Remarks,Auto_Reverse As [Auto Reverse] " & _
                           "  FROM TSPL_JOURNAL_MASTER LEFT OUTER JOIN TSPL_JOURNAL_DETAILS ON TSPL_JOURNAL_MASTER.Journal_No = TSPL_JOURNAL_DETAILS.Journal_No" & _
                           " left outer join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Document_No=TSPL_JOURNAL_MASTER.Source_Doc_No " & _
                           " left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_JOURNAL_MASTER.Source_Doc_No "

        Dim WhrCls As String = ""
        If clsCommon.CompairString(Program_Code, clsUserMgtCode.journalEntry) = CompairStringResult.Equal Then
            WhrCls = " (TSPL_JOURNAL_MASTER.ProgramCode is null or TSPL_JOURNAL_MASTER.ProgramCode='" & Program_Code & "')"
        Else
            WhrCls = " 2= 3"
        End If
        If clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal Then
            WhrCls = WhrCls
        Else
            If clsCommon.myLen(WhrCls) > 0 Then
                WhrCls = WhrCls & " and TSPL_JOURNAL_DETAILS.Account_code in (" + objCommonVar.strCurrUserGLAccount + ")"
            Else
                WhrCls = " TSPL_JOURNAL_DETAILS.Account_code in (" + objCommonVar.strCurrUserGLAccount + ")"
            End If
        End If
        WhrCls = WhrCls + " and TSPL_JOURNAL_MASTER.source_code ='GL-JE' and TSPL_JOURNAL_MASTER.Authorized = 'A'  and ( TSPL_JOURNAL_MASTER.AgainstVoucherNoReverseEntry is null or TSPL_JOURNAL_MASTER.AgainstVoucherNoReverseEntry = '' ) and TSPL_JOURNAL_MASTER.Voucher_No not in (select AgainstVoucherNoReverseEntry from  TSPL_JOURNAL_MASTER where AgainstVoucherNoReverseEntry is not null and AgainstVoucherNoReverseEntry <> '' )"



        txtReverseVoucher.Value = clsCommon.ShowSelectForm("ReverseVoucher@Selector", qry, "VoucherNo", WhrCls, txtReverseVoucher.Value, "", isButtonClicked, "TSPL_JOURNAL_MASTER.[Voucher_Date]")
        If clsCommon.myLen(txtReverseVoucher.Value) <= 0 Then
            funNew()
        Else
            funFillReverseVoucher()
        End If
    End Sub

    Private Sub butCostCenterAndHirerachy_Update_AfterPost_Click(sender As Object, e As EventArgs) Handles butCostCenterAndHirerachy_Update_AfterPost.Click
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim strJEStatus As String = clsDBFuncationality.getSingleValue("select Authorized from TSPL_JOURNAL_MASTER where Voucher_No ='" + fndVoucher.Value + "' ", trans)
            If clsCommon.CompairString(strJEStatus, "A") = CompairStringResult.Equal Then
                clsDBFuncationality.ExecuteNonQuery("ALTER TABLE TSPL_JOURNAL_DETAILS DISABLE TRIGGER TRG_JD_FiscaYearEndNoUpdateNoDelete", trans)
            Else
                trans.Rollback()
                clsCommon.MyMessageBoxShow(Me, "Document should be Posted.", Me.Text)
                Return
            End If

            For Each grow As GridViewRowInfo In gdAcc1.Rows
                Dim coll As New Hashtable()
                Dim strAccCode As String = clsCommon.myCstr(grow.Cells(1).Value)

                If clsCommon.myLen(strAccCode) > 0 Then
                    Dim LineNo As Integer = clsCommon.myCstr(grow.Cells(0).Value)
                    Dim strHierarchy As String = clsCommon.myCstr(grow.Cells(8).Value)
                    Dim strcostcentre As String = clsCommon.myCstr(grow.Cells(9).Value)
                    clsCommon.AddColumnsForChange(coll, "Hirerachy_Code", strHierarchy, True)
                    clsCommon.AddColumnsForChange(coll, "Cost_Centre_Code", strcostcentre, True)
                    clsCommonFunctionality.UpdateDataTable(coll, "tspl_journal_details", OMInsertOrUpdate.Update, " tspl_journal_details.Voucher_No='" + clsCommon.myCstr(fndVoucher) + "' and tspl_journal_details.Account_Code='" + clsCommon.myCstr(strAccCode) + "' and Detail_Line_No='" + clsCommon.myCstr(LineNo) + "'", trans)
                End If
            Next
            If clsCommon.CompairString(strJEStatus, "A") = CompairStringResult.Equal Then
                clsDBFuncationality.ExecuteNonQuery("ALTER TABLE TSPL_JOURNAL_DETAILS ENABLE TRIGGER TRG_JD_FiscaYearEndNoUpdateNoDelete", trans)
            End If
            trans.Commit()
            common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub funFillReverseVoucher()
        isInsideLoadData = True
        Try
            sql = " SELECT TSPL_JOURNAL_MASTER.Voucher_No, TSPL_JOURNAL_MASTER.Voucher_Date, TSPL_JOURNAL_MASTER.Source_Code, " &
                         "  TSPL_JOURNAL_MASTER.Source_Desc, TSPL_JOURNAL_MASTER.Source_Doc_No, TSPL_JOURNAL_MASTER.Source_Doc_Date, " &
                         "  TSPL_JOURNAL_MASTER.Posting_Date, TSPL_JOURNAL_MASTER.Voucher_Desc, TSPL_JOURNAL_MASTER.Source_Narration,  " &
                         "  TSPL_JOURNAL_MASTER.Remarks, TSPL_JOURNAL_MASTER.Comments, TSPL_JOURNAL_MASTER.Auto_Reverse,ISNULL(TSPL_JOURNAL_MASTER.Reverse_Date,'') AS Reverse_Date,  " &
                         "  TSPL_JOURNAL_MASTER.Source_Type, TSPL_JOURNAL_MASTER.CustVend_Code, CASE WHEN ISNULL(TSPL_JOURNAL_MASTER.Source_Type,'')='C' THEN TSPL_CUSTOMER_MASTER.CUSTOMER_NAME  WHEN ISNULL(TSPL_JOURNAL_MASTER.Source_Type,'')='V' THEN TSPL_VENDOR_MASTER.VENDOR_NAME ELSE TSPL_JOURNAL_MASTER.CustVend_Name end AS CustVend_Name,  " &
                         "  TSPL_JOURNAL_MASTER.Transaction_Type,  " &
                         "  TSPL_JOURNAL_MASTER.Provisional_Post, TSPL_JOURNAL_MASTER.Authorized, TSPL_JOURNAL_MASTER.Total_Debit_Amt,  " &
                         "  TSPL_JOURNAL_MASTER.Total_Credit_Amt, TSPL_JOURNAL_DETAILS.Detail_Line_No as [Line No], TSPL_JOURNAL_DETAILS.Account_code as [Acc Code],  " &
                         "  TSPL_JOURNAL_DETAILS.Account_Desc as [Acc Desc], case  when TSPL_JOURNAL_DETAILS.Amount >=0 then TSPL_JOURNAL_DETAILS.Amount else 0 end as DrAmt , case  when TSPL_JOURNAL_DETAILS.Amount <0 then TSPL_JOURNAL_DETAILS.Amount*-1 else 0 end as CrAmt, TSPL_JOURNAL_DETAILS.Description as [Desc],  " &
                         "  TSPL_JOURNAL_DETAILS.Reference as [Ref], convert(varchar(11),TSPL_JOURNAL_DETAILS.Posting_Date,103) AS [Date], " &
                         "  TSPL_JOURNAL_MASTER.SendToTally,TSPL_JOURNAL_MASTER.Segment_code,TSPL_JOURNAL_MASTER.MonthlyReverse,TSPL_JOURNAL_DETAILS.Hirerachy_code as [Hierarchy Code], " &
                         " TSPL_JOURNAL_DETAILS.Cost_Centre_Code as [Cost Centre],TSPL_JOURNAL_MASTER.Ind_As " &
                         "  FROM TSPL_JOURNAL_MASTER INNER JOIN " &
                         "  TSPL_JOURNAL_DETAILS ON   " &
                         "  TSPL_JOURNAL_MASTER.Voucher_No = TSPL_JOURNAL_DETAILS.Voucher_No " &
                                     "  left outer join  tspl_customer_master on tspl_customer_master.cust_code  =TSPL_JOURNAL_MASTER.CustVend_Code" &
            " left outer join  TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.VENDOR_CODE  =TSPL_JOURNAL_MASTER.CustVend_Code " &
            "  WHERE      TSPL_JOURNAL_MASTER.Voucher_No= '" + txtReverseVoucher.Value + "' order by [Line No] "

            ds = connectSql.RunSQLReturnDS(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                Me.txtBoxVoucher.Text = txtReverseVoucher.Value
                ' Me.dtVoucher.Value = clsCommon.GetPrintDate(ds.Tables(0).Rows(0).Item(1).ToString(), "dd/MMM/yyyy")
                Me.fndSrcCode.Value = ds.Tables(0).Rows(0).Item(2).ToString()
                Me.txtSrcDesc.Text = ds.Tables(0).Rows(0).Item(3).ToString()
                Me.txtSrcDoc.Text = ds.Tables(0).Rows(0).Item(4).ToString()
                Me.dtSrc.Value = clsCommon.GetPrintDate(ds.Tables(0).Rows(0).Item(5).ToString(), "dd/MMM/yyyy")
                Me.txtDocDesc.Text = ds.Tables(0).Rows(0).Item(8).ToString()
                Me.txtVoucherDesc.Text = "Against Reverse Voucher No : " + clsCommon.myCstr(txtReverseVoucher.Value) + "" ' ds.Tables(0).Rows(0).Item(7).ToString()
                Me.txtRemarks.Text = ds.Tables(0).Rows(0).Item(9).ToString()
                Me.txtComments.Text = ds.Tables(0).Rows(0).Item(10).ToString()
                txtLocation.Enabled = False
                txtLocation.Value = clsCommon.myCstr(ds.Tables(0).Rows(0).Item("Segment_code"))
                lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_SEGMENT_CODE where Segment_code='" + txtLocation.Value + "'"))
                Dim strRvsrFlag As String = ds.Tables(0).Rows(0).Item(11).ToString()
                If strRvsrFlag = "Y" OrElse clsCommon.CompairString(strRvsrFlag, "R") = CompairStringResult.Equal Then '' 31-July-2015 (Reverse 'R' means entry is reversed)
                    chkReverse.Checked = True
                    Me.dtRevese.Value = clsCommon.GetPrintDate(clsCommon.myCstr(ds.Tables(0).Rows(0).Item("Reverse_Date")), "dd/MM/yyyy")
                Else
                    chkReverse.Checked = False
                    Me.dtRevese.Value = clsCommon.GETSERVERDATE()
                End If
                If clsCommon.CompairString(clsCommon.myCstr(ds.Tables(0).Rows(0).Item("MonthlyReverse")), "1") = CompairStringResult.Equal Then
                    chkMonthly.Checked = True
                Else
                    chkMonthly.Checked = False
                End If

                chkIndAS.Checked = IIf(clsCommon.myCdbl(ds.Tables(0).Rows(0).Item("IND_AS")) = 1, True, False)
                '' 31-July-2015
                If clsCommon.myLen(txtSrcDoc.Text) > 0 Then
                    LblManAuto.Text = "GENERATED"
                Else
                    LblManAuto.Text = "ENTERED"
                End If
                '' 
                Dim strSrcFlag As String = ds.Tables(0).Rows(0).Item(13).ToString()
                If strSrcFlag = "O" Then
                    rdbOther.IsChecked = True
                ElseIf strSrcFlag = "V" Then
                    rdbVendor.IsChecked = True
                ElseIf strSrcFlag = "C" Then
                    rdbCustomer.IsChecked = True
                End If

                Me.fndCode.Value = ds.Tables(0).Rows(0).Item(14).ToString()
                Me.txtCodeDesc.Text = ds.Tables(0).Rows(0).Item(15).ToString()



                Dim strETypeFlag As String = ds.Tables(0).Rows(0).Item(16).ToString()
                If strETypeFlag = "N" Then
                    cmbType.SelectedIndex = 0
                ElseIf strETypeFlag = "A" Then
                    cmbType.SelectedIndex = 1
                ElseIf strETypeFlag = "X" Then
                    cmbType.SelectedIndex = 2
                ElseIf strETypeFlag = "O" Then
                    cmbType.SelectedIndex = 3
                End If

                Dim strProvFlag As String = ds.Tables(0).Rows(0).Item(17).ToString()

                Dim strAuthFlag As String = ds.Tables(0).Rows(0).Item(18).ToString()
                Dim SendToTally As Boolean = clsCommon.myCBool(ds.Tables(0).Rows(0).Item("SendToTally"))

                'If strAuthFlag = "A" Then
                '    btnSave.Text = "Update"
                '    UsLock1.Status = ERPTransactionStatus.Approved
                '    btnSave.Enabled = False
                '    btnDelete.Enabled = False
                '    btnAuth.Enabled = False
                '    btnSendToTally.Enabled = True
                '    btnProAuth.Enabled = False
                '    gdAcc1.ReadOnly = True
                'Else
                '    btnSave.Text = "Update"
                '    btnAuth.Enabled = True
                '    btnSendToTally.Enabled = False
                '    btnProAuth.Enabled = True
                '    UsLock1.Status = ERPTransactionStatus.Pending
                'End If
                'If clsCommon.myLen(txtSrcDoc.Text) > 0 Then
                '    btnSave.Enabled = False
                '    btnAuth.Enabled = False
                '    btnDelete.Enabled = False
                'End If

                If SendToTally Then
                    btnSendToTally.Enabled = False
                End If

                gdAcc1.AutoGenerateColumns = False
                Dim bs As New BindingSource()
                bs.DataSource = ds.Tables(0)
                gdAcc1.DataSource = bs
                gdAcc1.Columns(0).FieldName = "Line No"
                gdAcc1.Columns(1).FieldName = "Acc Code"
                gdAcc1.Columns(2).FieldName = "Acc Desc"

                gdAcc1.Columns(3).FieldName = "CrAmt"
                gdAcc1.Columns(4).FieldName = "DrAmt"

                gdAcc1.Columns(5).FieldName = "Desc"
                gdAcc1.Columns(6).FieldName = "Ref"
                gdAcc1.Columns(7).FieldName = "Date"
                gdAcc1.Columns(8).FieldName = "Hierarchy Code"
                gdAcc1.Columns(9).FieldName = "Cost Centre"
                UcAttachment1.LoadData(fndVoucher.Value)
            Else
                Return
                fndVoucher.MyReadOnly = False
            End If


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Journal-Entry", MessageBoxButtons.OK)

        End Try
        funShowAmt()

        isInsideLoadData = False
        gdAcc1.Rows.AddNew()
        gdAcc1.ReadOnly = True
        funControlEnableDisable(False)
    End Sub
    Public Sub funControlEnableDisable(ByVal isEnbale As Boolean)
        fndSrcCode.Enabled = isEnbale
        cmbType.Enabled = isEnbale
        txtSrcDoc.Enabled = isEnbale
        txtDocDesc.Enabled = isEnbale
        txtRemarks.Enabled = isEnbale
        txtComments.Enabled = isEnbale
        If isEnbale = True Then
            gdAcc1.ReadOnly = False
        ElseIf isEnbale = False Then
            gdAcc1.ReadOnly = True
        End If


        'txtBoxVoucher.Enabled = isEnbale
        'txtVoucherDesc.Enabled = isEnbale

        'fndSrcCode.Enabled = isEnbale
        'txtSrcDesc.Enabled = isEnbale
        'txtSrcDoc.Enabled = isEnbale
        'txtComments.Enabled = isEnbale
        'txtRemarks.Enabled = isEnbale

        'txtCr.Enabled = isEnbale
        'txtDr.Enabled = isEnbale
        'txtBoxCr.Enabled = isEnbale
        'txtBoxDr.Enabled = isEnbale
        'fndCode.Enabled = isEnbale
        'fndCode.Enabled = isEnbale
        'txtCodeDesc.Enabled = isEnbale
        'txtCodeDesc.Enabled = isEnbale
        'rdbOther.Enabled = isEnbale


        'chkReverse.Enabled = isEnbale
        'dtRevese.Enabled = isEnbale
        'txtSrcDoc.Enabled = isEnbale
        'txtDocDesc.Enabled = isEnbale
        'dtSrc.Enabled = isEnbale
        'dtSrc.Enabled = isEnbale


        'gdAcc1.ReadOnly = isEnbale
        'txtUnbalAmt.Enabled = isEnbale
        'LblManAuto.Enabled = isEnbale
        ''funSrcLoad()


        'gdAcc1.AllowAddNewRow = False
        'gdAcc1.ShowGroupPanel = False
        'gdAcc1.AllowColumnReorder = False
        'gdAcc1.AllowRowReorder = False
        'gdAcc1.EnableSorting = False
        'gdAcc1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        'gdAcc1.MasterTemplate.ShowRowHeaderColumn = False

        'txtLocation.Enabled = isEnbale
        'txtLocation.Enabled = isEnbale
        'lblLocation.Enabled = isEnbale


    End Sub
End Class

