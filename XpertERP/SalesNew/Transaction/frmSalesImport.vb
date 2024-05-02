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
'''' <summary>
'''' ''''''''''''''''''''''''TicketNo='BM00000002070''''''''''''''''''''''''''''''''''''''''
'''' </summary>
'''' <remarks></remarks>

Public Class frmSalesImport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Const colOrder_No As String = "colOrder_No"
    Const colCust_Code As String = "colCust_Code"
    Const colLocation As String = "colLocation"
    Const colShip_To As String = "colShip_To"
    Const colRef_No As String = "colRef_No"
    Const colDescription As String = "colDescription"
    Const colMode_Of_Transport As String = "colMode_Of_Transport"
    Const colTax_Group As String = "colTax_Group"
    Const colItemCode1 As String = "colItemCode1"
    Const colItemDesc1 As String = "colItemDesc1"
    Const colItemQty1 As String = "colItemQty1"
    Const colItemRate1 As String = "colItemRate1"

    Const colItemCode2 As String = "colItemCode2"
    Const colItemDesc2 As String = "colItemDesc2"
    Const colItemQty2 As String = "colItemQty2"
    Const colItemRate2 As String = "colItemRate2"

    Const colItemCode3 As String = "colItemCode3"
    Const colItemDesc3 As String = "colItemDesc3"
    Const colItemQty3 As String = "colItemQty3"
    Const colItemRate3 As String = "colItemRate3"

    Const colItemCode4 As String = "colItemCode4"
    Const colItemDesc4 As String = "colItemDesc4"
    Const colItemQty4 As String = "colItemQty4"
    Const colItemRate4 As String = "colItemRate4"

    Const colItemCode5 As String = "colItemCode5"
    Const colItemDesc5 As String = "colItemDesc5"
    Const colItemQty5 As String = "colItemQty5"
    Const colItemRate5 As String = "colItemRate5"

    Const colItemCode6 As String = "colItemCode6"
    Const colItemDesc6 As String = "colItemDesc6"
    Const colItemQty6 As String = "colItemQty6"
    Const colItemRate6 As String = "colItemRate6"

    Const colItemCode7 As String = "colItemCode7"
    Const colItemDesc7 As String = "colItemDesc7"
    Const colItemQty7 As String = "colItemQty7"
    Const colItemRate7 As String = "colItemRate7"

    Const colItemCode8 As String = "colItemCode8"
    Const colItemDesc8 As String = "colItemDesc8"
    Const colItemQty8 As String = "colItemQty8"
    Const colItemRate8 As String = "colItemRate8"

    Const colItemCode9 As String = "colItemCode9"
    Const colItemDesc9 As String = "colItemDesc9"
    Const colItemQty9 As String = "colItemQty9"
    Const colItemRate9 As String = "colItemRate9"

    Const colItemCode10 As String = "colItemCode10"
    Const colItemDesc10 As String = "colItemDesc10"
    Const colItemQty10 As String = "colItemQty10"
    Const colItemRate10 As String = "colItemRate10"

