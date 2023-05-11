'Created By Richa Agarwal 04/08/2014 Against Ticket No BM00000003248
'updation by richa agarwal against ticket no BM00000004069
Imports common
Imports System.Data.SqlClient
Imports common.Controls

Public Class FrmBulkSaleSettings
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Private Sub rdbtnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnclose.Click
        Me.Close()
        GC.Collect()
    End Sub

    Private Sub FrmBulkSaleSettings_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso rdbtnsave.Enabled Then
            SaveData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub

    Private Sub FrmBulkSaleSettings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isNewEntry = True
        ButtonToolTip.SetToolTip(rdbtnsave, "Press Alt+S for Save")
        ButtonToolTip.SetToolTip(rdbtnclose, "Press Alt+C Close the Window")
        SetControlsTag()
        LoadData()
    End Sub
    Sub SetControlsTag()
        
        chkDispatchOutstandingBS.Tag1 = clsFixedParameterCode.AllowDispatchOutstandingBS
        chkDispatchOutstandingBS.Tag = clsFixedParameterType.AllowDispatchOutstandingBS

        chkDispatchOutstandingFS.Tag1 = clsFixedParameterCode.AllowDispatchOutstandingFS
        chkDispatchOutstandingFS.Tag = clsFixedParameterType.AllowDispatchOutstandingFS

        'Richa Agarwal 19/08/2014 Against Ticket No BM00000003110
        chkCreateDeliveryorderincaseamountincrease.Tag1 = clsFixedParameterCode.AllowDeliveryOrderIncaseAmountIncreases
        chkCreateDeliveryorderincaseamountincrease.Tag = clsFixedParameterType.AllowDeliveryOrderIncaseAmountIncreases
        '-----------------------------------
        '' Anubhooti 12-Sep-2014 BM00000003890
        ChkAllowItemMRP.Tag1 = clsFixedParameterCode.AllowToEnterMRPManually
        ChkAllowItemMRP.Tag = clsFixedParameterType.AllowToEnterMRPManually
    End Sub
#Region "Functions"

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmBulkSaleSettings)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        rdbtnsave.Visible = MyBase.isModifyFlag

    End Sub

    Sub LoadData()
        Try

            For Each ctrl As Control In RadGroupBox1.Controls
                If ctrl.GetType Is GetType(MyCheckBox) Then
                    Dim chkBox As MyCheckBox = TryCast(ctrl, MyCheckBox)
                    If clsCommon.myLen(chkBox.Tag) >= 0 AndAlso clsCommon.myLen(chkBox.Tag1) >= 0 Then
                        chkBox.Checked = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(chkBox.Tag, chkBox.Tag1, Nothing)) = 1, True, False)
                    End If
                End If
            Next

            TxtAmountLimitForInvoiceBulkSale.Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select description from tspl_fixed_parameter where type='" + clsFixedParameterType.AmountLimitForInvoiceBulkSale + "' and code='" + clsFixedParameterCode.AmountLimitForInvoiceBulkSale + "'"))

            TxtCorrectionFactor.Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select description from tspl_fixed_parameter where type='" + clsFixedParameterType.DefaultCorrectionFactorForBulkSale + "' and code='" + clsFixedParameterCode.DefaultCorrectionFactorForBulkSale + "'"))
            fndItemCode.Value = clsFixedParameter.GetData(clsFixedParameterType.BulkSaleDefaultMilkItem, clsFixedParameterCode.BulkSaleDefaultMilkItem, Nothing)
            lblItemDesc.Text = clsDBFuncationality.getSingleValue("select item_desc from tspl_item_Master where Item_code='" & fndItemCode.Value & "'")

            FndItemCodeforBulk.Value = clsFixedParameter.GetData(clsFixedParameterType.BSDefaultMilkItem, clsFixedParameterCode.BSDefaultMilkItem, Nothing)
            lblItemDescBulk.Text = clsDBFuncationality.getSingleValue("select item_desc from tspl_item_Master where Item_code='" & FndItemCodeforBulk.Value & "'")

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Sub SaveData()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try

            For Each ctrl As Control In RadGroupBox1.Controls
                If ctrl.GetType Is GetType(MyCheckBox) Then
                    Dim chkBox As MyCheckBox = TryCast(ctrl, MyCheckBox)
                    If clsCommon.myLen(chkBox.Tag) >= 0 AndAlso clsCommon.myLen(chkBox.Tag1) >= 0 Then
                        clsFixedParameter.UpdateData(chkBox.Tag, chkBox.Tag1, IIf(chkBox.Checked, "1", "0"), trans)
                    End If
                End If
            Next

           
            clsDBFuncationality.ExecuteNonQuery("Update TSPL_FIXED_PARAMETER Set description='" + clsCommon.myCstr(TxtAmountLimitForInvoiceBulkSale.Value) + "' where TYPE='" + clsFixedParameterType.AmountLimitForInvoiceBulkSale + " ' and Code='" + clsFixedParameterCode.AmountLimitForInvoiceBulkSale + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("Update TSPL_FIXED_PARAMETER Set description='" + clsCommon.myCstr(TxtCorrectionFactor.Value) + "' where TYPE='" + clsFixedParameterType.DefaultCorrectionFactorForBulkSale + " ' and Code='" + clsFixedParameterCode.DefaultCorrectionFactorForBulkSale + "'", trans)
            'If fndItemCode.Value <> "" Then
            clsDBFuncationality.ExecuteNonQuery("update TSPL_FIXED_PARAMETER set Description='" & FndItemCode.Value & "' where Type='" & clsFixedParameterType.BulkSaleDefaultMilkItem & " ' and Code='" & clsFixedParameterCode.BulkSaleDefaultMilkItem & "'", trans)
            clsDBFuncationality.ExecuteNonQuery("update TSPL_FIXED_PARAMETER set Description='" & FndItemCodeforBulk.Value & "' where Type='" & clsFixedParameterType.BSDefaultMilkItem & " ' and Code='" & clsFixedParameterCode.BSDefaultMilkItem & "'", trans)
            'nd If
            trans.Commit()
            clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)

        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

#End Region

    Private Sub rdbtnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbtnsave.Click
        SaveData()
    End Sub

   
   
    Private Sub FndItemCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles FndItemCode._MYValidating
        Try
            Dim Qry As String = ""
            Qry = "Select Item_Code as Code,Item_Desc as Description from TSPL_ITEM_MASTER "
            FndItemCode.Value = clsCommon.ShowSelectForm("Selector", Qry, "Code", " Product_Type ='MI' and Active=1", FndItemCode.Value, "", isButtonClicked)
            lblItemDesc.Text = clsDBFuncationality.getSingleValue("Select Item_Desc from TSPL_ITEM_MASTER where Item_Code ='" + FndItemCode.Value + "' ")

        Catch ex As Exception
        End Try
    End Sub

    Private Sub FndItemCodeforBulk__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles FndItemCodeforBulk._MYValidating
        Try
            Dim Qry As String = ""
            Qry = "Select Item_Code as Code,Item_Desc as Description from TSPL_ITEM_MASTER "
            FndItemCodeforBulk.Value = clsCommon.ShowSelectForm("Selector", Qry, "Code", " Product_Type ='MI' and Active=1", FndItemCodeforBulk.Value, "", isButtonClicked)
            lblItemDescBulk.Text = clsDBFuncationality.getSingleValue("Select Item_Desc from TSPL_ITEM_MASTER where Item_Code ='" + FndItemCodeforBulk.Value + "' ")

        Catch ex As Exception
        End Try
    End Sub
End Class
