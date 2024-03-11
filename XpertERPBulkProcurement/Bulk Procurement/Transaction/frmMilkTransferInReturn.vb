'============================Sanjeet (07/12/2017)=========
Imports common
Imports System.Data.SqlClient

Public Class frmMilkTransferInReturn
    Inherits FrmMainTranScreen
    Dim AllowJobWorkonGateEntryBulkProc As Integer = 0

    '=============QC===================
    Const colQCsno As String = "SNO"
    Const colQCloc_code As String = "QCLoc_Code"
    Const colQCloc_name As String = "QCLoc_Name"
    Const colQCToloc_code As String = "QCTOLoc_Code"
    Const colQCToloc_name As String = "QCSTOLoc_Name"
    Const colQCitemcode As String = "qcitemcode"
    Const colQCiname As String = "iname"
    Const colQCparamcode As String = "paramcode"
    Const colQCparam_desc As String = "param_desc"
    Const colQCparam_type As String = "paramtype"
    Const colQCparam_nature As String = "paramnature"
    Const colQCrange1 As String = "range1"
    Const colQCrange2 As String = "range2"
    Const colQCstatus As String = "status"
    Const colQCvalue1 As String = "value1"
    Const colQCvalue2 As String = "value2"
    Const colQCRange As String = "Range"
    Const colQCValue As String = "OUTPUTVALUE"
    Const colQCOutStatus As String = "OutStatus"
    Const colQCremarks As String = "remarkS"
    Const colQCHistort As String = "History"
    Dim gv_qc As New RadGridView
    Dim AllowReverseUnpost As Integer = 0
    Public Const colSLNo As String = "SLNO"
    Public Const colSealName As String = "SealName"
    Public Const colItemCode As String = "ItemCode"
    Public Const colHSN As String = "HSNCode"
    Public Const colItemDesc As String = "ItemDesc"
    Public Const colUOM As String = "UOM"
    Public Const colFATPer As String = "FatPer"
    Public Const colSNFPer As String = "SNFPer"
    Public Const colFATPerQC As String = "FatPerQc"
    Public Const colSNFPerQC As String = "SNFPerQc"
    Public Const colFATKG As String = "FATKG"
    Public Const colSNFKG As String = "SNFKG"
    Public Const colGrossWeightDisp As String = "colGrossWeightDisp"
    Public Const colGrossWeightRcpt As String = "colGrossWeightRcpt"
    Public Const colTareWeightDisp As String = "colTareWeightDisp"
    Public Const colTareWeightRcpt As String = "colTareWeightRcpt"
    Public Const colNetWeightDisp As String = "colNetWeightDisp"
    Dim TankerFromMaster As Integer = 0
    Dim MCCChamberwise As Integer = 0
    Dim AllowBulkProcMCCwithoutTankerDispatch As Integer
    Const colChamberDesc As String = "colChamberDesc"
    Public Const colFATRate As String = "colFATRate"
    Public Const colSNFRate As String = "colSNFRate"
    Public Const colFATValue As String = "colFATValue"
    Public Const colSNFValue As String = "colSNFValue"
    Public Const colRcptAmt As String = "colRcptAmt"

    Public strDocNo As String = ""
    Dim isCellValueChangedOpen As Boolean = False
    Public Const colNetWeightRcpt As String = "colNetWeightRcpt"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public obj As clsMilkTransferInReturn = Nothing
    Sub loadDispatchData(ByVal RcptChlnNo As String)
        Try
            Dim objin As clsMilkTransferIn = Nothing
            Dim strReceiptChallanReturnNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Receipt_Challan_Return_No from tspl_milk_transfer_in_Return  where Receipt_Challan_No='" & RcptChlnNo & "'"))

            If clsCommon.myLen(strReceiptChallanReturnNo) > 0 Then
                LoadData(strReceiptChallanReturnNo, NavigatorType.Current)
                Exit Sub
            End If
            objin = clsMilkTransferIn.getData(RcptChlnNo, NavigatorType.Current)
            Dim strDispChallanNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Dispatch_Challan_No from tspl_milk_transfer_in  where Receipt_Challan_No='" & RcptChlnNo & "'"))
            If clsCommon.myLen(objin.Dispatch_Challan_No) > 0 Then
                txtMccPlantCode.Value = clsCommon.myCstr(objin.location_code)
                lblMccPlantName.Text = clsLocation.GetName(txtMccPlantCode.Value, Nothing)
                fndDispatchChallanNo.Value = strDispChallanNo
            End If
            ' Dim strWeighmentNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Weighment_No  from TSPL_Weighment_Detail where Challan_No='" & strDispChallanNo & "'"))
            Dim objW As clsWeighment = clsWeighment.getData(objin.Weighment_No, "MccProc", NavigatorType.Current)
            Dim strQcNo As String = clsCommon.myCstr(objin.Qc_No)
            Dim objQ As clsQualityCheck = clsQualityCheck.getData(objin.Qc_No, "MccProc", NavigatorType.Current)
            Dim objD As clsMccDispatch = clsMccDispatch.getData(objin.Dispatch_Challan_No, NavigatorType.Current)

            If ((objW IsNot Nothing AndAlso objQ IsNot Nothing AndAlso objD IsNot Nothing) OrElse AllowBulkProcMCCwithoutTankerDispatch = 1 AndAlso objW IsNot Nothing AndAlso objQ IsNot Nothing) Then
                dtpDispatchDateAndTime.Value = objD.Dispatch_Date
                'If AllowBulkProcMCCwithoutTankerDispatch = 1 Then
                '    txtDispatchFrom.Text = objW.Dispatched_From_Mcc
                '    lblDispatchFromDesc.Text = clsLocation.GetName(objW.Dispatched_From_Mcc, Nothing)
                '    txtTankerNo.Text = objW.Tanker_No
                'Else
                '    txtDispatchFrom.Text = objD.MCC_Code
                '    lblDispatchFromDesc.Text = objD.MCC_Name
                '    txtTransferPrice.Value = objD.Transfer_Price
                '    txtTankerNo.Text = objD.Tanker_No
                'End If

                If AllowBulkProcMCCwithoutTankerDispatch = 0 Then
                    dtpDispatchDateAndTime.Value = objD.Dispatch_Date
                    txtDispatchFrom.Text = objD.MCC_Code
                    lblDispatchFromDesc.Text = objD.MCC_Name
                    txtTransferPrice.Value = objD.Transfer_Price
                    txtTankerNo.Text = objD.Tanker_No
                    txtKMReadingDisp.Text = objD.Tanker_KM_Reading
                    txtDip.Text = objD.Drip_Marking
                    txtDispControlSampleFAT.Text = objD.control_sample_fat
                    txtDispControlSampleSNF.Text = objD.control_sample_snf
                Else
                    txtDispatchFrom.Text = objW.Dispatched_From_Mcc
                    lblDispatchFromDesc.Text = clsLocation.GetName(objW.Dispatched_From_Mcc, Nothing)
                    txtTransferPrice.Value = objin.Transfer_Price
                    txtTankerNo.Text = objW.Tanker_No
                    txtKMReadingDisp.Text = 0
                    txtDip.Text = 0
                End If

                fndPriceChart.Value = clsCommon.myCstr(objin.PriceCode)
                TxtFatWeightage.Text = clsCommon.myCdbl(objin.FAT_W)
                TxtSNFWeightage.Text = clsCommon.myCdbl(objin.SNF_W)
                txtfatPercentage.Text = clsCommon.myCdbl(objin.FAT_R)
                txtSNFPercentage.Text = clsCommon.myCdbl(objin.SNF_R)

                txtSubLocation.Value = objW.Joblocation_Code
                lblSubLocation.Text = clsLocation.GetName(txtSubLocation.Value, Nothing)
                chkJobWork.Checked = IIf(objW.IsAgainstJobWork = 1, True, False)
                '  txtKMReadingDisp.Text = objD.Tanker_KM_Reading
                txtKmReadingRecpt.Text = objin.km_reading_receipt
                txtDip.Text = objD.Drip_Marking
                txtDispControlSampleFAT.Text = objD.control_sample_fat
                txtDispControlSampleSNF.Text = objD.control_sample_snf
                'chkNewSealNo.Checked = False
                loadBlankGvOldSeal()
                'loadBlankGvNewSeal()
                loadBlankGvOldSealPaper()
                'loadBlankGvNewSealPaper()
                txtgateEntryNo.Text = objW.Gate_Entry_No
                If AllowBulkProcMCCwithoutTankerDispatch = 0 Then
                    gvOldSeal.Rows(0).Cells(colSealName).Value = clsCommon.myCstr(objD.Seal_No1)
                    gvOldSeal.Rows(1).Cells(colSealName).Value = clsCommon.myCstr(objD.Seal_No2)
                    gvOldSeal.Rows(2).Cells(colSealName).Value = clsCommon.myCstr(objD.Seal_No3)
                    gvOldSeal.Rows(3).Cells(colSealName).Value = clsCommon.myCstr(objD.Seal_No4)
                    gvOldSeal.Rows(4).Cells(colSealName).Value = clsCommon.myCstr(objD.Seal_No5)
                    gvOldSeal.Rows(5).Cells(colSealName).Value = clsCommon.myCstr(objD.Seal_No6)
                    gvOldSeal.Rows(6).Cells(colSealName).Value = clsCommon.myCstr(objD.Seal_No7)
                    gvOldSeal.Rows(7).Cells(colSealName).Value = clsCommon.myCstr(objD.Seal_No8)
                    gvOldSeal.Rows(8).Cells(colSealName).Value = clsCommon.myCstr(objD.Seal_No9)
                    gvOldSeal.Rows(9).Cells(colSealName).Value = clsCommon.myCstr(objD.Seal_No10)

                    If objD.arrPaperSeal IsNot Nothing AndAlso objD.arrPaperSeal.Count > 0 Then
                        Try
                            For i As Integer = 0 To objD.arrPaperSeal.Count - 1
                                gvOldSealPaper.Rows(i).Cells(colSealName).Value = objD.arrPaperSeal(i).Seal_No
                            Next
                        Catch exx As Exception
                        End Try
                    End If
                End If

                txtWeighmentNo.Text = objW.Weighment_No
                dtpWeighmentDate.Value = objW.Weighment_Date
                txtDipW.Text = objW.Dip_Value
                txtgateEntryNo.Text = objW.Gate_Entry_No

                loadBlankWeighment()
                gvWeighment.Rows(0).Cells(colItemCode).Value = objW.Item_Code
                gvWeighment.Rows(0).Cells(colItemDesc).Value = objW.Item_Desc
                gvWeighment.Rows(0).Cells(colHSN).Value = clsItemMaster.GetItemHSNCode(objW.Item_Code, Nothing)
                gvWeighment.Rows(0).Cells(colUOM).Value = clsItemMaster.GetStockUnit(objW.Item_Code, Nothing)

                If AllowBulkProcMCCwithoutTankerDispatch = 0 Then
                    gvWeighment.Rows(0).Cells(colGrossWeightDisp).Value = 0
                    gvWeighment.Rows(0).Cells(colNetWeightDisp).Value = 0
                    gvWeighment.Rows(0).Cells(colTareWeightDisp).Value = 0
                End If


                gvWeighment.Rows(0).Cells(colGrossWeightRcpt).Value = objW.Gross_Weight
                gvWeighment.Rows(0).Cells(colNetWeightRcpt).Value = objW.Net_Weight
                gvWeighment.Rows(0).Cells(colTareWeightRcpt).Value = objW.Tare_Weight
                gvWeighment.Rows(0).Cells("colSilo").Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Sub_location_Code  from TSPL_MILK_UNLOADING  where Gate_Entry_No='" & objW.Gate_Entry_No & "'"))
                gvWeighment.Rows(0).Cells("colSiloDesc").Value = clsCommon.myCstr(clsLocation.GetName(gvWeighment.Rows(0).Cells("colSilo").Value, Nothing))
                gvWeighment.Rows(0).Cells(colFATPer).Value = objW.fat_per
                gvWeighment.Rows(0).Cells(colSNFPer).Value = objW.snf_Per
                '        gvWeighment.Rows(0).Cells(colSNFKG).Value = clsCommon.myCdbl(objW.Net_Weight) * clsCommon.myCdbl(objW.snf_Per) / 100
                txtQCNo.Text = objQ.QC_No
                dtpQcInDate.Value = objQ.QC_In_Date_Time
                dtpQCOutDate.Value = objQ.QC_Out_Date_Time
                If MCCChamberwise = 1 Then
                    If objW.Arr IsNot Nothing AndAlso objW.Arr.Count > 0 Then
                        gvWeighment.Rows.Clear()
                        Dim intCount As Integer = 0
                        For Each objTr As clsWeighmentChemberNoDetails In objW.Arr
                            gvWeighment.Rows.AddNew()
                            gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colSLNo).Value = objTr.Line_No
                            gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colItemCode).Value = objTr.Item_Code
                            gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colChamberDesc).Value = objTr.Chamber_Desc
                            gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colItemDesc).Value = clsIntimation.getItemName(objTr.Item_Code, Nothing)
                            gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colHSN).Value = clsItemMaster.GetItemHSNCode(objTr.Item_Code, Nothing)
                            gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colUOM).Value = clsItemMaster.GetStockUnit(objTr.Item_Code, Nothing)
                            gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colGrossWeightRcpt).Value = objTr.Gross_Weight
                            gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colNetWeightRcpt).Value = objTr.Net_Weight
                            gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colTareWeightRcpt).Value = objTr.Tare_Weight
                            gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells("colSilo").Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Sub_location_Code  from TSPL_MILK_UNLOADING  where Gate_Entry_No='" & objW.Gate_Entry_No & "'"))
                            gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells("colSiloDesc").Value = clsCommon.myCstr(clsLocation.GetName(gvWeighment.Rows(0).Cells("colSilo").Value, Nothing))
                            gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colFATPer).Value = objTr.fat_per
                            gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colSNFPer).Value = objTr.snf_Per

                            gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colGrossWeightDisp).Value = 0
                            If AllowBulkProcMCCwithoutTankerDispatch = 0 Then
                                gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colNetWeightDisp).Value = objD.arr(intCount).Qty_KG
                            Else
                                gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colNetWeightDisp).Value = 0
                            End If

                            gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colTareWeightDisp).Value = 0
                            intCount += 1
                        Next
                    End If
                End If
                If objQ.arrQcParam IsNot Nothing Then
                    If MCCChamberwise = 0 Then
                        For i As Integer = 0 To objQ.arrQcParam.Count - 1
                            Try
                                If clsCommon.CompairString(objQ.arrQcParam(i).Param_Type, "FAT") = CompairStringResult.Equal Then
                                    gvWeighment.Rows(0).Cells(colFATKG).Value = clsCommon.myCdbl(objW.Net_Weight) * clsCommon.myCdbl(objQ.arrQcParam(i).Param_Field_Value) / 100
                                    gvWeighment.Rows(0).Cells(colFATPerQC).Value = clsCommon.myCdbl(objQ.arrQcParam(i).Param_Field_Value)
                                End If
                                If clsCommon.CompairString(objQ.arrQcParam(i).Param_Type, "SNF") = CompairStringResult.Equal Then
                                    gvWeighment.Rows(0).Cells(colSNFKG).Value = clsCommon.myCdbl(objW.Net_Weight) * clsCommon.myCdbl(objQ.arrQcParam(i).Param_Field_Value) / 100
                                    gvWeighment.Rows(0).Cells(colSNFPerQC).Value = clsCommon.myCdbl(objQ.arrQcParam(i).Param_Field_Value)
                                End If
                                gvParam.Rows(0).Cells(objQ.arrQcParam(i).Param_Field_Code).Value = objQ.arrQcParam(i).Param_Field_Value
                            Catch exx As Exception
                            End Try
                        Next
                    Else
                        loadBlankGvParam()
                        For i As Integer = 0 To objQ.arrQcParam.Count - 1
                            Try
                                If clsCommon.CompairString(objQ.arrQcParam(i).Param_Type, "FAT") = CompairStringResult.Equal Then
                                    gvWeighment.Rows(objQ.arrQcParam(i).LINE_NO - 1).Cells(colFATKG).Value = clsCommon.myCdbl(objW.Arr(objQ.arrQcParam(i).LINE_NO - 1).Net_Weight) * clsCommon.myCdbl(objQ.arrQcParam(i).Param_Field_Value) / 100
                                    gvWeighment.Rows(objQ.arrQcParam(i).LINE_NO - 1).Cells(colFATPerQC).Value = clsCommon.myCdbl(objQ.arrQcParam(i).Param_Field_Value)
                                End If
                                If clsCommon.CompairString(objQ.arrQcParam(i).Param_Type, "SNF") = CompairStringResult.Equal Then
                                    gvWeighment.Rows(objQ.arrQcParam(i).LINE_NO - 1).Cells(colSNFKG).Value = clsCommon.myCdbl(objW.Arr(objQ.arrQcParam(i).LINE_NO - 1).Net_Weight) * clsCommon.myCdbl(objQ.arrQcParam(i).Param_Field_Value) / 100
                                    gvWeighment.Rows(objQ.arrQcParam(i).LINE_NO - 1).Cells(colSNFPerQC).Value = clsCommon.myCdbl(objQ.arrQcParam(i).Param_Field_Value)
                                End If
                                gvParam.Rows(objQ.arrQcParam(i).LINE_NO - 1).Cells(objQ.arrQcParam(i).Param_Field_Code).Value = objQ.arrQcParam(i).Param_Field_Value
                            Catch exx As Exception
                            End Try
                        Next
                    End If
                End If

                'Dim objp As clsPriceChartBulkProc = clsPriceChartBulkProc.GetData(objD.PriceCode, NavigatorType.Current)
                If AllowBulkProcMCCwithoutTankerDispatch = 0 Then
                    If objD IsNot Nothing Then
                        If MCCChamberwise = 0 Then
                            gvWeighment.Rows(0).Cells(colFATRate).Value = Math.Round(clsCommon.myCdbl(txtTransferPrice.Value) * clsCommon.myCdbl(objD.FAT_W) / clsCommon.myCdbl(objD.FAT_R), 2)
                            gvWeighment.Rows(0).Cells(colSNFRate).Value = Math.Round(clsCommon.myCdbl(txtTransferPrice.Value) * clsCommon.myCdbl(objD.SNF_W) / clsCommon.myCdbl(objD.SNF_R), 2)
                            gvWeighment.Rows(0).Cells(colFATValue).Value = Math.Round((clsCommon.myCdbl(gvWeighment.Rows(0).Cells(colFATKG).Value) * clsCommon.myCdbl(gvWeighment.Rows(0).Cells(colFATRate).Value)), 2)
                            gvWeighment.Rows(0).Cells(colSNFValue).Value = Math.Round((clsCommon.myCdbl(gvWeighment.Rows(0).Cells(colSNFKG).Value) * clsCommon.myCdbl(gvWeighment.Rows(0).Cells(colSNFRate).Value)), 2)
                            gvWeighment.Rows(0).Cells(colRcptAmt).Value = Math.Round((clsCommon.myCdbl(gvWeighment.Rows(0).Cells(colFATValue).Value) + clsCommon.myCdbl(gvWeighment.Rows(0).Cells(colSNFValue).Value)), 2)
                        Else
                            For ii As Integer = 0 To gvWeighment.Rows.Count - 1
                                gvWeighment.Rows(ii).Cells(colFATRate).Value = Math.Round(clsCommon.myCdbl(objD.arr(ii).FAT_Rate), 2)
                                gvWeighment.Rows(ii).Cells(colSNFRate).Value = Math.Round(clsCommon.myCdbl(objD.arr(ii).SNF_Rate), 2)
                                gvWeighment.Rows(ii).Cells(colFATValue).Value = Math.Round((clsCommon.myCdbl(gvWeighment.Rows(ii).Cells(colFATKG).Value) * clsCommon.myCdbl(gvWeighment.Rows(ii).Cells(colFATRate).Value)), 2)
                                gvWeighment.Rows(ii).Cells(colSNFValue).Value = Math.Round((clsCommon.myCdbl(gvWeighment.Rows(ii).Cells(colSNFKG).Value) * clsCommon.myCdbl(gvWeighment.Rows(ii).Cells(colSNFRate).Value)), 2)
                                gvWeighment.Rows(ii).Cells(colRcptAmt).Value = Math.Round((clsCommon.myCdbl(gvWeighment.Rows(ii).Cells(colFATValue).Value) + clsCommon.myCdbl(gvWeighment.Rows(ii).Cells(colSNFValue).Value)), 2)
                            Next
                        End If
                    End If
                Else
                    Calculate()
                End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub Calculate()
        For ii As Integer = 0 To gvWeighment.Rows.Count - 1
            CalculateCurrentRow(ii)
        Next
    End Sub
    Sub CalculateCurrentRow(ByVal rowNo As Integer)
        Dim FatW As Double = 0
        Dim SNfW As Double = 0
        Dim FATRate As Double = 0
        Dim SNFRate As Double = 0
        Dim FATValue As Double = 0
        Dim SNfValue As Double = 0
        Dim FATRatio As Double = 0
        Dim SNFRatio As Double = 0
        Dim StdRate As Double = 0
        Dim fatKG As Double = 0
        Dim snfKG As Double = 0
        If clsCommon.myLen(fndPriceChart.Value) > 0 AndAlso clsCommon.myLen(txtTransferPrice.Value) > 0 AndAlso clsCommon.myCdbl(txtTransferPrice.Value) > 0 AndAlso (clsCommon.myCdbl(TxtFatWeightage.Text) + clsCommon.myCdbl(TxtSNFWeightage.Text)) = 100 AndAlso clsCommon.myCdbl(txtSNFPercentage.Text) > 0 AndAlso clsCommon.myCdbl(txtfatPercentage.Text) > 0 Then
            Try
                FatW = clsCommon.myCdbl(TxtFatWeightage.Text)
                SNfW = clsCommon.myCdbl(TxtSNFWeightage.Text)
                FATRatio = clsCommon.myCdbl(txtfatPercentage.Text)
                SNFRatio = clsCommon.myCdbl(txtSNFPercentage.Text)
                fatKG = clsCommon.myCdbl(gvWeighment.Rows(rowNo).Cells(colFATKG).Value)
                snfKG = clsCommon.myCdbl(gvWeighment.Rows(rowNo).Cells(colSNFKG).Value)
                FATRate = Math.Round(clsCommon.myCdbl(txtTransferPrice.Value) * FatW / FATRatio, 2)
                SNFRate = Math.Round(clsCommon.myCdbl(txtTransferPrice.Value) * SNfW / SNFRatio, 2)
                FATValue = Math.Round(fatKG * FATRate, 2)
                SNfValue = Math.Round(snfKG * SNFRate, 2)

                gvWeighment.Rows(rowNo).Cells(colFATRate).Value = Math.Round(FATRate, 2)
                gvWeighment.Rows(rowNo).Cells(colSNFRate).Value = Math.Round(SNFRate, 2)
                gvWeighment.Rows(rowNo).Cells(colFATValue).Value = Math.Round(FATValue, 2)
                gvWeighment.Rows(rowNo).Cells(colSNFValue).Value = Math.Round(SNfValue, 2)
                gvWeighment.Rows(rowNo).Cells(colRcptAmt).Value = Math.Round(FATValue + SNfValue, 2)
            Catch ex As Exception
            End Try
        End If
    End Sub
    Sub SaveData(ByVal isPost As Boolean, ByVal AutoEntry As Boolean)
        Try
            If allowToSave(AutoEntry) Then
                Dim trans As SqlTransaction = Nothing
                obj = New clsMilkTransferInReturn()
                If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                    obj.isNewEntry = True
                Else
                    obj.isNewEntry = False
                End If
                trans = clsDBFuncationality.GetTransactin()
                Dim dt As Date = clsCommon.GETSERVERDATE(trans, "dd/MMM/yyyy hh:mm:ss tt")
                If obj.isNewEntry Then
                    If chkJobWork.Checked Then
                        obj.Receipt_Challan_Return_No = clsERPFuncationality.GetNextCode(trans, dtpRcptDateAndTime.Value, clsDocType.MilkTransferInReturn, clsDocTransactionType.MCCProcJobWorkOutward, clsCommon.myCstr(txtSubLocation.Value))
                    Else
                        obj.Receipt_Challan_Return_No = clsERPFuncationality.GetNextCode(trans, dtpRcptDateAndTime.Value, clsDocType.MilkTransferInReturn, clsDocTransactionType.NA, clsCommon.myCstr(txtMccPlantCode.Value))
                    End If
                    If clsCommon.myLen(obj.Receipt_Challan_Return_No) <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Error in Receipt Challan  No genertion", Me.Text)
                        Exit Sub
                    End If
                Else
                    obj.Receipt_Challan_Return_No = clsCommon.myCstr(fndRcptChalanNo.Value)
                End If
                obj.IsAgainstJobWork = IIf(chkJobWork.Checked, 1, 0)
                obj.Joblocation_Code = txtSubLocation.Value
                fndRcptChalanNo.Value = obj.Receipt_Challan_Return_No
                obj.Receipt_Challan_No = fndMilkTransferInNo.Value
                obj.Receipt_Challan_Date = clsCommon.GetPrintDate(dtpRcptDateAndTime.Value, "dd/MMM/yyyy hh:mm:ss tt")
                obj.Dispatch_Challan_No = clsCommon.myCstr(fndDispatchChallanNo.Value)
                obj.Weighment_No = clsCommon.myCstr(txtWeighmentNo.Text)
                obj.Qc_No = clsCommon.myCstr(txtQCNo.Text)
                obj.Gate_Entry_no = clsCommon.myCstr(txtgateEntryNo.Text)
                obj.location_code = clsCommon.myCstr(txtMccPlantCode.Value)
                obj.km_reading_receipt = clsCommon.myCstr(txtKmReadingRecpt.Text)
                obj.Receipt_Control_FAT = clsCommon.myCdbl(txtRcptControlSampleFAT.Text)
                obj.Receipt_Control_SNF = clsCommon.myCdbl(txtRcptControlSampleSNF.Text)
                obj.PriceCode = clsCommon.myCstr(fndPriceChart.Value)
                obj.FAT_W = clsCommon.myCdbl(TxtFatWeightage.Text)
                obj.SNF_W = clsCommon.myCdbl(TxtSNFWeightage.Text)
                obj.FAT_R = clsCommon.myCdbl(txtfatPercentage.Text)
                obj.SNF_R = clsCommon.myCdbl(txtSNFPercentage.Text)
                obj.Transfer_Price = clsCommon.myCdbl(txtTransferPrice.Value)

                If Not isPost Then
                    obj.isPosted = 0
                End If
                obj.Modified_By = objCommonVar.CurrentUserCode
                obj.Modified_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                obj.Comp_Code = objCommonVar.CurrentCompanyCode
                If obj.isNewEntry Then
                    obj.Created_By = objCommonVar.CurrentUserCode
                    obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                End If
              
                If clsMilkTransferInReturn.saveData(obj, trans) Then
                    trans.Commit()
                    If Not isPost Then
                        If AutoEntry = False Then
                            If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                                myMessages.insert()
                            Else
                                myMessages.update()
                            End If
                        End If
                    End If
                    LoadData(obj.Receipt_Challan_Return_No, NavigatorType.Current)
                    btnSave.Text = "Update"
                    fndRcptChalanNo.MyReadOnly = True
                    btnDelete.Enabled = True
                    btnPost.Enabled = True
                    Exit Sub
                End If
                clsCommon.MyMessageBoxShow(Me, "Data Not Saved ", Me.Text)
                btnSave.Text = "Save"
                btnDelete.Enabled = False
                btnPost.Enabled = False
                fndRcptChalanNo.MyReadOnly = False
                trans.Rollback()
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub postData(ByVal AutoEntry As Boolean)
        Try

            Dim qry As String = ""
            Dim msg As String = ""
            Dim dt As DataTable = Nothing
            If (myMessages.postConfirm()) Then
                If Not allowToSave(AutoEntry) Then
                    Exit Sub
                End If
                SaveData(True, AutoEntry)
                'Dim strMilkRGP As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Milk_Transfer_In from TSPL_MILK_RGP_HEAD where Milk_Transfer_In='" & fndRcptChalanNo.Value & "'"))
                'If clsCommon.myLen(strMilkRGP) = 0 Then
                '    If Not SaveMILKRGPData() Then
                '        Exit Sub
                '    End If
                'End If

                If (clsMilkTransferInReturn.postData(fndRcptChalanNo.Value, Me.Form_ID, Nothing)) Then
                    If AutoEntry = False Then
                        msg = "Successfully Posted"
                    End If
                Else
                    qry = "select No_Of_Level, LEVEL from TSPL_APPROVAL_LEVEL_SCREEN where User_Code='" + objCommonVar.CurrentUserCode + "' and Trans_Code='" + MyBase.Form_ID + "' "
                    dt = clsDBFuncationality.GetDataTable(qry)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        Dim level As String = dt.Rows(0)("LEVEL").ToString()
                        Dim NoOflevel As Integer = clsCommon.myCdbl(dt.Rows(0)("No_Of_Level"))
                        If clsCommon.CompairString(level, "Level1") = CompairStringResult.Equal Then
                            msg = "Level 1 Approval done. "
                            If NoOflevel = 1 Then
                                msg += "Successfully Posted. "
                            Else
                                msg += "Level 2 Approval Required."
                            End If
                        ElseIf clsCommon.CompairString(level, "Level2") = CompairStringResult.Equal Then
                            msg = "Level 2 Approval done. "
                            If NoOflevel = 2 Then
                                msg += "Successfully Posted "
                            Else
                                msg += "Level 3 Approval Required."
                            End If
                        Else
                            msg = "Level 3 Approval done. Successfully Posted. "
                        End If
                    End If
                End If
                common.clsCommon.MyMessageBoxShow(Me, msg, Me.Text)
                LoadData(fndRcptChalanNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadData(ByVal strRcptChallanReturnNo As String, ByVal nav As NavigatorType)
        Dim objW As clsWeighment = Nothing
        Dim objQ As clsQualityCheck = Nothing
        Dim objD As clsMccDispatch = Nothing
        Dim objGate As clsGateEntry = Nothing
        obj = clsMilkTransferInReturn.getData(strRcptChallanReturnNo, nav)
        If obj IsNot Nothing Then
            txtMccPlantCode.Enabled = False
            objW = clsWeighment.getData(obj.Weighment_No, "MccProc", NavigatorType.Current)
            objGate = clsGateEntry.getData(obj.Gate_Entry_no, "MccProc", NavigatorType.Current)
            objQ = clsQualityCheck.getData(obj.Qc_No, "MccProc", NavigatorType.Current)
            objD = clsMccDispatch.getData(obj.Dispatch_Challan_No, NavigatorType.Current)
            If objW IsNot Nothing AndAlso objQ IsNot Nothing AndAlso objD IsNot Nothing Then
                chkJobWork.Checked = IIf(obj.IsAgainstJobWork = 1, True, False)
                txtSubLocation.Value = obj.Joblocation_Code
                If clsCommon.myLen(txtSubLocation.Value) > 0 Then
                    lblSubLocation.Text = clsLocation.GetName(txtSubLocation.Value, Nothing)
                End If
                fndRcptChalanNo.Value = obj.Receipt_Challan_Return_No
                fndMilkTransferInNo.Value = obj.Receipt_Challan_No
                dtpRcptDateAndTime.Value = obj.Receipt_Challan_Date
                txtMccPlantCode.Value = obj.location_code
                lblMccPlantName.Text = clsLocation.GetName(obj.location_code, Nothing)
                fndDispatchChallanNo.Value = obj.Dispatch_Challan_No
                fndPriceChart.Value = clsCommon.myCstr(obj.PriceCode)
                TxtFatWeightage.Text = clsCommon.myCdbl(obj.FAT_W)
                TxtSNFWeightage.Text = clsCommon.myCdbl(obj.SNF_W)
                txtfatPercentage.Text = clsCommon.myCdbl(obj.FAT_R)
                txtSNFPercentage.Text = clsCommon.myCdbl(obj.SNF_R)
                If AllowBulkProcMCCwithoutTankerDispatch = 0 Then
                    dtpDispatchDateAndTime.Value = objD.Dispatch_Date
                    txtDispatchFrom.Text = objD.MCC_Code
                    lblDispatchFromDesc.Text = objD.MCC_Name
                    txtTransferPrice.Value = objD.Transfer_Price
                    txtTankerNo.Text = objD.Tanker_No
                    txtKMReadingDisp.Text = objD.Tanker_KM_Reading
                    txtDip.Text = objD.Drip_Marking
                    txtDispControlSampleFAT.Text = objD.control_sample_fat
                    txtDispControlSampleSNF.Text = objD.control_sample_snf
                Else
                    txtDispatchFrom.Text = objW.Dispatched_From_Mcc
                    lblDispatchFromDesc.Text = clsLocation.GetName(objW.Dispatched_From_Mcc, Nothing)
                    txtTransferPrice.Value = obj.Transfer_Price
                    txtTankerNo.Text = objW.Tanker_No
                    txtKMReadingDisp.Text = 0
                    txtDip.Text = 0
                End If
                txtKmReadingRecpt.Text = obj.km_reading_receipt
                txtRcptControlSampleFAT.Text = obj.Receipt_Control_FAT
                txtRcptControlSampleSNF.Text = obj.Receipt_Control_SNF
                loadBlankGvOldSeal()

                If AllowBulkProcMCCwithoutTankerDispatch = 0 Then
                    Try
                        gvOldSeal.Rows(0).Cells(colSealName).Value = clsCommon.myCstr(objD.Seal_No1)
                        gvOldSeal.Rows(1).Cells(colSealName).Value = clsCommon.myCstr(objD.Seal_No2)
                        gvOldSeal.Rows(2).Cells(colSealName).Value = clsCommon.myCstr(objD.Seal_No3)
                        gvOldSeal.Rows(3).Cells(colSealName).Value = clsCommon.myCstr(objD.Seal_No4)
                        gvOldSeal.Rows(4).Cells(colSealName).Value = clsCommon.myCstr(objD.Seal_No5)
                        gvOldSeal.Rows(5).Cells(colSealName).Value = clsCommon.myCstr(objD.Seal_No6)
                        gvOldSeal.Rows(6).Cells(colSealName).Value = clsCommon.myCstr(objD.Seal_No7)
                        gvOldSeal.Rows(7).Cells(colSealName).Value = clsCommon.myCstr(objD.Seal_No8)
                        gvOldSeal.Rows(8).Cells(colSealName).Value = clsCommon.myCstr(objD.Seal_No9)
                        gvOldSeal.Rows(9).Cells(colSealName).Value = clsCommon.myCstr(objD.Seal_No10)
                    Catch ex1 As Exception

                    End Try

                    Try
                        loadBlankGvOldSealPaper()
                        If objD.arrPaperSeal IsNot Nothing AndAlso objD.arrPaperSeal.Count > 0 Then
                            Try
                                For i As Integer = 0 To objD.arrPaperSeal.Count - 1
                                    gvOldSealPaper.Rows(i).Cells(colSealName).Value = objD.arrPaperSeal(i).Seal_No
                                Next
                            Catch exx As Exception
                            End Try
                        End If
                    Catch ex2 As Exception

                    End Try
                End If


                txtWeighmentNo.Text = obj.Weighment_No
                dtpWeighmentDate.Value = objW.Weighment_Date
                txtDipW.Text = objW.Dip_Value
                loadBlankWeighment()
                gvWeighment.Rows(0).Cells(colItemCode).Value = objW.Item_Code
                gvWeighment.Rows(0).Cells(colItemDesc).Value = objW.Item_Desc
                gvWeighment.Rows(0).Cells(colUOM).Value = clsItemMaster.GetStockUnit(objW.Item_Code, Nothing)
                gvWeighment.Rows(0).Cells(colHSN).Value = clsItemMaster.GetItemHSNCode(objW.Item_Code, Nothing)
                If AllowBulkProcMCCwithoutTankerDispatch = 0 Then
                    gvWeighment.Rows(0).Cells(colGrossWeightDisp).Value = objD.Gross_Weight
                    gvWeighment.Rows(0).Cells(colNetWeightDisp).Value = objD.Net_Qty
                    gvWeighment.Rows(0).Cells(colTareWeightDisp).Value = objD.Tare_Weight
                End If

                txtgateEntryNo.Text = obj.Gate_Entry_no
                gvWeighment.Rows(0).Cells(colGrossWeightRcpt).Value = objW.Gross_Weight
                gvWeighment.Rows(0).Cells(colNetWeightRcpt).Value = objW.Net_Weight
                gvWeighment.Rows(0).Cells(colTareWeightRcpt).Value = objW.Tare_Weight
                gvWeighment.Rows(0).Cells("colSilo").Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Sub_location_Code  from TSPL_MILK_UNLOADING  where Gate_Entry_No='" & obj.Gate_Entry_no & "'"))
                gvWeighment.Rows(0).Cells("colSiloDesc").Value = clsCommon.myCstr(clsLocation.GetName(gvWeighment.Rows(0).Cells("colSilo").Value, Nothing))
                gvWeighment.Rows(0).Cells(colFATPer).Value = objW.fat_per
                'gvWeighment.Rows(0).Cells(colFATKG).Value = clsCommon.myCdbl(objW.Net_Weight) * clsCommon.myCdbl(objW.fat_per) / 100
                gvWeighment.Rows(0).Cells(colSNFPer).Value = objW.snf_Per
                'gvWeighment.Rows(0).Cells(colSNFKG).Value = clsCommon.myCdbl(objW.Net_Weight) * clsCommon.myCdbl(objW.snf_Per) / 100
                txtQCNo.Text = obj.Qc_No
                dtpQcInDate.Value = objQ.QC_In_Date_Time
                dtpQCOutDate.Value = objQ.QC_Out_Date_Time
                If MCCChamberwise = 1 Then
                    If objW.Arr IsNot Nothing AndAlso objW.Arr.Count > 0 Then
                        gvWeighment.Rows.Clear()
                        Dim intCount As Integer = 0
                        For Each objTr As clsWeighmentChemberNoDetails In objW.Arr
                            gvWeighment.Rows.AddNew()
                            gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colSLNo).Value = objTr.Line_No
                            gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colItemCode).Value = objTr.Item_Code
                            gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colChamberDesc).Value = objTr.Chamber_Desc
                            gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colItemDesc).Value = clsIntimation.getItemName(objTr.Item_Code, Nothing)
                            gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colUOM).Value = clsItemMaster.GetStockUnit(objTr.Item_Code, Nothing)
                            gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colHSN).Value = clsItemMaster.GetItemHSNCode(objTr.Item_Code, Nothing)
                            gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colGrossWeightRcpt).Value = objTr.Gross_Weight
                            gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colNetWeightRcpt).Value = objTr.Net_Weight
                            gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colTareWeightRcpt).Value = objTr.Tare_Weight
                            gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells("colSilo").Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Sub_location_Code  from TSPL_MILK_UNLOADING  where Gate_Entry_No='" & objW.Gate_Entry_No & "'"))
                            gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells("colSiloDesc").Value = clsCommon.myCstr(clsLocation.GetName(gvWeighment.Rows(0).Cells("colSilo").Value, Nothing))
                            gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colFATPer).Value = objTr.fat_per
                            gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colSNFPer).Value = objTr.snf_Per

                            gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colGrossWeightDisp).Value = 0
                            If AllowBulkProcMCCwithoutTankerDispatch = 0 Then
                                gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colNetWeightDisp).Value = objD.arr(intCount).Qty_KG
                            End If
                            gvWeighment.Rows(gvWeighment.Rows.Count - 1).Cells(colTareWeightDisp).Value = 0
                            intCount += 1
                        Next
                    End If
                End If
                If objQ.arrQcParam IsNot Nothing Then
                    If MCCChamberwise = 0 Then
                        For i As Integer = 0 To objQ.arrQcParam.Count - 1
                            Try
                                gvParam.Rows(0).Cells(objQ.arrQcParam(i).Param_Field_Code).Value = objQ.arrQcParam(i).Param_Field_Value
                                If clsCommon.CompairString(objQ.arrQcParam(i).Param_Type, "FAT") = CompairStringResult.Equal Then
                                    gvWeighment.Rows(0).Cells(colFATPerQC).Value = clsCommon.myCdbl(objQ.arrQcParam(i).Param_Field_Value)
                                    gvWeighment.Rows(0).Cells(colFATKG).Value = clsCommon.myCdbl(objW.Net_Weight) * clsCommon.myCdbl(objQ.arrQcParam(i).Param_Field_Value) / 100
                                End If
                                If clsCommon.CompairString(objQ.arrQcParam(i).Param_Type, "SNF") = CompairStringResult.Equal Then
                                    gvWeighment.Rows(0).Cells(colSNFPerQC).Value = clsCommon.myCdbl(objQ.arrQcParam(i).Param_Field_Value)
                                    gvWeighment.Rows(0).Cells(colSNFKG).Value = clsCommon.myCdbl(objW.Net_Weight) * clsCommon.myCdbl(objQ.arrQcParam(i).Param_Field_Value) / 100
                                End If
                            Catch exxx As Exception
                            End Try
                        Next
                    Else
                        For i As Integer = 0 To objQ.arrQcParam.Count - 1
                            Try
                                If clsCommon.CompairString(objQ.arrQcParam(i).Param_Type, "FAT") = CompairStringResult.Equal Then
                                    gvWeighment.Rows(objQ.arrQcParam(i).LINE_NO - 1).Cells(colFATKG).Value = clsCommon.myCdbl(objW.Arr(objQ.arrQcParam(i).LINE_NO - 1).Net_Weight) * clsCommon.myCdbl(objQ.arrQcParam(i).Param_Field_Value) / 100
                                    gvWeighment.Rows(objQ.arrQcParam(i).LINE_NO - 1).Cells(colFATPerQC).Value = clsCommon.myCdbl(objQ.arrQcParam(i).Param_Field_Value)
                                End If
                                If clsCommon.CompairString(objQ.arrQcParam(i).Param_Type, "SNF") = CompairStringResult.Equal Then
                                    gvWeighment.Rows(objQ.arrQcParam(i).LINE_NO - 1).Cells(colSNFKG).Value = clsCommon.myCdbl(objW.Arr(objQ.arrQcParam(i).LINE_NO - 1).Net_Weight) * clsCommon.myCdbl(objQ.arrQcParam(i).Param_Field_Value) / 100
                                    gvWeighment.Rows(objQ.arrQcParam(i).LINE_NO - 1).Cells(colSNFPerQC).Value = clsCommon.myCdbl(objQ.arrQcParam(i).Param_Field_Value)
                                End If
                                gvParam.Rows(objQ.arrQcParam(i).LINE_NO - 1).Cells(objQ.arrQcParam(i).Param_Field_Code).Value = objQ.arrQcParam(i).Param_Field_Value
                            Catch exx As Exception
                            End Try
                        Next
                    End If
                End If

                If AllowBulkProcMCCwithoutTankerDispatch = 0 Then
                    If objD IsNot Nothing Then
                        If MCCChamberwise = 0 Then
                            gvWeighment.Rows(0).Cells(colFATRate).Value = Math.Round(clsCommon.myCdbl(objD.FAT_RATE), 2)
                            gvWeighment.Rows(0).Cells(colSNFRate).Value = Math.Round(clsCommon.myCdbl(objD.SNF_RATE), 2)
                            gvWeighment.Rows(0).Cells(colFATValue).Value = Math.Round((clsCommon.myCdbl(gvWeighment.Rows(0).Cells(colFATKG).Value) * clsCommon.myCdbl(gvWeighment.Rows(0).Cells(colFATRate).Value)), 2)
                            gvWeighment.Rows(0).Cells(colSNFValue).Value = Math.Round((clsCommon.myCdbl(gvWeighment.Rows(0).Cells(colSNFKG).Value) * clsCommon.myCdbl(gvWeighment.Rows(0).Cells(colSNFRate).Value)), 2)
                            ' gvWeighment.Rows(0).Cells(colRcptAmt).Value = Math.Round((clsCommon.myCdbl(gvWeighment.Rows(0).Cells(colFATValue).Value) + clsCommon.myCdbl(gvWeighment.Rows(0).Cells(colSNFValue).Value)), 2)
                            gvWeighment.Rows(0).Cells(colRcptAmt).Value = Math.Round(((clsCommon.myCdbl(gvWeighment.Rows(0).Cells(colFATKG).Value) * clsCommon.myCdbl(gvWeighment.Rows(0).Cells(colFATRate).Value)) + (clsCommon.myCdbl(gvWeighment.Rows(0).Cells(colSNFKG).Value) * clsCommon.myCdbl(gvWeighment.Rows(0).Cells(colSNFRate).Value))), 2)
                        Else
                            For ii As Integer = 0 To gvWeighment.Rows.Count - 1
                                gvWeighment.Rows(ii).Cells(colFATRate).Value = Math.Round(clsCommon.myCdbl(objD.arr(ii).FAT_Rate), 2)
                                gvWeighment.Rows(ii).Cells(colSNFRate).Value = Math.Round(clsCommon.myCdbl(objD.arr(ii).SNF_Rate), 2)
                                gvWeighment.Rows(ii).Cells(colFATValue).Value = Math.Round((clsCommon.myCdbl(gvWeighment.Rows(ii).Cells(colFATKG).Value) * clsCommon.myCdbl(gvWeighment.Rows(ii).Cells(colFATRate).Value)), 2)
                                gvWeighment.Rows(ii).Cells(colSNFValue).Value = Math.Round((clsCommon.myCdbl(gvWeighment.Rows(ii).Cells(colSNFKG).Value) * clsCommon.myCdbl(gvWeighment.Rows(ii).Cells(colSNFRate).Value)), 2)
                                gvWeighment.Rows(ii).Cells(colRcptAmt).Value = Math.Round((clsCommon.myCdbl(gvWeighment.Rows(ii).Cells(colFATValue).Value) + clsCommon.myCdbl(gvWeighment.Rows(ii).Cells(colSNFValue).Value)), 2)
                            Next
                        End If
                    End If
                Else
                    Calculate()
                End If

                If obj.isPosted = 1 Then
                    lblPending.Status = ERPTransactionStatus.Approved
                    btnSave.Enabled = False
                    btnDelete.Enabled = False
                    btnPost.Enabled = False
                    btnPrint.Enabled = True
                Else
                    lblPending.Status = ERPTransactionStatus.Pending
                    btnSave.Enabled = True
                    btnDelete.Enabled = True
                    btnPost.Enabled = True
                    btnPrint.Enabled = True
                End If
                btnSave.Text = "Update"
            Else
                reset()
            End If
        End If
    End Sub

    Sub reset()
        fndMilkTransferInNo.Value = ""
        txtSubLocation.Value = ""
        lblSubLocation.Text = ""
        chkJobWork.Checked = False
        fndPriceChart.Value = ""
        TxtFatWeightage.Text = ""
        TxtSNFWeightage.Text = ""
        txtfatPercentage.Text = ""
        txtSNFPercentage.Text = ""
        txtMccPlantCode.Enabled = True
        chkNewSealNo.Visible = False
        txtgateEntryNo.Text = ""
        fndRcptChalanNo.Value = ""
        fndRcptChalanNo.MyReadOnly = False
        lblPending.Status = ERPTransactionStatus.Pending
        Dim dt As Date = clsCommon.GETSERVERDATE(Nothing, "dd/MMM/yyyy hh:mm:ss tt")
        dtpRcptDateAndTime.Value = dt
        txtMccPlantCode.Value = clsGateEntry.getUsersDefaultLocation()
        lblMccPlantName.Text = clsLocation.GetName(txtMccPlantCode.Value, Nothing)
        fndDispatchChallanNo.Value = ""
        dtpDispatchDateAndTime.Value = dt
        txtDispatchFrom.Text = ""
        lblDispatchFromDesc.Text = ""
        txtTankerNo.Text = ""
        txtKMReadingDisp.Text = ""
        txtDip.Text = ""
        txtKmReadingRecpt.Text = ""
        txtTransferPrice.Value = "0"
        chkNewSealNo.Checked = False
        txtWeighmentNo.Text = ""
        dtpWeighmentDate.Value = dt
        txtDipW.Text = ""
        txtQCNo.Text = ""
        dtpQcInDate.Value = dt
        dtpQCOutDate.Value = dt
        Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, Nothing)
        If DateTime = "1" Then
            dtpRcptDateAndTime.CustomFormat = "dd/MM/yyyy hh:mm tt"
            dtpDispatchDateAndTime.CustomFormat = "dd/MM/yyyy hh:mm tt"
            dtpWeighmentDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
            dtpQcInDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
            dtpQCOutDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Else
            dtpRcptDateAndTime.CustomFormat = "dd/MM/yyyy"
            dtpDispatchDateAndTime.CustomFormat = "dd/MM/yyyy"
            dtpWeighmentDate.CustomFormat = "dd/MM/yyyy"
            dtpQcInDate.CustomFormat = "dd/MM/yyyy"
            dtpQCOutDate.CustomFormat = "dd/MM/yyyy"
        End If
        loadBlankGvOldSeal()
        'loadBlankGvNewSeal()
        loadBlankGvOldSealPaper()
        'loadBlankGvNewSealPaper()

        loadBlankWeighment()
        loadBlankGvParam()
        gvNewSeal.Enabled = False
        gvNewSealPaper.Enabled = False
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = False
        btnPrint.Enabled = False
        btnPost.Enabled = False
        btnReverse.Visible = False
        RadPageView1.SelectedPage = RadPageViewPage1
        txtRcptControlSampleFAT.Text = ""
        txtRcptControlSampleSNF.Text = ""
        txtDispControlSampleFAT.Text = ""
        txtDispControlSampleSNF.Text = ""
        FindAndRestoreGridLayout(Me)
        FindAndSetTabStopFalse(Me)
    End Sub
    Public Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmMilkTransferIn)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPost.Visible = MyBase.isPostFlag
        btnPrint.Visible = MyBase.isPrintFlag
        btnReverse.Visible = False
        'If MyBase.isReverse Then
        '    btnReverse.Enabled = True
        'Else
        '    btnReverse.Enabled = False

        'End If
        'btnReverse.Enabled = False

    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
        GC.Collect()
    End Sub
    Sub loadBlankGvOldSeal()
        gvOldSeal.Rows.Clear()
        gvOldSeal.Columns.Clear()
        gvOldSeal.DataSource = Nothing

        gvOldSeal.Columns.Add(colSLNo, "SL. No.")
        gvOldSeal.Columns(colSLNo).ReadOnly = True
        gvOldSeal.Columns(colSLNo).Width = 60

        gvOldSeal.Columns.Add(colSealName, "Seal Desc")
        gvOldSeal.Columns(colSealName).ReadOnly = True
        gvOldSeal.Columns(colSealName).Width = 180



        For i As Integer = 0 To 9
            gvOldSeal.Rows.AddNew()
            gvOldSeal.Rows(i).Cells(colSLNo).Value = (i + 1)
            gvOldSeal.Rows(i).Cells(colSealName).Value = ""
            '    gvOldSeal.Rows(i).Cells(colSealValue).Value = ""
        Next
        gvOldSeal.AllowAddNewRow = False
        gvOldSeal.AllowDeleteRow = False
        gvOldSeal.AllowRowReorder = False
        gvOldSeal.ShowGroupPanel = False
        gvOldSeal.EnableFiltering = False
        gvOldSeal.EnableSorting = False
        gvOldSeal.EnableGrouping = False
    End Sub

    Sub loadBlankGvOldSealPaper()
        gvOldSealPaper.Rows.Clear()
        gvOldSealPaper.Columns.Clear()
        gvOldSealPaper.DataSource = Nothing

        gvOldSealPaper.Columns.Add(colSLNo, "SL. No.")
        gvOldSealPaper.Columns(colSLNo).ReadOnly = True
        gvOldSealPaper.Columns(colSLNo).Width = 60

        gvOldSealPaper.Columns.Add(colSealName, "Seal Desc")
        gvOldSealPaper.Columns(colSealName).ReadOnly = True
        gvOldSealPaper.Columns(colSealName).Width = 180



        For i As Integer = 0 To 9
            gvOldSealPaper.Rows.AddNew()
            gvOldSealPaper.Rows(i).Cells(colSLNo).Value = (i + 1)
            gvOldSealPaper.Rows(i).Cells(colSealName).Value = ""
            '    GvOldSealPaper.Rows(i).Cells(colSealValue).Value = ""
        Next
        gvOldSealPaper.AllowAddNewRow = False
        gvOldSealPaper.AllowDeleteRow = False
        gvOldSealPaper.AllowRowReorder = False
        gvOldSealPaper.ShowGroupPanel = False
        gvOldSealPaper.EnableFiltering = False
        gvOldSealPaper.EnableSorting = False
        gvOldSealPaper.EnableGrouping = False
    End Sub
    Sub loadBlankGvNewSeal()
        gvNewSeal.Rows.Clear()
        gvNewSeal.Columns.Clear()
        gvNewSeal.DataSource = Nothing

        gvNewSeal.Columns.Add(colSLNo, "SL. No.")
        gvNewSeal.Columns(colSLNo).ReadOnly = True
        gvNewSeal.Columns(colSLNo).Width = 60

        gvNewSeal.Columns.Add(colSealName, "Seal Desc")
        gvNewSeal.Columns(colSealName).ReadOnly = False
        gvNewSeal.Columns(colSealName).HeaderImage = My.Resources.search4
        gvNewSeal.Columns(colSealName).TextImageRelation = TextImageRelation.TextBeforeImage
        gvNewSeal.Columns(colSealName).Width = 180



        For i As Integer = 0 To 9
            gvNewSeal.Rows.AddNew()
            gvNewSeal.Rows(i).Cells(colSLNo).Value = (i + 1)
            gvNewSeal.Rows(i).Cells(colSealName).Value = ""
            'gvNewSeal.Rows(i).Cells(colSealValue).Value = ""
        Next
        gvNewSeal.AllowAddNewRow = False
        gvNewSeal.AllowDeleteRow = False
        gvNewSeal.AllowRowReorder = False
        gvNewSeal.ShowGroupPanel = False
        gvNewSeal.EnableFiltering = False
        gvNewSeal.EnableSorting = False
        gvNewSeal.EnableGrouping = False
    End Sub
    Sub loadBlankGvNewSealPaper()
        gvNewSealPaper.Rows.Clear()
        gvNewSealPaper.Columns.Clear()
        gvNewSealPaper.DataSource = Nothing

        gvNewSealPaper.Columns.Add(colSLNo, "SL. No.")
        gvNewSealPaper.Columns(colSLNo).ReadOnly = True
        gvNewSealPaper.Columns(colSLNo).Width = 60

        gvNewSealPaper.Columns.Add(colSealName, "Seal Desc")
        gvNewSealPaper.Columns(colSealName).ReadOnly = False
        gvNewSealPaper.Columns(colSealName).HeaderImage = My.Resources.search4
        gvNewSealPaper.Columns(colSealName).TextImageRelation = TextImageRelation.TextBeforeImage
        gvNewSealPaper.Columns(colSealName).Width = 180



        For i As Integer = 0 To 9
            gvNewSealPaper.Rows.AddNew()
            gvNewSealPaper.Rows(i).Cells(colSLNo).Value = (i + 1)
            gvNewSealPaper.Rows(i).Cells(colSealName).Value = ""
            'GvNewSealPaper.Rows(i).Cells(colSealValue).Value = ""
        Next
        gvNewSealPaper.AllowAddNewRow = False
        gvNewSealPaper.AllowDeleteRow = False
        gvNewSealPaper.AllowRowReorder = False
        gvNewSealPaper.ShowGroupPanel = False
        gvNewSealPaper.EnableFiltering = False
        gvNewSealPaper.EnableSorting = False
        gvNewSealPaper.EnableGrouping = False
    End Sub
    Sub loadBlankWeighment()
        gvWeighment.Rows.Clear()
        gvWeighment.Columns.Clear()
        gvWeighment.DataSource = Nothing

        gvWeighment.Columns.Add(colSLNo, "SL. No.")
        gvWeighment.Columns(colSLNo).ReadOnly = True
        gvWeighment.Columns(colSLNo).Width = 60

        gvWeighment.Columns.Add(colItemCode, "Item Code")
        gvWeighment.Columns(colItemCode).ReadOnly = True
        gvWeighment.Columns(colItemCode).Width = 100


        gvWeighment.Columns.Add(colItemDesc, "Item Desc")
        gvWeighment.Columns(colItemDesc).ReadOnly = True
        gvWeighment.Columns(colItemDesc).Width = 180

        gvWeighment.Columns.Add(colHSN, "HSN Code")
        gvWeighment.Columns(colHSN).ReadOnly = True
        gvWeighment.Columns(colHSN).Width = 100

        gvWeighment.Columns.Add(colChamberDesc, "Chamber Desc")
        gvWeighment.Columns(colChamberDesc).Width = 150
        gvWeighment.Columns(colChamberDesc).ReadOnly = True
        gvWeighment.Columns(colChamberDesc).IsVisible = False

        gvWeighment.Columns.Add(colUOM, "UOM")
        gvWeighment.Columns(colUOM).ReadOnly = True
        gvWeighment.Columns(colUOM).Width = 100

        gvWeighment.Columns.Add("colSilo", "Silo")
        gvWeighment.Columns("colSilo").ReadOnly = True
        gvWeighment.Columns("colSilo").Width = 100

        gvWeighment.Columns.Add("colSiloDesc", "Silo Desc")
        gvWeighment.Columns("colSiloDesc").ReadOnly = True
        gvWeighment.Columns("colSiloDesc").Width = 100


        gvWeighment.Columns.Add(colGrossWeightDisp, "Dispatch-Gross Qty")
        gvWeighment.Columns(colGrossWeightDisp).ReadOnly = True
        gvWeighment.Columns(colGrossWeightDisp).IsVisible = False
        gvWeighment.Columns(colGrossWeightDisp).Width = 180

        gvWeighment.Columns.Add(colTareWeightDisp, "Dispatch-Tare Qty")
        gvWeighment.Columns(colTareWeightDisp).ReadOnly = True
        gvWeighment.Columns(colTareWeightDisp).IsVisible = False
        gvWeighment.Columns(colTareWeightDisp).Width = 180

        gvWeighment.Columns.Add(colNetWeightDisp, "Dispatch-Net Qty")
        gvWeighment.Columns(colNetWeightDisp).ReadOnly = True
        gvWeighment.Columns(colNetWeightDisp).Width = 180

        gvWeighment.Columns.Add(colGrossWeightRcpt, "Rcpt-Gross Qty")
        gvWeighment.Columns(colGrossWeightRcpt).ReadOnly = True
        gvWeighment.Columns(colGrossWeightRcpt).Width = 180

        gvWeighment.Columns.Add(colTareWeightRcpt, "Rcpt-Tare Qty")
        gvWeighment.Columns(colTareWeightRcpt).ReadOnly = True
        gvWeighment.Columns(colTareWeightRcpt).Width = 180

        gvWeighment.Columns.Add(colNetWeightRcpt, "Rcpt-Net Qty")
        gvWeighment.Columns(colNetWeightRcpt).ReadOnly = True
        gvWeighment.Columns(colNetWeightRcpt).Width = 180


        gvWeighment.Columns.Add(colFATPer, "Disp-FAT(%)")
        gvWeighment.Columns(colFATPer).ReadOnly = True
        gvWeighment.Columns(colFATPer).Width = 180

        gvWeighment.Columns.Add(colSNFPer, "Disp-SNF(%)")
        gvWeighment.Columns(colSNFPer).ReadOnly = True
        gvWeighment.Columns(colSNFPer).Width = 180

        gvWeighment.Columns.Add(colFATPerQC, "Rcpt-FAT(%)")
        gvWeighment.Columns(colFATPerQC).ReadOnly = True
        gvWeighment.Columns(colFATPerQC).Width = 180

        gvWeighment.Columns.Add(colSNFPerQC, "Rcpt-SNF(%)")
        gvWeighment.Columns(colSNFPerQC).ReadOnly = True
        gvWeighment.Columns(colSNFPerQC).Width = 180

        gvWeighment.Columns.Add(colFATKG, "FAT(Kg)")
        gvWeighment.Columns(colFATKG).ReadOnly = True
        gvWeighment.Columns(colFATKG).Width = 180

        gvWeighment.Columns.Add(colSNFKG, "SNF(Kg)")
        gvWeighment.Columns(colSNFKG).ReadOnly = True
        gvWeighment.Columns(colSNFKG).Width = 180



        gvWeighment.Columns.Add(colFATRate, "FAT Rate")
        gvWeighment.Columns(colFATRate).ReadOnly = True
        gvWeighment.Columns(colFATRate).Width = 180

        gvWeighment.Columns.Add(colSNFRate, "SNF Rate")
        gvWeighment.Columns(colSNFRate).ReadOnly = True
        gvWeighment.Columns(colSNFRate).Width = 180

        gvWeighment.Columns.Add(colFATValue, "Fat Value")
        gvWeighment.Columns(colFATValue).ReadOnly = True
        gvWeighment.Columns(colFATValue).Width = 180

        gvWeighment.Columns.Add(colSNFValue, "SNF Value")
        gvWeighment.Columns(colSNFValue).ReadOnly = True
        gvWeighment.Columns(colSNFValue).Width = 180


        gvWeighment.Columns.Add(colRcptAmt, "Net Amount")
        gvWeighment.Columns(colRcptAmt).ReadOnly = True
        gvWeighment.Columns(colRcptAmt).Width = 180


        gvWeighment.Rows.AddNew()
        gvWeighment.Rows(0).Cells(colSLNo).Value = "1"

        gvWeighment.AllowAddNewRow = False
        gvWeighment.AllowDeleteRow = False
        gvWeighment.AllowRowReorder = False
        gvWeighment.ShowGroupPanel = False
        gvWeighment.EnableFiltering = False
        gvWeighment.EnableSorting = False
        gvWeighment.EnableGrouping = False
        gvWeighment.AllowColumnChooser = True
    End Sub
    Sub loadBlankGvParam()
        Dim pFields As Boolean = True
        Dim gridWidth As Integer = 60
        Dim whrCls As String = String.Empty
        If clsERPFuncationality.isCurrentUserMCC Then
            whrCls = " where Param_for='MCC' or Param_for='BOTH'"
        Else
            whrCls = " where Param_for='PLANT' or Param_for='BOTH'"
        End If
        Dim qry As String = " select *,case when Type='NA' then 1 when  Type='FAT' then 2 when Type='SNF' then 3 when Type='CLR' then 4 when Type='OTHERS' then 5 else 6 end as Ordering from tspl_parameter_Master " & whrCls & " order by Ordering "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 AndAlso dt IsNot Nothing Then
            pFields = True
        Else
            pFields = False
        End If
        gvParam.Rows.Clear()
        gvParam.Columns.Clear()
        gvParam.DataSource = Nothing
        gvParam.Columns.Add("colSLNO", "SL. No.")
        gvParam.Columns("colSLNO").Width = 60
        gvParam.Columns("colSLNO").ReadOnly = True
        gvParam.Columns("colSLNO").Tag = "SLNO"

        If pFields Then
            For i As Integer = 0 To dt.Rows.Count() - 1
                gvParam.Columns.Add(dt.Rows(i)("Code"), dt.Rows(i)("Description"))
                gvParam.Columns(dt.Rows(i)("Code")).Width = 120
                gvParam.Columns(dt.Rows(i)("Code")).ReadOnly = True
                gvParam.Columns(dt.Rows(i)("Code")).Tag = dt.Rows(i)("Type")
            Next


        End If
        If MCCChamberwise = 1 Then
            Dim ii As Integer = gvWeighment.Rows.Count
            Dim intCount As Integer = 0
            For intCount = 0 To ii - 1
                gvParam.Rows.AddNew()
                gvParam.Rows(intCount).Cells("colSLNO").Value = intCount + 1
            Next
        Else
            gvParam.Rows.AddNew()
            gvParam.Rows(0).Cells("colSLNO").Value = "1"
        End If


        gvParam.AllowAddNewRow = False
        gvParam.AllowDeleteRow = False
        gvParam.AllowRowReorder = False
        gvParam.ShowGroupPanel = False
        gvParam.EnableFiltering = False
        gvParam.EnableSorting = False
        gvParam.EnableGrouping = False
    End Sub

    Private Sub FrmMilkTransferIn_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnReset.Enabled Then
            btnReset_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            btnSave_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled AndAlso MyBase.isPostFlag Then
            btnPost_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled AndAlso MyBase.isDeleteFlag Then
            btnDelete_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            btnClose_Click(sender, e)
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
                clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
                'MessageBox.Show("You are not authorized to perform this action.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub



    Private Sub FrmMilkTransferIn_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Panel3.Enabled = False
        AllowBulkProcMCCwithoutTankerDispatch = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowBulkProcMCCwithoutTankerDispatch, clsFixedParameterCode.AllowBulkProcMCCwithoutTankerDispatch, Nothing))
        SetUserMgmtNew()
        reset()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D To Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C To Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N For New")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P to Post the Transaction")
        If clsCommon.myLen(strDocNo) > 0 Then
            fndRcptChalanNo.Value = strDocNo
            LoadData(fndRcptChalanNo.Value, NavigatorType.Current)
        End If

        If clsCommon.myLen(Me.Tag) > 0 Then
            fndRcptChalanNo.Value = clsCommon.myCstr(Me.Tag)
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        MCCChamberwise = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsChamberWiseTanker, clsFixedParameterCode.IsChamberWiseTanker, Nothing))
        AllowJobWorkonGateEntryBulkProc = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowJobWorkonGateEntryBulkProc, clsFixedParameterCode.AllowJobWorkonGateEntryBulkProc, Nothing))
        AllowReverseUnpost = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowReverseUnpost, clsFixedParameterCode.AllowReverseUnpost, Nothing))
        TankerFromMaster = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GateEntryTankerFromTankerMaster, clsFixedParameterCode.GateEntryTankerFromTankerMaster, Nothing))
        If AllowBulkProcMCCwithoutTankerDispatch = 1 Then
            txtTransferPrice.ReadOnly = False
            txtDip.ReadOnly = False
            GroupBox5.Visible = True
        Else
            txtTransferPrice.ReadOnly = True
            txtDip.ReadOnly = True
            GroupBox5.Visible = False
        End If
        If AllowJobWorkonGateEntryBulkProc = 1 Then
            Panel3.Visible = True
        Else
            Panel3.Visible = False
        End If
    End Sub
    Function allowToSave(ByVal AutoEntry As Boolean) As Boolean
        Try
            If AllowFutureDateTransaction(dtpRcptDateAndTime.Value, Nothing) = False Then
                dtpRcptDateAndTime.Focus()
                Return False
            End If
            If clsCommon.myLen(fndMilkTransferInNo.Value) <= 0 Then
                Throw New Exception("Please select Milk Transfer In No")
            End If
            If clsCommon.myLen(fndDispatchChallanNo.Value) <= 0 Then
                Throw New Exception("Please select dispatch challan ")
            End If
           
            If AllowBulkProcMCCwithoutTankerDispatch = 1 Then
                If clsCommon.myCdbl(txtTransferPrice.Text) <= 0 Then
                    Throw New Exception("Please enter Transfer Price. ")
                    txtTransferPrice.Focus()
                End If
                If clsCommon.myLen(fndPriceChart.Value) <= 0 Then
                    Throw New Exception("Please Select Price Chart . ")
                    fndPriceChart.Focus()
                End If
                For ii As Integer = 0 To gvWeighment.Rows.Count - 1
                    CalculateCurrentRow(ii)
                Next
            End If
            If AutoEntry = False Then
                If clsCommon.myLen(txtKmReadingRecpt.Text) <= 0 Then
                    Throw New Exception("Please Enter KM Reading Receipt ")
                    txtKmReadingRecpt.Focus()
                End If

                If clsCommon.myCdbl(txtKmReadingRecpt.Text) <= clsCommon.myCdbl(txtKMReadingDisp.Text) Then
                    Throw New Exception("Receipt KM Reading can't be smaller or equal Dispatch KM Reading ")
                End If
            End If
            For i As Integer = 0 To gvNewSeal.Rows.Count - 2
                For j As Integer = i + 1 To gvNewSeal.Rows.Count - 1
                    If clsCommon.myLen(gvNewSeal.Rows(i).Cells(colSealName).Value) > 0 AndAlso clsCommon.myLen(gvNewSeal.Rows(j).Cells(colSealName).Value) > 0 Then
                        If clsCommon.CompairString(gvNewSeal.Rows(i).Cells(colSealName).Value, gvNewSeal.Rows(j).Cells(colSealName).Value) = CompairStringResult.Equal Then
                            Throw New Exception("Duplicate New Manual Seal No found at Row no " & (j + 1))
                        End If
                    End If
                Next
            Next

            For i As Integer = 0 To gvNewSealPaper.Rows.Count - 2
                For j As Integer = i + 1 To gvNewSealPaper.Rows.Count - 1
                    If clsCommon.myLen(gvNewSealPaper.Rows(i).Cells(colSealName).Value) > 0 AndAlso clsCommon.myLen(gvNewSealPaper.Rows(j).Cells(colSealName).Value) > 0 Then
                        If clsCommon.CompairString(gvNewSealPaper.Rows(i).Cells(colSealName).Value, gvNewSealPaper.Rows(j).Cells(colSealName).Value) = CompairStringResult.Equal Then
                            Throw New Exception("Duplicate New Paper Seal No found at Row no " & (j + 1))
                        End If
                    End If
                Next
            Next

            Dim chk As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from tspl_milk_transfer_in_Return where gate_entry_no='" & txtgateEntryNo.Text & "' and receipt_challan_Return_no <>'" & fndRcptChalanNo.Value & "' "))
            If chk > 0 Then
                Throw New Exception("The Same Tanker you have selected is Already used in other Document.")
            End If
            'Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

        Return True
    End Function

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData(False, False)
    End Sub

    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
    End Sub

    Private Sub btnReverse_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReverse.Click
        Try
            If common.clsCommon.MyMessageBoxShow(Me, "Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If clsMilkTransferInReturn.ReverseAndUnpost(fndRcptChalanNo.Value, Nothing) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    LoadData(fndRcptChalanNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        printData()



    End Sub
    ''Updateed by Preeti Gupta ticket no[BM00000004913,BM00000005040]
    Sub printData()
        Dim PrintTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToPrintTimeWithDocumentDate, clsFixedParameterCode.AllowToPrintTimeWithDocumentDate, Nothing)
        If clsCommon.myLen(fndRcptChalanNo.Value) > 0 Then
            Dim strQuery As String = " select final .*,(FAT_RATE *Fat_Kg )+(SNF_RATE *SNF_Kg ) as Amount  from(select xx.*,(xx.Net_Weight *Fat_per)/100 as Fat_Kg,(xx.Net_Weight *SNF_Per)/100 as SNF_Kg from (select TSPL_COMPANY_MASTER.Comp_Name  as Comp_name,TSPL_COMPANY_MASTER.Add1 as comp_add1 ,TSPL_COMPANY_MASTER.Add2 as comp_add2 ,TSPL_COMPANY_MASTER.Add3  as comp_add3,TSPL_COMPANY_MASTER.Tin_No as comp_tin_no,case when ISNULL(TSPL_COMPANY_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_COMPANY_MASTER.Phone1 end +  Case When   ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as  Comp_Phn ,TSPL_COMPANY_MASTER.Pincode as comp_pin_code,TSPL_STATE_MASTER.State_Name as To_Loc_State_Name,TSPL_LOCATION_MASTER.Location_Desc as To_Loc_Des ,TSPL_LOCATION_MASTER.Location_Code as To_Loc_Code ,TSPL_LOCATION_MASTER.Add1 as To_Loc_Add1,TSPL_LOCATION_MASTER.Add2 as To_Loc_ADd2,TSPL_LOCATION_MASTER.Add3 as To_Loc_Add3,TSPL_LOCATION_MASTER.TIN_No as To_Loc_Tin_No,TSPL_LOCATION_MASTER.GSTNO as To_GSTNO,TSPL_STATE_MASTER.GST_STATE_Code as To_GSTState_Code,case when ISNULL(TSPL_LOCATION_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_LOCATION_MASTER.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_LOCATION_MASTER.Phone2 Else'' End as  To_Loc_Phn ,TSPL_LOCATION_MASTER.Email as To_Loc_Email,TSPL_LOCATION_MASTER.Pin_Code as To_Loc_Pin_Code,fromLoc. Add1 as frm_ADd1,fromLoc .Add2 as frm_Loc_add2,fromLoc.Add3 as frm_Loc_add3,fromLoc .Location_Code as frm_Loc_Code,fromLoc.Location_Desc  as frm_loc_des,fromLoc .TIN_No as frm_Tin_no,fromLoc.GSTNO as frm_GSTNo,Frm_Loc.GST_STATE_Code as frm_GSTState_code,case when ISNULL(fromLoc .Phone1,'')='(+__)__________' then '' else fromLoc.Phone1 end +  Case When   ISNULL(fromLoc.Phone2,'')<>'(+__)__________' Then ', '+ fromLoc.Phone2 Else'' End as  frm_Loc_Phn,fromLoc .Pin_Code as frm_pin_code,TSPL_Weighment_Detail.Net_Weight,t_FAT .Param_Field_Value as Fat_per,t_SNF .Param_Field_Value as SNF_Per,TSPL_MCC_Dispatch_Challan.FAT_RATE ,TSPL_MCC_Dispatch_Challan.SNF_RATE , 'For FAT ' + Convert(varchar,TSPL_Bulk_Price_MASTER.Fat_Percentage) + ' & SNF ' +    Convert(varchar,TSPL_Bulk_Price_MASTER.Snf_Percentage) As    Milk_Rate ,TSPL_MILK_TRANSFER_IN.Receipt_Challan_No ,"

            If PrintTime = "1" Then
                strQuery += "TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date  as Receipt_Challan_Date"
            Else
                strQuery += "convert(varchar,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date ,103) as Receipt_Challan_Date"
            End If


            strQuery += " ,TSPL_MCC_Dispatch_Challan.Transfer_Price  as Rate,TSPL_MCC_Dispatch_Challan.Tanker_No  from TSPL_MILK_TRANSFER_IN "
            strQuery += " left outer join TSPL_COMPANY_MASTER  on TSPL_COMPANY_MASTER .Comp_Code =TSPL_MILK_TRANSFER_IN.Comp_Code "
            strQuery += "  left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER.Location_Code  =TSPL_MILK_TRANSFER_IN.location_code "
            strQuery += "  left outer join TSPL_MCC_Dispatch_Challan on TSPL_MCC_Dispatch_Challan.Chalan_NO =TSPL_MILK_TRANSFER_IN.Dispatch_Challan_No "
            strQuery += "  left outer join TSPL_LOCATION_MASTER  as fromLoc on TSPL_MCC_Dispatch_Challan.MCC_Code  =fromLoc.Location_Code "
            strQuery += "  left outer join TSPL_Weighment_Detail on TSPL_Weighment_Detail.Weighment_No =TSPL_MILK_TRANSFER_IN.Weighment_No "
            strQuery += "   Left Outer Join TSPL_Bulk_Price_MASTER On TSPL_Bulk_Price_MASTER.Price_Code      = TSPL_MCC_Dispatch_Challan.PriceCode "
            strQuery += " 		  left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE=TSPL_LOCATION_MASTER.State "
            strQuery += " left outer join TSPL_STATE_MASTER as Frm_Loc on Frm_Loc.STATE_CODE=fromLoc.State "
            strQuery += " Left Outer Join (Select TSPL_QC_Parameter_Detail.*    From TSPL_MILK_TRANSFER_IN      Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No        = TSPL_MILK_TRANSFER_IN.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'FAT') t_FAT On t_FAT.QC_No = TSPL_MILK_TRANSFER_IN.QC_No "

            strQuery += "  Left Outer Join (Select TSPL_QC_Parameter_Detail.*    From TSPL_MILK_TRANSFER_IN      Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No        = TSPL_MILK_TRANSFER_IN.QC_No And TSPL_QC_Parameter_Detail.Param_Type =   'SNF') t_SNF On t_SNF.QC_No = TSPL_MILK_TRANSFER_IN.QC_No "
            strQuery += " where  TSPL_MILK_TRANSFER_IN.Receipt_Challan_No='" & fndRcptChalanNo.Value & "'"
            strQuery += " ) as xx)as final "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQuery)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "rptMilkTransferIn", "Milk Transfer In", clsCommon.myCDate(dt.Rows(0)("Receipt_Challan_Date")))
            frmCRV = Nothing
        Else
            clsCommon.MyMessageBoxShow(Me, "Please select an invoice to print", Me.Text)
        End If
    End Sub
    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click
        postData(False)
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If clsCommon.myLen(fndRcptChalanNo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Select Receipt Challan No To delete ", Me.Text)
        Else
            'Dim isUsed As Integer = clsDBFuncationality.getSingleValue("select SUM(row_Count ) from (select COUNT(*) as row_Count from  TSPL_Weighment_Detail where gate_entry_no='" & fndGateEntryNO.Value & "' union all select COUNT(*) as row_Count from tspl_quality_check where gate_entry_no='" & fndGateEntryNO.Value & "') xx ")
            'If isUsed > 0 Then
            '    clsCommon.MyMessageBoxShow("Gate Entry No is in use")
            '    Exit Sub
            'End If
            If myMessages.deleteConfirm() Then
                If clsMilkTransferInReturn.deleteData(fndRcptChalanNo.Value, Nothing) Then
                    reset()

                    myMessages.delete()
                End If
            End If
        End If
    End Sub

    Private Sub chkNewSealNo_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkNewSealNo.ToggleStateChanged
        If chkNewSealNo.Checked Then
            gvNewSeal.Enabled = True
            gvNewSealPaper.Enabled = True
        Else
            gvNewSeal.Enabled = False
            gvNewSealPaper.Enabled = False
            loadBlankGvNewSeal()
            loadBlankGvNewSealPaper()
        End If
    End Sub

    Private Sub fndDispatchChallanNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndDispatchChallanNo._MYValidating
        Try
            If AllowBulkProcMCCwithoutTankerDispatch = 0 Then
                Dim whrCls As String = " TSPL_MCC_Dispatch_Challan.Chalan_NO in (Select Chalan_NO  from TSPL_Weighment_Detail left outer join TSPL_MCC_Dispatch_Challan on TSPL_MCC_Dispatch_Challan.Chalan_NO= TSPL_Weighment_Detail.Challan_No  WHERE TSPL_Weighment_Detail.isPosted=1 AND TSPL_Weighment_Detail.Challan_No NOT IN (Select Dispatch_Challan_No from tspl_milk_transfer_in where Dispatch_Challan_No<>'" & fndDispatchChallanNo.Value & "' ) and TSPL_MCC_Dispatch_Challan.Mcc_Or_Plant_Code='" & txtMccPlantCode.Value & "')"
                fndDispatchChallanNo.Value = clsMccDispatch.getFinder(whrCls, fndDispatchChallanNo.Value, isButtonClicked)
                loadDispatchData(fndDispatchChallanNo.Value)
            Else
                Dim qry = "Select Challan_No as [ChallanNo],Tanker_No as [TankerNo],Gate_Entry_No as [Gate Entry No],Date_And_Time as [Gate Entry Date],Dispatched_From_Mcc as MCC,location_Code as [location] from Tspl_Gate_Entry_Details"
                Dim whrCls As String = "  Gate_Entry_No not in (select TSPL_MILK_TRANSFER_IN.Gate_Entry_no from TSPL_MILK_TRANSFER_IN) and Doc_Type='MccProc' and Gate_Entry_No in (select Gate_Entry_No from TSPL_Weighment_Detail where isPosted=1) and location_Code='" & txtMccPlantCode.Value & "'"
                fndDispatchChallanNo.Value = clsCommon.ShowSelectForm("MITNKRNO", qry, "ChallanNo", whrCls, fndDispatchChallanNo.Value, "ChallanNo", isButtonClicked)
                loadDispatchData(fndDispatchChallanNo.Value)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndRcptChalanNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndRcptChalanNo._MYNavigator
        LoadData(fndRcptChalanNo.Value, NavType)
    End Sub

    Private Sub fndRcptChalanNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndRcptChalanNo._MYValidating
        Dim whrCls As String = String.Empty
        If Not clsMccMaster.isCurrentUserHO() Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrCls = " location_code in ( " & objCommonVar.strCurrUserLocations & ")"
            End If
        End If
        fndRcptChalanNo.Value = clsMilkTransferInReturn.getFinder(whrCls, fndRcptChalanNo.Value, isButtonClicked)
        If clsCommon.myLen(fndRcptChalanNo.Value) > 0 Then
            LoadData(fndRcptChalanNo.Value, NavigatorType.Current)
        Else
            reset()
        End If
    End Sub

    Private Sub txtRcptControlSampleFAT_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtRcptControlSampleFAT.Validating
        Try
            Dim fracValue As Double = 0
            fracValue = clsCommon.myCdbl(txtRcptControlSampleFAT.Text)
            fracValue = Math.Round((fracValue - CInt(fracValue)) * 100, 2)
            If CInt(fracValue) Mod 5 <> 0 AndAlso clsCommon.myCdbl(txtRcptControlSampleFAT.Text) > 0 Then
                Throw New Exception("Control Sample FAT value, must have its decimal part multiple of 5")
                txtRcptControlSampleFAT.Focus()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtRcptControlSampleSNF_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtRcptControlSampleSNF.Validating
        'Try
        '    Dim fracValue As Double = 0
        '    fracValue = clsCommon.myCdbl(txtRcptControlSampleSNF.Text)
        '    fracValue = Math.Round((fracValue - CInt(fracValue)) * 100, 2)
        '    If CInt(fracValue) Mod 5 <> 0 AndAlso clsCommon.myCdbl(txtRcptControlSampleSNF.Text) > 0 Then
        '        Throw New Exception("Control Sample SNF value, must have its decimal part multiple of 5")
        '        txtRcptControlSampleSNF.Focus()
        '    End If
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.Message)
        'End Try
    End Sub
    Function getUsedPaperSealNoOnCurrentScreen(ByVal curRow As Integer) As String
        Dim strSealNo As String = String.Empty
        For i As Integer = 0 To gvNewSeal.Rows.Count - 1
            If clsCommon.myLen(gvNewSeal.Rows(i).Cells(colSealName).Value) > 0 And i <> curRow Then
                strSealNo = strSealNo & "'" & clsCommon.myCstr(gvNewSeal.Rows(i).Cells(colSealName).Value) & "',"
            End If
        Next
        If clsCommon.myLen(strSealNo) > 0 Then
            strSealNo = Microsoft.VisualBasic.Left(strSealNo, Microsoft.VisualBasic.Len(strSealNo) - 1)
        End If
        If clsCommon.myLen(strSealNo) <= 0 Then
            strSealNo = ""
        End If
        Return strSealNo
    End Function
    Private Sub gvNewSeal_CellEndEdit(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvNewSeal.CellEndEdit
        Dim qry As String = String.Empty
        Dim strItemCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Item_Code  from TSPL_ITEM_MASTER where Product_Type='MS' "))
        If clsCommon.myLen(strItemCode) > 0 Then
        Else
            clsCommon.MyMessageBoxShow(Me, "No Item Of Seal Type Found", Me.Text)
            Exit Sub
        End If
        Dim whrCls As String = String.Empty
        whrCls = getUsedPaperSealNoOnCurrentScreen(gvNewSeal.CurrentRow.Index)
        If Not isCellValueChangedOpen Then
            isCellValueChangedOpen = True
            If e.Column Is gvNewSeal.Columns(colSealName) Then
                qry = " select Auto_Sr_No as SealNo,Location_Code,Item_Code  from TSPL_SERIAL_ITEM  "
                Dim whrCls1 As String = " Item_Code ='" & strItemCode & "' and Location_Code= '" & txtDispatchFrom.Text & "'and In_Out_Type='I' and Auto_Sr_No not in (select seal_no from Tspl_Lost_defect_sealNo_Details union all select Seal_No1 as seal_No  from (select Seal_No1  from TSPL_MCC_Dispatch_Challan union all select  Seal_No2  from TSPL_MCC_Dispatch_Challan union all select Seal_No3  from TSPL_MCC_Dispatch_Challan union all select Seal_No4  from TSPL_MCC_Dispatch_Challan union all select Seal_No5  from TSPL_MCC_Dispatch_Challan union all select Seal_No6  from TSPL_MCC_Dispatch_Challan union all select Seal_No7  from TSPL_MCC_Dispatch_Challan union all select Seal_No8  from TSPL_MCC_Dispatch_Challan union all select Seal_No9  from TSPL_MCC_Dispatch_Challan union all select Seal_No10  from TSPL_MCC_Dispatch_Challan union all select New_Seal_No1  from tspl_milk_transfer_in union all select  New_Seal_No2  from tspl_milk_transfer_in union all select New_Seal_No3  from tspl_milk_transfer_in union all select New_Seal_No4  from tspl_milk_transfer_in union all select New_Seal_No5  from tspl_milk_transfer_in union all select New_Seal_No6  from tspl_milk_transfer_in union all select New_Seal_No7  from tspl_milk_transfer_in union all select New_Seal_No8  from tspl_milk_transfer_in union all select New_Seal_No9  from tspl_milk_transfer_in union all select New_Seal_No10  from tspl_milk_transfer_in  ) xx  where Seal_No1 <>'') "
                If clsCommon.myLen(whrCls) > 0 Then
                    whrCls = " and Auto_Sr_No not in (" & whrCls & ")"
                End If
                whrCls = whrCls1 & "  " & whrCls
                Try
                    gvNewSeal.CurrentRow.Cells(colSealName).Value = clsCommon.myCstr(clsCommon.ShowSelectForm("sealFnd", qry, "SealNO", whrCls, gvNewSeal.CurrentRow.Cells(colSealName).Value, "SealNO", Not isCellValueChangedOpen))
                Catch exx As Exception
                End Try
            End If
            isCellValueChangedOpen = False
        End If
    End Sub
    Function getUsedSealNoOnCurrentScreen(ByVal curRow As Integer) As String
        Dim strSealNo As String = String.Empty
        For i As Integer = 0 To gvNewSealPaper.Rows.Count - 1
            If clsCommon.myLen(gvNewSealPaper.Rows(i).Cells(colSealName).Value) > 0 And i <> curRow Then
                strSealNo = strSealNo & "'" & clsCommon.myCstr(gvNewSealPaper.Rows(i).Cells(colSealName).Value) & "',"
            End If
        Next
        If clsCommon.myLen(strSealNo) > 0 Then
            strSealNo = Microsoft.VisualBasic.Left(strSealNo, Microsoft.VisualBasic.Len(strSealNo) - 1)
        End If
        If clsCommon.myLen(strSealNo) <= 0 Then
            strSealNo = ""
        End If
        Return strSealNo
    End Function
    Private Sub gvNewSealPaper_CellEndEdit(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvNewSealPaper.CellEndEdit
        Dim qry As String = String.Empty
        Dim strItemCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Item_Code  from TSPL_ITEM_MASTER where Product_Type='PS' "))
        If clsCommon.myLen(strItemCode) > 0 Then
        Else
            clsCommon.MyMessageBoxShow(Me, "No Item Of Seal Type Found", Me.Text)
            Exit Sub
        End If
        Dim whrCls As String = String.Empty
        whrCls = getUsedSealNoOnCurrentScreen(gvNewSealPaper.CurrentRow.Index)
        If Not isCellValueChangedOpen Then
            isCellValueChangedOpen = True
            If e.Column Is gvNewSealPaper.Columns(colSealName) Then
                qry = " select Auto_Sr_No as SealNo,Location_Code,Item_Code  from TSPL_SERIAL_ITEM  "
                Dim whrCls1 As String = " Item_Code ='" & strItemCode & "' and Location_Code= '" & txtDispatchFrom.Text & "'and In_Out_Type='I' and Auto_Sr_No not in (select seal_no from Tspl_Lost_defect_sealNo_Details union all select seal_no from  TSPL_Mcc_Dispatch_Challan_Paper_Seal_Details union all select seal_no from  TSPL_Milk_Transfer_In_Paper_Seal_Details) "
                If clsCommon.myLen(whrCls) > 0 Then
                    whrCls = " and Auto_Sr_No not in (" & whrCls & ")"
                End If
                whrCls = whrCls1 & "  " & whrCls
                Try
                    gvNewSealPaper.CurrentRow.Cells(colSealName).Value = clsCommon.myCstr(clsCommon.ShowSelectForm("sealFnd", qry, "SealNO", whrCls, gvNewSealPaper.CurrentRow.Cells(colSealName).Value, "SealNO", Not isCellValueChangedOpen))
                Catch
                End Try
            End If
        End If
        isCellValueChangedOpen = False
    End Sub

    Private Sub GroupBox2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox2.Enter

    End Sub

    Private Sub txtKmReadingRecpt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtKmReadingRecpt.Validating
        If clsCommon.myCdbl(txtKMReadingDisp.Text) > clsCommon.myCdbl(txtKmReadingRecpt.Text) AndAlso clsCommon.myCdbl(txtKmReadingRecpt.Text) > 0 Then
            clsCommon.MyMessageBoxShow(Me, "Receipt KM reading must be more then Dispatch KM reading", Me.Text)
            txtKmReadingRecpt.Focus()
        End If
        If clsCommon.myCdbl(txtKmReadingRecpt.Text) < 0 Then
            clsCommon.MyMessageBoxShow(Me, "Receipt KM reading must not be  Negative", Me.Text)
            txtKmReadingRecpt.Focus()
        End If
    End Sub

    Private Sub txtMccPlantCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtMccPlantCode._MYValidating
        Dim whrCls As String = ""
        If Not clsMccMaster.isCurrentUserHO() Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrCls = " location_Code in ( " & objCommonVar.strCurrUserLocations & ")"
            End If
        End If
        txtMccPlantCode.Value = clsLocation.getFinder(whrCls, txtMccPlantCode.Value, isButtonClicked)
        lblMccPlantName.Text = clsLocation.GetName(txtMccPlantCode.Value, Nothing)
    End Sub

    Function SaveMILKRGPData()
        Try
            Dim strSiloNo As String = clsDBFuncationality.getSingleValue("select Sub_location_Code  from TSPL_MILK_UNLOADING where Gate_Entry_No='" & txtgateEntryNo.Text & "'")
            Dim JobworkLocaion As DataTable = clsDBFuncationality.GetDataTable("select * from tspl_location_Master inner join TSPL_LOCATION_MASTER_Jobwork_Item on TSPL_LOCATION_MASTER_Jobwork_Item.Main_Location_Code=Location_Code where location_Code='" & txtMccPlantCode.Value & "' and coalesce(is_Jobwork,'1')='1' and coalesce(jobwork_vendor,'')<>'' and coalesce(TSPL_LOCATION_MASTER_Jobwork_Item.jobwork_Item,'')<>''")
            If JobworkLocaion.Rows.Count <= 0 Then
                Return True
            End If
            Dim dblAmount As Double = 0
            Dim strItem As String = Nothing
            For Each grow As GridViewRowInfo In gvWeighment.Rows
                dblAmount += clsCommon.myCdbl(grow.Cells(colRcptAmt).Value)
                strItem = clsCommon.myCstr(grow.Cells(colItemCode).Value)
            Next
            For Each rows As DataRow In JobworkLocaion.Rows
                If clsCommon.CompairString(clsCommon.myCstr(rows.Item("JObwork_Item1")), strItem) = CompairStringResult.Equal Then 'clsCommon.CompairString(clsCommon.myCstr(JobworkLocaion.Rows(0).Item("JObwork_Vendor")), txtVendor.Text) = CompairStringResult.Equal And

                    Dim obj As New clsMilkRGPHead()

                    obj.chklocstion = "N"
                    obj.srnlocation = ""

                    obj.RGP_No = ""
                    obj.RGP_Date = dtpRcptDateAndTime.Value
                    obj.Doc_Type = "RGP"
                    ''richa Ticket No BM00000003061 on 01/08/2014

                    '-------------------------------------------
                    obj.Mode_Of_Transport = ""
                    obj.Cash_Memo_Detail = ""
                    obj.Vendor_Code = clsCommon.myCstr(JobworkLocaion.Rows(0).Item("JObwork_Vendor")) 'txtVendor.Text
                    obj.Vendor_Name = clsDBFuncationality.getSingleValue("select vendor_name from tspl_Vendor_Master where Vendor_Code='" & clsCommon.myCstr(JobworkLocaion.Rows(0).Item("JObwork_Vendor")) & "'") 'lblVendorName.Text
                    obj.VehicleNo = ""
                    obj.GPNo = ""
                    obj.GPDate = Nothing
                    obj.Remarks = "Auto Milk RGP from Milk Transfer In - " & fndDispatchChallanNo.Value & ""
                    obj.Reason = Nothing

                    obj.Document_Amount = dblAmount
                    obj.Delivered_ByName = clsCommon.myCstr(objCommonVar.CurrentUser)
                    obj.Location = txtMccPlantCode.Value
                    obj.Delivered_By = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select emp_COde from tspl_EMployee_Master where User_Code='" & objCommonVar.CurrentUserCode & "'"))
                    If clsCommon.myLen(obj.Delivered_By) <= 0 Then
                        obj.Delivered_By = objCommonVar.CurrentUserCode
                    End If
                    obj.Department = Nothing

                    obj.CostCentre = Nothing
                    obj.CostCentreDesc = Nothing
                    '' Anubhooti 09-Oct-2014 BM00000003663
                    ''
                    '' Anubhooti 10-Dec-2014 BM00000003662
                    obj.Item_Conversion_Type = "N"
                    ''

                    obj.PO_Id = ""
                    obj.Against_As_It_Is = 0
                    obj.Milk_Transfer_In = fndRcptChalanNo.Value

                    obj.Arr = New List(Of clsMilkRGPDetail)
                    For Each grow As GridViewRowInfo In gvWeighment.Rows
                        FillQCGrid(clsCommon.myCstr(grow.Cells(colItemCode).Value), txtMccPlantCode.Value, txtMccPlantCode.Value)
                        obj.ArrQC = New List(Of clsMilkRGPIssueQCDetail)
                        For Each grow_qc As GridViewRowInfo In gv_qc.Rows
                            Dim objtr_qc As New clsMilkRGPIssueQCDetail()

                            objtr_qc.sno = CInt(grow_qc.Cells(colQCsno).Value)
                            objtr_qc.frm_loc_code = clsCommon.myCstr(grow_qc.Cells(colQCloc_code).Value)
                            objtr_qc.to_loc_code = clsCommon.myCstr(grow_qc.Cells(colQCToloc_code).Value)
                            objtr_qc.itemcode = clsCommon.myCstr(grow_qc.Cells(colQCitemcode).Value)
                            objtr_qc.param_code = clsCommon.myCstr(grow_qc.Cells(colQCparamcode).Value)
                            objtr_qc.lrange = clsCommon.myCdbl(grow_qc.Cells(colQCrange1).Value)
                            objtr_qc.urange = clsCommon.myCdbl(grow_qc.Cells(colQCrange2).Value)
                            objtr_qc.value1 = clsCommon.myCstr(grow_qc.Cells(colQCvalue1).Value)
                            objtr_qc.value2 = clsCommon.myCstr(grow_qc.Cells(colQCvalue2).Value)
                            objtr_qc.status_grid = clsCommon.myCstr(grow_qc.Cells(colQCstatus).Value)
                            objtr_qc.QCRange = clsCommon.myCdbl(grow_qc.Cells(colQCRange).Value)
                            objtr_qc.QCStatus = clsCommon.myCstr(grow_qc.Cells(colQCOutStatus).Value)
                            If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select type from tspl_parameter_master where code='" & objtr_qc.param_code & "'")), "FAT") = CompairStringResult.Equal Then
                                objtr_qc.QCValue = clsCommon.myCdbl(grow.Cells(colFATPer).Value)
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select type from tspl_parameter_master where code='" & objtr_qc.param_code & "'")), "SNF") = CompairStringResult.Equal Then
                                objtr_qc.QCValue = clsCommon.myCdbl(grow.Cells(colSNFPer).Value)
                            Else
                                objtr_qc.QCValue = clsCommon.myCstr(grow_qc.Cells(colQCValue).Value)
                            End If


                            If objtr_qc.status_grid = "None" Then
                                objtr_qc.status_grid = ""
                            End If
                            If objtr_qc.QCStatus = "None" Then
                                objtr_qc.QCStatus = ""
                            End If

                            objtr_qc.remarks = clsCommon.myCstr(grow_qc.Cells(colQCremarks).Value).Replace("'", "`")

                            If clsCommon.myLen(objtr_qc.param_code) > 0 Then
                                obj.ArrQC.Add(objtr_qc)
                            End If
                        Next

                        Dim objTr As New clsMilkRGPDetail()
                        objTr.Line_No = clsCommon.myCdbl(grow.Cells(colSLNo).Value)
                        objTr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                        objTr.Item_Desc = clsCommon.myCstr(grow.Cells(colItemDesc).Value)
                        objTr.RGP_Qty = clsCommon.myCdbl(grow.Cells(colNetWeightRcpt).Value)
                        objTr.Unit_code = clsCommon.myCstr(grow.Cells(colUOM).Value)
                        'objTr.Item_Cost = clsCommon.myCdbl(grow.Cells(colrate).Value) 'clsCommon.myCdbl(grow.Cells(colRate).Value)
                        objTr.Amount = clsCommon.myCdbl(grow.Cells(colRcptAmt).Value)
                        objTr.FAT_pers = clsCommon.myCdbl(grow.Cells(colFATPerQC).Value)
                        objTr.FAT_KG = clsCommon.myCdbl(grow.Cells(colFATKG).Value)
                        objTr.FAT_Price = clsCommon.myCdbl(grow.Cells(colFATValue).Value)
                        objTr.FAT_Cost = clsCommon.myCdbl(grow.Cells(colFATRate).Value)
                        objTr.SNF_Cost = clsCommon.myCdbl(grow.Cells(colSNFRate).Value)
                        objTr.SNF_Pers = clsCommon.myCdbl(grow.Cells(colSNFPerQC).Value)
                        objTr.SNF_KG = clsCommon.myCdbl(grow.Cells(colSNFKG).Value)
                        objTr.SNF_Price = clsCommon.myCdbl(grow.Cells(colSNFValue).Value)
                        objTr.TanKer_No = clsCommon.myCstr(txtTankerNo.Text)
                        'objTr.Bulk_Milk_Srn_No = clsCommon.myCstr(fndRcptChalanNo.Value)
                        objTr.Location_Code = strSiloNo ' clsCommon.myCstr(txtLocation.Text)
                        objTr.Location_Type = clsDBFuncationality.getSingleValue("select case when coalesce(is_section,'N')='Y' then '3' when  coalesce(Is_Sub_Location,'N')='Y' then '2' else '1' end from TSPL_LOCATION_MASTER where Location_Code='" & strSiloNo & "'")
                        objTr.Specification = ""
                        '' Anubhooti 06-Feb-2015(Unit Cost)
                        objTr.Approx_Cost = clsCommon.myCdbl(grow.Cells(colRcptAmt).Value)
                        If clsCommon.myLen(objTr.Item_Code) > 0 Then
                            obj.Arr.Add(objTr)
                        End If
                    Next





                    If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                        common.clsCommon.MyMessageBoxShow(Me, "Please Fill at list one Item", Me.Text)
                        Return False
                    End If

                    '=======================================================

                    '======================================================


                    If (obj.SaveData(obj, True, True)) Then
                        ' clsMilkRGPHead.PostData(obj.RGP_No)
                        clsCommon.MyMessageBoxShow(Me, "RGP [" & obj.RGP_No & "] has been created.", Me.Text)
                    End If
                End If
            Next

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
    End Function
    Private Sub FillQCGrid(ByVal CurrentIcode As String, ByVal Frm_Loc_Code As String, ByVal To_Loc_Code As String)
        Dim qry As String = String.Empty
        Dim check As Integer = 0
        Try

            LoadQCBlankGrid()
            If clsCommon.myLen(gvWeighment.Rows(0).Cells(colItemCode).Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                gvWeighment.Focus()
                gvWeighment.Select()
                Throw New Exception("Fill item detail first.")
            End If
            Dim allicode As String = ""

            qry = "select count(*) from INFORMATION_SCHEMA.TABLES where TABLE_NAME='TEMP_LOC_QC_PARAM'"
            check = clsDBFuncationality.getSingleValue(qry)
            If check > 0 Then
                clsDBFuncationality.ExecuteNonQuery("drop table TEMP_LOC_QC_PARAM")
                clsDBFuncationality.ExecuteNonQuery("create table TEMP_LOC_QC_PARAM (Item_Code varchar(50) null,Frm_Loc varchar(50) null,To_Loc varchar(50) null)")
            Else
                clsDBFuncationality.ExecuteNonQuery("create table TEMP_LOC_QC_PARAM (Item_Code varchar(50) null,Frm_Loc varchar(50) null,To_Loc varchar(50) null)")
            End If

            If CurrentIcode IsNot Nothing AndAlso clsCommon.myLen(CurrentIcode) > 0 Then
                allicode = CurrentIcode
                clsDBFuncationality.ExecuteNonQuery("insert into TEMP_LOC_QC_PARAM select '" + CurrentIcode + "','" + Frm_Loc_Code + "','" + To_Loc_Code + "'")
            Else
                For Each grow As GridViewRowInfo In gvWeighment.Rows
                    allicode = allicode + "','" + clsCommon.myCstr(grow.Cells(colItemCode).Value)
                    clsDBFuncationality.ExecuteNonQuery("insert into TEMP_LOC_QC_PARAM select '" + clsCommon.myCstr(grow.Cells(colItemCode).Value) + "','" + clsCommon.myCstr(txtMccPlantCode.Value) + "','" + clsCommon.myCstr(txtMccPlantCode.Value) + "'")
                Next
            End If

            If clsCommon.myLen(allicode) > 0 AndAlso allicode.Substring(0, 3) = "','" Then
                allicode = allicode.Substring(3, allicode.Length - 3)
            End If

            qry = "select ROW_NUMBER() over(order by TSPL_ITEM_QC_PARAMETER_MASTER.Code) as Sno,TEMP_LOC_QC_PARAM.frm_loc,TEMP_LOC_QC_PARAM.to_loc,TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_QC_PARAMETER_MASTER.Code,TSPL_PARAMETER_MASTER.Description as parameterdesc,TSPL_PARAMETER_MASTER.Type,(Case when TSPL_PARAMETER_MASTER.Nature='A' then 'Alphanumeric' else case when TSPL_PARAMETER_MASTER.Nature='B' then 'Boolean' else case when TSPL_PARAMETER_MASTER.Nature='R' then 'Range' end end end) as Nature,sum(TSPL_ITEM_QC_PARAMETER_MASTER.actual_range)/count(TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code) as Lower_range,sum(TSPL_ITEM_QC_PARAMETER_MASTER.Upper_range)/count(TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code) as Upper_range,max(TSPL_ITEM_QC_PARAMETER_MASTER.actual_value) as Value1,max(TSPL_ITEM_QC_PARAMETER_MASTER.Value2) as Value2,max(TSPL_ITEM_QC_PARAMETER_MASTER.actual_status) as Status from TSPL_ITEM_QC_PARAMETER_MASTER "
            qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code "
            qry += " left outer join TEMP_LOC_QC_PARAM on TEMP_LOC_QC_PARAM.item_code=TSPL_ITEM_QC_PARAMETER_MASTER.item_code "
            qry += " where TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code in ('" + allicode + "') group by TEMP_LOC_QC_PARAM.frm_loc,TEMP_LOC_QC_PARAM.to_loc,TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_QC_PARAMETER_MASTER.Code,TSPL_PARAMETER_MASTER.Description,TSPL_PARAMETER_MASTER.Type,TSPL_PARAMETER_MASTER.Nature"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If gv_qc.Rows.Count > 0 AndAlso clsCommon.myLen(gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCitemcode).Value) <= 0 Then
                gv_qc.Rows.RemoveAt(gv_qc.Rows.Count - 1)
            End If

            Dim found As Boolean = False
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    found = False
                    For Each grow As GridViewRowInfo In gv_qc.Rows
                        If clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colQCparamcode).Value), clsCommon.myCstr(dr("Code"))) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colQCitemcode).Value), clsCommon.myCstr(dr("item_code"))) = CompairStringResult.Equal Then
                            found = True
                            GoTo a
                        End If
                    Next