#Region "Functions"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmSalesImport)
        'If Not (MyBase.isReadFlag) Then
        '    common.clsCommon.MyMessageBoxShow("Permission Denied")
        '    Me.Close()
        'End If
        'btnsave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btndelete.Visible = MyBase.isDeleteFlag
    End Sub
    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoOrder_No As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoOrder_No.FormatString = ""
        repoOrder_No.HeaderText = "Order No"
        repoOrder_No.Name = colOrder_No
        repoOrder_No.ReadOnly = True
        repoOrder_No.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoOrder_No)

        Dim repoCust_Code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCust_Code.FormatString = ""
        repoCust_Code.HeaderText = "Customer Code"
        repoCust_Code.Name = colCust_Code
        repoCust_Code.Width = 100
        gv1.MasterTemplate.Columns.Add(repoCust_Code)

        Dim repoLocation As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLocation.FormatString = ""
        repoLocation.HeaderText = "Location Code"
        repoLocation.Name = colLocation
        repoLocation.Width = 100
        gv1.MasterTemplate.Columns.Add(repoLocation)

        Dim repoShipToLocation As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoShipToLocation.FormatString = ""
        repoShipToLocation.HeaderText = "Ship To Location"
        repoShipToLocation.Name = colShip_To
        repoShipToLocation.Width = 100
        gv1.MasterTemplate.Columns.Add(repoShipToLocation)

        Dim repoReferenceNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoReferenceNo.FormatString = ""
        repoReferenceNo.HeaderText = "Reference No"
        repoReferenceNo.Name = colRef_No
        repoReferenceNo.Width = 100
        gv1.MasterTemplate.Columns.Add(repoReferenceNo)

        Dim repoDescription As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDescription.FormatString = ""
        repoDescription.HeaderText = "Description"
        repoDescription.Name = colDescription
        repoDescription.Width = 100
        gv1.MasterTemplate.Columns.Add(repoDescription)

        Dim repoTransportMode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTransportMode.FormatString = ""
        repoTransportMode.HeaderText = "Mode of Transport"
        repoTransportMode.Name = colMode_Of_Transport
        repoTransportMode.Width = 100
        gv1.MasterTemplate.Columns.Add(repoTransportMode)

        Dim repoTaxGroup As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTaxGroup.FormatString = ""
        repoTaxGroup.HeaderText = "Tax Group"
        repoTaxGroup.Name = colTax_Group
        repoTaxGroup.Width = 100
        gv1.MasterTemplate.Columns.Add(repoTaxGroup)

        Dim repoItemCode1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoItemCode1.FormatString = ""
        repoItemCode1.HeaderText = "Item Code 1"
        repoItemCode1.Name = colItemCode1
        repoItemCode1.Width = 100
        gv1.MasterTemplate.Columns.Add(repoItemCode1)

        Dim repoItemDesc1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoItemDesc1.FormatString = ""
        repoItemDesc1.HeaderText = "Item Description 1"
        repoItemDesc1.Name = colItemDesc1
        repoItemDesc1.Width = 100
        gv1.MasterTemplate.Columns.Add(repoItemDesc1)

        Dim repoItemRate1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoItemRate1.FormatString = ""
        repoItemRate1.HeaderText = "Item Rate 1"
        repoItemRate1.Name = colItemRate1
        repoItemRate1.Width = 100
        gv1.MasterTemplate.Columns.Add(repoItemRate1)

        Dim repoItemQty1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoItemQty1.FormatString = ""
        repoItemQty1.HeaderText = "Item Qty 1"
        repoItemQty1.Name = colItemQty1
        repoItemQty1.Width = 100
        gv1.MasterTemplate.Columns.Add(repoItemQty1)

        'gv1.AllowAddNewRow = False
        'gv1.ShowGroupPanel = False
        'gv1.AllowColumnReorder = True
        'gv1.AllowRowReorder = False
        'gv1.EnableSorting = False
        'gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        'gv1.MasterTemplate.ShowRowHeaderColumn = False
        'gv1.TableElement.TableHeaderHeight = 40
        'gv1.EnableSorting = False
        'ReStoreGridLayout()
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        'Dim obj As clsJobMaster = clsJobMaster.GetData(strCode, NavTyep)
        'If obj IsNot Nothing Then
        '    isNewEntry = False
        '    txtCode.Value = obj.JOB_CODE
        '    txtDesc.Text = obj.DESCRIPTION
        '    Me.cboJobType.Text = obj.JOB_TYPE
        '    Me.cboAccountingMethod.Text = obj.ACCOUNTING_METHOD
        '    Me.cboBillingType.Text = obj.BILLING_TYPE
        '    Me.chkAutoCreateTask.Checked = obj.AUTO_CREATE_TASK
        '    Me.cboJobType.Text = obj.JOB_TYPE

        '    txtCode.MyReadOnly = True

        '    ''For Custom Fields
        '    If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
        '        UcCustomFields1.LoadData(obj.JOB_CODE)
        '    End If
        '    ''End of For Custom Fields
        'End If
    End Sub
    Sub SaveData()
        'Dim trans As SqlTransaction
        'Try
        '    If AllowToSave() Then
        '        trans = clsDBFuncationality.GetTransactin()
        '        Dim obj As New clsJobMaster()
        '        obj.JOB_CODE = txtCode.Value
        '        obj.DESCRIPTION = txtDesc.Text
        '        obj.JOB_TYPE = Me.cboJobType.Text
        '        obj.ACCOUNTING_METHOD = Me.cboAccountingMethod.Text
        '        obj.BILLING_TYPE = Me.cboBillingType.Text
        '        obj.AUTO_CREATE_TASK = Me.chkAutoCreateTask.Checked
        '        Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(JOB_CODE) from TSPL_PJC_JOB where JOB_CODE='" + obj.JOB_CODE + "'", trans)
        '        If (qry = 0) Then
        '            isNewEntry = True
        '        Else
        '            isNewEntry = False
        '        End If

        '        ''For Custom Fields
        '        obj.Form_ID = MyBase.Form_ID
        '        obj.arrCustomFields = New List(Of clsCustomFieldValues)
        '        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
        '            UcCustomFields1.GetData(obj.arrCustomFields)
        '        End If
        '        ''End of For Custom Fields

        '        If (clsJobMaster.SaveData(obj, isNewEntry, trans)) Then
        '            If obj.AUTO_CREATE_TASK = True Then
        '                Dim objTask As New clsTaskMaster()
        '                objTask.TASK_CODE = txtCode.Value
        '                objTask.DESCRIPTION = txtDesc.Text
        '                objTask.JOB_CODE = clsCommon.myCstr(Me.txtCode.Value)

        '                Dim qryTask As Integer = clsDBFuncationality.getSingleValue("select count(TASK_CODE) from TSPL_PJC_TASK where TASK_CODE='" + obj.JOB_CODE + "'", trans)
        '                If (qry = 0) Then
        '                    isNewEntry = True
        '                Else
        '                    isNewEntry = False
        '                End If
        '                clsTaskMaster.SaveData(objTask, isNewEntry, trans)
        '            End If
        '            trans.Commit()
        '            clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
        '            LoadData(obj.JOB_CODE, NavigatorType.Current)

        '        End If
        '    End If
        'Catch ex As Exception
        '    trans.Rollback()
        '    clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        'End Try
    End Sub

    Private Function AllowToSave() As Boolean
        'If clsCommon.myLen(clsCommon.myCstr(txtCode.Value)) <= 0 Then
        '    txtCode.Focus()
        '    clsCommon.MyMessageBoxShow("Please Fill  Code")
        '    Return False
        'End If

        'If clsCommon.myLen(clsCommon.myCstr(txtDesc.Text)) <= 0 Then
        '    txtDesc.Focus()
        '    clsCommon.MyMessageBoxShow("Please Fill  Description")
        '    Return False
        'End If
        'If clsCommon.myLen(clsCommon.myCstr(cboJobType.Text)) <= 0 Then
        '    cboJobType.Focus()
        '    clsCommon.MyMessageBoxShow("Please Select  Job Type")
        '    Return False
        'End If
        'If clsCommon.myLen(clsCommon.myCstr(cboAccountingMethod.Text)) <= 0 Then
        '    cboAccountingMethod.Focus()
        '    clsCommon.MyMessageBoxShow("Please Select  Accounting Method")
        '    Return False
        'End If
        'If clsCommon.myLen(clsCommon.myCstr(cboBillingType.Text)) <= 0 Then
        '    cboBillingType.Focus()
        '    clsCommon.MyMessageBoxShow("Please Select  Billing Type")
        '    Return False
        'End If
        'UcCustomFields1.AllowToSave()
        'Return True
    End Function
    Private Sub DeleteData()
        'Dim trans As SqlTransaction
        'Try
        '    trans = clsDBFuncationality.GetTransactin
        '    If clsCommon.myLen(txtCode.Value) <= 0 Then
        '        Throw New Exception("  Code not found to delete")

        '    End If
        '    If clsCommon.MyMessageBoxShow("Do you want to delete  Code '" + txtCode.Value + "'", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then

        '        Dim qry As String = "delete from TSPL_PJC_JOB where JOB_CODE='" + txtCode.Value + "'"
        '        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        '        '' custom fields
        '        clsCustomFieldValues.DeleteData(Me.Form_ID, txtCode.Value, trans)
        '        trans.Commit()
        '        clsCommon.MyMessageBoxShow("Successfully Deleted", Me.Text)
        '        AddNew()
        '    End If
        'Catch ex As Exception
        '    If (clsCommon.CompairString(clsCommon.myCstr(ex.Message), "Cost Code not found to delete") <> CompairStringResult.Equal) Then
        '        clsCommon.MyMessageBoxShow("Current Cost Code is in use")
        '    Else
        '        clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
        '    End If
        '    trans.Rollback()
        'End Try
    End Sub
    Sub AddNew()
        'txtCode.Value = ""
        ''txtDesc.Text = ""
        'txtCode.MyReadOnly = False
        'txtDesc.Text = ""
        'Me.cboJobType.DataSource = clsJobMaster.GetJobTypeTable
        'Me.cboJobType.DisplayMember = "Name"
        'Me.cboJobType.ValueMember = "Code"
        'cboJobType.SelectedIndex = -1

        'Me.cboAccountingMethod.DataSource = clsJobMaster.GetAccountingMethodTable
        'Me.cboAccountingMethod.DisplayMember = "Name"
        'Me.cboAccountingMethod.ValueMember = "Code"
        'cboAccountingMethod.SelectedIndex = -1

        'Me.cboBillingType.DataSource = clsJobMaster.GetBillingTypeTable
        'Me.cboBillingType.DisplayMember = "Name"
        'Me.cboBillingType.ValueMember = "Code"
        'cboBillingType.SelectedIndex = -1
        'Me.chkAutoCreateTask.Checked = False
    End Sub

