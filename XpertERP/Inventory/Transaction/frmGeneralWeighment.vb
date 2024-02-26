'--------Created By Sanjay Ticket No - BHA/08/08/18-000397 Date - 08/Aug/2018, Client - Bharat Dairy 
Imports common
Imports System.Data.SqlClient

Public Class frmGeneralWeighment
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim Qry As String
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim isFlag As Boolean = False
    Dim arrLoc As String = Nothing
    Public strDocCode As String = ""
    Dim ApplyMultiChamber As Boolean = False
    Public isCellValueChangedOpen = False
    Public isInsideloaddata = False
    Public TareWeight As Double = 0
    Dim ApplyTSPriceAtBulkSale As Boolean = False
    Dim isFillGeneralWeighmentDetailsByJobworkTypeGateInNo As Boolean = False
 
#End Region

#Region "User Defined Functions and Subroutines"

    Public Sub New()
        InitializeComponent()
    End Sub
#End Region

    Private Sub frmGeneralWeighment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        clsDBFuncationality.ExecuteNonQuery("delete from TSPL_FIXED_PARAMETER_PROGRAM_MAPPING where Program_Code='G-Weight' and FP_Type='Auto Tanker Weightment'")
        Reset()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Transaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Transaction")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N New Transaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Transaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        If clsCommon.myLen(strDocCode) > 0 Then
            LoadData(strDocCode, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MachineIntegrationInGeneralWeighment, clsFixedParameterCode.MachineIntegrationInGeneralWeighment, Nothing)) > 0 Then
            UcWeighing1.Enabled = True
            UcWeighing1.form_ID = MyBase.Form_ID
            UcWeighing1.LoadPortAndMachine()
            UcWeighing1.LoadSettingAndStart()
            EnableDisableWeightTxtBox(True)
        Else
            UcWeighing1.Enabled = False
            EnableDisableWeightTxtBox(False)
        End If
        isFillGeneralWeighmentDetailsByJobworkTypeGateInNo = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.FillGeneralWeighmentDetailsByJobworkTypeGateInNo, clsFixedParameterCode.FillGeneralWeighmentDetailsByJobworkTypeGateInNo, Nothing)) = "1", True, False))
        'clsCommon.myCBool(clsFixedParameter.GetData(clsFixedParameterType.FillGeneralWeighmentDetailsByJobworkTypeGateInNo, clsFixedParameterCode.FillGeneralWeighmentDetailsByJobworkTypeGateInNo, Nothing))
        'If isFillGeneralWeighmentDetailsByJobworkTypeGateInNo = False Then
        txtGateInNoJW.Enabled = False
        ' End If
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmWeighmentEntry)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        If MyBase.isReverse Then
            btnReverse.Enabled = True
        Else
            btnReverse.Enabled = False
        End If
        btnPost.Visible = MyBase.isPostFlag
    End Sub

    Sub Reset()
        rbtnNone.IsChecked = True
        txtItemCode.Value = ""
        lblItemName.Text = ""
        fndWeighmentcode.Value = ""
        txtLocation.Value = ""
        LblLocationName.Text = ""
        txtVehicle_No.Text = ""
        txtTransporter.Text = ""
        txtRemarks.Text = ""
        txtComments.Text = ""
        txtNetWeight.Value = 0
        txtGrossWeight.Value = 0
        TxtTareWeight.Value = 0
        UsLock1.Status = ERPTransactionStatus.Pending
        txtWeighmentdate.Value = clsCommon.GETSERVERDATE()
        dtpTareWeighment.Value = clsCommon.GETSERVERDATE()
        dtpGrossWeighment.Value = clsCommon.GETSERVERDATE()
        Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, Nothing)
        If DateTime = "1" Then
            txtWeighmentdate.CustomFormat = "dd/MM/yyyy hh:mm tt"
            dtpTareWeighment.CustomFormat = "dd/MM/yyyy hh:mm tt"
            dtpGrossWeighment.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Else
            txtWeighmentdate.CustomFormat = "dd/MM/yyyy"
            dtpTareWeighment.CustomFormat = "dd/MM/yyyy"
            dtpGrossWeighment.CustomFormat = "dd/MM/yyyy"
        End If
        fndWeighmentcode.MyReadOnly = False
        txtNetWeight.Enabled = False
        btnsave.Text = "Save"
        btndelete.Enabled = False
        btnPost.Enabled = False
        btnsave.Enabled = True
        isNewEntry = True
        chkEmptyVehicle.Checked = False
        setGrossTareText()
        controlEnabledisable(True)
        txtGateInNoJW.Value = ""
        btnReverse.Visible = False
    End Sub

    Private Sub frmGeneralWeighment_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F2 Then
            Try
                If clsCommon.myCdbl(txtGrossWeight.Value) = 0 Then
                    txtGrossWeight.Text = UcWeighing1.LiveReading
                Else
                    TxtTareWeight.Text = UcWeighing1.LiveReading
                End If
                CalculatingNetWeight()
            Catch ex As Exception
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclose.Enabled Then
            CloseForm()
        ElseIf e.Control AndAlso e.Alt AndAlso e.Shift AndAlso e.KeyCode = Keys.M Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = clsFixedParameterType.GrossWeightChangePWD
            frm.strCode = clsFixedParameterCode.GrossWeightChangePWD
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                EnableDisableWeightTxtBox(False)
            End If
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then

                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SIRC"
                frm.strCode = "SIReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnReverse.Visible = True
                End If
            Else
                MessageBox.Show("You are not authorized to perform this action.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
        End If
    End Sub

    Sub EnableDisableWeightTxtBox(ByVal isReadOnly As Boolean)
        txtGrossWeight.ReadOnly = isReadOnly
        TxtTareWeight.ReadOnly = isReadOnly
        txtNetWeight.ReadOnly = True
    End Sub

    Private Sub CloseForm()
        Try
            UcWeighing1.CloseCOMPORT()
        Catch ex As Exception
        End Try

        clsERPFuncationality.closeForm(Me)
    End Sub

    Sub SaveData(Optional ByVal ispost As Boolean = False)
        ' Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim obj As New ClsGeneralWeighment()
        Try
            If AllowToSave(ispost) Then
                obj.Tare_Weight_date = dtpTareWeighment.Value
                obj.Gross_Weight_date = dtpGrossWeighment.Value
                obj.IsJobWork = IIf(rbtnJobWork.IsChecked, 1, 0)
                obj.Is_Scrap = rbtnScrap.IsChecked
                obj.Item_Code = txtItemCode.Value
                obj.Weighment_No = fndWeighmentcode.Value
                obj.Weighment_Date = txtWeighmentdate.Value
                obj.Location_Code = txtLocation.Value
                obj.Vehicle_No_Manual = txtVehicle_No.Text
                obj.Transporter_Name_Manual = txtTransporter.Text
                obj.Remarks = txtRemarks.Text
                obj.Comments = txtComments.Text
                obj.Tare_Weight = TxtTareWeight.Value
                obj.Gross_Weight = txtGrossWeight.Value
                obj.Net_Weight = txtNetWeight.Value
                obj.Is_Empty_Vehicle = chkEmptyVehicle.Checked
                If isFillGeneralWeighmentDetailsByJobworkTypeGateInNo = True AndAlso rbtnJobWork.IsChecked = True Then
                    obj.Gate_Entry_No = txtGateInNoJW.Value
                End If
                If (ClsGeneralWeighment.SaveData(obj, isNewEntry)) Then
                    If Not ispost Then
                        If isNewEntry Then
                            clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
                            LoadData(obj.Weighment_No, NavigatorType.Current)
                        Else
                            clsCommon.MyMessageBoxShow(Me, "Data updated successfully", Me.Text)
                            LoadData(obj.Weighment_No, NavigatorType.Current)
                        End If
                    End If

                End If
            End If
        Catch ex As Exception
            'trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
        End Try
    End Sub

    Private Function AllowToSave(Optional ByVal ispost As Boolean = False) As Boolean

        If AllowFutureDateTransaction(txtWeighmentdate.Value, Nothing) = False Then
            txtWeighmentdate.Focus()
            txtWeighmentdate.Select()
            Return False
        End If
        If rbtnJobWork.IsChecked = True AndAlso isFillGeneralWeighmentDetailsByJobworkTypeGateInNo = True Then
            If clsCommon.myLen(txtGateInNoJW.Value) <= 0 Then
                Throw New Exception("Gate Entery No can not be left blank.")
            End If
        End If
        If clsCommon.myLen(txtLocation.Value) <= 0 Then
            txtLocation.Focus()
            Throw New Exception("Location can not be left blank.")
        End If
        CalculatingNetWeight()
        If rbtnJobWork.IsChecked Then
            Dim strCount As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_LOCATION_MASTER where Location_Code='" & txtLocation.Value & "' and Is_Jobwork=1"))
            If strCount = 0 Then
                Throw New Exception("Location should be Job Work type in case of Job work is ON.")
            End If
        End If
        Return True

    End Function

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        isInsideloaddata = True
        Reset()
        Dim obj As ClsGeneralWeighment = ClsGeneralWeighment.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            isNewEntry = False
            dtpTareWeighment.Value = IIf(obj.Tare_Weight_date Is Nothing, obj.Weighment_Date, obj.Tare_Weight_date)
            dtpGrossWeighment.Value = IIf(obj.Gross_Weight_date Is Nothing, obj.Weighment_Date, obj.Gross_Weight_date)
            rbtnJobWork.IsChecked = IIf(obj.IsJobWork = 1, True, False)
            rbtnScrap.IsChecked = obj.Is_Scrap
            txtItemCode.Value = obj.Item_Code
            lblItemName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_desc from tspl_item_master where Item_Code='" + txtItemCode.Value + "'"))
            fndWeighmentcode.Value = obj.Weighment_No
            txtWeighmentdate.Value = obj.Weighment_Date
            txtLocation.Value = obj.Location_Code
            LblLocationName.Text = clsDBFuncationality.getSingleValue("Select Location_Desc from TSPL_LOCATION_MASTER where Location_Code  ='" + txtLocation.Value + "' ")
            txtVehicle_No.Text = obj.Vehicle_No_Manual
            txtTransporter.Text = obj.Transporter_Name_Manual
            txtRemarks.Text = obj.Remarks
            txtComments.Text = obj.Comments
            TxtTareWeight.Value = obj.Tare_Weight
            txtGrossWeight.Value = obj.Gross_Weight
            txtNetWeight.Value = obj.Net_Weight
            chkEmptyVehicle.Checked = obj.Is_Empty_Vehicle
            setGrossTareText()

            fndWeighmentcode.MyReadOnly = True
            If rbtnJobWork.IsChecked = True AndAlso isFillGeneralWeighmentDetailsByJobworkTypeGateInNo = True Then
                controlEnabledisable(False)
                txtGateInNoJW.Value = obj.Gate_Entry_No
            End If
            btnsave.Text = "Update"
            If clsCommon.CompairString(obj.Posted, "1") = CompairStringResult.Equal Then
                btnPost.Enabled = False
                btnsave.Enabled = False
                btndelete.Enabled = False
                UsLock1.Status = ERPTransactionStatus.Approved
            Else
                btnPost.Enabled = True
                btnsave.Enabled = True
                btndelete.Enabled = True
                UsLock1.Status = ERPTransactionStatus.Pending
            End If

        End If
        obj = Nothing
        isInsideloaddata = False
    End Sub

    Private Sub DeleteData()
        Try

            If (deleteConfirm()) Then

                If (ClsGeneralWeighment.DeleteData(fndWeighmentcode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data deleted successfully ", Me.Text)
                    Reset()
                End If

            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        CloseForm()
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        Reset()
    End Sub

    Private Sub fndWeighmentcode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndWeighmentcode._MYNavigator
        Dim qry As String = String.Empty
        Try

            qry = "select count(*) from TSPL_GENERAL_WEIGHMENT_DETAIL where Weighment_No='" + fndWeighmentcode.Value + "' and Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry))
            If check > 0 Then
                fndWeighmentcode.MyReadOnly = True
            ElseIf check <= 0 Then
                fndWeighmentcode.MyReadOnly = False
            End If
            
                LoadData(fndWeighmentcode.Value, NavType)
                
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            qry = Nothing
        End Try
    End Sub

    Private Sub fndWeighmentcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndWeighmentcode._MYValidating
        Dim qry As String = "Select TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_No as Code,convert(varchar,TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_Date,103) as Date,TSPL_GENERAL_WEIGHMENT_DETAIL.Transporter_Name_Manual as [Transporter],TSPL_GENERAL_WEIGHMENT_DETAIL.Vehicle_No_Manual as [Vehicle no],TSPL_GENERAL_WEIGHMENT_DETAIL.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Description],case when TSPL_GENERAL_WEIGHMENT_DETAIL.Posted=0 then 'Pending' else 'Approved' end as Status from TSPL_GENERAL_WEIGHMENT_DETAIL Left Outer Join TSPL_LOCATION_MASTER on TSPL_GENERAL_WEIGHMENT_DETAIL.Location_Code =TSPL_LOCATION_MASTER.Location_Code "
        Dim strwhrcls As String = ""
        
        fndWeighmentcode.Value = clsCommon.ShowSelectForm("GeneralWeighment", qry, "Code", strwhrcls, fndWeighmentcode.Value, "", isButtonClicked)
        LoadData(fndWeighmentcode.Value, NavigatorType.Current)

        qry = Nothing
        strwhrcls = Nothing
    End Sub

    Private Sub txtGrossWeight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtGrossWeight.Validating
        CalculatingNetWeight()
    End Sub

    Private Sub TxtTareWeight_TextChanged(sender As Object, e As EventArgs) Handles TxtTareWeight.Validating
        CalculatingNetWeight()
    End Sub

    Sub CalculatingNetWeight() ''BHA/21/11/18-000694 by balwinder on 30/11/2018
        If clsCommon.myCdbl(txtGrossWeight.Value) > 0 Then
            txtNetWeight.Value = clsCommon.myCdbl(clsCommon.myCdbl(txtGrossWeight.Value) - clsCommon.myCdbl(TxtTareWeight.Value)) * IIf(chkEmptyVehicle.Checked, -1, 1)
        Else
            txtNetWeight.Value = 0
        End If
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Dim msg As String = String.Empty
        Dim qry As String = String.Empty
        Dim dt As DataTable = Nothing
        Try
            If clsCommon.myLen(fndWeighmentcode.Value) > 0 Then
                If (myMessages.postConfirm()) Then
                    SaveData(True)
                    If (ClsGeneralWeighment.PostData(MyBase.Form_ID, fndWeighmentcode.Value)) Then
                        msg = "Successfully Posted"
                        common.clsCommon.MyMessageBoxShow(Me, msg)
                        LoadData(fndWeighmentcode.Value, NavigatorType.Current)
                    End If
                End If
            Else
                Throw New Exception("Weighment No not found to Post")
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isFlag = False
            msg = Nothing
            qry = Nothing
            dt = Nothing
        End Try
    End Sub

    Private Function GetDataTable(dtg As RadGridView) As DataTable
        Dim dt As New DataTable()

        Return dt
    End Function

    Private Sub frmMilkReceiptMCC_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Try
            UcWeighing1.CloseCOMPORT()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
        Dim qry As String = "select Location_Code as Code,Location_Desc as Name,Loc_Short_Name as [Short Name] from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = " 1=1 "
        If rbtnJobWork.IsChecked Then
            WhrCls += " and TSPL_LOCATION_MASTER.Is_Jobwork=1 "
        End If
        txtLocation.Value = clsCommon.ShowSelectForm("MM-SHLocFndr", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
        LblLocationName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
        txtRemarks.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_VENDOR_MASTER.Vendor_Name from TSPL_LOCATION_MASTER left join TSPL_VENDOR_MASTER on TSPL_LOCATION_MASTER.Jobwork_Vendor=TSPL_VENDOR_MASTER.Vendor_Code where Location_Code='" & txtLocation.Value & "'"))
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            If clsCommon.myLen(fndWeighmentcode.Value) <= 0 Then
                Throw New Exception("Not found anything to print")
            Else
                Dim qry As String = ""
                Dim frmCRV As New frmCrystalReportViewer()
                'Ticket No- BHA/12/11/18-000669 
                qry = "Select Weighment_No,Weighment_Date,Vehicle_No_Manual,Transporter_Name_Manual,(case when isnull(TSPL_GENERAL_WEIGHMENT_DETAIL.Is_Empty_Vehicle,0)=1 then Gross_Weight else Tare_Weight end) as Tare_Weight,(case when isnull(TSPL_GENERAL_WEIGHMENT_DETAIL.Is_Empty_Vehicle,0)=1 then Tare_Weight else Gross_Weight end) as Gross_Weight,Net_Weight  " & _
                  " ,TSPL_GENERAL_WEIGHMENT_DETAIL.location_Code [Location] , TSPL_LOCATION_MASTER.Location_Desc [Loc Desc] , TSPL_GENERAL_WEIGHMENT_DETAIL.comp_code [Company Code] , TSPL_COMPANY_MASTER.Comp_Name [Comp Desc] , CONCAT(TSPL_COMPANY_MASTER.Add1 , ' ' , TSPL_COMPANY_MASTER.Add2 , ' ', TSPL_COMPANY_MASTER.Add3 , ' , ', TSPL_COMPANY_MASTER.State ) as [Company Address],TSPL_GENERAL_WEIGHMENT_DETAIL.Remarks,TSPL_GENERAL_WEIGHMENT_DETAIL.Comments " & _
                 ",coalesce(Tare_Weight_date,Weighment_Date) as Tare_Weight_date,coalesce(Gross_Weight_date,Weighment_Date) as Gross_Weight_date" & _
                 " from TSPL_GENERAL_WEIGHMENT_DETAIL LEFT JOIN  TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.comp_code = TSPL_GENERAL_WEIGHMENT_DETAIL.Comp_Code " & _
                 " LEFT JOIN TSPL_LOCATION_MASTER  ON TSPL_LOCATION_MASTER.Location_Code = TSPL_GENERAL_WEIGHMENT_DETAIL.Location_Code " & _
                  "  where TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_No='" + fndWeighmentcode.Value + "' "
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt.Rows.Count > 0 Then
                    frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, "crptGeneralWeighment", "General Weighment", clsCommon.myCDate(dt.Rows(0)("Weighment_date")))
                End If
                frmCRV = Nothing
            End If
        Catch ex As Exception
            RadMessageBox.Show(ex.Message, Me.Text)
        End Try
    End Sub

   

    Private Sub txtItemCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtItemCode._MYValidating
        Dim WhrCls As String = "  Product_Type='MI' "
        If rbtnScrap.IsChecked Then
            WhrCls = " Is_Scrap_Item=1 and item_code in (select item_code from TSPL_ITEM_UOM_DETAIL where UOM_Code in(select Unit_Code from TSPL_UNIT_MASTER where item_category='K') group by item_code)"
        End If
        txtItemCode.Value = clsItemMaster.getFinder(WhrCls, txtItemCode.Value, isButtonClicked)
        lblItemName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_desc from tspl_item_master where Item_Code='" + txtItemCode.Value + "'"))
    End Sub

    Private Sub chkEmptyVehicle_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkEmptyVehicle.ToggleStateChanged
        setGrossTareText()
    End Sub

    Sub setGrossTareText()
        If chkEmptyVehicle.Checked Then
            MyLabel3.Text = "Gross Weight"
            MyLabel7.Text = "Tare Weight"
        Else
            MyLabel3.Text = "Tare Weight"
            MyLabel7.Text = "Gross Weight"
        End If
    End Sub
    ' Ticket No : ERO/06/03/19-000506 By Prabhakar
    Private Sub txtGateInNoJW__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtGateInNoJW._MYValidating
        Dim qry As String = "select tspl_gate_entry_details.Gate_Entry_No as GateEntryNo,FORMAT(Date_And_Time,'dd/MM/yyyy hh:mm:s tt') as  [Gate Entry Date And Time] ,tspl_gate_entry_details.Tanker_No as [Tanker No] , tspl_gate_entry_details.location_Code as [Location Code],tspl_gate_entry_details.Location_Desc as [Location Desc], tspl_gate_entry_details.Item_Code as [Item Code], tspl_gate_entry_details.Item_Desc as [Item Desc]  from tspl_gate_entry_details "
        Dim WhrCls As String = " tspl_gate_entry_details.isPosted = 1 and Gate_Entry_Type = 'J' and tspl_gate_entry_details.Gate_Entry_No not in (select TSPL_GENERAL_WEIGHMENT_DETAIL.Gate_Entry_No from TSPL_GENERAL_WEIGHMENT_DETAIL where TSPL_GENERAL_WEIGHMENT_DETAIL.Gate_Entry_No is not null and TSPL_GENERAL_WEIGHMENT_DETAIL.Gate_Entry_No <> '' )  "
        'If chkJobWork.Checked Then
        '    WhrCls += " and TSPL_LOCATION_MASTER.Is_Jobwork=1 "
        'End If
        txtGateInNoJW.Value = clsCommon.ShowSelectForm("GateIn@Finder", qry, "GateEntryNo", WhrCls, txtGateInNoJW.Value, "GateEntryNo", isButtonClicked)
        If clsCommon.myLen(txtGateInNoJW.Value) > 0 Then
            qry = qry + " where tspl_gate_entry_details.Gate_Entry_No = '" + txtGateInNoJW.Value + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                txtVehicle_No.Text = clsCommon.myCstr(dt.Rows(0)("Tanker No"))
                txtLocation.Value = clsCommon.myCstr(dt.Rows(0)("Location Code"))
                LblLocationName.Text = clsCommon.myCstr(dt.Rows(0)("Location Desc"))
                txtItemCode.Value = clsCommon.myCstr(dt.Rows(0)("Item Code"))
                lblItemName.Text = clsCommon.myCstr(dt.Rows(0)("Item Desc"))
            End If
            controlEnabledisable(False)
        End If
    End Sub
    Sub controlEnabledisable(ByVal isEnable As Boolean)
        txtVehicle_No.Enabled = isEnable
        txtLocation.Enabled = isEnable
        LblLocationName.Enabled = isEnable
        txtItemCode.Enabled = isEnable
        lblItemName.Enabled = isEnable
    End Sub
    ' Ticket No : ERO/07/03/19-000509 By Prabhakar
    Private Sub btnReverse_Click(sender As Object, e As EventArgs) Handles btnReverse.Click
        Try
            If common.clsCommon.MyMessageBoxShow(Me, "Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If ClsGeneralWeighment.ReverseAndUnpost(fndWeighmentcode.Value) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    LoadData(fndWeighmentcode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rbtnNone_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnNone.ToggleStateChanged
        chkJobWork_ToggleStateChanged()
    End Sub

    Private Sub rbtnJobWork_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnJobWork.ToggleStateChanged
        chkJobWork_ToggleStateChanged()
    End Sub

    Private Sub rbtnScrap_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnScrap.ToggleStateChanged
        chkJobWork_ToggleStateChanged() ''VIJ/23/01/20-001067 by baliwnder on 29/01/2020
    End Sub

    Private Sub chkJobWork_ToggleStateChanged()
        If rbtnJobWork.IsChecked Then
            txtItemCode.Enabled = True
            txtLocation.Value = ""
            LblLocationName.Text = ""
        ElseIf rbtnScrap.IsChecked Then
            txtItemCode.Enabled = True
            txtLocation.Value = ""
            LblLocationName.Text = ""
        Else
            txtItemCode.Enabled = False
        End If
        '==============================================================
        If isFillGeneralWeighmentDetailsByJobworkTypeGateInNo = True Then
            If rbtnJobWork.IsChecked = True Then
                txtGateInNoJW.Enabled = True
                controlEnabledisable(False)
            Else
                txtGateInNoJW.Enabled = False
                controlEnabledisable(False)
            End If
        End If
        '================================================================
    End Sub

    
End Class
