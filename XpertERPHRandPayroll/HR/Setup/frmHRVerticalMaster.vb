' ----------------- Created By Anubhooti On 14-May-2015 Against -------------------- '
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports common
Imports XpertERPEngine

Public Class FrmHRVerticalMaster
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()

#Region "Functions"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.HRVerticalMaster)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As ClsHRVerticalMaster = ClsHRVerticalMaster.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            isNewEntry = False
            txtcode.Value = obj.Code
            txtdesp.Text = obj.Name
            txtindustrycode.Value = obj.Industry_Code
            If clsCommon.myLen(obj.Industry_Code) > 0 Then
                lblindustry.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Name,'') As Description FROM TSPL_HR_INDUSTRY_TYPE_MASTER WHERE Code ='" & clsCommon.myCstr(obj.Industry_Code) & "'"))
            Else
                lblindustry.Text = ""
            End If
            txtcode.MyReadOnly = True
            btnsave.Text = "Update"
            btndelete.Enabled = True
        End If
    End Sub
    Private Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(clsCommon.myCstr(txtcode.Value)) <= 0 Or clsCommon.myLen(clsCommon.myCstr(txtcode.Value)) > 30 Then
                myMessages.blankValue("Code")

                txtcode.Focus()
                txtcode.Select()
                Errorcontrol.SetError(txtcode, "Code")
                Return False
            Else
                Errorcontrol.ResetError(txtcode)
            End If
            If clsCommon.myLen(clsCommon.myCstr(txtdesp.Text)) <= 0 Then
                myMessages.blankValue("Description")

                txtdesp.Focus()
                txtdesp.Select()
                Errorcontrol.SetError(txtdesp, "Description")
                Return False
            Else
                Errorcontrol.ResetError(txtdesp)
            End If
            If clsCommon.myLen(clsCommon.myCstr(txtindustrycode.Value)) <= 0 Then
                myMessages.blankValue("Industry Code")

                txtindustrycode.Focus()
                txtindustrycode.Select()
                Errorcontrol.SetError(txtindustrycode, "Industry Code")
                Return False
            Else
                Errorcontrol.ResetError(txtindustrycode)
            End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
        Return True
    End Function
    Sub SaveData()
        Try
            btnsave.Focus()
            If AllowToSave() Then
                Dim obj As New ClsHRVerticalMaster()
                obj.Code = txtcode.Value
                obj.Name = txtdesp.Text
                obj.Industry_Code = txtindustrycode.Value

                Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(Code) from TSPL_HR_VERTICAL_MASTER WHERE Code='" + obj.Code + "'")
                If (qry = 0) Then
                    isNewEntry = True
                Else
                    isNewEntry = False
                End If
                If (ClsHRVerticalMaster.SaveData(obj, isNewEntry)) Then
                    clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                    LoadData(obj.Code, NavigatorType.Current)
                    btnsave.Text = "Update"
                    btndelete.Enabled = True
                Else
                    btnsave.Text = "Save"
                    btndelete.Enabled = False
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub DeleteData()
        Try
            If clsCommon.myLen(txtcode.Value) <= 0 Then
                Throw New Exception("Code not found to delete")
            End If
            If clsCommon.MyMessageBoxShow("Are you sure? do you want to delete this Code ('" + txtcode.Value + "')", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then

                Dim qry As String = "DELETE FROM TSPL_HR_VERTICAL_MASTER WHERE Code='" + txtcode.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qry)
                clsCommon.MyMessageBoxShow("Deleted Successfully", Me.Text)
                AddNew()
            End If
        Catch ex As Exception
            If (clsCommon.CompairString(clsCommon.myCstr(ex.Message), "Code not found to delete") <> CompairStringResult.Equal) Then
                clsCommon.MyMessageBoxShow("Current Code is in use")
            Else
                clsCommon.MyMessageBoxShow(ex.Message)
            End If
        End Try
    End Sub
    Sub AddNew()
        txtcode.Value = ""
        txtdesp.Text = ""
        txtindustrycode.Value = ""
        lblindustry.Text = ""
        txtcode.MyReadOnly = False
        btnsave.Text = "Save"
        btndelete.Enabled = False
    End Sub
#End Region

