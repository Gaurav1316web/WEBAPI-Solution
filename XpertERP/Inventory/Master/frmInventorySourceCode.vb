Imports common
Imports System.Data.SqlClient

Public Class frmInventorySourceCode
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim userCode, companyCode As String
#End Region
    Private Sub fndaccgp__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndaccgp._MYNavigator
        Try
            LoadData(fndaccgp.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndaccgp__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndaccgp._MYValidating
        Dim str As String = "select count(*) from TSPL_INVENTORY_SOURCE_CODE where Code ='" + fndaccgp.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            fndaccgp.MyReadOnly = False
        Else
            fndaccgp.MyReadOnly = True
        End If
        If fndaccgp.MyReadOnly OrElse isButtonClicked Then
            fndaccgp.Value = clsInventorySourceCode.getFinder("", fndaccgp.Value, isButtonClicked)
            If fndaccgp.Value <> "" Then
                LoadData(fndaccgp.Value, NavigatorType.Current)
            Else
                funReset()
            End If
        End If
    End Sub

    Private Sub FrmAccountMainGroup_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Private Sub FrmAccountMainGroup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        funReset()
        LoadInOutType()
        LoadInCategory()
        LoadOutCategory()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Transaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D for Delete Transaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N for New Transaction")
    End Sub


    Sub LoadOutCategory()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "SA"
        dr("Name") = "Sale"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "IS"
        dr("Name") = "Issue/Adjustment"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "OT"
        dr("Name") = "Other"
        dt.Rows.Add(dr)

        cboOutCatg.DataSource = dt
        cboOutCatg.ValueMember = "Code"
        cboOutCatg.DisplayMember = "Name"
    End Sub

    Sub LoadInCategory()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "PU"
        dr("Name") = "Purchase"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "AD"
        dr("Name") = "Production/Adjustment"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "OT"
        dr("Name") = "Other"
        dt.Rows.Add(dr)

        cboInCatg.DataSource = dt
        cboInCatg.ValueMember = "Code"
        cboInCatg.DisplayMember = "Name"
    End Sub
    Sub LoadInOutType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "All"
        dr("Name") = "All"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "In"
        dr("Name") = "In"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Out"
        dr("Name") = "Out"
        dt.Rows.Add(dr)

        cboInOutType.DataSource = dt
        cboInOutType.ValueMember = "Code"
        cboInOutType.DisplayMember = "Name"
    End Sub


#Region "Functions"
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.InvetorySourceCode)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnsave.Visible = MyBase.isModifyFlag

        If btnsave.Visible = True Then
            rmExport.Enabled = True
            rmImport.Enabled = True
        Else
            rmExport.Enabled = False
            rmImport.Enabled = False
        End If

        btndelete.Visible = MyBase.isDeleteFlag
    End Sub
    Function AllowToSave() As Boolean
        Dim strcode As String = fndaccgp.Value.ToString()
        If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) AndAlso clsCommon.myLen(fndaccgp.Value) <= 0 Then
            myMessages.blankValue("Code")
            fndaccgp.Focus()
            Return False
        ElseIf clsCommon.myLen(txtdes.Text) <= 0 Then
            myMessages.blankValue("Description")
            txtdes.Focus()
            Return False
        ElseIf clsCommon.myLen(txtSequence.Text) <= 0 Then
            myMessages.blankValue("Sequence")
            txtdes.Focus()
            Return False
        End If
        ''=======Parteek Added in 23-11-2016
        Dim qry As String = ""
        qry = "Select Name from TSPL_INVENTORY_SOURCE_CODE where Sequence='" & clsCommon.myCdbl(txtSequence.Text) & "'"
        Dim FinalName As String = clsDBFuncationality.getSingleValue(qry)
        If clsCommon.myLen(FinalName) > 0 Then
            clsCommon.MyMessageBoxShow("Sequence already Added in " + FinalName)
            Return False
        End If
        ''==========End

        Return True
    End Function
    Private Sub DeleteData()
        Try
            If (deleteConfirm()) Then
                If (clsInventorySourceCode.DeleteData(fndaccgp.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    funReset()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub funReset()
        isNewEntry = True
        fndaccgp.MyReadOnly = False
        fndaccgp.Value = Nothing
        fndaccgp.Focus()
        txtdes.Text = ""
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btndelete.Enabled = False
        cboInOutType.SelectedValue = "All"
        txtSequence.Text = ""
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)

        Dim obj As New clsInventorySourceCode()
        obj = clsInventorySourceCode.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
            funReset()
            isNewEntry = False
            btnsave.Text = "Update"
            fndaccgp.Value = clsCommon.myCstr(obj.Code)
            txtdes.Text = clsCommon.myCstr(obj.Name)
            cboInOutType.SelectedValue = obj.InOutType
            txtType.Text = obj.Type
            txtSequence.Text = obj.Sequence
            cboInCatg.SelectedValue = obj.In_Category
            cboOutCatg.SelectedValue = obj.Out_Category
            btnsave.Enabled = True
            btndelete.Enabled = True
            fndaccgp.MyReadOnly = True
        End If
    End Sub

    Public Sub SaveData()
        If AllowToSave() Then

            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.InvetorySourceCode, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return
                End If
            End If
            Dim obj As New clsInventorySourceCode()
            obj.Code = fndaccgp.Value
            obj.Name = txtdes.Text
            obj.Type = txtType.Text
            obj.InOutType = clsCommon.myCstr(cboInOutType.SelectedValue)

            obj.In_Category = clsCommon.myCstr(cboInCatg.SelectedValue)
            obj.Out_Category = clsCommon.myCstr(cboOutCatg.SelectedValue)
            obj.Sequence = clsCommon.myCdbl(txtSequence.Text)
            If (clsInventorySourceCode.SaveData(obj, isNewEntry)) Then
                common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                LoadData(obj.Code, NavigatorType.Current)
            End If
        End If
    End Sub
    Sub funClose()
        Me.Close()
        GC.Collect()
    End Sub
#End Region

    Private Sub rmImport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rmImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        Dim linno As Integer = 0
        If transportSql.importExcel(gv, "Code", "Name", "InOutType", "Type") Then
            Dim trans As SqlTransaction = Nothing
            Try
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clsInventorySourceCode()
                    linno += 1
                    Dim strcode As String = clsCommon.myCstr(grow.Cells(0).Value)
                    If clsCommon.myLen(strcode) > 0 Then
                        If strcode.Length <= 0 Or (String.IsNullOrEmpty(strcode)) Then
                            Throw New Exception("code can't be blank at line no. " + clsCommon.myCstr(linno) + ".")
                        ElseIf strcode.Length > 20 Then
                            Throw New Exception("Code length can not be more than 20 at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        obj.Code = strcode

                        Dim strdes As String = clsCommon.myCstr(grow.Cells(1).Value)
                        If strdes.Length <= 0 Or (String.IsNullOrEmpty(strdes)) Then
                            Throw New Exception("Name can't be blank at line no. " + clsCommon.myCstr(linno) + ".")
                        ElseIf strdes.Length > 100 Then
                            Throw New Exception("Name length can not be more than 100 at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        obj.Name = strdes

                        obj.InOutType = clsCommon.myCstr(grow.Cells(2).Value)
                        If obj.InOutType.Length <= 0 Or (String.IsNullOrEmpty(obj.InOutType)) Then
                            Throw New Exception("In/Out Type can't be blank at line no. " + clsCommon.myCstr(linno) + ".")
                        ElseIf obj.InOutType.Length > 20 Then
                            Throw New Exception("In/Out length can not be more than 20 at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        If Not ((clsCommon.CompairString(obj.InOutType, "In") = CompairStringResult.Equal) OrElse (clsCommon.CompairString(obj.InOutType, "Out") = CompairStringResult.Equal) OrElse (clsCommon.CompairString(obj.InOutType, "All") = CompairStringResult.Equal)) Then
                            Throw New Exception("In/Out should be All/In/Out at line no. " + clsCommon.myCstr(linno) + ".")
                        End If

                        obj.In_Category = clsCommon.myCstr(grow.Cells(2).Value)
                        obj.Out_Category = clsCommon.myCstr(grow.Cells(2).Value)

                        If clsCommon.CompairString(obj.InOutType, "In") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.InOutType, "All") = CompairStringResult.Equal Then
                            If clsCommon.myLen(obj.In_Category) <= 0 Then
                                Throw New Exception("In Category can't be blank at line no. " + clsCommon.myCstr(linno) + ".")
                            ElseIf strdes.Length > 2 Then
                                Throw New Exception("In Category length can not be more than 2 at line no. " + clsCommon.myCstr(linno) + ".")
                            End If

                            If Not (clsCommon.CompairString(obj.In_Category, "PU") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.In_Category, "AD") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.In_Category, "OT") = CompairStringResult.Equal) Then
                                Throw New Exception("In Category Should be PU,AD or OT " + clsCommon.myCstr(linno) + ".")
                            End If
                        End If

                        If clsCommon.CompairString(obj.InOutType, "Out") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.InOutType, "All") = CompairStringResult.Equal Then
                            If clsCommon.myLen(obj.Out_Category) <= 0 Then
                                Throw New Exception("Out Category can't be blank at line no. " + clsCommon.myCstr(linno) + ".")
                            ElseIf strdes.Length > 2 Then
                                Throw New Exception("Out Category length can not be more than 2 at line no. " + clsCommon.myCstr(linno) + ".")
                            End If

                            If Not (clsCommon.CompairString(obj.Out_Category, "SA") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Out_Category, "IS") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Out_Category, "OT") = CompairStringResult.Equal) Then
                                Throw New Exception("Out Category Should be SA,IS or OT " + clsCommon.myCstr(linno) + ".")
                            End If
                        End If

                        obj.Type = clsCommon.myCstr(grow.Cells(3).Value)
                        If obj.Type.Length > 100 Then
                            Throw New Exception("Type length can not be more than 100 at line no. " + clsCommon.myCstr(linno) + ".")
                        End If

                        clsInventorySourceCode.SaveData(obj, clsInventorySourceCode.CheckNewEntry(obj.Code, trans), trans)
                    End If
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub rmExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rmExport.Click
        Dim str As String
        str = "select Code ,Name,InOutType,Type,In_Category,Out_Category from TSPL_INVENTORY_SOURCE_CODE"
        ListImpExpColumnsMandatory = New List(Of String)({"Code", "Name", "InOutType"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"Code"})
        transportSql.ExporttoExcel(str, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
    End Sub

    Private Sub btnnew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnnew.Click
        funReset()
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        funClose()
    End Sub

    Private Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub btndelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub cboInOutType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles cboInOutType.SelectedIndexChanged
        SetInOutCategory()
    End Sub

    Private Sub cboInOutType_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboInOutType.SelectedValueChanged
        SetInOutCategory()
    End Sub

    Private Sub SetInOutCategory()
        If clsCommon.CompairString(clsCommon.myCstr(cboInOutType.SelectedValue), "All") = CompairStringResult.Equal Then
            SplitContainer2.Panel1Collapsed = False
            SplitContainer2.Panel2Collapsed = False
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboInOutType.SelectedValue), "In") = CompairStringResult.Equal Then
            SplitContainer2.Panel1Collapsed = False
            SplitContainer2.Panel2Collapsed = True
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboInOutType.SelectedValue), "Out") = CompairStringResult.Equal Then
            SplitContainer2.Panel1Collapsed = True
            SplitContainer2.Panel2Collapsed = False
        End If

    End Sub

End Class
