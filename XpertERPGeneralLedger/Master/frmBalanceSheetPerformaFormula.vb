Imports common
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports System.Data.SqlClient

Public Class frmBalanceSheetPerformaFormula
    Inherits FrmMainTranScreen

#Region "Varibales"
    Private isInsideLoadData As Boolean = False
    Const ColSNo As String = "ColSNo"
    Const ColMainParticular As String = "Main Particular"
    Const ColParticular As String = "ColParticular"
    Const ColMainGroupCode As String = "ColGroupCode"
    Const ColType As String = "ColType"
    Const ColMainGroupName As String = "ColGroupName"
    Const ColNote As String = "ColNote"
    Const ColFontStype As String = "ColFontStype"
    Const ColFormula As String = "ColFormula"
    Const ColGLMainAccount As String = "ColGLMainAccount"

    Dim settSelectGLInBalanceSheetPerforma As Boolean = False
#End Region

    Private Sub FrmBalanceSheetPerforma1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ''ERO/12/12/18-000435 by balwinder on 18/12/2018
        settSelectGLInBalanceSheetPerforma = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SelectGLInBalanceSheetPerforma, clsFixedParameterCode.SelectGLInBalanceSheetPerforma, Nothing)) > 0)
        RadLabel12.Visible = settSelectGLInBalanceSheetPerforma
        SetUserMgmtNew()
        gv2.AllowDeleteRow = True
        gv2.AllowAddNewRow = False
        gv2.ShowGroupPanel = False
        gv2.AllowColumnReorder = False
        gv2.AllowRowReorder = True
        gv2.EnableSorting = False
        gv2.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv2.MasterTemplate.ShowRowHeaderColumn = False
        LoadBlankGrid()
        LoadData()
        gv2.Rows.AddNew()
        If clsCommon.myLen(gv2.Rows(0).Cells(ColMainGroupCode).Value) <= 0 Then
            btnDelete.Enabled = False
        End If
    End Sub

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnSave.Visible = MyBase.isModifyFlag
        If btnSave.Visible = True Then
            rdBtnImport.Enabled = True
            dbtnExport.Enabled = True
        Else
            rdBtnImport.Enabled = False
            dbtnExport.Enabled = False
        End If
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SavingData()
    End Sub

    Sub SavingData()
        Try
            Dim arr As New List(Of clsBalanceSheetPerformaFormula)
            If AllowToSave() = True Then
                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmProfitAndLossPerforma, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
                arr = New List(Of clsBalanceSheetPerformaFormula)
                For ii As Integer = 0 To gv2.Rows.Count - 1
                    Dim obj As New clsBalanceSheetPerformaFormula()
                    obj.SNo = clsCommon.myCdbl(gv2.Rows(ii).Cells(ColSNo).Value)
                    obj.MainParticular = clsCommon.myCstr(gv2.Rows(ii).Cells(ColMainParticular).Value)
                    obj.Particular = clsCommon.myCstr(gv2.Rows(ii).Cells(ColParticular).Value)
                    obj.GroupCode = clsCommon.myCstr(gv2.Rows(ii).Cells(ColMainGroupCode).Value)
                    obj.Type = clsCommon.myCstr(gv2.Rows(ii).Cells(ColType).Value)
                    obj.GroupName = clsCommon.myCstr(gv2.Rows(ii).Cells(ColMainGroupName).Value)
                    obj.Note = clsCommon.myCstr(gv2.Rows(ii).Cells(ColNote).Value)
                    obj.FONTSTYLE = clsCommon.myCstr(gv2.Rows(ii).Cells(ColFontStype).Value)
                    obj.Formula = clsCommon.myCstr(gv2.Rows(ii).Cells(ColFormula).Value)
                    If settSelectGLInBalanceSheetPerforma Then
                        If clsCommon.myLen(obj.GroupCode) > 0 Then
                            obj.arrGLMainAccount = TryCast(gv2.Rows(ii).Cells(ColGLMainAccount).Tag, ArrayList)
                            If obj.arrGLMainAccount Is Nothing OrElse obj.arrGLMainAccount.Count <= 0 Then
                                Throw New Exception("Please select GL Main Accounts for Group Code [" + obj.GroupCode + "] at Sno" + clsCommon.myCstr(obj.SNo))
                            End If
                        End If
                    End If
                    If (clsCommon.myLen(obj.GroupCode) > 0 Or clsCommon.myLen(obj.Formula) > 0) AndAlso clsCommon.myLen(obj.Type) > 0 Then
                        arr.Add(obj)
                    End If
                Next
                If clsBalanceSheetPerformaFormula.SaveData(arr) = True Then
                    common.clsCommon.MyMessageBoxShow("Data Saved Sucsessfully")
                    btnDelete.Enabled = True
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Public Function AllowToSave() As Boolean
        SetSNO()
        For ii As Integer = 0 To gv2.Rows.Count - 1
            If gv2.Rows(ii).Cells(ColSNo).Value IsNot Nothing Then
                If clsCommon.myLen(gv2.Rows(ii).Cells(ColParticular).Value) <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Please Fill the Particular")
                    Return False
                ElseIf clsCommon.myCstr(gv2.Rows(ii).Cells(ColType).Value) Is Nothing OrElse clsCommon.myLen(gv2.Rows(ii).Cells(ColType).Value) <= 0 And (clsCommon.myCstr(gv2.Rows(ii).Cells(ColFormula).Value) Is Nothing OrElse clsCommon.myLen(gv2.Rows(ii).Cells(ColFormula).Value) <= 0) Then
                    common.clsCommon.MyMessageBoxShow("Please Select the Type")
                    Return False
                ElseIf clsCommon.myCstr(gv2.Rows(ii).Cells(ColMainGroupCode).Value) Is Nothing OrElse clsCommon.myLen(gv2.Rows(ii).Cells(ColMainGroupCode).Value) <= 0 And (clsCommon.myCstr(gv2.Rows(ii).Cells(ColFormula).Value) Is Nothing OrElse clsCommon.myLen(gv2.Rows(ii).Cells(ColFormula).Value) <= 0) Then
                    common.clsCommon.MyMessageBoxShow("Please Fill the Group Code")
                    Return False
                End If
            End If
        Next
        For ii As Integer = 0 To gv2.Rows.Count - 1
            For jj As Integer = ii + 1 To gv2.Rows.Count - 1
                If clsCommon.CompairString(gv2.Rows(ii).Cells(ColMainGroupCode).Value, gv2.Rows(jj).Cells(ColMainGroupCode).Value) = CompairStringResult.Equal And clsCommon.CompairString(gv2.Rows(ii).Cells(ColFormula).Value, gv2.Rows(jj).Cells(ColFormula).Value) = CompairStringResult.Equal Then
                    common.clsCommon.MyMessageBoxShow("This Group Already Exist")
                    gv2.CurrentRow.Cells(ColMainGroupCode).Value = ""
                    Return False
                End If
                If Not clsCommon.CompairString(gv2.Rows(ii).Cells(ColMainParticular).Value, gv2.Rows(jj).Cells(ColMainParticular).Value) = CompairStringResult.Equal Then
                    Dim varMain As String = gv2.Rows(jj).Cells(ColMainParticular).Value
                    For pp As Integer = jj + 1 To gv2.Rows.Count - 1
                        If clsCommon.CompairString(varMain, gv2.Rows(pp).Cells(ColMainParticular).Value) <> CompairStringResult.Equal Then
                            If clsCommon.CompairString(gv2.Rows(ii).Cells(ColMainParticular).Value, gv2.Rows(pp).Cells(ColMainParticular).Value) = CompairStringResult.Equal Then
                                common.clsCommon.MyMessageBoxShow("The Main Particular are same at SNo " + clsCommon.myCstr(gv2.Rows(ii).Cells(ColSNo).Value) + " And " + clsCommon.myCstr(gv2.Rows(pp).Cells(ColSNo).Value))
                                Return False
                                'Continue For
                            End If
                        End If
                    Next
                End If
            Next

        Next


        Return True
    End Function

    Sub LoadBlankGrid()
        gv2.Rows.Clear()
        gv2.Columns.Clear()
        gv2.AllowAddNewRow = False


        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "SNo"
        repoLineNo.Name = ColSNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv2.MasterTemplate.Columns.Add(repoLineNo)


        Dim repoMain_Particular As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoMain_Particular.FormatString = ""
        repoMain_Particular.HeaderText = "Main Particular"
        repoMain_Particular.Width = 100
        repoMain_Particular.Name = ColMainParticular
        repoMain_Particular.ReadOnly = False
        gv2.MasterTemplate.Columns.Add(repoMain_Particular)


        Dim repoParticular As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoParticular.FormatString = ""
        repoParticular.HeaderText = "Particular"
        repoParticular.Width = 100
        repoParticular.Name = ColParticular
        repoParticular.ReadOnly = False
        gv2.MasterTemplate.Columns.Add(repoParticular)

        Dim repoGroupCod As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoGroupCod.FormatString = ""
        repoGroupCod.HeaderText = "Main Group Code"
        repoGroupCod.Width = 100
        repoGroupCod.Name = ColMainGroupCode
        repoGroupCod.HeaderImage = My.Resources.search4
        repoGroupCod.TextImageRelation = TextImageRelation.TextBeforeImage
        repoGroupCod.ReadOnly = False
        gv2.MasterTemplate.Columns.Add(repoGroupCod)

        Dim repoNote As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoNote.FormatString = ""
        repoNote.HeaderText = "Note"
        repoNote.Width = 100
        repoNote.Name = ColNote
        repoNote.ReadOnly = False
        gv2.MasterTemplate.Columns.Add(repoNote)

        Dim repoGroupName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoGroupName.FormatString = ""
        repoGroupName.HeaderText = "Main Group Name"
        repoGroupName.Width = 200
        repoGroupName.Name = ColMainGroupName
        repoGroupName.ReadOnly = False
        gv2.MasterTemplate.Columns.Add(repoGroupName)

        repoGroupName = New GridViewTextBoxColumn()
        repoGroupName.FormatString = ""
        repoGroupName.HeaderText = "GL Main Account"
        repoGroupName.Width = 100
        repoGroupName.Name = ColGLMainAccount
        repoGroupName.ReadOnly = True
        repoGroupName.IsVisible = settSelectGLInBalanceSheetPerforma
        gv2.MasterTemplate.Columns.Add(repoGroupName)

        Dim repoType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoType.FormatString = ""
        repoType.HeaderText = "Type"
        repoType.Width = 100
        repoType.Name = ColType
        repoType.ReadOnly = False
        repoType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoType.DataSource = GetItemType()
        repoType.ValueMember = "Code"
        repoType.DisplayMember = "Code"
        gv2.MasterTemplate.Columns.Add(repoType)

        Dim repoFontType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoFontType.FormatString = ""
        repoFontType.HeaderText = "Font Style"
        repoFontType.Width = 100
        repoFontType.Name = ColFontStype
        repoFontType.ReadOnly = False
        repoFontType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoFontType.DataSource = GetItemType(True)
        repoFontType.ValueMember = "Code"
        repoFontType.DisplayMember = "Code"
        gv2.MasterTemplate.Columns.Add(repoFontType)


        Dim repoFormula As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoFormula.FormatString = ""
        repoFormula.HeaderText = "Formula"
        repoFormula.Width = 100
        repoFormula.Name = ColFormula
        repoFormula.ReadOnly = False
        gv2.MasterTemplate.Columns.Add(repoFormula)
    End Sub

    Public Shared Function GetItemType(Optional ByVal isFontstylecol As Boolean = False) As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))

        If isFontstylecol = False Then
            Dim dr As DataRow = dt.NewRow()
            dr("Code") = "Add"
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Code") = "Subtract"
            dt.Rows.Add(dr)
        Else
            Dim dr As DataRow = dt.NewRow()
            dr("Code") = "None"
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Code") = "Bold"
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Code") = "Italic"
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Code") = "Underline"
            dt.Rows.Add(dr)
        End If


        Return dt
    End Function

    Private Sub gv2_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv2.CellValueChanged
        If Not isInsideLoadData Then
            If e.Column Is gv2.Columns(ColMainParticular) OrElse e.Column Is gv2.Columns(ColParticular) OrElse e.Column Is gv2.Columns(ColMainGroupCode) OrElse e.Column Is gv2.Columns(ColType) Then
                If e.Column Is gv2.Columns(ColMainGroupCode) Then
                    Dim strPAndLGroupCode As String = clsFixedParameter.GetData(clsFixedParameterType.BalanceSheetProftAndLossGroupCode, clsFixedParameterCode.BalanceSheetProftAndLossGroupCode, Nothing)
                    Dim strPAndLGroupDesc As String = clsFixedParameter.GetData(clsFixedParameterType.BalanceSheetProftAndLossGroupDesc, clsFixedParameterCode.BalanceSheetProftAndLossGroupDesc, Nothing)
                    Dim qry As String = ""
                    qry = " select account_Main_group_code as [AccountGroupsCode],account_Main_group_desc as [Description] from TSPL_ACCOUNT_MAIN_GROUPS "
                    gv2.CurrentRow.Cells(ColMainGroupCode).Value = clsCommon.ShowSelectForm("BSPGroupCode", qry, "AccountGroupsCode", "", clsCommon.myCstr(gv2.CurrentRow.Cells(ColMainGroupCode).Value), "", False)
                    If clsCommon.myLen(strPAndLGroupCode) > 0 AndAlso clsCommon.CompairString(strPAndLGroupCode, clsCommon.myCstr(gv2.CurrentRow.Cells(ColMainGroupCode).Value)) = CompairStringResult.Equal Then
                        gv2.CurrentRow.Cells(ColMainGroupName).Value = strPAndLGroupDesc
                    Else
                        gv2.CurrentRow.Cells(ColMainGroupName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select account_mAIN_group_desc from tspl_account_MAIN_groups where account_mAIN_group_code='" + gv2.CurrentRow.Cells(ColMainGroupCode).Value + "'"))
                    End If
                End If
                IndexChange()
            End If
        End If
    End Sub

    Sub IndexChange()
        Try
            Dim intCurrRow As Integer = gv2.CurrentRow.Index
            gv2.CurrentRow.Cells(ColSNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv2.Rows.Count - 1 Then
                gv2.Rows.AddNew()
                gv2.CurrentRow = gv2.Rows(intCurrRow)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Sub LoadData()
        isInsideLoadData = True
        Dim Counter As Integer = 0
        LoadBlankGrid()
        Dim arr As List(Of clsBalanceSheetPerformaFormula) = clsBalanceSheetPerformaFormula.GetData()
        If arr IsNot Nothing AndAlso arr.Count > 0 Then
            For Each obj As clsBalanceSheetPerformaFormula In arr
                gv2.Rows.AddNew()
                gv2.Rows(Counter).Cells(ColSNo).Value = obj.SNo
                gv2.Rows(Counter).Cells(ColMainParticular).Value = obj.MainParticular
                gv2.Rows(Counter).Cells(ColParticular).Value = obj.Particular
                gv2.Rows(Counter).Cells(ColMainGroupCode).Value = obj.GroupCode
                gv2.Rows(Counter).Cells(ColMainGroupName).Value = obj.GroupName
                gv2.Rows(Counter).Cells(ColNote).Value = obj.Note

                If clsCommon.myCdbl(obj.Type) = 1 Then
                    obj.Type = "Add"
                Else
                    obj.Type = "Subtract"
                End If
                gv2.Rows(Counter).Cells(ColType).Value = obj.Type
                If clsCommon.myCdbl(obj.FONTSTYLE) = 1 Then
                    obj.FONTSTYLE = "None"
                ElseIf clsCommon.myCdbl(obj.FONTSTYLE) = 2 Then
                    obj.FONTSTYLE = "Bold"
                ElseIf clsCommon.myCdbl(obj.FONTSTYLE) = 3 Then
                    obj.FONTSTYLE = "Italic"
                ElseIf clsCommon.myCdbl(obj.FONTSTYLE) = 4 Then
                    obj.FONTSTYLE = "Underline"
                End If
                gv2.Rows(Counter).Cells(ColFontStype).Value = obj.FONTSTYLE
                gv2.Rows(Counter).Cells(ColFormula).Value = obj.Formula

                gv2.Rows(Counter).Cells(ColGLMainAccount).Tag = obj.arrGLMainAccount
                gv2.Rows(Counter).Cells(ColGLMainAccount).Value = clsCommon.GetMulcallStringWithComma(obj.arrGLMainAccount)

                Counter += 1
            Next
        End If
        isInsideLoadData = False
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub gv2_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv2.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Do you want to Delete the Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub gv2_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv2.UserDeletedRow
        SetSNO()
    End Sub

    Sub SetSNO()
        For ii As Integer = 1 To gv2.Rows.Count - 1
            gv2.Rows(ii - 1).Cells(ColSNo).Value = ii
        Next
    End Sub

    Private Sub FrmBalanceSheetPerforma1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F2 AndAlso (gv2.CurrentCell IsNot Nothing OrElse clsCommon.myLen(gv2.CurrentRow.Cells(ColMainGroupCode).Value) <= 0) Then
            isInsideLoadData = False
            If gv2.CurrentColumn Is gv2.Columns(ColMainGroupCode) Then
                gv2.CurrentColumn = gv2.Columns(ColMainGroupName)
                gv2.CurrentColumn = gv2.Columns(ColMainGroupCode)
                gv2.CurrentRow.Cells(ColMainGroupCode).Value = "."
            End If
            ''setGridFocus()


        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SavingData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclose.Enabled Then
            Close()

        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        If common.clsCommon.MyMessageBoxShow("Do You Really Want to Delete All Data", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
            clsBalanceSheetPerformaFormula.DeleteData()
            LoadBlankGrid()
            gv2.Rows.AddNew()
            common.clsCommon.MyMessageBoxShow("Data Deleted Successfull")
            btnDelete.Enabled = False
        End If
    End Sub

    Private Sub rdBtnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdBtnImport.Click
        Dim dgv As New RadGridView
        Me.Controls.Add(dgv)
        If transportSql.importExcel(dgv, "S No", "Main Particular", "Particular", "Group Code", "Group Name", "Note", "Type", "Font Style", "Formula") Then
            Dim arr As New List(Of clsBalanceSheetPerformaFormula)
            Dim obj As clsBalanceSheetPerformaFormula
            Try
                Dim LineNo As String = "0"
                clsCommon.ProgressBarShow()

                For Each grow As GridViewRowInfo In dgv.Rows
                    LineNo = clsCommon.myCstr(grow.Index + 2)
                    obj = New clsBalanceSheetPerformaFormula

                    Dim strv_id As String = clsCommon.myCstr(grow.Cells(0).Value)
                    If String.IsNullOrEmpty(strv_id) Or clsCommon.myLen(strv_id) > 12 Then
                        Throw New Exception("Line " + LineNo + " : S_No has incorrect values")
                    End If
                    obj.SNo = strv_id

                    Dim strMain_Particular As String = clsCommon.myCstr(grow.Cells(1).Value)
                    If clsCommon.myLen(strMain_Particular) > 50 Then
                        Throw New Exception("Line " + LineNo + " : Main_Particular Can not be greater than 50")

                    End If
                    obj.MainParticular = strMain_Particular

                    Dim strParticular As String = clsCommon.myCstr(grow.Cells(2).Value)
                    If clsCommon.myLen(strParticular) > 50 Then
                        Throw New Exception("Line " + LineNo + " : Particular can not be greater than 50")


                    End If
                    obj.Particular = strParticular

                    Dim strGroup_Code As String = clsCommon.myCstr(grow.Cells(3).Value)
                    If clsCommon.myLen(strGroup_Code) > 50 Then
                        Throw New Exception("Line " + LineNo + " : Description Can not be greater than 50")
                    End If
                    Dim strFormula As String = clsCommon.myCstr(grow.Cells(8).Value)
                    obj.GroupCode = strGroup_Code
                    Dim Count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_ACCOUNT_MAIN_GROUPS where Account_Main_Group_Code='" + strGroup_Code + "'"))
                    If Count <= 0 And strFormula = "" Then
                        Throw New Exception("Line " + LineNo + " : This  '" + strGroup_Code + "'  group code does not exist")
                    End If


                    Dim strGroup_Name As String = clsCommon.myCstr(grow.Cells(4).Value)
                    obj.GroupName = strGroup_Name

                    Dim strNote As String = clsCommon.myCstr(grow.Cells(5).Value)
                    If clsCommon.myLen(strNote) > 50 Then
                        Throw New Exception("Line " + LineNo + " : Note can not be blank")
                    End If
                    obj.Note = strNote

                    Dim strType As String = clsCommon.myCstr(grow.Cells(6).Value)


                    If strType = "1" Then
                        obj.Type = "Add"
                    ElseIf strType = "-1" Then
                        obj.Type = "substract"
                    Else
                        Throw New Exception("Line " + LineNo + " : Type can not be blank")
                    End If
                    '============================Rohit========================
                    Dim strFontStyle As String = clsCommon.myCstr(grow.Cells(7).Value)


                    If strFontStyle.Contains("1") Then
                        obj.FONTSTYLE = "None"
                    ElseIf strFontStyle.Contains("2") Then
                        obj.FONTSTYLE = "Bold"
                    ElseIf strFontStyle.Contains("3") Then
                        obj.FONTSTYLE = "Italic"
                    ElseIf strFontStyle.Contains("4") Then
                        obj.FONTSTYLE = "Underline"
                    Else
                        Throw New Exception("Line " + LineNo + " : Font Style can not be blank")
                    End If

                    obj.Formula = strFormula
                    If strFormula <> "" And strGroup_Code <> "" Then
                        MessageBox.Show("Please Fill Formula or Group in Excel Sheet..", "Import", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        Exit Sub
                    End If
                    '===================================================================

                    arr.Add(obj)
                Next

                clsBalanceSheetPerformaFormula.SaveData(arr)
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)


            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            Finally
                clsCommon.ProgressBarHide()
            End Try
        End If
        Me.Controls.Remove(dgv)
    End Sub

    Private Sub dbtnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dbtnExport.Click
        Try
            Dim query As String = "Select S_No as [S No],Main_Particular as [Main Particular], Particular ,Group_Code as [Group Code],Group_Name as [Group Name],Note as [Note],Type,Font_style as [Font Style],Formula from TSPL_BALANCE_SHEET_PERFORMA_FORMULA" 'tspl_balance_sheet_performa"
            ListImpExpColumnsMandatory = New List(Of String)({"S No", "Type", "Font Style"})
            transportSql.ExporttoExcel(query, "", "", Me, ListImpExpColumnsMandatory)
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            Throw New Exception(ex.Message)

        End Try
    End Sub

    Private Sub gv2_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv2.CellDoubleClick
        Try
            Dim qry As String
            Dim arrGLMainAccount As ArrayList = Nothing
            If e.Column Is gv2.Columns(ColGLMainAccount) Then
                If clsCommon.myLen(gv2.CurrentRow.Cells(ColMainGroupCode).Value) > 0 AndAlso settSelectGLInBalanceSheetPerforma Then
                    arrGLMainAccount = TryCast(gv2.CurrentRow.Cells(ColGLMainAccount).Tag, ArrayList)
                    qry = "select Main_GL_Account as Code,Main_GL_Account_desc as Name,TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Sub_Group_Code as SubGroupCode, TSPL_ACCOUNT_SUB_GROUPS.Account_Sub_Group_Desc as SubGroupName,TSPL_ACCOUNT_SUB_GROUPS.Account_Group_Code as GroupCode,TSPL_ACCOUNT_GROUPS.Account_Group_Desc as GroupName " + Environment.NewLine + _
                    "from TSPL_ACCOUNT_MAIN_GL_ACCOUNT" + Environment.NewLine + _
                    "left outer join TSPL_ACCOUNT_SUB_GROUPS on TSPL_ACCOUNT_SUB_GROUPS.Account_Sub_Group_Code=TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Sub_Group_Code" + Environment.NewLine + _
                    "left outer join TSPL_ACCOUNT_GROUPS on TSPL_ACCOUNT_GROUPS.Account_Group_Code=TSPL_ACCOUNT_Sub_GROUPS.Account_Group_Code" + Environment.NewLine + _
                    "where TSPL_ACCOUNT_GROUPS.Account_Main_Group_Code in ('" + clsCommon.myCstr(gv2.CurrentRow.Cells(ColMainGroupCode).Value) + "')"
                    arrGLMainAccount = clsCommon.ShowMultipleSelectForm(False, "@PALPglmain", qry, "Code", "Name", arrGLMainAccount, Nothing)
                    gv2.CurrentRow.Cells(ColGLMainAccount).Tag = arrGLMainAccount
                    gv2.CurrentRow.Cells(ColGLMainAccount).Value = clsCommon.GetMulcallStringWithComma(arrGLMainAccount)
                End If
            End If
            arrGLMainAccount = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub


End Class
