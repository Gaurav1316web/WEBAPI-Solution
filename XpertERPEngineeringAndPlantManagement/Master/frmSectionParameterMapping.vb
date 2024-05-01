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
'ticket no--> Ticket No  VIJ/21/10/19-000039
'Client --> Vijaya Dairy
'created by --> Sanjay


Public Class frmSectionParameterMapping
    Inherits FrmMainTranScreen
    Const colSelect As String = "SELECT"
    Const colCCCode As String = "PCCODE"
    Const colCCName As String = "PCNAME"
    Const colCCSEQ As String = "PCSEQ"
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

    Private Sub frmSectionParameterMapping_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim coll As Dictionary(Of String, String)
        'coll = New Dictionary(Of String, String)()
        'coll.Add("Parameter_Seq", "INTEGER not null ")
        'clsCommonFunctionality.CreateOrAlterTable("TSPL_ENG_SECTION_PARAMETER_MAPPING", coll)

        funreset()
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New Trasnaction")
        ToolTipcity.SetToolTip(btnnew, "New")
        btnsave.Enabled = True
        btndelete.Enabled = False
        SetDataBaseGrid()
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
                Dim obj As New clsSectionParameterMapping()
                obj.Section_Code = txtCode.Value
                obj.Consumption_Code = txtConsumType.Value
                obj.arrListParameter = New List(Of clsSectionParameterMappingDetail)
                For Each grow As GridViewRowInfo In dgv.Rows
                    If (clsCommon.myCBool(grow.Cells(colSelect).Value) = True) Then
                        Dim objParam As New clsSectionParameterMappingDetail()
                        objParam.Parameter_Code = clsCommon.myCstr(grow.Cells(colCCCode).Value)
                        objParam.Parameter_Seq = CInt(grow.Cells(colCCSEQ).Value)
                        obj.arrListParameter.Add(objParam)
                    End If
                Next

                If (clsSectionParameterMapping.SaveData(obj)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadDataMapping(obj.Section_Code, obj.Consumption_Code)
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Function AllowToSave() As Boolean
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select section code.", Me.Text)
            txtCode.Focus()
            Return False
        End If

        If clsCommon.myLen(txtConsumType.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select consumption type.", Me.Text)
            txtConsumType.Focus()
            Return False
        End If

        'Squence number mandatory
        Dim TempMappingExist As Boolean = False
        Dim arrSequence As New List(Of Integer)
        For i As Integer = 0 To dgv.Rows.Count - 1
            If dgv.Rows(i).Cells(colSelect).Value = True Then

                TempMappingExist = True
                If clsCommon.myCdbl(dgv.Rows(i).Cells(colCCSEQ).Value) <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please enter parameter squence no At Line No. " & " : " + clsCommon.myCstr(i + 1) + "", Me.Text)
                    Return False
                End If

                If arrSequence.Contains(clsCommon.myCstr(dgv.Rows(i).Cells(colCCSEQ).Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Same parameter sequence no Repeated (" + clsCommon.myCstr(dgv.Rows(i).Cells(colCCSEQ).Value) + ") At Line No. " + clsCommon.myCstr(i + 1) + "", Me.Text)
                    Return False
                Else
                    arrSequence.Add(clsCommon.myCstr(dgv.Rows(i).Cells(colCCSEQ).Value))
                End If

            End If
        Next


        'For i As Integer = 0 To dgv.Rows.Count - 1
        '    If dgv.Rows(i).Cells(colSelect).Value = True Then
        '        TempMappingExist = True
        '    End If
        'Next
        If TempMappingExist = False Then
            common.clsCommon.MyMessageBoxShow(Me, "Please map at least one parameter.", Me.Text)
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
        ElseIf txtConsumType.Value = "" Then
            myMessages.blankValue(Me, "Consumption code", Me.Text)
            txtConsumType.Focus()
        ElseIf myMessages.deleteConfirm() Then
            If (clsSectionParameterMapping.DeleteData(txtCode.Value, txtConsumType.Value)) Then
                common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
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
        txtSecdes.Text = ""
        txtConsumType.Value = Nothing
        txtConsumTypedes.Text = ""
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = True
        For i As Integer = 0 To dgv.Rows.Count - 1
            'If dgv.Rows(i).Cells(colSelect).Value = True Then
            dgv.Rows(i).Cells(colSelect).Value = False
            dgv.Rows(i).Cells(colCCSEQ).Value = Nothing
            'End If
        Next
    End Sub

    Public Sub funClear()
        isNewEntry = True
        txtConsumType.Value = Nothing
        txtConsumTypedes.Text = ""
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = True
        For i As Integer = 0 To dgv.Rows.Count - 1
            'If dgv.Rows(i).Cells(colSelect).Value = True Then
            dgv.Rows(i).Cells(colSelect).Value = False
            dgv.Rows(i).Cells(colCCSEQ).Value = Nothing
            'End If
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
        'If transportSql.importExcel(gv, "Section Code", "Consumption Code", "ParameterCode1", "ParameterCode2", "ParameterCode3", "ParameterCode4", "ParameterCode5", "ParameterCode6", "ParameterCode7", "ParameterCode8", "ParameterCode9", "ParameterCode10", "ParameterCode11", "ParameterCode12", "ParameterCode13", "ParameterCode14", "ParameterCode15", "ParameterCode16", "ParameterCode17", "ParameterCode18", "ParameterCode19", "ParameterCode20", "ParameterCode21", "ParameterCode22", "ParameterCode23", "ParameterCode24", "ParameterCode25", "ParameterCode26", "ParameterCode27", "ParameterCode28", "ParameterCode29", "ParameterCode30", "ParameterCode31", "ParameterCode32", "ParameterCode33", "ParameterCode34", "ParameterCode35", "ParameterCode36", "ParameterCode37", "ParameterCode38", "ParameterCode39", "ParameterCode40", "ParameterCode41", "ParameterCode42", "ParameterCode43", "ParameterCode44", "ParameterCode45", "ParameterCode46", "ParameterCode47", "ParameterCode48", "ParameterCode49", "ParameterCode50") Then
        '    'Dim trans As SqlTransaction = Nothing

        '    Dim linno As Integer = 0
        '    Dim TempNewRecord As Boolean = False
        '    Try
        '        ' connectSql.OpenConnection()
        '        'trans = clsDBFuncationality.GetTransactin()
        '        Dim objList = New List(Of clsSectionParameterMapping)
        '        clsCommon.ProgressBarShow()

        '        Dim strSection_Code As String = ""
        '        Dim strConsumption_Code As String = ""

        '        For Each grow As GridViewRowInfo In gv.Rows
        '            linno += 1
        '            strSection_Code = clsCommon.myCstr(grow.Cells(0).Value)
        '            If (String.IsNullOrEmpty(strSection_Code)) Or clsCommon.myLen(strSection_Code) > 30 Then
        '                Throw New Exception("Length of Section Code should be max. 30 character At Line No. " + clsCommon.myCstr(linno) + ".")
        '            End If
        '            Dim TempSectionCode As String = ""
        '            TempSectionCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Code from TSPL_ENG_SECTION_MASTER where Code='" + strSection_Code + "'"))
        '            If clsCommon.myLen(TempSectionCode) <= 0 Then
        '                Throw New Exception("Not a valid Section Code At Line No. " & " : " + clsCommon.myCstr(linno) + "")
        '            End If

        '            strConsumption_Code = clsCommon.myCstr(grow.Cells(1).Value)
        '            If (String.IsNullOrEmpty(strConsumption_Code)) Or clsCommon.myLen(strConsumption_Code) > 30 Then
        '                Throw New Exception("Length of Consumption Code should be max. 30 character At Line No. " + clsCommon.myCstr(linno) + "")
        '            End If
        '            Dim TempConsumptionCode As String = ""
        '            TempConsumptionCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Code from TSPL_ENG_CONSUMPTION_TYPE_MASTER where Code='" + strConsumption_Code + "'"))
        '            If clsCommon.myLen(TempConsumptionCode) <= 0 Then
        '                Throw New Exception("Not a valid Consumption Code At Line No. " & " : " + clsCommon.myCstr(linno) + "")
        '            End If

        '            Dim obj As New clsSectionParameterMapping()
        '            obj.Section_Code = strSection_Code
        '            obj.Consumption_Code = strConsumption_Code

        '            Dim templist As New ArrayList
        '            For j As Integer = 1 To 50
        '                Dim CC_CODE As String
        '                CC_CODE = clsCommon.myCstr(grow.Cells("ParameterCode" & clsCommon.myCstr(j) & "").Value)
        '                If clsCommon.myLen(CC_CODE) > 0 Then
        '                    Dim ParameterCode As String = ""
        '                    ParameterCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Code from TSPL_ENG_PARAMETER_MASTER where Code='" + CC_CODE + "'"))
        '                    If clsCommon.myLen(ParameterCode) <= 0 Then
        '                        Throw New Exception("Not a valid Parameter Code" & clsCommon.myCstr(j) & " : " + clsCommon.myCstr(grow.Cells("ParameterCode" & clsCommon.myCstr(j) & "").Value) + " at line " + clsCommon.myCstr(linno) + "")
        '                    End If
        '                    templist.Add(clsCommon.myCstr(CC_CODE))
        '                End If
        '            Next

        '            If templist.Count = 0 Then
        '                Throw New Exception("Please map at least one Parameter Code At Line No. " + clsCommon.myCstr(linno))
        '            End If
        '            obj.arrListParameter = templist
        '            objList.Add(obj)
        '        Next
        '        clsSectionParameterMapping.SaveData(objList)
        '        clsCommon.ProgressBarHide()
        '        common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
        '    Catch ex As Exception
        '        clsCommon.ProgressBarHide()
        '        myMessages.myExceptions(ex)
        '    End Try
        'End If
        If transportSql.importExcel(gv, "Section Code", "Consumption Code", "Parameter Code", "Parameter Seq") Then
            'Dim trans As SqlTransaction = Nothing

            Dim linno As Integer = 0
            Dim TempNewRecord As Boolean = False
            Try
                ' connectSql.OpenConnection()
                'trans = clsDBFuncationality.GetTransactin()

                clsCommon.ProgressBarShow()

                Dim dt As DataTable = New DataTable()
                dt.Columns.Add(New DataColumn("Section_Code", System.Type.GetType("System.String")))
                dt.Columns.Add(New DataColumn("Consumption_Code", System.Type.GetType("System.String")))
                dt.Columns.Add(New DataColumn("Parameter_Code", System.Type.GetType("System.String")))
                dt.Columns.Add(New DataColumn("Parameter_Seq", System.Type.GetType("System.Int32")))
                Dim strSection_Code As String = ""
                Dim strConsumption_Code As String = ""
                Dim strParameter_Code As String = ""
                Dim intParameter_Seq As Int32 = 0
                Dim obj As clsSectionParameterMapping = Nothing
                Dim arrListParameter As List(Of clsSectionParameterMappingDetail) = Nothing
                Dim objList = New List(Of clsSectionParameterMapping)
                For Each grow As GridViewRowInfo In gv.Rows
                    linno += 1
                    strSection_Code = clsCommon.myCstr(grow.Cells("Section Code").Value)
                    If (String.IsNullOrEmpty(strSection_Code)) Or clsCommon.myLen(strSection_Code) > 30 Then
                        Throw New Exception("Length of Section Code should be max. 30 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    Dim TempSectionCode As String = ""
                    TempSectionCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Code from TSPL_ENG_SECTION_MASTER where Code='" + strSection_Code + "'"))
                    If clsCommon.myLen(TempSectionCode) <= 0 Then
                        Throw New Exception("Not a valid Section Code At Line No. " & " : " + clsCommon.myCstr(linno) + "")
                    End If

                    strConsumption_Code = clsCommon.myCstr(grow.Cells("Consumption Code").Value)
                    If (String.IsNullOrEmpty(strConsumption_Code)) Or clsCommon.myLen(strConsumption_Code) > 30 Then
                        Throw New Exception("Length of Consumption Code should be max. 30 character At Line No. " + clsCommon.myCstr(linno) + "")
                    End If
                    Dim TempConsumptionCode As String = ""
                    TempConsumptionCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Code from TSPL_ENG_CONSUMPTION_TYPE_MASTER where Code='" + strConsumption_Code + "'"))
                    If clsCommon.myLen(TempConsumptionCode) <= 0 Then
                        Throw New Exception("Not a valid Consumption Code At Line No. " & " : " + clsCommon.myCstr(linno) + "")
                    End If

                    strParameter_Code = clsCommon.myCstr(grow.Cells("Parameter Code").Value)
                    If (String.IsNullOrEmpty(strParameter_Code)) Or clsCommon.myLen(strParameter_Code) > 30 Then
                        Throw New Exception("Length of Parameter Code should be max. 30 character At Line No. " + clsCommon.myCstr(linno) + "")
                    End If
                    Dim TempParameterCode As String = ""
                    TempParameterCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Code from TSPL_ENG_PARAMETER_MASTER where Code='" + strParameter_Code + "'"))
                    If clsCommon.myLen(TempParameterCode) <= 0 Then
                        Throw New Exception("Not a valid Parameter Code At Line No. " & " : " + clsCommon.myCstr(linno) + "")
                    End If

                    intParameter_Seq = CInt(grow.Cells("Parameter Seq").Value)
                    If clsCommon.myCdbl(intParameter_Seq) <= 0 Then
                        Throw New Exception("Not a valid Parameter Sequence No At Line No. " & " : " + clsCommon.myCstr(linno) + "")
                    End If

                    Dim rows As DataRow() = dt.Select("Section_Code='" + clsCommon.myCstr(strSection_Code) + "' AND Consumption_Code='" + clsCommon.myCstr(strConsumption_Code) + "' AND Parameter_Code='" + clsCommon.myCstr(strParameter_Code) + "'")
                    Dim rowseq As DataRow() = dt.Select("Section_Code='" + clsCommon.myCstr(strSection_Code) + "' AND Consumption_Code='" + clsCommon.myCstr(strConsumption_Code) + "' AND Parameter_Seq=" + clsCommon.myCstr(intParameter_Seq) + "")

                    If (rows Is Nothing And rowseq Is Nothing) OrElse (rows.Length <= 0 And rowseq.Length <= 0) Then
                        dt.Rows.Add(strSection_Code, strConsumption_Code, strParameter_Code, intParameter_Seq)
                        obj = New clsSectionParameterMapping()
                        obj.Section_Code = strSection_Code
                        obj.Consumption_Code = strConsumption_Code

                        Dim objtr As New clsSectionParameterMappingDetail
                        objtr.Parameter_Code = strParameter_Code
                        objtr.Parameter_Seq = intParameter_Seq

                        arrListParameter = New List(Of clsSectionParameterMappingDetail)
                        arrListParameter.Add(objtr)
                        obj.arrListParameter = arrListParameter
                        objList.Add(obj)

                    End If

                Next

                If objList.Count > 0 Then
                    clsSectionParameterMapping.SaveData(objList)
                End If

                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
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

    Private Sub frmSectionParameterMapping_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
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
                                                                          "TSPL_ENG_SECTION_PARAMETER_MAPPING ")
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub


    Sub SetDataBaseGrid()
        Try
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
            repoCostCode.HeaderText = "Parameter Code"
            repoCostCode.Name = colCCCode
            repoCostCode.Width = 150
            repoCostCode.ReadOnly = True
            dgv.MasterTemplate.Columns.Add(repoCostCode)

            Dim repoCostName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoCostName.FormatString = ""
            repoCostName.HeaderText = "Parameter Description"
            repoCostName.Name = colCCName
            repoCostName.Width = 250
            repoCostName.ReadOnly = True
            dgv.MasterTemplate.Columns.Add(repoCostName)

            Dim repoDecimalColumn As GridViewDecimalColumn = Nothing
            repoDecimalColumn = New GridViewDecimalColumn()
            repoDecimalColumn.Name = colCCSEQ
            repoDecimalColumn.Width = 105
            repoDecimalColumn.FormatString = "{0:n0}"
            repoDecimalColumn.DecimalPlaces = 0
            repoDecimalColumn.HeaderText = "Parameter Seq"
            repoDecimalColumn.ReadOnly = False
            dgv.MasterTemplate.Columns.Add(repoDecimalColumn)


            Dim qry As String = "SELECT Code,Description from TSPL_ENG_PARAMETER_MASTER "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    dgv.Rows.AddNew()
                    dgv.Rows(dgv.Rows.Count - 1).Cells(colSelect).Value = False
                    dgv.Rows(dgv.Rows.Count - 1).Cells(colCCCode).Value = clsCommon.myCstr(dr("Code"))
                    dgv.Rows(dgv.Rows.Count - 1).Cells(colCCName).Value = clsCommon.myCstr(dr("Description"))
                    dgv.Rows(dgv.Rows.Count - 1).Cells(colCCSEQ).Value = Nothing
                Next
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Try
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
                txtCode.Value = clsCommon.ShowSelectForm("TSPL_ENG_SECTION_MASTER2", qry, "Code", whrClas, txtCode.Value, "Code", isButtonClicked)
                LoadData(txtCode.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            funClear()
            Dim obj As clsEngSectionMaster = clsEngSectionMaster.GetData(strCode, NavTyep)
            If obj IsNot Nothing Then
                'isNewEntry = False
                txtCode.Value = obj.Code
                txtSecdes.Text = obj.Description
                txtCode.MyReadOnly = True
                'btnsave.Text = "Update"
                'btndelete.Enabled = True
            Else
                funreset()
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Public Sub LoadDataMapping(ByVal strSection_Code As String, ByVal strConsumption_Code As String)
        Try
            txtCode.MyReadOnly = True
            btnsave.Enabled = True
            btndelete.Enabled = True
            isNewEntry = False

            Dim obj As New clsSectionParameterMapping()
            obj = clsSectionParameterMapping.GetData(strSection_Code, strConsumption_Code)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Section_Code) > 0) Then
                funreset()
                txtCode.Value = obj.Section_Code
                txtSecdes.Text = clsEngSectionMaster.GetName(obj.Section_Code, Nothing)
                txtConsumType.Value = obj.Consumption_Code
                txtConsumTypedes.Text = clsEngConsumptionTypeMaster.GetName(obj.Consumption_Code, Nothing)

                If obj.arrListParameter IsNot Nothing AndAlso obj.arrListParameter.Count > 0 Then
                    isNewEntry = False
                    btnsave.Text = "Update"

                    For Each objTr As clsSectionParameterMappingDetail In obj.arrListParameter
                        For Each grow As GridViewRowInfo In dgv.Rows
                            If (clsCommon.myCstr(grow.Cells(colCCCode).Value) = clsCommon.myCstr(objTr.Parameter_Code)) Then
                                grow.Cells(colSelect).Value = True
                                grow.Cells(colCCSEQ).Value = CInt(objTr.Parameter_Seq)
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
        Try
            Dim str As String
            'str = " select TSPL_ENG_SECTION_MASTER.Code as [Section Code],TSPL_ENG_CONSUMPTION_TYPE_MASTER.Code as [Consumption Code]"
            'For j As Integer = 1 To 50
            '    str += " ,(select Parameter_Code from (Select ROW_NUMBER () over (order by Section_Code,Consumption_Code ) As SNo,Section_Code,Consumption_Code,Parameter_Code From TSPL_ENG_SECTION_PARAMETER_MAPPING "
            '    str += " where TSPL_ENG_SECTION_PARAMETER_MAPPING.Section_Code=TSPL_ENG_SECTION_MASTER.Code and TSPL_ENG_SECTION_PARAMETER_MAPPING.Consumption_Code=TSPL_ENG_CONSUMPTION_TYPE_MASTER.Code "
            '    str += ")xxx where xxx.SNo=" & j & ") as ParameterCode" & j & ""
            'Next
            'str += " from TSPL_ENG_CONSUMPTION_TYPE_MASTER "
            'str += " left join TSPL_ENG_SECTION_CONSUMPTION_MAPPING on TSPL_ENG_SECTION_CONSUMPTION_MAPPING.Consumption_Code=TSPL_ENG_CONSUMPTION_TYPE_MASTER.Code"
            'str += " left join TSPL_ENG_SECTION_MASTER on TSPL_ENG_SECTION_MASTER.Code=TSPL_ENG_SECTION_CONSUMPTION_MAPPING.Section_Code "
            str = "  select TSPL_ENG_SECTION_PARAMETER_MAPPING.Section_Code as [Section Code],TSPL_ENG_SECTION_PARAMETER_MAPPING.Consumption_Code as [Consumption Code]" & _
              ",TSPL_ENG_SECTION_PARAMETER_MAPPING.parameter_code as [Parameter Code],TSPL_ENG_SECTION_PARAMETER_MAPPING.Parameter_Seq as [Parameter Seq]" & _
               " from TSPL_ENG_SECTION_PARAMETER_MAPPING "
            transportSql.ExporttoExcel(str, Me)
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub


    Private Sub txtConsumType__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtConsumType._MYValidating
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select section code first.", Me.Text)
                txtCode.Focus()
                Exit Sub
            End If

            Dim qry As String = ""
            Dim strWhrClause As String = ""
            qry = "select TSPL_ENG_CONSUMPTION_TYPE_MASTER.Code,TSPL_ENG_CONSUMPTION_TYPE_MASTER.Description from TSPL_ENG_CONSUMPTION_TYPE_MASTER " & _
                " left join TSPL_ENG_SECTION_CONSUMPTION_MAPPING on TSPL_ENG_SECTION_CONSUMPTION_MAPPING.Consumption_Code=TSPL_ENG_CONSUMPTION_TYPE_MASTER.Code " & _
                " left join TSPL_ENG_SECTION_MASTER on TSPL_ENG_SECTION_MASTER.Code=TSPL_ENG_SECTION_CONSUMPTION_MAPPING.Section_Code "
            strWhrClause = "TSPL_ENG_SECTION_MASTER.Code in ('" + txtCode.Value + "')"
            txtConsumType.Value = clsCommon.ShowSelectForm("ConsumTypeFndr", qry, "Code", strWhrClause, txtConsumType.Value, "Code", isButtonClicked)
            txtConsumTypedes.Text = clsEngConsumptionTypeMaster.GetName(txtConsumType.Value, Nothing)
            SetDataBaseGrid()
            If clsCommon.myLen(txtCode.Value) > 0 AndAlso clsCommon.myLen(txtConsumType.Value) > 0 Then
                LoadDataMapping(txtCode.Value, txtConsumType.Value)
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
End Class

