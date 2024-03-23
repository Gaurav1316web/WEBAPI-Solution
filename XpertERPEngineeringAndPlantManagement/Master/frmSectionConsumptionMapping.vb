Imports Microsoft.VisualBasic
Imports System
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
'ticket no--> Ticket No  VIJ/21/10/19-000037
'Client --> Vijaya Dairy
'created by --> Sanjay


Public Class frmSectionConsumptionMapping
    Inherits FrmMainTranScreen
    Const colSelect As String = "SELECT"
    Const colCCCode As String = "PCCODE"
    Const colCCName As String = "PCNAME"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim str As String

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        If btnsave.Visible = True Then
            import.Enabled = True
            export.Enabled = True
        Else
            import.Enabled = False
            export.Enabled = False
        End If
        '--------------------------------------------------
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub frmSectionConsumptionMapping_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        funreset()
        SetUserMgmtNew()
        SetDataBaseGrid()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New Trasnaction")
        ToolTipcity.SetToolTip(btnnew, "New")
        btnsave.Enabled = True
        btndelete.Enabled = False
    End Sub

    'Keypress validation on finder and converting lower case to upper case
    Public Sub key_press(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub
    'Validation on save button click and calling funinsert,funupdate
    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub
    Public Sub SaveData()
        Try
            If AllowToSave() Then
                Dim obj As New clsSectionConsumptionMapping()
                obj.Code = txtCode.Value
                Dim templist As New ArrayList
                For Each grow As GridViewRowInfo In dgv.Rows
                    If (clsCommon.myCBool(grow.Cells(colSelect).Value) = True) Then
                        templist.Add(clsCommon.myCstr(grow.Cells(colCCCode).Value))
                    End If
                Next
                obj.arrListConsumption = templist
                If (clsSectionConsumptionMapping.SaveData(obj)) Then
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    LoadData(obj.Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Function AllowToSave() As Boolean
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            myMessages.blankValue(Me, "Code", Me.Text)
            txtCode.Focus()
            Return False
        End If

        Dim TempMappingExist As Boolean = False
        For i As Integer = 0 To dgv.Rows.Count - 1
            If dgv.Rows(i).Cells(colSelect).Value = True Then
                TempMappingExist = True
            End If
        Next
        If TempMappingExist = False Then
            common.clsCommon.MyMessageBoxShow("Please map at least one comsumption Type.", Me.Text)
            Return False
        End If


        Return True
    End Function

    'Delete funtion call on delete button
    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Public Sub DeleteData()
        If btndelete.Enabled = False Then
            Exit Sub
        End If
        If txtCode.Value = "" Then
            myMessages.blankValue(Me, "Section code", Me.Text)
            txtCode.Focus()
        ElseIf myMessages.deleteConfirm() Then
            If (clsSectionConsumptionMapping.DeleteData(txtCode.Value)) Then
                common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                funreset()
            End If
        End If
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        funreset()
    End Sub
    'It will reset all the controls in screens
    Public Sub funreset()
        txtCode.Value = Nothing
        txtCode.MyReadOnly = False
        isNewEntry = True
        txtdes.Text = ""
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = True
        For i As Integer = 0 To dgv.Rows.Count - 1
            If dgv.Rows(i).Cells(colSelect).Value = True Then
                dgv.Rows(i).Cells(colSelect).Value = False
            End If
        Next
    End Sub
    'closing of current window form
    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    'For Import functionality 
    Private Sub import_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles import.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Section Code", "ConsumptionTypeCode1", "ConsumptionTypeCode2", "ConsumptionTypeCode3", "ConsumptionTypeCode4", "ConsumptionTypeCode5", "ConsumptionTypeCode6", "ConsumptionTypeCode7", "ConsumptionTypeCode8", "ConsumptionTypeCode9", "ConsumptionTypeCode10", "ConsumptionTypeCode11", "ConsumptionTypeCode12", "ConsumptionTypeCode13", "ConsumptionTypeCode14", "ConsumptionTypeCode15", "ConsumptionTypeCode16", "ConsumptionTypeCode17", "ConsumptionTypeCode18", "ConsumptionTypeCode19", "ConsumptionTypeCode20", "ConsumptionTypeCode21", "ConsumptionTypeCode22", "ConsumptionTypeCode23", "ConsumptionTypeCode24", "ConsumptionTypeCode25", "ConsumptionTypeCode26", "ConsumptionTypeCode27", "ConsumptionTypeCode28", "ConsumptionTypeCode29", "ConsumptionTypeCode30", "ConsumptionTypeCode31", "ConsumptionTypeCode32", "ConsumptionTypeCode33", "ConsumptionTypeCode34", "ConsumptionTypeCode35", "ConsumptionTypeCode36", "ConsumptionTypeCode37", "ConsumptionTypeCode38", "ConsumptionTypeCode39", "ConsumptionTypeCode40", "ConsumptionTypeCode41", "ConsumptionTypeCode42", "ConsumptionTypeCode43", "ConsumptionTypeCode44", "ConsumptionTypeCode45", "ConsumptionTypeCode46", "ConsumptionTypeCode47", "ConsumptionTypeCode48", "ConsumptionTypeCode49", "ConsumptionTypeCode50") Then
            'Dim trans As SqlTransaction = Nothing

            Dim linno As Integer = 0
            Dim TempNewRecord As Boolean = False
            Try
                ' connectSql.OpenConnection()
                'trans = clsDBFuncationality.GetTransactin()
                Dim objList = New List(Of clsSectionConsumptionMapping)
                clsCommon.ProgressBarShow()

                Dim strcode As String = ""
                'Dim strname As String = ""

                For Each grow As GridViewRowInfo In gv.Rows
                    linno += 1
                    strcode = clsCommon.myCstr(grow.Cells(0).Value)
                    If (String.IsNullOrEmpty(strcode)) Or clsCommon.myLen(strcode) > 30 Then
                        Throw New Exception("Length of Section Code should be max. 30 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    Dim TempSectionCode As String = ""
                    TempSectionCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Code from TSPL_ENG_SECTION_MASTER where Code='" + strcode + "'"))
                    If clsCommon.myLen(TempSectionCode) <= 0 Then
                        Throw New Exception("Not a valid Section Code At Line No. " & " : " + clsCommon.myCstr(linno) + ".")
                    End If

                    Dim obj As New clsSectionConsumptionMapping()
                    obj.Code = strcode
                    'obj.Name = strname

                    Dim templist As New ArrayList
                    For j As Integer = 1 To 50
                        Dim CC_CODE As String
                        CC_CODE = clsCommon.myCstr(grow.Cells("ConsumptionTypeCode" & clsCommon.myCstr(j) & "").Value)
                        If clsCommon.myLen(CC_CODE) > 0 Then
                            Dim ConsumptionTypeCode As String = ""
                            ConsumptionTypeCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Code from TSPL_ENG_CONSUMPTION_TYPE_MASTER where Code='" + CC_CODE + "'"))
                            If clsCommon.myLen(ConsumptionTypeCode) <= 0 Then
                                Throw New Exception("Not a valid ConsumptionTypeCode" & clsCommon.myCstr(j) & " : " + clsCommon.myCstr(grow.Cells("ConsumptionTypeCode" & clsCommon.myCstr(j) & "").Value) + " at line " + clsCommon.myCstr(linno) + ".")
                            End If
                            templist.Add(clsCommon.myCstr(CC_CODE))
                        End If
                    Next

                    If templist.Count = 0 Then
                        Throw New Exception("Please map at least one comsumption Type At Line No. " + clsCommon.myCstr(linno))
                    End If
                    obj.arrListConsumption = templist
                    objList.Add(obj)
                Next
                clsSectionConsumptionMapping.SaveData(objList)
                'trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                'trans.Rollback()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub
    'For closing current screen by menu strip Close
    Private Sub cityclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cityclose.Click
        closeform()
    End Sub
    Public Sub closeform()
        Me.Close()
    End Sub

    Private Sub frmSectionConsumptionMapping_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            closeform()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funreset()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine + _
                                                                      "TSPL_ENG_SECTION_CONSUMPTION_MAPPING ")
        End If
    End Sub


    Sub SetDataBaseGrid()
        dgv.Rows.Clear()
        dgv.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.FormatString = ""
        repoSelect.HeaderText = "Select"
        repoSelect.Name = colSelect
        repoSelect.Width = 50
        repoSelect.ReadOnly = False
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        dgv.MasterTemplate.Columns.Add(repoSelect)

        Dim repoCostCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCostCode.FormatString = ""
        repoCostCode.HeaderText = "Consumption Code"
        repoCostCode.Name = colCCCode
        repoCostCode.Width = 150
        repoCostCode.ReadOnly = True
        dgv.MasterTemplate.Columns.Add(repoCostCode)

        Dim repoCostName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCostName.FormatString = ""
        repoCostName.HeaderText = "Consumption Description"
        repoCostName.Name = colCCName
        repoCostName.Width = 247
        repoCostName.ReadOnly = True
        dgv.MasterTemplate.Columns.Add(repoCostName)


        Dim qry As String = "SELECT Code,Description from TSPL_ENG_CONSUMPTION_TYPE_MASTER "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                dgv.Rows.AddNew()
                dgv.Rows(dgv.Rows.Count - 1).Cells(colSelect).Value = False
                dgv.Rows(dgv.Rows.Count - 1).Cells(colCCCode).Value = clsCommon.myCstr(dr("Code"))
                dgv.Rows(dgv.Rows.Count - 1).Cells(colCCName).Value = clsCommon.myCstr(dr("Description"))
            Next
        End If
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Dim qst As String = "select count(*) from TSPL_ENG_SECTION_MASTER where Code='" + txtCode.Value + "'"
        Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
        If count = 0 Then
            txtCode.MyReadOnly = False
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            Dim whrClas As String = ""
            Dim qry As String = " select Code as [Code],Description as [Description] from TSPL_ENG_SECTION_MASTER  "
            txtCode.Value = clsCommon.ShowSelectForm("TSPL_ENG_SECTION_MASTER1", qry, "Code", whrClas, txtCode.Value, "Code", isButtonClicked)
            LoadData(txtCode.Value, NavigatorType.Current)
        End If
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub
    Public Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            txtCode.MyReadOnly = True
            btnsave.Enabled = True
            btndelete.Enabled = True
            isNewEntry = False

            Dim obj As New clsSectionConsumptionMapping()
            obj = clsSectionConsumptionMapping.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
                funreset()
                txtCode.Value = obj.Code
                txtdes.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_ENG_SECTION_MASTER where code='" + obj.Code + "'"))

                If obj.arrListConsumption IsNot Nothing AndAlso obj.arrListConsumption.Count > 0 Then
                    isNewEntry = False
                    btnsave.Text = "Update"
                    For i As Integer = 0 To obj.arrListConsumption.Count - 1
                        For Each grow As GridViewRowInfo In dgv.Rows
                            If (clsCommon.myCstr(grow.Cells(colCCCode).Value) = clsCommon.myCstr(obj.arrListConsumption(i))) Then
                                grow.Cells(colSelect).Value = True
                            End If
                        Next
                    Next
                End If

            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub


    Private Sub export_Click(sender As Object, e As EventArgs) Handles export.Click
        Dim str As String
        str = " select TSPL_ENG_SECTION_MASTER.Code as [Section Code]  "
        For j As Integer = 1 To 50
            str += " ,(select Consumption_Code from (Select ROW_NUMBER () over (order by Section_Code,Consumption_Code ) As SNo,Section_Code,Consumption_Code  From TSPL_ENG_SECTION_CONSUMPTION_MAPPING where Section_Code=TSPL_ENG_SECTION_MASTER.Code)xxx where xxx.SNo=" & j & ") as ConsumptionTypeCode" & j & ""
        Next
        str += " from TSPL_ENG_SECTION_MASTER "

        transportSql.ExporttoExcel(str, Me)
    End Sub


End Class

