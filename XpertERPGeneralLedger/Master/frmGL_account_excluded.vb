Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common
Public Class FrmGL_account_excluded
    Inherits FrmMainTranScreen
    Const colIACode As String = "COLIACODE"
    Const colDesc As String = "COLDESC"
    Const colISCODE As String = "SCODE"
    Const colTYPE As String = "TYPE"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Private isformLoad As Boolean = False

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        If Not isformLoad Then
            If e.Column.Name = "COLIACODE" Then
                Dim query As String = "select Main_GL_Account as Code,Main_GL_Account_Desc as Name from TSPL_ACCOUNT_MAIN_GL_ACCOUNT"
                gv1.CurrentRow.Cells(colIACode).Value = clsCommon.ShowSelectForm("GACCODE", query, "Code", "", gv1.CurrentRow.Cells(colIACode).Value, "", False)
                query = "SELECT Main_GL_Account_Desc from TSPL_ACCOUNT_MAIN_GL_ACCOUNT where Main_GL_Account='" + gv1.CurrentRow.Cells(colIACode).Value + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(query)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    gv1.CurrentRow.Cells(colDesc).Value = clsCommon.myCstr(dt.Rows(0)("Main_GL_Account_Desc"))
                End If
            End If
            'If e.Column.Name = "SCODE" Then
            '    Dim query As String = " select distinct Source_Code,Source_Desc  from TSPL_JOURNAL_MASTER"
            '    gv1.CurrentRow.Cells(colISCODE).Value = clsCommon.ShowSelectForm("GSCODE", query, "Source_Code", "", gv1.CurrentRow.Cells(colISCODE).Value, "", False)
            'End If
            'If e.Column.Name = "TYPE" Then
            '    Dim query As String = " select distinct Type from TSPL_JOURNAL_MASTER"
            '    gv1.CurrentRow.Cells(colTYPE).Value = clsCommon.ShowSelectForm("GTYPE", query, "type", "", gv1.CurrentRow.Cells(colTYPE).Value, "", False)
            'End If
        End If
    End Sub
    Sub LoadData()
        isformLoad = True
        Try

            ' LoadBlankGrid()

            Dim Arr As List(Of ClsGlTrialAccountExcluded) = ClsGlTrialAccountExcluded.GetData()
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each objTr As ClsGlTrialAccountExcluded In Arr
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIACode).Value = objTr.AccountCode
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDesc).Value = objTr.desc
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colISCODE).Value = objTr.SourceCode
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colTYPE).Value = objTr.Type

                Next
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)

        End Try
        isformLoad = False
    End Sub
    Sub LoadBlankGrid()
        'gv1.AddNewRowPosition = SystemRowPosition.Bottom
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Account Code"
        repoICode.Name = colIACode
        repoICode.HeaderImage = My.Resources.search4
        repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICode.Width = 200
        gv1.MasterTemplate.Columns.Add(repoICode)


        Dim repoDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDesc.FormatString = ""
        repoDesc.HeaderText = "Description"
        repoDesc.Name = colDesc
        repoDesc.ReadOnly = True
        repoDesc.HeaderImage = My.Resources.search4
        repoDesc.TextImageRelation = TextImageRelation.TextBeforeImage
        repoDesc.Width = 400
        gv1.MasterTemplate.Columns.Add(repoDesc)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Source Code"
        repoIName.HeaderImage = My.Resources.search4
        repoIName.TextImageRelation = TextImageRelation.TextBeforeImage
        repoIName.Name = colISCODE
        repoIName.Width = 150
        repoIName.ReadOnly = True
        repoIName.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoIName)


        Dim repodmc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repodmc = New GridViewTextBoxColumn()
        repodmc.FormatString = ""
        repodmc.HeaderText = "Type"
        repodmc.HeaderImage = My.Resources.search4
        repodmc.TextImageRelation = TextImageRelation.TextBeforeImage
        repodmc.Name = colTYPE
        repodmc.ReadOnly = True
        repodmc.IsVisible = False
        repodmc.Width = 150
        gv1.MasterTemplate.Columns.Add(repodmc)


        gv1.MasterTemplate.AllowAddNewRow = False
        gv1.MasterTemplate.AllowDeleteRow = True

        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.EnableFiltering = True
    End Sub
    Private Sub FrmGL_account_excluded_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        LoadBlankGrid()
        LoadData()
        gv1.Rows.AddNew()
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")

        ButtonToolTip.SetToolTip(BtnSave, "Press Alt+S to Save")
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FrmGL_account_excluded)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        BtnSave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 02/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If BtnSave.Visible = True Then
            rmImport.Enabled = True
            rmExport.Enabled = True
        Else
            rmImport.Enabled = False
            rmExport.Enabled = False
        End If
        '--------------------------------------------------

    End Sub
    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        Savedata()
    End Sub
    Sub Savedata()
        Try

            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd("", clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return
                End If
            End If
            Dim objtr As New ClsGlTrialAccountExcluded()

            Dim Arr As New List(Of ClsGlTrialAccountExcluded)
            For Each grow As GridViewRowInfo In gv1.Rows
                Dim obj As New ClsGlTrialAccountExcluded()

                'For ii As Integer = 0 To gv1.Rows.Count - 1
                obj.AccountCode = clsCommon.myCstr(grow.Cells(colIACode).Value)
                'obj.SourceCode = clsCommon.myCstr(grow.Cells(colISCODE).Value)
                'obj.Type = clsCommon.myCstr(grow.Cells(colTYPE).Value)


                'obj.itemcode = gv1.Rows(ii).Cells(colICode).Value
                'obj.name = gv1.Rows(ii).Cells(colIName).Value
                'obj.dmc = gv1.Rows(ii).Cells(colDMC).Value
                'obj.vmoh = gv1.Rows(ii).Cells(colVMOH).Value
                'obj.royality = gv1.Rows(ii).Cells(colRoyality).Value
                'obj.pfreight = gv1.Rows(ii).Cells(colPFreight).Value
                'obj.sfreight = gv1.Rows(ii).Cells(colSFreight).Value
                'obj.vsdrouter = gv1.Rows(ii).Cells(colVSDRouter).Value
                'obj.vsdLoUnlo = gv1.Rows(ii).Cells(colVSDLoULo).Value
                If (clsCommon.myLen(obj.AccountCode) > 0) Then
                    Arr.Add(obj)
                End If
            Next
            If BtnSave.Text = "Save" Then
                isNewEntry = True
            End If

            If (objtr.SaveData(Arr, isNewEntry)) Then
                common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub setGridFocus()
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            'gv1.CurrentRow.Cells(colICode).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If

    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        setGridFocus()
    End Sub


    Private Sub BtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
            Exit Sub
        Else
            Dim strqry As String = "delete from TSPL_TRIAL_GLACCOUNTS_EXCLUDED where account_code='" + gv1.CurrentRow.Cells(colIACode).Value + "'"



            clsDBFuncationality.GetDataTable(strqry)


        End If
    End Sub

    Private Sub rmImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Account Code") Then
            Dim linno As Integer = 0
            Try
                connectSql.OpenConnection()
                clsCommon.ProgressBarShow()

                Dim isnewentry As Boolean = True
                Dim obj As New ClsGlTrialAccountExcluded()
                Dim Arr As New List(Of ClsGlTrialAccountExcluded)
                For Each grow As GridViewRowInfo In gv.Rows
                    linno += 1
                    Dim strcode As String = clsCommon.myCstr(grow.Cells(0).Value)
                    If (String.IsNullOrEmpty(strcode)) Or clsCommon.myLen(strcode) > 30 Then
                        Throw New Exception("Length of Account Code should be max. 30 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.AccountCode = strcode

                    'Dim strSourceCode As String = clsCommon.myCstr(grow.Cells(1).Value)
                    'If (String.IsNullOrEmpty(strSourceCode)) Or clsCommon.myLen(strSourceCode) > 10 Then
                    '    Throw New Exception("Length of Source Code should be max. 10 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    'End If
                    'obj.SourceCode = strSourceCode

                    'Dim strType As String = clsCommon.myCstr(grow.Cells(2).Value)
                    'If (String.IsNullOrEmpty(strType)) Or clsCommon.myLen(strType) > 50 Then
                    '    Throw New Exception("Length of Type should be max. 50 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    'End If
                    'obj.Type = strType
                    If (clsCommon.myLen(obj.AccountCode) > 0) Then
                        Arr.Add(obj)
                    End If
                Next
                If isnewentry = True Then
                    obj.SaveData(Arr, True)
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


    Private Sub rmExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmExport.Click
        Dim str As String
        str = "select Account_Code as [Account Code]  from TSPL_TRIAL_GLACCOUNTS_EXCLUDED"
        transportSql.ExporttoExcel(str, Me)
    End Sub

End Class
