'' Created By Anubhooti BM00000006135 
Imports common
Imports System.Data.SqlClient
Imports System
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports Telerik.WinControls

Public Class frmHRReimbursementTypeMaster
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim ComboLoad As Boolean = False
#End Region

#Region "Functions"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmHRReimbursementTypeMaster)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Sub Reset()
        txtCode.Value = ""
        txtDesc.Text = ""
        Me.CmbRType.DataSource = ClsReimbursementTypeMaster.GetRT
        Me.CmbRType.DisplayMember = "Name"
        Me.CmbRType.ValueMember = "Code"
        Me.CmbTrType.DataSource = ClsReimbursementTypeMaster.GetTT
        Me.CmbTrType.DisplayMember = "Name"
        Me.CmbTrType.ValueMember = "Code"
        If clsCommon.CompairString(CmbRType.SelectedValue, "T") = CompairStringResult.Equal Then
            MyLabel2.Visible = True
            CmbTrType.Visible = True
        Else
            MyLabel2.Visible = False
            CmbTrType.Visible = False
            CmbTrType.SelectedValue = 0
        End If
        ComboLoad = True
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
            If clsCommon.myLen(CmbRType.SelectedValue) <= 0 Then
                clsCommon.MyMessageBoxShow("Please select reimbursement type", Me.Text)
                CmbRType.Focus()
                CmbRType.Select()
                Return False
            ElseIf clsCommon.CompairString(CmbRType.SelectedValue, "T") = CompairStringResult.Equal AndAlso clsCommon.CompairString(CmbTrType.SelectedValue, "") = CompairStringResult.Equal Then
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
            Dim obj As New ClsReimbursementTypeMaster()

            obj.Reimbursement_Code = clsCommon.myCstr(txtCode.Value)
            obj.Description = clsCommon.myCstr(txtDesc.Text).Replace("'", "`")
            obj.Reimbursement_Type = clsCommon.myCstr(CmbRType.SelectedValue)

            If clsCommon.CompairString(CmbRType.SelectedValue, "T") = CompairStringResult.Equal Then
                MyLabel2.Visible = True
                CmbTrType.Visible = True
                obj.Travel_Type = clsCommon.myCstr(CmbTrType.SelectedValue)
            Else
                MyLabel2.Visible = False
                CmbTrType.Visible = False
                obj.Travel_Type = ""
            End If

            If ClsReimbursementTypeMaster.SaveData(obj, txtCode.Value) Then
                clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                btnSave.Text = "Update"
                btnDelete.Enabled = True
                txtCode.Value = obj.Reimbursement_Code
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
            Dim obj As New ClsReimbursementTypeMaster()
            txtCode.Value = strCode
            obj = ClsReimbursementTypeMaster.GetData(strCode, NavTyep)

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Reimbursement_Code) > 0 Then
                txtCode.Value = obj.Reimbursement_Code
                txtDesc.Text = obj.Description
                CmbRType.SelectedValue = obj.Reimbursement_Type
                CmbTrType.SelectedValue = obj.Travel_Type

                If clsCommon.CompairString(CmbRType.SelectedValue, "T") = CompairStringResult.Equal Then
                    MyLabel2.Visible = True
                    CmbTrType.Visible = True
                Else
                    MyLabel2.Visible = False
                    CmbTrType.Visible = False
                    CmbTrType.SelectedValue = 0
                End If

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
                If (ClsReimbursementTypeMaster.DeleteData(txtCode.Value)) Then
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
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
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
    Private Sub ReimbursementTypeMaster_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            If AllowToSave() Then
                SaveData()
            End If
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            Me.Close()
            GC.Collect()
        End If
    End Sub
    Private Sub ReimbursementTypeMaster_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetUserMgmtNew()
        Reset()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub
    Private Sub txtCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtCode._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_HR_REIMBURSEMENT_TYPE_MASTER where Reimbursement_Code='" + txtCode.Value + "'"
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
            Dim str As String = "select count(*) from TSPL_HR_REIMBURSEMENT_TYPE_MASTER where Reimbursement_Code ='" + txtCode.Value + "' "
            Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
            If no = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If
            If txtCode.MyReadOnly OrElse isButtonClicked Then

                txtCode.Value = ClsReimbursementTypeMaster.getFinder("", txtCode.Value, isButtonClicked)
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
    Private Sub rmExport_Click(sender As Object, e As EventArgs) Handles rmExport.Click
        Dim str As String
        str = "select Reimbursement_Code AS Code, Description as [Description],Reimbursement_Type As [Reimbursement Type],Travel_Type AS [Travel Type] from TSPL_HR_REIMBURSEMENT_TYPE_MASTER"
        transportSql.ExporttoExcel(str, Me)
    End Sub
    Private Sub rmImport_Click(sender As Object, e As EventArgs) Handles rmImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        Dim linno As Integer = 0

        If transportSql.importExcel(gv, "Code", "Description", "Reimbursement Type", "Travel Type") Then

            Try
                clsCommon.ProgressBarPercentShow()

                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New ClsReimbursementTypeMaster()
                    linno += 1
                    Dim strCode As String = clsCommon.myCstr(grow.Cells("Code").Value.ToString().Replace("'", ""))
                    If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                        Throw New Exception("Code can not be blank or incorrect at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Reimbursement_Code = strCode

                    Dim strName As String = clsCommon.myCstr(grow.Cells("Description").Value.ToString().Replace("'", ""))
                    If (String.IsNullOrEmpty(strName)) Then
                        Throw New Exception("Description can not be blank or incorrect at line no. " + clsCommon.myCstr(linno) + ".")
                    ElseIf strName.Length > 500 Then
                        Throw New Exception("Please check ! Description lenght should be 500 at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Description = strName

                    Dim strReimburseType As String = clsCommon.myCstr(grow.Cells("Reimbursement Type").Value)
                    If clsCommon.myLen(strReimburseType) > 0 Then
                        If (clsCommon.CompairString(strReimburseType, "T") <> CompairStringResult.Equal) AndAlso (clsCommon.CompairString(strReimburseType, "F") <> CompairStringResult.Equal) AndAlso (clsCommon.CompairString(strReimburseType, "C") <> CompairStringResult.Equal) AndAlso (clsCommon.CompairString(strReimburseType, "O") <> CompairStringResult.Equal) AndAlso (clsCommon.CompairString(strReimburseType, "M") <> CompairStringResult.Equal) Then
                            Throw New Exception("Reimbursement Type should be 'T','F','C','O','M' at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    Else
                        Throw New Exception("Reimbursement type can not be blank at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Reimbursement_Type = strReimburseType.ToUpper()

                    Dim strTravelType As String = clsCommon.myCstr(grow.Cells("Travel Type").Value)
                    If clsCommon.CompairString(strReimburseType, "T") = CompairStringResult.Equal Then
                        If clsCommon.myLen(strTravelType) > 0 Then
                            If (clsCommon.CompairString(strTravelType, "D") <> CompairStringResult.Equal) AndAlso (clsCommon.CompairString(strReimburseType, "I") <> CompairStringResult.Equal) Then
                                Throw New Exception("Travel type should be 'D','I' at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                        Else
                            Throw New Exception("Travel type can not be blank at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        obj.Travel_Type = strTravelType.ToUpper()
                    Else
                        obj.Travel_Type = ""
                    End If


                    ClsReimbursementTypeMaster.SaveData(obj, obj.Reimbursement_Code)
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
    Private Sub CmbRType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles CmbRType.SelectedIndexChanged
        If ComboLoad = True Then
            If clsCommon.CompairString(CmbRType.SelectedValue, "T") = CompairStringResult.Equal Then
                MyLabel2.Visible = True
                CmbTrType.Visible = True
                CmbTrType.SelectedValue = ""
            Else
                MyLabel2.Visible = False
                CmbTrType.Visible = False
                CmbTrType.SelectedValue = ""
            End If
        End If
    End Sub
#End Region
End Class