a:
                    If Not found And clsCommon.myLen(clsCommon.myCstr(dr("frm_loc"))) > 0 Then
                        gv_qc.Rows.AddNew()
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCsno).Value = CInt(dr("sno"))
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCloc_code).Value = clsCommon.myCstr(dr("frm_loc"))
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCloc_name).Value = clsLocation.GetName(clsCommon.myCstr(dr("frm_loc")), Nothing)
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCToloc_code).Value = clsCommon.myCstr(dr("to_loc"))
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCToloc_name).Value = clsLocation.GetName(clsCommon.myCstr(dr("to_loc")), Nothing)
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCitemcode).Value = clsCommon.myCstr(dr("Item_Code"))
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCiname).Value = clsCommon.myCstr(dr("Item_Desc"))
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparamcode).Value = clsCommon.myCstr(dr("Code"))
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparam_desc).Value = clsCommon.myCstr(dr("parameterdesc"))
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparam_type).Value = clsCommon.myCstr(dr("Type"))
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparam_nature).Value = clsCommon.myCstr(dr("Nature"))
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCrange1).Value = clsCommon.myCdbl(dr("Lower_range"))
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCrange2).Value = clsCommon.myCdbl(dr("Upper_range"))
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCstatus).Value = clsCommon.myCstr(dr("Status"))
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCvalue1).Value = clsCommon.myCstr(dr("Value1"))
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCvalue2).Value = clsCommon.myCstr(dr("Value2"))

                        If clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparam_nature).Value), "Range") = CompairStringResult.Equal Then
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCValue).ReadOnly = True
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCOutStatus).ReadOnly = True
                        End If
                        If clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparam_nature).Value), "Boolean") = CompairStringResult.Equal Then
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCRange).ReadOnly = True
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCValue).ReadOnly = True
                        End If
                        If clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparam_nature).Value), "Alphanumeric") = CompairStringResult.Equal Then
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCRange).ReadOnly = True
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCOutStatus).ReadOnly = True
                        End If
                    End If ''found cond.

                Next
            Else
                'Throw New Exception("Mapped first QC parameter with items in Item Master screen")
            End If

            '===========refresh sno==
            For Each grow As GridViewRowInfo In gv_qc.Rows
                grow.Cells(colQCsno).Value = grow.Index + 1
            Next

            dt = Nothing
        Catch ex As Exception
            qry = "select count(*) from INFORMATION_SCHEMA.TABLES where TABLE_NAME='TEMP_LOC_QC_PARAM'"
            check = clsDBFuncationality.getSingleValue(qry)
            If check > 0 Then
                clsDBFuncationality.ExecuteNonQuery("drop table TEMP_LOC_QC_PARAM")
            End If
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            qry = "select count(*) from INFORMATION_SCHEMA.TABLES where TABLE_NAME='TEMP_LOC_QC_PARAM'"
            check = clsDBFuncationality.getSingleValue(qry)
            If check > 0 Then
                clsDBFuncationality.ExecuteNonQuery("drop table TEMP_LOC_QC_PARAM")
            End If

        End Try
    End Sub
    Private Sub LoadQCBlankGrid()
        gv_qc.Rows.Clear()
        gv_qc.Columns.Clear()

        Dim reposno As GridViewDecimalColumn = New GridViewDecimalColumn()
        reposno.FormatString = ""
        reposno.Name = colQCsno
        reposno.Width = 60
        reposno.DecimalPlaces = 0
        reposno.HeaderText = "S.No."
        reposno.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(reposno)
        reposno = Nothing

        Dim bomcodeFrom As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        bomcodeFrom.FormatString = ""
        bomcodeFrom.Name = colQCloc_code
        bomcodeFrom.Width = 80
        bomcodeFrom.HeaderText = "From Location Code"
        bomcodeFrom.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(bomcodeFrom)
        bomcodeFrom = Nothing

        Dim bomcodeFromName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        bomcodeFromName.FormatString = ""
        bomcodeFromName.Name = colQCloc_name
        bomcodeFromName.Width = 130
        bomcodeFromName.HeaderText = "From Location"
        bomcodeFromName.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(bomcodeFromName)
        bomcodeFromName = Nothing

        Dim bomcodeTO As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        bomcodeTO.FormatString = ""
        bomcodeTO.Name = colQCToloc_code
        bomcodeTO.Width = 80
        bomcodeTO.HeaderText = "To Location Code"
        bomcodeTO.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(bomcodeTO)
        bomcodeTO = Nothing

        Dim bomcodeTOName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        bomcodeTOName.FormatString = ""
        bomcodeTOName.Name = colQCToloc_name
        bomcodeTOName.Width = 130
        bomcodeTOName.HeaderText = "To Location"
        bomcodeTOName.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(bomcodeTOName)
        bomcodeTOName = Nothing

        Dim bomcode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        bomcode.FormatString = ""
        bomcode.Name = colQCitemcode
        bomcode.Width = 100
        bomcode.HeaderText = "Item Code"
        bomcode.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(bomcode)
        bomcode = Nothing

        Dim repolocname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repolocname.FormatString = ""
        repolocname.Name = colQCiname
        repolocname.Width = 200
        repolocname.HeaderText = "Item Name"
        repolocname.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(repolocname)
        repolocname = Nothing

        Dim bomcode1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        bomcode1.FormatString = ""
        bomcode1.Name = colQCparamcode
        bomcode1.Width = 100
        bomcode1.HeaderText = "Parameter Code"
        bomcode1.ReadOnly = True
        'bomcode1.HeaderImage = Global.ERP.My.Resources.Resources.search4
        'bomcode1.TextImageRelation = TextImageRelation.TextBeforeImage
        gv_qc.MasterTemplate.Columns.Add(bomcode1)
        bomcode1 = Nothing

        Dim repolocname1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repolocname1.FormatString = ""
        repolocname1.Name = colQCparam_desc
        repolocname1.Width = 200
        repolocname1.HeaderText = "Parameter Description"
        repolocname1.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(repolocname1)
        repolocname1 = Nothing

        Dim repotype As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repotype.FormatString = ""
        repotype.Name = colQCparam_type
        repotype.Width = 80
        repotype.HeaderText = "Parameter Type"
        repotype.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(repotype)
        repotype = Nothing

        Dim reponature As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reponature.FormatString = ""
        reponature.Name = colQCparam_nature
        reponature.Width = 80
        reponature.HeaderText = "Nature"
        reponature.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(reponature)
        reponature = Nothing

        Dim repolower As GridViewDecimalColumn = New GridViewDecimalColumn()
        repolower.Name = colQCrange1
        repolower.Width = 80
        repolower.HeaderText = "Std. Range"
        repolower.DecimalPlaces = 2
        repolower.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(repolower)
        repolower = Nothing

        Dim repoupper As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoupper.Name = colQCrange2
        repoupper.Width = 80
        repoupper.HeaderText = "Upper Range"
        repoupper.DecimalPlaces = 2
        repoupper.IsVisible = False
        gv_qc.MasterTemplate.Columns.Add(repoupper)
        repoupper = Nothing

        Dim repovalue1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repovalue1.Name = colQCvalue1
        repovalue1.Width = 80
        repovalue1.HeaderText = "Std. Value"
        repovalue1.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(repovalue1)
        repovalue1 = Nothing

        Dim repovalue2 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repovalue2.Name = colQCvalue2
        repovalue2.Width = 80
        repovalue2.HeaderText = "Value-2"
        repovalue2.MaxLength = 30
        repovalue2.IsVisible = False
        gv_qc.MasterTemplate.Columns.Add(repovalue2)
        repovalue2 = Nothing

        Dim repostatus As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repostatus.Name = colQCstatus
        repostatus.Width = 80
        repostatus.HeaderText = "Std. Status(Yes/No)"
        repostatus.DataSource = LoadCombobox()
        repostatus.ValueMember = "Code"
        repostatus.DisplayMember = "Name"
        repostatus.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(repostatus)
        repostatus = Nothing

        Dim repoupper1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoupper1.Name = colQCRange
        repoupper1.Width = 80
        repoupper1.HeaderText = "Actual Range"
        repoupper1.DecimalPlaces = 2
        gv_qc.MasterTemplate.Columns.Add(repoupper1)
        repoupper1 = Nothing

        Dim repovalue21 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repovalue21.Name = colQCValue
        repovalue21.Width = 80
        repovalue21.HeaderText = "Actual Value"
        repovalue21.MaxLength = 30
        repovalue21.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(repovalue21)
        repovalue21 = Nothing

        Dim repostatus1 As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repostatus1.Name = colQCOutStatus
        repostatus1.Width = 80
        repostatus1.HeaderText = "Actual Status(Yes/No)"
        repostatus1.DataSource = LoadCombobox()
        repostatus1.ValueMember = "Code"
        repostatus1.DisplayMember = "Name"
        'repostatus.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(repostatus1)
        repostatus1 = Nothing

        Dim repoHis As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoHis.FormatString = ""
        repoHis.Name = colQCHistort
        repoHis.Width = 80
        repoHis.ReadOnly = True
        repoHis.HeaderText = "History"
        gv_qc.MasterTemplate.Columns.Add(repoHis)
        repoHis = Nothing

        Dim reporem As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reporem.FormatString = ""
        reporem.Name = colQCremarks
        reporem.Width = 150
        reporem.MaxLength = 200
        reporem.HeaderText = "Remarks"
        gv_qc.MasterTemplate.Columns.Add(reporem)
        reporem = Nothing

        gv_qc.AllowDeleteRow = True
        gv_qc.AllowAddNewRow = False
        gv_qc.ShowGroupPanel = False
        gv_qc.AllowColumnReorder = True
        gv_qc.AllowRowReorder = False
        gv_qc.EnableSorting = False
        gv_qc.EnableFiltering = False
        gv_qc.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv_qc.MasterTemplate.ShowRowHeaderColumn = False
        gv_qc.Rows.AddNew()
    End Sub
    Function LoadCombobox() As DataTable
        Dim qry As String = "select * from (select '' as Code,'None' as Name union all select 'YES' as Code,'YES' as Name union all select 'NO' as Code,'NO' as Name)a"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Return dt
    End Function


    Private Sub fndPriceChart__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndPriceChart._MYValidating
        fndPriceChart.Value = clsPriceChartBulkProc.getFinder("", fndPriceChart.Value, isButtonClicked)
        If clsCommon.myLen(fndPriceChart.Value) > 0 Then
            Dim objP As clsPriceChartBulkProc = clsPriceChartBulkProc.GetData(fndPriceChart.Value, NavigatorType.Current)
            If objP IsNot Nothing Then
                TxtFatWeightage.Text = objP.Fat_Weightage
                TxtSNFWeightage.Text = objP.Snf_Weightage
                txtfatPercentage.Text = objP.Fat_Percentage
                txtSNFPercentage.Text = objP.Snf_Percentage
                txtTransferPrice.Value = objP.Standard_Rate
            Else

                TxtFatWeightage.Text = ""
                TxtSNFWeightage.Text = ""
                txtfatPercentage.Text = ""
                txtSNFPercentage.Text = ""
                gvWeighment.Rows(0).Cells(colFATRate).Value = ""
                gvWeighment.Rows(0).Cells(colSNFRate).Value = ""
                txtTransferPrice.Value = 0
            End If
        Else
            TxtFatWeightage.Text = ""
            TxtSNFWeightage.Text = ""
            txtfatPercentage.Text = ""
            txtSNFPercentage.Text = ""
            gvWeighment.Rows(0).Cells(colFATRate).Value = 0
            gvWeighment.Rows(0).Cells(colSNFRate).Value = 0
            txtTransferPrice.Value = 0
        End If
        For ii As Integer = 0 To gvWeighment.Rows.Count - 1
            Dim strIcode As String = clsCommon.myCstr(gvWeighment.Rows(ii).Cells(colItemCode).Value)
            If clsCommon.myLen(strIcode) > 0 Then
                CalculateCurrentRow(ii)
            End If
        Next
    End Sub

    Private Sub fndMilkTransferInNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndMilkTransferInNo._MYValidating
        Dim qry = "select Receipt_Challan_No,CONVERT(VARCHAR(15),Receipt_Challan_Date,103) AS Receipt_Challan_Date ,Dispatch_Challan_No,Weighment_No,Qc_No,Gate_Entry_no from TSPL_MILK_TRANSFER_IN "
        Dim whrCls As String = " isPosted=1 and Receipt_Challan_No not in (select Receipt_Challan_No from TSPL_MILK_TRANSFER_IN_RETURN )"
        fndMilkTransferInNo.Value = clsCommon.ShowSelectForm("MITNKRNO", qry, "Receipt_Challan_No", whrCls, fndMilkTransferInNo.Value, "Receipt_Challan_No", isButtonClicked)
        If clsCommon.myLen(fndMilkTransferInNo.Value) > 0 Then
            loadDispatchData(fndMilkTransferInNo.Value)
        Else
            reset()
        End If
    End Sub

    ' Ticket : TEC/29/10/18-000351 By Sanjay
    Private Sub btnShowInventory_Click(sender As Object, e As EventArgs) Handles btnShowInventory.Click
        clsOpenInventory.ShowInventoryDatails(fndRcptChalanNo.Value)
    End Sub
End Class