#Region "Events"
    Private Sub FrmHRVerticalMaster_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
            GC.Collect()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            AddNew()
        End If
    End Sub
    Private Sub FrmHRVerticalMaster_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetUserMgmtNew()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
        AddNew()
    End Sub
    Private Sub txtcode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtcode._MYNavigator
        Try
            LoadData(txtcode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub txtcode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtcode._MYValidating
        Dim str As String = "select count(*) from TSPL_HR_VERTICAL_MASTER where Code ='" + txtcode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtcode.MyReadOnly = False
        Else
            txtcode.MyReadOnly = True
        End If

        If txtcode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = ""
            qry = "Select Code As [Code],Name As [Name] from TSPL_HR_VERTICAL_MASTER"
            txtcode.Value = clsCommon.ShowSelectForm("TSPL_HR_VERTICAL_MASTER", qry, "Code", "", txtcode.Value, "TSPL_HR_VERTICAL_MASTER.Code", isButtonClicked)
            If clsCommon.myLen(txtcode.Value) > 0 Then
                Dim objOT As ClsHRVerticalMaster
                objOT = ClsHRVerticalMaster.GetData(txtcode.Value, NavigatorType.Current)
                If Not objOT Is Nothing Then
                    LoadData(txtcode.Value, NavigatorType.Current)
                End If
            Else
                AddNew()
            End If
        End If
    End Sub
    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        AddNew()
    End Sub
    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
    End Sub
    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
        GC.Collect()
    End Sub
    Private Sub rmExport_Click(sender As Object, e As EventArgs) Handles rmExport.Click
        Dim str As String
        str = "SELECT Code AS [Code],Name As [Description],Industry_Code AS [Industry Code],TSPL_HR_INDUSTRY_TYPE_MASTER.Name AS [Industry Name] FROM TSPL_HR_VERTICAL_MASTER LEFT OUTER JOIN TSPL_HR_INDUSTRY_TYPE_MASTER ON TSPL_HR_INDUSTRY_TYPE_MASTER.Code = TSPL_HR_VERTICAL_MASTER.Code"
        transportSql.ExporttoExcel(str, Me)
    End Sub
    Private Sub rmImport_Click(sender As Object, e As EventArgs) Handles rmImport.Click
        Dim gv As New RadGridView()
        Dim IsNewEntry As Boolean
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Code", "Description", "Industry Code") Then
            Dim linno As Integer = 1
            Try
                For Each grow As GridViewRowInfo In gv.Rows

                    Dim obj As New ClsHRVerticalMaster()
                    Dim strCode As String = clsCommon.myCstr(grow.Cells("Code").Value)
                    Dim strDescription As String = clsCommon.myCstr(grow.Cells("Description").Value)
                    linno += 1
                    If clsCommon.myLen(strCode) <= 0 Then
                        Throw New Exception("Code should not be left blankat line no. " + clsCommon.myCstr(linno) + ".")
                    ElseIf clsCommon.myLen(strCode) > 30 Then
                        Throw New Exception("Please check ! length of code should be 30 at line no. " + clsCommon.myCstr(linno) + ".")
                    End If

                    If clsCommon.myLen(strDescription) <= 0 Then
                        Throw New Exception("Description should not be left blank at line no. " + clsCommon.myCstr(linno) + ".")
                    ElseIf clsCommon.myLen(strDescription) > 150 Then
                        Throw New Exception("Please check ! length of description should be 150 at line no. " + clsCommon.myCstr(linno) + ".")
                    End If

                    Dim strIndusCode As String = clsCommon.myCstr(grow.Cells("Industry Code").Value.ToString().Replace("'", ""))
                    If (String.IsNullOrEmpty(strIndusCode)) Then
                        Throw New Exception("Industry Code can not be blank at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    Dim sQuery As String = "select * from  TSPL_HR_INDUSTRY_TYPE_MASTER where CODE='" + strIndusCode + "'"
                    Dim DTEmp As DataTable = clsDBFuncationality.GetDataTable(sQuery)
                    If DTEmp.Rows.Count <= 0 Then
                        Throw New Exception("Please check ! Industry code '" + strIndusCode + "' dose not exits in HR industry type master.")
                    End If
                    obj.Industry_Code = strIndusCode

                    If clsCommon.myLen(strCode) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from TSPL_HR_VERTICAL_MASTER where Code='" + strCode + "' ") > 0 Then
                        IsNewEntry = False
                    Else
                        IsNewEntry = True

                    End If
                    obj.Code = strCode
                    obj.Name = strDescription
                    ClsHRVerticalMaster.SaveData(obj, IsNewEntry)

                Next
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub
    Private Sub txtindustrycode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtindustrycode._MYValidating
        txtindustrycode.Value = ClsHRIndustryType.getFinder("", txtindustrycode.Value.Replace("'", ""), isButtonClicked)
        If clsCommon.myLen(txtindustrycode.Value) > 0 Then
            lblindustry.Text = clsDBFuncationality.getSingleValue("SELECT ISNULL(Name,'') AS Name FROM TSPL_HR_INDUSTRY_TYPE_MASTER WHERE Code='" + txtindustrycode.Value + "'")
        Else
            lblindustry.Text = ""
        End If
    End Sub
#End Region

End Class
