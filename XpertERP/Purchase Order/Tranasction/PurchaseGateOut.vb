Imports System.Data.SqlClient
Imports System.IO
Imports common

Public Class PurchaseGateOut
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Private Sub PurchaseGateOut_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New ")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for Post ")
    End Sub
    Public Sub SaveData()
        Try
            Dim obj As New clsGateOutt()
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                isNewEntry = True
            End If
            obj.Code = txtCode.Value
            obj.docDate = txtDate.Value
            obj.GRN_code = txtGRNNo.Value
            obj.Description = txtDescription.Text
            obj.Remarks = txtRemarks.Text
            If (obj.SaveData(obj, isNewEntry)) Then
                common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                LoadData(obj.Code, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        'AddNew()
        Dim obj As New clsGateOutt()
        obj = clsGateOutt.GetData(strCode, NavTyep, Nothing)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
            isNewEntry = False
            txtCode.Value = obj.Code
            txtDate.Value = obj.docDate
            txtGRNNo.Value = obj.GRN_code
            txtRemarks.Text = obj.Remarks
            txtDescription.Text = obj.Description
            UsLock1.Status = obj.Status
            If obj.Status = ERPTransactionStatus.Approved Then
                btnsave.Enabled = False
                btnPost.Enabled = False
                btnDelete.Enabled = False
            Else
                btnsave.Enabled = True
                btnsave.Text = "Update"
                btnPost.Enabled = True
                btnDelete.Enabled = True
            End If
        End If
    End Sub
    Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(txtGRNNo.Value) <= 0 Then
                txtGRNNo.Focus()
                Throw New Exception("Please select GRN No")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function
    Sub AddNew()
        isNewEntry = True
        txtCode.Value = Nothing
        txtCode.Focus()
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btnDelete.Enabled = False
        btnPost.Enabled = False
        txtDate.Text = clsCommon.GETSERVERDATE()
        BlankAllControls()
    End Sub
    Sub BlankAllControls()
        txtCode.Value = ""
        txtGRNNo.Value = ""
        txtDescription.Text = ""
        txtRemarks.Text = ""
        UsLock1.Status = ERPTransactionStatus.Pending
        txtDate.Value = clsCommon.GETSERVERDATE()
    End Sub
    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub
    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub txtGRNNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtGRNNo._MYValidating
        Dim whrcls As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrcls = " TSPL_SRN_HEAD.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ") and "
        End If
        whrcls += " TSPL_SRN_HEAD.Status=1"
        Dim qry As String = "select GRN_No as Code,TSPL_SRN_HEAD.SRN_No,case when TSPL_SRN_HEAD.Status=1 then 'posted'end as [SRN Status],CAST(TSPL_SRN_HEAD.Posting_Date AS date) as [SRN Post Date]   from TSPL_GRN_HEAD inner join TSPL_SRN_HEAD on TSPL_SRN_HEAD.Against_GRN=TSPL_GRN_HEAD.GRN_No"
        txtGRNNo.Value = clsCommon.ShowSelectForm("grnfunder", qry, "Code", whrcls, txtGRNNo.Value, "", isButtonClicked)
    End Sub
    Private Sub txtCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                If (clsGateOutt.PostData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Successfully Posted")
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsGateOutt.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    AddNew()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        funDelete()
    End Sub

    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        txtCode.Value = clsGateOutt.getFinder("", txtCode.Value, isButtonClicked)
        If txtCode.Value <> "" Then
            LoadData(txtCode.Value, NavigatorType.Current)
        Else
            AddNew()
        End If
    End Sub
End Class