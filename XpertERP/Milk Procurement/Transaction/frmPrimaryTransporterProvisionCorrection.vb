Imports common
Imports System.IO
Imports System.Data.SqlClient

Public Class frmPrimaryTransporterProvisionCorrection
    Inherits FrmMainTranScreen
#Region "Variables"
    Const ReportID As String = "PTPCorr"
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Dim ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster As Boolean = False
#End Region
    Private Sub FrmSerializeItemIn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        MyBase.SetUserMgmt(clsUserMgtCode.PrimaryTransportProvisionCorrection)
        ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster, clsFixedParameterCode.ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster, Nothing)) > 0, True, False)
        txtMCCFromDate.Value = DateTime.Now
        txtMCCToDate.Value = txtMCCFromDate.Value
        LoadShiftFrom()
    End Sub

    Sub LoadShiftFrom()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Shift")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "B"
        dr("Shift") = "Both"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "M"
        dr("Shift") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "E"
        dr("Shift") = "Evening"
        dt.Rows.Add(dr)

        txtFromShift.DataSource = dt.Copy
        txtFromShift.ValueMember = "Code"

        txtFromShift.SelectedValue = "B"
    End Sub
    Private Sub TxtMultiSelectFinder8__My_Click(sender As Object, e As EventArgs) Handles TxtMultiSelectFinder8._My_Click
        Dim qry As String = "select MCC_Code,MCC_NAME from TSPL_MCC_MASTER"
        TxtMultiSelectFinder8.arrValueMember = clsCommon.ShowMultipleSelectForm("BulkMCC@Uti1", qry, "MCC_Code", "MCC_NAME", TxtMultiSelectFinder8.arrValueMember, TxtMultiSelectFinder8.arrDispalyMember)
    End Sub
    Private Sub BulkDelete_Click(sender As Object, e As EventArgs) Handles BulkDelete.Click
        If TxtMultiSelectFinder8.arrValueMember Is Nothing OrElse TxtMultiSelectFinder8.arrValueMember.Count < 0 Then
            TxtMultiSelectFinder8.Focus()
            clsCommon.MyMessageBoxShow("Please First select MCC")
        End If
        Dim qry As String = "select TSPL_PROVISION_ENTRY.Doc_No,TSPL_PROVISION_ENTRY.Doc_Date,TSPL_PROVISION_ENTRY.Loc_Code as MCCCode,TSPL_LOCATION_MASTER.Location_Desc as MCC,TSPL_PROVISION_ENTRY.Route_Code,TSPL_MCC_ROUTE_MASTER.Route_Name,TSPL_MILK_Shift_End_Route_DETAIL.VEHICLE_CODE,TSPL_PROVISION_ENTRY.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name, TSPL_PROVISION_ENTRY.Ref_Doc_No as ShiftEndNo " + Environment.NewLine +
        ",TSPL_MILK_Shift_End_Route_DETAIL.PK_Id,TSPL_MILK_Shift_End_Route_DETAIL.Actual_KM" + Environment.NewLine +
        " ,TSPL_Primary_Vehicle_Master.Vehicle,TSPL_MILK_Shift_End_Route_DETAIL.DieselRate AS DiselRate,TSPL_MILK_Shift_End_Route_DETAIL.AvgKmLtr AS AvgKmLtr,TSPL_PROVISION_ENTRY.FixedCharge,convert(decimal(18,2),TSPL_PROVISION_ENTRY.Amount) as ProvisoinAmount" + Environment.NewLine +
        "from TSPL_PROVISION_ENTRY " + Environment.NewLine +
        "left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code   = TSPL_PROVISION_ENTRY.Loc_Code " + Environment.NewLine +
        "left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_PROVISION_ENTRY.Vendor_Code " + Environment.NewLine +
        "left outer join TSPL_MILK_Shift_End_Route_DETAIL on TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE=TSPL_PROVISION_ENTRY.Ref_Doc_No And TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE=TSPL_PROVISION_ENTRY.Route_Code" + Environment.NewLine +
        "left outer join TSPL_MILK_Shift_End_HEAD on TSPL_MILK_Shift_End_HEAD.DOC_CODE=TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE" + Environment.NewLine +
        "left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_PROVISION_ENTRY.Route_Code " + Environment.NewLine +
        "left outer join TSPL_Primary_Vehicle_Master on TSPL_Primary_Vehicle_Master.vehicle_Code=TSPL_MILK_Shift_End_Route_DETAIL.VEHICLE_CODE " + Environment.NewLine +
        "where TSPL_PROVISION_ENTRY.isPosted ='1' and TSPL_PROVISION_ENTRY.Status ='No' and TSPL_LOCATION_MASTER.Location_Code  IN (" + clsCommon.GetMulcallString(TxtMultiSelectFinder8.arrValueMember) + ")  and convert(date,TSPL_PROVISION_ENTRY.DOC_DATE,103)>=convert(date,'" + clsCommon.GetPrintDate(txtMCCFromDate.Value, "dd/MMM/yyyy") + "',103) and convert(date,TSPL_PROVISION_ENTRY.DOC_DATE,103) <=convert(date,'" + clsCommon.GetPrintDate(txtMCCToDate.Value, "dd/MMM/yyyy") + "' ,103)  " + Environment.NewLine +
        "and TSPL_PROVISION_ENTRY.Vendor_Type='Primary Transporter' and TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE is not null	" + Environment.NewLine
        If Not clsCommon.CompairString(txtFromShift.SelectedValue, "B") = CompairStringResult.Equal Then
            qry += " and TSPL_MILK_Shift_End_HEAD.SHIFT='" + clsCommon.myCstr(txtFromShift.SelectedValue) + "'"
        End If
        qry += "order by TSPL_PROVISION_ENTRY.DOC_DATE"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        gv1.GroupDescriptors.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        gv1.ShowGroupPanel = False
        gv1.EnableFiltering = True
        gv1.AllowAddNewRow = False
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            gv1.DataSource = dt
            For ii As Integer = 0 To gv1.Columns.Count - 1
                gv1.Columns(ii).ReadOnly = True
                gv1.Columns(ii).Width = 100
            Next
            gv1.Columns("Doc_No").HeaderText = "Provision No"
            gv1.Columns("Doc_No").Width = 130
            gv1.Columns("Doc_Date").HeaderText = "Provision Date"
            gv1.Columns("MCCCode").HeaderText = "MCC Code"
            gv1.Columns("MCC").HeaderText = "MCC"
            gv1.Columns("Route_Code").HeaderText = "Route Code"
            gv1.Columns("Route_Code").Width = 200
            gv1.Columns("Route_Name").HeaderText = "Route"
            gv1.Columns("VEHICLE_CODE").HeaderText = "Vehicle No"
            gv1.Columns("VEHICLE_CODE").ReadOnly = False
            gv1.Columns("Vendor_Code").HeaderText = "Vendor Code"
            gv1.Columns("Vendor_Name").HeaderText = "Vendor"
            gv1.Columns("ShiftEndNo").HeaderText = "Shift End No"
            gv1.Columns("PK_Id").HeaderText = "PK_Id"
            gv1.Columns("PK_Id").IsVisible = False
            gv1.Columns("Actual_KM").HeaderText = "Actual KM"
            gv1.Columns("Actual_KM").ReadOnly = False
            gv1.Columns("Actual_KM").TextAlignment = System.Drawing.ContentAlignment.MiddleRight

            gv1.Columns("Vehicle").IsVisible = ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster


            gv1.Columns("DiselRate").HeaderText = "Disel Rate"
            gv1.Columns("DiselRate").ReadOnly = False
            gv1.Columns("DiselRate").Width = 80
            gv1.Columns("DiselRate").TextAlignment = System.Drawing.ContentAlignment.MiddleRight

            gv1.Columns("AvgKmLtr").HeaderText = "Avg"
            gv1.Columns("AvgKmLtr").ReadOnly = False
            gv1.Columns("AvgKmLtr").Width = 80
            gv1.Columns("AvgKmLtr").TextAlignment = System.Drawing.ContentAlignment.MiddleRight

            gv1.Columns("FixedCharge").HeaderText = "FixedCharge"
            gv1.Columns("FixedCharge").ReadOnly = False
            gv1.Columns("FixedCharge").Width = 80
            gv1.Columns("FixedCharge").TextAlignment = System.Drawing.ContentAlignment.MiddleRight

            gv1.Columns("ProvisoinAmount").HeaderText = "Provision Amount"
        End If
    End Sub
    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns("VEHICLE_CODE") Then
                        OpenVehicleFinder(False)
                    ElseIf e.Column Is gv1.Columns("DiselRate") Then
                        Dim NewRate As Decimal = clsCommon.myCdbl(gv1.CurrentRow.Cells("DiselRate").Value)
                        If NewRate > 0 Then
                            If clsCommon.MyMessageBoxShow(Me, "Apply this rate to all down Rows", Me.Text, MessageBoxButtons.YesNo) = DialogResult.Yes Then
                                For ii As Integer = gv1.CurrentRow.Index + 1 To gv1.Rows.Count - 1
                                    gv1.Rows(ii).Cells("DiselRate").Value = NewRate
                                Next
                            End If
                        End If
                    ElseIf e.Column Is gv1.Columns("AvgKmLtr") Then
                        Dim NewAvgKmLtr As Decimal = clsCommon.myCdbl(gv1.CurrentRow.Cells("AvgKmLtr").Value)
                        If NewAvgKmLtr > 0 Then
                            If clsCommon.MyMessageBoxShow(Me, "Apply this Avg to all down Rows", Me.Text, MessageBoxButtons.YesNo) = DialogResult.Yes Then
                                For ii As Integer = gv1.CurrentRow.Index + 1 To gv1.Rows.Count - 1
                                    gv1.Rows(ii).Cells("AvgKmLtr").Value = NewAvgKmLtr
                                Next
                            End If
                        End If
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub OpenVehicleFinder(ByVal isButtonClick As Boolean)
        Dim qry As String = "select TSPL_Primary_Vehicle_Master.Vehicle_Code,TSPL_Primary_Vehicle_Master.Description,TSPL_Primary_Vehicle_Master.Vendor_Code ,tspl_vendor_master.Vendor_Name" + Environment.NewLine
        If ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster = true then
         qry += " ,TSPL_Primary_Vehicle_Master.Vehicle "
        End If
        qry += " From TSPL_Primary_Vehicle_Master " + Environment.NewLine +
        "Left outer join tspl_vendor_master on tspl_vendor_master.Vendor_Code=TSPL_Primary_Vehicle_Master.Vendor_Code"
        Dim whrCls As String = ""
        gv1.CurrentRow.Cells("VEHICLE_CODE").Value = clsCommon.ShowSelectForm("ptrvpcor", qry, "Vehicle_Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells("Vehicle_Code").Value), "", isButtonClick)
        gv1.CurrentRow.Cells("Vehicle").Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select vehicle from TSPL_Primary_Vehicle_Master where vehicle_Code='" & clsCommon.myCstr(gv1.CurrentRow.Cells("VEHICLE_CODE").Value) & "'"))
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry + " Where Vehicle_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells("Vehicle_Code").Value) + "'")
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            gv1.CurrentRow.Cells("Vendor_Code").Value = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
            gv1.CurrentRow.Cells("Vendor_Name").Value = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
        Else
            gv1.CurrentRow.Cells("Vendor_Code").Value = ""
            gv1.CurrentRow.Cells("Vendor_Name").Value = ""
        End If
    End Sub
    Public Function SaveData() As Boolean
        'Dim TempAvgKmLtr As Double = 0
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            Dim StrMsg As String = ""
            For jj As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myLen(gv1.Rows(jj).Cells("PK_Id").Value) > 0 AndAlso clsCommon.myLen(gv1.Rows(jj).Cells("VEHICLE_CODE").Value) > 0 And clsCommon.myCdbl(gv1.Rows(jj).Cells("DiselRate").Value) > 0 And clsCommon.myCdbl(gv1.Rows(jj).Cells("Actual_KM").Value) > 0 Then
                    Dim qry As String = "select  TSPL_Primary_Vehicle_Master.Status,TSPL_Primary_Vehicle_Master.Shift_Charges,TSPL_Primary_Vehicle_Master.Avg_Km_Ltr,TSPL_Primary_Vehicle_Master.Diesel_Rate,TSPL_Primary_Vehicle_Master.Price_KM, TSPL_Primary_Vehicle_Master.Price_Ltr_KG,TSPL_Primary_Vehicle_Master.Rate_Type,TSPL_Primary_Vehicle_Master.Rental_Type,TSPL_Primary_Vehicle_Master.Rental_Amount,Is_Additional,TSPL_Primary_Vehicle_Master.Two_Way " + Environment.NewLine +
                        " from TSPL_Primary_Vehicle_Master " + Environment.NewLine +
                        " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_Primary_Vehicle_Master.Vendor_Code " + Environment.NewLine +
                        " where Vehicle_Code='" + clsCommon.myCstr(gv1.Rows(jj).Cells("VEHICLE_CODE").Value) + "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    Dim obj As clsMilkShiftEndMCC_Route_Detail = clsMilkShiftEndMCC_Route_Detail.GetData(clsCommon.myCstr(gv1.Rows(jj).Cells("ShiftEndNo").Value), clsCommon.myCstr(gv1.Rows(jj).Cells("PK_Id").Value), trans)
                    obj.Status = clsCommon.myCstr(dt.Rows(0)("Status"))



                    If clsCommon.myCdbl(gv1.Rows(jj).Cells("AvgKmLtr").Value) = 0 AndAlso ((clsCommon.CompairString(obj.Status, "Day/Diesel") = CompairStringResult.Equal) OrElse (clsCommon.CompairString(obj.Status, "Rental/Diesel") = CompairStringResult.Equal)) Then
                        If clsCommon.myLen(StrMsg) <= 0 Then
                            StrMsg = "Enter Avg for (Day/Diesel OR Rental/Diesel)" + Environment.NewLine
                        End If
                        StrMsg = StrMsg + "Line No - " + clsCommon.myCstr(jj + 1) + Environment.NewLine
                    End If
                End If
            Next
            If clsCommon.myLen(StrMsg) > 0 Then
                Throw New Exception(StrMsg)
            End If



            Dim SettingStdQty As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterCode.PTMRatePerLtrKGOnStdQty, clsFixedParameterType.PTMRatePerLtrKGOnStdQty, trans)) > 0, True, False)
            For jj As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myLen(gv1.Rows(jj).Cells("PK_Id").Value) > 0 AndAlso clsCommon.myLen(gv1.Rows(jj).Cells("VEHICLE_CODE").Value) > 0 And clsCommon.myCdbl(gv1.Rows(jj).Cells("DiselRate").Value) > 0 And clsCommon.myCdbl(gv1.Rows(jj).Cells("Actual_KM").Value) > 0 Then 'And clsCommon.myCdbl(gv1.Rows(jj).Cells("AvgKmLtr").Value) > 0
                    'TempAvgKmLtr = 0
                    Dim qry As String = "Select  TSPL_Primary_Vehicle_Master.Status, TSPL_Primary_Vehicle_Master.Shift_Charges, TSPL_Primary_Vehicle_Master.Avg_Km_Ltr, TSPL_Primary_Vehicle_Master.Diesel_Rate, TSPL_Primary_Vehicle_Master.Price_KM, TSPL_Primary_Vehicle_Master.Price_Ltr_KG, TSPL_Primary_Vehicle_Master.Rate_Type, TSPL_Primary_Vehicle_Master.Rental_Type, TSPL_Primary_Vehicle_Master.Rental_Amount, Is_Additional, TSPL_Primary_Vehicle_Master.Two_Way " + Environment.NewLine +
                    " from TSPL_Primary_Vehicle_Master " + Environment.NewLine +
                    " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_Primary_Vehicle_Master.Vendor_Code " + Environment.NewLine +
                    " where Vehicle_Code='" + clsCommon.myCstr(gv1.Rows(jj).Cells("VEHICLE_CODE").Value) + "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    Dim obj As clsMilkShiftEndMCC_Route_Detail = clsMilkShiftEndMCC_Route_Detail.GetData(clsCommon.myCstr(gv1.Rows(jj).Cells("ShiftEndNo").Value), clsCommon.myCstr(gv1.Rows(jj).Cells("PK_Id").Value), trans)
                    obj.Actual_KM = clsCommon.myCdbl(gv1.Rows(jj).Cells("Actual_KM").Value)
                    obj.Status = clsCommon.myCstr(dt.Rows(0)("Status"))

                    Dim dclFixedCharges As Decimal = clsCommon.myCDecimal(dt.Rows(0)("Rental_Amount"))
                    If clsCommon.myCDecimal(gv1.Rows(jj).Cells("FixedCharge").Value) > 0 Then
                        dclFixedCharges = clsCommon.myCDecimal(gv1.Rows(jj).Cells("FixedCharge").Value)
                    End If
                    Dim dclFixedAmt As Decimal = 0
                    If clsCommon.CompairString(obj.Status, "Day/Diesel") = CompairStringResult.Equal Then
                        obj.Amount = Math.Round(clsCommon.myCdbl(dt.Rows(0)("Shift_Charges")) + ((obj.Actual_KM * clsCommon.myCdbl(gv1.Rows(jj).Cells("DiselRate").Value)) / clsCommon.myCdbl(gv1.Rows(jj).Cells("AvgKmLtr").Value)), 2, MidpointRounding.ToEven)
                        'TempAvgKmLtr = clsCommon.myCdbl(dt.Rows(0)("Avg_Km_Ltr"))
                    ElseIf clsCommon.CompairString(obj.Status, "Rate/K.M") = CompairStringResult.Equal Then
                        obj.Amount = Math.Round(obj.Actual_KM * clsCommon.myCdbl(dt.Rows(0)("Price_KM")), 2, MidpointRounding.ToEven)
                    ElseIf clsCommon.CompairString(obj.Status, "Rate/Ltr") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Rate_Type")), "LTR") = CompairStringResult.Equal Then
                            Dim convFactor As Double = clsWeightConversionInfo.GetWeightConverionFactorMilkType(objCommonVar.DefaultMilkItemCode, "KG", "LTR", trans)
                            obj.Amount = Math.Round((IIf(SettingStdQty, obj.Std_Qty, obj.Actual_Weight) * clsCommon.myCdbl(dt.Rows(0)("Price_Ltr_KG")) * convFactor), 2, MidpointRounding.ToEven)
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Rate_Type")), "KG") = CompairStringResult.Equal Then
                            obj.Amount = Math.Round(IIf(SettingStdQty, obj.Std_Qty, obj.Actual_Weight) * clsCommon.myCdbl(dt.Rows(0)("Price_Ltr_KG")), 2, MidpointRounding.ToEven)
                        Else
                            Throw New Exception("Wrong Rate Type of " + obj.Status + " and vehicle no " + obj.Vehicle_Code)
                        End If
                    ElseIf clsCommon.CompairString(obj.Status, "Rental") = CompairStringResult.Equal Then
                        Dim Days As Integer = 0
                        If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Rental_Type")), "Year") = CompairStringResult.Equal Then
                            Days = IIf(DateTime.IsLeapYear(obj.DOC_DATE.Year), 366, 365)
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Rental_Type")), "Month") = CompairStringResult.Equal Then
                            Days = System.DateTime.DaysInMonth(obj.DOC_DATE.Year, obj.DOC_DATE.Month)
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Rental_Type")), "Day") = CompairStringResult.Equal Then
                            Days = 1
                        Else
                            Throw New Exception("Wrong Rental Type of " + obj.Status + " and vehicle no " + obj.Vehicle_Code)
                        End If

                        dclFixedAmt = Math.Round(clsCommon.myCdbl(dclFixedCharges / (Days * 2)), 2, MidpointRounding.ToEven)
                        obj.Amount = dclFixedAmt
                    ElseIf clsCommon.CompairString(obj.Status, "Rental/Diesel") = CompairStringResult.Equal Then
                        dclFixedAmt = Math.Round(clsCommon.myCdbl(dclFixedCharges / (System.DateTime.DaysInMonth(obj.DOC_DATE.Year, obj.DOC_DATE.Month) * 2)), 2, MidpointRounding.ToEven)
                        obj.Amount = dclFixedAmt
                        obj.Amount += Math.Round(((obj.Actual_KM * clsCommon.myCdbl(gv1.Rows(jj).Cells("DiselRate").Value)) / clsCommon.myCdbl(gv1.Rows(jj).Cells("AvgKmLtr").Value)), 2, MidpointRounding.ToEven)
                        'TempAvgKmLtr = clsCommon.myCdbl(dt.Rows(0)("Avg_Km_Ltr"))
                    ElseIf clsCommon.CompairString(obj.Status, "KM_Range") = CompairStringResult.Equal Then
                        'Today do work
                        obj.Amount = 0
                        Dim dblRemainingKM As Double = obj.Actual_KM
                        Dim dtSlab As DataTable = clsDBFuncationality.GetDataTable("select Slab_Upto,Slab_Rate from tspl_slab_range_detail where Trans_ID='" + obj.Vehicle_Code + "' and Form_ID='PTV-MST' order by Slab_Upto desc", trans)
                        If dtSlab IsNot Nothing AndAlso dtSlab.Rows.Count > 0 Then
                            If dtSlab.Rows.Count = 1 Then
                                obj.Amount = Math.Round((clsCommon.myCdbl(dtSlab.Rows(0)("Slab_Rate")) * (obj.Actual_KM)), 2, MidpointRounding.ToEven)
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Is_Additional")), "T") = CompairStringResult.Equal Then
                                For ii As Integer = 0 To dtSlab.Rows.Count - 1
                                    Dim previousRange As Double = 0
                                    If (dtSlab.Rows.Count - (ii + 1)) > 0 Then
                                        previousRange = clsCommon.myCdbl(dtSlab.Rows(ii + 1)("Slab_Upto"))
                                    End If
                                    If dblRemainingKM >= clsCommon.myCdbl(dtSlab.Rows(ii)("Slab_Upto")) Then
                                        obj.Amount += (clsCommon.myCdbl(dtSlab.Rows(ii)("Slab_Rate")) * (dblRemainingKM))
                                        Exit For
                                    ElseIf dblRemainingKM > previousRange AndAlso dblRemainingKM <= clsCommon.myCdbl(dtSlab.Rows(ii)("Slab_Upto")) Then
                                        obj.Amount += (clsCommon.myCdbl(dtSlab.Rows(ii)("Slab_Rate")) * (dblRemainingKM - previousRange))
                                        dblRemainingKM = previousRange
                                    End If
                                Next
                                obj.Amount = Math.Round(obj.Amount, 2, MidpointRounding.ToEven)
                            Else
                                For Each drSlab As DataRow In dtSlab.Rows
                                    If obj.Actual_KM >= clsCommon.myCdbl(drSlab("Slab_Upto")) Then
                                        obj.Amount = Math.Round((clsCommon.myCdbl(drSlab("Slab_Rate")) * (obj.Actual_KM)), 2, MidpointRounding.ToEven)
                                        Exit For
                                    End If
                                Next
                            End If
                        End If
                    Else
                        Throw New Exception("Wrong method " + obj.Status + " for vehicle no " + obj.Vehicle_Code)
                    End If
                    If clsCommon.myCdbl(dt.Rows(0)("Two_Way")) = 1 Then
                        obj.Amount = obj.Amount * 2
                    End If
                    obj.Amount = Math.Round(obj.Amount, 2, MidpointRounding.ToEven)
                    obj.Rate_KM = IIf(obj.Actual_KM = 0, 0, Math.Round(obj.Amount / obj.Actual_KM, 3, MidpointRounding.AwayFromZero))


                    qry = "update  TSPL_MILK_RECEIPT_DETAIL set TSPL_MILK_RECEIPT_DETAIL.VEHICLE_CODE='" + clsCommon.myCstr(gv1.Rows(jj).Cells("VEHICLE_CODE").Value) + "' from (
