Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Configuration
Imports Telerik.Collections.Generic
Imports Excel = Microsoft.Office.Interop.Excel
Imports common

Public Class frmVendorsubGroup
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
    Dim dr As SqlDataReader
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String

#End Region
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Save()
    End Sub
    Public Sub Save()
        Try
            If AllowToSave() Then


                Dim obj As New clsVendorSubGroup()
                obj.VendorSubCode = fndgroupcode.Value
                obj.Description = txtDesc.Text
                If (clsVendorSubGroup.SaveData(obj, isNewEntry)) Then
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    LoadData(obj.VendorSubCode, NavigatorType.Current)
                    btnSave.Text = "Update"
                    btndelete.Enabled = True
                Else
                    btnSave.Text = "Save"
                    btnDelete.Enabled = False
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Function AllowToSave() As Boolean
        If clsCommon.myLen(txtDesc.Text) <= 0 Then
            myMessages.blankValue("Description")
            txtDesc.Focus()
            Return False
       
        End If
        Return True
    End Function
    Sub CloseForm()
        Me.Close()
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)

        btnSave.Enabled = True
        btnDelete.Enabled = True
        isNewEntry = False
        Dim obj As New clsVendorSubGroup()
        obj = clsVendorSubGroup.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.VendorSubCode) > 0) Then

            isNewEntry = False
            btnSave.Text = "Update"
            btnDelete.Enabled = True
            fndgroupcode.Value = obj.VendorSubCode
            txtDesc.Text = obj.Description
        End If

    End Sub
    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndgroupcode._MYNavigator
        Try
            LoadData(fndgroupcode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnreset.Click
        funReset()
    End Sub

    Sub funReset()
        isNewEntry = True
        fndgroupcode.Value = Nothing
        fndgroupcode.Focus()
        txtDesc.Text = ""
        btnSave.Text = "Save"
        btnSave.Enabled = True
        fndgroupcode.MyReadOnly = False
    End Sub

    Private Sub frmVendorsubGroup_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnsave.Enabled AndAlso MyBase.isModifyFlag Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btndelete.Enabled AndAlso MyBase.isDeleteFlag Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso rdbtnclose.Enabled Then
            CloseForm()
        End If
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(fndgroupcode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("You Cannot Delete Record")
            Exit Sub
        End If
        ' Code Ends 
        funDelete()
    End Sub

    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsVendorSubGroup.DeleteData(fndgroupcode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Private Sub frmStateMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isNewEntry = True
        funReset()

        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(rdbtnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(rdbtnreset, "Press Alt+N Adding New ")


    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndgroupcode._MYValidating

        Dim str As String = ""
        str = "select count(*) from TSPL_VENDOR_SUB_GROUP where Ven_Sub_Group_Code ='" + fndgroupcode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))

        If no = 0 AndAlso isButtonClicked = False Then
            fndgroupcode.MyReadOnly = False
        Else
            fndgroupcode.MyReadOnly = True
        End If
        If fndgroupcode.MyReadOnly OrElse isButtonClicked Then
          
            fndgroupcode.Value = clsVendorSubGroup.getFinder("", fndgroupcode.Value, isButtonClicked)

            If fndgroupcode.Value <> "" Then
                LoadData(fndgroupcode.Value, NavigatorType.Current)
            Else
                funReset()
            End If
        End If


    End Sub
    Private Sub MenuRackItemImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdmenuimport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Code", "Description") Then
            Try
                clsCommon.ProgressBarShow()
                Dim ii As Integer = 0
                Try
                    For Each grow As GridViewRowInfo In gv.Rows
                        ii = ii + 1
                        Dim obj As New clsVendorSubGroup()
                        obj.VendorSubCode = clsCommon.myCstr(grow.Cells("Code").Value)
                        If clsCommon.myLen(obj.VendorSubCode) <= 0 Then
                            If clsCommon.myLen(obj.VendorSubCode) > 12 Then
                                Throw New Exception("Vendor Sub Group length can not be more then 12.")
                            End If
                            Throw New Exception("Vendor Sub Group can not be blank or incorrect.")
                        End If
                        obj.Description = clsCommon.myCstr(grow.Cells("Description").Value)
                        If clsCommon.myLen(obj.Description) <= 0 Then
                            Throw New Exception("Description can not be blank or incorrect.")
                        End If
                      
                        clsVendorSubGroup.SaveData(obj, isNewEntry)

                    Next
                Catch ex As Exception
                    Throw New Exception("At Row No" + clsCommon.myCstr(ii) + ex.Message)
                End Try
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub


    Private Sub MenuItemExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdmenuexport.Click
        Dim str As String
        str = "select  Ven_Sub_Group_Code as Code,SubGroup_Desc as Description from TSPL_VENDOR_SUB_GROUP"
        transportSql.ExporttoExcel(str, Me)
    End Sub
End Class


