Imports common
Imports System.Data.SqlClient
Imports System.Data
Public Class frmSaveCrystalReportAction
    Inherits FrmMainTranScreen
#Region "Variables"
    Public ActionType As Boolean = False
    Public Report_ID As String = Nothing
    Public CrystalReportName As String = Nothing
#End Region
    Private Sub frmSaveCrystalReportAction_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = clsDBFuncationality.getSingleValue("select case When LEN(isnull(Re_Name,''))>0 then Re_Name else Program_Name end as Name from tspl_Program_Master where Program_Code= '" + Report_ID + "'")
        lblFormId.Text = Report_ID
        lblCrystalReportName.Text = CrystalReportName
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            SaveData()
        Catch err As Exception
            clsCommon.MyMessageBoxShow(Me, err.Message, Me.Text)
        End Try
    End Sub

    Public Function SaveData() As Boolean
        Dim isSaved As Boolean
        Dim obj As New clsCrystalReportActionType()
            obj.Arr = New List(Of clsCrystalReportActionType)()
            Dim objTr As New clsCrystalReportActionType()
            objTr.Form_ID = Report_ID
        objTr.Report_Name = CrystalReportName
        objTr.Action_Type = ActionType
        obj.Arr.Add(objTr)
        If obj.SaveData(obj, True) Then
            clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
            CloseForm()
        End If
        Return isSaved
    End Function
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub

    Sub CloseForm()
        Me.Close()
    End Sub
    Private Sub frmSaveCrystalReportAction_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F5 Then
            SaveData()
        ElseIf e.KeyCode = Keys.Escape Then
            CloseForm()
        End If
    End Sub
    Private Sub rbtnPdf_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnPdf.CheckedChanged, rbtnView.CheckedChanged
        If rbtnView.Checked Then
            ActionType = False
        ElseIf rbtnPdf.Checked Then
            ActionType = True
        End If
    End Sub
End Class