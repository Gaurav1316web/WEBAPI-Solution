Imports System.Data.SqlClient
Imports common
Public Class frmGateEntryTransaction
#Region "Variables"
    Dim IsInsideLoadData As Boolean = False
    Const colDSelect As String = "SELECT"
#End Region
    Private Sub frmGateEntryTransaction_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtDate.Value = clsCommon.GETSERVERDATE()
        cmbReportType.SelectedIndex = 0
    End Sub
    Private Sub txtRouteNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtRouteNo._MYValidating
        Try
            Dim qry As String = " select ROUTE_NO as Code,ROUTE_NAME as Name from TSPL_BULK_ROUTE_MASTER "
            txtRouteNo.Value = clsCommon.ShowSelectForm("GTENT-ROUTE", qry, "Code", "", txtRouteNo.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtVendor__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtVendor._MYValidating
        Try
            Dim qry As String = " select Vendor_Code AS Code,Vendor_Name as Name from TSPL_VEndor_MASTER  "
            txtVendor.Value = clsCommon.ShowSelectForm("GTENT-ROUTE", qry, "Code", " isnull(vendor_type,'') = 'A'", txtVendor.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCustomer__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCustomer._MYValidating
        Try
            Dim qry As String = " select Cust_Code as Code,Customer_Name as Name from TSPL_CUSTOMER_MASTER  "
            txtCustomer.Value = clsCommon.ShowSelectForm("GTENT-ROUTE", qry, "Code", "", txtCustomer.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub



    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        btnClosePressed()
    End Sub

    Sub btnClosePressed()
        Me.Close()
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Sub PostData()
        Dim trans As SqlTransaction = Nothing
        Dim CurrentUserCode As String = objCommonVar.CurrentUserCode
        Dim j As Integer = 0
        Try
            Dim ArrCheck As New ArrayList()
            If clsCommon.MyMessageBoxShow(Me, "Are you sure to post the Records?", "", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                Exit Sub
            End If

            For ii As Integer = 0 To gv1.RowCount - 1
                If (clsCommon.myCBool(gv1.Rows(ii).Cells(colDSelect).Value)) Then
                    ArrCheck.Add(gv1.Rows(ii).Cells("Gate_Entry_Date").Value)
                End If
            Next
            If ArrCheck.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select at least one Gate Entry Document", Me.Text)
                Exit Sub
            End If
            Dim obj
            Dim dt As Date = Nothing
            Dim AddDaysFOrExpiryDate As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ExpiryDaysBulkProcurementPriceChart, clsFixedParameterCode.ExpiryDaysBulkProcurementPriceChart, Nothing))

            For i As Integer = 0 To gv1.Rows.Count - 1
                trans = clsDBFuncationality.GetTransactin()
                j = j + 1
                clsCommon.ProgressBarPercentUpdate(j / gv1.Rows.Count * 100, " Saving and posting Record(s) " & j & " of Total " & gv1.Rows.Count)
                dt = clsCommon.GetPrintDate(gv1.Rows(i).Cells("Gate_Entry_Date").Value, "dd/MMM/yyyy hh:mm:ss tt")
                clsCommon.ProgressBarPercentUpdate(j / gv1.Rows.Count * 100, " Saving and posting Record(s) " & j & " of Total " & gv1.Rows.Count & " Gate Entry Document ")

                '                If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(i).Cells("Type").Value), "Route") = CompairStringResult.Equal Then
                '                    Dim intTDLastColumn As Integer = 0
                '                    ' Gate Entry start here
                '                    obj = New clsGateEntry()
                '                    obj.Gate_Entry_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.GateEntry, clsDocTransactionType.MccProc, gv1.Rows(i).Cells("Gate_Entry_Location_Code").Value)
                '                    If clsCommon.myLen(obj.Gate_Entry_No) <= 0 Then
                '                        Throw New Exception("Error in Gate Entry  No genertion")
                '                    End If
                '                    'obj.IsAgainstJobWork = clsCommon.myCstr(gv1.Rows(i).Cells("IsJobWork").Value)
                '                    'obj.Sublocation_Code = clsCommon.myCstr(gv1.Rows(i).Cells("JobWork Location").Value)
                '                    obj.Doc_Type = "MccProc"
                '                    obj.Ref_PK_Id = clsCommon.myCdbl(gv1.Rows(i).Cells("PK_Id").Value)
                '                    obj.Date_And_Time = clsCommon.GetPrintDate(gv1.Rows(i).Cells("Gate_Entry_Date").Value, "dd/MMM/yyyy hh:mm:ss tt")
                '                    obj.Vendor_Code = ""
                '                    obj.Vendor_Desc = ""
                '                    obj.Dispatched_From_Mcc = clsCommon.myCstr(gv1.Rows(i).Cells("Gate_Entry_Location_Code").Value)
                '                    obj.location_Code = clsCommon.myCstr(gv1.Rows(i).Cells("Gate_Entry_Location_Code").Value)
                '                    obj.Location_Desc = clsLocation.GetName(obj.location_Code, trans)
                '                    'obj.Tanker_No = clsCommon.myCstr(gv1.Rows(i).Cells("Tanker No.").Value)
                '                    obj.ROUTE_NO = clsCommon.myCstr(gv1.Rows(i).Cells("ROUTE_No").Value)
                '                    obj.Challan_Date = clsCommon.GetPrintDate(gv1.Rows(i).Cells("Gate_Entry_Date").Value, "dd/MMM/yyyy")
                '                    obj.Item_Code = clsCommon.myCstr(gv1.Rows(i).Cells("Item_Code").Value)
                '                    obj.Item_Desc = clsItemMaster.GetItemName(obj.Item_Code, trans)
                '                    Dim DefaultUOm As String = clsItemMaster.GetItemDefaultUnit(obj.Item_Code, trans)
                '                    obj.UOM = DefaultUOm
                '                    obj.Qty_In_Kg = clsCommon.myCdbl(gv1.Rows(i).Cells("Qty").Value)
                '                    obj.fat_per = clsCommon.myCdbl(gv1.Rows(i).Cells("FAT").Value)
                '                    obj.snf_Per = clsCommon.myCdbl(gv1.Rows(i).Cells("SNF").Value)
                '                    obj.isPosted = 1
                '                    obj.Posting_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                '                    obj.isNewEntry = True
                '                    obj.Modify_By = objCommonVar.CurrentUserCode
                '                    obj.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                '                    obj.Created_By = objCommonVar.CurrentUserCode
                '                    obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                '                    obj.comp_code = objCommonVar.CurrentCompanyCode
                '                    obj.MIKL_TYPE_CODE = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 MILK_TYPE_CODE from TSPL_MILK_TYPE_MASTER where milk_type='Mix'", trans))

                '                    obj.Arr = New List(Of clsGateEntryChemberNoDetails)
                '                    Dim objGateEntryTR As New clsGateEntryChemberNoDetails()
                '                    objGateEntryTR.Line_No = 1
                '                    objGateEntryTR.Chamber_Desc = "FRONT"
                '                    objGateEntryTR.Item_Code = obj.Item_Code
                '                    objGateEntryTR.UOM = obj.UOM
                '                    objGateEntryTR.fat_per = obj.fat_per
                '                    objGateEntryTR.snf_Per = obj.snf_Per
                '                    objGateEntryTR.Chamber_Qty = obj.Qty_In_Kg
                '                    objGateEntryTR.DIP_Status = "F"
                '                    objGateEntryTR.Sample_Lifted = "Y"
                '                    objGateEntryTR.MIKL_TYPE_CODE = obj.MIKL_TYPE_CODE
                '                    objGateEntryTR.Dip_value = ""
                '                    objGateEntryTR.Seal_No = ""
                '                    obj.Arr.Add(objGateEntryTR)
                '                    objCommonVar.CurrentUserCode = clsERPFuncationality.getRandomUserCode("Tspl_Gate_Entry_Details", "Created_By", clsCommon.myCstr(gv1.Rows(i).Cells("Gate_Entry_Location_Code").Value), "location_Code", trans)

                '                    clsGateEntry.saveData(obj, trans)
                '                    objCommonVar.CurrentUserCode = CurrentUserCode
                '                    Dim GateEntryNo As String = obj.Gate_Entry_No

                '                    ' Weighment start here

                '                    obj = New clsWeighment()

                '                    obj.Weighment_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.Weighment, clsDocTransactionType.MccProc, gv1.Rows(i).Cells("Gate_Entry_Location_Code").Value)
                '                    If clsCommon.myLen(obj.Weighment_No) <= 0 Then
                '                        Throw New Exception("Error in Weighment No genertion")
                '                    End If
                '                    'obj.IsAgainstJobWork = clsCommon.myCstr(gv1.Rows(i).Cells("IsJobWork").Value)
                '                    'obj.Joblocation_Code = clsCommon.myCstr(gv1.Rows(i).Cells("JobWork Location").Value)
                '                    obj.Tare_Weight_date = dt
                '                    obj.Weighment_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                '                    obj.Gate_Entry_No = clsCommon.myCstr(GateEntryNo)
                '                    obj.Doc_Type = "MccProc"
                '                    obj.Date_And_Time = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                '                    'obj.Challan_No = ChallanNo
                '                    'obj.Challan_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy")
                '                    'obj.Tanker_No = clsCommon.myCstr(gv1.Rows(i).Cells("Tanker No.").Value)
                '                    obj.Dispatched_From_Mcc = clsCommon.myCstr(gv1.Rows(i).Cells("Gate_Entry_Location_Code").Value)
                '                    obj.location_Code = clsCommon.myCstr(gv1.Rows(i).Cells("Gate_Entry_Location_Code").Value)
                '                    obj.Location_Desc = clsLocation.GetName(obj.location_Code, trans)
                '                    obj.Vendor_Code = ""
                '                    obj.Vendor_Desc = ""
                '                    obj.Item_Code = clsCommon.myCstr(gv1.Rows(i).Cells("Item_Code").Value)
                '                    obj.Item_Desc = clsItemMaster.GetItemName(obj.Item_Code, trans)
                '                    obj.Qty_In_Kg = clsCommon.myCdbl(gv1.Rows(i).Cells("Qty").Value)
                '                    obj.snf_Per = clsCommon.myCdbl(gv1.Rows(i).Cells("SNF").Value)
                '                    obj.fat_per = clsCommon.myCdbl(gv1.Rows(i).Cells("FAT").Value)
                '                    obj.Gross_Weight = clsCommon.myCdbl(gv1.Rows(i).Cells("Qty").Value)
                '                    obj.Tare_Weight = 0
                '                    obj.Net_Weight = clsCommon.myCdbl(gv1.Rows(i).Cells("Qty").Value)
                '                    obj.UOM = DefaultUOm
                '                    obj.Weighment_Slip_No = ""
                '                    obj.isPosted = 1
                '                    obj.Posting_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                '                    obj.isNewEntry = True
                '                    obj.Modify_By = objCommonVar.CurrentUserCode
                '                    obj.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                '                    obj.Created_By = objCommonVar.CurrentUserCode
                '                    obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                '                    obj.comp_code = objCommonVar.CurrentCompanyCode
                '                    obj.snf_KG = (obj.Qty_In_Kg * obj.snf_per) / 100
                '                    obj.fat_KG = (obj.Qty_In_Kg * obj.fat_per) / 100

                '                    Dim PriceCode As String = clsDBFuncationality.getSingleValue("select top 1 Price_Code from TSPL_BULK_PRICE_master where effective_Date<='" & clsCommon.GetPrintDate(dt, "dd/MMM/yyyy") & "' and expirydate >= '" & clsCommon.GetPrintDate(dt.AddDays(AddDaysFOrExpiryDate), "dd/MMM/yyyy") & "' and
                'IsDefaultForTankerDispatch =1 order by price_date desc", trans)

                '                    Dim dtPrice As DataTable = clsDBFuncationality.GetDataTable("Select  * from TSPL_Bulk_Price_MASTER where Price_Code='" & PriceCode & "'", trans)
                '                    Dim NetRate As Double = 0
                '                    Dim FatW As Double = 0
                '                    Dim SNfW As Double = 0
                '                    Dim FATRatio As Double = 0
                '                    Dim SNFRatio As Double = 0
                '                    If clsCommon.myLen(dtPrice) > 0 Then
                '                        NetRate = clsCommon.myCdbl(dtPrice.Rows(0)("Standard_Rate"))
                '                        FatW = clsCommon.myCdbl(dtPrice.Rows(0)("Fat_Weightage"))
                '                        SNfW = clsCommon.myCdbl(dtPrice.Rows(0)("Snf_Weightage"))
                '                        FATRatio = clsCommon.myCdbl(dtPrice.Rows(0)("Fat_Percentage"))
                '                        SNFRatio = clsCommon.myCdbl(dtPrice.Rows(0)("Snf_Percentage"))
                '                    Else
                '                        clsCommon.MyMessageBoxShow(Me, "Please create price", Me.Text)
                '                        Exit Sub
                '                    End If
                '                    obj.fat_Rate = MyMath.RoundDown(clsCommon.myCdbl(NetRate) * FatW / FATRatio, 2)
                '                    obj.SNF_Rate = MyMath.RoundDown(clsCommon.myCdbl(NetRate) * SNfW / SNFRatio, 2)
                '                    obj.FAT_Value = MyMath.RoundDown(obj.fat_KG * obj.fat_Rate, 2)
                '                    obj.Snf_Value = MyMath.RoundDown(obj.snf_KG * obj.SNF_Rate, 2)
                '                    obj.Amount = clsCommon.myFormat(Math.Round(obj.FAT_Value + obj.Snf_Value, 0))
                '                    Dim lineNo As Integer = 1

                '                    obj.Arr = New List(Of clsWeighmentChemberNoDetails)
                '                    Dim objTr As New clsWeighmentChemberNoDetails()
                '                    objTr.Line_No = lineNo
                '                    objTr.snf_Per = clsCommon.myCdbl(gv1.Rows(i).Cells("SNF").Value)
                '                    objTr.fat_per = clsCommon.myCdbl(gv1.Rows(i).Cells("FAT").Value)
                '                    objTr.Item_Code = clsCommon.myCstr(gv1.Rows(i).Cells("Item_Code").Value)
                '                    objTr.UOM = DefaultUOm
                '                    ' objTr.Sublocation_Code = clsCommon.myCstr(gv1.Rows(i).Cells("Silo Code").Value)
                '                    objTr.Tare_Weight = 0
                '                    objTr.Net_Weight = clsCommon.myCdbl(gv1.Rows(i).Cells("Qty").Value)
                '                    objTr.Gross_Weight = clsCommon.myCdbl(gv1.Rows(i).Cells("Qty").Value)
                '                    objTr.CH_FAT_Kg = (obj.Net_Weight * obj.fat_per) / 100
                '                    objTr.CH_SNF_Kg = (obj.Net_Weight * obj.snf_per) / 100
                '                    objTr.CH_FAT_Rate = obj.fat_Rate
                '                    objTr.CH_SNF_Rate = obj.SNF_Rate
                '                    objTr.CH_FAT_Value = objTr.CH_FAT_Kg * objTr.CH_FAT_Rate
                '                    objTr.CH_Amount = obj.Amount
                '                    objTr.CH_SNF_Value = objTr.CH_SNF_Kg * objTr.CH_SNF_Rate
                '                    obj.Arr.Add(objTr)


                '                    clsWeighment.saveData(obj, trans)
                '                    objCommonVar.CurrentUserCode = CurrentUserCode
                '                    Dim weighmentNo As String = obj.Weighment_No

                '                    ' Quality check start here
                '                    obj = New clsQualityCheck()
                '                    obj.QC_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.QualityCheck, clsDocTransactionType.MccProc, gv1.Rows(i).Cells("Gate_Entry_Location_Code").Value)
                '                    If clsCommon.myLen(obj.QC_No) <= 0 Then
                '                        Throw New Exception("Error in QC No genertion")
                '                    End If
                '                    'obj.IsAgainstJobWork = clsCommon.myCstr(gv1.Rows(i).Cells("IsJobWork").Value)
                '                    'obj.Joblocation_Code = clsCommon.myCstr(gv1.Rows(i).Cells("JobWork Location").Value)
                '                    obj.Gate_Entry_No = clsCommon.myCstr(GateEntryNo)
                '                    obj.Doc_Type = "MccProc"
                '                    obj.Gate_Entry_Date_And_Time = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                '                    obj.QC_In_Date_Time = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                '                    obj.QC_Out_Date_Time = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                '                    obj.Vendor_Code = ""
                '                    obj.Vendor_Desc = ""
                '                    obj.Dispatched_From_Mcc_Code = clsCommon.myCstr(gv1.Rows(i).Cells("Gate_Entry_Location_Code").Value)
                '                    obj.Dispatched_From_Mcc_Desc = clsLocation.GetName(obj.Dispatched_From_Mcc_Code, trans)
                '                    obj.location_Code = clsCommon.myCstr(gv1.Rows(i).Cells("Gate_Entry_Location_Code").Value)
                '                    obj.Location_Desc = clsLocation.GetName(obj.location_Code, trans)
                '                    'obj.Challan_No = ChallanNo
                '                    'obj.Challan_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy")
                '                    'obj.Tanker_No = clsCommon.myCstr(gv1.Rows(i).Cells("Tanker No.").Value)
                '                    obj.Weighment_No = clsCommon.myCstr(weighmentNo)
                '                    obj.Weighment_Date = clsCommon.myCDate(dt, "dd/MMM/yyyy")
                '                    obj.Item_Code = clsCommon.myCstr(gv1.Rows(i).Cells("Item_Code").Value)
                '                    obj.Item_Desc = clsItemMaster.GetItemName(obj.Item_Code, trans)
                '                    obj.Remarks = ""
                '                    obj.UOM = DefaultUOm
                '                    obj.Qty_In_Kg = clsCommon.myCdbl(gv1.Rows(i).Cells("Qty").Value)
                '                    obj.snf_Per = clsCommon.myCdbl(gv1.Rows(i).Cells("SNF").Value)
                '                    obj.fat_per = clsCommon.myCdbl(gv1.Rows(i).Cells("FAT").Value)
                '                    obj.snf_KG = (obj.Qty_In_Kg * obj.snf_per) / 100
                '                    obj.fat_KG = (obj.Qty_In_Kg * obj.fat_per) / 100
                '                    obj.Receipt_Control_FAT = 0
                '                    obj.Receipt_Control_SNF = 0
                '                    obj.DeductionAmount = 0
                '                    obj.isPosted = 1
                '                    obj.Posting_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                '                    obj.isNewEntry = True
                '                    obj.Modify_By = objCommonVar.CurrentUserCode
                '                    obj.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                '                    obj.Created_By = objCommonVar.CurrentUserCode
                '                    obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                '                    obj.comp_code = objCommonVar.CurrentCompanyCode
                '                    obj.is_Param_Accepted = 1
                '                    Dim intStartParam As Integer = 0

                '                    Dim intQCstartrColumn As Integer = 0
                '                    Dim paramcount As Integer = 0
                '                    intTDLastColumn = intStartParam + paramcount
                '                    Dim QcNo = obj.QC_No

                '                    obj.arrQcParam = New List(Of clsQcParam)
                '                    Dim index As Integer = 1

                '                    Dim objQCParam As New clsQcParam()

                '                    objQCParam.QC_No = clsCommon.myCstr(obj.QC_No)
                '                    objQCParam.LINE_NO = index
                '                    objQCParam.Param_Field_Code = "FAT"
                '                    objQCParam.Param_Field_Desc = "FAT"
                '                    objQCParam.Param_Field_Value = clsCommon.myCstr(gv1.Rows(i).Cells("FAT").Value)
                '                    objQCParam.Param_Type = "FAT"
                '                    obj.arrQcParam.Add(objQCParam)

                '                    objQCParam = New clsQcParam()
                '                    objQCParam.LINE_NO = index
                '                    objQCParam.QC_No = clsCommon.myCstr(obj.QC_No)
                '                    objQCParam.Param_Field_Code = "SNF"
                '                    objQCParam.Param_Field_Desc = "SNF"
                '                    objQCParam.Param_Field_Value = clsCommon.myCstr(gv1.Rows(i).Cells("SNF").Value)
                '                    objQCParam.Param_Type = "SNF"

                '                    obj.arrQcParam.Add(objQCParam)

                '                    obj.Arr = New List(Of clsQualityChemberNoDetails)
                '                    Dim objQcChamber As New clsQualityChemberNoDetails()
                '                    objQcChamber.Line_No = lineNo
                '                    objQcChamber.snf_Per = clsCommon.myCdbl(gv1.Rows(i).Cells("SNF").Value)
                '                    objQcChamber.fat_per = clsCommon.myCdbl(gv1.Rows(i).Cells("FAT").Value)
                '                    objQcChamber.Item_Code = clsCommon.myCstr(gv1.Rows(i).Cells("Item_Code").Value)
                '                    objQcChamber.UOM = DefaultUOm
                '                    obj.Arr.Add(objQcChamber)

                '                    clsQualityCheck.saveData(obj, trans)
                '                    ' unloading start here 

                '                    obj = New clsUnloading()
                '                    obj.isNewEntry = True

                '                    ''  Dim dt As Date = clsCommon.GETSERVERDATE(trans, "dd/MMM/yyyy hh:mm:ss tt")

                '                    If obj.isNewEntry Then
                '                        obj.Unloading_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.Unloading, clsDocTransactionType.NA, gv1.Rows(i).Cells("Gate_Entry_Location_Code").Value)
                '                        If clsCommon.myLen(obj.Unloading_No) <= 0 Then
                '                            Throw New Exception("Error In Unloading  No Genertion")
                '                        End If
                '                    End If
                '                    obj.Gate_Entry_No = GateEntryNo
                '                    obj.Unloading_Date_Time = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                '                    'obj.Tanker_No = clsCommon.myCstr(gv1.Rows(i).Cells("Tanker No.").Value)
                '                    obj.Weighment_No = weighmentNo
                '                    obj.QC_No = QcNo
                '                    obj.location_Code = clsCommon.myCstr(gv1.Rows(i).Cells("Gate_Entry_Location_Code").Value)
                '                    'obj.Sub_location_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select top 1 Location_Code   from TSPL_LOCATION_MASTER  where Is_Sub_Location='Y' and Main_Location_Code='" & (gv1.Rows(i).Cells("Gate_Entry_Location_Code").Value) & "'", trans))
                '                    obj.Item_Code = clsCommon.myCstr(gv1.Rows(i).Cells("Item_Code").Value)
                '                    obj.Item_Desc = clsItemMaster.GetItemName(obj.Item_Code, trans)
                '                    obj.UOM = DefaultUOm
                '                    obj.Qty = clsCommon.myCdbl(gv1.Rows(i).Cells("Qty").Value)
                '                    obj.fat_per = clsCommon.myCdbl(gv1.Rows(i).Cells("FAT").Value)
                '                    obj.snf_Per = clsCommon.myCdbl(gv1.Rows(i).Cells("SNF").Value)
                '                    obj.snf_KG = (obj.qty * obj.snf_per) / 100
                '                    obj.fat_KG = (obj.qty * obj.fat_per) / 100
                '                    obj.isPosted = 0
                '                    obj.isPosted = 1
                '                    obj.Posting_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy")
                '                    obj.Modify_By = objCommonVar.CurrentUserCode
                '                    obj.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                '                    obj.comp_code = objCommonVar.CurrentCompanyCode
                '                    obj.Created_By = objCommonVar.CurrentUserCode
                '                    obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                '                    'obj.Sub_location_Code = clsCommon.myCstr(gv1.Rows(i).Cells("Silo Code").Value)

                '                    obj.Arr = New List(Of clsUnloadingChemberNoDetails)
                '                    Dim Line As Integer = 1

                '                    Dim objUnloading As New clsUnloadingChemberNoDetails()
                '                    objUnloading.Item_Code = clsCommon.myCstr(gv1.Rows(i).Cells("Item_Code").Value)
                '                    objUnloading.UOM = DefaultUOm
                '                    objUnloading.Line_No = Line
                '                    objUnloading.fat_per = clsCommon.myCdbl(gv1.Rows(i).Cells("FAT").Value)
                '                    objUnloading.snf_Per = clsCommon.myCstr(gv1.Rows(i).Cells("SNF").Value)
                '                    'objUnloading.Sublocation_Code = clsCommon.myCstr(gv1.Rows(i).Cells("Silo Code").Value)
                '                    obj.Arr.Add(objUnloading)


                '                    clsUnloading.saveData(obj, trans)
                '                    Dim Unloading = obj.Unloading_No

                '                    'Gate out start here
                '                    obj = New clsGateOut()
                '                    obj.isNewEntry = True
                '                    obj.Doc_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.GateOut, clsDocTransactionType.NA, clsCommon.myCstr(gv1.Rows(i).Cells("Gate_Entry_Location_Code").Value))
                '                    If clsCommon.myLen(obj.Doc_No) <= 0 Then
                '                        Throw New Exception("Error In Document  No Genertion")
                '                    End If
                '                    obj.Gate_Entry_No = GateEntryNo
                '                    obj.Doc_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                '                    ' obj.Tanker_No = clsCommon.myCstr(gv1.Rows(i).Cells("Tanker No.").Value)
                '                    obj.Weighment_No = weighmentNo
                '                    obj.QC_No = QcNo
                '                    obj.Modify_By = objCommonVar.CurrentUserCode
                '                    obj.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                '                    obj.comp_code = objCommonVar.CurrentCompanyCode
                '                    obj.Created_By = objCommonVar.CurrentUserCode
                '                    obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                '                    clsGateOut.saveData(obj, trans)

                '                    'Cleaning start here
                '                    obj = New clsCleaning()
                '                    obj.isNewEntry = True

                '                    obj.Doc_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.Cleaning, clsDocTransactionType.NA, gv1.Rows(i).Cells("Gate_Entry_Location_Code").Value)
                '                    If clsCommon.myLen(obj.Doc_No) <= 0 Then
                '                        Throw New Exception("Error in Cleaning No genertion")
                '                    End If
                '                    'obj.IsAgainstJobWork = clsCommon.myCstr(gv1.Rows(i).Cells("IsJobWork").Value)
                '                    'obj.Joblocation_Code = clsCommon.myCstr(gv1.Rows(i).Cells("JobWork Location").Value)
                '                    obj.Gate_Entry_No = GateEntryNo
                '                    obj.Start_Date_Time = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                '                    obj.End_Date_Time = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                '                    'obj.Tanker_No = clsCommon.myCstr(gv1.Rows(i).Cells("Tanker No.").Value)
                '                    obj.Weighment_No = weighmentNo
                '                    obj.QC_No = QcNo
                '                    obj.Status = "OK"
                '                    obj.Remarks = ""
                '                    obj.isPosted = 1
                '                    obj.Posting_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy")
                '                    obj.Modify_By = objCommonVar.CurrentUserCode
                '                    obj.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                '                    obj.comp_code = objCommonVar.CurrentCompanyCode
                '                    obj.Created_By = objCommonVar.CurrentUserCode
                '                    obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                '                    obj.InTime = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                '                    obj.OutTime = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                '                    clsCleaning.saveData(obj, trans)
                '                    'Milk Transfer In
                '                    obj = New clsMilkTransferIn
                '                    obj.isNewEntry = True

                '                    obj.Receipt_Challan_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.MilkTransferIn, clsDocTransactionType.NA, gv1.Rows(i).Cells("Gate_Entry_Location_Code").Value)
                '                    If clsCommon.myLen(obj.Receipt_Challan_No) <= 0 Then
                '                        Throw New Exception("Error in Milk Transfer In genertion")
                '                    End If
                '                    obj.Receipt_Challan_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                '                    obj.Dispatch_Challan_No = ""
                '                    obj.Weighment_No = weighmentNo
                '                    obj.Qc_No = QcNo
                '                    obj.Gate_Entry_no = GateEntryNo
                '                    obj.location_code = clsCommon.myCstr(gv1.Rows(i).Cells("Gate_Entry_Location_Code").Value)
                '                    obj.km_reading_receipt = 0
                '                    obj.Receipt_Control_FAT = 0
                '                    obj.Receipt_Control_SNF = 0
                '                    obj.PriceCode = PriceCode
                '                    obj.FAT_W = clsCommon.myCdbl(dtPrice.Rows(0)("Fat_Weightage"))
                '                    obj.FAT_R = clsCommon.myCdbl(dtPrice.Rows(0)("Fat_Percentage"))
                '                    obj.SNf_W = clsCommon.myCdbl(dtPrice.Rows(0)("Snf_Weightage"))
                '                    obj.SNf_R = clsCommon.myCdbl(dtPrice.Rows(0)("Snf_Percentage"))

                '                    'obj.fat_Rate = MyMath.RoundDown(clsCommon.myCdbl(NetRate) * FatW / FATRatio, 2)
                '                    'obj.SNF_Rate = MyMath.RoundDown(clsCommon.myCdbl(NetRate) * SNfW / SNFRatio, 2)
                '                    'obj.FAT_Value = MyMath.RoundDown(obj.fat_KG * obj.fat_Rate, 2)
                '                    'obj.Snf_Value = MyMath.RoundDown(obj.snf_KG * obj.SNF_Rate, 2)
                '                    'obj.Amount = clsCommon.myFormat(Math.Round(obj.FAT_Value + obj.Snf_Value, 0))

                '                    obj.Document_Amount = 0

                '                    obj.Modified_By = objCommonVar.CurrentUserCode
                '                    obj.Modified_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                '                    obj.Comp_Code = objCommonVar.CurrentCompanyCode
                '                    obj.Created_By = objCommonVar.CurrentUserCode
                '                    obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")

                '                    clsMilkTransferIn.saveData(obj, trans)
                '                    clsMilkTransferIn.postData(obj.Receipt_Challan_No, trans)
                '                    clsDBFuncationality.ExecuteNonQuery("Update TSPL_GATE_ENTRY set Status = 1 where pk_id = " & clsCommon.myCdbl(gv1.Rows(i).Cells("PK_Id").Value) & "", trans)
                '                    trans.Commit()
                If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(i).Cells("Type").Value), "Purchase") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(i).Cells("Type").Value), "Conversion In") = CompairStringResult.Equal Then
                    obj = New clsGateEntry()
                    obj.Gate_Entry_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.GateEntry, clsDocTransactionType.BulkProc, gv1.Rows(i).Cells("Gate_Entry_Location_Code").Value)
                    If clsCommon.myLen(obj.Gate_Entry_No) <= 0 Then
                        Throw New Exception("Error in Gate Entry  No genertion")
                    End If
                    obj.Doc_Type = "BulkProc"
                    obj.Ref_PK_Id = clsCommon.myCdbl(gv1.Rows(i).Cells("PK_Id").Value)
                    obj.Date_And_Time = clsCommon.GetPrintDate(gv1.Rows(i).Cells("Gate_Entry_Date").Value, "dd/MMM/yyyy hh:mm:ss tt")
                    obj.location_Code = clsCommon.myCstr(gv1.Rows(i).Cells("Gate_Entry_Location_Code").Value)
                    obj.Location_Desc = clsLocation.GetName(obj.location_Code, trans)

                    If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(i).Cells("Type").Value), "Purchase") = CompairStringResult.Equal Then
                        obj.Vendor_Code = clsCommon.myCstr(gv1.Rows(i).Cells("Vendor_Code").Value)
                        obj.Vendor_Desc = clsVendorMaster.GetName(obj.Vendor_Code, trans)
                    Else
                        obj.Vendor_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Code from  TSPL_CUSTOMER_VENDOR_MAPPING where Cust_Code='" & gv1.Rows(i).Cells("Customer_Code").Value & "'", trans))
                        obj.Vendor_Desc = clsVendorMaster.GetName(obj.Vendor_Code, trans)
                        If clsCommon.myLen(obj.Vendor_Code) <= 0 Then
                            clsCommon.MyMessageBoxShow(Me, "Please map vendor with this Customer [" & gv1.Rows(i).Cells("Customer_Code").Value & "]", Me.Text)
                            Exit Sub
                        End If
                    End If
                    'obj.Challan_No = clsCommon.myCstr("ND")
                    'obj.Challan_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy")
                    obj.Item_Code = clsCommon.myCstr(gv1.Rows(i).Cells("Item_code").Value)
                    obj.Item_Desc = clsItemMaster.GetItemName(obj.Item_Code, trans)
                    Dim DefaultUOm As String = clsItemMaster.GetItemDefaultUnit(obj.Item_Code, trans)
                    obj.UOM = clsCommon.myCstr(DefaultUOm)
                    obj.Qty_In_Kg = clsCommon.myCDecimal(gv1.Rows(i).Cells("QTY").Value)
                    obj.fat_per = clsCommon.myCDecimal(gv1.Rows(i).Cells("FAT").Value)
                    obj.snf_Per = clsCommon.myCDecimal(gv1.Rows(i).Cells("SNF").Value)
                    obj.isPosted = 1
                    obj.Posting_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                    obj.isNewEntry = True
                    obj.Modify_By = objCommonVar.CurrentUserCode
                    obj.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                    obj.Created_By = objCommonVar.CurrentUserCode
                    obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                    obj.comp_code = objCommonVar.CurrentCompanyCode
                    objCommonVar.CurrentUserCode = clsERPFuncationality.getRandomUserCode("Tspl_Gate_Entry_Details", "Created_By", clsCommon.myCstr(gv1.Rows(i).Cells("Gate_Entry_Location_Code").Value), "location_Code", trans)
                    clsGateEntry.saveData(obj, trans)
                    objCommonVar.CurrentUserCode = CurrentUserCode
                    Dim GateEntryNo As String = obj.Gate_Entry_No

                    clsCommon.ProgressBarPercentUpdate(j / gv1.Rows.Count * 100, " Saving and posting Record(s) " & j & " of Total " & gv1.Rows.Count & " Weighment Document ")
                    obj = New clsWeighment()

                    obj.Weighment_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.Weighment, clsDocTransactionType.BulkProc, gv1.Rows(i).Cells("Gate_Entry_Location_Code").Value)
                    If clsCommon.myLen(obj.Weighment_No) <= 0 Then
                        Throw New Exception("Error in Weighment No genertion")
                    End If
                    '
                    'obj.Joblocation_Code = clsCommon.myCstr(gv1.Rows(i).Cells("JobWork Location").Value)
                    obj.Tare_Weight_date = dt
                    obj.Weighment_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                    obj.Gate_Entry_No = clsCommon.myCstr(GateEntryNo)
                    obj.Doc_Type = "BulkProc"
                    obj.Date_And_Time = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                    obj.Challan_No = clsCommon.myCstr("ND")
                    obj.Challan_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy")
                    obj.location_Code = clsCommon.myCstr(gv1.Rows(i).Cells("Gate_Entry_Location_Code").Value)
                    obj.Location_Desc = clsLocation.GetName(obj.location_Code, trans)
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(i).Cells("Type").Value), "Purchase") = CompairStringResult.Equal Then
                        obj.Vendor_Code = clsCommon.myCstr(gv1.Rows(i).Cells("Vendor_Code").Value)
                        obj.Vendor_Desc = clsVendorMaster.GetName(obj.Vendor_Code, trans)
                    Else
                        obj.Vendor_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Code from  TSPL_CUSTOMER_VENDOR_MAPPING where Cust_Code='" & gv1.Rows(i).Cells("Customer_Code").Value & "'", trans))
                        obj.Vendor_Desc = clsVendorMaster.GetName(obj.Vendor_Code, trans)
                        If clsCommon.myLen(obj.Vendor_Code) <= 0 Then
                            clsCommon.MyMessageBoxShow(Me, "Please map vendor with this Customer [" & gv1.Rows(i).Cells("Customer_Code").Value & "]", Me.Text)
                            Exit Sub
                        End If
                    End If
                    obj.Item_Code = clsCommon.myCstr(gv1.Rows(i).Cells("Item_Code").Value)
                    obj.Item_Desc = clsItemMaster.GetItemName(obj.Item_Code, trans)
                    obj.Qty_In_Kg = clsCommon.myCDecimal(gv1.Rows(i).Cells("QTY").Value)
                    obj.snf_Per = clsCommon.myCDecimal(gv1.Rows(i).Cells("SNF").Value)
                    obj.fat_per = clsCommon.myCDecimal(gv1.Rows(i).Cells("FAT").Value)
                    obj.snf_KG = (obj.Qty_In_Kg * obj.snf_per) / 100
                    obj.fat_KG = (obj.Qty_In_Kg * obj.fat_per) / 100
                    obj.Gross_Weight = clsCommon.myCdbl(gv1.Rows(i).Cells("Qty").Value)
                    obj.Tare_Weight = 0
                    obj.Net_Weight = clsCommon.myCdbl(gv1.Rows(i).Cells("Qty").Value)
                    obj.UOM = DefaultUOm
                    obj.Weighment_Slip_No = ""
                    obj.isPosted = 1
                    obj.Posting_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                    obj.isNewEntry = True
                    obj.Modify_By = objCommonVar.CurrentUserCode
                    obj.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                    obj.Created_By = objCommonVar.CurrentUserCode
                    obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                    obj.comp_code = objCommonVar.CurrentCompanyCode
                    objCommonVar.CurrentUserCode = clsERPFuncationality.getRandomUserCode("TSPL_Weighment_Detail", "Created_By", clsCommon.myCstr(gv1.Rows(i).Cells("Gate_Entry_Location_Code").Value), "location_Code", trans)

                    obj.Arr = New List(Of clsWeighmentChemberNoDetails)
                    Dim objTr As New clsWeighmentChemberNoDetails()
                    Dim lineNo As Integer = 1
                    objTr.Line_No = lineNo
                    objTr.snf_Per = clsCommon.myCdbl(gv1.Rows(i).Cells("SNF").Value)
                    objTr.fat_per = clsCommon.myCdbl(gv1.Rows(i).Cells("FAT").Value)
                    objTr.Item_Code = clsCommon.myCstr(gv1.Rows(i).Cells("Item_Code").Value)
                    objTr.UOM = DefaultUOm
                    ' objTr.Sublocation_Code = clsCommon.myCstr(gv1.Rows(i).Cells("Silo Code").Value)
                    objTr.Tare_Weight = 0
                    objTr.Net_Weight = clsCommon.myCdbl(gv1.Rows(i).Cells("Qty").Value)
                    objTr.Gross_Weight = clsCommon.myCdbl(gv1.Rows(i).Cells("Qty").Value)
                    objTr.CH_FAT_Kg = (obj.Net_Weight * obj.fat_per) / 100
                    objTr.CH_SNF_Kg = (obj.Net_Weight * obj.snf_per) / 100
                    objTr.CH_FAT_Rate = obj.fat_Rate
                    objTr.CH_SNF_Rate = obj.SNF_Rate
                    objTr.CH_FAT_Value = objTr.CH_FAT_Kg * objTr.CH_FAT_Rate
                    objTr.CH_Amount = obj.Amount
                    objTr.CH_SNF_Value = objTr.CH_SNF_Kg * objTr.CH_SNF_Rate
                    obj.Arr.Add(objTr)

                    clsWeighment.saveData(obj, trans)
                    objCommonVar.CurrentUserCode = CurrentUserCode
                    Dim weighmentNo As String = obj.Weighment_No
                    clsCommon.ProgressBarPercentUpdate(j / gv1.Rows.Count * 100, " Saving and posting Record(s) " & j & " of Total " & gv1.Rows.Count & " QC Document ")
                    obj = New clsQualityCheck()

                    obj.QC_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.QualityCheck, clsDocTransactionType.BulkProc, gv1.Rows(i).Cells("Gate_Entry_Location_Code").Value)

                    If clsCommon.myLen(obj.QC_No) <= 0 Then
                        Throw New Exception("Error in QC No genertion")
                    End If

                    obj.Gate_Entry_No = clsCommon.myCstr(GateEntryNo)
                    obj.Doc_Type = "BulkProc"
                    obj.Gate_Entry_Date_And_Time = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                    obj.QC_In_Date_Time = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                    obj.QC_Out_Date_Time = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(i).Cells("Type").Value), "Purchase") = CompairStringResult.Equal Then
                        obj.Vendor_Code = clsCommon.myCstr(gv1.Rows(i).Cells("Vendor_Code").Value)
                    Else
                        obj.Vendor_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Code from  TSPL_CUSTOMER_VENDOR_MAPPING where Cust_Code='" & gv1.Rows(i).Cells("Customer_Code").Value & "'", trans))
                        If clsCommon.myLen(obj.Vendor_Code) <= 0 Then
                            clsCommon.MyMessageBoxShow(Me, "Please map vendor with this Customer [" & gv1.Rows(i).Cells("Customer_Code").Value & "]", Me.Text)
                            Exit Sub
                        End If
                    End If
                    obj.location_Code = clsCommon.myCstr(gv1.Rows(i).Cells("Gate_Entry_Location_Code").Value)
                    obj.Location_Desc = clsLocation.GetName(obj.location_Code, trans)
                    obj.Challan_No = clsCommon.myCstr("ND")
                    obj.Challan_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy")
                    obj.Weighment_No = clsCommon.myCstr(weighmentNo)
                    obj.Weighment_Date = clsCommon.myCDate(dt, "dd/MMM/yyyy")
                    obj.Item_Code = clsCommon.myCstr(gv1.Rows(i).Cells("Item_Code").Value)
                    obj.Item_Desc = clsItemMaster.GetItemName(obj.Item_Code, trans)
                    obj.Remarks = clsCommon.myCstr("")
                    obj.UOM = DefaultUOm
                    obj.Qty_In_Kg = clsCommon.myCDecimal(gv1.Rows(i).Cells("Qty").Value)
                    obj.fat_per = clsCommon.myCDecimal(gv1.Rows(i).Cells("FAT").Value)
                    obj.snf_Per = clsCommon.myCDecimal(gv1.Rows(i).Cells("SNF").Value)
                    obj.snf_KG = (obj.Qty_In_Kg * obj.snf_per) / 100
                    obj.fat_KG = (obj.Qty_In_Kg * obj.fat_per) / 100
                    obj.Receipt_Control_FAT = 0
                    obj.Receipt_Control_SNF = 0
                    obj.DeductionAmount = 0
                    obj.isPosted = 1
                    obj.Posting_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                    obj.isNewEntry = True
                    obj.Modify_By = objCommonVar.CurrentUserCode
                    obj.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                    obj.Created_By = objCommonVar.CurrentUserCode
                    obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                    obj.comp_code = objCommonVar.CurrentCompanyCode
                    obj.is_Param_Accepted = 1

                    Dim intQCstartrColumn As Integer = 0
                    ' intTDLastColumn = intStartParam + paramcount
                    Dim QcNo = obj.QC_No

                    obj.arrQcParam = New List(Of clsQcParam)
                    Dim index As Integer = 1

                    Dim objQCParam As New clsQcParam()
                    objQCParam.QC_No = clsCommon.myCstr(obj.QC_No)
                    objQCParam.LINE_NO = index
                    objQCParam.Param_Field_Code = "FAT"
                    objQCParam.Param_Field_Desc = "FAT"
                    objQCParam.Param_Field_Value = clsCommon.myCstr(gv1.Rows(i).Cells("FAT").Value)
                    objQCParam.Param_Type = "FAT"
                    obj.arrQcParam.Add(objQCParam)

                    objQCParam = New clsQcParam()
                    objQCParam.LINE_NO = index
                    objQCParam.QC_No = clsCommon.myCstr(obj.QC_No)
                    objQCParam.Param_Field_Code = "SNF"
                    objQCParam.Param_Field_Desc = "SNF"
                    objQCParam.Param_Field_Value = clsCommon.myCstr(gv1.Rows(i).Cells("SNF").Value)
                    objQCParam.Param_Type = "SNF"

                    obj.arrQcParam.Add(objQCParam)
                    lineNo = 1
                    obj.Arr = New List(Of clsQualityChemberNoDetails)
                    Dim objQcChamber As New clsQualityChemberNoDetails()
                    objQcChamber.Line_No = lineNo
                    objQcChamber.snf_Per = clsCommon.myCdbl(gv1.Rows(i).Cells("SNF").Value)
                    objQcChamber.fat_per = clsCommon.myCdbl(gv1.Rows(i).Cells("FAT").Value)
                    objQcChamber.Item_Code = clsCommon.myCstr(gv1.Rows(i).Cells("Item_Code").Value)
                    objQcChamber.UOM = DefaultUOm
                    obj.Arr.Add(objQcChamber)

                    clsQualityCheck.saveData(obj, trans)
                    objCommonVar.CurrentUserCode = CurrentUserCode
                    Dim qc_no As String = obj.QC_No
                    clsCommon.ProgressBarPercentUpdate(j / gv1.Rows.Count * 100, " Saving and posting Record(s) " & j & " of Total " & gv1.Rows.Count & " Unloading Document ")
                    obj = New clsUnloading()
                    obj.isNewEntry = True

                    obj.Unloading_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.Unloading, clsDocTransactionType.NA, clsCommon.myCstr(gv1.Rows(i).Cells("Gate_Entry_Location_Code").Value))

                    If clsCommon.myLen(obj.Unloading_No) <= 0 Then
                        Throw New Exception("Error In Unloading  No Genertion")
                        Exit Sub
                    End If

                    ' obj.Joblocation_Code = clsCommon.myCstr(gv1.Rows(i).Cells("JobWork Location").Value)
                    'If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(i).Cells("IsJobWork").Value), "1") = CompairStringResult.Equal Then
                    '    Dim strVirtualLoc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Code from TSPL_LOCATION_MASTER where Main_Location_Code='" & clsCommon.myCstr(gv1.Rows(i).Cells("Gate_Entry_Location_Code").Value) & "' and is_sub_location='Y' and Location_Type='Virtual' and UseInJobWork=1  ", trans))
                    '    obj.Sub_location_Code = strVirtualLoc
                    'Else
                    '    obj.Sub_location_Code = clsCommon.myCstr(gv1.Rows(i).Cells("Silo No").Value)
                    'End If
                    obj.Gate_Entry_No = GateEntryNo
                    obj.Unloading_Date_Time = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                    ' obj.Tanker_No = clsCommon.myCstr(gv1.Rows(i).Cells("TankerNo.").Value)
                    obj.Weighment_No = weighmentNo
                    obj.QC_No = qc_no
                    obj.location_Code = clsCommon.myCstr(gv1.Rows(i).Cells("Gate_Entry_Location_Code").Value)

                    obj.Item_Code = clsCommon.myCstr(gv1.Rows(i).Cells("Item_Code").Value)
                    obj.Item_Desc = clsItemMaster.GetItemName(obj.Item_Code, trans)
                    obj.UOM = DefaultUOm
                    obj.qty = clsCommon.myCDecimal(gv1.Rows(i).Cells("QTY").Value)
                    obj.fat_per = clsCommon.myCDecimal(gv1.Rows(i).Cells("FAT").Value)
                    obj.snf_Per = clsCommon.myCDecimal(gv1.Rows(i).Cells("SNF").Value)
                    obj.snf_KG = (obj.qty * obj.snf_per) / 100
                    obj.fat_KG = (obj.qty * obj.fat_per) / 100
                    obj.isPosted = 1
                    obj.Posting_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                    obj.Modify_By = objCommonVar.CurrentUserCode
                    obj.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                    obj.comp_code = objCommonVar.CurrentCompanyCode
                    obj.Created_By = objCommonVar.CurrentUserCode
                    obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                    objCommonVar.CurrentUserCode = clsERPFuncationality.getRandomUserCode("TSPL_MILK_UNLOADING", "Created_By", clsCommon.myCstr(gv1.Rows(i).Cells("Gate_Entry_Location_Code").Value), "location_Code", trans)
                    clsUnloading.saveData(obj, trans)
                    objCommonVar.CurrentUserCode = CurrentUserCode

                    clsCommon.ProgressBarPercentUpdate(j / gv1.Rows.Count * 100, " Saving and posting Record(s) " & j & " of Total " & gv1.Rows.Count & " Gate Out Document ")
                    obj = New clsGateOut()
                    obj.isNewEntry = True
                    obj.Doc_No = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.GateOut, clsDocTransactionType.NA, clsCommon.myCstr(gv1.Rows(i).Cells("Gate_Entry_Location_Code").Value))

                    If clsCommon.myLen(obj.Doc_No) <= 0 Then
                        Throw New Exception("Error In GateOut  No Genertion")
                    End If

                    ' obj.Joblocation_Code = clsCommon.myCstr(gv1.Rows(i).Cells("JobWork Location").Value)
                    obj.Gate_Entry_No = GateEntryNo
                    obj.Doc_Date = clsCommon.GetPrintDate(dt, "dd/MMM/yyyy hh:mm:ss tt")
                    ' obj.Tanker_No = clsCommon.myCstr(gv1.Rows(i).Cells("TankerNo.").Value)
                    obj.Weighment_No = weighmentNo
                    obj.QC_No = qc_no
                    obj.Modify_By = objCommonVar.CurrentUserCode
                    obj.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                    obj.comp_code = objCommonVar.CurrentCompanyCode
                    obj.Created_By = objCommonVar.CurrentUserCode
                    obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                    objCommonVar.CurrentUserCode = clsERPFuncationality.getRandomUserCode("TSPL_Gate_Out", "Created_By", "", "", trans)
                    clsGateOut.saveData(obj, trans)
                    objCommonVar.CurrentUserCode = CurrentUserCode

                    clsCommon.ProgressBarPercentUpdate(j / gv1.Rows.Count * 100, " Saving and posting Record(s) " & j & " of Total " & gv1.Rows.Count & " Bulk Milk SRN Document ")
                    obj = New clsBulkMilkSRN()
                    obj.isNewEntry = True

                    obj.SRN_NO = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.BulkMilkSRN, clsDocTransactionType.NA, clsCommon.myCstr(gv1.Rows(i).Cells("Gate_Entry_Location_Code").Value))
                    If clsCommon.myLen(obj.SRN_NO) <= 0 Then
                        Throw New Exception("Error In SRN  No Genertion")
                    End If
                    obj.PO_NO = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.BulkMilkPO, "", clsCommon.myCstr(gv1.Rows(i).Cells("Gate_Entry_Location_Code").Value))
                    If clsCommon.myLen(obj.PO_NO) <= 0 Then
                        Throw New Exception("Error In Auto PO  No Genertion")
                    End If

                    ' obj.Joblocation_Code = clsCommon.myCstr(gv1.Rows(i).Cells("JobWork Location").Value)
                    obj.PO_Date = dt
                    obj.isApproved = 1
                    obj.SRN_Date = dt
                    obj.Gate_Entry_No = GateEntryNo
                    obj.Weighment_No = weighmentNo
                    obj.Weighment_Date = dt
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(i).Cells("Type").Value), "Purchase") = CompairStringResult.Equal Then
                        obj.Vendor_Code = clsCommon.myCstr(gv1.Rows(i).Cells("Vendor_Code").Value)
                    Else
                        obj.Vendor_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Code from  TSPL_CUSTOMER_VENDOR_MAPPING where Cust_Code='" & gv1.Rows(i).Cells("Customer_Code").Value & "'", trans))
                        If clsCommon.myLen(obj.Vendor_Code) <= 0 Then
                            clsCommon.MyMessageBoxShow(Me, "Please map vendor with this Customer [" & gv1.Rows(i).Cells("Customer_Code").Value & "]", Me.Text)
                            Exit Sub
                        End If
                    End If
                    obj.Loc_Code = clsCommon.myCstr(gv1.Rows(i).Cells("Gate_Entry_Location_Code").Value)
                    obj.Challan_No = clsCommon.myCstr("ND")
                    obj.Challan_Date = dt
                    ' obj.Tanker_No = clsCommon.myCstr(gv1.Rows(i).Cells("TankerNo.").Value)
                    obj.Price_Code = clsDBFuncationality.getSingleValue("select top 1 Price_Code from TSPL_BULK_PRICE_master where 2=2 and effective_Date<='" & clsCommon.GetPrintDate(dt, "dd/MMM/yyyy") & "' and expirydate >= '" & clsCommon.GetPrintDate(dt.AddDays(AddDaysFOrExpiryDate), "dd/MMM/yyyy") & "'  " & IIf(clsCommon.myLen(obj.vendor_code) > 0, " and TSPL_Bulk_Price_MASTER.Price_Code in (select PriceCode  from Tspl_Vendor_Price_Chart_mapping where VendorCode='" & obj.vendor_code & "') ", "") & " and Posted='1' and IsDefaultForTankerDispatch=0 order by price_date desc")
                    If clsCommon.myLen(obj.Price_Code) <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Please create price", Me.Text)
                        Exit Sub
                    End If
                    obj.QC_No = qc_no
                    obj.Qc_Date = dt
                    obj.isPosted = 0
                    obj.Item_Code = clsCommon.myCstr(gv1.Rows(i).Cells("Item_Code").Value)
                    obj.Item_Desc = clsItemMaster.GetItemName(obj.Item_Code, trans)
                    obj.UOM = DefaultUOm
                    obj.Gross_Weight = clsCommon.myCdbl(gv1.Rows(i).Cells("Qty").Value)
                    obj.Tare_Weight = 0
                    obj.Net_Weight = clsCommon.myCdbl(gv1.Rows(i).Cells("QTY").Value)
                    obj.snf_Per = clsCommon.myCdbl(gv1.Rows(i).Cells("SNF").Value)
                    obj.fat_per = clsCommon.myCdbl(gv1.Rows(i).Cells("FAT").Value)
                    obj.fat_KG = (obj.Net_Weight * obj.fat_per) / 100
                    obj.snf_KG = (obj.Net_Weight * obj.snf_per) / 100

                    Dim dtPrice As DataTable = clsDBFuncationality.GetDataTable("Select  * from TSPL_Bulk_Price_MASTER where Price_Code='" & obj.Price_Code & "'", trans)
                    obj.BasicRate = clsCommon.myCdbl(dtPrice.Rows(0)("Standard_Rate"))
                    obj.Standardrate = clsCommon.myCdbl(dtPrice.Rows(0)("Standard_Rate"))
                    obj.NetRate = clsCommon.myCdbl(dtPrice.Rows(0)("Standard_Rate"))

                    Dim FatW As Double = clsCommon.myCdbl(dtPrice.Rows(0)("Fat_Weightage"))
                    Dim SNfW As Double = clsCommon.myCdbl(dtPrice.Rows(0)("Snf_Weightage"))
                    Dim FATRatio As Double = clsCommon.myCdbl(dtPrice.Rows(0)("Fat_Percentage"))
                    Dim SNFRatio As Double = clsCommon.myCdbl(dtPrice.Rows(0)("Snf_Percentage"))
                    obj.fat_Rate = MyMath.RoundDown(clsCommon.myCdbl(obj.NetRate) * FatW / FATRatio, 2)
                    obj.SNF_Rate = MyMath.RoundDown(clsCommon.myCdbl(obj.NetRate) * SNfW / SNFRatio, 2)
                    obj.FatAmt = MyMath.RoundDown(obj.fat_KG * obj.fat_Rate, 2)
                    obj.SnfAmt = MyMath.RoundDown(obj.snf_KG * obj.SNF_Rate, 2)
                    obj.Actual_Amount = clsCommon.myFormat(Math.Round(obj.FatAmt + obj.SnfAmt, 0))
                    obj.Amount = obj.Actual_Amount
                    obj.FinalMilkRate = clsCommon.myFormat(Math.Round(clsCommon.myCdbl(obj.Amount) / clsCommon.myCdbl(obj.Net_Weight), 2))
                    obj.SpecialDeduction = 0
                    obj.Deduction = 0
                    obj.Incentive = 0
                    obj.Actual_Amount = clsCommon.myCdbl(gv1.Rows(i).Cells("Purchase_Amount").Value)
                    obj.Modify_By = objCommonVar.CurrentUserCode
                    obj.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                    obj.comp_code = objCommonVar.CurrentCompanyCode
                    obj.Created_By = objCommonVar.CurrentUserCode
                    obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                    objCommonVar.CurrentUserCode = clsERPFuncationality.getRandomUserCode("TSPL_Bulk_MILK_SRN", "Created_By", clsCommon.myCstr(gv1.Rows(i).Cells("Gate_Entry_Location_Code").Value), "Loc_Code", trans)
                    clsBulkMilkSRN.saveData(obj, trans)
                    clsBulkMilkSRN.postData(obj.SRN_NO, clsUserMgtCode.frmBulkMilkSRN, trans)
                    objCommonVar.CurrentUserCode = CurrentUserCode
                    clsDBFuncationality.ExecuteNonQuery("Update TSPL_GATE_ENTRY set Status = 1 where pk_id = " & clsCommon.myCdbl(gv1.Rows(i).Cells("PK_Id").Value) & "", trans)

                    trans.Commit()
                End If
            Next
            'trans.Commit()
            clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
            LoadData()

        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub frmGateEntryTransaction_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F5 Then
            '  btnOKPressed()
        ElseIf e.KeyCode = Keys.Escape Then
            btnClosePressed()
        End If
    End Sub

    'Private Sub gvHead_ValueChanging(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.ValueChangingEventArgs)
    '    If Not IsInsideLoadData Then
    '        If gvHead.CurrentColumn Is gvHead.Columns(colHSelect) Then
    '            Dim strVendorCode As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHVendorCode).Value)
    '            Dim strVendorName As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHVendorName).Value)
    '            If clsCommon.myLen(VendorCode) <= 0 Then
    '                VendorCode = strVendorCode
    '                VendorName = strVendorName
    '            End If
    '            If clsCommon.CompairString(strVendorCode, VendorCode) = CompairStringResult.Equal Then
    '                Dim strCode As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHCode).Value)
    '                If clsCommon.myLen(strCode) > 0 Then
    '                    LoadDetailData(e.NewValue, strCode)
    '                End If
    '            Else
    '                common.clsCommon.MyMessageBoxShow(Me, "Invoice's Customer should be `" + VendorName)
    '                e.Cancel = True
    '            End If
    '        End If
    '    End If
    'End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData()
    End Sub
    Sub LoadData()
        Try
            gv1.DataSource = Nothing
            gv1.Columns.Clear()
            gv1.AutoGenerateColumns = True
            If cmbReportType.SelectedIndex = 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select Report Type", Me.Text)
                Exit Sub
            End If
            If clsCommon.myLen(txtDate.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Date Can't be Blank. ", Me.Text)
                txtDate.Focus()
                Return
            End If
            Dim dt As DataTable = GetData()
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                gv1.DataSource = dt
                gv1.AllowDeleteRow = False
                gv1.AllowAddNewRow = False
                gv1.ShowGroupPanel = False
                gv1.AllowColumnReorder = False
                gv1.AllowRowReorder = False
                gv1.EnableSorting = True
                gv1.MasterTemplate.ShowRowHeaderColumn = False
                gv1.TableElement.TableHeaderHeight = 40
                gv1.AutoSizeRows = False

                For ii As Integer = 0 To gv1.Columns.Count - 1
                    gv1.Columns(ii).IsVisible = False
                    gv1.Columns(ii).ReadOnly = True
                Next
                If Not gv1.Columns.Contains(colDSelect) Then
                    Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
                    repoSelect.HeaderText = " "
                    repoSelect.Name = colDSelect
                    repoSelect.ReadOnly = False
                    repoSelect.Width = 25
                    repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
                    gv1.MasterTemplate.Columns.Insert(0, repoSelect)
                End If
                gv1.Columns("SNo").IsVisible = True
                gv1.Columns("SNo").Width = 60
                gv1.Columns("SNo").HeaderText = "SNo"

                gv1.Columns("Gate_Entry_Location_Code").IsVisible = True
                gv1.Columns("Gate_Entry_Location_Code").Width = 100
                gv1.Columns("Gate_Entry_Location_Code").HeaderText = "Gate Entry Location Code"

                gv1.Columns("Gate_Entry_Type").IsVisible = True
                gv1.Columns("Gate_Entry_Type").Width = 80
                gv1.Columns("Gate_Entry_Type").HeaderText = "Gate Entry Type"

                gv1.Columns("Type").IsVisible = True
                gv1.Columns("Type").Width = 100
                gv1.Columns("Type").HeaderText = "Type"

                If clsCommon.myLen(gv1.Columns("Route_No")) > 0 Then
                    gv1.Columns("Route_No").IsVisible = True
                    gv1.Columns("Route_No").Width = 100
                    gv1.Columns("Route_No").HeaderText = "Route"

                    gv1.Columns("Route_Desc").IsVisible = True
                    gv1.Columns("Route_Desc").Width = 100
                    gv1.Columns("Route_Desc").HeaderText = "Route Name"

                    gv1.Columns("Vehicle_No").IsVisible = True
                    gv1.Columns("Vehicle_No").Width = 100
                    gv1.Columns("Vehicle_No").HeaderText = "Vehicle No"

                    gv1.Columns("Vehicle_Desc").IsVisible = True
                    gv1.Columns("Vehicle_Desc").Width = 100
                    gv1.Columns("Vehicle_Desc").HeaderText = "Vehicle Description"
                End If
                gv1.Columns("PK_ID").IsVisible = False
                If clsCommon.myLen(gv1.Columns("Vendor_Code")) > 0 Then
                    gv1.Columns("Vendor_Code").IsVisible = True
                    gv1.Columns("Vendor_Code").Width = 100
                    gv1.Columns("Vendor_Code").HeaderText = "Vendor"

                    gv1.Columns("Vendor_Name").IsVisible = True
                    gv1.Columns("Vendor_Name").Width = 150
                    gv1.Columns("Vendor_Name").HeaderText = "Vendor Name"
                End If

                If clsCommon.myLen(gv1.Columns("Customer_Code")) > 0 Then
                    gv1.Columns("Customer_Code").IsVisible = True
                    gv1.Columns("Customer_Code").Width = 100
                    gv1.Columns("Customer_Code").HeaderText = "Customer"

                    gv1.Columns("Customer_Name").IsVisible = True
                    gv1.Columns("Customer_Name").Width = 150
                    gv1.Columns("Customer_Name").HeaderText = "Customer Name"
                End If
                'gv1.Columns("Location_Code").IsVisible = True
                'gv1.Columns("Location_Code").Width = 100
                'gv1.Columns("Location_Code").HeaderText = "Location"

                '    gv1.Columns("Location_Desc").IsVisible = True
                '    gv1.Columns("Location_Desc").Width = 100
                '    gv1.Columns("Location_Desc").HeaderText = "Location Name"

                gv1.Columns("Item_Code").IsVisible = True
                gv1.Columns("Item_Code").Width = 100
                gv1.Columns("Item_Code").HeaderText = "Item"

                gv1.Columns("Item_Desc").IsVisible = True
                gv1.Columns("Item_Desc").Width = 150
                gv1.Columns("Item_Desc").HeaderText = "Item Description"

                gv1.Columns("Qty").IsVisible = True
                gv1.Columns("Qty").Width = 80
                gv1.Columns("Qty").ReadOnly = False
                gv1.Columns("Qty").FormatString = "{0:n2}"
                gv1.Columns("Qty").HeaderText = "Qty"

                gv1.Columns("FAT").IsVisible = True
                gv1.Columns("FAT").Width = 80
                gv1.Columns("FAT").ReadOnly = False
                gv1.Columns("FAT").FormatString = "{0:n2}"
                gv1.Columns("FAT").HeaderText = "Fat"

                gv1.Columns("SNF").IsVisible = True
                gv1.Columns("SNF").Width = 80
                gv1.Columns("SNF").ReadOnly = False
                gv1.Columns("SNF").FormatString = "{0:n2}"
                gv1.Columns("SNF").HeaderText = "Snf"
                RadPageView1.SelectedPage = RadPageViewPage2
                EnableDisableCtrl(False)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Function GetData() As DataTable
        Dim qry As String = " Select ROW_NUMBER() OVER(ORDER BY PK_ID) as SNo,TSPL_GATE_ENTRY.PK_Id,TSPL_GATE_ENTRY.Gate_Entry_Date,FORMAT(TSPL_GATE_ENTRY.Gate_Entry_Date, 'dd/MMM/yyyy hh:mm tt')  as Gate_Entry_Date_Full,TSPL_GATE_ENTRY.Gate_Entry_Location_Code,case when TSPL_GATE_ENTRY.Gate_Entry_Type = 'I' then 'In' else 'Out' end as Gate_Entry_Type,case when TSPL_GATE_ENTRY.Type = 'R' then 'Route' when TSPL_GATE_ENTRY.Type = 'P' then 'Purchase' when TSPL_GATE_ENTRY.Type = 'CI' then 'Conversion In' when TSPL_GATE_ENTRY.Type = 'D' then 'Dock' when TSPL_GATE_ENTRY.Type = 'BS' then 'Bulk Sale' when TSPL_GATE_ENTRY.Type = 'CO' THEN 'Conversion Out' else '' end as Type ,TSPL_GATE_ENTRY.Route_No,
 TSPL_BULK_ROUTE_MASTER.ROUTE_NAME as Route_Desc,TSPL_GATE_ENTRY.Vehicle_No ,TSPL_GATE_ENTRY.Vehicle_Desc,TSPL_GATE_ENTRY.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_GATE_ENTRY.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_GATE_ENTRY.Location_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_GATE_ENTRY.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_GATE_ENTRY.Qty,TSPL_GATE_ENTRY.FAT,TSPL_GATE_ENTRY.SNF,TSPL_GATE_ENTRY.File_Info   
 from TSPL_GATE_ENTRY  
 left outer join  TSPL_BULK_ROUTE_MASTER on TSPL_BULK_ROUTE_MASTER.ROUTE_NO=TSPL_GATE_ENTRY.Route_No  
 left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_GATE_ENTRY.Customer_Code  
 left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_GATE_ENTRY.Location_Code  
 left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_GATE_ENTRY.Item_Code  
 left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_GATE_ENTRY.Vendor_Code 
 where convert(date,TSPL_GATE_ENTRY.Gate_Entry_Date,103) = '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "' and TSPL_GATE_ENTRY.status = 0  "
        If cmbReportType.SelectedIndex = 1 Then
            Throw New Exception("This Feature is not for you")
            If clsCommon.myLen(txtRouteNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select Route", Me.Text)
                Exit Function
            End If
            qry = " select SNo,PK_Id,Gate_Entry_Date,Gate_Entry_Location_Code,Gate_Entry_Type,Type,Route_No,Route_Desc,Vehicle_No,Vehicle_Desc,Item_Code,Item_Desc,Qty,FAT,SNF from ( " & Environment.NewLine & qry & " and TSPL_GATE_ENTRY.Route_No ='" & txtRouteNo.Value & "' "
        ElseIf cmbReportType.SelectedIndex = 2 Then
            If clsCommon.myLen(txtVendor.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select Vendor", Me.Text)
                Exit Function
            End If
            qry = " select SNo,PK_Id,Gate_Entry_Date,Gate_Entry_Location_Code,Gate_Entry_Type,Type,Vendor_Code,Vendor_Name,Item_Code,Item_Desc,Qty,FAT,SNF from ( " & Environment.NewLine & qry & " and TSPL_GATE_ENTRY.Vendor_Code = '" & txtVendor.Value & "'"
        ElseIf cmbReportType.SelectedIndex = 3 Then
            If clsCommon.myLen(txtCustomer.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select Customer", Me.Text)
                Exit Function
            End If
            qry = " select SNo,PK_Id,Gate_Entry_Date,Gate_Entry_Location_Code,Gate_Entry_Type,Type,Customer_Code,Customer_Name,Item_Code,Item_Desc,Qty,FAT,SNF from ( " & Environment.NewLine & qry & " and TSPL_GATE_ENTRY.Customer_Code = '" & txtCustomer.Value & "'"
        End If
        qry += " " & Environment.NewLine & " )xx order by PK_ID "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Return dt
    End Function

    Sub setGridPropery()
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = True
        gv1.EnableSorting = False
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.MasterTemplate.ShowColumnHeaders = True
        gv1.EnableAlternatingRowColor = True
        gv1.EnableFiltering = True
        gv1.ShowFilteringRow = True
    End Sub

    Private Sub cmbReportType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles cmbReportType.SelectedIndexChanged
        If cmbReportType.SelectedIndex = 1 Then
            RadGroupBoxRoute.Visible = True
            RadGroupBoxVendor.Visible = False
            RadGroupBoxCust.Visible = False
            txtVendor.Value = ""
            txtCustomer.Value = ""
        ElseIf cmbReportType.SelectedIndex = 2 Then
            RadGroupBoxRoute.Visible = False
            RadGroupBoxVendor.Visible = True
            RadGroupBoxCust.Visible = False
            txtRouteNo.Value = ""
            txtCustomer.Value = ""
        ElseIf cmbReportType.SelectedIndex = 3 Then
            RadGroupBoxRoute.Visible = False
            RadGroupBoxVendor.Visible = False
            RadGroupBoxCust.Visible = True
            txtRouteNo.Value = ""
            txtVendor.Value = ""
        Else
            RadGroupBoxRoute.Visible = False
            RadGroupBoxVendor.Visible = False
            RadGroupBoxCust.Visible = False
        End If
    End Sub

    Private Sub btnreset_Click(sender As Object, e As EventArgs) Handles btnreset.Click
        cmbReportType.SelectedIndex = 0
        RadPageView1.SelectedPage = RadPageViewPage1
        EnableDisableCtrl(True)
    End Sub
    Sub EnableDisableCtrl(ByVal val As Boolean)
        RadGroupBox1.Enabled = val
    End Sub
End Class

