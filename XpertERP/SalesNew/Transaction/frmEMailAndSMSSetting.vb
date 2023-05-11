Imports common
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.IO

Public Class frmEMailAndSMSSetting
    Public Const CustomerName As String = "$CUSTOMERNAME$"
    Public Const InvoiceNo As String = "$INVOICENO$"
    Public Const InvoiceDate As String = "$INVOICEDATE$"
    Public Const DocumentAmount As String = "$DOCUMENTAMOUNT$"
    Public Const Remarks As String = "$REMARKS$"
    Public Const Branch As String = "$BRANCH$"
    Public Const strCompanyName As String = "$COMPANYNAME$"

    Private isConfigPwdEntered As Boolean = False



    Private Sub frmEMailAndSMSSetting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        RadPageView1.SelectedPage = RadPageViewPage2

        ContextMenuStrip1.Items.Add(CustomerName)
        ContextMenuStrip1.Items.Add(InvoiceNo)
        ContextMenuStrip1.Items.Add(InvoiceDate)
        ContextMenuStrip1.Items.Add(DocumentAmount)
        ContextMenuStrip1.Items.Add(Remarks)
        ContextMenuStrip1.Items.Add(Branch)
        ContextMenuStrip1.Items.Add(strCompanyName)


        Dim obj As clsEmailAndSMSConfig = clsEmailAndSMSConfig.GetData()
        If obj IsNot Nothing Then
            txtMailSMTPClient.Text = obj.EMail_SMTP_Client
            txtMailPort.Text = obj.EMail_Port
            txtMailID.Text = obj.EMail_ID
            txtMailPwd.Text = obj.EMail_Pwd
            chkMailEnableSSL.Checked = obj.EMail_Enabel_SSL
            txtEmailText.Text = obj.EMail_Text
            txtSMSString.Text = obj.SMS_String
            txtSMSUserName.Text = obj.SMS_User_Name
            txtSMSPWD.Text = obj.SMS_Pwd
            txtSMSSenderName.Text = obj.SMS_Sender_Name
            txtSMSMobileNo.Text = obj.SMS_Mobile_no
            txtSMSText.Text = obj.SMS_Text
            txtSubject.Text = obj.EMail_Subject
        End If


    End Sub

    Private Sub btnSaveConfiguration_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveConfiguration.Click
        If AllowToSave(True, True, False) Then
            Try
                Dim obj As New clsEmailAndSMSConfig()
                obj.EMail_SMTP_Client = txtMailSMTPClient.Text
                obj.EMail_Port = txtMailPort.Text
                obj.EMail_ID = txtMailID.Text
                obj.EMail_Pwd = txtMailPwd.Text
                obj.EMail_Enabel_SSL = chkMailEnableSSL.Checked
                obj.EMail_Text = txtEmailText.Text
                obj.SMS_String = txtSMSString.Text
                obj.SMS_User_Name = txtSMSUserName.Text
                obj.SMS_Pwd = txtSMSPWD.Text
                obj.SMS_Sender_Name = txtSMSSenderName.Text
                obj.SMS_Mobile_no = txtSMSMobileNo.Text
                obj.SMS_Text = txtSMSText.Text
                obj.EMail_Subject = txtSubject.Text
                If obj.SaveData(obj) Then
                    clsCommon.MyMessageBoxShow("Data Saved successfully", Me.Text)
                End If
            Catch ex As Exception
                clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
            End Try
        End If
    End Sub

    Private Sub ContextMenuStrip1_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ContextMenuStrip1.ItemClicked

        If RadPageView1.SelectedPage Is RadPageViewPage2 Then
            txtSMSText.Text = txtSMSText.Text.Insert(txtSMSText.SelectionStart, " " + e.ClickedItem.Text)
        ElseIf RadPageView1.SelectedPage Is RadPageViewPage1 Then
            txtEmailText.Text = txtEmailText.Text.Insert(txtEmailText.SelectionStart, " " + e.ClickedItem.Text)
        End If
    End Sub

    Private Sub RadPageView1_SelectedPageChanging(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.RadPageViewCancelEventArgs) Handles RadPageView1.SelectedPageChanging
        If e.Page Is RadPageViewPage3 AndAlso Not isConfigPwdEntered Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "SIRC"
            frm.strCode = "SIReversAndCreate"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                isConfigPwdEntered = True
            Else
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub btnSendTestEMail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendTestEMail.Click
        Try
            If AllowToSave(True, False, True) Then
                If clsCommon.myLen(txtEmailTo.Text) <= 0 Then
                    clsCommon.MyMessageBoxShow("Please Enter Email ID (To)")
                    txtEmailTo.Focus()
                    Exit Sub
                End If

                Dim MailMsg As New MailMessage()
                MailMsg.Subject = txtSubject.Text
                MailMsg.From = New MailAddress(txtMailID.Text)
                MailMsg.To.Add(txtEmailTo.Text)
                MailMsg.Body = txtEmailText.Text
                MailMsg.Priority = MailPriority.High
                MailMsg.IsBodyHtml = False

                'Dim MsgAttach As New Attachment(Application.StartupPath() + "\Tecxpert\transportmaster.pdf")
                'MailMsg.Attachments.Add(MsgAttach)
                Dim SmtpMail As New SmtpClient(txtMailSMTPClient.Text)
                SmtpMail.Port = clsCommon.myCdbl(txtMailPort.Text)
                SmtpMail.Credentials = New System.Net.NetworkCredential(txtMailID.Text, txtMailPwd.Text)
                SmtpMail.EnableSsl = chkMailEnableSSL.Checked
                SmtpMail.Send(MailMsg)

                clsCommon.MyMessageBoxShow("Successfully send the Test Email", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try

    End Sub

    Private Function AllowToSave(ByVal ForEMail As Boolean, ByVal ForSMS As Boolean, ByVal CheckConnection As Boolean) As Boolean

        If ForEMail Then
            If clsCommon.myLen(txtMailSMTPClient.Text) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Enter SMTP Client")
                txtMailSMTPClient.Focus()
                Return False
            End If

            If clsCommon.myLen(txtMailPort.Text) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Enter Port")
                txtMailPort.Focus()
                Return False
            End If

            If clsCommon.myLen(txtMailID.Text) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Enter Email ID (From)")
                txtMailID.Focus()
                Return False
            End If

            If clsCommon.myLen(txtMailPwd.Text) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Enter Email Password")
                txtMailPwd.Focus()
                Return False
            End If
            If clsCommon.myLen(txtEmailText.Text) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Enter Email Text")
                txtEmailText.Focus()
                Return False
            End If
        End If

        If ForSMS Then
            If clsCommon.myLen(txtSMSString.Text) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Enter SMS String")
                txtSMSString.Focus()
                Return False
            End If

            If clsCommon.myLen(txtSMSUserName.Text) <= 0 Then
                clsCommon.MyMessageBoxShow("Please SMS User Name")
                txtSMSUserName.Focus()
                Return False
            End If

            If clsCommon.myLen(txtSMSPWD.Text) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Enter SMS Password")
                txtSMSPWD.Focus()
                Return False
            End If

            If clsCommon.myLen(txtSMSMobileNo.Text) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Enter SMS Mobile No")
                txtSMSMobileNo.Focus()
                Return False
            End If
        End If

        If clsCommon.myLen(txtSMSText.Text) <= 0 Then
            clsCommon.MyMessageBoxShow("Please Enter SMS Text")
            txtSMSText.Focus()
            Return False
        End If

        If CheckConnection Then
            If Not clsCommon.myInternetWork() Then
                clsCommon.MyMessageBoxShow("Internet is Not Working properly")
                Return False
            End If
        End If

        Return True
    End Function

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        Try
            If AllowToSave(False, True, True) Then
                Dim client As New System.Net.WebClient()
                Dim baseurl As String = txtSMSString.Text + "?username=" + txtSMSUserName.Text + "&password=" + txtSMSPWD.Text + "&sendername=" + txtSMSSenderName.Text + "&mobileno=91" + txtSMSMobileNo.Text + "&message=" + txtSMSText.Text
                Dim data As Stream = client.OpenRead(baseurl)
                Dim reader As StreamReader = New StreamReader(data)
                Dim s As String = reader.ReadToEnd()
                data.Close()
                reader.Close()

                clsCommon.MyMessageBoxShow("Successfully send the Test SMS", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class
