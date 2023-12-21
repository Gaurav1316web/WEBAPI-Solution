Imports common
Imports System.Data.SqlClient
Imports System.IO

Public Class FrmMccMilkTransferPrice
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Public avgCost As Double = 0
    Public totQty As Double = 0

#End Region


#Region "User Defined Functions and Subroutines"

    Public Sub New()
        InitializeComponent()
    End Sub
#End Region

    Private Sub FrmMccMilkTransferPrice_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Reset()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+P for Post")

    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.MccMilkTransferPrice)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag


    End Sub
    Sub Reset()
        fndMccCode.Enabled = True
        btnGo.Visible = True
        fndPriceCode.Value = ""
        fndPriceCode.MyReadOnly = False
        Dim dt As Date = clsCommon.GETSERVERDATE(Nothing, "dd/MMM/yyyy")
        dtpPriceDate.Value = dt
        dtpFromCost.Value = dt
        dtpToCost.Value = dt
        dtpFromPrice.Value = dt
        dtpToPrice.Value = dt
        lblPending.Status = ERPTransactionStatus.Pending
        fndMccCode.Value = ""
        lblMccDesc.Text = ""
        txtMilkCost.Text = "0"
        txtPrimaryTransporterCost.Text = "0"
        txtChillingPrice.Text = "0"
        txtMccRent.Text = "0"
        txtVspCharge.Text = "0"
        txtManPowerCost.Text = "0"
        txtAdminCost.Text = "0"
        txtProcurementCost.Text = "0"
        txtSecondaryTransporterCost.Text = "0"
        txtHeadCostPer.Text = "0"
        txtHeadCost.Text = "0"
        txtTotalCost.Text = "0"
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = False
        btnPost.Enabled = False

    End Sub

    Private Sub frmMccMilkTransferPrice_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.N Then
            Reset()
        ElseIf e.Alt And e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            btnSave.PerformClick()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            btnClose.PerformClick()
        ElseIf e.Alt And e.KeyCode = Keys.P AndAlso btnPost.Enabled AndAlso MyBase.isPostFlag Then
            btnPost.PerformClick()
        End If
    End Sub


    Sub SaveData()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If AllowToSave(trans) Then
                Dim obj As New clsMccMilkTransferPrice
                obj.Price_Code = fndPriceCode.Value
                obj.Price_Date = dtpPriceDate.Value
                obj.MCC_Code = clsCommon.myCstr(fndMccCode.Value)
                obj.Mcc_Name = clsCommon.myCstr(lblMccDesc.Text)
                obj.Cost_Calc_From_Date = clsCommon.myCstr(clsCommon.GetPrintDate(dtpFromCost.Value, "dd/MMM/yyyy"))
                obj.Cost_Calc_To_Date = clsCommon.myCstr(clsCommon.GetPrintDate(dtpToCost.Value, "dd/MMM/yyyy"))
                obj.Transfer_Price_From_Date = clsCommon.myCstr(clsCommon.GetPrintDate(dtpFromPrice.Value, "dd/MMM/yyyy"))
                obj.Transfer_Price_To_Date = clsCommon.myCstr(clsCommon.GetPrintDate(dtpToPrice.Value, "dd/MMM/yyyy"))

                obj.Milk_Cost = clsCommon.myCdbl(txtMilkCost.Text)
                obj.Primary_Transporter_Cost = clsCommon.myCdbl(txtPrimaryTransporterCost.Text)
                obj.Chilling_Charge = clsCommon.myCdbl(txtChillingPrice.Text)
                obj.Mcc_Rent = clsCommon.myCdbl(txtMccRent.Text)
                obj.VSP_Charge = clsCommon.myCdbl(txtVspCharge.Text)
                obj.Manpower_Cost = clsCommon.myCdbl(txtManPowerCost.Text)
                obj.Admin_Cost = clsCommon.myCdbl(txtAdminCost.Text)
                obj.Procurement_Cost = clsCommon.myCdbl(txtProcurementCost.Text)
                obj.Secondary_Transporter_Cost = clsCommon.myCdbl(txtSecondaryTransporterCost.Text)
                obj.Head_Cost_Per = clsCommon.myCdbl(txtHeadCostPer.Text)
                obj.Head_Cost = clsCommon.myCdbl(txtHeadCost.Text)
                obj.Total_Cost = clsCommon.myCdbl(txtTotalCost.Text)
                obj.isPosted = 0
                Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(Price_Code) from TSPL_MCC_MILK_TRANSFER_PRICE where Price_Code='" + obj.Price_Code + "'", trans)
                If (qry = 0) Then
                    isNewEntry = True
                Else
                    isNewEntry = False
                End If
                If (clsMccMilkTransferPrice.SaveData(obj, isNewEntry, trans)) Then
                    trans.Commit()
                    If isNewEntry Then
                        clsCommon.MyMessageBoxShow(Me, "Data saved Successfully", Me.Text)
                        LoadData(obj.Price_Code, NavigatorType.Current)
                    Else
                        clsCommon.MyMessageBoxShow(Me, "Data Updated Successfully", Me.Text)
                        LoadData(obj.Price_Code, NavigatorType.Current)
                    End If
                End If
            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Function AllowToSave(ByVal trans As SqlTransaction) As Boolean
        Try
            ' KUNAL > TICKET : BM00000009575 =====
            If AllowFutureDateTransaction(dtpPriceDate.Value, trans) = False Then
                dtpPriceDate.Focus()
                Return False
            End If

            If dtpFromCost.Value > dtpToCost.Value Then
                Throw New Exception("'Cost Calculate From Date' can not Larger then 'Cost Calculate To Date' ")
            Else
                Return True
            End If

            If dtpFromPrice.Value > dtpToPrice.Value Then
                Throw New Exception("'Transfer Price From Date' can not Larger then 'Transfer Price To Date' ")
            Else
                Return True
            End If

            If clsCommon.myLen(fndMccCode.Value) <= 0 Then
                Throw New Exception("MCC Code Can not be Left Blank")
            Else
                Return True
            End If

            'If clsCommon.myCdbl(txtMilkCost.Text) < 0 Then
            '    Throw New Exception("Milk Cost Can not be Negative")
            'End If

            'If clsCommon.myCdbl(txtMilkCost.Text) = 0 Then
            '    Throw New Exception("Milk Cost Can not be 0 or Blank")
            'End If

            'If clsCommon.myCdbl(txtPrimaryTransporterCost.Text) < 0 Then
            '    Throw New Exception("Primary Transporter Cost Can not be Negative")
            'End If

            'If clsCommon.myCdbl(txtPrimaryTransporterCost.Text) = 0 Then
            '    Throw New Exception("Primary Transporter Cost Can not be 0 or Blank")
            'End If
            'If clsMccMaster.isMccChillingBasis(fndMccCode.Value) Then
            '    If clsCommon.myCdbl(txtChillingPrice.Text) < 0 Then
            '        Throw New Exception("Chilling Charge Can not be Negative")
            '    End If
            'End If
            'If clsMccMaster.isMccChillingBasis(fndMccCode.Value) Then
            '    If clsCommon.myCdbl(txtChillingPrice.Text) = 0 Then
            '        Throw New Exception("Chilling Charge Can not be 0 or Blank")
            '    End If
            'End If
            'If clsMccMaster.isMccOnRent(fndMccCode.Value) Then
            '    If clsCommon.myCdbl(txtMccRent.Text) < 0 Then
            '        Throw New Exception("MCC Rent Can not be Negative")
            '    End If
            'End If
            'If clsMccMaster.isMccOnRent(fndMccCode.Value) Then
            '    If clsCommon.myCdbl(txtMccRent.Text) = 0 Then
            '        Throw New Exception("MCC Rent Can not be 0 or Blank")
            '    End If
            'End If


            'If clsCommon.myCdbl(txtVspCharge.Text) < 0 Then
            '    Throw New Exception("VSP Charge Can not be Negative")
            'End If

            'If clsCommon.myCdbl(txtVspCharge.Text) = 0 Then
            '    Throw New Exception("VSP Charge Can not be 0 or Blank")
            'End If

            'If clsCommon.myCdbl(txtManPowerCost.Text) < 0 Then
            '    Throw New Exception("Manpower cost Can not be Negative")
            'End If

            'If clsCommon.myCdbl(txtManPowerCost.Text) = 0 Then
            '    Throw New Exception("Man Power Can not be 0 or Blank")
            'End If

            'If clsCommon.myCdbl(txtAdminCost.Text) < 0 Then
            '    Throw New Exception("Admin Cost Can not be Negative")
            'End If

            'If clsCommon.myCdbl(txtAdminCost.Text) = 0 Then
            '    Throw New Exception("Admin Cost Can not be 0 or Blank")
            'End If

            'If clsCommon.myCdbl(txtProcurementCost.Text) < 0 Then
            '    Throw New Exception("Procurement Cost Can not be Negative")
            'End If

            'If clsCommon.myCdbl(txtProcurementCost.Text) = 0 Then
            '    Throw New Exception("Procurement Cost Can not be 0 or Blank")
            'End If

            'If clsCommon.myCdbl(txtSecondaryTransporterCost.Text) < 0 Then
            '    Throw New Exception("Secondary Transporter Cost Can not be Negative")
            'End If

            'If clsCommon.myCdbl(txtSecondaryTransporterCost.Text) = 0 Then
            '    Throw New Exception("Secondary Transporter Cost Can not be 0 or Blank")
            'End If

            'If clsCommon.myCdbl(txtHeadCostPer.Text) < 0 Then
            '    Throw New Exception("Head Cost Percent Can not be Negative")
            'End If

            'If clsCommon.myCdbl(txtHeadCostPer.Text) = 0 Then
            '    Throw New Exception("Head Cost Percent Can not be 0 or Blank")
            'End If

            'If clsCommon.myCdbl(txtHeadCost.Text) < 0 Then
            '    Throw New Exception("Head Cost  Can not be Negative")
            'End If

            'If clsCommon.myCdbl(txtHeadCost.Text) = 0 Then
            '    Throw New Exception("Head Cost Can not be 0 or Blank")
            'End If

            'If clsCommon.myCdbl(txtTotalCost.Text) < 0 Then
            '    Throw New Exception("Total Cost  Can not be Negative")
            'End If

            'If clsCommon.myCdbl(txtTotalCost.Text) = 0 Then
            '    Throw New Exception("Total Cost Can not be 0 or Blank")
            'End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try

    End Function

    Sub CalculateHeadCost()
        Dim totalHeadCost As Double = 0
        If clsCommon.myCdbl(txtHeadCostPer.Text) = 0 Then
            txtHeadCost.Text = "0"
        Else
            totalHeadCost = (clsCommon.myCdbl(txtMilkCost.Text) + clsCommon.myCdbl(txtPrimaryTransporterCost.Text) + clsCommon.myCdbl(txtChillingPrice.Text) + clsCommon.myCdbl(txtMccRent.Text) + clsCommon.myCdbl(txtVspCharge.Text) + clsCommon.myCdbl(txtManPowerCost.Text) + clsCommon.myCdbl(txtAdminCost.Text) + clsCommon.myCdbl(txtProcurementCost.Text) + clsCommon.myCdbl(txtSecondaryTransporterCost.Text)) * clsCommon.myCdbl(txtHeadCostPer.Text) / 100
        End If
        txtHeadCost.Text = Math.Round(totalHeadCost, 2)
    End Sub

    Sub CalculateTotalCost()
        Dim TotalCost As Double = 0
        TotalCost = (clsCommon.myCdbl(txtMilkCost.Text) + clsCommon.myCdbl(txtPrimaryTransporterCost.Text) + clsCommon.myCdbl(txtChillingPrice.Text) + clsCommon.myCdbl(txtMccRent.Text) + clsCommon.myCdbl(txtVspCharge.Text) + clsCommon.myCdbl(txtManPowerCost.Text) + clsCommon.myCdbl(txtAdminCost.Text) + clsCommon.myCdbl(txtProcurementCost.Text) + clsCommon.myCdbl(txtSecondaryTransporterCost.Text) + clsCommon.myCdbl(txtHeadCost.Text))
        txtTotalCost.Text = Math.Round(TotalCost, 2)
    End Sub

    Sub CalculateMilkCost()
        Dim totalWeight As Double = 0
        Dim milkCost As Double = 0
        Dim milkAverageCost As Double = 0
        totalWeight = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select SUM (TOTAL_WEIGHT ) as totalWeight from TSPL_MILK_RECEIPT_HEAD where  MCC_CODE='" & fndMccCode.Value & "' and convert(varchar,DOC_DATE,103) between '" & clsCommon.GetPrintDate(dtpFromCost.Value, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(dtpToCost.Value, "dd/MMM/yyyy") & "'"))
        totQty = totalWeight

        Dim strItemCode As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.MCCDefaultMilkItem, clsFixedParameterCode.MilkSetting, Nothing))
        If clsCommon.myLen(strItemCode) > 0 Then
            milkAverageCost = clsInventoryMovementNew.GetCost(EnumCostingMethod.Averege, strItemCode, fndMccCode.Value, totalWeight, dtpPriceDate.Value, dtpPriceDate.Value, False, Nothing)
        End If
        'txtMilkCost.Text = Math.Round(milkAverageCost * totQty, 2)
        If milkAverageCost > 0 And totQty > 0 Then
            avgCost = milkAverageCost / totQty
        Else
            avgCost = 0
        End If
        txtMilkCost.Text = Math.Round(milkAverageCost / totQty, 2)

    End Sub

    Sub CalculatePrimaryTransporterCost()
        Dim totalPrmryTrnsCost As Double = 0
        txtPrimaryTransporterCost.Text = Math.Round(totalPrmryTrnsCost, 2)
    End Sub

    Sub CalculateMccRent()
        Dim LeaseRate As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Lease_Rate   from tspl_mcc_master where MCC_Code='" & fndMccCode.Value & "'"))
        Dim LeaseRatePerShift As Double = 0
        If LeaseRate > 0 Then
            LeaseRatePerShift = (LeaseRate / 30) / 2
        End If
        Dim TotalNoOfShift As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select COUNT(shift) from TSPL_MILK_RECEIPT_HEAD where  MCC_CODE='" & fndMccCode.Value & "' and convert(varchar,DOC_DATE,103) between '" & clsCommon.GetPrintDate(dtpFromCost.Value, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(dtpToCost.Value, "dd/MMM/yyyy") & "'"))
        Dim totalWeight As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select SUM (TOTAL_WEIGHT ) as totalWeight from TSPL_MILK_RECEIPT_HEAD where  MCC_CODE='" & fndMccCode.Value & "' and convert(varchar,DOC_DATE,103) between '" & clsCommon.GetPrintDate(dtpFromCost.Value, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(dtpToCost.Value, "dd/MMM/yyyy") & "'"))
        Dim totalRent As Double = TotalNoOfShift * LeaseRatePerShift
        Dim totalLeaseCost As Double = 0
        If totalRent > 0 And totalWeight > 0 Then
            totalLeaseCost = totalRent / totalWeight
        End If
        txtMccRent.Text = Math.Round(totalLeaseCost, 2)

    End Sub

    Sub CalculateChillingCharge()
        Dim totalWeight As Double = 0
        Dim ChillingRate As Double = 0
        totalWeight = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select SUM (TOTAL_WEIGHT ) as totalWeight from TSPL_MILK_RECEIPT_HEAD where  MCC_CODE='" & fndMccCode.Value & "' and convert(varchar,DOC_DATE,103) between '" & clsCommon.GetPrintDate(dtpFromCost.Value, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(dtpToCost.Value, "dd/MMM/yyyy") & "'"))
        ChillingRate = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Chilling_Rate  from tspl_mcc_master where MCC_CODE='" & fndMccCode.Value & "'"))
        Dim chillingCost As Double = totalWeight * ChillingRate
        txtChillingPrice.Text = Math.Round(chillingCost, 2)
    End Sub

    Sub CalculateVspCharge()
        Dim strQry As String = " select VSP_CODE,sum(MILK_WEIGHT) as milk_weight,MAX(xx.service_charge_type ) as service_type,MAX(xx.Service_charges ) as service_charge  from TSPL_MILK_RECEIPT_DETAIL left outer join TSPL_MILK_RECEIPT_HEAD on TSPL_MILK_RECEIPT_HEAD.DOC_CODE=  TSPL_MILK_RECEIPT_DETAIL.DOC_CODE  left outer join (select case when isnull(Service_Charge_Type,'')='' then 0  when Service_Charge_Type='Rate/Kg' then 1 when Service_Charge_Type='Rate/Ltr' then 1 else 2 end as service_charge_type ,Service_charges,Vendor_Code   from TSPL_VENDOR_MASTER ) xx on TSPL_MILK_RECEIPT_DETAIL.VSP_CODE  =xx.Vendor_Code where  TSPL_MILK_RECEIPT_HEAD.MCC_CODE='" & fndMccCode.Value & "' and convert(varchar,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) between '" & clsCommon.GetPrintDate(dtpFromCost.Value, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(dtpToCost.Value, "dd/MMM/yyyy") & "' group by VSP_CODE "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry)
        Dim vspCost As Double = 0
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                If clsCommon.myCdbl(dt.Rows(i)("service_type")) = 1 Then
                    vspCost = vspCost + (clsCommon.myCdbl(dt.Rows(i)("service_charge")) * clsCommon.myCdbl(dt.Rows(i)("milk_weight")))
                ElseIf clsCommon.myCdbl(dt.Rows(i)("service_type")) = 2 Then
                    vspCost = vspCost + ((clsCommon.myCdbl(dt.Rows(i)("milk_weight")) * avgCost) * clsCommon.myCdbl(dt.Rows(i)("service_charge")) / 100)
                End If
            Next
            txtVspCharge.Text = Math.Round(vspCost, 2)
        End If
    End Sub
    Sub CalculateSecondaryTransporterCost()

    End Sub


    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As clsMccMilkTransferPrice = clsMccMilkTransferPrice.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            isNewEntry = False
            fndMccCode.Enabled = False
            fndPriceCode.Value = obj.Price_Code
            dtpPriceDate.Value = obj.Price_Date
            dtpFromCost.Value = obj.Cost_Calc_From_Date
            dtpToCost.Value = obj.Cost_Calc_To_Date
            dtpFromPrice.Value = obj.Transfer_Price_From_Date
            dtpToPrice.Value = obj.Transfer_Price_To_Date
            fndMccCode.Value = obj.MCC_Code
            lblMccDesc.Text = obj.Mcc_Name

            txtMilkCost.Text = obj.Milk_Cost
            txtPrimaryTransporterCost.Text = obj.Primary_Transporter_Cost

            txtChillingPrice.Text = obj.Chilling_Charge
            txtMccRent.Text = obj.Mcc_Rent

            txtVspCharge.Text = obj.VSP_Charge
            txtManPowerCost.Text = obj.Manpower_Cost
            txtAdminCost.Text = obj.Admin_Cost
            txtProcurementCost.Text = obj.Procurement_Cost
            txtSecondaryTransporterCost.Text = obj.Secondary_Transporter_Cost
            txtHeadCostPer.Text = obj.Head_Cost_Per
            txtHeadCost.Text = obj.Head_Cost
            txtTotalCost.Text = obj.Total_Cost
            fndPriceCode.MyReadOnly = True
            btnSave.Text = "Update"
            btnDelete.Enabled = True
            btnSave.Enabled = True
            If obj.isPosted = 1 Then
                btnSave.Enabled = False
                btnDelete.Enabled = False
                btnPost.Enabled = False
                lblPending.Status = ERPTransactionStatus.Approved
            Else
                btnSave.Enabled = True
                btnDelete.Enabled = True
                btnPost.Enabled = True
                lblPending.Status = ERPTransactionStatus.Pending
            End If
        Else
            Reset()
        End If
    End Sub
    Private Sub DeleteData()
        Try
            If (deleteConfirm()) Then
                If (clsMccMilkTransferPrice.DeleteData(fndPriceCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    Reset()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
        GC.Collect()
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub fndPriceCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndPriceCode._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_MCC_MILK_TRANSFER_PRICE where Price_Code='" + fndPriceCode.Value + "' and Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If check > 0 Then
                fndPriceCode.MyReadOnly = True
            Else
                fndPriceCode.MyReadOnly = False
            End If

            LoadData(fndPriceCode.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndPriceCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndPriceCode._MYValidating
        fndPriceCode.Value = clsMccMilkTransferPrice.getFinder("", fndPriceCode.Value, isButtonClicked)
        LoadData(fndPriceCode.Value, NavigatorType.Current)
    End Sub
    Private Sub fndVendor__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndMccCode._MYValidating
        Dim whrCls As String = String.Empty
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrCls = "  mcc_code in (" & objCommonVar.strCurrUserLocations & ") "
        End If
        fndMccCode.Value = clsMccMaster.getFinder(whrCls, fndMccCode.Value, isButtonClicked)
        lblMccDesc.Text = clsLocation.GetName(fndMccCode.Value, Nothing)
    End Sub

    'Private Sub txtMilkCost_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMilkCost.TextChanged, txtChillingPrice.TextChanged, txtPrimaryTransporterCost.TextChanged, txtMccRent.TextChanged, txtVspCharge.TextChanged, txtManPowerCost.TextChanged, txtAdminCost.TextChanged, txtProcurementCost.TextChanged, txtSecondaryTransporterCost.TextChanged, txtHeadCostPer.TextChanged
    '    CalculateHeadCost()
    'End Sub

    'Private Sub txtTotalCost_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMilkCost.TextChanged, txtChillingPrice.TextChanged, txtPrimaryTransporterCost.TextChanged, txtMccRent.TextChanged, txtVspCharge.TextChanged, txtManPowerCost.TextChanged, txtAdminCost.TextChanged, txtProcurementCost.TextChanged, txtSecondaryTransporterCost.TextChanged, txtHeadCost.TextChanged
    '    CalculateTotalCost()
    'End Sub

    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        Try
            If dtpFromCost.Value > dtpToCost.Value Then
                Throw New Exception("'Cost Calculate From Date' can not Larger then 'Cost Calculate To Date' ")
            End If

            If dtpFromPrice.Value > dtpToPrice.Value Then
                Throw New Exception("'Transfer Price From Date' can not Larger then 'Transfer Price To Date' ")
            End If

            If clsCommon.myLen(fndMccCode.Value) <= 0 Then
                Throw New Exception("MCC Code Can not be Left Blank")
            End If

            CalculateMilkCost()
            CalculateMccRent()
            CalculateChillingCharge()
            CalculatePrimaryTransporterCost()
            CalculateVspCharge()
            CalculateSecondaryTransporterCost()
            CalculateHeadCost()
            CalculateTotalCost()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    
End Class
