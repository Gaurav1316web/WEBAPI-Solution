Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Public Class frmMilkRejectType
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()

#Region "Variable"
    Private isNewEntry As Boolean = False
#End Region

    Private Sub frmJWPriceCodeMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim dt As DataTable
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update")
        ButtonToolTip.SetToolTip(rdbtndelete, "Press Alt+D  for Delete")
        ButtonToolTip.SetToolTip(rdbtnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(rdbtnreset, "Press Alt+N Adding New")

        dt = clsMilkReceiptMCC.GetMilkType()
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.InsertAt(dr, 0)
        cboType.DataSource = dt
        cboType.ValueMember = "Code"
        cboType.DisplayMember = "Name"

        If objCommonVar.DisplayTypeInMilkReceipt Then
            cboType.Visible = True
            Lbl_Type.Visible = True
        End If

        funReset()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Save()
    End Sub

    Public Sub Save()
        If AllowToSave() Then
            Dim obj As New clsMilkRejectType()
            obj.Code = txtCode.Value
            obj.Description = txtDescription.Text
            obj.Descriptionhindi = txtDescriptionhindi.Text
            obj.Item_Code = txtItem.Value
            obj.Applicable_On = 0
            If rbtnRate.IsChecked Then
                obj.Applicable_On = 1
            ElseIf rbtnFATKGRate.IsChecked Then
                obj.Applicable_On = 2
            ElseIf rbtnSNFKGRate.IsChecked Then
                obj.Applicable_On = 3
            End If
            obj.Include_In_DBT = chkIncludeInDBT.Checked
            obj.Exclude_Head = chkExcludeHead.Checked
            obj.Applicable_Per = txtApplicablePer.Value
            obj.Type = clsCommon.myCstr(cboType.SelectedValue)
            obj.SNo = txtSNo.Value
            obj.Prefix = txtPrefix.Value
            If (clsMilkRejectType.SaveData(obj)) Then
                common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                LoadData(obj.Code, NavigatorType.Current)
            End If
        End If
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        funReset()
        txtCode.MyReadOnly = True
        btnSave.Enabled = True
        rdbtndelete.Enabled = True
        isNewEntry = False
        Dim obj As New clsMilkRejectType()
        obj = clsMilkRejectType.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
            funReset()
            isNewEntry = False
            btnSave.Text = "Update"
            rdbtndelete.Enabled = True
            txtCode.Value = obj.Code
            txtDescription.Text = obj.Description
            txtDescriptionhindi.Text = obj.Descriptionhindi
            If obj.Applicable_On = 1 Then
                rbtnRate.IsChecked = True
            ElseIf obj.Applicable_On = 2 Then
                rbtnFATKGRate.IsChecked = True
            ElseIf obj.Applicable_On = 3 Then
                rbtnSNFKGRate.IsChecked = True
            Else
                rbtnPer.IsChecked = True
            End If
            chkIncludeInDBT.Checked = obj.Include_In_DBT
            chkExcludeHead.Checked = obj.Exclude_Head
            txtApplicablePer.Value = obj.Applicable_Per
            txtItem.Value = obj.Item_Code
            lblItem.Text = clsItemMaster.GetItemName(obj.Item_Code, Nothing)
            cboType.SelectedValue = obj.Type
            txtSNo.Value = obj.SNo
            txtPrefix.Value = obj.Prefix
        End If
    End Sub

    Function AllowToSave() As Boolean
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            myMessages.blankValue(Me, "Code", Me.Text)
            txtCode.Focus()
            Return False
        End If
        If clsCommon.myLen(txtDescription.Text) <= 0 Then
            myMessages.blankValue(Me, "Description", Me.Text)
            txtDescription.Focus()
            Return False
        End If
        If clsCommon.myLen(txtCode.Value) > 20 Then
            clsCommon.MyMessageBoxShow(Me, "Length is greater then 20.", Me.Text)
            txtCode.Focus()
            Return False
        End If
        Return True
    End Function

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtndelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "You Cannot Delete Record")
            Exit Sub
        End If
        ' Code Ends 
        funDelete()
    End Sub

    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsMilkRejectType.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ")
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating

        Dim str As String = "select count(*) from TSPL_MILK_REJECT_TYPE where Code ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            txtCode.Value = clsMilkRejectType.getFinder("", txtCode.Value, isButtonClicked)
            If txtCode.Value <> "" Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                funReset()
            End If
        End If
    End Sub

    Private Sub txtCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub funReset()
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        txtDescription.Text = ""
        txtDescriptionhindi.Text = ""
        txtItem.Value = ""
        lblItem.Text = ""
        btnsave.Text = "Save"
        btnsave.Enabled = True
        rdbtndelete.Enabled = False
        txtApplicablePer.Value = 0
        cboType.SelectedValue = ""
        txtSNo.Value = 0
        txtPrefix.Value = 0
        txtSNo.MendatroryField = False
        rbtnPer.IsChecked = True
        chkIncludeInDBT.Checked = False
    End Sub

    Private Sub frmHSNMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso rdbtnreset.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso rdbtndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        Import.Enabled = MyBase.isModifyFlag
        Export.Enabled = MyBase.isModifyFlag
        rdbtndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub rdbtnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnclose.Click
        Me.Close()
    End Sub

    Private Sub rdbtnreset_Click(sender As Object, e As EventArgs) Handles rdbtnreset.Click
        funReset()
    End Sub

    Private Sub MenuItemExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export.Click
        Dim str As String
        str = "select Code,Description,Item_Code as [Item Code],Applicable_Per as [Applicable%] , Prefix"
        If objCommonVar.DisplayTypeInMilkReceipt Then
            str += ",Type"
        End If
        str += " from TSPL_MILK_REJECT_TYPE"
        ListImpExpColumnsMandatory = New List(Of String)({"Code", "Item Code", "Applicable%", "Description", "Prefix"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"Code"})
        transportSql.ExporttoExcel(str, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
    End Sub

    Private Sub MenuItemImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Import.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        Dim flag As Boolean = False
        If objCommonVar.DisplayTypeInMilkReceipt Then
            flag = transportSql.importExcel(gv, "Code", "Description", "Item Code", "Applicable%", "Type", "Prefix")
        Else
            flag = transportSql.importExcel(gv, "Code", "Description", "Item Code", "Applicable%" , "Prefix")
        End If

        If flag Then
            Try
                clsCommon.ProgressBarShow()
                Dim ii As Integer = 0
                Try
                    For Each grow As GridViewRowInfo In gv.Rows
                        ii = ii + 1
                        Dim obj As New clsMilkRejectType()
                        obj.Code = clsCommon.myCstr(grow.Cells("Code").Value)
                        If clsCommon.myLen(obj.Code) <= 0 Then
                            Continue For
                        End If
                        If clsCommon.myLen(obj.Code) > 10 Then
                            Throw New Exception("length greater then 10.")
                        End If
                        obj.Description = clsCommon.myCstr(grow.Cells("Description").Value)
                        obj.Item_Code = clsCommon.myCstr(grow.Cells("Item Code").Value)
                        If clsCommon.myLen(obj.Item_Code) <= 0 Then
                            Throw New Exception("Please enter Item code")
                        End If
                        obj.Item_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Code from TSPL_ITEM_MASTER where Item_Code='" + obj.Item_Code + "' and Product_Type ='MI'"))
                        If clsCommon.myLen(obj.Item_Code) <= 0 Then
                            Throw New Exception("Invalid Item code - " + clsCommon.myCstr(grow.Cells("Item Code").Value))
                        End If
                        obj.Applicable_Per = clsCommon.myCdbl(grow.Cells("Applicable%").Value)
                        If obj.Applicable_Per <= 0 Then
                            Throw New Exception("Invalid tare weight: " + clsCommon.myCstr(obj.Applicable_Per))
                        End If
                        If clsCommon.myLen(obj.Description) <= 0 Then
                            Throw New Exception("Description can not be blank or incorrect.")
                        End If
                        If objCommonVar.DisplayTypeInMilkReceipt Then
                            obj.Type = clsCommon.myCstr(grow.Cells("Type").Value).ToUpper()
                            If Not (clsCommon.CompairString(obj.Type, "M") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Type, "B") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Type, "C") = CompairStringResult.Equal) Then
                                Throw New Exception("Type Should be M/B/C.")
                            End If
                        End If
                        obj.Prefix = clsCommon.myCdbl(grow.Cells("Prefix").Value)

                        clsMilkRejectType.SaveData(obj)
                    Next
                Catch ex As Exception
                    Throw New Exception("At Row No" + clsCommon.myCstr(ii) + " " + ex.Message)
                End Try
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select code", Me.Text)
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowHistoryData(txtCode.Value, "Code", "TSPL_MILK_REJECT_TYPE")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub txtItem__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtItem._MYValidating
        Dim qry As String = "select Item_Code as Code,Item_Desc as [Item Desc] from TSPL_ITEM_MASTER"
        txtItem.Value = clsCommon.ShowSelectForm("Item Code", qry, "Code", "Product_Type ='MI'", txtItem.Value, "", isButtonClicked)
        lblItem.Text = clsDBFuncationality.getSingleValue("select Item_Desc  from TSPL_ITEM_MASTER where Item_Code='" + txtItem.Value + "'")
    End Sub

    Private Sub SplitContainer1_Panel1_Paint(sender As Object, e As PaintEventArgs) Handles SplitContainer1.Panel1.Paint

    End Sub
End Class
