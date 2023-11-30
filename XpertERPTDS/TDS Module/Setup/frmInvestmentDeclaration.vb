' ------------------------- Created By Preeti Gupta  --------------------'
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports System.Text.RegularExpressions

Public Class FrmInvestmentDeclaration
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim userCode, companyCode As String
#Region "Variable"
    Private isNewEntry As Boolean = False
#End Region

#Region "Functions"
    Public Function SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmInvestmentDeclaration)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow(Me, "Permission Denied", Me.Text)
            Me.Close()
            Return False
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        RadMenuItem3.Enabled = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        Return True
    End Function
    Function AllowToSave() As Boolean
        Dim Attachment As Integer
        Dim Rows As Integer = 0
        Dim FinYr As Double
        Dim InvType As Double
        Dim EmpCode As Double
        Attachment = clsDBFuncationality.getSingleValue("Select Count(*) From TSPL_ATTACHMENTS WHERE FormId ='" & MyBase.Form_ID & "' AND TransactionId ='" & txtCode.Value & "'")

        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Code can not be left blank", Me.Text)
            txtcode.Focus()
            Return False
        ElseIf clsCommon.myLen(txtFinYear.Value) <= 0 Then
            myMessages.blankValue("Financial year entry code")
            txtFinYear.Focus()
            Return False
        ElseIf clsCommon.myLen(TxtInvType.Value) <= 0 Then
            myMessages.blankValue("Investment type code")
            TxtInvType.Focus()
            Return False
        ElseIf clsCommon.myLen(TxtEmpCode.Value) <= 0 Then
            myMessages.blankValue("Employee master code")
            TxtEmpCode.Focus()
            Return False
        ElseIf clsCommon.myCdbl(TxtProvAmt.Text) <= 0 Then
            clsCommon.MyMessageBoxShow("Provisional amount can not be left blank or negative", Me.Text)
            TxtProvAmt.Focus()
            Return False
        ElseIf clsCommon.myLen(TxtActualAmt.Text) > 0 AndAlso (clsCommon.myCdbl(TxtActualAmt.Text) < 0 Or Not IsNumeric(TxtActualAmt.Text)) Then
            clsCommon.MyMessageBoxShow("Actual amount can should be in correct format.", Me.Text)
            TxtActualAmt.Focus()
            Return False
            'ElseIf clsCommon.CompairString(cmbStatus.Text, "Approved") = CompairStringResult.Equal AndAlso Attachment <= 0 Then
            '    common.clsCommon.MyMessageBoxShow("Please make at least one attachment.")
            '    Return False
        End If
        If clsCommon.myLen(txtFinYear.Value) > 0 Then
            FinYr = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Count(*) From TSPL_TDS_FINANCIAL_YEAR Where Year_Name='" & clsCommon.myCstr(txtFinYear.Value) & "'"))
            If FinYr = 0 Then
                common.clsCommon.MyMessageBoxShow("Please check ! Finincial year code (" & clsCommon.myCstr(txtFinYear.Value) & ") does not exists.")
                txtFinYear.Focus()
                Return False
            End If
        End If
        If clsCommon.myLen(TxtInvType.Value) > 0 Then
            InvType = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Count(*)  From  TSPL_INVESTMENT_TYPE  Where Code='" & clsCommon.myCstr(TxtInvType.Value) & "'"))
            If InvType = 0 Then
                common.clsCommon.MyMessageBoxShow("Please check ! Investment type code (" & clsCommon.myCstr(TxtInvType.Value) & ") does not exists.")
                TxtInvType.Focus()
                Return False
            End If
        End If
        If clsCommon.myLen(TxtEmpCode.Value) > 0 Then
            EmpCode = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Count(*) From TSPL_EMPLOYEE_MASTER Where EMP_CODE='" & clsCommon.myCstr(TxtEmpCode.Value) & "'"))
            If EmpCode = 0 Then
                common.clsCommon.MyMessageBoxShow("Please check ! Employee code (" & clsCommon.myCstr(TxtEmpCode.Value) & ") does not exists.")
                TxtEmpCode.Focus()
                Return False
            End If
        End If
        UcAttachment1.AllowToSave()
        ' Dim aa As String = UcAttachment1.gv1.Rows(0).Cells(1).Value
        For Each grow As GridViewRowInfo In UcAttachment1.gv1.Rows
            If clsCommon.myLen(grow.Cells(4).Value) > 0 Then
                Rows = Rows + 1
            End If
        Next
        If cmbStatus.Text = "Approved" AndAlso Rows <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please make at least one attachment.", Me.Text)
            Return False
        End If
        Return True
    End Function
    Sub funReset()
        UcAttachment1.BlankAllControls()
        UsLock1.Status = ERPTransactionStatus.Pending
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        TxtDesp.Text = ""
        txtFinYear.Value = ""
        TxtEmpCode.Value = ""
        TxtInvType.Value = ""
        TxtProvAmt.Text = 0
        TxtActualAmt.Text = 0
        lblEmpName.Text = ""
        lblInvesTypeName.Text = ""
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = False
        btnPost.Enabled = False
    End Sub
    Sub LoadStatus()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr("Code") = "A"
        dr("Name") = "Approved"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "U"
        dr("Name") = "UnApproved"
        dt.Rows.Add(dr)

        cmbStatus.DataSource = dt
        cmbStatus.ValueMember = "Code"
        cmbStatus.DisplayMember = "Name"
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        'txtCode.MyReadOnly = True
        btnsave.Enabled = True

        'isNewEntry = False
        Dim obj As New ClsInvestmentDeclaration()
        obj = ClsInvestmentDeclaration.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.CODE) > 0) Then
            funReset()
            isNewEntry = False
            txtCode.MyReadOnly = True
            btnsave.Text = "Update"
            btndelete.Enabled = True
            btnPost.Enabled = True
            btndelete.Enabled = True
            If obj.lblStatus = ERPTransactionStatus.Approved Then
                btnsave.Enabled = False
                btnPost.Enabled = False
                btndelete.Enabled = False
            End If
            txtCode.Value = obj.CODE
            TxtDesp.Text = obj.Description
            txtFinYear.Value = obj.Financial_Year_Code
            TxtInvType.Value = obj.Investment_Type_Code
            TxtEmpCode.Value = obj.Employee_Code
            TxtProvAmt.Text = obj.Provisional_Amount
            lblEmpName.Text = obj.Emp_Name
            TxtEmpCode.Value = obj.Employee_Code
            TxtActualAmt.Text = obj.Actual_Amount
            lblInvesTypeName.Text = obj.Investment_Type_Name
            cmbStatus.SelectedValue = obj.Status
            UsLock1.Status = obj.lblStatus
            UcAttachment1.LoadData(obj.CODE)
        Else
            funReset()
        End If
    End Sub
    Private Function SaveData(ByVal IsPost As Boolean) As Boolean
        Try
            If AllowToSave() Then
                Dim obj As New ClsInvestmentDeclaration()
                obj.CODE = txtcode.Value
                obj.Description = TxtDesp.Text
                obj.Financial_Year_Code = txtFinYear.Value
                obj.Investment_Type_Code = TxtInvType.Value
                obj.Employee_Code = TxtEmpCode.Value
                obj.Status = cmbStatus.SelectedValue
                'If String.IsNullOrEmpty(TxtProvAmt.Text) Then
                '    TxtProvAmt.Text = 0
                'End If
                obj.Provisional_Amount = TxtProvAmt.Text

                If String.IsNullOrEmpty(TxtActualAmt.Text) Then
                    TxtActualAmt.Text = 0
                End If
                obj.Actual_Amount = TxtActualAmt.Text

                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Dim isSaved As Boolean = obj.SaveData(obj, isNewEntry, trans)

                'If (obj.SaveData(obj, isNewEntry, trans)) Then
                If isSaved Then
                    trans.Commit()
                    UcAttachment1.SaveData(obj.CODE)
                    If IsPost Then
                        common.clsCommon.MyMessageBoxShow(Me, "Data Posted Successfully", Me.Text)
                        btndelete.Enabled = False
                        btnPost.Enabled = False
                        btnsave.Enabled = False
                    Else
                        common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    End If
                    LoadData(obj.CODE, NavigatorType.Current)
                    btnsave.Text = "Update"
                    btndelete.Enabled = True
                    Return True
                Else
                    btnsave.Text = "Save"
                    btndelete.Enabled = False
                    trans.Rollback()
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
            Return False
        End Try
        Return False
    End Function
    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Code not found to delete", Me.Text)
            Exit Sub
        End If
        funDelete()
    End Sub
    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (ClsInvestmentDeclaration.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ", Me.Text)
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Function PostData()
        Try
            If myMessages.postConfirm Then
                If AllowToSave() Then
                    SavingData(True)
                    ClsInvestmentDeclaration.PostData(txtCode.Value)
                    btndelete.Enabled = False
                    btnPost.Enabled = False
                    btnsave.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return True
    End Function

    Sub SavingData(ByVal ChekBtnPost As Boolean)

        If (SaveData(True)) Then
            If ChekBtnPost = False Then
                common.clsCommon.MyMessageBoxShow(Me, "Data Posted Successfully", Me.Text)
            End If
        End If

    End Sub
#End Region

    Private Sub FrmInvestmentDeclaration_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isNewEntry = True
        RadPageView1.SelectedPage = RadPageViewPage1
        LoadStatus()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+N Adding New ")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        funReset()
        ''For Attachment
        If objCommonVar.IsDemoERP Then
            UcAttachment1.Form_ID = MyBase.Form_ID
            RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Visible
        Else
            RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Collapsed
        End If
        ''End of For Attachment
    End Sub

    Private Sub FrmInvestmentDeclaration_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnreset.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnsave.Enabled Then
            SaveData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Private Sub txtcode__MYNavigator(sender As Object, e As EventArgs, NavType As common.NavigatorType) Handles txtcode._MYNavigator
        Try
            LoadData(txtcode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtcode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtcode._MYValidating
        Dim str As String = "select count(*) from TSPL_INVESTMENT_DECLARATION_payroll where CODE ='" + txtcode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtcode.MyReadOnly = False
        Else
            txtcode.MyReadOnly = True
        End If

        If txtcode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = ""
            qry = "Select CODE As [Code],Description As [Description],FINANCIAL_YEAR_CODE As [Financial Year Code],Investment_Type_Code As [Investment_Type Code],EMP_CODE As [Emp Code],provisional_AMOUNT As [Provisional Amount] from TSPL_INVESTMENT_DECLARATION_payroll"
            txtcode.Value = clsCommon.ShowSelectForm("TSPL_INVESTMENT_DECLARATION_payroll", qry, "Code", "", txtcode.Value, "TSPL_INVESTMENT_DECLARATION_payroll.CODE", isButtonClicked)
            If clsCommon.myLen(txtcode.Value) > 0 Then
                Dim objOT As ClsInvestmentDeclaration
                objOT = ClsInvestmentDeclaration.GetData(txtcode.Value, NavigatorType.Current)
                If Not objOT Is Nothing Then
                    LoadData(txtcode.Value, NavigatorType.Current)
                End If
            Else
                funReset()
            End If
        End If
    End Sub

    Private Sub txtFinYear__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtFinYear._MYValidating
        Dim qry As String = " Select Year_Name AS CODE  From TSPL_TDS_FINANCIAL_YEAR "
        txtFinYear.Value = clsCommon.ShowSelectForm("fmFinYr", qry, "Code", "", txtFinYear.Value, "CODE", isButtonClicked)
    End Sub

    Private Sub TxtInvType__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtInvType._MYValidating
        Dim qry As String = " Select Code AS CODE,Description  From TSPL_INVESTMENT_TYPE "
        TxtInvType.Value = clsCommon.ShowSelectForm("fmInvType", qry, "Code", "", TxtInvType.Value, "CODE", isButtonClicked)

        If clsCommon.myLen(TxtInvType.Value) > 0 Then
            lblInvesTypeName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Description  From TSPL_INVESTMENT_TYPE WHERE Code='" + TxtInvType.Value + "'"))
        Else
            lblInvesTypeName.Text = ""
        End If

    End Sub

    Private Sub TxtEmpCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtEmpCode._MYValidating
        Dim qry As String = " Select EMP_CODE AS CODE ,Emp_Name  From TSPL_EMPLOYEE_MASTER "
        TxtEmpCode.Value = clsCommon.ShowSelectForm("fmEmpCode", qry, "Code", "", TxtEmpCode.Value, "CODE", isButtonClicked)

        If clsCommon.myLen(TxtEmpCode.Value) > 0 Then
            lblEmpName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Emp_Name  From TSPL_EMPLOYEE_MASTER WHERE EMP_CODE='" + TxtEmpCode.Value + "'"))
        Else
            lblEmpName.Text = ""
        End If
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData(False)
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
       PostData()
    End Sub

    Private Sub rmExport_Click(sender As Object, e As EventArgs) Handles rmExport.Click
        Dim str As String
        str = "Select CODE As [Code],Description As [Description],FINANCIAL_YEAR_CODE As [Financial Year Code],INVESTMENT_TYPE_Code As [Investment Type Code],EMP_CODE As [Emp Code],PROVISIONAL_AMOUNT As [Provisional Amount],ACTUAL_AMOUNT As [Actual Amount],Status from TSPL_INVESTMENT_DECLARATION_payroll"
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub rmImport_Click(sender As Object, e As EventArgs) Handles rmImport.Click
        Dim FIN_CODE As String = ""
        Dim EMP_CODE As String = ""
        Dim PAY_CODE As String = ""
        Dim IsPost As Integer = 0
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Code", "Description", "Financial Year Code", "Investment Type Code", "Emp Code", "Provisional Amount", "Actual Amount", "Status") Then
            Dim trans As SqlTransaction
            trans = clsDBFuncationality.GetTransactin()
            Try

                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New ClsInvestmentDeclaration()
                    Dim LineNo As String = clsCommon.myCstr(grow.Index) + 2
                    Dim strCode As String = clsCommon.myCstr(grow.Cells("Code").Value)
                    If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                        Throw New Exception("Code can not be blank or incorrect.")
                    End If
                    obj.CODE = strCode

                    Dim strDesp As String = clsCommon.myCstr(grow.Cells("Description").Value)
                    If strDesp.Length > 100 Then
                        Throw New Exception("Description can should be max. 100 character On Line No " & LineNo & ".")
                    End If
                    obj.Description = strDesp

                    Dim strFinYear As String = clsCommon.myCstr(grow.Cells("Financial Year Code").Value)
                    If strFinYear.Length > 20 Or (String.IsNullOrEmpty(strFinYear)) Then
                        Throw New Exception("Financial Year Code can not be blank or incorrect On Line No " & LineNo & ".")
                    Else
                        FIN_CODE = clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_TDS_FINANCIAL_YEAR Where Year_Name ='" & strFinYear & "'", trans)
                        If FIN_CODE <= 0 Then
                            Throw New Exception("Financial Year Code(" & strFinYear & ") On Line No " & LineNo & " does not exist . Please make it entry first.")
                        End If
                    End If
                    obj.Financial_Year_Code = strFinYear

                    Dim strInv As String = clsCommon.myCstr(grow.Cells("Investment Type Code").Value)
                    If strInv.Length > 30 Or (String.IsNullOrEmpty(strInv)) Then
                        Throw New Exception("Investment Type Code can not be blank or incorrect On Line No " & LineNo & ".")
                    Else
                        PAY_CODE = clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_INVESTMENT_TYPE Where CODE ='" & strInv & "'", trans)
                        If PAY_CODE <= 0 Then
                            Throw New Exception("Investment Type Code(" & strInv & ") On Line No " & LineNo & " does not exist.Please make it entry first.")
                        End If
                    End If
                    obj.Investment_Type_Code = strInv

                    Dim strEmpCode As String = clsCommon.myCstr(grow.Cells("Emp Code").Value)
                    If strEmpCode.Length > 12 Or (String.IsNullOrEmpty(strEmpCode)) Then
                        Throw New Exception("Emp Code can not be blank or incorrect On Line No " & LineNo & ".")
                    Else
                        EMP_CODE = clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_EMPLOYEE_MASTER Where EMP_CODE ='" & strEmpCode & "'", trans)
                        If EMP_CODE <= 0 Then
                            Throw New Exception("Emp Code(" & strEmpCode & ") On Line No " & LineNo & " does not exist . Please make it entry first.")
                        End If
                    End If
                    obj.Employee_Code = strEmpCode

                    Dim PAmt As String = clsCommon.myCstr(grow.Cells("Provisional Amount").Value)
                    Dim DblProAmt As Double = clsCommon.myCdbl(grow.Cells("Provisional Amount").Value)
                    If String.IsNullOrEmpty(PAmt) Or Not IsNumeric(grow.Cells("Provisional Amount").Value) Or DblProAmt <= 0 Then
                        Throw New Exception("Provisional Amount can not be blank or incorrect On Line No " & LineNo & ".")
                        'ElseIf Not IsNumeric(grow.Cells("Provisional Amount").Value) Then
                        '    Throw New Exception("Provisional Amount Should be Numeric ")
                    ElseIf PAmt.Length > 10 Then
                        Throw New Exception("Please check ! length of provisional amount should be max 10 digits on line no " & LineNo & ".")
                    End If
                    obj.Provisional_Amount = PAmt

                    Dim AAmt As String = clsCommon.myCstr(grow.Cells("Actual Amount").Value)
                    Dim DblAAmt As Double = clsCommon.myCdbl(grow.Cells("Actual Amount").Value)
                    If String.IsNullOrEmpty(AAmt) Then
                        AAmt = "0"
                    ElseIf Not IsNumeric(grow.Cells("Actual Amount").Value) Or DblAAmt < 0 Then
                        Throw New Exception("Actual Amount Should be Numeric On Line No " & LineNo & ".")
                    ElseIf AAmt.Length > 10 Then
                        Throw New Exception("Please check ! length of actual amount should be max 10 digits on line no " & LineNo & ".")
                    End If
                    obj.Actual_Amount = AAmt

                    Dim strStatus As String = clsCommon.myCstr(grow.Cells("Status").Value)
                    If strStatus.Length > 12 Or (String.IsNullOrEmpty(strStatus)) Then
                        Throw New Exception("Status can not be blank or incorrect On Line No " & LineNo & ".")
                    Else

                        IsPost = clsDBFuncationality.getSingleValue("SELECT isnull(Is_Post,0) As IsPost FROM TSPL_INVESTMENT_DECLARATION_payroll Where CODE ='" & strCode & "'", trans)
                        If clsCommon.CompairString(IsPost, 0) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strStatus, "A") = CompairStringResult.Equal Then
                            Throw New Exception("Status must be ('U') On Line No " & LineNo & ".")
                            'ElseIf clsCommon.CompairString(IsPost, "A") = CompairStringResult.Equal AndAlso clsCommon.CompairString(strStatus, "U") = CompairStringResult.Equal Then
                            '    Throw New Exception("Status must be ('A') On Line No " & LineNo & ".")
                            'Else
                            '    Throw New Exception("Status must be ('U') On Line No " & LineNo & ".")
                        End If
                    End If
                    obj.Status = strStatus

                    If IsPost = 0 Then
                        obj.SaveData(obj, ClsInvestmentDeclaration.CheckNewEntry(obj.CODE, trans), trans)
                    End If

                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
                trans.Rollback()
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub btnreset_Click(sender As Object, e As EventArgs) Handles btnreset.Click
        funReset()
    End Sub
End Class
