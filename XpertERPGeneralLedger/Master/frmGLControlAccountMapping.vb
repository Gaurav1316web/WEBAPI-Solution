Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common
Public Class frmGLControlAccountMapping
    Inherits FrmMainTranScreen
    Const colIACode As String = "COLIACODE"
    Const colDesc As String = "COLDESC"
    Const colReceipt As String = "colReceipt"
    Const colPayment As String = "colPayment"
    Const colAP As String = "colAP"
    Const colAR As String = "colAR"
    Const colJE As String = "colJE"
    'Const colISCODE As String = "SCODE"
    'Const colTYPE As String = "TYPE"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Private isformLoad As Boolean = False

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        If Not isformLoad Then
            If e.Column.Name = "COLIACODE" Then
                Dim query As String = "select Account_Code,Description from TSPL_GL_ACCOUNTS"
                Dim arr As New ArrayList
                arr = clsGLAccount.GetGLAccountsUsedInAllAccountSet(Nothing)
                ''remove account code conditions as per instructions if Ranjana Mam, 17 Feb 2021
                'Dim cond As String = "ControlAccount='Y'" & " and Account_Code not in (" & clsCommon.GetMulcallString(arr) & ") "
                Dim cond As String = "ControlAccount='Y'" & "  "


                'GetGLAccountsUsedInAllAccountSet()

                gv1.CurrentRow.Cells(colIACode).Value = clsCommon.ShowSelectForm("GACCODE", query, "Account_Code", cond, gv1.CurrentRow.Cells(colIACode).Value, "", False)
                query = "SELECT Description from TSPL_GL_ACCOUNTS where Account_Code='" + gv1.CurrentRow.Cells(colIACode).Value + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(query)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    gv1.CurrentRow.Cells(colDesc).Value = clsCommon.myCstr(dt.Rows(0)("Description"))
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

            Dim Arr As List(Of ClsGLControlAccountMapping) = ClsGLControlAccountMapping.GetData()
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each objTr As ClsGLControlAccountMapping In Arr
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIACode).Value = objTr.AccountCode
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDesc).Value = objTr.Account_Description
                    ''richa TEC/03/07/19-000927
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colReceipt).Value = IIf(objTr.IsForReceipt = 1, True, False)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPayment).Value = IIf(objTr.IsForPayment = 1, True, False)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAP).Value = IIf(objTr.IsForAP = 1, True, False)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAR).Value = IIf(objTr.IsForAR = 1, True, False)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colJE).Value = IIf(objTr.IsForJE = 1, True, False)
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colISCODE).Value = objTr.SourceCode
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colTYPE).Value = objTr.Type

                Next
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

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
        repoICode.Width = 150
        gv1.MasterTemplate.Columns.Add(repoICode)


        Dim repoDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDesc.FormatString = ""
        repoDesc.HeaderText = "Description"
        repoDesc.Name = colDesc
        repoDesc.ReadOnly = True
        repoDesc.HeaderImage = My.Resources.search4
        repoDesc.TextImageRelation = TextImageRelation.TextBeforeImage
        repoDesc.Width = 200
        gv1.MasterTemplate.Columns.Add(repoDesc)

        'Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'repoIName.FormatString = ""
        'repoIName.HeaderText = "Source Code"
        'repoIName.HeaderImage = Global.ERP.My.Resources.Resources.search4
        'repoIName.TextImageRelation = TextImageRelation.TextBeforeImage
        'repoIName.Name = colISCODE
        'repoIName.Width = 150
        'repoIName.ReadOnly = False
        'gv1.MasterTemplate.Columns.Add(repoIName)


        'Dim repodmc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'repodmc = New GridViewTextBoxColumn()
        'repodmc.FormatString = ""
        'repodmc.HeaderText = "Type"
        'repodmc.HeaderImage = Global.ERP.My.Resources.Resources.search4
        'repodmc.TextImageRelation = TextImageRelation.TextBeforeImage
        'repodmc.Name = colTYPE
        'repodmc.ReadOnly = False
        'repodmc.Width = 150
        'gv1.MasterTemplate.Columns.Add(repodmc)

        ''richa TEC/03/07/19-000927
        Dim repoIsReceipt As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsReceipt.HeaderText = "Is For Receipt"
        repoIsReceipt.Name = colReceipt
        repoIsReceipt.ReadOnly = False
        repoIsReceipt.IsVisible = True
        repoIsReceipt.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsReceipt)

        Dim repoIsPayment As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsPayment.HeaderText = "Is For Payment"
        repoIsPayment.Name = colPayment
        repoIsPayment.ReadOnly = False
        repoIsPayment.IsVisible = True
        repoIsPayment.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsPayment)

        Dim repoIsAP As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsAP.HeaderText = "Is For AP Invoice"
        repoIsAP.Name = colAP
        repoIsAP.ReadOnly = False
        repoIsAP.IsVisible = True
        repoIsAP.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsAP)

        Dim repoIsAR As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsAR.HeaderText = "Is For AR Invoice"
        repoIsAR.Name = colAR
        repoIsAR.ReadOnly = False
        repoIsAR.IsVisible = True
        repoIsAR.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsAR)

        Dim repoIsJE As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsJE.HeaderText = "Is For Journal Entry"
        repoIsJE.Name = colJE
        repoIsJE.ReadOnly = False
        repoIsJE.IsVisible = True
        repoIsJE.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsJE)

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
    Private Sub frmGLControlAccountMapping_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadBlankGrid()
        LoadData()
        gv1.Rows.AddNew()
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")

        ButtonToolTip.SetToolTip(BtnSave, "Press Alt+S to Save")
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmGLControlAccountMapping)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow(Me, "Permission Denied", Me.Text)
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
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmGLControlAccountMapping, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return
                End If
            End If
            Dim objtr As New ClsGLControlAccountMapping()

            Dim Arr As New List(Of ClsGLControlAccountMapping)
            For Each grow As GridViewRowInfo In gv1.Rows
                Dim obj As New ClsGLControlAccountMapping()

                'For ii As Integer = 0 To gv1.Rows.Count - 1
                obj.AccountCode = clsCommon.myCstr(grow.Cells(colIACode).Value)
                obj.Account_Description = clsCommon.myCstr(grow.Cells(colDesc).Value)
                'obj.Type = clsCommon.myCstr(grow.Cells(colTYPE).Value)
                ''richa TEC/03/07/19-000927
                obj.IsForPayment = IIf(grow.Cells(colPayment).Value = True, 1, 0)
                obj.IsForReceipt = IIf(grow.Cells(colReceipt).Value = True, 1, 0)
                obj.IsForAP = IIf(grow.Cells(colAP).Value = True, 1, 0)
                obj.IsForAR = IIf(grow.Cells(colAR).Value = True, 1, 0)
                obj.IsForJE = IIf(grow.Cells(colJE).Value = True, 1, 0)

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
                common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
        If common.clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
            Exit Sub
        Else
            Dim strqry As String = "delete from TSPL_CONTROL_ACC_MAPPING where account_code='" + gv1.CurrentRow.Cells(colIACode).Value + "'"



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
                Dim obj As New ClsGLControlAccountMapping()
                Dim Arr As New List(Of ClsGLControlAccountMapping)
                Dim arrAccExcl As New ArrayList
                arrAccExcl = clsGLAccount.GetGLAccountsUsedInAllAccountSet(Nothing)
                For Each grow As GridViewRowInfo In gv.Rows
                    linno += 1
                    Dim strcode As String = clsCommon.myCstr(grow.Cells("Account Code").Value)
                    If (String.IsNullOrEmpty(strcode)) Or clsCommon.myLen(strcode) > 30 Then
                        Throw New Exception("Length of Account Code should be max. 30 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.AccountCode = strcode
                    If arrAccExcl.Contains(strcode) = True Then
                        Throw New Exception("Account Code " & strcode & " at line no- " & (grow.Index + 1) & " is mapped in Vendor Account Set/Purchase Account Set/Customer Account Set/Sale Account Set/CSA Account Set/Bank master. " + clsCommon.myCstr(linno) + ".")
                    End If
                    Dim strAccDesc As String = clsCommon.myCstr(grow.Cells("Account Description").Value)
                    If clsCommon.myLen(strAccDesc) > 50 Then
                        Throw New Exception("Length of Account Description should be max. 50 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Account_Description = strAccDesc

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
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception

                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub


    Private Sub rmExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmExport.Click
        Dim str As String
        str = " select TSPL_CONTROL_ACC_MAPPING.Account_Code as [Account Code],TSPL_GL_ACCOUNTS.Description as [Account Description]  from TSPL_CONTROL_ACC_MAPPING left join TSPL_GL_ACCOUNTS on TSPL_CONTROL_ACC_MAPPING.Account_Code=TSPL_GL_ACCOUNTS.Account_Code"
        transportSql.ExporttoExcel(str, Me)
    End Sub

End Class
