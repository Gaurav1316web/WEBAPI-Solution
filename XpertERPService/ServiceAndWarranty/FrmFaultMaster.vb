' ----------------- Created By Anubhooti On 27-Aug-2015 Against BM00000007489-------------------- '
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

Public Class FrmFaultMaster
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()

#Region "Fucntions"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmFaultMaster)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub
    Sub AddNew()
        TxtCode.Value = ""
        TxtDesp.Text = ""
        FndFaultCat.Value = ""
        LblFaultCat.Text = ""
        TxtCode.MyReadOnly = False
        TxtCode.Focus()
        btnsave.Text = "Save"
        btndelete.Enabled = False
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As ClsFaultMaster = ClsFaultMaster.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            AddNew()
            isNewEntry = False
            TxtCode.Value = obj.Fault_Master_Code
            TxtDesp.Text = obj.Fault_Master_Name
            FndFaultCat.Value = obj.Fault_Category_Code

            If clsCommon.myLen(clsCommon.myCstr(obj.Fault_Category_Code)) > 0 Then
                LblFaultCat.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Fault_Category_Name FROM TSPL_SW_FAULT_CATEGORY_MASTER WHERE Fault_Category_Code='" & clsCommon.myCstr(obj.Fault_Category_Code) & "'"))
            Else
                LblFaultCat.Text = ""
            End If

            TxtCode.MyReadOnly = True
            btnsave.Text = "Update"
            btndelete.Enabled = True
        End If
    End Sub
    Private Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(clsCommon.myCstr(TxtCode.Value)) <= 0 Or clsCommon.myLen(clsCommon.myCstr(TxtCode.Value)) > 30 Then
                myMessages.blankValue(Me, "Code", Me.Text)
                TxtCode.Focus()
                TxtCode.Select()
                Errorcontrol.SetError(TxtCode, "Code")
                Return False
            Else
                Errorcontrol.ResetError(TxtCode)
            End If
            If clsCommon.myLen(clsCommon.myCstr(TxtDesp.Text)) <= 0 Or clsCommon.myLen(clsCommon.myCstr(TxtDesp.Text)) > 150 Then
                myMessages.blankValue(Me, "Description", Me.Text)
                TxtDesp.Focus()
                TxtDesp.Select()
                Errorcontrol.SetError(TxtDesp, "Description")
                Return False
            Else
                Errorcontrol.ResetError(TxtDesp)
            End If
            'Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function

    Sub SaveData()
        Try
            btnsave.Focus()
            If AllowToSave() Then
                Dim obj As New ClsFaultMaster()
                obj.Fault_Master_Code = TxtCode.Value
                obj.Fault_Master_Name = clsCommon.myCstr(TxtDesp.Text)
                obj.Fault_Category_Code = FndFaultCat.Value
                Dim qry As Integer = clsDBFuncationality.getSingleValue("SELECT COUNT(Fault_Master_Code) FROM TSPL_SW_FAULT_MASTER WHERE Fault_Master_Code='" + obj.Fault_Master_Code + "'")
                If (qry = 0) Then
                    isNewEntry = True
                Else
                    isNewEntry = False
                End If
                If (ClsFaultMaster.SaveData(obj, isNewEntry)) Then
                    clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                    LoadData(obj.Fault_Master_Code, NavigatorType.Current)
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
            If clsCommon.myLen(TxtCode.Value) <= 0 Then
                Throw New Exception("Code not found to delete")
            End If
            If clsCommon.MyMessageBoxShow("Are you sure? do you want to delete this Code ('" + TxtCode.Value + "')", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then

                Dim qry As String = "DELETE FROM TSPL_SW_FAULT_MASTER WHERE Fault_Master_Code='" + TxtCode.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qry)
                clsCommon.MyMessageBoxShow("Deleted Successfully", Me.Text)
                AddNew()
            End If
        Catch ex As Exception
            If (clsCommon.CompairString(clsCommon.myCstr(ex.Message), "Code not found to delete") <> CompairStringResult.Equal) Then
                clsCommon.MyMessageBoxShow("Current Code is in use")
            Else
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End If
        End Try
    End Sub
#End Region

