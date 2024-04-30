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
''''''''''''''''''''''''''''''''''''''''''Ticket No:BM00000000475''''''''''''''''''''''''''''''''''''''''''''''''Created by Shipra on 17/09/13''''''


Public Class FrmToolMaster
    Inherits FrmMainTranScreen
    Dim isNewEntry As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
    
#Region "Finders"


    Private Sub fndToolType__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndToolType._MYValidating
        Dim qry As String = "Select TOOL_TYPE_CODE as Code,DESCRIPTION as Description from TSPL_MF_TOOL_TYPE  "
        fndToolType.Value = clsCommon.ShowSelectForm("REC_CONf1", qry, "Code", "", fndToolType.Value, "", isButtonClicked)
        txtToolType.Text = clsDBFuncationality.getSingleValue("select description from TSPL_MF_TOOL_TYPE where TOOL_TYPE_CODE='" + fndToolType.Value + "' ")

    End Sub

    Private Sub fndSupplier__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndSupplier._MYValidating

        Dim qry As String = "Select Vendor_Code as Code,Vendor_Name as Description from TSPL_VENDOR_MASTER   "
        fndSupplier.Value = clsCommon.ShowSelectForm("REC_CONd2", qry, "Code", "", fndSupplier.Value, "", isButtonClicked)
        txtSupplier.Text = clsDBFuncationality.getSingleValue("select Vendor_Name from TSPL_VENDOR_MASTER where Vendor_Code='" + fndSupplier.Value + "' ")
    End Sub


    Private Sub fnduom__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fnduom._MYValidating
        Dim qry As String = " Select Unit_Code  as Code,Unit_Desc  as Description from TSPL_UNIT_MASTER  "
        fnduom.Value = clsCommon.ShowSelectForm("REC_CONfnd2", qry, "Code", "", fnduom.Value, "", isButtonClicked)
        txtuom.Text = clsDBFuncationality.getSingleValue("select Unit_Desc from TSPL_UNIT_MASTER where Unit_Code='" + fnduom.Value + "' ")
    End Sub

    Private Sub fndToolCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndToolCode._MYValidating
        Try
            Dim str As String = "select count(*) from TSPL_MF_TOOL_MASTER where TOOL_CODE ='" + fndToolCode.Value + "' "
            Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
            If no = 0 AndAlso isButtonClicked = False Then
                fndToolCode.MyReadOnly = False
            Else
                fndToolCode.MyReadOnly = True
            End If
            If fndToolCode.MyReadOnly OrElse isButtonClicked Then

                'Dim qry As String = " Select TOOL_CODE  as Code,DESCRIPTION from TSPL_MF_TOOL_MASTER    "
                'fndToolCode.Value = clsCommon.ShowSelectForm("TSPL_MF_TOOL_MASTER", qry, "Code", "", fndToolCode.Value, "", isButtonClicked)
                fndToolCode.Value = clsToolMaster.getFinder("", fndToolCode.Value, isButtonClicked)
                If fndToolCode.Value <> "" Then
                    LoadData(fndToolCode.Value, NavigatorType.Current)
                Else
                    Reset()
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message())
        End Try
    End Sub

    Private Sub fndToolCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndToolCode._MYNavigator
        Try
            LoadData(fndToolCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

#End Region
#Region "Functions"

    Private Function GetCboStatusDataTable() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("CODE", GetType(String))
        dt.Columns.Add("Value", GetType(String))

        Dim dr As DataRow
        dr = dt.NewRow()
        dr("CODE") = "Active"
        dr("Value") = "Active"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("CODE") = "Inactive"
        dr("Value") = "Inactive"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("CODE") = "Discontinued"
        dr("Value") = "Discontinued"
        dt.Rows.Add(dr)

        dt.AcceptChanges()
        Return dt
    End Function

    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (ClsMFToolMaster.DeleteData(fndToolCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    Reset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub


    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.TOOL)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        rdbtnsave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As ClsMFToolMaster = ClsMFToolMaster.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            isNewEntry = False
            fndToolCode.Value = obj.TOOL_CODE
            txtdescription.Text = obj.DESCRIPTION
            fndSupplier.Value = obj.SUPPLIER
            fndToolType.Value = obj.TOOL_TYPE_CODE
            fnduom.Value = obj.UNIT_CODE
            txtConsumeQTY.Text = obj.CONSUMED
            txtCost.Text = obj.COST_PER_UNIT
            txtCustodian.Text = obj.CUSTODIAN
            txtPo.Text = obj.PO_NUMBER
            txtReceivedBy.Text = obj.RECEIVED_BY
            txtMaintainedBy.Text = obj.MAINTAINED_BY
            txtOnHandCost.Text = obj.ON_HAND_COST
            txtOnhandqty.Text = obj.ON_HAND_QUANTITY
            txtOriginalQty.Text = obj.ORIGINAL_QUANTITY
            txtReceivedBy.Text = obj.RECEIVED_BY
            txtSerialno.Text = obj.SERIAL_NUMBER
            txtLastMaitainDate.Value = obj.INACTIVE_DATE
            If clsCommon.myLen(obj.RECEIPT_DATE) > 0 Then
                txtReceiptDate.Checked = True
                txtReceiptDate.Value = obj.RECEIPT_DATE
            End If
            If clsCommon.myLen(obj.REPLACEMENT_DATE) > 0 Then
                txtReplacementDate.Checked = True
                txtReplacementDate.Value = obj.REPLACEMENT_DATE
            End If
            txtComments.Text = obj.COMMENTS
            txtReceipt.Text = obj.RECEIPT_NUMBER
            cboStatus.SelectedValue = obj.STATUS
            txtToolType.Text = clsDBFuncationality.getSingleValue("select description from TSPL_MF_TOOL_TYPE where TOOL_TYPE_CODE='" + fndToolType.Value + "' ")
            txtSupplier.Text = clsDBFuncationality.getSingleValue("select Vendor_Name from TSPL_VENDOR_MASTER where Vendor_Code='" + fndSupplier.Value + "' ")
            txtuom.Text = clsDBFuncationality.getSingleValue("select Unit_Desc from TSPL_UNIT_MASTER where Unit_Code='" + fnduom.Value + "' ")
            fndToolCode.MyReadOnly = True
        End If
    End Sub
    Sub Reset()
        txtReceiptDate.Checked = False
        txtReplacementDate.Checked = False
        fndToolCode.Value = ""
        fndToolCode.MyReadOnly = False
        txtdescription.Text = ""
        fndSupplier.Value = ""
        fndSupplier.MyReadOnly = False
        fndToolType.Value = ""
        fndToolType.MyReadOnly = False
        fnduom.Value = ""
        fnduom.MyReadOnly = True
        txtConsumeQTY.Text = ""
        txtCost.Text = ""
        txtComments.Text = ""
        txtReceivedBy.Text = ""
        txtMaintainedBy.Text = ""
        txtOnHandCost.Text = 0.0
        txtOnhandqty.Text = 0.0
        txtOriginalQty.Text = 0.0
        txtReceivedBy.Text = ""
        txtSerialno.Text = 0
        txtLastMaitainDate.Value = clsCommon.GETSERVERDATE()
        txtReceiptDate.Value = clsCommon.GETSERVERDATE()
        txtReplacementDate.Value = clsCommon.GETSERVERDATE()
        txtToolType.Text = ""
        txtSupplier.Text = ""
        txtuom.Text = ""
        cboStatus.DataSource = GetCboStatusDataTable()
        txtPo.Text = ""
        txtCustodian.Text = ""
        cboStatus.ValueMember = "CODE"
        cboStatus.DisplayMember = "Value"
        txtComments.Text = ""
    End Sub
    Sub SaveData()
        If AllowToSave() Then
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try
                Dim obj As New ClsMFToolMaster()
                obj.TOOL_CODE = fndToolCode.Value
                obj.DESCRIPTION = txtdescription.Text
                obj.SUPPLIER = fndSupplier.Value
                obj.TOOL_TYPE_CODE = fndToolType.Value
                obj.UNIT_CODE = fnduom.Value
                obj.CONSUMED = txtConsumeQTY.Text
                obj.COST_PER_UNIT = txtCost.Text
                obj.CUSTODIAN = txtCustodian.Text
                obj.RECEIVED_BY = txtReceivedBy.Text

                obj.MAINTAINED_BY = objCommonVar.CurrentUser

                obj.ON_HAND_COST = txtOnHandCost.Text
                obj.ON_HAND_QUANTITY = txtOnhandqty.Text
                obj.ORIGINAL_QUANTITY = txtOriginalQty.Text
                obj.RECEIVED_BY = txtReceivedBy.Text
                obj.SERIAL_NUMBER = txtSerialno.Text
                obj.INACTIVE_DATE = clsCommon.GETSERVERDATE(trans)
                If txtReceiptDate.Checked Then
                    obj.RECEIPT_DATE = txtReceiptDate.Value
                End If
                If txtReplacementDate.Checked Then
                    obj.REPLACEMENT_DATE = txtReplacementDate.Value
                End If
                obj.RECEIPT_NUMBER = txtReceipt.Text
                obj.COMMENTS = txtComments.Text
                obj.PO_NUMBER = txtPo.Text
                obj.STATUS = cboStatus.SelectedValue
                Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(TOOL_CODE) from TSPL_MF_TOOL_MASTER where TOOL_CODE='" + obj.TOOL_CODE + "'", trans)
                If (qry = 0) Then
                    isNewEntry = True
                Else
                    isNewEntry = False
                End If
                If (ClsMFToolMaster.SaveData(obj, isNewEntry, trans)) Then
                    trans.Commit()
                    clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                    LoadData(obj.TOOL_CODE, NavigatorType.Current)

                End If
            Catch ex As Exception
                trans.Rollback()
                clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
            End Try
        End If
    End Sub
    Private Function AllowToSave() As Boolean
        If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) Then
            If clsCommon.myLen(clsCommon.myCstr(fndToolCode.Value)) <= 0 Then
                fndToolCode.Focus()
                clsCommon.MyMessageBoxShow("Please Fill AccountSet Code")
                Return False
            End If
        End If
        If clsCommon.myLen(clsCommon.myCstr(txtdescription.Text)) <= 0 Then
            txtdescription.Focus()
            clsCommon.MyMessageBoxShow("Please Fill Description")
            Return False
        End If
        If clsCommon.myLen(clsCommon.myCstr(fndToolType.Value)) <= 0 Then
            fndToolType.Focus()
            clsCommon.MyMessageBoxShow("Please Fill ToolType")
            Return False
        End If
        If clsCommon.myLen(clsCommon.myCstr(fnduom.Value)) <= 0 Then
            fnduom.Focus()
            clsCommon.MyMessageBoxShow("Please Fill UOM")
            Return False
        End If
        If clsCommon.myLen(clsCommon.myCstr(txtCost.Text)) <= 0 Then
            txtCost.Focus()
            clsCommon.MyMessageBoxShow("Please Fill Cost per unit")
            Return False
        End If
        If clsCommon.myLen(clsCommon.myCstr(txtOriginalQty.Text)) <= 0 Then
            txtdescription.Focus()
            clsCommon.MyMessageBoxShow("Please Fill Original Qty")
            Return False
        End If
        If clsCommon.myLen(clsCommon.myCstr(txtConsumeQTY.Text)) <= 0 Then
            txtConsumeQTY.Focus()
            clsCommon.MyMessageBoxShow("Please Fill Consume Qty")
            Return False
        End If
        If clsCommon.myLen(clsCommon.myCstr(txtOnHandCost.Text)) <= 0 Then
            txtConsumeQTY.Focus()
            clsCommon.MyMessageBoxShow("Please Fill On Hand Cost ")
            Return False
        End If
        If clsCommon.myLen(clsCommon.myCstr(txtOnhandqty.Text)) <= 0 Then
            txtConsumeQTY.Focus()
            clsCommon.MyMessageBoxShow("Please Fill On Hand Qty")
            Return False
        End If
        Return True
    End Function
#End Region



#Region "Events"
    Private Sub FrmToolMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Reset()
        ButtonToolTip.SetToolTip(rdbtnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete ")
        ButtonToolTip.SetToolTip(rdbtnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(rdbtnnew, "Press Alt+N Adding New ")
    End Sub
    Private Sub rdbtnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnsave.Click
        SaveData()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        funDelete()
    End Sub

    Private Sub rdbtnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnclose.Click
        Me.Close()
    End Sub

    Private Sub rdbtnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnnew.Click
        Reset()
    End Sub

    Private Sub FrmToolMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso rdbtnsave.Enabled Then
            SaveData()
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            '    PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            funDelete()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub
#End Region


End Class
