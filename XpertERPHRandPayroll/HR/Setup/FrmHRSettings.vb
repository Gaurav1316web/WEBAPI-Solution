Imports common
Imports System.Data.SqlClient
Imports common.Controls
Imports XpertERPEngine

Public Class FrmHRSettings
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmHRSettings)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
    End Sub
    Sub Reset()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        dtpFromDate.Value = dtpToDate.Value.AddMonths(-1)
        rbnSingleP.IsChecked = True
    End Sub
    Private Function AllowToSave(ByVal trans As SqlTransaction) As Boolean
        Dim FromDate As Date = Nothing
        Dim ToDate As Date = Nothing
        Dim DateDiff As Date = Nothing
        Dim Months As Integer = 0

        If clsCommon.myLen(dtpFromDate.Text) <= 0 Then
            dtpFromDate.Focus()
            dtpFromDate.Select()
            Throw New Exception("Please Fill From Date")
            'Return False
        End If
        If clsCommon.myLen(dtpToDate.Text) <= 0 Then
            dtpToDate.Focus()
            dtpToDate.Select()
            Throw New Exception("Please Fill To Date")
            ' Return False
        End If
        If rbnSingleP.IsChecked = False AndAlso rbnDoubleP.IsChecked = False Then
            rbnSingleP.Focus()
            rbnSingleP.Select()
            Throw New Exception("Please select one parameter.")
            '  Return False
        End If
        FromDate = clsCommon.myCDate(dtpFromDate.Value)
        ToDate = clsCommon.myCDate(dtpToDate.Value)
        'DateDiff = ToDate - FromDate
        Months = ToDate.Month - FromDate.Month
        If ToDate < FromDate = True Then
            dtpFromDate.Focus()
            dtpFromDate.Select()
            Throw New Exception("Please check To Date Should be Greater Than from From Date")
            'Return False
        End If
        
        If Months > 1 OrElse Months < 0 Then
            dtpFromDate.Focus()
            dtpFromDate.Select()
            Throw New Exception("Please check Date difference between From Date and To Date should be one month.")
            'Return False
        End If
        Return True
    End Function
    Sub SaveData()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If AllowToSave(trans) Then
                Dim obj As New ClsHRSettings()
              
                obj.From_Date = clsCommon.myCDate(dtpFromDate.Value)
                obj.To_Date = clsCommon.myCDate(dtpToDate.Value)
                If rbnSingleP.IsChecked = True Then
                    obj.Single_Parameter = 1
                    obj.Double_Parameter = 0
                ElseIf rbnDoubleP.IsChecked = True Then
                    obj.Double_Parameter = 1
                    obj.Single_Parameter = 0
                End If
                If (ClsHRSettings.SaveData(obj, trans)) Then
                    trans.Commit()
                    clsCommon.MyMessageBoxShow(Me, "Data saved Successfully", Me.Text)
                End If
            End If


        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadData()

        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As ClsHRSettings = ClsHRSettings.GetData()
            If obj IsNot Nothing Then
                isNewEntry = False
                
                dtpFromDate.Value = clsCommon.myCDate(obj.From_Date)
                dtpToDate.Value = clsCommon.myCDate(obj.To_Date)
                If clsCommon.CompairString(obj.Single_Parameter, "1") = CompairStringResult.Equal Then
                    rbnSingleP.IsChecked = True
                    rbnDoubleP.IsChecked = False
                ElseIf clsCommon.CompairString(obj.Double_Parameter, "1") = CompairStringResult.Equal Then
                    rbnDoubleP.IsChecked = True
                    rbnSingleP.IsChecked = False
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub FrmHRSettings_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub

    Private Sub FrmHRSettings_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetUserMgmtNew()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        LoadData()
    End Sub
End Class
