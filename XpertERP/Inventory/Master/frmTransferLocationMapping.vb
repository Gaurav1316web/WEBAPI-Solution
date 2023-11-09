'===================BM00000003069,Updated By Rohit========================
Imports common
Imports System.Data.SqlClient
Imports System.IO

Public Class frmTransferLocationMapping
    Inherits FrmMainTranScreen
    Const colSNo As String = "S.No"
    Const colFrmLoc As String = "FrmLoc"
    Const colFrmLocDesc As String = "FrmLocDesc"
    Const colToLoc As String = "ToLoc"
    Const colToLocDesc As String = "ToLocDesc"
    Const colGLAcc As String = "GLAcc"
    Const colGLAccDesc As String = "GLAccDesc"


    Dim ButtonToolTip As ToolTip = New ToolTip()

    Dim obj As New clsItemLoc
    Private ObjList As New List(Of clsItemLoc)
    Private isCellValueChangedOpen As Boolean = False
    Dim OpenFileDialog1 As New OpenFileDialog


    Sub LoadGridColumns()
        gvLoc.Rows.Clear()
        gvLoc.Columns.Clear()

        Dim SNo As New GridViewTextBoxColumn

        Dim FrmLocation As New GridViewTextBoxColumn
        Dim Tolocation As New GridViewTextBoxColumn
        Dim FrmLocationDesc As New GridViewTextBoxColumn
        Dim TolocationDesc As New GridViewTextBoxColumn
        Dim GLAccount As New GridViewTextBoxColumn
        Dim GLAccountDesc As New GridViewTextBoxColumn

        SNo.FormatString = ""
        SNo.HeaderText = "S.No"
        SNo.Name = colSNo
        SNo.Width = 70
        SNo.ReadOnly = True
        SNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvLoc.Columns.Add(SNo)

        FrmLocation.FormatString = ""
        FrmLocation.HeaderText = "From Location"
        FrmLocation.Name = colFrmLoc
        FrmLocation.Width = 200
        'FrmLocation.ReadOnly = True
        FrmLocation.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvLoc.Columns.Add(FrmLocation)

        FrmLocationDesc.FormatString = ""
        FrmLocationDesc.HeaderText = "From Location Desc"
        FrmLocationDesc.Name = colFrmLocDesc
        FrmLocationDesc.Width = 200
        FrmLocationDesc.ReadOnly = True
        FrmLocationDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvLoc.Columns.Add(FrmLocationDesc)

        Tolocation.FormatString = ""
        Tolocation.HeaderText = "To Location"
        Tolocation.Name = colToLoc
        Tolocation.Width = 200
        'Tolocation.ReadOnly = True
        Tolocation.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvLoc.Columns.Add(Tolocation)

        TolocationDesc.FormatString = ""
        TolocationDesc.HeaderText = "To Location Desc"
        TolocationDesc.Name = colToLocDesc
        TolocationDesc.Width = 200
        TolocationDesc.ReadOnly = True
        TolocationDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvLoc.Columns.Add(TolocationDesc)


        GLAccount.FormatString = ""
        GLAccount.HeaderText = "GL Account"
        GLAccount.Name = colGLAcc
        GLAccount.Width = 300
        'ItemCategoryCode.ReadOnly = True
        GLAccount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvLoc.Columns.Add(GLAccount)


        GLAccountDesc.FormatString = ""
        GLAccountDesc.HeaderText = "GL Account Desc"
        GLAccountDesc.Name = colGLAccDesc
        GLAccountDesc.Width = 300
        'ItemCategoryCode.ReadOnly = True
        GLAccountDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvLoc.Columns.Add(GLAccountDesc)


        gvLoc.AllowAddNewRow = True

    End Sub

    Private Sub frmTransferLocationMapping_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub
    Sub funClose()
        Me.Close()
    End Sub

    Private Sub frmTransferLocationMapping_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadGridColumns()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")
        LoadData(obj.Frm_Loc, NavigatorType.Current)
        ' Me.gvLoc.Rows.Clear()
        'Me.gvLoc.Rows.AddNew()
    End Sub
    Public Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.ItemLocationMapping)
        If Not (MyBase.isReadFlag) Then
            '--------richa Ticket no. BM00000003121 15/07/2014 
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag

        If btnsave.Visible = True Then
            MenuItemImport.Enabled = True
            MenuItemExport.Enabled = True
        Else
            MenuItemImport.Enabled = False
            MenuItemExport.Enabled = False
        End If

        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        funClose()
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            funReset()
        Catch ex As Exception
        End Try
    End Sub

    Sub funReset()
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = True
        Me.gvLoc.Rows.Clear()
        Me.gvLoc.Rows.AddNew()
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        funReset()
        btnsave.Enabled = True
        btndelete.Enabled = True
        clsItemLoc.ObjListBOM = clsItemLoc.GetData(strCode, NavTyep)
        If (clsItemLoc.ObjListBOM IsNot Nothing) Then
            btnsave.Text = "Update"
            Dim ii As Int16 = 0
            LoadGridColumns()
            Dim i As Integer = 1
            If (clsItemLoc.ObjListBOM IsNot Nothing AndAlso clsItemLoc.ObjListBOM.Count > 0) Then
                For Each objtr As clsItemLoc In clsItemLoc.ObjListBOM
                    gvLoc.Rows.AddNew()
                    gvLoc.Rows(gvLoc.Rows.Count - 1).Cells(colSNo).Value = i
                    i = i + 1
                    gvLoc.Rows(gvLoc.Rows.Count - 1).Cells(colFrmLoc).Value = objtr.Frm_Loc
                    gvLoc.Rows(gvLoc.Rows.Count - 1).Cells(colFrmLocDesc).Value = clsLocation.GetName(objtr.Frm_Loc, Nothing)
                    gvLoc.Rows(gvLoc.Rows.Count - 1).Cells(colToLoc).Value = objtr.To_Loc
                    gvLoc.Rows(gvLoc.Rows.Count - 1).Cells(colToLocDesc).Value = clsLocation.GetName(objtr.To_Loc, Nothing)
                    gvLoc.Rows(gvLoc.Rows.Count - 1).Cells(colGLAcc).Value = objtr.GL_Acc
                    gvLoc.Rows(gvLoc.Rows.Count - 1).Cells(colGLAccDesc).Value = clsGLAccount.GetName(objtr.GL_Acc)
                Next
            Else
                gvLoc.Rows.AddNew()
            End If
        End If

    End Sub
    Sub Show_BOM_Detail(ByVal strCode As String, ByVal NavTyep As NavigatorType)

        'Dim obj1 As clsMapPayHeadsToSalaStructure
        'obj1 = clsMapPayHeadsToSalaStructure.GetData(strCode, NavTyep)
        'If (obj1 IsNot Nothing AndAlso clsCommon.myLen(obj1.SALARY_STRUCTURE_CODE) > 0) Then

        '    Dim ii As Int16 = 0
        '    LoadGridColumns()

        '    lblMasterItemName.Text = obj1.SALARY_STRUCTURE_NAME
        '    If (obj1.ObjList IsNot Nothing AndAlso obj1.ObjList.Count > 0) Then
        '        For Each obj As clsMapPayHeadsToSalaStructure In obj1.ObjList
        '            gvBOM.Rows.AddNew()
        '            gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colFrmLoc).Value = obj.LINE_NO
        '            gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colpayHeadCode).Value = obj.PAY_HEAD_CODE
        '            gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colpayHeadName).Value = obj.PAY_HEAD_NAME
        '            gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colPayHeadFormula).Value = obj.PAYHEAD_FORMULA
        '            gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colRateAmount).Value = obj.RATE_AMOUNT
        '            gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colHiddenComponent).Value = obj.IsHiddenComponent
        '        Next
        '    Else
        '        gvBOM.Rows.AddNew()
        '    End If
        'End If

    End Sub


    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SavingData(False)
    End Sub

    Public Function Save() As Boolean
        If AllowToSave() Then

            'If MyBase.isModifyonPasswordFlag Then
            '    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.ItemLocationMapping, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
            '    Else
            '        Return False
            '    End If
            'End If
            Dim obj As New clsItemLoc
            Dim obj1 As clsItemLoc
            ObjList = New List(Of clsItemLoc)
            For Each grow As GridViewRowInfo In gvLoc.Rows
                If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colFrmLoc).Value)) > 0 And clsCommon.myLen(clsCommon.myCstr(grow.Cells(colToLoc).Value)) > 0 And clsCommon.myLen(clsCommon.myCstr(grow.Cells(colGLAcc).Value)) > 0 Then
                    obj1 = New clsItemLoc()
                    obj1.Line_No = clsCommon.myCdbl(grow.Cells("S.No").Value)
                    obj1.Frm_Loc = clsCommon.myCstr(grow.Cells(colFrmLoc).Value)
                    obj1.To_Loc = clsCommon.myCstr(grow.Cells(colToLoc).Value)
                    obj1.GL_Acc = clsCommon.myCstr(grow.Cells(colGLAcc).Value)
                    ObjList.Add(obj1)
                End If
            Next
            Dim issaved As Boolean = False
            issaved = obj.SaveData(obj, ObjList, True, "")
            If issaved = False Then
                Return False
            End If
            'If OpenFileDialog1.FileName = "" And issaved = True Then
            'clsCommon.MyMessageBoxShow("Document Save Successfully.")
            LoadData(obj.Frm_Loc, NavigatorType.Current)
            Return True
            'End If
            'Return False
        End If
    End Function
    Function AllowToSave() As Boolean

        Dim ii As Int16 = 0
        For Each grow As GridViewRowInfo In gvLoc.Rows
            'If clsCommon.myCdbl(grow.Cells(colSNo).Value) > 0 Then
            If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colFrmLoc).Value)) <= 0 And clsCommon.myLen(clsCommon.myCstr(grow.Cells(colToLoc).Value)) > 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Fill All [From Location] in Grid.")
                Return False
            End If
            If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colToLoc).Value)) <= 0 And clsCommon.myLen(clsCommon.myCstr(grow.Cells(colFrmLoc).Value)) > 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Fill All [To Location] in Grid.")
                Return False
            End If
            If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colGLAcc).Value)) <= 0 And clsCommon.myLen(clsCommon.myCstr(grow.Cells(colToLoc).Value)) > 0 And clsCommon.myLen(clsCommon.myCstr(grow.Cells(colFrmLoc).Value)) > 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Fill All [GL Accounts] in Grid.")
                Return False
            End If
            If clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFrmLoc).Value), clsCommon.myCstr(grow.Cells(colToLoc).Value)) = CompairStringResult.Equal And clsCommon.myLen(clsCommon.myCstr(grow.Cells(colGLAcc).Value)) > 0 And clsCommon.myLen(clsCommon.myCstr(grow.Cells(colToLoc).Value)) > 0 And clsCommon.myLen(clsCommon.myCstr(grow.Cells(colFrmLoc).Value)) > 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Fill Diff Location For GL Account [" & clsCommon.myCstr(grow.Cells(colGLAcc).Value) & "].")
                Return False
            End If
            'End If
        Next
        Return True
    End Function



    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        'If clsCommon.myLen(txtCode.Value) <= 0 Then
        '    common.clsCommon.MyMessageBoxShow("You Cannot Delete Record")
        '    Exit Sub
        'End If
        funDelete()
    End Sub

    Sub funDelete()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                'If clsCancelLog.CheckForReasonOnDelete() Then
                '    '' REASON FOR DELETE 
                '    Dim frm As New FrmFreeTxtBox1
                '    frm.Text = "Remarks for Delete"
                '    frm.ShowDialog()
                '    If clsCommon.myLen(frm.strRmks) <= 0 Then
                '        Exit Sub
                '    Else
                '        Reason = frm.strRmks
                '    End If
                'End If
                If (clsItemLoc.DeleteData(Nothing)) Then
                    'clsCancelLog.SaveData (Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ")
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Sub SavingData(ByVal ChekBtnPost As Boolean)
        If (Save()) Then
            If ChekBtnPost = False Then
                common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully")
            End If
        End If
    End Sub

    Private Sub MenuItemClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemClose.Click
        funClose()
    End Sub

    Private Sub gvLoc_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvLoc.CellValueChanged
        If gvLoc.CurrentRow Is Nothing Then
            Exit Sub
        End If

        'If gvLoc.CurrentRow.Cells(0).Value = "" Then
        '    gvLoc.CurrentRow.Cells(0).Value = gvLoc.CurrentRow.Index + 1 '+ 1
        'End If

        If Not isCellValueChangedOpen Then
            isCellValueChangedOpen = True

            If e.Column Is gvLoc.Columns(colGLAcc) Then
                gvLoc.CurrentRow.Cells(colGLAcc).Value = clsItemLoc.FinderForGlAccount(clsCommon.myCstr(gvLoc.CurrentRow.Cells(colGLAcc).Value), "", False)
                gvLoc.CurrentRow.Cells(colGLAccDesc).Value = clsGLAccount.GetName(gvLoc.CurrentRow.Cells(colGLAcc).Value)
            End If
            If e.Column Is gvLoc.Columns(colFrmLoc) Then
                gvLoc.CurrentRow.Cells(colFrmLoc).Value = clsItemLoc.FinderForLocation(clsCommon.myCstr(gvLoc.CurrentRow.Cells(colFrmLoc).Value), "", False)
                gvLoc.CurrentRow.Cells(colFrmLocDesc).Value = clsCommon.myCstr(clsLocation.GetName(gvLoc.CurrentRow.Cells(colFrmLoc).Value, Nothing))

            End If
            If e.Column Is gvLoc.Columns(colToLoc) Then
                gvLoc.CurrentRow.Cells(colToLoc).Value = clsItemLoc.FinderForLocation(clsCommon.myCstr(gvLoc.CurrentRow.Cells(colToLoc).Value), "", False)
                gvLoc.CurrentRow.Cells(colToLocDesc).Value = clsCommon.myCstr(clsLocation.GetName(gvLoc.CurrentRow.Cells(colToLoc).Value, Nothing))
            End If
            isCellValueChangedOpen = False

        End If
    End Sub

    Private Sub gvLoc_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvLoc.CurrentColumnChanged
        If gvLoc.RowCount > 0 Then
            Dim intCurrRow As Integer = gvLoc.CurrentRow.Index
            If intCurrRow = -1 Then
                intCurrRow = gvLoc.Rows.Count - 1
            End If
            gvLoc.CurrentRow.Cells(colSNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gvLoc.Rows.Count - 1 Then
                gvLoc.Rows.AddNew()
                gvLoc.CurrentRow = gvLoc.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub MenuItemExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MenuItemExport.Click
        Try
            Dim str As String = " select Line_no as [S No],Frm_Location as [From Location],FromLoc.Location_Desc as [Location Desc],To_Location as [To Location] ,ToLoc.Location_Desc as [To Location Desc],GL_Acc as [GL Account],TSPL_GL_ACCOUNTS.Description as [Account Desc] from tspl_item_location_mapping left outer join TSPL_LOCATION_MASTER FromLoc on FromLoc.Location_Code=Frm_Location left outer join TSPL_LOCATION_MASTER ToLoc on ToLoc.Location_Code=To_Location left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=GL_Acc "
            ListImpExpColumnsMandatory = New List(Of String)({"From Location", "To Location", "GL Account"})
            ListImpExpColumnsSuperMandatory = New List(Of String)({"From Location", "To Location", "GL Account"})
            transportSql.ExporttoExcel(str, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub MenuItemImport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MenuItemImport.Click
        Dim gv As New RadGridView()
        Dim intCounter As Integer = 1
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        Dim obj1 As clsItemLoc = Nothing
        ObjList = New List(Of clsItemLoc)
        Try
            If transportSql.importExcel(gv, "S No", "From Location", "To Location", "GL Account") Then
                ' Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Try
                    Dim strSno As Integer
                    Dim qry As String = "select COUNT(*) from tspl_item_location_mapping"
                    Dim i As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
                    ' If strSno <= 0 Then
                    strSno = 0
                    'End If
                    'clsCommon.ProgressBarPercentShow()

                    Dim arrVisi As New List(Of String)
                    For Each grow As GridViewRowInfo In gv.Rows
                        'Dim strSno As Integer = clsCommon.myCdbl(grow.Cells("S No").Value)
                        strSno = strSno + 1
                        Dim strFrm_Loc As String = clsCommon.myCstr(grow.Cells("From Location").Value)
                        If strFrm_Loc = "" Then
                            Throw New Exception("Please Fill The 'From Location' At Row No:  '" & strSno & "' ")
                        End If
                        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select count(*) from tspl_location_master where location_code='" & strFrm_Loc & "'")) = 0 Then
                            Throw New Exception(" 'From Location' " & strFrm_Loc & " Not found in Master At Row No:  '" & strSno & "' ")
                        End If
                        'If clsCommon.myLen(strFrm_Loc) > 50 Then
                        '    Throw New Exception("Check the length of 'From Location' for '" + strSno + "'")
                        'End If

                        Dim strTo_Loc As String = clsCommon.myCstr(grow.Cells("To Location").Value)
                        If strTo_Loc = "" Then
                            Throw New Exception("Please Fill The 'To Location' At Row No:  '" & strSno & "' ")
                        End If
                        'If clsCommon.myLen(strTo_Loc) > 50 Then
                        '    Throw New Exception("Check the length of 'To Location' for '" + strSno + "'")
                        'End If
                        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select count(*) from tspl_location_master where location_code='" & strTo_Loc & "'")) = 0 Then
                            Throw New Exception(" 'To Location' " & strTo_Loc & " Not found in Master At Row No:  '" & strSno & "' ")
                        End If

                        Dim str_GLAcc As String = clsCommon.myCstr(grow.Cells("GL Account").Value)
                        If str_GLAcc = "" Then
                            Throw New Exception("Please Fill The 'GL Account' At Row No:  '" & strSno & "' ")
                        End If
                        'If clsCommon.myLen(strTo_Loc) > 50 Then
                        '    Throw New Exception("Check the length of 'GL Account' for '" + strSno + "'")
                        'End If
                        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select COUNT(*) from TSPL_GL_ACCOUNTS where Account_Code='" & str_GLAcc & "'")) = 0 Then
                            Throw New Exception(" 'GL Account' " & str_GLAcc & " Not found in Master At Row No:  '" & strSno & "' ")
                        End If
                        obj1 = New clsItemLoc
                        obj1.Line_No = clsCommon.myCdbl(strSno)
                        obj1.Frm_Loc = clsCommon.myCstr(strFrm_Loc)
                        obj1.To_Loc = clsCommon.myCstr(strTo_Loc)
                        obj1.GL_Acc = clsCommon.myCstr(str_GLAcc)
                        ObjList.Add(obj1)

                        'Dim isAssetExits As String = clsDBFuncationality.getSingleValue("Select count(*) from tspl_item_location_mapping  Where Frm_Location='" + strFrm_Loc + " and To_Location='" + strTo_Loc + "'")
                        'If isAssetExits > 0 Then
                        '    Dim strQry As String = "Update tspl_item_location_mapping set  GL_Acc='" + str_GLAcc + "' Where Frm_Location='" + strFrm_Loc + "' and To_Location='" + strTo_Loc + "'"
                        '    clsDBFuncationality.ExecuteNonQuery(strQry)
                        'Else
                        '    'Dim obj As New clsItemLoc
                        '    If clsCommon.myLen(clsCommon.myCstr(strFrm_Loc)) > 0 And clsCommon.myLen(clsCommon.myCstr(strTo_Loc)) > 0 Then
                        '        obj1 = New clsItemLoc()
                        '        obj1.Line_No = clsCommon.myCdbl(strSno)
                        '        obj1.Frm_Loc = clsCommon.myCstr(strFrm_Loc)
                        '        obj1.To_Loc = clsCommon.myCstr(strTo_Loc)
                        '        obj1.GL_Acc = clsCommon.myCstr(str_GLAcc)
                        '        ObjList.Add(obj1)
                        '    End If
                        '    obj.SaveData(obj1, ObjList, True, "")
                        'End If
                    Next
                    If obj1.SaveData(obj1, ObjList, True, "") Then
                        '   clsCommon.ProgressBarHide()
                        common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                        LoadData(obj1.Frm_Loc, NavigatorType.Current)
                    End If

                Catch ex As Exception
                    'trans.Rollback()
                    ' clsCommon.ProgressBarHide()
                    common.clsCommon.MyMessageBoxShow(Me, "Error at row no:" + clsCommon.myCstr(intCounter) + Environment.NewLine + ex.Message)
                    '' myMessages.myExceptions(ex)
                    LoadData("", NavigatorType.Current)
                End Try
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            Me.Controls.Remove(gv)
        End Try
    End Sub
End Class