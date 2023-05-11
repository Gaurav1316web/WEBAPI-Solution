'-------shivani Tyagi,,,,,Ticket no.[BM00000003681]
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
Public Class FrmAgencyMaster
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isnewentry As Boolean
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()
    Function AllowTosave() As Boolean
       
        If clsCommon.myLen(clsCommon.myCstr(txt_Code.Value)) <= 0 Or clsCommon.myLen(clsCommon.myCstr(txt_Code.Value)) > 30 Then
            myMessages.blankValue("Code")

            txt_Code.Focus()
            txt_Code.Select()
            Errorcontrol.SetError(txt_Code, "Code")
            Return False
        Else
            Errorcontrol.ResetError(txt_Code)
        End If
        If clsCommon.myLen(clsCommon.myCstr(txt_Name.Text)) <= 0 Or clsCommon.myLen(clsCommon.myCstr(txt_Name.Text)) > 30 Then
            myMessages.blankValue("Name")

            txt_Name.Focus()
            txt_Name.Select()
            Errorcontrol.SetError(txt_Name, "Name")
            Return False
        Else
            Errorcontrol.ResetError(txt_Name)
        End If


        'If txt_Code.Value = "" Then
        '    MessageBox.Show("Code cannot be blank")
        '    Return False
        'ElseIf txt_Name.Text = "" Then

        '    MessageBox.Show("Name cannot be blank")
        '    Return False
        'End If
        Return True
    End Function
#Region "Functions"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.AgencyMaster)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        butnsave.Visible = MyBase.isModifyFlag
        butnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Sub SaveData()

        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If AllowTosave() Then
                Dim Agency As New ClsAgencyMaster
                Agency.Code = txt_Code.Value
                Agency.Name = txt_Name.Text

                If (ClsAgencyMaster.SaveData(Agency, isnewentry)) Then
                    clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                    LoadData(Agency.Code, NavigatorType.Current)
                    butnsave.Text = "Update"
                    butnDelete.Enabled = True
                Else
                    butnsave.Text = "Save"
                    butnDelete.Enabled = False
                End If
            End If




        Catch ex As Exception
            'trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub butnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butnsave.Click
        SaveData()

    End Sub
    
    Private Sub DeleteData()
        Try
            If clsCommon.myLen(txt_Code.Value) <= 0 Then
                Throw New Exception("Code not found to delete")
            End If
            If clsCommon.MyMessageBoxShow("are you sure? do you want to delete this Code ('" + txt_Code.Value + "')", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then

                Dim qry As String = "DELETE FROM TSPL_HR_Agency_master WHERE Code='" + txt_Code.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qry)
                clsCommon.MyMessageBoxShow("Deleted Successfully", Me.Text)
                ResetData()
            End If
        Catch ex As Exception
            If (clsCommon.CompairString(clsCommon.myCstr(ex.Message), "Code not found to delete") <> CompairStringResult.Equal) Then
                clsCommon.MyMessageBoxShow("Current Code is in use")
            Else
                clsCommon.MyMessageBoxShow(ex.Message)
            End If
        End Try
    End Sub
    Sub ResetData()
        txt_Code.Value = ""
        txt_Name.Text = ""
        isnewentry = True
        butnDelete.Enabled = False
        butnsave.Text = "Save"
        'butnClose.Enabled = False
    End Sub
    Private Sub butnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butnDelete.Click
        DeleteData()
        'ResetData()
    End Sub

    Private Sub butnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butnClose.Click
        Me.Close()
    End Sub

    Sub LoadData(ByVal Agency As String, ByVal NavigatorType As NavigatorType)

        'txtCode.MyReadOnly = True
        butnsave.Enabled = True
        butnDelete.Enabled = True
        isnewentry = False
        Dim cls As ClsAgencyMaster = ClsAgencyMaster.GetData(Agency, NavigatorType)
        If cls IsNot Nothing Then
            txt_Code.Value = cls.Code
            txt_Name.Text = cls.Name

            butnsave.Text = "Update"
            isnewentry = False
            butnDelete.Enabled = True
        End If


    End Sub

    Private Sub txt_Code__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txt_Code._MYNavigator
        LoadData(txt_Code.Value, NavType)
        butnsave.Text = "Update"
    End Sub

    Private Sub txt_Code__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txt_Code._MYValidating
        Dim qry As String
        If isButtonClicked Then
            qry = "select Code,Name from Tspl_HR_Agency_Master"
            txt_Code.Value = clsCommon.ShowSelectForm("id", qry, "Code", "", txt_Code.Value, "", isButtonClicked)
        End If
        If clsCommon.myLen(txt_Code.Value) > 0 Then
            LoadData(txt_Code.Value, NavigatorType.Current)

        End If

    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        ResetData()
    End Sub

    Private Sub RMImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RMImport.Click
        Dim gv As New RadGridView()
        Dim IsNewEntry As Boolean
        'Dim trans As SqlTransaction
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Code", "Name") Then
            Dim linno As Integer = 1
            Try
                'trans = clsDBFuncationality.GetTransactin()
                'connectSql.OpenConnection()
                For Each grow As GridViewRowInfo In gv.Rows

                    Dim obj As New ClsAgencyMaster()
                    Dim strCode As String = clsCommon.myCstr(grow.Cells("Code").Value)
                    Dim strName As String = clsCommon.myCstr(grow.Cells("Name").Value)
                    linno += 1
                    If clsCommon.myLen(strCode) <= 0 Then
                        Throw New Exception("Code should not be left blank" + clsCommon.myCstr(linno) + ".")
                    End If


                    If clsCommon.myLen(strName) <= 0 Then
                        Throw New Exception("Name should not be left blank" + clsCommon.myCstr(linno) + ".")
                    End If

                    If clsCommon.myLen(strCode) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from Tspl_HR_Agency_Master where comp_code='" + objCommonVar.CurrentCompanyCode + "' and Code='" + strCode + "' ") > 0 Then
                        IsNewEntry = False
                    Else
                        IsNewEntry = True

                    End If
                  
                    obj.Code = strCode
                    obj.Name = strName
                    ClsAgencyMaster.SaveData(obj, IsNewEntry)

                Next
                'trans.Commit()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                'trans.Rollback()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub RMExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RMExport.Click
        Dim str As String
        str = "select Code as [Code],Name As [Name]  from Tspl_HR_ Agency_Master "
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub frmAgencyMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso butnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso butnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            ResetData()
        End If
    End Sub


    Private Sub FrmAgencyMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        isnewentry = True
        ButtonToolTip.SetToolTip(butnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(butnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(butnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
        ResetData()
    End Sub
#End Region
End Class
