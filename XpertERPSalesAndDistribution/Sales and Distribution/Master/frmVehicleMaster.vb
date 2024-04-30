'Created By---> Mayank
'Created Date--->25/may/2011
'Modified By--> mayank
'Last Modify Date-->03/june/2011
'Tables Used-->TSPL_GL_SEGMENT_CODE ,TSPL_VEHICLE_MASTER
'--14/12/12-4:15PM--Update By Pankaj Kumar--Make Vehicle Number Uneditable---------
'--07/april/2014 AM --Update by Meenesh --add new Fields to vehicle master screen
''richa agarwal against ticket no BM00000004327 add Crate capacity field
'Sanjay Ticket No- BHA/01/10/18-000585 Date  01/Oct/2018 Freight detail Import,Export
Imports Microsoft.VisualBasic
Imports System
Imports XpertERPEngine
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Data.OleDb
Imports System.Drawing
Imports System.IO
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Imports System.Text.RegularExpressions
Imports Telerik.WinControls.UI.Export
Imports Telerik.WinControls.UI.Export.ExportToExcelML
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Globalization
Imports System.Threading
Imports common
Public Class frmVehicleMaster
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim userCode, companyCode As String

#Region "Variable"
    Dim MTCapacityRequired As Boolean = False
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim EmployeeNoMandatory As Boolean = False
    Dim Qry As String
    Public Const colSlabUpto As String = "colSlabUpto"
    Public Const colSlabRate As String = "colSlabRate"