select TSPL_MILK_RECEIPT_DETAIL.VEHICLE_CODE, TSPL_MILK_RECEIPT_DETAIL.DOC_CODE,TSPL_MILK_RECEIPT_DETAIL.PK_Id 
from TSPL_MILK_Shift_End_Route_DETAIL
inner join TSPL_MILK_Shift_End_HEAD on TSPL_MILK_Shift_End_HEAD.DOC_CODE= TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE
inner join TSPL_MILK_RECEIPT_HEAD on CONVERT(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103)=CONVERT(date, TSPL_MILK_Shift_End_HEAD.DOC_DATE,103) and TSPL_MILK_RECEIPT_HEAD.SHIFT= TSPL_MILK_Shift_End_HEAD.SHIFT and TSPL_MILK_RECEIPT_HEAD.MCC_CODE= TSPL_MILK_Shift_End_HEAD.MCC_CODE 
inner join TSPL_MILK_RECEIPT_DETAIL on TSPL_MILK_RECEIPT_DETAIL.DOC_CODE=TSPL_MILK_RECEIPT_HEAD.DOC_CODE and TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE=TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE
where  TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE='" + clsCommon.myCstr(gv1.Rows(jj).Cells("ShiftEndNo").Value) + "' and TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE='" + clsCommon.myCstr(gv1.Rows(jj).Cells("Route_Code").Value) + "'
)xx inner join TSPL_MILK_RECEIPT_DETAIL on TSPL_MILK_RECEIPT_DETAIL.DOC_CODE=xx.DOC_CODE and TSPL_MILK_RECEIPT_DETAIL.PK_Id=xx.PK_Id"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Status", obj.Status)
                    clsCommon.AddColumnsForChange(coll, "Rate", obj.Rate_KM)
                    clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                    clsCommon.AddColumnsForChange(coll, "Actual_KM", obj.Actual_KM)
                    clsCommon.AddColumnsForChange(coll, "VEHICLE_CODE", clsCommon.myCstr(gv1.Rows(jj).Cells("VEHICLE_CODE").Value))
                    clsCommon.AddColumnsForChange(coll, "DieselRate", clsCommon.myCdbl(gv1.Rows(jj).Cells("DiselRate").Value))
                    clsCommon.AddColumnsForChange(coll, "AvgKmLtr", clsCommon.myCdbl(gv1.Rows(jj).Cells("AvgKmLtr").Value)) 'TempAvgKmLtr
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_Shift_End_Route_DETAIL", OMInsertOrUpdate.Update, "DOC_CODE='" + clsCommon.myCstr(gv1.Rows(jj).Cells("ShiftEndNo").Value) + "' and PK_Id='" & clsCommon.myCstr(gv1.Rows(jj).Cells("PK_Id").Value) & "'", trans)


                    clsProvisionEntry.ReverseAndUnpost(clsCommon.myCstr(gv1.Rows(jj).Cells("Doc_No").Value), trans)
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Vendor_Code", clsCommon.myCstr(gv1.Rows(jj).Cells("Vendor_Code").Value))
                    clsCommon.AddColumnsForChange(coll, "Vendor_Desc", clsCommon.myCstr(gv1.Rows(jj).Cells("Vendor_Name").Value))
                    clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                    clsCommon.AddColumnsForChange(coll, "FixedAmount", dclFixedAmt)
                    clsCommon.AddColumnsForChange(coll, "FixedCharge", dclFixedCharges)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PROVISION_ENTRY", OMInsertOrUpdate.Update, "Doc_No='" + clsCommon.myCstr(gv1.Rows(jj).Cells("Doc_No").Value) + "'", trans)
                    clsProvisionEntry.PostData(clsCommon.myCstr(gv1.Rows(jj).Cells("Doc_No").Value), trans, False)
                End If
            Next
            trans.Commit()
            clsCommon.MyMessageBoxShow(Me, "Data saved Successfully", Me.Text)
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If SaveData() = True Then
            BulkDelete.PerformClick()
        End If
    End Sub
    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        If clsCommon.myLen(ReportID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub
    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
    End Sub
    Private Sub FrmSerializeItemIn_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.Escape Then
            CancelPressed()
        End If
    End Sub
    Sub CancelPressed()
        Me.Close()
    End Sub
    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CancelPressed()
    End Sub
    Private Sub RadMenuItem4_Click(sender As Object, e As EventArgs) Handles RadMenuItem4.Click
        Dim qry As String = "select 1 as 'S NO',null as 'VLC Uploader Code',null as 'Qty (Ltr)',null as 'FAT%',null as 'SNF%','' as [Milk Type(M/C/B)],'' as  [Reject Type],'' as [Reject Defaulter]"
        transportSql.ExporttoExcelWithoutFilter(qry, "", "", Me)
    End Sub
    Private Sub RadMenuItem6_Click(sender As Object, e As EventArgs) Handles RadMenuItem6.Click
        'Dim qry As String = "select SNo as 'S NO',TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as 'VLC Uploader Code',Milk_Weight as 'Qty (Ltr)',FAT as 'FAT%',SNF as 'SNF%',Dock_Collection_Milk_Type as [Milk Type(M/C/B)],Reject_Type as  [Reject Type],Reject_Defaulter as [Reject Defaulter]" + Environment.NewLine +
        '"from TSPL_MILK_SHIFT_UPLOADER_DETAIL" + Environment.NewLine +
        '"left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_SHIFT_UPLOADER_DETAIL.VLC_Code" + Environment.NewLine +
        '"where Document_No='" + txtDocNo.Value + "'"
        'transportSql.ExporttoExcelWithoutFilter(qry, "", "", Me)
    End Sub
    Private Sub RadMenuItem5_Click(sender As Object, e As EventArgs) Handles RadMenuItem5.Click
        Try
            '            If clsCommon.myLen(fndMCCCode.Value) <= 0 Then
            '                Throw New Exception("Please select MCC Code")
            '            End If
            '            Dim gv As New RadGridView()
            '            Me.Controls.Add(gv)
            '            Dim currentdate As Date = Date.Today
            '            If transportSql.importExcel(gv, "S NO", "VLC Uploader Code", "Qty (Ltr)", "FAT%", "SNF%", "Milk Type(M/C/B)", "Reject Type", "Reject Defaulter") Then
            '                Dim Arr As New List(Of clsMilkShiftUploaderDetail)
            '                Dim ii As Integer = 0

            '                Dim dtt As DataTable = TryCast(gv.DataSource, DataTable)
            '                dtt.Columns.Add("ErrorDesc", "".GetType())
            '                Try
            '                    Dim qry As String = ""
            '                    Dim ErrCount As Integer = 0
            '                    clsCommon.ProgressBarShow()
            '                    For ii = 0 To gv.RowCount - 1
            '                        If clsCommon.myLen(gv.Rows(ii).Cells("VLC Uploader Code").Value) > 0 Then
            '                            Dim objTr As New clsMilkShiftUploaderDetail()
            '                            objTr.SNo = ii + 1
            '                            objTr.Reject_Type = clsCommon.myCstr(gv.Rows(ii).Cells("Reject Type").Value)
            '                            If clsCommon.myLen(objTr.Reject_Type) > 0 Then
            '                                If clsCommon.myLen(objTr.Reject_Type) <= 0 Then
            '                                    dtt.Rows(ii)("ErrorDesc") = "Please enter Reject Type." & " at Line No :" & clsCommon.myCstr(ii + 2) & " "
            '                                    ErrCount = ErrCount + 1 : GoTo ExitLOOP
            '                                End If
            '                                qry = "select Code from TSPL_MILK_REJECT_TYPE where Code='" + objTr.Reject_Type + "'"
            '                                objTr.Reject_Type = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            '                                If clsCommon.myLen(objTr.Reject_Type) <= 0 Then
            '                                    dtt.Rows(ii)("ErrorDesc") = "Invalid Reject Type " + clsCommon.myCstr(gv.Rows(ii).Cells("Reject Type").Value) & " at Line No :" & clsCommon.myCstr(ii + 2) & " "
            '                                    ErrCount = ErrCount + 1 : GoTo ExitLOOP
            '                                End If

            '                                objTr.Reject_Defaulter = clsCommon.myCstr(gv.Rows(ii).Cells("Reject Defaulter").Value)
            '                                If clsCommon.myLen(objTr.Reject_Defaulter) <= 0 Then
            '                                    dtt.Rows(ii)("ErrorDesc") = "Please enter Reject Defaulter." & " at Line No :" & clsCommon.myCstr(ii + 2) & " "
            '                                    ErrCount = ErrCount + 1 : GoTo ExitLOOP
            '                                End If
            '                                If Not (clsCommon.CompairString(objTr.Reject_Defaulter, "VSP") = CompairStringResult.Equal OrElse clsCommon.CompairString(objTr.Reject_Defaulter, "Transporter") = CompairStringResult.Equal OrElse clsCommon.CompairString(objTr.Reject_Defaulter, "Company") = CompairStringResult.Equal) Then
            '                                    dtt.Rows(ii)("ErrorDesc") = "Invalid Reject Defaulter It should be [Company/Transporter/VSP]" & " at Line No :" & clsCommon.myCstr(ii + 2) & " "
            '                                    ErrCount = ErrCount + 1 : GoTo ExitLOOP
            '                                End If
            '                            End If
            '                            qry = "select VLC_Code from TSPL_VLC_MASTER_HEAD where MCC='" + fndMCCCode.Value + "' and VLC_Code_VLC_Uploader='" + clsCommon.myCstr(gv.Rows(ii).Cells("VLC Uploader Code").Value) + "'"
            '                            objTr.VLC_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            '                            If clsCommon.myLen(objTr.VLC_Code) <= 0 Then
            '                                dtt.Rows(ii)("ErrorDesc") = "Invalid VSP Uploader code " + clsCommon.myCstr(gv.Rows(ii).Cells("VLC Uploader Code").Value) & " at Line No :" & clsCommon.myCstr(ii + 2) & " "
            '                                ErrCount = ErrCount + 1 : GoTo ExitLOOP
            '                            End If

            '                            objTr.VLC_Uploader_Code = clsCommon.myCstr(gv.Rows(ii).Cells("VLC Uploader Code").Value)
            '                            objTr.Milk_Weight = clsCommon.myCdbl(gv.Rows(ii).Cells("Qty (Ltr)").Value)
            '                            objTr.No_Of_Cans = Math.Ceiling(objTr.Milk_Weight / MilkWeight_Setting)
            '                            objTr.FAT = Math.Round(clsCommon.myCdbl(gv.Rows(ii).Cells("FAT%").Value), 1, MidpointRounding.AwayFromZero)
            '                            If settMaxFATPerLimit > 0 Then
            '                                If objTr.FAT > settMaxFATPerLimit Then
            '                                    dtt.Rows(ii)("ErrorDesc") = "FAT % Can't be more than " + clsCommon.myCstr(settMaxFATPerLimit) & " at Line No :" & clsCommon.myCstr(ii + 2) & " "
            '                                    ErrCount = ErrCount + 1 : GoTo ExitLOOP
            '                                End If
            '                            End If
            '                            objTr.SNF = Math.Round(clsCommon.myCdbl(gv.Rows(ii).Cells(IIf(isPickCLRInsteadOfSNF, "CLR%", "SNF%")).Value), IIf(objCommonVar.MilkProcurementSNF2DecimalPlaces, 2, 1), MidpointRounding.AwayFromZero)
            '                            If settMaxReceiveSNFPer > 0 AndAlso objTr.SNF > settMaxReceiveSNFPer Then
            '                                objTr.SNF = settMaxReceiveSNFPer
            '                            End If
            '                            If settMaxSNFPerLimit > 0 Then
            '                                If objTr.SNF > settMaxSNFPerLimit Then
            '                                    dtt.Rows(ii)("ErrorDesc") = "SNF % Can't be more than " + clsCommon.myCstr(settMaxSNFPerLimit) & " at Line No :" & clsCommon.myCstr(ii + 2) & " "
            '                                    ErrCount = ErrCount + 1 : GoTo ExitLOOP
            '                                End If
            '                            End If
            '                            objTr.Dock_Collection_Milk_Type = clsCommon.myCstr(gv.Rows(ii).Cells("Milk Type(M/C/B)").Value).ToUpper()
            '                            If Not (clsCommon.CompairString(objTr.Dock_Collection_Milk_Type, "M") = CompairStringResult.Equal OrElse clsCommon.CompairString(objTr.Dock_Collection_Milk_Type, "C") = CompairStringResult.Equal OrElse clsCommon.CompairString(objTr.Dock_Collection_Milk_Type, "B") = CompairStringResult.Equal) Then
            '                                dtt.Rows(ii)("ErrorDesc") = "Milk Type can be M,B or C" & " at Line No :" & clsCommon.myCstr(ii + 2) & " "
            '                                ErrCount = ErrCount + 1 : GoTo ExitLOOP
            '                            End If
            '                            Arr.Add(objTr)
            '                        End If
            'ExitLOOP:

            '                    Next

            '                    dtt.DefaultView.RowFilter = "ErrorDesc<>''"
            '                    dtt = dtt.DefaultView.ToTable

            '                    If dtt.Rows.Count > 0 Then
            '                        clsCommon.ProgressBarHide()
            '                        common.clsCommon.MyMessageBoxShow("Error in " & dtt.Rows.Count & " Records.", Me.Text, MessageBoxButtons.OK)
            '                        Dim ff As New FrmFreeGrid
            '                        ff.ReportID = "UnImportedList"
            '                        ff.Text = "Record Could not Loaded"
            '                        ff.dt = dtt
            '                        ff.ShowDialog()
            '                        Exit Sub
            '                    End If


            '                    clsCommon.ProgressBarUpdate("Loading data in Grid.Please wait...")
            '                    AddRowsByImportAfterValidation(Arr, False)
            '                Catch ex As Exception
            '                    clsCommon.ProgressBarHide()
            '                    Throw New Exception("Error at Row No" + clsCommon.myCstr(ii + 1) + Environment.NewLine + ex.Message)
            '                Finally
            '                    clsCommon.ProgressBarHide()
            '                End Try
            '            End If
            '            Me.Controls.Remove(gv)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Sub LocalException(ByVal str As String)
        Throw New Exception(str)
    End Sub
    Sub AddRowsByImportAfterValidation(ByVal Arr As List(Of clsMilkShiftUploaderDetail), ByVal isShowPercent As Boolean)
        Try
            isInsideLoadData = True
            'If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
            '    LoadBlankGrid()
            '    Dim ii As Decimal = 1
            '    For Each objTr As clsMilkShiftUploaderDetail In Arr
            '        gv1.Rows.AddNew()
            '        gv1.Rows(gv1.Rows.Count - 1).Cells(ColSNo).Value = objTr.SNo
            '        gv1.Rows(gv1.Rows.Count - 1).Cells(colUploaderCode).Value = objTr.VLC_Uploader_Code
            '        gv1.Rows(gv1.Rows.Count - 1).Cells(colVLCCode).Value = objTr.VLC_Code
            '        gv1.Rows(gv1.Rows.Count - 1).Cells(colVLCName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select VLC_Name from TSPL_VLC_MASTER_HEAD where VLC_Code='" + objTr.VLC_Code + "'"))
            '        gv1.Rows(gv1.Rows.Count - 1).Cells(colNoOfCan).Value = objTr.No_Of_Cans
            '        gv1.Rows(gv1.Rows.Count - 1).Cells(colMilkWeight).Value = objTr.Milk_Weight
            '        gv1.Rows(gv1.Rows.Count - 1).Cells(colFATPer).Value = objTr.FAT
            '        gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFPer).Value = objTr.SNF
            '        gv1.Rows(gv1.Rows.Count - 1).Cells(colDockCollectionMilkType).Value = objTr.Dock_Collection_Milk_Type


            '        gv1.Rows(gv1.Rows.Count - 1).Cells(colRejectRejectType).Value = objTr.Reject_Type
            '        If clsCommon.myLen(objTr.Reject_Type) > 0 Then
            '            gv1.Rows(gv1.Rows.Count - 1).Cells(colRejectDefaulter).Value = objTr.Reject_Defaulter
            '        End If
            '        If isShowPercent Then
            '            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / Arr.Count), "Loading data in grid" + clsCommon.myCstr(ii) + "/" + clsCommon.myCstr(Arr.Count) + "")
            '        Else
            '            clsCommon.ProgressBarUpdate("Loading data in grid " + clsCommon.myCstr(ii) + "/" + clsCommon.myCstr(Arr.Count) + "")
            '        End If
            '        ii += 1
            '        gv1.Refresh()
            '    Next
            'End If
        Catch ex As Exception
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub txtMCCFromDate_ValueChanged(sender As Object, e As EventArgs) Handles txtMCCFromDate.ValueChanged

    End Sub

    Private Sub MyLabel39_Click(sender As Object, e As EventArgs) Handles MyLabel39.Click

    End Sub

    Private Sub MyLabel40_Click(sender As Object, e As EventArgs) Handles MyLabel40.Click

    End Sub

    Private Sub txtMCCToDate_ValueChanged(sender As Object, e As EventArgs) Handles txtMCCToDate.ValueChanged

    End Sub

    Private Sub TxtMultiSelectFinder8_Load(sender As Object, e As EventArgs) Handles TxtMultiSelectFinder8.Load

    End Sub
End Class
