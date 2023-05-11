'--11/10/2013--form Add By- Pradeep Sharma ---------
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class frmCurrencyMaster
    Inherits FrmMainTranScreen
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
        If AllowToSave() Then
            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmCurrencyMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return
                End If
            End If
            Dim obj As New clsCurrencyMaster()
            obj.Code = txtCode.Value
            obj.Description = txtDescription.Text
            obj.Name = txtName.Text
            obj.CURRENCY_SIGN = cboSymbol.SelectedValue
            If (obj.SaveData(obj, isNewEntry)) Then
                common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                LoadData(obj.Code, NavigatorType.Current)
                'Else  € £
                '    common.clsCommon.MyMessageBoxShow("This '" & obj.Code & "' already exist ")
            End If

        End If
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        funReset()
        txtCode.MyReadOnly = True
        btnSave.Enabled = True
        btnDelete.Enabled = True
        isNewEntry = False
        Dim obj As New clsCurrencyMaster()
        obj = clsCurrencyMaster.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
            funReset()
            isNewEntry = False
            btnSave.Text = "Update"
            txtCode.Value = obj.Code
            txtName.Text = obj.Name
            txtDescription.Text = obj.Description
            cboSymbol.SelectedValue = obj.CURRENCY_SIGN
        End If
    End Sub

    Function AllowToSave() As Boolean
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            myMessages.blankValue("Code")
            txtCode.Focus()
            Return False
        ElseIf clsCommon.myLen(txtName.Text) <= 0 Then
            myMessages.blankValue("Currency Name")
            txtName.Focus()
            Return False
        End If
        Return True
    End Function


    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("You Cannot Delete Record")
            Exit Sub
        End If
        'Dim discCode As String
        'discCode = clsDBFuncationality.getSingleValue("select CURRENCY_CODE  from TSPL_SHIPMENT_DETAILS  where CURRENCY_CODE ='" & txtCode.Value & "'")
        'If clsCommon.myLen(discCode) > 0 Then
        '    common.clsCommon.MyMessageBoxShow("This record can't be deleted.It is used in another process")
        '    Exit Sub
        'End If
        '' Code Ends 
        funDelete()
    End Sub

    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsCurrencyMaster.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub frmCurrencyMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        funReset()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ' ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")

    End Sub

    Private Sub SetUserMgmtNew()
        '' Anubhooti 31-July-2014 BM00000003131
        'MyBase.SetUserMgmt(clsUserMgtCode.frmCurrencyMaster)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If
        btnSave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 01/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnSave.Visible = True Then
            MenuItemImport.Enabled = True
            MenuItemExport.Enabled = True
        Else
            MenuItemImport.Enabled = False
            MenuItemExport.Enabled = False
        End If
        '--------------------------------------------------
        'btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        funReset()
    End Sub

    Sub funReset()
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        txtName.Text = ""
        txtDescription.Text = ""
        cboSymbol.DataSource = GetCurrencySymbol()
        cboSymbol.ValueMember = "Code"
        cboSymbol.DisplayMember = "Value"
        'cboSymbol.Font= Rupee Foradian, Rupee

        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = True
    End Sub

    Private Function GetCurrencySymbol() As DataTable

        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("value", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "`"
        dr("value") = "`"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "$"
        dr("value") = "$"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "€"
        dr("value") = "€"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "¥"
        dr("value") = "¥"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "£"
        dr("value") = "£"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "ƒ"
        dr("value") = "ƒ"
        dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "¢"
        'dr("value") = "¢"
        'dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "₪"
        dr("value") = "₪"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "₩"
        dr("value") = "₩"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "₭"
        dr("value") = "₭"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Rs"
        dr("value") = "Rs"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "₮"
        dr("value") = "₮"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "₦"
        dr("value") = "₦"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "₱"
        dr("value") = "₱"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "฿"
        dr("value") = "฿"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "₫"
        dr("value") = "₫"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "лв"
        dr("value") = "лв"
        dt.Rows.Add(dr)

        dt.AcceptChanges()

        Return dt
    End Function

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        funClose()

    End Sub

    Sub funClose()
        Me.Close()
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating

        Dim str As String = "select count(*) from TSPL_CURRENCY_MASTER where CURRENCY_CODE ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then

            'Dim qry As String = "select CURRENCY_CODE AS Code, CURRENCY_NAME AS Name, DESCRIPTION from TSPL_CURRENCY_MASTER"
            'txtCode.Value = clsCommon.ShowSelectForm("CURRENCY_MASTER", qry, "Code", "", txtCode.Value, "CURRENCY_CODE", isButtonClicked)
            txtCode.Value = clsCurrencyMaster.getFinder("", txtCode.Value, isButtonClicked)
            If txtCode.Value <> "" Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                funReset()
            End If
        End If


    End Sub

    Sub funFill()

    End Sub

    Private Sub txtCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub frmCurrencyMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Private Sub MenuItemImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Code", "Currency Name", "Description", "Symbol") Then
            'Dim trans As SqlTransaction
            Try
                'connectSql.OpenConnection()
                'trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clsCurrencyMaster()

                    Dim strCode As String = clsCommon.myCstr(grow.Cells(0).Value)
                    If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                        Throw New Exception("Code can not be blank or incorrect.")
                    End If
                    obj.Code = strCode

                    Dim strName As String = clsCommon.myCstr(grow.Cells(1).Value)
                    If strName.Length > 100 Or (String.IsNullOrEmpty(strName)) Then
                        Throw New Exception("Name can not be blank or incorrect.")
                    End If
                    obj.Name = strName


                    Dim strDes As String = clsCommon.myCstr(grow.Cells(2).Value)
                    If strDes.Length > 200 Then
                        Throw New Exception("Description can not be blank or incorrect.")
                    End If
                    obj.Description = strDes

                    strDes = clsCommon.myCstr(grow.Cells(3).Value)
                    If strDes.Length > 100 Then
                        Throw New Exception("Description can not be blank or incorrect.")
                    End If
                    obj.CURRENCY_SIGN = strDes

                    obj.SaveData(obj, clsCurrencyMaster.CheckNewEntry(obj.Code))
                Next
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub MenuItemExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemExport.Click
        Dim str As String
        str = "select CURRENCY_CODE AS Code, CURRENCY_NAME AS [Currency Name], DESCRIPTION as Description, CURRENCY_SIGN as [Symbol]  from TSPL_CURRENCY_MASTER"
        transportSql.ExporttoExcel(str, Me)

    End Sub

    Private Sub MenuItemClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemClose.Click
        funClose()

    End Sub

    Private Sub cboSymbol_VisualListItemFormatting(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.VisualItemFormattingEventArgs) Handles cboSymbol.VisualListItemFormatting
        args.VisualItem.Font = New System.Drawing.Font("Rupee", 9.25!)
    End Sub
End Class
