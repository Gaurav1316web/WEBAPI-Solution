Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class FrmCrptFooter

#Region "Variable"
    Public strFormId As String

#End Region

    Private Sub FrmCrptFooter_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadData("")
    End Sub

    Public Sub Save()
        If AllowToSave() Then
            Dim obj As New clsCrptFooterSetting()
            obj.Frm_ID = strFormId
            'Server.HtmlDecode(strValue)
            obj.Footer_Text = txtBody.Rtf
            If (obj.SaveData(obj)) Then
                common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
            End If

        End If

    End Sub
    Function AllowToSave() As Boolean
        If clsCommon.myLen(txtBody.Text) <= 0 Then
            myMessages.blankValue(Me, "Body", Me.Text)
            txtBody.Focus()
            Return False
        End If
        Return True
    End Function


    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Save()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Sub LoadData()
        Dim obj As New clsCrptFooterSetting()
        obj = clsCrptFooterSetting.GetData("", strFormId)
        If obj IsNot Nothing Then
            txtBody.Rtf = obj.Footer_Text
        End If
    End Sub

    Sub LoadData(ByVal strCode As String)
        LoadData()
    End Sub

End Class
