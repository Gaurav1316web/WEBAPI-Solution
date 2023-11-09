Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports common

Public Class FrmNDDBMaster
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt("NDDBMST")
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub AddNew()
        txtNDDBNo.Value = ""
        dtpNDDBDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
        txtDescription.Text = ""
        txtTagPrefix.Text = ""
        txtTagSNo.Text = ""
        txtUsedBy.Value = ""
        lblUsedBy.Text = ""
        txtFarmerID.Value = ""
        lblFarmer.Text = ""
        lblCattleDesc.Text = ""
        lblCattleType.Text = ""
        txtNDDBNo.MyReadOnly = False
        btnSave.Text = "Save"
        btnDelete.Enabled = False
        txtNDDBNo.Focus()
    End Sub
    Private Sub FrmNDDBMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        AddNew()
    End Sub
    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
        GC.Collect()
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As clsNDDBMaster = clsNDDBMaster.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            isNewEntry = False
            txtNDDBNo.Value = obj.NDDB_No
            txtDescription.Text = obj.NDDB_Desc
            dtpNDDBDate.Value = clsCommon.GetPrintDate(obj.NDDB_Date, "dd/MM/yyyy")
            txtTagPrefix.Text = obj.Tag_Prefix
            txtTagSNo.Text = obj.Tag_SNO
            txtUsedBy.Value = obj.USED_By
            If (clsCommon.myLen(obj.USED_By) > 0) Then
                lblUsedBy.Text = clsDBFuncationality.getSingleValue("select  Emp_Name from  TSPL_EMPLOYEE_MASTER where EMP_CODE='" & obj.USED_By & "'")
            End If

            txtFarmerID.Value = obj.Farmer_Id
            If (clsCommon.myLen(obj.Farmer_Id) > 0) Then
                lblFarmer.Text = clsDBFuncationality.getSingleValue("select  MP_Name from  TSPL_MP_MASTER where MP_Code='" & obj.Farmer_Id & "'")
            End If

            lblCattleDesc.Text = obj.Cattle_Id
            lblCattleType.Text = obj.Cattle_Type

            txtNDDBNo.MyReadOnly = True
            btnsave.Text = "Update"
            btndelete.Enabled = True

        End If
    End Sub

    Private Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(clsCommon.myCstr(txtNDDBNo.Value)) <= 0 Or clsCommon.myLen(clsCommon.myCstr(txtNDDBNo.Value)) > 16 Then
                myMessages.blankValue("NDDB No")
                txtNDDBNo.Focus()
                txtNDDBNo.Select()
                Errorcontrol.SetError(txtNDDBNo, "NDDB No")
                Return False
            Else
                Errorcontrol.ResetError(txtNDDBNo)
            End If
            If clsCommon.myLen(clsCommon.myCstr(txtTagPrefix.Text)) <= 0 Then
                myMessages.blankValue("Tag Prefix")
                txtTagPrefix.Focus()
                txtTagPrefix.Select()
                Errorcontrol.SetError(txtTagPrefix, "Tag Prefix")
                Return False
            Else
                Errorcontrol.ResetError(txtTagPrefix)
            End If

            If clsCommon.myLen(clsCommon.myCstr(txtTagSNo.Text)) <= 0 Then
                myMessages.blankValue("Tag SNo")
                txtTagSNo.Focus()
                txtTagSNo.Select()
                Errorcontrol.SetError(txtTagSNo, "Tag SNo")
                Return False
            Else
                Errorcontrol.ResetError(txtTagSNo)
            End If

            If clsCommon.myLen(clsCommon.myCstr(txtUsedBy.Value)) <= 0 Then
                myMessages.blankValue("Used By")
                txtUsedBy.Focus()
                txtUsedBy.Select()
                Errorcontrol.SetError(txtUsedBy, "Used By")
                Return False
            Else
                Errorcontrol.ResetError(txtUsedBy)
            End If

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
            Return False
        End Try
    End Function

    Sub SaveData()
        Try
            If AllowToSave() Then
                Dim obj As New clsNDDBMaster()
                obj.NDDB_No = txtNDDBNo.Value
                obj.NDDB_Desc = txtDescription.Text
                obj.NDDB_Date = dtpNDDBDate.Value
                obj.Tag_Prefix = txtTagPrefix.Text
                obj.Tag_SNO = txtTagSNo.Text
                obj.USED_By = txtUsedBy.Value
                obj.Farmer_Id = txtFarmerID.Value
                Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(NDDB_No) from TSPL_Paravet_NDDB_Master WHERE NDDB_No ='" + obj.NDDB_No + "'")
                If (qry = 0) Then
                    isNewEntry = True
                Else
                    isNewEntry = False
                End If
                If (clsNDDBMaster.SaveData(obj, isNewEntry)) Then
                    clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                    LoadData(obj.NDDB_No, NavigatorType.Current)
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
            If clsCommon.myLen(txtNDDBNo.Value) <= 0 Then
                Throw New Exception("Code not found to delete")
            End If
            If clsCommon.MyMessageBoxShow("Are you sure? do you want to delete this NDDB No ('" + txtNDDBNo.Value + "')", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then

                Dim qry As String = "DELETE FROM TSPL_Paravet_NDDB_Master WHERE NDDB_No='" + txtNDDBNo.Value + "'"
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


    Private Sub txtNDDBNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtNDDBNo._MYNavigator
        Try
            LoadData(txtNDDBNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub txtNDDBNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtNDDBNo._MYValidating
        Dim str As String = "select count(*) from TSPL_Paravet_NDDB_Master where NDDB_No ='" + txtNDDBNo.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtNDDBNo.MyReadOnly = False
        Else
            txtNDDBNo.MyReadOnly = True
        End If

        If txtNDDBNo.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = ""
            qry = "Select NDDB_No As [NDDB_NO],NDDB_Desc as [NDDB Desc],NDDB_Date As [NDDB Date] from TSPL_Paravet_NDDB_Master "
            txtNDDBNo.Value = clsCommon.ShowSelectForm("TSPL_Paravet_NDDB_Master", qry, "NDDB_NO", "", txtNDDBNo.Value, "TSPL_Paravet_NDDB_Master.NDDB_No", isButtonClicked)
            If clsCommon.myLen(txtNDDBNo.Value) > 0 Then
                Dim objOT As clsNDDBMaster
                objOT = clsNDDBMaster.GetData(txtNDDBNo.Value, NavigatorType.Current)
                If Not objOT Is Nothing Then
                    LoadData(txtNDDBNo.Value, NavigatorType.Current)
                End If
            Else
                AddNew()
            End If
        End If
    End Sub
    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        AddNew()
    End Sub

    Private Sub FrmNDDBMaster_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
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

    Private Sub RadMenuItem3_Click(sender As Object, e As EventArgs) Handles RadMenuItem3.Click
        Dim str As String
        str = "select NDDB_No as [NDDB No],NDDB_Desc as [NDDB Desc] , Convert(varchar, NDDB_Date,103) as [NDDB Date] ,Tag_Prefix as [Tag Prefix],Tag_SNO as [Tag SNO],USED_By as [USED By],Farmer_Id as [Farmer Id]  from TSPL_Paravet_NDDB_Master "
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Dim gv As New RadGridView()
        Dim IsNewEntry As Boolean
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "NDDB No", "NDDB Desc", "NDDB Date", "Tag Prefix", "Tag SNO", "USED By", "Farmer Id") Then
            Dim linno As Integer = 1
            Try
                For Each grow As GridViewRowInfo In gv.Rows

                    Dim obj As New clsNDDBMaster()
                    Dim strNDDB_No As String = clsCommon.myCstr(grow.Cells("NDDB No").Value)
                    Dim strNDDB_Desc As String = clsCommon.myCstr(grow.Cells("NDDB Desc").Value)
                    Dim strNDDB_Date As String = clsCommon.myCstr(grow.Cells("NDDB Date").Value)
                    Dim strTag_Prefix As String = clsCommon.myCstr(grow.Cells("Tag Prefix").Value)
                    Dim strTag_SNO As String = clsCommon.myCstr(grow.Cells("Tag SNO").Value)
                    Dim strUSED_By As String = clsCommon.myCstr(grow.Cells("USED By").Value)
                    ' Dim strFarmer_Id As String = clsCommon.myCstr(grow.Cells("Farmer Id").Value)
                   
                    linno += 1
                    If clsCommon.myLen(strNDDB_No) <= 0 Then
                        Throw New Exception("NDDB No should not be left blankat line no. " + clsCommon.myCstr(linno) + ".")
                    ElseIf clsCommon.myLen(strNDDB_No) > 16 Then
                        Throw New Exception("Please check ! length of NDDB No not greter then 16 at line no. " + clsCommon.myCstr(linno) + ".")
                    End If

                    If clsCommon.myLen(strTag_Prefix) <= 0 Then
                        Throw New Exception("Tag Prefix should not be left blank at line no. " + clsCommon.myCstr(linno) + ".")
                    ElseIf clsCommon.myLen(strTag_SNO) > 30 Then
                        Throw New Exception("Please check ! length of Tag Prefix not greter then 30 at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    If clsCommon.myLen(strTag_SNO) <= 0 Then
                        Throw New Exception("Tag SNO should not be left blank at line no. " + clsCommon.myCstr(linno) + ".")
                    ElseIf clsCommon.myLen(strTag_SNO) > 30 Then
                        Throw New Exception("Please check ! length of Tag SNO not greter then 30 at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    If clsCommon.myLen(strUSED_By) <= 0 Then
                        Throw New Exception("Used By should not be left blankat line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    If clsCommon.myLen(strUSED_By) > 0 Then
                        Dim Values As String = clsDBFuncationality.getSingleValue(" select count (*) from TSPL_EMPLOYEE_MASTER  where Emp_type='Paravet' and EMP_CODE='" & strUSED_By & "'  ")
                        If Values <= 0 Then
                            Throw New Exception("Invalid Used By Code,Line No " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If

                    If clsCommon.myLen(strNDDB_No) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Paravet_NDDB_Master where NDDB_No='" + strNDDB_No + "' ") > 0 Then
                        IsNewEntry = False
                    Else
                        IsNewEntry = True

                    End If
                    obj.NDDB_No = strNDDB_No
                    obj.NDDB_Desc = strNDDB_Desc
                    obj.NDDB_Date = strNDDB_Date
                    obj.Tag_Prefix = strTag_Prefix
                    obj.Tag_SNO = strTag_SNO
                    obj.USED_By = strUSED_By
                    'obj.Farmer_Id = strFarmer_Id

                    clsNDDBMaster.SaveData(obj, IsNewEntry)
                    'LoadData(obj.NDDB_No, NavigatorType.Current)
                Next
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub txtFarmerID__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtFarmerID._MYValidating
        'Dim query As String = " select MP_Code as [Code], MP_Name as [Description] from TSPL_MP_MASTER "
        'txtFarmerID.Value = clsCommon.ShowSelectForm("FarmerVald", query, "Code", "", txtFarmerID.Value, "Code", isButtonClicked)
        'Dim desc As String = "select  MP_Name from  TSPL_MP_MASTER where MP_Code='" & txtFarmerID.Value & "'"
        'Dim dt As DataTable = clsDBFuncationality.GetDataTable(desc)
        'If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
        '    lblFarmer.Text = clsCommon.myCstr(dt.Rows(0)("MP_Name"))
        'Else
        '    lblFarmer.Text = ""
        'End If
    End Sub


    Private Sub txtUsedBy__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtUsedBy._MYValidating
        Dim query As String = " select EMP_CODE as [Code], Emp_Name as [Name], Designation,Emp_type as [Emp Type] from TSPL_EMPLOYEE_MASTER   "
        txtUsedBy.Value = clsCommon.ShowSelectForm("UsedByVald", query, "Code", "Emp_type='Paravet'", txtUsedBy.Value, "Code", isButtonClicked)
        Dim desc As String = "select  Emp_Name from  TSPL_EMPLOYEE_MASTER where EMP_CODE='" & txtUsedBy.Value & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(desc)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            lblUsedBy.Text = clsCommon.myCstr(dt.Rows(0)("Emp_Name"))
        Else
            lblUsedBy.Text = ""
        End If
    End Sub

    

End Class
