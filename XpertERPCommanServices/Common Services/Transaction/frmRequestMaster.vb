Imports common
Imports System.Data.SqlClient
Public Class frmRequestMaster
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
    Public DocumentNo As String = ""
#End Region
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub frmRequestMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)()
        coll.Add("REQUEST_CODE", "Varchar(30) not null PRIMARY KEY")
        coll.Add("REQUEST_DATE", "date not null")
        coll.Add("REQUEST_BY", "varchar(12) NOT NULL REFERENCES TSPL_USER_MASTER (USER_CODE)")
        coll.Add("REQUEST_FOR", "varchar(12) NOT NULL REFERENCES TSPL_USER_MASTER (USER_CODE)")
        coll.Add("SCREEN_CODE", "VARCHAR(12) NOT NULL REFERENCES TSPL_PROGRAM_MASTER(Program_Code)")
        coll.Add("REASON", "varchar(1000) NOT NULL")
        coll.Add("REMARKS", "varchar(1000)  NULL")
        coll.Add("Created_By", "varchar(12) NOT NULL REFERENCES TSPL_USER_MASTER (USER_CODE)")
        coll.Add("Created_Date", "datetime NOT NULL")
        coll.Add("Modified_By", "varchar(12) NOT NULL REFERENCES TSPL_USER_MASTER (USER_CODE)")
        coll.Add("Modified_Date", "datetime NOT NULL")
        coll.Add("POSTED", "integer  NOT NULL DEFAULT 0")
        coll.Add("Posted_By", "varchar(12) NULL REFERENCES TSPL_USER_MASTER (USER_CODE)")
        coll.Add("Posted_Date", "datetime NULL")
        coll.Add("APPROVED_STATUS", "integer  NOT NULL DEFAULT 0")
        coll.Add("APPROVED_STATUS_BY", "varchar(12) NULL REFERENCES TSPL_USER_MASTER (USER_CODE)")
        coll.Add("APPROVED_STATUS_DATE", "datetime NULL")

        clsCommonFunctionality.CreateOrAlterTable(False, "TSPL_USER_REQUEST_MASTER", coll, Nothing, False)
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P  for Post ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New")
        funReset()
        txtRequestBy.Text = clsCommon.myCstr(objCommonVar.CurrentUserCode)
        lblRequestBy.Text = clsRequestMaster.getUserName(objCommonVar.CurrentUserCode, Nothing)
        txtRequestTo.Text = clsRequestMaster.getReportingUserCode(objCommonVar.CurrentUserCode, Nothing)
        lblRequestTo.Text = clsRequestMaster.getUserName(txtRequestTo.Text, Nothing)

        RadPageView1.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.Report_ID = MyBase.Form_ID
            UcCustomFields1.LoadCustomControls()
        End If
        If clsCommon.myLen(DocumentNo) > 0 Then
            LoadData(DocumentNo, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Sub funReset()
        isNewEntry = True
        textRequestCode.MyReadOnly = False
        textRequestCode.Focus()
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = False
        btnPost.Enabled = False
        textRequestCode.Value = Nothing
        txtReason.Text = Nothing
        txtRemarks.Text = Nothing
        txtScreen.Value = Nothing
        lblScreen.Text = Nothing
        dtpRequestDate.Value = clsCommon.GETSERVERDATE()
        UsLock1.Status = ERPTransactionStatus.Pending
        UsLock2.Status = ERPTransactionStatus.Pending
        UcAttachment1.Form_ID = Me.Form_ID
        UcAttachment1.BlankAllControls()
    End Sub

    Private Sub frmRequestMaster_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnnew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        End If
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        funReset()
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Sub PostData()
        Try
            If clsCommon.myLen(textRequestCode.Value) <= 0 Then
                Throw New Exception("Please select document no to post")
            End If
            If (myMessages.postConfirm()) Then
                If (clsRequestMaster.PostData(textRequestCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Successfully Posted", Me.Text)
                    LoadData(textRequestCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            isInsideLoadData = True
            funReset()
            Dim obj As clsRequestMaster = clsRequestMaster.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.REQUEST_CODE) > 0) Then
                btnsave.Enabled = True
                btnPost.Enabled = True
                isNewEntry = False
                textRequestCode.Value = obj.REQUEST_CODE
                dtpRequestDate.Value = obj.REQUEST_DATE
                txtRequestBy.Text = obj.REQUEST_BY
                lblRequestBy.Text = clsRequestMaster.getUserName(obj.REQUEST_BY, Nothing)
                txtRequestTo.Text = obj.REQUEST_FOR
                lblRequestTo.Text = clsRequestMaster.getUserName(obj.REQUEST_FOR, Nothing)
                txtScreen.Value = obj.SCREEN_CODE
                lblScreen.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select case when len( isnull (Re_Name,'') ) > 0 then Re_Name else Program_Name end as Program_Name from TSPL_PROGRAM_MASTER where Program_Code =  '" + txtScreen.Value + "' "))
                txtReason.Text = obj.REASON
                txtRemarks.Text = obj.REMARKS
                UsLock1.Status = obj.POSTED
                UsLock2.Status = obj.APPROVED_STATUS
                If obj.POSTED = ERPTransactionStatus.Approved Then
                    btnsave.Enabled = False
                    btnPost.Enabled = False
                    btndelete.Enabled = False
                Else
                    btnsave.Enabled = True
                    btndelete.Enabled = True
                    btnPost.Enabled = True
                End If
                btnsave.Text = "Update"
                UcAttachment1.LoadData(textRequestCode.Value)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    Function AllowToSave() As Boolean
        If clsCommon.myLen(txtReason.Text) <= 0 Then
            myMessages.blankValue("Reason")
            txtReason.Focus()
            Return False
        End If
        If clsCommon.myLen(txtRequestTo.Text) <= 0 Then
            myMessages.blankValue("Request To")
            'clsCommon.MyMessageBoxShow("Reporting User not taged with user.")
            txtRequestTo.Focus()
            Return False
        End If
        If clsCommon.myLen(txtScreen.Value) <= 0 Then
            myMessages.blankValue("Screen")
            'clsCommon.MyMessageBoxShow("Please Select Screen Code first.")
            txtScreen.Focus()
            Return False
        End If
        If UcAttachment1.gv1.Rows.Count <= 0 Then
            myMessages.blankValue("Attachment")
            Return False
        End If
        Return True
    End Function

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(textRequestCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please Select Request Code First")
            Exit Sub
        End If
        funDelete()
    End Sub

    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsRequestMaster.DeleteData(textRequestCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub textRequestCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles textRequestCode._MYValidating
        Dim str As String = "select count(*) from TSPL_USER_REQUEST_MASTER where REQUEST_CODE ='" + textRequestCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            textRequestCode.MyReadOnly = False
        Else
            textRequestCode.MyReadOnly = True
        End If
        If textRequestCode.MyReadOnly OrElse isButtonClicked Then
            textRequestCode.Value = clsRequestMaster.getFinder(" REQUEST_BY= '" + clsCommon.myCstr(objCommonVar.CurrentUserCode) + "' ", textRequestCode.Value, isButtonClicked)
            If textRequestCode.Value <> "" Then
                LoadData(textRequestCode.Value, NavigatorType.Current)
            Else
                funReset()
            End If
        End If
    End Sub

    Private Sub textRequestCode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles textRequestCode.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub textRequestCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles textRequestCode._MYNavigator
        Try
            LoadData(textRequestCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Save()
    End Sub
    Public Sub Save()
        Try
            If (AllowToSave()) Then
                Dim obj As New clsRequestMaster()
                obj.REQUEST_CODE = textRequestCode.Value
                obj.REQUEST_DATE = dtpRequestDate.Value
                obj.REQUEST_BY = txtRequestBy.Text
                obj.REQUEST_FOR = txtRequestTo.Text
                obj.SCREEN_CODE = txtScreen.Value
                obj.REASON = txtReason.Text
                obj.REMARKS = txtRemarks.Text


                If obj.SaveData(obj, isNewEntry) Then
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    UcAttachment1.SaveData(obj.REQUEST_CODE)
                    LoadData(obj.REQUEST_CODE, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub

    Private Sub txtScreen__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtScreen._MYValidating
        Try
            Dim qry As String = "select distinct TSPL_GROUP_PROGRAM_MAPPING.Program_Code as [Code], case when len( isnull (Re_Name,'') ) > 0 then Re_Name else Program_Name end as [Screen Name] from  TSPL_GROUP_PROGRAM_MAPPING 
inner join TSPL_PROGRAM_MASTER on TSPL_PROGRAM_MASTER.Program_Code = TSPL_GROUP_PROGRAM_MAPPING.Program_Code
inner join TSPL_USER_GROUP_MAPPING on TSPL_USER_GROUP_MAPPING.Group_Code = TSPL_GROUP_PROGRAM_MAPPING.Group_Code and TSPL_USER_GROUP_MAPPING.User_Code = '" + clsCommon.myCstr(objCommonVar.CurrentUserCode) + "' and TSPL_GROUP_PROGRAM_MAPPING.Read_Flag = 1 "

            txtScreen.Value = clsCommon.ShowSelectForm("DCS@Dis@Finder", qry, "Code", "", txtScreen.Value, "", isButtonClicked)
            lblScreen.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select case when len( isnull (Re_Name,'') ) > 0 then Re_Name else Program_Name end as Program_Name from TSPL_PROGRAM_MASTER where Program_Code =  '" + txtScreen.Value + "' "))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub
End Class
