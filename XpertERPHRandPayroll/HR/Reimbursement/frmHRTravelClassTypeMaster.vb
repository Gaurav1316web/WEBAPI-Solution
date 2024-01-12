'' Created By Anubhooti BM00000006160 
Imports common
Imports System.Data.SqlClient
Imports System
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class FrmHRTravelClassTypeMaster
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
#End Region
#Region "Functions"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmHRTravelClassTypeMaster)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Sub Reset()
        txtCode.Value = ""
        txtDesc.Text = ""
        txttravelmode.Value = ""
        LblTravelMode.Text = ""

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
                clsCommon.MyMessageBoxShow(Me, "Please fill Code", Me.Text)
                txtCode.Focus()
                txtCode.Select()
                Return False
            End If
            If clsCommon.myLen(txtDesc.Text) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please fill description", Me.Text)
                txtDesc.Focus()
                txtDesc.Select()
                Return False
            End If
            If clsCommon.myLen(txttravelmode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please fill travel mode", Me.Text)
                txttravelmode.Focus()
                txttravelmode.Select()
                Return False
            End If

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return True
    End Function
    Sub SaveData()
        Try
            Dim obj As New ClsHRTravelClassTypeMaster()

            obj.Travel_Class_Code = clsCommon.myCstr(txtCode.Value)
            obj.Desp = clsCommon.myCstr(txtDesc.Text).Replace("'", "`")
            obj.Travel_Mode_Code = clsCommon.myCstr(txttravelmode.Value)

            If ClsHRTravelClassTypeMaster.SaveData(obj, txtCode.Value) Then
                clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                btnSave.Text = "Update"
                btnDelete.Enabled = True
                LoadData(obj.Travel_Class_Code, NavigatorType.Current)
                txtCode.MyReadOnly = True
            Else
                txtCode.MyReadOnly = False
                btnSave.Text = "Save"
                btnDelete.Enabled = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            Dim obj As New ClsHRTravelClassTypeMaster()
            txtCode.Value = strCode
            obj = ClsHRTravelClassTypeMaster.GetData(strCode, NavTyep)

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Travel_Class_Code) > 0 Then
                txtCode.Value = clsCommon.myCstr(obj.Travel_Class_Code)
                txtDesc.Text = clsCommon.myCstr(obj.Desp)
                txttravelmode.Value = clsCommon.myCstr(obj.Travel_Mode_Code)

                If clsCommon.myLen(clsCommon.myCstr(obj.Travel_Mode_Code)) > 0 Then
                    LblTravelMode.Text = clsDBFuncationality.getSingleValue("SELECT ISNULL(Description,'') AS [Description] FROM TSPL_HR_TRAVEL_MODE_TYPE_MASTER WHERE Travel_Mode_Code ='" + clsCommon.myCstr(obj.Travel_Mode_Code) + "'")
                Else
                    LblTravelMode.Text = ""
                End If

                txtCode.MyReadOnly = True
                btnSave.Text = "Update"
                btnDelete.Enabled = True
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
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
                If (ClsHRTravelClassTypeMaster.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully.", Me.Text)
                    Reset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
#End Region
#Region "Events"
    Private Sub txtCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtCode._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_HR_TRAVEL_CLASS_TYPE_MASTER where Travel_Class_Code='" + txtCode.Value + "'"
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If check > 0 Then
                txtCode.MyReadOnly = True
            ElseIf check <= 0 Then
                txtCode.MyReadOnly = False
            End If
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        Try
            Dim str As String = "select count(*) from TSPL_HR_TRAVEL_CLASS_TYPE_MASTER where Travel_Class_Code ='" + txtCode.Value + "' "
            Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
            If no = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If
            If txtCode.MyReadOnly OrElse isButtonClicked Then

                txtCode.Value = ClsHRTravelClassTypeMaster.GetFinder("", txtCode.Value, isButtonClicked)
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
    Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
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
    Private Sub FrmHRTravelClassTypeMaster_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
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
    Private Sub FrmHRTravelClassTypeMaster_Load(sender As Object, e As EventArgs) Handles Me.Load
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
    Private Sub txttravelmode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txttravelmode._MYValidating
        txttravelmode.Value = ClsHRTravelModeTypeMaster.GetFinder("", txttravelmode.Value.Replace("'", ""), isButtonClicked)
        If clsCommon.myLen(txttravelmode.Value) > 0 Then
            LblTravelMode.Text = clsDBFuncationality.getSingleValue("SELECT ISNULL(Description,'') AS Description FROM TSPL_HR_TRAVEL_MODE_TYPE_MASTER WHERE Travel_Mode_Code='" + txttravelmode.Value + "'")
        Else
            LblTravelMode.Text = ""
        End If
    End Sub
    Private Sub rmImport_Click(sender As Object, e As EventArgs) Handles rmImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        Dim linno As Integer = 0

        If transportSql.importExcel(gv, "Code", "Description", "Travel Mode Code") Then
            Try
                clsCommon.ProgressBarPercentShow()

                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New ClsHRTravelClassTypeMaster()
                    linno += 1
                    Dim strCode As String = clsCommon.myCstr(grow.Cells("Code").Value.ToString().Replace("'", ""))
                    If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                        Throw New Exception("Code can not be blank or incorrect at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Travel_Class_Code = strCode

                    Dim strRemarks As String = clsCommon.myCstr(grow.Cells("Description").Value.ToString().Replace("'", ""))
                    If (String.IsNullOrEmpty(strRemarks)) Then
                        Throw New Exception("Description can not be blank at line no. " + clsCommon.myCstr(linno) + ".")
                    ElseIf strRemarks.Length > 150 Then
                        Throw New Exception("Please check ! Description lenght should be 150 at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Desp = strRemarks

                    Dim strTMCode As String = clsCommon.myCstr(grow.Cells("Travel Mode Code").Value.ToString().Replace("'", ""))
                    If (String.IsNullOrEmpty(strTMCode)) Then
                        Throw New Exception("Travel mode code can not be blank at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    Dim TQry As String = "select * from  TSPL_HR_TRAVEL_MODE_TYPE_MASTER where Travel_Mode_Code='" + strTMCode + "'"
                    Dim DTTM As DataTable = clsDBFuncationality.GetDataTable(TQry)
                    If DTTM.Rows.Count <= 0 Then
                        Throw New Exception("Please check travel mode code '" + strTMCode + "' .It dose not exits in travel mode master at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Travel_Mode_Code = strTMCode

                    ClsHRTravelClassTypeMaster.SaveData(obj, obj.Travel_Class_Code)
                Next
                clsCommon.ProgressBarPercentHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarPercentHide()
                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub
    Private Sub rmExport_Click(sender As Object, e As EventArgs) Handles rmExport.Click
        Dim str As String
        str = "SELECT TSPL_HR_TRAVEL_CLASS_TYPE_MASTER.Travel_Class_Code as [Code],TSPL_HR_TRAVEL_CLASS_TYPE_MASTER.Description as [Description],TSPL_HR_TRAVEL_CLASS_TYPE_MASTER.Travel_Mode_Code AS [Travel Mode Code] From TSPL_HR_TRAVEL_CLASS_TYPE_MASTER  " & _
        " LEFT OUTER JOIN TSPL_HR_TRAVEL_MODE_TYPE_MASTER ON TSPL_HR_TRAVEL_MODE_TYPE_MASTER.Travel_Mode_Code=TSPL_HR_TRAVEL_CLASS_TYPE_MASTER.Travel_Mode_Code "
        transportSql.ExporttoExcel(str, Me)
    End Sub
#End Region
End Class
