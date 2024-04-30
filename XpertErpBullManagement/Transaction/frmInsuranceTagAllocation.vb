Imports System.Data.SqlClient
Imports common
Imports System.IO
Imports Telerik
Imports XpertERPEngine
Imports XpertERPEngineFine
Imports Telerik.WinControls.UI
Imports Telerik.WinControls

Public Class frmInsuranceTagAllocation


#Region "Variables"
    Private isNewEntry As Boolean = False

#End Region

    Private Sub frmInsuranceTagAllocation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)()

        coll.Add("Document_Code", "varchar(30) NOT NULL Primary Key")
        coll.Add("Document_Date", "DateTime not NULL")
        coll.Add("Bull_Code", "VARCHAR(30) not NULL REFERENCES TSPL_BULL_MASTER (Bull_Code)")
        coll.Add("Tag_No", "integer not NULL REFERENCES TSPL_BULL_INSURANCE_TAG (PK_Id)")
        coll.Add("Status", "integer not null default 0")
        coll.Add("Created_By", "varchar(12) NOT NULL REFERENCES TSPL_USER_MASTER (USER_CODE) ")
        coll.Add("Created_Date", "datetime NOT NULL  ")
        coll.Add("Modified_By", "varchar(12) NOT NULL REFERENCES TSPL_USER_MASTER (USER_CODE) ")
        coll.Add("Modified_Date", "datetime NOT NULL ")
        coll.Add("Posted_By", "varchar(12) NULL  REFERENCES TSPL_USER_MASTER (USER_CODE)")
        coll.Add("Posted_Date", "datetime NULL")
        clsCommonFunctionality.CreateOrAlterTable(False, False, "TSPL_INS_TAG_ALLOCATION", coll, Nothing, True, False, Nothing, Nothing, Nothing, False)

        SetUserMgmtNew()
        Addnew()
        btnPost.Visible = True
        btnPost.Enabled = False
        If clsCommon.myLen(txtDocumentNo.Value) > 0 Then
            LoadData(clsCommon.myCstr(txtDocumentNo.Value), NavigatorType.Current)
        End If
    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        btnPost.Visible = MyBase.isPostFlag

    End Sub
    Private Sub txtDocumentNo__MYNavigator(sender As Object, e As EventArgs, NavType As common.NavigatorType) Handles txtDocumentNo._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_INS_TAG_ALLOCATION where Document_Code='" + txtDocumentNo.Value + "' "
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If count = 0 Then
                txtDocumentNo.MyReadOnly = False
            Else
                txtDocumentNo.MyReadOnly = True
            End If
            LoadData(txtDocumentNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocumentNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocumentNo._MYValidating
        If clsCommon.myLen(txtDocumentNo) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Document No can't be blank", Me.Text)
        End If
        txtDocumentNo.Value = clsInsuranceTagAllocation.getFinder(txtDocumentNo.Value, isButtonClicked)
        LoadData(txtDocumentNo.Value, NavigatorType.Current)
    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        Addnew()
    End Sub
    Private Sub frmInsuranceTagAllocation_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            btnAddNew.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnsave.Enabled AndAlso MyBase.isModifyFlag Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnsave.Enabled AndAlso MyBase.isDeleteFlag Then
            btndelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled AndAlso MyBase.isPostFlag Then
            btnPost.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclose.Enabled Then
            Me.Close()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then

                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SIRC"
                frm.strCode = "SIReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnReverseUnpost.Visible = True
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
            End If
        End If
    End Sub
    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        CancelPressed()
    End Sub
    Private Sub btnReverseUnpost_Click_(sender As Object, e As EventArgs) Handles btnReverseUnpost.Click
        Try
            If clsCommon.MyMessageBoxShow(Me, "Do you want to Reverse and unpost the current Document" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                Dim Reason As String = ""
                Dim frm As New FrmFreeTxtBox1
                frm.Text = "Remarks for Reverse"
                frm.ShowDialog()
                If clsCommon.myLen(frm.strRmks) <= 0 Then
                    Exit Sub
                Else
                    Reason = frm.strRmks
                End If
                If clsInsuranceTagAllocation.ReverseAndUnpost(txtDocumentNo.Value) Then
                    clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtDocumentNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub CancelPressed()
        Me.Close()
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click

        Try
            If clsCommon.myLen(txtDocumentNo.Value) <= 0 Then
                Throw New Exception("No document found to post")
            End If
            If clsCommon.MyMessageBoxShow(Me, "Post the Current Document [" + txtDocumentNo.Value + "]" + Environment.NewLine + "Are You Sure.", Me.Text, MessageBoxButtons.YesNo, WinControls.RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then

                clsInsuranceTagAllocation.PostData(MyBase.Form_ID, txtDocumentNo.Value)
                clsCommon.MyMessageBoxShow(Me, "Data posted successfully", Me.Text)
                LoadData(txtDocumentNo.Value, NavigatorType.Current)

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Function AllowToSave() As Boolean

        If clsCommon.myLen(txtBullCode.Value) <= 0 Then
            txtBullCode.Focus()
            clsCommon.MyMessageBoxShow("Bull Code can't be blank.")
            Exit Function
            Return False
        End If
        If clsCommon.myLen(txtTagNo.Value) <= 0 Then
            txtTagNo.Focus()
            clsCommon.MyMessageBoxShow("Insurance Tag can't be blank.")
            Exit Function
            Return False
        End If
        Return True
    End Function


    Sub SaveData()
        Try
            If (AllowToSave()) Then
                Dim obj As New clsInsuranceTagAllocation()
                obj.Document_Code = clsCommon.myCstr(txtDocumentNo.Value)
                obj.Bull_Code = clsCommon.myCstr(txtBullCode.Value)
                obj.Document_date = clsCommon.myCDate(txtdate.Value)
                obj.Tag_No = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select PK_Id from TSPL_BULL_INSURANCE_TAG where Tag_No = '" & txtTagNo.Value & "'"))

                If (obj.SaveData(obj, isNewEntry, Nothing, False)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(obj.Document_Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub Addnew()
        isNewEntry = True
        txtDocumentNo.Value = ""
        btnsave.Enabled = True
        txtBullCode.Value = ""
        txtTagNo.Value = ""
        lblBullAliasName.Text = ""
        btnPost.Enabled = True
        txtdate.Value = clsCommon.GETSERVERDATE()
        btnsave.Text = "Save"
        btndelete.Enabled = True
        lblStatus.Status = ERPTransactionStatus.Pending

    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            btnsave.Enabled = True
            btnPost.Enabled = True
            Addnew()
            Dim obj As New clsInsuranceTagAllocation()
            obj = clsInsuranceTagAllocation.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(clsCommon.myCstr(obj.Document_Code)) > 0) Then
                txtDocumentNo.Value = clsCommon.myCstr(obj.Document_Code)
                txtdate.Value = clsCommon.myCDate(obj.Document_date)
                txtBullCode.Value = clsCommon.myCstr(obj.Bull_Code)
                lblBullAliasName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Bull_Alia_Name as Name from TSPL_BULL_MASTER where Bull_Code = '" & txtBullCode.Value & "'"))
                txtTagNo.Value = clsCommon.myCstr(obj.Tag_No)

                isNewEntry = False
                btnsave.Text = "Update"
                If obj.Status = 1 Then
                    lblStatus.Status = ERPTransactionStatus.Approved
                    btndelete.Enabled = False
                    btnsave.Enabled = False
                    btnPost.Enabled = False
                Else
                    lblStatus.Status = ERPTransactionStatus.Pending
                    btndelete.Enabled = True
                    btnsave.Enabled = True
                    btnPost.Enabled = True
                End If

            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally

        End Try
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        Try
            If clsCommon.myLen(txtDocumentNo.Value) <= 0 Then
                Throw New Exception("Document No not found to delete")
            End If
            If (myMessages.deleteConfirm()) Then
                clsInsuranceTagAllocation.DeleteData(txtDocumentNo.Value)
                clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully", Me.Text)
                Addnew()
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub txtBullCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtBullCode._MYValidating
        Try
            Dim arrBull_Code As New List(Of String)
            Dim whrcls As String = ""
            Dim dt As DataTable = Nothing
            Dim qry As String = "select Bull_Code as Code,Bull_Alia_Name as Name from  TSPL_BULL_MASTER "
            dt = clsDBFuncationality.GetDataTable("select Bull_Code from TSPL_INS_TAG_ALLOCATION ")
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    arrBull_Code.Add(dr("Bull_Code"))
                Next
                whrcls = " Bull_Code not in ( " & clsCommon.GetMulcallString(arrBull_Code) & ")"

            End If

            txtBullCode.Value = clsCommon.ShowSelectForm("InsuranceTagAllocation", qry, "Code", whrcls, txtBullCode.Value, "Code", isButtonClicked)

            qry += "where bull_code = '" & txtBullCode.Value & "'"

            lblBullAliasName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Bull_Alia_Name as Name from TSPL_BULL_MASTER where Bull_Code = '" & txtBullCode.Value & "'"))


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub txtTagNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtTagNo._MYValidating
        Try
            Dim arrTagNo As New List(Of String)
            Dim whrcls As String = ""
            Dim dt As DataTable = Nothing
            Dim qry As String = "SELECT TSPL_BULL_INSURANCE_TAG.PK_Id as TagID,TSPL_BULL_INSURANCE_TAG.Tag_No as TagNo,TSPL_BULL_INSURANCE.Policy_Code as [Policy No],convert(varchar,TSPL_BULL_INSURANCE.Policy_Date,103)  as [Policy Date] ,
            convert(varchar,Policy_Start_Date,103) [Policy Start Date], convert(varchar,Policy_End__Date,103) [Policy End Date] FROM TSPL_BULL_INSURANCE_TAG left outer join TSPL_BULL_INSURANCE on TSPL_BULL_INSURANCE.Document_Code = TSPL_BULL_INSURANCE_TAG.Document_Code "

            dt = clsDBFuncationality.GetDataTable("select Tag_No from TSPL_INS_TAG_ALLOCATION ")
            If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        arrTagNo.Add(dr("Tag_No"))
                    Next
                    whrcls = " TSPL_BULL_INSURANCE_TAG.PK_Id not in ( " & clsCommon.GetMulcallStringWithComma(arrTagNo) & ")"

                End If

            txtTagNo.Value = clsCommon.ShowSelectForm("InsuranceTagAllocation", qry, "TagNo", whrcls, txtTagNo.Value, "TagID", isButtonClicked)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
End Class