#End Region
#Region "Events"

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click

        SaveData()
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        AddNew()
    End Sub


    Private Sub frmSalesImport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        'ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
        ' globalFunc.mandatoryText(fnddesig.txtValue, txtdes)

        AddNew()
    End Sub

    Private Sub frmSalesImport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            SaveData()
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            '    PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            AddNew()
        End If
    End Sub
#End Region


    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        Dim str As String
        str = " SELECT JOB_CODE AS [Code],DESCRIPTION AS [Description],JOB_TYPE AS [Job Type],ACCOUNTING_METHOD AS [Accounting Method]," & _
              " BILLING_TYPE AS [Billing Type],(case when AUTO_CREATE_TASK=1 then 'Yes' else 'No' end) AS [Auto Create Task] FROM TSPL_PJC_JOB "
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Code", "Description", "Job Type", "Accounting Method", "Billing Type", "Auto Create Task") Then
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try
                'connectSql.OpenConnection()
                'trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clsJobMaster()

                    Dim strCode As String = clsCommon.myCstr(grow.Cells("Code").Value)
                    If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                        Throw New Exception("Code can not be blank or incorrect.")
                    End If
                    obj.JOB_CODE = strCode

                    Dim strName As String = clsCommon.myCstr(grow.Cells("Description").Value)
                    If strName.Length > 200 Or (String.IsNullOrEmpty(strName)) Then
                        Throw New Exception("Description can not be blank or incorrect.")
                    End If
                    obj.DESCRIPTION = strName

                    strCode = clsCommon.myCstr(grow.Cells("Job Type").Value)
                    obj.JOB_TYPE = strCode
                    If strCode.Length > 30 Or (String.IsNullOrEmpty(strName)) Then
                        Throw New Exception("Job Type can not be blank or incorrect.")
                    End If

                    strCode = clsCommon.myCstr(grow.Cells("Accounting Method").Value)
                    obj.ACCOUNTING_METHOD = strCode
                    If strCode.Length > 30 Or (String.IsNullOrEmpty(strName)) Then
                        Throw New Exception("Accounting Method can not be blank or incorrect.")
                    End If

                    strCode = clsCommon.myCstr(grow.Cells("Billing Type").Value)
                    obj.BILLING_TYPE = strCode
                    If strCode.Length > 30 Or (String.IsNullOrEmpty(strName)) Then
                        Throw New Exception("Billing Type can not be blank or incorrect.")
                    End If

                    strCode = clsCommon.myCstr(grow.Cells("Auto Create Task").Value)
                    If clsCommon.CompairString(strCode, "Yes") = CompairStringResult.Equal Or clsCommon.CompairString(strCode, "1") = CompairStringResult.Equal Then
                        obj.AUTO_CREATE_TASK = True
                    Else
                        obj.AUTO_CREATE_TASK = False
                    End If



                    '' check for new entry
                    Dim isNewEntry As Boolean = False
                    Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(JOB_CODE) from TSPL_PJC_JOB where JOB_CODE='" + obj.JOB_CODE + "'", trans)
                    If (qry = 0) Then
                        isNewEntry = True
                    Else
                        isNewEntry = False
                    End If
                    clsJobMaster.SaveData(obj, isNewEntry, trans)

                    '' automatic create task
                    If obj.AUTO_CREATE_TASK = True Then
                        Dim objTask As New clsTaskMaster()
                        objTask.TASK_CODE = obj.JOB_CODE
                        objTask.DESCRIPTION = obj.DESCRIPTION
                        objTask.JOB_CODE = obj.JOB_CODE


                        Dim qryTask As Integer = clsDBFuncationality.getSingleValue("select count(TASK_CODE) from TSPL_PJC_TASK where TASK_CODE='" + obj.JOB_CODE + "'", trans)
                        If (qryTask = 0) Then
                            isNewEntry = True
                        Else
                            isNewEntry = False
                        End If
                        clsTaskMaster.SaveData(objTask, isNewEntry, trans)
                    End If
                Next
                trans.Commit()

                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
                trans.Rollback()
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub
End Class