#Region "Events"

    Private Sub FndFaultCat__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles FndFaultCat._MYValidating
        Dim query As String = "SELECT fault_category_code AS Code,Fault_Category_Name AS [Fault Category Name] FROM TSPL_SW_FAULT_CATEGORY_MASTER"
        FndFaultCat.Value = clsCommon.ShowSelectForm("SWFauCatM", query, "Code", "", FndFaultCat.Value, "Code", isButtonClicked)
        Dim desc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Fault_Category_Name FROM TSPL_SW_FAULT_CATEGORY_MASTER WHERE fault_category_code='" & FndFaultCat.Value & "'"))
        If clsCommon.myLen(desc) > 0 Then
            LblFaultCat.Text = desc
        Else
            LblFaultCat.Text = ""
        End If
    End Sub

    Private Sub rmImport_Click(sender As Object, e As EventArgs) Handles rmImport.Click
        Dim gv As New RadGridView()
        Dim IsNewEntry As Boolean
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Code", "Description", "Fault Category Code") Then
            Dim linno As Integer = 1
            Try
                For Each grow As GridViewRowInfo In gv.Rows

                    Dim obj As New ClsFaultMaster()
                    Dim strCode As String = clsCommon.myCstr(grow.Cells("Code").Value)
                    Dim strDescription As String = clsCommon.myCstr(grow.Cells("Description").Value)
                    Dim strFalutCatCode As String = clsCommon.myCstr(grow.Cells("Fault Category Code").Value)
                    linno += 1
                    If clsCommon.myLen(strCode) <= 0 Then
                        Throw New Exception("Code should not be left blank" + clsCommon.myCstr(linno) + ".")
                    ElseIf clsCommon.myLen(strCode) > 30 Then
                        Throw New Exception("Please check ! Length of code can not be more than 30 at line no." + clsCommon.myCstr(linno) + ".")
                    End If

                    If clsCommon.myLen(strDescription) <= 0 Then
                        Throw New Exception("Description should not be left blank" + clsCommon.myCstr(linno) + ".")
                    ElseIf clsCommon.myLen(strDescription) > 150 Then
                        Throw New Exception("Please check ! Length of description can not be more than 150 at line no." + clsCommon.myCstr(linno) + ".")
                    End If

                    'If clsCommon.myLen(strFalutCatCode) <= 0 Then
                    '    Throw New Exception("Fault category code should not be left blank" + clsCommon.myCstr(linno) + ".")
                    'Else
                    If clsCommon.myLen(strFalutCatCode) > 0 Then
                        Dim qryState As String = "select Count(*) As Row FROM TSPL_SW_FAULT_CATEGORY_MASTER WHERE Fault_Category_Code ='" & strFalutCatCode & "'"
                        Dim checkState As Integer = clsDBFuncationality.getSingleValue(qryState)
                        If checkState <= 0 Then
                            Throw New Exception("Filled fault category code (" & strFalutCatCode & ") does not exist " + Environment.NewLine + " at line no." + clsCommon.myCstr(linno) + ".")
                        End If
                    End If


                    If clsCommon.myLen(strCode) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from TSPL_SW_FAULT_MASTER where Fault_Master_Code='" + strCode + "' ") > 0 Then
                        IsNewEntry = False
                    Else
                        IsNewEntry = True

                    End If

                    obj.Fault_Master_Code = strCode
                    obj.Fault_Master_Name = strDescription
                    obj.Fault_Category_Code = strFalutCatCode
                    ClsFaultMaster.SaveData(obj, IsNewEntry)

                Next
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub rmExport_Click(sender As Object, e As EventArgs) Handles rmExport.Click
        Dim str As String
        str = "select Fault_Master_Code as [Code],Fault_Master_Name As [Description],Fault_Category_Code AS [Fault Category Code]  from TSPL_SW_FAULT_MASTER "
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub FrmFaultMaster_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            AddNew()
        End If
    End Sub

    Private Sub FrmFaultMaster_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetUserMgmtNew()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
        AddNew()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        AddNew()
    End Sub

    Private Sub TxtCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles TxtCode._MYNavigator
        Try
            LoadData(TxtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtCode._MYValidating
        Dim str As String = "select count(*) from TSPL_SW_FAULT_MASTER where Fault_Master_Code ='" + TxtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            TxtCode.MyReadOnly = False
        Else
            TxtCode.MyReadOnly = True
        End If

        If TxtCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = ""
            qry = "SELECT Fault_Master_Code As [Code],Fault_Master_Name As [Fault Category Name],Fault_Category_Code AS [Fault Category Code] FROM TSPL_SW_FAULT_MASTER"
            TxtCode.Value = clsCommon.ShowSelectForm("SWFauMas", qry, "Code", "", TxtCode.Value, "TSPL_SW_FAULT_MASTER.Fault_Master_Name", isButtonClicked)
            If clsCommon.myLen(TxtCode.Value) > 0 Then
                Dim objOT As ClsFaultMaster
                objOT = ClsFaultMaster.GetData(TxtCode.Value, NavigatorType.Current)
                If Not objOT Is Nothing Then
                    LoadData(TxtCode.Value, NavigatorType.Current)
                End If
            Else
                AddNew()
            End If
        End If
    End Sub
#End Region

End Class