#End Region
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company

    End Sub
    Public Sub SetLength()
        fndVehicle_id.MyMaxLength = 12
        rtxtCapacity.MaxLength = 18
        rtxtDescription.MaxLength = 50
        rtxtModel.MaxLength = 50
        rtxtNumber.MaxLength = 50
        rtxtRegistredOn.MaxLength = 100
        rtxtTranType.MaxLength = 50
        rtxtvehicle_Chechis_No.MaxLength = 50
        rtxtVehicle_registration_No.MaxLength = 50


    End Sub
    Private Sub VehicleMaster_Load_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'done by stuti on 04/10/2016 against ticket no - BM00000009928
        EmployeeNoMandatory = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MandatoryEmployeeOnVehicleMaster, clsFixedParameterCode.MandatoryEmployeeOnVehicleMaster, Nothing)) = 1, True, False)

        If EmployeeNoMandatory Then
            fndemployee.MendatroryField = True
        Else
            fndemployee.MendatroryField = False
        End If

        'done by stuti on 14/10/2016 against ticket no BM00000009724'
        MTCapacityRequired = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MTCapacityRequired, clsFixedParameterCode.MTCapacityRequired, Nothing)) = 1, True, False)
        If MTCapacityRequired Then
            pnlmt.Visible = True
        Else
            pnlmt.Visible = False
        End If

        isNewEntry = True
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(rbtnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(rbtnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(rbtnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(rbtnReset, "Press Alt+N Adding New ")
        rbtnDelete.Enabled = False
        rbtnSave.Enabled = True
        ToolTip_Vehicle_Master.SetToolTip(rbtnReset, "New")
        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-GB")
        funReset()
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.vhicleMaster)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        rbtnSave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 03/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If rbtnSave.Visible = True Then
            RadMenuItem_Import.Enabled = True
            RadMenuItem_Export.Enabled = True
        Else
            RadMenuItem_Import.Enabled = False
            RadMenuItem_Export.Enabled = False
        End If
        '--------------------------------------------------
        rbtnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    'It Is Used To Save And Update All Record To TSPL_VEHICLE_MASTER
    Private Sub rbtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnSave.Click
        Save()
    End Sub
    Public Sub Save()

        Dim strvalue As String

        If AllowToSave() Then

            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.vhicleMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return
                End If
            End If

            Dim obj As New ClsVehicleMaster()
            Dim qry = Nothing
            obj.SequenceNo = clsCommon.myCdbl(txtSequenceNo.Value)
            obj.VehicleCode = fndVehicle_id.Value
            obj.Description = rtxtDescription.Text
            obj.Number = rtxtNumber.Text
            If rbtndepot.IsChecked = True Then
                obj.Vehicle_Type = "D"
                fndTransporter.Enabled = False
                obj.TransportId = fndTransporter.Value
            End If
            If rbtnHire.IsChecked = True Then
                obj.Vehicle_Type = "H"
                fndTransporter.Enabled = True
                fndTransporter_Leave()
                obj.TransportId = fndTransporter.Value
            End If
            obj.EmployeeNo = fndemployee.Value

            obj.Vehicle_Reg_no = rtxtVehicle_registration_No.Text
            '   obj.TransportId = fndTransporter.Value
            obj.Vehicle_chasis_No = rtxtvehicle_Chechis_No.Text
            obj.Model = rtxtModel.Text
            obj.Capacity = clsCommon.myCdbl(rtxtCapacity.Text)

            obj.Tran_type = rtxtTranType.Text
            obj.Registered_On = rtxtRegistredOn.Text
            'obj.InsuranceExpirydate = dtpInsuExpDate.Text
            obj.VehicleBrand = rtxtvehiclebrand.Text
            obj.VehicleName = rtxtvehicleName.Text
            obj.EngineNo = rtxtengineno.Text
            obj.Vehicle_No = rtxtvehicleNo.Text
            obj.CrateCapacity = clsCommon.myCdbl(TxtCrateCapacity.Value)

            obj.MTCapacity = clsCommon.myCdbl(txtmtcapacity.Value)
            obj.MTValue = clsCommon.myCdbl(txtmtvalue.Value)

            obj.Location = rtxtlocation.Text
            If txtInsurance_valid_from.Checked = True Then
                obj.Insurance_valid_from = txtInsurance_valid_from.Text
            End If

            If txtInsurance_valid_till.Checked = True Then
                obj.Insurance_valid_Till = txtInsurance_valid_till.Text
            End If

            If txtFitness_valid_from.Checked = True Then
                obj.Fitness_valid_from = txtFitness_valid_from.Text
            End If

            If txtFitness_valid_till.Checked = True Then
                obj.Fitness_valid_Till = txtFitness_valid_till.Text
            End If

            If txtPollution_valid_from.Checked = True Then
                obj.Pollutionchk_valid_from = txtPollution_valid_from.Text
            End If

            If txtPollution_valid_till.Checked = True Then
                obj.Pollutionchk_valid_Till = txtPollution_valid_till.Text
            End If

            If txtRoad_tax_valid_from.Checked = True Then
                obj.RoadTax_valid_from = txtRoad_tax_valid_from.Text
            End If

            If txtRoad_tax_valid_till.Checked = True Then
                obj.RoadTax_valid_Till = txtRoad_tax_valid_till.Text
            End If

            obj.chagrshift = clsCommon.myCdbl(txtchrg.Text)
            obj.avgrate = clsCommon.myCdbl(txtavgkm.Text)
            obj.dieselrate = clsCommon.myCdbl(txtdiesel.Text)

            obj.RentalType = clsCommon.myCstr(cmbRentalType.Text)
            obj.RentalAmount = clsCommon.myCdbl(txtRentalAmt.Text)

            obj.Rate_Type = clsCommon.myCstr(cmbLtrKG.Text)
            obj.Price_Ltr_KG = clsCommon.myCdbl(txt_ltr.Text)
            obj.DriverName = clsCommon.myCstr(txtDriverName.Text)
            obj.pricekm = clsCommon.myCdbl(txt_km.Text)
            obj.status = ""
            obj.Vehicle_Weight = txtVehicleWeight.Value
            If rbtndiesel.IsChecked Then
                obj.status = "Day/Diesel"
            ElseIf rbtnratekm.IsChecked Then
                obj.status = "Rate/K.M"
            ElseIf rbtnrateltr.IsChecked Then
                obj.status = "Rate/Ltr"
            ElseIf rbtnrental.IsChecked Then
                obj.status = "Rental"
            ElseIf rbtKmrange.IsChecked Then
                obj.status = "KM_Range"
                obj.Is_Additional = chkIsAdditional.Checked
            End If
            obj.Column_Crate = clsCommon.myCdbl(txtOneColumnCrate.Value)
            qry = "select vehicle_id from tspl_vehicle_master where vehicle_id='" & obj.VehicleCode & "'"
            strvalue = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            If clsCommon.myLen(strvalue) > 0 Then
                isNewEntry = False
            Else
                isNewEntry = True
            End If
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            If (obj.SaveData(obj, isNewEntry, trans)) Then

                Dim objSlab As clsSlabRangeDetail = Nothing
                Dim arrObjSlab As List(Of clsSlabRangeDetail) = Nothing
                clsSlabRangeDetail.deleteData(Me.Form_ID, obj.VehicleCode, trans)
                If rbtKmrange.Checked Then
                    arrObjSlab = New List(Of clsSlabRangeDetail)
                    For i As Integer = 0 To gv.Rows.Count - 1
                        If clsCommon.myCdbl(gv.Rows(i).Cells(colSlabUpto).Value) > 0 AndAlso clsCommon.myCdbl(gv.Rows(i).Cells(colSlabRate).Value) > 0 Then
                            objSlab = New clsSlabRangeDetail()
                            objSlab.Form_ID = Me.Form_ID
                            objSlab.Trans_ID = obj.VehicleCode
                            objSlab.Slab_Upto = clsCommon.myCdbl(gv.Rows(i).Cells(colSlabUpto).Value)
                            objSlab.Slab_Rate = clsCommon.myCdbl(gv.Rows(i).Cells(colSlabRate).Value)
                            arrObjSlab.Add(objSlab)
                        End If
                    Next
                End If

                If arrObjSlab IsNot Nothing AndAlso arrObjSlab.Count > 0 Then
                    clsSlabRangeDetail.SaveData(arrObjSlab, trans)
                End If
                common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                LoadData(obj.VehicleCode, NavigatorType.Current)
            End If


        End If
    End Sub
    Function AllowToSave() As Boolean
        'Dim obj As New ClsVehicleMaster()
        If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) Then
            If fndVehicle_id.Value = "" Then
                myMessages.blankValue(Me, "Vehicle Code", Me.Text)
                fndVehicle_id.Focus()
                Return False
                'ElseIf rtxtDescription.Text = "" Then
                '    myMessages.blankValue("Description")
                '    rtxtDescription.Focus()
                '    Return False
            End If
        End If
        If EmployeeNoMandatory Then
            If clsCommon.myLen(clsCommon.myCstr(fndemployee.Value)) <= 0 Then
                clsCommon.MyMessageBoxShow("Please fill employee no")
                fndemployee.Focus()
                Return False
            End If
        End If

        If MTCapacityRequired Then
            If clsCommon.myCdbl(txtmtcapacity.Text) > 0 Then
                If clsCommon.myCdbl(txtmtvalue.Text) <= 0 Then
                    clsCommon.MyMessageBoxShow("Please fill MT Value")
                    txtmtvalue.Focus()
                    Return False
                End If
            End If
        End If

        Dim AllowMandFields As String = ""
        AllowMandFields = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowFieldsToBeManadatory, clsFixedParameterCode.AllowFieldsToBeManadatory, Nothing))
        If clsCommon.CompairString(AllowMandFields, "1") = CompairStringResult.Equal Then
            If clsCommon.myCstr(fndVehicle_id.Value.Contains(" ")) = True Then
                clsCommon.MyMessageBoxShow("Please check ! vehicle code does not contain space")
                fndVehicle_id.Focus()
                Return False
            ElseIf rbtndepot.IsChecked = False And rbtnHire.IsChecked = False Then
                clsCommon.MyMessageBoxShow("choose either Vehicle Type")
                rbtndepot.Focus()
                Return False
            ElseIf rbtnHire.IsChecked = True And fndTransporter.Value = "" Then
                myMessages.blankValue(Me, "Transport ID", Me.Text)
                fndTransporter.Focus()
                Return False
            ElseIf rtxtCapacity.Text = "" Then
                myMessages.blankValue(Me, "Capacity", Me.Text)
                rtxtCapacity.Focus()
                Return False
            ElseIf rtxtengineno.Text = "" Then
                myMessages.blankValue(Me, "Engine No", Me.Text)
                rtxtengineno.Focus()
                Return False
            ElseIf rtxtlocation.Text = "" Then
                myMessages.blankValue(Me, "Location", Me.Text)
                rtxtlocation.Focus()
                Return False
            ElseIf rtxtModel.Text = "" Then
                myMessages.blankValue(Me, "Model", Me.Text)
                rtxtModel.Focus()
                Return False
            ElseIf rtxtRegistredOn.Text = "" Then
                myMessages.blankValue(Me, "Registered On", Me.Text)
                rtxtRegistredOn.Focus()
                Return False

            ElseIf rtxtvehicle_Chechis_No.Text = "" Then
                myMessages.blankValue(Me, "Chasis No", Me.Text)
                rtxtvehicle_Chechis_No.Focus()
                Return False

            ElseIf rtxtvehiclebrand.Text = "" Then
                myMessages.blankValue(Me, "Vehicle Brand", Me.Text)
                rtxtvehiclebrand.Focus()
                Return False
            ElseIf rtxtvehicleName.Text = "" Then
                myMessages.blankValue(Me, "Vehicle Name", Me.Text)
                rtxtvehicleName.Focus()
                Return False
            ElseIf rtxtvehicleNo.Text = "" Then
                myMessages.blankValue(Me, "Vehicle No", Me.Text)
                rtxtvehicleNo.Focus()
                Return False
            ElseIf (txtInsurance_valid_from.Checked And txtInsurance_valid_till.Checked) Then
                If (txtInsurance_valid_from.Value > txtInsurance_valid_till.Value) Then
                    clsCommon.MyMessageBoxShow("Insurance Valid From Date cannot be less than Insurance Valid Till date ")
                    txtInsurance_valid_from.Focus()
                    Return False
                End If
            ElseIf (txtFitness_valid_from.Checked And txtFitness_valid_till.Checked) Then
                If (txtFitness_valid_from.Value > txtFitness_valid_till.Value) Then
                    clsCommon.MyMessageBoxShow("Fitness Valid From Date cannot be less than Fitness Valid Till date ")
                    txtFitness_valid_from.Focus()
                    Return False
                End If
            ElseIf (txtPollution_valid_from.Checked And txtPollution_valid_till.Checked) Then
                If (txtPollution_valid_from.Value > txtPollution_valid_till.Value) Then
                    clsCommon.MyMessageBoxShow("Pollution check Valid From Date cannot be less than Pollution check Valid Till date ")
                    txtPollution_valid_from.Focus()
                    Return False
                End If
            ElseIf (txtRoad_tax_valid_from.Checked And txtRoad_tax_valid_till.Checked) Then
                If (txtRoad_tax_valid_from.Value > txtRoad_tax_valid_till.Value) Then
                    clsCommon.MyMessageBoxShow("Road Tax Valid From Date cannot be less than Road Tax Valid Till date ")
                    txtRoad_tax_valid_from.Focus()
                    Return False
                End If
            End If
        End If
        Return True
    End Function
    ''It Is Used To Delete The Record From TSPL_VEHICLE_MASTER
    Private Sub rbtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnDelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(fndVehicle_id.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("You Cannot Delete Record")
            Exit Sub
        End If

        funDelete()
    End Sub

    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (ClsVehicleMaster.DeleteData(fndVehicle_id.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    'It Is Used To Close The Current Windows Form
    Private Sub rbtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnClose.Click
        Me.Close()
    End Sub
    'It is Used To Clear All Fields Of Current Windows Form
    Private Sub rbtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnReset.Click
        funReset()
    End Sub

    'This is Reset Function Used To Clear All Fields Of Current Windows Form
    Private Sub funReset()
        Dim AllowMandFields As String = ""
        fndemployee.Value = ""
        txtSequenceNo.Value = 0
        fndVehicle_id.Value = ""
        fndVehicle_id.MyReadOnly = False
        rtxtModel.Text = ""
        rtxtNumber.Text = ""
        rtxtDescription.Text = ""
        rbtndepot.IsChecked = False
        rbtnHire.IsChecked = False
        rtxtvehicle_Chechis_No.Text = ""
        rtxtVehicle_registration_No.Text = ""
        rtxtCapacity.Text = ""
        rtxtTranType.Text = ""
        fndTransporter.Value = ""
        txtDriverName.Text = ""
        fndTransporter.Enabled = True
        rbtnSave.Text = "Save"
        rbtnDelete.Enabled = False
        fndVehicle_id.Enabled = True
        ' dtpInsuExpDate.Checked = False
        txtInsurance_valid_from.Checked = False
        txtInsurance_valid_from.Value = txtInsurance_valid_from.Text
        txtInsurance_valid_till.Checked = False
        txtInsurance_valid_till.Value = txtInsurance_valid_till.Text
        txtFitness_valid_from.Checked = False
        txtFitness_valid_from.Value = txtFitness_valid_from.Text
        txtFitness_valid_till.Checked = False
        txtPollution_valid_from.Value = txtPollution_valid_from.Text
        txtPollution_valid_from.Checked = False
        txtPollution_valid_till.Value = txtPollution_valid_till.Text
        txtPollution_valid_till.Checked = False
        txtRoad_tax_valid_from.Checked = False
        txtRoad_tax_valid_from.Value = txtRoad_tax_valid_from.Text
        txtRoad_tax_valid_till.Checked = False
        txtRoad_tax_valid_till.Value = txtRoad_tax_valid_till.Text
        txtVehicleWeight.Value = 0
        rtxtlocation.Text = ""
        rtxtengineno.Text = ""
        rtxtNumber.Text = ""
        rtxtRegistredOn.Text = ""
        rtxtVehicle_registration_No.Text = ""
        rtxtvehicleName.Text = ""
        rtxtvehiclebrand.Text = ""
        rtxtvehicleNo.Text = ""
        TxtCrateCapacity.Value = 0
        txtOneColumnCrate.Value = 0
        AllowMandFields = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowFieldsToBeManadatory, clsFixedParameterCode.AllowFieldsToBeManadatory, Nothing))
        If clsCommon.CompairString(AllowMandFields, "1") = CompairStringResult.Equal Then
            rtxtlocation.MendatroryField = True
            rtxtCapacity.MendatroryField = True
            rtxtengineno.MendatroryField = True
            rtxtRegistredOn.MendatroryField = True
            rtxtVehicle_registration_No.MendatroryField = True
            rtxtvehicleName.MendatroryField = True
            rtxtvehiclebrand.MendatroryField = True
            rtxtvehicleNo.MendatroryField = True
            rtxtModel.MendatroryField = True
            rtxtvehicle_Chechis_No.MendatroryField = True
            rtxtTranType.MendatroryField = True
        Else
            rtxtlocation.MendatroryField = False
            rtxtCapacity.MendatroryField = False
            rtxtengineno.MendatroryField = False
            rtxtRegistredOn.MendatroryField = False
            rtxtVehicle_registration_No.MendatroryField = False
            rtxtvehicleName.MendatroryField = False
            rtxtvehiclebrand.MendatroryField = False
            rtxtvehicleNo.MendatroryField = False
            rtxtModel.MendatroryField = False
            rtxtvehicle_Chechis_No.MendatroryField = False
            rtxtTranType.MendatroryField = False
        End If
        txtchrg.Text = ""
        txtavgkm.Text = ""
        txtdiesel.Text = ""
        txtRentalAmt.Text = ""
        cmbRentalType.Enabled = False
        cmbRentalType.SelectedIndex = 0
        cmbLtrKG.SelectedIndex = 0
        txt_ltr.Text = ""
        txt_km.Text = ""
        txt_ltr.Text = ""

        txtmtcapacity.Text = 0
        txtmtvalue.Text = 0

        rbtndiesel.Checked = False
        rbtnratekm.Checked = False
        rbtnrateltr.Checked = False
        rbtnrental.Checked = False
        rbtKmrange.Checked = False
        chkIsAdditional.Checked = False
        loadBlankgv()
        gv.Rows.AddNew()
        'dtpInsuExpDate.Value = Now()
        'fndTransporter.txtValue.ThemeClassName = Office2010BlueTheme
        'fndTransporter.ValueBoxElement.TextBoxItem.BackColor = Office2010Blue
    End Sub

    'It Validate User To Press The Keys 
    Private Sub fndVehicle_id_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        'fndVehicle_id.Value.CharacterCasing = CharacterCasing.Upper
        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True

        End If
    End Sub
    'It Is Used To Export The Records From TSPL_VEHICLE_MASTER
    Private Sub RadMenuItem_Export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem_Export.Click
        'Dim Sql As String = "Select Vehicle_Id as [Vehicle Id],Model, Number,Description,Vehicle_no as [Vehicle No],Vehicle_Type as [Type],Vehicle_Reg_No as [Vehicle Reg No],Vehicle_Chesis_No as [Vehicle Chasis No],Capacity,Trans_type as [Trans Type],transport_id as[Transport Id],RegisteredOn as [Registered On],Vehicle_Brand as [Vehicle Brand],Vehicle_Name as [Vehicle Name],Engine_NO as [Engine No],Insurance_valid_From as [insurance Valid Frm],insurance_valid_Till as [Insurance Valid Till],Fitness_valid_From as [Fitness Valid Frm],Fitness_valid_Till as [Fitness Valid Till],pollutionCheck_valid_From as [Pollution Valid Frm],PollutionCheck_valid_till as [Pollution check Valid Till],Roadtax_valid_From as [Road Tax Valid Frm] ,Roadtax_valid_till as [Road Tax Valid Till] ,Location,CrateCapacity as [Crate Capacity],Employee_Id as [Employee No],MTCapacity as [MT Capacity],MTValue as [MT Value] from TSPL_VEHICLE_MASTER"
        Dim Sql As String = "Select Vehicle_Id as [Vehicle Id],Model, Number,Description,Vehicle_no as [Vehicle No],Vehicle_Type as [Type],Vehicle_Reg_No as [Vehicle Reg No],Vehicle_Chesis_No as [Vehicle Chasis No],Capacity,Trans_type as [Trans Type],transport_id as[Transport Id],RegisteredOn as [Registered On],Vehicle_Brand as [Vehicle Brand],Vehicle_Name as [Vehicle Name],Engine_NO as [Engine No],Insurance_valid_From as [insurance Valid Frm],insurance_valid_Till as [Insurance Valid Till],Fitness_valid_From as [Fitness Valid Frm],Fitness_valid_Till as [Fitness Valid Till],pollutionCheck_valid_From as [Pollution Valid Frm],PollutionCheck_valid_till as [Pollution check Valid Till],Roadtax_valid_From as [Road Tax Valid Frm] ,Roadtax_valid_till as [Road Tax Valid Till] ,Location,CrateCapacity as [Crate Capacity],Employee_Id as [Employee No],MTCapacity as [MT Capacity],MTValue as [MT Value],isnull(STATUS,'') as [Basis of Freight Payments],Shift_Charges as [Charges per Shift],Avg_Km_Ltr as [Average KM per Ltr],Diesel_Rate as [Rate of Diesel],Rental_type  as [Rental Type],Rental_Amount as [Rental Amount],Price_Ltr_KG as [Rate LtrKG],Rate_Type as [Rate Type],Price_KM as [Rate Per KM],Is_Additional as [Is Additional] "
        For j As Integer = 1 To 10
            Sql += " ,(select Slab_Upto from (Select ROW_NUMBER () over (order by Trans_ID,Slab_Upto ) As SNo,Trans_ID,Slab_Upto From tspl_slab_range_detail where Form_ID='" & Me.Form_ID & "' and Trans_ID=TSPL_VEHICLE_MASTER.Vehicle_Id)xxx where xxx.SNo=" & j & ") as Slab_Upto" & j & ""
            Sql += " ,(select Slab_Rate from (Select ROW_NUMBER () over (order by Trans_ID,Slab_Upto ) As SNo,Trans_ID,Slab_Rate From tspl_slab_range_detail where Form_ID='" & Me.Form_ID & "' and Trans_ID=TSPL_VEHICLE_MASTER.Vehicle_Id)xxx where xxx.SNo=" & j & ") as Slab_Rate" & j & ""
        Next
        Sql += " from TSPL_VEHICLE_MASTER"
        ListImpExpColumnsMandatory = New List(Of String)({"Vehicle Id", "Employee No", "MT Value"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"Vehicle Id"})
        transportSql.ExporttoExcel(Sql, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
    End Sub
    'It Is Used To Import The Records From TSPL_VEHICLE_MASTER
    Private Sub RadMenuItem_Import_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem_Import.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        'If transportSql.importExcel(gv, "Vehicle Id", "Model", "Number", "Description", "Vehicle No", "Type", "Vehicle Reg No", "Vehicle Chasis No", "Capacity", "Trans Type", "Transport Id", "Registered On", "Vehicle Brand", "Vehicle Name", "Engine No", "insurance Valid Frm", "Insurance Valid Till", "Fitness Valid Frm", "Fitness Valid Till", "Pollution Valid Frm", "Pollution check Valid Till", "Road Tax Valid Frm", "Road Tax Valid Till", "Location", "Crate Capacity", "Employee No", "MT Capacity", "MT Value") Then
        If transportSql.importExcel(gv, "Vehicle Id", "Model", "Number", "Description", "Vehicle No", "Type", "Vehicle Reg No", "Vehicle Chasis No", "Capacity", "Trans Type", "Transport Id", "Registered On", "Vehicle Brand", "Vehicle Name", "Engine No", "insurance Valid Frm", "Insurance Valid Till", "Fitness Valid Frm", "Fitness Valid Till", "Pollution Valid Frm", "Pollution check Valid Till", "Road Tax Valid Frm", "Road Tax Valid Till", "Location", "Crate Capacity", "Employee No", "MT Capacity", "MT Value", "Basis of Freight Payments", "Charges per Shift", "Average KM per Ltr", "Rate of Diesel", "Rental Type", "Rental Amount", "Rate LtrKG", "Rate Type", "Rate Per KM") Then
            Dim trans As SqlTransaction = Nothing
            Try
                'connectSql.OpenConnection()
                'trans = clsDBFuncationality.GetTransactin()
                ' Dim chk As Boolean = True
                ' clsCommon.ProgressBarShow()
                Dim rowCount As Int64 = gv.Rows.Count
                Dim rowIndex As Int64 = 0
                clsCommon.ProgressBarPercentShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    rowIndex = rowIndex + 1
                    'Dim LineNo As String = clsCommon.myCstr(grow.Index) + 2
                    clsCommon.ProgressBarPercentUpdate((rowIndex * 100) / rowCount, " Total " & rowIndex & " Records of " & rowCount & " Imported")
                    Dim obj As New ClsVehicleMaster()


                    Dim strv_id As String = clsCommon.myCstr(grow.Cells("Vehicle Id").Value)
                    If clsCommon.myLen(strv_id) <= 12 Then
                        Dim dt As DataTable
                        dt = clsDBFuncationality.GetDataTable("select * from TSPL_GL_SEGMENT_CODE where Seg_No='2' and Segment_Code='" + clsCommon.myCstr(strv_id) + "'")
                        If dt.Rows.Count <= 0 Then
                            Throw New Exception("Vehicle Code not exists in Segment Code master.")
                        Else
                            obj.VehicleCode = strv_id
                        End If
                    Else
                        Throw New Exception("Vehicle Code has some incorrect values")
                    End If



                    Dim strmodel As String = clsCommon.myCstr(grow.Cells("Model").Value)
                    If clsCommon.myLen(strmodel) > 50 Then
                        Throw New Exception("Model Can not be greater than 50")
                    End If
                    obj.Model = strmodel

                    Dim strNumber As String = clsCommon.myCstr(grow.Cells("Number").Value)
                    If clsCommon.myLen(strNumber) > 50 Then
                        Throw New Exception("Value of Number can not be greater than 50")
                    End If
                    obj.Number = strNumber

                    Dim strDesc As String = clsCommon.myCstr(grow.Cells("Description").Value)
                    If clsCommon.myLen(strDesc) > 50 Then
                        Throw New Exception("Description Can not be greater than 50")
                    End If

                    obj.Description = strDesc


                    Dim strVehicle_No As String = clsCommon.myCstr(grow.Cells("Vehicle No").Value)
                    If clsCommon.myLen(strVehicle_No) > 50 Then
                        Throw New Exception("Vehicle No not be blank")

                    End If
                    obj.Vehicle_No = strVehicle_No


                    Dim strType As String = clsCommon.myCstr(grow.Cells("Type").Value)

                    If strType = "d" Or strType = "D" Then
                        obj.Vehicle_Type = strType
                    ElseIf strType = "H" Or strType = "h" Then
                        obj.Vehicle_Type = strType
                    ElseIf clsCommon.myLen(strType) > 50 Then
                        clsCommon.MyMessageBoxShow("Vehicle Type has some incorrect values.You must Enter Depot or Hire")

                    End If
                    obj.Vehicle_Type = strType




                    Dim strReg_no As String = clsCommon.myCstr(grow.Cells("Vehicle Reg No").Value)
                    If clsCommon.myLen(strReg_no) > 50 Then
                        Throw New Exception("Value Of Vehicle Registration Number can not be greater than 50")
                    End If
                    obj.Vehicle_Reg_no = strReg_no

                    Dim strChesis_no As String = clsCommon.myCstr(grow.Cells("Vehicle Chasis No").Value)
                    If clsCommon.myLen(strChesis_no) > 50 Then
                        Throw New Exception("Value Of Vehicle chesis Number can not be greater than 50")
                    End If
                    obj.Vehicle_chasis_No = strChesis_no


                    Dim strCapacity As Double = clsCommon.myCdbl(grow.Cells("Capacity").Value)
                    If clsCommon.myLen(strCapacity) > 18 Then
                        Throw New Exception("Capacity can not be greater than 50")
                    End If
                    obj.Capacity = strCapacity


                    Dim strTrans_Type As String = clsCommon.myCstr(grow.Cells("Trans Type").Value)
                    If clsCommon.myLen(strTrans_Type) > 50 Then
                        Throw New Exception("Trans Typecan not be greater than 50")
                    End If
                    obj.Tran_type = strTrans_Type

                    Dim strTransportId As String = clsCommon.myCstr(grow.Cells("Transport Id").Value)
                    If clsCommon.myLen(strTransportId) > 50 Then
                        Throw New Exception("Transport Id can not be greater than 50")
                    End If
                    obj.TransportId = strTransportId

                    Dim strRegistered_On As String = clsCommon.myCstr(grow.Cells("Registered On").Value)
                    If clsCommon.myLen(strRegistered_On) > 50 Then
                        Throw New Exception("Registered On can not be greater than 50")

                    End If
                    obj.Registered_On = strRegistered_On


                    Dim strVehicleBrand As String = clsCommon.myCstr(grow.Cells("Vehicle Brand").Value)
                    If clsCommon.myLen(strVehicleBrand) > 50 Then
                        Throw New Exception("vehicle Brand can not be greater than 50")

                    End If
                    obj.VehicleBrand = strVehicleBrand


                    Dim strVehicleName As String = clsCommon.myCstr(grow.Cells("Vehicle Name").Value)
                    If clsCommon.myLen(strVehicleName) > 50 Then
                        Throw New Exception("vehicle Name can not be greater than 50")

                    End If
                    obj.VehicleName = strVehicleName







                    Dim strEngineNo As String = clsCommon.myCstr(grow.Cells("Engine No").Value)
                    If clsCommon.myLen(strEngineNo) > 50 Then
                        Throw New Exception("Engine no can not be greater than 50")

                    End If
                    obj.EngineNo = strEngineNo


                    'Dim strInsuranceExpirydate As String = clsCommon.myCstr(grow.Cells(14).Value)
                    'If clsCommon.myLen(strInsuranceExpirydate) > 0 Then
                    '    Throw New Exception("Insurance Expiry date cannot be blank")

                    'End If
                    'obj.InsuranceExpirydate = strInsuranceExpirydate




                    Dim strInsurance_valid_from As String = clsCommon.myCstr(grow.Cells("insurance Valid Frm").Value)
                    If clsCommon.myLen(strInsurance_valid_from) = Nothing Then
                        obj.Insurance_valid_from = Nothing
                    Else
                        obj.Insurance_valid_from = clsCommon.myCDate(strInsurance_valid_from)

                    End If




                    Dim strInsurance_valid_Till As String = clsCommon.myCstr(grow.Cells("Insurance Valid Till").Value)
                    If clsCommon.myLen(strInsurance_valid_Till) = Nothing Then
                        obj.Insurance_valid_Till = Nothing
                    Else
                        obj.Insurance_valid_Till = clsCommon.myCDate(strInsurance_valid_Till)

                    End If


                    Dim strFitness_valid_from As String = clsCommon.myCstr(grow.Cells("Fitness Valid Frm").Value)
                    If clsCommon.myLen(strFitness_valid_from) = Nothing Then
                        obj.Fitness_valid_from = Nothing
                    Else

                        obj.Fitness_valid_from = clsCommon.myCDate(strFitness_valid_from)

                    End If
                    Dim strFitness_valid_Till As String = clsCommon.myCstr(grow.Cells("Fitness Valid Till").Value)
                    If clsCommon.myLen(strFitness_valid_Till) = Nothing Then
                        strFitness_valid_Till = Nothing
                    Else
                        obj.Fitness_valid_Till = clsCommon.myCDate(strFitness_valid_Till)
                    End If

                    Dim strPollutionchk_valid_from As String = clsCommon.myCstr(grow.Cells("Pollution Valid Frm").Value)
                    If clsCommon.myLen(strPollutionchk_valid_from) = Nothing Then
                        strPollutionchk_valid_from = Nothing
                    Else
                        obj.Pollutionchk_valid_from = clsCommon.myCDate(strPollutionchk_valid_from)
                    End If


                    Dim strPollutionchk_valid_Till As String = clsCommon.myCstr(grow.Cells("Pollution check Valid Till").Value)
                    If clsCommon.myLen(strPollutionchk_valid_Till) = Nothing Then
                        strPollutionchk_valid_Till = Nothing
                    Else
                        obj.Pollutionchk_valid_Till = clsCommon.myCDate(strPollutionchk_valid_Till)

                    End If



                    Dim strRoadTax_valid_from As String = clsCommon.myCstr(grow.Cells("Road Tax Valid Frm").Value)
                    If clsCommon.myLen(strRoadTax_valid_from) = Nothing Then
                        obj.RoadTax_valid_from = Nothing
                    Else
                        obj.RoadTax_valid_from = clsCommon.myCDate(strRoadTax_valid_from)

                    End If

                    Dim strRoadTax_valid_Till As String = clsCommon.myCstr(grow.Cells("Road Tax Valid Till").Value)
                    If clsCommon.myLen(strRoadTax_valid_Till) = Nothing Then
                        obj.RoadTax_valid_Till = Nothing
                    Else
                        obj.RoadTax_valid_Till = clsCommon.myCDate(strRoadTax_valid_Till)
                    End If


                    Dim strLocation As String = clsCommon.myCstr(grow.Cells("Location").Value)
                    If clsCommon.myLen(strLocation) > 50 Then
                        Throw New Exception("location Can not be greater than 50")

                    End If

                    Dim strEmployeeNo As String = clsCommon.myCstr(grow.Cells("Employee No").Value)
                    If clsCommon.myLen(strEmployeeNo) > 0 Then
                        Dim dt As DataTable
                        dt = clsDBFuncationality.GetDataTable("select * from TSPL_EMPLOYEE_MASTER where EMP_Code='" + clsCommon.myCstr(strEmployeeNo) + "'")
                        If dt.Rows.Count <= 0 Then
                            Throw New Exception("Employee No not exists in employee master.")
                        Else
                            obj.EmployeeNo = strEmployeeNo
                        End If
                    Else
                        If EmployeeNoMandatory Then
                            Throw New Exception("Please fill employee no.")
                        End If
                    End If

                    Dim cratecapacity As Double = clsCommon.myCdbl(grow.Cells("Crate Capacity").Value)

                    If MTCapacityRequired Then
                        Dim MTCapacity As Double = clsCommon.myCdbl(grow.Cells("MT Capacity").Value)
                        obj.MTCapacity = MTCapacity
                        Dim MTValue As Double = clsCommon.myCdbl(grow.Cells("MT Value").Value)
                        obj.MTValue = MTValue
                        If MTCapacity > 0 Then
                            If MTValue <= 0 Then
                                Throw New Exception("Please fill MT Value")
                            End If
                        End If
                    End If

                    obj.CrateCapacity = cratecapacity
                    obj.Location = strLocation

                    'sanjay'''''''''''''
                    obj.status = ""
                    obj.chagrshift = 0
                    obj.avgrate = 0
                    obj.dieselrate = 0
                    obj.pricekm = 0
                    obj.Price_Ltr_KG = 0
                    obj.Rate_Type = ""
                    obj.RentalType = ""
                    obj.RentalAmount = 0

                    Dim BasisofFreightPayments As String = clsCommon.myCstr(grow.Cells("Basis of Freight Payments").Value)

                    If clsCommon.myLen(BasisofFreightPayments) > 0 Then
                        If Not (clsCommon.CompairString(BasisofFreightPayments, "Day/Diesel") = CompairStringResult.Equal OrElse clsCommon.CompairString(BasisofFreightPayments, "Rate/K.M") = CompairStringResult.Equal OrElse clsCommon.CompairString(BasisofFreightPayments, "Rate/Ltr") = CompairStringResult.Equal OrElse clsCommon.CompairString(BasisofFreightPayments, "Rental") = CompairStringResult.Equal OrElse clsCommon.CompairString(BasisofFreightPayments, "KM_Range") = CompairStringResult.Equal) Then
                            Throw New Exception("Invalid Basis of Freight Payments at line no" + clsCommon.myCstr(rowIndex) + ",Enter Day/Diesel or Rate/K.M or Rate/Ltr or Rental")
                        End If
                    End If

                    If clsCommon.CompairString(BasisofFreightPayments, "Rental") = CompairStringResult.Equal Then
                        If Not (clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Rental Type").Value), "Day") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Rental Type").Value), "Month") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Rental Type").Value), "Year") = CompairStringResult.Equal) Then
                            Throw New Exception("Invalid Rental Type at line no" + clsCommon.myCstr(rowIndex) + ",Enter Day or Month or Year")
                        End If
                    End If

                    If clsCommon.CompairString(BasisofFreightPayments, "Rate/Ltr") = CompairStringResult.Equal Then
                        If Not (clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Rate Type").Value), "LTR") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Rate Type").Value), "KG") = CompairStringResult.Equal) Then
                            Throw New Exception("Invalid Rate Type at line no" + clsCommon.myCstr(rowIndex) + ",Enter LTR or KG")
                        End If
                    End If

                    obj.status = BasisofFreightPayments

                    If clsCommon.CompairString(BasisofFreightPayments, "Day/Diesel") = CompairStringResult.Equal Then
                        obj.chagrshift = clsCommon.myCdbl(grow.Cells("Charges per Shift").Value)
                        obj.avgrate = clsCommon.myCdbl(grow.Cells("Average KM per Ltr").Value)
                        obj.dieselrate = clsCommon.myCdbl(grow.Cells("Rate of Diesel").Value)
                    ElseIf clsCommon.CompairString(BasisofFreightPayments, "Rate/K.M") = CompairStringResult.Equal Then
                        obj.pricekm = clsCommon.myCdbl(grow.Cells("Rate Per KM").Value)
                    ElseIf clsCommon.CompairString(BasisofFreightPayments, "Rate/Ltr") = CompairStringResult.Equal Then
                        obj.Price_Ltr_KG = clsCommon.myCdbl(grow.Cells("Rate LtrKG").Value)
                        obj.Rate_Type = clsCommon.myCstr(grow.Cells("Rate Type").Value)
                    ElseIf clsCommon.CompairString(BasisofFreightPayments, "Rental") = CompairStringResult.Equal Then
                        obj.RentalType = clsCommon.myCstr(grow.Cells("Rental Type").Value)
                        obj.RentalAmount = clsCommon.myCdbl(grow.Cells("Rental Amount").Value)
                    End If

                    If BasisofFreightPayments = "KM_Range" Then
                        obj.Is_Additional = IIf(clsCommon.myCstr(grow.Cells("Is Additional").Value) = "T", True, False)
                    Else
                        obj.Is_Additional = False
                    End If


                    'sanjay''''''''''''

                    obj.SaveData(obj, ClsVehicleMaster.CheckNewEntry(obj.VehicleCode), Nothing)

                    'sanjay''''''''''''
                    If BasisofFreightPayments = "KM_Range" Then
                        Dim arrObjSlab As New List(Of clsSlabRangeDetail)
                        clsSlabRangeDetail.deleteData(Me.Form_ID, obj.VehicleCode, trans)
                        For j As Integer = 1 To 10
                            Dim Temp_Slab_Upto As Double
                            Dim Temp_Slab_Rate As Double
                            Temp_Slab_Upto = clsCommon.myCdbl(grow.Cells("Slab_Upto" & clsCommon.myCstr(j) & "").Value)
                            Temp_Slab_Rate = clsCommon.myCdbl(grow.Cells("Slab_Rate" & clsCommon.myCstr(j) & "").Value)
                            If Temp_Slab_Upto > 0 AndAlso Temp_Slab_Rate > 0 Then
                                Dim objSlab As New clsSlabRangeDetail()
                                objSlab.Form_ID = Me.Form_ID
                                objSlab.Trans_ID = obj.VehicleCode
                                objSlab.Slab_Upto = Temp_Slab_Upto
                                objSlab.Slab_Rate = Temp_Slab_Rate
                                arrObjSlab.Add(objSlab)
                            End If
                        Next
                        If arrObjSlab IsNot Nothing AndAlso arrObjSlab.Count > 0 Then
                            clsSlabRangeDetail.SaveData(arrObjSlab, trans)
                        End If

                    End If
                    'sanjay''''''''''''

                Next
                'clsCommon.ProgressBarHide()
                clsCommon.ProgressBarPercentHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                'clsCommon.ProgressBarHide()
                Try
                    clsCommon.ProgressBarPercentHide()
                Catch ex1 As Exception
                End Try
                myMessages.myExceptions(ex)
            End Try
        End If

    End Sub
    'It Is Used To Check The Value Of Finder(fndVehicle_id),Is Present In Database Or Not
    'Sub fndVehicle_id_Leave()
    '    If fndVehicle_id.Value = "" Then
    '    Else
    '        Dim str As String = "select Segment_code from TSPL_GL_SEGMENT_CODE where Segment_code='" + fndVehicle_id.Value + "'"

    '        Dim strvalue As String = clsDBFuncationality.getSingleValue(str)

    '        If strvalue <> "" Then

    '        End If
    'End Sub
    'Private Sub fndVehicle_id_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    '  fndVehicle_id_Leave()
    '    'If userCode <> "ADMIN" Then
    '    '    If funSetUserAccess() = False Then Exit Sub
    '    'End If
    'End Sub
    'It Is Used To Close The Current Windows Form
    Private Sub RadMenuItem_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem_Close.Click
        Me.Close()
    End Sub
    'It Is Used To Give The Authority To User,To Access This Form (Vehicle Master) Or Not.(It Is Bassed On Mapping)
    Private Function funSetUserAccess() As Boolean
        Try
            Dim strRights As String
            Dim strTemp() As String
            Dim strProgCode = "VEHICLE-M"
            strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
            strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
            strTemp = Split(strRights, ",")
            If strTemp(0) = "0" Then
                clsCommon.MyMessageBoxShow("Permission Denied")
                funSetUserAccess = False
                blnRead = False
                Me.Close()
                Exit Function
            Else
                blnRead = True
            End If
            If strTemp(1) = "0" Then 'Grant modify access
                rbtnSave.Enabled = False
            End If
            If strTemp(2) = "0" Then 'Grant modify access
                rbtnDelete.Enabled = False
            End If

            funSetUserAccess = True
        Catch er As Exception
            clsCommon.MyMessageBoxShow(er.Message)
            Return False
        End Try
        Return True
    End Function

    Private Sub frmVehicleMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Control And e.KeyCode.ToString() = "S" Then
            If fndVehicle_id.Value = "" Then
                myMessages.blankValue(Me, "Vehicle Code", Me.Text)
                fndVehicle_id.Focus()
            Else
                'funinsert()
            End If
        End If
        If e.Control And e.KeyCode.ToString() = "U" Then
            If fndVehicle_id.Value = "" Then
                myMessages.blankValue(Me, "Vehicle Code", Me.Text)
                fndVehicle_id.Focus()
            Else
                'funUpdate()
            End If
        End If
        If e.Control And e.KeyCode.ToString() = "D" Then
            If fndVehicle_id.Value = "" Then
                myMessages.blankValue(Me, "Vehicle Code", Me.Text)
                fndVehicle_id.Focus()
            Else
                funDelete()
            End If
        End If
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso rbtnSave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            'PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso rbtnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Sub fndTransporter_Leave()
        If rbtnHire.IsChecked = True Then
            fndTransporter.Enabled = True
            Dim strQuery As String = "select Transport_Id from TSPL_TRANSPORT_MASTER where Transport_Id ='" + fndTransporter.Value + "'"

            Dim strvalue As String = clsDBFuncationality.getSingleValue(strQuery)
            If strvalue <> "" Then
                fndTransporter.Value = strvalue
            Else : strvalue = ""
                common.clsCommon.MyMessageBoxShow("Transport ID Does Not Exist in Master Table")
                fndTransporter.Value = ""
                fndTransporter.Focus()
            End If
        ElseIf rbtndepot.IsChecked = True Then
            fndTransporter.Enabled = False
        End If


    End Sub

    'It Validate User To Press The Keys 
    Private Sub fndTransporter_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        ' fndTransporter.txtValue.CharacterCasing = CharacterCasing.Upper
        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub rbtndepot_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtndepot.ToggleStateChanged
        fndTransporter.Value = ""
        fndTransporter.Enabled = False
    End Sub

    Private Sub rbtnHire_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnHire.ToggleStateChanged
        fndTransporter.Value = ""
        fndTransporter.Enabled = True
    End Sub



    Private Sub fndVehicle_id__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndVehicle_id._MYValidating

        Dim str As String = "select count(*) from TSPL_Vehicle_master where Vehicle_id ='" + fndVehicle_id.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 Then
            fndVehicle_id.MyReadOnly = False
        Else
            fndVehicle_id.MyReadOnly = True
        End If

        If fndVehicle_id.MyReadOnly OrElse isButtonClicked Then

            ' Dim qry As String = "select Segment_code as[segmentcode],Description from TSPL_GL_SEGMENT_CODE  "
            'fndVehicle_id.Value = clsCommon.ShowSelectForm("GroupCodFND", qry, "Segmentcode", "Seg_No='2'", fndVehicle_id.Value, "", isButtonClicked)
            fndVehicle_id.Value = ClsVehicleMaster.getFinder(" ", fndVehicle_id.Value, isButtonClicked)
            LoadData(fndVehicle_id.Value, NavigatorType.Current)
        End If


    End Sub

    Private Sub fndVehicle_id__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavigatorType As common.NavigatorType) Handles fndVehicle_id._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_VEHICLE_MASTER where Vehicle_Id='" + fndVehicle_id.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                fndVehicle_id.MyReadOnly = False
            Else
                fndVehicle_id.MyReadOnly = True
            End If
            LoadData(fndVehicle_id.Value, NavigatorType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try
            rbtndiesel.IsChecked = False
            rbtnratekm.IsChecked = False
            rbtnrateltr.IsChecked = False
            rbtnrental.IsChecked = False
            rbtKmrange.Checked = False
            chkIsAdditional.Checked = False
            loadBlankgv()

            rbtnSave.Enabled = True

            rbtnDelete.Enabled = True
            'isInsideLoadData = True
            isNewEntry = False
            rbtnSave.Text = "Update"

            'fndLocation.Enabled = False
            Dim obj As New ClsVehicleMaster()
            obj = ClsVehicleMaster.GetData(strCode, NavType)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.VehicleCode) > 0) Then
                txtmtcapacity.Value = clsCommon.myCdbl(obj.MTCapacity)
                txtmtvalue.Value = clsCommon.myCdbl(obj.MTValue)
                txtSequenceNo.Value = obj.SequenceNo
                fndVehicle_id.Value = obj.VehicleCode
                fndVehicle_id.MyReadOnly = False
                rtxtModel.Text = obj.Model
                rtxtNumber.Text = obj.Number
                txtDriverName.Text = obj.DriverName
                rtxtDescription.Text = obj.Description
                fndemployee.Value = clsCommon.myCstr(obj.EmployeeNo)
                If (obj.Vehicle_Type = "D" Or obj.Vehicle_Type = "d") Then
                    rbtndepot.IsChecked = True

                Else : rbtndepot.IsChecked = False

                End If
                txtVehicleWeight.Value = obj.Vehicle_Weight
                If (obj.Vehicle_Type = "H" Or obj.Vehicle_Type = "h") Then
                    rbtnHire.IsChecked = True
                Else : rbtnHire.IsChecked = False

                End If
                rtxtvehicle_Chechis_No.Text = obj.Vehicle_chasis_No
                rtxtVehicle_registration_No.Text = obj.Vehicle_Reg_no
                rtxtCapacity.Text = obj.Capacity
                rtxtTranType.Text = obj.Tran_type
                fndTransporter.Value = obj.TransportId
                'dtpInsuExpDate.value= obj.InsuranceExpirydate
                If (obj.Fitness_valid_from IsNot Nothing) Then
                    txtFitness_valid_from.Value = obj.Fitness_valid_from
                    txtFitness_valid_from.Checked = True

                End If
                If (obj.Fitness_valid_Till IsNot Nothing) Then
                    txtFitness_valid_till.Value = obj.Fitness_valid_Till
                    txtFitness_valid_till.Checked = True
                End If
                If (obj.Insurance_valid_from IsNot Nothing) Then
                    txtInsurance_valid_from.Value = obj.Insurance_valid_from
                    txtInsurance_valid_from.Checked = True

                End If
                If obj.Insurance_valid_Till IsNot Nothing Then
                    txtInsurance_valid_till.Value = obj.Insurance_valid_Till
                    txtInsurance_valid_till.Checked = True
                End If
                If obj.Pollutionchk_valid_from IsNot Nothing Then
                    txtPollution_valid_from.Value = obj.Pollutionchk_valid_from
                    txtPollution_valid_from.Checked = True
                End If
                If obj.Pollutionchk_valid_Till IsNot Nothing Then
                    txtPollution_valid_till.Value = obj.Pollutionchk_valid_Till
                    txtPollution_valid_till.Checked = True
                End If
                If obj.RoadTax_valid_from IsNot Nothing Then
                    txtRoad_tax_valid_from.Value = obj.RoadTax_valid_from
                    txtRoad_tax_valid_from.Checked = True
                End If
                If obj.RoadTax_valid_Till IsNot Nothing Then
                    txtRoad_tax_valid_till.Value = obj.RoadTax_valid_Till
                    txtRoad_tax_valid_till.Checked = True
                End If
                rtxtlocation.Text = obj.Location
                rtxtengineno.Text = obj.EngineNo
                rtxtNumber.Text = obj.Number
                rtxtRegistredOn.Text = obj.Registered_On
                rtxtVehicle_registration_No.Text = obj.Vehicle_Reg_no
                rtxtvehicleName.Text = obj.VehicleName
                rtxtvehiclebrand.Text = obj.VehicleBrand
                rtxtvehicleNo.Text = obj.Vehicle_No
                TxtCrateCapacity.Value = obj.CrateCapacity

                txtchrg.Text = obj.chagrshift
                txtavgkm.Text = obj.avgrate
                txtdiesel.Text = obj.dieselrate

                cmbRentalType.Text = clsCommon.myCstr(obj.RentalType)
                txtRentalAmt.Text = clsCommon.myCdbl(obj.RentalAmount)

                cmbLtrKG.Text = obj.Rate_Type
                txt_ltr.Text = obj.Price_Ltr_KG

                txt_km.Text = obj.pricekm
                If clsCommon.CompairString(obj.status, "Day/Diesel") = CompairStringResult.Equal Then
                    rbtndiesel.IsChecked = True
                ElseIf clsCommon.CompairString(obj.status, "Rate/K.M") = CompairStringResult.Equal Then
                    rbtnratekm.IsChecked = True
                ElseIf clsCommon.CompairString(obj.status, "Rate/Ltr") = CompairStringResult.Equal Then
                    rbtnrateltr.IsChecked = True
                ElseIf clsCommon.CompairString(obj.status, "Rental") = CompairStringResult.Equal Then
                    rbtnrental.IsChecked = True
                ElseIf clsCommon.CompairString(obj.status, "KM_Range") = CompairStringResult.Equal Then
                    rbtKmrange.IsChecked = True
                End If
                If rbtKmrange.IsChecked Then
                    Dim arrObjSlab As List(Of clsSlabRangeDetail) = clsSlabRangeDetail.getData(Me.Form_ID, obj.VehicleCode)
                    If arrObjSlab IsNot Nothing AndAlso arrObjSlab.Count > 0 Then
                        gv.Rows.Clear()
                        For i As Integer = 0 To arrObjSlab.Count - 1
                            gv.Rows.AddNew()
                            gv.Rows(i).Cells(colSlabUpto).Value = arrObjSlab.Item(i).Slab_Upto
                            gv.Rows(i).Cells(colSlabRate).Value = arrObjSlab.Item(i).Slab_Rate
                        Next
                    End If
                    chkIsAdditional.Checked = obj.Is_Additional
                End If
                txtOneColumnCrate.Value = obj.Column_Crate
            Else
                funReset()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub

    Private Sub fndTransporter__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndTransporter._MYValidating
        If isButtonClicked Then
            Dim qry As String = "select Transport_Id as [Transport Id],Transporter_Name as [Transporter Name] from TSPL_TRANSPORT_MASTER"
            fndTransporter.Value = clsCommon.ShowSelectForm("RoutMastrCodFND", qry, "Transport Id", "", fndTransporter.Value, "", isButtonClicked)

        End If
        fndTransporter_Leave()


    End Sub


    Private Sub fndVehicle_id_KeyPress1(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles fndVehicle_id.KeyPress
        If Asc(e.KeyChar) = Keys.Space Then
            e.Handled = False
            MsgBox("Space")
        End If
    End Sub


    Private Sub rbtndiesel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtndiesel.Click
        rbtndiesel.IsChecked = False
        rbtnratekm.IsChecked = False
        rbtnrateltr.IsChecked = False
        rbtnrental.IsChecked = False
        rbtKmrange.Checked = False
    End Sub

    Private Sub rbtnrental_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnrental.ToggleStateChanged

        If rbtnrental.Checked = True Then
            cmbRentalType.Enabled = True

            cmbRentalType.MendatroryField = True
            txtRentalAmt.Enabled = True

            txtRentalAmt.MendatroryField = True

        Else
            cmbRentalType.Enabled = False
            cmbRentalType.Text = ""
            cmbRentalType.MendatroryField = False
            txtRentalAmt.Enabled = False
            txtRentalAmt.Text = Nothing
            txtRentalAmt.MendatroryField = False


        End If
    End Sub

    Private Sub rbtnrateltr_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnrateltr.ToggleStateChanged


        If rbtnrateltr.Checked = True Then
            cmbLtrKG.Enabled = True

            cmbLtrKG.MendatroryField = True
            txt_ltr.Enabled = True

            txt_ltr.MendatroryField = True

        Else
            cmbLtrKG.Enabled = False
            cmbLtrKG.Text = ""
            cmbLtrKG.MendatroryField = False
            txt_ltr.Enabled = False
            txt_ltr.Text = Nothing
            txt_ltr.MendatroryField = False


        End If
    End Sub

    Private Sub rbtnratekm_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnratekm.ToggleStateChanged
        If rbtnratekm.Checked = True Then
            txt_km.Enabled = True
            txt_km.MendatroryField = True
        Else
            txt_km.Enabled = False
            txt_km.MendatroryField = False
            txt_km.Text = ""
        End If
    End Sub

    Private Sub rbtKmrange_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtKmrange.ToggleStateChanged
        If rbtKmrange.Checked = True Then

            gv.Enabled = True
        Else
            gv.Enabled = False
        End If
    End Sub

    Private Sub rbtnrental_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnrental.Click
        rbtndiesel.IsChecked = False
        rbtnratekm.IsChecked = False
        rbtnrateltr.IsChecked = False
        rbtnrental.IsChecked = False
        rbtKmrange.Checked = False
    End Sub

    Private Sub rbtnrateltr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnrateltr.Click
        rbtndiesel.IsChecked = False
        rbtnratekm.IsChecked = False
        rbtnrateltr.IsChecked = False
        rbtnrental.IsChecked = False
        rbtKmrange.Checked = False
    End Sub

    Private Sub rbtnratekm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnratekm.Click
        rbtndiesel.IsChecked = False
        rbtnratekm.IsChecked = False
        rbtnrateltr.IsChecked = False
        rbtnrental.IsChecked = False
        rbtKmrange.Checked = False
    End Sub

    Private Sub rbtKmrange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtKmrange.Click
        rbtndiesel.IsChecked = False
        rbtnratekm.IsChecked = False
        rbtnrateltr.IsChecked = False
        rbtnrental.IsChecked = False
        rbtKmrange.Checked = False
    End Sub

    Private Sub rbtndiesel_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtndiesel.ToggleStateChanged
        If rbtndiesel.IsChecked = True Then
            txtchrg.Enabled = True
            txtavgkm.Enabled = True
            txtdiesel.Enabled = True
            txtchrg.MendatroryField = True
            txtavgkm.MendatroryField = True
            txtdiesel.MendatroryField = True
        Else
            txtchrg.Text = ""
            txtavgkm.Text = ""
            txtdiesel.Text = ""
            txtchrg.Enabled = False
            txtavgkm.Enabled = False
            txtdiesel.Enabled = False
            txtchrg.MendatroryField = False
            txtavgkm.MendatroryField = False
            txtdiesel.MendatroryField = False
        End If
    End Sub

    Private Sub gv_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv.UserDeletingRow
        If Not myMessages.deleteConfirm() Then
            e.Cancel = True
        End If
    End Sub
    Sub loadBlankgv()
        Try
            gv.Rows.Clear()
            gv.Columns.Clear()
            Dim repoDeciCol As GridViewDecimalColumn
            repoDeciCol = New GridViewDecimalColumn()
            repoDeciCol.Name = colSlabUpto
            repoDeciCol.Width = 120
            repoDeciCol.DecimalPlaces = 0
            repoDeciCol.Minimum = 1
            repoDeciCol.HeaderText = "Slab Upto"
            gv.MasterTemplate.Columns.Add(repoDeciCol)

            repoDeciCol = New GridViewDecimalColumn()
            repoDeciCol.Name = colSlabRate
            repoDeciCol.Width = 200
            repoDeciCol.DecimalPlaces = 2
            repoDeciCol.Minimum = 1
            repoDeciCol.HeaderText = "Slab Rate"
            gv.MasterTemplate.Columns.Add(repoDeciCol)

            gv.AllowAddNewRow = True
            gv.AllowEditRow = True
            gv.AllowDeleteRow = True
            gv.AllowRowResize = False
            gv.AllowRowReorder = False
            gv.AllowColumnResize = True
            gv.AllowColumnChooser = False
            gv.AllowAutoSizeColumns = False
            gv.ShowGroupPanel = False
            gv.AddNewRowPosition = SystemRowPosition.Bottom
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try


    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(fndVehicle_id.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select Vehicle Code")
                Exit Sub
            End If

            clsERPFuncationalityOLD.ShowHistoryData(fndVehicle_id.Value, "Vehicle_Id", "TSPL_Vehicle_MASTER")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub fndemployee__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndemployee._MYValidating
        Dim qry As String = "select TSPL_EMPLOYEE_MASTER.EMP_CODE as [Code],TSPL_EMPLOYEE_MASTER.EMP_Name as Name from TSPL_EMPLOYEE_MASTER"
        fndemployee.Value = clsCommon.ShowSelectForm("FNDemployee", qry, "Code", "", fndemployee.Value, "", isButtonClicked)
    End Sub
End Class
