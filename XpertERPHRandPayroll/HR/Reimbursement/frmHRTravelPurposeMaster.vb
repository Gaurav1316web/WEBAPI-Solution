'' Created By Anubhooti BM00000006138 
Imports common
Imports System.Data.SqlClient
Imports System
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class FrmHRTravelPurposeMaster
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
#End Region

#Region "Functions"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmHRTravelPurposeMaster)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Sub Reset()
        txtCode.Value = ""
        txtDesc.Text = ""

        Me.CmbTrType.DataSource = ClsHRTravelPurposeMaster.GetTT
        Me.CmbTrType.DisplayMember = "Name"
        Me.CmbTrType.ValueMember = "Code"

        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = False
        txtCode.MyReadOnly = False

        txtCode.Focus()
        txtCode.Select()
    End Sub
    Function AllowToSave() As Boolean
        Try
            btnSave.Focus()
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please fill Code", Me.Text)
                txtCode.Focus()
                txtCode.Select()
                Return False
            End If
            If clsCommon.CompairString(CmbTrType.SelectedValue, "") = CompairStringResult.Equal Then
                clsCommon.MyMessageBoxShow("Please select travel type", Me.Text)
                CmbTrType.Focus()
                CmbTrType.Select()
                Return False
            End If
            If clsCommon.myLen(txtDesc.Text) <= 0 Then
                clsCommon.MyMessageBoxShow("Please fill description", Me.Text)
                txtDesc.Focus()
                txtDesc.Select()
                Return False
            End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return True
    End Function
    Sub SaveData()
        Try
            Dim obj As New ClsHRTravelPurposeMaster()

            obj.Travel_Code = clsCommon.myCstr(txtCode.Value)
            obj.Travel_Desp = clsCommon.myCstr(txtDesc.Text).Replace("'", "`")
            obj.Travel_Type = clsCommon.myCstr(CmbTrType.SelectedValue)

            If ClsHRTravelPurposeMaster.SaveData(obj, txtCode.Value) Then
                clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                btnSave.Text = "Update"
                btnDelete.Enabled = True
                LoadData(obj.Travel_Code, NavigatorType.Current)
                txtCode.MyReadOnly = True
            Else
                txtCode.MyReadOnly = False
                btnSave.Text = "Save"
                btnDelete.Enabled = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Public Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            Dim obj As New ClsHRTravelPurposeMaster()
            txtCode.Value = strCode
            obj = ClsHRTravelPurposeMaster.GetData(strCode, NavTyep)

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Travel_Code) > 0 Then
                txtCode.Value = obj.Travel_Code
                txtDesc.Text = obj.Travel_Desp
                CmbTrType.SelectedValue = obj.Travel_Type

                txtCode.MyReadOnly = True
                btnSave.Text = "Update"
                btnDelete.Enabled = True
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Code not found to delete")
            Exit Sub
        End If
        funDelete()
    End Sub
    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (ClsHRTravelPurposeMaster.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    Reset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
#End Region
#Region "Events"
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
        Me.Close()
        GC.Collect()
    End Sub
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Reset()
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If AllowToSave() Then
            SaveData()
        End If
    End Sub
    Private Sub rmExport_Click(sender As Object, e As EventArgs) Handles rmExport.Click
        Dim str As String
        str = "select TSPL_HR_TRAVEL_PURPOSE_MASTER.Travel_Code AS Code, TSPL_HR_TRAVEL_PURPOSE_MASTER.Travel_Desp AS [Travel Desp],TSPL_HR_TRAVEL_PURPOSE_MASTER.Travel_Type AS [Travel Type] FROM TSPL_HR_TRAVEL_PURPOSE_MASTER"
        transportSql.ExporttoExcel(str, Me)
    End Sub
    Private Sub rmImport_Click(sender As Object, e As EventArgs) Handles rmImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        Dim linno As Integer = 0

        If transportSql.importExcel(gv, "Code", "Travel Desp", "Travel Type") Then

            Try
                clsCommon.ProgressBarPercentShow()

                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New ClsHRTravelPurposeMaster()
                    linno += 1
                    Dim strCode As String = clsCommon.myCstr(grow.Cells("Code").Value.ToString().Replace("'", ""))
                    If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                        Throw New Exception("Code can not be blank or incorrect at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Travel_Code = strCode

                    Dim strName As String = clsCommon.myCstr(grow.Cells("Travel Desp").Value.ToString().Replace("'", ""))
                    If (String.IsNullOrEmpty(strName)) Then
                        Throw New Exception("Travel desp can not be blank or incorrect at line no. " + clsCommon.myCstr(linno) + ".")
                    ElseIf strName.Length > 150 Then
                        Throw New Exception("Please check ! Travel desp lenght should be 150 at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Travel_Desp = strName

                    Dim strTravelType As String = clsCommon.myCstr(grow.Cells("Travel Type").Value)
                    If clsCommon.myLen(strTravelType) > 0 Then
                        If (clsCommon.CompairString(strTravelType, "D") <> CompairStringResult.Equal) AndAlso (clsCommon.CompairString(strTravelType, "I") <> CompairStringResult.Equal) Then
                            Throw New Exception("Travel type should be 'D','I' at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    Else
                        Throw New Exception("Travel type can not be blank at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Travel_Type = strTravelType.ToUpper()

                    ClsHRTravelPurposeMaster.SaveData(obj, obj.Travel_Code)
                Next
                clsCommon.ProgressBarPercentHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarPercentHide()
                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub
    Private Sub FrmHRTravelPurposeMaster_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            If AllowToSave() Then
                SaveData()
            End If
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso BtnClose.Enabled Then
            Me.Close()
            GC.Collect()
        End If
    End Sub
    Private Sub FrmHRTravelPurposeMaster_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetUserMgmtNew()
        Reset()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(BtnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub
    Private Sub txtCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtCode._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_HR_TRAVEL_PURPOSE_MASTER where Travel_Code='" + txtCode.Value + "'"
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If check > 0 Then
                txtCode.MyReadOnly = True
            ElseIf check <= 0 Then
                txtCode.MyReadOnly = False
            End If
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        Try
            Dim str As String = "select count(*) from TSPL_HR_TRAVEL_PURPOSE_MASTER where Travel_Code ='" + txtCode.Value + "' "
            Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
            If no = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If
            If txtCode.MyReadOnly OrElse isButtonClicked Then

                txtCode.Value = ClsHRTravelPurposeMaster.GetFinder("", txtCode.Value, isButtonClicked)
                If clsCommon.myLen(txtCode.Value) > 0 Then
                    btnDelete.Enabled = True
                    btnSave.Text = "Update"
                    txtCode.MyReadOnly = True
                Else
                    btnSave.Text = "Save"
                    btnDelete.Enabled = False
                    txtCode.MyReadOnly = False
                End If
            End If
            LoadData(txtCode.Value, NavigatorType.Current)
        Catch ex As Exception
        End Try
    End Sub
#End Region
End Class
